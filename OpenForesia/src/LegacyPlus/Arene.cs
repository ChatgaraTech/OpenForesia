// =====================================================================
// LP-012 — le mode arène : l'aller vers 4/9, la salle vidée, la fin (scénette, bourse, portail),
// la défaite, la sauvegarde.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-arena, design D1, D2, D6, D7).
// L'arène est 4/9 « Parmi les Elus », la salle du boss de la vieille tour, chargée sous son propre
// numéro (D1) ; un interrupteur à nous décide de tout le reste :
//
//   - ARMÉ par la confirmation du menu du téléporteur (AreneDirecteur.Choisir) ;
//   - ACTIF au chargement de 4/9 qui suit, et à celui-là seul ;
//   - BAISSÉ au chargement de n'importe quelle autre sous-zone — et de 4/9 aussi, s'il n'est pas
//     armé : la vraie tour, par l'histoire, reste celle de 1998. Le portail et la défaite ramènent
//     au hub par un chargement : ils le baissent donc aussi.
//
// En mode arène :
//   - l'aller est la séquence QUEST du menu de mise au point (CommitDebugWarp), celle que le banc
//     d'essai emploie et que Vincent a éprouvée en jeu ; point d'entrée 0 de 4/9 — la salle du boss,
//     section 0, en (100, −2900) (`OpenForesia.Tools entrees 4 9`) ;
//   - le script d'arrivée de ce point (0x34, qui lance le combat de l'histoire) est remplacé par
//     « aucun script », comme le hub le fait pour « - Le coffre - » ;
//   - AUCUN script de la sous-zone ne tourne (couture ScriptTu, en tête de RunEntityScript) : ni
//     celui de l'entrée de section (0x005de04c, relancé à chaque placement), ni ceux que le boss porte
//     dans son mot de genre (au coup reçu, genre 10 ; à la mort, genre 11). C'est ce qui ôte la
//     cinématique du début du combat (Vincent, 2026-09-28 : « elle sert plus à rien »), et c'est le
//     point unique que le design D6 cherchait pour qu'aucun drapeau ne soit écrit ;
//   - les volumes de déclenchement sont tus (couture VolumesTus) : les sorties de la salle ne mènent
//     nulle part, on en sort par le portail ou la défaite ;
//   - pas de rencontre aléatoire ;
//   - le décor qui agit est rendu au néant dès sa naissance, sans décaler les rangs (D2) : le coffre,
//     l'effet 56, les gonds de la tour (leur verrou est un banc persistant) et le sous-type 11. La
//     scénette de sortie (sous-type 22, 0x00474380) reste : son état 0 la cache ;
//   - une sauvegarde écrit le lieu du hub, donc la réception d'origine (D6).
//
// LA FIN (lot 4). Quand le directeur a vu tomber la dernière vague :
//   1. la scénette de sortie reçoit l'état 1, le monde gelé comme un script le ferait : c'est la
//      cinématique du jeu — caméra, portail qui grandit et s'écrase, flash, musique ; à son état 0x63
//      elle a tout rendu et le portail reste debout, À SA PLACE D'ORIGINE (100, −2700) ;
//   2. la bourse du programme (AreneDirecteur.Bourse : ses pièces × le palier) est versée au groupe,
//      répartie à parts égales entre TOUS ses membres, debout ou non — le reste de la division, une
//      pièce chacun aux premiers —, chaque bourse bornée à 99 999 comme au ramassage d'origine
//      (PartyMemberStep). Le premier membre debout qui fait un pas reçoit le message. Plus de
//      coffres (Vincent, 2026-09-29 : « supprime tout compte fait pour les boss les coffres ») ;
//   3. un membre debout qui entre dans le portail se voit demander « Bon, on s'fait la malle ? » ;
//      Oui ramène le groupe au hub (HubAuberge.RetourDArene).
//
// LA DÉFAITE (couture DefaiteEvitee, au passage à l'état 9) : chaque membre ressuscité avec tous ses
// PV — les écritures de ReviveTarget_0053CD80 (Ghidra: FUN_0053cd80), sans l'appariement de combat —,
// retour au hub, et à l'arrivée « Ouille, nom d'un panda à la sauce tomate, j'ai mal à la tête ».
//
// Journal [ARENE] ; en Debug, chaque écriture dans les huit banques d'état pendant le mode arène
// (D6 : zéro attendu).
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class Arene
{
    private const byte Zone = 4, SousZone = 9;

    /// <summary>Le point d'entrée pris : la salle du boss (section 0).</summary>
    private const byte Entree = 0;

    /// <summary>
    /// L'arrivée du groupe : la place d'origine du boss, (100, −2200), tournée vers les z décroissants
    /// (cap 2048) — le point d'entrée 0 et le boss sont intervertis (Vincent, 2026-09-28). Le boss
    /// prend (100, −2900), cap 0 (<see cref="AreneDirecteur.Programmes"/>).
    /// </summary>
    private const short ArriveeX = 100, ArriveeZ = -2200, ArriveeCap = 2048;

    private static int s_Ecarts;                     // écarts de placement encore à annuler (x puis z)
    private static int s_CapMembres;                 // les membres (bits 0..3) qui n'ont pas encore reçu le cap

    /// <summary>Les sous-types de décor rendus au néant dans l'arène (decor.csv, 4/9).</summary>
    private static readonly byte[] s_Retires =
    [
        28,             // le coffre (0x00473ED0)
        56,             // l'effet d'ambiance (0x004746C0)
        11,             // 0x0045EC10, une entité de scène qui se déplace
        47, 48, 49,     // les gonds de la tour (0x0045FFA0, 0x00460080, 0x00460140) — verrous persistants
    ];

    private const byte SousTypeScenette = 22;
    private const int EtatScenetteFinie = 0x63;

    private const int BourseMax = 99999;             // la borne du ramassage d'origine (PartyMemberStep)
    private const int RayonPortail = 90, RayonPortailSortie = RayonPortail + 30;
    private const string QuestionPortail = "Bon, on s'fait la malle ?";

    /// <summary>Par membre (0 à 3) : déjà questionné au portail, et pas encore ressorti du rayon.</summary>
    private static readonly bool[] s_Questionne = new bool[4];

    private const int TailleEntite = 0x9c;

    private const string Panda = "Ouille, nom d'un panda à la sauce tomate, j'ai mal à la tête";

    private static bool s_Arme;
    private static bool s_Arrivee;                   // le prochain placement est l'arrivée dans l'arène
    private static bool s_Panda;                     // le message à montrer à l'arrivée dans le hub

    /// <summary>Vrai tant que le groupe est dans l'arène.</summary>
    internal static bool Actif { get; private set; }

    private static uint* s_Point;                    // le point d'entrée recopié, sans script d'arrivée
    private static byte* s_Scenette;                 // la scénette de sortie née dans l'arène
    private static bool s_Fin;                       // la dernière vague est tombée
    private static bool s_Portail;                   // la cinématique est finie : la bourse versée, le portail ouvert
    private static bool s_Paye;                      // la bourse de ce combat est versée — une fois par arène
    private static string? s_Annonce;                // le message de la bourse, pas encore montré
#if DEBUG
    private static readonly HashSet<(uint, string)> s_ScriptsTus = new();
#endif

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        s_Point = (uint*)NativeMemory.AllocZeroed(8, sizeof(uint));

        Coutures.Repertoire repertoire = Coutures.EntreeSousZone;
        Coutures.EntreeSousZone = (table, sousZone) =>
        {
            Chargement(sousZone);
            return repertoire(table, sousZone);
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

        Coutures.Naissance naissance = Coutures.DecorNe;
        Coutures.DecorNe = (enregistrement, entite) =>
        {
            naissance(enregistrement, entite);
            if (Actif) Vider(enregistrement, entite);
        };

        Func<bool> volumes = Coutures.VolumesTus;
        Coutures.VolumesTus = () => volumes() || Actif;

        Coutures.Script script = Coutures.ScriptTu;
        Coutures.ScriptTu = (indice, appelant) =>
        {
            if (script(indice, appelant)) return true;
            if (!Actif || indice >= 0xffff) return false;
#if DEBUG
            // Une ligne par script et par appelant : la liste de scripts du meneur (fiche de section,
            // RunLeaderScriptListEarly / Late) en relance un à chaque image.
            if (s_ScriptsTus.Add((indice, appelant))) Journal($"script {indice} tu ({appelant}) — noté une fois");
#endif
            return true;
        };

        Coutures.Tentative rencontre = Coutures.Rencontre;
        Coutures.Rencontre = (r, pool) =>
        {
            if (!Actif) rencontre(r, pool);
        };

        Coutures.Lieu lieu = Coutures.LieuSauve;
        Coutures.LieuSauve = entete =>
        {
            lieu(entete);
            Sauvegarde(entete);
        };

        Action image = Coutures.ImageSuivante;
        Coutures.ImageSuivante = () =>
        {
            image();
            if (!Actif) return;
            SuivreLaFin();
#if DEBUG
            SurveillerDrapeaux();
#endif
        };

        Coutures.Pas pas = Coutures.PasDuMembre;
        Coutures.PasDuMembre = (membre, pad, perChar, descripteur) =>
        {
            pas(membre, pad, perChar, descripteur);
            if (Actif && s_CapMembres != 0) Orienter(membre);
            if (Actif) Portail(membre, perChar, descripteur);
            else if (s_Panda) Message(membre, perChar, descripteur);
        };

        Func<bool> defaite = Coutures.DefaiteEvitee;
        Coutures.DefaiteEvitee = () => defaite() || Defaite();

        // La boîte du jeu est suivie à chaque image dessinée : le monde est gelé tant qu'elle est
        // ouverte, et le pas des membres ne tourne pas.
        Coutures.Dessin dessin = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = dessin(otBase, curseur);
            SuivreLaBoite();
            return curseur;
        };
    }

    // ---- l'aller et le retour --------------------------------------------------------------------

    /// <summary>
    /// Le départ confirmé au menu : l'interrupteur est armé, et le rechargement vers 4/9 est demandé.
    /// </summary>
    internal static void Partir()
    {
        s_Arme = true;
        s_Arrivee = true;
        Transferer(Zone, SousZone, Entree);
        Journal($"départ : zone {Zone}, sous-zone {SousZone}, entrée {Entree}, meneur J{Jeu.s_Unknown_005de53c + 1}");
    }

    /// <summary>Le retour au hub, par le portail ou après la défaite ; le chargement baisse le mode.</summary>
    private static void RevenirAuHub(string pourquoi)
    {
        s_Arme = false;
        HubAuberge.RetourDArene();
        Transferer(0, HubAuberge.Hub, 0);
        Journal($"retour au hub ({pourquoi})");
    }

    /// <summary>
    /// Un changement de zone comme la ligne QUEST du menu de mise au point le demande
    /// (<c>WorldMapDebugMenu.cs</c>, puis <c>CommitDebugWarp</c>) : la zone entière est rechargée.
    /// </summary>
    private static void Transferer(byte zone, byte sousZone, byte entree)
    {
        Jeu.Res.s_Unknown_005de528 = (int)((uint)Jeu.Res.s_Unknown_005de528 & 0xffffff00 | zone);
        Jeu.s_Unknown_00566c84 = 0xff;
        Jeu.s_Unknown_005de52c = sousZone;

        // CommitDebugWarp.
        Jeu.s_Unknown_00566c88 = 0xff;
        Jeu.s_Unknown_005de53c = (byte)Jeu.Mem.s_Unknown_005de538;
        Jeu.Mem.s_WorldFlags_005de02c |= 2;
        Jeu.s_Unknown_005de530 = entree;
        Jeu.s_WorldMapStep_005645f8 = 2;
    }

    /// <summary>Chaque chargement de sous-zone lève ou baisse l'interrupteur.</summary>
    private static void Chargement(int sousZone)
    {
        int zone = Jeu.Res.s_Unknown_005de528 & 0xff;
        bool arene = zone == Zone && sousZone == SousZone && s_Arme;
        s_Arme = false;

        if (arene)
        {
            Actif = true;
            s_Scenette = null;
            s_Fin = s_Portail = s_Paye = false;
            Array.Clear(s_Questionne);
            s_Annonce = null;
#if DEBUG
            s_ScriptsTus.Clear();
            PhotographierDrapeaux();
#endif
            Journal("mode arène levé : 4/9 se charge vidée");
            AreneDirecteur.Commencer();
            return;
        }

        if (Actif)
        {
            Journal($"mode arène baissé : chargement de {zone}/{sousZone}");
            AreneDirecteur.Arreter();
        }
        Actif = false;
        s_Annonce = null;
        s_Arrivee = false;
        s_Ecarts = 0;
        s_CapMembres = 0;
    }

    /// <summary>
    /// À l'arrivée, le point d'entrée 0 est recopié — section 0 gardée —, avec la place d'origine du
    /// boss et sans face imposée : les faces du point d'origine sont celles de SA place, et le jeu
    /// cherche alors la face sous le groupe ; les deux écarts qu'il tire sont annulés, et le cap est
    /// donné à chaque membre à son premier pas (la technique du hub). La moitié haute du premier dword
    /// (le script d'arrivée, lancé au pas 2 par <c>0x005de048</c>) passe à <c>0xffff</c>, « aucun
    /// script ».
    /// </summary>
    private static uint* Arrivee(uint* point)
    {
        bool arrivee = s_Arrivee;
        s_Arrivee = false;
        if (!arrivee || !Actif || point == null) return point;

        for (int i = 0; i < 8; i++) s_Point[i] = point[i];
        s_Point[0] |= 0xffff0000;
        *(short*)((byte*)s_Point + 4) = ArriveeX;
        *(short*)((byte*)s_Point + 8) = ArriveeZ;
        s_Point[4] = 0xffffffff;
        s_Point[5] = 0xffffffff;
        s_Ecarts = 2;
        s_CapMembres = 0xf;

        Journal($"arrivée en ({ArriveeX}, {ArriveeZ}), cap {ArriveeCap} — au lieu de ({*(short*)((byte*)point + 4)}, " +
                $"{*(short*)((byte*)point + 8)}) —, section {point[0] & 0xff}, script d'arrivée {point[0] >> 16:x4} → aucun");
        return s_Point;
    }

    /// <summary>Le cap de l'arrivée, donné à chaque membre à son premier pas après le placement.</summary>
    private static void Orienter(uint* membre)
    {
        byte* m = (byte*)membre;
        int bit = 1 << (m[8] & 3);
        if ((s_CapMembres & bit) == 0) return;
        s_CapMembres &= ~bit;
        *(short*)(m + 0x32) = ArriveeCap;
    }

    // ---- la salle ----------------------------------------------------------------------------------

    /// <summary>À la naissance : le décor qui agit retourne au néant (D2) ; la scénette est notée.</summary>
    private static void Vider(uint* enregistrement, uint* entite)
    {
        byte sousType = ((byte*)enregistrement)[0x1d];
        if (sousType == SousTypeScenette && s_Scenette == null) { s_Scenette = (byte*)entite; return; }
        if (Array.IndexOf(s_Retires, sousType) < 0) return;
        *entite = 0;   // un emplacement libre, comme ClearEntitySlots le laisse
    }

    // ---- la fin ------------------------------------------------------------------------------------

    /// <summary>Le directeur a vu tomber la dernière vague : la cinématique de sortie.</summary>
    internal static void Victoire()
    {
        if (!Actif || s_Fin) return;
        s_Fin = true;

        if (s_Scenette == null || *(ushort*)(s_Scenette + 0x10) != 0)
        {
            Journal("victoire : pas de scénette de sortie au repos — bourse et portail tout de suite");
            OuvrirLePortail();
            return;
        }

        // Le monde gelé, la scénette seule dégelée — le geste du coffre (EntityHandler_00473ED0 :
        // FreezeAllEntities, puis `& 0xfbffffff` sur lui-même). Sans ce second temps, le gel posait
        // 0x4000000 sur la scénette aussi : elle restait à l'état 1, et le monde avec elle (essai de
        // Vincent, 2026-09-28). Son état 4 dégèle tout à la fin.
        Jeu.FreezeAllEntities();
        *(uint*)s_Scenette &= 0xfbffffff;
        *(ushort*)(s_Scenette + 0x10) = 1;
        Journal($"victoire : la scénette de sortie reçoit l'état 1 (mot 0x{*(uint*)s_Scenette:x8})");
    }

    /// <summary>À chaque image : la cinématique finie, la bourse et le portail.</summary>
    private static void SuivreLaFin()
    {
        // s_Paye, pas seulement s_Portail : le Oui du portail remet s_Portail à faux, et l'image
        // suivante — avant le chargement du hub — rouvrait le portail et versait la bourse une
        // seconde fois (Vincent, 2026-09-30).
        if (!s_Fin || s_Portail || s_Paye || s_Scenette == null) return;
        if (*(ushort*)(s_Scenette + 0x10) != EtatScenetteFinie) return;
        OuvrirLePortail();
    }

    private static void OuvrirLePortail()
    {
        s_Portail = true;
        Recompenser();
        Journal("fin : portail ouvert");
    }

    /// <summary>
    /// La bourse du programme, répartie entre tous les membres du groupe : la part entière à chacun,
    /// puis une pièce de plus aux premiers tant qu'il en reste — la somme versée est exactement la
    /// bourse, hors borne de 99 999. La fiche du membre i est celle que <c>PartyMemberStep</c>
    /// crédite : mémoire principale + <c>0xf0000 + i × 0x2000</c>, l'or en <c>+0x26c</c>.
    /// </summary>
    private static void Recompenser()
    {
        if (s_Paye) return;
        s_Paye = true;

        int bourse = AreneDirecteur.Bourse;
        int membres = Jeu.s_PartySize_005e9020;
        if (bourse <= 0 || membres <= 0) return;

        byte* memoire = Jeu.GetMainMemPtr();
        int part = bourse / membres, reste = bourse % membres;
        var parts = new List<string>();
        for (int i = 0; i < membres; i++)
        {
            int gain = part + (i < reste ? 1 : 0);
            int* or = (int*)(memoire + 0xf0000 + i * 0x2000 + 0x26c);
            *or = Math.Min(BourseMax, *or + gain);
            parts.Add($"J{i + 1} +{gain} ({*or})");
        }

        s_Annonce = $"Le groupe remporte {bourse} pièces d'or";
        Journal($"bourse : {bourse} pièces, {string.Join(", ", parts)}");
    }

    /// <summary>
    /// Un membre debout, sans boîte ouverte, qui entre dans le rayon du portail : la question « Bon,
    /// on s'fait la malle ? » (Vincent, 2026-09-28 : sans elle, on partait trop vite). Oui ramène au
    /// hub ; Non ou menu la ferment, et elle ne se repose qu'une fois le membre ressorti du rayon, de
    /// quelques unités de plus — la forme du téléporteur du hub. Avant elle, au premier pas d'un membre
    /// debout, le message de la bourse.
    /// </summary>
    private static void Portail(uint* membre, byte* perChar, byte* descripteur)
    {
        if (!s_Portail || s_Scenette == null) return;

        if (s_Annonce != null)
        {
            if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return;
            if ((*membre & 0x10000) != 0) return;   // un compagnon piloté par le jeu
            if ((descripteur[0x8c7] & 1) != 0 || EcranMenu.Ouvert || s_Boite) return;
            string annonce = s_Annonce;
            s_Annonce = null;
            OuvrirLaBoite(membre, perChar, descripteur, annonce, null);
            return;
        }

        byte* m = (byte*)membre;
        int rang = m[8] & 3;
        int dx = (*(int*)(s_Scenette + 0x1c) >> 12) - (*(int*)(m + 0x1c) >> 12);
        int dz = (*(int*)(s_Scenette + 0x24) >> 12) - (*(int*)(m + 0x24) >> 12);
        int d2 = dx * dx + dz * dz;

        if (d2 > RayonPortailSortie * RayonPortailSortie) { s_Questionne[rang] = false; return; }
        if (s_Questionne[rang] || d2 > RayonPortail * RayonPortail) return;
        if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return;
        if ((*membre & 0x10000) != 0) return;   // un compagnon piloté par le jeu (AreneTeleporteur.Approcher)
        if ((descripteur[0x8c7] & 1) != 0 || EcranMenu.Ouvert || s_Boite) return;

        s_Questionne[rang] = true;
        OuvrirLaBoite(membre, perChar, descripteur, QuestionPortail, () =>
        {
            s_Portail = false;
            RevenirAuHub($"J{rang + 1} passe le portail");
        });
    }

    // ---- la défaite --------------------------------------------------------------------------------

    /// <summary>
    /// Tout le groupe est tombé en arène : chacun ressuscité avec tous ses PV, puis retour au hub
    /// (D7). Les écritures sont celles de <c>ReviveTarget_0053CD80</c> sur le bloc de combat du
    /// membre (<c>ResolvePartyCombatBlock_005387C0</c> : mémoire principale +
    /// <c>(membre + 0x78) × 0x2000 + 0x100</c>).
    /// </summary>
    private static bool Defaite()
    {
        if (!Actif) return false;

        byte* memoire = Jeu.GetMainMemPtr();
        for (int i = 0; i < Jeu.s_PartySize_005e9020; i++)
        {
            uint* membre = (uint*)(Jeu.Inp.s_SlotFlags_005e93c0 + i * TailleEntite);
            byte* bloc = memoire + (i + 0x78) * 0x2000 + 0x100;
            *(short*)(bloc + 0x4c) = *(short*)(bloc + 0x48);
            membre[6] = 0;
            *(uint*)(bloc + 0x40) &= 0xbfffffff;
            *membre = *membre & 0xbfdfdf79 | 0x1000000;
        }

        s_Panda = true;
        RevenirAuHub("défaite");
        return true;
    }

    /// <summary>À l'arrivée au hub après une défaite, le message, pour le premier membre debout.</summary>
    private static void Message(uint* membre, byte* perChar, byte* descripteur)
    {
        if ((Jeu.Res.s_Unknown_005de528 & 0xff) != 0 || Jeu.s_Unknown_00566c88 != HubAuberge.Hub) return;
        if (EcranMenu.Ouvert || s_Boite || (descripteur[0x8c7] & 1) != 0) return;
        if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return;
        if ((*membre & 0x10000) != 0) return;   // un compagnon piloté par le jeu (AreneTeleporteur.Approcher)

        s_Panda = false;
        OuvrirLaBoite(membre, perChar, descripteur, Panda, null);
    }

    // ---- la boîte du jeu -------------------------------------------------------------------------

    // Une boîte de dialogue du jeu, sur le modèle de la question d'un coffre (AnnounceItem,
    // Handlers/Chests.cs). Les libellés de la table +0x70 de l'état 3, relus hors jeu par
    // `OpenForesia.Tools libelles aa ac` :
    //
    //   0xaa  0b 01 a0 00             l'appel du libellé 0xa0 : gel (08 00), page vidée, cadre, icône
    //   0xac  … « Désires-tu l'emporter ? » 0f  04 10 10  03 18 03  0c  Oui 0d Non 0d  09 3f  00
    //
    // Un programme de l'arène = l'ouverture de 0xa0 sans son icône (s_Ouverture), le texte en Latin-1, puis :
    //   - une question : la suite de 0xac après sa phrase — 0f (attente de validation, curseur),
    //     position et largeur de ligne, page vidée, les deux lignes « Oui » / « Non », et 09 3f : le
    //     curseur de choix (UiWindow.cs), qui range la ligne choisie dans s_PanelSelection_005e9060
    //     [0x3f] à la validation — 0 pour Oui, 1 pour Non. C'est la case que le coffre emploie ; sa
    //     valeur d'avant est rendue une fois la réponse lue ;
    //   - un message : 0f seul ;
    // et enfin 08 01 (le dégel) et un zéro. Première version, fausse : la fin recopiée d'AnnounceItem
    // (`0b 01 ac 00 08 01`) appelait toute la question du coffre, « Désires-tu l'emporter ? » compris
    // (essai de Vincent, 2026-09-28).
    //
    // La mise en scène est celle de GrantChestContents_00530F20 : le monde gelé, l'animation de chaque
    // membre mise de côté ; le rappel 0x00530ed0 (RestorePartyReactions_00530ED0) la rend en fin de
    // boîte et réarme la manette de chaque membre — c'est ce qui rend la main au joueur.

    private const uint RappelFinDeBoite = 0x00530ed0;
    private const int CaseReponse = 0x3f;

    /// <summary>
    /// L'ouverture : le libellé 0xa0 recopié — 08 00 (gel), 0c (page vidée), 04 (position), 14 07
    /// (teinte), 03 (largeur de ligne), 1f d0 30 (cadre) — SANS son code 01 3c 3d 3e : l'icône d'objet,
    /// lue dans les cases 0x3c..0x3e que chaque coffre remplit (GrantChestContents_00530F20) ; elle
    /// montrait le dernier objet pris dans un coffre (Vincent, 2026-09-28). Sans icône, le texte part à
    /// gauche, avec la position et la largeur de la page « Oui / Non » de 0xac (04 10 10, 03 18 03)
    /// au lieu de 04 30 10 et 03 14 03.
    /// </summary>
    private static readonly byte[] s_Ouverture =
        [0x08, 0x00, 0x0c, 0x04, 0x10, 0x10, 0x14, 0x07, 0x03, 0x18, 0x03, 0x1f, 0xd0, 0x30];

    private static readonly byte[] s_SuiteQuestion =
    [
        0x0f, 0x04, 0x10, 0x10, 0x03, 0x18, 0x03, 0x0c,
        (byte)'O', (byte)'u', (byte)'i', 0x0d, (byte)'N', (byte)'o', (byte)'n', 0x0d,
        0x09, CaseReponse,
    ];
    private static readonly byte[] s_SuiteMessage = [0x0f];
    private static readonly byte[] s_FinDeBoite = [0x08, 0x01, 0x00];

    private static byte* s_Programme;                // le programme de texte, gardé tant que le jeu tourne
    private static bool s_Boite;                     // une boîte de l'arène est ouverte
    private static Action? s_SiOui;
    private static byte* s_Descripteur;
    private static byte s_ReponseAvant;              // la case 0x3f avant la question

    private static void OuvrirLaBoite(uint* membre, byte* perChar, byte* descripteur, string texte, Action? siOui)
    {
        _ = perChar;
        byte[] octets = System.Text.Encoding.Latin1.GetBytes(texte);
        byte[] suite = siOui != null ? s_SuiteQuestion : s_SuiteMessage;
        const int Taille = 256;
        if (s_Programme == null) s_Programme = (byte*)NativeMemory.AllocZeroed(Taille);
        int n = 0;
        foreach (byte b in s_Ouverture) s_Programme[n++] = b;
        int place = Taille - s_Ouverture.Length - suite.Length - s_FinDeBoite.Length;
        foreach (byte b in octets.AsSpan(0, Math.Min(octets.Length, place))) s_Programme[n++] = b;
        foreach (byte b in suite) s_Programme[n++] = b;
        foreach (byte b in s_FinDeBoite) s_Programme[n++] = b;

        Jeu.FreezeAllEntities();
        byte* slot = Jeu.Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < Jeu.s_PartySize_005e9020; i++, slot += TailleEntite)
        {
            slot[0x40] = slot[0x1a];
            slot[0x42] = slot[0x18];
        }

        s_Boite = true;
        s_SiOui = siOui;
        s_Descripteur = descripteur;
        if (siOui != null)
        {
            s_ReponseAvant = Jeu.Mem.s_PanelSelection_005e9060[CaseReponse];
            Jeu.Mem.s_PanelSelection_005e9060[CaseReponse] = 1;   // Non, tant que rien n'est validé
        }
        Jeu.BindMenuLabels(s_Programme, (byte*)membre, descripteur, RappelFinDeBoite);
        Journal($"boîte : « {texte} »{(siOui != null ? " (Oui / Non)" : "")}");
    }

    /// <summary>À chaque image dessinée : la boîte refermée, la réponse lue.</summary>
    private static void SuivreLaBoite()
    {
        if (!s_Boite || (s_Descripteur[0x8c7] & 1) != 0) return;

        s_Boite = false;
        Action? siOui = s_SiOui;
        s_SiOui = null;
        if (siOui == null) return;

        byte reponse = Jeu.Mem.s_PanelSelection_005e9060[CaseReponse];
        Jeu.Mem.s_PanelSelection_005e9060[CaseReponse] = s_ReponseAvant;
        Journal($"boîte : {(reponse == 0 ? "oui" : "non")} (ligne {reponse})");
        if (reponse == 0) siOui();
    }

    // ---- la sauvegarde ---------------------------------------------------------------------------

    /// <summary>
    /// Une sauvegarde en arène écrit le hub comme lieu, que <see cref="HubAuberge.Sauvegarde"/>
    /// change à son tour en réception d'origine : on reprend là où l'on serait sorti du hub (D6).
    /// </summary>
    private static void Sauvegarde(byte* entete)
    {
        if (!Actif || entete[1] != Zone || entete[2] != SousZone) return;

        entete[1] = 0;
        entete[2] = HubAuberge.Hub;
        HubAuberge.Sauvegarde(entete);
        Journal($"sauvegarde en arène : lieu de reprise écrit = {entete[1]}/{entete[2]}, reprise {entete[3]}");
    }

#if DEBUG
    // ---- le journal des drapeaux (D6) ------------------------------------------------------------

    /// <summary>Les huit banques d'état que la sauvegarde recopie (<c>CopyGlobalBanksIntoSaveBlock</c>).</summary>
    private static readonly int[] s_Banques =
        [0x005e96e0, 0x005e9780, 0x005e9060, 0x005e9840, 0x005e9720, 0x005e92c0, 0x005e9980, 0x005e9360];

    private const int TailleBanque = 0x40;
    private static readonly byte[] s_Photo = new byte[8 * TailleBanque];

    private static void PhotographierDrapeaux()
    {
        for (int b = 0; b < s_Banques.Length; b++)
        {
            byte* banque = Jeu.Mem.Arena(s_Banques[b]);
            for (int i = 0; i < TailleBanque; i++) s_Photo[b * TailleBanque + i] = banque[i];
        }
    }

    /// <summary>Toute différence avec la photo prise à l'entrée est écrite, puis retenue.</summary>
    private static void SurveillerDrapeaux()
    {
        for (int b = 0; b < s_Banques.Length; b++)
        {
            byte* banque = Jeu.Mem.Arena(s_Banques[b]);
            for (int i = 0; i < TailleBanque; i++)
            {
                byte avant = s_Photo[b * TailleBanque + i];
                if (banque[i] == avant) continue;
                Journal($"écriture de drapeau en arène : banque 0x{s_Banques[b]:x8} + 0x{i:x2}, {avant:x2} → {banque[i]:x2}");
                s_Photo[b * TailleBanque + i] = banque[i];
            }
        }
    }
#endif

    private static void Journal(string message) => Console.Error.WriteLine($"[ARENE] {message}");
}
