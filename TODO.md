# TODO

Point d'entrée entre deux sessions. État au 2026-08-15, fin de séance. Le build est vert.

Le code est commité jusqu'à `500f96c`. Restent non commités trois fichiers de cette fin de séance,
sans effet sur le jeu : ce `TODO.md`, plus un correctif de robustesse de `playtest.ps1` (il attend
désormais la fenêtre au lieu de la supposer prête) et sa note dans `test-protocol.md`.

---

## Où on en est

L'écran de sélection des personnages (état de jeu 4) **fonctionne** et se traverse jusqu'à l'écran
des bonus. Chaîne des sous-états :

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
6  ?                                     ← À FAIRE, c'est ici que ça s'arrête
│
7  ?                                     ← À FAIRE
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

---

## 1. La suite : sous-états 2, 6, 7

| sous-état | fonction | taille | note |
|---|---|---|---|
| 2 | `FUN_00508410` | 146 lignes | retour arrière depuis le 3 |
| 6 | `FUN_005098f0` | 544 lignes | **le prochain sur le chemin** |
| 7 | `FUN_0050a910` | 719 lignes | le dernier |

Les corps sont dans `Legacy/Raw/Blaze/FUN_*.c`, avec leur `.asm` à côté.
`InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact.

**Atteindre le sous-état 6 exercera enfin `EmitParticle`** (`FUN_00520ae0`), transcrit mais **jamais
exécuté à ce jour** : la passe de particules est lancée par le bloc « en scène » de la boucle de
frame, qui vient *après* le dispatch des sous-états. Tant qu'un sous-état lève, elle ne tourne pas.
C'est le premier point à vérifier en arrivant au 6, et il peut très bien y avoir un défaut dedans.

---

## 2. Comment tester — `test-protocol.md`

**Lire `test-protocol.md` avant tout lancement.** Une seule commande :

```powershell
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT2,ENTER -Seconds 12 -Shot
```

Elle construit, lance, navigue jusqu'à l'écran, joue la séquence de touches, capture et rend un
verdict (plantage avec sa pile, ou primitives par image). La capture est dans `scratchpad/` et il
faut **la regarder** : plusieurs bugs de cette séance ne se voyaient que là, les compteurs étant
identiques entre la version fausse et la bonne.

Chaque `ENTER` de la séquence fait avancer d'un sous-état ; en ajouter un pour aller plus loin.

Si le script ne couvre pas un cas, **l'étendre** et le noter dans `test-protocol.md`, ne pas le
contourner.

---

## 3. Re-run Ghidra à signature forcée

Le levier le plus utile de la séance. Deux scripts dans `~/ghidra_scripts` :

| script | rôle |
|---|---|
| `ForceDecompileSubStates.java` | les huit handlers de sous-état, signature `uint* h(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)` |
| `ForceDecompileParticlePass.java` | `FUN_00520ae0` seul, signature à cinq paramètres |

Lancer **depuis PowerShell** (le `.bat` casse son propre chemin sous Git Bash) :

```powershell
$gh = "F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "F:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "C:\Users\Sagara\ghidra_scripts" `
      -postScript ForceDecompileSubStates.java
```

**Ce que ça apporte et ce que ça n'apporte pas** — important, ça évite une fausse attente :

- ✅ le prototype et l'arité, donc `pad[3]` au lieu de `*(byte *)(in_stack_00000010 + 3)` ;
- ❌ **les locaux de pile**. Les descripteurs de sprite de 20 octets sortent toujours en
  `&stack0xffffffdc`. Pour ceux-là il faut lire le `.asm`, il n'y a pas de raccourci.

Pour forcer une autre fonction, copier `ForceDecompileParticlePass.java` et changer l'adresse et
les paramètres — le squelette (`fn.setCallingConvention` + `fn.updateFunction` avec
`DYNAMIC_STORAGE_FORMAL_PARAMS`) est réutilisable tel quel.

---

## 4. Pièges de méthode — ils ont tous coûté du temps cette séance

**Compter les `PUSH` avant de lire un `[ESP+n]`.** `MOV [ESP+0x24],AX` après un `PUSH` vise
`clean + 0x20`, pas `clean + 0x28`. Mal compté, ça faisait croire que le sprite du sous-état 5
chevauchait la zone des arguments et qu'une écriture était morte. **Le nombre de `POP` de l'épilogue
fixe le cadre sans ambiguïté** — c'est la vérification la moins chère.

**Les emplacements d'arguments sont réutilisés comme scratch.** Dans `FUN_00520ae0` les *quatre*
sont écrasés dès l'émission commencée ; le sous-état 4 réutilise l'emplacement de son 4ᵉ argument
comme compteur de boucle. Conséquence : **les noms de variables de Ghidra dans la queue d'une
fonction ne valent rien**, même signature forcée. Écrire depuis le listing et depuis la sémantique.

**Une boucle qui parcourt un tableau depuis un champ intérieur décale tous les `[reg + n]`.** Deux
erreurs de cette façon : la base des particules (fausse de `0x1c`) et `MOV ECX,[ESI + 0x10]` du
sous-état 4 où `ESI` vaut `icône + 0x28` — donc `+0x38`. Lu à `+0x10` il valait zéro et les huit
médaillons sortaient en **disques noirs**, sans que les compteurs bougent.

**Un amas de `DAT_` dans l'étendue d'un enregistrement connu est un champ, pas une globale.**
`DAT_00572b4c..74` sont les champs de `s_SceneEntity_00572b48` ; `DAT_00578df0..e00` sont
`s_SelectState_00578de8[2..6]`. Soustraire la base déjà déclarée la plus proche avant de déclarer
quoi que ce soit.

**Une faute native (`0xC0000005`) dans un appel winsys3d est une question d'arité avant tout.**
Trois fois sur trois : `Ordinal_795`, `FUN_0052a1e0`, `FUN_0052a280`. En cdecl, l'argument manquant
se lit sur la pile de l'appelant.

**Une fonction winsys3d sans export et sans appelant dans sa propre décompilation est nourrie par
la procédure de fenêtre de la DLL**, que ce portage ne fait pas tourner. C'est le cas de
`FUN_00a03f50`, l'écrivain de la file de caractères tapés : il a fallu transcrire la file et la
brancher sur notre `WM_CHAR`. Seule déviation de ce genre du portage.

**Quand la géométrie est bonne mais l'image fausse, regarder l'écran, pas les chiffres.**

---

## 5. Réserves connues (à regarder si l'image est fausse plutôt qu'absente)

- **`EmitParticle` jamais exécuté** — cf. §1.
- **Chemin MMX non transcrit** dans `DrawAnimatedModel` : seul le chemin scalaire du produit de
  matrices est écrit, l'original branche sur `MMXInstalled_exref`. Équivalence non vérifiée.
- **Quirk des pages de texture**, deux occurrences : `DrawAnimatedModel` calcule `tpageWord` et
  `uvWord` depuis le **premier** modèle du paquet ; `EmitParticle` lit ses attributs à
  `atlas[1]`, donc à l'**image 0**, alors que la couleur est relue à l'image courante. Les deux sont
  dans l'original, transcrits tels quels — premiers endroits à regarder si une texture est fausse.
- **Curseur de grille du clavier virtuel** : un disque sombre masque la première case. Position et
  texture sont transcrites telles que l'assembleur les construit ; pas établi si c'est le marqueur
  voulu.
- **`DAT_00562c9c`** : l'original compare un **octet** à `0x4081`, ce qu'aucun octet ne peut valoir.
  Branche morte, probablement une lecture sur deux octets que Ghidra a rétrécie. `TODO(REVERSE)`
  posé.
- **Boîtes de dialogue modales** : `DAT_00560fef`, `DAT_0056075b`, `DAT_00560763`, `DAT_005e909f`
  sont posés par du code non reconstruit, donc ils lisent 0 et seules les branches « pas de
  dialogue » sont atteignables. Toutes les confirmations passent donc directement.

---

## 6. Bugs du jeu d'origine — `legacy-bug.md`

Une bizarrerie constatée dans OpenForesia peut être la transcription **fidèle** d'un défaut de 1998.
Vérifier `legacy-bug.md` avant de corriger quoi que ce soit ; la règle du projet est de les
reproduire, pas de les réparer.

Première entrée (LB-001) : sortir de la saisie du nom par **Échap** bloque définitivement l'écran au
clavier. Cause établie — `DAT_00564a50` est désarmé par la saisie du nom et n'est réarmé que sur le
chemin « Entrée ». Reproduit à l'identique dans le portage.

---

## 7. Rappels d'outillage

**Lancer le jeu.** `dotnet run --project OpenForesia` **ne marche pas** : l'app est x86, le SDK du
PATH n'a que le runtime x64. Lancer l'exe directement, ou passer par `playtest.ps1`.

```
OpenForesia/bin/Debug/net9.0/OpenForesia.exe
```

Pour construire : `$env:DOTNET_ROOT = "C:\Users\Sagara\.dotnet"` **puis le retirer avant de
lancer** — un processus x86 qui en hérite meurt sur `hostfxr.dll … 0x800700C1`. `playtest.ps1 -Build`
s'en charge.

**Lire le binaire ne demande aucun outil.** Conversion adresse → décalage fichier :

| section | VA | décalage |
|---|---|---|
| `.text` | `0x00401000` | `0x000400` |
| `.rdata` | `0x0054D000` | `0x14B800` |
| `.data` | `0x0054F000` | `0x14D600` |
| `.idata` | `0x00608000` | `0x16FA00` |
| `.rsrc` | `0x0060A000` | `0x170C00` |

Puis `od -A x -t x4 -j <décalage> -N <taille> Legacy/Blaze.exe`, qui se diffe contre les constantes
du source. C'est ainsi qu'ont été établis les 26 dwords du descripteur d'interface, les tables de
teintes et de diviseurs, et les corps courts des gestionnaires de texte.

**`OpenForesia.Tools` ne tourne pas** (même cause x86/x64). Palliatif pour la table d'exports :
`OpenForesia.Tools/exports.ps1`.

**Éditer `Blaze.cs` depuis bash.** UTF-8 avec BOM, et les heredocs de ce shell n'honorent pas
toujours le délimiteur quoté (les backticks y sont évalués, les `\xNN` non). Passer par l'outil
d'édition, ou par un fichier temporaire écrit directement.

**Avant de déclarer un symbole, greper son adresse.** Plusieurs doublons créés faute de le faire.

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
grep -n "TODO(REVERSE)" OpenForesia/src/Blaze/Blaze.cs        # 83 — ce qui reste à établir
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # 26 — ce qui lève à l'exécution
```

Les seconds sont les vrais murs : chacun porte un message qui nomme précisément ce qui manque, et
c'est en général la meilleure entrée en matière quand on ne sait pas par quoi commencer.
