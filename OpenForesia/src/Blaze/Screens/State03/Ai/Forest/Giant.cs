// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GEANT — 0x004A2C90, sous-type 14 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// 2 308 octets, absents de `Blaze.c`. Quarante-sept etiquettes de saut.
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
    // 0x004A2C90 — LE GEANT, sous-type 14 du bloc B de la foret (sous-zones 6 a 8). Transcrite le
    // 2026-09-01 au desassembleur (Legacy/Raw/Blaze/FUN_004A2C90.asm, 2 308 octets, absente de
    // Blaze.c). Quarante-sept etiquettes de saut, table de sauts a 0x004A3594.
    //
    // LE CRIBLE L'A DESIGNEE, ET IL AVAIT RAISON. Sur les six routines qui restaient en zone 3,
    // c'est elle qui divergeait le moins d'un corps deja ecrit : 283 lignes du Grizzly 0x0049B840,
    // quand la suivante etait a 417. Le squelette est bien celui du Grizzly — deux gestes lus dans
    // +0x42, meme moitie d'attaque du bloc de modele, meme projection a 30x, meme detour par la
    // position de la victime pour y semer l'effet 0x39, meme gravite 0x6000.
    //
    // CE QUI LUI EST PROPRE TIENT EN TROIS MECANISMES :
    //
    //   - ELLE ARME UN ELAN QUI NE PART PAS TOUJOURS. Le compte a rebours +0x43 vaut 2 si la cible
    //     etait a 0x96 ou plus au moment de s'engager, 1 sinon — et les deux gestes ne s'elancent
    //     QUE si +0x43 vaut 2. Un Geant qui engage de trop pres joue son animation sans le bond.
    //     C'est un elan qui a besoin de place, et le binaire le mesure a l'engagement.
    //
    //   - SES DEUX GESTES QUITTENT LE SOL, pas seulement le bond : le coup de patte pose lui aussi
    //     le bit 0 et 0x17000 de vitesse verticale. Et tant qu'elle est en l'air, l'image
    //     d'animation du coup de patte est BLOQUEE a 0x11 — le geste se fige sur sa derniere image
    //     et n'en sort qu'a l'atterrissage.
    //
    //   - SA REPLANIFICATION A UN SECOND ESSAI. Si le cone du coup de patte echoue, elle ne se
    //     rabat pas sur l'hesitation : elle retente le BOND avec un cone TROIS FOIS plus large
    //     (timidite*4 + 0x300 contre +0x100). L'hesitation n'arrive qu'apres deux refus.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x005529d0</c> et <c>0x005529dc</c> — <b>les deux gabarits de portee du
    /// Geant</b>, releves dans <c>.data</c>. Ils comblent une partie du trou que le releve du
    /// 2026-08-29 laissait entre <c>0x00552978</c> et <c>0x005529e8</c>.
    ///
    /// <para>
    /// <b>Le gabarit du bond est le plus long de toute la serie</b> — <c>0x82</c> en portee avant
    /// contre <c>0x5a</c> au plus ailleurs — et il porte le decalage vertical <c>-0x100</c> de la
    /// griffe du Grizzly : elle tombe dessus. Celui du coup de patte est court et plat.
    /// </para>
    /// </summary>
    private static readonly short[] s_GiantProbes_005529D0 =
    [
        0x0000, 0x0032, 0x0032, 0x0064, 0x0000, 0x0078,                     // 0x005529d0 — la patte
        0x0000, 0x0032, 0x0082, 0x0064, unchecked((short)0xff00), 0x0096,   // 0x005529dc — le bond
    ];

    private static short* s_GiantProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_GiantProbes_005529D0"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* GiantProbe(int row)
    {
        if (s_GiantProbeStorage == null)
        {
            s_GiantProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_GiantProbes_005529D0.Length, sizeof(short));

            for (int i = 0; i < s_GiantProbes_005529D0.Length; i++)
            {
                s_GiantProbeStorage[i] = s_GiantProbes_005529D0[i];
            }
        }

        return s_GiantProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x004a2c90</c> — <b>l'intelligence du Geant</b>, sous-type 14 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x004A3594</c> : 0 attendre, 1 se tourner,
    /// 2 tenir la distance, 3 marcher vers un cap. <b>Deux gestes</b>, lus dans <c>+0x42</c> comme
    /// chez le Crabe et le Grizzly : posture 0 le coup de patte, posture 1 le bond.
    /// </para>
    /// <para>
    /// <b>Sa gravite est <c>0x6000</c></b>, celle du Grizzly, et non les <c>0x7000</c> du Mechant
    /// ours ni les <c>0x7fff</c> du Behemoth. Malgre son nom, ce n'est pas la plus lourde.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A2C90(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideGiantAction_004A2C90(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        *(ushort*)(e + 0x9a) = 0x6000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanGiantAction_004A2C90(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a2cf0..0x004a3593</c> — <b>le premier temps</b> du Geant.
    /// </summary>
    private static void DecideGiantAction_004A2C90(
        byte* e, byte* model, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            switch (e[0x42])
            {
                case 0: GiantSwipeGesture_004A324F(e, boldness, heading); return;
                case 1: GiantPounceGesture_004A2D2C(e, model, boldness, heading, flags); return;
                default: return;
            }
        }

        // ================================================================ la posture tenue
        // Le bit 0 est dans le garde, comme chez le Mechant ours : en l'air, aucune decision.
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

                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading);
                *(int*)(e + 0x5c) = (boldness + 0x100) << 7;
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

                // Elle DEPLACE sa position le long de +0x10 puis regarde la cible — le meme
                // decouplage que chez le Mechant ours et le Behemoth.
                *(int*)(e + 0x5c) = (boldness + 0xe0) << 7;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) +=
                    ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) +=
                    (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                // Ghidra: LAB_004a355e — le virage partage avec la detente du bond, atteint par un
                // saut et non par un appel ; le cone est le seul argument qui change.
                TurnSceneEntityTowardTarget(e, boldness + 0x80, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a324f..0x004a3393</c> — <b>le coup de patte</b>, le geste de la posture 0.
    ///
    /// <para>
    /// Avant la douzieme image elle se replace (cone <c>hardiesse / 2 + 0x40</c>), a la douzieme
    /// elle s'elance <b>si et seulement si <c>+0x43</c> vaut 2</b>, et elle balaie aux images
    /// <b>16 et 17</b> seulement — deux images, la fenetre la plus courte de la famille.
    /// </para>
    /// <para>
    /// <b>L'elan quitte le sol</b> : <c>+0x54 = 0x17000</c> et le bit 0 pose. Le Crabe et le
    /// Grizzly gardent leur coup de patte au ras du sol ; celui-ci est un saut.
    /// </para>
    /// <para>
    /// <b>Et tant qu'elle est en l'air, l'image d'animation est bloquee a <c>0x11</c></b> : le
    /// geste se fige sur sa derniere image et n'en sort qu'a l'atterrissage. C'est ce qui remplace
    /// le compte a rebours des autres — le geste dure exactement le temps du vol.
    /// </para>
    /// </summary>
    private static void GiantSwipeGesture_004A324F(byte* e, int boldness, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 0x0c) TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x40, heading);

        if (frame == 0x0c && e[0x43] == 2)
        {
            int lunge = (boldness + 0x300) << 7;
            *(int*)(e + 0x5c) = lunge;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) +=
                ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) +=
                (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(int*)(e + 0x54) = 0x17000;
            *(uint*)e |= 1;
            *(int*)(e + 0x5c) = 0;
        }

        short hitFrame = *(short*)(e + 0x1a);
        if (hitFrame >= 0x10 && hitFrame <= 0x11)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), GiantProbe(0), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }
        }

        uint flags = *(uint*)e;

        if ((flags & 1) != 0)
        {
            // Ghidra: LAB_004a3359 — l'image est bloquee tant que la bete n'a pas touche terre.
            if (*(short*)(e + 0x1a) > 0x11) *(short*)(e + 0x1a) = 0x11;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004a2d2c..0x004a2efc</c> — <b>le bond</b>, le geste de la posture 1.
    ///
    /// <para>
    /// <b>Deux phases sur <c>+0x10</c>, et non trois comme chez le Grizzly</b> : le temps de
    /// s'accroupir n'existe pas. A <b>1</b> elle se detend — a la septieme image, et seulement si
    /// <c>+0x43</c> vaut 2 —, a <b>0</b> elle empoigne ce qu'elle touche aux images 10 a 14.
    /// </para>
    /// <para>
    /// <b>La projection part vers la CIBLE, pas dans son propre cap</b> : le desassemblage ajoute
    /// le cap relatif <c>heading</c> a <c>+0x32</c> avant le sinus, ce que ni le Grizzly ni le
    /// Behemoth ne font. Elle envoie sa victime dans le prolongement de la ligne qui les joint.
    /// </para>
    /// <para>
    /// Le geste ne se ferme qu'au sol, comme celui du Grizzly, et la poussee verticale est
    /// <c>0x14000</c> — la plus faible des trois lourdes, contre <c>0x24000</c> pour le Grizzly.
    /// </para>
    /// </summary>
    private static void GiantPounceGesture_004A2D2C(
        byte* e, byte* model, int boldness, int heading, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        // -------------------------------------------------------------- 1 : detendre
        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 7)
            {
                if (e[0x43] == 2)
                {
                    int thrust = (boldness + 0x440) << 7;
                    *(int*)(e + 0x5c) = thrust;

                    int course = *(short*)(e + 0x32);
                    *(int*)(e + 0x50) +=
                        ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x58) +=
                        (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

                    *(int*)(e + 0x54) = 0x14000;
                    *(uint*)e |= 1;
                    *(int*)(e + 0x5c) = 0;
                }

                *(short*)(e + 0x10) -= 1;
            }

            // Ghidra: LAB_004a355e — le virage partage avec la posture 3.
            TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x40, heading);
            return;
        }

        if (phase != 0) return;

        // -------------------------------------------------------------- 0 : la chute et la prise
        if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
        {
            *(uint*)e = flags & 0xffffffdd;                 // le geste ET la prise
        }

        short frame = *(short*)(e + 0x1a);
        if (frame < 0x0a || frame > 0x0e) return;

        if ((*(uint*)e & 0x20) != 0) return;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), GiantProbe(1), 0);
        if (prey != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if ((*(uint*)prey & 0x40000000) != 0) return;

        // La direction est celle de la CIBLE, cap relatif compris.
        int impact = heading + *(short*)(e + 0x32);
        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(impact) * 30;

        uint preyFlags = *(uint*)prey;
        if ((preyFlags & 1) == 0)
        {
            *(int*)(prey + 0x54) = 0x18000;
            *(uint*)prey = preyFlags | 1;
        }

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
    /// Ghidra: <c>0x004a2f57..0x004a324e</c> — <b>le troisieme temps</b> du Geant.
    ///
    /// <para>
    /// <b>Le corps a corps a un second essai</b>, et c'est ce qui la distingue le plus a la
    /// lecture. Si le cone du coup de patte echoue — ou si la cible est trop loin —, elle ne se
    /// rabat pas sur l'hesitation : elle retente le <b>bond</b> avec un cone <b>trois fois plus
    /// large</b>, <c>timidite * 4 + 0x300</c> contre <c>+ 0x100</c>. L'hesitation n'arrive
    /// qu'apres deux refus.
    /// </para>
    /// </summary>
    private static void ReplanGiantAction_004A2C90(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0x12c)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                bool narrowCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

                if (narrowCone && distance < 0x12c + CrtRand() % (timid * 2 + 1))
                {
                    ArmGiantSwipe(e, distance);
                    return;
                }

                // Ghidra: LAB_004a302b — le second essai, sur un cone trois fois plus large.
                bool wideCone = ((heading + timid * 4 + 0x300) & 0xfff) <= timid * 8 + 0x600;

                if (wideCone && distance < 0x12c + CrtRand() % (timid * 2 + 1))
                {
                    ArmGiantPounce(e, distance);
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x4b)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ la distance moyenne
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                // Un demi-tour de part et d'autre : a cette distance il suffit qu'elle vous voie.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                ArmGiantPounce(e, distance);
                return;
            }

            if (roll < 0x46)
            {
                WanderSceneEntity(e, boldness, timid, heading, 0);
                return;
            }

            if (roll < 0x5a)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            RestSceneEntity(e, timid);
            return;
        }

        // ================================================================ le lointain
        if (CrtRand() % 100 < 0x32)
        {
            RestSceneEntity(e, timid);
            return;
        }

        // Ghidra: LAB_004a3223 — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_004a2ff6 — <b>armer le coup de patte</b> : le drapeau de geste,
    /// l'animation 3, la posture 0, et le compte a rebours que
    /// <see cref="GiantSwipeGesture_004A324F"/> relit pour savoir s'il s'elance.</summary>
    private static void ArmGiantSwipe(byte* e, int distance)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 3;
        e[0x42] = 0;
        e[0x43] = GiantLungeGate(distance);
        *(int*)(e + 0x5c) = 0;
        *(uint*)e = (flags & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: LAB_004a3068 et LAB_004a313a — <b>armer le bond</b> : l'animation 5, la
    /// posture 1, la machine a deux temps mise a 1, et le meme compte a rebours. Les deux sites qui
    /// bondissent partagent ce corps.</summary>
    private static void ArmGiantPounce(byte* e, int distance)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 5;
        e[0x42] = 1;
        e[0x43] = GiantLungeGate(distance);
        *(ushort*)(e + 0x10) = 1;
        *(uint*)e = (flags & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: le motif <c>cmp eax,96h ; setge cl ; inc ecx</c>, emis trois fois —
    /// <b>2 si la cible etait a <c>0x96</c> ou plus au moment de s'engager, 1 sinon</b>.
    ///
    /// <para>Ce n'est pas une duree : c'est le <b>verrou de l'elan</b>. Les deux gestes ne
    /// s'elancent que sur la valeur 2, donc <b>un Geant qui engage de trop pres joue son animation
    /// sans le bond</b>. La distance est mesuree une fois, a l'engagement, et ne se relit
    /// pas.</para></summary>
    private static byte GiantLungeGate(int distance) => (byte)(distance >= 0x96 ? 2 : 1);
}
