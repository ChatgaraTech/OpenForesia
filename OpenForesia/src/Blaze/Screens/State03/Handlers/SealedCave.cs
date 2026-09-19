// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA GROTTE SCELLEE — zone 10, table 0x0056adc8.
//
// Ouvert le 2026-09-19 avec le lot 1 de la zone. Ce que la grotte DECLENCHE — ce qui compte le
// groupe ou lance un script — ira dans `SealedCaveMecanismes.cs` le jour ou un corps le demandera.
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`) : meme classe partielle.
// Dumps : Legacy/Raw/Blaze/SealedCave/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_0046c800 @ 0x0046c800 — sous-zones 5, 8 et 10. Decompilee dans <c>Blaze.c</c>,
    /// 13 instructions. <b>Une amorce qui reste</b> : a sa premiere image elle fait naitre l'entite
    /// d'incantation <c>0xfd</c> (<see cref="SpawnCastEntity_0047AAE0"/>) et passe a l'etat 1, ou
    /// elle ne fait plus rien — sans s'eteindre.
    /// </summary>
    private static void EntityHandler_0046C800(byte* e)
    {
        if (*(short*)(e + 0x10) != 0) return;

        SpawnCastEntity_0047AAE0(e, 0xfd, 0, -1);                   // Ghidra: FUN_0047aae0
        *(short*)(e + 0x10) = 1;
    }

    // =====================================================================
    // LOT 2 (2026-09-19)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00468a20</c> (bloc 0), <c>0x0046b570</c> (2-3), <c>0x0046e6d0</c> (6-7) — trois
    /// corps sans aucune immediate differente. Absents de <c>Blaze.c</c> ; 44 instructions.
    ///
    /// <para>
    /// <b>Le souffle qui dresse.</b> A l'etat 0 il se tient a plat (<c>+0x2a = 0</c>) et arme sa
    /// phase a <c>0x80</c> ; <b>rien dans le corps ne le fait passer a l'etat 1</b> — c'est un
    /// script ou un voisin qui le reveille. A l'etat 1, un son (voix <c>0x27</c> de la zone), puis
    /// son echelle verticale suit deux fois le sinus d'une phase qui avance d'un seizieme de tour par
    /// image ; passe un demi-tour, il revient a l'etat 0, a plat.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468A20(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0x80;
            *(short*)(e + 0x98) = 0;
            return;
        }

        if (state != 1) return;

        if (*(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x27), 0x400, 0x2000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 1;
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));              // Ghidra: FUN_00545a1c
        *(short*)(e + 0x12) += 0x100;
        *(short*)(e + 0x2a) = (short)((sin << 0xc) >> 0xb);

        if (*(short*)(e + 0x12) <= 0x800) return;

        *(short*)(e + 0x10) = 0;
        *(short*)(e + 0x2a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c020</c> (sous-zones 2-3) et <c>0x0046de80</c> (5-8-10) — deux corps sans
    /// aucune immediate differente. Absents de <c>Blaze.c</c> ; 97 instructions.
    ///
    /// <para>
    /// <b>Le battant qui frappe.</b> Il retient son cap de repos, et vise ce cap plus
    /// <c>+0x14</c>. Il y tourne en accelerant de 10 par image, puis revient au repos de meme, en
    /// sonnant (voix <c>0x1f</c>) a la naissance et a chaque bout de course. <b>Si son
    /// <c>+0x14</c> vaut <c>0x1000</c> — un tour entier — il ne revient pas</b> : arrive au bout, il
    /// est remis au repos et repart, il tourne sans fin.
    /// </para>
    /// <para>
    /// <b>A chaque image, il frappe le groupe</b> (<see cref="StrikePartyMembers_0045DA40"/>, dix pour
    /// cent, les membres que son octet de contact designe). Abimer n'est pas decider : c'est du
    /// decor.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C020(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short sweep = *(short*)(e + 0x14);
            short rest = *(short*)(e + 0x32);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = rest;
            *(int*)(e + 0x5c) = sweep == 0x1000 ? 1 : 0;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = (short)(sweep + rest);
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x32) += speed;
            *(short*)(e + 0x16) = (short)(speed + 10);

            if (*(short*)(e + 0x32) > *(short*)(e + 0x14))
            {
                *(short*)(e + 0x32) = *(short*)(e + 0x14);

                if (*(int*)(e + 0x5c) == 0)
                {
                    *(short*)(e + 0x10) = 2;
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);
                }
                else
                {
                    *(short*)(e + 0x32) = *(short*)(e + 0x12);
                }

                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x32) -= speed;
            *(short*)(e + 0x16) = (short)(speed + 10);

            if (*(short*)(e + 0x32) < *(short*)(e + 0x12))
            {
                *(short*)(e + 0x32) = *(short*)(e + 0x12);
                *(short*)(e + 0x10) = 1;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);
                *(short*)(e + 0x16) = 0;
            }
        }

        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);               // Ghidra: FUN_0045da40
    }
}
