// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DES RUINES SOUTERRAINES — zone 7, table 0x0056b0c8.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `ImmortalCastleScenes.cs`. Une seule scene,
// et elle n'a PAS d'enregistrement de camera prive : comme la scene des eclairs du chateau
// (0x0042D4E0), elle travaille sur l'hote +0x6c et garde son etat dans ses propres champs.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x0042e150</c> — creneau <b>33</b> du bloc <c>0x0055a4a8</c> (table
    /// <c>0x0056b0c8</c>). <b>Absent de <c>Blaze.c</c></b> ; 349 instructions,
    /// <c>Legacy/Raw/Blaze/UndergroundRuins/FUN_0042E150.asm</c>.
    ///
    /// <para>
    /// <b>La scene de l'hote qui flambe</b>, soixante-quinze images sur l'hote <c>+0x6c</c> (ou la
    /// liste d'entites). A chaque image l'hote joue sa sequence <b>7</b>, porte <c>0x1000</c>, et
    /// n'avance son image d'animation qu'une image sur deux (bit 0 de <c>+0x16</c> <b>pose</b>),
    /// en butant sur la derniere. Tant que l'octet 2 de la banque 0 est nul, la musique decroit et
    /// les bits de reveil sont effaces.
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>le fond d'origine retenu dans
    ///     <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, l'hote remis a l'image 0, teinte blanche
    ///     (<c>0xffffff</c>), <c>+0x36 = 1</c>.</description></item>
    ///   <item><term>toujours</term><description>un grondement de cinq sons (voix <c>0x20</c>) sur
    ///     une horloge de 19 images, <c>+0x98</c>.</description></item>
    ///   <item><term>images 0 a 4</term><description>huit naissances <c>0xfd</c> par image ; de 4 a
    ///     5, trois <c>0xfc</c> de plus.</description></item>
    ///   <item><term>jusqu'a 56</term><description>le fond s'assombrit de 2 par canal et par image,
    ///     mode 2.</description></item>
    ///   <item><term>58 a 60</term><description>l'eclair : +30, +30, +60 au fond en mode 1, et deux
    ///     naissances <c>0xfb</c> par image.</description></item>
    ///   <item><term>61</term><description>le son <c>0x0b</c> haut, la secousse de camera armee a
    ///     10, et UNE entite d'incantation <c>0xff</c> (<see cref="SpawnCastEntity_0047AAE0"/>) ; a
    ///     62, le meme son grave.</description></item>
    ///   <item><term>passe 65</term><description>le fond redescend de 4, 4 et 7, mode 2.</description></item>
    ///   <item><term>75</term><description>fin : camera rendue, fond d'origine en mode 2,
    ///     <c>0x2004000</c> sur l'hote, et un script sur le membre que l'hote designe par son octet
    ///     <c>+9</c> — le script 0 si l'octet 2 de la banque 0 est pose ; sinon le script 1, et
    ///     l'effet <c>0x59</c> sur chaque membre vivant.</description></item>
    /// </list>
    /// <para>
    /// <b>La secousse est amortie en alternance</b> : <c>+0x12</c> se rapproche de zero d'une
    /// unite, puis <b>change de signe</b>, a chaque image — 10, -9, 8, -7... — et chaque valeur
    /// non nulle part dans <see cref="TryPublishCameraEffectNearEntity"/> et pose <c>0x4000</c> sur
    /// l'hote.
    /// </para>
    /// <para>
    /// <b>Le fond est modifie en place, pas repeint</b> : les ajouts s'appliquent a la valeur
    /// courante de <c>0x005e9160</c>..<c>64</c>. Les deux phases « assombrir » et « eclair » se
    /// chevauchent donc a l'image 58 sans s'annuler, et rien ne borne le resultat. La phase
    /// « passe 65 » ne fait pas descendre de meme que l'eclair a monte : -4, -4, -7 contre +30,
    /// +30, +60.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042E150(byte* e)
    {
        if (Mem.s_StateBank0_005e96e0[2] == 0)                   // Ghidra: DAT_005e96e2
        {
            MusicFadeStep_0041AF50();                            // Ghidra: FUN_0041af50
            ResetEntityWakeBits_0040E640();                      // Ghidra: FUN_0040e640
        }

        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        *(short*)(host + 0x18) = 7;
        byte* clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + host[4] * 4) + 0x1c);   // Ghidra: `mov [esp+14h],edx`
        *(uint*)host |= 0x1000;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(host + 0x1a) = 0;
            host[0x0a] = 0;
            *(int*)(host + 0x38) = 0xffffff;
            *(short*)(host + 0x36) = 1;
            *(short*)(e + 0x98) = 0;
        }

        // ---------------------------------------------------------------- le grondement
        // Ghidra: cinq tests qui relisent +0x98 chacun ; un seul peut etre vrai.
        if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(0x20, 0x200, 0x1200, 0x1200);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x98) == 1) Aud.PlaySoundEffect(0x20, 0x200, 0x1200, 0x1200);
        if (*(short*)(e + 0x98) == 2) Aud.PlaySoundEffect(0x20, 0x329, 0x1200, 0x1200);
        if (*(short*)(e + 0x98) == 3) Aud.PlaySoundEffect(0x20, 0x400, 0x1200, 0x1200);
        if (*(short*)(e + 0x98) == 4) Aud.PlaySoundEffect(0x20, 0x653, 0x1200, 0x1200);

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) == 0x13) *(short*)(e + 0x98) = 0;

        // ---------------------------------------------------------------- les naissances
        if (*(short*)(e + 0x16) < 5)
        {
            for (int i = 0; i < 8; i++)
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfd, 0, -1);   // Ghidra: FUN_0047ac40
        }

        if (*(short*)(e + 0x16) > 3 && *(short*)(e + 0x16) < 6)
        {
            for (int i = 0; i < 3; i++)
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfc, 0, -1);
        }

        // ---------------------------------------------------------------- le fond
        if (*(short*)(e + 0x16) < 0x39)
        {
            *(short*)Mem.Arena(0x005e9166) = 2;
            *(short*)Mem.Arena(0x005e9160) += -2;
            *(short*)Mem.Arena(0x005e9162) += -2;
            *(short*)Mem.Arena(0x005e9164) += -2;
        }

        if (*(short*)(e + 0x16) >= 0x3a && *(short*)(e + 0x16) <= 0x3c)
        {
            *(short*)Mem.Arena(0x005e9164) += 0x3c;
            *(short*)Mem.Arena(0x005e9160) += 0x1e;
            *(short*)Mem.Arena(0x005e9162) += 0x1e;
            *(short*)Mem.Arena(0x005e9166) = 1;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfb, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfb, 0, -1);
        }

        if (*(short*)(e + 0x16) > 0x41)
        {
            *(short*)Mem.Arena(0x005e9166) = 2;
            *(short*)Mem.Arena(0x005e9160) += -4;
            *(short*)Mem.Arena(0x005e9162) += -4;
            *(short*)Mem.Arena(0x005e9164) += -7;
        }

        // ---------------------------------------------------------------- l'incantation
        if (*(short*)(e + 0x16) == 0x3d)
        {
            *(short*)(e + 0x12) = 10;
            Aud.PlaySoundEffect(0x0b, 0x329, 0x1600, 0x1600);
            SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);            // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x16) == 0x3e) Aud.PlaySoundEffect(0x0b, 0x100, 0x1600, 0x1600);

        // ---------------------------------------------------------------- la secousse
        short jolt = *(short*)(e + 0x12);
        if (jolt != 0)
        {
            if (jolt > 0) *(short*)(e + 0x12) = --jolt;
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x12) = (short)(*(short*)(e + 0x12) + 1);

            jolt = (short)-*(short*)(e + 0x12);
            *(short*)(e + 0x12) = jolt;
            TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);   // Ghidra: FUN_0041be60
            *(uint*)host |= 0x4000;
        }

        // ---------------------------------------------------------------- l'hote, une image sur deux
        if ((e[0x16] & 1) != 0)
        {
            (*(short*)(host + 0x1a))++;

            // Ghidra: le nombre d'images est etendu SANS signe, l'image courante AVEC.
            ushort frames = *(ushort*)(clip + 0x16);
            if (*(short*)(host + 0x1a) >= frames) *(short*)(host + 0x1a) = (short)(frames - 1);
        }

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) < 0x4b) return;

        // ---------------------------------------------------------------- la fin
        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                             // Ghidra: FUN_005037b0, sans argument

        byte* member = Inp.s_SlotFlags_005e93c0 + host[9] * 0x9c;

        if (Mem.s_StateBank0_005e96e0[2] != 0)
        {
            RunEntityScript(s_Unknown_005e97cc, member, 0);      // Ghidra: FUN_00513dd0
        }
        else
        {
            RunEntityScript(s_Unknown_005e97cc, member, 1);

            byte* slot = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                SpawnEffectEntity(slot, 0x59, 0, -1);            // Ghidra: FUN_0047a9d0
            }
        }

        *(uint*)host |= 0x2004000;
        SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
    }
}
