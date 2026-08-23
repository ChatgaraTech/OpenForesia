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
                    //   FUN_0051aa30  594 l.   FUN_0051fa40  441 l.   FUN_0051e700  423 l.
                    //   FUN_0051c420  374 l.   FUN_00521000  365 l.   FUN_004011c0  363 l.
                    //   FUN_00522140  315 l.   FUN_005309b0  147 l.   FUN_0051be90   74 l.
                    //   FUN_005320f0   51 l.   FUN_005321f0   51 l.   FUN_00532b40   29 l.
                    //   FUN_0052a7d0   20 l.   FUN_0051f940   18 l.   FUN_00538bc0   13 l.
                    //   FUN_0051c250   10 l.
                    //
                    // Le sous-bloc de mise au point (0x00518473 en amont, ~150 l.) est garde par
                    // DAT_005de020, nul au chargement : le transcrire en dernier.
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

                throw new NotImplementedException(
                    "FUN_00516fc0 : la branche courte d'affichage (0x005179da a 0x00517a77), " +
                    "prise quand DAT_005de02c porte le bit 4 sans que le pilotage libre soit " +
                    "actif. Il lui manque FUN_0051aa30 (594 lignes) et FUN_0052a7d0 (20 lignes).");
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

    // =====================================================================
    // Les descripteurs d'atlas de la moitie dessin, releves dans le binaire (DataSymbols.md).
    //
    // Huit octets chacun : dword0 empaquette l'origine et la taille de cellule, dword1 part tel
    // quel comme moitie « attributs » du sprite. Ils sont poses ici plutot que dans GameMemory
    // parce qu'un seul ecran les lit — meme regle que s_WorldMapDescriptor_005633b0.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00517e7c</c>..<c>0x00519703</c> — <b>la moitie dessin du bloc d'affichage</b>.
    /// Elle emet tout ce que la carte du monde montre : le voile de transition, les deux
    /// indicateurs d'ecran, le panneau d'interface, la rose des vents, les quatre panneaux de
    /// membre avec leurs modeles, le balayage 7x7 des cellules de decor, et les trois listes de
    /// morceaux.
    ///
    /// <para>
    /// <b>Deux objets suffisent a lire tous les appels.</b> Ghidra repand ici six noms —
    /// <c>unaff_EBX</c>, <c>unaff_ESI</c>, <c>unaff_EDI</c>, <c>unaff_EBP</c>, <c>local_60</c>,
    /// <c>piStack_74</c> — qui ne designent que la <b>base d'ordering table</b> (<c>[esp+0x14]</c>)
    /// et le <b>curseur de primitives</b> (<c>[esp+0x10]</c>), plus <c>uStack_54</c> pour l'entree
    /// d'ordering table (<c>[esp+0x20]</c>). Chaque appel de dessin prend <c>(base + N, curseur,
    /// …)</c> et <b>rend le nouveau curseur</b>, que l'appelant range aussitot. C'est le seul
    /// invariant qui compte : un curseur non rechaine ne plante pas ici mais dans winsys3d, sans
    /// pile utilisable.
    /// </para>
    /// <para>
    /// Le decalage <c>+ N</c> sur la base choisit la <b>couche</b> : <c>+8</c> l'interface,
    /// <c>+0xc</c> les etiquettes, <c>+0x14</c> les ombres, <c>-0x14</c> l'arriere-plan, et un
    /// decalage lu dans l'entite pour les particules.
    /// </para>
    /// </summary>
    private static void DrawWorldMapFrame(
        uint* otBase, uint* otEntry, uint** cursorSlot, int debugLatch, byte* mainMem)
    {
        uint* cursor = *cursorSlot;

        // Le bloc de **sept champs sur 0x12 octets** que toutes les fonctions de dessin recoivent
        // par adresse — voir la disposition complete sur Primitives2d.DrawSpritePrimitive. Cinq
        // entiers, pas trois : la premiere version en allouait trois, et DrawSpritePrimitive lisait
        // deja l'echelle et l'indice six octets au-dela.
        int* block = stackalloc int[5];
        byte* b = (byte*)block;

        // ---------------------------------------------------------- le voile de transition
        if ((byte)(Mem.s_Unknown_005de024 >> 0x18) != 0)
        {
            cursor = FillScreenQuad_0051F940(otBase, cursor, Mem.s_Unknown_005de024, 0, 0, 0x140, 0x100);
        }

        // ---------------------------------------------------------- le compteur de mise au point
        if (*(byte*)Mem.Camera(0x005de0b4) != 0)
        {
            (*(byte*)Mem.Camera(0x005de0b4))--;

            block[0] = (int)(nint)SpriteAtlas(0x00566c28);
            block[2] = (0x10 << 0x10) | 8;
            cursor = Prims.DrawTextRun(otBase, cursor, block, Mem.Entity(0x005fa5d0), 0, 0, 0x10);

            block[2] += 10 << 0x10;
            cursor = Prims.DrawTextRun(otBase, cursor, block, (byte*)0x564a14, 0, 0, -1);

            block[2] = ((block[2] >> 0x10) << 0x10) | (((block[2] & 0xffff) + 0x12) & 0xffff);
            block[0] = (int)(nint)SpriteAtlas(0x00566c30);
            cursor = DrawIconGlyph_0051BE90(otBase, cursor, block, *(short*)Mem.Arena(0x005e9a22), 5, 0);
        }

        // ---------------------------------------------------------- les deux indicateurs d'ecran
        s_Unknown_005de15c = 1;
        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;

        UiWin.EmitViewportRect(otBase + 2, &cursor);
        UiWin.EmitClipRect(otBase + 2, &cursor);

        s_Unknown_005de15c = 0;

        byte* indicator = null;
        if (s_Unknown_005de54c == 2) indicator = SpriteAtlas(0x00564788);
        if (s_Unknown_005de54c == 1 && s_Unknown_005de550 == 2) indicator = SpriteAtlas(0x00564790);

        if (indicator != null)
        {
            // L'indicateur pulse : le sinus du compteur de fondu module son **echelle**, dans les
            // deux axes, autour de 0x1000 (= 1.0). La position (0x130, 0x10) ne bouge pas.
            //
            // **Ghidra se trompe ici aussi**, et dans l'autre sens que la rose des vents : il rend
            // le premier pouls par `uStack_40 = CONCAT22(uStack_40._2_2_, pouls)`, ce qui ecraserait
            // le X par une valeur de l'ordre de 0x1000 — hors ecran. Le desassemblage donne
            // `[esp+3Ch]=130h`, `[esp+3Eh]=10h`, `[esp+40h]=pouls`, `[esp+46h]=pouls` avec le bloc
            // a `esp+34h` : position conservee, pouls dans +0x0c **et** +0x0e. Releve le 2026-08-22.
            block[0] = (int)(nint)indicator;
            *(short*)(b + 8) = 0x130;
            *(short*)(b + 0xa) = 0x10;
            *(short*)(b + 0xc) = (short)(((int)Winsys3d.GetSinTableEntry(
                (int)(s_FadeCounter_005e9960 << 9)) >> 3) + 0x1000);
            *(short*)(b + 0xe) = (short)(((int)Winsys3d.GetSinTableEntry(
                (int)(s_FadeCounter_005e9960 << 9)) >> 3) + 0x1000);
            *(short*)(b + 0x10) = 0;

            // +0x04 n'est **pas** ecrit par l'original avant 0x0051833d : le mot de base part avec
            // ce que la pile portait. Voir legacy-bug.md LB-008 ; on ne comble pas, on transcrit.
            cursor = Prims.DrawSpritePrimitive(otBase, cursor, block, 0, 0);
        }

        // ---------------------------------------------------------- le panneau d'interface
        s_Unknown_005de15c = 1;
        s_Unknown_005de154 = s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7 ? 0 : 0xa0;
        s_Unknown_005de158 = (10 - Mem.s_SizeSetting_005de160) * 0xc;

        if (s_Unknown_00563c5c != 0)
        {
            cursor = Ui.RunTextEngine(cursor, s_WorldMapDescriptor_005633b0);
        }

        // Le panneau glisse vers -0x50 ou -0x40 selon qu'il est ouvert, d'un quart par image, et
        // ne se redessine que tant qu'il bouge.
        short before = *(short*)(s_WorldMapDescriptor_005633b0 + 6);
        *(short*)(s_WorldMapDescriptor_005633b0 + 6) +=
            (short)((((s_Unknown_00563c77 & 1) != 0 ? 0x70 : 0) - 0x50 - before) >> 2);

        if ((s_Unknown_00563c77 & 1) == 0 || before != *(short*)(s_WorldMapDescriptor_005633b0 + 6))
        {
            s_Unknown_00563c77 |= 0x20;
        }
        else
        {
            s_Unknown_00563c77 &= 0xdf;
        }

        if ((s_Unknown_00563c77 & 1) != 0 || before != *(short*)(s_WorldMapDescriptor_005633b0 + 6))
        {
            cursor = UiWin.DrawUiPanel(otBase, cursor, s_WorldMapDescriptor_005633b0);
        }

        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;
        s_Unknown_005de15c = 0;

        if (debugLatch == 0 && s_DebugOverlayEnabled_005de020 != 0)
        {
            throw new NotImplementedException(
                "FUN_00516fc0 : l'affichage de mise au point du bloc d'affichage " +
                "(0x005182b0 a 0x00518473) n'est pas transcrit. Il compare les trois bancs de " +
                "drapeaux a leurs miroirs d'image precedente et liste les bits qui ont change, " +
                "avec le descripteur 0x00563c78 et les tables de chaines 0x00563350 / 0x00563360, " +
                "toutes relevees dans DataSymbols.md. DAT_005de020 vaut 0 au chargement, donc ce " +
                "bloc ne s'atteint que par la touche de mise au point.");
        }

        DrawCompassRose(otBase, otEntry, ref cursor, block);
        DrawPartyPanels(otBase, ref cursor, block, mainMem);
        DrawWorldCells(otBase, otEntry, ref cursor, mainMem);

        *cursorSlot = cursor;
    }

    /// <summary>
    /// L'indice, dans le cadre de pile modelise, de l'emplacement que Ghidra nomme par le decalage
    /// <paramref name="ghidraOffset"/> (<c>uStack_48</c> → 0x48). Le cadre va de <c>piStack_74</c>
    /// a <c>uStack_04</c>, et les fonctions de dessin en recoivent l'ADRESSE : c'est pour ça qu'il
    /// faut un bloc contigu et non des variables separees.
    /// </summary>
    private static int SlotOf(int ghidraOffset) => (0x74 - ghidraOffset) / 4;

    /// <summary>
    /// Ghidra: <c>LAB_00518473</c>..<c>0x005185f0</c> — <b>la rose des vents</b>. La camera est
    /// mise de cote, remplacee par une vue fixe a 800 unites, et le modele de la boussole est emis
    /// avec ses huit graduations placees par sinus/cosinus autour du cap courant. La pose est
    /// ensuite restituee a l'identique.
    /// </summary>
    private static void DrawCompassRose(uint* otBase, uint* otEntry, ref uint* cursor, int* unused)
    {
        int* frame = stackalloc int[SlotOf(0x04) + 1];
        short* pose = (short*)Mem.s_CameraVector_00604928;

        Winsys3d.Ordinal_378(0x80, 0xb8);   // SetGeomOffset(128, 184)

        // Mise de cote du decalage monde, de la pose et de la matrice.
        *(int*)Mem.Entity(0x005fa520) = Mem.s_Unknown_006008d0;
        *(int*)Mem.Entity(0x005fa524) = Mem.s_Unknown_006008d4;
        *(int*)Mem.Entity(0x005fa528) = Mem.s_Unknown_006008d8;
        *(short*)Mem.Entity(0x005fa534) = pose[0];
        *(short*)Mem.Entity(0x005fa536) = pose[1];
        *(short*)Mem.Entity(0x005fa538) = pose[2];

        for (int i = 0; i < 8; i++)
        {
            ((uint*)Mem.Arena(0x005e9b00))[i] = ((uint*)Mem.s_CameraMatrix_006008e0)[i];
        }

        Mem.s_Unknown_006008d0 = 0;
        Mem.s_Unknown_006008d4 = 0;
        Mem.s_Unknown_006008d8 = 0;

        pose[0] = 0x400;
        RotMatrix(pose, (short*)Mem.s_CameraMatrix_006008e0);
        Mem.CameraTranslation[0] = 0x41;
        Mem.CameraTranslation[1] = 0x21;
        Mem.CameraTranslation[2] = 800;

        s_Unknown_005de15c = 1;
        int slide;
        if (s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7)
        {
            slide = Mem.s_SizeSetting_005de160 - 0xb;
            s_Unknown_005de154 = 0;
        }
        else
        {
            slide = Mem.s_SizeSetting_005de160 + 9;
            s_Unknown_005de154 = 0xa0;
        }

        s_Unknown_005de158 = slide * 0xc;

        uint* pen = cursor;
        UiWin.EmitViewportRect(otBase + 2, &pen);
        cursor = pen;

        frame[SlotOf(0x30)] = 0x800;
        frame[SlotOf(0x2c)] = 0x800;
        frame[SlotOf(0x20)] = 0x1ffffff;
        frame[SlotOf(0x3c)] = 0;
        frame[SlotOf(0x38)] = 0;
        frame[SlotOf(0x34)] = 0;
        frame[SlotOf(0x28)] = 0;
        frame[SlotOf(0x24)] = 0x40000;

        // **Deux dereferencements, pas un.** `s_ModelResultB_005f7b40` est l'adresse du slot .data ;
        // RelocateModelBundle y range le **tableau d'offsets** du paquet, et le modele est son
        // element 0. Ghidra ecrit `*DAT_005f7b40` en typant DAT_005f7b40 comme un pointeur (il le
        // passe par adresse en 0x00532640, et le relit `DAT_005f7b40[1]` / `[3]` ailleurs), ce qui
        // masque un niveau. Un seul dereferencement donne le tableau lui-meme : son « en-tete »
        // se lit alors comme un modele dont +0x08 est un pointeur et +0x28 un petit entier, et la
        // boucle de faces part dans le decor.
        cursor = DrawWorldModel_0051C420(otBase + 2, cursor,
            **(byte***)s_ModelResultB_005f7b40, frame + SlotOf(0x3c), 0xe);

        frame[SlotOf(0x40)] &= unchecked((int)0xffff0000);
        frame[SlotOf(0x50)] = (int)(nint)SpriteAtlas(0x005646b8);
        frame[SlotOf(0x4c)] = 0x1ffffff;
        frame[SlotOf(0x48)] = 0xb00088;

        cursor = Prims.DrawSpriteVariant(otBase + 2, cursor, frame + SlotOf(0x50), 0, 0);
        cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, frame + SlotOf(0x50));

        frame[SlotOf(0x4c)] = 0x1ffffff;
        frame[SlotOf(0x50)] = (int)(nint)SpriteAtlas(0x005646c0);

        // Les **quatre** graduations — la boucle va de 0 a 0x1000 par pas de 0x400, soit les quatre
        // points cardinaux — placees autour du centre (0x9c, 0xc5) a vingt-deux unites de rayon.
        // L'indice 0..3 designe les descripteurs 0x005646c0, c8, d0, d8 : quatre voisins de la
        // plage relevee, d'ou l'exigence de contiguite sur SpriteAtlas.
        //
        // **Ghidra place le X dans le mauvais champ.** Il ecrit
        // `piStack_4c = CONCAT22(piStack_4c._2_2_, 0x9c - ...)`, soit +0x04 — le mot de base. Le
        // desassemblage dit `mov [esp+3Ch],cx` avec le bloc a `esp+34h`, donc **+0x08**, la moitie
        // basse de la position, dont le Y occupe deja la moitie haute. Releve le 2026-08-22 ;
        // la premiere transcription avait copie l'erreur, ce qui envoyait le X dans le mot de
        // couleur et laissait la graduation sur place.
        short tick = 0;
        for (int angle = 0; angle < 0x1000; angle += 0x400, tick++)
        {
            frame[SlotOf(0x40)] = (frame[SlotOf(0x40)] & unchecked((int)0xffff0000)) | (ushort)tick;

            int sin = (int)Winsys3d.GetSinTableEntry(angle - pose[1]);
            int cos = Winsys3d.GetCosTableEntry(angle - pose[1]);

            frame[SlotOf(0x48)] = ((0xc5 - (cos * 0x16 >> 0xc)) << 0x10)
                | (ushort)(0x9c - (short)(sin * 0x16 >> 0xc));

            cursor = Prims.DrawSpriteVariant(otBase + 2, cursor, frame + SlotOf(0x50), 0, 0);
        }

        cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, frame + SlotOf(0x50));
        pen = cursor;
        UiWin.EmitClipRect(otBase + 2, &pen);
        cursor = pen;

        // Restitution.
        for (int i = 0; i < 8; i++)
        {
            ((uint*)Mem.s_CameraMatrix_006008e0)[i] = ((uint*)Mem.Arena(0x005e9b00))[i];
        }

        Mem.s_Unknown_006008d0 = *(int*)Mem.Entity(0x005fa520);
        Mem.s_Unknown_006008d4 = *(int*)Mem.Entity(0x005fa524);
        Mem.s_Unknown_006008d8 = *(int*)Mem.Entity(0x005fa528);

        s_Unknown_005de15c = 0;
        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;

        pose[0] = *(short*)Mem.Entity(0x005fa534);
        pose[1] = *(short*)Mem.Entity(0x005fa536);
        pose[2] = *(short*)Mem.Entity(0x005fa538);

        Winsys3d.Ordinal_378(0xa0, 0x78);
    }

    /// <summary>
    /// Ghidra: <c>0x005185f0</c>..<c>0x00518e60</c> — <b>les panneaux des membres du groupe</b> :
    /// pour chacun, la barre de vie et de magie, les pictogrammes d'etat, puis son modele.
    /// </summary>
    private static void DrawPartyPanels(uint* otBase, ref uint* cursor, int* block, byte* mainMem)
    {
        byte* b = (byte*)block;

        // ============================================================ A. les nombres flottants
        //
        // Soixante-quatre emplacements de 0x28 octets a 0x005e9b60 — les memes que
        // TickEffectSlots_005e9b60 fait vivre. Chacun porte une position monde (+0x00), une valeur
        // (+0x0c), un mot d'attributs (+0x26) et son temoin d'activite (+0x27). Ghidra adresse
        // l'enregistrement par son octet +0x26, d'ou les decalages negatifs de la pseudo-C.
        block[0] = (int)(nint)SpriteAtlas(0x00566c38);
        *(short*)(b + 0x10) = 0;

        short* projected = stackalloc short[4];
        byte* slot = Mem.Arena(0x005e9b60);

        for (int i = 0; i < 0x40; i++, slot += 0x28)
        {
            if (slot[0x27] == 0) continue;
            if (Mdl.ProjectPoint((int*)slot, projected) == 0) continue;

            *(uint*)(b + 8) = *(uint*)projected;
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, block, (uint)*(short*)(slot + 0xc), 5, slot[0x26]);
        }

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, block);

        // ============================================================ B. un panneau par membre
        *(short*)Mem.Entity(0x005fa544) = 0;
        *(short*)Mem.Entity(0x005fa53a) = 1;
        *(uint*)Mem.Entity(0x005fa52c) = 0x0e000e00;
        *(nint*)Mem.Entity(0x005fa540) = (nint)SpriteAtlas(0x00564690);

        *Mem.Camera(0x005de05c) = 0;

        byte* character = mainMem + 0xf0000;          // Ghidra: iVar12, +0x2000 par membre
        short* slotEntry = (short*)Inp.s_SlotArea_005e9180;   // Ghidra: local_50, pas de 0x4c
        byte* record = Inp.s_SlotFlags_005e93c0;      // les enregistrements de 0x9c octets
        byte* panel = s_PartyPanelDescriptors_005dbc68;

        for (int member = 0; member < s_PartySize_005e9020;
             member++, character += 0x2000, slotEntry += 0x26, record += 0x9c,
             panel += GameMemory.UiDescriptorStride)
        {
            uint recordFlags = *(uint*)record;                    // +0x00
            uint stateFlags = *(uint*)(record + 0x90);            // +0x90

            // La hauteur que la camera doit degager pour ce membre, cumulee dans DAT_005de05c.
            if ((stateFlags & 0x8000) == 0)
            {
                int reach = ((int)Winsys3d.GetSinTableEntry(
                                 *(short*)Mem.s_CameraVector_00604928) >> 3)
                          + *slotEntry;
                if (*Mem.Camera(0x005de05c) < reach) *Mem.Camera(0x005de05c) = reach;
            }

            s_Unknown_005de15c = 1;

            // Le decalage de dessin du panneau. Trois dispositions selon l'avancement de
            // l'animation d'ouverture du menu ; les quatre tables sont relevees dans
            // DataSymbols.md et le choix se lit ligne a ligne.
            if (s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de154 = Mem.s_SizeSetting_005de160 < 10
                    ? (((member & 1) != 0 ? unchecked((int)0xffffffce) : 0) + 0x19)
                    : 0;
                s_Unknown_005de158 = (Mem.s_SizeSetting_005de160 - 0xb) * 0xc;
            }
            else
            {
                s_Unknown_005de158 = Mem.s_SizeSetting_005de160 < 5
                    ? (Mem.s_SizeSetting_005de160 + 9) * 0xc
                    : (member > 1 ? -0xe : 0xe) + (Mem.s_SizeSetting_005de160 + 9) * 0xc;

                if (Mem.s_SizeSetting_005de160 >= 9)
                    s_Unknown_005de154 = s_PanelX_00564928[member];
                else if (Mem.s_SizeSetting_005de160 >= 5)
                    s_Unknown_005de154 = s_PanelX_00564938[member];
                else
                    s_Unknown_005de154 = Mem.s_SizeSetting_005de160 < 1
                        ? s_PanelX_00564958[member]
                        : s_PanelX_00564948[member];
            }

            // Le coin du panneau, dans le descripteur de fenetre du membre. Un membre touche
            // tremble : deux tirages independants deplacent le panneau de -2 a +1 sur chaque axe.
            short* corner = (short*)(panel + 4);
            corner[0] = (short)(((member & 1) != 0 ? 0xd1 : 0) + 5);
            corner[1] = (short)(((member & 2) != 0 ? 0x1c : 0) + 0xa6
                                + (s_PartySize_005e9020 > 2 ? 0 : 0xe));

            if ((short)*(uint*)(record + 0x96) != 0)
            {
                corner[0] += (short)((CrtRand() & 3) - 2);
                corner[1] += (short)((CrtRand() & 3) - 2);
            }

            *(uint*)(panel + 0x1c) = (uint)(((recordFlags & 1) != 0
                ? unchecked((int)0xff5100af) : 0) + 0x02cf1020);

            // ---- les deux jauges : valeur courante en couleur, maximum en blanc
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 0x12);

            uint gauge = GaugeColour(*(short*)(character + 0x14c), *(short*)(character + 0x148));
            *(uint*)(panel + 0x44) = gauge & 0xffffff;
            *(uint*)(b + 4) = gauge;

            block[0] = (int)(nint)SpriteAtlas(0x00566c20);
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14c), 0x84, 0);

            *(short*)(b + 8) += 0x14;
            *(uint*)(b + 4) = 0x1000000;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x148), 4, 0);

            *(uint*)(b + 4) = GaugeColour(*(short*)(character + 0x14e), *(short*)(character + 0x14a));
            *(short*)(b + 8) += 0x18;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14e), 0x84, 0);

            *(short*)(b + 8) += 0x14;
            *(uint*)(b + 4) = 0x1000000;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14a), 4, 0);

            // ---- le nom, puis deux barres obliques de separation
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 10);
            *(uint*)(b + 4) = 0x1000000;
            block[0] = (int)(nint)SpriteAtlas(0x00566c28);
            cursor = Prims.DrawTextRunAlias(otBase + 2, cursor, block, character + 0x100, 0, 0, 8);

            *(short*)(b + 8) += 0x10;
            *(short*)(b + 0xa) += 8;
            block[0] = (int)(nint)SpriteAtlas(0x00566c18);
            cursor = Prims.DrawTextRun(otBase + 2, cursor, block, s_SlashLabel_00564a0c, 0, 0, -1);

            *(short*)(b + 8) += 0x2c;
            cursor = Prims.DrawTextRun(otBase + 2, cursor, block, s_SlashLabel_00564a0c, 0, 0, -1);

            // ---- les neuf pictogrammes d'etat, un par bit de s_StatusBits_00564900
            //
            // L'indice +0x10 avance a chaque rang, **qu'il soit dessine ou non** : les neuf
            // descripteurs sont contigus a partir de 0x005648b8, et c'est le rang qui choisit
            // lequel — pas l'ordre d'emission.
            *(short*)(b + 0x10) = 0;
            block[0] = (int)(nint)SpriteAtlas(0x005648b8);
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 0x1a);

            // `cursor` est un parametre `ref`, dont on ne peut pas prendre l'adresse ; le curseur
            // passe par une locale le temps des deux encadrements, comme ailleurs dans ce fichier.
            uint* pen = cursor;
            UiWin.EmitViewportRect(otBase + 2, &pen);

            uint status = *(uint*)(character + 0x140);
            for (int bit = 0; bit < s_StatusBits_00564900.Length; bit++)
            {
                if ((status & s_StatusBits_00564900[bit]) != 0)
                    pen = Prims.DrawSpriteVariant(otBase + 2, pen, block, 0, 0);

                *(short*)(b + 8) += 8;
                (*(short*)(b + 0x10))++;
            }

            UiWin.EmitClipRect(otBase + 2, &pen);
            cursor = pen;

            *(short*)(b + 0x10) = 0;
            cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, block);
            cursor = UiWin.DrawUiPanel(otBase + 2, cursor, panel);

            s_Unknown_005de154 = 0;
            s_Unknown_005de158 = 0;
            s_Unknown_005de15c = 0;

            // ---- le membre lui-meme dans le monde
            if (VSyncDelayed() != 0) continue;

            if ((recordFlags & 0x4000) == 0)
            {
                int kind = record[4] * 4;
                int* transform = (int*)(record + 0x1c);

                cursor = Mdl.DrawAnimatedModel(otBase, cursor,
                    *(byte**)(*(int**)(Mem.Arena(0x005e9680) + kind)
                              + *(short*)(record + 0x18)),
                    *(int**)(Mem.Arena(0x005e9800) + kind),
                    transform,
                    *(short*)(record + 0x1a),
                    (uint)*(short*)(record + 0x0a));

                // L'ombre portee, si le decor la demande et si le membre est a l'ecran.
                if (character[0x2b9] != 0 && Mdl.ProjectPoint(transform, projected) != 0)
                {
                    block[0] = (recordFlags & 0x10000) == 0 && (byte)slotEntry[4] != 8
                        ? (int)(nint)SpriteAtlas(0x00564798 + *((byte*)slotEntry + 9) * 8)
                        : (int)(nint)SpriteAtlas(0x005647b8);

                    *(short*)(b + 8) = (short)(projected[0] - 0xc);
                    *(short*)(b + 0xa) = projected[1];

                    cursor = Prims.DrawSpriteVariant(otBase + 3, cursor, block, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase + 3, cursor, block);
                }

                if (character[0x2ba] != 0)
                {
                    cursor = DrawGroundDisc(otBase, cursor, transform, member);
                }

                if (*(int*)(record + 0x3c) != 0)
                {
                    cursor = DrawStatusBubble(otBase, cursor, transform, record);
                }
            }

            // La lueur du membre : sa position devient celle de la particule partagee.
            if ((stateFlags & 0x9000) == 0 && (recordFlags & 0x1000) == 0)
            {
                *(uint*)Mem.Entity(0x005fa524) = *(uint*)(record + 0x64);
                *(uint*)Mem.Entity(0x005fa520) = *(uint*)(record + 0x1c);
                *(uint*)Mem.Entity(0x005fa528) = *(uint*)(record + 0x24);

                // Ghidra: la paire `DAT_00564a4c = 0 / = 1` autour de l'appel, quatre fois dans le
                // binaire — elle force le chemin « sans capacite » de l'emetteur pour cette seule
                // primitive. Voir GameMemory, qui recense les paires.
                Mem.s_Unknown_00564a4c = 0;
                cursor = EmitParticle(otBase + 5, cursor, (int*)Mem.Entity(0x005fa520), 0, 0);
                Mem.s_Unknown_00564a4c = 1;
            }

            if ((recordFlags & 0x8000000) != 0)
            {
                cursor = DrawSpeechOverlay_005309B0(otBase, cursor, record, slotEntry, character);
            }
        }
    }

    /// <summary>
    /// Ghidra: les deux moities identiques du calcul de couleur de jauge, dans le bloc des
    /// panneaux. Le rapport <c>courant * 0xff / maximum</c> passe par deux regimes : sous la
    /// moitie il monte le vert en gardant le rouge sature, au-dessus il baisse le rouge. Le
    /// resultat sert deux fois — masque sur 24 bits dans le descripteur de fenetre, entier comme
    /// mot de base du sprite.
    /// </summary>
    private static uint GaugeColour(short current, short maximum)
    {
        int ratio = current * 0xff / maximum;
        return ratio < 0x80
            ? ((uint)(ratio << 9) >> 8 & 0xffffff) << 8 | 0xff
            : (uint)(ratio + 0x7f80) * 0x20000 | 0xffff;
    }

    /// <summary>Ghidra: le sous-bloc <c>character[0x2ba]</c> — le disque au sol sous le membre,
    /// pose par <c>FUN_0051c420</c> sur le modele <c>DAT_005f7b40[3]</c>.</summary>
    private static uint* DrawGroundDisc(uint* otBase, uint* cursor, int* transform, int member)
    {
        int* placement = stackalloc int[10];
        for (int i = 0; i < 10; i++) placement[i] = 0;

        Mem.CopyDwords((uint*)transform, (uint*)placement, 10);
        placement[1] += 0x32000;

        *(short*)((byte*)placement + 0x18) = (short)(s_FadeCounter_005e9960 << 6);
        placement[7] = (int)s_Unknown_005e9814;

        return DrawWorldModel_0051C420(otBase, cursor,
            ((byte**)*(byte***)s_ModelResultB_005f7b40)[3], placement,
            (uint)((s_FadeCounter_005e9960 << 6) & 0xffff0000) | s_PanelExtra_00563348[member]);
    }

    /// <summary>Ghidra: le sous-bloc <c>record[0x3c] != 0</c> — la bulle au-dessus du membre, dont
    /// la hauteur sort de <c>FUN_00538bc0</c>.</summary>
    private static uint* DrawStatusBubble(uint* otBase, uint* cursor, int* transform, byte* record)
        => throw new NotImplementedException(
            "FUN_00516fc0 : la bulle d'etat au-dessus du membre n'est pas transcrite. Elle a " +
            "besoin de FUN_00538bc0 (13 l., Blaze.c:84048), qui rend une hauteur prise dans l'une " +
            "de deux tables selon le bit 0x800000 de l'enregistrement : DAT_0056d440 (pas de 8, " +
            "dans .data de l'exe, a relever) ou DAT_005ea5b8 (pas de 0x120, dans l'arene de jeu). " +
            "Le reste du sous-bloc est un EmitParticle a l'emplacement 0x10001000.");

    /// <summary>Ghidra: FUN_005309b0 @ 0x005309b0 (147 l.) — la surimpression de dialogue.</summary>
    private static uint* DrawSpeechOverlay_005309B0(
        uint* otBase, uint* cursor, byte* record, short* slotEntry, byte* character)
        => throw new NotImplementedException(
            "FUN_005309b0 (0x005309b0) : non transcrite. 147 lignes dans Blaze.c — la " +
            "surimpression de dialogue, gardee par le bit 0x8000000 de l'enregistrement de membre. " +
            "Elle demande les tables DAT_0056a851 (choix d'icone), DAT_0056aae0 et DAT_0056ab30 " +
            "(descripteurs), toutes dans .data de l'exe et non relevees.");

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005dbc68</c> — <b>quatre descripteurs de fenetre</b>, un par membre du
    /// groupe, au meme pas <c>0x8c8</c> que ceux de <see cref="GameMemory.UiDescriptorStride"/>.
    /// Le bloc est **nul au chargement** (verifie : l'adresse tombe dans la queue non initialisee
    /// de <c>.data</c>), donc tout ce qu'il porte vient du code ; le bloc des panneaux n'en ecrit
    /// que le coin (+4/+6), la teinte (+0x1c) et la couleur de jauge (+0x44).
    ///
    /// <para>Alloue bas comme les trois de <see cref="GameMemory"/> : un descripteur de fenetre
    /// porte sa propre table d'affichage a +0xa4, que <c>ClearOTagR</c> chaine sur 24 bits.</para>
    /// </summary>
    private static byte* s_PartyPanelDescriptorsStorage;

    private static byte* s_PartyPanelDescriptors_005dbc68
        => s_PartyPanelDescriptorsStorage != null
            ? s_PartyPanelDescriptorsStorage
            : (s_PartyPanelDescriptorsStorage =
                   AllocLowZeroed(4 * (uint)GameMemory.UiDescriptorStride));

    /// <summary>Ghidra: <c>&amp;DAT_00563348</c> — quatre demi-mots, un par membre, qui forment la
    /// moitie basse du mot d'attributs passe au disque au sol. Releves dans le binaire.</summary>
    private static readonly ushort[] s_PanelExtra_00563348 = [0x000c, 0x0002, 0x0003, 0x000e];


    /// <summary>
    /// Ghidra: <c>0x0051905b</c>..<c>0x00519703</c> — <b>le balayage des cellules de decor</b>, le
    /// gros du rendu du monde.
    ///
    /// <para>La borne indiquee par le TODO — 0x00518e60 — tombait encore dans la boucle des
    /// panneaux (la bulle d'etat est en 0x00518f02) ; le bloc commence au test du second voile,
    /// juste apres la sortie de cette boucle.</para>
    ///
    /// <para>Le cap de la camera (<c>DAT_0060492a</c>) choisit un des <b>huit octants</b>. L'octant
    /// donne le coin de depart (<see cref="GameMemory.s_CellSweepCorner_00564548"/>) et la liste des cellules
    /// a sauter (<see cref="GameMemory.s_CellSkipTables_00564558"/>). Le balayage descend ensuite 7 rangs de
    /// 7 colonnes, en <b>decroissant</b> sur les deux axes.</para>
    ///
    /// <para>Chaque cellule porte <b>deux listes chainees</b>, prises dans les deux grilles 32x32
    /// que le prologue efface : <c>MainMem + 0x1c5000</c> (indice 0x38a0) pour les entites, et
    /// <c>MainMem + 0x1c4000</c> (indice 0x3880) pour le decor pose. Les deux se parcourent par le
    /// chainage <c>+0x68</c> des enregistrements de 0x9c octets.</para>
    ///
    /// <para>Viennent enfin les <b>trois listes de morceaux</b> — trois suites d'octets terminees
    /// par <c>0xff</c>, prises a la file dans le meme tampon — puis le <b>ciel</b>, garde par le
    /// bit 0x40 du premier octet de <c>DAT_005eca80</c>.</para>
    /// </summary>
    private static void DrawWorldCells(uint* otBase, uint* otEntry, ref uint* cursor, byte* mainMem)
    {
        // ---------------------------------------------------------- le second voile plein ecran
        // Ghidra: DAT_005de02b, c'est-a-dire l'octet de poids fort de DAT_005de028 — meme montage
        // que DAT_005de024/DAT_005de027 en tete du bloc d'affichage.
        if ((byte)(Mem.s_Unknown_005de028 >> 0x18) != 0)
        {
            cursor = FillScreenQuad_0051F940(
                otBase + 2, cursor, Mem.s_Unknown_005de028, 0, 0, 0x140, 0x100);
        }

        // ---------------------------------------------------------- l'octant, le coin, les sauts
        short* pose = (short*)Mem.s_CameraVector_00604928;
        int octant = (((ushort)pose[1] + 0x100) >> 9) & 7;

        int skipCursor = octant * 0x10;
        int skipKey = GameMemory.s_CellSkipTables_00564558[skipCursor]
                    | GameMemory.s_CellSkipTables_00564558[skipCursor + 1] << 8;
        skipCursor += 2;

        ushort corner = GameMemory.s_CellSweepCorner_00564548[octant];
        int cellX = ((-Mem.s_Unknown_006008d0 >> 0xc) + 0x2000 >> 9) + (corner & 0xff);
        int cellZ = ((-Mem.s_Unknown_006008d8 >> 0xc) + 0x2000 >> 9) + (corner >> 8);

        short* projected = stackalloc short[2];

        for (int row = 7; row != 0; row--, cellZ--)
        {
            int x = cellX;

            for (int col = 7; col != 0; col--, x--)
            {
                if (skipKey == (row << 8 | col))
                {
                    // LB-009 : la cle suivante est lue sur **un seul octet** (`movzx ax, byte
                    // [edi]` en 0x00519143) alors que le curseur avance de deux et que la table
                    // porte des cles de 16 bits. La cle devient donc <= 0xff et ne peut plus
                    // egaler `rang << 8 | colonne`, qui vaut au moins 0x0101 : une seule cellule
                    // est sautee par image au lieu de huit. Transcrit tel quel.
                    skipKey = GameMemory.s_CellSkipTables_00564558[skipCursor];
                    skipCursor += 2;
                    continue;
                }

                // Les deux bornes sont comparees sur 16 bits, ce qui rejette aussi les negatifs.
                if ((ushort)x >= 0x20 || (ushort)cellZ >= 0x20) continue;

                // ==================================================== la grille des entites
                for (byte* e = *(byte**)(mainMem + ((cellZ + 0x38a0) * 0x20 + x) * 4);
                     e != null; e = *(byte**)(e + 0x68))
                {
                    uint flags = *(uint*)e;
                    if ((flags & 0x4000) != 0 || (e[0x36] & 0x80) != 0) continue;

                    // La lueur ne suit que les chemins qui retombent sur LAB_005193ff ; les deux
                    // chemins de modele (0x80000 et 0x100000) sautent directement au maillon
                    // suivant.
                    bool glow = true;

                    if ((flags & 0x8000) == 0)
                    {
                        // -------- les particules : un decalage de couche pris dans l'entite,
                        // ou la couche d'arriere-plan.
                        if ((flags & 0x100000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x07000700;
                            cursor = EmitParticle(otBase + *(short*)(e + 0x2e), cursor,
                                (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        }
                        else if ((flags & 0x80000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x07000700;
                            cursor = EmitParticle(otBase - 5, cursor,
                                (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        }
                    }
                    else if (VSyncDelayed() == 0)
                    {
                        if ((flags & 0x80000) != 0)
                        {
                            cursor = DrawWorldModel_0051C420(otBase, cursor,
                                ((byte**)*(byte***)s_ModelResultB_005f7b40)[1],
                                (int*)(e + 0x1c), 0xc);
                            glow = false;
                        }
                        else if ((flags & 0x100000) != 0)
                        {
                            // Le paquet de modeles : la premiere serie tant que l'indice reste
                            // sous DAT_005edec4, la seconde au-dela — le meme debordement volontaire
                            // que les tables de sauts d'entites.
                            byte assetIndex = e[4];
                            byte* model = assetIndex < s_Unknown_005edec4
                                ? ((byte**)*(byte***)s_ModelResultA_005fa588)[assetIndex]
                                : ((byte**)*(byte***)Mem.Entity(0x00600780))
                                      [assetIndex - s_Unknown_005edec4];

                            cursor = DrawWorldModelVariant_0051E700(
                                otBase + *(short*)(e + 0x2e), cursor, model,
                                (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                            glow = false;
                        }
                        else if ((flags & 0x440000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x0e000e00;

                            int kind = e[4] * 4;
                            cursor = Mdl.DrawAnimatedModel(otBase, cursor,
                                *(byte**)(*(int**)(Mem.s_AnimationTable_005e9640 + kind)
                                          + *(short*)(e + 0x18)),
                                *(int**)(s_ModelTable_005e96a0 + kind),
                                (int*)(e + 0x1c),
                                *(short*)(e + 0x1a),
                                *(ushort*)(e + 0x0a));

                            // Les trois nombres de mise au point au-dessus de l'entite. Garde par
                            // le verrou du mode developpeur, donc hors du chemin ordinaire.
                            if (Mem.s_DebugModeLatch_005633a8 == 0 && s_DebugOverlayEnabled_005de020 != 0
                                && Mdl.ProjectPoint((int*)(e + 0x1c), projected) != 0)
                            {
                                int* dbg = stackalloc int[5];
                                dbg[0] = (int)(nint)SpriteAtlas(0x00566c30);
                                dbg[1] = 0x1000000;
                                byte* db = (byte*)dbg;
                                *(short*)(db + 8) = (short)(projected[0] - 0xc);
                                *(short*)(db + 0xa) = projected[1];

                                // La table des blocs de statistiques a sa propre allocation ; la
                                // lire par `Mem.Arena` serait le defaut d'aliasing que
                                // ResolveEntityBlocks a porte jusqu'au 2026-08-23.
                                byte* stats = s_EntityStatBlocks_005ea560 + e[8] * 0x120;

                                cursor = DrawIconGlyph_0051BE90(
                                    otBase + 2, cursor, dbg, *(ushort*)(stats + 0x44), 5, 0);
                                *(short*)(db + 0xa) += 8;
                                cursor = DrawIconGlyph_0051BE90(
                                    otBase + 2, cursor, dbg, *(short*)(stats + 0x4c), 5, 0);
                                *(short*)(db + 0xa) += 8;
                                cursor = DrawIconGlyph_0051BE90(
                                    otBase + 2, cursor, dbg, *(short*)(stats + 0x4e), 5, 0);
                            }
                        }
                    }

                    // -------- la lueur de l'entite, meme montage que celle des membres du groupe
                    if (glow && (*(uint*)(e + 0x90) & 0x9000) == 0 && (flags & 0x1000) == 0)
                    {
                        *(uint*)Mem.Entity(0x005fa520) = *(uint*)(e + 0x1c);
                        *(uint*)Mem.Entity(0x005fa524) = *(uint*)(e + 0x64);
                        *(uint*)Mem.Entity(0x005fa528) = *(uint*)(e + 0x24);

                        Mem.s_Unknown_00564a4c = 0;
                        cursor = EmitParticle(otBase + 5, cursor, (int*)Mem.Entity(0x005fa520), 0, 0);
                        Mem.s_Unknown_00564a4c = 1;
                    }
                }

                // ==================================================== la grille du decor pose
                if (VSyncDelayed() != 0) continue;

                for (byte* e = *(byte**)(mainMem + ((cellZ + 0x3880) * 0x20 + x) * 4);
                     e != null; e = *(byte**)(e + 0x68))
                {
                    if ((*(uint*)e & 0x4000) != 0) continue;

                    // Les deux bits de poids fort de l'octet +0x07 — c'est-a-dire du mot d'etat —
                    // choisissent entre particule, modele statique et sa variante.
                    int kind = e[7] & 3;
                    byte** chunks = (byte**)Mem.Entity(0x005f7bc0)
                                  + (*Mem.Entity(0x00600838) & 0xff);

                    if (kind == 0)
                    {
                        Mem.s_Unknown_00564a4c = 0;
                        cursor = EmitParticle(otBase + *(short*)(e + 0x2e), cursor,
                            (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        Mem.s_Unknown_00564a4c = 1;
                    }
                    else if (kind == 1)
                    {
                        cursor = DrawWorldModel_0051C420(otBase, cursor, chunks[e[4]],
                            (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                    }
                    else if (kind == 2)
                    {
                        cursor = DrawWorldModelVariant_0051E700(otBase, cursor, chunks[e[4]],
                            (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                    }
                }
            }
        }

        // ---------------------------------------------------------- les trois listes de morceaux
        //
        // Le bloc de pose remis a neuf : position nulle, echelles 0x1000, orientation nulle,
        // teinte blanche. C'est le meme bloc de dix mots que DrawGroundDisc recopie.
        int* placement = stackalloc int[10];
        for (int i = 0; i < 10; i++) placement[i] = 0;
        placement[3] = 0x10001000;
        placement[4] = 0x1000;
        placement[7] = 0xffffff;

        byte* list = ((byte**)*(byte**)(s_Unknown_005e97cc + 0x80))[s_Unknown_005f2ce0[8]];

        if (VSyncDelayed() != 0) return;

        // Les trois bases sont trois octets consecutifs de DAT_00600830/34 ; les trois suites
        // d'octets se suivent dans le meme tampon, chacune close par 0xff.
        byte** table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600832);
        for (; *list != 0xff; list++)
        {
            cursor = DrawChunkListA_0051FA40(otBase, cursor, table[*list], placement);
        }

        table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600834);
        for (list++; *list != 0xff; list++)
        {
            cursor = DrawChunkListB_00521000(otEntry, cursor, table[*list], placement);
        }

        table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600836);
        for (list++; *list != 0xff; list++)
        {
            cursor = DrawChunkListC_00522140(otEntry, cursor, table[*list], placement);
        }

        // ---------------------------------------------------------- le ciel
        if ((**(byte**)Mem.Entity(0x005eca80) & 0x40) != 0)
        {
            int* sky = *(int**)*(byte**)(s_Unknown_005e97cc + 0x3c);
            cursor = Prims.DrawSpriteVariant(otEntry, cursor, sky, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sky);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051f940 @ 0x0051f940 (18 l.) — <b>le voile plein ecran</b> : un rectangle plat
    /// de cinq mots, chaine dans la table d'affichage avec la longueur 0x04000000. La couleur est
    /// prise sur 24 bits et le voile est saute si elle est nulle.
    /// </summary>
    private static uint* FillScreenQuad_0051F940(
        uint* otEntry, uint* prim, uint colour, uint x, short y, uint width, short height)
    {
        if ((colour & 0xffffff) == 0) return prim;

        prim[2] = colour & 0xffffff | 0x62000000;
        prim[1] = 0xe1000200;
        prim[3] = (uint)((int)y << 0x10) | x & 0xffff;
        prim[4] = (uint)((int)height << 0x10) | width & 0xffff;
        *prim = *otEntry | 0x4000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 5;
    }

    /// <summary>Ghidra: FUN_0051e700 @ 0x0051e700 — la seconde voie d'emission d'un modele de
    /// decor, sœur de <c>FUN_0051c420</c> : meme signature a cinq arguments, meme place dans les
    /// deux grilles, mais un emplacement d'ordering table calcule **par face**. Transcrite dans
    /// <c>Engine/Model3d.cs</c>, a cote de ses deux sœurs.</summary>
    private static uint* DrawWorldModelVariant_0051E700(
        uint* otBase, uint* prim, byte* model, int* placement, uint flags)
        => Mdl.DrawDepthSortedModel(otBase, prim, model, placement, flags);

    /// <summary>Ghidra: FUN_0051fa40 @ 0x0051fa40 — la premiere des trois listes de morceaux,
    /// celle qui se dessine sur la base d'ordering table et trie chaque face a sa profondeur :
    /// <b>le terrain</b>. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListA_0051FA40(
        uint* otBase, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkCellsSorted(otBase, prim, chunk, placement);

    /// <summary>Ghidra: FUN_00521000 @ 0x00521000 — la deuxieme liste de morceaux : meme balayage
    /// de cellules, sans tri par face. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListB_00521000(
        uint* otEntry, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkCells(otEntry, prim, chunk, placement);

    /// <summary>Ghidra: FUN_00522140 @ 0x00522140 — la troisieme liste de morceaux : les faces se
    /// prennent a la file dans l''en-tete, sans grille. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListC_00522140(
        uint* otEntry, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkFaces(otEntry, prim, chunk, placement);

    /// <summary>Ghidra: FUN_0051be90 @ 0x0051be90 (74 l.) — le second chemin de pictogramme, celui
    /// que <c>Engine/UiText.cs</c> cite comme distinct de son propre glyphe en ligne.</summary>
    private static uint* DrawIconGlyph_0051BE90(
        uint* otEntry, uint* prim, int* block, int value, int digits, int flags)
        => throw new NotImplementedException(
            $"FUN_0051be90 (0x0051be90), valeur {value} : non transcrite. 74 lignes dans Blaze.c.");

    /// <summary>Ghidra: FUN_0051c420 @ 0x0051c420 — l'emission d'un modele de decor. Transcrite
    /// dans <c>Engine/Model3d.cs</c>, a cote de sa sœur animee FUN_0051d1f0 : meme banc GTE, memes
    /// primitives, et plusieurs ecrans l'appellent.</summary>
    private static uint* DrawWorldModel_0051C420(
        uint* otEntry, uint* prim, byte* model, int* placement, uint flags)
        => Mdl.DrawStaticModel(otEntry, prim, model, placement, flags);

    private static byte* s_SpriteAtlasStorage;

    /// <summary>Ghidra: <c>AU_00564a05._51_4_</c>, le dword de <c>0x00564a56</c> — releve **nul**
    /// dans le binaire. Il garde le glissement du panneau d'interface ; rien de transcrit ne
    /// l'ecrit encore. TODO(REVERSE) : trouver son ecrivain.</summary>
    private static int s_MenuSlideDone_00564a56;

    // Les deux plages de descripteurs, relevees **contigument** dans le binaire le 2026-08-22.
    //
    // La contiguite n'est pas un detail de commodite, c'est la correction d'un defaut : les
    // fonctions de dessin lisent leur descripteur a `bloc[0] + (short)bloc[4] * 8`, c'est-a-dire
    // qu'elles **indexent en avant** a partir de l'adresse qu'on leur donne. La rose des vents s'en
    // sert pour ses huit orientations : elle passe `&DAT_005646c0` et un indice 0..7, et lit donc
    // 0x005646c0, c8, d0, d8, e0, e8, f0, f8 — huit descripteurs voisins dans le binaire.
    //
    // La premiere version de ce relevé rangeait onze descripteurs isoles dans un tableau de onze
    // entrees, dans l'ordre ou je les avais rencontres. Indexer en avant y donnait le mauvais
    // descripteur des l'indice 1, et **sortait du tampon** des l'indice 4 : la fonction lisait un
    // couple couleur/attributs pris dans le tas, et la corruption se voyait une image plus loin,
    // dans la camera. Un descripteur ne se releve pas seul — on releve la plage.
    private const int AtlasA_Base = 0x00564680, AtlasA_End = 0x00564900;
    private const int AtlasB_Base = 0x00566c18, AtlasB_End = 0x00566c40;

    private static byte* s_AtlasBlockB;

    private static byte* SpriteAtlas(int va)
    {
        if (s_SpriteAtlasStorage == null)
        {
            // 0x00564680 .. 0x00564900 — la grande plage. Elle ne contient pas que des
            // descripteurs (0x00564740..0x00564770 sont des couples de coordonnees), mais on la
            // releve telle quelle : c'est ce qui rend l'indexation en avant fidele.
            ReadOnlySpan<uint> blocA =
            [
                0x0000f6c0, 0x00018040, 0x00018040, 0x00003c00,   // 0x00564680
                0x10181800, 0x01100302, 0x17179060, 0x0a110301,   // 0x00564690
                0x17179060, 0x0a110301, 0x17179060, 0x0a110301,   // 0x005646a0
                0x17179060, 0x0a110301, 0x3030a830, 0x0e110001,   // 0x005646b0
                0x0808f8a0, 0x0e110001, 0x0808f8b0, 0x0e110001,   // 0x005646c0
                0x0808f8b8, 0x0e110001, 0x0808f8a8, 0x0e110001,   // 0x005646d0
                0x17179018, 0x06110001, 0x17179030, 0x07110001,   // 0x005646e0
                0x4000c000, 0x003f0300, 0x20207000, 0x00120001,   // 0x005646f0
                0x20207020, 0x00120001, 0x20207040, 0x00120001,   // 0x00564700
                0x20207060, 0x00120001, 0x20209000, 0x00120001,   // 0x00564710
                0x20209020, 0x00120001, 0x20209040, 0x00120001,   // 0x00564720
                0x20209060, 0x00120001, 0x2020b040, 0x00120001,   // 0x00564730
                0x70800000, 0x00120001, 0x002c0068, 0x003c0088,   // 0x00564740
                0x004c0098, 0x006c0090, 0x006c0068, 0x006c0048,   // 0x00564750
                0x004c0040, 0x003c0050, 0x00540068, 0x002c0040,   // 0x00564760
                0x2c38b000, 0x00120000, 0x2430dc00, 0x00120000,   // 0x00564770
                0x2430dc30, 0x00120000, 0x0f0f80c0, 0x0d110000,   // 0x00564780
                0x0f0f80d0, 0x0d110000, 0x0818a860, 0x0a110000,   // 0x00564790
                0x0818b060, 0x0a110000, 0x0818b860, 0x0a110000,   // 0x005647a0
                0x0818c060, 0x0a110000, 0x0818c860, 0x0a110000,   // 0x005647b0
                0x08201880, 0x02300001, 0x082018a0, 0x02300001,   // 0x005647c0
                0x082818c0, 0x02300001, 0x081718e8, 0x02300001,   // 0x005647d0
                0x08222080, 0x02300001, 0x072c62c0, 0x02300001,   // 0x005647e0
                0x082820d0, 0x02300001, 0x08202980, 0x02300001,   // 0x005647f0
                0x081850d0, 0x02300001, 0x082428a0, 0x02300001,   // 0x00564800
                0x082828d0, 0x02300001, 0x08283880, 0x02300001,   // 0x00564810
                0x0728f9aa, 0x02300001, 0x083030c0, 0x02300001,   // 0x00564820
                0x082060a0, 0x02300001, 0x081838a8, 0x02300001,   // 0x00564830
                0x082038c0, 0x02300001, 0x081f38e0, 0x02300001,   // 0x00564840
                0x08204080, 0x02300001, 0x083040a0, 0x02300001,   // 0x00564850
                0x082f40d0, 0x02300001, 0x08204880, 0x02300001,   // 0x00564860
                0x083048a0, 0x02300001, 0x082f48d0, 0x02300001,   // 0x00564870
                0x08205080, 0x02300001, 0x083050a0, 0x02300001,   // 0x00564880
                0x082450d0, 0x02300001, 0x08305880, 0x02300001,   // 0x00564890
                0x082858b2, 0x02300001, 0x082758d8, 0x02300001,   // 0x005648a0
                0x083220a2, 0x02300001, 0x0808f800, 0x06110000,   // 0x005648b0
                0x0808f808, 0x04110000, 0x0808f810, 0x05110000,   // 0x005648c0
                0x0808f818, 0x07110000, 0x0808f820, 0x07110000,   // 0x005648d0
                0x0808f828, 0x09110000, 0x0808f830, 0x09110000,   // 0x005648e0
                0x0808f838, 0x0a110000, 0x0808f848, 0x04110000,   // 0x005648f0
            ];

            // 0x00566c18 .. 0x00566c40 — cinq descripteurs. La plage s'arrete la : 0x00566c40
            // vaut 0x007f007f, qui n'est pas un descripteur.
            ReadOnlySpan<uint> blocB =
            [
                0x40400000, 0x02300100,   // 0x00566c18
                0x08281800, 0x02300000,   // 0x00566c20
                0x40803000, 0x02300000,   // 0x00566c28
                0x08504800, 0x02300000,   // 0x00566c30
                0x0a640880, 0x02300100,   // 0x00566c38
            ];

            s_SpriteAtlasStorage = (byte*)NativeMemory.AllocZeroed((nuint)blocA.Length, sizeof(uint));
            for (int i = 0; i < blocA.Length; i++) ((uint*)s_SpriteAtlasStorage)[i] = blocA[i];

            s_AtlasBlockB = (byte*)NativeMemory.AllocZeroed((nuint)blocB.Length, sizeof(uint));
            for (int i = 0; i < blocB.Length; i++) ((uint*)s_AtlasBlockB)[i] = blocB[i];
        }

        if ((uint)(va - AtlasA_Base) < (uint)(AtlasA_End - AtlasA_Base))
            return s_SpriteAtlasStorage + (va - AtlasA_Base);

        if ((uint)(va - AtlasB_Base) < (uint)(AtlasB_End - AtlasB_Base))
            return s_AtlasBlockB + (va - AtlasB_Base);

        throw new NotImplementedException(
            $"Descripteur d'atlas 0x{va:X8} hors des deux plages relevees " +
            $"(0x{AtlasA_Base:X8}..0x{AtlasA_End:X8} et 0x{AtlasB_Base:X8}..0x{AtlasB_End:X8}). " +
            "Relever la plage — pas le descripteur seul — avec " +
            $"`OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{va:x8} 64`, et etendre les " +
            "bornes ci-dessus ainsi que DataSymbols.md.");
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00564900</c> — les neuf bits d'etat que la barre des membres teste contre
    /// <c>bloc + 0x140</c>, un pictogramme par bit. Releves dans le binaire ; la borne de la boucle
    /// de l'original (<c>&lt; 0x564924</c>) confirme les neuf entrees.
    /// </summary>
    private static readonly uint[] s_StatusBits_00564900 =
        [1, 2, 4, 8, 0x10, 0x20, 0x40, 0x100, 0x200];

    /// <summary>Ghidra: les trois dispositions en X des panneaux de membre, choisies par
    /// <c>DAT_005de160</c> — la position du menu dans son animation d'ouverture.</summary>
    private static readonly int[] s_PanelX_00564928 = [0x32, -50, 0x172, 0x10e];
    private static readonly int[] s_PanelX_00564938 = [0x5a, -20, 0x154, 0xe6];
    private static readonly int[] s_PanelX_00564948 = [0xa0, 0xa0, 0xa0, 0xa0];
    private static readonly int[] s_PanelX_00564958 = [0xb4, 0x8c, 0xb4, 0x8c];

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

    /// <summary>Le pas d'amortissement de la pose de camera : meme forme que
    /// <see cref="SmoothWrappedAngleStep"/> a la profondeur pres — un quart de l'ecart au lieu d'un
    /// huitieme.</summary>
    private static short SmoothCameraStep(ushort delta)
        => delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((0x1000 - (short)delta) >> 2);

    /// <summary>
    /// Ghidra: FUN_00532b40 @ 0x00532b40 — <b>le mouvement de scene scripte</b> : un flux
    /// d'enregistrements de 0x10 octets, lu a <c>DAT_005e9b3c</c>, dont chaque tour ajoute des
    /// deltas aux <b>consignes</b> que <see cref="SmoothSceneOrientationAndBackColour"/> poursuit.
    /// Le flux boucle sur lui-meme quand son premier octet vaut -1.
    ///
    /// <para>
    /// <b>Trois des quatre ecritures ont une forme inattendue, et les trois sont verifiees au
    /// desassembleur</b> (0x00532b57..0x00532bce) :
    /// </para>
    /// <list type="bullet">
    /// <item><c>0x005e9310</c> recoit <b>deux additions 16 bits independantes</b>, sans retenue de
    /// l'une vers l'autre — deux angles dans un dword.</item>
    /// <item><c>0x005e9314</c> est lu et ecrit en <b>dword</b> puis masque a 16 bits : la moitie
    /// haute (<c>0x005e9316</c>) est donc <b>remise a zero a chaque tour</b>. Idem pour
    /// <c>0x005e92bc</c>.</item>
    /// <item><c>0x005e92b8</c> voit ses deux moities <b>croisees</b> : la nouvelle moitie basse
    /// vaut l'ancienne moitie haute plus celle du flux, et la nouvelle moitie haute vaut l'ancienne
    /// moitie basse plus le mot suivant. Ghidra le rendait deja ainsi et on pouvait croire a un
    /// artefact ; <c>shr edi,10h ; add eax,edi ; shl edx,10h ; or eax,edx</c> dit que non.</item>
    /// </list>
    /// </summary>
    private static void AdvanceScriptedSceneMotion_00532B40()
    {
        // Ghidra: `if (DAT_005645fc != -1)`. C'est un OCTET (0x00563612 l'ecrit par CONCAT31), et
        // le depot le porte deja en `int` sous s_Unknown_005645fc — d'ou la comparaison a 0xff et
        // non a -1. **Ce symbole n'est PAS dans EntityArena** : le lire par Mem.Entity donnerait un
        // decalage negatif, ce qui a effectivement fait passer la garde a tort avant correction.
        if ((byte)s_Unknown_005645fc == 0xff) return;

        byte* stream = *(byte**)Mem.Arena(0x005e9b3c);

        uint deltaOrientation = *(uint*)(stream + 4);
        uint deltaRoll = *(uint*)(stream + 8);
        uint deltaColour = *(uint*)(stream + 0x0c);

        *(byte**)Mem.Arena(0x005e9b3c) = stream + 0x10;

        uint orientation = *(uint*)Mem.Arena(0x005e9310);
        *(uint*)Mem.Arena(0x005e9310) =
            ((deltaOrientation & 0xffff0000) + (orientation & 0xffff0000))
            | ((deltaOrientation + orientation) & 0xffff);

        *(uint*)Mem.Arena(0x005e9314) = (*(uint*)Mem.Arena(0x005e9314) + deltaRoll) & 0xffff;

        uint colour = *(uint*)Mem.Arena(0x005e92b8);
        *(uint*)Mem.Arena(0x005e92b8) =
            ((deltaRoll >> 0x10) + (colour >> 0x10)) | ((colour + deltaColour) << 0x10);

        *(uint*)Mem.Arena(0x005e92bc) = (deltaColour + *(uint*)Mem.Arena(0x005e92bc)) & 0xffff;

        if (**(sbyte**)Mem.Arena(0x005e9b3c) == -1)
        {
            *(byte**)Mem.Arena(0x005e9b3c) = **(byte***)Mem.Entity(0x005eca98);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005320f0 @ 0x005320f0 — <b>le defilement de texture</b> d'un morceau de decor :
    /// chaque face listee avance ses quatre coordonnees de texture d'une unite, et les quatre
    /// retombent ensemble de la periode des qu'elles l'ont toutes atteinte.
    ///
    /// <para>
    /// La periode est l'octet de <c>+0x3f</c> du morceau ; la liste des faces est terminee par
    /// <c>0xffff</c>. Les quatre coordonnees vivent dans deux dwords, un octet chacune sur les
    /// octets 0 et 2 — les octets 1 et 3 sont preserves par le masque <c>0xff00ff00</c>.
    /// </para>
    /// <para>
    /// <see cref="ScrollTextureTriangles_005321F0"/> est sa jumelle a deux differences pres : le
    /// pas d'enregistrement (0x10 au lieu de 0x20) et les deux decalages (+8/+0xc au lieu de
    /// +0x10/+0x14). Ecrites separement, comme les deux fonctions du binaire.
    /// </para>
    /// </summary>
    private static void ScrollTextureQuads_005320F0(byte* chunk)
        => ScrollTextureCoordinates(chunk, 0x20, 0x10);

    /// <summary>Ghidra: FUN_005321f0 @ 0x005321f0 — la jumelle de
    /// <see cref="ScrollTextureQuads_005320F0"/> pour les faces a trois sommets.</summary>
    private static void ScrollTextureTriangles_005321F0(byte* chunk)
        => ScrollTextureCoordinates(chunk, 0x10, 0x08);

    /// <summary>
    /// Le corps commun aux deux defilements. Factorise parce que les deux fonctions de l'original
    /// sont copie-collees a deux constantes pres — meme cas que
    /// <c>FreezeAllEntities</c>/<c>UnfreezeAllEntities</c>.
    /// </summary>
    private static void ScrollTextureCoordinates(byte* chunk, int stride, int offset)
    {
        if (chunk == null || *(short*)(chunk + 0x0e) == 0) return;

        ushort* faces = **(ushort***)(chunk + 0x38);
        if (faces == null) return;

        byte* records = *(byte**)(chunk + 0x28);
        ushort period = chunk[0x3f];

        for (ushort face = *faces; face != 0xffff; face = *(++faces))
        {
            byte* record = records + face * stride;
            uint first = *(uint*)(record + offset);
            uint second = *(uint*)(record + offset + 4);

            uint u0 = (first & 0xff) + 1;
            uint v0 = ((first >> 0x10) & 0xff) + 1;
            uint u1 = (second & 0xff) + 1;
            uint v1 = ((second >> 0x10) & 0xff) + 1;

            if (period <= (ushort)u0 && period <= (ushort)v0
                && period <= (ushort)u1 && period <= (ushort)v1)
            {
                u0 -= period;
                v0 -= period;
                u1 -= period;
                v1 -= period;
            }

            *(uint*)(record + offset) = ((v0 & 0xff) << 0x10) | (u0 & 0xff) | (first & 0xff00ff00);
            *(uint*)(record + offset + 4) = ((v1 & 0xff) << 0x10) | (u1 & 0xff) | (second & 0xff00ff00);
        }
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
    /// Ghidra: <c>&amp;DAT_0056d340</c> — <b>quarante-huit masques d'etat, un par minuteur</b>,
    /// releves dans le binaire le 2026-08-23. Le bloc de statistiques porte quarante-huit
    /// <c>short</c> a partir de <c>+0x80</c> ; quand le minuteur d'indice <i>i</i> retombe a zero,
    /// le bit que donne <c>masques[i]</c> est efface du mot d'etat <c>+0x40</c>.
    ///
    /// <para>La borne vient du code : la boucle avance de quatre en quatre <b>tant que l'adresse
    /// reste sous <c>0x0056d400</c></b>, soit 0x30 entrees. Onze d'entre elles valent zero — leur
    /// minuteur decompte sans rien lever a l'echeance, il n'agit que tant qu'il est non nul (c'est
    /// le cas des trois regenerations traitees a part dans la boucle).</para>
    /// </summary>
    private static readonly uint[] s_StatusTimerMasks_0056d340 =
    [
        0x08000000, 0x02000000, 0x04000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000004,
        0x00000008, 0x00000010, 0x00000020, 0x00000040,
        0x00000080, 0x00000100, 0x00000200, 0x00000400,
        0x00000800, 0x00001000, 0x00002000, 0x00004000,
        0x00008000, 0x00010000, 0x00020000, 0x00040000,
        0x00080000, 0x00100000, 0x00200000, 0x10000000,
        0x00400000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x01000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
    ];

    /// <summary>
    /// Ghidra: FUN_0047ac40 @ 0x0047ac40 — <b>fait naitre une entite dans la table
    /// <c>DAT_005f2d40</c></b> (0x80 emplacements), en recopiant les 0x27 dwords de la source puis
    /// en remplacant une dizaine de champs. Troisieme jumelle de <see cref="SpawnEffectEntity"/>
    /// (<c>FUN_0047a9d0</c>) et de <see cref="SpawnCastEntity_0047AAE0"/> : meme forme, autre table.
    ///
    /// <para>Elle prend six arguments la ou les deux autres en prennent quatre : la <b>position</b>
    /// est passee separement (elle ecrase <c>+0x1c</c>/<c>+0x20</c>/<c>+0x24</c> de la copie) et un
    /// <b>descripteur d'atlas</b> part dans <c>+0x3c</c>.</para>
    /// </summary>
    /// <param name="param6">Negatif veut dire « ne pas ecrire <c>+0x0a</c> » — la seule branche
    /// conditionnelle de la fonction.</param>
    /// <returns>1 si un emplacement etait libre, 0 sinon.</returns>
    private static uint SpawnStatusEffectEntity_0047AC40(
        byte* source, int* position, byte* descriptor, byte subtype, byte param5, int param6)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x005f7ce0));
        if (index < 0) return 0;

        byte* born = Mem.Entity(0x005f2d40) + index * 0x9c;
        Mem.CopyDwords((uint*)source, (uint*)born, 0x27);

        *(int*)(born + 0x1c) = position[0];
        *(int*)(born + 0x20) = position[1];
        *(int*)(born + 0x24) = position[2];

        born[0x08] = (byte)index;
        born[0x36] |= 0x80;
        born[0x05] = subtype;
        *(byte**)(born + 0x3c) = descriptor;
        born[0x0b] = param5;
        *(byte**)(born + 0x6c) = source;
        born[0x40] = 0;
        if (param6 >= 0) born[0x0a] = (byte)param6;
        *(ushort*)(born + 0x10) = 0;

        *(uint*)born = (*(uint*)born & 0x10e82f02) | 0x80101000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_0053aa10 @ 0x0053aa10 — <b>le pas d'etats d'une entite de scene</b>, appele sur
    /// chacune, une fois par image, juste apres son gestionnaire et la regeneration lente.
    ///
    /// <para>Malgre le nom que ce port lui avait donne d'avance, elle n'anime rien : elle fait
    /// vivre le <b>bloc de statistiques</b> que <see cref="GetEntityStatBlock"/> designe — poison,
    /// minuteurs d'etat, regenerations, et les deux seuils de zero.</para>
    ///
    /// <para><b>Trois cadences.</b> A chaque image : la teinte <c>+0x0b</c> de l'entite, prise dans
    /// la fiche de sous-zone pour une entite ordinaire et dans son propre enregistrement pour un
    /// membre du groupe, et forcee a <c>0xff</c> par le bit <c>0x100</c> (petrification). Toutes les
    /// <b>trente</b> images : les deux poisons et le drain, chacun retirant trois pour cent du
    /// maximum — <b>au moins un point</b> — et decomptant sa duree. Toutes les <b>vingt</b> images :
    /// les quarante-huit minuteurs d'etat, dont trois portent une regeneration au lieu d'un
    /// simple decompte.</para>
    ///
    /// <para>Un membre du groupe empoisonne repasse en plus par un declenchement d'objet equipe
    /// (<c>FUN_005238e0</c>) toutes les vingt images, precede de la remise a zero de
    /// <c>DAT_005e99c0</c>.</para>
    ///
    /// <para><b>Les deux seuils.</b> Points de vie a zero : le bit <c>0x40000000</c> part dans le
    /// bloc de statistiques <b>et</b> dans le mot d'etat de l'entite. Points de magie a zero, et
    /// seulement au passage : le bit <c>0x80</c>, un plancher de douze sur <c>+0x98</c>, une entite
    /// de sous-type <c>0x29</c> dans la table <c>DAT_005f2d40</c> et une copie dans la table des
    /// effets.</para>
    /// </summary>
    private static void AdvanceSceneEntityAnimation_0053aa10(byte* record)
    {
        uint* entity = (uint*)record;

        // La garde d'entree : ni mort (0x40000000) ni retire (0x10000000).
        if ((entity[0] & 0x50000000) != 0) return;

        byte* stats = GetEntityStatBlock(entity);

        if ((entity[0] & 0x800000) == 0)
        {
            // Entite ordinaire : la teinte vient de la fiche de sous-zone, huit octets par entree.
            record[0x0b] = (*(byte**)(s_Unknown_005e97cc + 0x4c))[record[0x0c] * 8 + 2];
        }
        else
        {
            // Membre du groupe : la teinte est dans son enregistrement de 0x2000 octets, et le
            // reveil d'un membre a terre y regagne un seizieme de son maximum.
            byte* character = GetMainMemPtr() + 0xf0000 + record[8] * 0x2000;
            record[0x0b] = character[0x2bd];

            if (character[0x206] == 0)
            {
                character[0x206] = 0x28;
                *(short*)(character + 0x14c) +=
                    (short)(*(short*)(character + 0x128) + 0x3f >> 6);
            }
        }

        // La petrification efface la teinte.
        if ((*(uint*)(stats + 0x40) & 0x100) != 0) record[0x0b] = 0xff;

        uint frame = *(uint*)Mem.Entity(0x005f2cf0);

        // ------------------------------------------------------------ toutes les trente images
        if (frame % 0x1e == 0)
        {
            if ((stats[0x40] & 1) != 0)
            {
                *(short*)(stats + 0x4c) -= ThreePercentAtLeastOne(*(short*)(stats + 0x48));
                *(short*)(stats + 0xe0) -= 1;
                if (*(short*)(stats + 0xe0) == 0) *(uint*)(stats + 0x40) &= 0xfffffffe;
            }

            if ((stats[0x40] & 2) != 0)
            {
                *(short*)(stats + 0x4e) -= ThreePercentAtLeastOne(*(short*)(stats + 0x4a));
                *(short*)(stats + 0xe2) -= 1;
                if (*(short*)(stats + 0xe2) == 0) *(uint*)(stats + 0x40) &= 0xfffffffd;
            }

            // La petrification draine cinq pour cent, sans duree : elle ne s'arrete pas d'elle-meme.
            if ((*(uint*)(stats + 0x40) & 0x100) != 0)
            {
                int drain = *(short*)(stats + 0x48) * 5 / 100;
                if (drain == 0) drain = 1;
                *(short*)(stats + 0x4c) -= (short)drain;
            }

            if ((*(uint*)(stats + 0x40) & 0x103) != 0)
            {
                Aud.PlayPositionalSound(5, 0xa00, 0x1800, (int*)(record + 0x1c));
                *(ushort*)(record + 0x96) = 8;
            }
        }

        // ------------------------------------------------------------ toutes les vingt images
        if (frame % 0x14 == 0)
        {
            short* timer = (short*)(stats + 0x80);

            for (int i = 0; i < s_StatusTimerMasks_0056d340.Length; i++, timer++)
            {
                if (*timer == 0) continue;

                *timer -= 1;

                if (*timer == 0)
                {
                    *(uint*)(stats + 0x40) &= ~s_StatusTimerMasks_0056d340[i];
                    if ((byte*)timer == stats + 0xc0) *(ushort*)(stats + 0xec) = 0;
                    continue;
                }

                // Trois minuteurs agissent **tant qu'ils tournent** : deux regenerations dont le
                // pas vit a +0xfc/+0xfe, et une troisieme a taux fixe.
                if ((byte*)timer == stats + 0xbc)
                {
                    *(short*)(stats + 0x4c) += *(short*)(stats + 0xfc);
                }
                if ((byte*)timer == stats + 0xbe)
                {
                    *(short*)(stats + 0x4e) += *(short*)(stats + 0xfe);
                }
                if ((byte*)timer == stats + 0xc2)
                {
                    *(short*)(stats + 0x4c) += 0x14;
                }
            }

            if ((entity[0] & 0x800000) != 0)
            {
                *(uint*)Mem.Arena(0x005e99c0) = 0;
                SetEntityAnimationState_005238E0(entity, 0);
            }
        }

        // ------------------------------------------------------------ les bornes et les deux seuils
        if (*(short*)(stats + 0x4c) > *(short*)(stats + 0x48))
        {
            *(ushort*)(stats + 0x4c) = *(ushort*)(stats + 0x48);
        }
        if (*(short*)(stats + 0x4e) > *(short*)(stats + 0x4a))
        {
            *(short*)(stats + 0x4e) = *(short*)(stats + 0x4a);
        }

        if (*(short*)(stats + 0x4c) < 1)
        {
            *(short*)(stats + 0x4c) = 0;
            *(uint*)(stats + 0x40) |= 0x40000000;
            entity[0] |= 0x40000000;
        }

        if (*(short*)(stats + 0x4e) < 1)
        {
            *(short*)(stats + 0x4e) = 0;

            // Le bit 0x80 fait office de verrou : tout ce bloc ne s'execute qu'au passage a zero.
            if ((stats[0x40] & 0x80) == 0)
            {
                *(uint*)(stats + 0x40) |= 0x80;
                if (*(ushort*)(stats + 0x98) < 0xc) *(ushort*)(stats + 0x98) = 0xc;

                SpawnStatusEffectEntity_0047AC40(
                    record, (int*)(record + 0x1c), SpriteAtlas(0x005648b0), 0x29, 0, 0);
                SpawnEffectEntity(record, 0x46, 0, 2);
            }
        }
    }

    /// <summary>Ghidra: le calcul de degat des deux poisons — <c>maximum * 3 / 100</c>, ramene a
    /// <b>un</b> quand la division rend zero. Ecrit deux fois a l'identique dans
    /// <c>FUN_0053aa10</c>.</summary>
    private static short ThreePercentAtLeastOne(short maximum)
    {
        int amount = maximum * 3 / 100;
        return amount == 0 ? (short)1 : (short)amount;
    }

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
            case 0x00450AA0: EntityHandler_00450AA0((uint*)record); return;
            case 0x00450D30: EntityHandler_00450D30((uint*)record); return;
            case 0x00486EE0: EntityHandler_00486EE0((uint*)record); return;
            case 0x00404EF0: EntityHandler_00404EF0((uint*)record); return;

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
    /// Ghidra: FUN_00404ef0 @ 0x00404ef0 — le gestionnaire <b>0x404EF0</b> de la table des effets
    /// <c>0x005ff400</c> : il ne touche pas a sa propre entite mais <b>balaie les 0x20
    /// enregistrements de la table <c>0x005ecaa0</c></b> et reveille ceux qui portent a la fois le
    /// bit <c>0x80000000</c> et le bit <c>8</c>, en aiguillant sur leur octet <c>+0x0c</c>.
    ///
    /// <para>Il se desarme lui-meme en sortant (bit <c>0x80000000</c> de son propre mot d'etat), ce
    /// qui en fait une impulsion : une passe, une seule fois.</para>
    /// </summary>
    private static void EntityHandler_00404EF0(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            uint flags = *(uint*)record;
            if ((flags & 0x80000000) == 0 || (flags & 8) == 0) continue;

            switch (record[0x0c])
            {
                case 0: WakeSceneEntityKind0_00404F70(i); break;
                case 1: WakeSceneEntityKind1_00405240(i); break;
                case 2: WakeSceneEntityKind2_00405510(i); break;
            }
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00404f70 @ 0x00404f70 (23 l.) — le reveil d'une entite de scene de
    /// type 0, aiguille a son tour sur les 30 bits bas de son mot <c>+0x5c</c>.</summary>
    private static void WakeSceneEntityKind0_00404F70(int index)
        => throw new NotImplementedException(
            $"FUN_00404f70 (0x00404f70), entree {index} de la table 0x005ecaa0 : non transcrite. " +
            "23 lignes dans Blaze.c, plus FUN_004051d0, FUN_00404fe0 et FUN_004050d0. Elle " +
            "aiguille sur les 30 bits bas de +0x5c (valeurs 1, 2 et 3).");

    /// <summary>Ghidra: FUN_00405240 @ 0x00405240 (31 l.) — le reveil d'une entite de scene de
    /// type 1, cinq branches sur les 30 bits bas de <c>+0x5c</c>.</summary>
    private static void WakeSceneEntityKind1_00405240(int index)
        => throw new NotImplementedException(
            $"FUN_00405240 (0x00405240), entree {index} de la table 0x005ecaa0 : non transcrite. " +
            "31 lignes dans Blaze.c, plus FUN_004051d0, FUN_00404fe0, FUN_004052f0 et " +
            "FUN_00405420. Elle aiguille sur les 30 bits bas de +0x5c (valeurs 1 a 5).");

    /// <summary>Ghidra: FUN_00405510 @ 0x00405510 (31 l.) — le reveil d'une entite de scene de
    /// type 2, meme forme que sa voisine.</summary>
    private static void WakeSceneEntityKind2_00405510(int index)
        => throw new NotImplementedException(
            $"FUN_00405510 (0x00405510), entree {index} de la table 0x005ecaa0 : non transcrite. " +
            "31 lignes dans Blaze.c. Meme forme que FUN_00405240 : un aiguillage sur les 30 bits " +
            "bas de +0x5c.");

    /// <summary>
    /// Ghidra: FUN_004db5a0 @ 0x004db5a0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (112 octets, 0x004db5a0..0x004db610). <b>L'avance d'une image d'animation</b>,
    /// partagee par les gestionnaires d'entite.
    ///
    /// <para>Le compteur d'images est <c>+0x1a</c>, la limite le champ <c>+0x16</c> de l'entree
    /// d'animation que designe <c>+0x18</c> dans la table du sous-type. Le bit <c>0x1000000</c> des
    /// drapeaux choisit le regime : pose, l'animation <b>boucle</b> (retour a zero) ; degage, elle
    /// <b>s'arrete</b> sur sa derniere image et pose le bit <c>0x2000000</c> pour l'annoncer. Le bit
    /// <c>0x2000000</c> est efface a l'entree dans les deux cas.</para>
    /// </summary>
    private static void AdvanceEntityAnimation_004DB5A0(uint* entity)
    {
        byte* e = (byte*)entity;

        uint flags = entity[0] & 0xfdffffff;
        entity[0] = flags;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + e[4] * 4))
                              [*(short*)(e + 0x18)];

        *(short*)(e + 0x1a) += 1;

        if ((flags & 0x1000000) != 0)
        {
            if (*(short*)(e + 0x1a) >= *(ushort*)(animation + 0x16))
            {
                *(short*)(e + 0x1a) = 0;
            }
        }
        else if (*(short*)(e + 0x1a) >= *(ushort*)(animation + 0x16))
        {
            *(short*)(e + 0x1a) -= 1;
            entity[0] = flags | 0x2000000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00486ee0 @ 0x00486ee0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (881 octets, 0x00486ee0..0x00487250, releve dans
    /// <c>Legacy/Raw/Blaze/FUN_00486EE0.asm</c>). C'est le sous-type <b>0</b> de la table
    /// <c>0x005ecaa0</c> — la <b>bestiole errante</b> : elle regarde qui du groupe la voit, et sinon
    /// se promene au hasard.
    ///
    /// <para><b>Premiere moitie — la detection.</b> Pour chacun des membres vivants, elle mesure la
    /// distance horizontale (racine de la somme des carres, ordinal 442), l'ecart vertical et
    /// l'angle relatif (ordinal 457) ; le membre « voit » la bestiole si la distance est dans
    /// <c>[0x3c, 0x78]</c>, l'ecart vertical dans <c>[-0x64, +0x64]</c> et l'angle dans un cone de
    /// <c>0x400</c> unites de part et d'autre. Le resultat est un <b>bit par membre</b> dans
    /// <c>entite[9]</c>, dont le rang est le type d'emplacement du membre. Quand le masque passe de
    /// vide a non vide, le script d'entite <c>+0x16</c> demarre.</para>
    ///
    /// <para><b>Seconde moitie — la promenade.</b> <c>entite[0x18]</c> alterne entre 0 (arret) et 1
    /// (marche) ; en marche, la position avance de cinq fois le sinus et le cosinus du cap
    /// <c>+0x32</c>. Le minuteur <c>+0x40</c> decompte, et a zero la bestiole change d'etat : elle
    /// <b>essaie</b> un cap et une vitesse tires au hasard sur une <b>copie</b> de son
    /// enregistrement (0x005e9a40), la fait avancer, et ne l'adopte que si
    /// <see cref="FinalizeEntityPlacement2"/> ne signale pas les bits <c>0x882</c> — un mur, un
    /// vide, ou le bord du maillage.</para>
    /// </summary>
    private static void EntityHandler_00486EE0(uint* entity)
    {
        byte* e = (byte*)entity;

        uint flags = entity[0];
        if ((flags & 0x4000) != 0) return;

        if ((flags & 0x8000000) == 0) AdvanceEntityAnimation_004DB5A0(entity);

        // -------------------------------------------------------------- la detection par membre
        int detectionBefore = e[9];

        for (int slot = 0; slot < s_PartySize_005e9020; slot++)
        {
            byte* member = Inp.s_SlotFlags_005e93c0 + slot * 0x9c;
            byte bit = (byte)(1 << member[8]);

            uint memberFlags = *(uint*)member;
            bool eligible = (memberFlags & 0x80000000) != 0
                         && (memberFlags & 0x50010007) == 0
                         && *(short*)(member + 0x12) == 0;

            if (!eligible)
            {
                e[9] &= (byte)~bit;
                continue;
            }

            int dx = (*(int*)(e + 0x1c) - *(int*)(member + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(member + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(member + 0x24)) >> 0xc;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            int bearing = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(member + 0x32)) & 0xfff;

            if (distance < 0x3c || distance > 0x78
                || (uint)(dy + 0x64) > 0xc8
                || ((bearing + 0x200) & 0xfff) > 0x400)
            {
                e[9] &= (byte)~bit;
                continue;
            }

            // Le meme angle, pris dans l'autre sens : du point de vue de la bestiole vers le
            // membre. Quand elle est deja alertee (+0x43), elle en profite pour se tourner.
            int backX = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int backZ = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int facing = (Winsys3d.Ordinal_457(backX, backZ) - *(short*)(e + 0x32)) & 0xfff;

            if (e[0x43] != 0)
            {
                *(short*)(e + 0x32) = (short)Winsys3d.Ordinal_457(backX, backZ);
            }

            if (((facing + 0x200) & 0xfff) > 0x400 && e[0x43] == 0) continue;

            e[9] |= bit;
            *(byte**)(e + 0x6c) = member;
        }

        // Le masque vient de passer de vide a non vide : le script de rencontre demarre.
        if (detectionBefore == 0 && e[9] != 0)
        {
            RunEntityScript(s_Unknown_005e97cc, *(byte**)(e + 0x6c), (uint)*(short*)(e + 0x16));
        }

        if (*(short*)(e + 0x10) == 0 && *(short*)(e + 0x12) == 0 && *(short*)(e + 0x14) != 0)
        {
            *(short*)(e + 0x14) = 0;
            e[0x43] = 1;
        }

        // -------------------------------------------------------------- la promenade
        if (e[0x43] != 0)
        {
            if (*(short*)(e + 0x18) > 1)
            {
                // Un dword : l'original efface l'etat **et** le compteur d'images d'un coup.
                *(int*)(e + 0x18) = 0;
                e[0x40] = (byte)((CrtRand() & 3) + 3);
            }
        }
        else
        {
            if (*(short*)(e + 0x18) != 0) *(int*)(e + 0x18) = 0;
            return;
        }


        short walkState = *(short*)(e + 0x18);
        if (walkState == 1)
        {
            // En marche : cinq unites par image dans la direction du cap.
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 5;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 5;
            e[0x40]--;
        }
        else if (walkState == 0 && *(short*)(e + 0x1a) == 0)
        {
            e[0x40]--;
        }

        FinalizeEntityPlacement2(entity);
        *(int*)(e + 0x20) = *(int*)(e + 0x64);

        if ((*(uint*)(e + 0x90) & 0x800) != 0) e[0x40] = 0;

        byte timer = e[0x40];
        if (timer != 0 && timer != 0xff) return;

        // Le minuteur est echu : on bascule marche/arret.
        *(short*)(e + 0x18) ^= 1;
        short newState = *(short*)(e + 0x18);
        *(short*)(e + 0x1a) = 0;

        if (newState == 0)
        {
            // On s'arrete : trois a six images d'immobilite.
            e[0x40] = (byte)((CrtRand() & 3) + 3);
            return;
        }

        // On repart : le cap et la vitesse sont tires au hasard, mais **essayes sur une copie**
        // de l'enregistrement avant d'etre adoptes.
        byte* trial = Mem.Arena(0x005e9a40);
        Mem.CopyDwords(entity, (uint*)trial, 0x27);

        *(short*)(trial + 0x32) = (short)CrtRand();
        trial[0x40] = (byte)((CrtRand() & 0x1f) + 0x28);

        int trialHeading = *(short*)(trial + 0x32);
        int speed = trial[0x40];

        *(int*)(trial + 0x1c) += (int)Winsys3d.GetSinTableEntry(trialHeading) * speed * 5;
        *(int*)(trial + 0x24) += Winsys3d.GetCosTableEntry(trialHeading) * speed * 5;

        FinalizeEntityPlacement2((uint*)trial);

        if ((*(uint*)(trial + 0x90) & 0x882) != 0)
        {
            // Refuse : mur, vide ou bord du maillage. On reste immobile une image.
            *(int*)(e + 0x18) = 0;
            e[0x40] = 1;
            return;
        }

        *(short*)(e + 0x32) = *(short*)(trial + 0x32);
        e[0x40] = trial[0x40];
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
    /// Ghidra: <c>DAT_00571540</c> — <b>le registre des types d'effet de zone actifs</b>, cinq bits
    /// dans l'octet bas d'un dword de l'arene d'etat d'entite. <c>Init_004502d0</c>
    /// (<c>Engine/Resources.cs</c>) le remet a zero a chaque chargement de zone.
    ///
    /// <para>
    /// Trois gestionnaires s'en servent et le cycle est maintenant complet :
    /// <see cref="EntityHandler_004508D0"/> et <see cref="EntityHandler_004509B0"/> <b>allument</b>
    /// le bit de leur type au premier tour, <see cref="EntityHandler_00450AA0"/> l'<b>eteint</b>
    /// quand son effet s'acheve — et se sert du meme bit, tant qu'il est allume, comme feu vert
    /// pour avancer.
    /// </para>
    /// <para>
    /// Le binaire n'ecrit jamais ce registre autrement que par les cinq branches d'une table de
    /// sauts dont les cibles ne different que par la constante (1, 2, 4, 8, 0x10) : c'est
    /// <c>1 &lt;&lt; (type - 1)</c>, ecrit ainsi plutot qu'en cinq branches identiques.
    /// </para>
    /// </summary>
    private static byte* AreaEffectKindBits => Mem.EntityStateBytes(0x00571540);

    /// <summary>
    /// Ghidra: FUN_00450AA0 @ 0x00450aa0 — l'entree <b>10</b> de la table 0x0056adc8, <b>absente de
    /// <c>Blaze.c</c></b> et transcrite depuis le desassemblage (0x00450aa0..0x00450bb1, plus la
    /// table de sauts de 0x00450bb4).
    ///
    /// <para>
    /// Une machine a quatre phases dans le <c>short</c> de <c>+0x10</c>, qui fait <b>monter puis
    /// tenir</b> un niveau range a <c>+0x28</c> :
    /// </para>
    /// <list type="number">
    /// <item><b>0</b> — amorcage : le niveau part de zero et la vitesse de montee est recopiee de
    /// <c>+0x16</c> dans <c>+0x12</c> ;</item>
    /// <item><b>1</b> — montee : le niveau gagne la vitesse a chaque image, jusqu'a saturer a
    /// <c>0x1000</c> ; <c>+0x12</c> est alors remis a zero et sert de compteur ;</item>
    /// <item><b>2</b> — maintien pendant 30 images ;</item>
    /// <item><b>3</b> — fini : l'entite pose <c>0x40000000</c> et <b>rend son bit de type</b>.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Les cinq gardes du binaire se resument a une seule ligne.</b> Ghidra n'a pas decompile ce
    /// corps, mais le desassemblage montre cinq blocs <c>cmp cx,N ; jne ; test dl,M ; je</c> — un
    /// par type — ou <c>M</c> vaut successivement 1, 2, 4, 8, 0x10. Le deuxieme s'ecrit
    /// <c>test cl,dl</c> et non <c>test dl,2</c> : le compilateur a reutilise <c>CL</c>, qui porte
    /// deja le 2 de la comparaison. C'est le meme test que les quatre autres, pas un cas
    /// particulier. Tout se ramene donc a <c>bits &amp; (1 &lt;&lt; (type - 1))</c> — et un type hors de
    /// 1..5 ne rencontre <b>aucune</b> garde et passe.
    /// </para>
    /// <para>
    /// La garde est <b>avant</b> l'avance de la machine : tant que le bit de type est eteint,
    /// l'effet est fige plutot qu'annule.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450AA0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = speed;
        }

        short kind = *(short*)(e + 0x14);
        byte bits = *AreaEffectKindBits;

        if (kind >= 1 && kind <= 5 && (bits & (1 << (kind - 1))) == 0) return;

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += *(short*)(e + 0x12);
            if (*(short*)(e + 0x28) >= 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 0x1e) return;

        *(short*)(e + 0x10) = 3;
        entity[0] |= 0x40000000;

        // La table de sauts de 0x00450bb4 : cinq cibles qui ne different que par le
        // `and al,0FEh` / `0FDh` / `0FBh` / `0F7h` / `0EFh` final.
        int index = kind - 1;
        if ((uint)index <= 4)
        {
            *AreaEffectKindBits &= (byte)~(1 << index);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00450D30 @ 0x00450d30 — l'entree <b>18</b> de la table 0x0056adc8, <b>absente de
    /// <c>Blaze.c</c></b> et transcrite depuis le desassemblage (1 087 octets,
    /// 0x00450d30..0x0045116e). C'est <b>le coffre</b> : un objet du decor qui s'ouvre quand un
    /// membre du groupe le touche, verifie une clef, remet un objet, puis se referme.
    ///
    /// <para>
    /// Une machine a etats dans le <c>short</c> de <c>+0x10</c>, dont les valeurs ne sont pas des
    /// numeros consecutifs mais des jalons espaces — <c>0</c> (amorcage), <c>0xFFFF</c> et
    /// <c>0xFFFE</c> (les deux repos, selon le sens d'ouverture), <c>1</c> et <c>2</c>
    /// (l'ouverture), <c>0x14</c>, <c>0x28</c>, <c>0x64</c>, <c>0xC8</c>, <c>0xC9</c>, <c>0xCA</c>
    /// (la fermeture), <c>0x12C</c> (refus) et <c>0x3E7</c> (echec). Chaque etat est un <c>if</c>
    /// separe qui <b>tombe dans le suivant</b> : plusieurs transitions s'enchainent dans la meme
    /// image, ce qui est voulu et transcrit tel quel plutot que rendu en <c>switch</c>.
    /// </para>
    /// <para>
    /// <b>Le piege du corps est <c>EDI</c>.</b> Il vaut zero a l'entree et sert de « zero » a
    /// plusieurs comparaisons, mais <c>0x00451097</c> le recharge a <c>0xC8</c> — et cette
    /// instruction est sur <b>toutes</b> les routes qui menent a l'etat <c>0x12C</c>. Le
    /// <c>mov [esi+10h],di</c> de <c>0x0045115d</c> pose donc <c>0xC8</c>, pas zero : apres le
    /// message de refus, le coffre repart par la sequence de fermeture. Une lecture naive du
    /// registre aurait mis zero et fait boucler l'objet sur son amorcage.
    /// </para>
    /// <para>
    /// Meme famille de piege pour <c>EBX</c>, qui vaut 1 en tete et <c>0x28</c> a partir de
    /// <c>0x00450e79</c> : les <c>mov [esi+10h],bx</c> des deux cotes ne posent pas la meme valeur.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998 est conserve</b> : dans l'etat <c>0x28</c>, le pointeur d'entite liee
    /// de <c>+0x6c</c> est remplace par <c>s_EntityList_005de558</c> quand il est nul — mais
    /// seulement dans <c>EAX</c>. Le <c>mov dword [ebx+6Ch],0</c> qui suit ecrit toujours a travers
    /// <c>EBX</c>, resté nul. Voir <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450D30(uint* entity)
    {
        byte* e = (byte*)entity;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;

        if (*(short*)(e + 0x10) == unchecked((short)0xFF01)) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) == unchecked((short)0xFFFE)) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // L'amorcage lit l'angle d'ouverture de +0x30 : son SIGNE choisit lequel des deux repos
            // sera le sien, et la valeur est rangee en positif dans +0x9a.
            short opening = *(short*)(e + 0x30);
            *(short*)(e + 0x10) = unchecked((short)0xFFFF);
            *(short*)(e + 0x9a) = opening;
            *(short*)(e + 0x98) = 0;

            if (opening < 0)
            {
                *(short*)(e + 0x10) = unchecked((short)0xFFFE);
                *(short*)(e + 0x9a) = (short)-opening;
                *(short*)(e + 0x98) = 1;
            }

            int packed = *(short*)(e + 0x34);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(int*)(e + 0x44) = packed & 0xff;
            *(int*)(e + 0x48) = packed >> 8;
        }

        if (*(short*)(e + 0x10) == unchecked((short)0xFFFF) && (entity[0] & 0x40) != 0)
        {
            // Le declenchement : un membre du groupe est en contact. L'entite liee de +0x6c
            // retombe sur s_EntityList_005de558 quand elle est nulle.
            uint* toucher = *(uint**)(e + 0x6c);
            if (toucher == null) toucher = (uint*)Res.s_EntityList_005de558;

            if ((*toucher & 0x10000) == 0)
            {
                entity[0] |= 0x2000;
                *(short*)(e + 0x10) = 1;
            }
        }

        short state = *(short*)(e + 0x10);
        if (state != unchecked((short)0xFF01) && state != unchecked((short)0xFFFE) && state != 0
            && state != unchecked((short)0xFFFF) && state != 0xCA)
        {
            FreezeAllEntities();
            entity[0] &= 0xFBFFFFFF;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x5c) = CrtRand() % 0x12C + 0x1F4;
            FreezeAllEntities();

            // Le son d'ouverture : meme double indirection que EntityHandler_004509B0, a +8 pres.
            byte soundId = (*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8)))[8];
            TryPlaySoundNearEntity(entity, soundId, 0x146, 0x3000, 0, 0);

            entity[0] &= 0xFBFFFFFF;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // La distance de vue glisse d'un quart vers la valeur tiree en entrant dans l'etat 1,
            // et le couvercle tourne de 35 par image.
            int target = *(int*)(e + 0x5c);
            int current = *Mem.EntityState(0x00571528);
            *Mem.EntityState(0x00571528) = current + ((target - current) >> 2);

            *(short*)(e + 0x34) += unchecked((short)0xFFDD);
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 0x1F)
            {
                // Le coffre a-t-il deja ete ouvert ? Le drapeau vit dans la banque 0, un bit par
                // identifiant, et l'identifiant est celui range en +0x9a a l'amorcage.
                int flagBit = *(short*)(e + 0x9a);
                if ((Mem.s_StateBank0_005e96e0[flagBit >> 3] & (1 << (flagBit & 7))) != 0)
                {
                    *(short*)(e + 0x10) = 0x12C;
                    *(short*)(e + 0x12) = 0;
                    return;
                }

                int contents = *(int*)(e + 0x48);
                short rest;

                if (*(short*)(e + 0x14) == -1)
                {
                    if (contents == 0)
                    {
                        // Vide et sans etiquette : on repart directement sur la fermeture.
                        *(short*)(e + 0x10) = 0x28;
                        *(short*)(e + 0x12) = 0;
                        goto tail;
                    }

                    rest = *(short*)(e + 0x98);
                    *(short*)(e + 0x98) = 0x28;
                }
                else
                {
                    if (contents == 0)
                    {
                        *(short*)(e + 0x10) = 0x14;
                        *(short*)(e + 0x12) = 0;
                        goto tail;
                    }

                    rest = *(short*)(e + 0x98);
                    *(short*)(e + 0x98) = 0x14;
                }

                // L'objet sort du coffre : une entite d'effet, dont le type est le contenu decale
                // dans la plage 0xC0.
                SpawnEffectEntity(e, (byte)(contents + 0xC0), 0, -1);
                *(short*)(e + 0x98) = rest;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x3E7;
            }
        }

    tail:
        state = *(short*)(e + 0x10);

        if (state == 0x14)
        {
            // L'etiquette : le bit de la banque 3 est pose, puis la boite de dialogue est liee.
            int label = *(short*)(e + 0x14);
            Mem.s_StateBank3_005e9840[label >> 3] |= (byte)(1 << (label & 7));

            int entry = *(short*)(e + 0x16);
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + entry * 4);
            BindMenuLabels(labels, *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);

            *(short*)(e + 0x10) = 0x64;
            return;
        }

        if (state == 0x28)
        {
            uint* linked = *(uint**)(e + 0x6c);
            uint* holder = linked;
            if (holder == null) holder = (uint*)Res.s_EntityList_005de558;

            // L'inventaire du porteur : dix rangees de 0x280 octets dans son bloc de 0x2000.
            byte* inventory = GetMainMemPtr() + ((*((byte*)holder + 8) + 0x78) << 0xd);

            for (int row = 0; row <= 9; row++)
            {
                byte* slot = inventory + row * 0x280;
                if (*(short*)(slot + 0x386) == 0x2F1F
                    && slot[0x300] != 0
                    && *(ushort*)(slot + 0x310) >= 2)
                {
                    *(short*)(e + 0x10) = 0x12C;
                    return;
                }
            }

            // Ghidra: `mov dword [ebx+6Ch],0` — EBX, pas EAX. Le remplacement par
            // s_EntityList_005de558 ci-dessus n'a touche qu'EAX, donc un +0x6c nul se dereference
            // ici. Defaut de 1998, conserve.
            *(uint*)((byte*)linked + 0x6c) = 0;

            uint* subject = *(uint**)(e + 0x6c);
            *((byte*)subject + 0x41) = e[8];

            byte* dropTable = Mem.Arena(0x005e9860);
            byte already = dropTable[*(int*)(e + 0x44)];

            if (already == 0)
            {
                int amount = PickWeightedTableEntry(*(short*)(e + 0x16), 0x64);
                ushort granted = GrantChestContents_00530F20(*(uint**)(e + 0x6c), amount, 0x00450700);

                if (granted == 0)
                {
                    *(short*)(e + 0x10) = 0x3E7;
                    return;
                }

                dropTable[*(int*)(e + 0x44)] = (byte)granted;
                *(short*)(e + 0x10) = 0xC8;
                return;
            }

            ushort again = GrantChestContents_00530F20(*(uint**)(e + 0x6c), already - 1, 0x00450700);

            // Ghidra: `neg ax ; sbb eax,eax ; and eax,0FFFFFCE1h ; add eax,3E7h` — la forme sans
            // branchement de « non nul ? 0xC8 : 0x3E7 ».
            *(short*)(e + 0x10) = (short)(again != 0 ? 0xC8 : 0x3E7);
            return;
        }

        // A partir d'ici, EDI vaut 0xC8 dans le binaire — voir le resume de la methode.
        if (state == 0x64)
        {
            int flagBit = *(short*)(e + 0x9a);
            Mem.s_StateBank0_005e96e0[flagBit >> 3] |= (byte)(1 << (flagBit & 7));
            *(short*)(e + 0x10) = 0xC8;
        }

        if (*(short*)(e + 0x10) == 0xC8)
        {
            FreezeAllEntities();
            entity[0] &= 0xFBFFFFFF;
            *(short*)(e + 0x10) = 0xC9;
        }

        if (*(short*)(e + 0x10) == 0xC9)
        {
            // La refermeture : le couvercle revient de 0x46 par image jusqu'a repasser par zero.
            *(short*)(e + 0x34) += 0x46;
            if (*(short*)(e + 0x34) >= 0)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 0xCA;
                UnfreezeAllEntities();
                entity[0] &= 0xFFFFDFFF;      // Ghidra: `and ah,0DFh`
                return;
            }
        }

        if (*(short*)(e + 0x10) == 0xCA && (entity[0] & 0x40) == 0)
        {
            // Retour au repos, du cote choisi a l'amorcage.
            *(short*)(e + 0x10) = *(short*)(e + 0x98) != 0
                ? unchecked((short)0xFFFE)
                : unchecked((short)0xFFFF);
        }

        if (*(short*)(e + 0x10) == 0x12C)
        {
            // Le refus : une etiquette fixe (+0x118) prise dans l'AUTRE repertoire, DAT_005e97c8.
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x118);
            BindMenuLabels(labels, *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);

            // `mov [esi+10h],di`, et EDI vaut 0xC8 ici — pas zero.
            *(short*)(e + 0x10) = 0xC8;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0053a920 @ 0x0053a920 — <b>un tirage pondere</b> dans une table de huit octets par
    /// ligne, prise a <c>ctx + 0xb4</c>.
    ///
    /// <para>
    /// Le tirage se fait en comptant combien de seuils <c>rand() % bound</c> depasse — 0x18, 0x29,
    /// 0x38, 0x45, 0x50, 0x59, 0x5f — ce qui donne une colonne de 0 a 7 dont la probabilite
    /// decroit : sept comparaisons independantes plutot qu'une table cumulative. La valeur rendue
    /// est l'octet <b>signe</b> de cette colonne.
    /// </para>
    /// <para>
    /// Ghidra lui donne deux parametres, et c'est exact — l'arite avait deja ete verifiee au niveau
    /// machine le 2026-08-21 (voir <c>TODO.md</c>, etape 2).
    /// </para>
    /// </summary>
    private static int PickWeightedTableEntry(int row, int bound)
    {
        byte* table = *(byte**)(s_Unknown_005e97cc + 0xb4);
        int roll = CrtRand() % bound;

        uint column = roll > 0x18 ? 1u : 0u;
        if (roll > 0x29) column++;
        if (roll > 0x38) column++;
        if (roll > 0x45) column++;
        if (roll > 0x50) column++;
        if (roll > 0x59) column++;
        if (roll > 0x5f) column++;

        return *(sbyte*)(table + column + row * 8);
    }

    /// <summary>
    /// Ghidra: FUN_00530f20 @ 0x00530f20 — la remise d'un objet au groupe, avec la boite de
    /// dialogue qui l'annonce. Atteinte uniquement quand un coffre est reellement ouvert.
    /// </summary>
    private static ushort GrantChestContents_00530F20(uint* recipient, int amount, uint callback)
        => throw new NotImplementedException(
            $"FUN_00530f20 (0x00530f20) n'est pas transcrite : la remise d'un objet au groupe, " +
            $"appelee ici avec (entite, {amount}, 0x{callback:X6}). C'est la cascade d'affichage " +
            "des nombres et messages que TODO.md a mise de cote sciemment a l'etape (2) de " +
            "l'interpreteur de script (opcode 27) : elle tire FUN_005312d0 (109 l., criblee " +
            "d'artefacts de pile), FUN_00544350, FUN_0051bcc0, FUN_00530ed0, FUN_0053db20 et " +
            "FUN_00532710. Son corps est dans Blaze.c (156 l.), et son arite de TROIS est deja " +
            "confirmee au desassembleur. Le troisieme argument est une adresse de code (0x00450700 " +
            "depuis le coffre), donc vraisemblablement un rappel de fin.");

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

    // =====================================================================
    // FUN_0050bf20 — LE PAS PAR PERSONNAGE
    //
    // 1 002 lignes de pseudo-C, 8 Ko de code, appelees une fois par membre du groupe et par image.
    // C'est le plus gros corps que le depot ait transcrit, et **la pseudo-C seule n'y suffit pas** :
    // Ghidra y perd trois registres et tout un bloc de calcul. Les quatre points ci-dessous ont ete
    // tranches au desassembleur avant d'ecrire une ligne, et ils valent pour toute fonction de
    // cette taille.
    //
    // **`iStack_4`, jamais affecte dans la pseudo-C, est `[esp+68h]`** — et `[esp+68h]` est
    // l'emplacement du PARAMETRE, que le prologue ecrase (`mov [esp+68h],eax` en 0x0050bf52) avec
    // `&DAT_005e9180 + membre * 0x4c`, l'entree d'emplacement. Confirme en 0x0050ce71 :
    // `mov edi,[esp+68h] ; movsx ebx,word [edi+44h]` est exactement le `*(short *)(iStack_4 + 0x44)`
    // de la pseudo-C. C'est donc le `puVar11` que Ghidra nomme correctement ailleurs dans la meme
    // fonction : **le meme objet sous deux noms**, l'un des deux perdu.
    //
    // **`unaff_EBP` est le bloc par personnage** (`MainMem + 0xf0000 + membre * 0x2000`), pose en
    // 0x0050bf61 et sauve en `[esp+10h]`. C'est le `iVar23` de la pseudo-C ; Ghidra le perd parce
    // que 0x0050cee9 (`mov ebp,[esi+78h]`) reutilise le registre en cours de route.
    //
    // **`unaff_EBX` est l'etat de manette** (`&DAT_00604a20 + slotKind * 0x10`), pose en 0x0050bf63
    // et sauve en `[esp+14h]`. C'est le `puVar21`.
    //
    // **Le bloc de matrices de 0x0050cfb1..0x0050d290** est rendu par Ghidra comme une cascade de
    // `__allmul`/`__allshr` dont les resultats sont perdus, ce qui le rend illisible. Au
    // desassembleur c'est deux produits matrice-vecteur parfaitement reguliers, decalage `>> 12`
    // (le `mov ecx,0Ch` de 0x0050d02f et de ses cinq jumeaux). Voir
    // <see cref="ComputeCarrierOffset"/>.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00562cc0</c> — huit entiers releves dans le binaire
    /// (<c>at Legacy/Binaries/Blaze.exe 0x00562cc0 32</c>), indexes par les trois bits bas de
    /// <c>bloc + 0x150</c>, la posture. C'est la <b>vitesse de deplacement</b> de cette posture.
    ///
    /// <para>
    /// <b>Le role n'a ete etabli qu'avec le deuxieme lecteur.</b> Vu de
    /// <see cref="TickFollowingMember"/> seul, la table ressemblait a une portee de detection ; vu
    /// de <see cref="HandleDirectionalInput_0050E140"/>, qui la decale de 12 bits pour en faire
    /// directement la vitesse du personnage, c'est une vitesse. Les deux se recoupent : le membre
    /// suivi s'arrete a une distance egale a un pas de son meneur, quelle que soit la posture.
    /// </para>
    /// </summary>
    private static readonly int[] s_PostureSpeed_00562cc0 =
        [0x13, 0x13, 0x15, 0x12, 0x14, 0x14, 0x12, 0x15];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00562dc8</c> — seize octets releves dans le binaire, juste apres la table
    /// de pas sonores. Indexes par le <b>quartet haut</b> de <c>manette[0]</c>, c'est-a-dire la
    /// croix directionnelle : ils rendent un <b>secteur sur huit</b>, ou zero quand la direction
    /// est nulle ou contradictoire.
    ///
    /// <para>
    /// Le contenu se lit tout seul une fois qu'on remarque que les quatre bits sont haut, droite,
    /// bas, gauche dans cet ordre : <c>1→1</c>, <c>3→2</c>, <c>2→3</c>, <c>6→4</c>, <c>4→5</c>,
    /// <c>12→6</c>, <c>8→7</c>, <c>9→8</c> — les huit secteurs dans le sens horaire, diagonales
    /// comprises. Les deux paires opposees (<c>5</c> = haut+bas, <c>10</c> = gauche+droite) et
    /// toutes les combinaisons de trois touches rendent zero : elles s'annulent, et c'est ce zero
    /// qui fait retomber le personnage a l'arret.
    /// </para>
    /// </summary>
    private static readonly sbyte[] s_PadDirection_00562dc8 =
        [0, 1, 3, 2, 5, 0, 4, 0, 7, 8, 0, 0, 6, 0, 0, 0];

    /// <summary>
    /// Ghidra: <c>&amp;PTR_FUN_00562ca0</c> — huit gestionnaires de posture, releves dans le binaire.
    /// <b>Quatre d'entre eux sont <c>0x0040A600</c></b>, le corps vide deja verifie au
    /// desassembleur, donc seules les postures 0, 4, 6 et 7 font quelque chose.
    /// </summary>
    private static readonly uint[] s_PostureHandlers_00562ca0 =
        [0x0051A670, 0x0040A600, 0x0040A600, 0x0040A600, 0x0051A6B0, 0x0040A600, 0x0051A700, 0x0051A710];

    /// <summary>
    /// Ghidra: <c>&amp;PTR_FUN_00562ce0</c> — quatre gestionnaires de commande, releves dans le
    /// binaire. Aucun n'est le corps vide.
    /// </summary>
    private static readonly uint[] s_InputHandlers_00562ce0 =
        [0x0050E140, 0x0050DE90, 0x0050DD00, 0x0050DFF0];

    /// <summary>Ghidra: DAT_005de708 — l'index de membre que le declenchement du repartiteur
    /// <c>DAT_005e9ae4</c> met de cote, en meme temps que <c>DAT_005de540</c>. Hors du bloc de
    /// camera (qui s'arrete a 0x005de100), donc declare a part.</summary>
    private static uint s_Unknown_005de708;

    /// <summary>
    /// Ghidra: FUN_0050bf20 @ 0x0050bf20 — <b>le pas par personnage</b>. Voir le pave ci-dessus pour
    /// les quatre lectures que seule la machine donne.
    ///
    /// <para>
    /// La fonction fait, dans l'ordre : avancer l'animation, lire la manette (ou l'intention de
    /// l'IA), decider d'un changement d'etat, appliquer le deplacement au terrain, gerer le
    /// portage par une autre entite, puis publier le resultat. Elle sort tot dans six cas
    /// distincts, tous conserves.
    /// </para>
    /// <para>
    /// <b>Onze appelees ne sont pas encore transcrites</b> et levent chacune en se nommant. Toutes
    /// sont dans <c>Blaze.c</c> — aucune ne demande de desassemblage. Elles n'ont pas ete ecrites
    /// d'avance : c'est le jeu qui dira lesquelles il emprunte, comme pour les verbes de zone et
    /// les gestionnaires d'entite.
    /// </para>
    /// </summary>
    private static void TickPartyMember(int member)
    {
        byte* mainMem = GetMainMemPtr();

        byte* s = Inp.s_SlotFlags_005e93c0 + member * 0x9c;      // Ghidra: puVar3
        uint* slot = (uint*)s;
        byte* entry = Inp.s_SlotArea_005e9180 + member * 0x4c;   // Ghidra: puVar11, alias iStack_4
        byte slotKind = Inp.s_PlayerSlots_005e9188[member * 0x4c];
        byte* pad = Inp.s_InputStateBase_00604a20 + slotKind * 0x10;   // Ghidra: puVar21, alias unaff_EBX
        byte* perChar = mainMem + 0xf0000 + member * 0x2000;           // Ghidra: iVar23, alias unaff_EBP

        if ((*slot & 0x4000008) != 0) return;

        if (perChar[0x207] != 0) perChar[0x207]--;

        // La cible tenue a +0x74 est oubliee des qu'elle cesse d'etre active.
        byte* watched = *(byte**)(s + 0x74);
        if (watched != null && (*(uint*)watched & 0x80000000) == 0) *(byte**)(s + 0x74) = null;

        // Le bouton 3 ouvre le menu, mais seulement si le repartiteur est au repos.
        if ((*slot & 0x40010000) == 0 && (pad[2] & 8) != 0 && (*(uint*)Mem.Arena(0x005e9ae4) & 3) == 0)
        {
            s_Unknown_005de540 = s[8];
            s_Unknown_005de708 = s_Unknown_005de540;
            (*Mem.Arena(0x005e9ae4))++;
            *Mem.Entity(0x005fa580) = 1;
        }

        // Le compte a rebours de transition : tant qu'il tourne, le membre est fige.
        if (*(ushort*)(s + 0x12) != 0)
        {
            *(ushort*)(s + 0x12) = 0x14;
            if ((*slot & 0x10000000) != 0) return;

            *(short*)(s + 0x32) = *(short*)(s + 0x98);
            *slot |= 0x10000000;
            return;
        }

        // ---------------------------------------------------------------- animation
        uint state = *slot;
        uint running = state & 0xfdffffff;
        *slot = running;

        byte* animSet = *(byte**)(Mem.Arena(0x005e9680) + s[4] * 4);
        byte* anim = *(byte**)(animSet + *(short*)(s + 0x18) * 4);

        if ((state & 0x1000000) == 0)
        {
            short frame = *(short*)(s + 0x1a);
            if (frame + 1 < (int)*(ushort*)(anim + 0x16))
            {
                // Un membre porte par une autre entite (0x40000000) n'avance qu'une image sur deux.
                if ((state & 0x40000000) == 0) *(short*)(s + 0x1a) = (short)(frame + 1);
                else if ((*(uint*)Mem.Entity(0x005f2cf0) & 1) != 0) *(short*)(s + 0x1a) = (short)(frame + 1);
            }
            else
            {
                *slot = running | 0x2000000;
                if ((state & 0x40000005) == 0)
                {
                    // Ghidra rend l'ecriture par CONCAT31 — c'est un `mov [.],al` sur l'octet bas,
                    // qui laisse la valeur inchangee puisque 0x2000000 ne touche pas cet octet.
                    *(uint*)(s + 0x18) = 0;
                    *slot = ((running | 0x2000000) & 0xfffffffd) | 0x1000000;
                }

                if ((*slot & 0x40) != 0) *slot &= 0xffffffbf;
            }
        }
        else
        {
            (*(short*)(s + 0x1a))++;
            if ((int)*(ushort*)(anim + 0x16) <= *(short*)(s + 0x1a)) *(short*)(s + 0x1a) = 0;
        }

        short stateId = *(short*)(s + 0x18);
        if (stateId == 0) *slot &= 0xfffffffb;

        state = *slot;
        if ((state & 0x40000000) == 0)
        {
            // La regeneration lente, un centieme d'image, tant qu'aucune touche n'est tenue.
            if ((*(ushort*)pad & *(ushort*)(perChar + 0x1fc4)) == 0)
            {
                short maximum = *(short*)(perChar + 0x14a);
                if (*(short*)(perChar + 0x14e) < maximum
                    && (perChar[0x140] & 2) == 0
                    && (state & 0x80) == 0
                    && *(uint*)Mem.Entity(0x005f2cf0) % 100 == 0)
                {
                    short raised = (short)(*(short*)(perChar + 0x156) + *(short*)(perChar + 0x14e));
                    *(short*)(perChar + 0x14e) = raised;
                    if (maximum < raised) *(short*)(perChar + 0x14e) = maximum;
                }
            }
        }
        else if (stateId == 7)
        {
            if ((state & 0x2000000) != 0 && (state & 0x200000) == 0) *slot = state | 0x200000;
        }
        else
        {
            // Porte, mais pas dans l'etat de portage : on y bascule.
            *(uint*)Mem.Arena(0x005e99c0) = 0;
            SetEntityAnimationState_005238E0(slot, 0x15);

            state = *slot;
            if ((state & 0x40000000) != 0)
            {
                *(uint*)(s + 0x18) = 7;
                *(uint*)(s + 0x38) = 0xffffff;
                *slot = (state & 0xf6fffff9) | 0x12000;
                byte carried = s[8];
                *(short*)(s + 0x36) = 1;
                *(short*)(s + 0x96) = 0;

                if (carried == (byte)Mem.s_Unknown_005de538)
                {
                    PassLeaderToNextEligible(slot, entry, 0);
                }

                *(uint*)(perChar + 0x140) = 0x40000000;
                for (int i = 0; i < 0x20; i++) ((uint*)(perChar + 0x180))[i] = 0;
            }
        }

        *(uint*)(s + 0x5c) = 0;

        // ---------------------------------------------------------------- l'aiguillage principal
        if ((*(uint*)(perChar + 0x140) & 0x400001c0) != 0)
        {
            // Hors de combat : l'etat retombe et le role de meneur passe.
            if ((*slot & 0x40000000) == 0) *(uint*)(s + 0x18) = 0;

            byte previousLeader = (byte)Mem.s_Unknown_005de538;
            if (s[8] == previousLeader)
            {
                PassLeaderToNextEligible(slot, entry, 0);
                if (previousLeader != (byte)Mem.s_Unknown_005de538) *slot |= 0x10000;
            }

            TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
            return;
        }

        if ((*slot & 0x80) != 0)
        {
            TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
            return;
        }

        if ((*slot & 0x8000000) != 0)
        {
            HandleKnockback_00530070(slot, entry, perChar, pad);
            TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
            return;
        }

        // Le tampon de commande : quatre images de memoire sur le bouton d'action.
        if (pad[0x0e] != 0) pad[0x0e]--;
        if ((*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fc4)) != 0) pad[0x0e] = 4;

        if ((*slot & 0x10002) == 0)
        {
            if ((*slot & 0x40) == 0)
            {
                DispatchInputHandler(perChar[0x2b7], slot, perChar, pad);
            }

            if ((*slot & 1) == 0)
            {
                ApplyGroundMotion_0051A020(slot, entry, perChar, pad, anim);
            }
        }

        DispatchPostureHandler((byte)(perChar[0x150] & 7), slot, entry, perChar, pad, anim);

        state = *slot;
        if ((state & 4) != 0)
        {
            TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
            return;
        }

        if ((state & 0x10000) == 0)
        {
            TickPlayableMember(member, s, slot, entry, slotKind, pad, perChar, state);
            TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
            return;
        }

        // ---------------------------------------------------------------- le membre suivi par l'IA
        if (s[8] == (byte)Mem.s_Unknown_005de538) PassLeaderToNextEligible(slot, entry, 0);

        uint follow = Mem.Entity(0x005edec0)[*Mem.Entity(0x005f2ce4)];
        (*Mem.Entity(0x005f2ce4))++;

        if (*(ushort*)(Inp.s_SlotFlags_005e93c0 + follow * 0x9c + 0x12) != 0)
        {
            uint held = *slot;
            *(short*)(s + 0x98) = *(short*)(s + 0x32);
            *slot = (held & 0xf7fffdff) | 0x2000;
            (*(ushort*)(s + 0x12))++;
            return;
        }

        TickFollowingMember(member, s, slot, entry, slotKind, pad, perChar, mainMem, follow);
        TickPartyMemberTail(member, s, slot, entry, pad, perChar, mainMem);
    }

    /// <summary>
    /// Ghidra: la branche <c>(*puVar3 &amp; 0x10000) == 0</c> de <see cref="TickPartyMember"/>
    /// (0x0050c396..0x0050c771) — <b>le membre que le joueur pilote</b>. Elle lit la manette et rien
    /// d'autre : coup, saut, et le tampon de quatre images qui rattrape une frappe legerement en
    /// avance.
    ///
    /// <para>
    /// Les deux moities du <c>if</c> final se ressemblent au point qu'on les croirait factorisables
    /// — meme pose de <c>0x4000</c> dans <c>+0x9a</c>, meme etat 4, meme decalage de position quand
    /// <c>+0x90</c> porte <c>0x100</c>. Elles different par une seule ligne : la premiere choisit sa
    /// portee selon le bouton tenu, la seconde la fixe a <c>0x1c000</c>. Transcrites separement,
    /// parce que ce sont deux blocs distincts du binaire et que l'oracle compare des corps.
    /// </para>
    /// </summary>
    private static void TickPlayableMember(
        int member, byte* s, uint* slot, byte* entry, byte slotKind, byte* pad, byte* perChar, uint state)
    {
        // Le coup : bouton d'attaque, pas deja en train de frapper, et le mode de mise au point
        // n'est pas en train de geler le monde.
        if ((state & 0x42) == 0
            && (*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fc0)) != 0
            && (sbyte)perChar[0x2b0] != -1
            && s_Unknown_00566c84 != 0
            && (perChar[0x140] & 0x10) == 0)
        {
            *(uint*)Mem.Arena(0x005e99c0) = 0;
            SetEntityAnimationState_005238E0(slot, 1);
            *slot = (*slot & 0xfcffffff) | 2;

            *(uint*)(s + 0x18) = (perChar[0x150] & 7) == 0 && (pad[2] & 0xf0) != 0 ? 6u : 3u;
        }

        // Le tampon de saut : quatre images de memoire sur le bouton, remis a zero si le bouton
        // RELACHE arrive dans la meme image — d'ou les deux mots de manette lus, +2 et +4.
        if ((sbyte)pad[0x0c] != 0) pad[0x0c]--;
        if ((*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fc2)) != 0) pad[0x0c] = 4;

        if ((*(ushort*)(perChar + 0x1fc2) & *(ushort*)(pad + 4)) != 0)
        {
            pad[0x0c] = 0;
            *(short*)(s + 0x9a) = 0x6000;
        }

        if ((*slot & 0x47) == 0)
        {
            if (pad[0x0c] == 0)
            {
                // Rien de tampon : reste le bouton de garde, qui libere une texture et un son.
                if ((*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fca)) != 0)
                {
                    Winsys3d.ReleaseTexture(0x18);
                    uint held = *slot;
                    *(uint*)(s + 0x18) = 0;
                    *slot = held | 0x9000000;
                    pad[0x0a] = 0xff;
                    Aud.PlayUiSound38();
                }

                return;
            }

            pad[0x0c] = 0;
            uint before = *slot;
            *(short*)(s + 0x9a) = 0x4000;
            *slot = (before & 0xfcffffff) | 1;

            // La portee du saut depend du bouton tenu, sauf en mode de mise au point.
            *(int*)(s + 0x54) = Mem.s_DebugModeLatch_005633a8 == 0
                ? (int)((uint)-(((*(ushort*)pad & 1) != 0) ? 1 : 0) & 0x7a000) + 0x1c000
                : 0x1c000;

            uint attributes = *(uint*)(s + 0x90);
            *(uint*)(s + 0x18) = 4;
            if ((attributes & 0x100) != 0)
            {
                *(int*)(s + 0x50) += *(short*)(entry + 0x44) * 8;
                *(int*)(s + 0x58) += *(short*)(entry + 0x48) * 8;
            }

            entry[0x0a] = 0;
        }
        else
        {
            if ((*slot & 0x46) != 0 || entry[0x0a] == 0) return;

            entry[0x0a]--;
            if ((*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fc2)) == 0) return;

            entry[0x0a] = 0;
            uint before = *slot;
            *(short*)(s + 0x9a) = 0x4000;
            *(int*)(s + 0x54) = 0x1c000;
            *slot = (before & 0xfcffffff) | 1;

            uint attributes = *(uint*)(s + 0x90);
            *(uint*)(s + 0x18) = 4;
            if ((attributes & 0x100) != 0)
            {
                *(int*)(s + 0x50) += *(short*)(entry + 0x44) * 8;
                *(int*)(s + 0x58) += *(short*)(entry + 0x48) * 8;
            }
        }

        Aud.PlayPositionalSound(0, 0xd00, 0x800, (int*)(s + 0x1c));
    }

    /// <summary>
    /// Ghidra: la branche <c>else</c> de 0x0050c3c8..0x0050caf3 — <b>le membre que l'IA fait
    /// suivre</b>. Il court derriere sa cible tant qu'il en est loin, se tourne vers elle en
    /// arrivant, et sonde le terrain devant lui pour sauter ce qui doit l'etre.
    ///
    /// <para>
    /// <b>Le flot a ete reconstruit, pas translittere.</b> Ghidra en rend deux labels
    /// (<c>LAB_0050c4d6</c> et <c>LAB_0050c781</c>) sautes depuis trois endroits, ce qui donne
    /// l'impression d'un graphe irreductible. Il ne l'est pas : <b>toutes</b> les routes qui
    /// atteignent <c>LAB_0050c781</c> le font avec une distance restante nulle ou negative, et
    /// toutes celles qui atteignent le bloc de sonde le font avec une distance d'au moins 1. Les
    /// deux labels se ramenent donc a un seul <c>if (distance &gt;= 1)</c>, et le drapeau
    /// « entrer en approche » que la lecture naive suggere est inutile.
    /// </para>
    /// <para>
    /// La cible est le membre suivant dans la liste <c>DAT_005edec0</c> quand celui-ci est
    /// disponible, sinon le meneur ; la distance d'arret vaut 100 dans le premier cas et la portee
    /// de posture <see cref="s_PostureSpeed_00562cc0"/> dans le second.
    /// </para>
    /// </summary>
    private static void TickFollowingMember(
        int member, byte* s, uint* slot, byte* entry, byte slotKind,
        byte* pad, byte* perChar, byte* mainMem, uint follow)
    {
        // L'echelle de portee, choisie par les drapeaux du bloc par personnage.
        uint posture = *(uint*)(perChar + 0x140);
        int scale = 0x1000;
        if ((posture & 0x4000) != 0) scale = 0x1333;
        if ((posture & 0x8000) != 0) scale = 0xccc;
        if ((posture & 4) != 0) scale = 0x800;

        uint leaderIndex = (uint)(byte)Mem.s_Unknown_005de538;
        byte* leaderSlot = Inp.s_SlotFlags_005e93c0 + leaderIndex * 0x9c;
        byte* leaderBlock = mainMem + 0xf0000 + leaderIndex * 0x2000;

        int reach = s_PostureSpeed_00562cc0[leaderBlock[0x150] & 7] * scale >> 0xc;

        int dx = 0;
        int dz = 0;
        int distance;

        if (*(ushort*)(Inp.s_SlotFlags_005e93c0 + follow * 0x9c + 0x12) == 0
            && (leaderSlot[0] & 1) == 0)
        {
            byte* target = Inp.s_SlotFlags_005e93c0 + follow * 0x9c;
            dx = (*(int*)(target + 0x1c) - *(int*)(s + 0x1c)) >> 0xc;
            dz = (*(int*)(target + 0x24) - *(int*)(s + 0x24)) >> 0xc;
            distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) - 100;
        }
        else if ((*(uint*)(leaderSlot + 0x90) & 0x8000) == 0)
        {
            dx = (*(int*)(leaderSlot + 0x1c) - *(int*)(s + 0x1c)) >> 0xc;
            dz = (*(int*)(leaderSlot + 0x24) - *(int*)(s + 0x24)) >> 0xc;
            distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) - reach;
        }
        else
        {
            // Ghidra laisse ici dx et dz a leur valeur precedente ; ils sont pourtant relus par
            // l'appel a ratan2 ci-dessous. Conserve tel quel — c'est ce que fait l'original, qui
            // lit deux emplacements de pile non ecrits sur ce chemin.
            distance = 0;
        }

        uint held = *slot;
        if ((distance < 1 && (held & 1) == 0) || (held & 2) != 0)
        {
            if (*(short*)(s + 0x18) != 0 && (*slot & 0x47) == 0)
            {
                *(uint*)(s + 0x18) = 0;
                *slot |= 0x1000000;
            }

            *(short*)(s + 0x98) = *(short*)(s + 0x32);
        }
        else
        {
            *(short*)(s + 0x98) = (short)Winsys3d.Ordinal_457(dx, dz);

            if ((*(uint*)(leaderSlot + 0x90) & 0x8000) == 0 && (*slot & 0x40) == 0)
            {
                held = *slot;
                if ((held & 7) == 0 && *(short*)(s + 0x18) != 1)
                {
                    *(uint*)(s + 0x18) = 1;
                    *slot = held | 0x1000000;
                }

                if (distance > 0)
                {
                    *(uint*)(s + 0x5c) = (uint)Math.Min(distance, reach) << 0xc;
                }
            }
        }

        if (distance >= 1)
        {
            if ((*slot & 0x46) == 0 && entry[0x0a] != 0)
            {
                entry[0x0a]--;
                if (ProbeGroundAhead(member, s, slot, entry, perChar, mainMem)) return;
            }
        }
        else if ((*slot & 0x46) == 0)
        {
            byte* obstacle = FindNearestOpponent_00474EB0(slot, 0xffffffff, 1);
            if (obstacle != null)
            {
                int ox = (*(int*)(obstacle + 0x1c) - *(int*)(s + 0x1c)) >> 0xc;
                int oy = (*(int*)(obstacle + 0x20) - *(int*)(s + 0x20)) >> 0xc;
                int oz = (*(int*)(obstacle + 0x24) - *(int*)(s + 0x24)) >> 0xc;
                int range = (int)Winsys3d.Ordinal_442((uint)(ox * ox + oy * oy + oz * oz));

                // Ghidra passe `iStack_24` en premier argument de ratan2, un emplacement qu'aucune
                // route n'ecrit sur ce chemin. Conserve tel quel.
                uint turn = (uint)(Winsys3d.Ordinal_457(ox, oy) - *(ushort*)(s + 0x32)) & 0xfff;
                byte stance = (byte)(perChar[0x150] & 7);

                if (range < (int)((uint)-(stance != 4 ? 1 : 0) & 0xffffff06) + 400
                    && ((turn + 0x80) & 0xfff) < 0x100
                    && *(ushort*)Mem.Arena(0x005e9ae6) != 0)
                {
                    if ((perChar[0x140] & 0x10) == 0 && (sbyte)perChar[0x2b0] != -1
                        && s_Unknown_00566c84 != 0)
                    {
                        *(uint*)Mem.Arena(0x005e99c0) = 0;
                        SetEntityAnimationState_005238E0(slot, 1);
                        held = *slot;
                        short facing = *(short*)(s + 0x98);
                        *(uint*)(s + 0x18) = 3;
                        *slot = (held & 0xfcffffff) | 2;
                        *(short*)(s + 0x32) = facing;
                        return;
                    }
                }
                else if (range < (int)((uint)-(stance != 4 ? 1 : 0) & 0xfffffed4) + 500
                         && ((turn + 0x80) & 0xfff) > 0x100)
                {
                    // Un pas de 0x80 vers la cible, dans le sens le plus court.
                    *(short*)(s + 0x98) += (short)((((turn > 0x7ff ? 1 : 0) - 1) & 0x100) - 0x80);
                }
            }
        }

        *(short*)(s + 0x32) = *(short*)(s + 0x98);
    }

    /// <summary>
    /// Ghidra: 0x0050c4f0..0x0050c780 — <b>la sonde de terrain</b> que le membre suivi lance devant
    /// lui avant d'avancer. Elle recopie l'entite dans le bloc d'essai <c>DAT_005e9a40</c>, la
    /// deplace de quatre pas d'un seizieme d'unite le long de son cap, et regarde ce que
    /// <see cref="FinalizeEntityPlacement2"/> rend a chaque pas.
    ///
    /// <para>
    /// <b>L'axe de chaque pas a ete verifie au desassembleur, pas suppose</b> : Ghidra reutilise le
    /// meme emplacement de pile pour le sinus et le cosinus, si bien que la pseudo-C ajoute deux
    /// fois la meme valeur. Le binaire (0x0050c512..0x0050c57b) donne <c>sinus &lt;&lt; 4</c> sur X et
    /// <c>cosinus &lt;&lt; 4</c> sur Z — la meme convention que
    /// <see cref="TrySpawnRandomEncounter"/>.
    /// </para>
    /// <para>
    /// Deux verdicts sortent de la sonde : un obstacle franchissable (bit 1) fait sauter, un mur
    /// (bit 0x800) fait tourner. Le bloc de rotation calcule l'angle de la pente rencontree et
    /// arrondit le cap au multiple le plus proche, avec trois plages distinctes selon l'ecart.
    /// </para>
    /// </summary>
    /// <returns><c>true</c> quand la sonde a change l'etat du membre et que l'appelant doit rendre
    /// la main sans poser le cap.</returns>
    private static bool ProbeGroundAhead(
        int member, byte* s, uint* slot, byte* entry, byte* perChar, byte* mainMem)
    {
        int facing = *(short*)(s + 0x98);
        int stepX = (int)Winsys3d.GetSinTableEntry(facing) << 4;
        int stepZ = Winsys3d.GetCosTableEntry(facing) << 4;

        byte* probe = Mem.Arena(0x005e9a40);
        byte* probeEntry = Mem.Entity(0x005f7d80);

        Mem.CopyDwords(slot, (uint*)probe, 0x27);
        Mem.CopyDwords((uint*)entry, (uint*)probeEntry, 0x13);
        *(uint*)probe |= 0x2000;

        *(int*)(probe + 0x1c) = *(int*)(s + 0x1c) + stepX;
        *(int*)(probe + 0x20) = *(int*)(s + 0x60) + 0x6e000;
        *(int*)(probe + 0x24) = *(int*)(s + 0x24) + stepZ;

        uint verdict = 0;
        int ground = *(int*)(s + 0x60);

        // Ghidra: `iStack_c = DAT_005e9abc;` avant chaque placement, restaure au bloc de rotation.
        // C'est une mise de cote, pas un calcul.
        int savedAbc = *(int*)Mem.Arena(0x005e9abc);
        int hereY = *(int*)(s + 0x20);

        FinalizeEntityPlacement2((uint*)probe);
        ApplyEntityMotion_00519DF0((uint*)probe, probeEntry);

        if ((*(uint*)(probe + 0x90) & 0x800) == 0)
        {
            if ((*(uint*)(probe + 0x90) & 0x2000) != 0) verdict |= 2;

            if ((*(uint*)(probe + 0x90) & 0x8000) == 0
                && (((*(uint*)(probe + 0x90) & 0x400) != 0
                     && (perChar[0x150] & 7) != 7
                     && (*(uint*)(perChar + 0x140) & 0x400000) == 0)
                    || ground + 0x32000 <= *(int*)(probe + 0x64)
                    || *(int*)(probe + 0x64) <= ground - 0x64000
                    || ((*(uint*)(probe + 0x90) & 0x100) != 0 && ground < *(int*)(probe + 0x60))))
            {
                verdict |= 1;
            }

            bool blocked = false;
            for (int step = 3; step != 0; step--)
            {
                ground = *(int*)(probe + 0x60);
                hereY = ground;
                savedAbc = *(int*)Mem.Arena(0x005e9abc);
                *(int*)(probe + 0x1c) += stepX;
                *(int*)(probe + 0x20) = *(int*)(probe + 0x60) + 0x6e000;
                *(int*)(probe + 0x24) += stepZ;

                FinalizeEntityPlacement2((uint*)probe);
                ApplyEntityMotion_00519DF0((uint*)probe, probeEntry);

                if ((*(uint*)(probe + 0x90) & 0x800) != 0) { blocked = true; break; }

                if ((*(uint*)(probe + 0x90) & 0x8000) == 0 && ground + 0x32000 <= *(int*)(probe + 0x64))
                {
                    verdict = 1;
                }
                else if (step == 3 && (*(uint*)(probe + 0x90) & 0x2000) == 0)
                {
                    verdict &= 1;
                }
            }

            if (!blocked)
            {
                if (verdict == 0) return false;

                // Franchissable : le membre saute, exactement comme le fait la branche jouable.
                entry[0x0a] = 0;
                uint before = *slot;
                uint attributes = *(uint*)(s + 0x90);
                *(short*)(s + 0x9a) = 0x4000;
                *slot = (before & 0xfcffffff) | 1;
                *(int*)(s + 0x54) = 0x1c000;
                *(uint*)(s + 0x18) = 4;

                if ((attributes & 0x100) != 0)
                {
                    *(int*)(s + 0x50) += *(short*)(entry + 0x44) * 8;
                    *(int*)(s + 0x58) += *(short*)(entry + 0x48) * 8;
                }

                Aud.PlayPositionalSound(0, 0xd00, 0x800, (int*)(s + 0x1c));
                *(short*)(s + 0x32) = *(short*)(s + 0x98);
                return true;
            }
        }

        // Ghidra: LAB_0050dc15 — un mur. Le cap est recale sur la normale de la pente rencontree,
        // et l'arrondi depend de l'ecart : moins d'un huitieme de tour se colle, sinon on tourne
        // d'un quart de l'ecart, dans un sens ou dans l'autre.
        if (*(uint*)Mem.Arena(0x005e9ab8) != 0) return false;

        *(int*)Mem.Arena(0x005e9abc) = savedAbc;

        // Ghidra passe `&iStack_20`, c'est-a-dire l'adresse de trois locales contigues : la
        // position du membre, avec en Y la hauteur de sol relevee au dernier pas de sonde.
        int* here = stackalloc int[3];
        here[0] = *(int*)(s + 0x1c);
        here[1] = hereY;
        here[2] = *(int*)(s + 0x24);

        int wallAngle = GroundSlopeAngle_005335E0((uint*)probe, here);
        ushort probeFacing = *(ushort*)(probe + 0x32);

        int corrected = ((wallAngle - (uint)*(ushort*)(s + 0x98)) & 0xfff) < 0x800
            ? wallAngle - 0x428
            : wallAngle + 0x428;

        *(ushort*)(s + 0x98) = probeFacing;
        uint delta = (uint)(corrected - probeFacing) & 0xfff;

        short newFacing;
        if (((delta + 0x80) & 0xfff) < 0x101)
        {
            uint signed = delta > 0x7ff ? delta - 0x1000 : delta;
            newFacing = (short)(probeFacing + (short)signed);
        }
        else if (delta < 0x800)
        {
            newFacing = (short)(probeFacing + (short)((int)delta >> 2));
        }
        else
        {
            newFacing = (short)(probeFacing - (short)((int)(0x1000 - delta) >> 2));
        }

        *(short*)(s + 0x98) = newFacing;
        *(short*)(s + 0x32) = newFacing;
        return true;
    }

    // =====================================================================
    // Les lacunes de FUN_0050bf20. Toutes ses appelees manquantes sont dans `Blaze.c` — aucune ne
    // demande de desassemblage — et aucune n'a ete ecrite d'avance : c'est le jeu qui dira
    // lesquelles il emprunte, comme pour les verbes de zone et les gestionnaires d'entite.
    // =====================================================================

    /// <summary>Ghidra: FUN_005238e0 @ 0x005238e0 (39 l.) — declenche l'objet equipe dont
    /// l'identifiant correspond, en sauvegardant puis restaurant la banque 0x005e9060 autour de
    /// l'appel. Tire <c>FUN_00523a20</c>.</summary>
    private static void SetEntityAnimationState_005238E0(uint* slot, uint kind)
        => throw new NotImplementedException(
            $"FUN_005238e0 (0x005238e0), declenchement {kind} : non transcrite. 39 lignes dans " +
            "Blaze.c, plus FUN_00523a20. Elle balaie les cinq emplacements d'objet equipe du bloc " +
            "par personnage (+0x2b0) et agit sur celui dont l'identifiant vaut le second argument.");

    /// <summary>Ghidra: FUN_00530070 @ 0x00530070 (365 l.) — le recul apres un coup recu, pris
    /// quand le mot d'etat porte <c>0x8000000</c>.</summary>
    private static void HandleKnockback_00530070(uint* slot, byte* entry, byte* perChar, byte* pad)
        => throw new NotImplementedException(
            "FUN_00530070 (0x00530070) : non transcrite. 365 lignes dans Blaze.c — le recul du " +
            "personnage touche. Elle prend (entite, entree d'emplacement, bloc par personnage, " +
            "manette) ; Ghidra lui donne les quatre parametres.");

    /// <summary>Ghidra: FUN_0051a020 @ 0x0051a020 (221 l.) — l'integration du deplacement au sol,
    /// appelee quand le personnage n'est pas en l'air.</summary>
    private static void ApplyGroundMotion_0051A020(uint* slot, byte* entry, byte* perChar, byte* pad, byte* anim)
    {
        byte* s = (byte*)slot;

        switch (perChar[0x150] & 7)
        {
            // ------------------------------------------------ la marche et sa variante
            case 0:
            case 6:
                if (pad[0x0e] != 0
                    && (sbyte)perChar[0x2b2] != -1
                    && s_Unknown_00566c84 != 0
                    && (*slot & 0x44) == 0
                    && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                {
                    *(uint*)(s + 0x18) = 5;
                    *slot = (*slot & 0xfeffffff) | 4;
                }

                if ((*(ushort*)pad & *(ushort*)(perChar + 0x1fc4)) == 0 && (*slot & 0x2000000) != 0)
                {
                    *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x18) == 5)
                {
                    *(int*)(s + 0x5c) = 0;
                    return;
                }

                break;

            // ------------------------------------------------ les quatre postures d'incantation
            case 1:
            case 3:
            case 5:
            case 7:
            {
                byte* spellSet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                           + perChar[0x2b5] * 4);
                uint held = *slot;

                if ((held & 4) == 0)
                {
                    if ((held & 0x40) != 0) return;
                    if ((*(uint*)(perChar + 0x140) & 0x400003e8) != 0) return;

                    if ((*(ushort*)(pad + 2) & 1) != 0 && s_Unknown_00566c84 != 0)
                    {
                        // Annulation : l'incantation part quand meme, mais sans effet retenu.
                        *slot = held | 0x1000004;
                        *(uint*)(s + 0x18) = 5;
                        entry[0x0b] = 0;
                        *(ushort*)(pad + 0x0c) = 0;
                        *(ushort*)(pad + 0x0e) = 0;
                        *(short*)(s + 0x14) = 0;
                        SpawnCastEntity_0047AAE0(s, 0xbd, 0, 0);
                        return;
                    }

                    if ((*(ushort*)(perChar + 0x1fc4) & *(ushort*)(pad + 2)) == 0) return;
                    if (s_Unknown_00566c84 == 0) return;

                    *(uint*)(s + 0x18) = 5;
                    *slot = held | 0x1000004;

                    byte* spell = spellSet + perChar[0x2b6] * 0x30;
                    *(ushort*)(s + 0x14) = spell[0x13];
                    *(ushort*)(perChar + 0x204) = *(ushort*)(spell + 0x14);
                    SpawnCastEntity_0047AAE0(s, 0xbd, 0, 0);
                    *(uint*)Mem.Arena(0x005e99c0) = 0;
                    SetEntityAnimationState_005238E0(slot, 5);
                    return;
                }

                *(int*)(s + 0x5c) = 0;

                if (*(short*)(s + 0x18) == 5)
                {
                    if (*(short*)(s + 0x14) == 0)
                    {
                        if ((*(ushort*)pad & 1) == 0)
                        {
                            // Le bouton est relache : on choisit la cible et on passe a l'etat 6.
                            *slot = held & 0xfeffffff;
                            *(uint*)(s + 0x18) = 6;
                            *(short*)(s + 0x14) = 5;

                            int chosen = PickCastTarget_0051A5D0(entry, perChar);
                            bool armed = false;

                            if (chosen >= 0)
                            {
                                uint* mask = (uint*)(perChar + ((chosen >> 5) + 0x58 + perChar[0x2b5] * 2) * 4);
                                if ((*mask & (1u << (chosen & 0x1f))) != 0)
                                {
                                    *(ushort*)(perChar + 0x204) = *(ushort*)(spellSet + chosen * 0x30 + 0x14);
                                    *(short*)(s + 0x16) = (short)chosen;
                                    armed = true;
                                }
                            }

                            if (!armed)
                            {
                                *slot = (*slot & 0xfffffffb) | 0x1000000;
                                *(uint*)(s + 0x18) = 0;
                            }
                        }
                        else if ((*(ushort*)(pad + 2) & 0xf000) != 0 && entry[0x0b] < 0x10)
                        {
                            // Le bouton est tenu : chaque direction s'empile dans la combinaison.
                            entry[0x0c + entry[0x0b]] = (byte)((*(ushort*)(pad + 2) >> 8) & 0xf0);
                            entry[0x0b]++;
                        }
                    }
                    else
                    {
                        if ((*(ushort*)pad & *(ushort*)(perChar + 0x1fc4)) == 0)
                        {
                            *slot = (held & 0xfffffffb) | 0x1000000;
                            *(uint*)(s + 0x18) = 0;
                        }
                        else
                        {
                            short remaining = (short)(*(short*)(s + 0x14) - 1);
                            *(short*)(s + 0x14) = remaining;
                            if (remaining == 0)
                            {
                                *(uint*)(s + 0x18) = 6;
                                *slot = held & 0xfeffffff;
                                *(short*)(s + 0x14) = 5;

                                uint index = perChar[0x2b6];
                                uint* mask = (uint*)(perChar + (((int)index >> 5) + 0x58 + perChar[0x2b5] * 2) * 4);
                                if ((*mask & (1u << (perChar[0x2b6] & 0x1f))) == 0) index = 0xffffffff;
                                *(short*)(s + 0x16) = (short)index;
                            }
                        }
                    }
                }

                if (*(short*)(s + 0x18) == 6)
                {
                    held = *slot;
                    if ((held & 0x2000000) != 0)
                    {
                        *(short*)(s + 0x14) = (short)(*(short*)(s + 0x14) - 1);
                        if (*(short*)(s + 0x14) == 0)
                        {
                            *(uint*)(s + 0x18) = 0;
                            *slot = (held & 0xfffffffb) | 0x1000000;
                            return;
                        }

                        *slot = held & 0xfeffffff;

                        // L'etat et l'image sont poses d'un seul dword : l'animation demarre a sa
                        // DERNIERE image.
                        *(uint*)(s + 0x18) = ((uint)(*(ushort*)(anim + 0x16) - 1) << 0x10) | 6;

                        if (*(short*)(s + 0x16) >= 0)
                        {
                            *(uint*)Mem.Arena(0x005e99c0) = 0;
                            SetEntityAnimationState_005238E0(slot, 6);

                            // Le cout n'est preleve que si la reserve le couvre — sinon
                            // l'incantation part sans rien consommer ni produire.
                            if (*(ushort*)(perChar + 0x204) <= (int)*(short*)(perChar + 0x14e))
                            {
                                *(ushort*)(perChar + 0x14e) =
                                    (ushort)(*(short*)(perChar + 0x14e) - *(ushort*)(perChar + 0x204));
                                CastSpell_0053A980(slot, perChar[0x2b5], *(short*)(s + 0x16), -1, -1);
                            }

                            *(ushort*)(s + 0x16) = 0xffff;
                            return;
                        }
                    }
                }

                break;
            }

            // ------------------------------------------------ la charge
            case 2:
                if (*(short*)(s + 0x18) != 5 || (*slot & 0x2000000) != 0)
                {
                    *(short*)(s + 0x14) = 0;
                    *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x14) == 0)
                {
                    if (pad[0x0e] != 0
                        && (*slot & 0x44) == 0
                        && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                    {
                        *(ushort*)(pad + 2) &= 0x7fff;
                        *(uint*)(s + 0x18) = 5;
                        *(short*)(s + 0x14) = 0x10;
                        *slot = (*slot & 0xfeffffff) | 4;
                        *(short*)(s + 0x98) = *(short*)(s + 0x32);
                    }
                }
                else
                {
                    short remaining = (short)(*(short*)(s + 0x14) - 1);
                    *(short*)(s + 0x14) = remaining;
                    if (remaining == 0) *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x18) == 5)
                {
                    uint posture = *(uint*)(perChar + 0x140);
                    int scale = 0x1000;
                    if ((posture & 0x4000) != 0) scale = 0x1333;
                    if ((posture & 0x8000) != 0) scale = 0xccc;
                    if ((posture & 4) != 0) scale = 0x800;

                    // La charge decelere toute seule : la vitesse suit le compte a rebours.
                    *(int*)(s + 0x5c) = *(short*)(s + 0x14) * 0x2000 * scale >> 0xc;
                    return;
                }

                break;

            // ------------------------------------------------ l'objet consommable
            case 4:
                if ((*slot & 0x44) == 0
                    && (*(ushort*)pad & *(ushort*)(perChar + 0x1fc4)) != 0
                    && (*(ushort*)(pad + 2) & *(ushort*)(perChar + 0x1fc4)) != 0
                    && s_Unknown_00566c84 != 0
                    && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                {
                    *(uint*)(s + 0x18) = 5;
                    *slot = (*slot & 0xfcffffff) | 4;
                    *(short*)(s + 0x14) = 5;
                }

                if ((*slot & 4) != 0)
                {
                    if ((*slot & 0x2000000) != 0)
                    {
                        if (*(short*)(s + 0x14) == 5)
                        {
                            // Ghidra lit ici les huit octets de +0x160 comme un COMPTE par
                            // emplacement, la ou la posture d'incantation lit la meme region
                            // comme un MASQUE de bits. Les deux vues coexistent dans l'original ;
                            // transcrites telles quelles. TODO(REVERSE) : etablir laquelle est la
                            // bonne, ou si les deux `kind` ne se recouvrent jamais.
                            byte* charges = perChar + 0x160 + perChar[0x2b5] * 8;
                            sbyte left = (sbyte)charges[perChar[0x2b6]];
                            if (left != 0)
                            {
                                charges[perChar[0x2b6]] = (byte)(left - 1);
                                s[6] = perChar[0x2b6];
                                *slot |= 0x800;
                                CastSpell_0053A980(slot, perChar[0x2b5], perChar[0x2b6], -1, -1);
                            }
                        }

                        *(short*)(s + 0x14) = (short)(*(short*)(s + 0x14) - 1);
                        if (*(short*)(s + 0x14) != 0)
                        {
                            *slot &= 0xfeffffff;
                            ushort frames = *(ushort*)(anim + 0x16);
                            *(int*)(s + 0x5c) = 0;
                            *(uint*)(s + 0x18) = ((uint)(frames - 1) << 0x10) | 5;
                            return;
                        }

                        *(uint*)(s + 0x18) = 0;
                        *slot = (*slot & 0xfffffffb) | 0x1000000;
                    }

                    *(int*)(s + 0x5c) = 0;
                    return;
                }

                break;

            default:
                *slot &= 0xfffffffb;
                break;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047aae0 @ 0x0047aae0 — <b>fait naitre une entite d'incantation</b> dans la table
    /// <c>DAT_005edee0</c>, en recopiant les 0x27 dwords du lanceur puis en remplacant six champs.
    /// Jumelle de <c>SpawnEffectEntity</c> (<c>FUN_0047a9d0</c>) a la table pres.
    ///
    /// <para>Le masque final <c>&amp; 0x10e82f02 | 0x80109000</c> ne garde du mot d'etat copie que ce
    /// qui a un sens pour une entite d'effet.</para>
    /// </summary>
    /// <returns>1 si un emplacement etait libre, 0 sinon.</returns>
    private static uint SpawnCastEntity_0047AAE0(byte* source, byte subtype, byte param3, int param4)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x006007a0));
        if (index < 0) return 0;

        byte* born = Mem.Entity(0x005edee0) + index * 0x9c;
        Mem.CopyDwords((uint*)source, (uint*)born, 0x27);

        born[0x36] |= 0x80;
        born[0x08] = (byte)index;
        born[0x05] = subtype;
        *(byte**)(born + 0x6c) = source;
        born[0x0b] = param3;
        if (param4 >= 0) born[0x0a] = (byte)param4;

        *(uint*)born = (*(uint*)born & 0x10e82f02) | 0x80109000;
        *(ushort*)(born + 0x10) = 0;
        return 1;
    }

    /// <summary>Ghidra: FUN_0051a5d0 @ 0x0051a5d0 (46 l.) — reconnait la combinaison de directions
    /// empilee dans l'entree d'emplacement et rend l'indice du sort correspondant, ou -1.</summary>
    private static int PickCastTarget_0051A5D0(byte* entry, byte* perChar)
        => throw new NotImplementedException(
            "FUN_0051a5d0 (0x0051a5d0) : non transcrite. 46 lignes dans Blaze.c — la " +
            "reconnaissance de la combinaison de directions saisie pendant l'incantation. Elle " +
            "rend un indice de sort, ou une valeur negative si rien ne correspond.");

    /// <summary>Ghidra: FUN_0053a980 @ 0x0053a980 (31 l.) — declenche le sort ou l'objet
    /// choisi.</summary>
    private static void CastSpell_0053A980(uint* caster, byte kind, int index, int a, int b)
        => throw new NotImplementedException(
            $"FUN_0053a980 (0x0053a980), famille {kind}, indice {index} : non transcrite. " +
            "31 lignes dans Blaze.c.");

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_FUN_00562ce0)[bloc + 0x2b7])(…)</c> — quatre gestionnaires de
    /// commande, releves dans le binaire sous <see cref="s_InputHandlers_00562ce0"/>. Aucun n'est
    /// le corps vide.
    /// </summary>
    private static void DispatchInputHandler(byte index, uint* slot, byte* perChar, byte* pad)
    {
        uint target = index < s_InputHandlers_00562ce0.Length
            ? s_InputHandlers_00562ce0[index]
            : 0;

        switch (target)
        {
            case 0x0050E140: HandleDirectionalInput_0050E140(slot, perChar, pad); return;

            default:
                throw new NotImplementedException(
                    $"FUN_0050bf20 : le gestionnaire de commande d'indice {index} (table " +
                    $"0x00562ce0) vaut 0x{target:X6} et n'est pas transcrit. Les quatre entrees de " +
                    "la table sont relevees dans le binaire : 0x0050E140, 0x0050DE90 (toutes deux " +
                    "dans Blaze.c), 0x0050DD00 et 0x0050DFF0 (absentes, a desassembler). Il prend " +
                    "(entite, bloc par personnage, manette).");
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e140 @ 0x0050e140 — l'entree <b>0</b> de la table de commande
    /// <c>PTR_FUN_00562ce0</c> : <b>la marche a la croix directionnelle</b>. C'est le premier
    /// gestionnaire que la carte du monde atteint, et le seul chemin par lequel le joueur fait
    /// bouger son personnage.
    ///
    /// <para>
    /// Trois façons d'obtenir une direction, selon le bit 3 de <c>bloc + 0x140</c> : normalement
    /// la croix, lue a travers <see cref="s_PadDirection_00562dc8"/> ; quand le bit est pose — le
    /// personnage est desoriente — <b>une direction tiree au sort</b> des qu'une touche est
    /// enfoncee, et rien du tout sinon.
    /// </para>
    /// <para>
    /// La direction n'est convertie en cap qu'a la <b>transition</b> de la croix (bit 1 de
    /// <c>manette + 0xb</c>, consomme au passage), pas a chaque image : c'est ce qui empeche le
    /// personnage de tourner en continu tant qu'une direction est tenue. Le cap est relatif a la
    /// camera, arrondi au seizieme de tour (<c>(angle + 0x80) &amp; 0xf00</c>) — et la meme
    /// desorientation remplace cette reference par un tirage.
    /// </para>
    /// <para>
    /// La vitesse vient de <see cref="s_PostureSpeed_00562cc0"/>, sauf en mode de mise au point
    /// avec le modificateur tenu, ou elle passe a <c>0x3c000</c> — la marche rapide. Elle est
    /// ensuite mise a l'echelle par les memes trois drapeaux de <c>bloc + 0x140</c> que le membre
    /// suivi utilise pour sa distance d'arret.
    /// </para>
    /// </summary>
    private static void HandleDirectionalInput_0050E140(uint* slot, byte* perChar, byte* pad)
    {
        byte* s = (byte*)slot;
        int direction;

        if ((perChar[0x140] & 8) == 0)
        {
            direction = s_PadDirection_00562dc8[*pad >> 4];
        }
        else if ((*pad & 0xf0) == 0)
        {
            direction = 0;
        }
        else
        {
            direction = (CrtRand() & 7) + 1;
        }

        if (direction == 0)
        {
            if ((*slot & 0x47) == 0 && (*pad & 0xf0) == 0 && *(short*)(s + 0x18) != 0)
            {
                *(uint*)(s + 0x18) = 0;
                *slot |= 0x1000000;
            }

            return;
        }

        if ((*slot & 0x8000047) == 0 && *(short*)(s + 0x18) != 1)
        {
            *(uint*)(s + 0x18) = 1;
            *slot |= 0x1000000;
        }

        if ((pad[0x0b] & 2) != 0)
        {
            pad[0x0b] &= 0xfd;

            short reference = (perChar[0x140] & 8) == 0
                ? (short)((*(ushort*)(Mem.s_CameraVector_00604928 + 2) + 0x80u) & 0xf00)
                : (short)(((short)CrtRand() + 0x80u) & 0xf00);

            *(short*)(s + 0x32) = (short)((short)(direction * 0x200) - reference - 0x200);
        }

        *(short*)(s + 0x98) = *(short*)(s + 0x32);

        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            *(int*)(s + 0x5c) = (pad[1] & 0x20) == 0
                ? s_PostureSpeed_00562cc0[perChar[0x150] & 7] << 0xc
                : 0x3c000;
        }
        else
        {
            *(int*)(s + 0x5c) = s_PostureSpeed_00562cc0[perChar[0x150] & 7] << 0xc;
        }

        uint posture = *(uint*)(perChar + 0x140);
        int scale = 0x1000;
        if ((posture & 0x4000) != 0) scale = 0x1333;
        if ((posture & 0x8000) != 0) scale = 0xccc;
        if ((posture & 4) != 0) scale = 0x800;

        *(int*)(s + 0x5c) = scale * *(int*)(s + 0x5c) >> 0xc;
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_FUN_00562ca0)[bloc + 0x150 &amp; 7])(…)</c> — huit gestionnaires
    /// de posture, releves sous <see cref="s_PostureHandlers_00562ca0"/>. <b>Quatre sont le corps
    /// vide <c>0x0040A600</c></b>, deja verifie au desassembleur : les postures 1, 2, 3 et 5 ne
    /// font rien.
    /// </summary>
    private static void DispatchPostureHandler(
        byte posture, uint* slot, byte* entry, byte* perChar, byte* pad, byte* anim)
    {
        uint target = s_PostureHandlers_00562ca0[posture];

        switch (target)
        {
            case 0x0040A600: return;   // corps vide verifie au desassembleur
            case 0x0051A670: PostureHandler_0051A670(slot); return;

            default:
                throw new NotImplementedException(
                    $"FUN_0050bf20 : le gestionnaire de posture {posture} (table 0x00562ca0) vaut " +
                    $"0x{target:X6} et n'est pas transcrit. Les trois qui restent — 0x0051A6B0, " +
                    "0x0051A700 et 0x0051A710 — sont **absentes de Blaze.c** : " +
                    $"`OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x{target:x8} 512`. " +
                    "Le site d'appel empile (entite, entree d'emplacement, bloc par personnage, " +
                    "manette, animation courante), mais 0x0051A670 ne lit que le premier — " +
                    "verifier au cas par cas ce que chacune consomme reellement.");
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051a670 @ 0x0051a670 — la posture <b>0</b>, la marche : quatorze lignes.
    ///
    /// <para>
    /// Elle ne fait qu'une chose : a la <b>cinquieme image de l'etat 6</b>, elle fait naitre
    /// l'entite d'effet <c>0xba</c> — la trainee de l'arme, dont la table 0x005ff400 porte la
    /// duree de vie. Le test <c>param_1[6] == 0x50006</c> compare l'etat ET l'image en un seul
    /// dword, ce qui est la forme compacte de « etat 6, image 5 » : les deux <c>short</c> de
    /// <c>+0x18</c> et <c>+0x1a</c> lus ensemble.
    /// </para>
    /// <para>
    /// <b>Ghidra ne lui donne qu'un parametre</b> alors que le site d'appel en empile cinq. Ce
    /// n'est pas une signature manquee : le corps ne touche jamais aux quatre autres, et l'appel
    /// est en cdecl, donc sans consequence. Meme precedent que <c>FreezeAllEntities</c>.
    /// </para>
    /// </summary>
    private static void PostureHandler_0051A670(uint* slot)
    {
        byte* s = (byte*)slot;

        if ((*slot & 2) != 0 && *(uint*)(s + 0x18) == 0x50006)
        {
            SpawnEffectEntity(s, 0xba, 0, 0);
        }

        if ((*slot & 1) != 0) *slot &= 0xfffffffb;
    }

    /// <summary>
    /// Ghidra: FUN_00474eb0 @ 0x00474eb0 — <b>cherche la cible la plus proche du camp d'en
    /// face</b>. Dix-neuf lignes d'aiguillage : selon le mode et le bit <c>0x800000</c> qui
    /// distingue un membre du groupe, elle balaie soit la table des entites de scene, soit celle
    /// du groupe.
    ///
    /// <para>
    /// Avec le mode <c>1</c> — le seul que la carte du monde emploie — un membre du groupe cherche
    /// parmi les entites de scene, et une entite de scene parmi les membres du groupe. C'est donc
    /// bien « l'adversaire le plus proche » et non un obstacle quelconque : le nom
    /// <c>FindNearbyObstacle</c> que ce depot lui donnait d'apres son seul site d'appel etait une
    /// supposition, corrigee ici.
    /// </para>
    /// <para>
    /// <b>Ghidra la declare <c>void</c></b> alors que son appelant lit son resultat. Ce n'est pas
    /// une signature manquee : les deux branches sont des appels <b>terminaux</b>, et le resultat
    /// de l'appelee traverse tel quel. Rendu explicitement ici.
    /// </para>
    /// </summary>
    private static byte* FindNearestOpponent_00474EB0(uint* entity, uint mask, int mode)
    {
        if (mode == 0)
        {
            if ((*entity & 0x800000) == 0) return FindNearestSceneEntity_00474DB0(entity, mask);
        }
        else if ((*entity & 0x800000) != 0)
        {
            return FindNearestSceneEntity_00474DB0(entity, mask);
        }

        return FindNearestPartyMember_00474CD0(entity);
    }

    /// <summary>
    /// Ghidra: FUN_00474db0 @ 0x00474db0 — la plus proche des 0x20 entites de scene
    /// (<c>DAT_005ecaa0</c>) dans un rayon de <b>600 unites</b>, en excluant la demanderesse
    /// elle-meme.
    ///
    /// <para>
    /// Quatre conditions filtrent les candidates : active (<c>0x80000000</c>), sans aucun des bits
    /// <c>0x50002040</c>, sans cible deja tenue a <c>+0x74</c>, et sans compte a rebours
    /// d'invulnerabilite a <c>+0x96</c>.
    /// </para>
    /// <para>
    /// Le <paramref name="mask"/> est teste par <c>(*bloc &amp; ~mask) == 0</c> contre le mot de
    /// <c>+0x14</c> du bloc de statistiques. <b>Le seul appelant passe -1</b>, ce qui rend
    /// <c>~mask</c> nul et la condition toujours vraie : le filtre existe mais dort.
    /// </para>
    /// </summary>
    private static byte* FindNearestSceneEntity_00474DB0(uint* entity, uint mask)
    {
        byte* self = (byte*)entity;
        int x = *(int*)(self + 0x1c);
        int y = *(int*)(self + 0x20);
        int z = *(int*)(self + 0x24);

        int best = 600;
        byte* found = null;

        byte* candidate = Mem.Entity(0x005ecaa0);
        byte* stats = s_EntityStatBlocks_005ea560 + 0x14;

        for (int i = 0x20; i != 0; i--, candidate += 0x9c, stats += 0x120)
        {
            if ((*(uint*)candidate & 0x80000000) != 0
                && (*(uint*)candidate & 0x50002040) == 0
                && (*(uint*)stats & ~mask) == 0
                && *(int*)(candidate + 0x74) == 0
                && *(short*)(candidate + 0x96) == 0)
            {
                int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                int dy = (y - *(int*)(candidate + 0x20)) >> 0xc;
                int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (distance < best && self != candidate)
                {
                    best = distance;
                    found = candidate;
                }
            }
        }

        return found;
    }

    /// <summary>
    /// Ghidra: FUN_00474cd0 @ 0x00474cd0 — la jumelle de
    /// <see cref="FindNearestSceneEntity_00474DB0"/> sur la table du groupe
    /// (<c>DAT_005e93c0</c>), bornee a la taille reelle du groupe et sans le filtre de masque.
    /// Meme rayon de 600, memes quatre conditions, meme exclusion de soi.
    /// </summary>
    private static byte* FindNearestPartyMember_00474CD0(uint* entity)
    {
        byte* self = (byte*)entity;
        int best = 600;
        byte* found = null;

        int x = *(int*)(self + 0x1c);
        int y = *(int*)(self + 0x20);
        int z = *(int*)(self + 0x24);

        if (s_PartySize_005e9020 == 0) return null;

        byte* candidate = Inp.s_SlotFlags_005e93c0;

        for (int i = s_PartySize_005e9020; i != 0; i--, candidate += 0x9c)
        {
            if ((*(uint*)candidate & 0x80000000) != 0
                && (*(uint*)candidate & 0x50002040) == 0
                && *(int*)(candidate + 0x74) == 0
                && *(short*)(candidate + 0x96) == 0)
            {
                int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                int dy = (y - *(int*)(candidate + 0x20)) >> 0xc;
                int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (distance < best && self != candidate)
                {
                    best = distance;
                    found = candidate;
                }
            }
        }

        return found;
    }

    /// <summary>Ghidra: FUN_00519df0 @ 0x00519df0 (51 l.) — applique la vitesse a la position et
    /// met a jour les drapeaux de collision de <c>+0x90</c>.</summary>
    /// <summary>
    /// Ghidra: FUN_00519df0 @ 0x00519df0 — <b>la collision contre le voisinage</b> : un balayage
    /// 3x3 des cellules de la <b>premiere</b> grille spatiale (celle de <c>0x1c4000</c>, que
    /// <see cref="TickEntityTable_005fa5e0"/> alimente) autour de l'entite, qui soumet chaque
    /// occupant a <c>FUN_00534330</c>.
    ///
    /// <para>
    /// La constante <c>0x3880</c> de la pseudo-C est la meme que celle de
    /// <see cref="LinkIntoWorldGrid"/> : <c>0x3880 * 0x20 * 4 = 0x1c4000</c>. Le pas de
    /// <c>0x80</c> entre deux cellules avance d'une rangee (0x20 cellules de quatre octets).
    /// </para>
    /// <para>
    /// <b>Ghidra reutilise <c>param_1</c> comme compteur de boucle</b> apres l'avoir sauve dans
    /// <c>puVar3</c> — d'ou un corps ou le meme nom designe tantot l'entite, tantot le nombre 3.
    /// Les deux ont ici des noms distincts.
    /// </para>
    /// <para>
    /// Le porteur de <c>+0x78</c> est <b>remis a zero avant le balayage</b> et relu apres : s'il a
    /// change, la position du nouveau porteur est recopiee dans l'entree d'emplacement. C'est ce
    /// qui permet a <see cref="ComputeCarrierOffset"/> de travailler au tour suivant.
    /// </para>
    /// </summary>
    private static void ApplyEntityMotion_00519DF0(uint* entity, byte* entry)
    {
        byte* e = (byte*)entity;
        byte* mainMem = GetMainMemPtr();

        uint previousCarrier = *(uint*)(e + 0x78);
        *(uint*)(e + 0x78) = 0;

        uint cellX = (uint)((((*(int*)(e + 0x1c) >> 0xc) + 0x2000) >> 9) - 1);
        *entity &= 0xffffffcf;
        uint cellZ = (uint)((((*(int*)(e + 0x24) >> 0xc) + 0x2000) >> 9) - 1);

        for (int column = 3; column != 0; column--)
        {
            int offset = (int)((cellZ + 0x3880) * 0x20 + cellX) * 4;
            uint row = cellZ;

            for (int step = 3; step != 0; step--)
            {
                if (cellX < 0x20 && row < 0x20)
                {
                    for (uint* occupant = *(uint**)(mainMem + offset);
                         occupant != null;
                         occupant = *(uint**)((byte*)occupant + 0x68))
                    {
                        if ((*occupant & 0x2000) == 0)
                        {
                            ResolveEntityContact_00534330(occupant, entity, entry);
                        }
                    }
                }

                offset += 0x80;
                row++;
            }

            cellZ = row - 3;
            cellX++;
        }

        uint carrier = *(uint*)(e + 0x78);
        if (carrier != 0 && carrier != previousCarrier)
        {
            byte* c = (byte*)(nuint)carrier;
            *(uint*)(entry + 0x28) = *(uint*)(c + 0x1c);
            *(uint*)(entry + 0x2c) = *(uint*)(c + 0x20);
            *(uint*)(entry + 0x30) = *(uint*)(c + 0x24);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00534330 @ 0x00534330 — <b>le contact entre deux entites</b> : le personnage
    /// contre le maillage de collision d'un occupant voisin. Appelee par
    /// <see cref="ApplyEntityMotion_00519DF0"/> pour chaque occupant des neuf cellules de grille.
    /// C'est elle qui pose le porteur de <c>+0x78</c> et les bits de <c>+0x90</c>.
    ///
    /// <para>
    /// <b>2 464 octets</b> (0x00534330..0x00534cd0), 277 lignes de pseudo-C : une boucle sur les
    /// faces du maillage, avec par face un produit vectoriel dans les registres GTE emules puis
    /// jusqu'a trois <c>NormalClip</c> pour le test d'appartenance au triangle.
    /// </para>
    /// <para>
    /// <b>La pseudo-C ne peut pas etre transcrite telle quelle : elle confond les DEUX entites.</b>
    /// Ghidra declare <c>FUN_00534330(uint *param_1)</c> alors que le site d'appel en empile trois
    /// (<c>add esp,0Ch</c> en 0x00519e83), et il fait porter a un meme nom des acces qui visent des
    /// objets differents. La correspondance ci-dessous est <b>etablie au desassembleur</b> et
    /// suffit a rendre la pseudo-C utilisable :
    /// </para>
    /// <list type="table">
    /// <item><term><c>param_1</c>, <c>puStack_18</c></term><description>l'<b>argument 1</b>,
    /// l'occupant — <c>[esp+74h]</c> apres les quatre <c>push</c> du prologue. Les deux noms
    /// designent le meme objet : <c>puStack_18 = param_1</c> en tete de boucle et
    /// <c>param_1 = puStack_18</c> en queue.</description></item>
    /// <item><term><c>local_14</c> <i>vu comme une entite</i></term><description>l'<b>argument
    /// 2</b>, le personnage qui se deplace — <c>[esp+78h]</c>.</description></item>
    /// <item><term><c>local_14</c> <i>passe a FUN_0051c300 / FUN_005327b0 / Ordinal_366</i></term>
    /// <description>un <b>tampon local de neuf shorts</b> a <c>[esp+5Ch]</c>, la matrice de
    /// l'occupant. Rien a voir avec l'entite ci-dessus : Ghidra a fusionne deux emplacements de
    /// pile voisins.</description></item>
    /// <item><term><c>iStack_10</c></term><description>l'<b>argument 3</b>, l'entree
    /// d'emplacement — <c>[esp+7Ch]</c>. Elle recoit le vecteur de contact a <c>+0x1c</c> et la
    /// pose de l'occupant a <c>+0x28</c>..<c>+0x48</c>.</description></item>
    /// <item><term><c>puStack_54</c></term><description>le bloc de modele de l'occupant, mis de
    /// cote en tete et restaure a chaque tour de boucle.</description></item>
    /// </list>
    /// <para>
    /// <b>Le recoupement decisif est 0x00534778</b> : <c>mov [edi+78h],esi</c> puis
    /// <c>mov ecx,[esp+7Ch] ; mov [ecx+1Ch],edx</c> ou <c>edx = [edi+1Ch] - [esi+1Ch]</c>. EDI est
    /// l'argument 2 et ESI l'argument 1, donc <c>entite[0x78] = occupant</c> et
    /// <c>entree[0x1c] = entite.x - occupant.x</c> — ce que la pseudo-C rend en
    /// <c>local_14[0x1e] = puStack_18</c> et
    /// <c>*(uint *)(iStack_10 + 0x1c) = local_14[7] - puStack_18[7]</c>.
    /// </para>
    /// <para>
    /// <b>Le danger de cette fonction est le silence.</b> Intervertir les deux entites compile,
    /// tourne, et rend une collision qui pousse le decor au lieu du personnage. Aucun compteur de
    /// primitives ne le dirait. Verifier chaque acces contre la table ci-dessus avant de
    /// l'ecrire, et ne rien deduire du nom seul.
    /// </para>
    /// <para>
    /// Les six termes du <c>NormalClip</c> sont <b>inlines</b> ici et le depot en porte deja la
    /// version reconstruite (<c>FUN_0051c280</c>, <c>Platform/Winsys3d.cs</c>) : les recouper terme
    /// a terme avant d'appeler, Ghidra les emet dans un autre ordre — precedent deja paye sur
    /// <c>FinalizeEntityPlacement2</c>.
    /// </para>
    /// </summary>
    private static uint ResolveEntityContact_00534330(uint* occupantPtr, uint* entityPtr, byte* entry)
    {
        byte* occupant = (byte*)occupantPtr;
        byte* entity = (byte*)entityPtr;
        byte* mainMem = GetMainMemPtr();

        EnsureGteRegisters();

        // Le maillage de collision de l'occupant, choisi par son type et le decalage de zone.
        byte* model = *(byte**)(Mem.Entity(0x005f7bc0) + (occupant[4] + *Mem.Entity(0x00600838)) * 4);

        // La rangee de translation du registre MATRIX est mise a zero : on travaille en repere
        // local de l'occupant, la translation est faite a la main par les deltas ci-dessous.
        *(int*)(s_GteMatrix814 + 0x1c) = 0;
        *(int*)(s_GteMatrix814 + 0x18) = 0;
        *(int*)(s_GteMatrix814 + 0x14) = 0;

        short* matrix = stackalloc short[9];
        RotMatrix((short*)(occupant + 0x30), matrix);
        ScaleMatrix_005327B0(matrix, (ushort*)(occupant + 0x28));
        Winsys3d.Ordinal_366(matrix);

        // Les deux nappes de sommets transformes : les positions dans MainMem + 0x1b0000, les
        // normales dans + 0x1b8000. Le mot de tete du modele porte les deux comptes.
        uint counts = *(uint*)model;
        Winsys3d.Ordinal_792((short*)(model + 0x40), (short*)(mainMem + 0x1b0000), counts & 0xffff);
        Winsys3d.Ordinal_792(*(short**)(model + 0x24), (short*)(mainMem + 0x1b8000),
            (uint)((int)counts >> 0x10) & 0xffff);

        bool pushed = false;      // Ghidra: bVar6
        bool overFace = false;    // Ghidra: bVar5

        int occupantY = *(int*)(occupant + 0x20);
        int entityY = *(int*)(entity + 0x20);

        short* faceIndex = *(short**)(*(byte**)(model + 0x38) + 4);

        // **L'asymetrie de ces trois deltas est reelle**, verifiee au desassembleur
        // (0x005343ef..0x0053440a) : X et Z sont pris `entite - occupant`, mais Y
        // `occupant - entite`. Ce n'est pas un artefact de Ghidra, et ce n'est pas corrige.
        int deltaX = (*(int*)(entity + 0x1c) - *(int*)(occupant + 0x1c)) >> 0xc;
        int deltaZ = (*(int*)(entity + 0x24) - *(int*)(occupant + 0x24)) >> 0xc;

        *(uint*)s_GteSxy807 = ((uint)deltaZ << 0x10) | ((uint)deltaX & 0xffff);

        int* vec799 = (int*)s_GteVec799;
        int* vec800 = (int*)s_GteVec800;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        while (true)
        {
            short index = *faceIndex;

            if (index < 0)
            {
                if (!overFace) break;

                // L'entite repose sur une face de cet occupant sans le traverser.
                *(uint*)(occupant + 0x70) = (uint)(nuint)entity;
                if ((*entityPtr & 0x10000) == 0)
                {
                    *(uint*)(occupant + 0x6c) = (uint)(nuint)entity;
                    *occupantPtr |= 0x40;
                }

                if ((*entityPtr & 0x40012000) == 0)
                {
                    byte kind = (byte)(occupant[0x0d] & 0xf);
                    if (kind == 0 || (kind == 1 && (occupant[9] & 0xf) == 0))
                    {
                        RunEntityScript(s_Unknown_005e97cc, entity, *(ushort*)(occupant + 0x0e));
                    }
                }

                if (pushed)
                {
                    *(uint*)(entity + 0x90) |= 0x8000800;
                    *(int*)(occupant + 0x50) += *(int*)(entity + 0x50);
                    *(int*)(occupant + 0x58) += *(int*)(entity + 0x58);
                }

                occupant[9] |= (byte)(1 << (int)(*(uint*)(entity + 8) & 0x1f));
                return FinishContact(occupantPtr, entityPtr);
            }

            byte* face = *(byte**)(model + 0x28) + index * 0x10;
            uint corners = *(uint*)face;
            ushort attributes = *(ushort*)(face + 0x0c);
            uint* normal = (uint*)(mainMem + 0x1b8000 + face[4] * 8);

            byte* cache = mainMem + 0x1b0000;
            short* a = (short*)(cache + (corners & 0xff) * 8);
            short* b = (short*)(cache + ((corners >> 8) & 0xff) * 8);
            short* c = (short*)(cache + ((corners >> 0x10) & 0xff) * 8);

            // Le produit vectoriel des deux aretes de la face, depose dans MAC1..3 puis recopie
            // dans IR1..3 — la normale non normalisee.
            vec800[0] = b[0] - a[0];
            vec800[1] = b[1] - a[1];
            vec800[2] = b[2] - a[2];
            vec799[0] = c[0] - a[0];
            vec799[1] = c[1] - a[1];
            vec799[2] = c[2] - a[2];

            mac801[0] = vec799[2] * vec800[1] - vec799[1] * vec800[2];
            mac801[1] = vec800[2] * vec799[0] - vec799[2] * vec800[0];
            mac801[2] = vec799[1] * vec800[0] - vec800[1] * vec799[0];

            ir813[0] = (short)mac801[0];
            ir813[1] = (short)mac801[1];
            ir813[2] = (short)mac801[2];

            int normalY = mac801[1];
            int planar = (deltaZ - a[2]) * mac801[2] + (deltaX - a[0]) * mac801[0];
            int side = (((occupantY - entityY) >> 0xc) - a[1]) * normalY + planar;

            // De quel cote du plan de la face l'entite se trouve-t-elle ? Les faces marquees
            // 0x2000 sont a sens unique et laissent passer par en dessous.
            if ((normalY == 0 && side >= 0)
                || (normalY > 0 && side > 0)
                || (normalY < 0 && side > 0 && (attributes & 0x2000) != 0))
            {
                break;
            }

            if ((attributes & 0x2000) == 0)
            {
                // Le test d'appartenance, en projection sur le plan XZ : trois (ou quatre pour un
                // quad) NormalClip contre les aretes. Les six termes inlines par le compilateur
                // sont **terme a terme** ceux de Winsys3d.NormalClip, deja reconstruit — recoupes
                // avant d'appeler, comme FinalizeEntityPlacement2 l'a exige en aout.
                uint flatA = ((uint)a[2] << 0x10) | (ushort)a[0];
                uint flatB = ((uint)b[2] << 0x10) | (ushort)b[0];
                uint flatC = ((uint)c[2] << 0x10) | (ushort)c[0];

                *(uint*)s_GteSxy805 = flatA;
                *(uint*)s_GteSxy806 = flatB;

                if (NormalClip() >= 0)
                {
                    *(uint*)s_GteSxy805 = flatB;
                    *(uint*)s_GteSxy806 = flatC;

                    if (NormalClip() >= 0)
                    {
                        int last;

                        if ((attributes & 1) == 0)
                        {
                            *(uint*)s_GteSxy805 = flatC;
                            *(uint*)s_GteSxy806 = flatA;
                            last = NormalClip();
                        }
                        else
                        {
                            // Face a quatre sommets : le quatrieme est l'octet haut du mot de
                            // coins, et le test se fait en deux aretes de plus.
                            short* d = (short*)(cache + (corners >> 0x18) * 8);
                            uint flatD = ((uint)d[2] << 0x10) | (ushort)d[0];

                            *(uint*)s_GteSxy805 = flatC;
                            *(uint*)s_GteSxy806 = flatD;
                            if (NormalClip() < 0) goto nextFace;

                            *(uint*)s_GteSxy805 = flatD;
                            *(uint*)s_GteSxy806 = flatA;
                            last = NormalClip();
                        }

                        if (last >= 0)
                        {
                            // L'entite est au-dessus de cette face : on en deduit la hauteur du
                            // plan sous elle.
                            int height = normalY == 0
                                ? occupantY
                                : (planar / normalY - a[1]) * 0x1000 + occupantY;

                            int reach = -0x32000;
                            if (*(int*)(entity + 0x54) < 0) reach = *(int*)(entity + 0x54) - 0x32000;

                            if (height + reach <= entityY)
                            {
                                *occupantPtr |= 0x20;
                                *entityPtr |= 0x20;

                                if ((*(int*)(entity + 0x64) <= height && (attributes & 0x1000) == 0)
                                    || (*(uint*)(entity + 0x90) & 4) != 0)
                                {
                                    *(int*)(entity + 0x64) = height;
                                    *(uint*)(entity + 0x78) = (uint)(nuint)occupant;
                                    PublishContact(entry, entity, occupant, normal);
                                }

                                if (*(int*)(entity + 0x60) <= height
                                    || (*(uint*)(entity + 0x90) & 4) != 0)
                                {
                                    *(int*)(entity + 0x60) = height;
                                    *(uint*)(entity + 0x78) = (uint)(nuint)occupant;

                                    if ((*(uint*)(entity + 0x90) & 0x8800) == 0)
                                    {
                                        *(uint*)(entity + 0x90) = attributes;
                                    }

                                    if (entityY <= height)
                                    {
                                        // Contact franc : l'entite est posee dessus.
                                        (*(short*)(occupant + 0x96))++;
                                        *occupantPtr |= 0x10;
                                        *(int*)(entity + 0x54) = 0;
                                        *entityPtr |= 0x10;
                                        PublishContact(entry, entity, occupant, normal);

                                        uint slotIndex = *(uint*)(entity + 8);
                                        *(uint*)(occupant + 0x70) = (uint)(nuint)entity;
                                        occupant[9] |= (byte)((1 << (int)(slotIndex & 0x1f)) << 4);

                                        if ((*entityPtr & 0x10000) == 0)
                                        {
                                            *(uint*)(occupant + 0x6c) = (uint)(nuint)entity;
                                        }

                                        if ((*entityPtr & 0x40012000) == 0
                                            && (occupant[0x0d] & 0xf) == 4)
                                        {
                                            RunEntityScript(s_Unknown_005e97cc, entity,
                                                *(ushort*)(occupant + 0x0e));
                                        }

                                        return FinishContact(occupantPtr, entityPtr);
                                    }
                                }

                                return 0;
                            }

                            // Trop haut pour se poser : la hauteur est notee et l'entite sera
                            // poussee au tour suivant.
                            *(int*)(entry + 4) = height;
                            pushed = true;
                            overFace = true;
                        }
                    }
                }
            }
            else
            {
                overFace = true;
            }

        nextFace:
            faceIndex++;
        }

        // Ghidra: LAB_0053470b — aucune face ne porte l'entite.
        if ((*entityPtr & 0x40012000) == 0 && (occupant[0x0d] & 0xf) == 2)
        {
            RunEntityScript(s_Unknown_005e97cc, entity, *(ushort*)(occupant + 0x0e));
        }

        occupant[9] &= (byte)~(1 << (int)(*(uint*)(entity + 8) & 0x1f));
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>LAB_005348d5</c> — les deux lignes communes aux deux sorties « contact » de
    /// <see cref="ResolveEntityContact_00534330"/>.
    /// </summary>
    private static uint FinishContact(uint* occupantPtr, uint* entityPtr)
    {
        if ((*occupantPtr & 0x10000000) == 0 && (*entityPtr & 0x10000) == 0)
        {
            *occupantPtr &= 0xbfffffff;
        }

        return 1;
    }

    /// <summary>
    /// Le bloc de treize ecritures que <see cref="ResolveEntityContact_00534330"/> repete
    /// <b>a l'identique</b> a ses deux points de contact : le vecteur qui separe les deux entites,
    /// la pose complete de l'occupant, et les deux mots de normale de la face touchee.
    /// Factorise parce que c'est une unite reellement independante — le meme bloc, deux fois.
    /// </summary>
    private static void PublishContact(byte* entry, byte* entity, byte* occupant, uint* normal)
    {
        *(uint*)(entry + 0x1c) = (uint)(*(int*)(entity + 0x1c) - *(int*)(occupant + 0x1c));
        *(uint*)(entry + 0x20) = (uint)(*(int*)(entity + 0x20) - *(int*)(occupant + 0x20));
        *(uint*)(entry + 0x24) = (uint)(*(int*)(entity + 0x24) - *(int*)(occupant + 0x24));

        *(uint*)(entry + 0x28) = *(uint*)(occupant + 0x1c);
        *(uint*)(entry + 0x2c) = *(uint*)(occupant + 0x20);
        *(uint*)(entry + 0x30) = *(uint*)(occupant + 0x24);
        *(uint*)(entry + 0x34) = *(uint*)(occupant + 0x28);
        *(uint*)(entry + 0x38) = *(uint*)(occupant + 0x2c);
        *(uint*)(entry + 0x3c) = *(uint*)(occupant + 0x30);
        *(uint*)(entry + 0x40) = *(uint*)(occupant + 0x34);

        *(uint*)(entry + 0x44) = normal[0];
        *(uint*)(entry + 0x48) = normal[1];
    }

    /// <summary>
    /// Ghidra: FUN_005327b0 @ 0x005327b0 — le <c>ScaleMatrix</c> du SDK : chaque <b>colonne</b> de
    /// la matrice 3x3 est multipliee par un facteur en 1.12 pris dans un triplet de
    /// <c>ushort</c>.
    ///
    /// <para>
    /// L'original passe par les registres GTE emules — le facteur dans 812, la colonne dans
    /// IR1..3 (813), le produit dans MAC1..3 (801) — et non par des variables. Transcrit tel quel :
    /// ces registres sont observables depuis le reste du moteur, et rien ne garantit qu'aucun
    /// lecteur ne les regarde entre deux appels.
    /// </para>
    /// <para>Ce sont bien les <b>colonnes</b> et non les rangees : la fonction touche les indices
    /// 0/3/6, puis 1/4/7, puis 2/5/8.</para>
    /// </summary>
    private static void ScaleMatrix_005327B0(short* matrix, ushort* scale)
    {
        EnsureGteRegisters();

        ushort* factor = (ushort*)s_GteScale812;
        short* ir = (short*)s_GteIr813;
        int* mac = (int*)s_GteMac801;

        for (int column = 0; column < 3; column++)
        {
            *factor = scale[column];
            ir[0] = matrix[column];
            ir[1] = matrix[column + 3];
            ir[2] = matrix[column + 6];

            mac[0] = (int)(*factor * ir[0]) >> 0xc;
            ir[0] = (short)mac[0];
            mac[1] = (int)(ir[1] * *factor) >> 0xc;
            ir[1] = (short)mac[1];
            mac[2] = (int)(ir[2] * *factor) >> 0xc;
            ir[2] = (short)mac[2];

            matrix[column] = ir[0];
            matrix[column + 3] = ir[1];
            matrix[column + 6] = ir[2];
        }
    }

    /// <summary>
    /// Ghidra: FUN_00519f50 @ 0x00519f50 — <b>la laisse</b> : l'entite est ramenee vers le point
    /// que suit la camera des qu'elle s'en eloigne de plus de 400 unites, proportionnellement a
    /// l'exces. En deca, elle se contente de poser le bit <c>0x100</c>.
    ///
    /// <para>
    /// <b>Ghidra a fondu les deux parametres en un.</b> Il declare
    /// <c>void FUN_00519f50(uint *param_1)</c> et rend l'ecriture de la distance comme
    /// <c>*(undefined2 *)param_1 = …</c>, c'est-a-dire dans la moitie basse du mot d'etat de
    /// l'entite — ce qui ecraserait seize drapeaux. Le desassemblage dit autre chose :
    /// <c>0x00519f9b</c> lit <c>[esp+18h]</c>, soit <b>le second argument</b> apres les quatre
    /// <c>push</c> du prologue, et ecrit la distance dans <c>[ecx]</c>. La distance va donc dans
    /// l'<b>entree d'emplacement</b>, pas dans l'entite.
    /// </para>
    /// <para>
    /// C'est le cinquieme parametre perdu de cet arbre d'appel, et le plus dangereux : les quatre
    /// autres se voyaient parce que la pseudo-C lisait une variable jamais ecrite. Celui-ci
    /// <b>compile et tourne</b>, en corrompant seize drapeaux a chaque image.
    /// </para>
    /// </summary>
    private static void PullEntityTowardCameraTarget_00519F50(uint* entity, byte* entry)
    {
        byte* e = (byte*)entity;

        *entity &= 0xfffffeff;
        if ((*(uint*)(e + 0x90) & 0x8000) != 0) return;

        int dx = (*Mem.Camera(0x005de098) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*Mem.Camera(0x005de0a0) - *(int*)(e + 0x24)) >> 0xc;
        *(ushort*)entry = (ushort)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        dz = (*Mem.Camera(0x005de070) - *(int*)(e + 0x24)) >> 0xc;
        dx = (*Mem.Camera(0x005de068) - *(int*)(e + 0x1c)) >> 0xc;

        int angle = Winsys3d.Ordinal_457(dx, dz);
        int excess = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) - 400;

        uint flags = *entity;
        if (excess < 1)
        {
            if ((flags & 0x4000) == 0) *entity = flags | 0x100;
        }
        else if ((flags & 0x40000000) == 0)
        {
            *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(angle) * excess;
            *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(angle) * excess;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00519ee0 @ 0x00519ee0 — <b>le clignotement d'invulnerabilite</b>. Le compte a
    /// rebours de <c>+0x96</c> descend, et sa parite alterne entre le rendu normal (mode 1, blanc)
    /// et le rendu teinte (mode 5, orange <c>0x4080ff</c>). A zero, le bit <c>0x40</c> tombe.
    ///
    /// <para>
    /// La garde du haut fait retomber trois etats de deplacement (0, 1 et 4) sur l'etat 2 des que
    /// <c>0x40</c> est pose : un personnage touche ne marche plus, il encaisse.
    /// </para>
    /// <para>
    /// Comme <see cref="PostureHandler_0051A670"/>, Ghidra ne lui donne qu'un parametre alors que
    /// le site d'appel en empile deux ; le corps ne lit pas le second.
    /// </para>
    /// </summary>
    private static void UpdateEntityShadow_00519EE0(uint* entity, byte* perChar)
    {
        byte* e = (byte*)entity;

        if ((*entity & 0x40) != 0)
        {
            short state = *(short*)(e + 0x18);
            if (state == 0 || state == 1 || state == 4)
            {
                *(uint*)(e + 0x18) = 2;
                *entity &= 0xfefffff9;
            }
        }

        if (*(short*)(e + 0x96) != 0)
        {
            ushort remaining = (ushort)(*(short*)(e + 0x96) - 1);
            *(ushort*)(e + 0x96) = remaining;

            if ((remaining & 1) == 0)
            {
                *(ushort*)(e + 0x36) = 1;
                *(uint*)(e + 0x38) = 0xffffff;
            }
            else
            {
                *(ushort*)(e + 0x36) = 5;
                *(uint*)(e + 0x38) = 0x4080ff;
            }

            if (remaining == 0) *entity &= 0xffffffbf;
        }
    }

    /// <summary>Ghidra: FUN_005335e0 @ 0x005335e0 (78 l.) — l'angle de la pente sous une
    /// position.</summary>
    private static int GroundSlopeAngle_005335E0(uint* entity, int* position)
        => throw new NotImplementedException(
            "FUN_005335e0 (0x005335e0) : non transcrite. 78 lignes dans Blaze.c — l'angle de la " +
            "face du terrain sous une position, dont la sonde se sert pour recaler le cap contre " +
            "un mur.");

    /// <summary>
    /// Ghidra: <c>LAB_0050caf3</c>, 0x0050caf3..0x0050dc0d — <b>la queue commune</b> de
    /// <see cref="TickPartyMember"/> : tous les chemins y aboutissent. Environ 500 lignes de
    /// pseudo-C.
    /// </summary>
    private static void TickPartyMemberTail(
        int member, byte* s, uint* slot, byte* entry, byte* pad, byte* perChar, byte* mainMem)
    {
        // ---------------------------------------------- (A) la restitution d'etat
        uint held = *slot;
        uint attributes = *(uint*)(s + 0x90);

        if (((held & 0x40000001) != 0 || (attributes & 0xad40) != 0)
            && (s[0x90] & 2) == 0 && (held & 0x20000000) != 0)
        {
            if ((held & 0x4000) == 0) *slot = held | 0x4000;

            // Le membre reprend l'etat d'un camarade valide ; a defaut, celui que la sous-zone a
            // mis de cote en entrant. La boucle s'arrete AUSSI sur le premier camarade qui porte
            // 0x4001 sans le bit 0x8000 — auquel cas rien n'est restitue du tout.
            bool restored = false;
            int index = 0;
            uint* donor = (uint*)Inp.s_SlotFlags_005e93c0;

            while (index < s_PartySize_005e9020)
            {
                if (donor != slot && (*donor & 0x50000000) == 0)
                {
                    byte* d = (byte*)donor;
                    if ((*donor & 0x4001) == 0)
                    {
                        Mem.CopyDwords((uint*)(d + 0x7c), (uint*)(s + 0x7c), 7);
                        Mem.CopyDwords((uint*)(d + 0x1c), (uint*)(s + 0x1c), 3);
                        Mem.CopyDwords((uint*)(d + 0x60), (uint*)(s + 0x60), 2);
                        Mem.CopyDwords(
                            (uint*)(Inp.s_SlotArea_005e9180 + index * 0x4c + 0x1c),
                            (uint*)(entry + 0x1c), 0xc);

                        *(int*)(s + 0x78) = *(int*)(d + 0x78);
                        *slot = (*donor & 0xc2dedf77) | (*slot & 0x49212000);
                        *(uint*)(s + 0x90) = *(uint*)(d + 0x90) & 0xffff7eff;
                        restored = true;
                        break;
                    }

                    if ((*(uint*)(d + 0x90) & 0x8000) == 0) break;
                }

                index++;
                donor = (uint*)((byte*)donor + 0x9c);
            }

            if (!restored && index == s_PartySize_005e9020)
            {
                *Mem.Camera(0x005de068) = *(int*)Mem.Arena(0x005e9a30);
                *Mem.Camera(0x005de06c) = *(int*)Mem.Arena(0x005e9a34);
                *Mem.Camera(0x005de070) = *(int*)Mem.Arena(0x005e9a38);

                *(uint*)(s + 0x80) = *(uint*)Mem.Entity(0x005f7d70);
                *(uint*)(s + 0x84) = *(uint*)Mem.Entity(0x005f7d74);
                *(uint*)(s + 0x88) = *(uint*)Mem.Entity(0x005f7d78);
                *(uint*)(s + 0x8c) = *(uint*)Mem.Entity(0x005f7d7c);

                *(int*)(s + 0x1c) = *(int*)Mem.Arena(0x005e9a30);
                int savedY = *(int*)Mem.Arena(0x005e9a34);
                *(int*)(s + 0x20) = savedY;
                *(int*)(s + 0x24) = *(int*)Mem.Arena(0x005e9a38);

                // Ghidra pose la MEME valeur dans +0x60 et +0x64 : le sol et le plafond partent
                // tous deux de la hauteur mise de cote. Transcrit tel quel.
                *(int*)(s + 0x60) = savedY;
                *(int*)(s + 0x64) = savedY;

                *slot = (*(uint*)Mem.Entity(0x005ff3e4) & 0xc2dedf77) | (*slot & 0x49212000);
                *(uint*)(s + 0x90) = *(ushort*)Mem.Arena(0x005e9b38) & 0xffff7effu;
                *(uint*)(s + 0x7c) = *(uint*)Mem.Entity(0x005f7b48);
                restored = true;
            }

            if (restored)
            {
                held = *slot;
                *slot = held & 0xdfffbff8;
                *(uint*)(s + 0x90) &= 0xffff7eff;

                if ((held & 0x40000000) == 0)
                {
                    *(short*)(s + 0x96) = 0;
                    SetEntityPose_005397B0(slot, 2, 0);
                    *(short*)(s + 0x96) = 0x10;
                    *(short*)(s + 0x14) = 0;
                    *(uint*)(s + 0x5c) = 0;
                    *(int*)(s + 0x50) = 0;
                    *(int*)(s + 0x54) = 0;
                    *(int*)(s + 0x58) = 0;
                    s[9] = 0;
                    *(uint*)(s + 0x18) = 0;
                }
            }
        }

        // ---------------------------------------------- (B) la publication vers les globales
        Mem.CopyDwords((uint*)(s + 0x1c), (uint*)Mem.Entity(0x005fa4e0), 10);
        *(uint*)Mem.Arena(0x005e9b4c) = *(uint*)(s + 0x80);
        *(uint*)Mem.Arena(0x005e9b50) = *(uint*)(s + 0x84);
        *(uint*)Mem.Arena(0x005e9b44) = *(uint*)(s + 0x88);
        *(uint*)Mem.Arena(0x005e9b48) = *(uint*)(s + 0x8c);
        *(int*)Mem.Entity(0x005f7cc4) = *(int*)(s + 0x78);

        // Ghidra: `_DAT_005ff3e8 = *puVar3;`. L'adresse LUE plus haut est 0x005ff3e4 et celle
        // ECRITE ici est 0x005ff3e8 — deux cases voisines et distinctes, verifie au
        // desassembleur (`mov [5FF3E8h],eax` en 0x0050cd43).
        *(uint*)Mem.Entity(0x005ff3e8) = *slot;
        *(ushort*)Mem.Entity(0x0060083c) = *(ushort*)(s + 0x90);

        // ---------------------------------------------- (C) l'integration de la vitesse
        //
        // **Le triplet est (sinus, 0, cosinus), verifie au desassembleur** (0x0050cd63..0x0050cd7e)
        // et non deduit de la pseudo-C, qui attache les deux noms aux mauvais emplacements et rend
        // la mise a zero de Y comme un `iStack_10 = 0` mort.
        int facing = *(short*)(s + 0x98);
        int* heading = stackalloc int[3];
        heading[0] = (int)Winsys3d.GetSinTableEntry(facing);
        heading[1] = 0;
        heading[2] = Winsys3d.GetCosTableEntry(facing);

        int* carried = stackalloc int[3];

        if ((*slot & 1) == 0)
        {
            if ((*slot & 0x10) == 0)
            {
                // Le cap est bascule sur la pente : on sonde le sol un seizieme d'unite devant,
                // et Ordinal_439 normalise le vecteur qui joint les deux points.
                int* slope = stackalloc int[3];
                slope[0] = heading[0] * 0x10 + *(int*)(s + 0x1c);
                slope[2] = heading[2] * 0x10 + *(int*)(s + 0x24);

                // Ghidra appelle GroundHeightOnFace AVANT d'ecrire slope[1] : la hauteur passee est
                // celle laissee par le tour precedent. Conserve — la fonction ne lit que X et Z.
                int groundAhead = GroundHeightOnFace(*(uint**)(s + 0x7c), slope);

                slope[1] = ((groundAhead - *(int*)(s + 0x20)) >> 0xc) - 5;
                slope[0] = (heading[0] << 4) >> 0xc;
                slope[2] = (heading[2] << 4) >> 0xc;
                Winsys3d.Ordinal_439(slope, heading);
            }

            int speed = *(int*)(s + 0x5c);
            if ((*(uint*)(s + 0x90) & 0x100) == 0)
            {
                carried[0] = *(int*)(s + 0x50);
                carried[2] = *(int*)(s + 0x58);
                heading[0] = heading[0] * speed >> 0xc;
                heading[1] = heading[1] * speed >> 0xc;
                carried[1] = *(int*)(s + 0x54);
                heading[2] = heading[2] * speed >> 0xc;
            }
            else
            {
                // Le regime « monte » : cinq bits de plus a l'amortissement, et le vecteur de
                // l'entree d'emplacement s'ajoute au cap.
                heading[0] = heading[0] * speed >> 0x11;
                heading[1] = heading[1] * speed >> 0x11;
                heading[2] = heading[2] * speed >> 0x11;

                if ((*slot & 0x10) == 0)
                {
                    heading[0] += *(short*)(entry + 0x44) * 4;
                    heading[1] += *(short*)(entry + 0x46) * 4;
                    heading[2] += *(short*)(entry + 0x48) * 4;
                }

                carried[0] = *(int*)(s + 0x50) >> 5;
                carried[1] = *(int*)(s + 0x54) >> 5;
                carried[2] = *(int*)(s + 0x58) >> 5;
            }
        }
        else
        {
            heading[0] = heading[0] * *(int*)(s + 0x5c) >> 0x10;
            heading[2] = heading[2] * *(int*)(s + 0x5c) >> 0x10;
            carried[0] = *(int*)(s + 0x50) >> 4;
            carried[2] = *(int*)(s + 0x58) >> 4;
            carried[1] = 0;
        }

        // ---------------------------------------------- (D) le porteur
        int* push = stackalloc int[3];
        push[0] = 0;
        push[1] = 0;
        push[2] = 0;

        byte* carrier = *(byte**)(s + 0x78);
        if (carrier != null)
        {
            if ((carrier[7] & 0x40) != 0)
            {
                *(short*)(s + 0x32) += (short)(*(short*)(carrier + 0x32) - *(short*)(entry + 0x3e));
                ComputeCarrierOffset(entry, carrier, push);

                if (((*(ushort*)(carrier + 0x34) | *(ushort*)(entry + 0x40)) & 0xfff) != 0)
                {
                    push[0] = -push[0];
                    push[1] = -0xa000 - push[1];
                    push[2] = -push[2];
                }
            }

            if ((carrier[7] & 0x20) != 0)
            {
                push[0] += *(int*)(carrier + 0x1c) - *(int*)(entry + 0x28);
                push[1] += *(int*)(carrier + 0x20) - *(int*)(entry + 0x2c);
                push[2] += *(int*)(carrier + 0x24) - *(int*)(entry + 0x30);
            }

            if ((carrier[7] & 0x10) != 0)
            {
                push[0] += *(int*)(carrier + 0x50);
                push[1] += *(int*)(carrier + 0x54);
                push[2] += *(int*)(carrier + 0x58);
            }

            if ((*slot & 1) == 0 && (*slot & 0x10) != 0)
            {
                *(int*)(s + 0x50) += push[0];
                *(int*)(s + 0x54) += push[1];
                *(int*)(s + 0x58) += push[2];
            }
        }

        // ---------------------------------------------- (E) deux demi-pas de placement
        *(int*)(s + 0x50) += heading[0] - carried[0];
        *(int*)(s + 0x54) += heading[1] - carried[1];
        *(int*)(s + 0x58) += heading[2] - carried[2];

        PullEntityTowardCameraTarget_00519F50(slot, entry);

        uint gathered = 0;
        *slot &= 0xdfffffff;

        // Deux demi-pas plutot qu'un pas entier : le placement rattrape une marche que le pas
        // entier aurait traversee. Le premier tour efface les trois bits de face du resultat.
        for (int half = 2; half != 0; half--)
        {
            *(int*)(s + 0x1c) += *(int*)(s + 0x50) >> 1;
            *(int*)(s + 0x20) += *(int*)(s + 0x54) >> 1;
            *(int*)(s + 0x24) += *(int*)(s + 0x58) >> 1;

            FinalizeEntityPlacement2(slot);
            gathered |= *(uint*)(s + 0x90);
            if (half == 2) gathered &= 0xfffff8ff;
        }

        *(uint*)(s + 0x90) = gathered;
        ApplyEntityMotion_00519DF0(slot, entry);

        // L'entree dans une zone marquee : le script d'ambiance part une fois.
        if ((*(ushort*)Mem.Entity(0x0060083c) & 0x8000) == 0
            && (*(uint*)(s + 0x90) & 0x8000) != 0
            && *(sbyte*)(s_Unknown_005f2ce0 + 0x11) != -1)
        {
            RunEntityScript(s_Unknown_005e97cc, s, (uint)*(sbyte*)(s_Unknown_005f2ce0 + 0x11));
        }

        // ---------------------------------------------- (F) l'amortissement et le rebond
        short gravity = *(short*)(s + 0x9a);
        int vx = *(int*)(s + 0x50) - (*(int*)(s + 0x50) >> 8);
        *(int*)(s + 0x50) = vx;

        int vy = *(int*)(s + 0x54) + (((gravity >> 8) - (*(int*)(s + 0x54) >> 8)) - gravity);
        int vz = *(int*)(s + 0x58) - (*(int*)(s + 0x58) >> 8);
        *(int*)(s + 0x54) = vy;
        *(int*)(s + 0x58) = vz;

        if (vy < 1) *(short*)(s + 0x9a) = 0x6000;

        attributes = *(uint*)(s + 0x90);
        if ((attributes & 0x800) != 0)
        {
            // Le placement a refuse : on revient a la position d'avant, publiee en (B).
            *(int*)(s + 0x1c) = *(int*)Mem.Entity(0x005fa4e0);
            *(int*)(s + 0x24) = *(int*)Mem.Entity(0x005fa4e8);

            uint after = *slot;
            if ((after & 1) == 0) *(int*)(s + 0x20) = *(int*)Mem.Entity(0x005fa4e4);

            *(uint*)(s + 0x80) = *(uint*)Mem.Arena(0x005e9b4c);
            *(uint*)(s + 0x84) = *(uint*)Mem.Arena(0x005e9b50);
            *(uint*)(s + 0x88) = *(uint*)Mem.Arena(0x005e9b44);
            *(uint*)(s + 0x8c) = *(uint*)Mem.Arena(0x005e9b48);
            *(int*)(s + 0x78) = *(int*)Mem.Entity(0x005f7cc4);

            if ((perChar[0x150] & 7) == 2)
            {
                if ((after & 4) == 0)
                {
                    *(int*)(s + 0x50) = 0;
                    *(int*)(s + 0x58) = 0;
                }
                else
                {
                    // Le rebond contre un mur en pleine course : la vitesse s'inverse de moitie et
                    // le personnage est relance vers le haut.
                    *(short*)(s + 0x9a) = 0x6000;
                    *(uint*)(s + 0x18) = 4;
                    *slot = (after & 0xfefffffb) | 1;
                    *(int*)(s + 0x50) = -(vx >> 1);
                    *(int*)(s + 0x54) = 0xe000;
                    *(int*)(s + 0x20) += 0xe000;
                    *(int*)(s + 0x58) = -(vz >> 1);
                    entry[0x0a] = 0;
                }
            }
            else
            {
                *(int*)(s + 0x50) = 0;
                *(int*)(s + 0x58) = 0;
            }

            if ((*(uint*)(s + 0x90) & 0x10000000) == 0)
            {
                byte* holder = *(byte**)Mem.Entity(0x005f7cc4);
                if (holder != null)
                {
                    *(int*)(s + 0x1c) += push[0];
                    *(int*)(s + 0x24) += push[2];

                    if ((*slot & 1) == 0)
                    {
                        if ((*slot & 0x20) != 0)
                        {
                            int lifted = push[1] + *(int*)(s + 0x20);
                            *(int*)(s + 0x20) = Math.Max(lifted, *(int*)(s + 0x60));
                        }
                    }
                    else
                    {
                        *(int*)(s + 0x50) = push[0];
                        *(int*)(s + 0x58) = push[2];

                        int drop = *(int*)(holder + 0x20) - *(int*)(s + 0x20);
                        if (drop < -0xc7fff)
                        {
                            *(int*)(s + 0x54) = drop >> 4;
                            *(int*)(s + 0x20) += drop >> 4;
                        }
                    }
                }
            }
            else if ((*(uint*)(s + 0x90) & 0x40000000) != 0 && (*slot & 1) == 0)
            {
                *(int*)(s + 0x54) = 0;
            }

            FinalizeEntityPlacement2(slot);
            ApplyEntityMotion_00519DF0(slot, entry);

            attributes = *(uint*)(s + 0x90);
            if ((attributes & 0x800) != 0)
            {
                if ((attributes & 0x10000000) == 0)
                {
                    byte* target = *(byte**)(s + 0x78);
                    if (target != null)
                    {
                        // Toujours refuse : on glisse d'un seizieme vers le porteur plutot que de
                        // rester bloque.
                        int slideX = *(int*)(target + 0x1c) - *(int*)(s + 0x1c);
                        int slideZ = *(int*)(target + 0x24) - *(int*)(s + 0x24);
                        *(int*)(s + 0x1c) += slideX >> 4;
                        *(int*)(s + 0x24) += slideZ >> 4;

                        if ((*slot & 1) == 0)
                        {
                            if ((*slot & 0x20) != 0) *(int*)(s + 0x20) = *(int*)(s + 0x60);
                        }
                        else
                        {
                            *(int*)(s + 0x50) += slideX >> 4;
                            *(int*)(s + 0x58) += slideZ >> 4;
                        }
                    }
                }
                else if ((attributes & 0x40000000) != 0 && (*slot & 1) == 0)
                {
                    *(int*)(s + 0x54) = 0;
                }
            }
        }

        // ---------------------------------------------- (G) le sol et le plafond
        if (*(int*)(s + 0x64) < *(int*)(s + 0x20) || (attributes & 0xa000) != 0)
        {
            uint after = *slot;
            if ((after & 1) == 0)
            {
                *(short*)(s + 0x9a) = 0x6000;
                *slot = (after & 0xfffffffb) | 1;
                if ((after & 0x40000002) == 0)
                {
                    *(uint*)(s + 0x18) = 4;
                    *slot = (after & 0xfefffffb) | 1;
                }
            }
        }
        else
        {
            // Pose au sol : la matiere de la face devient celle des pas, et le contact peut
            // declencher un son et une pose.
            byte* material = Mem.Entity(0x005fa508) + *(ushort*)(*(byte**)(s + 0x7c) + 8) * 8;
            *(uint*)(entry + 0x44) = *(uint*)material;
            *(uint*)(entry + 0x48) = *(uint*)(material + 4);

            int ground = *(int*)(s + 0x64);
            if (*(int*)(s + 0x20) + 0xa000 < ground) *(int*)(s + 0x54) = 0;
            *(int*)(s + 0x20) = ground;

            DispatchEntityHandler(
                Res.ResolveEntityHandler(0x0056afc8, 32), s,
                $"FUN_0050bf20, membre {member} : le crochet de contact au sol PTR_FUN_0056b048");

            if ((*(uint*)(s + 0x90) & 0x400) != 0
                && *(short*)(s + 0x96) == 0
                && (perChar[0x150] & 7) != 7
                && (*(uint*)(perChar + 0x140) & 0x40400000) == 0)
            {
                Aud.PlayPositionalSound(5, 0xa00, 0x1800, (int*)(s + 0x1c));
                SetEntityPose_005397B0(slot, 2, 0);
                pad[0x0c] = 0;
                *(short*)(s + 0x96) = 0x10;
            }

            if ((*slot & 1) != 0)
            {
                if (*(int*)(s + 0x54) < -0x18000)
                {
                    Aud.PlayPositionalSound(1, 0x400, 0x1800, (int*)(s + 0x1c));
                }

                uint landing = *slot;
                if ((landing & 0x40000042) == 0)
                {
                    *(uint*)(s + 0x18) = 0;
                    *slot = landing | 0x1000000;
                }

                *slot &= 0xfffffffe;
            }

            entry[0x0a] = 2;
        }

        // ---------------------------------------------- (H) la cascade de fin
        held = *slot;
        if ((held & 0x40000000) != 0)
        {
            *(short*)(s + 0x96) = 0;
            return;
        }

        // L'instantane que la sous-zone relira si ce membre doit etre restitue.
        if ((held & 0x40000001) == 0
            && ((*(uint*)(s + 0x90) & 0xad40) == 0 || (*(uint*)(s + 0x90) & 2) != 0)
            && (*(int*)(s + 0x78) == 0 || (held & 0x20) == 0))
        {
            *(uint*)Mem.Entity(0x005f7d70) = *(uint*)(s + 0x80);
            *(uint*)Mem.Entity(0x005f7d74) = *(uint*)(s + 0x84);
            *(uint*)Mem.Entity(0x005f7d78) = *(uint*)(s + 0x88);
            *(uint*)Mem.Entity(0x005f7d7c) = *(uint*)(s + 0x8c);
            *(int*)Mem.Arena(0x005e9a30) = *(int*)(s + 0x1c);
            *(int*)Mem.Arena(0x005e9a34) = *(int*)(s + 0x20);
            *(int*)Mem.Arena(0x005e9a38) = *(int*)(s + 0x24);
            *(uint*)Mem.Entity(0x005ff3e4) = *slot;
            *(short*)Mem.Arena(0x005e9b38) = *(short*)(s + 0x90);
            *(uint*)Mem.Entity(0x005f7b48) = *(uint*)(s + 0x7c);
        }

        // Le coup recu : l'entite que la collision a designee, si elle est armee.
        byte* hit = CollectEntityHit_005197D0(slot);
        if (hit != null && (*(uint*)hit & 0x80080000) == 0x80080000)
        {
            uint kind = *(uint*)hit;
            if ((kind & 0x8000) == 0)
            {
                if ((kind & 0x10000000) == 0)
                {
                    // Degats repartis sur tout le groupe encore debout.
                    int index = 0;
                    int offset = 0;
                    uint* victim = (uint*)Inp.s_SlotFlags_005e93c0;
                    while (index < s_PartySize_005e9020)
                    {
                        if ((*victim & 0x80000000) != 0 && (*victim & 0x50000000) == 0)
                        {
                            int share = (int)(*(uint*)((byte*)hit + 0x5c)
                                              - *(ushort*)(mainMem + 0xf0144 + offset)) >> 1;
                            share += (int)*(uint*)((byte*)hit + 0x5c) >> 1;
                            if (share < 0) share = 0;

                            share += (int)(*(uint*)((byte*)hit + 0x5c) + 0xf) >> 4;
                            *(int*)(mainMem + 0xf0158 + offset) += share;

                            ApplyDamageToMember_00539190(victim, mainMem + 0xf0000 + offset);
                        }

                        index++;
                        offset += 0x2000;
                        victim = (uint*)((byte*)victim + 0x9c);
                    }
                }
                else
                {
                    uint purse = (uint)(*(int*)(perChar + 0x26c) + (int)(*(uint*)((byte*)hit + 0x5c) + 9) / 10);
                    *(uint*)(perChar + 0x26c) = purse;
                    if (purse > 99999) *(uint*)(perChar + 0x26c) = 99999;
                }

                *(uint*)hit &= 0x7fffffff;
                FreeEntitySlot(Mem.Entity(0x005f7b60), ((byte*)hit)[8]);
                Aud.PlayPositionalSound(7, 0x800, 0x1000, (int*)(s + 0x1c));
            }
            else if ((*slot & 0x10000) == 0)
            {
                if (((byte*)hit)[6] == 0)
                {
                    *(byte**)(s + 0x6c) = hit;
                    GrantChestContents_00530F20(slot, ((byte*)hit)[7], 0x005312d0);
                    Aud.PlayPositionalSound(0x21, 0x800, 0x1800, (int*)(s + 0x1c));
                }
                else
                {
                    byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
                    if ((perChar[0x150] & 7) == 2)
                    {
                        BindMenuLabels(*(byte**)(labels + 0x5a0), (uint)(nuint)slot,
                            s_WorldMapDescriptor_005633b0, 0);
                    }
                    else
                    {
                        BindMenuLabels(*(byte**)(labels + 0x550), (uint)(nuint)slot,
                            s_WorldMapDescriptor_005633b0, 0);
                        SpawnEffectEntity(s, ((byte*)hit)[6], 0, 0);
                    }

                    ((byte*)hit)[6] = 0;
                    if (*(short*)((byte*)hit + 0x10) < 0x32) *(short*)((byte*)hit + 0x10) = 0x32;
                }
            }
        }

        // Le pas sonore, sur la derniere image de l'animation de marche.
        if ((*slot & 2) != 0)
        {
            byte posture = perChar[0x150];
            if ((posture & 7) != 4 && (posture & 7) != 7)
            {
                byte* animSet = *(byte**)(Mem.Arena(0x005e9680) + s[4] * 4);
                byte* anim = *(byte**)(animSet + *(short*)(s + 0x18) * 4);

                if ((uint)(*(short*)(s + 0x1a) + 1) == *(ushort*)(anim + 0x16))
                {
                    uint row = posture;
                    if ((posture & 7) == 0 && *(short*)(s + 0x18) == 6) row = (row >> 3) + 0x10;

                    EmitFootstep_00519940(slot, (int*)(s + 0x1c), FootstepParameters(row));
                }
            }
        }

        UpdateEntityShadow_00519EE0(slot, perChar);

        if ((*(uint*)(s + 0x90) & 0x8000) == 0) TestTriggerVolumes_00534CD0(slot);

        // Le changement de sous-zone : la face foulee porte l'identifiant de la zone voisine.
        byte destination = (*(byte**)(s + 0x7c))[0x0f];
        if (destination != 0
            && (uint)*Mem.Camera(0x005de058) != destination - 1u
            && (*(uint*)(s + 0x90) & 0x2000) == 0)
        {
            uint before = *slot;
            *(short*)(s + 0x98) = *(short*)(s + 0x32);
            *slot = (before & 0xf7fffdff) | 0x2000;
            (*(ushort*)(s + 0x12))++;

            Res.s_Unknown_005de528 = s_Unknown_00566c84;
            s_Unknown_005de52c = s_Unknown_00566c88;
            s_Unknown_005de53c = s[8];
            *(byte*)Mem.Camera(0x005de038) = (byte)(destination - 1);
            s_WorldMapStep_005645f8 = 1;
        }

        // L'immobilite : deux comptes a rebours descendent tant que rien ne bouge, et repartent au
        // maximum des qu'une touche ou une vitesse apparait.
        if (*(short*)(s + 0x32) == *(short*)Mem.Entity(0x005fa4f6)
            && *(int*)(s + 0x5c) == 0
            && *(short*)(s + 0x96) == 0
            && (*slot & 6) == 0
            && (*pad & 0xf0) == 0)
        {
            if (perChar[0x206] != 0) perChar[0x206]--;

            if (s[8] == (byte)Mem.s_Unknown_005de538 && *(ushort*)Mem.Arena(0x005e9ae6) != 0)
            {
                (*(ushort*)Mem.Arena(0x005e9ae6))--;
            }
        }
        else
        {
            perChar[0x206] = 100;
            if (s[8] == (byte)Mem.s_Unknown_005de538) *(ushort*)Mem.Arena(0x005e9ae6) = 300;
        }

        if (s_Unknown_00566c84 != 0 && *(ushort*)(s + 0x12) == 0)
        {
            AdvanceSceneEntityAnimation_0053aa10(s);
        }

        *slot |= 0xe00;
    }

    /// <summary>
    /// Ghidra: 0x0050cfb1..0x0050d290 — <b>l'offset impose par le porteur</b>, quand un membre du
    /// groupe est transporte par une autre entite.
    ///
    /// <para>
    /// Deux produits matrice-vecteur en 64 bits, decalage <c>&gt;&gt; 12</c> chacun. La premiere matrice
    /// est la <b>transposee</b> de celle du repere de l'emplacement : le code la construit avec
    /// <c>RotMatrix</c> puis la retourne a la main dans l'ordre 0,3,6 / 1,4,7 / 2,5,8. La seconde
    /// est celle du porteur. Autrement dit : exprimer la position dans le repere local de
    /// l'emplacement, puis la re-exprimer dans celui de ce qui le porte — un attachement
    /// pere-fils. Le resultat est la difference entre la position ainsi transportee et la position
    /// d'origine.
    /// </para>
    /// <para>
    /// <b>Ghidra brouille l'ordre des operandes dans chaque triplet</b>, ce qui donne l'illusion
    /// que les composantes sont permutees entre les deux passes. Elles ne le sont pas : la somme
    /// est commutative, et ce qui compte est quel coefficient va avec quel emplacement de pile. En
    /// suivant les <c>[esp+N]</c> a travers les <c>push</c>, les deux passes sont le meme
    /// <c>ligne_i = (m[3i]*x + m[3i+1]*y + m[3i+2]*z) &gt;&gt; 12</c>.
    /// </para>
    /// </summary>
    private static void ComputeCarrierOffset(byte* entry, byte* carrier, int* offset)
    {
        EnsureGteRegisters();

        short* work = (short*)Mem.Arena(0x005e9b20);
        short* transposed = (short*)Mem.Arena(0x005e9b00);

        RotMatrix((short*)(entry + 0x3c), work);

        transposed[0] = work[0]; transposed[1] = work[3]; transposed[2] = work[6];
        transposed[3] = work[1]; transposed[4] = work[4]; transposed[5] = work[7];
        transposed[6] = work[2]; transposed[7] = work[5]; transposed[8] = work[8];

        RotMatrix((short*)(carrier + 0x30), work);

        int* local = stackalloc int[3];
        int* world = stackalloc int[3];

        Winsys3d.Ordinal_366(transposed);
        ApplyGteMatrix((int*)(entry + 0x1c), local);

        Winsys3d.Ordinal_366(work);
        ApplyGteMatrix(local, world);

        offset[0] = world[0] - *(int*)(entry + 0x1c);
        offset[1] = world[1] - *(int*)(entry + 0x20);
        offset[2] = world[2] - *(int*)(entry + 0x24);
    }

    /// <summary>
    /// Le produit matrice-vecteur que le binaire ecrit en <c>__allmul</c>/<c>__allshr</c> : neuf
    /// multiplications 64 bits et un decalage de 12. La matrice est celle que le dernier
    /// <c>Ordinal_366</c> a chargee dans le registre MATRIX de la DLL (export 814).
    /// </summary>
    private static void ApplyGteMatrix(int* vector, int* result)
    {
        short* m = (short*)s_GteMatrix814;

        for (int row = 0; row < 3; row++)
        {
            long sum = (long)m[row * 3] * vector[0]
                     + (long)m[row * 3 + 1] * vector[1]
                     + (long)m[row * 3 + 2] * vector[2];
            result[row] = (int)(sum >> 12);
        }
    }

    /// <summary>Ghidra: FUN_005397b0 @ 0x005397b0 (37 l.) — pose une pose d'entite.</summary>
    private static void SetEntityPose_005397B0(uint* entity, int pose, int mode)
        => throw new NotImplementedException(
            $"FUN_005397b0 (0x005397b0), pose {pose} : non transcrite. 37 lignes dans Blaze.c.");


    /// <summary>
    /// Ghidra: FUN_005197d0 @ 0x005197d0 — <b>la collecte du coup recu</b> : un balayage 3x3 des
    /// cellules de la seconde grille spatiale autour du personnage, qui rend la premiere entite
    /// armee (bit <c>0x80000</c>) assez proche pour toucher, ou zero.
    ///
    /// <para>
    /// « Assez proche » se lit en deux temps : moins de 0x28 unites au sol, et une difference de
    /// hauteur dans une bande de <c>±0x50</c> — le test <c>(dy + 0x50) &lt; 0xa0</c> en <b>non
    /// signe</b>, qui rejette les deux debordements d'un coup.
    /// </para>
    /// <para>
    /// <b>Chaque entite ne touche qu'une fois.</b> L'octet <c>+0x09</c> de la candidate porte un
    /// bit par emplacement de personnage : il est pose au moment du coup et n'est efface que
    /// lorsque la distance repasse au-dessus du seuil. Une entite qui reste collee ne blesse donc
    /// pas a chaque image.
    /// </para>
    /// <para>
    /// <b>La constante de grille est <c>0x389f</c> et non <c>0x38a0</c></b> : le <c>-1</c> du
    /// balayage est replie dedans, la ou <see cref="ApplyEntityMotion_00519DF0"/> l'ecrit a part.
    /// La consequence est reelle et conservee : pour un personnage dans la rangee 0, la premiere
    /// des trois rangees balayees est <c>0x389f</c>, c'est-a-dire <b>la derniere rangee de l'autre
    /// grille</b> — les deux pages etant adjacentes (0x1c4000 et 0x1c5000). Le garde
    /// <c>row &lt; 0x71800</c> ne borne que le haut.
    /// </para>
    /// </summary>
    private static byte* CollectEntityHit_005197D0(uint* entityPtr)
    {
        byte* entity = (byte*)entityPtr;
        byte* mainMem = GetMainMemPtr();

        int y = *(int*)(entity + 0x20);
        int x = *(int*)(entity + 0x1c);
        int z = *(int*)(entity + 0x24);

        uint cellX = (uint)(((((x >> 0xc) + 0x2000) >> 9) - 1));
        uint row = (uint)((((z >> 0xc) + 0x2000) >> 9) + 0x389f) * 0x20;

        byte mask = (byte)(1 << (entity[8] & 0x1f));

        for (int outer = 3; outer != 0; outer--)
        {
            uint column = cellX;

            for (int inner = 3; inner != 0; inner--)
            {
                if (column < 0x20 && row < 0x71800)
                {
                    for (byte* candidate = *(byte**)(mainMem + (column + row) * 4);
                         candidate != null;
                         candidate = *(byte**)(candidate + 0x68))
                    {
                        int candidateY = *(int*)(candidate + 0x20);
                        int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                        int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                        if ((*(uint*)candidate & 0x80000) == 0) continue;

                        if (distance < 0x28 && (uint)(((y - candidateY) >> 0xc) + 0x50) < 0xa0)
                        {
                            if ((candidate[9] & mask) == 0)
                            {
                                candidate[9] |= mask;
                                *Mem.Camera(0x005de0b8) = 1;
                                return candidate;
                            }
                        }
                        else
                        {
                            candidate[9] &= (byte)~mask;
                        }
                    }
                }

                column++;
            }

            cellX = column - 3;
            row += 0x20;
        }

        // Ghidra: `DAT_005de0b8 = local_14;` — le compteur de boucle, nul a la sortie.
        *Mem.Camera(0x005de0b8) = 0;
        return null;
    }

    /// <summary>Ghidra: FUN_00539190 @ 0x00539190 (20 l.) — applique des degats a un membre.</summary>
    private static void ApplyDamageToMember_00539190(uint* victim, byte* block)
        => throw new NotImplementedException(
            "FUN_00539190 (0x00539190) : non transcrite. 20 lignes dans Blaze.c.");

    /// <summary>Ghidra: FUN_00519940 @ 0x00519940 (62 l.) — le pas sonore et sa poussiere.</summary>
    private static void EmitFootstep_00519940(uint* entity, int* position, byte* parameters)
        => throw new NotImplementedException(
            "FUN_00519940 (0x00519940) : non transcrite. 62 lignes dans Blaze.c.");


    /// <summary>
    /// Ghidra: FUN_00534cd0 @ 0x00534cd0 — <b>les volumes de declenchement</b>. Le nom
    /// « eclairage » que ce depot lui donnait d'apres son seul site d'appel etait faux : elle
    /// parcourt la liste de morceaux en attente (<c>DAT_005fa560</c>, la meme que
    /// <see cref="ResetPendingChunkRecords"/>) et teste l'entite contre une suite de <b>boites
    /// alignees sur les axes</b>, chacune tenant dans un enregistrement de 0x10 octets.
    ///
    /// <para>
    /// L'empaquetage des boites est irregulier et vaut d'etre ecrit une fois : le mot 1 porte
    /// <c>{centre X, centre Y}</c>, le mot 2 <c>{centre Z, demi-etendue X}</c> et le mot 3
    /// <c>{demi-etendue Y, demi-etendue Z}</c>. Le mot 0 porte l'etat sur son octet bas, le type
    /// et ses drapeaux sur le suivant, et l'index de script sur sa moitie haute.
    /// </para>
    /// <para>
    /// Cinq types se distinguent par les quatre bits bas du second octet : <b>1</b> compte les
    /// passages (l'etat gagne 8 a la sortie), <b>2</b> lance son script en sortant, <b>3</b> le
    /// lance a la premiere sortie seulement, <b>8</b> change la couleur de fond et pose le bit 3
    /// du monde, <b>9</b> fait naitre l'effet 0x59 quand les points de vie ou de magie ont bouge
    /// depuis la derniere image. Tout le reste lance son script en <b>entrant</b>.
    /// </para>
    /// <para>
    /// Le test <c>(*entite &amp; 0xfc0000) == 0x800000</c> revient six fois : c'est « est-ce un
    /// membre du groupe », et il conditionne la plupart des effets. Une entite quelconque traverse
    /// donc la plupart des volumes sans rien declencher.
    /// </para>
    /// <para>
    /// Le <c>uVar10 = (int)uVar2 &gt;&gt; 0xc &amp; 0xffff</c> que Ghidra place en fin de boucle exterieure
    /// est un <b>no-op</b> : la valeur n'est jamais lue autrement qu'a travers un <c>(short)</c>.
    /// Conserve implicitement, pas transcrit en tant que tel.
    /// </para>
    /// </summary>
    private static void TestTriggerVolumes_00534CD0(uint* entityPtr)
    {
        if ((Mem.s_WorldFlags_005de02c & 0x10) != 0) return;

        byte* entity = (byte*)entityPtr;
        byte* pending = Mem.Entity(0x005fa560);

        short count = *(short*)pending;
        if (count == 0) return;

        int worldY = *(int*)(entity + 0x20);
        int worldX = *(int*)(entity + 0x1c);
        int worldZ = *(int*)(entity + 0x24);

        bool isPartyMember = (*entityPtr & 0xfc0000) == 0x800000;

        for (int slot = 1; slot <= count; slot++)
        {
            uint* record = *(uint**)(*(byte**)(s_Unknown_005e97cc + 0x88) + pending[slot] * 4);
            if (record == null) return;

            for (uint header = *record; header != 0xffffffff; record += 4, header = *record)
            {
                if (isPartyMember) *(byte*)record = (byte)(*(byte*)record + 1);

                byte kind = (byte)(header >> 8);
                bool inside = false;

                int centre = *(short*)((byte*)record + 4);
                int delta = Math.Abs((short)(worldX >> 0xc) - centre);

                if (delta < *(short*)((byte*)record + 0x0a))
                {
                    centre = *(short*)((byte*)record + 6);
                    delta = Math.Abs((short)(worldY >> 0xc) - centre);

                    if (delta < *(short*)((byte*)record + 0x0c))
                    {
                        centre = *(short*)((byte*)record + 8);
                        delta = Math.Abs((short)(worldZ >> 0xc) - centre);
                        inside = delta < *(short*)((byte*)record + 0x0e);
                    }
                }

                if (inside)
                {
                    byte type = (byte)(kind & 0xf);

                    if ((header & 0xf00) != 0
                        && (type != 1 || (header & 0x80) != 0 || !isPartyMember))
                    {
                        if (type == 3 && isPartyMember) *(byte*)record = (byte)(*(byte*)record + 8);

                        if (type == 8)
                        {
                            *(byte*)Mem.Camera(0x005de044) = (byte)(header >> 0x10);
                            Mem.s_WorldFlags_005de02c |= 8;
                            Mem.s_BackColourR_005e93a0 = 0xff;
                            Mem.s_BackColourG_005e93a2 = 0xff;
                            Mem.s_BackColourB_005e93a4 = 0xff;
                        }

                        if (kind == 9 && isPartyMember && (*entityPtr & 0x40000000) == 0)
                        {
                            byte* block = GetMainMemPtr() + 0xf0000 + (entity[8] * 0x2000);
                            if (*(short*)(block + 0x14c) != *(short*)(block + 0x148)
                                || *(short*)(block + 0x14e) != *(short*)(block + 0x14a))
                            {
                                *(short*)(block + 0x14c) = *(short*)(block + 0x148);
                                *(short*)(block + 0x14e) = *(short*)(block + 0x14a);
                                SpawnEffectEntity(entity, 0x59, 0, -1);
                            }
                        }

                        continue;
                    }

                    RunEntityScript(s_Unknown_005e97cc, entity, header >> 0x10);
                    *(byte*)record = (byte)(*(byte*)record | 0x80);
                }
                else
                {
                    byte type = (byte)(kind & 0xf);

                    if (type == 2 || (type == 3 && (header & 0x80) == 0 && isPartyMember))
                    {
                        RunEntityScript(s_Unknown_005e97cc, entity, header >> 0x10);
                        *(byte*)record = (byte)(*(byte*)record | 0x80);
                    }
                    else if (type == 1 && isPartyMember)
                    {
                        *(byte*)record = (byte)(*(byte*)record + 8);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00562cf0</c> — les jeux de parametres de pas sonore, <b>0xc octets (six
    /// shorts) par entree</b>, releves dans le binaire
    /// (<c>at Legacy/Binaries/Blaze.exe 0x00562cf0 384</c>).
    ///
    /// <para>
    /// <b>La table compte dix-huit entrees</b>, pas seize ni vingt : le dword de 0x00562dc8 est
    /// <c>0x02030100</c>, une suite d'octets croissants qui n'a plus la forme des six shorts. La
    /// borne se lit donc dans la donnee, pas dans les sites d'appel. Quatre entrees (4, 7, 12, 15)
    /// sont entierement nulles — les postures sans pas sonore.
    /// </para>
    /// <para>
    /// L'indice est <b>l'octet de posture entier</b>, pas ses trois bits bas ; seule l'entree 6 de
    /// la posture 0 est detournee vers <c>(posture &gt;&gt; 3) + 0x10</c>, ce qui donne exactement les
    /// deux dernieres entrees. Un indice hors des dix-huit lit, dans l'original, la structure
    /// suivante — la lacune le dit plutot que de rendre des octets inventes.
    /// </para>
    /// </summary>
    private static readonly short[] s_FootstepTable_00562cf0 =
    [
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xffa0), 0x006e,   //  0
        0x0000, 0x001e, 0x001e, 0x0096, 0x0100, 0x005a,                     //  1
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x0064,   //  2
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xff80), 0x005a,   //  3
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  4
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     //  5
        0x0000, 0x0032, 0x0050, 0x0096, unchecked((short)0xff40), 0x0050,   //  6
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  7
        0x0000, 0x001e, 0x0028, 0x0096, 0x0000, 0x0064,                     //  8
        0x0000, 0x001e, 0x0028, 0x0096, 0x0100, 0x006e,                     //  9
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x005a,   // 10
        0x0000, 0x001e, 0x0046, 0x0096, unchecked((short)0xff00), 0x005a,   // 11
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 12
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     // 13
        0x0000, 0x0028, 0x0050, 0x0096, unchecked((short)0xff00), 0x005a,   // 14
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 15
        0x0000, 0x0032, 0x0078, 0x0064, unchecked((short)0xfe80), 0x0082,   // 16
        0x0000, 0x0032, 0x006e, 0x0064, unchecked((short)0xfe80), 0x006e,   // 17
    ];

    private static short* s_FootstepStorage;

    /// <summary>L'entree de <see cref="s_FootstepTable_00562cf0"/> pour une posture, en memoire
    /// native puisque l'appele la recoit par pointeur.</summary>
    private static byte* FootstepParameters(uint row)
    {
        if (row >= 18)
        {
            throw new NotImplementedException(
                $"FUN_0050bf20 : parametres de pas sonore d'indice {row}, hors des dix-huit " +
                "entrees relevees a 0x00562cf0. Dans le binaire cette lecture tombe dans la " +
                "structure suivante (0x00562dc8) ; relever ce qu'elle porte avant d'aller plus " +
                $"loin : `OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{0x00562cf0 + row * 0xc:x8} 12`.");
        }

        if (s_FootstepStorage == null)
        {
            s_FootstepStorage = (short*)NativeMemory.Alloc((nuint)s_FootstepTable_00562cf0.Length, sizeof(short));
            for (int i = 0; i < s_FootstepTable_00562cf0.Length; i++)
            {
                s_FootstepStorage[i] = s_FootstepTable_00562cf0[i];
            }
        }

        return (byte*)s_FootstepStorage + row * 0xc;
    }
}
