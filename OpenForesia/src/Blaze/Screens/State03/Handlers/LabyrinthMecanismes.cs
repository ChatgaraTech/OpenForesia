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
}
