// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DRAGON ROUGE — 0x00499140, sous-type 1 du bloc 0x00554ca8 (table 0x0056ad48), montagne du
// dragon de feu (zone 2, sous-zone 0).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 2, sous-zone 0, espece 0, modele 0 :
// « Dragon rouge », 8 000 PV, 740 PM, rang 306 — LE PLUS GROS DU DEPOT, pres de dix fois le Troll). 841
// instructions, absentes de Blaze.c ; releve Legacy/Raw/Blaze/FUN_00499140.asm, trois tables de
// sauts contiguës : 0x00499DAC (cinq gestes), 0x00499DC0 (quatre etats d'incantation),
// 0x00499DD0 (quatre postures). Aucun jumeau (diff STRICT et LOOSE du 2026-09-17).
//
// CE QU'IL REUNIT, ET QU'AUCUNE AUTRE BETE DU DEPOT N'A ENSEMBLE :
//
//   - CINQ GESTES ET UNE INCANTATION. Deux coups au corps a corps, un bond, deux souffles — et le
//     mecanisme d'incantation du Behemoth, au complet, sur sa propre table de trois sorts :
//     Superman (2,14), Boule de feu (0,20), Meteores (0,27).
//   - SES DEUX COUPS NE SE RESSEMBLENT PAS. Le premier porte une fois, a demi-degats, et projette
//     la victime (le bloc du Troll, emis deux fois de plus ici). Le second n'a pas de verrou : il
//     frappe a chaque image a partir de la douzieme, a pleins degats, sans projeter.
//   - SON BOND POUSSE A 0x50000 EN AVANT ET 0x64000 VERS LE HAUT : la hauteur du pietinement du
//     Behemoth, et deux fois la poussee du bond empoisonne du Troll. Le Behemoth decolle sur place ;
//     le dragon part aussi en avant.
//   - SES DEUX SOUFFLES SONT CEUX DU JEUNE DRAGON, a deux ecarts pres : le premier range 0x4e20 la
//     ou le cadet rangeait 0x3a98, et le second part de la gueule mais lit l'octet 0x13 du
//     repertoire de zone (le cadet lisait 0x0d), avec 3 au lieu de 2 en dernier argument, et ne
//     remet pas les decalages en place apres la naissance.
//   - SA PORTEE DE DECISION EST 0x1f4, PAS 0x12c : il se juge « au contact » de plus loin que toute
//     la famille terrestre. Mais sa posture d'attente garde le seuil 0x12c.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00552930</c>, <c>0x0055293c</c> et <c>0x00552948</c> — <b>les trois gabarits de
    /// portee du Dragon rouge</b>, un par geste qui frappe, releves dans <c>.data</c>
    /// (<c>OpenForesia.Tools -- at</c>).
    ///
    /// <para>
    /// Six termes chacun, dans l'ordre de <see cref="s_MeleeProbes_00562cf0"/>. Le premier a un
    /// decalage vertical <b>positif</b> (<c>0x100</c>), comme la prise du Behemoth, et porte plus
    /// loin que les deux autres (<c>0x78</c>, largeur <c>0xfa</c>). <b>Le second et le troisieme ont les memes
    /// octets</b> : le binaire les range pourtant a deux adresses, et chaque geste lit la sienne.
    /// </para>
    /// <para>
    /// <b>Bornes.</b> Ce qui precede, <c>0x00552920</c>, est la table de sorts de la Salamandre de la
    /// montagne. Apres le troisieme gabarit viennent quatre octets nuls (<c>0x00552954</c>), puis la
    /// table de sorts <see cref="s_RedDragonSpells_00552958"/>.
    /// </para>
    /// </summary>
    private static readonly short[] s_RedDragonProbes_00552930 =
    [
        0x0000, 0x0032, 0x0078, 0x0096, 0x0100, 0x00fa,   // 0x00552930 — le coup qui projette
        0x0000, 0x0032, 0x005a, 0x0096, 0x0000, 0x00c8,   // 0x0055293c — le coup sans verrou
        0x0000, 0x0032, 0x005a, 0x0096, 0x0000, 0x00c8,   // 0x00552948 — le bond
    ];

    private static short* s_RedDragonProbeStorage;

    /// <summary>Une des trois entrees de <see cref="s_RedDragonProbes_00552930"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* RedDragonProbe(int row)
    {
        if (s_RedDragonProbeStorage == null)
        {
            s_RedDragonProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_RedDragonProbes_00552930.Length, sizeof(short));

            for (int i = 0; i < s_RedDragonProbes_00552930.Length; i++)
            {
                s_RedDragonProbeStorage[i] = s_RedDragonProbes_00552930[i];
            }
        }

        return s_RedDragonProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x00552958</c> — <b>les trois sorts du Dragon rouge</b>, lus par
    /// <c>mov ax,[edx*2+552958h]</c> avec <c>edx = rand() % 3</c>. Octet haut la famille, octet bas
    /// l'indice, comme <see cref="s_BehemothSpells_00552A30"/> :
    /// <c>0x020e</c> Superman, <c>0x0014</c> Boule de feu, <c>0x001b</c> Meteores
    /// (<c>Bestiaire/sorts.csv</c>). Suivie de deux octets nuls, puis du gabarit de la griffe du
    /// Grizzly (<c>0x00552960</c>).
    /// </summary>
    private static readonly ushort[] s_RedDragonSpells_00552958 = [0x020e, 0x0014, 0x001b];

    /// <summary>Ghidra: le motif <c>rand() % 10 + 0x14</c>, emis quatre fois — <b>la duree d'une
    /// posture</b> du Dragon rouge, de 20 a 29 images. Comme le Troll, il ne module pas sa
    /// timidite.</summary>
    private static byte RedDragonDelay() => (byte)(CrtRand() % 10 + 0x14);

    /// <summary>
    /// Ghidra: <c>0x00499140</c> — <b>l'intelligence du Dragon rouge</b>, sous-type 1 de la table
    /// installee <c>0x0056ad48</c> dans la montagne du dragon de feu.
    ///
    /// <para>
    /// Les trois temps de la famille terrestre. <b>La hardiesse est lue</b>, contrairement au Troll et
    /// au Jeune dragon — mais a deux endroits seulement : le cone de l'incantation et la largeur du
    /// cap tire pour marcher. Le bloc de modele sert a diviser l'attaque par deux le temps d'un coup.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_00499140(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        DecideRedDragonAction_00499140(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_0049991d — la gravite du Jeune dragon et du Troll.
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanRedDragonAction_00499979(e, boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x00499189..0x0049991c</c> — <b>le premier temps</b> : mener le geste en cours,
    /// mener l'incantation, ou tenir la posture.
    /// </summary>
    private static void DecideRedDragonAction_00499140(
        byte* e, byte* model, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_00499750
            if ((flags & 0x10) != 0) return;

            if ((flags & 0x804) != 0)
            {
                AdvanceRedDragonIncantation_00499763(e, boldness, heading, traits, flags);
                return;
            }

            HoldRedDragonStance_004998B1(e, distance, heading, flags);
            return;
        }

        // Le trait 0x10 coupe court : l'espece ne poursuit pas son geste.
        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        // Ghidra: LAB_004991be — la table de sauts de 0x00499DAC.
        switch (e[0x42])
        {
            case 0: RedDragonThrowingStrike_004991D3(e, model, heading); return;
            case 1: RedDragonUnlockedStrike_00499391(e, heading); return;
            case 2: RedDragonLeap_00499488(e, model, flags); return;
            case 3: RedDragonBreath_00499620(e, flags); return;
            case 4: RedDragonMouthBreath_00499698(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004991d3</c> — <b>le coup qui projette</b>, geste 0, sur l'animation 3.
    ///
    /// <para>
    /// Il se replace jusqu'a l'image 5 (cone <c>0x40</c>), s'elance a l'image 8 (a-coup de
    /// <c>0x1c000</c>), et porte a partir de l'image 11 — <b>une seule fois</b> (verrou <c>0x20</c>)
    /// et a <b>demi-degats</b>, l'attaque du bloc de modele divisee le temps de la resolution. Une
    /// victime encore debout est projetee par le bloc que le Troll emet trois fois, et que le dragon
    /// emet deux fois de plus (<see cref="ThrowTrollVictim_0049684C"/> : memes ecritures, a l'ordre
    /// des registres pres).
    /// </para>
    /// </summary>
    private static void RedDragonThrowingStrike_004991D3(byte* e, byte* model, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 5) TurnSceneEntityTowardTarget(e, 0x40, heading);

        // Ghidra: LAB_00499211
        if (frame == 8) RedDragonLunge(e, 0x1c000);

        // Ghidra: LAB_00499257
        if (*(short*)(e + 0x1a) >= 0x0b && (*(uint*)e & 0x20) == 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), RedDragonProbe(0), 0);
            if (prey != null) *(uint*)e |= 0x20;

            // Ghidra: LAB_0049928b
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                RedDragonHalfPowerHit(e, model, prey);
            }
        }

        // Ghidra: LAB_00499374 — la fin d'animation ferme le geste ET le verrou.
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xffffffdd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00499391</c> — <b>le coup sans verrou</b>, geste 1, sur l'animation 9.
    ///
    /// <para>
    /// Plus lent a venir que le geste 0 — il se replace jusqu'a l'image 8 et s'elance a l'image 11,
    /// du meme <c>0x1c000</c> —, mais il ne pose <b>ni ne lit</b> le bit <c>0x20</c> : a partir de
    /// l'image 12 il frappe <b>a chaque image</b>, jusqu'a la fin de l'animation. Degats
    /// <b>pleins</b>, aucune projection.
    /// </para>
    /// </summary>
    private static void RedDragonUnlockedStrike_00499391(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 8) TurnSceneEntityTowardTarget(e, 0x40, heading);

        // Ghidra: LAB_004993cf
        if (frame == 0x0b) RedDragonLunge(e, 0x1c000);

        // Ghidra: LAB_00499415
        if (*(short*)(e + 0x1a) >= 0x0c)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), RedDragonProbe(1), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        // Ghidra: LAB_0049946b — ici seul le geste est ferme.
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00499488</c> — <b>le bond</b>, geste 2, sur l'animation 4.
    ///
    /// <para>
    /// <b>Phase 1, le saut</b> : poussee de <c>0x50000</c> dans le cap et <c>0x64000</c> vers le haut
    /// — la hauteur du pietinement du Behemoth —, bit « en l'air » pose, et la phase tombe a 0 dans
    /// la meme image.
    /// </para>
    /// <para>
    /// <b>Phase 0, la chute</b> : le geste et le verrou ne se ferment qu'au sol et en fin
    /// d'animation. A partir de l'image 18 il porte une fois, a demi-degats, et projette — le meme
    /// coup que le geste 0, sur le troisieme gabarit. <b>Aucune condition sur la vitesse
    /// verticale</b> : la ou le Troll ne touche qu'en montant, le dragon touche aussi en retombant.
    /// </para>
    /// <para>
    /// <b>Ce geste ne remet jamais <c>+0x5c</c> a zero</b> : ses sorties sautent directement a
    /// l'application, sans passer par le zero commun des autres gestes.
    /// </para>
    /// </summary>
    private static void RedDragonLeap_00499488(byte* e, byte* model, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0x50000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x54) = 0x64000;
            *(uint*)e |= 1;                      // « en l'air »
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 0) return;

        // Ghidra: LAB_004994ef — EAX porte encore le mot d'etat lu apres FUN_004db5a0.
        if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
        {
            *(uint*)e = flags & 0xffffffdd;
        }

        // Ghidra: LAB_004994fe
        if (*(short*)(e + 0x1a) < 0x12) return;
        if ((*(uint*)e & 0x20) != 0) return;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), RedDragonProbe(2), 0);
        if (prey != null) *(uint*)e |= 0x20;

        // Ghidra: LAB_00499532
        if (*Mem.Camera(0x005de0b8) == 0) return;

        RedDragonHalfPowerHit(e, model, prey);
    }

    /// <summary>
    /// Ghidra: <c>0x00499298..0x00499371</c> et <c>0x0049953f..0x00499618</c> — <b>le coup a
    /// demi-degats</b>, emis deux fois a l'identique par les gestes 0 et 2.
    ///
    /// <para>
    /// La victime clignote seize images et prend le bit <c>0x40</c> ; l'attaque du bloc de modele
    /// (<c>+0x30</c>) est divisee par deux sur seize bits (<c>sar ax,1</c>) le temps de
    /// <see cref="ResolveMeleeHit_00539B80"/>, puis rendue ; le choc sonne ; et si la victime n'est
    /// pas a terre (<c>0x40000000</c>), elle est projetee.
    /// </para>
    /// </summary>
    private static void RedDragonHalfPowerHit(byte* e, byte* model, byte* prey)
    {
        // Ghidra: `mov [edi+96h],bx` — EBX vaut encore le 0x10 du masque de trait.
        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)prey & 0x40000000) != 0) return;

        ThrowTrollVictim_0049684C(e, prey);
    }

    /// <summary>Ghidra: <c>0x00499217</c> et <c>0x004993d5</c> — <b>l'a-coup en avant</b> des deux
    /// coups : la poussee est posee, projetee sur le cap par les tables de sinus et de cosinus,
    /// ajoutee a la vitesse, puis remise a zero.</summary>
    private static void RedDragonLunge(byte* e, int thrust)
    {
        *(int*)(e + 0x5c) = thrust;

        int course = *(short*)(e + 0x32);
        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
        *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00499620</c> — <b>le premier souffle</b>, geste 3, sur l'animation 5.
    ///
    /// <para>
    /// Le premier souffle du Jeune dragon (<c>0x004946a3</c>) sans son cri : a la fin de
    /// l'animation, un pile ou face dans <c>+0x44</c>, <b><c>0x4e20</c></b> dans <c>+0x48</c> (le
    /// cadet y range <c>0x3a98</c>), la naissance de l'entite <c>0xc0 + octet 0x0e</c> du repertoire
    /// <c>+0xcc</c>, puis les deux champs rendus et un rebours de dix images.
    /// </para>
    /// </summary>
    private static void RedDragonBreath_00499620(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CloseRedDragonGesture_00499736(e, flags); return; }
        if (phase != 1 || (flags & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        int savedX = *(int*)(e + 0x44);
        int savedY = *(int*)(e + 0x48);

        *(int*)(e + 0x48) = 0x4e20;
        *(int*)(e + 0x44) = CrtRand() & 1;

        byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
        SpawnEffectEntity(e, (byte)((*(byte**)block)[0x0e] + 0xc0), 0, 2);   // Ghidra: FUN_0047a9d0

        *(short*)(e + 0x10) -= 1;
        *(int*)(e + 0x44) = savedX;
        *(int*)(e + 0x48) = savedY;
        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00499698</c> — <b>le souffle de la gueule</b>, geste 4, sur l'animation 8.
    ///
    /// <para>
    /// Le son 2 a l'image 7, puis <b>a la fin de l'animation</b> — la ou le Jeune dragon souffle a
    /// l'image 28 — la rangee 0 du tableau d'attaches du modele est posee dans
    /// <c>+0x44/+0x48/+0x4c</c> et l'entite <c>0xc0 + octet 0x13</c> du repertoire <c>+0xcc</c> nait,
    /// avec <c>3</c> en dernier argument. Rebours de <b>quarante</b> images.
    /// </para>
    /// <para>
    /// <b>Rien n'est sauve ni rendu</b> : ni la gravite, ni le tangage, ni la poussee que le cadet
    /// mettait de cote, ni les trois decalages. Ils gardent le point d'attache jusqu'a ce qu'un autre
    /// geste les reecrive.
    /// </para>
    /// </summary>
    private static void RedDragonMouthBreath_00499698(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CloseRedDragonGesture_00499736(e, flags); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }

        if (*(short*)(e + 0x1a) == 7)
        {
            Aud.PlayPositionalSound(2, 0x100, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }

        // Ghidra: LAB_004996cb
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        short* anchor = ModelAnchorRow_00535770(e, 0);
        *(int*)(e + 0x44) = anchor[0] << 0xc;
        *(int*)(e + 0x48) = anchor[1] << 0xc;
        *(int*)(e + 0x4c) = anchor[2] << 0xc;

        byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
        SpawnEffectEntity(e, (byte)((*(byte**)block)[0x13] + 0xc0), 0, 3);   // Ghidra: FUN_0047a9d0

        *(short*)(e + 0x10) -= 1;
        e[0x43] = 0x28;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: LAB_00499736 — <b>le rebours de recuperation</b>, partage par les deux
    /// souffles : la phase 0 decompte <c>+0x43</c> et ferme le geste a zero, avec le mot d'etat lu en
    /// tete (<c>EAX</c>).</summary>
    private static void CloseRedDragonGesture_00499736(byte* e, uint flags)
    {
        if (--e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00499763</c> — <b>l'incantation</b>, table de sauts a <c>0x00499DC0</c>. Le
    /// mecanisme du Behemoth (<see cref="AdvanceIncantation_004A5919"/>) instruction pour instruction
    /// dans sa logique, a deux donnees pres : <b>le sort part a l'image 19</b> de l'animation 7 (le
    /// Behemoth a la quatorzieme), et il est tire dans <see cref="s_RedDragonSpells_00552958"/>.
    ///
    /// <list type="number">
    /// <item><b>3</b> — l'entite de lancement <c>0xbd</c> nait, un sort est tire et range en
    /// <c>+0x06</c>, et la duree vient du <c>+0x13</c> de sa fiche.</item>
    /// <item><b>2</b> — le rebours s'ecoule, le dragon se tourne dans un cone de
    /// <c>hardiesse × 4 + 0x32</c>.</item>
    /// <item><b>1</b> — l'animation 7 est posee, et a sa dix-neuvieme image le sort part.</item>
    /// <item><b>0</b> — la fin de l'animation efface le bit <c>0x800</c>.</item>
    /// </list>
    /// <para>
    /// Le masque de traits <c>0x400003e8</c> annule tout avant le dispatch : bits <c>0x804</c>
    /// effaces, rebours a zero.
    /// </para>
    /// </summary>
    private static void AdvanceRedDragonIncantation_00499763(
        byte* e, int boldness, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;
            e[0x43] = 0;
            *(uint*)e = flags;
        }

        // Ghidra: LAB_00499778 — `movsx ecx,ax` puis `cmp ecx,3` / `ja` : une valeur negative sort.
        short state = *(short*)(e + 0x10);
        if ((uint)state > 3) return;

        switch (state)
        {
            // ---------------------------------------------------------- 0 : rendre la main
            case 0:
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffff7ff;
                return;

            // ---------------------------------------------------------- 1 : lancer
            case 1:
                if (*(ushort*)(e + 0x18) != 7)
                {
                    *(int*)(e + 0x18) = 7;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                // Ghidra: LAB_004997c2
                if (*(short*)(e + 0x1a) != 0x13) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            // ---------------------------------------------------------- 2 : incanter
            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) -= 1;

                // Ghidra: LAB_0049980a
                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;

            // ---------------------------------------------------------- 3 : choisir le sort
            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                ushort sheet = s_RedDragonSpells_00552958[CrtRand() % 3];
                *(ushort*)(e + 6) = sheet;

                // Ghidra: `mov ecx,[ecx+9Ch] ; mov ecx,[ecx+edx*4] ; mov dl,[ecx+eax+13h]`.
                byte* family = ((byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)))
                                   [(sheet >> 8) & 0xff];

                *(short*)(e + 0x10) -= 1;
                e[0x43] = family[(sheet & 0xff) * 0x30 + 0x13];
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: LAB_004998b1 — <b>la posture tenue</b>, table de sauts a <c>0x00499DD0</c>. Les quatre
    /// postures du Troll, a trois nombres pres : l'approche pousse <c>0x14000</c> (Troll
    /// <c>0x12000</c>), la marche <c>0x10000</c> (Troll <c>0xe000</c>), et la marche corrige son cap
    /// dans un cone de <c>0x60</c> au lieu de <c>0x80</c>.
    /// </summary>
    private static void HoldRedDragonStance_004998B1(byte* e, int distance, int heading, uint flags)
    {
        if ((flags & 1) != 0) return;

        byte stance = e[0x42];
        if (stance > 3) { RedDragonStepSound_004998EF(e); return; }

        switch (stance)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                // Ghidra: LAB_004998d8 — le seuil d'attente reste 0x12c, pas le 0x1f4 du replan.
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    break;
                }

                // Ghidra: LAB_00499c12
                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 2 : s'approcher
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    break;
                }

                // Ghidra: LAB_00499cba
                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x14000;
                break;

            // ---------------------------------------------------------- 3 : marcher
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                *(int*)(e + 0x5c) = 0x10000;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, 0x60, heading);
                break;
            }
        }

        RedDragonStepSound_004998EF(e);
    }

    /// <summary>Ghidra: LAB_004998ef — <b>le pas</b>, aux images 15 et 34 de l'animation 1 : le son
    /// <c>0x45</c>, que ni le Troll ni le Jeune dragon n'emploient (tous deux posent
    /// <c>0x35</c>).</summary>
    private static void RedDragonStepSound_004998EF(byte* e)
    {
        if (*(ushort*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame != 0x0f && frame != 0x22) return;

        Aud.PlayPositionalSound(0x45, 0x200, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x00499979..0x00499c11</c> — <b>le troisieme temps</b> : quelle posture prendre. La
    /// distance et le cap sont recalcules a plat.
    ///
    /// <para>
    /// <b>Sous 0x1f4</b>, un tirage sur cent : moins de 0x32 il <b>frappe</b> si la proie tient dans
    /// un cone de <c>0x400</c> — impair le coup qui projette, pair le coup sans verrou —, moins de
    /// 0x4b il s'approche a <c>0xc8</c>, au-dela il marche autour.
    /// </para>
    /// <para>
    /// <b>Au-dela</b> : moins de 0x1e, et la proie dans le cone, un <b>second tirage a trois</b>
    /// choisit le bond, le premier souffle ou le souffle de la gueule ; moins de 0x32 il s'approche ;
    /// moins de 0x46, la proie dans le cone, il <b>incante</b> ; moins de 0x5a il marche autour ;
    /// sinon il se repose.
    /// </para>
    /// <para>
    /// <b>Les deux tests de cone ne sont pas le meme</b> : le corps a corps hesite au-dela de
    /// <c>0x400</c> (<c>jg</c>), la distance des <c>0x400</c> compris (<c>jge</c>). Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void ReplanRedDragonAction_00499979(byte* e, int boldness)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + 0x200) & 0xfff) > 0x400)
                {
                    HesitateSceneEntity(e);       // Ghidra: LAB_00499b92
                    return;
                }

                if ((roll & 1) != 0) ArmRedDragonStrike(e, animation: 3, posture: 0);
                else                 ArmRedDragonStrike(e, animation: 9, posture: 1);   // Ghidra: LAB_00499a1f
                return;
            }

            // Ghidra: LAB_00499a42
            if (roll < 0x4b)
            {
                ApproachRedDragon(e);
                return;
            }

            WalkRedDragonAround_00499BA7(e, boldness, heading);
            return;
        }

        // ================================================================ la distance
        {
            // Ghidra: LAB_00499a70
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + 0x200) & 0xfff) >= 0x400)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                switch (CrtRand() % 3)
                {
                    case 0: ArmRedDragonPhasedGesture(e, animation: 4, posture: 2); return;   // Ghidra: LAB_00499b07
                    case 1: ArmRedDragonPhasedGesture(e, animation: 5, posture: 3); return;   // Ghidra: LAB_00499ae3
                    case 2: ArmRedDragonPhasedGesture(e, animation: 8, posture: 4); return;
                    default: return;
                }
            }

            // Ghidra: LAB_00499b2d
            if (roll < 0x32)
            {
                ApproachRedDragon(e);
                return;
            }

            // Ghidra: LAB_00499b57 — l'incantation.
            if (roll < 0x46)
            {
                if (((heading + 0x200) & 0xfff) >= 0x400)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 6;
                *(ushort*)(e + 0x10) = 3;
                *(uint*)e = state | 0x1000804;
                return;
            }

            // Ghidra: LAB_00499ba2
            if (roll < 0x5a)
            {
                WalkRedDragonAround_00499BA7(e, boldness, heading);
                return;
            }

            // Ghidra: LAB_00499bf3 — se reposer.
            e[0x42] = 0;
            e[0x43] = RedDragonDelay();
        }
    }

    /// <summary>Ghidra: <c>0x004999fd</c> et LAB_00499a1f — <b>armer un coup</b> : animation, posture,
    /// poussee a zero, bit de geste pose et <c>0x1000000</c> efface. Pas de phase : les deux coups se
    /// reglent sur l'image d'animation.</summary>
    private static void ArmRedDragonStrike(byte* e, int animation, byte posture)
    {
        uint state = *(uint*)e;
        *(int*)(e + 0x18) = animation;
        e[0x42] = posture;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e = (state & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: LAB_00499b07, LAB_00499ae3 et <c>0x00499abd</c> — <b>armer un geste a
    /// phases</b> (le bond et les deux souffles) : animation, posture, <c>+0x10 = 1</c>, bit de geste
    /// pose et <c>0x1000000</c> efface. La poussee n'est pas touchee.</summary>
    private static void ArmRedDragonPhasedGesture(byte* e, int animation, byte posture)
    {
        uint state = *(uint*)e;
        *(int*)(e + 0x18) = animation;
        e[0x42] = posture;
        *(ushort*)(e + 0x10) = 1;
        *(uint*)e = (state & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: LAB_00499a4b et LAB_00499b32 — <b>s'approcher</b> jusqu'a <c>0xc8</c>, pour
    /// une duree tiree. Le meme bloc emis deux fois ; le tirage precede l'ecriture de
    /// <c>+0x10</c>.</summary>
    private static void ApproachRedDragon(byte* e)
    {
        e[0x42] = 2;
        byte delay = RedDragonDelay();
        *(ushort*)(e + 0x10) = 0xc8;
        e[0x43] = delay;
    }

    /// <summary>
    /// Ghidra: LAB_00499ba7 — <b>marcher autour</b> : posture 3, et un cap tire <b>a l'oppose</b> de
    /// la proie (<c>+0x800</c>), dans une fourchette dont la largeur depend de la hardiesse —
    /// <c>(hardiesse + 0x80) × 16</c>, centree.
    ///
    /// <para>
    /// Le calcul est fait sur 32 bits puis seul le mot bas est range ; la somme avec <c>+0x32</c> est
    /// un <c>add dx</c> de seize bits, sans effet sur ce mot bas.
    /// </para>
    /// </summary>
    private static void WalkRedDragonAround_00499BA7(byte* e, int boldness, int heading)
    {
        e[0x42] = 3;
        e[0x43] = RedDragonDelay();

        int width = (boldness + 0x80) << 4;
        int course = CrtRand() % width - (width >> 1) + heading;
        course += *(short*)(e + 0x32) + 0x800;
        *(ushort*)(e + 0x10) = (ushort)course;
    }
}
