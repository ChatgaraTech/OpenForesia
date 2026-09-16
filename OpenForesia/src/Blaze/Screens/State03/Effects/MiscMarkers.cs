// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// CINQ GESTIONNAIRES DE MARQUE SANS FAMILLE — 0x00476770, 0x004767E0, 0x004758C0, 0x00483450,
// 0x004834D0.
//
// Ils ne forment pas un sujet : ce sont les cinq corps que la lecture du fichier d'origine a
// laisses entre les marques d'etat et l'appariement de combat, et dont le role commun n'est pas
// etabli. Ils restent ensemble plutot que d'etre disperses dans des fichiers ou ils mentiraient
// sur leur voisinage - regle de routage (4) d'ARCHITECTURE.md.
//
// Quand le role de l'un d'eux sera etabli, il part rejoindre sa famille et ce fichier maigrit.
// S'il se vide, il disparait.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00476770 @ 0x00476770 — <b>l'entree 186 (<c>0xba</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>C'est une naissance, pas une vie</b> : elle regle une dizaine de champs, fait naitre une
    /// entite de sort <c>0xba</c> et <b>s'efface</b> (<c>*e = 0</c>, l'emplacement redevient libre).
    /// Le vrai comportement est celui de l'enfant, <see cref="EntityHandler_004767E0"/> — meme
    /// numero de sous-type, autre table de sauts (<c>0x00551f30</c>, celle ou
    /// <see cref="SpawnCastEntity_0047AAE0"/> depose).
    /// </para>
    /// <para>
    /// Elle part du cap deja range en <c>+0x32</c> et en tire une vitesse horizontale de <c>12</c>
    /// par <see cref="SetVectorFromAngle_00532B10"/>, met la verticale a zero et la gravite
    /// (<c>+0x9a</c>) a zero aussi : <b>ce qu'elle lance ne tombe pas</b>. La hauteur de depart
    /// vient de <see cref="StatusBubbleHeight_00538BC0"/>, arrondie au pair — la meme mesure que
    /// les bulles d'etat emploient pour se poser au-dessus d'une bete.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476770(byte* e)
    {
        *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);

        uint height = (uint)StatusBubbleHeight_00538BC0(*(uint**)(e + 0x6c));
        *(int*)(e + 0x20) += (int)((height & 0xfffffffe) * 0x800);

        e[0x04] = 0x2a;
        *(short*)(e + 0x36) = 4;
        *(uint*)(e + 0x38) = 0x808080;
        *(uint*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;

        SetVectorFromAngle_00532B10((int*)(e + 0x50), *(short*)(e + 0x32), 0xc);

        *(uint*)(e + 0x54) = 0;
        *(short*)(e + 0x9a) = 0;

        SpawnCastEntity_0047AAE0(e, 0xba, 1, 2);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004767e0</c> — <b>l'entree 186 (<c>0xba</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 192 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004767E0.asm</c>.
    ///
    /// <para>
    /// C'est l'enfant de <see cref="EntityHandler_00476770"/>, et c'est lui qui <b>porte le
    /// coup</b> : chaque image il avance par <see cref="IntegrateWithDrag_00535880"/>, grossit d'un
    /// quart de ce qui lui reste pour atteindre <c>0x1000</c>, et tant que sa teinte n'est pas
    /// descendue sous <c>0x202020</c> il balaie le camp d'en face par
    /// <see cref="SweepOpposingSideForMeleeHit_00474C70"/>. Une entite touchee prend les degats de
    /// <see cref="ResolveMeleeHit_00539B80"/> — porte au compte du <b>lanceur</b>, range en
    /// <c>+0x74</c> par le pere, et non de l'entite elle-meme —, recoit le bit <c>0x40</c> et
    /// <c>0x0c</c> en <c>+0x96</c>.
    /// </para>
    /// <para>
    /// <b>Sa duree ne se compte pas, elle se soustrait</b> : la teinte perd <c>0x080808</c> par
    /// image, et l'entite meurt quand elle vaut <b>exactement</b> zero — le test est sur le drapeau
    /// de zero du <c>add</c>, pas sur un signe. Partie de <c>0x808080</c> que le pere a posee, cela
    /// fait <c>0x808080 / 0x080808 = 16</c> images pile, dont les treize premieres frappent
    /// (<c>0x808080 - 12 × 0x080808 = 0x202020</c>, et le test est un <c>jb</c> non signe).
    /// <b>Une teinte de depart qui ne serait pas un multiple de <c>0x080808</c> ferait vivre
    /// l'entite pour toujours</b> : c'est une fragilite du binaire d'origine, transcrite telle
    /// quelle.
    /// </para>
    /// <para>
    /// La sonde passee au balayage est une pile de six mots, dont <b>deux seulement</b> sont
    /// ecrits par ce corps : <c>+0x04</c> (<c>sonde[2]</c>, la demi-largeur) recoit
    /// <c>echelle × 80 &gt;&gt; 12</c> et <c>+0x0a</c> (<c>sonde[5]</c>, la profondeur) recoit
    /// <c>echelle × 90 &gt;&gt; 12</c> — la portee grandit donc avec l'entite. Le <c>0x64</c> pose
    /// a la naissance en <c>+0x06</c> (<c>sonde[3]</c>) est la <b>tolerance en hauteur</b> et
    /// <b>survit</b> jusqu'a l'appel ; les trois autres mots restent a zero.
    /// </para>
    /// <para>
    /// <b>Les deux decalages se lisent sur la pile, pas dans le pseudo-code.</b> Les six mots sont
    /// ecrits alors que l'argument de <c>FUN_00535880</c> est encore empile, et l'adresse de la
    /// sonde n'est prise (<c>lea ecx,[esp+0Ch]</c>) qu'apres le <c>add esp,4</c> qui le depile —
    /// d'ou un ecart de quatre octets entre les deux vues. La premiere transcription avait lu les
    /// deux ecritures dans la vue d'avant l'appel et les avait rangees en <c>sonde[0]</c> et
    /// <c>sonde[3]</c> : la demi-largeur et la profondeur tombaient a zero, le quadrilatere de
    /// frappe se reduisait a un <b>point</b>, et l'onde traversait les ennemis sans les toucher.
    /// Corrige le 2026-08-30.
    /// </para>
    /// </summary>
    private static void EntityHandler_004767E0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0;

        IntegrateWithDrag_00535880(e);

        // Ghidra: `1000h - taille` puis `sar 2` — elle rejoint 0x1000 par quarts, sans jamais
        // l'atteindre tout a fait.
        int scale = *(short*)(e + 0x28);
        scale += (0x1000 - scale) >> 2;
        *(short*)(e + 0x28) = (short)scale;
        *(short*)(e + 0x2c) = (short)scale;

        // Ghidra: `mov [esp+16h],ax` puis `mov [esp+10h],dx`, dans la vue de pile d'APRES le
        // depilement de l'argument de FUN_00535880 — soit sonde[5] et sonde[2], pas sonde[3] et
        // sonde[0]. Voir l'en-tete.
        probe[5] = (short)(scale * 90 >> 0xc);
        probe[2] = (short)(scale * 80 >> 0xc);

        // Ghidra: `jb` — comparaison NON signee.
        if (*(uint*)(e + 0x38) >= 0x202020)
        {
            byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);

            // DAT_005de0b8 est le drapeau « quelque chose a ete touche » que le balayage pose ;
            // c'est lui, et non le retour, qui garde le dereferencement.
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

                *(uint*)victim |= 0x40;
                *(short*)(victim + 0x96) = 0x0c;
            }
        }

        // Ghidra: LAB_0047688a — `add eax,0FFF7F7F8h` puis `jne` : la mort tient au drapeau de zero.
        uint tint = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004758c0 @ 0x004758c0 — <b>l'entree 182 (<c>0xb6</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme forme que <see cref="EntityHandler_00476770"/> : une naissance qui s'efface. Elle
    /// depose une entite d'etat <c>0x3f</c> avec l'atlas <see cref="s_Descriptors_00551A60"/> et le
    /// parametre <c>9</c>, teinte de depart <c>0xf0f0f0</c> et echelle <c>0x1800</c> sur les deux
    /// axes.
    /// </para>
    /// <para>
    /// Elle range sa cible en <c>+0x78</c> et non en <c>+0x74</c> comme les deux autres — c'est le
    /// champ que l'enfant relit pour se placer.
    /// </para>
    /// </summary>
    private static void EntityHandler_004758C0(byte* e)
    {
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

        *(short*)(e + 0x0a) = 0;
        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(uint*)(e + 0x28) = 0x18001800;
        e[0x40] = 0;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), Descriptors00551A60(), 0x3f, 0, 9);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00483450 @ 0x00483450 — <b>l'entree 223 (<c>0xdf</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Troisieme naissance de la meme forme, et la seule dont <b>le sous-type de l'enfant n'est pas
    /// une constante</b> : il vaut <c>0xc0 + n</c>, ou <c>n</c> est un octet lu dans le repertoire
    /// de zone (<c>+0xd4</c>, premier pointeur, octet <c>+2</c>). Comme <c>0xc0</c> vaut 192 et que
    /// les tables statiques n'ont que 192 creneaux, <b>l'enfant est toujours resolu dans la table
    /// rechargee par sous-zone</b>, jamais dans la table statique. Quel gestionnaire cela designe
    /// depend donc du lieu : ici la regle « un sous-type ne veut rien dire tout seul » est
    /// structurelle, et non accidentelle.
    /// </para>
    /// <para>
    /// Le cap <c>+0x32</c> est calcule et non recopie : <c>Ordinal_457</c> (le <c>ratan2</c> de
    /// libgte) sur la vitesse deja posee, <c>+0x50</c> en ordonnee et <c>+0x58</c> en abscisse.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483450(byte* e)
    {
        *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);

        uint height = (uint)StatusBubbleHeight_00538BC0(*(uint**)(e + 0x6c));
        *(int*)(e + 0x20) += (int)((height & 0xfffffffe) * 0x800);

        *(uint*)(e + 0x28) = 0x0a000a00;
        *(uint*)(e + 0x38) = 0x01000000;

        *(short*)(e + 0x32) =
            (short)Winsys3d.Ordinal_457(*(int*)(e + 0x50) >> 0xc, *(int*)(e + 0x58) >> 0xc);

        // Ghidra: `*(byte *)(**(int **)(DAT_005e97cc + 0xd4) + 2) + 0xc0`.
        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte* first = *(byte**)region;
        byte subtype = (byte)(first[2] + 0xc0);

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551B00(), subtype, 0, 3);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004834d0</c> — <b>l'enfant de <see cref="EntityHandler_00483450"/></b>, 384
    /// octets, <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004834D0.asm</c>.
    ///
    /// <para>
    /// <b>Elle confirme le mecanisme annonce en suite 10.</b> Le pere engendre <c>0xc0 + n</c> ;
    /// dans la foret (zone 3, sous-zone 0) <c>n</c> vaut <c>0x1f</c>, donc <c>0xdf</c>, un numero
    /// au-dela des 192 creneaux statiques — c'est la <b>table rechargee par sous-zone</b> qui l'a
    /// resolu, et elle a designe ce corps. Dans un autre lieu le meme <c>0xdf</c> designerait autre
    /// chose. C'est la premiere fois que ce chemin-la est observe de bout en bout.
    /// </para>
    /// <para>
    /// <b>C'est un projectile qui roule et qui meurt de toucher.</b> Il integre sa vitesse
    /// lui-meme, avec un frottement plus faible que <see cref="IntegrateWithDrag_00535880"/> —
    /// <c>1/8 + 1/4096</c> au lieu de <c>1/8 + 1/256</c>, ecrit en clair ici plutot que factorise,
    /// parce que ce n'est pas la meme fonction.
    /// </para>
    /// <para>
    /// <b>Il tient au sol.</b> <see cref="FinalizeEntityPlacement2"/> resout la face portante, et
    /// le corps <b>sauvegarde <c>+0x20</c> avant l'appel pour le remettre apres</b> : la hauteur
    /// que la finalisation calcule est ignoree, seule celle du plancher <c>+0x60</c> compte. Quand
    /// il retombe dessous, il rebondit — <c>+0x54</c> remis a <c>0xa000</c>, les bits
    /// <c>0x2010</c> poses, l'altitude ramenee au plancher.
    /// </para>
    /// <para>
    /// <b>Sa mort est la regle inverse des autres.</b> Toutes les sorties effacent son mot d'etat
    /// (<c>*e = 0</c>, l'emplacement redevient libre) <b>sauf une</b> : celle ou le balayage ne
    /// trouve personne. Il ne survit donc que tant qu'il ne touche rien — les 2995 appels du releve
    /// du 2026-08-29 sont ce vol-la, repete. Quand il touche : degats au compte du lanceur
    /// (<c>+0x74</c>), bit <c>0x40</c> et <c>0x10</c> en <c>+0x96</c> sur la victime, son <c>3</c>,
    /// et l'eclaboussure <c>0xbd</c> avec l'atlas <see cref="s_SplashDescriptors_00551A08"/> — la
    /// meme que le jet du Goblin.
    /// </para>
    /// </summary>
    private static void EntityHandler_004834D0(byte* e)
    {
        // La sonde du balayage : six mots, poses avant tout le reste.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x28;
        probe[2] = 0x28;
        probe[3] = 0x28;
        probe[4] = 0;
        probe[5] = 0x50;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            // Ghidra: `and ah,0F7h` puis `or eax,680h` — le bit 0x800 tombe, 0x680 se pose.
            *(uint*)e = (*(uint*)e & 0xfffff7ff) | 0x680;
        }

        // Ghidra: LAB_0048351c — l'integration, ecrite ici et non appelee.
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        int gravity = *(short*)(e + 0x9a);

        *(int*)(e + 0x50) = (vx - (vx >> 0xc)) - (vx >> 3);
        *(int*)(e + 0x54) = ((gravity >> 8) - (vy >> 0xc)) + vy - gravity;
        *(int*)(e + 0x58) = (vz - (vz >> 0xc)) - (vz >> 3);

        // L'altitude est relue AVANT la finalisation et remise APRES : ce que celle-ci ecrit en
        // +0x20 est deliberement jete.
        int height = *(int*)(e + 0x20);

        FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940

        uint terrain = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = height;

        if ((terrain & 0xa000) == 0)
        {
            uint blocked = terrain & 0x802;

            if (blocked != 0 || height <= *(int*)(e + 0x60))
            {
                // Ghidra: LAB_004835a1 — le contact avec le plancher.
                uint state = *(uint*)e;

                if ((state & 0x10) != 0 || blocked != 0)
                {
                    *(uint*)e = 0;
                    return;
                }

                *(uint*)e = state | 0x2010;
                *(int*)(e + 0x54) = 0xa000;
                *(int*)(e + 0x20) = *(int*)(e + 0x60);
            }
        }

        // Ghidra: LAB_004835c9
        if ((*(uint*)e & 0x60000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);

        // LA seule sortie qui n'efface pas : personne de touche, il vole une image de plus.
        if (victim == null) return;

        if (*Mem.Camera(0x005de0b8) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

        *(uint*)victim |= 0x40;
        *(short*)(victim + 0x96) = 0x10;

        Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), SplashDescriptors(), 0xbd, 0, 4);

        *(uint*)e = 0;
    }
}
