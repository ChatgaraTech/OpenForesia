// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// FUN_00516fc0, la logique par image de la carte du monde : 1 226 lignes de pseudo-C, le plus gros
// corps que le depot ait aborde. Le decoupage prevu par
// openspec/changes/split-blaze-into-subsystems (D9) lui reservait ce fichier ; il est ouvert le
// 2026-08-22, au moment ou la boucle par image commence a etre transcrite.
//
// Le desassemblage complet de la fonction est conserve dans
// Legacy/Raw/Blaze/FUN_00516FC0-full.asm. Il n'est pas decoratif : la pseudo-C de ce corps est
// truffee de `piStack_74 = X;` et de registres non attribues (unaff_EBX/ESI/EBP/EDI), et plusieurs
// decisions ci-dessous ne se lisent QUE dans le binaire.
//
// CE FICHIER NE PORTE PLUS QUE LA BOUCLE. Le 2026-09-16, les 74 072 lignes qu'il avait accumulees
// ont ete decoupees en 70 fichiers sous Screens/State03/ : ce qui reste ici, ce sont
// `RunWorldMapTick` et les deux crochets indirects, soit 1 % de ce que le fichier pesait.
//
// Les deux crochets `PTR_FUN_0056afe8` et `PTR_FUN_0056aff0` NE SONT PAS des globales isolees :
// ce sont les entrees 8 et 10 de la table 0x0056afc8, la quatrieme des sept tables de sauts
// d'entites que `InstallEntityVTables` (Engine/Resources.cs) recopie au chargement de zone. Le
// dispatch se fait sur la VALEUR d'adresse, comme partout ailleurs : toute autre cible leve en se
// nommant.
//
// Ou est passe le reste : Dispatch/ le carrefour, Tick/ le dessin et la simulation, Combat/,
// Physics/, Entities/, Effects/, Items/, Spells/, Cutscene/, Ai/, Handlers/. La carte complete et
// la regle qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00516fc0 @ 0x00516fc0 — la logique par image de la carte du monde.
    ///
    /// <para>
    /// <b>Etat au 2026-08-22 : le prologue et l'ossature de la boucle par image sont transcrits</b>
    /// (l'entree de boucle, les quatre cas du repartiteur <c>DAT_005e9ae4 &amp; 3</c>, la sortie par
    /// <c>DAT_005de02c &amp; 2</c> et la queue de boucle). Quatre trous sonores subsistent, chacun
    /// nomme sur place : l'affichage de mise au point, le corps du cas 0, le bloc d'affichage
    /// principal, et la branche <c>DAT_005de02c &amp; 0x40</c>.
    /// </para>
    /// <para>
    /// Le prologue va de l'entree jusqu'au <c>do { … }</c> de <c>0x00517163</c> : instantane de mise
    /// au point, ouverture d'image, remise a zero d'une dizaine de mots et de deux pages de 4 Kio
    /// dans MainMem, changement de musique si la zone a change, puis le pas de boucle — c'est lui
    /// qui lance les scripts — et enfin la purge de la liste d'attente.
    /// </para>
    /// <para>
    /// <b>Ecrit contre le desassemblage</b>, pas contre la pseudo-C seule, parce que celle-ci est
    /// truffee de <c>piStack_74 = X;</c>. La cle est confirmee au niveau machine :
    /// <b><c>piStack_74 = X</c> avant un appel est le DERNIER argument, celui que Ghidra a
    /// perdu</b> — <c>FUN_0052a1e0</c> en prend trois (<c>add esp,0Ch</c>) la ou la pseudo-C en
    /// montre deux. Quand la valeur posee est une adresse de code (0x5170bb, 0x5170d0), ce n'est pas
    /// un argument mais l'adresse de retour de l'appel : ces deux-la ne prennent rien.
    /// </para>
    /// <para>
    /// Le pas de boucle <see cref="s_WorldMapStep_005645f8"/> vaut <b>2</b> au chargement, donc la
    /// premiere image passe par la branche complete : vue publiee, puis <b>deux</b> lancements de
    /// script.
    /// </para>
    /// </summary>
    private static void RunWorldMapTick()
    {
        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            s_DebugQuest_005dbc14 = (byte)Res.s_Unknown_005de528;
            s_DebugMap_005ddfd0 = s_Unknown_005de52c;
            s_DebugPlayer_005dbc18 = (byte)s_PartySize_005e9020;
        }

        uint* otBase;
        uint* otEntry;
        uint* primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0, trois arguments

        s_Unknown_00566c90 = 0;
        *(uint*)Mem.Entity(0x005f2cf0) = 0;
        Mem.s_WorldFlags_005de02c = 0;
        *Mem.Arena(0x005e9ae4) = 0;                   // Ghidra: `& 0xffffff00` — un `mov byte [.],0`
        *Mem.Camera(0x005de0a8) = 0;
        *Mem.Camera(0x005de0ac) = 0;
        *Mem.Camera(0x005de0b0) = 0;
        Mem.s_Unknown_005de024 = 0;
        Mem.s_Unknown_005de028 = 0;

        // Deux pages de 0x400 dwords dans MainMem : les tampons de travail de l'image.
        byte* mainMem = GetMainMemPtr();
        for (int i = 0; i < 0x400; i++)
        {
            ((uint*)(mainMem + 0x1c5000))[i] = 0;
        }

        for (int i = 0; i < 0x400; i++)
        {
            ((uint*)(mainMem + 0x1c4000))[i] = 0;
        }

        if (s_Unknown_00566c8c != (byte)s_Unknown_005de534)
        {
            s_Unknown_00566c8c = (byte)s_Unknown_005de534;
            PlayMusicTrack(s_Unknown_00566c8c);       // Ghidra: FUN_00529ad0, un argument
        }

        byte* leader = Mem.Arena(0x005e93c0) + (byte)Mem.s_Unknown_005de538 * 0x9c;

        // Le `dec eax ; je … ; dec eax ; jne …` du binaire se lit : 1 ne fait que le second script,
        // 2 fait tout, et toute autre valeur ne fait rien. Rendu en deux `if` plutot qu'en cascade
        // de sauts.
        int step = s_WorldMapStep_005645f8 & 0xff;

        if (step == 2)
        {
            PublishFrameView();                       // Ghidra: FUN_00401080

            if (s_Unknown_00566c94 != 0)
            {
                s_Unknown_00566c94 = 0;
                SnapshotViewVector();                 // Ghidra: FUN_00401000
            }

            RunEntityScript(s_Unknown_005e97cc, leader, (uint)(*Mem.Camera(0x005de048) & 0xffff));
        }

        if (step == 1 || step == 2)
        {
            RunEntityScript(s_Unknown_005e97cc, leader, (uint)(*Mem.Camera(0x005de04c) & 0xffff));
        }

        // La liste d'attente que l'opcode 11 remplit. Son premier octet est le compte, et il est
        // **relu a chaque tour** par le binaire ; conserve tel quel. Pour chaque entree, la liste
        // d'enregistrements de 0x10 octets qu'elle designe est marquee consommee (premier octet a
        // zero) jusqu'au terminateur -1.
        byte* pending = Mem.Entity(0x005fa560);
        if (*pending >= 1)
        {
            for (int i = 1; i <= (*pending & 0xff); i++)
            {
                int* record = *(int**)(*(byte**)(s_Unknown_005e97cc + 0x88) + pending[i] * 4);
                if (record == null || *record == -1)
                {
                    continue;
                }

                int next;
                do
                {
                    *(byte*)record = 0;
                    next = record[4];
                    record += 4;
                }
                while (next != -1);
            }
        }

        Winsys3d.Ordinal_379(0x190);                  // libgte SetGeomScreen(400)

        // Ghidra: le bloc de sprite de sept champs a [esp+0x34] de la trame (voir
        // Primitives2d.DrawSpritePrimitive). Seul RunDebugMenuFrame_0051AA30 le recoit d'ici pour
        // l'instant ; DrawWorldMapFrame garde le sien, et chaque lecteur ecrit les champs qu'il lit.
        // Alloue hors de la boucle d'images, comme la trame de l'original.
        //
        // Le champ +4 part a 0x1000000 : c'est la valeur que l'emplacement porte a la fin de chaque
        // image (0x0051833d et 0x005192ef l'y ecrivent), et l'en-tete MONITOR le lit avant de
        // l'ecrire. Meme raisonnement, et meme limite, que LB-008 sur DrawWorldMapFrame.
        int* spriteBlock = stackalloc int[5];
        spriteBlock[1] = 0x1000000;
        Inp.JoystickResetState(false);                // Ghidra: FUN_00504b10(0)

        // =============================================================================
        // La boucle par image. Le `do { … } while(true)` de 0x00517163 : on n'en sort que par le
        // `return` de la branche DAT_005de02c & 2, ou par une exception.
        // =============================================================================
        while (true)
        {
            // Sous OPENFORESIA_TRACE_SCRIPT=1 : l'etat de la boucle de monde, une ligne toutes les
            // soixante images.
            //
            // **Elle est ICI et non a l'entree de la fonction**, et c'est tout le sujet :
            // `RunWorldMapTick` n'est PAS appelee une fois par image. Elle contient elle-meme la
            // boucle d'images, et n'est donc entree qu'une fois par chargement de zone. Une sonde
            // posee a son entree ne s'allume qu'une fois — ce qui s'est produit, et m'a fait croire
            // un moment que la fonction ne tournait pas.
            // ECHAFAUDAGE : la salle d'essai, sous OPENFORESIA_AI_BENCH. Sans le drapeau, le
            // premier test de TickAiBench rend la main aussitot. Voir AiBench.cs.
            TickAiBench();

            if (s_TraceScript && ++s_TickCount % 60 == 0)
            {
                Console.Error.WriteLine(
                    $"[ETAT] image={s_TickCount} pas={s_WorldMapStep_005645f8} " +
                    $"drapeaux=0x{Mem.s_WorldFlags_005de02c:x} " +
                    $"9ae4=0x{*(uint*)Mem.Arena(0x005e9ae4) & 3} " +
                    $"zone={Res.s_Unknown_005de528 & 0xff} sous-zone={s_Unknown_005de52c} " +
                    $"x={*(int*)(Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c + 0x1c) >> 12}" +
                    $" z={*(int*)(Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c + 0x24) >> 12}");
            }

            // Le mot de boutons du meneur, imprime a chaque CHANGEMENT. C'est ce qui manquait a
            // toutes les sondes precedentes : elles disaient ce que le jeu faisait, aucune ne disait
            // quand le joueur appuyait. Sans ce reperage, « rien ne se passe » ne se distingue pas
            // de « l'appui n'a jamais ete vu ».
            if (s_TraceScript)
            {
                uint padSlot = (uint)(byte)Mem.s_Unknown_005de538;
                byte* padRecord = Inp.s_InputStateBase_00604a20
                                + Inp.s_PlayerSlots_005e9188[padSlot * 0x4c] * 0x10;
                uint buttons = *(uint*)padRecord;
                if (buttons != s_LastButtons)
                {
                    s_LastButtons = buttons;
                    Console.Error.WriteLine($"[TOUCHE] image={s_TickCount} boutons=0x{buttons:x8}");
                }
            }

            // Ghidra: FUN_0040a600(DAT_005de534) — corps vide, confirme dans Blaze.c.
            AnimatePalettes(Mem.s_MainResourceDirectory_005e97c4);   // Ghidra: FUN_00533000
            Aud.TickDelayedSounds();                                 // Ghidra: FUN_0053dc30
            PrepareLeaderAndPartyFlags();                            // Ghidra: FUN_0050e2b0

            uint slot = (uint)(byte)Mem.s_Unknown_005de538;
            if ((*(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) & 0x10000) != 0
                || Inp.s_PlayerSlots_005e9188[slot * 0x4c] == 8)
            {
                PassLeaderToNextEligible(
                    (uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c),
                    Inp.s_SlotArea_005e9180 + slot * 0x4c,
                    0x10000);
            }

            // `iVar12 = DAT_005633a8` est relu ici et reutilise jusqu'au bas du tour : les cas 2 et
            // 3 du repartiteur le testent, et la branche d'affichage aussi.
            int debugLatch = Mem.s_DebugModeLatch_005633a8;

            if (debugLatch == 0 && s_DebugOverlayEnabled_005de020 != 0)
            {
                // L'en-tete MONITOR (0x00517208..0x0051762b), allume par la ligne MONITOR du menu
                // de mise au point : quatre lignes de valeurs, puis la recopie des banques dans les
                // miroirs que DrawDebugMonitorChanges comparera plus bas dans la meme image.
                primCursor = DrawDebugMonitorHeader(otBase, primCursor, spriteBlock);
            }

            if ((Mem.s_WorldFlags_005de02c & 0x40) != 0)
            {
                // Ghidra: `unaff_EBX = (*PTR_FUN_0056aff0)(local_60, unaff_EBX, …)` — un appel
                // INDIRECT, donc une vraie fonction et non un bloc en ligne. EBP y est le curseur
                // de primitives (`mov ebp,[esp+10h]` en 0x0051762b), et le resultat y retourne.
                primCursor = CallEntityHook_0056aff0(
                    otBase,
                    primCursor,
                    Inp.s_SlotFlags_005e93c0 + (uint)s_Unknown_005de540 * 0x9c,
                    Inp.s_InputStateBase_00604a20
                        + Inp.s_PlayerSlots_005e9188[s_Unknown_005de540 * 0x4c] * 0x10);
            }
            else
            {
                // Ghidra: `switch(_DAT_005e9ae4 & 3)` — le dword est lu en entier puis masque, mais
                // seul son octet bas est jamais ecrit (mise a zero du prologue, `inc` des cas 1
                // et 3).
                switch (*(uint*)Mem.Arena(0x005e9ae4) & 3)
                {
                    case 0:
                        // Le pas de simulation du monde, et la branche prise au premier tour : le
                        // prologue vient de remettre DAT_005e9ae4 a zero.
                        for (int i = 0; i < 0x400; i++) ((uint*)(mainMem + 0x1c5000))[i] = 0;
                        for (int i = 0; i < 0x400; i++) ((uint*)(mainMem + 0x1c4000))[i] = 0;

                        // Le bit 3 est efface ici et repose aussitot par le crochet ci-dessous.
                        // Transcrit tel quel : c'est ce que fait l'original.
                        Mem.s_WorldFlags_005de02c &= 0xfffffff7;
                        CallEntityHook_0056afe8();

                        // -------- le pas de simulation proprement dit, 0x005176d5..0x0051776b --------
                        // Quatorze appels a la file, sans une seule variable locale entre eux : la
                        // sequence est l'ordre du monde. Vue, puis scripts du meneur, puis les six
                        // tables d'entites, puis le groupe, puis les rencontres, les effets, les
                        // scripts de fin et la camera de scenette.
                        SmoothSceneOrientationAndBackColour();          // FUN_00532bf0
                        RunLeaderScriptListEarly(s_Unknown_005e97cc);   // FUN_00513ef0
                        TickEntityTable_005fa5e0();                     // FUN_0050e5e0
                        TickEntityTable_005ecaa0();                     // FUN_0050e680
                        TickEntityTable_005f7de0();                     // FUN_0050eb20

                        *Mem.Entity(0x005f2ce4) = 0;
                        for (int member = 0; member < s_PartySize_005e9020; member++)
                        {
                            TickPartyMember(member);                    // FUN_0050bf20
                        }

                        TickEntityTable_005ff400();                     // FUN_0047aa70
                        TickEntityTable_005edee0();                     // FUN_0047ab70
                        TickEntityTable_005f2d40();                     // FUN_0047acf0

                        if ((Mem.s_WorldFlags_005de02c & 0x10) == 0)
                        {
                            TrySpawnRandomEncounter();                  // FUN_0050ed70
                        }

                        TickEffectSlots_005e9b60();                     // FUN_0050f0d0
                        RunLeaderScriptListLate(s_Unknown_005e97cc);    // FUN_00513f20
                        ResetPendingChunkRecords();                     // FUN_00534f90
                        TickCutsceneCamera();                           // FUN_005036e0

                        (*(uint*)Mem.Entity(0x005f2cf0))++;

                        // Ghidra: `iVar12 = DAT_005633a8;` — le verrou de mise au point est relu ici
                        // avant la branche d'affichage. Rien du pas de simulation ne l'ecrit
                        // aujourd'hui, donc la relecture est un no-op ; conservee telle quelle.
                        debugLatch = Mem.s_DebugModeLatch_005633a8;
                        break;

                    case 1:
                        // Le degel differe : tant que DAT_005fa580 tient, on decompte ; a zero, on
                        // avance d'un cran et on recadre le regard sur le meneur.
                        if (*Mem.Entity(0x005fa580) == 0)
                        {
                            (*Mem.Arena(0x005e9ae4))++;
                            s_Unknown_005de6f8 = 0;
                            s_Unknown_005de6fc = 0;
                            s_Unknown_005de704 = 0;
                            s_Unknown_005de70c = -1;
                            s_Unknown_005de710 = -1;

                            // `sar eax,11h` puis `and ax,0xfffc` : le quart de la position monde,
                            // arrondi au multiple de quatre.
                            uint leaderSlot = (uint)(byte)Mem.s_Unknown_005de538;
                            byte* record = Inp.s_SlotFlags_005e93c0 + leaderSlot * 0x9c;
                            s_Unknown_005e8eda = (short)((*(int*)(record + 0x24) >> 0x11) & 0xfffc);
                            s_Unknown_005e8ed8 = (short)((*(int*)(record + 0x1c) >> 0x11) & 0xfffc);
                            s_Unknown_005de718 = -1;

                            if (s_Unknown_005e8eda > 0xa0) s_Unknown_005e8eda = 0xa0;
                            if (s_Unknown_005e8eda < -0xa0) s_Unknown_005e8eda = unchecked((short)0xff60);
                            if (s_Unknown_005e8ed8 < -0x90) s_Unknown_005e8ed8 = unchecked((short)0xff70);
                            if (s_Unknown_005e8ed8 > 0x90) s_Unknown_005e8ed8 = 0x90;

                            Mem.s_WorldFlags_005de02c |= 4;
                            // Ghidra: FUN_0040a600(); FUN_0040a600(); — deux corps vides.
                        }
                        else
                        {
                            (*Mem.Entity(0x005fa580))--;
                        }
                        break;

                    case 2:
                    {
                        // Le pilotage libre de la camera, reserve au mode de mise au point. Le
                        // premier test, lui, s'applique meme hors mise au point.
                        int padSlot = Inp.s_PlayerSlots_005e9188[s_Unknown_005de540 * 0x4c];
                        byte* pad = Inp.s_InputStateBase_00604a20 + padSlot * 0x10;

                        if ((pad[2] & 1) != 0)
                        {
                            Mem.s_WorldFlags_005de02c ^= 1;
                        }

                        if (debugLatch == 0)
                        {
                            if (*Mem.Camera(0x005de060) == 0)
                            {
                                if ((*(ushort*)(pad + 2) & 0xf00) == 0xf00)
                                {
                                    *Mem.Camera(0x005de060) = 1;
                                }
                            }
                            else
                            {
                                ushort held = *(ushort*)pad;
                                if ((held & 1) == 0)
                                {
                                    // Sans le bouton 0 : la cible de la camera glisse.
                                    if ((held & 0x20) != 0) *(short*)(s_WorldMapCamera_00564650 + 8) += 10;
                                    if ((*(ushort*)pad & 0x80) != 0) *(short*)(s_WorldMapCamera_00564650 + 8) -= 10;
                                    if ((*(ushort*)pad & 0x10) != 0) *(short*)(s_WorldMapCamera_00564650 + 0xc) -= 10;
                                    if ((*(ushort*)pad & 0x40) != 0) *(short*)(s_WorldMapCamera_00564650 + 0xc) += 10;
                                }
                                else
                                {
                                    // Avec : c'est l'orientation de la scene qui tourne.
                                    if ((held & 0x20) != 0) Mem.s_SceneYaw_005e930a += 0x20;
                                    if ((*(ushort*)pad & 0x80) != 0) Mem.s_SceneYaw_005e930a -= 0x20;
                                    if ((*(ushort*)pad & 0x10) != 0) Mem.s_ScenePitch_005e9308 -= 0x20;
                                    if ((*(ushort*)pad & 0x40) != 0) Mem.s_ScenePitch_005e9308 += 0x20;
                                }

                                if ((*(ushort*)(pad + 2) & 0xf00) == 0xf00)
                                {
                                    *Mem.Camera(0x005de060) = 0;
                                }

                                if ((pad[3] & 0x80) != 0)
                                {
                                    *(short*)(s_WorldMapCamera_00564650 + 0) = 500;
                                    *(short*)(s_WorldMapCamera_00564650 + 2) = 0;
                                    *(short*)(s_WorldMapCamera_00564650 + 4) = 0;
                                    *(short*)(s_WorldMapCamera_00564650 + 8) = 0;
                                    *(short*)(s_WorldMapCamera_00564650 + 0xc) = 0;
                                    *(short*)(s_WorldMapCamera_00564650 + 0x10) = 900;
                                }
                            }
                        }
                        break;
                    }

                    case 3:
                        if (*Mem.Entity(0x005fa580) == 0)
                        {
                            *(int*)Mem.Entity(0x005f7b4c) = -1;
                            (*Mem.Arena(0x005e9ae4))++;
                            *(int*)Mem.Entity(0x005f7ba4) = 0;
                            if (debugLatch == 0)
                            {
                                *Mem.Camera(0x005de060) = 0;
                            }
                        }
                        else
                        {
                            (*Mem.Entity(0x005fa580))--;
                        }
                        break;
                }

                if ((Mem.s_WorldFlags_005de02c & 2) != 0)
                {
                    // La seule sortie de la boucle par image : le monde demande a rendre la main.
                    if ((s_WorldMapStep_005645f8 & 0xff) == 1)
                    {
                        RunEntityScript(
                            s_Unknown_005e97cc,
                            Inp.s_SlotFlags_005e93c0 + (uint)s_Unknown_005de53c * 0x9c,
                            (uint)(*Mem.Camera(0x005de050) & 0xffff));
                    }

                    // Ghidra: FUN_0040a600(); FUN_0040a600(); — deux corps vides.
                    *(uint*)Mem.Arena(0x005e9898) = 1;   // le champ `isbg` des deux draw envs
                    *(uint*)Mem.Arena(0x005e9908) = 1;
                    return;
                }

                if ((Mem.s_WorldFlags_005de02c & 4) == 0
                    || (*Mem.Camera(0x005de060) != 0 && debugLatch == 0))
                {
                    // =========================================================================
                    // LE BLOC D'AFFICHAGE PRINCIPAL — 0x00517aa7..0x00519703, ~700 lignes.
                    //
                    // **La lecture qui coute cher est deja faite** : les `unaff_EBX`/`unaff_ESI`/
                    // `unaff_EDI`, le `local_60` et le `piStack_74` que Ghidra repand dans ce bloc
                    // ne sont que DEUX objets, et ce sont les deux emplacements de pile que
                    // FrameBegin a remplis.
                    //
                    //   [esp+0x14] = la base d'ordering table  -> Ghidra: unaff_EBP, unaff_EDI,
                    //                                                     local_60
                    //   [esp+0x10] = le curseur de primitives  -> Ghidra: unaff_ESI, unaff_EBX,
                    //                                                     piStack_74
                    //
                    // Verifie au premier appel de dessin (0x00517e95..0x00517eb6) : le binaire lit
                    // les deux emplacements, empile `(base, curseur, …)`, et **range le resultat
                    // dans [esp+0x10]**. Le motif `mov [esp+10h],eax` apres l'appel se repete a
                    // tous les sites, et la base est systematiquement passee en `base + 8`.
                    //
                    // C'est exactement le piege que CLAUDE.md signale comme ayant deja coute une
                    // journee : un curseur mal chaine ne plante pas ici, il plante DANS LA DLL,
                    // sans pile utilisable. Ne pas ecrire une seule ligne de ce bloc sans repasser
                    // par cette table.
                    //
                    // Reste a faire : le corps lui-meme, et onze appelees. Sept sont deja dans le
                    // depot (FUN_00533000, FUN_0051c300, FUN_00520ae0, FUN_0051f9c0, FUN_0051c120,
                    // FUN_00521df0, FUN_00545340, FUN_0051f650, FUN_0051be20, FUN_0051bdb0,
                    // FUN_005443a0, FUN_0051d1f0, FUN_00521f20, FUN_00544350, FUN_0052a280) ;
                    // manquent, toutes dans Blaze.c :
                    //
                    //   FUN_0051aa30  594 l. (transcrite le 2026-09-15)   FUN_0051fa40  441 l.   FUN_0051e700  423 l.
                    //   FUN_0051c420  374 l.   FUN_00521000  365 l.   FUN_004011c0  363 l.
                    //   FUN_00522140  315 l.   FUN_005309b0  147 l.   FUN_0051be90   74 l.
                    //   FUN_005320f0   51 l.   FUN_005321f0   51 l.   FUN_00532b40   29 l.
                    //   FUN_0052a7d0   20 l.   FUN_0051f940   18 l.   FUN_00538bc0   13 l.
                    //   FUN_0051c250   10 l.
                    //
                    // Le sous-bloc de mise au point (0x00518473 en amont, ~150 l.) est garde par
                    // DAT_005de020 : transcrit le 2026-09-15, DrawDebugMonitorChanges.
                    // =========================================================================
                    // -------- la tete du bloc : tout l'etat de vue, avant la premiere primitive --
                    ApplyViewMode_004011C0(s_Unknown_005de54c);
                    AdvanceScriptedSceneMotion_00532B40();
                    AnimatePalettes(s_Unknown_005e97c8);
                    AnimatePalettes(s_Unknown_005e97cc);

                    // Les deux familles de defilement de texture, sur deux tranches consecutives
                    // de la table de modeles : celles d'avant DAT_00600838 defilent par pas de
                    // 0x20 octets, celles d'apres par pas de 0x10.
                    byte** chunk = (byte**)Mem.Entity(0x005f7bc0)
                        + ((*(uint*)Mem.Entity(0x00600830) >> 0x10) & 0xff);

                    for (int i = (*Mem.Entity(0x00600838) & 0xff)
                             - (int)((*(uint*)Mem.Entity(0x00600830) & 0xffff) >> 8); i != 0; i--)
                    {
                        ScrollTextureQuads_005320F0(*chunk);
                        chunk++;
                    }

                    for (int i = *Mem.Entity(0x005f2cec) - (*Mem.Entity(0x00600838) & 0xff);
                         i != 0; i--)
                    {
                        ScrollTextureTriangles_005321F0(*chunk);
                        chunk++;
                    }

                    *(uint*)Mem.Arena(0x005e9b34) =
                        (uint)(nuint)(*(byte**)(s_Unknown_005e97cc + 0xa0) + s_Unknown_005f2ce0[0x0b] * 4);

                    // La pose de camera glisse d'un quart vers la consigne de DAT_005e9ae0, angle
                    // par angle et en modulo 0x1000 — le meme pas que
                    // SmoothSceneOrientationAndBackColour, a la profondeur pres (>> 2 au lieu de
                    // >> 3).
                    short* view = *(short**)Mem.Arena(0x005e9ae0);
                    short* pose = (short*)Mem.s_CameraVector_00604928;

                    pose[0] += SmoothCameraStep((ushort)((view[0] - pose[0]) & 0xfff));
                    pose[1] += SmoothCameraStep((ushort)((view[1] - pose[1]) & 0xfff));
                    pose[2] += SmoothCameraStep((ushort)((view[2] - pose[2]) & 0xfff));

                    RotMatrix(pose, (short*)Mem.s_CameraMatrix_006008e0);

                    // Le personnage suivi : celui dont le compteur de DAT_005e9180 est le plus
                    // haut, parmi ceux qui sont encore en jeu.
                    byte* followed = null;
                    short bestRank = -1;

                    for (int i = 0; i < s_PartySize_005e9020; i++)
                    {
                        byte* member = Inp.s_SlotFlags_005e93c0 + i * 0x9c;
                        short rank = *(short*)(Inp.s_SlotArea_005e9180 + i * 0x4c);

                        if ((*(uint*)member & 0x50000000) == 0
                            && (*(uint*)(member + 0x90) & 0x8000) == 0
                            && bestRank < rank)
                        {
                            followed = member;
                            bestRank = rank;
                        }
                    }

                    // Puis le plus eloigne de celui-la : c'est le couple qui doit tenir dans le
                    // cadre, et le milieu des deux devient la cible de camera.
                    byte* farthest = null;
                    int bestDistance = -1;

                    for (int i = 0; i < s_PartySize_005e9020; i++)
                    {
                        byte* member = Inp.s_SlotFlags_005e93c0 + i * 0x9c;

                        if (member != followed
                            && (*(uint*)member & 0x50000000) == 0
                            && (*(uint*)(member + 0x90) & 0x8000) == 0)
                        {
                            int dx = (*(int*)(member + 0x1c) - *(int*)(followed + 0x1c)) >> 0xc;
                            int dz = (*(int*)(member + 0x24) - *(int*)(followed + 0x24)) >> 0xc;
                            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                            if (bestDistance < distance)
                            {
                                bestDistance = distance;
                                farthest = member;
                            }
                        }
                    }

                    // Le centroide du groupe, et le plus haut de ses membres encore jouables.
                    int sumX = 0;
                    int sumZ = 0;
                    int highest = int.MinValue;
                    int counted = 0;

                    *Mem.Camera(0x005de098) = 0;
                    *Mem.Camera(0x005de0a0) = 0;

                    for (int i = 0; i < s_PartySize_005e9020; i++)
                    {
                        byte* member = Inp.s_SlotFlags_005e93c0 + i * 0x9c;

                        if ((*(uint*)member & 0x50000000) == 0
                            && (*(uint*)(member + 0x90) & 0x8000) == 0)
                        {
                            sumX += *(int*)(member + 0x1c);
                            sumZ += *(int*)(member + 0x24);

                            if ((*(uint*)member & 0x10000) == 0 && highest < *(int*)(member + 0x20))
                            {
                                highest = *(int*)(member + 0x20);
                            }

                            counted++;
                        }

                        *Mem.Camera(0x005de098) = sumX;
                        *Mem.Camera(0x005de0a0) = sumZ;
                    }

                    if (counted != 0)
                    {
                        byte* other = counted == 1 ? followed : farthest;

                        *Mem.Camera(0x005de098) /= counted;
                        int midX = (*(int*)(other + 0x1c) + *(int*)(followed + 0x1c)) >> 1;
                        int midZ = (*(int*)(other + 0x24) + *(int*)(followed + 0x24)) >> 1;
                        *Mem.Camera(0x005de0a0) /= counted;

                        *Mem.Camera(0x005de068) += (midX - *Mem.Camera(0x005de068)) >> 2;
                        *Mem.Camera(0x005de070) += (midZ - *Mem.Camera(0x005de070)) >> 2;

                        if (highest != int.MinValue)
                        {
                            *Mem.Camera(0x005de06c) += (highest - *Mem.Camera(0x005de06c)) >> 2;
                        }
                    }

                    // Le decalage monde glisse vers l'oppose de la camera de scenette. **Le X et
                    // le Z sont nies, le Y ne l'est pas** — asymetrie de l'original, conservee.
                    int* sceneCamera = *(int**)Mem.Entity(0x005edebc);
                    Mem.s_Unknown_006008d0 += -(sceneCamera[0] + Mem.s_Unknown_006008d0) >> 2;
                    Mem.s_Unknown_006008d4 += (sceneCamera[1] - Mem.s_Unknown_006008d4) >> 2;
                    Mem.s_Unknown_006008d8 += -(sceneCamera[2] + Mem.s_Unknown_006008d8) >> 2;

                    int* target = *(int**)Mem.Entity(0x005eca94);
                    Mem.CameraTranslation[0] = *Mem.Camera(0x005de0a8) + target[0];
                    Mem.CameraTranslation[1] = target[1] + *Mem.Camera(0x005de0ac);
                    Mem.CameraTranslation[2] = target[2] + *Mem.Camera(0x005de0b0);

                    Winsys3d.Ordinal_378(0xa0, 0x78);   // libgte SetGeomOffset(160, 120)

                    // La moitie dessin. Elle avait ete desactivee le 2026-08-22 parce qu'elle
                    // corrompait la memoire ; la cause est levee depuis, et elle etait triple :
                    // le bloc de sprite fait **sept champs sur 0x12 octets** et non trois entiers,
                    // les descripteurs d'atlas s'**indexent en avant** donc se relevent par plage,
                    // et Ghidra place deux champs au mauvais decalage. Les trois sont etablies au
                    // desassembleur — voir Primitives2d.DrawSpritePrimitive.
                    //
                    // Ce qui reste est de la lacune franche : cinq appelees non transcrites qui
                    // levent chacune sous leur nom, ce que le depot veut.
                    DrawWorldMapFrame(otBase, otEntry, &primCursor, debugLatch, mainMem);
                    goto frameTail;
                }

                // =========================================================================
                // LA BRANCHE COURTE D'AFFICHAGE — 0x005179da..0x00517a77.
                //
                // Le menu de jeu est ouvert (le cas 1 du repartiteur vient de poser le bit 4 de
                // DAT_005de02c). Le monde n'est pas dessine du tout : UN seul appel remplace les
                // ~700 lignes du bloc principal, et la fenetre de rendu se retrecit de 320x240 a
                // **256x224** le temps du menu — ce sont les quatre ecritures de la fin, que la
                // queue de boucle remet a 320x240 juste apres.
                //
                // Ghidra: `(&DAT_005e9188)[(byte)(&DAT_005e93c8)[DAT_005de540 * 0x9c] * 0x4c]`.
                // **DEUX indirections**, et non une seule comme au site de PTR_FUN_0056aff0 vingt
                // lignes plus haut : l'emplacement de manette se lit ici a travers l'index de
                // personnage porte par la fiche (+8), pas directement depuis DAT_005de540. Les
                // deux chaines sont differentes dans le binaire, et chacune est transcrite telle
                // qu'elle y est ecrite.
                // =========================================================================
                byte* menuSlot = Inp.s_SlotFlags_005e93c0 + (uint)s_Unknown_005de540 * 0x9c;
                byte* menuPad = Inp.s_InputStateBase_00604a20
                                + Inp.s_PlayerSlots_005e9188[menuSlot[8] * 0x4c] * 0x10;

                if (debugLatch == 0 && (Mem.s_WorldFlags_005de02c & 1) != 0)
                {
                    // Le seul chemin de cette branche qui ne mene PAS au menu de jeu : le menu de
                    // mise au point, a la place. Le troisieme argument est `lea ecx,[esp+38h]`
                    // apres un `push` — [esp+0x34] avant lui, le bloc de sprite de la trame. Les
                    // deux chemins se rejoignent en 0x00517a77 : les quatre ecritures 256x224 qui
                    // suivent valent pour l'un comme pour l'autre.
                    primCursor = RunDebugMenuFrame_0051AA30(
                        otBase, primCursor, spriteBlock, (ushort*)menuPad);
                }
                else
                {
                    primCursor = RunPartyMenuFrame_0052A7D0(
                        otBase, otEntry, primCursor, menuSlot, (ushort*)menuPad);
                }

                *(ushort*)Mem.Arena(0x005e9950) = 0x100;
                *(ushort*)Mem.Arena(0x005e98e0) = 0x100;
                *(ushort*)Mem.Arena(0x005e9952) = 0xe0;
                *(ushort*)Mem.Arena(0x005e98e2) = 0xe0;
                goto frameTail;
            }

        frameTail:
            // ================= queue de boucle, commune a tous les chemins =================
            // Ghidra: FUN_0052a280(&local_60,&uStack_54,&stack0xffffff9c,3,1). Le cinquieme
            // argument est EBP au niveau machine : il vaut 1 sur les deux chemins courts et le
            // curseur de primitives sur les autres. FrameEnd ne le compare qu'a zero, et le
            // curseur n'est jamais nul, donc le 1 rendu par Ghidra est exact en effet.
            //
            // Les trois emplacements sont ceux de FrameBegin, dans le meme ordre : le
            // desassemblage pose les memes `lea [esp+0x14] / [esp+0x20] / [esp+0x10]` aux deux
            // sites. Ghidra les nomme differemment aux deux endroits — un de ses noms est decale
            // de quatre octets — et c'est le binaire qui tranche.
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);

            *(ushort*)Mem.Arena(0x005e9950) = 0x140;
            *(ushort*)Mem.Arena(0x005e98e0) = 0x140;
            *(ushort*)Mem.Arena(0x005e9952) = 0xf0;
            *(ushort*)Mem.Arena(0x005e98e2) = 0xf0;
        }
    }

    // =====================================================================
    // Les deux crochets indirects de la boucle par image.
    //
    // `PTR_FUN_0056afe8` et `PTR_FUN_0056aff0` **ne sont pas des globales isolees** : ce sont les
    // entrees 8 et 10 de la table 0x0056afc8, la quatrieme des sept tables de sauts d'entites que
    // `InstallEntityVTables` (Engine/Resources.cs) recopie au chargement de zone.
    // (0x0056afe8 - 0x0056afc8) / 4 = 8, et (0x0056aff0 - 0x0056afc8) / 4 = 10. La table de
    // l'index 0 — le seul que la carte du monde atteint — est deja relevee dans le depot, et elle
    // y donne 0x0053DCB0 et 0x0053DF90, deux VRAIES fonctions et non la souche 0x00401FA0.
    //
    // Les deux sont **absentes de `Blaze.c`** : Ghidra ne decompile pas ce dont seule l'adresse est
    // prise. Elles ont ete desassemblees ; les relevés sont sous `Legacy/Raw/Blaze/`.
    //
    // Le dispatch se fait sur la VALEUR d'adresse, le patron de `CallAreaHandler` et de
    // `ScriptOpAreaVerb` : toute autre cible leve en se nommant.
    // =====================================================================

    /// <summary>Ghidra: <c>(*PTR_FUN_0056afe8)()</c>, en tete du cas 0 du repartiteur.</summary>
    private static void CallEntityHook_0056afe8()
    {
        uint target = Res.s_Vt_0056afc8[8];
        switch (target)
        {
            case 0x0053DCB0: ResetPerCharacterFrameState(); break;

            // Souche `ret` nu — elle ne rend rien et l'appelant ne lit rien ici.
            case 0x00401FA0: break;

            default:
                throw new NotImplementedException(
                    $"FUN_00516fc0, cas 0 : le crochet PTR_FUN_0056afe8 vaut 0x{target:X6}, pas " +
                    "encore reconstruit. C'est l'entree 8 de s_Vt_0056afc8 (Engine/Resources.cs) ; " +
                    $"`OpenForesia.Tools disasm Legacy/Binaries/Blaze.exe 0x{target:x8} 512` si " +
                    "elle n'est pas dans Blaze.c. Elle ne prend aucun argument.");
        }
    }

    /// <summary>
    /// Ghidra: <c>(*PTR_FUN_0056aff0)(local_60, unaff_EBX, …)</c> — la vue de remplacement de la
    /// carte du monde, appelee quand <c>DAT_005de02c</c> porte le bit <c>0x40</c>. Aucun code deja
    /// transcrit ne pose ce bit, donc cette branche n'est pas atteinte aujourd'hui.
    /// </summary>
    private static uint* CallEntityHook_0056aff0(uint* otBase, uint* prim, byte* slotRecord, byte* pad)
    {
        uint target = Res.s_Vt_0056afc8[10];
        return target switch
        {
            // L'entree 10 pour la zone d'index 0 : le crochet du coffre, transcrit le 2026-08-25.
            0x0053DF90 => EntityHook_0053DF90(otBase, prim, slotRecord, pad),

            // Souche `ret` nu : elle ne touche pas EAX, qui porte encore l'adresse du dernier
            // argument empile — `&DAT_005e93c0 + index * 0x27`. Le `mov [esp+10h],eax` de
            // l'appelant en fait donc le nouveau curseur de primitives. C'est absurde et c'est ce
            // que fait le processeur ; la meme forme est deja documentee sur ScriptOpAreaVerb.
            0x00401FA0 => (uint*)slotRecord,

            _ => throw new NotImplementedException(
                $"FUN_00516fc0 : le crochet PTR_FUN_0056aff0 vaut 0x{target:X6}, pas encore " +
                "reconstruit. C'est l'entree 10 de s_Vt_0056afc8 (Engine/Resources.cs), " +
                "0x0053DF90 pour la zone d'index 0 — absente de Blaze.c, relevee dans " +
                "Legacy/Raw/Blaze/FUN_0053DF90.asm. Elle prend (otBase, primitives, " +
                "enregistrement d'emplacement, etat de manette) et rend le curseur de primitives."),
        };
    }
}
