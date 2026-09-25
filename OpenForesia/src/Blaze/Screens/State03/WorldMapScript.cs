// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// =================================================================================================
// L'INTERPRETEUR DE SCRIPT D'ENTITE (Ghidra: 0x00513dd0, 0x00513e40, et la table 0x00563298)
//
// Un script est une suite d'octets. Le premier octet de chaque instruction est un opcode qui indexe
// une table de 39 gestionnaires ; chaque gestionnaire lit ses operandes dans les octets suivants et
// rend le pointeur sur l'instruction suivante. L'opcode 0xff termine.
//
// POURQUOI IL EST ICI ET PAS DANS Engine/. Le binaire l'appelle depuis 17 fonctions reparties sur
// deux bandes d'adresses : c'est une primitive partagee. Mais la carte du monde est aujourd'hui son
// SEUL appelant reconstruit, et la regle 4 d'ARCHITECTURE.md dit qu'une fonction reste avec son
// appelant tant que ce n'est pas le cas. Il est dans un FICHIER a part plutot que dans WorldMap.cs
// parce qu'un interpreteur au milieu d'un ecran ne serait pas coherent - et la coherence est le
// critere, pas la taille.
//
// Le jour ou un second appelant est reconstruit, ce fichier part dans Engine/ avec ApplyScriptOp
// (qui vit encore dans WorldMap.cs, meme condition de deplacement ecrite sur place).
//
// -------------------------------------------------------------------------------------------
// DEUX ARGUMENTS PERDUS PAR GHIDRA, RETROUVES AU DESASSEMBLEUR
//
// Ghidra rend le second appel de FUN_00513dd0 comme `FUN_00513e40(unaff_retaddr, param_1, param_2)`
// - une liste DECALEE. Le code machine (0x00513e0e) empile `[esp+4], [esp+8], [esp+0Ch]`, donc
// `(param_1, param_2, param_3)` comme le premier appel. Transcrire la pseudo-C telle quelle aurait
// decale tous les parametres d'un cran, en silence.
//
// C'est la deuxieme fois le meme jour (voir aussi `piStack_74` dans FUN_00516fc0). **Dans cette
// famille de fonctions, verifier chaque appel au desassembleur avant de le transcrire.**
// -------------------------------------------------------------------------------------------
// =================================================================================================

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    // =====================================================================================
    // L'etat du panneau que les verbes 57 et 58 ouvrent.
    //
    // Ces onze globales appartiennent en propre a FUN_0053e0a0, le rendu du panneau, qui n'est pas
    // transcrite : c'est elle qui les lit toutes (Blaze.c 88018 et suivantes). Elles restent donc
    // ici, chez leur seul ecrivain transcrit, jusqu'a ce que ce corps arrive — regle de routage (4)
    // d'ARCHITECTURE.md.
    //
    // **Aucune n'est une vue dans une allocation existante**, verifie contre la carte des symboles
    // le 2026-08-25 : la plus proche en dessous de 0x005dff28 et 0x005dff5c est
    // s_Particles_005de824, qui s'arrete a 0x005dfba4 (0x20 particules de 0x9c) — 900 octets plus
    // bas. Voir REVERSE_STATUS.md, « suite 20 ».
    // =====================================================================================

    /// <summary>Ghidra: DAT_005dff28 — l'index de membre du groupe sur lequel porte le panneau.
    /// <c>FUN_0053e0a0</c> en derive quatre pointeurs des sa premiere ligne.</summary>
    private static byte s_Unknown_005dff28;

    /// <summary>Ghidra: DAT_005de548 — le <b>mode</b> du panneau. <c>FUN_0053e0a0</c> ne connait que
    /// 0 et 1 ; sur toute autre valeur elle repart immediatement. Le verbe 57 pose 0, le 58 pose 1 :
    /// c'est la seule difference de fond entre les deux.</summary>
    private static byte s_Unknown_005de548;

    /// <summary>Ghidra: DAT_005dff5c .. DAT_005dff7c — neuf dwords contigus, l'etat de curseur du
    /// panneau, remis a zero d'un bloc a chaque ouverture. Seul <c>DAT_005dff64</c> est traite a
    /// part, et par un seul des deux verbes.</summary>
    private static uint s_Unknown_005dff5c, s_Unknown_005dff60, s_Unknown_005dff64,
                        s_Unknown_005dff68, s_Unknown_005dff6c, s_Unknown_005dff70,
                        s_Unknown_005dff74, s_Unknown_005dff78, s_Unknown_005dff7c;

    /// <summary>Ghidra: DAT_0056dc90 — le sous-etat du panneau, <b>-1</b> a l'ouverture.
    /// <c>FUN_0053e0a0</c> teste son signe avant tout le reste.</summary>
    private static int s_Unknown_0056dc90;

    /// <summary>
    /// Ghidra: FUN_0053dea0 @ 0x0053dea0 — <b>le verbe 57 : ouvre le panneau en mode 0</b>. Absente
    /// de <c>Blaze.c</c> ; relevés au depot depuis le 2026-08-16
    /// (<c>Legacy/Raw/Blaze/FUN_0053dea0.asm</c> et <c>.c</c>).
    ///
    /// <para>Elle ne fait que <b>poser un etat et lever un drapeau</b> : le membre concerne, le
    /// mode, le bit <c>0x40</c> de <see cref="GameMemory.s_WorldFlags_005de02c"/>, et la remise a
    /// neuf des neuf dwords de curseur. Le travail visible arrive ailleurs — le bit <c>0x40</c> est
    /// ce que le pas de monde teste pour appeler le crochet <c>PTR_FUN_0056aff0</c>, lequel mene a
    /// <c>FUN_0053e0a0</c>.</para>
    ///
    /// <para><b>Le seul champ qui ne soit pas mis a zero</b> est <c>DAT_005dff64</c> : il recoit
    /// l'octet <c>+0x2bc</c> du bloc du personnage (<c>MainMem + membre * 0x2000 + 0xf02bc</c>),
    /// etendu en dword. Le verbe 58 y met zero a la place, et c'est la deuxieme des deux seules
    /// differences entre les jumeaux.</para>
    ///
    /// <para><b>Elle rend 0</b> — <c>xor eax,eax</c> en <c>0x0053dec1</c>, jamais retouche jusqu'au
    /// <c>ret</c>. Le resultat de condition est donc nul et le script continue.</para>
    /// </summary>
    private static uint AreaVerb_0053DEA0(byte* entity)
    {
        byte member = entity[8];

        s_Unknown_005de540 = member;
        s_Unknown_005dff28 = member;
        Mem.s_WorldFlags_005de02c |= 0x40;
        s_Unknown_005de548 = 0;

        s_Unknown_005dff5c = 0;
        s_Unknown_005dff60 = 0;
        s_Unknown_005dff68 = 0;

        // Ghidra: `mov bl,[edx+ecx+0xf02bc]` sur EBX mis a zero — un octet etendu en dword.
        s_Unknown_005dff64 = GetMainMemPtr()[member * 0x2000 + 0xf02bc];

        s_Unknown_005dff6c = 0;
        s_Unknown_005dff70 = 0;
        s_Unknown_005dff74 = 0;
        s_Unknown_0056dc90 = -1;
        s_Unknown_005dff78 = 0;
        s_Unknown_005dff7c = 0;

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053df20 @ 0x0053df20 — <b>le verbe 58 : le meme panneau en mode 1</b>. Jumelle
    /// exacte de <see cref="AreaVerb_0053DEA0"/> a deux differences pres, et pas une de plus :
    /// <c>DAT_005de548</c> vaut <b>1</b>, et <c>DAT_005dff64</c> est mis a <b>zero</b> au lieu de
    /// recevoir l'octet <c>+0x2bc</c> du personnage.
    ///
    /// <para>L'ordre des stores differe aussi, mais sans consequence : aucun de ces champs n'est lu
    /// entre-temps.</para>
    ///
    /// <para>Le binaire fait ici <c>or al,0x40</c> puis reecrit le dword entier, la ou son jumeau
    /// fait <c>or edx,0x40</c>. Meme effet — les trois octets hauts viennent de la meme lecture — et
    /// les deux sont rendus par le meme <c>|= 0x40</c>.</para>
    ///
    /// <para>Elle rend 0 elle aussi (<c>xor eax,eax</c> en <c>0x0053df49</c>).</para>
    /// </summary>
    private static uint AreaVerb_0053DF20(byte* entity)
    {
        byte member = entity[8];

        s_Unknown_005de540 = member;
        s_Unknown_005dff28 = member;
        s_Unknown_005de548 = 1;
        Mem.s_WorldFlags_005de02c |= 0x40;

        s_Unknown_005dff5c = 0;
        s_Unknown_005dff60 = 0;
        s_Unknown_005dff64 = 0;
        s_Unknown_005dff68 = 0;
        s_Unknown_005dff6c = 0;
        s_Unknown_005dff70 = 0;
        s_Unknown_005dff74 = 0;
        s_Unknown_0056dc90 = -1;
        s_Unknown_005dff78 = 0;
        s_Unknown_005dff7c = 0;

        return 0;
    }
    /// <summary>
    /// Ghidra: FUN_00513dd0 @ 0x00513dd0 — lance un script en basculant l'allocateur de la DLL sur
    /// le tampon de travail, si tant est qu'il y en ait un a sauvegarder.
    ///
    /// <para>
    /// <c>Ordinal_46</c> rend l'etat courant de l'allocateur ; negatif veut dire « rien a
    /// preserver », et le script tourne alors tel quel. Sinon l'etat est mis de cote dans
    /// <c>DAT_005e9318</c>, l'allocateur pointe sur <c>MainMem + 0x1c0000</c> le temps du script,
    /// puis il est restaure.
    /// </para>
    /// </summary>
    /// <summary>
    /// Le nom du dernier appelant de <see cref="RunEntityScript"/>, sous
    /// <c>OPENFORESIA_TRACE_SCRIPT=1</c>. <c>RunEntityScript</c> a une vingtaine de sites d'appel :
    /// savoir <b>lequel</b> a lance un script est ce qui separe « le joueur a parle a quelqu'un » de
    /// « une entite a marche sur une face ». Le nom est pris par
    /// <see cref="CallerMemberNameAttribute"/>, donc sans cout quand la trace est eteinte.
    /// </summary>
    private static string s_ScriptCaller = "?";

    internal static void RunEntityScript(byte* context, byte* entity, uint scriptIndex,
                                         [CallerMemberName] string caller = "")
    {
        if (s_TraceScript) s_ScriptCaller = caller;

        if (Winsys3d.Ordinal_46() < 0)
        {
            RunScript(context, entity, scriptIndex);
            return;
        }

        *(int*)Mem.Arena(0x005e9318) = Winsys3d.Ordinal_46();
        Winsys3d.Ordinal_45(GetMainMemPtr() + 0x1c0000);
        RunScript(context, entity, scriptIndex);
        Winsys3d.Ordinal_45((void*)(nint)(*(int*)Mem.Arena(0x005e9318)));
    }

    /// <summary>
    /// Ghidra: FUN_00513e40 @ 0x00513e40 — la boucle de l'interpreteur.
    ///
    /// <para>
    /// Le script est pris dans la table <c>context + 0x8c</c>, indexee par
    /// <paramref name="scriptIndex"/>. La boucle s'arrete sur l'opcode <c>0xff</c>, et aussi des
    /// qu'un gestionnaire pose un resultat non nul — **sauf pour l'opcode 2**, qui est le seul a
    /// continuer malgre un resultat pose. C'est le mecanisme de condition : un test rend 1, la
    /// boucle s'arrete, et l'opcode 2 est la branche qui reprend la main.
    /// </para>
    /// </summary>
    /// <summary>
    /// Trace de l'interpreteur, sous <c>OPENFORESIA_TRACE_SCRIPT=1</c>
    /// (<c>playtest.ps1 -Trace SCRIPT</c>). Elle imprime chaque script lance et chaque opcode
    /// execute.
    ///
    /// <para>Elle repond a une question que <see cref="s_TraceVerbs"/> ne couvre pas : quand un
    /// decor <b>ne fait rien du tout</b>, il faut d'abord savoir si un script tourne. Trois issues,
    /// et elles se distinguent en un run :</para>
    ///
    /// <list type="bullet">
    /// <item>aucune ligne — l'interaction elle-meme n'atteint pas l'interpreteur ;</item>
    /// <item>des lignes <c>[SCRIPT]</c> mais aucun opcode interessant — le script tourne et ne fait
    /// que des tests ;</item>
    /// <item>des opcodes, dont le 0 — alors <see cref="s_TraceVerbs"/> nomme le verbe.</item>
    /// </list>
    /// </summary>
    private static readonly bool s_TraceScript =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_SCRIPT") == "1";

    internal static void RunScript(byte* context, byte* entity, uint scriptIndex)
    {
        uint condition = 0;                              // Ghidra: local_4
        *(uint*)Mem.Entity(0x00600860) = scriptIndex;

        if (scriptIndex >= 0xffff) return;

        byte* script = *(byte**)(*(byte**)(context + 0x8c) + scriptIndex * 4);

        if (s_TraceScript)
        {
            Console.Error.WriteLine(
                $"[SCRIPT] {s_ScriptCaller} index {scriptIndex} entite 0x{(nuint)entity:x} genre 0x" +
                (entity != null ? $"{*(uint*)entity & 0xfc0000:x}" : "?"));
        }

        for (uint op = *script; op != 0xff && (op == 2 || condition == 0); op = *script)
        {
            if (s_TraceScript) Console.Error.WriteLine($"[OPCODE] {op}");
            script = DispatchScriptOp(op, context, entity, script, &condition);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00513eb0 @ 0x00513eb0 — <b>lance une liste de scripts</b> d'affilee sur la meme
    /// entite. La liste est un simple tableau d'octets dont le premier est le compte : les
    /// <paramref name="list"/><c>[1..n]</c> qui suivent sont autant d'index de script.
    ///
    /// <para>
    /// Le compte est lu <b>une fois</b>, avant la boucle, la ou l'opcode 11 relit le sien a chaque
    /// tour ; les deux formes coexistent dans le binaire et sont transcrites telles quelles.
    /// </para>
    /// </summary>
    private static void RunEntityScriptList(byte* context, byte* entity, byte* list)
    {
        uint count = *list;
        for (uint i = 0; i < count; i++)
        {
            list++;
            RunEntityScript(context, entity, *list);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00513ef0 @ 0x00513ef0 — la liste de scripts « avant le pas de simulation »
    /// (<c>DAT_00600840</c>), lancee sur le meneur. Premier des deux points d'accroche du cas 0 de
    /// <see cref="RunWorldMapTick"/> ; <see cref="RunLeaderScriptListLate"/> est le second.
    /// </summary>
    internal static void RunLeaderScriptListEarly(byte* context)
        => RunEntityScriptList(
            context,
            Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c,
            Mem.Entity(0x00600840));

    /// <summary>
    /// Ghidra: FUN_00513f20 @ 0x00513f20 — jumelle exacte de <see cref="RunLeaderScriptListEarly"/>,
    /// a une adresse de liste pres (<c>DAT_00600880</c>), lancee en fin de pas de simulation.
    /// </summary>
    internal static void RunLeaderScriptListLate(byte* context)
        => RunEntityScriptList(
            context,
            Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c,
            Mem.Entity(0x00600880));

    /// <summary>
    /// Ghidra: FUN_00532710 @ 0x00532710 — <b>rend un emplacement</b> au pool que
    /// <see cref="AllocEntitySlot"/> sert : le sommet redescend d'un cran et l'indice libere est
    /// repose sur la pile.
    ///
    /// <para>
    /// Comme son jumeau, ses appelants empilent la capacite en second argument et le corps ne la
    /// lit pas — l'en-tete la porte deja. Le garde <c>*pool != 0</c> est ce qui rend l'operation
    /// sure quand rien n'a ete pris.
    /// </para>
    /// </summary>
    private static void FreeEntitySlot(byte* pool, byte index)
    {
        byte top = *pool;
        if (top == 0) return;

        // `pool[top + 1]`, et non `pool[top]` : la pile commence a l'octet 2 et le sommet redescend
        // a `top - 1`, donc la case rendue est `(top - 1) + 2`. C'est bien la case que
        // `AllocEntitySlot` reprendra — elle lit `pool[top + 2]`.
        pool[top + 1] = index;
        *pool = (byte)(top - 1);
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_LAB_00563298)[opcode])(…)</c> — la table de 39 gestionnaires.
    ///
    /// <para>
    /// Rendue en <c>switch</c> et non en tableau de delegues : c'est une table de sauts dans
    /// l'original, et <c>Strategy.md</c> demande de reconstruire le flot plutot que de le
    /// translitterer. Le <c>switch</c> evite en prime d'allouer 39 delegues au demarrage, ce qui
    /// passerait devant la course pour la memoire basse.
    /// </para>
    /// <para>
    /// L'etendue de la table est **deduite, pas supposee** : elle s'arrete a <c>+0x98</c>, le dword
    /// suivant etant nul, soit 39 entrees (opcodes 0..38).
    /// </para>
    /// </summary>
    private static byte* DispatchScriptOp(uint op, byte* ctx, byte* ent, byte* s, uint* result)
        => op switch
        {
            0  => ScriptOpAreaVerb(ent, s, result),
            1  => ScriptOpJump(ctx, s),
            2  => ScriptOpJumpIf(ctx, s, result),
            3  => ScriptOpAluEntity(ctx, ent, s, result),
            4  => ScriptOpAluArena(0x005e96e0, s, result),
            5  => ScriptOpAluArena(0x005e9780, s, result),
            6  => ScriptOpAluArena(0x005e9060, s, result),
            7  => ScriptOpAluArena(0x005e9840, s, result),
            8  => ScriptOpEnterSubArea(s, result),
            9  => ScriptOpAluEntityBank(ent, s, result),
            10 => ScriptOpAluChunkRecords(s),
            11 => ScriptOpPushPending(s),
            12 => ScriptOpSpawnSubArea(s),
            13 => ScriptOpSpawnFormation(ctx, s, result),
            14 => ScriptOpShowMessage(ent, s),
            16 => ScriptOpSpawnEffect(ent, s, result),
            17 => ScriptOpTransferArea(ent, s),
            18 => ScriptOpFreeze(s),
            19 => ScriptOpUnfreeze(s),
            22 => ScriptOpAluEntityStats(ent, s, result),
            23 => ScriptOpSetAdd(0x00600840, s, result),
            24 => ScriptOpSetAdd(0x00600880, s, result),
            25 => ScriptOpSetRemove(0x00600840, s, result),
            26 => ScriptOpSetRemove(0x00600880, s, result),
            28 => ScriptOpAluParty(s, result),
            29 => ScriptOpBeginScriptedCamera(ent, s),
            30 => ScriptOpExitToDestinationSelect(ent, s),
            15 => ScriptOpPlaySound(ctx, s),
            20 => ScriptOpSetCameraOffset(s),
            21 => ScriptOpSetBackColour(s),
            27 => ScriptOpGrantItem(ent, s, result),
            31 => ScriptOpPercentDamage(ent, s),
            32 => ScriptOpKillAllSceneEntities(s),
            33 => ScriptOpRetireAllSceneEntities(s),
            34 => ScriptOpChangeMusic(s),
            35 => s + 2,                                     // Ghidra: 0x00515f10 — `add eax,2 / ret`, rien d'autre
            36 => ScriptOpReenterSubArea(ent, s),
            37 => ScriptOpSwapStateBanks(s),
            38 => ScriptOpAluEntityStatsHigh(ent, s, result),
            _  => throw new NotImplementedException(BuildScriptOpGap(op)),
        };

    /// <summary>
    /// Le message d'un opcode non encore transcrit. Il nomme l'opcode, son adresse d'origine, et ou
    /// trouver de quoi l'ecrire — parce qu'un trou qui ne dit pas comment se combler se paye deux
    /// fois.
    /// </summary>
    private static string BuildScriptOpGap(uint op)
    {
        uint[] table = ScriptOpTable;
        if (op >= table.Length)
        {
            return $"Interpreteur de script : opcode {op} hors de la table de 39 entrees " +
                   "(0x00563298..+0x98). Le script lu est probablement corrompu, ou le pointeur de " +
                   "script est faux — verifier context + 0x8c avant de chercher un gestionnaire.";
        }

        uint address = table[op];

        // Un opcode a un blocage precis, etabli le 2026-08-21 : le dire ici evite de refaire
        // l'enquete le jour ou il tombe.

        if (op == 27)
        {
            return "Interpreteur de script : opcode 27 = FUN_00515d30, **laisse de cote " +
                   "sciemment**. Son corps est court, mais il tire FUN_00530f20 (153 l., avec un " +
                   "troisieme argument que Ghidra perd — verifie a `add esp,0Ch`) et FUN_005312d0 " +
                   "(109 l., criblee d'artefacts de pile), lesquelles tirent a leur tour " +
                   "FUN_00544350, FUN_0051bcc0, FUN_00530ed0, FUN_0053db20 et FUN_00532710. " +
                   "C'est la cascade d'affichage des nombres de degats, sans rapport avec le " +
                   "chemin vers le prologue. Voir Documentation/Etat/TODO.md, etape (2).";
        }

        bool inBlazeC = ScriptOpInBlazeC(op);
        return $"Interpreteur de script : opcode {op} = FUN_{address:x8}, pas encore transcrit. " +
               (inBlazeC
                   ? "Son corps est dans Legacy/Decompiled/Blaze.c ; il tire des appelees encore " +
                     "manquantes, listees dans Documentation/Etat/TODO.md, etape (2)."
                   : "**Son corps n'est pas dans Blaze.c** — Ghidra a nomme la table `PTR_LAB_`, " +
                     "donc il n'a pas reconnu ces corps comme des fonctions. Le desassemblage est " +
                     $"deja releve : Legacy/Raw/Blaze/ScriptOps/FUN_{address:x8}.asm (fenetre de " +
                     "96 octets ; en reprendre un plus large avec le verbe `disasm` si le corps " +
                     "deborde).");
    }

    /// <summary>
    /// Ghidra: <c>&amp;PTR_LAB_00563298</c> — les 39 adresses de la table, relevees dans le binaire.
    /// Elles ne servent qu'a nommer un opcode manquant dans un message d'erreur ; le dispatch, lui,
    /// passe par le <c>switch</c>.
    /// </summary>
    private static readonly uint[] ScriptOpTable =
    [
        0x00515490, 0x005154c0, 0x005154e0, 0x00515520, 0x00515550, 0x00515580, 0x005155b0,
        0x005155e0, 0x00515610, 0x00515710, 0x00515790, 0x00515850, 0x00515880, 0x005158a0,
        0x005158f0, 0x00515970, 0x005159d0, 0x00515a10, 0x00515a80, 0x00515aa0, 0x00515ab0,
        0x00515b00, 0x00515b40, 0x00515c20, 0x00515c50, 0x00515cd0, 0x00515d00, 0x00515d30,
        0x00515d70, 0x00515df0, 0x00515e20, 0x00515e80, 0x00515eb0, 0x00515ec0, 0x00515ed0,
        0x00515f10, 0x00515f20, 0x00515f90, 0x00515fa0,
    ];

    /// <summary>Les opcodes dont le corps est dans <c>Blaze.c</c> ; les autres n'existent que sous
    /// forme de desassemblage, dans <c>Legacy/Raw/Blaze/ScriptOps/</c>.</summary>
    private static bool ScriptOpInBlazeC(uint op) => op switch
    {
        3 or 4 or 5 or 6 or 7 or 8 or 9 or 10 or 12 or 13 or 16 or 18 or 23 or 24 or 25 or 26
            or 27 or 28 or 29 => true,
        _ => false,
    };

    // =============================================================================================
    // Les gestionnaires transcrits. Signature commune, imposee par le site de dispatch :
    //   (contexte, entite, pointeur de script, sortie: resultat de condition) -> instruction suivante
    // Les gestionnaires qui n'utilisent pas tous ces parametres ne les prennent pas ; Ghidra les
    // declare quand meme parce que l'appelant en empile toujours quatre.
    // =============================================================================================

    /// <summary>Ghidra: FUN_00515520 @ 0x00515520 (opcode 3) — l'operation porte sur l'entite
    /// courante, a l'octet <c>script[1]</c>.</summary>
    private static byte* ScriptOpAluEntity(byte* ctx, byte* ent, byte* s, uint* result)
    {
        *result = ApplyScriptOp(ent + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00515550 / 00515580 / 005155b0 / 005155e0 (opcodes 4 a 7) — la meme
    /// operation sur quatre bancs differents de l'arene de jeu. Les quatre corps sont identiques a
    /// l'adresse de base pres, d'ou un seul ici.</summary>
    private static byte* ScriptOpAluArena(int bankVa, byte* s, uint* result)
    {
        byte* cell = Mem.Arena(bankVa) + s[1];
        byte before = *cell;

        *result = ApplyScriptOp(cell, s[2], s[3]) ? 1u : 0u;

        if (s_TraceScript)
        {
            Console.Error.WriteLine(
                $"[ALU] banc 0x{bankVa:x6}[{s[1]}] valeur={before} op={s[2]} operande={s[3]}" +
                $" -> resultat={*result} (valeur apres={*cell})");
        }

        return s + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00515610 @ 0x00515610 (opcode 8) — l'operation porte sur une zone chargee, et si
    /// c'est la zone courante elle bascule en prime la sous-zone, la musique et la vue.
    /// </summary>
    private static byte* ScriptOpEnterSubArea(byte* s, uint* result)
    {
        byte area = s[2];

        // Ghidra: `(&DAT_005e97c0)[s[1]]` — les racines de ressources voisines, que la
        // reconstruction tient en cinq champs distincts. Les lire par Mem.Arena(0x005e97c0)
        // tombait sur une cellule que personne n'ecrit : pointeur nul, plantage a la premiere
        // entree de sous-zone scriptee (ruines anciennes). Voir UiText.ResourceRoot.
        byte* root = Ui.ResourceRoot(s[1], "FUN_00515610");
        byte* record = *(byte**)(*(byte**)(root + 0x74) + area * 4);

        *result = ApplyScriptOp(record + s[3], s[4], s[5]) ? 1u : 0u;

        if (*Mem.Camera(0x005de058) == area)
        {
            s_Unknown_005de534 = record[0];
            *(byte*)Mem.Camera(0x005de03c) = record[1];
            s_Unknown_005645fc = record[2];

            if (s_Unknown_00566c8c != s_Unknown_005de534)
            {
                s_Unknown_00566c8c = (byte)s_Unknown_005de534;
                PlayMusicTrack(s_Unknown_005de534);           // Ghidra: FUN_00529ad0
            }

            byte* view = *(byte**)(s_Unknown_005e97cc + 0x5c)
                       + (uint)*(byte*)Mem.Camera(0x005de03c) * 0x10;
            *(byte**)Mem.Arena(0x005e9af0) = view;
            *(uint*)Mem.Arena(0x005e9310) = *(uint*)(view + 4);
            *(uint*)Mem.Arena(0x005e9314) = *(uint*)(view + 8) & 0xffff;
            *(uint*)Mem.Arena(0x005e92b8) = *(uint*)(view + 8) >> 0x10 | *(uint*)(view + 0xc) << 0x10;
            *(uint*)Mem.Arena(0x005e92bc) = *(uint*)(view + 0xc) >> 0x10;

            byte* palette = *(byte**)(s_Unknown_005e97cc + 0x60) + (uint)s_Unknown_005645fc * 4;
            *(byte**)Mem.Entity(0x005eca98) = palette;
            *(uint*)Mem.Arena(0x005e9b3c) = *(uint*)palette;
        }

        return s + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00515710 @ 0x00515710 (opcode 9) — l'operation porte sur un emplacement d'entite
    /// pris dans l'un des trois bancs, choisi par <c>script[1]</c>. **Une valeur hors de 0..2 laisse
    /// la cible sur l'entite courante** — ce n'est pas un defaut, c'est la branche par defaut de
    /// l'original.
    /// </summary>
    private static byte* ScriptOpAluEntityBank(byte* ent, byte* s, uint* result)
    {
        byte* target = ent;
        switch ((sbyte)s[1])
        {
            case 0: target = Mem.Arena(0x005e93c0) + (uint)s[2] * 0x9c; break;
            case 1: target = Mem.Entity(0x005ecaa0) + (uint)s[2] * 0x9c; break;
            case 2: target = Mem.Entity(0x005fa5e0) + (uint)s[2] * 0x9c; break;
        }

        *result = ApplyScriptOp(target + s[3], s[4], s[5]) ? 1u : 0u;
        return s + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00515790 @ 0x00515790 (opcode 10) — applique l'operation a TOUS les
    /// enregistrements d'une liste du morceau de terrain, jusqu'au terminateur negatif.
    /// <b>Ne pose pas de resultat</b> : c'est une boucle d'effet, pas un test.
    /// </summary>
    private static byte* ScriptOpAluChunkRecords(byte* s)
    {
        byte stride = ScriptRecordStrides[s[1]];
        byte** chunkTable = (byte**)Mem.Entity(0x005f7bc0);
        uint chunk = (uint)*(byte*)(Mem.Entity(0x00600830) + (uint)s[1] * 2) + s[2];

        byte* records = *(byte**)(chunkTable[chunk] + 0x28);
        short* list = *(short**)(*(byte**)(chunkTable[chunk] + 0x38) + (uint)s[3] * 4);

        for (short index = *list; index >= 0; index = *++list)
            ApplyScriptOp(records + index * stride + s[4], s[5], s[6]);

        return s + 7;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00563338</c> — la foulee d'un enregistrement, par type de liste. Relevee
    /// dans le binaire (`at Legacy/Binaries/Blaze.exe 0x00563338`) : les six octets utiles sont
    /// <c>10 20 20 20 10 10</c>, suivis de deux zeros puis d'un pointeur sans rapport
    /// (<c>0x00516020</c>), ce qui borne la table a six entrees.
    ///
    /// <para>0x10 est la taille d'une face du terrain — le meme enregistrement de 16 octets que
    /// <c>TryPlaceEntity</c> parcourt.</para>
    /// </summary>
    private static readonly byte[] ScriptRecordStrides = [0x10, 0x20, 0x20, 0x20, 0x10, 0x10];

    /// <summary>Ghidra: FUN_00515880 @ 0x00515880 (opcode 12) — peuple la sous-zone nommee par
    /// <c>script[1]</c>.</summary>
    private static byte* ScriptOpSpawnSubArea(byte* s)
    {
        SpawnSubAreaEntities(s[1]);                          // Ghidra: FUN_005350b0
        return s + 2;
    }

    /// <summary>
    /// Ghidra: 0x00515a10 (opcode 17) — <b>le changement de salle</b>. C'est l'opcode qu'un script
    /// de porte, d'escalier ou de sortie de carte execute : il note ou aller, marque l'entite comme
    /// partante, et demande a la boucle de la carte du monde de recharger.
    ///
    /// <para>Absent de <c>Blaze.c</c> — Ghidra a nomme la table <c>PTR_LAB_</c>, donc il n'a pas
    /// reconnu ces corps comme des fonctions. Releve au desassembleur :
    /// <c>Legacy/Raw/Blaze/ScriptOps/FUN_00515a10.asm</c>, 110 octets (la fenetre de 96 du premier
    /// relevé coupait la fin).</para>
    ///
    /// <para><b>Le garde d'entree est le meme que celui de <see cref="ScriptOpShowMessage"/></b> :
    /// <c>(drapeaux &amp; 0xfc0000) == 0x800000</c>, le genre « membre du groupe ». Toute autre
    /// entite ne fait qu'avancer le curseur de quatre octets — l'opcode est inerte pour elle, et le
    /// <c>lea eax,[edx+4]</c> commun aux deux chemins le dit sans ambiguite.</para>
    ///
    /// <para><b>Les trois octets d'operande ne se lisent pas de la meme facon.</b> <c>script[1]</c>
    /// (la zone) et <c>script[2]</c> (la sous-zone) portent <c>0xff</c> pour « ne pas changer » ;
    /// <c>script[3]</c> (le point d'entree) est ecrit <b>inconditionnellement</b>, sans test de
    /// <c>0xff</c>. L'asymetrie est dans le binaire — deux <c>cmp cl,0FFh ; je</c> et pas de
    /// troisieme — et elle a un sens : on peut rester dans la meme zone en changeant de porte, on ne
    /// peut pas arriver sans point d'arrivee.</para>
    ///
    /// <para><b>Le meme geste existe ailleurs</b>, pour la sortie <i>marchee</i> plutot que scriptee
    /// (<c>Blaze.c</c> 57354-57363) : memes quatre champs d'entite, memes globales de destination.
    /// Une seule difference, et elle compte — celle-la pose le pas de boucle a <b>1</b>, celle-ci a
    /// <b>2</b>. Or <see cref="PlaceWorldMapParty"/> ne replace le groupe au point d'entree que si le
    /// pas vaut 2. Un transfert scripte repose donc le groupe ; une sortie marchee le laisse ou il
    /// est.</para>
    ///
    /// <para>Le marquage de l'entite : <c>+0x98</c> recoit <c>+0x32</c> (la valeur mise de cote le
    /// temps du transfert), les drapeaux perdent <c>0x08000200</c> et gagnent <c>0x2000</c>, et le
    /// compteur <c>+0x12</c> avance d'un — le meme champ « occupee » que
    /// <see cref="ScriptOpShowMessage"/> consulte pour choisir un locuteur.</para>
    /// </summary>
    private static byte* ScriptOpTransferArea(byte* ent, byte* s)
    {
        // Ghidra: `and edx,0FC0000h ; cmp edx,800000h ; jne 00515A7A`.
        if ((*(uint*)ent & 0xfc0000) != 0x800000) return s + 4;

        ushort saved = *(ushort*)(ent + 0x32);            // Ghidra: mov si,[eax+32h], lu avant tout
        *(ushort*)(ent + 0x12) += 1;                      // Ghidra: inc word [eax+12h]
        *(ushort*)(ent + 0x98) = saved;
        *(uint*)ent = *(uint*)ent & 0xf7fffdff | 0x2000;  // Ghidra: and ecx,0F7FFFDFFh ; or ch,20h

        // 0xff = « ne pas changer », pour la zone et la sous-zone seulement.
        if (s[1] != 0xff)
        {
            // DAT_005de528 est un dword ici alors que l'original y ecrit un octet (`mov [5DE528h],cl`) :
            // les trois octets hauts sont preserves. Tous ses lecteurs le masquent par 0xff, mais
            // reproduire la largeur du store coute une ligne et ne suppose rien.
            Res.s_Unknown_005de528 = (int)((uint)Res.s_Unknown_005de528 & 0xffffff00 | s[1]);
        }

        if (s[2] != 0xff) s_Unknown_005de52c = s[2];

        s_Unknown_005de530 = s[3];                        // sans test de 0xff — voir le resume
        s_Unknown_005de53c = ent[8];                      // le membre du groupe qui declenche
        s_WorldMapStep_005645f8 = 2;                      // rechargement, groupe repose au point d'entree

        return s + 4;
    }

    /// <summary>Ghidra: FUN_00515a80 @ 0x00515a80 (opcode 18) — gele la simulation.
    ///
    /// <para>
    /// <b>Cet opcode s'appelait <c>ScriptOpAreaCall</c> et appelait un <c>ApplyAreaTransition</c>
    /// imaginaire jusqu'au 2026-08-21.</b> Le nom venait de la seule adresse ; la lecture du corps
    /// de <c>FUN_00535300</c> montre qu'il n'y a aucune transition de zone la-dedans.
    /// </para>
    /// <para>
    /// L'octet <c>script[1]</c> est lu et empile, mais <see cref="FreezeAllEntities"/> ne le
    /// consulte jamais : son corps n'a pas de parametre du tout. Argument mort, transcrit tel quel
    /// puisque l'avancee du pointeur de script en depend (deux octets, contre un seul pour
    /// l'opcode 19 qui n'a pas d'operande).
    /// </para>
    /// </summary>
    private static byte* ScriptOpFreeze(byte* s)
    {
        FreezeAllEntities();                                 // Ghidra: FUN_00535300(script[1]) — operande ignoree
        return s + 2;
    }

    /// <summary>
    /// Ghidra: 0x00515ed0 (opcode 34) — <b>change la musique</b> de la carte : l'operande
    /// <c>script[1]</c> est un numero de piste. Absent de <c>Blaze.c</c> (table <c>PTR_LAB_</c>) ;
    /// transcrit depuis <c>Legacy/Raw/Blaze/ScriptOps/FUN_00515ed0.asm</c>, 50 octets.
    ///
    /// <para>Si la piste demandee n'est pas deja celle qui joue (<c>DAT_00566c8c</c>), elle devient la
    /// piste de la zone (<c>DAT_005de534</c>) et de la lecture, puis <see cref="PlayMusicTrack"/> la
    /// lance. C'est le meme couple que le prologue de <see cref="RunWorldMapTick"/> compare a chaque
    /// entree de carte : la piste choisie par script survit donc a un rechargement de sous-zone.</para>
    ///
    /// <para><c>mov [5DE534h],al</c> n'ecrit que l'octet bas de <c>DAT_005de534</c>, declare
    /// <c>int</c> ici ; les trois autres sont gardes.</para>
    /// </summary>
    private static byte* ScriptOpChangeMusic(byte* s)
    {
        byte track = s[1];

        if (s_Unknown_00566c8c != track)
        {
            s_Unknown_005de534 = (int)((uint)s_Unknown_005de534 & 0xffffff00) | track;
            s_Unknown_00566c8c = track;
            PlayMusicTrack(track);                           // Ghidra: FUN_00529ad0
        }

        return s + 2;
    }

    // =============================================================================================
    // Les dix derniers opcodes, transcrits le 2026-09-15 d'un bloc. Neuf sont absents de Blaze.c
    // (table PTR_LAB_) et relus au desassembleur ; le 27 y est, mais n'avait jamais ete branche.
    // Tous prennent (ctx, entite, script, resultat) en __cdecl et rendent le script avance.
    // =============================================================================================

    /// <summary>
    /// Ghidra: 0x00515970 (opcode 15) — <b>joue un son</b> decrit par un enregistrement de huit
    /// octets du contexte. <c>script[1]</c> indexe la table <c>ctx + 0x90</c> ; l'enregistrement
    /// porte quatre mots : l'indice dans la table d'identifiants <c>**(DAT_005e97cc + 0xa8)</c>, puis
    /// les trois derniers arguments de <c>FUN_0053cea0</c> tels quels.
    /// </summary>
    private static byte* ScriptOpPlaySound(byte* ctx, byte* s)
    {
        byte* record = *(byte**)(ctx + 0x90) + s[1] * 8;
        byte* ids = *(byte**)*(byte**)(s_Unknown_005e97cc + 0xa8);

        Aud.PlaySoundEffect(                                 // Ghidra: FUN_0053cea0
            ids[*(ushort*)record],
            *(ushort*)(record + 2),
            *(ushort*)(record + 4),
            *(ushort*)(record + 6));

        return s + 2;
    }

    /// <summary>
    /// Ghidra: 0x00515ab0 (opcode 20) — pose les quatre mots <c>DAT_005e9028..2e</c> : trois
    /// composantes multipliees par seize (<c>script[2..4] &lt;&lt; 4</c>) puis <c>script[1]</c> brut.
    /// Ce sont les mots que le chargement de sous-zone remplit depuis la camera de la carte
    /// (<c>WorldMap.cs</c>) : le script les reecrit. Cinq octets.
    /// </summary>
    private static byte* ScriptOpSetCameraOffset(byte* s)
    {
        *(ushort*)Mem.Arena(0x005e9028) = (ushort)(s[2] << 4);
        *(ushort*)Mem.Arena(0x005e902a) = (ushort)(s[3] << 4);
        *(ushort*)Mem.Arena(0x005e902c) = (ushort)(s[4] << 4);
        *(ushort*)Mem.Arena(0x005e902e) = s[1];
        return s + 5;
    }

    /// <summary>
    /// Ghidra: 0x00515b00 (opcode 21) — pose les quatre mots <c>DAT_005e9160..66</c> : les trois
    /// composantes <c>script[2..4]</c> puis <c>script[1]</c>. C'est le bloc que le lissage de couleur
    /// de fond recopie vers <c>s_BackColour*_005e93a0</c> quand <c>DAT_005e9166</c> vaut 1 (voir
    /// <c>GameMemory</c>) : le script fixe donc la couleur de fond et son mode. Cinq octets.
    /// </summary>
    private static byte* ScriptOpSetBackColour(byte* s)
    {
        *(ushort*)Mem.Arena(0x005e9160) = s[2];
        *(ushort*)Mem.Arena(0x005e9162) = s[3];
        *(ushort*)Mem.Arena(0x005e9164) = s[4];
        *(ushort*)Mem.Arena(0x005e9166) = s[1];
        return s + 5;
    }

    /// <summary>
    /// Ghidra: FUN_00515d30 @ 0x00515d30 (opcode 27) — <b>remet un objet</b> tire au sort a
    /// l'entite : un tirage pondere dans la rangee <c>script[1]</c> de la table de butin, confie a
    /// <see cref="GrantChestContents_00530F20"/> avec le rangement <c>0x005312d0</c> comme rappel. Le
    /// resultat est ce qu'elle rend (zero si l'objet est passe).
    ///
    /// <para><b>Present dans Blaze.c, et absent du repartiteur jusqu'ici.</b> Ghidra le montre avec
    /// <c>FUN_0053a920(…, 100, FUN_005312d0)</c> : le troisieme <c>push</c> est en realite le rappel de
    /// l'appel SUIVANT, pose sur la pile avant le tirage (<c>0x00515d3c</c>) et consomme par
    /// <c>FUN_00530f20</c>, qui nettoie douze octets.</para>
    /// </summary>
    private static byte* ScriptOpGrantItem(byte* ent, byte* s, uint* result)
    {
        *(uint*)(ent + 0x6c) = 0;
        Mem.s_Unknown_005e909f = 0;

        int sheet = PickWeightedTableEntry(s[1], 100);       // Ghidra: FUN_0053a920
        *result = (uint)GrantChestContents_00530F20((uint*)ent, sheet, 0x005312d0);
        return s + 2;
    }

    /// <summary>Ghidra: 0x00515e80 (opcode 31) — des degats en pourcentage des points de vie a
    /// l'entite du script : <c>script[1]</c> le pourcentage, <c>script[2]</c> le parametre d'effet.
    /// Trois octets.</summary>
    private static byte* ScriptOpPercentDamage(byte* ent, byte* s)
    {
        ApplyPercentDamage_005397B0((uint*)ent, s[1], s[2]); // Ghidra: FUN_005397b0
        return s + 3;
    }

    /// <summary>Ghidra: 0x00515eb0 (opcode 32) — <see cref="KillAllSceneEntities_0051BA70"/>, puis
    /// un octet.</summary>
    private static byte* ScriptOpKillAllSceneEntities(byte* s)
    {
        KillAllSceneEntities_0051BA70();
        return s + 1;
    }

    /// <summary>Ghidra: 0x00515ec0 (opcode 33) — <see cref="RetireAllSceneEntities_0051BAC0"/>, puis
    /// un octet.</summary>
    private static byte* ScriptOpRetireAllSceneEntities(byte* s)
    {
        RetireAllSceneEntities_0051BAC0();
        return s + 1;
    }

    /// <summary>
    /// Ghidra: FUN_0051ba70 @ 0x0051ba70 — <b>tue les trente-deux entites de scene</b> : clignotement
    /// <c>0x10</c>, bits <c>0x40000040</c> sur l'entite (morte, touchee) et <c>0x40000000</c> sur son
    /// bloc de statistiques. Aucun test : les emplacements vides recoivent les memes bits.
    ///
    /// <para><c>&amp;DAT_005ea5a0</c> est le champ <c>+0x40</c> du premier bloc de
    /// <c>s_EntityStatBlocks_005ea560</c> (0x120 octets par entite), parcouru au meme pas.</para>
    /// </summary>
    private static void KillAllSceneEntities_0051BA70()
    {
        byte* entity = Mem.Entity(0x005ecaa0);
        byte* stats = s_EntityStatBlocks_005ea560 + 0x40;

        for (int i = 0; i < 0x20; i++, entity += 0x9c, stats += 0x120)
        {
            *(ushort*)(entity + 0x96) = 0x10;
            *(uint*)entity |= 0x40000040;
            *(uint*)stats |= 0x40000000;
        }
    }

    /// <summary>Ghidra: FUN_0051bac0 @ 0x0051bac0 — <b>retire les entites de scene vivantes</b> :
    /// toute entite posee (<c>0x80000000</c>) et pas morte (<c>0x40000000</c>) recoit
    /// <c>0x18000000</c>. A l'inverse de <see cref="KillAllSceneEntities_0051BA70"/>, rien ne meurt
    /// et les emplacements vides ne sont pas touches.</summary>
    private static void RetireAllSceneEntities_0051BAC0()
    {
        byte* entity = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, entity += 0x9c)
        {
            uint flags = *(uint*)entity;
            if ((flags & 0x80000000) != 0 && (flags & 0x40000000) == 0)
            {
                *(uint*)entity = flags | 0x18000000;
            }
        }
    }

    /// <summary>
    /// Ghidra: 0x00515f20 (opcode 36) — <b>rentrer dans la sous-zone courante</b> par l'entree
    /// <c>script[1]</c>, si l'entite du script est un membre du groupe (seul <c>0x800000</c> parmi
    /// les bits <c>0xfc0000</c>).
    ///
    /// <para>Le membre recoit <c>0x2000</c> et perd <c>0x08000000</c> et <c>0x200</c>, son
    /// <c>+0x12</c> avance d'un cran et son cap est range en <c>+0x98</c>. Puis la zone et la
    /// sous-zone a charger reprennent celles qui sont chargees (<c>DAT_00566c84</c>,
    /// <c>DAT_00566c88</c>), l'entree devient <c>script[1]</c> (<c>DAT_005de038</c>), le meneur
    /// devient ce membre, et le pas de boucle passe a 1 — seul le second script d'entree sera
    /// relance. Deux octets dans tous les cas.</para>
    /// </summary>
    private static byte* ScriptOpReenterSubArea(byte* ent, byte* s)
    {
        uint flags = *(uint*)ent;
        if ((flags & 0xfc0000) != 0x800000) return s + 2;

        (*(ushort*)(ent + 0x12))++;
        *(short*)(ent + 0x98) = *(short*)(ent + 0x32);
        *(uint*)ent = (flags & 0xf7fffdff) | 0x2000;

        Res.s_Unknown_005de528 = (int)((uint)Res.s_Unknown_005de528 & 0xffffff00) | s_Unknown_00566c84;
        s_Unknown_005de52c = s_Unknown_00566c88;
        *(byte*)Mem.Camera(0x005de038) = s[1];
        s_Unknown_005de53c = ent[8];
        s_WorldMapStep_005645f8 = 1;

        return s + 2;
    }

    /// <summary>Ghidra: 0x00515f90 (opcode 37) — <see cref="SwapStateBanks_00513F50"/>, puis un
    /// octet.</summary>
    private static byte* ScriptOpSwapStateBanks(byte* s)
    {
        SwapStateBanks_00513F50();
        return s + 1;
    }

    /// <summary>
    /// Ghidra: FUN_00513f50 @ 0x00513f50 — <b>echange les quatre banques d'etat vives avec leurs
    /// quatre jumelles</b>, seize dwords chacune : FLAG (<c>0x005e96e0</c>) avec <c>0x005e9720</c>,
    /// WORK (<c>0x005e9780</c>) avec <c>0x005e92c0</c>, TEMP (<c>0x005e9060</c>) avec
    /// <c>0x005e9980</c>, ITEM (<c>0x005e9840</c>) avec <c>0x005e9360</c>. Ce sont les huit banques
    /// que la sauvegarde recopie : le jeu tient deux jeux d'etat et le script bascule de l'un a
    /// l'autre.
    /// </summary>
    private static void SwapStateBanks_00513F50()
    {
        SwapDwords(Mem.s_StateBank0_005e96e0, Mem.s_StateBank4_005e9720);
        SwapDwords(Mem.s_StateBank1_005e9780, Mem.s_StateBank5_005e92c0);
        SwapDwords(Mem.s_StateBank2_005e9060, Mem.s_StateBank6_005e9980);
        SwapDwords(Mem.s_StateBank3_005e9840, Mem.s_StateBank7_005e9360);
    }

    private static void SwapDwords(byte* a, byte* b)
    {
        for (int i = 0; i < 0x40; i += 4)
        {
            uint kept = *(uint*)(a + i);
            *(uint*)(a + i) = *(uint*)(b + i);
            *(uint*)(b + i) = kept;
        }
    }

    /// <summary>
    /// Ghidra: 0x00515fa0 (opcode 38) — la jumelle de <see cref="ScriptOpAluEntityStats"/> (opcode
    /// 22), <b>0x100 octets plus loin</b> dans le meme bloc : <c>MainMem + 0xf0200</c> au lieu de
    /// <c>0xf0100</c> pour un membre du groupe, <c>0x005ea660</c> au lieu de <c>0x005ea560</c> pour
    /// une entite de scene. Meme refus sans les bits <c>0xc00000</c> (resultat 1), memes quatre
    /// octets.
    /// </summary>
    private static byte* ScriptOpAluEntityStatsHigh(byte* ent, byte* s, uint* result)
    {
        if ((*(uint*)ent & 0xc00000) == 0)
        {
            *result = 1;
            return s + 4;
        }

        byte* stats = GetEntityStatBlock((uint*)ent) + 0x100;
        *result = ApplyScriptOp(stats + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: 0x00515aa0 (opcode 19) — degele la simulation, l'exact inverse de
    /// l'opcode 18.
    ///
    /// <para>
    /// Ce gestionnaire n'est pas dans <c>Blaze.c</c> : Ghidra a nomme la table <c>PTR_LAB_</c> et
    /// n'a donc pas reconnu ces corps comme des fonctions. Releve au desassembleur — quatre
    /// instructions, <c>call 00535400h ; mov eax,[esp+0Ch] ; inc eax ; ret</c>. Le <c>inc</c> et non
    /// <c>add eax,2</c> est ce qui prouve l'absence d'operande.
    /// </para>
    /// </summary>
    private static byte* ScriptOpUnfreeze(byte* s)
    {
        UnfreezeAllEntities();                               // Ghidra: FUN_00535400
        return s + 1;
    }

    /// <summary>
    /// Ghidra: FUN_00535300 @ 0x00535300 — <b>pose le bit de gel sur toutes les entites du
    /// monde</b>, puis sur la boucle de monde elle-meme.
    ///
    /// <para>
    /// Sept tables d'emplacements sont balayees, chacune au pas de <c>0x27</c> mots — soit
    /// <c>0x9c</c> octets, la taille d'une entite. Seule la premiere a un compte variable
    /// (<c>DAT_005e9020</c>, le nombre de membres du groupe) ; les six autres ont un compte fixe, et
    /// leur boucle est un <c>do/while</c> dans l'original : elles tournent au moins une fois meme si
    /// le compte etait nul, ce qui ne peut pas arriver puisqu'il est litteral.
    /// </para>
    /// <para>
    /// Le bit <c>0x4000000</c> ainsi pose est lu par les boucles de mise a jour, et le
    /// <c>0x10</c> ajoute a <c>s_WorldFlags_005de02c</c> gele la boucle de monde. La fonction est
    /// appelee de seize endroits dans le binaire ; seul l'interpreteur de script l'atteint pour
    /// l'instant, d'ou son sejour ici plutot que dans <c>Engine/</c> (regle 4 d'ARCHITECTURE.md).
    /// </para>
    /// <para>
    /// Ghidra la declare <c>void FUN_00535300(void)</c> alors que ses appelants empilent un
    /// argument. Ce n'est pas une signature manquee : le corps ne touche jamais a <c>[esp+4]</c>.
    /// L'argument est reellement mort, et l'appel est en cdecl, donc sans consequence.
    /// </para>
    /// </summary>
    internal static void FreezeAllEntities()
    {
        SetEntityFlagBit(0x00000000, 0x4000000);
        Mem.s_WorldFlags_005de02c |= 0x10;
    }

    /// <summary>Ghidra: FUN_00535400 @ 0x00535400 — l'exact inverse de
    /// <see cref="FreezeAllEntities"/> : les memes sept tables, les memes comptes, le meme pas, et
    /// <c>&amp; 0xfbffffff</c> au lieu de <c>| 0x4000000</c>.</summary>
    internal static void UnfreezeAllEntities()
    {
        SetEntityFlagBit(0x4000000, 0x00000000);
        Mem.s_WorldFlags_005de02c &= 0xffffffef;
    }

    /// <summary>
    /// Le corps commun a <see cref="FreezeAllEntities"/> et <see cref="UnfreezeAllEntities"/> : les
    /// deux fonctions de l'original sont copie-collees a un operateur pres, sur sept tables.
    /// Factorise ici parce que c'est une unite reellement independante — pas un <c>LAB_</c> promu en
    /// methode.
    /// </summary>
    /// <param name="clear">Les bits a retirer du premier mot de chaque entite.</param>
    /// <param name="set">Les bits a y ajouter.</param>
    private static void SetEntityFlagBit(uint clear, uint set)
    {
        // Premiere table : celle des membres du groupe, et son compte est variable. L'original la
        // parcourt en `for` — donc zero tour si le compte est nul, contrairement aux six suivantes
        // qui sont des `do/while` a compte litteral.
        //
        // Deux pieges d'adresse ici, tous deux verifies plutot que supposes :
        //   0x005e93c0 est bien dans GameArena, et `Inp.s_SlotFlags_005e93c0` en est une VUE
        //     (`mem.Arena(0x005e9180) + 0x240`) : meme memoire, pas d'aliasing.
        //   0x005e9020 en revanche est double par `s_PartySize_005e9020`, un `static int` ordinaire
        //     declare dans Screens/State04/SaveSlots.cs, HORS de l'arene. Le lire par adresse aurait
        //     rendu zero en silence et fait sauter le gel du groupe entier. On passe donc par le
        //     champ nomme — ce que fait aussi l'original, qui ecrit `DAT_005e9020` et non une
        //     adresse calculee. Le doublon reste a resorber : voir Documentation/Etat/TODO.md.
        ApplyFlagToSlots(Mem.Arena(0x005e93c0), s_PartySize_005e9020, clear, set);

        ApplyFlagToSlots(Mem.Entity(0x005fa5e0), 0x80, clear, set);
        ApplyFlagToSlots(Mem.Entity(0x005ecaa0), 0x20, clear, set);
        ApplyFlagToSlots(Mem.Entity(0x005f7de0), 0x40, clear, set);
        ApplyFlagToSlots(Mem.Entity(0x005ff400), 0x20, clear, set);
        ApplyFlagToSlots(Mem.Entity(0x005edee0), 0x80, clear, set);
        ApplyFlagToSlots(Mem.Entity(0x005f2d40), 0x80, clear, set);
    }

    /// <summary>Un balayage : <paramref name="count"/> emplacements au pas de 0x9c octets, le
    /// premier mot de chacun recevant <c>(mot &amp; ~clear) | set</c>.</summary>
    private static void ApplyFlagToSlots(byte* first, int count, uint clear, uint set)
    {
        uint* slot = (uint*)first;
        for (int i = count; i != 0; i--, slot += 0x27)
        {
            *slot = *slot & ~clear | set;
        }
    }


    /// <summary>
    /// Ghidra: FUN_00515b40 @ 0x00515b40 (opcode 22) — <b>l'operation porte sur le bloc de
    /// statistiques de l'entite courante</b>, la ou l'opcode 3 la fait porter sur son enregistrement
    /// de 0x9c octets. C'est par lui qu'un script lit ou change des points de vie, un niveau, une
    /// resistance.
    ///
    /// <para><b>Absent de <c>Blaze.c</c></b> comme les 38 autres entrees de
    /// <c>PTR_LAB_00563298</c> ; transcrit depuis le desassemblage releve dans
    /// <c>Legacy/Raw/Blaze/ScriptOps/FUN_00515b40.asm</c>.</para>
    ///
    /// <para><b>Une entite sans bloc rend vrai.</b> Si le mot d'etat ne porte ni <c>0x400000</c> ni
    /// <c>0x800000</c>, l'entite n'a pas de statistiques du tout : le resultat de condition recoit
    /// <c>1</c> et rien n'est ecrit. Ce n'est pas une garde ajoutee ici mais la premiere branche de
    /// l'original, et le <c>1</c> est le meme que rendrait une comparaison satisfaite — un script
    /// qui teste une statistique sur une entite qui n'en a pas voit donc son test passer.</para>
    ///
    /// <para>La resolution du bloc est mot pour mot celle de <see cref="GetEntityStatBlock"/>
    /// (<c>FUN_00538780</c>), reecrite en ligne par le compilateur : le bit <c>0x800000</c>
    /// distingue le membre du groupe, dont le bloc vit dans son enregistrement de 0x2000 octets,
    /// de toute autre entite, logee dans <see cref="s_EntityStatBlocks_005ea560"/> au pas de 0x120.
    /// La fonction est reutilisee ici plutot que recopiee — c'est la meme adresse, et la faire
    /// resoudre deux fois par deux chemins est exactement le defaut d'aliasing que
    /// <see cref="ResolveEntityBlocks"/> a deja coute une journee.</para>
    ///
    /// <para><b>Un residu de registre dans le deuxieme argument.</b> L'original charge l'indice
    /// d'entite dans <c>ECX</c>, s'en sert pour calculer l'adresse du bloc, puis n'ecrit que
    /// l'octet bas <c>CL</c> avec <c>script[2]</c> avant de pousser <c>ECX</c> entier. Sur la
    /// branche du membre de groupe, ou <c>ECX</c> vaut <c>indice &lt;&lt; 13</c>, les bits hauts
    /// pousses ne sont donc pas nuls. C'est sans effet : <c>FUN_00514040</c> declare son deuxieme
    /// parametre sur un octet et ne lit jamais au-dela — le compilateur le savait, d'ou le
    /// <c>xor</c> qu'il a omis la et fait pour le troisieme argument.</para>
    /// </summary>
    private static byte* ScriptOpAluEntityStats(byte* ent, byte* s, uint* result)
    {
        if ((*(uint*)ent & 0xc00000) == 0)
        {
            *result = 1;
            return s + 4;
        }

        byte* stats = GetEntityStatBlock((uint*)ent);        // Ghidra: FUN_00538780, reecrite en ligne
        *result = ApplyScriptOp(stats + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00515c20 / 00515c50 (opcodes 23 et 24) — ajoute une valeur a l'un des
    /// deux ensembles, et rend 1 si elle y etait deja.</summary>
    private static byte* ScriptOpSetAdd(int setVa, byte* s, uint* result)
    {
        *result = ScriptSetAdd(Mem.Entity(setVa), s[1]);
        return s + 2;
    }

    /// <summary>Ghidra: FUN_00515cd0 / 00515d00 (opcodes 25 et 26) — retire une valeur de l'un des
    /// deux ensembles.</summary>
    private static byte* ScriptOpSetRemove(int setVa, byte* s, uint* result)
    {
        *result = ScriptSetRemove(Mem.Entity(setVa), s[1]);
        return s + 2;
    }

    /// <summary>Ghidra: FUN_00515d70 @ 0x00515d70 (opcode 28) — deja transcrit sous le nom
    /// <see cref="ApplyPartyScriptOp"/>, appele par <c>RunArea0Handler8</c>.</summary>
    private static byte* ScriptOpAluParty(byte* s, uint* result)
    {
        ApplyPartyScriptOp(s, result);
        return s + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00515bc0 @ 0x00515bc0 — un ensemble compte : l'octet 0 porte le cardinal, les
    /// suivants les membres. Ajoute <paramref name="value"/> et rend <b>1 s'il y etait deja</b>.
    ///
    /// <para>
    /// <b>Le cardinal est incremente meme quand la valeur y est deja</b> dans la branche
    /// « ensemble vide » — non, dans celle-la l'ensemble est vide par construction. Mais le
    /// parcours ne s'arrete pas a la premiere place libre : il compare jusqu'au cardinal, puis
    /// ajoute. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static uint ScriptSetAdd(byte* set, byte value)
    {
        byte count = set[0];
        if (count == 0)
        {
            set[0] = (byte)(count + 1);
            set[(byte)(count + 1)] = value;
            return 0;
        }

        for (int i = 1; i <= count; i++)
            if (set[i] == value) return 1;

        set[0] = (byte)(count + 1);
        set[(byte)(count + 1)] = value;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00515c80 @ 0x00515c80 — retire une valeur de l'ensemble compte, en tassant la
    /// queue par-dessus le trou.
    /// </summary>
    private static uint ScriptSetRemove(byte* set, byte value)
    {
        uint count = set[0];
        if (count == 0) return 0;

        int index = 1;
        while (index <= (int)count && set[index] != value) index++;

        if (index > (int)count) return 0;

        int tail = (int)count - index;
        if (tail != 0)
        {
            byte* p = set + index;
            for (int i = 0; i < tail; i++, p++) *p = p[1];
        }

        set[0] = (byte)(count - 1);
        return 1;
    }

    // =============================================================================================
    // Les trois opcodes de peuplement, transcrits le 2026-08-21. Leurs arites ont toutes ete
    // verifiees au desassembleur avant transcription — dans ce fichier Ghidra s'est deja trompe
    // deux fois sur des listes d'arguments decalees.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_005158a0 @ 0x005158a0 (opcode 13) — fait apparaitre une <b>formation</b> entiere,
    /// puis applique une operation arithmetique sur le nombre d'entites reellement apparues.
    ///
    /// <para>
    /// La liste de formation est prise dans la table <c>contexte + 0x34</c>, indexee par
    /// <c>script[1]</c>. <c>script[4]</c> decide du mode de pose, <c>script[2]</c> et
    /// <c>script[3]</c> sont l'operation et son operande.
    /// </para>
    /// <para>
    /// Ghidra reutilise <c>param_3</c> — le pointeur de script — comme variable locale pour le
    /// compte, et sauvegarde le vrai script dans <c>iVar1</c>. C'est un artefact de reallocation de
    /// registre, pas une intention : ici le compte a son propre nom, et le script garde le sien.
    /// </para>
    /// </summary>
    private static byte* ScriptOpSpawnFormation(byte* ctx, byte* s, uint* result)
    {
        int* formation = *(int**)(*(byte**)(ctx + 0x34) + s[1] * 4);
        int spawned = SpawnEntityFormation(ctx, formation, 0, s[4]);
        *result = ApplyScriptOp((byte*)&spawned, s[2], s[3]) ? 1u : 0u;
        return s + 5;
    }

    /// <summary>
    /// Ghidra: FUN_005159d0 @ 0x005159d0 (opcode 16) — duplique l'entite courante dans la table des
    /// effets, et pose <b>l'inverse</b> du succes comme resultat de condition.
    ///
    /// <para>
    /// Le <c>^ 1</c> n'est pas une coquille : <see cref="SpawnEffectEntity"/> rend 1 en cas de
    /// succes, et la boucle de l'interpreteur s'arrete sur un resultat non nul. Le script continue
    /// donc quand l'effet a pu naitre, et s'interrompt quand la table etait pleine.
    /// </para>
    /// </summary>
    private static byte* ScriptOpSpawnEffect(byte* ent, byte* s, uint* result)
    {
        *result = SpawnEffectEntity(ent, s[1], s[2], s[3]) ^ 1u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00515df0 @ 0x00515df0 (opcode 29) — passe la main a une camera de
    /// scenette. Le troisieme argument est un <c>ushort</c> forme des deux octets <c>script[2]</c>
    /// et <c>script[3]</c> — le <c>mov ah,[esi+3] ; mov al,[esi+2]</c> du code machine le montre
    /// plus clairement que la pseudo-C.</summary>
    private static byte* ScriptOpBeginScriptedCamera(byte* ent, byte* s)
    {
        BeginScriptedCamera(ent, s[1], *(ushort*)(s + 2));
        return s + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00515e20 @ 0x00515e20 (opcode 30) — <b>quitte la zone pour l'ecran de choix de
    /// destination</b>. C'est la sortie d'un batiment : le script d'une porte pose l'etat, et le
    /// jeu ressort par la carte du monde.
    ///
    /// <para>
    /// Ce gestionnaire n'est pas dans <c>Blaze.c</c> — Ghidra a nomme la table <c>PTR_LAB_</c> et
    /// n'a donc pas reconnu ces corps comme des fonctions. Releve au desassembleur, corps entier
    /// dans la fenetre de 96 octets : <c>Legacy/Raw/Blaze/ScriptOps/FUN_00515e20.asm</c>, 81
    /// octets jusqu'au <c>ret</c>, puis quinze <c>nop</c> de bourrage.
    /// </para>
    ///
    /// <para>
    /// <b>C'est le jumeau de <see cref="ScriptOpTransferArea"/></b> (opcode 17) : meme garde de
    /// genre, et exactement les memes quatre champs d'entite pour marquer le membre comme partant
    /// — <c>+0x98</c> recoit <c>+0x32</c>, les drapeaux perdent <c>0x08000200</c> et gagnent
    /// <c>0x2000</c>, le compteur d'occupation <c>+0x12</c> avance d'un — suivis du meme
    /// <c>DAT_005de53c</c> = <c>entite[8]</c>. Toute la difference est dans la fin :
    /// </para>
    ///
    /// <list type="bullet">
    /// <item>l'opcode 17 <b>note une destination</b> (zone, sous-zone, point d'entree) et pose le
    /// pas de boucle a <b>2</b> : la carte du monde se recharge sur place, sans quitter l'etat 3.
    /// Ici il n'y a aucune operande a lire, et le pas passe a <b>0xfe</b> — la valeur sur laquelle
    /// <see cref="RunWorldMapState"/> fait <c>return</c> au lieu de reboucler ;</item>
    /// <item>l'opcode 30 pose en plus <c>s_GameStateId_005de510 = 2</c>. Le cas 2 de
    /// <see cref="RunGameStateTick"/> enchaine <c>Res.LoadMenuResources</c>,
    /// <see cref="RunDestinationSelect"/>, puis repose l'etat 3 : on sort du batiment, on choisit
    /// ou aller, on revient en jeu.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Aucune operande</b> : <c>lea eax,[ecx+1]</c>, et non <c>[ecx+4]</c> comme l'opcode 17. Et
    /// les deux chemins rendent <c>script + 1</c> — sur une entite qui n'est pas un membre du
    /// groupe, l'opcode ne fait rien du tout.
    /// </para>
    /// </summary>
    private static byte* ScriptOpExitToDestinationSelect(byte* ent, byte* s)
    {
        // Ghidra: `and edx,0FC0000h ; cmp edx,800000h ; jne 00515E69`.
        if ((*(uint*)ent & 0xfc0000) != 0x800000) return s + 1;

        ushort saved = *(ushort*)(ent + 0x32);            // Ghidra: mov dx,[eax+32h], lu avant tout
        *(ushort*)(ent + 0x12) += 1;                      // Ghidra: inc word [eax+12h]
        *(ushort*)(ent + 0x98) = saved;
        *(uint*)ent = *(uint*)ent & 0xf7fffdff | 0x2000;  // Ghidra: and ecx,0F7FFFDFFh ; or ch,20h

        // L'ordre est celui du binaire : le pas d'abord, le membre ensuite, l'etat en dernier.
        s_WorldMapStep_005645f8 = -2;                     // Ghidra: mov byte [5645F8h],0FEh
        s_Unknown_005de53c = ent[8];                      // le membre du groupe qui declenche
        s_GameStateId_005de510 = 2;                       // l'ecran de choix de destination

        return s + 1;
    }

    /// <summary>
    /// Ghidra: FUN_00532740 @ 0x00532740 — <b>l'allocateur d'emplacement</b> commun aux tables
    /// d'entites : un pool en pile, dont l'en-tete tient dans le premier <c>ushort</c>.
    ///
    /// <para>
    /// L'octet bas est le sommet, l'octet haut la capacite ; les octets suivants sont la pile
    /// d'indices libres. Rend l'indice pris, ou <c>-1</c> si le pool est plein.
    /// </para>
    /// <para>
    /// Ses appelants empilent DEUX arguments (<c>add esp,8</c>) alors que le corps ne lit que
    /// <c>[esp+4]</c> : la capacite passee en second est morte, puisqu'elle est deja dans
    /// l'en-tete. Verifie au desassembleur, pas suppose — la signature a un parametre que Ghidra
    /// donne est bien la bonne.
    /// </para>
    /// </summary>
    private static int AllocEntitySlot(ushort* pool)
    {
        ushort header = *pool;
        if ((byte)header >= (byte)(header >> 8))
        {
            return -1;
        }

        byte index = *((byte*)pool + (header & 0xff) + 2);
        *(byte*)pool = (byte)((byte)header + 1);
        return index;
    }

    /// <summary>
    /// Ghidra: FUN_0047a9d0 @ 0x0047a9d0 — <b>duplique une entite dans la table des effets</b>
    /// (0x005ff400, 0x20 emplacements).
    ///
    /// <para>
    /// L'emplacement recoit d'abord une copie mot pour mot des 0x9c octets de
    /// <paramref name="source"/>, puis une poignee de champs propres a l'effet. Le mot de drapeaux
    /// est reduit a <c>0xe80800</c> de ses bits d'origine avant de recevoir <c>0x80100000</c> :
    /// l'effet herite d'une partie de l'etat de son modele, pas de tout.
    /// </para>
    /// </summary>
    /// <param name="param4">Negatif veut dire « ne pas ecrire ce champ » — la seule branche
    /// conditionnelle de la fonction.</param>
    private static uint SpawnEffectEntity(byte* source, byte param2, byte param3, int param4)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x005fa5a0));
        if (index < 0)
        {
            return 0;
        }

        byte* slot = Mem.Entity(0x005ff400) + index * 0x9c;

        for (int i = 0; i < 0x27; i++)
        {
            ((uint*)slot)[i] = ((uint*)source)[i];
        }

        slot[0x08] = (byte)index;
        slot[0x05] = param2;
        *(ushort*)(slot + 0x36) = (ushort)(*(ushort*)(slot + 0x36) & 0xfffb | 2);
        slot[0x0b] = param3;
        *(byte**)(slot + 0x6c) = source;
        *(ushort*)(slot + 0x2e) = 0;

        if (param4 >= 0)
        {
            slot[0x0a] = (byte)param4;
        }

        *(uint*)(slot + 0x10) = 0;
        *(uint*)(slot + 0x14) = 0;

        uint flags = *(uint*)slot;
        *(uint*)(slot + 0x38) = 0x808080;
        *(uint*)slot = flags & 0xe80800 | 0x80100000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00538980 @ 0x00538980 — resout les <b>trois blocs de description</b> d'un
    /// emplacement d'entite a partir de deux de ses octets.
    ///
    /// <para>
    /// <c>slot[8]</c> indexe la table des blocs de statistiques (0x005ea560, pas de 0x120) ;
    /// <c>slot[0xc]</c> indexe la table d'especes du repertoire de zone (<c>+0x4c</c>, pas de 8),
    /// dont l'octet <c>+4</c> indexe a son tour la table de modeles (<c>+0x98</c>, pas de 0x60).
    /// </para>
    /// <para>
    /// <b>Defaut d'aliasing resorbe le 2026-08-23.</b> La table de statistiques etait resolue ici
    /// par <c>Mem.Arena(0x005ea560)</c> — une VUE sur <c>GameArena</c> — alors que
    /// <see cref="GetEntityStatBlock"/> (<c>FUN_00538780</c>), qui est le lecteur de toute la
    /// simulation, la resout par <see cref="s_EntityStatBlocks_005ea560"/>, une allocation
    /// <b>distincte</b>. Deux noms pour une seule adresse : le remplissage de
    /// <see cref="ResetEntityStats"/> partait dans la queue de <c>GameArena</c> et la simulation
    /// relisait des zeros, si bien que toute entite de scene naissait a 0 point de vie et se
    /// faisait tuer a la premiere image par <c>FUN_0053aa10</c>. Le defaut etait doublement
    /// fautif : <c>GameArena</c> s'arrete a <c>0x005ea600</c>, donc dès l'emplacement 1 l'ecriture
    /// sortait de l'arene.
    /// </para>
    /// </summary>
    private static void ResolveEntityBlocks(byte* slot, out byte* stats, out byte* species, out byte* model)
    {
        stats = s_EntityStatBlocks_005ea560 + slot[8] * 0x120;
        species = *(byte**)(s_Unknown_005e97cc + 0x4c) + slot[0xc] * 8;
        model = *(byte**)(s_Unknown_005e97cc + 0x98) + species[4] * 0x60;
    }

    /// <summary>
    /// Ghidra: FUN_005389e0 @ 0x005389e0 — <b>remet a neuf un bloc de statistiques</b> : 0x120
    /// octets remis a zero, puis les champs recopies depuis le bloc de modele.
    ///
    /// <para>
    /// Son deuxieme parametre n'est <b>jamais lu</b> par le corps. Ce n'est pas une signature
    /// manquee de Ghidra : l'appelant le pousse, la fonction l'ignore. Conserve pour que le site
    /// d'appel reste lisible contre l'original.
    /// </para>
    /// <para>
    /// <b>Les huit octets recopies deux fois — tels quels a <c>+0x18</c>, elargis en
    /// <c>ushort</c> a <c>+0x110</c> — sont les huit ELEMENTS</b> : feu, eau, terre, air, lumiere,
    /// tenebres, sacre, mal. Etabli le 2026-08-26 par le releve du CD, sur trois recoupements
    /// independants : les huit « Chaine de X » du catalogue d'objets, dont chacune ne remplit que
    /// son propre octet du bloc jumeau <c>objet+0x18</c> ; les quatre capes saisonnieres, qui
    /// remplissent exactement les emplacements 0 a 3 ; et le controle par les monstres eux-memes —
    /// Triton glace en eau, Grifon en air, Fantome en tenebres et mal. Voir
    /// <c>Documentation/Reference/Bestiaire/LISEZMOI.md</c>.
    /// </para>
    /// <para>
    /// TODO(REVERSE): pourquoi le meme bloc est ecrit aux deux endroits — octets a <c>+0x18</c>,
    /// ushorts a <c>+0x110</c> — reste a etablir ; le duo suggere une valeur de base et une valeur
    /// courante, comme les jauges, mais rien ne le confirme encore.
    /// </para>
    /// </summary>
    private static void ResetEntityStats(byte* stats, byte* species, byte* model)
    {
        _ = species;                                        // Ghidra: param_2, pousse mais jamais lu

        for (int i = 0; i < 0x48; i++)
        {
            ((uint*)stats)[i] = 0;
        }

        for (int i = 0; i < 0x10; i++)
        {
            stats[i] = model[i];
        }

        for (int i = 0; i < 8; i++)
        {
            stats[0x18 + i] = model[0x28 + i];
        }

        for (int i = 0; i < 8; i++)
        {
            *(ushort*)(stats + 0x110 + i * 2) = model[0x28 + i];
        }

        for (int i = 0; i < 8; i++)
        {
            *(ushort*)(stats + 0x30 + i * 2) = *(ushort*)(model + 0x30 + i * 2);
        }

        *(ushort*)(stats + 0x10) = *(ushort*)(model + 0x12);
        *(uint*)(stats + 0x14) = *(uint*)(model + 0x24);
        *(ushort*)(stats + 0x2e) = *(ushort*)(model + 0x1a);
        *(uint*)(stats + 0x58) = *(uint*)(model + 0x1c);
        *(uint*)(stats + 0x5c) = *(uint*)(model + 0x20);
        *(ushort*)(stats + 0x48) = *(ushort*)(model + 0x14);
        *(ushort*)(stats + 0x4c) = *(ushort*)(model + 0x14);
        *(ushort*)(stats + 0x4a) = *(ushort*)(model + 0x16);
        *(ushort*)(stats + 0x4e) = *(ushort*)(model + 0x16);
        *(ushort*)(stats + 0x44) = *(ushort*)(model + 0x10);
        *(uint*)(stats + 0x40) = 0;

        for (int i = 0; i < 0x10; i++)
        {
            *(uint*)(stats + 0x80 + i * 4) = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051b850 @ 0x0051b850 — <b>fait naitre une formation</b> : une liste
    /// d'enregistrements de 0x20 octets, terminee par <c>-1</c>, chacun donnant une entite de la
    /// table 0x005ecaa0.
    ///
    /// <para>
    /// La boucle s'arrete aussi des que le pool d'emplacements est plein, et rend le nombre
    /// d'entites reellement nees — c'est ce nombre que l'opcode 13 soumet ensuite a son operation.
    /// </para>
    /// <para>
    /// <b>Le piege de lecture est <c>param_2</c>.</b> A l'entree c'est la liste de formation ;
    /// <c>FUN_00538980</c> l'ECRASE avec le bloc d'espece, et tout ce qui suit dans le corps de
    /// boucle lit l'espece a travers ce meme nom. Le curseur de liste, lui, vit dans
    /// <c>piVar6</c>. Les deux ont ici des noms distincts.
    /// </para>
    /// </summary>
    /// <param name="mode">Ghidra <c>param_4</c> : zero donne une entite plate et invisible (echelle
    /// nulle, teinte nulle), non nul une entite a l'echelle 0x1000 et teintee de blanc.</param>
    private static int SpawnEntityFormation(byte* ctx, int* formation, ushort param3, int mode)
    {
        int spawned = 0;
        int* record = formation;
        int kind = *record;

        while (kind != -1)
        {
            int index = AllocEntitySlot((ushort*)Mem.Entity(0x005f2d00));
            if (index < 0)
            {
                break;
            }

            byte* slot = Mem.Entity(0x005ecaa0) + index * 0x9c;

            slot[0x08] = (byte)index;
            *(int*)(slot + 0x0c) = kind;

            ResolveEntityBlocks(slot, out byte* stats, out byte* species, out byte* model);

            *(byte**)(slot + 0x6c) = Mem.Arena(0x005e93c0) + CrtRand() % s_PartySize_005e9020 * 0x9c;
            *(int*)(slot + 0x74) = 0;
            *(uint*)(slot + 0x78) = 0;

            // A partir d'ici `kind` designe le premier mot du bloc d'espece, pas l'enregistrement :
            // c'est la reaffectation de `iVar4` par Ghidra, conservee parce que la valeur suivante
            // en depend.
            kind = *(int*)species;
            *(int*)(slot + 0x04) = kind;
            slot[0x0a] = (byte)((uint)kind >> 0x18);
            slot[0x0b] = (byte)((uint)kind >> 0x10);

            *(ushort*)(slot + 0x30) = *(ushort*)((byte*)record + 10);
            *(short*)(slot + 0x32) = (short)record[3];
            *(ushort*)(slot + 0x34) = *(ushort*)((byte*)record + 0xe);

            *(int*)(slot + 0x1c) = (short)record[1] << 0xc;
            int y = *(short*)((byte*)record + 6) << 0xc;
            *(int*)(slot + 0x20) = y;
            *(int*)(slot + 0x24) = (short)record[2] << 0xc;

            *(uint*)(slot + 0x40) = 0;
            *(short*)(slot + 0x36) = 1;
            *(int*)(slot + 0x60) = y;
            *(int*)(slot + 0x64) = y;
            *(int*)(slot + 0x5c) = 0x5000;
            *(uint*)(slot + 0x18) = 0;
            *(int*)(slot + 0x50) = 0;
            *(uint*)(slot + 0x54) = 0;
            *(int*)(slot + 0x58) = 0;
            *(ushort*)(slot + 0x9a) = 0x3000;
            *(short*)(slot + 0x96) = 0;
            *(ushort*)(slot + 0x98) = param3;

            uint flags = ((uint)*(ushort*)(species + 6) | 0xff810080u) << 8;
            *(uint*)slot = flags;

            if (mode == 0)
            {
                *(short*)(slot + 0x28) = 0;
                *(uint*)slot = flags | 0x10000000;
                *(short*)(slot + 0x2a) = 0x4000;
                *(short*)(slot + 0x2c) = 0;
                *(uint*)(slot + 0x38) = 0;
            }
            else
            {
                *(uint*)(slot + 0x38) = 0xffffff;
                *(short*)(slot + 0x28) = 0x1000;
                *(short*)(slot + 0x2a) = 0x1000;
                *(short*)(slot + 0x2c) = 0x1000;
            }

            *(uint*)(slot + 0x10) = 0;
            *(int*)(slot + 0x14) = record[6];

            if (record[4] == -1 && record[5] == -1 || record[4] == 0 && record[5] == 0)
            {
                *(int*)(slot + 0x60) = y;
                *(uint*)(slot + 0x80) = 0xa0000000;
                *(uint*)(slot + 0x84) = 0xa0000000;
                *(uint*)(slot + 0x88) = 0xa0000000;
                *(uint*)(slot + 0x8c) = 0xa0000000;
                *(int*)(slot + 0x64) = y;
                TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)slot, 0x8000);
            }
            else
            {
                CopyEntityReferences(slot, (byte*)record);
            }

            FinalizeEntityPlacement2((uint*)slot);

            if (*(int*)(ctx + 0x98) != 0)
            {
                ResetEntityStats(stats, species, model);
            }

            kind = record[8];
            record += 8;
            spawned++;
        }

        return spawned;
    }

    /// <summary>
    /// Ghidra: FUN_005035c0 @ 0x005035c0 — <b>passe la vue a une camera de scenette</b> : met de
    /// cote les trois blocs de vue courants, puis fait pointer le jeu sur des copies de travail
    /// qu'un script de camera pourra remuer sans perdre l'original.
    ///
    /// <para>
    /// Les trois blocs redirigees sont ceux dont l'adresse vit dans une variable : le vecteur de
    /// vue (<c>DAT_005e9ae0</c>, trois <c>short</c>), le bloc de camera (<c>DAT_005edebc</c>, trois
    /// <c>int</c>) et le bloc de cible (<c>DAT_005eca94</c>, trois <c>int</c>). Chacun est recopie
    /// **deux fois** dans l'etat d'entite : une copie de travail, sur laquelle la variable est
    /// ensuite pointee, et une copie de sauvegarde pour la restitution.
    /// </para>
    /// <para>
    /// La disposition des sauvegardes n'est pas symetrique, et c'est transcrit tel quel : celle du
    /// bloc de camera est coupee en deux (<c>0x005719f8</c>, <c>0x005719fc</c>, puis
    /// <c>0x00571a00</c> a l'ecart), la ou celle du bloc de cible est d'un seul tenant
    /// (<c>0x00571a28..0x00571a30</c>). Rien dans le corps ne justifie l'ecart ; il vient
    /// vraisemblablement de l'ordre dans lequel les champs ont ete ajoutes en 1998.
    /// </para>
    /// <para>
    /// Le garde <c>script &gt; 0xfffe</c> ne peut pas se declencher depuis l'opcode 29, qui ne passe
    /// qu'un octet. Conserve : la fonction a d'autres appelants, et c'est son seul chemin de sortie
    /// anticipe.
    /// </para>
    /// </summary>
    /// <param name="entity">Ghidra <c>param_1</c> — l'entite qui porte la scenette.</param>
    /// <param name="script">Ghidra <c>param_2</c> — l'index dans la table de scripts de camera du
    /// repertoire de zone (<c>+0xbc</c>).</param>
    /// <param name="mode">Ghidra <c>param_3</c> — un <c>ushort</c> range a <c>0x005719d8</c>.</param>
    private static void BeginScriptedCamera(byte* entity, int script, ushort mode)
    {
        if (script > 0xfffe)
        {
            *(byte**)Mem.EntityStateBytes(0x005719b4) = null;
            return;
        }

        // Le mode de vue precedent n'est mis de cote que s'il n'etait pas deja celui de la
        // scenette : deux entrees successives ne doivent pas ecraser l'original par un « 2 ».
        if (s_Unknown_005de54c != 2)
        {
            Saves.s_SavedViewMode_00604b7c = s_Unknown_005de54c;
            s_Unknown_005de54c = 2;
        }

        *(byte**)Mem.EntityStateBytes(0x005719c4) = entity;
        *Mem.EntityState(0x00571a34) = 0;
        Saves.s_CutsceneActive_00604b78 = 1;
        *(ushort*)Mem.EntityStateBytes(0x005719d8) = mode;

        *(byte**)Mem.EntityStateBytes(0x005719b4) =
            *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xbc) + script * 4);

        // --- le vecteur de vue : trois shorts, copie de travail puis sauvegarde ---
        short* view = *(short**)Mem.Arena(0x005e9ae0);
        short* viewWork = (short*)Mem.EntityStateBytes(0x005719e8);
        short* viewSaved = (short*)Mem.EntityStateBytes(0x005719f0);

        viewWork[0] = view[0];
        viewWork[1] = view[1];
        viewWork[2] = view[2];
        viewSaved[2] = view[2];                              // Ghidra: _DAT_005719f4, ecrit avant les deux autres
        viewSaved[0] = view[0];
        viewSaved[1] = view[1];
        *(short**)Mem.Arena(0x005e9ae0) = viewWork;

        // --- le bloc de camera : trois ints, sauvegarde coupee en deux ---
        int* camera = *(int**)Mem.Entity(0x005edebc);
        int* cameraWork = Mem.EntityState(0x00571a18);

        cameraWork[0] = camera[0];
        cameraWork[1] = camera[1];
        cameraWork[2] = camera[2];
        *Mem.EntityState(0x00571a00) = camera[2];
        *Mem.EntityState(0x005719f8) = camera[0];
        *Mem.EntityState(0x005719fc) = camera[1];
        *(int**)Mem.Entity(0x005edebc) = cameraWork;

        // --- le bloc de cible : trois ints, sauvegarde d'un seul tenant ---
        int* target = *(int**)Mem.Entity(0x005eca94);
        int* targetWork = Mem.EntityState(0x00571a08);
        int* targetSaved = Mem.EntityState(0x00571a28);

        targetWork[0] = target[0];
        targetWork[1] = target[1];
        targetWork[2] = target[2];
        targetSaved[0] = target[0];
        targetSaved[1] = target[1];
        targetSaved[2] = target[2];
        *(int**)Mem.Entity(0x005eca94) = targetWork;
    }

    // =============================================================================================
    // Les trois opcodes de structure, releves au desassembleur le 2026-08-21 : aucun des trois
    // n'est dans Blaze.c, Ghidra ayant nomme la table `PTR_LAB_` et n'ayant donc pas reconnu ces
    // corps comme des fonctions.
    //
    // Ce sont eux qui rendent la boucle de RunScript intelligible : l'opcode 1 est le saut, et
    // l'opcode 2 est la moitie manquante du mecanisme de condition — celui que la boucle laisse
    // passer alors meme qu'un resultat est pose.
    // =============================================================================================

    /// <summary>Ghidra: 0x005154c0 (opcode 1) — <b>saut inconditionnel</b>. L'operande est un
    /// <c>ushort</c> forme de <c>script[1]</c> et <c>script[2]</c>, et il indexe la meme table de
    /// scripts (<c>contexte + 0x8c</c>) que l'entree de l'interpreteur. Le corps tient en six
    /// instructions et ne touche ni a l'entite ni au resultat.</summary>
    private static byte* ScriptOpJump(byte* ctx, byte* s)
        => *(byte**)(*(byte**)(ctx + 0x8c) + (s[1] | s[2] << 8) * 4);

    /// <summary>
    /// Ghidra: 0x005154e0 (opcode 2) — <b>saut conditionnel</b>, et la piece qui explique toute la
    /// boucle.
    ///
    /// <para>
    /// Un resultat nul signifie « condition fausse » : l'instruction est enjambee, trois octets plus
    /// loin. Un resultat non nul fait sauter vers <c>scripts[operande]</c> <b>et remet le resultat a
    /// zero</b>, ce qui laisse la boucle continuer.
    /// </para>
    /// <para>
    /// C'est pour cela que <see cref="RunScript"/> traite l'opcode 2 a part dans sa condition
    /// d'arret : tous les autres opcodes arretent la boucle des qu'ils posent un resultat, celui-ci
    /// est le seul a la reprendre. Sans lui, un test de script arreterait le script pour de bon.
    /// </para>
    /// </summary>
    private static byte* ScriptOpJumpIf(byte* ctx, byte* s, uint* result)
    {
        if (s_TraceScript)
        {
            Console.Error.WriteLine(
                $"[SAUT] condition={*result} -> " +
                (*result == 0 ? "PAS de saut" : $"saut vers le script {s[1] | s[2] << 8}"));
        }

        if (*result == 0)
        {
            return s + 3;
        }

        byte* target = *(byte**)(*(byte**)(ctx + 0x8c) + (s[1] | s[2] << 8) * 4);
        *result = 0;
        return target;
    }

    /// <summary>
    /// Ghidra: 0x00515850 (opcode 11) — <b>empile un octet</b> sur la petite pile de 0x005fa560.
    ///
    /// <para>
    /// Le premier octet du bloc est le compte ; il est incremente <b>avant</b> l'ecriture, si bien
    /// que le premier element empile atterrit a <c>+1</c> et que le compte n'est jamais ecrase par
    /// une donnee. Meme disposition que le pool d'emplacements d'effets, 0x40 octets plus loin.
    /// </para>
    /// <para>
    /// TODO(REVERSE): aucun site transcrit ne DEPILE encore cette pile — sa capacite et son
    /// consommateur restent a etablir. Le compte etant un octet incremente sans borne, un script
    /// qui empile plus de 255 fois le fait reboucler sur lui-meme ; savoir si c'est atteignable
    /// demande le consommateur.
    /// </para>
    /// </summary>
    private static byte* ScriptOpPushPending(byte* s)
    {
        byte* pending = Mem.Entity(0x005fa560);
        byte count = (byte)(*pending + 1);
        *pending = count;
        pending[count] = s[1];
        return s + 2;
    }

    /// <summary>
    /// Ghidra: 0x00515490 (opcode 0) — <b>appelle un verbe de zone</b>, choisi dans la table
    /// <c>&amp;PTR_FUN_0056aec8</c> par <c>script[1]</c>, avec l'entite et un <c>ushort</c> forme de
    /// <c>script[2]</c> et <c>script[3]</c>.
    ///
    /// <para>
    /// La table n'est pas celle du binaire : <c>InstallEntityVTables</c> (branche
    /// <c>DAT_00600904</c> de <c>FUN_0052a660</c>) la remplit au chargement de zone depuis
    /// <c>(&amp;PTR_PTR_00567318)[zone * 0x10 + sous-zone]</c>. Le contenu de l'index 0 — le seul que
    /// la carte du monde atteint — est deja releve dans <c>Engine/Resources.cs</c>.
    /// </para>
    /// <para>
    /// <b>Le resultat de condition est un reste de registre, et c'est verifiable.</b> Le
    /// gestionnaire fait <c>xor eax,eax ; mov al,[esi+1]</c> pour construire l'index, puis
    /// <c>call dword [eax*4+56AEC8h]</c> et <c>mov [ecx],eax</c>. Ni le <c>call</c> ni un corps
    /// <c>ret</c> nu ne touchent <c>EAX</c> : quand le verbe est la souche <c>FUN_00401FA0</c>, le
    /// resultat recoit donc <b>l'indice du verbe lui-meme</b>. Ce n'est pas une reconstruction
    /// hasardeuse — c'est ce que le processeur fait, et la boucle s'arrete ou non selon que cet
    /// indice est nul.
    /// </para>
    /// <para>
    /// Tout verbe qui n'est pas la souche leve en se nommant, comme
    /// <see cref="Resources.CallAreaHandler"/> le fait pour la table de zone : la valeur de retour
    /// d'une vraie fonction ne peut pas etre devinee.
    /// </para>
    /// </summary>
    /// <summary>
    /// Trace des verbes de zone, sous <c>OPENFORESIA_TRACE_VERBS=1</c>
    /// (<c>playtest.ps1 -Trace VERBS</c>).
    ///
    /// <para>Elle est permanente, pas jetable, et pour une raison precise : <b>savoir quel verbe un
    /// decor declenche ne se devine pas</b>. Les tables de verbes sont installees par zone et les
    /// scripts vivent dans les donnees du disque — ni <c>Blaze.c</c> ni le desassemblage ne disent
    /// quel numero un coffre, une porte ou un marchand appelle. Seul le jeu le dit.</para>
    /// </summary>
    private static readonly bool s_TraceVerbs =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_VERBS") == "1";

    private static byte* ScriptOpAreaVerb(byte* ent, byte* s, uint* result)
    {
        uint[] verbs = Res.s_Vt_0056aec8;
        byte verb = s[1];

        if (s_TraceVerbs)
        {
            uint traced = verb < verbs.Length ? verbs[verb] : 0;
            Console.Error.WriteLine(
                $"[VERBE] {verb} -> 0x{traced:X6}  operande 0x{(ushort)(s[2] | s[3] << 8):X4}");
        }

        // L'indice est un octet, la table n'a que 0x40 entrees — et dans le binaire les six tables
        // sont CONTIGUES (0x0056aec8 + 0x40*4 = 0x0056afc8). Un indice de 0x40 ou plus lisait donc
        // la table suivante, ce qui est peut-etre voulu et peut-etre un debordement. Ici les tables
        // sont des tableaux separes : on ne peut pas reproduire la lecture, alors on le dit.
        if (verb >= verbs.Length)
        {
            throw new NotImplementedException(
                $"Interpreteur de script, opcode 0 : verbe {verb} au-dela des 0x40 entrees de la " +
                "table 0x0056aec8. Dans le binaire cette lecture tombe dans 0x0056afc8, la table " +
                "suivante, car les six sont contigues ; ici elles sont des tableaux separes. " +
                "Etablir si le debordement est voulu avant de le reproduire — voir " +
                "Documentation/Reference/legacy-bug.md, LB-006 pour un cas analogue deja tranche.");
        }

        uint target = verbs[verb];

        // La sonde de couverture. Le repartiteur de VERBES est le second point d'entree d'une
        // table de sauts, a cote de DispatchEntityHandler — et il avait ete oublie : le verbe 8,
        // qui a plante Vincent le 2026-08-30, n'apparaissait pas dans handlers-exerces.tsv alors
        // que le jeu l'avait bel et bien atteint. Les 67 verbes de la table 0x0056aec8 ne
        // pouvaient donc jamais passer a « teste ».
        NoteHandlerSeen(target);

        *result = target switch
        {
            // Souche `ret` nu, verifiee au desassembleur : elle ne touche pas EAX, qui contient
            // encore l'indice de verbe au moment du `mov [ecx],eax`.
            0x00401FA0 => verb,

            // 0x0040A600 : la seconde souche `ret` nu (c3 seul, verifie au desassembleur). Meme
            // registre, meme resultat : l'indice du verbe.
            0x0040A600 => verb,

            // Le compte des membres vivants, range dans la banque 2 (mine, sous-zones 0 a 2).
            0x004431B0 => AreaVerb_004431B0((short)(s[2] | (s[3] << 8))),

            // 0x004441F0 : `xor eax,eax ; ret`, verifie au desassembleur. A distinguer de la souche
            // ci-dessus — celle-ci met EAX a zero explicitement, donc le resultat de condition est
            // 0 et non l'indice du verbe. Deux souches, deux comportements.
            0x004441F0 => 0,

            0x00442D90 => AreaVerb_00442D90(ent, (short)(s[2] | (s[3] << 8))),

            // Le verbe du LABYRINTHE DES MORTS qui remet a zero les contacts du groupe. Il ignore
            // l'entite appelante : son corps ne lit que [esp+8], donc le second argument.
            0x00444340 => AreaVerb_00444340((short)(s[2] | (s[3] << 8))),

            // Ses deux voisins de table (2026-09-18, lot 8) : le silence du bloc de son, et « le
            // groupe entier a-t-il franchi la ligne ? ». Le premier ne lit aucun argument.
            0x004443D0 => AreaVerb_004443D0(),
            0x004444A0 => AreaVerb_004444A0((short)(s[2] | (s[3] << 8))),

            // LES QUATRE VERBES DU CHATEAU DE L'IMMORTEL (palier 2, 2026-09-18). Les deux premiers
            // sont le meme don d'objet, a un rappel de fin de boite pres.
            0x00444670 => AreaVerb_00444670(ent, (short)(s[2] | (s[3] << 8))),
            0x00444A40 => AreaVerb_00444A40(ent, (short)(s[2] | (s[3] << 8))),
            0x00444E00 => AreaVerb_00444E00((short)(s[2] | (s[3] << 8))),

            // Celui-ci ne lit aucun de ses deux arguments.
            0x004450A0 => AreaVerb_004450A0(),

            // LES DEUX VERBES DES RUINES SOUTERRAINES (2026-09-19), creneaux 0 et 2 du bloc
            // 0x0055a2a8 : les porteurs d'effet relaches, et les membres debout comptes dans
            // l'octet 1 de la banque 2. Ni l'un ni l'autre ne lit un argument.
            0x00445120 => AreaVerb_00445120(),
            0x004450E0 => AreaVerb_004450E0(),

            // LES QUATRE VERBES DES RUINES DANS LE LAC (2026-09-19) : tous lisent le bit 8 de
            // l'identite des membres. Le premier est une question sur le groupe et l'entite ; les
            // trois autres font parler le premier membre debout qui a (ou n'a pas) ce bit.
            0x004451B0 => AreaVerb_004451B0(ent),
            0x00445250 => AreaVerb_00445250(),
            0x004452D0 => AreaVerb_004452D0((short)(s[2] | (s[3] << 8))),
            0x00445360 => AreaVerb_00445360(),

            // Le verbe de la vallee d'argent (2026-09-19) : rappeler les membres ecartes.
            0x00445150 => AreaVerb_00445150(),

            // LES SIX VERBES DE LA GROTTE SCELLEE (2026-09-19, lot 2). Trois lisent l'entite
            // appelante, aucun ne lit l'operande.
            0x004457E0 => AreaVerb_004457E0(),
            0x004454C0 => AreaVerb_004454C0(ent),
            0x00445740 => AreaVerb_00445740(),
            0x00445810 => AreaVerb_00445810(ent),
            0x004457A0 => AreaVerb_004457A0(ent),
            0x00445760 => AreaVerb_00445760(),

            // Lot 4 (2026-09-19) : la vue saute sur le membre que designe l'operande.
            0x004453E0 => AreaVerb_004453E0((short)(s[2] | (s[3] << 8))),

            // Lot 5 (2026-09-19) : pousser l'entite d'un pas, deux copies a leur pas pres.
            0x004454E0 => AreaVerb_004454E0(ent, (short)(s[2] | (s[3] << 8)), 0x18000),
            0x00445610 => AreaVerb_004454E0(ent, (short)(s[2] | (s[3] << 8)), 0x8000),

            // PALIER 3 (2026-09-18). Les deux premiers sont le meme verbe — un effet inflige au
            // membre que designe la banque 2 —, le second sans nombres flottants ; le troisieme
            // lance un script quand tout le groupe a quitte le rectangle. Aucun ne lit l'entite.
            0x004446F0 => AreaVerb_004446F0((short)(s[2] | (s[3] << 8))),
            0x00444AF0 => AreaVerb_00444AF0((short)(s[2] | (s[3] << 8))),
            0x00444EF0 => AreaVerb_00444EF0((short)(s[2] | (s[3] << 8))),

            0x00442F60 => AreaVerb_00442F60(),

            0x00443100 => AreaVerb_00443100(),

            // Le « tout effacer » des quatre bancs de variables de script (verbe 6 en foret).
            0x00443260 => AreaVerb_00443260(),

            // Deux verbes courts de la meme table, relevés dans la foulée du precedent.
            0x00442E10 => AreaVerb_00442E10(),
            0x004432E0 => AreaVerb_004432E0(),

            // Le verbe 62 : « suis-je le dernier membre vivant ? »
            0x00445480 => AreaVerb_00445480(ent),

            // Le verbe 56 : l'expertise d'objet. Il ignore le second argument — son corps ne lit
            // que [esp+0x14], c'est-a-dire l'entite.
            0x0053DD20 => AreaVerb_0053DD20(ent),

            // Les verbes 57 et 58 : LE COFFRE, en mode 0 et en mode 1. Les deux corps etaient
            // ecrits depuis le 2026-08-25 — avec tout le panneau qu'ils ouvrent, WorldMapDepot.cs —
            // mais n'avaient jamais ete branches ici : le repartiteur levait sur les deux.
            0x0053DEA0 => AreaVerb_0053DEA0(ent),
            0x0053DF20 => AreaVerb_0053DF20(ent),

            // LES SIX VERBES DE L'AUBERGE, pris en une passe le 2026-09-15 : la remise d'un objet
            // a tout le groupe (12), la recherche de l'Ecu du destin (7), les deux lectures du
            // plancher de niveau (5 et 6), la recharge des sept compteurs de sorts (4) et le
            // compte des neuf drapeaux (2). Les quatre premiers rendent leur resultat dans la
            // BANQUE 0x005e9060, pas dans le retour.
            0x00442DB0 => AreaVerb_00442DB0(),
            0x00442E20 => AreaVerb_00442E20(),
            0x00442E70 => AreaVerb_00442E70(),
            0x00442EA0 => AreaVerb_00442EA0(),
            0x00442F20 => AreaVerb_00442F20(),
            0x004430B0 => AreaVerb_004430B0(),

            // Le verbe 5 : le rocher qui publie le titre tire du compteur de victimes. Il ne lit
            // ni l'entite ni le second argument — son creneau vient de la variable de script 4.
            0x004438A0 => AreaVerb_004438A0(),

            // Les verbes 8 et suivants : deux EFFETS SONORES, et rien d'autre. Les deux plus
            // employes du jeu — 78 emplois chacun — pour vingt-huit octets. Le 8 est celui de la
            // porte, signale le 2026-08-30.
            0x004457F0 => AreaVerb_004457F0(),
            0x00445840 => AreaVerb_00445840(),

            // Trois verbes de plus : deux comptes (membres debout, drapeaux poses) et la PORTE,
            // dont les cinq etats ouvrent puis referment l'animation d'une entite de sous-type 3.
            0x00443960 => AreaVerb_00443960((short)(s[2] | (s[3] << 8))),
            0x004439A0 => AreaVerb_004439A0((short)(s[2] | (s[3] << 8))),
            0x00443A00 => AreaVerb_00443A00(ent),

            // Six verbes de plus, tous relevés au désassembleur : deux ordres (l'effet 0x59 sur
            // tout le groupe, le réveil de la scène), deux relevés d'état (la classe du locuteur
            // rangée dans la banque 2, le niveau du groupe comparé à un seuil) et la boîte de
            // dialogue attachée au membre que la banque 2 désigne.
            0x004432A0 => AreaVerb_004432A0(),
            0x004431F0 => AreaVerb_004431F0(ent),
            0x00443830 => AreaVerb_00443830((short)(s[2] | (s[3] << 8))),
            0x00444E60 => AreaVerb_00444E60(),
            0x00443300 => AreaVerb_00443300((short)(s[2] | (s[3] << 8))),

            // La meme boite de dialogue sans garde-fou, mine abandonnee, sous-zones 0 a 2.
            0x00443170 => AreaVerb_00443170((short)(s[2] | (s[3] << 8))),
            0x00443B20 => AreaVerb_00443B20(),

            // Le verbe 13 : le rassemblement du groupe sur l'un des quatre emplacements des deux
            // clairieres. Le plus gros verbe de la table, et le premier a ne toucher qu'aux
            // creneaux d'entite de zone — il ignore l'entite qu'on lui passe.
            0x004433B0 => AreaVerb_004433B0(),

            // LES QUATRE VERBES SIMPLES DE LA VIEILLE TOUR (categorie B de Zone4ParComplexite.md).
            // Ils vont par paires de creneau selon la sous-zone, pas par sujet : voir le bandeau de
            // leurs corps. 0x00443CB0 est le premier verbe dont le resultat de condition soit un
            // reste de registre NON constant.
            0x00443C60 => AreaVerb_00443C60(),
            0x00443CB0 => AreaVerb_00443CB0(),
            0x004442F0 => AreaVerb_004442F0(),
            0x00444200 => AreaVerb_00444200(),

            // Le colocataire du creneau 11 : le minuteur qui retourne les vues des sous-zones 0 a 3
            // (categorie D de Zone4ParComplexite.md).
            0x00443D00 => AreaVerb_00443D00(),

            // LES TROIS VERBES SIMPLES DU LABYRINTHE DES MORTS (categorie B de
            // Zone5ParComplexite.md). Le creneau 5 est le meme dans les TROIS blocs de la zone ;
            // le creneau 7 a deux corps STRICT identiques, un par groupe de sous-zones.
            0x00444620 => AreaVerb_00444620(),
            0x00444400 => AreaVerb_00444400(),
            0x00444450 => AreaVerb_00444450(),

            _ => throw new NotImplementedException(
                $"Interpreteur de script, opcode 0 : verbe {verb} = 0x{target:X6}, pas encore " +
                "reconstruit. Son adresse vient de la table 0x0056aec8 de la zone courante " +
                "(Engine/EntityVTables.cs, relevee pour les onze zones). " +
                "La transcrire, puis la brancher ici : " +
                $"`OpenForesia.Tools disasm Legacy/Binaries/Blaze.exe 0x{target:x8} 512` si elle " +
                "n'est pas dans Blaze.c. L'argument passe est (entite, ushort(script[2], script[3])), " +
                "et la valeur de retour devient le resultat de condition."),
        };

        return s + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00442F60 @ 0x00442f60 — <b>le verbe 3 de la table de zone</b>, celui que le
    /// premier script de la carte du monde appelle. 336 octets, <b>absente de <c>Blaze.c</c></b> :
    /// relevee au desassembleur, dump conserve dans
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442F60.asm</c>.
    ///
    /// <para>
    /// Deux moities. La premiere <b>decale d'un cran</b> les dix bits bas du banc
    /// <c>0x005e9000</c> vers le banc <c>0x005e9840</c> — celui-la meme sur lequel porte l'opcode 7
    /// de l'interpreteur. Le bit <c>0x10</c> de <c>0x005e9001</c> commute vers l'autre branche, qui
    /// ne garde qu'un bit de chaque octet au lieu de les propager.
    /// </para>
    /// <para>
    /// La seconde choisit une piste musicale d'apres <c>0x005e96fe</c> et la lance. Le calcul est
    /// une identite bornee : 0, 1 et 2 se rendent eux-memes, <b>toute autre valeur rend 0</b>. Le
    /// <c>setne / dec / and 2</c> de l'original est une facon sans branchement d'ecrire ca, et il
    /// est rendu ici par ce qu'il calcule, pas par sa forme.
    /// </para>
    /// <para>
    /// La piste est enfin recopiee dans deux octets designes par le repertoire de zone, mais
    /// <b>seulement si la sous-zone est un multiple de 8 jusqu'a 0x50</b> — l'original le teste par
    /// onze comparaisons successives, rendues ici par le predicat qu'elles forment.
    /// </para>
    /// <para>
    /// Elle rend <b>0</b> (<c>xor eax,eax ; ret</c>), donc le resultat de condition est nul et le
    /// script continue. C'est le seul verbe dont on connaisse la valeur de retour pour l'instant.
    /// </para>
    /// </summary>
    /// <summary>
    /// Ghidra: FUN_00443100 @ 0x00443100 — <b>le verbe 0 de la table de zone</b>, celui qu'un
    /// dialogue de PNJ demande. 108 octets, <b>absente de <c>Blaze.c</c></b> : relevee au
    /// desassembleur, dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00443100.asm</c>.
    ///
    /// <para>
    /// <b>Elle degele trois tables d'entites</b>, et seulement celles-la : <c>0x005ff400</c>
    /// (0x20 emplacements), <c>0x005f2d40</c> (0x80) et <c>0x005edee0</c> (0x80). Pour chaque
    /// enregistrement dont le mot d'etat ne porte <b>pas</b> le bit <c>0x10000000</c> — c'est-a-dire
    /// qui n'est pas retire — elle efface le bit <c>0x04000000</c>, celui-la meme que
    /// <see cref="FreezeAllEntities"/> pose.
    /// </para>
    /// <para>
    /// Les trois passes sont ecrites en clair, l'une apres l'autre, dans le binaire ; la boucle
    /// commune ci-dessous est une factorisation de forme, pas un decoupage : les trois corps sont
    /// identiques a l'adresse et au compte pres.
    /// </para>
    /// <para>
    /// Noter <b>quelles tables sont absentes</b> : ni le groupe (<c>0x005e93c0</c>), ni les entites
    /// de zone (<c>0x005fa5e0</c>), ni celles de scene (<c>0x005ecaa0</c>), ni les projectiles
    /// (<c>0x005f7de0</c>). Un dialogue relache donc les effets, les incantations et les sorts
    /// d'etat, mais laisse le monde fige — ce qui est exactement le comportement attendu quand la
    /// boite de dialogue reste affichee.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero : le corps finit par <c>xor eax,eax ; ret</c>.</returns>
    private static uint AreaVerb_00443100()
    {
        ThawEntityTable(Mem.Entity(0x005ff400), 0x20);
        ThawEntityTable(Mem.Entity(0x005f2d40), 0x80);
        ThawEntityTable(Mem.Entity(0x005edee0), 0x80);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00443260 @ 0x00443260 — <b>remet a zero les quatre bancs de variables de
    /// script</b>. 53 octets, <b>absente de <c>Blaze.c</c></b> ; relevee au desassembleur, dump
    /// conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00443260.asm</c>.
    ///
    /// <para>
    /// Quatre <c>rep stosd</c> de <c>0x10</c> mots, soit 0x40 octets chacun, sur <c>0x005e9840</c>,
    /// <c>0x005e9060</c>, <c>0x005e9780</c> et <c>0x005e96e0</c> — dans cet ordre, soit les banques
    /// <b>3, 2, 1 et 0</b> des huit que la sauvegarde restaure. <b>Ce sont exactement les quatre
    /// bancs des opcodes 4 a 7 de l'interpreteur</b>
    /// (<see cref="ScriptOpAluArena"/>) : ce verbe est le « tout effacer » de la memoire de script,
    /// pas une operation d'affichage.
    /// </para>
    /// <para>
    /// L'ordre des quatre n'est pas celui des opcodes (7, 6, 5, 4) et il est transcrit tel quel :
    /// les etendues sont disjointes, donc l'ordre ne change rien, mais rien ne justifie de le
    /// reecrire.
    /// </para>
    /// <para>
    /// <b>Elle rend 0</b> : le <c>xor eax,eax</c> de <c>0x00443266</c> sert de valeur a stocker
    /// pour les <c>stosd</c>, et <c>rep stosd</c> ne touche pas EAX. Le registre vaut donc encore
    /// zero au <c>ret</c>, et le resultat de condition est nul.
    /// </para>
    /// <para>
    /// <b>C'est le verbe 6 de la zone 3</b>, la foret — et le verbe 1 de la zone 0. Le dispatch se
    /// fait sur l'adresse et non sur l'indice, donc le remappage d'une zone a l'autre est sans
    /// effet ici ; c'est precisement ce que cette forme de <c>switch</c> achete.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00443260()
    {
        ClearScriptBank(Mem.s_StateBank3_005e9840);
        ClearScriptBank(Mem.s_StateBank2_005e9060);
        ClearScriptBank(Mem.s_StateBank1_005e9780);
        ClearScriptBank(Mem.s_StateBank0_005e96e0);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004431b0</c> — <b>« combien de membres du groupe sont encore debout ? »</b>
    /// Cinquante-quatre octets, <b>absente de <c>Blaze.c</c></b> ; relevé gardé sous
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004431B0.asm</c>. Créneau 12 du bloc de verbes
    /// <c>0x00553aa8</c> (mine, sous-zones 0 à 2) et créneau 16 de <c>0x00558f28</c>.
    ///
    /// <para>
    /// Elle balaie les <b>quatre</b> emplacements de <c>0x9c</c> octets du groupe, comme
    /// <see cref="AreaVerb_00442DB0"/>, et compte ceux dont le mot d'état porte le bit de vie
    /// <c>0x80000000</c> <b>sans</b> <c>0x40000000</c> (mort) ni <c>0x10000000</c> (retiré) — le
    /// <c>test eax,50000000h</c> les prend ensemble. Le compte, un octet, est écrit dans la
    /// <b>banque 2</b> (<c>0x005e9060</c>) à l'indice que l'opérande désigne : le script le relira
    /// par les opcodes de banque.
    /// </para>
    /// <para>
    /// <b>L'indice est signé</b> (<c>movsx eax, word [esp+0Ch]</c>, le second argument une fois
    /// <c>ebx</c> empilé) et <b>aucune borne n'est vérifiée</b> — même situation que
    /// <see cref="AreaVerb_00442D90"/>, transcrite telle quelle.
    /// </para>
    /// </summary>
    /// <returns>Toujours zéro : <c>xor eax,eax</c> avant le <c>ret</c>.</returns>
    private static uint AreaVerb_004431B0(short index)
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;
        byte standing = 0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint word = *(uint*)slot;
            if ((word & 0x80000000) != 0 && (word & 0x50000000) == 0) standing++;
        }

        Mem.s_StateBank2_005e9060[index] = standing;
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004438a0</c> — <b>le rocher qui donne un titre</b>. 179 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; relevé gardé sous
    /// <c>Legacy/Raw/Blaze/FUN_004438A0.asm</c>. Signalé en jeu par Vincent le 2026-08-30 : le
    /// rocher qui « donne des statistiques sur notre personnage » plantait ici.
    ///
    /// <para>
    /// <b>Elle convertit un compteur de victimes en rang.</b> Le membre visé est l'octet bas de la
    /// <b>variable de script 4</b> ; son bloc de personnage est adressé comme partout ailleurs,
    /// <c>MainMem + (creneau + 0x78) * 0x2000</c>. Le compteur lu en <c>+0x15c</c> est celui que
    /// <see cref="ResolveMeleeHit_00539B80"/> incrémente à chaque mise à mort, et le rang écrit en
    /// <c>+0x1ff8</c> est l'indice de libellé que le pavé de statistiques relit
    /// (<c>libellés + 0x500 + rang * 4</c>, dans <c>WorldMapMenu.cs</c>). Le rocher ne fait donc
    /// que <b>recalculer et publier le titre</b>.
    /// </para>
    /// <para>
    /// <b>L'échelle est un doublement.</b> Treize seuils — 10, 20, 40, 80, 160, 320, 640, 1280,
    /// 2560, 5120, 10 240, 20 480, 40 960 — franchis un par un, ce qui donne un rang de 0 à 13,
    /// soit quatorze titres. Le premier seuil <b>pose</b> 1 (<c>mov ecx,1</c>) au lieu
    /// d'incrémenter ; les douze autres incrémentent. Sous dix victimes le rang est nul.
    /// </para>
    /// <para>
    /// <b>Les comparaisons sont NON signées</b> (<c>jb</c>), alors que le compteur est un
    /// <c>int</c> que le jeu borne ailleurs à <c>-1</c>. Un compteur négatif franchirait donc tous
    /// les seuils et donnerait le rang maximal. Transcrit tel quel — c'est ce que le binaire
    /// calcule.
    /// </para>
    /// <para>
    /// <b>Elle écrit son résultat dans deux vues de la même banque.</b> Le compteur brut part sur
    /// <b>quatre octets</b> à la variable 4 — donc par-dessus les variables 4 à 7, y compris le
    /// numéro de créneau qu'elle venait d'y lire — et le rang sur un octet à la variable 8. C'est
    /// la banque 2, celle que le moteur de texte relit pour composer sa ligne. Le champ
    /// <c>+0x15c</c> est relu du bloc après cette écriture, et non repris du registre : les deux
    /// lectures sont conservées, elles rendent la même valeur.
    /// </para>
    /// </summary>
    /// <returns>Toujours zéro — <c>xor eax,eax ; ret</c>, donc le script continue.</returns>
    private static uint AreaVerb_004438A0()
    {
        byte* bank = Mem.s_StateBank2_005e9060;

        // Ghidra: `mov eax,[5E9064h] ; and eax,0FFh` — la variable 4 lue sur 32 bits, dont seul
        // l'octet bas designe le creneau.
        uint slot = *(uint*)(bank + 4) & 0xff;
        byte* block = GetMainMemPtr() + (slot + 0x78) * 0x2000;

        bank[8] = 0;
        *(uint*)(bank + 4) = *(uint*)(block + 0x15c);

        // Ghidra: `mov edx,[eax+15Ch]` une seconde fois, apres l'ecriture ci-dessus.
        uint kills = *(uint*)(block + 0x15c);

        // Ghidra: le premier seuil POSE 1, les douze suivants incrementent. Comparaisons `jb`,
        // donc non signees.
        int rank = kills >= 0x0a ? 1 : 0;
        if (kills >= 0x14) rank++;
        if (kills >= 0x28) rank++;
        if (kills >= 0x50) rank++;
        if (kills >= 0xa0) rank++;
        if (kills >= 0x140) rank++;
        if (kills >= 0x280) rank++;
        if (kills >= 0x500) rank++;
        if (kills >= 0xa00) rank++;
        if (kills >= 0x1400) rank++;
        if (kills >= 0x2800) rank++;
        if (kills >= 0x5000) rank++;
        if (kills >= 0xa000) rank++;

        bank[8] = (byte)rank;

        // Ghidra: `movsx ecx,cx` avant le rangement — l'extension de signe de seize bits est sans
        // effet sur un rang de 0 a 13, mais elle est ce que le binaire fait.
        *(int*)(block + 0x1ff8) = (short)rank;

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004457f0</c> et <c>0x00445840</c> — <b>les deux verbes les plus employes du
    /// jeu</b>, 78 emplois chacun d'apres le recensement, et ce ne sont que <b>deux effets
    /// sonores</b>. Vingt-huit octets chacun, absents de <c>Blaze.c</c> ; releves gardes sous
    /// <c>Legacy/Raw/Blaze/FUN_004457F0.asm</c> et <c>FUN_00445840.asm</c>.
    ///
    /// <para>
    /// Le corps entier tient en un appel : <c>push</c> des quatre constantes,
    /// <c>call 0x0053cea0</c> — le point d'entree sonore, deja transcrit en
    /// <see cref="Audio.PlaySoundEffect"/> —, <c>xor eax,eax</c>, <c>ret</c>. Ils rendent donc
    /// <b>zero</b>, ce qui laisse le script continuer, et ils <b>ignorent leurs deux arguments</b> :
    /// ni l'entite ni le mot du script ne sont lus.
    /// </para>
    /// <para>
    /// <b>Ce que le recensement a appris.</b> Ces deux-la etaient en tete de la liste des
    /// gestionnaires manquants, loin devant tout le reste — et ils coutent deux lignes. C'est
    /// exactement ce que le classement par nombre d'emplois devait faire ressortir : la porte qui
    /// a plante Vincent le 2026-08-30 est le huitieme verbe, et il ne demandait qu'un son.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_004457F0()
    {
        Aud.PlaySoundEffect(0x29, 0x400, 0x1300, 0x1300);
        return 0;
    }

    /// <summary>Ghidra: <c>0x00445840</c> — la jumelle de <see cref="AreaVerb_004457F0"/>, avec un
    /// autre son et un autre gain. C'est le <b>verbe 8</b>, celui de la porte.</summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00445840()
    {
        Aud.PlaySoundEffect(0x1d, 0x300, 0x0c00, 0x0c00);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443960</c> — <b>« combien de membres sont debout ? »</b>. 58 octets, absente
    /// de <c>Blaze.c</c> ; relevé sous <c>Legacy/Raw/Blaze/FUN_00443960.asm</c>.
    ///
    /// <para>
    /// Elle balaie les quatre creneaux de <see cref="Inp.s_SlotFlags_005e93c0"/>, compte ceux qui
    /// portent <c>0x80000000</c> sans <c>0x50000000</c> — vivants et non retires, le meme test que
    /// <see cref="AreaVerb_00445480"/> —, et range le compte dans la <b>variable de script</b> que
    /// son argument designe. Elle rend zero, donc le script continue et lira la variable ensuite.
    /// </para>
    /// <para>
    /// L'argument est masque a huit bits : n'importe quelle valeur designe donc une variable de la
    /// banque, sans borne. C'est le binaire qui le fait.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00443960(short slot)
    {
        int standing = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) standing++;
        }

        Mem.s_StateBank2_005e9060[(uint)slot & 0xff] = (byte)standing;
        return 0;
    }

    // =====================================================================
    // CINQ VERBES DE ZONE DE PLUS (2026-08-30)
    //
    // Tous absents de Blaze.c, tous releves au desassembleur, tous courts : de 26 a 58
    // instructions. Aucun n'a de doublon.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004432a0</c> — <b>« pose l'effet 0x59 sur tout le groupe »</b>. 26 instructions,
    /// absente de <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, sous-zones 5 a 9.
    ///
    /// <para>Elle balaie les quatre fiches d'emplacement et fait naitre le sous-type <c>0x59</c>
    /// sur chacune qui porte <c>0x80000000</c> sans <c>0x50000000</c> — vivante et ni morte ni
    /// absente. Elle ne lit ni l'entite ni l'argument, et rend toujours 0 : c'est un ordre, pas une
    /// question.</para>
    /// </summary>
    private static uint AreaVerb_004432A0()
    {
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            SpawnEffectEntity(member, 0x59, 0, -1);      // Ghidra: FUN_0047a9d0
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004431f0</c> — <b>« range la classe du locuteur dans la banque 2 »</b>. 52
    /// instructions, absente de <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, sous-zones 0 a 4.
    ///
    /// <para>
    /// Elle lit les trois bits bas de l'octet d'identite <c>+0x150</c> de la fiche de personnage —
    /// <c>MainMem + (emplacement + 0x78) * 0x2000</c> — et le range dans le <b>premier octet de la
    /// banque d'etat 2</b> (<c>0x005e9060</c>), d'ou un script ulterieur le relira. Elle rend
    /// toujours 0.
    /// </para>
    /// <para>
    /// <b>Sept comparaisons pour calculer l'identite.</b> Le binaire ne range pas la classe
    /// directement : il part de zero et enchaine <c>cmp al,1</c> … <c>cmp al,7</c>, posant a chaque
    /// fois la constante correspondante. Comme la valeur a deja ete reduite a trois bits, le
    /// resultat vaut exactement l'entree. La cascade est ecrite ici en une affectation, parce que
    /// c'est ce qu'elle calcule ; le desassemblage garde la trace de la forme d'origine.
    /// </para>
    /// <para>
    /// Le binaire porte au passage un <c>jne</c> vers <b>l'instruction suivante</b>
    /// (<c>0x00443213</c>), c'est-a-dire un saut qui ne saute pas. C'est du code mort emis par le
    /// compilateur, sans effet et sans equivalent a transcrire.
    /// </para>
    /// </summary>
    private static uint AreaVerb_004431F0(byte* ent)
    {
        byte* record = GetMainMemPtr() + (ent[8] + 0x78) * 0x2000;
        Mem.s_StateBank2_005e9060[0] = (byte)(record[0x150] & 7);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443830</c> — <b>« tout le groupe est-il sous le niveau N ? »</b>. 38
    /// instructions, absente de <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, toutes sous-zones.
    ///
    /// <para>
    /// Elle part de <b>1</b> et retombe a 0 des qu'un membre vivant atteint ou depasse
    /// <paramref name="level"/> — le niveau se lit en <c>+0x144</c> de la fiche de personnage, le
    /// meme mot que l'ecran d'etat affiche. La reponse est donc « oui » pour un groupe vide, ce qui
    /// est la convention habituelle de ces verbes.
    /// </para>
    /// <para>
    /// <b>Le binaire n'indexe pas par emplacement mais par deux curseurs paralleles</b> : la fiche
    /// de personnage avance de <c>0x2000</c> a partir de <c>0xf0000</c>, la fiche d'emplacement de
    /// <c>0x9c</c> a partir de <c>0x005e93c0</c>. <c>0xf0000</c> est <c>0x78 * 0x2000</c>, la meme
    /// base que partout ailleurs.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00443830(short level)
    {
        short below = 1;

        byte* mainMem = GetMainMemPtr();
        byte* member = Inp.s_SlotFlags_005e93c0;
        int offset = 0xf0000;

        for (int i = 0; i < 4; i++, member += 0x9c, offset += 0x2000)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            if (*(ushort*)(mainMem + offset + 0x144) >= level) below = 0;
        }

        return (uint)below;
    }

    /// <summary>
    /// Ghidra: <c>0x00444e60</c> — <b>« reveille la scene »</b>. 57 instructions, absente de
    /// <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, sous-zones 5 et 9.
    ///
    /// <para>C'est <see cref="ResetEntityWakeBits_0040E640"/> <b>moins sa quatrieme boucle</b> —
    /// celle qui repeint les quatre fiches d'emplacement en blanc. Le corps partage est
    /// <see cref="ResetSceneWakeBits_00444E60"/> ; le verbe n'y ajoute que le <c>return 0</c>.</para>
    /// </summary>
    private static uint AreaVerb_00444E60()
    {
        ResetSceneWakeBits_00444E60();
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443300</c> — <b>« fais parler le membre que la banque 2 designe »</b>. 58
    /// instructions, absente de <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, sous-zones 0 a 4.
    ///
    /// <para>
    /// Elle cherche d'abord le <b>dernier</b> emplacement vivant — la boucle ne s'arrete pas au
    /// premier trouve — puis lit l'octet <c>+2</c> de la banque d'etat 2 : c'est le membre que le
    /// script veut faire parler. Si ce numero tient dans les quatre emplacements, il est employe
    /// tel quel ; sinon on retombe sur le dernier vivant. La boite de dialogue est ensuite liee a
    /// la fiche de ce membre, avec l'etiquette que l'argument designe dans le repertoire de la
    /// sous-zone.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998, conserve.</b> Le second test de validite lit
    /// <c>0x005e93c0 + 4 * 0x9c</c>, c'est-a-dire un <b>cinquieme</b> emplacement dans une table
    /// qui n'en a que quatre : le compilateur y a employe le compteur laisse par la boucle
    /// precedente au lieu du numero teste. La lecture tombe juste apres la table, dans l'arene, et
    /// ne peut se produire que si l'octet de banque depasse deja 3 — donc sur un chemin qui est de
    /// toute facon hors des rails. Reproduit tel quel, voir <c>legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00443300(short label)
    {
        ushort last = (ushort)(byte)Mem.s_Unknown_005de538;
        byte* slots = Inp.s_SlotFlags_005e93c0;

        int index = 0;
        for (; index <= 3; index++)
        {
            uint flags = *(uint*)(slots + index * 0x9c);
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            last = (ushort)index;
        }

        ushort speaker = Mem.s_StateBank2_005e9060[2];

        if (speaker > 3)
        {
            if ((*(uint*)(slots + speaker * 0x9c) & 0x80000000) == 0) speaker = last;

            // Ghidra: `index` vaut 4 en sortie de boucle. Le cinquieme emplacement. Voir le resume.
            else if ((*(uint*)(slots + index * 0x9c) & 0x50000000) != 0) speaker = last;
        }

        byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + label * 4);
        BindMenuLabels(labels, slots + speaker * 0x9c, s_WorldMapDescriptor_005633b0, 0);

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443170</c> — <b>« fais parler le membre que la banque 2 designe », sans
    /// garde-fou</b>. 18 instructions, absente de <c>Blaze.c</c> ;
    /// <c>Legacy/Raw/Blaze/FUN_00443170.asm</c>. Table <c>0x0056aec8</c>, mine abandonnee,
    /// sous-zones 0 a 2.
    ///
    /// <para>
    /// C'est <see cref="AreaVerb_00443300"/> <b>reduit a sa derniere ligne</b> : l'octet
    /// <c>0x005e9062</c> — l'octet <c>+2</c> de la banque 2 — designe l'emplacement tel quel, sans
    /// recherche du dernier membre vivant ni controle de bornes. Un octet au-dela de 3 lie la boite
    /// a une adresse hors de la table des quatre emplacements ; c'est ce que le binaire fait.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero (<c>xor eax,eax</c>).</returns>
    private static uint AreaVerb_00443170(short label)
    {
        byte* speaker = Inp.s_SlotFlags_005e93c0 + Mem.s_StateBank2_005e9060[2] * 0x9c;

        byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + label * 4);
        BindMenuLabels(labels, speaker, s_WorldMapDescriptor_005633b0, 0);

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443b20</c> — <b>« joue l'animation de la statue »</b>. 95 instructions,
    /// absente de <c>Blaze.c</c>. Table <c>0x0056aec8</c>, foret, sous-zones 5 et 9.
    ///
    /// <para>
    /// <b>Elle ne prend aucun argument et ne rend jamais autre chose que 0.</b> C'est un ordre qui
    /// se rappelle d'une image sur l'autre : tout son etat vit dans deux champs du bloc prive de la
    /// foret — la phase en <c>0x0057150c</c> et un compteur d'images en <c>0x00571510</c>.
    /// </para>
    /// <para>
    /// <b>Elle balaie d'abord les trente-deux entites de scene</b> et retient <b>la derniere</b>
    /// dont <c>+0x0c</c> vaut 3 et qui est vivante — la boucle ne s'arrete pas au premier trouve.
    /// A chacune elle remet la pose a zero et allume <c>0x8001000</c>. Elle retient au passage la
    /// premiere entree de la table d'animation du modele, d'ou vient le nombre d'images.
    /// </para>
    /// <para>
    /// <b>Cinq phases, gouvernees par le drapeau de script 208.</b> La phase 0 attend qu'une
    /// statue reponde au balayage ; la 1 attend que le drapeau se leve ; la 2 fait avancer
    /// l'animation <b>une image sur deux</b> — le compteur ne fait avancer que sur les valeurs
    /// impaires, et seulement passe la huitieme — jusqu'a l'avant-derniere image, ou elle se cale
    /// a <c>nombre - 2</c> ; la 3 attend que le drapeau retombe ; la 4 rembobine au meme rythme
    /// jusqu'a zero, et la 5 est la fin.
    /// </para>
    /// <para>
    /// <b>Deux valeurs ne survivent pas a un balayage infructueux.</b> Le binaire garde la statue
    /// dans <c>ecx</c>, mis a zero au prologue, et la table d'animation dans <c>edi</c>, charge
    /// depuis un emplacement de pile que <b>rien n'a ecrit</b> — le <c>push ecx</c> du prologue
    /// n'y met que le <c>ecx</c> de l'appelant. Tant que la phase reste 0, ni l'un ni l'autre n'est
    /// lu. Mais la phase vit dans le bloc et <b>survit d'une image a l'autre</b> : si la statue
    /// disparait en cours d'animation, les phases 2 et 4 dereferencent zero. Reproduit tel quel —
    /// voir <c>legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00443B20()
    {
        byte* statue = null;
        byte* animation = null;
        bool found = false;

        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 3) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(short*)(scene + 0x18) = 0;
            statue = scene;
            animation = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4));
            *(uint*)scene |= 0x8001000;
            found = true;
        }

        short* phase = (short*)ForestSceneField(0x0057150c);
        short* ticks = (short*)ForestSceneField(0x00571510);

        short state = *phase;

        if (state == 0 && found)
        {
            state = 1;
            statue[0x0a] = 0;
            *(short*)(statue + 0x1a) = 0;
            *phase = 1;
        }

        short tick;

        if (state == 1 && (Mem.s_StateBank0_005e96e0[0x1a] & 1) != 0)
        {
            state = 2;
            tick = 0;
            *phase = 2;
            *ticks = 0;
        }
        else
        {
            tick = *ticks;
        }

        // ------------------------------------------------------------------ 2 : l'aller
        if (state == 2)
        {
            if (tick > 8 && (tick & 1) != 0)
            {
                (*(short*)(statue + 0x1a))++;

                short frames = *(short*)(animation + 0x16);

                if ((int)*(short*)(statue + 0x1a) >= (int)(ushort)frames - 1)
                {
                    state = 3;
                    *(short*)(statue + 0x1a) = (short)(frames - 2);
                    *phase = 3;
                }
            }

            tick++;
            *ticks = tick;
        }

        // ------------------------------------------------------------------ 3 : l'attente
        if (state == 3 && (Mem.s_StateBank0_005e96e0[0x1a] & 1) == 0)
        {
            state = 4;
            tick = 0;
            *phase = 4;
            *ticks = 0;
        }

        // ------------------------------------------------------------------ 4 : le retour
        if (state != 4) return 0;

        if ((tick & 1) != 0)
        {
            (*(short*)(statue + 0x1a))--;

            if (*(short*)(statue + 0x1a) <= 0)
            {
                *(short*)(statue + 0x1a) = 0;
                *phase = 5;
            }
        }

        *ticks = (short)(tick + 1);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004439a0</c> — <b>« en ai-je moins que ça ? »</b>. 84 octets, absente de
    /// <c>Blaze.c</c> ; relevé sous <c>Legacy/Raw/Blaze/FUN_004439A0.asm</c>.
    ///
    /// <para>
    /// Elle compte les bits poses dans <b>neuf drapeaux</b> — les huit de l'octet
    /// <c>0x005e9000</c> et le bit 0 de <c>0x005e9001</c> — puis rend <c>1</c> si son argument est
    /// <b>strictement superieur</b> a ce compte, <c>0</c> sinon. C'est donc un verbe de
    /// <b>test</b> : l'interpreteur s'arrete sur un resultat non nul.
    /// </para>
    /// <para>
    /// La comparaison est sur <b>seize bits et signee</b> (<c>cmp [esp+8],ax</c> puis
    /// <c>setg</c>). Le premier bit <b>pose</b> 1 au lieu d'incrementer, comme dans
    /// <c>AreaVerb_004438A0</c> — meme idiome de compilateur, meme resultat.
    /// </para>
    /// </summary>
    private static uint AreaVerb_004439A0(short threshold)
    {
        byte flags = Mem.Arena(0x005e9000)[0];

        int count = (flags & 0x01) != 0 ? 1 : 0;
        if ((flags & 0x02) != 0) count++;
        if ((flags & 0x04) != 0) count++;
        if ((flags & 0x08) != 0) count++;
        if ((flags & 0x10) != 0) count++;
        if ((flags & 0x20) != 0) count++;
        if ((flags & 0x40) != 0) count++;
        if ((flags & 0x80) != 0) count++;
        if ((Mem.Arena(0x005e9001)[0] & 1) != 0) count++;

        return threshold > (short)count ? 1u : 0u;
    }

    /// <summary>
    /// Ghidra: FUN_00443a00 @ 0x00443a00 — <b>la porte qui s'ouvre et se referme</b>. Decompilee
    /// dans <c>Blaze.c</c>. Verbe de la table <c>0x0056aec8</c>.
    ///
    /// <para>
    /// Elle cherche, parmi les <c>0x20</c> entites de scene de <c>0x005ecaa0</c>, celle de
    /// <b>sous-type 3</b> qui est vivante — la porte —, lui pose <c>0x8001000</c> et remet son
    /// image d'animation a zero. Puis elle deroule <b>cinq etats</b> sur l'entite appelante :
    /// </para>
    /// <list type="number">
    /// <item><b>0 → 1</b> : des qu'une porte a ete trouvee, son image et son cap sont remis a
    /// zero ;</item>
    /// <item><b>1 → 2</b> : attend que le bit 0 de <c>0x005e96fa</c> soit pose ;</item>
    /// <item><b>2</b> : passe la neuvieme image et <b>une image sur deux</b>, l'image d'animation
    /// de la porte avance ; arrivee a l'avant-derniere de sa planche, elle s'y arrete et l'etat
    /// passe a 3. La porte est ouverte ;</item>
    /// <item><b>3 → 4</b> : attend que ce meme bit <b>retombe</b> ;</item>
    /// <item><b>4</b> : une image sur deux, l'animation <b>recule</b> jusqu'a zero, puis l'etat
    /// passe a 5 et plus rien ne s'execute. La porte est refermee.</item>
    /// </list>
    /// <para>
    /// <b>Le nombre d'images vient de la planche</b>, pas d'une constante : la table
    /// <c>0x005e9640</c> est lue a l'indice <c>+0x04</c> de la porte, puis dereferencee deux fois
    /// pour atteindre le <c>+0x16</c> qui porte le compte.
    /// </para>
    /// <para>
    /// <b>Une fragilite du binaire d'origine, conservee.</b> La porte est cherchee a chaque appel
    /// et le pointeur est <b>local</b> : si elle disparait alors que l'etat vaut 2 ou 4, le corps
    /// la dereference tout de meme. L'original a le meme defaut, et le meme sort.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00443A00(byte* entity)
    {
        byte* door = null;
        bool found = false;
        byte* frames = entity;                    // iVar5, initialise au parametre par le binaire

        byte* record = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] != 3 || (*(uint*)record & 0x80000000) == 0) continue;

            *(short*)(record + 0x18) = 0;
            frames = *(byte**)*(byte***)(Mem.Arena(0x005e9640) + record[4] * 4);
            *(uint*)record |= 0x8001000;
            found = true;
            door = record;
        }

        if (*(short*)(entity + 0x10) == 0 && found)
        {
            door[0x0a] = 0;
            *(short*)(door + 0x1a) = 0;
            *(short*)(entity + 0x10) = 1;
        }

        if (*(short*)(entity + 0x10) == 1 && (Mem.Arena(0x005e96fa)[0] & 1) != 0)
        {
            *(short*)(entity + 0x10) = 2;
            *(short*)(entity + 0x16) = 0;
        }

        if (*(short*)(entity + 0x10) == 2)
        {
            if (*(short*)(entity + 0x16) > 8 && (*(ushort*)(entity + 0x16) & 1) != 0)
            {
                (*(short*)(door + 0x1a))++;
                if (*(short*)(door + 0x1a) >= *(ushort*)(frames + 0x16) - 1)
                {
                    *(ushort*)(door + 0x1a) = (ushort)(*(ushort*)(frames + 0x16) - 2);
                    *(short*)(entity + 0x10) = 3;
                    *(short*)(entity + 0x12) = 0;
                }
            }

            (*(short*)(entity + 0x16))++;
        }

        if (*(short*)(entity + 0x10) == 3 && (Mem.Arena(0x005e96fa)[0] & 1) == 0)
        {
            *(short*)(entity + 0x10) = 4;
            *(short*)(entity + 0x16) = 0;
        }

        if (*(short*)(entity + 0x10) == 4)
        {
            if ((entity[0x16] & 1) != 0)
            {
                (*(short*)(door + 0x1a))--;
                if (*(short*)(door + 0x1a) < 1)
                {
                    *(short*)(door + 0x1a) = 0;
                    *(short*)(entity + 0x10) = 5;
                }
            }

            (*(short*)(entity + 0x16))++;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00445480 @ 0x00445480 — <b>le verbe 62 de la foret</b> : rend <b>0 si l'entite
    /// est le dernier membre vivant du groupe</b>, 1 sinon. 61 octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// La boucle retient l'indice du <b>dernier</b> emplacement vivant rencontre — pas le premier :
    /// <c>esi</c> est ecrase a chaque correspondance, il n'y a aucun <c>break</c>. Puis elle compare
    /// cet indice a <c>+0x8</c> de l'entite et rend le resultat de <c>setne</c>.
    /// </para>
    /// <para>
    /// <b>C'est donc un verbe de test</b>, et son sens s'inverse dans la boucle de l'interpreteur :
    /// un resultat nul laisse le script continuer. Le script continue quand l'entite <b>est</b> ce
    /// dernier membre.
    /// </para>
    /// <para>
    /// Le compteur part a <c>-1</c> et la comparaison finale se fait sur <b>16 bits</b>
    /// (<c>cmp cx,si</c>) : si aucun membre n'est vivant, <c>si</c> vaut <c>0xffff</c>, qu'aucun
    /// indice de membre ne peut egaler — le verbe rend alors 1. Transcrit avec la meme largeur.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00445480(byte* entity)
    {
        int last = -1;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) last = i;
        }

        // Ghidra: `movzx cx,byte [eax+8]` puis `cmp cx,si` — seize bits des deux cotes.
        return (ushort)entity[8] != (ushort)last ? 1u : 0u;
    }

    // =====================================================================
    // LE VERBE 13 DE LA FORET (0x004433B0) — LE RASSEMBLEMENT DU GROUPE
    //
    // 1150 octets, absent de Blaze.c, releve au desassembleur ; le dump est conserve sous
    // Legacy/Raw/Blaze/AreaVerbs/FUN_004433B0.asm. C'est le plus gros verbe de la table
    // 0x0056aec8 et de loin — les huit autres du meme lot tiennent en 28 a 179 octets.
    //
    // Le corps est QUATRE FOIS le meme bloc, a trois choses pres : le creneau d'entite qu'il
    // arme, le bit de garde qu'il consulte, et les trois rectangles qu'il teste. Il est donc
    // ecrit une fois, avec les constantes en table — la meme decision que pour l'ascenseur
    // (EntityHandler_00457370), dont les deux sens etaient ecrits deux fois dans le binaire.
    // =====================================================================

    /// <summary>
    /// Les <b>quatre zones de rassemblement</b> de <see cref="AreaVerb_004433B0"/>, indexees par
    /// le creneau d'entite de zone qu'elles arment (0 a 3).
    ///
    /// <para>
    /// Chaque ligne est <b>trois rectangles</b> a la suite — <c>xMin, xMax, zMin, zMax</c> — et la
    /// zone est leur <b>reunion</b>. Le binaire ne les ecrit pas comme une reunion : il enchaine
    /// les comparaisons en partageant les bornes communes, si bien que l'echec d'un rectangle
    /// tombe parfois sur le test du suivant et parfois directement sur le membre suivant. Les
    /// vingt-quatre sauts ont ete suivis un par un : dans les quatre blocs, le resultat est
    /// exactement un OU des trois rectangles, sans trou ni recouvrement parasite.
    /// </para>
    /// <para>
    /// Les coordonnees sont en <b>unites entieres</b>, c'est-a-dire la position de l'entite
    /// decalee de douze bits vers la droite. Elles dessinent <b>deux clairieres</b> : les creneaux
    /// 1 et 3 au sud (<c>z</c> de -200 a 780), les creneaux 0 et 2 au nord (<c>z</c> de 2020 a
    /// 3000), toutes deux entre <c>x = -4200</c> et <c>x = -3220</c>. Dans chaque clairiere les
    /// deux creneaux decoupent le meme sol de deux facons differentes : l'un en trois bandes
    /// verticales dont celle du milieu est plus courte, l'autre en trois bandes horizontales dont
    /// celle du milieu est plus etroite. Il y a donc bien <b>deux emplacements distincts</b> a
    /// occuper par clairiere, et non un seul ecrit deux fois.
    /// </para>
    /// </summary>
    private static readonly int[][] GatherRegions_004433B0 =
    [
        // creneau 0 — clairiere nord, trois bandes verticales
        [-4200, -3850, 2020, 3000,   -3850, -3550, 2200, 3000,   -3550, -3220, 2020, 3000],

        // creneau 1 — clairiere sud, trois bandes verticales
        [-4200, -3850, -200,  780,   -3850, -3550, -200,  600,   -3550, -3220, -200,  780],

        // creneau 2 — clairiere nord, trois bandes horizontales
        [-4200, -3220, 2650, 3000,   -4200, -3400, 2350, 2650,   -4200, -3220, 2020, 2350],

        // creneau 3 — clairiere sud, trois bandes horizontales
        [-4200, -3220,  450,  780,   -4200, -3400,  150,  450,   -4200, -3220, -200,  150],
    ];

    /// <summary>
    /// Ghidra: FUN_004433B0 @ 0x004433b0 — <b>le verbe 13 de la foret</b> : « le groupe est-il
    /// entierement rassemble sur l'un des quatre emplacements ? ». 1150 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; desassemblage conserve sous
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004433B0.asm</c>. Table <c>0x0056aec8</c>, foret,
    /// sous-zones 0 a 4.
    ///
    /// <para>
    /// <b>Sur quoi elle porte.</b> Sur les <b>quatre premiers creneaux d'entite de zone</b>
    /// (<c>0x005fa5e0</c>, pas de <c>0x9c</c>), et sur eux seuls — jamais sur l'entite qu'on lui
    /// passe, qu'elle ne lit pas. De chaque creneau elle utilise trois champs : <c>+0x16</c>
    /// l'<b>armement</b> (1 = en attente, -1 = desarme), <c>+0x14</c> l'<b>etat</b> (99 = pret,
    /// 206 = declenche) et <c>+0x98</c> le <b>temoin</b> qu'elle pose a 1 en declenchant.
    /// </para>
    /// <para>
    /// <b>La garde.</b> L'octet <c>0x1f</c> du banc de script 0 — <c>0x005e96ff</c>, celui dont
    /// <c>EntityHandler_00456750</c>, le radeau, consulte les bits 2 et 3. Ici ce sont les bits
    /// <b>0</b> et <b>1</b> : le bit 0 eteint la paire de creneaux 1 et 3 (la clairiere sud), le
    /// bit 1 la paire 0 et 2 (la clairiere nord). Un bit pose n'est pas seulement un
    /// « deja fait » : il coupe aussi le reamorcage ci-dessous.
    /// </para>
    /// <para>
    /// <b>Le reamorcage est croise</b>, et c'est la seule surprise de la fonction : dans une paire,
    /// l'armement de l'un remet l'<b>autre</b> a l'etat pret. Le <c>+0x16</c> du creneau 3 desarme
    /// (-1) remet le <c>+0x14</c> du creneau 1 a 99, et reciproquement ; meme croisement entre 0
    /// et 2. Les deux emplacements d'une clairiere s'excluent donc : desarmer l'un rouvre l'autre.
    /// </para>
    /// <para>
    /// <b>Le declenchement.</b> Pour chacun des quatre creneaux, dans l'ordre 3, 1, 0, 2 : si sa
    /// garde est levee, s'il est arme (<c>+0x16 == 1</c>) et pret (<c>+0x14 == 99</c>), et si
    /// <see cref="PartyGatheredInRegion"/> repond oui, alors <c>+0x14</c> passe a <b>206</b> et
    /// <c>+0x98</c> a <b>1</b>. Le mot d'armement du creneau 3 est lu <b>une seule fois, a
    /// l'entree</b> (<c>mov cx,[5FA7CAh]</c>, et <c>ecx</c> n'est plus touche jusqu'au
    /// <c>cmp cx,1</c>) : transcrit tel quel, meme si le preambule n'ecrit que des <c>+0x14</c> et
    /// ne pourrait donc pas le changer.
    /// </para>
    /// <para>
    /// Elle finit par <c>xor eax,eax ; ret</c> : elle rend <b>toujours zero</b>, donc le script
    /// continue et lira l'etat des creneaux ensuite. C'est un ordre, pas une question.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_004433B0()
    {
        // Ghidra: `mov al,[5E96FFh]` puis `and dl,1` et `and al,2` — l'octet 0x1f du banc 0, le
        // meme que celui du radeau.
        byte gates = Mem.s_StateBank0_005e96e0[0x1f];
        bool southLocked = (gates & 1) != 0;         // dl
        bool northLocked = (gates & 2) != 0;         // al apres `and al,2`

        byte* slot0 = ZoneEntitySlot(0);
        byte* slot1 = ZoneEntitySlot(1);
        byte* slot2 = ZoneEntitySlot(2);
        byte* slot3 = ZoneEntitySlot(3);

        // Ghidra: `mov cx,[5FA7CAh]`, avant tout le reste.
        short armed3 = *(short*)(slot3 + 0x16);

        // Le reamorcage croise, une paire par bit de garde.
        if (!southLocked)
        {
            if (armed3 == -1) *(short*)(slot1 + 0x14) = 99;
            if (*(short*)(slot1 + 0x16) == -1) *(short*)(slot3 + 0x14) = 99;
        }

        if (!northLocked)
        {
            if (*(short*)(slot0 + 0x16) == -1) *(short*)(slot2 + 0x14) = 99;
            if (*(short*)(slot2 + 0x16) == -1) *(short*)(slot0 + 0x14) = 99;
        }

        // Les quatre blocs, dans l'ordre du binaire. Le creneau 3 est le seul dont l'armement
        // vienne de la lecture d'entree.
        if (!southLocked && armed3 == 1)
        {
            TriggerGatherMarker(slot3, GatherRegions_004433B0[3]);
        }

        if (!southLocked && *(short*)(slot1 + 0x16) == 1)
        {
            TriggerGatherMarker(slot1, GatherRegions_004433B0[1]);
        }

        if (!northLocked && *(short*)(slot0 + 0x16) == 1)
        {
            TriggerGatherMarker(slot0, GatherRegions_004433B0[0]);
        }

        if (!northLocked && *(short*)(slot2 + 0x16) == 1)
        {
            TriggerGatherMarker(slot2, GatherRegions_004433B0[2]);
        }

        return 0;
    }

    /// <summary>Le creneau <paramref name="index"/> de la table d'entites de zone
    /// (<c>0x005fa5e0</c>, pas de <c>0x9c</c>).</summary>
    private static byte* ZoneEntitySlot(int index) => Mem.Entity(0x005fa5e0) + index * 0x9c;

    /// <summary>
    /// Un des quatre blocs de <see cref="AreaVerb_004433B0"/> : si le creneau est pret et que le
    /// groupe est rassemble sur sa zone, il passe a l'etat declenche et pose son temoin.
    /// </summary>
    private static void TriggerGatherMarker(byte* slot, int[] region)
    {
        if (*(short*)(slot + 0x14) != 99) return;
        if (!PartyGatheredInRegion(region)) return;

        *(short*)(slot + 0x14) = 206;               // Ghidra: 0xce
        *(ushort*)(slot + 0x98) = 1;
    }

    /// <summary>
    /// La question que posent les quatre blocs de <see cref="AreaVerb_004433B0"/> : <b>tous</b> les
    /// membres debout sont-ils dans la zone, et y en a-t-il au moins un ?
    ///
    /// <para>
    /// Le binaire ne compte pas « combien sont dedans » mais deux nombres qu'il compare :
    /// <c>esi</c>, incremente pour chaque membre vivant, et <c>ebp</c>, incremente pour chaque
    /// membre vivant qui passe les trois tests suivants. L'increment de <c>esi</c> vient
    /// <b>avant</b> ces tests — c'est ce qui fait qu'un membre trop bas, marque <c>0x2000</c> ou
    /// simplement ailleurs suffit a faire echouer l'egalite.
    /// </para>
    /// <para>
    /// Les trois tests : la hauteur <c>+0x20</c> tronquee au millier doit atteindre
    /// <c>0x50000</c> — le meme idiome de plancher que le radeau, avec une autre borne —, le mot
    /// <c>+0x90</c> ne doit pas porter le bit <c>0x2000</c>, et la position doit tomber dans l'un
    /// des trois rectangles. Le binaire ecrit ce bit de deux facons selon le bloc
    /// (<c>test bh,0x20</c> dans trois d'entre eux, <c>test dword …,0x2000</c> dans le quatrieme) :
    /// c'est le meme test.
    /// </para>
    /// </summary>
    private static bool PartyGatheredInRegion(int[] region)
    {
        int standing = 0;
        int inside = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            standing++;

            if ((*(int*)(member + 0x20) & unchecked((int)0xfffff000)) < 0x50000) continue;
            if ((*(uint*)(member + 0x90) & 0x2000) != 0) continue;

            int x = *(int*)(member + 0x1c) >> 0xc;
            int z = *(int*)(member + 0x24) >> 0xc;

            for (int r = 0; r < 12; r += 4)
            {
                if (x >= region[r] && x <= region[r + 1] && z >= region[r + 2] && z <= region[r + 3])
                {
                    inside++;
                    break;
                }
            }
        }

        // Ghidra: `cmp si,bp` puis `test si,si` et `test bp,bp` — trois sorties, une seule
        // condition. Les deux comptes valent au plus 4, la largeur de seize bits ne se voit pas.
        return standing == inside && standing != 0;
    }

    /// <summary>Un des quatre <c>rep stosd</c> de <see cref="AreaVerb_00443260"/> : 0x10 dwords a
    /// zero a partir de <paramref name="bank"/>.</summary>
    private static void ClearScriptBank(byte* bank)
    {
        uint* words = (uint*)bank;
        for (int i = 0; i < 0x10; i++) words[i] = 0;
    }


    /// <summary>
    /// Ghidra: FUN_00442E10 @ 0x00442e10 — <b>rend le bit 0 du champ <c>+0x8c7</c> du descripteur
    /// de menu</b>, et rien d'autre. Onze octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <c>xor eax,eax ; mov al,[563C77h] ; and eax,1 ; ret</c>. C'est un verbe de <b>test</b> : le
    /// resultat de condition vaut 1 ou 0, donc l'interpreteur s'arrete ou continue selon l'etat de
    /// ce bit.
    /// </para>
    /// <para>
    /// <b>L'adresse <c>0x00563c77</c> n'est pas une globale</b> — c'est le champ <c>+0x8c7</c> du
    /// descripteur <c>0x005633b0</c>, tranche le 2026-08-23 apres que la boite de dialogue soit
    /// restee invisible pour cette raison exacte. Le passage par
    /// <see cref="s_Unknown_00563c77"/> est donc obligatoire : un <c>static byte</c> de plus
    /// referait le meme defaut.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00442E10() => (uint)(s_Unknown_00563c77 & 1);

    /// <summary>
    /// Ghidra: FUN_004432E0 @ 0x004432e0 — <b>efface le bit 0 de la banque 3 quand l'octet de tete
    /// de l'arene porte <c>0x10</c></b>. Dix-neuf octets, absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <c>0x005e9841</c> est <c>s_StateBank3_005e9840 + 1</c> — la meme banque que
    /// <see cref="AreaVerb_00443260"/> remet a zero en entier. Ce verbe-ci n'en touche qu'un bit,
    /// et seulement si <c>0x005e9001</c> porte <c>0x10</c>.
    /// </para>
    /// <para><b>Elle rend 0</b> par les deux chemins : le <c>xor eax,eax</c> est apres la
    /// jonction.</para>
    /// </summary>
    private static uint AreaVerb_004432E0()
    {
        // Ghidra: `test byte [5E9001h],10h ; je 004432F0 ; and byte [5E9841h],0FEh`.
        if ((*Mem.Arena(0x005e9001) & 0x10) != 0)
            Mem.s_StateBank3_005e9840[1] &= 0xfe;

        return 0;
    }
    /// <summary>
    /// Ghidra: FUN_00442d90 @ 0x00442d90 — <b>le verbe 14 de la table de zone</b> : il désigne
    /// l'entité appelante comme <b>porteur du script</b> de l'entité de scène numéro
    /// <paramref name="index"/>. Vingt-sept octets, <b>absente de <c>Blaze.c</c></b> ; dump conservé
    /// dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442D90.asm</c>.
    ///
    /// <para>Le champ écrit est <c>+0x6c</c> de l'enregistrement, celui que
    /// <see cref="RunEntityScript"/> reçoit en deuxième argument partout ailleurs —
    /// <c>AdvanceSceneEntityState_00404FE0</c>, <c>CycleSceneEntityAnimations_004052F0</c> et leurs
    /// voisines le lisent toutes. Le verbe répond donc à la question « pour le compte de qui ce
    /// décor joue-t-il son script ? », et c'est exactement ce qu'il faut à un coffre : le script du
    /// coffre doit savoir qui l'ouvre.</para>
    ///
    /// <para>L'original écrit <c>[ecx*4 + 0x005ecb0c]</c> avec <c>ecx = 39 * index</c>, soit
    /// <c>0x005ecaa0 + index * 0x9c + 0x6c</c> : la même table d'entités de scène que partout, prise
    /// à son champ <c>+0x6c</c>. Elle est adressée ici par son début et son décalage plutôt que par
    /// l'adresse composée, pour rester lisible contre les autres accès.</para>
    ///
    /// <para><b>L'indice est signé</b> (<c>movsx eax, word [esp+8]</c>). Aucune borne n'est
    /// vérifiée : un indice négatif ou au-delà de 0x20 écrit hors de la table. Transcrit tel quel —
    /// c'est au script de zone d'être correct, et rien n'indique que le jeu s'en protège.</para>
    /// </summary>
    /// <returns>Toujours zéro : <c>xor eax,eax</c> précède l'écriture.</returns>
    private static uint AreaVerb_00442D90(byte* entity, short index)
    {
        *(byte**)(Mem.Entity(0x005ecaa0) + index * 0x9c + 0x6c) = entity;
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00444340</c> — <b>le verbe du labyrinthe des morts qui rend au groupe ses
    /// contacts</b>. Creneau <b>13</b> du bloc <c>0x00557ba8</c> (table <c>0x0056aec8</c>,
    /// sous-zones 0 a 3). Quarante instructions, <b>absente de <c>Blaze.c</c></b> ; dump conserve
    /// dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00444340.asm</c>.
    ///
    /// <para>
    /// <b>Elle n'est PAS un gestionnaire d'entite</b>, malgre sa presence dans
    /// <c>GestionnairesEntites.md</c> : la table <c>0x0056aec8</c> est celle des verbes de zone, et
    /// le corps ne lit meme pas l'entite appelante — seulement l'argument de seize bits que le
    /// script lui passe.
    /// </para>
    /// <para>
    /// <b>Cet argument est un numero dans la TROISIEME table d'entites</b> (<c>0x005fa5e0</c>, pas
    /// <c>0x005ecaa0</c> : celle que <c>FUN_0050e5e0</c> parcourt). Le verbe commence par exiger que
    /// le champ <c>+0x16</c> de cet enregistrement soit <b>nul</b> ; sinon il rend la main.
    /// </para>
    /// <para>
    /// <b>L'octet <c>+9</c> de cet enregistrement est un masque d'emplacements</b>, sur ses quatre
    /// bits bas. Pour chaque emplacement actif (<c>0x80000000</c>, sans <c>0x50000000</c>) dont le
    /// bit est pose, le verbe ecrit <c>0xA000</c> dans les <b>quatre</b> demi-mots <c>+0x82</c>,
    /// <c>+0x86</c>, <c>+0x8a</c> et <c>+0x8e</c> — <b>un OU, pas une affectation</b> — et remet
    /// <c>+0x94</c> a zero. Ce sont les memes champs que <see cref="MoveSceneEntity_00455B30"/>
    /// arme a la naissance d'une entite : le verbe <b>rend au membre l'etat de contact d'une entite
    /// neuve</b>.
    /// </para>
    /// <para>
    /// <b>Le masque est relu de la pile a chaque tour</b>, puis decale de l'indice du tour
    /// (<c>shr bl,cl</c>) : c'est bien le bit <i>n</i> qui commande l'emplacement <i>n</i>, et non
    /// un decalage cumulatif.
    /// </para>
    /// <para>
    /// <b>Aucune borne sur l'indice</b> — <c>movsx</c>, donc signe — et la boucle est ecrite
    /// <c>cmp cx,3 ; jle</c>, donc quatre tours en dur, sans consulter
    /// <c>s_PartySize_005e9020</c>.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero : les deux sorties font <c>xor eax,eax</c>.</returns>
    private static uint AreaVerb_00444340(short index)
    {
        byte* record = Mem.Entity(0x005fa5e0) + index * 0x9c;

        if (*(short*)(record + 0x16) != 0) return 0;

        byte mask = (byte)(record[9] & 0x0f);
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;
            if (((mask >> i) & 1) == 0) continue;

            *(ushort*)(slot + 0x82) |= 0xa000;
            *(ushort*)(slot + 0x86) |= 0xa000;
            *(ushort*)(slot + 0x8a) |= 0xa000;
            *(ushort*)(slot + 0x8e) |= 0xa000;

            slot[0x94] = 0;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004443d0</c> — le <b>verbe 4</b> du bloc <c>0x00557ba8</c> (table <c>0x0056aec8</c>,
    /// Labyrinthe des morts, sous-zones 0 a 3). <b>Absent de <c>Blaze.c</c></b> ; releve au
    /// desassembleur, <b>7 instructions</b>, dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004443D0.asm</c>.
    ///
    /// <para>
    /// <b>La fin sans palier du decompte de <see cref="MusicFadeStep_0041AF50"/></b> : la tete du
    /// bloc <c>0x00566c48</c> a <c>0xc0</c>, ses deux <c>short</c> de <c>+0x10</c>/<c>+0x12</c>
    /// mis a zero d'un coup, et le bloc republie par <c>Ordinal_762</c>. Les deux autres ecrivains
    /// de <c>0xc0</c> descendent de 300 par image ; celui-ci saute a zero.
    /// </para>
    /// <para>
    /// <b><c>Ordinal_762</c> ne fait rien dans cette version de <c>winsys3d.dll</c></b> : son
    /// export tombe sur <c>0x00a02870</c>, un <c>ret 4</c> nu que partagent une quarantaine
    /// d'ordinaux (voir <c>Documentation/DLL/winsys3d.md</c>). Il ne touche donc pas EAX, et le
    /// resultat du verbe est le <c>xor eax,eax</c> de sa premiere instruction — les deux
    /// <c>mov [..],ax</c> qui suivent n'y changent rien.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_004443D0()
    {
        byte* block = (byte*)s_MenuScreenBlock_00566c48;

        *(uint*)block = 0xc0;
        *(short*)(block + 0x10) = 0;
        *(short*)(block + 0x12) = 0;

        Winsys3d.Ordinal_762(block);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004444a0</c> — le <b>verbe 12</b> du bloc <c>0x005588a8</c> (table <c>0x0056aec8</c>,
    /// Labyrinthe des morts, sous-zones 4 et 9). <b>Absent de <c>Blaze.c</c></b> ; releve au
    /// desassembleur, <b>114 instructions</b>, dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004444A0.asm</c>.
    ///
    /// <para>
    /// <b>« Tout le groupe a-t-il passe la ligne ? » — et quand oui, lancer le script.</b> Il
    /// parcourt les quatre emplacements vivants (<c>0x80000000</c> sans aucun bit de
    /// <c>0x50010000</c>) et compte, d'une part, ces vivants, d'autre part ceux dont la position
    /// est au-dela de <b>deux seuils</b> : <c>+0x1c</c> strictement au-dessus de <c>0x5dc000</c>
    /// et <c>+0x20</c> strictement au-dessus de <c>0xffe5c000</c>, les deux compares
    /// <b>signes</b>. Chaque membre passe qui ne porte encore ni <c>0x04000000</c> ni le bit 0
    /// recoit le <c>0x04000000</c>, sa teinte <c>+0x38</c>..<c>+0x3a</c> a <c>0xff</c>, le bit
    /// <c>4</c> de <c>+0x36</c> efface, <c>+0x96</c> a zero, et l'effet d'etat <c>0x9d</c> —
    /// le meme que pose <see cref="EvaluatePartyDispersal"/>.
    /// </para>
    /// <para>
    /// <b>Le script ne part que si les deux comptes sont egaux et non nuls.</b> Il part sur le
    /// chef (<c>0x005de538</c>) quand son mot de tete n'a aucun bit de <c>0xd0000000</c>, sinon
    /// sur le premier emplacement vivant — la boucle de repli sort par <c>cx = 0x63</c>, un
    /// <c>break</c> ecrit a la main. L'indice de script est le second argument du verbe.
    /// </para>
    /// <para>
    /// Deux gardes en tete : l'octet <c>0x005de058</c> doit etre nul, et le bit <c>0x20</c> de
    /// l'octet <c>0x20</c> du banc d'etat 0 (<c>0x005e9700</c>) eteint — une fois ce drapeau
    /// pose par la suite du scenario, le verbe ne fait plus rien.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero : les trois sorties font <c>xor eax,eax</c>.</returns>
    private static uint AreaVerb_004444A0(short script)
    {
        if ((byte)*Mem.Camera(0x005de058) != 0) return 0;
        if ((Mem.s_StateBank0_005e96e0[0x20] & 0x20) != 0) return 0;   // Ghidra: DAT_005e9700

        short alive = 0;
        short across = 0;
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50010000) != 0) continue;

            alive++;
            if (*(int*)(slot + 0x1c) <= 0x5dc000) continue;
            if (*(int*)(slot + 0x20) <= unchecked((int)0xffe5c000)) continue;

            across++;
            if ((flags & 0x04000001) != 0) continue;

            slot[0x38] = 0xff;
            slot[0x39] = 0xff;
            slot[0x3a] = 0xff;
            *(ushort*)(slot + 0x36) &= 0xfffb;
            *(short*)(slot + 0x96) = 0;
            *(uint*)slot = flags | 0x04000000;
            SpawnStatusEffectEntity_0047AC40(slot, (int*)(slot + 0x1c), null, 0x9d, 0, -1);
        }

        if (alive != across || alive == 0 || across == 0) return 0;

        byte* leader = Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c;
        if ((*(uint*)leader & 0xd0000000) == 0)
        {
            RunEntityScript(s_Unknown_005e97cc, leader, (uint)(int)script);   // Ghidra: FUN_00513dd0
            return 0;
        }

        slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50010000) != 0) continue;

            RunEntityScript(s_Unknown_005e97cc, slot, (uint)(int)script);     // Ghidra: FUN_00513dd0
            break;                                                             // Ghidra: mov ecx,63h
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00442db0 @ 0x00442db0 — <b>le verbe 12 de la table de zone</b> : il <b>remet un
    /// objet a chacun des quatre membres du groupe</b>. Quatre-vingt-seize octets, decompilee dans
    /// <c>Blaze.c</c> ; dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442DB0.asm</c>.
    ///
    /// <para>
    /// La boucle est ecrite en dur sur <b>quatre</b> emplacements de <c>0x9c</c> octets a partir de
    /// <see cref="Input.s_SlotFlags_005e93c0"/>, sans consulter
    /// <c>s_PartySize_005e9020</c> — comme la boucle de resurrection de
    /// <see cref="AcquireHolyTarget_00447CB0"/>. Seuls les emplacements portant le bit
    /// <c>0x80000000</c> sont servis.
    /// </para>
    /// <para>
    /// Pour chacun, trois gestes dans cet ordre : le tirage pondere
    /// (<see cref="PickWeightedTableEntry"/>, ligne 0, borne 100) choisit la fiche ;
    /// <see cref="GrantChestContents_00530F20"/> fige le monde, joue l'animation de reaction et
    /// compose la phrase ; <see cref="StoreItemInInventory_005312D0"/> range enfin l'objet.
    /// </para>
    /// <para>
    /// <b>Le rappel de fin de boite vaut zero ici</b>, la ou les trois autres appelants
    /// reconstruits passent <c>0x00450700</c> ou <c>0x00474320</c> : le rangement n'est pas
    /// differe, il est fait tout de suite par l'appel suivant. C'est le seul site du depot qui
    /// enchaine les deux a la main.
    /// </para>
    /// <para>
    /// <c>+0x6c</c> de l'emplacement est remis a zero <b>deux fois</b>, avant et apres la remise :
    /// le porteur du script ne doit rester designe ni pendant ni apres. Et
    /// <see cref="GameMemory.s_Unknown_005e909f"/> — la porte de validation des boites — est
    /// rabaissee a chaque tour.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00442DB0()
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            if ((*(uint*)slot & 0x80000000) == 0) continue;

            Mem.s_Unknown_005e909f = 0;
            *(uint*)(slot + 0x6c) = 0;

            int sheet = PickWeightedTableEntry(0, 100);        // Ghidra: FUN_0053a920(0,100,0)
            GrantChestContents_00530F20((uint*)slot, sheet, 0);

            *(uint*)(slot + 0x6c) = 0;
            StoreItemInInventory_005312D0((uint*)slot);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00442e20 @ 0x00442e20 — <b>le verbe 7 de la table de zone</b> :
    /// <b>« ce personnage porte-t-il l'Ecu du destin ? »</b> Soixante-dix-sept octets,
    /// <b>absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442E20.asm</c>.
    ///
    /// <para>
    /// Le membre n'est pas l'entite qu'on lui passe : il vient de la <b>banque de script</b>
    /// <c>0x005e9060</c>, octet 0 — celle que les verbes 5 et 6 ecrivent aussi. Le verbe la
    /// <b>remet a zero</b> avant de chercher, puis y pose 1 s'il trouve : la banque sert donc
    /// d'argument et de resultat, et la valeur de retour du verbe ne porte rien
    /// (<c>xor eax,eax</c>).
    /// </para>
    /// <para>
    /// L'inventaire parcouru est celui de <see cref="GrantChestContents_00530F20"/> : dix
    /// emplacements de <c>0x280</c> octets a <c>MainMem + 0xf0300 + membre * 0x2000</c>. Un
    /// emplacement dont le premier octet est nul est vide et saute.
    /// </para>
    /// <para>
    /// <b>Le mot cherche est <c>0x2efb</c> en <c>+0x86</c></b>, et <c>objets.csv</c> le nomme :
    /// c'est l'<b>Ecu du destin</b>, la meme fiche dans toutes les zones ou elle apparait. Le champ
    /// <c>+0x86</c> est celui dont <see cref="GrantChestContents_00530F20"/> teste deja la plage
    /// <c>0x32c9</c>–<c>0x32cf</c> ; <c>0x2efb</c> tombe en dehors, c'est une autre famille.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero — le resultat est dans la banque, pas dans le retour.</returns>
    private static uint AreaVerb_00442E20()
    {
        byte* bank = Mem.Arena(0x005e9060);
        byte member = bank[0];

        bank[0] = 0;

        byte* inventory = GetMainMemPtr() + (member << 0x0d) + 0xf0300;

        for (int i = 0; i < 10; i++, inventory += 0x280)
        {
            if (inventory[0] == 0) continue;
            if (*(ushort*)(inventory + 0x86) == 0x2efb) bank[0] = 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00442e70 @ 0x00442e70 — <b>le verbe 5 de la table de zone</b> : <b>le plancher
    /// de niveau du personnage atteint-il 180 ?</b> Quarante octets, <b>absente de
    /// <c>Blaze.c</c></b> ; dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442E70.asm</c>.
    ///
    /// <para>
    /// Meme convention que <see cref="AreaVerb_00442E20"/> : le membre est lu dans l'octet 0 de la
    /// banque <c>0x005e9060</c>, et le resultat y est reecrit. Le champ mesure est <c>+0x144</c> du
    /// bloc de personnage — le <b>plancher de niveau</b>, celui que
    /// <c>LevelUpCharacter_005391F0</c> avance (voir <c>MonteeDeNiveau.md</c>).
    /// </para>
    /// <para>
    /// <b>La banque recoit 1 ou 2, jamais 0.</b> <c>setb</c> rend 1 quand le plancher est
    /// <i>sous</i> <c>0xb4</c>, et l'<c>inc</c> qui suit porte donc <b>2 pour « pas encore »</b> et
    /// <b>1 pour « oui »</b>. La comparaison est <b>non signee</b>.
    /// </para>
    /// <para>
    /// <b>Le detail qui compte</b> : l'<c>inc eax</c> porte sur le registre entier, qui contient
    /// encore <c>membre &lt;&lt; 13</c>. L'octet bas de ce produit est nul pour les quatre membres
    /// possibles, donc l'increment ne deborde jamais sur le resultat — c'est vrai par construction,
    /// pas par hasard, et c'est pourquoi la transcription peut s'en tenir a 1 et 2.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00442E70()
    {
        byte* bank = Mem.Arena(0x005e9060);
        byte member = bank[0];

        ushort floor = *(ushort*)(GetMainMemPtr() + (member << 0x0d) + 0xf0144);

        bank[0] = (byte)(floor < 0xb4 ? 2 : 1);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00442ea0 @ 0x00442ea0 — <b>le verbe 6 de la table de zone</b> : <b>le meme
    /// plancher de niveau, echelonne de 0 a 5</b>. Cent quatorze octets, <b>absente de
    /// <c>Blaze.c</c></b> ; dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442EA0.asm</c>.
    ///
    /// <para>
    /// Cinq seuils testes l'un apres l'autre — 20, 40, 60, 80, 100 — et un increment a chacun qui
    /// passe : la banque <c>0x005e9060</c> ressort avec le <b>nombre de paliers franchis</b>. Les
    /// cinq comparaisons sont <b>non signees</b> et portent toutes sur le meme mot, relu a chaque
    /// fois.
    /// </para>
    /// <para>
    /// <b>Il adresse le bloc autrement que son voisin, pour la meme adresse.</b> Ici
    /// <c>(membre + 0x78) &lt;&lt; 13</c>, la <c>membre &lt;&lt; 13</c> puis <c>+ 0xf0144</c> chez
    /// <see cref="AreaVerb_00442E70"/> — et <c>0x78 &times; 0x2000 = 0xf0000</c>, donc les deux
    /// designent le meme octet. Deux ecritures d'une meme adresse dans deux verbes voisins : c'est
    /// le compilateur, pas une difference de sens.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00442EA0()
    {
        byte* bank = Mem.Arena(0x005e9060);

        // `(membre + 0x78) << 13` — la meme adresse que `membre << 13 + 0xf0000`.
        byte* block = GetMainMemPtr() + (bank[0] + 0x78 << 0x0d);

        bank[0] = 0;

        if (*(ushort*)(block + 0x144) >= 0x14) bank[0] = 1;
        if (*(ushort*)(block + 0x144) >= 0x28) bank[0]++;
        if (*(ushort*)(block + 0x144) >= 0x3c) bank[0]++;
        if (*(ushort*)(block + 0x144) >= 0x50) bank[0]++;
        if (*(ushort*)(block + 0x144) >= 0x64) bank[0]++;

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00442f20 @ 0x00442f20 — <b>le verbe 4 de la table de zone</b> : il <b>remonte a
    /// cinq les sept compteurs de sorts</b> d'un personnage. Cinquante-sept octets, <b>absente de
    /// <c>Blaze.c</c></b> ; dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00442F20.asm</c>.
    ///
    /// <para>
    /// Les sept octets contigus a <c>+0x178</c> du bloc de personnage sont ceux que
    /// <see cref="StoreItemInInventory_005312D0"/> indexe par <c>famille - 0x3151</c> : <b>deux
    /// releves independants sur le meme champ</b>, celui-ci et le rangement d'objet. Chacun est
    /// force a 5 s'il etait en dessous ; ceux qui valaient plus ne bougent pas.
    /// </para>
    /// <para>
    /// <b>Il lit son membre dans l'octet 2 de la banque</b> <c>0x005e9060</c>, et non l'octet 0
    /// comme les trois verbes voisins — c'est la banque que <c>AreaVerb_004431F0</c> remplit avec
    /// la classe du locuteur.
    /// </para>
    /// <para>
    /// <b>C'est le seul des six a rendre autre chose que zero</b> : <c>1</c> si <b>aucun</b>
    /// compteur n'a eu besoin d'etre remonte, <c>0</c> si au moins un l'a ete. Le script peut donc
    /// distinguer « il n'y avait rien a faire » de « j'ai rechargé » — la forme exacte d'un service
    /// d'auberge qu'on ne facture que s'il a servi.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00442F20()
    {
        byte* bank = Mem.Arena(0x005e9060);
        byte* counters = GetMainMemPtr() + (bank[2] + 0x78 << 0x0d) + 0x178;

        int raised = 0;

        for (int i = 0; i < 7; i++)
        {
            if (counters[i] < 5)
            {
                counters[i] = 5;
                raised++;
            }
        }

        // Ghidra: `test cx,cx ; sete al` — seize bits, mais le compte ne depasse jamais sept.
        return raised == 0 ? 1u : 0u;
    }

    /// <summary>
    /// Ghidra: FUN_004430b0 @ 0x004430b0 — <b>le verbe 2 de la table de zone</b> : il <b>compte les
    /// neuf drapeaux bas du banc d'etat</b>. Soixante-dix-huit octets, <b>absente de
    /// <c>Blaze.c</c></b> ; dump conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004430B0.asm</c>.
    ///
    /// <para>
    /// Les huit bits de <c>0x005e9000</c> et le bit 0 de <c>0x005e9001</c> — <b>exactement les neuf
    /// que <see cref="AreaVerb_00442F60"/> decale d'un cran vers le banc <c>0x005e9840</c></b>.
    /// Les deux verbes lisent donc la meme collection, et c'est ce qui la delimite : neuf, pas
    /// seize. Le compte part dans l'octet <b>10</b> de la banque de script, <c>0x005e906a</c>.
    /// </para>
    /// <para>
    /// Le binaire ecrit neuf tests successifs plutot qu'une boucle, et le premier fait
    /// <c>mov eax,1</c> la ou les huit autres font <c>inc eax</c> — sans difference, le registre
    /// venant d'etre mis a zero.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero — le compte est dans la banque.</returns>
    private static uint AreaVerb_004430B0()
    {
        byte* flags = Mem.Arena(0x005e9000);
        byte low = flags[0];

        int count = 0;
        for (int bit = 0; bit < 8; bit++)
        {
            if ((low & 1 << bit) != 0) count++;
        }

        if ((flags[1] & 1) != 0) count++;

        Mem.Arena(0x005e9060)[10] = (byte)count;   // Ghidra: DAT_005e906a
        return 0;
    }

    /// <summary>Une des trois passes de <see cref="AreaVerb_00443100"/> : efface le bit de gel
    /// <c>0x04000000</c> de chaque enregistrement encore present.</summary>
    private static void ThawEntityTable(byte* first, int count)
    {
        for (int i = 0; i < count; i++, first += 0x9c)
        {
            uint flags = *(uint*)first;
            if ((flags & 0x10000000) == 0)
            {
                *(uint*)first = flags & 0xfbffffff;
            }
        }
    }

    private static uint AreaVerb_00442F60()
    {
        byte* flags = Mem.Arena(0x005e9000);
        byte* bank = Mem.Arena(0x005e9840);

        if ((flags[1] & 0x10) != 0)
        {
            // Branche de repli : on ne garde que le bit 0 du premier octet et les bits hauts du
            // second. Le reste du banc est abandonne.
            bank[0] &= 1;
            bank[1] &= 0xf8;
        }
        else
        {
            // Chaque bit arme le bit SUIVANT du banc de destination ; le bit 7 deborde sur l'octet
            // d'apres, et les deux bits bas de flags[1] suivent dans la meme foulee.
            byte low = flags[0];
            for (int bit = 0; bit < 7; bit++)
            {
                if ((low & 1 << bit) != 0) bank[0] |= (byte)(1 << bit + 1);
            }

            if ((low & 0x80) != 0) bank[1] |= 0x01;
            if ((flags[1] & 0x01) != 0) bank[1] |= 0x02;
            if ((flags[1] & 0x02) != 0) bank[1] |= 0x04;
        }

        if ((flags[6] & 8) != 0)
        {
            bank[0] |= 1;
        }

        byte source = *Mem.Arena(0x005e96fe);
        byte track = source <= 2 ? source : (byte)0;       // Ghidra: `setne bl ; dec ebx ; and ebx,2`

        s_Unknown_005de534 = track;
        s_Unknown_00566c8c = track;
        PlayMusicTrack(track);                             // Ghidra: FUN_00529ad0

        byte subArea = s_Unknown_005de52c;
        if (subArea <= 0x50 && (subArea & 7) == 0)         // Ghidra: onze `cmp al,N ; je`
        {
            byte** slots = *(byte***)(s_Unknown_005e97cc + 0x74);
            *slots[0] = track;
            *slots[1] = track;
        }

        return 0;                                          // Ghidra: xor eax,eax ; ret
    }

    // =====================================================================
    // LES QUATRE VERBES SIMPLES DE LA VIEILLE TOUR (2026-09-17)
    //
    // Categorie B de Zone4ParComplexite.md : quatre corps, 139 instructions en tout, aucune table
    // de sauts, aucune appelee inconnue, aucun jumeau. Deux sont dans Blaze.c, deux ont demande le
    // desassembleur (dumps sous Legacy/Raw/Blaze/AreaVerbs/).
    //
    // ILS VONT PAR PAIRES DE CRENEAU, ET LES DEUX OCCUPANTS D'UN CRENEAU N'ONT RIEN A VOIR L'UN
    // AVEC L'AUTRE. La tour installe deux bancs de verbes selon la sous-zone :
    //
    //   creneau 11 : 0x00443D00 (sous-zones 0,1,2,4)  /  0x004442F0 (3,7,9)
    //   creneau 12 : 0x00443CB0 (sous-zones 0,1,2,4)  /  0x00444200 (3,7,9)
    //   creneau 14 : 0x00443C60 (sous-zones 0,1,2,4)  /  la souche 0x00401FA0
    //
    // Lire « le verbe 12 » sans dire de quelle sous-zone ne veut donc rien dire. 0x00443D00, le
    // colocataire du creneau 11, est de categorie D (363 instructions) et reste a ecrire.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00443c60 @ 0x00443c60 — <b>« le groupe est-il encore en transition ? »</b>.
    /// Vingt-deux instructions, <b>absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00443C60.asm</c>. Table <c>0x0056aec8</c>, vieille tour,
    /// creneau 14 des sous-zones 0, 1, 2 et 4.
    ///
    /// <para>Elle balaie les quatre fiches d'emplacement et rend <b>1</b> des que l'une des deux
    /// conditions se presente :</para>
    ///
    /// <list type="number">
    /// <item>la fiche est active (<c>0x80000000</c>), porte <c>0x10000000</c> et n'est <b>pas</b>
    /// portee par une autre entite (<c>0x40000000</c> eteint) ;</item>
    /// <item>son compte a rebours de transition <c>+0x12</c> n'est pas nul.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Le second test porte sur les quatre emplacements, actifs ou non</b>, et ce n'est pas une
    /// simplification : le binaire y arrive par trois chemins, dont celui de la fiche inactive
    /// (<c>test eax,80000000h ; je LAB_00443c8e</c>). Seul le troisieme <c>test</c> sort
    /// directement.
    /// </para>
    /// <para>
    /// Les deux champs sont ceux que <see cref="TickPartyMember"/> manie ensemble : tant que
    /// <c>+0x12</c> tourne le membre est fige, et <c>0x10000000</c> est le temoin que la bascule a
    /// deja ete prise en compte. Le script s'arrete donc tant que la reponse est « oui ».
    /// </para>
    /// <para>Elle ne lit ni l'entite ni l'argument.</para>
    /// </summary>
    /// <returns>1 si un emplacement est encore en transition, 0 sinon.</returns>
    private static uint AreaVerb_00443C60()
    {
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;

            if ((flags & 0x80000000) != 0 && (flags & 0x10000000) != 0 && (flags & 0x40000000) == 0)
                return 1;                                  // Ghidra: LAB_00443ca2

            if (*(ushort*)(member + 0x12) != 0) return 1;  // Ghidra: LAB_00443c8e
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00443cb0 @ 0x00443cb0 — <b>« pose l'effet 0x2c sur tout membre au sol qui
    /// touche »</b>. Trente instructions, dans <c>Blaze.c</c>. Table <c>0x0056aec8</c>, vieille
    /// tour, creneau 12 des sous-zones 0, 1, 2 et 4.
    ///
    /// <para>
    /// Quatre conditions, toutes sur la fiche d'emplacement : active (<c>0x80000000</c>) et ni
    /// morte ni absente (<c>0x50000000</c> eteint), le <b>mot de contact</b> <c>+0x90</c> — celui
    /// que <see cref="FinalizeEntityPlacement2"/> ecrit — portant le bit <c>0x40</c>, et le bit 0
    /// du mot d'etat eteint, celui que la passe du sol pose quand le membre quitte son plancher.
    /// La naissance passe par <see cref="SpawnEffectNearCamera_00468110"/>, donc <b>rien ne nait
    /// au-dela de 1800 unites de la camera</b>.
    /// </para>
    /// <para>
    /// <b>Son resultat de condition est un reste de registre</b>, comme celui des souches, et il
    /// n'est pas anodin : le corps ne finit pas par un <c>xor eax,eax</c>. Au <c>ret</c>,
    /// <c>EAX</c> porte ce que le <b>dernier tour</b> y a laisse — le mot d'etat de l'emplacement
    /// 3 s'il n'a pas appele, le retour de <c>FUN_00468110</c> s'il a appele. Comme un emplacement
    /// actif porte <c>0x80000000</c>, le premier cas rend presque toujours une valeur non nulle :
    /// rendre 0 ici serait un contresens, pas une simplification.
    /// </para>
    /// <para>
    /// TODO(REVERSE) : dans le cas « il a appele », le reste n'est reproduit qu'a seize bits pres.
    /// <c>FUN_00468110</c> finit par <c>mov ax,cx</c>, qui ne touche que la moitie basse de
    /// <c>EAX</c> ; la moitie haute vient de <c>FUN_00545A10</c> (la distance) ou de
    /// <c>FUN_0047AC40</c> selon la branche prise. <see cref="SpawnEffectNearCamera_00468110"/>
    /// rend un <c>ushort</c> et perd donc cette moitie. Etablir si un script de la tour lit ce
    /// resultat avant de decider s'il faut modeliser le mot haut.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00443CB0()
    {
        // Ghidra: `mov eax,[esi]` est en TETE de tour, donc le reste vaut le mot d'etat du
        // dernier emplacement traite quand il n'a pas appele.
        uint leftover = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            leftover = flags;

            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if ((*(uint*)(member + 0x90) & 0x40) == 0) continue;
            if ((flags & 1) != 0) continue;

            leftover = SpawnEffectNearCamera_00468110(member, 0x2c, 0, -1);   // Ghidra: FUN_00468110
        }

        return leftover;
    }

    /// <summary>
    /// Ghidra: FUN_004442f0 @ 0x004442f0 — <b>range deux drapeaux d'avancement en un nombre de 0 a
    /// 3 dans la banque 2</b>. Vingt-cinq instructions, <b>absente de <c>Blaze.c</c></b> ; dump
    /// conserve dans <c>Legacy/Raw/Blaze/AreaVerbs/FUN_004442F0.asm</c>. Table <c>0x0056aec8</c>,
    /// vieille tour, creneau 11 des sous-zones 3, 7 et 9.
    ///
    /// <para>
    /// Les deux bits sont <c>0x10</c> et <c>0x20</c> du premier octet de <c>0x005e9000</c>, le banc
    /// d'avancement du groupe ; le resultat part dans le <b>troisieme octet de la banque d'etat
    /// 2</b> (<c>0x005e9062</c>), d'ou un script ulterieur le relira pour choisir une branche.
    /// </para>
    /// <para>
    /// <b>Les poids sont inverses par rapport a l'ordre des bits</b>, et c'est la seule chose a
    /// retenir de la fonction : <c>0x10</c>, le plus bas, pese <b>2</b> ; <c>0x20</c> pese
    /// <b>1</b>. Le binaire ne calcule pas un decalage mais enchaine quatre <c>test</c> qui posent
    /// la constante 1, 2 ou 3 — et il laisse en chemin un <c>jne</c> vers l'instruction suivante
    /// (<c>0x00444303</c>), un saut qui ne saute pas, comme <see cref="AreaVerb_004431F0"/> en
    /// porte un.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero : <c>xor eax,eax</c> suit l'ecriture.</returns>
    private static uint AreaVerb_004442F0()
    {
        byte progress = *Mem.Arena(0x005e9000);          // Ghidra: `mov cl,[5E9000h]`

        int value = 0;
        if ((progress & 0x10) != 0) value |= 2;
        if ((progress & 0x20) != 0) value |= 1;

        Mem.s_StateBank2_005e9060[2] = (byte)value;      // Ghidra: DAT_005e9062
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00444200 @ 0x00444200 — <b>le verbe qui range le groupe en V derriere son
    /// premier emplacement, et le fige</b>. Soixante-deux instructions, dans <c>Blaze.c</c>. Table
    /// <c>0x0056aec8</c>, vieille tour, creneau 12 des sous-zones 3, 7 et 9.
    ///
    /// <para>
    /// Pour chaque emplacement actif et ni mort ni absent, il <b>deplace la position d'un ecart
    /// fixe</b>, la repose par <see cref="FinalizeEntityPlacement2"/>, met le cap <c>+0x32</c> a
    /// zero et pose le bit de gel <c>0x04000000</c> — celui sur lequel
    /// <see cref="TickPartyMember"/> rend la main tout de suite (<c>*slot &amp; 0x4000008</c>).
    /// Apres ce verbe, le groupe ne bouge plus de lui-meme.
    /// </para>
    /// <para><b>Les ecarts, en unites de <c>0x1000</c>.</b> Le premier emplacement n'est pas
    /// deplace ; les trois autres se rangent par rapport a la ou ils sont deja.</para>
    ///
    /// <list type="bullet">
    /// <item>emplacement 1 : <c>Z -= 0x64000</c> (cent unites en arriere) ;</item>
    /// <item>emplacement 2 : <c>Z -= 0x32000</c>, <c>X -= 0x32000</c> (cinquante en arriere, cinquante d'un cote) ;</item>
    /// <item>emplacement 3 : <c>Z -= 0x32000</c>, <c>X += 0x32000</c> (l'autre cote).</item>
    /// </list>
    ///
    /// <para>
    /// Le binaire nomme ces cinq champs par leur adresse absolue — <c>DAT_005e9480</c>,
    /// <c>DAT_005e951c</c>, <c>DAT_005e9514</c>, <c>DAT_005e95b8</c>, <c>DAT_005e95b0</c> — ce qui
    /// cache qu'il s'agit simplement de <c>+0x24</c> et <c>+0x1c</c> des emplacements 1, 2 et 3.
    /// Ils sont ecrits ici par emplacement et decalage, comme partout ailleurs.
    /// </para>
    /// <para>
    /// <b>Le mot d'etat est relu apres l'appel</b> (<c>mov eax,[esi+5E93C0h]</c> juste avant le
    /// <c>or</c>) : <see cref="FinalizeEntityPlacement2"/> ecrit dans la fiche, et la valeur lue en
    /// tete de tour ne vaut donc plus. Conserve tel quel.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero : <c>xor eax,eax</c> entre les depilements.</returns>
    private static uint AreaVerb_00444200()
    {
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            if (i == 1)
            {
                *(int*)(member + 0x24) -= 0x64000;              // Ghidra: DAT_005e9480
                FinalizeEntityPlacement2((uint*)member);        // Ghidra: FUN_00533940
            }

            if (i == 2)
            {
                *(int*)(member + 0x24) -= 0x32000;              // Ghidra: DAT_005e951c
                *(int*)(member + 0x1c) -= 0x32000;              // Ghidra: DAT_005e9514
                FinalizeEntityPlacement2((uint*)member);
            }

            if (i == 3)
            {
                *(int*)(member + 0x24) -= 0x32000;              // Ghidra: DAT_005e95b8
                *(int*)(member + 0x1c) += 0x32000;              // Ghidra: DAT_005e95b0
                FinalizeEntityPlacement2((uint*)member);
            }

            *(ushort*)(member + 0x32) = 0;                      // Ghidra: `mov [esi+5E93F2h],bp`
            *(uint*)member |= 0x4000000;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00443d00</c> — <b>le minuteur qui retourne les vues de quatre sous-zones</b>.
    /// 363 instructions, <b>absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00443D00.asm</c>. Table <c>0x0056aec8</c>, vieille tour,
    /// creneau 11 des sous-zones 0, 1, 2 et 4 — le colocataire de <see cref="AreaVerb_004442F0"/>.
    ///
    /// <para>
    /// <b>La taille ment</b> : le corps est vingt fois la meme sequence. Quatre instructions de six
    /// octets sont baties sur la pile, <c>{0, 3, s, 1, 0, v}</c> pour les sous-zones <c>s</c> = 0 a
    /// 3, et passees a <c>FUN_00514020</c> — l'enveloppe du gestionnaire de l'opcode 8,
    /// <see cref="ScriptOpEnterSubArea"/>. Chacune pose l'octet <c>+1</c> de la fiche de sous-zone —
    /// son <b>indice de vue</b> — a <c>v</c>, et bascule la vue sur-le-champ si la sous-zone est la
    /// courante. Le verbe ne fait qu'alterner les <c>v</c>.
    /// </para>
    /// <para><b>Les deux bits <c>0x10</c> et <c>0x20</c> de <c>0x005e9702</c> choisissent :</b></para>
    /// <list type="table">
    ///   <item><term>les deux</term><description>vue 5, et <b>pas de decompte</b> ;</description></item>
    ///   <item><term><c>0x10</c> seul</term><description>vue 3 — puis vue 7 si l'on est dans la
    ///     sous-zone 0, camera en <c>Z &gt; 0</c> et en <c>X &gt; 2000</c> ;</description></item>
    ///   <item><term><c>0x20</c> seul</term><description>vue 4 — puis vue 6, meme lieu mais
    ///     <c>X &lt; -2000</c> ;</description></item>
    ///   <item><term>aucun</term><description>vue 0 — puis vue 2 a l'est, vue 1 a l'ouest, dans
    ///     la sous-zone 0 et en <c>Z &gt; 0</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Un seul bit pose arme le decompte</b> : <c>0x005e9703</c> compte les appels jusqu'a 200,
    /// puis <c>0x005e9704</c> compte les centaines de deux. A la sixieme, les deux bits tombent, les
    /// quatre vues reviennent a 0, et le verbe rend <b>1</b> — le seul cas ou il rend autre chose que
    /// zero. <c>0x005e9704</c> n'est jamais remis a zero ici : c'est a qui pose les bits de le faire.
    /// </para>
    /// <para>
    /// <b>Le mode est tenu dans <c>CX</c></b>, et le chemin des deux bits l'initialise depuis le mot
    /// de resultat <b>non initialise</b> (<c>mov ecx,[esp+4]</c>). Ce reste n'est jamais lu : les
    /// quatre cas s'excluent, et chacun reecrit <c>CX</c> avant la comparaison. Les relectures de
    /// <c>0x005e9702</c> entre deux cas sont reproduites.
    /// </para>
    /// <para>Elle ne lit ni l'entite ni l'argument.</para>
    /// </summary>
    /// <returns>1 quand le decompte s'acheve, 0 sinon.</returns>
    private static uint AreaVerb_00443D00()
    {
        // Ghidra: [esp+8], [esp+10h], [esp+18h], [esp+20h] — les quatre instructions ; [esp+4] le
        // resultat, jamais relu.
        byte* enter = stackalloc byte[4 * 6];
        for (int area = 0; area < 4; area++)
        {
            byte* s = enter + area * 6;
            s[0] = 0; s[1] = 3; s[2] = (byte)area; s[3] = 1; s[4] = 0; s[5] = 0;
        }

        uint* result = stackalloc uint[1];
        byte* bank = Mem.s_StateBank0_005e96e0;

        void SetFourViews(byte view)
        {
            for (int area = 0; area < 4; area++) enter[area * 6 + 5] = view;
            for (int area = 0; area < 4; area++) ScriptOpEnterSubArea(enter + area * 6, result);   // Ghidra: FUN_00514020
        }

        bool InFirstAreaNorth()
            => (byte)*Mem.Camera(0x005de058) == 0 && (*Mem.Camera(0x005de070) & unchecked((int)0xfffff000)) > 0;

        int CameraXCell() => *Mem.Camera(0x005de068) & unchecked((int)0xfffff000);

        byte flags = bank[0x22];                                // Ghidra: DAT_005e9702
        short mode;

        if ((flags & 0x10) != 0 && (flags & 0x20) != 0)
        {
            SetFourViews(5);
            flags = bank[0x22];
            mode = 2;
        }
        else
        {
            mode = 0;   // Ghidra: `mov ecx,[esp+4]`, reste de pile — reecrit plus bas dans tous les cas
        }

        if ((flags & 0x10) != 0 && (flags & 0x20) == 0)
        {
            SetFourViews(3);
            if (InFirstAreaNorth() && CameraXCell() > 0x7d0000) SetFourViews(7);
            flags = bank[0x22];
            mode = 1;
        }

        if ((flags & 0x10) == 0 && (flags & 0x20) != 0)
        {
            SetFourViews(4);
            if (InFirstAreaNorth() && CameraXCell() < -0x7d0000) SetFourViews(6);
            flags = bank[0x22];
            mode = 1;
        }

        if ((flags & 0x30) == 0)
        {
            SetFourViews(0);
            if (InFirstAreaNorth())
            {
                if (CameraXCell() > 0x7d0000) SetFourViews(2);
                if (CameraXCell() < -0x7d0000) SetFourViews(1);   // Ghidra: X relu apres l'appel
            }
            flags = bank[0x22];
            mode = 0;
        }

        if (mode != 1) return 0;

        bank[0x23]++;                                           // Ghidra: DAT_005e9703
        if (bank[0x23] != 200) return 0;

        bank[0x23] = 0;
        bank[0x24]++;                                           // Ghidra: DAT_005e9704
        if (bank[0x24] != 6) return 0;

        bank[0x22] = (byte)(flags & 0xef & 0xdf);
        SetFourViews(0);
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_0053dd20 @ 0x0053dd20 — <b>le verbe 56 de la table de zone : l'expertise d'un
    /// objet</b>. C'est l'homme qui identifie les objets non identifies, et c'est lui que son second
    /// dialogue appelle.
    ///
    /// <para>Absente de <c>Blaze.c</c>. Le relevé existait deja au depot depuis le 2026-08-16 —
    /// <c>Legacy/Raw/Blaze/FUN_0053dd20.asm</c> et sa decompilation ciblee
    /// <c>FUN_0053dd20.c</c> — et c'est celle-ci qui a servi de base, recoupee instruction par
    /// instruction contre le desassemblage.</para>
    ///
    /// <para><b>Trois temps.</b></para>
    ///
    /// <para><b>1. Trouver.</b> Les dix emplacements d'objet du personnage
    /// (<c>MainMem + (joueur + 0x78) * 0x2000 + 0x300</c>, pas de <c>0x280</c>) sont parcourus
    /// jusqu'au premier qui soit <b>occupe</b> et <b>non identifie</b> — l'octet de tete non nul, le
    /// bit <c>0x8000</c> de <c>+0x12</c> eteint. Si les dix sont epuises, <c>DAT_005e909f</c>
    /// retombe a 0 et le verbe rend <b>1</b> : « rien a expertiser ». C'est la seule sortie non
    /// nulle, donc la seule qui fasse brancher le script.</para>
    ///
    /// <para><b>2. Expertiser.</b> Le bit <c>0x8000</c> est pose, le nom complet de l'objet
    /// (<see cref="ItemDisplayName"/>) est <b>recopie octet par octet</b> dans le libelle 1 de la
    /// zone (<c>repertoire + 0x70</c>, entree 1) — c'est ainsi que la phrase du personnage nomme
    /// l'objet — et cinq champs du bloc de panneau sont remplis :</para>
    ///
    /// <list type="bullet">
    /// <item><c>DAT_005e909a</c> : 1 si le mot de <c>+0x14</c> porte <c>0x400</c>, mais 0 s'il vaut
    /// exactement <c>0x4000400</c>. L'ordre du binaire est <c>cmp ; mov 1 ; jnz ; mov 0</c> — il
    /// pose 1 puis se retracte, ce qui n'est pas la meme chose qu'un test compose.</item>
    /// <item><c>DAT_005e909b</c> : 0 a 3 selon la valeur de base de l'objet (<c>+0x88</c>, le champ
    /// dont <see cref="ComputeItemValue"/> part), par trois seuils <b>non signes</b> — plus de
    /// 0x15e, plus de 1000, plus de 5000.</item>
    /// <item><c>DAT_005e909c</c> = 3, <c>DAT_005e909d</c> = 1, <c>DAT_005e909e</c> = 0 : le meme
    /// triplet de mise en page que les autres ouvertures de panneau posent.</item>
    /// </list>
    ///
    /// <para>Deux entrees de la table de decodage sont aussi ecrites : <c>0x005e8ffc</c> recoit
    /// <b>en un dword</b> la copie de <c>0x005e8fa0</c>, puis sa moitie haute est reecrite
    /// <b>en seize bits</b> avec <c>joueur * 0x40</c> ajoute. Ghidra rend les deux par un seul
    /// <c>CONCAT22</c> ; le binaire fait bien deux ecritures de largeurs differentes, et c'est cette
    /// forme-la qui est transcrite.</para>
    ///
    /// <para><b>3. Propager.</b> Les dix emplacements sont reparcourus, et tout objet occupe encore
    /// non identifie dont les <b>seize premiers octets</b> sont identiques a celui qu'on vient
    /// d'expertiser est marque identifie a son tour. Une expertise vaut donc pour toute la pile.
    /// L'emplacement trouve lui-meme est saute par le garde, son bit venant d'etre pose.</para>
    ///
    /// <para>La comparaison sur seize octets est <b>le meme predicat d'identite d'objet</b> que le
    /// transfert entre membres du menu « Objets » emploie, et il y propage deja <c>0x8000</c> dans
    /// l'autre sens. Deux sites, une regle.</para>
    ///
    /// <para><b>L'astuce de pointeur du binaire est conservee</b> : il tient l'ecart en octets entre
    /// le curseur et l'emplacement expertise, et le decremente de <c>0x280</c> a chaque tour, si
    /// bien que <c>curseur[ecart]</c> designe toujours l'objet de reference sans le recalculer.</para>
    /// </summary>
    /// <returns>1 si aucun objet n'etait a expertiser, 0 sinon.</returns>
    private static uint AreaVerb_0053DD20(byte* entity)
    {
        byte player = entity[8];
        byte* items = GetMainMemPtr() + ((player + 0x78) << 0xd) + 0x300;

        int index = 0;
        byte* found = items;

        while (found[0] == 0 || (*(ushort*)(found + 0x12) & 0x8000) != 0)
        {
            index++;
            found += 0x280;
            if (index >= 10)
            {
                Mem.s_Unknown_005e909f = 0;
                return 1;                                  // rien a expertiser
            }
        }

        *(ushort*)(found + 0x12) |= 0x8000;

        // Le nom est recopie octet par octet dans le libelle 1 de la zone — c'est la phrase du
        // personnage qui le lira.
        byte* name = ItemDisplayName(found);               // Ghidra: FUN_0051bd30
        byte* label = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4);
        while (*name != 0) *label++ = *name++;
        *label = 0;

        // Deux ecritures de largeurs differentes, et non un seul CONCAT22 : `mov edx,[5e8fa0] ;
        // mov [5e8ffc],edx` puis `movzx ax,[edi+8] ; shl eax,6 ; add ax,[5e8fa2] ; mov [5e8ffe],ax`.
        byte* decode = Mem.s_EntityDecodeTable_005e8f00;
        *(uint*)(decode + 0xfc) = *(uint*)(decode + 0xa0);
        *(ushort*)(decode + 0xfe) = (ushort)(player * 0x40 + *(ushort*)(decode + 0xa2));

        // `cmp eax,0x4000400 ; mov [5e909a],dl ; jnz` — il pose 1 puis se retracte.
        uint attributes = *(uint*)(found + 0x14);
        if ((attributes & 0x400) != 0)
        {
            s_Unknown_005e909a = 1;
            if (attributes == 0x4000400) s_Unknown_005e909a = 0;
        }
        else
        {
            s_Unknown_005e909a = 0;
        }

        ushort value = *(ushort*)(found + 0x88);           // la valeur de base de l'objet
        byte tier = (byte)(value > 0x15e ? 1 : 0);         // trois `cmp` / `jbe` : NON SIGNES
        if (value > 1000) tier++;
        if (value > 5000) tier++;
        s_Unknown_005e909b = tier;

        s_Unknown_005e909c = 3;
        s_Unknown_005e909d = 1;
        s_Unknown_005e909e = 0;

        byte* sheet = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x94) + 4);
        sheet[0] = (byte)(index * 0x18);
        sheet[1] = (byte)(player * 0x18);
        sheet[7] = (byte)index;

        Mem.s_Unknown_005e909f = 1;

        // L'ecart en octets jusqu'a l'objet expertise, decremente de 0x280 par tour : `curseur[gap]`
        // reste sur lui sans qu'on le recalcule. Idiome du binaire, conserve.
        int gap = (int)(found - items);

        for (int remaining = 10; remaining != 0; remaining--, items += 0x280, gap -= 0x280)
        {
            if (items[0] == 0 || (*(ushort*)(items + 0x12) & 0x8000) != 0) continue;

            int same = 0;
            while (same < 0x10 && items[same] == items[same + gap]) same++;
            if (same == 0x10) *(ushort*)(items + 0x12) |= 0x8000;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: 0x005158f0 (opcode 14) — <b>affiche un dialogue</b>, attache a un locuteur. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur.
    ///
    /// <para>
    /// Le texte est pris dans la table <c>repertoire de zone + 0x70</c>, indexee par
    /// <c>script[1]</c>, et remis a <see cref="BindMenuLabels"/> avec le descripteur de la carte du
    /// monde.
    /// </para>
    /// <para>
    /// <b>Le choix du locuteur se lit a l'envers de l'intuition.</b> L'entite courante n'est
    /// remplacee que si elle est <i>deja</i> un locuteur valide — nulle, ou d'un genre autre que
    /// <c>0x800000</c>, elle est gardee telle quelle. Quand elle l'est, la fonction cherche le
    /// premier membre du groupe <b>disponible</b> (bits <c>0x50010000</c> tous eteints et champ
    /// <c>+0x12</c> nul) et le prend a sa place ; si aucun ne convient, l'entite d'origine reste.
    /// C'est ce que disent les trois <c>jne/je 00515945</c> du prologue, et rien d'autre.
    /// </para>
    /// </summary>
    private static byte* ScriptOpShowMessage(byte* ent, byte* s)
    {
        byte* speaker = ent;

        bool prefersPartyMember =
            speaker != null
            && (*(uint*)speaker & 0xfc0000) == 0x800000
            && ((*(uint*)speaker & 0x50000000) != 0 || *(ushort*)(speaker + 0x12) != 0);

        if (prefersPartyMember)
        {
            byte* slot = Mem.Arena(0x005e93c0);
            for (int remaining = s_PartySize_005e9020; remaining != 0; remaining--, slot += 0x9c)
            {
                if ((*(uint*)slot & 0x50010000) == 0 && *(ushort*)(slot + 0x12) == 0)
                {
                    speaker = slot;
                    break;
                }
            }
        }

        byte* text = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + s[1] * 4);
        BindMenuLabels(text, (uint)(nint)speaker, s_WorldMapDescriptor_005633b0, 0);
        return s + 2;
    }

    // =====================================================================
    // LES TROIS VERBES SIMPLES DU LABYRINTHE DES MORTS — table 0x0056aec8, zone 5
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00444620</c> — <b>le verbe du creneau 5</b>, le meme dans les <b>trois</b> blocs
    /// du labyrinthe (<c>0x00557ba8</c>, <c>0x00558228</c>, <c>0x005588a8</c>). 77 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/AreaVerbs/FUN_00444620.asm</c>.
    ///
    /// <para>
    /// <b>Il lit les quatre boutons de face d'une manette.</b> L'octet 0 de la banque
    /// <c>0x005e9060</c> designe la manette ; le verbe va chercher son <b>mot des boutons tenus</b>
    /// — le mot 0 de son entree de <see cref="Input.s_InputStateBase_00604a20"/>, entrees de
    /// <c>0x10</c> octets — et repose dans le <b>meme octet</b> quatre bits :
    /// </para>
    /// <list type="table">
    ///   <item><term>bit 0</term><description><c>0x8000</c> de la manette — le carre</description></item>
    ///   <item><term>bit 1</term><description><c>0x1000</c> — le triangle</description></item>
    ///   <item><term>bit 2</term><description><c>0x2000</c> — le rond</description></item>
    ///   <item><term>bit 3</term><description><c>0x4000</c> — la croix</description></item>
    /// </list>
    /// <para>
    /// La disposition des bits est celle d'une manette PlayStation, relevee dans
    /// <c>Engine/Input.cs</c> ; le mot 0 de la table d'entree est <b>actif haut</b> — un bit pose
    /// veut dire « tenu » — parce que <c>UpdateButtonEdges</c> l'y range deja inverse.
    /// </para>
    /// <para>
    /// <b>Le resultat de condition n'est pas ce qu'on croit.</b> Le binaire ne remet jamais EAX a
    /// zero : le dernier registre ecrit est le <c>mov ax,[eax+604a20h]</c> du mot des boutons, et
    /// l'octet haut d'EAX est nul parce que l'index tient sur douze bits. Le verbe rend donc le
    /// <b>mot des boutons tenus lui-meme</b>, pas la reponse rangee dans la banque. C'est ecrit
    /// tel quel ici.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00444620()
    {
        byte* bank = Mem.Arena(0x005e9060);

        // Ghidra: `movzx ax,byte [5e9060h] ; movsx eax,ax ; shl eax,4` — l'index de manette est
        // lu non signe puis multiplie par la taille d'une entree.
        ushort held = *(ushort*)(Inp.s_InputStateBase_00604a20 + bank[0] * 0x10);

        bank[0] = 0;
        if ((held & 0x8000) != 0) bank[0] = 1;
        if ((held & 0x1000) != 0) bank[0] |= 2;
        if ((held & 0x2000) != 0) bank[0] |= 4;
        if ((held & 0x4000) != 0) bank[0] |= 8;

        return held;
    }

    /// <summary>
    /// Ghidra: FUN_00444400 @ 0x00444400 — <b>le verbe du creneau 7</b> du bloc
    /// <c>0x00557ba8</c> (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 30 instructions.
    /// Corps partage avec <see cref="AreaVerb_00444450"/>.
    /// </summary>
    private static uint AreaVerb_00444400() => SpawnEffect39OnEligibleMembers();

    /// <summary>
    /// Ghidra: FUN_00444450 @ 0x00444450 — <b>le meme verbe, creneau 7 du bloc
    /// <c>0x00558228</c></b> (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c> : le compilateur a
    /// emis <b>deux fois le meme corps</b>, STRICT identique instruction pour instruction. Les deux
    /// adresses partagent donc <see cref="SpawnEffect39OnEligibleMembers"/>.
    /// </summary>
    private static uint AreaVerb_00444450() => SpawnEffect39OnEligibleMembers();

    /// <summary>
    /// Le corps commun des deux verbes ci-dessus : il <b>parcourt les quatre emplacements du
    /// groupe</b> (<c>0x005e93c0</c>, pas de <c>0x9c</c>) et fait naitre l'effet de sous-type
    /// <c>0x39</c> sur chacun de ceux qui passent <b>quatre</b> filtres, tous sur le mot d'etat
    /// sauf un :
    ///
    /// <list type="bullet">
    ///   <item>bit <c>0x80000000</c> pose — l'emplacement est occupe ;</item>
    ///   <item>bits <c>0x50000000</c> absents ;</item>
    ///   <item>bit <c>0x200</c> de <c>+0x90</c> pose ;</item>
    ///   <item>bit <c>0x1</c> absent.</item>
    /// </list>
    /// <para>
    /// La naissance passe par <see cref="SpawnEffectNearCamera_00468110"/>, donc elle
    /// <b>n'a pas lieu</b> pour un membre a plus de 2000 unites de la camera. Le binaire empile six
    /// arguments la ou l'appelee n'en lit que quatre — meme constat que sur les autres sites.
    /// </para>
    /// <para>
    /// <b>Le resultat de condition est un reste de registre.</b> La boucle ne met jamais EAX a
    /// zero : a la sortie il contient soit le retour de la derniere naissance, soit le mot d'etat
    /// du quatrieme emplacement quand celui-ci n'a pas passe les filtres. C'est suivi a
    /// l'identique ici plutot que remplace par un zero de confort.
    /// </para>
    /// </summary>
    private static uint SpawnEffect39OnEligibleMembers()
    {
        uint result = 0;
        byte* slot = Mem.Arena(0x005e93c0);

        for (int remaining = 4; remaining != 0; remaining--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            result = flags;

            if ((flags & 0x80000000) != 0
                && (flags & 0x50000000) == 0
                && (*(uint*)(slot + 0x90) & 0x200) != 0
                && (flags & 1) == 0)
            {
                result = SpawnEffectNearCamera_00468110(slot, 0x39, 0, -1);   // Ghidra: FUN_00468110
            }
        }

        return result;
    }

    // =====================================================================
    // PALIER 2, LOT 2 (2026-09-18) — LES QUATRE VERBES DU CHATEAU DE L'IMMORTEL
    //
    // Quatre lignes de `GestionnairesEntites.md` qui ne sont PAS des gestionnaires d'entite : ce
    // sont des creneaux de la table 0x0056aec8, donc des verbes de script, et ils vivent ici. C'est
    // le meme cas que 0x00444340 au lot 8 du Labyrinthe, et ils en sont les voisins d'adresse — la
    // table range les verbes par zone.
    //
    // DEUX D'ENTRE EUX SONT LE MEME VERBE, A UN RAPPEL PRES : « donner au groupe un objet tire au
    // sort dans la table de la zone ». Le premier range l'objet tout de suite, le second differe le
    // rangement jusqu'a la fermeture de la boite de dialogue. C'est la seule difference, et elle se
    // lit a l'`add esp` du site d'appel : 8 octets contre 12.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x005e9704</c> — <b>l'octet 0x24 de la banque de script 0</b>. Son bit
    /// <c>0x40</c> est le drapeau « c'est fait » des deux verbes de don du chateau : tant qu'il
    /// n'est pas pose, le verbe rend zero et le script reste sur place.
    /// </summary>
    private static byte* s_CastleGiftDone_005e9704 => Mem.s_StateBank0_005e96e0 + 0x24;

    /// <summary>
    /// Ghidra: <c>0x005e986c</c> — <b>la fiche en attente</b>, plus un. Zero veut dire « rien en
    /// attente » ; c'est ce <c>+1</c> qui permet a la fiche d'indice 0 d'exister. Les deux verbes
    /// de don s'en servent pour <b>reessayer la meme fiche a l'image suivante</b> quand
    /// l'inventaire du groupe est plein.
    /// </summary>
    private static byte* s_CastleGiftPending_005e986c => Mem.Arena(0x005e986c);

    /// <summary>
    /// Ghidra: <c>0x00444670</c> — <b>« donner au groupe un objet tire au sort »</b>, sous-zones
    /// 0 a 4 et 6 a 10. Absent de <c>Blaze.c</c> ; 41 instructions relevees au desassembleur.
    ///
    /// <para>
    /// Le verbe rend <c>1</c> — donc le script avance — <b>uniquement</b> quand le drapeau
    /// <see cref="s_CastleGiftDone_005e9704"/> est pose. Tant qu'il ne l'est pas, il rend zero et
    /// recommence a l'image suivante :
    /// </para>
    /// <list type="number">
    ///   <item>rien en attente : <see cref="PickWeightedTableEntry"/> choisit une fiche dans la
    ///     ligne <paramref name="row"/> de la table <c>+0xb4</c> de la zone, borne 100, et
    ///     <see cref="GrantChestContents_00530F20"/> la remet au groupe <b>sans rappel de fin de
    ///     boite</b> ;</item>
    ///   <item>si la remise passe (retour nul), le drapeau est pose et l'attente effacee ;</item>
    ///   <item>sinon le retour est <c>fiche + 1</c>, garde tel quel dans
    ///     <see cref="s_CastleGiftPending_005e986c"/> ;</item>
    ///   <item>a l'image suivante, l'attente non nulle fait rejouer <b>la meme fiche</b>
    ///     (<c>attente - 1</c>) sans nouveau tirage.</item>
    /// </list>
    /// <para>
    /// <b>Le second passage ne remet pas l'attente a zero quand il reussit</b> : il pose le drapeau
    /// et laisse <c>0x005e986c</c> tel quel. Transcrit tel quel — le drapeau suffit a ce que le
    /// verbe ne repasse plus.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00444670(byte* entity, short row)
    {
        if ((*s_CastleGiftDone_005e9704 & 0x40) != 0) return 1;

        byte pending = *s_CastleGiftPending_005e986c;

        if (pending == 0)
        {
            int sheet = PickWeightedTableEntry(row, 100);                     // Ghidra: FUN_0053a920
            int left = GrantChestContents_00530F20((uint*)entity, sheet, 0);  // Ghidra: FUN_00530f20

            if ((short)left == 0)
            {
                *s_CastleGiftPending_005e986c = 0;
                *s_CastleGiftDone_005e9704 |= 0x40;
                return 0;
            }

            *s_CastleGiftPending_005e986c = (byte)left;
            return 0;
        }

        int again = GrantChestContents_00530F20((uint*)entity, pending - 1, 0);
        if ((short)again == 0) *s_CastleGiftDone_005e9704 |= 0x40;
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00444a40</c> — <b>le meme verbe, rangement differe</b>, sous-zones 6 a 10.
    /// Absent de <c>Blaze.c</c> ; 59 instructions.
    ///
    /// <para>
    /// Il empile <c>0x00444ab0</c> comme <b>rappel de fin de boite</b> — d'ou l'<c>add esp,0Ch</c>
    /// la ou <see cref="AreaVerb_00444670"/> fait <c>add esp,8</c> — et c'est le rappel, non le
    /// verbe, qui pose le drapeau. Voir <see cref="CastleGiftDone_00444AB0"/>.
    /// </para>
    /// <para>
    /// <b>Et le sens du test est inverse par rapport a son jumeau.</b> Ici le retour NUL de
    /// <see cref="GrantChestContents_00530F20"/> ne fait rien du tout : le verbe ne garde l'attente
    /// que lorsque la remise echoue. C'est coherent — quand elle passe, c'est le rappel qui
    /// conclura.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00444A40(byte* entity, short row)
    {
        if ((*s_CastleGiftDone_005e9704 & 0x40) != 0) return 1;

        byte pending = *s_CastleGiftPending_005e986c;

        if (pending == 0)
        {
            int sheet = PickWeightedTableEntry(row, 100);                            // Ghidra: FUN_0053a920
            int left = GrantChestContents_00530F20((uint*)entity, sheet, 0x00444ab0);
            if ((short)left != 0) *s_CastleGiftPending_005e986c = (byte)left;
            return 0;
        }

        int again = GrantChestContents_00530F20((uint*)entity, pending - 1, 0x00444ab0);
        if ((short)again != 0) *s_CastleGiftPending_005e986c = (byte)again;
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00444ab0</c> — <b>le rappel de fin de boite</b> de
    /// <see cref="AreaVerb_00444A40"/>. Absent de <c>Blaze.c</c> ; quatorze instructions, relevees
    /// au desassembleur dans la foulee du verbe.
    ///
    /// <para>
    /// La forme exacte de <see cref="ChestPickupDone_00450700"/> — le porteur du script est
    /// dedesigne (<c>+0x6c = 0</c>), la porte de validation des boites est rabaissee, et
    /// <see cref="StoreItemInInventory_005312D0"/> range enfin l'objet — mais la ou le coffre
    /// reporte le reliquat dans sa table de butin, celui-ci le garde dans l'attente du verbe et,
    /// s'il n'y a pas de reliquat, <b>pose le drapeau « c'est fait »</b>.
    /// </para>
    /// <para>
    /// <b>La porte n'est pas la meme que celle du coffre</b> : <c>0x005e987f</c> ici,
    /// <c>0x005e9060</c> la-bas. Deux octets voisins de la meme arene, deux portes distinctes.
    /// </para>
    /// </summary>
    internal static void CastleGiftDone_00444AB0(uint* entity)
    {
        *(uint*)((byte*)entity + 0x6c) = 0;
        *Mem.Arena(0x005e987f) = 0;                            // Ghidra: DAT_005e987f

        int remaining = StoreItemInInventory_005312D0(entity);

        if (remaining != 0)
        {
            *s_CastleGiftPending_005e986c = (byte)remaining;
            return;
        }

        *s_CastleGiftPending_005e986c = 0;
        *s_CastleGiftDone_005e9704 |= 0x40;
    }

    /// <summary>
    /// Ghidra: FUN_00444e00 @ 0x00444e00 — <b>« allumer ou eteindre toutes les entites de scene
    /// d'espece 4 »</b>, toutes sous-zones sauf 5 et 11. Decompile dans <c>Blaze.c</c>,
    /// 33 instructions.
    ///
    /// <para>
    /// Il balaie les <b>trente-deux</b> entites de scene de <c>0x005ecaa0</c>, au pas de
    /// <c>0x9c</c>, et ne retient que celles dont l'espece <c>+0x0c</c> vaut <c>4</c> et qui sont
    /// vivantes. Sur celles-la, l'argument decide :
    /// </para>
    /// <list type="table">
    ///   <item><term><c>0</c></term><description>l'entite <b>s'allume</b> : posture <c>+0x18 = 3</c>,
    ///     <c>+0x1a = 1</c>, et les bits <c>0x2008</c> du mot d'etat sont poses ;</description></item>
    ///   <item><term>autre</term><description>les memes bits sont <b>retires</b>, et rien d'autre
    ///     n'est touche — ni la posture, ni le compteur.</description></item>
    /// </list>
    /// <para>
    /// <b>Le balayage ne consulte pas le compte d'entites de la sous-zone</b> : il fait toujours
    /// ses trente-deux tours. C'est la meme boucle en dur que les autres verbes de zone.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00444E00(short off)
    {
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0x20; i != 0; i--, scene += 0x9c)
        {
            if (scene[0x0c] != 4 || (*(uint*)scene & 0x80000000) == 0) continue;

            if (off == 0)
            {
                *(short*)(scene + 0x18) = 3;
                *(short*)(scene + 0x1a) = 1;
                *(uint*)scene |= 0x2008;
            }
            else
            {
                *(uint*)scene &= 0xffffdff7;
            }
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004450a0</c> — <b>« plus personne n'est en etat d'agir ? »</b>, sous-zones 5
    /// et 11. Absent de <c>Blaze.c</c> ; dix-huit instructions.
    ///
    /// <para>
    /// Il rend <c>1</c> tant qu'<b>aucun</b> des quatre emplacements ne porte a la fois le bit
    /// <c>0x80000000</c> (vivant), aucun bit de <c>0x50000000</c>, et un <c>+0x12</c> nul. Le
    /// premier emplacement qui remplit les trois conditions fait tomber le resultat a zero — la
    /// boucle continue quand meme ses quatre tours, sans pouvoir remonter.
    /// </para>
    /// <para>
    /// <b>Le masque interdit est <c>0x50000000</c>, pas <c>0x50010000</c></b> : c'est le meme piege
    /// que <c>0x004618B0</c> au Labyrinthe, ou deux comptes d'emplacements voisins ne refusaient
    /// pas les memes etats. Ce verbe-ci n'est donc pas
    /// <see cref="ActivePartySlotCount_00466D70"/> retourne.
    /// </para>
    /// <para>
    /// Il ne lit <b>aucun</b> de ses deux arguments.
    /// </para>
    /// </summary>
    private static uint AreaVerb_004450A0()
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;
        int none = 1;

        for (int i = 4; i != 0; i--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;
            if (*(short*)(slot + 0x12) != 0) continue;
            none = 0;
        }

        return (uint)(short)none;
    }

    // =====================================================================
    // LES DEUX VERBES DES RUINES SOUTERRAINES (2026-09-19)
    //
    // Les creneaux 0 et 2 du bloc 0x0055a2a8 (table 0x0056aec8, zone 7, sous-zones 0 a 2), voisins
    // d'adresse du dernier verbe du chateau. Deux lignes de `GestionnairesEntites.md` qui ne sont
    // pas des gestionnaires d'entite. Dumps : Legacy/Raw/Blaze/UndergroundRuins/.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00445120</c> — <b>degeler les trente-deux porteurs d'effet</b>. Absent de
    /// <c>Blaze.c</c> ; dix instructions.
    ///
    /// <para>
    /// Il efface le bit <c>0x04000000</c> — celui que <see cref="FreezeAllEntities"/> pose — sur
    /// les <c>0x20</c> enregistrements de <c>0x005ff400</c>, <b>sans condition</b>. C'est un
    /// morceau de <see cref="UnfreezeAllEntities"/>, qui degele cette table-la et deux autres, mais
    /// en epargnant les enregistrements retires (<c>0x10000000</c>) ; ce verbe-ci n'epargne rien.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_00445120()
    {
        byte* record = Mem.Entity(0x005ff400);

        for (int i = 0x20; i != 0; i--, record += 0x9c)
            *(uint*)record &= 0xfbffffff;

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004450e0</c> — <b>« combien de membres sont debout ? »</b>, range dans
    /// <b>l'octet 1 de la banque 2</b> (<c>DAT_005e9061</c>). Absent de <c>Blaze.c</c> ; dix-huit
    /// instructions.
    ///
    /// <para>
    /// Le meme compte que <see cref="AreaVerb_00443960"/> — <c>0x80000000</c> sans
    /// <c>0x50000000</c> sur les quatre emplacements —, mais <b>la variable est fixee</b> au lieu
    /// d'etre designee par l'argument. L'octet est d'abord remis a zero, puis ecrit une seconde
    /// fois a la fin de la boucle : le compte tient dans un registre entre les deux.
    /// </para>
    /// <para>
    /// L'octet 1 de la banque 2 est aussi celui que lit la scene des eclairs du chateau
    /// (<c>ImmortalCastleScenes.cs</c>) et le membre que vise le verbe d'effet de la banque 2.
    /// </para>
    /// </summary>
    /// <returns>Toujours zero.</returns>
    private static uint AreaVerb_004450E0()
    {
        byte standing = 0;
        Mem.s_StateBank2_005e9060[1] = standing;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 4; i != 0; i--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) standing++;
        }

        Mem.s_StateBank2_005e9060[1] = standing;
        return 0;
    }

    // =====================================================================
    // LES QUATRE VERBES DES RUINES DANS LE LAC (2026-09-19)
    //
    // Les creneaux de la table 0x0056aec8 propres a la zone 9, voisins d'adresse des deux verbes
    // des ruines souterraines. Tous les quatre lisent LE BIT 8 DE L'OCTET D'IDENTITE d'un membre
    // (MainMem + 0xf0150 + rang * 0x2000) — le quatrieme bit, apres les trois de la classe ; celui
    // que les postures du Chasseur (Tick/PostureAndInput.cs) et la sonde du Sosie lisent aussi, et
    // que MineMecanismes.cs appelle le sexe. Aucun ne dit quelle valeur est lequel.
    //
    // Trois d'entre eux sont LE MEME VERBE A DEUX CONSTANTES PRES : trouver le premier membre debout
    // (0x80000000 sans 0x50000000, dans l'ordre des emplacements) dont le bit a la valeur voulue, et
    // lui faire dire un libelle de la zone (`BindMenuLabels`, le meme couple que la boite de
    // dialogue de MineMecanismes.cs). Ils ne lisent pas l'entite qui les appelle. Dumps :
    // Legacy/Raw/Blaze/LakeRuins/.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004451b0</c> — <b>« deux membres debout, dont un seul porte le bit 8, et c'est
    /// l'entite qui parle ? »</b>. Absent de <c>Blaze.c</c> ; 56 instructions.
    ///
    /// <para>
    /// Il compte les membres debout — <b>au masque <c>0x50010000</c></b>, pas <c>0x50000000</c>
    /// comme ses trois voisins — et parmi eux ceux qui portent le bit 8. Puis il lit le bit 8 du
    /// membre que l'entite appelante designe par son octet <c>+8</c>. Il rend <b>0</b> (le script
    /// avance) si et seulement si les trois conditions tiennent : deux debout, un seul porteur, et
    /// c'est l'entite. Sinon il rend <b>1</b>.
    /// </para>
    /// <para>
    /// Les deux comptes sont compares en seize bits (<c>cmp di,2</c>, <c>cmp word [esp+10h],1</c>).
    /// </para>
    /// </summary>
    private static uint AreaVerb_004451B0(byte* entity)
    {
        byte* mainMem = GetMainMemPtr();
        short standing = 0;
        short marked = 0;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50010000) != 0) continue;

            standing++;
            if ((mainMem[0xf0150 + (i << 0xd)] & 8) != 0) marked++;
        }

        int own = (mainMem[0xf0150 + (entity[8] << 0xd)] >> 3) & 1;

        if (standing == 2 && marked == 1 && own == 1) return 0;
        return 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00445250</c> — <b>le premier membre debout SANS le bit 8 dit le libelle
    /// <c>+0x10</c></b> de la table <c>+0x70</c> du repertoire de zone. Absent de <c>Blaze.c</c> ;
    /// 43 instructions. S'il n'y en a aucun, rien n'est dit. Rend toujours zero.
    /// </summary>
    private static uint AreaVerb_00445250()
    {
        byte* member = FirstStandingMemberByMark(false);
        if (member != null)
        {
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x10);
            BindMenuLabels(labels, member, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004452d0</c> — <b>le premier membre debout AVEC le bit 8 dit le libelle
    /// <c>+0x14 + 4 * n</c></b>, <c>n</c> etant l'operande du script : le seul des trois qui
    /// choisit sa replique. Absent de <c>Blaze.c</c> ; 44 instructions. Rend toujours zero.
    /// </summary>
    private static uint AreaVerb_004452D0(short line)
    {
        byte* member = FirstStandingMemberByMark(true);
        if (member != null)
        {
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + line * 4 + 0x14);
            BindMenuLabels(labels, member, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00445360</c> — <see cref="AreaVerb_00445250"/> au libelle pres :
    /// <c>+0x2c</c>. Absent de <c>Blaze.c</c> ; 43 instructions. Rend toujours zero.
    /// </summary>
    private static uint AreaVerb_00445360()
    {
        byte* member = FirstStandingMemberByMark(false);
        if (member != null)
        {
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x2c);
            BindMenuLabels(labels, member, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00445150</c> — <b>rappeler les membres ecartes</b>, le verbe de la vallee
    /// d'argent. Absent de <c>Blaze.c</c> ; 30 instructions.
    ///
    /// <para>
    /// Chaque emplacement qui porte <c>0x10000000</c> le perd, avec <c>0x6000</c>
    /// (<c>and 0xefff9fff</c>) ; son <c>+0x12</c> revient a zero et ses trois echelles a
    /// <c>0x1000</c>. Et les deux compteurs du tri de groupe (<see cref="PrepareLeaderAndPartyFlags"/>)
    /// perdent un chacun : l'octet <c>0x005de030</c> et <see cref="s_Unknown_005de544"/>, les
    /// membres ecartes. Il ne lit aucun argument et rend zero.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00445150()
    {
        byte removed = *(byte*)Mem.Camera(0x005de030);            // Ghidra: DAT_005de030

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 4; i != 0; i--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x10000000) == 0) continue;

            removed--;
            *(uint*)slot = flags & 0xefff9fff;
            *(short*)(slot + 0x12) = 0;
            *(short*)(slot + 0x28) = 0x1000;
            *(short*)(slot + 0x2a) = 0x1000;
            *(short*)(slot + 0x2c) = 0x1000;
            s_Unknown_005de544--;                                  // Ghidra: DAT_005de544
        }

        *(byte*)Mem.Camera(0x005de030) = removed;
        return 0;
    }

    // =====================================================================
    // LES SIX VERBES DE LA GROTTE SCELLEE (2026-09-19, lot 2 de la zone)
    //
    // Tous absents de Blaze.c, tous de trois a vingt instructions. Dumps :
    // Legacy/Raw/Blaze/SealedCave/.
    // =====================================================================

    /// <summary>Ghidra: <c>0x004457e0</c> — remet l'octet <see cref="s_Unknown_00566c88"/> a
    /// <c>0xff</c>, sa valeur au chargement (voir <c>DebugParty.cs</c>), et rend zero. Trois
    /// instructions.</summary>
    private static uint AreaVerb_004457E0()
    {
        s_Unknown_00566c88 = 0xff;
        return 0;
    }

    /// <summary>Ghidra: <c>0x004454c0</c> — <b>« l'entite porte-t-elle le bit <c>0x200</c> de son
    /// mot de contact <c>+0x90</c> ? »</b> Rend 1 ou 0 : le script avance quand le bit est
    /// eteint. Cinq instructions.</summary>
    private static uint AreaVerb_004454C0(byte* entity)
        => (*(uint*)(entity + 0x90) >> 9) & 1;

    /// <summary>
    /// Ghidra: <c>0x00445740</c> — <b>quitter la carte du monde vers l'etat de jeu 10</b>. Sept
    /// instructions : le bit 2 des drapeaux de boucle (<c>Mem.s_WorldFlags_005de02c</c>),
    /// le pas <c>0xfe</c> qui arrete la boucle de la carte a la fin de l'image — le meme que la
    /// sortie du sort de <c>School0Wizard.cs</c> —, et l'etat suivant, 10. Rend zero.
    /// </summary>
    private static uint AreaVerb_00445740()
    {
        uint flags = Mem.s_WorldFlags_005de02c;
        s_WorldMapStep_005645f8 = -2;                               // 0xfe
        s_GameStateId_005de510 = 10;
        Mem.s_WorldFlags_005de02c = flags | 2;
        return 0;
    }

    /// <summary>Ghidra: <c>0x00445810</c> — <b>blesser l'entite de 5 pour cent</b>
    /// (<see cref="ApplyPercentDamage_005397B0"/>), lui poser le bit <c>0x40</c> et un verrou de
    /// trois images. Rend zero.</summary>
    private static uint AreaVerb_00445810(byte* entity)
    {
        ApplyPercentDamage_005397B0((uint*)entity, 5, 0);           // Ghidra: FUN_005397b0
        *(ushort*)(entity + 0x96) = 3;
        *(uint*)entity |= 0x40;
        return 0;
    }

    /// <summary>Ghidra: <c>0x004457a0</c> — une fois sur sept (tirage compare sur seize bits), fait
    /// naitre l'effet <c>0xeb</c> sur l'entite, sans planche. Rend zero.</summary>
    private static uint AreaVerb_004457A0(byte* entity)
    {
        if ((short)(CrtRand() % 7) == 1)
            SpawnStatusEffectEntity_0047AC40(entity, (int*)(entity + 0x1c), null, 0xeb, 0, -1);   // Ghidra: FUN_0047ac40

        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00445760</c> — <b>« plus personne n'est en etat d'agir ? »</b>, le meme sens que
    /// <see cref="AreaVerb_004450A0"/> du chateau (vivant, sans <c>0x50000000</c>, <c>+0x12</c> nul),
    /// mais ecrit autrement : il sort des le premier membre qui repond, en rendant 0, au lieu de
    /// finir la boucle. Rend 1 si aucun.
    /// </summary>
    private static uint AreaVerb_00445760()
    {
        for (short i = 0; i <= 3; i++)
        {
            byte* slot = Inp.s_SlotFlags_005e93c0 + i * 0x9c;
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if (*(short*)(slot + 0x12) == 0) return 0;
        }

        return 1;
    }

    /// <summary>
    /// Ghidra: <c>0x004453e0</c> — grotte scellee, sous-zones 2 et 3 (lot 4, 2026-09-19). Absent de
    /// <c>Blaze.c</c> ; 39 instructions. <b>La vue saute sur un membre</b> : l'operande (0 a 3)
    /// designe l'un des quatre enregistrements <c>0x005fa988 + n * 0x9c</c> ; son placement est
    /// finalise (<see cref="FinalizeEntityPlacement2"/>), puis sa pose est publiee dans les blocs de
    /// vue — la meme rafale que le radeau de la foret. Rend zero.
    /// <para>
    /// <b>Hors de 0 a 3, le pointeur reste nul</b> et la finalisation le recoit tel quel : le binaire
    /// n'a pas de garde. Transcrit sans en ajouter.
    /// </para>
    /// </summary>
    /// <summary>
    /// Ghidra: <c>0x004454e0</c> (pas de 24 unites) et <c>0x00445610</c> (pas de 8) — grotte
    /// scellee, sous-zones 5, 8 et 10 (lot 5, 2026-09-19) ; absents de <c>Blaze.c</c>, 82
    /// instructions, a ces trois immediates pres. <b>Pousser l'entite d'un pas</b>, si elle porte le
    /// bit <c>0x800000</c> : l'operande donne la direction (0 : +X, 1 : +Z, 2 : -X, 3 : -Z). Son
    /// placement est finalise ; si son mot de contact revient avec <c>0x8800</c>, elle est heurtee
    /// et le pas est annule — mot d'etat, placement, contact (lu en seize bits) rendus — et ses
    /// decalages <c>+0x44</c>/<c>+0x4c</c> sont NIES. Rend zero.
    /// </summary>
    private static uint AreaVerb_004454E0(byte* entity, short direction, int step)
    {
        uint flags = *(uint*)entity;
        if ((flags & 0x800000) == 0) return 0;

        int at7c = *(int*)(entity + 0x7c);
        int at60 = *(int*)(entity + 0x60);
        int at64 = *(int*)(entity + 0x64);
        int at80 = *(int*)(entity + 0x80);
        ushort at90 = *(ushort*)(entity + 0x90);
        int x = *(int*)(entity + 0x1c);
        int z = *(int*)(entity + 0x24);
        int at8c = *(int*)(entity + 0x8c);
        int at84 = *(int*)(entity + 0x84);
        int at88 = *(int*)(entity + 0x88);

        if (direction == 0) *(int*)(entity + 0x1c) = x + step;
        if (direction == 1) *(int*)(entity + 0x24) = z + step;
        if (direction == 2) *(int*)(entity + 0x1c) -= step;
        if (direction == 3) *(int*)(entity + 0x24) -= step;

        FinalizeEntityPlacement2((uint*)entity);                    // Ghidra: FUN_00533940

        if ((*(uint*)(entity + 0x90) & 0x8800) == 0) return 0;

        *(int*)(entity + 0x7c) = at7c;
        *(int*)(entity + 0x60) = at60;
        *(int*)(entity + 0x64) = at64;
        *(uint*)entity = flags;
        *(int*)(entity + 0x84) = at84;
        *(int*)(entity + 0x88) = at88;
        *(int*)(entity + 0x80) = at80;
        *(uint*)(entity + 0x90) = at90;
        *(int*)(entity + 0x1c) = x;
        *(int*)(entity + 0x24) = z;
        *(int*)(entity + 0x8c) = at8c;
        *(int*)(entity + 0x44) = -*(int*)(entity + 0x44);
        *(int*)(entity + 0x4c) = -*(int*)(entity + 0x4c);
        return 0;
    }

    private static uint AreaVerb_004453E0(short member)
    {
        byte* m = null;
        if (member == 0) m = Mem.Entity(0x005fa988);
        if (member == 1) m = Mem.Entity(0x005faa24);
        if (member == 2) m = Mem.Entity(0x005faac0);
        if (member == 3) m = Mem.Entity(0x005fab5c);

        FinalizeEntityPlacement2((uint*)m);                         // Ghidra: FUN_00533940

        for (int i = 0; i < 4; i++)
            *(uint*)Mem.Entity(0x005f7d70 + i * 4) = *(uint*)(m + 0x80 + i * 4);

        *(int*)Mem.Arena(0x005e9a30) = *(int*)(m + 0x1c);
        *(int*)Mem.Arena(0x005e9a34) = *(int*)(m + 0x20);
        *(int*)Mem.Arena(0x005e9a38) = *(int*)(m + 0x24);
        *(short*)Mem.Arena(0x005e9b38) = *(short*)(m + 0x90);
        *(uint*)Mem.Entity(0x005f7b48) = *(uint*)(m + 0x7c);
        return 0;
    }

    /// <summary>
    /// Ghidra: la boucle commune de <c>0x00445250</c>, <c>0x004452d0</c> et <c>0x00445360</c>,
    /// recopiee en ligne dans les trois : les quatre emplacements dans l'ordre, le premier debout
    /// (<c>0x80000000</c> sans <c>0x50000000</c>) dont le bit 8 de l'identite vaut
    /// <paramref name="marked"/>. <c>null</c> si aucun.
    /// </summary>
    private static byte* FirstStandingMemberByMark(bool marked)
    {
        byte* mainMem = GetMainMemPtr();
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i <= 3; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            bool bit = (mainMem[0xf0150 + (i << 0xd)] & 8) != 0;
            if (bit == marked) return slot;
        }

        return null;
    }

    // =====================================================================
    // PALIER 3 (2026-09-18) — TROIS VERBES DE PLUS DU CHATEAU DE L'IMMORTEL
    //
    // Encore trois lignes de `GestionnairesEntites.md` qui sont des verbes de 0x0056aec8. Les deux
    // premiers sont LE MEME VERBE A UN APPEL PRES : infliger au membre que designe la banque 2 l'un
    // de onze effets, choisi par l'operande. Le second (sous-zones 6 a 10) est le premier sans les
    // nombres flottants. Le troisieme est la sortie des sous-zones 5 et 11.
    // =====================================================================

    /// <summary>
    /// Le corps commun de <see cref="AreaVerb_004446F0"/> et <see cref="AreaVerb_00444AF0"/> — deux
    /// tables de sauts a <b>onze entrees dans le meme ordre</b>, <c>0x00444a08</c> et
    /// <c>0x00444dc8</c>, relevees au desassembleur. Les deux sont absents de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Le membre vise est l'octet 1 de la banque 2</b> (<c>DAT_005e9061</c>) : ni l'entite
    /// appelante, ni le premier argument. Sa fiche est <c>MainMem + (membre + 0x78) * 0x2000</c>,
    /// la fiche que les herbes ecrivent directement (voir <c>Combat/HerbEffects.cs</c>).
    /// </para>
    /// <list type="table">
    ///   <item><term>0 a 4, 9</term><description><b>un etat</b> : l'effet de sous-type <c>0x29</c>
    ///     nait sur le membre avec l'atlas de l'etat, le bit est pose dans <c>+0x140</c>, et la duree
    ///     est portee <b>a au moins dix</b> — une duree plus longue deja en place est gardee.
    ///     Les six couples atlas / bit / duree sont ceux de
    ///     <see cref="SpawnStatusEffectEntity_0047AC40"/> chez les effets d'objet.</description></item>
    ///   <item><term>5, 6</term><description><b>rendre ou retirer le vingtieme des PV
    ///     maximum</b>. Le gain est plafonne au maximum ; la perte qui fait tomber a zero ou moins
    ///     met les PV a zero et pose <c>0x40000000</c>.</description></item>
    ///   <item><term>7, 8</term><description>la meme chose <b>sur les PM</b> — mais la perte ne
    ///     pose aucun bit, et ne ramene a zero que strictement en dessous (<c>jge</c> la ou les
    ///     PV testent <c>jg</c>).</description></item>
    ///   <item><term>10</term><description><b>lever le bit 1</b> et remettre sa duree
    ///     <c>+0x1e0</c> a zero — la forme de <see cref="ClearHerbStatus_0053AE20"/>, sans
    ///     validation.</description></item>
    /// </list>
    /// <para>
    /// L'operande est compare <b>sans signe</b> a dix (<c>cmp ecx,edi ; ja</c>) : une valeur
    /// negative ne fait rien. Le verbe rend toujours zero.
    /// </para>
    /// </summary>
    /// <param name="showNumbers">Vrai pour <c>0x004446F0</c> : les cas 5 a 8 font partir le montant
    /// en nombre flottant par <see cref="SpawnParticle_0050F070"/>, genre 1 pour un gain, 0 pour
    /// une perte. <c>0x00444AF0</c> n'a pas ces quatre appels, et c'est sa seule difference.</param>
    private static uint AfflictBankMember_004446F0(short effect, bool showNumbers)
    {
        uint slot = Mem.s_StateBank2_005e9060[1];
        byte* member = Inp.s_SlotFlags_005e93c0 + slot * 0x9c;
        byte* sheet = GetMainMemPtr() + (slot + 0x78) * 0x2000;

        switch ((uint)effect)
        {
            case 0: InflictBankMemberStatus(member, sheet, 0x005647d0, 4, 0x18e); break;
            case 1: InflictBankMemberStatus(member, sheet, 0x005647e0, 8, 0x190); break;
            case 2: InflictBankMemberStatus(member, sheet, 0x005647f0, 0x20, 0x194); break;
            case 3: InflictBankMemberStatus(member, sheet, 0x00564848, 0x1000, 0x1a2); break;
            case 4: InflictBankMemberStatus(member, sheet, 0x005647c0, 1, 0x1e0); break;

            case 5:
            {
                int amount = *(short*)(sheet + 0x148) / 20;
                *(short*)(sheet + 0x14c) += (short)amount;
                if (showNumbers) SpawnParticle_0050F070((uint*)(member + 0x1c), (uint)amount, 1);

                if (*(short*)(sheet + 0x14c) > *(short*)(sheet + 0x148))
                {
                    *(short*)(sheet + 0x14c) = *(short*)(sheet + 0x148);
                }
                break;
            }

            case 6:
            {
                int amount = *(short*)(sheet + 0x148) / 20;
                *(short*)(sheet + 0x14c) -= (short)amount;
                if (showNumbers) SpawnParticle_0050F070((uint*)(member + 0x1c), (uint)amount, 0);

                if (*(short*)(sheet + 0x14c) <= 0)
                {
                    *(short*)(sheet + 0x14c) = 0;
                    *(uint*)(sheet + 0x140) |= 0x40000000;
                }
                break;
            }

            case 7:
            {
                int amount = *(short*)(sheet + 0x14a) / 20;
                *(short*)(sheet + 0x14e) += (short)amount;
                if (showNumbers) SpawnParticle_0050F070((uint*)(member + 0x1c), (uint)amount, 1);

                if (*(short*)(sheet + 0x14e) > *(short*)(sheet + 0x14a))
                {
                    *(short*)(sheet + 0x14e) = *(short*)(sheet + 0x14a);
                }
                break;
            }

            case 8:
            {
                int amount = *(short*)(sheet + 0x14a) / 20;
                *(short*)(sheet + 0x14e) -= (short)amount;
                if (showNumbers) SpawnParticle_0050F070((uint*)(member + 0x1c), (uint)amount, 0);

                if (*(short*)(sheet + 0x14e) < 0) *(short*)(sheet + 0x14e) = 0;
                break;
            }

            case 9: InflictBankMemberStatus(member, sheet, 0x00564850, 0x400, 0x19e); break;

            case 10:
                *(ushort*)(sheet + 0x1e0) = 0;
                *(uint*)(sheet + 0x140) &= 0xfffffffe;
                break;
        }

        return 0;
    }

    /// <summary>Un cas « etat » de <see cref="AfflictBankMember_004446F0"/> : le binaire deroule
    /// six fois le meme bloc, seuls changent l'atlas, le bit et le champ de duree.</summary>
    private static void InflictBankMemberStatus(byte* member, byte* sheet, int atlas, uint bit, int durationOffset)
    {
        SpawnStatusEffectEntity_0047AC40(member, (int*)(member + 0x1c), SpriteAtlas(atlas), 0x29, 0, 0);   // Ghidra: FUN_0047ac40

        *(uint*)(sheet + 0x140) |= bit;
        if (*(ushort*)(sheet + durationOffset) < 10) *(ushort*)(sheet + durationOffset) = 10;
    }

    /// <summary>Ghidra: <c>0x004446f0</c> — verbe <b>10</b> du bloc <c>0x00558f28</c>, sous-zones
    /// 0 a 4 : l'effet, <b>avec</b> les nombres flottants. 848 octets.</summary>
    private static uint AreaVerb_004446F0(short effect) => AfflictBankMember_004446F0(effect, true);

    /// <summary>Ghidra: <c>0x00444af0</c> — verbe <b>10</b> du bloc <c>0x005595a8</c>, sous-zones
    /// 6 a 10 : le meme effet, <b>sans</b> nombre flottant. 784 octets.</summary>
    private static uint AreaVerb_00444AF0(short effect) => AfflictBankMember_004446F0(effect, false);

    /// <summary>
    /// Ghidra: <c>0x00444ef0</c> — le verbe <b>12</b> du bloc <c>0x00559c28</c>, sous-zones 5 et
    /// 11. Absent de <c>Blaze.c</c> ; 125 instructions relevees au desassembleur.
    ///
    /// <para>
    /// <b>« Tout le groupe est-il sorti du rectangle ? » — et si oui, lancer un script.</b> Le verbe
    /// ne fait rien si l'octet <c>DAT_005de058</c> ne vaut pas 2, ni si le bit 2 de
    /// <c>DAT_005e9706</c> (banque 0, octet <c>0x26</c>) est pose.
    /// </para>
    /// <para>
    /// Il parcourt les quatre membres. Chaque membre debout (<c>0x80000000</c> sans
    /// <c>0x50010000</c>) est compte ; s'il est <b>hors</b> du rectangle
    /// <c>x ∈ [0x8fc000, 0xbea000]</c>, <c>z ∈ [0xfff24000, 0x26c000]</c>, il est compte une
    /// seconde fois, et, s'il ne porte ni <c>1</c> ni <c>0x04000000</c>, il est <b>fige</b> : ses
    /// trois octets <c>+0x38</c> a <c>+0x3a</c> a <c>0xff</c>, le bit 4 de <c>+0x36</c> leve, son
    /// verrou <c>+0x96</c> a zero, le bit <c>0x04000000</c> pose, et un effet de sous-type
    /// <b><c>0x9d</c></b> nait sur lui, sans atlas.
    /// </para>
    /// <para>
    /// Quand les deux comptes sont egaux et non nuls — tous les membres debout sont dehors — le
    /// script d'indice <paramref name="scriptIndex"/> part. Il part sur le membre designe par
    /// <c>DAT_005de538</c> <b>si son emplacement ne porte aucun des bits <c>0xd0000000</c></b> ;
    /// sinon sur le premier membre debout. La premiere branche lance donc le script sur un
    /// emplacement que la boucle n'aurait pas retenu : transcrit tel quel.
    /// </para>
    /// <para>
    /// Le verbe rend toujours zero. Ce qui l'empeche de relancer le script a chaque image n'est pas
    /// ici : c'est vraisemblablement le script lui-meme qui pose le bit de <c>0x005e9706</c>.
    /// </para>
    /// </summary>
    private static uint AreaVerb_00444EF0(short scriptIndex)
    {
        if ((byte)*Mem.Camera(0x005de058) != 2) return 0;
        if ((Mem.s_StateBank0_005e96e0[0x26] & 2) != 0) return 0;

        int standing = 0;
        int outside = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50010000) != 0) continue;

            standing++;

            int x = *(int*)(member + 0x1c);
            int z = *(int*)(member + 0x24);
            if (x >= 0x8fc000 && x <= 0xbea000 && z >= unchecked((int)0xfff24000) && z <= 0x26c000) continue;

            outside++;

            if ((flags & 0x04000001) != 0) continue;

            member[0x38] = 0xff;
            member[0x39] = 0xff;
            member[0x3a] = 0xff;
            *(ushort*)(member + 0x36) &= 0xfffb;
            *(ushort*)(member + 0x96) = 0;
            *(uint*)member |= 0x04000000;

            SpawnStatusEffectEntity_0047AC40(member, (int*)(member + 0x1c), null, 0x9d, 0, -1);   // Ghidra: FUN_0047ac40
        }

        if ((short)standing != (short)outside || (short)standing == 0 || (short)outside == 0) return 0;

        byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;

        if ((*(uint*)leader & 0xd0000000) == 0)
        {
            RunEntityScript(s_Unknown_005e97cc, leader, (uint)scriptIndex);   // Ghidra: FUN_00513dd0
            return 0;
        }

        member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50010000) != 0) continue;

            RunEntityScript(s_Unknown_005e97cc, member, (uint)scriptIndex);
            break;
        }

        return 0;
    }
}
