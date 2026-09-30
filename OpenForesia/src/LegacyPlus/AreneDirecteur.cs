// =====================================================================
// LP-012 — le directeur de l'arène : les programmes, leurs vagues, la naissance et le palier.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-arena, design D3 et D4). Un
// programme est une liste de vagues ; une vague, des créatures désignées par leur place dans les
// formations de la sous-zone (formation n, rang r de la table +0x34 du répertoire). Le directeur,
// appelé à chaque image en mode arène (Arene.Actif) :
//
//   1. laisse passer un court délai après l'arrivée, puis fait naître la vague courante par
//      SpawnEntityFormation (Ghidra: FUN_0051b850), le chemin de l'opcode 13 des scripts — la
//      créature a donc son IA, sa fiche, ses collisions, exactement comme d'origine ;
//   2. suit les emplacements de la table de scène (0x005ecaa0) nés de cet appel, et passe à la vague
//      suivante quand tous sont morts (le bit 0x80000000 du mot d'état est tombé) ;
//   3. après la dernière, passe la main à Arene.Victoire : la cinématique, la bourse, le portail.
//
// LE PALIER (D4) — Normal, Difficile, Très difficile, Impossible (Paliers ; les paliers 1 à 10 jusqu'au
// 2026-09-30) — sur deux coutures existantes :
//   - PvALaNaissance (LP-002) : pendant la naissance d'une vague, et pendant elle seule, les PV
//     maximum et courants × 1 à 4, bornés à PvMax = 30 000. Pas 65 535 : le jeu range les PV en
//     ushort mais les lit en short SIGNÉ (WorldSimulationStep, chaque image : « PV < 1 » pose le bit
//     de mort 0x40000000 sans la séquence de mort). Le Dragon rouge (8 000 PV) au palier 5 naissait à
//     40 000 = −25 536 : figé, intouchable, modèle en place (Vincent, 2026-09-30). Et pas 32 767 non
//     plus : un soin ou une régénération ajoute AVANT de ramener au maximum, un boss à 32 000 PV
//     débordait avec plus de 767 PV rendus. La marge de 2 767 couvre ces ajouts ;
//   - DegatsSubis (LP-001) : un coup porté À UN MEMBRE DU GROUPE par une créature suivie × 1 à 2,5,
//     borné à 32 767. L'attaquant d'un sort est souvent l'entité du projectile, pas la créature : pour
//     un attaquant né dans l'une des deux tables d'effets (0x005ff400, 0x005edee0), on suit ses deux
//     pointeurs vers une entité source, +0x74 (le lanceur) et +0x6c (SpawnEffectEntity y range sa
//     source). En Debug, chaque coup reçu en arène est écrit [ARENE], pour vérifier à l'essai qui
//     est l'attaquant.
//
// Limite connue : ce que la créature fait naître elle-même dans la table de scène (une invocation)
// n'est pas suivi — ni multiplié, ni attendu pour finir la vague.
//
// Le directeur ne tire aucun nombre ; SpawnEntityFormation, elle, tire la cible initiale de chaque
// créature (CrtRand, comme pour toute formation du jeu) : c'est le tirage du jeu, pas celui du tweak.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class AreneDirecteur
{
    /// <summary>
    /// Une créature : le rang <paramref name="Rang"/> de la formation <paramref name="Formation"/> de
    /// la sous-zone. Une <paramref name="Place"/> la déplace (x, z, cap) : l'enregistrement perd alors
    /// ses deux faces de sol (<c>-1</c>), et <c>SpawnEntityFormation</c> pose la créature au sol par
    /// <c>TryPlaceEntity</c> — comme le banc d'essai.
    /// <para>Une créature <paramref name="Greffe"/> (LP-013) vient d'une autre sous-zone : la formation
    /// et le rang sont ceux de SA sous-zone, l'espèce devient celle que la greffe lui a prêtée dans
    /// 4/9, et elle naît à la hauteur du sol de l'arène.</para>
    /// </summary>
    internal sealed record Creature(int Formation, int Rang, (short X, short Z, short Cap)? Place = null,
                                    AreneGreffe.Espece? Greffe = null);

    /// <summary>Une vague : les créatures qui naissent ensemble.</summary>
    internal sealed record Vague(Creature[] Creatures);

    /// <summary>
    /// Un programme de l'arène : le nom que le menu affiche, la bourse de la victoire (pièces d'or au
    /// palier 1, réparties entre les membres du groupe — <c>Arene.Recompenser</c>), et ses vagues dans
    /// l'ordre.
    /// </summary>
    /// <remarks>
    /// <paramref name="Pause"/> : les millisecondes entre la disparition de la dernière créature d'une vague et
    /// la naissance de la suivante. <paramref name="Challenge"/> : un boss rush (LP-014), que le menu
    /// range à part, sur une page ouverte par le premier.
    /// </remarks>
    internal sealed record Programme(string Nom, int Pieces, Vague[] Vagues, int Pause = PauseParDefaut, bool Challenge = false);

    /// <summary>
    /// Les programmes, dans l'ordre du menu. L'Enchanteresse, boss natif de 4/9, valide la mécanique :
    /// une vague, elle seule — le rang 0 de la formation 0 (formations.csv : espèce 1, (100, 200,
    /// −2200), cap 2048), sans ses deux Ténébreux. Elle prend la place d'arrivée du groupe, (100,
    /// −2900), tournée vers lui (cap 0), et le groupe la sienne (Vincent, 2026-09-28 ;
    /// <c>Arene.cs</c>). Elle reste au menu quand les boss greffés arrivent.
    /// <para>Le Méchant ours (LP-013) : le combat de boss de la forêt entier, la formation 0 de 3/5 —
    /// l'ours au rang 0, ses deux Grizzlys aux rangs 1 et 2, à 200 unités de part et d'autre comme
    /// dans la forêt (formations.csv), à la place du boss de la tour.</para>
    /// <para>Le Behemoth (LP-013) : seul, la formation 0 de 3/9 « La grotte souterraine » (2 160 PV,
    /// rang 150), à la place du boss de la tour.</para>
    /// <para>La bourse suit la force du boss (Vincent, 2026-09-29 : moins pour les boss faibles, plus
    /// pour les forts ; le Méchant ours à 40 pièces). Celles de l'Enchanteresse (910 PV, rang 140) et
    /// du Behemoth, à cette échelle, confirmées par Vincent le 2026-09-29.</para>
    /// <para>Sylphide, Fougre et le gros poilu (LP-013, 2026-09-29) : la Sylphide et le Wybarn de la
    /// formation 1 de 4/7 (au même point dans la tour, ici à 200 unités l'un de l'autre) ; le Fougre
    /// et ses deux Grifons de la formation 0 de 8/1 (à 100 unités devant et derrière lui dans le nid,
    /// ici de part et d'autre comme les Grizzlys) ; le gros poilu seul, formation 0 de 10/12 ; le Jeune
    /// dragon seul, formation 0 de 1/4 ; le Loup-garou et le Tigre, formation 0 de 6/5, à 340 unités
    /// l'un de l'autre comme au château ; le Nocturio et ses deux Sacubas, formation 1 de 6/11, les
    /// Sacubas de part et d'autre comme les Grizzlys ; le Durahan et ses trois Sanguinaires,
    /// formation 0 de 5/4, deux de part et d'autre, le troisième derrière lui ; le Troll et ses deux
    /// Géants verts, formation 0 de 1/9, les Géants de part et d'autre ; l'Ange noir seul, formation 0
    /// de 10/14 ; Eifridt seul, formation 0 de 10/11 ; le Lordémon seul, formation 0 de 10/13 ;
    /// le Cerbère et ses quatre Groklebs, formation 0 de 10/9, deux devant lui et deux derrière comme
    /// dans la grotte ; le Dragon rouge et ses deux Salamandres feu, formation 0 de 2/0, les Salamandres
    /// de part et d'autre, à 350 unités pour laisser la place au dragon ; Belzébuth seul, formation 0 de
    /// 10/15 ; le Dragon zombie et ses deux Maudites, formation 0 de 10/4, les Maudites de part et
    /// d'autre ; le Roi-zombie et sa cour, formation 0 de 5/9 : les Durahans noirs à ses côtés, les
    /// Revenants devant, les Ombres rouges — qui volent à 600 dans le labyrinthe — plus loin sur les
    /// flancs, posées au sol de l'arène (2026-09-29).</para>
    /// <para>L'ordre du menu : les PV du combat entier au palier 1, boss et escorte, croissants
    /// (Vincent, 2026-09-29) — 460, 700, 910, 968, 1 220, 1 263, 1 272, 1 531, 1 650, 2 160, 2 300,
    /// 2 391, 2 400, 2 726, 2 763, 2 812, 3 000, 5 438, 8 480.</para>
    /// </summary>
    internal static readonly Programme[] Programmes =
    [
        new("MECHANT OURS", 40, [new Vague([                                    // 300 + 2 × 80
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.MechantOurs),
            new Creature(0, 1, (-100, -2900, 0), AreneGreffe.Grizzly),
            new Creature(0, 2, (300, -2900, 0), AreneGreffe.Grizzly),
        ])]),
        new("JEUNE DRAGON", 100, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.JeuneDragon)])]),   // 700
        new("ENCHANTERESSE", 150, [new Vague([new Creature(0, 0, (100, -2900, 0))])]),   // 910
        new("LOUP-GAROU ET TIGRE", 140, [new Vague([                                     // 468 + 500
            new Creature(0, 0, (-70, -2900, 0), AreneGreffe.LoupGarou),
            new Creature(0, 1, (270, -2900, 0), AreneGreffe.Tigre),
        ])]),
        new("NOCTURIO", 170, [new Vague([                                       // 840 + 2 × 190
            new Creature(1, 0, (100, -2900, 0), AreneGreffe.Nocturio),
            new Creature(1, 1, (-100, -2900, 0), AreneGreffe.Sacuba),
            new Creature(1, 2, (300, -2900, 0), AreneGreffe.Sacuba),
        ])]),
        new("SYLPHIDE", 180, [new Vague([                                       // 563 + 700
            new Creature(1, 0, (0, -2900, 0), AreneGreffe.Sylphide),
            new Creature(1, 1, (200, -2900, 0), AreneGreffe.Wybarn),
        ])]),
        new("FOUGRE", 180, [new Vague([                                         // 1 030 + 2 × 121
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.Fougre),
            new Creature(0, 1, (-100, -2900, 0), AreneGreffe.Grifon),
            new Creature(0, 2, (300, -2900, 0), AreneGreffe.Grifon),
        ])]),
        new("DURAHAN", 220, [new Vague([                                        // 808 + 3 × 241
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.Durahan),
            new Creature(0, 1, (-150, -2900, 0), AreneGreffe.Sanguinaire),
            new Creature(0, 2, (350, -2900, 0), AreneGreffe.Sanguinaire),
            new Creature(0, 3, (100, -3150, 0), AreneGreffe.Sanguinaire),
        ])]),
        new("TROLL", 230, [new Vague([                                          // 850 + 2 × 400
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.Troll),
            new Creature(0, 1, (-100, -2900, 0), AreneGreffe.GeantVert),
            new Creature(0, 2, (300, -2900, 0), AreneGreffe.GeantVert),
        ])]),
        new("BEHEMOTH", 300, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Behemoth)])]),   // 2 160
        new("GROS POILU", 330, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.GrosPoilu)])]),  // 2 300
        new("ANGE NOIR", 340, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.AngeNoir)])]),    // 2 391
        new("EIFRIDT", 340, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Eifridt)])]),       // 2 400
        new("ROI-ZOMBIE", 330, [new Vague([                                     // 1 350 + 2 × (358 + 190 + 140)
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.RoiZombie),
            new Creature(0, 1, (-100, -2900, 0), AreneGreffe.DurahanNoir),
            new Creature(0, 2, (300, -2900, 0), AreneGreffe.DurahanNoir),
            new Creature(0, 3, (-100, -2600, 0), AreneGreffe.Revenant),
            new Creature(0, 4, (300, -2600, 0), AreneGreffe.Revenant),
            new Creature(0, 5, (-300, -2750, 0), AreneGreffe.OmbreRouge),
            new Creature(0, 6, (500, -2750, 0), AreneGreffe.OmbreRouge),
        ])]),
        new("LORDEMON", 400, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Lordemon)])]),    // 2 763
        new("CERBERE", 400, [new Vague([                                        // 2 100 + 4 × 178
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.Cerbere),
            new Creature(0, 1, (0, -2650, 0), AreneGreffe.Groklebs),
            new Creature(0, 2, (200, -2650, 0), AreneGreffe.Groklebs),
            new Creature(0, 3, (0, -3150, 0), AreneGreffe.Groklebs),
            new Creature(0, 4, (200, -3150, 0), AreneGreffe.Groklebs),
        ])]),
        new("BELZEBUTH", 430, [new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Belzebuth)])]),  // 3 000
        new("DRAGON ZOMBIE", 780, [new Vague([                                  // 4 400 + 2 × 519
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.DragonZombie),
            new Creature(0, 1, (-100, -2900, 0), AreneGreffe.Maudite),
            new Creature(0, 2, (300, -2900, 0), AreneGreffe.Maudite),
        ])]),
        new("DRAGON ROUGE", 1200, [new Vague([                                  // 8 000 + 2 × 240
            new Creature(0, 0, (100, -2900, 0), AreneGreffe.DragonRouge),
            new Creature(0, 1, (-250, -2900, 0), AreneGreffe.SalamandreFeu),
            new Creature(0, 2, (450, -2900, 0), AreneGreffe.SalamandreFeu),
        ])]),

        // ---- les boss rush (LP-014, Vincent, 2026-09-30) ----------------------------------------
        // Chaque boss de son combat seul, sans escorte, une vague chacun ; cinq secondes entre la
        // disparition d'un boss et la naissance du suivant. La bourse : la somme des combats seuls,
        // + 25 %, arrondie à la dizaine quand elle ne tombe pas juste.
        new("[CHALLENGE] - Boss Rush 1", 750, [                                  // (40+100+150+140+170) × 1,25
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.MechantOurs)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.JeuneDragon)]),
            new Vague([new Creature(0, 0, (100, -2900, 0))]),                         // l'Enchanteresse, native
            new Vague([
                new Creature(0, 0, (-70, -2900, 0), AreneGreffe.LoupGarou),
                new Creature(0, 1, (270, -2900, 0), AreneGreffe.Tigre),
            ]),
            new Vague([new Creature(1, 0, (100, -2900, 0), AreneGreffe.Nocturio)]),
        ], PauseChallenge, Challenge: true),
        new("[CHALLENGE] - Boss Rush 2", 1390, [                                 // (180+180+220+230+300) × 1,25 = 1 387,5
            new Vague([new Creature(1, 0, (100, -2900, 0), AreneGreffe.Sylphide)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Fougre)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Durahan)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Troll)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Behemoth)]),
        ], PauseChallenge, Challenge: true),
        new("[CHALLENGE] - Boss Rush 3", 2290, [                                 // (330+330+400+340+430) × 1,25 = 2 287,5
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.GrosPoilu)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.RoiZombie)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Cerbere)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Eifridt)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Belzebuth)]),
        ], PauseChallenge, Challenge: true),
        new("[CHALLENGE] - Ultimate", 5875, [                                    // (400+340+780+780+1200+1200) × 1,25
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.Lordemon)]),
            new Vague([new Creature(0, 0, (100, -2900, 0), AreneGreffe.AngeNoir)]),
            new Vague([
                new Creature(0, 0, (-150, -2900, 0), AreneGreffe.DragonZombie),
                new Creature(0, 0, (350, -2900, 0), AreneGreffe.DragonZombie),
            ]),
            new Vague([
                new Creature(0, 0, (-150, -2900, 0), AreneGreffe.DragonRouge),
                new Creature(0, 0, (350, -2900, 0), AreneGreffe.DragonRouge),
            ]),
        ], PauseChallenge, Challenge: true),
    ];

    /// <summary>La hauteur du sol de la salle du boss (formations.csv, 4/9 : y = 200) : une créature
    /// greffée y naît, et <c>TryPlaceEntity</c> la pose sur la face qui est dessous.</summary>
    private const short HauteurArene = 200;

    /// <summary>
    /// Un palier de l'arène : les PV des créatures × <paramref name="Pv"/> (la bourse aussi), les
    /// coups qu'elles portent au groupe × <paramref name="Degats"/> centièmes.
    /// </summary>
    internal sealed record Palier(string Nom, int Pv, int Degats);

    /// <summary>
    /// Les quatre paliers (Vincent, 2026-09-30) ; ils remplacent les paliers 1 à 10, où PV, dégâts et
    /// bourse étaient × le numéro du palier.
    /// </summary>
    internal static readonly Palier[] Paliers =
    [
        new("Normal", 1, 100),
        new("Difficile", 2, 150),
        new("Tres difficile", 3, 200),
        new("Impossible", 4, 250),
    ];

    /// <summary>Les PV d'une créature de l'arène, palier compris, au plus (voir l'en-tête).</summary>
    private const ushort PvMax = 30000;

    /// <summary>Le dernier choix confirmé au menu ; <c>null</c> tant qu'aucun départ n'a été demandé.</summary>
    internal static (Programme Programme, Palier Palier)? Choix { get; private set; }

    /// <summary>Le joueur a confirmé : l'arène part avec ce programme, à ce palier.</summary>
    internal static void Choisir(Programme programme, Palier palier)
    {
        Choix = (programme, palier);
        Journal($"départ demandé : {programme.Nom}, {palier.Nom}");
        Arene.Partir();
    }

    // ---- le déroulé ------------------------------------------------------------------------------

    /// <summary>Images entre l'arrivée et la première vague.</summary>
    private const int Delai = 30;

    /// <summary>
    /// Millisecondes entre la disparition d'une vague et la naissance de la suivante, en temps réel :
    /// compter des images allongeait la pause dès que le jeu passait sous 60 images par seconde
    /// (Vincent, 2026-09-30 : « plus que 5 secondes »). Par défaut, et pour un boss rush.
    /// </summary>
    private const int PauseParDefaut = 500, PauseChallenge = 5000;

    private const int Enregistrement = 8;                // ints par enregistrement de formation
    private const int TailleScene = 0x9c, PlacesScene = 0x20;
    private const int PoolScene = 0x005ecaa0;
    private static readonly (int Base, int Places)[] s_PoolsEffets = [(0x005ff400, 0x20), (0x005edee0, 0x80)];

    private static Programme? s_Programme;
    private static Palier s_Palier = Paliers[0];
    private static int s_Vague;
    private static int s_Attente;
    private static bool s_EnCours;
    private static bool s_Naissance;                     // SpawnEntityFormation tourne pour le directeur

    /// <summary>La bourse de la victoire du programme en cours, palier compris ; 0 sans programme.</summary>
    internal static int Bourse => s_Programme == null ? 0 : s_Programme.Pieces * s_Palier.Pv;
    private static uint s_Suivis;                        // emplacements de scène de la vague, en masque
    private static uint s_Morts;                         // ceux dont la mort (0x40000000) est vue, pour le journal
    private static long s_DerniereMort, s_Disparition;   // Environment.TickCount64
    private static long s_Reprise;                       // quand la vague suivante naît ; 0 : pas de pause en cours

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        Coutures.Pv pv = Coutures.PvALaNaissance;
        Coutures.PvALaNaissance = (stats, valeur) =>
        {
            valeur = pv(stats, valeur);
            if (!s_Naissance) return valeur;
            int voulu = valeur * s_Palier.Pv;
            if (voulu <= PvMax) return (ushort)voulu;
            Journal($"PV {valeur} × {s_Palier.Pv} ({s_Palier.Nom}) = {voulu} : bornés à {PvMax}");
            return PvMax;
        };

        Coutures.Degats degats = Coutures.DegatsSubis;
        Coutures.DegatsSubis = (attaquant, victime, montant) => Degats(attaquant, victime, degats(attaquant, victime, montant));

        Action image = Coutures.ImageSuivante;
        Coutures.ImageSuivante = () =>
        {
            image();
            if (Arene.Actif) Image();
        };
    }

    /// <summary>Le mode arène vient d'être levé : le programme choisi repart de sa première vague.</summary>
    internal static void Commencer()
    {
        s_Suivis = s_Morts = 0;
        s_DerniereMort = s_Reprise = 0;
        s_Vague = 0;
        s_Attente = Delai;
        s_EnCours = Choix != null;
        s_Programme = null;
        AreneGreffe.Commencer(Choix?.Programme);
        if (Choix is not { } choix) { Journal("aucun programme choisi : l'arène reste vide"); return; }

        s_Programme = choix.Programme;
        s_Palier = choix.Palier;
        Journal($"programme {s_Programme.Nom}, {s_Palier.Nom} (PV × {s_Palier.Pv}, dégâts × {s_Palier.Degats / 100.0:0.##}), {s_Programme.Vagues.Length} vague(s)");
    }

    /// <summary>Le mode arène vient d'être baissé.</summary>
    internal static void Arreter()
    {
        s_EnCours = false;
        s_Suivis = 0;
    }

    private static void Image()
    {
        if (!s_EnCours || s_Programme == null) return;

        if (s_Suivis == 0)
        {
            if (--s_Attente > 0 || Environment.TickCount64 < s_Reprise) return;
            if (s_Reprise != 0)
                Journal($"pause de {Environment.TickCount64 - s_Disparition} ms entre la disparition et la vague {s_Vague + 1}");
            s_Reprise = 0;
            Naitre(s_Programme.Vagues[s_Vague]);
            return;
        }

        // Un emplacement suivi qui n'est plus vivant ne l'est plus jamais pour nous : réutilisé,
        // il porterait autre chose. Le bit 0x40000000 (PV à zéro, WorldSimulationStep) date la mort,
        // pour le journal : la disparition du corps vient après.
        byte* table = Jeu.Mem.Entity(PoolScene);
        for (int i = 0; i < PlacesScene; i++)
        {
            uint bit = 1u << i;
            if ((s_Suivis & bit) == 0) continue;
            uint mot = *(uint*)(table + i * TailleScene);
            if ((mot & 0x80000000) == 0) s_Suivis &= ~bit;
            else if ((mot & 0x40000000) != 0 && (s_Morts & bit) == 0) { s_Morts |= bit; s_DerniereMort = Environment.TickCount64; }
        }
        if (s_Suivis != 0) return;

        s_Disparition = Environment.TickCount64;
        Journal($"vague {s_Vague + 1} vaincue" + (s_DerniereMort != 0 ? $" — {s_Disparition - s_DerniereMort} ms entre la dernière mort et la disparition" : ""));
        s_Morts = 0;
        s_DerniereMort = 0;
        if (++s_Vague < s_Programme.Vagues.Length) { s_Reprise = s_Disparition + s_Programme.Pause; return; }

        s_EnCours = false;
        Journal($"programme {s_Programme.Nom} terminé");
        Arene.Victoire();
    }

    /// <summary>
    /// Une vague : ses enregistrements recopiés depuis les formations de la sous-zone, terminés par
    /// <c>-1</c>, et remis à <c>SpawnEntityFormation</c> en mode 0 — celui des rencontres du jeu
    /// (<c>Goblin.cs</c>) et du banc d'essai.
    /// </summary>
    private static void Naitre(Vague vague)
    {
        byte* repertoire = Jeu.s_Unknown_005e97cc;
        int** formations = repertoire == null ? null : *(int***)(repertoire + 0x34);
        if (formations == null) { Abandonner("la sous-zone n'a pas de table de formations"); return; }
        if (!AreneGreffe.PreparerVague(vague)) { Abandonner($"vague {s_Vague + 1} : ses espèces greffées n'ont pas pu prendre place en VRAM"); return; }

        int* liste = (int*)NativeMemory.AllocZeroed((nuint)((vague.Creatures.Length + 1) * Enregistrement), sizeof(int));
        try
        {
            int n = 0;
            foreach (Creature c in vague.Creatures)
            {
                int** table = formations;
                if (c.Greffe != null)
                {
                    if (!AreneGreffe.Prete(c.Greffe)) { Abandonner($"{c.Greffe.Nom} n'a pas pu être greffé"); return; }
                    table = AreneGreffe.Formations(c.Greffe);
                }
                int* source = table[c.Formation];
                int rang = 0;
                while (rang < c.Rang && source[rang * Enregistrement] != -1) rang++;
                if (source[rang * Enregistrement] == -1) { Abandonner($"formation {c.Formation} sans rang {c.Rang}"); return; }
                int* r = liste + n * Enregistrement;
                for (int k = 0; k < Enregistrement; k++) r[k] = source[rang * Enregistrement + k];
                if (c.Greffe != null)
                {
                    // Le mot 0 n'est pas que l'espèce : octet 0 l'espèce, octet 1 le genre (10 : script au
                    // coup reçu, 11 : à la mort), octets 2-3 le script (+0x0e de l'entité). Seule
                    // l'espèce change ; genre et script sont gardés — l'arène tait tous les scripts.
                    byte* mot = (byte*)r;
                    if (mot[0] != c.Greffe.Indice) { Abandonner($"formation {c.Formation}, rang {c.Rang} : espèce {mot[0]}, pas {c.Greffe.Nom}"); return; }
                    mot[0] = (byte)c.Greffe.Rang;
                    *(short*)((byte*)r + 6) = HauteurArene;
                }
                if (c.Place is { } place)
                {
                    *(short*)((byte*)r + 4) = place.X;           // la hauteur (+6) est gardée
                    *(short*)((byte*)r + 8) = place.Z;           // +0xa, recopié en +0x30, est gardé
                    *(short*)((byte*)r + 0xc) = place.Cap;       // +0xe, recopié en +0x34, est gardé
                    r[4] = -1;
                    r[5] = -1;
                }
                n++;
            }
            liste[n * Enregistrement] = -1;

            uint avant = Vivants();
            int nes;
            s_Naissance = true;
            try { nes = Jeu.SpawnEntityFormation(repertoire, liste, 0, 0); }
            finally { s_Naissance = false; }
            s_Suivis = Vivants() & ~avant;

            if (s_Suivis == 0) { Abandonner($"vague {s_Vague + 1} : aucune créature n'a pu naître"); return; }
            Journal($"vague {s_Vague + 1} : {nes} créature(s) née(s), PV × {s_Palier.Pv}, emplacements 0x{s_Suivis:x8}");
        }
        finally { NativeMemory.Free(liste); }
    }

    private static void Abandonner(string raison)
    {
        s_EnCours = false;
        Journal($"programme arrêté : {raison}");
    }

    /// <summary>Les emplacements vivants de la table de scène, en masque.</summary>
    private static uint Vivants()
    {
        uint masque = 0;
        byte* table = Jeu.Mem.Entity(PoolScene);
        for (int i = 0; i < PlacesScene; i++)
            if ((*(uint*)(table + i * TailleScene) & 0x80000000) != 0) masque |= 1u << i;
        return masque;
    }

    // ---- le palier des dégâts --------------------------------------------------------------------

    private static short Degats(uint* attaquant, uint* victime, short montant)
    {
        if (!Arene.Actif || victime == null || (*victime & 0xfc0000) != 0x800000) return montant;

        bool suivi = s_EnCours && DeLaVague((byte*)attaquant);
        short rendu = suivi && montant > 0
            ? (short)Math.Clamp(montant * s_Palier.Degats / 100, 1, short.MaxValue)
            : montant;
#if DEBUG
        Journal($"J{((byte*)victime)[8] + 1} touché par {Nommer((byte*)attaquant)}{(suivi ? " (suivi)" : "")} : {montant} → {rendu}");
#endif
        return rendu;
    }

    /// <summary>L'attaquant est une créature suivie, ou une entité d'effet qui en vient.</summary>
    private static bool DeLaVague(byte* attaquant)
    {
        if (attaquant == null) return false;
        if (Suivi(attaquant)) return true;
        if (!DansUnPoolEffets(attaquant)) return false;
        return Suivi(*(byte**)(attaquant + 0x74)) || Suivi(*(byte**)(attaquant + 0x6c));
    }

    private static bool Suivi(byte* entite)
    {
        long ecart = entite - Jeu.Mem.Entity(PoolScene);
        if (ecart < 0 || ecart >= PlacesScene * TailleScene || ecart % TailleScene != 0) return false;
        return (s_Suivis & (1u << (int)(ecart / TailleScene))) != 0;
    }

    private static bool DansUnPoolEffets(byte* entite)
    {
        foreach ((int debut, int places) in s_PoolsEffets)
        {
            long ecart = entite - Jeu.Mem.Entity(debut);
            if (ecart >= 0 && ecart < places * TailleScene && ecart % TailleScene == 0) return true;
        }
        return false;
    }

#if DEBUG
    private static string Nommer(byte* entite)
    {
        if (entite == null) return "rien";
        foreach ((int debut, int places) in s_PoolsEffets.Prepend((PoolScene, PlacesScene)))
        {
            long ecart = entite - Jeu.Mem.Entity(debut);
            if (ecart >= 0 && ecart < places * TailleScene && ecart % TailleScene == 0)
                return $"0x{debut:x8}[{ecart / TailleScene}] (sous-type {entite[5]})";
        }
        return $"0x{(nint)entite:x}";
    }
#endif

    private static void Journal(string message) => Console.Error.WriteLine($"[ARENE] {message}");
}
