// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE PAS PAR PERSONNAGE — `FUN_0050bf20` et sa queue.
//
// Ce qu'un membre du groupe fait a chaque image : lire son entree, avancer, sonder le sol devant
// lui, declencher ses objets equipes, et — pour un suiveur — tenir sa place derriere le meneur.
//
// Fichier recolle de DEUX plages (decision D7) : la tete et la queue de ce meme traitement
// etaient separees par vingt-six mille lignes, sous deux bandeaux sans rapport.
//
// Les postures et l'entree directionnelle sont dans `PostureAndInput.cs`, le mouvement lui-meme
// dans `Physics/MotionAndContact.cs`, la montee de niveau dans `Combat/Progression.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
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
    private static int s_Unknown_005de708;

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
            TriggerEquippedItems_005238E0(slot, 0x15);

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

        if (*(ushort*)(pad + 2) != 0)
        {
        }

        if ((*slot & 0x8000000) != 0)
        {
            TickQuickMenu_00530070(slot, entry, perChar, pad);
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
            TriggerEquippedItems_005238E0(slot, 1);
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
                        TriggerEquippedItems_005238E0(slot, 1);
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

    /// <summary>
    /// Ghidra: FUN_005238e0 @ 0x005238e0 — <b>l'evenement d'objet equipe</b> : elle balaie les cinq
    /// emplacements de <c>+0x2b0</c> et fait tourner le script <b>declenche</b> de chaque objet dont
    /// <c>item[0x84]</c> vaut <paramref name="ev"/>.
    ///
    /// <para>C'est la <b>seconde entree</b> de <see cref="RunItemScript_00523AA0"/>, et elle ne
    /// prend pas le meme script que la premiere : <c>FUN_00523a20</c> part de <c>item[0x82]</c> la
    /// ou <c>FUN_00523860</c> part de <c>item[0x80]</c>. Chaque bloc d'objet porte donc deux
    /// scripts — celui qu'on declenche en s'en servant, et celui qui reagit aux evenements.</para>
    ///
    /// <para><b>Trois protections encadrent l'appel</b>, et elles disent que ce script peut en
    /// declencher un autre :</para>
    /// <list type="bullet">
    /// <item><c>DAT_005e99c0</c> est un masque de cinq bits, un par emplacement : un objet deja
    /// entre ne se re-declenche pas. C'est l'appelant qui le remet a zero avant d'ouvrir une
    /// serie.</item>
    /// <item>la banque de variables de script (<c>0x005e9060</c>, 0x10 dwords) est empilee dans
    /// <c>0x005de170</c> au pas de 0x40, et depilee au retour.</item>
    /// <item>la cible <c>slot[0x70]</c> est empilee en parallele dans <c>0x005de2b8</c>.</item>
    /// </list>
    ///
    /// <para>La profondeur vit dans l'octet bas de <c>0x005de2b0</c> — et <b>ce compteur suit
    /// immediatement la pile qu'il indexe</b> : <c>0x005de170 + 5 * 0x40</c> vaut exactement
    /// <c>0x005de2b0</c>. Cinq places, cinq emplacements d'equipement ; la disposition de 1998 est
    /// juste au dernier octet pres, et c'est pourquoi la pile de sauvegarde et son compteur vivent
    /// dans la meme allocation ici.</para>
    ///
    /// <para>Le nom precedent, « SetEntityAnimationState », etait faux : rien dans ce corps ne
    /// touche a une animation.</para>
    /// </summary>
    private static void TriggerEquippedItems_005238E0(uint* slot, uint ev)
    {

        byte* record = GetMainMemPtr() + (((byte*)slot)[8] + 0x78) * 0x2000;

        byte* entered = Mem.Arena(0x005e99c0);
        byte* bank = Mem.Arena(0x005e9060);
        byte* saveStack = Mem.ItemMenuBytes(0x005de170);
        byte* depth = Mem.ItemMenuBytes(0x005de2b0);
        byte* targetStack = Mem.ItemMenuBytes(0x005de2b8);

        for (int i = 0; i < 5; i++)
        {
            if ((*entered & (byte)(1 << i)) != 0) continue;

            byte worn = record[0x2b0 + i];
            if (worn >= 10) continue;

            int block = worn * 0x280;
            if (*(ushort*)(record + block + 0x384) != ev) continue;

            // La premiere entree d'une serie repart d'une pile vide.
            if (*entered == 0) *depth = 0;

            Mem.CopyDwords((uint*)bank, (uint*)(saveStack + *depth * 0x40), 0x10);
            byte slotOfSave = *depth;
            (*depth)++;
            *(uint*)(targetStack + slotOfSave * 4) = *(uint*)((byte*)slot + 0x70);

            *entered |= (byte)(1 << i);

            RunTriggeredItemScript_00523A20(slot, record, record + block + 0x300);

            (*depth)--;
            Mem.CopyDwords((uint*)(saveStack + *depth * 0x40), (uint*)bank, 0x10);
            *(uint*)((byte*)slot + 0x70) = *(uint*)(targetStack + *depth * 4);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523a20 @ 0x00523a20 — le meme sas que <see cref="RunItemScriptSaved_00523860"/>
    /// autour de l'allocateur de <c>winsys3d</c>, mais sur le script <b>declenche</b> de
    /// <c>item[0x82]</c> et non sur celui de <c>item[0x80]</c>.
    ///
    /// <para>Ghidra y perd le meme parametre que dans son jumeau — sa seconde branche appelle
    /// <c>FUN_00523aa0(unaff_retaddr, param_1, param_2, …)</c>, decalee d'un cran. Trois arguments
    /// dans les deux branches.</para>
    /// </summary>
    private static void RunTriggeredItemScript_00523A20(uint* slot, byte* record, byte* item)
    {
        byte* script = item + *(ushort*)(item + 0x82);

        if (Winsys3d.Ordinal_46() < 0)
        {
            RunItemScript_00523AA0(slot, record, item, script);
            return;
        }

        *(int*)Mem.Arena(0x005e9318) = Winsys3d.Ordinal_46();
        Winsys3d.Ordinal_45(GetMainMemPtr() + 0x1c0000);
        RunItemScript_00523AA0(slot, record, item, script);
        Winsys3d.Ordinal_45((void*)(nint)(*(int*)Mem.Arena(0x005e9318)));
    }

    /// <summary>Ghidra: &amp;DAT_00564600 — <b>le nombre de sorts par école</b>, indexé par
    /// <c>personnage[0x2b5]</c>. Trois écoles seulement : la table de pointeurs
    /// <c>PTR_DAT_00564640</c> porte trois rangées puis un mot nul, et la rangée cumulée voisine
    /// (<c>DAT_00564608</c>, <c>00 1d 35 49</c>) est bien la somme courante de <c>1d 18 14</c>.
    /// Relevé verbatim dans <c>Legacy/Binaries/Blaze.exe</c>.
    ///
    /// <para>Ces comptes bornent le balayage de <see cref="TickQuickMenu_00530070"/> et l'index de
    /// bit dans <c>personnage + 0x160 + école * 8</c>, huit octets par école — soit 64 bits pour au
    /// plus 29 sorts.</para></summary>
    private static readonly byte* s_SpellCounts_00564600 = AllocBytes([
        0x1d, 0x18, 0x14, 0x01, 0x01, 0x01, 0x26, 0x00,
    ]);


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
                    ApplyPercentDamage_005397B0(slot, 2, 0);
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
            //
            // DAT_005fa508 est un POINTEUR vers la table des matieres (Blaze.c 63402 : il recoit
            // `*(chunk + 0x24)` au chargement de la sous-zone) — il faut le dereferencer. Lire
            // a l'adresse de la globale elle-meme donnait des poussees prises au hasard dans
            // l'arene : vent, sable et plateformes glissantes poussaient de travers (2026-09-25).
            byte* material = *(byte**)Mem.Entity(0x005fa508) + *(ushort*)(*(byte**)(s + 0x7c) + 8) * 8;
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
                ApplyPercentDamage_005397B0(slot, 2, 0);
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
                    // EXPERIENCE repartie sur tout le groupe encore debout — et non « degats »,
                    // comme ce commentaire le disait jusqu'au 2026-08-29 : la part s'ajoute a
                    // +0x158, la jauge que CheckLevelUp_00539190 examine juste apres.
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

                            CheckLevelUp_00539190((byte*)victim, mainMem + 0xf0000 + offset);
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

        // LE COUP, sur la derniere image du geste — et non « le pas sonore », comme ce commentaire
        // le disait jusqu'au 2026-08-29 : voir SweepSceneEntitiesForMeleeHit_00519940. Les deux
        // postures ecartees (4 et 7, modulo 8) sont exactement celles dont le gabarit de portee est
        // entierement nul dans la table de 0x00562cf0.
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

                    SweepSceneEntitiesForMeleeHit_00519940((byte*)slot, (int*)(s + 0x1c), MeleeProbeForPosture(row));
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
}
