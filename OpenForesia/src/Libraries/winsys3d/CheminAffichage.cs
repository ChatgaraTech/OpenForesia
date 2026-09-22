// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** (0x00a061b0, 288 octets) — voir « Ce que Ghidra ne donne pas ».
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE CHEMIN D'AFFICHAGE — premiere pierre : `DrawOTag`, l'ordinal 701
//
// C'est le seul `[DllImport]` du perimetre de `Documentation/Etat/CheminAffichage.md`, et c'est
// donc la porte : 157 corps et 12 408 lignes sont derriere lui. Il est porte ici **en premier et
// seul**, avec ses sept appelees encore natives (`Natif.cs`) et ses globales aliasees sur le
// `.data` reel de la DLL (`Data.cs`). Le jeu doit afficher exactement la meme chose qu'avant —
// c'est la seule verification qui compte, et elle est disponible des maintenant.
//
// ============================================================================================
// CE QUE `DrawOTag` FAIT, EN UNE IMAGE
//
//     si la garde est posee               -> ne rien faire du tout
//     remettre les trois memos a -1       l'etat du tampon de commandes redemarre a chaque image
//     basculer l'arene de travail         0 <-> 0x40000, un double tampon
//     si l'image est en retard            -> ne rien dessiner, la rattraper
//     etat de rendu = 8, compteur++
//     PASSE 1 : parcourir l'OT            FUN_00a26b20 sur chaque nœud vivant  (les textures)
//     effacer                             device[+0x5c] ou [+0x60], selon un octet
//     device[+0xb0]()                     BeginScene
//     PASSE 2 : parcourir l'OT            FUN_00a13400 sur chaque nœud vivant  (le rejeu)
//     device[+0xb4]()                     EndScene
//     le rappel de fin d'image, s'il existe
//
// **L'OT est une liste chainee sur 24 bits**, la convention PlayStation que
// `Documentation/DLL/winsys3d.md` documente en tete : le mot d'un nœud porte le lien dans ses
// 24 bits bas et une longueur dans l'octet haut. Un nœud dont l'octet haut est nul est **saute
// sans etre suivi** — mais la marche continue par son lien. La sentinelle est 0xffffff.
//
// **Les deux parcours sont distincts et complets.** Le premier prepare les textures de toute la
// scene avant que le moindre triangle ne parte ; le second rejoue. Les fusionner changerait
// l'ordre des liaisons de texture, donc l'image.
//
// ============================================================================================
// CE QUE GHIDRA NE DONNE PAS, ET QUE LE DESASSEMBLAGE A CORRIGE
//
// **`VSyncDelayed` est `DAT_011be67c`.** Ghidra affiche le nom d'export, ce qui cache que c'est
// une globale a une adresse connue ; le binaire fait `mov eax,[11BE67Ch]`. Les deux sont la meme
// chose — `Platform/Winsys3d.cs` la resout par `GetProcAddress` sous ce nom —, mais ici on la
// prend par son adresse, comme ses voisines.
//
// **`MainMem` est lu, pas ecrit.** `mov ecx,[0B6DBD4h]` puis `lea eax,[eax+ecx+200000h]` : la
// nouvelle arene vaut `MainMem + 0x200000 + bascule`.
//
// **La bascule est un double tampon, pas une addition.** Ghidra ecrit
// `iVar1 = -DAT_00dedbe4 ; DAT_00dedbe4 = iVar1 + 0x40000`, ce qui se lit comme une
// accumulation ; le binaire fait `eax = 0x40000 - [0DEDBE4h]`, et la valeur **alterne entre 0 et
// 0x40000**. Rien d'autre dans la DLL ne l'ecrit. Transcrire l'addition litteralement aurait
// donne la meme chose, mais personne n'aurait vu que c'etait un double tampon.
//
// **L'ordre des deux ecritures d'arene est inverse.** Ghidra pose `DAT_00dedbd8` puis
// `_DAT_00dedbdc` ; le binaire ecrit `[0DEDBDCh]` d'abord. Meme valeur dans les deux, donc sans
// consequence — note pour que la prochaine lecture ne s'y arrete pas.
//
// **`ret 4`** : un seul argument, et la valeur rendue n'est pas definie. La facade le declare
// deja `void`.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCheminAffichage
{
    /// <summary>Le lien d'un nœud d'OT tient sur 24 bits ; l'octet haut porte autre chose.</summary>
    private const uint LienMasque = 0x00ffffff;

    /// <summary>La fin de l'OT. Ghidra l'ecrit `&amp;DAT_00ffffff`, ce qui est la meme valeur.</summary>
    private const uint LienFin = 0x00ffffff;

    private const int Device_Effacer5c = 0x5c / 4;
    private const int Device_Effacer60 = 0x60 / 4;
    private const int Device_BeginScene = 0xb0 / 4;
    private const int Device_EndScene = 0xb4 / 4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a061b0 @ 0x00a061b0 (ordinal 701) — `DrawOTag` : dessine une
    /// image entiere a partir de la table d'ordonnancement que le jeu a remplie.
    ///
    /// Les sept appelees sont encore natives (`Natif.cs`) et les globales sont celles de la DLL
    /// (`Data.cs`) : ce corps-ci est donc interchangeable avec l'original, et c'est voulu — il
    /// doit produire la meme image avant qu'on porte la piece suivante,
    /// <c>FUN_00a13400</c>.
    /// </summary>
    internal static void DrawOTag(uint* ot)
    {
        // La garde : non nulle, l'image entiere est sautee. Personne dans le perimetre ne
        // l'ecrit — voir le TODO(REVERSE) de `Data.cs`.
        if (Winsys3dData.Garde_00dedc44 != 0) return;

        // L'etat du tampon de commandes redemarre a chaque image. C'est CE reset qui interdisait
        // de porter `DrawOTag` sans les producteurs tant que les globales etaient dupliquees :
        // ils auraient relu des memos que plus personne ne remettait a -1.
        Winsys3dData.Memo_00a2c484 = -1;
        Winsys3dData.TypePrimitif_00a2c488 = -1;
        Winsys3dData.Memo_00a2c48c = -1;

        // Le double tampon de l'arene de travail : 0 <-> 0x40000.
        int bascule = 0x40000 - Winsys3dData.Bascule_00dedbe4;
        Winsys3dData.Bascule_00dedbe4 = bascule;

        byte* arene = Winsys3dData.MainMem_00b6dbd4 + bascule + 0x200000;
        Winsys3dData.AreneDebut_00dedbdc = arene;
        Winsys3dData.Arene_00dedbd8 = arene;

        // L'image est en retard : tout le dessin est saute pour la rattraper. Le reset des memos
        // et la bascule ci-dessus ont quand meme eu lieu — c'est l'original qui en decide ainsi.
        if (Winsys3dData.VSyncDelayed_011be67c != 0) return;

        Winsys3dData.EtatRendu_00a2c480 = 8;
        Winsys3dData.CompteurImages_011be75c++;

        // ---- PASSE 1 : les textures de toute la scene, avant le moindre triangle ----
        Winsys3dNatif.Fun_00a26490();
        for (uint* noeud = ot; ; )
        {
            if ((byte)(*noeud >> 24) != 0) Winsys3dNatif.Fun_00a26b20(noeud);
            uint lien = *noeud & LienMasque;
            if (lien == LienFin) break;
            noeud = (uint*)lien;
        }
        Winsys3dNatif.Fun_00a027a0();
        Winsys3dNatif.Fun_00a26f20();

        // ---- l'effacement, puis BeginScene ----
        void** vtable = Winsys3dData.DeviceVtable;
        void* device = Winsys3dData.Device_011be760;

        if (Winsys3dData.ModeEffacement_011be6f0 != 0)
            ((delegate* unmanaged[Thiscall]<void*, int, int, void>)vtable[Device_Effacer5c])(device, 0, 0);
        else
            ((delegate* unmanaged[Thiscall]<void*, int, int, void>)vtable[Device_Effacer60])(device, 0, 0);

        // La vtable est relue apres l'effacement : le binaire recharge `[11BE760h]` avant chaque
        // appel, et ce n'est pas une optimisation manquee — l'effacement peut recreer le device.
        vtable = Winsys3dData.DeviceVtable;
        device = Winsys3dData.Device_011be760;
        ((delegate* unmanaged[Thiscall]<void*, void>)vtable[Device_BeginScene])(device);

        // ---- PASSE 2 : le rejeu ----
        for (uint* noeud = ot; ; )
        {
            if ((byte)(*noeud >> 24) != 0) RejouerNoeud(noeud);
            uint lien = *noeud & LienMasque;
            if (lien == LienFin) break;
            noeud = (uint*)lien;
        }
        Winsys3dNatif.Fun_00a027e0();

        vtable = Winsys3dData.DeviceVtable;
        device = Winsys3dData.Device_011be760;
        ((delegate* unmanaged[Thiscall]<void*, void>)vtable[Device_EndScene])(device);

        Winsys3dNatif.Fun_00a02820();

        // Le rappel de fin d'image, sans argument, s'il a ete pose.
        nint rappel = Winsys3dData.RappelFinImage_011be754;
        if (rappel != 0) ((delegate* unmanaged[Stdcall]<void>)rappel)();
    }

    // ============================================================================================
    // LE REPARTITEUR DE PRIMITIVES — `FUN_00a13400`, porte le 2026-09-22
    //
    // Un nœud d'OT porte un PAQUET de primitives, pas une seule. Ce corps le deroule : il lit le
    // code de primitive a l'octet 7, appelle le gestionnaire correspondant, avance du nombre de
    // mots que ce code occupe, et recommence jusqu'a epuiser la longueur annoncee dans l'octet de
    // poids fort du premier mot.
    //
    // **118 etiquettes de `case`, 42 gestionnaires distincts.** La table est reproduite
    // mecaniquement depuis `winsys3d.c` — elle n'a pas ete tapee a la main — et les arites des 48
    // appelees sont relevees dans `Legacy/Raw/Winsys3d/FUN_00a13400.arites.txt`.
    //
    // --------------------------------------------------------------------------------------------
    // LE DRAPEAU D'ECLAIRAGE REDIRIGE, IL NE TESTE PAS
    //
    //     cle = (code < 0x80) ? code | Elo_NoLighting : code
    //
    // `Elo_NoLighting` n'est pas consulte a l'interieur d'un cas : il est **OU-e dans la cle du
    // switch**. Les codes 0x20..0x23 tombent sur un seul gestionnaire, 0x24..0x27 sur quatre
    // differents — poser le drapeau fait donc basculer chaque primitive sur sa variante non
    // eclairee. Transcrire cela en `if (Elo_NoLighting)` a l'interieur des cas serait faux.
    //
    // --------------------------------------------------------------------------------------------
    // QUATRE ENDROITS QUE GHIDRA SEUL NE TRANCHE PAS
    //
    // **1. `local_50`, dans le cas 0xe3.** Ghidra appelle `FUN_00a0cdd0(local_50)` sur une locale
    // qu'il n'a jamais ecrite. Le desassemblage la resout : `lea eax,[esi+8]` a `0x00a13a12`, range
    // dans `[esp+18h]`, relu a `0x00a13b60`. **C'est `param_1 + 2`** — la meme valeur que l'autre
    // branche, que Ghidra nomme correctement. Les deux chemins appellent donc la meme chose.
    //
    // **2. La chute du cas 0xe2 dans les cas 0..3.** Il n'y a pas de `break` apres
    // `param_1 = param_1 + 1` : le decompte `uVar12 - 1` des cas 0..3 s'applique aussi a 0xe2. En
    // C# cela s'ecrit `goto case 0`, et l'oublier decalerait le paquet d'un mot.
    //
    // **3. Le pavage des sprites (codes 0x64..0x67).** Le seul cas qui ne soit pas un appel : quand
    // `DAT_011cb1e0` est non nul, le sprite est **decoupe en tuiles** par une double boucle qui
    // reecrit temporairement quatre champs du paquet, emet une primitive par tuile, puis restaure
    // les quatre champs. Les tuiles font `DAT_011cb1d8 / 2` par `DAT_011cb1dc / 2`.
    //
    // **4. `DAT_00a2c5a0` vaut -1 et n'est jamais ecrit.** Le test `paquet[b + 8] != -1` veut donc
    // dire « ce paquet porte-t-il une matrice ». Et `_DAT_00a2a0b8` vaut `0x39800000`, soit
    // **1/4096** : la virgule fixe 4.12 de la libgte, la meme que `ScaleMatrix`.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a13400 @ 0x00a13400 — deroule le paquet de primitives d'un nœud de
    /// l'OT. Appele par <see cref="DrawOTag"/> sur chaque nœud dont l'octet de poids fort est non
    /// nul, pendant la seconde passe.
    /// </summary>
    internal static void RejouerNoeud(uint* noeud)
    {
        // L'octet de poids fort du premier mot : la longueur du paquet, en mots.
        uint restant = *noeud >> 24;

        do
        {
            // Relue a CHAQUE tour, et pas une fois pour toutes : le cas 0xe2 peut la changer en
            // cours de paquet, et le pavage s'en sert juste apres.
            int hauteurTuile = Winsys3dData.HauteurTuile_011cb1dc;

            uint codeBrut = ((byte*)noeud)[7];
            uint cle = codeBrut < 0x80
                ? codeBrut | (uint)Winsys3dData.EloNoLighting_011be758
                : codeBrut;

            switch (cle)
            {
            case 0x20: case 0x21: case 0x22: case 0x23: 
                Winsys3dNatif.Fun_00a0ef40(noeud); noeud += 4; restant -= 4; break;
            case 0x24: 
                Winsys3dNatif.Fun_00a0f090(noeud); noeud += 7; restant -= 7; break;
            case 0x25: 
                Winsys3dNatif.Fun_00a0f260(noeud); noeud += 7; restant -= 7; break;
            case 0x26: 
                Winsys3dNatif.Fun_00a116e0(noeud); noeud += 7; restant -= 7; break;
            case 0x27: 
                Winsys3dNatif.Fun_00a118e0(noeud); noeud += 7; restant -= 7; break;
            case 0x28: case 0x29: case 0x2a: case 0x2b: 
                Winsys3dNatif.Fun_00a0f3f0(noeud); noeud += 5; restant -= 5; break;
            case 0x2c: 
                Winsys3dNatif.Fun_00a0f570(noeud); noeud += 9; restant -= 9; break;
            case 0x2d: 
                Winsys3dNatif.Fun_00a0f780(noeud); noeud += 9; restant -= 9; break;
            case 0x2e: 
                Winsys3dNatif.Fun_00a11a90(noeud); noeud += 9; restant -= 9; break;
            case 0x2f: 
                Winsys3dNatif.Fun_00a11cd0(noeud); noeud += 9; restant -= 9; break;
            case 0x30: case 0x31: case 0x32: case 0x33: 
                Winsys3dNatif.Fun_00a0f940(noeud); noeud += 6; restant -= 6; break;
            case 0x34: 
                Winsys3dNatif.Fun_00a0fb40(noeud); noeud += 9; restant -= 9; break;
            case 0x35: 
                Winsys3dNatif.Fun_00a0fdc0(noeud); noeud += 9; restant -= 9; break;
            case 0x36: 
                Winsys3dNatif.Fun_00a11ec0(noeud); noeud += 9; restant -= 9; break;
            case 0x37: 
                Winsys3dNatif.Fun_00a12190(noeud); noeud += 9; restant -= 9; break;
            case 0x38: case 0x39: case 0x3a: case 0x3b: 
                Winsys3dNatif.Fun_00a0ff50(noeud); noeud += 8; restant -= 8; break;
            case 0x3c: 
                Winsys3dNatif.Fun_00a101c0(noeud); noeud += 0xc; restant -= 0xc; break;
            case 0x3d: 
                Winsys3dNatif.Fun_00a104c0(noeud); noeud += 0xc; restant -= 0xc; break;
            case 0x3e: 
                Winsys3dNatif.Fun_00a12350(noeud); noeud += 0xc; restant -= 0xc; break;
            case 0x3f: 
                Winsys3dNatif.Fun_00a126d0(noeud); noeud += 0xc; restant -= 0xc; break;
            case 0x40: case 0x41: case 0x42: case 0x43: 
            case 0x44: case 0x45: case 0x46: case 0x47: 
                Winsys3dNatif.Fun_00a0e580(noeud); noeud += 3; restant -= 3; break;
            case 0x48: case 0x49: case 0x4a: case 0x4b: 
                Winsys3dNatif.Fun_00a0e6a0(noeud); noeud += 5; restant -= 5; break;
            case 0x4c: case 0x4d: case 0x4e: case 0x4f: 
                Winsys3dNatif.Fun_00a0e7f0(noeud); noeud += 6; restant -= 6; break;
            case 0x50: case 0x51: case 0x52: case 0x53: 
            case 0x54: case 0x55: case 0x56: case 0x57: 
                Winsys3dNatif.Fun_00a0e970(noeud); noeud += 4; restant -= 4; break;
            case 0x58: case 0x59: case 0x5a: case 0x5b: 
                Winsys3dNatif.Fun_00a0eaf0(noeud); noeud += 7; restant -= 7; break;
            case 0x5c: case 0x5d: case 0x5e: case 0x5f: 
                Winsys3dNatif.Fun_00a0ecf0(noeud); noeud += 9; restant -= 9; break;
            case 0x60: case 0x61: case 0x62: case 0x63: 
                Winsys3dNatif.Fun_00a10690(noeud); noeud += 3; restant -= 3; break;
            case 0x68: case 0x69: case 0x6a: case 0x6b: 
            case 0x6c: case 0x6d: case 0x6e: case 0x6f: 
                Winsys3dNatif.Fun_00a10b60(noeud); noeud += 2; restant -= 2; break;
            case 0x70: case 0x71: case 0x72: case 0x73: 
                Winsys3dNatif.Fun_00a10ca0(noeud); noeud += 2; restant -= 2; break;
            case 0x74: 
                Winsys3dNatif.Fun_00a10df0(noeud); noeud += 3; restant -= 3; break;
            case 0x75: 
                Winsys3dNatif.Fun_00a11000(noeud); noeud += 3; restant -= 3; break;
            case 0x76: 
                Winsys3dNatif.Fun_00a128d0(noeud); noeud += 3; restant -= 3; break;
            case 0x77: 
                Winsys3dNatif.Fun_00a12ac0(noeud); noeud += 3; restant -= 3; break;
            case 0x78: case 0x79: case 0x7a: case 0x7b: 
                Winsys3dNatif.Fun_00a111c0(noeud); noeud += 2; restant -= 2; break;
            case 0x7c: 
                Winsys3dNatif.Fun_00a11310(noeud); noeud += 3; restant -= 3; break;
            case 0x7d: 
                Winsys3dNatif.Fun_00a11520(noeud); noeud += 3; restant -= 3; break;
            case 0x7e: 
                Winsys3dNatif.Fun_00a12c70(noeud); noeud += 3; restant -= 3; break;
            case 0x7f: 
                Winsys3dNatif.Fun_00a12e60(noeud); noeud += 3; restant -= 3; break;

            // ---- le pavage des sprites : le seul cas qui ne soit pas un simple appel ----
            case 0x64: case 0x65: case 0x66: case 0x67:
                if (Winsys3dData.Pavage_011cb1e0 == 0)
                {
                    // Le chemin ordinaire. Noter que le sous-switch porte sur le code BRUT, pas
                    // sur la cle : le drapeau d'eclairage a deja fait son office plus haut.
                    EmettreSprite(noeud, codeBrut);
                    noeud += 4; restant -= 4;
                }
                else
                {
                    short hautSauve = *(short*)((byte*)noeud + 0x12);
                    short largSauve = (short)noeud[4];
                    short xSauve = (short)noeud[2];
                    short ySauve = *(short*)((byte*)noeud + 10);

                    int demiLargeur = Winsys3dData.LargeurTuile_011cb1d8 >> 1;
                    *(short*)(noeud + 4) = (short)demiLargeur;

                    int xFin = xSauve + largSauve;
                    if ((short)noeud[2] < xFin)
                    {
                        do
                        {
                            // Rogner la derniere colonne si elle deborde.
                            int trop = demiLargeur - largSauve - xSauve + (short)noeud[2];
                            if (0 < trop) *(short*)(noeud + 4) = (short)((short)noeud[4] - (short)trop);

                            short demiHauteur = (short)(hauteurTuile >> 1);
                            *(short*)((byte*)noeud + 0x12) = demiHauteur;
                            *(short*)((byte*)noeud + 10) = ySauve;

                            short y = ySauve;
                            while (y < ySauve + hautSauve)
                            {
                                // Rogner la derniere rangee si elle deborde.
                                int tropY = (hauteurTuile >> 1) - ySauve - hautSauve
                                          + *(short*)((byte*)noeud + 10);
                                if (0 < tropY)
                                    *(short*)((byte*)noeud + 0x12) =
                                        (short)(*(short*)((byte*)noeud + 0x12) - (short)tropY);

                                EmettreSprite(noeud, ((byte*)noeud)[7]);

                                *(short*)((byte*)noeud + 10) =
                                    (short)(*(short*)((byte*)noeud + 10) + demiHauteur);
                                y = *(short*)((byte*)noeud + 10);
                            }
                            *(short*)(noeud + 2) = (short)((short)noeud[2] + (short)demiLargeur);
                        }
                        while ((short)noeud[2] < xFin);
                    }

                    // Le paquet est rendu tel qu'on l'a trouve : il sera peut-etre rejoue.
                    *(short*)(noeud + 4) = largSauve;
                    *(short*)((byte*)noeud + 0x12) = hautSauve;
                    *(short*)(noeud + 2) = xSauve;
                    *(short*)((byte*)noeud + 10) = ySauve;

                    noeud += 4; restant -= 4;
                }
                break;

            // ---- les opcodes de service ----
            case 0xe1:
                Winsys3dData.Demi_011be6ec = (ushort)noeud[1];
                noeud += 1; restant -= 1;
                break;

            case 0xe2:
                Winsys3dData.Pavage_011cb1e0 = (int)(noeud[1] & 0xffffff);
                if (Winsys3dData.Pavage_011cb1e0 != 0)
                {
                    Winsys3dData.LargeurTuile_011cb1d8 = (int)(noeud[1] & 0x1f);
                    Winsys3dData.HauteurTuile_011cb1dc = Winsys3dData.Pavage_011cb1e0 >> 5 & 0x1f;
                    Winsys3dData.Pavage_011cb1d0 = Winsys3dData.Pavage_011cb1e0 >> 10 & 0x1f;
                    Winsys3dData.Pavage_011cb1d4 = Winsys3dData.Pavage_011cb1e0 >> 0xf & 0x1f;
                }
                noeud += 1;
                goto case 0;        // la CHUTE de l'original : pas de `break` apres l'avance

            case 0x0: case 0x1: case 0x2: case 0x3:
                restant -= 1;
                break;

            // ---- les opcodes qui posent une matrice puis RENDENT LA MAIN ----
            case 0xe3:
                PoserMatriceSiPresente(noeud, 8);
                Winsys3dNatif.Fun_00a0cdd0(noeud + 2);
                return;

            case 0xe4:
                PoserMatriceSiPresente(noeud, 8);
                Winsys3dNatif.Fun_00a0cdd0(noeud + 2);
                return;

            case 0xe5:
                Winsys3dNatif.Fun_00a0e0c0(noeud + 2);
                return;

            case 0xe6:
                PoserMatriceSiPresente(noeud, 5);
                Winsys3dNatif.Fun_00a0e000(noeud[2], noeud[3], (short)noeud[4]);
                return;

            case 0xe7:
                PoserMatriceSiPresente(noeud, 5);
                Winsys3dNatif.Fun_00a133e0(noeud[2], noeud[3], (short)noeud[4]);
                return;

            case 0xe8:
                Winsys3dNatif.Fun_00a142a0(noeud + 2);
                return;

            case 0xf9:
                PoserMatriceSiPresente(noeud, 8);
                Winsys3dNatif.Fun_00a0e040(noeud + 2);
                return;

            // ---- la fin de paquet, et les deux opcodes de vue ----
            case 0xf1: case 0xf3: case 0xf4: case 0xf5:
            case 0xf6: case 0xf7: case 0xf8:
                // -1 : la condition de boucle `0 < (int)restant` est fausse, on sort.
                restant = unchecked((uint)-1);
                break;

            case 0xf2:
                ((delegate* unmanaged[Thiscall]<void*, int, int, void>)
                    Winsys3dData.DeviceVtable[Device_Effacer5c])(Winsys3dData.Device_011be760, 0, 0);
                return;

            case 0xfb:
                if (noeud[2] == 0xffffffff)
                {
                    // Restaurer le decalage de vue sauvegarde au passage precedent.
                    Blaze.Winsys3d.SetShift(Winsys3dData.DecalSauveX_011cb1bc,
                                            Winsys3dData.DecalSauveY_011cb1b8,
                                            Winsys3dData.DecalSauveZoom_011be7e0, 1);
                    return;
                }
                Winsys3dData.DecalSauveX_011cb1bc = Winsys3dData.DecalX_011be778;
                Winsys3dData.DecalSauveY_011cb1b8 = Winsys3dData.DecalY_011be77c;
                Winsys3dData.DecalSauveZoom_011be7e0 = Winsys3dData.Zoom_00a2c498;
                Blaze.Winsys3d.SetShift((int)noeud[2], (int)noeud[3], (int)(noeud[1] & 0xff), 1);
                return;

            default:
                // Le `goto switchD_00a13437_caseD_4` de Ghidra : tout code inconnu arrete le
                // deroulement du paquet, sans rien signaler.
                return;
            }
        }
        while ((int)restant > 0);
    }

    /// <summary>
    /// Le sous-switch des sprites, partage par les deux branches du cas 0x64..0x67 — chez
    /// l'original il est ecrit deux fois, a l'identique. Il porte sur les **deux bits de poids
    /// faible du code brut**, pas sur la cle du switch principal.
    /// </summary>
    private static void EmettreSprite(uint* noeud, uint codeBrut)
    {
        switch (codeBrut & 3)
        {
            case 0: Winsys3dNatif.Fun_00a107e0(noeud); break;
            case 1: Winsys3dNatif.Fun_00a109c0(noeud); break;
            case 2: Winsys3dNatif.Fun_00a13010(noeud); break;
            default: Winsys3dNatif.Fun_00a13220(noeud); break;
        }
    }

    /// <summary>
    /// Les cinq opcodes 0xe3, 0xe4, 0xe6, 0xe7 et 0xf9 portent le meme bloc, ecrit cinq fois chez
    /// l'original et ne differant que par le mot de depart : <b>8</b> pour 0xe3/0xe4/0xf9,
    /// <b>5</b> pour 0xe6/0xe7. Il pose une transformation sur le device si le paquet en porte une.
    ///
    /// La matrice du paquet est <b>3 x 3 de shorts en virgule fixe 4.12, plus trois entiers de
    /// translation</b>, et elle part vers le device en 4 x 4 de flottants <b>transposee</b> — la
    /// PlayStation range par lignes, Direct3D attend l'autre sens. Le mot <c>b + 8</c> vaut -1
    /// quand il n'y a pas de matrice, et -1 est une constante : <c>DAT_00a2c5a0</c> n'est ecrit
    /// par personne.
    /// </summary>
    private static void PoserMatriceSiPresente(uint* noeud, int motDeBase)
    {
        if ((int)noeud[motDeBase + 8] == Winsys3dData.PasDeMatrice_00a2c5a0) return;

        float echelle = Winsys3dData.EchelleFixe_00a2a0b8;
        short* r = (short*)(noeud + motDeBase);

        float* m = stackalloc float[16];
        m[0] = r[0] * echelle;  m[1] = r[3] * echelle;  m[2] = r[6] * echelle;  m[3] = 0f;
        m[4] = r[1] * echelle;  m[5] = r[4] * echelle;  m[6] = r[7] * echelle;  m[7] = 0f;
        m[8] = r[2] * echelle;  m[9] = r[5] * echelle;  m[10] = r[8] * echelle; m[11] = 0f;
        m[12] = (int)noeud[motDeBase + 5];
        m[13] = (int)noeud[motDeBase + 6];
        m[14] = (int)noeud[motDeBase + 7];
        m[15] = 1f;

        ((delegate* unmanaged[Thiscall]<void*, float*, void>)
            Winsys3dData.DeviceVtable[0x6c / 4])(Winsys3dData.Device_011be760, m);
    }
}
