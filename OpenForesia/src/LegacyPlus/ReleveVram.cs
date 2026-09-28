// =====================================================================
// Diagnostic — le relevé de la VRAM émulée, à l'arrivée dans chaque carte.
//
// Hors reconstruction : un outil de mise au point, pas un tweak. Activé par
// `[Diagnostic] ReleveVram = oui`, version Debug seulement (Coutures.cs). Écrit pour LP-010 (les
// marchands du hub) : où poser les textures de quatre personnages qui ne sont pas dans le groupe.
//
// Une seconde après le chargement d'une sous-zone (60 images dessinées), il écrit à côté de l'exe :
//   - `legacyplus-vram-z<zone>-s<sous-zone>.bmp` : les 1 024 × 512 mots de la VRAM, lus comme des
//     couleurs 15 bits — une texture 4 ou 8 bits y paraît en bruit, mais sa place se voit ;
//   - `legacyplus-vram-z<zone>-s<sous-zone>.txt` : la carte des cases de 16 × 16 pixels (`.` = case
//     entièrement à zéro, `1`..`9` = remplissage, `#` = pleine), avec les pages de texture de
//     64 × 256 en repère, puis la table des pages et palettes que les modèles désignent
//     (`0x005e8f00`, quatre octets par entrée : page de texture, palette).
//
// Une case à zéro n'est qu'un CANDIDAT : une image noire y ressemble. Le relevé ne change rien au jeu.
// =====================================================================

using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class ReleveVram
{
    private const int Largeur = 1024, Hauteur = 512, Case = 16;
    private const int ImagesAttente = 60;

    private static int s_Cle = -1;          // zone << 8 | sous-zone déjà relevée
    private static int s_Attente;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.ReleveVram) return;

        Coutures.Dessin precedent = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = precedent(otBase, curseur);
            Observer();
            return curseur;
        };
        Console.Error.WriteLine("[VRAM] relevé actif : une image et une carte de la VRAM à l'arrivée dans chaque carte");
    }

    private static void Observer()
    {
        int zone = Jeu.Res.s_Unknown_005de528 & 0xff, sousZone = Jeu.s_Unknown_00566c88;
        int cle = zone << 8 | sousZone;
        if (cle != s_Cle) { s_Cle = cle; s_Attente = ImagesAttente; return; }
        if (s_Attente <= 0 || --s_Attente > 0) return;

        try { Ecrire(zone, sousZone); }
        catch (Exception ex) { Console.Error.WriteLine($"[VRAM] relevé impossible ({ex.Message})"); }
    }

    private static void Ecrire(int zone, int sousZone)
    {
        byte* vram = Libraries.Winsys3dData.Vram_00b6dbd0;
        if (vram == null) return;
        ushort* px = (ushort*)vram;

        string nom = Path.Combine(AppContext.BaseDirectory, $"legacyplus-vram-z{zone}-s{sousZone}");
        EcrireBmp(nom + ".bmp", px);

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# VRAM, zone {zone}, sous-zone {sousZone} — cases de {Case}×{Case} pixels ; '.' = tout à zéro, 1..9 = remplissage, '#' = plein");
        sb.AppendLine("# colonnes : x / 16 ; un '|' toutes les 4 cases = une page de texture de 64 pixels ; ligne '---' tous les 256 (deuxième rangée de pages)");
        sb.Append("      ");
        for (int cx = 0; cx < Largeur / Case; cx++) sb.Append(cx % 4 == 0 ? (char)('0' + cx / 4 % 10) : ' ');
        sb.AppendLine();
        for (int cy = 0; cy < Hauteur / Case; cy++)
        {
            if (cy == Hauteur / Case / 2) sb.AppendLine(new string('-', 6 + Largeur / Case));
            sb.Append($"y{cy * Case,4} ");
            for (int cx = 0; cx < Largeur / Case; cx++)
            {
                int plein = 0;
                for (int y = 0; y < Case; y++)
                    for (int x = 0; x < Case; x++)
                        if (px[(cy * Case + y) * Largeur + cx * Case + x] != 0) plein++;
                sb.Append(plein == 0 ? '.' : plein == Case * Case ? '#' : (char)('1' + Math.Min(8, plein * 9 / (Case * Case))));
            }
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.AppendLine("# table 0x005e8f00 : entrée, page de texture (x, y, mode), palette (x, y) ; 0x80..0x8c et 0x90..0x9c = les quatre membres du groupe");
        byte* table = Jeu.Mem.s_EntityDecodeTable_005e8f00;
        for (int i = 0; i < 0x100; i += 4)
        {
            ushort tpage = *(ushort*)(table + i), clut = *(ushort*)(table + i + 2);
            if (tpage == 0 && clut == 0) continue;
            string modes = ((tpage >> 7) & 3) switch { 0 => "4 bits", 1 => "8 bits", _ => "15 bits" };
            sb.AppendLine($"+0x{i:x3} : page 0x{tpage:x4} = ({(tpage & 0xf) * 64}, {((tpage >> 4) & 1) * 256}) {modes} ; palette 0x{clut:x4} = ({(clut & 0x3f) * 16}, {clut >> 6})");
        }

        File.WriteAllText(nom + ".txt", sb.ToString());
        Console.Error.WriteLine($"[VRAM] relevé écrit : {nom}.bmp et .txt");
    }

    /// <summary>Un BMP 24 bits, de bas en haut ; chaque mot lu comme une couleur 15 bits de la PlayStation.</summary>
    private static void EcrireBmp(string chemin, ushort* px)
    {
        int ligne = Largeur * 3, taille = 54 + ligne * Hauteur;
        using var f = new BinaryWriter(File.Create(chemin));
        f.Write((byte)'B'); f.Write((byte)'M'); f.Write(taille); f.Write(0); f.Write(54);
        f.Write(40); f.Write(Largeur); f.Write(Hauteur); f.Write((short)1); f.Write((short)24);
        f.Write(0); f.Write(ligne * Hauteur); f.Write(2835); f.Write(2835); f.Write(0); f.Write(0);
        var rangee = new byte[ligne];
        for (int y = Hauteur - 1; y >= 0; y--)
        {
            for (int x = 0; x < Largeur; x++)
            {
                ushort v = px[y * Largeur + x];
                rangee[x * 3] = (byte)(((v >> 10) & 31) << 3);
                rangee[x * 3 + 1] = (byte)(((v >> 5) & 31) << 3);
                rangee[x * 3 + 2] = (byte)((v & 31) << 3);
            }
            f.Write(rangee);
        }
    }
}
