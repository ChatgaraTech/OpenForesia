// =====================================================================
// LP-009 — le hub de l'auberge : un téléporteur dans chaque réception, vers la carte 82 vidée.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-28-add-legacy-plus-inn-hub). Contenu nouveau,
// demandé par Vincent : un lieu à nous, futur hôte des marchands et d'autres fonctions. Le hub est
// la sous-zone 86 de la zone 0 — un emplacement vide de la table (étape 1, legacy.md) — chargée avec
// le répertoire de la 82 « Le coffre », dont seuls la porte et le coffre de stockage restent.
//
//   - le téléporteur : dans une réception qui porte la porte de la 82, le téléporteur rouge du hub
//     (la greffe de décor de LP-012, AreneTeleporteur) est dessiné près du mur ouest ; qui entre
//     dans son rayon part aussitôt pour le hub, la réception d'origine est retenue (design D4). Il
//     remplace depuis le 2026-09-30 une PORTE ajoutée contre ce mur (le modèle de la porte de la 82,
//     bouton d'action face au mur) — demande de Vincent ;
//   - le hub : l'entrée d'archive de la 82 sous le numéro 86 (D2), les meubles rendus au néant dès
//     leur naissance, sans décaler le rang des entités gardées (D3) ;
//   - le retour : le transfert scripté qui sort du hub est renvoyé vers la réception d'origine, et le
//     groupe y est posé à côté du téléporteur (D5) ;
//   - la sauvegarde : dans le hub, le lieu de reprise écrit est celui de la réception d'origine (D6).
//
// Tout ce que le tweak décide est écrit dans la console, préfixé [HUB] — lisible dans Rider.
// Ne tire aucun nombre aléatoire : l'écart de placement est tiré par le jeu, le tweak le remplace.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class HubAuberge
{
    /// <summary>La sous-zone du hub (un emplacement vide de la zone 0) et celle dont il prend le contenu.</summary>
    internal const byte Hub = 86;
    private const byte Coffre = 82;

    /// <summary>Le point d'entrée de la 82 pris pour arriver dans le hub. TODO : confirmé par le journal
    /// [HUB] d'un passage ordinaire couloir → 82 (tâche 3.2).</summary>
    private const byte EntreeCoffre = 0;

    /// <summary>Les rangs d'entité gardés dans le hub : la porte (0) et son échelle (1), le corps du
    /// coffre (15) et son couvercle scripté (16) — decor.csv, carte 82.</summary>
    /// <remarks>Les deux effets 14 et 17 (sous-types 12 et 11) ont été essayés comme téléporteur de
    /// l'arène le 2026-09-28 : c'est une flamme (Vincent). Retirés à nouveau.</remarks>
    private static readonly int[] s_Gardes = [0, 1, 15, 16];

    // La porte de la 82 dans la liste de la réception : sous-types 2 (la porte) et 15 (son échelle).
    private const short SourceX = -556, SourceZ = 1000;

    // Le téléporteur de la réception, là où se tenait la porte ajoutée jusqu'au 2026-09-29 : le mur
    // ouest est entre x = -389 et -430 (la porte en -400 était visible côté salle, en -430 derrière
    // le mur), et Vincent, debout devant cette porte, était en (-397, 483). Premier essai en
    // (-310, 483) : le téléporteur était dans le mur ; il est à la place relevée par Vincent
    // (2026-09-30, TracePosition : carte 80, y = 0). Le sol y est à 0 (`OpenForesia.Tools terrain 0 0`).
    private const int TeleporteurX = -182, TeleporteurZ = 606, TeleporteurSol = 0;

    // Où le groupe revient dans la réception, et où il arrive dans le hub, avec son cap (relevés de
    // Vincent, 2026-09-28).
    // Le retour ne précise pas de hauteur : le jeu prend une face de sol sous (x, z). En (-236, 504),
    // l'estrade passe au-dessus et le groupe s'y retrouvait perché ; (-109, 504) est juste après elle.
    // Depuis le 2026-09-30 : (24, 599), relevé de Vincent, à l'écart du téléporteur (-182, 606).
    private const short RetourX = 24, RetourZ = 599, RetourCap = 1024;
    private const short HubX = -695, HubZ = 888, HubCap = 3072;

    // Le coffre du hub (corps 15 et couvercle 16), déplacé depuis (-1400, 1100) à la place relevée
    // par Vincent ; chaque entité garde sa hauteur (400 et 440).
    private const int RangCorps = 15, RangCouvercle = 16;
    private const short CoffreX = -1337, CoffreZ = 886;

    /// <summary>Le plancher de la 82, dans le repère des sommets de collision (y négatif vers le haut).</summary>
    private const short Plancher = -400;

    private const int Enregistrement = 8;              // dwords par enregistrement d'apparition
    private const int MaxEnregistrements = 0x80;

    // Ce qui survit d'un chargement à l'autre.
    private static byte s_Origine = 0xff;               // la réception d'où l'on est parti
    private static byte s_RepriseOrigine;               // son point de reprise (0x005de044) au départ
    private static bool s_Retour;                       // le prochain placement pose le groupe dans la réception
    private static bool s_Aller;                        // le prochain placement pose le groupe dans le hub
    private static short s_Cap;                         // le cap à donner aux membres après un placement
    private static int s_CapMembres;                    // les membres (bits 0..3) qui ne l'ont pas encore reçu
    private static int s_Ecarts;                        // écarts de placement encore à annuler (x puis z)
    private static uint* s_ListeHub;                    // la liste du hub en cours de peuplement
    private static byte s_Reception = 0xff;             // la réception chargée qui a son téléporteur dessiné
    private static readonly bool[] s_Declenche = new bool[4]; // par membre : déjà parti, pas encore ressorti du rayon

    // Mémoire native gardée tant que le jeu tourne.
    private static uint* s_ListeHubCopie;
    private static uint* s_PointRetour;
    private static int* s_PlacementTeleporteur;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        s_PlacementTeleporteur = AreneTeleporteur.NouveauPlacement(TeleporteurX, TeleporteurSol + AreneTeleporteur.Hauteur, TeleporteurZ);
        s_ListeHubCopie = (uint*)NativeMemory.AllocZeroed((nuint)((MaxEnregistrements + 1) * Enregistrement), sizeof(uint));
        s_PointRetour = (uint*)NativeMemory.AllocZeroed(Enregistrement, sizeof(uint));

        Coutures.Repertoire repertoire = Coutures.EntreeSousZone;
        Coutures.EntreeSousZone = (table, sousZone) =>
            ZoneAuberge() && sousZone == Hub ? repertoire(table, Coffre) : repertoire(table, sousZone);

        Coutures.Liste liste = Coutures.ListeDApparition;
        Coutures.ListeDApparition = (indice, l) => Liste(indice, liste(indice, l));

        Coutures.Naissance naissance = Coutures.DecorNe;
        Coutures.DecorNe = (enregistrement, entite) =>
        {
            naissance(enregistrement, entite);
            Naitre(enregistrement, entite);
        };

        Coutures.Pas pas = Coutures.PasDuMembre;
        Coutures.PasDuMembre = (membre, pad, perChar, descripteur) =>
        {
            pas(membre, pad, perChar, descripteur);
            if (s_CapMembres != 0) Orienter(membre);
            Teleporteur(membre, descripteur);
        };

        Coutures.Dessin dessin = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = dessin(otBase, curseur);
            return TeleporteurIci() ? AreneTeleporteur.Dessiner(otBase, curseur, s_PlacementTeleporteur) : curseur;
        };

        Action transfert = Coutures.TransfertScripte;
        Coutures.TransfertScripte = () =>
        {
            transfert();
            Transfert();
        };

        Coutures.Point point = Coutures.PointDEntree;
        Coutures.PointDEntree = p => Arrivee(point(p));

        Func<int, int> ecart = Coutures.EcartDeReprise;
        Coutures.EcartDeReprise = e =>
        {
            e = ecart(e);
            if (s_Ecarts <= 0) return e;
            s_Ecarts--;
            return 0;
        };

        Coutures.Lieu lieu = Coutures.LieuSauve;
        Coutures.LieuSauve = entete =>
        {
            lieu(entete);
            Sauvegarde(entete);
        };

        Journal($"actif — téléporteur en ({TeleporteurX}, {TeleporteurZ}), hub = sous-zone {Hub} (contenu de la {Coffre})");
    }

    private static bool ZoneAuberge() => (Jeu.Res.s_Unknown_005de528 & 0xff) == 0;

    /// <summary>La sous-zone chargée est une réception dont le téléporteur a été préparé.</summary>
    private static bool TeleporteurIci() =>
        s_Reception != 0xff && ZoneAuberge() && Jeu.s_Unknown_00566c88 == s_Reception;

    /// <summary>Une réception : la pièce 0 d'un palier, de 0 à 80.</summary>
    private static bool EstReception(byte sousZone) => sousZone % 8 == 0 && sousZone <= 80;

    // ---- le peuplement --------------------------------------------------------------------------

    /// <summary>
    /// Dans une réception, la liste qui porte la porte de la 82 décide du téléporteur vers le hub
    /// (D4) : sa greffe est préparée (la page de texture renvoyée en VRAM à chaque peuplement), la
    /// liste n'est pas touchée. Dans le hub, la liste est notée pour <see cref="Naitre"/>.
    /// </summary>
    private static uint* Liste(int indice, uint* liste)
    {
        if (indice == 0xff || liste == null || !ZoneAuberge()) return liste;

        byte chargee = Jeu.s_Unknown_00566c88;
        if (s_Reception != chargee) s_Reception = 0xff;
        if (chargee == Hub)
        {
            Aplanir();
            s_ListeHub = CopieDuHub(liste);
            return s_ListeHub;
        }
        s_ListeHub = null;
        if (!EstReception(chargee)) return liste;

        bool porte = false;
        for (int n = 0; n < MaxEnregistrements && liste[n * Enregistrement] != 0xffffffff; n++)
        {
            byte* r = (byte*)(liste + n * Enregistrement);
            if (*(short*)(r + 4) == SourceX && *(short*)(r + 8) == SourceZ && r[0x1d] == 2) porte = true;
        }
        if (!porte) return liste;

        bool nouveau = s_Reception != chargee;
        s_Reception = AreneTeleporteur.PreparerGreffe() ? chargee : (byte)0xff;
        if (nouveau)
            Journal(s_Reception == chargee
                ? $"réception {chargee}, liste {indice} : téléporteur vers le hub en ({TeleporteurX}, {TeleporteurZ})"
                : $"réception {chargee} : téléporteur impossible à greffer, pas de passage vers le hub");
        return liste;
    }

    /// <summary>
    /// Les meubles de la 82 sont aussi dans son maillage de collision (le morceau 0, celui que
    /// <c>TryPlaceEntity</c> reçoit) : des faces de sol SURÉLEVÉES — de -432 à -520 pour un plancher à
    /// -400 —, sans plancher dessous. Relevé hors jeu par <c>OpenForesia.Tools terrain 0 82</c>. Dans
    /// le hub, chaque sommet d'une face de sol plus haute que le plancher y est ramené, et le bit
    /// <c>0x0800</c> (on ne s'y tient pas) est effacé. Le morceau est rechargé à chaque entrée dans le
    /// hub ; le geste est sans effet la seconde fois.
    /// </summary>
    private static void Aplanir()
    {
        byte* morceau = *(byte**)Jeu.Mem.Entity(0x005f7bc0);
        if (morceau == null) return;

        byte* matieres = *(byte**)(morceau + 0x24);
        uint* face = *(uint**)(morceau + 0x28);
        byte* sommets = morceau + 0x40;
        int nombre = *(ushort*)(morceau + 4);
        int faces = 0, points = 0;

        for (int i = 0; i < nombre; i++, face += 4)
        {
            face[3] &= ~0x0800u;
            if (*(short*)(matieres + 2 + (ushort)face[2] * 8) >= 0) continue;   // pas une face de sol

            bool haute = false;
            int n = (face[3] & 1) != 0 ? 4 : 3;
            for (int k = 0; k < n; k++)
            {
                uint v = k switch { 0 => face[0] & 0xffff, 1 => face[0] >> 16, 2 => face[1] & 0xffff, _ => face[1] >> 16 };
                short* y = (short*)(sommets + v * 8 + 2);
                if (*y >= Plancher) continue;
                *y = Plancher;
                haute = true;
                points++;
            }
            if (haute) faces++;
        }

        if (faces > 0) Journal($"hub : {faces} face(s) de meuble ramenée(s) au plancher ({points} sommets)");
    }

    /// <summary>
    /// La liste du hub : celle de la 82, recopiée, le coffre (corps et couvercle) posé à sa nouvelle
    /// place. Les rangs ne bougent pas, <see cref="Naitre"/> les lit dans cette copie.
    /// </summary>
    private static uint* CopieDuHub(uint* liste)
    {
        int n = 0;
        while (n < MaxEnregistrements && liste[n * Enregistrement] != 0xffffffff) n++;
        long octets = (long)n * Enregistrement * 4;
        Buffer.MemoryCopy(liste, s_ListeHubCopie, octets, octets);
        s_ListeHubCopie[n * Enregistrement] = 0xffffffff;

        foreach (int rang in (ReadOnlySpan<int>)[RangCorps, RangCouvercle])
        {
            if (rang >= n) continue;
            byte* r = (byte*)(s_ListeHubCopie + rang * Enregistrement);
            *(short*)(r + 4) = CoffreX;
            *(short*)(r + 8) = CoffreZ;
        }
        return s_ListeHubCopie;
    }

    /// <summary>Dans le hub, tout ce qui n'est ni la porte ni le coffre retourne au néant (D3).</summary>
    private static void Naitre(uint* enregistrement, uint* entite)
    {
        if (s_ListeHub == null || Jeu.s_Unknown_00566c88 != Hub) return;

        long rang = (enregistrement - s_ListeHub) / Enregistrement;
        if (rang < 0 || rang >= MaxEnregistrements) return;
        if (Array.IndexOf(s_Gardes, (int)rang) >= 0) return;

        *entite = 0;   // un emplacement libre, comme ClearEntitySlots le laisse
    }

    // ---- l'aller ---------------------------------------------------------------------------------

    /// <summary>
    /// Le téléporteur de la réception : un membre qui entre dans son rayon (la même règle que celui de
    /// l'arène, <see cref="AreneTeleporteur.EntreDansLeRayon"/>) part aussitôt pour le hub. Une
    /// question le précédait jusqu'au 2026-09-29 ; retirée à la demande de Vincent (2026-09-30).
    /// </summary>
    private static void Teleporteur(uint* membre, byte* descripteur)
    {
        if (!TeleporteurIci()) return;
        if (!AreneTeleporteur.EntreDansLeRayon(membre, descripteur, TeleporteurX, TeleporteurZ, s_Declenche)) return;
        Journal($"J{(((byte*)membre)[8] & 3) + 1} entre dans le téléporteur de la réception {s_Reception}");
        Passer(membre);
    }

    /// <summary>
    /// Le passage vers le hub (D4), depuis la réception chargée — pour CHAQUE joueur qui entre dans le
    /// téléporteur. Comme une porte du jeu (opcode 17) : le membre est marqué parti et figé, et la
    /// salle ne se recharge que quand tous les membres le sont (<c>PrepareLeaderAndPartyFlags</c>,
    /// <c>0x005de030 &gt;= taille du groupe</c>). À plusieurs, le second joueur refait donc le même
    /// geste ; le refuser (« un passage est engagé ») laissait le premier figé pour toujours (essai de
    /// Vincent en multijoueur, 2026-09-30).
    /// </summary>
    private static void Passer(uint* membre)
    {
        byte chargee = Jeu.s_Unknown_00566c88;
        bool dejaEngage = Jeu.s_Unknown_005de52c == Hub;
        if (Jeu.s_Unknown_005de52c != chargee && !dejaEngage)   // un autre passage est engagé (une porte, un script)
        {
            Journal($"téléporteur ignoré : un autre passage est engagé (sous-zone chargée {chargee}, demandée {Jeu.s_Unknown_005de52c})");
            return;
        }

        byte* m = (byte*)membre;

        // Le geste de l'opcode 17 (ScriptOpTransferArea), zone inchangée.
        *(ushort*)(m + 0x12) += 1;
        *(short*)(m + 0x98) = *(short*)(m + 0x32);
        *membre = *membre & 0xf7fffdff | 0x2000;

        if (dejaEngage)
        {
            // Les globales du passage sont déjà posées par le premier : seul ce membre part à son tour.
            Journal($"J{m[8] + 1} prend le téléporteur à son tour (passage vers le hub déjà engagé)");
            return;
        }

        s_Origine = chargee;
        s_Aller = true;
        s_RepriseOrigine = *(byte*)Jeu.Mem.Camera(0x005de044);
        Jeu.s_Unknown_005de52c = Hub;
        Jeu.s_Unknown_005de530 = EntreeCoffre;
        Jeu.s_Unknown_005de53c = m[8];
        Jeu.s_WorldMapStep_005645f8 = 2;

        Journal($"J{m[8] + 1} prend le téléporteur : réception {chargee} (reprise {s_RepriseOrigine}) → hub, entrée {EntreeCoffre}");
    }

    /// <summary>
    /// L'arène (LP-012) renvoie le groupe au hub : le prochain placement est une arrivée dans le hub,
    /// comme par le téléporteur de la réception — même place, même cap, aucun script d'arrivée. La
    /// réception d'origine, retenue au départ, reste celle où mène la sortie du hub.
    /// </summary>
    internal static void RetourDArene()
    {
        s_Aller = true;
        s_Retour = false;
    }

    // ---- le retour -------------------------------------------------------------------------------

    /// <summary>
    /// Chaque transfert scripté de la zone 0 est journalisé (il dit le point d'entrée que le couloir
    /// donne à la 82). Celui qui sort du hub est renvoyé vers la réception d'origine (D5).
    /// </summary>
    private static void Transfert()
    {
        if (!ZoneAuberge()) return;
        byte chargee = Jeu.s_Unknown_00566c88;
        Journal($"transfert scripté : sous-zone {chargee} → {Jeu.s_Unknown_005de52c}, entrée {Jeu.s_Unknown_005de530}");

        if (chargee != Hub) return;

        byte vers = EstReception(s_Origine) ? s_Origine : (byte)(Jeu.s_Unknown_005de52c & 0xf8);
        Jeu.s_Unknown_005de52c = vers;
        s_Retour = true;
        Journal($"sortie du hub → réception {vers}, entrée {Jeu.s_Unknown_005de530}");
    }

    /// <summary>
    /// À l'arrivée dans le hub comme au retour, le point d'entrée des données est recopié avec la
    /// position voulue et sans face imposée : le jeu cherche alors la face sous le groupe, et les
    /// deux écarts qu'il tire sont annulés (<see cref="Coutures.EcartDeReprise"/>). La section (octet
    /// bas du premier dword) est gardée.
    ///
    /// <para>Dans le hub, la moitié haute du premier dword — le script d'arrivée, que la boucle de la
    /// carte lance au pas 2 (<c>0x005de048</c>) — passe à <c>0xffff</c>, « aucun script » pour
    /// <c>RunScript</c> : c'est ce script qui affiche « - Le coffre - ».</para>
    /// </summary>
    private static uint* Arrivee(uint* point)
    {
        bool aller = s_Aller, retour = s_Retour;
        s_Aller = s_Retour = false;
        if (point == null || !ZoneAuberge()) return point;

        byte chargee = Jeu.s_Unknown_00566c88;
        short x, z, cap;
        if (aller && chargee == Hub) { x = HubX; z = HubZ; cap = HubCap; }
        else if (retour && EstReception(chargee)) { x = RetourX; z = RetourZ; cap = RetourCap; }
        else return point;

        for (int i = 0; i < Enregistrement; i++) s_PointRetour[i] = point[i];
        byte* r = (byte*)s_PointRetour;
        *(short*)(r + 4) = x;
        *(short*)(r + 8) = z;
        s_PointRetour[4] = 0xffffffff;
        s_PointRetour[5] = 0xffffffff;
        if (aller) s_PointRetour[0] |= 0xffff0000;
        s_Ecarts = 2;
        s_Cap = cap;
        s_CapMembres = 0xf;

        Journal($"placement en ({x}, {z}), cap {cap}, section {point[0] & 0xff}, script d'arrivée " +
                $"{point[0] >> 16}{(aller ? " → aucun" : "")}");
        return s_PointRetour;
    }

    /// <summary>Le cap voulu, donné à chaque membre à son premier pas après le placement.</summary>
    private static void Orienter(uint* membre)
    {
        byte* m = (byte*)membre;
        int bit = 1 << (m[8] & 3);
        if ((s_CapMembres & bit) == 0) return;
        s_CapMembres &= ~bit;
        *(short*)(m + 0x32) = s_Cap;
    }

    // ---- la sauvegarde ---------------------------------------------------------------------------

    /// <summary>Dans le hub, la sauvegarde retient la réception d'origine (D6). L'arène (LP-012) y
    /// renvoie la sienne, après avoir écrit le hub comme lieu.</summary>
    internal static void Sauvegarde(byte* entete)
    {
        if (entete[1] != 0 || entete[2] != Hub) return;

        byte reception = EstReception(s_Origine) ? s_Origine : (byte)80;
        entete[2] = reception;
        entete[3] = s_RepriseOrigine;
        Journal($"sauvegarde dans le hub : lieu de reprise écrit = réception {reception}, reprise {s_RepriseOrigine}");
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[HUB] {message}");
}
