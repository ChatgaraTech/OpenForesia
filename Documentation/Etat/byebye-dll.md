# byebye-dll — se passer des cinq DLL de 1998

Point de reprise du chantier. **Lire ce fichier en premier**, puis `DLLAtteignables.md` pour les
chiffres à jour. Dernière mise à jour : **2026-09-21** (souches, trigo, section critique, liste d'affichage, carte des globales).

## Le but

Reconstruire en C#, sous `OpenForesia/src/Libraries/`, ce que le jeu appelle des cinq DLL livrées
avec `Blaze.exe`, et retirer les `[DllImport]` au fur et à mesure. **Seulement ce qui est atteint** :
sur 135 623 lignes de décompilation, le devis réel est de ~15 000.

## Où on en est

| DLL | points d'entrée restants | corps portés / clôture | reste |
|---|---:|---:|---|
| `wsyskern` | **2** (`Malloc`, `Free`) | 49 / 83 | les 2 sont des passerelles de tas, voir ci-dessous |
| `winsys3d` | 61 | 21 / 243 | **en cours** ; les quatre lots faciles sont faits |
| `3dhelp` | 0 | — | 1 seul export, déjà remplacé par `Host/SyntheticDevice.cs` |
| `gldhelp` | 0 | — | Glide/3Dfx, inatteignable ; à faire pour la complétude seulement |
| `playst` | 0 | — | **morte** : aucun binaire ne la nomme. Rien à faire |

`wsyskern` est fait : messages, images, codec, famille tableau. Tout compile, **rien n'a encore
tourné** — voir « à vérifier ».

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

## Le prochain lot : `winsys3d`

61 points d'entrée restants, 243 corps, 13 323 lignes — et, vu la leçon n° 2, **davantage** : le chemin
d'affichage sort par la vtable du device de `3dhelp`, une arête que le relevé ne voit pas.

Découper par sous-arbre, dans cet ordre suggéré (les ordinaux se groupent d'eux-mêmes) :

1. ~~les souches vides~~ — **fait le 2026-09-21** : 21 imports retirés, `Libraries/winsys3d/Souches.cs` ;
2. ~~la trigonométrie~~ — **fait le 2026-09-21** : `rcos`, `rsin`, `ratan2` (450/451/457),
   `Libraries/winsys3d/Trigonometrie.cs`. **337, 376, 378, 379 restent natifs** — voir la règle 6 ;
3. ~~la section critique~~ — **fait le 2026-09-21** : 40 et 41, `SectionCritique.cs`. Le clavier
   était déjà fait. Reste `Setup3DDevice` (823) et la sous-couche carte mémoire ;
4. ~~la liste d'affichage~~ — **partiellement fait le 2026-09-21** : 658, 660, 661, 688 portés
   (`ListeAffichage.cs`). **Les dix autres sont bloqués**, le tableau corps par corps est dans
   l'en-tête de ce fichier.

## Ce qui reste, mesuré par `DLLGlobales.md`

`sh OpenForesia.Tools/census-dll-globals.sh winsys3d` soude les corps par leurs appels et par les
globales qu'ils écrivent, et sort les composantes. Une composante se porte entière ou pas du tout.

**La masse** — composante 1 : **425 corps, 22 415 lignes, 33 points d'entrée**, dont `DrawOTag`
(701), `InitTrigTables` (337), toute la géométrie et la carte mémoire. `DrawOTag` seul touche
treize globales, sept appelées et **quatre emplacements de la vtable du device de 3dhelp**.
C'est un chantier à part entière, pas un lot.

**Mais treize composantes indépendantes restent**, 21 corps et ~530 lignes au total :

| composante | corps | lignes | ordinaux |
|---:|---:|---:|---|
| 2 | 7 | 245 | 18, 19, 20, 21 |
| 4 | 2 | 47 | 677, 662 |
| 6 | 2 | 28 | 136 |
| 3, 5, 7-14 | 1 chacune | 12-52 | 767, 765, 794, 97, 24, 25, 10, 23, 22, 92 |

**C'est par là qu'il faut continuer** : treize imports retirables sans toucher à la masse. Faire
la composante 2 (la famille `FindFirstFile` de la carte mémoire) d'abord — c'est la plus grosse
et la plus cohérente.

Pour la masse, le vrai levier est ailleurs : `DAT_011be760`, le pointeur de device, est touché par
**73 corps mais écrit par un seul**. C'est le genre de globale où une coupure est possible — et
c'est aussi la couture que `SyntheticDevice` occupe déjà.

Lire `Documentation/DLL/winsys3d.md` avant de commencer — 52 Ko déjà accumulés.

## Rappel

**Aucune commande `git` qui écrit.** Décrire ce qui a changé, mettre à jour `HISTORY.md`, laisser
Vincent commiter.
