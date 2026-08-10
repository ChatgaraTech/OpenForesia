# TODO

## Présentation

Ce fichier est à destination du responsable du projet. Il sert de point de reprise entre deux
sessions : l'IA le relit en début de séance pour repartir sur les mêmes rails.

L'historique complet et les décisions techniques sont dans `OpenForesia/src/Blaze/REVERSE_STATUS.md`.
Ce fichier-ci ne garde que l'état courant et ce qu'il faut faire ensuite.

---

## Où en est le projet

L'application **affiche l'écran-titre du CD**, **entre dans la boucle de jeu** et **rend des
frames**. Le démarrage traverse : arène display-list → CRT → scan CD → `BLAZE.cfg` → séquence
d'attract (avec l'image de `TITLE640.BMP`) → init moteur/3D → chargement des ressources → boucle
de jeu → menu principal → boucle de frame.

L'état de jeu 0 est le **menu principal**, confirmé par ses propres données : la table de libellés
lue dans le binaire contient « Début du jeu », « Sélection des personnages », « Menu objets »,
« Options », « Quitter » — exactement les cinq cas du `switch`.

### Ça tourne

- La frame complète du menu : préparation, animation de palette, deux renderers de primitives,
  `DrawOTag`, permutation des tampons, vidage de la file d'uploads VRAM.
- **`DrawOTag` s'exécute sans fauter, frame après frame.** C'était le blocage précédent.
- Le sondage d'entrées, atteint pour la première fois (il est situé après `DrawOTag`).
- `Legacy/Blaze.exe` est dans le dépôt, et `OpenForesia.Tools at <va>` lit n'importe quelle donnée.
- Sept changes OpenSpec, six implémentées.

---

## Le point de reprise : le menu se dessine, mais à l'échelle 1:1 dans un coin

**Le rendu marche.** Une bissection l'a établi : une primitive plate non texturée (un rectangle
rouge plein écran) s'affiche. Donc device, environnements, `DrawOTag`, permutation et présentation
sont tous sains — inutile d'y revenir.

Et l'observation qui explique le reste : ce rectangle de **320×240** occupait le **quart supérieur
gauche** d'un écran en **640×480**, avec le cadre du menu visible au même endroit. Le menu est donc
dessiné correctement, **il n'est simplement pas mis à l'échelle**.

La mise à l'échelle passe par les primitives de code `0xFB` qu'émettent `EmitViewportRect`
(`FUN_0051be20`) et `EmitClipRect` (`FUN_0051bdb0`), toutes deux pilotées par
`s_UnknownDetailSetting_005de160` et par `SetShift`. Avec le niveau de détail par défaut `0xb`,
`(0xb - 0xb) = 0` : les deux rectangles sortent à zéro. **C'est la première chose à examiner.**

### Stabilité : réglée (12/12)

Trois causes distinctes de corruption mémoire, toutes converties en échecs nommés :

1. **Débordement de pile** dans `RunTextEngine` — la collecte des échappements `$` écrivait sans
   borne dans un tampon de pile de 8 octets. L'original s'en sort parce que son texte ne porte
   jamais plus de 6 échappements. C'était la signature du `Fatal error` CLR.
2. **Index de slot joueur non borné** dans `DrawUiPanel` — `slot` est un octet servant d'index avec
   un pas de `0x9c`/`0x4c` dans des tables de `0x500`, et la valeur lue devenait un **offset
   d'écriture** dans un tampon de `0x140`. C'était le `0x80131506` (corruption du tas managé).
3. **Descripteur alloué trop tard** — rendu paresseux pour que la relance fonctionne, il se
   retrouvait alloué à la première frame du menu, quand le runtime avait saturé la fenêtre basse.
   Il est maintenant réclamé dès l'initialiseur de module, dans le `try`.

Plus des bornes sur : banc de glyphes, slot tpage/CLUT, atlas UI, parcours de la table d'ordre du
panneau, et parcours du flux de texte dans les deux passes.

L'arène fait jusqu'à **3 tentatives** de relance (~1 perte sur 4 par tentative, donc <2 % d'échec).

### Protocole de test automatique

Plus besoin d'appuyer sur une touche. Lancer, attendre ~5 s, puis :

```powershell
$sh = New-Object -ComObject WScript.Shell
$t = Get-Process -Name OpenForesia | Sort-Object Id | Select-Object -Last 1
$sh.AppActivate($t.Id); Start-Sleep -Milliseconds 600; $sh.SendKeys(" ")
```

**Tuer tout l'arbre** en fin de test (`Get-Process -Name OpenForesia | Stop-Process -Force`) :
les enfants relancés survivent au parent et verrouillent l'exe, ce qui fait échouer le build suivant.

### Reste à traiter

- **Comportement aléatoire d'un lancement à l'autre**, et des `Fatal Error` du CLR occasionnels.
  Signe de corruption mémoire quelque part dans les écritures par pointeur. Une garde a été posée
  sur la boucle de recopie de la table d'ordre du panneau (`DrawUiPanel`), qui déréférençait sans
  vérifier le lien nul — reste à chercher ailleurs.
- **Le placement de l'arène** est passé à 10/10 grâce à une relance unique bornée, mais celle-ci
  se déclenche encore ~4 fois sur 10. Le fond du problème reste la concurrence avec le CLR sur la
  fenêtre de 16 Mo.

---

## Historique du diagnostic de l'écran noir

Le menu tourne sans lever aucune exception, et **la géométrie est correcte** : le panneau émet
21 primitives bien formées dans la table d'ordre, à des adresses valides sous 16 Mo. Mesuré.

Ce qui a déjà été corrigé pendant le diagnostic :

- **Le descripteur du panneau était zéroé.** `DAT_0056dd58` est statiquement initialisé dans la
  `.data` (position, dimensions, et surtout les bits `0x1000000` qui activent fond / cadre / texte).
  Récupéré du binaire, 35 octets non nuls. Sans lui, `DrawUiPanel` ne dessinait strictement rien.
- **`AllocLowZeroed` affamait l'arène.** Les initialiseurs de champs statiques s'exécutent avant
  le corps d'`entry()`, donc une petite allocation basse coupait en deux la région dont l'arène a
  besoin. L'arène se réserve maintenant en premier, et les petites allocations partent du haut.

- **`DAT_005e8fc0` n'est pas une seconde table.** `0x005e8fc0 − 0x005e8f00 = 0xC0`, soit 48 entrées :
  c'est la **même** table de tpage/CLUT vue 48 entrées plus loin, et Ghidra en a nommé deux points.
  Modélisées comme deux allocations séparées, l'entrée `0x31` que réclame le panneau
  (= entrée 1 de `DAT_005e8fc0`, remplie par `LoadFirstLevelResources`) revenait à zéro, donc
  **toutes les primitives référençaient la page de texture 0** — une VRAM vide, d'où le noir avec
  une géométrie par ailleurs juste. Mesuré : `slot[0x31]` passe de `0x00000000` à `0x73C0021B`.

  Au passage : le répertoire du menu ne contient que **4 entrées** (slots 0–3), donc
  `LoadMenuResources` ne pouvait jamais atteindre l'entrée 49 par lui-même.

**Ce qui reste ouvert : le placement de l'arène est intermittent.**

L'arène de 2,5 Mo doit tenir sous `0x01000000`, une fenêtre de 16 Mo que le runtime .NET se
dispute. Elle est réclamée dès l'initialiseur de module (le plus tôt où du code managé s'exécute),
et le repli ne demande plus les `0xDBD8` de décalage inutiles — ça a fait passer la réussite de
~60 % à ~75 %, mais **environ un lancement sur quatre échoue encore**, proprement, avec le message
qui nomme la contrainte.

Prochain levier si ça gêne : empêcher le CLR de réserver dans cette fenêtre (variables
d'environnement GC), ou un lanceur natif qui réserve avant le démarrage du runtime.

Écartés par la mesure, inutile d'y revenir :

| vérifié | résultat |
|---|---|
| gardes de `DrawOTag` (`VSyncDelayed`, `DAT_00dedc44`) | tous à 0, le dessin s'exécute |
| device 3D (`DAT_011be760`) | vivant, fenêtre valide |
| environnement de dessin | `clip=(0,240,320,240) ofs=(0,240) isbg=1` — double buffering correct |
| liste d'affichage | 21 primitives, liens 24 bits valides |

**Piste à suivre** : `LoadMenuResources` (Ghidra `FUN_00542160`) et `DecodeResourceChunk`, qui
doivent remplir `DAT_005e8f00` et téléverser l'atlas du menu en VRAM.

---

## Ensuite, par ordre de rentabilité

1. **Remplir `DAT_005e8f00` et téléverser l'atlas** — c'est ce qui sépare d'un menu visible.
2. **`FUN_0052a440`** — le dispatcher d'état de jeu, atteint quand le menu passe la main.
3. **`FUN_00512320`** — la page d'options (80 l.), stub actuel.
4. **`FUN_00523120`** — création du device DirectInput. Débloquerait `FUN_005048f0` et supprimerait
   la déviation du sondage joystick.
5. **Le device DirectSound** (TODO #26) — aucun `DirectSoundCreate` localisé. Toute la pile audio
   est reconstruite mais inerte : `PlayMusicTrack` s'exécute sans rien produire, ce n'est pas un bug.
6. **Les 26 gestionnaires de codes de contrôle** non reconstruits (dialogue, listes d'objets, texte
   de combat). Le menu n'en touche aucun ; ils lèvent nommément si un autre écran les atteint.
   Ils demandent une reprise Ghidra ciblée — adresses dans `Documentation/DataSymbols.md`.

---

## Dette et points ouverts

- **Sept changes OpenSpec non archivées.**
- **Trois déviations environnementales** documentées dans `REVERSE_STATUS.md` : le repli 32 bits
  quand `SetDisplayMode(640,480,16)` renvoie `E_NOTIMPL`, `Restore` sur `DDERR_SURFACELOST`, et le
  placement de l'arène display-list (voir ci-dessous).
- **Une infidélité connue, non corrigée** : l'index de `PollInputAndAudio` code en dur l'index
  externe à 0 au lieu de lire `_DAT_005de538`. Équivalent aujourd'hui, à corriger quand on
  retouchera cette zone. Détail dans `REVERSE_STATUS.md`.
- **Deux tâches non faites** dans `reconstruct-main-menu-state` : la table musicale complète
  (seules les entrées échantillonnées sont transcrites) et `FUN_005048f0`.

---

## La contrainte 24 bits — à connaître avant de toucher au rendu

Toute la display list (table d'ordre **et** primitives) est adressée sur **24 bits** : `ClearOTagR`
et `DrawOTag` masquent par `0xffffff` et se servent de l'octet de poids fort comme champ de
longueur, et `Blaze.exe` fait pareil (`*prim = *otEntry | 0x2000000`). Tout ce qui entre dans une
display list doit donc vivre **sous `0x01000000`**.

En 1998 c'était automatique. Aujourd'hui l'ASLR déplace `winsys3d.dll`, donc au démarrage on alloue
l'arène nous-mêmes en mémoire basse et on écrit son adresse dans le pointeur `MainMem` que la DLL
exporte. Le fichier `.dll` n'est pas modifié.

La ligne affichée au lancement dit où elle a atterri :

```
winsys3d arena at MainMem=0x00B6DBD8 — original 1998 layout, addresses match winsys3d.c one for one
winsys3d arena at MainMem=0x0083DBD8 — 0x00B6DBD8 was unavailable; subtract 0x330000 from ...
```

Dans le second cas, **il faut appliquer le delta** à toute adresse lue dans `winsys3d.c`.

---

## Comment lancer

Build et exécution demandent des environnements **incompatibles** (l'appli est x86, le seul SDK
est x64) :

```
Build :  $env:DOTNET_ROOT="C:\Users\Sagara\.dotnet" ; dotnet build OpenForesia.sln
Run   :  DOTNET_ROOT non défini, lancer OpenForesia\bin\Debug\net9.0\OpenForesia.exe
```

Le CD doit être monté. Cliquer la fenêtre dans la barre des tâches pour lui donner le focus,
puis une touche pour sortir de l'attract.
