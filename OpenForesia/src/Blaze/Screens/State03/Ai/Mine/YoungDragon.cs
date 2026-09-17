// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE JEUNE DRAGON — 0x004944E0, sous-type 13 du bloc C de la mine (zone 1, sous-zone 4).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, sous-zone 4, modele 2 : « Jeune
// dragon », 700 PV, 50 PM, rang 40 — de loin le plus gros de la mine avant le Troll). 652
// instructions, absentes de Blaze.c ; releve Legacy/Raw/Blaze/FUN_004944E0.asm, table de sauts a
// quatre entrees a 0x00494CF4. Aucun jumeau : le corps le plus proche du depot en differe de 393
// lignes sur 652.
//
// CE QUI LE DISTINGUE DE LA FAMILLE TERRESTRE (Crabe, Grizzly, Geant) :
//
//   - IL SOUFFLE DEUX FOIS, PAR DEUX GESTES DIFFERENTS, ET AUCUN DES DEUX N'EST UN COUP. Les
//     postures 1 et 2 ne consultent jamais un gabarit de portee : elles font NAITRE une entite,
//     dont le sous-type est lu dans le repertoire de zone (+0xcc) — l'octet 0x0e pour la posture 1,
//     l'octet 0x0d pour la posture 2. C'est le meme mecanisme que le souffle de l'Ours mechant.
//   - SON SOUFFLE DE POSTURE 2 PART DE SA GUEULE. A l'image 28 il lit la rangee 0 du tableau
//     d'attaches de son bloc de modele (FUN_00535770, modele+0x40) et la pose dans
//     +0x44/+0x48/+0x4c — les trois decalages que la naissance recopie. Les deux autres souffles du
//     depot partent du centre de la bete ; celui-ci a une bouche.
//   - SA GRAVITE EST 0x7FFF, la plus forte ecrite par une routine d'intelligence (le Grizzly pose
//     0x6000, l'Ours mechant 0x7000). Elle est reposee a chaque image, sur toutes les sorties.
//   - SA MORSURE EMPOISONNE une fois sur dix (effet 12, +0x07 = 7), et son clignotement de degat
//     ne dure que SIX images la ou toute la famille en pose seize.
//   - IL NE TOURNE JAMAIS SELON SA HARDIESSE. Les quatre cones sont des constantes — 0x40 dans le
//     geste, 0x80 partout ailleurs. LA HARDIESSE N'EST PAS LUE UNE SEULE FOIS par ce corps, bien
//     que ReadSceneEntitySituation la calcule pour lui.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005528d8</c> — <b>le gabarit de portee de la morsure</b> du Jeune dragon, releve
    /// dans <c>.data</c> (<c>OpenForesia.Tools -- at</c>) : <c>0000 3200 6400 9600 0000 9600</c>.
    ///
    /// <para>
    /// Six termes, dans l'ordre de <see cref="s_MeleeProbes_00562cf0"/>, comme
    /// <see cref="s_GrizzlyProbes_00552960"/>. Le cinquieme — le decalage vertical — est nul : le
    /// dragon mord a plat. Le sixieme vaut <c>0x96</c> la ou toute la famille terrestre pose
    /// <c>0x50</c> : <b>c'est la plus large gueule du depot</b>.
    /// </para>
    /// <para>
    /// <b>Borne des deux cotes.</b> Ce qui precede est le gabarit du bond du Grizzly de la mine
    /// (<c>0x005528cc</c>, douze octets) ; ce qui suit a <c>0x005528e4</c> reprend la forme d'une
    /// sonde (<c>0000 0000 6400 9600 0000 5000</c>) et appartient a une autre routine. Le dragon
    /// n'a donc qu'<b>une</b> rangee, et il n'en lit jamais d'autre.
    /// </para>
    /// </summary>
    private static readonly short[] s_YoungDragonProbe_005528D8 =
    [
        0x0000, 0x0032, 0x0064, 0x0096, 0x0000, 0x0096,
    ];

    private static short* s_YoungDragonProbeStorage;

    /// <summary>Le gabarit de <see cref="s_YoungDragonProbe_005528D8"/> en memoire native, puisque
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> le recoit par pointeur.</summary>
    private static short* YoungDragonProbe()
    {
        if (s_YoungDragonProbeStorage == null)
        {
            s_YoungDragonProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_YoungDragonProbe_005528D8.Length, sizeof(short));

            for (int i = 0; i < s_YoungDragonProbe_005528D8.Length; i++)
            {
                s_YoungDragonProbeStorage[i] = s_YoungDragonProbe_005528D8[i];
            }
        }

        return s_YoungDragonProbeStorage;
    }

    /// <summary>
    /// Ghidra: FUN_00535770 @ 0x00535770 — <b>une rangee du tableau d'attaches du bloc de
    /// modele</b>. Le corps original calcule
    /// <c>modeles + 0x40 + (rangee + espece[4] * 0xc) * 8</c>, ce qui est exactement
    /// <c>modele + 0x40 + rangee * 8</c> : le bloc de modele fait <c>0x60</c> octets et
    /// <c>0xc * 8</c> vaut <c>0x60</c>. Douze rangees de huit octets par modele, dont les trois
    /// premiers mots sont un <b>point d'attache</b> en coordonnees de modele.
    ///
    /// <para>
    /// <b>Aucun appelant dans <c>Blaze.c</c></b> : la fonction n'est atteinte que par des corps que
    /// Ghidra n'a pas decompiles. Le Jeune dragon est le premier a la reclamer, et elle reste donc
    /// ici, aupres de lui, jusqu'a ce qu'un second appelant etablisse son rang (regle 4 de
    /// <c>ARCHITECTURE.md</c>).
    /// </para>
    /// </summary>
    private static short* ModelAnchorRow_00535770(byte* e, int row)
    {
        ResolveEntityBlocks(e, out _, out _, out byte* model);
        return (short*)(model + 0x40 + row * 8);
    }

    /// <summary>
    /// Ghidra: <c>0x004944e0</c> — <b>l'intelligence du Jeune dragon</b>, sous-type 13 de la table
    /// installee <c>0x0056ad48</c> dans la quatrieme sous-zone de la mine.
    ///
    /// <para>
    /// Les trois temps de la famille terrestre — decider, appliquer, replanifier — mais la
    /// <b>hardiesse</b> que <see cref="ReadSceneEntitySituation_00535F20"/> calcule n'est
    /// <b>jamais relue</b>, pas plus que le bloc d'espece ni le bloc de modele que
    /// <see cref="ResolveEntityBlocks"/> resout au passage. Seul le bloc de statistiques sert, et
    /// seulement pour <see cref="InterruptSceneEntity_004E3770"/>.
    /// </para>
    /// <para>
    /// <b>Quatre postures tenues</b>, table de sauts a <c>0x00494CF4</c> (<c>0x0049486b</c>,
    /// <c>0x004948a2</c>, <c>0x00494901</c>, <c>0x00494973</c>) : 0 attendre, 1 se tourner,
    /// 2 s'approcher, 3 marcher — et <b>trois gestes</b>, aiguilles par la meme <c>+0x42</c>
    /// pendant que le bit 1 est pose : 0 la morsure, 1 le premier souffle, 2 le second.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004944E0(byte* record)
    {
        byte* e = record;

        // Ghidra: FUN_00538980 — l'espece et le modele sont resolus et jamais consultes.
        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        DecideYoungDragonAction_004944E0(e, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_00494a30 — sa gravite, reposee a chaque image et sur toutes les sorties.
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanYoungDragonAction_004944E0(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00494538..0x00494a2f</c> — <b>le premier temps</b> du Jeune dragon : mener le
    /// geste en cours, ou tenir la posture.
    ///
    /// <para>
    /// <b>Les deux souffles ne touchent personne.</b> Ni la posture 1 ni la posture 2 n'appelle
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> : elles font naitre une entite et la laissent
    /// vivre sa vie. Ce qu'elles font, c'est preparer les champs que
    /// <see cref="SpawnEffectEntity"/> recopie — <c>+0x34</c>, <c>+0x44/+0x48/+0x4c</c>,
    /// <c>+0x5c</c>, <c>+0x9a</c> — puis les remettre comme ils etaient.
    /// </para>
    /// </summary>
    private static void DecideYoungDragonAction_004944E0(
        byte* e, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ la posture tenue
        if ((flags & 2) == 0)
        {
            HoldYoungDragonStance_00494843(e, distance, heading, flags);
            return;
        }

        // Le trait 0x10 coupe court : l'espece ne poursuit pas son geste.
        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        byte posture = e[0x42];

        // ------------------------------------------------------------ 0 : LA MORSURE
        if (posture == 0)
        {
            BiteGesture_0049471D(e, heading);
            return;
        }

        // ------------------------------------------------------------ 1 : LE PREMIER SOUFFLE
        if (posture == 1)
        {
            short phase1 = *(short*)(e + 0x10);
            if (phase1 == 0) { CloseYoungDragonGesture_00494703(e, flags); return; }
            if (phase1 != 1) { *(int*)(e + 0x5c) = 0; return; }

            // Ghidra: le `cmp [esi+1Ah],dx` compare a EDX, laisse a 0x10 par le masque de trait
            // teste plus haut — donc bien a l'image 16, mais le compilateur ne le reecrit pas.
            if (*(short*)(e + 0x1a) == 0x10)
            {
                Aud.PlayPositionalSound(0x41, 0x200, 0x1800, (int*)(e + 0x1c));
            }

            // Ghidra: LAB_004946a3 — la naissance n'a lieu QU'A LA FIN de l'animation.
            if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

            int savedX = *(int*)(e + 0x44);
            int savedY = *(int*)(e + 0x48);

            // Un tirage a pile ou face dans +0x44, et 0x3a98 dans +0x48 : les deux champs que la
            // naissance recopie et que le nouveau-ne lira comme sa mise en place.
            *(int*)(e + 0x44) = CrtRand() & 1;
            *(int*)(e + 0x48) = 0x3a98;

            byte* block1 = *(byte**)(s_Unknown_005e97cc + 0xcc);
            SpawnEffectEntity(e, (byte)((*(byte**)block1)[0x0e] + 0xc0), 0, 2);

            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x44) = savedX;
            *(int*)(e + 0x48) = savedY;
            e[0x43] = 0x0a;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (posture != 2) { *(int*)(e + 0x5c) = 0; return; }

        // ------------------------------------------------------------ 2 : LE SECOND SOUFFLE
        short phase = *(short*)(e + 0x10);
        if (phase == 0) { CloseYoungDragonGesture_00494703(e, flags); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }

        // Ghidra: 0x00494595 — ici le drapeau de fin d'animation ne ferme pas le geste, il arme
        // seulement le rebours : la posture 2 souffle A L'IMAGE 28, pas a la fin.
        if ((flags & 0x2000000) != 0)
        {
            *(short*)(e + 0x10) = (short)(phase - 1);
            e[0x43] = 0x0a;
        }

        // Ghidra: LAB_004945a5
        if (*(short*)(e + 0x1a) == 0x0c)
        {
            Aud.PlayPositionalSound(2, 0x32a, 0x1800, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x1a) != 0x1c) { *(int*)(e + 0x5c) = 0; return; }

        // Ghidra: 0x004945cf — LE SOUFFLE. Quatre champs sauves, six poses, la naissance, puis tout
        // est remis : le dragon lui-meme ne bouge pas d'un pouce.
        short savedGravity = *(short*)(e + 0x9a);
        short savedPitch = *(short*)(e + 0x34);
        int savedThrust = *(int*)(e + 0x5c);

        *(short*)(e + 0x9a) = 0;          // le souffle ne tombe pas
        *(short*)(e + 0x34) = 0x0f80;
        *(int*)(e + 0x5c) = 0x32;         // et il part avec sa propre vitesse

        // La gueule : la rangee 0 du tableau d'attaches du modele, en unites de 1/4096.
        short* anchor = ModelAnchorRow_00535770(e, 0);
        *(int*)(e + 0x44) = anchor[0] << 0xc;
        *(int*)(e + 0x48) = anchor[1] << 0xc;
        *(int*)(e + 0x4c) = anchor[2] << 0xc;

        byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
        SpawnEffectEntity(e, (byte)((*(byte**)block)[0x0d] + 0xc0), 0, 2);

        *(short*)(e + 0x9a) = savedGravity;
        *(short*)(e + 0x34) = savedPitch;

        Aud.PlayPositionalSound(0x20, 0x653, 0x1800, (int*)(e + 0x1c));

        // Ghidra: 0x0049465d puis 0x0049466a — la poussee est rendue, et immediatement reecrasee
        // par le zero de la sortie commune. Les deux ecritures sont dans le binaire ; la premiere
        // ne survit pas a la seconde.
        *(int*)(e + 0x5c) = savedThrust;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: LAB_00494703 — <b>le rebours de recuperation</b>, partage par les deux
    /// souffles : la phase 0 decompte <c>+0x43</c> et ferme le geste a zero.</summary>
    private static void CloseYoungDragonGesture_00494703(byte* e, uint flags)
    {
        if (--e[0x43] != 0) { *(int*)(e + 0x5c) = 0; return; }

        *(int*)(e + 0x5c) = 0;
        *(uint*)e = flags & 0xfffffffd;
    }

    /// <summary>
    /// Ghidra: LAB_0049471d — <b>la morsure</b>, posture 0 pendant un geste.
    ///
    /// <para>
    /// Trois temps sur l'animation 3 : elle <b>se replace</b> tant qu'elle n'a pas atteint la
    /// cinquieme image (cone <c>0x40</c>), elle <b>s'elance</b> a la cinquieme (poussee
    /// <c>0x28000</c>, la plus forte du depot, avec le rugissement 5), et elle <b>porte a partir de
    /// la sixieme</b> — sans borne haute et sans verrou, donc une victime restee dessous encaisse a
    /// chaque image jusqu'a la fin de l'animation.
    /// </para>
    /// <para>
    /// <b>Un coup sur dix empoisonne</b> : <c>+0x07 = 7</c> puis l'effet 12
    /// (<see cref="ApplyStatusEffect_0053B810"/>), le meme couple que le Crotale.
    /// </para>
    /// </summary>
    private static void BiteGesture_0049471D(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 5) TurnSceneEntityTowardTarget(e, 0x40, heading);

        if (frame == 5)
        {
            *(int*)(e + 0x5c) = 0x28000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(5, 0x32a, 0x1800, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_004947b5
        if (*(short*)(e + 0x1a) >= 6)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), YoungDragonProbe(), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)victim |= 0x40;
                *(short*)(victim + 0x96) = 6;      // six images de clignotement, pas seize
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);

                if (CrtRand() % 100 < 0x0a)
                {
                    e[0x07] = 7;
                    ApplyStatusEffect_0053B810((uint*)e, (uint*)victim, 0);
                }

                // Ghidra: LAB_00494816
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }
        }

        // Ghidra: LAB_0049482e
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: LAB_00494843 — <b>la posture tenue</b>, quand aucun geste n'est en cours.
    ///
    /// <para>
    /// Les quatre postures de la famille terrestre, aux poussees pres : l'approche pousse
    /// <c>0x12000</c> et la marche <c>0xe000</c>, deux constantes la ou le Grizzly calcule
    /// <c>(hardiesse + 0xc0) &lt;&lt; 7</c>. <b>Le dragon avance toujours au meme pas.</b>
    /// </para>
    /// <para>
    /// <b>Et il a un bruit de pas</b> : <see cref="StepSound_00494A02"/>, aux images 10 et 23 de
    /// l'animation 1, pose a la sortie commune des quatre postures.
    /// </para>
    /// </summary>
    private static void HoldYoungDragonStance_00494843(byte* e, int distance, int heading, uint flags)
    {
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte stance = e[0x42];
        if (stance > 3) { StepSound_00494A02(e); return; }

        switch (stance)
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
                    break;
                }

                if (e[0x43] != 0)
                {
                    e[0x43]--;
                    *(int*)(e + 0x5c) = 0;
                    break;
                }

                *(int*)(e + 0x5c) = 0;      // Ghidra: LAB_004948f9
                break;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 2 : s'approcher
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
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x12000;
                break;

            // ---------------------------------------------------------- 3 : marcher
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                *(int*)(e + 0x5c) = 0xe000;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                break;
            }
        }

        StepSound_00494A02(e);
    }

    /// <summary>Ghidra: LAB_00494a02 — <b>le pas</b> : aux images 10 et 23 de l'animation 1, et
    /// seulement d'elle. Aucune des routines terrestres deja transcrites n'en pose.</summary>
    private static void StepSound_00494A02(byte* e)
    {
        if (*(ushort*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame != 0x0a && frame != 0x17) return;

        Aud.PlayPositionalSound(0x35, 0x200, 0x1800, (int*)(e + 0x1c));
    }

    /// <summary>
    /// Ghidra: <c>0x00494a8a..0x00494cf0</c> — <b>le troisieme temps</b> : quelle posture prendre.
    /// La distance et le cap sont recalcules a plat, comme chez toute la famille.
    ///
    /// <para>
    /// <b>Sous 0x12c</b>, un tirage sur cent : moins de 0x32 il <b>mord</b> si la proie tient dans
    /// un cone de <c>0x200</c> (sinon il hesite), moins de 0x4b il tient sa distance a <c>0x64</c>,
    /// au-dela il marche a l'oppose.
    /// </para>
    /// <para>
    /// <b>La branche 0x4b..0x5a ne se distingue de la suivante par rien.</b> Elle teste le cone,
    /// puis pose exactement la meme chose que l'echec du test : posture 3, meme rebours, meme cap.
    /// Le compilateur a emis deux fois le meme bloc et le test qui les separe est sans effet
    /// observable — transcrit tel quel plutot que simplifie, parce que la fidelite prime.
    /// </para>
    /// <para>
    /// <b>Au-dela de 0x12c</b>, c'est la seule plage ou il SOUFFLE : moins de 0x2d, et la proie dans
    /// un cone de <c>0x400</c>, le tirage lui-meme choisit lequel — impair le premier souffle
    /// (animation 6), pair le second (animation 4). Ni l'un ni l'autre ne pose de rebours : c'est
    /// leur propre phase 0 qui le fera.
    /// </para>
    /// </summary>
    private static void ReplanYoungDragonAction_004944E0(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;

        // ================================================================ le corps a corps
        if (distance < 0x12c)
        {
            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    HesitateSceneEntity(e);       // Ghidra: LAB_00494c58
                    return;
                }

                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x4b)
            {
                // Ghidra: LAB_00494b2b — la distance tenue est 0x64, laissee dans EDI par le
                // modulo du tirage.
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = YoungDragonDelay();
                return;
            }

            if (roll < 0x5a && ((heading + 0x100) & 0xfff) <= 0x200)
            {
                WalkYoungDragonAway_00494B6F(e, heading);   // Ghidra: LAB_00494b6f
                return;
            }

            WalkYoungDragonAway_00494B6F(e, heading);       // Ghidra: LAB_00494bae — le meme bloc
            return;
        }

        // ================================================================ l'approche et le souffle
        if (roll < 0x2d)
        {
            if (((heading + 0x200) & 0xfff) > 0x400)
            {
                HesitateSceneEntity(e);           // Ghidra: LAB_00494c58
                return;
            }

            uint state = *(uint*)e;

            if ((roll & 1) != 0)
            {
                *(int*)(e + 0x18) = 6;            // Ghidra: 0x00494c0c — le premier souffle
                e[0x42] = 1;
            }
            else
            {
                *(int*)(e + 0x18) = 4;            // Ghidra: LAB_00494c32 — le second
                e[0x42] = 2;
            }

            *(short*)(e + 0x10) = 1;
            *(uint*)e = (state & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x46)
        {
            // Ghidra: LAB_00494c68 — s'ecarter, cap tire a moins de 0x600 de la proie.
            e[0x42] = 3;
            e[0x43] = YoungDragonDelay();
            *(ushort*)(e + 0x10) =
                (ushort)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) - 0x600);
            return;
        }

        if (roll < 0x5a)
        {
            // Ghidra: LAB_00494cac
            e[0x42] = 2;
            *(ushort*)(e + 0x10) = 0x64;
            e[0x43] = YoungDragonDelay();
            return;
        }

        // Ghidra: LAB_00494cd3 — se reposer.
        e[0x42] = 0;
        e[0x43] = YoungDragonDelay();
    }

    /// <summary>Ghidra: le motif <c>rand() % 10 + 10</c>, emis six fois — <b>la duree d'une
    /// posture</b> du Jeune dragon, de 10 a 19 images. Il ne module PAS sa timidite, la ou toute la
    /// famille terrestre pose <c>rand() % 10 + (timide &gt;&gt; 1) + 0x14</c>
    /// (<see cref="SceneEntityDelay"/>) : le dragon replanifie deux a trois fois plus vite que ses
    /// voisins, quelle que soit la partie.</summary>
    private static byte YoungDragonDelay() => (byte)(CrtRand() % 10 + 10);

    /// <summary>Ghidra: LAB_00494b6f et LAB_00494bae — <b>marcher a l'oppose</b> : posture 3, cap
    /// tire dans une fourchette de <c>0xc00</c> autour de la proie, plus un demi-tour de
    /// <c>0x200</c>. Les deux sites sont le meme bloc, emis deux fois.</summary>
    private static void WalkYoungDragonAway_00494B6F(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = YoungDragonDelay();
        *(ushort*)(e + 0x10) = (ushort)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) + 0x200);
    }
}
