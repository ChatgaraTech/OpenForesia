// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA VALLEE D'ARGENT — zone 8, sous-zones 0 et 1, table 0x0056adc8.
//
// Ouvert le 2026-09-19 avec la zone. Six corps propres a la zone, qui ne consultent pas le groupe :
//
//   0x00466C30   la neige qui tombe, un flocon double une image sur six
//   0x00466DB0   trois gerbes 0x32 pres de la camera, a chaque image
//   0x00466E60   le son d'ambiance 0x13, toutes les dix images
//   0x00466DF0   le meme, mais de portee
//   0x00466ED0   le balancier qui grince
//   0x00467280   l'eclat qui suit un voisin de table, et vole en morceaux avec lui
//
// Ce que la zone DECLENCHE — l'attente du groupe, le radeau, la luge — est dans
// `SilverValleyMecanismes.cs`. Le son d'une zone se lit par `ZoneSound` (`Mine.cs`).
// Dumps : Legacy/Raw/Blaze/SilverValley/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00466c30</c> — <b>absent de <c>Blaze.c</c></b> ; 20 instructions.
    ///
    /// <para>
    /// <b>La neige.</b> Une image sur six (un tirage, sur seize bits), il fait naitre a sa place
    /// le sous-type <c>0xf3</c> — le flocon double de <see cref="EntityHandler_0042FDB0"/>.
    /// </para>
    /// <para>
    /// <b>La planche passee est un pointeur dans la pile du corps</b> : <c>lea edx,[esp+8]</c>, sur
    /// un local de huit octets jamais ecrit, et qui n'existe plus des le retour. C'est le flocon
    /// qui le sauve, en posant sa propre planche a la naissance. Transcrit avec un tampon local,
    /// de meme duree de vie.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466C30(byte* e)
    {
        byte* scratch = stackalloc byte[8];                        // Ghidra: [esp+0..7], jamais ecrit

        if ((short)(CrtRand() % 6) != 0) return;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), scratch, 0xf3, 0, -1);   // Ghidra: FUN_0047ac40
    }

    /// <summary>Ghidra: FUN_00466db0 @ 0x00466db0 — decompilee dans <c>Blaze.c</c>. Trois gerbes
    /// <c>0x32</c> pres de la camera, a chaque image, sans etat.</summary>
    private static void EntityHandler_00466DB0(byte* e)
    {
        SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);            // Ghidra: FUN_00468110
        SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
        SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
    }

    /// <summary>
    /// Ghidra: FUN_00466e60 @ 0x00466e60 — decompilee dans <c>Blaze.c</c>. <b>Le son
    /// d'ambiance</b> : la voix <c>0x13</c> de la zone, hauteur <c>0x400</c>, gain <c>0x800</c>,
    /// toutes les dix images, sans distance. L'entite est cachee (<c>0x4000</c>) a chaque
    /// retour a l'etat 0 — la derniere image de chaque cycle.
    /// </summary>
    private static void EntityHandler_00466E60(byte* e)
    {
        if (StepTenFrameClock(e))
            Aud.PlaySoundEffect(ZoneSound(0x13), 0x400, 0x800, 0x800);   // Ghidra: FUN_0053cea0
    }

    /// <summary>
    /// Ghidra: FUN_00466df0 @ 0x00466df0 — decompilee dans <c>Blaze.c</c>. <b>Le meme son</b> que
    /// <see cref="EntityHandler_00466E60"/>, mais passe par <see cref="TryPlaySoundNearEntity"/> :
    /// il part de l'entite, gain <c>0x1000</c>.
    /// </summary>
    private static void EntityHandler_00466DF0(byte* e)
    {
        if (StepTenFrameClock(e))
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }

    /// <summary>L'horloge commune aux deux sons : a l'etat 0, l'entite se cache et repart a
    /// zero ; a la dixieme image de l'etat 1, elle sonne et retombe a l'etat 0.</summary>
    private static bool StepTenFrameClock(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) != 1) return false;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 10) return false;

        *(short*)(e + 0x10) = 0;
        return true;
    }

    /// <summary>
    /// Ghidra: FUN_00466ed0 @ 0x00466ed0 — decompilee dans <c>Blaze.c</c>, 34 instructions.
    ///
    /// <para>
    /// <b>Le balancier.</b> Son inclinaison <c>+0x34</c> suit le sinus d'une phase <c>+0x14</c>
    /// qui avance de <c>0x50</c> par image, divise par 64 — ±64 unites d'angle. A chaque tour
    /// complet, s'il porte <c>+0x16 = 1</c>, il grince (voix <c>0x12</c> de la zone, hauteur
    /// <c>0x200</c>, de portee).
    /// </para>
    /// </summary>
    private static void EntityHandler_00466ED0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) != 1) return;

        int swing = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14));   // Ghidra: Ordinal_451
        *(short*)(e + 0x14) += 0x50;
        *(short*)(e + 0x34) = (short)(swing >> 6);

        if (*(short*)(e + 0x14) <= 0xfff) return;

        *(short*)(e + 0x14) -= 0x1000;
        if (*(short*)(e + 0x16) == 1)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x12), 0x200, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }

    /// <summary>
    /// Ghidra: FUN_00467280 @ 0x00467280 — decompilee dans <c>Blaze.c</c>, 132 instructions.
    ///
    /// <para>
    /// <b>L'eclat qui suit un voisin.</b> Son <c>+0x14</c> est un DECALAGE en enregistrements dans
    /// sa propre table : il suit l'entite qui se trouve <c>+0x14 * 0x9c</c> octets plus loin. A
    /// chaque image, il lui prend son etat et sa hauteur ; dans les etats 1 et 3 du voisin, aussi
    /// sa position a plat et ses trois angles, et il tire trois vitesses de rotation (±300) et
    /// deux de deplacement (±15). Les etats qu'il suit — 1, 2, 3 et 99 — sont exactement ceux du
    /// radeau <see cref="EntityHandler_00466F50"/> ; quel voisin la fiche de placement lui donne
    /// n'est pas etabli.
    /// </para>
    /// <para>
    /// Dans les etats 2 et 3, il tourne, derive et retrecit de 200 par image jusqu'a zero ;
    /// <b>en etat 3 les vitesses sont retirees a chaque image</b>, puisque la recopie a lieu
    /// avant. En etat 99, il compte dix images et s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_00467280(byte* e)
    {
        byte* leader = e + *(short*)(e + 0x14) * 0x9c;

        short state = *(short*)(leader + 0x10);
        *(short*)(e + 0x10) = state;
        *(int*)(e + 0x20) = *(int*)(leader + 0x20);

        if (state == 1 || state == 3)
        {
            *(int*)(e + 0x1c) = *(int*)(leader + 0x1c);
            *(int*)(e + 0x24) = *(int*)(leader + 0x24);
            *(short*)(e + 0x30) = *(short*)(leader + 0x30);
            *(short*)(e + 0x32) = *(short*)(leader + 0x32);
            *(short*)(e + 0x34) = *(short*)(leader + 0x34);

            *(short*)(e + 0x12) = (short)(CrtRand() % 600 - 300);
            *(short*)(e + 0x9a) = (short)(CrtRand() % 600 - 300);
            *(short*)(e + 0x16) = (short)(CrtRand() % 600 - 300);
            *(int*)(e + 0x50) = (CrtRand() % 0x1e - 0xf) * 0x1000;
            *(int*)(e + 0x54) = (CrtRand() % 0x1e - 0xf) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 0x1e - 0xf) * 0x1000;
        }

        state = *(short*)(e + 0x10);

        if (state == 2)
        {
            SpinShard(e);
            ShrinkShard(e);
            DriftShard(e);
            ClampShardScale(e);
        }

        if (state == 3)
        {
            ShrinkShard(e);
            SpinShard(e);
            DriftShard(e);
            ClampShardScale(e);
        }

        if (state == 99)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 10) *(uint*)e &= 0x7fffffff;
        }
    }

    private static void SpinShard(byte* e)
    {
        *(short*)(e + 0x30) += *(short*)(e + 0x12);
        *(short*)(e + 0x32) += *(short*)(e + 0x9a);
        *(short*)(e + 0x34) += *(short*)(e + 0x16);
    }

    private static void ShrinkShard(byte* e)
    {
        *(short*)(e + 0x28) += -200;
        *(short*)(e + 0x2a) += -200;
        *(short*)(e + 0x2c) += -200;
    }

    private static void DriftShard(byte* e)
    {
        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
    }

    private static void ClampShardScale(byte* e)
    {
        if (*(short*)(e + 0x28) >= 0) return;

        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
    }
}
