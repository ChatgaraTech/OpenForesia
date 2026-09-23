// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// ============================================================================================
// LES GLOBALES DE `winsys3d`, ALIASEES SUR LE `.data` REEL DE LA DLL CHARGEE
//
// **Ce fichier ne declare aucune variable.** Chaque « globale » ci-dessous est une propriete
// `ref` qui designe l'octet exact du segment de donnees de `winsys3d.dll`, telle qu'elle est en
// memoire. Un corps porte et son voisin reste natif voient donc LA MEME memoire, la meme qu'en
// 1998.
//
// ============================================================================================
// POURQUOI, ET POURQUOI C'EST UN TROISIEME CAS QUE `LISEZMOI.md` NE PREVOYAIT PAS
//
// La regle 2 du repertoire dit : « les globales d'une DLL ne rejoignent jamais `GameMemory` ».
// Elle vise a ne pas melanger deux espaces d'adressage, et elle reste vraie. Elle suppose en
// revanche qu'une DLL se porte ENTIEREMENT, d'un bloc, avec ses globales recreees en C#.
//
// Pour le chemin d'affichage, ce bloc pese **157 corps et 12 408 lignes** — voir
// `Documentation/Etat/CheminAffichage.md` — et il est indivisible en transcription classique :
// a etat inchange un producteur **allonge l'enregistrement precedent** du paquet
// (`paquet[curseur - 1] += n`), donc deux moities ne peuvent pas coexister avec deux copies des
// memos. Trois decoupages plus courts ont ete essayes et mesures, tous faux :
//
//     `DAT_011be760` seul   detache 1 corps sur 425
//     le tampon seul        sa base est aussi celle du rejeu
//     le rejeu seul         sa cloture contient 20 des 38 producteurs
//
// L'aliasing leve exactement cette contrainte. **Il n'y a plus deux copies : il n'y en a
// qu'une**, celle de la DLL. Le lot redevient divisible, corps par corps, et chaque etape se
// verifie a l'ecran au lieu d'attendre la douze-millieme ligne.
//
// Ce n'est pas un precedent nouveau, c'est la generalisation de deux choses qui tournent deja :
//
//   - l'ordinal 750 rend `DAT_011be760`, et `Screens/State00/Options.cs` s'en sert pour appeler
//     la vtable du device directement depuis `src/Blaze/`, depuis le 2026-08-12 ;
//   - `Platform/Winsys3d.cs` resout `MainMem`, `VSyncDelayed` et `TexFilterPossible` par
//     `GetProcAddress` et les lit **dans la DLL**, parce que P/Invoke ne sait pas importer une
//     donnee. Ce fichier fait la meme chose pour les globales qui n'ont pas de nom d'export.
//
// ============================================================================================
// LA REGLE QUI VA AVEC, ET QUI N'EST PAS NEGOCIABLE
//
// **Une globale aliasee ne se transcrit pas, elle se designe.** Tant qu'un seul corps de
// `winsys3d.dll` reste natif, aucune de ces adresses ne doit exister en double cote C# : une
// copie serait morte a la seconde ou le natif ecrirait la vraie.
//
// **Et le jour ou la DLL partira entierement**, ce fichier se remplace par de vraies variables,
// d'un seul geste et sans toucher aux appelants — c'est pour cela que tout passe par des
// proprietes et que personne ne manipule `Base` directement.
//
// ============================================================================================
// LES ADRESSES
//
// `winsys3d.dll` a pour `ImageBase` 0x00a00000, mais elle porte une section `.reloc` : Windows
// peut la charger ailleurs. **On ne se sert donc jamais de la VA telle quelle** — on prend la
// base reelle rendue par `GetModuleHandleA` et on y ajoute la RVA, soit `VA - 0x00a00000`.
//
// Les quatre regions en jeu, toutes dans `.data` (VA 0x00a2c000 .. 0x01214214, 8,2 Mo dont
// l'essentiel est non initialise dans le fichier) :
//
//     0x00a2c4xx   les memos d'etat du tampon de commandes
//     0x00b6dbd4   `MainMem`, le pointeur d'arene de la DLL
//     0x00dedbxx   l'arene de travail par image, en double tampon
//     0x011be7xx   le paquet courant, le curseur, le device, les rappels
// ============================================================================================

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dData
{
    /// <summary>L'`ImageBase` du PE, celle a laquelle les VA de `winsys3d.c` sont exprimees.</summary>
    private const nuint ImageBase = 0x00a00000;

    private static byte* s_Base;

    /// <summary>
    /// La base de chargement REELLE de `winsys3d.dll`. Resolue paresseusement et non dans un
    /// initialiseur statique : la DLL n'est pas forcement chargee quand cette classe l'est, et le
    /// depot a deja paye une fois le prix des initialiseurs statiques dont l'ordre n'est pas
    /// specifie (le decoupage de `WorldMapTick.cs`, huit champs rendus `null + offset`).
    /// </summary>
    private static byte* Base
    {
        get
        {
            if (s_Base != null) return s_Base;

            IntPtr module = Win32.GetModuleHandleA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero) module = Win32.LoadLibraryA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero)
                throw new InvalidOperationException(
                    "winsys3d.dll n'est pas chargee : les globales aliasees de Libraries/winsys3d " +
                    "n'ont pas d'adresse. Voir src/Libraries/winsys3d/Data.cs.");

            s_Base = (byte*)module;
            return s_Base;
        }
    }

    /// <summary>Une VA de `winsys3d.c` traduite en adresse reelle. `VA - 0x00a00000` est la RVA.</summary>
    private static byte* At(nuint va) => Base + (va - ImageBase);

    // ----------------------------------------------------------------------------------------
    // Les memos d'etat du tampon de commandes. Le coeur du perimetre : ce sont EUX qui rendent
    // le chemin d'affichage indivisible, parce qu'ils ne disent pas seulement « cet etat est deja
    // pose », ils disent « et l'enregistrement qui le porte est celui-ci, allonge-le ».
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00a2c480 — l'etat de rendu courant. Remis a 8 par `DrawOTag` a chaque image.</summary>
    internal static ref int EtatRendu_00a2c480 => ref *(int*)At(0x00a2c480);

    /// <summary>DAT_00a2c484 — memo d'etat. Remis a -1 par `DrawOTag`.</summary>
    internal static ref int Memo_00a2c484 => ref *(int*)At(0x00a2c484);

    /// <summary>
    /// DAT_00a2c488 — **la page de texture liee** (le mot tpage d'une primitive texturee), avec
    /// deux valeurs reservees : -1 « aucune, en dessin immediat », -2 « aucune, dans le tampon de
    /// commandes ». Remis a -1 par `DrawOTag`. Nomme « le type de primitive courant » jusqu'au
    /// 2026-09-23 : c'etait faux, `FUN_00a08850` le compare a `*(ushort*)(prim + 10)` et le pose.
    /// </summary>
    internal static ref int PageTexture_00a2c488 => ref *(int*)At(0x00a2c488);

    /// <summary>DAT_00a2c48c — memo d'etat. Remis a -1 par `DrawOTag`.</summary>
    internal static ref int Memo_00a2c48c => ref *(int*)At(0x00a2c48c);

    // ----------------------------------------------------------------------------------------
    // L'arene de travail par image, et le pointeur d'arene de la DLL.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// `MainMem` @ 0x00b6dbd4 — le pointeur d'arene de la DLL, pose par son `entry` a
    /// `&amp;DAT_00b6dbd8`. C'est le meme que `Platform/Winsys3d.cs` resout par `GetProcAddress`
    /// sous son nom d'export ; ici on le prend par son adresse, pour ne pas dependre du nom.
    /// </summary>
    internal static ref byte* MainMem_00b6dbd4 => ref *(byte**)At(0x00b6dbd4);

    /// <summary>
    /// DAT_00dedbe4 — le **bascule de double tampon** de l'arene par image. `DrawOTag` fait
    /// `x = 0x40000 - x` a chaque appel : la valeur alterne entre 0 et 0x40000, et rien d'autre
    /// ne l'ecrit. Ghidra le rend en deux temps (`iVar1 = -x ; x = iVar1 + 0x40000`), ce qui est
    /// la meme chose.
    /// </summary>
    internal static ref int Bascule_00dedbe4 => ref *(int*)At(0x00dedbe4);

    /// <summary>DAT_00dedbd8 — le curseur d'allocation dans l'arene par image.</summary>
    internal static ref byte* Arene_00dedbd8 => ref *(byte**)At(0x00dedbd8);

    /// <summary>DAT_00dedbdc — sa copie, ecrite en meme temps et a la meme valeur.</summary>
    internal static ref byte* AreneDebut_00dedbdc => ref *(byte**)At(0x00dedbdc);

    /// <summary>
    /// DAT_00dedc44 — la garde d'entree de `DrawOTag` : non nul, l'image entiere est sautee.
    /// TODO(REVERSE): qui la pose. Aucun corps du perimetre ne l'ecrit.
    /// </summary>
    internal static ref int Garde_00dedc44 => ref *(int*)At(0x00dedc44);

    // ----------------------------------------------------------------------------------------
    // Le device, les compteurs et les rappels.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// DAT_011be67c — l'export de donnees `VSyncDelayed`. Non nul, l'image est en retard et tout
    /// le dessin est saute pour la rattraper. Ghidra le nomme deja `VSyncDelayed` dans le corps de
    /// `DrawOTag` ; `Platform/Winsys3d.cs` le resout par `GetProcAddress` sous le meme nom.
    /// </summary>
    internal static ref int VSyncDelayed_011be67c => ref *(int*)At(0x011be67c);

    /// <summary>DAT_011be6f0 — un octet : choisit le slot `+0x5c` (non nul) ou `+0x60` (nul).</summary>
    internal static ref byte ModeEffacement_011be6f0 => ref *At(0x011be6f0);

    /// <summary>
    /// DAT_011be754 — un rappel de fin d'image, appele sans argument s'il est non nul. Pose par
    /// un corps hors du perimetre. TODO(REVERSE): par qui, et pour quoi.
    /// </summary>
    internal static ref IntPtr RappelFinImage_011be754 => ref *(IntPtr*)At(0x011be754);

    /// <summary>DAT_011be75c — le compteur d'images, incremente par `DrawOTag`.</summary>
    internal static ref int CompteurImages_011be75c => ref *(int*)At(0x011be75c);

    /// <summary>
    /// DAT_011be760 — **le device**. Une poignee, pas un etat : depuis le 2026-08-12 elle pointe
    /// sur un objet C#, `Host/SyntheticDevice.cs` detournant `GetProcAddress` dans la table
    /// d'imports de `winsys3d` pour rendre sa propre fabrique. L'ecrivain unique est
    /// `FUN_00a06420` (ordinal 789), reste natif.
    /// </summary>
    internal static ref void* Device_011be760 => ref *(void**)At(0x011be760);

    /// <summary>
    /// La vtable du device, c'est-a-dire `*DAT_011be760` — **DEUX indirections depuis l'adresse de
    /// la globale**, pas une.
    ///
    /// Le binaire le dit en trois temps : `mov ecx,[11BE760h]` prend le device, `mov eax,[ecx]`
    /// prend sa vtable, `call [eax+0B0h]` appelle la case. En partant de l'ADRESSE de la globale il
    /// faut donc `*(void***)(*(void**)...)`.
    ///
    /// **Ce fichier a porte la version fausse le 2026-09-22**, avec une seule indirection :
    /// `*(void***)At(0x011be760)` rend le device lui-meme type `void**`, et `vtable[0x2c]` allait
    /// lire a `device + 0xb0` — dans l'objet, pas dans la vtable. Plantage a la premiere image.
    /// L'expression `*(void***)x` est juste dans `Options.cs` parce que le `x` y est la VALEUR que
    /// rend l'ordinal 750 ; la recopier telle quelle sur une ADRESSE enleve une indirection sans
    /// que rien ne le signale — les deux compilent, les deux sont des `void**`.
    /// </summary>
    internal static void** DeviceVtable => *(void***)(*(void**)At(0x011be760));

    // ----------------------------------------------------------------------------------------
    // Le tampon de commandes lui-meme — la piece autour de laquelle tout le perimetre est defini.
    // Le consommateur (`RejouerPaquet`/`RejouerPaquetCourant`, 2026-09-23) lit et ecrit
    // `PaquetCourant_011be7ac` ; le curseur et les sommets attendent les producteurs.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_011be7ac — le paquet courant : celui qu'on remplit, qu'on rejoue, qu'on libere.</summary>
    internal static ref uint* PaquetCourant_011be7ac => ref *(uint**)At(0x011be7ac);

    /// <summary>DAT_011be7dc — le curseur d'ecriture dans le paquet, en mots de 32 bits.</summary>
    internal static ref int Curseur_011be7dc => ref *(int*)At(0x011be7dc);

    /// <summary>DAT_011be7a8 — la zone de sommets, dans le meme bloc que le paquet.</summary>
    internal static ref float* Sommets_011be7a8 => ref *(float**)At(0x011be7a8);

    /// <summary>DAT_00a2c578 — la derniere commande d'etat ecrite dans le paquet.</summary>
    internal static ref int DerniereCommande_00a2c578 => ref *(int*)At(0x00a2c578);

    /// <summary>DAT_00a2c57c — son argument.</summary>
    internal static ref int DerniereCommandeArg_00a2c57c => ref *(int*)At(0x00a2c57c);

    // ----------------------------------------------------------------------------------------
    // Ce que le repartiteur de primitives (`FUN_00a13400`) lit et ecrit en plus.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// Elo_NoLighting @ 0x011be758 — le drapeau « pas d'eclairage ». Le repartiteur l'OU-e dans le
    /// code de primitive quand celui-ci est sous 0x80 : il **redirige vers la variante non
    /// eclairee** de chaque primitive, il ne se contente pas d'un test. Ghidra le nomme deja ainsi.
    /// </summary>
    internal static ref int EloNoLighting_011be758 => ref *(int*)At(0x011be758);

    /// <summary>
    /// _DAT_00a2a0b8 — **1/4096**, releve dans `.rdata` (0x39800000). C'est l'echelle de la
    /// virgule fixe 4.12 de la libgte, la meme que le `&gt;&gt; 12` de `ScaleMatrix`.
    /// </summary>
    internal static float EchelleFixe_00a2a0b8 => *(float*)At(0x00a2a0b8);

    /// <summary>
    /// DAT_00a2c5a0 — **la constante -1**, relevee dans `.data` (0xffffffff). Aucun corps de la
    /// DLL ne l'ecrit : c'est le marqueur « ce paquet ne porte pas de matrice ». Le repartiteur
    /// compare le mot de queue du paquet a cette valeur avant de poser une transformation.
    /// </summary>
    internal static int PasDeMatrice_00a2c5a0 => *(int*)At(0x00a2c5a0);

    /// <summary>DAT_011cb1e0 — non nul, les sprites sont **paves** ; nul, ils partent d'un bloc.</summary>
    internal static ref int Pavage_011cb1e0 => ref *(int*)At(0x011cb1e0);

    /// <summary>DAT_011cb1d8 — la largeur de tuile du pavage.</summary>
    internal static ref int LargeurTuile_011cb1d8 => ref *(int*)At(0x011cb1d8);

    /// <summary>DAT_011cb1dc — la hauteur de tuile du pavage.</summary>
    internal static ref int HauteurTuile_011cb1dc => ref *(int*)At(0x011cb1dc);

    /// <summary>_DAT_011cb1d0 et _DAT_011cb1d4 — les deux autres champs que l'opcode 0xe2 depaquete.</summary>
    internal static ref int Pavage_011cb1d0 => ref *(int*)At(0x011cb1d0);

    /// <summary>Voir <see cref="Pavage_011cb1d0"/>.</summary>
    internal static ref int Pavage_011cb1d4 => ref *(int*)At(0x011cb1d4);

    /// <summary>DAT_011be6ec — l'opcode 0xe1 y depose **un demi-mot**, pas un mot.</summary>
    internal static ref ushort Demi_011be6ec => ref *(ushort*)At(0x011be6ec);

    /// <summary>DAT_011cb1bc, DAT_011cb1b8, DAT_011be7e0 — le decalage de vue sauvegarde par l'opcode 0xfb.</summary>
    internal static ref int DecalSauveX_011cb1bc => ref *(int*)At(0x011cb1bc);

    /// <summary>Voir <see cref="DecalSauveX_011cb1bc"/>.</summary>
    internal static ref int DecalSauveY_011cb1b8 => ref *(int*)At(0x011cb1b8);

    /// <summary>Voir <see cref="DecalSauveX_011cb1bc"/>.</summary>
    internal static ref int DecalSauveZoom_011be7e0 => ref *(int*)At(0x011be7e0);

    /// <summary>DAT_011be778, DAT_011be77c, DAT_00a2c498 — le decalage de vue courant, que l'opcode 0xfb sauvegarde.</summary>
    internal static ref int DecalX_011be778 => ref *(int*)At(0x011be778);

    /// <summary>Voir <see cref="DecalX_011be778"/>.</summary>
    internal static ref int DecalY_011be77c => ref *(int*)At(0x011be77c);

    /// <summary>Voir <see cref="DecalX_011be778"/>.</summary>
    internal static ref int Zoom_00a2c498 => ref *(int*)At(0x00a2c498);

    /// <summary>
    /// DAT_012141d4 — le `this` que le consommateur passe a `FUN_00a27880` pour l'opcode -4.
    /// **Une seule indirection** (garde-fou 5) : le binaire fait `mov ecx,[12141D4h]` puis `call`,
    /// c'est donc la valeur de la variable qui part dans `ecx`. Pose par `FUN_00a27650`
    /// (`winsys3d.c:2479`), remis a 0 a la ligne 2573. Le corps de `FUN_00a27880` y lit une table
    /// de mots courts a `+0x80` et 32 pointeurs a `+0` ; **le role n'est pas etabli**, d'ou le nom
    /// neutre.
    /// </summary>
    internal static ref void* Objet_012141d4 => ref *(void**)At(0x012141d4);

    // ----------------------------------------------------------------------------------------
    // Le dessin IMMEDIAT des primitives — `Primitives.cs`. Celles-la ne passent pas par le
    // tampon de commandes : elles remplissent quatre sommets fixes et appellent `DrawPrimitive`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// &amp;DAT_011cb138 — **l'ADRESSE** d'un tableau de quatre `D3DTLVERTEX` de 0x20 octets
    /// (x, y, z, rhw, couleur, speculaire, u, v). Zero indirection : c'est `&amp;DAT_011cb138` qui
    /// part vers le device (`push 11CB138h`), pas le contenu. `z` et `rhw` ne sont ecrits par
    /// aucun des corps portes ; ils sont poses ailleurs.
    /// </summary>
    internal static float* SommetsImmediats_011cb138 => (float*)At(0x011cb138);

    /// <summary>
    /// PTR_DAT_00a2c4a0 — la table de conversion X, coordonnee PlayStation -> pixel ecran, en
    /// flottants. Une indirection pour la variable (`mov eax,[0A2C4A0h]`), puis l'indice. Posee
    /// par `SetShift` (796) selon la taille d'ecran. Voir `Documentation/DLL/winsys3d.md`.
    /// </summary>
    internal static ref float* TableEcranX_00a2c4a0 => ref *(float**)At(0x00a2c4a0);

    /// <summary>PTR_DAT_00a2c4a4 — la table de conversion Y. Voir <see cref="TableEcranX_00a2c4a0"/>.</summary>
    internal static ref float* TableEcranY_00a2c4a4 => ref *(float**)At(0x00a2c4a4);

    /// <summary>
    /// DAT_011be770 — le decalage X absolu a l'ecran, en pixels **entiers** (`fild`), pose par
    /// `SetShift` (796) et `SetAbsShift` (791). A ne pas confondre avec <see cref="DecalX_011be778"/>,
    /// le decalage tel que le jeu l'a demande.
    /// </summary>
    internal static ref int DecalEcranX_011be770 => ref *(int*)At(0x011be770);

    /// <summary>DAT_011be774 — le decalage Y absolu. Voir <see cref="DecalEcranX_011be770"/>.</summary>
    internal static ref int DecalEcranY_011be774 => ref *(int*)At(0x011be774);

    /// <summary>
    /// DAT_00b2d07c — la profondeur de couleur du device, en bits : `device[0x28]()` a
    /// l'initialisation (`winsys3d.c:2489`), ou 16 d'office (2543). Sous 16, pas de melange alpha.
    /// </summary>
    internal static ref int ProfondeurCouleur_00b2d07c => ref *(int*)At(0x00b2d07c);

    /// <summary>
    /// DAT_00a2c210 — memo du mode de melange pose par `FUN_00a03db0` : -1 opaque, -2 alpha par
    /// la source. Toute autre valeur force la prochaine pose.
    /// </summary>
    internal static ref int MemoMelange_00a2c210 => ref *(int*)At(0x00a2c210);

    /// <summary>
    /// &amp;DAT_011be7e8 — **l'ADRESSE** du second tableau de quatre `D3DTLVERTEX`, celui des
    /// primitives TEXTUREES en dessin immediat (`push 11BE7E8h`). Meme disposition que
    /// <see cref="SommetsImmediats_011cb138"/>, plus `tu`/`tv` aux flottants 6 et 7.
    /// </summary>
    internal static float* SommetsTextures_011be7e8 => (float*)At(0x011be7e8);

    /// <summary>
    /// DAT_011cb1c0 / 1c4 / 1c8 / 1cc — quatre tables de conversion de coordonnee de texture
    /// (octet PlayStation -> `tu`/`tv` flottant), toutes des **variables pointeurs** :
    /// `mov eax,[11CB1C8h]` puis `[eax+ecx*4]`. Posees ensemble depuis un descripteur
    /// (`winsys3d.c:25472`, `param_1[6..9]`). Les polygones lisent 1c8 (u) / 1cc (v), les sprites
    /// 1c0 (u) / 1c4 (v). Les mots sont recopies **bruts** (`mov edx,[...] ; mov [...],edx`), d'ou
    /// le type `uint*` : aucune conversion flottante ne passe par eux.
    /// </summary>
    internal static ref uint* TexSpriteU_011cb1c0 => ref *(uint**)At(0x011cb1c0);

    /// <summary>Voir <see cref="TexSpriteU_011cb1c0"/>.</summary>
    internal static ref uint* TexSpriteV_011cb1c4 => ref *(uint**)At(0x011cb1c4);

    /// <summary>Voir <see cref="TexSpriteU_011cb1c0"/>.</summary>
    internal static ref uint* TexPolyU_011cb1c8 => ref *(uint**)At(0x011cb1c8);

    /// <summary>Voir <see cref="TexSpriteU_011cb1c0"/>.</summary>
    internal static ref uint* TexPolyV_011cb1cc => ref *(uint**)At(0x011cb1cc);

    /// <summary>
    /// &amp;DAT_00a2c5a8 — l'alpha de chacun des quatre modes de semi-transparence PlayStation
    /// (`abr`, bits 5-6 de la page de texture), un octet chacun. Relus dans le binaire :
    /// <c>7f ff ff 3f</c> — 50 %, 100 %, 100 %, 25 %, soit B/2+F/2, B+F, B-F, B+F/4. Personne ne
    /// l'ecrit.
    /// </summary>
    internal static byte* AlphaParMode_00a2c5a8 => (byte*)At(0x00a2c5a8);

    /// <summary>
    /// DAT_00a2c5a4 — le mode d'ombrage des primitives Gouraud texturees. Vaut **2**
    /// (`D3DSHADE_GOURAUD`) dans `.data`, et aucun corps ne l'ecrit.
    /// </summary>
    internal static int OmbrageGouraud_00a2c5a4 => *(int*)At(0x00a2c5a4);

    // ----------------------------------------------------------------------------------------
    // Le texte a l'ecran — `TexteEcran.cs`. Trois mecanismes : des fenetres de texte dessinees
    // en sprites, une console deroulante de trente lignes, et une file de textes GDI.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00a2ea98 — le nombre de fenetres de texte. Relu a chaque tour par les deux boucles.</summary>
    internal static ref int NombreFenetresTexte_00a2ea98 => ref *(int*)At(0x00a2ea98);

    /// <summary>
    /// &amp;DAT_00a2e8f0 — **l'ADRESSE** de la table des fenetres de texte, 0x24 octets chacune :
    /// +0 x, +4 y, +8 largeur, +0xc hauteur, +0x10 genre (1 -> CLUT <c>DAT_00a2ea38</c>, sinon
    /// <c>DAT_00a2ea34</c>), +0x18 nombre de caracteres en attente, +0x1c remis a zero avec lui,
    /// +0x20 le tampon de caracteres.
    /// </summary>
    internal static byte* FenetresTexte_00a2e8f0 => (byte*)At(0x00a2e8f0);

    /// <summary>DAT_00a2c198 — la page de texture de la police.</summary>
    internal static ref int PagePolice_00a2c198 => ref *(int*)At(0x00a2c198);

    /// <summary>DAT_00a2ea38 — la CLUT de la police pour les fenetres de genre 1 et la console.</summary>
    internal static ref int ClutPolice_00a2ea38 => ref *(int*)At(0x00a2ea38);

    /// <summary>DAT_00a2ea34 — la CLUT de la police pour les autres fenetres.</summary>
    internal static ref int ClutPoliceAutre_00a2ea34 => ref *(int*)At(0x00a2ea34);

    /// <summary>DAT_00a2f420 — la console : l'indice de la derniere ligne (0..29).</summary>
    internal static ref int ConsoleFin_00a2f420 => ref *(int*)At(0x00a2f420);

    /// <summary>DAT_00a2f424 — la console : l'indice de la premiere ligne (0..29).</summary>
    internal static ref int ConsoleDebut_00a2f424 => ref *(int*)At(0x00a2f424);

    /// <summary>DAT_00a2f428 — la console : non nul, elle s'affiche meme vide d'un tour complet.</summary>
    internal static ref int ConsoleDrapeau_00a2f428 => ref *(int*)At(0x00a2f428);

    /// <summary>&amp;DAT_00a2eaa0 — l'ADRESSE des trente lignes de la console, 0x51 octets chacune, terminees par un zero.</summary>
    internal static byte* ConsoleLignes_00a2eaa0 => (byte*)At(0x00a2eaa0);

    /// <summary>
    /// DAT_00a2ea90 — la tete de la file de textes GDI que `WinDrawText` remplit dans l'arene de
    /// l'image. Chaque maillon : +0 suivant, +4 indice de police, +8 x, +0xc y, +0x14 le texte.
    /// </summary>
    internal static ref byte* FileTextesGdi_00a2ea90 => ref *(byte**)At(0x00a2ea90);

    /// <summary>&amp;DAT_00a2ea40 — l'ADRESSE de la table des polices GDI (HFONT), par indice ; nulle = rien a ecrire.</summary>
    internal static IntPtr* PolicesGdi_00a2ea40 => (IntPtr*)At(0x00a2ea40);

    /// <summary>&amp;DAT_00a2c1a0 — l'ADRESSE de la table des couleurs de texte (COLORREF), par indice de police.</summary>
    internal static uint* CouleursGdi_00a2c1a0 => (uint*)At(0x00a2c1a0);

    /// <summary>&amp;DAT_00a2ea68 — l'ADRESSE de la table des hauteurs de ligne, par indice de police.</summary>
    internal static int* HauteursLigneGdi_00a2ea68 => (int*)At(0x00a2ea68);

    // ----------------------------------------------------------------------------------------
    // Le cache de textures — `CacheTextures.cs`. Le releve des pages demandees par l'image, puis
    // leur chargement, en evincant les plus anciennes.
    // ----------------------------------------------------------------------------------------

    /// <summary>&amp;DAT_00a2c654 — l'ADRESSE de quatre octets, remplis par `FUN_00a03400(0..3)` tant que le premier vaut 0xfe.</summary>
    internal static byte* Quatre_00a2c654 => (byte*)At(0x00a2c654);

    /// <summary>DAT_00a2c650 — la page de texture courante des SPRITES pendant le releve (l'opcode 0xe1 la change).</summary>
    internal static ref int PageReleve_00a2c650 => ref *(int*)At(0x00a2c650);

    /// <summary>&amp;DAT_011f4db8 — l'ADRESSE d'une table de 32 000 mots, remise a -1 a chaque image.</summary>
    internal static uint* TableReleve_011f4db8 => (uint*)At(0x011f4db8);

    /// <summary>DAT_012141b8 — le nombre de demandes de texture relevees dans l'image.</summary>
    internal static ref int NombreDemandes_012141b8 => ref *(int*)At(0x012141b8);

    /// <summary>
    /// &amp;DAT_011f0f38 — l'ADRESSE des demandes de texture, 0x18 octets chacune (3 000 mots remis
    /// a zero par image) : +0 x0, +4 y0, +8 x1, +0xc y1, +0x10 genre (0xff = satisfaite), +0x11,
    /// +0x12 CLUT (mot), +0x14 (mot).
    /// </summary>
    internal static byte* Demandes_011f0f38 => (byte*)At(0x011f0f38);

    /// <summary>
    /// &amp;DAT_011f3e18 — l'ADRESSE de la liste d'eviction, 8 octets par entree (emplacement, page,
    /// date). Dimensionnee a 1 000 mots — 500 entrees — et remplie sans borne : au-dela, elle deborde
    /// sur <see cref="TableReleve_011f4db8"/>, qui la suit exactement.
    /// </summary>
    internal static byte* ListeEviction_011f3e18 => (byte*)At(0x011f3e18);

    /// <summary>DAT_012141cc — la copie du pavage (opcode 0xe2) propre au releve ; <c>c4</c>, <c>c8</c>, <c>bc</c>, <c>c0</c> ses quatre champs.</summary>
    internal static ref int PavageReleve_012141cc => ref *(int*)At(0x012141cc);

    /// <summary>Voir <see cref="PavageReleve_012141cc"/>.</summary>
    internal static ref int PavageReleve_012141c4 => ref *(int*)At(0x012141c4);

    /// <summary>Voir <see cref="PavageReleve_012141cc"/>.</summary>
    internal static ref int PavageReleve_012141c8 => ref *(int*)At(0x012141c8);

    /// <summary>Voir <see cref="PavageReleve_012141cc"/>.</summary>
    internal static ref int PavageReleve_012141bc => ref *(int*)At(0x012141bc);

    /// <summary>Voir <see cref="PavageReleve_012141cc"/>.</summary>
    internal static ref int PavageReleve_012141c0 => ref *(int*)At(0x012141c0);

    /// <summary>
    /// DAT_012141d0 — l'EMPLACEMENT VIDE : un emplacement de texture statique (construit par
    /// `FUN_00a27140`, `winsys3d.c:25608`) que les pages du cache designent a la place d'un
    /// emplacement libere. Jamais rendu par `Free`, jamais date.
    /// </summary>
    internal static ref void* EmplacementVide_012141d0 => ref *(void**)At(0x012141d0);

    /// <summary>DAT_00b2d180 — non nul, les textures du cache sont CARREES (le cote du plus grand). Pose a l'initialisation (`winsys3d.c:2503`).</summary>
    internal static ref int TexturesCarrees_00b2d180 => ref *(int*)At(0x00b2d180);

    /// <summary>
    /// &amp;DAT_00a2f430 — l'ADRESSE d'une table d'octets : le mode de melange <c>i</c> est-il
    /// disponible sur ce device. Posee a l'initialisation (`winsys3d.c:2505`..`2547`).
    /// </summary>
    internal static byte* MelangesDisponibles_00a2f430 => (byte*)At(0x00a2f430);

    /// <summary>&amp;DAT_00a2c218 — l'ADRESSE de la table des modes de REPLI, un entier par mode : a essayer quand le mode demande manque.</summary>
    internal static int* MelangesRepli_00a2c218 => (int*)At(0x00a2c218);

    /// <summary>DAT_00a2f454 — les capacites de melange du device : 0x1000 alpha par la source, 0x2000 l'autre voie (`device[0x9c]`).</summary>
    internal static ref int CapacitesMelange_00a2f454 => ref *(int*)At(0x00a2f454);

    /// <summary>
    /// `Zoom8BitTextures` @ 0x011be750 — donnee exportee, lue ici par son adresse comme ses
    /// voisines (`mov eax,[11BE750h]` a 0x00a2747c). `Screens/GameStates.cs` la resout par nom.
    /// </summary>
    internal static ref int Zoom8BitTextures_011be750 => ref *(int*)At(0x011be750);

    /// <summary>
    /// &amp;DAT_011c4cd0 — l'ADRESSE que reçoivent les deux tables de coordonnées de texture des
    /// SPRITES d'un emplacement neuf (`mov edx,11C4CD0h`, relogée) — et que `LierEmplacement`
    /// recopie dans <see cref="TexSpriteU_011cb1c0"/> / <see cref="TexSpriteV_011cb1c4"/>.
    /// </summary>
    internal static uint* TablesUvSpriteDefaut_011c4cd0 => (uint*)At(0x011c4cd0);

    /// <summary>&amp;DAT_011be868 — idem pour les tables des POLYGONES (<see cref="TexPolyU_011cb1c8"/>, <see cref="TexPolyV_011cb1cc"/>).</summary>
    internal static uint* TablesUvPolyDefaut_011be868 => (uint*)At(0x011be868);

    // ----------------------------------------------------------------------------------------
    // Le chargement des textures — `ChargementTextures.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// EXT_00b6dbd0 — le pointeur de la VRAM PlayStation emulee : 1 024 x 512 mots de 16 bits,
    /// 0x800 octets par ligne. Une variable, lue `mov ecx,[0B6DBD0h]` : une indirection.
    /// </summary>
    internal static ref byte* Vram_00b6dbd0 => ref *(byte**)At(0x00b6dbd0);

    /// <summary>
    /// &amp;DAT_00aed078 — l'ADRESSE du tampon ou les pixels convertis sont poses avant d'etre
    /// remis a l'objet texture (`push 0AED078h`). Il suit exactement les trois tables de couleurs.
    /// </summary>
    internal static byte* TamponConversion_00aed078 => (byte*)At(0x00aed078);

    /// <summary>
    /// &amp;DAT_00aad078 — l'ADRESSE de la table de couleurs du genre 0 : 65 536 mots, couleur PS
    /// -> couleur 16 bits 1-5-5-5 du device. C'est son adresse qui decide du format (`cmp ebp,0AAD078h`).
    /// </summary>
    internal static ushort* Couleurs555_00aad078 => (ushort*)At(0x00aad078);

    /// <summary>&amp;DAT_00a8d078 — la table de couleurs du genre 1 (format 4-4-4-4).</summary>
    internal static ushort* CouleursGenre1_00a8d078 => (ushort*)At(0x00a8d078);

    /// <summary>&amp;DAT_00acd078 — la table de couleurs du genre 2 (format 4-4-4-4).</summary>
    internal static ushort* CouleursGenre2_00acd078 => (ushort*)At(0x00acd078);

    /// <summary>&amp;DAT_00b2d080 — l'ADRESSE d'une table de 256 octets : composante 5 bits (x 8) -> composante 8 bits de la palette DIB.</summary>
    internal static byte* Composantes_00b2d080 => (byte*)At(0x00b2d080);

    /// <summary>DAT_00b2d188 — nul, le genre 1 est ramene au genre 0 au chargement.</summary>
    internal static ref int Genre1Permis_00b2d188 => ref *(int*)At(0x00b2d188);
}
