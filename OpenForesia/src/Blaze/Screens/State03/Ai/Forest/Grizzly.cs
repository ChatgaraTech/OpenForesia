// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GRIZZLY — 0x0049B840, sous-type 4 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// Porte aussi 0x004A4270, le Grizzly du bloc C : MEME CODE, emis deux fois par le
// compilateur, a deux immediates pres (les adresses de ses gabarits de portee).
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
    // 0x0049B840 — LE GRIZZLY, sous-type 4 du bloc A de la foret. Transcrite le 2026-08-29 au
    // desassembleur (Legacy/Raw/Blaze/FUN_0049B840.asm, 2 141 octets de corps, absente de Blaze.c).
    //
    // C'EST LE JUMEAU DU CRABE, et le premier cas ou la recette « calquer sur une sœur de meme
    // milieu » a vraiment tenu de bout en bout : integration terrestre FUN_00488160, quatre postures
    // aiguillees par une table de sauts (0x0049c0a0), mesme decoupage des gestes sur +0x42, meme
    // machine a trois temps sur +0x10 pour le bond, et la meme division par deux de l'attaque du
    // bloc de modele autour de FUN_00539b80. Les quatre postures tenues sont identiques au mot pres.
    //
    // TROIS CHOSES LUI SONT PROPRES, ET ELLES CHANGENT LA SILHOUETTE :
    //
    //   - IL QUITTE LE SOL. La ou le crabe se rue a plat, le Grizzly pose une vitesse verticale de
    //     0x24000 et allume le bit 0 (« en l'air ») au moment de se lancer ; son geste ne se ferme
    //     donc PAS sur un compte a rebours mais a l'ATTERRISSAGE, quand ce bit retombe. Et il ecrit
    //     0x6000 dans +0x9a a chaque image, avant l'integration : c'est sa gravite propre, qu'aucune
    //     des six routines precedentes ne touche.
    //
    //   - SES GABARITS SONT DANS .data. Ses deux sondes ne sont pas construites sur la pile mais
    //     lues a 0x00552960 (la griffe) et 0x0055296c (le bond) — un cas que Comportement.md n'avait
    //     pas encore rencontre. La griffe porte `probe[4] = -0x100`, donc elle frappe EN HAUTEUR :
    //     une patte qui s'abat, la ou le bond frappe au ras du sol.
    //
    //   - SA GRIFFE SE CORRIGE EN COURS DE GESTE. Jusqu'a la neuvieme image elle continue de se
    //     tourner vers sa cible, a la neuvieme elle avance d'un a-coup, et elle balaie ensuite les
    //     images 10 a 13. Le crabe, lui, part droit et ne rectifie plus. C'est pour cela qu'un
    //     Grizzly rate beaucoup moins souvent.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552960</c> et <c>0x0055296c</c> — <b>les deux gabarits de portee du
    /// Grizzly</b>, releves dans <c>.data</c> (<c>OpenForesia.Tools -- at</c>). C'est la premiere
    /// routine d'intelligence dont les sondes soient <b>statiques</b> : les six precedentes les
    /// construisaient sur la pile a chaque image.
    ///
    /// <para>
    /// Les six termes sont ceux de <see cref="TestMeleeReach_00535A90"/>, dans l'ordre de
    /// <see cref="s_MeleeProbes_00562cf0"/>. Le cinquieme est un <b>decalage vertical</b> : la
    /// griffe le porte a <c>-0x100</c> — elle s'abat d'en haut — quand le bond reste a zero.
    /// </para>
    /// <para>
    /// Trois entrees de la meme forme les precedent a <c>0x00552930</c>, <c>0x0055293c</c> et
    /// <c>0x00552948</c> ; elles appartiennent a une routine non encore transcrite et ne sont pas
    /// reprises ici. La suite immediate (<c>0x00552978</c>) n'a plus la forme d'une sonde, donc la
    /// paire du Grizzly s'arrete bien a deux entrees.
    /// </para>
    /// <para>
    /// <b>Le binaire en porte une seconde copie</b>, a <c>0x005529e8</c> et <c>0x005529f4</c>, que
    /// lit le Grizzly du bloc C (<see cref="TickSceneEntityAi_004A4270"/>). Les vingt-quatre octets
    /// sont <b>byte a byte les memes</b>, verifies au releve : ces valeurs-ci valent donc pour les
    /// deux adresses, et il n'y a pas de second tableau a tenir.
    /// </para>
    /// </summary>
    private static readonly short[] s_GrizzlyProbes_00552960 =
    [
        0x0000, 0x001e, 0x0046, 0x0096, unchecked((short)0xff00), 0x0050,   // 0x00552960 — la griffe
        0x0000, 0x001e, 0x0032, 0x0096, 0x0000, 0x0050,                     // 0x0055296c — le bond
    ];

    private static short* s_GrizzlyProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_GrizzlyProbes_00552960"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* GrizzlyProbe(int row)
    {
        if (s_GrizzlyProbeStorage == null)
        {
            s_GrizzlyProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_GrizzlyProbes_00552960.Length, sizeof(short));

            for (int i = 0; i < s_GrizzlyProbes_00552960.Length; i++)
            {
                s_GrizzlyProbeStorage[i] = s_GrizzlyProbes_00552960[i];
            }
        }

        return s_GrizzlyProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x0049b840</c> — <b>l'intelligence du Grizzly</b>, sous-type 4 de la table
    /// installee <c>0x0056ad48</c> en foret. Septieme gestionnaire d'entite de scene, sixieme
    /// routine d'intelligence.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x0049c0a0</c> (<c>0x0049bed3</c>,
    /// <c>0x0049bf0c</c>, <c>0x0049bf6f</c>, <c>0x0049bff7</c>) : 0 attendre, 1 se tourner,
    /// 2 s'approcher, 3 marcher. Elles sont <b>identiques a celles du Crabe</b> — memes cones
    /// <c>hardiesse * 2 + 0x80</c>, meme distance tenue <c>0x64</c>, meme poussee
    /// <c>(hardiesse + 0xc0) &lt;&lt; 7</c>.
    /// </para>
    /// <para>
    /// <b>Le rappel au sol est ecrit ici, a chaque image</b> : <c>+0x9a = 0x6000</c> avant
    /// l'integration. C'est le terme que <see cref="IntegrateSceneEntityMotion_00488160"/> retranche
    /// de la vitesse verticale, donc la gravite que le Grizzly s'impose — et il l'impose meme quand
    /// sa vitesse est nulle, l'ecriture etant sur le chemin commun de sortie.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049B840(byte* record)
    {
        byte* e = record;

        // Comme le Crabe, il lui faut le bloc de MODELE : son bond ampute l'attaque de moitie.
        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecidePouncingEntityAction_0049B840(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_0049baa8 — sa gravite, reposee a chaque image et sur toutes les sorties.
        *(ushort*)(e + 0x9a) = 0x6000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanPouncingEntityAction_0049B840(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a4270</c> — <b>le Grizzly du bloc C</b>, sous-type 4 des sous-zones 5 et 9.
    /// Il n'a pas de corps a lui : c'est le <b>meme code</b> que
    /// <see cref="TickSceneEntityAi_0049B840"/>, emis une seconde fois par le compilateur.
    ///
    /// <para>
    /// <b>La preuve tient en deux comparaisons</b>, et il en fallait bien deux. Les 2 141 octets
    /// des deux corps donnent, apres normalisation des deplacements relatifs d'appel, <b>705 lignes
    /// d'assembleur identiques sur 705</b> — a l'exception de <b>deux immediates</b> :
    /// <c>push 0x0055296c</c> devient <c>push 0x005529f4</c>, et <c>push 0x00552960</c> devient
    /// <c>push 0x005529e8</c>. Ce sont les deux gabarits de portee. Le corps est donc le meme
    /// code lisant une <b>autre copie</b> de la meme table, et un simple <c>od</c> du code ne
    /// suffisait pas a conclure : il fallait aussi lire la donnee.
    /// </para>
    /// <para>
    /// <b>Et les deux copies sont byte a byte egales</b> : les vingt-quatre octets de
    /// <c>0x005529e8</c> sont ceux de <c>0x00552960</c>, releves l'un et l'autre par
    /// <c>OpenForesia.Tools -- at</c> (<c>0000 1e00 4600 9600 00ff 5000 0000 1e00 3200 9600 0000
    /// 5000</c>). Les deux Grizzlys se comportent donc <b>exactement</b> pareil ; la seconde
    /// adresse ne merite pas une seconde transcription, seulement ce renvoi.
    /// </para>
    /// <para>
    /// Releve garde sous <c>Legacy/Raw/Blaze/FUN_004A4270.asm</c>, pour que la comparaison reste
    /// refaisable sans le binaire.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A4270(byte* record) => TickSceneEntityAi_0049B840(record);

    /// <summary>
    /// Ghidra: <c>0x0049b8a5..0x0049c098</c> — <b>le premier temps</b> du Grizzly.
    ///
    /// <para>
    /// <b>Deux gestes, comme le Crabe</b>, et lus au meme endroit : posture 0 c'est la griffe,
    /// posture 1 c'est le bond. Mais les deux different de ceux du Crabe sur un point chacun — la
    /// griffe <i>corrige son cap</i> pendant les neuf premieres images du geste, et le bond
    /// <i>quitte le sol</i> au lieu de glisser.
    /// </para>
    /// </summary>
    private static void DecidePouncingEntityAction_0049B840(
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

            byte posture = e[0x42];

            // ------------------------------------------------------------ 0 : LA GRIFFE
            if (posture == 0)
            {
                // Ghidra: LAB_0049bd9b — tant que la patte n'est pas partie, la bete se replace.
                // Aucune des six routines precedentes ne rectifie son cap DANS un geste.
                short frame = *(short*)(e + 0x1a);
                if (frame < 9) TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x20, heading);

                // A la neuvieme image, l'a-coup en avant : la patte porte le corps avec elle.
                if (frame == 9)
                {
                    int lunge = (boldness + 0x300) << 7;
                    *(int*)(e + 0x5c) = lunge;

                    int course = *(short*)(e + 0x32);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * lunge) >> 0xc;
                    *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * lunge) >> 0xc;
                    *(int*)(e + 0x5c) = 0;
                }

                // Ghidra: LAB_0049be2d — quatre images de contact, sans verrou : une victime restee
                // dessous encaisse quatre fois, comme chez le Crabe. Degats PLEINS ici.
                short hitFrame = *(short*)(e + 0x1a);
                if (hitFrame >= 0x0a && hitFrame <= 0x0d)
                {
                    byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), GrizzlyProbe(0), 0);

                    if (*Mem.Camera(0x005de0b8) != 0)
                    {
                        *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                        *(uint*)victim |= 0x40;
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                    }
                }

                // Ghidra: LAB_0049be8e
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0)
                {
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = flags & 0xfffffffd;
                    return;
                }

                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049bf63
                return;
            }

            if (posture != 1) return;

            // ------------------------------------------------------------ 1 : LE BOND
            short phase = *(short*)(e + 0x10);

            // -------------------------------------------------------- 2 : s'accroupir et viser
            if (phase == 2)
            {
                int carried = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = carried - (carried >> 4);

                if (*(short*)(e + 0x1a) == 0x0e) *(short*)(e + 0x10) = (short)(phase - 1);

                TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x20, heading);
                return;
            }

            // -------------------------------------------------------- 1 : detendre
            if (phase == 1)
            {
                // Ghidra: LAB_0049b923 — plus fort que la ruee du Crabe (0x380), et VERTICAL en
                // plus : le bit 0 dit « en l'air », et c'est sa retombee qui fermera le geste.
                int thrust = (boldness + 0x440) << 7;
                *(int*)(e + 0x5c) = thrust;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x54) = 0x24000;
                *(uint*)e |= 1;

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            if (phase != 0) return;

            // -------------------------------------------------------- 0 : la chute et le contact
            // Ghidra: LAB_0049b981 — le geste ne se ferme QU'AU SOL. Le Crabe attendait un compte a
            // rebours ; ici c'est le bit 0 qui commande, donc un bond long tient plus longtemps.
            if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
            {
                *(uint*)e = flags & 0xffffffdd;                 // le geste ET le verrou de coup
            }

            // Ghidra: LAB_0049b990 — le verrou : un bond ne touche qu'une fois.
            if ((*(uint*)e & 0x20) != 0) return;

            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), GrizzlyProbe(1), 0);
            if (prey != null) *(uint*)e |= 0x20;

            // Ghidra: LAB_0049b9b9
            if (*Mem.Camera(0x005de0b8) == 0) return;

            *(ushort*)(prey + 0x96) = 0x10;
            *(uint*)prey |= 0x40;

            // LA MOITIE, exactement comme le Crabe : l'attaque du bloc de MODELE est divisee le
            // temps de la resolution, puis remise. Deux routines voisines la partagent — c'est donc
            // un idiome de la famille, pas une singularite.
            short power = *(short*)(model + 0x30);
            *(short*)(model + 0x30) = (short)(power >> 1);
            ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
            *(short*)(model + 0x30) = power;

            Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

            // Une victime que le coup vient d'emporter n'est pas projetee.
            if ((*(uint*)prey & 0x40000000) != 0) return;

            int impact = *(short*)(e + 0x32);
            *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
            *(int*)(prey + 0x58) = (int)Winsys3d.GetCosTableEntry(impact) * 30;

            uint preyFlags = *(uint*)prey;
            if ((preyFlags & 1) == 0)
            {
                *(int*)(prey + 0x54) = 0x18000;                 // et elle decolle
                *(uint*)prey = preyFlags | 1;
            }

            // Ghidra: LAB_0049ba5a — l'effet 0x39 au point d'impact, par le meme detour que le
            // Crabe : la bete se pose une image sur sa victime, puis reprend sa position.
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
            return;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_0049beab — ni recul ni relance, comme le Crabe.
        if ((flags & 0x11) != 0) return;
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
                *(int*)(e + 0x5c) = (boldness + 0xc0) << 7;
                return;

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

                int walk = (boldness + 0x80) << 7;
                *(int*)(e + 0x5c) = walk;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * walk) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(course) * walk) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049bb02..0x0049bd9a</c> — <b>le troisieme temps</b> du Grizzly.
    ///
    /// <para>
    /// <b>Il bondit depuis les DEUX plages utiles</b>, et c'est ce qui le distingue le plus a l'œil
    /// du Crabe. Au corps a corps, un tirage sur cent le fait griffer sous <c>0x32</c>, tenir sa
    /// distance sous <c>0x4b</c>, <b>bondir</b> entre <c>0x4b</c> et <c>0x59</c> s'il est dans le
    /// cone, et s'ecarter au-dela. A l'approche, la sortie qui se tournait vers la cible chez la
    /// famille terrestre bondit ici aussi.
    /// </para>
    /// <para>
    /// <b>Le bond ne pose aucun compte a rebours</b> — ni <c>+0x43</c>, ni remise a zero de la
    /// poussee. Il n'en a pas besoin : le geste vit tant que la bete est en l'air.
    /// </para>
    /// <para>
    /// L'errance lointaine n'a que deux sorties, contre trois chez toutes les precedentes : sous
    /// <c>0x32</c> il se repose, au-dela il part dans une direction entierement tiree. <b>Il
    /// n'hesite jamais de loin.</b>
    /// </para>
    /// </summary>
    private static void ReplanPouncingEntityAction_0049B840(byte* e, int boldness, int timid)
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

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    uint state = *(uint*)e;
                    *(int*)(e + 0x18) = 3;
                    e[0x42] = 0;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (state & 0xfeffffff) | 2;
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

            // Ghidra: LAB_0049bbd2 — le bond de pres, sur le meme cone que la griffe.
            if (roll < 0x5a && ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
            {
                PounceAt(e);
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                // On ne bondit pas sur ce qu'on a dans le dos.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                PounceAt(e);
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

        // ================================================================ l'errance
        if (CrtRand() % 100 < 0x32)
        {
            RestSceneEntity(e, timid);
            return;
        }

        // Ghidra: LAB_0049bd6f — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_0049bc99 / LAB_0049bca4 — <b>armer le bond</b> : le drapeau de geste,
    /// l'animation 4, la posture 1 et la machine a trois temps mise a 2 dans <c>+0x10</c>. Les deux
    /// sites qui bondissent partagent ce corps, et ni l'un ni l'autre ne pose de compte a
    /// rebours.</summary>
    private static void PounceAt(byte* e)
    {
        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
        *(int*)(e + 0x18) = 4;
        e[0x42] = 1;
        *(ushort*)(e + 0x10) = 2;
    }
}
