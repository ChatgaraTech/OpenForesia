// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MECHANT OURS — 0x004A4AE0, sous-type 16 du bloc C de la foret (zone 3, sous-zones 5 et 9).
//
// 2 542 octets, absents de `Blaze.c`. Passee au crible du doublon d'abord, comme le
// Grizzly du bloc C - mais elle, elle a bien un corps a elle.
//
// La charpente commune (postures, replanification, errance, hesitation) est dans
// `Ai/SceneEntityAiCommon.cs` ; la resolution du coup dans `Combat/HitResolution.cs` ;
// le ciblage dans `Entities/Targeting.cs`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004A4AE0 — LE MECHANT OURS, sous-type 16 du bloc C de la foret (sous-zones 5 et 9).
    // Transcrite le 2026-09-01 au desassembleur (Legacy/Raw/Blaze/FUN_004A4AE0.asm, 2 542 octets,
    // absente de Blaze.c). Quarante-sept etiquettes de saut, table de sauts a 0x004A54D0.
    //
    // PASSEE AU CRIBLE DU DOUBLON D'ABORD, comme le Grizzly du bloc C : compare aux treize
    // routines d'intelligence deja ecrites, l'ecart minimal est de 533 lignes (le Crabe). Ce n'est
    // donc pas une copie, c'est un corps neuf — et le plus riche de la famille a ce jour.
    //
    // TROIS GESTES, LA OU LE CRABE ET LE GRIZZLY EN ONT DEUX. La posture tenue au moment ou le bit
    // 2 se pose decide lequel part : 0 la griffe, 1 la prise, 2 le souffle.
    //
    //   - LA GRIFFE (posture 0). Elle se replace jusqu'a la douzieme image, s'elance a la douzieme,
    //     et balaie a partir de la treizieme jusqu'a la fin de l'animation. Degats pleins.
    //
    //   - LA PRISE (posture 1). C'EST CE QU'AUCUNE AUTRE NE FAIT : elle empoigne un membre du
    //     groupe, lui inflige la MOITIE de l'attaque du bloc de modele, puis le PROJETTE en
    //     diagonale et le fait decoller. Elle se deplace un instant SUR sa victime pour y faire
    //     naitre l'effet 0x39, puis se remet ou elle etait. Le bit 0x20 marque la prise faite et
    //     empeche de rejouer le balayage ; il est efface avec le bit de geste quand le rebours
    //     tombe.
    //
    //   - LE SOUFFLE (posture 2). Une attaque A DISTANCE : deux bruits aux images 11 et 13, puis a
    //     la fin de l'animation la naissance d'un sous-type >= 0xc0 lu dans le repertoire de zone.
    //     C'est la premiere routine d'intelligence du depot qui fasse naitre une entite de la table
    //     PAR SOUS-ZONE (0x0056adc8) plutot que de la table statique.
    //
    // ET ELLE SECOUE L'ECRAN QUAND ELLE MARCHE. A deux images precises de deux animations, elle
    // pose -8 dans DAT_005de0ac — le decalage vertical de camera — et joue un son de pas. Puis elle
    // amortit elle-meme la secousse, par le meme a <- (a/4) - a que EntityHandler_00476730, mais
    // avec un plancher : sous 4 en valeur absolue, elle remet a zero. C'est la seule des quatorze
    // qui touche a la camera, et la seule qui NETTOIE derriere elle.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552a00</c> et <c>0x00552a0c</c> — <b>les deux gabarits de portee du Mechant
    /// ours</b>, releves dans <c>.data</c> (<c>OpenForesia.Tools -- at</c>). Meme forme que
    /// <see cref="s_GrizzlyProbes_00552960"/>, dont ils suivent la paire dans la meme serie.
    ///
    /// <para>
    /// <b>Aucun des deux ne porte de decalage vertical</b> (le cinquieme terme est nul des deux
    /// cotes), la ou la griffe du Grizzly s'abat d'en haut. Celui de la prise est le plus large en
    /// portee laterale de la serie (<c>0x50</c>) : il faut bien attraper.
    /// </para>
    /// </summary>
    private static readonly short[] s_MeanBearProbes_00552A00 =
    [
        0x0000, 0x0028, 0x0050, 0x0096, 0x0000, 0x0078,   // 0x00552a00 — la griffe
        0x0000, 0x0050, 0x005a, 0x0096, 0x0000, 0x0064,   // 0x00552a0c — la prise
    ];

    private static short* s_MeanBearProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_MeanBearProbes_00552A00"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* MeanBearProbe(int row)
    {
        if (s_MeanBearProbeStorage == null)
        {
            s_MeanBearProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_MeanBearProbes_00552A00.Length, sizeof(short));

            for (int i = 0; i < s_MeanBearProbes_00552A00.Length; i++)
            {
                s_MeanBearProbeStorage[i] = s_MeanBearProbes_00552A00[i];
            }
        }

        return s_MeanBearProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x004a4ae0</c> — <b>l'intelligence du Mechant ours</b>, sous-type 16 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 5 et 9 de la foret.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x004A54D0</c> : 0 attendre, 1 se tourner,
    /// 2 tenir la distance, 3 marcher vers un cap. Et <b>trois gestes</b>, aiguilles par la posture
    /// tenue au moment ou le bit 2 se pose — c'est une de plus que le Crabe et le Grizzly.
    /// </para>
    /// <para>
    /// <b>Sa gravite est <c>0x7000</c></b>, reposee a chaque image avant l'integration, contre
    /// <c>0x6000</c> pour le Grizzly. Elle est plus lourde.
    /// </para>
    /// <para>
    /// <b>Son garde de posture exclut le bit 0</b> (<c>flags &amp; 0x11</c> et non <c>0x10</c>) :
    /// tant qu'elle n'a pas les quatre pattes au sol, elle ne prend aucune decision de posture.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A4AE0(byte* record)
    {
        byte* e = record;

        // Comme le Crabe et le Grizzly, il lui faut le bloc de MODELE : sa prise ampute l'attaque
        // de moitie.
        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideHurlingEntityAction_004A4AE0(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        StampBearFootfall(e);

        // Ghidra: LAB_004a5157 — sa gravite, reposee a chaque image et sur toutes les sorties.
        *(ushort*)(e + 0x9a) = 0x7000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanHurlingEntityAction_004A4AE0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a50df..0x004a5152</c> — <b>le pas qui secoue l'ecran</b>, sur le chemin
    /// commun de sortie, donc joue meme quand la bete ne pense pas.
    ///
    /// <para>
    /// Deux images de deux animations le declenchent : la quatrieme et la douzieme de l'animation
    /// <b>1</b> (la marche), la huitieme de l'animation <b>4</b> (la prise). Chacune pose
    /// <c>-8</c> dans <c>DAT_005de0ac</c> — le decalage vertical que la camera ajoute a sa cible —
    /// et joue le son <c>0x35</c>.
    /// </para>
    /// <para>
    /// L'amortissement est celui de <see cref="EntityHandler_00476730"/>, <c>a &lt;- (a / 4) - a</c>,
    /// donc une oscillation qui perd trois quarts par image et change de signe. <b>Mais celle-ci
    /// nettoie derriere elle</b> : sous <c>4</c> en valeur absolue la valeur est remise a zero,
    /// quand la secousse d'explosion laissait un decalage residuel pour toujours.
    /// </para>
    /// </summary>
    private static void StampBearFootfall(byte* e)
    {
        short animation = *(short*)(e + 0x18);
        short frame = *(short*)(e + 0x1a);

        bool footfall = (animation == 1 && (frame == 4 || frame == 0x0b))
                     || (animation == 4 && frame == 8);

        if (footfall)
        {
            *Mem.Camera(0x005de0ac) = -8;
            Aud.PlayPositionalSound(0x35, 0x200, 0x1800, (int*)(e + 0x1c));
        }

        int shake = *Mem.Camera(0x005de0ac);
        if (shake == 0) return;

        shake = (shake >> 2) - shake;
        *Mem.Camera(0x005de0ac) = shake;

        if (shake < 0) shake = -shake;
        if (shake < 4) *Mem.Camera(0x005de0ac) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004a4b40..0x004a50de</c> — <b>le premier temps</b> du Mechant ours.
    ///
    /// <para>
    /// <b>Trois gestes</b>, et c'est la posture <c>+0x42</c> tenue au moment ou le bit 2 s'est pose
    /// qui dit lequel : 0 la griffe, 1 la prise, 2 le souffle. Une posture au-dela de 2 en cours de
    /// geste ne fait rien du tout — le binaire retombe sur la sortie commune.
    /// </para>
    /// </summary>
    private static void DecideHurlingEntityAction_004A4AE0(
        byte* e, byte* model, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            // Le trait 0x10 coupe court : l'espece ne poursuit pas son geste.
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            switch (e[0x42])
            {
                case 0: ClawGesture_004A4DD1(e, boldness, heading); return;
                case 1: SeizeGesture_004A4C46(e, model, boldness, heading, flags); return;
                case 2: BreathGesture_004A4B89(e); return;
                default: return;
            }
        }

        // ================================================================ la posture tenue
        // Ghidra: `test al,11h` — le bit 0 est dans le garde, contrairement a ses voisines : une
        // bete en l'air ne change pas de posture.
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

        switch (held)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : tenir la distance
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                // Le cone de l'approche est deux fois plus etroit que celui du demi-tour.
                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x40, heading);
                *(int*)(e + 0x5c) = (boldness + 0x120) << 7;
                return;

            // ---------------------------------------------------------- 3 : marcher vers un cap
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // Elle DEPLACE sa position le long de +0x10 — elle ne pousse pas sa vitesse —,
                // puis tourne son corps vers la cible. Le cap suivi et le cap regarde sont donc
                // deux choses distinctes, comme chez 0x0049e190.
                int stride = (boldness + 0x100) << 7;
                *(int*)(e + 0x5c) = stride;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * stride) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x40, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a4dd1..0x004a4eeb</c> — <b>la griffe</b>, le geste de la posture 0.
    ///
    /// <para>
    /// Trois temps repris de l'image d'animation, sans compte a rebours : <b>avant la douzieme</b>
    /// elle se replace vers la cible, <b>a la douzieme</b> elle s'elance et grogne,
    /// <b>a partir de la treizieme</b> elle balaie le groupe a chaque image. Le geste se ferme a la
    /// fin de l'animation.
    /// </para>
    /// <para>
    /// Le cone de correction, <c>hardiesse / 2 + 0x20</c>, est deux fois plus large que celui du
    /// Grizzly dans le meme role — mais il court jusqu'a l'image 11 contre 8 chez lui.
    /// </para>
    /// </summary>
    private static void ClawGesture_004A4DD1(byte* e, int boldness, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 0x0c) TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x20, heading);

        if (frame == 0x0c)
        {
            int lunge = (boldness + 0x300) << 7;
            *(int*)(e + 0x5c) = lunge;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * lunge) >> 0xc;
            *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(0x3f, 0x4aa, 0x1800, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x1a) >= 0x0d)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), MeanBearProbe(0), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }
        }

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004a4c46..0x004a4dd0</c> — <b>la prise</b>, le geste de la posture 1, et le
    /// morceau qui n'appartient qu'a cette espece.
    ///
    /// <para>
    /// Le compte a rebours <c>+0x43</c> mene le geste, et a zero il efface <b>deux</b> bits :
    /// celui du geste et le <c>0x20</c> qui marque « la prise est faite ». Tant que <c>0x20</c> est
    /// pose, plus aucun balayage n'a lieu — on n'empoigne qu'une fois.
    /// </para>
    /// <para>
    /// <b>La poussee monte par QUARTS</b> vers <c>(hardiesse + 0x400) &lt;&lt; 7</c>, la ou toute la
    /// famille monte par seiziemes : la bete se jette en avant pour attraper.
    /// </para>
    /// <para>
    /// <b>La projection.</b> La victime encaisse la moitie de l'attaque du bloc de modele — le meme
    /// idiome que le Crabe et le Grizzly —, puis part a <b>quarante fois</b> le sinus et le cosinus
    /// d'un cap oblique, et decolle avec <c>0x54 = 0x18000</c> si elle n'etait pas deja en l'air.
    /// </para>
    /// <para>
    /// <b>Le sens de l'oblique se lit sur l'attaque du modele</b>, pas sur la geometrie :
    /// <c>-0x400</c> quand elle vaut <c>0x800</c> ou plus, <c>+0x400</c> sinon. C'est ce que dit le
    /// desassemblage (<c>cmp ebp,800h</c> sur la valeur <b>sauvegardee</b> de <c>modele + 0x30</c>,
    /// apres la restauration), et c'est transcrit tel quel. Aucune autre routine ne fait dependre
    /// une direction d'une statistique de degats ; on ne cherche pas ici a expliquer l'intention.
    /// </para>
    /// <para>
    /// <b>Elle se deplace SUR sa victime pour semer l'effet <c>0x39</c></b>, puis se remet ou elle
    /// etait : <see cref="SpawnEffectEntity"/> recopie la position de son modele, et c'est le seul
    /// moyen de faire naitre l'effet ailleurs qu'a ses propres pieds. Meme detour que chez le
    /// Grizzly.
    /// </para>
    /// </summary>
    private static void SeizeGesture_004A4C46(byte* e, byte* model, int boldness, int heading, uint flags)
    {
        if (e[0x43] != 0)
        {
            if (--e[0x43] == 0) *(uint*)e = flags & 0xffffffdd;   // le geste ET la prise
        }

        // Ghidra: LAB_004a4c58 — la montee de poussee par QUARTS, propre a ce geste.
        int push = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = push + ((((boldness + 0x400) << 7) - push) >> 2);

        TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x20, heading);

        if ((*(uint*)e & 0x20) != 0) return;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), MeanBearProbe(1), 0);
        if (prey != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        // Une victime que le coup vient d'emporter n'est pas projetee.
        if ((*(uint*)prey & 0x40000000) != 0) return;

        int bias = power >= 0x800 ? -0x400 : 0x400;

        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(bias + *(short*)(e + 0x32)) * 40;
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(bias + *(short*)(e + 0x32)) * 40;

        uint preyFlags = *(uint*)prey;
        if ((preyFlags & 1) == 0)
        {
            *(int*)(prey + 0x54) = 0x18000;
            *(uint*)prey = preyFlags | 1;
        }

        // Ghidra: LAB_004a4d8a — les trois mots de position sont echanges avec ceux de la victime
        // le temps de la naissance, puis rendus.
        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(prey + 0x1c);
        *(int*)(e + 0x20) = *(int*)(prey + 0x20);
        *(int*)(e + 0x24) = *(int*)(prey + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
    }

    /// <summary>
    /// Ghidra: <c>0x004a4b89..0x004a4c45</c> — <b>le souffle</b>, le geste de la posture 2 : la
    /// seule attaque a distance de toute la famille.
    ///
    /// <para>
    /// <c>+0x10</c> y sert de machine a deux temps. <b>A 1</b>, deux bruits ponctuent l'animation —
    /// aux images 11 et 13 — et sa derniere image fait naitre l'entite, puis <c>+0x10</c> retombe a
    /// 0 et un rebours de dix images s'arme. <b>A 0</b>, ce rebours s'ecoule et ferme le geste.
    /// </para>
    /// <para>
    /// <b>Le sous-type n'est pas une constante</b> : <c>0xc0</c> plus le neuvieme octet du premier
    /// bloc du repertoire de zone (<c>+0xcc</c>). Un sous-type <c>&gt;= 0xc0</c> designe la table
    /// <b>par sous-zone</b> <c>0x0056adc8</c> et non la table statique <c>0x00551c30</c> : le
    /// souffle de l'ours n'est donc pas le meme objet d'une carte a l'autre. Meme idiome que la
    /// trainee de <see cref="TickSceneEntityAi_0049A980"/>, a l'octet pres.
    /// </para>
    /// <para>
    /// <b><c>+0x9a</c> est mis a zero le temps de la naissance</b>, puis rendu :
    /// <see cref="SpawnEffectEntity"/> recopie les <c>0x9c</c> octets de l'entite, donc le souffle
    /// heriterait de la gravite de l'ours et tomberait comme une pierre.
    /// </para>
    /// </summary>
    private static void BreathGesture_004A4B89(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            if (*(short*)(e + 0x1a) == 0x0b)
                Aud.PlayPositionalSound(0x3f, 0x32a, 0x1800, (int*)(e + 0x1c));

            if (*(short*)(e + 0x1a) == 0x0d)
                Aud.PlayPositionalSound(0x3f, 0x384, 0x1800, (int*)(e + 0x1c));

            if ((*(uint*)e & 0x2000000) != 0)
            {
                short savedGravity = *(short*)(e + 0x9a);
                *(short*)(e + 0x9a) = 0;

                byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
                int subtype = (*(byte**)block)[9] + 0xc0;

                SpawnEffectEntity(e, (byte)subtype, 0xff, -1);

                *(short*)(e + 0x10) -= 1;
                *(short*)(e + 0x9a) = savedGravity;
                e[0x43] = 0x0a;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }
        else if (phase == 0)
        {
            // Ghidra: LAB_004a4c30 — le rebours de recuperation, et le geste se ferme a zero.
            if (--e[0x43] == 0) *(uint*)e &= 0xfffffffd;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004a51ad..0x004a54c5</c> — <b>le troisieme temps</b> : quelle posture, ou quel
    /// geste, pour la suite.
    ///
    /// <para>
    /// <b>La distance et le cap sont recalcules ici, en deux dimensions</b>, comme chez toutes ses
    /// voisines.
    /// </para>
    /// <para>
    /// <b>Les trois gestes se repartissent sur les trois plages</b>, et c'est ce qui rend la bete
    /// lisible : au corps a corps (sous <c>0xfa</c>) elle griffe ou elle empoigne, a moyenne
    /// distance (sous <c>0x1f4</c>) elle empoigne ou elle souffle, et de loin elle ne peut plus
    /// qu'empoigner — la prise est le seul geste qui parte a n'importe quelle distance.
    /// </para>
    /// </summary>
    private static void ReplanHurlingEntityAction_004A4AE0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x3c)
            {
                if (!inCone)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Le seuil AUGMENTE du tirage, comme chez l'abeille tueuse : plus elle est timide,
                // plus loin elle accepte de griffer.
                if (distance >= 0xc8 + CrtRand() % (timid * 2 + 1))
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: LAB_004a5254 — armer LA GRIFFE : posture 0, animation 3, bit de geste.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x4b)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            if (roll < 0x5a)
            {
                if (!inCone)
                {
                    WanderSceneEntity(e, boldness, timid, heading, 0x800);
                    return;
                }

                SeizeAt(e, boldness);
                return;
            }

            // 0x800 est un demi-tour : elle recule.
            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ la distance moyenne
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                // Ghidra: LAB_004a536b — un cone d'un demi-tour entier, et non celui de la
                // timidite : a cette distance elle empoigne des que la cible est DEVANT elle.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                SeizeAt(e, boldness);
                return;
            }

            if (roll < 0x46)
            {
                // Ghidra: LAB_004a53bd — armer LE SOUFFLE : posture 2, animation 5, machine a
                // deux temps mise a 1. Aucun compte a rebours : c'est l'animation qui mene.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 5;
                e[0x42] = 2;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                *(short*)(e + 0x10) = 1;
                return;
            }

            if (roll < 0x5a)
            {
                WanderSceneEntity(e, boldness, timid, heading, 0);
                return;
            }

            RestSceneEntity(e, timid);
            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            SeizeAt(e, boldness);
            return;
        }

        if (far < 0x3c)
        {
            RestSceneEntity(e, timid);
            return;
        }

        // Ghidra: LAB_004a549e — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_004a52d2, LAB_004a5393 et LAB_004a544c — <b>armer la prise</b> : le
    /// drapeau de geste, le bit de bouclage d'animation, l'animation 4, la posture 1 et un compte a
    /// rebours de <c>hardiesse / 8 + 0x18</c>. Les trois sites qui empoignent partagent ce corps.
    ///
    /// <para>Contrairement au bond du Grizzly, <b>le bit <c>0x1000000</c> est POSE et non
    /// efface</b> : l'animation de prise boucle, et c'est le rebours seul qui la termine.</para>
    /// </summary>
    private static void SeizeAt(byte* e, int boldness)
    {
        *(uint*)e |= 0x1000002;
        *(int*)(e + 0x18) = 4;
        e[0x42] = 1;
        e[0x43] = (byte)((boldness >> 3) + 0x18);
    }
}
