# Protocole de test

**À lire avant tout test, et à mettre à jour dès qu'un test apprend quelque chose sur la façon de
tester.** Ce fichier existe parce que retrouver comment exercer le jeu coûtait plus de temps, chaque
jour, que le travail de reconstruction lui-même.

---

## La règle courte

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 25 -Shot
```

Puis lire l'image produite (`scratchpad/playtest-ecran.png`). C'est tout. Ne pas réécrire de script
de lancement : celui-ci contient déjà tout ce qui a été appris, et chaque contournement improvisé
l'a été à nouveau à partir de zéro.

Si le script ne couvre pas le cas voulu, **l'étendre**, ne pas le contourner.

---

## Ce que `playtest.ps1` fait

1. Construit si on lui passe `-Build` (bascule sur un SDK connu via `DOTNET_ROOT` seulement si le
   `dotnet` du PATH n'en a pas déjà un — voir le piège d'outillage plus bas).
2. Lance `OpenForesia.exe` en redirigeant stdout et stderr dans `scratchpad/`.
3. Attend le menu principal, prend le focus, **navigue en boucle fermée** jusqu'à l'entrée demandée.
4. Valide, en s'assurant que le jeu a bien vu la frappe.
5. Observe, capture éventuellement la fenêtre en PNG.
6. Tue le processus et rend un **verdict** : plantage (avec sa pile) ou non, et la progression du
   rendu (`DrawPrimitive` par image).

### Paramètres

| paramètre | rôle |
|---|---|
| `-Entry <n>` | entrée du menu principal. `0` Début du jeu · **`1` Sélection des personnages** · `2` Menu objets · `3` Options · `4` Quitter · `-1` rester au menu |
| `-Seconds <n>` | durée d'observation après validation (défaut 25) |
| `-Shot` | capture la fenêtre en PNG à la fin |
| `-Build` | reconstruit la solution d'abord |
| `-Then A,B` | touches à jouer **une fois l'écran atteint**, dans l'ordre. Jetons : `ENTER` `ESC` `UP` `DOWN` `LEFT` `RIGHT` `LCTRL` `START` `SELECT` `LALT` ; `TYPE:texte` et `KBENTER` pour la frappe clavier ; `HOLD:x`/`FREE:x` pour enfoncer et relâcher ; `WAIT<n>` pour patienter n secondes. Exemple : `-Then DOWN,WAIT2,ENTER` |
| `-Hold <jeton>` | touche maintenue **de la prise de focus jusqu'à la fin du run**, navigation du menu comprise. Indispensable pour les conditions que le jeu lit sur l'état *tenu* à un instant précoce : `-Then` ne démarre qu'après la validation du menu, et certains écrans sont atteints avant |
| `-Trace A,B` | pose `OPENFORESIA_TRACE_A=1`, `OPENFORESIA_TRACE_B=1` (voir plus bas) |
| `-KeepOpen` | laisse le jeu ouvert |
| `-MenuDelay`, `-TapMs`, `-MaxTaps` | réglages fins, rarement utiles |
| `-WorkingDir <chemin>` | dossier de travail du processus lancé. Par défaut celui de l'exe (comme un double-clic Explorer). À poser pour reproduire un lancement dont le CWD diffère — **Rider "Run" et `dotnet run` mettent tous deux par défaut le dossier du `.csproj`**, pas celui de l'exe compilé — ce qui change la résolution de tout chemin relatif que le jeu ouvre (`save\...` notamment, voir plus bas) |

### Sorties

| fichier | contenu |
|---|---|
| `scratchpad/playtest-<horodatage>.log` | **le journal utile** — tout passe par stderr |
| `scratchpad/playtest-<horodatage>-stdout.log` | quasi vide |
| `scratchpad/playtest-<horodatage>.png` | la capture, à lire directement |

Un fichier par exécution, et les vingt derniers sont conservés. Le nom fixe qu'utilisait la première
version se faisait réécrire **sans être tronqué** : le verdict mélangeait alors deux essais, et
présentait la mesure d'un run précédent comme celle du run courant. Le chemin exact est rappelé en
fin de verdict. `scratchpad/` est ignoré par git.

---

## Lire le verdict

```
===== VERDICT =====
aucun plantage
images 481 ; DrawPrimitive 116451 (+22940 sur 60 images = 382.3 par image)
```

- **`aucun plantage`** + un débit non nul : l'écran tourne. Regarder la capture pour juger du reste.
- **`ATTENTION : rien de dessiné`** : la boucle tourne mais n'émet aucune primitive. Là seulement il
  vaut la peine d'instrumenter (`EmitModelQuads`, le budget `s_Unknown_005e9818`).
- **`PLANTAGE`** : la pile est imprimée. Deux familles à distinguer tout de suite :
  - une **exception managée** (`NotImplementedException`, `InvalidOperationException`) est un garde
    du projet : son message nomme précisément ce qui manque, il suffit de le lire ;
  - un **`Fatal error. 0xC0000005`** est une violation d'accès native. Voir la section suivante.

### Repères mesurés

| écran | `DrawPrimitive` par image |
|---|---|
| menu principal | ~110 |
| sélection des personnages | ~382 |
| groupe de mise au point (`RunDebugPartySelect`) | 58 à vide, +10 par membre |

Un écart franc par rapport à ces valeurs est en soi une information.

---

## Un `0xC0000005` dans un appel winsys3d est une question d'arité

Trois fois sur trois, une faute native dans un appel vers la DLL venait d'une signature P/Invoke
avec **moins de paramètres que la vraie fonction** : en cdecl, l'argument manquant se lit sur la
pile de l'appelant et la DLL écrit à travers.

Avant toute autre hypothèse : ouvrir `winsys3d.c`, retrouver le corps, compter ses paramètres.
Attention, la récupération de paramètres de Ghidra est peu fiable sur ce binaire — elle échange
parfois deux paramètres en tête de fonction, ce qui fait lire une fonction à trois arguments comme
une fonction à deux. Comparer les chemins scalaire et MMX : ils se contredisent rarement.

Précédents : `Ordinal_795`, `FUN_0052a1e0`, `FUN_0052a280`.

---

## Pourquoi le pilotage clavier est ce qu'il est

À ne pas redécouvrir. Chacun de ces points a coûté au moins un essai raté.

- **Le clavier passe par DirectInput.** `SendKeys` et les messages de fenêtre sont invisibles pour
  lui. Il faut `SendInput` avec `KEYEVENTF_SCANCODE`.
- **Seules les flèches et Entrée sont liées.** `DIK_DOWN` = `0xd0` (scancode `0x50` + drapeau
  étendu), `DIK_RETURN` = `0x1c`. La moitié « pavé numérique » de la table de liaisons par défaut ne
  répond pas : `OpenForesia.cfg` la remplace.
- **Le relâchement d'une touche étendue se perd parfois** et DirectInput garde la touche enfoncée.
  Le script renvoie le relâchement avec puis sans le préfixe `E0`.
- **Le jeu tourne à ~6 images/s et sonde une fois par image.** Un appui de moins de ~200 ms tombe
  entre deux sondages et disparaît ; un appui de plus de ~450 ms se répète et le curseur s'emballe
  (il boucle sur les cinq entrées). **Chronométrer un appui ne marche pas** — cinq essais l'ont
  confirmé.
- **Le premier appui après `SetForegroundWindow` est toujours avalé.** Le script envoie donc un
  appui d'échauffement — et la validation, elle, ne se rattrape pas si on la laisse être ce premier
  appui.
- **Attendre la fenêtre, ne pas la supposer prête.** Avec `-Build`, ou après une relance, elle peut
  arriver plusieurs secondes après `-MenuDelay`. Sans attente le focus échoue en silence et la
  navigation part dans le vide : mesuré, 11 appuis au lieu de 2. Le script sonde jusqu'à 20 s une
  fenêtre **dont le titre est exactement `OpenForesia`** — un jeu relancé porte aussi une console
  dont le titre est le chemin de l'exe, et la prendre envoie toutes les frappes ailleurs.
- **Le curseur du menu butte sur la première et la dernière entrée, il ne boucle pas.** Un
  dépassement est donc sans retour si on ne tape que vers le bas : la navigation doit être
  bidirectionnelle (`DIK_UP` = `0xc8`, scancode `0x48` étendu).
- **`CurrentPadWord()` reste figé sur la dernière valeur non nulle après relâchement.** La valeur
  `pad=` de la trace n'est donc *pas* un indicateur de « touche tenue ».
- **`LALT` (la croix) fait perdre le focus.** Windows ouvre le menu système de la fenêtre sur Alt, et
  toutes les frappes suivantes de la séquence `-Then` partent ailleurs — silencieusement, le script
  les annonce quand même. Symptôme observé : une capture qui ne montre aucun des effets attendus
  alors que le débit de primitives, lui, en montrait pendant la mesure. **Utiliser `ESC` à la
  place** : c'est la liaison secondaire de la même action, donc le même masque manette, sans le
  problème.

### Quel bouton pour quel jeton

Établi en croisant `s_PadButtonMasks_00564f18` (l'action → masque manette) avec la section
`[Controls]` d'`OpenForesia.cfg` (l'action → scancode). À refaire ainsi plutôt qu'à tâtonner.

| jeton | action | masque manette | scancode | touche |
|---|---|---|---|---|
| `LCTRL` / `ENTER` | 7 | `0x8000` carré | 29 / 28 | Ctrl gauche, Entrée |
| `ESC` / `LALT` | 6 | `0x4000` croix | 1 / 56 | **Échap**, Alt gauche |
| `SELECT` | 8 | `0x0001` select | 59 | F1 |
| `START` | 11 | `0x0008` start | 60 | F2 |
| `UP`/`RIGHT`/`DOWN`/`LEFT` | 12–15 | `0x10`/`0x20`/`0x40`/`0x80` | 72/77/80/75 | flèches |

Chaque action a deux liaisons clavier, la primaire dans `K00xx` et la secondaire dans `K01xx` — d'où
les deux touches sur les deux premières lignes. **Préférer `ESC` à `LALT` pour la croix** : les deux
donnent le même masque manette, mais Alt fait perdre le focus (voir le piège ci-dessus).

Le tableau est **vérifié en jeu** : une trace posée sur le mot « tenu » (`0x604a20`) montre
`0x0001` avec F1 enfoncé, `0x0008` avec F2, `0x8000` avec Entrée, `0x0040` avec Bas. Refaire cette
mesure plutôt que de supposer, si un doute revient.

Attention : **`OpenForesia/` ne contient pas de `.cfg`**, donc avec `-WorkingDir OpenForesia` ce sont
les liaisons **par défaut** (`s_KeyBindingDefaults_00564ab8`) qui s'appliquent, pas le
`bin/Debug/net9.0/OpenForesia.cfg`. Les deux tables se trouvent être identiques ici, mais la
distinction compte dès que l'une des deux change.

### Taper du texte : `TYPE:` et `KBENTER`

L'écran de saisie du nom est le seul du jeu à lire de **vrais caractères** : il coupe la traduction
manette→clavier et dépile la file `WM_CHAR` de winsys3d par `GetKBChar`. Les jetons `TYPE:` et
`KBENTER` passent donc par `KEYEVENTF_UNICODE` et non par un scancode. Deux raisons :

- un scancode dépend de la disposition clavier — sur AZERTY, le scancode du `a` QWERTY donne `q` ;
- `KEYEVENTF_UNICODE` injecte le caractère voulu, le système synthétise `VK_PACKET`, et le
  `TranslateMessage` de `D3D11Window` en fait le `WM_CHAR` que le jeu consomme.

`KBENTER` envoie `0x0d`, que l'écran traite comme la case « fin » : il confirme le nom et passe au
sous-état suivant. À ne pas confondre avec `ENTER`, qui est le bouton **carré** de la manette.

D'où la seule méthode fiable : **la boucle fermée**. `OPENFORESIA_TRACE_MENU=1` fait écrire au jeu
la position de son curseur (`+0x8C4:xx->yy`) ; le script relit le stderr redirigé pendant que le jeu
tourne (ouverture en `FileShare.ReadWrite`, le jeu tient encore le fichier) et retape jusqu'à
atteindre la cible. Même principe pour la validation : on attend que `pad=0x8000` apparaisse, c'est
le jeu qui confirme avoir vu la frappe.

---

## Échafaudages de trace disponibles

Poser avec `-Trace <nom sans le préfixe>` :

| variable | ce qu'elle montre |
|---|---|
| `OPENFORESIA_TRACE_MENU` | le descripteur du menu champ par champ (posée d'office par le script) |
| `OPENFORESIA_TRACE_GLYPHS` | le moteur de texte |
| `OPENFORESIA_TRACE_VP` | le viewport |
| `OPENFORESIA_DUMP_GEOMETRY` | l'enveloppe des coordonnées émises, ajoutée au bilan `[SHIM]` |
| `OPENFORESIA_OPTIONS_SETUP` | la mise en place de la page d'options |

Un échafaudage ajouté pour une enquête ponctuelle **se retire une fois l'enquête close** (règle du
projet). Ceux listés ici sont permanents.

---

## L'écran de sauvegarde a besoin d'un dossier `save\` déjà présent

Le jeu original (ni `Blaze.c` ni `winsys3d.c`) n'appelle jamais `CreateDirectory` : la sauvegarde
passe par un simple `CreateFileA("save\Blaze-Saving<code>", ...)`, qui échoue silencieusement
(bascule propre sur l'écran d'erreur, pas de plantage) si le dossier `save\` n'existe pas déjà —
fidèle à l'original, qui partait du principe que le dossier était déjà là (fourni à l'installation
en 1998).

**Le nom du fichier n'est pas le nom tapé** : c'est `Blaze-Saving` suivi de sept caractères de code
dérivés du personnage (nom, statistiques, classe, élément, style) — `Blaze-Saving@MJAXY_` par
exemple. Un personnage = un fichier ; deux personnages qui diffèrent par n'importe lequel de ces
éléments donnent deux fichiers. Détail dans `legacy-bug.md` (LB-002).

`OpenForesia/save/` (à côté du `.csproj`, avec un `.gitkeep` pour rester suivi par git) existe donc
dans le dépôt pour cette raison. **C'est l'endroit qui compte** : Rider « Run » et `dotnet run`
lancent tous deux le process avec ce dossier comme répertoire de travail par défaut — pas
`bin/Debug/net9.0/`, où se trouve l'exe compilé mais où `playtest.ps1` pointe par défaut. Pour
tester le chemin d'écriture dans les mêmes conditions que Rider, passer
`-WorkingDir OpenForesia` :

### Le parcours de création complet est scriptable (2026-08-18)

`OPENFORESIA_FORCE_SUBSTATE=7` **ne sert plus à tester la sauvegarde** : il saute la finalisation
qui compose le nom de fichier, et l'échafaudage `OPENFORESIA_FORCE_CARD_NAME` qui compensait ce
manque avec un nom `TEST` a été retiré. `FORCE_SUBSTATE=6` ne marche pas non plus (le moteur de
texte n'a pas mis la page en page une fois : `InvalidOperationException` dans `DrawUiPanel`).

La bonne manière est le vrai parcours, qui tient dans un `-Then`. Vérifié en jeu, deux personnages
de suite, deux fichiers distincts :

```powershell
$env:OPENFORESIA_FORCE_CONFIRM_STATS = '1'
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 55 -Shot -WorkingDir OpenForesia `
  -Then WAIT3,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,TYPE:VINCENT,WAIT1,KBENTER,WAIT3,
        ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,
        ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT4,
        ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT5
Get-ChildItem 'OpenForesia\save'
```

La suite d'`ENTER` correspond aux écrans décrits dans
`Analyse/Comportement Legacy/Selection des personnages.md` : carrousel (3 validations), nom (frappe
clavier + `KBENTER`), style de langage (3), élément de protection (3), bonus (3 cartes + 2), puis la
répartition des points — que `OPENFORESIA_FORCE_CONFIRM_STATS=1` valide d'office — et enfin l'écran
de sauvegarde. Compter environ 55 s ; changer le `TYPE:` suffit à obtenir un autre fichier.

Puis lire le fichier écrit dans `OpenForesia/save/Blaze-Saving<code>` (24576 octets, en-tête
`SC 11 03`, le nom tapé encodé sur deux octets par caractère à partir de l'offset 4).

**Rejouer exactement le même personnage écrase sa sauvegarde**, sans plantage : le parcours
`0xe → 0xf → 0x10` supprime le fichier existant puis repart à la phase 0, qui le récrée. Rien à
nettoyer entre deux essais.

> **Historique, à ne pas re-soupçonner** : jusqu'au 2026-08-18 ce même cas plantait en
> `0xC0000005` dans `ResetCardTransferStatus`/`Ordinal_67` (huit gestionnaires winsys3d enregistrés
> avec un pointeur `null` ; corrigé). Il avait coûté trois essais le 2026-08-17, dont deux passés à
> soupçonner à tort le code de la séance : la trace était identique d'un run à l'autre (même numéro
> d'image, même compteur de primitives), ce qui **ressemblait** à un défaut déterministe du code
> alors que c'était l'état du disque. La leçon reste bonne — un plantage parfaitement reproductible
> qui n'apparaît qu'à partir du deuxième lancement doit faire regarder ce que le premier a laissé
> derrière lui.

Pour repartir d'un dossier vide malgré tout :

```powershell
Get-ChildItem 'OpenForesia\save' -Filter 'Blaze-Saving*' | Remove-Item -Force
```

### Le fichier atterrit dans le `save\` du répertoire de travail, pas dans celui du dépôt

Le chemin est relatif (`save\Blaze-Saving<code>`), donc il suit le **répertoire de travail du
processus**, pas l'emplacement de l'exe ni celui du `.csproj` :

| lancement | le fichier apparaît dans |
|---|---|
| Rider « Run », `dotnet run` | `OpenForesia\save\` |
| double-clic sur l'exe, `bin\Debug\net9.0\OpenForesia.exe` | `OpenForesia\bin\Debug\net9.0\save\` |
| `playtest.ps1` sans `-WorkingDir` | `OpenForesia\bin\Debug\net9.0\save\` |
| `playtest.ps1 -WorkingDir OpenForesia` | `OpenForesia\save\` |

« La sauvegarde ne crée plus de fichier » est presque toujours ce décalage-là : regarder les deux
dossiers avant de conclure.

---

## Capturer un écran de transition (2026-08-18)

Entre deux états de jeu, `RunLoadingScreen` affiche une illustration plein cadre pendant **150
images, soit environ 2,6 s**. C'est court, et `playtest.ps1` capture à la *fin* de sa fenêtre
d'observation — en comptant environ une seconde entre la validation détectée et le début de cette
fenêtre. Il faut donc **`-Seconds 0`** :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 0 -Shot -WorkingDir OpenForesia
```

À `-Seconds 2` on est déjà sur l'écran suivant, ce qui donne l'illusion que la transition n'existe
pas. **Le repère fiable est le débit de primitives du verdict** : `1 par image` est la signature de
l'écran de transition (une seule tuile plein cadre), contre ~380 pour la sélection des personnages
et ~400 pour l'organisation du groupe.

Transitions connues : à chaque changement d'état de jeu **sauf** vers 7, 9, 2 et 3 — donc menu
principal → sélection des personnages, et sélection des personnages → organisation du groupe.

---

## Le choix final de l'écran de sauvegarde (2026-08-18)

Après « Enregistrement achevé. », un appui de plus ouvre la boîte à deux lignes :

```
> Définir de nouveaux personnages    → retour au carrousel (étape I de la création)
  Constituer un groupe               → l'écran « Organisation du groupe »
```

Le curseur démarre sur la première ligne ; **BAS puis validation** prend la seconde. En scripté :

```powershell
$env:OPENFORESIA_FORCE_CONFIRM_STATS = '1'
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 92 -Shot -WorkingDir OpenForesia `
  -Then WAIT3,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,TYPE:ARTHUR,WAIT1,KBENTER,WAIT3,
        ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT3,
        ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT1,ENTER,WAIT4,
        ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT5,ENTER,WAIT4,DOWN,WAIT3,ENTER,WAIT10
```

Retirer le `DOWN,WAIT3` pour tester l'autre branche.

**Le nombre d'appuis de la queue n'est pas stable d'un run à l'autre** : réécrire une sauvegarde
existante ajoute les phases de suppression, donc une ou deux validations de plus qu'une création
neuve. Si la capture montre encore « Enregistrement achevé. », il manque un `ENTER` ; si elle montre
déjà le carrousel, il y en a un de trop. Le repère fiable est la capture, pas le compte.

---

## Atteindre l'écran de constitution du groupe (`RunPartyConstitution`)

C'est le chemin B de l'état de jeu 1, et il est **directement accessible depuis le menu principal** :
entrée 0, « Début du jeu ». Aucun échafaudage.

Le seul prérequis est qu'il existe au moins une sauvegarde dans le `save\` du répertoire de travail
(voir le tableau plus haut) — sinon l'écran bascule aussitôt vers la sélection des personnages, ce
qui est le comportement correct et vaut d'être testé aussi.

```powershell
# 1. produire une sauvegarde (voir « Le parcours de création complet est scriptable »)
# 2. puis, avec cette sauvegarde en place :
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 40 -Shot -WorkingDir OpenForesia `
    -Then WAIT4,ENTER,WAIT3,START,WAIT4,ENTER,WAIT4,ENTER,WAIT8
```

Ce que la séquence joue : `ENTER` (carré) retient le personnage sous le curseur, `START` demande la
confirmation (« Veux-tu jouer avec ce groupe? »), `ENTER` ouvre la boîte Oui/Non, `ENTER` répond
Oui. Le run se termine alors sur `thunk_FUN_00516020 (game state 3) not yet reconstructed` — c'est
le **résultat attendu**, la frontière actuelle, pas un défaut.

#### Il faut retenir AUTANT de personnages que la sauvegarde en porte (2026-08-21)

La séquence ci-dessus n'a marché que tant que le `save\` de test ne portait **qu'un** personnage.
Avec quatre, elle reste bloquée sur l'écran « Organisation du groupe » : un seul `ENTER` ne retient
qu'un personnage, et le `START` qui suit ne déclenche pas la confirmation. Le run ne plante pas — il
finit sa fenêtre d'observation sur le même écran, ce qui se lit « aucun plantage » au verdict et
peut passer pour un succès. **Regarder la capture**, pas seulement le verdict.

La forme qui marche descend d'un cran entre chaque prise :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 50 -Shot -WorkingDir OpenForesia `
    -Then WAIT4,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT2,START,WAIT4,ENTER,WAIT4,ENTER,WAIT10
```

Elle mène jusqu'à l'état de jeu 3. Frontière au **2026-08-22** : le prologue et l'ossature de la
boucle par image de `FUN_00516fc0` s'exécutent, et l'arrêt est dans le **cas 0 du répartiteur
`DAT_005e9ae4 & 3`** (le pas de simulation du monde). Repère mesuré sur ce chemin : **1 primitive
par image** une fois la carte du monde atteinte (rien n'y est encore dessiné : le bloc d'affichage
principal est le dernier trou de la boucle), contre ~434 sur l'écran d'organisation.

**Ce repère de 1 primitive par image ne bougera pas** tant que `0x00517aa7` n'est pas transcrit,
quel que soit le nombre d'appelées du cas 0 qui s'ajoutent. Le seul indicateur de progrès sur ce
chemin est donc **le texte de l'exception**, pas le compte de primitives — le lire, et le comparer
à la frontière écrite ici.

Repère mesuré : environ 384 primitives par image sur cet écran.

---

## Atteindre l'écran de groupe de mise au point (`RunDebugPartySelect`)

Cet écran est le chemin A de l'état de jeu 1, gardé par `DAT_005633a8 == 0 && SELECT maintenu`.

### La recette complète, relevée sur le jeu officiel par Vincent (2026-08-18)

La séquence exacte, de bout en bout, telle qu'elle marche sur le jeu de 1998 :

1. Créer un nouveau personnage nommé **Gurkha**.
2. **Sauvegarder** ce personnage.
3. À la fin, choisir **« Constituer un groupe »** (et non « Commencer à jouer malgré tout »).
4. **Maintenir F1 pendant le chargement** qui suit.
5. On arrive sur l'écran de groupe de mise au point — celui que `RunDebugPartySelect` dessine.
6. Après le choix fait sur cet écran, le jeu démarre **en mode développeur**, avec les fonctions de
   debug actives.
7. Et il arrive sur la **mappemonde avec absolument tout débloqué** — très utile pour tester les
   écrans qui dépendent de la progression (les icônes de destination, notamment, qui se superposent
   toutes à l'origine quand la progression est nulle).

Ce que ça apprend au-delà de la recette : « Constituer un groupe » et « Début du jeu » mènent au
**même endroit**, l'état de jeu 1, dont le chemin A est cet écran et le chemin B l'écran de
constitution du groupe (`FUN_00536ea0`). L'étape 4 n'est pas un appui mais bien un maintien, ce qui
concorde avec la lecture du branchement (`RunGameState1Entry` teste l'état *tenu* de SELECT).

**Les deux moitiés du verrou, vérifiées séparément dans notre port :**

1. **Faire afficher le mot « Gurkha »**. `CheatCodeProbe` file la séquence `DAT_00564a40` sur chaque
   caractère *dessiné* (par `DrawTextRun` comme par `RasterizeGlyph`), pas sur une saisie. Nommer un
   personnage « Gurkha » suffit : le nom est dessiné pendant la frappe, et le curseur `_` tracé juste
   derrière est le caractère qui déclenche la bascule. Mesuré : la trace tombe dès la frappe du `a`.
   Le clavier virtuel de l'écran porte majuscules **et** minuscules, et la frappe réelle marche
   aussi (`-Then TYPE:Gurkha`).
2. **Maintenir SELECT (F1) à l'entrée de l'état de jeu 1** — pas un appui, l'état tenu. Mesuré : avec
   `-Hold SELECT`, la trace posée au branchement lit `pad0=0x01`.

Le verrou est **à sens unique** : `DAT_005633a8` n'a qu'un seul écrivain dans les 98 000 lignes de
`Blaze.c` (ligne 66792, qui le met à 0) et 22 lecteurs. Une fois « Gurkha » tapé, le mode
développeur reste actif jusqu'à la fermeture du jeu — inutile de le refaire entre deux écrans.

Réunir les deux dans **un même run scripté** bute sur la longueur de la création de personnage (la
répartition des points demande une trentaine d'appuis) ; à la main c'est l'affaire de quelques
secondes. D'où l'échafaudage `OPENFORESIA_FORCE_DEBUG_PARTY=1`, qui force la branche pour les tests
automatisés.

```powershell
Get-ChildItem 'OpenForesia\save' -Filter 'Blaze-Saving*' | Remove-Item -Force
$env:OPENFORESIA_FORCE_SUBSTATE='7'; $env:OPENFORESIA_FORCE_CONFIRM_STATS='1'
$env:OPENFORESIA_FORCE_DEBUG_PARTY='1'
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 34 -Shot -WorkingDir OpenForesia `
    -Then WAIT14,DOWN,WAIT1,ENTER,WAIT3,START,WAIT6
```

> **2026-08-18** : cette recette portait aussi `OPENFORESIA_FORCE_CARD_NAME='1'`, retiré depuis avec
> l'échafaudage. Avec `FORCE_SUBSTATE=7`, la finalisation ne tourne pas, donc le nom de fichier reste
> vide et **l'écriture de la sauvegarde échoue** — sans conséquence pour ce test-ci, dont
> `FORCE_DEBUG_PARTY` est le vrai levier, mais non rejoué depuis le changement.

Le `WAIT14` n'est pas négociable : la chaîne création + sauvegarde forcée occupe encore l'écran
pendant une douzaine de secondes après la validation du menu, et une frappe qui tombe là-dedans part
dans l'écran de sauvegarde, pas dans celui qu'on veut tester.

Repères mesurés : **58** primitives par image sur l'écran à vide, environ 10 de plus par membre
ajouté au groupe.

---

## Pièges d'outillage, hors du jeu

- **Le comportement de `dotnet run --project OpenForesia` dépend de la machine.** Vérifier
  `dotnet --list-sdks` : s'il liste un SDK, le `dotnet` du PATH suffit tel quel (cas de la machine
  actuelle — SDK x86 9.0.317 directement sur le PATH). S'il est vide, le PATH ne porte qu'un
  runtime et il faut pointer `DOTNET_ROOT` vers le vrai SDK avant de construire, puis **le retirer
  aussitôt après** : un processus **x86** qui hérite d'un `DOTNET_ROOT` x64 meurt avant d'ouvrir sa
  fenêtre, sur `Failed to load hostfxr.dll … 0x800700C1`. `playtest.ps1 -Build` fait cette
  détection automatiquement (voir sa section « construction »). Son repli est désormais une **liste**
  de chemins candidats (`$fallbackCandidates`), `C:\Program Files (x86)\dotnet` en tête — y ajouter
  un chemin plutôt que d'en coder un ailleurs. Noter aussi que le PATH d'une fenêtre PowerShell
  n'est pas forcément celui d'une fenêtre Bash : `dotnet` peut être introuvable d'un côté et présent
  de l'autre dans la même session de travail.
- **`OpenForesia.Tools` suit la même règle** que `dotnet run` ci-dessus — marche directement si le
  PATH a un SDK, sinon même contournement `DOTNET_ROOT`. Palliatif de secours pour la table
  d'exports sans passer par dotnet du tout : `OpenForesia.Tools/exports.ps1`.
- **Lire le binaire ne demande aucun outil.** `od -A x -t x4 -j <offset> -N <taille> Legacy/Binaries/Blaze.exe`
  suffit, et se diffe contre les constantes du source — c'est ainsi que les 26 dwords du descripteur
  d'interface ont été établis.
- **Les heredocs de bash sont peu fiables ici** : les backticks y sont évalués même avec un
  délimiteur entre quotes, et les `\xNN` ne le sont pas. Pour écrire un fichier, l'écrire
  directement ; pour éditer `Blaze.cs` (UTF-8 avec BOM), passer par l'outil d'édition.

---

## Quand ce fichier doit changer

Dès qu'un test apprend quelque chose de réutilisable : un nouveau repère chiffré, un piège de
pilotage, une famille de plantage récurrente, un échafaudage utile. Et si `playtest.ps1` gagne un
paramètre, le tableau ci-dessus le mentionne.

## Atteindre le sous-écran « Invertaire » (2026-08-18)

Comme l'écran d'organisation du groupe, il exige **au moins une sauvegarde** dans le `save\` du
répertoire de travail : sans elle, la phase 0 repart aussitôt vers l'écran racine du menu objets et
la capture montre les trois libellés, pas la liste. Créer d'abord une sauvegarde (voir « Le parcours
de création complet est scriptable »), puis lancer avec **le même `-WorkingDir`** :

```powershell
# la liste des sauvegardes
OpenForesia.Tools\playtest.ps1 -Entry 2 -Seconds 12 -WorkingDir OpenForesia `
  -Then WAIT2,ENTER,WAIT6 -Shot

# le fichier chargé et la liste d'objets
OpenForesia.Tools\playtest.ps1 -Entry 2 -Seconds 16 -WorkingDir OpenForesia `
  -Then WAIT2,ENTER,WAIT3,ENTER,WAIT7 -Shot
```

`ENTER` est le bouton **carré** (`K0107=28` dans le fichier de configuration), celui que valident
l'écran racine du menu objets et la liste des sauvegardes.

**Le `WAIT3` entre les deux `ENTER` n'est pas décoratif** : la phase 5 est décalée d'une image (celle
qui entre dans la phase se contente d'abaisser `DAT_005de4dc`, la suivante lit le fichier), et la
lecture porte sur trois blocs. Enchaîner les deux validations sans attente laisse la capture sur la
liste des sauvegardes.

Signatures utiles : ~416 primitives par image sur la liste des sauvegardes, ~374 sur la liste
d'objets, ~387 sur l'écran racine du menu objets.

**Il n'y a rien à atteindre au-delà de la liste d'objets.** La fiche détaillée et le clavier de
renommage sont injoignables dans le jeu de 1998 — voir `legacy-bug.md`, LB-004. Ne pas chercher une
séquence de touches qui les ouvrirait : elle n'existe pas.

## Exercer l'enchère du sous-écran « Utilisation » (2026-08-18)

Le cas 2 de `FUN_00527e30` exige un **équipement non porté**, et un personnage fraîchement créé n'en
a pas : ses trois pièces sont portées et sa potion n'est pas un équipement (`+0x12 & 0xf00 == 0`).
Ce n'est pas un défaut, c'est la règle du jeu — voir `REVERSE_STATUS.md`.

Pour rendre le cas atteignable, déséquiper une pièce sur une sauvegarde **de test**. La somme de
contrôle est stockée **deux fois** (0x1ffc et 0x3ffc) et il faut recalculer les deux, sans quoi le
sous-écran « Invertaire » refusera le fichier :

```powershell
$p = 'OpenForesia\save\Blaze-Saving<code>'
$b = [IO.File]::ReadAllBytes($p)
$b[0x2b2] = 0xFF          # +0x2b0..0x2b4 = les emplacements portes ; 0xFF = aucun
$sum = 0
for ($i = 0; $i -lt 0x1ffc; $i++) { $v = $b[$i]; if ($v -gt 127) { $v -= 256 }; $sum += $v }
$cs = [BitConverter]::GetBytes([int]$sum)
[Array]::Copy($cs, 0, $b, 0x1ffc, 4)
[Array]::Copy($cs, 0, $b, 0x3ffc, 4)
[IO.File]::WriteAllBytes($p, $b)
```

**`[sbyte]` ne convient pas en PowerShell** pour la somme : le cast lève sur toute valeur > 127. La
somme du jeu porte bien sur des octets **signés**, d'où la soustraction de 256 à la main.

Puis, avec deux personnages retenus (le vendeur d'abord, il devient le membre 0) :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 2 -Seconds 34 -WorkingDir OpenForesia -Shot `
  -Then WAIT2,DOWN,WAIT1,ENTER,WAIT4,DOWN,WAIT1,ENTER,WAIT1,UP,WAIT1,ENTER,WAIT1,START,
        WAIT5,ENTER,WAIT3,RIGHT,WAIT1,RIGHT,WAIT1,ENTER,WAIT5
```

Les deux `RIGHT` amènent le curseur sur la pièce déséquipée ; la grille se compacte, donc le rang
dans la grille est celui de l'emplacement **occupé**, pas l'index brut.

Signatures : ~403 primitives par image sur la grille d'objets (cas 1), ~368 sur l'enchère (cas 2).

**Ne pas modifier les sauvegardes que l'on n'a pas créées soi-même** : elles sont indistinguables
d'une partie réelle une fois dans `save\`. Créer la sienne avec le parcours scripté plus haut.
## Atteindre la carte du monde (état 3)

`OPENFORESIA_SKIP_AREA_HANDLER` **n'existe plus depuis le 2026-08-21** : `FUN_004747F0`, que Ghidra
n'avait pas décompilée, est transcrite. Aucun drapeau n'est nécessaire.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 36 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT5,ENTER,WAIT4,ENTER,WAIT16
```

La séquence est : « Début du jeu » → un personnage retenu (`ENTER`) → `START` → « Veux-tu jouer
avec ce groupe? » → `ENTER` → « Oui » → `ENTER`.

**Prérequis** : au moins une sauvegarde dans `OpenForesia\save\`.

**Cette séquence est sensible au minutage et échoue régulièrement.** Deux causes distinctes, à ne
pas confondre :

* une **relance perdue** (voir la section dédiée plus haut) décale tout — chercher
  `relance detectee` dans la sortie ;
* et même sans relance, la validation du groupe rate parfois. Les attentes ci-dessus sont plus
  longues que la version d'origine pour cette raison.

**Dans les deux cas le verdict affiche « aucun plantage », ce qui est trompeur.** Le contrôle qui
tranche est le **débit** : ~434 primitives par image veut dire qu'on est resté sur l'écran
d'organisation du groupe ; l'état 3 atteint donne ~1 par image (il ne dessine pas encore) et
s'arrête sur l'exception nommée du prochain corps manquant. Relancer suffit.
## Le `save\` suit le répertoire de travail, pas l'exe (rappel)

Symptôme classique : « Invertaire » et « Enchère » semblent ne mener nulle part. Ils rebondissent
en fait vers l'écran racine parce que le `save\` du répertoire de travail est vide — c'est le
comportement de l'original quand il n'y a aucune sauvegarde. Le `.cfg`, lui, suit le chemin de
l'**exe**. Les deux ne coïncident que si l'on lance depuis `bin\Debug\net9.0\`.

---

## `-Entry -1` reste sur l'écran-titre, et n'a donc aucun débit à mesurer (2026-08-19)

Relevé en figeant les références d'avant-découpage. `-Entry -1` (« rester au menu ») produit une
capture de l'**écran-titre / attract**, pas d'une liste d'entrées : l'image est **identique à
`-Seconds 3` et à `-Seconds 25`**, donc ce n'est pas un cycle d'attract qu'on attraperait au mauvais
moment.

Sur cet écran le verdict affiche toujours :

```
pas assez de bilans [SHIM] pour mesurer la progression
```

**C'est normal, pas un échec.** Le régime titre/attract passe par `StartupSurface`
(DIB section + `StretchDIBits`) et n'émet aucune primitive 3D ; les compteurs `[SHIM]`, alimentés
par les slots `BeginScene` et `Flip`, restent donc à zéro. Ce n'est **pas** un effet du device
synthétique : les entrées 1 et 2 l'utilisent aussi et comptent normalement.

Conséquence pratique : pour une comparaison avant/après sur le menu principal, **le seul repère est
la capture**, jamais le débit. Les écrans qui donnent un débit exploitable sont ceux qui empruntent
le chemin Direct3D.

### Repères mesurés le 2026-08-19, dépôt à `6cc5301`, aucune modification C#

| écran | commande | débit |
|---|---|---|
| titre / attract | `-Entry -1 -Seconds 3` | aucun (voir ci-dessus) |
| sélection des personnages | `-Entry 1 -Seconds 20 -WorkingDir OpenForesia` | **382,3** par image |
| menu objets | `-Entry 2 -Seconds 20 -WorkingDir OpenForesia` | **387** par image |

Les 382,3 concordent avec le repère historique (~382) mesuré sur le chemin hérité : le passage au
device synthétique n'a pas déplacé la valeur.

---

## « No free block ... below 0x01000000 » : relancer, ce n'est pas une régression (2026-08-19)

Un run peut échouer sur :

```
InvalidOperationException: No free block of 0x2000 bytes below 0x01000000.
UI text descriptors are masked to 24 bits by FUN_005443a0, so they cannot live higher.
   at AllocLowZeroed(...)
```

**C'est une course perdue pour la mémoire basse, et elle est non déterministe.** Mesuré le
2026-08-19 : la même commande, relancée sans rien changer, passe et rend 434 primitives par image.

Le mécanisme est documenté dans `REVERSE_STATUS.md`, § « The UI descriptors carry an ordering table,
so they must live low ». Les descripteurs d'interface contiennent leur propre table d'ordonnancement
à `+0xa4`, que `ClearOTagR` relie en masquant sur 24 bits : au-dessus de `0x01000000` le descripteur
relie sa liste sur des adresses tronquées. Ils sont donc alloués sous cette barre, dans une fenêtre
que l'ASLR et le chargeur laissent plus ou moins libre d'un lancement à l'autre.

Ces symboles sont des **propriétés paresseuses** et non des initialiseurs de champ, précisément pour
que la course perdue donne une exception rattrapable au lieu d'une `TypeInitializationException`
hors du `try`/`catch` de l'initialiseur de module.

**Conduite à tenir** : relancer une fois. Si l'échec se répète plusieurs fois d'affilée, alors
seulement chercher ce qui a augmenté la pression sur la fenêtre basse.

## « relance détectée » : ce que cette ligne veut dire, et pourquoi il faut la lire (2026-08-19)

`playtest.ps1` imprime `(relance detectee : fenetre portee par le PID N)` quand le processus qui
porte la fenêtre n'est pas celui qu'il a lancé. C'est le chemin de récupération documenté : le jeu
**se relance en processus fils quand il perd la course pour la mémoire basse** (voir
`EnsureDisplayListArena`). Le journal le confirme :
`display-list arena: lost the race for low memory, relaunching (attempt N of 3)`.

Ce n'est **pas** une erreur — la partie relancée tourne normalement. Mais **le taux compte**. Le
taux de référence est d'environ **une exécution sur quatre**. Le 2026-08-19, il est monté à cinq sur
huit à cause d'un changement du composition root, et ni le verdict « aucun plantage » ni le débit
par image n'ont bougé. Cette ligne d'information a été le seul signal.

**Si tu la vois plusieurs fois d'affilée alors que les exécutions précédentes ne la montraient pas,
mesure le taux avant de conclure.** Le lancement direct suffit et va plus vite que `playtest.ps1`
(pas d'attente de menu) :

```powershell
$exe = "bin\Debug\net9.0\OpenForesia.exe"; $lost = 0; $n = 16
for ($i = 1; $i -le $n; $i++) {
    $o = "$env:TEMP\of_o.txt"; $e = "$env:TEMP\of_e.txt"
    Start-Process $exe -WorkingDirectory (Get-Location) -RedirectStandardOutput $o -RedirectStandardError $e | Out-Null
    Start-Sleep -Seconds 6
    Get-Process -Name 'OpenForesia' -ErrorAction SilentlyContinue | Stop-Process -Force
    Start-Sleep -Milliseconds 400
    if (((Get-Content $o -Raw) + (Get-Content $e -Raw)) -match 'lost the race') { $lost++ }
}
"$lost perdues / $n"
```

À lancer depuis `OpenForesia/`. **Seize essais, pas huit** : à ce taux, huit ne distinguent pas 25 %
de 40 %.

### Mesurer au repos, sinon la mesure ment

Contre-exemple chiffré, le 2026-08-19, **sur le même binaire** :

```
  16 essais pendant qu'un build et des tests tournaient  →  7 perdues = 44 %   (faux)
  32 essais machine au repos                             →  7 perdues = 22 %   (juste)
```

La première mesure a fait croire à une régression et a coûté une enquête. Fermer les builds, tests
et autres lancements avant de mesurer.

### Et « au repos » ne suffit pas : le taux DÉRIVE au fil d'une session (2026-08-21)

Le 2026-08-21, à la conversion d'`UiWindow`, sur **le même arbre** et machine au repos à chaque
fois :

```
  16 essais, en milieu de session   →   5 perdues = 31 %
  16 essais, juste après            →   6 perdues = 37 %     ← « régression ! »
  32 essais, en fin de session      →   1 perdue  =  3 %     ← même code, même machine
```

Les deux premiers lots ont fait croire à une régression franche contre les 22–25 % documentés.
**Il n'y en avait aucune** : la troisième mesure, prise plus tard, rend 3 %.

**La référence documentée est datée, donc elle ne vaut pas contrôle.** Comparer un taux d'aujourd'hui
à un chiffre écrit un autre jour, c'est comparer deux états de machine différents.

**Le contrôle, c'est l'arbre d'avant mesuré JUSTE À CÔTÉ.** La séquence qui tranche :

```powershell
# 1. arbre modifie, 32 essais
# 2. git stash -q ; dotnet build ; 32 essais        <- le controle, adjacent dans le temps
# 3. git stash pop -q ; dotnet build ; 32 essais    <- confirmation
```

Ce jour-là : contrôle **5/32 = 16 %**, arbre modifié mesuré juste après **1/32 = 3 %**. Le sens
était même inverse de l'alarme, et cohérent avec le raisonnement : la conversion déplaçait des
allocations *hors* du chemin pré-course.

**Corollaire à ne pas oublier : raisonner sur le mécanisme avant de lancer une enquête.** Une
conversion qui rend des champs paresseux ne peut que *réduire* le travail placé devant la course.
Quand la mesure dit le contraire de ce que le code impose, suspecter la mesure d'abord — mais la
refaire, pas l'ignorer.

### Une relance perdue invalide une séquence `-Then` (2026-08-21)

`playtest.ps1 -Then` envoie ses touches à la fenêtre selon un minutage fixe. **Si le jeu perd la
course pour la mémoire basse, il se relance en processus fils et tout le minutage se décale** : la
séquence part dans le vide et le jeu reste à l'écran où il était.

Vu le 2026-08-21 en reproduisant l'arrêt de la carte du monde. Première tentative : la ligne
`(relance detectee : fenetre portee par le PID …)` et un verdict « aucun plantage » — trompeur, la
séquence n'avait tout simplement pas été jouée. Seconde tentative, sans relance : la pile attendue
tombe.

**Donc : quand un `-Then` ne reproduit pas ce qu'il devrait, chercher `relance detectee` dans la
sortie AVANT de soupçonner la séquence.** Relancer suffit — le taux de relance est d'environ une
fois sur cinq.

Corollaire : un verdict « aucun plantage » sur une séquence `-Then` ne vaut que si la capture montre
bien l'écran visé. Le compteur de primitives ne le dit pas.

---

## Un plantage identique à un plantage connu n'a pas forcément la même cause (2026-08-22)

En vérifiant `FUN_0051c420`, deux exécutions ont levé un `NullReferenceException` dans
`PublishCameraFrame_004022B0` — **exactement la signature de la corruption mémoire de la veille**.
La conclusion naturelle, « la fonction que je viens d'écrire régresse », était fausse : c'était
l'échec de minutage de la séquence de formation du groupe, déjà documenté plus haut.

Le piège est que **le contrôle de débit ne le détecte pas**. La section « Atteindre la carte du
monde » dit de vérifier le débit : ~434 primitives par image = resté sur l'écran d'organisation,
~1 par image = état 3 atteint. Ici le débit valait bien 1 par image — l'état 3 *était* atteint — et
le jeu plantait quand même, plus tôt dans l'image que la lacune attendue.

**Le contrôle qui tranche est de vérifier que le code soupçonné s'est exécuté.** Un vidage d'une
ligne au premier passage de la fonction neuve a réglé la question en une exécution : le fichier
n'était pas écrit, donc la fonction n'avait pas tourné, donc la faute était ailleurs.

Des attentes plus longues rendent la séquence fiable :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 44 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT20
```

## Écrire un diagnostic dans un fichier, pas sur `stderr` (2026-08-22)

`Console.Error.WriteLine` **ne ressort nulle part** : ni dans le journal du playtest, ni dans son
`-stdout.log`. Une première tentative de diagnostic a été perdue de cette façon, et l'absence de
sortie a d'abord été lue comme « la fonction n'a pas été appelée » alors que le run précédent
prouvait le contraire.

Pour instrumenter une fonction le temps d'une exécution, écrire dans un fichier du `scratchpad\`
avec un chemin **absolu** (le répertoire de travail du jeu n'est pas celui du dépôt), et l'effacer
avant chaque run pour qu'un fichier périmé ne se fasse pas passer pour un résultat :

```powershell
Remove-Item scratchpad\diag-xxx.txt -ErrorAction SilentlyContinue
```

Sur du code à pointeurs, ce détour coûte moins cher qu'une relecture : deux passes de relecture
n'avaient pas trouvé un déréférencement manquant que l'impression de quatre champs d'en-tête a
rendu évident.

## La carte du monde ne s'arrête pas toujours au même endroit (2026-08-23)

À partir du moment où la moitié dessin passe, la même recette exécutée trois fois de suite donne
**trois sorties différentes** :

* la plus fréquente, `FUN_0053aa10` non transcrite, dans le pas de simulation ;
* de temps en temps, `DAT_005eca94` nul dans `PublishCameraFrame_004022B0`, **avant** le premier
  dessin de l'image ;
* parfois quelques images de plus avant de retomber sur l'une des deux.

Ce n'est pas du bruit d'outillage : la zone de départ pose des entités dont le pas d'états et les
scripts dépendent du tirage aléatoire et du nombre d'images écoulées avant que le groupe n'entre.
Un chemin de code atteint à une exécution ne l'est pas forcément à la suivante.

**Conséquence pratique : ne pas conclure sur un seul lancement.** Une lacune qui « disparaît » après
une modification a pu simplement ne pas être atteinte ce coup-ci ; une lacune qui « apparaît » n'est
pas forcément une régression. Lancer deux ou trois fois, et comparer les messages — ils se nomment
tous, c'est exactement à cela qu'ils servent.

Corollaire déjà vérifié ce jour-là : la nullité de `DAT_005eca94` a été prise pour une régression du
travail en cours, alors qu'elle était apparue à **la toute première exécution de la séance**, avant
la moindre modification. La trace de cette première exécution a suffi à trancher.

## Une lacune franche n'empêche pas de mesurer, mais empêche de voir (2026-08-23)

Quand le jeu meurt une ou deux images après être entré dans un écran, `-Shot` ne rend rien
d'utilisable : la fenêtre est déjà détruite quand la capture a lieu. Le compteur de primitives ne
sert pas davantage — il est imprimé toutes les soixante images, et les soixante dernières avant le
plantage sont encore celles de l'écran précédent.

Autrement dit : **tant qu'un écran ne survit pas quelques secondes, on ne peut ni le voir ni le
mesurer.** Le seul indicateur exploitable est alors la position de l'exception dans la pile — d'où
l'importance de transcrire les lacunes en `NotImplementedException` nommées plutôt qu'en no-op.

## Relire les assertions rouges de `MemoryLayoutTests` quand un symptôme est inexplicable (2026-08-23)

`dotnet test OpenForesia.Tests` rend trois assertions rouges, présentées de longue date comme
« volontaires » : elles nomment des défauts de disposition que le dépôt a choisi de ne pas corriger
parce que la correction change le comportement.

**Une assertion rouge volontaire cesse d'être volontaire dès qu'un site vivant l'active, et rien ne
signale ce basculement.** Le 23 août, une demi-journée est passée à chercher pourquoi les entités de
scène naissaient mortes ; le message de `Aucune_allocation_propre_dans_l_etendue_d_une_arene`
nommait le symbole fautif mot pour mot, avec la bonne explication :

```
s_EntityStatBlocks_005ea560 -> dans GameArena (0x005ea560, …CharacterCreation.cs:985)
L'arene et le symbole designent deux blocs distincts : une ecriture par l'un
n'est jamais lue par l'autre.
```

**Réflexe à prendre** : devant un symptôme qui n'a pas de cause visible dans le code — une valeur
qui revient à zéro, un état qui ne se propage pas, une écriture sans effet — lancer les tests de
disposition et *lire les trois messages* avant d'instrumenter quoi que ce soit. Une minute contre
une matinée.

## Instrumenter par échafaudage jetable, en remontant la chaîne (2026-08-23)

La méthode qui a fini par trouver ce défaut, en quatre exécutions :

1. **Poser la question au site suspecté**, pas ailleurs : un `throw` dans la branche « le
   remplissage est sauté » — il n'a pas tiré, donc le remplissage tournait.
2. **Retourner la question** : un `throw` inconditionnel juste après le remplissage, imprimant ce
   qu'il venait d'écrire — PV 1/1, donc l'écriture était bonne.
3. **Aller voir le lecteur** : un `throw` là où la valeur est lue à zéro — bloc 0, image 1, avant
   même le premier `Flip`.
4. **Élargir d'un cran** : dumper *toute* la table et le compteur de la réserve d'emplacements —
   trois entités légitimement allouées, donc pas de mémoire parasite, donc écriture et lecture ne
   désignaient pas le même bloc.

Chaque étape coûte un `throw`, une compilation et un lancement, et **élimine une moitié**. Écrire un
échafaudage qui imprime tout d'un coup coûte plus cher et informe moins.
