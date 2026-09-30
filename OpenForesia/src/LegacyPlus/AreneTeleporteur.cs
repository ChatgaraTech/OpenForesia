// =====================================================================
// LP-012 — le téléporteur du hub : son dessin, et le menu de l'arène à son approche.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-arena, design D5 et D8). Le
// téléporteur se tient en (−766, 1235) dans le hub (relevé de Vincent, 2026-09-28). Un membre debout
// qui entre dans son rayon ouvre le menu (sans question préalable depuis le 2026-09-30, Vincent) ;
// il ne se rouvre qu'une fois le membre ressorti du rayon (de quelques unités de plus : pas de
// menu en boucle au bord). Le menu est tenu par CE membre seul :
//
//   1. le mode — un programme de l'arène (AreneDirecteur.Programmes) ;
//   2. le palier — Normal, Difficile, Très difficile, Impossible : PV × 1 à 4, dégâts reçus × 1 à 2,5,
//      bourse × 1 à 4 (AreneDirecteur.Paliers), décrit sous la liste ;
//   3. la confirmation, par la question de la page 2.
//
// Menu revient d'une page, puis ferme.
//
// LE DESSIN — une greffe de décor (design D8). Le téléporteur du Labyrinthe est en 5/9, section 2
// (relevé et captures de Vincent, `Tests/11. Teleporteur`) : au centre d'une salle à quatre piliers
// lumineux, le décor de rang 6 (`OpenForesia.Tools decor-modele 5 9 2 6`) — genre 2, le MODÈLE 6 du
// paquet de la liste +0x24, descripteur `01 03 01 20` : entrée de texture 1 (une page 4 bits entière),
// semi-transparent (0x200) ; en (−2500, 55, −100), sans rotation, paramètre de dessin 0x000c (la
// palette 12 de la ligne). Premier essai sur le rang 2 : c'était un des quatre piliers (le modèle 1),
// et sa lueur (une particule) sortait en « bloc lumineux » (Vincent, 2026-09-28).
//
// On relit le répertoire de 5/9 dans l'archive (une fois), on reloge le paquet de modèles comme
// LoadWorldMapSubArea, on envoie la page dans la VRAM libre du hub (relevé LP-010) et on écrit une
// entrée de la table 0x005e8f00 à nous, que le mot +0x3c du modèle désigne :
//
//   page entière en (960, 256) — à droite des marchands     palette ligne 276     entrée 0xe0
//
// Les réceptions de l'auberge dessinent le même modèle (LP-009, le téléporteur vers le hub), avec la
// même page, la même palette et la même entrée : libres dans le relevé VRAM de la réception 0.
//
// La hauteur suit celle de 5/9 au-dessus du sol : 55 de plus que les piliers, posés au sol (400).
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Globalization;
using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;
using Resources = OpenForesia.Blaze.Resources;
using Winsys3d = OpenForesia.Blaze.Winsys3d;

namespace OpenForesia.LegacyPlus;

internal static unsafe class AreneTeleporteur
{
    /// <summary>La place du téléporteur dans le hub, en unités de formations.csv (relevé de Vincent).</summary>
    internal const int X = -766, Z = 1235;

    /// <summary>Le rayon du déclenchement, et celui qu'il faut repasser pour qu'il se réarme.</summary>
    private const int Rayon = 80, RayonSortie = Rayon + 30;

    /// <summary>Par membre (0 à 3) : déjà déclenché, et pas encore ressorti du rayon.</summary>
    private static readonly bool[] s_Questionne = new bool[4];

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;
        EcranMenu.Installer();

        Coutures.Pas pas = Coutures.PasDuMembre;
        Coutures.PasDuMembre = (membre, pad, perChar, descripteur) =>
        {
            pas(membre, pad, perChar, descripteur);
            if (EstLeHub()) Approcher(membre, perChar, descripteur);
        };

        Coutures.Liste liste = Coutures.ListeDApparition;
        Coutures.ListeDApparition = (indice, l) =>
        {
            uint* r = liste(indice, l);
            if (indice != 0xff && EstLeHub()) PreparerGreffe();
            return r;
        };

        Coutures.Dessin dessin = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = dessin(otBase, curseur);
            return EstLeHub() && s_Pret ? Dessiner(otBase, curseur, s_Placement) : curseur;
        };

        Console.Error.WriteLine($"[ARENE] téléporteur en ({X}, {Z}), {AreneDirecteur.Programmes.Length} programme(s)");
    }

    private static bool EstLeHub() =>
        (Jeu.Res.s_Unknown_005de528 & 0xff) == 0 && Jeu.s_Unknown_00566c88 == HubAuberge.Hub;

    // ---- la greffe du décor ------------------------------------------------------------------------

    private const int ZoneSource = 5, SousZoneSource = 9;
    private const int IndiceModele = 6, TextureModele = 1;
    private const uint ParametreModele = 0x000c;              // mot +0x1e de l'enregistrement : la palette 12
    private const int Sol = 400;                              // le sol du hub

    /// <summary>La hauteur du téléporteur au-dessus du sol, comme en 5/9 (aussi dans les réceptions, LP-009).</summary>
    internal const int Hauteur = 55;
    private const int AvanceOt = 4;                           // emplacements d'ordering table, vers la caméra

    private const int PageX = 960, PageY = 256, Palette = 276, Entree = 0xe0;

    private static byte* s_SousZone;       // le répertoire de 5/9, relu une fois
    private static byte* s_Modele;         // le modèle 6 du paquet, relogé chez nous
    private static int* s_Placement;       // son bloc de placement
    private static bool s_Pret;

    /// <summary>
    /// À chaque peuplement du hub — et d'une réception, dont le téléporteur mène au hub (LP-009) : le
    /// répertoire et le modèle une fois, la texture chaque fois. Vrai si le modèle peut être dessiné.
    /// </summary>
    internal static bool PreparerGreffe()
    {
        try
        {
            if (s_SousZone == null && !Charger()) return false;
            EnvoyerTexture();
            s_Pret = true;
        }
        catch (Exception ex)
        {
            s_Pret = false;
            Console.Error.WriteLine($"[ARENE] téléporteur impossible à greffer ({ex.GetType().Name} : {ex.Message}) — pas de dessin");
        }
        return s_Pret;
    }

    /// <summary>Un bloc de placement du modèle en (x, y, z), unités de formations.csv, sans rotation.</summary>
    internal static int* NouveauPlacement(int x, int y, int z)
    {
        int* placement = (int*)NativeMemory.AllocZeroed(0x20);
        placement[0] = x << 12;
        placement[1] = y << 12;
        placement[2] = z << 12;
        ((ushort*)placement)[6] = 0x1000;                        // +0x0c..0x10 : échelles
        ((ushort*)placement)[7] = 0x1000;
        ((ushort*)placement)[8] = 0x1000;
        placement[7] = 0xffffff;                                 // +0x1c : la couleur
        return placement;
    }

    private static byte* Lire(int* entree)
    {
        byte* tampon = (byte*)NativeMemory.AllocZeroed((nuint)(entree[1] + 16));
        Jeu.Res.LoadResourceEntry(entree, tampon);
        return tampon;
    }

    /// <summary>Le répertoire de 5/9 (état 3 → zone 5 → sous-zone 9), et le modèle relogé.</summary>
    private static bool Charger()
    {
        byte* etat = Jeu.Mem.s_MainResourceDirectory_005e97c4;
        if (etat == null) return false;

        byte* zone = Lire((int*)(nint)((int*)*(byte**)(etat + 0x48))[ZoneSource]);
        Jeu.Res.FixupResourceHeader(zone);
        byte* sousZone = Lire((int*)(nint)((int*)*(byte**)(zone + 0x48))[SousZoneSource]);
        Jeu.Res.FixupResourceHeader(sousZone);

        // Le paquet de modèles de décor : le dernier de la liste +0x24 (ici le seul), relogé comme le
        // fait LoadWorldMapSubArea, avec une pagination nulle — le descripteur du modèle garde son
        // octet 2 (l'entrée 1 de 5/9), que l'on remplace ensuite par la nôtre.
        int* paquets = *(int**)(sousZone + 0x24);
        int* dernier = paquets;
        for (int* e = paquets; *e != 0; e += 2) dernier = e;
        byte* paquet = Lire((int*)(nint)(*dernier));
        byte* pagination = (byte*)NativeMemory.AllocZeroed(8);
        byte* modeles;
        int n = Jeu.RelocateModelBundle((int*)paquet, pagination, &modeles);
        if (n <= IndiceModele) throw new InvalidOperationException($"le paquet de 5/9 n'a que {n} modèle(s)");
        s_Modele = ((byte**)modeles)[IndiceModele];
        s_Modele[0x3e] = Entree / 4;

        s_Placement = NouveauPlacement(X, Sol + Hauteur, Z);

        s_SousZone = sousZone;
        Console.Error.WriteLine($"[ARENE] téléporteur : répertoire de {ZoneSource}/{SousZoneSource} relu, {n} modèles, modèle {IndiceModele} 0x{*(uint*)(s_Modele + 0x3c):x8}");
        return true;
    }

    /// <summary>La page dans la VRAM libre du hub, et l'entrée de table qui la désigne.</summary>
    private static void EnvoyerTexture()
    {
        int* textures = *(int**)(s_SousZone + 0x10);
        byte* tim = Lire((int*)(nint)textures[TextureModele * 2]);
        try
        {
            Resources.TimImage t;
            Winsys3d.OpenTim(tim);
            Winsys3d.ReadTim(&t);

            // La palette sur une ligne de 256, comme DecodeResourceChunk la remet en forme.
            if (t.ClutData != null)
            {
                short* palette = t.ClutRect;
                int octets = (palette[3] * palette[2] + 0xff) & ~0xff;
                palette[0] = 0;
                palette[1] = Palette;
                palette[2] = 0x100;
                palette[3] = (short)(octets >> 8);
                Winsys3d.Ordinal_674(palette, t.ClutData);
            }

            short* image = t.PixelRect;
            image[0] = PageX;
            image[1] = PageY;
            Winsys3d.Ordinal_674(image, t.PixelData);

            // L'entrée : la page, calculée comme DecodeResourceChunk, et la palette.
            int tpage = ((PageX >> 2) & 0xf0) | (PageY & 0x100);
            tpage = (short)(tpage | 0x2000) >> 4;
            tpage |= ((int)t.Mode & 3) << 7;
            byte* table = Jeu.Mem.s_EntityDecodeTable_005e8f00 + Entree;
            *(ushort*)table = (ushort)tpage;
            *(ushort*)(table + 2) = (ushort)(Palette << 6);
        }
        finally { NativeMemory.Free(tim); }
    }

    /// <summary>
    /// Le modèle, avec la garde <c>0x00564a4c</c> à 1 comme la passe du décor la laisse pour un modèle
    /// (<c>WorldMapHud.cs</c>, ~1415), et quelques emplacements plus près de la caméra : ajouté à la liste
    /// d'affichage APRÈS le décor, il passerait sinon sous le sol à profondeur égale (le dernier entré
    /// dans un emplacement est dessiné le premier).
    /// </summary>
    internal static uint* Dessiner(uint* otBase, uint* curseur, int* placement)
    {
        int avant = Jeu.Mem.s_Unknown_00564a4c;
        Jeu.Mem.s_Unknown_00564a4c = 1;
        curseur = Jeu.Mdl.DrawDepthSortedModel(otBase - AvanceOt, curseur, s_Modele, placement, ParametreModele);
        Jeu.Mem.s_Unknown_00564a4c = avant;
        return curseur;
    }

    // ---- l'approche -------------------------------------------------------------------------------

    /// <summary>
    /// Un membre du hub qui entre dans le rayon du téléporteur : le menu de l'arène, directement.
    /// Jusqu'au 2026-09-29, une question le précédait (« Ce teleporteur semble dangereux, on y va ? ») ;
    /// retirée à la demande de Vincent (2026-09-30).
    /// </summary>
    private static void Approcher(uint* membre, byte* perChar, byte* descripteur)
    {
        if (!EntreDansLeRayon(membre, descripteur, X, Z, s_Questionne)) return;
        Console.Error.WriteLine($"[ARENE] J{(((byte*)membre)[8] & 3) + 1} approche du téléporteur");
        EcranMenu.Ouvrir(membre, perChar, PageModes());
    }

    /// <summary>
    /// Un membre debout, sans boîte ouverte, qui entre dans le rayon d'un téléporteur en (x, z) : vrai
    /// une seule fois, jusqu'à ce qu'il ressorte du rayon (<paramref name="questionne"/>, un drapeau
    /// par membre). La même forme que les marchands (membre debout, aucune boîte), sans bouton à
    /// presser. Sert aussi au téléporteur des réceptions (LP-009).
    /// </summary>
    internal static bool EntreDansLeRayon(uint* membre, byte* descripteur, int x, int z, bool[] questionne)
    {
        byte* p = (byte*)membre;
        int rang = p[8] & 3;
        int dx = x - (*(int*)(p + 0x1c) >> 12), dz = z - (*(int*)(p + 0x24) >> 12);
        int d2 = dx * dx + dz * dz;

        if (d2 > RayonSortie * RayonSortie) { questionne[rang] = false; return false; }
        if (questionne[rang] || d2 > Rayon * Rayon) return false;
        if (EcranMenu.Ouvert) return false;
        if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return false;

        // Un membre tenu par un joueur seulement : 0x10000 est le compagnon que le jeu pilote
        // (PrepareLeaderAndPartyFlags, WorldMapHud.cs, ne compte comme jouable que celui qui ne le
        // porte pas). Sans cette garde, le compagnon qui suivait le joueur dans le rayon rouvrait la
        // question pour lui-même, sur une manette qui n'existe pas : plus rien ne répondait après un
        // Non (Vincent, 2026-09-28).
        if ((*membre & 0x10000) != 0) return false;
        if ((descripteur[0x8c7] & 1) != 0) return false;

        questionne[rang] = true;
        return true;
    }

    // ---- les pages -------------------------------------------------------------------------------

    private static EcranMenu.Page PageModes() => new()
    {
        Titre = "LE TELEPORTEUR - ARENE",
        // Les boss rush (LP-014) sur une page à eux : le premier l'ouvre.
        Lister = () => AreneDirecteur.Programmes
            .Select((p, i) => new EcranMenu.Ligne(p.Nom, "", p,
                NouvellePage: p.Challenge && (i == 0 || !AreneDirecteur.Programmes[i - 1].Challenge)))
            .ToList(),
        SiVide = "Aucun combat n'est pret.",
        Valider = l => new EcranMenu.Suite(Suivante: PagePaliers((AreneDirecteur.Programme)l.Donnee!)),
    };

    private static EcranMenu.Page PagePaliers(AreneDirecteur.Programme programme) => new()
    {
        Titre = $"ARENE - {programme.Nom}",
        Lister = () => AreneDirecteur.Paliers
            .Select(p => new EcranMenu.Ligne(p.Nom, $"PV x{p.Pv}", p)).ToList(),
        Description = l =>
        {
            var p = (AreneDirecteur.Palier)l.Donnee!;
            string degats = (p.Degats / 100.0).ToString("0.##", CultureInfo.GetCultureInfo("fr-FR"));
            return $"PV des monstres : x{p.Pv}\n" +
                   $"Degats des monstres : x{degats}\n" +
                   $"Pieces d'or : {programme.Pieces * p.Pv}";
        },
        Question = l => $"{((AreneDirecteur.Palier)l.Donnee!).Nom} : on y va ?",
        Valider = l =>
        {
            AreneDirecteur.Choisir(programme, (AreneDirecteur.Palier)l.Donnee!);
            return new EcranMenu.Suite(Fermer: true);
        },
    };
}
