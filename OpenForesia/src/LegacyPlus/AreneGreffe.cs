// =====================================================================
// LP-013 — la greffe : un monstre d'une autre sous-zone, dans l'arène, comme chez lui.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-boss-graft, design G1 à G4).
// Un monstre n'existe que dans sa sous-zone : son espèce, sa fiche, son maillage, ses animations, sa
// texture, ses sons de niveau et la table qui donne son IA sont chargés avec elle. La greffe les fait
// venir dans 4/9, à chaque entrée dans l'arène d'un programme qui en a besoin :
//
//   - le RÉPERTOIRE de la sous-zone d'origine est relu dans l'archive, une fois (état 3 → zone →
//     sous-zone, comme LoadWorldMapArea et LoadWorldMapSubArea) ;
//   - le MAILLAGE (+0x2c) et les ANIMATIONS (+0x7c) de l'espèce sont chargés et relogés dans une
//     mémoire à nous, une fois ; leur pagination désigne notre entrée de la table de décodage
//     0x005e8f00 et notre bande de VRAM ; ils prennent les rangs 3 et suivants des tables
//     0x005e96a0 / 0x005e9640, que 4/9 (trois maillages) laisse libres ;
//   - la TEXTURE (+0x10, l'entrée que la pagination d'origine désigne) est envoyée en VRAM à la place
//     relevée libre (legacyplus-vram-z4-s9.txt), et l'entrée de décodage écrite ;
//   - l'ESPÈCE et la FICHE : les tables +0x4c et +0x98 du répertoire de 4/9 sont recopiées dans une
//     mémoire à nous, avec les espèces greffées au rang 16 et suivants ; le répertoire est repointé
//     sur elles jusqu'au prochain chargement ;
//   - l'IA : l'adresse du gestionnaire d'origine, dans un créneau libre de 0x0056ad48 ;
//   - les SONS DE NIVEAU (ids 60 à 76, banc Snd\Qzz_ss.snd) que l'espèce joue : leurs échantillons
//     ajoutés au bout du banc de 4/9, sous des ids que 4/9 n'emploie pas.
//
// La traduction (G3), par deux coutures, pour les seules créatures greffées :
//   - SonPositionnel : un son de niveau traduit vers son id greffé ; sans traduction, coupé ;
//   - EffetCoupe : un effet de sous-type >= 0xc0 (table par sous-zone) coupé — lot 1 : le souffle
//     du Méchant ours (0xc0 + octet 9 du premier bloc de +0xcc) n'est pas encore greffé. Sous 0xc0,
//     la table est statique, la même partout : l'éclat d'impact 0x39 passe tel quel.
//   - ButinNe : ni objet ni pièces (Vincent, 2026-09-28 : la récompense de l'arène, ce sont les
//     coffres) ; les éclats d'expérience restent.
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
    /// table 0x0056ad48 de sa sous-zone, relevée par <c>OpenForesia.Tools greffe</c>), les sons de
    /// niveau qu'elle joue, et sa place en VRAM : sa texture en (<see cref="TexX"/>,
    /// <see cref="TexY"/>), sa palette sur la ligne <see cref="Palette"/>, l'entrée
    /// <see cref="Entree"/> de 0x005e8f00.
    /// </summary>
    internal sealed class Espece
    {
        public required string Nom;
        public required Source Source;
        public required int Indice;
        public required uint Ia;
        public required int[] SonsDeNiveau;
        public required int TexX, TexY, Palette, Entree;

        // Ce que le chargement donne, une fois pour toutes.
        public byte* Ligne;                     // les 8 octets de l'espèce d'origine
        public byte* Fiche;                     // ses 0x60 octets de fiche
        public byte* Modele;                    // le tableau de parties (RelocateModelBundle)
        public byte* Animations;                // le tableau de clips (RelocateAnimationBundle)
        public int* EntreeTexture;              // l'entrée d'archive de sa texture

        // Ce que chaque entrée dans l'arène donne.
        public int Rang;                        // son rang dans la table d'espèces de 4/9
    }

    // ---- les monstres greffés (mesures : legacy.md, LP-013) ------------------------------------

    private static readonly Source s_LieuDeCoupe = new(3, 5);

    /// <summary>
    /// Le Méchant ours de 3/5, espèce 0 : maillage 0, IA 16 = 0x004A4AE0, fiche 0 (300 PV, rang 38).
    /// Sa texture, l'entrée 5 de 3/5 (60 × 48 mots, 8 bits), va en (128, 128) : la page (128, 0),
    /// bande de V 0x80.
    /// </summary>
    internal static readonly Espece MechantOurs = new()
    {
        Nom = "Méchant ours", Source = s_LieuDeCoupe, Indice = 0, Ia = 0x004A4AE0, SonsDeNiveau = [0x3f],
        TexX = 128, TexY = 128, Palette = 288, Entree = 0xe8,
    };

    /// <summary>
    /// Le Grizzly de 3/5, espèce 4 : maillage 2, IA 4 = 0x004A4270, fiche 3 (80 PV, rang 25). Sa
    /// texture, l'entrée 7 de 3/5 (128 × 64 mots, 8 bits), va en (128, 64) : bande de V 0x40.
    /// </summary>
    internal static readonly Espece Grizzly = new()
    {
        Nom = "Grizzly", Source = s_LieuDeCoupe, Indice = 4, Ia = 0x004A4270, SonsDeNiveau = [],
        TexX = 128, TexY = 64, Palette = 289, Entree = 0xec,
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
    private static readonly Dictionary<int, int> s_Sons = new();   // id d'origine → id greffé
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

        Coutures.Effet effet = Coutures.EffetCoupe;
        Coutures.EffetCoupe = (source, sousType) => effet(source, sousType) || Effet(source, sousType);

        Coutures.Butin butin = Coutures.ButinNe;
        Coutures.ButinNe = (entite, b) =>
        {
            butin(entite, b);
            if (Greffee(entite)) *(short*)(b + 0x10) = 1;    // éteint à la passe suivante, avant d'être ramassable
        };
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

            int k = 0;
            foreach (Espece e in s_Greffees)
            {
                if (e.Source.Repertoire == null) LireRepertoire(e.Source);
                if (e.Modele == null) Charger(e);
                EnvoyerTexture(e);
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
    /// L'espèce, sa fiche, son maillage relogé sous notre pagination, ses animations, et l'entrée de
    /// sa texture — tout ce qui ne change pas d'une entrée dans l'arène à l'autre.
    /// </summary>
    private static void Charger(Espece e)
    {
        byte* rep = e.Source.Repertoire;
        byte* ligne = *(byte**)(rep + 0x4c) + e.Indice * 8;
        e.Ligne = (byte*)NativeMemory.Alloc(8);
        NativeMemory.Copy(ligne, e.Ligne, 8);
        e.Fiche = (byte*)NativeMemory.Alloc(0x60);
        NativeMemory.Copy(*(byte**)(rep + 0x98) + ligne[4] * 0x60, e.Fiche, 0x60);

        int* modele = *(int**)(rep + 0x2c) + ligne[0] * 2;
        uint pagination = (uint)modele[1];
        int entreeOrigine = (int)(pagination >> 16) & 0xff;
        e.EntreeTexture = (int*)(nint)(*(int**)(rep + 0x10))[entreeOrigine * 2];

        // La pagination : la bande de V de notre place (AdjustModelTexturePages ajoute (p >> 8) & 0xc0
        // aux V), et notre entrée de décodage ; les octets 0 et 3 d'origine gardés.
        uint bande = (uint)(e.TexY & 0xc0);
        byte* meta = (byte*)NativeMemory.AllocZeroed(8);
        *(uint*)(meta + 4) = (pagination & 0xff0000ffu) | (bande << 8) | ((uint)(e.Entree >> 2) << 16);

        byte* paquet = Charger((int*)(nint)modele[0]);
        byte* parties;
        Jeu.RelocateModelBundle((int*)paquet, meta, &parties);
        e.Modele = parties;

        byte* animations = Charger((int*)(nint)(*(int**)(rep + 0x7c))[ligne[0]]);
        byte* clips;
        Jeu.RelocateAnimationBundle((int*)animations, &clips);
        e.Animations = clips;

        Journal($"{e.Nom} ({e.Source.Zone}/{e.Source.SousZone}, espèce {e.Indice}) : maillage {ligne[0]}, fiche {ligne[4]}, "
                + $"texture {entreeOrigine}, pagination 0x{pagination:x8} → 0x{*(uint*)(meta + 4):x8}");
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

    /// <summary>L'espèce au rang <paramref name="rang"/> de 4/9, son maillage et ses animations, son IA, ses sons.</summary>
    private static void Installer(Espece e, byte* repertoire, int rang, int maillage)
    {
        e.Rang = rang;

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

        ((byte**)Jeu.Mem.Arena(0x005e96a0))[maillage] = e.Modele;
        ((byte**)Jeu.Mem.s_AnimationTable_005e9640)[maillage] = e.Animations;

        foreach (int id in e.SonsDeNiveau) GrefferSon(e.Source, id);

        Journal($"{e.Nom} : rang {rang}, maillage {maillage}, IA 0x{e.Ia:x8} au créneau {creneau}, "
                + $"texture en ({e.TexX}, {e.TexY}), palette ligne {e.Palette}, entrée +0x{e.Entree:x}");
    }

    /// <summary>
    /// Un son de niveau de la sous-zone d'origine, ajouté au bout du banc de 4/9 sous le premier id
    /// libre (longueur nulle dans le répertoire des sons).
    /// </summary>
    private static void GrefferSon(Source s, int id)
    {
        if (s_Sons.ContainsKey(id) || s.Sons == null) return;
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
        s_Sons[id] = libre;
        Journal($"son {id} (0x{id:x2}) de {s.Zone}/{s.SousZone} : {longueur} octets, joué sous l'id {libre} (0x{libre:x2})");
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

    private static int Son(int id, int* position)
    {
        if (id < 60 || !Greffee((byte*)position - 0x1c)) return id;
        if (s_Sons.TryGetValue(id, out int greffe)) return greffe;
        Couper($"son de niveau {id} (0x{id:x2})");
        return SonCoupe;
    }

    private static bool Effet(byte* source, byte sousType)
    {
        if (sousType < 0xc0 || !Greffee(source)) return false;
        Couper($"effet de sous-type 0x{sousType:x2} (table par sous-zone)");
        return true;
    }

    private static void Couper(string quoi)
    {
        if (s_Coupes.Add(quoi)) Journal($"{quoi} d'une créature greffée : coupé");
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[GREFFE] {message}");
}
