// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA FLEUR DU MAL — 0x004A0480, sous-type 11 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// La seconde routine de la charpente du Varan, et la plus grosse de la zone 3 (3 936
// octets). Elle ne choisit pas sa proie : elle la tire au sort.
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
    // 0x004A0480 — LA FLEUR DU MAL, sous-type 11 du bloc B de la foret (sous-zones 6 a 8).
    // Transcrite le 2026-09-02 au desassembleur (Legacy/Raw/Blaze/FUN_004A0480.asm, 3 936 octets,
    // absente de Blaze.c). La derniere des 136 de la zone 3, et la plus grosse.
    //
    // LA PREDICTION DE LA VEILLE ETAIT BONNE, ET ELLE A COUTE UNE COMMANDE. Le releve du Varan
    // notait : « si elle n'appelle pas FUN_00488160 ni FUN_004E3770, c'est une seconde routine de
    // la charpente du Varan, et les cinq feuilles ecrites aujourd'hui la couvrent deja ». Sa liste
    // d'appels donne FUN_004A1020, FUN_004C3810, FUN_004D5DE0 et FUN_004D5E60 — les quatre feuilles
    // propres au Varan — et aucune des quatre feuilles de la famille. C'est bien la meme charpente,
    // et rien n'a eu besoin d'etre ecrit pour elle : le heurt des murs, la pose au sol, le
    // chercheur de proie et l'assommement etaient deja la.
    //
    // LA CHARPENTE DU VARAN EST DONC UNE VRAIE FAMILLE, PAS UN CAS ISOLE. Deux membres, tous deux
    // du bloc B, tous deux sans feuille partagee, tous deux avec leur sonde batie sur la pile — et
    // c'est EXACTEMENT LA MEME SONDE, {0, 0x1e, 0x32, 0x64, 0, 0x64}, mot pour mot.
    //
    // CE QUI LA SEPARE DU VARAN, ET C'EST PEU :
    //
    //   - ELLE NE CHOISIT PAS SA PROIE, ELLE LA TIRE AU SORT. Meme appel a FUN_004A1020 a la
    //     naissance, mais avec le mode 0 — et le mode 0 n'a pas de case dans le chercheur. Il ne
    //     reste donc que la piece a 60 % qui remplace le mode par 0xffff, « n'importe qui de
    //     vivant » : une fois sur trois environ elle change de cible au hasard, sinon elle garde la
    //     sienne. Le Varan, lui, demande la classe 3.
    //
    //   - ELLE N'A NI SORT, NI PRISE, NI POISON. Douze cases de table de sauts au lieu de treize,
    //     dont cinq vides (4, 5, 8, 9, 10) : il ne lui reste que attendre, se tourner, approcher,
    //     louvoyer, bondir, marcher droit, et son geste. Pas de CastSpell, pas de projection, pas
    //     d'effet de piege — trois choses que le Varan fait et qu'elle ignore.
    //
    //   - ET SON GESTE SEME DES BRAISES. La posture 0x0B ne pose jamais +0x12, donc son corps
    //     tourne a CHAQUE image tant qu'elle la tient ; une image sur trois elle fait naitre le
    //     sous-type 0xEF a sa propre position, par FUN_0047AC40 — la table 0x005f2d40. Ce
    //     sous-type se resout en 0x004101D0, la BRAISE QUI DERIVE, deja transcrite : une entite
    //     qui part tout droit, grossit de trois paliers et s'eteint. Une fleur qui crache des
    //     etincelles.
    //
    //   - LE DESCRIPTEUR D'ATLAS QU'ELLE PASSE EST NUL, ET CE N'EST PAS UN OUBLI. FUN_0047AC40
    //     range son troisieme argument dans +0x3c de l'entite qui nait ; la Fleur y met zero parce
    //     que la braise s'en donne un elle-meme a sa premiere image. Verifie dans le corps de
    //     0x004101D0, qui ecrit +0x3c avant toute lecture.
    //
    //   - SA POSTURE 7 N'EST JOIGNABLE QUE PAR UN MUR. Le Varan l'arme aussi depuis sa bande du
    //     milieu ; la Fleur, jamais. Le seul site qui pose 7 est le heurt. Sa bande du milieu a
    //     d'ailleurs, a la place, une branche qui NE FAIT RIEN — cible en face, un tirage sur cinq :
    //     ni posture ni rebours, donc on retire a l'image suivante. Le meme temps mort que chez le
    //     Wyrm, decouvert le meme jour dans une routine sans rapport.
    //
    //   - ET SES DEUX DERNIERES BANDES NE SE RECOUVRENT PAS. Celle du milieu s'arrete a 0x258, ou
    //     commence celle du lointain — exactement adjacentes. Le Varan, lui, laisse la sienne
    //     courir jusqu'a 0x2bc et se fait ecraser sa posture entre 0x258 et 0x2bb. Deux routines de
    //     la meme charpente, une seule des deux avec le defaut : le recouvrement du Varan est donc
    //     bien une SINGULARITE de son corps, pas un trait de famille.
    //
    //   - MENUES DIFFERENCES DE REGLAGE : ses pas de virage prennent le socle 0x28 la ou le Varan
    //     prend 0x32 ou 0x80, sa posture 3 tourne de hardiesse * 8 + 0x28 (le pas le plus vif des
    //     deux corps), sa posture 7 pousse a 0x7000 et non 0x5000, et son masque de contact est
    //     0x2000af82 — un bit de plus que celui du Varan.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004a0480</c> — <b>l'intelligence de la Fleur du mal</b>, sous-type 11 de la
    /// table installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret. <b>Seconde routine
    /// de la charpente du Varan</b> : aucune des quatre feuilles partagees, les quatre feuilles
    /// propres, et la meme sonde batie sur la pile.
    ///
    /// <para>
    /// Douze cases dans sa table de sauts (<c>0x004A0FE8</c>), dont <b>cinq vides</b> — 4, 5, 8, 9
    /// et 10 retombent sur la sortie. Sept postures reelles : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 louvoyer, 6 bondir, 7 marcher droit (le heurt seul y mene), <c>0x0b</c> le
    /// geste.
    /// </para>
    /// <para>
    /// <b>Elle n'a pas la machine a poser le bit <c>0x10</c> sur la posture 5</b> que le Varan
    /// porte : sa posture 5 est vide et rien de plus.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A0480(byte* record)
    {
        byte* e = record;

        // Ghidra: 0x004a04a1 — la naissance appelle le chercheur de proie avec le MODE 0, qui n'a
        // pas de case : seule la piece a 60 % qui bascule sur 0xffff peut changer la cible.
        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 0, 0);
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        // Le bloc de modele est resolu et jamais lu : son geste porte a pleine attaque.
        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004a0594 — la distance est RECALCULEE a trois dimensions, comme chez le Varan
        // et dans le meme sens, de l'entite vers la cible.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideDarkFlowerAction_004A0480(e, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000af82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanDarkFlowerAction_004A0480(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a05e2..0x004a0a44</c> — <b>le premier temps</b> de la Fleur du mal.
    ///
    /// <para>
    /// <b>Le prelude de contact ne regarde pas la posture</b>, contrairement a celui du Varan : le
    /// bit de geste et l'image <c>0x10</c> suffisent. Comme la posture <c>0x0b</c> est la seule qui
    /// pose ce bit, cela revient au meme — mais c'est ecrit plus court, et il faut le lire tel
    /// quel.
    /// </para>
    /// <para>
    /// <b>Et la case de la posture s'execute ensuite dans tous les cas</b>, geste ou non : c'est le
    /// trait de charpente que le Varan a introduit.
    /// </para>
    /// </summary>
    private static void DecideDarkFlowerAction_004A0480(
        byte* e, int boldness, int distance, int heading, int speed)
    {
        // Ghidra: 0x004a05e2..0x004a0646 — FUN_004db5a0 recopie en ligne, au mot pres.
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        // ================================================================ le prelude de contact
        if ((*(uint*)e & 2) != 0 && *(short*)(e + 0x1a) == 0x10)
        {
            DarkFlowerStrike_004A0653(e);
        }

        // ================================================================ la posture
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                }

                // Aucun decompte ici, la ou le Varan fait descendre +0x43 a chaque image.
                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x28 : -0x28 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x28 : -0x28 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) +=
                        ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;
                    *(int*)(e + 0x24) +=
                        (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;

                    // Le pas de virage le plus vif des deux corps : hardiesse * 8 + 0x28.
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                            ? boldness * 8 + 0x28
                            : -0x28 - boldness * 8);
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;
                        *(ushort*)(e + 0x18) = 1;
                        *(ushort*)(e + 0x1a) = 0;

                        int thrust = (boldness + 0x100) << 6;
                        *(int*)(e + 0x5c) = thrust;
                        int step = (speed * thrust) >> 0xc;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(ushort*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) != 0) return;
                e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 7 : marcher droit
            case 7:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;

                // 0x7000, la ou le Varan pose 0x5000 sur la meme posture.
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                return;
            }

            // ---------------------------------------------------------- 0x0b : le geste
            case 0x0b:
                // +0x12 n'est JAMAIS pose : ce corps tourne a chaque image tant que la posture
                // tient.
                if (*(ushort*)(e + 0x12) != 0) return;

                *(int*)(e + 0x5c) = 0;

                if (CrtRand() % 3 == 1)                      // Ghidra: FUN_00547140
                {
                    // Le descripteur d'atlas est NUL : la braise 0x004101D0 s'en donne un
                    // elle-meme a sa premiere image.
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xef, 0, -1);
                }

                uint g = *(uint*)e;
                if ((g & 0x2000000) == 0) return;

                e[0x43] = 0;
                *(uint*)e = g & 0xfffffffd;
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a0653..0x004a06a7</c> — <b>le coup</b>, a la seizieme image du geste et
    /// seulement elle.
    ///
    /// <para>
    /// La sonde est batie sur la pile — <c>{0, 0x1e, 0x32, 0x64, 0, 0x64}</c>, <b>exactement celle
    /// du Varan</b>, mot pour mot — et le releve de <c>+0x1a</c> est refait apres le balayage,
    /// comme chez lui.
    /// </para>
    /// <para>
    /// <b>Elle porte a pleine attaque</b> : elle ne touche pas au <c>+0x30</c> du bloc de modele,
    /// contrairement a la prise du Varan (le quart) et a celles du Crabe, du Grizzly, du Geant, du
    /// Wyrm et du Behemoth (la moitie).
    /// </para>
    /// </summary>
    private static void DarkFlowerStrike_004A0653(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;
        if (*(short*)(e + 0x1a) != 0x10) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;

        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
    }

    /// <summary>
    /// Ghidra: <c>0x004a0c86..0x004a0fe7</c> — <b>le troisieme temps</b> de la Fleur du mal.
    ///
    /// <para>
    /// La meme forme a trois bandes que le Varan, aux constantes pres — <b>mais sans son
    /// recouvrement</b> : la bande du milieu s'arrete a <c>0x258</c>, exactement ou commence celle
    /// du lointain. Les deux corps partagent la charpente et pas le defaut, ce qui range celui du
    /// Varan du cote des singularites.
    /// </para>
    /// <para>
    /// <b>Sa bande du milieu porte en revanche une branche qui ne fait rien</b> : cible dans la
    /// moitie avant, un tirage sur cinq, et ni posture ni rebours ne bougent — donc on retire a
    /// l'image suivante. Le meme temps mort que chez le Wyrm.
    /// </para>
    /// <para>
    /// <b>Et la posture 7 n'y est jamais armee</b> : le seul site qui la pose est le heurt d'un mur.
    /// </para>
    /// </summary>
    private static void ReplanDarkFlowerAction_004A0480(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;                  // Ghidra: FUN_00547140

            if (roll < 0x28)
            {
                bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

                if (!inCone || distance >= CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    HesitateSceneEntity(e);
                }
                else
                {
                    // Ghidra: 0x004a0d34 — armer LE GESTE, animation 3.
                    uint flags = *(uint*)e;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
            }
            else if (roll < 0x46 && distance > boldness + 0x64)
            {
                // Ghidra: 0x004a0dc5 — le bond, et la seule sortie du corps a corps qui exige une
                // distance MINIMALE : il faut du recul pour sauter.
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();              // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
        }

        // ================================================================ l'approche
        if (distance >= 0xfa && distance < 0x258)
        {
            int roll = CrtRand() % 100;                  // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                // Ghidra: 0x004a0e50 — cible dans la moitie AVANT : rien du tout. Sinon elle
                // hesite. Le Varan arme ici sa posture 7 ; la Fleur ne l'arme jamais.
                if (((heading + 0x400) & 0xfff) >= 0x800) HesitateSceneEntity(e);
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();              // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
            }
        }

        // ================================================================ le lointain
        if (distance < 0x258) return;

        int far = CrtRand() % 100;                       // Ghidra: FUN_00547140

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = SceneEntityDelay(timid);
                return;
            }

            // Ghidra: LAB_004a0f37 — le repos, avec le rebours court du Varan : timidite / 4, sans
            // les 0x14.
            e[0x42] = 0;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (byte)(timid >> 2));   // Ghidra: FUN_00547140
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);       // Ghidra: FUN_00547140
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();                      // Ghidra: FUN_00547140
    }
}
