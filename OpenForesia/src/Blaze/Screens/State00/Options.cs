// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // Options .ini persistence (Ghidra: FUN_0050f140 "load" -> LoadOptionsIni,
    // FUN_0050f300/thunk_FUN_0050f300 "save" -> SaveOptionsIni, FUN_005235d0/FUN_00523540
    // "key bindings load/save" -> Inp.LoadKeyBindings/Inp.SaveKeyBindings).
    //
    // Section/key name strings below are reconstructed from Ghidra's auto-derived symbol names
    // (e.g. `s_SoundVol_005630b0` -> "SoundVol"), which mirror the literal string bytes closely
    // enough to trust directly. Two exceptions have no derived name (Ghidra didn't recognize
    // them as printable text) and are marked TODO(REVERSE) below.
    // =====================================================================

    internal const string IniSection = "Options"; // Ghidra: s_Options_005630a8
    internal const string ControlsIniSection = "Controls"; // Ghidra: s_Controls_00564f60

    // TODO(REVERSE): DAT_005630bc has no Ghidra-derived string name (short/undecoded literal);
    // ".ini" is a strong but unconfirmed guess for the settings-file extension.
    // Ghidra: DAT_005630bc â€” recovered from Legacy/Blaze.exe as ".cfg", not ".ini". The installed
    // game's own options file is BLAZE.cfg, and its [Options]/[D3DDriver]/[Controls] sections match
    // what LoadOptionsIni/Inp.LoadKeyBindings already read â€” so those two were correct all along and
    // had simply never located their file.
    internal const string IniFileExtension = ".cfg";

    // Ghidra: &DAT_00563080 (Blaze.c:58266) â€” Ghidra gave it no string name, but the literal is
    // present in Legacy/Blaze.exe: "Size". It sits in the .data key block exactly where the call
    // order puts it, between "Small" (0x00563088) and "Resolution" (0x00563074), and the installed
    // game's BLAZE.cfg carries Size=23 alongside Resolution=4 â€” which is precisely
    // s_ResolutionDetailLimit[4]. Three independent confirmations; no longer a TODO.
    private const string SizeIniKey = "Size";

    // Ghidra: DAT_0054d000. TODO(REVERSE): no literal value in Blaze.c (data-only global); this
    // is inferred as 1/64 from cross-referencing winsys3d.c's own default
    // `CreateTranslationTables(1.0f)` call against the ini's default Gamma value of 0x40 (64) â€”
    // 64 * (1/64) == 1.0, matching the DLL's default. See Documentation/DLL/winsys3d.md.
    private const float GammaIniScale = 1.0f / 64.0f;

    // Les trois cles d'ini (DAT_00589c70 / DAT_0057925c / DAT_005d9de0) sont dans GameMemory
    // depuis le 2026-08-19 : Mem.s_GammaIni EST s_Unknown_005d9de0, et un alias ne se separe pas.

    // Ghidra: AU_00564a05._51_4_ â€” a 4-byte field at offset 0x33 inside a larger, not-yet-
    // reconstructed blob (AU_00564a05). Ini key "Small", default 0.
    internal static uint s_SmallTextureFlag;

    // Deplace dans GameMemory le 2026-08-19 : globale .data lue par Engine/Input.cs.

    /// <summary>Taille de rendu que le rÃ©glage <c>Size</c> implique, en pixels.</summary>
    // `internal` : appele depuis src/Host/DeviceShim.cs.
    internal static (int Width, int Height) RenderSizeFromSizeSetting()
        => ((Mem.s_SizeSetting_005de160 + 9) * 0x20, (Mem.s_SizeSetting_005de160 + 9) * 0x18);

    // Ghidra: DAT_00566cc4 (ini "Resolution", default 2) / DAT_00562df8 (ini "Timing", default 1).
    internal static uint s_ResolutionIndex;
    private static uint s_TimingMode;

    // Ghidra: &DAT_00564a58, 4 ints â€” per-joystick-slot assignment (-1 = unassigned). Ini keys
    // built from format string "Joy%d" â€” no space (Ghidra: s_Joy_d_00563064; literal read back
    // from Legacy/Blaze.exe at 0x00563064). An earlier reconstruction wrote "Joy %d", which reads
    // keys the real BLAZE.cfg does not contain: the joystick settings silently fell back to their
    // defaults instead of being loaded.
    internal static readonly int[] s_JoystickSlot = new int[4];

    // Ghidra: &DAT_00562e00 â€” per-resolution clamp table for Mem.s_SizeSetting_005de160.
    // TODO(REVERSE): actual byte content not present in Blaze.c (data-only global); sized as a
    // guess and filled with int.MaxValue as a safe (non-clamping) placeholder rather than
    // inventing real limits.
    internal static readonly int[] s_ResolutionDetailLimit = CreateUnknownResolutionLimitTable();

    // Ghidra: DAT_00562e00 â€” recovered from Legacy/Blaze.exe as the dwords
    // { 1, 7, 11, 16, 23, 0 }. These are real per-resolution detail clamps; the table was
    // previously filled with int.MaxValue on the assumption that its content was unrecoverable,
    // which turned the clamp into a silent no-op.
    // Entries 0..4 are confirmed: the installed BLAZE.cfg pairs Resolution=4 with Size=23, and
    // this table's index 4 is 23 â€” the clamp is a no-op there, which is what a settings file
    // written by the game itself must produce.
    // TODO(REVERSE): the trailing 0 is transcribed as read. It is plausibly a terminator rather
    // than a 6th entry meaning "clamp everything to 0"; the bytes that follow (01 02 03 04 05 06
    // 07 08) are visibly a different table, so 0x00562e00 ends here either way. Kept verbatim.
    private static int[] CreateUnknownResolutionLimitTable()
        => [1, 7, 11, 16, 23, 0];

    // Ghidra: &DAT_00564b58, 240 ints (4 groups x 3 categories x 20 actions) â€” key-binding table.
    // Key format reconstructed as "K%01x%01x%02x" (Ghidra: s_K_01x_01x_02x_00564f6c).
    //
    // **Initialisé depuis `.data`, pas à zéro (2026-08-18).** Ce tableau est pré-rempli à l'édition
    // de liens : `Inp.LoadKeyBindings` ne fait qu'écraser les entrées dont la clé existe réellement dans
    // le fichier (`GetPrivateProfileIntA` avec `-1` en repli, retenu seulement s'il diffère de -1).
    // Un `new int[240]` à zéro était donc faux dès le premier lancement sans fichier de
    // configuration : les vingt liaisons **manette** de chaque joueur, qui ont de vraies valeurs par
    // défaut, restaient vides — et `0` y est un numéro de bouton valide, la marque « aucun » étant
    // `-1`.
    //
    // Les 960 octets sont relus directement dans Legacy/Blaze.exe (`.data`, VA 0x00564B58) et
    // correspondent **clé pour clé** aux 240 lignes `[Controls]` du BLAZE.cfg installé — les deux
    // sources se confirment mutuellement. Copie de référence et format détaillé :
    // Documentation/OpenForesia.cfg.default et Documentation/OpenForesia.cfg.md.
    // Construit à la première lecture, et non dans l'initialiseur de champ : les deux tables de
    // valeurs par défaut sont déclarées bien plus bas dans ce fichier, et les initialiseurs
    // statiques C# s'exécutent dans l'ordre de déclaration — les lire ici les trouverait nulles.
    // Même motif paresseux que s_LoadScreenDescriptors_0056b558.
    private static int[] s_KeyBindingsStorage;
    internal static int[] s_KeyBindings => s_KeyBindingsStorage ??= BuildKeyBindings();

    private static int[] BuildKeyBindings()
    {
        var bindings = new int[4 * 3 * 20];

        // Joueur 1 : les deux jeux clavier, puis la manette.
        for (int i = 0; i < 40; i++) bindings[i] = s_KeyBindingDefaults_00564ab8[i];

        // Joueurs 1 à 4 : la table manette, identique pour les quatre. Les tables clavier des
        // joueurs 2 à 4 restent à zéro, comme dans le binaire — aucun clavier ne leur est attribué
        // par défaut.
        for (int group = 0; group < 4; group++)
            for (int i = 0; i < 20; i++)
                bindings[group * 0x3c + 40 + i] = s_JoyBindingDefaults_00564a68[i];

        return bindings;
    }


    /// <summary>
    /// Ghidra: FUN_0050f140 @ 0x0050f140. Loads Blaze.ini (built from the exe's own path with
    /// its extension replaced), applies the sound/CD volume and gamma settings immediately, and
    /// loads the key-binding table.
    /// </summary>
    private static void LoadOptionsIni()
    {
        byte* iniPathAnsi = ToAnsiZ(Aud.GetIniFilePath());

        Mem.s_SoundVolumeIni = Aud.ReadIniInt("SoundVol", 0x40, iniPathAnsi);
        Mem.s_CdVolumeIni = Aud.ReadIniInt("CdVol", 0x40, iniPathAnsi);
        Mem.s_GammaIni = Aud.ReadIniInt("Gamma", 0x40, iniPathAnsi);

        uint filter = Aud.ReadIniInt("Filter", 1, iniPathAnsi);
        uint* texFilterPossible = Aud.GetTexFilterPossiblePtr();
        if (texFilterPossible != null) *texFilterPossible = filter;

        s_SmallTextureFlag = Aud.ReadIniInt("Small", 0, iniPathAnsi);
        Mem.s_SizeSetting_005de160 = (int)Aud.ReadIniInt(SizeIniKey, 0xb, iniPathAnsi);
        s_ResolutionIndex = Aud.ReadIniInt("Resolution", 2, iniPathAnsi);
        s_TimingMode = Aud.ReadIniInt("Timing", 1, iniPathAnsi);

        // ECHAFAUDAGE TEMPORAIRE : trace du fichier d'options rÃ©ellement lu et des valeurs prises.
        DeviceShim.LogDisplayMode("bureau, avant init 3D");
        Console.Error.WriteLine(
            $"[CFG] {Marshal.PtrToStringAnsi((IntPtr)iniPathAnsi)} -> Resolution={s_ResolutionIndex} " +
            $"Size={Mem.s_SizeSetting_005de160} Small={s_SmallTextureFlag} Timing={s_TimingMode}");

        int nextJoystickSlot = 0;
        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            uint enabled = Aud.ReadIniInt($"Joy{i}", 1, iniPathAnsi);
            s_JoystickSlot[i] = enabled == 0 ? -1 : nextJoystickSlot++;
        }

        Aud.ApplyCdVolume();
        Aud.ApplyAllSoundBufferVolumes();
        Winsys3d.CreateTranslationTables(Mem.s_GammaIni * GammaIniScale);

        if (s_ResolutionIndex < (uint)s_ResolutionDetailLimit.Length &&
            s_ResolutionDetailLimit[s_ResolutionIndex] < Mem.s_SizeSetting_005de160)
        {
            Mem.s_SizeSetting_005de160 = s_ResolutionDetailLimit[s_ResolutionIndex];
        }

        Inp.LoadKeyBindings(iniPathAnsi);
        Free(iniPathAnsi);
    }

    /// <summary>
    /// Ghidra: FUN_0050f300 @ 0x0050f300, duplicated by the linker/Ghidra as
    /// thunk_FUN_0050f300 @ 0x0050f520 (identical body â€” both map onto this one method). Mirror
    /// of <see cref="LoadOptionsIni"/>: writes every setting back to the ini file.
    /// </summary>
    internal static void SaveOptionsIni()
    {
        byte* iniPathAnsi = ToAnsiZ(Aud.GetIniFilePath());

        WriteIniInt("SoundVol", Mem.s_SoundVolumeIni, iniPathAnsi);
        WriteIniInt("CdVol", Mem.s_CdVolumeIni, iniPathAnsi);
        WriteIniInt("Gamma", Mem.s_GammaIni, iniPathAnsi);

        uint* texFilterPossible = Aud.GetTexFilterPossiblePtr();
        WriteIniInt("Filter", texFilterPossible != null ? *texFilterPossible : 0, iniPathAnsi);

        WriteIniInt("Small", s_SmallTextureFlag, iniPathAnsi);
        WriteIniInt(SizeIniKey, (uint)Mem.s_SizeSetting_005de160, iniPathAnsi);
        WriteIniInt("Resolution", s_ResolutionIndex, iniPathAnsi);
        WriteIniInt("Timing", s_TimingMode, iniPathAnsi);

        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            WriteIniInt($"Joy{i}", s_JoystickSlot[i] != -1 ? 1u : 0u, iniPathAnsi);
        }

        Inp.SaveKeyBindings(iniPathAnsi);
        Free(iniPathAnsi);
    }


    /// <summary>
    /// Ghidra: FUN_0050f120 @ 0x0050f120. Reads a capability flag off the main device interface
    /// (Ordinal_750) via a vtable call at offset 0x40. TODO(REVERSE): exact meaning of that vtable
    /// slot not confirmed (likely a "hardware texture filtering supported"-style caps query).
    /// </summary>
    private static void CheckDeviceCapabilityFlag()
    {
        void* device = Winsys3d.Ordinal_750();
        if (device == null) { Mem.s_DeviceCapFlag_00564a48 = true; return; }
        void** vtable = *(void***)device;
        var query = (delegate* unmanaged[Stdcall]<void*, int>)vtable[0x10];
        int result = query(device);
        Mem.s_DeviceCapFlag_00564a48 = result == 0;
    }
    // =====================================================================
    // The options page's drawing layer (Ghidra: the FUN_00510xxx family)
    //
    // A second, self-contained UI layer, unrelated to the display-list renderer the main menu
    // uses. It talks to the 3D device **directly**: every widget ends up in FUN_00510500 or
    // FUN_00510330, which build four `D3DTLVERTEX` on the stack and submit them as one
    // `DrawPrimitive(D3DPT_TRIANGLEFAN, D3DVT_TLVERTEX, â€¦, 4, 0)`. The device is the one
    // `Ordinal_750` returns â€” `DAT_011be760`, the same object DeviceShim faces â€” so this whole
    // layer renders through the Direct3D 11 backend without any further plumbing.
    //
    // **On the vertex order, which Ghidra does not give directly.** In both builders the address
    // passed to `DrawPrimitive` disagrees with the one passed to the scaling helper by 16 bytes,
    // so the slot naming cannot be trusted on its own. Grouping the assignments by the 32-byte
    // `TLVERTEX` stride resolves it without guesswork, and the two agree: the array starts at the
    // address the scaling helper receives, and its four vertices are
    // `(x1,y1) (x2,y1) (x2,y2) (x1,y2)` â€” the standard quad fan, with `z` and `rhw` both 0.9f.
    // =====================================================================

    /// <summary>Ghidra: DAT_00578e28 â€” the device, fetched once per use through Ordinal_750.</summary>
    private static void** UiDeviceVtable => *(void***)Winsys3d.Ordinal_750();

    private const int Device_BindTexture = 0x64 / 4;
    private const int Device_AlphaBlendEnable = 0x98 / 4;
    private const int Device_StippleState = 0x9c / 4;
    private const int Device_SrcBlend = 0xa0 / 4;
    private const int Device_DestBlend = 0xa4 / 4;
    private const int Device_DrawPrimitive = 0xbc / 4;
    private const int Device_GetDeviceDesc = 0x4c / 4;

    private static void UiSetState(int slot, uint value)
    {
        var method = (delegate* unmanaged[Thiscall]<void*, uint, void>)UiDeviceVtable[slot];
        method(Winsys3d.Ordinal_750(), value);
    }

    private static void UiDrawFan(TlVertex* vertices)
    {
        var draw = (delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            UiDeviceVtable[Device_DrawPrimitive];
        draw(Winsys3d.Ordinal_750(), 6, 3, vertices, 4, 0);   // D3DPT_TRIANGLEFAN, D3DVT_TLVERTEX
    }

    /// <summary>
    /// Ghidra: FUN_00510110 @ 0x00510110 â€” maps the layer's own 320x240-ish coordinates onto the
    /// current resolution, in place, for <paramref name="count"/> vertices.
    /// <para>
    /// The two offset tables were read out of <c>Legacy/Blaze.exe</c>: X at 0x00566c98 is
    /// <c>{-160, -64, 0, 80, 192}</c> and Y at 0x00566cb0 is <c>{-120, -48, 0, 60, 144}</c>,
    /// indexed by the ini's Resolution. Entry 4 is `(192, 144)` â€” exactly the "base" the
    /// `[SHIFT]` runtime trace reports, which is what confirms the tables are the right ones.
    /// </para>
    /// </summary>
    private static void UiScaleVertices(TlVertex* vertices, int count)
    {
        int scale, offsetX, offsetY;
        if (s_SmallTextureFlag == 0 && Mem.s_SizeSetting_005de160 > 7)
        {
            scale = 2; offsetX = 0; offsetY = 0;
        }
        else
        {
            scale = 1; offsetX = 0xa0; offsetY = 0x78;
        }

        uint resolution = s_ResolutionIndex < (uint)s_UiOffsetX_00566c98.Length ? s_ResolutionIndex : 0;

        for (int i = 0; i < count; i++)
        {
            vertices[i].X = vertices[i].X * scale + s_UiOffsetX_00566c98[resolution] + offsetX;
            vertices[i].Y = vertices[i].Y * scale + s_UiOffsetY_00566cb0[resolution] + offsetY;
        }
    }

    // Ghidra: the tables at 0x00566c98 and 0x00566cb0, read verbatim from Legacy/Blaze.exe.
    private static readonly int[] s_UiOffsetX_00566c98 = [-160, -64, 0, 80, 192];
    private static readonly int[] s_UiOffsetY_00566cb0 = [-120, -48, 0, 60, 144];

    // Ghidra: _DAT_0054d020 = -0.1f and _DAT_0054d024 = 1/256f â€” the half-texel bias and the
    // reciprocal of the atlas width, read from the binary's .rdata.
    private const float UiTexelBias = -0.1f;
    private const float UiTexelScale = 1.0f / 256.0f;

    /// <summary>Ghidra: the 0.9f both `z` and `rhw` carry on every vertex this layer builds.</summary>
    private const float UiDepth = 0.9f;

    /// <summary>
    /// Ghidra: FUN_00510330 @ 0x00510330 â€” a flat filled rectangle, drawn untextured (it binds
    /// texture 0) and alpha-blended.
    /// </summary>
    private static void UiFillRect(int x, int y, int width, int height, uint colour)
    {
        void* deviceDesc = ((delegate* unmanaged[Thiscall]<void*, void*>)
            UiDeviceVtable[Device_GetDeviceDesc])(Winsys3d.Ordinal_750());

        TlVertex* quad = stackalloc TlVertex[4];
        UiQuad(quad, x, y, width, height, colour);
        UiScaleVertices(quad, 4);

        UiSetState(Device_BindTexture, 0);

        // Ghidra: the capability word at deviceDesc + 0x80 chooses between two blend setups â€”
        // bit 0x1000 leaves the state alone, bit 0x2000 raises the 0x9c pair instead.
        uint caps = deviceDesc != null ? *(uint*)((byte*)deviceDesc + 0x80) : 0;
        if ((caps & 0x1000) == 0)
        {
            if ((caps & 0x2000) != 0) UiSetState(Device_StippleState, 1);
        }
        else
        {
            UiSetState(Device_AlphaBlendEnable, 1);
        }

        UiSetState(Device_SrcBlend, 5);     // D3DBLEND_SRCALPHA
        UiSetState(Device_DestBlend, 6);    // D3DBLEND_INVSRCALPHA
        UiDrawFan(quad);
        UiSetState(Device_StippleState, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00510500 @ 0x00510500 â€” one textured quad out of an atlas: the destination
    /// rectangle, the source position and size in texels, the texture object and a modulating
    /// colour. Opaque: it forces alpha off and ONE/ZERO blending.
    /// </summary>
    private static void UiBlit(int x, int y, int width, int height,
                               int sourceX, int sourceY, void* texture,
                               int sourceWidth, int sourceHeight, uint colour)
    {
        TlVertex* quad = stackalloc TlVertex[4];
        UiQuad(quad, x, y, width, height, colour | 0xff000000);

        float u1 = (sourceX - UiTexelBias) * UiTexelScale;
        float v1 = (sourceY - UiTexelBias) * UiTexelScale;
        float u2 = (sourceWidth - UiTexelBias + sourceX) * UiTexelScale;
        float v2 = (sourceHeight - UiTexelBias + sourceY) * UiTexelScale;

        quad[0].U = u1; quad[0].V = v1;
        quad[1].U = u2; quad[1].V = v1;
        quad[2].U = u2; quad[2].V = v2;
        quad[3].U = u1; quad[3].V = v2;

        UiScaleVertices(quad, 4);

        var bind = (delegate* unmanaged[Thiscall]<void*, void*, void>)UiDeviceVtable[Device_BindTexture];
        bind(Winsys3d.Ordinal_750(), texture);

        UiSetState(Device_AlphaBlendEnable, 0);
        UiSetState(Device_SrcBlend, 2);     // D3DBLEND_ONE
        UiSetState(Device_DestBlend, 1);    // D3DBLEND_ZERO
        UiDrawFan(quad);
    }

    /// <summary>Fills the four vertices both builders share, less their texture coordinates.</summary>
    private static void UiQuad(TlVertex* quad, int x, int y, int width, int height, uint colour)
    {
        float x1 = x, y1 = y, x2 = x + width, y2 = y + height;
        quad[0] = new TlVertex { X = x1, Y = y1, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[1] = new TlVertex { X = x2, Y = y1, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[2] = new TlVertex { X = x2, Y = y2, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[3] = new TlVertex { X = x1, Y = y2, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
    }

    /// <summary>
    /// Ghidra: FUN_005108f0 @ 0x005108f0 â€” draws one pre-rendered label strip. The label index
    /// selects both its width (from the runtime-built table at DAT_00589758) and which of four
    /// atlases holds it: 0..0x11 in the first, 0x12..0x23 in the second, 0x24..0x35 in the third,
    /// and negative indices in the fourth, counted backwards. Rows are 14 texels tall.
    /// <paramref name="centred"/> is the original's `param_4 == 1`, which shifts left by half the
    /// label's width.
    /// </summary>
    private static void UiDrawLabel(int x, int y, int label, int centred, uint colour)
    {
        // La table est indexÃ©e des deux cÃ´tÃ©s : les libellÃ©s d'ACTION.TXT ont des identifiants
        // nÃ©gatifs et leurs largeurs sont Ã©crites en descendant sous la base. Un garde qui ramenait
        // ces index Ã  0 donnait Ã  toutes les actions la largeur du libellÃ© 0, ce qui les tronquait
        // toutes Ã  la mÃªme longueur.
        int width = s_UiLabelWidths_00589758[label];
        if (centred == 1) x -= width >> 1;

        if (label < 0)
        {
            UiBlit(x, y, width, 0xe, 0, (-1 - label) * 0xe, s_UiAtlasNegative_00589708,
                   width, 0xe, colour);
            return;
        }

        void* atlas = s_UiAtlas0_00589c6c;
        if (label > 0x11)
        {
            if (label < 0x24) { label -= 0x12; atlas = s_UiAtlas1_00589830; }
            else if (label > 0x35) return;
            else { label -= 0x24; atlas = s_UiAtlas2_00589834; }
        }

        UiBlit(x, y, width, 0xe, 0, label * 0xe, atlas, width, 0xe, colour);
    }

    /// <summary>
    /// Ghidra: FUN_00510740 @ 0x00510740 â€” the page's window frame: a translucent fill, then the
    /// eight pieces of a nine-patch border taken from the 16x16 cells of the frame atlas.
    /// </summary>
    private static void UiDrawFrame(int x, int y, int width, int height)
    {
        void* atlas = s_UiFrameAtlas_00599ddc;
        UiFillRect(x + 8, y + 8, width - 0x10, height - 0x10, 0x7f080437);

        int right = x + width - 0x10;
        int bottom = y + height - 0x10;

        UiBlit(x, y, 0x10, 0x10, 0x40, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x + 0x10, y, width - 0x1f, 0x10, 0, 0, atlas, 0xf, 0x10, 0xffffff);
        UiBlit(right, y, 0x10, 0x10, 0x50, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x, y + 0x10, 0x10, height - 0x1f, 0x20, 0, atlas, 0x10, 0xf, 0xffffff);
        UiBlit(right, y + 0x10, 0x10, height - 0x1f, 0x30, 0, atlas, 0x10, 0xf, 0xffffff);
        UiBlit(x, bottom, 0x10, 0x10, 0x60, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x + 0x10, bottom, width - 0x1f, 0x10, 0x10, 0, atlas, 0xf, 0x10, 0xffffff);
        UiBlit(right, bottom, 0x10, 0x10, 0x70, 0, atlas, 0x10, 0x10, 0xffffff);
    }

    /// <summary>Ghidra: FUN_00510a70 @ 0x00510a70 â€” the selection cursor, one 15x16 cell.</summary>
    private static void UiDrawCursor(int x, int y)
        => UiBlit(x, y, 0xf, 0x10, 0xf0, 0, s_UiFrameAtlas_00599ddc, 0xf, 0x10, 0xffffff);





    /// <summary>
    /// Ghidra: FUN_0050fe90 @ 0x0050fe90 â€” dÃ©but d'image : chacune des huit textures de la page est
    /// interrogÃ©e par son slot <c>[8]</c> (<c>IDirectDrawSurface::IsLost</c> chez 3dhelp) et
    /// recrÃ©Ã©e depuis sa source si elle a Ã©tÃ© perdue.
    /// <para>
    /// Sur notre chemin le slot rend toujours Â« pas perdue Â» â€” une texture Direct3D 11 ne se perd
    /// pas comme une surface DirectDraw â€” donc cette passe ne fait rien. Elle est transcrite
    /// entiÃ¨re malgrÃ© tout : c'est elle qui prouve que le slot 8 existe, et le jour oÃ¹ une perte de
    /// device sera gÃ©rÃ©e, la reconstruction est dÃ©jÃ  en place.
    /// </para>
    /// </summary>
    private static void RestoreLostUiTextures()
    {
        byte* background = s_UiBackgroundFile_00579264;
        byte* frame = s_UiFrameFile_00589838;

        RestoreIfLost(ref s_UiBackgroundTexture_0057926c, s_UiBackgroundInfo_00578e30,
                      background != null ? background + *(int*)(background + 10) : null, null);
        RestoreIfLost(ref s_UiFrameAtlas_00599ddc, s_UiFrameInfo_00589840,
                      frame != null ? frame + *(int*)(frame + 10) : null, s_UiFrameColourKey_005d9df8);

        RestoreIfLost(ref s_UiAtlas0_00589c6c, s_UiTargetInfo_00579278, s_UiAtlas0Bits_00589c80, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlas1_00589830, s_UiTargetInfo_00579278, s_UiAtlas1Bits_005b9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlas2_00589834, s_UiTargetInfo_00579278, s_UiAtlas2Bits_005c9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasNegative_00589708, s_UiTargetInfo_00579278, s_UiAtlasNegativeBits_00579708, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasKeyboard_00589c78, s_UiTargetInfo_00579278, s_UiKeyboardBits_005a9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasJoystick_00599dd8, s_UiTargetInfo_00579278, s_UiAtlasJoystickBits_00599de0, s_UiRestoreColourKey_005d9df8);
    }

    private static void RestoreIfLost(ref void* texture, byte* bitmapInfo, void* bits, uint* colourKey)
    {
        if (texture == null || bits == null) return;

        void** vtable = *(void***)texture;
        int lost = ((delegate* unmanaged[Thiscall]<void*, int>)vtable[8])(texture);
        if (lost == 0) return;

        ((delegate* unmanaged[Thiscall]<void*, uint, int>)vtable[0])(texture, 1);
        texture = UiCreateTexture(bitmapInfo, bits, colourKey);
    }

    /// <summary>
    /// Ghidra: FUN_005101c0 @ 0x005101c0 â€” le fond de page : un seul quad `(0,0)-(320,241)` en
    /// coordonnÃ©es du jeu, texturÃ© par BACKGRND.PIC, opaque, filtrage activÃ© le temps du dessin.
    /// Les coordonnÃ©es de texture sont les constantes du binaire, `0.5/256` et `255/256` en X,
    /// `224.5/256` en Y.
    /// </summary>
    private static void DrawUiBackground()
    {
        TlVertex* quad = stackalloc TlVertex[4];
        const float u1 = 0.001953125f, u2 = 0.99609375f;   // 0x3B000000, 0x3F7F8000
        const float v2 = 0.876953125f;                     // 0x3F608000

        quad[0] = UiBackgroundVertex(0f, 0f, u1, u1);
        quad[1] = UiBackgroundVertex(320f, 0f, u2, u1);
        quad[2] = UiBackgroundVertex(320f, 241f, u2, v2);
        quad[3] = UiBackgroundVertex(0f, 241f, u1, v2);

        // Ghidra perd l'argument du premier appel ; le second est SetTextureFilterMode(0), et le
        // filtrage n'aurait pas de sens laissÃ© actif aprÃ¨s un fond plein Ã©cran.
        Winsys3d.SetTextureFilterMode(1);
        Winsys3d.ShiftVertices(quad, 4);

        var bind = (delegate* unmanaged[Thiscall]<void*, void*, void>)UiDeviceVtable[Device_BindTexture];
        bind(Winsys3d.Ordinal_750(), s_UiBackgroundTexture_0057926c);

        UiSetState(Device_AlphaBlendEnable, 0);
        UiSetState(Device_SrcBlend, 2);     // D3DBLEND_ONE
        UiSetState(Device_DestBlend, 1);    // D3DBLEND_ZERO
        UiDrawFan(quad);
        Winsys3d.SetTextureFilterMode(0);
    }

    private static TlVertex UiBackgroundVertex(float x, float y, float u, float v)
        => new() { X = x, Y = y, Z = UiDepth, Rhw = UiDepth, Diffuse = 0xffffffff, Specular = 0, U = u, V = v };

    /// <summary>
    /// Ghidra: FUN_00510aa0 @ 0x00510aa0 â€” la sÃ©lection. Fait avancer <paramref name="selection"/>
    /// entre <paramref name="min"/> et <paramref name="max"/>, et rend un masque de ce qui a Ã©tÃ©
    /// demandÃ© : 1 valider, 2 gauche, 4 droite, 8 page prÃ©cÃ©dente, 0x10 page suivante, et
    /// <c>0xffffffff</c> pour annuler.
    /// <para>
    /// Deux sources d'entrÃ©e, lues sÃ©parÃ©ment. Le clavier est pris **directement dans le tampon
    /// DirectInput** (`DAT_00604b50`), par scancode : 0xC8 haut, 0xD0 bas, 0xCB gauche, 0xCD
    /// droite, 0x1C entrÃ©e, 0x01 Ã©chap, 0xC9 et 0xD1 page prÃ©cÃ©dente/suivante. La manette passe par
    /// le bloc d'Ã©tat que <c>Inp.PollInputAndAudio</c> dÃ©signe (`DAT_00579270`). La rÃ©pÃ©tition tient en
    /// deux compteurs : 12 images avant la premiÃ¨re rÃ©pÃ©tition, 2 ensuite.
    /// </para>
    /// </summary>
    private static uint UiSelect(int* selection, int min, int max)
    {
        uint result = 0;
        uint held = 0;

        Inp.PollJoystickState();            // Ghidra: FUN_005048f0
        Inp.UpdateButtonEdges();            // Ghidra: FUN_00504a30
        Inp.AssignControllersToSlots();     // Ghidra: FUN_005357a0

        byte* keys = Inp.s_KeyboardState_00604b50;
        if (keys == null) return 0;

        // Ghidra: haut et bas partagent le mÃªme compteur de rÃ©pÃ©tition, et s'excluent â€” le bas
        // n'est mÃªme pas consultÃ© tant que le haut est enfoncÃ©.
        if (keys[0xc8] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0 && *selection > min) { (*selection)--; Aud.PlayUiSound3A(); }
        }
        else if (keys[0xd0] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0 && *selection < max) { (*selection)++; Aud.PlayUiSound3A(); }
        }

        if (keys[0xcb] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0) { result = 2; Aud.PlayUiSound3A(); }
        }

        if (keys[0xcd] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0) { result |= 4; Aud.PlayUiSound3A(); }
        }

        if (held == 0) { s_UiRepeatCounter_005d9e08 = 0; s_UiRepeatActive_005d9e00 = 0; }

        // Les quatre touches Ã  front : entrÃ©e, Ã©chap, page prÃ©cÃ©dente, page suivante. L'Ã©tat
        // prÃ©cÃ©dent tient dans les quatre bits de DAT_005d9e04.
        uint edges = s_UiKeyEdges_005d9e04;
        edges = UiEdge(keys[0x1c] != 0, edges, 1, ref result, 1);
        if (keys[0x01] != 0 && (edges & 2) == 0) result = 0xffffffff;
        edges = keys[0x01] != 0 ? edges | 2 : edges & ~2u;
        edges = UiEdge(keys[0xc9] != 0, edges, 4, ref result, 8);
        edges = UiEdge(keys[0xd1] != 0, edges, 8, ref result, 0x10);
        s_UiKeyEdges_005d9e04 = edges;

        if (s_TraceMenu && (result != 0 || held != 0 || keys[0x01] != 0))
            Console.Error.WriteLine(
                $"[UI] sÃ©lection -> 0x{result:X8} (maintien={held} fronts=0x{edges:X} " +
                $"entrÃ©e={keys[0x1c]:X2} Ã©chap={keys[0x01]:X2})");

        if (held != 0 || edges != 0) return result;

        // La manette, seulement si le clavier ne dit rien.
        byte* pad = Inp.s_Unknown_00579270;
        if (pad == null) return result;

        ushort directions = *(ushort*)(pad + 6);
        if ((directions & 0x10) != 0 && *selection > min) { (*selection)--; Aud.PlayUiSound3A(); }
        else if ((directions & 0x40) != 0 && *selection < max) { (*selection)++; Aud.PlayUiSound3A(); }

        if ((directions & 0x80) != 0) result |= 2;
        if ((directions & 0x20) != 0) result |= 4;

        ushort buttons = *(ushort*)(pad + 2);
        if ((buttons & 0x8000) != 0) result |= 1;
        if ((buttons & 0x0800) != 0) result |= 8;
        if ((buttons & 0x0400) != 0) result |= 0x10;
        if ((buttons & 0x4000) != 0) return 0xffffffff;
        return result;
    }

    /// <summary>Ghidra: le motif de rÃ©pÃ©tition rÃ©pÃ©tÃ© cinq fois dans FUN_00510aa0.</summary>
    private static uint UiRepeat()
    {
        if (s_UiRepeatActive_005d9e00 == 0)
        {
            s_UiRepeatActive_005d9e00 = 1;
            s_UiRepeatCounter_005d9e08 = 0xc;
            return 3;
        }

        s_UiRepeatCounter_005d9e08--;
        if (s_UiRepeatCounter_005d9e08 != 0) return 1;

        s_UiRepeatCounter_005d9e08 = 2;
        return 3;
    }

    /// <summary>Ghidra: le motif de dÃ©tection de front des quatre touches d'action.</summary>
    private static uint UiEdge(bool down, uint edges, uint bit, ref uint result, uint resultBit)
    {
        if (!down) return edges & ~bit;
        if ((edges & bit) == 0) result |= resultBit;
        return edges | bit;
    }

    private static int s_UiRepeatActive_005d9e00;    // Ghidra: DAT_005d9e00
    private static uint s_UiKeyEdges_005d9e04;       // Ghidra: DAT_005d9e04
    private static int s_UiRepeatCounter_005d9e08;   // Ghidra: DAT_005d9e08
    private static int s_UiOptionsSelection_005d9e48;// Ghidra: DAT_005d9e48

    // Ghidra: DAT_005d9df8 â€” la couleur-clÃ© que la restauration rÃ©applique. Comme ses voisines,
    // elle n'est Ã©crite nulle part dans le graphe reconstruit et vaut donc zÃ©ro.
    private static readonly uint* s_UiRestoreColourKey_005d9df8 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));
    private static uint* s_UiFrameColourKey_005d9df8 => s_UiRestoreColourKey_005d9df8;

    // =====================================================================
    // The options page's setup (Ghidra: FUN_0050fab0 and its chain)
    //
    // Everything this page shows is built once, on first entry, out of files on the CD:
    //
    //     DATA/FONT.PIC       the 8-bit font sheet, 31 glyphs per row, cells 10 x 14
    //     DATA/CONFIG.TXT     the option labels          -> three 256x256 atlases
    //     DATA/ACTION.TXT     the action names           -> one atlas, negative label ids
    //     DATA/JOYSTICK.TXT   the joystick button names  -> one atlas
    //     DATA/KEYBOARD.TXT   the key names              -> one atlas, packed into rows
    //     DATA/BACKGRND.PIC   the page background        -> one texture
    //     DATA/FRAMEMOD.PIC   the window frame and cursor sheet
    //
    // The labels are **rasterised in software** â€” glyph by glyph, through wsyskern's DIBCopyBlock,
    // into 256x256 8-bit buffers â€” and only then handed to the device as textures via the texture
    // class's `[2]` slot. That is the path this file wrongly recorded as never taken; see
    // Documentation/DLL/3dhelp.md, correction of 2026-08-14.
    // =====================================================================




    // =====================================================================
    // DEVIATION, mesurÃ©e et bornÃ©e : la lecture des fichiers texte ne passe plus par l'API de
    // tableaux de `wsyskern`.
    //
    // Pourquoi. `LoadArray` **corrompt le tas** dans ce processus, de faÃ§on cumulative avec le
    // nombre de lignes lues : 1 ligne passe, 4 lignes font tomber le processus plus loin, le
    // fichier entier le fait tomber tout de suite â€” et toujours Ã  la premiÃ¨re allocation qui suit,
    // signature d'une corruption causÃ©e ailleurs. Ce qui a Ã©tÃ© Ã©cartÃ© par la mesure, et n'est donc
    // pas Ã  rouvrir :
    //
    //   - l'appel est conforme : `RET 0x18` dans la DLL, donc 6 arguments stdcall, et le sens de
    //     chacun est lu dans `FUN_10004060` (index de dÃ©part, plafond de lignes, lignes Ã  sauter,
    //     drapeaux) ; `ArrayToString` de mÃªme, `RET 0x10` ;
    //   - l'objet est valide : `CreateArray` pose le tag Â« ARAY Â» que le validateur exige, et
    //     `LoadArray` sortirait proprement s'il ne l'Ã©tait pas â€” ce qu'il fait avec un fichier
    //     absent, cas oÃ¹ **toute la mise en place s'exÃ©cute jusqu'au bout** ;
    //   - ce n'est ni la capacitÃ© initiale (0 comme 0x80 Ã©chouent), ni la croissance (le drapeau
    //     Â« ne pas agrandir Â» n'y change rien), ni le chemin (correct, fichier prÃ©sent).
    //
    // Ce que le tableau apporte ici se rÃ©duit Ã  Â« rendre la ligne i d'un fichier texte Â». On le
    // fait donc nous-mÃªmes. C'est la mÃªme nature de dÃ©viation que le remplacement de DirectDraw
    // par Direct3D 11, en beaucoup plus petit, et elle ne touche aucune logique du jeu.
    //
    // `OPENFORESIA_WSYSKERN_ARRAYS=1` rÃ©tablit le chemin d'origine, pour qui voudra reprendre le
    // diagnostic cÃ´tÃ© DLL.
    // =====================================================================

    private sealed class UiTextFile
    {
        internal readonly List<byte[]> Lines = [];
    }

    private static readonly bool s_UseWsyskernArrays =
        Environment.GetEnvironmentVariable("OPENFORESIA_WSYSKERN_ARRAYS") == "1";

    /// <summary>Ghidra: LoadArray â€” lit tout le fichier et le dÃ©coupe en lignes.</summary>
    private static void ReadUiTextFile(UiTextFile file, string name)
    {
        file.Lines.Clear();

        byte* fileName = ToAnsiZ(name);
        byte* pathAnsi = Res.BuildCdPath(fileName);
        // Le tampon fait 64 octets et n'est zÃ©ro que jusqu'au terminateur : couper au premier zÃ©ro,
        // pas Ã  la fin, sinon la queue non initialisÃ©e se retrouve dans le chemin.
        int length = 0;
        while (length < 64 && pathAnsi[length] != 0) length++;
        string path = System.Text.Encoding.Latin1.GetString(pathAnsi, length);
        Free(pathAnsi);
        Free(fileName);

        byte[] bytes;
        try { bytes = System.IO.File.ReadAllBytes(path); }
        catch (System.IO.IOException) { return; }
        catch (UnauthorizedAccessException) { return; }

        int start = 0;
        for (int i = 0; i <= bytes.Length; i++)
        {
            if (i != bytes.Length && bytes[i] != (byte)'\n') continue;

            int end = i;
            if (end > start && bytes[end - 1] == (byte)'\r') end--;
            if (end > start || i != bytes.Length) file.Lines.Add(bytes[start..end]);
            start = i + 1;
        }
    }

    /// <summary>
    /// Ghidra: ArrayToString â€” copie la ligne demandÃ©e dans un tampon ANSI terminÃ© par zÃ©ro. Un
    /// index hors du fichier rend une chaÃ®ne vide, ce que la boucle appelante traite dÃ©jÃ  comme
    /// Â« rien Ã  dessiner Â».
    /// </summary>
    private static void UiLineToBuffer(UiTextFile file, int index, byte* buffer, int size)
    {
        buffer[0] = 0;
        if ((uint)index >= (uint)file.Lines.Count) return;

        byte[] line = file.Lines[index];
        int n = Math.Min(line.Length, size - 1);
        for (int i = 0; i < n; i++) buffer[i] = line[i];
        buffer[n] = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0050f560 @ 0x0050f560 â€” character to glyph index. The five ranges skip the
    /// punctuation gaps of the cp1252 layout the font sheet is laid out in; six accented capitals
    /// and the currency sign all fold onto index 0xa5.
    /// </summary>
    private static int GlyphIndex(int character)
    {
        switch (character)
        {
            case 0x40: case 0x80: case 0x8a: case 0x9a: case 0xc0: case 0xe0:
                character = 0xa5;
                break;
        }

        if (character > 0xe0) return character - 0x46;
        if (character > 0xc0) return character - 0x45;
        if (character > 0xa0) return character - 0x44;
        if (character > 0x60) return character - 0x23;
        if (character > 0x40) return character - 0x22;
        return character - 0x21;
    }

    /// <summary>
    /// Ghidra: FUN_0050f770 @ 0x0050f770 â€” the width a string will occupy, in the same terms
    /// FUN_0050f680 advances by: a space is 4, anything else its glyph's width, control characters
    /// nothing.
    /// </summary>
    private static int MeasureLabel(byte* text)
    {
        int width = 0;
        for (int i = 0; text[i] != 0; i++)
        {
            byte c = text[i];
            // Ghidra rend le caractÃ¨re en `char` **signÃ©**, ce qui est un artefact : `FUN_0050f560`
            // compare son paramÃ¨tre Ã  0x80, 0x8a, 0x9a, 0xc0 et 0xe0, valeurs qu'un char signÃ© ne
            // peut jamais prendre. Le test des caractÃ¨res de contrÃ´le, lui, est bien signÃ© dans
            // l'original â€” les deux formulations y sont Ã©quivalentes.
            if ((sbyte)c is > 0 and < 0x20) continue;
            if (c == ' ') { width += 4; continue; }

            int glyph = GlyphIndex(c);
            if ((uint)glyph >= (uint)Ui.s_GlyphWidths_0056e7b0.Length) continue;
            width += Ui.s_GlyphWidths_0056e7b0[glyph];
        }
        return width;
    }

    /// <summary>
    /// Ghidra: FUN_0050f680 @ 0x0050f680 â€” draws one string into a DIB at row
    /// <paramref name="row"/> (rows are 14 texels tall), starting at <paramref name="x"/>, and
    /// returns the pen position it ended at. Each glyph is one DIBCopyBlock out of the font sheet,
    /// whose cells are 10 wide and 14 tall, 31 to a row.
    /// </summary>
    private static int RasteriseLabel(void* destinationBits, void* destinationInfo,
                                      int row, byte* text, int x)
    {
        byte* font = s_UiFontFile_00589c74;
        if (font == null) return x;

        void* fontBits = font + *(int*)(font + 10);   // BITMAPFILEHEADER::bfOffBits
        void* fontInfo = font + 0xe;                  // the BITMAPINFO that follows it

        for (int i = 0; text[i] != 0; i++)
        {
            byte c = text[i];
            if ((sbyte)c is > 0 and < 0x20) continue;

            if (c == ' ') { x += 4; continue; }

            int glyph = GlyphIndex(c);
            if ((uint)glyph >= (uint)Ui.s_GlyphWidths_0056e7b0.Length)
            {
                UiStage($"    glyphe hors table : car=0x{(byte)c:X2} index={glyph} " +
                        $"(table={Ui.s_GlyphWidths_0056e7b0.Length})");
                continue;
            }

            int width = Ui.s_GlyphWidths_0056e7b0[glyph];

            // GARDE-FOU, et instrument. La cible fait 256x256 en 8 bits, soit exactement les
            // 64 Kio du tampon. `DIBCopyBlock` ne clippe pas de maniÃ¨re sÃ»re : un blit qui sort
            // Ã©crit hors du tampon et corrompt le tas â€” le symptÃ´me observÃ©. On refuse le blit et
            // on le signale, ce qui dit du mÃªme coup s'il arrive vraiment.
            if (x < 0 || row < 0 || x + width > 0x100 || row * 0xe + 0xe > 0x100)
            {
                UiStage($"    blit REFUSE hors cible : ({x},{row * 0xe}) l={width} " +
                        $"car=0x{(byte)c:X2} glyphe={glyph}");
                x += width;
                continue;
            }

            if (s_TraceGlyphs)
                UiStage($"    blit car=0x{(byte)c:X2} glyphe={glyph} l={width} -> ({x},{row * 0xe}) " +
                        $"source=({(glyph % 0x1f) * 10},{(glyph / 0x1f) * 0xe})");
            Wsyskern.DIBCopyBlock(destinationBits, destinationInfo, x, row * 0xe, (uint)width, 0xe,
                         fontBits, fontInfo, (glyph % 0x1f) * 10, (glyph / 0x1f) * 0xe);
            x += width;
        }

        return x;
    }

    /// <summary>
    /// Ghidra: FUN_0050f9b0 @ 0x0050f9b0 â€” the key names, which unlike the other label sets do not
    /// get one row each: they are packed left to right and wrapped at 253 texels, each entry's
    /// (x, row, width) recorded in the placement table at DAT_00599c80.
    /// </summary>
    private static void RasteriseKeyLabels(UiTextFile array)
    {
        NativeMemory.Fill(s_UiKeyboardBits_005a9de0, UiAtlasBytes, 0);

        byte* placement = s_UiKeyPlacement_00599c80;
        byte* text = stackalloc byte[0x20];
        int x = 0, row = 0;

        for (int i = 0; i < UiKeyLabelCount; i++, placement += 4)
        {
            UiLineToBuffer(array, i, text, 0x20);
            int width = MeasureLabel(text);

            if (width + x > 0xfd) { x = 0; row++; }

            placement[0] = (byte)x;
            placement[1] = (byte)row;
            *(short*)(placement + 2) = (short)width;

            RasteriseLabel(s_UiKeyboardBits_005a9de0, s_UiTargetInfo_00579278, row, text, x);
            x += width;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050f7d0 @ 0x0050f7d0 â€” the two .PIC-backed textures. Both bitmaps' headers are
    /// copied out of the loaded file, their 256-entry palettes passed through wsyskern's
    /// <c>Gamma</c> at the ini's gamma setting, and the result handed to a texture object.
    /// </summary>
    private static void BuildPictureTextures()
    {
        byte* background = s_UiBackgroundFile_00579264;
        byte* frame = s_UiFrameFile_00589838;
        if (background == null || frame == null) return;

        // Ghidra: 10 dwords â€” the BITMAPINFOHEADER alone; the palette that follows is rebuilt.
        NativeMemory.Copy(background + 0xe, s_UiBackgroundInfo_00578e30, 40);
        NativeMemory.Copy(frame + 0xe, s_UiFrameInfo_00589840, 40);

        double gamma = (float)Mem.s_Unknown_005d9de0 * Input.s_GammaScale_0054d000;

        // Ghidra: the palettes start at file + 0x36 (0x0e + 0x28) and are walked as B, G, R with
        // the fourth byte forced to zero. The three reads are at +0x36/+0x37/+0x38 of the file,
        // which is that same triple.
        for (int i = 0; i < 0x400; i += 4)
        {
            s_UiBackgroundInfo_00578e30[0x28 + i + 2] = Wsyskern.Gamma(background[0x38 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 1] = Wsyskern.Gamma(background[0x37 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 0] = Wsyskern.Gamma(background[0x36 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 3] = 0;

            s_UiFrameInfo_00589840[0x28 + i + 2] = Wsyskern.Gamma(frame[0x38 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 1] = Wsyskern.Gamma(frame[0x37 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 0] = Wsyskern.Gamma(frame[0x36 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 3] = 0;
        }

        s_UiBackgroundTexture_0057926c = UiCreateTexture(
            s_UiBackgroundInfo_00578e30, background + *(int*)(background + 10), null);
        s_UiFrameAtlas_00599ddc = UiCreateTexture(
            s_UiFrameInfo_00589840, frame + *(int*)(frame + 10), s_UiFrameColourKey_005d9de8);
    }

    /// <summary>
    /// Ghidra: the three-call idiom this page repeats for every texture â€” create through the
    /// device's `+0x10` factory, load through the texture class's `[2]` slot, and, when it has one,
    /// set a colour key through `[6]`.
    /// </summary>
    private static void* UiCreateTexture(byte* bitmapInfo, void* bits, uint* colourKey)
    {
        void* device = Winsys3d.Ordinal_750();
        void* texture = ((delegate* unmanaged[Thiscall]<void*, void*>)UiDeviceVtable[0x10 / 4])(device);
        if (texture == null) return null;

        void** vtable = *(void***)texture;
        ((delegate* unmanaged[Thiscall]<void*, void*, void*, int>)vtable[2])(texture, bitmapInfo, bits);
        if (colourKey != null)
            ((delegate* unmanaged[Thiscall]<void*, void*, int>)vtable[6])(texture, colourKey);
        return texture;
    }

    /// <summary>
    /// Ghidra: FUN_0050fab0 @ 0x0050fab0 â€” the options page's one-time setup, guarded by
    /// DAT_005d9de4 so it runs on first entry only, followed by the texture creation that runs
    /// every time (the gamma may have changed).
    /// </summary>
    private static void OptionsSetup()
    {
        if (s_UiSetupDone_005d9de4 == 0)
        {
            s_UiSetupDone_005d9de4 = 1;

            s_UiBackgroundFile_00579264 = LoadUiFile(s_UiFileBackground, silent: true);
            s_UiFrameFile_00589838 = LoadUiFile(s_UiFileFrame, silent: true);
            UiStage($"BACKGRND.PIC=0x{(nint)s_UiBackgroundFile_00579264:X8} " +
                    $"FRAMEMOD.PIC=0x{(nint)s_UiFrameFile_00589838:X8}");

            UiStage("lecture CONFIG.TXT");
            var array = new UiTextFile();
            ReadUiTextFile(array, s_UiFileConfigText);
            UiStage($"  {array.Lines.Count} lignes");

            UiStage("chargement FONT.PIC");
            s_UiFontFile_00589c74 = LoadUiFile(s_UiFileFont, silent: true);

            // Ghidra: 0x10a dwords out of the font file at +0xe â€” a BITMAPINFOHEADER plus its
            // 256-entry palette, 1064 bytes â€” reused as the header of every rasterisation target,
            // with the dimensions forced to 256x256. Measured on the disc's FONT.PIC: 312x84, 8
            // bits, so the copy covers a real 256-colour palette and the target is exactly the
            // 64 KiB the buffers provide.
            if (s_UiFontFile_00589c74 != null)
                NativeMemory.Copy(s_UiFontFile_00589c74 + 0xe, s_UiTargetInfo_00579278, 0x10a * 4);

            *(int*)(s_UiTargetInfo_00579278 + 4) = 0x100;    // biWidth
            *(int*)(s_UiTargetInfo_00579278 + 8) = 0x100;    // biHeight, positive: bottom-up

            UiStage("rastÃ©risation CONFIG.TXT (3 atlas)");
            RasteriseLabelSet(array, s_UiAtlas0Bits_00589c80, 0, 0, 0x12);
            RasteriseLabelSet(array, s_UiAtlas1Bits_005b9de0, 0x12, 0x12, 0x12);
            RasteriseLabelSet(array, s_UiAtlas2Bits_005c9de0, 0x24, 0x24, 0x12);

            // Ghidra: ACTION.TXT's widths are written **downward** from DAT_00589754, which is what
            // makes FUN_005108f0's negative label ids resolve.
            UiStage("rastÃ©risation ACTION.TXT");
            ReadUiTextFile(array, s_UiFileActionText);
            NativeMemory.Fill(s_UiAtlasNegativeBits_00579708, UiAtlasBytes, 0);
            byte* text = stackalloc byte[0x20];
            for (int i = 0; i < 0x12; i++)
            {
                UiLineToBuffer(array, i, text, 0x20);
                s_UiLabelWidths_00589758[-1 - i] =
                    RasteriseLabel(s_UiAtlasNegativeBits_00579708, s_UiTargetInfo_00579278, i, text, 0);
            }

            // Ghidra: the joystick set is the only one drawn twice â€” 0x24 entries folded onto 0x12
            // rows, the second half offset by 0x80. Its widths are not recorded.
            UiStage("rastÃ©risation JOYSTICK.TXT");
            ReadUiTextFile(array, s_UiFileJoystickText);
            NativeMemory.Fill(s_UiAtlasJoystickBits_00599de0, UiAtlasBytes, 0);
            for (int i = 0; i < 0x24; i++)
            {
                UiLineToBuffer(array, i, text, 0x20);
                RasteriseLabel(s_UiAtlasJoystickBits_00599de0, s_UiTargetInfo_00579278,
                               i % 0x12, text, i < 0x12 ? 0 : 0x80);
            }

            UiStage("rastÃ©risation KEYBOARD.TXT");
            ReadUiTextFile(array, s_UiFileKeyboardText);
            RasteriseKeyLabels(array);
        }

        UiStage("textures BACKGRND/FRAMEMOD");
        BuildPictureTextures();
        UiStage("textures des atlas");

        s_UiAtlas0_00589c6c = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas0Bits_00589c80, s_UiAtlasColourKey_005d9df0);
        s_UiAtlas1_00589830 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas1Bits_005b9de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlas2_00589834 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas2Bits_005c9de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasNegative_00589708 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlasNegativeBits_00579708, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasJoystick_00599dd8 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlasJoystickBits_00599de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasKeyboard_00589c78 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiKeyboardBits_005a9de0, s_UiAtlasColourKey_005d9df0);
    }

    /// <summary>One of the three CONFIG.TXT label runs: clear the atlas, draw 18 rows, record 18 widths.</summary>
    private static void RasteriseLabelSet(UiTextFile array, void* bits, int firstEntry, int firstWidth, int count)
    {
        NativeMemory.Fill(bits, UiAtlasBytes, 0);
        byte* text = stackalloc byte[0x20];

        for (int i = 0; i < count; i++)
        {
            UiLineToBuffer(array, firstEntry + i, text, 0x20);
            s_UiLabelWidths_00589758[firstWidth + i] =
                RasteriseLabel(bits, s_UiTargetInfo_00579278, i, text, 0);
        }
    }

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE. Jalonne la mise en place : le tas s'est corrompu quelque part dans
    /// cette chaÃ®ne au premier essai, et une corruption de tas ne lÃ¨ve pas lÃ  oÃ¹ elle est causÃ©e.
    /// La derniÃ¨re Ã©tape journalisÃ©e avant la mort nomme la coupable.
    /// </summary>
    private static void UiStage(string stage)
    {
        if (s_RunOptionsSetup) Console.Error.WriteLine($"[UI] {stage}");
    }

    /// <summary>ECHAFAUDAGE : entrÃ©e et sortie de chaque page, sous OPENFORESIA_TRACE_MENU=1.</summary>
    private static void UiPageTrace(string page)
    {
        if (s_TraceMenu) Console.Error.WriteLine($"[UI] {page}");
    }

    /// <summary>Ghidra: FUN_0050fa50 â€” the CD path, then LoadBitmapFile in its silent mode.</summary>
    private static byte* LoadUiFile(string name, bool silent)
    {
        byte* fileName = ToAnsiZ(name);
        byte* path = Res.BuildCdPath(fileName);
        byte* file = Wsyskern.LoadBitmapFile(path, silent ? 0u : 1u);
        Free(path);
        Free(fileName);
        return file;
    }

    private static void LoadUiArray(void* array, string name)
    {
        byte* fileName = ToAnsiZ(name);
        byte* path = Res.BuildCdPath(fileName);
        // ECHAFAUDAGE : OPENFORESIA_OPTIONS_BADPATH=1 remplace le chemin par un fichier absent,
        // ce qui fait sortir le chargeur juste aprÃ¨s l'ouverture ratÃ©e. SÃ©pare Â« la faute est dans
        // l'ouverture Â» de Â« la faute est dans la lecture des lignes Â».
        if (Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_BADPATH") == "1")
        {
            byte* absent = ToAnsiZ("Z:/pas-de-fichier.txt");
            UiStage("  Wsyskern.LoadArray(chemin absent)");
            Wsyskern.LoadArray(array, absent, 0, 0, 0, 0);
            UiStage("  LoadArray revenu (chemin absent)");
            Free(absent);
            Free(path);
            Free(fileName);
            return;
        }

        // ECHAFAUDAGE : OPENFORESIA_OPTIONS_NOGROW=1 met le drapeau 8 (Â« ne pas agrandir Â»), ce qui
        // court-circuite l'appel de croissance du chargeur. SÃ©pare Â« la croissance corrompt Â» de
        // Â« la lecture/stockage des lignes corrompt Â».
        int flags = Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_NOGROW") == "1" ? 8 : 0;

        // ECHAFAUDAGE : le 4e argument du chargeur est un **plafond de lignes** (0 = illimitÃ©).
        // OPENFORESIA_OPTIONS_MAXLINES borne la lecture, pour trouver Ã  quelle ligne Ã§a casse.
        _ = int.TryParse(Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_MAXLINES"), out int maxLines);

        UiStage($"  Wsyskern.LoadArray(\"{System.Text.Encoding.Latin1.GetString(path, 40).TrimEnd('\0', ' ')}\") lignes<={maxLines} drapeaux={flags}");
        Wsyskern.LoadArray(array, path, 0, maxLines, 0, flags);
        UiStage("  LoadArray revenu");
        Free(path);
        Free(fileName);
        UiStage("  libÃ©rÃ©");
    }

    // Ghidra: the seven file names at 0x005630c8..0x00563134, read verbatim from Legacy/Blaze.exe â€”
    // forward slashes, like every other CD path this reconstruction builds.
    private const string s_UiFileKeyboardText = "DATA/KEYBOARD.TXT";
    private const string s_UiFileJoystickText = "DATA/JOYSTICK.TXT";
    private const string s_UiFileActionText = "DATA/ACTION.TXT";
    private const string s_UiFileFont = "DATA/FONT.PIC";
    private const string s_UiFileConfigText = "DATA/CONFIG.TXT";
    private const string s_UiFileFrame = "DATA/FRAMEMOD.PIC";
    private const string s_UiFileBackground = "DATA/BACKGRND.PIC";

    /// <summary>Ghidra: the 0x4000-dword clears â€” 64 KB, one 256x256 8-bit page.</summary>
    private const nuint UiAtlasBytes = 0x4000 * 4;

    /// <summary>
    /// Ghidra: la borne de boucle de FUN_0050f9b0 â€” de `0x599c81` Ã  `0x599dd6` par pas de 4, soit
    /// **86** entrÃ©es. Les 85 premiÃ¨res sont les noms de touches de KEYBOARD.TXT ; la 86áµ‰ est
    /// l'invite que l'Ã©diteur affiche Ã  la place d'une touche pendant la capture.
    /// </summary>
    private const int UiKeyLabelCount = 0x56;

    private static int s_UiSetupDone_005d9de4;                  // Ghidra: DAT_005d9de4
    private static byte* s_UiBackgroundFile_00579264;           // Ghidra: DAT_00579264
    private static byte* s_UiFrameFile_00589838;                // Ghidra: DAT_00589838
    private static byte* s_UiFontFile_00589c74;                 // Ghidra: DAT_00589c74

    private static readonly byte* s_UiTargetInfo_00579278 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);
    private static readonly byte* s_UiBackgroundInfo_00578e30 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);
    private static readonly byte* s_UiFrameInfo_00589840 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);

    private static readonly void* s_UiAtlas0Bits_00589c80 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlas1Bits_005b9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlas2Bits_005c9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlasNegativeBits_00579708 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlasJoystickBits_00599de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiKeyboardBits_005a9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly byte* s_UiKeyPlacement_00599c80 = (byte*)NativeMemory.AllocZeroed(UiKeyLabelCount * 4 + 8);

    // Ghidra: DAT_005d9de8 and DAT_005d9df0 â€” the two colour keys, DDCOLORKEY pairs of palette
    // indices. TODO(REVERSE): neither is written anywhere in the reconstructed graph and both read
    // as uninitialised data in the binary, so they are zero â€” index 0 transparent, which is the
    // usual convention for these sheets but is not confirmed.
    private static readonly uint* s_UiFrameColourKey_005d9de8 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));
    private static readonly uint* s_UiAtlasColourKey_005d9df0 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));

    // Ghidra: &DAT_0056e7b0, the glyph widths, is **already reconstructed** further up this file
    // for the UI text engine. A second copy transcribed here from the same hex dump carried one
    // 0x06 too many â€” the duplicate-symbol error is what caught it. One table, one definition.

    // Ghidra: the texture objects this layer draws from, and the label-width table. All are built
    // at runtime by FUN_0050fab0 â€” 0x00589758 reads as uninitialised data in the binary, which is
    // an answer rather than a gap: nothing but code can fill it.
    private static void* s_UiAtlas0_00589c6c;
    private static void* s_UiAtlas1_00589830;
    private static void* s_UiAtlas2_00589834;
    private static void* s_UiAtlasNegative_00589708;
    private static void* s_UiAtlasJoystick_00599dd8;
    private static void* s_UiAtlasKeyboard_00589c78;
    private static void* s_UiBackgroundTexture_0057926c;
    private static void* s_UiFrameAtlas_00599ddc;

    /// <summary>
    /// Ghidra: &amp;DAT_00589758 â€” the label widths, **indexed on both sides of that address**.
    /// FUN_0050fab0 writes three contiguous 18-entry runs upward from it (0x00589758, 0x005897a0,
    /// 0x005897e8) for label ids 0..0x35, and a fourth run **downward** from 0x00589754 for the
    /// ACTION.TXT labels, which FUN_005108f0 reaches with negative ids. One block with the base
    /// pointer placed inside it reproduces that exactly; two arrays would not.
    /// </summary>
    private static readonly int* s_UiLabelWidths_00589758 =
        (int*)NativeMemory.AllocZeroed(0x100, sizeof(int)) + 0x20;

    // ---------------------------------------------------------------------------------------
    // Deliberately deferred. Each has a decompiled body in Blaze.c and is ordinary uncovered
    // code, not blocked work; they are stubbed so the frame structure above can be exercised and
    // the runtime can name the next target. See the change proposal for why.
    // ---------------------------------------------------------------------------------------


    /// <summary>
    /// Ghidra: FUN_00512320 @ 0x00512320 — la page d'options : quatre entrées, dont la quatrième
    /// quitte le jeu par une exception logicielle, et Échap qui libère les huit textures.
    ///
    /// <para><b>Renommée le 2026-08-18.</b> Elle s'appelait <c>OptionsScreenStub</c> et sa note
    /// disait « the page's own loop is not [reconstructed] » — les deux dataient du premier jet, où
    /// seule la mise en place (<c>FUN_0050fab0</c>) était transcrite. La boucle l'est depuis
    /// longtemps, ainsi que les quatre sous-pages qu'elle ouvre. Un nom en « Stub » sur du code
    /// complet coûte plus cher qu'il n'y paraît : il envoie chercher un trou qui n'existe plus.</para>
    /// </summary>
    internal static void OptionsPage()
    {
        OptionsSetup();     // Ghidra: FUN_0050fab0

        // Ghidra: la boucle de page. Quatre entrÃ©es, sÃ©lectionnÃ©es entre 0 et 3 ; la quatriÃ¨me
        // quitte par une exception logicielle, Ã©chap quitte en libÃ©rant les huit textures.
        while (true)
        {
            RestoreLostUiTextures();                                    // FUN_0050fe90
            UiBeginScene();                                             // (*device + 0xb0)
            DrawUiBackground();                                         // FUN_005101c0

            UiDrawFrame(0x4c, 0x20, 0xa8, 100);                         // FUN_00510740
            UiDrawLabel(0xa0, 0x32, 0, 1, 0xffffff);                    // FUN_005108f0
            UiDrawLabel(0x5c, 0x48, 1, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 2, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x60, 3, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x6c, 0x10, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiOptionsSelection_005d9e48 + 6) * 0xc);   // FUN_00510a70

            UiEndScene();                                               // (*device + 0xb4)
            UiPresent();                                                // (*device + 0xb8)

            uint action;
            fixed (int* selection = &s_UiOptionsSelection_005d9e48)
                action = UiSelect(selection, 0, 3);                     // FUN_00510aa0

            if (action == 0xffffffff)
            {
                ReleaseUiTextures();
                return;
            }

            if ((action & 1) != 0)
            {
                switch (s_UiOptionsSelection_005d9e48)
                {
                    case 0: Aud.PlayUiSound38(); DisplayOptionsPage(); break;    // FUN_005110b0
                    case 1: Aud.PlayUiSound38(); SoundOptionsPage(); break;      // FUN_005113d0
                    case 2: Aud.PlayUiSound38(); ControlOptionsPage(); break;    // FUN_00512220
                    case 3:
                        UiPageTrace("Quitter le jeu");
                        Aud.PlayUiSound38();
                        // Ghidra: RaiseQuitException() â€” la sortie du jeu depuis
                        // la page d'options, la mÃªme exception logicielle que le lecteur de vidÃ©o
                        // utilise pour WM_QUIT.
                        RaiseQuitException();
                        break;
                }
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: les trois slots de scÃ¨ne que la page emprunte, sur le mÃªme device.</summary>
    private static void UiBeginScene()
        => ((delegate* unmanaged[Thiscall]<void*, void>)UiDeviceVtable[0xb0 / 4])(Winsys3d.Ordinal_750());

    private static void UiEndScene()
        => ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xb4 / 4])(Winsys3d.Ordinal_750());

    private static void UiPresent()
        => ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xb8 / 4])(Winsys3d.Ordinal_750());

    /// <summary>
    /// Ghidra: la sortie par Ã©chap libÃ¨re les huit textures de la page â€” chacune par le slot
    /// <c>[0]</c> de sa classe, avec le drapeau 1 des destructeurs C++ de l'Ã©poque.
    /// </summary>
    private static void ReleaseUiTextures()
    {
        ReleaseUiTexture(ref s_UiAtlas0_00589c6c);
        ReleaseUiTexture(ref s_UiAtlas1_00589830);
        ReleaseUiTexture(ref s_UiAtlasJoystick_00599dd8);
        ReleaseUiTexture(ref s_UiBackgroundTexture_0057926c);
        ReleaseUiTexture(ref s_UiAtlas2_00589834);
        ReleaseUiTexture(ref s_UiAtlasNegative_00589708);
        ReleaseUiTexture(ref s_UiFrameAtlas_00599ddc);
        ReleaseUiTexture(ref s_UiAtlasKeyboard_00589c78);
    }

    private static void ReleaseUiTexture(ref void* texture)
    {
        if (texture == null) return;
        ((delegate* unmanaged[Thiscall]<void*, uint, int>)(*(void***)texture)[0])(texture, 1);
        texture = null;
    }

    /// <summary>
    /// Ghidra: FUN_00510a00 @ 0x00510a00 â€” une jauge : la glissiÃ¨re (0x86 x 10, prise en
    /// <c>(0, 0x20)</c> de la planche du cadre) puis le curseur (8 x 10, pris en <c>(0x8e, 0x20)</c>)
    /// posÃ© Ã  <c>x + valeur + 1</c>.
    /// </summary>
    private static void UiDrawGauge(int x, int y, int value)
    {
        UiBlit(x, y, 0x86, 10, 0, 0x20, s_UiFrameAtlas_00599ddc, 0x86, 10, 0xffffff);
        UiBlit(x + value + 1, y, 8, 10, 0x8e, 0x20, s_UiFrameAtlas_00599ddc, 8, 10, 0xffffff);
    }

    /// <summary>
    /// Ghidra: FUN_0050f980 @ 0x0050f980 â€” rÃ©applique le gamma : dÃ©truit les deux textures issues
    /// des `.PIC` et les refabrique, palettes repassÃ©es dans <c>Gamma</c> au nouveau rÃ©glage.
    /// </summary>
    private static void RebuildGammaTextures()
    {
        ReleaseUiTexture(ref s_UiBackgroundTexture_0057926c);
        ReleaseUiTexture(ref s_UiFrameAtlas_00599ddc);
        BuildPictureTextures();     // Ghidra: FUN_0050f7d0
    }

    /// <summary>
    /// Ghidra: FUN_005110b0 @ 0x005110b0 â€” la page Â« Affichage Â». Cinq rÃ©glages, six quand le
    /// device annonce la capacitÃ© que son slot <c>+0xE0</c> rapporte : taille de l'Ã©cran, gamma,
    /// filtrage de texture, affichage de statut, puis deux pages filles.
    /// </summary>
    private static void DisplayOptionsPage()
    {
        UiPageTrace("entrée page Affichage");
        // Ghidra: le slot +0xE0 dÃ©cide si la sixiÃ¨me ligne existe. Non portÃ© par le device
        // synthÃ©tique, il rend zÃ©ro â€” donc cinq lignes, ce qui est le cas nominal.
        int extra = ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xe0 / 4])(Winsys3d.Ordinal_750()) != 0 ? 1 : 0;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, extra * 0xc + 0x88);
            UiDrawLabel(0xa0, 0x32, 4, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 5, 0, 0xffffff);
            UiDrawGauge(100, 0x55, (int)(Mem.s_SizeSetting_005de160 * 0x79) / UiDetailLimit + 4);
            UiDrawLabel(0x5c, 0x60, 7, 0, 0xffffff);
            UiDrawGauge(100, 0x6d, Mem.s_Unknown_005d9de0 - 8);
            UiDrawLabel(0x5c, 0x78, 9 - (*Aud.GetTexFilterPossiblePtr() != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawLabel(0x5c, 0x84, 0xb - (s_SmallTextureFlag != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawLabel(0x5c, 0x90, 0x25, 0, 0xffffff);
            if (extra != 0) UiDrawLabel(0x5c, 0x9c, 0x24, 0, 0xffffff);

            // Ghidra: la position du curseur saute une ligne au-delÃ  de l'entrÃ©e 1 â€” les deux
            // premiÃ¨res portent une jauge, donc occupent deux crans chacune.
            int line = (s_UiDisplaySelection_005d9e10 > 1 ? 1 : 0) + 6 + s_UiDisplaySelection_005d9e10
                       + (s_UiDisplaySelection_005d9e10 > 0 ? 1 : 0);
            UiDrawCursor(0x4b, line * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiDisplaySelection_005d9e10)
                action = UiSelect(selection, 0, 4 + extra);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 4) TimingPage();        // FUN_00510d40
                if (s_UiDisplaySelection_005d9e10 == 5) ResolutionPage();    // FUN_00510e60
            }

            if ((action & 7) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 2)
                {
                    Aud.PlayUiSound38();
                    uint* filter = Aud.GetTexFilterPossiblePtr();
                    *filter = *filter == 0 ? 1u : 0u;
                }
                else if (s_UiDisplaySelection_005d9e10 == 3 && Mem.s_SizeSetting_005de160 > 6)
                {
                    Aud.PlayUiSound38();
                    s_SmallTextureFlag = s_SmallTextureFlag == 0 ? 1u : 0u;
                }
            }

            if ((action & 2) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 0)
                {
                    if (Mem.s_SizeSetting_005de160 != 0)
                    {
                        Aud.PlayUiSound3A();
                        Mem.s_SizeSetting_005de160--;
                        Winsys3d.SetShift((int)((0xb - Mem.s_SizeSetting_005de160) * 0x10),
                                 (int)((0xb - Mem.s_SizeSetting_005de160) * 0xc),
                                 (int)Mem.s_SizeSetting_005de160, 0);
                        if (Mem.s_SizeSetting_005de160 < 8) s_SmallTextureFlag = 1;
                    }
                }
                else if (s_UiDisplaySelection_005d9e10 == 1 && Mem.s_Unknown_005d9de0 > 8)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_GammaIni -= 8;
                    RebuildGammaTextures();
                }
            }

            if ((action & 4) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 0)
                {
                    if (Mem.s_SizeSetting_005de160 < UiDetailLimit)
                    {
                        Aud.PlayUiSound3A();
                        Mem.s_SizeSetting_005de160++;
                        Winsys3d.SetShift((int)((0xb - Mem.s_SizeSetting_005de160) * 0x10),
                                 (int)((0xb - Mem.s_SizeSetting_005de160) * 0xc),
                                 (int)Mem.s_SizeSetting_005de160, 0);
                    }
                }
                else if (s_UiDisplaySelection_005d9e10 == 1 && Mem.s_Unknown_005d9de0 < 0x88)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_GammaIni += 8;
                    RebuildGammaTextures();
                }
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: `*(int *)(&DAT_00562e00 + DAT_00566cc4 * 4)` â€” le plafond de dÃ©tail de la rÃ©solution courante.</summary>
    private static int UiDetailLimit
        => s_ResolutionIndex < (uint)s_ResolutionDetailLimit.Length
            ? s_ResolutionDetailLimit[s_ResolutionIndex]
            : 1;

    /// <summary>
    /// Ghidra: FUN_005113d0 @ 0x005113d0 â€” la page Â« Son Â» : volume CD, volume des effets, et
    /// l'interrupteur de musique. Les deux volumes bougent par pas de 8 entre 0 et 0x80, et chaque
    /// changement est appliquÃ© immÃ©diatement.
    /// </summary>
    private static void SoundOptionsPage()
    {
        UiPageTrace("entrée page Son");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x70);
            UiDrawLabel(0xa0, 0x32, 0xc, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0xd, 0, 0xffffff);
            UiDrawGauge(100, 0x55, (int)Mem.s_CdVolumeIni);
            UiDrawLabel(0x5c, 0x60, 0xe, 0, 0xffffff);
            UiDrawGauge(100, 0x6d, (int)Mem.s_SoundVolumeIni);
            UiDrawLabel(0x5c, 0x78, 0x28 - (Aud.s_MusicEnabled != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiSoundSelection_005d9e14 + 3) * 0x18);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiSoundSelection_005d9e14)
                action = UiSelect(selection, 0, 2);

            if (action == 0xffffffff) return;

            if ((action & 7) != 0 && s_UiSoundSelection_005d9e14 == 2)
            {
                Aud.PlayUiSound38();
                Aud.ToggleMusicEnabled();       // Ghidra: FUN_00513860
            }

            if ((action & 2) != 0)
            {
                if (s_UiSoundSelection_005d9e14 == 0 && Mem.s_CdVolumeIni != 0)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_CdVolumeIni -= 8;
                    Aud.ApplyCdVolume();
                }
                else if (s_UiSoundSelection_005d9e14 == 1 && Mem.s_SoundVolumeIni != 0)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_SoundVolumeIni -= 8;
                    Aud.ApplyAllSoundBufferVolumes();
                }
            }

            if ((action & 4) != 0)
            {
                if (s_UiSoundSelection_005d9e14 == 0 && Mem.s_CdVolumeIni < 0x80)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_CdVolumeIni += 8;
                    Aud.ApplyCdVolume();
                }
                else if (s_UiSoundSelection_005d9e14 == 1 && Mem.s_SoundVolumeIni < 0x80)
                {
                    Aud.PlayUiSound3A();
                    Mem.s_SoundVolumeIni += 8;
                    Aud.ApplyAllSoundBufferVolumes();
                }
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00512220 @ 0x00512220 â€” la page Â« ContrÃ´le Â», deux entrÃ©es qui ouvrent chacune
    /// une page de configuration.
    /// </summary>
    private static void ControlOptionsPage()
    {
        UiPageTrace("entrée page Contrôle");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x4c);
            UiDrawLabel(0xa0, 0x32, 0xf, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0x11, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 0x12, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiControlSelection_005d9e44 + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiControlSelection_005d9e44)
                action = UiSelect(selection, 0, 1);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                if (s_UiControlSelection_005d9e44 == 0) { Aud.PlayUiSound38(); KeyConfigPage(); }
                else if (s_UiControlSelection_005d9e44 == 1) { Aud.PlayUiSound38(); JoystickAssignmentPage(); }
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    private static int s_UiDisplaySelection_005d9e10;   // Ghidra: DAT_005d9e10
    private static int s_UiSoundSelection_005d9e14;     // Ghidra: DAT_005d9e14
    private static int s_UiControlSelection_005d9e44;   // Ghidra: DAT_005d9e44

    // Les quatre pages filles. Toutes ont un corps dÃ©compilÃ© et aucune dÃ©pendance bloquÃ©e ; elles
    // sont nommÃ©es pour que le runtime dÃ©signe la suivante.
    /// <summary>
    /// Ghidra: FUN_00510d40 @ 0x00510d40 â€” la page **Timing**, quatre choix. La sÃ©lection est
    /// initialisÃ©e depuis le rÃ©glage courant Ã  la premiÃ¨re ouverture seulement (bit 0 de
    /// DAT_00579260), et n'est appliquÃ©e qu'Ã  la validation.
    /// </summary>
    private static void TimingPage()
    {
        if ((s_UiPageFlags_00579260 & 1) == 0)
        {
            s_UiPageFlags_00579260 |= 1;
            s_UiTimingSelection_00579268 = (int)s_TimingMode;
        }

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 100);
            UiDrawLabel(0xa0, 0x32, 0x29, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0x32, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 0x2a, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x60, 0x2b, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x6c, 0x31, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiTimingSelection_00579268 + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiTimingSelection_00579268)
                action = UiSelect(selection, 0, 3);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                s_TimingMode = (uint)s_UiTimingSelection_00579268;
                Winsys3d.SetTimingMode(s_UiTimingSelection_00579268);
                return;
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00510e60 @ 0x00510e60 â€” la page **rÃ©solution**. Le device annonce les modes
    /// qu'il accepte par un masque de bits (slot <c>+0x1c</c>), la page en fait une liste, et une
    /// validation qui change rÃ©ellement de mode dÃ©truit les huit textures, refait la mise en place
    /// et recale le zoom.
    ///
    /// <para><b>Complète depuis le 2026-08-18.</b> La page était transcrite en entier mais restait
    /// vide : le device synthétique ne portait aucun des trois slots dont elle dépend. Ils sont
    /// posés maintenant — <c>+0x1c</c> annonce les cinq modes, <c>+0xE0</c> la capacité qui fait
    /// apparaître l'entrée dans la page « Affichage », et <c>+0xE8</c> consent au changement (voir
    /// <c>SyntheticDevice.cs</c>). Chez nous aucun mode d'affichage physique ne change : la cible
    /// de rendu suit le viewport, et <c>Present</c> la met à l'échelle dans la fenêtre.</para>
    ///
    /// <para>Le garde sur la liste vide est conservé : l'original n'en a pas besoin, son device
    /// annonçant toujours au moins un mode, mais rien ne garantit ça d'un shim.</para>
    /// </summary>
    private static void ResolutionPage()
    {
        int* modes = stackalloc int[32];
        int count = 0;

        uint mask = (uint)((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0x1c / 4])(Winsys3d.Ordinal_750());
        for (int bit = 0; mask != 0; mask >>= 1, bit++)
            if ((mask & 1) != 0) modes[count++] = bit;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, count * 0xc + 0x38);
            UiDrawLabel(0xa0, 0x32, 0x26, 1, 0xffffff);
            for (int i = 0, y = 0x48; i < count; i++, y += 0xc)
                UiDrawLabel(0x5c, y, modes[i] + 0x2c, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiResolutionSelection_005d9e0c + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiResolutionSelection_005d9e0c)
                action = UiSelect(selection, 0, count - 1);

            if (action == 0xffffffff) return;

            // Le garde : sans mode annoncÃ© il n'y a rien Ã  choisir.
            if ((action & 1) != 0 && count > 0
                && (uint)s_UiResolutionSelection_005d9e0c < (uint)count
                && modes[s_UiResolutionSelection_005d9e0c] != (int)s_ResolutionIndex)
            {
                ReleaseUiTextures();
                Winsys3d.ReInitializeAllTextures();

                s_ResolutionIndex = (uint)modes[s_UiResolutionSelection_005d9e0c];
                Winsys3d.ChangeResolution((int)s_ResolutionIndex);

                // Ghidra: la file est vidÃ©e de ses WM_QUIT (0x12) avant de continuer.
                MSG message;
                while (Win32.PeekMessageA(&message, IntPtr.Zero, 0x12, 0x12, 1) != 0) { }

                Mem.s_SizeSetting_005de160 = UiDetailLimit;
                Winsys3d.SetShift((int)((0xb - Mem.s_SizeSetting_005de160) * 0x10),
                         (int)((0xb - Mem.s_SizeSetting_005de160) * 0xc),
                         (int)Mem.s_SizeSetting_005de160, 0);
                if (Mem.s_SizeSetting_005de160 < 8) s_SmallTextureFlag = 1;

                OptionsSetup();     // Ghidra: DAT_00578e28 = Winsys3d.Ordinal_750(); FUN_0050fab0();
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005119c0 @ 0x005119c0 â€” l'affectation des manettes. Quatre lignes Â« joueur N Â»
    /// suivies du numÃ©ro de manette attribuÃ©, puis quatre lignes Â« configurer Â», grisÃ©es
    /// (0x5f5f5f au lieu de 0xffffff) tant que le joueur correspondant n'a pas de manette.
    /// <para>
    /// L'affectation est un rang compact : attribuer Ã  un joueur lui donne le rang suivant libre et
    /// dÃ©cale ceux d'aprÃ¨s ; le retirer les dÃ©cale en arriÃ¨re. Les quatre lignes du bas sont
    /// sautÃ©es Ã  la navigation quand elles sont inactives.
    /// </para>
    /// </summary>
    private static void JoystickAssignmentPage()
    {
        UiPageTrace("entrée page Manettes");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x94);
            UiDrawLabel(0xa0, 0x32, 0x20, 1, 0xffffff);

            for (int player = 0; player < 4; player++)
            {
                int y = 0x48 + player * 0xc;
                int label = 0x13 + player;
                UiDrawLabel(0x5c, y, label, 0, 0xffffff);
                // Ghidra: DAT_005897a4 + player*4 â€” la largeur du libellÃ© qu'on vient de poser,
                // prise dans la table des largeurs, sert de dÃ©calage pour la valeur.
                UiDrawLabel(s_UiLabelWidths_00589758[label] + 0x5c, y,
                            s_JoystickSlot[player] + 0x18, 0, 0xffffff);
            }

            for (int player = 0; player < 4; player++)
            {
                uint colour = s_JoystickSlot[player] != -1 ? 0xffffffu : 0x5f5f5fu;
                UiDrawLabel(0x5c, 0x78 + player * 0xc, 0x1c + player, 0, colour);
            }

            UiDrawCursor(0x4b, (s_UiJoystickSelection_005d9e38 + 6) * 0xc);
            UiEndScene();
            UiPresent();

            int before = s_UiJoystickSelection_005d9e38;
            uint action;
            fixed (int* selection = &s_UiJoystickSelection_005d9e38)
                action = UiSelect(selection, 0, 7);

            SkipInactiveJoystickRows(before);
            if (action == 0xffffffff) return;

            int row = s_UiJoystickSelection_005d9e38;

            if ((action & 7) != 0 && row is >= 0 and < 4)
            {
                Aud.PlayUiSound3A();
                ToggleJoystickAssignment(row);
            }

            if ((action & 1) != 0 && row is >= 4 and <= 7)
            {
                if (s_JoystickSlot[row - 4] < 0) Aud.PlayUiSoundError();
                else { Aud.PlayUiSound38(); JoystickButtonsPage(row - 4); }
            }

            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: la boucle qui repousse le curseur hors des quatre lignes Â« configurer Â» inactives â€”
    /// vers le haut si on y arrivait en descendant... et l'inverse, selon le sens du mouvement.
    /// </summary>
    private static void SkipInactiveJoystickRows(int before)
    {
        int row = s_UiJoystickSelection_005d9e38;
        if (row < 4 || s_JoystickSlot[row - 4] != -1) return;

        if (row < before)
        {
            int candidate = row;
            while (candidate > 3 && s_JoystickSlot[candidate - 4] == -1) candidate--;
            s_UiJoystickSelection_005d9e38 = candidate;
            return;
        }

        while (row < 8 && s_JoystickSlot[row - 4] == -1) row++;
        s_UiJoystickSelection_005d9e38 = row == 8 ? before : row;
    }

    /// <summary>
    /// Ghidra: attribuer donne au joueur le rang Ã©gal au nombre de joueurs dÃ©jÃ  servis avant lui et
    /// incrÃ©mente ceux d'aprÃ¨s ; retirer met -1 et dÃ©crÃ©mente ceux d'aprÃ¨s. Les rangs restent donc
    /// toujours compacts.
    /// </summary>
    private static void ToggleJoystickAssignment(int player)
    {
        if (s_JoystickSlot[player] == -1)
        {
            int rank = 0;
            for (int i = 0; i < player; i++) if (s_JoystickSlot[i] >= 0) rank++;
            s_JoystickSlot[player] = rank;
            for (int i = player + 1; i < 4; i++) if (s_JoystickSlot[i] != -1) s_JoystickSlot[i]++;
        }
        else
        {
            s_JoystickSlot[player] = -1;
            for (int i = player + 1; i < 4; i++) if (s_JoystickSlot[i] != -1) s_JoystickSlot[i]--;
        }
    }

    private static int s_UiPageFlags_00579260;            // Ghidra: DAT_00579260
    private static int s_UiTimingSelection_00579268;      // Ghidra: DAT_00579268
    private static int s_UiResolutionSelection_005d9e0c;  // Ghidra: DAT_005d9e0c
    private static int s_UiJoystickSelection_005d9e38;    // Ghidra: DAT_005d9e38

    // Ghidra: &DAT_005d9e18 et &DAT_005d9e28 — quatre entiers chacun, le curseur et le haut de
    // fenêtre de la page des boutons, **un par joueur**. La page est écrite une fois et paramétrée
    // par le numéro de joueur ; c'est ce qui la rend valable pour les manettes 1 à 4 sans
    // duplication.
    private static readonly int[] s_UiJoySelection_005d9e18 = new int[4];
    private static readonly int[] s_UiJoyScroll_005d9e28 = new int[4];

    /// <summary>
    /// Ghidra: &amp;DAT_00562e70 — les trente-six décalages de bouton que la page balaie dans l'état
    /// DirectInput. La suite est <c>0x00..0x03</c> puis <c>0x06..0x25</c> : **les décalages 4 et 5
    /// sont sautés**, et c'est aussi pourquoi les deux entrées correspondantes de
    /// <see cref="s_JoyButtonLabel_00562e99"/> valent 0.
    /// </summary>
    private static readonly byte[] s_JoyButtonScan_00562e70 =
    [
        0x00,0x01,0x02,0x03,0x06,0x07,0x08,0x09, 0x0a,0x0b,0x0c,0x0d,0x0e,0x0f,0x10,0x11,
        0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x19, 0x1a,0x1b,0x1c,0x1d,0x1e,0x1f,0x20,0x21,
        0x22,0x23,0x24,0x25,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00562e99 — le numéro de bouton vers son rang dans la planche de libellés.
    ///
    /// <para><b>L'octet qui précède la table vaut <c>0xff</c>, et c'est délibéré.</b> Le code lit
    /// <c>(char)(&amp;DAT_00562e99)[liaison]</c> avec une liaison qui vaut <c>-1</c> quand aucun
    /// bouton n'est affecté ; l'index -1 tombe donc sur ce <c>0xff</c>, soit <c>-1</c> une fois lu
    /// en octet signé, ce que la ligne suivante teste pour afficher « aucun ». Reproduit ici par un
    /// tableau décalé d'une case, comme le tampon d'échappements du moteur de texte.</para>
    /// </summary>
    private static readonly byte* s_JoyButtonLabelStorage_00562e98 = BuildJoyButtonLabels();

    private static byte* s_JoyButtonLabel_00562e99 => s_JoyButtonLabelStorage_00562e98 + 1;

    private static byte* BuildJoyButtonLabels()
    {
        ReadOnlySpan<byte> table =
        [
            0xff,                                                   // l'index -1
            0x00,0x01,0x02,0x03,0x00,0x00,0x04,0x05, 0x06,0x07,0x08,0x09,0x0a,0x0b,0x0c,0x0d,
            0x0e,0x0f,0x10,0x11,0x12,0x13,0x14,0x15, 0x16,0x17,0x18,0x19,0x1a,0x1b,0x1c,0x1d,
            0x1e,0x1f,0x20,0x21,0x22,0x23,0x24,
        ];
        byte* buffer = (byte*)NativeMemory.AllocZeroed((nuint)table.Length);
        for (int i = 0; i < table.Length; i++) buffer[i] = table[i];
        return buffer;
    }

    /// <summary>
    /// Ghidra: FUN_005115c0 @ 0x005115c0 — la page des **boutons de manette**, celle qu'atteint
    /// « Joueur N » depuis l'affectation des manettes. Dix-huit actions listées par fenêtre de dix,
    /// plus une dernière ligne qui remet les liaisons par défaut.
    ///
    /// <para>Elle est écrite une seule fois et prend le joueur en paramètre : les manettes 1 à 4
    /// passent par ce même corps, avec leur propre curseur (<see cref="s_UiJoySelection_005d9e18"/>),
    /// leur propre haut de fenêtre (<see cref="s_UiJoyScroll_005d9e28"/>) et leur propre bloc de
    /// vingt liaisons dans <see cref="s_KeyBindings"/> — c'est exactement ce que fait l'original,
    /// qui indexe tout par <c>iVar1 * 0x3c</c>.</para>
    ///
    /// <para>Deux détails que seul le désassemblage donne, la pseudo-C ayant perdu l'arité de
    /// <c>FUN_005108b0</c> : la couleur du glyphe de bouton est <c>0xFFFFFF</c>
    /// (<c>0x00511682  PUSH 0x00FFFFFF</c>, le septième argument que Ghidra n'affichait pas), et
    /// l'ordonnée dans la planche est 0 pour les dix-huit premiers boutons, <c>0x80</c> au-delà
    /// (<c>SETGE / DEC / AND 0x80 / ADD 0x80</c> à <c>0x005116B3</c>).</para>
    /// </summary>
    private static void JoystickButtonsPage(int player)
    {
        UiPageTrace($"entrée boutons manette, joueur {player + 1}");

        int capturing = -1;   // Ghidra: param_1, réutilisé comme « ligne en cours de réaffectation »

        while (true)
        {
            RestoreLostUiTextures();     // Ghidra: FUN_0050fe90
            UiBeginScene();
            DrawUiBackground();          // Ghidra: FUN_005101c0

            UiDrawFrame(0x2e, 0x20, 0xe4, 0xb8);
            UiDrawLabel(0xa0, 0x32, 0x21, 1, 0xffffff);
            UiDrawLabel(0xa0, 0x3e, player + 0x13, 1, 0xffffff);   // « Joueur N »

            for (int slot = 0, y = 0x54; y < 0xcc; slot++, y += 0xc)
            {
                int row = slot + s_UiJoyScroll_005d9e28[player];

                if (row < 0x12)
                {
                    UiDrawLabel(0x3e, y, -1 - row, 0, 0xffffff);

                    if (row == capturing)
                    {
                        DrawKeyName(0xd7, y, 0x55, 0xffffff);      // le libellé « ? » d'attente
                    }
                    else
                    {
                        int binding = s_KeyBindings[player * 0x3c + 40 + s_KeyActionIndex_00562ec0[row]];
                        int label = (sbyte)s_JoyButtonLabel_00562e99[binding];
                        if (label == -1)
                        {
                            UiDrawLabel(0xd7, y, 6, 0, 0xffffff);  // « aucun »
                        }
                        else
                        {
                            UiBlit(0xd7, y, 0x80, 0xe, label >= 0x12 ? 0x80 : 0, (label % 0x12) * 0xe,
                                   s_UiAtlasJoystick_00599dd8, 0x80, 0xe, 0xffffff);
                        }
                    }
                }
                else if (row == 0x12)
                {
                    UiDrawLabel(0x3e, y, 0x23, 0, 0xffffff);       // « valeurs par défaut »
                }

                // Les quatre directions portent la même flèche que l'éditeur de touches, posée
                // juste après le libellé — d'où la largeur de ce libellé comme décalage.
                if (row is > 3 and < 8)
                {
                    UiBlit(s_UiLabelWidths_00589758[-1 - row] + 0x3e, y + 2, 8, 9,
                           s_KeyArrowColumn_00563060[row - 4] * 8 + 0x98, 0x20,
                           s_UiFrameAtlas_00599ddc, 8, 9, 0xffffff);
                }
            }

            UiDrawCursor(0x2d, (s_UiJoySelection_005d9e18[player] - s_UiJoyScroll_005d9e28[player] + 7) * 0xc);
            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiJoySelection_005d9e18[player])
                action = UiSelect(selection, 0, 0x12);

            // Page précédente / suivante : neuf lignes, curseur et fenêtre ensemble.
            if ((action & 8) != 0)
            {
                s_UiJoySelection_005d9e18[player] -= 9;
                s_UiJoyScroll_005d9e28[player] -= 9;
                if (s_UiJoySelection_005d9e18[player] < 0) s_UiJoySelection_005d9e18[player] = 0;
                if (s_UiJoyScroll_005d9e28[player] < 0) s_UiJoyScroll_005d9e28[player] = 0;
            }
            if ((action & 0x10) != 0)
            {
                s_UiJoySelection_005d9e18[player] += 9;
                s_UiJoyScroll_005d9e28[player] += 9;
                if (s_UiJoySelection_005d9e18[player] > 0x12) s_UiJoySelection_005d9e18[player] = 0x12;
                if (s_UiJoyScroll_005d9e28[player] > 9) s_UiJoyScroll_005d9e28[player] = 9;
            }

            // La fenêtre suit le curseur d'une ligne à la fois.
            if (s_UiJoySelection_005d9e18[player] < s_UiJoyScroll_005d9e28[player])
                s_UiJoyScroll_005d9e28[player]--;
            else if (s_UiJoyScroll_005d9e28[player] + 9 < s_UiJoySelection_005d9e18[player])
                s_UiJoyScroll_005d9e28[player]++;

            if (capturing == -1)
            {
                if ((action & 1) != 0)
                {
                    if (s_UiJoySelection_005d9e18[player] < 0x12)
                    {
                        Aud.PlayUiSound38();
                        capturing = s_UiJoySelection_005d9e18[player];
                        WaitForAllJoystickButtonsReleased(player);
                    }
                    else if (s_UiJoySelection_005d9e18[player] == 0x12)
                    {
                        // La remise aux valeurs par défaut : les vingt entiers de DAT_00564a68,
                        // recopiés dans le bloc manette de ce joueur.
                        Aud.PlayUiSound38();
                        for (int i = 0; i < 20; i++)
                            s_KeyBindings[player * 0x3c + 40 + i] = s_JoyBindingDefaults_00564a68[i];
                    }
                }

                if (action == 0xffffffff) return;
                Winsys3d.Ordinal_132(2);
            }
            else
            {
                Inp.PollJoystickState();     // Ghidra: FUN_005048f0

                byte* keys = Inp.s_KeyboardState_00604b50;
                int stick = s_JoystickSlot[player];

                // Échap (scancode 1) abandonne la capture sans rien affecter.
                if (keys != null && (keys[1] & 0x80) == 0)
                {
                    int captured = -1;
                    for (int i = 0; i < s_JoyButtonScan_00562e70.Length; i++)
                    {
                        int button = s_JoyButtonScan_00562e70[i];
                        if (stick == -1) break;
                        if ((keys[0x100 + stick * 0x26 + button] & 0x80) == 0) continue;
                        captured = button;
                        break;
                    }

                    if (captured == -1) { Winsys3d.Ordinal_132(2); continue; }

                    // Le bouton retenu est d'abord retiré de toutes les autres actions de ce
                    // joueur : une manette ne peut pas commander deux actions à la fois.
                    for (int i = 0; i < 20; i++)
                    {
                        if (s_KeyBindings[player * 0x3c + 40 + i] == captured)
                            s_KeyBindings[player * 0x3c + 40 + i] = -1;
                    }
                    Aud.PlayUiSound38();
                    s_KeyBindings[player * 0x3c + 40 + s_KeyActionIndex_00562ec0[capturing]] = captured;
                }

                capturing = -1;
                Winsys3d.Ordinal_132(2);
            }
        }
    }

    /// <summary>
    /// Ghidra: la boucle d'attente qui suit l'ouverture d'une capture — tant qu'un des trente-six
    /// boutons de la manette du joueur est enfoncé, on ne lit rien. Sans elle, le bouton qui vient
    /// de valider la ligne serait aussitôt capturé comme sa nouvelle liaison.
    /// </summary>
    private static void WaitForAllJoystickButtonsReleased(int player)
    {
        while (true)
        {
            Inp.PollJoystickState();         // Ghidra: FUN_005048f0

            byte* keys = Inp.s_KeyboardState_00604b50;
            int stick = s_JoystickSlot[player];
            if (keys == null || stick == -1) return;

            byte held = 0;
            for (int i = 0; i < s_JoyButtonScan_00562e70.Length; i++)
                held |= (byte)(keys[0x100 + stick * 0x26 + s_JoyButtonScan_00562e70[i]] & 0x80);

            Winsys3d.Ordinal_132(0);
            if (held == 0) return;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005115a0 @ 0x005115a0 â€” retrouve l'index du nom d'une touche Ã  partir de son
    /// scancode, en cherchant dans la table des 85 scancodes. -1 si la touche n'est pas listÃ©e.
    /// </summary>
    private static int KeyLabelForScancode(int scancode)
    {
        for (int i = 0; i < 0x55; i++)
            if (s_KeyScancodes_00562e18[i] == scancode) return i;
        return -1;
    }

    /// <summary>
    /// Ghidra: FUN_005109c0 @ 0x005109c0 puis FUN_005108b0 â€” dessine un nom de touche depuis
    /// l'atlas clavier, en lisant sa position dans la table de placement que
    /// <see cref="RasteriseKeyLabels"/> a remplie : x en <c>+0</c>, ligne en <c>+1</c>, largeur en
    /// <c>+2</c>.
    /// </summary>
    private static void DrawKeyName(int x, int y, int keyIndex, uint colour)
    {
        byte* placement = s_UiKeyPlacement_00599c80 + keyIndex * 4;
        int sourceX = placement[0];
        int row = placement[1];
        int width = *(short*)(placement + 2);
        UiBlit(x, y, width, 0xe, sourceX, row * 0xe, s_UiAtlasKeyboard_00589c78, width, 0xe, colour);
    }

    /// <summary>
    /// Ghidra: FUN_00511d40 @ 0x00511d40 â€” l'Ã©diteur de touches. Une liste de 0x31 lignes dÃ©filant
    /// par fenÃªtre de onze, bÃ¢tie sur la table de descripteurs `DAT_00562ed8` : huit actions
    /// globales, puis quatre blocs Â« Joueur N Â» (un en-tÃªte colorÃ© et neuf actions), et une
    /// derniÃ¨re ligne qui remet les liaisons par dÃ©faut.
    /// <para>
    /// Chaque ligne d'action montre jusqu'Ã  deux touches. Ce sont les catÃ©gories 0 et 1 de la mÃªme
    /// table de liaisons â€” `DAT_00564ba8` est `DAT_00564b58` dÃ©calÃ© de vingt entrÃ©es â€” c'est-Ã -dire
    /// les deux jeux clavier que <see cref="FoldKeyboardIntoPadState"/> lit dÃ©jÃ . Assigner une
    /// touche pousse l'ancienne en second et efface ce scancode partout ailleurs.
    /// </para>
    /// </summary>
    private static void KeyConfigPage()
    {
        UiPageTrace("entrée éditeur de touches");
        int capturing = -1;
        bool drainAfterCapture = false;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x10, 0x20, 0x120, 0xb8);
            UiDrawLabel(0xa0, 0x32, 0x22, 1, 0xffffff);

            for (int slot = 0, y = 0x48; y < 0xcc; slot++, y += 0xc)
            {
                int row = slot + s_UiKeyScroll_005d9e40;
                if ((uint)row >= KeyRowCount) continue;

                byte* desc = s_KeyRowTable_00562ed8 + row * 8;
                uint colour = *(uint*)desc;
                int kind = (sbyte)desc[4];

                // Ghidra: un `kind` nÃ©gatif est une ligne d'en-tÃªte â€” elle ne porte qu'un libellÃ©.
                if (kind < 0) { UiDrawLabel(0x20, y, (sbyte)desc[6], 0, colour); continue; }

                UiDrawLabel(0x20, y, -1 - kind, 0, colour);

                // Ghidra: les quatre directions portent une flÃ¨che prise dans la planche du cadre.
                if (kind is > 3 and < 8)
                {
                    UiBlit(s_UiLabelWidths_00589758[-1 - kind] + 0x20, y + 2, 8, 9,
                           s_KeyArrowColumn_00563060[kind - 4] * 8 + 0x98, 0x20,
                           s_UiFrameAtlas_00599ddc, 8, 9, 0xffffff);
                }

                int group = (sbyte)desc[5];
                if (group == -1) group = 0;
                int binding = s_KeyActionIndex_00562ec0[kind] + group * 0x3c;

                int x = 0xb9;
                int primary = KeyLabelForScancode(s_KeyBindings[binding]);
                if (primary != -1) { DrawKeyName(0xb9, y, primary, colour); x = 0xf3; }

                if (row != capturing || x == 0xb9)
                {
                    int secondary = KeyLabelForScancode(s_KeyBindings[binding + 20]);
                    if (secondary != -1) { DrawKeyName(x, y, secondary, colour); x += 0x3a; }
                }

                if (row == capturing) DrawKeyName(x, y, 0x55, colour);
                else if (x == 0xb9) UiDrawLabel(0xb9, y, 6, 0, colour);
            }

            UiDrawCursor(0xf, (s_UiKeySelection_005d9e3c - s_UiKeyScroll_005d9e40 + 6) * 0xc);
            UiEndScene();
            UiPresent();

            if (drainAfterCapture) { WaitForAllKeysReleased(); drainAfterCapture = false; }

            int before = s_UiKeySelection_005d9e3c;
            uint action;
            fixed (int* selection = &s_UiKeySelection_005d9e3c)
                action = UiSelect(selection, 0, 0x30);

            // Ghidra: page prÃ©cÃ©dente / page suivante dÃ©placent de dix, curseur et fenÃªtre ensemble.
            if ((action & 8) != 0)
            {
                s_UiKeySelection_005d9e3c -= 10;
                s_UiKeyScroll_005d9e40 -= 10;
                if (s_UiKeySelection_005d9e3c < 0) s_UiKeySelection_005d9e3c = 0;
                if (s_UiKeyScroll_005d9e40 < 0) s_UiKeyScroll_005d9e40 = 0;
            }
            if ((action & 0x10) != 0)
            {
                s_UiKeySelection_005d9e3c += 10;
                s_UiKeyScroll_005d9e40 += 10;
                if (s_UiKeySelection_005d9e3c > 0x30) s_UiKeySelection_005d9e3c = 0x30;
                if (s_UiKeyScroll_005d9e40 > 0x26) s_UiKeyScroll_005d9e40 = 0x26;
            }

            FollowKeyCursor();

            // Ghidra: une ligne d'en-tÃªte (kind == -1, mais pas -2) est franchie dans le sens du
            // dÃ©placement. La derniÃ¨re ligne, de type -2, reste sÃ©lectionnable.
            if ((sbyte)s_KeyRowTable_00562ed8[s_UiKeySelection_005d9e3c * 8 + 4] == -1)
                s_UiKeySelection_005d9e3c += s_UiKeySelection_005d9e3c > before ? 1 : -1;

            FollowKeyCursor();

            if (capturing == -1)
            {
                if ((action & 1) != 0)
                {
                    if (s_UiKeySelection_005d9e3c < 0x30)
                    {
                        Aud.PlayUiSound38();
                        capturing = s_UiKeySelection_005d9e3c;
                        WaitForAllKeysReleased();
                    }
                    else if (s_UiKeySelection_005d9e3c == 0x30)
                    {
                        Aud.PlayUiSound38();
                        ResetKeyBindings();
                    }
                }

                if (action == 0xffffffff) return;
                Winsys3d.Ordinal_132(2);
                continue;
            }

            // Mode capture : la premiÃ¨re touche listÃ©e qu'on trouve enfoncÃ©e devient la liaison.
            Inp.PollJoystickState();
            int pressed = -1;
            for (int i = 0; i < 0x55; i++)
            {
                if ((Inp.s_KeyboardState_00604b50[s_KeyScancodes_00562e18[i]] & 0x80) == 0) continue;
                pressed = i;
                break;
            }

            if (pressed == -1) { Winsys3d.Ordinal_132(2); continue; }

            int scancode = s_KeyScancodes_00562e18[pressed];
            ClearScancodeEverywhere(scancode);
            Aud.PlayUiSound38();

            byte* captured = s_KeyRowTable_00562ed8 + capturing * 8;
            int capturedGroup = (sbyte)captured[5];
            if (capturedGroup == -1) capturedGroup = 0;
            int target = s_KeyActionIndex_00562ec0[(sbyte)captured[4]] + capturedGroup * 0x3c;

            capturing = -1;
            drainAfterCapture = true;

            // L'ancienne liaison devient la seconde ; la nouvelle prend la premiÃ¨re place.
            s_KeyBindings[target + 20] = s_KeyBindings[target];
            s_KeyBindings[target] = scancode;
            Winsys3d.Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: la fenÃªtre suit le curseur, une ligne Ã  la fois, deux fois par tour.</summary>
    private static void FollowKeyCursor()
    {
        if (s_UiKeySelection_005d9e3c < s_UiKeyScroll_005d9e40) s_UiKeyScroll_005d9e40--;
        else if (s_UiKeyScroll_005d9e40 + 10 < s_UiKeySelection_005d9e3c) s_UiKeyScroll_005d9e40++;
    }

    /// <summary>
    /// Ghidra: la boucle d'attente qui suit une validation â€” tant qu'une touche listÃ©e est
    /// enfoncÃ©e, on repolle. Elle empÃªche la touche de validation d'Ãªtre capturÃ©e aussitÃ´t.
    /// </summary>
    private static void WaitForAllKeysReleased()
    {
        while (true)
        {
            Inp.PollJoystickState();
            byte any = 0;
            for (int i = 0; i < 0x55; i++)
                any |= (byte)(Inp.s_KeyboardState_00604b50[s_KeyScancodes_00562e18[i]] & 0x80);
            Winsys3d.Ordinal_132(0);
            if (any == 0) return;
        }
    }

    /// <summary>
    /// Ghidra: le scancode retenu est effacÃ© de **toutes** les liaisons clavier â€” les deux
    /// catÃ©gories des quatre groupes, soit 2 x 20 entiers par groupe de 0x3c. La troisiÃ¨me
    /// catÃ©gorie, celle de la manette, n'est pas touchÃ©e.
    /// </summary>
    private static void ClearScancodeEverywhere(int scancode)
    {
        for (int group = 0; group < 4; group++)
            for (int i = 0; i < 40; i++)
                if (s_KeyBindings[group * 0x3c + i] == scancode) s_KeyBindings[group * 0x3c + i] = 0;
    }

    /// <summary>
    /// Ghidra: la derniÃ¨re ligne rÃ©tablit les liaisons du joueur 1 depuis la table de dÃ©fauts et
    /// efface celles des trois autres.
    /// </summary>
    private static void ResetKeyBindings()
    {
        for (int i = 0; i < 0x28; i++) s_KeyBindings[i] = s_KeyBindingDefaults_00564ab8[i];
        for (int group = 1; group < 4; group++)
            for (int i = 0; i < 0x28; i++) s_KeyBindings[group * 0x3c + i] = 0;
    }

    private static int s_UiKeySelection_005d9e3c;   // Ghidra: DAT_005d9e3c
    private static int s_UiKeyScroll_005d9e40;      // Ghidra: DAT_005d9e40

    private const int KeyRowCount = 0x31;

    // =====================================================================
    // Les cinq tables de l'Ã©diteur de touches, relevÃ©es dans Legacy/Blaze.exe.
    // =====================================================================

    /// <summary>
    /// Ghidra: &amp;DAT_00562e18 â€” les 85 scancodes DirectInput que l'Ã©diteur sait nommer, dans le
    /// mÃªme ordre que les lignes de KEYBOARD.TXT. Le 0xff qui suit est un terminateur.
    /// </summary>
    private static readonly byte[] s_KeyScancodes_00562e18 =
    [
        0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08, 0x09,0x0a,0x0b,0x0c,0x0d,0x0e,0x0f,0x10,
        0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18, 0x19,0x1a,0x1b,0x1c,0x1d,0x1e,0x1f,0x20,
        0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28, 0x29,0x2a,0x2b,0x2c,0x2d,0x2e,0x2f,0x30,
        0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38, 0x39,0x47,0x48,0x49,0x4a,0x4b,0x4c,0x4d,
        0x4e,0x4f,0x50,0x51,0x52,0x53,0x9d,0x56, 0xb5,0xb8,0xc7,0xc8,0xc9,0xcb,0xcd,0xcf,
        0xd0,0xd1,0xd2,0xd3,0x3b,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00562ec0 â€” l'action d'une ligne vers son rang dans le bloc de vingt liaisons.
    /// </summary>
    private static readonly byte[] s_KeyActionIndex_00562ec0 =
    [
        0x0c,0x0e,0x0f,0x0d,0x07,0x05,0x06,0x04, 0x03,0x02,0x01,0x00,0x0b,0x08,0x12,0x13,
        0x10,0x11,0x00,0x00,0x03,0x01,0x02,0x00,
    ];

    /// <summary>
    /// Ghidra: `(&amp;DAT_0056305c)[kind]` pour kind 4..7 â€” la colonne de la flÃ¨che de direction dans
    /// la planche du cadre. Les quatre octets utiles sont ceux de 0x00563060.
    /// </summary>
    private static readonly byte[] s_KeyArrowColumn_00563060 = [0x03, 0x01, 0x02, 0x00];

    /// <summary>
    /// Ghidra: &amp;DAT_00564ab8 â€” les liaisons par dÃ©faut du joueur 1, quarante entiers : les deux
    /// jeux clavier. Ils correspondent exactement aux clÃ©s `K0000`..`K0113` du BLAZE.cfg installÃ©,
    /// ce qui les confirme.
    /// </summary>
    private static readonly int[] s_KeyBindingDefaults_00564ab8 =
    [
        0x47, 0x4f, 0x51, 0x49, 0x39, 0x2a, 0x38, 0x1d, 0x3b, 0x00,
        0x00, 0x3c, 0x48, 0x4d, 0x50, 0x4b, 0x00, 0x00, 0x0c, 0x0d,
        0xc7, 0xcf, 0xd1, 0xc9, 0x00, 0x00, 0x01, 0x1c, 0x00, 0x00,
        0x00, 0x00, 0xc8, 0xcd, 0xd0, 0xcb, 0xd2, 0xd3, 0x4a, 0x4e,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00564a68 — les liaisons **manette** par défaut, vingt entiers, celles que la
    /// dernière ligne de <see cref="JoystickButtonsPage"/> recopie. Relevées dans
    /// <c>Legacy/Blaze.exe</c> et identiques aux clés <c>K0200</c>..<c>K0213</c> du BLAZE.cfg
    /// installé, ainsi qu'aux quatre tables manette pré-remplies de <c>DAT_00564b58</c>.
    ///
    /// <para><c>-1</c> y signifie « aucun bouton », et non <c>0</c> : le bouton 0 est valide. Voir
    /// <see cref="s_JoyButtonLabel_00562e99"/>, dont l'octet d'index -1 encode cette convention.</para>
    /// </summary>
    private static readonly int[] s_JoyBindingDefaults_00564a68 =
    [
        11, 8, 13, 12, 10, 9, 7, 6, 15, -1,
        -1, 14, 2, 1, 3, 0, -1, -1, -1, -1,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00562ed8 â€” les 0x31 lignes de l'Ã©diteur, huit octets chacune : couleur sur
    /// quatre octets, puis l'action, le groupe et le libellÃ©, tous trois signÃ©s.
    /// <para>
    /// Transcrites en octets bruts plutÃ´t qu'interprÃ©tÃ©es : la table se relit alors ligne Ã  ligne
    /// contre le vidage du binaire, ce qu'une reconstruction structurÃ©e ne permet pas. La structure
    /// s'y lit quand mÃªme â€” huit actions globales en blanc, puis quatre blocs Â« Joueur N Â» dans les
    /// quatre teintes `FFBFBF`, `BFFFBF`, `BFBFFF`, `FFFFBF`, chacun un en-tÃªte (action -1) et neuf
    /// actions, et une derniÃ¨re ligne d'action -2 : la remise aux valeurs par dÃ©faut.
    /// </para>
    /// </summary>
    private static readonly byte* s_KeyRowTable_00562ed8 = BuildKeyRowTable();

    private static byte* BuildKeyRowTable()
    {
        ReadOnlySpan<byte> rows =
        [
            0xff,0xff,0xff,0x00, 0x08,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x09,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x0a,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x0b,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x0e,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x0f,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x10,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x11,0xff,0x00,0x00,

            0xbf,0xbf,0xff,0x00, 0xff,0xff,0x13,0x00,   0xbf,0xbf,0xff,0x00, 0x00,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x01,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x02,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x03,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x04,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x05,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x06,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x07,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x0d,0x00,0x00,0x00,

            0xbf,0xff,0xbf,0x00, 0xff,0xff,0x14,0x00,   0xbf,0xff,0xbf,0x00, 0x00,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x01,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x02,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x03,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x04,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x05,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x06,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x07,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x0d,0x01,0x00,0x00,

            0xff,0xbf,0xbf,0x00, 0xff,0xff,0x15,0x00,   0xff,0xbf,0xbf,0x00, 0x00,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x01,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x02,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x03,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x04,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x05,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x06,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x07,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x0d,0x02,0x00,0x00,

            0xbf,0xff,0xff,0x00, 0xff,0xff,0x16,0x00,   0xbf,0xff,0xff,0x00, 0x00,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x01,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x02,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x03,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x04,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x05,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x06,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x07,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x0d,0x03,0x00,0x00,

            0xff,0xff,0xff,0x00, 0xfe,0xff,0x23,0x00,
        ];

        byte* table = (byte*)NativeMemory.AllocZeroed((nuint)rows.Length);
        for (int i = 0; i < rows.Length; i++) table[i] = rows[i];
        return table;
    }

    /// <summary>Journalisation des Ã©tapes de la mise en place : <c>OPENFORESIA_OPTIONS_SETUP=1</c>.</summary>
    private static readonly bool s_RunOptionsSetup =
        Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_SETUP") == "1";

    private static readonly bool s_TraceGlyphs =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_GLYPHS") == "1";

    /// <summary>
    /// Ghidra: the menu's options branch drains input with `while (FUN_005048f0() != 0)` â€” it waits
    /// until nothing is held before opening the page, so the keypress that chose "Options" is not
    /// still down inside it. `FUN_005048f0` is <see cref="PollJoystickState"/>, reconstructed on
    /// 2026-08-14; this stub predates it and was never anything else.
    /// </summary>
    private static int InputDrainStub() => Inp.PollJoystickState();

    // Ghidra: &DAT_0056e670 â€” the menu's label table, recovered verbatim from Legacy/Blaze.exe.
    // A 0x0c length byte, then five cp1252 labels separated by carriage returns, ending 0x09 '?'.
    // They match the five switch cases in RunMainMenuFrameLoop one for one and in order:
    //   "DÃ©but du jeu"                -> game state 1
    //   "SÃ©lection des personnages"   -> game state 4
    //   "Menu objets"                 -> game state 6
    //   "Options"                     -> the options page
    //   "Quitter"                     -> Exit(0)
    private static readonly byte* s_MenuLabels_0056e670 = BuildMenuLabels();

    private static byte* BuildMenuLabels()
    {
        ReadOnlySpan<byte> bytes =
        [
            0x0c, 0x44, 0xe9, 0x62, 0x75, 0x74, 0x20, 0x64, 0x75, 0x20, 0x6a, 0x65, 0x75, 0x0d,
            0x53, 0xe9, 0x6c, 0x65, 0x63, 0x74, 0x69, 0x6f, 0x6e, 0x20, 0x64, 0x65, 0x73, 0x20,
            0x70, 0x65, 0x72, 0x73, 0x6f, 0x6e, 0x6e, 0x61, 0x67, 0x65, 0x73, 0x0d,
            0x4d, 0x65, 0x6e, 0x75, 0x20, 0x6f, 0x62, 0x6a, 0x65, 0x74, 0x73, 0x0d,
            0x4f, 0x70, 0x74, 0x69, 0x6f, 0x6e, 0x73, 0x0d,
            0x51, 0x75, 0x69, 0x74, 0x74, 0x65, 0x72, 0x0d,
            0x09, 0x3f, 0x00, 0x00,
        ];

        byte* buffer = (byte*)NativeMemory.AllocZeroed((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) buffer[i] = bytes[i];
        return buffer;
    }

    // Ghidra: &DAT_0056dd58 â€” the menu context struct FUN_00544350 writes into. Its highest touched
    // offset is 0x8c7, and the next named global sits at 0x0056e670, so it spans 0x918 bytes.
    /// <summary>
    /// Ghidra: &amp;DAT_0056dd58 â€” the main menu's panel descriptor, **statically initialised in
    /// `.data`** and recovered verbatim from `Legacy/Blaze.exe`. Only 35 of its bytes are
    /// non-zero; the rest is the scratch the text engine and the renderer fill in.
    /// <para>
    /// Leaving it zeroed is what produced a black screen: every feature test in
    /// <see cref="DrawUiPanel"/> is <c>field &amp; 0x1000000</c>, so a zeroed descriptor draws
    /// nothing at all â€” no background, no frame, no text.
    /// </para>
    /// Allocated low because <see cref="RunTextEngine"/> masks it to 24 bits.
    /// </summary>
    // Lazy, not a field initializer: field initializers run inside the type constructor, so a
    // failure to secure low memory here would surface as TypeInitializationException â€” outside the
    // reach of the module initializer's try/catch, and therefore unrecoverable by relaunching.
    private static byte* s_MenuContextStorage;
    private static byte* s_MenuContext_0056dd58
        => s_MenuContextStorage != null ? s_MenuContextStorage : (s_MenuContextStorage = BuildMenuContext());

    private static byte* BuildMenuContext()
    {
        // The allocation keeps 0x918 bytes of slack, but the descriptor proper ends at +0x8c7:
        // the bytes beyond it in the original belong to neighbouring globals, not to this struct
        // (0x0056e620 is the controller remap table, 0x0056e660 its four-entry pointer table).
        byte* ctx = AllocLowZeroed(0x918);

        // (offset, value) pairs, read straight out of the binary. Key fields:
        //   +0x00  0x0031            tpage/clut slot, indexes DAT_005e8f00
        //   +0x04  0x00080008        panel position (8, 8)
        //   +0x08  0x00480088        panel extent (0x88 x 0x48)
        //   +0x18  0x010F0808        background   â€” bit 0x1000000 set, so it draws
        //   +0x20  0x01070C10        text         â€” set
        //   +0x30  0x000F2874        cursor       â€” clear
        //   +0x38  0x000F0C00        scroll arrowsâ€” clear
        //   +0x40  0x010F0000        frame        â€” set
        ReadOnlySpan<(short Offset, byte Value)> initial =
        [
            (0x000,0x31), (0x004,0x08), (0x006,0x08), (0x008,0x88), (0x00A,0x48), (0x012,0x1A),
            (0x013,0x0C), (0x018,0x08), (0x019,0x08), (0x01A,0x0F), (0x01B,0x01), (0x01C,0x20),
            (0x01D,0x10), (0x01E,0xCF), (0x01F,0x02), (0x020,0x10), (0x021,0x0C), (0x022,0x07),
            (0x023,0x01), (0x027,0x01), (0x02F,0x01), (0x030,0x74), (0x031,0x28), (0x032,0x0F),
            (0x037,0x01), (0x039,0x0C), (0x03A,0x0F), (0x03F,0x01), (0x042,0x0F), (0x043,0x01),
            (0x047,0x01), (0x04F,0x01), (0x057,0x01), (0x05F,0x01), (0x067,0x01),
        ];

        foreach ((short offset, byte value) in initial) ctx[offset] = value;
        return ctx;
    }

    /// <summary>
    /// Ghidra: FUN_00544350 @ 0x00544350 â€” binds the label table and its flags into the menu
    /// context. The call site is <c>FUN_00544350(&amp;DAT_0056e670, 0, &amp;DAT_0056dd58, 0)</c>.
    /// </summary>
    internal static void BindMenuLabels(byte* labels, uint flags, byte* context, uint extra)
    {
        *(uint*)(context + 0x8b0) = flags;
        context[0x8c7] |= 1;
        *(byte**)(context + 0x8ac) = labels;
        *(uint*)(context + 0x8a8) = extra;

        // Ghidra: if (*param_1 == 8) FUN_00535300(0). The label table's first short is 0x440c
        // (recovered from the binary), so this never fires at this call site â€” but the branch is
        // kept because other callers may pass a table that does.
        if (*(short*)labels == 8)
        {
            throw new NotImplementedException(
                "FUN_00535300: reached only when the bound table's first short is 8 â€” " +
                "not the case for the main menu's label table");
        }
    }

    /// <summary>
    /// Ghidra: FUN_00547140 @ 0x00547140 â€” the CRT `rand()`. The constants are MSVC's linear
    /// congruential generator, which also identifies its neighbour: `FUN_00547130`, already called
    /// by RunMainLoop with 1, is `srand`, and the global they share (`DAT_0056e9c0`) is the seed
    /// rather than the flag an earlier comment called it.
    /// </summary>
    private static int CrtRand()
    {
        Mem.s_RandSeed_0056e9c0 = Mem.s_RandSeed_0056e9c0 * 0x343fd + 0x269ec3;
        return (int)((Mem.s_RandSeed_0056e9c0 >> 0x10) & 0x7fff);
    }

    /// <summary>
    /// Ghidra: FUN_00545140 @ 0x00545140 â€” flushes the deferred VRAM upload queue: walks the
    /// singly-linked list whose head <see cref="FrameBegin"/> resets each frame, calling
    /// <c>Ordinal_674</c> (libgpu LoadImage) with each node's rect at +8 and its data at +4.
    /// </summary>
    private static void FlushDeferredImageUploads()
    {
        for (uint* node = (uint*)(nuint)Mem.s_Unknown_005e8310;
             node != null;
             node = (uint*)(nuint)node[0])
        {
            Winsys3d.Ordinal_674(node + 2, (void*)(nuint)node[1]);
        }
    }
}
