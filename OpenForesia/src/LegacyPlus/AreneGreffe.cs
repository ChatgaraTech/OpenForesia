// =====================================================================
// LP-013 — la greffe : un monstre d'une autre sous-zone, dans l'arène, comme chez lui.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-29-add-legacy-plus-boss-graft, design G1 à G4).
// Un monstre n'existe que dans sa sous-zone : son espèce, sa fiche, son maillage, ses animations, sa
// texture, ses sons de niveau et la table qui donne son IA sont chargés avec elle. La greffe les fait
// venir dans 4/9, à chaque entrée dans l'arène d'un programme qui en a besoin :
//
//   - le RÉPERTOIRE de la sous-zone d'origine est relu dans l'archive, une fois (état 3 → zone →
//     sous-zone, comme LoadWorldMapArea et LoadWorldMapSubArea) ;
//   - le MAILLAGE (+0x2c) et les ANIMATIONS (+0x7c) de l'espèce sont chargés et relogés dans une
//     mémoire à nous ; la pagination du maillage désigne notre entrée de la table de décodage
//     0x005e8f00 et notre bande de VRAM, et il est relogé à nouveau quand sa place change ;
//     ils prennent les rangs 3 et suivants des tables 0x005e96a0 / 0x005e9640,
//     que 4/9 (trois maillages) laisse libres ;
//   - la TEXTURE (+0x10, l'entrée que la pagination d'origine désigne) est envoyée en VRAM à la place
//     que Placer donne dans la VAGUE de l'espèce (cinq bandes de 128 × 64 mots, 8.1.1), juste avant
//     qu'elle naisse (PreparerVague, LP-014 : un boss rush enchaîne plus de boss que les cinq bandes
//     n'en portent), et l'entrée de décodage écrite ; tout le reste est installé au peuplement ;
//   - l'ESPÈCE et la FICHE : les tables +0x4c et +0x98 du répertoire de 4/9 sont recopiées dans une
//     mémoire à nous, avec les espèces greffées au rang 16 et suivants ; le répertoire est repointé
//     sur elles jusqu'au prochain chargement ;
//   - l'IA : l'adresse du gestionnaire d'origine, dans un créneau libre de 0x0056ad48 ;
//   - les SONS DE NIVEAU (ids 60 à 76, banc Snd\Qzz_ss.snd) que l'espèce joue : leurs échantillons
//     ajoutés au bout du banc de 4/9, sous des ids que 4/9 n'emploie pas.
//
// La traduction (G3), pour la seule LIGNÉE des créatures greffées (elles et leurs effets) :
//   - le CONTEXTE (8.1.2, AvantGestionnaire / ApresGestionnaire / GestionnaireInstalle) : pendant
//     le gestionnaire d'une entité de la lignée, le répertoire de 4/9 désigne les catalogues
//     d'effets (+0xcc, +0xd0, +0xd4) et la table de sons (+0xa8) de la sous-zone d'origine, et un
//     effet de sous-type >= 0xc0 se résout dans les tables de celle-ci ; voir « la lignée et le
//     contexte », plus bas ;
//   - SonPositionnel : un son de niveau traduit vers son id greffé, greffé à sa première demande ;
//     sans place, coupé ;
//   - EffetCoupe : un effet de sous-type >= 0xc0 d'une espèce sans EffetsGreffes coupé à la
//     naissance (lot 1). Depuis le 2026-09-29, toutes les espèces ont leurs effets greffés ; la
//     coupure reste pour une espèce qu'on remettrait à faux. Sous 0xc0, la table est statique, la
//     même partout : l'éclat 0x39 passe tel quel.
//   - ButinNe : ni objet ni pièces tombés au sol (Vincent, 2026-09-29 : la récompense de l'arène,
//     c'est la bourse du programme, versée au groupe à la victoire — Arene.Recompenser) ; les éclats
//     d'expérience restent.
// Journal [GREFFE] ; toute référence coupée y est écrite une fois par chargement.
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;
using Resources = OpenForesia.Blaze.Resources;
using Winsys3d = OpenForesia.Blaze.Winsys3d;

namespace OpenForesia.LegacyPlus;

internal static unsafe class AreneGreffe
{
    /// <summary>Une sous-zone d'où viennent des monstres ; son répertoire, relu une fois.</summary>
    internal sealed class Source(byte zone, byte sousZone)
    {
        public readonly byte Zone = zone, SousZone = sousZone;
        public byte* Repertoire;
        public byte[]? Sons;                    // le fichier Snd\Qzz_ss.snd, lu une fois
    }

    /// <summary>
    /// Une espèce greffée : son rang dans la table +0x4c de sa sous-zone, l'adresse de son IA (la
    /// table 0x0056ad48 de sa sous-zone, relevée par <c>OpenForesia.Tools greffe</c>), et les sons de
    /// niveau qu'elle joue. Sa place en VRAM n'est pas fixe : <see cref="Placer"/> la donne à chaque
    /// entrée dans l'arène, selon les autres espèces du programme.
    /// </summary>
    internal sealed class Espece
    {
        public required string Nom;
        public required Source Source;
        public required int Indice;
        public required uint Ia;
        public required int[] SonsDeNiveau;

        /// <summary>
        /// Ses effets par sous-zone naissent et se résolvent dans sa sous-zone d'origine (8.1.2) ;
        /// faux, ils sont coupés à la naissance (lot 1). Vrai pour toutes les espèces depuis le
        /// 2026-09-29 (le souffle de l'ours validé en jeu) ; à mettre à faux pour une espèce dont un
        /// effet se révélerait mal greffé.
        /// </summary>
        public bool EffetsGreffes = true;

        // Ce que le chargement donne, une fois pour toutes.
        public byte* Ligne;                     // les 8 octets de l'espèce d'origine
        public byte* Fiche;                     // ses 0x60 octets de fiche
        public byte* Animations;                // le tableau de clips (RelocateAnimationBundle)
        public int* EntreeTexture;              // l'entrée d'archive de sa texture
        public int* EntreeModele;               // l'entrée d'archive de son paquet de maillage
        public uint Pagination;                 // la pagination d'origine du maillage
        public int Largeur, Hauteur;            // sa texture, en mots et en lignes
        public int LignesPalette;               // les lignes de sa palette (1 en 8 bits, 16 en 4 bits)

        // Sa place dans ce programme (Placer), et le maillage relogé pour elle.
        public int TexX, TexY, Palette, Entree;
        public byte* Modele;                    // le tableau de parties (RelocateModelBundle)
        public byte* Paquet;                    // le tampon qui le porte
        public (int Bande, int Entree) ModelePour = (-1, -1);

        // Ce que chaque entrée dans l'arène donne.
        public int Rang;                        // son rang dans la table d'espèces de 4/9
        public int Maillage;                    // son rang dans les tables 0x005e96a0 / 0x005e9640
    }

    // ---- les monstres greffés (mesures : legacy.md, LP-013) ------------------------------------

    private static readonly Source s_LieuDeCoupe = new(3, 5);

    /// <summary>
    /// Le Méchant ours de 3/5, espèce 0 : maillage 0, IA 16 = 0x004A4AE0, fiche 0 (300 PV, rang 38).
    /// Texture : l'entrée 5, 60 × 48 mots, 8 bits.
    /// </summary>
    internal static readonly Espece MechantOurs = new()
    {
        Nom = "Méchant ours", Source = s_LieuDeCoupe, Indice = 0, Ia = 0x004A4AE0, SonsDeNiveau = [0x3f],
    };

    /// <summary>
    /// Le Grizzly de 3/5, espèce 4 : maillage 2, IA 4 = 0x004A4270, fiche 3 (80 PV, rang 25). 
    /// Texture : l'entrée 7, 128 × 64 mots, 8 bits.
    /// </summary>
    internal static readonly Espece Grizzly = new()
    {
        Nom = "Grizzly", Source = s_LieuDeCoupe, Indice = 4, Ia = 0x004A4270, SonsDeNiveau = [],
    };

    private static readonly Source s_GrotteSouterraine = new(3, 9);

    /// <summary>
    /// Le Behemoth de 3/9 « La grotte souterraine », espèce 0 : maillage 0, IA 17 = 0x004A54E0, fiche 0
    /// (2 160 PV, rang 150). Ses sons (3, 0x20, 0x35) sont du banc commun, son lancement de sort
    /// (0xbd) et son éclat (0x39) de la table statique ; seul l'atterrissage de son bond (0xc0 +
    /// octet 2 du premier bloc de +0xcc : 0xdf en 3/9, 0x00483650) dépend de la sous-zone — greffé (8.1.2).
    /// Texture : l'entrée 5, 92 × 48 mots, 8 bits.
    /// </summary>
    internal static readonly Espece Behemoth = new()
    {
        Nom = "Behemoth", Source = s_GrotteSouterraine, Indice = 0, Ia = 0x004A54E0, SonsDeNiveau = [],
    };

    // Les trois boss de 2026-09-29 (legacy.md, LP-013, « Les boss restants ») : leurs sons sont du banc
    // commun, sauf le cri du Wybarn (0x47, greffé) ; leurs effets par sous-zone sont greffés (8.1.2).

    private static readonly Source s_VieilleTour = new(4, 7);
    private static readonly Source s_SalleDesDemons = new(10, 12);
    private static readonly Source s_NidDesMonstres = new(8, 1);

    /// <summary>
    /// La Sylphide de 4/7 « La vieille tour », espèce 1 : maillage 1, IA 8 = 0x004B4570, fiche 1 (563 PV,
    /// rang 100). Des sorts et le son 3, rien de sa sous-zone. Texture : l'entrée 4, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Sylphide = new()
    {
        Nom = "Sylphide", Source = s_VieilleTour, Indice = 1, Ia = 0x004B4570, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Wybarn de 4/7, espèce 2 : maillage 2, IA 14 = 0x004B7540, fiche 2 (700 PV, rang 96). Son cri
    /// 0x47 (Q04_07.snd, 7 100 octets) est greffé ; son effet 0xc0 + octet 0x0c du premier bloc de
    /// +0xcc (0xdf = 0x00483930 en 4/7) est greffé. Texture : l'entrée 5, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Wybarn = new()
    {
        Nom = "Wybarn", Source = s_VieilleTour, Indice = 2, Ia = 0x004B7540, SonsDeNiveau = [0x47],
    };

    /// <summary>
    /// Le gros poilu de 10/12 « La salle des démons », espèce 0 : maillage 0, IA 19 = 0x00500E10, fiche 0
    /// (2 300 PV, rang 218). Sons 2, 3, 0x24, 0x25, communs ; son effet 0xc0 + octet (+0x16) du premier
    /// bloc de +0xcc est greffé. Texture : l'entrée 4, 96 × 64 mots.
    /// </summary>
    internal static readonly Espece GrosPoilu = new()
    {
        Nom = "Le gros poilu", Source = s_SalleDesDemons, Indice = 0, Ia = 0x00500E10, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Fougre de 8/1 « Nid des monstres », espèce 1 : maillage 1, IA 3 = 0x004E20F0, fiche 1 (1 030 PV,
    /// rang 180). Son effet 0xc4 (0x0042f2a0 en 8/1) est greffé. Texture : l'entrée 4, 104 × 64 mots.
    /// </summary>
    internal static readonly Espece Fougre = new()
    {
        Nom = "Fougre", Source = s_NidDesMonstres, Indice = 1, Ia = 0x004E20F0, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Grifon de 8/1, espèce 0 : maillage 0, IA 2 = 0x004E1380, fiche 0 (121 PV, rang 65). Son 3 et
    /// éclat 0x39, communs. Texture : l'entrée 3, 112 × 48 mots.
    /// </summary>
    internal static readonly Espece Grifon = new()
    {
        Nom = "Grifon", Source = s_NidDesMonstres, Indice = 0, Ia = 0x004E1380, SonsDeNiveau = [],
    };

    private static readonly Source s_MineAbandonnee = new(1, 4);

    /// <summary>
    /// Le Jeune dragon de 1/4 « La mine abandonnée », espèce 2 : maillage 2, IA 13 = 0x004944E0,
    /// fiche 2 (700 PV, rang 40). Son cri 0x41 (Q01_04.snd) est greffé ; ses effets par sous-zone
    /// (EffetsDesBoss.md) : 0x00483B50 (0x0056b0c8[31], catalogue +0xcc[0x0d]) et son enfant
    /// 0x00482FF0 (+0xd0), l'éclair 0x00409E40 (0x0056b0c8[32], +0xcc[0x0e]), son son +0xa8[0x2c] et
    /// ses onze enfants 0x00409F40 (0x0056b1c8[30]). Texture : l'entrée 5, 96 × 48 mots.
    /// </summary>
    internal static readonly Espece JeuneDragon = new()
    {
        Nom = "Jeune dragon", Source = s_MineAbandonnee, Indice = 2, Ia = 0x004944E0, SonsDeNiveau = [0x41],
    };

    private static readonly Source s_ChateauDeLImmortel = new(6, 5);

    /// <summary>
    /// Le Loup-garou de 6/5 « Château de l'immortel », espèce 0 : maillage 0, IA 6 = 0x004D7200, fiche
    /// 0 (468 PV, rang 85). Il joue +0xa8[0x2c] par PlaySoundEffect (SonJoue) et fait naître 0xde
    /// (0x0042A440, 0x0056b0c8[30]) et ses enfants 0x0042A540 (0x0056b1c8[30]). Texture : l'entrée 7,
    /// 60 × 48 mots.
    /// </summary>
    internal static readonly Espece LoupGarou = new()
    {
        Nom = "Loup-garou", Source = s_ChateauDeLImmortel, Indice = 0, Ia = 0x004D7200, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Tigre de 6/5, espèce 1 : maillage 1, IA 7 = 0x004D80D0, fiche 1 (500 PV, rang 85). Même son
    /// +0xa8[0x2c] et même effet 0xde que le Loup-garou. Texture : l'entrée 8, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Tigre = new()
    {
        Nom = "Tigre", Source = s_ChateauDeLImmortel, Indice = 1, Ia = 0x004D80D0, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDuNocturio = new(6, 11);

    /// <summary>
    /// Le Nocturio de 6/11, espèce 1 — celui de la formation 1, avec ses deux Sacubas (la formation 0,
    /// espèce 0, est le même Nocturio seul) : maillage 1, IA 14 = 0x004DACC0, fiche 1 (840 PV,
    /// rang 150). Des sorts, et sept effets par sous-zone (EffetsDesBoss.md : 0x00429A20, 0x0042A920
    /// et leurs enfants). Texture : l'entrée 8, 116 × 64 mots.
    /// </summary>
    internal static readonly Espece Nocturio = new()
    {
        Nom = "Nocturio", Source = s_SalleDuNocturio, Indice = 1, Ia = 0x004DACC0, SonsDeNiveau = [],
    };

    /// <summary>
    /// La Sacuba de 6/11, espèce 2 : maillage 2, IA 13 = 0x004D9D90, fiche 2 (190 PV, rang 125).
    /// Texture : l'entrée 9, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Sacuba = new()
    {
        Nom = "Sacuba", Source = s_SalleDuNocturio, Indice = 2, Ia = 0x004D9D90, SonsDeNiveau = [],
    };

    private static readonly Source s_LabyrintheDesMorts = new(5, 4);

    /// <summary>
    /// Le Durahan de 5/4 « Le Labyrinthe des morts », espèce 2 : maillage 2, IA 7 = 0x004C4F90, fiche
    /// 2 (808 PV, rang 80). Son cri 0x47 (Q05_04.snd) est greffé ; son effet 0x00422D50
    /// (0x0056b0c8[31], catalogue +0xcc[9]) et son enfant 0x00422E50 viennent de 5/4. L'effet qu'il
    /// lit dans son +0x14 vaut 0x36, posé par sa formation : statique, le même partout. Texture :
    /// l'entrée 8, 104 × 64 mots.
    /// </summary>
    internal static readonly Espece Durahan = new()
    {
        Nom = "Durahan", Source = s_LabyrintheDesMorts, Indice = 2, Ia = 0x004C4F90, SonsDeNiveau = [0x47],
    };

    /// <summary>
    /// Le Sanguinaire de 5/4, espèce 3 : maillage 3, IA 0 = 0x004C2770, fiche 3 (241 PV, rang 70).
    /// Texture : l'entrée 9, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Sanguinaire = new()
    {
        Nom = "Sanguinaire", Source = s_LabyrintheDesMorts, Indice = 3, Ia = 0x004C2770, SonsDeNiveau = [],
    };

    private static readonly Source s_RuinesSouterraines = new(1, 9);

    /// <summary>
    /// Le Troll de 1/9 « Les ruines souterraines », espèce 3 : maillage 3, IA 16 = 0x004965B0, fiche
    /// 3 (850 PV, rang 130). Son cri 0x40 (Q01_09.snd) est greffé ; son éclair 0x00409E40
    /// (0x0056b0c8[32], catalogue +0xcc[0x0e]) et ses enfants 0x00409F40 viennent de 1/9 — le même
    /// que celui du Jeune dragon. Texture : l'entrée 5, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Troll = new()
    {
        Nom = "Troll", Source = s_RuinesSouterraines, Indice = 3, Ia = 0x004965B0, SonsDeNiveau = [0x40],
    };

    /// <summary>
    /// Le Géant vert de 1/9, espèce 0 : maillage 0, IA 15 = 0x00495C90, fiche 0 (400 PV, rang 100).
    /// Texture : l'entrée 2, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece GeantVert = new()
    {
        Nom = "Géant vert", Source = s_RuinesSouterraines, Indice = 0, Ia = 0x00495C90, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDeLAngeNoir = new(10, 14);

    /// <summary>
    /// L'Ange noir de 10/14 « La salle des démons », espèce 0 : maillage 0, IA 26 = 0x00502210, fiche
    /// 0 (2 391 PV, rang 238). Des sorts, et huit effets par sous-zone (EffetsDesBoss.md) lus dans les
    /// trois catalogues — la pluie (BlackAngelRainSubtype) dans +0xd4[7]. Texture : l'entrée 4,
    /// 128 × 64 mots.
    /// </summary>
    internal static readonly Espece AngeNoir = new()
    {
        Nom = "L'Ange noir", Source = s_SalleDeLAngeNoir, Indice = 0, Ia = 0x00502210, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDEifridt = new(10, 11);

    /// <summary>
    /// Eifridt de 10/11 « La salle des démons », espèce 0 : maillage 0, IA 17 = 0x005003E0, fiche 0
    /// (2 400 PV, rang 212). Des sorts, et les deux effets du Jeune dragon (0x00483B50 et son enfant
    /// 0x00482FF0), pris ici dans les tables de 10/11. Texture : l'entrée 3, 128 × 128 mots — la
    /// première qui prend deux bandes de VRAM (Placer).
    /// </summary>
    internal static readonly Espece Eifridt = new()
    {
        Nom = "Eifridt", Source = s_SalleDEifridt, Indice = 0, Ia = 0x005003E0, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDuLordemon = new(10, 13);

    /// <summary>
    /// Le Lordémon de 10/13 « La salle des démons », espèce 0 : maillage 0, IA 25 = 0x00501840, fiche
    /// 0 (2 763 PV, rang 235). Des sorts, et six effets par sous-zone (EffetsDesBoss.md : 0x0043EB60,
    /// 0x00486680, 0x004868E0 et leurs enfants). Texture : l'entrée 3, 120 × 64 mots.
    /// </summary>
    internal static readonly Espece Lordemon = new()
    {
        Nom = "Lordémon", Source = s_SalleDuLordemon, Indice = 0, Ia = 0x00501840, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDuCerbere = new(10, 9);

    /// <summary>
    /// Le Cerbère de 10/9 « La grotte scellée », espèce 0 : maillage 0, IA 14 = 0x004F7980, fiche 0
    /// (2 100 PV, rang 200). Neuf effets par sous-zone (EffetsDesBoss.md), dont l'émetteur 0x00484F80
    /// qui lit son enfant dans +0xd4[0x0c] : un sous-type que la mesure ne lit pas, résolu en jeu par
    /// le contexte. Texture : l'entrée 4, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Cerbere = new()
    {
        Nom = "Cerbère", Source = s_SalleDuCerbere, Indice = 0, Ia = 0x004F7980, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Groklebs de 10/9, espèce 1 : maillage 1, IA 12 = 0x004F5630, fiche 1 (178 PV, rang 157).
    /// Texture : l'entrée 5, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Groklebs = new()
    {
        Nom = "Groklebs", Source = s_SalleDuCerbere, Indice = 1, Ia = 0x004F5630, SonsDeNiveau = [],
    };

    private static readonly Source s_MontagneDuDragon = new(2, 0);

    /// <summary>
    /// Le Dragon rouge de 2/0 « La montagne du dragon de feu », espèce 0 : maillage 0, IA 1 =
    /// 0x00499140, fiche 0 (8 000 PV, rang 306 — en Impossible, ses PV butent sur les 30 000 de
    /// AreneDirecteur.PvMax). Son cri 0x45 (Q02_00.snd) est greffé ; des sorts, et sept effets par
    /// sous-zone (EffetsDesBoss.md : 0x0040CDA0, 0x0040D560, 0x004830F0 et leurs enfants). Texture :
    /// l'entrée 4, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece DragonRouge = new()
    {
        Nom = "Dragon rouge", Source = s_MontagneDuDragon, Indice = 0, Ia = 0x00499140, SonsDeNiveau = [0x45],
    };

    /// <summary>
    /// La Salamandre feu de 2/0, espèce 2 : maillage 1 (celui de la Salamandre, variante 1 en octet
    /// 2), IA 0 = 0x00497EF0, fiche 2 (240 PV, rang 155). Texture : l'entrée 5, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece SalamandreFeu = new()
    {
        Nom = "Salamandre feu", Source = s_MontagneDuDragon, Indice = 2, Ia = 0x00497EF0, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDeBelzebuth = new(10, 15);

    /// <summary>
    /// Belzébuth de 10/15 « La salle des démons », espèce 0 : maillage 0, IA 27 = 0x00502CD0, fiche 0
    /// (3 000 PV, rang 320). Son cri 0x3c (Q10_15.snd) est greffé ; des sorts, et onze effets par
    /// sous-zone lus dans les catalogues +0xcc (octets 3 à 7) et +0xd0 (EffetsDesBoss.md). Le
    /// « script 0x00553278 » de son geste 2 est un script de SONS différés (voix 5 du banc commun),
    /// pas un script de carte : l'arène ne le tait pas. Texture : l'entrée 3, 128 × 192 mots — elle
    /// prend les trois bandes de VRAM (y 64 à 255).
    /// </summary>
    internal static readonly Espece Belzebuth = new()
    {
        Nom = "Belzébuth", Source = s_SalleDeBelzebuth, Indice = 0, Ia = 0x00502CD0, SonsDeNiveau = [0x3c],
    };

    private static readonly Source s_GrotteDuDragonZombie = new(10, 4);

    /// <summary>
    /// Le Dragon zombie de 10/4 « La grotte scellée », espèce 0 : maillage 0, IA 13 = 0x004F6740,
    /// fiche 0 (4 400 PV, rang 180). Son cri 0x42 (Q10_04.snd) est greffé ; quatre effets par
    /// sous-zone mesurés (EffetsDesBoss.md), plus un tiré au sort parmi les octets 0x10, 0x0c et 0x11
    /// du catalogue +0xcc — lu en jeu, dans le catalogue de 10/4, par le contexte. Texture : l'entrée
    /// 3, 120 × 96 mots — deux bandes de VRAM.
    /// </summary>
    internal static readonly Espece DragonZombie = new()
    {
        Nom = "Dragon zombie", Source = s_GrotteDuDragonZombie, Indice = 0, Ia = 0x004F6740, SonsDeNiveau = [0x42],
    };

    /// <summary>
    /// La Maudite de 10/4, espèce 1 : maillage 1, IA 5 = 0x004F4FD0, fiche 1 (519 PV, rang 157).
    /// Texture : l'entrée 4, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece Maudite = new()
    {
        Nom = "Maudite", Source = s_GrotteDuDragonZombie, Indice = 1, Ia = 0x004F4FD0, SonsDeNiveau = [],
    };

    private static readonly Source s_SalleDuRoiZombie = new(5, 9);

    /// <summary>
    /// Le Roi-zombie de 5/9 « Le Labyrinthe des morts », espèce 1 : maillage 1, IA 12 = 0x004C7240,
    /// fiche 1 (1 350 PV, rang 150). Des sorts, et treize effets par sous-zone lus dans +0xcc et
    /// +0xd0 (EffetsDesBoss.md). La texture en plus que la mesure lui prêtait (entrée 9, lue dans la
    /// planche 0x00550460) n'existe pas : la planche a quatre images sur l'entrée commune 0x10, et
    /// l'outil en lit huit. Texture : l'entrée 5, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece RoiZombie = new()
    {
        Nom = "Roi-zombie", Source = s_SalleDuRoiZombie, Indice = 1, Ia = 0x004C7240, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Durahan-noir de 5/9, espèce 0 : maillage 0, IA 7 = 0x004C4F90 (le corps du Durahan),
    /// fiche 0 (358 PV, rang 110) ; son +0x14 vaut 75, posé par la formation. Texture : l'entrée 4,
    /// 128 × 64 mots.
    /// </summary>
    internal static readonly Espece DurahanNoir = new()
    {
        Nom = "Durahan-noir", Source = s_SalleDuRoiZombie, Indice = 0, Ia = 0x004C4F90, SonsDeNiveau = [],
    };

    /// <summary>
    /// Le Revenant de 5/9, espèce 2 : maillage 2, IA 11 = 0x004C6700, fiche 2 (190 PV, rang 100).
    /// Texture : l'entrée 6, 96 × 48 mots.
    /// </summary>
    internal static readonly Espece Revenant = new()
    {
        Nom = "Revenant", Source = s_SalleDuRoiZombie, Indice = 2, Ia = 0x004C6700, SonsDeNiveau = [],
    };

    /// <summary>
    /// L'Ombre rouge de 5/9, espèce 3 : maillage 3, IA 9 = 0x004C5A30, fiche 3 (140 PV, rang 102).
    /// Texture : l'entrée 7, 128 × 64 mots.
    /// </summary>
    internal static readonly Espece OmbreRouge = new()
    {
        Nom = "Ombre rouge", Source = s_SalleDuRoiZombie, Indice = 3, Ia = 0x004C5A30, SonsDeNiveau = [],
    };

    private const int PremierRang = 16;          // rang de la première espèce greffée dans 4/9
    private const int PremierMaillage = 3;       // rang du premier maillage greffé (4/9 en a trois)
    private const int PremierSon = 69;           // premier id de son de niveau libre dans 4/9 (0x45)
    private const int FinSons = 0x4c;            // dernier id de son de niveau (PlaySoundEffect)
    private const int TailleBanc = 260000;       // le banc de niveau, 0x00600900
    private const int PremierCreneauIa = 16;     // où chercher un créneau libre de 0x0056ad48
    private const uint CorpsVide = 0x00401FA0;
    private const int SonCoupe = 0xff;           // au-delà de 0x4c : PlaySoundEffect ne joue rien

    private const int PoolScene = 0x005ecaa0, TailleEntite = 0x9c, PlacesScene = 0x20;

    // ---- l'état d'une entrée dans l'arène --------------------------------------------------------

    private static List<Espece> s_Greffees = new();          // demandées par le programme
    private static bool s_Pret;                              // installées dans ce chargement
    private static byte* s_TableEspeces;                     // 0x20 × 8 octets
    private static byte* s_TableFiches;                      // 0x20 × 0x60 octets
    private static readonly Dictionary<(Source, int), int> s_Sons = new();   // (sous-zone, id d'origine) → id greffé
    private static readonly HashSet<string> s_Coupes = new();

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        s_TableEspeces = (byte*)NativeMemory.AllocZeroed(0x20 * 8);
        s_TableFiches = (byte*)NativeMemory.AllocZeroed(0x20 * 0x60);

        Coutures.Liste liste = Coutures.ListeDApparition;
        Coutures.ListeDApparition = (indice, l) =>
        {
            l = liste(indice, l);
            if (Arene.Actif && !s_Pret && indice != 0xff && s_Greffees.Count > 0) Preparer();
            return l;
        };

        Coutures.Son son = Coutures.SonPositionnel;
        Coutures.SonPositionnel = (id, position) => Son(son(id, position), position);
        Func<int, int> joue = Coutures.SonJoue;
        Coutures.SonJoue = id => SonJoue(joue(id));

        Coutures.Effet effet = Coutures.EffetCoupe;
        Coutures.EffetCoupe = (source, sousType) => effet(source, sousType) || Effet(source, sousType);

        Coutures.Butin butin = Coutures.ButinNe;
        Coutures.ButinNe = (entite, b) =>
        {
            butin(entite, b);
            if (Greffee(entite)) *(short*)(b + 0x10) = 1;    // éteint à la passe suivante, avant d'être ramassable
        };

        InstallerContexte();
    }

    /// <summary>
    /// Le mode arène vient d'être levé pour ce programme : les espèces greffées qu'il demande seront
    /// installées au peuplement de 4/9 (<see cref="Preparer"/>).
    /// </summary>
    internal static void Commencer(AreneDirecteur.Programme? programme)
    {
        s_Pret = false;
        s_Sons.Clear();
        s_Coupes.Clear();
        ViderLignees();
        if (programme == null) { s_Greffees = new(); return; }
        s_Greffees = programme.Vagues.SelectMany(v => v.Creatures).Select(c => c.Greffe)
                              .OfType<Espece>().Distinct().ToList();
    }

    /// <summary>L'espèce est installée dans ce chargement de 4/9 : son rang est bon.</summary>
    internal static bool Prete(Espece espece) => s_Pret && s_Greffees.Contains(espece);

    /// <summary>La table des formations (+0x34) de la sous-zone d'origine, pour le directeur.</summary>
    internal static int** Formations(Espece espece) =>
        espece.Source.Repertoire == null ? null : *(int***)(espece.Source.Repertoire + 0x34);

    // ---- l'installation ------------------------------------------------------------------------

    private static void Preparer()
    {
        s_Pret = true;
        try
        {
            byte* repertoire = Jeu.s_Unknown_005e97cc;
            RecopierTables(repertoire);

            foreach (Espece e in s_Greffees)
            {
                if (e.Source.Repertoire == null) LireRepertoire(e.Source);
                if (e.Ligne == null) Charger(e);
            }

            // En mémoire seulement : la VRAM (texture, palette, entrée de décodage, maillage relogé
            // pour sa place) attend la vague de l'espèce — PreparerVague (LP-014, design D2).
            int k = 0;
            foreach (Espece e in s_Greffees)
            {
                Installer(e, repertoire, PremierRang + k, PremierMaillage + k);
                k++;
            }
            Journal($"{s_Greffees.Count} espèce(s) greffée(s) : {string.Join(", ", s_Greffees.Select(e => $"{e.Nom} (rang {e.Rang})"))}");
        }
        catch (Exception ex)
        {
            Journal($"greffe impossible ({ex.GetType().Name} : {ex.Message}) — le programme n'aura pas ses monstres");
            s_Greffees.Clear();
        }
    }

    /// <summary>
    /// Les tables d'espèces (+0x4c) et de fiches (+0x98) de 4/9, recopiées dans nos deux tables, et le
    /// répertoire repointé sur elles. 4/9 en a trois ; on en recopie seize — les rangs au-delà des
    /// trois ne sont désignés par rien, et la lecture reste dans le tampon du répertoire.
    /// </summary>
    private static void RecopierTables(byte* repertoire)
    {
        byte* especes = *(byte**)(repertoire + 0x4c);
        byte* fiches = *(byte**)(repertoire + 0x98);
        if (especes != s_TableEspeces)
        {
            NativeMemory.Copy(especes, s_TableEspeces, PremierRang * 8);
            *(byte**)(repertoire + 0x4c) = s_TableEspeces;
        }
        if (fiches != s_TableFiches)
        {
            NativeMemory.Copy(fiches, s_TableFiches, PremierRang * 0x60);
            *(byte**)(repertoire + 0x98) = s_TableFiches;
        }
    }

    /// <summary>Le répertoire de la sous-zone d'origine, comme LoadWorldMapArea puis LoadWorldMapSubArea le lisent.</summary>
    private static void LireRepertoire(Source s)
    {
        byte* etat = Jeu.Mem.s_MainResourceDirectory_005e97c4;
        byte* zone = Charger((int*)(nint)((int*)*(byte**)(etat + 0x48))[s.Zone]);
        Jeu.Res.FixupResourceHeader(zone);
        byte* sousZone = Charger((int*)(nint)((int*)*(byte**)(zone + 0x48))[s.SousZone]);
        Jeu.Res.FixupResourceHeader(sousZone);
        NativeMemory.Free(zone);
        s.Repertoire = sousZone;

        string chemin = Path.Combine("snd", $"Q{s.Zone:D2}_{s.SousZone:D2}.snd");
        s.Sons = File.Exists(chemin) ? File.ReadAllBytes(chemin) : null;
        Journal($"répertoire de {s.Zone}/{s.SousZone} relu ; sons de niveau {(s.Sons == null ? "introuvables" : $"{s.Sons.Length} octets")}");
    }

    private static byte* Charger(int* entree)
    {
        byte* tampon = (byte*)NativeMemory.AllocZeroed((nuint)(entree[1] + 16));
        Jeu.Res.LoadResourceEntry(entree, tampon);
        return tampon;
    }

    /// <summary>
    /// L'espèce, sa fiche, ses animations, l'entrée de sa texture et ses dimensions, l'entrée de son
    /// paquet de maillage — tout ce qui ne dépend pas de sa place en VRAM.
    /// </summary>
    private static void Charger(Espece e)
    {
        byte* rep = e.Source.Repertoire;
        byte* ligne = *(byte**)(rep + 0x4c) + e.Indice * 8;
        e.Fiche = (byte*)NativeMemory.Alloc(0x60);
        NativeMemory.Copy(*(byte**)(rep + 0x98) + ligne[4] * 0x60, e.Fiche, 0x60);

        int* modele = *(int**)(rep + 0x2c) + ligne[0] * 2;
        e.EntreeModele = (int*)(nint)modele[0];
        e.Pagination = (uint)modele[1];
        int entreeOrigine = (int)(e.Pagination >> 16) & 0xff;
        e.EntreeTexture = (int*)(nint)(*(int**)(rep + 0x10))[entreeOrigine * 2];

        byte* tampon = Charger(e.EntreeTexture);
        try
        {
            Resources.TimImage tim;
            Winsys3d.OpenTim(tampon);
            Winsys3d.ReadTim(&tim);
            e.Largeur = tim.PixelRect[2];
            e.Hauteur = tim.PixelRect[3];
            e.LignesPalette = tim.ClutRect == null ? 0 : tim.ClutRect[3];
        }
        finally { NativeMemory.Free(tampon); }

        byte* animations = Charger((int*)(nint)(*(int**)(rep + 0x7c))[ligne[0]]);
        byte* clips;
        Jeu.RelocateAnimationBundle((int*)animations, &clips);
        e.Animations = clips;

        e.Ligne = (byte*)NativeMemory.Alloc(8);            // en dernier : Ligne non nulle = chargée
        NativeMemory.Copy(ligne, e.Ligne, 8);

        Journal($"{e.Nom} ({e.Source.Zone}/{e.Source.SousZone}, espèce {e.Indice}) : maillage {ligne[0]}, fiche {ligne[4]}, "
                + $"texture {entreeOrigine} ({e.Largeur} × {e.Hauteur} mots, palette de {e.LignesPalette} ligne(s)), "
                + $"pagination 0x{e.Pagination:x8}");
    }

    // ---- la place en VRAM (8.1.1) ----------------------------------------------------------------
    //
    // Le relevé VRAM de 4/9 (legacyplus-vram-z4-s9.txt) laisse libre x 128..255 en y 16..255 dans la
    // page (128, 0), et en y 304..447 dans la page (128, 256). Un maillage ne se déplace dans sa page
    // que par la bande de V de sa pagination (AdjustModelTexturePages ajoute (p >> 8) & 0xc0 à ses V),
    // pas en U : une texture commence donc en x = 128 et en y multiple de 64 dans sa page. D'où cinq
    // bandes de 128 × 64 mots : y 64, 128 et 192 en haut (la bande 0 porte en y 0..15 autre chose),
    // y 320 et 384 en bas (y 256..303 porte d'autres pages et nos palettes, y 448.. est occupé) — la
    // page, elle, passe par l'entrée de décodage (EnvoyerTexture : TexY & 0x100). Une texture plus
    // haute que 64 lignes prend des bandes qui se suivent DANS UNE MÊME PAGE : 128 × 192 (Belzébuth)
    // prend les trois du haut. Les palettes vont en ligne 288 et suivantes (x 0), les entrées de
    // décodage en 0xe8 et suivantes. Cinq bandes, c'est la place du Roi-zombie et de ses trois
    // escortes (quatre textures de 64 lignes).

    private const int PageX = 128, LargeurPage = 128, HauteurBande = 64;
    private const int PremierePalette = 288, DernierePalette = 319, PremiereEntree = 0xe8;

    /// <summary>Les bandes libres : leur y, et leur page (0 : (128, 0), 1 : (128, 256)).</summary>
    private static readonly (int Y, int Page)[] s_Bandes = [(64, 0), (128, 0), (192, 0), (320, 1), (384, 1)];

    /// <summary>Une place pour chaque espèce de la vague, dans l'ordre de la vague.</summary>
    private static void Placer(List<Espece> especes)
    {
        var prises = new bool[s_Bandes.Length];
        int palette = PremierePalette, k = 0;
        foreach (Espece e in especes)
        {
            int n = (e.Hauteur + HauteurBande - 1) / HauteurBande;
            if (e.Largeur > LargeurPage || n > 3)
                throw new InvalidOperationException($"{e.Nom} : texture de {e.Largeur} × {e.Hauteur} mots, plus grande que la place libre");

            // La première suite de n bandes libres, qui se suivent dans une même page.
            int b = -1;
            for (int i = 0; i + n <= s_Bandes.Length && b < 0; i++)
            {
                bool bonne = true;
                for (int j = i; j < i + n; j++)
                    if (prises[j] || s_Bandes[j].Page != s_Bandes[i].Page || s_Bandes[j].Y != s_Bandes[i].Y + (j - i) * HauteurBande)
                        bonne = false;
                if (bonne) b = i;
            }
            if (b < 0)
                throw new InvalidOperationException($"{e.Nom} : plus de bande libre en VRAM ({e.Largeur} × {e.Hauteur} mots)");
            for (int j = b; j < b + n; j++) prises[j] = true;

            int lignes = Math.Max(1, e.LignesPalette);
            if (palette + lignes - 1 > DernierePalette)
                throw new InvalidOperationException($"{e.Nom} : plus de ligne de palette libre");

            e.TexX = PageX;
            e.TexY = s_Bandes[b].Y;
            e.Palette = palette;
            e.Entree = PremiereEntree + 4 * k++;
            palette += lignes;
        }
    }

    /// <summary>
    /// Le maillage relogé pour la place de ce programme : sa pagination porte notre bande de V et notre
    /// entrée de décodage (les octets 0 et 3 d'origine gardés). Relogé à nouveau, depuis l'archive,
    /// seulement si la place a changé depuis la dernière fois.
    /// </summary>
    private static void ChargerModele(Espece e)
    {
        int bande = e.TexY & 0xc0;
        if (e.Modele != null && e.ModelePour == (bande, e.Entree)) return;

        byte* meta = stackalloc byte[8];
        *(uint*)(meta + 4) = (e.Pagination & 0xff0000ffu) | ((uint)bande << 8) | ((uint)(e.Entree >> 2) << 16);

        byte* paquet = Charger(e.EntreeModele);
        byte* parties;
        Jeu.RelocateModelBundle((int*)paquet, meta, &parties);
        if (e.Paquet != null) NativeMemory.Free(e.Paquet);
        e.Paquet = paquet;
        e.Modele = parties;
        e.ModelePour = (bande, e.Entree);

        Journal($"{e.Nom} : maillage relogé pour la bande 0x{bande:x2}, entrée +0x{e.Entree:x} (pagination 0x{*(uint*)(meta + 4):x8})");
    }

    /// <summary>La texture de l'espèce à sa place, et l'entrée de décodage qui la désigne — à chaque entrée.</summary>
    private static void EnvoyerTexture(Espece e)
    {
        byte* tampon = Charger(e.EntreeTexture);
        try
        {
            Resources.TimImage tim;
            Winsys3d.OpenTim(tampon);
            Winsys3d.ReadTim(&tim);

            short* palette = tim.ClutRect;
            palette[0] = 0;
            palette[1] = (short)e.Palette;
            Winsys3d.Ordinal_674(palette, tim.ClutData);

            short* image = tim.PixelRect;
            image[0] = (short)e.TexX;
            image[1] = (short)e.TexY;
            Winsys3d.Ordinal_674(image, tim.PixelData);

            // La page qui contient la bande, comme DecodeResourceChunk la calcule — la bande de V
            // passe par la pagination du maillage, pas par la page.
            int pageY = e.TexY & 0x100;
            int tpage = ((e.TexX >> 2) & 0xf0) | pageY;
            tpage = (short)(tpage | 0x2000) >> 4;
            tpage |= ((int)tim.Mode & 3) << 7;
            byte* table = Jeu.Mem.s_EntityDecodeTable_005e8f00 + e.Entree;
            *(ushort*)table = (ushort)tpage;
            *(ushort*)(table + 2) = (ushort)(e.Palette << 6);
        }
        finally { NativeMemory.Free(tampon); }
    }

    /// <summary>
    /// Avant la naissance d'une vague (LP-014, design D2) : les espèces greffées de CETTE vague seules
    /// prennent les cinq bandes de VRAM — leur texture et leur palette envoyées, leur entrée de
    /// décodage écrite, leur maillage relogé si sa place a changé et remis dans la table des maillages.
    /// Les espèces des vagues passées perdent leur place : leurs créatures ont disparu. Faux si la
    /// vague ne peut pas être installée (le directeur abandonne alors le programme).
    /// </summary>
    internal static bool PreparerVague(AreneDirecteur.Vague vague)
    {
        var especes = vague.Creatures.Select(c => c.Greffe).OfType<Espece>().Distinct().ToList();
        if (especes.Count == 0) return true;
        if (!s_Pret || especes.Any(e => !s_Greffees.Contains(e))) return false;

        try
        {
            Placer(especes);
            foreach (Espece e in especes)
            {
                ChargerModele(e);
                EnvoyerTexture(e);
                ((byte**)Jeu.Mem.Arena(0x005e96a0))[e.Maillage] = e.Modele;
                Journal($"{e.Nom} : texture en ({e.TexX}, {e.TexY}), palette ligne {e.Palette}, entrée +0x{e.Entree:x}, maillage {e.Maillage}");
            }

            int restes = s_Lignees.Sum(pool => pool.Count(l => l != null && !especes.Contains(l)));
            if (restes > 0) Journal($"vague : {restes} entité(s) d'une vague passée encore vivante(s) à la bascule de VRAM");
            return true;
        }
        catch (Exception ex)
        {
            Journal($"vague impossible à installer ({ex.GetType().Name} : {ex.Message})");
            return false;
        }
    }

    /// <summary>
    /// L'espèce au rang <paramref name="rang"/> de 4/9 : sa fiche, son IA, ses animations, ses sons,
    /// et le rang de son maillage — que <see cref="PreparerVague"/> remplit, sa place connue.
    /// </summary>
    private static void Installer(Espece e, byte* repertoire, int rang, int maillage)
    {
        e.Rang = rang;
        e.Maillage = maillage;

        int creneau = PremierCreneauIa;
        uint[] ia = Jeu.Res.s_Vt_0056ad48;
        while (creneau < ia.Length && ia[creneau] != CorpsVide && ia[creneau] != e.Ia) creneau++;
        if (creneau >= ia.Length) throw new InvalidOperationException("aucun créneau d'IA libre dans 0x0056ad48");
        ia[creneau] = e.Ia;

        byte* ligne = s_TableEspeces + rang * 8;
        NativeMemory.Copy(e.Ligne, ligne, 8);
        ligne[0] = (byte)maillage;
        ligne[1] = (byte)creneau;
        ligne[4] = (byte)rang;
        ligne[5] = 0;                            // le butin : jamais lu, ButinNe l'éteint
        NativeMemory.Copy(e.Fiche, s_TableFiches + rang * 0x60, 0x60);

        ((byte**)Jeu.Mem.s_AnimationTable_005e9640)[maillage] = e.Animations;

        foreach (int id in e.SonsDeNiveau) GrefferSon(e.Source, id);

        Journal($"{e.Nom} : rang {rang}, maillage {maillage}, IA 0x{e.Ia:x8} au créneau {creneau}");
    }


    /// <summary>
    /// Un son de niveau de la sous-zone d'origine, ajouté au bout du banc de 4/9 sous le premier id
    /// libre (longueur nulle dans le répertoire des sons). Le banc est lu au moment de jouer
    /// (<c>Audio.cs</c>, <c>PlaySoundEffect</c>) : un son peut donc être greffé à sa première demande.
    /// </summary>
    private static void GrefferSon(Source s, int id)
    {
        if (s_Sons.ContainsKey((s, id)) || s.Sons == null || id < 60 || id > FinSons) return;
        int k = id - 60;
        int debut = BitConverter.ToInt32(s.Sons, k * 8), longueur = BitConverter.ToInt32(s.Sons, k * 8 + 4);
        if (longueur <= 0) return;

        var repertoire = Jeu.Mem.s_SoundDirectory_005e8340;
        int fin = 0, libre = -1;
        for (int i = 60; i <= FinSons; i++)
        {
            fin = Math.Max(fin, repertoire[i].Offset + repertoire[i].Length);
            if (libre < 0 && i >= PremierSon && repertoire[i].Length == 0 && !s_Sons.ContainsValue(i)) libre = i;
        }
        if (libre < 0 || fin + longueur > TailleBanc) { Journal($"son {id} de {s.Zone}/{s.SousZone} : pas de place, coupé"); return; }

        Marshal.Copy(s.Sons, 0x88 + debut, (nint)((byte*)Jeu.Mem.s_LevelSoundBank_00600900 + fin), longueur);
        repertoire[libre].Offset = fin;
        repertoire[libre].Length = longueur;
        s_Sons[(s, id)] = libre;
        Journal($"son {id} (0x{id:x2}) de {s.Zone}/{s.SousZone} : {longueur} octets, joué sous l'id {libre} (0x{libre:x2})");
    }

    // ---- la lignée et le contexte (8.1.2) --------------------------------------------------------
    //
    // Un gestionnaire lit « sa » sous-zone par le répertoire courant (0x005e97cc) : les catalogues
    // d'effets +0xcc, +0xd0 et +0xd4 (le sous-type d'un enfant vaut 0xc0 + un de leurs octets) et la
    // table de sons +0xa8. Et un effet de sous-type >= 0xc0 trouve son gestionnaire dans les tables
    // que la sous-zone installe (0x0056b0c8, b1c8, b2c8). Dans l'arène, c'est 4/9 qui répond : le
    // souffle du Méchant ours y serait l'octet 9 du catalogue de la tour (0), donc rien.
    //
    // D'où le CONTEXTE : le temps qu'une entité de la LIGNÉE d'une créature greffée exécute son
    // gestionnaire (AvantGestionnaire → ApresGestionnaire), les quatre champs du répertoire de 4/9
    // désignent ceux de la sous-zone d'origine, et un indice >= 0xc0 se résout dans ses tables
    // (GestionnaireInstalle, Resources.SubAreaEffectHandler). La LIGNÉE : une créature greffée de la
    // table de scène (rang d'espèce >= 16), et tout effet né pendant qu'une entité de la lignée
    // exécutait son gestionnaire — relevé en comparant les emplacements vivants des trois pools
    // d'effets avant et après l'appel. Un emplacement mort perd sa lignée à l'image suivante.
    //
    // Les effets d'une espèce sans EffetsGreffes restent coupés à la naissance (lot 1) ; tout le
    // reste de la lignée est résolu dans sa sous-zone d'origine.

    private static readonly (int Base, int Places)[] s_PoolsEffets = [(0x005ff400, 0x20), (0x005edee0, 0x80), (0x005f2d40, 0x80)];
    private static readonly Espece?[][] s_Lignees = [new Espece?[0x20], new Espece?[0x80], new Espece?[0x80]];
    private static readonly bool[][] s_VivantsAvant = [new bool[0x20], new bool[0x80], new bool[0x80]];
    private static readonly int[] s_ChampsContexte = [0xa8, 0xcc, 0xd0, 0xd4];
    private static readonly nint[] s_ChampsSauves = new nint[4];
    private static Espece? s_Contexte;
    private static int s_Imbrique;                   // appels de gestionnaire imbriqués dans un contexte ouvert

    private static void InstallerContexte()
    {
        Coutures.Gestionnaire avant = Coutures.AvantGestionnaire;
        Coutures.AvantGestionnaire = e => { avant(e); Avant(e); };
        Coutures.Gestionnaire apres = Coutures.ApresGestionnaire;
        Coutures.ApresGestionnaire = e => { Apres(); apres(e); };

        Coutures.Resolution resolution = Coutures.GestionnaireInstalle;
        Coutures.GestionnaireInstalle = (e, table, indice, g) =>
        {
            g = resolution(e, table, indice, g);
            if (s_Contexte == null || indice < 0xc0) return g;
            Source s = s_Contexte.Source;
            uint origine = Jeu.Res.SubAreaEffectHandler(s.Zone == 0 ? 0 : s.Zone * 0x10 + s.SousZone, table, indice);
            string note = $"effet 0x{indice:x2} de la table 0x{table:x8} : 0x{origine:x8} de {s.Zone}/{s.SousZone} (0x{g:x8} dans 4/9)";
            if (s_Coupes.Add(note)) Journal($"{s_Contexte.Nom} : {note}");
            return origine;
        };

        Action image = Coutures.ImageSuivante;
        Coutures.ImageSuivante = () => { image(); OublierLesMorts(); };
    }

    private static void ViderLignees()
    {
        foreach (Espece?[] l in s_Lignees) Array.Clear(l);
        s_Contexte = null;
        s_Imbrique = 0;
    }

    private static void Avant(byte* e)
    {
        if (s_Contexte != null) { s_Imbrique++; return; }   // le contexte ouvert reste celui de l'appel extérieur
        if (!Arene.Actif || !s_Pret || s_Greffees.Count == 0) return;
        Espece? espece = Lignee(e);
        if (espece?.Source.Repertoire == null) return;

        byte* rep = Jeu.s_Unknown_005e97cc, source = espece.Source.Repertoire;
        for (int k = 0; k < s_ChampsContexte.Length; k++)
        {
            s_ChampsSauves[k] = *(nint*)(rep + s_ChampsContexte[k]);
            *(nint*)(rep + s_ChampsContexte[k]) = *(nint*)(source + s_ChampsContexte[k]);
        }
        for (int p = 0; p < s_PoolsEffets.Length; p++)
            for (int i = 0; i < s_PoolsEffets[p].Places; i++) s_VivantsAvant[p][i] = Vivant(p, i);
        s_Contexte = espece;
    }

    private static void Apres()
    {
        if (s_Contexte == null) return;
        if (s_Imbrique > 0) { s_Imbrique--; return; }
        byte* rep = Jeu.s_Unknown_005e97cc;
        for (int k = 0; k < s_ChampsContexte.Length; k++) *(nint*)(rep + s_ChampsContexte[k]) = s_ChampsSauves[k];
        for (int p = 0; p < s_PoolsEffets.Length; p++)
            for (int i = 0; i < s_PoolsEffets[p].Places; i++)
                if (!s_VivantsAvant[p][i] && Vivant(p, i)) s_Lignees[p][i] = s_Contexte;
        s_Contexte = null;
    }

    private static void OublierLesMorts()
    {
        for (int p = 0; p < s_PoolsEffets.Length; p++)
            for (int i = 0; i < s_PoolsEffets[p].Places; i++)
                if (s_Lignees[p][i] != null && !Vivant(p, i)) s_Lignees[p][i] = null;
    }

    private static bool Vivant(int pool, int i) =>
        (*(uint*)(Jeu.Mem.Entity(s_PoolsEffets[pool].Base) + i * TailleEntite) & 0x80000000) != 0;

    /// <summary>L'espèce greffée dont l'entité descend : elle-même si c'est une créature greffée de la
    /// table de scène, sa lignée relevée si c'est un effet ; sinon null.</summary>
    private static Espece? Lignee(byte* e)
    {
        if (!Arene.Actif || !s_Pret || s_Greffees.Count == 0 || e == null) return null;
        if (Greffee(e))
        {
            int k = e[0x0c] - PremierRang;
            return k >= 0 && k < s_Greffees.Count ? s_Greffees[k] : null;
        }
        for (int p = 0; p < s_PoolsEffets.Length; p++)
        {
            long ecart = e - Jeu.Mem.Entity(s_PoolsEffets[p].Base);
            if (ecart >= 0 && ecart < s_PoolsEffets[p].Places * TailleEntite && ecart % TailleEntite == 0)
                return s_Lignees[p][ecart / TailleEntite];
        }
        return null;
    }

    // ---- la traduction ---------------------------------------------------------------------------

    /// <summary>Une entité de la table de scène, vivante, d'une espèce greffée.</summary>
    private static bool Greffee(byte* entite)
    {
        if (!Arene.Actif || !s_Pret || s_Greffees.Count == 0 || entite == null) return false;
        long ecart = entite - Jeu.Mem.Entity(PoolScene);
        if (ecart < 0 || ecart >= PlacesScene * TailleEntite || ecart % TailleEntite != 0) return false;
        return entite[0x0c] >= PremierRang;             // l'octet de l'espèce ; +0x0d est le genre
    }

    /// <summary>
    /// Un son de niveau joué par la lignée d'une créature greffée : l'id de la sous-zone d'origine,
    /// greffé au banc de 4/9 à sa première demande ; coupé s'il n'y a pas la place. Deux coutures,
    /// une seule traduction : PENDANT le gestionnaire de la lignée (le contexte), c'est SonJoue, à
    /// l'entrée de PlaySoundEffect, que prennent aussi les sons non positionnels (le Loup-garou et le
    /// Tigre jouent +0xa8[0x2c] ainsi) ; HORS contexte, c'est SonPositionnel, par la position de la
    /// créature greffée. PlayPositionalSound finissant par PlaySoundEffect, l'une laisse passer ce
    /// que l'autre traduit.
    /// </summary>
    private static int Son(int id, int* position)
    {
        if (s_Contexte != null) return id;              // SonJoue le traduira
        return Traduire(id, Lignee((byte*)position - 0x1c));
    }

    private static int SonJoue(int id) => s_Contexte == null ? id : Traduire(id, s_Contexte);

    private static int Traduire(int id, Espece? espece)
    {
        if (id < 60 || id > FinSons || espece == null) return id;
        GrefferSon(espece.Source, id);
        if (s_Sons.TryGetValue((espece.Source, id), out int greffe)) return greffe;
        Couper($"son de niveau {id} (0x{id:x2})");
        return SonCoupe;
    }

    /// <summary>Un effet par sous-zone d'une espèce qui n'a pas ses effets greffés : coupé (lot 1).</summary>
    private static bool Effet(byte* source, byte sousType)
    {
        if (sousType < 0xc0) return false;
        Espece? espece = s_Contexte ?? Lignee(source);
        if (espece == null || espece.EffetsGreffes) return false;
        Couper($"effet de sous-type 0x{sousType:x2} (table par sous-zone)");
        return true;
    }

    private static void Couper(string quoi)
    {
        if (s_Coupes.Add(quoi)) Journal($"{quoi} d'une créature greffée : coupé");
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[GREFFE] {message}");
}
