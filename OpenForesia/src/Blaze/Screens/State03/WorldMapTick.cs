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
            s_Unknown_005dbc14 = (byte)Res.s_Unknown_005de528;
            s_Unknown_005ddfd0 = s_Unknown_005de52c;
            s_Unknown_005dbc18 = (byte)s_PartySize_005e9020;
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
        Inp.JoystickResetState(false);                // Ghidra: FUN_00504b10(0)

        // =============================================================================
        // La boucle par image. Le `do { … } while(true)` de 0x00517163 : on n'en sort que par le
        // `return` de la branche DAT_005de02c & 2, ou par une exception.
        // =============================================================================
        while (true)
        {
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
                throw new NotImplementedException(
                    "FUN_00516fc0 : l'affichage de mise au point de la carte du monde (0x0051718c " +
                    "a 0x005174d0) n'est pas transcrit. Il pose une trentaine d'appels a " +
                    "FUN_0051c120 / FUN_0051be90 puis recopie les trois bancs de drapeaux " +
                    "(DAT_005e96e0, DAT_005e9840, DAT_005e9000) vers leurs miroirs d'image " +
                    "precedente. DAT_005de020 vaut 0 au chargement — releve dans le binaire, " +
                    "queue non initialisee de .data — donc ce bloc ne s'atteint que par la touche " +
                    "de mise au point qui le bascule (Blaze.c 66241/66244), elle-meme non " +
                    "transcrite. FUN_0051be90 est le seul appele qui manque.");
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
                    throw new NotImplementedException(
                        "FUN_00516fc0 : le bloc d'affichage principal (0x00517aa7 a 0x00519703) " +
                        "n'est pas transcrit — c'est le gros du corps, environ 700 lignes de " +
                        "pseudo-C : lissage de camera, choix du personnage suivi, panneaux d'etat " +
                        "des quatre membres, puis le balayage 8x7 des cellules de la carte qui " +
                        "emet les modeles et les particules. Il commence par FUN_004011c0, " +
                        "FUN_00532b40 et deux FUN_00533000. Voir " +
                        "Legacy/Raw/Blaze/FUN_00516FC0-full.asm.");
                }

                throw new NotImplementedException(
                    "FUN_00516fc0 : la branche courte d'affichage (0x005179da a 0x00517a77), " +
                    "prise quand DAT_005de02c porte le bit 4 sans que le pilotage libre soit " +
                    "actif. Il lui manque FUN_0051aa30 (594 lignes) et FUN_0052a7d0 (20 lignes).");
            }

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

    /// <summary>
    /// Ghidra: FUN_0053dcb0 @ 0x0053dcb0 — <b>absente de <c>Blaze.c</c></b> (Ghidra ne decompile pas
    /// une fonction dont seule l'adresse est prise), transcrite depuis
    /// <c>Legacy/Raw/Blaze/FUN_0053DCB0.asm</c>. Ouvre le pas de simulation : pour chaque membre du
    /// groupe, elle fait glisser la paire de mots de <c>+0x148</c> vers <c>+0x14c</c> — l'instantane
    /// « image precedente » que le bloc d'affichage relit — remet a zero le dword de <c>+0x140</c>
    /// et les 0x80 octets de <c>+0x180</c>.
    ///
    /// <para>Le bit 3 de <c>DAT_005de02c</c> qu'elle pose vient d'etre efface par son appelant, deux
    /// instructions plus haut. Transcrit tel quel.</para>
    /// </summary>
    private static void ResetPerCharacterFrameState()
    {
        byte* mainMem = GetMainMemPtr();

        *(byte*)Mem.Camera(0x005de044) = 0;
        Mem.s_WorldFlags_005de02c |= 8;

        for (int i = 0; i < s_PartySize_005e9020; i++)
        {
            byte* block = mainMem + 0xf0000 + 0x148 + i * 0x2000;
            *(ushort*)(block + 4) = *(ushort*)(block + 0);
            *(ushort*)(block + 6) = *(ushort*)(block + 2);
            *(int*)(block - 8) = 0;
            for (int k = 0; k < 0x20; k++) ((uint*)(block + 0x38))[k] = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e2b0 @ 0x0050e2b0 — le tri des membres du groupe en tete de chaque image :
    /// qui est jouable, qui est hors-jeu, et qui mene.
    ///
    /// <para>Trois compteurs en sortent, tous des octets (confirme au desassemblage — cinq
    /// <c>mov byte [.],0</c> a l'entree) : <c>DAT_005de034</c> le nombre de membres encore
    /// jouables, <c>DAT_005de544</c> le nombre de membres ecartes, et <c>DAT_005de518</c> ceux
    /// portant le bit <c>0x40000000</c>. Quand tous les membres sont ecartes ET tous portent ce
    /// bit, la partie bascule vers l'etat de jeu 9 avec un pas de boucle a <c>-2</c>.</para>
    ///
    /// <para>Deux index distincts sont suivis en parallele, et la pseudo-C les melange :
    /// <c>EBX</c> retient le dernier membre jouable rencontre (il devient le meneur quand il n'y
    /// en a qu'un), tandis que <c>[ESP+0x14]</c> retient le precedent pour l'empiler dans la liste
    /// <c>DAT_005edec0</c>. Tous deux partent de <c>DAT_005de538</c>.</para>
    ///
    /// <para>Placee ici et non dans <c>Engine/</c> : la carte du monde est aujourd'hui son unique
    /// appelant transcrit. Elle remontera quand <c>FUN_0050bf20</c> arrivera.</para>
    /// </summary>
    private static void PrepareLeaderAndPartyFlags()
    {
        byte* mainMem = GetMainMemPtr();

        s_Unknown_005de518 = 0;
        *(byte*)Mem.Camera(0x005de030) = 0;
        s_Unknown_005de544 = 0;
        *(byte*)Mem.Camera(0x005de034) = 0;
        *Mem.Entity(0x005f2ce4) = 0;

        // Les deux index suivis, tous deux amorces sur le meneur courant.
        uint lastPlayable = (uint)(byte)Mem.s_Unknown_005de538;
        uint previousPlayable = lastPlayable;

        // -1 tant qu'aucun membre « disponible » n'a ete vu ; sinon l'index du premier.
        int firstAvailable = -1;

        for (int index = 0; index < s_PartySize_005e9020; index++)
        {
            uint* flags = (uint*)(Inp.s_SlotFlags_005e93c0 + index * 0x9c);
            byte slotKind = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            byte* pad = Inp.s_InputStateBase_00604a20 + slotKind * 0x10;
            byte* perCharacter = mainMem + 0xf02b8 + index * 0x2000;

            if ((pad[0xb] & 0x80) == 0)
            {
                // Manette absente : le membre est ecarte du jeu.
                *flags = (*flags & 0xf7ffffff) | 0x10000;
                *perCharacter = 1;
            }
            else if (firstAvailable == -1)
            {
                firstAvailable = index;
            }

            if (slotKind < 8)
            {
                uint value = *flags | 0x20000;
                *flags = value;
                if ((value & 0x10000) != 0 && (pad[0] & 0xf0) != 0)
                {
                    *flags = (value & 0xfffeffff) | 0x20000;
                    *perCharacter = 0;
                }

                value = *flags;
                if ((value & 0x10000) == 0)
                {
                    (*(byte*)Mem.Camera(0x005de034))++;
                    lastPlayable = (uint)index;
                }
                else if ((value & 0x40000000) != 0)
                {
                    *flags = value & 0xf7ffffff;
                }
            }
            else if ((byte)Mem.s_Unknown_005de538 != index)
            {
                *flags = (*flags & 0xf7ffffff) | 0x10000;
                *perCharacter = 1;
            }

            uint state = *flags;
            if ((state & 0x50200000) == 0 && *(short*)((byte*)flags + 0x12) == 0)
            {
                if ((state & 0x10000) != 0 && (*(uint*)(perCharacter - 0x178) & 0x1c0) == 0)
                {
                    Mem.Entity(0x005edec0)[*Mem.Entity(0x005f2ce4)] = (byte)previousPlayable;
                    (*Mem.Entity(0x005f2ce4))++;
                    previousPlayable = (uint)index;
                }
            }
            else
            {
                s_Unknown_005de544++;
                if ((state & 0x40000000) != 0) s_Unknown_005de518++;
                if ((state & 0x10200000) != 0) (*(byte*)Mem.Camera(0x005de030))++;
            }
        }

        if (*(byte*)Mem.Camera(0x005de034) == 0)
        {
            // Plus personne de jouable : on retombe sur le premier membre disponible, ou sur le
            // membre 0 si meme celui-la n'existe pas.
            byte fallback = firstAvailable < 0 ? (byte)0 : (byte)firstAvailable;
            s_Unknown_005de540 = fallback;
            Mem.s_Unknown_005de538 = fallback;

            *(uint*)(Inp.s_SlotFlags_005e93c0 + fallback * 0x9c) &= 0xfffeffff;
            *(mainMem + 0xf02b8 + fallback * 0x2000) = 0;
            *(byte*)Mem.Camera(0x005de034) = 1;
        }
        else if (*(byte*)Mem.Camera(0x005de034) == 1)
        {
            Mem.s_Unknown_005de538 = (byte)lastPlayable;
            s_Unknown_005de540 = (byte)lastPlayable;
        }

        if (*(byte*)Mem.Camera(0x005de030) >= s_PartySize_005e9020)
        {
            Mem.s_WorldFlags_005de02c |= 2;
            if (s_Unknown_005de518 >= s_PartySize_005e9020)
            {
                s_WorldMapStep_005645f8 = -2;
                s_GameStateId_005de510 = 9;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e520 @ 0x0050e520 — passe le role de meneur au premier membre du groupe qui
    /// ne porte ni le masque demande ni les bits <c>0x400001c0</c> de son bloc par personnage.
    ///
    /// <para>Quand le membre retenu etait lui-meme ecarte (<c>0x10000</c>), la fonction le
    /// reintegre et <b>echange</b> deux octets — <c>+8</c> et <c>+9</c> de l'entree
    /// <c>DAT_005e9180</c> — entre l'ancien meneur et le nouveau. Ce sont l'emplacement de manette
    /// et l'index de personnage : c'est un echange de sieges, pas une copie.</para>
    ///
    /// <para>Trois autres appelants existent dans <c>Blaze.c</c> (<c>FUN_0050bf20</c> deux fois,
    /// <c>FUN_0052a850</c> une fois), aucun n'est encore transcrit. Elle rejoindra
    /// <c>Engine/</c> quand ce sera le cas.</para>
    /// </summary>
    private static void PassLeaderToNextEligible(uint* currentFlags, byte* currentSlot, uint mask)
    {
        // L'original teste ensuite `0 < DAT_005e9020` une seconde fois ; le test est mort, la garde
        // ci-dessus ayant deja rendu la main pour tout groupe de moins de deux membres.
        if (s_PartySize_005e9020 <= 1 || *(byte*)Mem.Camera(0x005de034) == 0) return;

        byte* mainMem = GetMainMemPtr();
        int index = 0;
        uint* flags = (uint*)Inp.s_SlotFlags_005e93c0;
        byte* slot = Inp.s_SlotArea_005e9180;
        uint* perCharacter = (uint*)(mainMem + 0xf0140);

        while ((*flags & mask) != 0 || (*perCharacter & 0x400001c0) != 0)
        {
            index++;
            flags = (uint*)((byte*)flags + 0x9c);
            slot += 0x4c;
            perCharacter = (uint*)((byte*)perCharacter + 0x2000);
            if (s_PartySize_005e9020 <= index) return;
        }

        Mem.s_Unknown_005de538 = (byte)index;
        *flags &= 0xf7ffffff;
        *currentFlags &= 0xf7ffffff;

        if ((*flags & 0x10000) != 0)
        {
            *flags &= 0xfffeffff;

            byte previousIndex = currentSlot[9];
            byte previousPad = currentSlot[8];
            currentSlot[8] = slot[8];
            currentSlot[9] = slot[9];
            slot[8] = previousPad;
            slot[9] = previousIndex;
        }
    }

    // =====================================================================
    // Les scalaires isoles que la boucle par image touche hors arene.
    // =====================================================================

    /// <summary>Ghidra: DAT_005de020 — le drapeau d'affichage de mise au point, bascule par une
    /// touche (Blaze.c 66241/66244, non transcrite). <b>Zero au chargement</b> : l'adresse tombe
    /// dans la queue non initialisee de <c>.data</c>, verifie avec
    /// <c>OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005de018</c>.</summary>
    private static byte s_DebugOverlayEnabled_005de020;

    /// <summary>Ghidra: DAT_005de540 — l'index du membre du groupe que le pilotage de camera et
    /// l'affichage suivent. Pose en meme temps que <c>DAT_005de538</c> par
    /// <see cref="PrepareLeaderAndPartyFlags"/>, mais relu separement : les deux divergent des que
    /// <see cref="PassLeaderToNextEligible"/> agit, qui ne touche que le second.</summary>
    private static byte s_Unknown_005de540;

    /// <summary>Ghidra: DAT_005de518 et DAT_005de544 — les deux compteurs d'octets que
    /// <see cref="PrepareLeaderAndPartyFlags"/> reconstruit a chaque image : combien de membres
    /// portent le bit <c>0x40000000</c>, et combien sont ecartes du jeu pour une raison ou une
    /// autre. Quand les deux atteignent la taille du groupe, la partie est finie.</summary>
    private static byte s_Unknown_005de518, s_Unknown_005de544;

    /// <summary>Ghidra: DAT_005de6f8 / 6fc / 704 / 70c / 710 / 718 — six dwords remis a l'etat neuf
    /// par le cas 1 du repartiteur, trois a zero et trois a -1. Les trous (0x700, 0x708, 0x714)
    /// portent autre chose, que rien de transcrit ne touche encore : declares un par un plutot
    /// qu'en bloc, faute de connaitre l'etendue reelle. TODO(REVERSE) : leurs roles.</summary>
    private static int s_Unknown_005de6f8, s_Unknown_005de6fc, s_Unknown_005de704;
    private static int s_Unknown_005de70c, s_Unknown_005de710, s_Unknown_005de718;

    /// <summary>Ghidra: DAT_005e8ed8 et DAT_005e8eda — deux <c>short</c> (confirme au
    /// desassemblage : <c>mov [5E8ED8h],cx</c>) ou le cas 1 depose la position du meneur reduite au
    /// quart et bornee a la fenetre de la carte. Sous <c>0x005e9000</c>, donc hors GameArena.</summary>
    private static short s_Unknown_005e8ed8, s_Unknown_005e8eda;

    // =====================================================================
    // LE PAS DE SIMULATION DU MONDE — les appelees du cas 0 (0x005176d5..0x0051776b).
    //
    // Elles restent ici plutot que de monter dans Engine/ : la regle de routage (4) d'
    // ARCHITECTURE.md les laisse chez leur appelant tant qu'un seul ecran les appelle, et c'est le
    // cas — la carte du monde est aujourd'hui le seul chemin transcrit qui y mene.
    //
    // **Six d'entre elles sont la meme passe sur six tables differentes.** C'est la decouverte qui
    // rend ce bloc court : les tables 0x005fa5e0, 0x005ecaa0, 0x005f7de0, 0x005ff400, 0x005edee0 et
    // 0x005f2d40 portent toutes des enregistrements de **0x9c octets** avec la meme disposition de
    // tete — mot d'etat a +0x00, indice d'emplacement a +0x08, position a +0x1c/+0x20/+0x24,
    // chainage de grille a +0x68 — et chaque passe fait les memes trois gestes : ignorer
    // l'enregistrement inactif, appeler son gestionnaire par table de sauts, puis le raccrocher a
    // la grille spatiale. Ce qui les distingue tient dans le corps du gestionnaire, pas dans la
    // passe.
    //
    // Le raccrochage est <see cref="LinkIntoWorldGrid"/>, et les deux grilles qu'il alimente sont
    // exactement les deux pages de 0x400 dwords que le prologue et le cas 0 remettent a zero :
    // 0x3880 * 0x20 * 4 = 0x1c4000 et 0x38a0 * 0x20 * 4 = 0x1c5000. Le rapprochement n'etait pas
    // evident dans la pseudo-C, qui n'ecrit que les constantes 0x3880 et 0x38a0.
    // =====================================================================

    /// <summary>
    /// Le raccrochage d'une entite a une des deux <b>grilles spatiales 32x32</b> de MainMem : la
    /// cellule est deduite de la position monde, et l'entite est poussee en tete de la liste
    /// simplement chainee de cette cellule.
    ///
    /// <para>
    /// <c>((v &gt;&gt; 12) + 0x2000) &gt;&gt; 9</c> : la position est en 20.12, donc <c>v &gt;&gt; 12</c> est en unites
    /// monde ; <c>+0x2000</c> recentre la plage <c>[-0x2000, 0x2000[</c> sur zero et <c>&gt;&gt; 9</c> la
    /// divise en 32 cases de 512. Le test d'appartenance est fait <b>en non signe</b> dans
    /// l'original, ce qui rejette d'un coup les cellules negatives ; transcrit tel quel.
    /// </para>
    /// <para>
    /// Le chainage stocke une <b>adresse</b> dans une case de quatre octets. C'est exact ici parce
    /// que la reconstruction est batie en x86 (<c>PlatformTarget x86</c> dans OpenForesia.csproj),
    /// donc un pointeur y fait bien quatre octets, comme en 1998.
    /// </para>
    /// </summary>
    /// <param name="gridOffset">0x1c4000 ou 0x1c5000 — l'origine de la grille dans MainMem.</param>
    private static void LinkIntoWorldGrid(byte* mainMem, int gridOffset, byte* record)
    {
        uint cellX = (uint)(((*(int*)(record + 0x1c) >> 0xc) + 0x2000) >> 9);
        uint cellZ = (uint)(((*(int*)(record + 0x24) >> 0xc) + 0x2000) >> 9);
        if (cellX >= 0x20 || cellZ >= 0x20) return;

        byte* cell = mainMem + gridOffset + (int)(cellZ * 0x20 + cellX) * 4;
        *(uint*)(record + 0x68) = *(uint*)cell;
        *(uint*)cell = (uint)(nuint)record;
    }

    /// <summary>Le pas d'amortissement des angles du cas 0 : le delta est deja ramene dans
    /// <c>[0, 0x1000[</c> par un <c>&amp; 0xfff</c>, et la moitie haute veut dire « dans l'autre
    /// sens ».</summary>
    private static short SmoothWrappedAngleStep(ushort delta)
        => delta < 0x800
            ? (short)((short)delta >> 3)
            : (short)-((0x1000 - (short)delta) >> 3);

    /// <summary>Le pas d'amortissement du cas 2, ou le delta n'est <b>pas</b> masque et a deja subi
    /// un decalage de trois sur 32 bits. La comparaison a <c>0x800</c> est signee — confirme au
    /// desassembleur (<c>cmp ax,800h ; jge</c>) et non deduit du texte de Ghidra.</summary>
    private static short SmoothSignedAngleStep(short delta)
        => delta < 0x800
            ? (short)(delta >> 3)
            : (short)-((0x1000 - delta) >> 3);

    /// <summary>
    /// Ghidra: FUN_00532bf0 @ 0x00532bf0 — <b>le lissage de la vue</b> : l'orientation de scene et la
    /// couleur de fond glissent chacune vers leur consigne, d'un huitieme de l'ecart par image.
    ///
    /// <para>
    /// Les deux moities sont batties sur le meme patron, avec un mode a trois etats
    /// (<c>DAT_005e902e</c> pour l'orientation, <c>DAT_005e9166</c> pour la couleur) :
    /// <b>0</b> glisse vers la consigne et met le resultat de cote, <b>1</b> restitue simplement ce
    /// qui a ete mis de cote, <b>2</b> glisse vers ce qui a ete mis de cote. Autrement dit : suivre,
    /// figer, revenir.
    /// </para>
    /// <para>
    /// Quand le pas calcule tombe a zero, la valeur <b>saute</b> directement a la consigne au lieu
    /// de rester bloquee a un ecart de moins de huit. C'est ce qui fait converger le lissage, et ce
    /// n'est pas un cas particulier ecrit a part : c'est la valeur d'initialisation de la variable
    /// avant le <c>if</c>.
    /// </para>
    /// <para>
    /// <b>Les deux moities ne comptent pas pareil.</b> L'orientation masque son ecart par
    /// <c>&amp; 0xfff</c> — ce sont des angles, ils font le tour — la ou la couleur ne masque rien et
    /// decale son ecart <b>deux fois</b> de trois. Verifie au desassembleur (0x00532bf0 et
    /// 0x00532e38) avant transcription : la difference est reelle, pas un artefact de Ghidra.
    /// </para>
    /// </summary>
    private static void SmoothSceneOrientationAndBackColour()
    {
        // Ghidra: DAT_005e9028 / 902a / 902c / 902e — l'orientation mise de cote, puis le mode.
        // Quatre shorts contigus, confirmes par `movsx eax,word [5E902Eh]` en tete de fonction.
        short* saved = (short*)Mem.Arena(0x005e9028);

        // Ghidra: DAT_005e9310 / 9312 pour la paire, DAT_005e9314 pour le roulis.
        short* orientTarget = (short*)Mem.Arena(0x005e9310);

        switch (saved[3])
        {
            case 0:
            {
                saved[2] = orientTarget[2];

                short stepPitch = SmoothWrappedAngleStep((ushort)((orientTarget[0] - Mem.s_ScenePitch_005e9308) & 0xfff));
                short stepYaw = SmoothWrappedAngleStep((ushort)((orientTarget[1] - Mem.s_SceneYaw_005e930a) & 0xfff));
                short stepRoll = SmoothWrappedAngleStep((ushort)((saved[2] - Mem.s_SceneRoll_005e930c) & 0xfff));

                short pitch = orientTarget[0];
                if (stepPitch != 0)
                {
                    Mem.s_ScenePitch_005e9308 += stepPitch;
                    pitch = Mem.s_ScenePitch_005e9308;
                }

                short yaw = orientTarget[1];
                if (stepYaw != 0)
                {
                    Mem.s_SceneYaw_005e930a += stepYaw;
                    yaw = Mem.s_SceneYaw_005e930a;
                }

                Mem.s_ScenePitch_005e9308 = pitch;
                Mem.s_SceneYaw_005e930a = yaw;

                if (stepRoll != 0)
                {
                    Mem.s_SceneRoll_005e930c += stepRoll;
                    saved[2] = Mem.s_SceneRoll_005e930c;
                }

                Mem.s_SceneRoll_005e930c = saved[2];

                // Ghidra: `DAT_005e9028 = _DAT_005e9308;` — une copie de DWORD, donc les deux
                // premiers shorts seulement. Le roulis mis de cote l'a deja ete ci-dessus.
                saved[0] = Mem.s_ScenePitch_005e9308;
                saved[1] = Mem.s_SceneYaw_005e930a;
                break;
            }

            case 1:
                Mem.s_ScenePitch_005e9308 = saved[0];
                Mem.s_SceneYaw_005e930a = saved[1];
                Mem.s_SceneRoll_005e930c = saved[2];
                break;

            case 2:
            {
                short stepPitch = SmoothSignedAngleStep((short)((saved[0] - Mem.s_ScenePitch_005e9308) >> 3));
                short stepYaw = SmoothSignedAngleStep((short)((saved[1] - Mem.s_SceneYaw_005e930a) >> 3));
                short stepRoll = SmoothSignedAngleStep((short)((saved[2] - Mem.s_SceneRoll_005e930c) >> 3));

                short pitch = saved[0];
                if (stepPitch != 0) pitch = (short)(stepPitch + Mem.s_ScenePitch_005e9308);

                short yaw = saved[1];
                if (stepYaw != 0) yaw = (short)(stepYaw + Mem.s_SceneYaw_005e930a);

                short roll = stepRoll == 0
                    ? saved[2]
                    : (short)(stepRoll + Mem.s_SceneRoll_005e930c);

                Mem.s_ScenePitch_005e9308 = pitch;
                Mem.s_SceneYaw_005e930a = yaw;
                Mem.s_SceneRoll_005e930c = roll;
                break;
            }
        }

        // Ghidra: DAT_005e9160 / 9162 / 9164 / 9166 — meme disposition pour la couleur de fond :
        // trois shorts mis de cote, puis le mode.
        short* savedColour = (short*)Mem.Arena(0x005e9160);
        short* colourTarget = (short*)Mem.Arena(0x005e92b8);

        switch (savedColour[3])
        {
            case 0:
            {
                short stepR = (short)((colourTarget[0] - Mem.s_BackColourR_005e93a0) >> 3);
                short stepG = (short)((colourTarget[1] - Mem.s_BackColourG_005e93a2) >> 3);
                short stepB = (short)((colourTarget[2] - Mem.s_BackColourB_005e93a4) >> 3);

                short red = colourTarget[0];
                if (stepR != 0) red = (short)(stepR + Mem.s_BackColourR_005e93a0);

                short green = colourTarget[1];
                if (stepG != 0) green = (short)(stepG + Mem.s_BackColourG_005e93a2);

                Mem.s_BackColourR_005e93a0 = red;
                Mem.s_BackColourG_005e93a2 = green;

                savedColour[2] = colourTarget[2];
                if (stepB != 0) savedColour[2] = (short)(stepB + Mem.s_BackColourB_005e93a4);

                savedColour[0] = Mem.s_BackColourR_005e93a0;
                savedColour[1] = Mem.s_BackColourG_005e93a2;
                Mem.s_BackColourB_005e93a4 = savedColour[2];
                break;
            }

            case 1:
                Mem.s_BackColourR_005e93a0 = savedColour[0];
                Mem.s_BackColourG_005e93a2 = savedColour[1];
                Mem.s_BackColourB_005e93a4 = savedColour[2];
                break;

            case 2:
            {
                short stepR = (short)((savedColour[0] - Mem.s_BackColourR_005e93a0) >> 3);
                short stepG = (short)((savedColour[1] - Mem.s_BackColourG_005e93a2) >> 3);
                short stepB = (short)((savedColour[2] - Mem.s_BackColourB_005e93a4) >> 3);

                short red = savedColour[0];
                if (stepR != 0) red = (short)(stepR + Mem.s_BackColourR_005e93a0);

                short green = savedColour[1];
                if (stepG != 0) green = (short)(stepG + Mem.s_BackColourG_005e93a2);

                Mem.s_BackColourR_005e93a0 = red;
                Mem.s_BackColourG_005e93a2 = green;

                Mem.s_BackColourB_005e93a4 = stepB == 0
                    ? savedColour[2]
                    : (short)(stepB + Mem.s_BackColourB_005e93a4);
                break;
            }
        }

        Winsys3d.Ordinal_376(
            (byte)Mem.s_BackColourR_005e93a0,
            (byte)Mem.s_BackColourG_005e93a2,
            (byte)Mem.s_BackColourB_005e93a4);
    }

    /// <summary>
    /// Ghidra: FUN_0050e5e0 @ 0x0050e5e0 — la passe sur la table <c>DAT_005fa5e0</c>, 0x80
    /// enregistrements. La plus simple des six : gestionnaire, puis raccrochage a la grille.
    ///
    /// <para>
    /// C'est la seule des six qui alimente la grille de <b>0x1c4000</b> ; les cinq autres vont dans
    /// celle de 0x1c5000. Les deux pages sont remises a zero cote a cote en tete de chaque pas,
    /// donc la separation est deliberee.
    /// </para>
    /// <para>
    /// Les deux ecritures qui suivent le gestionnaire — un <c>ushort</c> a <c>+0x96</c> remis a zero
    /// et le masque <c>&amp; 0xffffff8f</c> sur le mot d'etat — effacent la commande que le
    /// gestionnaire vient de consommer.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005fa5e0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005fa5e0);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0 && record[5] != 0)
            {
                DispatchEntityHandler(
                    Res.ResolveEntityHandler(0x0056adc8, record[5]), record,
                    $"FUN_0050e5e0, table 0x005fa5e0 entree {i}, sous-type {record[5]}");

                *(ushort*)(record + 0x96) = 0;
                *(uint*)record &= 0xffffff8f;
            }

            LinkIntoWorldGrid(mainMem, 0x1c4000, record);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e680 @ 0x0050e680 — la passe sur la table <c>DAT_005ecaa0</c>, 0x20
    /// enregistrements. La plus fournie des six : c'est elle qui porte le <b>clignotement</b>, la
    /// <b>poursuite de cible</b> et l'<b>evanouissement</b> des entites de scene.
    ///
    /// <para>
    /// Le compte a rebours de <c>+0x96</c> est un clignotement : sa parite choisit entre deux
    /// couples (mode 1 / blanc et mode 5 / rouge) ecrits a <c>+0x36</c> et <c>+0x38</c>. Quand il
    /// atteint zero et que l'entite portait <c>0x40000000</c>, elle est rendue au pool et la passe
    /// saute a l'enregistrement suivant — le <c>goto LAB_0050eaf7</c> de Ghidra, rendu ici par un
    /// <c>continue</c>.
    /// </para>
    /// <para>
    /// <b>La boucle de poursuite est ecrite telle quelle, y compris son inefficacite.</b> Elle
    /// balaie tout le groupe et reaffecte la cible a <b>chaque</b> membre eligible rencontre, sans
    /// s'arreter au premier : c'est donc le dernier qui gagne. Rien dans le binaire n'interrompt la
    /// boucle, et ce n'est pas transcrit autrement.
    /// </para>
    /// <para>
    /// Le bloc central publie onze champs de l'entite dans des globales, appelle le gestionnaire de
    /// la table 0x0056ad48, puis les relit — mais seulement si <c>+0x90</c> porte l'un des bits de
    /// <c>0x2000a88a</c>. C'est une convention d'appel par globales, comme celle deja rencontree
    /// autour de <c>DAT_005fa4e0</c>.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005ecaa0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0 && s_Unknown_00571a3c == 0)
            {
                if (s_Unknown_00564540 == 0)
                {
                    s_Unknown_00564540 = (CrtRand() % 6 + 10) * 0x14;
                }

                if (*(ushort*)(record + 0x96) != 0)
                {
                    ushort blink = (ushort)(*(ushort*)(record + 0x96) - 1);
                    *(ushort*)(record + 0x96) = blink;

                    if ((blink & 1) == 0)
                    {
                        *(ushort*)(record + 0x36) = 1;
                        *(uint*)(record + 0x38) = 0xffffff;
                    }
                    else
                    {
                        *(ushort*)(record + 0x36) = 5;
                        *(uint*)(record + 0x38) = 0xff;
                    }

                    if ((*(uint*)record & 0x40000000) != 0)
                    {
                        *(ushort*)(record + 0x28) -= (ushort)(*(short*)(record + 0x28) >> 3);
                        *(ushort*)(record + 0x2c) -= (ushort)(*(short*)(record + 0x2c) >> 3);

                        if ((record[0x0d] & 0xf) == 0xb)
                        {
                            RunEntityScript(s_Unknown_005e97cc, record, *(ushort*)(record + 0x0e));
                            record[0x0d] = 0xff;
                        }
                    }

                    if (*(ushort*)(record + 0x96) == 0)
                    {
                        uint state = *(uint*)record;
                        *(uint*)record = state & 0xffffffbf;

                        if ((state & 0x40000000) != 0)
                        {
                            if ((state & 0x200000) == 0)
                            {
                                Aud.PlayPositionalSound(4, 0x400, 0x1800, (int*)(record + 0x1c));
                                DespawnSceneEntity_0051a740(record);
                            }

                            *(uint*)record &= 0x7fffffff;
                            FreeEntitySlot(Mem.Entity(0x005f2d00), record[8]);
                            continue;
                        }
                    }
                }

                // La cible tenue a +0x74 est oubliee des qu'elle cesse d'etre active.
                byte* watched = *(byte**)(record + 0x74);
                if (watched != null && (*(uint*)watched & 0x80000000) == 0)
                {
                    *(byte**)(record + 0x74) = null;
                }

                byte* chased = *(byte**)(record + 0x6c);
                if (chased != null && (*(uint*)chased & 0x50000000) != 0)
                {
                    uint candidate = chased[8];
                    for (int n = s_PartySize_005e9020; n != 0; n--)
                    {
                        if ((*(Inp.s_SlotFlags_005e93c0 + candidate * 0x9c + 3) & 0x50) == 0)
                        {
                            *(byte**)(record + 0x6c) = Inp.s_SlotFlags_005e93c0 + candidate * 0x9c;
                        }

                        candidate = (uint)((int)(candidate + 1) % s_PartySize_005e9020);
                    }
                }

                uint flags = *(uint*)record;
                if ((flags & 0x10000000) == 0)
                {
                    if ((flags & 0x40000000) == 0)
                    {
                        PublishSceneEntityToGlobals(record);

                        DispatchEntityHandler(
                            Res.ResolveEntityHandler(0x0056ad48, record[5]), record,
                            $"FUN_0050e680, table 0x005ecaa0 entree {i}, sous-type {record[5]}");

                        *(uint*)record |= 0x600;

                        byte* stats = GetEntityStatBlock((uint*)record);
                        if ((stats[0x40] & 2) == 0 && *(uint*)Mem.Entity(0x005f2cf0) % 100 == 0)
                        {
                            // La regeneration lente : un seizieme du maximum tous les cent tours,
                            // borne au maximum.
                            short maximum = *(short*)(stats + 0x4a);
                            *(short*)(stats + 0x4e) += (short)(maximum + 0xf >> 4);
                            if (maximum < *(short*)(stats + 0x4e)) *(short*)(stats + 0x4e) = maximum;
                        }

                        AdvanceSceneEntityAnimation_0053aa10(record);

                        if ((*(uint*)(record + 0x90) & 0x2000a88a) != 0)
                        {
                            ReadSceneEntityBackFromGlobals(record);
                        }
                    }
                }
                else
                {
                    // L'evanouissement, dans un sens ou dans l'autre selon le bit 0x8000000 :
                    // l'echelle rejoint 0x1000 en se divisant par deux, ou tombe vers zero.
                    short scale = *(short*)(record + 0x28);
                    if ((flags & 0x8000000) == 0)
                    {
                        scale = (short)(scale + (short)(0x1000 - scale >> 1));
                        *(short*)(record + 0x28) = scale;
                        *(short*)(record + 0x2c) = scale;

                        scale = (short)(*(short*)(record + 0x2a) + (short)(0x1000 - *(short*)(record + 0x2a) >> 1));
                        *(short*)(record + 0x2a) = scale;

                        if (scale < 0x1003)
                        {
                            *(uint*)(record + 0x38) = 0xffffff;
                            *(uint*)record &= 0xefffffff;
                        }
                        else
                        {
                            *(int*)(record + 0x38) += 0x80808;
                        }
                    }
                    else
                    {
                        scale = (short)(scale - (scale >> 1));
                        *(short*)(record + 0x28) = scale;
                        *(short*)(record + 0x2c) = scale;

                        scale = (short)(*(short*)(record + 0x2a) + (short)(0x4000 - *(short*)(record + 0x2a) >> 1));
                        *(short*)(record + 0x2a) = scale;

                        if (scale > 0x3ffd)
                        {
                            *(uint*)record &= 0x7fffffff;
                            FreeEntitySlot(Mem.Entity(0x005f2d00), record[8]);
                            continue;
                        }

                        *(int*)(record + 0x38) += -0x80808;
                    }
                }

                // La distance a la camera decide du bit de proximite, et un compte a rebours
                // d'eloignement finit par declencher un son et deux bits d'etat.
                int dx = (*Mem.Camera(0x005de068) - *(int*)(record + 0x1c)) >> 0xc;
                int dz = (*Mem.Camera(0x005de070) - *(int*)(record + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                if (distance < 600)
                {
                    *(uint*)record |= 0x100;
                    if (*(ushort*)(record + 0x98) != 0) *(ushort*)(record + 0x98) = 200;
                }
                else
                {
                    *(uint*)record &= 0xfffffeff;
                    if (*(ushort*)(record + 0x98) != 0)
                    {
                        (*(ushort*)(record + 0x98))--;
                        if (*(ushort*)(record + 0x98) == 0)
                        {
                            *(uint*)record |= 0x18000000;
                            Aud.PlayPositionalSound(0x1f, 0x800, 0x2000, Mem.Camera(0x005de068));
                        }
                    }
                }
            }

            if ((*(uint*)record & 0x80000000) != 0)
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: les onze globales que <see cref="TickEntityTable_005ecaa0"/> remplit avant d'appeler
    /// le gestionnaire de la table 0x0056ad48, et relit apres. Extraites en deux methodes parce que
    /// la pseudo-C entrelace les onze ecritures de la relecture avec des temporaires renommes, ce
    /// qui la rend illisible sans rien changer au sens.
    ///
    /// <para><c>DAT_0060083c</c> n'est ecrit qu'en moitie basse (un <c>ushort</c>) et relu en
    /// entier : c'est bien ce que fait l'original.</para>
    /// </summary>
    private static void PublishSceneEntityToGlobals(byte* record)
    {
        *(uint*)Mem.Entity(0x005fa4e0) = *(uint*)(record + 0x1c);
        *(uint*)Mem.Entity(0x005fa4e4) = *(uint*)(record + 0x20);
        *(uint*)Mem.Entity(0x005fa4e8) = *(uint*)(record + 0x24);
        *(uint*)Mem.Arena(0x005e99c4) = *(uint*)(record + 0x7c);
        *(ushort*)Mem.Entity(0x0060083c) = *(ushort*)(record + 0x90);
        *(uint*)Mem.Arena(0x005e9b4c) = *(uint*)(record + 0x80);
        *(uint*)Mem.Arena(0x005e9b48) = *(uint*)(record + 0x8c);
        *(uint*)Mem.Arena(0x005e9b50) = *(uint*)(record + 0x84);
        *(uint*)Mem.Arena(0x005e9b44) = *(uint*)(record + 0x88);
        *(uint*)Mem.Entity(0x005ff3e0) = *(uint*)(record + 0x60);
        *(uint*)Mem.Entity(0x005ff3ec) = *(uint*)(record + 0x64);
    }

    /// <summary>Le retour des onze globales dans l'entite, plus la negation des deux vitesses de
    /// <c>+0x50</c> et <c>+0x58</c> — le rebond.</summary>
    private static void ReadSceneEntityBackFromGlobals(byte* record)
    {
        *(uint*)(record + 0x7c) = *(uint*)Mem.Arena(0x005e99c4);
        *(uint*)(record + 0x1c) = *(uint*)Mem.Entity(0x005fa4e0);
        *(uint*)(record + 0x20) = *(uint*)Mem.Entity(0x005fa4e4);
        *(uint*)(record + 0x90) = *(ushort*)Mem.Entity(0x0060083c);
        *(uint*)(record + 0x80) = *(uint*)Mem.Arena(0x005e9b4c);
        *(uint*)(record + 0x84) = *(uint*)Mem.Arena(0x005e9b50);
        *(uint*)(record + 0x24) = *(uint*)Mem.Entity(0x005fa4e8);
        *(uint*)(record + 0x8c) = *(uint*)Mem.Arena(0x005e9b48);
        *(uint*)(record + 0x60) = *(uint*)Mem.Entity(0x005ff3e0);
        *(uint*)(record + 0x88) = *(uint*)Mem.Arena(0x005e9b44);
        *(uint*)(record + 0x64) = *(uint*)Mem.Entity(0x005ff3ec);

        *(int*)(record + 0x50) = -*(int*)(record + 0x50);
        *(int*)(record + 0x58) = -*(int*)(record + 0x58);
    }

    /// <summary>
    /// Ghidra: FUN_0051a740 @ 0x0051a740 — appelee quand une entite de scene expire sans porter le
    /// bit <c>0x200000</c>, juste apres le son de disparition.
    /// </summary>
    private static void DespawnSceneEntity_0051a740(byte* record)
        => throw new NotImplementedException(
            "FUN_0051a740 (0x0051a740) n'est pas transcrite : la mise a mort d'une entite de scene, " +
            "appelee par FUN_0050e680 quand son clignotement expire. Son corps est dans " +
            "Legacy/Decompiled/Blaze.c. Elle prend l'entite en unique argument et ne rend rien.");

    /// <summary>
    /// Ghidra: FUN_0053aa10 @ 0x0053aa10 — appelee sur chaque entite de scene active, juste apres
    /// son gestionnaire et la regeneration lente.
    /// </summary>
    private static void AdvanceSceneEntityAnimation_0053aa10(byte* record)
        => throw new NotImplementedException(
            "FUN_0053aa10 (0x0053aa10) n'est pas transcrite : le pas d'animation d'une entite de " +
            "scene, appele une fois par image par FUN_0050e680. Son corps est dans " +
            "Legacy/Decompiled/Blaze.c. Elle prend l'entite en unique argument et ne rend rien.");

    /// <summary>
    /// Ghidra: FUN_0050eb20 @ 0x0050eb20 — la passe sur la table <c>DAT_005f7de0</c>, 0x40
    /// enregistrements : les <b>projectiles</b>, ou du moins ce qui vole et retombe.
    ///
    /// <para>
    /// C'est la seule des six qui integre un mouvement : la position de <c>+0x1c</c>/<c>+0x20</c>/
    /// <c>+0x24</c> avance de la vitesse de <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, les composantes
    /// horizontales perdent un huitieme par image (une trainee) et la verticale recoit la gravite
    /// de <c>+0x9a</c>. <see cref="FinalizeEntityPlacement2"/> replace ensuite l'objet sur le
    /// terrain, et le rebond est le bloc <c>0x802</c> : les deux vitesses horizontales sont
    /// niees et huit champs sauvegardes a l'entree sont restitues.
    /// </para>
    /// <para>
    /// Deux fins possibles : la duree de vie de <c>+0x10</c> qui tombe a zero, ou une chute
    /// au-dessous de <c>-0x4e1ffff</c> — soit -20000 en unites monde. Les deux effacent le bit
    /// d'activite, et c'est le test <b>relu</b> en bas de tour qui rend alors l'emplacement.
    /// </para>
    /// <para>
    /// La teinte suit la duree de vie restante : au-dessous de douze images, les trois composantes
    /// valent <c>vie * 0x100</c>, donc l'objet s'eteint en gris ; au-dessus, elles sont figees a
    /// 0x0c00.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005f7de0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005f7de0);

        for (int i = 0; i < 0x40; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if (((byte)Mem.s_WorldFlags_005de02c & 0x10) == 0)
            {
                // Les huit champs que le rebond restituera.
                int saved7c = *(int*)(record + 0x7c);
                int saved60 = *(int*)(record + 0x60);
                int saved64 = *(int*)(record + 0x64);
                ushort saved90 = *(ushort*)(record + 0x90);
                int saved80 = *(int*)(record + 0x80);
                int saved84 = *(int*)(record + 0x84);
                int saved88 = *(int*)(record + 0x88);
                int saved8c = *(int*)(record + 0x8c);

                if ((*(uint*)record & 0x10008000) == 0)
                {
                    // La teinte pulse avec la duree de vie : un sinus ramene dans 0..0xff, puis
                    // repete sur les trois composantes.
                    int wave = (int)Winsys3d.GetSinTableEntry((int)*(short*)(record + 0x10) << 8);
                    uint grey = (uint)((wave >> 6) - 0x80) & 0xff;
                    *(int*)(record + 0x38) = (int)(((grey << 8 | grey) << 8) | grey);
                }

                int vx = *(int*)(record + 0x50);
                *(int*)(record + 0x1c) += vx;

                int vz = *(int*)(record + 0x58);
                int vy = *(int*)(record + 0x54);
                *(int*)(record + 0x24) += vz;
                *(int*)(record + 0x20) += vy;

                *(int*)(record + 0x50) = vx - (vx >> 3);
                *(int*)(record + 0x54) =
                    (((int)*(short*)(record + 0x9a) >> 8) - (vy >> 8)) - (int)*(short*)(record + 0x9a) + vy;
                *(int*)(record + 0x58) = vz - (vz >> 3);

                FinalizeEntityPlacement2((uint*)record);

                if ((*(uint*)(record + 0x90) & 0x802) != 0)
                {
                    *(int*)(record + 0x50) = -*(int*)(record + 0x50);
                    *(int*)(record + 0x58) = -*(int*)(record + 0x58);
                    *(int*)(record + 0x7c) = saved7c;
                    *(int*)(record + 0x60) = saved60;
                    *(int*)(record + 0x64) = saved64;
                    *(int*)(record + 0x80) = saved80;
                    *(int*)(record + 0x84) = saved84;
                    *(uint*)(record + 0x90) = saved90;
                    *(int*)(record + 0x88) = saved88;
                    *(int*)(record + 0x8c) = saved8c;
                }

                if (*(int*)(record + 0x20) < -0x4e1ffff)
                {
                    *(uint*)record &= 0x7fffffff;
                }
                else
                {
                    if (*(int*)(record + 0x20) <= *(int*)(record + 0x60)
                        && (*(uint*)(record + 0x90) & 0xa000) == 0)
                    {
                        // Le sol : la hauteur est ramenee au plancher et la vitesse verticale
                        // repart vers le haut, amortie d'un quart, puis annulee si elle est
                        // devenue plus petite que deux fois la gravite.
                        *(int*)(record + 0x20) = *(int*)(record + 0x60);

                        int bounce = *(int*)(record + 0x54);
                        if (bounce < 0) bounce = -bounce;
                        bounce = (bounce - (bounce >> 2)) - (int)*(short*)(record + 0x9a);
                        *(int*)(record + 0x54) = bounce;

                        if (bounce < *(short*)(record + 0x9a) * 2) *(int*)(record + 0x54) = 0;
                    }

                    short life = *(short*)(record + 0x10);
                    if (life < 0xc)
                    {
                        uint tint = (uint)((int)life << 8);
                        *(short*)(record + 0x2c) = (short)tint;
                        *(int*)(record + 0x28) = (int)((uint)((int)life << 0x18) | tint);
                    }
                    else
                    {
                        *(int*)(record + 0x28) = 0xc000c00;
                        *(ushort*)(record + 0x2c) = 0xc00;
                    }
                }

                (*(short*)(record + 0x10))--;
                if (*(short*)(record + 0x10) == 0) *(uint*)record &= 0x7fffffff;
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005f7b60), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Le gestionnaire d'une des trois passes qui indexent leur table de sauts avec un octet entier.
    /// Sous <c>0xc0</c>, l'indice designe une table <b>statique</b> du binaire — que Ghidra a nommee
    /// <c>PTR_LAB_…</c>, donc dont il n'a pas reconnu les cibles comme des fonctions ; au-dessus,
    /// il deborde de trois tables et retombe dans une des sept tables installees par zone (voir
    /// <see cref="Resources.ResolveEntityHandler"/>).
    /// </summary>
    /// <param name="staticTableVa">La table <c>PTR_LAB_…</c> lue pour un indice sous 0xc0.</param>
    /// <param name="installedTableVa">La table nommee au site d'appel, indexee telle quelle.</param>
    private static void CallIndexedEntityHandler(
        int staticTableVa, int installedTableVa, byte index, byte* record, string where)
    {
        if (index < 0xc0)
        {
            throw new NotImplementedException(
                $"{where} : le gestionnaire d'indice {index} vit dans la table statique " +
                $"0x{staticTableVa:X6} du binaire, que Ghidra a nommee PTR_LAB_ — donc ses cibles " +
                "sont **absentes de Blaze.c**. Relever l'entree avec " +
                $"`OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{staticTableVa + index * 4:x8} 4`, " +
                "puis desassembler le corps qu'elle designe. Le gestionnaire ne prend AUCUN " +
                "argument (l'appel est `(*(code *)…)()`), a la difference de ceux de la branche " +
                "haute.");
        }

        DispatchEntityHandler(Res.ResolveEntityHandler(installedTableVa, index), record, where);
    }

    /// <summary>
    /// L'appel indirect a un gestionnaire d'entite, une fois son adresse resolue. Meme patron que
    /// <c>ScriptOpAreaVerb</c> pour les verbes de zone : <b>le dispatch se fait sur la valeur
    /// d'adresse</b>, un cas par gestionnaire transcrit, et tout le reste leve en se nommant.
    ///
    /// <para>
    /// C'est le point d'entree unique des cinq passes qui appellent une table de sauts. Ecrit ici
    /// plutot que dans <c>Engine/Resources.cs</c> parce que les corps sont des gestionnaires de la
    /// carte du monde et que la regle de routage (4) les laisse chez leur appelant.
    /// </para>
    /// <para>
    /// <b>Le nom n'est pas <c>CallEntityHandler</c>, et ce n'est pas cosmetique</b> : l'oracle de
    /// <c>compare-bodies.ps1</c> indexe les corps <b>par nom seul</b>. Une surcharge homonyme de
    /// <see cref="Resources.CallEntityHandler"/> lui faisait comparer l'un a l'autre et signaler
    /// une « logique changee » qui n'existait pas. Meme famille de piege que la liste blanche
    /// insensible a la casse, deja documentee dans TODO.md.
    /// </para>
    /// </summary>
    private static void DispatchEntityHandler(uint target, byte* record, string where)
    {
        switch (target)
        {
            // Souche `ret` nu, verifiee dans Blaze.c.
            case 0x00401FA0: return;

            case 0x004508D0: EntityHandler_004508D0((uint*)record); return;
            case 0x004509B0: EntityHandler_004509B0((uint*)record); return;

            default:
                // Les seize premiers octets de l'enregistrement partent avec la lacune. Un
                // gestionnaire manquant se lit toujours avec sa donnee : c'est ce qui permet de
                // distinguer une entite legitime d'un enregistrement mal rempli sans instrumenter
                // le jeu pour un seul essai.
                Res.CallEntityHandler(target, $"{where} [{DumpRecordHead(record)}]");
                return;
        }
    }

    /// <summary>Les seize premiers octets d'un enregistrement d'entite, en hexadecimal, pour les
    /// messages de lacune.</summary>
    private static string DumpRecordHead(byte* record)
    {
        var text = new System.Text.StringBuilder(48);
        for (int i = 0; i < 16; i++)
        {
            if (i != 0) text.Append(' ');
            text.Append(record[i].ToString("x2"));
        }

        return text.ToString();
    }

    /// <summary>
    /// Ghidra: FUN_004508D0 @ 0x004508d0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (197 octets, 0x004508d0..0x00450994). C'est l'entree <b>8</b> de la table
    /// 0x0056adc8, le premier gestionnaire d'entite que la carte du monde atteint reellement.
    ///
    /// <para>
    /// Une <b>animation de 0x28 images</b>, comptee dans le <c>short</c> de <c>+0x10</c>. Au
    /// premier tour (compteur nul) elle pose le bit <c>0x2000</c> de l'etat, amorce a 15 la valeur
    /// de <c>+0x54</c>, et allume un bit de <c>DAT_00571540</c> choisi par le <c>short</c> de
    /// <c>+0x14</c> : c'est une <b>table de sauts a cinq entrees</b> (0x00450998 dans le binaire),
    /// dont les cinq cibles ne different que par la constante OU-ee — 1, 2, 4, 8, 0x10. Elle se
    /// resume donc a <c>1 &lt;&lt; (kind - 1)</c>, et c'est ecrit ainsi plutot qu'en cinq branches
    /// identiques. <c>DAT_00571540</c> est justement le mot que <c>Init_004502d0</c> remet a zero
    /// au chargement de zone.
    /// </para>
    /// <para>
    /// Ensuite la valeur de <c>+0x54</c> <b>enfle puis retombe</b> : elle gagne un huitieme d'
    /// elle-meme tant que le compteur est sous 12, et en perd un huitieme au-dela. Elle est ajoutee
    /// a chaque image au <c>short</c> de <c>+0x32</c> — un angle qui accelere puis ralentit.
    /// </para>
    /// <para>
    /// A la 0x28e image, l'entite pose <c>0x40002000</c> et rend la main <b>sans</b> demander l'
    /// effet de camera : c'est sa fin.
    /// </para>
    /// </summary>
    private static void EntityHandler_004508D0(uint* entity)
    {
        if ((entity[0] & 0x40000000) != 0) return;

        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            // Ghidra rend `or ch,20h` : ch est l'octet 1 d'ECX, donc le bit 0x2000 du mot d'etat.
            entity[0] |= 0x2000;
            *(int*)(e + 0x54) = 0x0f;

            int kind = *(short*)(e + 0x14) - 1;
            if ((uint)kind <= 4)
            {
                *Mem.EntityState(0x00571540) |= 1 << kind;
            }
        }

        short frame = *(short*)(e + 0x10);
        int rate = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = frame > 0xc ? rate - (rate >> 3) : (rate >> 3) + rate;

        short next = (short)(frame + 1);
        *(short*)(e + 0x10) = next;
        if (next == 0x28)
        {
            entity[0] |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) += (short)*(int*)(e + 0x54);
        TryPublishCameraEffectNearEntity(entity, 1, 1, 1, 0, 0);
    }

    /// <summary>
    /// Ghidra: FUN_0041be60 @ 0x0041be60 — publie jusqu'a trois valeurs dans le bloc
    /// <c>DAT_005de0a8</c> / <c>ac</c> / <c>b0</c>, mais <b>seulement si l'entite est a moins de
    /// 1500 unites de la camera</b>. Les trois mots sont remis a zero en tete de chaque image par
    /// le prologue de <see cref="RunWorldMapTick"/> : c'est donc une requete valable une image.
    /// La valeur sentinelle <c>999</c> veut dire « ne touche pas a celui-la ».
    ///
    /// <para>
    /// <b>Ghidra s'est trompe de signature, et le desassembleur tranche.</b> La pseudo-C montre
    /// trois parametres et se sert de <c>param_1</c> a la fois comme entite (<c>param_1 + 0x1c</c>)
    /// et comme premiere valeur publiee — ce qui ne peut pas etre. Le site d'appel empile
    /// <b>six</b> arguments (<c>add esp,18h</c>), et le corps lit <c>[esp+10h]</c>,
    /// <c>[esp+14h]</c> et <c>[esp+18h]</c> apres deux <c>push</c>, soit les arguments 2, 3 et 4.
    /// Les arguments 5 et 6 ne sont lus <b>nulle part</b> dans le corps ; ils sont conserves dans
    /// la signature parce que l'appelant les empile.
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que la requete porte, 0 sinon.</returns>
    private static ushort TryPublishCameraEffectNearEntity(
        uint* entity, int first, int second, int third, int unused5, int unused6)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);
        byte* e = (byte*)entity;

        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x5dc) return 0;

        if (first != 999) *Mem.Camera(0x005de0a8) = first;
        if (second != 999) *Mem.Camera(0x005de0ac) = second;
        if (third != 999) *Mem.Camera(0x005de0b0) = third;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004509b0 @ 0x004509b0 — l'entree <b>9</b> de la table 0x0056adc8, et la
    /// <b>jumelle</b> de <see cref="EntityHandler_004508D0"/> : meme animation de 0x28 images, meme
    /// enflure de la valeur de <c>+0x54</c>, meme table de sauts a cinq entrees sur
    /// <c>DAT_00571540</c>. Deux differences, et elles vont ensemble :
    ///
    /// <list type="bullet">
    /// <item>l'angle de <c>+0x32</c> est <b>retranche</b> au lieu d'etre ajoute — les deux tournent
    /// en sens contraire ;</item>
    /// <item>elle joue un son au premier tour au lieu de demander l'effet de camera a chaque
    /// tour.</item>
    /// </list>
    ///
    /// <para>
    /// <b>L'identifiant du son ne se lit pas dans la pseudo-C.</b> Ghidra rend
    /// <c>CONCAT22((short)(DAT_005e97cc &gt;&gt; 0x10), …)</c>, ce qui n'a aucun sens comme valeur : c'est
    /// sa facon de modeliser un <c>movzx ax, byte […]</c> qui laisse la moitie haute d'EAX
    /// perimee. Le desassemblage (0x004509d5..0x004509f4) donne la vraie lecture — une <b>double
    /// indirection</b> depuis <c>ctx + 0xa8</c>, dont on prend le premier octet — et confirme que
    /// l'appele ne lit que la moitie basse.
    /// </para>
    /// </summary>
    private static void EntityHandler_004509B0(uint* entity)
    {
        if ((entity[0] & 0x40000000) != 0) return;

        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x54) = 0x0f;
            entity[0] |= 0x2000;

            ushort soundId = **(byte**)*(byte**)(s_Unknown_005e97cc + 0xa8);
            TryPlaySoundNearEntity(entity, (short)soundId, 0x200, 0x1600, 0, 0);

            // La meme table de sauts a cinq entrees que EntityHandler_004508D0 (ici en 0x00450a80),
            // dont les cibles ne different que par la constante OU-ee.
            int kind = *(short*)(e + 0x14) - 1;
            if ((uint)kind <= 4)
            {
                *Mem.EntityState(0x00571540) |= 1 << kind;
            }
        }

        int rate = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = *(short*)(e + 0x10) < 0xd ? (rate >> 3) + rate : rate - (rate >> 3);

        short next = (short)(*(short*)(e + 0x10) + 1);
        *(short*)(e + 0x10) = next;
        if (next == 0x28)
        {
            entity[0] |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) -= (short)*(int*)(e + 0x54);
    }

    /// <summary>
    /// Ghidra: FUN_00468080 @ 0x00468080 — <b>le jumeau sonore</b> de
    /// <see cref="TryPublishCameraEffectNearEntity"/> : meme calcul de distance a la camera, meme
    /// signature perdue par Ghidra, mais un rayon de 2000 au lieu de 1500 et un son a la place des
    /// trois mots publies.
    ///
    /// <para>
    /// Comme sa jumelle, <b>la pseudo-C se sert de <c>param_1</c> a la fois comme entite et comme
    /// identifiant de son</b>, ce qui est impossible. Le desassemblage donne les vrais arguments :
    /// le site d'appel en empile <b>six</b> (<c>add esp,18h</c>) et le corps lit
    /// <c>[esp+14h]</c>, <c>[esp+18h]</c> et <c>[esp+1ch]</c> apres trois <c>push</c>, soit les
    /// arguments 2, 3 et 4, tous trois en <c>movsx word</c>. Les arguments 5 et 6 ne sont lus
    /// nulle part.
    /// </para>
    /// <para>
    /// Le gain est <b>double</b> avant d'etre passe (<c>shl edx,1</c>), et la position sonore est
    /// celle de l'entite (<c>+0x1c</c>).
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que le son parte, 0 sinon.</returns>
    private static ushort TryPlaySoundNearEntity(
        uint* entity, short soundId, short pitch, short gain, int unused5, int unused6)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);
        byte* e = (byte*)entity;

        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 2000) return 0;

        Aud.PlayPositionalSound(soundId, pitch, gain << 1, (int*)(e + 0x1c));
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047aa70 @ 0x0047aa70 — la passe sur la table <c>DAT_005ff400</c>, 0x20
    /// enregistrements. Elle ne raccroche <b>rien</b> a la grille spatiale : c'est la seule des six
    /// dans ce cas, donc ces entites ne sont pas cherchees par proximite.
    /// </summary>
    private static void TickEntityTable_005ff400()
    {
        byte* record = Mem.Entity(0x005ff400);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                CallIndexedEntityHandler(
                    0x00551c30, 0x0056adc8, record[5], record,
                    $"FUN_0047aa70, table 0x005ff400 entree {i}");
            }

            // Le mot d'etat est RELU : le gestionnaire a pu eteindre l'entite lui-meme.
            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005fa5a0), record[8]);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047ab70 @ 0x0047ab70 — la passe sur la table <c>DAT_005edee0</c>, 0x80
    /// enregistrements.
    /// </summary>
    private static void TickEntityTable_005edee0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005edee0);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                record[0x36] &= 0x7f;
                CallIndexedEntityHandler(
                    0x00551f30, 0x0056aec8, record[5], record,
                    $"FUN_0047ab70, table 0x005edee0 entree {i}");
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x006007a0), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047acf0 @ 0x0047acf0 — la passe sur la table <c>DAT_005f2d40</c>, 0x80
    /// enregistrements. Copie conforme de <see cref="TickEntityTable_005edee0"/> a trois adresses
    /// pres — la table, son pool et sa table de sauts. Les deux sont ecrites separement plutot que
    /// factorisees : ce sont deux fonctions distinctes du binaire, et l'oracle compare des corps.
    /// </summary>
    private static void TickEntityTable_005f2d40()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005f2d40);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                record[0x36] &= 0x7f;
                CallIndexedEntityHandler(
                    0x00552230, 0x0056afc8, record[5], record,
                    $"FUN_0047acf0, table 0x005f2d40 entree {i}");
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005f7ce0), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050ed70 @ 0x0050ed70 — <b>la rencontre aleatoire</b> : de temps en temps, une
    /// formation d'ennemis apparait autour d'un membre du groupe tire au sort.
    ///
    /// <para>
    /// Le delai entre deux tentatives est <c>DAT_00564540</c>, recharge a <c>(rand()%6 + 10) * 20</c>
    /// par <see cref="TickEntityTable_005ecaa0"/> — soit 200 a 300 images, trois a cinq secondes.
    /// La passe ne fait rien tant qu'il n'est pas retombe a zero.
    /// </para>
    /// <para>
    /// <b>Le placement est teste avant d'etre commis, et c'est tout ou rien.</b> Chaque
    /// enregistrement de 0x20 octets de la formation est pose a 400 unites du groupe dans une
    /// direction tiree au sort, passe a <see cref="FinalizeEntityPlacement2"/>, et compte comme
    /// valide si les drapeaux de vue rendus n'ont aucun bit hors de <c>0x201</c>. La formation
    /// n'est reellement engendree — <see cref="SpawnEntityFormation"/> puis un son — que si
    /// <b>tous</b> les enregistrements ont passe le test.
    /// </para>
    /// <para>
    /// <b>Les trois appels successifs a <see cref="FinalizeEntityPlacement2"/> sont transcrits tels
    /// quels.</b> La pseudo-C n'ecrit rien entre eux, donc les trois partent du meme point et les
    /// trois drapeaux OU-es devraient etre identiques. C'est peut-etre une marche du placement que
    /// Ghidra a rendue invisible ; en l'absence de preuve au desassembleur, rien n'est simplifie
    /// ici. TODO(REVERSE) : verifier ces trois appels sur le binaire.
    /// </para>
    /// </summary>
    private static void TrySpawnRandomEncounter()
    {
        if (s_Unknown_00564540 != 0)
        {
            s_Unknown_00564540--;
            return;
        }

        if (s_Unknown_005de544 == s_PartySize_005e9020) return;
        if (*(sbyte*)(s_Unknown_005f2ce0 + 0x10) == -1) return;
        if (*Mem.Entity(0x005f2d00) != 0) return;

        int member = CrtRand() % s_PartySize_005e9020;
        uint* slot = (uint*)(Inp.s_SlotFlags_005e93c0 + member * 0x9c);

        if ((*slot & 0x80000000) == 0) return;
        if ((*slot & 0x50000000) != 0) return;
        if ((*((byte*)slot + 0x90) & 2) != 0) return;

        // La table des formations de la sous-zone courante, puis une de ses huit entrees.
        short* formations = *(short**)(*(byte**)(s_Unknown_005e97cc + 0xb8) + s_Unknown_005f2ce0[0x10] * 4);
        if (formations == null) return;

        uint roll = (uint)CrtRand();
        int* formation = *(int**)(*(byte**)(s_Unknown_005e97cc + 0x34) + formations[roll & 7] * 4);
        if (formation == null) return;

        if ((*slot & 0x80000000) == 0 || (*slot & 0x40000000) != 0) return;

        byte* scratch = Mem.Arena(0x005e9a40);
        int* record = formation;
        int placed = 0;
        int tried = 0;

        for (int kind = *record; kind != -1; kind = *record)
        {
            // L'entite d'essai est une copie du membre tire au sort, deplacee de 400 unites dans
            // une direction aleatoire. Le Y a 0x7fffffff force la recherche de sol a partir du
            // haut.
            Mem.CopyDwords(slot, (uint*)scratch, 0x27);
            *(int*)(scratch + 0x1c) = *Mem.Camera(0x005de068);
            *(int*)(scratch + 0x20) = 0x7fffffff;
            *(int*)(scratch + 0x24) = *Mem.Camera(0x005de070);

            int angle = CrtRand();
            *(int*)(scratch + 0x1c) += (int)Winsys3d.GetSinTableEntry(angle) * 400;
            *(int*)(scratch + 0x24) += Winsys3d.GetCosTableEntry(angle) * 400;

            FinalizeEntityPlacement2((uint*)scratch);
            uint clip1 = *(uint*)(scratch + 0x90);
            FinalizeEntityPlacement2((uint*)scratch);
            uint clip2 = *(uint*)(scratch + 0x90);
            FinalizeEntityPlacement2((uint*)scratch);

            if (((clip1 | clip2 | *(uint*)(scratch + 0x90)) & 0xfffffdfe) == 0)
            {
                int dx = *Mem.Camera(0x005de068) - *(int*)(scratch + 0x1c);
                int dz = *Mem.Camera(0x005de070) - *(int*)(scratch + 0x24);

                *(short*)((byte*)record + 0x0a) = *(short*)(scratch + 0x30);
                *(short*)((byte*)record + 0x0c) = (short)Winsys3d.Ordinal_457(dx >> 0xc, dz >> 0xc);
                *(short*)((byte*)record + 0x0e) = *(short*)(scratch + 0x34);
                *(short*)((byte*)record + 0x04) = (short)(*(int*)(scratch + 0x1c) >> 0xc);
                *(short*)((byte*)record + 0x06) = (short)(*(int*)(scratch + 0x60) >> 0xc);
                *(short*)((byte*)record + 0x08) = (short)(*(int*)(scratch + 0x24) >> 0xc);

                // Les quatre distances de bord : chacune est gardee par le SIGNE de la suivante,
                // et un signe negatif la sature a 0xffff.
                *(short*)((byte*)record + 0x10) = (*(short*)(scratch + 0x82) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x80);
                *(short*)((byte*)record + 0x12) = (*(short*)(scratch + 0x86) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x84);
                *(short*)((byte*)record + 0x14) = (*(short*)(scratch + 0x8a) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x88);
                *(short*)((byte*)record + 0x16) = (*(short*)(scratch + 0x8e) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x8c);

                placed++;
            }

            record += 8;      // 0x20 octets
            tried++;
        }

        if (tried == placed)
        {
            SpawnEntityFormation(s_Unknown_005e97cc, formation, 200, 0);
            Aud.PlayPositionalSound(0x1b, 0x800, 0x2000, Mem.Camera(0x005de068));
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050f0d0 @ 0x0050f0d0 — les 0x40 <b>effets ephemeres</b> de <c>DAT_005e9b60</c>,
    /// 0x28 octets chacun. Chaque effet a une duree de vie d'un octet a <c>+0x27</c>, posee a 0x14
    /// par <c>FUN_0050f070</c>.
    ///
    /// <para>
    /// Tant qu'elle dure, la hauteur de l'effet (<c>+0x04</c>) est deplacee de
    /// <c>(vie - 8) * 0x2000</c> : positif au debut, negatif ensuite — l'effet monte puis
    /// redescend, en passant par l'immobilite exactement a mi-vie. A zero, l'emplacement retourne
    /// au pool <c>DAT_005e99e0</c>.
    /// </para>
    /// </summary>
    private static void TickEffectSlots_005e9b60()
    {
        byte* entry = Mem.Arena(0x005e9b60);

        for (int i = 0; i < 0x40; i++, entry += 0x28)
        {
            if (entry[0x27] == 0) continue;

            byte life = (byte)(entry[0x27] - 1);
            entry[0x27] = life;

            if (life == 0)
            {
                FreeEntitySlot(Mem.Arena(0x005e99e0), entry[0x25]);
            }
            else
            {
                *(int*)(entry + 0x04) += (entry[0x27] - 8) * 0x2000;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00534f90 @ 0x00534f90 — <b>la remise a l'etat neuf des morceaux consommes</b>.
    /// Elle parcourt la meme liste d'attente que le prologue (<c>DAT_005fa560</c>, premier octet =
    /// compte), et pour chaque liste d'enregistrements de 0x10 octets qu'elle designe, elle
    /// retravaille le premier octet de chaque enregistrement jusqu'au terminateur <c>-1</c>.
    ///
    /// <para>
    /// La regle est celle-ci : si les trois bits bas sont nuls, ou s'ils different des trois
    /// suivants, seul le bit 7 survit ; sinon l'octet est efface entierement. Autrement dit
    /// « garder la marque de visite sauf quand les deux champs de trois bits concordent ».
    /// </para>
    /// <para>
    /// Le prologue de <see cref="RunWorldMapTick"/> parcourt exactement la meme structure pour la
    /// mettre a zero. Les deux passes coexistent dans l'original et ne sont pas fusionnees ici.
    /// </para>
    /// </summary>
    private static void ResetPendingChunkRecords()
    {
        byte* pending = Mem.Entity(0x005fa560);
        if (*pending == 0) return;

        for (int i = 1; i <= *pending; i++)
        {
            uint* record = *(uint**)(*(byte**)(s_Unknown_005e97cc + 0x88) + pending[i] * 4);
            if (record == null) return;

            for (uint value = *record; value != 0xffffffff; value = *record)
            {
                if ((value & 7) == 0 || (value >> 3 & 7) != (value & 7))
                {
                    *(byte*)record = (byte)(*(byte*)record & 0x80);
                }
                else
                {
                    *(byte*)record = 0;
                }

                record += 4;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_005036e0 @ 0x005036e0 — <b>le pas de la camera de scenette</b>, celle que
    /// l'opcode 29 installe (voir <c>BeginScriptedCamera</c>). Le script vit a
    /// <c>DAT_005719b4</c> et avance d'un verbe par appel, jusqu'a ce qu'un verbe rende la main.
    ///
    /// <para>
    /// Avant cela vient <b>l'echappement du joueur</b> : si l'entite qui porte la scenette est dans
    /// l'etat <c>0x800000</c>, que sa manette est branchee et que le drapeau de scenette est
    /// retombe, le script d'interruption est lance. S'il n'a pas lui-meme change le script en
    /// cours — le test <c>pbVar1 == DAT_005719b4</c>, qui compare l'avant et l'apres — la scenette
    /// est coupee nette.
    /// </para>
    /// <para>
    /// <b>La boucle de verbes n'est pas atteignable aujourd'hui</b> : ses cibles vivent dans
    /// <c>PTR_LAB_0055eb68</c>, que Ghidra n'a pas decompilee. La lacune est nommee sur place.
    /// </para>
    /// </summary>
    private static void TickCutsceneCamera()
    {
        byte* script = *(byte**)Mem.EntityStateBytes(0x005719b4);
        byte* cursor = script;

        if (script != null)
        {
            uint* owner = *(uint**)Mem.EntityStateBytes(0x005719c4);
            if (owner != null)
            {
                if ((*owner & 0xfc0000) == 0x800000)
                {
                    byte padSlot = Inp.s_PlayerSlots_005e9188[(byte)owner[2] * 0x4c];

                    if ((Inp.s_InputStateBase_00604a20[padSlot * 0x10 + 3] & 0x80) != 0
                        && (Saves.s_CutsceneActive_00604b78 & 1) == 0)
                    {
                        RunEntityScript(
                            s_Unknown_005e97cc,
                            (byte*)owner,
                            *(ushort*)Mem.EntityStateBytes(0x005719d8));

                        cursor = *(byte**)Mem.EntityStateBytes(0x005719b4);

                        if (script == cursor)
                        {
                            // Le script d'interruption n'a pas repris la main : la scenette
                            // s'arrete ici, et le pointeur reste nul.
                            *(byte**)Mem.EntityStateBytes(0x005719b4) = null;
                            return;
                        }
                    }
                }
                else
                {
                    *(uint**)Mem.EntityStateBytes(0x005719c4) = null;
                }
            }

            throw new NotImplementedException(
                "FUN_005036e0 : la boucle de verbes de la camera de scenette n'est pas transcrite. " +
                $"Le verbe a executer est l'indice {*cursor} de la table PTR_LAB_0055eb68 " +
                $"(0x{0x0055eb68 + *cursor * 4:X8}), **absente de Blaze.c** — Ghidra l'a nommee " +
                "PTR_LAB_, donc il n'a pas reconnu ses cibles comme des fonctions. Relever " +
                $"l'entree avec `OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe " +
                $"0x{0x0055eb68 + *cursor * 4:x8} 4`, puis desassembler le corps. Le verbe prend " +
                "(curseur de script, &continuer) et rend le nouveau curseur ; poser `continuer` a " +
                "zero est ce qui rend la main.");
        }

        *(byte**)Mem.EntityStateBytes(0x005719b4) = cursor;
    }

    /// <summary>
    /// Ghidra: FUN_0050bf20 @ 0x0050bf20 — <b>le pas par personnage</b>, appele une fois par membre
    /// du groupe et par image. C'est le plus gros corps du cas 0 : 1 002 lignes de pseudo-C.
    /// </summary>
    private static void TickPartyMember(int member)
        => throw new NotImplementedException(
            $"FUN_0050bf20 (0x0050bf20), membre {member} : le pas par personnage n'est pas " +
            "transcrit. C'est desormais le seul trou du pas de simulation du monde — les treize " +
            "autres appelees du cas 0 sont ecrites. 1 002 lignes de pseudo-C, et **sa moitie basse " +
            "n'est pas exploitable telle quelle** : a partir de 0x0050cca1 environ, Ghidra rend une " +
            "cascade de __allmul/__allshr dont il a perdu les resultats, plus un iStack_4 jamais " +
            "affecte et des unaff_EBX/EBP — c'est la classe de blocage nommee dans CLAUDE.md, celle " +
            "qui demande le desassemblage. `OpenForesia.Tools -- disasm " +
            "Legacy/Binaries/Blaze.exe 0x0050bf20 3000`. Appelees manquantes deja reperees : " +
            "FUN_005238e0, FUN_00530070, FUN_0051a020, et les deux tables de sauts statiques " +
            "PTR_FUN_00562ce0 et PTR_FUN_00562ca0.");
}
