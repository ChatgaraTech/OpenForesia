# TODO

Point d'entrée entre deux sessions. État au 2026-08-15, fin de séance (deuxième séance de la
journée, sur une machine différente de la précédente — voir §7). Le build est vert.

Rien de non commité à ce stade : ce `TODO.md` est la seule chose qui reste à committer avec le
reste de la séance (sous-état 6, corrections `playtest.ps1`/`CLAUDE.md`/`test-protocol.md` pour la
nouvelle machine).

---

## Où on en est

L'écran de sélection des personnages (état de jeu 4) **fonctionne** et se traverse jusqu'au
sous-état 6 inclus (répartition des points de statistiques). Chaîne des sous-états :

```
0  carrousel des 16 personnages          OK
│
1  saisie du nom (clavier virtuel)       OK
│
3  choix du style de langage             OK      (2 = retour arrière, non transcrit)
│
4  carrousel des 8 éléments              OK
│
5  tirage du bonus (trois cartes)        OK
│
6  répartition des points de stats       OK      **nouveau cette séance**, cf. ci-dessous
│
7  ?                                     ← À FAIRE, c'est ici que ça s'arrête
```

Le sous-état 2 n'est atteint qu'en retour arrière depuis le 3.

Débit de rendu par écran, utile comme repère de non-régression :

| écran | primitives / image |
|---|---|
| menu principal | ~110 |
| carrousel des personnages | ~382 |
| saisie du nom | ~593 |
| style de langage | ~555 |
| carrousel des éléments | ~433 |
| tirage du bonus | ~429 |
| répartition des points | ~644 |

---

## 1. Le sous-état 6 — ce qui marche, ce qui s'arrête

`CharacterSelectSubState6` (Ghidra: `FUN_005098f0`) est transcrit et confirmé par playtest : sept
lignes de statistiques en éventail, curseur, dépense/remboursement de points, icône + texte de
description, plus deux lignes partagées (rendu identique, valeurs à l'origine incertaine — voir
REVERSE_STATUS.md). **`EmitParticle` tourne enfin** (débit ~644 prim/image, nettement au-dessus des
autres écrans, aucune exception).

**`FinalizeCharacterStats` s'arrête à cinq appels non reconstruits** (`FUN_00538ab0`, `538c50`,
`538e20`, `538f50`, `538fa0`) quand le joueur confirme la répartition (pool à zéro + bouton de
confirmation). Tout ce qui précède est transcrit et testé ; l'exception nomme les cinq adresses.
Le chemin **annulation** (`case 0x5d`, pas besoin de ces fonctions) est complet et sort l'écran
correctement via `state[0] = -1`.

Détail complet, y compris la table de contrôle-flux et les branches de dialogue mortes : voir
`REVERSE_STATUS.md`, section « 2026-08-15 — new machine, then `FUN_005098f0` ».

**Trouvaille en marge, sans rapport avec le sous-état 6** : un test avec une séquence de touches
différente a fait tomber sur une exception préexistante (control code `0x14` de l'engin de texte,
`Blaze.cs` ~ligne 10267) via le panneau « Bonus » toujours actif — confirmé antérieur à cette
séance (`git show HEAD:...`), déclenché par le hasard du lancer de dés, sans lien avec ce qui a été
écrit ici. Pas traité, à corriger un autre jour si ça revient.

---

## 2. La suite : sous-état 7, puis 2

| sous-état | fonction | taille | note |
|---|---|---|---|
| 7 | `FUN_0050a910` | 719 lignes | **le prochain sur le chemin** — `state[2]=0x6e` à l'arrivée |
| 2 | `FUN_00508410` | 146 lignes | retour arrière depuis le 3, pas urgent |

Les corps sont dans `Legacy/Raw/Blaze/FUN_*.c`. Pour `FUN_0050a910`, vérifier d'abord si le
`.asm` à côté est complet (celui de `FUN_005098f0` ne l'était pas — la table de saut à deux niveaux
a fait sauter Ghidra par-dessus tous les corps de case ; se reporter au `grep -c DAT_X Blaze.c`
plutôt qu'au `.asm` dans ce cas, comme détaillé dans REVERSE_STATUS.md).

`InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact pour tout sous-état
pas encore câblé.

---

## 3. Comment tester — `test-protocol.md`

**Lire `test-protocol.md` avant tout lancement.** Pour atteindre le sous-état 6 :

```powershell
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER -Seconds 15 -Shot
```

Neuf `ENTER` avec `WAIT2` entre chaque : nom, style de langage, élément, puis le tirage des trois
dés (chaque carte a besoin de son propre `ENTER` pour se retourner, plus un dernier pour confirmer
le total). **Le minutage est sensible au hasard du lancer de dés** — si la capture montre encore
l'écran des dés ou un plantage sur le control-code 0x14 (voir §1), relancer ou ajuster les `WAIT`.

Pour exercer les entrées du sous-état 6 une fois arrivé (curseur, dépense de point) : ajouter
`DOWN,WAIT1,DOWN,WAIT1,RIGHT,WAIT1,RIGHT,WAIT1,UP,WAIT1,LEFT` à la suite.

Si le script ne couvre pas un cas, **l'étendre** et le noter dans `test-protocol.md`, ne pas le
contourner.

---

## 4. Re-run Ghidra à signature forcée

Levier le plus utile pour aborder un nouveau sous-état à signature perdue. Deux scripts, **jamais
commités** — seul leur code source est préservé dans `REVERSE_STATUS.md` (§"The re-run" et
§"All eight handlers re-decompiled") :

| script | rôle |
|---|---|
| `ForceDecompileSubStates.java` | les huit handlers de sous-état, signature `uint* h(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)` |
| `ForceDecompileParticlePass.java` | une fonction seule, signature à cinq paramètres (gabarit à adapter) |

**Sur cette machine, `~/ghidra_scripts` n'existe pas** : recopier les deux scripts depuis
`REVERSE_STATUS.md` avant le premier lancement. Ghidra est installé sous
`D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC` (pas `F:\`, chemin de l'ancienne machine).

Lancer **depuis PowerShell** (le `.bat` casse son propre chemin sous Git Bash) :

```powershell
$gh = "D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "D:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "$env:USERPROFILE\ghidra_scripts" `
      -postScript ForceDecompileSubStates.java
```

**Ce que ça apporte et ce que ça n'apporte pas** :

- ✅ le prototype et l'arité, donc `pad[3]` au lieu de `*(byte *)(in_stack_00000010 + 3)` ;
- ❌ **les locaux de pile**. Les descripteurs de sprite de 20 octets sortent toujours en
  `&stack0xffffffdc`. Pour ceux-là il faut lire le `.asm`, il n'y a pas de raccourci —
  **sauf si le `.asm` lui-même est incomplet** (cas rencontré cette séance, voir §2 et
  REVERSE_STATUS.md) : dans ce cas, `grep -c DAT_X Blaze.c` sur l'ensemble du fichier peut trancher
  une ambiguïté que le `.asm` local ne peut pas.

---

## 5. Pièges de méthode

**Compter les `PUSH` avant de lire un `[ESP+n]`, et vérifier via le nombre de `POP` de
l'épilogue.** Le plus fiable et le moins cher des deux.

**Les emplacements d'arguments sont réutilisés comme scratch** ; les noms de variables de Ghidra
dans la queue d'une fonction ne valent rien, même signature forcée.

**Une boucle qui parcourt un tableau depuis un champ intérieur décale tous les `[reg + n]`.**

**Un amas de `DAT_` dans l'étendue d'un enregistrement connu est un champ, pas une globale** —
soustraire la base déjà déclarée la plus proche avant de déclarer quoi que ce soit.

**Deux `DAT_` qui ne diffèrent que de quelques octets ne sont pas forcément un artefact du
décompilateur.** Trouvé cette séance : `DAT_005e97c0` et `DAT_005e97c4` avaient l'air d'un
aliasing Ghidra classique (quatre octets d'écart, même usage `+0x94`) — un `grep -c` sur tout
`Blaze.c` a montré que ce sont deux globales réelles et distinctes (74556/74793 : la seconde est
construite à partir de la première). **Le réflexe « ça sent l'artefact » se vérifie par un grep
pangénérique, pas par un haussement d'épaules.**

**Une faute native (`0xC0000005`) dans un appel winsys3d est une question d'arité avant tout.**

**Une fonction winsys3d sans export et sans appelant dans sa propre décompilation est nourrie par
la procédure de fenêtre de la DLL**, que ce portage ne fait pas tourner — vérifier avant de
supposer une transcription manquante côté jeu.

**Quand la géométrie est bonne mais l'image fausse, regarder l'écran, pas les chiffres.**

---

## 6. Réserves connues (à regarder si l'image est fausse plutôt qu'absente)

- **Chemin MMX non transcrit** dans `DrawAnimatedModel` : seul le chemin scalaire du produit de
  matrices est écrit, l'original branche sur `MMXInstalled_exref`. Équivalence non vérifiée.
- **Quirk des pages de texture**, deux occurrences dans `DrawAnimatedModel`/`EmitParticle` — voir
  REVERSE_STATUS.md, transcrites telles quelles.
- **Curseur de grille du clavier virtuel** : un disque sombre masque la première case.
- **`DAT_00562c9c`** : comparaison morte (`TODO(REVERSE)` posé).
- **Boîtes de dialogue modales** : `DAT_00560fef`, `DAT_0056075b`, `DAT_00560763`, `DAT_005e909f`
  lisent 0 (code non reconstruit) — toutes les confirmations passent directement. Le sous-état 6 a
  plusieurs branches supplémentaires de cette famille, transcrites mais mortes de la même façon.
- **Contrôle-flux texte 0x14** : `FUN_00544FB0` a un corps reconstruit pour un des deux codes qui y
  mènent, mais pas pour l'entrée `0x14` de la table de rendu — voir §1. Pas grave en soi, juste à
  garder en tête si le panneau « Bonus » replante ailleurs.

---

## 7. Bugs du jeu d'origine — `legacy-bug.md`

Une bizarrerie constatée dans OpenForesia peut être la transcription **fidèle** d'un défaut de 1998.
Vérifier `legacy-bug.md` avant de corriger quoi que ce soit ; la règle du projet est de les
reproduire, pas de les réparer.

Première entrée (LB-001) : sortir de la saisie du nom par **Échap** bloque définitivement l'écran au
clavier. Reproduit à l'identique dans le portage.

---

## 8. Rappels d'outillage — nouvelle machine

**Cette séance a démarré sur une machine différente de la précédente.** Ce qui a changé, déjà
corrigé dans le code/la doc (`CLAUDE.md`, `test-protocol.md`, `playtest.ps1`) :

- Le `dotnet` du PATH (`C:\Program Files (x86)\dotnet`) porte déjà un SDK **x86** (9.0.317) : `dotnet
  build` et `dotnet run --project OpenForesia` marchent tel quels, **aucun** `DOTNET_ROOT` à poser.
  `playtest.ps1 -Build` détecte ce cas automatiquement (`dotnet --list-sdks` d'abord) et ne bascule
  sur un chemin de repli que si le PATH n'a pas de SDK.
- `OpenForesia.Tools` tourne directement aussi (même raison).
- Ghidra est sous `D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC`, pas `F:\`.
- `~/ghidra_scripts` est vide sur cette machine — voir §4 avant d'en avoir besoin.

**Lire le binaire ne demande aucun outil.** Conversion adresse → décalage fichier :

| section | VA | décalage |
|---|---|---|
| `.text` | `0x00401000` | `0x000400` |
| `.rdata` | `0x0054D000` | `0x14B800` |
| `.data` | `0x0054F000` | `0x14D600` |
| `.idata` | `0x00608000` | `0x16FA00` |
| `.rsrc` | `0x0060A000` | `0x170C00` |

`dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe <va> <taille>` lit directement une
adresse Ghidra (hex décalage/section/octets/chaîne C/dwords résolus) — plus simple que `od` sur
cette machine puisque l'outil tourne nativement. C'est ainsi qu'ont été relevées cette séance les
trois tables de `FUN_005098f0` (`0x562ab8`, `0x562ac0`, `0x562ac8`).

**Éditer `Blaze.cs` depuis bash.** UTF-8 avec BOM, et les heredocs de ce shell n'honorent pas
toujours le délimiteur quoté. Passer par l'outil d'édition, ou par un fichier temporaire écrit
directement.

**Avant de déclarer un symbole, greper son adresse.** Plusieurs doublons créés faute de le faire
dans des séances passées — et cette séance a montré que le grep doit porter sur `Blaze.c` en
entier, pas seulement sur ce qui est déjà dans `Blaze.cs` (cf. §5, `DAT_005e97c0`).

---

## Contexte accumulé

| fichier | contenu |
|---|---|
| `OpenForesia/src/Blaze/REVERSE_STATUS.md` | le journal détaillé, sections datées. Tout le raisonnement de cette séance y est. |
| `Documentation/DataSymbols.md` | les valeurs relevées dans le binaire, par VA Ghidra |
| `Documentation/DLL/winsys3d.md` | libgpu / libgte / registres GTE, et l'arité de l'ordinal 795 |
| `legacy-bug.md` | les bugs de 1998 |
| `test-protocol.md` | comment exercer le jeu |

Deux inventaires utiles dans `Blaze.cs` :

```bash
grep -n "TODO(REVERSE)" OpenForesia/src/Blaze/Blaze.cs        # 85 — ce qui reste à établir
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # 27 — ce qui lève à l'exécution
```

Les seconds sont les vrais murs : chacun porte un message qui nomme précisément ce qui manque, et
c'est en général la meilleure entrée en matière quand on ne sait pas par quoi commencer.
