// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA FORET (1/2) — l'eclaboussure, l'eclat, les braises, le flash, la gerbe,
// la bulle.
//
// SIX FAMILLES, ET AUCUNE N'EST UN CORPS UNIQUE. Le compilateur de 1998 a emis chacune deux,
// trois ou quatre fois, et a DUPLIQUE LA DONNEE AVEC : les trois planches de la bulle
// (0x0054f6f8, 0x0054f818, 0x0054f930) portent les memes cinq enregistrements. Chaque bandeau
// `// ====` ci-dessous dit combien d'exemplaires, et ce qui differe entre eux - souvent une seule
// ligne, l'adresse de la planche ; parfois un carre a deux parametres croises.
//
// Les corps sont donc ecrits UNE fois, avec la planche en parametre. C'est une unite reelle, pas
// un decoupage de confort : plusieurs sites l'emploient a l'identique.
//
// LA LECON DES PLANCHES TIENT ICI PLUS QU'AILLEURS. 0x0054f8f0 est le DEUXIEME enregistrement
// d'un bloc dont un autre gestionnaire ne lit que le premier : releve au plus juste, il aurait
// manque a l'autre. On releve la PLAGE, jamais le descripteur.
// Lancer `sh OpenForesia.Tools/check-descriptors.sh` apres toute retouche.
//
// Suite dans `ForestParticlesB.cs`. Voir Documentation/Reference/DataSymbols.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // L'ECLABOUSSURE DU PAS, EN TROIS EXEMPLAIRES (2026-08-30)
    //
    // 0x0040f7f0, 0x00412b30 et 0x00416120 sont le meme corps. Compares instruction par
    // instruction sur 77 lignes, ils ne different que d'une seule : l'adresse de la planche.
    // Leurs trois planches — 0x0054f7a8, 0x0054f8c8, 0x0054f9c0 — portent les memes quatre
    // enregistrements. C'est la sixieme famille de la foret batie ainsi.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f8c8</c> — la deuxieme copie de la planche du remous,
    /// quatre enregistrements jusqu'a <c>DAT_0054f8e8</c>.</summary>
    private static readonly byte[] s_SplashDescriptors_0054F8C8 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f8c8
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f8d0
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f8d8
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f8e0
    ];

    private static byte* s_SplashDescriptors0054F8C8Storage;

    private static byte* Descriptors0054F8C8()
        => NativeCopy(s_SplashDescriptors_0054F8C8, ref s_SplashDescriptors0054F8C8Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f9c0</c> — la troisieme copie, jusqu'a
    /// <c>DAT_0054f9e0</c>.</summary>
    private static readonly byte[] s_SplashDescriptors_0054F9C0 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f9c0
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f9c8
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f9d0
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f9d8
    ];

    private static byte* s_SplashDescriptors0054F9C0Storage;

    private static byte* Descriptors0054F9C0()
        => NativeCopy(s_SplashDescriptors_0054F9C0, ref s_SplashDescriptors0054F9C0Storage);

    /// <summary>
    /// Ghidra: <c>0x0040f7f0</c> — la premiere des trois eclaboussures. Le corps est dans
    /// <see cref="TickFootSplash"/>.
    ///
    /// <para>Atteinte par le sous-type <c>0xfe</c> : la passe
    /// <see cref="TickEntityTable_005f2d40"/> indexe <c>0x0056afc8</c>, deborde de trois tables et
    /// retombe sur l'entree <c>0x3e</c> de <c>0x0056b2c8</c>. C'est le crochet de contact au sol
    /// (<see cref="EntityHandler_00541440"/>) qui la seme, un pas sur deux, quand le sol porte le
    /// bit <c>0x200</c>.</para>
    /// </summary>
    private static void EntityHandler_0040F7F0(byte* e)
        => TickFootSplash(e, Descriptors0054F7A8());   // Ghidra: &DAT_0054f7a8

    /// <summary>Ghidra: <c>0x00412b30</c> — la deuxieme.</summary>
    private static void EntityHandler_00412B30(byte* e)
        => TickFootSplash(e, Descriptors0054F8C8());

    /// <summary>Ghidra: <c>0x00416120</c> — la troisieme.</summary>
    private static void EntityHandler_00416120(byte* e)
        => TickFootSplash(e, Descriptors0054F9C0());

    /// <summary>
    /// Le corps commun des trois eclaboussures. Releve au desassembleur, 231 octets —
    /// <c>Legacy/Raw/Blaze/FUN_0040F7F0.asm</c> ; les deux copies n'ont pas leur propre releve,
    /// leur diff tenant en une ligne.
    ///
    /// <para>
    /// <b>Elle ne bouge pas et ne compte que ses images.</b> La naissance se disperse une fois pour
    /// toutes — les trois coordonnees sont ecartees d'un tirage sur <c>[-20, +19]</c> unites
    /// entieres, et les deux echelles partent de <c>0x1400</c> moins un tirage sur <c>0x7ff</c>,
    /// soit une gerbe toujours plus petite que la taille nominale, jamais plus grande. Passe la
    /// naissance, plus rien ne touche ni la position ni l'echelle.
    /// </para>
    /// <para>
    /// <b>Huit images de vie.</b> <c>+0x12</c> compte jusqu'a 2, se remet a zero et fait avancer
    /// l'indice d'atlas <c>+0x40</c> : chacune des quatre tuiles tient deux images, et l'entite
    /// s'eteint a la quatrieme.
    /// </para>
    /// </summary>
    private static void TickFootSplash(byte* e, byte* atlas)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = atlas;
            e[0x0a] = 9;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 40 - 20) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 40 - 20) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 40 - 20) << 0xc;

            *(short*)(e + 0x28) = (short)(0x1400 - SignedMask(CrtRand(), 0x7ff));

            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;

            *(short*)(e + 0x2a) = (short)(0x1400 - SignedMask(CrtRand(), 0x7ff));
        }

        // La premiere image passe ici aussi : la naissance pose +0x10 = 1.
        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 2) return;

        *(short*)(e + 0x12) = 0;
        e[0x40] += 1;
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f720</c> — la plage relevee jusqu'a <c>DAT_0054f758</c>, soit
    /// <b>sept</b> enregistrements de huit octets, tous en <c>0x18 x 0x18</c> sur la rangee
    /// <c>v = 0x30</c>.
    ///
    /// <para>
    /// <b>Ce sont deux groupes, pas un.</b> Les quatre premiers (<c>u</c> = 0x48, 0x60, 0x78,
    /// 0x90) portent <c>01 02</c> en cinquieme et sixieme octets ; les trois derniers
    /// (<c>u</c> = 0x00, 0x18, 0x30) portent <c>00 01</c>. Seuls les quatre premiers sont
    /// atteignables depuis <see cref="EntityHandler_0040F370"/>, dont le compteur <c>+0x40</c>
    /// s'arrete a 4 — les trois autres appartiennent a un groupe voisin qu'aucun symbole de
    /// <c>Blaze.c</c> ne nomme.
    /// </para>
    /// <para>
    /// La plage entiere est relevee quand meme : l'indexation se fait <b>en avant</b> depuis
    /// l'adresse donnee, et un descripteur releve seul n'a aucun garde-fou — c'est la lecon de
    /// <c>0x005646c0</c> dans <c>DataSymbols.md</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F720 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f720
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f728
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f730
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f738
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f740 — l'autre groupe
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f748
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f750
    ];

    private static byte* s_Descriptors0054F720Storage;

    private static byte* Descriptors0054F720()
        => NativeCopy(s_Descriptors_0054F720, ref s_Descriptors0054F720Storage);

    /// <summary>
    /// Ghidra: FUN_0040f370 @ 0x0040f370 — <b>la poussiere du bloc pousse</b>. Decompilee dans
    /// <c>Blaze.c</c>. Sous-type <c>0xfd</c> de la table installee par zone : la passe
    /// <see cref="TickEntityTable_005f2d40"/> indexe <c>0x0056afc8</c>, deborde de trois tables et
    /// retombe sur <c>0x0056b2c8</c>. Presente dans les sous-zones 0 a 4 de la foret.
    ///
    /// <para>
    /// Signalee en jeu par Vincent le 2026-08-30, en poussant le bloc sur le teleporteur — c'est
    /// le premier gestionnaire que la sonde ait designe <b>avant</b> qu'il soit ecrit : elle note
    /// l'adresse avant d'appeler, donc le plantage lui-meme l'a enregistre.
    /// </para>
    /// <para>
    /// <b>Elle ne bouge pas, elle s'eteint.</b> La naissance disperse la position sur les trois
    /// axes — <c>[-40, +39]</c> unites entieres a l'horizontale, <c>[0, +14]</c> vers le haut —,
    /// tire une echelle de <c>0x800 + (rand &amp; 0x3ff)</c>, et pose une teinte <b>sombre</b> a
    /// <c>0x3c3c3c</c>. Cinq tirages en tout, dans cet ordre : echelle, X, Z, Y, duree.
    /// </para>
    /// <para>
    /// Ensuite chaque image assombrit d'un cran par canal
    /// (<see cref="BrightenEntityTint_00404DE0"/> avec <c>-1</c>) et decompte <c>+0x12</c>. A zero,
    /// l'image d'atlas suivante est prise et le compteur repart de <c>+0x14</c>, tire une fois
    /// pour toutes entre 5 et 9. <b>Quatre images d'atlas</b>, donc 20 a 36 images de jeu — la
    /// teinte, partie de 60, n'a pas le temps d'atteindre zero : c'est le compteur d'atlas qui
    /// tue l'entite, pas l'extinction.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040F370(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F720();   // Ghidra: &DAT_0054f720
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;
            e[0x38] = 0x3c;
            e[0x39] = 0x3c;
            e[0x3a] = 0x3c;

            short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x1c) += (CrtRand() % 0x50 - 0x28) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x50 - 0x28) * 0x1000;

            // Ghidra pose l'etat ENTRE le tirage et son emploi ; sans effet, transcrit a sa place.
            int rise = CrtRand();
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += (rise % 0xf) * 0x1000;

            short hold = (short)(CrtRand() % 5 + 5);
            *(short*)(e + 0x14) = hold;
            *(short*)(e + 0x12) = hold;
        }

        // La premiere image passe ici aussi : la naissance pose +0x10 = 1.
        if (*(short*)(e + 0x10) != 1) return;

        BrightenEntityTint_00404DE0(e, -1, -1, -1);

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) >= 0) return;

        // Ghidra: `(char)param_1[0x10] + 1` — l'octet de +0x40, en CHAR signe.
        byte frame = (byte)(e[0x40] + 1);
        *(short*)(e + 0x12) = *(short*)(e + 0x14);
        e[0x40] = frame;

        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0040edf0</c> — <b>le fondu au blanc de la transition</b>. 327 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0040EDF0.asm</c>. Entree de la table <c>0x0056b0c8</c> —
    /// le debordement de <c>0x0056adc8</c>, donc la passe <see cref="TickEntityTable_005fa5e0"/>
    /// pour un sous-type au-dela de <c>0xc0</c>. Presente dans les sous-zones 0 a 4 de la foret.
    ///
    /// <para>
    /// <b>Elle etablit le role des deux mots <c>0x005de024</c> et <c>0x005de028</c></b>, que
    /// <see cref="GameMemory.s_Unknown_005de024"/> disait « non etabli ». Ce sont <b>deux voiles
    /// plein cadre</b> : les trois octets bas portent la couleur, l'octet de poids fort commande
    /// l'affichage — c'est lui que la boucle de dessin teste avant d'appeler
    /// <see cref="FillScreenQuad_0051F940"/>. Ce corps-ci est le seul reconstruit qui les
    /// <b>anime</b>.
    /// </para>
    /// <para>
    /// <b>Un compte a rebours de 220 images, et rien d'autre.</b> La naissance fait naitre l'effet
    /// <c>0xfa</c> a sa propre position, puis <c>+0x16</c> avance d'une image par image :
    /// </para>
    /// <list type="bullet">
    /// <item><b>0 a 169</b> — elle ne fait rien. L'effet <c>0xfa</c> occupe l'ecran pendant ce
    /// temps ;</item>
    /// <item><b>170 a 194</b> — les deux voiles <b>montent</b> : <c>+10</c> par image sur les trois
    /// canaux de <c>0x005de028</c>, <c>+3</c> sur ceux de <c>0x005de024</c>, et les deux octets de
    /// commande passent a 1 ;</item>
    /// <item><b>195 a 219</b> — ils <b>redescendent</b>, <c>-10</c> et <c>-3</c>, aux memes
    /// cadences ;</item>
    /// <item><b>220</b> — l'entite s'eteint et <b>rend la main a son porteur</b> : le mot
    /// <c>+0x98</c> qu'elle transportait est ecrit dans le <c>+0x10</c> du porteur
    /// (<c>+0x6c</c>), c'est-a-dire dans son <b>etat</b>. C'est ainsi que la transition dit « c'est
    /// fini, reprends la » a ce qui l'a declenchee.</item>
    /// </list>
    /// <para>
    /// <b>Les additions sont sur huit bits et debordent.</b> Vingt-cinq images a <c>+10</c> font
    /// 250, qui tient ; vingt-cinq a <c>+3</c> font 75. La descente ramene exactement au point de
    /// depart, donc le voile revient a ce qu'il etait — pourvu que la montee soit allee au bout.
    /// Une entite eteinte en cours de montee laisserait l'ecran teinte : c'est la fragilite du
    /// binaire d'origine, transcrite telle quelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040EDF0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfa, 0, -1);
        }

        (*(short*)(e + 0x16))++;
        short age = *(short*)(e + 0x16);

        if (age >= 0xaa && age <= 0xc2)
        {
            Mem.s_Unknown_005de028 = ShiftScreenVeil(Mem.s_Unknown_005de028, 10);
            Mem.s_Unknown_005de024 = ShiftScreenVeil(Mem.s_Unknown_005de024, 3);
        }

        if (age >= 0xc3 && age <= 0xdb)
        {
            Mem.s_Unknown_005de028 = ShiftScreenVeil(Mem.s_Unknown_005de028, -10);
            Mem.s_Unknown_005de024 = ShiftScreenVeil(Mem.s_Unknown_005de024, -3);
        }

        if (*(short*)(e + 0x16) != 0xdc) return;

        *(uint*)e &= 0x7fffffff;

        // Ghidra: le porteur est relu et teste APRES l'extinction ; l'ordre est conserve.
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier != null) *(short*)(carrier + 0x10) = *(short*)(e + 0x98);
    }

    /// <summary>
    /// Les trois canaux d'un voile plein cadre, decales de <paramref name="delta"/>, l'octet de
    /// commande pose a 1. Les additions se font <b>sur huit bits</b>, comme les <c>add dl,al</c>
    /// du binaire : elles debordent au lieu de saturer.
    /// </summary>
    private static uint ShiftScreenVeil(uint veil, int delta)
    {
        byte r = (byte)((byte)veil + delta);
        byte g = (byte)((byte)(veil >> 8) + delta);
        byte b = (byte)((byte)(veil >> 0x10) + delta);

        return 0x01000000u | ((uint)b << 0x10) | ((uint)g << 8) | r;
    }

    // =====================================================================
    // Quatre effets de la table 0x0056b2c8, foret — la meme famille que l'eclaboussure du pas
    // (0x0040f7f0) : une naissance qui disperse, puis un decompte. Transcrits d'un bloc le
    // 2026-08-30, tous decompiles dans Blaze.c.
    //
    // Leurs quatre planches se suivent dans .data et se bornent l'une l'autre ; chacune est
    // relevee ENTIERE, pas seulement les enregistrements que son corps atteint. C'est la lecon de
    // 0x005646c0 dans DataSymbols.md : l'indexation se fait en avant, un descripteur releve seul
    // n'a aucun garde-fou.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f6f8</c> — cinq enregistrements jusqu'a
    /// <c>DAT_0054f720</c>. <see cref="EntityHandler_0040FAC0"/> n'atteint que le <b>premier</b> :
    /// son corps ne touche jamais l'indice <c>+0x40</c>, que la naissance a mis a zero. Les quatre
    /// suivants reprennent les memes <c>u, v</c> que <see cref="s_Descriptors_0054F720"/> avec un
    /// autre couple d'attributs (<c>00 03</c> au lieu de <c>01 02</c>) — une variante de la meme
    /// animation.</summary>
    private static readonly byte[] s_Descriptors_0054F6F8 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f6f8
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f700
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f708
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f710
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f718
    ];

    private static byte* s_Descriptors0054F6F8Storage;

    private static byte* Descriptors0054F6F8()
        => NativeCopy(s_Descriptors_0054F6F8, ref s_Descriptors0054F6F8Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f778</c> — <b>six</b> enregistrements jusqu'a
    /// <c>0x0054f7a8</c>, six tuiles de <c>0x18 x 0x18</c> sur deux rangees. Le compte se lit dans
    /// le corps : <see cref="EntityHandler_0040F8E0"/> pose son indice a <c>rand % 6</c> a chaque
    /// image.</summary>
    private static readonly byte[] s_Descriptors_0054F778 =
    [
        0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f778
        0x18, 0x00, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f780
        0x00, 0x18, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f788
        0x18, 0x18, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f790
        0x00, 0x30, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f798
        0x18, 0x30, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00,   // 0x0054f7a0
    ];

    private static byte* s_Descriptors0054F778Storage;

    private static byte* Descriptors0054F778()
        => NativeCopy(s_Descriptors_0054F778, ref s_Descriptors0054F778Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f7c8</c> — six enregistrements jusqu'a
    /// <c>DAT_0054f7f8</c>. <see cref="EntityHandler_0040F470"/> n'atteint que le <b>premier</b> :
    /// il pose <c>+0x40 = 0</c> et n'y revient jamais. Les quatre suivants sont des tuiles de
    /// <c>0x20 x 0x20</c> et le dernier une tuile <c>0x17 x 0x0f</c> d'une autre planche : trois
    /// groupes dans une seule plage.</summary>
    private static readonly byte[] s_Descriptors_0054F7C8 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f7c8
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f7d0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f7d8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f7e0
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f7e8
        0xa0, 0xf0, 0x17, 0x0f, 0x00, 0x00, 0x08, 0x00,   // 0x0054f7f0
    ];

    private static byte* s_Descriptors0054F7C8Storage;

    private static byte* Descriptors0054F7C8()
        => NativeCopy(s_Descriptors_0054F7C8, ref s_Descriptors0054F7C8Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f7f8</c> — deux enregistrements jusqu'a
    /// <c>DAT_0054f808</c>. <see cref="EntityHandler_0040F190"/> n'atteint que le premier ; le
    /// second est une tuile de <c>0x3f x 0x3f</c>, sans rapport de taille avec lui.</summary>
    private static readonly byte[] s_Descriptors_0054F7F8 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f7f8
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f800
    ];

    private static byte* s_Descriptors0054F7F8Storage;

    private static byte* Descriptors0054F7F8()
        => NativeCopy(s_Descriptors_0054F7F8, ref s_Descriptors0054F7F8Storage);

    /// <summary>
    /// Ghidra: FUN_0040f190 @ 0x0040f190 — <b>l'etincelle grise qui retombe</b>. Decompilee dans
    /// <c>Blaze.c</c>. Table <c>0x0056b2c8</c>, foret.
    ///
    /// <para>
    /// La naissance se disperse de <c>[-20, +19]</c> unites entieres sur les <b>trois</b> axes,
    /// prend une echelle de <c>0x400 + rand % 1000</c> et une teinte grise a <c>0x808080</c>.
    /// Ensuite elle <b>tombe</b> d'une unite entiere par image (<c>-0x1000</c> sur <c>+0x20</c>) et
    /// s'assombrit de six par canal, jusqu'a s'eteindre quand le canal rouge atteint zero — soit
    /// vingt-deux images, l'assombrissement etant borne a zero par
    /// <see cref="BrightenEntityTint_00404DE0"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040F190(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F7F8();   // Ghidra: &DAT_0054f7f8
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;

            short scale = (short)(CrtRand() % 1000 + 0x400);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) -= 0x1000;
        BrightenEntityTint_00404DE0(e, -6, -6, -6);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // L'ECLAT QUI JAILLIT, EN TROIS EXEMPLAIRES (2026-08-30)
    //
    // Comme la bulle : 0x0040f470, 0x00412950 et 0x00415f40 sont le meme corps, a l'adresse du
    // descripteur pres. Les trois pseudo-C de Blaze.c ne different que d'une ligne, celle qui pose
    // la planche — le diff tient en trois lignes.
    //
    // Les trois planches portent elles aussi les memes cinq enregistrements : 0x0054f7c8,
    // 0x0054f8e8 et 0x0054f9e0. Le corps ci-dessous pose l'indice a zero et n'y revient jamais :
    // il n'atteint donc que le PREMIER des cinq.
    //
    // Les quatre autres ne sont pas perdus pour autant — ce sont les quatre images des braises
    // (TickRisingEmber), qui prennent la meme planche huit octets plus loin. Deux gestionnaires
    // differents se partagent un bloc, chacun a son decalage : releve au plus juste sur le seul
    // enregistrement lu ici, le bloc aurait manque a l'autre.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f8e8</c> — cinq enregistrements jusqu'a
    /// <c>DAT_0054f910</c>, les memes que <see cref="s_Descriptors_0054F7C8"/>.</summary>
    private static readonly byte[] s_Descriptors_0054F8E8 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f8e8
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f8f0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f8f8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f900
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f908
    ];

    private static byte* s_Descriptors0054F8E8Storage;

    private static byte* Descriptors0054F8E8()
        => NativeCopy(s_Descriptors_0054F8E8, ref s_Descriptors0054F8E8Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f9e0</c> — la troisieme copie, jusqu'a
    /// <c>DAT_0054fa08</c>.</summary>
    private static readonly byte[] s_Descriptors_0054F9E0 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f9e0
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f9e8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f9f0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f9f8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fa00
    ];

    private static byte* s_Descriptors0054F9E0Storage;

    private static byte* Descriptors0054F9E0()
        => NativeCopy(s_Descriptors_0054F9E0, ref s_Descriptors0054F9E0Storage);

    /// <summary>Ghidra: FUN_0040f470 @ 0x0040f470 — le premier des trois eclats. Le corps est dans
    /// <see cref="TickBurstingSpark"/>.</summary>
    private static void EntityHandler_0040F470(byte* e)
        => TickBurstingSpark(e, Descriptors0054F7C8());   // Ghidra: &DAT_0054f7c8

    /// <summary>Ghidra: FUN_00412950 @ 0x00412950 — le deuxieme.</summary>
    private static void EntityHandler_00412950(byte* e)
        => TickBurstingSpark(e, Descriptors0054F8E8());

    /// <summary>Ghidra: FUN_00415f40 @ 0x00415f40 — le troisieme.</summary>
    private static void EntityHandler_00415F40(byte* e)
        => TickBurstingSpark(e, Descriptors0054F9E0());

    // =====================================================================
    // LES BRAISES QUI MONTENT, EN QUATRE EXEMPLAIRES (2026-08-30)
    //
    // 0x00412330, 0x00412810, 0x00415860 et 0x00415e00 sont le meme corps. Absentes de Blaze.c ;
    // relevees au desassembleur, 78 instructions — Legacy/Raw/Blaze/FUN_00412330.asm.
    //
    // Ce sont DEUX familles de deux, et non quatre copies d'une seule :
    //
    //   A : 0x00412330 / 0x00415860 — +0x0a vaut 9, la teinte perd 5 par canal ;
    //   B : 0x00412810 / 0x00415e00 — +0x0a vaut 2, et le vert et le bleu perdent 1 de plus,
    //       de sorte que la braise ROUGIT en montant.
    //
    // Les deux copies d'une meme famille ne different que d'une ligne, l'adresse de la planche.
    // Le diff normalise entre les deux familles tient en trois endroits : le +0x0a, et les deux
    // decrements supplementaires.
    //
    // ELLES POINTENT AU MILIEU D'UN BLOC DEJA RELEVE. 0x0054f8f0 est le DEUXIEME enregistrement
    // de s_Descriptors_0054F8E8, dont EntityHandler_00412950 n'atteint que le premier ; les
    // quatre images que ces braises parcourent sont les enregistrements 1 a 4 de ce meme bloc.
    // C'est le cas exact que la regle « relever la plage, pas le descripteur » protege : releves
    // au plus juste, ces quatre enregistrements n'auraient laisse aucune trace, et les braises
    // seraient parties chercher une planche absente.
    // =====================================================================

    /// <summary>Ghidra: <c>0x00412330</c> — la premiere braise de la famille A.</summary>
    private static void EntityHandler_00412330(byte* e)
        => TickRisingEmber(e, Descriptors0054F8E8() + 8, 9, false);   // Ghidra: &DAT_0054f8f0

    /// <summary>Ghidra: <c>0x00415860</c> — la seconde copie de la famille A.</summary>
    private static void EntityHandler_00415860(byte* e)
        => TickRisingEmber(e, Descriptors0054F9E0() + 8, 9, false);   // Ghidra: &DAT_0054f9e8

    /// <summary>Ghidra: <c>0x00412810</c> — la premiere braise de la famille B, celle qui rougit.</summary>
    private static void EntityHandler_00412810(byte* e)
        => TickRisingEmber(e, Descriptors0054F8E8() + 8, 2, true);    // Ghidra: &DAT_0054f8f0

    /// <summary>Ghidra: <c>0x00415e00</c> — la seconde copie de la famille B.</summary>
    private static void EntityHandler_00415E00(byte* e)
        => TickRisingEmber(e, Descriptors0054F9E0() + 8, 2, true);    // Ghidra: &DAT_0054f9e8

    /// <summary>
    /// Le corps commun des quatre braises.
    ///
    /// <para>
    /// <b>Elle file droit et monte.</b> Contrairement aux spirales de
    /// <see cref="TickSummonEffect"/>, son cap <c>+0x12</c> est tire une fois a la naissance et
    /// <b>ne tourne jamais</b> : c'est le rayon <c>+0x5c</c> qui grandit, de <c>10</c> a <c>19</c>
    /// par image. La trajectoire est donc une droite qui part de l'ancre figee a la naissance —
    /// <c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c> retiennent les trois coordonnees de depart —
    /// pendant que la hauteur gagne <c>3</c> a <c>7</c> unites entieres par image.
    /// </para>
    /// <para>
    /// <b>Seize images, en quatre temps.</b> Le compteur <c>+0x9a</c> monte jusqu'a 4, se remet a
    /// zero et fait avancer l'indice d'atlas <c>+0x40</c> ; a la quatrieme image d'atlas l'entite
    /// s'eteint. La teinte perd cinq par canal a chaque image, partant de <c>0x808080</c> — elle
    /// n'atteint donc jamais zero, et ce n'est pas elle qui decide de la fin.
    /// </para>
    /// <para>
    /// <b><paramref name="reddens"/> est le seul ecart de comportement entre les deux familles</b> :
    /// le vert et le bleu perdent un de plus par image, apres la teinte commune. Sur seize images
    /// cela fait seize niveaux d'ecart — assez pour que la braise vire au rouge en fin de course,
    /// pas assez pour qu'elle y parte.
    /// </para>
    /// </summary>
    private static void TickRisingEmber(byte* e, byte* atlas, byte kind, bool reddens)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x0a] = kind;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = atlas;
            e[0x40] = 0;

            *(short*)(e + 0x28) = 0x2000;
            *(short*)(e + 0x2a) = 0x2000;

            *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);
            *(short*)(e + 0x14) = (short)(CrtRand() % 10 + 10);

            int rise = CrtRand();
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x9a) = 0;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            *(short*)(e + 0x16) = (short)(rise % 5 + 3);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short course = *(short*)(e + 0x12);
        int radius = *(int*)(e + 0x5c);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(short*)(e + 0x16) << 0xc;
        *(int*)(e + 0x5c) = radius + *(short*)(e + 0x14);

        (*(short*)(e + 0x9a))++;
        BrightenEntityTint_00404DE0(e, -5, -5, -5);

        if (reddens)
        {
            e[0x39]--;
            e[0x3a]--;
        }

        if (*(short*)(e + 0x9a) <= 3) return;

        *(short*)(e + 0x9a) = 0;
        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;

        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LA GERBE AUX CINQ PHASES, ET SES DEUX AMORCES (2026-08-30)
    //
    // 0x00411860 et 0x00414cd0 sont absents de Blaze.c ; ce sont deux AMORCES de quinze
    // instructions qui posent trois champs puis sautent dans FUN_004118a0 et FUN_00414d10 —
    // lesquelles sont, elles, decompilees. Comparees instruction par instruction sur leurs 561
    // lignes, ces deux-la ne different qu'en QUATRE endroits, tous des adresses de planche.
    //
    // Le releve des amorces est dans Legacy/Raw/Blaze/FUN_00411860.asm.
    // =====================================================================


    /// <summary>
    /// Ghidra: <c>0x00411860</c> — <b>l'amorce de la gerbe</b>. Absente de <c>Blaze.c</c> ; quinze
    /// instructions.
    ///
    /// <para>Elle pose <c>+0x98 = 0x39</c> et <c>+0x96 = 6</c> — le premier est le sous-type que
    /// les phases 2 et 3 feront naitre (<c>0x39 + 0xc0 = 0xf9</c>), le second la planche de
    /// couleurs qu'elles iront ecrire dans le descripteur. <b>Puis, et seulement si la phase vaut
    /// 2 et que l'etat est neuf</b>, elle installe la planche <c>0x0054f918</c> : la phase 2 est la seule
    /// qui lit <c>+0x3c</c> avant de l'avoir ecrit, et ce descripteur-la n'est PAS celui que le
    /// corps installe lui-meme.</para>
    /// </summary>
    private static void EntityHandler_00411860(byte* e)
    {
        *(short*)(e + 0x98) = 0x39;
        *(short*)(e + 0x96) = 6;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F910() + 8;   // Ghidra: &DAT_0054f918
        }

        TickFivePhaseBurst(e, Descriptors0054F808());
    }

    /// <summary>Ghidra: <c>0x00414cd0</c> — la seconde amorce, sur l'autre copie des planches
    /// (<c>0x0054f920</c> / <c>0x0054f928</c>).</summary>
    private static void EntityHandler_00414CD0(byte* e)
    {
        *(short*)(e + 0x98) = 0x39;
        *(short*)(e + 0x96) = 6;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FA08() + 8;   // Ghidra: &DAT_0054fa10
        }

        TickFivePhaseBurst(e, Descriptors0054F920());
    }

    /// <summary>
    /// Ghidra: FUN_004118a0 @ 0x004118a0 (et FUN_00414d10, sa copie) — <b>la gerbe aux cinq
    /// phases</b>. Decompilee dans <c>Blaze.c</c>, 561 instructions.
    ///
    /// <para>
    /// <b>Le champ <c>+0x14</c> n'est pas un etat, c'est un ROLE.</b> Le corps est une batterie de
    /// cinq effets sans rapport les uns avec les autres, choisis par <c>+0x14</c> a la naissance ;
    /// <c>+0x10</c> est l'etat a l'interieur du role. Une meme entite ne passe jamais d'un role a
    /// l'autre — sauf le temps d'un appel, voir plus bas.
    /// </para>
    /// <list type="bullet">
    /// <item><b>0</b> — <b>la retombee</b> : un rayon <c>+0x9a</c> parti de <c>0x46..0x59</c> qui
    /// <b>diminue</b> de 2 a 4 par image pendant que le cap tourne de seize fois autant. La teinte
    /// s'allume de dix par canal jusqu'a <c>0x80</c>. L'entite meurt quand le rayon passe sous
    /// zero, donc en une vingtaine d'images.</item>
    /// <item><b>1</b> — <b>l'eclat qui s'eteint</b> : ne bouge pas sauf de monter de <c>0x1000</c>
    /// par image, part d'une position dispersee sur <c>[-20, +19]</c> unites entieres dans les
    /// trois axes, et perd six par canal jusqu'a s'eteindre.</item>
    /// <item><b>2</b> — <b>la grande gerbe</b>, le seul role qui en seme d'autres : trente-trois
    /// naissances d'un coup a l'entree, puis une par image, puis trente-deux a la fermeture.
    /// Trois sons la ponctuent.</item>
    /// <item><b>3</b> — <b>le halo</b> : la grosse tuile <c>0x3f x 0x3f</c>, qui monte a
    /// <c>0x3c</c> de teinte puis <b>bat au rythme d'un sinus</b> pendant qu'elle redescend.</item>
    /// <item><b>4</b> — <b>la montee en spirale</b> : l'inverse du role 0, rayon croissant et cap
    /// tournant a l'envers, la teinte s'assombrissant de 2 a 4 par image.</item>
    /// </list>
    /// <para>
    /// <b>Le role est preté, puis rendu.</b> Aux naissances de la phase 2, le corps ecrit
    /// <c>+0x14</c> (3, puis 0, puis 1, puis 4 selon l'endroit), appelle
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> — qui recopie les <c>0x27</c> dwords de
    /// l'entite, <c>+0x14</c> compris — puis <b>remet <c>+0x14</c> a ce qu'il etait</b>. C'est
    /// ainsi qu'un seul corps se seme lui-meme dans quatre roles differents : le champ sert
    /// d'argument, le temps d'un appel.
    /// </para>
    /// <para>
    /// <b>Le descripteur est repeint en place.</b> Les phases 2 et 3 ecrivent <c>+0x96</c> dans
    /// l'octet 6 — la planche de couleurs — de l'enregistrement d'atlas, la phase 2 par le
    /// pointeur <c>+0x3c</c> courant, la phase 3 <b>par l'adresse en dur</b> <c>0x0054f816</c>.
    /// C'est pour cela que ces blocs sont alloues en natif et non lus depuis un tableau C#
    /// constant : le jeu ecrit dedans.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998, reproduit.</b> Les trente-trois naissances de la phase 2 passent
    /// comme descripteur un tampon de huit octets de la pile <b>qui n'est jamais rempli</b> —
    /// Ghidra le nomme <c>auStack_8</c> et le desassemblage confirme qu'aucune ecriture ne le
    /// precede. Le pointeur atterrit dans le <c>+0x3c</c> de l'entite qui nait, et n'y survit que
    /// jusqu'a sa premiere image : tous les corps d'effet posent leur propre planche a l'etat 0.
    /// Rien ne le lit donc jamais — mais rien ne le garantissait.
    /// </para>
    /// </summary>
    private static void TickFivePhaseBurst(byte* e, byte* atlas)
    {
        // Le tampon jamais rempli du binaire. Voir le resume : il est passe, stocke, jamais lu.
        byte* scratch = stackalloc byte[8];

        short role = *(short*)(e + 0x14);

        // ------------------------------------------------------------------ 0 : la retombee
        if (role == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = atlas;
                e[0x40] = 0;

                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(short*)(e + 0x32) = (short)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x14 + 0x46);

                int speed = CrtRand();
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
                *(int*)(e + 0x5c) = speed % 3 + 2;

                e[0x0a] = 9;
                e[0x0b] = 0;
                *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 10, 10, 10);

                if (e[0x38] > 0x7f)
                {
                    e[0x38] = 0x80;
                    e[0x39] = 0x80;
                    e[0x3a] = 0x80;
                }

                short heading = *(short*)(e + 0x32);
                short radius = *(short*)(e + 0x9a);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + heading) * 0x14;

                short step = (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x9a) -= step;
                *(short*)(e + 0x32) += (short)(step * 0x10);

                if (*(short*)(e + 0x9a) < 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ------------------------------------------------------------------ 1 : l'eclat qui s'eteint
        if (role == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = atlas;
                e[0x40] = 0;
                e[0x0a] = 9;
                e[0x0b] = 0;

                int scatter = CrtRand();
                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;

                short scale = (short)(SignedMask(scatter, 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(int*)(e + 0x20) += 0x1000;
                BrightenEntityTint_00404DE0(e, -6, -6, -6);
                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ------------------------------------------------------------------ 2 : la grande gerbe
        if (role == 2)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                // La planche de couleurs part dans le descripteur COURANT, pas dans une copie.
                (*(byte**)(e + 0x3c))[6] = e[0x96];

                *(int*)(e + 0x20) += 0x12c000;
                e[0x40] = 0;
                e[0x0a] = 7;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;

                short heading = *(short*)(e + 0x32);
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 0x28;
                *(short*)(e + 0x28) = 0x800;
                *(short*)(e + 0x2a) = 0x800;
                *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(heading) * 0x28;

                short saved = *(short*)(e + 0x14);
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;

                byte born = (byte)(*(short*)(e + 0x98) + 0xc0);

                *(short*)(e + 0x14) = 3;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), scratch, born, 0, -1);

                *(short*)(e + 0x14) = 0;
                for (int i = 0x1f; i != 0; i--)
                {
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), scratch, born, 0, -1);
                }

                *(short*)(e + 0x14) = saved;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 3, 3, 3);

                if (e[0x38] > 0x7f)
                {
                    e[0x38] = 0x80;
                    e[0x39] = 0x80;
                    e[0x3a] = 0x80;
                }

                (*(short*)(e + 0x12))++;

                if (*(short*)(e + 0x12) == 0x2a) Aud.PlaySoundEffect(0x2c, 0x16a, 0x1000, 0x1000);

                if (*(short*)(e + 0x12) > 0x2a)
                {
                    *(short*)(e + 0x10) = 2;
                    *(short*)(e + 0x12) = 0;
                    Aud.PlaySoundEffect(0x20, 0x200, 0x1000, 0x1000);
                }
            }

            if (*(short*)(e + 0x10) == 2)
            {
                byte born = (byte)(*(short*)(e + 0x98) + 0xc0);

                short saved = *(short*)(e + 0x14);
                *(short*)(e + 0x14) = 1;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), scratch, born, 0, -1);
                *(short*)(e + 0x14) = saved;

                if (*(short*)(e + 0x12) < 0x4b) *(int*)(e + 0x20) -= 0x3000;

                short life = *(short*)(e + 0x16);
                (*(short*)(e + 0x12))++;

                if (life == 0x9b)
                {
                    Aud.PlaySoundEffect(0x23, 0x329, 0x1000, 0x1000);
                    life = *(short*)(e + 0x16);
                }

                if (life > 0x9a)
                {
                    for (int i = 0x1f; i != 0; i--)
                    {
                        short keep = *(short*)(e + 0x14);
                        *(short*)(e + 0x14) = 4;
                        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), scratch, born, 0, -1);
                        *(short*)(e + 0x14) = keep;
                    }

                    *(uint*)e &= 0x7fffffff;
                }
            }
        }

        // ------------------------------------------------------------------ 3 : le halo
        if (role == 3)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                // Ghidra: DAT_0054f816 — l'octet 6 du SECOND enregistrement de la planche, ecrit
                // par son adresse en dur et non par +0x3c.
                (atlas + 8)[6] = e[0x96];

                *(byte**)(e + 0x3c) = atlas + 8;
                *(short*)(e + 0x28) = 0x800;
                *(short*)(e + 0x2a) = 0x800;
                e[0x40] = 0;
                e[0x0a] = 7;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;

                // Ghidra: `*(short *)(puVar3 + 5) = (short)puVar3[5];` — le champ se reecrit
                // lui-meme. Conserve tel quel : c'est ce que le binaire fait.
                *(short*)(e + 0x14) = *(short*)(e + 0x14);
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 3, 3, 3);

                if (e[0x38] > 0x3b)
                {
                    e[0x38] = 0x3c;
                    e[0x39] = 0x3c;
                    e[0x3a] = 0x3c;
                }

                (*(short*)(e + 0x12))++;

                if (*(short*)(e + 0x12) > 0x2a)
                {
                    *(short*)(e + 0x10) = 2;
                    *(short*)(e + 0x12) = 0;
                }
            }

            if (*(short*)(e + 0x10) == 2)
            {
                if (*(short*)(e + 0x12) < 0x4b) *(int*)(e + 0x20) -= 0x3000;

                // Les trois canaux lisent le MEME +0x12 : le binaire n'incremente qu'apres les
                // trois appels. Ghidra le rend en trois `extraout_AH`, qui sont l'octet haut du
                // sinus — `sar eax,8` puis `add al,0x3c`.
                byte beat = (byte)(((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) << 8) >> 8) + 0x3c);

                e[0x38] = beat;
                e[0x39] = beat;
                e[0x3a] = beat;

                (*(short*)(e + 0x12))++;

                if (*(short*)(e + 0x16) > 0x9a) *(uint*)e &= 0x7fffffff;
            }
        }

        // ------------------------------------------------------------------ 4 : la montee en spirale
        if (role == 4)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = atlas;
                e[0x40] = 0;

                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(short*)(e + 0x9a) = 0;
                *(short*)(e + 0x32) = (short)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                int speed = CrtRand();
                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
                *(int*)(e + 0x5c) = SignedMask(speed, 3) + 5;

                e[0x0a] = 9;
                e[0x0b] = 0;
                *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x44) = -(CrtRand() % 3) - 2;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                short heading = *(short*)(e + 0x32);
                short radius = *(short*)(e + 0x9a);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + heading) * 0x28;

                short step = (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x9a) += step;
                *(short*)(e + 0x32) += (short)(step * -2);

                // Ghidra: CONCAT22(...) — l'octet haut est du bruit de registre reutilise ; la
                // seule moitie qui compte est le mot bas de +0x44.
                short fade = (short)*(int*)(e + 0x44);
                BrightenEntityTint_00404DE0(e, fade, fade, fade);

                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        (*(short*)(e + 0x16))++;
    }


    /// <summary>
    /// Le corps commun des trois eclats.
    ///
    /// <para>
    /// <b>Il tire son echelle deux fois</b>, et la seconde ecrase la premiere :
    /// <c>0xc00 + (rand &amp; 0x3ff)</c> sur les deux axes, puis <c>0x1000 + rand % 0xc00</c>. Les
    /// deux premiers tirages ne servent qu'a consommer deux nombres de la suite pseudo-aleatoire —
    /// transcrits tels quels, la suite est partagee par tout le jeu.
    /// </para>
    /// <para>
    /// L'eclat part d'une position dispersee puis <b>decalee de 0x32 fois le sinus et le cosinus
    /// de son cap</b> <c>+0x32</c> — il jaillit donc dans la direction que son porteur regarde. Sa
    /// vitesse verticale part vers le haut (<c>+10 a +24</c> unites) et perd deux unites par
    /// image : une parabole.
    /// </para>
    /// <para>
    /// <b>Sa mort a deux portes, et l'une est aleatoire</b> : passe la onzieme image, une chance
    /// sur six par image ; et de toute facon a la seizieme. La teinte perd <c>0x24</c> par canal et
    /// par image <b>sans borne</b> — soustraction d'octet directe, pas
    /// <see cref="BrightenEntityTint_00404DE0"/> —, donc elle deborde.
    /// </para>
    /// </summary>
    private static void TickBurstingSpark(byte* e, byte* atlas)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = atlas;

            // Les deux premiers tirages d'echelle sont ecrases plus bas. Ils consomment deux
            // nombres de la suite, et c'est leur seul effet.
            *(ushort*)(e + 0x28) = (ushort)(SignedMask(CrtRand(), 0x3ff) + 0xc00);
            ushort discarded = (ushort)(SignedMask(CrtRand(), 0x3ff) + 0xc00);
            e[0x0a] = 0x0d;
            e[0x0b] = 0;
            *(ushort*)(e + 0x2a) = discarded;

            *(int*)(e + 0x1c) += (CrtRand() % 0x96 - 0x4b) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;

            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(course) * 0x32;
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(course) * 0x32;

            *(int*)(e + 0x50) = (CrtRand() % 6 - 3) * 0x1000;
            *(int*)(e + 0x54) = (CrtRand() % 0x0f + 10) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 6 - 3) * 0x1000;

            *(short*)(e + 0x28) = (short)(CrtRand() % 0xc00 + 0x1000);

            int last = CrtRand();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x2a) = (short)(last % 0xc00 + 0x1000);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        (*(short*)(e + 0x12))++;

        // Soustraction d'octet NUE, sans borne a zero : elle deborde.
        e[0x38] = (byte)(e[0x38] - 0x24);
        *(int*)(e + 0x54) -= 0x2000;
        e[0x39] = (byte)(e[0x39] - 0x24);
        e[0x3a] = (byte)(e[0x3a] - 0x24);

        int roll = CrtRand();
        if ((*(short*)(e + 0x12) > 10 && (short)(roll % 6) == 1) || *(short*)(e + 0x12) == 0x10)
        {
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0040f8e0 @ 0x0040f8e0 — <b>la gerbe large</b>. Decompilee dans <c>Blaze.c</c>.
    /// Table <c>0x0056b2c8</c>, foret. Meme charpente que
    /// <see cref="EntityHandler_0040F470"/> : jaillir, retomber, disparaitre.
    ///
    /// <para>
    /// <b>Elle tire elle aussi son echelle deux fois</b>, la seconde ecrasant la premiere
    /// (<c>0xc00 + (rand &amp; 0x3ff)</c> puis <c>0x800 + (rand &amp; 0x7ff)</c>), et se disperse
    /// beaucoup plus large que sa sœur : <c>±250</c> unites sur X, <c>±35</c> sur Y, <c>±100</c>
    /// sur Z. Le decalage par le cap est le meme, <c>0x32</c> fois sinus et cosinus.
    /// </para>
    /// <para>
    /// Sa vie est <b>fixe</b> : vingt-quatre images, sans tirage. Et elle change d'image d'atlas a
    /// chaque image, au hasard parmi six — c'est ce qui borne
    /// <see cref="s_Descriptors_0054F778"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040F8E0(byte* e)
        => TickWideSpray_0040F8E0(e, Descriptors0054F778(), 500, 0xfa, 0x46, 0x1e, 0x32);   // Ghidra: &DAT_0054f778

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0040F8E0"/>, avec ce que les copies du Labyrinthe des
    /// morts changent : la planche, la largeur et le recul des deux dispersions (<c>rand % largeur -
    /// recul</c> en X et en Y — Z garde <c>±100</c> ; le Y du modele n'est pas centre, <c>0x46</c> et
    /// <c>0x1e</c>) et le facteur du decalage de Z par le
    /// sinus du cap, <b>negatif</b> la-bas (<c>neg ecx</c> avant le produit par 50).
    /// </summary>
    private static void TickWideSpray_0040F8E0(byte* e, byte* plate, int xSpread, int xBack, int ySpread, int yBack, int zCourse)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;

            *(ushort*)(e + 0x28) = (ushort)(SignedMask(CrtRand(), 0x3ff) + 0xc00);
            ushort discarded = (ushort)(SignedMask(CrtRand(), 0x3ff) + 0xc00);
            e[0x0a] = 5;
            e[0x0b] = 0;
            *(ushort*)(e + 0x2a) = discarded;

            *(int*)(e + 0x1c) += (CrtRand() % xSpread - xBack) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % ySpread - yBack) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;

            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(course) * zCourse;
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(course) * 0x32;

            *(int*)(e + 0x50) = (SignedMask(CrtRand(), 0x0f) - 8) * 0x1000;
            *(int*)(e + 0x54) = (CrtRand() % 10 + 3) * 0x1000;
            *(int*)(e + 0x58) = (SignedMask(CrtRand(), 0x0f) - 8) * 0x1000;

            *(ushort*)(e + 0x28) = (ushort)(SignedMask(CrtRand(), 0x7ff) + 0x800);

            uint tail = (uint)CrtRand();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(ushort*)(e + 0x2a) = (ushort)(SignedMask((int)tail, 0x7ff) + 0x800);

            e[0x40] = (byte)(CrtRand() % 6);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        (*(short*)(e + 0x12))++;
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(int*)(e + 0x54) -= 0x1000;

        if (*(short*)(e + 0x12) == 0x18) *(uint*)e &= 0x7fffffff;

        e[0x40] = (byte)(CrtRand() % 6);
    }

    /// <summary>Ghidra: FUN_0040fac0 @ 0x0040fac0 — la premiere des trois bulles. Le corps est
    /// dans <see cref="TickRisingBubble"/> : voir le bloc de commentaire qui la precede.</summary>
    private static void EntityHandler_0040FAC0(byte* e)
        => TickRisingBubble(e, Descriptors0054F6F8());   // Ghidra: &DAT_0054f6f8

    /// <summary>Le corps commun des trois bulles. Elle se disperse de <c>±80</c> unites a
    /// l'horizontale, monte de <c>0 a 19</c> d'un coup, part d'une echelle fixe de <c>0x9c4</c>,
    /// et son pas de montee <c>+0x12</c> — tire entre 10 et 19 — <b>diminue d'une unite une image
    /// sur deux</b>. Deux seuils sur ce pas : sous 7 l'echelle perd 200 par image, sous 3 l'entite
    /// s'eteint. Elle ne compte donc pas ses images : sa vitesse lui sert d'horloge.</summary>
    private static void TickRisingBubble(byte* e, byte* atlas)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = atlas;
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0xa0 - 0x50) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0xa0 - 0x50) * 0x1000;

            int rise = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x28) = 0x9c4;
            *(short*)(e + 0x2a) = 0x9c4;
            *(int*)(e + 0x20) += (rise % 0x14) * 0x1000;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 10);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x14))++;
        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;

        if (*(short*)(e + 0x14) == 2)
        {
            *(short*)(e + 0x14) = 0;
            (*(short*)(e + 0x12))--;
        }

        if (*(short*)(e + 0x12) < 7)
        {
            *(short*)(e + 0x28) -= 200;
            *(short*)(e + 0x2a) -= 200;
        }

        if (*(short*)(e + 0x12) < 3) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LA BULLE, EN TROIS EXEMPLAIRES (2026-08-30)
    //
    // 0x0040fac0, 0x00412c20 et 0x00416210 sont le MEME corps, emis trois fois par le compilateur.
    // Compares octet a octet, ils ne different que par l'adresse du descripteur et par les
    // deplacements relatifs des quatre `call rand` — consequence mecanique de leurs adresses.
    //
    // Leurs trois planches sont elles aussi identiques, a l'adresse pres : 0x0054f6f8, 0x0054f818
    // et 0x0054f930 portent les memes cinq enregistrements. Le jeu de 1998 a duplique le code ET
    // la donnee.
    //
    // Le corps est donc ecrit UNE fois, avec la planche en parametre. C'est une unite reelle, pas
    // un decoupage de confort : trois sites l'emploient a l'identique.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f818</c> — les cinq premiers enregistrements, identiques a
    /// ceux de <see cref="s_Descriptors_0054F6F8"/>. La plage va jusqu'a <c>DAT_0054f8e8</c>, soit
    /// vingt-six enregistrements ; seul le <b>premier</b> est atteignable depuis
    /// <see cref="TickRisingBubble"/>, dont le corps ne touche jamais l'indice.</summary>
    private static readonly byte[] s_Descriptors_0054F818 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f818
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f820
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f828
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f830
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f838
    ];

    private static byte* s_Descriptors0054F818Storage;

    private static byte* Descriptors0054F818()
        => NativeCopy(s_Descriptors_0054F818, ref s_Descriptors0054F818Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f930</c> — la troisieme copie, meme contenu. La plage va
    /// jusqu'a <c>DAT_0054f9e0</c>.</summary>
    private static readonly byte[] s_Descriptors_0054F930 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f930
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f938
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f940
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f948
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f950
    ];

    private static byte* s_Descriptors0054F930Storage;

    private static byte* Descriptors0054F930()
        => NativeCopy(s_Descriptors_0054F930, ref s_Descriptors0054F930Storage);

    /// <summary>Ghidra: FUN_00412c20 @ 0x00412c20 — la deuxieme des trois bulles.</summary>
    private static void EntityHandler_00412C20(byte* e)
        => TickRisingBubble(e, Descriptors0054F818());

    /// <summary>Ghidra: FUN_00416210 @ 0x00416210 — la troisieme.</summary>
    private static void EntityHandler_00416210(byte* e)
        => TickRisingBubble(e, Descriptors0054F930());

    /// <summary>
    /// Ghidra: FUN_00457e20 @ 0x00457e20 — <b>le rocher qui roule</b>. Decompilee dans
    /// <c>Blaze.c</c>. Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// <b>C'est la sœur maigre de <see cref="EntityHandler_00456A30"/></b> : le meme deplacement
    /// avec collision (<see cref="MoveSceneEntity_00455B30"/>, masque <c>0x88c2</c> au lieu de
    /// <c>0x8882</c>), le meme test de vitesse, le meme son de roulement, les memes volumes de
    /// declenchement, les memes trois vitesses remises a zero a la fin. Ce qu'elle n'a pas : la
    /// position gardee hors de l'enregistrement, la recherche du membre le plus proche, et le
    /// second effet.
    /// </para>
    /// <para>
    /// <b>Son compteur se comporte a l'envers de celui de sa sœur.</b> Ici il decompte jusqu'a
    /// zero et <b>s'y arrete</b> ; chez <c>0x00456a30</c> il se recharge a 15. Le son repart donc
    /// des que la vitesse retombe puis remonte, au lieu d'une image sur seize.
    /// </para>
    /// <para>
    /// Elle ne pose jamais <c>+0x10</c> a 1 : le test de naissance ne sert qu'a remettre le
    /// compteur a zero, et il est donc vrai a chaque image tant que rien d'autre n'ecrit
    /// <c>+0x10</c>. C'est ce que fait le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00457E20(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x98) = 0;

        MoveSceneEntity_00455B30(e, 0x88c2);

        if (Rolling(e) && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, 2, 700, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0x0f;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        TestTriggerVolumes_00534CD0((uint*)e);

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00459a70 @ 0x00459a70 — <b>le troisieme rocher qui roule</b>. Decompilee dans
    /// <c>Blaze.c</c>. Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// Elle tient le milieu entre ses deux sœurs : comme <see cref="EntityHandler_00457E20"/> son
    /// compteur <c>+0x98</c> s'arrete a zero au lieu de se recharger, mais comme
    /// <see cref="EntityHandler_00456A30"/> elle <b>s'eteint quand le deplacement le lui dit</b> —
    /// le bit 29 du mot d'etat, que <see cref="MoveSceneEntity_00455B30"/> rend. Son masque de
    /// refus est <c>0x8880</c>, le plus permissif des trois.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459A70(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x98) = 0;

        if ((ushort)MoveSceneEntity_00455B30(e, 0x8880) == 1) *(uint*)e &= 0x7fffffff;

        if (Rolling(e) && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, 2, 700, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0x0f;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        TestTriggerVolumes_00534CD0((uint*)e);

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Le bit que <see cref="EntityHandler_00459C20"/> et <see cref="EntityHandler_00459DC0"/>
    /// attendent : le numero range en <c>+0x14</c> designe un bit de la <b>banque 0</b>
    /// (<c>0x005e96e0</c>), octet <c>n / 8</c>, bit <c>n % 8</c>.
    ///
    /// <para>La division est celle du compilateur — <c>(n + (n &gt;&gt; 31 &amp; 7)) &gt;&gt; 3</c>,
    /// donc vers zero — et le decalage est masque a cinq bits. Les deux corps l'ecrivent a
    /// l'identique.</para>
    /// </summary>
    private static bool ScriptFlagSet(short index)
    {
        int cell = DivideTowardZeroBy8(index);
        int bit = (index - cell * 8) & 0x1f;

        return (Mem.s_StateBank0_005e96e0[(short)cell] & (byte)(1 << bit)) != 0;
    }

    /// <summary>Les sons de la sous-zone, ou les deux mecanismes prennent l'identifiant qu'ils
    /// jouent. Ghidra: <c>**(int **)(DAT_005e97cc + 0xa8)</c>.</summary>
    private static byte* SubAreaSounds()
        => *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));

    /// <summary>
    /// Ghidra: FUN_00459c20 @ 0x00459c20 — <b>le mecanisme qui monte</b>. Decompilee dans
    /// <c>Blaze.c</c>. Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// Il <b>dort</b> tant que le bit de script que designe son <c>+0x14</c> n'est pas pose
    /// (<see cref="ScriptFlagSet"/>) — l'etat 0 rend la main sans rien faire. Une fois arme, il
    /// monte : <c>+0x34</c> gagne <c>+0x12</c> par image et <c>+0x12</c> lui-meme gagne un, donc
    /// l'ascension <b>accelere</b>. A <c>0x4af</c> depasse, il s'arrete.
    /// </para>
    /// <para>
    /// <b>Le son accompagne la montee en s'eteignant</b> : aux images 0, 3, 6, 9 et 12 du compteur
    /// <c>+0x98</c>, le meme son (<c>sons[0x21]</c>) part avec un gain qui <b>halve</b> a chaque
    /// fois — <c>0x2000</c>, <c>0x1000</c>, <c>0x800</c>, <c>0x400</c>, <c>0x200</c>. Le
    /// <c>switch</c> du binaire a cinq entrees qui convergent toutes vers le meme appel ; seul le
    /// gain change.
    /// </para>
    /// <para>
    /// <b>Puis il clignote et se fige.</b> L'etat 3 bascule le bit <c>0x4000</c> une image sur
    /// deux pendant vingt-et-une images, et au bout l'etat passe a <b>100</b> — une valeur qu'aucun
    /// test ne reconnait, donc plus rien ne s'execute — en laissant le bit <b>pose</b>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459C20(byte* e)
    {
        if (ScriptFlagSet(*(short*)(e + 0x14)) && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // Ghidra: cinq entrees de switch qui convergent vers le meme appel, gain decroissant.
            int gain = *(short*)(e + 0x98) switch
            {
                0 => 0x2000,
                3 => 0x1000,
                6 => 0x800,
                9 => 0x400,
                0x0c => 0x200,
                _ => 0,
            };

            if (gain != 0)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x21], 0x400, (short)gain, 0, 0);
            }

            (*(short*)(e + 0x98))++;
            *(short*)(e + 0x34) += *(short*)(e + 0x12);
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x34) > 0x4af)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        uint flags = (*(ushort*)(e + 0x12) & 1) == 0
            ? *(uint*)e | 0x4000
            : *(uint*)e & 0xffffbfff;

        short blink = (short)(*(ushort*)(e + 0x12) + 1);
        *(uint*)e = flags;
        *(short*)(e + 0x12) = blink;

        if (blink > 0x14)
        {
            *(short*)(e + 0x10) = 100;
            *(uint*)e = flags | 0x4000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00459dc0 @ 0x00459dc0 — <b>le mecanisme qui tombe</b>, le miroir du precedent.
    /// Decompilee dans <c>Blaze.c</c>. Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// Meme attente sur le meme genre de bit de script, puis une <b>chute qui accelere</b> :
    /// <c>+0x20</c> perd <c>+0x12</c> unites entieres par image et <c>+0x12</c> gagne un. En
    /// tombant, l'objet <b>tourne</b> (<c>+0x32</c> gagne <c>+0x9a</c>) et <b>bascule</b>
    /// (<c>+0x34</c> gagne <c>+0x5c</c>) — deux vitesses tirees une fois pour toutes au moment ou
    /// la chute commence, entre 1 et 60 pour la bascule, entre 1 et 20 pour la rotation.
    /// </para>
    /// <para>
    /// <b>L'etat 1 raccourcit le delai a chaque passage</b> : <c>+0x16</c> perd huit, borne a 1.
    /// L'etat 2 compte jusqu'a ce delai avant de lacher. Un mecanisme rejoue tombe donc de plus en
    /// plus vite.
    /// </para>
    /// <para>
    /// Le son est la meme serie decroissante que chez son miroir, un cran plus bas
    /// (<c>0x800</c> a <c>0x80</c>) et sur un autre identifiant, <c>sons[0x22]</c>. Sous
    /// <c>-0xfa0000</c> l'etat passe a 100 et tout s'arrete.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459DC0(byte* e)
    {
        if (ScriptFlagSet(*(short*)(e + 0x14)) && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x16) -= 8;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 2;
            if (*(short*)(e + 0x16) < 1) *(short*)(e + 0x16) = 1;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x16) <= *(short*)(e + 0x12))
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0x14;

                *(int*)(e + 0x5c) = CrtRand() % 0x3c + 1;

                int spin = CrtRand();
                *(short*)(e + 0x98) = 0;
                *(short*)(e + 0x9a) = (short)(spin % 0x14 + 1);
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        int gain = *(short*)(e + 0x98) switch
        {
            0 => 0x800,
            3 => 0x400,
            6 => 0x200,
            9 => 0x100,
            0x0c => 0x80,
            _ => 0,
        };

        if (gain != 0)
        {
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x22], 0x400, (short)gain, 0, 0);
        }

        (*(short*)(e + 0x98))++;
        (*(short*)(e + 0x12))++;
        *(short*)(e + 0x34) += (short)*(int*)(e + 0x5c);

        int height = *(int*)(e + 0x20) + *(short*)(e + 0x12) * -0x1000;
        *(short*)(e + 0x32) += *(short*)(e + 0x9a);
        *(int*)(e + 0x20) = height;

        if ((height & unchecked((int)0xfffff000)) < -0xfa0000) *(short*)(e + 0x10) = 100;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552638</c> — <b>le script de sons differes de l'explosion</b>, onze
    /// enregistrements de huit octets jusqu'a <c>DAT_00552690</c>. Le format est celui que decrit
    /// <c>Audio.QueueDelayedSoundScript</c> : identifiant, hauteur en <c>+4</c>, gain en <c>+6</c>,
    /// et les octets 1 et 2 du <b>premier</b> enregistrement servent en meme temps d'en-tete —
    /// combien de sons partent ensemble, et dans combien d'images part le lot suivant.
    ///
    /// <para>La plage est relevee entiere : le script se termine sur un delai nul, et c'est bien
    /// le dernier enregistrement (<c>30 00</c>) qui le porte.</para>
    /// </summary>
    private static readonly byte[] s_ExplosionSoundScript_00552638 =
    [
        0x0b, 0x01, 0x01, 0x00, 0xbc, 0x02, 0x00, 0x18,   // 0x00552638
        0x20, 0x01, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x00552640
        0x0b, 0x01, 0x01, 0x00, 0x00, 0x01, 0x00, 0x18,   // 0x00552648
        0x10, 0x10, 0x01, 0x00, 0xa8, 0x0a, 0x00, 0x18,   // 0x00552650
        0x20, 0x00, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00552658
        0x21, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00552660
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552668
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552670
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552678
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552680
        0x30, 0x00, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552688 — delai nul : fin
    ];

    private static byte* s_ExplosionSoundScriptStorage;

    private static byte* ExplosionSoundScript()
        => NativeCopy(s_ExplosionSoundScript_00552638, ref s_ExplosionSoundScriptStorage);

    /// <summary>
    /// Ghidra: FUN_00483650 @ 0x00483650 — <b>l'explosion</b>. Decompilee dans <c>Blaze.c</c>.
    /// Table <c>0x0056b0c8</c>, foret.
    ///
    /// <para>
    /// <b>Tout se joue a la premiere image</b> ; les neuf suivantes ne font qu'entretenir la
    /// secousse d'ecran, et a la dixieme l'entite s'efface (<c>*e = 0</c>, l'emplacement redevient
    /// libre).
    /// </para>
    /// <para>
    /// <b>Six eclats en couronne.</b> Une boucle de six tours fait tourner le cap <c>+0x32</c> de
    /// <c>0x2aa</c> — un sixieme de tour — et lui ajoute un tirage sur <c>0xff</c> plus
    /// <c>0x380</c>, puis engendre a chaque tour une entite dont le <b>sous-type vient de la
    /// sous-zone</b> : <c>repertoire[+0xd0][0] + 0xc0</c>. Chaque lieu explose donc avec ses
    /// propres eclats.
    /// </para>
    /// <para>
    /// <b>Puis le noyau.</b> L'entite se redecore de fond en comble — modele <c>7</c>, teinte
    /// <c>0x808080</c>, echelles remises a zero — et engendre le sous-type <c>0x6c</c>. Les
    /// valeurs posees avant la couronne (modele <c>0x38</c>, echelle <c>0x4002000</c>,
    /// <c>+0x2e = -0x10</c>) ne servent qu'aux six eclats, qui heritent de l'entite par recopie :
    /// c'est pour eux qu'elles sont ecrites, et elles sont ecrasees ensuite.
    /// </para>
    /// <para>
    /// <b>La secousse d'ecran.</b> <c>0x005de0ac</c> — le decalage vertical que la camera ajoute a
    /// sa cible — est pose a <b>10</b>, puis chaque image lui applique
    /// <c>(n &gt;&gt; 2) - n</c> : la valeur change de signe et decroit, soit 10, -8, 6, -5, 3…
    /// C'est une oscillation amortie, ecrite en une ligne.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483650(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            // Ce que les six eclats heriteront par recopie.
            e[0x04] = 0x38;
            short course = *(short*)(e + 0x32);
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x36) = 4;
            *(short*)(e + 0x2e) = -0x10;
            *(uint*)(e + 0x28) = 0x4002000;
            *(short*)(e + 0x2c) = 0x2000;
            *(short*)(e + 0x12) = 2;

            byte burst = (byte)(**(byte**)(s_Unknown_005e97cc + 0xd0) + 0xc0);

            for (int i = 0; i < 6; i++)
            {
                *(ushort*)(e + 0x32) = (ushort)((CrtRand() & 0xff) + 0x380 + course);
                SpawnCastEntity_0047AAE0(e, burst, 1, 1);
                course += 0x2aa;
            }

            // Puis le noyau, qui ecrase tout ce qui precede.
            *(uint*)(e + 0x70) = 0;
            *(uint*)(e + 0x44) = 0;
            *(uint*)(e + 0x48) = 0;
            *(uint*)(e + 0x4c) = 0;
            *(uint*)(e + 0x50) = 0x6000;
            *(uint*)(e + 0x54) = 0x6000;
            *(uint*)(e + 0x58) = 0x6000;
            *(uint*)(e + 0x38) = 0x808080;
            *(uint*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            e[0x04] = 7;

            SpawnCastEntity_0047AAE0(e, 0x6c, 1, 2);

            *Mem.Camera(0x005de0ac) = 10;
            Aud.QueueDelayedSoundScript(ExplosionSoundScript(), (int*)(e + 0x1c));
        }

        // Ghidra: `(n >> 2) - n` — la secousse change de signe et decroit a chaque image.
        int* shake = Mem.Camera(0x005de0ac);
        if (*shake != 0) *shake = (*shake >> 2) - *shake;

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) > 9) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004134c0 @ 0x004134c0 — <b>deux effets dans un seul corps</b>, choisis par
    /// <c>+0x5c</c>. Decompilee dans <c>Blaze.c</c>. Table <c>0x0056b1c8</c>, foret.
    ///
    /// <para>
    /// Le champ <c>+0x5c</c> n'est pas un etat mais un <b>aiguillage</b> : il vaut 0 ou 1 et ne
    /// change jamais dans ce corps. Les deux moities ne partagent rien, pas meme leur decompte —
    /// c'est le meme creneau de table employe pour deux choses.
    /// </para>
    ///
    /// <para><b>Variante 0 — l'onde qui marque.</b> Un anneau qui s'ouvre : les echelles
    /// <c>+0x28</c> et <c>+0x2c</c> rejoignent <c>30000</c> par <b>huitiemes</b>, donc vite au
    /// debut et de plus en plus lentement. La teinte part de <c>0xfafafa</c> et perd 7, 7 et
    /// <b>5</b> par image — le bleu tient plus longtemps, et c'est lui qui decide de la mort.
    /// L'echelle de depart de l'axe Z vaut <c>(0x19 - +0x12) * 400</c> : plus le compteur qu'elle
    /// herite est haut, plus l'anneau nait plat.</para>
    ///
    /// <para><b>Et elle tient le compte de qui elle a touche.</b> Tant que <c>+0x16</c> est nul et
    /// que l'anneau n'a pas atteint 27000, elle balaie les quatre membres du groupe et pose, pour
    /// chacun de vivant qui se trouve a moins de <c>echelle / 50</c> unites horizontales, <b>son
    /// bit</b> dans <c>+0x14</c>. Le masque grandit donc avec l'anneau, et rien ne l'efface : un
    /// membre touche reste marque.</para>
    ///
    /// <para><b>Variante 1 — l'etincelle qui s'allume puis s'eteint.</b> Modele <c>0x30</c>, trois
    /// echelles tirees entre 200 et 399, un cap au hasard sur douze bits, une hauteur relevee d'un
    /// tirage sur 200. Elle tourne de <c>-0x96</c> par image et grossit de 100 sur les trois axes,
    /// sans arret. Sa teinte fait deux mouvements opposes : pendant six images elle <b>rejoint
    /// <c>0xfa</c> par moities</b> — un eclat presque instantane —, puis pendant vingt-et-une elle
    /// <b>retombe par huitiemes</b> vers zero. Le second mouvement est trois fois plus long que le
    /// premier.</para>
    /// </summary>
    private static void EntityHandler_004134C0(byte* e)
    {
        if (*(uint*)(e + 0x5c) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                e[0x04] = 7;
                e[0x0a] = 2;
                e[0x0b] = 0;
                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = (short)((0x19 - *(short*)(e + 0x12)) * 400);
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x2e) = -0x3c;
                *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffe) | 2);
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, -7, -7, -5);

                short spread = (short)(((30000 - *(short*)(e + 0x28)) >> 3) + *(short*)(e + 0x28));
                *(short*)(e + 0x28) = spread;
                *(short*)(e + 0x2c) = (short)(((30000 - *(short*)(e + 0x2c)) >> 3) + *(short*)(e + 0x2c));

                // Ghidra: `(char)` — le canal BLEU, celui qui perd 5 et non 7, decide de la mort.
                if ((sbyte)e[0x3a] == 0) *(uint*)e &= 0x7fffffff;

                if (*(short*)(e + 0x16) == 0 && spread < 27000)
                {
                    byte* member = Inp.s_SlotFlags_005e93c0;

                    for (int i = 0; i < 4; i++, member += 0x9c)
                    {
                        uint flags = *(uint*)member;
                        if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                        int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                        int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= spread / 0x32) continue;
                        if ((*(short*)(e + 0x14) & (1 << i)) != 0) continue;

                        *(ushort*)(e + 0x14) |= (ushort)(1 << i);
                    }
                }
            }
        }

        if (*(uint*)(e + 0x5c) != 1) return;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x30;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(short*)(e + 0x28) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 200 + 200);

            *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);

            int seed = CrtRand();
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x30) = (short)(seed % 500);

            *(short*)(e + 0x2e) = -0x3c;
            *(int*)(e + 0x20) += (CrtRand() % 200) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) -= 0x96;

            // Ghidra: `(0xfa - b) >> 1` ajoute a b — la teinte rejoint 0xfa par moities.
            e[0x38] = (byte)(e[0x38] + (byte)((0xfa - (int)e[0x38]) >> 1));
            e[0x39] = (byte)(e[0x39] + (byte)((0xfa - (int)e[0x39]) >> 1));
            (*(short*)(e + 0x12))++;
            e[0x3a] = (byte)(e[0x3a] + (byte)((0xfa - (int)e[0x3a]) >> 1));

            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            GrowSparkScales(e);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x32) -= 0x96;

            // Ghidra: `-b >> 3` ajoute a b — la teinte retombe par huitiemes.
            e[0x38] = (byte)(e[0x38] + (byte)((-(int)e[0x38]) >> 3));
            e[0x39] = (byte)(e[0x39] + (byte)((-(int)e[0x39]) >> 3));
            (*(short*)(e + 0x12))++;
            e[0x3a] = (byte)(e[0x3a] + (byte)((-(int)e[0x3a]) >> 3));

            if (*(short*)(e + 0x12) > 0x14) *(uint*)e &= 0x7fffffff;

            GrowSparkScales(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00458fc0 @ 0x00458fc0 — <b>l'ascenseur</b>. Decompilee dans <c>Blaze.c</c>.
    /// Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// <b>Deux ascenseurs en un.</b> Le champ <c>+0x44</c>, fige a la premiere image depuis le
    /// <c>+0x14</c> que la sous-zone a pose, choisit la course : <c>0</c> monte de <c>0x14000</c>
    /// a <c>0x258000</c>, <c>1</c> monte de <c>-0xa0000</c> a <c>0xdc000</c>. Toutes les hauteurs
    /// du corps sont doublees en consequence, y compris le seuil au-dessus duquel un membre compte
    /// comme <b>deja en haut</b>.
    /// </para>
    /// <para>
    /// <b>Il part quand tout le monde est monte.</b> Le compte des membres rattaches a la
    /// plate-forme (<see cref="CountPartyMembersMatching"/>, masque <c>0x10</c> requis et
    /// <c>0x40000000</c> interdit) est compare a
    /// <see cref="ActivePartySlotCount_00466D70"/> <b>moins</b> ceux qui sont deja arrives — car
    /// un membre debarque en haut cesse d'etre rattache. Le depart demande aussi qu'il reste au
    /// moins un absent, sans quoi la plate-forme ferait la course a vide.
    /// </para>
    /// <para>
    /// <b>Et il redescend tout seul</b> si un membre rattache tombe plus de <c>0x28000</c> sous
    /// lui, ou si la camera (<c>0x005de06c</c>) passe au-dessus du seuil : l'etat 100 est la
    /// descente, l'etat 1 la montee.
    /// </para>
    /// <para>
    /// <b>Le son, une image sur cinq.</b> Pendant la course, le meme identifiant
    /// (<c>sons[0x17]</c>) part toutes les cinq images a la hauteur <c>0x400</c>, et <b>une seule
    /// fois</b> a l'arrivee, plus grave (<c>0x200</c>) — l'arret ne sonne que si la course avait
    /// commence, ce que le drapeau <c>+0x5c</c> retient.
    /// </para>
    /// <para>
    /// <b>Une bizarrerie du binaire, conservee.</b> Les deux tests d'arrivee verifient
    /// <c>+0x10 != 0</c> alors que l'etat vient d'etre reconnu comme 2 ou 0x65 — la condition est
    /// donc toujours vraie. Ghidra la rend telle quelle et le desassemblage la confirme.
    /// </para>
    /// </summary>
    private static void EntityHandler_00458FC0(byte* e)
    {
        if (*(short*)(e + 0x16) == 0)
        {
            *(int*)(e + 0x44) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 1;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        bool high = *(int*)(e + 0x44) != 0;
        int arrivedFloor = high ? 0xdbfff : 0x257fff;

        // ------------------------------------------------- qui est proche, et qui est deja arrive
        short arrived = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dy = (*(int*)(member + 0x20) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
            int dz = (*(int*)(member + 0x24) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x4b0) continue;

            int height = *(int*)(member + 0x20);
            if (height > arrivedFloor) arrived++;

            // Un membre tombe trop bas fait redescendre la plate-forme.
            if (height < *(int*)(e + 0x20) - 0x28000 && *(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 100;
            }
        }

        short riding = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);

        // ------------------------------------------------- le depart
        if (*(short*)(e + 0x14) != 0 && riding == 0) *(short*)(e + 0x14) = 0;

        if (*(short*)(e + 0x14) == 0 && *(short*)(e + 0x10) == 0)
        {
            int active = ActivePartySlotCount_00466D70();
            if (riding == active - arrived && active != arrived)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x14) = 1;
            }
        }

        // ------------------------------------------------- les deux conditions de rappel
        int top = high ? 0xdc000 : 0x258000;

        if (*(short*)(e + 0x14) == 0 && *(short*)(e + 0x10) == 0
            && riding == ActivePartySlotCount_00466D70() && *(int*)(e + 0x20) == top)
        {
            *(short*)(e + 0x10) = 100;
            *(short*)(e + 0x14) = 1;
        }

        if (*Mem.Camera(0x005de06c) > arrivedFloor && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        // ------------------------------------------------- la montee
        if (*(short*)(e + 0x10) == 2)
        {
            TickLiftTravel(e, 0x7000);

            int height = *(int*)(e + 0x20);
            if (height >= top + 1)
            {
                *(int*)(e + 0x20) = top;
                if (*(int*)(e + 0x5c) == 1) LiftArrivalSound(e);
                *(short*)(e + 0x10) = 0;
            }
        }

        // ------------------------------------------------- la descente
        if (*(short*)(e + 0x10) == 100)
        {
            *(short*)(e + 0x10) = 0x65;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) != 0x65) return;

        TickLiftTravel(e, -0x7000);

        int bottom = high ? unchecked((int)0xfff60000) : 0x14000;
        int floor = high ? -0xa0001 : 0x13fff;

        if (*(int*)(e + 0x20) > floor) return;

        *(int*)(e + 0x20) = bottom;
        if (*(int*)(e + 0x5c) == 1) LiftArrivalSound(e);
        *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00457770 @ 0x00457770 — <b>le tourniquet</b>. Decompilee dans <c>Blaze.c</c>.
    /// Table <c>0x0056adc8</c>, foret.
    ///
    /// <para>
    /// <b>Deux etats se superposent.</b> <c>+0x10</c> est un compteur qui monte d'un par image
    /// active et se rabat a <c>0x10</c> passe 200 ; <c>+0x14</c> est la <b>phase</b>, et c'est elle
    /// qui porte le comportement. Le <c>+0x9a</c> est un delai qui suspend tout : tant qu'il
    /// decompte, le corps rend la main des la troisieme ligne.
    /// </para>
    /// <para>
    /// <b>La rotation accelere puis freine.</b> La vitesse <c>+0x5c</c> part de <c>0x0f</c> et
    /// gagne un huitieme d'elle-meme pendant douze images, puis en <b>perd</b> un huitieme pendant
    /// les vingt-huit suivantes. Au bout de quarante images le tour est fait, le bit
    /// <c>0x40000000</c> se pose et la phase retombe a 99. Les phases 1, 0xc9 et 0x15 sont trois
    /// copies de ce meme mouvement, a la valeur de <c>+0x16</c> pres qu'elles laissent derriere.
    /// </para>
    /// <para>
    /// <b>La phase 0xca guette le depart du groupe.</b> Elle balaie les quatre membres et cherche
    /// s'il en reste un a moins de 500 unites ; s'il n'y en a plus, elle enchaine sur la phase 10,
    /// qui referme. La boucle du binaire ne compte pas ses tours : elle avance le curseur jusqu'a
    /// depasser <c>0x5e9595</c>, ce qui fait exactement quatre membres.
    /// </para>
    /// <para>
    /// <b>La phase 0xb est un retour en arriere</b> : le cap <b>recule</b> d'un pas qui grandit
    /// d'un par image, jusqu'a ce que la somme parcourue depasse <c>0x372</c> ; le depassement est
    /// alors rendu au cap, de sorte que le tourniquet retombe pile. C'est la seule phase qui
    /// corrige son propre arrondi.
    /// </para>
    /// <para>
    /// <b>Deux messages, deux sources.</b> A la phase 99, un tourniquet verrouille
    /// (<c>+0x48 == 1</c>, pose a la naissance quand la phase de depart valait 200) affiche
    /// l'etiquette <c>0x00551648</c> — « ça ne s'ouvre pas ! », deja relevee dans
    /// <see cref="GameMemory.s_EntityParams_00551640"/>. A la fin du retour en arriere, c'est
    /// l'etiquette que <b>la sous-zone</b> designe, prise dans son repertoire a l'indice que
    /// <c>+0x30</c> portait a la naissance et que <c>+0x44</c> conserve.
    /// </para>
    /// </summary>
    private static void EntityHandler_00457770(byte* e)
    {
        short counter = *(short*)(e + 0x10);

        if (counter == 0)
        {
            short label = *(short*)(e + 0x30);
            short start = *(short*)(e + 0x14);

            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x44) = label;
            *(short*)(e + 0x30) = 0;
            *(int*)(e + 0x48) = start == 200 ? 1 : 0;
        }

        (*(short*)(e + 0x9a))--;
        if (*(short*)(e + 0x9a) < 0) *(short*)(e + 0x9a) = 0;
        if (*(short*)(e + 0x9a) != 0) return;

        short bumped = (short)(counter + 1);
        *(short*)(e + 0x10) = bumped;

        if (bumped > 0x0f && *(short*)(e + 0x14) == 200) *(short*)(e + 0x14) = 99;
        if (bumped > 200) *(short*)(e + 0x10) = 0x10;

        short phase = *(short*)(e + 0x14);

        if (phase == 99)
        {
            if (*(short*)(e + 0x16) == 0 && (*(uint*)e & 0x40) != 0 && *(int*)(e + 0x48) == 1)
            {
                // Ghidra: &DAT_00551648 — « ça ne s'ouvre pas ! », a huit octets du bloc releve.
                BindMenuLabels(Mem.s_EntityParams_00551640 + 8, 0, s_WorldMapDescriptor_005633b0, 0);
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                *(short*)(e + 0x9a) = 5;
            }

            return;
        }

        if ((*(uint*)e & 0x40000000) != 0 && phase != 0 && phase != 200) return;

        // ------------------------------------------------------------ le premier tour
        if (phase == 0)
        {
            TurnstileSound(e);
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0x0f;
            *(uint*)e |= 0x2000;
            *(short*)(e + 0x14) = 1;
        }

        if (*(short*)(e + 0x14) == 1 && TurnstileSpin(e))
        {
            *(short*)(e + 0x16) = 1;
            *(short*)(e + 0x14) = 99;
            *(uint*)e |= 0x40000000;
            return;
        }

        // ------------------------------------------------------------ le tour de la phase 200
        if (*(short*)(e + 0x14) == 200)
        {
            TurnstileSound(e);
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x14) = 0xc9;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x2000;
            *(int*)(e + 0x5c) = 0x0f;
        }

        if (*(short*)(e + 0x14) == 0xc9 && TurnstileSpin(e))
        {
            *(short*)(e + 0x14) = 0xca;
            *(short*)(e + 0x16) = 2;
            *(uint*)e |= 0x40000000;
        }

        // ------------------------------------------------------------ attendre que le groupe parte
        if (*(short*)(e + 0x14) == 0xca)
        {
            bool nearby = false;
            byte* member = Inp.s_SlotFlags_005e93c0;

            for (int i = 0; i < 4; i++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x10000000) != 0) continue;

                int dx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(member + 0x1c) >> 0xc);
                int dy = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(member + 0x20) >> 0xc);
                int dz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(member + 0x24) >> 0xc);

                if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < 500) nearby = true;
            }

            if (!nearby)
            {
                *(short*)(e + 0x14) = 10;
                *(short*)(e + 0x16) = 1;
            }
        }

        if (*(short*)(e + 0x14) == 10 && *(short*)(e + 0x16) == 0) *(short*)(e + 0x14) = 99;

        if (*(short*)(e + 0x14) == 10
            && (*(short*)(e + 0x16) == 1 || *(short*)(e + 0x16) == 2))
        {
            TurnstileSound(e);
            *(short*)(e + 0x14) = 0x0b;
            *(uint*)e &= 0xffffdfff;
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x12) = 5;
            *(int*)(e + 0x5c) = 0;
        }

        // ------------------------------------------------------------ le retour en arriere
        if (*(short*)(e + 0x14) == 0x0b)
        {
            short step = *(short*)(e + 0x12);
            int travelled = *(int*)(e + 0x5c);

            *(short*)(e + 0x32) -= step;
            *(short*)(e + 0x12) = (short)(step + 1);
            *(int*)(e + 0x5c) = travelled + step;

            if (travelled + step > 0x372)
            {
                int total = *(int*)(e + 0x5c);
                *(short*)(e + 0x14) = 99;
                *(short*)(e + 0x16) = 0;

                // Le depassement est rendu au cap : le tourniquet retombe pile.
                *(short*)(e + 0x32) += (short)(total - 0x373);

                if (*(int*)(e + 0x44) == 0) return;

                byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + *(int*)(e + 0x44) * 4);
                BindMenuLabels(labels, 0, s_WorldMapDescriptor_005633b0, 0);
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                return;
            }
        }

        // ------------------------------------------------------------ le tour de la phase 0x14
        if (*(short*)(e + 0x14) == 0x14
            && (*(short*)(e + 0x16) == 1 || *(short*)(e + 0x16) == 2))
        {
            *(short*)(e + 0x14) = 99;
        }

        if (*(short*)(e + 0x14) == 0x14 && *(short*)(e + 0x16) == 0)
        {
            TurnstileSound(e);
            *(short*)(e + 0x14) = 0x15;
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x2000;
            *(int*)(e + 0x5c) = 0x0f;
        }

        if (*(short*)(e + 0x14) == 0x15 && TurnstileSpin(e))
        {
            *(short*)(e + 0x16) = 1;
            *(short*)(e + 0x14) = 99;
            *(uint*)e |= 0x40000000;
        }
    }
}
