// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: DAT_005de0c0..DAT_005de0c7 — huit octets que <see cref="RunGameOverState_0051BB00"/>
    /// remplit (<c>00 00 00 E0 01 00 3F 00</c>) et que <b>rien ne relit</b> : ni <c>Blaze.c</c>, ni
    /// le binaire (seules occurrences de l'adresse comme immediat : les huit <c>mov</c> de cette
    /// fonction, releve du 2026-09-26). La forme — un descripteur d'atlas de huit octets dont
    /// l'octet 6 vaut 0x3f, l'entree de la table de decodage que la meme fonction ecrit — laisse
    /// penser a un curseur prepare puis jamais dessine. Allocation propre : l'adresse tombe dans le
    /// trou entre <c>CameraBlock</c> (fin 0x005de0b8) et <c>s_ItemNameScratch_005de0c8</c>, sans
    /// chevauchement.
    /// </summary>
    private static readonly byte* s_GameOverDescriptor_005de0c0 = (byte*)NativeMemory.AllocZeroed(8);

    /// <summary>
    /// Ghidra: FUN_0051bb00 @ 0x0051bb00 — <b>l'etat de jeu 9, la fin de partie</b>. La boucle de
    /// monde y passe quand tout le groupe est tombe (<c>DAT_005de030 &gt;= DAT_005e9020</c>, Blaze.c
    /// 57630). Transcrit contre le desassemblage (<c>Legacy/Raw/Blaze/FUN_0051BB00.asm</c>, 0x1bb
    /// octets).
    ///
    /// <para><b>Ce que l'ecran fait.</b> Il recharge les ressources de jeu, lance la piste 0x26 et
    /// affiche le premier enregistrement du premier atlas de la table <c>+0x94</c> du repertoire
    /// d'etat, centre en (0xa0, 0x78), agrandi a 0x1420 / 0x1149 (1,26 x 1,08). Au plus 500 images,
    /// puis retour a l'etat 0.</para>
    ///
    /// <para><b>Le fondu</b> est celui de <see cref="RunRebootScreen_00513BA0"/> : teinte
    /// <c>(s_FadeCounter_005e9960 &amp; 0x3f) * 4</c>, bloquee a 0x80 des qu'elle la depasserait, le
    /// compteur reculant alors d'un cran. <b>START ou CARRE ne coupent l'ecran qu'une fois le fondu
    /// monte.</b> La forme de Ghidra, <c>(c &amp; 0x3f) != 0x20 &amp;&amp; 0x7f &lt; c * 4</c>, n'est que
    /// <c>c * 4 &gt; 0x80</c> (<c>cmp eax,80h / jle</c>).</para>
    ///
    /// <para><b>Le meme piege de pile</b> que l'ecran REBOOT : <c>call FUN_00504ca0</c> puis
    /// <c>jmp</c> sur le vrai prologue, d'ou le <c>local_4 = 0x51bb05</c> de Ghidra. Le bloc de
    /// sprite est a <c>[esp+0x1c]</c> ; son indice <c>+0x10</c> est ecrit a 0 a chaque image
    /// (<c>mov [esp+2Ch],bx</c>), et les deux octets hauts de ce dword — l'adresse perimee — ne
    /// sont lus ni par <see cref="Primitives2d.DrawSpritePrimitive"/> ni par
    /// <see cref="Primitives2d.DrawFlatPrimitive"/>. Bloc alloue a zero ici.</para>
    /// </summary>
    private static void RunGameOverState_0051BB00()
    {
        Res.ReloadGameplayResources();                   // Ghidra: FUN_00504ca0

        // Entree 0x3f de la table de decodage (mots, `mov word`).
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfc) = 0x11c;    // Ghidra: DAT_005e8ffc
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfe) = 0x7fc0;   // Ghidra: DAT_005e8ffe

        s_GameOverDescriptor_005de0c0[0] = 0;
        s_GameOverDescriptor_005de0c0[1] = 0;
        s_GameOverDescriptor_005de0c0[2] = 0;
        s_GameOverDescriptor_005de0c0[3] = 0xe0;
        s_GameOverDescriptor_005de0c0[4] = 1;
        s_GameOverDescriptor_005de0c0[5] = 0;
        s_GameOverDescriptor_005de0c0[6] = 0x3f;
        s_GameOverDescriptor_005de0c0[7] = 0;

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);      // Ghidra: FUN_0052a1e0

        // `mov byte [.],1` : le champ `isbg` des deux draw envs, sur un octet.
        *Mem.Arena(0x005e9898) = 1;
        *Mem.Arena(0x005e9908) = 1;

        byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
        byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[leader[8] * 0x4c] * 0x10;
        *(ushort*)(pad + 2) = 0;

        PlayMusicTrack(0x26);                            // Ghidra: FUN_00529ad0(0x26)
        s_FadeCounter_005e9960 = 0;

        int* sprite = stackalloc int[5];
        byte* s = (byte*)sprite;

        for (int frame = 0; frame < 500; frame++)        // Ghidra: esi
        {
            uint shade = (s_FadeCounter_005e9960 & 0x3f) * 4;
            if ((int)shade > 0x80)
            {
                shade = 0x80;
                if ((*(ushort*)(pad + 2) & 0x8008) != 0) break;
                s_FadeCounter_005e9960--;
            }

            *(uint*)(s + 4) = shade * 0x10101;
            *(short*)(s + 8) = 0xa0;
            *(short*)(s + 0xa) = 0x78;
            *(short*)(s + 0xc) = 0x1420;
            *(short*)(s + 0xe) = 0x1149;
            *(short*)(s + 0x10) = 0;
            *(int**)s = **(int***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            primCursor = Prims.DrawSpritePrimitive(otEntry, primCursor, sprite, 0, 0);   // Ghidra: FUN_00521f20
            primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, sprite);           // Ghidra: FUN_0051f9c0

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);   // Ghidra: FUN_0052a280
        }

        s_GameStateId_005de510 = 0;
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
    }
}
