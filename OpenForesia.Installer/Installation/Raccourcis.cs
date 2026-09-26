// Les raccourcis Windows (.lnk), par l'interface COM IShellLinkW du shell — la seule maniere
// documentee d'en ecrire un, et celle qu'emploient tous les installeurs. Les interfaces sont dans
// Interface/Com.cs, sous leur forme generee a la compilation (compatible avec l'elagage).

using OpenForesia.Installer.Interface;

namespace OpenForesia.Installer.Installation;

internal static class Raccourcis
{
    internal static void Creer(string raccourci, string cible, string arguments, string dossierDeTravail,
        string description, string icone)
    {
        IShellLinkW lien = Com.CreerRaccourci();
        lien.SetPath(cible);
        lien.SetArguments(arguments);
        lien.SetWorkingDirectory(dossierDeTravail);
        lien.SetDescription(description);
        lien.SetIconLocation(icone, 0);
        ((IPersistFile)lien).Save(raccourci, 1);
    }
}
