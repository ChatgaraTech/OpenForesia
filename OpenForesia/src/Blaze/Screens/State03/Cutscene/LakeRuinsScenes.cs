// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DES RUINES DANS LE LAC — zone 9, table 0x0056b0c8.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `ImmortalCastleScenes.cs`. Une scene, sans
// enregistrement de camera prive : elle cherche sa cible parmi les entites de scene, comme
// 0x0042D170 au chateau.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00430950</c> — creneau de la table <c>0x0056b0c8</c>, sous-zones 0 et 1.
    /// <b>Absent de <c>Blaze.c</c></b> ; 205 instructions,
    /// <c>Legacy/Raw/Blaze/LakeRuins/FUN_00430950.asm</c>.
    ///
    /// <para>
    /// <b>L'entite de scene d'espece 4 sombre.</b> A chaque image, la musique decroit, les bits de
    /// reveil sont effaces, et le balayage des trente-deux entites de scene retient la derniere
    /// d'espece 4 (<c>+0x0c</c>) vivante : la scene se pose sur elle, la met a la sequence 0
    /// (<c>+0x18 = 0</c>), pose le bit <c>2</c> de son octet <c>+0x3b</c> et <c>0x1000</c> sur son
    /// mot d'etat, et prend l'animation 0 de son espece. Sans cible, la liste d'entites sert, et
    /// l'« animation » est la scene elle-meme — c'est son <c>+0x16</c> que le compteur lira.
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>le fond d'origine retenu, la cible remise a
    ///     l'image 0 en teinte blanche.</description></item>
    ///   <item><term>tant que l'etat vaut 1</term><description>la cible descend de 4 unites par
    ///     image et s'assombrit de 2 par canal ; trois naissances <c>0xf3</c> par image autour de
    ///     la scene ; un grondement de sept notes descendantes (voix <c>0x18</c>, de
    ///     <c>0x554</c> a <c>0x200</c>) sur une horloge de 49 images.</description></item>
    ///   <item><term>passe 230</term><description>fin : camera rendue, script 0 du membre
    ///     <c>DAT_005de538</c>, <c>0x2004000</c> sur la cible, fond d'origine en mode 2. L'etat
    ///     n'est jamais change : c'est l'extinction du bit vivant qui arrete tout.</description></item>
    /// </list>
    /// <para>
    /// <b><c>+0x16</c> avance DE DEUX par image</b> — un <c>inc</c> avant le test de fin, un
    /// autre apres, sans condition. La fin tombe donc vers la cent-seizieme image, et le test de
    /// parite qui cadence l'animation de la cible (bit 0 <b>eteint</b>) est vrai a chaque image :
    /// la cible avance d'une image d'animation par image, et <b>reboucle</b> a zero a la fin de la
    /// sequence au lieu d'y buter.
    /// </para>
    /// </summary>
    private static void EntityHandler_00430950(byte* e)
    {
        byte* target = null;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* clip = e;                                          // Ghidra: `mov ebp,[esp+18h]`
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 4) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 0;
            clip = **(byte***)(Mem.s_AnimationTable_005e9640 + scene[4] * 4);
            scene[0x3b] |= 2;
            *(uint*)scene |= 0x1000;
        }

        if (target == null) target = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(target + 0x20) += -0x4000;
            BrightenEntityTint_00404DE0(target, -2, -2, -2);     // Ghidra: FUN_00404de0
        }

        if ((e[0x16] & 1) == 0)
        {
            (*(short*)(target + 0x1a))++;

            // Ghidra: le nombre d'images est etendu SANS signe, l'image courante AVEC.
            if (*(short*)(target + 0x1a) >= *(ushort*)(clip + 0x16)) *(short*)(target + 0x1a) = 0;
        }

        for (int i = 0; i < 3; i++)
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf3, 0, -1);   // Ghidra: FUN_0047ac40

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) > 0xe6)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument

            byte* member = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
            RunEntityScript(s_Unknown_005e97cc, member, 0);      // Ghidra: FUN_00513dd0

            *(uint*)target |= 0x2004000;
            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        (*(short*)(e + 0x16))++;

        // ---------------------------------------------------------------- le grondement
        if (*(short*)(e + 0x98) == 0x00) Aud.PlaySoundEffect(0x18, 0x554, 0x800, 0x800);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x98) == 0x07) Aud.PlaySoundEffect(0x18, 0x4ff, 0x800, 0x800);
        if (*(short*)(e + 0x98) == 0x0e) Aud.PlaySoundEffect(0x18, 0x4aa, 0x800, 0x800);
        if (*(short*)(e + 0x98) == 0x15) Aud.PlaySoundEffect(0x18, 0x400, 0x800, 0x800);
        if (*(short*)(e + 0x98) == 0x1c) Aud.PlaySoundEffect(0x18, 0x37e, 0x800, 0x800);
        if (*(short*)(e + 0x98) == 0x23) Aud.PlaySoundEffect(0x18, 0x2d4, 0x800, 0x800);
        if (*(short*)(e + 0x98) == 0x2a) Aud.PlaySoundEffect(0x18, 0x200, 0x800, 0x800);

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) >= 0x31) *(short*)(e + 0x98) = 0;
    }
}
