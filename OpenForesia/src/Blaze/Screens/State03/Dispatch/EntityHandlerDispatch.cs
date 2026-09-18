// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CARREFOUR — une adresse virtuelle, une methode. 426 cases.
//
// C'est le seul point de rendez-vous entre l'infrastructure de la carte du monde et les quelque
// 68 000 lignes de corps qu'elle atteint. Les tables de sauts du binaire portent des ADRESSES ;
// la reconstruction, elle, porte des methodes. `DispatchEntityHandlerCore` fait la conversion,
// case par case, et c'est deliberement une table plate : le patron de `CallAreaHandler` et de
// `ScriptOpAreaVerb`, ou toute cible inconnue leve en se nommant plutot que d'etre ignoree.
//
// IL NE SE DECOUPE PAS. Un switch de 426 cases est illisible par tranches : ce qu'on y cherche,
// c'est toujours UNE adresse, et la recherche se fait au grep. Le decoupage de 2026-09-16 l'a
// deplace entier.
//
// Deux creneaux meritent d'etre connus :
//   - `0x00401FA0` est la SOUCHE, un `ret` nu verifie dans `Blaze.c` : elle ne fait rien, et ce
//     n'est pas une lacune ;
//   - `0x0047ADC0` est le creneau « sous-type inconnu » de la table statique 0x00551c30 : il
//     ETEINT l'entite, il ne l'ignore pas. Soixante-neuf des 192 creneaux y pointent.
//
// Ce qui manque est compte et recapitule a la fermeture (`NoteMissingHandler`,
// `PrintMissingHandlerRecap`) ; ce qui passe REELLEMENT est releve par `CoverageProbes.cs`.
// Les trois tables statiques sont dans `StaticEntityTables.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

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
    /// <summary>
    /// Inventaire des creneaux qui retombent sur la souche vide, sous
    /// <c>OPENFORESIA_TRACE_ENTITY=1</c> (<c>playtest.ps1 -Trace ENTITY</c>).
    ///
    /// <para>C'est le <b>cas silencieux</b> et donc le plus difficile a debusquer : un gestionnaire
    /// absent leve en se nommant, un gestionnaire transcrit fait son travail, mais un creneau qui
    /// vaut <c>0x00401FA0</c> ne fait rien <b>et ne le dit pas</b>. Quand un decor repond a moitie —
    /// le couvercle du coffre s'ouvre mais le panneau ne vient pas — c'est la premiere chose a
    /// regarder.</para>
    ///
    /// <para>Chaque creneau n'est imprime qu'une fois : la liste reste courte et se lit d'un coup,
    /// la ou une trace par image serait illisible.</para>
    /// </summary>
    private static int s_TickCount;
    private static uint s_LastButtons = 0xffffffff;

    private static readonly bool s_TraceEntity =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_ENTITY") == "1";

    private static readonly System.Collections.Generic.HashSet<string> s_SeenEmptySlots = new();

    /// <summary>
    /// ECHAFAUDAGE — <b>le filet</b>, pose sous <c>OPENFORESIA_AI_STUB=1</c>. Sous ce drapeau, un
    /// gestionnaire d'entite non transcrit <b>ne tue plus le processus</b> : il se journalise une
    /// fois et rend la main, donc la creature reste immobile et la partie continue.
    ///
    /// <para>
    /// La regle « une lacune hurle » est tenue, pas contournee : elle hurle dans la trace au lieu
    /// de lever, et seulement sous ce drapeau — sans lui,
    /// <see cref="Resources.CallEntityHandler"/> leve comme avant. Ce que ca change est la
    /// <b>maniere de recolter</b> : une partie jouee d'un bout a l'autre rend la liste de TOUS les
    /// gestionnaires rencontres, la ou une levee n'en donne jamais qu'un, celui qui a tue le
    /// processus. C'est cette liste-la — et non le recensement de
    /// <c>Documentation/Ennemies/Comportement.md</c> — qui donne l'ordre de transcription : elle ne
    /// contient que ce que le joueur croise vraiment.
    /// </para>
    /// <para>
    /// <b>Une entite sans cervelle n'est pas une entite absente</b> : elle garde sa place, son
    /// gabarit et ses statistiques, elle ne decide simplement rien. Un decor ou un declencheur mis
    /// en souche peut donc bloquer une progression sans planter — c'est le prix du filet, et c'est
    /// pourquoi il ne s'allume pas tout seul.
    /// </para>
    /// </summary>
    private static readonly bool s_AiStub =
        Environment.GetEnvironmentVariable("OPENFORESIA_AI_STUB") == "1";

    /// <summary>
    /// Les lacunes rencontrees sous <see cref="s_AiStub"/>, comptees par <b>couple (gestionnaire,
    /// sous-zone)</b>. La sous-zone fait partie de la cle parce qu'un numero de sous-type ne veut
    /// rien dire tout seul : la table 0x0056ad48 est rechargee a chaque sous-zone, et le meme
    /// gestionnaire rencontre en deux lieux se transcrit une fois mais se rencontre deux fois.
    /// </summary>
    private static readonly
        System.Collections.Generic.Dictionary<(uint Target, int Zone, int SubArea), int>
            s_MissingHandlers = new();

    private static bool s_MissingHandlerRecapArmed;

    /// <summary>

    /// ECHAFAUDAGE : l'enveloppe de banc. Sous <c>OPENFORESIA_AI_BENCH</c>, chaque appel de
    /// gestionnaire est compte et sa levee retenue au lieu de tuer le run — c'est ce qui permet
    /// d'exercer vingt comportements d'un coup alors que le premier manquant aurait arrete la
    /// partie. Hors banc, l'appel est direct et rien ne change.
    /// </summary>
    private static void DispatchEntityHandler(uint target, byte* record, string where)
    {
        NoteHandlerSeen(target);

        if (!s_AiBench)
        {
            DispatchEntityHandlerCore(target, record, where);
            return;
        }

        BenchNoteCall(target);

        try
        {
            DispatchEntityHandlerCore(target, record, where);
        }
        catch (Exception error)
        {
            BenchNoteThrow(target, error);
        }
    }

    private static void DispatchEntityHandlerCore(uint target, byte* record, string where)
    {
        switch (target)
        {
            // Souche `ret` nu, verifiee dans Blaze.c. 0x0040A600 est la seconde : un `ret` seul lui
            // aussi (c3, verifie au desassembleur), installe dans 21 creneaux des blocs de
            // sous-zone, et deja traite en corps vide par Resources.CallAreaHandler.
            case 0x00401FA0:
            case 0x0040A600:
                if (s_TraceEntity && s_SeenEmptySlots.Add(where))
                {
                    Console.Error.WriteLine($"[SOUCHE] {where}");
                }
                return;

            // Le creneau « sous-type inconnu » de la table statique 0x00551c30 : il ETEINT
            // l'entite, il ne l'ignore pas. Soixante-neuf de ses 192 creneaux y pointent.
            case 0x0047ADC0: DespawnEntity_0047ADC0(record); return;

            case 0x004508D0: EntityHandler_004508D0((uint*)record); return;
            case 0x004509B0: EntityHandler_004509B0((uint*)record); return;
            case 0x00450AA0: EntityHandler_00450AA0((uint*)record); return;
            case 0x00450D30: EntityHandler_00450D30((uint*)record); return;
            case 0x00486EE0: EntityHandler_00486EE0((uint*)record); return;

            // Le sous-type 1 de l'auberge — la seconde et derniere routine d'intelligence de la
            // zone 0, et le QUATRIEME lanceur de sorts du depot. Elle porte le mecanisme
            // d'incantation du Behemoth au complet, mais choisit ses sorts par le PLACEMENT.
            case 0x00487260: TickSceneEntityAi_00487260(record); return;
            // La meme routine dans la mine abandonnee, sous-zones 0 a 2 : seule la table de sorts change
            // d'adresse, 0x00552880, dont les quatre rangees sont celles de 0x00552868.
            case 0x00488210: TickSceneEntityAi_00488210(record); return;
            // Et le Goblin-sorcier de la vieille tour, sous-zones 0, 1, 2 et 4 : sa propre table,
            // 0x00552a38, aux memes octets. Ai/Inn.cs.
            case 0x004A6A00: TickSceneEntityAi_004A6A00(record); return;

            // LA BELLE BLEUE — sous-type 7 de la mine (sous-zones 5 a 8), et deux copies au Labyrinthe
            // des morts. Ai/Mine/BlueBeauty.cs.
            case 0x0048B530:
            case 0x004BC530:
            case 0x004BE200:
            // La Belle verte de la vieille tour (sous-zones 0, 1, 2, 4) et sa copie stricte des ruines
            // souterraines : le meme corps a l'ordre des registres pres (le calcul de +0x54 permute
            // deux operandes), chercheur de proie recopie (0x004A8150, conforme a FUN_004A1020),
            // table de sauts identique en relatif. Verifie le 2026-09-17.
            case 0x004A8510:
            case 0x004DEA40:
                TickSceneEntityAi_0048B530(record); return;

            // LA SALAMANDRE — sous-type 12 de la mine (sous-zone 6), et sa copie de la montagne du
            // dragon de feu. La charpente du Gremlin, avec ses propres postures. Ai/Mine/Salamander.cs.
            case 0x0048E3A0:
            case 0x00497EF0:
                TickSceneEntityAi_0048E3A0(record); return;

            // L'OGRE — sous-type 14 des deux blocs de la mine, et ses deux copies de la grotte scellee.
            // La charpente du Pourpre preux sans la parade. Ai/Mine/Ogre.cs.
            case 0x0048F230:
            case 0x00494D10:
            case 0x004F9C70:
            case 0x004F23D0:
                TickSceneEntityAi_0048F230(record); return;

            // LA CHAUVE-SOURIS — sous-type 3 des deux blocs de la mine, et sa copie du chateau de
            // l'immortel. La charpente du Gremlin. Ai/Mine/Bat.cs.
            case 0x00489CA0:
            case 0x004901B0:
            case 0x004D01C0:
                TickSceneEntityAi_00489CA0(record); return;

            // 0x004a5f10 est un DUPLICATA de 0x00486ee0 : 299 instructions comparees une a une,
            // zero difference. Meme corps, deux creneaux.
            case 0x004A5F10: EntityHandler_00486EE0((uint*)record); return;
            case 0x00404EF0: EntityHandler_00404EF0((uint*)record); return;

            // L'entree 89 de la table statique 0x00551c30 — le seul creneau que la BRANCHE BASSE
            // de FUN_0047aa70 puisse atteindre, son engendreur ne posant que le sous-type 0x59.
            case 0x00403710: RestoreVitalsEntity_00403710(record); return;

            // L'entree 84 de la meme table — un effet de sort qui TIENT sa cible : animation 2
            // imposee et defilee a la main une image sur deux, gerbe de 0x38 et de 0x5a autour
            // d'elle. Signale en jeu le 2026-08-29, en frappant un goblin.
            case 0x00402FC0: EntityHandler_00402FC0(record); return;

            // L'entree 82, la sœur sombre de la precedente : meme prise de la victime, mais
            // elle la NOIRCIT de quinze par image et ne lache qu'une entite 0x34 par image.
            // Signalee en jeu le 2026-08-29, en frappant.
            case 0x004041C0: EntityHandler_004041C0(record); return;

            // L'entree 52 (0x34) de la table 0x00552230 — la RETOMBEE que la precedente lache
            // par une a chaque image : elle nait au-dessus de la victime, monte, s'assombrit
            // et s'eteint a la septieme image d'atlas.
            case 0x00404310: EntityHandler_00404310(record); return;

            // L'entree 80 (0x50) — un effet qui SUIT sa cible sans la tenir, la mesure selon
            // son genre et seme une entite 0x32 par image. Le gestionnaire le plus rencontre du
            // releve du 2026-08-29 : 1203 appels pour une sortie en foret.
            case 0x00403C10: EntityHandler_00403C10(record); return;

            // L'entree 50 (0x32) de la table 0x00552230 — la bouffee que le precedent seme :
            // meme atlas et meme mort a sept que 0x00404310, mais elle ne monte pas.
            case 0x00403D60: EntityHandler_00403D60(record); return;

            // L'entree 81 (0x51) — la troisieme de la fratrie 80-85 : meme prise de la victime que
            // la 82, mais deux sons dates par le compteur d'images, une entite 0x33 une image sur
            // deux, et un noircissement de dix au lieu de quinze.
            case 0x00403E80: EntityHandler_00403E80(record); return;

            // L'entree 51 (0x33) de la table 0x00552230 — ce que la precedente lache : elle monte
            // en accelerant, enfle, et reboucle sur son atlas jusqu'a ce que sa duree tombe.
            case 0x00404000: EntityHandler_00404000(record); return;

            // L'entree 83 (0x53) — la seule de la fratrie 80-85 qui se joue en DEUX temps : elle
            // seme du 0x35 pendant 6 a 10 images, puis lache onze 0x36 d'un coup a la dixieme
            // image de son attente, et s'eteint a la quinzieme.
            case 0x00404460: EntityHandler_00404460(record); return;

            // Ses deux enfants, entrees 53 (0x35) et 54 (0x36) de la table 0x00552230 : la bouffee
            // qui ne monte pas, et la gerbe qui file en ligne droite en enflant.
            case 0x004045F0: EntityHandler_004045F0(record); return;
            case 0x00404730: EntityHandler_00404730(record); return;

            // L'entree 54 (0x36) de la table 0x00551c30 — un sort de pretre qui rampe au sol en
            // spirale et frappe a chaque image, et les onze traineaux 0xa1 qu'il lache a sa
            // naissance (entree 161 de la table 0x00551f30).
            case 0x00447A90: EntityHandler_00447A90(record); return;
            case 0x00447D50: EntityHandler_00447D50(record); return;

            // L'entree 186 (0xba) et son enfant de meme numero dans l'autre table : le pere n'est
            // qu'une naissance qui s'efface, l'enfant est ce qui vole et porte le coup.
            case 0x00476770: EntityHandler_00476770(record); return;
            case 0x004767E0: EntityHandler_004767E0(record); return;

            // L'entree 182 (0xb6) — naissance qui depose une entite d'etat 0x3f et s'efface.
            case 0x004758C0: EntityHandler_004758C0(record); return;

            // L'entree 223 (0xdf) — meme forme, mais le sous-type de son enfant vaut 0xc0 + n, donc
            // au-dela des 192 creneaux statiques : il est TOUJOURS resolu par la table de sous-zone.
            case 0x00483450: EntityHandler_00483450(record); return;

            // L'ENFANT du precedent, resolu par la table de sous-zone : en foret 0xc0 + 0x1f
            // = 0xdf designe ce corps-ci. Un projectile qui roule, rebondit sur le plancher, et
            // ne survit qu'aux images ou son balayage ne touche personne.
            case 0x004834D0: EntityHandler_004834D0(record); return;

            // L'entree 181 (0xb5) — le PIEGE DE COFFRE : huit images, une gerbe 0x3d une image
            // sur deux, puis le coup porte par une entite d'emprunt posee en 0x005ede20.
            case 0x004755F0: EntityHandler_004755F0(record); return;

            // L'entree 61 (0x3d) de la table 0x00552230 — la gerbe que le precedent seme.
            case 0x00475730: EntityHandler_00475730(record); return;

            // L'entree 180 (0xb4) — l'AUTRE piege de coffre, tire une fois sur deux a la place du
            // precedent : seize images, une gerbe 0x3c a chaque image, puis 5 % des points de vie.
            case 0x004754C0: EntityHandler_004754C0(record); return;

            // L'entree 60 (0x3c) de la table 0x00552230 — la gerbe que le precedent seme.
            case 0x004755C0: EntityHandler_004755C0(record); return;

            // L'entree 63 (0x3f) — l'enfant de la naissance 0xb6 : trente-trois images de
            // brouillard autour de sa victime, puis un effet d'etat porte par 0x005ede20.
            case 0x00475910: EntityHandler_00475910(record); return;

            // L'entree 191 (0xbf) — le retour visuel du coup qui NE passe pas (victime immunisee
            // ou qui resiste). Toute la chaine des degats le fait naitre.
            case 0x00475D40: EntityHandler_00475D40(record); return;

            // L'entree 153 (0x99) de la table 0x00552230 — la poussiere que 0x3a seme une image
            // sur deux. Deux aspects dans un seul corps, choisis par +0x44 de la victime.
            case 0x004459E0: EntityHandler_004459E0(record); return;

            // LE RESTE DE LA FAMILLE DE PIEGES DE COFFRE. Le coffre nomme son piege par l'octet
            // haut de son +0x34, plus 0xc0 replie sur huit bits : les valeurs 0xf4..0xfd tombent
            // sur les entrees 0xb4..0xbd de la table statique 0x00551c30, ci-dessous. Chacune est
            // une naissance qui lache un enfant et s'efface ; l'enfant tient la duree et appelle
            // l'application d'effet.
            case 0x00475A30: EntityHandler_00475A30(record); return;   // 0xb7 -> 0x40
            case 0x00475B90: EntityHandler_00475B90(record); return;   // 0xb8 -> 0x5f
            case 0x00475760: EntityHandler_00475760(record); return;   // 0xb9 -> 0x3e
            case 0x00476730: EntityHandler_00476730(record); return;   // 0xbb, la secousse d'ecran
            case 0x00475170: EntityHandler_00475170(record); return;   // 0xbc -> deux 0xbc
            case 0x004753D0: EntityHandler_004753D0(record); return;   // 0xbd -> 0x29

            // Les enfants, et les enfants des enfants.
            case 0x004757A0: EntityHandler_004757A0(record); return;   // 0x3e -> effet 8
            case 0x00475A80: EntityHandler_00475A80(record); return;   // 0x40 -> effet 9
            case 0x00475BE0: EntityHandler_00475BE0(record); return;   // 0x5f -> effet 10
            case 0x004751E0: EntityHandler_004751E0(record); return;   // 0xbc de 0x00551f30
            case 0x004752F0: EntityHandler_004752F0(record); return;   // 0xbc de 0x00552230
            case 0x0047A880: EntityHandler_0047A880(record); return;   // 0x2d, les six eclats
            case 0x004768A0: EntityHandler_004768A0(record); return;   // 0xbb, l'anneau de l'effet 9

            // LA MONTEE DE NIVEAU. Elle lache un 0x59 de la table 0x00551c30, qui est
            // RestoreVitalsEntity_00403710 ; celui-ci seme les deux corps ci-dessous — le grain
            // 0x3b dans 0x005f2d40, et un SECOND 0x59, celui de la table 0x00551f30.
            case 0x004038F0: EntityHandler_004038F0(record); return;
            case 0x00403A90: EntityHandler_00403A90(record); return;

            // Les deux entites que la resolution du coup fait naitre : 0x29 la MARQUE (elle enfle
            // puis palit au-dessus de la victime) et 0x3a, qui seme du 0x99 une image sur deux.
            case 0x00475400: EntityHandler_00475400(record); return;
            case 0x00445950: EntityHandler_00445950(record); return;

            // La branche 0xbe : le pere (entree 190 de 0x00551c30) n'est qu'une naissance, il
            // lache un 0xbe dans l'AUTRE table plus deux 0x68. Meme numero, deux corps.
            case 0x00475EA0: EntityHandler_00475EA0(record); return;
            case 0x00475FC0: EntityHandler_00475FC0(record); return;
            case 0x0047BD00: EntityHandler_0047BD00(record); return;

            // L'entree 90 de la table 0x00551f30 — l'ETINCELLE que le precedent lache par six.
            // Elle decrit une spirale autour de son point de naissance et meurt de retrecir.
            case 0x004034A0: EntityHandler_004034A0(record); return;

            // Le GRAIN DE POUSSIERE, sœur de l'etincelle : meme spirale, mais elle meurt d'etre
            // eteinte au lieu d'avoir retreci. Sous-type 0x38 de la table 0x00552230.
            case 0x00403250: EntityHandler_00403250(record); return;

            // L'ECLAT D'IMPACT — le sous-type 0x39 que les neuf routines d'intelligence font naitre
            // a chaque coup porte —, et les deux entites qu'il seme : l'anneau (0xa4) et les huit
            // etincelles de sa couronne (0x98).
            case 0x00447280: EntityHandler_00447280(record); return;
            case 0x00447380: EntityHandler_00447380(record); return;
            case 0x004474C0: EntityHandler_004474C0(record); return;

            // LA BOUFFEE DE L'OBJET UTILISE — ce que le script d'une potion fait naitre, et
            // l'etincelle (0xa9) qu'elle seme par paires.
            case 0x00446790: EntityHandler_00446790(record); return;
            case 0x0044FB10: EntityHandler_0044FB10(record); return;

            // LES DIX AUTRES GESTIONNAIRES D'OBJET DECOMPILES, du plus employe au moins employe
            // (voir ObjetsAtteignables.md), et les trois PETITS-ENFANTS qu'ils designent.
            case 0x00446DD0: EntityHandler_00446DD0(record); return;   // 0x3e, 189 emplois
            case 0x00446E20: EntityHandler_00446E20(record); return;   //   sa fille 0xa6
            case 0x00445F60: EntityHandler_00445F60(record); return;   // 0x44, 166 emplois
            case 0x00446060: EntityHandler_00446060(record); return;   //   sa fille 0x9c
            case 0x004462B0: EntityHandler_004462B0(record); return;   // 0x43, 162 emplois
            case 0x00446900: EntityHandler_00446900(record); return;   // 0x41, 110 emplois
            case 0x00446200: EntityHandler_00446200(record); return;   // 0x45,  36 emplois
            case 0x00476350: EntityHandler_00476350(record); return;   // 0x5a,  17 emplois
            case 0x00476410: EntityHandler_00476410(record); return;   //   sa fille 0x6e
            case 0x00445E70: EntityHandler_00445E70(record); return;   // 0x48,  17 emplois
            case 0x00445CF0: EntityHandler_00445CF0(record); return;   // 0x4c,  15 emplois
            case 0x00446170: EntityHandler_00446170(record); return;   // 0x4d,  10 emplois
            case 0x00475CC0: EntityHandler_00475CC0(record); return;   // 0x4e,   8 emplois

            // LES GESTIONNAIRES D'OBJET ABSENTS DE Blaze.c, sortis du desassembleur.
            case 0x00446F30: EntityHandler_00446F30(record); return;   // 0x3d,  95 emplois
            case 0x00447050: EntityHandler_00447050(record); return;   //   sa fille 0x9a
            case 0x00476500: EntityHandler_00476500(record); return;   // 0x5b,  75 emplois
            case 0x00446410: EntityHandler_00446410(record); return;   // 0x42,  25 emplois
            case 0x00446620: EntityHandler_00446620(record); return;   //   sa fille 0xa7
            case 0x00447170: EntityHandler_00447170(record); return;   // 0x3c,   1 emploi
            case 0x00447630: EntityHandler_00447630(record); return;   // 0x3b,  14 emplois
            case 0x00404920: EntityHandler_00404920(record); return;   // 0x55,  10 emplois
            case 0x00404A50: EntityHandler_00404A50(record); return;   //   sa braise 0x37
            case 0x00446A90: EntityHandler_00446A90(record); return;   // 0x3f,   4 emplois
            case 0x00446C90: EntityHandler_00446C90(record); return;   //   sa fille 0x9b

            // LE TIR — les trois corps du jet du Goblin : le lanceur (0x49), la trajectoire (0x52)
            // et l'eclaboussure (0xbd). Premier sous-systeme d'attaque a distance transcrit.
            case 0x0047A100: EntityHandler_0047A100(record); return;
            case 0x0047A270: EntityHandler_0047A270(record); return;
            case 0x00476320: EntityHandler_00476320(record); return;

            // L'entree 0x9d de la table statique 0x00552230, atteinte par l'effet de dispersion
            // du groupe.
            case 0x00445860: ShowEntityLabel_00445860(record); return;

            // L'entree 0xe1 de la table installee par zone — le second balayeur d'entites de
            // scene. C'est le plantage de la salle du barman, signale le 2026-08-25.
            case 0x004055C0: EntityHandler_004055C0((uint*)record); return;

            // L'entree 0 de la meme table installee par zone — le troisieme balayeur d'entites
            // de scene. C'est le plantage de la salle DERRIERE le barman, signale le 2026-08-25.
            case 0x00405A70: EntityHandler_00405A70((uint*)record); return;

            // L'entree 1 de la meme table — le changement de musique differe de la femme a la
            // petite harpe. Signale le 2026-08-25.
            case 0x00404E50: EntityHandler_00404E50((uint*)record); return;

            // Le sous-type 0xfe de la meme table (0x0056adc8 + 0xfe * 4 deborde sur 0x0056b0c8)
            // et son etincelle 0xff, qui vit dans l'AUTRE debordement (0x0056afc8 -> 0x0056b2c8).
            // Trente images, trois etincelles par image, et un arpege de trois notes.
            case 0x00404B90: EntityHandler_00404B90(record); return;
            case 0x00404C80: EntityHandler_00404C80(record); return;

            // Les trois derniers balayeurs d'entites de scene, transcrits d'un bloc le 2026-08-25 :
            // le binaire range les six cote a cote, entrees 32 a 37 de 0x0056b0c8, soit les
            // sous-types 0xe0 a 0xe5. Ce sont les trois derniers de ce petit bloc.
            case 0x00405D60: EntityHandler_00405D60((uint*)record); return;
            case 0x00405E70: EntityHandler_00405E70((uint*)record); return;
            case 0x00405FE0: EntityHandler_00405FE0((uint*)record); return;

            // La famille commune de la table 0x0056adc8, transcrite le 2026-08-24. 0x004734B0 est
            // le gestionnaire par defaut : 47 des 64 sous-types y pointent.
            case 0x004734B0: EntityHandler_004734B0((uint*)record); return;
            case 0x004738A0: EntityHandler_004738A0((uint*)record); return;
            case 0x00473A90: EntityHandler_00473A90((uint*)record); return;
            case 0x00473CB0: EntityHandler_00473CB0((uint*)record); return;
            case 0x00450C60: EntityHandler_00450C60((uint*)record); return;
            case 0x00450760: EntityHandler_00450760((uint*)record); return;
            case 0x00473ED0: EntityHandler_00473ED0((uint*)record); return;

            // Corps absents de Blaze.c, transcrits depuis le binaire le 2026-08-24.
            case 0x004736D0: EntityHandler_004736D0((uint*)record); return;
            case 0x00473A70: EntityHandler_00473A70((uint*)record); return;
            case 0x00450BD0: EntityHandler_00450BD0((uint*)record); return;

            // Les trois copies de 0x00450BD0 dans la vieille tour, une par bloc de sous-zones :
            // desassemblage STRICT identique, sans aucune donnee propre.
            case 0x0045B6C0:
            case 0x0045CE50:
            case 0x0045F290:
                EntityHandler_00450BD0((uint*)record); return;

            // ====================================================================================
            // LE DECOR DE LA VIEILLE TOUR — table 0x0056adc8, Handlers/OldTower.cs. Les jumeaux des
            // autres blocs (desassemblage STRICT identique) sont groupes sur leur representant.
            // ====================================================================================
            case 0x0045C7C0: EntityHandler_0045C7C0(record); return;   // 11, l'objet qui racle

            case 0x0045CEE0:                                           // 23, le decor qui respire
            case 0x0045B750:
            case 0x0045F320:
                EntityHandler_0045CEE0(record); return;

            case 0x0045D1B0: EntityHandler_0045D1B0(record); return;   // 31 a 38, ceux qui tournent
            case 0x0045D240: EntityHandler_0045D240(record); return;
            case 0x0045D2B0: EntityHandler_0045D2B0(record); return;
            case 0x0045D400: EntityHandler_0045D400(record); return;
            case 0x0045D4C0: EntityHandler_0045D4C0(record); return;
            case 0x0045D550: EntityHandler_0045D550(record); return;

            case 0x0045D340: EntityHandler_0045D340(record); return;   // 34, un tour puis dort
            case 0x0045D3A0: EntityHandler_0045D3A0(record); return;   // 35, la plate-forme
            case 0x0045D5E0: EntityHandler_0045D5E0(record); return;   // 39, tombe et revient
            case 0x0045D6F0: EntityHandler_0045D6F0(record); return;   // 40, le balancier

            case 0x0045DB10:                                           // 42, la chute qui tremble
            case 0x0045C120:
            case 0x0045F5F0:
                EntityHandler_0045DB10(record); return;

            case 0x0045DBE0:                                           // 43, la bascule
            case 0x0045F6C0:
                EntityHandler_0045DBE0(record); return;

            case 0x0045C6E0:                                           // 53, « un bruit, au loin »
            case 0x0045EB30:
                EntityHandler_0045C6E0(record); return;

            case 0x0045C660:                                           // 55, la source d'effets
            case 0x0045EAB0:
                EntityHandler_0045C660(record); return;

            // Sous-zones 0, 1, 2, 4 et 3, 7, 9.
            case 0x0045EC10: EntityHandler_0045EC10(record); return;   // 11, masque 0x882
            case 0x0045B290: EntityHandler_0045B290(record); return;   // 16, le balancier qui cogne
            case 0x0045B9E0: EntityHandler_0045B9E0(record); return;   // 25, monte puis derive
            case 0x0045BE00: EntityHandler_0045BE00(record); return;   // 27, s'etire sur un bit
            case 0x0045C090: EntityHandler_0045C090(record); return;   // 30, cercle vertical
            case 0x0045AA20: EntityHandler_0045AA20(record); return;   // 44, son a portee
            case 0x0045FA80: EntityHandler_0045FA80(record); return;   // 45, la dalle a poids
            case 0x0045A9B0: EntityHandler_0045A9B0(record); return;   // 50, son global
            case 0x0045ABC0: EntityHandler_0045ABC0(record); return;   // 53, la boite sur un membre
            case 0x0045A3B0: EntityHandler_0045A3B0(record); return;   // 59, les quatre verrous
            case 0x0045E4B0: EntityHandler_0045E4B0(record); return;   // 60, recopie le partenaire
            case 0x0045A310: EntityHandler_0045A310(record); return;   // 61 et 62, son au hasard
            case 0x004746C0: EntityHandler_004746C0((uint*)record); return;
            case 0x004508B0: EntityHandler_004508B0((uint*)record); return;
            case 0x004502E0: EntityHandler_004502E0((uint*)record); return;

            // Le premier gestionnaire PROPRE A UNE ZONE : sous-type 32 en foret, la ou la zone 0
            // renvoie au defaut. Releve le 2026-08-26.
            case 0x00456730: EntityHandler_00456730((uint*)record); return;

            // Le sous-type 29 de la carte suivante — la premiere entite reconstruite qui emprunte
            // MoveSceneEntity_00455B30. Signale le 2026-08-30.
            case 0x00456A30: EntityHandler_00456A30(record); return;
            case 0x00458170: EntityHandler_00458170((uint*)record); return;
            case 0x004566C0: EntityHandler_004566C0((uint*)record); return;
            case 0x004571D0: EntityHandler_004571D0((uint*)record); return;
            case 0x00457240: EntityHandler_00457240((uint*)record); return;
            case 0x00457140: EntityHandler_00457140((uint*)record); return;
            case 0x00456E80: EntityHandler_00456E80((uint*)record); return;
            case 0x00457EC0: EntityHandler_00457EC0((uint*)record); return;
            case 0x00457FC0: EntityHandler_00457FC0((uint*)record); return;
            case 0x00456F40: EntityHandler_00456F40((uint*)record); return;
            case 0x004746F0: EntityHandler_004746F0((uint*)record); return;
            case 0x00474380: EntityHandler_00474380((uint*)record); return;
            case 0x00457BF0: EntityHandler_00457BF0((uint*)record); return;
            case 0x00456750: EntityHandler_00456750((uint*)record); return;
            case 0x00457370: EntityHandler_00457370((uint*)record); return;
            case 0x00456CF0: EntityHandler_00456CF0((uint*)record); return;
            case 0x00541440: EntityHandler_00541440((uint*)record); return;
            case 0x005413E0: EntityHandler_005413E0((uint*)record); return;   // les dix autres zones
            case 0x00541B80: EntityHandler_00541B80((uint*)record); return;

            // L'entree 0xbd de la table statique 0x00551f30 — l'aureole d'incantation, le seul
            // creneau de CETTE table qu'un chemin reconstruit atteigne. C'est le plantage a
            // l'appui sur Select en foret, signale le 2026-08-28.
            case 0x004765D0: TickCastAura_004765D0(record); return;

            // BALLE DE FEU, ecole 0 indice 0 — le premier sort transcrit. La graine est le
            // sous-type 0 de la table statique 0x00551c30 (celle des effets, ou CastSpell_0053A980
            // depose), le projectile le sous-type 0x64 de 0x00551f30 (celle des incantations).
            case 0x0047ADD0: SpawnFireball_0047ADD0(record); return;
            case 0x0047AE80: TickFireball_0047AE80(record); return;
            case 0x0047B050: TickSpark_0047B050(record); return;

            // BOULE DE LUMIERE, ecole 0 indice 1 — quatre corps : la graine, la boule, son
            // etincelle et son halo.
            case 0x0047B0B0: SpawnLightBall_0047B0B0(record); return;
            case 0x0047B150: TickLightBall_0047B150(record); return;
            case 0x0047B340: TickLightSparkle_0047B340(record); return;
            case 0x0047B3A0: TickLightHalo_0047B3A0(record); return;

            // PIEDS MOUILLES, ecole 0 indice 2 — trois corps, et pas de graine : le sous-type 2
            // est deja la vague. Elle seme la crete et l'eclaboussure a chaque image.
            case 0x0047B460: TickWetFeet_0047B460(record); return;
            case 0x0047B6B0: TickWetFeetWave_0047B6B0(record); return;
            case 0x0047B750: TickWetFeetSplash_0047B750(record); return;

            // ROCHER, ecole 0 indice 3 — trois corps, et le premier compte de references du
            // depot : le cercle attend ses quatre rochers avant de rendre la main au lanceur.
            case 0x0047B7A0: TickRockCircle_0047B7A0(record); return;
            case 0x0047B850: TickRock_0047B850(record); return;
            case 0x0047BB10: TickRockDust_0047BB10(record); return;

            // FEU D'ENFER, ecole 0 indice 4 — le premier sort qui VISE : il choisit une cible et
            // tous ses enfants la suivent. La braise 0x68 (0x0047BD00) etait deja ecrite.
            case 0x0047BB50: TickHellfire_0047BB50(record); return;
            case 0x0047BC40: TickHellfireFlame_0047BC40(record); return;
            case 0x0047BDF0: TickHellfireBlaze_0047BDF0(record); return;

            // WONDER, ecole 0 indice 5 — le premier sort ENTRETENU : il tient tant que le joueur
            // garde le bouton, a 2 PM toutes les vingt images, et fait signe a ses deux enfants
            // par le bit 0x2000000 qu'ils heritent de lui.
            case 0x0047BF00: TickWonder_0047BF00(record); return;
            case 0x0047C1E0: TickWonderAura_0047C1E0(record); return;
            case 0x0047C2C0: TickWonderRing_0047C2C0(record); return;

            // TENEBRES, ecole 0 indice 6 — cinq ombres en eventail. L'ombre est le premier corps
            // du depot qui ANNULE son deplacement pour rebondir.
            case 0x0047C3A0: TickDarkness_0047C3A0(record); return;
            case 0x0047C450: TickDarknessShade_0047C450(record); return;

            // ONDE DE CHOC, ecole 0 indice 7 — la premiere poussee RADIALE : chaque victime part
            // sur son propre angle, calcule par le ratan2 de libgte.
            case 0x0047C660: TickShockwave_0047C660(record); return;
            case 0x0047C830: TickShockwaveRing_0047C830(record); return;

            // FLECHES MAGIQUES, ecole 0 indice 8 — le premier projectile a tete chercheuse, et le
            // plus gros corps de sort du depot (1007 octets).
            case 0x0047C910: TickMagicArrow_0047C910(record); return;
            case 0x0047CA50: TickMagicArrowShaft_0047CA50(record); return;
            case 0x00475F60: TickTrailSpark_00475F60(record); return;

            // ENCHANT, ecole 0 indice 9 — le premier sort qui NE FRAPPE PAS : il pose son effet
            // d'un seul coup a la soixantieme image, et cet effet-la ne rate jamais.
            case 0x0047CE40: TickEnchant_0047CE40(record); return;
            case 0x0047CF30: TickEnchantMote_0047CF30(record); return;

            // MUR DE FLAMMES, ecole 0 indice 10 — Pieds mouilles refait en feu : meme chute, mais
            // le decor vire a l'orange et le contact acheve le sort au lieu de l'eteindre.
            case 0x0047CFF0: TickFlameWall_0047CFF0(record); return;
            case 0x0047D270: TickFlameWallEmber_0047D270(record); return;
            case 0x0047D2A0: TickFlameWallColumn_0047D2A0(record); return;

            // COUP DE JUS, ecole 0 indice 11 — la foudre est une polyligne : l'arc pose trois
            // segments bout a bout en une seule image, et chacun frappe sur sa propre longueur.
            case 0x0047D330: TickLightningArc_0047D330(record); return;
            case 0x0047D470: TickLightningBolt_0047D470(record); return;

            // CONGELO, ecole 0 indice 12 — quatre corps, et le premier a alimenter la file des
            // sons differes, restee vide jusqu'ici.
            case 0x0047D600: TickFreeze_0047D600(record); return;
            case 0x0047D670: TickIcePillar_0047D670(record); return;
            case 0x0047D950: TickIceBlock_0047D950(record); return;
            case 0x0047DA50: TickIceShard_0047DA50(record); return;

            // ATCHOUM, ecole 0 indice 13 — le second sort entretenu, et le seul ou l'AGE d'un
            // enfant lui sert de laissez-passer pour frapper.
            case 0x0047DB50: TickSneeze_0047DB50(record); return;
            case 0x0047DE10: TickSneezePuff_0047DE10(record); return;

            // ASSAUT MAGIQUE, ecole 0 indice 14 — le seul sort ou le signal de fin REMONTE : ce
            // sont les lames qui marquent leur pere, pas l'inverse.
            case 0x0047DF70: TickMagicAssault_0047DF70(record); return;
            case 0x0047E150: TickAssaultBlade_0047E150(record); return;
            case 0x0047E200: TickAssaultRing_0047E200(record); return;

            // LUMIERE, ecole 0 indice 15 — troisieme sort entretenu (6 PM par vingt images), et
            // son globe tire une FLECHE MAGIQUE par image : le projectile du sort 8, deja ecrit.
            case 0x0047E260: TickLight_0047E260(record); return;
            case 0x0047E370: TickLightGlobe_0047E370(record); return;

            // ECLAIR, ecole 0 indice 16 — quatrieme sort entretenu (1 PM par DIX images), et le
            // seul dont le compte de references se fait sur les naissances REUSSIES.
            case 0x0047E590: TickThunder_0047E590(record); return;
            case 0x0047E6F0: TickThunderStrand_0047E6F0(record); return;
            case 0x0047EA80: TickThunderSpark_0047EA80(record); return;

            // TROUILLE, ecole 0 indice 17 — cinquieme sort entretenu, et le seul qui ne puisse
            // meme pas MOURIR avant que son fantome ne lui ait pose le bit 0x02.
            case 0x0047EAE0: TickFear_0047EAE0(record); return;
            case 0x0047EC30: TickFearGhost_0047EC30(record); return;
            case 0x0047EF60: TickFearWisp_0047EF60(record); return;

            // EXORCISTE, ecole 0 indice 18 — il ne frappe pas, il NETTOIE : ClearAllStatusEffects
            // a chaque image, sur ce qu'il balaie, ou sur le lanceur faute de cible.
            case 0x0047F070: TickExorcism_0047F070(record); return;
            case 0x0047F290: TickExorcismHalo_0047F290(record); return;

            // DURDUR, ecole 0 indice 19 — sa coquille lui dit quand partir, et il reemploie
            // l'anneau d'Onde de choc par sa branche SANS ancre, jamais empruntee jusqu'ici.
            case 0x0047F2B0: TickHardening_0047F2B0(record); return;
            case 0x0047F400: TickHardeningShell_0047F400(record); return;
            case 0x0047F470: TickHardeningBurst_0047F470(record); return;

            // BOULE DE FEU, ecole 0 indice 20 — le plus gros sort du depot (1425 octets), le seul
            // qui CHOISISSE entre deux sondes, et le seul qui secoue l'ecran.
            case 0x0047F570: TickFireOrb_0047F570(record); return;
            case 0x0047FB10: TickFireOrbShell_0047FB10(record); return;
            case 0x00482EF0: TickFireOrbTrail_00482EF0(record); return;

            // VENTILO, ecole 0 indice 21 — Coup de jus trois fois : trois polylignes de trois
            // segments, toutes parties du meme point. Le segment 0x75 est celui du sort 11.
            case 0x0047FB80: TickGale_0047FB80(record); return;
            case 0x0047FE40: TickGaleGust_0047FE40(record); return;

            // DEMON POLAIRE, ecole 0 indice 22 — trois demons en deux moities, qui se retrouvent
            // par leur NUMERO D'EMPLACEMENT et non par un pointeur.
            case 0x0047FE60: TickPolarDemon_0047FE60(record); return;
            case 0x00480080: TickPolarDemonHead_00480080(record); return;
            case 0x00480490: TickPolarDemonTail_00480490(record); return;

            // STALAGTITE, ecole 0 indice 23 — un seul corps pour deux vies : la pointe qui rampe,
            // puis l'eboulement. Le bit 0x2000000 les separe au lieu de l'eteindre.
            case 0x00480600: TickStalactite_00480600(record); return;
            case 0x00480A20: TickStalactiteSpike_00480A20(record); return;
            case 0x00480A70: TickStalactiteShard_00480A70(record); return;

            // SATANAS, ecole 0 indice 24 — le seul sort qui TUE : son demon appelle
            // SlayTarget_0053CDF0, l'entree 47 de la table des 48, et non ResolveSpellHit.
            case 0x00480B90: TickSatanas_00480B90(record); return;
            case 0x00480E40: TickSatanasSigil_00480E40(record); return;
            case 0x00481250: TickSatanasReaper_00481250(record); return;

            // AU SEC, ecole 0 indice 25 — le seul sort qui CHANGE D'ECRAN : sa spirale pose
            // s_GameStateId_005de510 = 2 et arrete la boucle de la carte du monde.
            case 0x00481670: TickRecall_00481670(record); return;
            case 0x00481720: TickRecallSpiral_00481720(record); return;

            // COLERE, ecole 0 indice 26 — cinq corps, un serpent de huit anneaux chaine par
            // NUMERO D'EMPLACEMENT, et le seul appel du depot avec une variante non nulle.
            case 0x004819D0: TickWrath_004819D0(record); return;
            case 0x00481D50: TickWrathGlow_00481D50(record); return;
            case 0x00481E30: TickWrathHead_00481E30(record); return;
            case 0x004821A0: TickWrathLink_004821A0(record); return;
            case 0x00482300: TickWrathPillar_00482300(record); return;

            // METEORES, ecole 0 indice 27 — le seul sort qui n'immobilise pas son lanceur, et le
            // seul qui sonde le decor avant d'engendrer.
            case 0x00482370: TickMeteorStorm_00482370(record); return;
            case 0x004824F0: TickMeteor_004824F0(record); return;
            case 0x004828B0: TickMeteorTrail_004828B0(record); return;

            // FUSION, ecole 0 indice 28 — le DERNIER de l'ecole. Son +0x16 est a la fois l'avance
            // de la boule et le decompte avant la detonation : c'est son arret qui l'allume.
            case 0x004828F0: TickFusion_004828F0(record); return;
            case 0x00482DE0: TickFusionOrb_00482DE0(record); return;
            case 0x00482ED0: TickFusionSpark_00482ED0(record); return;

            // PARDON, ecole 1 indice 0 — la premiere fiche du PRETRE. Il ne frappe pas : il
            // designe une victime sur un bit de sa fiche d'espece, et son trait 0x9e fait le reste.
            case 0x0044F060: TickForgive_0044F060(record); return;
            case 0x0044F290: TickForgiveBolt_0044F290(record); return;

            // SANG PUR, ecole 1 indice 1 — le soin de base du pretre, et le premier sort du depot
            // qui n'apporte aucun corps d'enfant : ses deux sous-types etaient deja ecrits.
            case 0x00447DF0: TickPureBlood_00447DF0(record); return;

            // RENFORT, ecole 1 indice 2 — la premiere benediction. Ses deux enfants sont ceux de
            // Gnoukoule aussi : l'etincelle 0xa7 et l'eclat 0xa0 servent aux deux fiches.
            case 0x00448060: TickReinforce_00448060(record); return;
            case 0x0044FE00: TickHolySpark_0044FE00(record); return;

            // Les deux dernieres du bloc de cinq enfants de benediction (0xa5 a 0xa9) : l'anneau
            // qui ondule autour de sa cible (0xa8) et la bulle qui monte en s'eclaircissant (0xa6).
            case 0x0044FC90: EntityHandler_0044FC90(record); return;
            case 0x0044FF40: EntityHandler_0044FF40(record); return;

            // LES DEUX RELAIS, entrees 98 (0x62) et 99 (0x63) de la table 0x00551c30 : elles
            // lachent un enfant dont le SOUS-TYPE EST LU DANS +0x0b, et s'effacent. Un seul
            // creneau ouvre donc sur n'importe quel gestionnaire de l'autre table.
            case 0x00476950: EntityHandler_00476950(record); return;
            case 0x00476970: EntityHandler_00476970(record); return;

            // L'entree 42 (0x2a) de la table 0x00552230 — la bulle d'etat au-dessus d'une tete,
            // deux images, qui pulse au rythme du compteur de fondu global.
            case 0x0047A950: EntityHandler_0047A950(record); return;

            // L'entree 0 de la meme table — LE SOUS-TYPE ZERO, un eclat qui perd un quart de sa
            // taille par image et s'efface a la onzieme.
            case 0x00482FA0: EntityHandler_00482FA0(record); return;
            case 0x00448250: TickHolyFlash_00448250(record); return;

            // GNOUKOULE, ecole 1 indice 3 — 418 octets recopies de Renfort a quatre valeurs pres.
            // Aucun enfant neuf : il partage l'etincelle 0xa7 et l'eclat 0xa0 avec lui.
            case 0x00448300: TickGnoukoule_00448300(record); return;

            // SONDE, ecole 1 indice 4 — 2 500 octets pour deux PM. Huit traits en coordonnees
            // spheriques, un embrasement qui tient la cible, et une etincelle par trait tous les
            // quatre images.
            case 0x004484F0: TickProbe_004484F0(record); return;
            case 0x004487F0: TickProbeRing_004487F0(record); return;
            case 0x00448C40: TickProbeBurst_00448C40(record); return;
            case 0x00448E20: TickProbeMote_00448E20(record); return;

            // BOBOS FINIS, ecole 1 indice 5 — Sang pur sur les quatre emplacements du groupe, avec
            // le soin PARTAGE au lieu du soin simple. Aucun corps d'enfant neuf.
            case 0x004491F0: TickHealAll_004491F0(record); return;

            // 3EME OEIL, ecole 1 indice 6 — Sonde a cinq valeurs pres, au meme prix de deux PM.
            // Il reveille la branche 0x0d du semis d'etincelle du trait, morte jusqu'ici.
            case 0x00448EF0: TickThirdEye_00448EF0(record); return;

            // ANTIDOTE, ecole 1 indice 7 — le premier sort de l'ecole qui laisse le decor
            // tranquille. Son 0xa9 est celui de la table 0x00551f30, pas celui de la 0x00552230.
            case 0x00449590: TickAntidote_00449590(record); return;
            case 0x00449760: TickHolyBubble_00449760(record); return;
            case 0x004501B0: TickHolyHalo_004501B0(record); return;

            // LOURDES, ecole 1 indice 8 — Antidote a quatre valeurs pres. Son enfant porte le meme
            // sous-type 0x8c que la bulle d'Antidote, mais dans l'autre table : c'est un grain.
            case 0x004498B0: TickLourdes_004498B0(record); return;
            case 0x00449A70: TickHolyMote_00449A70(record); return;

            // ANTISORT, ecole 1 indice 9 — la troisieme du meme moule, et la derniere. Ses vingt
            // spirales sortent au MILIEU du sort, pas au debut.
            case 0x00449C30: TickAntispell_00449C30(record); return;
            case 0x00449E30: TickHolySpiral_00449E30(record); return;

            // MARTEAU, ecole 1 indice 10 — le corps de sort le plus court de l'ecole. Tout est dans
            // sa masse, qui ECRASE la cible et fait clignoter l'ecran.
            case 0x00449F90: TickHammer_00449F90(record); return;
            case 0x0044A080: TickHammerHead_0044A080(record); return;
            case 0x0044A320: TickHammerDebris_0044A320(record); return;

            // SANG D'ANGE, ecole 1 indice 11 — malgre son nom il ne ressuscite pas : il pose la
            // guerison COMPLETE, entree 45. Son 0x8d est celui de la table 0x00551f30.
            case 0x0044A4B0: TickAngelBlood_0044A4B0(record); return;
            case 0x0044A6A0: TickAngelWing_0044A6A0(record); return;

            // LAPECHE, ecole 1 indice 12 — le premier sort qui DURE TANT QU'ON LE PAIE, et le
            // premier sans enveloppe d'effet : son dome travaille sur les quatre tables d'entites.
            case 0x0044A7A0: TickLapeche_0044A7A0(record); return;
            case 0x0044A880: TickHolyDome_0044A880(record); return;
            case 0x004500B0: TickHolyCatchSpark_004500B0(record); return;

            // PRIERE, ecole 1 indice 13 — la seconde moitie de Lapeche : sa nasse attrape
            // exactement ce que l'autre laisse passer (le bit 0x800 exige au lieu d'interdit).
            case 0x0044AE90: TickPrayer_0044AE90(record); return;
            case 0x0044AF70: TickHolyWard_0044AF70(record); return;

            // CALGON, ecole 1 indice 14 — le premier corps de sort de l'ecole qui ne recopie pas la
            // position de sa cible, et le seul dont la teinte du decor MONTE au fil des images.
            case 0x0044B510: TickCalgon_0044B510(record); return;
            case 0x0044B6F0: TickCalgonDroplet_0044B6F0(record); return;
            case 0x0044C800: TickCalgonRing_0044C800(record); return;

            // ZOMBIE, ecole 1 indice 15 — Pardon sur TOUT LE MONDE : meme critere d'espece, meme
            // trait 0x9e, mais toutes les candidates au lieu de la plus proche. Porte aussi LB-019.
            case 0x0044F930: TickZombie_0044F930(record); return;

            // PAROLE SAINTE, ecole 1 indice 16 — le premier sort de l'ecole qui vise un CONE et
            // non une cible, et le seul qui verifie la vue : il marche jusqu'a chaque candidate en
            // quatre pas et renonce des que le decor le touche.
            case 0x0044D370: TickHolyWord_0044D370(record); return;
            case 0x0044D8E0: TickHolyWordFlare_0044D8E0(record); return;
            case 0x0044DB30: TickHolyWordShard_0044DB30(record); return;
            case 0x0044D9B0: TickHolyWordMote_0044D9B0(record); return;

            // DELIVRANCE, ecole 1 indice 17 — Sang pur a l'envers : trente images de decor, puis
            // la levee de l'effet d'etat 9 a la derniere.
            case 0x0044B7B0: TickDeliverance_0044B7B0(record); return;
            case 0x0044B950: TickDeliveranceHalo_0044B950(record); return;
            case 0x0044BA00: TickDeliverancePetal_0044BA00(record); return;

            // MICRO, ecole 1 indice 18 — le jumeau exact de Delivrance : meme cout, meme palier,
            // meme portee, meme forme ; il leve l'etat 10 la ou l'autre leve le 9.
            case 0x0044BAF0: TickMicro_0044BAF0(record); return;
            case 0x0044BCC0: TickMicroMote_0044BCC0(record); return;
            case 0x0044BE40: TickMicroFlash_0044BE40(record); return;

            // COUP DE POUCE, ecole 1 indice 19 — le premier sort de l'ecole qui prend TOUT LE
            // GROUPE : il balaie 0x005e93c0 et pose l'effet 33 sur chaque membre vivant.
            case 0x0044BF00: TickHelpingHand_0044BF00(record); return;
            case 0x0044C110: TickBlessingSpark_0044C110(record); return;
            case 0x0044C260: TickHelpingHandSpiral_0044C260(record); return;

            // REMONTANT, ecole 1 indice 20 — la grande levee : neuf effets d'un coup, la ou
            // Delivrance et Micro n'en enlevent qu'un chacun.
            case 0x0044C3A0: TickRestorative_0044C3A0(record); return;
            case 0x0044C730: TickRestorativeBeam_0044C730(record); return;

            // MIRACLE, ecole 1 indice 21 — il leve le seul effet qui COMPTE au lieu de poser un
            // bit : la duree +0xc0 et le compteur +0xec, les trois marques d'un coup.
            case 0x0044C8A0: TickMiracle_0044C8A0(record); return;
            case 0x0044CAC0: TickMiraclePillar_0044CAC0(record); return;
            case 0x0044CBD0: TickMiracleAureole_0044CBD0(record); return;

            // RESURECTION, ecole 1 indice 22 — la seule fiche a ne pas recopier le patron
            // d'enveloppe : le garde-fou commun refuse une cible mourante, et elle vise un mort.
            case 0x0044CD00: TickResurrection_0044CD00(record); return;
            case 0x0044CF30: TickResurrectionColumn_0044CF30(record); return;
            case 0x0044D030: TickResurrectionMote_0044D030(record); return;
            case 0x0044D170: TickResurrectionVeil_0044D170(record); return;

            // ANGE, ecole 1 indice 23 — la DERNIERE fiche de l'ecole, et le seul sort du depot qui
            // dure tant qu'on tient le bouton : quatre conditions le soutiennent, deux PM toutes
            // les dix images le paient.
            case 0x0044DC60: TickAngel_0044DC60(record); return;
            case 0x0044DED0: TickAngelForm_0044DED0(record); return;
            case 0x0044EA60: TickAngelFeather_0044EA60(record); return;

            // DODO, ecole 2 indice 0 — la PREMIERE fiche de l'ecole de la Fee, et elle ne
            // ressemble a rien de l'ecole 1 : le sort ne vit qu'une image et tout est dans sa
            // bulle, un projectile a tete chercheuse qui pose l'effet d'etat 11.
            case 0x00476C70: TickSleep_00476C70(record); return;
            case 0x00476CF0: TickSleepBubble_00476CF0(record); return;

            // GROS SABOTS, ecole 2 indice 1 — meme charpente que Dodo : le sort ne vit qu'une
            // image et tout est dans son enfant. Mais celui-ci ne cherche rien — le sort a deja
            // choisi la victime — il TOMBE dessus de trois cents unites de haut et l'aplatit.
            case 0x00476FD0: TickHeavyHoof_00476FD0(record); return;
            case 0x004770A0: TickHeavyHoofStomp_004770A0(record); return;

            // VITESSE, ecole 2 indice 2 — le PREMIER SORT BIENFAISANT de l'ecole : sa cible est
            // un allie, et faute de voisin le lanceur lui-meme. Deux anneaux naissent la meme
            // image ; seul le second, marque +0x16 = 1, pose l'effet 19.
            case 0x00477360: TickHaste_00477360(record); return;
            case 0x00477420: TickHasteRing_00477420(record); return;

            // MATERIA FEU, ecole 2 indice 3 — la premiere d'une fratrie de QUATRE corps
            // identiques (terre, air, eau), qui ne different que par le son de la premiere
            // image, sa hauteur, et l'indice de l'effet a intensite pose a la derniere. Son
            // embrasement 0x4c est celui de Feu d'enfer, deja ecrit.
            case 0x004774C0: TickFireMateria_004774C0(record); return;
            case 0x00477600: TickFireMateriaFlame_00477600(record); return;

            // MATERIA TERRE, ecole 2 indice 4 — la deuxieme de la fratrie. Meme corps que feu a
            // trois constantes pres ; meme embrasement, meme flamme.
            case 0x00477660: TickEarthMateria_00477660(record); return;

            // MATERIA AIR, ecole 2 indice 5 — la troisieme de la fratrie, meme parente
            // verifiee au desassembleur : trois constantes d'ecart, et rien d'autre.
            case 0x004777A0: TickAirMateria_004777A0(record); return;

            // MATERIA EAU, ecole 2 indice 6 — la QUATRIEME ET DERNIERE de la fratrie. Les
            // quatre copies sont relevees : pas de quatrieme difference, le comportement est
            // unique et seules trois constantes changent.
            case 0x004778E0: TickWaterMateria_004778E0(record); return;

            // CHANT SIRENES, ecole 2 indice 7 — le premier corps vraiment neuf de l'ecole
            // depuis Vitesse, et c'est DODO REECRIT : meme charpente que sa bulle, mais
            // refaite et non copiee. Elle respire au lieu de semer, et pose l'effet 8.
            case 0x00477A20: TickSirenSong_00477A20(record); return;
            case 0x00477A90: TickSirenSongNote_00477A90(record); return;

            // ECRASE, ecole 2 indice 8 — le sort est celui de DODO, octet pour octet : deux
            // constantes changent, le sous-type engendre et l'atlas. Sa boule engendre a son
            // tour, et c'est la PREMIERE PETITE-FILLE de l'ecole 2.
            case 0x00477D20: TickCrush_00477D20(record); return;
            case 0x00477E40: TickCrushOrb_00477E40(record); return;
            case 0x00477DA0: TickCrushMark_00477DA0(record); return;

            // CHAMP MAGIQUE, ecole 2 indice 9 — le plus gros corps de l'ecole, et le premier
            // sans enfant a lui : une SENTINELLE qui balaie les trois tables d'entites et
            // eteint ce qui approche. Entretenu comme Ange, quatre PM toutes les vingt images.
            case 0x004780B0: TickMagicField_004780B0(record); return;

            // COUSSIN D'AIR, ecole 2 indice 10 — apres le plus gros corps de l'ecole, le plus
            // petit : 205 octets, un seul saut, aucun enfant a ecrire. Un souffle sous les
            // pieds d'un allie, vingt images, puis l'effet 28.
            case 0x004785F0: TickAirCushion_004785F0(record); return;

            // ESCARGOT, ecole 2 indice 11 — la JUMELLE de Champ magique : meme corps a trois
            // differences pres, dont une seule compte — elle guette une AUTRE classe
            // d'entites, (etat & 0xe80) == 0x680 au lieu de (etat & 0x880) == 0x880.
            case 0x004786C0: TickSnail_004786C0(record); return;

            // RALENTI, ecole 2 indice 12 — GROS SABOTS REPETE : le meme sabot 0x7e, mais
            // toutes les huit images et sans borne, tant qu'il reste quelqu'un en face.
            case 0x00478C20: TickSlow_00478C20(record); return;

            // TURBO, ecole 2 indice 13 — VITESSE DANS UNE BOUCLE : le meme corps, rejoue tant
            // qu'il trouve un allie de plus. Vitesse hate un compagnon, Turbo tout le groupe.
            case 0x00478D10: TickTurbo_00478D10(record); return;

            // SUPERMAN, ecole 2 indice 14 — VITESSE EN PLUS GRAND ET EN PLUS LONG : la meme
            // premiere image a deux anneaux, divisee par 100 au lieu de 150, puis vingt-quatre
            // images qui sement un grain de poussiere chacune.
            case 0x00478DF0: TickSuperman_00478DF0(record); return;
            case 0x00479030: TickSupermanRing_00479030(record); return;
            case 0x00478FC0: TickSupermanMote_00478FC0(record); return;

            // BLINDE, ecole 2 indice 15 — LE SEUL SORT DE L'ECOLE QUI SE LANCE SUR LUI-MEME :
            // le lanceur est passe des deux cotes a l'effet 25, et si celui-ci ne passe pas,
            // le sort n'a pas lieu du tout. Sa duree lui est rendue par l'effet.
            case 0x004790C0: TickArmour_004790C0(record); return;
            case 0x00479250: TickArmourShell_00479250(record); return;

            // LIQUIDE, ecole 2 indice 16 — la premiere des QUATRE ELEMENTAIRES, et elles
            // partagent UN SEUL corps d'enfant : 0x00479490 lit son +0x06 (l'indice du sort,
            // 0x10 a 0x13) et saute dans une table de quatre entrees pour choisir l'element.
            case 0x00479F70: TickLiquid_00479F70(record); return;
            case 0x00479490: TickElementalFamiliar_00479490(record); return;
            case 0x00479B90: TickLiquidColumn_00479B90(record); return;

            // FLAMME, ecole 2 indice 17 — LE JUMEAU EXACT DE LIQUIDE : les deux corps sont les
            // memes 392 octets, a un seul mot pres (le script de sons). L'elementaire ci-dessus
            // se branche seul sur l'element ; le seul corps qui manquait est LA BRAISE, que
            // Terre emploiera aussi.
            case 0x00479300: TickFlame_00479300(record); return;
            case 0x00479BC0: TickFlameEmber_00479BC0(record); return;

            // ZEPHE, ecole 2 indice 18 — le TROISIEME jumeau du meme corps, et le seul des quatre
            // qui n'ait demande aucun corps neuf : sa bourrasque 0x50 est TickFireOrbTrail_00482EF0,
            // la trainee de BOULE DE FEU, deja ecrite pour l'ecole 0.
            case 0x00479DE0: TickZephyr_00479DE0(record); return;

            // TERRE, ecole 2 indice 19 — LA DERNIERE FICHE DE L'ECOLE, et le quatrieme jumeau du
            // meme corps. Sa motte 0x57 est la braise de Flamme, TickFlameEmber_00479BC0. Les
            // quatre elementaires n'auront donc apporte que DEUX corps neufs a eux tous.
            case 0x00479C50: TickEarth_00479C50(record); return;

            // L'HERBORISTERIE, ecole 3 — LES SEPT PLANTES SUR UN SEUL CORPS, qui se branche sur son
            // +0x06 pour choisir non pas un decor mais L'EFFET (ApplyHerbEffect_0056D4C0). Ecole
            // injouable : ces quatre corps ne tournent dans aucune partie.
            case 0x0047A5D0: TickHerbalism_0047A5D0(record); return;
            case 0x0047A7D0: TickHerbGlow_0047A7D0(record); return;
            case 0x0047A730: TickHerbMote_0047A730(record); return;

            // LES DEUX AURES D'ETAT — 0x46 et 0x47, posees sur la VICTIME par les effets 12 et 11,
            // avec leurs deux enfants. `0x00476140` etait range ici sous le nom « TickHerbSeed » :
            // c'est une erreur d'identification, il n'appartient pas a l'herboristerie. Le seul
            // engendreur de son sous-type 0x2e dans tout le binaire est `0x004760C0`.
            case 0x004760C0: TickStatusBurst_004760C0(record); return;
            case 0x00476140: TickStatusMote_00476140(record); return;
            case 0x004761D0: TickSleepAura_004761D0(record); return;
            case 0x00476290: TickSleepBubble_00476290(record); return;

            // LES SORTS DE MONSTRE, familles 4 et 6 — les deux seules fiches des quatre familles
            // hors ecole qui designent un corps a elles. Leurs sous-types 0x4a et 0x4b sont
            // voisins de celui de la famille 5 (0x49), qui etait deja ecrit.
            case 0x004477D0: TickMonsterBoltCast_004477D0(record); return;
            case 0x004478F0: TickMonsterBolt_004478F0(record); return;
            case 0x004769A0: TickMonsterOrbCast_004769A0(record); return;
            case 0x004769F0: TickMonsterOrb_004769F0(record); return;

            // LES TROIS PETITS-ENFANTS DE L'ECOLE 1 — la colonne et le grain de PARDON, l'etincelle
            // d'ANGE. Ni la fiche ni son enfant direct ne les designent : ce sont les enfants des
            // enfants, que le recensement des sorts ne remonte pas. Sans eux, lancer Pardon ou Ange
            // levait une NotImplementedException des la premiere image du trait.
            case 0x0044F600: TickForgivePillar_0044F600(record); return;
            case 0x0044F750: TickForgiveMote_0044F750(record); return;
            case 0x0044EF40: TickAngelSpark_0044EF40(record); return;

            // Le sous-type 2 de la table installee 0x0056ad48 — l'INTELLIGENCE DES MONSTRES.
            // C'est le plantage juste apres l'apparition d'un groupe d'ennemis, signale le
            // 2026-08-28.
            case 0x0049A980: TickSceneEntityAi_0049A980(record); return;
            case 0x0049E190: TickSceneEntityAi_0049E190(record); return;

            // La QUATRIEME intelligence d'entite de scene : celle qui EMPOIGNE. Foret,
            // sous-zones 0 a 4 ; 1932 octets absents de Blaze.c. Elle etait la seule ligne
            // « implemente=non, teste=oui » de la zone 3.
            // Et deux copies dans la mine (sous-zones 0 a 2 et 3, 4, 9) : le meme texte, a trois
            // chargements de champ pres, avances en registre avant le calcul qui les emploie.
            case 0x0048AD80:
            case 0x00492E40:
            case 0x0049E920: TickSceneEntityAi_0049E920(record); return;

            // Le sous-type 0 de la meme table — la sœur du precedent, une bete plus petite et plus
            // prudente. C'est le plantage a l'apparition d'un groupe, signale le 2026-08-28 apres
            // que le sous-type 2 eut ete transcrit.
            case 0x00499DE0: TickSceneEntityAi_00499DE0(record); return;

            // Le sous-type 1 de la meme table — la COCCINELLE, premiere bete volante ecrite.
            // Relevee par le banc le 2026-08-29 : la seule des neuf lacunes du bloc A qui soit une
            // vraie espece de la sous-zone d'arrivee, donc validable sans greffe.
            case 0x0049A360: TickSceneEntityAi_0049A360(record); return;

            // La TROISIEME BETE VOLANTE, celle des sous-zones 5 et 9 : meme vol que la Coccinelle,
            // mais sa posture 3 est une RUEE — une impulsion d'une image, et elle recule en
            // restant face au groupe.
            case 0x004A3C30: TickSceneEntityAi_004A3C30(record); return;

            // Le sous-type 6 — le POISSON VOLANT, qui ne vole pas : il bondit, et il s'accroche.
            // Et sa copie de la mine, sous-zones 5 a 8 : trois chargements de champ non avances en
            // registre, le meme calcul.
            case 0x0048CBB0:
            case 0x0049C8E0: TickSceneEntityAi_0049C8E0(record); return;

            // Le sous-type 5 — le CRABE GEANT, la plus proche parente des deux premieres : meme
            // integration terrestre, meme table de sauts, mais DEUX attaques — la pince, qui balaie
            // quatre images de suite, et la ruee, qui ne porte qu'a moitie.
            // Et sa copie de la mine, sous-zones 5 a 8, identique instruction pour instruction.
            case 0x0048D300:
            case 0x0049C0B0: TickSceneEntityAi_0049C0B0(record); return;

            // Le sous-type 4 — le GRIZZLY, jumeau du precedent : memes quatre postures, mais son
            // bond QUITTE LE SOL et son geste se ferme a l'atterrissage, pas sur un compte a
            // rebours. Ses deux gabarits de portee sont dans .data (0x00552960, 0x0055296c).
            // Et sa copie de la mine, sous-zones 3, 4 et 9 : le gabarit du bond est lu a 0x005528cc,
            // dont les douze octets sont ceux de 0x0055296c.
            // Et sa copie de la mine, sous-zones 6 a 8 : l'OURS GRIS du bestiaire. 660 instructions
            // identiques sur 660, a trois immediates pres — ses deux gabarits, 0x00552898 et
            // 0x005528a4, dont les 24 octets sont ceux de 0x00552960, et sa table de sauts
            // 0x0048e390, dont les quatre entrees sont aux memes decalages que 0x0049c0a0.
            case 0x00493C70:
            case 0x0048DB30:
            case 0x0049B840: TickSceneEntityAi_0049B840(record); return;

            // Le sous-type 4 du bloc C — LE MEME GRIZZLY, emis une seconde fois par le compilateur.
            // 705 lignes d'assembleur identiques sur 705, a deux immediates pres : les adresses des
            // deux gabarits, 0x005529e8 et 0x005529f4, dont les 24 octets sont ceux de 0x00552960.
            case 0x004A4270: TickSceneEntityAi_004A4270(record); return;

            // Le sous-type 13 du bloc C de la mine — LE JEUNE DRAGON, 700 PV. Trois gestes, dont
            // DEUX SOUFFLES qui ne touchent personne : ils font naitre une entite dont le sous-type
            // est lu dans le repertoire de zone, et celui de la posture 2 part de sa gueule (la
            // rangee 0 du tableau d'attaches du modele). Gravite 0x7fff, la plus forte du depot.
            case 0x004944E0: TickSceneEntityAi_004944E0(record); return;

            // Le sous-type 16 du bloc C de la mine — LE TROLL, 850 PV, le plus gros du depot. CINQ
            // gestes (bond, massue, charge, bond empoisonne, souffle), sa charge s'enchaine seule
            // sur un bond, et IL SE REGENERE de 8 points de vie une image sur vingt, sans condition.
            case 0x004965B0: TickSceneEntityAi_004965B0(record); return;

            // Le sous-type 1 de la montagne du dragon de feu — LE DRAGON ROUGE, 8 000 PV. Cinq gestes
            // (deux coups dissemblables, un bond, deux souffles) et l'incantation du Behemoth sur
            // trois sorts : Superman, Boule de feu, Meteores. Ai/FireMountain/RedDragon.cs.
            case 0x00499140: TickSceneEntityAi_00499140(record); return;

            // Le sous-type 5 du bloc C de la mine — LE SCORPION, charpente du Varan : placement
            // annule au heurt, treize postures, +0x12 comme phase. Deux armes — la pince, et le
            // dard qui tire son effet d'etat AU SORT, un tiers l'effet 7 et deux tiers le piege.
            case 0x004921A0: TickSceneEntityAi_004921A0(record); return;

            // Le sous-type 17 du bloc C de la mine — L'ARAIGNEE, le Scorpion en plus simple : une
            // seule arme, douze postures au lieu de treize, et un poison sur quinze morsures.
            case 0x00497370: TickSceneEntityAi_00497370(record); return;

            // Le sous-type 4 du bloc C de la mine — LE MILLE-PATTE, la troisieme variante de la
            // charpente. IL NE MORD QU'EN L'AIR : son geste bondit, et le prelude efface le bit de
            // geste a chaque image passee au sol.
            case 0x00491650: TickSceneEntityAi_00491650(record); return;

            // Le sous-type 2 du bloc A de la mine — LE CHEF-GOBLIN, le corps de l'Ogre a sept
            // ecarts pres : sa charge reste au sol et s'amortit, elle projette dans son cap, son
            // combo enchaine plus tot, et il bondit de pres la ou l'Ogre chargeait.
            case 0x004889A0: TickSceneEntityAi_004889A0(record); return;

            // ====================================================================================
            // LES PARTICULES DE LA MONTAGNE DU DRAGON DE FEU — zone 2, Effects/FireMountainParticles.cs
            // ====================================================================================

            // Le flash bleu, emis une seconde fois par le compilateur : 89 instructions identiques
            // sur 89 a celles de 0x00409E40, immediates comprises.
            case 0x0040CDA0: EntityHandler_0040CDA0(record); return;

            // La trainee de feu qui se recolle sur son porteur et meurt avec lui.
            case 0x0040D560: EntityHandler_0040D560(record); return;

            // La braise, quatre variantes sans rapport dans un seul corps : deux spirales, une
            // etincelle droite, et la gerbe qui pond trente et une filles a la naissance.
            case 0x0040DDE0: EntityHandler_0040DDE0(record); return;

            // Les deux bouffees, meme planche et meme animation : l'une monte sur place, l'autre
            // part dans son cap. Toutes deux brulent le groupe a 0x28 unites.
            case 0x0040E6F0: EntityHandler_0040E6F0(record); return;
            case 0x0040E860: EntityHandler_0040E860(record); return;

            // Le grain de la mine (0x004070F0) lisant une autre planche, 0x0054F6B8.
            case 0x0040EA00: EntityHandler_0040EA00(record); return;

            // L'escarbille, dont la montee s'essouffle d'un cran toutes les deux images.
            case 0x0040EAF0: EntityHandler_0040EAF0(record); return;

            // L'etincelle qui garde une ancre et meurt de s'en etre eloignee de 0x78.
            case 0x0040EBE0: EntityHandler_0040EBE0(record); return;

            // ====================================================================================
            // LE DECOR DE LA MONTAGNE DU DRAGON DE FEU — zone 2, bloc 0x00554d28 de la table
            // 0x0056adc8, Handlers/FireMountain.cs
            // ====================================================================================

            // Les deux sons d'ambiance a vingt images : l'un global, l'autre a portee de camera.
            case 0x004560A0: EntityHandler_004560A0(record); return;
            case 0x00456110: EntityHandler_00456110(record); return;

            // Le decor qui ondule en hauteur et sonne a chaque passage par sa base, et celui qui
            // tourne sur un cercle de 200.
            case 0x00456180: EntityHandler_00456180(record); return;
            case 0x00456270: EntityHandler_00456270(record); return;

            // L'evenement unique : un drapeau de script leve a la naissance, une attente qui fait
            // trembler la camera, une chute, puis l'entite se rendort.
            case 0x00456310: EntityHandler_00456310(record); return;

            // Le jet intermittent : six naissances par salve, une pause tiree au sort.
            case 0x00456590: EntityHandler_00456590(record); return;

            // ====================================================================================
            // LE TRIO DES TABLES DE SOUS-ZONE — zone 2, Effects/MiscMarkers.cs
            // ====================================================================================

            // Le pere (0x0056b0c8, 0x1d) lance ce qui tombe (0x0056b2c8, 0x1e) ; a l'impact, il fait
            // naitre 39 eclats (0x0056b1c8, 0x1f) qui tournent en s'ecartant et frappent dix images.
            case 0x004830F0: EntityHandler_004830F0(record); return;
            case 0x00483360: EntityHandler_00483360(record); return;
            case 0x00483260: EntityHandler_00483260(record); return;

            // Le sous-type 16 du bloc C — LE MECHANT OURS, la plus riche de la famille : TROIS
            // gestes au lieu de deux, une attaque a distance, une prise qui projette, et le seul
            // pas du depot qui secoue la camera.
            case 0x004A4AE0: TickSceneEntityAi_004A4AE0(record); return;

            // Le sous-type 17 du bloc C — LE BEHEMOTH, la derniere IA du bloc. Deux machines a
            // postures (le bit 0x800 choisit), trois gestes, et LE PREMIER SORT LANCE PAR UNE BETE.
            case 0x004A54E0: TickSceneEntityAi_004A54E0(record); return;

            // Le sous-type 14 du bloc B — LE GEANT, un Grizzly a trois differences : ses DEUX
            // gestes quittent le sol, son elan ne part que si la cible etait assez loin a
            // l'engagement, et sa replanification a un second essai sur un cone elargi.
            // Et sa copie de la mine, sous-zones 3, 4 et 9 : le gabarit du bond est lu a 0x005528fc,
            // dont les douze octets sont ceux de 0x005529dc.
            case 0x00495C90:
            case 0x004A2C90: TickSceneEntityAi_004A2C90(record); return;

            // Le sous-type 15 du bloc B — WILLIE, quatrieme bete volante : la sœur de la
            // Coccinelle qui ENFLE pour frapper (trois fois sa taille), dont la sonde est centree
            // sur son propre corps, et qui pulse en taille et en couleur sur la phase de son vol.
            // Et ses deux copies de la mine (sous-zones 5 a 8, puis 3, 4 et 9), identiques.
            case 0x0048C530:
            case 0x004935F0:
            case 0x004A35B0: TickSceneEntityAi_004A35B0(record); return;

            // Le sous-type 10 du bloc B — LE TRENT, seconde bete du depot qui LANCE UN SORT : le
            // mecanisme d'incantation du Behemoth au complet, plus une charge qui ne frappe qu'en
            // mouvement et un saut dont la hardiesse decide la frequence.
            case 0x0049FAD0: TickSceneEntityAi_0049FAD0(record); return;

            // Le sous-type 13 du bloc B — LE VARAN, batie sur une AUTRE charpente : treize
            // postures au lieu de quatre, aucune des quatre feuilles partagees, un heurt de mur
            // qui annule l'image et contourne, et le troisieme lanceur de sort de la foret.
            case 0x004A1D50: TickSceneEntityAi_004A1D50(record); return;

            // Le sous-type 12 du bloc B — LE WYRM, cinquieme bete volante et la seule des cinq a
            // porter DEUX gestes : le coup par en dessous et une charge qui ne blesse qu'une fois.
            // Le crible l'annoncait pres du Crabe ; la liste d'appels dit Willie.
            case 0x004A13E0: TickSceneEntityAi_004A13E0(record); return;

            // LE GREMLIN — le sous-type 2 de la vieille tour, et cinq copies du meme corps dans
            // cinq blocs : elles ne different que par l'adresse de leur table de sorts, et les cinq
            // tables portent les memes octets. Un seul corps, cinq entrees.
            case 0x004A7190:
            case 0x004B22D0:
            case 0x004E8DF0:
            case 0x004EDB60:
            case 0x004F8270:
                TickSceneEntityAi_004A7190(record); return;

            // LE POURPRE PREUX — le sous-type 11 du chateau de l'immortel, et deux copies dans la
            // grotte scellee. Identiques instruction pour instruction, sans donnee propre.
            case 0x004D1F70:
            case 0x004E67C0:
            case 0x004EB530:
            // L AGONISANT du Labyrinthe des morts (sous-zones 5 a 8) : le Pourpre preux, integration
            // reordonnee, tables de sauts identiques en relatif, chercheur 0x004BDE40 conforme.
            case 0x004BEE40:
            // ... et 0x004C7D00, le creneau des sous-zones 0 a 4 du MEME chateau (palier 1,
            // 2026-09-18). Le compilateur a EMIS DEUX FOIS le bloc de posture 2 ici et une seule
            // fois dans 0x004D1F70, qu'il a fusionne par la queue : d'ou quinze lignes de `diff`
            // et un tirage de plus dans le corps, pour la meme partie jouee. Hors ces quinze
            // lignes, la seule immediate de donnee qui change est l'adresse de la table de sauts —
            // quatorze entrees de part et d'autre, aux memes decalages depuis la tete du corps.
            case 0x004C7D00:
                TickSceneEntityAi_004D1F70(record); return;

            // LE VAMPIRE ET LE PETIT VAMPIRE (chateau, sous-zones 6 a 10 ; et 5 et 11) — la charpente
            // du Rampant, et une posture 12 qui invoque le nuage du chateau. Ai/ImmortalCastle/Vampire.cs.
            case 0x004CF3A0:
            case 0x004D8F70:
                TickSceneEntityAi_004CF3A0(record); return;

            // LE CHEVALIER NOIR (vieille tour, sous-zones 5, 6, 8, 10) ET LE PLATINUM (3, 7, 9) — le Pourpre
            // preux retouche : poussee 0x7000, coups speciaux au quart, charge refusee a la variante 0.
            // Ai/OldTower/BlackKnight.cs.
            case 0x004AF560:
            case 0x004B5AF0:
                TickSceneEntityAi_004AF560(record); return;

            // LE SORCIER (vieille tour, sous-zones 5, 6, 8, 10) et sa copie des sous-zones 3, 7, 9 — la
            // charpente de la Salamandre, ses propres sorts et un plongeon. Ai/OldTower/Sorcerer.cs.
            case 0x004AE820:
            case 0x004B4DB0:
                TickSceneEntityAi_004AE820(record); return;

            // LE GARDE GOLEM (vieille tour, sous-zones 0, 1, 2, 4) ET LES DEUX ARMURES VOLANTES (3, 7, 9 et
            // 5, 6, 8, 10) — trois corps, une routine. Ai/OldTower/GuardGolem.cs.
            case 0x004A9150:
            case 0x004B3650:
            case 0x004AB940:
                TickSceneEntityAi_004A9150(record); return;

            // LA SYLPHIDE (vieille tour, sous-zones 3, 7, 9) — la charpente du Behemoth allegee : deux gestes,
            // six sorts. Ai/OldTower/Sylphide.cs.
            case 0x004B4570: TickSceneEntityAi_004B4570(record); return;

            // LE WYBARN (vieille tour, sous-zones 3, 7, 9) — le vol de la Coccinelle, trois gestes de
            // Behemoth : morsure empoisonnee, prise, souffle. Ai/OldTower/Wybarn.cs.
            case 0x004B7540: TickSceneEntityAi_004B7540(record); return;

            // LE TENEBREUX (vieille tour, sous-zones 3, 7, 9) et sa copie de la grotte scellee (11, 12) — le
            // grand frere du Wybarn : ruee, serre qui s eleve, souffle choisi par la variante.
            // Ai/OldTower/Tenebrous.cs.
            case 0x004B6B90:
            case 0x00500E10:
                TickSceneEntityAi_004B6B90(record); return;

            // L ENCHANTERESSE (vieille tour, sous-zones 3, 7, 9) — le vol du Tenebreux, une ruee qui touche a
            // chaque image et l incantation du Behemoth sur neuf tirages. Ai/OldTower/Enchantress.cs.
            case 0x004B7E20: TickSceneEntityAi_004B7E20(record); return;

            // LE RAMPANT (vieille tour, sous-zones 5, 6, 8, 10) — la charpente du Pourpre preux, un coup, un
            // bond et le sort de la Salamandre. Ai/OldTower/Crawler.cs.
            case 0x004ADAE0: TickSceneEntityAi_004ADAE0(record); return;

            // LE SABRE VOLANT (vieille tour, sous-zones 5, 6, 8, 10) — le cousin du Garde golem : pique,
            // plongeon, vol plane dont la phase ecrase la variante. Ai/OldTower/FlyingSaber.cs.
            case 0x004B1660: TickSceneEntityAi_004B1660(record); return;

            // LE GOLEM-SQUELETTE ET LE GROS DUR (vieille tour, sous-zones 5, 6, 8, 10) — la charpente du
            // Pourpre preux sans parade : coup, frappe qui altere, charge, recul, et une replanification
            // qui se relance. Ai/OldTower/SkeletonGolem.cs.
            case 0x004B0600: TickSceneEntityAi_004B0600(record); return;

            // LA CRAPOUILLE (vieille tour), LA GARGOUILLE (chateau) ET LE CRAPAUD (ruines
            // souterraines) — trois especes, trois copies conformes, une seule routine.
            case 0x004ACC20:
            case 0x004CE120:
            case 0x004DB610:
            // Une seconde Crapouille de la vieille tour (sous-zones 0, 1, 2, 4) et sa copie stricte du
            // chateau : deux comparaisons `100 - x` contre `(y >> 1) + 10` dont les registres sont
            // echanges, rien d'autre ; chercheur de proie 0x004A8150, table de sauts identique en
            // relatif. Verifie le 2026-09-17.
            case 0x004AAA80:
            case 0x004CD260:
                TickSceneEntityAi_004ACC20(record); return;

            // LE GREMLIN DE LA GROTTE SCELLEE — un terrestre qui n'a rien du Gremlin de la vieille
            // tour. Trois copies conformes dans la meme zone.
            case 0x004F3350:
            case 0x004F5630:
            case 0x004FC9B0:
                TickSceneEntityAi_004F3350(record); return;

            // LE PERSIL (Labyrinthe des morts, sous-zones 0 a 3 pour 0x004BAB60, 4 et 9 pour
            // 0x004C4290) — la charpente du Rampant, mais il frappe EN PLEIN BOND, sa posture 10
            // fait naitre le sous-type 0x36 au lieu d'incanter, et il plane (8) et vire au huitieme
            // de l'ecart (9). Ai/Labyrinth/Persil.cs.
            case 0x004BAB60:
            case 0x004C4290:
                TickSceneEntityAi_004BAB60(record); return;

            // LE SANGUINAIRE ET LE TAS D'OS (Labyrinthe des morts, sous-zones 0 a 3 pour
            // 0x004B8540, 4 et 9 pour 0x004C2770 — le meme corps au sens STRICT) — la seule bete du
            // depot qui PARE : toute replanification finit par un examen qui jette la posture
            // choisie pour la garde si la cible est elle-meme en train de frapper.
            // Ai/Labyrinth/Sanguinaire.cs.
            case 0x004B8540:
            case 0x004C2770:
            // ... et 0x004C9140, le chateau de l'immortel (palier 1, 2026-09-18) : MEME CORPS, a
            // l'adresse de sa table de sauts pres — douze entrees aux memes decalages. Ses quinze
            // lignes de `diff` sont une fusion par la queue des DEUX blocs de posture 2, que le
            // Labyrinthe emet en double ; aucune immediate de donnee ne change.
            case 0x004C9140:
                TickSceneEntityAi_004B8540(record); return;

            // LE MORT-VIVANT (Labyrinthe des morts, sous-zones 0 a 3 pour 0x004BB860, 5 a 8 pour
            // 0x004BD170) — un volant de la famille du Sorcier qui frappe A CHAQUE IMAGE tant que
            // son plongeon dure, et dont le plongeon descend vers la proie au lieu de flotter.
            // Ai/Labyrinth/Undead.cs.
            case 0x004BB860:
            case 0x004BD170:
                TickSceneEntityAi_004BB860(record); return;

            // L'OMBRE (Labyrinthe des morts, sous-zones 5 a 8), L'OMBRE ROUGE (4 et 9) et leurs deux
            // copies de la grotte scellee — un volant incantateur qui scintille, ne tombe jamais et
            // porte le bit 0x1000 a chaque image. Les quatre corps sont LOOSE-identiques ; seul
            // differe le mot lu seize places apres leur liste de sorts, que la variante 2 et au-dela
            // atteint par un registre non charge. Ai/Labyrinth/Shadow.cs.
            // LE DUNŒUD (Labyrinthe des morts, sous-zones 0 a 3 pour 0x004BA110, 4 et 9 pour
            // 0x004C3840) — la plus courte routine de la zone : elle ne sauvegarde pas son
            // placement, dort tant que la proie est loin, et n'a qu'une idee, une charge en onze
            // temps dont elle est la seule posture a tourner en frappant. Ai/Labyrinth/Dunoeud.cs.
            case 0x004BA110:
            case 0x004C3840:
                TickSceneEntityAi_004BA110(record); return;

            // LE REVENANT (Labyrinthe des morts, sous-zones 5 a 8 pour 0x004C1C30, 4 et 9 pour
            // 0x004C6700) — le Dunœud en plus lourd : meme sommeil, meme charge en onze temps, mais
            // il rend son placement sur un heurt et le mur le met en marche droite.
            // Ai/Labyrinth/Revenant.cs.
            case 0x004C1C30:
            case 0x004C6700:
                TickSceneEntityAi_004C1C30(record); return;

            // LE ZOMBIE (Labyrinthe des morts, sous-zones 0 a 3) et sa copie STRICTE du chateau de
            // l'immortel (sous-zones 0 a 4) — un terrestre que les portes arretent, dont la demarche
            // saute une fois sur hardiesse/8, et dont toute decision est ecrasee sous 0x64.
            // Ai/Labyrinth/Zombie.cs.
            case 0x004B95E0:
            case 0x004CA9C0:
                TickSceneEntityAi_004B95E0(record); return;

            // LE DURAHAN ET LE DURAHAN-NOIR (Labyrinthe des morts, sous-zones 4 et 9) — la charpente
            // du Behemoth, DEUX tables de postures selon que le bit d'attaque est pose, un combo a
            // trois temps chaine a pile ou face, et un bond qui frappe a demi-puissance.
            // Ai/Labyrinth/Durahan.cs.
            case 0x004C4F90: TickSceneEntityAi_004C4F90(record); return;

            // LE ROI-ZOMBIE (Labyrinthe des morts, sous-zone 9) — le patron : un volant incantateur
            // a QUATRE tables de sauts, dont cinq appels dont les sous-types viennent tous du bloc
            // de zone, et un plongeon qui frappe a la vitesse et non a l'image.
            // Ai/Labyrinth/ZombieKing.cs.
            case 0x004C7240: TickSceneEntityAi_004C7240(record); return;

            // LA MOMIE, LA REINE MOMIE, LA SANGLANTE ET LA VIEILLE MOMIE (Labyrinthe des morts,
            // sous-zones 5 a 8) — la plus longue routine de la zone, quatorze postures, et une
            // VARIANTE qui decide de tout : la taille, la teinte, l'effet du coup et la naissance.
            // Ai/Labyrinth/Mummy.cs.
            case 0x004C0B60: TickSceneEntityAi_004C0B60(record); return;

            // LOT 3 — LE BANC DES SOUS-ZONES 4 ET 9 DU LABYRINTHE, ET SES PARENTS HORS ZONE.
            // Ce banc n'est PAS une copie du banc 0-3 : cinq corps seulement y ont un parent proche,
            // et ces parents vivent au CHATEAU DE L'IMMORTEL (zone 6) et dans la GROTTE SCELLEE
            // (zone 10). Douze corps pour quatre lectures, reparties sur trois zones.
            // Effects/LabyrinthParticles.cs, sauf le decor, dans Handlers/Labyrinth.cs.
            case 0x00422C60: EntityHandler_00422C60(record); return;   // la luciole a deux porteurs, labyrinthe 4-9
            case 0x0043FA90: EntityHandler_0043FA90(record); return;   // la meme, grotte scellee
            case 0x00422D50: EntityHandler_00422D50(record); return;   // l'invocation en onze, labyrinthe 4-9
            case 0x0042A440: EntityHandler_0042A440(record); return;   // la meme, chateau de l'immortel
            case 0x00422330: EntityHandler_00422330(record); return;   // la mort du porteur, labyrinthe 4-9
            case 0x00429A20: EntityHandler_00429A20(record); return;   // la meme, chateau de l'immortel
            case 0x00436790: EntityHandler_00436790(record); return;   // la meme, grotte scellee
            case 0x0043F320: EntityHandler_0043F320(record); return;   // la meme, grotte scellee, second bloc
            case 0x00462860: EntityHandler_00462860(record); return;   // le decor qui respire en couleur, 5-8
            case 0x00463640: EntityHandler_00463640(record); return;   // le meme, 4 et 9

            // LES EFFETS DU LABYRINTHE DES MORTS — table 0x0056b2c8, lot 2 : les NEUF PAIRES MIROIR.
            // Le bloc des sous-zones 5 a 8 est celui des sous-zones 0 a 3 recopie a +0x2ff0, corps
            // par corps et creneau par creneau ; 19 adresses pour 8 lectures. Effects/LabyrinthParticles.cs.
            case 0x0041E610: EntityHandler_0041E610(record); return;   // la bouffee large, 0-3
            case 0x0041E700: EntityHandler_0041E700(record); return;   // la bouffee serree, 0-3
            case 0x00421600: EntityHandler_00421600(record); return;   // la bouffee large, 5-8
            case 0x004216F0: EntityHandler_004216F0(record); return;   // la bouffee serree, 5-8
            case 0x0041E340: EntityHandler_0041E340(record); return;   // le grain qui monte, 0-3
            case 0x00421330: EntityHandler_00421330(record); return;   // le grain qui monte, 5-8
            case 0x00425500: EntityHandler_00425500(record); return;   // le grain qui monte, 4 et 9
            case 0x0041DF60: EntityHandler_0041DF60(record); return;   // le grain projete, 0-3
            case 0x00420F50: EntityHandler_00420F50(record); return;   // le grain projete, 5-8
            case 0x0041E9D0: EntityHandler_0041E9D0(record); return;   // le grain qui attend puis tombe, 0-3
            case 0x004219C0: EntityHandler_004219C0(record); return;   // idem, 5-8
            case 0x0041E7F0: EntityHandler_0041E7F0(record); return;   // la braise, cap soustrait, 0-3
            case 0x004217E0: EntityHandler_004217E0(record); return;   // la braise, cap AJOUTE, 5-8
            case 0x0041DA80: EntityHandler_0041DA80(record); return;   // la nappe qui tombe, 0-3
            case 0x00420A70: EntityHandler_00420A70(record); return;   // la nappe qui tombe, 5-8
            case 0x0041DBD0: EntityHandler_0041DBD0(record); return;   // le flocon de cendre, 0-3
            case 0x00420BC0: EntityHandler_00420BC0(record); return;   // le flocon de cendre, 5-8
            case 0x0041DD40: EntityHandler_0041DD40(record); return;   // l'etincelle d'eventail, 0-3
            case 0x00420D30: EntityHandler_00420D30(record); return;   // l'etincelle d'eventail, 5-8

            // LE DECOR DU LABYRINTHE DES MORTS — table 0x0056adc8, lot 1 (les sept plus simples de
            // la categorie B de Zone5ParComplexite.md). Handlers/Labyrinth.cs.
            case 0x004610E0: EntityHandler_004610E0(record); return;   // l'echelle reposee a chaque image
            case 0x00460240: EntityHandler_00460240(record); return;   // visible si le drapeau 236 est pose
            case 0x00460A90: EntityHandler_00460A90(record); return;   // ce que la sous-zone 1 cache au depart
            case 0x004603C0: EntityHandler_004603C0(record); return;   // la source sonore, une fois sur 21 images
            case 0x00460D10: EntityHandler_00460D10(record); return;   // la hauteur qui flotte tant que le 250 tient
            case 0x00461BE0: EntityHandler_00461BE0(record); return;   // le balancement a phase tiree au hasard
            case 0x00461E00: EntityHandler_00461E00(record); return;   // la chute de dix images

            // LE DECOR DU LABYRINTHE DES MORTS — table 0x0056adc8, lot 4 : les sept corps de decor
            // des dix plus simples qui restaient. Handlers/Labyrinth.cs.
            case 0x00460260: EntityHandler_00460260(record); return;   // la lueur rouge que son drapeau eteint
            case 0x00463300: EntityHandler_00463300(record); return;   // la porte a un battant, 4 et 9
            case 0x00461B00: EntityHandler_00461B00(record); return;   // ce qui descend une fois et se tait
            case 0x00461E80: EntityHandler_00461E80(record); return;   // la chute au contact, et ses deux voisines
            case 0x00460FD0: EntityHandler_00460FD0(record); return;   // la respiration, puis quarante images de clignotement
            case 0x00462250: EntityHandler_00462250(record); return;   // le bloc qu'on pousse, son propre a la sous-zone 6
            case 0x00460310: EntityHandler_00460310(record); return;   // la source d'effets qui seme a cote d'elle

            // LES MECANISMES DU LABYRINTHE DES MORTS — table 0x0056adc8, lot 4 : les trois corps du
            // lot qui consultent le groupe ou la table des emplacements. Handlers/LabyrinthMecanismes.cs.
            case 0x004618B0: EntityHandler_004618B0(record); return;   // ne part que si le groupe entier est a bord
            case 0x00460990: EntityHandler_00460990(record); return;   // la porte tenue ouverte a la manette
            case 0x00462990: EntityHandler_00462990(record); return;   // le panneau qui tourne puis s'eteint

            // LE DECOR DU LABYRINTHE DES MORTS — table 0x0056adc8, lot 5 : la fin de la categorie B
            // du decor, et ses trois plus simples « C ». Handlers/Labyrinth.cs.
            case 0x00460C00: EntityHandler_00460C00(record); return;   // le demi-tour verrouille par un drapeau
            case 0x00461390: EntityHandler_00461390(record); return;   // ce qui frappe et projette en l'air
            case 0x00461980: EntityHandler_00461980(record); return;   // le bloc dont la position survit a la carte
            case 0x00461100: EntityHandler_00461100(record); return;   // le balancier qui change de bord
            case 0x004620D0: EntityHandler_004620D0(record); return;   // la navette, aller et retour sans fin
            case 0x00460800: EntityHandler_00460800(record); return;   // ce qui s'effondre quand on monte dessus
            case 0x00461F50: EntityHandler_00461F50(record); return;   // l'ecroulement a quatre compteurs empaquetes
            case 0x00461260: EntityHandler_00461260(record); return;   // le porte-voix des trois points fixes

            // ... et les deux qui lancent un script de zone. Handlers/LabyrinthMecanismes.cs.
            case 0x004606C0: EntityHandler_004606C0(record); return;   // l'escalier qui se derobe, marche par marche
            case 0x00460AC0: EntityHandler_00460AC0(record); return;   // ce qui guette l'approche puis lance un script

            // LE LABYRINTHE DES MORTS, lot 6 : la categorie B de la zone, VIDEE. Onze gestionnaires
            // d'entite sur les douze restants — le douzieme, 0x00444340, est un VERBE de script
            // (table 0x0056aec8) et vit dans WorldMapScript.cs, aiguille par ScriptOpAreaVerb.
            // Effects/LabyrinthParticles.cs, sauf les deux dernieres.
            case 0x00483C50: EntityHandler_00483C50(record); return;   // elle se transforme et meurt en une image
            case 0x00483E90: EntityHandler_00483E90(record); return;   // le halo qui s'ouvre, monte et s'eteint
            case 0x004840E0: EntityHandler_004840E0(record); return;   // le meme, qui entraine son jumeau de +0x70
            case 0x00425270: EntityHandler_00425270(record); return;   // la bulle qui enfle et fait naitre
            case 0x00425350: EntityHandler_00425350(record); return;   // la meme, deja grande et deja lancee
            case 0x0041D090: EntityHandler_0041D090(record); return;   // l'etincelle a deux ancrages, version gardee
            case 0x0041F520: EntityHandler_0041F520(record); return;   // la meme, sans le garde
            case 0x004249D0: EntityHandler_004249D0(record); return;   // le debris qui attend puis tombe au sol
            case 0x00425410: EntityHandler_00425410(record); return;   // huit images, quatre planches, fini

            // ... et les deux secousses de camera, Handlers/LabyrinthMecanismes.cs.
            case 0x0041D860: EntityHandler_0041D860(record); return;   // la secousse qui finit sur une phrase
            case 0x00420860: EntityHandler_00420860(record); return;   // la meme, qui finit sur un script

            // LE LABYRINTHE DES MORTS, lot 7 : la categorie C du DECOR (table 0x0056adc8), videe.
            // Handlers/Labyrinth.cs, sauf les trois dernieres.
            case 0x00460430: EntityHandler_00460430(record); return;   // le porte-voix des quatorze points
            case 0x00460D60: EntityHandler_00460D60(record); return;   // ce qui tourne puis fait trembler la carte
            case 0x00461C60: EntityHandler_00461C60(record); return;   // le pilon qui descend, ecrase et remonte
            case 0x00463150: EntityHandler_00463150(record); return;   // le chemin en huit temps, et ses deux voisines
            case 0x00462330: EntityHandler_00462330(record); return;   // ce qui tombe du plafond et rebondit
            case 0x004625B0: EntityHandler_004625B0(record); return;   // sa sœur, au garde de camera inverse

            // ... et les trois qui prennent le groupe en charge. Handlers/LabyrinthMecanismes.cs.
            case 0x00461730: EntityHandler_00461730(record); return;   // elle guette, file, et lance un script
            case 0x004633C0: EntityHandler_004633C0(record); return;   // le mecanisme a usage unique qui fige la partie
            case 0x004614B0: EntityHandler_004614B0(record); return;   // ce qui roule et pousse ce qu'il rencontre

            // LE LABYRINTHE DES MORTS, lot 8 : la categorie C des tables d'effets. Les deux verbes
            // du lot (0x004443D0, 0x004444A0) vivent dans WorldMapScript.cs, aiguilles par
            // ScriptOpAreaVerb. Effects/LabyrinthParticles.cs.
            case 0x0041D1A0: EntityHandler_0041D1A0(record); return;   // le nuage cale sur son porteur, effet 32
            case 0x0041F600: EntityHandler_0041F600(record); return;   //   le meme, sans tester le porteur
            case 0x00421C10: EntityHandler_00421C10(record); return;   //   le meme, avec le repli et le garde
            case 0x0041FCA0: EntityHandler_0041FCA0(record); return;   //   le stroboscope de la foret ou le nuage, selon +0x9a
            case 0x0041C740: EntityHandler_0041C740(record); return;   // la mort du porteur, sans porteur : on joue quand meme
            case 0x0041EC20: EntityHandler_0041EC20(record); return;   //   la meme, porteur lu sans test
            case 0x00424C70: EntityHandler_00424C70(record); return;   // l'escarbille qui retombe et palit
            case 0x00424AE0: EntityHandler_00424AE0(record); return;   // la ligne oblique qui s'eloigne en montant
            case 0x00424800: EntityHandler_00424800(record); return;   // le modele qui s'ouvre en tournant, tient, s'eteint
            case 0x004250B0: EntityHandler_004250B0(record); return;   // la fumee qui derive, tourbillonne et gonfle
            case 0x00483F30: EntityHandler_00483F30(record); return;   // naissance, ou tireur qui vise le porteur de son porteur
            case 0x00483CC0: EntityHandler_00483CC0(record); return;   // le tourbillon qui grandit, avance et frappe

            // ... et une scene, Cutscene/LabyrinthScenes.cs.
            case 0x00424DC0: EntityHandler_00424DC0(record); return;   // cent images sur l'espece 1, puis le script du membre
            case 0x004244D0: EntityHandler_004244D0(record); return;   // deux cents images sur l'espece 2, fond rouge

            // LE LABYRINTHE DES MORTS, lot 9 : le premier « D », et son jumeau de la grotte scellee.
            // Effects/LabyrinthParticles.cs.
            case 0x004224F0: EntityHandler_004224F0(record); return;   // la nappe devant le porteur, qui marque qui s'y trouve
            case 0x0043F4E0: EntityHandler_0043F4E0(record); return;   //   la meme, grotte scellee — une instruction d'ecart

            // LE LABYRINTHE DES MORTS, lot 10 : les deux autres « charpentes », qui sont le meme corps.
            case 0x0041C900: EntityHandler_0041C900(record); return;   //   la meme, repli sur un tampon nul
            case 0x0041EDD0: EntityHandler_0041EDD0(record); return;   //   la meme, sans repli

            // LE LABYRINTHE DES MORTS, lot 11 : les semis des nuages du lot 8, quatre corps pour un.
            case 0x0041D310: EntityHandler_0041D310(record); return;   // 0xed, 0-3 : l'etincelle qui va chercher la cible
            case 0x0041F760: EntityHandler_0041F760(record); return;   //   0xed, 5-8
            case 0x0041FF40: EntityHandler_0041FF40(record); return;   //   0xdf, 5-8 — le semis du stroboscope
            case 0x00421DC0: EntityHandler_00421DC0(record); return;   //   0xed, 4 et 9, avec les replis

            // LE LABYRINTHE DES MORTS, lot 12 : l'onde qui frappe une fois chacun, ou sa gerbe.
            case 0x00420480: EntityHandler_00420480(record); return;   // 5-8, sans test
            case 0x00422E50: EntityHandler_00422E50(record); return;   //   4 et 9, avec les replis

            // LE LABYRINTHE DES MORTS, lot 13 : les deux derniers.
            case 0x00484190: EntityHandler_00484190(record); return;   // le projectile a tete chercheuse (0xda)
            case 0x00462A80: EntityHandler_00462A80(record); return;   // le battant qui bascule et entraine sa voisine

            case 0x004BFE90: TickSceneEntityAi_004BFE90(record, 0x0000); return;   // &DAT_00552ba0
            case 0x004C5A30: TickSceneEntityAi_004BFE90(record, 0x0200); return;   // &DAT_00552bf0
            case 0x004E7810: TickSceneEntityAi_004BFE90(record, 0x000d); return;   // &DAT_00552e98
            case 0x004EC580: TickSceneEntityAi_004BFE90(record, 0x000d); return;   // &DAT_00552ef8
            // ... et celle du chateau de l'immortel (palier 1, 2026-09-18), sous-zones 6 a 10 :
            // meme corps, table de sauts a onze entrees aux memes decalages, chercheur 0x004CEFE0
            // conforme a 0x004A1020. Sa liste de sorts est 0x00552c28 — memes dix-sept mots — et
            // le mot egare qui la suit de seize places vaut 0x0102.
            case 0x004D12A0: TickSceneEntityAi_004BFE90(record, 0x0102); return;   // &DAT_00552c68

            // Le sous-type 11 du bloc B — LA FLEUR DU MAL, seconde routine de la charpente du
            // Varan : memes feuilles propres, meme sonde au mot pres, et un geste qui seme des
            // braises. Elle FERME la zone 3.
            case 0x004A0480: TickSceneEntityAi_004A0480(record); return;

            // Le sous-type 3 — l'ABEILLE, deuxieme bete volante : la queue de la Coccinelle (vol,
            // integration, interruption) sur la charpente des terrestres, et une posture 3 qui file
            // en ligne droite au lieu de marcher.
            case 0x0049B220: TickSceneEntityAi_0049B220(record); return;

            // Le sous-type 7 — l'HOMME-LEZARD, la plus riche : les cinq postures de la Fourmi
            // geante, recul et esquive compris, ET deux gestes. Sa course BOUSCULE sans blesser —
            // FUN_00539b80 n'est appele que depuis son autre geste.
            case 0x0049D030: TickSceneEntityAi_0049D030(record); return;

            // 0x0049f0c0 est un DUPLICATA RELOCALISE de l'Homme-lezard : 648 instructions
            // comparees une a une, une seule difference — l'adresse de sa propre table de sauts,
            // dont les cinq cibles sont decalees de 0x2090, exactement l'ecart entre les deux
            // fonctions. Le compilateur a emis le meme corps deux fois.
            case 0x0049F0C0: TickSceneEntityAi_0049D030(record); return;

            // L'Homme-lezard et le Lezard rouge de la vieille tour, sous-zones 0, 1, 2 et 4 : le
            // desassemblage STRICT est identique a celui de 0x0049D030, table de sauts comprise.
            case 0x004AA070: TickSceneEntityAi_0049D030(record); return;

            // Le sous-type 8 — le GOBLIN, la premiere bete qui ATTAQUE DE LOIN : son second geste
            // ne croise aucun gabarit, il fait naitre le projectile 0x49 et rend la main.
            // Et deux copies : la mine (sous-zones 0 a 2) et la vieille tour (0, 1, 2, 4). Leur texte
            // porte en ligne le « tenir la distance » (posture 2, portee 0x64) que la foret atteint
            // par un saut ; memes ecritures, meme tirage.
            case 0x004879F0:
            case 0x004A6290:
            case 0x0049DA40: TickSceneEntityAi_0049DA40(record); return;

            // Le sous-type 0xfe de la table installee par zone (0x0056afc8 + 0xfe * 4 deborde
            // sur 0x0056b2c8) — l'ECLABOUSSURE DU PAS DANS L'EAU, que le crochet de contact au
            // sol (EntityHandler_00541440) seme un pas sur deux quand le sol porte 0x200. C'est
            // le plantage a l'approche de l'eau, signale le 2026-08-30.
            case 0x0040F7F0: EntityHandler_0040F7F0(record); return;
            case 0x00412B30: EntityHandler_00412B30(record); return;
            case 0x00416120: EntityHandler_00416120(record); return;

            // Le sous-type 0xfd de la meme table — la POUSSIERE DU BLOC POUSSE, signalee le
            // 2026-08-30 sur le teleporteur. Premier gestionnaire que la sonde a designe avant
            // qu'il soit ecrit.
            case 0x0040F370: EntityHandler_0040F370(record); return;

            // Le fondu au blanc de la transition — table 0x0056b0c8, foret. Il anime les deux
            // voiles plein cadre 0x005de024 et 0x005de028, dont le role etait « non etabli ».
            case 0x0040EDF0: EntityHandler_0040EDF0(record); return;

            // Quatre effets de plus de la meme table, transcrits d'un bloc le 2026-08-30 : tous
            // decompiles dans Blaze.c, tous de la famille « naitre, se disperser, decompter ».
            case 0x0040F190: EntityHandler_0040F190(record); return;
            case 0x0040F470: EntityHandler_0040F470(record); return;
            case 0x0040F8E0: EntityHandler_0040F8E0(record); return;
            case 0x0040FAC0: EntityHandler_0040FAC0(record); return;

            // Les deux autres exemplaires de la bulle : meme corps, autre planche.
            case 0x00412C20: EntityHandler_00412C20(record); return;
            case 0x00416210: EntityHandler_00416210(record); return;

            // Le rocher qui roule, sœur maigre de 0x00456a30.
            case 0x00457E20: EntityHandler_00457E20(record); return;
            case 0x00459A70: EntityHandler_00459A70(record); return;

            // Les deux mecanismes de decor, miroirs l'un de l'autre : celui qui monte et celui
            // qui tombe, tous deux armes par un bit de la banque de script 0.
            case 0x00459C20: EntityHandler_00459C20(record); return;
            case 0x00459DC0: EntityHandler_00459DC0(record); return;

            // L'EXPLOSION : six eclats en couronne, un noyau, une secousse d'ecran amortie et
            // un script de sons differes. Tout a la premiere image ; dix images de vie.
            case 0x00483650: EntityHandler_00483650(record); return;
            case 0x004134C0: EntityHandler_004134C0(record); return;

            // L'ASCENSEUR — deux courses en un corps, choisies par +0x44. Il part quand tout
            // le monde est monte et redescend si quelqu'un tombe.
            case 0x00458FC0: EntityHandler_00458FC0(record); return;
            case 0x00457770: EntityHandler_00457770(record); return;

            // LA CONVOCATION, en deux exemplaires : quatre variantes dans un corps, dont un
            // chef qui lache trente-et-une spirales, une bouffee par image, puis trente-et-une
            // spirales sortantes avant de s'eteindre.
            case 0x00410CD0: EntityHandler_00410CD0(record); return;
            case 0x00414140: EntityHandler_00414140(record); return;

            // 0x004593c0 est un DUPLICATA du tourniquet : 293 instructions comparees une a une,
            // zero difference, et les deux pseudo-C de Blaze.c sont identiques ligne pour ligne.
            // Le compilateur a emis le meme corps deux fois.
            case 0x004593C0: EntityHandler_00457770(record); return;

            // Les deux autres exemplaires de l'eclat : meme corps, autre planche.
            case 0x00412950: EntityHandler_00412950(record); return;
            case 0x00415F40: EntityHandler_00415F40(record); return;

            // 0x00458070 est un DOUBLON octet pour octet de 0x004566c0, deja transcrit : trois
            // caracteres hexadecimaux d'ecart, le deplacement relatif du `call`. Le compilateur a
            // emis le meme corps deux fois. Un case suffit — voir la regle « verifier s'il y a un
            // doublon avant de transcrire » dans CLAUDE.md.
            case 0x00458070: EntityHandler_004566C0((uint*)record); return;
            case 0x0040F650: EntityHandler_0040F650(record); return;

            // Les quatre braises : deux familles de deux, voir le pave devant TickRisingEmber.
            case 0x00412330: EntityHandler_00412330(record); return;
            case 0x00415860: EntityHandler_00415860(record); return;
            case 0x00412810: EntityHandler_00412810(record); return;
            case 0x00415E00: EntityHandler_00415E00(record); return;
            case 0x00410370: EntityHandler_00410370(record); return;
            case 0x004137E0: EntityHandler_004137E0(record); return;
            // La troisieme amorce du meme flash, celle de la mine abandonnee (zone 1).
            case 0x004060C0: EntityHandler_004060C0(record); return;

            // Les particules de la mine abandonnee (zone 1) — Effects/MineParticles.cs.
            case 0x00406120: EntityHandler_00406120(record); return;
            case 0x00406130: EntityHandler_00406130(record); return;
            case 0x00406570: EntityHandler_00406570(record); return;
            case 0x004065D0: EntityHandler_004065D0(record); return;
            case 0x00406680: EntityHandler_00406680(record); return;
            case 0x00406810: EntityHandler_00406810(record); return;
            case 0x00406970: EntityHandler_00406970(record); return;
            case 0x00406AA0: EntityHandler_00406AA0(record); return;
            case 0x00406C40: EntityHandler_00406C40(record); return;
            case 0x00406DF0: EntityHandler_00406DF0(record); return;
            case 0x00406F60: EntityHandler_00406F60(record); return;
            case 0x004070F0: EntityHandler_004070F0(record); return;
            case 0x004071E0: EntityHandler_004071E0(record); return;
            case 0x00407800: EntityHandler_00407800(record); return;
            case 0x004079E0: EntityHandler_004079E0(record); return;
            case 0x00407AD0: EntityHandler_00407AD0(record); return;
            case 0x00407C10: EntityHandler_00407C10(record); return;
            case 0x00408490: EntityHandler_00408490(record); return;
            case 0x00408680: EntityHandler_00408680(record); return;
            case 0x00408910: EntityHandler_00408910(record); return;
            // Les copies des sous-zones 5 a 8 et 3, 4, 9 — des amorces et des corps deja ecrits
            // ailleurs, a la planche pres. Effects/MineParticles.cs.
            // Les decors de la mine, table 0x0056adc8 — Handlers/Mine.cs.
            case 0x00451170: EntityHandler_00451170(record); return;
            case 0x004511F0: EntityHandler_004511F0(record); return;
            case 0x004523A0: EntityHandler_004523A0(record); return;
            case 0x00452B20: EntityHandler_00452B20(record); return;
            case 0x00452B60: EntityHandler_00452B60(record); return;
            case 0x00453000: EntityHandler_00453000(record); return;
            case 0x00453DE0: EntityHandler_00453DE0(record); return;
            case 0x004545B0: EntityHandler_004545B0(record); return;
            case 0x00454AD0: EntityHandler_00454AD0(record); return;
            case 0x00454B40: EntityHandler_00454B40(record); return;
            case 0x00452950: EntityHandler_00452950(record); return;
            case 0x00454220: EntityHandler_00454220(record); return;
            case 0x004544E0: EntityHandler_004544E0(record); return;
            case 0x004529F0: EntityHandler_004529F0(record); return;
            case 0x004542C0: EntityHandler_004542C0(record); return;
            case 0x00455A90: EntityHandler_00455A90(record); return;
            case 0x00452A90: EntityHandler_00452A90(record); return;
            case 0x00454450: EntityHandler_00454450(record); return;
            case 0x00456010: EntityHandler_00456010(record); return;
            case 0x00454D90: EntityHandler_00454D90(record); return;
            case 0x00451250: EntityHandler_00451250(record); return;
            case 0x00451A00: EntityHandler_00451A00(record); return;
            case 0x00452670: EntityHandler_00452670(record); return;
            case 0x004528D0: EntityHandler_004528D0(record); return;
            case 0x00452BD0: EntityHandler_00452BD0(record); return;
            case 0x00453280: EntityHandler_00453280(record); return;
            case 0x00453E60: EntityHandler_00453E60(record); return;
            case 0x00454130: EntityHandler_00454130(record); return;
            case 0x00454BB0: EntityHandler_00454BB0(record); return;
            case 0x00454E10: EntityHandler_00454E10(record); return;
            case 0x00452090: EntityHandler_00452090(record); return;
            case 0x00454800: EntityHandler_00454800(record); return;
            case 0x00452790: EntityHandler_00452790(record); return;
            case 0x00453F60: EntityHandler_00453F60(record); return;
            case 0x004538C0: EntityHandler_004538C0(record); return;
            case 0x00455570: EntityHandler_00455570(record); return;
            case 0x00454360: EntityHandler_00454360(record); return;
            case 0x00455EB0: EntityHandler_00455EB0(record); return;
            case 0x00452410: EntityHandler_00452410(record); return;
            case 0x00453650: EntityHandler_00453650(record); return;
            case 0x00451460: EntityHandler_00451460(record); return;
            case 0x00451630: EntityHandler_00451630(record); return;
            case 0x00452E40: EntityHandler_00452E40(record); return;
            case 0x00451790: EntityHandler_00451790(record); return;
            case 0x00453070: EntityHandler_00453070(record); return;
            case 0x004518D0: EntityHandler_004518D0(record); return;
            case 0x00454610: EntityHandler_00454610(record); return;
            case 0x004533A0: EntityHandler_004533A0(record); return;
            case 0x00454ED0: EntityHandler_00454ED0(record); return;
            case 0x004551D0: EntityHandler_004551D0(record); return;
            case 0x00451B40: EntityHandler_00451B40(record); return;
            case 0x00409320: EntityHandler_00409320(record); return;
            case 0x0040C730: EntityHandler_0040C730(record); return;
            case 0x00409410: EntityHandler_00409410(record); return;
            case 0x0040A610: EntityHandler_0040A610(record); return;
            case 0x004092B0: EntityHandler_004092B0(record); return;
            case 0x00408DD0: EntityHandler_00408DD0(record); return;
            case 0x004087E0: EntityHandler_004087E0(record); return;
            case 0x00409E40: EntityHandler_00409E40(record); return;
            case 0x0040C460: EntityHandler_0040C460(record); return;
            case 0x004084F0: EntityHandler_004084F0(record); return;
            case 0x00408AB0: EntityHandler_00408AB0(record); return;
            case 0x00409120: EntityHandler_00409120(record); return;
            case 0x0040C5D0: EntityHandler_0040C5D0(record); return;
            case 0x0040C910: EntityHandler_0040C910(record); return;
            case 0x0040BCA0: EntityHandler_0040BCA0(record); return;
            case 0x004380C0: EntityHandler_004380C0(record); return;   // grotte scellee, corps de 0x0040BCA0
            case 0x0040BE50: EntityHandler_0040BE50(record); return;
            case 0x00408EF0: EntityHandler_00408EF0(record); return;   // Handlers/Mine.cs
            case 0x0040B940: EntityHandler_0040B940(record); return;
            case 0x0040BF50: EntityHandler_0040BF50(record); return;
            case 0x0043E170: EntityHandler_0043E170(record); return;   // grotte scellee, corps de 0x0040BF50
            case 0x00483B50: EntityHandler_00483B50(record); return;
            case 0x00486DE0: EntityHandler_00486DE0(record); return;   // grotte scellee, corps de 0x00483B50
            // La bouffee qui blesse, un corps pour trois zones — Combat/Projectiles.cs.
            case 0x00407D70: EntityHandler_00407D70(record); return;
            case 0x0040D6C0: EntityHandler_0040D6C0(record); return;
            case 0x00439D30: EntityHandler_00439D30(record); return;
            // L'onde qui afflige, mine et montagne du dragon de feu — Combat/Projectiles.cs.
            case 0x00409F40: EntityHandler_00409F40(record); return;
            case 0x0040CEA0: EntityHandler_0040CEA0(record); return;
            case 0x00482FF0: EntityHandler_00482FF0(record); return;   // Combat/Projectiles.cs
            case 0x00408040: EntityHandler_00408040(record); return;
            case 0x00408C60: EntityHandler_00408C60(record); return;
            case 0x00409A30: EntityHandler_00409A30(record); return;
            case 0x00409C10: EntityHandler_00409C10(record); return;
            case 0x00409D00: EntityHandler_00409D00(record); return;
            case 0x0040A670: EntityHandler_0040A670(record); return;
            case 0x0040AAC0: EntityHandler_0040AAC0(record); return;   // Cutscene/MineScenes.cs
            case 0x0040B8D0: EntityHandler_0040B8D0(record); return;   // Cutscene/MineScenes.cs

            // Les deux scenes de la montagne du dragon de feu, bloc prive 0x005713f0 — l'envol du
            // Dragon rouge (0xc2), qui fait naitre la scene muette (0xff). Cutscene/FireMountainScenes.cs.
            case 0x0040E3A0: EntityHandler_0040E3A0(record); return;
            case 0x0040D930: EntityHandler_0040D930(record); return;

            // ====================================================================================
            // LA VIEILLE TOUR, categorie A — des corps deja ecrits, a la planche ou au bloc pres.
            // Effects/OldTowerParticles.cs et Cutscene/OldTowerScenes.cs.
            // ====================================================================================

            // Les bulles (le corps de 0x004079E0) et les grains (celui de 0x004070F0), un par bloc.
            case 0x00417A20: EntityHandler_00417A20(record); return;
            case 0x00419630: EntityHandler_00419630(record); return;
            case 0x0041C650: EntityHandler_0041C650(record); return;
            case 0x00417930: EntityHandler_00417930(record); return;
            case 0x00419540: EntityHandler_00419540(record); return;
            case 0x0041C560: EntityHandler_0041C560(record); return;

            // La fumee violette et les deux amorces de la gerbe a cinq phases.
            case 0x004186A0: EntityHandler_004186A0(record); return;
            case 0x0041A560: EntityHandler_0041A560(record); return;
            case 0x0041B420: EntityHandler_0041B420(record); return;

            // Les deux amorces de la scene parlante, bloc prive 0x00571480.
            case 0x00419F40: EntityHandler_00419F40(record); return;
            case 0x0041B3F0: EntityHandler_0041B3F0(record); return;

            // ====================================================================================
            // LA VIEILLE TOUR, categorie B — les effets simples (lot 3).
            // ====================================================================================

            // Effects/OldTowerParticles.cs
            case 0x00416DE0: EntityHandler_00416DE0(record); return;   // le scintillement
            case 0x00416EE0: EntityHandler_00416EE0(record); return;   // la poussiere, reecrit la page
            case 0x00417000: EntityHandler_00417000(record); return;   // la bouffee
            case 0x00418290: EntityHandler_00418290(record); return;   // l'etincelle, 0-1-2-4
            case 0x00419E60: EntityHandler_00419E60(record); return;   // l'etincelle, 5-6-8-10
            case 0x00417650: EntityHandler_00417650(record); return;   // la lueur attiree, 0-1-2-4
            case 0x00419260: EntityHandler_00419260(record); return;   // la lueur attiree, 5-6-8-10
            case 0x00419D30: EntityHandler_00419D30(record); return;   // l'eclat projete
            case 0x0041B220: EntityHandler_0041B220(record); return;   // la goutte
            case 0x0041B2F0: EntityHandler_0041B2F0(record); return;   // la spirale qui monte
            case 0x0041B9F0: EntityHandler_0041B9F0(record); return;   // l'helice
            case 0x0041BFA0: EntityHandler_0041BFA0(record); return;   // la spirale qui s'ouvre
            case 0x0041BF00: EntityHandler_0041BF00(record); return;   // l'onde

            // Effects/AreaExitFlash.cs — les deux amorces du flash, ancres 0 et 2, Z + 0x64000.
            case 0x00418370: EntityHandler_00418370(record); return;
            case 0x00418640: EntityHandler_00418640(record); return;

            // Effects/MiscMarkers.cs et Combat/Projectiles.cs — les emetteurs a lanceur et la bulle
            // empoisonnee.
            case 0x00483930: EntityHandler_00483930(record); return;
            case 0x00484F80: EntityHandler_00484F80(record); return;
            case 0x00483A30: EntityHandler_00483A30(record); return;

            // ====================================================================================
            // LA VIEILLE TOUR, categorie C — LES HUIT FORMES A JUMEAUX (lot 4).
            //
            // Dix-sept creneaux pour huit corps. Les paires et le triplet sont marques ; deux paires
            // n'ont meme pas de parametre, leurs octets etant les memes a l'adresse pres.
            // ====================================================================================

            // Handlers/OldTower.cs — table 0x0056adc8.
            case 0x0045DF60: EntityHandler_0045DF60(record); return;   // le pivot sonore, 5-6-8-10
            case 0x0045FFA0: EntityHandler_0045FFA0(record); return;   // le meme, 3-7-9
            case 0x0045E040: EntityHandler_0045E040(record); return;   // le pivot silencieux, 5-6-8-10
            case 0x00460080: EntityHandler_00460080(record); return;   // le meme, 3-7-9
            case 0x0045E100: EntityHandler_0045E100(record); return;   // l'attente du verrou, 5-6-8-10
            case 0x00460140: EntityHandler_00460140(record); return;   // le meme, 3-7-9
            case 0x0045CF70: EntityHandler_0045CF70(record); return;   // le souffleur, 5-6-8-10
            case 0x0045F3B0: EntityHandler_0045F3B0(record); return;   // le meme, 3-7-9
            case 0x0045A630: EntityHandler_0045A630(record); return;   // la balance, 0-1-2-4
            case 0x0045E4E0: EntityHandler_0045E4E0(record); return;   // la meme, 3-7-9

            // Effects/OldTowerParticles.cs — table 0x0056b2c8.
            case 0x00417E10: EntityHandler_00417E10(record); return;   // la spirale, 0-1-2-4
            case 0x00419A20: EntityHandler_00419A20(record); return;   // la meme, 5-6-8-10
            case 0x00417F80: EntityHandler_00417F80(record); return;   // l'eclaboussure, 0-1-2-4
            case 0x00419B90: EntityHandler_00419B90(record); return;   // la meme, 5-6-8-10

            // Combat/Projectiles.cs — le feu follet, un par bloc, et il brule.
            case 0x00417110: EntityHandler_00417110(record); return;   // 0-1-2-4
            case 0x00418C40: EntityHandler_00418C40(record); return;   // 5-6-8-10
            case 0x0041C250: EntityHandler_0041C250(record); return;   // 3-7-9

            // ====================================================================================
            // LA VIEILLE TOUR, categorie C — les particules de 0x0056b2c8 (lot 2).
            //
            // Les quatre premieres paires partagent leur corps : le diff inter-blocs ne rend que la
            // planche, et pour deux d'entre elles une constante, passee en parametre. Les cinq
            // corps uniques sont appeles par leur nom de role — le `case` porte l'adresse, qui est
            // ce que le recensement lit.
            // ====================================================================================

            // Effects/OldTowerParticles.cs
            case 0x00417CA0: EntityHandler_00417CA0(record); return;   // la spirale dressee, 0-1-2-4
            case 0x004198B0: EntityHandler_004198B0(record); return;   // la meme, 5-6-8-10
            case 0x00417B10: EntityHandler_00417B10(record); return;   // le scintillement double, 0-1-2-4
            case 0x00419720: EntityHandler_00419720(record); return;   // le meme, 5-6-8-10
            case 0x00417770: EntityHandler_00417770(record); return;   // l'orbite clignotante, 0-1-2-4
            case 0x00419380: EntityHandler_00419380(record); return;   // la meme, 5-6-8-10
            case 0x00417420: EntityHandler_00417420(record); return;   // le follet invoque, 0-1-2-4
            case 0x00419030: EntityHandler_00419030(record); return;   // le meme, 5-6-8-10
            case 0x00418120: TickDimmingEmber_00418120(record); return;   // la braise
            case 0x0041C0E0: TickOpeningVolute_0041C0E0(record); return;  // la volute qui s'ouvre
            case 0x00416920: TickDriftingShard_00416920(record); return;  // l'eclat en ligne droite
            case 0x0041B7C0: TickTwoFacedPlume_0041B7C0(record); return;  // le panache a deux visages
            case 0x00418AF0: TickSlantedPuff_00418AF0(record); return;    // la bouffee de biais

            // Combat/Projectiles.cs — les deux qui frappent.
            case 0x00416C40: TickDescendingBurnMat_00416C40(record); return;
            case 0x004850C0: TickGrowingBolt_004850C0(record); return;

            // Handlers/OldTower.cs — les sept decors de 0x0056adc8 decompiles dans Blaze.c.
            case 0x0045BF20: TickBreathingBrazier_0045BF20(record); return;   // le brasier qui bat
            case 0x0045AA90: TickTiltingHatch_0045AA90(record); return;       // la trappe qui bascule
            case 0x0045AD20: TickTwinEmberJets_0045AD20(record); return;      // les deux jets
            case 0x0045AE90: TickPushableBlock_0045AE90(record); return;      // le bloc pousse
            case 0x0045B3B0: TickSlammingPiston_0045B3B0(record); return;     // le piston
            case 0x0045B7E0: TickSinkingPillar_0045B7E0(record); return;      // le pilier
            case 0x0045FBA0: TickFreezingVeil_0045FBA0(record); return;       // le voile qui fige

            // ====================================================================================
            // LA VIEILLE TOUR, categorie C — LE SOLDE (lot 3). Avec ces seize, la categorie C de la
            // zone 4 est vide : il n'y reste que D (4) et les seize routines d'intelligence.
            // ====================================================================================

            // Handlers/OldTower.cs — les dix de 0x0056adc8.
            case 0x0045A4D0: TickLatchPlate_0045A4D0(record); return;         // le plateau a verrou
            case 0x0045B540: TickCollapsingWall_0045B540(record); return;     // le mur qui se derobe
            case 0x0045E300: TickBytePushableBlock_0045E300(record); return;  // le bloc a six octets
            case 0x0045DD00: EntityHandler_0045DD00(record); return;          // la bouche, 5-6-8-10
            case 0x0045FD40: EntityHandler_0045FD40(record); return;          // la meme, 3-7-9
            case 0x0045B030: TickGuardedSteamVent_0045B030(record); return;   // la bouche a portee
            case 0x0045E860: TickGuardroomMaster_0045E860(record); return;    // le maitre de salle
            case 0x0045D750: TickScythePendulum_0045D750(record); return;     // le pendule
            case 0x0045BB10: TickCameraLift_0045BB10(record); return;         // la plateforme-camera
            case 0x0045F7E0: TickSaggingWalkway_0045F7E0(record); return;     // la passerelle
            case 0x0045C870: EntityHandler_0045C870(record); return;          // le wagonnet qui regrandit a l arret
            case 0x0045ECC0: EntityHandler_0045ECC0(record); return;          // le wagonnet qui regrandit toujours

            // ====================================================================================
            // LE LABYRINTHE DES MORTS, lot 1 — des corps deja ecrits, a la planche ou au bloc pres.
            // Effects/LabyrinthParticles.cs, Cutscene/LabyrinthScenes.cs, Effects/AreaExitFlash.cs.
            // ====================================================================================
            case 0x00460F30: EntityHandler_00452A90(record); return;          // la lueur, copie stricte (0-3)
            case 0x004635B0: EntityHandler_00452A90(record); return;          // la lueur, copie stricte (4, 9)
            case 0x0041E250: EntityHandler_0041E250(record); return;          // le grain
            case 0x00421240: EntityHandler_00421240(record); return;
            case 0x0041E070: EntityHandler_0041E070(record); return;          // l escarbille lourde
            case 0x00421060: EntityHandler_00421060(record); return;
            case 0x0041D920: EntityHandler_0041D920(record); return;          // l eclat en ligne droite
            case 0x00420910: EntityHandler_00420910(record); return;
            case 0x0041EAF0: EntityHandler_0041EAF0(record); return;          // l eclat projete
            case 0x00421AE0: EntityHandler_00421AE0(record); return;
            case 0x0041E430: EntityHandler_0041E430(record); return;          // la gerbe serree
            case 0x00421420: EntityHandler_00421420(record); return;
            case 0x004232A0: EntityHandler_004232A0(record); return;          // la fumee violette
            case 0x00423D40: EntityHandler_00423D40(record); return;          // les amorces de la gerbe
            case 0x00424490: EntityHandler_00424490(record); return;
            case 0x004236F0: EntityHandler_004236F0(record); return;          // les amorces de la scene parlante
            case 0x00424460: EntityHandler_00424460(record); return;
            case 0x00423240: EntityHandler_00423240(record); return;          // l amorce du flash de sortie
            case 0x0045C2E0: TickWardingGuardroomMaster_0045C2E0(record); return; // le maitre qui rend la garde invulnerable

            // Cutscene/OldTowerScenes.cs — les trois fins de scene de 0x0056b0c8.
            case 0x0041BB20: EntityHandler_0041BB20(record); return;          // la sortie breve
            case 0x0041AC80: EntityHandler_0041AC80(record); return;          // l'enlevement
            case 0x0041B460: EntityHandler_0041B460(record); return;          // la chute

            // Effects/OldTowerParticles.cs — les trois de 0x0056b0c8 / 0x0056b1c8 qui ne sont pas
            // des scenes.
            case 0x00416A80: TickHolderHalo_00416A80(record); return;         // le halo du porteur
            case 0x0041BC90: TickFallingMass_0041BC90(record); return;        // la masse qui tombe
            case 0x0041AFA0: TickThreeBeatTrail_0041AFA0(record); return;     // la traine

            case 0x0040B1B0: EntityHandler_0040B1B0(record); return;
            case 0x0040B900: EntityHandler_0040B900(record); return;
            case 0x0040C820: EntityHandler_0040C820(record); return;
            case 0x0040CA80: EntityHandler_0040CA80(record); return;
            case 0x0040CC60: EntityHandler_0040CC60(record); return;
            // La quatrieme amorce du flash de sortie de zone — Effects/AreaExitFlash.cs.
            case 0x00407FE0: EntityHandler_00407FE0(record); return;
            case 0x00411860: EntityHandler_00411860(record); return;
            case 0x00414CD0: EntityHandler_00414CD0(record); return;
            case 0x004103D0: EntityHandler_004103D0(record); return;
            case 0x00413840: EntityHandler_00413840(record); return;
            case 0x00412240: EntityHandler_00412240(record); return;
            case 0x00412720: EntityHandler_00412720(record); return;
            case 0x004156B0: EntityHandler_004156B0(record); return;
            case 0x00415C50: EntityHandler_00415C50(record); return;
            case 0x00411F80: EntityHandler_00411F80(record); return;
            case 0x004153F0: EntityHandler_004153F0(record); return;
            case 0x00412460: EntityHandler_00412460(record); return;
            case 0x00415990: EntityHandler_00415990(record); return;
            case 0x0040FBB0: EntityHandler_0040FBB0(record); return;
            case 0x00412D10: EntityHandler_00412D10(record); return;
            case 0x00416300: EntityHandler_00416300(record); return;
            case 0x00483920: EntityHandler_00483920(record); return;
            case 0x00415D40: EntityHandler_00415D40(record); return;
            case 0x004157A0: EntityHandler_004157A0(record); return;
            case 0x0040F260: EntityHandler_0040F260(record); return;
            case 0x00458DE0: EntityHandler_00458DE0(record); return;
            case 0x00459B20: EntityHandler_00459B20(record); return;
            case 0x00458E90: EntityHandler_00458E90(record); return;

            // L'EMBARCATION que le groupe pilote a la pagaie, sous-zones 6 a 8 de la foret.
            // 2088 octets absents de Blaze.c ; voir le pave devant TickPartyRaft_00458200.
            case 0x00458200: TickPartyRaft_00458200(record); return;

            // Les deux scenes cinema de la foret et leurs deux amorces : un corps chacune,
            // parametre par l'enregistrement 0 ou 1 du bloc prive 0x00571420.
            case 0x00410820: EntityHandler_00410820(record); return;
            case 0x00413C90: EntityHandler_00413C90(record); return;
            case 0x00411240: EntityHandler_00411240(record); return;
            case 0x004146B0: EntityHandler_004146B0(record); return;
            case 0x0040EF40: EntityHandler_0040EF40(record); return;
            case 0x004101D0: EntityHandler_004101D0(record); return;
            case 0x00413330: EntityHandler_00413330(record); return;
            case 0x00483760: EntityHandler_00483760(record); return;
            case 0x00459840: EntityHandler_00459840(record); return;
            case 0x00459FA0: EntityHandler_00459FA0(record); return;

            // ====================================================================================
            // ZONE 6 — LE CHATEAU DE L'IMMORTEL, PALIER 1 (2026-09-18) : deja ecrit, ou presque.
            //
            // Le chateau se lit en TROIS groupes de sous-zones — 0 a 4, 6 a 10, puis 5 et 11 — et
            // le compilateur a emis chaque corps une fois par groupe. Presque tout ce palier est
            // donc un renvoi vers un corps de la mine, de la foret ou de la tour, a une immediate
            // pres.
            // ====================================================================================

            // La spirale triple (creneau 61), la bulle (63) et le grain (62), un jeu par groupe :
            // neuf adresses pour trois corps deja ecrits, a la planche pres.
            // Effects/ImmortalCastleParticles.cs.
            case 0x00426410: EntityHandler_00426410(record); return;
            case 0x004289F0: EntityHandler_004289F0(record); return;
            case 0x0042BF70: EntityHandler_0042BF70(record); return;
            case 0x004270C0: EntityHandler_004270C0(record); return;
            case 0x00429930: EntityHandler_00429930(record); return;
            case 0x0042E060: EntityHandler_0042E060(record); return;
            case 0x00426FD0: EntityHandler_00426FD0(record); return;
            case 0x00429840: EntityHandler_00429840(record); return;
            case 0x0042DF70: EntityHandler_0042DF70(record); return;

            // La gerbe a cinq phases, creneaux 10 et 11 du bloc des sous-zones 5 et 11.
            case 0x0042C9E0: EntityHandler_0042C9E0(record); return;
            case 0x0042D130: EntityHandler_0042D130(record); return;

            // Le flash blanc de sortie de zone : le chateau emet SON amorce trois fois, une par
            // groupe, et les trois sont identiques. Effects/AreaExitFlash.cs.
            case 0x004263B0: EntityHandler_004263B0(record); return;
            case 0x00428990: EntityHandler_00428990(record); return;
            case 0x0042BF10: EntityHandler_0042BF10(record); return;

            // Les deux amorces de la scene parlante, enregistrement prive 0x005714e0 —
            // le suivant de celui du Labyrinthe au pas de 0x30. Cutscene/ImmortalCastleScenes.cs.
            case 0x0042C3C0: EntityHandler_0042C3C0(record); return;
            case 0x0042D100: EntityHandler_0042D100(record); return;

            // Le decor : quatre corps, quatre variantes d'un corps ecrit ailleurs.
            // Handlers/ImmortalCastle.cs.
            case 0x00463C80: EntityHandler_00463C80(record); return;
            case 0x00465180: EntityHandler_00465180(record); return;
            case 0x00466A10: EntityHandler_00466A10(record); return;
            case 0x00463E80: EntityHandler_00463E80(record); return;
            case 0x004657B0: EntityHandler_004657B0(record); return;

            // 0x00464B30 et 0x00466470 sont des DOUBLONS de 0x00452A90 (le halo vacillant de la
            // mine) : 47 instructions comparees une a une, zero ecart, et les trois cibles d'appel
            // resolvent au meme endroit. Le compilateur a emis le meme corps trois fois. Deux
            // renvois suffisent — voir la regle « verifier s'il y a un doublon avant de
            // transcrire » dans CLAUDE.md.
            case 0x00464B30: EntityHandler_00452A90(record); return;
            case 0x00466470: EntityHandler_00452A90(record); return;

            // Les cinq corps « presque ecrits » du palier 1, plus les DEUX gestionnaires de la
            // GROTTE SCELLEE (zone 10) qu'ils ferment en prime — meme corps, autre planche.
            // Effects/ImmortalCastleParticles.cs, sauf 0x00439170 (Effects/LabyrinthParticles.cs,
            // avec son parent du Labyrinthe).
            case 0x00426940: EntityHandler_00426940(record); return;
            case 0x00428E40: EntityHandler_00428E40(record); return;
            case 0x0042DB10: EntityHandler_0042DB10(record); return;
            case 0x00442C20: EntityHandler_00442C20(record); return;
            case 0x0042B430: EntityHandler_0042B430(record); return;
            case 0x00439170: EntityHandler_00439170(record); return;
            case 0x00429BE0: EntityHandler_00429BE0(record); return;
            case 0x0042A540: EntityHandler_0042A540(record); return;


            // ====================================================================================
            // ZONE 6 — LE CHATEAU DE L'IMMORTEL, PALIER 2 (2026-09-18) : les 62 corps « simples »,
            // 49 formes distinctes, et AUCUNE n'avait de parent deja ecrit dans le depot.
            // ====================================================================================

            // Les effets des tables 0x0056b0c8 / b1c8 / b2c8 — Effects/ImmortalCastleParticles.cs,
            // plus 0x00439B00, le meme corps dans la grotte scellee.
            case 0x00426C40: EntityHandler_00426C40(record); return;
            case 0x004293F0: EntityHandler_004293F0(record); return;
            case 0x00426D20: EntityHandler_00426D20(record); return;
            case 0x00426E20: EntityHandler_00426E20(record); return;
            case 0x00429680: EntityHandler_00429680(record); return;
            case 0x00427C10: EntityHandler_00427C10(record); return;
            case 0x0042B0C0: EntityHandler_0042B0C0(record); return;
            case 0x00439B00: EntityHandler_00439B00(record); return;
            case 0x00429270: EntityHandler_00429270(record); return;
            case 0x0042A350: EntityHandler_0042A350(record); return;
            case 0x00429330: EntityHandler_00429330(record); return;

            // Les quatre qui LANCENT UN SCRIPT — Handlers/ImmortalCastleMecanismes.cs.
            case 0x00426860: EntityHandler_00426860(record); return;
            case 0x00426B10: EntityHandler_00426B10(record); return;
            case 0x004647F0: EntityHandler_004647F0(record); return;
            case 0x00466130: EntityHandler_00466130(record); return;

            // Le decor de la table 0x0056adc8 — Handlers/ImmortalCastle.cs. Quatorze paires
            // miroir (bloc 0x00558e28 pour les sous-zones 0 a 4, 0x005594a8 pour 6 a 10, au meme
            // numero de creneau), dont la FAMILLE DE SIX des creneaux 22, 23 et 24.
            case 0x00463770: EntityHandler_00463770(record); return;
            case 0x00463A10: EntityHandler_00463A10(record); return;
            case 0x00463AE0: EntityHandler_00463AE0(record); return;
            case 0x00463B30: EntityHandler_00463B30(record); return;
            case 0x00463CA0: EntityHandler_00463CA0(record); return;
            case 0x00463D90: EntityHandler_00463D90(record); return;
            case 0x004642C0: EntityHandler_004642C0(record); return;
            case 0x004643B0: EntityHandler_004643B0(record); return;
            case 0x00464440: EntityHandler_00464440(record); return;
            case 0x00464660: EntityHandler_00464660(record); return;
            case 0x00464900: EntityHandler_00464900(record); return;
            case 0x00464A10: EntityHandler_00464A10(record); return;
            case 0x00464AC0: EntityHandler_00464AC0(record); return;
            case 0x00464BC0: EntityHandler_00464BC0(record); return;
            case 0x00464C30: EntityHandler_00464C30(record); return;
            case 0x00464CF0: EntityHandler_00464CF0(record); return;
            case 0x00464DB0: EntityHandler_00464DB0(record); return;
            case 0x00465020: EntityHandler_00465020(record); return;
            case 0x00465070: EntityHandler_00465070(record); return;
            case 0x00465150: EntityHandler_00465150(record); return;
            case 0x00465220: EntityHandler_00465220(record); return;
            case 0x00465290: EntityHandler_00465290(record); return;
            case 0x00465330: EntityHandler_00465330(record); return;
            case 0x004653F0: EntityHandler_004653F0(record); return;
            case 0x004654C0: EntityHandler_004654C0(record); return;
            case 0x00465610: EntityHandler_00465610(record); return;
            case 0x00465640: EntityHandler_00465640(record); return;
            case 0x00465670: EntityHandler_00465670(record); return;
            case 0x004656C0: EntityHandler_004656C0(record); return;
            case 0x00465BF0: EntityHandler_00465BF0(record); return;
            case 0x00465CE0: EntityHandler_00465CE0(record); return;
            case 0x00465D70: EntityHandler_00465D70(record); return;
            case 0x00465FA0: EntityHandler_00465FA0(record); return;
            case 0x00466240: EntityHandler_00466240(record); return;

            // ====================================================================================
            // ZONE 6 — LE CHATEAU DE L'IMMORTEL, PALIER 3 (2026-09-18) : les « C ».
            // ====================================================================================

            // Le decor de 0x0056adc8 — Handlers/ImmortalCastle.cs. Deux jumeaux enrichis de corps
            // deja ecrits (creneaux 45 et 26), et deux paires miroir identiques.
            case 0x00463850: EntityHandler_00463850(record); return;
            case 0x00464E70: EntityHandler_00464E70(record); return;
            case 0x004646A0: EntityHandler_004646A0(record); return;
            case 0x00465FE0: EntityHandler_00465FE0(record); return;
            case 0x004644F0: EntityHandler_004644F0(record); return;
            case 0x00465E30: EntityHandler_00465E30(record); return;

            // Les effets de 0x0056b0c8 / b1c8 / b2c8 — Effects/ImmortalCastleParticles.cs. Les
            // quatre nuages a effet 32 sont StepStatusCloud (Labyrinthe) plus une gerbe propre au
            // chateau ; 0x00442AC0 est le jumeau de 0x0042D9B0 dans la grotte scellee.
            case 0x0042A920: EntityHandler_0042A920(record); return;
            case 0x0042B190: EntityHandler_0042B190(record); return;
            case 0x004271B0: EntityHandler_004271B0(record); return;
            case 0x004255F0: EntityHandler_004255F0(record); return;
            case 0x00426E80: EntityHandler_00426E80(record); return;
            case 0x004296F0: EntityHandler_004296F0(record); return;
            case 0x004294D0: EntityHandler_004294D0(record); return;
            case 0x00429010: EntityHandler_00429010(record); return;
            case 0x0042D9B0: EntityHandler_0042D9B0(record); return;
            case 0x00442AC0: EntityHandler_00442AC0(record); return;
            case 0x0042D350: EntityHandler_0042D350(record); return;
            case 0x0042D770: EntityHandler_0042D770(record); return;

            // Trois scenes des sous-zones 5 et 11 — Cutscene/ImmortalCastleScenes.cs.
            case 0x0042D170: EntityHandler_0042D170(record); return;
            case 0x0042D4E0: EntityHandler_0042D4E0(record); return;
            case 0x0042DC80: EntityHandler_0042DC80(record); return;

            // ====================================================================================
            // ZONE 6 — LE CHATEAU DE L'IMMORTEL, PALIER 4 (2026-09-18) : les « D ».
            // ====================================================================================

            // La famille a trois vies (feu follet, projectile qui rebondit, eclair rouge), les
            // semis qui chantent (TickCloudSeed plus des sons) et la nappe devant le proprietaire
            // (TickOwnerFrontSweep a la mise en place du chateau) — Effects/ImmortalCastleParticles.cs.
            case 0x00425E50: EntityHandler_00425E50(record); return;
            case 0x00428420: EntityHandler_00428420(record); return;
            case 0x0042B9A0: EntityHandler_0042B9A0(record); return;
            case 0x00425750: EntityHandler_00425750(record); return;
            case 0x00427CE0: EntityHandler_00427CE0(record); return;
            case 0x004274C0: EntityHandler_004274C0(record); return;
            case 0x0042ABF0: EntityHandler_0042ABF0(record); return;

            // Le battant a trois positions du creneau 29 — Handlers/ImmortalCastle.cs.
            case 0x00463EF0: EntityHandler_00463EF0(record); return;
            case 0x00465820: EntityHandler_00465820(record); return;
            case 0x00466350: EntityHandler_00466350(record); return;
            case 0x00466400: EntityHandler_00466400(record); return;
            case 0x00466500: EntityHandler_00466500(record); return;
            case 0x004665C0: EntityHandler_004665C0(record); return;
            case 0x00466680: EntityHandler_00466680(record); return;
            case 0x00466740: EntityHandler_00466740(record); return;
            case 0x00466840: EntityHandler_00466840(record); return;
            case 0x00466890: EntityHandler_00466890(record); return;
            case 0x00466970: EntityHandler_00466970(record); return;
            case 0x00466AA0: EntityHandler_00466AA0(record); return;

            default:
                // Les seize premiers octets de l'enregistrement partent avec la lacune. Un
                // gestionnaire manquant se lit toujours avec sa donnee : c'est ce qui permet de
                // distinguer une entite legitime d'un enregistrement mal rempli sans instrumenter
                // le jeu pour un seul essai.
                string detail = $"{where} [{DumpRecordHead(record)}]";

                // ECHAFAUDAGE : voir s_AiStub. Sans le drapeau, rien ne change — la lacune leve.
                if (s_AiStub)
                {
                    NoteMissingHandler(target, detail);
                    return;
                }

                Res.CallEntityHandler(target, detail);
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
    /// Note une lacune au lieu de lever : une ligne a la <b>premiere</b> rencontre du couple
    /// (gestionnaire, sous-zone), un compte pour le recapitulatif. Une ligne par image serait
    /// illisible — un gestionnaire manquant est rappele soixante fois par seconde.
    ///
    /// <para>
    /// La ligne part sur la sortie d'erreur, que <c>playtest.ps1</c> redirige deja vers
    /// <c>scratchpad/playtest-&lt;horodatage&gt;.log</c>. C'est voulu : le script <b>tue</b> le
    /// processus a la fin du run, donc le recapitulatif de <see cref="PrintMissingHandlerRecap"/>
    /// ne s'imprime que sur une sortie propre. Les lignes ecrites au fil de l'eau, elles, sont
    /// dans le journal quoi qu'il arrive.
    /// </para>
    /// </summary>
    private static void NoteMissingHandler(uint target, string detail)
    {
        (uint Target, int Zone, int SubArea) key =
            (target, Res.s_Unknown_005de528 & 0xff, s_Unknown_005de52c);

        if (s_MissingHandlers.TryGetValue(key, out int seen))
        {
            s_MissingHandlers[key] = seen + 1;
            return;
        }

        s_MissingHandlers[key] = 1;

        Console.Error.WriteLine(
            $"[LACUNE] 0x{target:X6} zone={key.Zone} sous-zone={key.SubArea} : {detail}");

        if (!s_MissingHandlerRecapArmed)
        {
            s_MissingHandlerRecapArmed = true;
            AppDomain.CurrentDomain.ProcessExit += (_, _) => PrintMissingHandlerRecap();
        }
    }

    /// <summary>
    /// Le recapitulatif de fin de partie : les couples (gestionnaire, sous-zone) rencontres, du
    /// plus sollicite au moins sollicite. C'est la liste de courses — l'ordre de transcription se
    /// lit dessus, un gestionnaire appele des milliers de fois etant une bete que le joueur a sous
    /// les yeux, la ou un appel unique peut n'etre qu'un passage.
    ///
    /// <para>Ne s'imprime que si le processus se termine proprement ; voir
    /// <see cref="NoteMissingHandler"/>.</para>
    /// </summary>
    private static void PrintMissingHandlerRecap()
    {
        if (s_MissingHandlers.Count == 0) return;

        var lines = new System.Collections.Generic.List<
            System.Collections.Generic.KeyValuePair<(uint Target, int Zone, int SubArea), int>>(
                s_MissingHandlers);

        lines.Sort((a, b) => b.Value.CompareTo(a.Value));

        Console.Error.WriteLine(
            $"[LACUNE] recapitulatif : {lines.Count} couple(s) (gestionnaire, sous-zone) " +
            "rencontre(s) et non transcrit(s)");

        foreach (var line in lines)
        {
            Console.Error.WriteLine(
                $"[LACUNE]   0x{line.Key.Target:X6} zone={line.Key.Zone} " +
                $"sous-zone={line.Key.SubArea} : {line.Value} appel(s)");
        }
    }
}
