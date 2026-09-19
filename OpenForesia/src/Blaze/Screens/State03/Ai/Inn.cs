// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'AUBERGE — 0x00486EE0 et 0x00487260, zone 0 - les deux seules routines de la zone.
//
// 0x00487260 est le QUATRIEME LANCEUR DE SORTS du depot, et le seul qui choisit ses sorts
// par le RELEVE DE PLACEMENT : deux exemplaires de la meme bete sur la meme carte peuvent
// n'avoir pas les memes sorts. 1 904 octets, absents de `Blaze.c`.
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

    /// <summary>
    /// Ghidra: FUN_00486ee0 @ 0x00486ee0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (881 octets, 0x00486ee0..0x00487250, releve dans
    /// <c>Legacy/Raw/Blaze/FUN_00486EE0.asm</c>). C'est le sous-type <b>0</b> de la table
    /// <c>0x005ecaa0</c> — la <b>bestiole errante</b> : elle regarde qui du groupe la voit, et sinon
    /// se promene au hasard.
    ///
    /// <para><b>Premiere moitie — la detection.</b> Pour chacun des membres vivants, elle mesure la
    /// distance horizontale (racine de la somme des carres, ordinal 442), l'ecart vertical et
    /// l'angle relatif (ordinal 457) ; le membre « voit » la bestiole si la distance est dans
    /// <c>[0x3c, 0x78]</c>, l'ecart vertical dans <c>[-0x64, +0x64]</c> et l'angle dans un cone de
    /// <c>0x400</c> unites de part et d'autre. Le resultat est un <b>bit par membre</b> dans
    /// <c>entite[9]</c>, dont le rang est le type d'emplacement du membre. Quand le masque passe de
    /// vide a non vide, le script d'entite <c>+0x16</c> demarre.</para>
    ///
    /// <para><b>Seconde moitie — la promenade.</b> <c>entite[0x18]</c> alterne entre 0 (arret) et 1
    /// (marche) ; en marche, la position avance de cinq fois le sinus et le cosinus du cap
    /// <c>+0x32</c>. Le minuteur <c>+0x40</c> decompte, et a zero la bestiole change d'etat : elle
    /// <b>essaie</b> un cap et une vitesse tires au hasard sur une <b>copie</b> de son
    /// enregistrement (0x005e9a40), la fait avancer, et ne l'adopte que si
    /// <see cref="FinalizeEntityPlacement2"/> ne signale pas les bits <c>0x882</c> — un mur, un
    /// vide, ou le bord du maillage.</para>
    /// </summary>
    private static void EntityHandler_00486EE0(uint* entity)
    {
        byte* e = (byte*)entity;

        uint flags = entity[0];
        if ((flags & 0x4000) != 0) return;

        if ((flags & 0x8000000) == 0) AdvanceEntityAnimation_004DB5A0(entity);

        // -------------------------------------------------------------- la detection par membre
        int detectionBefore = e[9];

        for (int slot = 0; slot < s_PartySize_005e9020; slot++)
        {
            byte* member = Inp.s_SlotFlags_005e93c0 + slot * 0x9c;
            byte bit = (byte)(1 << member[8]);

            uint memberFlags = *(uint*)member;
            bool eligible = (memberFlags & 0x80000000) != 0
                         && (memberFlags & 0x50010007) == 0
                         && *(short*)(member + 0x12) == 0;

            if (!eligible)
            {
                e[9] &= (byte)~bit;
                continue;
            }

            int dx = (*(int*)(e + 0x1c) - *(int*)(member + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(member + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(member + 0x24)) >> 0xc;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            int bearing = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(member + 0x32)) & 0xfff;

            if (distance < 0x3c || distance > 0x78
                || (uint)(dy + 0x64) > 0xc8
                || ((bearing + 0x200) & 0xfff) > 0x400)
            {
                e[9] &= (byte)~bit;
                continue;
            }

            // Le meme angle, pris dans l'autre sens : du point de vue de la bestiole vers le
            // membre. Quand elle est deja alertee (+0x43), elle en profite pour se tourner.
            int backX = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int backZ = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int facing = (Winsys3d.Ordinal_457(backX, backZ) - *(short*)(e + 0x32)) & 0xfff;

            if (e[0x43] != 0)
            {
                *(short*)(e + 0x32) = (short)Winsys3d.Ordinal_457(backX, backZ);
            }

            if (((facing + 0x200) & 0xfff) > 0x400 && e[0x43] == 0) continue;

            e[9] |= bit;
            *(byte**)(e + 0x6c) = member;
        }

        // Le masque vient de passer de vide a non vide : le script de rencontre demarre.
        if (detectionBefore == 0 && e[9] != 0)
        {
            RunEntityScript(s_Unknown_005e97cc, *(byte**)(e + 0x6c), (uint)*(short*)(e + 0x16));
        }

        if (*(short*)(e + 0x10) == 0 && *(short*)(e + 0x12) == 0 && *(short*)(e + 0x14) != 0)
        {
            *(short*)(e + 0x14) = 0;
            e[0x43] = 1;
        }

        // -------------------------------------------------------------- la promenade
        if (e[0x43] != 0)
        {
            if (*(short*)(e + 0x18) > 1)
            {
                // Un dword : l'original efface l'etat **et** le compteur d'images d'un coup.
                *(int*)(e + 0x18) = 0;
                e[0x40] = (byte)((CrtRand() & 3) + 3);
            }
        }
        else
        {
            if (*(short*)(e + 0x18) != 0) *(int*)(e + 0x18) = 0;
            return;
        }


        short walkState = *(short*)(e + 0x18);
        if (walkState == 1)
        {
            // En marche : cinq unites par image dans la direction du cap.
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 5;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 5;
            e[0x40]--;
        }
        else if (walkState == 0 && *(short*)(e + 0x1a) == 0)
        {
            e[0x40]--;
        }

        FinalizeEntityPlacement2(entity);
        *(int*)(e + 0x20) = *(int*)(e + 0x64);

        if ((*(uint*)(e + 0x90) & 0x800) != 0) e[0x40] = 0;

        byte timer = e[0x40];
        if (timer != 0 && timer != 0xff) return;

        // Le minuteur est echu : on bascule marche/arret.
        *(short*)(e + 0x18) ^= 1;
        short newState = *(short*)(e + 0x18);
        *(short*)(e + 0x1a) = 0;

        if (newState == 0)
        {
            // On s'arrete : trois a six images d'immobilite.
            e[0x40] = (byte)((CrtRand() & 3) + 3);
            return;
        }

        // On repart : le cap et la vitesse sont tires au hasard, mais **essayes sur une copie**
        // de l'enregistrement avant d'etre adoptes.
        byte* trial = Mem.Arena(0x005e9a40);
        Mem.CopyDwords(entity, (uint*)trial, 0x27);

        *(short*)(trial + 0x32) = (short)CrtRand();
        trial[0x40] = (byte)((CrtRand() & 0x1f) + 0x28);

        int trialHeading = *(short*)(trial + 0x32);
        int speed = trial[0x40];

        *(int*)(trial + 0x1c) += (int)Winsys3d.GetSinTableEntry(trialHeading) * speed * 5;
        *(int*)(trial + 0x24) += Winsys3d.GetCosTableEntry(trialHeading) * speed * 5;

        FinalizeEntityPlacement2((uint*)trial);

        if ((*(uint*)(trial + 0x90) & 0x882) != 0)
        {
            // Refuse : mur, vide ou bord du maillage. On reste immobile une image.
            *(int*)(e + 0x18) = 0;
            e[0x40] = 1;
            return;
        }

        *(short*)(e + 0x32) = *(short*)(trial + 0x32);
        e[0x40] = trial[0x40];
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552868</c> — <b>la table de sorts de <c>0x00487260</c></b> : huit
    /// rangees de trois fiches, bornee par la sonde de geste qui suit a <c>0x00552898</c>
    /// (<c>0000 1e00 4600 9600 00ff 5000</c>, la forme des six <c>short</c> qu'une routine pose sur
    /// sa pile). Chaque mot porte la <b>famille dans l'octet haut</b> et l'<b>indice dans l'octet
    /// bas</b>, comme le <c>+0x06</c> ou il est range.
    ///
    /// <para>
    /// <b>Les quatre dernieres rangees repetent les quatre premieres</b>, octet pour octet. Ce
    /// n'est pas une lecture trop longue : le motif de 0x18 octets apparait exactement deux fois
    /// avant que la donnee ne change de nature. Transcrit tel quel — c'est la donnee de 1998 qui
    /// est redondante, et le corps peut adresser les huit rangees.
    /// </para>
    /// <para>
    /// Comparer avec <see cref="s_BehemothSpells_00552A30"/>, qui est une <b>seule</b> rangee de
    /// trois : la meme mecanique d'incantation, mais la ou le Behemoth tire dans une liste fixe,
    /// celle-ci <b>choisit d'abord la rangee par <c>+0x14</c></b>, c'est-a-dire par l'octet 6 du
    /// <b>relevé de placement</b> que <c>WorldMapScript</c> recopie a la naissance. Deux exemplaires
    /// de la meme bete posees sur la meme carte peuvent donc n'avoir pas les memes sorts.
    /// </para>
    /// </summary>
    private static readonly ushort[] s_Spells_00552868 =
    [
        0x0000, 0x0003, 0x0008,   // 0x00552868
        0x000a, 0x0010, 0x020c,   // 0x0055286e
        0x0003, 0x0008, 0x0200,   // 0x00552874
        0x0008, 0x0200, 0x0101,   // 0x0055287a
        0x0000, 0x0003, 0x0008,   // 0x00552880 — la repetition commence ici
        0x000a, 0x0010, 0x020c,   // 0x00552886
        0x0003, 0x0008, 0x0200,   // 0x0055288c
        0x0008, 0x0200, 0x0101,   // 0x00552892
    ];

    /// <summary>
    /// Ghidra: <c>0x00487260</c> — <b>l'intelligence du sous-type 1 de l'auberge</b>, la seconde et
    /// derniere routine de la zone 0 apres <see cref="EntityHandler_00486EE0"/>. 1904 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00487260.asm</c>.
    ///
    /// <para>
    /// <b>C'est un LANCEUR DE SORTS, et le quatrieme du depot</b> apres le Behemoth, le Trent et le
    /// Varan. Elle porte le mecanisme d'incantation du Behemoth <b>au complet</b> : meme bit
    /// <c>0x800</c> qui bascule sur une seconde machine a etats logee dans <c>+0x10</c>, memes
    /// quatre etats dans le meme ordre, meme entite de lancement <c>0xbd</c>, meme masque de traits
    /// <c>0x400003e8</c> qui l'annule net, et meme durée lue dans le <c>+0x13</c> de la fiche de
    /// sort. C'est le <b>troisieme</b> emploi de ce bloc : il ne se reecrit pas, il se recopie.
    /// </para>
    /// <para>
    /// <b>Ce qui lui appartient : ses sorts sont choisis par le PLACEMENT et non par l'espece.</b>
    /// La table <see cref="s_Spells_00552868"/> a huit rangees ; la rangee vient de <c>+0x14</c>,
    /// que la naissance recopie de l'octet 6 du relevé de placement, et la fiche de la rangee est
    /// tiree a trois. Aucune autre bete du depot ne fait dependre son arsenal de l'endroit ou elle
    /// a ete posee.
    /// </para>
    /// <para>
    /// <b>Elle n'a qu'un seul geste</b> — la sonde <c>{0, 0, 0x32, 0x64, 0, 0x5a}</c>, croisee a
    /// <b>chaque</b> image a partir de la huitieme, sans posture pour le nuancer : la ou le Goblin
    /// choisit son coup selon <c>+0x42</c>, celle-ci frappe et c'est tout. Sa sonde part du sol
    /// (<c>probe[1] = 0</c>) la ou toute la famille commence a <c>0x1e</c> : <b>elle atteint ce qui
    /// est a terre.</b>
    /// </para>
    /// <para>
    /// <b>Et ses quatre postures ne connaissent ni bond ni charge.</b> La posture 3 marche — en
    /// integrant la position a la main, comme le Goblin, mais <b>sans multiplier par le pas</b> :
    /// son allure ne depend pas de sa vitesse d'animation. Il n'y a aucune posture 4.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_00487260(byte* record)
        => TickSpellcasterAi_00487260(record, s_Spells_00552868, 0);

    /// <summary>
    /// Ghidra: <c>0x00488210</c> — <b>la meme routine dans la mine abandonnee</b>, sous-zones 0 a 2,
    /// table <c>0x0056ad48</c>. Absente de <c>Blaze.c</c>. Le desassemblage, adresses de saut
    /// ramenees, ne differe de celui de <c>0x00487260</c> que par une ligne :
    /// <c>mov ax,[ecx*2+552880h]</c> au lieu de <c>552868h</c>.
    ///
    /// <para>
    /// <b>Et <c>0x00552880</c> est la seconde moitie de <see cref="s_Spells_00552868"/></b>, celle que
    /// son resume decrivait comme une repetition : les quatre rangees de la mine. La table de
    /// l'auberge n'a donc vraisemblablement que QUATRE rangees, et la suite est la table de la
    /// mine, octet pour octet la meme — et <c>0x00552978</c>, celle du Goblin-sorcier de la foret,
    /// porte encore ces quatre rangees. La mine lit la meme donnee a partir de la rangee 4 ; une
    /// rangee <c>+0x14</c> au-dela de 3 y leve, la ou le binaire lirait la sonde qui suit.
    /// </para>
    /// <para>
    /// <b>Et le Goblin-sorcier est lui aussi ce corps</b> : <c>0x0049E190</c> ne differe de
    /// <c>0x00487260</c> que par l'adresse de sa table (<c>552978h</c>). Il a ete transcrit a part
    /// (<see cref="TickSceneEntityAi_0049E190"/>) avant que la comparaison ne soit faite ; les deux
    /// transcriptions restent a rapprocher.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_00488210(byte* record)
        => TickSpellcasterAi_00487260(record, s_Spells_00552868, 4 * 3);   // Ghidra: &DAT_00552880

    /// <summary>
    /// Ghidra: <c>0x00552a38</c> — <b>la table de sorts du Goblin-sorcier de la vieille tour</b>,
    /// quatre rangees de trois, relevees dans <c>.data</c>. <b>Memes octets</b> que les quatre
    /// premieres rangees de <see cref="s_Spells_00552868"/>, a une autre adresse. Bornee en bas par
    /// la table du Behemoth (<c>0x00552a30</c>, trois mots) et ses deux octets nuls ; les
    /// quatre rangees finissent a <c>0x00552a50</c>.
    /// </summary>
    private static readonly ushort[] s_Spells_00552A38 =
    [
        0x0000, 0x0003, 0x0008,   // 0x00552a38
        0x000a, 0x0010, 0x020c,   // 0x00552a3e
        0x0003, 0x0008, 0x0200,   // 0x00552a44
        0x0008, 0x0200, 0x0101,   // 0x00552a4a
    ];

    /// <summary>
    /// Ghidra: <c>0x004a6a00</c> — <b>le Goblin-sorcier de la vieille tour</b>, creneau 1 de la
    /// table <c>0x0056ad48</c> dans les sous-zones 0, 1, 2 et 4 (<c>especes.csv</c> x
    /// <c>modeles.csv</c>). Absente de <c>Blaze.c</c>. Le desassemblage STRICT ne differe de celui de
    /// <see cref="TickSceneEntityAi_00487260"/> que par une ligne :
    /// <c>mov ax,[ecx*2+552A38h]</c> au lieu de <c>552868h</c> — la meme mecanique d'incantation,
    /// sur <see cref="s_Spells_00552A38"/>.
    /// </summary>
    private static void TickSceneEntityAi_004A6A00(byte* record)
        => TickSpellcasterAi_00487260(record, s_Spells_00552A38, 0);   // Ghidra: &DAT_00552a38

    /// <summary>
    /// Ghidra: <c>0x00553020</c> — <b>la table de sorts de la copie de la grotte scellee</b>,
    /// quatre rangees de trois, relevees dans <c>.data</c> le 2026-09-19. Memes octets que les
    /// quatre premieres rangees de <see cref="s_Spells_00552868"/> ; a <c>0x00553038</c> la donnee
    /// change de nature (<c>0000 0000 3c00 9600 ...</c>, une sonde). Comme pour
    /// <see cref="s_Spells_00552A38"/>, une fiche de placement dont l'octet 6 depasserait 3 ferait
    /// lire au binaire les octets qui suivent — et ici, hors du tableau.
    /// </summary>
    private static readonly ushort[] s_Spells_00553020 =
    [
        0x0000, 0x0003, 0x0008,   // 0x00553020
        0x000a, 0x0010, 0x020c,   // 0x00553026
        0x0003, 0x0008, 0x0200,   // 0x0055302c
        0x0008, 0x0200, 0x0101,   // 0x00553032
    ];

    /// <summary>
    /// Ghidra: <c>0x004ffc50</c> — <b>la meme routine dans la grotte scellee</b>, sous-zone 1.
    /// Absente de <c>Blaze.c</c>. Le desassemblage ne differe de celui de
    /// <see cref="TickSceneEntityAi_00487260"/> que par l'adresse de la table
    /// (<c>mov ax,[ecx*2+553020h]</c>) et par ses deux tables de sauts, decalees de
    /// <c>0x789f0</c> comme le corps.
    /// </summary>
    private static void TickSceneEntityAi_004FFC50(byte* record)
        => TickSpellcasterAi_00487260(record, s_Spells_00553020, 0);   // Ghidra: &DAT_00553020

    /// <summary>
    /// Le corps partage par <see cref="TickSceneEntityAi_00487260"/> (auberge),
    /// <see cref="TickSceneEntityAi_00488210"/> (mine) et <see cref="TickSceneEntityAi_004A6A00"/>
    /// (vieille tour). <paramref name="spells"/> est la table de sorts lue par l'emission, et
    /// <paramref name="spellBase"/> la position de son debut dans cette table — la mine lit la
    /// seconde moitie de <see cref="s_Spells_00552868"/>, la tour sa propre table.
    /// </summary>
    private static void TickSpellcasterAi_00487260(byte* record, ushort[] spells, int spellBase)
    {
        byte* e = record;

        // Des trois blocs resolus, seul celui de statistiques sert — a la fin, en argument de
        // l'interruption.
        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        // La sonde du geste. Son plancher est a ZERO, la ou la famille le met a 0x1e.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x5a;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideSceneEntityAction_00487260(e, probe, boldness, distance, heading, speed, traits, spells, spellBase);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSceneEntityAction_00487260(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004872eb..0x004876cf</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_00487260"/>.
    ///
    /// <para>
    /// Trois regimes, testes dans cet ordre : le <b>geste</b> (bit 2), le <b>recul</b> (bit 0x10,
    /// qui ici ne fait <b>rien du tout</b> — pas meme se tourner, contrairement au Goblin), puis
    /// soit l'<b>incantation</b> (bits <c>0x804</c>) soit la <b>posture tenue</b>.
    /// </para>
    /// </summary>
    private static void DecideSceneEntityAction_00487260(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, uint traits, ushort[] spells, int spellBase)
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

            // Ghidra: LAB_00487318 — a partir de la huitieme image, et a CHAQUE image ensuite.
            if (*(short*)(e + 0x1a) >= 8)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                    *(uint*)victim |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }

                // Ghidra: LAB_0048736d — le mot d'etat est RELU : le coup a pu le changer.
                flags = *(uint*)e;
                if ((flags & 1) != 0) *(short*)(e + 0x1a) -= 1;
            }

            // Ghidra: LAB_00487377 — en l'air, l'image du geste ne progresse pas et rien ne finit.
            if ((flags & 1) != 0) return;

            // Ghidra: LAB_0048737f — la fin d'animation termine le geste ; sinon on se contente de
            // couper la poussee. Les deux chemins passent par LAB_004875a6.
            if ((flags & 0x2000000) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
            }

            *(int*)(e + 0x5c) = 0;
            return;
        }

        // ================================================================ le recul
        // Ghidra: LAB_0048739a — le bit 0x10 rend la main sans rien faire. La famille s'en sert
        // pour se tourner vers ce qui l'a touchee ; celle-ci encaisse sans reagir.
        if ((flags & 0x10) != 0) return;

        // ================================================================ l'incantation
        if ((flags & 0x804) != 0)
        {
            AdvanceIncantation_00487260(e, boldness, heading, traits, flags, spells, spellBase);
            return;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_004874ff — une bete en l'air ne change pas de posture.
        if ((flags & 1) != 0) return;

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

                // Ghidra: LAB_0048753f — le compteur a zero laisse la poussee telle quelle ; ce
                // n'est qu'en decomptant qu'elle est coupee. Les deux autres postures ne font pas
                // cette distinction.
                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                e[0x43]--;
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

                // Le compte a rebours ne repart que lorsque le cap est ATTEINT.
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

                // La distance tenue est dans +0x10, la meme case que l'incantation emploie comme
                // compteur d'etat et que la posture 3 remplit d'un cap : trois roles, jamais en
                // meme temps.
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

                // La POSITION avance directement, et — contrairement au Goblin — le pas n'est PAS
                // multiplie par la vitesse d'animation : l'allure est la meme quoi qu'il arrive.
                int walk = (boldness + 0x80) << 7;
                *(int*)(e + 0x5c) = walk;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * walk) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * walk) >> 0xc;

                *(int*)(e + 0x5c) = 0;
                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004873ad..0x004874fe</c> — <b>l'incantation</b>, la seconde machine a etats,
    /// logee dans <c>+0x10</c> et gardee par le bit <c>0x800</c>.
    ///
    /// <para>
    /// <b>C'est <see cref="AdvanceIncantation_004A5919"/> a quatre valeurs pres.</b> Meme masque de
    /// traits qui l'annule, memes quatre etats decomptes de 3 vers 0, meme lecture de la duree dans
    /// la fiche de sort. Les differences : l'animation de lancement est la <b>5</b> et non la 7, le
    /// coup part a l'image <b>7</b> et non a la 14, et la fiche est prise dans une table <b>a
    /// plusieurs rangees</b> (<see cref="s_Spells_00552868"/>), la rangee venant de <c>+0x14</c>.
    /// </para>
    /// <para>
    /// <b>Les quatre etats, dans l'ordre ou ils se jouent</b> — <c>3</c> choisir le sort et faire
    /// naitre l'entite de lancement ; <c>2</c> incanter, en se tournant, le temps que le
    /// <c>+0x43</c> de la fiche s'ecoule ; <c>1</c> jouer l'animation et lancer a l'image 7 ;
    /// <c>0</c> attendre la fin de l'animation, qui efface <c>0x800</c> et rend la main au regime
    /// ordinaire.
    /// </para>
    /// </summary>
    private static void AdvanceIncantation_00487260(
        byte* e, int boldness, int heading, uint traits, uint flags, ushort[] spells, int spellBase)
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
                if (*(ushort*)(e + 0x18) != 5)
                {
                    *(int*)(e + 0x18) = 5;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 7) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            // ---------------------------------------------------------- 2 : incanter
            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) -= 1;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;

            // ---------------------------------------------------------- 3 : choisir le sort
            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                // La rangee vient du placement (+0x14), la colonne d'un tirage a trois. L'original
                // ne borne PAS la rangee : un +0x14 au-dela de 7 y lirait la donnee qui suit. Ici
                // la lecture leve, ce qui est la bonne facon de decouvrir qu'une carte pose une
                // valeur inattendue — les huit rangees couvrent tout ce que la donnee declare.
                ushort sheet = spells[spellBase + *(short*)(e + 0x14) * 3 + CrtRand() % 3];
                *(ushort*)(e + 6) = sheet;

                // Ghidra: `mov edx,[edx+9Ch] ; mov ecx,[edx+ecx*4] ; mov dl,[ecx+eax+13h]` — le
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
    /// Ghidra: <c>0x00487721..0x004879cf</c> — <b>le troisieme temps</b> : quelle posture prendre
    /// pour la suite. Comme chez toute la famille, la distance et le cap sont <b>recalcules en deux
    /// dimensions</b>, la hauteur ignoree.
    ///
    /// <para>
    /// Trois plages, et dans chacune un tirage sur cent. <b>Sous <c>0xfa</c></b> : frapper (30 %),
    /// s'ecarter (30 %) ou <b>fuir</b> (40 %). <b>Sous <c>0x1f4</c></b> : charger (45 %), aller
    /// vers la cible (25 %), tenir la distance a 100 (20 %), attendre (10 %). <b>Au-dela</b> :
    /// charger (30 %), attendre (30 %), errer (40 %).
    /// </para>
    /// <para>
    /// <b>La fuite est ce qui la distingue.</b> Dans la plage de corps a corps, la sortie la plus
    /// probable pose la posture 3 sur un cap tire autour de la cible <b>plus un demi-tour</b>
    /// (<c>+0x800</c>) : elle s'eloigne. Aucune autre routine ecrite ne fait ca — c'est la
    /// contrepartie du lanceur de sorts, qui n'a rien a gagner a rester au contact.
    /// </para>
    /// <para>
    /// <b>La portee du coup est plus courte que celle du Goblin</b> : <c>rand % (timide * 2 + 1) +
    /// 0x50</c> contre <c>+ 100</c> chez lui, pour le meme cone.
    /// </para>
    /// <para>
    /// <b>La charge n'est pas une posture, c'est un etat pose a la main</b> : animation 4, image 0,
    /// bits <c>0x1000804</c> — dont le <c>0x800</c> qui bascule sur l'incantation — et
    /// <c>+0x10 = 3</c>, le premier etat de celle-ci. <b>« Charger » veut donc dire « incanter ».</b>
    /// </para>
    /// </summary>
    private static void ReplanSceneEntityAction_00487260(byte* e, int boldness, int timid)
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
                // Frapper : dans le cone ET assez pres, la portee s'elargissant avec la timidite.
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x50)
                {
                    uint flags = *(uint*)e;
                    *(int*)(e + 0x18) = 3;                 // animation 3, image 0
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x3c)
            {
                // Tenir la distance a 100 — la meme sortie que la plage moyenne.
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            // FUIR : le cap est tire autour de la cible, puis retourne d'un demi-tour.
            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
                {
                    BeginIncantation_00487260(e);
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x46)
            {
                // Aller vers la cible : le meme tirage de cap, sans le demi-tour.
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
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
                {
                    BeginIncantation_00487260(e);
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x3c)
            {
                RestSceneEntity(e, timid);
                return;
            }

            // Ghidra: LAB_004879a0 — errer : le cap est un tirage BRUT, sans rapport avec la cible.
            e[0x42] = 3;
            e[0x43] = SceneEntityDelay(timid);
            *(ushort*)(e + 0x10) = (ushort)CrtRand();
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_00487946</c> — <b>entrer en incantation</b>. Ce n'est pas une posture : c'est
    /// l'animation 4 posee a la main, les bits <c>0x1000804</c> leves — dont le <c>0x800</c> qui
    /// bascule <see cref="DecideSceneEntityAction_00487260"/> sur
    /// <see cref="AdvanceIncantation_00487260"/> — et <c>+0x10 = 3</c>, le premier des quatre etats.
    /// </summary>
    private static void BeginIncantation_00487260(byte* e)
    {
        *(int*)(e + 0x18) = 4;                 // animation 4, image 0
        *(short*)(e + 0x10) = 3;
        *(uint*)e |= 0x1000804;
    }
}
