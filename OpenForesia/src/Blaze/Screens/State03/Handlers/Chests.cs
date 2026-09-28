// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES COFFRES — leur contenu, son tirage, et l'ouverture qui le montre.
//
// Fichier recolle de DEUX endroits du fichier d'origine (decision D7) : le tirage du contenu et
// l'annonce de l'objet d'un cote, l'animation d'ouverture de l'autre. C'est le meme sujet, et il
// etait separe par trente mille lignes.
//
// UN COFFRE TIRE DANS LA MEME TABLE QU'UN MONSTRE. Le `+0xb4` de la sous-zone sert aux deux ; ce
// qui distingue le coffre, c'est la LIGNE que son relevé de placement lui designe. C'est pourquoi
// **112 objets du jeu n'existent que dans des coffres** : aucune bete ne tire leur ligne.
// Le recensement hors ligne est dans Documentation/Reference/Bestiaire/coffres.csv.
//
// `PickWeightedTableEntry` est le tirage pondere, `GrantChestContents_00530F20` la remise,
// `StepChestOpening` / `ChestDispatchTail` l'animation. Les coffres de CAPACITE, eux, sont dans
// `Handlers/Forest.cs` - ils ont leur propre chaine.
//
// Le piege qu'un coffre peut porter est dans `Effects/TrapScripts.cs`, et son effet dans
// `Combat/EffectTable.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

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

    /// <summary>Ghidra: <c>&amp;DAT_0056aaa0</c> .. <c>&amp;DAT_0056abe8</c> — <b>un seul bloc</b>,
    /// et c'est délibéré. Il porte deux tables voisines relevées dans <c>.data</c> :
    /// <c>0x0056aaa0</c> les réactions par classe (seize dwords) et <c>0x0056aae0</c> les
    /// descripteurs de sprite de la surimpression de dialogue (33 entrées de huit octets, jusqu'à
    /// <c>0x0056abe8</c> où commence une chaîne de caractères).
    ///
    /// <para><b>Pourquoi une seule allocation.</b> Le choix d'icône de
    /// <see cref="s_SpeechIconIndex_0056a851"/> est un octet <b>signé</b> et vaut <c>-1</c> à deux
    /// entrées ; <c>DAT_0056aae0 + (-1) * 8</c> lit donc les huit derniers octets de la table des
    /// réactions. Les séparer en deux allocations aurait fait lire du tas quelconque à la place —
    /// exactement le défaut d'aliasing que <c>ARCHITECTURE.md</c> décrit, dans l'autre sens. Ici
    /// l'entrée <c>-1</c> a une hauteur nulle, donc elle ne dessine rien : c'est vraisemblablement
    /// la façon dont le jeu de 1998 écrit « pas d'icône ».</para>
    ///
    /// <para><b>Ces deux <c>-1</c> sont en fait inatteignables</b>, établi le 2026-08-26 —
    /// l'octet de compte de <see cref="s_MenuLayout_0056a850"/> borne l'index avant eux. L'adjacence
    /// reste néanmoins la bonne façon d'allouer : c'est l'indexation du binaire qu'elle décrit, et
    /// rien ne garantit qu'aucun chemin ne l'atteigne jamais.</para>
    ///
    /// TODO(REVERSE) : le rôle des champs d'un descripteur (<c>u, v, w, h, ?, ?, clut</c>) est
    /// supposé d'après leur forme, pas établi.</summary>
    private static readonly byte* s_IconTables_0056aaa0 = AllocBytes([
        // 0x0056aaa0 — les réactions par classe, seize dwords
        0x08, 0x00, 0x00, 0x00,  0x06, 0x00, 0x12, 0x00,  0x04, 0x00, 0x05, 0x00,  0x03, 0x00, 0x03, 0x00,
        0x05, 0x00, 0x08, 0x00,  0x06, 0x00, 0x09, 0x00,  0x06, 0x00, 0x0b, 0x00,  0x06, 0x00, 0x12, 0x00,
        0x06, 0x00, 0x04, 0x00,  0x06, 0x00, 0x09, 0x00,  0x03, 0x00, 0x00, 0x00,  0x06, 0x00, 0x12, 0x00,
        0x05, 0x00, 0x08, 0x00,  0x07, 0x00, 0x05, 0x00,  0x06, 0x00, 0x04, 0x00,  0x06, 0x00, 0x09, 0x00,
        // 0x0056aae0 — les descripteurs de sprite, 33 entrées de huit octets
        0x00, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,  0x10, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0x20, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,  0x30, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0b,
        0x40, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0b,  0x50, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x08,
        0x60, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x08,  0x70, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0a,
        0x80, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0f,  0x80, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0f,
        0x00, 0x90, 0x18, 0x18, 0x00, 0x00, 0x11, 0x00,  0x90, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xa0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,  0xb0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xc0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,  0xd0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xe0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,  0xf0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xf8, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x07,  0xe0, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0a,
        0x80, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,  0x90, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xa0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,  0xb0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xc0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,  0xd0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xe0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,  0xf0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0x80, 0xe8, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,  0x80, 0xf8, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,
        0x80, 0xf0, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,  0xa8, 0xe8, 0x38, 0x08, 0x00, 0x00, 0x30, 0x02,
        0xa8, 0xf0, 0x38, 0x08, 0x00, 0x00, 0x30, 0x02,
    ]);

    /// <summary>Ghidra: &amp;DAT_0056aaa0 — les seize réactions par classe. Vue sur
    /// <see cref="s_IconTables_0056aaa0"/> ; voir son commentaire pour l'adjacence.</summary>
    private static byte* s_ReactionAnimations_0056aaa0 => s_IconTables_0056aaa0;

    /// <summary>Ghidra: &amp;DAT_0056aae0 — les descripteurs de sprite de la surimpression de
    /// dialogue. <b>S'indexe en signé</b> : voir <see cref="s_IconTables_0056aaa0"/>.</summary>
    private static byte* s_SpeechIcons_0056aae0 => s_IconTables_0056aaa0 + 0x40;

    /// <summary>Ghidra: &amp;DAT_0056ab30 — l'entrée 10, le cadre de la bulle (0x18 x 0x18).</summary>
    private static byte* s_SpeechBubble_0056ab30 => s_IconTables_0056aaa0 + 0x90;

    /// <summary>Ghidra: &amp;DAT_0056ab38 — l'entrée 11, la planche de chiffres.</summary>
    private static byte* s_SpeechDigits_0056ab38 => s_IconTables_0056aaa0 + 0x98;

    /// <summary>Ghidra: &amp;DAT_0056ab70 — l'entrée 18, le petit repère 8x8 de la cible courante.</summary>
    private static byte* s_SpeechMarker_0056ab70 => s_IconTables_0056aaa0 + 0xd0;


    /// <summary>Ghidra: &amp;DAT_005de620 — le tampon ou <see cref="GrantChestContents_00530F20"/>
    /// compose la phrase « untel obtient tel objet ». Alloue a <c>0x80</c> octets : le symbole
    /// suivant dans <c>Blaze.c</c> est <c>DAT_005de6a0</c>, et la phrase la plus longue possible
    /// (prefixe, nom d'objet de 0x42 au plus, suffixe de 8, NUL) y tient largement.
    ///
    /// <para>Hors de <c>ItemMenuArena</c>, qui s'arrete a <c>0x005de510</c> : allocation propre,
    /// pas une vue d'arene.</para></summary>
    private static readonly byte* s_ItemMessageScratch_005de620 = (byte*)NativeMemory.AllocZeroed(0x80);

    /// <summary>
    /// Ghidra: FUN_00530f20 @ 0x00530f20 — <b>la remise d'un objet au groupe</b>, avec la boite de
    /// dialogue qui l'annonce. C'est ce qu'un coffre appelle quand on en prend le contenu.
    ///
    /// <para>Le deroule est en quatre temps. Le monde se fige ; chaque membre du groupe qui peut se
    /// servir de l'objet prend une <b>animation de reaction</b> choisie par sa classe dans
    /// <see cref="s_ReactionAnimations_0056aaa0"/>, apres que son etat d'animation courant a ete mis
    /// de cote en <c>+0x40</c> et <c>+0x42</c> ; l'inventaire du porteur est parcouru ; et une
    /// phrase est composee puis confiee au moteur de texte.</para>
    ///
    /// <para><b>La valeur de retour dit si l'objet est passe</b> : zero quand il est remis,
    /// <c>quantite + 1</c> quand il ne l'est pas — inventaire plein, ou destinataire qui n'est pas
    /// un membre du groupe. Les deux appelants ne lisent que <c>AX</c> (<c>neg ax</c>) ou
    /// <c>AL</c>, d'ou les retrecissements explicites a leurs sites.</para>
    ///
    /// <para><b>L'objet n'est pas rangé ici.</b> La fonction ne fait que vider les deux mots
    /// <c>+0x310</c> et <c>+0x312</c> de l'emplacement libre ; le rangement proprement dit est dans
    /// le rappel de fin de boite, que l'appelant passe en troisieme argument. Ne pas s'attendre a
    /// voir une copie de la fiche d'objet dans ce corps.</para>
    ///
    /// <para>L'inventaire du porteur fait dix emplacements de <c>0x280</c> octets a
    /// <c>MainMem + 0xf0000 + indice * 0x2000 + 0x300</c>. Un emplacement dont les seize premiers
    /// octets — le nom court — egalent ceux de l'objet est un <b>empilement</b> : si l'objet porte
    /// un des bits <c>0x30</c> en <c>+0x13</c>, la remise se fait dans cet emplacement-la sans
    /// chercher plus loin.</para>
    ///
    /// <para>Le champ <c>+0x86</c> de la fiche d'objet, entre <c>0x32c9</c> et <c>0x32cf</c>, decrit
    /// une famille a part : elle passe outre l'inventaire des que les trois bits bas de
    /// <c>+0x150</c> du bloc du porteur valent 4. TODO(REVERSE) : la nature de cette famille n'est
    /// pas etablie.</para>
    /// </summary>
    /// <param name="recipient">Ghidra <c>param_1</c> — l'entite a qui l'objet est remis. Sans le
    /// bit <c>0x800000</c> (membre du groupe), la fonction refuse sans rien afficher.</param>
    /// <param name="amount">Ghidra <c>param_2</c> — l'indice de la fiche d'objet, pas une
    /// quantite : il indexe la table de <c>0x140</c> octets pointee par <c>DAT_00600824</c> et sert
    /// aussi a placer l'icone dans sa planche (colonne = indice % 10, ligne = indice / 10 + 4, au
    /// pas de 24 pixels).</param>
    /// <param name="callback">Ghidra <c>param_3</c> — l'adresse du rappel de fin de boite, range
    /// tel quel en <c>+0x8a8</c> du descripteur. Deux des trois appelants reconstruits y passent
    /// <c>0x00450700</c> ; <see cref="EntityHandler_00473ED0"/>, le coffre de la table 0x0056adc8,
    /// y passe <c>0x00474320</c>. Les deux adresses portent <b>le meme corps</b> — voir
    /// <see cref="ChestPickupDone_00450700"/>.</param>
    private static int GrantChestContents_00530F20(uint* recipient, int amount, uint callback)
    {
        if ((*recipient & 0x800000) == 0) return amount + 1;

        FreezeAllEntities();                                 // Ghidra: FUN_00535300(0)

        *Mem.Entity(0x005f7b50) = (byte)amount;
        Mem.s_PanelSelection_005e9060[0x3c] = 1;             // Ghidra: DAT_005e909c
        Mem.s_PanelSelection_005e9060[0x3d] = 3;             // Ghidra: DAT_005e909d
        Mem.s_PanelSelection_005e9060[0x3e] = 0;             // Ghidra: DAT_005e909e

        byte* item = *(byte**)Mem.Entity(0x00600824) + amount * 0x140;

        // Le sous-repertoire +0x94 porte le descripteur d'icone en +0xc et, en +0x28, le mot que
        // les reactions du groupe recopient.
        byte* iconDirectory = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        byte* icon = *(byte**)(iconDirectory + 0xc);
        icon[7] = *Mem.Entity(0x005f7b50);
        icon[0] = (byte)(amount % 10 * 0x18);
        icon[1] = (byte)((amount / 10 + 4) * 0x18);

        byte* mainMem = GetMainMemPtr();

        // --- les reactions du groupe ---------------------------------------------------------
        if (s_PartySize_005e9020 != 0)
        {
            byte* slot = Inp.s_SlotFlags_005e93c0;
            byte* block = mainMem + 0xf0150;

            for (int i = s_PartySize_005e9020; i != 0; i--, slot += 0x9c, block += 0x2000)
            {
                // L'etat d'animation courant est mis de cote ; FUN_00530ed0 le rendra.
                slot[0x40] = slot[0x1a];
                slot[0x42] = slot[0x18];

                uint flags = *(uint*)slot;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                // +0xf0112 du bloc du personnage porte les classes qui savent se servir d'un objet,
                // +0x12 de la fiche celles auxquelles il est destine.
                if ((*(ushort*)(block - 0x3e) & *(ushort*)(item + 0x12)) == 0) continue;

                *(uint*)(slot + 0x18) = ((uint*)s_ReactionAnimations_0056aaa0)[*block];
                *(uint*)(slot + 0x3c) = *(uint*)(iconDirectory + 0x28);
            }
        }

        // --- ou l'objet doit aller ------------------------------------------------------------
        byte* holder = mainMem + 0xf0000 + (byte)recipient[2] * 0x2000;
        byte* target = Mem.Entity(0x005eca90);

        bool deliver = false;
        ushort family = *(ushort*)(item + 0x86);

        if (family >= 0x32c9 && family <= 0x32cf)
        {
            if ((holder[0x150] & 7) == 4)
            {
                *(ushort*)(item + 0x12) |= 0x8000;
                deliver = true;                              // l'emplacement reste celui d'avant
            }
            else
            {
                *(ushort*)(item + 0x12) &= 0x3fff;
            }
        }
        else if (recipient[0x1b] != 0)
        {
            *(ushort*)(item + 0x12) &= 0x3fff;
        }

        if (!deliver)
        {
            int occupied = 0;
            byte* stored = holder + 0x300;
            *target = 0;

            for (int i = 0; i < 10; i++, stored += 0x280)
            {
                byte chosen = (byte)i;

                if (*stored != 0)
                {
                    int k = 0;
                    while (k < 0x10 && stored[k] == item[k]) k++;

                    if (k == 0x10)
                    {
                        if ((*(ushort*)(stored + 0x12) & 0x8000) != 0)
                        {
                            *(ushort*)(item + 0x12) |= 0x8000;
                        }

                        // Empilable : on s'arrete sur cet emplacement-ci, dont l'indice reste dans
                        // `chosen` — c'est ce que l'original garde en sautant a la remise.
                        if ((item[0x13] & 0x30) != 0)
                        {
                            *target = chosen;
                            deliver = true;
                            break;
                        }
                    }

                    occupied++;
                    chosen = *target;                        // emplacement pris : on garde le dernier libre
                }

                *target = chosen;
            }

            if (!deliver)
            {
                if (occupied == 10)
                {
                    // Inventaire plein : la phrase d'echec, et l'objet reste ou il est.
                    AnnounceItem(item, recipient, 0x2b4, 0x3fc, 0x00530ed0);
                    return amount + 1;
                }

                *(ushort*)(holder + *target * 0x280 + 0x310) = 0;
                *(ushort*)(holder + *target * 0x280 + 0x312) = 0;
            }
        }

        AnnounceItem(item, recipient, 0x2ac, 0x26c, callback);
        return 0;
    }

    /// <summary>
    /// Les deux sorties de <see cref="GrantChestContents_00530F20"/> composent leur phrase de la
    /// meme facon ; l'original ecrit le code deux fois, a trois constantes pres. Reunies ici parce
    /// que c'est une unite qui se tient, pas pour eviter une repetition.
    ///
    /// <para>Quand l'objet porte le bit <c>0x8000</c>, la phrase est <b>construite</b> : un prefixe
    /// pris dans la table de libelles du repertoire, le nom de l'objet mis en forme par
    /// <see cref="BuildItemDisplayName"/>, puis un suffixe de huit octets et un NUL. Sinon elle est
    /// simplement <b>choisie</b> : le premier des bits 8 a 13 de <c>+0x12</c> designe une entree
    /// toute faite.</para>
    ///
    /// <para>La boucle de prefixe copie <b>au moins quatre octets</b> avant de s'arreter sur un
    /// NUL — <c>*src != 0 || i &lt; 4</c> — donc elle recopie les NUL des libelles courts. Transcrit
    /// tel quel : ces quatre octets sont vraisemblablement un en-tete de sequence du moteur de
    /// texte.</para>
    ///
    /// <para>La recherche de bit s'arrete a 13 <b>par le test de fin de boucle</b>, pas par une
    /// borne : si aucun bit n'est pose, l'indice sort a 14 et designe l'entree suivante de la
    /// table. Conserve.</para>
    /// </summary>
    private static void AnnounceItem(byte* item, uint* recipient, int suffix, int fallback, uint callback)
    {
        byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);

        if ((*(ushort*)(item + 0x12) & 0x8000) != 0)
        {
            byte* dest = s_ItemMessageScratch_005de620;
            byte* src = *(byte**)(labels + 0x2a8);

            for (int i = 0; *src != 0 || i < 4; i++)
            {
                *dest++ = *src++;
            }

            dest = BuildItemDisplayName(dest, item);         // Ghidra: FUN_0051bcc0

            byte* tail = *(byte**)(labels + suffix);
            for (int i = 0; i < 8; i++)
            {
                *dest++ = *tail++;
            }

            *dest = 0;

            BindMenuLabels(s_ItemMessageScratch_005de620, (uint)(nint)recipient,
                           s_WorldMapDescriptor_005633b0, callback);
            return;
        }

        int bit = 8;
        while ((*(ushort*)(item + 0x12) & (1 << (bit & 0x1f))) == 0 && ++bit < 0xe) { }

        BindMenuLabels(*(byte**)(labels + fallback + bit * 4), (uint)(nint)recipient,
                       s_WorldMapDescriptor_005633b0, callback);
    }

    /// <summary>
    /// Ghidra: FUN_00530ed0 @ 0x00530ed0 — <b>le miroir exact</b> de la mise de cote faite en tete
    /// de <see cref="GrantChestContents_00530F20"/> : chaque membre du groupe retrouve son animation
    /// (<c>+0x18</c> et <c>+0x1a</c> repris de <c>+0x42</c> et <c>+0x40</c>) et ses deux mots
    /// <c>+0x3c</c> / <c>+0x3e</c> sont remis a zero.
    ///
    /// <para>Il rearme en prime les quatre bits hauts de l'octet d'entree de chaque manette
    /// (<c>DAT_00604a24 + slot * 4</c>), ce qui rend la main au joueur.</para>
    ///
    /// <para>C'est le rappel de fin de boite de la <b>sortie d'echec</b> : quand l'inventaire est
    /// plein, la seule chose a faire en refermant le message est de defaire la mise en scene.</para>
    /// </summary>
    internal static void RestorePartyReactions_00530ED0()
    {
        if (s_PartySize_005e9020 == 0) return;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        byte* pads = Inp.s_PlayerSlots_005e9188;

        for (int i = s_PartySize_005e9020; i != 0; i--, slot += 0x9c, pads += 0x4c)
        {
            *(ushort*)(slot + 0x1a) = slot[0x40];
            *(ushort*)(slot + 0x3c) = 0;
            *(ushort*)(slot + 0x3e) = 0;
            *(ushort*)(slot + 0x18) = slot[0x42];

            // Ghidra: `(&DAT_00604a24)[*pads * 4]` sur un tableau de DWORDS — le binaire fait
            // `shl ecx,4 ; or byte [ecx+604A24h],0F0h` : l'octet +4 du bloc de 0x10 octets de CETTE
            // manette. Transcrit jusqu'au 2026-09-28 avec un pas de 4 OCTETS, ce qui écrivait 0xf0
            // dans le bloc d'une autre manette dès que le type n'était pas 0 : le joueur perdait la
            // main en refermant la boîte (relevé par l'écran de menu LP-012, qui emploie ce rappel).
            Inp.s_InputStateBase_00604a20[*pads * 0x10 + 4] |= 0xf0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005312d0 @ 0x005312d0 — <b>le rangement proprement dit</b> : la fiche d'objet est
    /// recopiee dans l'emplacement d'inventaire, son icone est decoupee de la VRAM et posee a la
    /// place qui revient au personnage, et le compteur d'exemplaires monte d'un.
    ///
    /// <para><b>Ne pas suivre <c>Blaze.c</c> sur cette fonction.</b> Ghidra y a perdu la convention
    /// de <c>FUN_0053db20</c> et de <c>FUN_00532710</c>, qu'il declare sans parametre : leurs
    /// arguments apparaissent comme des affectations a des locales (<c>puStack_20</c>,
    /// <c>uStack_24</c>) et leurs adresses de retour comme des valeurs (<c>iStack_30 = 0x53134e</c>).
    /// Les quatre appels d'ordinaux subissent le meme sort. Tout ce qui suit est transcrit depuis le
    /// desassemblage, conserve dans <c>Legacy/Raw/Blaze/Chest/</c> ; <c>Blaze.c</c> n'a servi que
    /// pour la charpente.</para>
    ///
    /// <para>Les deux sorties anticipees rendent <c>indice + 1</c>, la meme convention que
    /// <see cref="GrantChestContents_00530F20"/> : <c>DAT_005e909f</c> leve (un menu occupe l'ecran)
    /// ou destinataire qui n'est pas un membre du groupe.</para>
    ///
    /// <para><b>La famille <c>0x32c9..0x32cf</c> ne passe pas par l'inventaire</b> : quand les trois
    /// bits bas de <c>+0x150</c> du bloc du porteur valent 4, l'objet incremente un simple compteur.
    /// L'original ecrit <c>holder[famille - 0x3151]</c>, soit <c>holder[0x178 + (famille -
    /// 0x32c9)]</c> : <b>sept compteurs contigus a <c>+0x178</c></b>. Ce sont exactement les sept
    /// octets que le verbe de zone <c>0x00442F20</c> remonte a 5 quand ils sont en dessous — les
    /// deux relevés se confirment l'un l'autre. La forme d'origine est conservee ici pour rester
    /// comparable au desassemblage.</para>
    ///
    /// <para><b>La recopie ecrase le compteur, et l'original le sait</b> : les <c>0x50</c> dwords de
    /// la fiche recouvrent <c>+0x310</c> et <c>+0x312</c> de l'emplacement, donc le compteur
    /// d'exemplaires et le bit <c>0x8000</c> sont releves avant et remis apres. Le compteur plafonne
    /// a 99.</para>
    ///
    /// <para>L'icone se fait en <b>deux passes identiques</b> : un rectangle de <c>0x10 x 1</c> pour
    /// la palette, un de <c>6 x 0x18</c> pour l'image. Chacune est lue de la VRAM vers l'emplacement
    /// (<c>+0x440</c> et <c>+0x460</c>) par l'ordinal 684, puis recopiee a sa place d'affichage par
    /// l'ordinal 676. Les ordinaux 667 qui les encadrent sont des appels vides confirmes.</para>
    /// </summary>
    /// <returns>Zero si l'objet est range, <c>indice + 1</c> sinon.</returns>
    internal static int StoreItemInInventory_005312D0(uint* recipient)
    {
        RestorePartyReactions_00530ED0();                 // Ghidra: FUN_00530ed0()

        byte index = *Mem.Entity(0x005f7b50);
        if (Mem.s_Unknown_005e909f != 0) return index + 1;
        if ((*recipient & 0x800000) == 0) return index + 1;

        byte* item = *(byte**)Mem.Entity(0x00600824) + index * 0x140;
        byte* holder = GetMainMemPtr() + 0xf0000 + (byte)recipient[2] * 0x2000;

        Aud.PlayPositionalSound(0x29, 0x400, 0x1300, (int*)((byte*)recipient + 0x1c));

        // Le coffre lui-meme : on le desarme et on rend son emplacement au pool.
        uint* chest = *(uint**)((byte*)recipient + 0x6c);
        if (chest != null)
        {
            *chest &= 0x7fffffff;
            FreeEntitySlot(Mem.Entity(0x005f7b60), (byte)chest[2]);
        }

        if ((holder[0x150] & 7) == 4)
        {
            ushort family = *(ushort*)(item + 0x86);
            if (family >= 0x32c9 && family <= 0x32cf)
            {
                holder[family - 0x3151]++;
                return 0;
            }
        }

        byte target = *Mem.Entity(0x005eca90);
        byte* slot = holder + target * 0x280;

        ushort keptFlags = *(ushort*)(slot + 0x312);
        ushort keptCount = *(ushort*)(slot + 0x310);

        uint* destination = (uint*)(slot + 0x300);
        uint* source = (uint*)item;
        for (int i = 0x50; i != 0; i--) *destination++ = *source++;

        // --- l'icone, deux passes ---------------------------------------------------------------
        byte* atlas = Mem.s_EntityDecodeTable_005e8f00;
        short* rect = stackalloc short[4];

        // Passe 1 : la palette. DAT_005e8fa6 place la planche dans la VRAM.
        ushort clutOrigin = *(ushort*)(atlas + 0xa6);
        rect[0] = (short)(((index & 0xf) + (clutOrigin & 0x3f)) * 0x10);
        rect[1] = (short)((index >> 4) + (clutOrigin >> 6));
        rect[2] = 0x10;
        rect[3] = 1;

        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(rect, slot + 0x440);
        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_676((ushort*)rect, target * 0x10, (byte)recipient[2] + 0x1f1);

        // Passe 2 : l'image. DAT_005e8fa4 place l'autre planche, et l'icone y occupe une case de
        // 6 x 0x18 sur une grille de dix colonnes.
        ushort iconOrigin = *(ushort*)(atlas + 0xa4);
        rect[0] = (short)((iconOrigin & 0xf) * 0x40 + index % 10 * 6);
        rect[1] = (short)((iconOrigin & 0x10) * 0x10 + (index / 10 + 4) * 0x18);
        rect[2] = 6;
        rect[3] = 0x18;

        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(rect, slot + 0x460);
        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_676((ushort*)rect, target * 6 + 0x180, (byte)recipient[2] * 0x18 + 0x100);

        // --- le compteur, que la recopie vient d'ecraser ----------------------------------------
        *(ushort*)(slot + 0x310) = keptCount;
        *(ushort*)(slot + 0x312) |= (ushort)(keptFlags & 0x8000);

        if (*(ushort*)(slot + 0x310) < 99)
        {
            *(ushort*)(slot + 0x310) += 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00450700 @ 0x00450700 — <b>le rappel de fin de boite du coffre</b> : ce que le
    /// moteur de texte appelle quand le joueur referme « untel obtient tel objet ». Quatre-vingt-sept
    /// octets, <b>absente de <c>Blaze.c</c></b> ; dump dans <c>Legacy/Raw/Blaze/Chest/</c>.
    ///
    /// <para>Elle range l'objet, puis <b>ecrit le verdict dans deux endroits</b> : l'etat du coffre
    /// en <c>+0x10</c> — <c>0xC8</c> si l'objet est passe, <c>0x64</c> sinon, les deux valeurs que
    /// <c>TickEntityTable</c> aiguille — et la table de butin <c>0x005e9860</c>, qui retient ce qui
    /// reste a donner pour que le coffre puisse etre rouvert.</para>
    ///
    /// <para>Le coffre est retrouve par <c>entite[0x41]</c>, l'octet que le script y a range juste
    /// avant d'appeler <see cref="GrantChestContents_00530F20"/>.</para>
    ///
    /// <para>
    /// <b>Le meme corps vit a une seconde adresse, <c>0x00474320</c></b>, celle que
    /// <see cref="EntityHandler_00473ED0"/> passe en rappel. Les 87 octets sont identiques dans le
    /// binaire a l'exception du deplacement relatif du <c>call</c>, et les deux visent
    /// <c>0x005312D0</c> : le compilateur a emis deux fois le meme code sans les replier. Les deux
    /// adresses sont donc aiguillees ici, dans <c>Engine/UiText.cs</c>. Dumps cote a cote dans
    /// <c>Legacy/Raw/Blaze/Chest/</c>.
    /// </para>
    /// </summary>
    internal static void ChestPickupDone_00450700(uint* entity)
    {
        byte index = ((byte*)entity)[0x41];

        *(uint*)((byte*)entity + 0x6c) = 0;
        Mem.s_PanelSelection_005e9060[0] = 0;             // Ghidra: DAT_005e9060

        byte* chest = Mem.Entity(0x005fa5e0) + index * 0x9c;
        int remaining = StoreItemInInventory_005312D0(entity);
        byte* drops = Mem.Arena(0x005e9860);

        if (remaining != 0)
        {
            *(ushort*)(chest + 0x10) = 0xC8;
            drops[*(int*)(chest + 0x44)] = (byte)remaining;
            return;
        }

        *(ushort*)(chest + 0x10) = 0x64;
        drops[*(int*)(chest + 0x44)] = 0;
    }



    /// <summary>
    /// Ghidra: FUN_00473ed0 @ 0x00473ed0 - le sous-type <b>13</b> de la table 0x0056adc8 : <b>le
    /// coffre</b>. C'est le plus fourni de la famille, et le seul a remettre un objet au groupe.
    ///
    /// <para>
    /// L'etat vit dans +0x10 et prend des valeurs tres dispersees, y compris negatives :
    /// <c>-0xff</c> et <c>-2</c> sont des sorties immediates, <c>-1</c> la veille, puis 1, 2, 0x14,
    /// 0x28, 100, 200, 0xc9, 0xca, 300 et 999. Ghidra les rend en <c>undefined2</c>, donc non
    /// signes par endroits (<c>0xffff</c> pour -1, <c>0xfffe</c> pour -2) ; ils sont ici tous lus
    /// en <c>short</c> signe, ce qui est ce que fait le binaire.
    /// </para>
    /// <para>
    /// <b>Deux champs de bits d'arene servent de memoire persistante</b> : celui de
    /// <c>DAT_005e96e0</c>, indexe par le numero de coffre de +0x9a, dit qu'il a deja ete ouvert -
    /// teste a l'ouverture et pose a la fermeture ; celui de <c>DAT_005e9840</c>, indexe par +0x14,
    /// est pose quand le contenu est refuse. Les deux s'ecrivent par le meme motif
    /// <c>bit |= 1 &lt;&lt; (n &amp; 7)</c>, que Ghidra rend en <c>bVar2 - (bVar2 &amp; 0xf8)</c>.
    /// </para>
    /// <para>
    /// <b>Le rappel de fin de boite est <c>0x00474320</c></b>, un bloc dont Ghidra n'a vu que
    /// l'adresse prise. Il n'est pas appele ici : il est range dans le descripteur par
    /// <see cref="GrantChestContents_00530F20"/> et rejoue plus tard. Longtemps porte comme une
    /// lacune, il n'en etait pas une : ses 87 octets sont <b>identiques</b> a ceux de
    /// <c>0x00450700</c>, et <see cref="ChestPickupDone_00450700"/> le couvre. Le contenu du coffre
    /// est donc reellement range.
    /// </para>
    /// </summary>
    private static void EntityHandler_00473ED0(uint* entity)
    {
        byte* e = (byte*)entity;

        *(uint*)(e + 0x50) = 0;
        *(uint*)(e + 0x54) = 0;
        *(uint*)(e + 0x58) = 0;

        if (*(short*)(e + 0x10) == -0xff) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) == -2) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // +0x30 porte le numero de coffre, signe : un numero negatif arme la variante -2 et
            // memorise le signe dans +0x98.
            short chest = *(short*)(e + 0x30);
            *(short*)(e + 0x10) = -1;
            *(short*)(e + 0x9a) = chest;
            *(short*)(e + 0x98) = 0;
            if (chest < 0)
            {
                *(short*)(e + 0x10) = -2;
                *(short*)(e + 0x9a) = (short)-chest;
                *(short*)(e + 0x98) = 1;
            }

            short packed = *(short*)(e + 0x34);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(uint*)(e + 0x44) = (uint)(packed & 0xff);
            *(uint*)(e + 0x48) = (uint)(packed >> 8);
        }

        if (*(short*)(e + 0x10) == -1 && (*(uint*)e & 0x40) != 0)
        {
            byte* target = *(byte**)(e + 0x6c);
            if (target == null) target = Res.s_EntityList_005de558;

            if ((*(uint*)target & 0x10000) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(uint*)e |= 0x2000;
            }
        }

        short state = *(short*)(e + 0x10);
        if (state != -0xff && state != -2 && state != 0 && state != -1 && state != 0xca)
        {
            FreezeAllEntities();                              // Ghidra: FUN_00535300(0)
            *(uint*)e &= 0xfbffffff;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // La cible de lissage de l'etat 2, tiree une fois : 500 a 799.
            *(uint*)(e + 0x5c) = (uint)(CrtRand() % 300 + 500);   // Ghidra: FUN_00547140
            FreezeAllEntities();

            byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, (short)sounds[8], 0x146, 0x3000, 0, 0);

            *(short*)(e + 0x10) = 2;
            *(uint*)e &= 0xfbffffff;
            *(short*)(e + 0x12) = 0;
        }

        // Ghidra: `if (param_1[4] != 2) goto LAB_004740e9;` — le bloc de l'etat 2 est le seul a
        // sauter par-dessus la cascade de sortie, reconstruit en un bloc `if` dont chaque issue
        // rejoint la meme suite.
        if (*(short*)(e + 0x10) == 2 && !StepChestOpening(e, entity)) return;

        ChestDispatchTail(e, entity);
    }

    /// <summary>
    /// L'etat 2 du coffre : le couvercle descend de 0x23 par pas pendant trente pas, la valeur
    /// lissee de <c>DAT_00571990</c> converge d'un quart vers la cible tiree a l'etat 1. Rend faux
    /// quand il faut rendre la main sans passer par la cascade de sortie.
    /// </summary>
    private static bool StepChestOpening(byte* e, uint* entity)
    {
        int* smoothed = Mem.EntityState(0x00571990);
        *smoothed += ((int)*(uint*)(e + 0x5c) - *smoothed) >> 2;

        *(short*)(e + 0x34) -= 0x23;
        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) < 0x1f) return true;

        // Deja ouvert ? Le champ de bits de DAT_005e96e0 le dit, indexe par le numero de coffre.
        short chest = *(short*)(e + 0x9a);
        byte* opened = Mem.s_StateBank0_005e96e0 + (chest >> 3);
        if ((*opened & (byte)(1 << (chest & 7))) != 0)
        {
            *(short*)(e + 0x10) = 300;
            *(short*)(e + 0x12) = 0;
            return false;
        }

        uint slot = *(uint*)(e + 0x48);
        short saved;

        if (*(short*)(e + 0x14) == -1)
        {
            if (slot == 0)
            {
                *(short*)(e + 0x10) = 0x28;
                *(short*)(e + 0x12) = 0;
                return true;
            }

            saved = *(short*)(e + 0x98);
            *(short*)(e + 0x98) = 0x28;
        }
        else
        {
            if (slot == 0)
            {
                *(short*)(e + 0x10) = 0x14;
                *(short*)(e + 0x12) = 0;
                return true;
            }

            saved = *(short*)(e + 0x98);
            *(short*)(e + 0x98) = 0x14;
        }

        SpawnEffectEntity((byte*)entity, (byte)(slot + 0xc0), 0, -1);   // Ghidra: FUN_0047a9d0
        *(short*)(e + 0x98) = saved;
        *(short*)(e + 0x12) = 0;
        *(short*)(e + 0x10) = 999;
        return true;
    }

    /// <summary>
    /// Ghidra: <c>LAB_004740e9</c> — la cascade de sortie du coffre, atteinte par cinq chemins.
    /// Extraite pour la meme raison que <see cref="EntityAnimCommon"/> : elle ne relit aucun etat
    /// local de son appelant.
    /// </summary>
    private static void ChestDispatchTail(byte* e, uint* entity)
    {
        byte* areaDir = s_Unknown_005e97cc;
        short state = *(short*)(e + 0x10);

        if (state == 0x14)
        {
            // Le contenu est refuse : on marque le coffre dans le second champ de bits, indexe par
            // +0x14, et on annonce la phrase que +0x18 designe.
            short index = *(short*)(e + 0x14);
            byte* refused = Mem.Arena(0x005e9840) + (index >> 3);
            *refused |= (byte)(1 << (index & 7));

            BindMenuLabels(
                *(byte**)(*(int*)(areaDir + 0x70) + *(short*)(e + 0x16) * 4),
                *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
            *(short*)(e + 0x10) = 100;
            return;
        }

        if (state == 0x28)
        {
            ChestGrantContents(e, entity);
            return;
        }

        if (state == 100)
        {
            short chest = *(short*)(e + 0x9a);
            byte* opened = Mem.s_StateBank0_005e96e0 + (chest >> 3);
            *opened |= (byte)(1 << (chest & 7));
            *(short*)(e + 0x10) = 200;
        }

        if (*(short*)(e + 0x10) == 200)
        {
            FreezeAllEntities();
            *(short*)(e + 0x10) = 0xc9;
            *(uint*)e &= 0xfbffffff;
        }

        // Ghidra place l'increment DANS la condition (`*(short *)(param_1 + 0xd) = … + 0x46, -1 <
        // …`) : le couvercle remonte a chaque pas de cet etat, et l'etat ne change que lorsque la
        // hauteur repasse positive.
        if (*(short*)(e + 0x10) == 0xc9)
        {
            *(short*)(e + 0x34) += 0x46;
            if (*(short*)(e + 0x34) > -1)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 0xca;
                UnfreezeAllEntities();                        // Ghidra: FUN_00535400
                *(uint*)e &= 0xffffdfff;
                return;
            }
        }

        if (*(short*)(e + 0x10) == 0xca && (e[9] & 0xf) == 0)
        {
            // Ghidra: `-1 - (ushort)((short)param_1[0x26] != 0)` — l'etat de repos depend du signe
            // memorise a l'entree : -1 pour un coffre ordinaire, -2 pour la variante.
            *(short*)(e + 0x10) = (short)(-1 - (*(short*)(e + 0x98) != 0 ? 1 : 0));
        }

        if (*(short*)(e + 0x10) != 300)
        {
            ClearWakeBitOnUnpinnedEntities_004742B0();        // Ghidra: FUN_004742b0
            return;
        }

        // L'etat 300 est le coffre deja vide : une phrase fixe du repertoire principal, puis 200.
        BindMenuLabels(
            *(byte**)(*(int*)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x48c),
            *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);
        *(short*)(e + 0x10) = 200;
    }

    /// <summary>
    /// L'etat 0x28 : la remise effective du contenu. Le rappel de fin de boite est
    /// <c>0x00474320</c> dans les deux branches — c'est-a-dire le meme corps que
    /// <c>0x00450700</c>, emis deux fois par le compilateur.
    ///
    /// <para>
    /// <c>DAT_005e9860</c> retient, par numero d'objet de +0x44, ce que le coffre a deja donne :
    /// non nul, l'objet est redonne tel quel (moins un) ; nul, il est tire dans la table ponderee
    /// de la zone et le resultat est memorise. Le code de retour non nul veut dire « pas passe »,
    /// et l'etat 999 en decoule par le calcul de masque que Ghidra rend en arithmetique.
    /// </para>
    /// </summary>
    private static void ChestGrantContents(byte* e, uint* entity)
    {
        uint* recipient = *(uint**)(e + 0x6c);

        *(uint*)((byte*)recipient + 0x6c) = 0;
        *((sbyte*)recipient + 0x41) = (sbyte)*(uint*)(e + 8);

        byte* given = Mem.Arena(0x005e9860);
        uint item = *(uint*)(e + 0x44);

        if (given[item] != 0)
        {
            int refused = GrantChestContents_00530F20(recipient, given[item] - 1, 0x00474320);

            // Ghidra: `(-(ushort)(sVar4 != 0) & 0xfce1) + 999` — 999 quand l'objet est passe,
            // 999 - 0x31f == 200 quand il ne l'est pas. Ecrit ici pour ce qu'il vaut.
            *(short*)(e + 0x10) = (short)(refused != 0 ? 200 : 999);
            return;
        }

        int roll = PickWeightedTableEntry(*(short*)(e + 0x16), 100);   // Ghidra: FUN_0053a920
        int result = GrantChestContents_00530F20(recipient, roll, 0x00474320);

        if (result != 0)
        {
            *(short*)(e + 0x10) = 200;
            given[item] = (byte)result;
            return;
        }

        *(short*)(e + 0x10) = 999;
    }
}
