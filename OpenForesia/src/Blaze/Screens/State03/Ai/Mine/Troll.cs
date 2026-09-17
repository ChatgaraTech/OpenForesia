// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE TROLL — 0x004965B0, sous-type 16 du bloc C de la mine (zone 1, sous-zone 9).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, sous-zone 9, modele 3 : « Troll »,
// 850 PV, 100 PM, rang 130 — LE PLUS GROS DE LA MINE, et le plus gros du depot a ce jour).
// 1 082 instructions, absentes de Blaze.c ; releve Legacy/Raw/Blaze/FUN_004965B0.asm, deux tables
// de sauts contiguës a 0x0049734C (cinq gestes) et 0x00497360 (quatre postures). Aucun jumeau.
//
// TROIS CHOSES QU'AUCUNE AUTRE ROUTINE D'INTELLIGENCE NE FAIT :
//
//   - IL SE SOIGNE. Avant toute autre chose, et QUELLE QUE SOIT SA SITUATION — endormi, en recul,
//     hors de portee —, il ajoute 8 points de vie a son bloc de statistiques une image sur vingt
//     (l'horloge du monde 0x005f2cf0), plafonnes a son maximum. Sur ses 850 points de vie cela fait
//     24 points par seconde a 60 images : il faut le tuer vite, ou pas du tout. C'est la seule
//     regeneration ecrite dans une routine d'IA du depot.
//   - IL A CINQ GESTES, la ou la famille terrestre en a deux et l'Ours mechant trois : 0 le bond,
//     1 le coup de massue, 2 la charge, 3 le bond empoisonne, 4 le souffle.
//   - SA CHARGE S'ENCHAINE TOUTE SEULE SUR UN BOND. A la fin de l'animation 5, le geste 2 ne se
//     ferme pas : il repose lui-meme la posture 0 et la phase 2, donc le Troll charge PUIS bondit,
//     sans repasser par la replanification. Et pendant la charge il frappe A CHAQUE IMAGE, sans
//     verrou — c'est le seul geste du depot qui ne pose ni ne lit le bit 0x20.
//
// DEUX AUTRES SINGULARITES, PLUS DISCRETES :
//
//   - SES DEUX BONDS NE PORTENT QU'EN MONTANT (+0x54 > 0). Le Grizzly frappait pendant toute la
//     trajectoire ; le Troll rate ce qu'il survole.
//   - IL NE MODULE RIEN SUR SA TIMIDITE. Ses rebours valent rand()%10 + 0x14 et ses cones sont des
//     constantes (0x40 dans un geste, 0x80 hors geste). La hardiesse que ReadSceneEntitySituation
//     calcule pour lui n'est jamais relue — exactement comme chez le Jeune dragon.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00552908</c> et <c>0x00552914</c> — <b>les deux gabarits de portee du
    /// Troll</b>, releves dans <c>.data</c> (<c>OpenForesia.Tools -- at</c>).
    ///
    /// <para>
    /// Six termes chacun, dans l'ordre de <see cref="s_MeleeProbes_00562cf0"/>, comme
    /// <see cref="s_GrizzlyProbes_00552960"/>. Les deux ont un decalage vertical NUL : le Troll
    /// frappe a plat, la ou la griffe du Grizzly s'abattait d'en haut (<c>-0x100</c>).
    /// </para>
    /// <para>
    /// La rangee du <b>bond</b> sert a trois gestes — le bond, la charge et le bond empoisonne ; la
    /// rangee de la <b>massue</b> n'est lue que par le geste 1, et elle porte plus loin
    /// (<c>0x64</c> contre <c>0x3c</c>) mais moins large (<c>0x50</c> contre <c>0x64</c>).
    /// </para>
    /// <para>
    /// <b>Bornes.</b> Les sondes de cette region sont alignees de douze en douze depuis
    /// <c>0x005528e4</c> ; celle du Troll occupe les deux creneaux <c>0x00552908</c> et
    /// <c>0x00552914</c>, et ce qui suit a <c>0x00552920</c> est la table de sorts de la
    /// Salamandre-de-la-montagne (<c>0000 0a00 0a00 1400</c>), qui n'a pas la forme d'une sonde.
    /// </para>
    /// </summary>
    private static readonly short[] s_TrollProbes_00552908 =
    [
        0x0000, 0x0000, 0x003c, 0x0096, 0x0000, 0x0064,   // 0x00552908 — le bond et la charge
        0x0000, 0x0000, 0x0064, 0x0096, 0x0000, 0x0050,   // 0x00552914 — la massue
    ];

    private static short* s_TrollProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_TrollProbes_00552908"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* TrollProbe(int row)
    {
        if (s_TrollProbeStorage == null)
        {
            s_TrollProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_TrollProbes_00552908.Length, sizeof(short));

            for (int i = 0; i < s_TrollProbes_00552908.Length; i++)
            {
                s_TrollProbeStorage[i] = s_TrollProbes_00552908[i];
            }
        }

        return s_TrollProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x004965b0</c> — <b>l'intelligence du Troll</b>, sous-type 16 de la table
    /// installee <c>0x0056ad48</c> dans la neuvieme sous-zone de la mine.
    ///
    /// <para>
    /// <b>La regeneration est en tete de fonction, avant meme la resolution des blocs</b>, et rien
    /// ne la garde : ni la vitesse du monde, ni les drapeaux de l'entite, ni sa distance au joueur.
    /// Un Troll hors de l'ecran se soigne.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004965B0(byte* record)
    {
        byte* e = record;

        // ---------------------------------------------------------------- 0. se soigner
        // Ghidra: 0x004965b0 — l'horloge du monde, une image sur vingt. Le bloc de statistiques est
        // adresse ici A LA MAIN (e[8] * 0x120 + 0x005ea560) et non par FUN_00538980, qui n'est
        // appele que quatre instructions plus loin : le compilateur a garde les deux calculs.
        if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
        {
            byte* block = s_EntityStatBlocks_005ea560 + e[8] * 0x120;

            *(ushort*)(block + 0x4c) += 8;
            if (*(short*)(block + 0x4c) > *(short*)(block + 0x48))
            {
                *(short*)(block + 0x4c) = *(short*)(block + 0x48);
            }
        }

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        DecideTrollAction_004965B0(e, model, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_00496ebc — sa gravite, la meme que celle du Jeune dragon.
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanTrollAction_004965B0(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00496639..0x00496eb9</c> — <b>le premier temps</b> du Troll : mener le geste en
    /// cours, ou tenir la posture. Cinq gestes, table de sauts a <c>0x0049734C</c>.
    /// </summary>
    private static void DecideTrollAction_004965B0(
        byte* e, byte* model, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ la posture tenue
        if ((flags & 2) == 0)
        {
            HoldTrollStance_00496E49(e, distance, heading, flags);
            return;
        }

        // Le trait 0x10 coupe court : l'espece ne poursuit pas son geste.
        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        switch (e[0x42])
        {
            case 0: TrollLeapGesture_00496675(e, model, heading, poison: false); return;
            case 1: TrollClubGesture_0049689B(e, model, heading); return;
            case 2: TrollChargeGesture_00496AAF(e, heading); return;
            case 3: TrollLeapGesture_00496B92(e, model, heading); return;
            case 4: TrollBreathGesture_00496D9E(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00496675</c> — <b>le bond</b>, geste 0. La machine a trois temps du Grizzly, aux
    /// nombres pres : phase 2 s'accroupir (la poussee decroit d'un seizieme par image, le cri
    /// <c>0x36</c> a l'image 5), phase 1 se detendre (poussee <c>0x19000</c>, verticale
    /// <c>0x32000</c> — presque le double de celle du Grizzly), phase 0 retomber et porter.
    /// </summary>
    private static void TrollLeapGesture_00496675(byte* e, byte* model, int heading, bool poison)
    {
        short phase = *(short*)(e + 0x10);

        // -------------------------------------------------------- 2 : s'accroupir et viser
        if (phase == 2)
        {
            int carried = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = carried - (carried >> 4);

            if (*(short*)(e + 0x1a) == 5)
            {
                *(short*)(e + 0x10) = (short)(phase - 1);
                Aud.PlayPositionalSound(0x36, 0x384, 0x1800, (int*)(e + 0x1c));
            }

            // Ghidra: LAB_004966c0
            TurnSceneEntityTowardTarget(e, 0x40, heading);
            return;
        }

        // -------------------------------------------------------- 1 : detendre
        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0x19000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(uint*)e |= 1;                      // « en l'air »
            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x54) = 0x32000;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(0x36, 0x400, 0x1800, (int*)(e + 0x1c));
            return;
        }

        if (phase != 0) return;

        // -------------------------------------------------------- 0 : la chute et le contact
        ResolveTrollLanding_00496768(e, model, poison);
    }

    /// <summary>
    /// Ghidra: <c>0x00496b92</c> — <b>le bond empoisonne</b>, geste 3. Le meme corps que le geste 0,
    /// emis une seconde fois par le compilateur, a quatre differences :
    /// l'accroupissement se detend a l'image <b>4</b> et sans cri, la detente pousse
    /// <c>0x28000</c> au lieu de <c>0x19000</c> et monte de <c>0x28000</c> au lieu de
    /// <c>0x32000</c>, et l'atterrissage <b>empoisonne</b>.
    /// </summary>
    private static void TrollLeapGesture_00496B92(byte* e, byte* model, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 2)
        {
            int carried = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = carried - (carried >> 4);

            if (*(short*)(e + 0x1a) == 4) *(short*)(e + 0x10) = (short)(phase - 1);

            // Ghidra: LAB_00496bc5
            TurnSceneEntityTowardTarget(e, 0x40, heading);
            return;
        }

        if (phase == 1)
        {
            // Ghidra: LAB_00496c07 — EDI porte 0x28000 et sert aux deux ecritures, la poussee et la
            // vitesse verticale : le bond empoisonne monte exactement autant qu'il avance.
            *(int*)(e + 0x5c) = 0x28000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(uint*)e |= 1;
            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x54) = 0x28000;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 0) return;

        ResolveTrollLanding_00496768(e, model, poison: true);
    }

    /// <summary>
    /// Ghidra: LAB_00496768 et LAB_00496c5b — <b>l'atterrissage d'un bond</b>, les deux sites etant
    /// le meme bloc emis deux fois, a l'empoisonnement pres.
    ///
    /// <para>
    /// <b>Le geste ne se ferme qu'au sol</b>, comme chez le Grizzly, et le verrou <c>0x20</c> ne
    /// laisse porter qu'une fois. Mais le Troll ajoute une condition que personne d'autre n'a :
    /// <c>+0x54 &gt; 0</c>, la vitesse verticale — <b>il ne touche qu'en MONTANT</b>. Ce qu'il
    /// survole en redescendant est sauf.
    /// </para>
    /// <para>
    /// Degats de MOITIE (l'attaque du bloc de modele divisee le temps de la resolution), le meme
    /// idiome que le Crabe, le Grizzly et le Geant.
    /// </para>
    /// </summary>
    private static void ResolveTrollLanding_00496768(byte* e, byte* model, bool poison)
    {
        uint flags = *(uint*)e;

        if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
        {
            *(uint*)e = flags & 0xffffffdd;      // le geste ET le verrou de coup
        }

        if ((*(uint*)e & 0x20) != 0) return;
        if (*(int*)(e + 0x54) <= 0) return;      // il ne porte qu'en montant

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TrollProbe(0), 0);
        if (prey != null) *(uint*)e |= 0x20;

        // Ghidra: LAB_004967ab
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        if (poison)
        {
            e[0x07] = 7;
            ApplyStatusEffect_0053B810((uint*)e, (uint*)prey, 0);
        }

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if ((*(uint*)prey & 0x40000000) != 0) return;

        ThrowTrollVictim_0049684C(e, prey);
    }

    /// <summary>
    /// Ghidra: <c>0x0049689b</c> — <b>le coup de massue</b>, geste 1, sur l'animation 4.
    ///
    /// <para>
    /// Le geste le plus long du depot : il se replace jusqu'a l'image 17 (cone <c>0x40</c>), prend
    /// son elan avec deux cris — <c>0x20</c> a l'image 1, <c>0x24</c> a l'image 16 —, <b>avance
    /// d'un a-coup de <c>0x30000</c> a l'image 17</b> avec le cri <c>0x25</c>, puis porte a partir
    /// de l'image 18, une seule fois (verrou <c>0x20</c>) et a demi-degats.
    /// </para>
    /// </summary>
    private static void TrollClubGesture_0049689B(byte* e, byte* model, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 0x11)
        {
            TurnSceneEntityTowardTarget(e, 0x40, heading);

            if (frame == 1) Aud.PlayPositionalSound(0x20, 0x400, 0x1800, (int*)(e + 0x1c));

            // Ghidra: LAB_004968f7
            if (*(short*)(e + 0x1a) == 0x10)
            {
                Aud.PlayPositionalSound(0x24, 0x200, 0x1800, (int*)(e + 0x1c));
            }
        }

        // Ghidra: LAB_00496916 — l'a-coup en avant, la massue porte le corps avec elle.
        if (*(short*)(e + 0x1a) == 0x11)
        {
            *(int*)(e + 0x5c) = 0x30000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(0x25, 0x32a, 0x1800, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00496975
        if (*(short*)(e + 0x1a) >= 0x12 && (*(uint*)e & 0x20) == 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TrollProbe(1), 0);
            if (prey != null) *(uint*)e |= 0x20;

            // Ghidra: LAB_004969a9
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power >> 1);
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                if ((*(uint*)prey & 0x40000000) == 0) ThrowTrollVictim_0049684C(e, prey);
            }
        }

        // Ghidra: LAB_00496a92
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        *(uint*)e = flags & 0xffffffdd;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00496aaf</c> — <b>la charge</b>, geste 2, sur l'animation 5.
    ///
    /// <para>
    /// <b>Elle frappe a chaque image et ne pose aucun verrou.</b> C'est le seul geste du depot dans
    /// ce cas : le bit <c>0x20</c> est allume quand la sonde ramene quelqu'un, mais jamais teste, si
    /// bien qu'une victime restee devant encaisse autant de coups que la charge dure d'images. Les
    /// degats sont PLEINS — l'attaque du bloc de modele n'est pas divisee, contrairement aux trois
    /// autres gestes qui touchent.
    /// </para>
    /// <para>
    /// <b>Et elle s'enchaine toute seule sur un bond.</b> A la fin de l'animation, au lieu de
    /// fermer le geste, elle repose <c>+0x18 = 3</c>, <c>+0x42 = 0</c> et <c>+0x10 = 2</c> : c'est
    /// exactement ce que la replanification ecrirait pour armer le geste 0. Le Troll charge, puis
    /// bondit, sans reprendre son souffle.
    /// </para>
    /// </summary>
    private static void TrollChargeGesture_00496AAF(byte* e, int heading)
    {
        TurnSceneEntityTowardTarget(e, 0x40, heading);

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TrollProbe(0), 0);
        if (prey != null) *(uint*)e |= 0x20;

        // Ghidra: LAB_00496b03
        if (*Mem.Camera(0x005de0b8) != 0)
        {
            *(uint*)prey |= 0x40;
            *(short*)(prey + 0x96) = 6;          // six images de clignotement, pas seize
            ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
            Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
        }

        // Ghidra: LAB_00496b3e — le martellement, deux fois par tour d'animation.
        short frame = *(short*)(e + 0x1a);
        if (frame == 1 || frame == 0x0b)
        {
            Aud.PlayPositionalSound(0x36, 0x32a, 0x1800, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00496b63 — l'enchainement.
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0)
        {
            *(int*)(e + 0x18) = 3;
            e[0x42] = 0;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            *(ushort*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_00496b86 — et la poussee de la charge, reposee a chaque image.
        *(int*)(e + 0x5c) = 0xc000;
    }

    /// <summary>
    /// Ghidra: <c>0x00496d9e</c> — <b>le souffle</b>, geste 4, sur l'animation 3.
    ///
    /// <para>
    /// <b>Mot pour mot le premier souffle du Jeune dragon</b> (<c>0x004946a3</c>) : meme cri a une
    /// image pres (<c>0x40</c> a l'image 7 ici, <c>0x41</c> a l'image 16 la-bas), meme pile ou face
    /// dans <c>+0x44</c>, meme <c>0x3a98</c> dans <c>+0x48</c>, meme sous-type lu a l'octet
    /// <c>0x0e</c> du repertoire de zone, meme rebours de dix images. Les deux especes partagent
    /// donc <b>la meme entite soufflee</b>, celle que le repertoire de la mine designe.
    /// </para>
    /// </summary>
    private static void TrollBreathGesture_00496D9E(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            // Ghidra: LAB_00496e32 — le rebours de recuperation.
            if (--e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }

        if (*(short*)(e + 0x1a) == 7)
        {
            Aud.PlayPositionalSound(0x40, 0x200, 0x1800, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00496dd1 — la naissance n'a lieu QU'A LA FIN de l'animation.
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        int savedX = *(int*)(e + 0x44);
        int savedY = *(int*)(e + 0x48);

        *(int*)(e + 0x44) = CrtRand() & 1;
        *(int*)(e + 0x48) = 0x3a98;

        byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
        SpawnEffectEntity(e, (byte)((*(byte**)block)[0x0e] + 0xc0), 0, 2);

        *(short*)(e + 0x10) -= 1;
        *(int*)(e + 0x44) = savedX;
        *(int*)(e + 0x48) = savedY;
        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: LAB_0049684c, LAB_00496a4a et LAB_00496d4f — <b>projeter la victime et
    /// poser l'etincelle</b>. Emis trois fois, mot pour mot, par les trois gestes qui touchent hors
    /// de la charge : trente unites dans le cap du Troll, un decollage de <c>0x18000</c> si la
    /// victime etait au sol, puis l'effet <c>0x39</c> ne au point d'impact — le Troll se pose une
    /// image sur sa victime pour le faire naitre, puis reprend sa place.</summary>
    private static void ThrowTrollVictim_0049684C(byte* e, byte* prey)
    {
        int impact = *(short*)(e + 0x32);
        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
        *(int*)(prey + 0x58) = (int)Winsys3d.GetCosTableEntry(impact) * 30;

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
    /// Ghidra: LAB_00496e49 — <b>la posture tenue</b>. Les quatre postures de la famille terrestre,
    /// table de sauts a <c>0x00497360</c>, avec les memes poussees constantes que le Jeune dragon
    /// (<c>0x12000</c> a l'approche, <c>0xe000</c> a la marche) et le meme cone fixe de
    /// <c>0x80</c>.
    /// </summary>
    private static void HoldTrollStance_00496E49(byte* e, int distance, int heading, uint flags)
    {
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte stance = e[0x42];
        if (stance > 3) { TrollStepSound_00496E8E(e); return; }

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

                // Ghidra: LAB_00496e77 — la sortie lointaine passe par LAB_004971af, qui decompte
                // ET remet la poussee a zero ; la sortie proche se contente d'annuler le rebours.
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
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

        TrollStepSound_00496E8E(e);
    }

    /// <summary>Ghidra: LAB_00496e8e — <b>le pas</b>, aux images 4 et 12 de l'animation 1. Le meme
    /// son <c>0x35</c> que le Jeune dragon, une octave plus haut (<c>0x352</c> contre
    /// <c>0x200</c>). Les gestes ne passent pas par ici : seule une posture tenue fait du
    /// bruit.</summary>
    private static void TrollStepSound_00496E8E(byte* e)
    {
        if (*(ushort*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame != 4 && frame != 0x0c) return;

        Aud.PlayPositionalSound(0x35, 0x352, 0x1800, (int*)(e + 0x1c));
    }

    /// <summary>
    /// Ghidra: <c>0x00496f16..0x004971ae</c> — <b>le troisieme temps</b> : quelle posture prendre.
    /// La distance et le cap sont recalcules a plat.
    ///
    /// <para>
    /// <b>La parite du tirage choisit le geste</b>, trois fois sur quatre : au corps a corps, sous
    /// 0x32, impair il <b>bondit</b> et pair il <b>charge</b> ; entre 0x4b et 0x5a, impair la
    /// <b>massue</b> et pair le <b>bond empoisonne</b> ; a distance, sous 0x2d, impair la
    /// <b>massue</b> et pair le <b>souffle</b>. Le meme <c>rand()</c> sert donc deux fois, au seuil
    /// et au choix — et comme il est pris modulo 100, les deux tirages ne sont pas independants.
    /// </para>
    /// <para>
    /// <b>Aucun des cinq gestes ne pose de rebours <c>+0x43</c></b> : c'est leur propre fin
    /// d'animation qui les ferme. Seules les postures tenues en posent un.
    /// </para>
    /// </summary>
    private static void ReplanTrollAction_004965B0(byte* e)
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
                    HesitateSceneEntity(e);       // Ghidra: LAB_00497113
                    return;
                }

                if ((roll & 1) != 0) ArmTrollGesture_004965B0(e, animation: 3, posture: 0, phase: 2);
                else                 ArmTrollGesture_004965B0(e, animation: 5, posture: 2, phase: -1);
                return;
            }

            if (roll < 0x4b)
            {
                // Ghidra: LAB_00496fe1 — la distance tenue est 0x64, laissee dans EDI par le
                // modulo du tirage.
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = TrollDelay();
                return;
            }

            if (roll < 0x5a && ((heading + 0x100) & 0xfff) <= 0x200)
            {
                if ((roll & 1) != 0) ArmTrollGesture_004965B0(e, animation: 4, posture: 1, phase: -1);
                else                 ArmTrollGesture_004965B0(e, animation: 6, posture: 3, phase: 2);
                return;
            }

            // Ghidra: LAB_0049706e — s'ecarter.
            WalkTrollAway_0049706E(e, heading, 0x200);
            return;
        }

        // ================================================================ l'approche
        if (roll < 0x2d)
        {
            if (((heading + 0x200) & 0xfff) > 0x400)
            {
                HesitateSceneEntity(e);           // Ghidra: LAB_00497113
                return;
            }

            if ((roll & 1) != 0) ArmTrollGesture_004965B0(e, animation: 4, posture: 1, phase: -1);
            else                 ArmTrollGesture_004965B0(e, animation: 3, posture: 4, phase: 1);
            return;
        }

        if (roll < 0x46)
        {
            // Ghidra: LAB_00497123 — s'ecarter, mais de l'autre cote.
            WalkTrollAway_0049706E(e, heading, -0x600);
            return;
        }

        if (roll < 0x5a)
        {
            // Ghidra: LAB_00497168
            e[0x42] = 2;
            *(ushort*)(e + 0x10) = 0x64;
            e[0x43] = TrollDelay();
            return;
        }

        // Ghidra: LAB_00497190 — se reposer.
        e[0x42] = 0;
        e[0x43] = TrollDelay();
    }

    /// <summary>Ghidra: les six sites d'armement de <c>0x00496f9b</c> a <c>0x004970ed</c> —
    /// <b>armer un geste</b> : l'animation dans <c>+0x18</c>, la posture dans <c>+0x42</c>, le bit
    /// de geste pose et le bit <c>0x1000000</c> efface. <paramref name="phase"/> negatif veut dire
    /// que le site n'ecrit pas <c>+0x10</c> — c'est le cas de la charge et de la massue, qui n'ont
    /// pas de machine a phases.</summary>
    private static void ArmTrollGesture_004965B0(byte* e, int animation, byte posture, int phase)
    {
        uint state = *(uint*)e;
        *(int*)(e + 0x18) = animation;
        e[0x42] = posture;
        *(uint*)e = (state & 0xfeffffff) | 2;

        if (phase >= 0) *(ushort*)(e + 0x10) = (ushort)phase;
    }

    /// <summary>Ghidra: LAB_0049706e et LAB_00497123 — <b>marcher a cote</b> : posture 3, cap tire
    /// dans une fourchette de <c>0xc00</c> autour de la proie, plus un biais. Les deux sites sont le
    /// meme bloc, au biais pres — <c>+0x200</c> de pres, <c>-0x600</c> de loin.</summary>
    private static void WalkTrollAway_0049706E(byte* e, int heading, int bias)
    {
        e[0x42] = 3;
        e[0x43] = TrollDelay();
        *(ushort*)(e + 0x10) = (ushort)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) + bias);
    }

    /// <summary>Ghidra: le motif <c>rand() % 10 + 0x14</c>, emis cinq fois — <b>la duree d'une
    /// posture</b> du Troll, de 20 a 29 images. Comme le Jeune dragon, il ne module pas sa
    /// timidite ; contrairement a lui, il part du meme <c>0x14</c> que la famille terrestre, si bien
    /// que seul l'ecart <c>timide &gt;&gt; 1</c> manque.</summary>
    private static byte TrollDelay() => (byte)(CrtRand() % 10 + 0x14);
}
