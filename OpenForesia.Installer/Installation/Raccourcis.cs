// Les raccourcis Windows (.lnk), par l'interface COM IShellLinkW du shell — la seule maniere
// documentee d'en ecrire un, et celle qu'emploient tous les installeurs.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace OpenForesia.Installer.Installation;

internal static class Raccourcis
{
    [ComImport, Guid("00021401-0000-0000-C000-000000000046")]
    private class ShellLink;

    // L'ordre des methodes est celui de la vtable de IShellLinkW : il ne se change pas.
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F9-0000-0000-C000-000000000046")]
    private interface IShellLinkW
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder fichier, int taille, IntPtr donnees, int drapeaux);
        void GetIDList(out IntPtr liste);
        void SetIDList(IntPtr liste);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder nom, int taille);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string nom);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder dossier, int taille);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string dossier);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder arguments, int taille);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string arguments);
        void GetHotkey(out short touche);
        void SetHotkey(short touche);
        void GetShowCmd(out int commande);
        void SetShowCmd(int commande);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder icone, int taille, out int index);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string icone, int index);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string chemin, int reserve);
        void Resolve(IntPtr fenetre, int drapeaux);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string fichier);
    }

    internal static void Creer(string raccourci, string cible, string arguments, string dossierDeTravail,
        string description, string icone)
    {
        var lien = (IShellLinkW)new ShellLink();
        try
        {
            lien.SetPath(cible);
            lien.SetArguments(arguments);
            lien.SetWorkingDirectory(dossierDeTravail);
            lien.SetDescription(description);
            lien.SetIconLocation(icone, 0);
            ((IPersistFile)lien).Save(raccourci, true);
        }
        finally
        {
            Marshal.FinalReleaseComObject(lien);
        }
    }
}
