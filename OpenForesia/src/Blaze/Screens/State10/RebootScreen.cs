// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00513ba0 @ 0x00513ba0 — <b>l'etat de jeu 10</b>, que seule la ligne REBOOT du menu
    /// de mise au point atteint dans <c>Blaze.c</c> (d'ou le nom). Transcrit contre le desassemblage
    /// (<c>Legacy/Raw/Blaze/FUN_00513BA0.asm</c>, 0x223 octets).
    ///
    /// <para><b>Ce que l'ecran fait.</b> Il recharge les ressources de jeu, lance la piste 7, puis
    /// fait <b>defiler vers la gauche</b> les enregistrements 0 et 1 du premier atlas de la table
    /// <c>+0x94</c> du repertoire d'etat, poses cote a cote a 0x100 pixels d'ecart : 256 pixels en
    /// 1 024 images. Il tient ensuite 700 images, et pendant les cent dernieres superpose le second
    /// atlas de la table, anime sur ses enregistrements 0 a 4 (un toutes les vingt images). Puis
    /// l'etat 0.</para>
    ///
    /// <para><b>Le fondu</b> vient de <c>s_FadeCounter_005e9960</c>, que <see cref="FrameEnd"/> fait
    /// avancer : la teinte des deux images est <c>(compteur &amp; 0x3f) * 4</c> sur les trois canaux.
    /// Des qu'elle depasserait 0x80, elle est bloquee a 0x80 et le compteur recule d'un cran — le
    /// fondu monte donc une fois et se fige. <b>START ou CARRE ne coupent l'ecran qu'a partir de
    /// la</b>, pas pendant le fondu.</para>
    ///
    /// <para><b>Le piege de pile que Ghidra rend en <c>local_4 = 0x513ba5</c>.</b> La fonction
    /// commence par <c>call FUN_00504ca0</c> puis <c>jmp</c> sur son vrai prologue : l'adresse de
    /// retour de cet appel, <c>0x00513ba5</c>, reste perimee sous la pile, et le <c>sub esp,20h</c>
    /// qui suit la range dans le cadre. Le bloc de sprite commence a <c>[esp+0x0c]</c> du cadre, et
    /// son champ d'<b>indice</b> <c>+0x10</c> tombe sur cette adresse perimee — ce que Ghidra a pris pour
    /// une variable initialisee. Sans effet : l'indice est ecrit avant chaque dessin, et les champs
    /// d'echelle <c>+0xc</c>/<c>+0xe</c>, jamais ecrits, ne sont lus ni par
    /// <see cref="Primitives2d.DrawSpriteVariant"/> ni par <see cref="Primitives2d.DrawFlatPrimitive"/>.
    /// Le bloc est donc alloue a zero ici.</para>
    ///
    /// <para><b>La troisieme image est dessinee APRES <see cref="FrameEnd"/></b>, donc dans la liste
    /// de l'image suivante, avant les deux panneaux de celle-ci. Transcrit dans cet ordre.</para>
    /// </summary>
    private static void RunRebootScreen_00513BA0()
    {
        Res.ReloadGameplayResources();                   // Ghidra: FUN_00504ca0

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);      // Ghidra: FUN_0052a1e0

        int* sprite = stackalloc int[5];
        byte* s = (byte*)sprite;

        int scroll = 0;      // Ghidra: esi — le defilement, 0..0x400
        int hold = 0;        // Ghidra: ebp — les images de pause, 0..700

        // `mov byte [.],1` : le champ `isbg` des deux draw envs, sur un octet.
        *Mem.Arena(0x005e9898) = 1;
        *Mem.Arena(0x005e9908) = 1;

        byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
        byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[leader[8] * 0x4c] * 0x10;
        *(ushort*)(pad + 2) = 0;

        PlayMusicTrack(7);                               // Ghidra: FUN_00529ad0(7)
        s_FadeCounter_005e9960 = 0;

        while (true)
        {
            uint shade = (s_FadeCounter_005e9960 & 0x3f) * 4;
            if ((int)shade > 0x80)
            {
                shade = 0x80;
                if ((*(ushort*)(pad + 2) & 0x8008) != 0) break;
                s_FadeCounter_005e9960--;
            }

            // Les deux panneaux, enregistrements 0 et 1 du premier atlas, a x et x + 0x100.
            int** pictures = *(int***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            *(int**)s = pictures[0];
            *(short*)(s + 0xa) = 0;
            *(short*)(s + 8) = (short)-(scroll >> 2);
            *(uint*)(s + 4) = shade * 0x10101;
            *(short*)(s + 0x10) = 0;
            primCursor = Prims.DrawSpriteVariant(otEntry, primCursor, sprite, 0, 0);   // Ghidra: FUN_00521df0
            primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, sprite);          // Ghidra: FUN_0051f9c0

            *(short*)(s + 8) += 0x100;
            *(short*)(s + 0x10) = 1;
            primCursor = Prims.DrawSpriteVariant(otEntry, primCursor, sprite, 0, 0);
            primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, sprite);

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);   // Ghidra: FUN_0052a280

            if (scroll < 0x400)
            {
                scroll++;
                continue;
            }

            hold++;
            if (hold >= 700) break;

            if (hold >= 600)
            {
                // Le second atlas de la table, anime : `imul 66666667h / sar edx,3` puis le bit
                // de signe, c'est-a-dire une division par vingt tronquee vers zero.
                *(int**)s = (*(int***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94))[1];
                *(short*)(s + 0x10) = (short)((hold - 600) / 20);
                primCursor = Prims.DrawSpriteVariant(otEntry, primCursor, sprite, 0, 0);
                primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, sprite);
            }
        }

        s_GameStateId_005de510 = 0;
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
    }
}
