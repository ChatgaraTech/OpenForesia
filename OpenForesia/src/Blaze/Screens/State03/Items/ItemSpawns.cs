// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// CE QU'UN OBJET FAIT NAITRE — les corps que les scripts de `ItemScript.cs` atteignent.
//
// Deux vagues, et les deux bandeaux `// ====` ci-dessous les separent : les dix corps que Ghidra
// avait DECOMPILES, puis ceux qui etaient ABSENTS de `Blaze.c` et ont ete releves au
// desassembleur.
//
// Les 2 370 fiches d'objet du CD ne font naitre que **21 gestionnaires d'entite** - le recensement
// hors ligne le montre en deroulant les scripts sans jouer une partie. C'est ce qui rend ce
// fichier fini : il n'y a pas de vingt-deuxieme a attendre.
//
// L'etat implemente / teste, objet par objet, est dans Documentation/Etat/ObjetsAtteignables.md.
// La sonde d'objets (`Documentation/Etat/objets-exerces.tsv`) est dans
// `Dispatch/CoverageProbes.cs` ; elle releve la fiche par l'hexa de ses seize premiers octets,
// pas par son nom - les noms d'objet ne sont pas de l'ASCII.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // CE QU'UN OBJET FAIT NAITRE — la premiere vague, les dix corps DECOMPILES
    //
    // `ObjetsAtteignables.md` recense 21 gestionnaires d'entite atteignables par un script d'objet,
    // pour les 2 370 fiches du CD. Quatre etaient ecrits ; voici les dix que Ghidra a decompiles,
    // du plus employe au moins employe, plus les trois PETITS-ENFANTS qu'ils designent et qui
    // manquaient.
    //
    //   0x00446DD0  189 emplois  0x3e  -> engendre 0xa6 (0x00446E20, ecrit plus bas)
    //   0x00445F60  166 emplois  0x44  -> engendre 0x9c (0x00446060, ecrit plus bas)
    //   0x004462B0  162 emplois  0x43  -> 0x70 et 0x93, deja ecrits
    //   0x00446900  110 emplois  0x41  -> 0xa9, deja ecrit
    //   0x00446200   36 emplois  0x45  -> 0x93, deja ecrit
    //   0x00476350   17 emplois  0x5a  -> 0x6e (0x00476410, ecrit plus bas)
    //   0x00445E70   17 emplois  0x48  -> 0x91, deja ecrit
    //   0x00445CF0   15 emplois  0x4c  -> 0x56, deja ecrit
    //   0x00446170   10 emplois  0x4d  -> rien : il peint le fond
    //   0x00475CC0    8 emplois  0x4e  -> 0x2b, deja ecrit
    //
    // TOUS SUIVENT LE MEME PATRON, celui de la bouffee 0x00446790 ci-dessus : `+0x10` est un etat
    // qui vaut 0 a la naissance, la premiere image l'avance et arme un compte a rebours, et
    // l'entite s'eteint en perdant son bit 0x80000000. Ce qui les distingue tient dans ce qu'ils
    // sement et dans leurs sons.
    //
    // LA GARDE DU POINTEUR NUL EST DANS LE BINAIRE, pas dans la transcription : quatre d'entre eux
    // relisent leur porteur en `+0x6c` et retombent sur l'enregistrement modele `0x005de558` quand
    // il est nul. Une entite orpheline se pose a l'origine plutot que de fauter.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551630</c> — <b>trois descripteurs d'atlas</b> que rien ne couvrait
    /// encore, releves a l'occasion des gestionnaires d'objet. <c>0x00551630</c> est celui que
    /// <see cref="EntityHandler_00446060"/> pose dans son <c>+0x3c</c>, <c>0x00551638</c> celui que
    /// <see cref="EntityHandler_00445CF0"/> passe a sa fille <c>0x56</c>.
    ///
    /// <para>
    /// <b>La borne droite est un changement de forme, pas de planche</b> : <c>0x00551648</c> porte
    /// <c>1f d0 20 08 00 0c 8a 4a</c>, qui n'est plus un descripteur du tout. Le troisieme
    /// enregistrement est relevé bien qu'aucun consommateur ne le designe encore — ses cinquieme et
    /// septieme octets different (<c>01 00 11 00</c> au lieu de <c>01 02 10 00</c>), donc il n'est
    /// pas de la meme serie, mais il est dans la plage et le rendu indexe en avant. Voir
    /// <c>check-descriptors.sh</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551630 =
    [
        0x18, 0x00, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551630
        0x00, 0xc0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551638
        0xf0, 0x80, 0x0f, 0x0f, 0x01, 0x00, 0x11, 0x00,   // 0x00551640
    ];

    private static byte* s_Descriptors00551630Storage;

    private static byte* Descriptors00551630()
        => NativeCopy(s_Descriptors_00551630, ref s_Descriptors00551630Storage);

    /// <summary>
    /// Ghidra: FUN_00446dd0 @ 0x00446dd0 — <b>le sous-type <c>0x3e</c></b>, creneau 62 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>189 emplois</b>, le plus employe des
    /// gestionnaires d'objet restants. Seize lignes, decompilees.
    ///
    /// <para>
    /// Le corps le plus court de la famille : sa premiere image arme quarante images de compte a
    /// rebours et lache <b>une</b> fille <c>0xa6</c> — <see cref="EntityHandler_00446E20"/>, la
    /// bulle qui enfle et s'eteint —, puis il decompte et disparait. Il ne bouge pas, ne suit
    /// personne et ne joue aucun son : tout est dans la fille, et lui n'existe que pour tenir sa
    /// duree.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446DD0(byte* e)
    {
        // Ghidra: le corps entier est sous `if (param_1 != NULL)`.
        if (e == null) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x28;

            SpawnCastEntity_0047AAE0(e, 0xa6, e[0x0b], e[0x0a]);
        }

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00446e20 @ 0x00446e20 — <b>la bulle du sous-type <c>0xa6</c></b>, creneau 166 de
    /// <see cref="s_StaticEntityHandlers_00551f30"/>. La fille unique de
    /// <see cref="EntityHandler_00446DD0"/>, et son seul appelant.
    ///
    /// <para>
    /// <b>Elle enfle vers <c>0x11f4</c> d'un huitieme par image</b>, sur les trois axes a la fois,
    /// en partant de 3000 — le rapprochement geometrique habituel du depot. Elle se recale sur son
    /// porteur a chaque image, <b>une demi-unite au-dessus de lui</b> (<c>+0x50000</c> sur Y), avec
    /// la meme garde de pointeur nul que ses sœurs.
    /// </para>
    /// <para>
    /// <b>Deux ages, et la teinte fait la bascule.</b> Elle naît noire (<c>+0x38</c>..<c>+0x3a</c>
    /// a zero) et s'eclaircit de <c>0x46</c> par image ; passe <c>0xc3</c> sur le rouge elle
    /// change d'age et redescend de <c>0x19</c> par image, jusqu'a s'eteindre au noir. Une
    /// respiration, pas un fondu.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446E20(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 2;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            *(ushort*)(e + 0x28) = 3000;
            *(ushort*)(e + 0x2a) = 3000;
            *(ushort*)(e + 0x2c) = 3000;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2e) = -0x3c;               // Ghidra: 0xffc4

            Aud.PlayPositionalSound(0x1c, 0x954, 0x1500, (int*)(e + 0x1c));
        }

        // L'enflement : un huitieme de ce qui reste jusqu'a 0x11f4, sur les trois axes.
        *(short*)(e + 0x28) += (short)((0x11f4 - *(short*)(e + 0x28)) >> 3);
        *(short*)(e + 0x2a) += (short)((0x11f4 - *(short*)(e + 0x2a)) >> 3);
        *(short*)(e + 0x2c) += (short)((0x11f4 - *(short*)(e + 0x2c)) >> 3);

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 0x46, 0x46, 0x46);
            if (e[0x38] > 0xc3) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, -0x19, -0x19, -0x19);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00445f60 @ 0x00445f60 — <b>le sous-type <c>0x44</c></b>, creneau 68 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>166 emplois</b>. Quarante-trois lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Il seme onze filles <c>0x9c</c> d'affilee</b> — une par image pendant que son compteur
    /// <c>+0x12</c> descend de dix a moins un —, sans atlas : c'est la fille
    /// (<see cref="EntityHandler_00446060"/>) qui pose le sien. Puis il attend, et le meme son
    /// <c>0x16</c> sonne <b>trois fois</b>, aux images 1, 8 et 15 ; la troisieme l'eteint.
    /// </para>
    /// <para>
    /// <b>Il garde le porteur BRUT dans <c>+0x78</c>, pas celui de la garde</b> : le champ recoit
    /// <c>+0x6c</c> tel quel, y compris nul, alors que la position se lit sur le modele de secours.
    /// Les deux ne sont pas le meme pointeur, et c'est ecrit ainsi dans le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00445F60(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        byte* source = carrier;
        if (source == null) source = Res.s_EntityList_005de558;

        *(byte**)(e + 0x78) = carrier;                 // le brut, pas celui de la garde
        *(int*)(e + 0x74) = *(int*)(e + 0x70);

        *(int*)(e + 0x1c) = *(int*)(source + 0x1c);
        *(int*)(e + 0x20) = *(int*)(source + 0x20);
        *(int*)(e + 0x24) = *(int*)(source + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x9c, e[0x0b], e[0x0a]);

            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x10) = 2;
        }

        (*(short*)(e + 0x98))++;

        if (*(short*)(e + 0x98) == 1) Aud.PlayPositionalSound(0x16, 400, 0x1200, (int*)(e + 0x1c));
        if (*(short*)(e + 0x98) == 8) Aud.PlayPositionalSound(0x16, 400, 0x1200, (int*)(e + 0x1c));

        if (*(short*)(e + 0x98) == 0xf)
        {
            Aud.PlayPositionalSound(0x16, 400, 0x1200, (int*)(e + 0x1c));
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00446060 @ 0x00446060 — <b>l'etincelle du sous-type <c>0x9c</c></b>, creneau 156
    /// de <see cref="s_StaticEntityHandlers_00552230"/>. La fille de
    /// <see cref="EntityHandler_00445F60"/>, et son seul appelant.
    ///
    /// <para>
    /// <b>Elle tourne autour du point ou elle est nee</b> — position figee dans
    /// <c>+0x50</c>..<c>+0x58</c> a la naissance, cap fixe tire au hasard sur le tour entier,
    /// rayon tire entre <c>0x1e</c> et <c>0x4f</c> — et <b>monte</b> de cinq a neuf unites par
    /// image. Le cap ne bouge jamais : les onze etincelles forment une gerbe verticale, chacune
    /// sur sa propre verticale.
    /// </para>
    /// <para>
    /// Son echelle est tiree entre 4000 et 6999 sur les deux axes, sa teinte naît a
    /// <c>b4/b4/b4</c> et perd dix par image ; elle s'eteint quand le rouge touche zero, soit une
    /// dizaine d'images.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446060(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551630();
            e[0x40] = 0;
            *(short*)(e + 0x10) = 1;

            *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);   // le cap, sur le tour entier
            *(short*)(e + 0x14) = (short)(CrtRand() % 5 + 5);      // la montee par image

            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;

            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x16) = (short)(CrtRand() % 0x32 + 0x1e);   // le rayon
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(short*)(e + 0x28) = (short)(CrtRand() % 3000 + 4000);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 3000 + 4000);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12))
                                * *(short*)(e + 0x16) + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12))
                                * *(short*)(e + 0x16) + *(int*)(e + 0x58);
            *(int*)(e + 0x20) += *(short*)(e + 0x14) * 0x1000;

            BrightenEntityTint_00404DE0(e, -10, -10, -10);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004462b0 @ 0x004462b0 — <b>le sous-type <c>0x43</c></b>, creneau 67 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>162 emplois</b>. Cinquante-deux lignes,
    /// decompilees.
    ///
    /// <para><b>Trois ages.</b> Il annonce d'un son <c>0x10</c>, puis pendant <b>vingt et une
    /// images</b> il lache une bille <c>0x70</c> par image — celle d'Enchant — sur un cap et un
    /// tangage tires au hasard a chaque fois, en se recalant sur <c>+0x78</c>. Au bout du compte il
    /// pose deux eclats <c>0x93</c> a la verticale, sonne le <c>0x13</c>, et six images plus tard
    /// sonne le <c>0x30</c> en s'eteignant.
    /// </para>
    /// <para>
    /// <b>Le cap est tire dans un demi-tour, decale d'un quart</b> : <c>rand % 0x400 + 0x400</c>.
    /// Le tangage, lui, prend le tour entier. La gerbe monte donc d'un cote.
    /// </para>
    /// </summary>
    private static void EntityHandler_004462B0(byte* e)
    {
        *(int*)(e + 0x74) = *(int*)(e + 0x70);
        *(int*)(e + 0x78) = *(int*)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x14;

            Aud.PlayPositionalSound(0x10, 0x6fd, 0x1200, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) == 1)
        {
            e[0x04] = 0x3f;
            *(int*)(e + 0x70) = *(int*)(e + 0x78);

            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;
            *(int*)(e + 0x4c) = 0;

            *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x400 + 0x400);
            *(short*)(e + 0x14) = 0;
            *(ushort*)(e + 0x34) = (ushort)(CrtRand() % 0x1000);

            *(short*)(e + 0x12) = 200;
            *(ushort*)(e + 0x28) = 0x1fff;
            *(ushort*)(e + 0x2a) = 0;
            *(ushort*)(e + 0x2c) = 0x100;

            SpawnCastEntity_0047AAE0(e, 0x70, e[0x0b], e[0x0a]);

            (*(short*)(e + 0x16))--;

            if (*(short*)(e + 0x16) < 0)
            {
                Aud.PlayPositionalSound(0x13, 400, 0x1200, (int*)(e + 0x1c));

                *(int*)(e + 0x50) = 0;
                *(int*)(e + 0x54) = 0x3c000;
                *(int*)(e + 0x58) = 0;

                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);

                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x16))++;

            if (*(short*)(e + 0x16) > 5)
            {
                Aud.PlayPositionalSound(0x30, 400, 0x800, (int*)(e + 0x1c));
                *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00446900 @ 0x00446900 — <b>le sous-type <c>0x41</c></b>, creneau 65 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>110 emplois</b>. Soixante-deux lignes,
    /// decompilees. Jumeau de la bouffee <see cref="EntityHandler_00446790"/> : memes deux sons aux
    /// memes images, meme fille <c>0xa9</c>, meme recalage sur le porteur.
    ///
    /// <para>
    /// <b>Ce qui le distingue tient dans son octet de saveur <c>+0x0b</c>.</b> Au-dela de neuf, il
    /// lui <b>retire dix</b>, garde la marque dans <c>+0x5c</c>, et seme quatre filles au lieu de
    /// zero — la premiere avec l'ancienne saveur diminuee, les trois autres avec la nouvelle. C'est
    /// la seule lecture conditionnelle de cette valeur dans toute la famille.
    /// </para>
    /// <para>
    /// Ensuite, une fille toutes les cinq images ; et <b>si la marque est levee</b>, une seconde
    /// une image sur deux. Onze images de vie.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446900(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0x50;
            *(short*)(e + 0x16) = 0;
            *(ushort*)(e + 0x9a) = e[0x0a];
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x5c) = 0;

            if (e[0x0b] > 9)
            {
                // La saveur descend d'un cran, et la premiere fille prend deja la nouvelle.
                byte reduced = (byte)(e[0x0b] - 10);
                e[0x0b] = reduced;
                *(int*)(e + 0x5c) = 1;

                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, reduced, e[0x0a]);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
            }
        }

        int beat = *(int*)(e + 0x44) + 1;
        *(int*)(e + 0x44) = beat;

        if (beat > 4)
        {
            *(int*)(e + 0x44) = 0;
            *(short*)(e + 0x14) = 0x50;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);

            if (*(int*)(e + 0x5c) == 1 && (e[0x16] & 1) != 0)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
            }
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlayPositionalSound(0x30, 0x200, 0x1000, (int*)(e + 0x1c));
        if (*(short*)(e + 0x16) == 2) Aud.PlayPositionalSound(0x2f, 0x653, 0x1500, (int*)(e + 0x1c));

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) > 10) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00446200 @ 0x00446200 — <b>le sous-type <c>0x45</c></b>, creneau 69 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>36 emplois</b>. Vingt-deux lignes,
    /// decompilees.
    ///
    /// <para>
    /// Deux images de vie. La premiere pose <b>deux eclats <c>0x93</c> a la verticale</b>
    /// (<c>+0x54 = 0x3c000</c>) et sonne le <c>0x2c</c> ; la troisieme image sonne le <c>0x2f</c>
    /// et l'eteint. C'est la moitie finale de <see cref="EntityHandler_004462B0"/>, extraite pour
    /// elle-meme.
    /// </para>
    /// <para>
    /// <b>Ghidra ecrit les deux ages a l'envers</b> — l'age 1 avant l'age 0 —, ce qui n'a aucun
    /// effet ici puisque l'age 0 se termine en posant 1 : l'entite ne peut pas traverser les deux
    /// blocs dans la meme image. La transcription garde l'ordre du binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446200(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 3)
            {
                Aud.PlayPositionalSound(0x2f, 0x400, 0x1100, (int*)(e + 0x1c));
                *(uint*)e &= 0x7fffffff;
            }
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x74) = *(int*)(e + 0x70);
            *(int*)(e + 0x78) = *(int*)(e + 0x6c);

            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0x3c000;
            *(int*)(e + 0x58) = 0;

            SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
            SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            Aud.PlayPositionalSound(0x2c, 700, 0xa00, (int*)(e + 0x1c));
        }
    }

    /// <summary>
    /// Ghidra: FUN_00476350 @ 0x00476350 — <b>le sous-type <c>0x5a</c></b>, creneau 90 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>17 emplois</b>. Trente-cinq lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Trois anneaux d'un coup</b> a la naissance — sous-type <c>0x6e</c>,
    /// <see cref="EntityHandler_00476410"/> —, les caps espaces de <c>0x555</c>, soit le tour
    /// divise en trois. Puis il se colle a la <b>tete</b> de son porteur (la moitie de
    /// <see cref="StatusBubbleHeight_00538BC0"/>) et decompte de dix par image depuis 200 : vingt
    /// images.
    /// </para>
    /// <para>
    /// <b>Il ne s'eteint pas, il se marque</b> : a zero il pose son propre bit <c>0x2000000</c>, et
    /// c'est l'image suivante qui le voit et le libere. Les trois anneaux heritent de ce bit par
    /// leur propre corps, ce qui les fait mourir avec lui.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476350(byte* e)
    {
        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* carrier = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2e) = -0x14;               // Ghidra: 0xffec

            for (int ring = 0; ring < 3; ring++)
            {
                SpawnEffectEntity(e, 0x6e, e[0x0b], -1);
                *(short*)(e + 0x32) += 0x555;          // le tour en trois
            }

            *(short*)(e + 0x12) = 200;
            *(short*)(e + 0x14) = (short)(StatusBubbleHeight_00538BC0((uint*)carrier) >> 1);

            Aud.PlayPositionalSound(0x23, 0x1000, 0x1200, (int*)(e + 0x1c));
        }

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(short*)(e + 0x14) * 0x1000 + *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        *(short*)(e + 0x12) -= 10;
        if (*(short*)(e + 0x12) == 0) *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_00476410 @ 0x00476410 — <b>l'anneau du sous-type <c>0x6e</c></b>, creneau 110 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. Les trois filles de
    /// <see cref="EntityHandler_00476350"/>, et son seul appelant.
    ///
    /// <para>
    /// <b>Il tourne en se refermant</b> : son rayon <c>+0x12</c> part de 200 et perd dix par image
    /// pendant que son cap <c>+0x32</c> avance de <c>0x80</c>, autour de la position de son porteur
    /// — dont il prend la hauteur telle quelle, sans decalage. Vingt images, et le rayon passe par
    /// zero.
    /// </para>
    /// <para>
    /// <b>Il herite du bit de fin de son porteur</b> (<c>*e |= *porteur &amp; 0x2000000</c>) et
    /// meurt avec lui — c'est ainsi que les trois anneaux s'eteignent ensemble.
    /// </para>
    /// <para>
    /// Sa naissance seme a son tour : une fille <c>0x4d</c> avec l'atlas <c>0x00551ba0</c>, et
    /// <b>deux</b> filles <c>0x6a</c>. Le <c>+0x12</c> est pose a <c>0x1000</c> le temps de ces
    /// naissances puis ramene a 200 — les filles heritent donc du grand rayon, lui garde le petit.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476410(byte* e)
    {
        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* carrier = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4d, e[0x0b], -1);

            // Les deux filles 0x6a heritent d'un rayon de 0x1000 ; lui repart a 200.
            *(short*)(e + 0x12) = 0x1000;
            SpawnCastEntity_0047AAE0(e, 0x6a, e[0x0b], -1);
            SpawnCastEntity_0047AAE0(e, 0x6a, e[0x0b], -1);
            *(short*)(e + 0x12) = 200;

            Aud.PlayPositionalSound(0x2f, 0x554, 0x1200, (int*)(e + 0x1c));
        }

        short yaw = *(short*)(e + 0x32);
        short radius = *(short*)(e + 0x12);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(carrier + 0x24);

        *(short*)(e + 0x32) = (short)(yaw + 0x80);
        *(short*)(e + 0x12) = (short)(radius - 10);

        *(uint*)e |= *(uint*)carrier & 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_00445e70 @ 0x00445e70 — <b>le sous-type <c>0x48</c></b>, creneau 72 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>17 emplois</b>. Quarante-quatre lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Il fait clignoter son porteur</b> : le mot <c>+0x9a</c> vaut <c>0x800</c> une image sur
    /// deux et zero l'autre, pendant cinquante images. C'est le seul de la famille dont l'effet
    /// principal n'est pas une naissance.
    /// </para>
    /// <para>
    /// <b>Et il seme sur le porteur, pas sur lui-meme</b> : les quatorze premieres images, une
    /// image sur deux, il lache une fille <c>0x91</c> a la position du porteur — <b>a condition
    /// que celui-ci soit encore vivant</b> (bit <c>0x80000000</c> pose, bits <c>0x50000000</c>
    /// eteints). C'est la seule garde de vivacite de la famille.
    /// </para>
    /// <para>Deux sons : le <c>0x31</c> a la premiere image, le <c>0x16</c> a la quinzieme.</para>
    /// </summary>
    private static void EntityHandler_00445E70(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        byte* source = carrier;
        if (source == null) source = Res.s_EntityList_005de558;

        *(int*)(e + 0x74) = *(int*)(e + 0x70);
        *(byte**)(e + 0x78) = carrier;                 // le brut, pas celui de la garde

        *(int*)(e + 0x1c) = *(int*)(source + 0x1c);
        *(int*)(e + 0x20) = *(int*)(source + 0x20);
        *(int*)(e + 0x24) = *(int*)(source + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
        }

        // Le clignotement : une image sur deux.
        *(ushort*)(e + 0x9a) = (ushort)((*(ushort*)(e + 0x16) & 1) == 0 ? 0x800 : 0);

        if (*(ushort*)(e + 0x16) == 0) Aud.PlayPositionalSound(0x31, 300, 0x1000, (int*)(e + 0x1c));
        if (*(short*)(e + 0x16) == 0xf) Aud.PlayPositionalSound(0x16, 0x100, 0x1000, (int*)(e + 0x1c));

        if ((short)*(ushort*)(e + 0x16) < 0xe
            && (*(uint*)source & 0x80000000) != 0
            && (*(uint*)source & 0x50000000) == 0)
        {
            *(byte**)(e + 0x78) = source;

            if ((*(ushort*)(e + 0x16) & 1) != 0)
            {
                *(int*)(e + 0x5c) = 0;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(source + 0x1c), null, 0x91, 0, -1);
            }
        }

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) == 0x32) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00445cf0 @ 0x00445cf0 — <b>le sous-type <c>0x4c</c></b>, creneau 76 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>15 emplois</b>. Cinquante-deux lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Onze filles <c>0x56</c> — la flamme de Materia feu</b> —, une par image, chacune posee a
    /// un <b>decalage lateral tire au hasard</b> entre <c>-0x3c</c> et <c>0x3b</c> unites sur X et
    /// sur Z (<c>rand % 0x78 - 0x3c</c>, mis a l'echelle par <c>&lt;&lt; 12</c>). L'atlas
    /// <c>0x00551638</c> leur est passe explicitement.
    /// </para>
    /// <para>
    /// <b>Cinq coups du meme son <c>0x10</c>, de plus en plus aigus</b> : hauteur <c>0x400</c>,
    /// <c>0x800</c>, puis <c>0x1000</c> trois fois, aux images 1, 11, 21, 24 et 27. Le dernier
    /// l'eteint.
    /// </para>
    /// <para>
    /// Il <b>repose <c>+0x6c</c> apres chaque naissance</b> : la fille recoit le porteur, mais le
    /// champ a servi de brouillon entre-temps.
    /// </para>
    /// </summary>
    private static void EntityHandler_00445CF0(byte* e)
    {
        uint carrier = *(uint*)(e + 0x6c);

        *(int*)(e + 0x74) = *(int*)(e + 0x70);
        *(uint*)(e + 0x78) = carrier;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x10) = 2;

            *(uint*)(e + 0x70) = carrier;
            *(ushort*)(e + 0x28) = 7000;
            *(ushort*)(e + 0x2a) = 7000;

            *(int*)(e + 0x44) = (CrtRand() % 0x78 - 0x3c) * 0x1000;

            int drift = CrtRand();
            *(short*)(e + 0x14) = 3;
            e[0x38] = 0xf0;
            e[0x39] = 0xf0;
            e[0x3a] = 0xf0;
            *(int*)(e + 0x4c) = (drift % 0x78 - 0x3c) * 0x1000;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551630() + 8, 0x56, e[0x0b], e[0x0a]);

            *(uint*)(e + 0x6c) = carrier;
        }

        (*(short*)(e + 0x98))++;

        short beat = *(short*)(e + 0x98);
        if (beat == 1) Aud.PlayPositionalSound(0x10, 0x400, 0x1000, (int*)(e + 0x1c));
        if (beat == 0xb) Aud.PlayPositionalSound(0x10, 0x800, 0x1000, (int*)(e + 0x1c));
        if (beat == 0x15) Aud.PlayPositionalSound(0x10, 0x1000, 0x1000, (int*)(e + 0x1c));
        if (beat == 0x18) Aud.PlayPositionalSound(0x10, 0x1000, 0x1000, (int*)(e + 0x1c));

        if (beat == 0x1b)
        {
            Aud.PlayPositionalSound(0x10, 0x1000, 0x1000, (int*)(e + 0x1c));
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00446170 @ 0x00446170 — <b>le sous-type <c>0x4d</c></b>, creneau 77 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>10 emplois</b>. Vingt-trois lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Il ne fait naitre rien du tout : il eteint le fond de l'ecran.</b> Sa premiere image
    /// ecrit zero dans les trois composantes de <c>s_BackColour*_005e93a0</c>, directement, sans
    /// passer par le fondu du mode 2. Puis il sonne trois fois le <c>0x0e</c>, une image sur six,
    /// et disparait — dix-huit images en tout.
    /// </para>
    /// <para>
    /// C'est, avec l'explosion de Boule de feu, le seul corps du depot a ecrire ce champ en clair.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446170(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 6)
            {
                *(short*)(e + 0x12) = 0;
                Aud.PlayPositionalSound(0x0e, 0x200, 0x1000, (int*)(e + 0x1c));

                (*(short*)(e + 0x14))++;
                if (*(short*)(e + 0x14) == 3) *(uint*)e &= 0x7fffffff;
            }
        }

        if (*(short*)(e + 0x10) == 0)
        {
            Mem.s_BackColourR_005e93a0 = 0;
            Mem.s_BackColourG_005e93a2 = 0;
            Mem.s_BackColourB_005e93a4 = 0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;

            Aud.PlayPositionalSound(0x1a, 0x2d0, 0x1000, (int*)(e + 0x1c));
        }
    }

    /// <summary>
    /// Ghidra: FUN_00475cc0 @ 0x00475cc0 — <b>le sous-type <c>0x4e</c></b>, creneau 78 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>8 emplois</b>. Dix-neuf lignes,
    /// decompilees.
    ///
    /// <para>
    /// <b>Une couronne qui se dessine en tournant</b> : vingt-quatre images, une fille <c>0x2b</c>
    /// par image, le cap avancant de <c>0x400</c> a chaque fois — soit un quart de tour, donc six
    /// tours complets en vingt-quatre images. L'atlas <c>0x005519a0</c> leur est passe.
    /// </para>
    /// <para>
    /// Il n'a pas d'age : <c>+0x10</c> lui sert directement de compteur d'images, ce qui est
    /// l'exception dans la famille. Et il s'eteint par <c>*e = 0</c> franc, pas en retirant le bit
    /// <c>0x80000000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475CC0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x40] = 0;
            *(int*)(e + 0x74) = *(int*)(e + 0x6c);
            *(int*)(e + 0x78) = *(int*)(e + 0x6c);
            *(uint*)(e + 0x38) = 0x00b0b0b0;
            *(uint*)(e + 0x28) = 0x20002000;

            Aud.PlayPositionalSound(0x2f, 0x653, 0x3fff, (int*)(e + 0x1c));
        }

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors005519A0(), 0x2b, e[0x0b], -1);

        *(short*)(e + 0x32) += 0x400;

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) > 0x17) *(uint*)e = 0;
    }

    // =====================================================================
    // CE QU'UN OBJET FAIT NAITRE — la seconde vague, les corps ABSENTS DE `Blaze.c`
    //
    // Sept des vingt et un gestionnaires d'objet n'ont pas de corps decompile. Ils sortent du
    // desassembleur ; les releves sont gardes sous `Legacy/Raw/Blaze/`.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005515e0</c> — <b>dix descripteurs d'atlas</b>, la plage
    /// <c>0x005515e0</c>-<c>0x0055162f</c>. Elle comble le trou entre
    /// <see cref="s_Descriptors_005515C0"/> et <see cref="s_Descriptors_00551630"/> : les trois
    /// tables se suivent maintenant sans interstice de <c>0x005515c0</c> a <c>0x00551648</c>.
    ///
    /// <para>
    /// <b>Sept d'entre eux sont les sept images de <see cref="EntityHandler_00447050"/></b>, qui
    /// pose cette adresse dans son <c>+0x3c</c> et fait monter son <c>+0x40</c> jusqu'a sept. Les
    /// trois derniers (<c>0x00551618</c> et suivants) changent de bande dans l'atlas et n'ont pas
    /// de consommateur connu ; ils sont releves parce qu'ils sont dans la plage, pas parce qu'on
    /// sait a quoi ils servent.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_005515E0 =
    [
        0xc0, 0xb8, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x005515e0
        0xd8, 0xb8, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x005515e8
        0x60, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x005515f0
        0x78, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x005515f8
        0x90, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551600
        0xa8, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551608
        0xc0, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551610
        0x00, 0x30, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551618
        0x18, 0x30, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551620
        0x30, 0x30, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x00551628
    ];

    private static byte* s_Descriptors005515E0Storage;

    private static byte* Descriptors005515E0()
        => NativeCopy(s_Descriptors_005515E0, ref s_Descriptors005515E0Storage);

    /// <summary>
    /// Ghidra: FUN_00446f30 @ 0x00446f30 — <b>le sous-type <c>0x3d</c></b>, creneau 61 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>95 emplois</b>, le cinquieme gestionnaire
    /// d'objet le plus employe. 273 octets, <b>absent de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00446F30.asm</c>.
    ///
    /// <para>
    /// <b>Il MESURE son porteur, et c'est ce qui le distingue de toute la famille.</b> Il resout
    /// les trois blocs de description de l'entite (<see cref="ResolveEntityBlocks"/>,
    /// <c>FUN_00538980</c>) et lit les <b>trois dimensions du bloc de modele</b> en <c>+0x1c</c>,
    /// <c>+0x1e</c> et <c>+0x20</c> :
    /// </para>
    /// <list type="bullet">
    /// <item><c>+0x14</c> = <c>(largeur + profondeur) / 4</c> — deux decalages successifs, un sur
    /// 32 bits puis un sur 16.</item>
    /// <item><c>+0x16</c> = <c>(largeur + hauteur + profondeur) / 3</c>, <b>plafonne a 600</b>,
    /// puis multiplie par 50 — une multiplication sur seize bits.</item>
    /// </list>
    /// <para>
    /// <b>Un membre du groupe n'est pas mesure</b> : le bit <c>0x800000</c> du porteur court-
    /// circuite tout le calcul et impose <c>0x1e</c> et <c>6000</c>. Les personnages ont donc tous
    /// la meme taille d'effet, quel que soit leur modele.
    /// </para>
    /// <para>
    /// Ni <c>+0x14</c> ni <c>+0x16</c> ne sont relus par ce corps : ils partent dans la fille par
    /// la recopie des 0x27 mots que fait <see cref="SpawnStatusEffectEntity_0047AC40"/>. Il seme
    /// une fille <c>0x9a</c> par image pendant cinq images, en se recalant sur le porteur, puis
    /// s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446F30(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        if ((*(uint*)carrier & 0x800000) != 0)
        {
            // Un membre du groupe : pas de mesure, deux constantes.
            *(short*)(e + 0x14) = 0x1e;
            *(short*)(e + 0x16) = 0x1770;              // 6000
        }
        else
        {
            // Ghidra: LAB_00446f63 — la mesure du modele.
            ResolveEntityBlocks(carrier, out _, out _, out byte* model);

            int width = *(short*)(model + 0x1c);
            int height = *(short*)(model + 0x1e);
            int depth = *(short*)(model + 0x20);

            // Deux decalages, un sur 32 bits puis un sur 16 : le quart de la somme.
            *(short*)(e + 0x14) = (short)((short)((width + depth) >> 1) >> 1);

            short average = (short)((width + height + depth) / 3);
            if (average > 0x258) average = 0x258;      // le plafond, 600

            *(short*)(e + 0x16) = (short)(average * 0x32);   // multiplication SEIZE bits
        }

        // Ghidra: LAB_00446fcd
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            Aud.PlayPositionalSound(3, 0x80, 0x1000, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x9a, e[0x0b], e[0x0a]);

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 5) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00447170 @ 0x00447170 — <b>le sous-type <c>0x3c</c></b>, creneau 60 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>Un seul emploi</b> dans les 2 370 fiches
    /// d'objet du CD, et dans la seule zone 1. 272 octets, <b>absent de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Cinq etincelles <c>0x98</c> d'un coup</b> — celles que les routines d'intelligence
    /// emploient a chaque coup porte —, chacune avec son propre cap tire sur le tour entier et sa
    /// propre distance tiree entre <c>0x200</c> et <c>0x5ff</c>, ecrite dans <c>+0x16</c> avant la
    /// naissance pour que la fille en herite. Puis un son, et quatre images plus tard un second son
    /// en s'eteignant. Six images de vie en tout.
    /// </para>
    /// <para>
    /// <b>Il se pose sur la TETE du porteur, mesuree</b> : il resout le bloc de modele
    /// (<see cref="ResolveEntityBlocks"/>) et ajoute sa hauteur <c>+0x1e</c> a la position — mais
    /// <b>sans mise a l'echelle</b>, alors que la position est en 20.12. C'est ce que fait le
    /// binaire ; la gerbe se pose donc pratiquement au sol.
    /// </para>
    /// <para>
    /// <b>Il lit son porteur dans <c>+0x70</c></b>, pas dans <c>+0x6c</c> comme toute la famille.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447170(byte* e)
    {
        if (e == null) return;

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 4)
            {
                *(uint*)e &= 0x7fffffff;
                Aud.PlayPositionalSound(0x0d, 0xcb2, 0x1200, (int*)(e + 0x1c));
            }
        }

        // Ghidra: LAB_004471b5
        if (*(short*)(e + 0x10) != 0) return;

        byte* carrier = *(byte**)(e + 0x70);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        ResolveEntityBlocks(carrier, out _, out _, out byte* model);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        // La hauteur du modele s'ajoute TELLE QUELLE a une coordonnee en 20.12.
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + *(short*)(model + 0x1e);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        *(short*)(e + 0x14) = 0xb4;

        for (int spark = 0; spark < 5; spark++)
        {
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x16) = (short)(CrtRand() % 0x400 + 0x200);

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x98, 0, -1);
        }

        Aud.PlayPositionalSound(0x0d, 0x1964, 0x1200, (int*)(e + 0x1c));

        *(short*)(e + 0x10) = 1;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00446410 @ 0x00446410 — <b>le sous-type <c>0x42</c></b>, creneau 66 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>25 emplois</b>. 515 octets, <b>absent de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00446410.asm</c>.
    ///
    /// <para>
    /// <b>Quatre bulles a la naissance, puis vingt et une billes.</b> Sa premiere image pose
    /// <b>quatre</b> spheres <c>0xa7</c> (<see cref="EntityHandler_00446620"/>) par paires : deux
    /// larges — echelle de depart <c>0x1b58</c>/<c>0x1770</c>/<c>0x1b58</c> vers une cible de
    /// <c>0x1000</c> partout — et deux etroites, qui partent de rien et enflent vers
    /// <c>0xbb8</c>. Dans chaque paire, la seconde a son tangage <c>+0x34</c> a <c>0x800</c> : les
    /// deux moities de la sphere.
    /// </para>
    /// <para>
    /// Ensuite, vingt et une images durant, une bille <c>0x70</c> par image — celle d'Enchant — sur
    /// un cap tire dans un demi-tour decale d'un quart et un tangage tire sur le tour entier.
    /// </para>
    /// <para>
    /// <b>Trois sons de plus en plus aigus</b> aux trois premieres images, comptes par
    /// <c>+0x98</c> : <c>0x1e</c> a <c>0x100</c>, <c>0x1f</c> a <c>0x400</c>, <c>0x23</c> a
    /// <c>0x5a9</c>. Le compteur monte ensuite pour rien — c'est un compteur d'images, pas un age.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446410(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x6c);
        *(byte**)(e + 0x78) = carrier;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 2;
            e[0x0b] = 0;

            // Les deux larges : elles partent grosses et se resserrent vers 0x1000.
            *(ushort*)(e + 0x28) = 0x1000;
            *(ushort*)(e + 0x2a) = 0x1000;
            *(ushort*)(e + 0x2c) = 0x1000;
            *(int*)(e + 0x44) = 0x1b58;
            *(int*)(e + 0x48) = 0x1770;
            *(int*)(e + 0x4c) = 0x1b58;

            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0;
            SpawnCastEntity_0047AAE0(e, 0xa7, 0, 2);

            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0x800;               // l'autre moitie
            SpawnCastEntity_0047AAE0(e, 0xa7, e[0x0b], e[0x0a]);

            // Les deux etroites : elles partent de rien et enflent vers 0xbb8.
            *(ushort*)(e + 0x28) = 0xbb8;
            *(ushort*)(e + 0x2c) = 0xbb8;
            *(int*)(e + 0x48) = 0xbb8;
            *(ushort*)(e + 0x2a) = 0x1000;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x4c) = 0;

            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0;
            SpawnCastEntity_0047AAE0(e, 0xa7, e[0x0b], e[0x0a]);

            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0x800;
            SpawnCastEntity_0047AAE0(e, 0xa7, e[0x0b], e[0x0a]);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x14;
            *(short*)(e + 0x98) = 0;
        }

        // Ghidra: LAB_0044651b — vingt et une billes, une par image.
        if (*(short*)(e + 0x10) == 1)
        {
            e[0x04] = 0x3f;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(byte**)(e + 0x70) = *(byte**)(e + 0x78);

            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;
            *(int*)(e + 0x4c) = 0;

            *(short*)(e + 0x32) = (short)(CrtRand() % 0x400 + 0x400);
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) = 0xc8;
            *(ushort*)(e + 0x28) = 0x1fff;
            *(short*)(e + 0x2a) = 0;
            *(ushort*)(e + 0x2c) = 0x100;

            SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);

            (*(short*)(e + 0x16))--;
            if (*(short*)(e + 0x16) < 0) *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_004465af — les trois sons, comptes a part.
        if (*(short*)(e + 0x98) == 0) Aud.PlayPositionalSound(0x1e, 0x100, 0x1000, (int*)(e + 0x1c));
        if (*(short*)(e + 0x98) == 1) Aud.PlayPositionalSound(0x1f, 0x400, 0x1000, (int*)(e + 0x1c));
        if (*(short*)(e + 0x98) == 2) Aud.PlayPositionalSound(0x23, 0x5a9, 0x1000, (int*)(e + 0x1c));

        (*(short*)(e + 0x98))++;
    }

    /// <summary>
    /// Ghidra: FUN_00446620 @ 0x00446620 — <b>la sphere du sous-type <c>0xa7</c></b>, creneau 167
    /// de <see cref="s_StaticEntityHandlers_00551f30"/>. Les quatre filles de
    /// <see cref="EntityHandler_00446410"/>, et son seul appelant. 362 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle intervertit sa taille de depart et sa taille d'arrivee a la naissance.</b> Sa mere
    /// lui a laisse la cible dans <c>+0x28</c>..<c>+0x2c</c> et le depart dans
    /// <c>+0x44</c>..<c>+0x4c</c> ; sa premiere image recopie la cible dans
    /// <c>+0x50</c>..<c>+0x58</c>, puis charge le depart dans l'echelle. C'est ce qui permet a la
    /// mere de decrire quatre spheres differentes avec les memes champs.
    /// </para>
    /// <para>
    /// <b>Deux ages.</b> Pendant trente images, l'echelle approche sa cible d'un huitieme par image
    /// sur les trois axes, et la teinte monte vers <c>0xc8</c> au meme rythme — partie du noir.
    /// Puis elle s'assombrit de vingt par image et s'eteint au noir. Son cap <c>+0x32</c> tourne de
    /// <c>0xc8</c> par image, masque sur douze bits, tout du long.
    /// </para>
    /// <para>
    /// Elle suit <c>+0x78</c>, une demi-unite au-dessus, avec la garde du pointeur nul.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446620(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x78);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            // L'echelle courante devient la CIBLE, et le depart vient de +0x44..+0x4c.
            *(int*)(e + 0x50) = *(short*)(e + 0x28);
            *(int*)(e + 0x54) = *(short*)(e + 0x2a);
            *(int*)(e + 0x58) = *(short*)(e + 0x2c);

            *(short*)(e + 0x28) = *(short*)(e + 0x44);
            *(short*)(e + 0x2a) = *(short*)(e + 0x48);
            *(short*)(e + 0x2c) = *(short*)(e + 0x4c);

            e[0x04] = 0x22;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        // Ghidra: LAB_004466a2 — le cap tourne toujours, masque sur douze bits.
        *(short*)(e + 0x32) = (short)((*(short*)(e + 0x32) + 0xc8) & 0xfff);

        if (*(short*)(e + 0x10) == 1)
        {
            // La teinte monte vers 0xc8, l'echelle vers sa cible : un huitieme par image.
            e[0x38] = (byte)(((0xc8 - e[0x38]) >> 3) + e[0x38]);
            e[0x39] = (byte)(((0xc8 - e[0x39]) >> 3) + e[0x39]);
            e[0x3a] = (byte)(((0xc8 - e[0x3a]) >> 3) + e[0x3a]);

            *(short*)(e + 0x28) =
                (short)(((*(int*)(e + 0x50) - *(short*)(e + 0x28)) >> 3) + *(short*)(e + 0x28));
            *(short*)(e + 0x2a) =
                (short)(((*(int*)(e + 0x54) - *(short*)(e + 0x2a)) >> 3) + *(short*)(e + 0x2a));
            *(short*)(e + 0x2c) =
                (short)(((*(int*)(e + 0x58) - *(short*)(e + 0x2c)) >> 3) + *(short*)(e + 0x2c));

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x1e) *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_00446760
        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, -20, -20, -20);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00476500 @ 0x00476500 — <b>le sous-type <c>0x5b</c></b>, creneau 91 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>75 emplois</b>. 197 octets, <b>absent de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00476500.asm</c>.
    ///
    /// <para>
    /// <b>Une colonne de flammes tirees au hasard dans un disque.</b> Vingt-quatre images durant,
    /// il lache une fille <c>0x56</c> par image a un cap tire sur le tour entier et a une distance
    /// tiree entre zero et la <b>demi-hauteur de tete</b> du porteur — le disque ou tombent les
    /// flammes a donc la taille du porteur.
    /// </para>
    /// <para>
    /// <b>Son echelle aussi vient de cette hauteur</b> : <c>hauteur &lt;&lt; 12 / 150</c>, la meme
    /// formule que les aures d'etat. La division par 150 est emise en multiplication magique
    /// (<c>0x1b4e81b5</c> puis <c>sar 4</c>), ce que le desassemblage ne dit pas en clair.
    /// </para>
    /// <para>
    /// <b>Il vit soixante images, mais ne seme que pendant les vingt-quatre premieres</b> : les
    /// trente-six suivantes laissent les dernieres flammes s'eteindre. Et l'extinction est ecrite a
    /// l'envers de l'usage — <c>*e = 0</c> franc, puis <c>+0x10</c> est <b>quand meme</b>
    /// incremente et reecrit.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476500(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte* carrier = *(byte**)(e + 0x6c);

            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -0x40;               // Ghidra: 0xffc0
            *(byte**)(e + 0x78) = carrier;
            *(byte**)(e + 0x70) = carrier;

            int height = StatusBubbleHeight_00538BC0((uint*)carrier);

            *(short*)(e + 0x12) = (short)(height >> 1);
            *(short*)(e + 0x14) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;

            // Ghidra: `imul 1B4E81B5h` puis `sar edx,4` — une division par 150.
            *(int*)(e + 0x28) = (height << 0xc) / 0x96;
        }

        // Ghidra: LAB_00476558 — vingt-quatre images de semis.
        if (*(short*)(e + 0x10) < 0x18)
        {
            int yaw = CrtRand();
            int radius = CrtRand() % *(short*)(e + 0x12);

            *(short*)(e + 0x32) = (short)yaw;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(yaw) * radius;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(yaw) * radius;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x56, e[0x0b], -1);
        }

        // Ghidra: LAB_004765ac — l'extinction precede l'increment, et l'increment a lieu quand meme.
        short age = *(short*)(e + 0x10);
        if (age >= 0x3c) *(uint*)e = 0;
        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: FUN_00446a90 @ 0x00446a90 — <b>le sous-type <c>0x3f</c></b>, creneau 63 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>4 emplois</b> dans les 2 370 fiches
    /// d'objet du CD. 507 octets, <b>absent de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00446A90.asm</c>.
    ///
    /// <para>
    /// <b>C'est le jumeau TIRE AU SORT de <see cref="EntityHandler_00446F30"/></b> : meme mesure du
    /// porteur par <see cref="ResolveEntityBlocks"/>, meme court-circuit du bit <c>0x800000</c> pour
    /// un membre du groupe, meme semis de cinq images — mais chacune des trois mesures passe
    /// ensuite par un tirage, de sorte que les cinq filles ne se superposent pas :
    /// </para>
    /// <list type="bullet">
    /// <item><c>+0x14</c>, le <b>rayon</b> : <c>hasard % ((largeur + profondeur) / 4)</c>.</item>
    /// <item><c>+0x16</c>, l'<b>echelle</b> : la moyenne des trois dimensions plafonnee a 600 puis
    /// multipliee par 50 — comme chez <c>0x00446f30</c> —, dont on garde la moitie et a laquelle on
    /// ajoute un tirage sur cette meme moitie. L'echelle tient donc dans <c>[v/2, v[</c>.</item>
    /// <item><c>+0x9a</c>, la <b>dispersion verticale</b> : <c>hasard % hauteur</c>, ou <c>0x0b</c>
    /// si le tirage sort nul.</item>
    /// </list>
    /// <para>
    /// Les trois champs partent dans la fille <c>0x9b</c> (<see cref="EntityHandler_00446C90"/>)
    /// par la recopie des 0x27 mots que fait <see cref="SpawnStatusEffectEntity_0047AC40"/>, et
    /// c'est elle qui les emploie : rayon pour s'ecarter, echelle pour sa taille, dispersion pour
    /// sa hauteur de depart.
    /// </para>
    /// <para>
    /// <b>Un membre du groupe n'est pas mesure</b> — le bit <c>0x800000</c> impose <c>0x1e</c>,
    /// <c>6000</c> et <c>0xa0</c>, et aucun des trois tirages n'a lieu.
    /// </para>
    /// <para>
    /// <b>Puis trente images de bourdon</b> : passe en phase 2, il ne seme plus rien et joue le son
    /// <c>0x14</c> a <c>0x1000</c> une image sur quatre, comptees par <c>+0x98</c>.
    /// </para>
    /// <para>
    /// <b>Deux traces de 1998 transcrites telles quelles.</b> (1) La premiere branche de la phase 2
    /// — le son <c>0x14</c> a <c>0xf4e</c> quand <c>+0x98</c> vaut zero — est <b>morte</b> :
    /// <c>+0x98</c> est mis a zero a l'entree en phase 2 puis incremente avant le test, et remis a
    /// zero des qu'il atteint 4, donc il ne vaut jamais zero a cet endroit. (2) Les trois modulos
    /// divisent par une mesure du modele sans la verifier ; un modele plat ferait un <c>#DE</c> en
    /// 1998 comme il fait une <c>DivideByZeroException</c> ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446A90(byte* e)
    {
        if (e == null) return;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        // Ghidra: LAB_00446aae
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        // Ghidra: LAB_00446ac4 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

            if ((*(uint*)carrier & 0x800000) != 0)
            {
                // Un membre du groupe : pas de mesure, pas de tirage, trois constantes.
                *(short*)(e + 0x14) = 0x1e;
                *(short*)(e + 0x16) = 0x1770;             // 6000
                *(short*)(e + 0x9a) = 0xa0;
            }
            else
            {
                // Ghidra: LAB_00446b06 — la mesure du modele, puis les trois tirages.
                ResolveEntityBlocks(carrier, out _, out _, out byte* model);

                int width = *(short*)(model + 0x1c);
                int height = *(short*)(model + 0x1e);
                int depth = *(short*)(model + 0x20);

                // Deux decalages, un sur 32 bits puis un sur 16 : le quart de la somme.
                *(short*)(e + 0x14) = (short)((short)((width + depth) >> 1) >> 1);
                *(short*)(e + 0x14) = (short)(CrtRand() % *(short*)(e + 0x14));

                short average = (short)((width + height + depth) / 3);
                if (average > 0x258) average = 0x258;     // le plafond, 600

                // Ghidra: LAB_00446b78 — multiplication SEIZE bits, puis plancher a 1.
                *(short*)(e + 0x16) = (short)(average * 0x32);
                if (*(short*)(e + 0x16) <= 0) *(short*)(e + 0x16) = 1;

                // Ghidra: LAB_00446b8d — la moitie, plus un tirage sur cette moitie.
                short half = (short)(*(short*)(e + 0x16) >> 1);
                *(short*)(e + 0x16) = (short)(CrtRand() % half + half);

                *(short*)(e + 0x9a) = (short)(CrtRand() % *(short*)(model + 0x1e));
                if (*(short*)(e + 0x9a) <= 0) *(short*)(e + 0x9a) = 0x0b;
            }

            // Ghidra: LAB_00446bcc
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x9b, e[0x0b], e[0x0a]);

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 5)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x1e;
                *(short*)(e + 0x98) = 0;
            }
        }

        // Ghidra: LAB_00446c09 — trente images de bourdon, sans plus rien semer.
        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x12))--;
        (*(short*)(e + 0x98))++;

        // MORTE : +0x98 vient d'etre incremente et vaut 1 a 4, jamais 0. Transcrite parce qu'elle
        // est dans le corps.
        if (*(short*)(e + 0x98) == 0)
        {
            Aud.PlayPositionalSound(0x14, 0xf4e, 0x1000, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00446c43
        if (*(short*)(e + 0x98) == 2)
        {
            Aud.PlayPositionalSound(0x14, 0x1000, 0x1000, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00446c64
        if (*(short*)(e + 0x98) == 4) *(short*)(e + 0x98) = 0;

        // Ghidra: LAB_00446c77
        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00446c90 @ 0x00446c90 — <b>le sous-type <c>0x9b</c></b>, creneau 155 de
    /// <see cref="s_StaticEntityHandlers_00552230"/>. La fille de
    /// <see cref="EntityHandler_00446A90"/>, et son seul appelant. Decompilee.
    ///
    /// <para>
    /// <b>Elle emploie les trois mesures tirees par sa mere</b>, et c'est ce qui les identifie :
    /// <c>+0x14</c> l'ecarte du centre sur un cap tire au hasard (sinus et cosinus), <c>+0x9a</c>
    /// borne le tirage qui la souleve, <c>+0x16</c> devient son echelle <c>+0x28</c>/<c>+0x2a</c>.
    /// Elle recoit enfin une montee constante de <c>hasard % 6</c> unites par image, rangee dans
    /// <c>+0x5c</c>.
    /// </para>
    /// <para>
    /// <b>Son atlas est la queue de <see cref="s_Descriptors_005515E0"/></b> : les trois derniers
    /// enregistrements de la plage, <c>0x00551618</c>-<c>0x0055162f</c>, qui y etaient releves sans
    /// consommateur connu. Le corps tire <c>hasard % 3</c> — <b>et le retire a chaque image</b>,
    /// donc l'entite papillote entre les trois images au lieu de les defiler.
    /// </para>
    /// <para>
    /// <b>Elle s'eteint de temps, pas de teinte</b> : vingt images de compte a rebours, pendant que
    /// la teinte part de <c>0xb4</c> et perd un huitieme d'elle-meme par image — une decroissance
    /// proportionnelle, la seule de la famille, la ou les sœurs retirent une constante.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446C90(byte* e)
    {
        if (e == null) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // Ghidra: &DAT_00551618 — les trois derniers enregistrements de la plage 0x005515e0.
            *(byte**)(e + 0x3c) = Descriptors005515E0() + 0x38;
            e[0x40] = (byte)(CrtRand() % 3);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x14;

            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;
            *(short*)(e + 0x2e) = -0x3c;                  // Ghidra: 0xffc4

            // L'echelle vient de la mesure tiree par la mere.
            *(short*)(e + 0x28) = *(short*)(e + 0x16);
            *(short*)(e + 0x2a) = *(short*)(e + 0x16);

            // Ghidra ecrit l'idiome MSVC du modulo SIGNE par 0x1000 ; rand() etant positif, il se
            // reduit a un et logique.
            int yaw = CrtRand() % 0x1000;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(yaw) * *(short*)(e + 0x14);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(yaw) * *(short*)(e + 0x14);
            *(int*)(e + 0x20) += (CrtRand() % *(short*)(e + 0x9a)) * 0x1000;

            *(int*)(e + 0x5c) = CrtRand() % 6;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            e[0x40] = (byte)(CrtRand() % 3);

            // Ghidra: `(char)((int)-(uint)t >> 3) + t` — la teinte perd un huitieme d'elle-meme.
            e[0x38] = (byte)(e[0x38] + (sbyte)(-(int)e[0x38] >> 3));
            e[0x39] = (byte)(e[0x39] + (sbyte)(-(int)e[0x39] >> 3));
            e[0x3a] = (byte)(e[0x3a] + (sbyte)(-(int)e[0x3a] >> 3));

            *(short*)(e + 0x12) -= 1;
            *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;

            if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00447630 @ 0x00447630 — <b>le sous-type <c>0x3b</c></b>, creneau 59 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>14 emplois</b>, dont Durandal et la Corne
    /// du Minotaure. 416 octets, <b>absent de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00447630.asm</c>.
    ///
    /// <para>
    /// <b>C'est l'eclat d'impact de <see cref="EntityHandler_00447280"/> qui REPOUSSE.</b> Le corps
    /// est le meme a trois details pres — meme couronne de huit etincelles <c>0x98</c>
    /// (<see cref="EntityHandler_004474C0"/>) semees a huit angles reguliers par <c>+0x16</c>, meme
    /// anneau <c>0xa4</c> (<see cref="EntityHandler_00447380"/>), memes deux sons, meme vie de deux
    /// images — puis il ajoute une <b>poussee</b> a la victime.
    /// </para>
    /// <para>
    /// <b>La poussee</b> : <c>(300 - moyenne des trois dimensions du modele) / 8</c>, plancher a
    /// zero, projetee sur le cap <c>+0x32</c> du <b>frappeur</b> (<c>+0x6c</c>) et posee dans les
    /// trois vitesses de la <b>victime</b> (<c>+0x70</c>) : <c>+0x50</c> en x, <c>+0x58</c> en z, et
    /// <c>0x18000</c> tout net en y — elle decolle. Le bit <c>1</c> de son mot d'etat est allume,
    /// celui qui fait vivre la vitesse. <b>Une grosse bete est donc moins repoussee qu'une
    /// petite</b>, et au-dela de 900 unites de dimension totale elle ne bouge plus du tout.
    /// </para>
    /// <para>
    /// <b>Les deux entites qu'il lit ne sont pas la meme</b> : <c>+0x70</c> est la victime — c'est
    /// d'elle qu'il prend sa position et son modele —, <c>+0x6c</c> est le frappeur, dont il ne
    /// prend que le cap. La ou <c>0x00447280</c> abandonne si <c>+0x70</c> est nul, celui-ci
    /// retombe sur <c>s_EntityList_005de558</c> pour les deux.
    /// </para>
    /// <para>
    /// <b>Trois lignes mort-nees, comme chez son jumeau</b> : l'encombrement borne a <c>0x15e</c>
    /// puis a <c>0x64</c> est ecrase par la constante <c>0xaa</c> quatre instructions plus loin —
    /// c'est <c>0xaa</c>, et non <c>0xb4</c>, qui part dans l'anneau. Le calcul est transcrit parce
    /// qu'il est dans le corps.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447630(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            Aud.PlayPositionalSound(9, 0xb52, 0x1200, (int*)(e + 0x1c));
            *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_00447663
        if (*(short*)(e + 0x10) != 0) return;

        byte* struck = *(byte**)(e + 0x70);
        if (struck == null) struck = Res.s_EntityList_005de558;

        byte* striker = *(byte**)(e + 0x6c);
        if (striker == null) striker = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044768d
        *(int*)(e + 0x1c) = *(int*)(struck + 0x1c);
        *(int*)(e + 0x20) = *(int*)(struck + 0x20);
        *(int*)(e + 0x24) = *(int*)(struck + 0x24);

        ResolveEntityBlocks(struck, out _, out _, out byte* model);

        // Mort-ne : ecrase par 0xaa plus bas. Les additions sont sur SEIZE bits dans le binaire.
        short bulk = (short)(*(short*)(model + 0x1e) + *(short*)(model + 0x1c) + *(short*)(model + 0x20));
        *(short*)(e + 0x14) = bulk;
        if (bulk > 0x15e) *(short*)(e + 0x14) = 0x15e;
        if (*(short*)(e + 0x14) < 0x64) *(short*)(e + 0x14) = 0x64;

        // Ghidra: LAB_004476e8
        *(int*)(e + 0x20) += 0x55000;
        *(short*)(e + 0x14) = 0xaa;

        SpawnCastEntity_0047AAE0(e, 0xa4, 0, -1);

        // Ghidra: LAB_0044770e — huit etincelles, un huitieme de tour d'ecart. L'angle passe par
        // +0x16, que la recopie des 0x27 mots transmet a la fille.
        for (short angle = 0; angle <= 0xe00; angle += 0x200)
        {
            *(short*)(e + 0x16) = angle;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x98, 0, -1);
        }

        // La POUSSEE. Ici la somme des trois dimensions est faite sur 32 bits, contrairement au
        // calcul mort-ne ci-dessus.
        int extent = *(short*)(model + 0x20) + *(short*)(model + 0x1e) + *(short*)(model + 0x1c);
        short push = (short)(0x12c - extent / 3);
        *(short*)(e + 0x14) = push;
        if (push < 0) *(short*)(e + 0x14) = 0;

        // Ghidra: LAB_0044776c — le cap du FRAPPEUR, la vitesse de la VICTIME.
        int heading = *(short*)(striker + 0x32);
        int strength = *(short*)(e + 0x14) >> 3;

        *(int*)(struck + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) * strength;
        *(int*)(struck + 0x58) = Winsys3d.GetCosTableEntry(heading) * strength;
        *(int*)(struck + 0x54) = 0x18000;
        *(uint*)struck |= 1;

        Aud.PlayPositionalSound(5, 0x320, 0x1200, (int*)(e + 0x1c));
        *(short*)(e + 0x10) = 1;
    }

    /// <summary>
    /// Ghidra: FUN_00404920 @ 0x00404920 — <b>le sous-type <c>0x55</c></b>, creneau 85 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. <b>10 emplois</b>, dont Genocide et la Faux
    /// de la mort. 298 octets, <b>absent de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00404920.asm</c>.
    ///
    /// <para>
    /// <b>Le troisieme effet qui TIENT sa cible</b>, apres <see cref="EntityHandler_00402FC0"/>
    /// (<c>0x54</c>) et <see cref="EntityHandler_004041C0"/> (<c>0x52</c>) : meme squelette — la
    /// victime en <c>+0x6c</c>, collee a la position de l'effet, animation <b>2</b> imposee et
    /// defilee a la main une image sur deux jusqu'a la derniere image de la sequence, bit 3 du mot
    /// d'etat pose puis rendu a l'extinction.
    /// </para>
    /// <para>
    /// <b>Quatre differences avec les deux sœurs</b>, toutes lues dans le desassemblage :
    /// </para>
    /// <list type="number">
    /// <item><b>Il ne pose que le bit 3</b> (<c>0x8</c>), pas <c>0x1008</c> : la victime n'est pas
    /// marquee « rendue comme modele du paquet ».</item>
    /// <item><b>La duree est bien plus courte</b> — <c>hasard % 5 + 6</c> images, contre 15 a 19
    /// chez les deux autres.</item>
    /// <item><b>Il n'a pas de compteur de parite propre</b> : la cadence d'une image sur deux est
    /// prise sur le bit bas du <b>compte a rebours</b> <c>+0x12</c> lui-meme.</item>
    /// <item><b>Il seme deux choses au lieu d'une</b> : a la naissance un unique effet <c>0x50</c>
    /// (<see cref="EntityHandler_00403C10"/>) — engendre depuis la <b>victime</b>, pas depuis
    /// l'effet, donc c'est elle qu'il suivra —, puis une braise <c>0x37</c>
    /// (<see cref="EntityHandler_00404A50"/>) a chaque image.</item>
    /// </list>
    /// <para>
    /// <c>+0x14</c> recoit la <b>moyenne</b> des trois etendues du bloc de modele, comme chez
    /// <c>0x004041c0</c> ; la braise s'en sert pour tirer ses trois vitesses.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404920(byte* effect)
    {
        byte* victim = *(byte**)(effect + 0x6c);
        if (victim == null) return;

        *(int*)(effect + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(victim + 0x20);
        *(int*)(effect + 0x24) = *(int*)(victim + 0x24);

        // L'indice 2 est litteral dans l'instruction (`mov edx,[ecx+8]`), donc c'est bien la
        // sequence qu'on vient de forcer.
        *(short*)(victim + 0x18) = 2;
        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + victim[4] * 4))[2];

        *(uint*)victim |= 8;

        if (*(short*)(effect + 0x10) == 0)
        {
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = (short)(CrtRand() % 5 + 6);

            victim[0x0a] = 0;
            *(short*)(victim + 0x1a) = 0;

            ResolveEntityBlocks(victim, out _, out _, out byte* model);

            // Ghidra: `imul 55555556h`, soit une division par trois.
            int extent = *(short*)(model + 0x1c) + *(short*)(model + 0x1e) + *(short*)(model + 0x20);
            *(short*)(effect + 0x14) = (short)(extent / 3);

            // La source est la VICTIME, relue en +0x6c — pas l'effet.
            SpawnEffectEntity(*(byte**)(effect + 0x6c), 0x50, 0, -1);
        }

        // Ghidra: LAB_004049e5 — la premiere image passe ici aussi.
        if (*(short*)(effect + 0x10) != 1) return;

        *(short*)(effect + 0x12) -= 1;

        SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x37, 0, -1);

        if (*(short*)(effect + 0x12) < 0)
        {
            *(uint*)effect &= 0x7fffffff;
            *(uint*)victim &= 0xfffffff7;
        }

        // Ghidra: LAB_00404a19 — une image d'animation sur deux, bornee a la derniere de la
        // sequence. La parite est celle du compte a rebours lui-meme.
        if ((effect[0x12] & 1) != 0)
        {
            *(short*)(victim + 0x1a) += 1;

            ushort frames = *(ushort*)(animation + 0x16);
            if (*(short*)(victim + 0x1a) >= (int)frames)
            {
                *(ushort*)(victim + 0x1a) = (ushort)(frames - 1);
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f098</c> — <b>quatre descripteurs d'atlas</b>, la plage
    /// <c>0x0054f098</c>-<c>0x0054f0b7</c>, celle de la braise de
    /// <see cref="EntityHandler_00404A50"/>. Relevee dans <c>Blaze.exe</c> le 2026-09-01.
    ///
    /// <para>
    /// <b>La longueur est lue dans les deux sens.</b> Le corps fait monter <c>+0x40</c> jusqu'a 4
    /// puis le retient a 3 : il consomme donc exactement quatre enregistrements. Et le cinquieme
    /// enregistrement de la plage (<c>0x0054f0b8</c>) change de bande — son octet <c>+0x04</c>
    /// passe de <c>0x01</c> a <c>0x00</c> —, ce qui borne la table du cote du binaire.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F098 =
    [
        0xd8, 0xd0, 0x0f, 0x0f, 0x01, 0x00, 0x10, 0x00,   // 0x0054f098
        0xd8, 0xe0, 0x0f, 0x0f, 0x01, 0x00, 0x10, 0x00,   // 0x0054f0a0
        0xd8, 0xf0, 0x0f, 0x0f, 0x01, 0x00, 0x10, 0x00,   // 0x0054f0a8
        0xe8, 0xf0, 0x0f, 0x0f, 0x01, 0x00, 0x10, 0x00,   // 0x0054f0b0
    ];

    private static byte* s_Descriptors0054F098Storage;

    private static byte* Descriptors0054F098()
        => NativeCopy(s_Descriptors_0054F098, ref s_Descriptors0054F098Storage);

    /// <summary>
    /// Ghidra: FUN_00404a50 @ 0x00404a50 — <b>le sous-type <c>0x37</c></b>, creneau 55 de
    /// <see cref="s_StaticEntityHandlers_00552230"/>. La braise que
    /// <see cref="EntityHandler_00404920"/> lache a chaque image, et son seul appelant. Decompilee.
    ///
    /// <para>
    /// <b>Elle jaillit et retombe.</b> Ses trois vitesses sont tirees a la naissance a partir du
    /// seul champ que sa mere lui a passe, <c>+0x14</c> — la moyenne des dimensions du modele de la
    /// victime : les deux horizontales dans <c>[-etendue/16, etendue/8 - etendue/16[</c>, donc
    /// centrees sur zero, et la verticale dans <c>[etendue/16, etendue/8[</c>, donc toujours vers
    /// le haut. Elle nait deja surelevee de <c>(etendue &amp; ~3) * 0x400</c>.
    /// </para>
    /// <para>
    /// <b>Puis la pesanteur</b> : la vitesse verticale perd <c>0x1000</c> par image, sans plancher,
    /// et l'entite s'eteint quand elle passe sous <c>-0xa000</c> — soit dix images de chute apres
    /// avoir atteint le sommet, quelle que soit la hauteur atteinte.
    /// </para>
    /// <para>
    /// Son atlas est <see cref="s_Descriptors_0054F098"/>, quatre images defilees une image sur
    /// deux et <b>retenues sur la derniere</b> : le corps remet le compteur a 3 des qu'il atteint 4.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404A50(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int extent = *(short*)(e + 0x14);

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F098();
            e[0x40] = 0;

            // Ghidra: `(x + (x >> 31 & 7)) >> 3` et `(x + (x >> 31 & 0xf)) >> 4`, les divisions
            // signees par 8 et par 16 tronquees vers zero de l'idiome MSVC.
            *(int*)(e + 0x50) = (CrtRand() % (extent / 8) - extent / 16) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % (extent / 8) - extent / 16) * 0x1000;

            int half = extent / 16;

            e[0x0a] = 0x0c;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 0;

            *(int*)(e + 0x54) = (CrtRand() % half + half) * 0x1000;
            *(int*)(e + 0x20) += (int)(((uint)extent & 0xfffffffc) * 0x400);

            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;

            e[0x38] = 0xa0;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int rise = *(int*)(e + 0x54) - 0x1000;

            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x24) += *(int*)(e + 0x58);
            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(int*)(e + 0x54) = rise;

            if (rise < -0xa000) *(uint*)e &= 0x7fffffff;

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 1)
            {
                *(short*)(e + 0x12) = 0;
                e[0x40]++;
                if (e[0x40] == 4) e[0x40] = 3;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00447050 @ 0x00447050 — <b>la spirale du sous-type <c>0x9a</c></b>, creneau 154
    /// de <see cref="s_StaticEntityHandlers_00552230"/>. La fille de
    /// <see cref="EntityHandler_00446F30"/>, et son seul appelant. Decompilee.
    ///
    /// <para>
    /// <b>Elle tourne autour du point ou elle est nee et monte</b> : cap tire au hasard sur le tour
    /// entier puis avancant de <b>600 par image</b>, rayon <c>+0x14</c>, montee <c>rand % 10 +
    /// 14</c> unites par image. Position d'origine figee dans <c>+0x50</c>..<c>+0x58</c>.
    /// </para>
    /// <para>
    /// <b>Elle prend son echelle de la mesure que sa mere a faite</b> : <c>+0x28</c> et
    /// <c>+0x2a</c> recoivent le <c>+0x16</c> herite, c'est-a-dire la moyenne des trois dimensions
    /// du modele du porteur. Un gros monstre a une grosse spirale — c'est tout l'objet de la mesure
    /// de <c>0x00446f30</c>.
    /// </para>
    /// <para>
    /// <b>Elle vit quatorze images</b> : son image d'atlas avance toutes les <b>deux</b> images
    /// (le compteur est <c>+0x9a</c>, detourne de son role habituel) et elle s'eteint a la
    /// septieme. Sa teinte part de <c>a0/a0/a0</c> et perd treize par image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447050(byte* e)
    {
        if (e == null) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005515E0();
            e[0x40] = 0;
            *(short*)(e + 0x10) = 1;

            *(ushort*)(e + 0x9a) = 0;
            *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);   // le cap

            // L'echelle vient de la mesure du porteur, faite par la mere.
            *(ushort*)(e + 0x28) = *(ushort*)(e + 0x16);
            *(ushort*)(e + 0x2a) = *(ushort*)(e + 0x16);

            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);

            e[0x38] = 0xa0;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;

            *(int*)(e + 0x5c) = CrtRand() % 10 + 0x0e;             // la montee par image
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short yaw = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * *(short*)(e + 0x14)
                                + *(int*)(e + 0x50);

            int cosine = Winsys3d.GetCosTableEntry(yaw);
            *(short*)(e + 0x12) = (short)(yaw + 600);

            *(int*)(e + 0x24) = cosine * *(short*)(e + 0x14) + *(int*)(e + 0x58);
            *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;

            BrightenEntityTint_00404DE0(e, -13, -13, -13);

            (*(short*)(e + 0x9a))++;

            if (*(short*)(e + 0x9a) == 2)
            {
                *(ushort*)(e + 0x9a) = 0;

                e[0x40]++;
                if (e[0x40] == 7) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00447280</c> — <b>l'entree 57 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 255 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; relevé garde sous <c>Legacy/Raw/Blaze/FUN_00447280.asm</c>.
    ///
    /// <para>
    /// <b>C'est l'ECLAT D'IMPACT que les neuf routines d'intelligence font naitre a chaque coup
    /// porte</b> — le <c>SpawnEffectEntity(e, 0x39, 0, -1)</c> du Crabe, du Grizzly et de
    /// l'Homme-lezard. Il ne vit que <b>deux images</b> : la premiere pose la gerbe, la seconde
    /// joue un second son et eteint l'entite.
    /// </para>
    /// <para>
    /// La gerbe est une <b>couronne de huit etincelles</b> (<see cref="EntityHandler_004474C0"/>,
    /// sous-type <c>0x98</c>), semees a huit angles reguliers ecrits dans <c>+0x16</c> avant chaque
    /// naissance — c'est ainsi que la fille herite du sien —, plus un anneau
    /// (<see cref="EntityHandler_00447380"/>, sous-type <c>0xa4</c>). L'eclat se souleve de
    /// <c>0x5a000</c> avant de semer.
    /// </para>
    /// <para>
    /// <b>Trois lignes mort-nees.</b> Le corps calcule l'encombrement de la victime a partir de son
    /// bloc de modele, le borne a <c>0x15e</c> puis a <c>0x64</c>, et <b>ecrase le resultat par la
    /// constante <c>0xb4</c></b> trois instructions plus loin. Le calcul est transcrit parce qu'il
    /// est dans le corps ; il ne sert a rien, et il ne consomme pas de tirage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447280(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            Aud.PlayPositionalSound(9, 0xb52, 0x1200, (int*)(e + 0x1c));
            *(uint*)e &= 0x7fffffff;
        }

        if (*(short*)(e + 0x10) != 0) return;

        byte* struck = *(byte**)(e + 0x70);
        if (struck == null) return;

        ResolveEntityBlocks(struck, out _, out _, out byte* model);

        // Mort-ne : ecrase par 0xb4 plus bas. Les additions sont sur SEIZE bits dans le binaire.
        short bulk = (short)(*(short*)(model + 0x20) + *(short*)(model + 0x1e) + *(short*)(model + 0x1c));
        *(short*)(e + 0x14) = bulk;
        if (bulk > 0x15e) *(short*)(e + 0x14) = 0x15e;
        if (*(short*)(e + 0x14) < 0x64) *(short*)(e + 0x14) = 0x64;

        *(int*)(e + 0x20) += 0x5a000;
        *(short*)(e + 0x14) = 0xb4;

        SpawnCastEntity_0047AAE0(e, 0xa4, 0, -1);

        // Huit etincelles, un huitieme de tour d'ecart. L'angle passe par +0x16, que la copie de
        // 0x27 dwords de FUN_0047ac40 transmet a la fille.
        for (short angle = 0; angle <= 0xe00; angle += 0x200)
        {
            *(short*)(e + 0x16) = angle;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x98, 0, -1);
        }

        Aud.PlayPositionalSound(5, 0x320, 0x1200, (int*)(e + 0x1c));
        *(short*)(e + 0x10) = 1;
    }

    /// <summary>
    /// Ghidra: FUN_00447380 @ 0x00447380 — <b>l'entree 164 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, l'<b>anneau</b> que
    /// <see cref="EntityHandler_00447280"/> pose au point d'impact.
    ///
    /// <para>
    /// Il naît blanc et gros (les trois echelles a 500), tourne d'un quart de tour
    /// (<c>+0x32 += 0xc00</c>), et <b>s'ouvre en s'amortissant</b> : son rayon <c>+0x9a</c> croit
    /// de <c>+0x5c</c>, lequel est <b>divise par deux a chaque image</b> — l'anneau s'elargit vite
    /// puis se fige. Ses echelles rejoignent <c>+0x14 * 0x28</c> par quarts. Il s'assombrit de
    /// quinze par canal et meurt quand le rouge atteint zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447380(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;

            *(short*)(e + 0x32) += 0xc00;

            *(short*)(e + 0x28) = 500;
            *(short*)(e + 0x2a) = 500;
            *(short*)(e + 0x2c) = 500;

            e[4] = 7;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x34) = 0x400;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int target = *(short*)(e + 0x14) * 0x28;
        *(short*)(e + 0x28) += (short)((target - *(short*)(e + 0x28)) >> 2);
        *(short*)(e + 0x2a) += (short)((target - *(short*)(e + 0x2a)) >> 2);
        *(short*)(e + 0x2c) += (short)((target - *(short*)(e + 0x2c)) >> 2);

        int course = *(short*)(e + 0x32) - 0xc00;
        short radius = *(short*)(e + 0x9a);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);

        *(short*)(e + 0x9a) = (short)(radius + *(int*)(e + 0x5c));
        *(int*)(e + 0x5c) -= *(int*)(e + 0x5c) >> 1;

        BrightenEntityTint_00404DE0(e, -0x0f, -0x0f, -0x0f);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x004474c0</c> — <b>l'entree 152 de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 358 octets, <b>absente de
    /// <c>Blaze.c</c></b>. C'est l'<b>etincelle</b> de la couronne que
    /// <see cref="EntityHandler_00447280"/> seme par huit.
    ///
    /// <para>
    /// Chacune part dans sa direction — l'angle que le pere lui a ecrit en <c>+0x16</c> — et
    /// s'eloigne sur une sphere de rayon <c>+0x9a</c>, croissant de <c>+0x5c</c> qui perd un quart
    /// a chaque image : un jet vif qui retombe. Ses deux premieres echelles rejoignent
    /// <c>+0x14 * 30</c> par quarts ; la troisieme n'est jamais touchee.
    /// </para>
    /// <para>
    /// <b>Elle a trois images et les fait defiler elle-meme</b> : <c>+0x40</c> monte de un par
    /// image et repart a zero apres deux, en indexant <see cref="s_SparkDescriptors_005515A0"/>.
    /// Elle s'assombrit de quinze par canal et meurt quand le rouge atteint zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_004474C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;

            *(byte**)(e + 0x3c) = SparkDescriptors();        // Ghidra: &DAT_005515a0
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 0x3c;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int course = *(short*)(e + 0x32) + 0x400;
        short radius = *(short*)(e + 0x9a);
        short spread = *(short*)(e + 0x16);

        *(int*)(e + 0x24) =
            ((Winsys3d.GetCosTableEntry(course) * radius) >> 0xc)
            * (int)Winsys3d.GetSinTableEntry(spread) + *(int*)(e + 0x58);

        *(int*)(e + 0x1c) =
            (((int)Winsys3d.GetSinTableEntry(course) * radius) >> 0xc)
            * (int)Winsys3d.GetSinTableEntry(spread) + *(int*)(e + 0x50);

        *(int*)(e + 0x20) = Winsys3d.GetCosTableEntry(spread) * radius + *(int*)(e + 0x54);

        // Ghidra: `add di,[esi+5Ch]` — l'addition est sur SEIZE bits, le mot bas de +0x5c.
        *(short*)(e + 0x9a) = (short)(radius + *(short*)(e + 0x5c));
        *(int*)(e + 0x5c) -= *(int*)(e + 0x5c) >> 2;

        int target = *(short*)(e + 0x14) * 30;
        *(short*)(e + 0x28) += (short)((target - *(short*)(e + 0x28)) >> 2);
        *(short*)(e + 0x2a) += (short)((target - *(short*)(e + 0x2a)) >> 2);

        BrightenEntityTint_00404DE0(e, -0x0f, -0x0f, -0x0f);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

        // L'image suivante, sur trois.
        e[0x40] += 1;
        if (e[0x40] == 3) e[0x40] = 0;
    }
}
