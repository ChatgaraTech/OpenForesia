// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES TROIS TABLES STATIQUES — 0x00551c30, 0x00551f30, 0x00552230 — et leur appelant indexe.
//
// Ce sont les tables de sauts que le binaire porte en dur, par opposition aux sept tables que
// `InstallEntityVTables` (Engine/Resources.cs) recopie au chargement de zone. Un sous-type y
// choisit une adresse ; `CallIndexedEntityHandler` la passe au carrefour
// (`EntityHandlerDispatch.cs`), qui la convertit en methode.
//
// Soixante-neuf des 192 creneaux de 0x00551c30 pointent sur `0x0047ADC0`, qui ETEINT l'entite :
// un sous-type absent de la table n'est pas ignore, il est tue. C'est ce qui rend une lacune
// visible en jeu plutot que silencieuse.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: <c>PTR_LAB_00551c30</c> — <b>la table statique des gestionnaires d'entite de la
    /// passe <c>FUN_0047aa70</c></b>, celle que le repartiteur emprunte pour tout sous-type
    /// <c>&lt; 0xC0</c>. Cent quatre-vingt-douze entrees, relevees verbatim dans
    /// <c>Legacy/Binaries/Blaze.exe</c> (<c>.data</c>, <c>at 0x00551c30 768</c>).
    ///
    /// <para>
    /// Ghidra n'a pas reconnu ses cibles comme des fonctions — d'ou le prefixe <c>PTR_LAB_</c> —
    /// mais cela ne veut <b>pas</b> dire qu'elles manquent a <c>Blaze.c</c> : la plupart y sont,
    /// atteintes par d'autres appelants. Elles se repartissent en deux bandes,
    /// <c>0x0044xxxx</c> et <c>0x00475xxx</c>..<c>0x00482xxx</c>, et comptent <b>124 adresses
    /// distinctes</b>.
    /// </para>
    /// <para>
    /// <b>Soixante-neuf des 192 creneaux pointent sur <c>0x0047ADC0</c></b>, et ce n'est pas une
    /// souche vide : ses trois instructions lisent l'enregistrement et y ecrivent zero, ce qui
    /// <b>eteint l'entite</b>. C'est le creneau « sous-type inconnu » de cette table, et il fait
    /// quelque chose. Le transcrire fait passer 69 creneaux sur 192 d'un plantage a un comportement
    /// juste.
    /// </para>
    /// <para>
    /// Restent <b>123 corps reels</b>, dont <b>63 sont decompiles dans <c>Blaze.c</c></b> et
    /// relevent de la transcription ordinaire ; les <b>60</b> autres demandent le desassembleur.
    /// C'est un mur, pas une lacune : il se franchit un corps a la fois, et la table ci-dessous est
    /// la carte qui dit lequel.
    /// </para>
    /// <para>
    /// Ses deux sœurs — <see cref="s_StaticEntityHandlers_00551f30"/> et
    /// <see cref="s_StaticEntityHandlers_00552230"/> — sont relevees aussi. Les trois se suivent
    /// sans trou de <c>0x00551c30</c> a <c>0x0055252f</c>, et sont <b>disjointes deux a deux</b> :
    /// leur seule cible commune est la souche.
    /// </para>
    /// </summary>
    private static readonly uint[] s_StaticEntityHandlers_00551c30 =
    [
        0x0047ADD0, 0x0047B0B0, 0x0047B460, 0x0047B7A0, 0x0047BB50, 0x0047BF00, 0x0047C3A0, 0x0047C660,
        0x0047C910, 0x0047CE40, 0x0047CFF0, 0x0047D330, 0x0047D600, 0x0047DB50, 0x0047DF70, 0x0047E260,
        0x0047E590, 0x0047EAE0, 0x0047F070, 0x0047F2B0, 0x0047F570, 0x0047FB80, 0x0047FE60, 0x00480600,
        0x00480B90, 0x00481670, 0x004819D0, 0x00482370, 0x004828F0, 0x0047ADC0, 0x0044F060, 0x00447DF0,
        0x00448060, 0x00448300, 0x0047ADC0, 0x004491F0, 0x0047ADC0, 0x00449590, 0x004498B0, 0x00449C30,
        0x00449F90, 0x0044A4B0, 0x0044A7A0, 0x0044AE90, 0x0044B510, 0x0044F930, 0x0044D370, 0x0044B7B0,
        0x0044BAF0, 0x0044BF00, 0x0044C3A0, 0x0044C8A0, 0x0044CD00, 0x0044DC60, 0x00447A90, 0x004484F0,
        0x00448EF0, 0x00447280, 0x00445950, 0x00447630, 0x00447170, 0x00446F30, 0x00446DD0, 0x00446A90,
        0x00446790, 0x00446900, 0x00446410, 0x004462B0, 0x00445F60, 0x00446200, 0x004760C0, 0x004761D0,
        0x00445E70, 0x0047A100, 0x004769A0, 0x004477D0, 0x00445CF0, 0x00446170, 0x00475CC0, 0x0047A5D0,
        0x00403C10, 0x00403E80, 0x004041C0, 0x00404460, 0x00402FC0, 0x00404920, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x00403710, 0x00476350, 0x00476500, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x00476950, 0x00476970, 0x0047E6F0, 0x004824F0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x00476410, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x00476C70, 0x00476FD0, 0x00477360, 0x004774C0, 0x00477660, 0x004777A0, 0x004778E0, 0x00477A20,
        0x00477D20, 0x004780B0, 0x004785F0, 0x004786C0, 0x00478C20, 0x00478D10, 0x00478DF0, 0x004790C0,
        0x00479F70, 0x00479300, 0x00479DE0, 0x00479C50, 0x004754C0, 0x004755F0, 0x004758C0, 0x00475A30,
        0x00475B90, 0x00475760, 0x00476770, 0x00476730, 0x00475170, 0x004753D0, 0x00475EA0, 0x00475D40,
    ];

    /// <summary>
    /// Ghidra: <c>0x0047adc0</c> — <b>le creneau « sous-type inconnu »</b> de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/>, et <b>le seul de ses 124 corps qui soit
    /// transcrit</b>. Trois instructions :
    ///
    /// <code>
    /// 0047adc0  mov   eax,[esp+4]        ; l'enregistrement
    /// 0047adc4  mov   dword [eax],0      ; son mot d'etat part a zero
    /// 0047adca  ret
    /// </code>
    ///
    /// <para>
    /// Effacer le mot d'etat eteint le bit <c>0x80000000</c>, ce que la passe relit juste apres
    /// pour rendre le creneau au pool. C'est donc un <b>desengendrement</b>, et non un no-op :
    /// l'entite disparait au pas suivant. Ne pas le confondre avec la souche <c>0x00401FA0</c> de
    /// l'autre table, qui est un <c>ret</c> nu.
    /// </para>
    /// </summary>
    private static void DespawnEntity_0047ADC0(byte* record) => *(uint*)record = 0;

    /// <summary>
    /// Ghidra: <c>PTR_LAB_00551f30</c> — <b>la table statique de la passe <c>FUN_0047ab70</c></b>,
    /// celle des 0x80 enregistrements de <c>DAT_005edee0</c>. Cent quatre-vingt-douze entrees,
    /// relevees verbatim dans <c>Legacy/Binaries/Blaze.exe</c> (<c>.data</c>,
    /// <c>at 0x00551f30 768</c>). Elle commence exactement ou
    /// <see cref="s_StaticEntityHandlers_00551c30"/> s'acheve : les trois tables de 0x300 octets
    /// se suivent sans trou.
    ///
    /// <para>
    /// <b>Les deux tables sont disjointes.</b> Elles n'ont qu'une seule cible en commun — la souche
    /// <c>0x0047ADC0</c> — et pas un seul gestionnaire reel. Ce ne sont donc pas deux vues d'un
    /// meme jeu de comportements avec des indices differents : ce sont deux familles d'entites
    /// distinctes, ce que confirme la passe elle-meme (celle-ci raccroche ses entites a la grille
    /// spatiale, <see cref="TickEntityTable_005ff400"/> non).
    /// </para>
    /// <para>
    /// <b>Cent neuf des 192 creneaux</b> pointent sur <see cref="DespawnEntity_0047ADC0"/>, contre
    /// 69 dans la table sœur — plus de la moitie de l'espace d'indices est vide et eteint ce qui
    /// s'y presente. Restent <b>83 corps reels</b>, dont <b>26 decompiles dans <c>Blaze.c</c></b> ;
    /// les 57 autres demandent le desassembleur — dont <c>0x004765D0</c>, fait le 2026-08-28.
    /// </para>
    /// </summary>
    private static readonly uint[] s_StaticEntityHandlers_00551f30 =
    [
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x00480080, 0x00480490, 0x00481D50, 0x00481E30,
        0x004821A0, 0x00482300, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x004478F0, 0x0047A270, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x00403A90, 0x004034A0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x00475BE0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x00481720, 0x0047AE80, 0x0047ADC0, 0x0047B6B0, 0x0047BC40,
        0x0047BD00, 0x0047B850, 0x0047C1E0, 0x0047C450, 0x0047C830, 0x0047CA50, 0x0047E370, 0x0047EA80,
        0x0047CF30, 0x0047D670, 0x0047D950, 0x00480A20, 0x00480A70, 0x0047D470, 0x0047E150, 0x0047E200,
        0x004828B0, 0x00482DE0, 0x00482ED0, 0x0047D270, 0x00477DA0, 0x00478FC0, 0x004770A0, 0x00479490,
        0x00479250, 0x00477420, 0x00479030, 0x0047FE40, 0x0047F290, 0x0047F400, 0x0047F470, 0x00480E40,
        0x00481250, 0x0047EC30, 0x0047EF60, 0x00479B90, 0x00449A70, 0x0044A6A0, 0x0044A880, 0x0044AF70,
        0x0044B6F0, 0x0044BA00, 0x0044BE40, 0x0044C110, 0x0044C730, 0x0044C800, 0x0044CAC0, 0x0044CBD0,
        0x0044CF30, 0x0044D030, 0x0044D8E0, 0x0044DB30, 0x0044DED0, 0x0044EA60, 0x0044F290, 0x0044F600,
        0x00448250, 0x00447D50, 0x004487F0, 0x00448C40, 0x00447380, 0x0044A080, 0x00446E20, 0x00446620,
        0x0047ADC0, 0x004501B0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x004767E0, 0x0047ADC0, 0x004751E0, 0x004765D0, 0x0047ADC0, 0x0047ADC0,
    ];

    /// <summary>
    /// Ghidra: <c>PTR_LAB_00552230</c> — <b>la table statique de la passe <c>FUN_0047acf0</c></b>,
    /// celle des 0x80 enregistrements de <c>DAT_005f2d40</c>, et la <b>troisieme et derniere</b> du
    /// bloc contigu. Cent quatre-vingt-douze entrees, relevees verbatim dans
    /// <c>Legacy/Binaries/Blaze.exe</c> (<c>.data</c>, <c>at 0x00552230 768</c>).
    ///
    /// <para>
    /// <b>La plus creuse des trois</b> : 125 de ses 192 creneaux eteignent ce qui s'y presente,
    /// contre 109 et 69 pour ses sœurs. Restent <b>67 corps reels</b>, dont <b>32 decompiles dans
    /// <c>Blaze.c</c></b> ; les 35 autres demandent le desassembleur. Elle est disjointe des deux
    /// autres, comme elles le sont entre elles : une seule cible commune, la souche.
    /// </para>
    /// <para>
    /// <b>Deux des trois tables ont desormais un creneau atteint par un chemin reconstruit</b> —
    /// celle-ci et sa sœur 0x00551f30, dont le sous-type 0xbd est l’aureole d’incantation
    /// (<see cref="TickCastAura_004765D0"/>), relevee le 2026-08-28.
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> engendre dans <c>0x005f2d40</c> en posant son
    /// sous-type en <c>+0x05</c>, l'octet meme que cette passe aiguille ; l'effet de dispersion du
    /// groupe de <see cref="EvaluatePartyDispersal"/> y entre avec <c>0x9d</c>, ce qui designe
    /// <c>0x00445860</c>, transcrit depuis (<see cref="ShowEntityLabel_00445860"/>).
    /// </para>
    /// </summary>
    private static readonly uint[] s_StaticEntityHandlers_00552230 =
    [
        0x00482FA0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x004769F0, 0x00475400, 0x0047A950, 0x0047A730, 0x0047A7D0, 0x0047A880, 0x00476140, 0x00476290,
        0x0047ADC0, 0x0047ADC0, 0x00403D60, 0x00404000, 0x00404310, 0x004045F0, 0x00404730, 0x00404A50,
        0x00403250, 0x0047ADC0, 0x0047ADC0, 0x004038F0, 0x004755C0, 0x00475730, 0x004757A0, 0x00475910,
        0x00475A80, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047B050, 0x0047B150,
        0x0047B340, 0x0047B3A0, 0x0047B750, 0x0047BB10, 0x0047BDF0, 0x0047C2C0, 0x0047DA50, 0x0047DE10,
        0x00482EF0, 0x0047FB10, 0x0047D2A0, 0x00476CF0, 0x00477A90, 0x00477E40, 0x00477600, 0x00479BC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x00449760, 0x00449E30, 0x0044A320, 0x0044B950,
        0x0044BCC0, 0x0044C260, 0x0047ADC0, 0x0044D170, 0x0044D9B0, 0x0044EF40, 0x0044F750, 0x00448E20,
        0x004474C0, 0x004459E0, 0x00447050, 0x00446C90, 0x00446060, 0x00445860, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x004500B0, 0x0044FF40, 0x0044FE00,
        0x0044FC90, 0x0044FB10, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x0047ADC0,
        0x0047ADC0, 0x0047ADC0, 0x0047ADC0, 0x004768A0, 0x004752F0, 0x00476320, 0x00475FC0, 0x00475F60,
    ];

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
            // Ghidra ecrit l'appel `(*(code *)…)()`, sans argument, et ce depot l'a longtemps
            // repete. C'est faux, et le binaire le dit en une ligne : le `push esi` est fait
            // AVANT le branchement, donc les deux tables recoivent le meme unique argument —
            // l'enregistrement — et le `add esp,4` commun les nettoie toutes deux. La branche
            // basse a exactement la convention de la branche haute : __cdecl, un argument.
            //
            //   0047aa8e  push esi                      ; l'enregistrement
            //   0047aa92  cmp  eax,0C0h
            //   0047aa97  jge  0047AAA2
            //   0047aa99  call dword [eax*4+551C30h]    ; table basse
            //   0047aaa2  call dword [eax*4+56ADC8h]    ; table haute
            //   0047aaa9  add  esp,4
            uint[] staticTable = staticTableVa switch
            {
                0x00551c30 => s_StaticEntityHandlers_00551c30,
                0x00551f30 => s_StaticEntityHandlers_00551f30,
                0x00552230 => s_StaticEntityHandlers_00552230,

                // Les trois tables statiques du bloc contigu 0x00551c30..0x0055252f sont relevees.
                // Un quatrieme site d'appel avec une autre table serait une decouverte, pas un
                // oubli : le relever avant de l'ajouter ici.
                _ => throw new NotImplementedException(
                    $"{where} : le gestionnaire d'indice {index} vit dans la table statique " +
                    $"0x{staticTableVa:X6}, qui n'est pas relevee. Les TROIS tables connues " +
                    "(0x00551c30, 0x00551f30, 0x00552230) le sont ; celle-ci est donc une " +
                    "quatrieme, inattendue. La relever avec " +
                    $"`OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{staticTableVa:x8} 768`, " +
                    "puis la materialiser comme les autres. Le gestionnaire prend UN argument, " +
                    "l'enregistrement, en __cdecl."),
            };

            LegacyPlus.Coutures.AvantGestionnaire(record);   // Couture legacy ++ (LP-013)
            DispatchEntityHandler(staticTable[index], record, where);
            LegacyPlus.Coutures.ApresGestionnaire(record);   // Couture legacy ++ (LP-013)
            return;
        }

        LegacyPlus.Coutures.AvantGestionnaire(record);   // Couture legacy ++ (LP-013)
        DispatchEntityHandler(LegacyPlus.Coutures.GestionnaireInstalle(record, installedTableVa, index, Res.ResolveEntityHandler(installedTableVa, index)), record, where);   // Couture legacy ++ (LP-013)
        LegacyPlus.Coutures.ApresGestionnaire(record);   // Couture legacy ++ (LP-013)
    }
}
