// COM et WinRT a la main : appels par vtable, sans CsWinRT ni interfaces generees.
//
// La capture de fenetre (Windows.Graphics.Capture) est une API WinRT et Media Foundation une API COM.
// Les projections (CsWinRT) ne s'elaguent pas proprement et pesent lourd ; ici, un objet est un
// pointeur, une methode un rang de vtable, et chaque rang est verifie sur l'en-tete Windows qui le
// declare (cite en commentaire a l'endroit de l'appel). IUnknown occupe les rangs 0 a 2, IInspectable
// ajoute GetIids, GetRuntimeClassName et GetTrustLevel (3 a 5) : la premiere methode d'une interface
// WinRT est donc au rang 6.

using System.Runtime.InteropServices;

namespace OpenForesia.Multiplayer.Natif;

internal static unsafe partial class Com
{
    internal const int PremiereMethodeWinRT = 6;

    [LibraryImport("ole32.dll")] internal static partial int CoInitializeEx(IntPtr reserve, uint modele);
    [LibraryImport("ole32.dll")] internal static partial int CoCreateInstance(Guid* clsid, IntPtr externe, uint contexte, Guid* iid, void** objet);
    [LibraryImport("combase.dll")] internal static partial int RoInitialize(int genre);
    [LibraryImport("combase.dll")] private static partial int RoGetActivationFactory(IntPtr classe, Guid* iid, void** fabrique);
    [LibraryImport("combase.dll")] private static partial int WindowsCreateString(char* texte, uint longueur, IntPtr* chaine);
    [LibraryImport("combase.dll")] private static partial int WindowsDeleteString(IntPtr chaine);

    internal const uint CoinitMultithreaded = 0;
    internal const uint ClsctxInprocServer = 1;

    /// <summary>Le fil courant en appartement multithread, pour COM comme pour WinRT (sans erreur s'il l'est déjà).</summary>
    internal static void InitialiserFil()
    {
        CoInitializeEx(IntPtr.Zero, CoinitMultithreaded);
        RoInitialize(1);                          // RO_INIT_MULTITHREADED
    }

    /// <summary>Lève une exception si <paramref name="hr"/> est un échec — avec ce qu'on tentait de faire.</summary>
    internal static void Verifier(int hr, string quoi)
    {
        if (hr < 0) throw new COMException($"{quoi} : 0x{hr:X8}", hr);
    }

    internal static void** Vtable(void* objet) => *(void***)objet;

    internal static uint Liberer(void* objet) =>
        objet == null ? 0 : ((delegate* unmanaged[Stdcall]<void*, uint>)Vtable(objet)[2])(objet);

    internal static void* Interroger(void* objet, Guid iid)
    {
        void* resultat;
        int hr = ((delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)Vtable(objet)[0])(objet, &iid, &resultat);
        return hr < 0 ? null : resultat;
    }

    /// <summary>La fabrique d'activation d'une classe WinRT, interrogée sur <paramref name="iid"/>.</summary>
    internal static void* Fabrique(string classe, Guid iid)
    {
        IntPtr chaine;
        fixed (char* c = classe) Verifier(WindowsCreateString(c, (uint)classe.Length, &chaine), $"WindowsCreateString({classe})");
        try
        {
            void* fabrique;
            Verifier(RoGetActivationFactory(chaine, &iid, &fabrique), $"RoGetActivationFactory({classe})");
            return fabrique;
        }
        finally { WindowsDeleteString(chaine); }
    }

    /// <summary>Un objet COM créé par sa CLSID, interrogé sur <paramref name="iid"/>.</summary>
    internal static void* Creer(Guid clsid, Guid iid, string quoi)
    {
        void* objet;
        Verifier(CoCreateInstance(&clsid, IntPtr.Zero, ClsctxInprocServer, &iid, &objet), $"CoCreateInstance({quoi})");
        return objet;
    }
}
