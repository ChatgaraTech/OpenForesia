# Analyse — écran noir sur le menu principal

> ## RÉSOLU le 2026-08-10
>
> **Cause** : les quatre structures d'environnement de rendu sont **contiguës** dans le binaire, et
> cette contiguïté est un contrat :
>
> ```
> 0x005e9880  DrawEnv0  92 octets (0x5C)
> 0x005e98dc  DispEnv0  20        <- DrawEnv0 + 0x5C
> 0x005e98f0  DrawEnv1  92        <- DrawEnv0 + 0x70
> 0x005e994c  DispEnv1  20        <- DrawEnv1 + 0x5C
> ```
>
> `FrameEnd` publie l'environnement d'affichage par `Ordinal_657(nextDrawEnv + 0x5c)` : il *dépend*
> du fait que le DISPENV suive immédiatement son DRAWENV. La reconstruction les allouait en
> **quatre blocs de tas séparés**, donc cet accès lisait 20 octets de mémoire quelconque au lieu du
> DISPENV.
>
> **Mécanisme de l'intermittence.** Côté `winsys3d`, l'ordinal 657 (`FUN_00a05a00`,
> `winsys3d.c:5041`) branche sur un champ de cette structure :
>
> ```c
> if (*(short *)(param_1 + 1) < 0x141) {      // largeur <= 320
>     SetAbsShift(0x140 - DAT_011be6bc >> 1, 0xf0 - _DAT_011be6be >> 1);
> } else {
>     DAT_011be770 = 0; DAT_011be774 = 0;     // remise à zéro du décalage
> }
> ```
>
> Selon ce que le tas contenait, la branche prise variait. Quand la première branche partait, elle
> posait un décalage absolu calculé sur des ordures, et `SetShift` (`winsys3d.c:4934`) le composait
> dans la translation appliquée à **tous** les sommets — qui sortaient de l'écran.
>
> **Preuve.** Relevé des globales au premier dessin :
>
> ```
> NOIR     abs=(1453,7725)   shift=(-160,-120) size=21  -> composé=(4391,23199)
> NOIR     abs=(3380,10490)  shift=(-160,-120) size=21  -> composé=(10172,31494)
> CONTENU  abs=(0,0)         shift=(0,0)       size=0   -> composé=(0,0)
> ```
>
> Et les coordonnées de la même primitive (mêmes uv, même couleur) :
> `NOIR x=9946 y=10368` contre `CONTENU x=80 y=136`.
>
> **Correction** : allouer les quatre structures dans un unique bloc contigu de 224 octets, aux
> offsets 0x00 / 0x5C / 0x70 / 0xCC.
>
> **Vérification** : 10 lancements sur 10 affichent le menu, contre 3 sur 10 avant. Mesure
> automatisée et reproductible (capture de la zone cliente, comptage des pixels non noirs).
>
> **Classe de bug** : la même que celles déjà rencontrées dans ce projet — un symbole `DAT_` qui
> désigne un point *intérieur* d'une structure plus grande, et dont la reconstruction fait à tort
> une entité indépendante. C'est la quatrième occurrence.
>
> **Ce que l'hypothèse initiale avait de faux.** Tout ce document, plus bas, poursuit l'affichage
> exclusif DirectDraw et le changement de mode. Cette piste était erronée : le bug a survécu au
> remplacement complet du backend par Direct3D 11 en mode fenêtré, avec exactement le même taux.
> Le remplacement reste utile pour d'autres raisons (voir `openspec/changes/replace-3d-backend-with-d3d11`),
> mais il n'a pas corrigé ceci — et c'est justement parce qu'il a rendu le bug *observable* que la
> cause a pu être isolée.

> **Mesure du 2026-08-10, à lire avant le reste.** Le mode d'affichage réellement appliqué a enfin
> été mesuré (`DeviceShim.LogDisplayMode`, appelé avant et après l'init 3D) :
>
> ```
> bureau           : 2560x1440 32bpp 144Hz
> après init 3D    : 1440x900  32bpp 144Hz      <- constant, quelle que soit Resolution
> ```
>
> Balayage de la clé `Resolution` du `.cfg`, un lancement par valeur :
>
> | `Resolution` | mode obtenu | viewport demandé |
> |---|---|---|
> | 0 | 1440x900 | (0,0) 320x240 |
> | 1 | 1440x900 | (0,0) 512x384 |
> | 2 | 1440x900 | (0,0) 640x480 |
> | 3 | 1440x900 | (0,0) 800x600 |
> | 4 | 1440x900 | (32,24) 960x720 *(Size=21 ; 1024x768 à Size=23)* |
> | 5 | 1440x900 | (176,133) 288x216 — **hors table**, valeurs parasites |
>
> Deux conclusions :
>
> 1. `Resolution` ne choisit **pas** le mode d'affichage, contrairement à ce qui était supposé plus
>    bas dans ce document : elle indexe la table de décalages de base de `winsys3d`
>    (`DAT_00a2c49c`) et ne fixe que la taille du rendu. Le mode, lui, est négocié par `3dhelp`
>    (`SetDisplayMode`, `3dhelp.c:1275`) et vaut 1440x900 dans tous les cas.
> 2. **Le jeu rend donc au maximum 1024x768 dans un mode 1440x900.** L'image occupe le coin haut
>    gauche avec du remplissage autour — exactement le symptôme observé. Aucun mode 16:10 de ce
>    type n'existait sur le matériel de 1998 : la négociation de mode ne survit pas au matériel
>    moderne, et c'est elle qu'il faut supprimer, pas replacer le viewport.
>
> Effet de bord utile : `Resolution=5` produit des valeurs parasites, ce qui confirme que la table
> de base de `winsys3d` compte exactement 5 entrées valides (0..4). Comme la table de bridage
> `DAT_00562e00` est indexée par la même valeur, son 6ᵉ dword (`0`) est bien un terminateur.
>
> Le taux d'écran noir, lui, **n'a pas bougé** après la correction de la clé `Size` : ce bug était
> réel mais orthogonal.

Document de synthèse à destination du responsable du projet. Il rassemble le symptôme, tout ce qui
a été mesuré, les hypothèses écartées et pourquoi, afin qu'aucune piste déjà close ne soit
rouverte.

**État au moment de la rédaction** : le menu principal s'affiche fidèlement à l'original lorsqu'il
s'affiche, mais **environ un lancement sur trois donne un écran entièrement noir**. La cause n'a
pas été trouvée, et toutes les pistes accessibles depuis la reconstruction ont été épuisées.

---

## 1. Attention : il y a eu **deux** écrans noirs distincts

C'est la première chose à ne pas confondre, parce que le symptôme portait le même nom.

| | écran noir n°1 | écran noir n°2 |
|---|---|---|
| **comportement** | systématique, à chaque lancement | intermittent, ~1 lancement sur 3 |
| **cause** | trouvée et corrigée | **non trouvée** |
| **statut** | clos | **ouvert** |

### Écran noir n°1 — résolu

Le descripteur de panneau `DAT_0056dd58` est **statiquement initialisé dans la `.data`** et la
reconstruction l'alimentait avec des zéros. Chaque test de fonctionnalité de `DrawUiPanel` étant de
la forme `champ & 0x1000000`, un descripteur nul ne dessinait strictement rien.

Corrigé en récupérant les 35 octets non nuls depuis `Legacy/Blaze.exe`. Puis un second défaut du
même ordre : `DAT_005e8fc0` n'est pas une seconde table de tpage/CLUT mais **`DAT_005e8f00`
décalée de 48 entrées** (`0x005e8fc0 - 0x005e8f00 = 0xC0`). Modélisées séparément, l'entrée `0x31`
que réclame le panneau revenait à zéro et toutes les primitives échantillonnaient la page de
texture 0 — une VRAM vide. Mesuré : `slot[0x31]` passe de `0x00000000` à `0x73C0021B`.

**Le reste de ce document ne concerne que l'écran noir n°2.**

---

## 2. Le symptôme

- L'écran-titre s'affiche normalement (chemin DirectDraw + `StretchDIBits`).
- Après appui sur une touche, l'écran devient **entièrement noir** et le reste.
- Aucun plantage, aucune exception, aucun message. Le processus tourne normalement.
- Environ **un lancement sur trois** est affecté ; relancer suffit.
- Observation décisive du responsable : lors d'un essai, **le menu clignotait** au lieu de rester
  noir.

---

## 3. Ce que la mesure établit

Toutes ces valeurs ont été obtenues par instrumentation temporaire du programme, retirée depuis.

### 3.1 L'état du menu est rigoureusement identique, écran visible ou noir

Six lancements instrumentés, journal pris sur les trois premières frames de menu :

```
texte=20 mots  panneau=114 mots  flags=0x05  panelOT=0x…0828  slot31=0x73C0021B  fade=8
```

**La liste d'affichage est construite à l'identique à chaque fois.** Le nombre de mots émis par le
moteur de texte et par le renderer de panneau ne varie pas d'un run à l'autre.

### 3.2 `DrawOTag` s'exécute réellement, sur presque toutes les frames

`Ordinal_701` (`DrawOTag`) ne dessine que si **deux gardes** sont à zéro :

```c
if (DAT_00dedc44 == 0) {
   ...
   if (VSyncDelayed == 0) {
      ... tout le dessin ...
   }
}
```

Mesuré sur 7 lancements, 60 frames chacun :

| grandeur | résultat |
|---|---|
| frames où `VSyncDelayed == 0` (donc dessinées) | **59 à 60 sur 60** |
| frames bloquées par `DAT_00dedc44` | **0** |
| timer multimédia `DAT_011be6d0` (`timeSetEvent`) | actif, valeurs ~3800 |

`VSyncDelayed` vaut 1 sur la toute première frame puis retombe à 0 et y reste. Le timer qui la
remet à zéro tourne bien.

### 3.3 Les uploads VRAM des glyphes n'écrasent rien

Cinq uploads, un par ligne de texte :

```
rect=(x=768, y=288) taille=(64x16)
rect=(x=768, y=304) taille=(64x16)
rect=(x=768, y=320) taille=(64x16)
rect=(x=768, y=336) taille=(64x16)
rect=(x=768, y=352) taille=(64x16)
```

La VRAM PlayStation fait 1024×512 et les deux tampons d'image occupent `(0,0)-(320,480)`.
**Aucune collision** : la texture des glyphes n'est pas écrasée entre deux frames.

### 3.4 L'appariement des tampons est conforme

Vérifié contre Ghidra (`FUN_0052a280`) :

| env | dessine en | affiche |
|---|---|---|
| env0 (`DAT_005e9880`) | `y = 0` | `y = 240` |
| env1 (`DAT_005e98f0`) | `y = 240` | `y = 0` |

Du double buffering canonique : on dessine dans une moitié et on affiche l'autre.

---

## 4. Hypothèses testées et écartées

| hypothèse | comment testée | verdict |
|---|---|---|
| L'adresse de l'arène (variable à chaque lancement) | corrélation sur 8 runs | **écartée** — un run à l'adresse d'origine `0x00B6DBD8` était noir |
| La relance automatique de l'arène | corrélation sur 8 runs | **écartée** — sans lien |
| `RegisterDisplayModeRestore` (notre déviation) retient DirectDraw | lecture du code | **écartée** — n'ajoute aucune référence COM |
| La table de tpage/CLUT non remplie | mesure de `slot[0x31]` | **écartée** — vaut `0x73C0021B`, correct |
| La texture des glyphes écrasée par le tampon d'image | mesure des rectangles d'upload | **écartée** — hors zone |
| Les gardes de `DrawOTag` | lecture à chaud sur 7×60 frames | **écartée** — jamais bloquantes |
| Conflit entre les deux changements de mode vidéo | 3 variantes, 5 lancements chacune | **écartée** — voir ci-dessous |
| Basculer sur le renderer « software » | lecture de `Setup3DDevice` | **impasse** — voir ci-dessous |

### 4.1 Les trois variantes de mode vidéo

Hypothèse : DirectDraw prend le mode exclusif 640×480 pour l'écran-titre, puis le device 3D
s'initialise à la résolution de l'ini (1024×768). Le second échouerait par intermittence tant que
le premier tient le mode. À quoi s'ajoute une **déviation de notre fait** : `SetDisplayMode(640,
480, 16)` renvoie `E_NOTIMPL` sur Windows moderne et on se rabat sur **32 bpp**.

Trois variantes ont été construites, sélectionnables sans recompilation :

| variante | menus visibles sur 5 essais |
|---|---|
| 0 — comportement actuel | 2/5 |
| 1 — rendre le mode exclusif DirectDraw avant l'init 3D | 3/5 |
| 2 — ne jamais appeler `SetDisplayMode` | 1/5 |

**Indiscernables.** Aucune variante n'améliore quoi que ce soit. Hypothèse abandonnée, dispositif
de test supprimé.

### 4.2 L'impasse du choix de renderer

Le jeu d'origine propose deux renderers (« Direct3D Software Emulation » et « Pilote d'affichage
principal (Direct3D) »). Il était tentant de basculer sur le premier.

**Ça ne peut rien donner depuis notre code**, pour trois raisons établies par lecture :

1. `Setup3DDevice` (winsys3d.c ligne 389) se contente de **lire** `[D3DDriver] Driver` et
   `Description` dans l'ini, de les comparer à `"GLIDE"` et `"SOFTWARE"`, puis de les réécrire.
   **Aucun dialogue, aucune énumération de pilotes** dans ce chemin.
2. Le drapeau `SOFTWARE` (`DAT_011be73c`) ne sert qu'à **désactiver le filtrage de textures**
   (winsys3d.c ligne 5935). Il ne change pas le device.
3. Le device vient de `Create3DDevice()` dans `3dhelp.dll`, appelée **sans argument**.

Le sélecteur de renderer appartient au programme de configuration séparé du jeu, non reconstruit.

Pour mémoire, la sélection en vigueur : `DAT_011be738 = (strcmpi(driver, "GLIDE") == 0)` vaut 0,
donc `3dhelp.dll` (Direct3D), ce qui correspond au `Driver=display` du fichier de configuration.

---

## 5. Pièges de méthode rencontrés

À connaître avant de reprendre, sous peine de mesurer du bruit.

- **Une capture d'écran ne mesure pas ce bug de façon fiable.** En plein écran exclusif,
  `CopyFromScreen` ne voit pas la surface DirectDraw. Une mesure donnant exactement 150 pixels
  allumés à chaque run « visible » captait en réalité un élément du bureau, pas le menu.
- **L'épuisement des tentatives d'arène n'est pas un écran noir.** Quand les 3 tentatives de
  placement échouent, l'application sort proprement avec un message. C'est un symptôme différent.
- **Les processus enfants relancés survivent au parent.** Tuer le processus lancé ne suffit pas :
  les enfants restent, verrouillent l'exécutable et font échouer le build suivant. Toujours faire
  `Get-Process -Name OpenForesia | Stop-Process -Force`.
- **Le test peut être automatisé.** Inutile d'appuyer sur une touche manuellement :

  ```powershell
  $sh = New-Object -ComObject WScript.Shell
  $t = Get-Process -Name OpenForesia | Where-Object { $_.MainWindowHandle -ne 0 } | Select-Object -First 1
  $sh.AppActivate($t.Id); Start-Sleep -Milliseconds 700; $sh.SendKeys(" ")
  ```

  Viser le processus **qui possède une fenêtre** : après une relance d'arène, ce n'est pas celui
  qu'on a lancé.

---

## 6. Conclusion

Tout ce qui est mesurable depuis la reconstruction indique qu'elle **construit et soumet
correctement sa liste d'affichage, à chaque frame, de façon identique d'un lancement à l'autre**.

Le clignotement observé une fois oriente vers un problème de présentation ou de permutation de
tampons **en aval de `DrawOTag`**, donc à l'intérieur de `3dhelp.dll`, de Direct3D et du pilote
graphique — sur du matériel qui n'a plus rien de commun avec celui de 1998.

Aucune piste solide ne reste accessible depuis le code de la reconstruction.

---

## 7. Si le sujet est repris

Par ordre de coût croissant, et aucune de ces pistes n'est une simple vérification :

1. **Confirmer le caractère environnemental** en testant sur une autre machine ou un autre GPU. Si
   le taux d'échec change nettement, la cause est bien dans la pile graphique.
2. **Observer sous un débogueur graphique** (PIX, RenderDoc, ou le débogueur D3D historique) ce que
   fait le device au moment du clignotement.
3. **Instrumenter `Create3DDevice`** dans `3dhelp.dll` — hooker l'export ou reconstruire la
   création du device pour maîtriser le type de device et les surfaces. C'est un chantier, et il
   sort du cadre de la transcription fidèle tant qu'aucune autre voie n'a été épuisée.

Bonne nouvelle pour la troisième piste : **`3dhelp.c` est déjà dans le dépôt** et ne fait que
**3 622 lignes** — c'est de loin la plus petite des décompilations de DLL. C'est la source de
vérité à lire en premier si cette voie est ouverte, avant toute instrumentation.
