// GENERE par `OpenForesia.Tools extraire-donnees` depuis Legacy/Binaries — NE PAS EDITER A LA MAIN.
// Voir OpenForesia.Tools/ExtractionDonnees.cs. Chaque valeur a ete reencodee et comparee octet par
// octet a la DLL de 1998 lors de la generation.
//
// Le modele du dialogue de choix du pilote (ressource RT_DIALOG 0x66 de `winsys3d.dll`), un
// `DLGTEMPLATE` decrit controle par controle. `Winsys3dImage.ModeleDialogue` l'assemble.

namespace OpenForesia.Libraries;

internal static class Winsys3dModeleDialogue
{
    internal static byte[] Construire() => Winsys3dImage.ModeleDialogue(
        style: 0x80c008c1, styleEtendu: 0x00000000, x: 0, y: 0, largeur: 323, hauteur: 247,
        titre: "System Startup", taillePolice: 8, police: "MS Sans Serif",
        Winsys3dImage.Controle(Winsys3dImage.ClasseListe, id: 0x3e8, "", x: 5, y: 173, largeur: 146, hauteur: 45, style: 0x50a10101, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseListe, id: 0x3eb, "", x: 185, y: 173, largeur: 129, hauteur: 45, style: 0x50a10101, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseBouton, id: 0x1, "OK", x: 86, y: 226, largeur: 50, hauteur: 14, style: 0x50010001, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseBouton, id: 0x2, "&Cancel", x: 187, y: 226, largeur: 50, hauteur: 14, style: 0x50010000, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseLibelle, id: 0x3e9, "Select Renderer", x: 8, y: 160, largeur: 130, hauteur: 9, style: 0x50020000, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseLibelle, id: 0x3ea, "Select Resolution", x: 188, y: 160, largeur: 130, hauteur: 9, style: 0x50020000, styleEtendu: 0x00000000),
        Winsys3dImage.Controle(Winsys3dImage.ClasseBouton, id: 0x3ec, "Button2", x: 62, y: 5, largeur: 200, hauteur: 150, style: 0x5000000b, styleEtendu: 0x00000000));
}
