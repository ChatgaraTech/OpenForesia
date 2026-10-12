// Un clavier DirectInput tel que le jeu l'ouvre — pour mesurer, hors du jeu, ce qu'il recevra.
//
// Reproduit l'initialisation du jeu (OpenForesia/src/Blaze/Engine/Input.cs, Ghidra FUN_00522dc0 et
// FUN_005233c0) : DirectInputCreateA version 5, CreateDevice(GUID_SysKeyboard), le format clavier
// de 256 objets relu dans Blaze.exe (0x00546f60), SetCooperativeLevel(fenetre, 6 =
// DISCL_FOREGROUND | DISCL_NONEXCLUSIVE), Acquire ; lu par GetDeviceState sur 256 octets, octet
// enfonce = bit 0x80. Ce que ce clavier voit, le jeu le voit.

using System.Runtime.InteropServices;

namespace OpenForesia.Lanceur.Natif;

internal sealed unsafe partial class ClavierDirectInput : IDisposable
{
    [LibraryImport("dinput.dll")] private static partial int DirectInputCreateA(IntPtr instance, uint version, void** directInput, void* externe);

    // Les rangs de vtable du jeu (octets / 4), verifies sur Blaze.c.
    private const int CreateDevice = 0x0c / 4, Release = 2;
    private const int Acquire = 0x1c / 4, GetDeviceState = 0x24 / 4, SetDataFormat = 0x2c / 4, SetCooperativeLevel = 0x34 / 4;
    private const uint PremierPlanNonExclusif = 6;

    private static readonly Guid GuidSysKeyboard = new(0x6F1D2B61, 0xD5A0, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private static readonly Guid GuidKey = new(0x55728220, 0xD33C, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);

    [StructLayout(LayoutKind.Sequential)]
    private struct DIDATAFORMAT { public uint Size, ObjSize, Flags, DataSize, NumObjs; public void* Rgodf; }

    [StructLayout(LayoutKind.Sequential)]
    private struct DIOBJECTDATAFORMAT { public Guid* Guid; public uint Ofs, Type, Flags; }

    private void* _directInput, _clavier;
    private readonly DIDATAFORMAT* _format;
    private readonly DIOBJECTDATAFORMAT* _objets;
    private readonly Guid* _guidKey;
    private readonly byte* _etat = (byte*)NativeMemory.AllocZeroed(256);

    internal ClavierDirectInput(IntPtr fenetre)
    {
        void* di;
        int hr = DirectInputCreateA(Win32.GetModuleHandle(IntPtr.Zero), 0x500, &di, null);
        if (hr < 0) throw new InvalidOperationException($"DirectInputCreateA : 0x{hr:X8}");
        _directInput = di;

        void* clavier;
        Guid guid = GuidSysKeyboard;
        hr = ((delegate* unmanaged[Stdcall]<void*, Guid*, void**, void*, int>)(*(void***)di)[CreateDevice])(di, &guid, &clavier, null);
        if (hr < 0) throw new InvalidOperationException($"CreateDevice(clavier) : 0x{hr:X8}");
        _clavier = clavier;

        // Le format du jeu : 256 boutons optionnels, un octet chacun (relevé à 0x00546f60).
        _guidKey = (Guid*)NativeMemory.Alloc((nuint)sizeof(Guid));
        *_guidKey = GuidKey;
        _objets = (DIOBJECTDATAFORMAT*)NativeMemory.AllocZeroed(256, (nuint)sizeof(DIOBJECTDATAFORMAT));
        for (int i = 0; i < 256; i++)
        {
            _objets[i].Guid = _guidKey;
            _objets[i].Ofs = (uint)i;
            _objets[i].Type = 0x8000000C | ((uint)i << 8);
        }
        _format = (DIDATAFORMAT*)NativeMemory.AllocZeroed((nuint)sizeof(DIDATAFORMAT));
        _format->Size = 0x18;
        _format->ObjSize = 0x10;
        _format->Flags = 2;
        _format->DataSize = 256;
        _format->NumObjs = 256;
        _format->Rgodf = _objets;

        void** vt = *(void***)clavier;
        hr = ((delegate* unmanaged[Stdcall]<void*, DIDATAFORMAT*, int>)vt[SetDataFormat])(clavier, _format);
        if (hr < 0) throw new InvalidOperationException($"SetDataFormat : 0x{hr:X8}");
        hr = ((delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)vt[SetCooperativeLevel])(clavier, fenetre, PremierPlanNonExclusif);
        if (hr < 0) throw new InvalidOperationException($"SetCooperativeLevel : 0x{hr:X8}");
        ((delegate* unmanaged[Stdcall]<void*, int>)vt[Acquire])(clavier);
    }

    /// <summary>L'état des 256 touches, relu maintenant ; faux si le clavier ne peut pas être lu.</summary>
    internal bool Lire(out ReadOnlySpan<byte> etat)
    {
        void** vt = *(void***)_clavier;
        var lire = (delegate* unmanaged[Stdcall]<void*, uint, void*, int>)vt[GetDeviceState];
        var acquerir = (delegate* unmanaged[Stdcall]<void*, int>)vt[Acquire];

        // Comme le jeu (FUN_00523410) : sur échec, réacquérir et relire.
        int hr = lire(_clavier, 256, _etat);
        if (hr < 0 && acquerir(_clavier) >= 0) hr = lire(_clavier, 256, _etat);
        etat = new ReadOnlySpan<byte>(_etat, 256);
        return hr >= 0;
    }

    public void Dispose()
    {
        if (_clavier != null) ((delegate* unmanaged[Stdcall]<void*, uint>)(*(void***)_clavier)[Release])(_clavier);
        if (_directInput != null) ((delegate* unmanaged[Stdcall]<void*, uint>)(*(void***)_directInput)[Release])(_directInput);
        _clavier = _directInput = null;
    }
}
