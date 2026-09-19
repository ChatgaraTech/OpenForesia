// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DE LA VALLEE D'ARGENT — zone 8, sous-zones 0 et 1, table 0x0056adc8.
//
// Ouvert le 2026-09-19 avec la zone, par le test d'appartenance d'ARCHITECTURE.md : les trois
// corps d'ici COMPTENT LE GROUPE (CountPartyMembersMatching contre ActivePartySlotCount_00466D70)
// et deux d'entre eux lancent des scripts.
//
//   0x00466C70   la tempete : dix secondes a attendre ensemble, puis un libelle
//   0x00466F50   LE RADEAU : quand tout le groupe est a bord, il part, tourne, et sombre
//   0x00467440   LA LUGE : quand tout le groupe est a bord, la descente — la camera suit
//
// Dumps : Legacy/Raw/Blaze/SilverValley/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LA TEMPETE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00466c70 @ 0x00466c70 — decompilee dans <c>Blaze.c</c>, 93 instructions.
    ///
    /// <para>
    /// <b>Six bouffees <c>0x30</c> pres de la camera a chaque image</b>, et une attente : tant que
    /// TOUS les membres actifs sont rattaches a l'entite (<c>CountPartyMembersMatching(e, 0x10,
    /// 0x40000000)</c>, au moins un), l'horloge <c>+0x12</c> avance ; qu'un seul s'eloigne et elle
    /// retombe a l'etat 0. Passe 599 images — dix secondes —, l'etat 2 et le libelle <c>+4</c> de la
    /// table <c>+0x70</c> du repertoire de zone, sans locuteur.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466C70(byte* e)
    {
        for (int i = 0; i < 6; i++)
            SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);       // Ghidra: FUN_00468110

        short together = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (*(short*)(e + 0x10) == 0)
        {
            short active = (short)ActivePartySlotCount_00466D70();   // Ghidra: FUN_00466d70
            if (together == active && together != 0)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 1) return;

        short count = (short)ActivePartySlotCount_00466D70();
        if (together != count || together == 0)
        {
            *(short*)(e + 0x10) = 0;
            return;
        }

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) <= 599) return;

        *(short*)(e + 0x10) = 2;
        byte* label = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4);
        BindMenuLabels(label, 0u, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
    }

    // =====================================================================
    // LE RADEAU
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00466f50 @ 0x00466f50 — decompilee dans <c>Blaze.c</c>, 245 instructions.
    ///
    /// <para>
    /// <b>Le radeau part quand tout le groupe est a bord</b> (le meme compte qu'a la tempete). Il
    /// file alors dans son cap a une vitesse <c>+0x5c</c> qui monte de 30 a 150, en laissant
    /// quatre remous <c>0x31</c> par image ; il vire a gauche des images 11 a 29, a droite des
    /// images 31 a 49 ; passe la 75e il pique du nez et s'enfonce en accelerant ; a la 98e il
    /// passe a l'etat 2, avec un son (voix <c>0x14</c>).
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 2</term><description>il sombre : la camera tremble d'une secousse qui part
    ///     de 15 et s'amortit en changeant de signe, il tourne sur ses trois axes, retrecit de 200
    ///     par image et coule de plus en plus vite. A zero : l'etat 99.</description></item>
    ///   <item><term>etat 3</term><description><b>un membre est tombe plus bas que lui</b> : le
    ///     radeau ne fait plus que chavirer et retrecir, et tout membre qui passe sous -200
    ///     unites lance son script 0. A zero : l'etat 99, cache.</description></item>
    ///   <item><term>etat 99</term><description>des qu'un membre est revenu en deca de
    ///     <c>0xa28000</c> en X, le radeau s'eteint ; un membre sous -500 lance son script
    ///     0.</description></item>
    /// </list>
    /// <para>
    /// Le test qui fait passer a l'etat 3 est fait <b>a chaque image, avant tout</b>, sur les
    /// quatre emplacements : il peut couper le radeau en pleine course.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466F50(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x5c) = 0x1e;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0x14;
        }

        short aboard = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000
        short active = (short)ActivePartySlotCount_00466D70();                 // Ghidra: FUN_00466d70
        if (aboard == active && *(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0 && *(int*)(slot + 0x20) < *(int*)(e + 0x20))
                *(short*)(e + 0x10) = 3;
        }

        // ---------------------------------------------------------------- 1 et 3 : la course
        if (*(short*)(e + 0x10) == 1 || *(short*)(e + 0x10) == 3)
        {
            int speed = *(int*)(e + 0x5c) + 1;
            (*(short*)(e + 0x12))++;
            *(int*)(e + 0x5c) = speed;
            if (speed > 0x96) *(int*)(e + 0x5c) = 0x96;

            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 2);   // Ghidra: Ordinal_451
            *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 2);        // Ghidra: Ordinal_450

            short t = *(short*)(e + 0x12);
            if (t > 10 && t < 0x1e) *(short*)(e + 0x32) += -0x1e;
            if (t > 0x1e && t < 0x32) *(short*)(e + 0x32) += 0x1e;

            if (t > 0x4b)
            {
                *(short*)(e + 0x14) += 2;
                *(short*)(e + 0x34) -= *(short*)(e + 0x14);
                *(int*)(e + 0x20) += *(short*)(e + 0x14) * -0xc00;
            }

            if (t > 0x61)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x9a) = 0x0f;
                if (t == 0x62)
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14), 0x400, 0x2000, 0, 0);   // Ghidra: FUN_00468080
            }

            short keep = *(short*)(e + 0x12);
            *(short*)(e + 0x12) = 0;
            for (int i = 0; i < 4; i++)
                SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);   // Ghidra: FUN_00468110
            *(short*)(e + 0x12) = keep;
        }

        // ---------------------------------------------------------------- 2 : il sombre
        if (*(short*)(e + 0x10) == 2)
        {
            // Ghidra: `neg ax ; test ax,ax ; setle cl ; dec ecx ; and ecx,-2 ; inc ecx ; add eax,ecx`
            // — rapprocher de zero d'une unite, puis changer de signe : 15, -14, 13, -12...
            short jolt = (short)-*(short*)(e + 0x9a);
            jolt = (short)(jolt <= 0 ? jolt + 1 : jolt - 1);
            *(short*)(e + 0x9a) = jolt;
            TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);   // Ghidra: FUN_0041be60

            *(short*)(e + 0x28) += -200;
            *(short*)(e + 0x2a) += -200;
            *(short*)(e + 0x2c) += -200;
            *(short*)(e + 0x30) += 0x14;
            *(short*)(e + 0x32) += 0x78;
            *(short*)(e + 0x34) += -100;
            *(int*)(e + 0x20) += *(short*)(e + 0x16) * -0x1000;
            *(short*)(e + 0x16) += 2;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 99;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ---------------------------------------------------------------- 3 : il chavire
        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x34) += -100;
            *(short*)(e + 0x28) += -200;
            uint flags = *(uint*)e;
            *(short*)(e + 0x2a) += -200;
            *(short*)(e + 0x2c) += -200;
            *(short*)(e + 0x30) += 0x14;
            *(short*)(e + 0x32) += 0x78;
            *(uint*)e = flags | 0x2000;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 99;
                *(short*)(e + 0x12) = 0;
                *(uint*)e = flags | 0x6000;
            }

            ScriptFallenMembers(-0xc8000);
        }

        // ---------------------------------------------------------------- 99 : l'epave
        if (*(short*)(e + 0x10) != 99) return;

        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            if ((*(uint*)member & 0x80000000) != 0 && *(int*)(member + 0x1c) < 0xa28000)
                *(uint*)e &= 0x7fffffff;
        }

        ScriptFallenMembers(-0x1f4000);
    }

    /// <summary>Ghidra: la boucle des chutes, recopiee deux fois dans le radeau — tout membre vivant
    /// dont la hauteur est sous <paramref name="depth"/> lance son script 0. Rien ne l'empeche de
    /// le relancer a l'image suivante.</summary>
    private static void ScriptFallenMembers(int depth)
    {
        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            if ((*(uint*)member & 0x80000000) != 0 && *(int*)(member + 0x20) < depth)
                RunEntityScript(s_Unknown_005e97cc, member, 0);    // Ghidra: FUN_00513dd0
        }
    }

    // =====================================================================
    // LA LUGE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00467b98</c> — <b>le tracé du virage de la luge</b> : un octet par
    /// porte, pour les portes 10 a 44 (<c>+0x16 - 10</c>). Il designe l'un des douze creneaux de la
    /// table de sauts <c>0x00467b68</c> : 0 a 8 menent au virage « + », 9 et 10 au virage « - »,
    /// 11 a « rien ». Releve dans <c>Blaze.exe</c>.
    /// </summary>
    private static readonly byte[] s_SledSteer_00467B98 =
    [
        0x00, 0x01, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x02, 0x03, 0x04, 0x0b, 0x0b,
        0x05, 0x06, 0x07, 0x08, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b,
        0x0b, 0x09, 0x0a,
    ];

    /// <summary>Ghidra: les deux tables de sauts de la luge a <c>0x00467bbc</c> et
    /// <c>0x00467bd8</c>, indexees par <c>+0x16 - 0x17</c> : le cap de la pente pour les portes 23
    /// a 29, et le plafond de la vitesse de lacet <c>+0x9a</c>.</summary>
    private static readonly int[] s_SledSlopeCourse = [0x464, 0x52c, 0x5f4, 0x6bc, 0x720, 0x784, 0x800];
    private static readonly short[] s_SledYawCeiling = [0x64, 0x12c, 0x1f4, 0x2bc, 0x320, 0x384, 0x400];

    /// <summary>Ghidra: <c>0x00571760</c>..<c>0x0057179c</c> — des mots de <c>EntityState</c> que
    /// seule la luge ecrit, a chaque image de course. TODO(REVERSE) : leur lecteur n'est pas
    /// trouve dans les sources transcrites — la forme (une distance qui tend vers <c>0x5dc</c>, un
    /// angle qui suit le cap, deux zeros) est celle d'un placement de camera.</summary>
    private static byte* SledView(int va) => Mem.EntityStateBytes(va);

    /// <summary>
    /// Ghidra: <c>0x00467440</c> — <b>absent de <c>Blaze.c</c></b> ; 506 instructions, trois tables
    /// de sauts (<c>0x00467b68</c> par l'octet de <c>0x00467b98</c>, <c>0x00467bbc</c>,
    /// <c>0x00467bd8</c>).
    ///
    /// <para>
    /// <b>La luge.</b> Elle attend a sa place de depart ; quand tout le groupe est a bord, elle
    /// s'elance (etat 1) : elle glisse dans le cap de la pente <c>+0x5c</c>, a une vitesse
    /// <c>+0x14</c> qui monte de 10 a 40, en derivant de <c>+0x12</c> sur le cote. La piste est
    /// une suite de PORTES : chaque fois que le maillage change sous elle, <c>+0x16</c> avance, et
    /// les portes commandent le reste — le virage (<see cref="s_SledSteer_00467B98"/>), le cap de
    /// la pente (portes 23 a 29), la vitesse de lacet <c>+0x9a</c>, qui fait tourner la luge
    /// d'elle-meme. Passe la porte 35, le cap est ramene vers <c>0x800</c> ; a la porte 56, la
    /// course est finie (etat -1) et les QUATRE emplacements lancent leur script 0.
    /// </para>
    /// <para>
    /// Hors du sol elle se cabre (<c>+0x34</c> +15 par image) et compte ses images d'envol
    /// <c>+0x98</c> ; a l'atterrissage, un son (voix <c>0x15</c>) si elle etait cabree, et des gerbes
    /// de neige. <b>Un heurt</b> (<c>+0x90</c> : <c>0x800</c> ou <c>0x8000</c>) <b>la renverse</b>
    /// (etat 2) : elle tourne, retrecit de 100 par image, saute une fois en l'air, et a zero revient
    /// a l'etat 3, qui la replace au depart et la fait regrossir.
    /// </para>
    /// <para>
    /// <b>Deux curiosites du binaire, transcrites telles quelles.</b> Une premiere boucle sur les
    /// quatre emplacements lit leur mot <c>+0x90</c> et n'en fait rien. Et la derive <c>+0x12</c>
    /// s'amortit de deux par image hors des virages, dans les deux sens : partie impaire, elle
    /// reste bloquee a 1 — 1 devient -1 au premier test, puis 1 au second, dans la meme image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00467440(byte* e)
    {
        // Ghidra: 0x0046744c..0x00467474 — la boucle sans effet.
        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) _ = *(uint*)(slot + 0x90) & 0x8000;
        }

        e[7] &= 0xef;

        // ---------------------------------------------------------------- 3 : retour au depart
        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x1c) = 0xdf8000;
            *(int*)(e + 0x20) = 0x71c000;
            *(int*)(e + 0x24) = 0x1256000;
            *(short*)(e + 0x30) = 0;
            *(ushort*)(e + 0x82) = 0xa000;
            *(ushort*)(e + 0x86) = 0xa000;
            *(ushort*)(e + 0x8a) = 0xa000;
            *(ushort*)(e + 0x8e) = 0xa000;
            *(short*)(e + 0x32) = 0x400;
            *(short*)(e + 0x34) = 0;
            *(int*)(e + 0x64) = *(int*)(e + 0x20);
            *(int*)(e + 0x60) = *(int*)(e + 0x20);

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0

            if (*(short*)(e + 0x28) < 0x1000)
            {
                *(short*)(e + 0x28) += 0x12c;
                *(short*)(e + 0x2a) += 0x12c;
                *(short*)(e + 0x2c) += 0x12c;
                return;
            }

            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x2c) = 0x1000;
            *(short*)(e + 0x10) = 0;
            return;
        }

        // ---------------------------------------------------------------- l'embarquement
        int near = CountPartyMembersMatching(e, 0x20, 0x40000000);     // Ghidra: FUN_00535000
        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (aboard == (short)ActivePartySlotCount_00466D70() && *(short*)(e + 0x10) == 0)   // Ghidra: FUN_00466d70
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0x400;
            *(short*)(e + 0x14) = 10;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x9a) = 0;
            *(int*)SledView(0x00571790) = *(int*)(e + 0x60);
            *(short*)(e + 0x98) = 0;
        }

        if (near == 0 && *(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }
        else
        {
            SledSpray(e, variant: 2, count: 3);
        }

        // ---------------------------------------------------------------- la vue
        short yaw = *(short*)SledView(0x0057179a);
        *(short*)SledView(0x00571798) = 0x1f4;
        *(short*)SledView(0x0057179a) = (short)(yaw + ((-0x400 - yaw - *(int*)(e + 0x5c)) >> 3));
        *(short*)SledView(0x0057179c) = 0;
        *(int*)SledView(0x00571760) = 0;
        int reach = *(int*)SledView(0x00571768);
        *(int*)SledView(0x00571764) = 0;
        *(int*)SledView(0x00571768) = reach + ((0x5dc - reach) >> 4);

        // ---------------------------------------------------------------- l'arrivee
        if (*(short*)(e + 0x16) >= 0x38 && *(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = -1;
            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, member += 0x9c)
                RunEntityScript(s_Unknown_005e97cc, member, 0);    // Ghidra: FUN_00513dd0
        }

        if (*(short*)(e + 0x10) != 0 && *(short*)(e + 0x10) != -1) RideSled(e);

        // ---------------------------------------------------------------- 2 : la chute
        if (*(short*)(e + 0x10) != 2) return;

        uint state = *(uint*)e;
        *(short*)(e + 0x30) += 0x14;
        *(short*)(e + 0x32) += 0x80;
        *(short*)(e + 0x34) += -0x32;
        *(short*)(e + 0x28) += -100;
        *(short*)(e + 0x2a) += -100;
        *(short*)(e + 0x2c) += -100;
        *(uint*)e = state | 0x2000;

        if (*(short*)(e + 0x12) == 0)
        {
            e[7] |= 0x10;
            *(int*)(e + 0x54) = 0xa000;
            *(int*)(e + 0x50) = ((CrtRand() % 5 - 2) * 25) << 0xd;
            *(int*)(e + 0x58) = ((CrtRand() % 5 - 2) * 25) << 0xd;
        }

        (*(short*)(e + 0x12))++;

        if (*(short*)(e + 0x28) >= 0) return;

        *(short*)(e + 0x28) = 0;
        *(uint*)e &= 0xffffdfff;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x12) = 0;
        *(short*)(e + 0x10) = 3;
    }

    /// <summary>Ghidra: <c>0x004676ad..0x00467aba</c> — <b>la course</b> de la luge, un pas par
    /// image, pour tout etat autre que 0 et -1.</summary>
    private static void RideSled(byte* e)
    {
        // ------------------------------------------------------------ le virage de la porte
        int gate = *(short*)(e + 0x16) - 10;
        *(short*)(e + 0x32) -= *(short*)(e + 0x9a);
        short baseYaw = *(short*)(e + 0x32);

        bool plus = false, minus = false;
        if ((uint)gate <= 0x22)
        {
            byte slotIndex = s_SledSteer_00467B98[gate];
            if (slotIndex <= 8) plus = true;
            else if (slotIndex <= 10) minus = true;
        }

        if (plus) (*(short*)(e + 0x12))++;
        if (minus) (*(short*)(e + 0x12))--;

        if (!minus && !plus)
        {
            if (*(short*)(e + 0x12) > 0) *(short*)(e + 0x12) += -2;
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x12) += 2;
        }

        // ------------------------------------------------------------ la derive, puis la pente
        short yaw = (short)(baseYaw + *(short*)(e + 0x12));
        *(short*)(e + 0x32) = yaw;

        int course = *(int*)(e + 0x5c);
        *(int*)(e + 0x24) -= (int)Winsys3d.GetSinTableEntry(yaw - course) * *(short*)(e + 0x12);         // Ghidra: FUN_00545a1c
        *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - *(int*)(e + 0x5c)) * *(short*)(e + 0x12);   // Ghidra: FUN_00545a16

        short speed = *(short*)(e + 0x14);
        *(int*)(e + 0x24) -= (int)Winsys3d.GetSinTableEntry(*(int*)(e + 0x5c)) * speed;
        *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(*(int*)(e + 0x5c)) * speed;

        *(short*)(e + 0x14) = (short)(speed + 1);
        if ((short)(speed + 1) >= 0x28) *(short*)(e + 0x14) = 0x28;

        // ------------------------------------------------------------ la pose
        ushort keep90 = *(ushort*)(e + 0x90);
        int keep60 = *(int*)(e + 0x60);
        int keep64 = *(int*)(e + 0x64);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        byte* keep7c = *(byte**)(e + 0x7c);
        int keep80 = *(int*)(e + 0x80);
        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        int keep88 = *(int*)(e + 0x88);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        int keep84 = *(int*)(e + 0x84);
        int keep8c = *(int*)(e + 0x8c);
        short gateBefore = *(short*)(e + 0x80 + keep7c[0x0e] * 4);

        *(int*)(e + 0x20) = *(int*)SledView(0x00571790);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        int height = *(int*)(e + 0x20) + -0xf000;
        *(short*)(e + 0x34) += 5;
        short pitch = *(short*)(e + 0x34);
        *(int*)(e + 0x20) = height;

        if (height > *(int*)(e + 0x60))
        {
            // En l'air : elle se cabre, et compte.
            *(short*)(e + 0x34) = (short)(pitch + 10);
            (*(short*)(e + 0x98))++;
        }
        else
        {
            if (pitch > 0x14)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x15), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080

            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(short*)(e + 0x98) += -2;
            *(short*)(e + 0x34) = 0;

            if (*(short*)(e + 0x98) <= 0) *(short*)(e + 0x98) = 0;
            else SledSpray(e, variant: 1, count: 4);

            SledSpray(e, variant: 0, count: 3);
        }

        *(int*)SledView(0x00571790) = *(int*)(e + 0x60);

        // ------------------------------------------------------------ les portes
        byte* terrain = *(byte**)(e + 0x7c);
        if (*(short*)(e + 0x80 + terrain[0x0e] * 4) != gateBefore)
        {
            (*(short*)(e + 0x16))++;
            uint slope = (uint)(*(short*)(e + 0x16) - 0x17);
            if (slope <= 6) *(int*)(e + 0x5c) = s_SledSlopeCourse[slope];
        }

        short passed = *(short*)(e + 0x16);
        uint ceilingIndex = (uint)(passed - 0x17);
        if (ceilingIndex <= 6 && *(short*)(e + 0x9a) < s_SledYawCeiling[ceilingIndex])
            *(short*)(e + 0x9a) += 0x14;

        if (passed > 0x1d)
        {
            *(short*)(e + 0x9a) += 0x14;
            if (*(short*)(e + 0x9a) > 0x400) *(short*)(e + 0x9a) = 0x400;
        }

        // ------------------------------------------------------------ le heurt
        uint contact = *(uint*)(e + 0x90);
        if ((contact & 0x800) != 0) *(short*)(e + 0x10) = 2;
        if ((contact & 0x8000) != 0) *(short*)(e + 0x10) = 2;

        if ((contact & 0x8900) != 0)
        {
            *(byte**)(e + 0x7c) = keep7c;
            *(int*)(e + 0x60) = keep60;
            *(uint*)(e + 0x90) = keep90;
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x64) = keep64;
            *(int*)(e + 0x20) = *(int*)(e + 0x48);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = keep80;
            *(int*)(e + 0x84) = keep84;
            *(int*)(e + 0x88) = keep88;
            *(int*)(e + 0x8c) = keep8c;
        }

        *(short*)(e + 0x32) += *(short*)(e + 0x9a);

        if (passed > 0x23)
        {
            short y = *(short*)(e + 0x32);
            *(short*)(e + 0x32) = (short)(((0x800 - y) >> 4) + y);
        }
    }

    /// <summary>Ghidra: les gerbes de neige de la luge, recopiees trois fois — <paramref name="count"/>
    /// naissances <c>0x31</c> pres de la camera, avec <c>+0x12</c> prete a <paramref name="variant"/>
    /// le temps de la naissance, puis rendu.</summary>
    private static void SledSpray(byte* e, short variant, int count)
    {
        short keep = *(short*)(e + 0x12);
        *(short*)(e + 0x12) = variant;
        for (int i = 0; i < count; i++)
            SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);       // Ghidra: FUN_00468110
        *(short*)(e + 0x12) = keep;
    }
}
