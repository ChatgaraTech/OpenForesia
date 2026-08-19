// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00520ae0 @ 0x00520ae0 — l'émetteur d'une particule : il projette sa position,
    /// écarte ce qui sort du champ, et émet la même primitive de dix mots que
    /// <see cref="DrawSpritePrimitive"/>, mise à l'échelle par la perspective.
    ///
    /// <para><b>Signature établie par re-run Ghidra à signature forcée</b>
    /// (<c>~/ghidra_scripts/ForceDecompileParticlePass.java</c>), Ghidra ne lui recouvrant que
    /// trois paramètres seul. cdecl à cinq arguments, chacun épinglé à une instruction :
    /// <c>[ESP+0x2C]</c> otEntry (l'écriture du lien d'OT en 0x520eb8), <c>[ESP+0x30]</c> prim
    /// (l'écriture de la primitive en 0x520ea8, et ce que rendent les sorties anticipées),
    /// <c>[ESP+0x34]</c> particle (0x520ae7), <c>[ESP+0x38]</c> flags (0x520ec3, <c>OR 0x2c</c> —
    /// l'octet de commande GPU), <c>[ESP+0x3C]</c> extra (0x520f5b, l'idiome
    /// <c>((short)x >> 2) &amp; 0xffc0 + (x &amp; 0xff)</c>).</para>
    ///
    /// <para><b>Le pseudo-C ne peut pas être recopié, même signature forcée.</b> Les **quatre**
    /// emplacements d'arguments sont réutilisés comme scratch dès l'émission commencée — écrasés en
    /// 0x520e89, 0x520efa, 0x520f08 et 0x520f26 — si bien que Ghidra rend <c>flags</c> sous le nom
    /// <c>primCursor</c>, <c>extra</c> sous le nom <c>particle</c>, <c>otEntry</c> en local
    /// <c>iStack_4</c>, <c>prim</c> en <c>unaff_retaddr</c>, et <c>particle+0x1c/0x24</c> en
    /// <c>otEntry[7]</c>/<c>otEntry[9]</c>. Ce corps est donc écrit depuis le listing et depuis la
    /// structure de <see cref="DrawSpritePrimitive"/>, à laquelle sa queue est identique mot pour
    /// mot.</para>
    ///
    /// <para>TODO(REVERSE): les attributs sont lus à <c>atlas[1]</c>, c'est-à-dire à l'**image 0**,
    /// alors que la couleur est relue à l'image courante (<c>atlas[frame * 2]</c>). C'est le même
    /// panachage que le quirk de pages de texture de <see cref="DrawAnimatedModel"/> ; transcrit
    /// tel quel, et c'est le premier endroit à regarder si les particules sortent avec la mauvaise
    /// texture.</para>
    /// </summary>
    private static uint* EmitParticle(uint* otEntry, uint* prim, int* particle, uint flags, uint extra)
    {
        // Ghidra: particle[8], soit particle + 0x20 — le descripteur d'atlas.
        uint* atlas = *(uint**)((byte*)particle + 0x20);
        if (atlas == null) return prim;

        uint attributes = atlas[1];

        if (Mem.s_DeviceCapFlag_00564a48 && (attributes & 0x200) != 0)
        {
            if (Mem.s_Unknown_00564a4c == 0) return prim;
            attributes &= 0xfffffdff;
        }

        LoadPointAsMatrixTranslation(particle);

        // Ghidra lit ce mot **avant** de projeter, et c'est ce qui commande le test d'écran.
        ushort cullFlags = *(ushort*)((byte*)particle + 0x1a);

        ProjectOriginThroughMatrix();

        uint screen = *(uint*)s_GteSxy809;
        short screenX = (short)screen;
        short screenY = (short)(screen >> 0x10);
        short depth = (short)*(int*)s_GteSz811;
        int otSlot = depth >> 2;

        if ((uint)(otSlot - 0x58) > 0x39f) return prim;

        if ((cullFlags & 1) != 0
            && ((ushort)(screenX + 0x18) > 0x16f || (ushort)(screenY + 0x18) > 0x11f))
        {
            return prim;
        }

        uint baseWord = *(uint*)((byte*)particle + 0x1c);
        byte frame = *((byte*)particle + 0x24);
        uint colour = atlas[frame * 2];

        int halfW = (int)((colour >> 0x10) & 0xff) - 1;
        int halfH = (int)(colour >> 0x18) - 1;

        // La mise à l'échelle par la perspective : 0x190000 divisé par la profondeur en OT.
        int scale = otSlot != 0 ? (int)(0x190000L / otSlot) : 0x190000;

        short sizeX = *(short*)((byte*)particle + 0x0c);
        short sizeY = *(short*)((byte*)particle + 0x0e);
        uint spanX = (uint)(short)((uint)(sizeX * halfW) >> 0xc) * (uint)scale >> 0xc;
        uint spanY = (uint)(short)((uint)(sizeY * halfH) >> 0xc) * (uint)scale >> 0xc;

        uint clut = *(uint*)(Mem.s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4);

        // Le lien d'OT. Le décalage de -0x28 octets est dans l'original : l'appelant passe
        // `otBase + 0x14`, et l'emplacement visé est `otEntry - 0x28 + otSlot * 4`.
        uint* slot = (uint*)((byte*)otEntry - 0x28 + otSlot * 4);
        *prim = *slot | 0x9000000;
        *slot = (uint)(nuint)prim;

        prim[1] = ((flags & 0xff) | 0x2c | (attributes >> 8)) << 0x18 | (baseWord & 0x3ffffff);

        uint rightU = (uint)halfW + (colour & 0xff);
        uint bottomV = (uint)halfH * 0x100 + (colour & 0xff00);
        uint left = (screen - (spanX >> 1)) & 0xffff;
        uint right = (left + spanX) & 0xffff;
        uint top = (uint)(screenY - (spanY >> 1)) * 0x10000;
        uint bottom = spanY * 0x10000 + top;

        prim[2] = top | left;
        prim[3] = (uint)(((((ushort)((short)extra >> 2)) & 0xffc0) + (extra & 0xff)) * 0x10000)
                  + (clut & 0xffff0000) + ((attributes >> 8) & 0xff0000) | (colour & 0xffff);
        prim[4] = right | top;
        prim[5] = (((attributes & 0xff) << 5) | clut) << 0x10 | rightU | (colour & 0xff00);
        prim[6] = bottom | left;
        prim[7] = bottomV | (colour & 0xff);
        prim[8] = bottom | right;
        prim[9] = bottomV | rightU;

        return prim + 10;
    }

    /// <summary>
    /// Ghidra: la boucle de particules de FUN_00506880, atteinte aux seuls sous-états 5 et 6.
    /// Chaque record subit une marche aléatoire bornée : trois axes, chacun poussé de ±0x2000 vers
    /// l'entité de scène et saturé à ±0x19000, puis la position intègre la vitesse.
    ///
    /// <para><b>Le corps est transcrit, l'émission ne l'est pas.</b> Le tirage, la saturation et
    /// l'intégration sont entièrement lisibles ; ce qui manque est l'appel de dessin,
    /// <c>FUN_00520ae0</c>, dont la signature n'est pas établie — voir le <c>throw</c> ci-dessous.
    /// Écrire la boucle sans son émission donnerait un rendu silencieusement faux, ce que le projet
    /// interdit ; l'écrire avec un trou nommé situe la lacune exactement.</para>
    /// </summary>
    private static uint* EmitSceneParticles(uint* otBase, uint* primCursor)
    {
        for (int i = 0; i < GameMemory.SceneParticleCount; i++)
        {
            // La boucle travaille sur le champ de position (+0x1c), pas sur la base du record.
            int* particle = (int*)(Mem.s_SceneParticles_00572be4 + i * GameMemory.SceneParticleStride + 0x1c);
            int* velocity = particle + 0xd;         // +0x50 du record, la vitesse des trois axes
            int* target = (int*)(Mem.s_SceneEntity_00572b48 + 0x1c);

            // Ghidra: trois blocs identiques, l'un par axe, à deux détails près — le second se
            // compare à -0x88001 au lieu de +0x3fff. Le tirage (FUN_00547140 = rand) décide du
            // sens, la borne 0x19000 de l'amplitude.
            ReadOnlySpan<int> bias = [0x3fff, -0x88001, 0x3fff];
            for (int axis = 0; axis < 3; axis++)
            {
                int roll = CrtRand();
                int v = velocity[axis];
                if (particle[axis] - target[axis] + bias[axis] - roll < 0)
                {
                    if (v < 0x19000) velocity[axis] = v + 0x2000;
                }
                else if (-0x19000 < v)
                {
                    velocity[axis] = v - 0x2000;
                }
            }

            particle[0] += velocity[0];
            particle[1] += velocity[1];
            particle[2] += velocity[2];

            // Ghidra: trois shorts à 0x640 puis un octet cyclique de période 6, pris sur le
            // compteur de frames décalé d'un bit.
            *(short*)((byte*)particle + 0x0c) = 0x640;
            *(short*)((byte*)particle + 0x0e) = 0x640;
            *(short*)((byte*)particle + 0x10) = 0x640;
            *((byte*)particle + 0x24) = (byte)((s_FadeCounter_005e9960 >> 1) % 6);

            // Ghidra: FUN_00520ae0(iVar13 + 0x14, uVar12, piVar1, 0, 0). Le premier argument est
            // **otBase + 0x14 octets**, tranché par analyse de plage plutôt que par le pseudo-C :
            // l'emplacement visé à l'intérieur est `arg1 - 0x28 + otSlot * 4`, avec otSlot dans
            // [0x58, 0x3f7] d'après le test de cull. Cela ne reste dans la table de 0x400 entrées
            // qu'avec otBase ; avec otEntry (soit otBase + 0xffc) on écrirait un millier d'octets
            // au-delà de la table.
            primCursor = EmitParticle(otBase + 5, primCursor, particle, 0, 0);
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_LAB_00560708)[DAT_00578de8])(...)</c> @ 0x00506a52.
    ///
    /// <para>La signature est <b>reconstruite depuis l'assembleur</b>, parce que le pseudo-C de
    /// Ghidra est faux ici : il rend les trois premiers arguments en <c>unaff_EBP</c>,
    /// <c>unaff_EBX</c>, <c>unaff_ESI</c>, alors que l'assembleur empile quatre valeurs et restaure
    /// par <c>ADD ESP,0x10</c> — donc cdecl à quatre arguments, et les trois premiers sont des
    /// <b>locaux de pile</b>, pas des registres. En suivant les décalages : <c>[E+0x14]</c>,
    /// <c>[E+0x18]</c> et <c>[E+0x10]</c>, qui sont exactement les trois out-params de
    /// <c>FUN_0052a1e0</c> — <see cref="FrameBegin"/>. La valeur de retour est réécrite dans
    /// <c>[E+0x10]</c>, le curseur de primitives.</para>
    ///
    /// <para>Le quatrième argument est le même pointeur de manette que le test de sortie vient
    /// d'utiliser (<c>PUSH EAX</c> en 0x00506a46). Le pseudo-C le rend <c>&amp;DAT_00604a20 + i * 4</c>
    /// alors que l'assembleur fait <c>SHL EAX,0x4</c> : voir <see cref="CurrentPadState"/>.</para>
    /// </summary>
    private static uint* InvokeCharacterSelectSubState(
        int index, uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        if ((uint)index >= (uint)Mem.s_CharacterSelectSubStateAddresses.Length)
        {
            throw new NotImplementedException(
                $"Character-select sub-state index {index} is outside PTR_LAB_00560708's eight " +
                "entries; the original would call through whatever the table's neighbour holds.");
        }

        if (index == 0) return CharacterSelectSubState0(otBase, otEntry, primCursor, pad);
        if (index == 1) return CharacterSelectSubState1(otBase, otEntry, primCursor, pad);
        if (index == 3) return CharacterSelectSubState3(otBase, otEntry, primCursor, pad);
        if (index == 4) return CharacterSelectSubState4(otBase, otEntry, primCursor, pad);
        if (index == 5) return CharacterSelectSubState5(otBase, otEntry, primCursor, pad);
        if (index == 6) return CharacterSelectSubState6(otBase, otEntry, primCursor, pad);
        if (index == 7) return CharacterSelectSubState7(otBase, otEntry, primCursor, pad);

        throw new NotImplementedException(
            $"Character-select sub-state {index} (0x{Mem.s_CharacterSelectSubStateAddresses[index]:x8}) " +
            "is decompiled but not yet transcribed — see Legacy/Raw/Blaze/FUN_" +
            $"{Mem.s_CharacterSelectSubStateAddresses[index]:x8}.c. Signature: " +
            "uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad).");
    }

    /// <summary>
    /// Ghidra: FUN_00507010 @ 0x00507010 — <c>PTR_LAB_00560708[0]</c>, le sous-état d'entrée : le
    /// carrousel de personnages.
    ///
    /// <para>Attention aux deux échelles sur le pointeur de manette. Ghidra le type <c>ushort *</c>,
    /// donc <c>in_stack_00000010[1]</c> et <c>[3]</c> sont les mots aux octets 2 et 6, tandis que
    /// <c>*(byte *)((int)in_stack_00000010 + 3)</c> est l'octet 3 — la moitié haute du premier. Les
    /// deux formes coexistent dans la même fonction ; elles sont distinguées ci-dessous par
    /// <c>padWords</c> et <c>pad</c>.</para>
    /// </summary>
    /// <summary>
    /// Ghidra: FUN_00507cb0 @ 0x00507cb0 — <c>PTR_LAB_00560708[1]</c>, la **saisie du nom** : un
    /// clavier virtuel de huit lignes que la manette parcourt, doublé de la frappe réelle que
    /// winsys3d met en file.
    ///
    /// <para><b>Signature reconstruite depuis l'assembleur</b>, comme pour la table de répartition.
    /// Ghidra ne récupère qu'un paramètre et rend les deux autres en <c>unaff_retaddr</c> et
    /// <c>uStack_8</c>. Le prologue est <c>SUB ESP,0x28</c> puis quatre <c>PUSH</c>, soit 0x38 :
    /// les arguments sont donc en <c>[ESP+0x3C..0x48]</c>, et les décalages observés dans le corps
    /// tombent juste — <c>[ESP+0x3C]</c> est <c>uStack_8</c> (otBase), <c>[ESP+0x44]</c> est
    /// <c>unaff_retaddr</c> (le curseur de primitives, huit accès), <c>[ESP+0x48]</c> est
    /// <c>param_1</c> (la manette). Le <c>RET</c> nu confirme cdecl.</para>
    ///
    /// <para>Les globales que Ghidra nomme <c>DAT_00578df4</c>, <c>df8</c>, <c>dfc</c> et
    /// <c>e00</c> sont les entrées 3 à 6 de <see cref="s_SelectState_00578de8"/> ; celles en
    /// <c>DAT_0057351c..3c</c> sont les champs du **premier** descripteur de
    /// <see cref="s_SelectSlots_00573518"/> ; <c>DAT_00572b7a</c> est le lacet de l'entité de
    /// scène. Aucune n'est un nouveau bloc.</para>
    /// </summary>
    private static uint* CharacterSelectSubState1(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = Mem.s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;   // [3] page, [4] colonne, [5] ligne, [6] longueur

        // Première frame du sous-état : on arme la frappe réelle, on coupe la traduction
        // manette→clavier, et on installe le descripteur de la boîte de saisie.
        if (s_SelectFlags_00562a48[0] != 0)
        {
            EnableKBInput(1);
            Inp.DisablePadKeyboard();              // Ghidra: FUN_00523530
            s_SelectFlags_00562a48[0] = 0;
            state[4] = 0;
            state[5] = 0;
            state[3] = 0;

            byte* box = s_SelectSlots_00573518;
            *(short*)(box + 0x04) = 0x28;      // DAT_0057351c
            *(short*)(box + 0x06) = 0x38;      // DAT_0057351e
            *(short*)(box + 0x08) = 0xe0;      // DAT_00573520
            *(short*)(box + 0x0a) = 0x68;      // DAT_00573522
            box[0x12] = 0x20;                  // DAT_00573528 + 2
            *(uint*)(box + 0x20) = 0x1070c28;  // DAT_00573538 — le mot de fonction « texte »
            *(uint*)(box + 0x24) = 0x1000000;  // DAT_0057353c — son activation
            s_SelectFlags_00562a48[2] = 2;
        }

        SettleSceneCamera(scratch);

        // Ghidra: `if (DAT_00562a4c != 0) goto LAB_0050820a` — pendant le fondu d'entrée du
        // sous-état, aucune entrée n'est lue ; on va directement au dessin.
        if (s_SelectFlags_00562a48[1] == 0)
        {
            int typed = GetKBChar();

            // Les deux chemins de sortie de l'original se rejoignent juste avant EnableKBInput(0) :
            // le drapeau rendu ici tient lieu de ce point de jonction, que le `goto` masquait.
            bool leaving = typed == 0
                ? HandlePadNameEntry(pad, scratch, state)
                : HandleTypedNameEntry(typed, scratch, state);

            if (leaving) EnableKBInput(0);
        }

        return DrawNameEntryScreen(otBase, primCursor, scratch, state);
    }

    /// <summary>
    /// Le bloc d'ouverture que **tous** les sous-états « un personnage est en scène » répètent mot
    /// pour mot : la caméra rejoint l'angle du personnage courant, la teinte de fond revient au
    /// gris 0x60, la pose devient une matrice, et l'entité de scène tourne vers le joueur.
    ///
    /// <para>Extrait parce que c'est une unité indépendante répétée telle quelle d'un sous-état à
    /// l'autre, pas pour découper un corps unique.</para>
    /// </summary>
    private static void SettleSceneCamera(byte* scratch)
    {
        // Un seizième de tour par cran, par le chemin court : au-delà d'un demi-tour on tourne
        // dans l'autre sens plutôt que de traverser tout le cercle.
        ushort delta = (ushort)(((scratch[0x150] - 8) * 0x100
                                 - *(short*)(Mem.s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((short)(0x1000 - (short)delta) >> 2);
        *(short*)(Mem.s_CameraVector_00604928 + 2) += step;

        Mem.s_BackColourR_005e93a0 += (short)((0x60 - Mem.s_BackColourR_005e93a0) >> 2);
        Mem.s_BackColourG_005e93a2 += (short)((0x60 - Mem.s_BackColourG_005e93a2) >> 2);
        Mem.s_BackColourB_005e93a4 += (short)((0x60 - Mem.s_BackColourB_005e93a4) >> 2);

        // Ghidra: FUN_0051c300(&DAT_00604928, &DAT_006008e0) — la pose de caméra devient la matrice.
        RotMatrix((short*)Mem.s_CameraVector_00604928, (short*)Mem.s_CameraMatrix_006008e0);
        Winsys3d.Ordinal_376((byte)Mem.s_BackColourR_005e93a0,
                    (byte)Mem.s_BackColourG_005e93a2,
                    (byte)Mem.s_BackColourB_005e93a4);

        // DAT_00572b7a — le lacet de l'entité de scène, à +0x32, amené vers l'angle du personnage.
        short* sceneYaw = (short*)(Mem.s_SceneEntity_00572b48 + 0x32);
        *sceneYaw += (short)((scratch[0x150] * -0x100 - *sceneYaw) >> 2);
    }

    // Ghidra: &DAT_00562b70 — seize octets relevés au décalage fichier 0x161170, un par personnage.
    // Le sous-état 3 s'en sert pour préarmer scratch[0x153] selon la classe choisie. La table
    // s'arrête net sur celle de 0x00562b80, ce qui en fixe la taille.
    private static readonly byte[] s_ClassDefault_00562b70 =
    [
        0x08, 0x02, 0x00, 0x04, 0x0a, 0x02, 0x04, 0x06,
        0x09, 0x03, 0x0f, 0x0d, 0x01, 0x03, 0x05, 0x07,
    ];

    // Ghidra: &DAT_00573350 — les trois dés, pas 0x9c octets, encore le même type d'enregistrement.
    // Seuls les cinq premiers champs servent : +0x00 l'angle de rotation, +0x18 son cosinus,
    // +0x1a un facteur figé à 0x1000, +0x28 le mot d'attribut, +0x30 la face tirée.
    //
    // Les trois records s'étendent jusqu'à 0x573524 et **chevauchent** donc DAT_00573518 sur douze
    // octets. Ce n'est pas une erreur de relevé : le pas de 0x9c est confirmé par les trois
    // adresses que le code compare entre elles (0x573380, 0x57341c, 0x5734b8), et aucun champ
    // touché ne dépasse +0x30, soit 0x5734e8 au plus loin. Le chevauchement est dans l'original et
    // reste inoffensif.
    private const int DiceCount = 3;
    private const int DiceStride = 0x9c;
    private static readonly byte* s_Dice_00573350 =
        (byte*)NativeMemory.AllocZeroed(DiceCount, DiceStride);

    // Ghidra: DAT_00578e04 — le nombre de dés déjà lancés, et DAT_005e906a le total de points.
    private static int s_DiceRolled_00578e04;

    // **Corrigé 2026-08-16** : même défaut d'aliasing que dialogGate/confirmGate (voir plus bas) —
    // DAT_005e906a est l'octet +0xa du même tableau DAT_005e9060, pas une globale isolée.
    // Handler_005449f0 (l'affichage décimal, code de contrôle 0x06) lit déjà
    // s_PanelSelection_005e9060+offset ; un `sbyte` séparé ici faisait que le panneau « Bonus »
    // affichait toujours 0 alors que la répartition des points fonctionnait (elle lisait/écrivait
    // ce même `sbyte`, juste jamais le tableau que l'affichage regarde).
    private static ref byte s_StatTotal_005e906a => ref Mem.s_PanelSelection_005e9060[0xa];

    // Ghidra: DAT_00578de4 — la source des sept octets recopiés dans s_PanelSelection_005e9060.
    private static byte* s_StatSource_00578de4;

    /// <summary>
    /// Ghidra: FUN_00509470 @ 0x00509470 — <c>PTR_LAB_00560708[5]</c>, le **lancer des trois dés**
    /// qui fixe le total de points du personnage.
    ///
    /// <para>Chaque dé tourne de 0 à <c>0x800</c> par pas de <c>0x100</c> ; à mi-course
    /// (<c>0x400</c>) sa face est tirée entre 1 et 6. Quand les trois sont arrivés, leurs faces
    /// s'ajoutent au total (parti de 15, plafonné à 99). **Trois faces identiques et un total
    /// encore sous 99 donnent droit à un relancer** — c'est le seul rôle de la phase
    /// <c>0x3a</c>.</para>
    ///
    /// <para>Signature confirmée par le re-run à signature forcée
    /// (<c>~/ghidra_scripts/ForceDecompileSubStates.java</c>), qui rend maintenant <c>pad[3]</c>
    /// tel quel. Le cadre reste à reconstruire à la main pour le sprite : <c>SUB ESP,0x14</c> puis
    /// quatre <c>PUSH</c> (comptés sur les quatre <c>POP</c> de l'épilogue), donc les locaux sont
    /// en <c>[ESP+0x10..0x23]</c> et le descripteur de sprite occupe exactement ces vingt
    /// octets.</para>
    /// </summary>
    private static uint* CharacterSelectSubState5(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = Mem.s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;
            s_StatTotal_005e906a = 0xf;
            Mem.s_Unknown_005e909f = 0;
            s_DiceRolled_00578e04 = 0;
            ResetDice();

            // Les sept octets de statistiques de la classe choisie.
            s_StatSource_00578de4 =
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x74))
                + (scratch[0x150] & 7) * 8;
            for (int i = 0; i < 7; i++)
            {
                Mem.s_PanelSelection_005e9060[i] = s_StatSource_00578de4[i];
            }

            s_SelectFlags_00562a48[2] = 3;
        }

        if (s_SelectFlags_00562a48[1] == 0) StepSubState5Phase(pad, state);

        Mem.s_BackColourR_005e93a0 += (short)((0x60 - Mem.s_BackColourR_005e93a0) >> 2);
        Mem.s_BackColourG_005e93a2 += (short)((0x60 - Mem.s_BackColourG_005e93a2) >> 2);
        Mem.s_BackColourB_005e93a4 += (short)((0x60 - Mem.s_BackColourB_005e93a4) >> 2);
        Winsys3d.Ordinal_376((byte)Mem.s_BackColourR_005e93a0,
                    (byte)Mem.s_BackColourG_005e93a2,
                    (byte)Mem.s_BackColourB_005e93a4);

        // Les trois dés. Le sprite est le bloc de vingt octets des locaux : atlas, attribut,
        // position, les deux facteurs d'échelle, et la face.
        byte* sprite = stackalloc byte[20];
        NativeMemory.Clear(sprite, 20);
        *(uint**)sprite = ((uint**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94))[1];

        for (int i = 0, spin = 0; spin < 0x4b0; i++, spin += 400)
        {
            byte* die = s_Dice_00573350 + i * DiceStride;
            short angle = *(short*)(die + 0x00);

            *(uint*)(sprite + 4) = *(uint*)(die + 0x28);
            *(short*)(sprite + 0x0c) = (short)(*(short*)(die + 0x18) - (angle >> 1));
            *(short*)(sprite + 0x0e) = (short)((angle >> 1) + *(short*)(die + 0x1a));
            *(short*)(sprite + 0x10) = die[0x30];
            *(short*)(sprite + 0x0a) = 0x78;

            // Ghidra: `LEA ECX,[EDI + EDI*2] ; SHL ECX,4 ; ADD ECX,0x6e` — trois dés espacés de
            // 48 pixels à partir de 0x6e, plus un balancement proportionnel à l'angle.
            short x = (short)(i * 48 + 0x6e);
            x += (short)((int)Winsys3d.GetSinTableEntry(spin - 400) * angle >> 0x13);
            *(short*)(sprite + 8) = x;

            primCursor = Prims.DrawSpritePrimitive(otBase, primCursor, (int*)sprite, 0, 0);
        }

        byte* label = Mem.s_UiDescriptors_00560ff0 + 2 * GameMemory.UiDescriptorStride;   // &DAT_00562180
        BindMenuLabels(
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0xf4), 0, label, 0);
        return Ui.RunTextEngine(primCursor, label);
    }

    /// <summary>Remet les trois dés à l'arrêt — répété à l'entrée et au relancer.</summary>
    private static void ResetDice()
    {
        for (int i = 0; i < DiceCount; i++)
        {
            byte* die = s_Dice_00573350 + i * DiceStride;
            *(short*)(die + 0x00) = 0;
            *(short*)(die + 0x18) = 0x1000;
            *(short*)(die + 0x1a) = 0x1000;
            *(uint*)(die + 0x28) = 0x1000000;
            die[0x30] = 0;
        }
    }

    private static void StepSubState5Phase(byte* pad, int* state)
    {
        switch (state[2])
        {
            case 0x39:
            {
                // Annulation : possible seulement tant qu'aucun dé n'est lancé.
                if ((*(ushort*)(pad + 2) & 0x4000) != 0 && s_DiceRolled_00578e04 == 0)
                {
                    PlayUiSound39();
                    state[2] = 0x2e;
                    state[0]--;
                    break;
                }

                if ((*(ushort*)(pad + 2) & 0x8000) != 0 && s_DiceRolled_00578e04 < 3)
                {
                    PlayUiSound38();
                    *(short*)(s_Dice_00573350 + s_DiceRolled_00578e04 * DiceStride) = 0;
                    s_DiceRolled_00578e04++;
                }

                // Les dés déjà lancés tournent.
                for (int i = 0; i < s_DiceRolled_00578e04; i++)
                {
                    byte* die = s_Dice_00573350 + i * DiceStride;
                    short angle = *(short*)(die + 0x00);

                    *(short*)(die + 0x18) = (short)Winsys3d.GetCosTableEntry(angle);
                    if (angle == 0x400) die[0x30] = (byte)(CrtRand() % 6 + 1);
                    if (angle < 0x800) *(short*)(die + 0x00) = (short)(angle + 0x100);
                }

                if (s_DiceRolled_00578e04 != 3) break;

                // Les trois sont-ils arrêtés ? On somme les faces au passage.
                int stopped = 0;
                sbyte sum = 0;
                for (; stopped < 3; stopped++)
                {
                    byte* die = s_Dice_00573350 + stopped * DiceStride;
                    if (*(short*)(die + 0x00) != 0x800) break;
                    sum += (sbyte)die[0x30];
                }
                if (stopped != 3) break;

                s_StatTotal_005e906a = (byte)(s_StatTotal_005e906a + sum);

                byte f0 = s_Dice_00573350[0x30];
                byte f1 = (s_Dice_00573350 + DiceStride)[0x30];
                byte f2 = (s_Dice_00573350 + 2 * DiceStride)[0x30];

                if (f0 == f1 && f1 == f2 && s_StatTotal_005e906a < 99)
                {
                    state[2]++;                     // 0x3a — un relancer est offert
                }
                else
                {
                    if (s_StatTotal_005e906a > 99) s_StatTotal_005e906a = 99;
                    state[2] += 2;                  // 0x3b — on passe à la confirmation
                }
                break;
            }

            case 0x3a:
                // Le relancer : on remet les trois dés à zéro et on repart en 0x39.
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2] = 0x39;
                    s_DiceRolled_00578e04 = 0;
                    ResetDice();
                }
                break;

            case 0x3b:
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;
                    break;
                }
                if ((pad[3] & 0x40) == 0) break;
                Mem.s_Unknown_0056075b = 0;
                Mem.s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                state[2] = 0x39;
                s_SelectFlags_00562a48[0] = 1;
                break;

            case 0x3c:
                if ((Mem.s_Unknown_00560fef & 4) == 0)
                {
                    if (Mem.s_Unknown_005e909f == 0)
                    {
                        state[2] = 0x3e;
                        state[0]++;
                    }
                    else
                    {
                        state[2] = 0x39;
                        s_SelectFlags_00562a48[0] = 1;
                    }
                    break;
                }
                if ((pad[3] & 0x40) == 0) break;
                Mem.s_Unknown_00560763 = 0;
                Mem.s_Unknown_00560fef &= 0xfb;
                PlayUiSound39();
                state[2] = 0x39;
                s_SelectFlags_00562a48[0] = 1;
                break;
        }
    }

    // Ghidra: &DAT_00562ab8 — huit octets, un par classe (&7), relevés au décalage fichier
    // 0x1610b8. Utilisé pour DAT_00573338+0x2b5 à la finalisation de la répartition.
    private static readonly byte[] s_ClassStartIcon_00562ab8 = [0, 1, 0, 0, 3, 0, 0, 2];

    // Ghidra: &DAT_00562ac0 (huit octets, décalage fichier 0x1610c0) et &DAT_00562ac8 (neuf mots,
    // décalage fichier 0x1610c8). La finalisation copie huit drapeaux dans DAT_00573338+0x1fc0 en
    // suivant cet ordre fixe : DAT_00573338[0x1fc0 + i*2] = table[remap[i]], indépendamment du
    // personnage. La table est une puissance de deux décroissante à partir du bit 15, sauf
    // l'entrée 0 qui vaut 1 — un jeu de drapeaux bit à bit dont le rôle exact (probablement des
    // emplacements d'équipement ou de sorts) n'est pas établi.
    private static readonly byte[] s_StatBitRemap_00562ac0 = [1, 2, 3, 6, 5, 4, 7, 8];
    private static readonly ushort[] s_StatBitTable_00562ac8 =
        [1, 0x8000, 0x4000, 0x2000, 0x1000, 0x800, 0x400, 0x200, 0x100];

    // Ghidra: `pcRam00566bc0` (Legacy/Raw/Blaze/FUN_005098f0.c:320-338) — **un pointeur logé à
    // 0x00566bc0, pas un tampon**. Tranché le 2026-08-18 en désassemblant directement les huit
    // sites qui le touchent dans Legacy/Blaze.exe : tous chargent l'adresse
    // (`8B 35 / 8B 0D / 8B 15  C0 6B 56 00` = `MOV ESI,[0x566BC0]`) avant d'écrire à travers
    // (`88 54 30 0C` = `MOV [EAX+ESI+0xC],DL`) — une déréférence de plus que la pseudo-C ne le
    // montre, exactement le même piège que dans Saves.SetCardPathDeviceFields. Et aucun des huit n'écrit
    // l'adresse elle-même (pas un seul `A3 C0 6B 56 00` ni `89 xx C0 6B 56 00` dans tout le
    // .text) : le pointeur garde donc à l'exécution sa valeur de chargement, 0x00568498.
    //
    // À 0x00568498 se trouve le gabarit du nom de fichier de sauvegarde : "Blaze-Saving" (12
    // octets) suivi de huit espaces et d'un NUL. Les douze premiers octets, dont le rôle était noté
    // « non établi » jusqu'ici, sont donc simplement le **préfixe littéral** que la copie vers
    // DAT_00573338+0x1fd0 reprend depuis l'octet 0 ; la finalisation n'écrit que les sept
    // caractères de code à +0xc..+0x12, puis le NUL à +0x13. D'où la forme des noms observés dans
    // le jeu de 1998 : `Blaze-Saving@MVB@R_`, soit 12 + 7 caractères. Le voisinage confirme la
    // lecture — 0x566bb8 et 0x566bbc portent les pointeurs de "SYSTEM.INI" et "BLAZE.ALL;1",
    // 0x566bc0 est l'entrée suivante de la même table de noms de fichiers.
    //
    // Alloué à 32 octets (le gabarit en fait 21 avec son NUL) pour garder de la marge autour de
    // l'écriture du NUL à l'indice 0x13.
    private static readonly byte* s_SaveNameTemplate_00568498 = AllocSaveNameTemplate();

    private static byte* AllocSaveNameTemplate()
    {
        byte* buffer = (byte*)NativeMemory.AllocZeroed(32);
        byte* template = ToAnsiZ("Blaze-Saving        ");
        for (int i = 0; template[i] != 0; i++) buffer[i] = template[i];
        return buffer;
    }

    /// <summary>
    /// Ghidra: FUN_005098f0 @ 0x005098f0 — <c>PTR_LAB_00560708[6]</c>, la répartition des points de
    /// statistiques : sept lignes de compteurs (Force, ...), plus deux lignes partagées pour le
    /// total restant et la confirmation, disposées en éventail sur <see cref="s_SelectSlots_00573518"/>
    /// (neuf enregistrements, comme <see cref="s_UiDescriptors_00560ff0"/>).
    ///
    /// <para>Signature confirmée par le re-run à signature forcée
    /// (<c>~/ghidra_scripts/ForceDecompileSubStates.java</c>). Ce sous-état termine l'écran entier
    /// en posant <c>state[0] = -1</c> et <see cref="s_GameStateId"/> — le retour vers le jeu, pas
    /// seulement un pas de la machine à sous-états ; voir le commentaire sur ce test dans
    /// <c>RunCharacterSelectFrameLoop</c>.</para>
    ///
    /// <para>La machine à phases sur <c>state[2]</c> (<c>DAT_00578df0</c>) comporte plusieurs
    /// branches de boîte de dialogue modale gardées par <see cref="s_Unknown_00560fef"/> et
    /// <see cref="s_Unknown_005e909f"/>, toujours à zéro tant que le code qui les pose n'est pas
    /// reconstruit (TODO.md §5) : elles sont transcrites mais actuellement mortes, comme dans
    /// <c>StepSubState5Phase</c>.</para>
    /// </summary>
    private static uint* CharacterSelectSubState6(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = Mem.s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;

        // ECHAFAUDAGE TEMPORAIRE : OPENFORESIA_FORCE_CONFIRM_STATS=1 appelle
        // FinalizeCharacterStats directement au premier passage, sans dépendre du mappage
        // clavier→manette réel (non élucidé pour le bouton de confirmation, `pad+2 & 0x8000`) ni
        // du nombre de points à dépenser avant de pouvoir confirmer.
        if (s_SelectFlags_00562a48[0] != 0
            && Environment.GetEnvironmentVariable("OPENFORESIA_FORCE_CONFIRM_STATS") == "1")
        {
            byte* syntheticPad = stackalloc byte[16];
            NativeMemory.Clear(syntheticPad, 16);
            FinalizeCharacterStats(syntheticPad, scratch, state);
        }

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;
            state[8] = 0;   // DAT_00578e08 — le rang du curseur de statistique, 0..6

            // Ghidra: sept lignes disposées en éventail — Ordinal_450/451 (cos/rsin) à l'angle
            // i*0x155, comme le cercle du sous-état 0 mais sur un pas plus grand et sans tour
            // complet (7*0x155 = 0x953, un peu plus d'un tiers de tour sur 0x1000).
            int angle = 0;
            for (int i = 0; i < 7; i++, angle += 0x155)
            {
                byte* row = s_SelectSlots_00573518 + i * CharacterSelectSlotStride;
                *(short*)(row + 0x04) = (short)(((Winsys3d.GetCosTableEntry(angle) * -0x58) >> 0xc) + 0x88);
                *(short*)(row + 0x06) = (short)((((int)Winsys3d.GetSinTableEntry(angle) << 6) >> 0xc) + 0x48);
                *(short*)(row + 0x08) = 0x20;
                *(short*)(row + 0x0a) = 0x18;
                *(uint*)(row + 0x10) = 0x1180000;
                *(int*)(row + 0x14) = i + 3;
                *(uint*)(row + 0x18) = 0x10f0808;
                *(uint*)(row + 0x1c) = 0x2cf1020;
                *(uint*)(row + 0x20) = 0x1070c10;
                *(uint*)(row + 0x24) = 0x1000000;
                *(uint*)(row + 0x28) = 0;
                *(uint*)(row + 0x30) = 0;
                *(uint*)(row + 0x38) = 0;
                *(uint*)(row + 0x40) = 0x10f0000;
                *(uint*)(row + 0x44) = 0x1000000;
                *(uint*)(row + 0x48) = 0;
                *(uint*)(row + 0x50) = 0;
                *(uint*)(row + 0x58) = 0;
                *(uint*)(row + 0x60) = 0;
            }

            // Lignes 7 et 8 : le total restant et la confirmation, suite immédiate du tableau.
            byte* totalRow = s_SelectSlots_00573518 + 7 * CharacterSelectSlotStride;
            *(short*)(totalRow + 0x04) = 0x58;
            *(short*)(totalRow + 0x06) = 0x38;
            *(short*)(totalRow + 0x08) = 0x28;
            *(short*)(totalRow + 0x0a) = 0x18;
            totalRow[0x20] = 0x10;
            *(uint*)(totalRow + 0x14) = 10;

            byte* confirmRow = s_SelectSlots_00573518 + 8 * CharacterSelectSlotStride;
            *(short*)(confirmRow + 0x04) = 0xb0;
            *(short*)(confirmRow + 0x06) = 0x38;
            *(short*)(confirmRow + 0x08) = 0x28;
            *(short*)(confirmRow + 0x0a) = 0x18;
            confirmRow[0x20] = 0x10;
            *(uint*)(confirmRow + 0x14) = 0xb;

            SetClassBaseHpMp(scratch);
        }

        // Ghidra: `if (DAT_00562a4c != 0) goto LAB_0050a5e0` — pendant le fondu d'entrée, aucune
        // entrée n'est lue ; on va directement au rendu, comme dans les autres sous-états.
        if (s_SelectFlags_00562a48[1] == 0)
        {
            primCursor = StepSubState6Phase(pad, state, scratch, otBase, primCursor);
        }

        return RenderStatRows(otBase, primCursor);
    }

    /// <summary>Pose les <b>points de vie et de magie de départ</b> de la classe dans
    /// <c>+0x148</c> / <c>+0x14a</c>. Répété quatre fois dans l'original (entrée du sous-état, et
    /// trois points de retour à la phase 0x3e) ; extrait ici comme dans les autres sous-états.
    ///
    /// <para>Renommé le 2026-08-17 (il s'appelait <c>SetClassHighlightColour</c>, un rôle
    /// simplement supposé). Trois recoupements le fixent :
    /// <see cref="FinalizeCharacterStats"/> recopie <c>+0x148</c>/<c>+0x14a</c> dans
    /// <c>+0x14c</c>/<c>+0x14e</c> â€” la paire courant/maximum classique ; l'accumulateur qui
    /// prépare ces valeurs les calcule en <c>(stat + stat) / 2 + base</c>, la formule usuelle
    /// PV/PM ; et <see cref="RunDebugPartySelect"/> écrase les quatre champs par 9000/9000/200/200,
    /// ce qui n'a de sens que pour des PV et des PM. Les valeurs par classe sont d'ailleurs
    /// cohérentes : 16 PV / 2 PM pour le Guerrier et le Nain, 14/8 pour le Prêtre, le Sorcier et
    /// la Fée, 15/4 pour le Voleur, le Chasseur et l'Elfe.</para></summary>
    private static void SetClassBaseHpMp(byte* scratch)
    {
        switch (scratch[0x150] & 7)
        {
            case 0:
            case 6:
                *(short*)(scratch + 0x148) = 0x10;
                *(short*)(scratch + 0x14a) = 2;
                break;
            case 1:
            case 3:
            case 7:
                *(short*)(scratch + 0x148) = 0xe;
                *(short*)(scratch + 0x14a) = 8;
                break;
            case 2:
            case 4:
            case 5:
                *(short*)(scratch + 0x148) = 0xf;
                *(short*)(scratch + 0x14a) = 4;
                break;
        }
    }

    private static uint* StepSubState6Phase(byte* pad, int* state, byte* scratch, uint* otBase, uint* primCursor)
    {
        switch (state[2])
        {
            case 0x3e:
            {
                int rowBefore = state[8];
                ushort buttons = *(ushort*)(pad + 2);
                if ((buttons & 0x4000) == 0)
                {
                    if ((buttons & 0x8000) == 0 || s_StatTotal_005e906a != 0)
                    {
                        primCursor = EditCharacterStats(pad, state, scratch, otBase, primCursor, rowBefore);
                    }
                    else
                    {
                        FinalizeCharacterStats(pad, scratch, state);
                    }
                }
                else
                {
                    PlayUiSound39();
                    state[2]++;             // 0x3e -> 0x3f
                }
                break;
            }

            // Ghidra: `(DAT_00560fef & 2) == 0` est toujours vrai — la boîte de confirmation n'est
            // pas reconstruite (TODO.md §5) — donc la branche `pad[3] & 0x40` qui suit reste morte
            // dans ce port, comme dans StepSubState5Phase.
            case 0x3f:
            case 0x41:
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;             // 0x3f->0x40, 0x41->0x42
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_0056075b = 0;
                    Mem.s_Unknown_00560fef &= 0xfd;
                    PlayUiSound39();
                    state[2] = 0x3e;
                }
                break;

            case 0x40:
                if ((Mem.s_Unknown_00560fef & 4) == 0)
                {
                    if (Mem.s_Unknown_005e909f == 0) { state[2] = 0x39; state[0]--; }
                    else state[2] = 0x3e;
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_00560763 = 0;
                    Mem.s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2] = 0x3e;
                }
                break;

            case 0x42:
                if ((Mem.s_Unknown_00560fef & 4) == 0)
                {
                    if (Mem.s_Unknown_005e909f != 0) { state[2] = 0x3e; break; }
                    state[2] = 0x6e;
                    state[0]++;
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_00560763 = 0;
                    Mem.s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2] = 0x3e;
                }
                break;

            case 0x5d:
                if ((Mem.s_Unknown_00560fef & 4) == 0)
                {
                    if (Mem.s_Unknown_005e909f == 0)
                    {
                        CancelCharacterCreation(scratch, state);
                    }
                    else
                    {
                        state[2] = 0x3e;
                        SetClassBaseHpMp(scratch);
                    }
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_00560763 = 0;
                    Mem.s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2] = 0x81;
                }
                break;

            case 0x7f:
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2] = 0x5d;
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_0056075b = 0;
                    Mem.s_Unknown_00560fef &= 0xfd;
                    PlayUiSound39();
                    state[2] = 0x81;
                }
                break;

            case 0x80:
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]--;             // 0x80 -> 0x7f
                    break;
                }
                if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_0056075b = 0;
                    Mem.s_Unknown_00560fef &= 0xfd;
                    PlayUiSound39();
                    state[2]++;             // 0x80 -> 0x81
                }
                break;

            case 0x81:
                if ((Mem.s_Unknown_00560fef & 4) != 0)
                {
                    if ((pad[3] & 0x40) != 0)
                    {
                        Mem.s_Unknown_00560763 = 0;
                        Mem.s_Unknown_00560fef &= 0xfb;
                        PlayUiSound39();
                        state[2] = 0x3e;
                        SetClassBaseHpMp(scratch);
                    }
                    break;
                }
                if (Mem.s_Unknown_005e909f != 0) { state[2] = 0x80; break; }
                state[2] = 0x6e;
                state[0]++;                 // vers le sous-état 7
                break;
        }

        return primCursor;
    }

    /// <summary>Déplacement du curseur, dépense/remboursement d'un point, et dessin de l'en-tête
    /// de ligne (icône + texte de statut). Ghidra: le corps du bloc « if (bit0x8000==0 ||
    /// total!=0) » de <c>case 0x3e</c>.</summary>
    private static uint* EditCharacterStats(
        byte* pad, int* state, byte* scratch, uint* otBase, uint* primCursor, int rowBefore)
    {
        if ((pad[6] & 0x80) != 0 && state[8] != 0) state[8]--;
        if ((pad[6] & 0x20) != 0 && state[8] < 6) state[8]++;
        if (rowBefore != state[8]) PlayUiSound3A();

        byte* stats = Mem.s_PanelSelection_005e9060;
        if ((pad[6] & 0x10) != 0 && s_StatTotal_005e906a != 0 && stats[state[8]] < 99)
        {
            s_StatTotal_005e906a--;
            stats[state[8]]++;
            PlayUiSound38();
        }
        if ((pad[6] & 0x40) != 0 && s_StatSource_00578de4[state[8]] < stats[state[8]])
        {
            stats[state[8]]--;
            s_StatTotal_005e906a++;
            PlayUiSound39();
        }

        // Ghidra: DAT_005e9064/68 — une teinte animée vers une moyenne glissante de deux
        // statistiques, écrite dans les octets 7 et 8 du même tampon que les valeurs (0x100
        // octets, réutilisé). Aucune lecture de ces deux octets n'apparaît dans cette fonction.
        // TODO(REVERSE) : rôle exact non établi, probablement un fondu consommé ailleurs.
        byte oldByte4 = stats[4];
        byte oldByte5 = stats[5];
        stats[7] = (byte)((byte)((stats[0] + oldByte4) >> 1) + scratch[0x148]);
        stats[8] = (byte)((byte)((stats[2] + oldByte5) >> 1) + scratch[0x14a]);

        EmitViewportRect(otBase, &primCursor);

        byte* row = s_SelectSlots_00573518 + state[8] * CharacterSelectSlotStride;
        byte* box = stackalloc byte[20];
        NativeMemory.Clear(box, 20);
        *(uint**)box = ((uint**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94))[0];
        *(uint*)(box + 4) = 0x1000000;
        *(short*)(box + 8) = *(short*)(row + 4);
        *(short*)(box + 0xa) = (short)(*(short*)(row + 6) + 0xc);
        *(short*)(box + 0x10) = 0x18;

        primCursor = Prims.DrawSpriteVariant(otBase, primCursor, (int*)box, 0, 0);
        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, (int*)box);
        EmitClipRect(otBase, &primCursor);

        BindMenuLabels(
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x1e0 + state[8] * 4),
            0, Mem.s_StatusDescriptor_00560728, 0);
        return Ui.RunTextEngine(primCursor, Mem.s_StatusDescriptor_00560728);
    }

    // Ghidra: &DAT_005ea560 — la table des blocs de statistiques des entités **hors groupe**
    // (monstres, PNJ), 0x120 octets par entrée. Deux bornes concordantes donnent 32 entrées :
    //  - le voisinage dans .data — les symboles vont de 0x5ea560 à 0x5ea663 (tous dans la première
    //    entrée, 0x5ea663 étant son décalage +0x103, celui que FUN_00538830 écrit), et le groupe
    //    suivant ne reprend qu'à 0x5eca80 ; 32 * 0x120 = 0x2400 tient exactement dans l'intervalle ;
    //  - la boucle d'initialisation en tête de FUN_00537da0, qui parcourt **0x20** emplacements
    //    d'entité de 0x9c octets.
    private static readonly byte* s_EntityStatBlocks_005ea560 =
        (byte*)NativeMemory.AllocZeroed(0x20 * 0x120);

    /// <summary>
    /// Ghidra: FUN_00538780 @ 0x00538780 — rend le bloc de statistiques d'une entité, en choisissant
    /// entre les deux endroits où ils vivent : le bit <c>0x800000</c> du mot d'état de l'entité
    /// distingue un **membre du groupe**, dont le bloc est dans son enregistrement de 0x2000 octets
    /// (<c>MainMem + 0xf0100 + index * 0x2000</c>), de toute autre entité, logée dans la table
    /// compacte <see cref="s_EntityStatBlocks_005ea560"/> à 0x120 octets par entrée.
    ///
    /// <para>L'index est l'octet de poids faible du dword <c>+8</c> de l'entité, le même que
    /// <c>FUN_005387c0</c> et <c>FUN_00538830</c> lisent. Recoupement utile : <c>FUN_005387c0</c>
    /// écrit la même adresse sous la forme <c>(index + 0x78) * 0x2000 + MainMem</c>, et
    /// 0x78 * 0x2000 vaut précisément 0xf0000 — les deux formes désignent bien le même
    /// enregistrement.</para>
    /// </summary>
    private static byte* GetEntityStatBlock(uint* entity)
    {
        if ((entity[0] & 0x800000) != 0)
            return GetMainMemPtr() + 0xf0100 + (byte)entity[2] * 0x2000;

        return s_EntityStatBlocks_005ea560 + (byte)entity[2] * 0x120;
    }

    /// <summary>
    /// Ghidra: FUN_00538ab0 @ 0x00538ab0 — copie quatre dwords de <c>param+0x280</c> vers
    /// <c>param+0x120</c>. Corps trivial et autonome (pas d'appel, pas de branche), donc
    /// transcrit directement plutôt que déclaré non résolu comme ses quatre voisins.
    ///
    /// <para>Appelée à plusieurs dizaines de sites dans <c>Blaze.c</c> sur toutes sortes
    /// d'enregistrements (pas seulement le personnage en cours de création) : un « commit » de
    /// portée générale, probablement la copie du bloc de statistiques préparé (<c>+0x280</c>,
    /// posé juste avant par <see cref="FinalizeCharacterStats"/>) vers l'emplacement que le reste
    /// du moteur relit (<c>+0x120</c>). Nommée sur ce rôle plutôt que sur sa seule occurrence
    /// ici ; à revoir si un autre site l'utilise autrement.</para>
    /// </summary>
    private static void CommitStagedStatBlock(byte* record)
    {
        *(uint*)(record + 0x120) = *(uint*)(record + 0x280);
        *(uint*)(record + 0x124) = *(uint*)(record + 0x284);
        *(uint*)(record + 0x128) = *(uint*)(record + 0x288);
        *(uint*)(record + 0x12c) = *(uint*)(record + 0x28c);
    }

    /// <summary>Ghidra: la branche « confirmer » de <c>case 0x3e</c>, atteinte quand le pool de
    /// points est à zéro et que le bouton de confirmation est pressé. Prépare l'enregistrement du
    /// personnage (mêmes conventions que <c>s_UiDescriptors_00560ff0</c>), copie le portrait,
    /// calcule les statistiques dérivées et le code à sept caractères. Ne fait <b>pas</b> sortir
    /// l'écran elle-même : <c>state[2]</c> passe à <c>0x81</c>, et c'est ce sous-état de phase
    /// (déjà câblé) qui avance <c>state[0]</c> vers le sous-état 7 à l'image suivante.</summary>
    private static void FinalizeCharacterStats(byte* pad, byte* scratch, int* state)
    {
        *(ushort*)(pad + 2) &= 0x7fff;
        PlayUiSound38();
        state[2] = 0x81;

        *(uint*)scratch = 0;
        *(uint*)(scratch + 0x140) = 0;
        *(short*)(scratch + 0x144) = 1;
        *(short*)(scratch + 0x146) = 1;
        *(uint*)(scratch + 0x158) = 0;
        *(uint*)(scratch + 0x15c) = 0;
        *(uint*)(scratch + 0x114) = (uint)(1 << (scratch[0x151] & 0x1f)) | 0x800;
        *(short*)(scratch + 0x112) = (short)(1 << (scratch[0x150] & 7));

        for (int i = 0; i < 7; i++)
        {
            ushort value = Mem.s_PanelSelection_005e9060[i];
            *(ushort*)(scratch + 0x280 + i * 2) = value;
            *(ushort*)(scratch + 0x2a0 + i * 2) = value;
        }

        byte* stats = Mem.s_PanelSelection_005e9060;
        *(ushort*)(scratch + 0x148) = stats[7];   // la teinte animée par EditCharacterStats
        *(ushort*)(scratch + 0x14a) = stats[8];
        *(ushort*)(scratch + 0x14c) = *(ushort*)(scratch + 0x148);
        *(ushort*)(scratch + 0x14e) = *(ushort*)(scratch + 0x14a);

        *(uint*)(scratch + 0x26c) = 0;
        scratch[0x2b0] = 0;
        scratch[0x2b1] = 1;
        scratch[0x2b2] = (byte)(((scratch[0x150] & 7) == 0 || (scratch[0x150] & 7) == 6) ? 2 : 0xff);
        scratch[0x2b3] = 0xff;
        scratch[0x2b4] = 0xff;
        scratch[0x2b5] = s_ClassStartIcon_00562ab8[scratch[0x150] & 7];
        scratch[0x2b6] = 0; scratch[0x2b7] = 0; scratch[0x2b8] = 0; scratch[0x2b9] = 0;
        scratch[0x2ba] = 0; scratch[0x2bb] = 0;

        scratch[0x160] = 1; scratch[0x161] = 0; scratch[0x162] = 0; scratch[0x163] = 0;
        scratch[0x168] = 1; scratch[0x169] = 0; scratch[0x16a] = 0; scratch[0x16b] = 0;
        scratch[0x170] = 1; scratch[0x171] = 0; scratch[0x172] = 0; scratch[0x173] = 0;
        scratch[0x178] = 3; scratch[0x179] = 3; scratch[0x17a] = 3; scratch[0x17b] = 3;
        scratch[0x17c] = 3; scratch[0x17d] = 3; scratch[0x17e] = 3;

        NativeMemory.Clear(scratch + 0x1f00, 4);
        NativeMemory.Clear(scratch + 0x1d00, 0x40);
        NativeMemory.Clear(scratch + 0x1d40, 0x40);
        NativeMemory.Clear(scratch + 0x1d80, 0x40);
        NativeMemory.Clear(scratch + 0x1dc0, 0x40);
        NativeMemory.Clear(scratch + 0x1e00, 0x40);
        NativeMemory.Clear(scratch + 0x1e40, 0x40);
        NativeMemory.Clear(scratch + 0x1e80, 0x40);
        NativeMemory.Clear(scratch + 0x1ec0, 0x40);

        for (int i = 0; i < 0x40; i += 8)
        {
            *(uint*)(scratch + 0x1f04 + i) = 0;
            *(uint*)(scratch + 0x1f08 + i) = 0xffffffff;
        }

        NativeMemory.Clear(scratch + 0x2e0, 0x20);

        for (int i = 0; i < 8; i++)
        {
            *(ushort*)(scratch + 0x1fc0 + i * 2) = s_StatBitTable_00562ac8[s_StatBitRemap_00562ac0[i]];
        }

        byte* texture = GetMainMemPtr() + ((scratch[0x150] & 7) + 0x7c) * 0x2000;
        Mem.CopyDwords((uint*)texture, (uint*)(scratch + 0x300), 0x640);

        for (int i = 0; i < 0x1900; i += 0x280)
        {
            *(ushort*)(scratch + i + 0x312) |= 0x8000;
        }

        CommitStagedStatBlock(scratch);
        ScaleStatBlockByDifficulty(scratch + 0x100);
        ComputeDerivedStats(scratch);
        ApplyEquipmentStatBonuses(scratch);
        FinalizeEquipmentEffects(scratch + 0x100);

        // Ghidra: la somme de contrôle du code à sept caractères — un accumulateur
        // « + puis ×2 » sur scratch[0x100..0x110), les seize octets du **nom tapé** au clavier
        // virtuel (HandlePadNameEntry/HandleTypedNameEntry les y écrivent un par un, et
        // Saves.WriteCardSaveFile les relit au même endroit pour l'en-tête du fichier), puis sur les sept
        // statistiques brutes à scratch+0x280, repliée avec la classe/l'élément/le style choisis.
        // Le nom entre donc bien dans le code : deux personnages ne partagent leur fichier de
        // sauvegarde que s'ils ont à la fois le même nom, la même classe, le même élément, le même
        // style et exactement la même répartition de points (legacy-bug.md, LB-002).
        int hash = 0;
        for (int i = 0; i < 0x10; i++) hash = (hash + scratch[0x100 + i]) * 2;

        short* stagedStats = (short*)(scratch + 0x280);
        for (int i = 0; i < 7; i++) hash = (hash + stagedStats[i]) * 2;

        int folded = (scratch[0x150] + hash) * 2;
        folded = (scratch[0x151] + folded) * 2;
        folded = (scratch[0x152] + folded) * 2;
        folded = (scratch[0x153] + folded) * 2;

        // Sept caractères, cinq bits de `folded` à la fois. Ghidra vérifie aussi l'octet juste
        // après le septième contre '\\' avant de l'écraser par le terminateur nul qui suit
        // immédiatement — un test dont le résultat est toujours effacé par l'écriture d'après,
        // donc sans effet observable ; élidé ici plutôt que transcrit pour rien.
        byte* code = s_SaveNameTemplate_00568498;
        for (int i = 0; i < 7; i++)
        {
            byte c = (byte)((folded & 0x1f) + 0x40);
            code[0xc + i] = c == (byte)'\\' ? (byte)'7' : c;
            folded >>= 5;
        }
        code[0x13] = 0;

        WriteCharacterCode(scratch, code);
    }

    /// <summary>
    /// Ghidra: FUN_00538f50 @ 0x00538f50 — remet à l'échelle huit statistiques à
    /// <c>record+0x20</c> selon un facteur de difficulté à <c>record+0xec</c> :
    /// <c>valeur *= (0x1000 - facteur*0x200) / 0x1000</c>, bornée à <c>[0, 999]</c>. Facteur à
    /// zéro (rien n'écrit encore <c>DAT_005de034</c>/<c>scratch+0x1ec</c> dans le code déjà
    /// reconstruit) laisse les valeurs inchangées : la multiplication par <c>0x1000</c> suivie du
    /// décalage de 12 bits est l'identité.
    /// </summary>
    private static void ScaleStatBlockByDifficulty(byte* record)
    {
        ushort factor = *(ushort*)(record + 0xec);
        short* stats = (short*)(record + 0x20);
        for (int i = 0; i < 8; i++)
        {
            int scaled = ((0x1000 - factor * 0x200) * stats[i]) >> 0xc;
            if (scaled < 0) scaled = 0;
            if (scaled > 999) scaled = 999;
            stats[i] = (short)scaled;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00538c50 @ 0x00538c50 — calcule huit statistiques dérivées à
    /// <c>record+0x290..0x29f</c> à partir des huit statistiques primaires à
    /// <c>record+0x120..0x12f</c> (celles que <see cref="CommitStagedStatBlock"/> vient d'y
    /// déposer), les borne à 999, puis les recopie sur <c>record+0x130..0x13f</c>.
    ///
    /// <para>Chaque formule est une combinaison pondérée de deux ou trois statistiques primaires
    /// plus <c>record+0x144</c> (une neuvième valeur, hors du bloc de sept), transcrite opération
    /// pour opération — la précédence de <c>&gt;&gt;</c> sous celle de <c>+</c> en C comme en C#
    /// donne le même groupement, explicité ici par des parenthèses plutôt que laissé implicite.
    /// Aucune ne correspond à une formule de jeu connue par ailleurs ; nommer les statistiques
    /// dérivées attendra qu'on les voie affichées quelque part.</para>
    /// </summary>
    private static void ComputeDerivedStats(byte* record)
    {
        int extra = *(ushort*)(record + 0x144);
        int dword1 = *(int*)(record + 0x124);
        int stat0 = *(ushort*)(record + 0x120);
        int stat1 = *(ushort*)(record + 0x122);
        int stat6 = *(ushort*)(record + 0x12c);
        int dword2 = *(int*)(record + 0x128);
        int stat3 = dword1 >> 0x10;
        int stat4 = dword2 & 0xffff;
        int stat5 = dword2 >> 0x10;

        *(short*)(record + 0x290) = (short)((extra + stat0) >> 2);
        *(short*)(record + 0x292) =
            (short)((((stat4 >> 2) + stat3 + stat4) >> 2) + extra >> 2);
        *(short*)(record + 0x294) = (short)(((stat0 + stat3) >> 1) + extra >> 2);

        int halfExtra = extra >> 1;
        *(short*)(record + 0x296) =
            (short)((((dword1 >> 0x12) + stat3 + stat4) >> 1) + halfExtra >> 2);
        *(short*)(record + 0x298) = (short)((extra + stat5) >> 2);

        int stat2 = dword1 & 0xffff;
        *(short*)(record + 0x29e) =
            (short)((((stat2 >> 2) + stat2 + stat1) >> 1) + halfExtra >> 2);
        *(short*)(record + 0x29a) =
            (short)((((stat1 >> 2) + stat2 + stat1) >> 2) + extra >> 2);
        *(short*)(record + 0x29c) = (short)(((stat5 + stat2) >> 1) + extra >> 2);

        short* derived = (short*)(record + 0x290);
        for (int i = 0; i < 8; i++)
        {
            if (derived[i] > 999) derived[i] = 999;
        }

        *(uint*)(record + 0x210) = 0;
        *(uint*)(record + 0x214) = 0;
        *(uint*)(record + 0x218) = 0;
        *(uint*)(record + 0x21c) = 0;

        int slot7 = (stat6 >> 5) + (*(short*)(record + 0x2a6) >> 3);
        *(ushort*)(record + 0x28e) = (ushort)slot7;
        if (slot7 > 0xf) *(ushort*)(record + 0x28e) = 0xf;

        *(ushort*)(record + 0x154) = (ushort)(*(short*)(record + 0x2a2) + (stat1 >> 3));
        *(short*)(record + 0x156) = (short)(*(short*)(record + 0x2aa) >> 2);

        Mem.CopyDwords((uint*)(record + 0x290), (uint*)(record + 0x130), 4);
    }

    // Ghidra: &DAT_0056d2c0 — seize octets, une prime en pourcent par palier de difficulté,
    // indexée par DAT_005de034. Relevé au décalage fichier 0x16D2C0.
    private static readonly byte[] s_DifficultyBonus_0056d2c0 =
        [0x00, 0x00, 0x0a, 0x14, 0x1e, 0x00, 0x00, 0x00, 0x00, 0x02, 0x03, 0x01, 0x04, 0x01, 0x00, 0x00];

    // Ghidra: DAT_005de034 — le palier de difficulté courant. Jamais posé par du code déjà
    // reconstruit, donc toujours 0 (aucune prime) pour l'instant.
    private static int s_Unknown_005de034;

    /// <summary>
    /// Ghidra: FUN_00538e20 @ 0x00538e20 — applique les bonus des cinq emplacements d'équipement
    /// à <c>record+0x2b0..0x2b4</c> (<c>0xff</c> = vide) puis remet à l'échelle par le palier de
    /// difficulté. Chaque emplacement pointe, via <c>record+0x300+id*0x280</c>, dans le bloc
    /// texture/objet chargé plus tôt par <see cref="FinalizeCharacterStats"/> (le même
    /// <c>CopyDwords</c> qui copie <c>0x640</c> dwords depuis <c>MainMem</c>) : ce n'est pas
    /// seulement une texture, c'est un tableau d'enregistrements de <c>0x280</c> octets, un par
    /// objet, dont les seize premiers mots à <c>+0x20</c> sont des bonus de statistiques.
    ///
    /// <para>Avec l'équipement par défaut posé par <c>FinalizeCharacterStats</c>
    /// (<c>scratch[0x2b0..1]=0,1</c> toujours actifs, <c>[0x2b2]</c> selon la classe,
    /// <c>[0x2b3..4]=0xff</c> toujours vides), les deux derniers emplacements — et donc la
    /// branche des huit octets à <c>+0x210</c> qui ne se déclenche que pour eux — restent morts
    /// tant qu'aucun autre code ne pose un objet dans ces emplacements.</para>
    /// </summary>
    private static void ApplyEquipmentStatBonuses(byte* record)
    {
        byte* slots = record + 0x2b0;
        for (int i = 0; i < 5; i++)
        {
            byte itemId = slots[i];
            if (itemId == 0xff) continue;

            byte* item = record + 0x300 + itemId * 0x280;
            short* primary = (short*)(record + 0x120);
            short* bonus = (short*)(item + 0x20);
            for (int k = 0; k < 0x10; k++) primary[k] += bonus[k];

            if (i == 3 || i == 4)
            {
                short* extra = (short*)(record + 0x210);
                for (int k = 0; k < 8; k++) extra[k] += (sbyte)item[0x18 + k];
            }

            *(short*)(record + 0x156) += *(short*)(item + 0x8a);
        }

        byte difficultyBonus = s_DifficultyBonus_0056d2c0[s_Unknown_005de034];
        int scale = difficultyBonus + 100;
        short* stats = (short*)(record + 0x130);
        for (int k = 0; k < 8; k++) stats[k] = DivideRoundedBy100(scale * stats[k]);

        int finalScaled = scale * *(short*)(record + 0x156);
        *(short*)(record + 0x12e) += (short)(difficultyBonus >> 3);
        short result = DivideRoundedBy100(finalScaled);
        if (result < 0) result = 0;
        if (result > 99) result = 99;
        *(short*)(record + 0x156) = result;
    }

    /// <summary>L'idiome de division par 100 signée que Ghidra reconstruit avec un multiplicateur
    /// magique et une correction de signe — transcrit terme à terme plutôt que simplifié, cette
    /// famille de fonctions n'ayant pas été vérifiée pour tous les cas limites (négatifs,
    /// débordement).</summary>
    private static short DivideRoundedBy100(int value)
    {
        return (short)(((short)(value / 100) + (short)(value >> 0x1f))
            - (short)(((long)value * 0x51eb851f) >> 0x3f));
    }

    /// <summary>
    /// Ghidra: FUN_00538fa0 @ 0x00538fa0 — appelée sur <c>scratch+0x100</c>, donc les décalages
    /// ci-dessous (relatifs à <c>record</c>) tombent sur <c>scratch+0x108..0x156</c>. Choisit un
    /// octet de remplissage (0x00, 0x01 ou 0xff, répété sur quatre octets) selon deux bits de
    /// <c>record+0x40</c> (= <c>scratch+0x140</c>, mis à zéro par <c>FinalizeCharacterStats</c> —
    /// donc toujours le cas « 0 » ici), pose huit compteurs à <c>record+0x108..0x10f</c> que
    /// d'autres bits du même mot incrémentent ou décrémentent, puis relit ces compteurs pour
    /// mettre à l'échelle (×1,5 ou ×0,5) les huit statistiques dérivées à <c>record+0x30</c>
    /// (<c>scratch+0x130</c>, celles que <see cref="ApplyEquipmentStatBonuses"/> vient de
    /// calculer). Termine par un dernier plafonnement <c>[0, 999]</c> des seize statistiques à
    /// <c>record+0x20..0x3f</c>.
    ///
    /// <para>Tous les octets sources (<c>record+0x40</c>, <c>+0x86..0x8a</c>, <c>+0xba</c>) sont
    /// à zéro tant que rien d'autre ne les pose — <c>scratch+0x100..0x1ff</c> reste vierge après
    /// le nettoyage de <c>RunCharacterSelectFrameLoop</c>, aucun sous-état ne l'utilise encore.
    /// La fonction est donc actuellement un plafonnement pur, mais transcrite branche pour
    /// branche plutôt que réduite à ça : rien ne garantit que ce sera encore vrai une fois
    /// l'équipement ou un autre sous-état câblés dessus.</para>
    /// </summary>
    private static void FinalizeEquipmentEffects(byte* record)
    {
        uint flags = *(uint*)(record + 0x40);
        uint masked = flags & 0x10200000;
        // Les quatre valeurs possibles de `masked` (deux bits) sont toutes couvertes ; le `_`
        // reproduit le `iVar7 = param_1` initial de l'original, qu'aucune branche n'atteint.
        int fill = masked switch
        {
            0 => 0,
            0x200000 => 0x1010101,
            0x10000000 => -1,
            0x10200000 => 0,
            _ => 0,
        };

        byte* counters = record + 0x108;
        *(int*)(record + 0x10c) = fill;
        *(int*)counters = fill;

        if ((flags & 0x400) != 0) counters[0]++;
        if ((flags & 0x1000) != 0) counters[1]++;
        if ((flags & 0x10000) != 0) counters[4]++;
        if ((flags & 0x40000) != 0) counters[5]++;
        if ((flags & 0x800) != 0) counters[0]--;
        if ((flags & 0x2000) != 0) counters[1]--;
        if ((flags & 0x20000) != 0) counters[4]--;
        if ((flags & 0x80000) != 0) counters[5]--;
        if ((flags & 0x200) != 0) { counters[0]++; counters[1]--; }

        if (*(short*)(record + 0x86) != 0) *(short*)(record + 0x110) += *(short*)(record + 0xe4);
        if (*(short*)(record + 0x8c) != 0) *(short*)(record + 0x112) += *(short*)(record + 0xea);
        if (*(short*)(record + 0x88) != 0) *(short*)(record + 0x114) += *(short*)(record + 0xe6);
        if (*(short*)(record + 0x8a) != 0) *(short*)(record + 0x116) += *(short*)(record + 0xe8);

        short* derived = (short*)(record + 0x30);
        for (int i = 0; i < 8; i++)
        {
            if (counters[i] == 0) continue;
            derived[i] = (counters[i] & 0x80) == 0
                ? (short)((derived[i] >> 1) + derived[i])
                : (short)(derived[i] >> 1);
        }

        if (*(short*)(record + 0xba) != 0)
        {
            ushort index = *(ushort*)(record + 0xf8);
            short delta = *(short*)(record + 0xfa);
            if (index < 7)
            {
                *(short*)(record + 0x20 + index * 2) += delta;
            }
            else if (index < 8)
            {
                *(short*)(record + 0x2e) += delta;
            }
            else if (index < 0x10)
            {
                *(short*)(record + 0x20 + index * 2) += delta;
            }
            else if (index < 0x18)
            {
                *(short*)(record + 0xf0 + index * 2) += delta;
            }
        }

        short* primary = (short*)(record + 0x20);
        for (int i = 0; i < 0x10; i++)
        {
            short value = primary[i];
            if (value < 0) value = 0;
            if (value > 999) value = 999;
            primary[i] = value;
        }
    }

    /// <summary>Ghidra: la branche « annuler » de <c>case 0x5d</c> — un code placeholder
    /// "0000000" puis la sortie de l'écran entier. Chemin qui n'a jamais eu besoin des cinq
    /// fonctions de <see cref="FinalizeCharacterStats"/>, désormais toutes transcrites.</summary>
    private static void CancelCharacterCreation(byte* scratch, int* state)
    {
        byte* code = s_SaveNameTemplate_00568498;
        for (int i = 0; i < 7; i++) code[0xc + i] = (byte)'0';
        code[0x13] = 0;

        WriteCharacterCode(scratch, code);

        s_Unknown_005de51c = 1;
        state[0] = -1;
        s_GameStateId = 1;
    }

    /// <summary>Ghidra: la boucle de recopie partagée par les deux branches ci-dessus — copie
    /// depuis le début de <c>code</c> jusqu'au premier octet nul, vers DAT_00573338+0x1fd0. Le
    /// départ à l'octet 0 (et non 0xc, où les sept caractères de code viennent d'être écrits) est
    /// bien celui de l'original : c'est ce qui emporte le préfixe « Blaze-Saving » du gabarit
    /// <see cref="s_SaveNameTemplate_00568498"/> dans le nom de fichier final.</summary>
    private static void WriteCharacterCode(byte* scratch, byte* code)
    {
        int i = 0;
        byte* cursor = code;
        while (*cursor != 0)
        {
            scratch[0x1fd0 + i] = *cursor;
            i++;
            cursor++;
        }
        scratch[0x1fd0 + i] = 0;
    }

    /// <summary>Le rendu partagé de fin de fonction (<c>LAB_0050a5e0</c>) : l'en-tête, puis les
    /// neuf lignes (sept statistiques, le total, la confirmation).</summary>
    private static uint* RenderStatRows(uint* otBase, uint* primCursor)
    {
        byte* directory = Mem.s_MainResourceDirectory_005e97c4;

        byte* label = Mem.s_UiDescriptors_00560ff0 + 2 * GameMemory.UiDescriptorStride;   // &DAT_00562180
        BindMenuLabels(*(byte**)(*(byte**)(directory + 0x70) + 0x10c), 0, label, 0);
        primCursor = Ui.RunTextEngine(primCursor, label);

        byte* box = stackalloc byte[20];
        NativeMemory.Clear(box, 20);
        byte* atlasPtr = *(byte**)(directory + 0x94);
        *(uint*)box = *(uint*)(atlasPtr + 0x30);
        *(uint*)(box + 4) = 0x1000000;

        byte* labelTable = *(byte**)(directory + 0x70);
        int labelOffset = 0x110;

        for (int i = 0; i < 9; i++)
        {
            byte* row = s_SelectSlots_00573518 + i * CharacterSelectSlotStride;
            *(short*)(box + 8) = (short)(*(short*)(row + 4) + 0xc);
            *(short*)(box + 0xa) = (short)(*(short*)(row + 6) + 4);
            *(short*)(box + 0x10) = (short)(i + 4);

            EmitViewportRect(otBase, &primCursor);
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, (int*)box, 0, 0);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, (int*)box);
            EmitClipRect(otBase, &primCursor);

            byte funcByte = (byte)(i <= 6 ? 0x08 : 0x10);
            byte statValue = Mem.s_PanelSelection_005e9060[i];
            if (statValue < 100) funcByte += 8;
            if (statValue < 10) funcByte += 8;
            row[0x20] = funcByte;

            BindMenuLabels(*(byte**)(labelTable + labelOffset), 0, row, 0);
            primCursor = Ui.RunTextEngine(primCursor, row);
            primCursor = DrawUiPanel(otBase, primCursor, row);

            labelOffset += 4;
        }

        return primCursor;
    }

    // ─────────────────────────────────────────────────────────────────────
    // Sous-état 7 — l'écran de sauvegarde/chargement (carte mémoire)
    // ─────────────────────────────────────────────────────────────────────

    // Ghidra: DAT_00578de0 — le résultat du dernier appel carte (FUN_00504820), -3/-4 étant des
    // codes d'erreur spécifiques. Jamais écrit par du code déjà reconstruit ; lit 0.
    private static int s_CardLoadResult_00578de0;
    // Deplace dans GameMemory le 2026-08-19 : globale .data.
    private static int s_CardPrevPhase_00578e10;
    private static int s_CardPhaseChanged_00578e14;
    // Ghidra: DAT_00578e18 — le curseur (emplacement de sauvegarde en surbrillance).
    private static int s_CardCursor_00578e18;
    // Ghidra: DAT_00578e20 — 0..3 selon quelles manettes sont branchées (ports 1 et 2), sélectionne
    // laquelle des quatre entrées de s_CardDirectorySize_00562b08 / s_CardSlotFlags_00562adc lire.
    private static int s_CardPort_00578e20;

    // Ghidra: &DAT_00562b30 (quatre pointeurs) → &DAT_00562b08 (les quatre blocs eux-mêmes, pas de
    // pointeurs intermédiaires ici : la mémoire est statique, jamais réécrite par du code non
    // reconstruit). Premier octet de chaque bloc = le nombre d'emplacements de sauvegarde ; le
    // reste, non lu par cette fonction. Relevé au décalage fichier 0x161108.
    private static readonly byte[][] s_CardDirectorySize_00562b08 =
    [
        [0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00],
        [0x05, 0x02, 0x03, 0x04, 0x05, 0x01, 0x00, 0x00],
        [0x05, 0x00, 0x06, 0x07, 0x08, 0x09, 0x00, 0x00],
        [0x08, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08],
    ];

    // Ghidra: &DAT_00562af8 (quatre pointeurs) → &DAT_00562adc, même remarque. Un octet de
    // drapeaux par emplacement de sauvegarde. Relevé au décalage fichier 0x1610dc.
    private static readonly byte[][] s_CardSlotFlags_00562adc =
    [
        [0x00, 0x04, 0x00, 0x00],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x00, 0x00, 0x00],
        [0x00, 0x04, 0x05, 0x06, 0x07, 0x00, 0x00, 0x00],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07],
    ];

    // Ghidra: &DAT_00572b40 — seize octets juste avant s_SceneEntity_00572b48, confirmés distincts
    // de cette entité (voir son commentaire). Relevés au décalage fichier 0x172740 ; passés tels
    // quels à BindMenuLabels comme une étiquette « vide » par défaut à plusieurs points de
    // FUN_0050a910.
    private static readonly byte* s_EmptyLabel_00572b40 = BuildEmptyLabel00572b40();

    private static byte* BuildEmptyLabel00572b40()
    {
        byte[] bytes = [0x00, 0x18, 0x18, 0x00, 0x10, 0x29, 0xac, 0x00,
                        0x31, 0x29, 0x31, 0x00, 0x52, 0x7b, 0x7b, 0x00];
        byte* buffer = (byte*)NativeMemory.Alloc((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) buffer[i] = bytes[i];
        return buffer;
    }

    // `ThrowSaveCardUnresolved` a été retiré le 2026-08-18 : ses six sites d'appel (cases 4, 0xd,
    // 0x10, -5, -3/-2 de FUN_0050a910) sont tous transcrits, la machine à phases de l'écran de
    // sauvegarde n'a plus de trou.
}
