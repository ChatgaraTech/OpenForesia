# TODO

Point d'entrée entre deux sessions. État au 2026-08-15, fin de séance (troisième bloc de travail de
la journée : nouvelle machine, sous-état 6, puis sous-état 7). Le build est vert.

Non commité à ce stade : ce `TODO.md`, plus le sous-état 7 et le hook `OPENFORESIA_FORCE_SUBSTATE`
dans `Blaze.cs`. Le reste (nouvelle machine, sous-état 6) est déjà commité (`62c1461`).

---

## Où on en est

L'écran de sélection des personnages (état de jeu 4) se traverse jusqu'au sous-état 6 inclus par le
jeu normal ; le sous-état 7 est transcrit mais **pas atteignable en jouant** (voir §1) et se teste
via un hook dédié.

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
6  répartition des points de stats       transcrit ; la confirmation finale bloque (§2)
│
7  écran de sauvegarde/chargement        transcrit ; le rendu bloque (§3) — **nouveau cette séance**
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
| sauvegarde | pas mesuré — le rendu plante avant la fin de l'image (§3) |

---

## 1. Sous-état 6 → 7 : la chaîne normale ne passe pas encore

`FinalizeCharacterStats` (sous-état 6, confirmation quand le pool de points est à zéro) s'arrête
sur cinq fonctions non reconstruites (`FUN_00538ab0`, `538c50`, `538e20`, `538f50`, `538fa0`) —
c'est le SEUL chemin qui fait avancer `state[0]` de 6 à 7. Tant qu'il n'est pas débloqué, le
sous-état 7 ne se voit jamais en jouant normalement.

**Pour tester le sous-état 7 quand même**, un hook temporaire existe désormais dans
`LoadCharacterSelectResources` :

```powershell
$env:OPENFORESIA_FORCE_SUBSTATE = "7"
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER -Seconds 10 -Shot
Remove-Item Env:OPENFORESIA_FORCE_SUBSTATE
```

Saute directement au sous-état N à l'entrée dans la sélection de personnage, sans traverser les
précédents. Utile pour n'importe quel sous-état bloqué en amont, pas seulement le 7 — même famille
que `OPENFORESIA_TRACE_MENU`/`OPENFORESIA_OPTIONS_BADPATH` déjà dans le code.

---

## 2. Sous-état 6 — ce qui marche, ce qui s'arrête

`CharacterSelectSubState6` (`FUN_005098f0`) est transcrit et confirmé par playtest en jeu normal :
sept lignes de statistiques en éventail, curseur, dépense/remboursement de points, icône + texte de
description. **`EmitParticle` tourne enfin** (débit ~644 prim/image).

Le chemin **annulation** (`case 0x5d`) est complet et sort l'écran via `state[0] = -1`. Le chemin
**confirmation** (`FinalizeCharacterStats`) s'arrête aux cinq fonctions citées en §1 — tout ce qui
précède (reset de la fiche personnage, copie du portrait) est transcrit et testé.

Détail complet dans `REVERSE_STATUS.md`, section « 2026-08-15 — new machine, then `FUN_005098f0` ».

---

## 3. Sous-état 7 — l'écran de sauvegarde, ce qui marche, ce qui bloque

`CharacterSelectSubState7` (`FUN_0050a910`) est transcrit : machine à phases (init, navigation du
curseur, dix-neuf cas), rendu de fin de fonction. **Confirmé sans plantage jusque dans le rendu**
via le hook du §1 — init, calcul du port, machine à phases : rien à y redire. Deux blocages, tous
deux extérieurs à ce qui a été écrit cette séance :

1. **Control code texte `0x04`** — un gap préexistant (comme le `0x14` trouvé en testant le
   sous-état 6), pas quelque chose d'introduit ici. Rapide à corriger : lire son gestionnaire dans
   `Legacy/Blaze.exe` comme les autres codes déjà faits (`0x06`, `0x09`, `0x1c`).
2. **`FUN_0051be90`** — jamais décompilée par Ghidra nulle part dans `Blaze.c` (juste des sites
   d'appel), donc pas un problème de `.asm` incomplet cette fois : il faut un re-run Ghidra à
   signature forcée. Six paramètres cdecl observés : `(otEntry, prim, sprite, value, mode, extra)`.
   Elle est dans la partie **inconditionnelle** du rendu (les cases de stats courantes tournent à
   chaque image, pas seulement en parcourant les cartes), donc bloque tout le rendu de cet écran
   une fois le point 1 réglé.

La sous-couche carte mémoire elle-même (`Ordinal_9/21/22/23/24/25/67/68/69/72/684`, encapsulés dans
`FUN_00504690/6c0/730/770/7e0/820`, `FUN_00504b40/70/90/c20`, `FUN_00512900`, `FUN_005128e0`) n'a
**pas été lue** cette séance — c'est tout un sous-système à part (chargement/écriture réels sur la
carte), volontairement laissé pour une séance dédiée plutôt que refait à la hâte. Un seul point
d'arrêt (`ThrowSaveCardUnresolved`) couvre tous les cas qui la touchent (`1`, `4`, `6`, `0xd`,
`0x10`, `-5`, `-3`/`-2`).

Détail complet, y compris la table de saut (encore un artefact de compilateur, pas une vraie table
de saut — voir §5) et les deux tables statiques de répertoire de sauvegarde (données réelles, pas
des pointeurs vers de la mémoire jamais peuplée) : `REVERSE_STATUS.md`, section « 2026-08-15 —
`FUN_0050a910`: sub-state 7, the memory-card save screen ».

---

## 4. La suite

| sous-état / fonction | note |
|---|---|
| `FinalizeCharacterStats` (§2) | 5 fonctions à lire — le plus direct pour rendre le sous-état 6 traversable en jeu normal |
| Control code `0x04` (§3) | rapide, même recette que `0x06`/`0x09`/`0x1c` |
| `FUN_0051be90` (§3) | a besoin d'un re-run Ghidra à signature forcée, pas de lecture de `.asm` |
| Sous-état 2 (`FUN_00508410`, 146 lignes) | retour arrière depuis le 3, pas urgent |
| Sous-couche carte mémoire | grosse pièce à part, séance dédiée |

`InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact pour tout sous-état
pas encore câblé.

---

## 5. Comment tester — `test-protocol.md`

**Lire `test-protocol.md` avant tout lancement.** Pour atteindre le sous-état 6 en jeu normal :

```powershell
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER -Seconds 15 -Shot
```

Neuf `ENTER` avec `WAIT2` entre chaque : nom, style de langage, élément, puis le tirage des trois
dés. **Le minutage est sensible au hasard du lancer de dés** — si la capture montre encore l'écran
des dés ou un plantage sur un control-code texte (0x14, 0x04, ...), relancer ou ajuster les `WAIT`.

Pour exercer les entrées du sous-état 6 une fois arrivé : ajouter
`DOWN,WAIT1,DOWN,WAIT1,RIGHT,WAIT1,RIGHT,WAIT1,UP,WAIT1,LEFT` à la suite.

Pour le sous-état 7, ou tout sous-état bloqué en amont : voir le hook `OPENFORESIA_FORCE_SUBSTATE`
au §1.

Si le script ne couvre pas un cas, **l'étendre** et le noter dans `test-protocol.md`, ne pas le
contourner.

---

## 6. Re-run Ghidra à signature forcée

Deux scripts, **jamais commités** — seul leur code source est préservé dans `REVERSE_STATUS.md`
(§"The re-run" et §"All eight handlers re-decompiled") :

| script | rôle |
|---|---|
| `ForceDecompileSubStates.java` | les huit handlers de sous-état, signature `uint* h(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)` |
| `ForceDecompileParticlePass.java` | une fonction seule, signature à cinq paramètres (gabarit à adapter — `FUN_0051be90` en aurait besoin à six) |

**Sur cette machine, `~/ghidra_scripts` n'existe pas** : recopier les deux scripts depuis
`REVERSE_STATUS.md` avant le premier lancement. Ghidra est installé sous
`D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC` (pas `F:\`, chemin de l'ancienne machine).

```powershell
$gh = "D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "D:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "$env:USERPROFILE\ghidra_scripts" `
      -postScript ForceDecompileSubStates.java
```

**Ce que ça apporte et ce que ça n'apporte pas** :

- ✅ le prototype et l'arité, donc `pad[3]` au lieu de `*(byte *)(in_stack_00000010 + 3)` ;
- ❌ **les locaux de pile**. Pour ceux-là il faut lire le `.asm` — **sauf si le `.asm` lui-même est
  incomplet** (arrivé une fois sur deux cette séance) ou **si Ghidra n'a jamais rien décompilé du
  tout pour la fonction** (`FUN_0051be90`, cas différent des deux autres) : dans ces cas, un re-run
  à signature forcée est le seul levier, la lecture manuelle ne peut rien apporter de plus.

---

## 7. Pièges de méthode

**Compter les `PUSH` avant de lire un `[ESP+n]`, et vérifier via le nombre de `POP` de
l'épilogue.**

**Les emplacements d'arguments sont réutilisés comme scratch** ; les noms de variables de Ghidra
dans la queue d'une fonction ne valent rien, même signature forcée.

**Une boucle qui parcourt un tableau depuis un champ intérieur décale tous les `[reg + n]`.**

**Un amas de `DAT_` dans l'étendue d'un enregistrement connu est un champ, pas une globale** —
soustraire la base déjà déclarée la plus proche avant de déclarer quoi que ce soit.

**Deux `DAT_` qui ne diffèrent que de quelques octets ne sont pas forcément un artefact du
décompilateur.** `DAT_005e97c0`/`005e97c4` avaient l'air d'un aliasing Ghidra classique — un `grep -c`
sur tout `Blaze.c` a montré que ce sont deux globales réelles et distinctes. **Le réflexe « ça sent
l'artefact » se vérifie par un grep pangénérique, pas par un haussement d'épaules.**

**Un « `switch` que Ghidra n'a pas su normaliser » n'est pas forcément un vrai problème.** Rencontré
deux fois cette séance (sous-états 6 et 7) : dans les deux cas la table de saut à deux niveaux
n'était qu'une implémentation de compilateur pour un switch clairsemé, et **le découpage en `case`
que Ghidra affiche dans le C reste le vrai contrôle de flux**. Vérifier en lisant la table
directement dans le binaire avant de supposer que le C ment.

**Une case qui ne fait que poser une étiquette puis `break` cache souvent un morceau de code
partagé après le switch** (l'idiome `uVar17 = X; break;`). Fusionner ces cases à l'œil sans relire
chacune contre le C original fait perdre ce morceau partagé (ici : l'incrément de phase, oublié
sur quatre cases sur six au premier jet). Revérifier chaque case fusionnée une par une.

**Une faute native (`0xC0000005`) dans un appel winsys3d est une question d'arité avant tout.**

**Une fonction winsys3d sans export et sans appelant dans sa propre décompilation est nourrie par
la procédure de fenêtre de la DLL**, que ce portage ne fait pas tourner.

**Quand la géométrie est bonne mais l'image fausse, regarder l'écran, pas les chiffres.**

---

## 8. Réserves connues (à regarder si l'image est fausse plutôt qu'absente)

- **Chemin MMX non transcrit** dans `DrawAnimatedModel`.
- **Quirk des pages de texture**, deux occurrences dans `DrawAnimatedModel`/`EmitParticle`.
- **Curseur de grille du clavier virtuel** : un disque sombre masque la première case.
- **`DAT_00562c9c`** : comparaison morte (`TODO(REVERSE)` posé).
- **Boîtes de dialogue modales** : `DAT_00560fef`, `DAT_0056075b`, `DAT_00560763`, `DAT_005e909f`
  lisent 0 — toutes les confirmations passent directement. Sous-états 5, 6 et 7 ont chacun plusieurs
  branches de cette famille, transcrites mais mortes de la même façon.
- **Contrôle-flux texte** : codes `0x14` et `0x04` non individuellement reconstruits — voir §3.

---

## 9. Bugs du jeu d'origine — `legacy-bug.md`

Une bizarrerie constatée dans OpenForesia peut être la transcription **fidèle** d'un défaut de 1998.
Vérifier `legacy-bug.md` avant de corriger quoi que ce soit.

Première entrée (LB-001) : sortir de la saisie du nom par **Échap** bloque définitivement l'écran au
clavier. Reproduit à l'identique dans le portage.

---

## 10. Rappels d'outillage — nouvelle machine

- Le `dotnet` du PATH (`C:\Program Files (x86)\dotnet`) porte déjà un SDK **x86** (9.0.317) :
  `dotnet build`/`dotnet run --project OpenForesia` marchent tel quels, **aucun** `DOTNET_ROOT` à
  poser. `playtest.ps1 -Build` détecte ce cas automatiquement.
- `OpenForesia.Tools` tourne directement aussi.
- Ghidra est sous `D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC`, pas `F:\`.
- `~/ghidra_scripts` est vide sur cette machine — voir §6 avant d'en avoir besoin.

**Lire le binaire ne demande aucun outil.** Conversion adresse → décalage fichier :

| section | VA | décalage |
|---|---|---|
| `.text` | `0x00401000` | `0x000400` |
| `.rdata` | `0x0054D000` | `0x14B800` |
| `.data` | `0x0054F000` | `0x14D600` |
| `.idata` | `0x00608000` | `0x16FA00` |
| `.rsrc` | `0x0060A000` | `0x170C00` |

`dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe <va> <taille>` lit directement une
adresse Ghidra — plus simple que `od` sur cette machine puisque l'outil tourne nativement. Utilisé
cette séance pour la table de saut de `FUN_0050a910` (`0x50bea8`, 105 octets) et ses deux tables de
répertoire de sauvegarde (`0x562b08`, `0x562adc`).

**Éditer `Blaze.cs` depuis bash.** UTF-8 avec BOM, et les heredocs de ce shell n'honorent pas
toujours le délimiteur quoté. Passer par l'outil d'édition, ou par un fichier temporaire écrit
directement.

**Avant de déclarer un symbole, greper son adresse — sur `Blaze.c` en entier, pas seulement sur ce
qui est déjà dans `Blaze.cs`.** Plusieurs doublons créés faute de le faire dans des séances passées.

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
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # 29 — ce qui lève à l'exécution
```

Les seconds sont les vrais murs : chacun porte un message qui nomme précisément ce qui manque, et
c'est en général la meilleure entrée en matière quand on ne sait pas par quoi commencer.
