// Les interfaces COM du shell dont l'installeur a besoin : le selecteur de fichier et de dossier
// (IFileOpenDialog, celui qu'emploient WinForms et l'Explorateur depuis Vista) et les raccourcis
// (IShellLinkW + IPersistFile).
//
// Sous leur forme GENEREE a la compilation ([GeneratedComInterface]) : l'installeur est publie
// elague, et l'interoperabilite COM classique ([ComImport]) ne survit pas a l'elagage. L'ordre des
// methodes est celui des vtables Windows : il ne se change pas, meme pour une methode inutilisee.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace OpenForesia.Installer.Interface;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
internal partial interface IShellItem
{
    void BindToHandler(IntPtr contexte, IntPtr gestionnaire, IntPtr interfaceVoulue, out IntPtr resultat);
    void GetParent(out IntPtr parent);
    void GetDisplayName(uint forme, out IntPtr nom);
    void GetAttributes(uint masque, out uint attributs);
    void Compare(IntPtr autre, uint critere, out int ordre);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802")]
internal partial interface IModalWindow
{
    [PreserveSig] int Show(IntPtr proprietaire);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
internal partial interface IFileDialog : IModalWindow
{
    void SetFileTypes(uint nombre, IntPtr filtres);
    void SetFileTypeIndex(uint index);
    void GetFileTypeIndex(out uint index);
    void Advise(IntPtr evenements, out uint cookie);
    void Unadvise(uint cookie);
    void SetOptions(uint options);
    void GetOptions(out uint options);
    void SetDefaultFolder(IntPtr dossier);
    void SetFolder(IntPtr dossier);
    void GetFolder(out IntPtr dossier);
    void GetCurrentSelection(out IntPtr element);
    void SetFileName(string nom);
    void GetFileName(out IntPtr nom);
    void SetTitle(string titre);
    void SetOkButtonLabel(string texte);
    void SetFileNameLabel(string texte);
    void GetResult(out IShellItem element);
    void AddPlace(IntPtr element, int position);
    void SetDefaultExtension(string extension);
    void Close(int resultat);
    void SetClientGuid(IntPtr guid);
    void ClearClientData();
    void SetFilter(IntPtr filtre);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
internal partial interface IFileOpenDialog : IFileDialog
{
    void GetResults(out IntPtr elements);
    void GetSelectedItems(out IntPtr elements);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("000214F9-0000-0000-C000-000000000046")]
internal partial interface IShellLinkW
{
    void GetPath(IntPtr fichier, int taille, IntPtr donnees, uint drapeaux);
    void GetIDList(out IntPtr liste);
    void SetIDList(IntPtr liste);
    void GetDescription(IntPtr nom, int taille);
    void SetDescription(string nom);
    void GetWorkingDirectory(IntPtr dossier, int taille);
    void SetWorkingDirectory(string dossier);
    void GetArguments(IntPtr arguments, int taille);
    void SetArguments(string arguments);
    void GetHotkey(out ushort touche);
    void SetHotkey(ushort touche);
    void GetShowCmd(out int commande);
    void SetShowCmd(int commande);
    void GetIconLocation(IntPtr icone, int taille, out int index);
    void SetIconLocation(string icone, int index);
    void SetRelativePath(string chemin, uint reserve);
    void Resolve(IntPtr fenetre, uint drapeaux);
    void SetPath(string fichier);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("0000010c-0000-0000-C000-000000000046")]
internal partial interface IPersist
{
    void GetClassID(out Guid classe);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16), Guid("0000010b-0000-0000-C000-000000000046")]
internal partial interface IPersistFile : IPersist
{
    [PreserveSig] int IsDirty();
    void Load(string fichier, uint mode);
    void Save(string fichier, int retenir);
    void SaveCompleted(string fichier);
    void GetCurFile(out IntPtr fichier);
}

internal static unsafe partial class Com
{
    private static readonly Guid ClsidFileOpenDialog = new("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7");
    private static readonly Guid ClsidShellLink = new("00021401-0000-0000-C000-000000000046");
    private const uint ClsctxInprocServer = 1;

    private const uint FosPickFolders = 0x20, FosForceFileSystem = 0x40, FosPathMustExist = 0x800,
                       FosFileMustExist = 0x1000;
    private const uint SigdnFileSysPath = 0x80058000;
    private const int ErreurAnnule = unchecked((int)0x800704C7);

    [LibraryImport("ole32.dll")]
    private static partial int CoCreateInstance(Guid* classe, IntPtr externe, uint contexte, Guid* interfaceVoulue, out IntPtr objet);

    private static readonly StrategyBasedComWrappers s_Enveloppes = new();

    private static T Creer<T>(Guid classe, Guid interfaceVoulue)
    {
        int hr = CoCreateInstance(&classe, IntPtr.Zero, ClsctxInprocServer, &interfaceVoulue, out IntPtr objet);
        Marshal.ThrowExceptionForHR(hr);
        try { return (T)s_Enveloppes.GetOrCreateObjectForComInstance(objet, CreateObjectFlags.UniqueInstance); }
        finally { Marshal.Release(objet); }
    }

    internal static IShellLinkW CreerRaccourci() =>
        Creer<IShellLinkW>(ClsidShellLink, typeof(IShellLinkW).GUID);

    /// <summary>Le selecteur de fichier. Null si le joueur annule.</summary>
    internal static string? ChoisirFichier(IntPtr proprietaire, string titre, (string Nom, string Motif)[] filtres) =>
        Choisir(proprietaire, titre, FosFileMustExist | FosPathMustExist | FosForceFileSystem, filtres);

    /// <summary>Le selecteur de dossier. Null si le joueur annule.</summary>
    internal static string? ChoisirDossier(IntPtr proprietaire, string titre) =>
        Choisir(proprietaire, titre, FosPickFolders | FosPathMustExist | FosForceFileSystem, []);

    private static string? Choisir(IntPtr proprietaire, string titre, uint options, (string Nom, string Motif)[] filtres)
    {
        IFileOpenDialog dialogue = Creer<IFileOpenDialog>(ClsidFileOpenDialog, typeof(IFileOpenDialog).GUID);
        var chaines = new List<IntPtr>();
        IntPtr specs = IntPtr.Zero;
        try
        {
            dialogue.SetTitle(titre);
            dialogue.GetOptions(out uint actuelles);
            dialogue.SetOptions(actuelles | options);

            if (filtres.Length > 0)
            {
                // COMDLG_FILTERSPEC : deux pointeurs de chaine par filtre.
                specs = Marshal.AllocHGlobal(filtres.Length * 2 * IntPtr.Size);
                for (int i = 0; i < filtres.Length; i++)
                {
                    IntPtr nom = Marshal.StringToHGlobalUni(filtres[i].Nom), motif = Marshal.StringToHGlobalUni(filtres[i].Motif);
                    chaines.Add(nom);
                    chaines.Add(motif);
                    Marshal.WriteIntPtr(specs, (2 * i) * IntPtr.Size, nom);
                    Marshal.WriteIntPtr(specs, (2 * i + 1) * IntPtr.Size, motif);
                }
                dialogue.SetFileTypes((uint)filtres.Length, specs);
            }

            int hr = dialogue.Show(proprietaire);
            if (hr == ErreurAnnule) return null;
            Marshal.ThrowExceptionForHR(hr);

            dialogue.GetResult(out IShellItem element);
            element.GetDisplayName(SigdnFileSysPath, out IntPtr chemin);
            try { return Marshal.PtrToStringUni(chemin); }
            finally { Marshal.FreeCoTaskMem(chemin); }
        }
        finally
        {
            foreach (IntPtr chaine in chaines) Marshal.FreeHGlobal(chaine);
            if (specs != IntPtr.Zero) Marshal.FreeHGlobal(specs);
        }
    }
}
