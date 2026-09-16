// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES DEUX AURES D'ETAT — les creneaux 0x46 et 0x47 de la table statique 0x00551c30, et leurs
// deux enfants.
//
// Une aure est la marque PERSISTANTE d'un etat, par opposition aux marques de
// `StatusMarkers.cs` qui naissent, scintillent et meurent : celle-ci reste tant que l'etat dure.
// `TickStatusBurst_004760C0` est la gerbe de pose, `TickSleepAura_004761D0` l'aure du sommeil, et
// `TickSleepBubble_00476290` la bulle qui monte au-dessus du dormeur.
//
// La duree, elle, est dans le bloc de combat en +0x80..+0xc2 - voir `Combat/StatusEffects.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES DEUX AURES D'ETAT — 0x46 et 0x47 de 0x00551c30, et leurs deux enfants
    //
    // Une entite d'aura par etat inflige, posee sur la VICTIME et non sur le lanceur. Les deux se
    // ressemblent assez pour se lire ensemble, et different sur un point qui compte :
    //
    //   0x004760C0   L'etat 0x80 (l'effet 12, FUN_0053b810). Une SEULE image : elle seme quatre
    //                enfants 0x2e en croix, puis s'eteint. L'aura ne dure pas ; les enfants si.
    //   0x004761D0   L'etat 0x40, LE SOMMEIL (l'effet 11, FUN_0053b730). Elle VIT : un enfant
    //                0x2f toutes les huit images, tant que la victime dort.
    //
    //   0x00476140   l'enfant de la premiere, sous-type 0x2e — deja ecrit.
    //   0x00476290   l'enfant de la seconde, sous-type 0x2f — ecrit ici.
    //
    // LES DEUX SUIVENT LA TETE DE LA VICTIME. Elles appellent StatusBubbleHeight_00538BC0 sur elle,
    // rangent la hauteur en +0x12 et s'en servent deux fois : pour poser l'enfant a cette hauteur
    // au-dessus d'elle, et pour l'echelle, `hauteur << n / 150`. Le decalage n differe — 0xd pour le
    // sommeil, 0xb pour l'autre —, donc les bulles du sommeil sont QUATRE FOIS plus grosses.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004760c0 @ 0x004760c0 — <b>la gerbe de l'etat <c>0x80</c></b> : le sous-type
    /// <c>0x46</c>, creneau 70 de <see cref="s_StaticEntityHandlers_00551c30"/>. Vingt lignes.
    ///
    /// <para>
    /// <b>Elle ne vit qu'une image.</b> Elle se cale sur la victime (<c>+0x6c</c> recopie dans
    /// <c>+0x74</c>), prend sa hauteur de tete, seme <b>quatre</b> enfants <c>0x2e</c> en tournant
    /// le cap de <c>0x400</c> — un quart de tour — entre chacun, puis s'eteint. Ce sont les enfants
    /// qui portent la duree : ils vivent tant que la victime garde le bit <c>0x80</c>.
    /// </para>
    /// <para>
    /// Elle ne pose <b>pas</b> la position des enfants : <c>+0x1c</c>..<c>+0x24</c> partent tels que
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> les recopie, et
    /// <see cref="TickStatusMote_00476140"/> les recalcule des sa premiere image depuis
    /// <c>+0x74</c>. C'est ce qui la distingue de sa sœur du sommeil.
    /// </para>
    /// </summary>
    private static void TickStatusBurst_004760C0(byte* record)
    {
        byte* e = record;
        byte* victim = *(byte**)(e + 0x6c);

        *(byte**)(e + 0x74) = victim;

        int height = StatusBubbleHeight_00538BC0((uint*)victim);
        *(short*)(e + 0x12) = (short)height;

        uint scale = (uint)((height << 0xb) / 0x96);
        *(ushort*)(e + 0x1a) = 3;
        *(uint*)(e + 0x28) = scale << 0x10 | scale;

        for (int i = 0; i < 4; i++)
        {
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x2e, e[0x0b], -1);

            *(short*)(e + 0x32) += 0x400;
        }

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004761d0 @ 0x004761d0 — <b>l'aure du sommeil</b> : le sous-type <c>0x47</c>,
    /// creneau 71 de <see cref="s_StaticEntityHandlers_00551c30"/>. C'est l'entite que
    /// <see cref="ApplyStatusEffect_0053B730"/> — l'effet 11, bit <c>0x40</c> — fait naitre sur la
    /// victime en meme temps que la marque <c>0x29</c>.
    ///
    /// <para><b>Trois temps, et ils ne tombent pas ensemble :</b></para>
    /// <list type="bullet">
    /// <item><b>a la premiere image seulement</b> — elle prend la victime pour ancre, mesure sa
    /// hauteur de tete et en tire son echelle, <c>hauteur &lt;&lt; 13 / 150</c> dans les deux
    /// axes.</item>
    /// <item><b>une image sur huit</b> — un cap tire au hasard, la position reprise de la victime
    /// mais relevee de la hauteur de tete, et un enfant <c>0x2f</c> lache. Le test porte sur
    /// l'octet bas de <c>+0x10</c>, donc bien sur l'age et non sur un compteur separe.</item>
    /// <item><b>a chaque image</b> — elle relit le bloc de statistiques de la victime et s'efface
    /// des que celle-ci disparait, meurt, ou <b>se reveille</b> : le bit <c>0x40</c> de
    /// <c>+0x40</c> est la condition de sa survie.</item>
    /// </list>
    /// </summary>
    private static void TickSleepAura_004761D0(byte* record)
    {
        byte* e = record;
        byte* victim = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x74) = victim;

            int height = StatusBubbleHeight_00538BC0((uint*)victim);
            *(short*)(e + 0x12) = (short)height;

            uint scale = (uint)((height << 0xd) / 0x96);
            *(uint*)(e + 0x28) = scale << 0x10 | scale;
        }

        // Ghidra lit l'octet bas de +0x10 : une image sur huit.
        if ((e[0x10] & 7) == 0)
        {
            *(short*)(e + 0x32) = (short)CrtRand();

            *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
            *(int*)(e + 0x20) = (*(short*)(e + 0x12) << 0xc) + *(int*)(victim + 0x20);
            *(int*)(e + 0x24) = *(int*)(victim + 0x24);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0() + 0x30, 0x2f, e[0x0b], -1);
        }

        uint state = *(uint*)(GetEntityStatBlock((uint*)victim) + 0x40);

        if ((*(uint*)victim & 0x80000000) == 0 || (state & 0x40000000) != 0 || (state & 0x40) == 0)
        {
            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00476290 @ 0x00476290 — <b>la bulle du sommeil</b> : le sous-type <c>0x2f</c>,
    /// creneau 47 de <see cref="s_StaticEntityHandlers_00552230"/>. L'enfant que
    /// <see cref="TickSleepAura_004761D0"/> lache une image sur huit.
    ///
    /// <para>
    /// <b>Elle monte en ondulant.</b> Sa hauteur gagne <c>0x4000</c> par image — quatre unites — et
    /// sa derive horizontale est un balancement : l'amplitude est <c>sin(age &lt;&lt; 8) &gt;&gt;
    /// 10</c>, appliquee sur le cap <b>a angle droit</b> (<c>cap + 0x800</c>), et s'ajoute a une
    /// derive constante de quatre unites sur le cap lui-meme. Le cap ne bouge pas ; c'est
    /// l'amplitude qui change de signe, ce qui fait osciller la bulle de part et d'autre de sa
    /// trajectoire.
    /// </para>
    /// <para><b>Vingt images</b>, et elle s'eteint : elle n'a pas besoin de savoir si la victime
    /// dort encore, sa mere s'en charge et cesse d'en lacher.</para>
    /// </summary>
    private static void TickSleepBubble_00476290(byte* record)
    {
        byte* e = record;

        int heading = *(short*)(e + 0x32);
        int sway = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading + 0x800) * (sway >> 10)
                             + (int)Winsys3d.GetSinTableEntry(heading) * 4;
        *(int*)(e + 0x20) += 0x4000;

        int lateral = Winsys3d.GetCosTableEntry(heading + 0x800);
        int along = Winsys3d.GetCosTableEntry(heading);

        *(short*)(e + 0x10) += 1;

        *(int*)(e + 0x24) += lateral * (sway >> 10) + along * 4;

        if (*(short*)(e + 0x10) > 0x13) *(uint*)e = 0;
    }
}
