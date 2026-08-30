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
        byte* record = *(byte**)(*(byte**)(((byte**)Mem.Arena(0x005e97c0))[s[1]] + 0x74) + area * 4);

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

            // 0x004441F0 : `xor eax,eax ; ret`, verifie au desassembleur. A distinguer de la souche
            // ci-dessus — celle-ci met EAX a zero explicitement, donc le resultat de condition est
            // 0 et non l'indice du verbe. Deux souches, deux comportements.
            0x004441F0 => 0,

            0x00442D90 => AreaVerb_00442D90(ent, (short)(s[2] | (s[3] << 8))),

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

            // Le verbe 5 : le rocher qui publie le titre tire du compteur de victimes. Il ne lit
            // ni l'entite ni le second argument — son creneau vient de la variable de script 4.
            0x004438A0 => AreaVerb_004438A0(),

            // Les verbes 8 et suivants : deux EFFETS SONORES, et rien d'autre. Les deux plus
            // employes du jeu — 78 emplois chacun — pour vingt-huit octets. Le 8 est celui de la
            // porte, signale le 2026-08-30.
            0x004457F0 => AreaVerb_004457F0(),
            0x00445840 => AreaVerb_00445840(),

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
}
