// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DE LA VALLEE D'ARGENT — zone 8, table 0x0056b0c8.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `LakeRuinsScenes.cs` : une scene qui
// cherche sa cible parmi les entites de scene.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_0042f620 @ 0x0042f620 — table <c>0x0056b0c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 174 instructions.
    ///
    /// <para>
    /// <b>L'entite de scene d'espece 1 recule, s'eleve, et s'efface.</b> A chaque image, musique
    /// decroissante et bits de reveil effaces ; le balayage retient la derniere entite de scene
    /// d'espece 1 vivante, lui pose le bit 2 de <c>+0x3b</c>, la sequence 3 et <c>0x1000</c>, et la
    /// scene se pose sur elle.
    /// </para>
    /// <list type="table">
    ///   <item><term>toujours</term><description>la cible recule de 4 dans son cap, monte de
    ///     <c>+0x12</c> (2, puis un de moins aux images 15, 25, 35 et 45 — elle finit par
    ///     redescendre), s'assombrit de <c>(1, 8, 8)</c> ; son image avance deux images sur quatre
    ///     et bute sur 11.</description></item>
    ///   <item><term>avant 60</term><description>deux naissances <c>0xc2</c> par image, sans
    ///     planche, et le son <c>0x0b</c> a la cinquieme image d'une horloge <c>+0x98</c> ou sur un
    ///     tirage sur six.</description></item>
    ///   <item><term>60</term><description>l'entite d'incantation <c>0xc1</c>, la cible cachee
    ///     (<c>0x4000</c>), deux sons <c>0x2e</c>.</description></item>
    ///   <item><term>passe 119</term><description>fin : camera rendue, script 1 du membre
    ///     <c>DAT_005de538</c>, <c>0x2004000</c> sur la cible. Le fond n'est pas
    ///     touche.</description></item>
    /// </list>
    /// </summary>
    private static void EntityHandler_0042F620(byte* e)
    {
        byte* target = null;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);
            scene[0x3b] |= 2;
            *(short*)(scene + 0x18) = 3;
            *(uint*)scene |= 0x1000;
            target = scene;
        }

        if (target == null) target = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 2;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *(short*)(e + 0x98) = 0;
        }

        *(int*)(target + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(target + 0x32)) * -4;   // Ghidra: Ordinal_451
        *(int*)(target + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(target + 0x32)) * -4;        // Ghidra: Ordinal_450
        *(int*)(target + 0x20) += *(short*)(e + 0x12) * 0x1000;
        BrightenEntityTint_00404DE0(target, -1, -8, -8);        // Ghidra: FUN_00404de0

        ushort clock = *(ushort*)(e + 0x16);
        if (clock == 0x0f || clock == 0x19 || clock == 0x23 || clock == 0x2d) (*(short*)(e + 0x12))--;

        if ((clock & 2) == 0)
        {
            (*(short*)(target + 0x1a))++;
            if (*(short*)(target + 0x1a) > 10) *(short*)(target + 0x1a) = 0x0b;
        }

        short frame = *(short*)(e + 0x16);
        if (frame < 0x3c)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc2, 0, -1);   // Ghidra: FUN_0047ac40
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc2, 0, -1);

            (*(short*)(e + 0x98))++;
            int roll = CrtRand();
            if (*(short*)(e + 0x98) == 5 || (short)(roll % 6) == 1)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(0x0b, 500, 0x800, 0x800);    // Ghidra: FUN_0053cea0
            }

            frame = *(short*)(e + 0x16);
        }

        if (frame == 0x3c)
        {
            SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);            // Ghidra: FUN_0047aae0
            *(uint*)target |= 0x4000;
            Aud.PlaySoundEffect(0x2e, 0x200, 0x1000, 0x1000);
            Aud.PlaySoundEffect(0x2e, 0x22a, 0x1000, 0x1000);
        }

        if (*(short*)(e + 0x16) > 0x77)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument
            RunEntityScript(s_Unknown_005e97cc,
                Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c, 1);   // Ghidra: FUN_00513dd0
            *(uint*)target |= 0x2004000;
        }

        (*(short*)(e + 0x16))++;
    }
}
