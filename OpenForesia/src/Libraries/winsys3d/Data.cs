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

    /// <summary>
    /// DAT_011be6f0 — un octet : choisit le slot `+0x5c` (non nul) ou `+0x60` (nul). **C'est le
    /// champ `isbg` du `DRAWENV` que `PutDrawEnv` range** — l'octet +0x18 de
    /// <see cref="DrawEnv_011be6d8"/>.
    /// </summary>
    internal static ref byte ModeEffacement_011be6f0 => ref *At(0x011be6f0);

    /// <summary>
    /// &amp;DAT_011be6d8 — le `DRAWENV` courant, 23 mots, que l'ordinal 660 (`PutDrawEnv`) range et
    /// que le 659 ressort. Son octet +0x18 est <see cref="ModeEffacement_011be6f0"/>, que
    /// `DrawOTag` lit : une copie privee de ce bloc empecherait tout effacement de l'image.
    /// </summary>
    internal static uint* DrawEnv_011be6d8 => (uint*)At(0x011be6d8);

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

    /// <summary>
    /// &amp;DAT_00add078 — la MOITIE HAUTE de la table du genre 2 (`0x00acd078 + 0x10000`) : les
    /// couleurs PS au bit 15 pose. Aucun corps ne la nomme ; la conversion l'atteint en indexant
    /// la table du genre 2 par un mot de 16 bits. `CreateTranslationTables` l'ecrit sous ce nom.
    /// </summary>
    internal static ushort* CouleursGenre2Haute_00add078 => (ushort*)At(0x00add078);

    /// <summary>_DAT_00a2c228 — le dernier gamma passe a `CreateTranslationTables`, en `float`.</summary>
    internal static ref float GammaCourant_00a2c228 => ref *(float*)At(0x00a2c228);

    /// <summary>&amp;DAT_00b2d080 — l'ADRESSE d'une table de 256 octets : composante 5 bits (x 8) -> composante 8 bits de la palette DIB.</summary>
    internal static byte* Composantes_00b2d080 => (byte*)At(0x00b2d080);

    /// <summary>
    /// DAT_00b2d188 — nul au chargement : le genre 1 est alors ramene au genre 0. `ChangeResolution`
    /// (749) le pose a 1 quand le device n'a pas le bit 3 de l'octet `+0x8c` de ses capacites.
    /// </summary>
    internal static ref int Genre1Permis_00b2d188 => ref *(int*)At(0x00b2d188);

    // ----------------------------------------------------------------------------------------
    // La construction des paquets — `ConstructionPaquets.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// DAT_011be7a4 — le DEBUT de la zone de sommets du paquet courant, pose avec
    /// <see cref="Sommets_011be7a8"/> par `FUN_00a0c480` ; celui-la avance, celui-ci reste.
    /// </summary>
    internal static ref float* SommetsDebut_011be7a4 => ref *(float**)At(0x011be7a4);

    /// <summary>
    /// DAT_011be764, DAT_011be768, DAT_011be76c — le memo de la derniere couleur r, g, b emise
    /// dans le tampon, en mots de 16 bits (`mov [11BE764h],ax`). 0xffff = aucune.
    /// </summary>
    internal static ref ushort MemoRouge_011be764 => ref *(ushort*)At(0x011be764);

    /// <summary>Voir <see cref="MemoRouge_011be764"/>.</summary>
    internal static ref ushort MemoVert_011be768 => ref *(ushort*)At(0x011be768);

    /// <summary>Voir <see cref="MemoRouge_011be764"/>.</summary>
    internal static ref ushort MemoBleu_011be76c => ref *(ushort*)At(0x011be76c);

    /// <summary>
    /// _DAT_00a2a0b4 — **1/4096** (0x39800000 dans `.rdata`), l'echelle des normales des modeles
    /// TMD. Meme valeur que <see cref="EchelleFixe_00a2a0b8"/>, a une autre adresse : les
    /// producteurs lisent celle-ci.
    /// </summary>
    internal static float EchelleNormale_00a2a0b4 => *(float*)At(0x00a2a0b4);

    // ----------------------------------------------------------------------------------------
    // L'objet TMD en cours de conversion — `FUN_00a06a40` (ordinal 678) les pose depuis une
    // entree de la table d'objets TMD (sept mots : sommets, nombre, normales, nombre, primitives,
    // nombre, echelle), et `FUN_00a0c4f0` les lit.
    // ----------------------------------------------------------------------------------------

    /// <summary>_DAT_011be7b0 — le numero de l'objet choisi dans le TMD (0 pour une entree seule).</summary>
    internal static ref int TmdObjet_011be7b0 => ref *(int*)At(0x011be7b0);

    /// <summary>_DAT_011be7b4 — le nombre d'objets du TMD (1 pour une entree seule).</summary>
    internal static ref int TmdNbObjets_011be7b4 => ref *(int*)At(0x011be7b4);

    /// <summary>DAT_011be7b8 — les sommets de l'objet : quatre `short` par sommet (x, y, z, bourrage).</summary>
    internal static ref short* TmdSommets_011be7b8 => ref *(short**)At(0x011be7b8);

    /// <summary>_DAT_011be7bc — leur nombre.</summary>
    internal static ref int TmdNbSommets_011be7bc => ref *(int*)At(0x011be7bc);

    /// <summary>DAT_011be7c0 — les normales de l'objet, meme forme que les sommets, en virgule fixe 4.12.</summary>
    internal static ref short* TmdNormales_011be7c0 => ref *(short**)At(0x011be7c0);

    /// <summary>_DAT_011be7c4 — leur nombre.</summary>
    internal static ref int TmdNbNormales_011be7c4 => ref *(int*)At(0x011be7c4);

    /// <summary>_DAT_011be7c8 — la premiere primitive de l'objet.</summary>
    internal static ref byte* TmdPrimitives_011be7c8 => ref *(byte**)At(0x011be7c8);

    /// <summary>DAT_011be7cc — le nombre de primitives.</summary>
    internal static ref int TmdNbPrimitives_011be7cc => ref *(int*)At(0x011be7cc);

    /// <summary>_DAT_011be7d0 — l'echelle de l'objet (mot 6 de l'entree).</summary>
    internal static ref int TmdEchelle_011be7d0 => ref *(int*)At(0x011be7d0);

    /// <summary>DAT_011be7d4 — le numero de la primitive courante.</summary>
    internal static ref int TmdIndice_011be7d4 => ref *(int*)At(0x011be7d4);

    /// <summary>DAT_011be7d8 — la primitive courante : `[olen][ilen][drapeaux][mode]` puis ses champs.</summary>
    internal static ref byte* TmdPrimitive_011be7d8 => ref *(byte**)At(0x011be7d8);

    // ----------------------------------------------------------------------------------------
    // Le decalage d'ecran — `DecalageEcran.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// DAT_011be780, DAT_011be784 — le decalage ABSOLU que pose `SetAbsShift` (791), en unites
    /// PlayStation, mis a l'echelle du zoom avant d'entrer dans <see cref="DecalEcranX_011be770"/>.
    /// </summary>
    internal static ref int DecalAbsX_011be780 => ref *(int*)At(0x011be780);

    /// <summary>Voir <see cref="DecalAbsX_011be780"/>.</summary>
    internal static ref int DecalAbsY_011be784 => ref *(int*)At(0x011be784);

    /// <summary>DAT_00a2c49c — le mode d'affichage (2 dans `.data`), indice des deux tables d'origine.</summary>
    internal static ref int ModeAffichage_00a2c49c => ref *(int*)At(0x00a2c49c);

    /// <summary>
    /// 0x00a2c4d8 — l'origine X de l'image a l'ecran, un entier par mode d'affichage. Relue dans
    /// le binaire : -160, -64, 0, 80, 192, 0.
    /// </summary>
    internal static int* OrigineX_00a2c4d8 => (int*)At(0x00a2c4d8);

    /// <summary>0x00a2c4f0 — l'origine Y, meme forme : -120, -48, 0, 60, 144, -1.</summary>
    internal static int* OrigineY_00a2c4f0 => (int*)At(0x00a2c4f0);

    /// <summary>
    /// &amp;DAT_00a31138 — les tables d'echelle, une de 16 000 octets par niveau de zoom. `SetShift`
    /// fait pointer <see cref="TableEcranX_00a2c4a0"/> ET <see cref="TableEcranY_00a2c4a4"/> sur la
    /// meme.
    /// </summary>
    internal static byte* TablesEchelle_00a31138 => (byte*)At(0x00a31138);

    /// <summary>_DAT_00a2a06c — **1/320** (0x3B4CCCCD dans `.rdata`), le facteur de `ShiftVertices`.</summary>
    internal static float UnSur320_00a2a06c => *(float*)At(0x00a2a06c);

    // ----------------------------------------------------------------------------------------
    // L'environnement d'affichage — `PutDispEnv` (657), dans `ListeAffichage.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// &amp;DAT_011be6b8 — le `DISPENV` courant, cinq mots que l'ordinal 657 range et que le 656
    /// ressort : `disp` (x, y, w, h), `screen`, puis `isinter`, `isrgb24`.
    /// </summary>
    internal static uint* DispEnv_011be6b8 => (uint*)At(0x011be6b8);

    /// <summary>DAT_011be6bc — la largeur `disp.w` du `DISPENV`, un `short` (`movsx`).</summary>
    internal static ref short DispLargeur_011be6bc => ref *(short*)At(0x011be6bc);

    /// <summary>_DAT_011be6be — la hauteur `disp.h`, un `short`.</summary>
    internal static ref short DispHauteur_011be6be => ref *(short*)At(0x011be6be);

    // ----------------------------------------------------------------------------------------
    // Les manettes — `Manettes.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// `TexFilterPossible` @ 0x00a2c504 — donnee exportee (ordinal 825), que F10 bascule.
    /// `Engine/Audio.cs` la lit par `GetProcAddress` ; c'est la meme case.
    /// </summary>
    internal static ref int TexFilterPossible_00a2c504 => ref *(int*)At(0x00a2c504);

    /// <summary>DAT_00dedc40 — non nul : les manettes sont lues meme quand le tampon est demande.</summary>
    internal static ref int ManettesActives_00dedc40 => ref *(int*)At(0x00dedc40);

    /// <summary>DAT_00dedc38 — le tampon de la manette 1 (octets 2 et 3 : les boutons, actifs a 0), ou nul.</summary>
    internal static ref byte* TamponManette1_00dedc38 => ref *(byte**)At(0x00dedc38);

    /// <summary>DAT_00dedc3c — le tampon de la manette 2, meme forme.</summary>
    internal static ref byte* TamponManette2_00dedc3c => ref *(byte**)At(0x00dedc3c);

    /// <summary>DAT_00b6dbcc — non nul : ni clavier ni joystick ne sont lus, tout est relache.</summary>
    internal static ref int SaisieCoupee_00b6dbcc => ref *(int*)At(0x00b6dbcc);

    /// <summary>
    /// 0x00a2c258 — la table de touches de la manette 1, 38 mots : dix boutons de trois mots
    /// (masque de bouton joystick, touche, touche de rechange), puis quatre directions de deux
    /// touches (gauche, droite, haut, bas). Relue dans le binaire : pave numerique et chiffres,
    /// fleches et S/D/E/X.
    /// </summary>
    internal static uint* TouchesManette1_00a2c258 => (uint*)At(0x00a2c258);

    /// <summary>0x00a2c2f0 — la table de la manette 2, meme forme (0x98 octets plus loin).</summary>
    internal static uint* TouchesManette2_00a2c2f0 => (uint*)At(0x00a2c2f0);

    /// <summary>Une case de `.data` de la DLL par son adresse — pour les seuils de joystick, epars.</summary>
    internal static ref uint Mot(nuint va) => ref *(uint*)At(va);

    /// <summary>
    /// &amp;DAT_00a34fb8 — la table de conversion X d'une surface affichee de plus de 320, que
    /// `PutDispEnv` (657) pose a la place de celle du zoom.
    /// </summary>
    internal static float* TableGrandeSurfaceX_00a34fb8 => (float*)At(0x00a34fb8);

    /// <summary>&amp;DAT_00a5c0b8 — sa jumelle en Y. Voir <see cref="TableGrandeSurfaceX_00a34fb8"/>.</summary>
    internal static float* TableGrandeSurfaceY_00a5c0b8 => (float*)At(0x00a5c0b8);

    // ----------------------------------------------------------------------------------------
    // Les registres du GTE emule — `Matrices.cs`. Une `MATRIX` de la libgte fait 0x20 octets :
    // neuf `short` par lignes (0x12), deux de bourrage, puis trois `int` de translation (+0x14).
    // ----------------------------------------------------------------------------------------

    /// <summary>EXT_011d0870 — la matrice de rotation et translation courante (l'export de donnees 814).</summary>
    internal static byte* RegistreRotation_011d0870 => At(0x011d0870);

    /// <summary>EXT_011d0850 — la matrice d'eclairage (l'export de donnees 815), 0x20 octets.</summary>
    internal static byte* RegistreLumiere_011d0850 => At(0x011d0850);

    /// <summary>EXT_011d0828 — la matrice de couleur des lumieres, 0x20 octets.</summary>
    internal static byte* RegistreCouleur_011d0828 => At(0x011d0828);

    /// <summary>DAT_011f0898 — la couleur de fond (r, g, b), trois octets.</summary>
    internal static byte* CouleurFond_011f0898 => At(0x011f0898);

    /// <summary>EXT_011cb414 — le decalage de projection X (premier argument de l'ordinal 378).</summary>
    internal static ref int DecalGeomX_011cb414 => ref *(int*)At(0x011cb414);

    /// <summary>EXT_011cb410 — le decalage de projection Y (second argument de l'ordinal 378).</summary>
    internal static ref int DecalGeomY_011cb410 => ref *(int*)At(0x011cb410);

    /// <summary>
    /// &amp;EXT_011d0898 — la table de projection, 0x10000 `short` indexes par la profondeur sur
    /// 16 bits : `(h << 12) / z`, saturee a 0x7fff. Remplie par `SetGeomScreen` (379, encore
    /// natif), relue par 418 et 795.
    /// </summary>
    internal static short* TableProjection_011d0898 => (short*)At(0x011d0898);

    /// <summary>
    /// _EXT_011d0890 — le dernier XY d'ecran projete (le `SXY2` du GTE, par la forme). Seule la
    /// branche scalaire de 418 et 795 l'ecrit, a chaque sommet ; la branche MMX ne le touche pas.
    /// </summary>
    internal static ref int DernierXY_011d0890 => ref *(int*)At(0x011d0890);

    /// <summary>
    /// MMXInstalled (0x00dedbe8) — pose a l'initialisation (789) par `FUN_00a06930`, un
    /// `__try { pand ; emms }` qui rend 1 : **toujours 1 sur une machine d'aujourd'hui**. Choisit
    /// entre les deux branches de 360, 418 et 795, qui ne calculent pas toutes la meme chose.
    /// </summary>
    internal static ref int MmxInstalle_00dedbe8 => ref *(int*)At(0x00dedbe8);

    /// <summary>
    /// &amp;EXT_00b6d198 — la couleur de base de l'eclairage, quatre octets `r, g, b, code`
    /// (l'export de donnees 817, que `Platform/Winsys3d.cs` resout par ordinal). L'ordinal 793 la
    /// passe a `NormalColorCol3`.
    /// </summary>
    internal static byte* CouleurEclairage_00b6d198 => (byte*)At(0x00b6d198);

    // ----------------------------------------------------------------------------------------
    // Les reglages du device — `Device.cs` — et la cadence — `Cadence.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00a2c508 — le dernier mode de filtrage demande a `SetTextureFilterMode` (798).</summary>
    internal static ref int FiltreTexture_00a2c508 => ref *(int*)At(0x00a2c508);

    /// <summary>&amp;DAT_00a2c4a8 — les largeurs des modes d'affichage : 320, 512, 640, 800, 1024, puis 0.</summary>
    internal static int* LargeursModes_00a2c4a8 => (int*)At(0x00a2c4a8);

    /// <summary>&amp;DAT_00a2c4c0 — les hauteurs : 240, 384, 480, 600, 768, puis 0.</summary>
    internal static int* HauteursModes_00a2c4c0 => (int*)At(0x00a2c4c0);

    /// <summary>
    /// DAT_011be798 — le rappel que l'initialisation (789) appelle apres avoir retabli le device,
    /// pose par l'ordinal 790. Le jeu y range `Audio.DeviceInitCallback`.
    /// </summary>
    internal static ref void* RappelDevice_011be798 => ref *(void**)At(0x011be798);

    /// <summary>DAT_00a2c3b0 — le mode de cadence, que `SetTimingMode` (822) echange et que 132 aiguille.</summary>
    internal static ref int ModeCadence_00a2c3b0 => ref *(int*)At(0x00a2c3b0);

    /// <summary>DAT_00dedd70 — le dernier `QueryPerformanceCounter`, 64 bits.</summary>
    internal static long* CompteurPerf_00dedd70 => (long*)At(0x00dedd70);

    /// <summary>DAT_011be684 — les tops de `QueryPerformanceCounter` par image, un `int` (sign-etendu dans les calculs 64 bits).</summary>
    internal static ref int TopsParImage_011be684 => ref *(int*)At(0x011be684);

    /// <summary>`VSyncCounter` @ 0x00dedbf4 — le compteur d'images ecoulees, donnee exportee.</summary>
    internal static ref int VSyncCounter_00dedbf4 => ref *(int*)At(0x00dedbf4);

    /// <summary>DAT_011be680 — l'image a attendre, en unites de `VSyncCounter`.</summary>
    internal static ref int ImageAttendue_011be680 => ref *(int*)At(0x011be680);

    /// <summary>DAT_011be690 — l'echeance en tops de compteur, 64 bits (mot haut a 0x011be694).</summary>
    internal static long* EcheancePerf_011be690 => (long*)At(0x011be690);

    /// <summary>
    /// &amp;DAT_00dedd78 — le journal des attentes d'image : un enregistrement de 0x28 octets par
    /// appel a l'ordinal 132, a l'indice <see cref="IndiceJournal_011be69c"/>. **100 000 places
    /// exactement** (0x00dedd78 .. 0x011be678), et l'indice n'est pas borne : voir LB-047.
    /// </summary>
    internal static byte* JournalImages_00dedd78 => At(0x00dedd78);

    /// <summary>DAT_011be69c — l'indice du dernier enregistrement du journal ; incremente AVANT l'ecriture.</summary>
    internal static ref int IndiceJournal_011be69c => ref *(int*)At(0x011be69c);

    /// <summary>DAT_011be688 — le compte de rattrapage : 3 quand une image est en retard, decremente a chaque image a l'heure. Tant qu'il est non nul, 132 n'attend pas.</summary>
    internal static ref int Rattrapage_011be688 => ref *(int*)At(0x011be688);

    /// <summary>DAT_011be698 — la bascule du mode 3 (une image sur deux).</summary>
    internal static ref int BasculeMode3_011be698 => ref *(int*)At(0x011be698);

    /// <summary>_DAT_011be678 — le `VSyncCounter` de la derniere attente du mode 1.</summary>
    internal static ref int DerniereImage_011be678 => ref *(int*)At(0x011be678);

    /// <summary>_DAT_00dedbf0 — pose a 1 par la pompe de messages quand elle voit `WM_QUIT`.</summary>
    internal static ref int QuitteRecu_00dedbf0 => ref *(int*)At(0x00dedbf0);

    /// <summary>
    /// FUN_00a01d60 — un CODE, pas une donnee : le filtre d'exceptions `or eax,-1 ; ret 4`
    /// (`EXCEPTION_CONTINUE_EXECUTION`) que la pompe installe avant `RaiseException`. On passe son
    /// adresse native a `SetUnhandledExceptionFilter`, comme l'original.
    /// </summary>
    internal static IntPtr FiltreExceptions_00a01d60 => (IntPtr)At(0x00a01d60);

    // ----------------------------------------------------------------------------------------
    // L'initialisation du device — `Device.cs` (789) et `FUN_00a03110`.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_011be738 — non nul : le device est celui de `gldhelp.dll` (Glide) au lieu de `3dhelp.dll`.</summary>
    internal static ref int Glide_011be738 => ref *(int*)At(0x011be738);

    /// <summary>
    /// DAT_011be73c — passe deux fois a `device[0x2c]`, et coupe le filtrage des textures
    /// (`TexFilterPossible = 0`) quand il est non nul. Role non etabli.
    /// </summary>
    internal static ref int Unknown_011be73c => ref *(int*)At(0x011be73c);

    /// <summary>DAT_011be79c — la fenetre du jeu, que `device[0x18]` recoit PAR ADRESSE et remplit.</summary>
    internal static ref IntPtr Fenetre_011be79c => ref *(IntPtr*)At(0x011be79c);

    /// <summary>L'ADRESSE de <see cref="Fenetre_011be79c"/>, telle que `push 11BE79Ch` la passe au device.</summary>
    internal static IntPtr* AdresseFenetre_011be79c => (IntPtr*)At(0x011be79c);

    /// <summary>DAT_011be6cc — le module `3dhelp.dll` (ou `gldhelp.dll`) charge par 789.</summary>
    internal static ref IntPtr ModuleDevice_011be6cc => ref *(IntPtr*)At(0x011be6cc);

    /// <summary>_DAT_011be7a0 — la fabrique `Create3DDevice` que `GetProcAddress` a rendue.</summary>
    internal static ref void* FabriqueDevice_011be7a0 => ref *(void**)At(0x011be7a0);

    /// <summary>DAT_011be6d0 — l'identifiant du minuteur multimedia de 17 ms (`timeSetEvent`).</summary>
    internal static ref uint Minuteur_011be6d0 => ref *(uint*)At(0x011be6d0);

    /// <summary>_DAT_00dedbe0 — le troisieme pointeur d'arene, pose avec les deux autres a `MainMem + 0x200000`.</summary>
    internal static ref byte* AreneFin_00dedbe0 => ref *(byte**)At(0x00dedbe0);

    /// <summary>DAT_011be6c8 — le `isinter` du `DISPENV` (+0x10), remis a 0 par 789.</summary>
    internal static ref byte DispEntrelace_011be6c8 => ref *At(0x011be6c8);

    /// <summary>DAT_00a2f470 — la fenetre dont `FUN_00a03110` a remplace la procedure.</summary>
    internal static ref IntPtr FenetreSousClassee_00a2f470 => ref *(IntPtr*)At(0x00a2f470);

    /// <summary>DAT_00b6d194 — la procedure de fenetre d'origine, rendue par `SetWindowLongA`, que `FUN_00a033c0` remet.</summary>
    internal static ref IntPtr ProcedureOrigine_00b6d194 => ref *(IntPtr*)At(0x00b6d194);

    /// <summary>
    /// &amp;DAT_00a2f478 — 24 rangees de 4 000 `float`, remplies par `FUN_00a03110` :
    /// `(depart + k * pas) * K`, le depart reculant de 0xe600 et le pas croissant de 0x20 par rangee.
    /// </summary>
    internal static float* TableProfondeurs_00a2f478 => (float*)At(0x00a2f478);

    /// <summary>&amp;DAT_00a2f438 — la copie des 14 mots de capacites que rend `device[0x4c]` (a +0x64).</summary>
    internal static uint* CopieCapacites_00a2f438 => (uint*)At(0x00a2f438);

    /// <summary>_DAT_00b2d184 — pose a 1 quand les capacites n'ont aucun des bits 14 et 15 (`0x00a2f454 &amp; 0xc000`).</summary>
    internal static ref int Unknown_00b2d184 => ref *(int*)At(0x00b2d184);

    /// <summary>L'entree `GetProcAddress` de la table d'imports de `winsys3d` (`call [121532Ch]`) — celle que `SyntheticDevice` detourne.</summary>
    internal static void** ImportGetProcAddress_0121532c => (void**)At(0x0121532c);

    /// <summary>`s_3dhelp_dll_00a2c54c`, `s_gldhelp_dll_00a2c540`, `s_Create3DDevice_00a2c530`, `s_3D_Graphics_initialisation_error_00a2c50c` — les chaines de la DLL.</summary>
    internal static byte* Chaine3dhelp_00a2c54c => At(0x00a2c54c);
    internal static byte* ChaineGldhelp_00a2c540 => At(0x00a2c540);
    internal static byte* ChaineCreate3DDevice_00a2c530 => At(0x00a2c530);
    internal static byte* ChaineErreurInit_00a2c50c => At(0x00a2c50c);

    /// <summary>LAB_00a06360 — un CODE : le rappel du minuteur de 17 ms, reste natif, passe par adresse.</summary>
    internal static IntPtr RappelMinuteur_00a06360 => (IntPtr)At(0x00a06360);

    /// <summary>LAB_00a02ee0 — un CODE : la procedure de fenetre que `FUN_00a03110` installe, restee native.</summary>
    internal static IntPtr ProcedureFenetre_00a02ee0 => (IntPtr)At(0x00a02ee0);

    /// <summary>
    /// 0x00a2a070 .. 0x00a2a0a0 — les treize constantes `float` des tables UV (`.rdata`) : [0] le pas
    /// de la table 0, [1] son origine (sprites), [2]/[4]/[6] les pas des trois familles, [3]/[5]/[7]
    /// leurs origines (sprites) ; [9] l'origine de la table 0 et [10]/[11]/[12] celles des familles
    /// (polygones).
    /// </summary>
    internal static float* ConstantesUv_00a2a070 => (float*)At(0x00a2a070);

    /// <summary>_DAT_00a2a024 — le facteur de <see cref="TableProfondeurs_00a2f478"/>.</summary>
    internal static float FacteurProfondeurs_00a2a024 => *(float*)At(0x00a2a024);

    /// <summary>_DAT_00a2a028 — le gamma « jamais pose » : si le gamma courant l'egale, `FUN_00a03110` pose 1.0.</summary>
    internal static float GammaInitial_00a2a028 => *(float*)At(0x00a2a028);

    // ----------------------------------------------------------------------------------------
    // Le dialogue de choix du pilote — `Device.cs`, `Setup3DDevice` (823).
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00a2e8e8 — le `HINSTANCE` de la DLL, pose par son point d'entree : celui dont le dialogue 0x66 est une ressource.</summary>
    internal static IntPtr InstanceDll_00a2e8e8 => *(IntPtr*)At(0x00a2e8e8);

    /// <summary>DAT_00a2e8e0 — le bloc de parametres du dialogue en cours, que sa procedure (native) relit.</summary>
    internal static ref byte* ParametresDialogue_00a2e8e0 => ref *(byte**)At(0x00a2e8e0);

    /// <summary>Les entrees `DirectDrawEnumerateA` et `DirectDrawCreate` de la table d'imports, que les deux thunks `jmp [..]` suivent.</summary>
    internal static void** ImportDirectDrawEnumerateA_01215244 => (void**)At(0x01215244);
    internal static void** ImportDirectDrawCreate_01215248 => (void**)At(0x01215248);

    /// <summary>FUN_00a012f0 — un CODE : la procedure du dialogue de choix du pilote, restee native, passee a `DialogBoxParamA`.</summary>
    internal static IntPtr ProcedureDialogue_00a012f0 => (IntPtr)At(0x00a012f0);

    /// <summary>LAB_00a01810 — un CODE : le rappel de `DirectDrawEnumerateA`, reste natif ; il remplit le bloc des pilotes.</summary>
    internal static IntPtr RappelPilotes_00a01810 => (IntPtr)At(0x00a01810);

    /// <summary>LAB_00a01000 — un CODE : le rappel de `EnumDisplayModes`, reste natif.</summary>
    internal static IntPtr RappelModes_00a01000 => (IntPtr)At(0x00a01000);

    /// <summary>&amp;DAT_00a2c018 — la taille (clé « Size ») ecrite pour chaque resolution : 1, 7, 11, 16, 23.</summary>
    internal static int* TaillesParResolution_00a2c018 => (int*)At(0x00a2c018);

    /// <summary>
    /// Les chaines du dialogue et du fichier de configuration, 0x00a2c058 .. 0x00a2c13c : « unknown »,
    /// « display », « 3Dfx Glide », « Glide », « _grSstQueryBoards@4 », « GLIDE2X.DLL »,
    /// « Direct3D Software Emulation », « Software », « SOFTWARE », « GLIDE », « Small », « 0 », « 1 »,
    /// « Size », « %d », « Options », « Resolution », « Description », « D3DDriver », « Driver », « !! »,
    /// « .cfg ». Designees par leur VA : `Chaine(0x00a2c0b8)`.
    /// </summary>
    internal static byte* Chaine(nuint va) => At(va);

    /// <summary>DAT_011be6d4 — la TIM de la police 8 x 16, une ressource de la DLL (`LockResource` a l'initialisation). `FntLoad` la lit a +4.</summary>
    internal static ref byte* PoliceTim_011be6d4 => ref *(byte**)At(0x011be6d4);

    /// <summary>DAT_00b6d5b0 — la police kanji 16 x 16, une ressource de la DLL : un index de `short` par code Shift-JIS depuis 0x8140, puis les dessins (32 octets) a +0x2e64.</summary>
    internal static ref byte* PoliceKanji_00b6d5b0 => ref *(byte**)At(0x00b6d5b0);

    /// <summary>&amp;DAT_00a2c388 — le dessin rendu pour un code hors table : une diagonale de 16 x 16 (`0x0001, 0x0002, 0x0004`…).</summary>
    internal static byte* GlypheParDefaut_00a2c388 => At(0x00a2c388);

    /// <summary>DAT_011d0820 — la distance de projection `h` du GTE, posee par `SetGeomScreen` (379).</summary>
    internal static ref int DistanceEcran_011d0820 => ref *(int*)At(0x011d0820);

    /// <summary>
    /// _DAT_00a2c5f0 — le plan proche de la projection du device, un `int` (`fild`). Vaut 1 au
    /// chargement ; ecrit par `FUN_00a1dae0` (631) — son jumeau `FUN_00a1db50` (632) ecrit le lointain —, que rien d'atteint n'appelle.
    /// </summary>
    internal static ref int PlanProche_00a2c5f0 => ref *(int*)At(0x00a2c5f0);

    /// <summary>_DAT_00a2c5f4 — le plan lointain, 0x01388000 (20 480 000) au chargement.</summary>
    internal static ref int PlanLointain_00a2c5f4 => ref *(int*)At(0x00a2c5f4);

    /// <summary>
    /// _DAT_00a2c5f8 — la distance de projection vue du device (1000 au chargement), la copie que
    /// `FUN_00a1dbc0` (564) garde de <see cref="DistanceEcran_011d0820"/>.
    /// </summary>
    internal static ref int DistanceDevice_00a2c5f8 => ref *(int*)At(0x00a2c5f8);

    /// <summary>
    /// Les quatre tables de la racine carree, remplies a l'initialisation de la DLL et relues par
    /// les ordinaux 439 et 442 : une par ordre de grandeur de l'argument.
    /// </summary>
    internal static ushort* RacinesPetites_011d0620 => (ushort*)At(0x011d0620);

    /// <summary>Indexee par `n >> 8`, pour `n` &lt; 0x10000.</summary>
    internal static ushort* Racines8_011d0420 => (ushort*)At(0x011d0420);

    /// <summary>Indexee par `n >> 16`, pour `n` &lt; 0x1000000.</summary>
    internal static ushort* Racines16_011f0c90 => (ushort*)At(0x011f0c90);

    /// <summary>Indexee par `n >> 24`.</summary>
    internal static ushort* Racines24_011cb210 => (ushort*)At(0x011cb210);

    // ----------------------------------------------------------------------------------------
    // Les evenements de la libapi — `Evenements.cs`.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00dedc24 — le premier nœud d'evenement, d'ou `DeliverEvent` part.</summary>
    internal static ref int* PremierEvenement_00dedc24 => ref *(int**)At(0x00dedc24);

    /// <summary>DAT_00dedc20 — le dernier nœud ouvert.</summary>
    internal static ref int* DernierEvenement_00dedc20 => ref *(int**)At(0x00dedc20);

    /// <summary>DAT_011be794 — l'evenement Win32 que `DeliverEvent` signale et que `TestEvent` remet a zero.</summary>
    internal static ref IntPtr EvenementWin32_011be794 => ref *(IntPtr*)At(0x011be794);
}
