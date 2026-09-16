// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES GESTIONNAIRES D'ENTITE DE LA FORET — table 0x0056adc8, bloc 0x005553a8.
//
// Tout ce qui, dans la foret, n'est ni une bete ni un effet : l'embarcation qu'on prend a huit,
// l'ascenseur, les portes, les volumes qui prennent la camera, les coffres de capacite. Fichier
// recolle de DEUX plages (decision D7) - l'embarcation avait ete transcrite le 2026-09-01 au
// milieu des particules, le reste vit a la fin du fichier d'origine.
//
// IL PORTE AUSSI UN BANDEAU DE PROVENANCE, ET NON DE SUJET : « Les gestionnaires que Ghidra n'a
// pas decompiles » groupe six corps releves au desassembleur le meme jour (0x004508B0,
// 0x00473A70, 0x00450BD0, 0x004736D0, 0x004746C0, 0x00468110), qui n'ont rien d'autre en commun.
// UN SEUL le suit immediatement ; les cinq autres sont plus bas dans ce meme fichier, et deux
// corps qui le suivent (0x00456730, 0x00458170) ne figurent pas dans sa liste. Le decoupage les
// a tous reunis ici - c'est un hasard heureux, pas un classement.
//
// ET IL PORTE LA NOTE AU COLLEGUE. `BuildDeveloperNotice_00551814` compose une boite de dialogue
// en japonais, restee dans la version francaise vendue : « la porte a un battant n'est pas
// utilisable pour l'instant. Contactez Imai. » Voir Documentation/Etat/NOTE_DEVELOPPEUR.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // L'EMBARCATION DE LA FORET (2026-09-01) — 0x00458200, sous-zones 6 a 8, table 0x0056adc8.
    //
    // 2088 octets, ABSENTE de `Blaze.c`, quarante-huit etiquettes de saut et cinq `return`.
    // Releve garde sous `Legacy/Raw/Blaze/FUN_00458200.asm`.
    //
    // C'est le plus gros corps d'entite de scene transcrit jusqu'ici, et le premier que le groupe
    // PILOTE : les quatre membres pagayent, l'engin tourne et avance, la camera passe a son bord
    // tant que tout le monde est dessus, et sa position est reecrite dans la fiche de placement de
    // la sous-zone pour qu'il soit encore la au retour.
    // =====================================================================

    /// <summary>
    /// Le calage de l'embarcation sur le terrain, ecrit deux fois dans le binaire (a
    /// <c>0x004582c2</c> et a <c>0x0045834d</c>) avec les memes six lignes.
    ///
    /// <para>
    /// Les quatre demi-mots hauts de <c>+0x80</c>..<c>+0x8c</c> portent les <b>attributs de face</b>
    /// que <see cref="TryPlaceEntity"/> y ecrit (<c>+0x82 + genre * 4</c>) ; ils sont remis au motif
    /// <c>0xa000</c> — celui que le reste du moteur lit comme « aucune face » — avant l'appel, pour
    /// que les genres que le nouveau point ne rencontre pas ne gardent pas l'ancienne reponse.
    /// Plancher et plafond sont ramenes a la hauteur courante dans la foulee.
    /// </para>
    /// </summary>
    private static void SettleRaftOnTerrain(byte* e)
    {
        *(ushort*)(e + 0x82) = 0xa000;
        *(ushort*)(e + 0x86) = 0xa000;
        *(ushort*)(e + 0x8a) = 0xa000;
        *(ushort*)(e + 0x8e) = 0xa000;

        *(int*)(e + 0x64) = *(int*)(e + 0x20);
        *(int*)(e + 0x60) = *(int*)(e + 0x20);

        TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);
    }

    /// <summary>
    /// L'etat de manette du membre <paramref name="member"/>, par la meme triple indirection que
    /// <c>LeaderPadState</c> : l'octet <c>+8</c> de l'emplacement donne le personnage, le
    /// <c>* 0x4c</c> de <see cref="Input.s_PlayerSlots_005e9188"/> donne la manette, et le
    /// <c>* 0x10</c> donne sa ligne. Le binaire lit <b>l'octet <c>+3</c></b>, c'est-a-dire le haut
    /// du mot de fronts <c>+2</c> : les quatre gachettes y sont les bits <c>1</c>, <c>2</c>,
    /// <c>4</c> et <c>8</c>, soit <c>0x100</c>, <c>0x200</c>, <c>0x400</c> et <c>0x800</c> du mot.
    /// </summary>
    private static byte RaftPaddleBits(byte* member)
        => Inp.s_InputStateBase_00604a20[Inp.s_PlayerSlots_005e9188[member[8] * 0x4c] * 0x10 + 3];

    /// <summary>
    /// Ghidra: <c>0x00458200</c> — <b>l'embarcation que le groupe pilote a la pagaie</b>. Creneau
    /// de la table <c>0x0056adc8</c> installee par les sous-zones 6, 7 et 8 de la foret, donc une
    /// entite de la troisieme table (<c>0x005fa5e0</c>, passe <c>FUN_0050e5e0</c>). 2088 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; releve sous <c>Legacy/Raw/Blaze/FUN_00458200.asm</c>.
    ///
    /// <para>
    /// <b>Ce que le corps etablit</b>, sans rien supposer : quatre gachettes agissent en couples
    /// (rotation, poussee) opposes deux a deux — c'est un coup de pagaie ; l'engin avance sur son
    /// propre cap <c>+0x32</c> par sinus et cosinus ; il seme deux entites <c>0x30</c> par image
    /// des qu'il bouge, par <see cref="SpawnEffectNearCamera_00468110"/>, plus une troisieme une
    /// fois sur six — un sillage ; et il ne bouge <b>que si tout le groupe est a bord</b>. Ce que
    /// le corps <b>n'etablit pas</b> : la nature du milieu. Rien ici ne dit « eau » ; c'est le
    /// terrain qui le dit, par le bit <c>0x2000</c> des attributs de face.
    /// </para>
    ///
    /// <para>
    /// <b>Qui est a bord.</b> Chaque image, les quatre emplacements sont classes en trois tas. Un
    /// membre est <b>a bord</b> si les attributs de la face qu'il foule — a l'indice de genre que
    /// l'embarcation foule elle-meme, <c>(*(byte**)(e + 0x7c))[0x0e]</c> — portent le bit
    /// <c>0x2000</c>, <b>et</b> si son <c>+0x78</c> designe une entite de sous-type <c>0x14</c>. Il
    /// est <b>a terre</b> si le bit de face manque. Entre les deux — le bit present mais pas la
    /// monture — il n'est compte nulle part : les deux totaux ne sont pas complementaires, et c'est
    /// ce qui rend les trois branches suivantes distinctes.
    /// </para>
    ///
    /// <list type="bullet">
    /// <item><b>Tout le monde a bord</b> (<c>aBord == ActivePartySlotCount_00466D70</c>) : la
    /// camera passe a l'embarcation. Le mode de vue courant est mis de cote dans
    /// <c>DAT_00604b7c</c>, le mode devient <b>3</b>, la cible et l'angle de vue sont rebranches
    /// sur deux triplets du bloc prive de la foret (<c>0x00571658</c> et <c>0x005715a8</c>), et
    /// <c>+0x9a</c> retient « j'ai la barre ».</item>
    /// <item><b>Tout le monde a terre</b> : l'angle courant est rendu au but <c>0x006067e0</c>
    /// masque sur douze bits, <see cref="CameraModeApply_005037E0"/> rabat le mode 3 sur celui de
    /// la sauvegarde et rebranche les trois globales, et <c>+0x9a</c> repasse a zero.</item>
    /// <item><b>Entre les deux</b> : l'embarcation garde son echelle mais ne bouge pas.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Le pilotage, et pourquoi il depend du nombre de rameurs.</b> Le pas de rotation, le pas
    /// de poussee et le plafond des deux sont choisis par le nombre de membres <i>utilisables</i>
    /// (bit <c>0x80000000</c> pose, <c>0x50000000</c> et <c>0x10000</c> absents) :
    /// </para>
    /// <list type="table">
    /// <item><term>1</term><description>rotation 8, poussee 4, plafond 0x23</description></item>
    /// <item><term>2</term><description>rotation 4, poussee 2, plafond 0x2b</description></item>
    /// <item><term>3</term><description>rotation 3, poussee 2, plafond 0x32</description></item>
    /// <item><term>4</term><description>rotation 2, poussee 1, plafond 0x3c</description></item>
    /// </list>
    /// <para>
    /// <b>Plus on est nombreux, moins chaque coup porte, mais plus l'engin peut aller vite</b> : le
    /// pas est divise par quatre entre un rameur et quatre, le plafond est presque double. Les
    /// quatre gachettes se repartissent alors les quatre coins : <c>0x400</c> tourne a gauche en
    /// poussant, <c>0x800</c> tourne a droite en poussant, <c>0x100</c> tourne a droite en tirant,
    /// <c>0x200</c> tourne a gauche en tirant. Chaque membre encore debout ajoute la sienne, et les
    /// deux accumulateurs <c>+0x14</c> et <c>+0x16</c> perdent une unite par image en dehors de ca.
    /// </para>
    ///
    /// <para>
    /// <b>Trois arrets d'urgence</b>, tous en remettant <c>+0x14</c> et <c>+0x16</c> a zero : un
    /// membre portant le bit <c>0x10000000</c> (et la, retour immediat), un membre portant a la
    /// fois <c>0x80000000</c>, <c>0x40000000</c> et <c>0x80</c>, et l'absence de barre.
    /// </para>
    ///
    /// <para>
    /// <b>Le pas et son annulation.</b> Le cap avance de <c>+0x14</c>, la position de
    /// <c>sin/cos(cap + 0x400) * +0x16</c>, puis <see cref="FinalizeEntityPlacement2"/> pose
    /// l'entite. <c>FUN_00458a30</c> — le balayage a six sondes autour d'elle — dit si elle a
    /// touche ; si oui, <b>dix champs</b> sont remis comme ils etaient (position, cap, les quatre
    /// mots de face <c>+0x80</c>..<c>+0x8c</c>, la face <c>+0x7c</c>, le mot de contact
    /// <c>+0x90</c>) et les deux accumulateurs sont annules. C'est le meme jeu de champs que
    /// <c>RollBackMove</c> restaure pour une entite de scene ordinaire.
    /// </para>
    ///
    /// <para>
    /// <b>Elle se souvient d'ou elle est.</b> <c>+0x10</c> n'est pas un etat mais un
    /// <b>renvoi vers sa propre fiche de placement</b> : la premiere image y range
    /// <c>(+0x14 &lt;&lt; 8) | +0x16</c>, c'est-a-dire la liste de sous-zone dans le haut et le
    /// rang dans le bas, tels que la sous-zone les avait poses. Chaque image suivante y reecrit
    /// position et cap. <b>Les memes deux champs servent donc a deux choses selon que
    /// l'initialisation a eu lieu ou non</b> — indices de fiche avant, pilotage apres.
    /// </para>
    ///
    /// <para>
    /// <b>Deux traces de 1998 conservees.</b> (1) Quand aucun membre n'est utilisable, ni le pas ni
    /// le plafond ne sont poses : les trois gardent ce que les comptages precedents avaient laisse
    /// dans leurs registres — le nombre de membres a terre pour la rotation, le nombre a bord pour
    /// la poussee et pour le plafond. (2) Les quatre <c>short</c> de <c>+0x10</c> a <c>+0x16</c> de
    /// la fiche de placement recoivent <c>0xffff</c> et non zero, le registre servant a la fois de
    /// « pas de teinte » pour le sillage et de valeur ecrite ici.
    /// </para>
    /// </summary>
    private static void TickPartyRaft_00458200(byte* e)
    {
        byte* members = Inp.s_SlotFlags_005e93c0;

        // --- Franchir la limite en z relance le script de chaque membre -----------------------
        if (*(int*)(e + 0x24) > 0x125c000)
        {
            for (int i = 0; i < 4; i++)
            {
                uint flags = *(uint*)(members + i * 0x9c);
                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x50000000) != 0) continue;

                RunEntityScript(s_Unknown_005e97cc, members + i * 0x9c, 0);
            }
        }

        // --- Ghidra: LAB_00458249 — la premiere image, et elle seule ---------------------------
        if (*(short*)(e + 0x10) == 0)
        {
            int dx = (*Mem.Camera(0x005de068) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dy = (*Mem.Camera(0x005de06c) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
            int dz = (*Mem.Camera(0x005de070) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

            // A moins de cent unites du meneur, l'embarcation se colle a lui.
            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < 0x64)
            {
                *(int*)(e + 0x1c) = *Mem.Camera(0x005de068);
                *(int*)(e + 0x20) = *Mem.Camera(0x005de06c);
                *(int*)(e + 0x24) = *Mem.Camera(0x005de070);
            }

            SettleRaftOnTerrain(e);

            // Le binaire reteste +0x10 apres l'appel, puis une troisieme fois plus bas. Rien de ce
            // qui precede n'y touche ; les deux tests sont conserves comme ils sont ecrits.
            if (*(short*)(e + 0x10) == 0)
            {
                if ((*Mem.Arena(0x005e9700) & 1) != 0)
                {
                    int px = *Mem.Camera(0x005de068) >> 0xc;
                    int pz = *Mem.Camera(0x005de070) >> 0xc;

                    // Le meneur dans ce rectangle : l'embarcation est deplacee d'office a son
                    // poste d'amarrage.
                    if (px > -100 && px < 300 && pz > 1000 && pz < 1300)
                    {
                        *(int*)(e + 0x1c) = -0x96000;
                        *(int*)(e + 0x24) = 0x960000;
                        *(short*)(e + 0x32) = 0x400;

                        SettleRaftOnTerrain(e);
                    }
                }

                // Ghidra: LAB_00458389 — +0x14 et +0x16 changent de metier ici.
                if (*(short*)(e + 0x10) == 0)
                {
                    *(short*)(e + 0x9a) = 0;
                    *(short*)(e + 0x10) =
                        (short)((*(short*)(e + 0x14) << 8) | *(short*)(e + 0x16));
                    *(short*)(e + 0x12) = 0;
                    *(short*)(e + 0x14) = 0;
                    *(short*)(e + 0x16) = 0;
                }
            }
        }

        // --- Ghidra: LAB_004583b3 — qui est a bord, qui est a terre ---------------------------
        int ashore = 0;
        int aboard = 0;

        for (int i = 0; i < 4; i++)
        {
            byte* member = members + i * 0x9c;
            uint flags = *(uint*)member;

            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            // La face que l'embarcation foule est relue A CHAQUE tour, et seulement une fois les
            // deux tests d'etat passes : un emplacement vide ne la deference donc pas.
            byte raftFaceKind = (*(byte**)(e + 0x7c))[0x0e];

            // Les attributs que TryPlaceEntity a ecrits pour le membre, au genre de face que
            // l'embarcation foule elle-meme.
            if ((*(ushort*)(member + 0x82 + raftFaceKind * 4) & 0x2000) == 0)
            {
                ashore++;
                continue;
            }

            byte* mount = *(byte**)(member + 0x78);
            if (mount == null || mount[5] != 0x14) continue;

            aboard++;

            // L'angle de vue suit l'embarcation tant qu'on est dessus.
            short* view = (short*)ForestSceneField(0x005715a8);
            view[1] = (short)(0xfc00 - *(short*)(e + 0x32));
            view[0] = (short)(*(short*)(e + 0x30) + 0x200);
            view[2] = *(short*)(e + 0x34);
        }

        // --- La barre : tout le monde a bord la prend, tout le monde a terre la rend -----------
        if (aboard == (short)ActivePartySlotCount_00466D70())
        {
            if (*(short*)(e + 0x9a) == 0)
            {
                Saves.s_SavedViewMode_00604b7c = s_Unknown_005de54c;
                int* target = *(int**)Mem.Entity(0x005eca94);

                s_Unknown_005de54c = 3;

                *(int*)ForestSceneField(0x00571660) = target[2];
                *(int*)ForestSceneField(0x00571658) = target[0];
                *(int*)ForestSceneField(0x0057165c) = target[1];

                *(byte**)Mem.Entity(0x005eca94) = ForestSceneField(0x00571658);
                *(byte**)Mem.Arena(0x005e9ae0) = ForestSceneField(0x005715a8);
            }

            *(short*)(e + 0x9a) = 1;
        }

        // Ghidra: LAB_004584af — le second appel, le binaire ne reutilise pas le premier resultat.
        if (ashore == (short)ActivePartySlotCount_00466D70())
        {
            if (*(short*)(e + 0x9a) == 1)
            {
                short* view = (short*)ForestSceneField(0x005715a8);
                short* goal = (short*)Mem.View(0x006067e0);

                goal[0] = (short)(view[0] & 0xfff);
                goal[1] = (short)(view[1] & 0xfff);
                goal[2] = (short)(view[2] & 0xfff);

                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x16) = 0;

                CameraModeApply_005037E0(0);                  // Ghidra: FUN_005037b0
            }

            *(short*)(e + 0x9a) = 0;
            return;
        }

        // Ghidra: LAB_0045851a
        if (*(short*)(e + 0x9a) == 0) return;

        if (ashore == 0 && aboard == 0)
        {
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            return;
        }

        // Ghidra: LAB_00458541
        *(short*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2c) = 0x1000;

        if (aboard != (short)ActivePartySlotCount_00466D70() && *(short*)(e + 0x9a) == 1)
        {
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            return;
        }

        // Ghidra: LAB_00458572 — un membre occupe arrete tout.
        for (int i = 0; i <= 3; i++)
        {
            if ((*(uint*)(members + i * 0x9c) & 0x10000000) != 0)
            {
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x16) = 0;
                return;
            }
        }

        // La distance de camera glisse vers 1800 par huitiemes.
        int* reach = (int*)ForestSceneField(0x00571660);
        *reach += (0x708 - *reach) >> 3;

        // --- L'embarcation devient la reference du groupe -------------------------------------
        if (*(short*)(e + 0x9a) == 1)
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

        // --- Ghidra: LAB_00458617 — les deux accumulateurs perdent une unite par image ---------
        if (*(short*)(e + 0x14) > 0) *(short*)(e + 0x14) -= 1;
        if (*(short*)(e + 0x14) < 0) *(short*)(e + 0x14) += 1;
        if (*(short*)(e + 0x16) > 0) *(short*)(e + 0x16) -= 1;
        if (*(short*)(e + 0x16) < 0) *(short*)(e + 0x16) += 1;

        // --- Ghidra: LAB_0045864f — la sauvegarde que l'annulation relira ----------------------
        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);
        short savedHeading = *(short*)(e + 0x32);
        uint saved80 = *(uint*)(e + 0x80);
        uint saved84 = *(uint*)(e + 0x84);
        uint saved88 = *(uint*)(e + 0x88);
        uint saved8c = *(uint*)(e + 0x8c);
        uint savedFace = *(uint*)(e + 0x7c);
        ushort savedContact = *(ushort*)(e + 0x90);

        // --- Combien de rameurs, et donc quel pas ---------------------------------------------
        int usable = 0;
        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(members + i * 0x9c);
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;
            if ((flags & 0x10000) != 0) continue;
            usable++;
        }

        // Aucun rameur : les trois gardent ce que les comptages precedents ont laisse dans les
        // registres. C'est ce que fait le binaire ; voir le resume.
        int turnStep = ashore;
        int thrustStep = aboard;
        int cap = aboard;

        if (usable == 1) { turnStep = 8; thrustStep = 4; cap = 0x23; }
        if (usable == 2) { turnStep = 4; thrustStep = 2; cap = 0x2b; }
        if (usable == 3) { turnStep = 3; thrustStep = 2; cap = 0x32; }
        if (usable == 4) { turnStep = 2; thrustStep = 1; cap = 0x3c; }

        // --- Ghidra: LAB_00458741 — les quatre coups de pagaie --------------------------------
        for (int i = 0; i < 4; i++)
        {
            byte* member = members + i * 0x9c;
            uint flags = *(uint*)member;

            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            if ((RaftPaddleBits(member) & 4) != 0)
            {
                *(short*)(e + 0x14) -= (short)turnStep;
                *(short*)(e + 0x16) += (short)thrustStep;
            }

            if ((RaftPaddleBits(member) & 8) != 0)
            {
                *(short*)(e + 0x14) += (short)turnStep;
                *(short*)(e + 0x16) += (short)thrustStep;
            }

            if ((RaftPaddleBits(member) & 1) != 0)
            {
                *(short*)(e + 0x14) += (short)turnStep;
                *(short*)(e + 0x16) -= (short)thrustStep;
            }

            if ((RaftPaddleBits(member) & 2) != 0)
            {
                *(short*)(e + 0x14) -= (short)turnStep;
                *(short*)(e + 0x16) -= (short)thrustStep;
            }
        }

        // Ghidra: LAB_004587ff — un membre dans cet etat-la arrete tout.
        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(members + i * 0x9c);

            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x40000000) == 0) continue;
            if ((flags & 0x80) == 0) continue;

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        // --- Le plafond, sur les deux accumulateurs et dans les deux sens ----------------------
        if (*(short*)(e + 0x16) > cap) *(short*)(e + 0x16) = (short)cap;
        if (*(short*)(e + 0x16) < -cap) *(short*)(e + 0x16) = (short)-cap;
        if (*(short*)(e + 0x14) > cap) *(short*)(e + 0x14) = (short)cap;
        if (*(short*)(e + 0x14) < -cap) *(short*)(e + 0x14) = (short)-cap;

        // --- Ghidra: LAB_0045885f — le sillage ------------------------------------------------
        if (*(short*)(e + 0x16) != 0 || *(short*)(e + 0x14) != 0)
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
                if (CrtRand() % 6 == 1) SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
            }
        }

        // --- Ghidra: LAB_004588af — le pas ----------------------------------------------------
        *(short*)(e + 0x32) += *(short*)(e + 0x14);

        int heading = *(short*)(e + 0x32) + 0x400;
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * *(short*)(e + 0x16);
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * *(short*)(e + 0x16);

        FinalizeEntityPlacement2((uint*)e);                   // Ghidra: FUN_00533940

        int height;

        if (SweepEntitySurroundings_00458A30((uint*)e) != 0)
        {
            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x24) = savedZ;
            *(short*)(e + 0x32) = savedHeading;
            *(uint*)(e + 0x80) = saved80;
            *(uint*)(e + 0x84) = saved84;
            *(uint*)(e + 0x88) = saved88;
            *(uint*)(e + 0x8c) = saved8c;
            *(uint*)(e + 0x7c) = savedFace;
            *(int*)(e + 0x20) = savedY;
            *(uint*)(e + 0x90) = savedContact;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            height = savedY;
        }
        else
        {
            // Ghidra: LAB_00458974 — la hauteur n'est relue que sur le chemin sans contact.
            height = *(int*)(e + 0x20);
        }

        // Ghidra: LAB_00458977
        *(int*)(e + 0x64) = height;
        *(int*)(e + 0x60) = height;

        FinalizeEntityPlacement2((uint*)e);

        // --- La fiche de placement de la sous-zone est reecrite --------------------------------
        short handle = *(short*)(e + 0x10);
        byte* record = ((byte**)*(byte***)(s_Unknown_005e97cc + 0x30))[handle >> 8]
                       + (handle & 0xff) * 0x20;

        *(short*)(record + 0x04) = (short)(*(int*)(e + 0x1c) >> 0xc);
        *(short*)(record + 0x06) = (short)(*(int*)(e + 0x20) >> 0xc);
        *(short*)(record + 0x08) = (short)(*(int*)(e + 0x24) >> 0xc);
        *(short*)(record + 0x0c) = *(short*)(e + 0x32);

        short* here = (short*)ForestSceneField(0x00571650);
        here[0] = (short)(*(int*)(e + 0x1c) >> 0xc);
        here[1] = (short)(*(int*)(e + 0x20) >> 0xc);
        here[2] = (short)(*(int*)(e + 0x24) >> 0xc);
        *(short*)ForestSceneField(0x00571580) = *(short*)(e + 0x32);

        // Le registre vaut -1 depuis le sillage, et c'est lui qui est ecrit : 0xffff, pas zero.
        *(short*)(record + 0x10) = -1;
        *(short*)(record + 0x12) = -1;
        *(short*)(record + 0x14) = -1;
        *(short*)(record + 0x16) = -1;
    }

    /// <summary>
    /// Ghidra: <c>PTR_FUN_0056b0c4</c> — <b>l'entree 63 de la table installee <c>0x0056afc8</c></b>,
    /// et non un pointeur isole. Le chargement de sous-zone recopie <b>0x40 entrees</b> a partir de
    /// <c>0x0056afc8</c> (<c>FUN_0052a660</c>, branche <c>DAT_00600904</c>), et
    /// <c>0x0056afc8 + 63 * 4 = 0x0056b0c4</c> : c'est la derniere. Ce n'est donc pas une globale
    /// qu'il faudrait chercher, c'est un creneau de table que le jeu remplit par sous-zone.
    ///
    /// <para>
    /// <b>Elle n'est pas appelee comme un gestionnaire d'entite.</b> Les sept tables portent
    /// partout ailleurs des fonctions a un seul argument ; ici le binaire pousse <b>trois</b>
    /// arguments et nettoie <c>0x0c</c> octets — l'entite, sa position d'avant le pas, et un
    /// masque. Le creneau 63 est reserve a autre chose qu'un pas d'entite.
    /// </para>
    /// <para>
    /// En foret, sous-zones 6 a 8, il vaut <c>0x00541550</c> (bloc <c>0x00555c28</c>, entree 63).
    /// <b>C'est le seul creneau 63 non vide du lieu</b> : les autres blocs de la foret, celui des
    /// sous-zones 5 et 9 (<c>0x005562a8</c>) compris, y laissent la souche <c>0x00401FA0</c>. Le
    /// pas de collision est donc une specificite de l'embarcation, pas un equipement de zone.
    /// </para>
    /// </summary>
    private static void CallSubAreaMoveProbe(byte* entity, int* previous, int mask)
    {
        uint target = Res.ResolveEntityHandler(0x0056afc8, 0x3f);
        if (target == 0x00401FA0) return;                     // corps vide, verifie dans Blaze.c

        switch (target)
        {
            case 0x00541550: StepEntityAcrossTerrain_00541550(entity, previous, mask); return;
        }

        throw new NotImplementedException(
            $"FUN_00458a30 : le pas de collision de la sous-zone (0x0056afc8[63] = 0x{target:X6}) " +
            "n'est pas transcrit. Celui de la foret 6-8 (0x00541550) l'est, et c'est le seul " +
            "creneau 63 non vide de la foret : les sous-zones 5 et 9 (bloc 0x005562a8) y portent " +
            "la souche 0x00401FA0. Relever le corps manquant avec " +
            $"`OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x{target:X6} <taille>`.");
    }

    /// <summary>
    /// Une des six sondes de <see cref="SweepEntitySurroundings_00458A30"/> : la copie de travail
    /// avance de <paramref name="distance"/> sur le cap tourne de <paramref name="turn"/>, puis le
    /// pas de collision de la sous-zone dit si elle a touche.
    ///
    /// <para>
    /// <b>Les six sondes s'enchainent, elles ne repartent pas du centre</b> : chacune relit la
    /// position que la precedente a laissee, y compris ce que le pas de collision a pu y corriger.
    /// C'est une marche autour de l'entite, pas six rayons.
    /// </para>
    /// </summary>
    private static bool ProbeAroundEntity(byte* scratch, int turn, int distance)
    {
        int* previous = stackalloc int[3];
        previous[0] = *(int*)(scratch + 0x1c);
        previous[1] = *(int*)(scratch + 0x20);
        previous[2] = *(int*)(scratch + 0x24);

        int angle = *(short*)(scratch + 0x32) + turn;

        *(int*)(scratch + 0x1c) += (int)Winsys3d.GetSinTableEntry(angle) * distance;
        *(int*)(scratch + 0x24) += Winsys3d.GetCosTableEntry(angle) * distance;

        CallSubAreaMoveProbe(scratch, previous, 0x2000);

        return (*(uint*)ForestSceneField(0x00571640) & 0x8800) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00458a30 @ 0x00458a30 — <b>le balayage a six sondes</b> qui dit a
    /// <see cref="TickPartyRaft_00458200"/> si elle vient de heurter quelque chose. 943 octets,
    /// decompilee dans <c>Blaze.c</c> mais avec les arguments des appels indirects perdus ; le
    /// releve du desassembleur est garde sous <c>Legacy/Raw/Blaze/FUN_00458A30.asm</c>.
    ///
    /// <para>
    /// <b>Elle travaille sur une copie</b>, les 0x27 mots de l'entite recopies dans le bloc prive
    /// de la foret a <c>0x005715b0</c> — d'ou les noms <c>DAT_005715cc</c> (sa position),
    /// <c>DAT_005715e2</c> (son cap) et <c>DAT_00571640</c> (son mot de contact) que
    /// <c>Blaze.c</c> emploie sans les relier a l'entite. L'originale n'est jamais touchee.
    /// </para>
    /// <para>
    /// <b>Six pas, six directions, six distances</b>, dans cet ordre : <c>+0x400</c> a 220,
    /// <c>+0xe00</c> a 150, <c>+0xc00</c> a 330, <c>+0x800</c> a 220, <c>+0x400</c> a 330,
    /// <c>+0x200</c> a 150. Le rendu est <b>le numero de la derniere sonde qui a touche</b>, de 1 a
    /// 6, ou zero — les six ne s'arretent pas au premier contact, et un contact precoce est donc
    /// ecrase par un contact tardif. L'appelant ne regarde que « nul ou non ».
    /// </para>
    /// </summary>
    private static short SweepEntitySurroundings_00458A30(uint* entity)
    {
        byte* scratch = ForestSceneField(0x005715b0);
        Mem.CopyDwords(entity, (uint*)scratch, 0x27);

        short hit = 0;

        if (ProbeAroundEntity(scratch, 0x400, 0xdc)) hit = 1;
        if (ProbeAroundEntity(scratch, 0xe00, 0x96)) hit = 2;
        if (ProbeAroundEntity(scratch, 0xc00, 0x14a)) hit = 3;
        if (ProbeAroundEntity(scratch, 0x800, 0xdc)) hit = 4;
        if (ProbeAroundEntity(scratch, 0x400, 0x14a)) hit = 5;
        if (ProbeAroundEntity(scratch, 0x200, 0x96)) hit = 6;

        return hit;
    }

    // =====================================================================
    // Les gestionnaires que Ghidra n'a pas decompiles - transcrits depuis le binaire le
    // 2026-08-24. Ils manquaient tous a Blaze.c pour la meme raison que LAB_00512ef0 et
    // LAB_00513660 : vus seulement pris en adresse dans une table de sauts, jamais appeles
    // directement, donc jamais promus en fonction.
    //
    // Les cinq ci-dessous sont courts et reguliers. Le sixieme, 0x004502E0, fait ~1030 octets et
    // reste a faire : voir la lacune nommee plus bas.
    //
    // Relevés bruts : Legacy/Raw/Blaze/FUN_004508B0.asm, FUN_00473A70.asm, FUN_00450BD0.asm,
    // FUN_004736D0.asm, FUN_004746C0.asm, FUN_00468110.asm.
    //
    // ATTENTION AUX CONVENTIONS : ces corps sont __cdecl (`ret` nu, l'appelant nettoie), a la
    // difference du rappel de l'ordinal 790 qui est __stdcall. Cela ne change rien ici - le
    // repartiteur les appelle par un pointeur gere - mais c'est ce que dit le binaire.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004508b0 @ 0x004508b0 - le sous-type <b>15</b>. Dix-huit octets : il pose une
    /// echelle de 0x320 en +0x2c et allume le bit 0x4000 du mot d'etat, puis rend la main.
    ///
    /// <para>
    /// Le bit est pose par <c>or ch,40h</c>, c'est-a-dire sur le <b>deuxieme octet</b> de
    /// <c>ecx</c> : 0x4000, pas 0x40. C'est le genre de detail qu'une lecture rapide du
    /// desassemblage inverse.
    /// </para>
    /// </summary>
    private static void EntityHandler_004508B0(uint* entity)
    {
        byte* e = (byte*)entity;
        *(ushort*)(e + 0x2c) = 0x320;
        *(uint*)e |= 0x4000;
    }

    /// <summary>
    /// Ghidra: FUN_00456730 @ 0x00456730 — <b>le sous-type 32 de la foret</b> : il pose l'echelle
    /// de l'entite a <c>0x0C00</c> sur les trois axes, et rien d'autre. Vingt-deux octets, absente
    /// de <c>Blaze.c</c> ; dump dans <c>Legacy/Raw/Blaze/FUN_00456730.asm</c>.
    ///
    /// <para>
    /// <c>+0x28</c>, <c>+0x2a</c> et <c>+0x2c</c> sont l'echelle X, Y, Z de l'enregistrement, ou
    /// <c>0x1000</c> est l'unite — c'est ce que <see cref="SpawnEntityFormation"/> y ecrit pour une
    /// entite a taille normale. <c>0x0C00</c> est donc <b>trois quarts</b> : cette entite de zone
    /// nait plus petite que son modele.
    /// </para>
    /// <para>
    /// Le meme sous-type 32 renvoie au gestionnaire par defaut <c>0x004734B0</c> dans la zone 0.
    /// C'est la premiere fois qu'une entree de table <b>propre a une zone</b> est exercee, et elle
    /// confirme le releve des onze zones : la valeur lue au runtime est bien celle du bloc
    /// <c>0x005553a8</c>, entree 32.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456730(uint* entity)
    {
        byte* e = (byte*)entity;
        *(ushort*)(e + 0x28) = 0x0c00;
        *(ushort*)(e + 0x2a) = 0x0c00;
        *(ushort*)(e + 0x2c) = 0x0c00;
    }

    /// <summary>
    /// Ghidra: FUN_00458170 @ 0x00458170 — <b>le sous-type 31 de la foret</b> : un decor qui
    /// <b>parle quand on le touche</b>, avec une petite machine a trois etats en <c>+0x10</c>.
    /// 133 octets, absente de <c>Blaze.c</c> ; dump dans
    /// <c>Legacy/Raw/Blaze/FUN_00458170.asm</c>.
    ///
    /// <para>
    /// <b>L'etat 0 est le repos.</b> Il ne se declenche que si les quatre bits bas de <c>+0x9</c>
    /// sont poses — c'est le champ de contact que la simulation remplit. Le decor lie alors son
    /// texte et passe a l'etat 2 avec un compte de 3 en <c>+0x12</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est un compte a rebours de trois images</b> vers l'etat 1, et <b>l'etat 1
    /// attend que le contact cesse</b> pour revenir a 0. C'est ce qui empeche un decor de reparler
    /// a chaque image tant qu'on reste appuye contre lui.
    /// </para>
    /// <para>
    /// <b>Le sujet du texte a un repli</b> : <c>+0x70</c> designe l'entite concernee, et quand il
    /// est nul c'est <c>s_EntityList_005de558</c> — l'enregistrement neutre que
    /// <see cref="Resources"/> remet a zero a chaque chargement de zone. Le bit <c>0x10000</c> de
    /// son mot de tete (« ecarte ») annule l'affichage sans annuler le changement d'etat : le decor
    /// consomme le contact meme quand il ne dit rien.
    /// </para>
    /// <para>
    /// Le bloc d'etiquettes vient de la table <c>+0x70</c> du repertoire de zone, indexee par
    /// <c>+0x16</c> — l'index est lu <c>movsx</c>, donc <b>signe</b>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00458170(uint* entity)
    {
        byte* e = (byte*)entity;

        // Ghidra: cmp word [esi+10h],0 ; jne LAB_004581C6 — puis test byte [esi+9],0Fh ; je idem.
        if (*(ushort*)(e + 0x10) == 0 && (e[9] & 0x0f) != 0)
        {
            // Ghidra: mov eax,[esi+70h] ; test eax,eax ; jne LAB_0045818E ; mov eax,5DE558h
            byte* subject = *(byte**)(e + 0x70);
            if (subject == null) subject = Res.s_EntityList_005de558;

            if ((*(uint*)subject & 0x10000) == 0)
            {
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + *(short*)(e + 0x16) * 4),
                    *(uint*)(e + 0x6c),
                    s_WorldMapDescriptor_005633b0,
                    0);
            }

            *(ushort*)(e + 0x10) = 2;
            *(ushort*)(e + 0x12) = 3;
        }

        // Ghidra: LAB_004581C6 — l'etat 1 retombe a 0 des que les bits bas de +0x9 s'eteignent.
        if (*(ushort*)(e + 0x10) == 1 && (e[9] & 0x0f) == 0)
            *(ushort*)(e + 0x10) = 0;

        // Ghidra: LAB_004581DD — `dec word [esi+12h]` puis `jg`, donc une comparaison SIGNEE.
        if (*(ushort*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0) *(ushort*)(e + 0x10) = 1;
        }
    }

    // =====================================================================================
    // LES GESTIONNAIRES D'ENTITE DE LA FORET (table 0x0056adc8, bloc 0x005553a8)
    //
    // La zone 3 remappe 25 des 64 sous-types. Ils sont transcrits ici a la demande, dans l'ordre
    // ou le jeu les demande, avec leurs deux appelees communes en tete.
    // =====================================================================================

    /// <summary>
    /// Ghidra: FUN_00535000 @ 0x00535000 — <b>compte les membres du groupe rattaches a une
    /// entite</b>. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Elle balaie les <see cref="s_PartySize_005e9020"/> premiers emplacements de
    /// <c>0x005e93c0</c> (pas de <c>0x9c</c>) et compte ceux dont le champ <c>+0x78</c> vaut
    /// <paramref name="subject"/>, dont le mot de tete porte <b>au moins un</b> bit de
    /// <paramref name="required"/>, et <b>aucun</b> de <paramref name="forbidden"/>.
    /// </para>
    /// <para>
    /// <b>L'ordre des arguments vient du desassembleur</b>, pas d'une lecture de la pseudo-C : les
    /// quatre <c>push</c> du prologue decalent les offsets, et les trois lectures
    /// (<c>[esp+0Ch]</c>, <c>[esp+10h]</c>, <c>[esp+1Ch]</c>) tombent respectivement sur les
    /// arguments <b>2, 1 et 3</b>. Les lire dans l'ordre du code donnerait la permutation inverse.
    /// </para>
    /// <para>Le garde <c>test ecx,ecx</c> en tete rend 0 quand le groupe est vide, sans entrer dans
    /// la boucle — un groupe vide ferait sinon 4 milliards de tours.</para>
    /// </summary>
    private static int CountPartyMembersMatching(byte* subject, uint required, uint forbidden)
    {
        int size = s_PartySize_005e9020;
        if (size == 0) return 0;

        int count = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < size; i++, member += 0x9c)
        {
            if (*(byte**)(member + 0x78) != subject) continue;

            uint flags = *(uint*)member;
            if ((flags & required) == 0) continue;
            if ((flags & forbidden) != 0) continue;

            count++;
        }

        return count;
    }

    /// <summary>
    /// Ghidra: FUN_004580E0 @ 0x004580e0 — <b>le son de proximite a portee variable</b>. Absente de
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// C'est le <b>troisieme membre</b> de la famille de <see cref="TryPlaySoundNearEntity"/> et
    /// <see cref="TryPublishCameraEffectNearEntity"/> : meme calcul de distance a la camera
    /// (<c>DAT_005edebc</c>), memes six arguments, meme convention <b>stdcall</b> de
    /// <c>Ordinal_442</c> qui rend les offsets de pile lisibles.
    /// </para>
    /// <para>
    /// Les deux differences tiennent en une ligne chacune. La portee n'est <b>pas une constante</b>
    /// (1500 et 2000 chez ses jumelles) mais l'argument <paramref name="range"/>. Et le gain n'est
    /// pas passe tel quel : il <b>decroit lineairement avec la distance</b>, puis part sur les deux
    /// voies. Le son est donc <b>non positionnel</b> — <c>PlaySoundEffect</c> et non
    /// <c>PlayPositionalSound</c> : c'est l'attenuation qui porte la distance, pas la voie.
    /// </para>
    /// <para>Les arguments 5 et 6 ne sont lus nulle part, comme chez ses jumelles.</para>
    /// </summary>
    /// <returns>1 si l'entite etait dans la portee et que le son est parti, 0 sinon.</returns>
    private static ushort TryPlayDistanceSound(
        uint* entity, short soundId, short pitch, short gain, int range, int unused6)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);
        byte* e = (byte*)entity;

        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
        if (distance >= range) return 0;

        // Ghidra: `shl eax,0Ch ; cdq ; idiv esi` puis `imul eax,edx ; sar eax,0Ch`.
        int attenuated = ((range - distance) << 0xc) / range * gain >> 0xc;

        Aud.PlaySoundEffect(soundId, pitch, attenuated, attenuated);
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004566C0 @ 0x004566c0 — <b>le sous-type 33</b> : un decor qui <b>chuchote toutes
    /// les vingt images</b>. 110 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Un unique compteur en <c>+0x98</c> : a zero le son part, puis le compteur avance et
    /// <b>reboucle a 20</b>. L'etat <c>+0x10</c> ne sert qu'a l'amorcage — pose a 1 au premier
    /// tour, il n'est plus jamais relu.
    /// </para>
    /// <para>
    /// L'identifiant du son n'est pas une constante : c'est l'octet <c>+0x15</c> du premier
    /// enregistrement de la table <c>+0xA8</c> du repertoire de zone, <b>etendu sans signe</b>
    /// (<c>movzx</c>). Chaque zone donne donc sa propre voix a ce decor — ce qui est coherent avec
    /// un sous-type remappe par zone.
    /// </para>
    /// <para>Portee 3000, hauteur 0x400, gain 0x600.</para>
    /// </summary>
    private static void EntityHandler_004566C0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(ushort*)(e + 0x98) = 0;
        }

        if (*(ushort*)(e + 0x98) == 0)
        {
            // Ghidra: `mov ecx,[eax+0A8h] ; mov edx,[ecx] ; movzx ax,byte [edx+15h]`.
            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlayDistanceSound(entity, sounds[0x15], 0x400, 0x600, 0xbb8, 0);
        }

        *(ushort*)(e + 0x98) += 1;
        if (*(ushort*)(e + 0x98) == 0x14) *(ushort*)(e + 0x98) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004571D0 @ 0x004571d0 — <b>le sous-type 22</b> : un decor qui <b>balance</b>.
    /// 103 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Au premier tour il se tire une <b>phase</b> et une <b>vitesse</b> au hasard :
    /// <c>+0x12 = rand() % 0x1000</c> et <c>+0x14 = rand() % 50 + 50</c>. Deux decors du meme
    /// sous-type ne balancent donc jamais ensemble, ce qui est tout l'interet.
    /// </para>
    /// <para>
    /// Ensuite, a chaque image : <c>+0x34 = sin(+0x12) &gt;&gt; 5</c>, et la phase avance de sa
    /// vitesse, masquee sur <c>0xfff</c> — la table de sinus de <c>winsys3d</c> fait <c>0x1000</c>
    /// entrees par tour.
    /// </para>
    /// <para>
    /// <b>Le premier tirage n'est pas un simple masque</b> : le binaire fait
    /// <c>cdq ; xor ; sub ; and 0xfff ; xor ; sub</c>, c'est-a-dire l'idiome du <b>reste signe</b>
    /// que le compilateur emet pour <c>% 0x1000</c>. Transcrire le masque seul changerait le
    /// resultat sur un tirage negatif.
    /// </para>
    /// </summary>
    private static void EntityHandler_004571D0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x32 + 0x32);
        }

        if (*(ushort*)(e + 0x10) == 1)
        {
            // Ghidra: call 00545A1C — le thunk d import winsys3d Ordinal_451, la table de sinus.
            // `sar eax,5` : decalage ARITHMETIQUE, donc l entree de table est relue en int signe.
            *(short*)(e + 0x34) = (short)((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) >> 5);
            *(ushort*)(e + 0x12) = (ushort)((*(ushort*)(e + 0x14) + *(ushort*)(e + 0x12)) & 0xfff);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00457240 @ 0x00457240 — <b>le sous-type 19</b> : un objet qui <b>tombe, touche
    /// le sol, puis flotte</b>. 300 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>Le bit <c>0x40000000</c> du mot de tete separe les deux moities de la fonction, et
    /// c'est lui qui dit si l'objet est <b>pose</b> :</para>
    ///
    /// <list type="bullet">
    /// <item><b>Pose</b> — il oscille. <c>+0x16</c> porte la vitesse verticale (<b>-1</b> au
    /// depart), <c>+0x14</c> la hauteur de repos en unites entieres. La hauteur <c>+0x20</c> avance
    /// de la vitesse a chaque image, et la vitesse <b>s'inverse</b> des que la hauteur repasse
    /// au-dessus du repos ou descend de plus de <b>20</b> en dessous. Puis, si le bit <c>0x10</c>
    /// n'est pas pose, le bit <c>0x10000000</c> est efface.</item>
    /// <item><b>Pas pose</b> — il tombe. Aux etats 0 et 1 il passe a l'etat <b>2</b>, pose
    /// <c>0x10000000</c>, arme un compte de <b>10</b> en <c>+0x12</c> et <b>joue un son</b> ; a
    /// l'etat 2, tant que le compte est positif il monte (le rebond), puis il redescend de
    /// <c>0x5000</c> par image jusqu'a repasser sous la hauteur de repos — la il se cale
    /// exactement dessus, pose <c>0x40000000</c>, revient a l'etat 1 et rearme <c>+0x16</c> a
    /// <b>-1</b> et <c>+0x12</c> a <b>100</b>.</item>
    /// </list>
    ///
    /// <para>
    /// Le son vient de l'octet <c>+0x18</c> du meme enregistrement de zone que
    /// <see cref="EntityHandler_004566C0"/> lit en <c>+0x15</c> — deux voix voisines dans une seule
    /// table par zone. Il passe par <see cref="TryPlaySoundNearEntity"/> (portee 2000), pas par le
    /// son a portee variable.
    /// </para>
    /// </summary>
    private static void EntityHandler_00457240(uint* entity)
    {
        byte* e = (byte*)entity;
        uint flags = *(uint*)e;

        if ((flags & 0x40000000) != 0)
        {
            if (*(ushort*)(e + 0x10) == 0)
            {
                *(ushort*)(e + 0x16) = 0xffff;                    // Ghidra: mov word [esi+16h],0FFFFh
                *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
                *(ushort*)(e + 0x10) = 1;
            }

            if (*(ushort*)(e + 0x10) == 1)
            {
                short speed = *(short*)(e + 0x16);
                int height = *(int*)(e + 0x20) + (speed << 0xc);
                *(int*)(e + 0x20) = height;

                int rest = *(short*)(e + 0x14);

                // Ghidra: `jge` puis `jg` — l'inversion a lieu au-dessus du repos ET vingt unites
                // en dessous ; entre les deux, la vitesse est laissee telle quelle.
                if (height >= rest << 0xc || height <= (rest - 20) << 0xc)
                    *(short*)(e + 0x16) = (short)-speed;
            }

            // Ghidra: `test al,10h` — le bit 4 du mot de tete, a ne pas confondre avec le
            // 0x10000000 que la ligne suivante efface.
            if ((flags & 0x10) == 0) *(uint*)e = flags & 0xefffffff;
            return;
        }

        if (*(ushort*)(e + 0x10) == 1 || *(ushort*)(e + 0x10) == 0)
        {
            flags |= 0x10000000;
            *(ushort*)(e + 0x10) = 2;
            *(ushort*)(e + 0x12) = 10;
            *(uint*)e = flags;

            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, sounds[0x18], 0x400, 0x1000, 0, 0);
        }

        if (*(ushort*)(e + 0x10) != 2) return;

        short countdown = *(short*)(e + 0x12);
        if (countdown > 0)
        {
            countdown--;
            *(short*)(e + 0x12) = countdown;
            *(int*)(e + 0x20) -= countdown << 0xc;
            return;
        }

        int y = *(int*)(e + 0x20) + 0x5000;
        *(int*)(e + 0x20) = y;

        int floor = *(short*)(e + 0x14) << 0xc;
        if (floor > y) return;

        *(int*)(e + 0x20) = floor;
        *(uint*)e |= 0x40000000;
        *(ushort*)(e + 0x10) = 1;
        *(ushort*)(e + 0x16) = 0xffff;
        *(ushort*)(e + 0x12) = 0x64;
    }

    /// <summary>
    /// Ghidra: FUN_00457140 @ 0x00457140 — <b>le sous-type 23</b> : le <b>frere d'echelle</b> de
    /// <see cref="EntityHandler_004571D0"/>. 142 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme amorcage exactement — phase <c>rand() % 0x1000</c> en <c>+0x12</c>, vitesse
    /// <c>rand() % 50 + 50</c> en <c>+0x14</c> — mais le sinus ne part pas dans une rotation : il
    /// module l'<b>echelle</b> <c>+0x28</c> et <c>+0x2c</c>, autour de <c>0xED8</c> et a
    /// <c>&gt;&gt; 4</c> au lieu de <c>&gt;&gt; 5</c>. Le decor respire au lieu de balancer.
    /// </para>
    /// <para>
    /// L'amorcage pose en plus <c>0x1A</c> dans les trois octets <c>+0x38</c>, <c>+0x39</c> et
    /// <c>+0x3a</c> — une teinte, ecrite entre le <c>idiv</c> et son <c>add</c>, ce que seul le
    /// desassemblage montre.
    /// </para>
    /// <para>
    /// <b>Le binaire appelle la table de sinus deux fois avec la meme phase</b> : elle n'est
    /// avancee qu'apres. Les deux axes recoivent donc la meme valeur, et un seul calcul suffit ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_00457140(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

            // Ghidra: `mov al,1Ah` puis trois `mov [esi+3xh],al`, glisses entre le idiv et son add.
            e[0x38] = 0x1a;
            e[0x39] = 0x1a;
            e[0x3a] = 0x1a;

            *(short*)(e + 0x14) = (short)(CrtRand() % 0x32 + 0x32);
        }

        if (*(ushort*)(e + 0x10) == 1)
        {
            short breath = (short)(((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) >> 4) + 0xed8);
            *(short*)(e + 0x28) = breath;
            *(short*)(e + 0x2c) = breath;
            *(ushort*)(e + 0x12) = (ushort)((*(ushort*)(e + 0x14) + *(ushort*)(e + 0x12)) & 0xfff);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00456E80 @ 0x00456e80 — <b>le sous-type 25</b> : un decor qui <b>lance un script
    /// sur le meneur</b> a chaque image, et s'eteint au bout de trente. 183 octets, absente de
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>L'etat 0 ne se pose pas lui-meme a 1.</b> Il remet <c>+0x12</c> a zero et lance le script
    /// <b>1</b>, puis la fonction teste <c>etat == 1</c> — qui est encore faux. C'est le script qui
    /// fait avancer l'etat, par un opcode ALU sur l'entite. Transcrit tel quel : ce n'est pas un
    /// oubli du binaire, c'est la facon dont ces decors delegent leur machine a etats au script.
    /// </para>
    /// <para>
    /// A l'etat 1 le script <b>0</b> part a chaque image, l'orientation <c>+0x34</c> recule de
    /// <b>25</b> par image (<c>0xFFE7</c> ajoute en <c>short</c>), et au premier tour seulement un
    /// son part et le bit <c>0x2000</c> est pose (<c>or ah,20h</c> — le deuxieme octet, pas
    /// <c>0x20</c>). Au trentieme tour l'etat passe a <b>0x63</b>, une valeur qu'aucune branche de
    /// cette fonction ne relit : le decor est fini.
    /// </para>
    /// <para>Le membre vise est toujours le meneur, <c>s_SlotFlags_005e93c0 + DAT_005de538 *
    /// 0x9c</c> — le binaire l'ecrit <c>(a * 5 &lt;&lt; 3 - a) * 4</c>, soit <c>a * 0x9c</c>.</para>
    /// </summary>
    private static void EntityHandler_00456E80(uint* entity)
    {
        byte* e = (byte*)entity;
        byte* leader = Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x12) = 0;
            RunEntityScript(s_Unknown_005e97cc, leader, 1, nameof(EntityHandler_00456E80));
        }

        if (*(ushort*)(e + 0x10) != 1) return;

        RunEntityScript(s_Unknown_005e97cc, leader, 0, nameof(EntityHandler_00456E80));

        if (*(ushort*)(e + 0x12) == 0)
        {
            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, sounds[0], 0x200, 0x1000, 0, 0);
            *(uint*)e |= 0x2000;                        // Ghidra: or ah,20h
        }

        *(short*)(e + 0x34) += unchecked((short)0xffe7);
        *(ushort*)(e + 0x12) += 1;
        if (*(ushort*)(e + 0x12) == 0x1e) *(ushort*)(e + 0x10) = 0x63;
    }

    /// <summary>
    /// Ghidra: FUN_00457EC0 @ 0x00457ec0 — <b>le sous-type 9</b> : une <b>dalle de pression</b>.
    /// 256 octets, absente de <c>Blaze.c</c>. C'est le premier decor transcrit qui <b>ecrit dans la
    /// memoire de script</b>, et le seul appelant connu de
    /// <see cref="CountPartyMembersMatching"/>.
    ///
    /// <para>
    /// A l'amorcage la dalle retient sa hauteur de repos en <c>+0x12</c>, en unites entieres.
    /// Ensuite, a chaque image, elle compte les membres du groupe poses sur elle — ceux dont
    /// <c>+0x78</c> la designe, qui portent <c>0x10</c> et pas <c>0x40000000</c> :
    /// </para>
    /// <list type="bullet">
    /// <item>au moins un, et l'etat 1 passe a <b>2</b> — enfoncee ;</item>
    /// <item>aucun, et l'etat 1 passe a <b>3</b> — relachee.</item>
    /// </list>
    ///
    /// <para>
    /// <b>L'etat 2 pose un bit dans le banc <c>0x005e96e0</c></b>, celui de l'opcode 4 de
    /// l'interpreteur : <c>+0x16</c> est un numero de bit global, l'octet est <c>+0x16 / 8</c> et le
    /// bit <c>+0x16 % 8</c>. Un numero au-dela de <c>0x1f</c> (donc 256 bits) est ignore, ce qui
    /// borne l'ecriture aux <c>0x20</c> premiers octets du banc et non a ses <c>0x40</c>. La dalle
    /// s'enfonce de <c>+0x14</c> et l'etat retombe a 1.
    /// </para>
    /// <para>
    /// <b>L'etat 3 remonte</b> la dalle a sa hauteur de repos, rearme le verrou de son
    /// (<c>+0x98</c>) et repasse a 1 — mais <b>n'efface pas le bit</b>. Le bit reste donc pose une
    /// fois la dalle foulee : c'est un declencheur a sens unique, ce que le script relit ensuite.
    /// </para>
    /// <para>
    /// La division par huit est signee (<c>cdq ; and edx,7 ; add ; sar</c>, l'arrondi vers zero) et
    /// la borne est comparee en <b>16 bits signes</b> (<c>cmp ax,1Fh ; jg</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_00457EC0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(ushort*)(e + 0x98) = 0;
        }

        int standing = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (standing != 0)
        {
            if (*(ushort*)(e + 0x10) == 1) *(ushort*)(e + 0x10) = 2;
        }
        else if (*(ushort*)(e + 0x10) == 1)
        {
            *(ushort*)(e + 0x10) = 3;
        }

        if (*(ushort*)(e + 0x10) == 2)
        {
            if (*(ushort*)(e + 0x98) == 0)
            {
                byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
                TryPlaySoundNearEntity(entity, sounds[0x12], 0x400, 0x1000, 0, 0);
                *(ushort*)(e + 0x98) = 1;
            }

            int bit = *(short*)(e + 0x16);
            *(ushort*)(e + 0x10) = 1;
            *(int*)(e + 0x20) = (*(short*)(e + 0x12) - *(short*)(e + 0x14)) << 0xc;

            // Ghidra: `cdq ; and edx,7 ; add eax,edx ; sar eax,3` — la division signee par 8.
            int index = bit / 8;
            if ((short)index <= 0x1f)
                Mem.s_StateBank0_005e96e0[index] |= (byte)(1 << (bit - index * 8));
        }

        if (*(ushort*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
            *(ushort*)(e + 0x98) = 0;
            *(ushort*)(e + 0x10) = 1;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00457FC0 @ 0x00457fc0 — <b>le sous-type 34</b> : un decor qui <b>tremble sur
    /// place</b>. 172 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// L'amorcage <b>recopie la position</b> (<c>+0x1c</c>, <c>+0x20</c>, <c>+0x24</c>) dans
    /// <c>+0x44</c>, <c>+0x48</c>, <c>+0x4c</c>. C'est ce qui rend la secousse repetable : chaque
    /// image repart de l'original, jamais du tremblement precedent, donc le decor ne derive pas.
    /// </para>
    /// <para>
    /// Chaque axe recoit ensuite <c>(rand() % 50 - 25) &lt;&lt; 12</c> — une secousse centree, de
    /// plus ou moins vingt-cinq unites. L'octet <c>+0x40</c> prend <c>rand() % 4</c>, avec l'idiome
    /// du <b>reste signe</b> (<c>cdq ; xor ; sub ; and 3 ; xor ; sub</c>) et non un masque.
    /// </para>
    /// <para><c>+0x12</c> est mis a zero a l'amorcage et n'est relu par aucune branche.</para>
    /// </summary>
    private static void EntityHandler_00457FC0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
            *(ushort*)(e + 0x10) = 1;
            *(ushort*)(e + 0x12) = 0;
        }

        if (*(ushort*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) = *(int*)(e + 0x44);
        *(int*)(e + 0x20) = *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c);

        *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) << 0xc;
        *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) << 0xc;

        e[0x40] = (byte)(CrtRand() % 4);
    }

    /// <summary>
    /// Ghidra: FUN_00456F40 @ 0x00456f40 — <b>le sous-type 24</b> : quelque chose qui <b>traverse
    /// la scene, tombe, eclate, et recommence</b>. 234 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>Quatre etats, dont un porte le numero <b>0x63</b> :</para>
    /// <list type="bullet">
    /// <item><b>0x63</b> — l'attente. <c>+0x16</c> compte a rebours depuis <c>rand() % 100</c>, et
    /// le bit <c>0x4000</c> (invisible) reste pose. Il tombe a l'etat 1 quand le compte passe sous
    /// zero, en effacant le bit ;</item>
    /// <item><b>1</b> — la traversee. <c>+0x1c</c> avance de <c>+0x12</c> par image et
    /// <c>+0x32</c> tourne de <c>+0x14</c>, une image sur deux au hasard laisse une trainee
    /// (sous-type d'effet <c>0x32</c>). Passe a l'etat 2 quand <c>+0x1c</c> depasse
    /// <c>0xFF042000</c> ;</item>
    /// <item><b>2</b> — la chute. Meme avance horizontale, plus une descente qui <b>accelere</b>
    /// (<c>+0x16</c> gagne un par image et sert de vitesse), et une orientation prise dans la table
    /// de sinus decalee d'un quart de tour. En bas — <c>+0x20</c> sous <c>0xFFEA2000</c> — il
    /// <b>eclate en neuf effets</b> de sous-type <c>0x33</c> et passe a l'etat 3 ;</item>
    /// <item><b>3</b> — l'effacement. L'echelle des trois axes perd 100 par image ; quand elle
    /// passe sous zero, tout est remis a neuf a une position fixe et l'etat repart a 0.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Les trois comparaisons de position sont signees</b> et leurs constantes negatives
    /// (<c>0xFF042000</c>, <c>0xFFEA2000</c>) : les lire en <c>uint</c> inverserait les deux
    /// transitions.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456F40(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 0x63;
            *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0xf);

            int wait = CrtRand() % 0x64;
            *(uint*)e |= 0x4000;                          // Ghidra: or ah,40h
            *(short*)(e + 0x16) = (short)wait;
        }

        if (*(ushort*)(e + 0x10) == 0x63)
        {
            *(short*)(e + 0x16) -= 1;
            if (*(short*)(e + 0x16) < 0)
            {
                *(ushort*)(e + 0x10) = 1;
                *(uint*)e &= 0xffffbfff;                  // Ghidra: and ah,0BFh
            }
        }

        if (*(ushort*)(e + 0x10) == 1)
        {
            if (CrtRand() % 2 == 1) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);

            int x = *(int*)(e + 0x1c) + (*(short*)(e + 0x12) << 0xc);
            *(int*)(e + 0x1c) = x;

            *(short*)(e + 0x32) += *(short*)(e + 0x14);
            if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;

            if (x > unchecked((int)0xff042000))
            {
                *(ushort*)(e + 0x10) = 2;
                *(short*)(e + 0x16) = 5;
                return;
            }
        }

        if (*(ushort*)(e + 0x10) == 2)
        {
            if (CrtRand() % 2 == 1) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);

            *(int*)(e + 0x1c) += *(short*)(e + 0x12) << 0xc;
            *(short*)(e + 0x32) += *(short*)(e + 0x14);
            *(int*)(e + 0x20) -= *(short*)(e + 0x16) << 0xc;

            // Ghidra: `add edx,400h ; call 00545A1C ; sar eax,8` — le sinus decale d'un quart de
            // tour sur une table de 0x1000, c'est-a-dire un cosinus.
            *(short*)(e + 0x34) +=
                (short)((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + 0x400) >> 8);

            *(short*)(e + 0x16) += 1;

            if (*(int*)(e + 0x20) < unchecked((int)0xffea2000))
            {
                for (int i = 0; i < 9; i++) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);
                *(ushort*)(e + 0x10) = 3;
                return;
            }
        }

        if (*(ushort*)(e + 0x10) != 3) return;

        *(short*)(e + 0x28) += -100;
        *(short*)(e + 0x2a) += -100;
        *(short*)(e + 0x2c) += -100;

        short scale = *(short*)(e + 0x28);
        *(int*)(e + 0x20) -= *(short*)(e + 0x16) << 0xc;
        *(short*)(e + 0x16) += 1;

        if (scale >= 0) return;

        *(ushort*)(e + 0x10) = 0;
        *(uint*)e |= 0x4000;
        *(int*)(e + 0x1c) = unchecked((int)0xfea84000);
        *(int*)(e + 0x20) = 0x226000;
        *(short*)(e + 0x34) = 0;
        *(ushort*)(e + 0x28) = 0x1000;
        *(ushort*)(e + 0x2a) = 0x1000;
        *(ushort*)(e + 0x2c) = 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_004746F0 @ 0x004746f0 — <b>le sous-type 26</b> : un decor qui <b>attend un bit de
    /// script</b>, et pulse en attendant. 242 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>C'est le pendant de la dalle</b> (<see cref="EntityHandler_00457EC0"/>) : celle-la
    /// <i>ecrit</i> un bit dans le banc <c>0x005e96e0</c>, celui-ci en <i>lit</i> un dans le banc
    /// <c>0x005e9840</c>, avec la meme arithmetique — octet <c>n / 8</c>, bit <c>n % 8</c>, division
    /// signee. Des que le bit est pose, l'etat passe a <b>2</b> et le decor devient invisible pour
    /// de bon (<c>0x4000</c> repose a chaque image).
    /// </para>
    /// <para>
    /// <b>Le champ <c>+0x14</c> sert a deux choses successivement</b>, et c'est le seul endroit du
    /// lot ou cela arrive : a l'amorcage il porte le <b>numero de bit</b>, recopie dans
    /// <c>+0x16</c> ; plus tard il devient la <b>vitesse</b> du pulse (<c>0x200</c>, puis
    /// <c>0xFC00</c> soit -1024 quand l'echelle depasse <c>0x1000</c>). Le relire comme un seul
    /// champ ferait un decor qui ne clignote jamais.
    /// </para>
    /// <para>
    /// Tant que le bit n'est pas pose, une image sur treize environ (<c>rand() % 13 == 1</c>) lance
    /// un pulse : l'echelle <c>+0x28</c>/<c>+0x2a</c> part de zero, monte, redescend, et le decor
    /// se cache a nouveau des qu'elle repasse sous zero. <c>+0x40</c> cycle sur 0, 1, 2 pendant le
    /// pulse.
    /// </para>
    /// </summary>
    private static void EntityHandler_004746F0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            short bit = *(short*)(e + 0x14);
            *(ushort*)(e + 0x10) = 1;
            *(ushort*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = bit;
        }

        // Ghidra: LAB_00474710 — la lecture du bit, hors de toute branche d'etat.
        int watched = *(short*)(e + 0x16);
        if (watched != 0)
        {
            int index = watched / 8;                       // division signee, comme la dalle
            if (((Mem.s_StateBank3_005e9840[(short)index] >> (watched - index * 8)) & 1) != 0)
            {
                *(ushort*)(e + 0x10) = 2;
                *(uint*)e |= 0x4000;
            }
        }

        if (*(ushort*)(e + 0x10) == 1)
        {
            if (*(ushort*)(e + 0x12) == 0)
            {
                *(uint*)e |= 0x4000;                       // Ghidra: or ch,40h

                int roll = CrtRand() % 0xd;
                if (roll == 1)
                {
                    *(short*)(e + 0x12) = (short)roll;
                    *(uint*)e &= 0xffffbfff;               // Ghidra: and ah,0BFh
                    *(ushort*)(e + 0x28) = 0;
                    *(ushort*)(e + 0x2a) = 0;
                    *(ushort*)(e + 0x14) = 0x200;
                }
            }
            else
            {
                short step = *(short*)(e + 0x14);
                *(short*)(e + 0x28) += step;
                short scale = *(short*)(e + 0x28);
                *(short*)(e + 0x2a) += step;

                if (scale < 0)
                {
                    *(ushort*)(e + 0x12) = 0;
                    *(uint*)e |= 0x4000;
                }

                if (scale > 0x1000) *(ushort*)(e + 0x14) = 0xfc00;   // -1024

                e[0x40] += 1;
                if (e[0x40] == 3) e[0x40] = 0;
            }
        }

        if (*(ushort*)(e + 0x10) == 2) *(uint*)e |= 0x4000;
    }

    /// <summary>
    /// Ghidra: FUN_00474380 @ 0x00474380 — <b>le sous-type 15</b> : la <b>scenette de sortie</b>.
    /// 400 octets, absente de <c>Blaze.c</c>. C'est de loin le plus gros des gestionnaires de la
    /// foret, et le seul qui prenne la main sur la camera et sur la musique.
    ///
    /// <para>Cinq etats, le dernier numerote <b>0x63</b> :</para>
    /// <list type="number">
    /// <item><b>0</b> — pose <c>+0x12</c> a 1 et cache l'entite. Rien d'autre ; c'est l'etat 1 que
    /// le script doit poser.</item>
    /// <item><b>1</b> — <b>detourne la camera</b>. Le bloc <c>0x00571980</c> recoit la position de
    /// l'entite, deux angles bruites (<c>rand() % 0x190</c> et <c>rand() % 0xC8</c>, ajoutes a
    /// <c>+0x30</c> et <c>+0x32</c>), l'orientation <c>+0x34</c>, et une copie de la cible de
    /// camera courante ; puis les <b>trois globales de camera</b> sont rebranchees dessus. Le mode
    /// de vue est sauve dans <c>DAT_00604b7c</c> et force a <b>3</b>. L'entite devient visible,
    /// echelle 0, et l'etat passe a 2 avec un son.</item>
    /// <item><b>2</b> — l'entite <b>grandit</b> de 0x78 par image jusqu'a l'unite, puis etat 3 avec
    /// un second son.</item>
    /// <item><b>3</b> — l'axe <c>+0x2a</c> <b>s'ecrase</b> a vitesse croissante (<c>+0x12</c> gagne
    /// 15 par image) jusqu'a repasser sous l'unite, puis etat 4 avec un troisieme son.</item>
    /// <item><b>4</b> — le <b>fondu</b> : les trois octets de <c>DAT_005de028</c> montent de 10 par
    /// image pendant huit images, puis redescendent de 5, sur vingt et une images en tout. Au bout,
    /// tout est rendu : camera d'origine, entites degelees, musique de la sous-zone relancee, et
    /// les quatre membres du groupe recoivent un effet <c>0x59</c>.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Deux couples zone/sous-zone court-circuitent la fin</b> — (3, 8) et (4, 6), testes
    /// <b>deux fois</b>, avant et apres le changement de musique. Le second test n'est pas
    /// redondant : <see cref="PlayMusicTrack"/> peut avoir change la sous-zone entre-temps. Ces
    /// deux couples sortent donc sans reveiller le groupe.
    /// </para>
    /// <para>
    /// <c>DAT_005de028</c> est declare <c>uint</c> dans <see cref="GameMemory"/> ; le binaire
    /// l'ecrit <b>octet par octet</b> (<c>0x5de028</c>, <c>29</c>, <c>2a</c>, et le drapeau
    /// <c>2b</c>). Le <c>fixed</c> ci-dessous reproduit cet acces sans changer la declaration —
    /// c'est la premiere preuve que ce dword est une couleur de fondu plus un drapeau, et non un
    /// scalaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00474380(uint* entity)
    {
        byte* e = (byte*)entity;
        ushort state = *(ushort*)(e + 0x10);

        if (state == 0 && *(ushort*)(e + 0x12) == 0)
        {
            *(ushort*)(e + 0x12) = 1;
            *(uint*)e |= 0x4000;                      // Ghidra: or ch,40h
        }

        if (state == 1)
        {
            short* view = (short*)Mem.EntityStateBytes(0x00571980);
            int* target = (int*)Mem.EntityStateBytes(0x00571988);
            int* eye = (int*)Mem.EntityStateBytes(0x00571998);

            eye[0] = *(int*)(e + 0x1c);
            eye[1] = *(int*)(e + 0x20);
            eye[2] = *(int*)(e + 0x24);

            view[0] = (short)(CrtRand() % 0x190 + *(short*)(e + 0x30) + 0xc8);

            int* previous = *(int**)Mem.Entity(0x005eca94);
            view[1] = (short)(CrtRand() % 0xc8 + *(short*)(e + 0x32) + 0x200);
            view[2] = *(short*)(e + 0x34);

            target[0] = previous[0];
            target[1] = previous[1];
            target[2] = previous[2];

            int depth = CrtRand() % 0xc8;

            *(int**)Mem.Entity(0x005eca94) = target;
            *(short**)Mem.Arena(0x005e9ae0) = view;
            *(int**)Mem.Entity(0x005edebc) = eye;

            // Ghidra: `add edx,258h` puis `mov [571990h],edx` — la troisieme composante que les
            // trois lignes ci-dessus venaient de recopier est ecrasee. Fidele, pas un doublon.
            target[2] = depth + 0x258;

            Saves.s_SavedViewMode_00604b7c = s_Unknown_005de54c;
            s_Unknown_005de54c = 3;

            *(ushort*)(e + 0x2a) = 0x7d00;
            *(uint*)e &= 0xffffbfff;                  // Ghidra: and dh,0BFh
            *(ushort*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x10) = 2;

            TryPlaySoundNearEntity(entity, 0x0d, 0x400, 0x1000, 0, 0);
        }

        if (*(ushort*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x28) += 0x78;
            short grown = *(short*)(e + 0x28);
            *(short*)(e + 0x2c) += 0x78;

            if (grown >= 0x1000)
            {
                *(ushort*)(e + 0x28) = 0x1000;
                *(ushort*)(e + 0x2c) = 0x1000;
                *(ushort*)(e + 0x10) = 3;
                *(ushort*)(e + 0x12) = 0x78;
                TryPlaySoundNearEntity(entity, 0x0a, 0x2bc, 0x1000, 0, 0);
            }
        }

        if (*(ushort*)(e + 0x10) == 3)
        {
            short speed = *(short*)(e + 0x12);
            *(short*)(e + 0x2a) -= speed;
            *(short*)(e + 0x12) = (short)(speed + 0xf);

            if (*(short*)(e + 0x2a) <= 0x1000)
            {
                *(ushort*)(e + 0x2a) = 0x1000;
                *(ushort*)(e + 0x10) = 4;
                *(ushort*)(e + 0x12) = 0;
                TryPlaySoundNearEntity(entity, 0x0f, 0x400, 0x1000, 0, 0);
            }
        }

        if (*(ushort*)(e + 0x10) != 4) { FinishSceneCameraScale(e); return; }

        fixed (uint* fadeWord = &Mem.s_Unknown_005de028)
        {
            byte* fade = (byte*)fadeWord;
            fade[3] = 1;

            // Ghidra: `cmp word [esi+12h],8 ; jge` — huit images qui montent, le reste qui descend.
            if (*(short*)(e + 0x12) < 8)
            {
                fade[0] += 0x0a;
                fade[1] += 0x0a;
                fade[2] += 0x0a;
            }
            else
            {
                fade[0] += 0xfb;                      // Ghidra: mov al,0FBh — soit -5
                fade[1] += 0xfb;
                fade[2] += 0xfb;
            }

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) <= 0x14) { FinishSceneCameraScale(e); return; }

            fade[3] = 1;
            fade[0] = 0;
            fade[1] = 0;
            fade[2] = 0;
        }

        *(ushort*)(e + 0x10) = 0x63;

        *(int**)Mem.Entity(0x005eca94) = Mem.Camera(0x005de078);
        *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
        *(int**)Mem.Entity(0x005edebc) = Mem.Camera(0x005de068);
        CameraModeApply_005037E0(0);                  // Ghidra: FUN_005037b0 -> FUN_005037e0
        UnfreezeAllEntities();                        // Ghidra: FUN_00535400

        if (IsSceneCameraShortcutArea()) { FinishSceneCameraScale(e); return; }

        byte track = e[0x14];
        byte current = s_Unknown_00566c8c;
        s_Unknown_005de534 = track;
        if (current == track) { FinishSceneCameraScale(e); return; }

        s_Unknown_00566c8c = track;
        PlayMusicTrack(track);

        // Ghidra: le MEME test refait apres le changement de musique — pas une redondance, la
        // sous-zone peut avoir change entre les deux.
        if (IsSceneCameraShortcutArea()) { FinishSceneCameraScale(e); return; }

        // Ghidra: `cmp edi,5E9594h ; jle` — quatre emplacements, la borne etant inclusive.
        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0)
                SpawnEffectEntity(member, 0x59, 0, -1);
        }

        FinishSceneCameraScale(e);
    }

    /// <summary>Les deux couples zone/sous-zone qui sortent de <see cref="EntityHandler_00474380"/>
    /// sans reveiller le groupe : (3, 8) et (4, 6). Testes deux fois dans le binaire, d'ou
    /// l'extraction.</summary>
    private static bool IsSceneCameraShortcutArea()
    {
        byte area = (byte)Res.s_Unknown_005de528;
        byte sub = (byte)s_Unknown_005de52c;
        return (area == 3 && sub == 8) || (area == 4 && sub == 6);
    }

    /// <summary>La queue commune de <see cref="EntityHandler_00474380"/> : a l'etat <c>0x63</c>
    /// l'echelle des trois axes est ramenee a l'unite, quel que soit le chemin de sortie. Le
    /// binaire y arrive par <c>LAB_004746A3</c> depuis sept endroits, avec <c>EDI</c> valant
    /// <c>0x1000</c> sur tous.</summary>
    private static void FinishSceneCameraScale(byte* e)
    {
        if (*(ushort*)(e + 0x10) != 0x63) return;

        *(ushort*)(e + 0x28) = 0x1000;
        *(ushort*)(e + 0x2a) = 0x1000;
        *(ushort*)(e + 0x2c) = 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_00457BF0 @ 0x00457bf0 — <b>le sous-type 16</b> : un projectile qui <b>part en
    /// cloche, clignote et s'eteint en cinq echos</b>. 503 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Il ne fait rien du tout tant qu'un bit de script n'est pas pose.</b> La toute premiere
    /// instruction lit <c>0x005e96fb</c> — l'octet <c>0x1b</c> du banc <c>0x005e96e0</c>, celui de
    /// l'opcode 4 — et rend la main si le bit 0 est a zero. C'est le meme banc que la dalle
    /// <see cref="EntityHandler_00457EC0"/> ecrit : voici l'autre bout du fil.
    /// </para>
    /// <para>
    /// L'amorcage tire <b>six</b> valeurs au hasard : deux vitesses horizontales centrees
    /// (<c>(rand() % 16 - 8) &lt;&lt; 12</c>), une vitesse verticale positive
    /// (<c>(rand() % 15 + 5) &lt;&lt; 12</c>), deux vitesses de rotation (<c>rand() % 30</c>) et
    /// une duree de vie (<c>rand() % 10 + 20</c>).
    /// </para>
    /// <para>
    /// Ensuite, a chaque image : la position avance des trois vitesses, la verticale perd
    /// <c>0x1000</c> — c'est la <b>gravite</b> —, les deux angles tournent, et le compteur
    /// <c>+0x5c</c> avance. Passe la duree de vie, le bit <c>0x80000000</c> tombe. Et le decor
    /// <b>clignote une image sur deux</b> : <c>+0x5c</c> pair, il est cache.
    /// </para>
    /// <para>
    /// <b>Les echos sont une table de sauts</b> (<c>0x00457df0</c>, six branches, indexee par la
    /// table d'octets <c>0x00457e08</c>) : <c>+0x98</c> vaut 0, 3, 6, 9, 12 aux images sonores, et
    /// le gain descend <c>0x1800 → 0x1000 → 0x0B00 → 0x0800 → 0x400</c>. Tout le reste tombe sur la
    /// branche 5, qui ne joue rien. Un son toutes les trois images, de plus en plus faible.
    /// </para>
    /// <para>
    /// La borne <c>+0x98 &gt; 12</c> est testee <b>non signee</b> (<c>movsx</c> puis <c>ja</c>) :
    /// une valeur negative depasse la table au lieu de tomber dessous. Transcrit tel quel.
    /// </para>
    /// <para>Au premier tour de l'etat 1, seize effets de sous-type <c>0x31</c> naissent d'un
    /// coup — la gerbe de depart.</para>
    /// </summary>
    private static void EntityHandler_00457BF0(uint* entity)
    {
        // Ghidra: `mov al,[5E96FBh] ; test al,1 ; je` — le bit 0 de l'octet 0x1b du banc 0.
        if ((Mem.s_StateBank0_005e96e0[0x1b] & 1) == 0) return;

        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(int*)(e + 0x50) = (CrtRand() % 0x10 - 8) << 0xc;
            *(int*)(e + 0x58) = (CrtRand() % 0x10 - 8) << 0xc;
            *(int*)(e + 0x54) = (CrtRand() % 0xf + 5) << 0xc;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1e);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1e);

            int life = CrtRand() % 0xa;
            *(int*)(e + 0x5c) = 0;
            *(ushort*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = (short)(life + 0x14);
            *(uint*)e |= 0x2000;                         // Ghidra: or ah,20h
        }

        if (*(ushort*)(e + 0x10) != 1) return;

        if (*(ushort*)(e + 0x98) == 0)
        {
            for (int i = 0; i < 0x10; i++) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);
        }

        // Ghidra: `movsx eax,word [esi+98h] ; cmp eax,0Ch ; ja` — la comparaison est NON SIGNEE.
        uint step = (uint)(int)*(short*)(e + 0x98);
        if (step <= 0xc)
        {
            int gain = EchoGainTable_00457DF0[EchoStepTable_00457E08[step]];
            if (gain != 0)
            {
                byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
                TryPlaySoundNearEntity(entity, sounds[0x1c], 0x400, (short)gain, 0, 0);
            }
        }

        // Ghidra: LAB_00457D82 — la queue commune, atteinte aussi bien depuis la branche muette.
        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(ushort*)(e + 0x98) += 1;
        *(int*)(e + 0x54) += unchecked((int)0xfffff000);   // la gravite : -0x1000 par image
        *(short*)(e + 0x32) += *(short*)(e + 0x12);
        *(short*)(e + 0x34) += *(short*)(e + 0x14);

        int age = *(int*)(e + 0x5c) + 1;
        *(int*)(e + 0x5c) = age;
        if (age > *(short*)(e + 0x16)) *(uint*)e &= 0x7fffffff;

        // Ghidra: `test al,1` sur l'octet bas du compteur — une image sur deux, l'entite est cachee.
        if ((age & 1) == 0) *(uint*)e |= 0x4000;
        else *(uint*)e &= 0xffffbfff;
    }

    /// <summary>Ghidra: <c>0x00457df0</c> — les six branches de la table de sauts de
    /// <see cref="EntityHandler_00457BF0"/>, reduites a ce qui les distingue : le <b>gain</b>. Les
    /// cinq premieres ne different que par cette constante ; la sixieme (<c>0x00457d82</c>) saute
    /// l'appel, d'ou le zero.</summary>
    private static readonly int[] EchoGainTable_00457DF0 = [0x1800, 0x1000, 0x0b00, 0x0800, 0x0400, 0];

    /// <summary>Ghidra: <c>0x00457e08</c> — treize octets relevés dans le binaire, l'index de
    /// branche pour chaque valeur de <c>+0x98</c> de 0 a 12.</summary>
    private static readonly byte[] EchoStepTable_00457E08 =
        [0x00, 0x05, 0x05, 0x01, 0x05, 0x05, 0x02, 0x05, 0x05, 0x03, 0x05, 0x05, 0x04];

    /// <summary>
    /// Ghidra: FUN_00456750 @ 0x00456750 — <b>le sous-type 30</b> : le <b>radeau</b>. 734 octets,
    /// absente de <c>Blaze.c</c>. C est le gestionnaire le plus lie au groupe de tout le lot : il
    /// ne demarre que si <b>tout</b> le groupe est monte dessus, et il pilote la vue pendant la
    /// traversee.
    ///
    /// <para><b>La condition de depart tient en quatre tests</b>, et les quatre doivent passer :</para>
    /// <list type="number">
    /// <item>le nombre de membres <b>vivants</b> (bit <c>0x80000000</c>, sans <c>0x50000000</c>)
    /// egale le nombre de membres <b>poses sur l'entite</b> (<see cref="CountPartyMembersMatching"/>),
    /// et ce nombre n'est pas nul — donc <b>tout le monde est a bord</b> ;</item>
    /// <item>les bits <b>2 et 4</b> de l'octet <c>0x1f</c> du banc <c>0x005e96e0</c> sont poses —
    /// deux drapeaux de script, verifies separement ;</item>
    /// <item>tous les membres a bord ont leur <c>+0x24</c>, tronque au millier, <b>sous
    /// 0x14000</b> — ils sont assez bas, c'est-a-dire reellement sur le plancher.</item>
    /// </list>
    ///
    /// <para>
    /// Alors seulement l'etat passe a <b>2</b>, l'octet de tete du banc <c>0x005e9060</c> recoit
    /// <b>0x63</b> et le script <b>0</b> part sur le meneur. L'etat 2 est une <b>salle
    /// d'attente</b> : il regarde ce que le script a fait de cet octet — <b>0</b> et la traversee
    /// commence (etat 1), <b>1</b> et il verifie que personne n'est descendu, sans quoi tout est
    /// annule (retour a l'etat 0).
    /// </para>
    ///
    /// <para>
    /// <b>L'etat 1 est la traversee.</b> A chaque image, le radeau republie sa pose dans les cinq
    /// blocs de vue (<c>0x005f7d70</c>, <c>0x005e9a30</c>, <c>0x005e9b38</c>, <c>0x005f7b48</c>) —
    /// la meme rafale que le chargement de zone —, avance de <c>+0x5c</c> unites le long de son cap
    /// <c>+0x32</c> (sinus sur Z, cosinus sur X), et <b>accelere</b> jusqu'a <c>0x0a</c>.
    /// </para>
    /// <para>
    /// <b>Le cap change a quatre moments precis</b>, lus sur le compteur d'images <c>+0x9a</c> :
    /// <c>-4</c> entre 0x19 et 0x21, <c>+4</c> entre 0x23 et 0x2b, <c>+3</c> entre 0x6c et 0x75,
    /// <c>-3</c> entre 0x78 et 0x81. Les quatre plages sont disjointes : c'est un <b>trajet
    /// ecrit</b>, deux virages a l'aller et deux au retour, pas une navigation.
    /// </para>
    /// <para>
    /// A l'image <b>0xc8</b> — la 200e — la traversee finit : l'etat passe a <c>0x63</c> et le
    /// script <b>1</b> part sur chacun des quatre membres. Et a chaque image, deux effets
    /// <c>0x30</c> naissent, plus un troisieme une fois sur six : le sillage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456750(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0) StartRaftIfPartyAboard(e);

        if (*(ushort*)(e + 0x10) == 2)
        {
            byte* gate = Mem.s_StateBank2_005e9060;

            if (*gate == 0) *(ushort*)(e + 0x10) = 1;

            if (*gate == 1 && CountLivingPartyMembers() != CountPartyMembersMatching(e, 0x10, 0x40000000))
                *(ushort*)(e + 0x10) = 0;
        }

        if (*(ushort*)(e + 0x10) != 1) return;

        FinalizeEntityPlacement2(entity);          // Ghidra: FUN_00533940

        // Ghidra: la meme rafale de publication de pose que le chargement de zone.
        for (int i = 0; i < 4; i++)
            *(uint*)Mem.Entity(0x005f7d70 + i * 4) = *(uint*)(e + 0x80 + i * 4);

        *(int*)Mem.Arena(0x005e9a30) = *(int*)(e + 0x1c);
        *(int*)Mem.Arena(0x005e9a34) = *(int*)(e + 0x20);
        *(int*)Mem.Arena(0x005e9a38) = *(int*)(e + 0x24);
        *(short*)Mem.Arena(0x005e9b38) = *(short*)(e + 0x90);
        *(uint*)Mem.Entity(0x005f7b48) = *(uint*)(e + 0x7c);

        int speed = *(int*)(e + 0x5c) + 1;
        *(ushort*)(e + 0x9a) += 1;
        *(int*)(e + 0x5c) = speed;
        if (speed >= 0xa) *(int*)(e + 0x5c) = 0xa;

        short heading = *(short*)(e + 0x32);
        *(int*)(e + 0x24) -= (int)Winsys3d.GetSinTableEntry(heading) * *(int*)(e + 0x5c);

        short turn = *(short*)(e + 0x12);
        *(short*)(e + 0x32) += turn;
        *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(heading) * *(int*)(e + 0x5c);

        // Ghidra: quatre plages disjointes sur le compteur d'images. La premiere reecrit +0x12
        // depuis la valeur lue plus haut ; les trois autres ajoutent en memoire. Meme effet, les
        // plages ne se recouvrent pas — transcrit dans les deux formes du binaire.
        short frame = *(short*)(e + 0x9a);
        if (frame >= 0x19 && frame <= 0x21) *(short*)(e + 0x12) = (short)(turn - 4);
        if (frame >= 0x23 && frame <= 0x2b) *(short*)(e + 0x12) += 4;
        if (frame >= 0x6c && frame <= 0x75) *(short*)(e + 0x12) += 3;
        if (frame >= 0x78 && frame <= 0x81) *(short*)(e + 0x12) += -3;

        if (frame == 0xc8)
        {
            *(ushort*)(e + 0x10) = 0x63;

            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0)
                    RunEntityScript(s_Unknown_005e97cc, member, 1, nameof(EntityHandler_00456750));
            }
        }

        // Ghidra: LAB_004569EE — deux tours, avec un troisieme effet une fois sur six par tour.
        for (int i = 0; i < 2; i++)
        {
            SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
            if (CrtRand() % 6 == 1) SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
        }
    }

    /// <summary>La condition de depart du radeau (<see cref="EntityHandler_00456750"/>), extraite
    /// parce qu'elle est un bloc a elle seule : quatre tests, et le premier qui echoue laisse
    /// l'etat a zero.</summary>
    private static void StartRaftIfPartyAboard(byte* e)
    {
        *(int*)(e + 0x5c) = 0;
        *(ushort*)(e + 0x9a) = 0;
        *(ushort*)(e + 0x12) = 0;

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);
        if (aboard == 0) return;
        if (CountLivingPartyMembers() != aboard) return;

        // Ghidra: `mov cl,[5E96FFh]` puis deux `test` separes — l'octet 0x1f du banc 0.
        byte gates = Mem.s_StateBank0_005e96e0[0x1f];
        if ((gates & 4) == 0) return;
        if ((gates & 8) == 0) return;

        int lowEnough = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if ((*(int*)(member + 0x24) & unchecked((int)0xfffff000)) < 0x14000) lowEnough++;
        }

        if (lowEnough != aboard) return;

        *(ushort*)(e + 0x10) = 2;
        *Mem.s_StateBank2_005e9060 = 0x63;

        RunEntityScript(
            s_Unknown_005e97cc,
            Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c,
            0,
            nameof(StartRaftIfPartyAboard));
    }

    /// <summary>Les membres du groupe qui portent <c>0x80000000</c> et aucun bit de
    /// <c>0x50000000</c> — la boucle de quatre que <see cref="EntityHandler_00456750"/> ecrit trois
    /// fois en clair.</summary>
    private static int CountLivingPartyMembers()
    {
        int count = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) count++;
        }

        return count;
    }

    /// <summary>
    /// Ghidra: FUN_00457370 @ 0x00457370 — <b>le sous-type 18</b> : l'<b>ascenseur</b>. 946 octets,
    /// absente de <c>Blaze.c</c>. Le plus gros gestionnaire de la foret, et le plus lu : sa
    /// difficulte n'est pas sa taille mais le fait que <b>tout y est double</b>.
    ///
    /// <para>
    /// <c>+0x44</c> porte le <b>sens</b>, recopie une fois pour toutes depuis <c>+0x14</c> au tout
    /// premier tour (garde par <c>+0x16</c>). Zero, et la plate-forme va de <c>0x14000</c> a
    /// <c>0x258000</c> ; non nul, de <c>0xFFF60000</c> a <c>0xDC000</c>. <b>Chacune des cinq
    /// decisions de la fonction est ecrite deux fois dans le binaire</b>, une par sens, avec les
    /// deux constantes. Ici les deux jeux sont choisis d'avance et le corps n'est ecrit qu'une
    /// fois — c'est de la reconstruction de flot, pas une simplification : les branches sont
    /// identiques a la constante pres, verifie instruction par instruction.
    /// </para>
    ///
    /// <para><b>L'appel</b>. Un balayage des quatre membres cherche qui est a moins de
    /// <c>0x4b0</c> de la plate-forme (meme <c>SquareRoot0</c> que la famille des sons) et
    /// <b>plus bas qu'elle de 0x28000</b> : celui-la l'appelle, et l'etat passe a <c>0x64</c>, la
    /// descente. Le meme balayage compte au passage les membres deja arrives en haut.</para>
    ///
    /// <para><b>Le depart</b>. La plate-forme ne monte que si le nombre de passagers
    /// (<see cref="CountPartyMembersMatching"/> avec le masque <c>0x20</c>, et non <c>0x10</c>
    /// comme la dalle) egale le nombre d'emplacements actifs <b>moins</b> ceux restes en haut, et
    /// que cette difference n'est pas nulle. Autrement dit : <b>tout le monde qui n'est pas deja
    /// arrive est monte</b>.</para>
    ///
    /// <para><b>Les deux courses</b> sont jumelles a un signe pres — <c>+0x7000</c> par image vers
    /// le haut (etat 2), <c>-0x7000</c> vers le bas (etat 0x65) — avec un cliquetis toutes les cinq
    /// images (<c>sounds[0x17]</c> a la hauteur <c>0x400</c>) et un <b>choc d'arrivee</b> a la
    /// hauteur <c>0x200</c>, qui ne part que si la course a dure assez pour qu'au moins un
    /// cliquetis ait eu lieu (<c>+0x5c</c>).</para>
    ///
    /// <para>
    /// <c>DAT_005de06c</c> — la hauteur du groupe — rappelle la plate-forme en bas des que le
    /// groupe atteint le haut de sa course. C'est le seul endroit ou l'ascenseur regarde ailleurs
    /// que ses passagers.
    /// </para>
    /// </summary>
    private static void EntityHandler_00457370(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x16) == 0)
        {
            *(int*)(e + 0x44) = *(short*)(e + 0x14);
            *(ushort*)(e + 0x14) = 0;
            *(ushort*)(e + 0x16) = 1;
        }

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x98) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        // Les deux jeux de constantes, choisis une fois : le binaire ecrit les cinq decisions
        // ci-dessous deux fois, une par sens.
        bool up = *(int*)(e + 0x44) == 0;
        int top = up ? 0x258000 : 0xdc000;
        int bottom = up ? 0x14000 : unchecked((int)0xfff60000);

        int arrived = 0;
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
            if (height >= top) arrived++;
            if (height >= *(int*)(e + 0x20) - 0x28000) continue;

            if (*(ushort*)(e + 0x10) == 0) *(ushort*)(e + 0x10) = 0x64;
        }

        // Ghidra: le masque est 0x20 ici, la ou la dalle prend 0x10 — deux facons distinctes
        // d'etre pose sur une entite.
        int riders = CountPartyMembersMatching(e, 0x20, 0x40000000);

        if (*(ushort*)(e + 0x14) != 0)
        {
            if (riders == 0 && *(ushort*)(e + 0x10) == 0) *(ushort*)(e + 0x14) = 0;
        }

        if (*(ushort*)(e + 0x14) == 0 && *(ushort*)(e + 0x10) == 0)
        {
            int waiting = ActivePartySlotCount_00466D70() - arrived;
            if ((short)riders == waiting && waiting != 0)
            {
                *(ushort*)(e + 0x10) = 1;
                *(ushort*)(e + 0x14) = 1;
            }
        }

        // Ghidra: LAB_004574D2 — le rappel quand un passage est deja fait, et le rappel par la
        // hauteur du groupe.
        if (*(ushort*)(e + 0x14) == 0 && *(ushort*)(e + 0x10) == 0
            && riders == ActivePartySlotCount_00466D70() && *(int*)(e + 0x20) == top)
        {
            *(ushort*)(e + 0x10) = 0x64;
            *(ushort*)(e + 0x14) = 1;
        }

        if (*Mem.Camera(0x005de06c) >= top && *(ushort*)(e + 0x10) == 0) *(ushort*)(e + 0x10) = 1;

        if (*(ushort*)(e + 0x10) == 1)
        {
            *(ushort*)(e + 0x10) = 2;
            *(ushort*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(ushort*)(e + 0x10) == 2 && RunLiftLeg(e, 0x7000, top, true)) return;

        if (*(ushort*)(e + 0x10) == 0x64)
        {
            *(ushort*)(e + 0x10) = 0x65;
            *(ushort*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(ushort*)(e + 0x10) == 0x65) RunLiftLeg(e, -0x7000, bottom, false);
    }

    /// <summary>
    /// Une course de <see cref="EntityHandler_00457370"/>, montee ou descente. Les deux moities du
    /// binaire (etats 2 et 0x65) ne different que par le signe du pas, la butee, et le sens de la
    /// comparaison qui l'atteint.
    ///
    /// <para>Le cliquetis part toutes les cinq images et arme <c>+0x5c</c> ; le choc d'arrivee ne
    /// part que si <c>+0x5c</c> vaut 1, c'est-a-dire si la course a dure assez longtemps pour
    /// qu'un cliquetis ait eu lieu. Une course d'une image est donc silencieuse aux deux bouts —
    /// ce n'est pas un oubli, c'est ce que le binaire fait.</para>
    /// </summary>
    /// <returns><c>true</c> si le binaire quitte la fonction ici sans passer par la seconde
    /// moitie.</returns>
    private static bool RunLiftLeg(byte* e, int step, int limit, bool rising)
    {
        *(ushort*)(e + 0x98) += 1;
        if (*(ushort*)(e + 0x98) == 5)
        {
            *(ushort*)(e + 0x98) = 0;
            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity((uint*)e, sounds[0x17], 0x400, 0x1000, 0, 0);
            *(int*)(e + 0x5c) = 1;
        }

        *(ushort*)(e + 0x12) += 1;
        int height = *(int*)(e + 0x20) + step;
        *(int*)(e + 0x20) = height;

        // Ghidra: `jle` a la montee, `jge` a la descente — la course continue tant que la butee
        // n'est pas franchie.
        if (rising ? height <= limit : height >= limit) return false;

        *(int*)(e + 0x20) = limit;

        if (*(int*)(e + 0x5c) == 1 && *(ushort*)(e + 0x10) != 0)
        {
            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity((uint*)e, sounds[0x17], 0x200, 0x1000, 0, 0);
        }

        *(ushort*)(e + 0x10) = 0;
        return true;
    }

    /// <summary>
    /// Ghidra: FUN_0045DA40 @ 0x0045da40 — <b>frappe les membres du groupe que l'octet de contact
    /// designe</b>. 300 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <c>+0x9</c> de la source porte <b>deux masques de quatre bits</b>, un par demi-octet, et
    /// <paramref name="selector"/> dit lequel prendre : <b>0</b> le bas, <b>1</b> le haut. Toute
    /// autre valeur est prise <b>telle quelle comme masque</b> — le selecteur sert de masque par
    /// defaut, ce que seul le desassemblage montre (l'argument et le masque partagent la meme case
    /// de pile, <c>[esp+0x20]</c>, et les deux branches ne font que l'ecraser).
    /// </para>
    /// <para>
    /// Chaque membre vivant dont le bit est pose, et dont le verrou <c>+0x96</c> est libre, prend
    /// <see cref="ApplyPercentDamage_005397B0"/>, gagne le bit <c>0x40</c> et se voit poser un
    /// verrou de <paramref name="stunFrames"/> images — ou <b>0x3c</b> quand cet argument est nul.
    /// </para>
    /// <para>
    /// <b>L'octet de contact est remis a zero en sortant</b>, et le retour est le masque des
    /// membres reellement touches. La boucle est ecrite <c>cmp bx,3 ; jle</c>, donc quatre tours.
    /// </para>
    /// </summary>
    private static ushort StrikePartyMembers_0045DA40(
        byte* source, int percent, ushort selector, ushort effectParam, int stunFrames)
    {
        byte mask = (byte)selector;
        if (selector == 0) mask = (byte)(source[9] & 0x0f);
        if (selector == 1) mask = (byte)(source[9] >> 4);

        ushort struck = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if (((mask >> i) & 1) == 0) continue;
            if (*(short*)(member + 0x96) != 0) continue;

            ApplyPercentDamage_005397B0((uint*)member, (short)percent, effectParam);

            *(uint*)member |= 0x40;
            *(ushort*)(member + 0x96) = stunFrames != 0 ? (ushort)stunFrames : (ushort)0x3c;

            struck |= (ushort)(1 << i);
        }

        source[9] = 0;
        return struck;
    }

    /// <summary>
    /// Ghidra: FUN_00456CF0 @ 0x00456cf0 — <b>le sous-type 27</b> : un piege qui <b>tombe sur le
    /// groupe</b>. 395 octets, absente de <c>Blaze.c</c>. C'est le seul decor de la foret qui fasse
    /// des degats.
    ///
    /// <para>
    /// A l'amorcage il prend l'echelle <c>0x1194</c> sur deux axes et <b>retient trois shorts de
    /// l'arene</b> (<c>0x005e92b8</c>, <c>ba</c>, <c>bc</c>) dans <c>+0x44</c>, <c>+0x48</c>,
    /// <c>+0x4c</c> : ce sont les valeurs de l'effet d'ecran qu'il restaurera plus tard.
    /// </para>
    /// <para>
    /// <b>L'etat 1 est l'attente.</b> Il efface le bit <c>0x2000</c>, remet ses trois vitesses a
    /// zero, et compte les membres poses sur lui. Le premier tour ou quelqu'un y est, il tire
    /// <c>+0x14 = rand() % 6</c> ; des que plus personne n'y est, <c>+0x12</c> retombe a zero.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est le declenchement</b> — pose par un script, pas par cette fonction. Il vide
    /// quatre octets espaces de <c>0x4c</c> a partir de <c>0x005e918a</c>, allume <c>0x2000</c> et
    /// le bit <c>0x10</c> de <c>+0x7</c>, se donne une vitesse verticale de <c>0x64000</c>, joue
    /// <c>sounds[0x2a]</c> a gain <c>0x1800</c>, et <b>revient a l'etat 1</b>.
    /// </para>
    /// <para>
    /// <b><c>+0x16</c> est la machine de l'impact</b>, et elle non plus n'est jamais armee ici :
    /// a <b>1</b> le piege frappe (4 % des points de vie, demi-octet haut du contact, verrou de
    /// deux images), joue le son <c>3</c> et arme l'effet d'ecran <c>(0xfa, 0x3c, 0x3c, 1)</c> ;
    /// a <b>2</b> il restaure les trois valeurs retenues a l'amorcage et remet le mode a 2. Les
    /// deux etats sont donc <b>poses de l'exterieur</b>, comme l'etat 2 — ce decor est pilote par
    /// son script d'un bout a l'autre.
    /// </para>
    /// <para>L'octet de contact <c>+0x9</c> est remis a zero a chaque image, quel que soit le
    /// chemin.</para>
    /// </summary>
    private static void EntityHandler_00456CF0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(ushort*)(e + 0x28) = 0x1194;
            *(ushort*)(e + 0x2c) = 0x1194;
            *(ushort*)(e + 0x12) = 0;
            *(ushort*)(e + 0x16) = 0;

            *(int*)(e + 0x44) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x48) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x4c) = *(short*)Mem.Arena(0x005e92bc);
        }

        if (*(ushort*)(e + 0x10) == 1)
        {
            *(uint*)e &= 0xffffdfff;                       // Ghidra: and ch,0DFh
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;

            if (CountPartyMembersMatching(e, 0x10, 0x40000000) == 0)
            {
                *(ushort*)(e + 0x12) = 0;
            }
            else if (*(ushort*)(e + 0x12) == 0)
            {
                *(ushort*)(e + 0x12) = 1;
                *(short*)(e + 0x14) = (short)(CrtRand() % 6);
            }
        }

        if (*(ushort*)(e + 0x10) == 2)
        {
            // Ghidra: quatre octets espaces de 0x4c — une table dont seul ce champ est touche.
            for (int i = 0; i < 4; i++) *Mem.Arena(0x005e918a + i * 0x4c) = 0;

            *(uint*)e |= 0x2000;                           // Ghidra: or ah,20h
            e[7] |= 0x10;
            *(int*)(e + 0x54) = 0x64000;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;

            byte* sounds = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, sounds[0x2a], 0x400, 0x1800, 0, 0);

            *(ushort*)(e + 0x10) = 1;
        }

        if (*(ushort*)(e + 0x16) == 2)
        {
            *(short*)Mem.Arena(0x005e9160) = (short)*(int*)(e + 0x44);
            *(short*)Mem.Arena(0x005e9162) = (short)*(int*)(e + 0x48);
            *(short*)Mem.Arena(0x005e9164) = (short)*(int*)(e + 0x4c);
            *(short*)Mem.Arena(0x005e9166) = 2;
        }

        if (*(ushort*)(e + 0x16) == 1)
        {
            StrikePartyMembers_0045DA40(e, 4, 1, 0, 2);
            Aud.PlaySoundEffect(3, 0x400, 0x800, 0x800);   // Ghidra: FUN_0053cea0

            *(ushort*)(e + 0x16) = 2;
            *(short*)Mem.Arena(0x005e9160) = 0xfa;
            *(short*)Mem.Arena(0x005e9162) = 0x3c;
            *(short*)Mem.Arena(0x005e9164) = 0x3c;
            *(short*)Mem.Arena(0x005e9166) = 1;
        }

        e[9] = 0;
    }

    /// <summary>
    /// <b>Le crochet de contact au sol de la foret</b>, entree 32 de la table <c>0x0056afc8</c>.
    /// La zone 0 met la souche vide a cette entree ; la foret y met ceci, et c'est <b>ce que le sol
    /// fait a chaque pas</b>.
    ///
    /// <para>
    /// <b>Le binaire en porte deux exemplaires</b>, un par groupe de sous-zones :
    /// <c>FUN_00541440</c> (271 octets, blocs <c>0x005555a8</c> et <c>0x00555c28</c>) et
    /// <c>FUN_00541b80</c> (259 octets, bloc <c>0x005562a8</c>, <b>sous-zones 5 et 9</b>). Aucun des
    /// deux n'est dans <c>Blaze.c</c>. Mis cote a cote, les deux desassemblages <b>ne different que
    /// par trois instructions</b> — le <c>test dword [esi],50000000h</c>, son saut, et le <c>je</c>
    /// qui passe de court a long pour l'absorber : exactement les douze octets d'ecart. Tout le
    /// reste, registres et constantes compris, est identique. Le corps est donc ecrit une fois, et
    /// <paramref name="guardRemovedOrDead"/> porte la seule divergence. Releves sous
    /// <c>Legacy/Raw/Blaze/FUN_00541440.asm</c> et <c>FUN_00541B80.asm</c>.
    /// </para>
    /// <para>
    /// <b>Cette divergence n'est pas cosmetique.</b> En sous-zones 5 et 9, le sol brulant s'applique
    /// <b>meme a une entite retiree ou morte</b> : l'effet <c>0x2A</c> nait et le mini-script part
    /// quand meme. Ailleurs en foret, le <c>0x50000000</c> coupe court. Transcrit tel quel.
    /// </para>
    ///
    /// <para><b>Deux moities independantes</b>, sans etat ni compteur — la fonction est rejouee en
    /// entier a chaque image et ne retient rien.</para>
    ///
    /// <para>
    /// <b>La poussiere du pas.</b> L'animation courante du membre est prise dans
    /// <c>0x005e9680</c> — la table des quatre bundles d'animation, indexee par <c>+0x4</c> — puis
    /// par le clip <c>+0x18</c>. Son champ <c>+0x16</c>, <b>divise par deux</b>, donne la periode :
    /// quand l'image courante <c>+0x1a</c> tombe dessus (ou a chaque image si le bit 0 du mot de
    /// tete est pose), et si <c>+0x90</c> porte <c>0x200</c>, un effet <c>0xFE</c> nait a la
    /// position de l'entite. <b>Un pas sur deux du cycle de marche</b> : c'est la periode du clip
    /// qui donne le rythme, pas une constante.
    /// </para>
    /// <para>
    /// La division est <b>non signee</b> (<c>movsx</c> puis <c>div</c>, pas <c>idiv</c>) : une
    /// image negative devient un tres grand nombre au lieu de rester negative. Transcrit tel quel.
    /// </para>
    ///
    /// <para>
    /// <b>Le sol qui brule.</b> Si le porteur (<c>+0x7c</c>) a un <c>+0x0a</c> non nul, que
    /// l'entite n'est ni retiree ni morte (<c>0x50000000</c> — cette garde-la pour
    /// <c>FUN_00541440</c> seulement), et que les <b>trois bits bas</b> de
    /// <c>+0x150</c> du bloc de personnage valent <b>4</b> — le type de terrain —, alors un effet
    /// <c>0x2A</c> nait et un mini-script part.
    /// </para>
    /// <para>
    /// <b><c>+0x0a</c> est mis a zero le temps de la naissance</b>, puis rendu :
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> recopie l'entite entiere, donc l'effet
    /// heriterait de son orientation. Meme geste que dans
    /// <see cref="ApplyPercentDamage_005397B0"/>, et pour la meme raison.
    /// </para>
    /// <para>
    /// <b>Le mini-script est construit sur la pile</b> : sept octets
    /// <c>{0, 0, 0, porteur[0x0a], 0x0d, 4, 0xf7}</c> passes a <c>FUN_00513FE0</c>, qui n'est qu'une
    /// enveloppe autour du <b>gestionnaire de l'opcode 10</b> de l'interpreteur
    /// (<see cref="ScriptOpAluChunkRecords"/>, <c>FUN_00515790</c>) avec le contexte de zone. Le
    /// jeu ne lance donc pas un script du disque : il <b>fabrique une instruction</b> et appelle son
    /// gestionnaire directement. Le huitieme octet du tampon n'est jamais ecrit ni lu — le
    /// gestionnaire s'arrete a <c>s[6]</c>.
    /// </para>
    /// </summary>
    private static void TickGroundContact(uint* entity, bool guardRemovedOrDead)
    {
        byte* e = (byte*)entity;

        // Ghidra: `mov edx,[eax*4+5E9680h] ; mov eax,[edx+ecx*4]` — bundle du membre, puis clip.
        byte* bundle = *(byte**)(Mem.Arena(0x005e9680) + e[4] * 4);
        byte* clip = *(byte**)(bundle + *(short*)(e + 0x18) * 4);

        uint period = (uint)*(ushort*)(clip + 0x16) >> 1;
        if (period != 0)
        {
            // Ghidra: `div ecx` — division NON SIGNEE sur une valeur etendue par movsx.
            uint frame = (uint)(int)*(short*)(e + 0x1a);
            if (frame % period == 0 || (e[0] & 1) != 0)
            {
                if ((*(uint*)(e + 0x90) & 0x200) != 0)
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfe, 0x18, -1);
            }
        }

        byte* character = GetMainMemPtr() + (e[8] + 0x78) * 0x2000;
        byte* carrier = *(byte**)(e + 0x7c);

        if (*(short*)(carrier + 0x0a) == 0) return;
        if (guardRemovedOrDead && (*(uint*)e & 0x50000000) != 0) return;
        if ((character[0x150] & 7) != 4) return;

        ushort savedFacing = *(ushort*)(e + 0x0a);
        *(ushort*)(e + 0x0a) = 0;

        byte* descriptor =
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x24);

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), descriptor, 0x2a, 0, 0);

        *(ushort*)(e + 0x0a) = savedFacing;

        // Ghidra: `mov eax,[esi+7Ch]` est REFAIT apres l'appel — le porteur est relu, pas garde.
        byte* script = stackalloc byte[8];
        script[0] = 0;
        script[1] = 0;
        script[2] = 0;
        script[3] = (*(byte**)(e + 0x7c))[0x0a];
        script[4] = 0x0d;
        script[5] = 4;
        script[6] = 0xf7;

        ScriptOpAluChunkRecords(script);           // Ghidra: FUN_00513FE0 -> FUN_00515790
    }

    /// <summary>
    /// Ghidra: FUN_00541440 @ 0x00541440 — l'exemplaire du crochet de sol installe hors des
    /// sous-zones 5 et 9 (blocs <c>0x005555a8</c> et <c>0x00555c28</c>), 271 octets. Il <b>coupe
    /// court sur une entite retiree ou morte</b>. Corps et comparaison des deux exemplaires :
    /// <see cref="TickGroundContact"/>.
    /// </summary>
    private static void EntityHandler_00541440(uint* entity)
        => TickGroundContact(entity, guardRemovedOrDead: true);

    /// <summary>
    /// Ghidra: FUN_00541b80 @ 0x00541b80 — l'exemplaire des <b>sous-zones 5 et 9</b> (bloc
    /// <c>0x005562a8</c>), 259 octets. Meme corps que <see cref="EntityHandler_00541440"/> a une
    /// garde pres : ici le sol brulant s'applique <b>meme a une entite retiree ou morte</b>. Corps
    /// et comparaison : <see cref="TickGroundContact"/>.
    /// </summary>
    private static void EntityHandler_00541B80(uint* entity)
        => TickGroundContact(entity, guardRemovedOrDead: false);

    /// <summary>
    /// Ghidra: FUN_00473a70 @ 0x00473a70 - les sous-types <b>6 et 7</b>. Vingt-trois octets, et il
    /// <b>ignore completement son entite</b> : il affiche un enonce fixe et rend la main.
    ///
    /// <para>
    /// L'enonce est <b>un message de developpeur</b>, reste en japonais dans le binaire francais :
    /// « la porte a un battant n'est pas utilisable pour l'instant, contactez Imai ». Ces deux
    /// sous-types sont donc une porte <b>inachevee</b>, et le texte n'a jamais ete traduit parce
    /// qu'il n'etait pas destine au joueur. Voir <see cref="s_DeveloperNotice_00551814"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00473A70(uint* entity)
        => BindMenuLabels(s_DeveloperNotice_00551814, 0, s_WorldMapDescriptor_005633b0, 0);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551814</c> — le bloc d'etiquettes que <see cref="EntityHandler_00473A70"/>
    /// affiche, <b>releve verbatim</b> dans <c>Legacy/Binaries/Blaze.exe</c> (<c>.data</c>, decalage
    /// fichier <c>0x0014FE14</c>). Soixante-six octets jusqu'au terminateur, plus deux octets de
    /// bourrage jusqu'a la frontiere de quatre.
    ///
    /// <para>
    /// <b>Sa longueur ne se lit pas dans un en-tete</b> — c'etait l'erreur qui l'a laisse en lacune
    /// deux jours. Un bloc d'etiquettes est un <b>flux</b> : tout octet au-dessus de 0x1f est du
    /// texte, tout octet en dessous est un code de controle, et le flux s'arrete au premier 0x00
    /// <b>qui n'est pas une operande</b>. La longueur se mesure donc en parcourant le flux avec la
    /// table de la passe 1 (<c>s_ScanTable_0056e870</c>, dans <c>Engine/UiText.cs</c>), dont les
    /// talons <c>0x00543940</c>..<c>0x00543970</c> donnent directement le nombre d'operandes de
    /// chaque code — zero a trois. Ici deux 0x00 sont des operandes avant celui qui termine.
    /// </para>
    /// <para>
    /// Le flux se lit comme une scenette complete :
    /// </para>
    /// <list type="bullet">
    /// <item><c>1f d0 20</c> — code 0x1f, pose le champ de <c>+0x08</c> a deux <c>short</c> : la
    /// boite se place en (0xd0, 0x20) ;</item>
    /// <item><c>08 00</c> — code 0x08, <b>gele</b> le monde ;</item>
    /// <item><c>0c</c> — code 0x0c, ouvre la boite (c'est lui qui <c>ClearOTagR</c> la table de
    /// +0x68) ;</item>
    /// <item>28 paires Shift-JIS — <c>片開きのドアは現在使用出来ません。今井まで連絡ください。</c> ;</item>
    /// <item><c>0f</c> — code 0x0f, <b>attend le joueur</b> en montrant le curseur ;</item>
    /// <item><c>08 01</c> — code 0x08, <b>degele</b> ;</item>
    /// <item><c>00</c> — fin de flux.</item>
    /// </list>
    /// <para>
    /// <b>今井 est un nom propre</b> — celui d'un membre de l'equipe. Le message dit « la porte a
    /// un battant n'est pas utilisable pour l'instant. Contactez Imai. » : c'est une note interne
    /// laissee a qui construisait les niveaux, expediee telle quelle dans la version vendue. Elle
    /// est restee en japonais dans le binaire francais pour la meme raison qu'elle n'a jamais eu
    /// de traduction — personne n'etait cense la lire.
    /// </para>
    /// </summary>
    private static readonly byte* s_DeveloperNotice_00551814 = BuildDeveloperNotice_00551814();

    private static byte* BuildDeveloperNotice_00551814()
    {
        ReadOnlySpan<byte> bytes =
        [
            0x1f, 0xd0, 0x20,                                     // place la boite en (0xd0, 0x20)
            0x08, 0x00,                                           // gele le monde
            0x0c,                                                 // ouvre la boite
            0x95, 0xd0, 0x8a, 0x4a, 0x82, 0xab, 0x82, 0xcc,       // 片開きの
            0x83, 0x68, 0x83, 0x41, 0x82, 0xcd,                   // ドアは
            0x8c, 0xbb, 0x8d, 0xdd, 0x8e, 0x67, 0x97, 0x70,       // 現在使用
            0x8f, 0x6f, 0x97, 0x88, 0x82, 0xdc, 0x82, 0xb9,       // 出来ませ
            0x82, 0xf1, 0x81, 0x42,                               // ん。
            0x8d, 0xa1, 0x88, 0xe4, 0x82, 0xdc, 0x82, 0xc5,       // 今井まで
            0x98, 0x41, 0x97, 0x8d,                               // 連絡
            0x82, 0xad, 0x82, 0xbe, 0x82, 0xb3, 0x82, 0xa2,       // ください
            0x81, 0x42,                                           // 。
            0x0f,                                                 // attend le joueur
            0x08, 0x01,                                           // degele
            0x00,                                                 // fin de flux
            0x00, 0x00,                                           // bourrage jusqu'a la frontiere de 4
        ];

        byte* buffer = (byte*)NativeMemory.AllocZeroed((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) buffer[i] = bytes[i];
        return buffer;
    }

    /// <summary>
    /// Ghidra: FUN_00450bd0 @ 0x00450bd0 - le sous-type <b>11</b>. Une entite qui <b>scintille</b> :
    /// son mot de couleur de +0x38 recoit un gris dont le niveau suit une sinusoide bruitee.
    ///
    /// <para>
    /// Le niveau est <c>0x78 + (rand() % 16) - (sin(phase) &gt;&gt; 8)</c>, calcule <b>sur huit
    /// bits</b> (<c>sub al,bl</c> puis <c>add al,78h</c>), donc il boucle. Il est ensuite replique
    /// sur les trois octets de couleur par les deux <c>shl 8 ; or</c> du binaire.
    /// </para>
    /// <para>
    /// Le modulo signe de Ghidra (<c>cdq ; xor ; sub ; and 0xF ; xor ; sub</c>) est l'idiome MSVC
    /// pour <c>% 16</c> sur un entier signe : le reste garde le signe du dividende.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450BD0(uint* entity)
    {
        byte* e = (byte*)entity;
        ClearEntityVelocity_00460FC0(e);       // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 0x100;

        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) >> 8;   // Ghidra: Ordinal_451
        byte level = (byte)((CrtRand() % 16) - wave + 0x78);                   // Ghidra: FUN_00547140

        uint grey = level;
        grey = grey << 8 | level;
        grey = grey << 8 | level;
        *(uint*)(e + 0x38) = grey;

        if (*(short*)(e + 0x12) == 0x1000) *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004746c0 @ 0x004746c0 - le sous-type <b>14</b>. Quarante-cinq octets : une fois
    /// sur deux, il fait naitre une entite d'effet d'etat sur lui-meme ; l'autre fois il ne fait
    /// rien.
    /// </summary>
    private static void EntityHandler_004746C0(uint* entity)
    {
        if (CrtRand() % 2 != 0) return;                 // Ghidra: FUN_00547140
        SpawnEffectNearCamera_00468110((byte*)entity, 0x3f, 0, -1);
    }

    /// <summary>
    /// Ghidra: FUN_00468110 @ 0x00468110 - <b>la troisieme sœur</b> de
    /// <see cref="TryPlaySoundNearEntity"/> et <see cref="TryPublishCameraEffectNearEntity"/> :
    /// meme calcul de distance a la camera, meme signature perdue par Ghidra, mais un rayon de
    /// <b>1800</b> et une naissance d'entite a la place d'un son ou de trois mots publies.
    ///
    /// <para>
    /// <b>Ghidra se trompe de signature une troisieme fois</b>, et de la meme facon : sa pseudo-C
    /// declare trois parametres et ecrit <c>(short)param_1 + 0xc0</c>, ou <c>param_1</c> est un
    /// pointeur. Le desassemblage donne quatre parametres et le vrai calcul : apres trois
    /// <c>push</c>, le corps lit <c>[esp+14h]</c>, <c>[esp+18h]</c> et <c>[esp+1ch]</c>, soit les
    /// arguments 2, 3 et 4, et c'est <c>param_2</c> qui recoit le <c>+ 0xC0</c>.
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que la naissance ait lieu, 0 sinon.</returns>
    private static ushort SpawnEffectNearCamera_00468110(byte* entity, short subtype, short param3, short param4)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);

        int dx = (camera[0] >> 0xc) - (*(int*)(entity + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(entity + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(entity + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x708) return 0;

        SpawnStatusEffectEntity_0047AC40(
            entity, (int*)(entity + 0x1c), null, (byte)(subtype + 0xc0), (byte)param3, param4);
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004736d0 @ 0x004736d0 - le sous-type <b>2</b>. Meme famille que
    /// <see cref="EntityHandler_004738A0"/> : la vitesse amortie de +0x5c pousse le cap de +0x32.
    ///
    /// <para>
    /// <b>Ce n'est PAS un simple miroir de son frere</b>, et c'etait le piege. Les signes de +0x32
    /// sont bien inverses branche par branche — mais le drapeau <c>0x40000000</c> ne se pose pas
    /// aux memes etats : ici seul l'etat 0x16 le pose, alors que 0x4738A0 le pose aussi en 2 et en
    /// 0xca. Les deux corps sont donc ecrits separement plutot que partages avec un parametre de
    /// signe, qui aurait efface cette difference.
    /// </para>
    /// <para>
    /// Autre difference de forme : ici les etats 1 et 0xc9 <b>rendent la main</b> apres avoir
    /// arme la branche suivante, alors que chez le frere ils tombent dedans dans le meme pas.
    /// </para>
    /// </summary>
    private static void EntityHandler_004736D0(uint* entity)
    {
        byte* e = (byte*)entity;
        if (EntityAnimTick_004727D0(entity, 2, 3) == 1) return;

        short state = *(short*)(e + 0x14);

        if (state == 1)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            *(short*)(e + 0x14) = 2;
            return;
        }

        if (state == 2)
        {
            if (StepDampedSpeed(e) == 0x28)
            {
                *(short*)(e + 0x16) = 1;
                *(short*)(e + 0x14) = 5;
                return;
            }

            *(short*)(e + 0x32) += (short)*(uint*)(e + 0x5c);
        }

        if (state == 0xc9)
        {
            *(short*)(e + 0x14) = 0xca;
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            return;
        }

        if (state == 0xca)
        {
            if (StepDampedSpeed(e) == 0x28)
            {
                *(short*)(e + 0x14) = 0xcd;
                *(short*)(e + 0x16) = 2;
            }

            *(short*)(e + 0x32) += (short)*(uint*)(e + 0x5c);
        }

        if (*(short*)(e + 0x14) == 0xb)
        {
            *(short*)(e + 0x14) = 0xc;
            *(short*)(e + 0x12) = 5;
            *(uint*)(e + 0x5c) = 0;
        }

        // Le binaire relit l'etat ICI, une seule fois, et s'en sert pour les deux tests suivants
        // (0xc puis 0x15) ; le troisieme, celui de 0x16, relit la memoire. Transcrit tel quel.
        short current = *(short*)(e + 0x14);

        if (current == 0xc)
        {
            short step = *(short*)(e + 0x12);
            int travelled = (int)*(uint*)(e + 0x5c);
            *(short*)(e + 0x32) -= step;
            travelled += step;
            *(short*)(e + 0x12) = (short)(step + 1);
            *(uint*)(e + 0x5c) = (uint)travelled;

            if (travelled >= 0x373)
            {
                *(short*)(e + 0x14) = 0xf;
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x32) += (short)((short)*(uint*)(e + 0x5c) - 0x373);
                return;
            }
        }

        if (current == 0x15)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            *(short*)(e + 0x14) = 0x16;
        }

        if (*(short*)(e + 0x14) != 0x16) return;

        if (StepDampedSpeed(e) == 0x28)
        {
            *(short*)(e + 0x16) = 1;
            *(uint*)e |= 0x40000000;
            *(short*)(e + 0x14) = 0x19;
            return;
        }

        *(short*)(e + 0x32) += (short)*(uint*)(e + 0x5c);
    }

    /// <summary>
    /// Ghidra: FUN_004502e0 @ 0x004502e0 - le sous-type <b>17</b> de la table 0x0056adc8, et le
    /// <b>quasi-jumeau</b> du coffre de <see cref="EntityHandler_00473ED0"/> : meme automate, memes
    /// etats, memes appels, a cinq differences pres. Absent de <c>Blaze.c</c> ; 1042 octets
    /// (0x004502e0..0x004506f1), releve dans <c>Legacy/Raw/Blaze/FUN_004502E0.asm</c>.
    ///
    /// <para>
    /// <b>Ce qui le separe du coffre</b>, et c'est tout :
    /// </para>
    /// <list type="number">
    /// <item>la memoire du « deja pris » n'est pas le champ de bits de coffres <c>DAT_005e96e0</c>
    /// mais <c>s_PartyAbilityMask_005e9000</c>, teste par le meme <c>1 &lt;&lt; (n &amp; 7)</c> ;</item>
    /// <item>et il n'y pose pas le bit lui-meme : l'etat 100 passe par
    /// <see cref="ApplyPartyScriptOp"/> avec l'opcode 3 (<c>|=</c>), ce qui allume le bit dans le
    /// bloc de drapeaux de <b>chaque membre du groupe</b> avant de le refleter dans le masque
    /// commun ;</item>
    /// <item>la valeur lissee de l'ouverture vit en <c>0x00571528</c>, pas <c>0x00571990</c> ;</item>
    /// <item>le rappel de fin de boite est <c>0x00450700</c> la ou le coffre passe
    /// <c>0x00474320</c> — deux adresses pour un seul corps, celui de
    /// <see cref="ChestPickupDone_00450700"/> ;</item>
    /// <item>la passe de fin est <see cref="ResetEntityWakeBits_0040E640"/> et non
    /// <see cref="ClearWakeBitOnUnpinnedEntities_004742B0"/> : trois tables identiques, plus deux
    /// sous-types epingles et la remise a blanc des quatre fiches d'emplacement.</item>
    /// </list>
    /// <para>
    /// Le nom donne ici aux trois auxiliaires vient du champ de bits sur lequel il s'indexe
    /// (<c>s_PartyAbilityMask</c>), pas d'une observation en jeu : ce sous-type n'apparait pas a
    /// l'auberge, comme les onze autres ajoutes le 2026-08-24.
    /// </para>
    /// </summary>
    private static void EntityHandler_004502E0(uint* entity)
    {
        byte* e = (byte*)entity;

        // L'operande de script que l'etat 100 appliquera. Le binaire l'initialise ICI, a
        // { 0, 1, 4, 0xEF }, et en reecrit les trois derniers octets avant le seul site qui la
        // lit : ces valeurs d'entree sont donc mortes. Elles sont ecrites quand meme, parce que
        // c'est ce que fait le binaire et que le cout est nul.
        byte* op = stackalloc byte[4];
        op[0] = 0;
        op[1] = 1;
        op[2] = 4;
        op[3] = 0xef;

        *(uint*)(e + 0x50) = 0;
        *(uint*)(e + 0x54) = 0;
        *(uint*)(e + 0x58) = 0;

        if (*(short*)(e + 0x10) == -0xff) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) == -2) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // +0x30 porte le numero, signe : un numero negatif arme la variante -2 et memorise le
            // signe dans +0x98. Identique au coffre.
            short number = *(short*)(e + 0x30);
            *(short*)(e + 0x10) = -1;
            *(short*)(e + 0x9a) = number;
            *(short*)(e + 0x98) = 0;
            if (number < 0)
            {
                *(short*)(e + 0x10) = -2;
                *(short*)(e + 0x9a) = (short)-number;
                *(short*)(e + 0x98) = 1;
            }

            short packed = *(short*)(e + 0x34);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(uint*)(e + 0x44) = (uint)(packed & 0xff);
            *(uint*)(e + 0x48) = (uint)(packed >> 8);
        }

        if (*(short*)(e + 0x10) == -1 && (*(uint*)e & 0x40) != 0)
        {
            byte* target = *(byte**)(e + 0x6c);
            if (target == null) target = Res.s_EntityList_005de558;

            if ((*(uint*)target & 0x10000) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(uint*)e |= 0x2000;
            }
        }

        short state = *(short*)(e + 0x10);
        if (state != -0xff && state != -2 && state != 0 && state != -1 && state != 0xca)
        {
            FreezeAllEntities();                              // Ghidra: FUN_00535300(0)
            *(uint*)e &= 0xfbffffff;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // La cible de lissage de l'etat 2, tiree une fois : 500 a 799.
            *(uint*)(e + 0x5c) = (uint)(CrtRand() % 300 + 500);   // Ghidra: FUN_00547140
            FreezeAllEntities();

            byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, (short)sounds[8], 0x146, 0x3000, 0, 0);

            *(short*)(e + 0x10) = 2;
            *(uint*)e &= 0xfbffffff;
            *(short*)(e + 0x12) = 0;
        }

        // Seule sortie anticipee du corps : l'etat 2 quand la capacite est deja acquise. Tous les
        // autres chemins de l'etat 2 rejoignent la cascade de sortie (Ghidra: LAB_0045050e).
        if (*(short*)(e + 0x10) == 2 && !StepAbilityChestOpening(e, entity)) return;

        AbilityChestDispatchTail(e, op);
    }

    /// <summary>
    /// L'etat 2 : le couvercle descend de 0x23 par pas pendant trente pas, la valeur lissee de
    /// <c>DAT_00571528</c> converge d'un quart vers la cible tiree a l'etat 1. Rend faux quand il
    /// faut rendre la main sans passer par la cascade de sortie.
    ///
    /// <para>
    /// C'est ici que se lit la premiere difference avec le coffre : le champ de bits interroge est
    /// <c>s_PartyAbilityMask_005e9000</c>, indexe par le numero de +0x9a. Bit deja pose, la
    /// capacite est deja acquise et l'etat part directement en 300.
    /// </para>
    /// </summary>
    private static bool StepAbilityChestOpening(byte* e, uint* entity)
    {
        int* smoothed = Mem.EntityState(0x00571528);
        *smoothed += ((int)*(uint*)(e + 0x5c) - *smoothed) >> 2;

        *(short*)(e + 0x34) -= 0x23;
        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) < 0x1f) return true;

        short number = *(short*)(e + 0x9a);
        byte* mask = Mem.Arena(0x005e9000) + (number >> 3);
        if ((*mask & (byte)(1 << (number & 7))) != 0)
        {
            *(short*)(e + 0x10) = 300;
            *(short*)(e + 0x12) = 0;
            return false;
        }

        uint slot = *(uint*)(e + 0x48);
        short saved;

        if (*(short*)(e + 0x14) == -1)
        {
            if (slot == 0)
            {
                *(short*)(e + 0x10) = 0x28;
                *(short*)(e + 0x12) = 0;
                return true;
            }

            saved = *(short*)(e + 0x98);
            *(short*)(e + 0x98) = 0x28;
        }
        else
        {
            if (slot == 0)
            {
                *(short*)(e + 0x10) = 0x14;
                *(short*)(e + 0x12) = 0;
                return true;
            }

            saved = *(short*)(e + 0x98);
            *(short*)(e + 0x98) = 0x14;
        }

        SpawnEffectEntity((byte*)entity, (byte)(slot + 0xc0), 0, -1);   // Ghidra: FUN_0047a9d0
        *(short*)(e + 0x98) = saved;
        *(short*)(e + 0x12) = 0;
        *(short*)(e + 0x10) = 999;
        return true;
    }

    /// <summary>
    /// Ghidra: <c>LAB_0045050e</c> — la cascade de sortie, atteinte par cinq chemins, exactement
    /// comme <see cref="ChestDispatchTail"/>.
    /// </summary>
    /// <param name="op">L'operande de script de quatre octets allouee par l'appelant ; seul l'etat
    /// 100 la reecrit et l'emploie.</param>
    private static void AbilityChestDispatchTail(byte* e, byte* op)
    {
        byte* areaDir = s_Unknown_005e97cc;
        short state = *(short*)(e + 0x10);

        if (state == 0x14)
        {
            // Le contenu est refuse : marque dans la banque 0x005e9840, indexee par +0x14, puis la
            // phrase que +0x16 designe.
            short index = *(short*)(e + 0x14);
            byte* refused = Mem.Arena(0x005e9840) + (index >> 3);
            *refused |= (byte)(1 << (index & 7));

            BindMenuLabels(
                *(byte**)(*(int*)(areaDir + 0x70) + *(short*)(e + 0x16) * 4),
                *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
            *(short*)(e + 0x10) = 100;
            return;
        }

        if (state == 0x28)
        {
            AbilityChestGrantContents(e);
            return;
        }

        if (state == 100)
        {
            // La difference qui fait ce sous-type : au lieu de poser un bit dans un champ de bits
            // de zone, il applique l'operation de script 3 (`|=`) au bloc de drapeaux de chaque
            // membre du groupe, puis au masque commun 0x005e9000.
            short number = *(short*)(e + 0x9a);
            op[1] = (byte)(number >> 3);
            op[2] = 3;
            op[3] = (byte)(1 << (number & 7));

            // Ghidra: `lea ecx,[esp+18h]` — le binaire passe l'adresse de son PROPRE emplacement
            // d'argument comme sortie, et ecrase donc le pointeur d'entite qu'il y avait recu.
            // C'est sans effet : la suite du corps ne lit plus que ESI. Transcrit par une variable
            // morte, qui dit la meme chose sans reproduire l'ecrasement.
            uint clobberedArgumentSlot;
            ApplyPartyScriptOp(op, &clobberedArgumentSlot);   // Ghidra: FUN_00514000

            *(short*)(e + 0x10) = 200;
        }

        if (*(short*)(e + 0x10) == 200)
        {
            FreezeAllEntities();
            *(short*)(e + 0x10) = 0xc9;
            *(uint*)e &= 0xfbffffff;
        }

        // Le couvercle remonte de 0x46 a chaque pas de l'etat 0xc9, et l'etat ne change que
        // lorsque la hauteur repasse positive.
        if (*(short*)(e + 0x10) == 0xc9)
        {
            *(short*)(e + 0x34) += 0x46;
            if (*(short*)(e + 0x34) > -1)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 0xca;
                UnfreezeAllEntities();                        // Ghidra: FUN_00535400
                *(uint*)e &= 0xffffdfff;
                return;
            }
        }

        if (*(short*)(e + 0x10) == 0xca && (e[9] & 0xf) == 0)
        {
            // Ghidra: `neg ax ; sbb eax,eax ; dec eax` — l'etat de repos depend du signe memorise
            // a l'entree : -1 pour un exemplaire ordinaire, -2 pour la variante.
            *(short*)(e + 0x10) = (short)(-1 - (*(short*)(e + 0x98) != 0 ? 1 : 0));
        }

        if (*(short*)(e + 0x10) != 300)
        {
            ResetEntityWakeBits_0040E640();                   // Ghidra: FUN_0040e640
            return;
        }

        // L'etat 300 est l'exemplaire deja pris : une phrase fixe du repertoire principal, puis
        // 200 (Ghidra: `mov [esi+10h],di`, et EDI vaut 0xC8 depuis LAB_004505ff).
        BindMenuLabels(
            *(byte**)(*(int*)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x48c),
            *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);
        *(short*)(e + 0x10) = 200;
    }

    /// <summary>
    /// L'etat 0x28 : la remise effective du contenu, mot pour mot celle du coffre. Le rappel de
    /// fin de boite est <c>0x00450700</c> ici et <c>0x00474320</c> chez le coffre, mais les deux
    /// adresses portent le meme corps — <see cref="ChestPickupDone_00450700"/>.
    ///
    /// <para>
    /// <c>DAT_005e9860</c> retient, par numero d'objet de +0x44, ce que l'exemplaire a deja donne :
    /// non nul, l'objet est redonne tel quel (moins un) ; nul, il est tire dans la table ponderee
    /// de la zone et le resultat est memorise. Le code de retour non nul veut dire « pas passe ».
    /// </para>
    /// </summary>
    private static void AbilityChestGrantContents(byte* e)
    {
        uint* recipient = *(uint**)(e + 0x6c);

        *(uint*)((byte*)recipient + 0x6c) = 0;
        *((sbyte*)recipient + 0x41) = (sbyte)e[8];

        byte* given = Mem.Arena(0x005e9860);
        uint item = *(uint*)(e + 0x44);

        if (given[item] != 0)
        {
            int refused = GrantChestContents_00530F20(recipient, given[item] - 1, 0x00450700);

            // Ghidra: `(-(ushort)(sVar4 != 0) & 0xfce1) + 999` — 999 quand l'objet est passe,
            // 999 - 0x31f == 200 quand il ne l'est pas. Ecrit ici pour ce qu'il vaut.
            *(short*)(e + 0x10) = (short)(refused != 0 ? 200 : 999);
            return;
        }

        int roll = PickWeightedTableEntry(*(short*)(e + 0x16), 100);   // Ghidra: FUN_0053a920
        int result = GrantChestContents_00530F20(recipient, roll, 0x00450700);

        if (result != 0)
        {
            *(short*)(e + 0x10) = 200;
            given[item] = (byte)result;
            return;
        }

        *(short*)(e + 0x10) = 999;
    }
}
