// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CRABE GEANT — 0x0049C0B0, sous-type 5 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// Terrestre. La plus proche parente des deux premieres routines : meme integration
// FUN_00488160, meme queue de quatre appels, meme table de sauts a quatre entrees.
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
    // 0x0049C0B0 — LE CRABE GEANT, sous-type 5 du bloc A de la foret. Transcrite le 2026-08-29 au
    // desassembleur (Legacy/Raw/Blaze/FUN_0049C0B0.asm, 2096 octets, absente de Blaze.c).
    //
    // ELLE EST TERRESTRE, et c'est la plus proche parente des deux premieres routines : meme
    // integration FUN_00488160, meme queue de quatre appels, meme table de sauts — quatre entrees
    // relevees a 0x0049c8cc — et les memes feuilles partagees. Le tri par la fonction d'integration
    // recommande dans Comportement.md s'est verifie du premier coup, et la replanification est
    // celle de FUN_0049a980 a deux branches pres.
    //
    // CE QUI LUI EST PROPRE : DEUX ATTAQUES, PAS UNE.
    //
    //   - La PINCE, posture 0 en geste : un balayage du gabarit a CHAQUE image de la douzieme a la
    //     quinzieme, sans verrou de coup unique — donc jusqu'a quatre occasions de toucher dans un
    //     seul geste. Aucune des quatre routines precedentes ne frappe plus d'une fois par geste.
    //
    //   - La RUEE, posture 1 en geste : une machine a trois temps portee par +0x10, decomptee de 2
    //     a 0. A 2 elle amortit sa poussee d'un seizieme et vise ; a la quatorzieme image du geste
    //     elle passe a 1 ; a 1 elle se lance, en versant (hardiesse + 0x380) << 7 dans ses VITESSES
    //     +0x50/+0x58 ; a 0 elle cherche ce qu'elle a percute, et le verrou 0x20 la limite alors a
    //     un seul contact.
    //
    // ET LA RUEE NE PORTE QU'A MOITIE. Avant d'appeler FUN_00539b80 elle divise par deux le
    // premier des huit caracteres du bloc de MODELE (+0x30, l'attaque), puis le remet tel quel
    // aussitot apres. Comme FUN_00538af0 recopie ce bloc dans la fiche a chaque coup, la division
    // se propage jusqu'aux degats et disparait ensuite sans laisser de trace. C'est la premiere
    // routine ecrite qui MODULE les degats plutot que de les subir, et le detour par le modele
    // partage — et non par la fiche de la bete — est ce qui rend l'astuce invisible a la lecture
    // de FUN_00539b80.
    //
    // Enfin, un contact reussi projette la victime a trente unites par image dans le cap du crabe,
    // la decolle du sol si elle y etait, et fait naitre l'effet 0x39 au point d'impact — le crabe
    // se pose une image sur la position de sa victime pour cela, puis reprend la sienne.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0049c0b0</c> — <b>l'intelligence du Crabe geant</b>, sous-type 5 de la table
    /// installee <c>0x0056ad48</c> en foret. Cinquieme routine d'intelligence transcrite.
    ///
    /// <para>
    /// <b>Quatre postures</b>, aiguillees par la table de sauts relevee a <c>0x0049c8cc</c>
    /// (<c>0x0049c415</c>, <c>0x0049c44e</c>, <c>0x0049c4b1</c>, <c>0x0049c539</c>) : 0 attendre,
    /// 1 se tourner, 2 s'approcher jusqu'a la distance tenue en <c>+0x10</c>, 3 marcher vers un cap
    /// range dans ce meme <c>+0x10</c>. Il n'y a pas de « foncer » comme chez
    /// <see cref="TickSceneEntityAi_0049A980"/> : la ruee prend sa place, et elle est un GESTE et
    /// non une posture.
    /// </para>
    /// <para>
    /// Sa sonde est <c>{0, 0x32, 0x3c, 0x64, 0, 0x50}</c> — la plus longue des cinq en portee comme
    /// en tolerance de hauteur, et la seule dont le deuxieme terme vaille <c>0x32</c>. Une pince
    /// qui balaie large.
    /// </para>
    /// <para>
    /// <b>Sa marche ne tient pas compte de la vitesse rendue par la lecture de situation</b>, la ou
    /// sa sœur <c>0x0049a980</c> multiplie sa poussee par elle avant d'avancer. Le crabe applique
    /// la poussee brute a sa position. Ce n'est pas un oubli de transcription : les deux corps
    /// different bien d'un <c>imul</c>.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049C0B0(byte* record)
    {
        byte* e = record;

        // Le bloc de MODELE sert ici, ce qui n'arrive dans aucune des quatre routines precedentes :
        // c'est lui que la ruee ampute de la moitie de son attaque le temps d'un coup.
        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x32;
        probe[2] = 0x3c;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x50;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideLungingEntityAction_0049C0B0(e, model, probe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanLungingEntityAction_0049C0B0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049c13d..0x0049c5d6</c> — <b>le premier temps</b> du Crabe geant : ce qu'il
    /// fait de l'image en cours, selon qu'il est en train de frapper ou qu'il tient une posture.
    ///
    /// <para>
    /// <b>La forme du geste se lit sur la posture</b>, et non sur un champ propre : posture 0 c'est
    /// la pince, posture 1 c'est la ruee, toute autre valeur ne fait rien de l'image. Le champ
    /// <c>+0x42</c> sert donc a deux choses selon que le bit 1 des drapeaux est mis ou non —
    /// posture tenue quand il est absent, forme du geste quand il est present.
    /// </para>
    /// <para>
    /// <b>Ni recul ni relance</b> : contrairement a <see cref="TickSceneEntityAi_0049A980"/>, le
    /// mot d'etat <c>0x11</c> renvoie directement a l'application, sans la branche qui se tourne
    /// vers l'agresseur ni la chance de relancer une attaque en fin de geste. Sur ce point le crabe
    /// suit <see cref="TickSceneEntityAi_00499DE0"/>.
    /// </para>
    /// </summary>
    private static void DecideLungingEntityAction_0049C0B0(
        byte* e, byte* model, short* probe,
        int boldness, int distance, int heading, int speed, uint traits)
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

            // ------------------------------------------------------------ 0 : LA PINCE
            if (posture == 0)
            {
                // Ghidra: LAB_0049c371 — quatre images de contact, et pas de verrou : le gabarit
                // est croise a chacune d'elles, donc une victime restee dessous encaisse quatre
                // fois. C'est ce que fait le binaire, pas une garde oubliee a la transcription.
                short frame = *(short*)(e + 0x1a);
                if (frame >= 0x0c && frame <= 0x0f)
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

                // Ghidra: LAB_0049c3d0 — la poussee retombe dans tous les cas ; le geste ne se
                // ferme que si le bit 0x2000000 est la.
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0)
                {
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = flags & 0xfffffffd;
                    return;
                }

                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049c4a5
                return;
            }

            if (posture != 1) return;

            // ------------------------------------------------------------ 1 : LA RUEE
            short phase = *(short*)(e + 0x10);

            // -------------------------------------------------------- 2 : amortir et viser
            if (phase == 2)
            {
                int carried = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = carried - (carried >> 4);

                // Le passage au temps suivant est cale sur l'ANIMATION, pas sur un compteur : la
                // quatorzieme image du geste est celle ou les pinces sont ecartees.
                if (*(short*)(e + 0x1a) == 0x0e) *(short*)(e + 0x10) = (short)(phase - 1);

                TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x20, heading);
                return;
            }

            // -------------------------------------------------------- 1 : se lancer
            if (phase == 1)
            {
                // La poussee la plus forte de toutes les routines ecrites, et la seule versee dans
                // les VITESSES d'un coup puis remise a zero dans la meme image : ce qui avance le
                // crabe ensuite, c'est l'inertie que FUN_00488160 amortit.
                int thrust = (boldness + 0x380) << 7;
                *(int*)(e + 0x5c) = thrust;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                *(short*)(e + 0x10) -= 1;
                return;
            }

            if (phase != 0) return;

            // -------------------------------------------------------- 0 : le contact
            // Ghidra: LAB_0049c241 — le compte a rebours ferme le geste ET leve le verrou de coup
            // unique en meme temps (0xdd efface les bits 0x02 et 0x20).
            if ((flags & 0x2000000) != 0 && e[0x43] != 0)
            {
                e[0x43]--;
                if (e[0x43] == 0) *(uint*)e = flags & 0xffffffdd;
            }

            // Ghidra: LAB_0049c25a — le verrou : une ruee ne touche qu'une fois.
            if ((*(uint*)e & 0x20) != 0) return;

            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
            if (prey != null) *(uint*)e |= 0x20;

            // Ghidra: LAB_0049c283
            if (*Mem.Camera(0x005de0b8) == 0) return;

            *(ushort*)(prey + 0x96) = 0x10;
            *(uint*)prey |= 0x40;

            // LA MOITIE : le premier caractere du bloc de MODELE est l'attaque, et
            // RebaseStatBlockOnModel_00538AF0 le recopie dans la fiche au debut de chaque
            // resolution. Le diviser ici puis le remettre fait donc un coup deux fois moins fort,
            // sans rien laisser derriere.
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

            // Ghidra: LAB_0049c322 — l'effet 0x39 doit naitre AU POINT D'IMPACT. Le binaire n'a pas
            // d'autre moyen de le poser ailleurs qu'a l'entite source, alors il deplace le crabe
            // sur sa victime le temps de l'appel et le remet ensuite. Meme ruse que la trainee de
            // TickSceneEntityAi_0049A980, sur la position au lieu de la vitesse.
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
        // Ghidra: LAB_0049c3ed — ni le recul (0x10) ni la fin de geste (0x01) ne donnent lieu a
        // quoi que ce soit ici : le crabe encaisse sans se retourner.
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

                // La POSITION avance directement, sans passer par la vitesse — et sans le facteur
                // de vitesse que la sœur 0x0049a980 applique ici.
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
    /// Ghidra: <c>0x0049c628..0x0049c8c0</c> — <b>le troisieme temps</b> du Crabe geant : quelle
    /// posture prendre pour la suite.
    ///
    /// <para>
    /// <b>C'est celle de <see cref="TickSceneEntityAi_0049A980"/> a deux differences pres</b>, et
    /// les deux tiennent au fait que la ruee remplace le « foncer » : au corps a corps, une attaque
    /// refusee mene droit a l'hesitation, sans l'esquive de cote ni la charge que la sœur essaie
    /// avant d'y renoncer ; a l'approche, la sortie qui se tournait vers la cible LANCE LA RUEE —
    /// drapeau de geste, animation 4, posture 1 et le compte a trois temps arme a 2 dans
    /// <c>+0x10</c>.
    /// </para>
    /// <para>
    /// <b>Un chiffre de 1998 qui ne dit rien.</b> Le compte a rebours de la ruee vaut
    /// <c>(timidite &gt;&gt; 8) + 8</c>, et la timidite ne depasse jamais <c>0x40</c> : le decalage
    /// rend toujours zero, donc la duree est invariablement 8. Le <c>sar ecx,8</c> est bien dans le
    /// binaire — c'est vraisemblablement un <c>/2</c> ecrit trop grand, mais rien ne le prouve, et
    /// la transcription garde le calcul tel quel.
    /// </para>
    /// </summary>
    private static void ReplanLungingEntityAction_0049C0B0(byte* e, int boldness, int timid)
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
                // La pince : dans le cone ET assez pres, la portee tiree s'elargissant avec la
                // timidite. Sinon on hesite — le crabe n'a pas d'esquive.
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

            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
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
                // La cible doit etre dans la moitie avant : on ne se rue pas sur ce qu'on a dans
                // le dos.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: LAB_0049c795 — LA RUEE. Le geste s'arme ici ; la machine a trois temps
                // de la decision le deroule.
                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 4;
                *(uint*)e = (state & 0xfeffffff) | 2;
                e[0x42] = 1;
                e[0x43] = (byte)((timid >> 8) + 8);
                *(ushort*)(e + 0x10) = 2;
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

        // Ghidra: LAB_0049c899 — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }
}
