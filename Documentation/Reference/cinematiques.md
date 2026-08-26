# Les cinématiques

Ce que le jeu joue au démarrage, pourquoi ça ne marche plus nulle part, et par quel bout ça se
reprend. Établi le 2026-08-26, en lisant les fichiers du CD et en décodant deux images.

## Ce que l'original joue, et quand

Le séquencement est déjà transcrit — `FUN_00529c10`, et la spécification `blaze-intro-attract`
d'`openspec`. Il se lit ainsi :

```
TEPS.AVI                     le logo de l'éditeur
OPENING.AVI                  l'introduction
puis en boucle :             titre 20 s → ANI01.AVI → titre 20 s → OPENING.AVI
```

Chaque étape s'interrompt à la première touche, au clic, ou à un bouton de manette — le filtre de
messages de `FUN_00542c40` liste `WM_KEYDOWN`, `WM_LBUTTONDOWN`, `WM_MBUTTONDOWN`, `WM_RBUTTONDOWN`
et les deux `MM_JOY*BUTTONDOWN`. L'écran de titre entre deux films est un simple bitmap,
`DATA\TITLE640.BMP`, affiché par `StretchDIBits` avec un délai de **20 secondes**.

Le chemin est construit par `FUN_00542eb0` à partir de `DAT_005631f8`, qui vaut `"@:\FRENCH\"` : le
`@` est remplacé au démarrage par la lettre du lecteur CD.

## Les fichiers

Relevés sur le CD, dans `E:\French\` (et `E:\FRENCH2\`, une seconde version) :

| fichier | taille déclarée | images | durée |
|---|---|---|---|
| `Teps.AVI` | 1,6 Mo | 324 | 21 s |
| `opening.avi` | 28 Mo | 2 692 | 3 min |
| `ani01.avi` | — | — | l'attract |
| `ending.avi` | 47 Mo | — | la fin |

**La taille du fichier n'est pas la taille des données.** `opening.avi` pèse 463 Mo sur le disque et
son `RIFF` n'en déclare que 28 : le reste est du bourrage de pressage CD. Un lecteur qui va jusqu'à
la fin du fichier lit 435 Mo de zéros. C'est la taille du `RIFF` qui fait autorité.

| flux | format |
|---|---|
| vidéo | **Indeo Video 4.1** (`IV41`), 256×224, 24 bits, **15 im/s** |
| audio | **MS-ADPCM** (`wFormatTag` 0x0002), mono, 22 050 Hz, 4 bits |

## Pourquoi ça ne marche plus

**Microsoft a retiré les codecs Indeo de Windows** — ils ont d'abord été désactivés pour cause de
faille, puis supprimés. Sur cette machine, `SysWOW64` ne contient ni `ir41_32.ax` ni `ir50_32.dll` ;
seul Cinepak (`iccvid.dll`) survit. Le jeu d'origine construit donc un graphe DirectShow qui ne peut
pas se bâtir, lève, et l'exception est rattrapée : la séquence passe à la suite sans rien afficher.

Ce n'est pas un défaut du jeu, et ce n'est pas réparable dans le jeu.

## Le décodeur est sur le CD du jeu

`E:\DXMedia\DXMEDIA.exe` (DirectX Media, 28/09/1998) est un auto-extractible qui contient **douze
cabinets**. Le dixième porte `ir41_32.ax`, `ir50_32.dll` et leurs compagnons. Les cabinets ne sont
pas dans un format qu'`expand` reconnaît depuis l'exécutable ; il faut les découper d'abord — un
cabinet commence par `MSCF`, quatre octets nuls, puis sa taille sur quatre octets.

```powershell
# 1. découper les cabinets (voir le script de la séance dans scratchpad, ou refaire la boucle)
# 2. extraire le codec
expand.exe cab10.cab -F:ir41_32.ax .
```

`ir41_32.ax` **exporte `DriverProc`** : ce n'est pas seulement un filtre DirectShow, c'est aussi un
codec **Video for Windows**. Et `ICOpenFunction` accepte qu'on lui passe un `DriverProc` directement.
Conséquence : le codec s'utilise **sans rien installer et sans rien enregistrer dans le système**,
avec cinq appels, dans le seul processus du jeu.

### Le piège qui coûte une demi-journée

`ICOpenFunction` n'envoie au pilote que `DRV_OPEN`. Il n'envoie **ni `DRV_LOAD` ni `DRV_ENABLE`**, et
Indeo 4 prépare ses tables à la charge. Sans ces deux messages, le codec :

- s'ouvre correctement,
- répond `0` à `ICM_DECOMPRESS_QUERY`,
- répond `0` à `ICM_DECOMPRESS_BEGIN`,
- puis rend **`-100` (`ICERR_ERROR`)** au premier `ICM_DECOMPRESS`, pour tous les formats de sortie
  qu'il vient pourtant d'accepter.

Un échec générique, très loin de sa cause. Les envoyer à la main avant `ICOpenFunction` suffit :

```csharp
drv(IntPtr.Zero, IntPtr.Zero, 1 /* DRV_LOAD   */, IntPtr.Zero, IntPtr.Zero);
drv(IntPtr.Zero, IntPtr.Zero, 2 /* DRV_ENABLE */, IntPtr.Zero, IntPtr.Zero);
```

La sonde qui l'a établi essayait huit formats de sortie sur deux orientations : **les seize
échouaient identiquement**, ce qui disait que le format n'était pas en cause. Quand toutes les
variantes d'un paramètre échouent de la même façon, ce n'est pas ce paramètre.

### Ce qui marche, vérifié

`OpenForesia.Tools\decode-movie.ps1`, lancé sous le PowerShell 32 bits, sort une image en PNG :
le logo THQ de `Teps.AVI` à l'image 120, et le texte d'introduction français d'`opening.avi` à
l'image 400. Le démultiplexage AVI et l'écriture PNG sont écrits dans le script, sans dépendance ;
seul le décodage passe par le codec de 1998.

## Dans le jeu — fait le 2026-08-26

Les cinématiques se jouent au démarrage. `TEPS.AVI` puis `OPENING.AVI`, image par image, à la
cadence lue dans l'en-tête du film, et une touche les passe.

| pièce | où | état |
|---|---|---|
| démultiplexage AVI | `Host/AviReader.cs` | **en flux**, jamais le fichier entier en mémoire |
| décodage Indeo 4 | `Platform/IndeoCodec.cs` | la DLL du CD, ni installée ni enregistrée |
| cadence, affichage, abandon | `Host/MoviePlayback.cs` | horloge du film, `StretchDIBits` en 640×480 |
| branchement | `Engine/Video.cs`, `TrySetupMovieStream` | remplace le graphe DirectShow introuvable |
| **le son** | — | **pas encore joué**, voir ci-dessous |

**Le pas-à-pas ne passe pas par le minuteur de 100 ms de l'original.** Les films sont à 15 im/s, soit
66,7 ms : un minuteur Windows à 100 ms ne saurait pas les tenir. La boucle de lecture avance donc
sur l'horloge du film, et rattrape son retard en décodant sans afficher — une image qui n'est pas
une image clé se compose sur la précédente, donc aucune ne peut être sautée.

**L'abandon vient de la fenêtre du jeu, pas de celle du lecteur.** En démarrage fenêtré c'est la
nôtre qui a le focus, donc c'est elle qui reçoit les touches ; `D3D11Window` appelle
`MoviePlayback.Abort`. C'est la même déviation, et pour la même raison, que celle déjà documentée
pour les frappes de la boucle d'attract. Un film interrompu rend **1**, un film allé au bout rend
**0** — distinction que `FUN_00529c10` exploite : `OPENING.AVI` ne se joue que si `TEPS.AVI` a rendu
zéro.

**Conséquence sur les tests** : `playtest.ps1` attend désormais **30 secondes** par défaut avant de
chercher le menu, contre 12 auparavant — `TEPS.AVI` dure 21 secondes.

### Ce qui reste : le son

Les films portent une piste **MS-ADPCM mono 22 050 Hz**, et elle n'est pas jouée. Il faut la
décoder — une table de sept couples de coefficients et une boucle, une centaine de lignes sans
dépendance — puis la faire sortir par le chemin audio du jeu, en la tenant synchronisée avec
l'horloge des images. C'est le prochain morceau.

## Et si on voulait écrire le codec à la main ?

C'est possible, et le projet a déjà tout ce qu'il faut pour : `ir41_32.ax` est un binaire x86 de
857 Ko, exactement le genre d'artefact que `OpenForesia.Tools disasm` sait ouvrir. Le format
lui-même se laisse lire — le mot de synchronisation de 18 bits en tête de chaque image vaut
`0x3FFF8`, **poids faible en tête**, vérifié sur douze images d'affilée ; le champ de trois bits qui
suit est le genre d'image, et vaut `0` sur la première puis alterne `2` et `4`.

Mais il faut mesurer ce que ça représente : les 857 Ko contiennent l'encodeur, le filtre DirectShow,
les boîtes de dialogue et plusieurs variantes optimisées ; le décodeur seul reste un sous-système
entier, avec ses tables de Huffman, ses matrices de quantification et ses transformées. Ce serait la
plus grosse pièce de rétro-ingénierie que le projet ait entreprise.

**Et ce n'est pas nécessaire.** Le projet appelle déjà `winsys3d.dll` et `wsyskern.dll` d'origine
plutôt que de les reconstruire ; se servir du décodeur d'origine est le même geste, pas un
compromis. Si la réécriture se fait un jour, le chemin Video for Windows restera de toute façon
l'oracle qui la valide image par image — c'est lui qui rend la réécriture faisable, pas l'inverse.

**Une réserve honnête** : ce codec a été retiré de Windows pour cause de faille. Ici il n'est ni
installé ni enregistré, il vit dans le seul processus du jeu et ne reçoit que les quatre fichiers du
CD. Le risque est borné par là, et il faut le savoir plutôt que l'ignorer.
