# 3dhelp.dll — cartographie du device 3D

Décompilation : `3dhelp.c` (racine, sortie Ghidra, **lecture seule**). Base d'image `0x0FFE0000`.
Binaire : `3dhelp.dll`, 29 696 octets, 97 fonctions décompilées.

Ce document est le livrable de l'**étape 1** décrite dans `TODO.md` : cartographier l'interface que
`winsys3d` utilise, pour dimensionner un backend de remplacement.

---

## Ce qu'est ce device

`Create3DDevice` (`3dhelp.c` ligne 342, ordinal 1) alloue **200 octets** et construit un objet C++
dont la vtable est `PTR_FUN_0ffe6000`. Cet objet est **un shim mince sur `IDirect3DDevice2`** :
la majorité de ses méthodes se contentent de rappeler le device Direct3D ou une surface DirectDraw
qu'il détient.

`winsys3d` le pilote à travers **37 slots de vtable distincts**, sur les 59 que la vtable expose.

> **Correction.** Une première rédaction de ce document annonçait 34 slots. Ghidra écrit certains
> offsets en **décimal** (`+ 100` pour `0x64`), et l'extraction ne cherchait que la forme
> hexadécimale. Quatre slots manquaient, dont `+0x64` qui est le **troisième plus appelé**.
> Relevé correct :
>
> ```
> grep -oE "\*DAT_011be760 \+ (0x[0-9a-f]+|[0-9]+)\)" winsys3d.c
> ```

### `winsys3d` n'utilise que la vtable

Vérifié exhaustivement : chaque usage est soit `(**(code **)(*DAT_011be760 + offset))(...)`, soit
un test de nullité, soit `return DAT_011be760`. **Aucun champ du device n'est lu directement.**

Conséquence pour un remplacement : l'objet peut être un **pur porteur de vtable** — un seul
pointeur suffit, aucune disposition mémoire à reproduire.

### Champs connus de l'objet (indices en dwords)

| index | offset | contenu |
|---|---|---|
| `[0x0D]` | `+0x34` | surface DirectDraw (celle que `Flip` fait tourner) |
| `[0x0E]` | `+0x38` | objet DirectDraw |
| `[0x10]` | `+0x40` | surface DirectDraw primaire |
| `[0x15]` | `+0x54` | surface DirectDraw secondaire |
| `[0x16]` | `+0x58` | surface optionnelle — **nulle en pratique**, `3dhelp` gère ce cas |
| `[0x17]` | `+0x5C` | **`IDirect3DDevice2`** — la cible de la plupart des slots |
| `[0x2B]` | `+0xAC` | **objet matériau** (vtable `0x0FFE6248`) — porte le `D3DMATERIAL` dont `hTexture` est réécrit à chaque liaison de texture |
| — | `+0xA0`, `+0xA4`, `+0xB4`, `+0xB8` | champs d'état, lus par les getters |

Vérifié à l'exécution : `[0x10]`, `[0x15]` et `[0x17]` sont toujours non nuls, `[0x16]` toujours nul.

---

## Les 34 slots

Rôles établis par décodage du code machine, sauf mention « décompilé ». Les identifiants d'état
sont ceux de Direct3D 3 (`D3DRENDERSTATE_*`).

### Cœur de l'affichage — **le sous-ensemble minimal**

| slot | cible | taille | rôle |
|---|---|---|---|
| `+0xB0` | `0x0FFE2880` | 96 | **BeginScene** + récupération de surfaces perdues (décompilé) |
| `+0xB4` | `0x0FFE28E0` | 16 | **EndScene** — `d3d->vtable[0x2C]` |
| `+0xBC` | `0x0FFE1CE0` | 48 | **DrawPrimitive** (5 arguments) — `d3d->vtable[0x74]` |
| `+0xDC` | `0x0FFE1E00` | 32 | **Flip** — `surface[0x0D]->vtable[0x2C]`, 2 arguments |
| `+0x5C` | `0x0FFE2830` | 80 | mise en place du contexte, rappelée par la récupération |
| `+0x60` | `0x0FFE27D0` | 96 | mise en place du contexte, rappelée par la récupération |

**Quatre méthodes suffisent à sortir une image** : `BeginScene`, `DrawPrimitive`, `EndScene`,
`Flip`. C'est le chiffre que l'étape 1 devait produire.

### États de rendu — shims triviaux sur `IDirect3DDevice2`

Tous suivent le même motif : `MOV EAX,[ECX+0x5C]` puis `d3d->SetRenderState(id, valeur)`.

| slot | cible | taille | rôle |
|---|---|---|---|
| `+0x8C` | `0x0FFE1AD0` | 32 | `SetRenderState(9 SHADEMODE)` |
| `+0x90` | `0x0FFE1AF0` | 64 | filtre de texture : `SetRenderState(0x11 TEXTUREMAG)` puis `(0x12 TEXTUREMIN)` |
| `+0x94` | `0x0FFE1B30` | 32 | `SetRenderState(0x15 TEXTUREMAPBLEND)` |
| `+0x98` | `0x0FFE1B50` | 32 | `SetRenderState(0x1B ALPHABLENDENABLE)` |
| `+0x9C` | `0x0FFE1B70` | 48 | `SetRenderState(0x21)` puis `(0x27 STIPPLEENABLE)` |
| `+0xA0` | `0x0FFE1BA0` | 32 | `SetRenderState(0x13 SRCBLEND)` |
| `+0xA4` | `0x0FFE1BC0` | 32 | `SetRenderState(0x14 DESTBLEND)` |
| `+0xA8` | `0x0FFE1BE0` | 32 | `SetRenderState(id, valeur)` générique, 2 arguments |
| `+0x6C` | `0x0FFE1990` | 32 | `SetTransform(1 WORLD, matrice)` — `d3d->vtable[0x68]` |
| `+0x80` | `0x0FFE19B0` | 32 | `SetLightState(2 AMBIENT, valeur)` — `d3d->vtable[0x64]` |

### Accès aux surfaces

| slot | cible | taille | rôle |
|---|---|---|---|
| `+0xC4` | `0x0FFE1D40` | 32 | `surface[0x15]->GetDC` |
| `+0xCC` | `0x0FFE1D80` | 32 | `surface[0x10]->GetDC` |
| `+0xD0` | `0x0FFE1DA0` | 32 | `surface[0x10]->ReleaseDC` |
| `+0xD8` | `0x0FFE1DE0` | 32 | **attente de retour de balayage vertical** — `[0x0D]->vtable[0x58]`, 2 arguments. Décrit à tort ici comme `GetSurfaceDesc`, voir la correction plus bas |

### Accesseurs et cycle de vie

| slot | cible | taille | rôle |
|---|---|---|---|
| `+0x0C` | `0x0FFE1840` | 48 | `Initialize(arg)` — remet `+0xA0`/`+0xB4`/`+0xB8` à zéro, stocke `arg` en `+0x1C` |
| `+0x10` | `0x0FFE1970` | 32 | **fabrique de textures** — `Malloc(0x20)` puis constructeur (vtable `0x0FFE61F8`). Voir « Les textures » |
| `+0x14` | `0x0FFE1A40` | 32 | **fabrique de matériaux** — `Malloc(0x68)` puis `FUN_0ffe30f0` (vtable `0x0FFE6248`) |
| `+0x20` | `0x0FFE1790` | 16 | getter du champ `+0xB4` |
| `+0x24` | `0x0FFE17A0` | 16 | getter du champ `+0xB8` |
| `+0x28` | `0x0FFE17A0` | 16 | **même fonction que `+0x24`** |
| `+0x3C` | `0x0FFE17C0` | 16 | getter du champ `+0xA0` |
| `+0x4C` | `0x0FFE1820` | 32 | aiguillage : si `+0xA4` non nul → `vtable[0x44]`, sinon `vtable[0x48]` |

### Les quatre gros morceaux — **à décompiler**

| slot | cible | taille | statut |
|---|---|---|---|
| `+0x30` | `0x0FFE20F0` | **800** | non décompilé |
| `+0xE8` | `0x0FFE2410` | **704** | non décompilé |
| `+0x2C` | `0x0FFE1E40` | **640** | non décompilé |
| `+0x34` | `0x0FFE26D0` | 256 | **décompilé** dans `3dhelp.c` |
| `+0x50` | `0x0FFE29E0` | 256 | non décompilé |
| `+0x18` | `0x0FFE1C20` | 192 | non décompilé |
| `+0xB8` | `0x0FFE2930` | 176 | non décompilé |

---

## Décompilation : plus de zone d'ombre

`3dhelp.c` ne couvrait que **4 des 33 cibles** — les autres sont atteintes uniquement par la
vtable, la classe que Ghidra n'émet pas comme fonctions. Elles ont été obtenues en forçant les
adresses puis en décompilant, **en mode headless, sans intervention** :

```powershell
$gh = "F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC"
& "$gh\support\analyzeHeadless.bat" <projet_temporaire> tmp3dhelp `
    -import 3dhelp.dll -scriptPath <dossier_script> `
    -postScript ExportVtableFuncs.java <sortie.c> -deleteProject
```

Résultat : 52 fonctions créées, **57 décompilées, 0 échec**. Le script force les cibles comme
fonctions, les décompile et écrit le pseudocode. Travailler dans un projet temporaire évite de
toucher au projet Ghidra du dépôt, qui est verrouillé quand l'interface est ouverte.

**Ce chemin est réutilisable pour n'importe quelle DLL du projet** — c'est la réponse générale à la
classe « corps atteints seulement par adresse » qui bloquait aussi le moteur de texte.

---

## Fréquence d'appel — ce qui dimensionne réellement l'étape 2

Nombre de sites d'appel dans `winsys3d.c` :

| slot | rôle | sites |
|---|---|---|
| `+0xBC` | **DrawPrimitive** | **45** |
| `+0x8C` | `SetRenderState(SHADEMODE)` | **45** |
| `+0x64` | **liaison de texture** — reçoit un *objet* texture, voir « Les textures » | **18** |
| `+0x6C` | `SetTransform(WORLD)` | 5 |
| `+0xA4`, `+0x94` | `SetRenderState` | 4 |
| `+0xB0`, `+0xB4`, `+0xA0`, `+0x98` | scène et états | 3 |
| tout le reste | — | 1 à 2 |

### Format des sommets — déterminant pour le portage

Les appels à `DrawPrimitive` sont visibles en clair dans `winsys3d.c` :

```c
(**(code **)(*DAT_011be760 + 0xbc))(6, 3, &DAT_011cb138, 4, 0)
(**(code **)(*DAT_011be760 + 0xbc))(4, 3, &DAT_011cb138, 3, 0)
(**(code **)(*DAT_011be760 + 0xbc))(2, 3, &DAT_011cb138, 3, 0)
```

Soit `DrawPrimitive(type, 3, sommets, nombre, 0)` avec les types Direct3D 6 = `TRIANGLEFAN`,
4 = `TRIANGLESTRIP`, 2 = `LINESTRIP`, et **3 = `D3DVT_TLVERTEX`**.

`TLVERTEX` signifie sommets **déjà transformés et éclairés**, en coordonnées écran. Un backend de
remplacement n'a donc **ni pipeline de transformation ni éclairage à reproduire** : uniquement des
triangles texturés ou dégradés en 2D. C'est le point qui rend l'étape 2 abordable.

**Les quatre grosses fonctions ont un seul site d'appel chacune.** `+0x2C`, `+0x30`, `+0xE8` et
`+0x50` ne sont donc **pas** le chemin chaud : ce sont de l'initialisation ou des opérations
ponctuelles. `+0x30` et `+0xE8` font des `Blt` de surface et posent `ZENABLE`/`ZWRITEENABLE` ;
`+0x50` appelle `SetCurrentViewport`.

Le chemin par frame se réduit à : **`Clear` ×2 → `BeginScene` → `DrawPrimitive` ×N → `EndScene` →
`Flip`**, avec une poignée d'états de rendu. C'est une frame Direct3D 3 tout à fait standard.

---

## Les textures — du DIB au handle

> Établi le 2026-08-12, en préparation de la tâche 3.5 de `replace-3d-backend-with-d3d11`.
> Chaque maillon est vérifié soit dans `3dhelp.c`, soit directement dans les octets de
> `3dhelp.dll` (`OpenForesia.Tools -- at 3dhelp.dll <VA> <n>`).

### Correction — ce que `+0x64` reçoit

Ce document annonçait pour `+0x64` : « `SetLightState(1 MATERIAL)` puis
`SetRenderState(1 TEXTUREHANDLE)` ». C'est le résumé de ce qui se passe **deux niveaux plus bas**,
et il a fait croire que l'argument du slot était un handle opaque — donc que les pixels étaient
hors d'atteinte. Ils ne le sont pas. Le corps réel du slot est à `0x0FFE1900` (vtable device
`0x0FFE6000` + `0x64`) :

```
8b 44 24 04        mov eax,[esp+4]        ; l'argument
56                 push esi
8b f1              mov esi,ecx            ; this = device 3dhelp
50                 push eax
8b 8e ac 00 00 00  mov ecx,[esi+0xAC]     ; sous-objet MATÉRIAU
e8 6d 19 00 00     call 0x0FFE3280        ;   FUN_0ffe3280(materiau, argument)
8b 4e 5c           mov ecx,[esi+0x5C]     ; IDirect3DDevice2
51                 push ecx
8b 8e ac 00 00 00  mov ecx,[esi+0xAC]
e8 de 18 00 00     call 0x0FFE3200        ;   FUN_0ffe3200(materiau, d3dDevice)
5e                 pop esi
c2 04 00           ret 4
```

`device+0xAC` est un objet **matériau** (vtable `0x0FFE6248`, `Malloc(0x68)`, construit par
`FUN_0ffe30f0` à `3dhelp.c:863`). Il porte un `D3DMATERIAL` à son offset `+0x08`, `dwSize = 0x50`
écrit en clair. `FUN_0ffe3280` (`3dhelp.c:994`) fait :

```c
materiau->+0x58 = argument;
materiau->+0x50 = argument->vtable[1]();   /* +0x50 = struct+0x48 = hTexture */
```

`+0x50` tombe donc exactement sur le champ `hTexture` du `D3DMATERIAL`. Et `argument->vtable[1]`,
lu à `0x0FFE2B40`, est `mov eax,[ecx+0x10] / ret`.

**L'argument de `+0x64` est un pointeur d'objet texture, pas un handle.** Les deux `SetLightState`
/ `SetRenderState` que ce document mentionnait sont dans `FUN_0ffe3200` (`3dhelp.c:938`), en aval.

### La classe texture

Fabriquée par le **slot device `+0x10`** (`0x0FFE1970`) : `Malloc(0x20)` puis `FUN_0ffe2af0`, qui
pose la vtable `0x0FFE61F8`. Trente-deux octets, huit champs :

| offset | contenu |
|---|---|
| `+0x00` | vtable = `0x0FFE61F8` |
| `+0x04` | `IDirectDrawSurface` — **mémoire système, porte les pixels** |
| `+0x08` | `IDirectDrawSurface` — résultat de `CreateSurface`, la texture vue par Direct3D |
| `+0x0C` | `IDirectDrawPalette` — seulement si profondeur ≤ 8 bits |
| `+0x10` | `D3DTEXTUREHANDLE` — ce que `vtable[1]` renvoie |
| `+0x14` | drapeau de chemin : `0` = DIB palettisé via GDI, `≠0` = autre chemin |
| `+0x18` | a une palette |
| `+0x1C` | device `3dhelp` propriétaire (dont `+0x5C` = `IDirect3DDevice2`) |

Vtable de la classe, lue à `0x0FFE61F8` :

| slot | cible | rôle |
|---|---|---|
| `[0]` | `0x0FFE2B20` | destructeur (`FUN_0ffe2b20`) |
| `[1]` | `0x0FFE2B40` | `return this->+0x10` — le handle |
| `[2]` | `0x0FFE2D10` | **le chargeur**, décompilé à `3dhelp.c:677` |
| `[3]` | `0x0FFE15D0` | rôle non établi |
| `[4]` | `0x0FFE3020` | rôle non établi |
| `[5]` | `0x0FFE2F90` | rôle non établi — appelé en fin de chargement, vraisemblablement la remontée `+0x04` → `+0x08` |

### Le chargeur, `FUN_0ffe2d10`

Ghidra a typé son second paramètre en `BITMAPINFO*` alors que c'est le **`this`** qui l'est ; en
rétablissant, la fonction se lit sans ambiguïté. Elle prend un `BITMAPINFO` du jeu et produit un
handle :

```
BITMAPINFO
    │  DDSURFACEDESC dwSize=0x6C, dwFlags |= 0x1007
    │  ddsCaps = 0x1800 (TEXTURE|SYSTEMMEMORY)
    │       ou  0x4005000 si le device annonce la mémoire vidéo
    ▼
IDirectDraw::CreateSurface  (dd->vtable +0x18)   ──►  this->+0x08
    │
    ├─ une seule surface : this->+0x04 = this->+0x08, AddRef
    └─ sinon : second CreateSurface caps=0x1800   ──►  this->+0x04
    │
    ▼
DDPaletteFromBitmap  ──►  this->+0x0C
SetPalette (surf +0x7C) appliqué aux DEUX surfaces
    │
    ▼
FUN_0ffe2f20 :   surface(+0x04)->GetDC       (surf +0x44)
                 StretchDIBits(hdc, …, bitmapInfo, DIB_RGB_COLORS, SRCCOPY)
                 surface(+0x04)->ReleaseDC   (surf +0x68)
                 this->vtable[5]()           ← remontée +0x04 → +0x08
    │
    ▼
surface(+0x08)->QueryInterface(IID_IDirect3DTexture2)   IID à 0x0FFE6670
texture2->GetHandle(d3dDevice, &this->+0x10)            (tex2 +0x0C)
texture2->Release()
```

Deux faits en découlent, et ce sont eux qui rendent la tâche 3.5 abordable :

1. **la source des pixels est un DIB GDI ordinaire** — pas un format DirectDraw ni un format PSX ;
2. **la surface `+0x04` est en mémoire système par construction**, donc verrouillable, à la
   différence d'une texture en mémoire vidéo.

### Trois points d'accroche possibles

| route | où | ce qu'on obtient | dépendance |
|---|---|---|---|
| **A** | `+0x64`, l'objet est déjà construit | lire `+0x04`, `GetSurfaceDesc` (surf `+0x58`) puis `Lock` (surf `+0x64`) | exige que DirectDraw soit encore vivant après la tâche 4.3 — **non vérifié** |
| **B** | `+0x10` (fabrique, **déjà intercepté**) puis échange de la vtable de l'objet pour capter `[2]` | le `BITMAPINFO` d'origine, avant tout DirectDraw | aucune |
| **C** | `winsys3d`, en amont du DIB (`FUN_00a27430`, tpage/CLUT) | les données du jeu | chantier de reconstruction complet, hors tâche 3.5 |

La route **B** ne dépend d'aucun état DirectDraw et réutilise le mécanisme d'échange de vtable déjà
éprouvé dans `DeviceShim.cs`. La route **A** est plus courte mais son hypothèse reste à mesurer.

### Correction du 2026-08-14 — **le chargeur emprunté est le slot `[3]`, pas le `[2]`**

Tout ce qui précède décrit le slot `[2]` (`FUN_0ffe2d10`, chargement depuis un `BITMAPINFO`). Relevé
au runtime : sur un démarrage complet jusqu'au menu, le slot `[2]` reçoit **zéro appel**, et le slot
`[3]` — noté « rôle non établi » dans la table de la classe — en reçoit **un par objet texture
créé**.

> **Correction du même jour, plus tard.** Une première rédaction de ce paragraphe concluait de là
> que `[2]` « porte sur un chemin que le jeu n'emprunte pas ». C'est trop fort : le relevé ne
> couvrait que le menu principal. **La page d'options emprunte bien `[2]`** — `FUN_0050fab0`
> rastérise ses quatre atlas de libellés en 256x256 8 bits à partir de `FONT.PIC` et des fichiers
> texte du CD, puis les charge par `vtable[2](BITMAPINFO*, pixels*)` avant de poser une couleur-clé
> par `vtable[6]`. Les deux chemins coexistent : `[3]` pour la géométrie du jeu, `[2]` pour
> l'interface de configuration. Les deux sont désormais implémentés côté device synthétique.
> Leçon générale, la même que pour `+0xDC` : « zéro appel mesuré » veut dire *dans la phase
> mesurée*, jamais *jamais*.

La lecture de `winsys3d.c` le confirme et l'explique. Deux fabriques de textures y coexistent :

| fonction | pixels produits | slot appelé |
|---|---|---|
| `FUN_00a03460` | DIB palettisé 4/8/16 bpp + palette CLUT | `[2]`, 2 arguments — **jamais atteint** |
| `FUN_00a03980` (`winsys3d.c:3064`) | texels **16 bits**, jointifs, de haut en bas | `[3]`, 4 arguments |

```c
piVar7  = device->vtable[0x10]();                    /* création de l'objet texture   */
puVar2  = *piVar7;                                   /* sa vtable                     */
iVar10  = puVar2[3](largeur, hauteur, texels, &ddpf); /* ← le chargeur réel           */
if (piVar7 != 0 && DAT_00b2d07c < 0x10)
    puVar2[6](&couleurCle);                          /* seulement en deçà de 16 bits  */
if (iVar10 < 0) { puVar2[0](1); piVar7 = 0; }        /* échec : destruction           */
```

Le quatrième argument est un `DDPIXELFORMAT` complet monté sur la pile (`dwSize = 0x20`,
`dwFlags = 0x41` = `DDPF_RGB | DDPF_ALPHAPIXELS`, `dwRGBBitCount = 0x10`, puis les quatre masques).
Le jeu en emploie **deux**, choisis d'après la table de traduction employée :

| | R | G | B | A |
|---|---|---|---|---|
| ARGB1555 | `0x7C00` | `0x03E0` | `0x001F` | `0x8000` |
| ARGB4444 | `0x0F00` | `0x00F0` | `0x000F` | `0xF000` |

Mesuré en phase menu : ARGB1555, textures de 256x256, 256x32 et 128x128, avec une fraction notable
de texels à alpha nul (4264 sur 8192, 3779 sur 16384) — **c'est le bit d'alpha qui porte la
transparence**, pas la couleur-clé, dont l'appel est justement sauté à 16 bits par texel.

Conséquence pratique : il n'y a **ni palette ni DIB à décoder** pour porter les textures. La
conversion se réduit à développer chaque canal de 16 bits vers 8 d'après les masques fournis.

### Mise à jour du 2026-08-14 — la route B n'a plus rien à accrocher : le point est déjà à nous

Depuis le device synthétique (`SyntheticDevice.cs`, change `render-title-screen-in-window`),
`3dhelp` n'est plus chargé du tout. **C'est nous qui fabriquons la classe texture** : le slot device
`+0x10` est `Synth_CreateTexture`, qui alloue les 32 octets et pose une vtable de 8 stubs neutres
(`SyntheticDevice.cs:497`). Il n'y a donc plus de vtable à échanger — le chargeur `[2]` **reçoit
déjà** le `BITMAPINFO` et le pointeur de pixels du jeu, et les jette (`Synth_2`, `return 0`).

Trois faits mesurés le 2026-08-14, qui bornent la tâche 3.5 :

- **Un seul site lie une vraie texture.** Sur les 18 appels à `+0x64` de `winsys3d.c`
  (écrits `+ 100` en décimal, d'où l'échec d'un `grep 0x64`), **17 passent `0`** — déliaison. Le
  seul qui passe un objet est `FUN_00a27430` (`winsys3d.c:25471`), qui lit `entry[0]` d'une entrée
  de cache. La même entrée porte en `[6]..[9]` les tables de conversion UV et en `[10]` un tampon
  d'image : c'est un cache **LRU**, donc les objets texture sont détruits et refabriqués — le
  slot `[0]` (destructeur) est l'endroit où libérer la ressource D3D, sinon fuite.
- **Les `uv` qui parviennent au backend sont déjà normalisés `0..1`.** Relevé
  `OPENFORESIA_DUMP_GEOMETRY=1` : `uv=(0,002..0,998)`, `(0,438..0,368)`, `(0,688..0,472)`. Aucune
  conversion de coordonnées de texture n'est à écrire côté port.
- **Les pixels viennent de notre propre arène.** `FUN_00a03460` lit
  `puVar7 * 0x800 + offset + EXT_00b6dbd0`, c'est-à-dire la VRAM PSX émulée que la reconstruction
  remplit déjà avec les TIM du CD. Trois formats à convertir, pas un : `biBitCount` vaut **4, 8 ou
  16**, `biHeight > 0` (DIB **bottom-up**, à retourner), palette de 256 `RGBQUAD` en ligne montée
  depuis la CLUT via `DAT_00b2d080`, et la transparence arrive séparément — index de couleur-clé
  par `texture->vtable[6]` en 4/8 bpp (seulement si `DAT_00b2d07c < 0x10`), bit `0x8000` du
  `DDPIXELFORMAT` en 16 bpp (`FUN_00a03980`).

---

## L'écran-titre et l'attract — un chemin GDI, mais **pas** par la vtable device

> **Correction du 2026-08-12, par la mesure.** Une première rédaction de cette section, le même
> jour, affirmait que l'écran-titre dessinait par les slots `+0xC4` / `+0xCC` / `+0xD0` de la vtable
> device, et qu'il suffisait de les intercepter. **C'est faux.** Les compteurs posés sur ces trois
> slots (tâche 1.2 de `render-title-screen-in-window`) donnent **zéro appel** sur un démarrage
> complet — attract compris — jusqu'à 361 images de menu. Ces slots existent, `winsys3d` les
> référence, mais aucune des phases observées ne les emprunte.

Ces trois slots restent décrits ici pour ce qu'ils sont, sans plus leur prêter de rôle :

| slot | cible | corps décodé | appels mesurés |
|---|---|---|---|
| `+0xC4` | `0x0FFE1D40` | `this->+0x54 ->vtable[0x44]` — `GetDC(HDC*)`, 1 arg | **0** |
| `+0xCC` | `0x0FFE1D80` | `this->+0x40 ->vtable[0x44]` — `GetDC(HDC*)`, 1 arg | **0** |
| `+0xD0` | `0x0FFE1DA0` | `this->+0x40 ->vtable[0x68]` — `ReleaseDC(HDC)`, 1 arg | **0** |

**Où l'écran-titre dessine réellement** : dans la reconstruction elle-même, à
`OpenForesia/src/Blaze/Blaze.cs`, méthode `BlitTitleScreen` (Ghidra `Blaze.c:89916-89929`). Elle
prend la surface primaire du wrapper DirectDraw que le jeu construit lui-même
(`FUN_00512a10`), appelle `GetDC` (surface `+0x44`), `StretchDIBits` du bitmap `TITLE640.BMP` en
**640x480** (`0x280 × 0x1e0`, constantes de l'original), puis `ReleaseDC` (surface `+0x68`) — avec
une reprise sur `DDERR_SURFACELOST` via `Restore` (`+0x6C`).

C'est donc du code C# que le projet contrôle directement. Porter l'écran-titre dans la fenêtre ne
demande **aucune interception de vtable**.

### Le changement de mode d'affichage — mécanisme, et trois façons de ne pas l'éviter

Établi le 2026-08-12. `3dhelp` prend l'affichage en `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE`
(`FUN_0ffe35e0`, niveau `0x11`) puis appelle `IDirectDraw2::SetDisplayMode` (`FUN_0ffe3620`,
`3dhelp.c:1276`, corps à `0x0FFE3680`). Mesuré depuis un processus séparé à 30 ms : le bureau
bascule réellement — sur cette machine, **2560x1440 vers 1440x900 pendant 200 à 600 ms**, puis
retour. Windows repositionne les fenêtres à chaque bascule. C'est le clignotement perçu entre
l'écran-titre et le menu.

Tout cela se produit dans `Create3DDevice`, **à l'intérieur d'`Ordinal_789`**, donc avant qu'une
façade de vtable puisse être posée.

**Ce qui a été tenté, et ce que chaque échec apprend :**

| tentative | clignotement | init `3dhelp` |
|---|---|---|
| détourner `DirectDrawCreate` (import statique de `3dhelp`) puis échanger la vtable des objets `IDirectDraw` | supprimé | **faute d'accès** `0xC0000005` dans `Ordinal_789`, avant même l'appel à `SetDisplayMode` |
| supprimer `ChangeDisplaySettingsExA` (import statique de `ddraw.dll` vers `USER32.dll`) | supprimé | `Ordinal_789` ne rend jamais la main |
| rediriger le même appel vers le mode courant, pour garder `ddraw` et Windows d'accord | supprimé | idem |

Deux enseignements durables :

- **`ddraw` ne supporte pas qu'on remplace le pointeur de vtable de ses objets**, contrairement à
  ceux de `3dhelp` que `DeviceShim` échange sans difficulté. Réduire l'intrusion à un seul slot sur
  une seule interface n'y change rien.
- **`Create3DDevice` exige une vraie bascule en mode exclusif.** Trois approches indépendantes
  aboutissent au même constat : on ne peut pas avoir simultanément « pas de changement de mode » et
  « `3dhelp` initialisé ».

Détail utile au passage : le journal montre que `ddraw` demande précisément
**1440x900x32 avec `CDS_FULLSCREEN`**, suivi d'une restauration — d'où deux clignotements.

**La seule voie restante est de ne plus faire initialiser `3dhelp` du tout.** `winsys3d` le charge
**dynamiquement** (`winsys3d.c:5854-5855`, `LoadLibraryA` puis `GetProcAddress("Create3DDevice")`),
et non par table d'imports : détourner `GetProcAddress` permet de rendre un device à nous. La
séquence d'initialisation qu'il devra encaisser est `+0x18` (crée la fenêtre `Example`), `+0x0C`,
`+0x2C`, puis `+0x30(0, largeur, …)` — **`+0x30` étant la prise d'affichage**, et si elle rend zéro
`winsys3d` affiche « 3D Graphics initialisation error » et appelle `ExitProcess(3)`.

Ampleur réelle : `FUN_0ffe1990` (slots `+0x68` / `+0x6C`) commence par `mov eax,[ecx+0x5C]`, l'objet
`IDirect3DDevice2` que `+0x30` crée ; tous les slots d'état font de même. Neutraliser `+0x30` sans
porter les autres les fait déréférencer un pointeur nul. **Le device synthétique est donc le portage
des slots**, pas un correctif ponctuel.

Contrainte à ne pas oublier : chaque slot remplacé doit nettoyer exactement autant d'octets que
l'original, `stdcall` étant à nettoyage par l'appelé. Les comptes se lisent dans le `ret N` du code
de `3dhelp` — `+0x30` fait `ret 12` (trois arguments), `+0x2C` fait `ret 0`. **Ne pas se fier à
Ghidra** : il annonçait deux arguments pour `+0x30`.

### Table des arités — indispensable au device synthétique

Extraite du binaire le 2026-08-12 : pour chaque slot, le nombre d'octets que la fonction retire de
la pile (`ret N`), donc `N / 4` arguments après le `this` passé en `ECX`. **Tout slot remplacé doit
nettoyer exactement autant**, `stdcall` étant à nettoyage par l'appelé ; une erreur d'un seul slot
corrompt la pile.

Méthode : parcours de la vtable `0x0FFE6000`, puis recherche dans chaque fonction du premier
`C2 imm16` ou `C3` suivi de remplissage (`0x90`/`0xCC`), ce qui écarte les faux positifs à
l'intérieur d'instructions.

| slot | ret | slot | ret | slot | ret | slot | ret |
|---|---|---|---|---|---|---|---|
| `+0x00` | 4 | `+0x40` | 0 | `+0x80` | 4 | `+0xC0` | 28 |
| `+0x04` | 4 | `+0x44` | 0 | `+0x84` | 4 | `+0xC4` | 4 |
| `+0x08` | 4 | `+0x48` | 0 | `+0x88` | 8 | `+0xC8` | 4 |
| `+0x0C` | 4 | `+0x4C` | 0 | `+0x8C` | 4 | `+0xCC` | 4 |
| `+0x10` | 0 | `+0x50` | **16** | `+0x90` | 4 | `+0xD0` | 4 |
| `+0x14` | 0 | `+0x54` | 4 | `+0x94` | 4 | `+0xD4` | 4 |
| `+0x18` | 4 | `+0x58` | 4 | `+0x98` | 4 | `+0xD8` | 8 |
| `+0x1C` | 0 | `+0x5C` | **8** | `+0x9C` | 4 | `+0xDC` | 8 |
| `+0x20` | 0 | `+0x60` | 8 | `+0xA0` | 4 | `+0xE0` | 0 |
| `+0x24` | 0 | `+0x64` | **4** | `+0xA4` | 4 | `+0xE4` | 0 |
| `+0x28` | 0 | `+0x68` | 4 | `+0xA8` | 8 | `+0xE8` | 8 |
| `+0x2C` | **8** | `+0x6C` | 4 | `+0xAC` | 8 | `+0xF0` | 4 |
| `+0x30` | **12** | `+0x70` | 4 | `+0xB0` | **0** | `+0xF4` | 4 |
| `+0x34` | 0 | `+0x74` | 4 | `+0xB4` | **0** | `+0xF8` | 0 |
| `+0x38` | 4 | `+0x78` | 4 | `+0xB8` | 0 | `+0xFC` | 0 |
| `+0x3C` | 0 | `+0x7C` | 4 | `+0xBC` | **20** | | |

Les valeurs en gras sont celles des slots que `DeviceShim` remplace déjà, et **elles concordent
toutes** avec les signatures qu'il déclare — validation croisée de la table et du code existant.

Deux pièges confirmés :

- **Ne pas se fier à Ghidra pour l'arité.** Il annonce deux arguments pour `+0x30`, qui fait
  `ret 12`, soit trois.
- Une lecture manuelle en fin de fonction est trompeuse : le `ret 0` visible à `0x0FFE20E5` n'est
  pas la fin de `+0x2C` (`0x0FFE1E40`, 640 octets, donc jusqu'à `0x0FFE20C0`) mais celle de `+0x1C`,
  la fonction suivante. `+0x2C` fait bien `ret 8`.

### Correction — `+0xD8` n'est pas `GetSurfaceDesc`

Ce document annonçait « `surface[0x0D]->GetSurfaceDesc`, 2 arguments ». Deux faits le contredisent :

- le corps décodé (`0x0FFE1DE0`) appelle `this->+0x34 ->vtable[0x58]` avec **deux** arguments
  (`ret 8`), alors que `GetSurfaceDesc` n'en prend qu'un ;
- `winsys3d.c:5070` l'appelle `(1, 0)` sous une garde `VSyncDelayed`.

Mesure : **361 appels pour 361 `BeginScene`** — exactement un par image — toujours `(a=1, b=0)`,
toujours `S_OK`. C'est une **attente de retour de balayage vertical**, et l'objet en `+0x34` est
donc l'objet `IDirectDraw` et non une surface : `WaitForVerticalBlank` est à `+0x58` de
`IDirectDraw`, avec la signature `(dwFlags, hEvent)` et `DDWAITVB_BLOCKBEGIN = 1`.

Ce qui remet en cause l'entrée voisine : `+0xDC`, décrit ici comme « Flip », appelle
`this->+0x34 ->vtable[0x2C]` sur **le même objet**, avec deux arguments. Sur `IDirectDraw`, `+0x2C`
est `GetCaps(halCaps, helCaps)`. Cela expliquerait enfin pourquoi ce slot est mesuré à **zéro appel**
sur 121 puis 361 images : ce n'a jamais été une fonction de présentation. À confirmer — l'objet en
`+0x34` mériterait d'être identifié par sa vtable à l'exécution plutôt que par déduction.

---

## Conclusion pour l'étape 2

Le périmètre minimal d'un device de remplacement :

| | méthodes |
|---|---|
| chemin par frame | `+0x5C` Clear(TARGET), `+0x60` Clear(ZBUFFER), `+0xB0` BeginScene, `+0x64` liaison de texture, `+0x8C` SHADEMODE, `+0xBC` DrawPrimitive, `+0xB4` EndScene, `+0xDC` Flip |

### Ce qui est repris par le backend Direct3D 11

Depuis `replace-3d-backend-with-d3d11`, `3dhelp` n'est plus sur le chemin d'affichage. La façade
`OpenForesia/src/Blaze/DeviceShim.cs` remplace les slots suivants dans une copie de la vtable :

| slot | rôle | état |
|---|---|---|
| `+0x50` | pose du viewport | repris — seules les dimensions sont retenues ; le décalage n'a plus d'objet, la présentation centre elle-même |
| `+0x5C` | effacement de la cible | repris |
| `+0x60` | effacement du tampon Z | repris (jamais appelé en phase menu) |
| `+0x64` | liaison de texture | **portée le 2026-08-14** (tâche 3.5) — le pointeur d'objet sert de clé au registre de textures du backend, alimenté par le slot `[3]` de la classe texture. Rendu mesuré identique à `3dhelp` : écart moyen 0,2/255 par canal |
| `+0x94` | `TEXTUREMAPBLEND` | portée — relevé : le jeu pose **4 (MODULATEALPHA)** |
| `+0x98`, `+0xA0`, `+0xA4` | `ALPHABLENDENABLE`, `SRCBLEND`, `DESTBLEND` | portées — sans elles les quads à alpha partiel sortaient opaques, donc deux fois trop clairs |
| `+0x8C` | mode d'ombrage | repris — plat reproduit en recopiant la couleur du premier sommet |
| `+0xB0` | début de scène | repris ; c'est aussi là que le backend est créé, au premier appel |
| `+0xB4` | fin de scène | repris ; **c'est ici que se fait la présentation** |
| `+0xBC` | `DrawPrimitive` | repris — bandes et éventails dépliés en listes |
| `+0xDC` | « Flip » | inerte (jamais appelé, voir ci-dessous) |

Tous les autres slots continuent d'aller à `3dhelp`. Son initialisation reste nécessaire — c'est
elle qui crée l'objet dont `winsys3d` appelle la vtable — mais son emprise sur l'affichage est
défaite juste après : sa fenêtre (classe `Example`) est masquée et le mode d'affichage restauré.

`OPENFORESIA_LEGACY_3DHELP=1` rétablit le pass-through intégral.

### Correction du 2026-08-10 — mesure au runtime, phase menu

Cette cartographie a été établie par lecture statique des sites d'appel. Comptage réel sur 121
images du menu (compteurs par slot, façade `DeviceShim`) :

| slot | appels |
|---|---|
| `+0xB0` BeginScene | 121 |
| `+0xB4` EndScene | 120 |
| `+0xBC` DrawPrimitive | 12 655 |
| `+0x8C` SHADEMODE | 120 |
| `+0x5C` Clear(TARGET) | 122 |
| `+0x64` liaison de texture | 10 870 *(dont ~10 000 avant la première image — phase attract)* |
| `+0x60` Clear(ZBUFFER) | **0** |
| `+0xDC` Flip | **0** |

Deux écarts avec la lecture statique :

1. **`+0xDC` n'est jamais appelé.** La présentation ne passe pas par ce slot dans la phase menu ;
   elle emprunte vraisemblablement les `Blt` de surface de `+0x30` / `+0xE8`. La frontière d'image
   exploitable est donc `EndScene`, dont le compte suit exactement celui de `BeginScene`.
2. **`+0x60` n'est jamais appelé** en 2D : le menu ne se sert pas du tampon de profondeur.

Les sites d'appel existent bel et bien dans le code — ils ne sont simplement pas empruntés par
cette phase. La cartographie statique n'est pas fausse, elle est incomplète sur la question « quels
slots sont réellement parcourus, et quand ».
| états de rendu | ~10 passe-plats d'une ligne |
| accesseurs | 22, déjà cartographiés et triviaux |
| ponctuelles | 4 grosses fonctions, 1 appel chacune, décompilées et lisibles |

**Huit méthodes portent l'affichage.** Le reste est mécanique ou rare.

Trois faits rendent l'étape 2 abordable, tous établis par la mesure :

1. l'objet est un **pur porteur de vtable** — rien à reproduire de sa disposition mémoire ;
2. les sommets sont des **`TLVERTEX`**, déjà transformés — pas de pipeline géométrique à écrire ;
3. les quatre grosses fonctions ne sont **pas** sur le chemin chaud (un appel chacune), et leur
   pseudocode est désormais disponible dans `Legacy/Raw/3dhelp/3dhelp-vtable.c`.
