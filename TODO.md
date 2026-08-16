# TODO

Point d'entrée entre deux sessions. État au 2026-08-16, quatrième passe de la journée. Trois passes
précédentes (rendu complet du sous-état 7, régression du carrousel) **commitées par l'utilisateur**.
Cette passe : début de la sous-couche carte mémoire — **ce n'est pas une émulation de carte PS1,
juste des fichiers Win32 ordinaires dans un dossier `save\`**. Le cas 1 (ouverture pour charger) est
transcrit et confirmé en jeu ; le cas 6 (écriture, gros morceau — en-tête + icône VRAM + somme de
contrôle) reste à faire. Le build est vert. Détail complet en §2bis.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée. Consigné en mémoire
pour ne plus le proposer.

Non commité à ce stade : `TODO.md`, `REVERSE_STATUS.md`, et les changements dans `Blaze.cs` (dix
P/Invoke `Ordinal_9/21/22/23/24/25/67/68/69/72`, `SetCardPathDeviceFields`/`SetCardFileName`/
`ResetCardTransferStatus`/`ResetCardReadEvents`/`ResetCardWriteEvents`/`PollCardWriteStatus`/
`PollCardReadStatus`/`PollCardTransfer`/`RetryOpenCardFile`/`RetryCloseCardFile`/`CardSlotSelector`,
nouveau champ `s_CardSlotIndex_00604980` + correctif du case 0 qui écrivait dans le mauvais champ,
case 1 de `StepSubState7Phase` transcrit, sondage de transfert en tête de fonction transcrit).

---

## Où on en est

**La chaîne complète 0→6 se traverse en jeu normal, confirmation de personnage incluse.** Le
sous-état 7 est atteint pour de vrai, et **son rendu se termine désormais sans exception ni
plantage** — vérifié en jeu, chaîne normale complète comme via le hook. Le seul blocage restant sur
cet écran n'est plus dans le rendu : c'est la logique de phase de la sous-couche carte mémoire
(`ThrowSaveCardUnresolved`), qui se déclenche systématiquement à la deuxième image (le compteur de
phase passe de 0 à 1 dès la première). Voir §2.

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
6  répartition des points de stats       OK — confirmation incluse
│
7  écran de sauvegarde/chargement        rendu terminé ; bloque à la 2e image, carte mémoire (§2)
```

Le sous-état 2 n'est atteint qu'en retour arrière depuis le 3.

Débit de rendu par écran, utile comme repère de non-régression :

| écran | primitives / image |
|---|---|
| menu principal | ~110 |
| carrousel des personnages | **382,3 mesuré, confirmé visuellement** (portraits inclus) après les trois correctifs de cette séance (`s_Unknown_00564a48`, `s_Unknown_00564a4c`, `s_SmallTextureFlag`→`s_Unknown_00564a4c` dans `DrawSpritePrimitive`/`DrawFlatPrimitive`) — colle exactement à la référence historique |
| saisie du nom | ~593 |
| style de langage | ~555 |
| carrousel des éléments | ~433 (pas re-mesuré ni re-capturé — même famille de correctifs applicable, à vérifier visuellement, pas seulement par le chiffre) |
| tirage du bonus | ~429 (idem) |
| répartition des points | ~644 |
| sauvegarde | pas mesuré — la logique de phase (pas le rendu) plante à la 2e image (§2) |

---

## 1. Sous-état 6 — `FinalizeCharacterStats` complet

Les cinq fonctions bloquantes (`FUN_00538ab0`, `538f50`, `538c50`, `538e20`, `538fa0`) sont toutes
transcrites : commit du bloc de stats, mise à l'échelle par difficulté (identité pour l'instant,
rien ne pose de palier), calcul de huit statistiques dérivées, bonus d'équipement (deux des cinq
emplacements toujours vides avec l'équipement par défaut), plafonnement final. La somme de contrôle
à sept caractères qui restait derrière l'exception (jamais transcrite faute d'être atteignable) est
maintenant écrite aussi.

**Confirmé en jeu normal** : neuf `ENTER` avec le hook `OPENFORESIA_FORCE_CONFIRM_STATS=1` (voir
§3) déclenchent `FinalizeCharacterStats` sans exception, et la pile d'appel du crash suivant montre
`RunCharacterSelectFrameLoop → CharacterSelectSubState7` — la transition sous-état 6 → 7 passe
désormais par le dispatcher normal, pas par un hook de saut.

**Non élucidé, volontairement laissé de côté** : quelle touche physique pose le bit de confirmation
réel (`pad+2 & 0x8000`) en jeu, et combien de points le lancer de dés laisse à dépenser avant que
la confirmation ne soit acceptée. Le hook `OPENFORESIA_FORCE_CONFIRM_STATS` contourne les deux ;
tracer `FoldKeyboardIntoPadState` réglerait la question si `playtest.ps1` en a besoin un jour.

Détail complet, fonction par fonction : `REVERSE_STATUS.md`, section « 2026-08-15 —
`FinalizeCharacterStats`'s five functions, one at a time ».

---

## 2. Sous-état 7 — l'écran de sauvegarde : le rendu est terminé

**Cinq choses réglées cette séance, dans l'ordre où elles se sont débloquées l'une l'autre :**

1. **Control code texte `0x04`** — `Handler_005449b0`, décodé à la main depuis `Legacy/Blaze.exe`,
   même recette que `0x06`/`0x09`/`0x1c`.
2. **Control code texte `0x01`** — entièrement décompilé par Ghidra (`Blaze.c:91274`,
   `FUN_00544810`). Un deuxième chemin d'icône-dans-texte, indépendant de `FUN_0051be90`.
3. **Gabarit du fichier de vérification CD** (TODO #2, ancien) — **résolu**, pas contourné. Le CD
   réel monté (`E:`) : son fichier racine `cd.idx` fait **exactement 671 088 640 octets =
   0x28000000**, confirmant que le `return` de `FUN_00544810` (`(DAT_0056321c + 1) * (param_1 + 3)`)
   est bien une protection anti-copie de 1998 — une image authentique fait tomber la multiplication
   sur l'identité. Le gabarit lui-même relu directement dans `Legacy/Blaze.exe` (`DAT_00563280`,
   décalage fichier `0x161880`) : `"@:\cd.idx"`. `cdCheckPath` dans `RunApplication` en est
   maintenant rempli.
4. **`s_Unknown_00564a48`** — doublon mort de `DAT_00564a48` à côté de `s_DeviceCapFlag_00564a48`
   (`bool`, câblé correctement). Neuf sites d'appel (`DrawAnimatedModel`, `EmitParticle`, voisins de
   `DrawUiPanel`) testaient le mauvais champ et prenaient toujours la branche « désactivée ».
   Fusionné sur `s_DeviceCapFlag_00564a48`. **A réveillé une vraie régression du carrousel, en deux
   couches :**
   - `s_Unknown_00564a4c` (une garde de réentrance, pas un drapeau — sa valeur stable dans `Blaze.c`
     est `1`, jamais `0`) restait à son défaut C# de `0`, ce qui faisait sauter le dessin de tout
     modèle 3D au lieu de simplement lui nettoyer un bit d'attribut. Corrigé (`= 1`).
   - **Insuffisant à lui seul** : le débit de primitives (~350/image) collait déjà à peu près à la
     référence historique (~382/image), ce qui a fait conclure trop vite que c'était réglé. C'est
     une capture du **jeu original**, fournie par l'utilisateur après un deuxième désaccord, qui a
     montré ce qui manquait encore : les cartes-portrait des 15 autres personnages, en éventail
     derrière le modèle 3D. `DrawSpritePrimitive`/`DrawFlatPrimitive` utilisaient `s_SmallTextureFlag`
     (réglage ini « Small », `0` par défaut) au lieu de `s_Unknown_00564a4c` pour leur garde
     secondaire — confirmé octet pour octet contre `Blaze.c:69625-69638`/`68565-68569`.
     `DrawSpriteVariant`, la fonction sœur, avait déjà le bon symbole ; un TODO déjà posé signalait
     la divergence sans trancher lequel des deux avait tort. Corrigé aux trois sites concernés.

   Confirmé **visuellement, capture contre capture**, pas seulement par un chiffre qui colle : les
   15 portraits sont maintenant à l'écran, et le débit tombe pile sur 382,3/image. Détail complet et
   raisonnement (y compris la méthode d'élimination par instrumentation temporaire) :
   `REVERSE_STATUS.md`, sections « An unrelated visible change... » et « The portrait cards — a
   second, unrelated bug... » ; mémoire dédiée mise à jour en conséquence.
5. **`FUN_0051be90` / `EmitCardGlyphTile`** — **n'avait pas besoin d'un re-run Ghidra.** La note
   précédente (« jamais décompilée ») était fausse — le corps est à `Blaze.c:66710`, complet, juste
   raté par une recherche antérieure dans les 98 000 lignes du fichier. Dessine un nombre décimal en
   tuiles numériques (pas via le moteur de texte) ; tout ce qu'elle appelle
   (`EmitViewportRect`/`FUN_0051be20`, `DrawFlatPrimitive`/`FUN_0051f9c0`,
   `EmitClipRect`/`FUN_0051bdb0`) était déjà reconstruit. Transcrite d'un coup.

   En la testant, un **second bug latent** est apparu (préexistant, pas introduit ici) :
   `RenderSaveScreen` posait `*(uint*)box = 0x566c30;` — une **adresse mémoire brute de 1998**
   utilisée comme pointeur, invalide dans ce port. Relu les 8 vrais octets dans `Legacy/Blaze.exe`
   (décalage `0x165230`) et créé `s_NumeralTileRecord_00566c30`, un vrai buffer, à la place du
   littéral, aux trois sites d'appel.

**Résultat en jeu, confirmé deux fois (hook et chaîne normale complète)** : `RenderSaveScreen`
s'exécute maintenant **entièrement sans exception ni plantage natif**. Le seul blocage restant
n'est plus le rendu : `CharacterSelectSubState7` appelle `StepSubState7Phase` avant le rendu à
chaque image, et son compteur de phase passe de 0 à 1 dès la première image — donc la **deuxième**
image tombe systématiquement sur `case 1` de la sous-couche carte mémoire
(`ThrowSaveCardUnresolved`), le même point d'arrêt documenté depuis la toute première séance sur ce
sous-état, jamais dans le rendu lui-même.

La sous-couche carte mémoire a commencé à être lue cette séance — voir §2bis. Un seul point d'arrêt
(`ThrowSaveCardUnresolved`) couvre encore tous les cas non traités.

Détail complet : `REVERSE_STATUS.md`, sections « 2026-08-15 — `FUN_0050a910`: sub-state 7, the
memory-card save screen », « 2026-08-16 — control codes `0x04` and `0x01`... », « 2026-08-16 — same
day, continued: the copy-protection hypothesis confirmed... » et « 2026-08-16 — same day, third
pass: `FUN_0051be90` needed no Ghidra re-run at all... ».

---

## 2bis. La sous-couche carte mémoire — pas une émulation PS1, des fichiers Win32 ordinaires

**Ce n'est pas ce que son nom laisse penser.** `winsys3d.c` (ordinaux 9/21/22/23/24/25/67/68/69/72)
montre de simples wrappers `CreateFileA`/`CloseHandle`/`SetFilePointer`/`ReadFile`/`WriteFile` sur
un dossier **`save\`** — le port PC n'émule aucune vraie carte mémoire. Le gabarit de chemin
(`DAT_00563158`) relu directement dans `Legacy/Blaze.exe` (décalage `0x161758`) : deux chaînes,
`"save\"` et `"Blaze-Saving*"`.

**Un vrai piège d'arité, attrapé avant d'écrire le P/Invoke plutôt qu'après un plantage natif** :
les ordinaux 67/68/69/72 partagent tous un seul corps que Ghidra décompile en `(void)`, alors que
chaque site d'appel lui passe un argument. Lu les octets bruts directement dans `winsys3d.dll`
(`OpenForesia.Tools -- at winsys3d.dll <va>`) avant de faire confiance à Ghidra : les quatre font
`RET 4` — un vrai paramètre stdcall, juste ignoré par le corps. Confirmé aussi : 67/68/69 rendent
toujours 1, et 72 (le talon partagé par ~50 ordinaux) rend toujours 0 — ce qui dégénère tout le
mécanisme de statut de transfert asynchrone en « toujours prêt », puisque rien dans ce port ne crée
le worker asynchrone qui l'utiliserait pour de vrai (`FUN_00a04320`/`4390` dans `winsys3d.c`,
jamais appelées).

**Un vrai bug préexistant trouvé en câblant le cas 1** : le case 0 (déjà transcrit, déjà commité)
écrivait dans `s_CardCursorSlot_00604984`, mais la source brute écrit dans `_DAT_00604980` — une
adresse **différente** (4 octets plus tôt), confirmé en comparant au `.c` forcé
(`Legacy/Raw/Blaze/FUN_0050a910.c:131-133`). Le cas 1 a besoin de la vraie valeur de `DAT_00604980`
pendant que `DAT_00604984` est remise à zéro juste avant — les deux champs ne pouvaient pas être le
même. Corrigé : nouveau champ `s_CardSlotIndex_00604980`, case 0 repointé dessus,
`s_CardCursorSlot_00604984` inchangée ailleurs.

**Cas 1 (ouverture pour charger) transcrit et confirmé en jeu.** `OPENFORESIA_FORCE_SUBSTATE=7` +
un `ENTER` : plus de plantage sur le cas 1 — il détecte correctement qu'aucune sauvegarde n'existe
encore (dossier `save\` vide dans ce port) et bascule proprement vers le cas 6, pas encore
implémenté. C'est le blocage suivant, réel, atteint par le vrai dispatcher plutôt que par un ancien
point d'arrêt générique.

**Reste à faire** : cas 6 (écriture — en-tête `"SC"`, nom, titre converti en Shift-JIS, nom de
classe, **icône de sauvegarde capturée depuis la VRAM** via `func_0x00545ac4`/`abe`
(vraisemblablement Ordinal_684, `FUN_00a05f50`, déjà lue et confirmée comme une copie rectangulaire
VRAM→mémoire, pas de l'E/S fichier), somme de contrôle sur `0x1ffc` octets, puis quatre
`FUN_00504c20` seek+écriture+vérif) — un morceau nettement plus gros que le cas 1, volontairement
pas commencé cette passe. Cas 4, 0xd, 0x10, -5, -3/-2 restent aussi derrière
`ThrowSaveCardUnresolved`, chacun plus petit que le cas 6 mais avec sa propre séquence à lire.

Détail complet : `REVERSE_STATUS.md`, section « 2026-08-16 — the memory-card subsystem: not PS1
hardware, just Win32 file I/O — case 1 done ».

---

## 3. La suite

| sous-état / fonction | note |
|---|---|
| Cas 6 de la carte mémoire (§2bis) | le morceau le plus gros restant sur cet écran — en-tête + icône VRAM + somme de contrôle + écriture |
| Cas 4, 0xd, 0x10, -5, -3/-2 de la carte mémoire (§2bis) | plus petits que le cas 6 mais chacun sa propre séquence |
| Sous-état 2 (`FUN_00508410`, 146 lignes) | retour arrière depuis le 3, pas urgent |
| Mappage clavier → `pad+2` (§1) | seulement si on veut se passer d'`OPENFORESIA_FORCE_CONFIRM_STATS` |
| Recapturer carrousel des éléments / tirage du bonus | carrousel des personnages déjà confirmé **visuellement** (382,3, portraits inclus) ; les deux autres écrans à entités multiples n'ont été ni recapturés ni revérifiés — un chiffre qui colle ne suffit pas, voir la mésaventure de la séance précédente |
| Balayer les autres sites `DrawSpritePrimitive`/`DrawFlatPrimitive` | le correctif `s_SmallTextureFlag`→`s_Unknown_00564a4c` peut réveiller d'autres sprites ailleurs qui se taisaient silencieusement pour la même raison |

`InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact pour tout sous-état
pas encore câblé.

---

## 4. Comment tester — `test-protocol.md`

**Lire `test-protocol.md` avant tout lancement.** Pour atteindre le sous-état 6 en jeu normal :

```powershell
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER,WAIT2,ENTER -Seconds 15 -Shot
```

Neuf `ENTER` avec `WAIT2` entre chaque : nom, style de langage, élément, puis le tirage des trois
dés. **Le minutage est sensible au hasard du lancer de dés** — si la capture montre encore l'écran
des dés ou un plantage sur un control-code texte (0x14 au sous-état 6 reste ouvert ; 0x04/0x01 sont
réglés depuis le 2026-08-16), relancer ou ajuster les `WAIT`.

Pour exercer les entrées du sous-état 6 une fois arrivé : ajouter
`DOWN,WAIT1,DOWN,WAIT1,RIGHT,WAIT1,RIGHT,WAIT1,UP,WAIT1,LEFT` à la suite — mais le mappage exact
touche→bit n'est pas fiable (voir §1) ; pour aller jusqu'à la confirmation à coup sûr, préférer :

```powershell
$env:OPENFORESIA_FORCE_CONFIRM_STATS = "1"
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3 -Seconds 15 -Shot
Remove-Item Env:OPENFORESIA_FORCE_CONFIRM_STATS
```

Appelle `FinalizeCharacterStats` directement à la première image du sous-état 6, sans dépendre du
lancer de dés ni de la touche de confirmation réelle.

Pour le sous-état 7 isolément, ou tout sous-état bloqué en amont :

```powershell
$env:OPENFORESIA_FORCE_SUBSTATE = "7"
OpenForesia.Tools\playtest.ps1 -Build -Entry 1 -Then ENTER -Seconds 10 -Shot
Remove-Item Env:OPENFORESIA_FORCE_SUBSTATE
```

Les deux hooks sont dans la même famille qu'`OPENFORESIA_TRACE_MENU`/`OPENFORESIA_OPTIONS_BADPATH`,
déjà dans le code.

Si le script ne couvre pas un cas, **l'étendre** et le noter dans `test-protocol.md`, ne pas le
contourner.

---

## 5. Re-run Ghidra à signature forcée

Deux scripts, **jamais commités** — seul leur code source est préservé dans `REVERSE_STATUS.md`
(§"The re-run" et §"All eight handlers re-decompiled") :

| script | rôle |
|---|---|
| `ForceDecompileSubStates.java` | les huit handlers de sous-état, signature `uint* h(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)` |
| `ForceDecompileParticlePass.java` | une fonction seule, signature à cinq paramètres |

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
  incomplet** (arrivé une fois sur deux dans une séance antérieure) ou si Ghidra n'a vraiment jamais
  rien décompilé pour la fonction : dans ces cas, un re-run à signature forcée est le seul levier.

**`FUN_0051be90` (2026-08-16) était un faux négatif** — noté pendant plusieurs séances comme
« jamais décompilée par Ghidra », alors que son corps complet était bien dans `Blaze.c` (ligne
66710), juste raté par une recherche antérieure dans les 98 000 lignes du fichier. **Toujours
`grep` l'adresse exacte sur `Blaze.c` en entier avant de conclure qu'un re-run est nécessaire** —
un re-run coûte du temps (Ghidra headless, plusieurs minutes) qu'un grep de deux secondes évite.

---

## 6. Pièges de méthode

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

## 7. Réserves connues (à regarder si l'image est fausse plutôt qu'absente)

- **Chemin MMX non transcrit** dans `DrawAnimatedModel`.
- **Quirk des pages de texture**, deux occurrences dans `DrawAnimatedModel`/`EmitParticle`.
- **Curseur de grille du clavier virtuel** : un disque sombre masque la première case.
- **`DAT_00562c9c`** : comparaison morte (`TODO(REVERSE)` posé).
- **Boîtes de dialogue modales** : `DAT_00560fef`, `DAT_0056075b`, `DAT_00560763`, `DAT_005e909f`
  lisent 0 — toutes les confirmations passent directement. Sous-états 5, 6 et 7 ont chacun plusieurs
  branches de cette famille, transcrites mais mortes de la même façon.
- **Contrôle-flux texte** : codes `0x14` et `0x04` non individuellement reconstruits — voir §3.

---

## 8. Bugs du jeu d'origine — `legacy-bug.md`

Une bizarrerie constatée dans OpenForesia peut être la transcription **fidèle** d'un défaut de 1998.
Vérifier `legacy-bug.md` avant de corriger quoi que ce soit.

Première entrée (LB-001) : sortir de la saisie du nom par **Échap** bloque définitivement l'écran au
clavier. Reproduit à l'identique dans le portage.

---

## 9. Rappels d'outillage — nouvelle machine

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
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # 28 — ce qui lève à l'exécution
```

Les seconds sont les vrais murs : chacun porte un message qui nomme précisément ce qui manque, et
c'est en général la meilleure entrée en matière quand on ne sait pas par quoi commencer.
