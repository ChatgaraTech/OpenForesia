// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore ce que cette classe appelle : les tables d'options
// (s_KeyBindings, s_JoystickSlot, ...), les aides CRT et winsys3d.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Les entrees : DirectInput (clavier et manettes), l'etat de pad par joueur, les fronts de
/// boutons, l'affectation des manettes aux emplacements et les raccourcis clavier
/// (Ghidra: 0x00523 et 0x00504).
///
/// <para>
/// Un seul membre reste <c>static</c> : <c>CountJoystickCallback</c>, porte par
/// <c>[UnmanagedCallersOnly]</c> et passe a <c>EnumDevices</c>. Contrairement au rappel de
/// <see cref="Video"/>, il n'a <b>pas</b> besoin de rejoindre l'instance : il ne touche que son
/// parametre <c>context</c>. C'est ce que la tache 4.1 avait mesure, et qui se confirme ici.
/// </para>
/// <para>
/// Ce que la signature ne dit pas encore : cette classe lit aussi quatre membres d'echafaudage de
/// la page d'options (<c>s_KeyBindings</c>, <c>s_JoystickSlot</c>, <c>s_ResolutionDetailLimit</c>,
/// <c>s_SmallTextureFlag</c>). Ils ne sont PAS des globales <c>.data</c> - ce sont nos propres
/// tables de configuration - donc ils restent chez leur proprietaire plutot que de rejoindre
/// <see cref="GameMemory"/>. Ils deviendront un parametre de constructeur quand la page d'options
/// deviendra elle-meme une classe.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Input(GameMemory mem)
{

    // TODO(REVERSE): FUN_0053da20 @ 0x0053da20 (positional gain calculation: distance/angle ->
    // left/right gain pair via the cos table, ordinal 450) and its caller FUN_0053db20 are the
    // remaining members of this family and are deliberately *not* reconstructed yet. Ghidra
    // declares FUN_0053da20 with 2 parameters but FUN_0053db20 calls it with 3, and its body reads
    // `unaff_SI` and `unaff_retaddr` â€” register-passed values Ghidra never tied to a parameter.
    // Reconstructing it would mean inventing a signature, so it waits for a Ghidra re-run with the
    // calling convention corrected (same class of problem as the cases listed in REVERSE_STATUS.md).

    /// <summary>Ghidra: FUN_005235d0 @ 0x005235d0 â€” loads the [Controls] key-binding table.</summary>
    internal void LoadKeyBindings(byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(ControlsIniSection);
        int index = 0;
        for (int group = 0; group < 4; group++)
        {
            for (int category = 0; category < 3; category++)
            {
                for (int action = 0; action < 20; action++)
                {
                    byte* keyAnsi = ToAnsiZ($"K{group:x1}{category:x1}{action:x2}");
                    uint value = Win32.GetPrivateProfileIntA(sectionAnsi, keyAnsi, -1, iniPathAnsi);
                    Free(keyAnsi);
                    if (value != 0xffffffff) s_KeyBindings[index] = (int)value;
                    index++;
                }
            }
        }
        Free(sectionAnsi);
    }

    /// <summary>Ghidra: FUN_00523540 @ 0x00523540 â€” saves the [Controls] key-binding table.</summary>
    internal void SaveKeyBindings(byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(ControlsIniSection);
        int index = 0;
        for (int group = 0; group < 4; group++)
        {
            for (int category = 0; category < 3; category++)
            {
                for (int action = 0; action < 20; action++)
                {
                    byte* keyAnsi = ToAnsiZ($"K{group:x1}{category:x1}{action:x2}");
                    byte* valueAnsi = ToAnsiZ(s_KeyBindings[index].ToString(System.Globalization.CultureInfo.InvariantCulture));
                    Win32.WritePrivateProfileStringA(sectionAnsi, keyAnsi, valueAnsi, iniPathAnsi);
                    Free(keyAnsi);
                    Free(valueAnsi);
                    index++;
                }
            }
        }
        Free(sectionAnsi);
    }
    // =====================================================================
    // DirectInput bring-up â€” Ghidra: FUN_00523120 and its init chain
    //
    // The game's entire input path hangs off one 0x8c4-byte object. `SetupJoystickHandlers`
    // creates it; `PollJoystickState` (FUN_005048f0) reads it every frame; `ShutdownStage1`
    // destroys it. Its layout, recovered from the constructor's zeroing runs and from the three
    // out-pointers FUN_00523120 hands back:
    //
    //     +0x000  IDirectInputA*
    //     +0x004  HWND â€” the window given to SetCooperativeLevel
    //     +0x008  keyboard device cell (a pointer to a pointer, allocated separately)
    //     +0x00c  16 joystick device cells
    //     +0x04c  mouse device cell
    //     +0x050  keyboard state area, 0x360 bytes   <- out-pointer 1 (DAT_00604b50)
    //     +0x3b0  joystick state area, 0x500 bytes   <- out-pointer 2
    //     +0x8b0  number of attached joysticks
    //     +0x8b4  mouse state area                   <- out-pointer 3
    //
    // Everything the DLL side needs was recovered from `Legacy/Binaries/Blaze.exe` rather than assumed:
    // the two device GUIDs (`GUID_SysKeyboard` at 0x0054db58, `GUID_SysMouse` at 0x0054db48),
    // both DIDATAFORMATs (0x00546f60 = the 256-object keyboard format, 0x00546ff0 = the 7-object
    // mouse format) and the keyboard object table at 0x00545f60, whose entries are exactly
    // `{ &GUID_Key, i, DIDFT_OPTIONAL | DIDFT_BUTTON | (i << 8), 0 }`.
    // =====================================================================


    // IDirectInputA â€” byte offsets divided by 4, checked against the call sites in Blaze.c.
    private const int IDirectInput_CreateDevice = 0x0c / 4;
    private const int IDirectInput_EnumDevices = 0x10 / 4;

    // IDirectInputDeviceA â€” likewise; SetDataFormat at +0x2c, SetCooperativeLevel at +0x34 and
    // Acquire at +0x1c are what FUN_005233c0/FUN_00523470 call, in that order.
    private const int IDirectInputDevice_Acquire = 0x1c / 4;
    private const int IDirectInputDevice_GetDeviceState = 0x24 / 4;
    private const int IDirectInputDevice_SetDataFormat = 0x2c / 4;
    private const int IDirectInputDevice_SetCooperativeLevel = 0x34 / 4;

    /// <summary>DISCL_FOREGROUND | DISCL_NONEXCLUSIVE â€” the literal 6 both device setups pass.</summary>
    private const uint DiscLevel_ForegroundNonExclusive = 6;

    private readonly Guid s_GuidSysKeyboard =            // Ghidra: DAT_0054db58
        new(0x6F1D2B61, 0xD5A0, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private readonly Guid s_GuidSysMouse =               // Ghidra: DAT_0054db48
        new(0x6F1D2B60, 0xD5A0, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private readonly Guid s_GuidKey =                    // Ghidra: DAT_0054db18
        new(0x55728220, 0xD33C, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);

    [StructLayout(LayoutKind.Sequential)]
    private struct DIDATAFORMAT
    {
        public uint Size, ObjSize, Flags, DataSize, NumObjs;
        public void* Rgodf;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DIOBJECTDATAFORMAT
    {
        public Guid* Guid;
        public uint Ofs, Type, Flags;
    }

    /// <summary>
    /// Ghidra: the keyboard DIDATAFORMAT at 0x00546f60 and its object table at 0x00545f60, rebuilt
    /// in this process's memory. The table cannot be copied verbatim: every entry holds a pointer
    /// to a GUID at `Blaze.exe`'s own image base, which is not mapped here â€” so the bytes are
    /// reproduced field for field with our copy of `GUID_Key` substituted.
    /// </summary>
    private DIDATAFORMAT* BuildKeyboardDataFormat()
    {
        const int objects = 0x100;
        var format = (DIDATAFORMAT*)NativeMemory.AllocZeroed((nuint)sizeof(DIDATAFORMAT));
        var table = (DIOBJECTDATAFORMAT*)NativeMemory.AllocZeroed(objects, (nuint)sizeof(DIOBJECTDATAFORMAT));
        var guid = (Guid*)NativeMemory.Alloc((nuint)sizeof(Guid));
        *guid = s_GuidKey;

        for (int i = 0; i < objects; i++)
        {
            table[i].Guid = guid;
            table[i].Ofs = (uint)i;
            table[i].Type = 0x8000000C | ((uint)i << 8);   // DIDFT_OPTIONAL | DIDFT_BUTTON | instance
            table[i].Flags = 0;
        }

        format->Size = 0x18;          // relevÃ© verbatim Ã  0x00546f60
        format->ObjSize = 0x10;
        format->Flags = 2;            // DIDF_ABSAXIS
        format->DataSize = objects;   // 256 octets d'Ã©tat clavier
        format->NumObjs = objects;
        format->Rgodf = table;
        return format;
    }

    private DIDATAFORMAT* s_KeyboardDataFormat;

    private readonly Guid s_GuidXAxis =                  // Ghidra: DAT_0054da98
        new(0xA36D02E0, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private readonly Guid s_GuidYAxis =                  // Ghidra: DAT_0054daa8
        new(0xA36D02E1, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private readonly Guid s_GuidZAxis =                  // Ghidra: DAT_0054dab8
        new(0xA36D02E2, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);

    /// <summary>
    /// Ghidra: the mouse DIDATAFORMAT at 0x00546ff0 and its 7-object table at 0x00546f80, rebuilt
    /// the same way as the keyboard's. The table is transcribed entry for entry from the binary:
    /// three axes carrying the X/Y/Z axis GUIDs, then four buttons with a null GUID, the Z axis and
    /// the last two buttons flagged optional.
    /// </summary>
    private DIDATAFORMAT* BuildMouseDataFormat()
    {
        const int objects = 7;
        var format = (DIDATAFORMAT*)NativeMemory.AllocZeroed((nuint)sizeof(DIDATAFORMAT));
        var table = (DIOBJECTDATAFORMAT*)NativeMemory.AllocZeroed(objects, (nuint)sizeof(DIOBJECTDATAFORMAT));
        var axes = (Guid*)NativeMemory.Alloc(3, (nuint)sizeof(Guid));
        axes[0] = s_GuidXAxis; axes[1] = s_GuidYAxis; axes[2] = s_GuidZAxis;

        // (GUID, dwOfs, dwType) relevÃ©s Ã  0x00546f80 ; 0x03 = DIDFT_AXIS, 0x0c = DIDFT_BUTTON,
        // 0xffff00 = DIDFT_ANYINSTANCE, 0x80000000 = DIDFT_OPTIONAL.
        table[0] = new DIOBJECTDATAFORMAT { Guid = &axes[0], Ofs = 0x00, Type = 0x00FFFF03 };
        table[1] = new DIOBJECTDATAFORMAT { Guid = &axes[1], Ofs = 0x04, Type = 0x00FFFF03 };
        table[2] = new DIOBJECTDATAFORMAT { Guid = &axes[2], Ofs = 0x08, Type = 0x80FFFF03 };
        table[3] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0C, Type = 0x00FFFF0C };
        table[4] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0D, Type = 0x00FFFF0C };
        table[5] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0E, Type = 0x80FFFF0C };
        table[6] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0F, Type = 0x80FFFF0C };

        format->Size = 0x18;          // relevÃ© verbatim Ã  0x00546ff0
        format->ObjSize = 0x10;
        format->Flags = 2;            // DIDF_ABSAXIS
        format->DataSize = 0x10;      // DIMOUSESTATE
        format->NumObjs = objects;
        format->Rgodf = table;
        return format;
    }

    private DIDATAFORMAT* s_MouseDataFormat;

    /// <summary>
    /// Ghidra: FUN_00523120 @ 0x00523120 â€” allocates the input-device object, constructs it and
    /// runs its initialisation, handing back three pointers into it. Returns null if either step
    /// fails, exactly as the original does (it frees the object and returns 0).
    /// </summary>
    internal byte* CreateInputDevice(IntPtr instance, IntPtr window,
                                           byte** keyboardState, byte** joystickState, byte** mouseState)
    {
        // Ghidra: iVar1 = FUN_00547220(0x8c4) â€” the CRT operator new. A null return is carried
        // through rather than short-circuited, matching the original.
        var device = (byte*)Malloc(0x8c4);
        if (device != null) InputDeviceConstruct(device, instance, window);

        int hr = InputDeviceInitialise(device);
        if (hr < 0)
        {
            if (device != null)
            {
                DestroyJoystickDevice((void**)device);
                Free(device);
            }
            return null;
        }

        if (keyboardState != null) *keyboardState = device + 0x50;
        if (joystickState != null) *joystickState = device + 0x3b0;
        if (mouseState != null) *mouseState = device + 0x8b4;
        return device;
    }

    /// <summary>
    /// Ghidra: FUN_00522dc0 @ 0x00522dc0 â€” the constructor. Creates the DirectInput object and
    /// zeroes the three state areas.
    /// </summary>
    private void InputDeviceConstruct(byte* device, IntPtr instance, IntPtr window)
    {
        // Ghidra: if (param_2 == 0) param_2 = GetModuleHandleA(NULL) â€” which is why the never-found
        // DAT_005db85c never blocked this call.
        if (instance == IntPtr.Zero) instance = Win32.GetModuleHandleA(null);

        // Ghidra: DirectInputCreateA(hInstance, 0x500, this, 0) â€” version 5, the object receiving
        // the interface pointer at its own offset 0.
        int hr = Win32.DirectInputCreateA(instance, 0x500, (void**)device, null);
        if (hr < 0)
            Console.Error.WriteLine($"[INPUT] DirectInputCreateA -> 0x{hr:X8}");

        *(IntPtr*)(device + 4) = window;
        *(void**)(device + 8) = null;
        NativeMemory.Fill(device + 0x0c, 0x10 * sizeof(uint), 0);     // 16 joystick cells
        NativeMemory.Fill(device + 0x50, 0xd8 * sizeof(uint), 0);     // keyboard state area
        NativeMemory.Fill(device + 0x3b0, 0x140 * sizeof(uint), 0);   // joystick state area
        *(uint*)(device + 0x8b0) = 0;
        *(uint*)(device + 0x8b4) = 0;
        *(uint*)(device + 0x8b8) = 0;
        *(uint*)(device + 0x8bc) = 0;
        *(uint*)(device + 0x8c0) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00522eb0 @ 0x00522eb0 â€” creates the keyboard device, then the mouse device
    /// (whose failure is tolerated and simply clears its cell), then enumerates attached joysticks.
    /// </summary>
    private int InputDeviceInitialise(byte* device)
    {
        if (device == null || *(void**)device == null) return -1;

        void* directInput = *(void**)device;
        IntPtr window = *(IntPtr*)(device + 4);

        // Ghidra: a 4-byte cell is allocated for each device pointer and stored in the object; the
        // device interfaces themselves live one indirection further out.
        var keyboardCell = (void**)Malloc(4);
        if (keyboardCell != null) *keyboardCell = null;
        *(void***)(device + 8) = keyboardCell;

        int hr = CreateKeyboardDevice(keyboardCell, directInput, window);
        if (hr < 0) return hr;

        var mouseCell = (void**)Malloc(4);
        if (mouseCell != null) *mouseCell = null;
        *(void***)(device + 0x4c) = mouseCell;

        // Ghidra: a failing mouse releases its cell and carries on â€” the mouse is optional.
        if (CreateMouseDevice(mouseCell, directInput, window) < 0)
        {
            if (mouseCell != null) { ReleaseComObject(*mouseCell); Free(mouseCell); }
            *(void***)(device + 0x4c) = null;
        }

        // Ghidra: (*directInput)[0x10](4, &LAB_00522d80, &count, 1) â€” EnumDevices over
        // DIDEVTYPE_JOYSTICK with DIEDFL_ATTACHEDONLY, whose callback counts what is attached.
        int joystickCount = 0;
        var enumDevices = (delegate* unmanaged[Stdcall]<void*, uint, void*, void*, uint, int>)
            (*(void***)directInput)[IDirectInput_EnumDevices];
        enumDevices(directInput, 4, (delegate* unmanaged[Stdcall]<void*, void*, int>)&CountJoystickCallback,
                    &joystickCount, 1);

        // TODO(REVERSE): FUN_00523230 creates one device per enumerated joystick. Ghidra's
        // decompilation of it is unusable â€” the whole frame is rendered as `puStack_`/`piStack_`
        // temporaries and every vtable call has lost its arguments â€” so it belongs to the
        // "read the disassembly" class rather than the reading class. Until it is recovered the
        // 16 joystick cells stay null, which every consumer already tests for (FUN_005230b0 skips
        // null cells), so the keyboard path is unaffected.
        *(int*)(device + 0x8b0) = joystickCount;

        Console.Error.WriteLine(
            $"[INPUT] DirectInput prÃªt â€” clavier acquis, {joystickCount} manette(s) dÃ©tectÃ©e(s), " +
            "crÃ©ation des manettes non reconstruite (FUN_00523230)");
        return 0;
    }

    /// <summary>
    /// Ghidra: &LAB_00522d80 â€” the enumeration callback, address-taken only and with no decompiled
    /// body anywhere. Its role is fixed by its use: the count it accumulates in the caller's
    /// `pvRef` is what the loop below it iterates over. Returning DIENUM_CONTINUE (1) is what makes
    /// the enumeration visit every attached device.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int CountJoystickCallback(void* instance, void* context)
    {
        if (context != null) (*(int*)context)++;
        return 1;   // DIENUM_CONTINUE
    }

    /// <summary>
    /// Ghidra: FUN_005233c0 @ 0x005233c0 â€” CreateDevice(GUID_SysKeyboard), SetDataFormat,
    /// SetCooperativeLevel(window, 6), Acquire. Ghidra recovered two parameters where the call site
    /// passes three: the window it hands to SetCooperativeLevel is the one it dropped.
    /// </summary>
    private int CreateKeyboardDevice(void** cell, void* directInput, IntPtr window)
    {
        if (cell == null) return -1;

        var createDevice = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, void*, int>)
            (*(void***)directInput)[IDirectInput_CreateDevice];

        Guid guid = s_GuidSysKeyboard;
        int hr = createDevice(directInput, &guid, cell, null);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] CreateDevice(clavier) -> 0x{hr:X8}"); return hr; }

        if (s_KeyboardDataFormat == null) s_KeyboardDataFormat = BuildKeyboardDataFormat();

        void* keyboard = *cell;
        void** vtable = *(void***)keyboard;

        var setDataFormat = (delegate* unmanaged[Stdcall]<void*, DIDATAFORMAT*, int>)
            vtable[IDirectInputDevice_SetDataFormat];
        hr = setDataFormat(keyboard, s_KeyboardDataFormat);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetDataFormat(clavier) -> 0x{hr:X8}"); return hr; }

        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)
            vtable[IDirectInputDevice_SetCooperativeLevel];
        hr = setCooperativeLevel(keyboard, window, DiscLevel_ForegroundNonExclusive);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetCooperativeLevel(clavier) -> 0x{hr:X8}"); return hr; }

        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];
        acquire(keyboard);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00523470 @ 0x00523470 â€” the same sequence for GUID_SysMouse, with the 7-object
    /// mouse data format at 0x00546ff0. A failure at any step is returned to the caller, which
    /// treats a missing mouse as normal.
    /// </summary>
    private int CreateMouseDevice(void** cell, void* directInput, IntPtr window)
    {
        if (cell == null) return -1;

        var createDevice = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, void*, int>)
            (*(void***)directInput)[IDirectInput_CreateDevice];

        Guid guid = s_GuidSysMouse;
        int hr = createDevice(directInput, &guid, cell, null);
        if (hr < 0) return hr;

        if (s_MouseDataFormat == null) s_MouseDataFormat = BuildMouseDataFormat();

        void* mouse = *cell;
        void** vtable = *(void***)mouse;

        var setDataFormat = (delegate* unmanaged[Stdcall]<void*, DIDATAFORMAT*, int>)
            vtable[IDirectInputDevice_SetDataFormat];
        hr = setDataFormat(mouse, s_MouseDataFormat);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetDataFormat(souris) -> 0x{hr:X8}"); return hr; }

        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)
            vtable[IDirectInputDevice_SetCooperativeLevel];
        hr = setCooperativeLevel(mouse, window, DiscLevel_ForegroundNonExclusive);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetCooperativeLevel(souris) -> 0x{hr:X8}"); return hr; }

        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];
        acquire(mouse);
        return 0;
    }

    /// <summary>Ghidra: the IUnknown::Release calls the device teardown paths make.</summary>
    private void ReleaseComObject(void* obj)
    {
        if (obj == null) return;
        var release = (delegate* unmanaged[Stdcall]<void*, uint>)(*(void***)obj)[Video.IUnknown_Release];
        release(obj);
    }

    // Ghidra: DAT_00604b50 â€” the keyboard state area inside the device object, handed back as
    // FUN_00523120's third argument. `DAT_00604b54`, the object itself, is declared with the
    // teardown code that already owned it.
    internal byte* s_KeyboardState_00604b50;

    /// <summary>
    /// Ghidra: FUN_00504b10 @ 0x00504b10 â€” resets the 9-slot joystick binding/state table.
    /// `clearName` mirrors the original's `param_1 != 0` guard around one extra field
    /// (`puVar1[1] = 0`) that's cleared only on some call sites â€” exact field meaning unconfirmed.
    /// </summary>
    internal void JoystickResetState(bool clearName)
    {
        for (int i = 0; i < s_JoystickState.Length; i++)
        {
            s_JoystickState[i] = new JoystickSlotState
            {
                AxisOrButton = 0xffff,
                Name = clearName ? (ushort)0 : s_JoystickState[i].Name,
            };
        }
    }

    private struct JoystickSlotState
    {
        public ushort AxisOrButton;
        public ushort Name; // Ghidra: puVar1[1] â€” TODO(REVERSE) exact field meaning unconfirmed
    }

    // Ghidra: &DAT_00604a28-relative 9-entry table walked by FUN_00504b10.
    private readonly JoystickSlotState[] s_JoystickState = new JoystickSlotState[9];

    // Deplace dans GameMemory le 2026-08-19 : globale .data.

    // The four global libgpu environment structs Vid.InitViewportSurfaces sets up (Ghidra:
    // DAT_005e9880 / DAT_005e98dc / DAT_005e98f0 / DAT_005e994c). Their **addresses prove their
    // sizes**: consecutive structs sit 92 and 20 bytes apart, alternating, which is exactly
    // sizeof(DRAWENV) and sizeof(DISPENV) in the PlayStation SDK's libgpu:
    //
    //   0x005e9880  DRAWENV  92 bytes   draw env 0
    //   0x005e98dc  DISPENV  20 bytes   disp env 0
    //   0x005e98f0  DRAWENV  92 bytes   draw env 1
    //   0x005e994c  DISPENV  20 bytes   disp env 1   (ends 0x005e9960)
    //
    // DISPENV = { RECT disp; RECT screen; u_char isinter, isrgb24, pad0, pad1; }
    // DRAWENV = { RECT clip; short ofs[2]; RECT tw; u_short tpage; u_char dtd, dfe, isbg,
    //             r0, g0, b0; ... }  with RECT = { short x, y, w, h; }
    //
    // Allocated zeroed, matching how this file models other DAT_ globals that are really structs.
    // They must be process-lifetime globals, not stack buffers: the not-yet-reconstructed rendering
    // code reads them back on every frame.
    private const int DrawEnvSize = 92;   // 0x5C
    private const int DispEnvSize = 20;   // 0x14

    // Les quatre structures sont **contiguÃ«s** dans le binaire, et cette contiguÃ¯tÃ© est un contrat,
    // pas un hasard de placement :
    //
    //     0x005e9880  DrawEnv0  92 octets
    //     0x005e98dc  DispEnv0  20      <- DrawEnv0 + 0x5C
    //     0x005e98f0  DrawEnv1  92      <- DrawEnv0 + 0x70
    //     0x005e994c  DispEnv1  20      <- DrawEnv1 + 0x5C
    //     0x005e9960  (s_FadeCounter, la globale suivante â€” d'oÃ¹ la taille totale de 224)
    //
    // FrameEnd publie l'environnement d'affichage par `Ordinal_657(nextDrawEnv + 0x5c)` : il
    // *dÃ©pend* du fait que le DISPENV suive immÃ©diatement son DRAWENV. Les allouer en quatre blocs
    // sÃ©parÃ©s faisait lire 20 octets de tas quelconque Ã  la place du DISPENV â€” d'oÃ¹ un Ã©cran noir
    // intermittent, mesurÃ© Ã  7 lancements sur 10, selon ce que le tas contenait Ã  cet endroit.
    // Voir ANALYSE_ECRAN_NOIR_MENU.md pour la mesure qui a isolÃ© la cause.
    // =====================================================================
    // Per-frame input polling (Ghidra: FUN_00512520 / FUN_005048f0 / FUN_00504a30 / FUN_005357a0).
    //
    // These share three global regions whose *relative* offsets the code depends on, so each is
    // modelled as one block with derived pointers rather than as separate allocations:
    //   0x00604a20  the per-button input state table (0x10 bytes per entry)
    //   0x00604b00  the two joystick axis/state blocks
    //   0x005e9180  the player-slot table and its parallel per-slot array at +0x240
    // =====================================================================

    internal readonly byte* s_InputStateBase_00604a20 = (byte*)NativeMemory.AllocZeroed(0x140);
    internal byte* s_ButtonStates_00604a2a => s_InputStateBase_00604a20 + 0x0a;
    internal ushort* s_JoyAxis1_00604b0a => (ushort*)(s_InputStateBase_00604a20 + 0xea);
    internal uint* s_JoyBlock1_00604b0c => (uint*)(s_InputStateBase_00604a20 + 0xec);
    internal ushort* s_JoyAxis2_00604b2e => (ushort*)(s_InputStateBase_00604a20 + 0x10e);

    // 0x4c0 utiles, borné par mem.s_AnimationTable_005e9640 — ce qui couvre bien le plus grand décalage
    // lu (0x240 + 4 * 0x9c = 0x4bc).
    internal byte* s_SlotArea_005e9180 => mem.Arena(0x005e9180);
    internal byte* s_PlayerSlots_005e9188 => s_SlotArea_005e9180 + 8;
    internal byte* s_SlotFlags_005e93c0 => s_SlotArea_005e9180 + 0x240;

    // Ghidra: the four 16-byte controllerâ†’player remap rows at 0x0056acf8, reached through the
    // pointer table at 0x0056ad38. Recovered verbatim from Legacy/Binaries/Blaze.exe. The row is chosen by
    // which of the two joystick axes reads the -0x8000 "absent" sentinel.
    private readonly byte[][] s_ControllerRemapRows =
    [
        [0x00, 0x04, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x04, 0x05, 0x06, 0x07, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0, 0, 0, 0, 0, 0, 0],
    ];

    /// <summary>
    /// Ghidra: the index computation shared by FUN_00542250 and FUN_005357a0 â€” one bit per joystick,
    /// set when its axis reads the <c>-0x8000</c> sentinel.
    /// </summary>
    private int ControllerRemapIndex()
    {
        int index = *(short*)s_JoyAxis1_00604b0a == -0x8000 ? 1 : 0;
        if (*(short*)s_JoyAxis2_00604b2e == -0x8000) index += 2;
        return index;
    }

    /// <summary>
    /// Ghidra: FUN_00512520 @ 0x00512520 â€” the debug hotkey handler, polled once per frame:
    /// F3 opens the options page, F11/F12 step the detail level, F9 toggles the small-texture flag.
    /// Returns 1 when the options page ran, 0 otherwise.
    /// </summary>
    private int PollDebugHotkeys(byte* context)
    {
        s_Unknown_00579270 = context;

        if (s_Unknown_005de554 == 0 && Win32.GetAsyncKeyState(0x72 /* VK_F3 */) < 0)
        {
            int gammaBefore = mem.s_Unknown_005d9de0;
            Winsys3d.ReInitializeAllTextures();
            int shiftX, shiftY;
            Winsys3d.GetAbsShift(&shiftX, &shiftY);
            Winsys3d.SetAbsShift(0, 0);
            if (mem.s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
            OptionsPage();
            Winsys3d.SetAbsShift(shiftX, shiftY);
            SaveOptionsIni();
            if (gammaBefore != mem.s_Unknown_005d9de0)
            {
                // Note: this site scales by _DAT_0054d000, *not* the _DAT_0054d074 the menu's
                // options case uses. Two distinct constants; kept distinct.
                Winsys3d.CreateTranslationTables(mem.s_Unknown_005d9de0 * s_GammaScale_0054d000);
            }
            return 1;
        }

        if (Win32.GetAsyncKeyState(0x7a /* VK_F11 */) != 0)
        {
            if (mem.s_SizeSetting_005de160 != 0)
            {
                mem.s_SizeSetting_005de160--;
                Winsys3d.SetShift((0xb - mem.s_SizeSetting_005de160) * 0x10,
                         (0xb - mem.s_SizeSetting_005de160) * 0x0c,
                         mem.s_SizeSetting_005de160, 0);
                if (mem.s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
                SaveOptionsIni();
                do { Win32.Sleep(100); } while (Win32.GetAsyncKeyState(0x7a) < 0);
                return 0;
            }
        }
        else if (Win32.GetAsyncKeyState(0x7b /* VK_F12 */) != 0)
        {
            // The clamp table here is DAT_00562e00 â€” recovered as { 1, 7, 11, 16, 23, 0 }. F12
            // raises Size up to the ceiling for the current Resolution; at Resolution=4 that
            // ceiling is 23, which is what the installed BLAZE.cfg holds.
            int limit = s_ResolutionIndexForClamp < s_ResolutionDetailLimit.Length
                ? s_ResolutionDetailLimit[s_ResolutionIndexForClamp]
                : int.MaxValue;
            if (mem.s_SizeSetting_005de160 < limit)
            {
                mem.s_SizeSetting_005de160++;
                Winsys3d.SetShift((0xb - mem.s_SizeSetting_005de160) * 0x10,
                         (0xb - mem.s_SizeSetting_005de160) * 0x0c,
                         mem.s_SizeSetting_005de160, 0);
                SaveOptionsIni();
                do { Win32.Sleep(100); } while (Win32.GetAsyncKeyState(0x7b) < 0);
                return 0;
            }
        }
        else if (Win32.GetAsyncKeyState(0x78 /* VK_F9 */) != 0 && mem.s_SizeSetting_005de160 > 6)
        {
            s_SmallTextureFlag = s_SmallTextureFlag == 0 ? 1u : 0u;
            SaveOptionsIni();
            do { Win32.Sleep(100); } while (Win32.GetAsyncKeyState(0x78) < 0);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00504a30 @ 0x00504a30 â€” button edge detection and auto-repeat over two blocks of
    /// four slots. `0x4100` marks a live entry; the XOR pair yields pressed/released masks, and the
    /// counter at +0xa runs 0xc frames before repeating every 2.
    /// </summary>
    internal void UpdateButtonEdges()
    {
        ushort* state = (ushort*)s_InputStateBase_00604a20;
        uint* joyBlock = s_JoyBlock1_00604b0c;

        for (int block = 0; block < 2; block++)
        {
            uint* entry = joyBlock;
            for (int i = 0; i < 4; i++)
            {
                uint raw = *entry;
                if ((short)raw == 0x4100)
                {
                    state[3] = 0;
                    ushort current = (ushort)~(ushort)(raw >> 0x10);
                    state[4] = state[0];
                    ushort changed = (ushort)(state[0] ^ current);
                    state[0] = current;
                    state[1] = changed;

                    if ((changed & 0xf0) == 0)
                    {
                        byte countdown = (byte)(((byte*)(state + 5))[0] - 1);
                        ((byte*)(state + 5))[0] = countdown;
                        if (countdown == 0)
                        {
                            ((byte*)(state + 5))[0] = 2;
                            if (((byte)state[0] & 0xf0) != 0) state[3] = (ushort)((byte)state[0] & 0xf0);
                        }
                    }
                    else
                    {
                        ((byte*)(state + 5))[0] = 0xc;
                        state[3] = (ushort)((byte)state[0] & 0xf0);
                    }

                    state[2] = (ushort)(state[4] & state[1]);
                    ushort held = (ushort)(state[0] & state[1]);
                    state[1] = held;
                    ((byte*)state)[0xb] |= 0x80;
                    ((byte*)state)[0xb] = (((byte)state[2] | (byte)held) & 0xf0) != 0
                        ? (byte)(((byte*)state)[0xb] | 2)
                        : ((byte*)state)[0xb];
                }
                else
                {
                    ((byte*)state)[0xb] &= 0x7e;
                }

                entry += 2;
                state += 8;
            }
            joyBlock += 9;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005357a0 @ 0x005357a0 â€” assigns controllers to the four player slots through the
    /// remap row selected by <see cref="ControllerRemapIndex"/>.
    /// </summary>
    internal void AssignControllersToSlots()
    {
        byte[] row = s_ControllerRemapRows[ControllerRemapIndex()];

        byte* slotFlags = s_SlotFlags_005e93c0;
        byte* slot = s_PlayerSlots_005e9188;

        for (int i = 0; i < 4; i++)
        {
            byte mapped = row[slot[1]];
            int mappedOffset = (sbyte)mapped * 0x10;
            byte* current = s_ButtonStates_00604a2a + slot[0] * 0x10 + 1;
            byte currentFlags = *current;

            if ((currentFlags & 0x80) == 0)
            {
                byte* target = s_ButtonStates_00604a2a + mappedOffset + 1;
                byte targetFlags = *target;
                if ((targetFlags & 0x80) == 0)
                {
                    *current = (byte)(currentFlags & 0xfe);
                    *target = (byte)(*target & 0xfe);
                    slot[0] = 8;
                }
                else if ((targetFlags & 1) == 0)
                {
                    *target = (byte)(targetFlags | 1);
                    slot[0] = mapped;
                }
            }
            else if ((slotFlags[2] & 1) != 0 && slot[0] != (sbyte)mapped)
            {
                *current = (byte)(currentFlags & 0x7e);
                slot[0] = 8;
            }

            slotFlags += 0x9c;
            slot += 0x4c;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005048f0 @ 0x005048f0 â€” polls the DirectInput joystick device and folds its
    /// state into the per-slot tables.
    /// <para>
    /// Three parts, all reconstructed: `FUN_005231f0`/`FUN_005230b0` read every acquired device,
    /// `FUN_00523660` folds the keyboard into the four players' pad states through the
    /// `[Controls]` binding table, and the loop below distributes those states across the slot
    /// table the rest of the game reads. Verified at runtime: the menu cursor moves one entry per
    /// arrow press and the confirm key leaves the menu.
    /// </para>
    /// </summary>
    internal int PollJoystickState()
    {
        if (mem.s_JoystickDevice_00604b54 == null) return 0;

        // Ghidra: FUN_005231f0(DAT_00604b54) â€” a one-line forwarder to FUN_005230b0.
        PollInputDevices(mem.s_JoystickDevice_00604b54);

        // Ghidra: FUN_00523660(DAT_00604b50, &DAT_00604ac0)
        int anyInput = FoldKeyboardIntoPadState(s_KeyboardState_00604b50,
                                                s_InputStateBase_00604a20 + 0xa0);

        // Ghidra: the two-block folding loop that closes FUN_005048f0. Its cursor starts at
        // `&DAT_00604b0c` and advances by 9 dwords (0x24 bytes); every access below is written as
        // the byte offset that cursor computes, so the pointer arithmetic stays checkable against
        // the pseudocode instead of being re-expressed.
        byte* state = s_InputStateBase_00604a20;
        int cursor = 0xec;
        for (int block = 0; block < 2; block++, cursor += 0x24)
        {
            uint packed = *(uint*)(state + cursor - 0x4c);       // &DAT_00604ac0 for the first block
            uint live = (packed & 0x7777) << 1;

            if ((byte)(packed >> 0x10) == 0)
            {
                if ((packed & 0x80000000) == 0)                  // (packed >> 0x10 & 0x8000)
                {
                    live |= 1;
                    *(uint*)(state + cursor) =
                        *(ushort*)(state + cursor - 0x4a) | (*(uint*)(state + cursor - 0x48) << 0x10);

                    if ((packed & 0x30000000) != 0)              // (packed >> 0x10 & 0x3000)
                    {
                        *(uint*)(state + cursor + 4) =
                            (*(uint*)(state + cursor - 0x44) << 0x10)
                            | (*(uint*)(state + cursor - 0x48) >> 0x10);
                    }
                }
                else
                {
                    uint bit = 1;
                    int source = cursor - 0x48;
                    for (int i = 0; i < 4; i++, source += 8, bit <<= 4)
                    {
                        uint entry = *(uint*)(state + source);
                        if ((byte)entry != 0) continue;

                        live |= bit & 0xffff;
                        *(uint*)(state + source + 0x48) = entry;
                        if ((entry & 0x3000) != 0)
                            *(uint*)(state + source + 0x4c) = *(uint*)(state + source + 4);
                    }
                }
            }

            // Ghidra: any of the four sub-slots the folding above did not mark live is stamped
            // with the "nothing pressed" pattern â€” 0xffff41ff / 0xffffffff, the same active-low
            // convention the button masks use.
            uint slotMask = 0xf;
            int slot = cursor;
            for (int i = 0; i < 4; i++, slot += 8, slotMask <<= 4)
            {
                if ((slotMask & 0xffff & live) != 0) continue;
                *(uint*)(state + slot) = 0xffff41ff;
                *(uint*)(state + slot + 4) = 0xffffffff;
            }

            *(short*)(state + cursor - 0x4c) = (short)live;
            if ((short)live != 0)
                *(ushort*)(state + cursor - 2) = *(ushort*)(state + cursor - 0x4a);
        }

        return anyInput;
    }

    /// <summary>
    /// Ghidra: FUN_005230b0 @ 0x005230b0 â€” reads the current state of every acquired device into
    /// the object's three state areas.
    /// </summary>
    private void PollInputDevices(byte* device)
    {
        if (device == null) return;

        // Ghidra: FUN_00523410(device + 0x50) â€” a thiscall whose `this` is the keyboard cell;
        // Ghidra shows only the stack argument. 0x100 bytes of keyboard state, cleared as 0x40
        // dwords when the device is gone.
        if (*(void**)(device + 8) != null)
            ReadDeviceState(*(void***)(device + 8), device + 0x50, 0x100, 0x40);

        // Ghidra: FUN_005234c0(device + 0x8b4) â€” same shape for the mouse: 0x10 bytes, 4 dwords.
        if (*(void**)(device + 0x4c) != null)
            ReadDeviceState(*(void***)(device + 0x4c), device + 0x8b4, 0x10, 4);

        *(uint*)(device + 0x14c) = *(uint*)(device + 0x8c0);

        // Ghidra: FUN_00523340 / FUN_00522fe0 per attached joystick. Every cell is null while
        // FUN_00523230 is unreconstructed, so this cannot fire today; it is left loud rather than
        // silently skipped so the day a stick is created the gap names itself.
        for (int i = 0; i < 0x10; i++)
        {
            if (*(void**)(device + 0x0c + i * 4) != null)
                throw new NotImplementedException(
                    "FUN_005230b0: FUN_00523340/FUN_00522fe0 (lecture d'une manette) non reconstruites");
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523410 @ 0x00523410 and FUN_005234c0 @ 0x005234c0 â€” the same function twice,
    /// differing only in the state size they request and the number of dwords they clear on
    /// failure. `IDirectInputDevice::GetDeviceState` is retried after re-acquiring, which is what
    /// makes input survive the window losing and regaining the foreground.
    /// </summary>
    private void ReadDeviceState(void** cell, byte* destination, uint size, int zeroDwords)
    {
        void* device = cell != null ? *cell : null;
        if (device == null)
        {
            NativeMemory.Fill(destination, (nuint)(zeroDwords * sizeof(uint)), 0);
            return;
        }

        void** vtable = *(void***)device;
        var getDeviceState = (delegate* unmanaged[Stdcall]<void*, uint, void*, int>)
            vtable[IDirectInputDevice_GetDeviceState];
        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];

        while (getDeviceState(device, size, destination) < 0)
        {
            if (acquire(device) < 0)
            {
                NativeMemory.Fill(destination, (nuint)(zeroDwords * sizeof(uint)), 0);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523660 @ 0x00523660 â€” folds the raw keyboard (and joystick) state into the
    /// four players' pad states, through the `[Controls]` binding table.
    /// <para>
    /// The convention is **active low**: a player's button word starts at 0xffff and every bound
    /// key that is down clears its bit. The bit each binding clears comes from the 20-entry mask
    /// table at 0x00564f18, recovered from `Legacy/Binaries/Blaze.exe` and unmistakably a PlayStation pad
    /// layout â€” 0x0010 up, 0x0020 right, 0x0040 down, 0x0080 left, 0x0008 start, 0x0001 select,
    /// the four face buttons at 0x1000..0x8000, the shoulders at 0x0100..0x0800, and four
    /// two-button combinations closing the table.
    /// </para>
    /// <para>
    /// Returns non-zero if anything at all was pressed â€” the value FUN_005048f0 passes on.
    /// </para>
    /// </summary>
    internal int FoldKeyboardIntoPadState(byte* deviceState, byte* padState)
    {
        if (deviceState == null || padState == null) return 0;

        int anyInput = 0;

        // Ghidra: if (DAT_00564a50 == 0) the raw state is zeroed before use â€” a keyboard-enable
        // gate. Recovered from Legacy/Binaries/Blaze.exe as 1, so the state is kept.
        if (s_KeyboardEnabled_00564a50 == 0)
            NativeMemory.Fill(deviceState, 0x3f * sizeof(uint), 0);

        // Ghidra: state[0x1c] |= state[0x9c]; state[0x9c] = 0. Those two DirectInput scancodes are
        // DIK_RETURN and DIK_NUMPADENTER: the keypad's Enter is folded into the main one.
        deviceState[0x1c] |= deviceState[0x9c];
        deviceState[0x9c] = 0;

        // Ghidra: nine pairs of ushorts cleared from +0x24, then three fixed bytes.
        NativeMemory.Fill(padState + 0x24, 9 * 4, 0);
        padState[0x27] = 0x41;
        padState[0x02] = 0;
        padState[0x03] = 0x80;

        for (int player = 0; player < 4; player++)
        {
            int bindings = player * 60;     // Ghidra: the 0x3c-int stride of DAT_00564b58
            int entry = player * 8;         // Ghidra: iVar6, the pad-state stride

            // Ghidra: scancode 0x3e is DIK_F4 â€” held down, it silences the keyboard entirely. The
            // second test lets only player 0 through while DAT_005de168 is set.
            if ((deviceState[0x3e] & 0x80) == 0 && (player == 0 || s_Unknown_005de168 == 0))
            {
                ushort buttons = 0xffff;
                padState[entry + 4] = 0;
                padState[entry + 5] = 0x41;

                // Ghidra: two passes of 20 over binding categories 0 and 1 â€” the primary and
                // alternate keyboard layouts. The binding cursor runs on across both.
                for (int category = 0; category < 2; category++)
                {
                    for (int action = 0; action < 20; action++)
                    {
                        int scancode = s_KeyBindings[bindings + category * 20 + action];
                        if (scancode == 0) continue;
                        if ((deviceState[scancode] & 0x80) == 0) continue;

                        anyInput = 1;
                        buttons &= (ushort)~s_PadButtonMasks_00564f18[action];
                    }
                }

                // Ghidra: the third pass reads category 2 â€” the joystick bindings â€” from the
                // joystick button area at state + 0x100, indexed by this player's assigned stick.
                ushort accumulated = buttons;
                for (int action = 0; action < 20; action++)
                {
                    int scancode = s_KeyBindings[bindings + 40 + action];
                    int stick = s_JoystickSlot[player];
                    if (scancode == -1 || stick == -1) continue;

                    // Transcribed in the original's order: the assignment happens whether or not
                    // the button test passes, which is why it is inside the guard and not after.
                    buttons = accumulated;
                    if ((deviceState[0x100 + stick * 0x26 + scancode] & 0x80) == 0) continue;

                    anyInput = 1;
                    accumulated &= (ushort)~s_PadButtonMasks_00564f18[action];
                    buttons = accumulated;
                }

                *(ushort*)(padState + entry + 6) = buttons;
            }
            else
            {
                ushort clear = (ushort)~(0xf << (player * 4));
                *(ushort*)padState &= clear;
                *(ushort*)(padState + 0x48) &= clear;
                padState[entry + 4] = 0xff;
                padState[entry + 5] = 0;
                *(ushort*)(padState + entry + 6) = 0;
            }
        }

        return anyInput;
    }

    /// <summary>
    /// Ghidra: &amp;DAT_00564f18 â€” 20 PlayStation pad button masks, one per binding slot, read
    /// verbatim out of <c>Legacy/Binaries/Blaze.exe</c>. Slots 9 and 10 are genuinely zero; the last four
    /// are two-button combinations.
    /// </summary>
    internal readonly ushort[] s_PadButtonMasks_00564f18 =
    [
        0x0100, 0x0200, 0x0400, 0x0800,     // L2, R2, L1, R1
        0x1000, 0x2000, 0x4000, 0x8000,     // triangle, circle, cross, square
        0x0001, 0x0000, 0x0000, 0x0008,     // select, --, --, start
        0x0010, 0x0020, 0x0040, 0x0080,     // up, right, down, left
        0x0500, 0x0a00, 0x0c00, 0x0300,     // L2+L1, R2+R1, L1+R1, L2+R2
    ];

    /// <summary>
    /// Ghidra: DAT_00564a50 â€” read from Legacy/Binaries/Blaze.exe as 1. **Not read-only**: the two
    /// one-line functions <c>FUN_00523520</c> and <c>FUN_00523530</c> exist for nothing else than
    /// to set it to 1 and 0, and the name-entry sub-state calls both. It gates
    /// <see cref="FoldKeyboardIntoPadState"/>, so clearing it silences the pad mapping while raw
    /// characters are being typed.
    /// </summary>
    private int s_KeyboardEnabled_00564a50 = 1;

    /// <summary>Ghidra: FUN_00523520 @ 0x00523520 â€” <c>DAT_00564a50 = 1</c>, nothing else.</summary>
    internal void EnablePadKeyboard() => s_KeyboardEnabled_00564a50 = 1;

    /// <summary>Ghidra: FUN_00523530 @ 0x00523530 â€” <c>DAT_00564a50 = 0</c>, nothing else.</summary>
    internal void DisablePadKeyboard() => s_KeyboardEnabled_00564a50 = 0;

    // Ghidra: DAT_005de168 â€” gates players 2..4 in FUN_00523660. Never assigned anywhere in the
    // reconstructed graph yet, so it stays 0 and all four players are polled, as the original does
    // on a fresh start.
    internal int s_Unknown_005de168;
    internal byte* s_Unknown_00579270;    // Ghidra: DAT_00579270
    private int s_Unknown_005de554;      // Ghidra: DAT_005de554
    // Ghidra: DAT_00566cc4 â€” **the same global as s_ResolutionIndex**, not a second one. Blaze.c
    // line 58267 fills it from the ini's Resolution key, exactly as LoadOptionsIni does here, and
    // the reconstruction had split one global into two fields with only one of them ever assigned.
    // Third instance of this shape today, after DAT_0056e61f and DAT_005e8fc0.
    internal int s_ResolutionIndexForClamp => (int)s_ResolutionIndex;

    // Ghidra: _DAT_0054d000 â€” **lu dans Legacy/Binaries/Blaze.exe : 0x3C800000, soit 1/64**, et non 1.0f
    // comme un TODO le supposait ici. C'est aussi le mÃªme symbole que `GammaIniScale` dÃ©clarÃ© plus
    // haut dans ce fichier, qui portait dÃ©jÃ  la bonne valeur par dÃ©duction ; huitiÃ¨me global
    // reconstruit sous deux noms.
    //
    // La valeur compte : la page d'options passe ses palettes dans `Gamma(valeur, rÃ©glage * ce
    // facteur)`. Ã€ 1.0f le produit valait 64 et rendait toutes les palettes blanches â€” fond et
    // cadre invisibles. Ã€ 1/64 le rÃ©glage par dÃ©faut de l'ini (64) donne 1.0, le gamma neutre.
    internal const float s_GammaScale_0054d000 = 1.0f / 64.0f;

    /// <summary>Ghidra: the four calls guarded by arg5 in FUN_0052a280.</summary>
    internal void PollInputAndAudio()
    {
        // Ghidra: FUN_00512520(&DAT_00604a20 +
        //   (byte)(&DAT_005e9188)[(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4)
        // The outer index had been hardcoded to 0 instead of reading _DAT_005de538. Equivalent
        // while that global is 0, but not faithful â€” and FUN_00545340 uses the same expression,
        // which is what settled it.
        int slotIndex = s_PlayerSlots_005e9188[
            s_SlotArea_005e9180[0x248 + (uint)(mem.s_Unknown_005de538 & 0xff) * 0x9c] * 0x4c];
        // Pas â€‹**0x10**, pas 4 : `Legacy/Raw/Blaze/FUN_0052a280.asm` 0x0052a34c montre
        // `SHL ECX,0x4` puis `ADD ECX,0x604a20`. Même piège que celui documenté sur
        // <see cref="CurrentPadState"/> â€” la pseudo-C écrit `&DAT_00604a20 + n * 4` parce que le
        // symbole est typé `undefined4`, ce qui vaut n*0x10 en octets. Corrigé 2026-08-17 ; le
        // décalage n'était juste que pour l'emplacement 0.
        PollDebugHotkeys(s_InputStateBase_00604a20 + slotIndex * 0x10);

        PollJoystickState();
        UpdateButtonEdges();
        AssignControllersToSlots();
    }
}
