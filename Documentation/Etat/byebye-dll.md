# byebye-dll — se passer des cinq DLL de 1998

Point de reprise du chantier. **Lire ce fichier en premier**, puis `DLLAtteignables.md` pour les
chiffres à jour. Dernière mise à jour : **2026-09-23** — **le chemin d'affichage est entièrement en C#** : `DrawOTag` et tout ce qu'il atteint (134 corps sur 157, les 23 autres ne sont atteints par rien). Reste `winsys3d` hors chemin d'affichage.

## Le but

Reconstruire en C#, sous `OpenForesia/src/Libraries/`, ce que le jeu appelle des cinq DLL livrées
avec `Blaze.exe`, et retirer les `[DllImport]` au fur et à mesure. **Seulement ce qui est atteint** :
sur 135 623 lignes de décompilation, le devis réel est de ~15 000.

## Où on en est

| DLL | points d'entrée restants | corps portés / clôture | reste |
|---|---:|---:|---|
| `wsyskern` | **2** (`Malloc`, `Free`) | 55 / 89 | les 2 sont des passerelles de tas, voir ci-dessous |
| `winsys3d` | 15 | 212 / 247 | **en cours** ; sept lots faits, puis le chemin d'affichage par aliasing, **fini** — voir la fin |
| `3dhelp` | 0 | — | 1 seul export, déjà remplacé par `Host/SyntheticDevice.cs` |
| `gldhelp` | 0 | — | Glide/3Dfx, inatteignable ; à faire pour la complétude seulement |
| `playst` | 0 | — | **morte** : aucun binaire ne la nomme. Rien à faire |

`wsyskern` est fait : messages, images, codec, famille tableau. Tout compile, **rien n'a encore
tourné** — voir « à vérifier ». Les quatre corps ajoutés le 2026-09-22 (`_splitpath`, `_makepath`
et leurs deux appelées) n'ont pas d'import : c'est `winsys3d` qui les appelait, pas le jeu.

## Reprendre demain

```bash
sh OpenForesia.Tools/census-dll-surface.sh > Documentation/Etat/DLLAtteignables.md
# ou, en double-clic : tests\update-DLLAtteignables.bat
```

Le document donne, par DLL : les points d'entrée employés, la clôture d'appels triée par taille
(le devis, du plus cher au moins cher), la CRT écartée, et ce qui sort vers Win32.

## Les sept règles apprises en chemin

1. **Porter par sous-arbre complet d'un point d'entrée**, jamais par sujet. Le tas, les vtables et
   les sections critiques sont des représentations partagées : les couper en deux donne une
   corruption qui ne se diagnostique pas. Voir `src/Libraries/LISEZMOI.md`.
2. **La clôture est un plancher.** Elle ne suit que les appels nommés. Les vtables sont invisibles :
   la famille tableau annonçait 18 corps, elle en a demandé 30 de plus.
3. **Ghidra ne décompile pas tout.** Six corps de vtable manquaient à `wsyskern.c`. `disasm` marche
   sur les DLL comme sur `Blaze.exe` ; garder les relevés sous `Legacy/Raw/<Dll>/`.
4. **La CRT est liée statiquement** et Ghidra n'en nomme qu'une partie. Les amorces anonymes vont
   dans `OpenForesia.Tools/crt-dll.txt`, une par une, **avec leur preuve**. Sur `wsyskern` elles
   pesaient 40 corps et 1 655 lignes.
5. **Le commentaire de traçabilité porte les trois pièces** — `// Ghidra: wsyskern.c LoadBitmapEx
   @ 0x1000bac0`. C'est ce que le recensement lit. Une mention en prose n'est pas une transcription.
6. **Une globale de DLL relue par du code encore natif bloque le portage de qui l'écrit.**
   `InitTrigTables` (337) remplit cinq tables que 21 corps natifs relisent : le porter les mettrait
   à zéro côté DLL. En revanche une table qui est une **fonction pure de constantes** (la table de
   sinus) peut exister en double sans danger — les deux copies sont identiques par construction.
   C'est ce qui distingue ce cas de la règle 1.
7. **La façade `Interop/` ne bouge pas.** Porter = y remplacer un `[DllImport]` par un renvoi.
   Aucun appelant ne change (1 970 sites `Winsys3d.*` dans 154 fichiers).

## À vérifier — la dette du chantier

- **La famille de fichiers de la carte mémoire n'a pas tourné non plus** (2026-09-22). Elle se
  valide en une manœuvre : l'écran de sauvegarde (state 4) liste les fichiers par les ordinaux
  18/19, en ouvre un par le 21, le lit par le 24 et le referme par le 22 — et `SaveSlots.cs` passe
  par les quatre. **Si la liste sort vide alors qu'une sauvegarde existe**, regarder d'abord la
  convention `~` : c'est le seul endroit où la transcription décide quelque chose de non trivial.
- **Les quatre derniers corps isolés non plus** (2026-09-22), mais trois d'entre eux ne peuvent
  guère échouer : `CdSearchFile` rend une adresse fixe, `EnableEvent` écrit un mot, le casier
  135/136 est jeté par son unique appelant. **Le seul qui calcule vraiment est `ScaleMatrix`**
  (794) — neuf produits en virgule fixe 4.12 — et il se voit à l'œil : un facteur d'échelle
  appliqué de travers déforme ou aplatit un modèle.
- **Rien de `wsyskern` n'a été exercé.** Le codec et la famille tableau se valident ensemble :
  `LoadArray` → `LoadFile` → `Unpack` est le chemin qui fait passer du vrai flux compressé. Une
  session de jeu suffit à le dire.
- **Les deux passerelles de tas.** Plus rien n'appelle `wsyskern.dll` à part elles. Avant de
  basculer sur `NativeMemory`, établir qu'**aucun pointeur alloué par `winsys3d.dll` (encore
  native, et qui alloue dans le tas de wsyskern) ne remonte jusqu'à nous**. Tombera de soi-même
  quand `winsys3d` sera porté — c'est le bon moment.
- **La table de sinus, au dernier bit.** Le 387 calcule `fsin` sur 80 bits, `Math.Sin` sur 64. Un
  écart d'un ulp ne changerait l'entier tronqué que si `sin * 4096` tombait à moins de ~1e-13 d'un
  entier ; ce n'est le cas qu'aux indices 0, 1024, 2048 et 3072, où les deux donnent exactement 0,
  4096, 0 et -4096. Probabilité cumulée ailleurs : ~1e-9. Vérifiable pour de bon en appelant
  l'ordinal 337 natif et en relisant `base + 0x1cb420` — non fait.
- **`TODO(REVERSE)` ouverts** : le bloc par thread de `wsyskern` (`+0x11c` sortie fatale, `+0x120`
  crochet de message, `+0x124` caractère d'échappement) — rien dans la clôture ne les écrit, donc
  ils valent zéro ; trouver qui les pose. Et `FUN_10012020`, la conversion en flottant du genre 2,
  dont le corps n'a pas été lu — ne pas supposer qu'elle est standard, `strtolong` ne l'était pas.

## `winsys3d` — les sept lots faits, et ce qu'il reste

44 points d'entrée restants, 246 corps, 13 402 lignes — et, vu la leçon n° 2, **davantage** : le chemin
d'affichage sort par la vtable du device de `3dhelp`, une arête que le relevé ne voit pas.

**Les sept lots ci-dessous ont vidé tout ce qui se détachait.** Les 44 points d'entrée restants
sont tous dans la masse ; la suite n'est plus un découpage par sous-arbre mais une décision, voir
« Ce qui reste » plus bas.

L'ordre suivi (les ordinaux se groupent d'eux-mêmes) :

1. ~~les souches vides~~ — **fait le 2026-09-21** : 21 imports retirés, `Libraries/winsys3d/Souches.cs`.
   **Le relevé était incomplet : il y a HUIT corps de souche, pas quatre** — 223 exports, pas 149.
   Les quatre manquants sont sortis le 2026-09-22 : `FUN_00a02e10` (`xor eax,eax ; ret 8`, 22 ord.,
   **765**), `FUN_00a02e40` (`ret 8`, 41 ord., **767**), `FUN_00a046e0` (`xor eax,eax ; ret 0Ch`,
   9 ord., **97**) et `FUN_00a046f0` (`mov eax,1 ; ret 4`, 2 ord., importée par personne). Les deux
   premières sont les jumelles à **deux** arguments de souches déjà connues, à trente octets de
   distance ; les deux autres vivent au milieu du `libapi`. **La leçon** : chercher par le corps
   (`grep -c "0x2e10  *[0-9]" winsys3d.c`), jamais par l'ordinal importé — c'est l'erreur
   symétrique de celle que `ListeAffichage.cs` documente pour 677/662 ;
2. ~~la trigonométrie~~ — **fait le 2026-09-21** : `rcos`, `rsin`, `ratan2` (450/451/457),
   `Libraries/winsys3d/Trigonometrie.cs`. **337, 376, 378, 379 restent natifs** — voir la règle 6 ;
3. ~~la section critique~~ — **fait le 2026-09-21** : 40 et 41, `SectionCritique.cs`. Le clavier
   était déjà fait. Reste `Setup3DDevice` (823) et la sous-couche carte mémoire ;
4. ~~la liste d'affichage~~ — **partiellement fait le 2026-09-21** : 658, 660, 661, 688 portés
   (`ListeAffichage.cs`). **Les dix autres sont bloqués**, le tableau corps par corps est dans
   l'en-tête de ce fichier ;
5. ~~la famille de fichiers de la carte mémoire~~ — **fait le 2026-09-22** : **8 imports retirés**
   (18 à 25), `Libraries/winsys3d/CarteMemoire.cs`, plus le pivot `FUN_00a03f80` et les ordinaux
   29 et 30 que la composante lui soude sans que rien les importe. Les composantes 2, 9, 10, 12 et
   13 de l'ancien relevé ont disparu d'un coup. Deux corps de `wsyskern` ont suivi — `_splitpath`
   et `_makepath` (ordinaux 4 et 5), que `winsys3d` importe et que le jeu n'appelle jamais :
   `Libraries/wsyskern/Chemins.cs` ;
6. ~~le lecteur de conteneur TIM~~ — **fait le 2026-09-22** : 677 et 662 (`OpenTIM`/`ReadTIM`),
   `Libraries/winsys3d/Tim.cs`. Deux corps, une globale (`DAT_011be790`) dont les dix références
   tiennent dans la paire. **Ils n'ont pas rejoint `ListeAffichage.cs`** : lire un conteneur
   d'image n'est pas tenir la liste d'affichage, et `LISEZMOI.md` veut qu'un sujet identifiable
   prenne son fichier. Sont partis avec eux les ordinaux **765 et 767**, qui se sont révélés être
   deux *souches* de plus (voir le point 1) ;
7. ~~les quatre derniers corps isolés~~ — **fait le 2026-09-22**, et c'est **la fin de tout ce qui
   se détachait**. Quatre familles PSY-Q différentes, un fichier chacune, chacun amorce du lot que
   la masse rendra un jour :
   - **92** → `Cdrom.cs`, la `libcd`. `CdSearchFile`, vidé : il ignore ses deux arguments et rend
     l'adresse d'une `CdlFILE` statique **que rien n'écrit** (une seule référence dans toute la
     DLL). Le nom est établi par trois preuves concordantes — voir le pavé de tête ;
   - **794** → `Matrices.cs`, la `libgte`. `ScaleMatrix`, le **seul des neuf ordinaux libgte
     importés qui ne touche aucun registre émulé du GTE** ; les huit autres sont dans la masse ;
   - **10** → `Evenements.cs`, la `libapi`. `EnableEvent`, le seul des huit ordinaux 6-13 qui ne
     parcourt pas la liste chaînée — il n'écrit que dans le nœud que l'appelant lui donne ;
   - **135/136** → `Cadence.cs`. Un casier (`DAT_011be6a4`) et ses deux seuls accesseurs. **Le
     rôle n'est pas établi** : les deux corps gardent un nom d'adresse, et le nom du fichier dit
     le voisinage d'ordinaux (132, la temporisation d'image), pas le rôle.

**Trois choses apprises sur le lot de la carte mémoire**, toutes dans les pavés de tête :

- **la convention `~` de la carte mémoire.** `FUN_00a03f80` remplace le premier `:` d'un chemin, à
  partir de l'octet 2, par un `~` ; l'énumération (18 et 19) fait la lecture inverse en sautant les
  4 ou 5 premiers octets quand elle voit un `~`. Les deux bouts se lisent ensemble ou pas du tout ;
- **l'ordinal 29 ne peut pas fonctionner.** Il résout ses deux descripteurs dans le *même* casier
  statique `DAT_00dedc58`, donc `MoveFileA` reçoit deux fois le nom d'arrivée. C'est sans doute
  pourquoi rien ne l'appelle. Transcrit tel quel — voir `Reference/legacy-bug.md` ;
- **la CRT MBCS ne change rien sur une page à un octet.** `_ismbblead` et `_mbsdec` dépendent de
  `_mbctype`, que `_setmbcp` remet à zéro pour toute page dont `GetCPInfo` dit `MaxCharSize < 2`.
  Les deux branches des quatre corps concernés coïncident alors octet pour octet. La preuve tient
  en quatre lignes et elle est écrite dans `wsyskern/Chemins.cs` — ne pas la refaire.

## Ce qui reste, mesuré par `DLLGlobales.md`

`sh OpenForesia.Tools/census-dll-globals.sh winsys3d` soude les corps par leurs appels et par les
globales qu'ils écrivent, et sort les composantes. Une composante se porte entière ou pas du tout.

**Depuis le 2026-09-22 il ne reste QU'UNE composante**, et c'est la masse. Les treize autres sont
parties : plus rien ne se détache sans l'attaquer. Le relevé n'a donc plus de liste de courses,
il a un front unique.

**La masse** — composante 1 : **425 corps, 22 415 lignes, 44 points d'entrée**, dont `DrawOTag`
(701), `InitTrigTables` (337), toute la géométrie, `Setup3DDevice` et la sous-couche d'affichage.
`DrawOTag` seul touche treize globales, sept appelées et **quatre emplacements de la vtable du
device de 3dhelp**. C'est un chantier à part entière, pas un lot.

### `DAT_011be760` n'est PAS le levier — mesuré le 2026-09-22

**La note qui figurait ici était fausse, et elle était de la veille.** Elle disait : « le pointeur
de device est touché par 73 corps et écrit par un seul, c'est le genre de globale où une coupure
est possible ». Le raisonnement était bon, le résultat non. Mesure, en relançant
`census-dll-globals.awk` avec `DAT_011be760` **retiré de la carte** :

| | composantes | plus gros morceau |
|---|---:|---|
| carte normale | 1 | 425 corps, 22 415 lignes |
| sans `DAT_011be760` | **2** | **419 corps, 22 326 lignes** |

Le seul corps qui se détache est **l'ordinal 750** (`FUN_00a01e30`, 12 lignes), qui n'est rien
d'autre que `return DAT_011be760;`. Les 419 autres restent soudés par autre chose. **Retirer le
pointeur de device ne sert à rien.**

### Et ce n'en a jamais été une, de soudure

Le relevé le comptait parce que la règle est volontairement pessimiste, mais la valeur de
`DAT_011be760` est **une poignée vers un objet que nous possédons déjà** :

- `Host/SyntheticDevice.cs` détourne `GetProcAddress` dans la table d'imports de `winsys3d` et
  rend sa propre fabrique. Le `DAT_011be760` de la DLL native **pointe donc déjà sur un objet C#** ;
- `Screens/State00/Options.cs` s'en sert depuis `src/Blaze/` sans passer par `Libraries/` :
  `UiDeviceVtable => *(void***)Winsys3d.Ordinal_750()`, puis des appels `Thiscall` directs sur la
  vtable. Cela tourne en production depuis le 2026-08-12.

Un corps porté n'a donc pas besoin d'une copie de la globale : **il la relit à la demande par
l'ordinal 750**, ce qui est exactement ce que fait l'original — il relit la globale à chaque usage,
il ne la met pas en cache.

**Et c'est pourquoi il ne faut PAS porter l'ordinal 750.** Notre copie serait vide : l'écrivain est
`FUN_00a06420` (789), resté natif. Le `[DllImport]` de 750 *est* la fenêtre sur la globale native,
et le remplacer par une lecture du `.data` de la DLL chargée serait un faux portage — le même
travail, déguisé. Il reste natif tant que 789 l'est.

### Ce qui soude vraiment : deux familles disjointes

Le chemin d'affichage de `winsys3d` est en **deux moitiés qui ne se touchent pas**. C'est la
mesure la plus utile de la journée, et elle n'était écrite nulle part :

| famille | reconnue à | corps | lignes | squelettes distincts |
|---|---|---:|---:|---:|
| les **producteurs** | ils écrivent `DAT_011be7dc` | 38 | 4 886 | **11** |
| les **appelants de device** | ils lisent `DAT_011be760` | 73 | 4 696 | 36 |
| *recouvrement* | | **0** | | |

**Zéro recouvrement, vérifié.** Aucun des 38 producteurs n'appelle le device ; aucun des 73
appelants n'écrit le tampon. Ensemble ils font 111 corps et 9 582 lignes — moins de la moitié de
la masse (22 415) : le reste est la géométrie, les textures et l'initialisation.

**Les producteurs sont le morceau bon marché.** Onze squelettes de contrôle pour trente-huit
corps, et les six familles les plus peuplées en couvrent trente. Ce sont des émetteurs de
primitive, un par genre : chacun pose une commande d'état *si le cache dit qu'elle a changé*,
écrit trois ou quatre sommets (`DAT_011be7a8 += 0x18` ou `0x20`, soit 3 ou 4 `TLVERTEX` de
32 octets) et avance le curseur. **C'est la leçon de `FUN_004747F0` appliquée** : ce qui prédit
le coût est le nombre de formes, pas le nombre de lignes.

Ce qu'ils partagent, et qui interdit de les couper en deux :

| globale | rôle | ampleur |
|---|---|---|
| `DAT_011be7ac` | la base du tampon, allouée par `FUN_00a0c480` (`Malloc(n * 0x94)`) | 46 corps |
| `DAT_011be7dc` | **le curseur d'écriture** | **1 572 références dans 38 corps** |
| `DAT_011be7a8` | la zone de sommets, à `base + n * 0x14` dans le même bloc | 38 corps |
| `DAT_00a2c480/484/488/48c` | le cache « quel état ai-je déjà émis » | 85, 53, 84, 33 corps |
| `DAT_00a2c578`, `DAT_00a2c57c` | la dernière commande d'état écrite | 38 corps chacune |

`FUN_00a0c480` les remet toutes ensemble — curseur à 2, tous les mémos à `-1`.

**C'est le cache qui interdit la coupure**, plus encore que le curseur. Deux curseurs dans un
tampon, cela se voit ; deux caches de « quel état est posé », non : la moitié portée croirait
l'état déjà émis parce que la moitié native l'a émis, et sauterait la commande. Une primitive
sortirait avec le mélange de la précédente, une image sur cent. C'est exactement le défaut que la
règle 1 décrit, à son pire.

**CORRECTION du 2026-09-22.** Une version antérieure de ce paragraphe disait que `DAT_011be7ac`
était « un symbole réutilisé par des corps sans rapport, les comptes de soudure sont surévalués ».
**C'est faux.** En remontant la chaîne jusqu'au consommateur, la globale a un sens unique et net :
c'est **le paquet courant** — celui qu'on remplit pendant la construction, celui qu'on rejoue
pendant l'affichage, celui qu'on libère quand le nœud disparaît (`FUN_00a0c440`, ordinal 754,
lit son en-tête puis le `Free`). Ce n'est pas de la réutilisation de compilateur, c'est une
variable de travail parfaitement cohérente. Les comptes de `DLLGlobales.md` ne sont pas
surévalués pour elle.

### La chaîne complète, du producteur à l'écran

Trouvée le 2026-09-22 en remontant les appelants. **Le consommateur est `FUN_00a0cd20`**, 46 lignes.

```
  CONSTRUCTION, pendant l'image
    FUN_00a0dde0 (ordinal 752)          le point d'entrée : un paquet, un genre de primitive
      -> FUN_00a0c480(n)                Malloc(n * 0x94), curseur à 2, tous les mémos à -1
      -> switch (genre) -> l'un des ~31 producteurs    écrit les commandes et les sommets
      -> *noeud = paquet | 0x80000000   LIE LE PAQUET DANS L'OT DU JEU, pointeur taggé

  AFFICHAGE, à DrawOTag
    FUN_00a061b0 (ordinal 701, DrawOTag)
      device[0x60 ou 0x5c](0,0) ; device[0xb0]()        BeginScene
      parcours de l'OT (liens sur 24 bits, sentinelle 0x00ffffff)
        -> FUN_00a13400(noeud)          540 lignes
             -> FUN_00a0cdd0            DAT_011be7ac = *noeud
                  -> FUN_00a0cd20()     ** LE CONSOMMATEUR **
      device[0xb4]()                                    EndScene
```

### Le format du paquet

En-tête de 8 octets — `[0]` le lien OT, `[1]` la longueur —, puis des enregistrements de **0x10
octets** à partir de l'offset 8 (ce qui est exactement le `DAT_011be7dc = 2` de l'initialisation).
Le parcours s'arrête quand le mot d'opcode **suivant** vaut `0xffffffff` ; chaque producteur écrit
ses quatre mots puis pose cette sentinelle sans avancer, de sorte que le producteur d'après
l'écrase.

| opcode | ce que le consommateur en fait |
|---|---|
| `0xfffffffb` | `device[0x64](0)` — délie la texture |
| `0xfffffffc` | `FUN_00a27880(arg0, arg2, 0, 0)` |
| `0xfffffffd` | rien — un trou, laissé exprès |
| `0xfffffffe` | `device[0x8c](arg0)` |
| **≥ 0** | `device[0xbc](opcode, arg0, arg1, arg2, 0)` — **`DrawPrimitive`** |

Le cas par défaut dit tout : l'enregistrement **est** un appel `DrawPrimitive` sérialisé —
`[type de primitive][type de sommet][pointeur de sommets][nombre]`. Le slot `+0xbc` est confirmé
par `Documentation/DLL/3dhelp.md` et par `Options.cs`, qui l'appelle déjà directement.

### Ce n'est pas un cache anti-doublons : c'est un LOT

La raison pour laquelle les producteurs ne se coupent pas est plus forte que ce que disait la
section précédente. Un producteur qui trouve l'état inchangé n'omet pas seulement sa commande —
**il allonge celle d'avant** :

```c
DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;   /* nombre += n */
```

Le mot « nombre » du dernier enregistrement est incrémenté sur place, et les sommets viennent se
poser à la suite dans la zone partagée. **Deux primitives consécutives de même état deviennent un
seul `DrawPrimitive`.** Porter la moitié des producteurs ferait allonger par l'un un enregistrement
écrit par l'autre, avec des sommets rangés de part et d'autre d'une frontière qu'aucun des deux ne
connaît. Ce n'est pas une coupure risquée, c'est une coupure impossible.

### Donc : par où, vraiment — et la piste « le rejeu d'abord » est morte aussi

**CORRECTION du 2026-09-22.** Cette section recommandait « le rejeu d'abord : 97 corps, il ne
construit rien, il lit un format connu ». **Faux.** La clôture de `DrawOTag` contient **20 des
38 producteurs** (1 883 lignes) et **68 de ses 134 corps touchent un mémo d'état** — `DrawOTag`
lui-même remet `DAT_00a2c484/488/48c` à `-1` et `DAT_00a2c480` à 8 au début de chaque image. Le
porter seul laisserait les producteurs natifs relire des mémos que plus personne ne réinitialise :
ils croiraient l'état posé depuis l'image précédente et allongeraient un enregistrement mort.

C'est la troisième piste ouverte puis fermée en une journée — `DAT_011be760`, le tampon seul, le
rejeu seul. Toutes les trois pour la même raison de fond : **le chemin d'affichage est un seul
organisme**, et le mesurer corps par corps ne le disait pas.

**Le périmètre réel est fermé et il est mesuré** : `sh OpenForesia.Tools/census-display-path.sh`
le régénère dans `Documentation/Etat/CheminAffichage.md`.

| | |
|---|---|
| corps | **157** |
| lignes | **12 408** — 55 % de la masse |
| formes distinctes | **70** |
| points d'entrée importés dedans | **1** — l'ordinal 701, `DrawOTag` |
| coutures restées dehors | 26 corps, tous de 13 à 123 lignes |

**Un seul `[DllImport]` pour 12 408 lignes**, et c'est ce qui rend la décision difficile : le
rapport est mauvais si on compte les imports, excellent si on compte ce qu'il ouvre — l'ordinal
701 *est* l'affichage du jeu. Après lui, `winsys3d` n'a plus de raison d'être chargée pour autre
chose que la géométrie et les textures.

**Deux façons de le prendre, et c'est une décision de projet :**

1. **Le portage fidèle intégral** — les 157 corps, 70 formes, avec leurs globales. Aucune
   globale du noyau ne peut exister en double : elles partent toutes, d'un bloc. Plusieurs
   séances, et rien ne tourne avant la fin.
2. **Le portage par aliasing des globales** — notre copie C# du chemin lit et écrit **le `.data`
   réel de `winsys3d.dll` chargée** (base connue, adresses fixes), au lieu d'en tenir une copie.
   C'est ce que fait déjà l'ordinal 750 pour le device, et `Options.cs` pour sa vtable. Le lot
   devient alors **divisible** : un corps porté et son voisin natif partagent la même mémoire, la
   même que dans la DLL. Beaucoup plus rapide, et vérifiable au fur et à mesure — mais cela change
   ce que `Libraries/` veut dire, et `LISEZMOI.md` ne prévoit pas ce troisième cas.

La question à trancher avant d'écrire une ligne est celle-là, pas le découpage.

**Tranchée le 2026-09-22 : l'option 2, l'aliasing.** `LISEZMOI.md` en a fait la sixième règle,
avec cinq garde-fous. Les globales sont désignées dans `winsys3d/Data.cs`, les corps pas encore
portés sont appelés par adresse depuis `winsys3d/Natif.cs`.

### Le chemin d'affichage par aliasing — où on en est

Portage **de haut en bas**, un corps à la fois ; le tableau de bord était l'en-tête de `Natif.cs`, supprimé à l'étape 10.

| étape | corps | état |
|---|---|---|
| 1 | `DrawOTag` (701) — `CheminAffichage.DrawOTag` | fait le 2026-09-22 ; `OPENFORESIA_LEGACY_DRAWOTAG=1` rebranche l'original |
| 2 | `FUN_00a13400`, le répartiteur — `RejouerNoeud` | fait le 2026-09-22 |
| 3 | `FUN_00a0cdd0` + le consommateur `FUN_00a0cd20` — `RejouerPaquet` / `RejouerPaquetCourant` | **fait le 2026-09-23** |
| 4 | les **42 primitives GPU** du répartiteur, six familles — `winsys3d/Primitives.cs` | **fait le 2026-09-23** |
| 5 | les 5 dernières appelées du répartiteur : trois rejeux, le sprite tourné, le fond en tuiles | **fait le 2026-09-23** |
| 6 | les six appelées de `DrawOTag` : cache de textures et texte à l'écran | **fait le 2026-09-23** |
| 7 | l'intérieur du cache de textures et la liaison de texture | **fait le 2026-09-23** |
| 8 | le chargement des textures, et le `qsort` de la CRT | **fait le 2026-09-23** |
| 9 | le constructeur `dde0` (752) et ses 15 adaptateurs | **fait le 2026-09-23** |
| 10 | les 18 producteurs TMD et la reconstruction `c4f0` | **fait le 2026-09-23** |

**Rien des étapes 3 et 4 n'a tourné.** L'étape 3 se voit dès qu'une scène passe par les opcodes
0xe3/0xe4 (paquets de modèles 3D) : si le consommateur se trompait, ce sont les modèles qui
disparaîtraient ou planteraient, pas le décor 2D. L'étape 4, elle, se voit **partout** : tout ce
qui passe par l'OT du jeu sans tampon de commandes — sprites, tuiles, polygones plats ou
texturés, lignes — sort désormais du C#. Une erreur y donne une image fausse dès le premier écran
dessiné par `DrawOTag`.
`OPENFORESIA_LEGACY_DRAWOTAG=1` rebranche tout l'original pour comparer.

### Étape 4 — les primitives GPU, en six familles

Les codes de l'aiguillage sont ceux de la `libgpu` PlayStation ; chaque gestionnaire traduit une
primitive en un `DrawPrimitive` immédiat, sur l'un de **deux tableaux fixes de quatre
`D3DTLVERTEX`** (`&DAT_011cb138`, `&DAT_011be7e8`). Le relevé par squelette de
`CheminAffichage.md` a donné les familles exactement :

| famille | forme | corps | ce que c'est |
|---|---|---:|---|
| 1 | `CIVIVCIIIVR` | 9 | plates : `LINE_F2/3/4`, `POLY_F3/4`, `TILE`, `TILE_1/8/16` |
| 2 | `CIICRCIIVVR` | 7 | texturées semi-transparentes brutes (FT3/FT4/GT3/GT4, SPRT/8/16) |
| 3 | `CICECIVVR` | 7 | les mêmes, opaques |
| 4 | `CIICRCIIVIIIVR` | 5 | texturées semi-transparentes modulées (FT3/FT4, SPRT/8/16) |
| 5 | `CICECIVIIIVR` | 5 | les mêmes, opaques |
| 6 | (plusieurs) | 9 | Gouraud : `LINE_G2/3/4`, `POLY_G3/G4`, `POLY_GT3/GT4` ×2 |

Plus `FUN_00a03db0` (`ChoisirMelange`), l'appelée commune du prologue. Restent natives et appelées
par adresse : `FUN_00a27880` (liaison de texture, `__thiscall`) et `FUN_00a03c80` (mélange selon
le mode `abr`), qui dépendent de corps encore jamais lus.

Appris à l'étape 4 :

- **`DAT_00a2c488` est la page de texture liée**, pas « le type de primitive » — renommée
  `PageTexture_00a2c488` partout, y compris dans `census-display-path.awk` ;
- **deux défauts du binaire**, transcrits tels quels : `LINE_G3`/`LINE_G4` ne tracent que leur
  premier segment (**LB-036**) et `POLY_G4` échange les couleurs de deux coins (**LB-037**) ;
- les pièges de lecture de Ghidra sur ces corps : temporaires rangés dans les sommets voisins,
  variables réutilisées au point de rendre illisible la couleur des `GT3`/`GT4` semi-transparents
  (le désassembleur tranche), et un `POLY_FT3` modulé qui n'écrit que trois couleurs quand son
  jumeau brut en écrit quatre.

Appris à l'étape 3 : **`FUN_00a27880` est un `__thiscall`** (this = `DAT_012141d4`) que Ghidra
déclare comme tel mais dont il omet le `this` à ses 30 sites d'appel. Tout appel par adresse vers
un corps de ce voisinage (`0x00a27xxx`, les autres `__thiscall` de Ghidra) doit d'abord regarder
si `ecx` est chargé avant le `call`.

### Étape 5 — les cinq dernières appelées du répartiteur

Faite le 2026-09-23. Le répartiteur `RejouerNoeud` **n'appelle plus rien de natif**.

| corps | opcode | où | ce que c'est |
|---|---|---|---|
| `FUN_00a0e000` | 0xe6 | `CheminAffichage.ConstruireEtRejouer` | construit le paquet si le bit 31 du descripteur est nul, puis le rejoue |
| `FUN_00a133e0` | 0xe7 | `CheminAffichage.RejouerPaquetConstruit` | rejoue sans construire ; `ret 0Ch`, un seul des trois arguments lu |
| `FUN_00a0e040` | 0xf9 | `CheminAffichage.RejouerListe` | rejoue une liste de descripteurs |
| `FUN_00a0e0c0` | 0xe5 | `Primitives.SpriteTourne` | sprite à pivot, échelle et angle — **LB-038** |
| `FUN_00a142a0` | 0xe8 | `FondTuiles.DessinerFond` (fichier neuf, validé par Vincent) | fond en tuiles qui défile — **LB-039** |

Les deux gros ont été transcrits **sur le désassemblage**, pile x87 suivie instruction par
instruction ; les relevés sont sous `Legacy/Raw/Winsys3d/`. Ghidra y donnait les bonnes formules
— y compris les tables d'écran croisées du sprite non tourné, qui sont dans le binaire.

**Un écart assumé, le seul des étapes 3 à 5** : ces deux corps gardent leurs sommets sur la pile et
n'en écrivent ni `z`, ni `rhw`, ni la spéculaire. L'original y laisse ce que la pile contenait,
ce qui n'est pas reproductible ; le C# y met zéro (`w = 1` pour le device). Si une différence se
voit avec `OPENFORESIA_LEGACY_DRAWOTAG=1` sur un sprite tourné ou un fond en tuiles, c'est là.

### Étape 6 — les six appelées de `DrawOTag`

Faite le 2026-09-23. **`DrawOTag` n'appelle plus directement aucun corps natif.** Deux sujets,
deux fichiers neufs validés par Vincent :

| fichier | corps | ce que c'est |
|---|---|---|
| `winsys3d/CacheTextures.cs` | `FUN_00a26490`, `FUN_00a26b20`, `FUN_00a26f20` | le cache de 32 pages de texture : ouvrir le relevé, relever les pages que l'OT va lire, charger en évinçant les plus anciennes |
| `winsys3d/TexteEcran.cs` | `FUN_00a027a0`, `FUN_00a027e0`, `FUN_00a02820`, + `024a0`, `024e0`, `02340`, `02380`, `01f00` | les fenêtres de texte (sprites), la console de 30 lignes (`POLY_FT4`), la file GDI de `WinDrawText` — **porté en entier** |

Appris à l'étape 6 :

- **Ghidra se trompe sur `FUN_00a02820`** : il passe au texte GDI l'adresse de la locale ; le
  binaire passe le HDC qu'elle contient (`mov ecx,[esp] ; push ecx`) ;
- **trois `__thiscall` de plus sur `DAT_012141d4`** — `FUN_00a27920`, `FUN_00a27850`,
  `FUN_00a27820` —, et un `cdecl`, le tri `FUN_00a27e00`, qui reçoit sa comparaison par adresse
  (`LAB_00a27100`, passée native telle quelle) ;
- **la liste d'éviction déborde sans garde** sur la table qui la suit — reproduit à l'identique,
  puisque les deux sont aliasées sur la mémoire de la DLL ;
- la table de sauts du relevé a été **relue dans le binaire** (octets à `0x00a26d14`, cibles à
  `0x00a26ca4`), pas prise à Ghidra.

### Étape 7 — l'intérieur du cache de textures et la liaison

Faite le 2026-09-23. **24 corps**, dans `CacheTextures.cs` (le cache) et `Primitives.cs` (le choix
du mélange, `03400`/`03430`/`03c80`, à côté de `ChoisirMelange`) : les huit relevés par genre de
primitive, l'enregistrement des demandes (`264f0`), l'attribution des emplacements (`278c0`),
l'arrondi (`26e10`), la recherche, le placement, la libération et l'éviction des emplacements
(`27920`, `27850`, `275e0`, `27140`, `275a0`, `27180`, `27820`, `27120`), et la liaison de texture
(`27880`, `27430`) que toutes les primitives appellent.

Appris à l'étape 7 :

- **Ghidra perd la moitié de `FUN_00a264f0`** (« bad instruction data ») : la branche
  semi-transparente, avec sa table de sauts à `0x00a26650`, a été transcrite sur le
  désassemblage (`Legacy/Raw/Winsys3d/FUN_00a264f0.disasm.txt`). Et dans la branche qu'il donne,
  il écrit `genre = genre` là où le binaire fait `genre |= nouveau` ;
- **`FUN_00a278c0` rend 1999 quand tout est pris** (`mov eax,7CFh`), un chemin sans valeur de
  retour chez Ghidra ;
- **le cache est une petite hiérarchie C++** — cache, 32 pages, 2 000 emplacements de 0x34 octets
  alloués par `Malloc` — dont Ghidra confond `this` et les arguments (`27850` surtout) ;
- **`Malloc`/`Free` sont appelés par la façade, pas par la passerelle** de `Libraries/wsyskern` :
  la table d'import de `winsys3d` nomme bien ces deux exports, et la passerelle ajoute un
  `OutOfMemory` que l'original n'a pas. Un seul tas, puisque du natif libère encore ;
- **le `qsort` de la CRT reste natif** (`27e00` + `27fb0` + `28010`, avec sa comparaison
  `LAB_00a27100` = `a.date - b.date`) : `Array.Sort` ne rangerait pas les dates égales dans le
  même ordre, donc n'évincerait pas les mêmes pages.

### Étape 8 — le chargement des textures

Faite le 2026-09-23. **6 corps.** `winsys3d/ChargementTextures.cs` (fichier neuf, validé par
Vincent) pour la conversion de la VRAM PlayStation en objets texture — `271e0` (l'aiguillage par
genre), `03460` (image DIB 4/8/24 bits, palette, zoom des pages 8 bits, couleur-clé) et `03980`
(16 bits bruts par une table de 65 536 couleurs) —, et le `qsort` de la CRT (`27e00`, `27fb0`,
`28010`, avec sa comparaison) **transcrit** dans `CacheTextures.cs`, pour garder l'ordre des dates
égales. **Plus rien du sous-système des textures n'est natif.**

Appris à l'étape 8 :

- **LB-040** : dans la conversion 16 → 24 bits, un pixel semi-transparent en mode soustractif est
  effacé par un mot de 32 bits sur 3 octets, et la source n'avance pas — toute la fin de la ligne
  sort noire ;
- l'argument anonyme de Ghidra dans `03980` (`&stack0xfffffdc0`) est une **couleur-clé `{0, 0}`**
  construite sur la pile ; la clé d'une page 16 bits dans `03460` vaut **0**, pas -1 ;
- **un genre inconnu ne charge rien**, et aucun des deux convertisseurs ne teste l'objet que rend
  `device[0x10]` avant d'en lire la vtable ;
- un écart assumé : `dwFourCC` du format de pixel n'est jamais écrit dans l'original (ce que la
  pile contenait) ; le C# y met 0, sans effet pour un format RGB.

### Étape 9 — le constructeur de paquets

Faite le 2026-09-23. **17 corps**, dans `winsys3d/ConstructionPaquets.cs` (fichier neuf, validé
par Vincent) : `ConstruirePaquet` (`dde0`, ordinal 752), `AllouerPaquet` (`c480`) et les 15
adaptateurs `d260`..`dce0`.

**Ce ne sont pas des producteurs mais des adaptateurs** : chacun relit les enregistrements d'un
modèle et en fait, un par un, un paquet de primitive sur sa pile, qu'il passe au vrai producteur
avec les sommets. Huit producteurs seulement pour quinze genres (les genres 8 à 15 reprennent ceux
de 0 à 7, avec des sommets externes et des numéros divisés par 8) — ce sont eux, l'étape 10.

**Engendrés mécaniquement** : un script lit les affectations de Ghidra (six formes, aucune laissée
de côté) et écrit le C#, vérifié ensuite au désassembleur sur trois adaptateurs des deux formes.
Un écart assumé : les champs du paquet de pile que l'adaptateur n'écrit jamais valent 0 ici,
n'importe quoi dans l'original — à contrôler producteur par producteur à l'étape 10.

Le contrôle est fait à l'étape 10 : **aucun producteur ne lit un champ que son adaptateur
n'écrit pas** — l'écart est sans effet.

### Étape 10 — les producteurs TMD et la reconstruction

Faite le 2026-09-23. **22 corps**, dans `ConstructionPaquets.cs` (pas de fichier neuf : c'est la
suite directe de l'étape 9) : la reconstruction `c4f0` (ordinal 755), le choix d'objet `06a40`
(678), la résolution des pointeurs `1cff0` (563), la fonction d'émission `07ca0`, et **dix-huit
producteurs**, un par mode de primitive TMD — F3, FT3, G3, GT3, F4, FT4, G4, GT4, chacun éclairé
ou non, plus les segments LF2 et LG2.

**Les « modèles » de ce chemin sont des TMD PlayStation** : `06a40` vérifie l'identifiant `0x41`
de l'en-tête, et les codes du `switch` de `c4f0` sont les modes TMD. Une entrée de la table
d'objets (sept mots) est convertie en paquet la première fois qu'un nœud 0xe3/0xe4 la rejoue.

Appris à l'étape 10 :

- **Ghidra a deux écarts systématiques sur les producteurs**, tous deux relevés au
  désassembleur : la couleur est rangée **en entier** dans le DWORD du `D3DLVERTEX`, pas
  convertie (`(float)` n'est chez lui qu'une réinterprétation) ; et les coordonnées de texture,
  qu'il écrit `0.0`, sont une **division entière par ~140 millions** — donc bien nulles, mais
  transcrites telles quelles, constante par constante (**LB-042**) ;
- **LB-041** : le quadrilatère plat non éclairé émet une liste de triangles à quatre sommets,
  soit un seul triangle ;
- **le flux de `c4f0` est reconstruit sur le désassemblage** (Ghidra saute dans la boucle), et
  ses deux tables de sauts relues dans le binaire. Une bizarrerie gardée : après conversion, le
  premier non fait n'avance que si `debut == indice - 1` ;
- les quadrilatères écrivent leur commande **toujours** (un éventail ne s'allonge pas), les
  triangles et segments seulement si elle change.

Relevés : `Legacy/Raw/Winsys3d/FUN_00a0*.disasm.txt`, un par corps.

### Ce qui reste — le compte exact (2026-09-23)

Mesuré en croisant le périmètre de `CheminAffichage.md` (157 corps) avec la clôture ATTEINTE de
`DLLAtteignables.md` :

| | corps |
|---|---:|
| chemin d'affichage, porté | **134** — tout ce qui est atteint |
| chemin d'affichage, **jamais atteint** | 23 (~3 300 lignes) |
| `winsys3d` hors chemin d'affichage, atteint et à porter | **72** |

**`DrawOTag` et tout ce qu'il atteint sont en C#**, et `Natif.cs`, sa dette à zéro, est **supprimé** (garde-fou 2 de `LISEZMOI.md`). **Rien
de l'étape 3 à l'étape 10 n'a encore tourné** : c'est la vérification à faire avant d'avancer.
`OPENFORESIA_LEGACY_DRAWOTAG=1` rebranche tout l'original pour comparer une même scène.

**Les 23 jamais atteints n'ont pas à être portés.** Ce sont surtout les exports 548 à 555
(`FUN_00a149f0`..`FUN_00a1c800`, une vingtaine de corps de ~190 lignes), plus 704, 753 et 754 :
ils écrivent le tampon de commandes, d'où leur place dans le périmètre, mais ni le jeu ni aucun
corps atteint ne les appelle.

Restent les **72 corps atteints hors du chemin d'affichage** — 44 points d'entrée : la géométrie
GTE, l'initialisation du device (`Setup3DDevice`), `InitTrigTables`, les décalages d'écran
(`SetShift`, `SetAbsShift`, `ShiftVertices`), `ChangeResolution`, `SetTimingMode`... —, puis les
deux passerelles de tas de `wsyskern`, qui ne tomberont qu'avec le dernier corps de `winsys3d`.
L'aliasing lève la règle 6 : une globale relue par du natif ne bloque plus rien, puisque C# et
natif lisent la même mémoire.

### Hors chemin d'affichage — lot 1, les décalages d'écran (2026-09-23)

`winsys3d/DecalageEcran.cs` (fichier neuf, validé par Vincent) : `SetShift` (796), `SetAbsShift`
(791), `GetAbsShift` (776), `ShiftVertices` (824). Quatre feuilles, vérifiées au désassembleur ;
la façade renvoie, 15 sites d'appel inchangés. `winsys3d` passe à 178 corps, 40 points d'entrée.
À retenir : `SetShift` fait pointer les deux tables de conversion X et Y sur **la même**, et
`ShiftVertices` n'arrondit que son facteur — le produit et la somme restent en précision étendue.

**Lot 2, `PutDispEnv` (657)** : dans `ListeAffichage.cs` avec son jumeau `GetDispEnv` (656), et la
lecture des manettes `FUN_00a049c0` dans `winsys3d/Manettes.cs` (fichier neuf, validé par
Vincent). `PutDispEnv` est **la fin d'image** : recentrage, attente du balayage (`+0xd8`),
présentation (`+0xb8`), manettes. Deux défauts d'origine dans les manettes, reproduits : **LB-043**.
`winsys3d` passe à 181 corps, 39 points d'entrée.

**Lot 3, la VRAM** (le lot 2 testé en jeu par Vincent) : `winsys3d/Vram.cs` (fichier neuf, validé)
pour `ClearImage` (665), `LoadImage` (674), `MoveImage` (676), `StoreImage` (684) et
l'invalidation d'un rectangle dans le cache (`05c60`) ; trois `__thiscall` du cache (`27700`,
`27730`, `27770`) et les exports `ReInitializeAllTextures` (785) et `ReleaseTexture` (788) dans
`CacheTextures.cs`. Ghidra donnait `27700` sans argument et sans `this` ; `27770` avance sa borne au
lieu de son indice (de l'éviction en trop, transcrite). **LB-044** : la couleur de `ClearImage`.
`winsys3d` passe à 191 corps, 33 points d'entrée.

**Lot 4, les réglages du GTE** (le lot 3 testé en jeu par Vincent, jusqu'à la montagne du dragon de
feu) : neuf corps de la `libgte` dans `Matrices.cs`, qui les attendait — `SetRotMatrix` (366),
`SetLightMatrix` (367), `SetColorMatrix` (368, par la forme), `SetTransMatrix` (369),
`SetBackColor` (376), `SetGeomOffset` (378, par la forme), la rotation-translation de vecteurs
courts (792), `VectorNormal` (439) et `SquareRoot0` (442). **L'aliasing lève la « masse » GTE** :
les registres sont la mémoire de la DLL. `winsys3d` passe à 200 corps, 24 points d'entrée.

**Le GTE importé est entièrement porté** depuis le lot 7 (plus bas) ; 360, 418 et 795 étaient
partis au lot 6.

**Lot 5, les événements `libapi`** : `OpenEvent` (6), `TestEvent` (9), `DeliverEvent` (12) et les
livraisons de fin d'opération carte (67 = 69, 68), dans `Evenements.cs` — qui les disait « dans la
masse » parce qu'ils parcourent une liste tenue dans les globales ; aliasée, elle est la même des
deux côtés. **LB-045** : `OpenEvent` n'enchaîne pas vers l'avant, seul le premier événement est
jamais livré. `winsys3d` passe à 205 corps, 19 points d'entrée.

**Lot 6, la fin du GTE sauf 379** : `CompMatrix` (360), la projection par lots (418) et sa jumelle
sans profondeur (795), plus les deux appelées de 360 — `ApplyMatrixSV` (`1f8f0`, 345) et
`TransMatrix` (`203e0`, 356) —, dans `Matrices.cs`. Tous les trois ont **une branche MMX et une
branche scalaire**, choisies par `MMXInstalled` (`0x00dedbe8`, aliasé) : l'initialisation le pose
par un `__try { pand ; emms }` qui rend 1, donc **c'est la MMX qui tourne**. Les deux sont
transcrites. Sur 360 elles donnent le même résultat au bit près ; **sur 418/795 non** — la MMX
décale chaque moitié de `pmaddwd` avant la somme et ne garde que 16 bits de la translation
(**LB-046**). Relevés sous `Legacy/Raw/Winsys3d/`. La façade de 360 nommait ses arguments à
l'envers (`destMatrix` en premier) : corrigé, les sites passaient déjà le bon ordre.
`winsys3d` passe à 210 corps, 16 points d'entrée. **Pas encore exercé** — une erreur se verrait
sur tous les modèles 3D (déformés, déplacés ou absents) ; `Model3d.cs` passe par les trois.
Testé en jeu par Vincent le même jour.

**Lot 7, `SetGeomScreen` (379) — la fin du GTE** : `SetGeomScreen` et la mise à jour de la
projection du device (`FUN_00a1dbc0`, 564) dans `winsys3d/Matrices.cs` ; côté `wsyskern`,
`ZeroMatrix` (30) et `ProjectionMatrix` (21) dans **`wsyskern/Matrices.cs`** (fichier neuf,
validé par Vincent). Trois choses lues au désassembleur :

- `ZeroMatrix` n'est que `mov eax, 100581B8h ; ret` — l'adresse d'un bloc non initialisé que
  rien n'écrit. Remplacé par un tableau de seize zéros **épinglé** (son adresse sort de la
  fonction, elle ne doit pas bouger) ;
- `ProjectionMatrix` est la matrice D3D du SDK DirectX 5 (`m[0] = m[5] = cos`, `m[10] = Q`,
  `m[11] = sin`, `m[14] = −Q·near`, avec `Q = (float)sin / (1 − near/far)`) ; `m[14]` part du Q
  **non arrondi**, resté sur la pile x87. Écart assumé : `double` au lieu de la précision étendue,
  avant l'arrondi en `float` ;
- la table `(h << 12) / z` de `SetGeomScreen` ne teste que les 16 bits hauts : toute profondeur
  négative sature à 0x7fff, et un quotient de 0x8000 à 0xffff se range tel quel, relu négatif.

Relevés : `FUN_00a21040`, `FUN_00a1dbc0` sous `Legacy/Raw/Winsys3d/`, `ProjectionMatrix` sous
`Legacy/Raw/Wsyskern/`. `winsys3d` passe à 212 corps, 15 points d'entrée. **Pas encore exercé** —
une erreur se verrait sur la perspective : un champ de vision faux étire ou écrase toute la scène
3D, et une table fausse déforme les modèles avec la profondeur.

**Un défaut trouvé en jeu le même jour, corrigé** : le `PutDrawEnv` porté (660) rangeait le
`DRAWENV` dans un tableau C# privé, alors que `DrawOTag` lit son `isbg` dans la DLL
(`DAT_011be6f0`, +0x18 du bloc, sous un autre nom Ghidra) — l'image n'était jamais effacée. **La
leçon** : compter les références d'un bloc par le nom Ghidra de son début ne voit pas les
symboles posés au milieu. Chercher toute la plage d'adresses.

Lire `Documentation/DLL/winsys3d.md` avant de commencer — 52 Ko déjà accumulés.

## Rappel

**Aucune commande `git` qui écrit.** Décrire ce qui a changé, mettre à jour `HISTORY.md`, laisser
Vincent commiter.
