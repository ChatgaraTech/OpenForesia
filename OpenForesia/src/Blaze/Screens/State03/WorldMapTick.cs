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

                        throw new NotImplementedException(
                            "FUN_00516fc0 : la suite du cas 0 (0x005176d5 a 0x0051776b) n'est pas " +
                            "transcrite. Il lui manque quatorze appelees : FUN_00532bf0, " +
                            "FUN_00513ef0, FUN_0050e5e0, FUN_0050e680, FUN_0050eb20, FUN_0050bf20 " +
                            "(1002 lignes, une par personnage), FUN_0047aa70, FUN_0047ab70, " +
                            "FUN_0047acf0, FUN_0050ed70, FUN_0050f0d0, FUN_00513f20, FUN_00534f90 " +
                            "et FUN_005036e0.");

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
}
