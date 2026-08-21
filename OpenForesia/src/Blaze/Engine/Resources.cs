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

// `Blaze` porte encore ce que cette classe appelle : ToAnsiZ et les aides CRT, les enveloppes
// Win32 et winsys3d, et les globales `.data` restees chez leur proprietaire (voir plus bas).
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Les ressources : les chemins CD, l'archive <c>BLAZE.ALL</c>, le decoupage des entrees de
/// ressource et leur remise en place, le decodage des images TIM, les tables de sauts d'entites
/// et de zones, et l'ecran de chargement (Ghidra: 0x00529 · 0x0052a).
///
/// <para>
/// Aucun membre ne reste <c>static</c> : ni rappel natif, ni pointeur de fonction, ni decision
/// d'hote. C'est la premiere conversion entierement d'instance.
/// </para>
/// <para>
/// <b>Les trois chaines de tete cessent d'etre un piege d'ordre.</b> <c>s_ArchiveFileName</c>,
/// <c>s_SystemFileName</c> et <c>s_SoundArchiveTemplate</c> etaient des champs
/// <c>static readonly</c> initialises par <c>ToAnsiZ</c> - une methode qui vit dans un AUTRE
/// fichier (<c>Platform/Startup.cs</c>). C'est exactement la forme du defaut qui a coute un
/// plantage au demarrage le 2026-08-19 : entre fichiers, l'ordre d'initialisation des champs
/// statiques n'est pas specifie. Devenus champs d'instance, ils sont construits au premier acces
/// a <c>Res</c>, donc apres <c>Blaze</c>. Le risque disparait au lieu d'etre documente.
/// </para>
/// <para>
/// Ce que la signature ne dit pas encore : six globales `.data` traversent toujours la frontiere
/// de cette classe sans passer par <see cref="GameMemory"/> - elles sont restees chez leur
/// proprietaire, exposees <c>internal</c>. Leur rapatriement est l'etape A inachevee, traitee
/// separement pour rester verifiable ; <c>s_MenuRenderBlock_00566c70</c> en est exclu tant que le
/// defaut d'aliasing de <c>0x00566c70</c> n'est pas tranche.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Resources(GameMemory mem, Primitives2d prims, Input inp, Audio aud)
{
    // =====================================================================
    // Resource-file loader subsystem (Ghidra: FUN_00529b00/FUN_00529b50/FUN_00532070/FUN_005322f0/
    // FUN_0052a660), used by LoadFirstLevelResources (FUN_0052a030) below. Everything here loads
    // from "BLAZE.ALL", a monolithic resource archive on the CD (sector-addressed â€” offsets are in
    // 2048-byte units, confirmed by the `<< 0xb` shift in LoadResourceEntry), plus a couple of
    // standalone files ("SYSTEM.INI"-equivalent, "BASE.SND"-equivalent) loaded whole.
    // =====================================================================

    // Ghidra: PTR_s_BLAZE_ALL_1_00566bbc -> 0x005684b0, PTR_s_SYSTEM_INI_1_00566bb8 -> 0x005684bc.
    // Recovered verbatim from Legacy/Binaries/Blaze.exe â€” the `;1` is an ISO-9660 file version, and Ghidra's
    // "_1_" in the symbol names was echoing it all along. Keep the suffix: NormalizePath truncates
    // at ';' before the open, exactly as winsys3d.dll does. See Documentation/Reference/DataSymbols.md.
    private readonly byte* s_ArchiveFileName = ToAnsiZ("BLAZE.ALL;1");
    private readonly byte* s_SystemFileName = ToAnsiZ("SYSTEM.INI;1");

    // Ghidra: s____snd_Base_snd_00568500, recovered as "@:\snd\Base.snd". A CD-**root** absolute
    // template with its own '@' drive-letter placeholder â€” it does NOT go through the data root.
    // Lowercase "snd", matching the disc's E:\Snd\ directory (Windows is case-insensitive here).
    private readonly byte* s_SoundArchiveTemplate = ToAnsiZ("@:\\snd\\Base.snd");

    /// <summary>
    /// Builds a **data-root-relative** path: the data root (Ghidra DAT_005631f8, "@:\FRENCH\" with
    /// its drive letter patched) followed by a relative filename. This is the family that covers
    /// BLAZE.ALL;1, SYSTEM.INI;1, DATA/Font.bmp, DATA/TITLE640.BMP and the three intro movies.
    /// <para>
    /// It is <b>not</b> the only family â€” see <see cref="BuildCdRootPath"/> for templates that are
    /// already absolute and address the CD root instead.
    /// </para>
    /// </summary>
    internal byte* BuildCdPath(byte* fileName)
    {
        byte* path = (byte*)Malloc(64);
        int i = 0;
        for (byte* p = mem.s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = fileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;
        return path;
    }

    /// <summary>
    /// Builds a **CD-root** path from a template that is already absolute and carries its own '@'
    /// drive-letter placeholder â€” e.g. "@:\snd\Base.snd" (Ghidra: s____snd_Base_snd_00568500) and
    /// the music table's "@:/Music/Mel00_01.WAV" entries. Only index 0 is substituted; the rest of
    /// the template is copied verbatim, separators and case included.
    /// <para>
    /// These live at the CD root, not under the language directory, which the disc layout confirms:
    /// <c>E:\snd\</c> and <c>E:\Music\</c> sit beside <c>E:\French\</c> rather than inside it. Running
    /// them through <see cref="BuildCdPath"/> would prepend the data root and never resolve.
    /// </para>
    /// </summary>
    private byte* BuildCdRootPath(byte* absoluteTemplate)
    {
        byte* path = (byte*)Malloc(64);
        int i = 0;
        for (byte* p = absoluteTemplate; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;
        if (i > 0) path[0] = mem.s_CdDataRootPath[0]; // the detected drive letter
        return path;
    }

    /// <summary>
    /// Ghidra: FUN_0052a660 @ 0x0052a660 — teste si <paramref name="dest"/> est l'une des trois
    /// destinations « table de sauts » ; si oui, il installe une table de pointeurs de code au lieu
    /// de charger les octets du fichier de ressources.
    ///
    /// <para><b>Correction du 2026-08-18 : les trois cibles ne sont pas des pointeurs et elles
    /// sont bel et bien assignées.</b> La note précédente disait « never assigned anywhere reached
    /// yet » ; c'est faux. <see cref="RunApplication"/> — reconstruite depuis longtemps — les pose
    /// juste avant le balayage des lecteurs (<c>Blaze.c:61733</c>) :
    /// <c>DAT_006008c8 = 1; DAT_00604930 = 2; DAT_00600904 = 3;</c>. Ce sont trois **sentinelles
    /// entières**, pas des adresses. Tant qu'elles restaient nulles côté port, la comparaison
    /// `dest == null` réussissait par accident sur la première et le mécanisme entier était faux.</para>
    ///
    /// <para><b>Anatomie de la branche 0x00604930</b>, lue le 2026-08-18 : elle copie
    /// <b>0x20 dwords</b> depuis <c>(&amp;PTR_PTR_00566d68)[DAT_005de528 &amp; 0xff]</c> — la zone
    /// courante — vers <c>&amp;PTR_FUN_0056b4d0</c>. La table d'entrée compte <b>onze</b> zones,
    /// relevées à 0x00566d68 : 0x55e8a8, 0x55e8e8, 0x55e928 … 0x55eb28, puis un dword nul.
    /// <c>PTR_FUN_0056b4f0</c>, que <c>FUN_00516250</c> appelle indirectement, est l'entrée 8 de la
    /// table de destination — et vaut <c>FUN_00401fa0</c>, un corps vide, avant toute installation.</para>
    ///
    /// <para>TODO(REVERSE) : les onze tables sont espacées de <b>0x40</b> octets alors que la copie
    /// en transfère <b>0x80</b> ; chaque zone déborde donc sur la suivante. Défaut d'origine ou
    /// tables volontairement à moitié partagées, non tranché — il faut lire le contenu de
    /// 0x0055e8a8 avant de conclure. Le port ne transcrit pas encore l'installation : les 32
    /// pointeurs sont des adresses de code, et les rendre appelables demande un mécanisme de
    /// vtable que rien de reconstruit n'exerce pour l'instant. L'inventer maintenant serait de
    /// l'échafaudage spéculatif.</para>
    /// </summary>
    private bool ApplyResourceDestinationOverride(void* dest)
    {
        if (dest == s_OverrideDestA_006008c8) return true; // Ghidra: pas de corps du tout ici — juste `return 1;`

        // **Effet de bord sur le chemin normal**, longtemps omis par le port : le nettoyage
        // ci-dessous n'est PAS dans une branche, il court a chaque chargement de ressource dont la
        // destination n'est pas la sentinelle A. Trente-neuf dwords remis a zero, puis le curseur
        // de +0x6c pointe sur le debut du bloc qu'on vient d'effacer.
        NativeMemory.Clear(s_EntityList_005de558, 0x27 * sizeof(uint));
        *(byte**)(s_EntityList_005de558 + 0x6c) = s_EntityList_005de558;   // Ghidra: DAT_005de5c4

        if (dest == s_OverrideDestB_00604930)
        {
            InstallAreaHandlerTable(s_Unknown_005de528 & 0xff);
            return true;
        }

        if (dest == s_OverrideDestC_00600904)
        {
            // Ghidra: `iVar1 = (zone & 0xff) * 0x10 + (sous-zone & 0xff)`, force a 0 quand la zone
            // est 0, et FUN_00536170() (l'arret fatal) si la sous-zone depasse 0xf.
            int index;
            if ((s_Unknown_005de528 & 0xff) == 0) index = 0;
            else
            {
                if ((s_Unknown_005de52c & 0xff) > 0xf)
                    throw new NotImplementedException(
                        "FUN_00536170 : l'arret fatal sur sous-zone > 0xf, non transcrit");
                index = (s_Unknown_005de528 & 0xff) * 0x10 + (s_Unknown_005de52c & 0xff);
            }

            InstallEntityVTables(index);
            return true;
        }
        return false;
    }

    // ---------------------------------------------------------------------------------------
    // Les sept tables de sauts d'entites (Ghidra: la branche DAT_00600904 de FUN_0052a660).
    // ---------------------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: les sept tables que la branche <c>DAT_00600904</c> installe d'affilee. Chacune est
    /// choisie dans une table de pointeurs indexee par <c>zone * 0x10 + sous-zone</c> ; les valeurs
    /// ci-dessous sont celles de l'**index 0**, la seule que la carte du monde atteint, relevees
    /// dans <c>Legacy/Binaries/Blaze.exe</c> et **generees mecaniquement** plutot que recopiees a la main.
    ///
    /// <para><c>0x00401FA0</c> revient partout : c'est la fonction au corps vide. La table
    /// <c>0056adc8</c> est la seule pleine — soixante-quatre entrees, dont dix-neuf distinctes puis
    /// <c>0x004734B0</c> repete, la forme classique d'une table virtuelle avec son gestionnaire par
    /// defaut.</para>
    /// </summary>
    private readonly uint[] s_VtSource_0056ad48 =
    [
        0x00486EE0, 0x00487260, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
    ];

    private readonly uint[] s_VtSource_0056adc8 =
    [
        0x004734B0, 0x004734B0, 0x004736D0, 0x004738A0, 0x00473A90, 0x00473CB0, 0x00473A70, 0x00473A70,
        0x004508D0, 0x004509B0, 0x00450AA0, 0x00450BD0, 0x00450C60, 0x00473ED0, 0x004746C0, 0x004508B0,
        0x00450760, 0x004502E0, 0x00450D30, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
        0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
        0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
        0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
        0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
        0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0, 0x004734B0,
    ];

    private readonly uint[] s_VtSource_0056aec8 =
    [
        0x00443100, 0x00443260, 0x004430B0, 0x00442F60, 0x00442F20, 0x00442E70, 0x00442EA0, 0x00442E20,
        0x00445840, 0x004431F0, 0x004441F0, 0x00442E10, 0x00442DB0, 0x004457F0, 0x00442D90, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x0053DD20, 0x0053DEA0, 0x0053DF20, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
    ];

    private readonly uint[] s_VtSource_0056afc8 =
    [
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x0053DCB0, 0x00401FA0, 0x0053DF90, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
    ];

    private readonly uint[] s_VtSource_0056b0c8 =
    [
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00404EF0, 0x004055C0, 0x00405A70, 0x00405D60, 0x00405E70, 0x00405FE0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00404B90, 0x00404E50,
    ];

    private readonly uint[] s_VtSource_0056b1c8 =
    [
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
    ];

    private readonly uint[] s_VtSource_0056b2c8 =
    [
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0,
        0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00401FA0, 0x00404C80,
    ];

    /// <summary>Les sept tables actives, dans l'ordre ou l'original les remplit.</summary>
    private readonly uint[] s_Vt_0056ad48 = new uint[0x20];
    private readonly uint[] s_Vt_0056adc8 = new uint[0x40];
    private readonly uint[] s_Vt_0056aec8 = new uint[0x40];
    internal readonly uint[] s_Vt_0056afc8 = new uint[0x40];
    private readonly uint[] s_Vt_0056b0c8 = new uint[0x40];
    private readonly uint[] s_Vt_0056b1c8 = new uint[0x40];
    private readonly uint[] s_Vt_0056b2c8 = new uint[0x40];

    private void InstallEntityVTables(int index)
    {
        if (index != 0)
            throw new NotImplementedException(
                $"Tables de sauts d'entites, index {index} (zone * 0x10 + sous-zone) : seules les " +
                "valeurs de l'index 0 sont relevees dans Legacy/Binaries/Blaze.exe. Relever les sept " +
                "tables de cet index avant d'aller plus loin — voir REVERSE_STATUS.md");

        Array.Copy(s_VtSource_0056ad48, s_Vt_0056ad48, s_Vt_0056ad48.Length);
        Array.Copy(s_VtSource_0056adc8, s_Vt_0056adc8, s_Vt_0056adc8.Length);
        Array.Copy(s_VtSource_0056aec8, s_Vt_0056aec8, s_Vt_0056aec8.Length);
        Array.Copy(s_VtSource_0056afc8, s_Vt_0056afc8, s_Vt_0056afc8.Length);
        Array.Copy(s_VtSource_0056b0c8, s_Vt_0056b0c8, s_Vt_0056b0c8.Length);
        Array.Copy(s_VtSource_0056b1c8, s_Vt_0056b1c8, s_Vt_0056b1c8.Length);
        Array.Copy(s_VtSource_0056b2c8, s_Vt_0056b2c8, s_Vt_0056b2c8.Length);

        // Ghidra: les onze initialiseurs appeles a la suite des sept copies.
        Init_004502d0(); Init_0045a220(); Init_0045c1f0(); Init_0045e200(); Init_004610a0();
        Init_00467c00(); Init_00468570(); Init_0046a770(); Init_0046c530(); Init_0046e2b0();
        Init_00470250();
    }

    /// <summary>L'appel indirect a travers une des sept tables. Comme
    /// <see cref="CallAreaHandler"/> : <c>FUN_00401FA0</c> a un corps vide, tout le reste leve.</summary>
    internal void CallEntityHandler(uint[] table, int slot, string name)
    {
        uint target = table[slot];
        if (target == 0x00401FA0) return;   // corps vide, verifie dans Blaze.c

        throw new NotImplementedException(
            $"{name}, entree {slot} = 0x{target:X6} : gestionnaire d'entite non transcrit");
    }

    private void Init_004502d0() => *mem.EntityState(0x00571540) = 0;
    private void Init_00467c00() => *mem.EntityState(0x005717c8) = 0;
    private void Init_00468570() { *mem.EntityState(0x00571828) = 0; *mem.EntityState(0x0057182c) = 0; }
    private void Init_0046a770() { *mem.EntityState(0x00571888) = 0; *mem.EntityState(0x0057188c) = 0; }
    private void Init_0046c530() { *mem.EntityState(0x005718b8) = 0; *mem.EntityState(0x005718bc) = 0; }
    private void Init_0046e2b0() { *mem.EntityState(0x00571918) = 0; *mem.EntityState(0x0057191c) = 0; }
    private void Init_00470250() { *mem.EntityState(0x00571978) = 0; *mem.EntityState(0x0057197c) = 0; }

    private void Init_004610a0()
    {
        for (int i = 0; i < 8; i++) *mem.EntityParam(0x005517e8 + i * 4) = unchecked((int)0xfffffaec);
        for (int i = 0; i < 8; i++) *mem.EntityParam(0x005517c8 + i * 4) = 0x25d;
        for (int i = 0; i < 8; i++) *mem.EntityParam(0x005517a8 + i * 4) = 100;
    }

    /// <summary>Ghidra: FUN_0045a220, FUN_0045c1f0 et FUN_0045e200 — trois blocs de parametres de
    /// classe, les memes valeurs a des adresses decalees de 0x60 pour les deux premiers. Le
    /// troisieme reordonne ses champs et n'est donc pas un simple decalage : transcrit separement.</summary>
    private void Init_0045a220()
    {
        *mem.EntityParam(0x00551660) = 0x9c4;
        *mem.EntityParam(0x00551664) = 0x9c4;
        *mem.EntityState(0x00571690) = 0;
        *mem.EntityParam(0x00551668) = unchecked((int)0xfffff704);
        *mem.EntityParam(0x0055166c) = unchecked((int)0xfffff704);
        for (int va = 0x00571694; va <= 0x005716b0; va += 4) *mem.EntityState(va) = 0;
        for (int va = 0x00551680; va <= 0x00551690; va += 4) *mem.EntityParam(va) = 400;
        *mem.EntityParam(0x00551694) = 0;
        *mem.EntityParam(0x00551698) = 0;
        *mem.EntityParam(0x0055169c) = 400;
        *mem.EntityParam(0x005516a0) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x005516a4) = 100;
        *mem.EntityParam(0x005516a8) = 100;
        *mem.EntityParam(0x005516ac) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x00551670) = unchecked((int)0xfffff3e4);
        *mem.EntityParam(0x00551674) = unchecked((int)0xfffffa24);
        *mem.EntityParam(0x00551678) = 0x6a4;
        *mem.EntityParam(0x0055167c) = 0xce4;
        *mem.EntityParam(0x005516b0) = 0xb54;
        *mem.EntityParam(0x005516b4) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x005516b8) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x005516bc) = 0xb54;
    }

    private void Init_0045c1f0()
    {
        *mem.EntityParam(0x005516c0) = 0x9c4;
        *mem.EntityParam(0x005516c4) = 0x9c4;
        *mem.EntityState(0x005716e0) = 0;
        *mem.EntityParam(0x005516c8) = unchecked((int)0xfffff704);
        *mem.EntityParam(0x005516cc) = unchecked((int)0xfffff704);
        for (int va = 0x005716e4; va <= 0x00571700; va += 4) *mem.EntityState(va) = 0;
        for (int va = 0x005516e0; va <= 0x005516f0; va += 4) *mem.EntityParam(va) = 400;
        *mem.EntityParam(0x005516f4) = 0;
        *mem.EntityParam(0x005516f8) = 0;
        *mem.EntityParam(0x005516fc) = 400;
        *mem.EntityParam(0x00551700) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x00551704) = 100;
        *mem.EntityParam(0x00551708) = 100;
        *mem.EntityParam(0x0055170c) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x005516d0) = unchecked((int)0xfffff3e4);
        *mem.EntityParam(0x005516d4) = unchecked((int)0xfffffa24);
        *mem.EntityParam(0x005516d8) = 0x6a4;
        *mem.EntityParam(0x005516dc) = 0xce4;
        *mem.EntityParam(0x00551710) = 0xb54;
        *mem.EntityParam(0x00551714) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x00551718) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x0055171c) = 0xb54;
    }

    private void Init_0045e200()
    {
        *mem.EntityParam(0x00551738) = 0x9c4;
        *mem.EntityParam(0x0055173c) = 0x9c4;
        for (int va = 0x00571730; va <= 0x00571750; va += 4) *mem.EntityState(va) = 0;
        *mem.EntityParam(0x00551740) = unchecked((int)0xfffff704);
        *mem.EntityParam(0x00551744) = unchecked((int)0xfffff704);
        *mem.EntityParam(0x0055176c) = 0;
        *mem.EntityParam(0x00551770) = 0;
        for (int va = 0x00551758; va <= 0x00551768; va += 4) *mem.EntityParam(va) = 400;
        *mem.EntityParam(0x00551774) = 400;
        *mem.EntityParam(0x0055177c) = 100;
        *mem.EntityParam(0x00551780) = 100;
        *mem.EntityParam(0x00551748) = unchecked((int)0xfffff3e4);
        *mem.EntityParam(0x0055174c) = unchecked((int)0xfffffa24);
        *mem.EntityParam(0x00551750) = 0x6a4;
        *mem.EntityParam(0x00551754) = 0xce4;
        *mem.EntityParam(0x00551778) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x00551784) = unchecked((int)0xfffffed4);
        *mem.EntityParam(0x00551788) = 0xf3c;
        *mem.EntityParam(0x0055178c) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x00551790) = unchecked((int)0xfffff63c);
        *mem.EntityParam(0x00551794) = 0xb54;
    }

    /// <summary>Ghidra: &amp;DAT_005de558 — trente-neuf dwords remis a zero a chaque chargement de
    /// ressource, avec un curseur a <c>+0x6c</c> (<c>DAT_005de5c4</c>) qui repointe sur leur debut.
    /// Une douzaine de lecteurs dans le code de jeu, aucun encore transcrit.</summary>
    private readonly byte* s_EntityList_005de558 = (byte*)NativeMemory.AllocZeroed(0x9c);

    /// <summary>
    /// Ghidra: &amp;PTR_PTR_00566d68 et les onze tables qu'elle designe — un bloc plat de 192 dwords
    /// a partir de 0x0055e8a8, releve dans <c>Legacy/Binaries/Blaze.exe</c>. Les tables de zone y commencent
    /// tous les <b>0x40</b> octets, et 174 des 192 entrees valent <c>FUN_00401FA0</c>, dont le corps
    /// est vide — d'ou la construction par remplissage puis dix-huit exceptions.
    /// </summary>
    private readonly uint* s_AreaHandlerSource_0055e8a8 = BuildAreaHandlerSource();

    private static uint* BuildAreaHandlerSource()
    {
        uint* block = (uint*)NativeMemory.Alloc(192, sizeof(uint));
        for (int i = 0; i < 192; i++) block[i] = 0x00401FA0;

        block[0x020 / 4] = 0x004747F0;   // zone 0, entree 8
        block[0x060 / 4] = 0x0040A600;   // zone 1, entree 8 — corps vide lui aussi

        // Les seize fonctions qui suivent la derniere table de zone. Elles n'appartiennent a
        // aucune zone : seul le debordement decrit ci-dessous les fait entrer dans la table.
        uint[] tail =
        [
            0x005037D0, 0x005037E0, 0x00503850, 0x005038A0, 0x00503990, 0x00503B50,
            0x00503D30, 0x00503D70, 0x00503D80, 0x00503D90, 0x00503DB0, 0x00503DD0,
            0x00503DF0, 0x00503E10, 0x00503E50, 0x00503E80,
        ];
        for (int i = 0; i < tail.Length; i++) block[(0x2c0 / 4) + i] = tail[i];

        return block;
    }

    /// <summary>
    /// Ghidra: &amp;PTR_FUN_0056b4d0 — la table de sauts active. **Seize** entrees utiles : le relevé
    /// du binaire montre 0..15 initialisees a <c>FUN_00401FA0</c> et 16..31 a zero, ce qui borne la
    /// table sans ambiguite. Les seize suivantes sont conservees ici parce que l'installation
    /// deborde dessus — voir <see cref="InstallAreaHandlerTable"/>.
    /// </summary>
    private readonly uint* s_AreaHandlerTable_0056b4d0 = BuildAreaHandlerTable();

    private static uint* BuildAreaHandlerTable()
    {
        uint* block = (uint*)NativeMemory.AllocZeroed(32, sizeof(uint));
        for (int i = 0; i < 16; i++) block[i] = 0x00401FA0;
        return block;
    }

    /// <summary>
    /// Ghidra: la branche <c>DAT_00604930</c> de <c>FUN_0052a660</c> — installe la table de sauts
    /// de la zone courante.
    ///
    /// <para><b>Elle copie 0x20 dwords alors que la table n'en fait que 0x10</b> : c'est un
    /// depassement, des deux cotes. En lecture, chaque zone empiete sur la premiere moitie de la
    /// suivante (les onze tables sont espacees de 0x40, la copie en prend 0x80) ; en ecriture, les
    /// seize dwords excedentaires tombent au-dela de la table de destination. Voir
    /// <c>legacy-bug.md</c>, LB-006 — transcrit tel quel, pas corrige.</para>
    /// </summary>
    private void InstallAreaHandlerTable(int area)
    {
        uint* source = s_AreaHandlerSource_0055e8a8 + area * (0x40 / 4);
        for (int i = 0; i < 0x20; i++) s_AreaHandlerTable_0056b4d0[i] = source[i];
    }

    /// <summary>
    /// L'appel indirect a travers <see cref="s_AreaHandlerTable_0056b4d0"/>. Les deux adresses que
    /// les zones 0 a 9 installent reellement — <c>FUN_00401FA0</c> et <c>FUN_0040A600</c> — ont un
    /// corps <b>vide</b> dans <c>Blaze.c</c>, donc l'appel est un no-op verifiable et non une
    /// supposition. Toute autre adresse leve, en la nommant.
    /// </summary>
    internal void CallAreaHandler(int slot)
    {
        uint target = s_AreaHandlerTable_0056b4d0[slot];
        if (target == 0x00401FA0 || target == 0x0040A600) return;   // corps vides, verifies

        // 0x004747F0 : la transition de sous-zone de la zone 0. **Elle n'est pas dans `Blaze.c`** —
        // Ghidra ne l'a pas decompilee — et elle a ete transcrite le 2026-08-21 depuis le
        // desassemblage produit par `OpenForesia.Tools disasm`. Voir RunArea0Handler8 dans
        // Screens/State03/WorldMap.cs pour le detail et le releve conserve.
        //
        // L'echafaudage OPENFORESIA_SKIP_AREA_HANDLER qui vivait ici est retire : il n'a plus
        // d'objet, et le garder aurait laisse un moyen de sauter du code desormais present.
        if (target == 0x004747F0) { RunArea0Handler8(); return; }

        throw new NotImplementedException(
            $"Table de sauts de zone, entree {slot} = 0x{target:X6} : cette adresse n'a pas encore " +
            "de corps reconstruit. Si elle n'est pas dans Blaze.c non plus, la desassembler : " +
            $"`OpenForesia.Tools disasm Legacy/Binaries/Blaze.exe 0x{target:x8} 512`. " +
            "Voir Documentation/Etat/REVERSE_STATUS.md, section du 2026-08-21.");
    }

    // Ghidra: DAT_006008c8 / DAT_00604930 / DAT_00600904 — les trois sentinelles de destination,
    // posées à 1, 2 et 3 par RunApplication. Typées `void*` parce que c'est à un `void*` qu'elles
    // sont comparées, pas parce qu'elles pointent sur quoi que ce soit.
    internal void* s_OverrideDestA_006008c8;
    internal void* s_OverrideDestB_00604930;
    internal void* s_OverrideDestC_00600904;

    /// <summary>
    /// Ghidra: FUN_00529b00 @ 0x00529b00. Opens `path`, reads the whole file into `dest`, closes
    /// it, and returns the next dword-aligned address past the loaded data (bump-allocator style â€”
    /// matches LoadResourceEntry's own return convention below). TODO(REVERSE): Ghidra's own
    /// decompilation of this function is incomplete â€” the failure path returns `unaff_ESI` (a
    /// register Ghidra could not attribute a value to at function entry) and the read call shows a
    /// literal `2` where a destination-buffer argument would be expected
    /// (`Ordinal_334(iVar1,2,iVar2)`), one parameter short of the two arguments every real call
    /// site actually passes. Reconstructed here using `dest` as that argument (the only reading
    /// consistent with both call sites) and 0 as a safe/explicit failure return, not Ghidra's
    /// unrecoverable `unaff_ESI`.
    /// </summary>
    private byte* LoadWholeFileToBuffer(byte* path, byte* dest)
    {
        IntPtr handle = OpenExistingFileReadOnly(path);
        if ((int)handle < 0) return null; // TODO(REVERSE): see summary above â€” original's real failure value unrecoverable

        uint size = Win32.SetFilePointer(handle, 0, null, 2 /* FILE_END */);
        Win32.SetFilePointer(handle, 0, null, 0 /* FILE_BEGIN */);
        if (dest != null)
        {
            uint bytesRead;
            Win32.ReadFile(handle, dest, size, &bytesRead, null);
        }
        Win32.CloseHandle(handle);

        return (byte*)(((nint)dest + size + 5) & ~3);
    }

    /// <summary>
    /// Ghidra: FUN_00529b50 @ 0x00529b50. Loads one sector-addressed chunk (`entryPtr[0]` = sector
    /// offset, `entryPtr[1]` = byte size) out of BLAZE.ALL into `dest`, unless `dest` is one of the
    /// ApplyResourceDestinationOverride special cases. Returns the next dword-aligned address past the
    /// loaded data.
    /// </summary>
    internal byte* LoadResourceEntry(int* entryPtr, byte* dest)
    {
        if (ApplyResourceDestinationOverride(dest)) return dest;

        byte* path = BuildCdPath(s_ArchiveFileName);
        IntPtr handle = OpenExistingFileReadOnly(path);
        Free(path);
        if ((int)handle < 0)
        {
            // Ghidra: `/* WARNING: Do nothing block with infinite loop */ do {} while (true);` â€”
            // preserved faithfully rather than silently changed to a graceful failure; the original
            // hangs here instead of continuing with an invalid handle.
            while (true) { }
        }

        Win32.SetFilePointer(handle, entryPtr[0] << 0xb, null, 0 /* FILE_BEGIN */);
        int size = entryPtr[1];
        uint bytesRead;
        Win32.ReadFile(handle, dest, (uint)size, &bytesRead, null);
        Win32.CloseHandle(handle);

        return (byte*)(((nint)dest + size + 3) & ~3);
    }

    // Ghidra: DAT_0056aca8 â€” a ushort-driven schema table describing which fields of a loaded
    // resource blob need base-relative-to-absolute pointer fixups (see FixupResourceHeader below).
    // TODO(REVERSE): data-only global, byte content not present in Blaze.c's text; left empty, so
    // the schema-driven second fixup pass below is a safe (but incomplete) no-op â€” same category of
    // gap as s_ResolutionDetailLimit elsewhere in this file.
    // Ghidra: DAT_0056aca8 â€” recovered from Legacy/Binaries/Blaze.exe, 37 ushorts ending at the 0x0000 the
    // original's loop terminates on. The terminator is NOT stored here; the loop below runs to the
    // array's length instead, which is equivalent and keeps the sentinel out of the data.
    //
    // Each entry packs two fields, and this is confirmed by FUN_00532070's own masking rather than
    // inferred: the **low byte is an index** into the header's already-relocated pointer array
    // (`header[4 + index]`), and the **high byte is the byte stride** used to walk that list.
    // The earlier "(record kind, size)" reading in an older revision of DataSymbols.md was wrong.
    //
    // Every index is below 0x3c, matching the 60 dwords the fixed pass relocates â€” an internal
    // consistency check the recovered bytes pass.
    private readonly ushort[] s_ResourceFixupSchema_0056aca8 =
    [
        0x0800, 0x0801, 0x0802, 0x0803, 0x0804, 0x0805, 0x0806, 0x0807,
        0x0408, 0x0409, 0x040a, 0x140b, 0x040e, 0x0410, 0x0411, 0x0414,
        0x0815, 0x0417, 0x0418, 0x0419, 0x041b, 0x041c, 0x081d, 0x041e,
        0x041f, 0x0421, 0x0423, 0x0425, 0x0426, 0x0827, 0x0428, 0x042a,
        0x042b, 0x042d, 0x042f, 0x0430, 0x0431,
    ];

    /// <summary>
    /// Ghidra: FUN_00532070 @ 0x00532070. Turns a just-loaded resource blob's stored
    /// "base-relative" offsets into real pointers, now that its load address is known. Two passes:
    /// a fixed run of 60 header dwords, then a schema-driven walk over the chained lists those
    /// dwords point at.
    /// <para>
    /// The two passes use **different** relocation formulas â€” the fixed pass adds
    /// <c>header[0]</c>, the schema pass adds <c>header[0] + header[1]</c>. That extra term is not
    /// incidental; dropping it yields pointers that look valid and are wrong by a constant.
    /// </para>
    /// </summary>
    internal void FixupResourceHeader(byte* blob)
    {
        if (blob == null) return;

        // Ghidra: iVar1 = *param_1; the fixed 60-dword pass over blob[4..0x40).
        int origin = *(int*)blob;
        int* field = (int*)blob + 4;
        for (int i = 0; i < 0x3c; i++, field++)
        {
            *field = (int)(nint)blob + *field + origin;
        }

        // Ghidra: iVar3 = param_1[1]; the schema pass's extra relocation term.
        int recordOrigin = ((int*)blob)[1];

        // Ghidra: the `while (uVar5 != 0)` walk over DAT_0056aca8. Each schema entry names one of
        // the pointers the pass above just relocated, plus the stride to walk the list it heads.
        foreach (ushort entry in s_ResourceFixupSchema_0056aca8)
        {
            int listIndex = entry & 0xff;
            int stride = entry >> 8;

            int* node = (int*)(nint)((int*)blob)[4 + listIndex];

            // Ghidra checks both the pointer and its first dword â€” an empty list is stored as a
            // valid pointer to a zero, not as a null.
            if (node == null) continue;
            int value = *node;
            if (value == 0) continue;

            // Read-then-advance, in the original's order: relocate the current dword, then take the
            // next link from `node + stride`, then step. Reordering these changes which dword ends
            // the walk.
            do
            {
                *node = (int)(nint)blob + value + recordOrigin + origin;
                value = *(int*)((byte*)node + stride);
                node = (int*)((byte*)node + stride);
            } while (value != 0);
        }
    }

    /// <summary>
    /// <summary>
    /// libgpu's `TIM_IMAGE`, filled by ReadTIM. Exactly the 20 bytes FUN_005322f0 reserves with
    /// `SUB ESP,0x14` and hands to Ordinal_662 via `LEA ECX,[ESP+8]`.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct TimImage
    {
        public uint Mode;        // +0x00 â€” pixel mode; header & 7, or 2 when the TIM has no CLUT
        public short* ClutRect;  // +0x04 â€” RECT {x,y,w,h}; null when there is no CLUT
        public void* ClutData;   // +0x08 â€” palette bytes; null when there is no CLUT
        public short* PixelRect; // +0x0c â€” RECT {x,y,w,h}
        public void* PixelData;  // +0x10 â€” pixel bytes
    }

    /// <summary>
    /// Ghidra: FUN_005322f0 @ 0x005322f0 â€” uploads one TIM texture (loaded by LoadResourceEntry
    /// into a scratch buffer) into VRAM, and records its texture-page / CLUT pair in a slot.
    /// <para>
    /// Reconstructed from the raw disassembly in <c>Legacy/Raw/FUN_005322f0.txt</c>, because the
    /// decompiler's output for this function was not merely unnamed but **wrong**:
    /// </para>
    /// <list type="bullet">
    /// <item>Its `puStack_14`/`psStack_10`/`iStack_c` "indeterminate" locals are the fields of the
    /// libgpu <c>TIM_IMAGE</c> that <c>Ordinal_662</c> (ReadTIM) fills through the out-pointer
    /// `&amp;stack0xffffffe8`. Ghidra never linked the pointer to the slots it writes.</item>
    /// <item>`unaff_retaddr` is not a return address: it is **parameter 2**, the 2-short texture
    /// slot. Both call sites advance their slot pointer by exactly 4 bytes, which corroborates it.</item>
    /// <item>Every `param_1[0..2]` byte read in the decompiled body actually comes from
    /// **`EDI` = parameter 3**, the metadata triple â€” not from the chunk pointer.</item>
    /// <item>`(*param_1 &amp; 0xff10)` is a decompiler artifact. The instruction is `AND AL,0x10`,
    /// a byte mask; using `0xff10` would corrupt the pixel rect's y coordinate.</item>
    /// </list>
    /// <para>
    /// The `extraout_var*` values Ghidra invented for this function are simply Ordinal_674's
    /// ignored return value and have no role at all.
    /// </para>
    /// </summary>
    internal void DecodeResourceChunk(byte* timData, ushort* slot, byte* meta)
    {
        // Ghidra: PUSH arg1; CALL Ordinal_677; LEA ECX,[ESP+8]; PUSH ECX; CALL Ordinal_662.
        // The 20 bytes SUB ESP,0x14 reserves are exactly this struct.
        TimImage tim;
        Winsys3d.OpenTim(timData);
        Winsys3d.ReadTim(&tim);

        // ESI carries the CLUT's byte count from the branch below into the copy loop at the end.
        // It is only meaningful when a CLUT exists; the no-CLUT path loads arg1 into it (Ghidra:
        // `MOV ESI,[ESP+0x20]`) but that value is dead, because ReadTIM zeroes `caddr` in the same
        // case and `caddr` guards the loop.
        int clutByteCount = 0;

        // Ghidra: MOV EAX,[ESP+0x10]; TEST EAX,EAX; JZ â€” the test is on **caddr**, not crect.
        if (tim.ClutData != null)
        {
            short* clutRect = tim.ClutRect;

            clutRect[0] = 0;                                    // x = 0
            clutRect[1] = (short)(meta[1] + 0x1c0);             // y = meta[1] + 0x1c0

            // Read w and h *before* w is overwritten â€” the original does the same, and the
            // multiply below depends on the original width.
            int packed = clutRect[3] * clutRect[2];             // h * w
            packed = (packed + 0xff) & unchecked((int)0xffffff00);
            clutRect[2] = 0x100;                                // w = 256
            clutRect[3] = (short)(packed >> 8);                 // h = rounded / 256
            clutByteCount = packed;

            Winsys3d.Ordinal_674(clutRect, tim.ClutData);                // LoadImage(crect, caddr)
        }

        // Ghidra: MOV EAX,[ESP+0x18] â€” guarded on **paddr**, plus a skip when meta[0] == 0x1c.
        if (tim.PixelData != null && meta[0] != 0x1c)
        {
            short* pixelRect = tim.PixelRect;

            // Ghidra: AND AL,0xf; SHL ECX,6.
            pixelRect[0] = (short)((meta[0] & 0x0f) << 6);

            // Ghidra: AND AL,0x10 â€” a *byte* mask. The decompiler rendered this as `& 0xff10`,
            // which is wrong and would have corrupted the y coordinate.
            pixelRect[1] = (short)((((meta[0] & 0x10) + meta[2] * 4)) << 4);

            Winsys3d.Ordinal_674(pixelRect, tim.PixelData);              // LoadImage(prect, paddr)
        }

        // Ghidra: 005323bf-005323f2. Packs the texture-page word out of the pixel rect and the TIM
        // mode â€” libgpu's GetTPage shape. Transcribed operation for operation rather than
        // simplified, since the intermediate 16-bit SAR is not equivalent to a 32-bit one.
        int x = tim.PixelRect[0];
        int y = tim.PixelRect[1];
        int tpage = ((x >> 2) & 0xf0) | (y & 0x100);
        tpage |= 0x2000;                                        // Ghidra: OR CH,0x20
        tpage = (short)tpage >> 4;                              // Ghidra: SAR CX,4 â€” 16-bit
        tpage |= ((int)tim.Mode & 3) << 7;
        slot[0] = (ushort)tpage;

        // Ghidra: (meta[1] + 0x1c0) << 6 â€” libgpu's GetClut for x=0, matching the crect y above.
        slot[1] = (ushort)((meta[1] + 0x1c0) << 6);

        // Ghidra: 00532407-0053243c. Mirrors the CLUT into main video memory for the low banks.
        if (meta[1] < 0x10 && tim.ClutData != null)
        {
            byte* mainMem = GetMainMemPtr();
            if (mainMem != null)
            {
                uint* source = (uint*)tim.ClutData;
                uint* dest = (uint*)(mainMem + ((meta[1] + 0xe10) << 9));
                for (int remaining = clutByteCount >> 1; remaining != 0; remaining--)
                {
                    *dest++ = *source++;
                }
            }
        }
    }

    // Ghidra: DAT_005e8fc0 â€” **not a second table**. It is DAT_005e8f00 offset by 0xC0, i.e. the
    // same tpage/clut array starting 48 entries in, and Ghidra simply named two points inside it.
    //
    // Modelling them as separate allocations is what left the main menu black: its panel
    // descriptor asks for entry 0x31 of DAT_005e8f00 = 0x31 - 0x30 = entry 1 of DAT_005e8fc0,
    // which LoadFirstLevelResources fills. LoadMenuResources only writes entries 0..3 (measured:
    // the menu's directory has exactly four), so entry 0x31 came back zero and every panel
    // primitive sampled texture page 0 â€” an empty region of VRAM, hence a black screen with
    // otherwise perfectly formed geometry.
    //
    // Same shape of bug as DAT_0056e61f being menuContext[0x8c7]: suspect any DAT_ that lands
    // inside a known structure's extent.
    private byte* s_EntityDecodeTable_005e8fc0 => mem.s_EntityDecodeTable_005e8f00 + 0xC0;
    // Ghidra: DAT_005e97e4 / _DAT_005e9810 / _DAT_005e9824 / DAT_005de538 â€” all read again by
    // not-yet-reconstructed code.
    internal uint s_Unknown_005e97e4;
    internal int s_Unknown_005e9810;
    private IntPtr s_Unknown_005e9824;
    // Deplace dans GameMemory le 2026-08-19 : globale .data lue par Engine/Input.cs.
    // Deplace dans GameMemory le 2026-08-19 : globale .data.

    /// <summary>
    /// Ghidra: FUN_0052a030 @ 0x0052a030. Loads the "&lt;cd-drive-root&gt;SYSTEM.INI"-equivalent
    /// level/base resource file, fixes up its internal pointers, decodes each directory entry it
    /// lists (throws at the first one â€” see DecodeResourceChunk above), loads the base sound
    /// archive's header + big scratch buffer, and resets the small entity-slot table.
    /// </summary>
    internal void LoadFirstLevelResources()
    {
        // Ghidra: FUN_0040a600() â€” confirmed no-op.
        byte* path = BuildCdPath(s_SystemFileName);
        mem.s_MainResourceDirectory_005e97c4 = LoadWholeFileToBuffer(path, mem.s_MainResourceBuffer_005e97c0);
        Free(path);
        FixupResourceHeader(mem.s_MainResourceBuffer_005e97c0);

        if (mem.s_MainResourceBuffer_005e97c0 != null)
        {
            int* directory = *(int**)(mem.s_MainResourceBuffer_005e97c0 + 0x10);
            int entryPtrValue = *directory;
            if (entryPtrValue != 0)
            {
                int* metadata = directory + 1;
                byte* destSlot = s_EntityDecodeTable_005e8fc0;
                byte* mainMem = GetMainMemPtr();
                do
                {
                    byte* scratch = mainMem != null ? mainMem + 0x1b0000 : null;
                    LoadResourceEntry((int*)(nint)entryPtrValue, scratch);
                    DecodeResourceChunk(scratch, (ushort*)destSlot, (byte*)metadata);
                    entryPtrValue = directory[2];
                    directory += 2;
                    // Ghidra: `undefined *puVar13; ... puVar13 = puVar13 + 4;` â€” a **byte** pointer
                    // advancing 4 bytes, i.e. one tpage/clut pair per entry. This was 16, a 4x
                    // over-stride that would have scattered the texture slots; it went unnoticed
                    // because DecodeResourceChunk threw before the loop could ever iterate.
                    destSlot += 4;
                    metadata += 2;
                } while (entryPtrValue != 0);
            }
        }

        uint savedStride = mem.s_ScreenStride_005e97e0;

        // CD-root family, not data-root: the sound bank lives at E:\Snd\, beside E:\French\ rather
        // than inside it. This previously went through BuildCdPath, which prefixed the data root
        // onto an already-absolute template and could never resolve.
        byte* sndPath = BuildCdRootPath(s_SoundArchiveTemplate);
        IntPtr sndHandle = OpenExistingFileReadOnly(sndPath);
        Free(sndPath);
        // Ghidra doesn't guard the open failing here either â€” ReadFile/CloseHandle on an invalid
        // handle simply fail harmlessly, matching the original's unconditional calls.
        uint sndBytesRead;
        // The first 60 directory entries, then the whole base sample bank they point into.
        Win32.ReadFile(sndHandle, mem.s_SoundDirectory_005e8340, 0x1e0, &sndBytesRead, null);
        if (mem.s_BaseSoundBank_00604920 != null)
        {
            Win32.ReadFile(sndHandle, mem.s_BaseSoundBank_00604920, 790000, &sndBytesRead, null);
        }
        Win32.CloseHandle(sndHandle);

        s_Unknown_005e97e4 = savedStride;
        if (mem.s_MainResourceBuffer_005e97c0 != null)
        {
            s_Unknown_005e9810 = *(int*)(mem.s_MainResourceBuffer_005e97c0 + 100);
        }
        s_Unknown_005e9824 = sndHandle;

        byte entryId = 0;
        byte* slotIdByte = mem.s_EntitySlotTable_005e9180 + 9; // Ghidra: &DAT_005e9189
        uint* slotBase = (uint*)mem.s_EntitySlotTable_005e9180; // Ghidra: &DAT_005e9180
        byte* tableEnd = mem.s_EntitySlotTable_005e9180 + (0x5e92b9 - 0x5e9180);
        do
        {
            uint* p = slotBase;
            for (int k = 0; k < 0x13; k++, p++) *p = 0;
            slotIdByte[-1] = 8;
            *slotIdByte = entryId;
            slotIdByte += 0x4c;
            entryId++;
            slotBase += 0x13;
        } while (slotIdByte < tableEnd);

        mem.s_Unknown_005de538 = 0;
    }

    // Deplace dans GameMemory le 2026-08-19 : globale .data.

    /// <summary>
    /// Ghidra: FUN_0052a440 @ 0x0052a440 â€” **the inter-state loading screen**, and not the
    /// "per-game-state dispatcher" an earlier revision of this file called it. Two call sites fix
    /// its role beyond doubt: <see cref="RunGameLoop"/> runs it whenever the game leaves a state
    /// for one other than 7, 9, 2 or 3, and <c>FUN_00516250</c> runs it when the current area index
    /// changes, immediately before that area's resources are loaded.
    /// <para>
    /// What it does: load one resource entry into the scratch at <c>MainMem + 0x1b0000</c>, decode
    /// it, then show it for 150 frames. It is skippable â€” pressing START or SQUARE plays a UI sound
    /// and the screen ends when that press is <em>released</em>, which is also why a button already
    /// held on entry cannot skip it.
    /// </para>
    /// <para>
    /// **Ghidra did not recover this function's stack frame.** Several calls are rendered with
    /// their arguments as free-standing locals (<c>piStack_50</c>/<c>puStack_54</c> are argument
    /// slots, not variables), and one slot the code reads is never written anywhere in the
    /// pseudocode. Two consequences are marked TODO(REVERSE) below. The argument order of the two
    /// resource calls is not among them: <c>FUN_00516250</c> calls the same
    /// <c>FUN_00529b50(entry, destination)</c> with both arguments intact, which settles it.
    /// </para>
    /// </summary>
    internal void RunLoadingScreen()
    {
        // Ghidra: FUN_0040a600() â€” an empty body in Blaze.c (`void FUN_0040a600(void) { return; }`).
        // Called here and at the top of every game-loop iteration; nothing to reproduce.

        byte* mainMem = GetMainMemPtr();
        if (mem.s_MainResourceBuffer_005e97c0 == null || mainMem == null) return;

        // Ghidra: piVar2 = *(int **)(DAT_005e97c0 + 0x84) â€” a table of (entry, metadata) pairs.
        int* pairs = *(int**)(mem.s_MainResourceBuffer_005e97c0 + 0x84);
        if (pairs == null) return;

        // Ghidra: the walk to the end of the pair list. **Its result is never used** â€” transcribed
        // rather than dropped, because a discarded traversal usually means Ghidra lost the use, not
        // that the original wrote a dead loop.
        int* walk = pairs;
        while (*walk != 0) walk += 2;

        // Ghidra: game state 3 picks its entry from DAT_005de528; every other state uses 0xb.
        uint entryIndex = s_GameStateId_005de510 == 3 ? (uint)(s_Unknown_005de528 & 0xff) : 0xb;

        byte* scratch = mainMem + 0x1b0000;
        LoadResourceEntry((int*)(nint)pairs[entryIndex * 2], scratch);
        DecodeResourceChunk(scratch,
                            (ushort*)(mem.s_EntityDecodeTable_005e8f00 + 0xfc),   // Ghidra: &DAT_005e8ffc
                            (byte*)(pairs + entryIndex * 2 + 1));

        // Ghidra: two Ordinal_676 calls â€” winsys3d's MoveImage(RECT* src, int dstX, int dstY),
        // confirmed at winsys3d.c:5348, which copies a VRAM rectangle to another VRAM position.
        // The two rectangles are the eight ushorts the function opens with.
        //
        // **TODO(REVERSE) levé le 2026-08-18** — le désassemblage direct confirme la transcription
        // telle qu'elle était. `0x0052A4F4..0x0052A507` :
        //     LEA ECX,[ESP+0x28]   PUSH 0  PUSH 0  PUSH ECX   CALL Ordinal_676   ; &rect[0], (0,0)
        //     PUSH 0   LEA EDX,[ESP+0x34]  PUSH 0  PUSH EDX   CALL Ordinal_676   ; &rect[4], (0,0)
        // Le second `LEA` se lit avec un `PUSH` déjà passé, donc son décalage effectif est 0x30,
        // soit exactement `rect + 4` — pas « quatre octets en dessous du premier rectangle » comme
        // la pseudo-C le laissait croire. Les deux destinations sont bien (0, 0).
        ushort* rect = stackalloc ushort[8];
        rect[0] = 0x380; rect[1] = 0; rect[2] = 0x80; rect[3] = 0xe0;
        rect[4] = 0; rect[5] = 0x1ff; rect[6] = 0x100; rect[7] = 1;
        Winsys3d.Ordinal_676(rect, 0, 0);
        Winsys3d.Ordinal_676(rect + 4, 0, 0);

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);

        // Ghidra: the 20-byte sprite descriptor, the same shape as the main menu's â€” pointer,
        // colour, x/y, tpage/clut, index â€” and passed by address to both renderers.
        byte* sprite = s_LoadingScreenSprite;
        *(uint**)sprite = s_LoadingScreenTextureRecord;
        *(uint*)(sprite + 0x04) = 0x1000000;
        *(ushort*)(sprite + 0x08) = 0xa0;
        *(ushort*)(sprite + 0x0a) = 0x80;
        *(ushort*)(sprite + 0x0c) = 0x1400;
        *(ushort*)(sprite + 0x0e) = 0x1249;
        *(uint*)(sprite + 0x10) = 0;

        // Ghidra: iVar5 is a three-state gate â€” -1 until nothing is held (so a button carried in
        // from the previous screen cannot skip this one), 0 armed, 1 pressed and acknowledged.
        int phase = -1;
        int framesLeft = 0x96;

        while (framesLeft != 0 || phase != 0)
        {
            primCursor = prims.DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            primCursor = prims.DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);
            FrameEnd(&otBase, &otEntry, &primCursor, 1, 1);

            // Ghidra: START (0x0008) or SQUARE (0x8000) of the current player's pad word. The word
            // at DAT_00604a20 is **active high** â€” inp.UpdateButtonEdges inverts the active-low word
            // FUN_00523660 builds â€” so a set bit is a held button.
            bool held = (CurrentPadWord() & 0x8008) != 0;

            if (!held && phase == -1) phase = 0;

            if (!held)
            {
                if (phase == 1) return;
            }
            else if (phase == 0)
            {
                phase = 1;
                aud.PlayUiSound38();        // Ghidra: FUN_0053db80
            }

            framesLeft--;
        }

    }

    /// <summary>
    /// Ghidra: the pad-state expression shared by FUN_00512520, FUN_00545340, FUN_0052a440 and
    /// FUN_00506880. Factored because it had already been transcribed twice and drifted once: one
    /// copy hardcoded the outer index to 0 instead of reading DAT_005de538.
    ///
    /// <para><b>The stride is 0x10, not 4.</b> Ghidra's pseudo-C renders the final scaling as
    /// <c>&amp;DAT_00604a20 + slot * 4</c>, which is wrong — the decompiler lost the shift. All three
    /// call sites disassemble to <c>SHL EAX,0x4</c> followed by <c>ADD EAX,0x604a20</c>:
    /// <c>FUN_0052a440</c> at <c>0x0052a60b</c>, <c>FUN_00545340</c> at <c>0x00545423</c>, and
    /// <c>FUN_00506880</c> at <c>0x005069fa</c> (see <c>Legacy/Raw/Blaze/*.asm</c>). Each pad slot
    /// is a 16-byte record. The earlier <c>* 4</c> here was a latent 4× address error: it only
    /// agreed with the original at slot 0, which is the single-controller case everything has been
    /// exercised with so far.</para>
    /// </summary>
    internal byte* CurrentPadState()
    {
        int slot = inp.s_PlayerSlots_005e9188[
            inp.s_SlotArea_005e9180[0x248 + (uint)(mem.s_Unknown_005de538 & 0xff) * 0x9c] * 0x4c];
        return inp.s_InputStateBase_00604a20 + slot * 0x10;
    }

    /// <summary>
    /// Ghidra: the halfword at offset 0 of the current pad record — the form FUN_0052a440 reads
    /// (<c>MOV AX, word ptr [EAX]</c> at <c>0x0052a613</c>, then <c>AND AX,0x8008</c>).
    /// </summary>
    internal ushort CurrentPadWord() => *(ushort*)CurrentPadState();

    /// <summary>
    /// Ghidra: the loading screen's sprite descriptor â€” 20 bytes, **one contiguous block**. Split
    /// into two allocations it would draw nothing at all, which is exactly what happened to the
    /// main menu's background; see REVERSE_STATUS.md, 2026-08-14.
    /// </summary>
    private readonly byte* s_LoadingScreenSprite = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// L'enregistrement de texture de 8 octets que le descripteur de l'écran de chargement désigne
    /// par son champ 0 — **résolu le 2026-08-18 en désassemblant `FUN_0052a440` directement**, là où
    /// la pseudo-C ne montrait qu'un emplacement de pile que rien n'écrivait (le cadre de pile perdu
    /// dont parlait le TODO à ce site).
    ///
    /// <para>L'original le monte octet par octet juste avant la boucle d'image, puis relie son
    /// adresse au descripteur à chaque tour (<c>Legacy/Binaries/Blaze.exe</c>, <c>0x0052A51B..0x0052A53D</c>
    /// puis <c>0x0052A57E</c>) :</para>
    /// <code>
    /// MOV [ESP+0x2C],BL      ; 0     MOV [ESP+0x30],BL      ; 0
    /// MOV [ESP+0x2D],BL      ; 0     MOV byte [ESP+0x31],1
    /// MOV [ESP+0x2E],BL      ; 0     MOV byte [ESP+0x32],0x3F
    /// MOV byte [ESP+0x2F],0xE0       MOV [ESP+0x33],BL      ; 0
    /// ...
    /// LEA EAX,[ESP+0x20]   →   MOV [ESP+0x3C],EAX          ; descripteur+0 = &l'enregistrement
    /// </code>
    ///
    /// <para>Soit les deux dwords <c>0xE0000000</c> et <c>0x003F0100</c>. Le second se recoupe de
    /// lui-même : son octet 2 vaut <c>0x3F</c>, l'index que <see cref="DrawSpritePrimitive"/> utilise
    /// dans la table de pages — <c>s_EntityDecodeTable_005e8f00 + 0x3F * 4 = 0x005e8ffc</c>, très
    /// exactement l'adresse où <c>DecodeResourceChunk</c> vient d'écrire quelques lignes plus haut.
    /// Le premier porte la hauteur, <c>0xE0</c> = 224 lignes, ce qui concorde avec le rectangle
    /// source <c>{0x380, 0, 0x80, 0xE0}</c> de la première recopie VRAM.</para>
    /// </summary>
    private readonly uint* s_LoadingScreenTextureRecord = BuildLoadingScreenTextureRecord();

    private static uint* BuildLoadingScreenTextureRecord()
    {
        uint* record = (uint*)NativeMemory.AllocZeroed(8);
        record[0] = 0xE0000000;
        record[1] = 0x003F0100;
        return record;
    }

    // Ghidra: DAT_005de528 â€” the area index game state 3 loads its screen from. Never assigned in
    // the reconstructed graph yet, so it reads 0.
    internal int s_Unknown_005de528;


    /// <summary>
    /// Ghidra: the directory-reload prefix shared verbatim by <see cref="LoadMenuResources"/>
    /// (<c>FUN_00542160</c>) and <see cref="ReloadGameplayResources"/> (<c>FUN_00504ca0</c>) â€”
    /// confirmed byte-for-byte identical in both raw bodies, not just the same shape. Reloads the
    /// resource entry for the current state id (<see cref="s_Unknown_005e92b0"/>), fixes up its
    /// header, then walks the directory's entry list decoding each chunk into
    /// <see cref="s_EntityDecodeTable_005e8f00"/> â€” the same loop <see cref="LoadFirstLevelResources"/>
    /// already runs, reusing its three helpers rather than duplicating them a third time.
    /// </summary>
    private void ReloadStateResources()
    {
        s_Unknown_005e97c8 = LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, mem.s_MainResourceDirectory_005e97c4);
        FixupResourceHeader(mem.s_MainResourceDirectory_005e97c4);

        if (mem.s_MainResourceDirectory_005e97c4 != null)
        {
            int* directory = *(int**)(mem.s_MainResourceDirectory_005e97c4 + 0x10);
            int entryPtrValue = *directory;
            if (entryPtrValue != 0)
            {
                int* metadata = directory + 1;
                byte* destSlot = mem.s_EntityDecodeTable_005e8f00;
                byte* mainMem = GetMainMemPtr();
                do
                {
                    byte* scratch = mainMem != null ? mainMem + 0x1b0000 : null;
                    LoadResourceEntry((int*)(nint)entryPtrValue, scratch);
                    DecodeResourceChunk(scratch, (ushort*)destSlot, (byte*)metadata);
                    entryPtrValue = directory[2];
                    directory += 2;
                    destSlot += 4;      // same 4-byte tpage/clut stride as the other call site
                    metadata += 2;
                } while (entryPtrValue != 0);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00504ca0 @ 0x00504ca0 â€” called at the very start of game-state 1's entry
    /// (<c>thunk_FUN_00536180</c>). Confirmed byte-for-byte the same directory-reload prefix as
    /// <see cref="LoadMenuResources"/> (<c>FUN_00542160</c>), just without that function's
    /// menu-specific tail (the six render-block ordinals) â€” extracted as
    /// <see cref="ReloadStateResources"/> once a third call site needed the identical loop. The
    /// leading <c>FUN_0040a600()</c> is the confirmed no-op already elided at every other call site.
    /// </summary>
    internal void ReloadGameplayResources() => ReloadStateResources();

    /// <summary>
    /// Ghidra: FUN_00542160 @ 0x00542160 â€” reloads the state's resources, then reconfigures the
    /// menu's own render block. See <see cref="ReloadStateResources"/> for the shared prefix.
    /// </summary>
    internal void LoadMenuResources()
    {
        ReloadStateResources();

        // Ghidra: _DAT_00566c74 = 0x100; _DAT_00566c78 = 0; _DAT_00566c7a = 0; then six ordinals.
        byte* block = mem.s_ViewRect_00566c70;
        *(uint*)(block + 4) = 0x100;
        *(ushort*)(block + 8) = 0;
        *(ushort*)(block + 0x0a) = 0;

        Winsys3d.Ordinal_770(block);
        Winsys3d.Ordinal_769(1);
        Winsys3d.Ordinal_768(1);
        Winsys3d.Ordinal_767(0, 0xffffff);
        Winsys3d.Ordinal_766(block);
        Winsys3d.Ordinal_765(1, 0xffffff);
    }
}
