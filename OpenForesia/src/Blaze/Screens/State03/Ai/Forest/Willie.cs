// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// WILLIE — 0x004A35B0, sous-type 15 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// La sœur de la Coccinelle : meme vol, meme charpente, a 417 lignes d'ecart dans le crible.
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
    // 0x004A35B0 — WILLIE, sous-type 15 du bloc B de la foret (sous-zones 6 a 8). Transcrite le
    // 2026-09-01 au desassembleur (Legacy/Raw/Blaze/FUN_004A35B0.asm, 1 636 octets, absente de
    // Blaze.c). Trente-deux etiquettes, table de sauts a 0x004A3C14.
    //
    // LE CRIBLE L'ANNONCAIT A 417 LIGNES DE LA COCCINELLE 0x0049A360, ET C'EST BIEN SA SŒUR : meme
    // queue de vol (FUN_0049a8e0, le bloc de battement, FUN_004a1cf0), meme cone hardiesse/4 + 0x38,
    // meme distance tenue 0x12c, meme delai FlyingEntityDelay, et une replanification qui est la
    // sienne mot pour mot a un champ pres — +0x10 au lieu de +0x12 pour le cap d'errance.
    //
    // CE QUI LUI APPARTIENT TIENT EN QUATRE POINTS, ET LES TROIS PREMIERS SE VOIENT A L'ECRAN :
    //
    //   - ELLE ENFLE POUR FRAPPER. Les trois echelles +0x28/+0x2a/+0x2c convergent par quarts vers
    //     0x3000 — TROIS FOIS sa taille — des que le compte a rebours du geste passe sous 8, et
    //     vers 0x1000 avant cela. Aucune des dix-huit routines precedentes ne touche a son echelle
    //     pour se battre.
    //
    //   - SA SONDE EST CENTREE SUR ELLE-MEME. probe[1] vaut -0x3c, le seul decalage avant NEGATIF
    //     de tout le depot : le quadrilatere de frappe s'etend de -0x3c a +0x3c le long de son cap,
    //     donc il l'englobe. C'est le gabarit d'une chose qui blesse par contact, pas d'un coup
    //     porte — et il va avec l'enflure.
    //
    //   - ELLE PULSE. Hors geste, les memes trois echelles convergent vers sin(+0x14)/4 + 0x1000, et
    //     le mot de couleur +0x38 recoit un gris qui oscille autour de 0x80 sur la MEME phase que
    //     sa hauteur de vol. Elle respire.
    //
    //   - ET ELLE NE CHANGE JAMAIS D'ANIMATION. Aucune de ses quatre postures n'ecrit +0x18 ; toutes
    //     ses sœurs y posent 0 ou 1. Elle n'a qu'un seul clip.
    //
    // SON INTERRUPTION EST CONDITIONNELLE, ce qu'aucune autre n'a : au-dessus de 0x0a dans +0x16
    // elle prend l'interruption TERRESTRE (FUN_004e3770), en dessous la VOLANTE (FUN_004a1cf0), et
    // l'effacement des drapeaux de vue n'accompagne que la seconde.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004a35b0</c> — <b>l'intelligence de Willie</b>, sous-type 15 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret. <b>Quatrieme bete
    /// volante</b> du depot, apres la Coccinelle, l'Abeille et l'abeille tueuse.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x004A3C14</c>, et <b>un seul geste</b> — pas
    /// d'aiguillage sur <c>+0x42</c> comme chez les lourdes. Le geste balaie le groupe a
    /// <b>chaque</b> image, comme celui de la Coccinelle.
    /// </para>
    /// <para>
    /// <b>Son interruption est conditionnelle</b> : <c>+0x16</c> au-dessus de <c>0x0a</c> lui donne
    /// l'interruption terrestre, en dessous la volante. C'est la seule routine du depot qui choisisse
    /// entre les deux, et l'effacement des drapeaux de vue <c>0xdfffd8ff</c> n'accompagne que la
    /// branche volante.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A35B0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x3c;      // le seul decalage avant NEGATIF du depot : la sonde l'englobe
        probe[2] = 0x3c;
        probe[3] = 0x96;
        probe[4] = 0;
        probe[5] = 0x78;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideSwellingEntityAction_004A35B0(e, probe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        HoverOnWings(e);
        PulseSceneEntityColour_004A39D0(e);

        // Ghidra: LAB_004a39fb — le choix d'interruption, et le masque avec lui.
        if (*(short*)(e + 0x16) >= 0x0a)
        {
            InterruptSceneEntity_004E3770((uint*)e, stats);
        }
        else
        {
            *(uint*)(e + 0x90) &= 0xdfffd8ff;
            InterruptFlyingEntity_004A1CF0((uint*)e, stats);
        }

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSwellingEntityAction_004A35B0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a3638..0x004a394c</c> — <b>le premier temps</b> de Willie.
    ///
    /// <para>
    /// <b>Les quatre postures convergent sur la pulsation</b> (<c>LAB_004a3901</c>), y compris une
    /// posture hors bornes. Les deux gardes qui la precedent — <c>0x10</c> et <c>0x804</c> —
    /// sautent en revanche par-dessus : une entite figee ou en recul ne respire pas.
    /// </para>
    /// </summary>
    private static void DecideSwellingEntityAction_004A35B0(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, uint traits)
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

            // Aucun controle d'image du geste : le balayage a lieu a CHAQUE image, comme chez la
            // Coccinelle. Avec une sonde centree sur le corps, c'est un contact continu.
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }

            RampFlyingEntityPush(e, (boldness + 0x280) << 7);

            // Ghidra: LAB_004a36c5 — L'ENFLURE. La comparaison est NON SIGNEE (`jb`), et le seuil
            // est sur le compte a rebours : elle grossit pendant les huit dernieres images.
            byte countdown = e[0x43];
            ConvergeSceneEntityScale(e, countdown >= 8 ? 0x3000 : 0x1000);

            if (countdown == 0) return;
            if (--e[0x43] != 0) return;

            *(uint*)e &= 0xfffffffd;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held <= 3)
        {
            // Le cone de virage de la Coccinelle, au terme pres.
            int cone = (boldness >> 2) + 0x38;

            switch (held)
            {
                // ------------------------------------------------------ 0 : attendre
                case 0:
                    if (distance < 0x12c)
                    {
                        e[0x43] = 0;
                        break;
                    }

                    if (e[0x43] != 0) e[0x43]--;
                    DampFlyingEntityPush(e);
                    break;

                // ------------------------------------------------------ 1 : se tourner
                case 1:
                    if (TurnSceneEntityTowardTarget(e, cone, heading)) e[0x43] = 0;
                    DampFlyingEntityPush(e);
                    break;

                // ------------------------------------------------------ 2 : s'approcher
                case 2:
                    if (distance < 0x12c)
                    {
                        e[0x43] = 0;
                        break;
                    }

                    if (e[0x43] != 0) e[0x43]--;

                    TurnSceneEntityTowardTarget(e, cone, heading);
                    RampFlyingEntityPush(e, (boldness + 0x1a0) << 7);
                    break;

                // ------------------------------------------------------ 3 : errer
                default:
                {
                    if (e[0x43] != 0) e[0x43]--;

                    RampFlyingEntityPush(e, (boldness + 0x140) << 7);

                    // Le cap vise est en +0x10, la ou la Coccinelle emploie +0x12.
                    int bearing = (*(ushort*)(e + 0x10) - *(ushort*)(e + 0x32)) & 0xfff;

                    if (((bearing + 0x100) & 0xfff) > 0x200)
                    {
                        int step = bearing >= 0x800
                            ? -0x38 - (boldness >> 2)
                            : (boldness >> 2) + 0x38;

                        *(ushort*)(e + 0x32) += (ushort)step;
                    }

                    break;
                }
            }
        }

        // Ghidra: LAB_004a3901 — la pulsation, sur laquelle les quatre postures convergent.
        ConvergeSceneEntityScale(
            e, ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) >> 2) + 0x1000);
    }

    /// <summary>Ghidra: le motif <c>edi = cible - echelle ; sar edi,2 ; add edi,echelle</c>, emis
    /// neuf fois — <b>les trois echelles de l'entite convergent d'un quart vers une cible
    /// commune</b>. <c>0x1000</c> est la taille normale ; le geste vise <c>0x3000</c>, soit le
    /// triple.</summary>
    private static void ConvergeSceneEntityScale(byte* e, int target)
    {
        for (int axis = 0; axis < 3; axis++)
        {
            short* scale = (short*)(e + 0x28 + axis * 2);
            *scale = (short)(*scale + ((target - *scale) >> 2));
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a39d0..0x004a39fa</c> — <b>la pulsation de couleur</b> : le mot <c>+0x38</c>
    /// recoit un gris uniforme qui oscille autour de <c>0x80</c>.
    ///
    /// <para>
    /// La phase est <c>+0x14</c>, <b>celle-la meme qui fait osciller sa hauteur de vol</b> : la
    /// couleur, la taille et l'altitude battent ensemble. L'amplitude est <c>3 * sinus / 2</c>
    /// ramene sur six bits, donc le gris court de <c>0x20</c> a <c>0xe0</c>.
    /// </para>
    /// </summary>
    private static void PulseSceneEntityColour_004A39D0(byte* e)
    {
        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14));
        uint level = (uint)((((sin >> 1) + sin) >> 6) + 0x80) & 0xff;

        *(uint*)(e + 0x38) = level << 16 | level << 8 | level;
    }

    /// <summary>
    /// Ghidra: <c>0x004a3a55..0x004a3c13</c> — <b>le troisieme temps</b> de Willie.
    ///
    /// <para>
    /// <b>C'est la replanification de la Coccinelle</b>
    /// (<see cref="ReplanFlyingEntityAction_0049A360"/>), decision pour decision et seuil pour
    /// seuil : les memes plages <c>0x12c</c> et <c>0x258</c>, la meme chance sur <c>0x3c</c>
    /// d'engager, le meme seuil <c>0xfa</c> <b>diminue</b> d'un tirage, le meme engagement qui ne
    /// pose que le bit 2 et un rebours de <c>timidite/8 + 0x10</c>, et le meme tirage jete de la
    /// plage moyenne.
    /// </para>
    /// <para>
    /// <b>Une seule difference : le cap d'errance va dans <c>+0x10</c> et non <c>+0x12</c></b>, ce
    /// que la posture 3 relit au meme endroit. Les deux corps ne sont pas identiques a l'octet — le
    /// compilateur n'a pas alloue les memes registres et Willie a recopie en clair le bloc de delai
    /// que la Coccinelle partage —, donc ils sont transcrits separement plutot que fusionnes.
    /// </para>
    /// </summary>
    private static void ReplanSwellingEntityAction_004A35B0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0x12c)
        {
            if (CrtRand() % 100 >= 0x3c)
            {
                e[0x42] = 3;
                e[0x43] = FlyingEntityDelay(timid);

                int spread = (boldness + 0x80) << 4;
                int offset = CrtRand() % spread - (spread >> 1);

                *(short*)(e + 0x10) = (short)(offset + heading + *(short*)(e + 0x32));
                return;
            }

            if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (distance >= 0xfa - CrtRand() % (timid * 2 + 1))
            {
                HesitateSceneEntity(e);
                return;
            }

            // L'engagement ne touche ni a +0x42 ni a +0x18 : elle n'a qu'un clip, et sa posture
            // d'avant reprend a la fin du geste.
            *(uint*)e |= 2;
            e[0x43] = (byte)((timid >> 3) + 0x10);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x258)
        {
            CrtRand();                       // Ghidra: `call 00547140h` dont le resultat est jete
            e[0x42] = 2;
            e[0x43] = FlyingEntityDelay(timid);
            return;
        }

        // ================================================================ l'errance
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            e[0x42] = 0;
            e[0x43] = FlyingEntityDelay(timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        e[0x42] = 3;
        e[0x43] = FlyingEntityDelay(timid);
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
