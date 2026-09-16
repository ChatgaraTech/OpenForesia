// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA MACHINE A OPCODES DES OBJETS — Ghidra FUN_00523aa0 et la table PTR_LAB_00564f80.
//
// Une fiche d'objet du CD porte un SCRIPT : une suite d'octets dont le premier de chaque
// instruction indexe une table de gestionnaires. `RunItemScript_00523AA0` la deroule,
// `DispatchItemOp` aiguille, et les `ItemOp…` sont les opcodes un par un - consommer, sauter,
// arithmetique sur soi / sur la cible / sur la banque, choisir une cible, poser un effet, semer
// une particule, jouer un son, ouvrir le menu objets.
//
// Trois opcodes sortent du fichier et c'est ce qui rend ce fichier interessant :
//   - l'opcode 18 entre dans la table des 48 effets (`Combat/EffectTable.cs`) - une potion, un
//     piege de coffre et un sort de guerison passent tous les trois par la ;
//   - l'opcode 14 appelle `CastSpell_0053A980` (`Spells/SpellCasting.cs`) ;
//   - les opcodes 9 et 10 font naitre un sous-type, par le MEME mecanisme qu'une fiche de sort.
//
// CE QUE CES SCRIPTS ATTEIGNENT EST RECENSE HORS LIGNE, sans jouer : les 2 370 fiches du CD ne
// font naitre que 21 gestionnaires d'entite. Voir Documentation/Etat/ObjetsAtteignables.md,
// genere par `sh OpenForesia.Tools/census-item-effects.sh`.
//
// Les corps que ces scripts font naitre sont dans `ItemSpawns.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // La machine a opcodes des objets (Ghidra: FUN_00523aa0 et la table PTR_LAB_00564f80).
    //
    // C'est la SECONDE machine a opcodes du jeu. Sa boucle est celle de `RunScript` a la ligne
    // pres — arret sur 0xff, ou des qu'un gestionnaire pose un resultat, sauf l'opcode 2 qui est
    // la branche — mais sa table est autre : 23 opcodes, 22 corps distincts (12 et 19 partagent le
    // meme), de 0x00523b00 a 0x00524170.
    //
    // **Onze de ces vingt-deux corps sont absents de `Blaze.c`.** Ghidra ne les a vus que pris en
    // adresse dans la table, d'ou le prefixe `PTR_LAB_`. Ils sont transcrits depuis le
    // desassemblage, conserve dans `Legacy/Raw/Blaze/FUN_00523aa0-handlers.disasm.txt`.
    //
    // Les quatre arguments que chaque gestionnaire recoit, etablis en croisant les corps decompiles
    // avec les decalages de pile des autres :
    //
    //   slot    l'enregistrement d'entite de 0x9c octets de celui qui se sert de l'objet ;
    //           `slot[0x70]` designe sa cible, ce que les opcodes 16 et 17 posent
    //   record  son bloc par personnage de 0x2000 octets (statistiques, equipement, inventaire)
    //   item    le bloc de 0x280 octets de l'objet employe, dans `record + 0x300 + n * 0x280`
    //   s       le curseur de script, qui vit DANS le bloc d'objet — d'ou les sauts des opcodes
    //           1 et 2, qui sont relatifs a `item` et non a `s`
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00523aa0 @ 0x00523aa0 — <b>la machine a opcodes des objets</b>. Jumelle exacte
    /// de <see cref="RunScript"/> : elle s'arrete sur <c>0xff</c>, et aussi des qu'un gestionnaire
    /// pose un resultat non nul — <b>sauf pour l'opcode 2</b>, le seul a continuer malgre un
    /// resultat pose. C'est le mecanisme de condition : un test rend 1, la boucle s'arreterait, et
    /// l'opcode 2 est la branche qui reprend la main.
    /// </summary>
    private static void RunItemScript_00523AA0(uint* slot, byte* record, byte* item, byte* script)
    {
        NoteItemUsed(item);          // ECHAFAUDAGE : hors du binaire, voir la sonde plus bas.

        uint condition = 0;                              // Ghidra: local_4

        for (uint op = *script; op != 0xff && (op == 2 || condition == 0); op = *script)
        {
            script = DispatchItemOp(op, slot, record, item, script, &condition);
        }
    }


    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_LAB_00564f80)[opcode])(…)</c> — les 23 gestionnaires.
    ///
    /// <para>Rendue en <c>switch</c> comme <see cref="DispatchScriptOp"/>, et pour la meme raison :
    /// c'est une table de sauts dans l'original, et <c>Strategy.md</c> demande de reconstruire le
    /// flot. L'etendue est <b>deduite</b> : le dword qui suit l'entree 22 est nul.</para>
    ///
    /// <para><b>Un opcode inconnu n'est pas garde dans l'original</b> — il saute dans la table sans
    /// borne, donc au-dela de 22 il lit un pointeur quelconque. Le rendre bruyant ici vaut mieux
    /// qu'un saut dans le vide : c'est le seul endroit ou la transcription s'ecarte, et elle
    /// s'ecarte vers un plantage nomme.</para>
    /// </summary>
    private static byte* DispatchItemOp(
        uint op, uint* slot, byte* record, byte* item, byte* s, uint* result)
        => op switch
        {
            0 => ItemOpConsume(record, item, s),                          // 0x00523b00
            1 => ItemOpJump(item, s),                                     // 0x00523bb0
            2 => ItemOpJumpIf(item, s, result),                           // 0x00523bd0
            3 => ItemOpAluSelf(slot, s, result),                          // 0x00523c00
            4 => ItemOpAluTarget(slot, s, result),                        // 0x00523c30
            5 => ItemOpAluRecord(record, 0x100, s, result),               // 0x00523c70
            6 => ItemOpAluTargetStats(slot, 0, s, result),                // 0x00523cb0
            7 => ItemOpAluRecord(record, 0x200, s, result),               // 0x00523d20
            8 => ItemOpAluTargetStats(slot, 0x100, s, result),            // 0x00523d60
            9 => ItemOpSpawnSelf(slot, s),                                // 0x00523dd0
            10 => ItemOpSpawnTarget(slot, s),                             // 0x00523e10
            11 => ItemOpAluBank(s, result),                               // 0x00523e50
            12 => ItemOpParticleSelf(slot, s),                            // 0x00524060
            13 => ItemOpAluEquipped(record, s, result),                   // 0x00523e80
            14 => ItemOpCast(slot, s),                                    // 0x00523ed0
            15 => ItemOpRefuseSound(s),                                   // 0x00523f00
            16 => ItemOpPickTargetByIndex(slot, s, result),               // 0x00523f10
            17 => ItemOpPickTargetBySearch(slot, s, result),              // 0x00523f80
            18 => ItemOpApplyEffect(slot, item, s, result),               // 0x00523fc0
            19 => ItemOpParticleSelf(slot, s),                            // 0x00524060, le meme
            20 => ItemOpParticleTarget(slot, s),                          // 0x005240d0
            21 => ItemOpSound(s),                                         // 0x00524140
            22 => ItemOpOpenItemMenu(s),                                  // 0x00524170
            _ => throw new NotImplementedException(
                     $"Script d'objet : opcode {op} hors des 23 de PTR_LAB_00564f80. "
                     + "L'original saute dans la table sans borne ; ici on le dit."),
        };

    /// <summary>Ghidra: LAB_00523b00 — <b>consommer l'objet</b>, avec une chance sur cent de
    /// <c>s[1]</c>.
    ///
    /// <para>Le tirage rate ne fait rien du tout. Reussi, il decremente le compte de l'objet
    /// (<c>item[0x10]</c>, un mot) et, <b>seulement si ce compte tombe pile a zero</b>, vide
    /// l'emplacement puis reconstruit deux choses : la liste d'inventaire de <c>record[0x2c0]</c> /
    /// <c>+0x2c1</c>, et les cinq emplacements d'equipement de <c>+0x2b0</c>, dont celui qui
    /// designait l'objet retire repasse a <c>0xff</c>.</para>
    ///
    /// <para>La liste de <c>+0x2c0</c> n'est pas celle du menu rapide (<c>+0x2d0</c>/<c>+0x2d1</c>,
    /// voir <see cref="TickQuickMenu_00530070"/>) : celle-ci recense tous les objets portes, celle-la
    /// ceux d'une categorie.</para>
    ///
    /// <para>Un compte deja nul retombe a <c>0xffff</c> et l'objet survit : c'est ce que fait
    /// l'original, le test porte sur l'egalite a zero et non sur le signe.</para></summary>
    private static byte* ItemOpConsume(byte* record, byte* item, byte* s)
    {
        if (CrtRand() % 100 >= s[1]) return s + 2;        // Ghidra: FUN_00547140

        if (--*(ushort*)(item + 0x10) != 0) return s + 2;

        item[0] = 0;
        record[0x2c0] = 0;

        for (int i = 0; i < 10; i++)
        {
            byte* candidate = record + 0x300 + i * 0x280;

            if (candidate[0] != 0)
            {
                record[0x2c1 + record[0x2c0]] = (byte)i;
                record[0x2c0]++;
                continue;
            }

            // L'emplacement vide qu'on vient de liberer, et lui seul, deshabille le personnage.
            if (candidate != item) continue;

            byte* worn = record + 0x2b0;
            for (int k = 5; k != 0; k--, worn++)
            {
                if (*worn == i) { *worn = 0xff; break; }
            }
        }

        return s + 2;
    }

    /// <summary>Ghidra: LAB_00523bb0 — le saut. La destination est un decalage de <b>seize bits</b>
    /// depuis le debut du <b>bloc d'objet</b>, pas depuis le curseur : le script vit dans ce bloc,
    /// donc c'est bien lui la base.</summary>
    private static byte* ItemOpJump(byte* item, byte* s) => item + (s[1] | (s[2] << 8));

    /// <summary>Ghidra: LAB_00523bd0 — le saut conditionnel, et <b>le seul opcode que la boucle
    /// laisse passer quand un resultat est pose</b>. Il saute si la condition est non nulle, et la
    /// remet a zero du meme geste ; sinon il avance de trois.</summary>
    private static byte* ItemOpJumpIf(byte* item, byte* s, uint* result)
    {
        if (*result == 0) return s + 3;

        byte* destination = item + (s[1] | (s[2] << 8));
        *result = 0;
        return destination;
    }

    /// <summary>Ghidra: FUN_00523c00 — l'unite arithmetique sur <b>l'enregistrement d'entite</b> de
    /// celui qui se sert de l'objet.</summary>
    private static byte* ItemOpAluSelf(uint* slot, byte* s, uint* result)
    {
        *result = ApplyScriptOp((byte*)slot + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00523c30 — la meme, sur l'enregistrement de <b>la cible</b>
    /// (<c>slot[0x70]</c>).</summary>
    private static byte* ItemOpAluTarget(uint* slot, byte* s, uint* result)
    {
        byte* target = *(byte**)((byte*)slot + 0x70);
        *result = ApplyScriptOp(target + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00523c70 (<paramref name="bank"/> = 0x100) et FUN_00523d20
    /// (0x200) — l'unite arithmetique sur l'une des deux banques du <b>bloc par personnage</b>.
    /// Deux corps identiques a la constante pres dans l'original.</summary>
    private static byte* ItemOpAluRecord(byte* record, int bank, byte* s, uint* result)
    {
        *result = ApplyScriptOp(record + bank + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: LAB_00523cb0 (<paramref name="bank"/> = 0) et LAB_00523d60 (0x100) —
    /// l'unite arithmetique sur le <b>bloc de statistiques de la cible</b>.
    ///
    /// <para>La resolution du bloc est celle de <see cref="StatusBubbleHeight_00538BC0"/>, et elle
    /// depend du bit <c>0x800000</c> : un membre du groupe passe par <c>MainMem + 0xf0100</c>, toute
    /// autre entite par <see cref="s_EntityStatBlocks_005ea560"/>. Passer par une vue d'arene ici
    /// serait le defaut d'aliasing deja resolu une fois pour cette table.</para></summary>
    private static byte* ItemOpAluTargetStats(uint* slot, int bank, byte* s, uint* result)
    {
        byte* target = *(byte**)((byte*)slot + 0x70);

        byte* stats = (*(uint*)target & 0x800000) != 0
            ? GetMainMemPtr() + 0xf0100 + target[8] * 0x2000
            : s_EntityStatBlocks_005ea560 + target[8] * 0x120;

        *result = ApplyScriptOp(stats + bank + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00523dd0 — <b>engendre un effet sur soi</b>. Le mot de <c>+0x0a</c> est
    /// mis a zero le temps de l'appel puis restaure : c'est ce qui empeche l'effet d'heriter du
    /// compteur de son parent.</summary>
    private static byte* ItemOpSpawnSelf(uint* slot, byte* s)
    {
        byte* e = (byte*)slot;
        ushort kept = *(ushort*)(e + 0x0a);
        *(ushort*)(e + 0x0a) = 0;
        SpawnEffectEntity(e, s[1], s[2], s[3]);          // Ghidra: FUN_0047a9d0
        *(ushort*)(e + 0x0a) = kept;
        return s + 4;
    }

    /// <summary>Ghidra: LAB_00523e10 — la meme, sur <b>la cible</b>.</summary>
    private static byte* ItemOpSpawnTarget(uint* slot, byte* s)
    {
        byte* target = *(byte**)((byte*)slot + 0x70);
        ushort kept = *(ushort*)(target + 0x0a);
        *(ushort*)(target + 0x0a) = 0;
        SpawnEffectEntity(target, s[1], s[2], s[3]);     // Ghidra: FUN_0047a9d0
        *(ushort*)(target + 0x0a) = kept;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00523e50 — l'unite arithmetique sur <b>la banque de variables de
    /// script</b>, celle que les deux machines partagent.</summary>
    private static byte* ItemOpAluBank(byte* s, uint* result)
    {
        *result = ApplyScriptOp(Mem.Arena(0x005e9060) + s[1], s[2], s[3]) ? 1u : 0u;
        return s + 4;
    }

    /// <summary>Ghidra: FUN_00523e80 — l'unite arithmetique sur le bloc de <b>l'objet equipe</b> a
    /// l'emplacement <c>s[1]</c> : <c>record[0x2b0 + s[1]]</c> donne l'indice, et le bloc suit a
    /// <c>record + 0x300 + indice * 0x280</c>. Cinq octets d'opcode, seul de son espece.</summary>
    private static byte* ItemOpAluEquipped(byte* record, byte* s, uint* result)
    {
        byte* equipped = record + 0x300 + record[0x2b0 + s[1]] * 0x280;
        *result = ApplyScriptOp(equipped + s[2], s[3], s[4]) ? 1u : 0u;
        return s + 5;
    }

    /// <summary>Ghidra: FUN_00523ed0 — <b>declenche un sort ou un effet d'objet</b> par
    /// <see cref="CastSpell_0053A980"/>. Les quatre octets de script sont pousses en <b>zero</b>
    /// extension (<c>xor</c> puis <c>mov al</c> dans le binaire), donc les deux tests de signe de
    /// l'appelee ne peuvent pas se declencher par ce chemin — ils servent a ses autres
    /// appelants.</summary>
    private static byte* ItemOpCast(uint* slot, byte* s)
    {
        CastSpell_0053A980(slot, s[1], s[2], s[3], s[4]);
        return s + 5;
    }

    /// <summary>Ghidra: LAB_00523f00 — le son de refus, et rien d'autre. Un octet
    /// d'opcode.</summary>
    private static byte* ItemOpRefuseSound(byte* s)
    {
        QuickMenuRefuseSound();                          // Ghidra: FUN_0053dbc0
        return s + 1;
    }

    /// <summary>Ghidra: LAB_00523f10 — <b>choisit la cible par son indice</b>, pris dans la banque
    /// de variables a <c>s[2]</c> et non dans le script : c'est donc une cible calculee.
    ///
    /// <para><c>s[1]</c> choisit la famille : zero les quatre emplacements de groupe
    /// (<see cref="Input.s_SlotFlags_005e93c0"/>), non nul les trente-deux entites de scene
    /// (<c>0x005ecaa0</c>). Les deux tables ont le meme pas de <c>0x9c</c>. Un indice hors borne
    /// <b>pose la condition a 1</b> et laisse la cible inchangee — c'est le seul opcode de choix
    /// qui signale son echec.</para></summary>
    private static byte* ItemOpPickTargetByIndex(uint* slot, byte* s, uint* result)
    {
        uint index = Mem.Arena(0x005e9060)[s[2]];

        if (s[1] != 0)
        {
            if (index >= 0x20) { *result = 1; return s + 3; }
            *(byte**)((byte*)slot + 0x70) = Mem.Entity(0x005ecaa0) + index * 0x9c;
        }
        else
        {
            if (index >= 4) { *result = 1; return s + 3; }
            *(byte**)((byte*)slot + 0x70) = Inp.s_SlotFlags_005e93c0 + index * 0x9c;
        }

        return s + 3;
    }

    /// <summary>Ghidra: LAB_00523f80 — <b>choisit la cible par recherche</b>.
    ///
    /// <para>Le tour de passe-passe est dans les trois champs sauves : <c>+0x74</c> est pointe sur
    /// l'entite elle-meme le temps de l'appel, parce que <c>FUN_00474ad0</c> cherche depuis
    /// <c>[+0x74]</c> et non depuis son argument ; puis le resultat, que la recherche a range en
    /// <c>+0x78</c>, est recopie en <c>+0x70</c>, et <c>+0x74</c> comme <c>+0x78</c> sont
    /// restaures. La cible du script est donc <b>ce que la recherche a trouve</b>, sans que rien
    /// d'autre de l'entite ne bouge.</para></summary>
    private static byte* ItemOpPickTargetBySearch(uint* slot, byte* s, uint* result)
    {
        byte* e = (byte*)slot;

        byte* keptWatched = *(byte**)(e + 0x74);
        byte* keptFound = *(byte**)(e + 0x78);

        *(byte**)(e + 0x74) = e;
        *result = PickTargetBySearch_00474AD0(slot, s[1]);

        byte* found = *(byte**)(e + 0x78);
        *(byte**)(e + 0x74) = keptWatched;
        *(byte**)(e + 0x70) = found;
        *(byte**)(e + 0x78) = keptFound;

        return s + 2;
    }

    /// <summary>Ghidra: &amp;DAT_0052403c — <b>la famille de l'objet</b>, indexee par le mot de
    /// <c>item[0x84]</c> (0 a 0x15). Relevee verbatim. Elle ne sert qu'a choisir, par la table de
    /// sauts de <c>0x00524014</c>, entre les deux seules valeurs que l'opcode 18 passe en troisieme
    /// argument : <b>0 ou 1</b>. Les familles 1, 3, 5 et 7 donnent 1, les autres 0.</summary>
    private static readonly byte[] s_ItemFamily_0052403c =
    [
        0x00, 0x00, 0x01, 0x01, 0x01, 0x02, 0x02, 0x03, 0x03, 0x03, 0x03,
        0x04, 0x05, 0x05, 0x05, 0x06, 0x06, 0x07, 0x07, 0x07, 0x07, 0x08,
    ];

    /// <summary>Ghidra: LAB_00523fc0 — <b>applique un effet</b> de la table de 48 fonctions de
    /// <c>0x0056d4e0</c>, choisi par <c>s[1]</c>.
    ///
    /// <para>Le troisieme argument vaut 1 pour les familles impaires 1, 3, 5 et 7 de
    /// <see cref="s_ItemFamily_0052403c"/>, et 0 partout ailleurs — <b>y compris</b> quand
    /// <c>item[0x84]</c> depasse 0x15, ou le binaire saute directement a la branche zero. Le detour
    /// par une table de sauts a neuf entrees dont sept sont des doublons est ce que le compilateur
    /// a fait d'un <c>switch</c> ; le resultat tient en une comparaison.</para></summary>
    private static byte* ItemOpApplyEffect(uint* slot, byte* item, byte* s, uint* result)
    {
        ushort kind = *(ushort*)(item + 0x84);

        uint flag = 0;
        if (kind <= 0x15)
        {
            byte family = s_ItemFamily_0052403c[kind];
            flag = family == 1 || family == 3 || family == 5 || family == 7 ? 1u : 0u;
        }

        byte* target = *(byte**)((byte*)slot + 0x70);
        *result = ApplyItemEffect_0056D4E0(s[1], slot, target, flag);
        return s + 2;
    }

    /// <summary>Ghidra: FUN_00524060 (opcodes 12 et 19, le meme corps) — <b>pose une particule</b>
    /// a la position de celui qui se sert de l'objet.
    ///
    /// <para><c>s[1]</c> dit la largeur avec laquelle lire la banque de variables a <c>s[2]</c> :
    /// 0 octet, 1 mot, 2 double mot. <b>Toute autre valeur passe le curseur de script lui-meme</b>
    /// comme parametre — le registre n'est pas initialise dans cette branche, et Ghidra le rend
    /// exactement ainsi (<c>uVar2 = param_4</c>). Transcrit tel quel.</para></summary>
    private static byte* ItemOpParticleSelf(uint* slot, byte* s)
    {
        SpawnParticle_0050F070((uint*)((byte*)slot + 0x1c), ScriptBankValue(s), s[3]);
        return s + 4;
    }

    /// <summary>Ghidra: FUN_005240d0 (opcode 20) — la meme, a la position de <b>la cible</b>.</summary>
    private static byte* ItemOpParticleTarget(uint* slot, byte* s)
    {
        byte* target = *(byte**)((byte*)slot + 0x70);
        SpawnParticle_0050F070((uint*)(target + 0x1c), ScriptBankValue(s), s[3]);
        return s + 4;
    }

    /// <summary>La lecture de banque commune aux opcodes 12, 19 et 20 — voir
    /// <see cref="ItemOpParticleSelf"/> pour la quatrieme branche, celle qui n'initialise
    /// rien.</summary>
    private static uint ScriptBankValue(byte* s)
    {
        byte* bank = Mem.Arena(0x005e9060);

        return s[1] switch
        {
            0 => bank[s[2]],
            1 => *(ushort*)(bank + s[2]),
            2 => *(uint*)(bank + s[2]),
            _ => (uint)(nuint)s,
        };
    }

    /// <summary>Ghidra: FUN_00524140 — <b>joue un son</b>. Le meme mot sert de gain gauche et de
    /// gain droit : l'original lit deux fois <c>s+4</c>, ce n'est pas une coquille de
    /// transcription.</summary>
    private static byte* ItemOpSound(byte* s)
    {
        Aud.PlaySoundEffect(s[1], *(ushort*)(s + 2), *(ushort*)(s + 4), *(ushort*)(s + 4));
        return s + 6;
    }

    /// <summary>Ghidra: LAB_00524170 — <b>ouvre le menu objets</b>, ecran complet, depuis le script
    /// de l'objet. Le corps est celui de <see cref="RunItemMenuState"/> (<c>FUN_00524180</c>)
    /// duplique par le compilateur, a ceci pres que le second appel y est un saut de queue.
    ///
    /// <para><b>TODO(REVERSE) — cet opcode rend une valeur qui n'est pas un curseur de script.</b>
    /// Le saut de queue fait de la valeur rendue celle de <c>FUN_00524320</c>, dont le dernier
    /// appel est <c>FUN_00524190</c> — <see cref="DrawItemMenuTitle"/>, qui rend un <b>curseur de
    /// liste d'affichage</b>. La boucle de la machine dereference ensuite ce pointeur comme un
    /// opcode. C'est un defaut de 1998, latent : il ne se manifeste que si un script d'objet emet
    /// reellement l'opcode 22, ce qu'on n'a pas observe. Il n'est donc pas verse dans
    /// <c>legacy-bug.md</c>, qui ne recense que l'observe.</para>
    ///
    /// <para>La transcription s'arrete au bord : elle fait les deux appels de l'original et leve
    /// plutot que d'inventer une valeur de retour. Le jour ou le jeu y passe, la levee dira ou
    /// regarder.</para></summary>
    private static byte* ItemOpOpenItemMenu(byte* s)
    {
        _ = s;
        LoadItemMenuResources();                         // Ghidra: FUN_00524210
        RunItemMenuFrameLoop();                          // Ghidra: FUN_00524320

        throw new NotImplementedException(
            "Script d'objet, opcode 22 (LAB_00524170) : l'ecran du menu objets a tourne, et "
            + "l'original rend ici la valeur de FUN_00524320 — c'est-a-dire le curseur de liste "
            + "d'affichage de FUN_00524190, pas un curseur de script. La machine le dereference "
            + "ensuite comme un opcode. Defaut de 1998, jamais observe jusqu'ici : voir le "
            + "TODO(REVERSE) sur ItemOpOpenItemMenu avant de choisir quoi en faire.");
    }
}
