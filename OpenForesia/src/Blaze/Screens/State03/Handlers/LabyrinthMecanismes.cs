// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DU LABYRINTHE DES MORTS — zone 5, table 0x0056adc8.
//
// Ouvert le 2026-09-18, au lot 4, sur le patron de `MineMecanismes.cs` et par le meme test
// d appartenance : `Labyrinth.cs` garde le decor — ce qui sonne, bouge, brille, tombe —, ce
// fichier prend ce qui consulte le GROUPE ou la PARTIE. Trois corps l ont ouvert :
//
//   0x004618B0  ce qui ne part que si le groupe entier est a bord
//   0x00460990  la porte tenue ouverte a la manette
//   0x00462990  le panneau de dialogue qui tourne puis s eteint
//
// Le lot 5, le meme jour, en a ajoute deux — ceux qui lancent un script de zone :
//
//   0x004606C0  l escalier qui se derobe, marche par marche
//   0x00460AC0  ce qui guette l approche du groupe puis lance un script
//
// Les trois blocs de la zone dans cette table, rappeles de `Labyrinth.cs` :
//
//   0x00557aa8  sous-zones 0, 1, 2, 3
//   0x00558128  sous-zones 5, 6, 7, 8
//   0x005587a8  sous-zones 4, 9
//
// Les aides partagees ne sont PAS dupliquees : `ZoneSound` vit dans `Mine.cs`,
// `DoorHolderPadState` et `BindMenuLabels` dans `MineMecanismes.cs` et `State00/Options.cs`,
// `CountPartyMembersMatching` et `ActivePartySlotCount_00466D70` dans `Forest.cs` et `Common.cs` —
// meme classe partielle. Le tri de la zone par complexite est dans
// `Documentation/Etat/Zone5ParComplexite.md`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // CRENEAU 35 DU BLOC 0x00558128 — CE QUI NE PART QUE SI LE GROUPE ENTIER EST A BORD
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004618b0</c> — creneau <b>35</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>60 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_004618B0.asm</c>.
    ///
    /// <para>
    /// <b>Elle attend que tout le monde soit monte, puis s'enfonce et disparaît.</b> Le
    /// declenchement est en deux temps. D'abord un comptage <b>en clair dans le corps</b> : les
    /// quatre emplacements de <c>0x005e93c0</c>, au pas de <c>0x9c</c>, dont le mot de tete porte
    /// <c>0x80000000</c> et <b>aucun</b> bit de <c>0x50010000</c>. Il en faut au moins <b>deux</b>.
    /// Ensuite <see cref="CountPartyMembersMatching"/> compte les membres qui lui sont
    /// <i>rattaches</i> (bit <c>0x10</c>, sans <c>0x40000000</c>) et la comparaison avec
    /// <see cref="ActivePartySlotCount_00466D70"/> doit tomber juste : <b>tous</b>, pas seulement
    /// deux.
    /// </para>
    /// <para>
    /// <b>Le comptage inline n'est PAS <see cref="ActivePartySlotCount_00466D70"/></b>, et c'etait
    /// le piege de ce corps : la fonction commune interdit <c>0x50000000</c>, celui-ci interdit
    /// <c>0x50010000</c>. Le bit <c>0x10000</c> en plus exclut le membre a terre, qui compte donc
    /// pour la fonction commune et pas pour ce mecanisme. Les deux comptes sont bien confrontes
    /// l'un a l'autre dans la meme image, et cette asymetrie est le coeur de la condition :
    /// <b>avec un membre a terre, elle ne part jamais</b>.
    /// </para>
    /// <para>
    /// <b>L'etat 1 est la descente</b> : <c>0x5000</c> par image — un peu plus de cinq cases —
    /// pendant <b>36 images</b>, apres quoi le bit <c>0x2000</c> s'allume et l'etat passe a 2.
    /// <b>L'etat 2 est la disparition</b> : l'angle <c>+0x32</c> tourne de <c>0x32</c> par image
    /// tandis que les echelles <c>+0x28</c> et <c>+0x2c</c> perdent 100 chacune. Des que
    /// <c>+0x28</c> passe sous zero, les deux sont mises a zero et le bit <c>0x4000</c> est pose :
    /// l'entite finit plate, en rotation, a l'endroit ou elle s'est enfoncee. L'echelle
    /// <c>+0x2a</c> — la verticale — n'est jamais touchee.
    /// </para>
    /// <para>
    /// <b>Le declenchement est reevalue a chaque image, meme apres le depart.</b> Le comptage est
    /// hors de toute garde d'etat, et il repose <c>+0x10</c> a 1 chaque fois que la condition
    /// tient. Une entite arrivee a l'etat 2 dont le groupe est toujours a bord <b>repart donc a
    /// l'etat 1</b>, echelles a zero et bit <c>0x4000</c> pose. C'est ce que le binaire fait.
    /// </para>
    /// </summary>
    private static void EntityHandler_004618B0(byte* e)
    {
        // Ghidra: 0x004618b2..0x004618d6 — les quatre emplacements, sans lire
        // s_PartySize_005e9020, et avec le masque interdit 0x50010000.
        int usable = 0;
        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(Inp.s_SlotFlags_005e93c0 + i * 0x9c);
            if ((flags & 0x80000000) != 0 && (flags & 0x50010000) == 0) usable++;
        }

        if (usable >= 2)
        {
            short aboard = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // FUN_00535000
            if (aboard == (short)ActivePartySlotCount_00466D70()) *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x12) = 0;

        if (*(short*)(e + 0x10) == 1)
        {
            // Ghidra: `add edx,0FFFFB000h` — la hauteur perd 0x5000 par image.
            *(int*)(e + 0x20) -= 0x5000;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x23)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e |= 0x2000;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x32) += 0x32;

            // Ghidra: `mov ecx,0FFFFFF9Ch ; add [esi+28h],cx ; add [esi+2Ch],cx` — CX vaut -100,
            // et l'echelle verticale +0x2a n'est pas du voyage.
            *(short*)(e + 0x28) -= 100;
            *(short*)(e + 0x2c) -= 100;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(uint*)e |= 0x4000;
            }
        }
    }

    // =====================================================================
    // CRENEAU 32 DU BLOC 0x00557AA8 — LA PORTE QU'ON TIENT OUVERTE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00460990</c> — creneau <b>32</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>72 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460990.asm</c>.
    ///
    /// <para>
    /// <b>La cousine du labyrinthe de <see cref="EntityHandler_00451790"/></b>, la porte de la mine
    /// qu'on tient ouverte : memes quatre etats, meme attente sur les bits <c>0xa000</c> de la
    /// manette du membre <c>+0x14</c> par <see cref="DoorHolderPadState"/>, memes rampes de
    /// <b>dix images</b>. Trois differences, toutes du cote du labyrinthe : elle n'a <b>qu'un
    /// battant</b>, elle <b>sonne</b> au depart de l'ouverture, et son repos ne rappelle rien.
    /// </para>
    /// <para>
    /// <b>Elle naît au repos et retient sa pose.</b> L'etat 0 range l'angle <c>+0x34</c> dans
    /// <c>+0x9a</c> et passe a <c>0x63</c> : c'est sa position fermee, gardee pour l'etat 10.
    /// <b>Etat 1, l'ouverture</b> : le son <c>0x10</c> de la sous-zone sur la premiere image, puis
    /// <c>+100</c> par image dix fois — <c>0x3e8</c> en tout, comme le battant
    /// <see cref="EntityHandler_00463300"/>. <b>Etat 2, l'attente</b> : elle reste ouverte tant que
    /// la manette porte l'un des bits <c>0xa000</c>. <b>Etat 3, le retour</b>, <c>-100</c> dix
    /// fois, et le repos.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est teste dans la meme image que la fin de l'ouverture</b> : le binaire tombe de
    /// l'un dans l'autre sans garde. Une porte dont le tenant a deja lache se referme donc a
    /// l'image suivante, sans pause.
    /// </para>
    /// <para>
    /// <b>L'etat 10 n'est pas une rampe mais un calage</b> : il ecrit <c>+0x9a + 0x44c</c> dans
    /// <c>+0x34</c> d'un coup. Rien dans ce corps ne pose l'etat 10, ni les etats 1 et 2 — ils
    /// viennent d'un script de zone. <c>0x44c</c> est un peu plus que la course de l'ouverture
    /// (<c>0x3e8</c>) : c'est une pose <b>grande ouverte</b>, celle d'une porte qu'un evenement
    /// laisse ouverte pour de bon.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460990(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 0x63;
            *(short*)(e + 0x9a) = *(short*)(e + 0x34);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // Ghidra: FUN_00468080(param_1, **(int **)(DAT_005e97cc + 0xa8) + 0x10, 0x800, 0x1000, 0, 0)
            if (*(short*)(e + 0x12) == 0)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x800, 0x1000, 0, 0);

            *(short*)(e + 0x34) += 100;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 10)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2 && (DoorHolderPadState(e) & 0xa000) == 0)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x34) -= 100;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 10)
            {
                *(short*)(e + 0x10) = 0x63;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 10)
            *(short*)(e + 0x34) = (short)(*(short*)(e + 0x9a) + 0x44c);
    }

    // =====================================================================
    // CRENEAU 48 DU BLOC 0x005587A8 — LE PANNEAU QUI TOURNE PUIS S'ETEINT
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00462990</c> — creneau <b>48</b> du bloc <c>0x005587a8</c> (sous-zones 4 et 9).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>68 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00462990.asm</c>.
    ///
    /// <para>
    /// <b>Elle dit une phrase, tourne neuf images et meurt.</b> Toute sa vie tient en dix images :
    /// <c>+0x10</c> avance d'un par image sans condition, et des qu'il atteint 10 le mot d'etat
    /// prend <c>0x40002000</c> — apres quoi la garde d'entree renvoie tout de suite.
    /// </para>
    /// <para>
    /// <b>Le numero du libelle vient du placement, decale de huit bits.</b> La premiere image lit
    /// <c>+0x30</c> — un demi-mot <b>signe</b> —, range <c>+0x30 &gt;&gt; 8</c> dans <c>+0x44</c>
    /// et remet <c>+0x30</c> a zero : le champ de pose est <i>consomme</i>, et l'angle qu'il portera
    /// ensuite part de zero. Si le numero est nul, rien n'est dit.
    /// </para>
    /// <para>
    /// <b>La phrase est affichee comme les autres panneaux de la carte du monde</b> :
    /// <see cref="BindMenuLabels"/> avec le libelle <c>+0x44</c> de la table <c>+0x70</c> de la
    /// zone, le descripteur <c>0x005633b0</c>, et <c>0x005e99c8</c> qui passe a <c>0x40</c>. Le
    /// <b>locuteur</b> n'est pas le membre qui l'a touchee mais l'emplacement que
    /// <see cref="Memory.s_Unknown_005de538"/> designe — le joueur qui tient la main.
    /// </para>
    /// <para>
    /// <b>Les images 2 a 9 tournent</b>, de plus en plus vite : <c>+0x14</c> part de 10 et gagne
    /// <c>0x14</c> par image, et c'est lui qu'on ajoute a l'angle <c>+0x32</c>. Le son <c>4</c> de
    /// la sous-zone part sur l'image 2 seule, a hauteur <c>0x400</c>.
    /// </para>
    /// <para>
    /// <b>Le compteur <c>+0x12</c> est mort-ne.</b> Il avance d'un par image et boucle de 20 a 200,
    /// mais l'entite n'atteint jamais la 15e image : son seul emploi reel est le garde
    /// <c>&lt; 0x0f</c> du libelle, toujours vrai a la premiere image. La boucle a 200 ne sert
    /// jamais. Transcrite telle quelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00462990(byte* e)
    {
        short beat = *(short*)(e + 0x12);

        if (beat == 0)
        {
            // Ghidra: `movsx ecx,word [esi+30h] ; sar ecx,8` — le champ de pose porte le libelle.
            *(int*)(e + 0x44) = *(short*)(e + 0x30) >> 8;
            *(short*)(e + 0x30) = 0;
        }

        beat++;
        *(short*)(e + 0x12) = beat;
        if (beat > 0xc8) *(short*)(e + 0x12) = 0x14;

        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            *(short*)(e + 0x14) = 10;

            if (*(short*)(e + 0x12) < 0x0f && *(int*)(e + 0x44) != 0)
            {
                byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + *(int*)(e + 0x44) * 4);

                // Ghidra: `mov al,[5de538h] ; lea edx,[eax+eax*4] ; shl edx,3 ; sub edx,eax ;
                // lea eax,[edx*4+5E93C0h]` — l'emplacement du joueur qui tient la main.
                byte* reader = Inp.s_SlotFlags_005e93c0 + (Mem.s_Unknown_005de538 & 0xff) * 0x9c;

                BindMenuLabels(labels, (uint)(nint)reader, s_WorldMapDescriptor_005633b0, 0);
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
            }
        }
        else
        {
            *(short*)(e + 0x14) += 0x14;
            *(short*)(e + 0x32) += *(short*)(e + 0x14);
        }

        if (*(short*)(e + 0x10) == 1)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(4), 0x400, 0x1000, 0, 0);

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) >= 0x0a) *(uint*)e |= 0x40002000;
    }

    // =====================================================================
    // LOT 5 (2026-09-18) — LES DEUX QUI LANCENT UN SCRIPT DE ZONE
    //
    // Les huit autres corps du lot sont du decor et vivent dans `Labyrinth.cs`. Ces deux-ci
    // appellent `RunEntityScript` (`FUN_00513dd0`), ce qui les range ici par le test
    // d'appartenance ; le second lit en plus la table des emplacements en entier.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004606c0 @ 0x004606c0 — creneau <b>38</b> du bloc <c>0x00557aa8</c>
    /// (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 95 instructions.
    ///
    /// <para>
    /// <b>L'escalier qui se derobe, marche par marche, en lancant un script a chacune.</b> Le
    /// contact (bit <c>0x10</c>) au repos leve le drapeau de script <c>+0x16</c> et met la
    /// descente en marche. Le compteur <c>+0x5c</c> — un mot de 32 bits, arme a <b>3</b> a la
    /// naissance — est la vitesse : la hauteur perd <c>(compteur + 1) * 0x1000</c> par image et le
    /// compteur monte d'un, si bien que la chute accelere sans borne.
    /// </para>
    /// <para>
    /// <b>Toutes les trois images, une marche.</b> Le corps appelle
    /// <see cref="RunEntityScript"/> avec l'indice <c>+0x9a + +0x14 * 4</c> — quatre scripts
    /// consecutifs a partir de <c>+0x14 * 4</c>, un par marche — et <c>+0x9a</c> compte de 0 a 3.
    /// A la quatrieme, l'etat passe a 99, le son <c>0x1a</c> de la sous-zone part a hauteur
    /// <c>0x400</c>, et <b>seize</b> effets <c>0x32</c> naissent d'un coup.
    /// </para>
    /// <para>
    /// <b>L'etat 99 n'est pas une fin : c'est un regime.</b> Il fait naitre <b>un</b> effet
    /// <c>0x32</c> par image, indefiniment — la poussiere ne retombe jamais.
    /// </para>
    /// <para>
    /// <b>L'etat <c>0x62</c> est une entree separee, posee par ailleurs</b> : il enfonce la hauteur
    /// de <c>0x72000</c> d'un seul coup — quatorze cases — et passe directement au regime 99, sans
    /// script ni son. C'est le chemin de celui qui n'a pas ete declenche par un pas.
    /// </para>
    /// <para>
    /// Le drapeau est pose <b>sans le garde a 32 octets</b> de <see cref="ScriptFlagRaise"/> — voir
    /// <see cref="ScriptFlagRaiseUnbounded"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_004606C0(byte* e)
    {
        // Ghidra: l'etat est lu UNE fois en tete et les deux premiers blocs le relisent par cette
        // copie ; le troisieme relit le champ.
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 3;
        }

        if ((*e & 0x10) != 0 && state == 0)
        {
            *(short*)(e + 0x10) = 1;
            ScriptFlagRaiseUnbounded(*(short*)(e + 0x16));
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int speed = *(int*)(e + 0x5c);

            (*(short*)(e + 0x12))++;
            *(int*)(e + 0x5c) = speed + 1;
            *(int*)(e + 0x20) += (speed + 1) * -0x1000;

            if (*(short*)(e + 0x12) == 3)
            {
                short group = *(short*)(e + 0x14);
                short step = *(short*)(e + 0x9a);

                *(short*)(e + 0x12) = 0;

                // Ghidra: FUN_00513dd0(DAT_005e97cc, param_1, +0x9a + +0x14 * 4)
                RunEntityScript(s_Unknown_005e97cc, e, (uint)(step + group * 4));

                (*(short*)(e + 0x9a))++;
                if (*(short*)(e + 0x9a) == 4)
                {
                    *(short*)(e + 0x10) = 99;
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1a), 0x400, 0x1000, 0, 0);

                    for (int i = 0x10; i != 0; i--)
                        SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
                }
            }
        }

        if (*(short*)(e + 0x10) == 0x62)
        {
            *(short*)(e + 0x10) = 99;
            *(int*)(e + 0x20) -= 0x72000;
        }

        if (*(short*)(e + 0x10) == 99) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
    }

    /// <summary>
    /// Ghidra: <c>0x00460ac0</c> — creneau <b>24</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>99 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460AC0.asm</c>.
    ///
    /// <para>
    /// <b>Elle guette l'approche du groupe, clignote, puis lance un script.</b> Son garde d'entree
    /// est celui de <see cref="EntityHandler_00460A90"/> au bit pres : dans la <b>sous-zone 1</b>
    /// et seulement la, elle reste muette tant que le <b>drapeau 234</b> n'est pas pose.
    /// </para>
    /// <para>
    /// <b>La detection balaie les quatre emplacements a chaque image</b> — actifs
    /// (<c>0x80000000</c>) et sans <c>0x50000000</c> —, et mesure la distance <b>horizontale</b>
    /// seule : X et Z, pas la hauteur. Sous <b>200</b> cases, et si elle est encore au repos, elle
    /// part avec le son <c>0x27</c> de la sous-zone a hauteur <c>0x400</c>. La garde d'etat est
    /// <i>dans</i> la boucle, si bien qu'un second membre proche dans la meme image ne rejoue pas
    /// le son.
    /// </para>
    /// <para>
    /// <b>La soustraction precede le decalage</b> : le binaire fait
    /// <c>(membre - elle) &gt;&gt; 12</c> et non <c>(membre &gt;&gt; 12) - (elle &gt;&gt; 12)</c>.
    /// C'est l'inverse de l'idiome des trois sœurs de distance a la camera
    /// (<see cref="TryPlaySoundNearEntity"/> et ses voisines), et le resultat differe d'une unite
    /// selon les restes.
    /// </para>
    /// <para>
    /// <b>L'etat 1 clignote quatorze images</b> : seize effets <c>0x35</c> naissent sur la premiere,
    /// puis le bit <c>0x4000</c> suit la <b>parite</b> du compteur — pose sur les pairs. A la 14e,
    /// le mot d'etat prend <c>0x6000</c> et l'etat passe a 2. <b>L'etat 2 lance le script</b>
    /// numero <c>+0x14</c> par <see cref="RunEntityScript"/>, une seule fois, et passe a 3 : la
    /// fin.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460AC0(byte* e)
    {
        // Ghidra: `mov al,[5e96fdh] ; test al,4 ; jne` puis `cmp [5de52ch],bl ; je` — le drapeau
        // 234 ne commande que dans la sous-zone 1.
        if ((Mem.s_StateBank0_005e96e0[0x1d] & 4) == 0 && s_Unknown_005de52c == 1) return;

        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            // Ghidra: `sub eax,edx ; sar eax,0Ch` — la difference d'abord, le decalage ensuite.
            int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 0xc8) continue;
            if (*(short*)(e + 0x10) != 0) continue;

            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x27), 0x400, 0x1000, 0, 0);
        }

        short state = *(short*)(e + 0x10);
        if (state == 0) *(short*)(e + 0x12) = 0;

        if (state == 1)
        {
            if (*(short*)(e + 0x12) == 0)
                for (int i = 0x10; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);

            (*(short*)(e + 0x12))++;

            short beat = *(short*)(e + 0x12);
            uint word = *(uint*)e;

            word = (beat & 1) == 0 ? word | 0x4000 : word & 0xffffbfff;
            *(uint*)e = word;

            if (beat >= 0x0e)
            {
                *(uint*)e = word | 0x6000;
                *(short*)(e + 0x10) = 2;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // Ghidra: FUN_00513dd0(DAT_005e97cc, param_1, (short)+0x14)
            RunEntityScript(s_Unknown_005e97cc, e, (uint)*(short*)(e + 0x14));
            *(short*)(e + 0x10) = 3;
        }
    }

    // =====================================================================
    // LOT 6 (2026-09-18) — LA SECOUSSE QUI DECLENCHE, EN DEUX VERSIONS
    //
    // Table 0x0056b0c8, creneau 63 des blocs 0x00557da8 (sous-zones 0 a 3) et 0x00558428
    // (sous-zones 5 a 8). Deux corps au MEME creneau de leurs blocs, et donc le meme sous-type ;
    // ils sont identiques jusqu'a la derniere instruction, ou ils se separent.
    // =====================================================================

    /// <summary>Ghidra: <c>DAT_005e9060._2_1_</c> — l'octet 2 du bloc que
    /// <see cref="Memory.s_PanelSelection_005e9060"/> ouvre. <see cref="EntityHandler_0041D860"/>
    /// s'en sert comme <b>numero d'emplacement</b> pour designer le locuteur d'une boite de
    /// dialogue, exactement comme <c>0x005de538</c> ailleurs — mais ce sont deux globales
    /// differentes, verifiees au desassembleur (<c>mov al,[5E9062h]</c> contre
    /// <c>mov al,[5DE538h]</c>).
    /// TODO(REVERSE) : qui ecrit <c>0x005e9062</c>, et pourquoi la zone 5 l'emploie la ou sa jumelle
    /// emploie <c>0x005de538</c>.</summary>
    private static ref byte s_Unknown_005e9062 => ref Mem.s_PanelSelection_005e9060[2];

    /// <summary>
    /// Le compte a rebours partage par <see cref="EntityHandler_0041D860"/> et
    /// <see cref="EntityHandler_00420860"/>. <b>Dix images de secousse</b>, apres quoi chacune fait
    /// sa propre chose.
    ///
    /// <para>
    /// La naissance joue le son <c>0x3e</c> de la sous-zone par
    /// <see cref="Audio.PlaySoundEffect"/> — donc <b>sans attenuation par la distance</b> — a
    /// hauteur <c>0x200</c>, gain plein sur les deux voies, et arme <c>+0x12</c> a dix.
    /// </para>
    /// <para>
    /// <b>Le compteur alterne de signe a chaque image</b>, et c'est ce qui fait la secousse :
    /// <c>0x005de0ac</c> — le deuxieme des trois mots de secousse de camera, celui que
    /// <see cref="TryPublishCameraEffectNearEntity"/> publie — recoit <c>+0x12</c> tel quel, puis le
    /// corps rapproche <c>+0x12</c> de zero d'une unite (par le haut ou par le bas) et le
    /// <b>nie</b>. La camera reçoit donc <c>+10, -9, +8, -7…</c> : une oscillation amortie qui
    /// s'eteint en dix images.
    /// </para>
    /// <para>
    /// <b>Les trois mots de secousse sont remis a zero en tete de chaque image</b> par le prologue
    /// de <c>RunWorldMapTick</c> ; l'ecriture directe ici est donc une requete valable une image, au
    /// meme titre que celle de sa sœur — a ceci pres qu'elle ne passe pas par le garde de distance.
    /// </para>
    /// </summary>
    /// <returns><c>true</c> a l'image ou le compteur atteint zero, une seule fois.</returns>
    private static bool TickQuakeCountdown(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 10;

            // Ghidra: FUN_0053cea0(**(int **)(DAT_005e97cc + 0xa8) + 0x3e, 0x200, 0x1000, 0x1000)
            Aud.PlaySoundEffect(ZoneSound(0x3e), 0x200, 0x1000, 0x1000);
        }

        *Mem.Camera(0x005de0ac) = *(short*)(e + 0x12);

        if (*(short*)(e + 0x12) > 0) (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) (*(short*)(e + 0x12))++;

        short swung = (short)-*(short*)(e + 0x12);
        *(short*)(e + 0x12) = swung;

        if (swung != 0) return false;

        *(uint*)e &= 0x7fffffff;
        return true;
    }

    /// <summary>
    /// Ghidra: FUN_0041d860 @ 0x0041d860 — creneau <b>63</b> du bloc <c>0x00557da8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 53 instructions.
    ///
    /// <para>
    /// <b>La secousse qui finit sur une phrase.</b> Dix images de tremblement
    /// (<see cref="TickQuakeCountdown"/>), puis <see cref="BindMenuLabels"/> avec le libelle
    /// <b>1</b> de la table <c>+0x70</c> de la zone et le descripteur <c>0x005633b0</c>. Le locuteur
    /// est l'emplacement que <see cref="s_Unknown_005e9062"/> designe.
    /// </para>
    /// <para>
    /// <b>Elle ne pose PAS <c>0x005e99c8</c> a <c>0x40</c></b>, contrairement a tous les autres
    /// panneaux de la carte du monde releves jusqu'ici. C'est la seule difference de forme avec
    /// eux, et elle est dans le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041D860(byte* e)
    {
        if (!TickQuakeCountdown(e)) return;

        // Ghidra: FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97cc + 0x70) + 4),
        //                      &DAT_005e93c0 + DAT_005e9060._2_1_ * 0x27, &DAT_005633b0, 0)
        byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4);
        byte* speaker = Inp.s_SlotFlags_005e93c0 + s_Unknown_005e9062 * 0x9c;

        BindMenuLabels(labels, (uint)(nint)speaker, s_WorldMapDescriptor_005633b0, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00420860 @ 0x00420860 — creneau <b>63</b> du bloc <c>0x00558428</c>
    /// (<c>0x0056b0c8</c>, sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 50 instructions.
    ///
    /// <para>
    /// <b>La meme secousse, qui finit sur un script.</b> Les cinquante premieres instructions sont
    /// celles de sa jumelle <see cref="EntityHandler_0041D860"/> ; a la derniere image elle appelle
    /// <see cref="RunEntityScript"/> avec le script <b>0x15</b>, sur l'emplacement que
    /// <see cref="Memory.s_Unknown_005de538"/> designe — pas la meme globale que sa jumelle, verifie
    /// au desassembleur.
    /// </para>
    /// <para>
    /// Les deux corps occupent le <b>meme creneau</b> de leurs blocs respectifs, donc le meme
    /// sous-type : c'est bien <i>un</i> objet du jeu, dont la moitie basse du labyrinthe parle et
    /// dont la moitie haute declenche une scene.
    /// </para>
    /// </summary>
    private static void EntityHandler_00420860(byte* e)
    {
        if (!TickQuakeCountdown(e)) return;

        // Ghidra: FUN_00513dd0(DAT_005e97cc, &DAT_005e93c0 + DAT_005de538 * 0x27, 0x15)
        byte* subject = Inp.s_SlotFlags_005e93c0 + (Mem.s_Unknown_005de538 & 0xff) * 0x9c;

        RunEntityScript(s_Unknown_005e97cc, subject, 0x15);
    }

    // =====================================================================
    // LOT 7 (2026-09-18) — LES TROIS « MOYENS » QUI PRENNENT LE GROUPE EN CHARGE
    // =====================================================================

    /// <summary>Ghidra: <c>DAT_005e9065</c> — l'octet 5 du bloc de
    /// <see cref="Memory.s_PanelSelection_005e9060"/>. <see cref="EntityHandler_004633C0"/> s'en
    /// sert comme <b>autorisation</b> : tant qu'il vaut zero, le mecanisme entier rend la main.
    /// TODO(REVERSE) : qui l'ecrit. Il est voisin de <see cref="s_Unknown_005e9062"/>, sans qu'on
    /// sache encore si les deux appartiennent a la meme structure.</summary>
    private static ref byte s_Unknown_005e9065 => ref Mem.s_PanelSelection_005e9060[5];

    /// <summary>
    /// Ghidra: FUN_00461730 @ 0x00461730 — creneau <b>40</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 129 instructions.
    ///
    /// <para>
    /// <b>Elle guette le groupe, puis file en ligne droite et lance un script.</b> La naissance met
    /// sa position de depart de cote — <c>+0x50</c> et <c>+0x58</c> pour <c>X</c> et <c>Z</c> — et
    /// <c>+0x5c</c> sert de verrou a un coup.
    /// </para>
    /// <para>
    /// <b>La detection balaie les quatre emplacements a chaque image</b>, y compris apres le
    /// depart, et ne mesure que la distance <b>horizontale</b>. Sous <b>200</b> cases, et
    /// seulement si elle est encore a l'etat 1, elle part avec le son <c>0x26</c> de la sous-zone a
    /// <b>gain <c>0x2800</c></b> — deux fois et demie le maximum ordinaire, le plus fort de toute
    /// la zone.
    /// </para>
    /// <para>
    /// <b>Sa course est une droite le long de son cap <c>+0x32</c></b>, a une distance
    /// <c>+0x12</c> qui croit de 3 par image depuis le point de depart, jusqu'a <c>0x46</c>. Elle
    /// seme un effet <c>0x37</c> par image, <b>plus un second une fois sur deux</b>.
    /// </para>
    /// <para>
    /// <b>Le script part passe la 45e image de course, et une seule fois</b> : <c>+0x5c</c> passe
    /// a 1 et la course s'arrete la — le <c>return</c> saute tout le reste du corps. L'etat 3 qui
    /// suit la course normale n'est donc atteint que si le script n'a jamais pu partir.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461730(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x5c) = 0;
        }

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 4; i != 0; i--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(slot + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(slot + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 200) continue;
            if (*(short*)(e + 0x10) != 1) continue;

            *(short*)(e + 0x10) = 2;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x26), 0x300, 0x2800, 0, 0);
        }

        if (*(short*)(e + 0x10) != 2) return;

        if (*(short*)(e + 0x12) > 0x2d && *(int*)(e + 0x5c) == 0)
        {
            // Ghidra: FUN_00513dd0(DAT_005e97cc, param_1, (int)*(short *)(param_1 + 0x14))
            RunEntityScript(s_Unknown_005e97cc, e, (uint)*(short*)(e + 0x14));
            *(int*)(e + 0x5c) = 1;
            return;
        }

        SpawnEffectNearCamera_00468110(e, 0x37, 0, -1);                  // Ghidra: FUN_00468110
        if (CrtRand() % 2 == 0) SpawnEffectNearCamera_00468110(e, 0x37, 0, -1);

        short reach = *(short*)(e + 0x12);

        *(int*)(e + 0x24) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * reach
                          + *(int*)(e + 0x58);
        *(int*)(e + 0x1c) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * reach
                          + *(int*)(e + 0x50);

        reach += 3;
        *(short*)(e + 0x12) = reach;
        if (reach > 0x46) *(short*)(e + 0x10) = 3;
    }

    /// <summary>
    /// Ghidra: <c>0x004633c0</c> — creneau <b>29</b> du bloc <c>0x005587a8</c> (sous-zones 4 et 9).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>141 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_004633C0.asm</c>.
    ///
    /// <para>
    /// <b>Le mecanisme a usage unique de la zone : il fige la partie, s'ouvre, et dit une
    /// phrase.</b> Le <b>drapeau 230</b> — bit 3 de <c>0x005e96fc</c>, banque 0 — est sa memoire :
    /// pose, il veut dire « deja fait », et le corps se contente alors, a la premiere image, de se
    /// decaler de <c>0x1c000</c> et d'un huitieme de tour pour rester dans sa pose finale, puis ne
    /// fait plus rien.
    /// </para>
    /// <para>
    /// <b>Trois gardes avant de partir</b> : le drapeau doit etre absent, <c>0x005e9065</c> doit
    /// etre non nul, et le membre en <c>+0x6c</c> — celui qui l'a touche — ne doit pas porter
    /// <c>0x10000</c>. S'il le porte, l'entite prend <c>0x40000000</c> et s'eteint sans rien
    /// faire.
    /// </para>
    /// <para>
    /// <b>Le depart FIGE tout</b> (<see cref="FreezeAllEntities"/>), retient le membre en
    /// <c>+0x74</c> et efface le bit <c>0x04000000</c>. Suivent deux mouvements, chacun sonnant le
    /// <c>0x26</c> de la sous-zone <b>toutes les cinq images</b> : quatorze images de translation a
    /// <c>0x2000</c>, puis une <b>rotation qui accelere</b> — <c>+0x5c</c> part de 5 et gagne 5 par
    /// image, et c'est lui qu'on ajoute a l'angle <c>+0x34</c> et au cumul <c>+0x12</c>.
    /// </para>
    /// <para>
    /// <b>A <c>0x200</c> de cumul, tout se referme</b> : le son <c>0x28</c>, le drapeau 230 pose,
    /// <see cref="UnfreezeAllEntities"/>, et — <b>une seule fois</b>, garde par le bit <c>0x20</c>
    /// de <see cref="Memory.s_StateBank3_005e9840"/> — le libelle <b>1</b> de la zone affiche par
    /// <see cref="BindMenuLabels"/>, avec le membre retenu pour locuteur. Comme
    /// <see cref="EntityHandler_0041D860"/>, elle ne pose <b>pas</b> <c>0x005e99c8</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_004633C0(byte* e)
    {
        bool alreadyDone = (Mem.s_StateBank0_005e96e0[0x1c] & 8) != 0;

        if (alreadyDone && *(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x1c) += 0x1c000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x34) += 0x200;
            return;
        }

        if (alreadyDone && *(short*)(e + 0x10) == 1) return;

        if (s_Unknown_005e9065 == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            byte* toucher = *(byte**)(e + 0x6c);

            if (toucher != null && (*(uint*)toucher & 0x10000) != 0)
            {
                *(uint*)e |= 0x40000000;
                return;
            }

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x98) = 0;
            *(byte**)(e + 0x74) = toucher;

            FreezeAllEntities();                          // Ghidra: FUN_00535300(0)
            *(uint*)e &= 0xfbffffff;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) == 5)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x26), 0x200, 0x1000, 0, 0);
                *(short*)(e + 0x98) = 0;
            }

            *(int*)(e + 0x1c) += 0x2000;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x0e)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x5c) = 5;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) == 5)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x26), 0x200, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0;
        }

        short spin = (short)*(int*)(e + 0x5c);
        *(short*)(e + 0x34) += spin;
        *(short*)(e + 0x12) += spin;
        *(int*)(e + 0x5c) += 5;

        if (*(short*)(e + 0x12) < 0x200) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x28), 0x100, 0x1000, 0, 0);
        *(short*)(e + 0x10) = 3;

        Mem.s_StateBank0_005e96e0[0x1c] |= 8;             // le drapeau 230
        UnfreezeAllEntities();                            // Ghidra: FUN_00535400

        if ((Mem.s_StateBank3_005e9840[0] & 0x20) != 0) return;
        Mem.s_StateBank3_005e9840[0] |= 0x20;

        byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4);
        BindMenuLabels(labels, *(uint*)(e + 0x74), s_WorldMapDescriptor_005633b0, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x004614b0</c> — creneau <b>42</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>204 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_004614B0.asm</c>.
    ///
    /// <para>
    /// <b>Ce qui roule le long d'un couloir et POUSSE ce qu'il rencontre.</b> C'est le seul corps
    /// du labyrinthe qui ecrive la <b>position</b> d'un membre du groupe, et non seulement sa
    /// vitesse.
    /// </para>
    /// <para>
    /// <b>Son elan a une forme : il monte, plafonne, puis retombe.</b> <c>+0x14</c> est un decompte
    /// pose par le placement ; tant qu'il est positif, <c>+0x5c</c> monte d'un par image jusqu'a
    /// <b>16</b> ; une fois epuise, <c>+0x5c</c> redescend, et a zero l'etat passe a <b>2</b> —
    /// l'arret, ou le bit <c>0x2000</c> tombe. <c>+0x5c</c> est aussi la <b>vitesse</b> : la chose
    /// avance de <c>sinus/cosinus * +0x5c</c> le long de son cap.
    /// </para>
    /// <para>
    /// <b>Six effets <c>0x38</c> par image</b>, en deux bouffees de quatre et deux — le binaire
    /// ecrit deux boucles a la suite, pas une de six.
    /// </para>
    /// <para>
    /// <b>La poussee, emplacement par emplacement.</b> Un membre est bouscule s'il est a moins de
    /// <b>300</b> cases en <c>X</c> <i>et</i> en <c>Z</c>, et si la chose est <b>plus haute que
    /// lui de plus de 200 cases</b>. Alors : son bit <c>1</c> est pose, <c>+0x12</c> de la chose
    /// passe a 2, sa vitesse devient <c>0xa000</c> vers le haut et <c>sinus/cosinus * 100</c> le
    /// long du cap, et le bit de son rang est pose dans l'octet de contact <c>+9</c> de la chose.
    /// </para>
    /// <para>
    /// <b>Et il est DEPLACE de <c>0xa000</c> d'un coup</b>, mais sur un seul axe, choisi par le
    /// cap : <b>Z</b> quand le cap n'est ni <c>0</c> ni <c>0x800</c>, <b>X</b> sinon — et
    /// seulement si l'ecart sur cet axe depasse 190 cases. Le sens suit le signe de l'ecart : on
    /// est pousse <i>plus loin</i>, jamais vers la chose.
    /// </para>
    /// <para>
    /// Le binaire relit <c>+0x32</c> dans <c>BP</c> et le rerange tel quel en fin de tour ; c'est
    /// un reste de compilation, il n'y a rien a transcrire.
    /// </para>
    /// </summary>
    private static void EntityHandler_004614B0(byte* e)
    {
        *(short*)(e + 0x28) = 0x10cc;

        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x98) = 0;
        }

        if (state == 1)
        {
            if (*(short*)(e + 0x98) == 0)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2a), 0x300, 0x1800, 0, 0);

            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) == 0x0f) *(short*)(e + 0x98) = 0;

            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < 0)
            {
                *(short*)(e + 0x12) = 0;
                *(uint*)e &= 0xffffdfff;
            }

            (*(short*)(e + 0x14))--;
            if (*(short*)(e + 0x14) < 0)
            {
                (*(int*)(e + 0x5c))--;
                if (*(int*)(e + 0x5c) < 0) *(short*)(e + 0x10) = 2;
            }
            else
            {
                (*(int*)(e + 0x5c))++;
                if (*(int*)(e + 0x5c) >= 0x10) *(int*)(e + 0x5c) = 0x10;
            }

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);

            *(short*)(e + 0x16) = 0;

            for (int i = 4; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x38, 0, -1);
            for (int i = 2; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x38, 0, -1);

            short course = *(short*)(e + 0x32);
            byte* slot = Inp.s_SlotFlags_005e93c0;

            for (int i = 0; i < 4; i++, slot += 0x9c)
            {
                // Ghidra: la difference d'abord, le decalage ensuite.
                int dx = (*(int*)(e + 0x1c) - *(int*)(slot + 0x1c)) >> 0xc;
                int dz = (*(int*)(e + 0x24) - *(int*)(slot + 0x24)) >> 0xc;

                int spanX = dx < 0 ? -dx : dx;
                int spanZ = dz < 0 ? -dz : dz;

                if (spanX >= 0x12c || spanZ >= 0x12c) continue;
                if ((*(int*)(e + 0x20) >> 0xc) - 0xc8 >= *(int*)(slot + 0x20) >> 0xc) continue;

                *(short*)(e + 0x12) = 2;
                *(uint*)slot |= 1;

                if (course != 0 && course != 0x800)
                {
                    if (spanZ > 0xbe)
                        *(int*)(slot + 0x24) += dz < 0 ? -0xa000 : 0xa000;
                }
                else
                {
                    if (spanX > 0xbe)
                        *(int*)(slot + 0x1c) += dx < 0 ? -0xa000 : 0xa000;
                }

                *(int*)(slot + 0x54) = 0xa000;

                // Ghidra: `lea eax,[eax+eax*4] ; lea ecx,[eax+eax*4] ; shl ecx,2` — fois cent.
                *(int*)(slot + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * 100;
                *(int*)(slot + 0x58) = Winsys3d.GetCosTableEntry(course) * 100;

                e[9] |= (byte)(1 << i);
            }

            StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);       // Ghidra: FUN_0045da40
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0xffffdfff;
    }

    // =====================================================================
    // CRENEAU 43 DU BLOC 0x005587a8 — LE BATTANT QUI BASCULE ET ENTRAINE SA VOISINE
    // =====================================================================

    /// <summary>L'enregistrement voisin que <see cref="EntityHandler_00462A80"/> entraine :
    /// <c>e + (short)+0x14 × 0x9c</c>, dans le meme pool. Relu a chaque emploi, comme le fait le
    /// binaire — les scripts lances en cours de corps pourraient le changer.</summary>
    private static byte* HingeNeighbour(byte* e) => e + *(short*)(e + 0x14) * 0x9c;

    /// <summary>
    /// Ghidra: FUN_00462a80 @ 0x00462a80 — creneau <b>43</b> du bloc <c>0x005587a8</c>
    /// (<c>0x0056adc8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 536 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_00462A80.asm</c>. Lot 13, 2026-09-18 : <b>le dernier
    /// corps de la zone 5</b>.
    ///
    /// <para>
    /// <b>Un battant qui bascule et entraine une voisine.</b> Il pivote sur son tangage
    /// <c>+0x34</c> ; la voisine — l'enregistrement a <c>+0x14</c> rangs de <c>0x9c</c>, le meme
    /// adressage relatif que <see cref="EntityHandler_00463150"/> — glisse avec lui selon
    /// l'orientation <c>+0x32</c> (0, <c>0x400</c>, <c>0x800</c> ou <c>0xc00</c> : X+, Z-, X-, Z+),
    /// tangue de son cote et monte ou descend avec le cosinus.
    /// </para>
    /// <list type="table">
    ///   <item><term>0</term><description>la voisine prend <c>0x4000</c>, le script 1 part —
    ///     sans avancer l'etat : c'est le script qui fait la suite.</description></item>
    ///   <item><term>1, l'ouverture</term><description><c>+0x34</c> monte de <c>0x11</c> par
    ///     image jusqu'a zero, en sonnant (son de zone <c>0x1d</c>) une image sur huit ; a zero, le
    ///     son <c>0x27</c> et l'etat 2.</description></item>
    ///   <item><term>2 et <c>0xc9</c>, la secousse</term><description><c>+0x12</c> change de signe
    ///     et se rapproche de zero d'un cran par image, publie par
    ///     <see cref="TryPublishCameraEffectNearEntity"/> ; a zero, l'etat suivant (3,
    ///     <c>0xca</c>). L'etat 2 relance a chaque image les scripts 2 et 4.</description></item>
    ///   <item><term>99, le rattrapage</term><description>la voisine perd <c>0x4000</c>, sa hauteur
    ///     est posee a <c>0xffe63000</c>, les scripts 2 et 4 partent, et <b>soixante pas</b>
    ///     d'ouverture sont joues d'un coup, avec d'autres coefficients et sans mouvement
    ///     vertical ; puis l'etat 100. C'est la position « deja ouvert » qu'un script impose.</description></item>
    ///   <item><term>200, la fermeture</term><description>les scripts 1 et 3 a chaque image, le
    ///     son <c>0x1d</c> une image sur trois, <c>+0x34</c> descend de <c>0x22</c> jusqu'a
    ///     <c>-0x400</c>, la voisine repart en sens inverse, deux fois plus vite ; puis le son
    ///     <c>0x27</c> et la secousse <c>0xc9</c>.</description></item>
    /// </list>
    /// <para>
    /// Les coefficients se lisent dans <c>Blaze.c</c> tels quels : <c>Ordinal_451</c> (le sinus) y
    /// est appele deux ou trois fois sur le <b>meme</b> angle dans une meme expression ; la table
    /// est pure, les appels sont gardes a leur place pour la lecture cote a cote.
    /// </para>
    /// </summary>
    private static void EntityHandler_00462A80(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)HingeNeighbour(e) |= 0x4000;
            *(short*)(e + 0x98) = 0;
            RunEntityScript(s_Unknown_005e97cc, null, 1);            // Ghidra: FUN_00513dd0
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x98) == 0)
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1d], 0x600, 0xb00, 0, 0);   // Ghidra: FUN_00468080
            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) == 8) *(short*)(e + 0x98) = 0;

            *(short*)(e + 0x34) += 0x11;
            if (*(short*)(e + 0x34) >= 0)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 10;
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x27], 0x400, 0x1000, 0, 0);
            }

            short heading = *(short*)(e + 0x32);
            if (heading == 0)
                *(int*)(HingeNeighbour(e) + 0x1c) += (Sin(e) >> 1) + Sin(e) * 6;
            if (*(short*)(e + 0x32) == 0x400)
                *(int*)(HingeNeighbour(e) + 0x24) = *(int*)(HingeNeighbour(e) + 0x24) + Sin(e) * -6 - (Sin(e) >> 1);
            if (*(short*)(e + 0x32) == 0x800)
                *(int*)(HingeNeighbour(e) + 0x1c) = *(int*)(HingeNeighbour(e) + 0x1c) + Sin(e) * -6 - (Sin(e) >> 1);
            if (*(short*)(e + 0x32) == 0xc00)
                *(int*)(HingeNeighbour(e) + 0x24) += (Sin(e) >> 1) + Sin(e) * 6;

            *(short*)(HingeNeighbour(e) + 0x34) += 5;
            *(int*)(HingeNeighbour(e) + 0x20) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x34)) * 6;   // Ghidra: Ordinal_450
        }

        if (*(short*)(e + 0x10) == 2)
        {
            RunEntityScript(s_Unknown_005e97cc, null, 2);
            RunEntityScript(s_Unknown_005e97cc, null, 4);
            StepHingeJolt(e, 3);
        }

        if (*(short*)(e + 0x10) == 99)
        {
            *(uint*)HingeNeighbour(e) &= 0xffffbfff;
            *(int*)(HingeNeighbour(e) + 0x20) = unchecked((int)0xffe63000);
            RunEntityScript(s_Unknown_005e97cc, null, 2);
            RunEntityScript(s_Unknown_005e97cc, null, 4);

            for (int step = 0x3c; step != 0; step--)
            {
                *(short*)(e + 0x34) += 0x11;

                if (*(short*)(e + 0x32) == 0)
                    *(int*)(HingeNeighbour(e) + 0x1c) += (Sin(e) >> 2) + Sin(e) * 6 + (Sin(e) >> 1);
                if (*(short*)(e + 0x32) == 0x400)
                    *(int*)(HingeNeighbour(e) + 0x24) = *(int*)(HingeNeighbour(e) + 0x24) + Sin(e) * -6 - (Sin(e) >> 2) - (Sin(e) >> 1);
                if (*(short*)(e + 0x32) == 0x800)
                    *(int*)(HingeNeighbour(e) + 0x1c) = *(int*)(HingeNeighbour(e) + 0x1c) + Sin(e) * -6 - (Sin(e) >> 2) - (Sin(e) >> 1);
                if (*(short*)(e + 0x32) == 0xc00)
                    *(int*)(HingeNeighbour(e) + 0x24) += (Sin(e) >> 1) + (Sin(e) >> 2) + Sin(e) * 6;

                *(short*)(HingeNeighbour(e) + 0x34) += 5;
            }

            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x10) = 100;
        }

        if (*(short*)(e + 0x10) == 200)
        {
            RunEntityScript(s_Unknown_005e97cc, null, 1);
            RunEntityScript(s_Unknown_005e97cc, null, 3);

            if (*(short*)(e + 0x98) == 0)
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1d], 0x600, 0xb00, 0, 0);
            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) > 2) *(short*)(e + 0x98) = 0;

            *(short*)(e + 0x34) -= 0x22;
            if (*(short*)(e + 0x34) < -0x3ff)
            {
                *(short*)(e + 0x34) = -0x400;                        // Ghidra: 0xfc00
                *(short*)(e + 0x10) = 0xc9;
                *(short*)(e + 0x12) = 0x14;
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x27], 0x500, 0x1800, 0, 0);
            }

            if (*(short*)(e + 0x32) == 0)
                *(int*)(HingeNeighbour(e) + 0x1c) = *(int*)(HingeNeighbour(e) + 0x1c) + Sin(e) * -0xc - Sin(e);
            if (*(short*)(e + 0x32) == 0x400)
                *(int*)(HingeNeighbour(e) + 0x24) += Sin(e) + Sin(e) * 0xc;
            if (*(short*)(e + 0x32) == 0x800)
                *(int*)(HingeNeighbour(e) + 0x1c) += Sin(e) + Sin(e) * 0xc;
            if (*(short*)(e + 0x32) == 0xc00)
                *(int*)(HingeNeighbour(e) + 0x24) = *(int*)(HingeNeighbour(e) + 0x24) + Sin(e) * -0xc - Sin(e);

            *(short*)(HingeNeighbour(e) + 0x34) -= 10;
            *(int*)(HingeNeighbour(e) + 0x20) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x34)) * -0xc;
        }

        if (*(short*)(e + 0x10) == 0xc9) StepHingeJolt(e, 0xca);

        // Ghidra: Ordinal_451 sur le tangage courant du battant, relu a chaque appel.
        static int Sin(byte* hinge) => (int)Winsys3d.GetSinTableEntry(*(short*)(hinge + 0x34));
    }

    /// <summary>La secousse des etats 2 et <c>0xc9</c> de <see cref="EntityHandler_00462A80"/>,
    /// ecrite deux fois a l'identique : <c>+0x12</c> change de signe et se rapproche de zero d'un
    /// cran, la camera est publiee a <c>(999, +0x12, 999)</c>, et a zero l'etat passe a
    /// <paramref name="next"/>.</summary>
    private static void StepHingeJolt(byte* e, short next)
    {
        short flipped = (short)-*(short*)(e + 0x12);
        short jolt = (short)(flipped + (flipped < 1 ? 1 : -1));
        *(short*)(e + 0x12) = jolt;

        TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);   // Ghidra: FUN_0041be60

        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x10) = next;
            *(short*)(e + 0x12) = 0;
        }
    }
}
