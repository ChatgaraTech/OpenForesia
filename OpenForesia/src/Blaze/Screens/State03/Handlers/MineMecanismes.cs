// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DE LA MINE ABANDONNEE — zone 1, table 0x0056adc8.
//
// Detache de `Handlers/Mine.cs` le 2026-09-16, a 3 734 lignes. La coupure n'est pas une coupure
// de taille : c'est une coupure de ROLE, et elle a un test d'appartenance.
//
//   Mine.cs             le decor : ce qui sonne, bouge, brille, tombe — et ce qui frappe.
//                       Une masse qui ecrase le groupe reste un decor : elle lui enleve des
//                       points de vie, elle ne decide de rien.
//
//   MineMecanismes.cs   ce qui prend le GROUPE ou la PARTIE en charge : le porter, le saisir,
//                       lui couper les manettes, changer de carte, lancer un script sur lui.
//
// Le test se lit dans le corps, pas dans le nom : un gestionnaire appartient a ce fichier s'il
// compte les membres du groupe (`CountPartyMembersMatching`, `ActivePartySlotCount_00466D70`),
// s'il lit la table des emplacements, s'il lance un script, ou s'il coupe les manettes. C'est
// pourquoi le pilon et les deux masses sont restes dans `Mine.cs` alors qu'ils touchent le
// groupe, et pourquoi la tremie est ici alors qu'elle ressemble a un bloc pousse.
//
// Les deux fichiers sont la MEME classe partielle. `ZoneSound`, `AddEntityVelocity_00454580`,
// `EmitJetPair` et `ScrapeSoundTick` vivent dans `Mine.cs` et servent des deux cotes ; rien n'a
// ete duplique. Aucun des deux fichiers ne declare de champ statique, ce qui met la coupure a
// l'abri du piege d'ordre d'initialisation decrit dans CLAUDE.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES DEUX ASCENSEURS
    //
    // Creneau 28 du bloc 0x005539a8 (sous-zones 0 a 2) et creneau 27 du bloc 0x005546a8
    // (sous-zones 3, 4 et 9). Meme forme : la plateforme compte les membres du groupe DANS UNE
    // BOITE codee en dur, et quand ils y sont TOUS, elle lance le script 2 sur l'un d'eux. Les
    // etats sont ensuite deux courses symetriques, l'une vers le haut et l'autre vers le bas,
    // avec le meme son toutes les dix images.
    //
    // La boite est le seul endroit du corps qui dise ou l'ascenseur se trouve : c'est un volume
    // en (x, z), la hauteur n'y entre jamais.
    // =====================================================================

    /// <summary>
    /// Le bloc de vue que les deux ascenseurs publient quand le groupe est a bord : les quatre
    /// dwords de <c>+0x80</c>, la position, le mot de contact et la face. C'est la copie que fait
    /// deja l'embarcation de la foret ; voir <c>Forest.cs</c>.
    /// </summary>
    private static void PublishCarrierView(byte* e)
    {
        for (int i = 0; i < 4; i++)
        {
            *(uint*)Mem.Entity(0x005f7d70 + i * 4) = *(uint*)(e + 0x80 + i * 4);
        }

        *(int*)Mem.Arena(0x005e9a30) = *(int*)(e + 0x1c);
        *(int*)Mem.Arena(0x005e9a34) = *(int*)(e + 0x20);
        *(int*)Mem.Arena(0x005e9a38) = *(int*)(e + 0x24);
        *(short*)Mem.Arena(0x005e9b38) = *(short*)(e + 0x90);
        *(uint*)Mem.Entity(0x005f7b48) = *(uint*)(e + 0x7c);
    }

    /// <summary>
    /// Combien de membres du groupe se tiennent dans la boite <c>[minX, maxX] x [minZ, maxZ]</c>,
    /// bornes <b>exclues</b> des deux cotes. Seuls les emplacements actifs comptent — bit de signe
    /// pose, masque <c>0x50000000</c> vide, la meme lecture que
    /// <see cref="ActivePartySlotCount_00466D70"/>. La hauteur n'entre pas dans le test.
    /// </summary>
    private static short PartyMembersInBox(int minX, int maxX, int minZ, int maxZ)
    {
        short inside = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            int x = *(int*)(member + 0x1c);
            if (x <= minX || x >= maxX) continue;

            int z = *(int*)(member + 0x24);
            if (z <= minZ || z >= maxZ) continue;

            inside++;
        }

        return inside;
    }

    /// <summary>
    /// A qui l'ascenseur adresse son script : le meneur <c>0x005de538</c> s'il est actif, sinon
    /// <b>le dernier</b> emplacement actif — la boucle de repli ecrase son candidat a chaque tour
    /// et ne s'arrete pas au premier trouve. Aucun emplacement actif laisse le meneur tel quel.
    /// </summary>
    private static byte* PartyScriptTarget()
    {
        byte* members = Inp.s_SlotFlags_005e93c0;
        byte leader = (byte)Mem.s_Unknown_005de538;

        uint flags = *(uint*)(members + leader * 0x9c);
        short chosen = leader;

        if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                uint slot = *(uint*)(members + i * 0x9c);
                if ((slot & 0x80000000) == 0) continue;
                if ((slot & 0x50000000) != 0) continue;
                chosen = (short)i;
            }
        }

        return members + chosen * 0x9c;
    }

    /// <summary>Ce que fait l'ascenseur de <see cref="EntityHandler_00452090"/> quand le verrou de
    /// la salle est efface : il pose les bits <c>0x6000</c> de son mot d'etat et rend la main au
    /// script 1. Aucun de ses etats de course ne tourne cette image-la.</summary>
    private static void LiftStandsDown(byte* e)
    {
        *(uint*)e |= 0x6000;
        RunEntityScript(s_Unknown_005e97cc, e, 1);
    }

    /// <summary>Le grondement des deux ascenseurs : le son <c>0x1b</c> de la zone toutes les dix
    /// images, le compteur <c>+0x5c</c> etant teste a 10 <b>avant</b> d'etre incremente.</summary>
    private static void LiftRumble(byte* e)
    {
        if (*(int*)(e + 0x5c) == 0x0a)
        {
            *(int*)(e + 0x5c) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1b), 0x200, 0x1000, 0, 0);
        }

        *(int*)(e + 0x5c) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00452090</c> — <b>l'ascenseur des sous-zones 0 a 2</b>, sous-type <b>28</b> du
    /// bloc <c>0x005539a8</c>. Absente de <c>Blaze.c</c> ; 129 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00452090.asm</c>.
    ///
    /// <para>
    /// <b>En tete</b>, comme <see cref="EntityHandler_00454800"/> : sans membre rattache elle lance
    /// le script 1 sur elle-meme, avec des membres rattaches elle publie le bloc de vue. Puis elle
    /// compte le groupe dans la boite <c>x dans ]0x7eb000, 0x863000[</c>,
    /// <c>z dans ]0, 0xb4000[</c>. Boite vide et etat <c>0xfa</c> : l'etat revient a 0. Boite
    /// pleine — <b>tout</b> le groupe actif — et etat 0 : le script 2 part sur
    /// <see cref="PartyScriptTarget"/>.
    /// </para>
    /// <para>
    /// <b>Le bit <c>0x40</c> de <c>0x005e96fc</c> commande tout le reste.</b> Efface, l'entite pose
    /// les bits <c>0x6000</c> de son mot d'etat, lance le script 1 et rend la main : les etats de
    /// course ne tournent pas. Pose, les quatre etats se deroulent — et l'etat 0 place la cabine
    /// en haut (<c>-0x1a4000</c>) en effacant <c>0x6000</c>.
    /// </para>
    /// <list type="table">
    ///   <item><term>1</term><description>en haut, puis etat 2 et script 0</description></item>
    ///   <item><term>2</term><description>elle descend de <c>0xa000</c> par image, butee a
    ///     <c>-0x514000</c></description></item>
    ///   <item><term>0x65</term><description>en bas (<c>-0x49c000</c>), puis etat <c>0x66</c> et
    ///     script 0</description></item>
    ///   <item><term>0x66</term><description>elle remonte de <c>0xa000</c> par image ; arrivee en
    ///     haut, etat <c>0xfa</c> et script 1</description></item>
    /// </list>
    /// <para>Dans les deux courses, <see cref="LiftRumble"/> sonne toutes les dix images.</para>
    /// </summary>
    private static void EntityHandler_00452090(byte* e)
    {
        if ((short)CountPartyMembersMatching(e, 0x20, 0x40000000) == 0)
        {
            RunEntityScript(s_Unknown_005e97cc, e, 1);
        }
        else
        {
            PublishCarrierView(e);
        }

        short inside = PartyMembersInBox(0x7eb000, 0x863000, 0, 0xb4000);

        if (inside == 0
            && ActivePartySlotCount_00466D70() != 0
            && *(short*)(e + 0x10) == 0xfa)
        {
            *(short*)(e + 0x10) = 0;
        }

        // Le verrou de la salle, le bit 0x40 de 0x005e96fc. Il est relu APRES le script 2, et
        // c'est voulu : le binaire le teste a deux endroits, et le script qui vient de tourner a
        // pu le changer.
        if (inside != 0
            && inside == ActivePartySlotCount_00466D70()
            && *(short*)(e + 0x10) == 0)
        {
            if ((Mem.Arena(0x005e96fc)[0] & 0x40) == 0)
            {
                LiftStandsDown(e);
                return;
            }

            RunEntityScript(s_Unknown_005e97cc, PartyScriptTarget(), 2);
        }

        if ((Mem.Arena(0x005e96fc)[0] & 0x40) == 0)
        {
            LiftStandsDown(e);
            return;
        }

        const int top = -0x1a4000;      // 0xffe5c000
        const int bottom = -0x514000;   // 0xffaec000

        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(int*)(e + 0x20) = top;
            *(uint*)e &= 0xffff9fff;
        }

        if (state == 0x65)
        {
            *(int*)(e + 0x20) = -0x49c000;   // 0xffb64000
            *(short*)(e + 0x10) = 0x66;
            RunEntityScript(s_Unknown_005e97cc, e, 0);
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 0x66)
        {
            *(int*)(e + 0x20) += 0xa000;
            if (*(int*)(e + 0x20) > top)
            {
                *(int*)(e + 0x20) = top;
                *(short*)(e + 0x10) = 0xfa;
                RunEntityScript(s_Unknown_005e97cc, e, 1);
            }

            LiftRumble(e);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) = top;
            *(short*)(e + 0x10) = 2;
            RunEntityScript(s_Unknown_005e97cc, e, 0);
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x20) -= 0xa000;
            if (*(int*)(e + 0x20) < bottom) *(int*)(e + 0x20) = bottom;

            LiftRumble(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00454800 @ 0x00454800 — <b>l'ascenseur des sous-zones 3, 4 et 9</b>, sous-type
    /// <b>27</b> du bloc <c>0x005546a8</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Le jumeau de <see cref="EntityHandler_00452090"/>, <b>sans verrou de salle</b> : les etats
    /// tournent toujours. La boite est <c>x dans ]0x16e000, 0x1f5000[</c>,
    /// <c>z dans ]0x64a000, 0x6fe000[</c>, et les deux courses sont inversees — l'etat 2
    /// <b>monte</b> jusqu'a <c>0x2f8000</c>, l'etat <c>0x66</c> <b>descend</b> jusqu'a
    /// <c>0x5000</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 1 pose la hauteur a 5</b>, pas a <c>5 &lt;&lt; 12</c> : cinq millimes d'unite,
    /// c'est-a-dire le sol. C'est ce qu'ecrit le binaire, et l'etat 2 la remonte des la meme image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454800(byte* e)
    {
        if ((short)CountPartyMembersMatching(e, 0x20, 0x40000000) == 0)
        {
            RunEntityScript(s_Unknown_005e97cc, e, 1);
        }
        else
        {
            PublishCarrierView(e);
        }

        short inside = PartyMembersInBox(0x16e000, 0x1f5000, 0x64a000, 0x6fe000);

        if (inside == 0)
        {
            if (ActivePartySlotCount_00466D70() != 0 && *(short*)(e + 0x10) == 0xfa)
            {
                *(short*)(e + 0x10) = 0;
            }
        }
        else if (inside == ActivePartySlotCount_00466D70() && *(short*)(e + 0x10) == 0)
        {
            RunEntityScript(s_Unknown_005e97cc, PartyScriptTarget(), 2);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) = 5;
            *(short*)(e + 0x10) = 2;
            RunEntityScript(s_Unknown_005e97cc, e, 0);
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x20) += 0xa000;
            if (*(int*)(e + 0x20) > 0x2f8000) *(int*)(e + 0x20) = 0x2f8000;

            LiftRumble(e);
        }

        if (*(short*)(e + 0x10) == 0x65)
        {
            *(int*)(e + 0x20) = 0x258000;
            *(short*)(e + 0x10) = 0x66;
            RunEntityScript(s_Unknown_005e97cc, e, 0);
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 0x66)
        {
            *(int*)(e + 0x20) -= 0xa000;
            if (*(int*)(e + 0x20) < 0x5000)
            {
                *(int*)(e + 0x20) = 0x5000;
                *(short*)(e + 0x10) = 0xfa;
                RunEntityScript(s_Unknown_005e97cc, e, 1);
            }

            LiftRumble(e);
        }
    }

    // =====================================================================
    // LE WAGONNET SUR SON RAIL — creneau 44 des blocs 0x00554028 et 0x005546a8
    //
    // 0x004538C0 et 0x00455570 sont le MEME CORPS : 0x511 octets identiques a l'octet pres, sauf
    // les quatre deplacements relatifs de leurs appels, qui resolvent sur les memes cibles. Les
    // deux sont decompilees dans Blaze.c, et un `diff` des deux relevés le confirme.
    //
    // Le wagonnet ne va qu'en ligne droite, le long de son cap +0x32. Il connait son depart
    // (+0x44, +0x4c) et son arrivee (+0x48, +0x9a) parce qu'a sa NAISSANCE il joue toute sa course
    // d'un coup, en boucle serree, et note ou elle le mene avant de revenir se poser au depart.
    // =====================================================================

    /// <summary>
    /// Un pas du wagonnet, dans un sens ou dans l'autre. Le compteur de vitesse <c>+0x5c</c> monte
    /// d'un tant qu'il reste sous <c>+0x16</c> <b>et</b> que la distance parcourue <c>+0x12</c>
    /// reste sous <c>+0x14</c> ; passe cette distance, il redescend d'un par image. La position
    /// avance de <c>(sin, cos)</c> du cap multiplies par ce compteur.
    /// </summary>
    /// <param name="endsBelowZero">Le test d'arret. La boucle de naissance ecrit
    /// <c>cmp eax,-1 ; jg</c> (« au plus -1 ») la ou les deux etats de course ecrivent
    /// <c>cmp eax,-1 ; jne</c> (« exactement -1 »). Le compteur ne descendant jamais sous zero, les
    /// deux disent la meme chose — mais ce sont deux tests differents dans le binaire.</param>
    /// <returns>Vrai quand la course est finie et que l'appelant doit changer d'etat.</returns>
    private static bool RailCartStep(byte* e, bool forward, bool endsBelowZero)
    {
        if (*(int*)(e + 0x5c) < *(short*)(e + 0x16) && *(short*)(e + 0x12) < *(short*)(e + 0x14))
        {
            *(int*)(e + 0x5c) += 1;
        }

        short heading = *(short*)(e + 0x32);

        int alongX = (int)Winsys3d.GetSinTableEntry(heading) * *(int*)(e + 0x5c);   // Ordinal_451
        if (forward) *(int*)(e + 0x1c) += alongX; else *(int*)(e + 0x1c) -= alongX;

        int speed = *(int*)(e + 0x5c);
        int alongZ = Winsys3d.GetCosTableEntry(heading) * speed;                    // Ordinal_450
        if (forward) *(int*)(e + 0x24) += alongZ; else *(int*)(e + 0x24) -= alongZ;

        *(short*)(e + 0x12) += *(short*)(e + 0x5c);

        if (*(short*)(e + 0x12) <= *(short*)(e + 0x14)) return false;

        *(int*)(e + 0x5c) = speed - 1;
        return endsBelowZero ? speed - 1 <= -1 : speed - 1 == -1;
    }

    /// <summary>
    /// Le wagonnet dont le groupe s'est detache se replie sur celle de ses deux extremites qui est
    /// la plus proche du point vise par la camera. Son extremite « de repos » depend de l'etat :
    /// l'etat 8 se tient au depart <c>(+0x44, +0x4c)</c>, l'etat 2 a l'arrivee
    /// <c>(+0x48, +0x9a)</c>.
    ///
    /// <para>
    /// Si c'est bien la sienne la plus proche, il s'y pose et rien d'autre ne change. Sinon il
    /// passe a l'<b>autre</b> extremite, prend l'autre etat, et ses trois echelles retombent a
    /// zero — il reapparaitra en grandissant.
    /// </para>
    /// </summary>
    private static void RailCartSnapToAnchor(byte* e, bool homeIsStart)
    {
        int camX = *Mem.Camera(0x005de068) >> 0xc;
        int camZ = *Mem.Camera(0x005de070) >> 0xc;

        int sx = camX - *(int*)(e + 0x44);
        int sz = camZ - *(int*)(e + 0x4c);
        int toStart = (int)Winsys3d.Ordinal_442((uint)(sx * sx + sz * sz));

        int ex = camX - *(int*)(e + 0x48);
        int ez = camZ - *(short*)(e + 0x9a);
        int toEnd = (int)Winsys3d.Ordinal_442((uint)(ex * ex + ez * ez));

        bool homeIsNearer = homeIsStart ? toStart <= toEnd : toEnd <= toStart;
        bool takeStart = homeIsStart == homeIsNearer;

        if (takeStart)
        {
            *(int*)(e + 0x1c) = *(int*)(e + 0x44) << 0xc;
            *(int*)(e + 0x24) = *(int*)(e + 0x4c) << 0xc;
        }
        else
        {
            *(int*)(e + 0x1c) = *(int*)(e + 0x48) << 0xc;
            *(int*)(e + 0x24) = *(short*)(e + 0x9a) << 0xc;
        }

        if (homeIsNearer) return;

        *(short*)(e + 0x10) = homeIsStart ? (short)2 : (short)8;
        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
    }

    /// <summary>
    /// La disparition du wagonnet, etats 6 et 7 : les trois echelles perdent 200 par image, et
    /// quand la premiere passe a zero ou au-dessous les trois sont calees a zero et l'etat passe a
    /// <paramref name="stateWhenGone"/>.
    /// </summary>
    private static void RailCartFade(byte* e, short stateWhenGone)
    {
        *(short*)(e + 0x28) -= 200;
        short scale = *(short*)(e + 0x28);
        *(short*)(e + 0x2a) -= 200;
        *(short*)(e + 0x2c) -= 200;

        if (scale > 0) return;

        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x10) = stateWhenGone;
    }

    /// <summary>
    /// Le corps commun de <c>0x004538C0</c> (sous-zones 5 a 8) et <c>0x00455570</c> (sous-zones 3,
    /// 4 et 9), <b>identiques a l'octet pres</b> — 0x511 octets, seuls les quatre deplacements
    /// relatifs des appels different, et ils resolvent sur les memes cibles. Toutes deux
    /// decompilees dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Les trois echelles reviennent vers <c>0x1000</c> par huitiemes</b> a chaque image, sauf
    /// pendant la disparition (etats 6 et 7). C'est la premiere chose que fait le corps.
    /// </para>
    /// <para>
    /// <b>A la naissance, il joue toute sa course d'avance</b>, dans une boucle serree qui appelle
    /// <see cref="RailCartStep"/> jusqu'a l'arret. La position d'arrivee est notee en
    /// <c>+0x48</c>/<c>+0x9a</c>, la position de depart etant deja en <c>+0x44</c>/<c>+0x4c</c>, et
    /// l'entite est remise ou elle etait. C'est la seule chose que cette boucle produit : deux
    /// bornes. Elle laisse derriere elle <c>+0x12</c> et <c>+0x5c</c> en l'etat, et l'etat 8.
    /// </para>
    /// <para>
    /// <b>Le groupe commande le reste.</b> <see cref="CountPartyMembersMatching"/> avec
    /// <c>0x10</c> donne combien de membres sont rattaches au wagonnet ; tant que ce nombre n'est
    /// pas celui des membres actifs, le wagonnet reste a l'arret et
    /// <see cref="RailCartSnapToAnchor"/> le pose a l'extremite la plus proche de la camera. Des
    /// que tout le monde est a bord, l'etat 8 devient 1 (aller) et l'etat 2 devient 3 (retour).
    /// </para>
    /// <para>
    /// <b>Pendant la course</b>, le meme comptage avec <c>0x20</c> — les membres <i>en contact</i>
    /// — surveille l'obstacle : s'il y en a, mais pas tout le groupe, la course s'interrompt en
    /// disparition (etat 6 a l'aller, 7 au retour). Les etats 4 et 5 sont les arrivees : elles
    /// attendent que le groupe se detache entierement pour rendre l'etat 2 ou 8.
    /// </para>
    /// <para>
    /// <b>La hauteur 600 gele le repli</b> : quand <c>+0x20 &amp; 0xfffff000</c> vaut
    /// <c>0x258000</c>, les deux branches de detachement rendent la main sans rien faire.
    /// </para>
    /// </summary>
    private static void TickRailCart(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state != 6 && state != 7)
        {
            *(short*)(e + 0x28) += (short)((0x1000 - *(short*)(e + 0x28)) >> 3);
            *(short*)(e + 0x2a) += (short)((0x1000 - *(short*)(e + 0x2a)) >> 3);
            *(short*)(e + 0x2c) += (short)((0x1000 - *(short*)(e + 0x2c)) >> 3);
        }

        if (state == 0)
        {
            int homeX = *(int*)(e + 0x1c);
            int homeY = *(int*)(e + 0x20);
            int homeZ = *(int*)(e + 0x24);

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x44) = homeX >> 0xc;
            *(int*)(e + 0x48) = homeY >> 0xc;
            *(int*)(e + 0x4c) = homeZ >> 0xc;

            do
            {
                if (RailCartStep(e, true, true)) *(short*)(e + 0x10) = 8;
            }
            while (*(short*)(e + 0x10) == 0);

            *(int*)(e + 0x48) = *(int*)(e + 0x1c) >> 0xc;
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x24) >> 0xc);

            *(int*)(e + 0x1c) = homeX;
            *(int*)(e + 0x20) = homeY;
            *(int*)(e + 0x24) = homeZ;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (ActivePartySlotCount_00466D70() != aboard && *(short*)(e + 0x10) == 8)
        {
            if ((*(uint*)(e + 0x20) & 0xfffff000) != 0x258000) RailCartSnapToAnchor(e, true);
            return;
        }

        if (ActivePartySlotCount_00466D70() != aboard && *(short*)(e + 0x10) == 2)
        {
            if ((*(uint*)(e + 0x20) & 0xfffff000) != 0x258000) RailCartSnapToAnchor(e, false);
            return;
        }

        if (ActivePartySlotCount_00466D70() == aboard && *(short*)(e + 0x10) == 8)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (RailCartStep(e, true, false)) *(short*)(e + 0x10) = 4;

            int blocked = CountPartyMembersMatching(e, 0x20, 0x40000000);
            if (ActivePartySlotCount_00466D70() != blocked && blocked != 0)
            {
                *(short*)(e + 0x10) = 6;
                *(int*)(e + 0x5c) = 0;
            }
        }

        if (ActivePartySlotCount_00466D70() == aboard && *(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            if (RailCartStep(e, false, false)) *(short*)(e + 0x10) = 5;

            int blocked = CountPartyMembersMatching(e, 0x20, 0x40000000);
            if (ActivePartySlotCount_00466D70() != blocked && blocked != 0)
            {
                *(short*)(e + 0x10) = 7;
                *(int*)(e + 0x5c) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 4 && aboard == 0) *(short*)(e + 0x10) = 2;
        if (*(short*)(e + 0x10) == 5 && aboard == 0) *(short*)(e + 0x10) = 8;

        if (*(short*)(e + 0x10) == 6 && *(int*)(e + 0x5c) == 0) RailCartFade(e, 8);
        if (*(short*)(e + 0x10) == 7 && *(int*)(e + 0x5c) == 0) RailCartFade(e, 2);
    }

    /// <summary>Ghidra: FUN_004538c0 @ 0x004538c0 — sous-type <b>44</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Voir <see cref="TickRailCart"/>.</summary>
    private static void EntityHandler_004538C0(byte* e) => TickRailCart(e);

    /// <summary>Ghidra: FUN_00455570 @ 0x00455570 — sous-type <b>44</b> du bloc
    /// <c>0x005546a8</c>, sous-zones 3, 4 et 9 ; <b>la copie conforme</b> de
    /// <see cref="EntityHandler_004538C0"/>, octet pour octet. Voir
    /// <see cref="TickRailCart"/>.</summary>
    private static void EntityHandler_00455570(byte* e) => TickRailCart(e);

    // =====================================================================
    // LA TREMIE QUI MENE A LA SOUS-ZONE SUIVANTE — creneau 37
    //
    // 0x00454360 (sous-zones 5 a 8) et 0x00455EB0 (sous-zones 3, 4 et 9) sont deux variantes d'un
    // meme dessin : un bloc pousse par le groupe, qui racle quand il bouge, et qui, une fois
    // tombe sous -0x258000, EFFACE SON ENTITE ET CHANGE DE CARTE — le compteur 0x005e96fa donne la
    // sous-zone suivante et sert deux fois, une fois pour nommer la carte et une fois comme numero
    // de script.
    // =====================================================================

    /// <summary>
    /// Le passage a la carte suivante : l'entite s'eteint (bit de signe efface), le titre de la
    /// carte numero <paramref name="slot"/> est lie par <see cref="BindMenuLabels"/> depuis la
    /// table <c>+0x70</c> du repertoire de zone, le script du meme numero part sur l'entite, et le
    /// compteur <c>0x005e96fa</c> avance d'un.
    /// </summary>
    private static void EnterNextSubArea(byte* e, int slot)
    {
        *(uint*)e &= 0x7fffffff;

        byte* titles = *(byte**)(s_Unknown_005e97cc + 0x70);
        BindMenuLabels(*(byte**)(titles + 4 + slot * 4), *(byte**)(e + 0x6c),
                       s_WorldMapDescriptor_005633b0, 0);                      // Ghidra: FUN_00544350

        RunEntityScript(s_Unknown_005e97cc, e, (uint)slot);
        Mem.Arena(0x005e96fa)[0] += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00454360 @ 0x00454360 — <b>la tremie des sous-zones 5 a 8</b>, sous-type
    /// <b>37</b> du bloc <c>0x00554028</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Le bloc est deplace par <see cref="MoveSceneEntity_00455B30"/> avec le masque
    /// <c>0xa800</c>, racle par <see cref="ScrapeSoundTick"/>, et <b>des qu'il passe sous
    /// <c>-0x258000</c> il emmene le groupe ailleurs</b> : le numero de carte est
    /// <c>0x005e96fa &amp; 0x0f</c>, <b>sans aucune borne</b> — la table <c>+0x70</c> est lue a cet
    /// index tel quel.
    /// </para>
    /// <para>Les trois vitesses sont remises a zero en sortant, a chaque image.</para>
    /// </summary>
    private static void EntityHandler_00454360(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x12) = 0;

        MoveSceneEntity_00455B30(e, 0xa800);
        ScrapeSoundTick(e);

        if (*(int*)(e + 0x20) < -0x258000)
        {
            EnterNextSubArea(e, Mem.Arena(0x005e96fa)[0] & 0x0f);
        }

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00455eb0</c> — <b>la tremie des sous-zones 3, 4 et 9</b>, sous-type <b>37</b>
    /// du bloc <c>0x005546a8</c>. Absente de <c>Blaze.c</c> ; 112 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00455EB0.asm</c>.
    ///
    /// <para>
    /// La variante prudente de <see cref="EntityHandler_00454360"/>. Le masque de deplacement est
    /// <c>0x800</c> et non <c>0xa800</c>, et le passage a la carte suivante est garde par
    /// <b>trois</b> conditions de plus :
    /// </para>
    /// <list type="bullet">
    ///   <item><b>aucun membre actif ne doit etre sous <c>0x12c000</c></b> — le premier qui l'est
    ///     annule le passage pour cette image ;</item>
    ///   <item>le bit 0 de <see cref="s_Unknown_00563c77"/> — le meme drapeau que rend le verbe de
    ///     zone <c>0x00442e10</c> — doit etre efface ;</item>
    ///   <item>et le numero de carte est <b>borne a 4</b> — <c>0x005e96fa &amp; 0x0f</c>, ramene a
    ///     zero s'il est nul ou negatif et a 4 s'il l'atteint.</item>
    /// </list>
    /// <para>
    /// <b>Elle a aussi un plancher</b>, que sa sœur n'a pas : sous <c>-0x320000</c> la hauteur est
    /// calee la et les bits <c>0x6000</c> sont poses. Le bloc s'arrete donc de tomber meme quand
    /// le changement de carte n'a pas eu lieu.
    /// </para>
    /// </summary>
    private static void EntityHandler_00455EB0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x12) = 0;

        MoveSceneEntity_00455B30(e, 0x800);
        ScrapeSoundTick(e);

        if (*(int*)(e + 0x20) < -0x258000 && PartyIsAboveNextSubAreaFloor() && (s_Unknown_00563c77 & 1) == 0)
        {
            int slot = Mem.Arena(0x005e96fa)[0] & 0x0f;
            if ((short)slot <= 0) slot = 0;
            if ((short)slot >= 4) slot = 4;

            EnterNextSubArea(e, slot);
        }

        if (*(int*)(e + 0x20) < -0x320000)
        {
            *(int*)(e + 0x20) = -0x320000;
            *(uint*)e |= 0x6000;
        }

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>Vrai tant qu'aucun emplacement actif du groupe n'est sous la hauteur
    /// <c>0x12c000</c> — la garde de <see cref="EntityHandler_00455EB0"/> avant un changement de
    /// carte.</summary>
    private static bool PartyIsAboveNextSubAreaFloor()
    {
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;
            if (*(int*)(member + 0x20) < 0x12c000) return false;
        }

        return true;
    }

    // =====================================================================
    // QUELQUE CHOSE SAISIT UN PERSONNAGE
    //
    // Trois corps de meme dessin, un par bloc de sous-zones : 0x00451460 (creneau 47, sous-zones
    // 0 a 2), 0x00451630 (creneau 35, memes sous-zones) et 0x00452E40 (creneau 36, sous-zones 5
    // a 8). Ils prennent UN membre du groupe, le mettent dans l'animation 6, GELENT LE MONDE
    // ENTIER le temps que cette animation se joue jusqu'a sa derniere image, puis le degelent en
    // posant le bit 0x2000000 sur la victime et en crachant une bouffee d'effets.
    //
    // Ils ne different que par trois choses : QUI est la victime, la hauteur du son, et la forme
    // de la bouffee. Le premier est le plus interessant — voir EntityHandler_00451460.
    // =====================================================================

    /// <summary>
    /// La fiche d'animation que la victime va jouer : son <c>+0x18</c> passe a <b>6</b>, et
    /// l'entree numero 6 du paquet d'animations de son personnage est rendue. Le paquet est
    /// <c>0x005e9680</c> indexe par l'octet <c>+4</c> de la victime (son numero d'apparence), et
    /// l'entree <c>n</c> y est au mot <c>n</c> — d'ou le <c>+0x18</c> du binaire.
    /// </summary>
    private static byte* SeizeVictimAnimation(byte* victim)
    {
        *(short*)(victim + 0x18) = 6;
        byte* bundle = *(byte**)(s_PartyAnimationResult_005e9680 + victim[4] * 4);
        return *(byte**)(bundle + 6 * 4);
    }

    /// <summary>
    /// L'entree en saisie, etat 1 des trois corps : bit <c>0x2000</c>, etat 2, la victime remise
    /// a l'image zero de l'animation 6, <see cref="FreezeAllEntities"/>, et le bit
    /// <c>0x4000000</c> <b>retire de l'entite elle-meme</b> — le gel vient d'etre pose sur tout le
    /// monde, y compris sur elle, et elle s'en exempte aussitot pour continuer a tourner.
    /// </summary>
    private static void SeizeFreezeWorld(byte* e, byte* victim)
    {
        *(uint*)e |= 0x2000;
        *(short*)(e + 0x10) = 2;

        *(short*)(victim + 0x18) = 6;
        *(short*)(victim + 0x1a) = 0;

        FreezeAllEntities();                 // Ghidra: FUN_00535300
        *(uint*)e &= 0xfbffffff;
    }

    /// <summary>
    /// L'attente de la derniere image, etat 2 des trois corps : l'image <c>+0x1a</c> de la victime
    /// avance tant qu'il en reste une apres elle (<c>+0x16</c> de la fiche d'animation porte le
    /// compte, <b>lu sans signe</b>). A la derniere : le son <b>8</b> — un numero litteral, pas un
    /// son de zone —, le bit <c>0x2000000</c> sur la victime,
    /// <see cref="UnfreezeAllEntities"/>, etat 3 et compteur a zero.
    /// </summary>
    private static void SeizeAdvanceFrame(byte* e, byte* victim, byte* animEntry, short pitch)
    {
        short frame = *(short*)(victim + 0x1a);
        if (frame + 1 < *(ushort*)(animEntry + 0x16))
        {
            *(short*)(victim + 0x1a) = (short)(frame + 1);
            return;
        }

        TryPlaySoundNearEntity((uint*)e, 8, pitch, 0x1000, 0, 0);
        *(uint*)victim |= 0x2000000;
        UnfreezeAllEntities();               // Ghidra: FUN_00535400

        *(short*)(e + 0x10) = 3;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// La bouffee, etat 3 de <see cref="EntityHandler_00451460"/> et de
    /// <see cref="EntityHandler_00451630"/> : bit <c>0x4000</c>, le compteur avance, et les effets
    /// naissent <b>a <paramref name="lift"/> au-dessus de l'entite</b> — la hauteur est decalee le
    /// temps des appels puis remise. Cinq images, puis l'etat de repos <c>0x63</c>.
    /// </summary>
    private static void SeizePuff(byte* e, int lift, int count36, int count31)
    {
        int home = *(int*)(e + 0x20);

        *(uint*)e |= 0x4000;
        *(short*)(e + 0x12) += 1;
        *(int*)(e + 0x20) = home + lift;

        for (int i = 0; i < count36; i++) SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);
        for (int i = 0; i < count31; i++) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);

        if (*(short*)(e + 0x12) == 5) *(short*)(e + 0x10) = 0x63;

        *(int*)(e + 0x20) = home;
    }

    /// <summary>
    /// Ghidra: <c>0x00451460</c> — <b>ce qui n'attrape que le Nain</b>, sous-type <b>47</b> du
    /// bloc <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 149 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00451460.asm</c>.
    ///
    /// <para>
    /// <b>Le declenchement lit la classe du personnage qui la touche</b>, et c'est la seule chose
    /// de ce genre dans la mine. Au contact (bit <c>0x40</c> du mot d'etat) et au repos, elle prend
    /// le membre en <c>+0x6c</c>, remonte a son enregistrement de personnage — <c>MainMem</c> par
    /// l'emplacement <c>0x006087b4</c>, plus <c>0xf0150 + emplacement * 0x2000</c> — et lit
    /// l'octet d'identite. Ses <b>trois bits bas sont la classe</b> (le quatrieme est le sexe), et
    /// elle ne reagit qu'a la valeur <b>6</b> : le <b>Nain</b>. Dans une mine abandonnee, c'est
    /// difficilement un hasard.
    /// </para>
    /// <para>
    /// Le membre doit en outre ne pas porter <c>0x10000</c>. Les deux conditions reunies, le
    /// libelle <c>0x4a8</c> de la zone est affiche par <see cref="BindMenuLabels"/>,
    /// <c>0x005e99c8</c> passe a <c>0x40</c> — le meme couple que les autres panneaux de la carte
    /// du monde — et l'etat passe a 1. <b>Elle rend alors la main</b> : la saisie commence a
    /// l'image suivante.
    /// </para>
    /// <para>
    /// La suite est la saisie commune, avec le son a la hauteur <c>0x300</c>, une bouffee de
    /// <b>neuf</b> effets <c>0x36</c> et <b>deux</b> <c>0x31</c> a <c>0x1e000</c> au-dessus, et un
    /// repos qui pose <c>0x6000</c>.
    /// </para>
    /// <para>
    /// <b>Le garde <c>si le membre est nul</c> du binaire est mort</b> : <c>+0x6c</c> est relu deux
    /// fois, la premiere avec ce garde et la seconde sans. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451460(byte* e)
    {
        if ((*(uint*)e & 0x40) != 0 && *(short*)(e + 0x10) == 0)
        {
            byte* toucher = *(byte**)(e + 0x6c);
            if (toucher == null) toucher = Res.s_EntityList_005de558;

            byte* identity = GetMainMemPtr() + 0xf0150 + (toucher[8] << 0xd);
            if ((*identity & 7) != 6) return;
            if ((*(uint*)toucher & 0x10000) != 0) return;

            byte* labels = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x4a8);
            BindMenuLabels(labels, toucher, s_WorldMapDescriptor_005633b0, 0);   // FUN_00544350
            *(uint*)Mem.Arena(0x005e99c8) = 0x40;

            *(short*)(e + 0x10) = 1;
            return;
        }

        byte* victim = null;
        byte* animEntry = null;

        short state = *(short*)(e + 0x10);
        if (state == 1 || state == 2)
        {
            victim = *(byte**)(e + 0x6c);
            animEntry = SeizeVictimAnimation(victim);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SeizeFreezeWorld(e, *(byte**)(e + 0x6c));
            return;
        }

        if (*(short*)(e + 0x10) == 2) SeizeAdvanceFrame(e, victim, animEntry, 0x300);
        if (*(short*)(e + 0x10) == 3) SeizePuff(e, 0x1e000, 9, 2);
        if (*(short*)(e + 0x10) == 0x63) *(uint*)e |= 0x6000;
    }

    /// <summary>
    /// Ghidra: <c>0x00451630</c> — <b>la saisie adressee par numero</b>, sous-type <b>35</b> du
    /// bloc <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 118 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00451630.asm</c>.
    ///
    /// <para>
    /// La meme saisie que <see cref="EntityHandler_00451460"/>, mais <b>sans declenchement</b> :
    /// elle n'a pas d'etat 0, c'est le script de la salle qui la met a l'etat 1. Et la victime
    /// n'est pas celle qui l'a touchee, c'est <b>le membre dont <c>+0x14</c> porte le
    /// numero</b>.
    /// </para>
    /// <para>
    /// Son a la hauteur <c>0x400</c>, bouffee de <b>cinq</b> effets <c>0x36</c> et <b>un</b>
    /// <c>0x31</c> a <c>0x3c000</c> au-dessus, et un repos qui ne pose que <c>0x4000</c> — la
    /// seule des trois a ne pas poser aussi <c>0x2000</c>.
    /// </para>
    /// <para>
    /// <b>Le garde du binaire ne peut pas se declencher</b> : l'adresse de la victime est un
    /// <c>lea</c> sur une base fixe, donc jamais nulle, et le repli sur
    /// <c>s_EntityList_005de558</c> est mort. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451630(byte* e)
    {
        byte* victim = null;
        byte* animEntry = null;

        short state = *(short*)(e + 0x10);
        if (state == 1 || state == 2)
        {
            victim = Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x14) * 0x9c;
            if (victim == null) victim = Res.s_EntityList_005de558;
            animEntry = SeizeVictimAnimation(victim);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SeizeFreezeWorld(e, Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x14) * 0x9c);
            return;
        }

        if (*(short*)(e + 0x10) == 2) SeizeAdvanceFrame(e, victim, animEntry, 0x400);
        if (*(short*)(e + 0x10) == 3) SeizePuff(e, 0x3c000, 5, 1);
        if (*(short*)(e + 0x10) == 0x63) *(uint*)e |= 0x4000;
    }

    /// <summary>
    /// Ghidra: FUN_00452e40 @ 0x00452e40 — <b>la saisie des sous-zones 5 a 8</b>, sous-type
    /// <b>36</b> du bloc <c>0x00554028</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme victime que <see cref="EntityHandler_00451630"/> — le membre numero <c>+0x14</c> — et
    /// meme saisie, mais <b>une autre fin</b> : l'etat 3 ne fait pas naitre des effets au-dessus
    /// d'elle, il en fait naitre <b>cinquante et un</b> (<c>0x33</c>) <b>tout autour</b>, chacun a
    /// une position tiree au sort dans une boite de 200 x 100 x 100, et seulement aux images
    /// <b>1 et 4</b> du compteur. Quatre images en tout, puis l'etat 99.
    /// </para>
    /// <para>
    /// Les etats 3 et 99 posent l'un et l'autre <c>0x6000</c>. L'entite reste donc visible et
    /// figee sur place — rien dans ce corps ne la fait disparaitre.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452E40(byte* e)
    {
        byte* victim = null;
        byte* animEntry = null;

        short state = *(short*)(e + 0x10);
        if (state == 1 || state == 2)
        {
            victim = Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x14) * 0x9c;
            animEntry = SeizeVictimAnimation(victim);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SeizeFreezeWorld(e, Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x14) * 0x9c);
            return;
        }

        if (*(short*)(e + 0x10) == 2) SeizeAdvanceFrame(e, victim, animEntry, 0x400);

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x12) += 1;
            *(uint*)e |= 0x6000;

            if (*(short*)(e + 0x12) == 1 || *(short*)(e + 0x12) == 4)
            {
                for (int i = 0; i < 0x33; i++)
                {
                    int homeX = *(int*)(e + 0x1c);
                    int homeY = *(int*)(e + 0x20);
                    int homeZ = *(int*)(e + 0x24);

                    *(int*)(e + 0x1c) += (CrtRand() % 200 - 100) * 0x1000;
                    *(int*)(e + 0x20) += (CrtRand() % 100) * 0x1000;
                    *(int*)(e + 0x24) += (CrtRand() % 100 - 0x32) * 0x1000;

                    SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);

                    *(int*)(e + 0x1c) = homeX;
                    *(int*)(e + 0x20) = homeY;
                    *(int*)(e + 0x24) = homeZ;
                }
            }

            if (*(short*)(e + 0x12) > 3) *(short*)(e + 0x10) = 99;
        }

        if (*(short*)(e + 0x10) == 99) *(uint*)e |= 0x6000;
    }

    // =====================================================================
    // LES PORTES A DEUX BATTANTS
    //
    // 0x00451790 (creneau 32) et 0x00453070 (creneau 21) ne bougent pas seules : chacune fait
    // tourner UNE ENTITE VOISINE en meme temps qu'elle, atteinte par un DEPLACEMENT RELATIF dans
    // sa propre table — `this + n * 0x9c`, ou n est un petit numero range dans un champ de
    // placement. C'est la seule construction de la mine qui adresse une autre entite de scene
    // autrement que par le groupe.
    // =====================================================================

    /// <summary>L'angle <c>+0x34</c> du second battant de <see cref="EntityHandler_00451790"/> :
    /// l'entite qui suit la sienne de <c>+0x16</c> rangs dans la meme table, au pas de
    /// <c>0x9c</c>.</summary>
    private static short* SecondLeafAngle(byte* e) => (short*)(e + *(short*)(e + 0x16) * 0x9c + 0x34);

    /// <summary>
    /// L'etat de manette du membre que <c>+0x14</c> designe, <b>lu en double mot</b> : la meme
    /// triple indirection que <see cref="RaftPaddleBits"/> en foret — l'octet <c>+8</c> de
    /// l'emplacement donne le personnage, le <c>* 0x4c</c> de
    /// <see cref="Input.s_PlayerSlots_005e9188"/> donne la manette, le <c>* 0x10</c> donne sa
    /// ligne — mais le mot entier plutot que son octet de poids fort.
    /// </summary>
    private static uint DoorHolderPadState(byte* e)
    {
        byte* member = Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x14) * 0x9c;
        int pad = Inp.s_PlayerSlots_005e9188[member[8] * 0x4c];
        return *(uint*)(Inp.s_InputStateBase_00604a20 + pad * 0x10);
    }

    /// <summary>
    /// Ghidra: <c>0x00451790</c> — <b>la porte qu'on tient ouverte</b>, sous-type <b>32</b> du
    /// bloc <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 90 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00451790.asm</c>.
    ///
    /// <para>
    /// <b>Au repos (etat <c>0x63</c>)</b> les deux battants glissent vers l'angle <c>0x320</c> par
    /// quarts — une fermeture molle qui ne finit jamais tout a fait. L'etat 0 ne fait que basculer
    /// au repos, donc la porte naît fermee.
    /// </para>
    /// <para>
    /// <b>Etat 1, l'ouverture</b> : dix images ou le premier battant perd 100 et le second 75, avec
    /// une butee — le premier ne descend pas sous <c>-200</c>, et quand il y arrive le second est
    /// cale a <c>50</c>. <b>Etat 2, l'attente</b> : elle reste ouverte tant que la manette du membre
    /// <c>+0x14</c> porte l'un des bits <c>0xa000</c> ; des qu'ils retombent, <b>etat 3</b>, dix
    /// images de retour a la meme cadence, et le repos.
    /// </para>
    /// <para>
    /// Deux <c>lea</c> du binaire calculent l'adresse du second battant sans s'en servir ; c'est un
    /// reste de compilation, il n'y a rien a transcrire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451790(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 0x63;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x34) -= 100;
            *SecondLeafAngle(e) -= 75;

            if (*(short*)(e + 0x34) < -200)
            {
                *(short*)(e + 0x34) = -200;
                *SecondLeafAngle(e) = 50;
            }

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 0x0a)
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
            *(short*)(e + 0x34) += 100;
            *SecondLeafAngle(e) += 75;

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 0x0a)
            {
                *(short*)(e + 0x10) = 0x63;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 0x63) return;

        *(short*)(e + 0x34) += (short)((0x320 - *(short*)(e + 0x34)) >> 2);

        short* leaf = SecondLeafAngle(e);
        *leaf += (short)((0x320 - *leaf) >> 2);
    }

    /// <summary>
    /// Ghidra: <c>0x00453070</c> — <b>la porte que le groupe entier doit franchir</b>, sous-type
    /// <b>21</b> du bloc <c>0x00554028</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; 161
    /// instructions, <c>Legacy/Raw/Blaze/FUN_00453070.asm</c>.
    ///
    /// <para>
    /// Elle compte trois choses par image : combien d'emplacements sont <b>utilisables</b>
    /// (actifs et sans le bit <c>0x10000</c>), combien de membres lui sont <b>rattaches</b>
    /// (<c>0x10</c>) et combien la <b>touchent</b> (<c>0x20</c>). Le sous-etat <c>+0x14</c>
    /// progresse de 0 a 3 au fil de ces conditions : <b>1</b> des que le groupe entier est
    /// rattache, <b>2</b> quand ils la touchent tous sans qu'aucun ne soit reste de l'autre cote,
    /// <b>3</b> a l'ouverture, avec le son <c>0x28</c> de la zone.
    /// </para>
    /// <para>
    /// <b>A l'ouverture, elle pousse sa voisine avec elle</b> : l'entite qui la suit de
    /// <c>+0x16</c> rangs voit son etat passer a 2 et son <c>+0x14</c> a 3, si bien que les deux
    /// battants tournent ensemble. L'angle <c>+0x34</c> monte alors de 100 par image jusqu'a 300
    /// (etat 6), puis jusqu'a <c>0x400</c>, ou le sous-etat retombe a 0.
    /// </para>
    /// <para>
    /// <b>L'etat 3 est la refermeture</b>, et elle est commandee par la camera : tant que la
    /// hauteur visee <c>0x005de06c</c> est au-dessus de la porte, l'angle redescend de 100 par
    /// image jusqu'a zero, puis l'etat revient a 1. Les etats 4 et 5 sont une temporisation de
    /// quinze images qui ramene a l'etat 0.
    /// </para>
    /// <para>
    /// <c>+0x16</c> recoit le numero de la voisine a la naissance, depuis <c>+0x14</c> — le meme
    /// detournement d'un champ de placement que chez <see cref="EntityHandler_00451790"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453070(byte* e)
    {
        short usable = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50010000) != 0) continue;
            usable++;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x16) = *(short*)(e + 0x14);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (aboard == ActivePartySlotCount_00466D70() && aboard != 0)
        {
            if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
            if (*(short*)(e + 0x14) == 0) *(short*)(e + 0x14) = 1;

            if (*(short*)(e + 0x14) == 2)
            {
                *(short*)(e + 0x14) = 3;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x28), 0x400, 0x1000, 0, 0);
            }
        }

        int touching = CountPartyMembersMatching(e, 0x20, 0x40000000);

        if (aboard <= ActivePartySlotCount_00466D70() - usable
            && touching >= usable
            && touching != 0
            && *(short*)(e + 0x14) == 1)
        {
            *(short*)(e + 0x14) = 2;
        }

        short state = *(short*)(e + 0x10);
        if (state != 2) *(short*)(e + 0x14) = 0;

        if (state == 2 && *(short*)(e + 0x14) == 3 && *(short*)(e + 0x34) == 0)
        {
            byte* neighbour = e + *(short*)(e + 0x16) * 0x9c;
            *(short*)(neighbour + 0x10) = 2;
            *(short*)(neighbour + 0x14) = 3;
        }

        if (*(short*)(e + 0x10) == 2 && *(short*)(e + 0x14) == 3)
        {
            *(short*)(e + 0x34) += 100;
            if (*(short*)(e + 0x34) > 0x12c)
            {
                *(short*)(e + 0x10) = 6;
                *(short*)(e + 0x34) = 0x400;
            }
        }

        state = *(short*)(e + 0x10);

        if (state == 6 && *(short*)(e + 0x14) == 3)
        {
            *(short*)(e + 0x34) += 100;
            if (*(short*)(e + 0x34) > 0x400)
            {
                *(short*)(e + 0x34) = 0x400;
                *(short*)(e + 0x14) = 0;
            }
        }

        if (state == 3 && *Mem.Camera(0x005de06c) > *(int*)(e + 0x20))
        {
            *(short*)(e + 0x34) -= 100;
            if (*(short*)(e + 0x34) < 0)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 1;
            }
        }

        if (*(short*)(e + 0x10) == 4)
        {
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x10) = 5;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 5)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 0x0f)
            {
                *(short*)(e + 0x10) = 0;
                *(short*)(e + 0x12) = 0;
            }
        }
    }

    // =====================================================================
    // LA CATAPULTE — creneau 20 du bloc 0x00554028
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004533a0</c> — <b>la catapulte</b>, sous-type <b>20</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; 205 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_004533A0.asm</c>.
    ///
    /// <para>
    /// <b>Elle envoie le groupe en l'air.</b> Quand tout le monde lui est rattache, elle
    /// s'enfonce de <c>0x2000</c> par image pendant vingt images (etat 1), remonte de
    /// <c>0x3c000</c> par image pendant cinq (etat 2), et au bout de cette detente elle ecrit
    /// <c>+0x54</c> — la composante verticale de la vitesse — avec la <b>puissance rangee en
    /// <c>+0x16</c></b>, puis lance le script 0. Elle retombe ensuite (etat 3) jusqu'a la hauteur
    /// notee au depart, et l'etat 4 attend que le groupe soit revenu.
    /// </para>
    /// <para>
    /// <b>Deux voisines, pas une.</b> Celle de <c>+0x5c</c> — le numero arrive par <c>+0x16</c> a
    /// l'armement — voit son echelle <c>+0x2a</c> suivre le mouvement : elle s'ecrase avec la
    /// detente et se retend a <c>0x1000</c> a l'arrivee. Celle de <c>+0x44</c> — dont le numero
    /// arrive par le champ de placement <c>+0x30</c>, consomme des la premiere image — recoit
    /// l'etat 4 et, a <c>+0xac</c>, la valeur 4 au moment du tir.
    /// </para>
    /// <para>
    /// <b>Elle coupe les manettes</b> : les quatre octets <c>0x005e918a</c> au pas de <c>0x4c</c>
    /// sont remis a zero a chaque image des etats 1, 2 et 3. Le groupe ne reprend la main qu'a
    /// l'atterrissage.
    /// </para>
    /// <para>
    /// <b>Groupe absent au moment du tir</b> : la puissance retombe a la moitie de <c>+0x16</c>,
    /// plancher a 50, et aucun script ne part. <c>+0x9a</c> recoit la puissance <b>divisee par
    /// sept</b> — le binaire l'ecrit en multipliant par <c>0x92492493</c>, la recette habituelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_004533A0(byte* e)
    {
        if (*(short*)(e + 0x30) != 0)
        {
            *(int*)(e + 0x44) = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (aboard != 0 && aboard == ActivePartySlotCount_00466D70() && *(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x5c) = *(short*)(e + 0x16);
            *(short*)(e + 0x16) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x9a) = 0;
        }

        if (*(short*)(e + 0x10) == 4)
        {
            e[7] &= 0xef;
            if (aboard == ActivePartySlotCount_00466D70())
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x20) -= 0x5000;
            *(uint*)e &= 0xffffdfff;
            *CatapultArmScale(e) -= 160;

            if ((*(int*)(e + 0x20) >> 0xc) < *(short*)(e + 0x14))
            {
                *(int*)(e + 0x20) = *(short*)(e + 0x14) << 0xc;
                *CatapultArmScale(e) = 0x1000;
                *(short*)(e + 0x10) = 4;
            }

            *(short*)(e + 0x9a) -= 1;
            if (*(short*)(e + 0x9a) < 0)
            {
                *(short*)(e + 0x9a) = 0;
                RunEntityScript(s_Unknown_005e97cc, e, 1);
            }

            SilencePads();
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x20) -= 0x2000;
            *CatapultArmScale(e) -= 100;

            if (*(short*)(e + 0x12) >= 0x14)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            SilencePads();
        }

        if (*(short*)(e + 0x10) != 2) return;

        SilencePads();

        *(short*)(e + 0x12) += 1;
        *(int*)(e + 0x20) += 0x3c000;
        *CatapultArmScale(e) += 0x834;

        if (*(short*)(e + 0x12) < 5) return;

        e[7] |= 0x10;
        *(uint*)e |= 0x2000;
        *(short*)(e + 0x10) = 3;
        *(short*)(e + 0x12) = 0;
        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x58) = 0;

        int riders = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (riders == ActivePartySlotCount_00466D70() && riders != 0)
        {
            *(int*)(e + 0x54) = *(short*)(e + 0x16) << 0xc;
            RunEntityScript(s_Unknown_005e97cc, e, 0);

            // Ghidra: `imul 92492493h ; add edx,ecx ; sar edx,2` — la division signee par 7.
            *(short*)(e + 0x9a) = (short)(*(short*)(e + 0x16) / 7);

            byte* partner = e + *(int*)(e + 0x44) * 0x9c;
            *(short*)(partner + 0x10) = 4;
            *(short*)(partner + 0xac) = 4;
        }
        else
        {
            int half = *(short*)(e + 0x16) / 2;
            if (half < 0x32) half = 0x32;
            *(int*)(e + 0x54) = half << 0xc;
        }

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x29), 0x400, 0x1000, 0, 0);
    }

    /// <summary>L'echelle <c>+0x2a</c> du bras de <see cref="EntityHandler_004533A0"/> : l'entite
    /// qui suit la sienne de <c>+0x5c</c> rangs.</summary>
    private static short* CatapultArmScale(byte* e) => (short*)(e + *(int*)(e + 0x5c) * 0x9c + 0x2a);

    /// <summary>
    /// Coupe les quatre manettes : l'octet <c>0x005e918a</c> et les trois suivants au pas de
    /// <c>0x4c</c> sont remis a zero. Le binaire ecrit la boucle en comparant l'adresse a
    /// <c>0x005e926e</c>, ce qui fait bien quatre tours.
    /// </summary>
    private static void SilencePads()
    {
        byte* slot = Inp.s_PlayerSlots_005e9188 + 2;
        for (int i = 0; i < 4; i++, slot += 0x4c) *slot = 0;
    }

    // =====================================================================
    // LE BATTANT QUI PIVOTE AU SIGNAL — creneau 12 du bloc 0x005546a8
    //
    // Deux moities symetriques dans un seul corps, commandees par les deux bits bas de
    // DAT_00571570 : le bit 1 fait faire un quart de tour, le bit 2 le defait. Chaque moitie
    // consomme son bit en arrivant a l'etat 2, si bien que le meme corps sert d'aller et de
    // retour. Les deux bits ensemble a zero remettent l'etat a 0.
    // =====================================================================

    /// <summary>
    /// La gerbe d'etincelles du battant : elle ne nait pas sur l'entite mais <b>au bout de son
    /// bras</b> — a <c>+0x14 * 800</c> unites d'angle du cap courant, moins 400, et a 230 unites
    /// de rayon. La position est decalee le temps des <paramref name="count"/> effets <c>0x31</c>,
    /// puis remise.
    /// </summary>
    private static void PivotSparks(byte* e, int count)
    {
        int homeX = *(int*)(e + 0x1c), homeY = *(int*)(e + 0x20), homeZ = *(int*)(e + 0x24);

        int angle = *(short*)(e + 0x14) * 800 + *(short*)(e + 0x32) - 0x190;
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(angle) * 230;   // Ordinal_451
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(angle) * 230;        // Ordinal_450

        for (int i = 0; i < count; i++) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);

        *(int*)(e + 0x1c) = homeX;
        *(int*)(e + 0x20) = homeY;
        *(int*)(e + 0x24) = homeZ;
    }

    /// <summary>
    /// Un quart de tour du battant : le cap <c>+0x32</c> avance de <c>+0x12</c> par image vers
    /// <c>+0x16 +- 0x200</c>, ou il est cale et l'etat passe a 2. <paramref name="forward"/> dit
    /// le sens, et <c>+0x14</c> de l'entite l'inverse — les deux moities du corps lisent ce champ
    /// en sens contraire l'une de l'autre.
    /// </summary>
    private static void PivotTurn(byte* e, bool forward)
    {
        if (forward)
        {
            *(short*)(e + 0x32) += *(short*)(e + 0x12);
            if (*(short*)(e + 0x32) < *(short*)(e + 0x16) + 0x200) return;
            *(short*)(e + 0x32) = (short)(*(short*)(e + 0x16) + 0x200);
        }
        else
        {
            *(short*)(e + 0x32) -= *(short*)(e + 0x12);
            if (*(short*)(e + 0x32) > *(short*)(e + 0x16) - 0x200) return;
            *(short*)(e + 0x32) = (short)(*(short*)(e + 0x16) - 0x200);
        }

        *(short*)(e + 0x10) = 2;
    }

    /// <summary>
    /// Ghidra: <c>0x00454ed0</c> — <b>le battant qui pivote au signal</b>, sous-type <b>12</b> du
    /// bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9. Absente de <c>Blaze.c</c> ; 222 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00454ED0.asm</c>.
    ///
    /// <para>
    /// <b>Les deux bits bas de <c>DAT_00571570</c> le commandent</b>, et les deux moities du corps
    /// sont l'image l'une de l'autre. Ni l'un ni l'autre pose : l'etat retombe a 0. Le <b>bit
    /// 1</b> : etat 1, cadence <c>+0x12</c> a <c>0x28</c> et croissante de <c>0x28</c> par image,
    /// le script 5 lance sans entite, onze etincelles des que la cadence depasse <c>0xa0</c>. Le
    /// <b>bit 2</b> fait le chemin inverse : cadence a <c>0xf0</c> et decroissante de <c>0x28</c>,
    /// vingt et une etincelles a l'unique image ou elle vaut exactement <c>0xa0</c>, et les sens
    /// de rotation echanges.
    /// </para>
    /// <para>
    /// <b>Chaque moitie consomme son bit</b> a l'etat 2, en l'effacant de <c>DAT_00571570</c> —
    /// c'est ce qui rend le corps reutilisable dans les deux sens sans autre memoire que le cap.
    /// </para>
    /// <para>
    /// Le quart de tour vaut <c>0x200</c> d'angle, soit un huitieme de tour complet ; <c>+0x16</c>
    /// garde le cap de depart, releve au moment ou le signal arrive.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454ED0(byte* e)
    {
        if ((*Mem.EntityState(0x00571570) & 3) == 0) *(short*)(e + 0x10) = 0;

        if ((*Mem.EntityState(0x00571570) & 1) != 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0x28;
                *(short*)(e + 0x16) = *(short*)(e + 0x32);
                RunEntityScript(s_Unknown_005e97cc, null, 5);
            }

            if (*(short*)(e + 0x10) == 2) *Mem.EntityState(0x00571570) &= ~1;

            if (*(short*)(e + 0x10) == 1)
            {
                *(short*)(e + 0x12) += 0x28;
                if (*(short*)(e + 0x12) > 0xa0) PivotSparks(e, 0x0b);

                if (*(short*)(e + 0x14) == 0) PivotTurn(e, true);
                if (*(short*)(e + 0x14) == 1) PivotTurn(e, false);
            }
        }

        if ((*Mem.EntityState(0x00571570) & 2) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0xf0;
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
        }

        if (*(short*)(e + 0x10) == 2) *Mem.EntityState(0x00571570) &= ~2;

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) -= 0x28;
        if (*(short*)(e + 0x12) == 0xa0) PivotSparks(e, 0x15);

        if (*(short*)(e + 0x14) == 0) PivotTurn(e, false);
        if (*(short*)(e + 0x14) == 1) PivotTurn(e, true);
    }

    /// <summary>
    /// Ghidra: FUN_004551d0 @ 0x004551d0 — <b>la nacelle qui traverse le gouffre</b>, sous-type
    /// <b>13</b> du bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle va se ranger du cote ou est le groupe.</b> Au repos, elle mesure la distance du
    /// point vise par la camera aux deux quais — <c>(0x10cc, 0x834)</c> et <c>(0x10cc, -500)</c> —
    /// et, <b>si elle est amarree a l'autre</b>, elle se teleporte au plus proche, a la hauteur
    /// <c>0x320000</c> et echelles a zero, d'ou la reapparition en grandissant juste apres. Le
    /// signe de sa propre coordonnee <c>z</c> dit a quel quai elle est.
    /// </para>
    /// <para>
    /// Ses trois echelles remontent ensuite a <c>0x1000</c> par pas de 400 — elle apparait en
    /// grandissant. <b>Quand le groupe entier lui est rattache</b>, elle note son sens de marche
    /// dans <c>+0x12</c> (<c>0x46</c> vers les z positifs, <c>-0x46</c> vers les negatifs), passe
    /// a l'etat 2 et pose le bit <b>1</b> de <c>DAT_00571570</c>.
    /// </para>
    /// <para>
    /// <b>Etat 2 puis 1, la traversee</b> : apres cinq images d'elan, le cap <c>+0x14</c> avance de
    /// <c>0x80</c> par image et la hauteur suit un sinus <b>redresse</b> — <c>|sin| * 32</c>
    /// au-dessus de <c>0x320000</c>, donc deux bosses par tour. Aux caps <c>0x800</c> et
    /// <c>0x1000</c>, un son et treize effets <c>0x30</c> plus trois <c>0x32</c>. A mi-course
    /// (<c>0x1194</c>) elle pose le bit <b>2</b> de <c>DAT_00571570</c>, une seule fois.
    /// </para>
    /// <para>
    /// <b>A l'arrivee</b> — au-dela d'un quai et dans le bon sens — elle se cale sur lui, pose
    /// <c>0x10</c> sur son octet 7 et <c>0x2000</c> sur son mot d'etat, se donne une vitesse de
    /// debarquement (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>), passe a l'etat 99 et
    /// <b>coupe les manettes</b>. L'etat 99 les tient coupees quinze images de plus, puis rend
    /// tout au repos.
    /// </para>
    /// </summary>
    private static void EntityHandler_004551D0(byte* e)
    {
        *(short*)(e + 0x9a) = 0x6000;

        if (*(short*)(e + 0x10) == 0)
        {
            e[7] &= 0xef;
            *(uint*)e &= 0xffffdfff;

            // Ghidra: `(int)(param_1[9] & 0xfffff000) < 1` — la comparaison est SIGNEE, et le
            // masque met le bit de signe en jeu : en non signe, le quai negatif passerait pour le
            // plus grand des deux et la nacelle n'irait jamais chercher le groupe.
            bool atNearDock = (int)(*(uint*)(e + 0x24) & 0xfffff000) < 1;

            int camX = *Mem.Camera(0x005de068) >> 0xc;
            int camZ = *Mem.Camera(0x005de070) >> 0xc;

            int nx = camX - 0x10cc, nz = camZ - 0x834;
            int toFar = (int)Winsys3d.Ordinal_442((uint)(nx * nx + nz * nz));

            int fx = camX - 0x10cc, fz = camZ + 500;
            int toNear = (int)Winsys3d.Ordinal_442((uint)(fx * fx + fz * fz));

            if (toFar < toNear && atNearDock) MoveFerryToDock(e, 0x834000);
            if (toNear < toFar && !atNearDock) MoveFerryToDock(e, -0x1f4000);   // 0xffe0c000
        }

        if (*(short*)(e + 0x28) < 0x1000)
        {
            short grown = (short)(*(short*)(e + 0x28) + 400);
            *(short*)(e + 0x2a) += 400;
            *(short*)(e + 0x2c) += 400;
            *(short*)(e + 0x28) = grown;

            if (grown > 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x2c) = 0x1000;
            }
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (aboard != 0 && aboard == ActivePartySlotCount_00466D70() && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = *(int*)(e + 0x24) < 1 ? (short)0x46 : (short)-0x46;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 0;
            *Mem.EntityState(0x00571570) |= 1;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x14) += 1;
            if (*(short*)(e + 0x14) > 4)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x16) = 0;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x18), 0x310, 0x1000, 0, 0);
            }
        }

        if (*(short*)(e + 0x10) == 99)
        {
            if (*(short*)(e + 0x12) == 0) SilencePads();

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 0x0e) *(short*)(e + 0x10) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x14) += 0x80;
        *(int*)(e + 0x24) += *(short*)(e + 0x12) * 0x1000;

        if (*(short*)(e + 0x14) > 0x1194 && *(short*)(e + 0x16) == 0)
        {
            *Mem.EntityState(0x00571570) |= 2;
            *(short*)(e + 0x16) = 1;
        }

        // La hauteur suit |sin| : le binaire calcule le sinus, teste le signe du produit, puis le
        // recalcule — deux appels pour une seule valeur, la table ne bougeant pas entre les deux.
        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) << 5;
        if (wave < 0) wave = -wave;
        *(int*)(e + 0x20) = wave + 0x320000;

        if (*(short*)(e + 0x14) == 0x800 || *(short*)(e + 0x14) == 0x1000)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x19), 0x400, 0x1000, 0, 0);
            for (int i = 0; i < 0x0d; i++) SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
            for (int i = 0; i < 3; i++) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
        }

        if ((int)(*(uint*)(e + 0x24) & 0xfffff000) < -0x1f4000 && *(short*)(e + 0x12) < 0)
        {
            FerryDock(e, -0x1f4000, 0, 0xa000, -0x46000);   // 0xffe0c000, 0xfffba000
        }

        if ((int)(*(uint*)(e + 0x24) & 0xfffff000) > 0x834000 && *(short*)(e + 0x12) > 0)
        {
            FerryDock(e, 0x834000, 0, 0xa000, 0x28000);
        }
    }

    /// <summary>Le placement au repos de <see cref="EntityHandler_004551D0"/> : les deux quais
    /// partagent le meme <c>x</c> et la meme hauteur, seul le <c>z</c> change, et les trois
    /// echelles repartent de zero.</summary>
    private static void MoveFerryToDock(byte* e, int z)
    {
        *(int*)(e + 0x1c) = 0x10cc000;
        *(int*)(e + 0x20) = 0x320000;
        *(int*)(e + 0x24) = z;
        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
    }

    /// <summary>L'accostage de <see cref="EntityHandler_004551D0"/> : la nacelle est calee sur le
    /// quai, rendue solide (<c>0x10</c> sur l'octet 7, <c>0x2000</c> sur le mot d'etat), lancee
    /// vers la berge par les trois composantes de vitesse, et les manettes sont coupees.</summary>
    private static void FerryDock(byte* e, int z, int vx, int vy, int vz)
    {
        e[7] |= 0x10;
        *(uint*)e |= 0x2000;
        *(int*)(e + 0x24) = z;

        *(short*)(e + 0x10) = 99;
        *(short*)(e + 0x12) = 0;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        SilencePads();
    }
}
