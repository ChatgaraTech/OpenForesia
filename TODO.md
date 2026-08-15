# TODO

Point d'entrée entre deux sessions. État du 2026-08-15.

---

## Où on en est

**L'écran de sélection des personnages fonctionne.** Atteint depuis le menu, 2400+ images sans
faute, et la capture montre le carrousel des seize portraits, le modèle animé, le panneau
« Classe / Guerrier » et la bannière « Sélectionne un personnage. ».

Chaîne reconstruite :

```
RunGameStateTick case 4
  └─ RunCharacterSelectState
       ├─ LoadCharacterSelectResources     OK
       └─ RunCharacterSelectFrameLoop      OK (sauf le bloc « en scène », sous-états 1 à 6)
            └─ CharacterSelectSubState0    OK, dessine
                 ├─ DrawAnimatedModel      OK
                 └─ ProjectPoint           OK
```

`DrawPrimitive` monte d'environ **382 primitives par image**, régulier. Build vert (0 erreur).
Rien n'est commité — c'est l'affaire de Vincent.

---

## 1. À faire maintenant : les sous-états 2, 6, 7

Les sous-états **3** (style de langage), **4** (carrousel des éléments) et **5** (tirage du bonus,
trois cartes) sont reconstruits et tournent. La chaîne va donc 0 → 1 → 3 → 4 → 5 → *6*.
Reproduction :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Then ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT2,ENTER -Seconds 12 -Shot
```

Le sous-état 2 (143 lignes) n'est atteint qu'en retour arrière depuis le 3. Les 6 et 7 font 545 et
713 lignes : ce ne sera pas une seule séance.

**Atteindre le 6 exercera enfin `EmitParticle`** (`FUN_00520ae0`), transcrit mais jamais exécuté :
la passe de particules est lancée par le bloc « en scène » de la boucle de frame, qui vient **après**
le dispatch — donc tant qu'un sous-état lève, elle ne tourne pas.

**Outil décisif pour la suite** : `~/ghidra_scripts/ForceDecompileSubStates.java` re-décompile les
huit handlers avec leur vraie signature forcée. Ça confirme l'arité, mais **ça ne récupère pas les
locaux de pile** — les descripteurs de sprite de 20 octets sortent encore en `&stack0xffffffdc` et
demandent le listing.

## 1bis. Rappel de méthode pour les suivants

**Quand une boucle parcourt un tableau d'enregistrements depuis un champ intérieur, tous les
`[reg + n]` de son corps sont décalés d'autant.** Deux erreurs coûtées de cette façon : la base du
tableau de particules (fausse de 0x1c) et `MOV ECX,[ESI + 0x10]` du sous-état 4, où `ESI` vaut
`icône + 0x28` — c'est donc `+0x38`, le mot d'attribut. Lu à `+0x10` il vaut zéro et les huit
médaillons sortaient en **disques noirs**, sans que les compteurs ne bougent d'un poil. Seule la
**Compter les `PUSH` avant de lire un `[ESP+n]`.** `MOV [ESP+0x24],AX` après un `PUSH` vise
`clean + 0x20`, pas `clean + 0x28`. Se tromper là faisait croire que le sprite du sous-état 5
chevauchait la zone des arguments et qu'une écriture était morte. Le nombre de `POP` de l'épilogue
fixe le cadre sans ambiguïté — c'est la vérification qui coûte le moins.

capture l'a montré : quand la géométrie est bonne mais l'image fausse, regarder l'écran, pas les
chiffres.

Le bloc « en scène » et le **sous-état 1** (la saisie du nom) sont reconstruits et tournent : le
clavier virtuel s'affiche, la boîte « Nom » aussi, ~593 primitives par image. Reproduction :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Then ENTER,WAIT3,ENTER -Seconds 12 -Shot
```

Ce qui reste :

1. **Les sous-états 2 à 7**, tous décompilés dans `Legacy/Raw/Blaze/FUN_*.c`.
   `InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact. Le sous-état 1
   confirme la méthode : la signature de Ghidra est fausse à chaque fois, il faut la reprendre au
   prologue (`SUB ESP,n` + les `PUSH`, puis les décalages observés dans le corps).
2. **`FUN_00520ae0`**, la passe de particules — atteinte aux seuls sous-états 5 et 6, donc pas
   bloquante avant. C'est le seul point des trois pièces qui demande vraiment un **re-run Ghidra
   avec la signature forcée à cinq arguments** : l'assembleur prouve que les cinq emplacements sont
   lus, Ghidra n'en déclare que trois et rend le curseur de primitives en `unaff_retaddr`.
3. **Le sprite de curseur de la grille** dessine un disque sombre sur la première case. Position et
   texture sont transcrites telles que l'assembleur les construit ; à regarder en premier si le
   surlignage de la grille est faux.

Si l'image paraît fausse plutôt qu'absente, les deux réserves connues sur `DrawAnimatedModel` sont
en §3.

---

## 2. Ce qui a été corrigé (pour mémoire)

**La file de caractères tapés est reconstruite, pas appelée dans la DLL.** C'est la seule déviation
de ce genre du portage, et elle était bloquante : `FUN_00a03f50`, qui remplit la file que
`GetKBChar` dépile, **n'est pas exportée** (aucune des 830 entrées d'export ne porte son RVA
`0x3f50`) — seule la procédure de fenêtre interne de winsys3d l'appelle, et notre fenêtre est la
nôtre. La saisie du nom coupant par ailleurs la traduction manette→clavier, l'écran n'avait
**aucune entrée du tout**. Règle à retenir : *une fonction winsys3d sans export et sans appelant
dans sa propre décompilation est nourrie par la procédure de fenêtre de la DLL*, que ce portage ne
fait pas tourner.

**Le code de contrôle `0x1c` du moteur de texte** — `FUN_00544e80`, décodé à la main : un saut de
sous-chaîne qui empile un retour à `+0x8b4` et substitue une valeur prise dans le bloc de 0x2000 du
joueur. Le dépilement était déjà reconstruit, seul l'empilement manquait.

**La base du tableau de particules était fausse de 0x1c** : c'est `0x00572be4`, pas `0x00572c00` —
la boucle de frame manipule le champ de position (+0x1c), pas la base. `0x00572b48 + 0x9c =
0x00572be4` : le tableau commence exactement où finit l'entité de scène.

**Un amas de `DAT_` dans l'étendue d'un enregistrement connu est un champ, pas une globale.** Deux
listes de « globales encore à modéliser » se sont dissoutes par simple soustraction : les
`DAT_00572b4c..74` sont les champs de `s_SceneEntity_00572b48`, les `DAT_00578df0..e00` sont
`s_SelectState_00578de8[2..6]`, les `DAT_0057351c..3c` sont ceux du premier descripteur de
`s_SelectSlots_00573518`. Soustraire la base déjà déclarée la plus proche avant de déclarer quoi
que ce soit — c'est la vérification qui coûte le moins.

**Les deux poseurs de table de largeurs de glyphes ont enfin un site d'appel.** `FUN_00507870`
(fixe) et `FUN_00507890` (proportionnel) étaient notés « aucun site d'appel dans `Blaze.c` ». Le
sous-état 1 appelle les **deux** : fixe juste avant la grille de caractères, proportionnel juste
après — il faut des cases de largeur égale pour qu'elles s'alignent. La table est donc mutable, pas
la constante qu'on en avait fait.

**Le code de contrôle `0x0f` du moteur de texte.** Valider un personnage relie l'étiquette d'état à
un autre flux, qui ouvre sur `0x0f` — un des 26 gestionnaires que Ghidra n'a jamais émis. Le garde
du projet conseille un re-run ciblé, mais le corps ne fait que **33 octets** : décodé à la main
comme les talons de la passe 1 et `0x00544b10`. Il signifie « arrête-toi ici et montre le curseur ».
Au passage, il **confirme** la lecture du matin : l'octet qu'il écrit (`+0x33`) est le haut du dword
de fonction du curseur, c'est-à-dire son bit d'activation `0x1000000`. Détail dans
`Documentation/DataSymbols.md`, avec la conversion adresse `.text` → offset fichier.

**Ordinal 795 était déclaré avec deux arguments et en prend trois.** C'était le plantage suivant,
une violation d'accès native (`0xC0000005`) au premier modèle dessiné par le carrousel :
`FUN_00a226c0(vertices, outScreenXy, count)`. En cdecl, l'argument manquant se lit sur la pile de
l'appelant et la DLL écrit à travers. Le détail — dont pourquoi le pseudo-C se lit comme une
fonction à deux arguments si on rate l'échange dans son chemin scalaire — est dans
`Documentation/DLL/winsys3d.md`.

C'est la **troisième** fois que la récupération de paramètres de Ghidra est fausse sur un appel de
cet écran (`FUN_0052a1e0` et `FUN_0052a280` étaient les deux autres). Règle à retenir : *une faute
native dans un appel winsys3d est une question d'arité avant d'être autre chose.*

**Les descripteurs d'interface sont alloués bas.** Le plantage `FUN_005443a0 masks its descriptor
to 24 bits` n'était que le symptôme visible : un descripteur **porte sa propre ordering table à
+0xa4**, que `ClearOTagR` remplit de liens masqués à `0xffffff`. Trois symboles convertis à
`AllocLowZeroed`, en propriétés paresseuses et réclamés d'avance dans
`TryPrepareDisplayListArena` : `s_UiDescriptors_00560ff0`, `s_StatusDescriptor_00560728`,
`s_SelectSlots_00573518`.

Six des neuf symboles listés hier soir **n'en avaient pas besoin** — le détail est dans
`REVERSE_STATUS.md`, entrée du 2026-08-15. La règle utile : « est-ce une structure `0x8c8` qui
traverse `FUN_005443a0` / `FUN_00545340` ? », pas « est-ce proche du code de liste d'affichage ? ».

**`BuildUiDescriptors` n'écrivait que 8 de ses 26 dwords.** Il manquait `f8` (mot de fonction
« texte ») et `f16` (« cadre ») ; tous les tests de `DrawUiPanel` étant `champ & 0x1000000`, le
panneau se serait dessiné vide juste après avoir corrigé le plantage. `BuildStatusDescriptor`
avait le même trou d'un dword (`f25`). Les quatre instances sont relues du binaire et **diffées
mécaniquement** contre `od` : 78/78 et 26/26 identiques. Table complète dans
`Documentation/DataSymbols.md`.

**Le gel de `DrawPrimitive` n'était pas un défaut.** `ShimReport` sort toutes les 60 images, et la
boucle mourait à la **première** frame du sous-état 0 : les soixante images muettes étaient celles
du chargement, pas du rendu. Re-mesuré depuis : ~382 primitives par image, régulier. Aucune trace
sur `EmitModelQuads` n'a été nécessaire.

---

## 3. Les deux réserves connues sur `DrawAnimatedModel`

À garder en tête si l'image est fausse plutôt qu'absente :

- **Chemin MMX non transcrit.** Seul le chemin scalaire du produit de matrices est écrit ; l'original
  branche sur `MMXInstalled_exref`. L'équivalence des deux n'a **pas** été vérifiée opérande par
  opérande. `TODO(REVERSE)` posé sur place.
- **Quirk des pages de texture.** `tpageWord` et `uvWord` sont calculés une seule fois depuis le
  **premier** modèle du paquet, alors que chaque partie relit ses propres attributs. C'est dans
  l'original, transcrit tel quel — mais si les parties sortent avec la mauvaise texture, c'est là
  qu'il faut regarder d'abord.

---

## 4. Ensuite : le reste de l'écran

- **`FUN_00520ae0`** + le bloc de globales `0x00572b40` — c'est tout ce qui manque au bloc
  « en scène » de la boucle de frame (sous-états 1 à 6).
- **Les sept autres sous-états** (1 à 7). Tous décompilés, dans `Legacy/Raw/Blaze/FUN_*.c`.
  `InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact.
  Attention : dès qu'un d'eux passe une structure de `DAT_00573518` au moteur de texte, elle est
  déjà allouée bas — c'était le but de la conversion préventive.
- **`FUN_005066c0`** — pointeur de code trouvé dans `DAT_005606f8`, absent de `Blaze.c`. S'il est
  appelé, ça se verra ; il faudra alors un re-run Ghidra ciblé de plus.

---

## Rappels d'outillage (ils ont coûté du temps)

**Lancer le jeu.** `dotnet run --project OpenForesia` **ne marche pas** depuis le shell : l'app est
en x86 et le SDK sur le PATH n'a que le runtime x64. Lancer l'exe directement, il trouve le
runtime x86 (9.0.8) de `C:\Program Files (x86)\dotnet` :

```
OpenForesia/bin/Debug/net9.0/OpenForesia.exe
```

Même cause pour `OpenForesia.Tools`, qui ne tourne pas du tout. Palliatif :
`OpenForesia.Tools/exports.ps1`, un parseur de table d'exports PE en PowerShell.

**Tester le jeu : voir `test-protocol.md`**, à la racine. Un seul point d'entrée :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 25 -Shot
```

Il construit, lance, navigue jusqu'à l'écran voulu, capture et rend un verdict. Tout ce qui a été
appris sur le pilotage du jeu (DirectInput, cadence, boucle fermée sur la trace du menu) est dans ce
fichier et dans le script — ne pas le réinventer, l'étendre si besoin.

**Lire le binaire sans outil.** `od -A x -t x4 -j <offset> -N <taille> Legacy/Blaze.exe` suffit pour
relever une table de `.data`, et se diffe contre les constantes du source. C'est ce qui a établi
les 26 dwords ce matin.

**Éditer `Blaze.cs` depuis bash.** Le fichier est en UTF-8 avec BOM. Les heredocs de ce shell
n'honorent pas toujours le délimiteur quoté : les backticks y sont évalués et les `\xNN` d'un
heredoc simple-quoté ne le sont pas. Passer par un fichier temporaire écrit directement, ou par
l'outil d'édition, plutôt que par `cat <<EOF`.

**Re-run Ghidra.** Les scripts `.py` de `~/ghidra_scripts` sont morts (Ghidra 12 rétrograde Jython
en extension non installée). Utiliser `~/ghidra_scripts/ForceDecompileCharacterSelect.java`,
**depuis PowerShell** (le `.bat` casse son propre chemin sous Git Bash), avec `-readOnly` :

```powershell
$gh = "F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "F:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "C:\Users\Sagara\ghidra_scripts" `
      -postScript ForceDecompileCharacterSelect.java
```

Le script crée **toutes** les fonctions manquantes avant d'appeler `analyzeChanges` puis de
décompiler. Ne pas inverser : sans la passe d'analyse, les corps sortent tronqués et pleins de
`func_0x005…`.

**Avant de déclarer un symbole, greper son adresse.** Trois doublons créés en trois séances
(`DAT_00564a48`, ordinaux 450/451, `DAT_005e9818`). Le fichier a dépassé la taille où on peut s'en
passer.

**Quand le pseudo-C fait de l'arithmétique de pointeur, aller voir l'assembleur.** Deux pièges
évités de cette manière : le seuil de profondeur (`-0x50` sur un `uint*` = `-0x140` octets) et
le pas des manettes (`* 4` dans le pseudo-C, `SHL 4` dans le listing).

---

## Bugs du jeu d'origine

`legacy-bug.md` à la racine. Une bizarrerie constatée dans OpenForesia peut être la transcription
**fidèle** d'un défaut de 1998 — à vérifier là avant de corriger quoi que ce soit. Première entrée :
sortir de la saisie du nom par Échap bloque tout l'écran de sélection, au clavier.

## Contexte accumulé

Tout le détail est dans `OpenForesia/src/Blaze/REVERSE_STATUS.md`, sections datées des 2026-08-14
et 2026-08-15 — la carte du banc de registres GTE, la structure de `FUN_0051d1f0`, les signatures
reconstruites depuis l'assembleur, et l'analyse des deux défauts corrigés ce matin.
`Documentation/DLL/winsys3d.md` a les familles libgpu / libgte / registres GTE.
`Documentation/DataSymbols.md` a les tables relevées dans le binaire, dont les 26 dwords du
descripteur d'interface.

Change OpenSpec close et validée : `openspec/changes/finalize-character-select-loading/`.
Le travail depuis n'est pas couvert par une change — à décider s'il en faut une.
