# TODO

Point d'entrée entre deux sessions. Ce fichier ne contient pas le plan — il dit où il est.

---

## Le plan est dans la change OpenSpec active

```
openspec/changes/replace-3d-backend-with-d3d11/
```

`tasks.md` fait foi : 27 tâches faites, 4 restantes, formulées précisément. `proposal.md` dit
pourquoi, `design.md` dit comment et liste les risques.

```powershell
openspec status --change "replace-3d-backend-with-d3d11" --json
```

Ne pas recopier ces tâches ici : deux listes divergent toujours.

---

## Où en est le rendu

Le jeu rend par un backend **Direct3D 11** (Silk.NET), dans une **fenêtre**, sans changement de mode
d'affichage. `3dhelp.dll` n'est plus sur le chemin d'affichage et n'est même plus initialisée : un
device synthétique répond à sa place (`SyntheticDevice.cs`).

**Le menu s'affiche texturé dans la fenêtre depuis le 2026-08-14** — tâche 3.5 faite : textures,
modulation par la couleur diffuse, mélange alpha et transparence par couleur-clé. Vérifié par
comparaison automatisée de deux captures du même binaire, l'une avec `OPENFORESIA_LEGACY_3DHELP=1` :
boîte englobante `290x148` **des deux côtés**, **écart moyen 0,2/255 par canal**, 0,1 % de pixels
franchement différents. Tenu sur 1021 images consécutives sans exception. Détail et hypothèses
corrigées dans `tasks.md` de la change et dans `Documentation/DLL/3dhelp.md`.

**Le fond du menu s'affiche depuis le 2026-08-14** — le parchemin, la plume, l'encrier. La cause
n'était pas dans le backend : `FUN_00542250` monte un descripteur de sprite de 20 octets d'un seul
tenant et en passe l'adresse aux deux émetteurs, alors que la reconstruction l'avait coupé en deux
blocs de tas. **Cinquième occurrence** de ce défaut ; détail et règle qui en découle dans
`REVERSE_STATUS.md`, section du 2026-08-14.

~~**tâche 4.5** — l'échelle~~ — **prémisse réfutée, et la réfutation tient une fois le fond présent.**
Le panneau du menu est bien plus petit, relativement au fond, que sur la capture officielle en
640x480 (30 % de la largeur contre 45 %) — mais `3dhelp`, sur le même build et à la même
`Resolution=4`, donne exactement les mêmes proportions. L'interface du jeu ne suit pas la
résolution alors que son fond la suit : c'est le comportement du jeu, pas un défaut de portage.
Y toucher serait une amélioration délibérée.

---

## La suite : interagir avec le menu

Le chemin est cartographié, et un blocage supposé n'en est pas un.

```
FUN_00504460  SetupJoystickHandlers        [FAIT — appelle la création]
  └─ FUN_00523120  création du device d'entrée              [FAIT le 2026-08-14]
       ├─ FUN_00522dc0  DirectInputCreateA(hInstance, 0x500) [FAIT]
       └─ FUN_00522eb0  init                                 [FAIT]
            ├─ FUN_005233c0  clavier : CreateDevice(GUID_SysKeyboard) + SetDataFormat
            │                + SetCooperativeLevel(hwnd, 6) + Acquire      [FAIT, acquis]
            ├─ FUN_00523470  souris  : même séquence, GUID_SysMouse        [FAIT]
            ├─ EnumDevices(DIDEVTYPE_JOYSTICK, ATTACHEDONLY)               [FAIT, compte]
            └─ FUN_00523230  création d'une manette par stick   [GHIDRA INEXPLOITABLE —
                                                                 à lire en désassemblé]
FUN_005048f0  PollJoystickState            [FAIT le 2026-08-14]
  ├─ FUN_005231f0 / FUN_005230b0  lecture de tous les devices acquis  [FAIT]
  ├─ FUN_00523410 / FUN_005234c0  GetDeviceState + reprise d'Acquire  [FAIT]
  ├─ FUN_00523660  clavier -> état pad, via la table [Controls]       [FAIT]
  └─ la boucle de repliage de bits sur deux emplacements              [FAIT]
FUN_00504a30  UpdateButtonEdges            [FAIT]
```

**Le menu se navigue.** Vérifié en injectant de vraies frappes : le curseur descend d'un item par
flèche et remonte, et la touche de validation quitte le menu.

**`FUN_0052a440` est reconstruite** — ce n'est pas le « répartiteur d'états de jeu » que ce fichier
annonçait, mais **l'écran de chargement inter-états** : il charge une entrée de ressource, la
décode, et l'affiche 150 images, passable avec START ou CARRÉ (au relâchement). Il tourne ; le
blocage est désormais **l'état de jeu 1** (`thunk_FUN_00536180`).

**La page d'options : couche de dessin et mise en place reconstruites.** Le dessin
(`FUN_00510110`, `FUN_00510330`, `FUN_00510500`, `FUN_005108f0`, `FUN_00510740`, `FUN_00510a70`)
rend à travers notre backend, et la mise en place (`FUN_0050fab0` et sa chaîne : `FUN_0050f7d0`,
`FUN_0050f9b0`, `FUN_0050f680`, `FUN_0050f770`, `FUN_0050f560`) rastérise en logiciel les libellés
depuis `DATA/FONT.PIC` et les quatre fichiers texte du CD. Le chemin de texture par DIB (slots
`[2]` et `[6]`) est posé aussi, puisque c'est cette page qui l'emprunte.

**Non vérifié au runtime**, et c'est le point important ci-dessous : la page ne s'atteint pas.
Reste `FUN_0050fe90`, `FUN_005101c0`, `FUN_00510aa0` et la boucle `FUN_00512320`.

## L'index du curseur du menu — **trouvé**

C'est `menuContext[0x8c4]`, soit l'adresse **0x0056e61c**, trois octets avant l'octet de drapeaux
déjà identifié. Trouvé en comparant tout le descripteur d'une image à l'autre
(`OPENFORESIA_TRACE_MENU=1`) : un seul octet bouge, en cadence exacte avec les flèches.

Ghidra n'a **jamais nommé** cette adresse — rien n'y accède en absolu, c'est toujours
`descripteur + 0x8c4` — et il a donc attribué le `switch` à `DAT_005e909f`, un global sans rapport
qui n'est écrit qu'à zéro dans tout `Blaze.c`. Sixième occurrence dans ce projet d'un `DAT_` qui est
en réalité un champ de structure.

**Vérifié de bout en bout** : le curseur amené sur « Options », la validation ouvre bien Options et
non l'entrée 0. Le menu se navigue pour de vrai.

## La mise en place de la page d'options s'exécute de bout en bout

Il a fallu lever deux défauts distincts.

**1. `LoadArray` (wsyskern) corrompt le tas, et est contourné.** La corruption est *cumulative avec
le nombre de lignes lues* — 1 ligne passe, 4 tuent plus loin, le fichier entier tue tout de suite —
et toujours à l'allocation suivante. Écarté par la mesure : l'appel est conforme (`RET 0x18`, donc
6 arguments stdcall, sens de chacun lu dans `FUN_10004060`), l'objet est valide (`CreateArray` pose
le tag « ARAY » que le validateur exige, et `LoadArray` sort proprement s'il manque — avec un
fichier absent **toute la mise en place va au bout**), ce n'est ni la capacité, ni la croissance,
ni le chemin. Comme le tableau ne sert ici qu'à « rendre la ligne *i* d'un fichier texte », la
reconstruction lit désormais les fichiers elle-même. `OPENFORESIA_WSYSKERN_ARRAYS=1` rétablit le
chemin d'origine pour qui voudra reprendre le diagnostic côté DLL.

**2. L'index de glyphe était signé.** Défaut de notre côté : Ghidra type le caractère en `char`, et
transcrit tel quel, « ô » (0xF4) arrivait à **-12**, donc index **-45**. Le pseudocode prouve
pourtant que le paramètre est non signé — `FUN_0050f560` le compare à `0x80`, `0x8a`, `0x9a`,
`0xc0`, `0xe0`, valeurs qu'un `char` signé ne peut jamais prendre. Lu en non signé, 0xF4 donne le
glyphe 174, dans la grille 31x6 de la planche.

Vérifié : les quatre jeux de libellés se rastérisent, les six atlas et les deux textures `.PIC`
sont créés (256x256, 8 bits, bas-haut, par le slot `[2]` de la classe texture).

## La page d'options s'affiche et se navigue

`FUN_0050fe90`, `FUN_005101c0`, `FUN_00510aa0` et la boucle `FUN_00512320` sont reconstruites. À
l'écran : le fond, le panneau encadré, le titre « OPTIONS », les quatre entrées avec leurs accents,
et un curseur qui descend aux flèches (vérifié par différence d'images — le changement est confiné
à sa colonne).

**Les trois sous-menus fonctionnent aussi** — `FUN_005110b0` (Affichage), `FUN_005113d0` (Son),
`FUN_00512220` (Contrôle), plus la jauge `FUN_00510a00` et la réapplication du gamma
`FUN_0050f980`. Vérifié à l'écran : « Volume musique » et « Volume effets » avec des jauges qui
bougent par pas de 8 et s'appliquent aussitôt ; « Dimensions de l'écran » et « Clarté » avec leurs
jauges, puis filtre, taille d'affichage et timing. Tout ce dont ils dépendaient était déjà
reconstruit.

**Trois des quatre feuilles sont faites** : `FUN_00510d40` (la page **Timing** — et non un
recadrage, comme son stub le supposait), `FUN_00510e60` (résolution) et `FUN_005119c0`
(affectation des manettes, vérifiée à l'écran : quatre joueurs, quatre lignes de configuration
grisées quand le joueur n'a pas de manette).

Deux réserves à connaître :

- **la page résolution ne peut rien faire** tant que le device synthétique ne porte pas ses deux
  slots : `+0x1c` (le masque des modes acceptés) et `+0xE8` (le changement effectif, que
  `ChangeResolution` pilote). Le masque revient nul, la liste est vide, seul le titre s'affiche ;
- **l'éditeur de touches est fait aussi.** `FUN_00511d40`, ses deux helpers et ses cinq tables de
  données (85 scancodes, 24 index d'action, 4 colonnes de flèche, 40 liaisons par défaut, et les
  49 descripteurs de ligne) sont reconstruits ; les défauts correspondent exactement aux clés
  `K0000`..`K0113` du BLAZE.cfg installé. Vérifié à l'écran : les huit actions globales avec leurs
  **deux** touches, l'en-tête « Joueur 1 » coloré, le défilement et le curseur.

  Deux points de structure : la « seconde touche » n'est pas une seconde table — `DAT_00564ba8` est
  `DAT_00564b58` décalé de vingt entrées, c'est-à-dire la **catégorie 1** de la même table, le
  second jeu clavier que `FUN_00523660` lit déjà ; et assigner une touche l'efface de toutes les
  liaisons clavier des quatre groupes, en laissant la catégorie manette intacte.

Reste **une** feuille, apparue en lisant la page manettes : `FUN_005115c0`, la configuration des
boutons d'un joueur, 168 lignes.

Trois défauts levés au passage, tous les trois instructifs :

- **la classe texture a plus de slots que ce fichier n'en listait** — la vtable réelle en porte au
  moins onze, et `FUN_0050fe90` appelle le slot **8** (`IDirectDrawSurface::IsLost`) une fois par
  texture et par image ; la classe synthétique en avait huit et serait sortie de sa propre vtable ;
- **`DAT_005d9de0` est `s_GammaIni`**, déjà reconstruit, redéclaré et jamais écrit — donc nul ;
- **`_DAT_0054d000` vaut `1/64`**, lu dans le binaire (`0x3C800000`), et non le `1.0f` qu'un TODO
  supposait. Ensemble, ces deux-là faisaient calculer `Gamma(valeur, 64.0)`, ce qui blanchit toutes
  les palettes : fond et cadre sortaient blancs donc invisibles, alors que les libellés, qui ne
  passent pas par `Gamma`, étaient corrects.

Septième et huitième globals reconstruits sous deux noms. La règle qui s'en dégage :
**avant de déclarer un champ `DAT_`, chercher son adresse dans le fichier** — la moitié y sont déjà
sous un autre nom, et l'ancien n'est pas toujours le mauvais.

Deux réserves sur `FUN_0052a440`, toutes deux dues à une frame que Ghidra n'a pas récupérée :
la source du second `MoveImage`, et le pointeur du descripteur de sprite — laissé nul, donc
**l'écran de chargement est vide pour l'instant**. Un listing désassemblé de `0x0052a440` referme
les deux ; il n'y a pas de désassembleur ici, il faut un export Ghidra comme ceux de
`Legacy/Raw/Blaze/`.

Trois choses à savoir avant de toucher à l'entrée :

- la convention des boutons est **active basse** — le mot part de `0xffff` et chaque touche
  enfoncée éteint son bit ; les 20 masques (`DAT_00564f18`, lus dans le binaire) sont ceux d'une
  manette PlayStation : `0x0010` haut, `0x0020` droite, `0x0040` bas, `0x0080` gauche, `0x0008`
  start ;
- `state[0x1c] |= state[0x9c]` est `DIK_RETURN |= DIK_NUMPADENTER` — l'Entrée du pavé numérique
  repliée sur l'autre ;
- le scancode `0x3e` est `DIK_F4` : maintenu, il coupe le clavier pour tous les joueurs.

**Le clavier passe par DirectInput** : le GUID lu à `0x0054db58` dans `Legacy/Blaze.exe` est
`{6F1D2B61-D5A0-11CF-BFC7-444553540000}` = `GUID_SysKeyboard`. Les touches sont déjà chargées depuis
le `[Controls]` du `.cfg` par `LoadKeyBindings` (table `DAT_00564b58`).

**`DAT_005db85c` (TODO #6) ne bloque plus — vérifié au runtime.** Ce handle jamais trouvé sert à
deux choses seulement : l'`hInstance` de `DirectInputCreateA` — dont le code traite explicitement le
cas nul par `GetModuleHandleA(NULL)`, et dont l'unique site d'appel passe justement 0 — et le `hwnd`
de `SetCooperativeLevel`. La fenêtre du backend Direct3D 11, qui n'existait pas quand ce TODO a été
déclaré bloqué, fournit ce second handle. Le clavier est acquis sans erreur.

Reste aussi stubbée sur ce chemin : `FUN_00512320`, la page d'options (80 lignes), atteignable
depuis l'entrée « Options » du menu.

**Une autre observation du même jour, non expliquée**, à ne pas confondre avec ce qui précède : sans
frappe clavier sur l'écran-titre, le processus **sort de lui-même après exactement 60 images de
menu**, sans exception ni entrée dans le journal d'événements. Avec une frappe (le jeu attend une
touche pour passer au menu), il tourne indéfiniment — 301 images et plus. Reproductible dans les
deux sens.

---

## Demande du responsable — **traitée le 2026-08-12**, un résiduel

La change `render-title-screen-in-window` est implémentée. Ce qu'on voit maintenant au lancement :
une seule fenêtre, l'écran-titre dedans, le bureau intact.

| | avant | après |
|---|---|---|
| plein écran exclusif 640x480 | ~9 s | supprimé |
| fenêtre `VfwClass` 2000x2000 `WS_EX_TOPMOST` | visible | jamais affichée |
| écran-titre | plein écran | dans la fenêtre du jeu |
| fenêtres visibles | 2 | **1** |
| rendu du menu | — | identique au bit près |

**Le clignotement est supprimé.** `3dhelp` n'est plus initialisé du tout : `winsys3d` le charge
dynamiquement, on détourne son `GetProcAddress` et on lui rend un **device synthétique**
(`OpenForesia/src/Blaze/SyntheticDevice.cs`). Mesuré : 500 ms de bascule de mode sur le chemin
d'origine, **0 ms** désormais, et le rendu du menu est identique au bit près — 0 pixel différent sur
172 800. Actif par défaut ; `OPENFORESIA_LEGACY_STARTUP=1` rétablit le chemin d'origine.

Ce que le device synthétique ne fait **pas** : porter les textures. Les objets qu'il rend existent
et répondent, mais leur handle vaut zéro, soit « aucune texture liée » — l'état que le backend
traite déjà en rendant les primitives en aplats blancs. Le portage réel reste la tâche 3.5 de
`replace-3d-backend-with-d3d11`.

## Demande d'origine (2026-08-10), conservée pour mémoire

> « J'aimerai qu'on ait qu'une fenêtre, point barre : plus d'affichage en plein écran du titre puis
> basculement sur la fenêtre. »

Constat exact. Aujourd'hui l'écran-titre s'affiche encore dans l'affichage exclusif de `3dhelp`, et
la fenêtre n'apparaît qu'ensuite. La raison est dans le code : la reprise de l'affichage
(`D3D11Window.ReleaseLegacyDisplay`) est appelée au moment où le backend est créé, c'est-à-dire au
**premier début de scène** — donc après le titre.

Ce que ça implique, à vérifier avant de chiffrer :

1. Découpler la reprise de l'affichage de la création du device. La reprise peut sans doute avoir
   lieu tôt ; la création du device, **non** — c'est mesuré, la faire pendant l'initialisation
   empêche le jeu d'atteindre sa boucle de rendu (voir le commentaire sur `EnsureBackend`).
2. L'écran-titre et l'attract ne passent pas par `DrawPrimitive` : ils passent par un
   `GetDC` / `StretchDIBits` sur une surface DirectDraw. Les afficher dans la fenêtre demande de
   porter ce chemin-là aussi, ce n'est pas un simple déplacement d'appel.

   **Corrigé le 2026-08-12** — ce portage est plus petit qu'estimé ici. Il passe par trois slots de
   la vtable device, `+0xC4` / `+0xCC` / `+0xD0`, que `DeviceShim` intercepte déjà, et il ne demande
   **pas** de lire une surface DirectDraw : rendre le HDC d'une DIB section à nous suffit. Voir
   `Documentation/DLL/3dhelp.md`, section « L'écran-titre et l'attract ». Ce sujet est donc
   **indépendant de la tâche 3.5**, contrairement à ce que cette page laissait entendre.

---

## Résolu, ne pas rouvrir

L'**écran noir intermittent** est corrigé (10 lancements sur 10). Cause : les structures
DRAWENV/DISPENV sont contiguës dans le binaire et `FrameEnd` lit le DISPENV à `DrawEnv + 0x5C` ; les
allouer séparément faisait lire du tas quelconque. Détail et preuves en tête de
`ANALYSE_ECRAN_NOIR_MENU.md`.

L'hypothèse « affichage exclusif DirectDraw » qui occupait ce fichier était **fausse** : le bug a
survécu au changement complet de backend, au même taux.

---

## Build, run, mesure

```powershell
$env:DOTNET_ROOT = "C:\Users\Sagara\.dotnet"
$env:PATH = "C:\Users\Sagara\.dotnet;$env:PATH"
dotnet build OpenForesia.sln

# Lancer le jeu : vider DOTNET_ROOT d'abord, le processus est 32 bits
$env:DOTNET_ROOT = $null
dotnet run --project OpenForesia
```

Variables d'environnement utiles :

| variable | effet |
|---|---|
| `OPENFORESIA_LEGACY_3DHELP=1` | rétablit l'ancien chemin `3dhelp` intégral |
| `OPENFORESIA_DUMP_GEOMETRY=1` | relève sommets et décalages sur quelques primitives |

Le taux d'affichage se mesure automatiquement (capture de la zone cliente, comptage des pixels
allumés) — script dans le bloc-notes de session, à reconstituer au besoin : trouver la fenêtre par
sa classe `OpenForesiaWindow` via `EnumWindows` (`FindWindowA` échoue avec `CharSet.Ansi`).
