// =====================================================================
// LP-009 — le hub de l'auberge : une porte dans chaque réception, vers la carte 82 vidée.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-28-add-legacy-plus-inn-hub). Contenu nouveau,
// demandé par Vincent : un lieu à nous, futur hôte des marchands et d'autres fonctions. Le hub est
// la sous-zone 86 de la zone 0 — un emplacement vide de la table (étape 1, legacy.md) — chargée avec
// le répertoire de la 82 « Le coffre », dont seuls la porte et le coffre de stockage restent.
//
//   - la porte : un enregistrement d'apparition ajouté à la liste de la réception qui porte déjà la
//     porte de la 82 (même modèle, même orientation, autre place) ; face à elle, bouton d'action :
//     on part pour le hub, la réception d'origine est retenue (design D4) ;
//   - le hub : l'entrée d'archive de la 82 sous le numéro 86 (D2), les meubles rendus au néant dès
//     leur naissance, sans décaler le rang des entités gardées (D3) ;
//   - le retour : le transfert scripté qui sort du hub est renvoyé vers la réception d'origine, et le
//     groupe y est posé devant la porte ajoutée (D5) ;
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
    private const byte Hub = 86, Coffre = 82;

    /// <summary>Le point d'entrée de la 82 pris pour arriver dans le hub. TODO : confirmé par le journal
    /// [HUB] d'un passage ordinaire couloir → 82 (tâche 3.2).</summary>
    private const byte EntreeCoffre = 0;

    /// <summary>Les rangs d'entité gardés dans le hub : la porte (0) et son échelle (1), le corps du
    /// coffre (15) et son couvercle scripté (16) — decor.csv, carte 82.</summary>
    private static readonly int[] s_Gardes = [0, 1, 15, 16];

    // La porte de la 82 dans la liste de la réception : sous-types 2 (la porte) et 15 (son échelle).
    private const short SourceX = -556, SourceZ = 1000;

    // La porte ajoutée, contre le mur ouest de la salle (relevé de Vincent, 2026-09-28 : debout face
    // au mur en x = -389, z = 583, cap 3072). Premier essai à -430 : la porte était DERRIÈRE le mur —
    // le plan du mur est donc entre -389 et -430. TODO : PorteX à ajuster au second essai.
    // Second essai à -400 : visible côté salle (Vincent, 2026-09-28).
    private const short PorteX = -400, PorteY = 0, PorteZ = 583;

    // Le point d'ancrage de la porte est son GOND : le battant s'étend vers les z décroissants, et
    // Vincent, debout devant la porte, était en (-397, 483). On passe donc la porte depuis une bande
    // de sol devant le mur, centrée sur z = 483, en regardant le mur (vers les x décroissants).
    private const int DevantZ = 483, DemiLargeur = 100;   // z de 383 à 583 : du milieu du battant au gond
    private const int DevantXMin = -20, DevantXMax = 100; // écart au plan de la porte, côté salle
    private const int CapMur = 3072;                      // regarder vers les x décroissants
    private const int DemiAngle = 0x200;                  // 45°, en 4096e de tour

    // Où le groupe revient dans la réception, et où il arrive dans le hub, avec son cap (relevés de
    // Vincent, 2026-09-28).
    // Le retour ne précise pas de hauteur : le jeu prend une face de sol sous (x, z). En (-236, 504),
    // l'estrade passe au-dessus et le groupe s'y retrouvait perché ; (-109, 504) est juste après elle.
    private const short RetourX = -109, RetourZ = 504, RetourCap = 1024;
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
    private static bool s_Retour;                       // le prochain placement pose le groupe devant la porte
    private static bool s_Aller;                        // le prochain placement pose le groupe dans le hub
    private static short s_Cap;                         // le cap à donner aux membres après un placement
    private static int s_CapMembres;                    // les membres (bits 0..3) qui ne l'ont pas encore reçu
    private static int s_Ecarts;                        // écarts de placement encore à annuler (x puis z)
    private static uint* s_ListeHub;                    // la liste du hub en cours de peuplement
    private static int s_RangPorte, s_NbPorte;          // où la porte ajoutée est rangée dans s_ListeReception

    // Mémoire native gardée tant que le jeu tourne.
    private static uint* s_ListeReception;
    private static uint* s_ListeHubCopie;
    private static uint* s_PointRetour;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        s_ListeReception = (uint*)NativeMemory.AllocZeroed((nuint)((MaxEnregistrements + 3) * Enregistrement), sizeof(uint));
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
            Porte(membre, pad, perChar, descripteur);
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

        Journal($"actif — porte en ({PorteX}, {PorteY}, {PorteZ}), hub = sous-zone {Hub} (contenu de la {Coffre})");
    }

    private static bool ZoneAuberge() => (Jeu.Res.s_Unknown_005de528 & 0xff) == 0;

    /// <summary>Une réception : la pièce 0 d'un palier, de 0 à 80.</summary>
    private static bool EstReception(byte sousZone) => sousZone % 8 == 0 && sousZone <= 80;

    // ---- le peuplement --------------------------------------------------------------------------

    /// <summary>
    /// Dans une réception, la liste qui porte la porte de la 82 reçoit la porte du hub (D4), copiée à
    /// la suite : les rangs d'origine ne bougent pas. Dans le hub, la liste est notée pour
    /// <see cref="Naitre"/>.
    /// </summary>
    private static uint* Liste(int indice, uint* liste)
    {
        if (indice == 0xff || liste == null || !ZoneAuberge()) return liste;

        byte chargee = Jeu.s_Unknown_00566c88;
        if (chargee == Hub)
        {
            Aplanir();
            s_ListeHub = CopieDuHub(liste);
            return s_ListeHub;
        }
        s_ListeHub = null;
        if (!EstReception(chargee)) return liste;

        int n = 0;
        uint* porte = null, echelle = null;
        for (; n < MaxEnregistrements && liste[n * Enregistrement] != 0xffffffff; n++)
        {
            byte* r = (byte*)(liste + n * Enregistrement);
            if (*(short*)(r + 4) != SourceX || *(short*)(r + 8) != SourceZ) continue;
            if (r[0x1d] == 2) porte = (uint*)r;
            else if (r[0x1d] == 15) echelle = (uint*)r;
        }
        if (porte == null || n + 2 > MaxEnregistrements) return liste;

        uint* copie = s_ListeReception;
        Buffer.MemoryCopy(liste, copie, (long)n * Enregistrement * 4, (long)n * Enregistrement * 4);
        int ajout = n;
        Poser(copie + ajout++ * Enregistrement, porte);
        if (echelle != null) Poser(copie + ajout++ * Enregistrement, echelle);
        copie[ajout * Enregistrement] = 0xffffffff;
        s_RangPorte = n;
        s_NbPorte = ajout - n;

        Journal($"réception {chargee}, liste {indice} : porte du hub ajoutée (rang {n}, {ajout - n} enregistrement(s))");
        return copie;
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

    /// <summary>Recopie un enregistrement de porte à la place de la porte du hub.</summary>
    private static void Poser(uint* dest, uint* source)
    {
        for (int i = 0; i < Enregistrement; i++) dest[i] = source[i];
        byte* r = (byte*)dest;
        *(short*)(r + 4) = PorteX;
        *(short*)(r + 6) = PorteY;
        *(short*)(r + 8) = PorteZ;
    }

    /// <summary>
    /// Dans une réception, la porte ajoutée ne réagit pas au contact : une porte de l'auberge est un
    /// battant que le groupe pousse en marchant dedans (le gestionnaire 0x004736D0 fait tourner
    /// <c>+0x32</c> de la vitesse reçue), et la nôtre, contre un mur plein, s'ouvrirait sur le mur. Le
    /// bit <c>0x2000</c> est celui que <c>ApplyEntityMotion_00519DF0</c> écarte du contact. Dans le
    /// hub, tout ce qui n'est ni la porte ni le coffre retourne au néant (D3).
    /// </summary>
    private static void Naitre(uint* enregistrement, uint* entite)
    {
        if (s_NbPorte > 0 && EstReception(Jeu.s_Unknown_00566c88)
            && enregistrement >= s_ListeReception + s_RangPorte * Enregistrement
            && enregistrement < s_ListeReception + (s_RangPorte + s_NbPorte) * Enregistrement)
        {
            byte* e = (byte*)entite;
            *entite |= 0x2000;
            Journal($"porte ajoutée née : sous-type {e[5]}, état +0x14 = {*(short*)(e + 0x14)}, +0x16 = {*(short*)(e + 0x16)}, mot 0x{*entite:x8}");
            return;
        }

        if (s_ListeHub == null || Jeu.s_Unknown_00566c88 != Hub) return;

        long rang = (enregistrement - s_ListeHub) / Enregistrement;
        if (rang < 0 || rang >= MaxEnregistrements) return;
        if (Array.IndexOf(s_Gardes, (int)rang) >= 0) return;

        *entite = 0;   // un emplacement libre, comme ClearEntitySlots le laisse
    }

    // ---- l'aller ---------------------------------------------------------------------------------

    /// <summary>Face à la porte ajoutée, bouton d'action : le passage vers le hub (D4).</summary>
    private static void Porte(uint* membre, byte* pad, byte* perChar, byte* descripteur)
    {
        if (!ZoneAuberge()) return;
        byte chargee = Jeu.s_Unknown_00566c88;
        if (!EstReception(chargee)) return;

        ushort action = *(ushort*)(perChar + 0x1fc4);
        if ((*(ushort*)(pad + 2) & action) == 0) return;

        if (Jeu.s_Unknown_005de52c != chargee)   // un passage est déjà engagé
        {
            Journal($"appui ignoré : un passage est engagé (sous-zone chargée {chargee}, demandée {Jeu.s_Unknown_005de52c})");
            return;
        }

        // Un appui près de la porte qui n'ouvre rien dit pourquoi, dans la console.
        byte* m = (byte*)membre;
        int x = *(int*)(m + 0x1c) >> 12, z = *(int*)(m + 0x24) >> 12;
        int devant = x - PorteX;                        // > 0 : côté salle
        int cote = z - DevantZ;
        int cap = *(short*)(m + 0x32) & 0xfff;
        int ecart = (CapMur - cap) & 0xfff;
        if (ecart > 0x800) ecart = 0x1000 - ecart;

        string? refus =
            devant < DevantXMin || devant > DevantXMax || Math.Abs(cote) > DemiLargeur
                ? $"pas devant la porte (x = {x}, z = {z} ; attendu x de {PorteX + DevantXMin} à {PorteX + DevantXMax}, z de {DevantZ - DemiLargeur} à {DevantZ + DemiLargeur})" :
            ecart > DemiAngle ? $"pas tourné vers le mur (cap {cap}, attendu {CapMur} à 45° près)" :
            (*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0 ? $"pas debout (mot 0x{*membre:x8})" :
            (descripteur[0x8c7] & 1) != 0 ? "une boîte est ouverte" :
            null;
        if (refus != null)
        {
            if (Math.Abs(devant) <= 250 && Math.Abs(cote) <= 250) Journal($"J{m[8] + 1} appuie près de la porte, refusé : {refus}");
            return;
        }

        *(ushort*)(pad + 2) &= (ushort)~action;   // l'appui est consommé

        // Le geste de l'opcode 17 (ScriptOpTransferArea), zone inchangée.
        *(ushort*)(m + 0x12) += 1;
        *(short*)(m + 0x98) = *(short*)(m + 0x32);
        *membre = *membre & 0xf7fffdff | 0x2000;

        s_Origine = chargee;
        s_Aller = true;
        s_RepriseOrigine = *(byte*)Jeu.Mem.Camera(0x005de044);
        Jeu.s_Unknown_005de52c = Hub;
        Jeu.s_Unknown_005de530 = EntreeCoffre;
        Jeu.s_Unknown_005de53c = m[8];
        Jeu.s_WorldMapStep_005645f8 = 2;

        Journal($"J{m[8] + 1} passe la porte : réception {chargee} (reprise {s_RepriseOrigine}) → hub, entrée {EntreeCoffre}");
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

    /// <summary>Dans le hub, la sauvegarde retient la réception d'origine (D6).</summary>
    private static void Sauvegarde(byte* entete)
    {
        if (entete[1] != 0 || entete[2] != Hub) return;

        byte reception = EstReception(s_Origine) ? s_Origine : (byte)80;
        entete[2] = reception;
        entete[3] = s_RepriseOrigine;
        Journal($"sauvegarde dans le hub : lieu de reprise écrit = réception {reception}, reprise {s_RepriseOrigine}");
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[HUB] {message}");
}
