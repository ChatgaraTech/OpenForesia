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
| `+0xD8` | `0x0FFE1DE0` | 32 | `surface[0x0D]->GetSurfaceDesc`, 2 arguments |

### Accesseurs et cycle de vie

| slot | cible | taille | rôle |
|---|---|---|---|
| `+0x0C` | `0x0FFE1840` | 48 | `Initialize(arg)` — remet `+0xA0`/`+0xB4`/`+0xB8` à zéro, stocke `arg` en `+0x1C` |
| `+0x10` | `0x0FFE1970` | 32 | alloue `0x20` octets et construit (décompilé) |
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
| `+0x64` | **liaison de texture** — `SetLightState(1 MATERIAL)` puis `SetRenderState(1 TEXTUREHANDLE)` | **18** |
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
| `+0x64` | liaison de texture | **enregistrée mais neutre** — les pixels vivent dans une surface DirectDraw dont le chemin de création n'est pas identifié |
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
