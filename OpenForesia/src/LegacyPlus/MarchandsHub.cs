// =====================================================================
// LP-010 — les marchands du hub : des PNJ au repos, sous le modèle d'une classe jouable.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-28-add-legacy-plus-hub-merchants). Le Lutin a validé
// le chargement, la texture et la profondeur ; les trois autres suivent le même chemin. Les boutiques
// viennent au lot suivant.
//
// Le jeu ne charge que les classes du groupe (LoadPartyMemberResources, FUN_00536ba0, pendant
// l'état 1). Le tweak relit donc le répertoire de l'ÉTAT 1 dans l'archive et y prend, pour chaque
// marchand, les ressources que le jeu prendrait pour un membre (design D2, mesure dans legacy.md) :
//   - le paquet de modèle (+0x2c) et celui d'animations (+0x7c), dans sa propre mémoire, relogés par
//     les fonctions du jeu — une fois pour toutes ;
//   - la texture 1 (+0x84) en VRAM, à chaque entrée dans le hub : en (832, 256 + 64·k), la bande que
//     le relevé a trouvée vide, ses palettes en (0, 256 + 4·k) ; l'entrée 0xcc + 4·k de 0x005e8f00
//     porte la page et la palette. La pagination du modèle vaut 0x40·k (le décalage V que le jeu
//     applique au membre k) et désigne cette entrée.
// Le dessin passe par la couture DessinSurImpression, comme pour un membre du groupe
// (DrawAnimatedModel, clip 0 = le repos). Aucune entité, aucun gestionnaire du jeu.
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;
using Resources = OpenForesia.Blaze.Resources;
using Winsys3d = OpenForesia.Blaze.Winsys3d;

namespace OpenForesia.LegacyPlus;

internal static unsafe class MarchandsHub
{
    private sealed class Marchand
    {
        public required string Nom;
        public required int Identite;           // genre << 3 | classe
        public required short X, Z, Cap;
        public int* Modele;                     // tableau de parties (RelocateModelBundle)
        public byte** Animations;               // tableau de clips (RelocateAnimationBundle)
        public int* Transformee;                // le bloc que DrawAnimatedModel lit, comme +0x1c d'une entité
        public int Image;

        // La boutique (LP-010) : le fichier, le titre de l'écran, la catégorie vendue (octet +0x13 de
        // la fiche : 0x01 arme, 0x02 armure, 0x04 bouclier, 0x08 accessoire ; 0 = objet utilisable),
        // et le filtre de classe de l'acheteur (les quatre boutiques, demande de Vincent).
        public required string Fichier, Titre;
        public required byte Categorie;
        public required bool FiltreClasse;
        public ReglagesBoutique? Boutique;
        public List<(string Nom, int Prix)>? Vente;   // le catalogue du fichier, filtré par catégorie
        public bool Rachat;                            // LP-011 : la Voleuse rachète au lieu de vendre

        // La place en VRAM (legacy.md, LP-010) : la texture 1 en (TexX, 256 + 64·Rang), la page de
        // TexX en y 256, le décalage V de 64·Rang porté par la pagination du modèle ; les palettes en
        // (0, PalY) ; l'entrée Entree de 0x005e8f00.
        public required int TexX, Rang, PalY, Entree;
    }

    // Les quatre marchands, le long du mur ouest, à 150 unités l'un de l'autre à partir de la place
    // relevée par Vincent pour le Lutin. Le rang k donne la place de la texture en VRAM et
    // l'entrée de table : ne pas réordonner sans le savoir.
    private static readonly Marchand[] s_Marchands =
    [
        new() { Nom = "Lutin", Identite = 7, X = -1301, Z = 1060, Cap = 1024, TexX = 832, Rang = 0, PalY = 256, Entree = 0xcc,
                Fichier = "BoutiqueObjets.ini", Titre = "LE LUTIN - OBJETS", Categorie = 0, FiltreClasse = true },   // les 7 herbes : Chasseur seulement
        new() { Nom = "Voleur", Identite = 2, X = -1301, Z = 1210, Cap = 1024, TexX = 832, Rang = 1, PalY = 260, Entree = 0xd0,
                Fichier = "BoutiqueAccessoires.ini", Titre = "LE VOLEUR - ACCESSOIRES", Categorie = 0x08, FiltreClasse = true },   // 33 des 119 sont réservés (Vincent, 2026-09-28)
        new() { Nom = "Kobold", Identite = 6, X = -1301, Z = 1360, Cap = 1024, TexX = 832, Rang = 2, PalY = 264, Entree = 0xd4,
                Fichier = "BoutiqueArmures.ini", Titre = "LE KOBOLD - ARMURES", Categorie = 0x06, FiltreClasse = true },
        new() { Nom = "Sylphide", Identite = 13, X = -1301, Z = 1510, Cap = 1024, TexX = 832, Rang = 3, PalY = 268, Entree = 0xd8,   // l'Elfe au féminin
                Fichier = "BoutiqueArmes.ini", Titre = "LA SYLPHIDE - ARMES", Categorie = 0x01, FiltreClasse = true },
        // LP-011 : la Voleuse (le Voleur au féminin), devant le mur du fond, tournée vers la porte
        // (cap 2048 : un quart de tour à droite des autres, demande de Vincent). La bande x 832 est
        // pleine : sa texture va dans l'autre zone vide du relevé, x 0..255, y 272..447.
        new() { Nom = "Voleuse", Identite = 10, X = -1028, Z = 1618, Cap = 2048, TexX = 0, Rang = 1, PalY = 272, Entree = 0xdc,
                Fichier = "BoutiqueVente.ini", Titre = "LA VOLEUSE - RACHAT", Categorie = 0, FiltreClasse = false, Rachat = true },
    ];

    private const short Sol = 400;              // la hauteur des pieds dans la 82, repère des relevés
    private const int Clip = 0;                 // le repos (relevé en jeu)
    private const int TextureY = 256;           // la rangée de pages des textures de marchand

    private static byte* s_Etat1;               // le répertoire de l'état 1, relu une fois
    private static byte* s_Tampon;              // la texture le temps de l'envoi

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.Hub) return;

        // Le hub actif, les cinq marchands sont là (Vincent, 2026-09-28 : plus de clé Actif par boutique).
        foreach (Marchand m in s_Marchands) m.Boutique = ReglagesBoutique.Lire(m.Fichier);

        BoutiqueEcran.Installer();

        Coutures.Liste liste = Coutures.ListeDApparition;
        Coutures.ListeDApparition = (indice, l) =>
        {
            uint* r = liste(indice, l);
            if (indice != 0xff && EstLeHub()) Preparer();
            return r;
        };

        Coutures.Pas pas = Coutures.PasDuMembre;
        Coutures.PasDuMembre = (membre, pad, perChar, descripteur) =>
        {
            pas(membre, pad, perChar, descripteur);
            if (!EstLeHub()) return;
            Repousser(membre);
            Parler(membre, pad, perChar, descripteur);
        };

        Coutures.Dessin dessin = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = dessin(otBase, curseur);
            return EstLeHub() ? Dessiner(otBase, curseur) : curseur;
        };
    }

    private static bool EstLeHub() =>
        (Jeu.Res.s_Unknown_005de528 & 0xff) == 0 && Jeu.s_Unknown_00566c88 == HubAuberge.Hub;

    // ---- le chargement ---------------------------------------------------------------------------

    /// <summary>À chaque peuplement du hub : les paquets une fois, la texture et l'entrée de table à chaque fois.</summary>
    private static void Preparer()
    {
        try
        {
            if (s_Etat1 == null && !LireEtat1()) return;
            for (int k = 0; k < s_Marchands.Length; k++)
            {
                Marchand m = s_Marchands[k];
                if (m.Modele == null) ChargerPaquets(m);
                EnvoyerTexture(m);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[MARCHANDS] chargement impossible ({ex.GetType().Name} : {ex.Message}) — pas de marchand");
            foreach (Marchand m in s_Marchands) m.Modele = null;
        }
    }

    /// <summary>Le répertoire de l'état 1, relu dans l'archive par l'entrée que la racine lui donne.</summary>
    private static bool LireEtat1()
    {
        byte* racine = Jeu.Mem.s_MainResourceBuffer_005e97c0;
        if (racine == null) return false;
        int* entree = (int*)(nint)(*(int**)(racine + 0x48))[1];
        s_Etat1 = (byte*)NativeMemory.AllocZeroed((nuint)(entree[1] + 16));
        Jeu.Res.LoadResourceEntry(entree, s_Etat1);
        Jeu.Res.FixupResourceHeader(s_Etat1);
        Journal($"répertoire de l'état 1 relu ({entree[1]} octets)");
        return true;
    }

    private static byte* Charger(int* entree)
    {
        byte* tampon = (byte*)NativeMemory.AllocZeroed((nuint)(entree[1] + 16));
        Jeu.Res.LoadResourceEntry(entree, tampon);
        return tampon;
    }

    /// <summary>Le modèle et les animations de la classe, relogés comme pour un membre k.</summary>
    private static void ChargerPaquets(Marchand m)
    {
        int* modele = (int*)(*(int*)(s_Etat1 + 0x2c) + m.Identite * 8);
        uint pagination = (uint)modele[1];
        uint pb1 = (uint)(m.Rang * 0x40), pb2 = (uint)(m.Entree >> 2);
        byte* meta = (byte*)NativeMemory.AllocZeroed(8);
        *(uint*)(meta + 4) = (pagination & 0xff0000ffu) | (pb1 << 8) | (pb2 << 16);

        byte* paquet = Charger((int*)(nint)modele[0]);
        byte* parties;
        Jeu.RelocateModelBundle((int*)paquet, meta, &parties);
        m.Modele = (int*)parties;

        byte* animations = Charger((int*)(nint)(*(int**)(s_Etat1 + 0x7c))[m.Identite]);
        byte* clips;
        Jeu.RelocateAnimationBundle((int*)animations, &clips);
        m.Animations = (byte**)clips;

        m.Transformee = (int*)NativeMemory.AllocZeroed(0x20);
        m.Transformee[0] = m.X << 12;
        m.Transformee[1] = Sol << 12;
        m.Transformee[2] = m.Z << 12;
        *(uint*)(m.Transformee + 3) = 0x10001000;                 // échelle x, y
        *(ushort*)(m.Transformee + 4) = 0x1000;                   // échelle z
        *(short*)((byte*)m.Transformee + 0x16) = m.Cap;           // le cap, comme +0x32 d'une entité
        // Comme +0x36 d'un membre (PlaceWorldMapParty le met à 1) : le bit 1 fait calculer par
        // DrawAnimatedModel la profondeur des parties posées (drapeau 0x400) ; à 0, elles tombaient
        // toutes dans l'emplacement 0 de l'ordering table, devant tout (premier essai de Vincent).
        *(short*)((byte*)m.Transformee + 0x1a) = 1;
        m.Transformee[7] = 0xffffff;                              // la couleur, comme +0x38

        Journal($"{m.Nom} (identité {m.Identite}) : modèle et animations chargés, pagination 0x{*(uint*)(meta + 4):x8}");
    }

    /// <summary>La texture 1 de la classe à la place du marchand k, et l'entrée de table qui la désigne.</summary>
    private static void EnvoyerTexture(Marchand m)
    {
        int* texture = (int*)(*(int*)(s_Etat1 + 0x84) + m.Identite * 8);
        int* entree = (int*)(nint)texture[0];
        if (s_Tampon != null) NativeMemory.Free(s_Tampon);
        s_Tampon = Charger(entree);

        Resources.TimImage tim;
        Winsys3d.OpenTim(s_Tampon);
        Winsys3d.ReadTim(&tim);

        short* palette = tim.ClutRect;
        palette[0] = 0;
        palette[1] = (short)m.PalY;
        Winsys3d.Ordinal_674(palette, tim.ClutData);

        short* image = tim.PixelRect;
        image[0] = (short)m.TexX;
        image[1] = (short)(TextureY + 64 * m.Rang);
        Winsys3d.Ordinal_674(image, tim.PixelData);

        // La page de la bande (x 832, y 256), comme DecodeResourceChunk la calcule — le décalage de
        // 64·k lignes passe par la pagination du modèle, pas par la page.
        int tpage = ((m.TexX >> 2) & 0xf0) | (TextureY & 0x100);
        tpage = (short)(tpage | 0x2000) >> 4;
        tpage |= ((int)tim.Mode & 3) << 7;
        byte* table = Jeu.Mem.s_EntityDecodeTable_005e8f00 + m.Entree;
        *(ushort*)table = (ushort)tpage;
        *(ushort*)(table + 2) = (ushort)((m.PalY << 6) | 0);

        Journal($"{m.Nom} : texture {image[2]}×{image[3]} en ({image[0]}, {image[1]}), palette {palette[2]}×{palette[3]} en (0, {palette[1]}), " +
                $"page 0x{tpage:x4}, entrée +0x{m.Entree:x}");
    }

    // ---- la boutique -----------------------------------------------------------------------------

    private const int PorteeParole = Rayon + 50;        // unités, du centre du marchand
    private const int DemiAngle = 0x200;                // 45°, en 4096e de tour

    /// <summary>
    /// Face à un marchand, bouton d'action : son écran de boutique (design D4). Même forme que la porte
    /// du hub et que l'armoire de LP-008 : membre debout, à portée, tourné vers le marchand à 45° près,
    /// aucune boîte ouverte, appui consommé.
    /// </summary>
    private static void Parler(uint* membre, byte* pad, byte* perChar, byte* descripteur)
    {
        if (BoutiqueEcran.Ouvert) return;
        ushort action = OpenForesia.Blaze.Input.CombatMask(*(ushort*)(perChar + 0x1fc4));
        if ((*(ushort*)(pad + 2) & action) == 0) return;
        if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return;
        if ((descripteur[0x8c7] & 1) != 0) return;

        byte* p = (byte*)membre;
        int cap = *(short*)(p + 0x32) & 0xfff;
        foreach (Marchand m in s_Marchands)
        {
            if (m.Modele == null) continue;
            int dx = m.X - (*(int*)(p + 0x1c) >> 12), dz = m.Z - (*(int*)(p + 0x24) >> 12);
            if (dx * dx + dz * dz > PorteeParole * PorteeParole) continue;
            int vers = (int)Math.Round(Math.Atan2(dx, dz) * 4096 / (2 * Math.PI)) & 0xfff;
            int ecart = (vers - cap) & 0xfff;
            if (ecart > 0x800) ecart = 0x1000 - ecart;
            if (ecart > DemiAngle) continue;

            *(ushort*)(pad + 2) &= (ushort)~action;
            if (m.Rachat)
            {
                var prix = PrixDAchat();
                int taux = m.Boutique!.Taux;
                Journal($"J{p[8] + 1} parle à la {m.Nom} (rachat à {taux} %)");
                BoutiqueEcran.OuvrirVente(membre, perChar, m.Titre,
                    () => BoutiqueVente.Lister(perChar, prix, taux),
                    ligne => BoutiqueVente.Vendre(membre, perChar, ligne));
                return;
            }

            int classe = perChar[0x150] & 7;
            var vente = Vente(m).Where(o => !m.FiltreClasse || PeutPorter(o.Nom, classe)).ToList();
            Journal($"J{p[8] + 1} (classe {classe}) parle au {m.Nom} : {vente.Count} objets sur {m.Vente!.Count}");
            BoutiqueEcran.Ouvrir(membre, perChar, m.Titre, vente);
            return;
        }
    }

    /// <summary>
    /// Le catalogue d'un marchand, filtré par catégorie (design D6), calculé à la première
    /// ouverture. L'index des objets est construit une fois, pour les noms des quatre boutiques
    /// qui vendent : il relit les fichiers du jeu, et c'est la seule lecture lente.
    /// </summary>
    private static List<(string Nom, int Prix)> Vente(Marchand m)
    {
        if (m.Vente != null) return m.Vente;
        if (!CatalogueObjets.Construit)
        {
            var noms = s_Marchands.Where(x => !x.Rachat).SelectMany(x => x.Boutique!.Catalogue.Select(o => o.Nom));
            CatalogueObjets.Construire(noms, Console.Error);
        }

        m.Vente = new();
        foreach (var o in m.Boutique!.Catalogue)
        {
            var objet = CatalogueObjets.Trouver(o.Nom);
            if (objet == null) { Journal($"{m.Fichier} : « {o.Nom} » introuvable dans les données du jeu, écarté"); continue; }
            byte genre = (byte)(objet.Fiche[0x13] & 0x0f);
            bool vendu = m.Categorie == 0 ? genre == 0 : (genre & m.Categorie) != 0;
            if (vendu) m.Vente.Add(o);
            else Journal($"{m.Fichier} : « {o.Nom} » n'est pas de cette catégorie (octet +0x13 = 0x{objet.Fiche[0x13]:x2}), écarté");
        }
        return m.Vente;
    }

    private static Dictionary<string, int>? s_PrixDAchat;

    /// <summary>
    /// Le prix d'achat de chaque objet dans l'une des quatre boutiques (c'est le
    /// fichier qui fait foi) — la base du rachat (LP-011). Un objet absent n'est pas racheté.
    /// </summary>
    private static Dictionary<string, int> PrixDAchat()
    {
        if (s_PrixDAchat != null) return s_PrixDAchat;
        s_PrixDAchat = new();
        foreach (Marchand m in s_Marchands)
        {
            if (m.Rachat || m.Boutique == null) continue;
            foreach (var (nom, prix) in m.Boutique.Catalogue) s_PrixDAchat.TryAdd(nom, prix);
        }
        return s_PrixDAchat;
    }

    /// <summary>La fiche dit quelles classes peuvent porter l'objet : l'octet +0x12, un bit par classe.</summary>
    private static bool PeutPorter(string nom, int classe)
    {
        var objet = CatalogueObjets.Trouver(nom);
        return objet != null && (objet.Fiche[0x12] & (1 << classe)) != 0;
    }

    // ---- la collision ----------------------------------------------------------------------------

    /// <summary>Le rayon d'un marchand, en unités : un membre n'entre pas dans ce cercle.</summary>
    private const int Rayon = 48;

    /// <summary>
    /// Un marchand n'est pas une entité du jeu : le moteur ne le heurte pas. À l'entrée du pas de
    /// chaque membre, un membre entré dans le cercle d'un marchand est ramené à son bord, sur la
    /// droite qui les joint — il glisse autour. La hauteur n'est pas touchée : le sol du hub est plat.
    /// </summary>
    private static void Repousser(uint* membre)
    {
        byte* m = (byte*)membre;
        foreach (Marchand marchand in s_Marchands)
        {
            if (marchand.Modele == null) continue;
            long dx = *(int*)(m + 0x1c) - (marchand.X << 12);
            long dz = *(int*)(m + 0x24) - (marchand.Z << 12);
            long r = (long)Rayon << 12;
            long d2 = dx * dx + dz * dz;
            if (d2 >= r * r) continue;

            double d = Math.Sqrt(d2);
            if (d < 1) { dx = r; dz = 0; d = r; }                  // pile au centre : vers +x
            *(int*)(m + 0x1c) = (marchand.X << 12) + (int)(dx * r / d);
            *(int*)(m + 0x24) = (marchand.Z << 12) + (int)(dz * r / d);
        }
    }

    // ---- le dessin -------------------------------------------------------------------------------

    private static uint* Dessiner(uint* otBase, uint* curseur)
    {
        foreach (Marchand m in s_Marchands)
        {
            if (m.Modele == null) continue;
            byte* clip = m.Animations[Clip];
            int images = *(ushort*)(clip + 0x16);
            if (images > 0) m.Image = (m.Image + 1) % images;
            curseur = Jeu.Mdl.DrawAnimatedModel(otBase, curseur, clip, m.Modele, m.Transformee, m.Image, 0);
        }
        return curseur;
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[MARCHANDS] {message}");
}
