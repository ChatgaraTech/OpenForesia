// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DE LA GROTTE SCELLEE — zone 10, table 0x0056adc8.
//
// Ouvert le 2026-09-19 au lot 2 de la zone, avec son premier corps qui COMPTE LE GROUPE — la
// dalle de pression 0x00468850 et ses deux copies. C'est le test d'appartenance
// d'ARCHITECTURE.md, et la regle convenue pour la grotte : ce fichier s'ouvre quand un corps le
// demande.
//
// Dumps : Legacy/Raw/Blaze/SealedCave/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00468850</c> (bloc 0), <c>0x0046b340</c> (2-3), <c>0x0046d760</c> (5-8-10) —
    /// trois corps sans aucune immediate differente. Absents de <c>Blaze.c</c> ; 73 instructions.
    ///
    /// <para>
    /// <b>La dalle de pression.</b> Elle retient sa hauteur de repos. Tant qu'un membre au moins
    /// se tient dessus (<see cref="CountPartyMembersMatching"/>, bit <c>0x10</c>), elle s'enfonce de
    /// <c>+0x14</c> unites (etat 2, une image, puis retour a 1 — et de nouveau 2 tant qu'on y reste),
    /// sonne une fois (voix <c>0x25</c> de la zone), et <b>pose dans la banque 0 le bit que designe
    /// son <c>+0x16</c></b> — s'il n'est pas nul. Quand plus personne n'y est, elle remonte (etat 3)
    /// et le son est rearme.
    /// </para>
    /// <para>
    /// Le bit se calcule en division C (<c>n / 8</c>, <c>n % 8</c>), comme le poisson des ruines
    /// dans le lac ; il n'est jamais efface ici : la dalle se souvient d'avoir ete pressee.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468850(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x25), 0x200, 0x2000, 0, 0);   // Ghidra: FUN_00468080
                *(short*)(e + 0x98) = 1;
            }

            *(int*)(e + 0x20) = (*(short*)(e + 0x12) - *(short*)(e + 0x14)) << 0xc;
            *(short*)(e + 0x10) = 1;

            short flag = *(short*)(e + 0x16);
            if (flag != 0)
            {
                int index = flag / 8;
                Mem.s_StateBank0_005e96e0[index] |= (byte)(1 << ((flag - index * 8) & 0x1f));   // Ghidra: DAT_005e96e0
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x10) = 1;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (aboard != 0)
        {
            if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
            return;
        }

        if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 3;
    }
}
