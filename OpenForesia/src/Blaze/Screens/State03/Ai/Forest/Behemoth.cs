// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE BEHEMOTH — 0x004A54E0, sous-type 17 du bloc C de la foret (zone 3, sous-zones 5 et 9).
//
// LANCEUR DE SORTS, et le PREMIER ecrit : son bloc d'incantation a ete recopie tel quel
// pour le Trent, le Varan et le sous-type 1 de l'auberge. 2 571 octets, soixante-quatre
// etiquettes, DEUX tables de sauts.
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
    // 0x004A54E0 — LE BEHEMOTH, sous-type 17 du bloc C de la foret (sous-zones 5 et 9), et la
    // derniere intelligence du bloc. Transcrite le 2026-09-01 au desassembleur
    // (Legacy/Raw/Blaze/FUN_004A54E0.asm, 2 571 octets, absente de Blaze.c). Soixante-quatre
    // etiquettes de saut, et DEUX tables de sauts a quatre entrees, 0x004A5EEC et 0x004A5EFC.
    //
    // CRIBLE DU DOUBLON PASSE D'ABORD : ecart minimal 722 lignes sur les quatorze IA deja ecrites.
    // Corps neuf.
    //
    // C'EST LA SEULE DU DEPOT QUI AIT DEUX MACHINES A POSTURES. Le bit 0x800 des drapeaux choisit
    // laquelle tourne :
    //
    //   - degage, c'est le regime ORDINAIRE : quatre postures lues dans +0x42, table 0x004A5EFC,
    //     la charpente de toute la famille ;
    //   - pose, c'est L'INCANTATION : quatre etats lus dans +0x10, table 0x004A5EEC, comptes a
    //     rebours de 3 vers 0. Le regime se termine tout seul en effacant son propre bit.
    //
    // ET C'EST LA PREMIERE BETE DU DEPOT QUI LANCE UN SORT. Elle en tire un sur trois dans une
    // table de 0x00552A30, le range dans +0x06, va lire la DUREE de l'incantation dans la fiche de
    // sort elle-meme (+0x13), puis lance a la quatorzieme image de l'animation 7.
    //
    // TROIS GESTES, comme le Mechant ours, mais pas les memes : le coup de patte, la prise (au mot
    // pres celle de l'ours, projection a 30x comprise) et le SAUT — elle bondit sur place a
    // 0x64000 de vitesse verticale, et son ATTERRISSAGE fait naitre une entite de la table par
    // sous-zone. Une onde de choc, tres probablement.
    //
    // SA GRAVITE EST 0x7fff, LE MAXIMUM D'UN SHORT SIGNE. Le Grizzly a 0x6000, le Mechant ours
    // 0x7000. C'est la bete la plus lourde du jeu, et le binaire le dit en une constante.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552a18</c> et <c>0x00552a24</c> — <b>les deux gabarits de portee du
    /// Behemoth</b>, releves dans <c>.data</c>. Ils ferment la serie ouverte a <c>0x00552930</c>.
    ///
    /// <para>
    /// <b>Celui de la prise porte un decalage vertical POSITIF</b>, <c>+0x100</c>, le seul de toute
    /// la serie : la griffe du Grizzly a <c>-0x100</c> et s'abat d'en haut, celle-ci empoigne par
    /// le bas. Et le sixieme terme du coup de patte, <c>0x96</c>, est la plus longue portee
    /// relevee.
    /// </para>
    /// </summary>
    private static readonly short[] s_BehemothProbes_00552A18 =
    [
        0x0000, 0x0032, 0x005a, 0x0096, 0x0000, 0x0096,   // 0x00552a18 — le coup de patte
        0x0000, 0x0032, 0x0046, 0x0096, 0x0100, 0x00aa,   // 0x00552a24 — la prise
    ];

    private static short* s_BehemothProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_BehemothProbes_00552A18"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* BehemothProbe(int row)
    {
        if (s_BehemothProbeStorage == null)
        {
            s_BehemothProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_BehemothProbes_00552A18.Length, sizeof(short));

            for (int i = 0; i < s_BehemothProbes_00552A18.Length; i++)
            {
                s_BehemothProbeStorage[i] = s_BehemothProbes_00552A18[i];
            }
        }

        return s_BehemothProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552a30</c> — <b>les trois sorts du Behemoth</b>, releves dans
    /// <c>.data</c> : <c>0x000d</c>, <c>0x0013</c>, <c>0x0017</c>, suivis d'un mot nul.
    ///
    /// <para>
    /// Meme forme que <see cref="s_SpellGestures_00552978"/>, la table du Goblin-sorcier : l'octet
    /// haut est la <b>famille</b>, l'octet bas l'<b>indice</b>, les deux tels que
    /// <see cref="CastSpell_0053A980"/> les recoit. <b>Les trois sont de la famille 0</b>, la
    /// premiere ecole, aux indices 13, 19 et 23.
    /// </para>
    /// <para>
    /// <b>La borne est double</b>, et c'est ce qui la rend sure : le corps tire <c>hasard % 3</c>,
    /// donc il n'en lit jamais plus de trois, et le quatrieme mot du binaire est nul.
    /// </para>
    /// </summary>
    private static readonly ushort[] s_BehemothSpells_00552A30 = [0x000d, 0x0013, 0x0017];

    /// <summary>Ghidra: le motif <c>rand() % 10 + 0x14</c>, emis cinq fois — <b>la duree d'une
    /// posture du Behemoth</b>, de 20 a 29 images. <b>Elle ne consulte pas la timidite</b>, la ou
    /// <see cref="SceneEntityDelay"/> y ajoute <c>timide / 2</c> : c'est la seule de la famille
    /// dont le rythme ne depende pas de l'ecart de niveau. Le corps ne calcule d'ailleurs jamais la
    /// timidite.</summary>
    private static byte BehemothDelay() => (byte)(CrtRand() % 10 + 0x14);

    /// <summary>
    /// Ghidra: <c>0x004a54e0</c> — <b>l'intelligence du Behemoth</b>, sous-type 17 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 5 et 9 de la foret, et la derniere du bloc.
    ///
    /// <para>
    /// <b>Deux machines a postures</b>, et le bit <c>0x800</c> des drapeaux choisit laquelle :
    /// degage, quatre postures lues dans <c>+0x42</c> (table <c>0x004A5EFC</c>) ; pose, quatre
    /// etats d'incantation lus dans <c>+0x10</c> (table <c>0x004A5EEC</c>). Aucune autre routine du
    /// depot n'a deux dispatchs.
    /// </para>
    /// <para>
    /// <b>Sa gravite est <c>0x7fff</c></b>, le maximum d'un <c>short</c> signe — contre
    /// <c>0x6000</c> pour le Grizzly et <c>0x7000</c> pour le Mechant ours.
    /// </para>
    /// <para>
    /// <b>Ses cones de virage sont des CONSTANTES</b> — <c>0x40</c> dans les gestes, <c>0x80</c>
    /// aux postures 1 et 2, <c>0x60</c> a la posture 3 — la ou toute la famille les tire de la
    /// hardiesse. Seule l'incantation en a un qui varie (<c>hardiesse * 4 + 0x32</c>). Elle tourne
    /// donc au meme rythme quel que soit le niveau du groupe : une masse, pas un duelliste.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A54E0(byte* record)
    {
        byte* e = record;

        // Comme le Crabe, le Grizzly et le Mechant ours : sa prise ampute l'attaque de moitie.
        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        DecideCastingBeastAction_004A54E0(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004a5c5b — la gravite la plus lourde du depot, reposee a chaque image.
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanCastingBeastAction_004A54E0(e, boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004a5536..0x004a5c56</c> — <b>le premier temps</b> du Behemoth.
    /// </summary>
    private static void DecideCastingBeastAction_004A54E0(
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
                case 0: SwipeGesture_004A5814(e, heading); return;
                case 1: BehemothSeizeGesture_004A5618(e, model, heading, flags); return;
                case 2: StompGesture_004A5580(e, flags); return;
                default: return;
            }
        }

        if ((flags & 0x10) != 0) return;

        // ================================================================ l'incantation
        // Ghidra: toutes les sorties de l'incantation sautent DIRECTEMENT au bloc d'application, en
        // contournant LAB_004a5c29 : une bete qui incante ne fait pas de bruit de pas.
        if ((flags & 0x804) != 0)
        {
            AdvanceIncantation_004A5919(e, boldness, heading, traits, flags);
            return;
        }

        // ================================================================ la posture tenue
        // Une bete en l'air ne change pas de posture — mais le pas est quand meme evalue.
        if ((flags & 1) != 0) return;

        byte held = e[0x42];
        if (held > 3)
        {
            StampBehemothFootfall(e);
            return;
        }

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
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x14000;
                break;

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

                // Comme le Mechant ours, elle DEPLACE sa position le long de +0x10 puis tourne son
                // corps vers la cible — mais d'un pas fixe de 0x10000, sans terme de hardiesse.
                *(int*)(e + 0x5c) = 0x10000;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) +=
                    ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) +=
                    (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, 0x60, heading);
                break;
            }
        }

        StampBehemothFootfall(e);
    }

    /// <summary>Ghidra: <c>0x004a5c29..0x004a5c56</c> — <b>le pas</b>, sur le chemin commun de
    /// sortie des postures : le son <c>0x35</c> aux images 6 et 18 de l'animation de marche. Elle
    /// ne secoue PAS la camera, contrairement au Mechant ours — la seule des deux a le faire est la
    /// plus legere des deux, ce qui n'a pas d'explication et n'en demande pas.</summary>
    private static void StampBehemothFootfall(byte* e)
    {
        if (*(ushort*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame != 6 && frame != 0x12) return;

        Aud.PlayPositionalSound(0x35, 0x200, 0x1800, (int*)(e + 0x1c));
    }

    /// <summary>
    /// Ghidra: <c>0x004a5919..0x004a5a69</c> — <b>l'incantation</b>, la seconde machine a postures,
    /// et <b>le premier sort lance par une bete du depot</b>.
    ///
    /// <para>
    /// Elle tourne tant que le bit <c>0x800</c> est pose, sur un compteur <c>+0x10</c> qui descend
    /// de 3 a 0 :
    /// </para>
    /// <list type="number">
    /// <item><b>3</b> — l'entite de lancement <c>0xbd</c> nait, un sort est tire dans
    /// <see cref="s_BehemothSpells_00552A30"/> et range dans <c>+0x06</c>, et la <b>duree</b> de
    /// l'incantation est lue dans la <b>fiche de sort elle-meme</b> : le <c>+0x13</c> de
    /// <c>famille * 4</c> puis <c>indice * 0x30</c> dans le repertoire <c>+0x9c</c>. Le temps
    /// d'incantation est donc une propriete du sort, pas de la bete.</item>
    /// <item><b>2</b> — le rebours s'ecoule pendant qu'elle se tourne vers sa cible, seul endroit
    /// du corps ou le cone depende de la hardiesse (<c>hardiesse * 4 + 0x32</c>).</item>
    /// <item><b>1</b> — l'animation 7 est posee, et a sa quatorzieme image le sort part.</item>
    /// <item><b>0</b> — la fin de l'animation efface le bit <c>0x800</c> et rend la main au regime
    /// ordinaire.</item>
    /// </list>
    /// <para>
    /// <b>Certains traits d'espece annulent l'incantation net</b> : si <c>stat + 0x40</c> porte un
    /// bit de <c>0x400003e8</c>, les bits <c>0x804</c> sont effaces et le rebours remis a zero
    /// avant meme le dispatch. Le masque n'est pas decompose ici — chacun de ses bits est un trait
    /// dont le sens n'est pas etabli.
    /// </para>
    /// </summary>
    private static void AdvanceIncantation_004A5919(
        byte* e, int boldness, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;                 // le bit 0x800 et le bit 0x4
            e[0x43] = 0;
            *(uint*)e = flags;
        }

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

                if (*(short*)(e + 0x1a) != 0x0e) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            // ---------------------------------------------------------- 2 : incanter
            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) -= 1;

                // Le seul cone du corps qui depende de la hardiesse.
                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;

            // ---------------------------------------------------------- 3 : choisir le sort
            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                ushort sheet = s_BehemothSpells_00552A30[CrtRand() % 3];
                *(ushort*)(e + 6) = sheet;

                // Ghidra: `mov ecx,[ecx+9Ch] ; mov ecx,[ecx+edx*4] ; mov dl,[ecx+eax+13h]` — le
                // repertoire des sorts, adresse famille * 4 puis indice * 0x30, comme
                // CastSpell_0053A980 le fait lui-meme.
                byte* family = ((byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)))
                                   [(sheet >> 8) & 0xff];

                e[0x43] = family[(sheet & 0xff) * 0x30 + 0x13];

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a5814..0x004a5905</c> — <b>le coup de patte</b>, le geste de la posture 0.
    ///
    /// <para>
    /// Jusqu'a la cinquieme image elle se replace, a la sixieme elle s'elance
    /// (<c>+0x5c = 0x24000</c>), a partir de la septieme elle balaie. Le cone de correction est une
    /// <b>constante <c>0x40</c></b> : la plus etroite de la famille, et la seule qui ne varie pas.
    /// </para>
    /// </summary>
    private static void SwipeGesture_004A5814(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 5) TurnSceneEntityTowardTarget(e, 0x40, heading);

        if (frame == 6)
        {
            *(int*)(e + 0x5c) = 0x24000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) +=
                ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) +=
                (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x1a) >= 7)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), BehemothProbe(0), 0);

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
    /// Ghidra: <c>0x004a5618..0x004a5813</c> — <b>la prise</b>, le geste de la posture 1, et
    /// <b>la jumelle de celle du Mechant ours</b> : meme bit <c>0x20</c> de marquage, meme moitie
    /// d'attaque, meme deplacement sur la victime pour y semer l'effet <c>0x39</c>.
    ///
    /// <para>
    /// <b>Trois differences, toutes chiffrees.</b> Elle se detend en <b>sautant</b> —
    /// <c>+0x54 = 0x32000</c> et le bit 0 pose, la ou l'ours reste au sol —, la projection est a
    /// <b>trente</b> fois le sinus et non quarante, et l'oblique n'existe pas : la victime part
    /// droit devant, sans le biais de <c>0x400</c> que l'ours calcule sur l'attaque du modele.
    /// </para>
    /// <para>
    /// La machine est a trois temps sur <c>+0x10</c> : <b>2</b> elle se ramasse en amortissant sa
    /// poussee par quarts, <b>1</b> elle bondit, <b>0</b> elle empoigne ce qu'elle touche — et le
    /// geste ne se ferme <b>qu'au sol</b>, comme le bond du Grizzly.
    /// </para>
    /// </summary>
    private static void BehemothSeizeGesture_004A5618(byte* e, byte* model, int heading, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        // -------------------------------------------------------------- 2 : se ramasser
        if (phase == 2)
        {
            int carried = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = carried - (carried >> 2);

            if (*(short*)(e + 0x1a) == 0x0d) *(short*)(e + 0x10) = (short)(phase - 1);

            TurnSceneEntityTowardTarget(e, 0x40, heading);
            return;
        }

        // -------------------------------------------------------------- 1 : bondir
        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0x30000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) +=
                ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) +=
                (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(int*)(e + 0x54) = 0x32000;
            *(uint*)e |= 1;
            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(0x20, 0x1550, 0x1800, (int*)(e + 0x1c));
            return;
        }

        if (phase != 0) return;

        // -------------------------------------------------------------- 0 : la chute et la prise
        if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
        {
            *(uint*)e = flags & 0xffffffdd;                 // le geste ET la prise
        }

        if ((*(uint*)e & 0x20) != 0) return;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), BehemothProbe(1), 0);
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

        int impact = *(short*)(e + 0x32);
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
    /// Ghidra: <c>0x004a5580..0x004a5617</c> — <b>le saut</b>, le geste de la posture 2 : elle
    /// bondit sur place et <b>son atterrissage fait naitre une entite</b>.
    ///
    /// <para>
    /// Trois temps sur le compte a rebours <c>+0x43</c>, et non sur <c>+0x10</c> comme les deux
    /// autres gestes. <b>2</b> — a la huitieme image elle decolle, <c>+0x54 = 0x64000</c>, la plus
    /// forte impulsion verticale du depot. <b>1</b> — elle attend d'avoir retrouve le sol (le bit 0
    /// retombe), puis fait naitre un sous-type <c>0xc0 + repertoire[2]</c> et s'arme une
    /// recuperation de <c>0x28</c> images dans <c>+0x10</c>. <b>0</b> — cette recuperation s'ecoule
    /// et ferme le geste.
    /// </para>
    /// <para>
    /// <b>Le sous-type est pris dans la table PAR SOUS-ZONE</b> (<c>&gt;= 0xc0</c>, donc
    /// <c>0x0056adc8</c>), au <b>troisieme</b> octet du premier bloc du repertoire de zone — le
    /// Mechant ours prend le <b>dixieme</b> pour son souffle, et l'entite de sillage de
    /// <see cref="TickSceneEntityAi_0049A980"/> le <b>deuxieme</b>. Le premier bloc du repertoire
    /// est donc un petit catalogue d'effets propres a la carte, adresse par rang.
    /// </para>
    /// </summary>
    private static void StompGesture_004A5580(byte* e, uint flags)
    {
        switch (e[0x43])
        {
            // ---------------------------------------------------------- 2 : decoller
            case 2:
                if (*(short*)(e + 0x1a) != 8) return;

                flags &= 0xfeffffff;
                *(int*)(e + 0x54) = 0x64000;
                flags |= 1;
                e[0x43]--;
                *(uint*)e = flags;
                return;

            // ---------------------------------------------------------- 1 : retomber
            case 1:
                if ((flags & 1) != 0) return;               // encore en l'air

                {
                    byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
                    int subtype = (*(byte**)block)[2] + 0xc0;
                    SpawnEffectEntity(e, (byte)subtype, 0xff, -1);
                }

                e[0x43]--;
                *(ushort*)(e + 0x10) = 0x28;
                return;

            // ---------------------------------------------------------- 0 : se relever
            case 0:
                *(short*)(e + 0x10) -= 1;
                if (*(short*)(e + 0x10) == 0) *(uint*)e = flags & 0xfffffffd;
                return;

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a5cb1..0x004a5ee2</c> — <b>le troisieme temps</b> : quelle posture, ou quel
    /// geste, pour la suite.
    ///
    /// <para>
    /// <b>Deux plages seulement</b>, separees a <c>0x12c</c>, la ou toute la famille en a trois.
    /// Et ses cones d'engagement sont, eux aussi, <b>des constantes</b> : un huitieme de tour pour
    /// le coup de patte, un quart de tour pour la prise et pour le sort.
    /// </para>
    /// <para>
    /// <b>Le lointain lui donne quatre choses a faire</b> — empoigner, sauter, incanter, ou errer —
    /// contre deux au corps a corps. C'est l'inverse de toutes ses voisines : <b>plus on s'eloigne
    /// d'un Behemoth, plus il a de moyens</b>.
    /// </para>
    /// </summary>
    private static void ReplanCastingBeastAction_004A54E0(byte* e, int boldness)
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
                // Un huitieme de tour de part et d'autre, et une portee ferme a 0xc8 — sans le
                // tirage que ses deux voisines ajoutent.
                if (((heading + 0x100) & 0xfff) > 0x200 || distance >= 0xc8)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: LAB_004a5d41 — armer LE COUP DE PATTE.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x4b)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0xc8;
                e[0x43] = BehemothDelay();
                return;
            }

            WanderBehemoth(e, boldness, heading);
            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            // Ghidra: LAB_004a5dbe — armer LA PRISE, si la cible est dans un quart de tour.
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                HesitateSceneEntity(e);
                return;
            }

            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 4;
            e[0x42] = 1;
            *(ushort*)(e + 0x10) = 2;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (far < 0x32)
        {
            // Ghidra: LAB_004a5df7 — armer LE SAUT. Aucune condition de cap : il frappe autour.
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 5;
            e[0x42] = 2;
            e[0x43] = 2;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (far < 0x46)
        {
            // Ghidra: LAB_004a5e38 — armer L'INCANTATION : le bit 0x800 bascule la machine a
            // postures, et +0x10 y demarre a 3.
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0xc8;
                e[0x43] = BehemothDelay();
                return;
            }

            *(int*)(e + 0x18) = 6;
            *(uint*)e |= 0x1000804;
            *(ushort*)(e + 0x10) = 3;
            return;
        }

        if (far < 0x5a)
        {
            WanderBehemoth(e, boldness, heading);
            return;
        }

        // Ghidra: LAB_004a5ecc — le repos.
        e[0x42] = 0;
        e[0x43] = BehemothDelay();
    }

    /// <summary>Ghidra: LAB_004a5e80 — <b>l'errance du Behemoth</b> : posture 3, un cap tire dans
    /// une fourchette large de <c>(hardiesse + 0x80) * 16</c> centree sur la cible, plus un
    /// demi-tour. <b>Elle s'eloigne toujours</b> : les deux sites qui errent portent le meme biais
    /// <c>0x800</c>, aucun ne la fait avancer.
    ///
    /// <para>C'est <see cref="WanderSceneEntity"/> a une chose pres — le compte a rebours vient de
    /// <see cref="BehemothDelay"/>, qui ignore la timidite.</para></summary>
    private static void WanderBehemoth(byte* e, int boldness, int heading)
    {
        e[0x42] = 3;
        e[0x43] = BehemothDelay();

        int spread = (boldness + 0x80) << 4;
        int offset = CrtRand() % spread - (spread >> 1);

        *(ushort*)(e + 0x10) = (ushort)(offset + heading + *(short*)(e + 0x32) + 0x800);
    }
}
