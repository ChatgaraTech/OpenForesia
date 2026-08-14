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
flèche et remonte, et la touche de validation quitte le menu. La reconstruction bute alors sur
`FUN_0052a440`, le répartiteur d'états de jeu — **nouveau point de blocage, et du code ordinaire.**

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
