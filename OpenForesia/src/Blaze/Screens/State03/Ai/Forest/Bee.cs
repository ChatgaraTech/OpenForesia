// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ABEILLE (et l'Abeille tueuse) — 0x0049B220, sous-type 3 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// Le bloc C porte une AUTRE « Abeille tueuse » sous un gestionnaire different (0x004A3C30,
// `KillerBee.cs`) : le nom d'espece ne determine pas le comportement, le bloc et le
// sous-type le font.
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
    // 0x0049B220 — L'ABEILLE (et l'Abeille tueuse), sous-type 3 du bloc A de la foret. Transcrite
    // le 2026-08-29 au desassembleur (Legacy/Raw/Blaze/FUN_0049B220.asm, 1 542 octets de corps,
    // absente de Blaze.c).
    //
    // LE TRI A TROIS LECTURES L'A CLASSEE EN UNE MINUTE : elle appelle FUN_0049A8E0, l'integration
    // VOLANTE, et FUN_004A1CF0, l'interruption volante — c'est donc une sœur de la Coccinelle, pas
    // des quatre terrestres. Elle a une table de sauts (0x0049b828, quatre entrees) et sa sonde est
    // construite sur la pile.
    //
    // MAIS ELLE EST BATIE COMME UNE TERRESTRE. Son squelette est celui de FUN_0049A980 — quatre
    // postures, trois plages de replanification a 0xfa et 0x1f4, le meme cone hardiesse * 2 + 0x80,
    // la meme distance tenue 0x64, le meme SceneEntityDelay. La Coccinelle, elle, n'a que deux
    // plages, un delai qui lui est propre et un engagement sans animation. AUTREMENT DIT : « volante
    // ou terrestre » ne decide que de trois appels et du bloc de vol ; la charpente, elle, se choisit
    // independamment. C'est la premiere routine ou les deux axes se croisent.
    //
    // CE QUI LUI EST PROPRE : LA POSTURE 3 EST UN TRAIT, PAS UNE MARCHE.
    //
    //   +0x43 y sert de machine a deux temps au lieu de compte a rebours. A 2 l'abeille verse d'un
    //   coup (timidite + 0x120) << 8 dans ses vitesses, le long du cap range en +0x10 ; a 1 elle
    //   attend que cette vitesse soit retombee sous hardiesse/8 + 1 avant de rendre la main. Et a
    //   chaque image de cette posture elle ORIENTE son corps plein cap sur la cible, sans cone.
    //   C'est le vol d'insecte : des traits rectilignes separes par des arrets face a vous.
    //
    //   La poussee du trait se prend sur la TIMIDITE, pas sur la hardiesse — une abeille craintive
    //   file plus vite. Aucune autre routine ecrite ne fait cela ; verifie deux fois au
    //   desassembleur (edi vaut 0x40 - hardiesse depuis 0x0049b29a et n'est pas reecrit sur ce
    //   chemin) avant d'etre transcrit tel quel.
    //
    // ET SON COUP TIENT EN UNE IMAGE : la seizieme du geste, exactement. Ni les quatre images du
    // Crabe et du Grizzly, ni le balayage continu de la Coccinelle — un dard, une fois.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0049b220</c> — <b>l'intelligence de l'Abeille</b>, sous-type 3 de la table
    /// installee <c>0x0056ad48</c> en foret. Huitieme gestionnaire d'entite de scene, septieme
    /// routine d'intelligence, et <b>deuxieme bete volante</b>.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x0049b828</c> (<c>0x0049b36b</c>,
    /// <c>0x0049b3ac</c>, <c>0x0049b420</c>, <c>0x0049b4a8</c>) : 0 attendre, 1 se tourner,
    /// 2 s'approcher, <b>3 filer en ligne droite</b>. Les trois premieres sont celles des
    /// terrestres, a ceci pres que la poussee y est <i>amortie</i> ou <i>montee par seiziemes</i>
    /// au lieu d'etre remise a zero — elle vole, elle ne pile pas.
    /// </para>
    /// <para>
    /// Sa sonde est <c>{0, 0x1e, 0x32, 0x64, 0, 0x50}</c>, exactement celle de la Fourmi geante
    /// <see cref="TickSceneEntityAi_0049A980"/> — et notamment <b>sans</b> le decalage vertical
    /// <c>-0x100</c> de la Coccinelle. Elle pique a hauteur d'homme.
    /// </para>
    /// <para>
    /// <b>Elle ne pose pas le bit 0 de ses drapeaux</b> a l'entree, la ou la Coccinelle le fait a
    /// chaque image. Son garde de posture n'en tient pas compte non plus : il teste <c>0x10</c> seul
    /// quand les terrestres testent <c>0x11</c>. Les deux choix vont ensemble.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049B220(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x50;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideDartingEntityAction_0049B220(e, probe, boldness, distance, heading, speed, timid, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // La meme queue que la Coccinelle, bloc de vol compris : c'est le meme corps, emis deux
        // fois par le compilateur de 1998.
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        BeatWings_0049A68F(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanDartingEntityAction_0049B220(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049b2a8..0x0049b549</c> — <b>le premier temps</b> de l'Abeille.
    ///
    /// <para>
    /// <b>Un seul geste, et un seul coup.</b> Contrairement au Crabe et au Grizzly, la posture n'est
    /// pas relue pendant le geste : il n'y a qu'une forme de frappe, et elle porte a la
    /// <b>seizieme</b> image, une seule fois. Aucun verrou n'est necessaire — l'egalite stricte sur
    /// <c>+0x1a</c> en tient lieu.
    /// </para>
    /// </summary>
    private static void DecideDartingEntityAction_0049B220(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, int timid, uint traits)
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

            // Ghidra: LAB_0049b2d5 — le dard, a la seizieme image et a elle seule. Le 0x10 compare
            // ici est le meme registre que le clignotement pose plus bas : le compilateur a garde
            // la constante dans ebx.
            if (*(short*)(e + 0x1a) == 0x10)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                    *(uint*)victim |= 0x40;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }
            }

            // Ghidra: LAB_0049b329 — la fin du geste libere aussi la replanification, puisque c'est
            // +0x43 qui la retient depuis l'engagement.
            flags = *(uint*)e;
            if ((flags & 0x2000000) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
            }

            return;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_0049b343 — 0x10 seul, sans le bit 0 que les terrestres excluent aussi.
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte stance = e[0x42];
        if (stance > 3) return;

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
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                DampFlyingEntityPush(e);
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
                DampFlyingEntityPush(e);
                return;

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
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading);
                RampFlyingEntityPush(e, (boldness + 0x200) << 7);
                return;

            // ---------------------------------------------------------- 3 : filer
            default:
            {
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // Ghidra: LAB_0049b4bd — +0x43 n'est pas ici un compte a rebours mais une machine a
                // deux temps : 2 lance le trait, 1 attend qu'il soit retombe. Toute autre valeur ne
                // fait que l'orientation ci-dessous.
                byte phase = e[0x43];

                if (phase == 2)
                {
                    // La poussee se prend sur la TIMIDITE : une abeille craintive file plus vite.
                    // Le decalage est de huit bits et non de sept comme partout ailleurs.
                    int thrust = (timid + 0x120) << 8;
                    *(int*)(e + 0x5c) = thrust;

                    int course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * thrust) >> 0xc;
                    *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    e[0x43]--;
                }
                else if (phase == 1)
                {
                    // Ghidra: LAB_0049b513 — le trait est fini quand la vitesse horizontale est
                    // retombee sous hardiesse/8 + 1. Une bete hardie repart donc plus tot.
                    int vx = *(int*)(e + 0x50) >> 0xc;
                    int vz = *(int*)(e + 0x58) >> 0xc;

                    if ((int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz)) <= (boldness >> 3) + 1)
                    {
                        e[0x43] = 0;
                    }
                }

                // Ghidra: LAB_0049b541 — plein cap sur la cible, sans cone et a chaque image. C'est
                // ce qui donne l'arret face au joueur entre deux traits.
                *(ushort*)(e + 0x32) += (ushort)heading;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049b613..0x0049b81d</c> — <b>le troisieme temps</b> de l'Abeille.
    ///
    /// <para>
    /// <b>C'est la replanification des terrestres</b>, plages comprises : le corps a corps sous
    /// <c>0xfa</c>, l'approche sous <c>0x1f4</c>, l'errance au-dela, et le meme
    /// <see cref="SceneEntityDelay"/> pour le repos et la tenue de distance. La Coccinelle n'avait
    /// ni ces bornes ni ce delai.
    /// </para>
    /// <para>
    /// <b>Trois differences, toutes du meme cote.</b> Son engagement pose <c>+0x43 = 1</c> et laisse
    /// la posture telle quelle — le geste n'a pas de forme a choisir, et ce 1 sert seulement a
    /// retenir la replanification jusqu'a la fin de l'animation. Ses trois sorties d'errance posent
    /// <c>+0x43 = 2</c>, qui arme le trait au lieu de compter des images. Et elle n'a ni bond, ni
    /// recul, ni esquive.
    /// </para>
    /// </summary>
    private static void ReplanDartingEntityAction_0049B220(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    // Ghidra: 0x0049b6b7 — le dard. +0x42 n'est pas touche : il n'y a qu'un geste.
                    uint state = *(uint*)e;
                    *(int*)(e + 0x18) = 3;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (state & 0xfeffffff) | 2;
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            DartSceneEntity(e, boldness, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x46)
            {
                DartSceneEntity(e, boldness, heading, 0);
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

        // ================================================================ l'errance
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            RestSceneEntity(e, timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_0049b80d — trop loin pour que la cible compte : le cap est entierement tire,
        // mais le trait s'arme quand meme.
        e[0x42] = 3;
        e[0x43] = 2;
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>
    /// Ghidra: LAB_0049b6e7 et LAB_0049b756 — <b>armer un trait</b> : posture 3, la machine a deux
    /// temps mise a 2, et un cap tire dans une fourchette large de <c>(hardiesse + 0x80) * 16</c>
    /// centree sur la cible.
    ///
    /// <para>C'est <see cref="WanderSceneEntity"/> avec un compte a rebours fixe a la place du
    /// delai tire : l'Abeille ne tient pas un cap pendant vingt images, elle file une fois et
    /// s'arrete. <paramref name="bias"/> vaut <c>0x800</c> au corps a corps — elle s'ecarte — et
    /// zero a l'approche.</para>
    /// </summary>
    private static void DartSceneEntity(byte* e, int boldness, int heading, int bias)
    {
        e[0x42] = 3;
        e[0x43] = 2;

        int spread = (boldness + 0x80) << 4;
        int offset = CrtRand() % spread - (spread >> 1);

        *(ushort*)(e + 0x10) = (ushort)(offset + heading + *(short*)(e + 0x32) + bias);
    }
}
