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
| `PGDN` | 2 | `0x0400` L1 | 81 / 209 | Pg. suiv. — **recule** d'une page dans le menu |
| `PGUP` | 3 | `0x0800` R1 | 73 / 201 | Pg. préc. — **avance** d'une page dans le menu |

**Les deux dernières lignes sont inversées par rapport à ce que leurs noms suggèrent**, et c'est
dans la table de liaisons du binaire : L1, qui recule d'une page, est lié à PageDown ; R1, qui
avance, à PageUp. Ajoutées le 2026-08-25 pour atteindre les pages 1 à 5 du menu de jeu.

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

### ~~Le même échec **pendant un chargement**~~ — CORRIGÉ le 2026-08-24

Signalé deux fois par Vincent : l'échec s'est produit **en cours de partie**, pas au lancement. La
seconde occurrence donnait `No free block of 0x2000 bytes`.

**Ce cas n'existe plus.** La récupération — le jeu se relance en processus fils quand il perd la
course — n'existe **qu'au démarrage** : `TryPrepareDisplayListArena` est appelée par l'initialiseur
de module, et son `catch (InvalidOperationException)` est ce qui permet la relance. Une
`AllocLowZeroed` qui échouait en cours de partie n'avait rien pour la rattraper.

Il y a **neuf** sites d'allocation basse. Quatre étaient pré-réclamés au démarrage, **cinq étaient
pris paresseusement** — et les trois premiers de cette liste sont pris exactement pendant un
chargement de zone, c'est-à-dire au pire moment : le runtime .NET a eu plusieurs secondes pour
grandir dans la même fenêtre de 16 Mo. Les cinq ont rejoint la liste pré-réclamée.

| site | taille arrondie | quand |
|---|---|---|
| `s_UiDescriptors_00560ff0` | `0x2000` | démarrage (déjà) |
| `s_StatusDescriptor_00560728` | `0x1000` | démarrage (déjà) |
| `s_MenuContext_0056dd58` | `0x1000` | démarrage (déjà) |
| `s_SelectSlots_00573518` | `0x5000` | démarrage (déjà) |
| `s_LoadScreenDescriptors_0056b558` | `0x2000` | **chargement de zone** → démarrage |
| `s_ItemMenuDescriptors_00564fe0` | `0x2000` | menu objets → démarrage |
| `s_WorldPanels_005dbc68` | `0x3000` | carte du monde → démarrage |
| `s_WorldMapDescriptor_005633b0` | `0x1000` | carte du monde → démarrage |
| `s_DestinationDescriptor_0056c978` | `0x1000` | choix de destination → démarrage |

**La table taille → site de la version précédente de cette note était fausse** (« `0x4000` =
l'écran de chargement » : il fait 2 × `0x8c8` = `0x1190`, donc `0x2000`), et **trois** sites
arrondissent à `0x2000`. Le message d'échec ne permettait donc pas de désigner le coupable. Il le
nomme maintenant, via `[CallerMemberName]`, et il donne le relevé de la fenêtre :

```
No free block of 0x2000 bytes below 0x01000000 for BuildLoadScreenDescriptors.
Libre sous le plafond : 0x33B000 octets au total, plus grand intervalle 0x1A0000.
```

Total libre et plus grand intervalle distinguent les deux causes, qui n'appellent pas la même
suite : fenêtre **pleine** (la contrainte est la contrainte) contre fenêtre **fragmentée** (il reste
de la place mais aucun intervalle d'un seul tenant).

### La ligne `[BAS]` du démarrage, et sa garde

Chaque lancement imprime désormais la marge restante une fois toutes les réservations basses faites :

```
[BAS] fenetre <0x01000000 apres reservations : libre 0x7D1000 octets, plus grand intervalle 0x690000
```

Relevés du 2026-08-24, trois lancements consécutifs sans rien changer : **0x7D1000, 0xB0B000,
0x33B000** libres, plus grands intervalles 0x690000, 0xA50000, **0x1A0000**. La variation est
énorme — c'est l'ASLR, et c'est bien le mécanisme de la panne. Sur le troisième lancement il ne
restait qu'1,6 Mo d'un seul tenant : l'arène en demande 2,6, donc ce lancement-là aurait perdu la
course si l'arène n'avait pas été servie en premier.

Une seconde ligne n'apparaît que si un **dixième** site d'allocation basse est ajouté un jour sans
rejoindre la liste pré-réclamée :

```
[BAS] ATTENTION : <nom> alloue bas APRES le demarrage, hors de la portee de la relance.
```

Elle n'interrompt rien — l'allocation, elle, a réussi. Elle signale que le site court à nouveau le
risque que la pré-réclamation supprime. **Si elle apparaît, ajouter le site à la liste.**

**Conduite à tenir**, désormais : un échec au démarrage est rattrapé tout seul (trois tentatives,
ligne `lost the race for low memory` dans le journal). Un échec qui traverse malgré tout veut dire
que les trois tentatives ont perdu — relever la ligne complète, elle nomme le site et donne la
marge.

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

---

## Vérifier le son sans oreille (2026-08-24)

Le son ne laisse aucune trace dans la capture ni dans le débit de primitives. Un agent ne peut donc
pas conclure « la musique marche » d'un run vert — et une reconstruction audio complète mais
**inerte** rend exactement le même verdict qu'une reconstruction qui joue. C'est ce qui a laissé
`REVERSE_STATUS` #26 endormi pendant des semaines.

Trois preuves, du moins au plus fort, toutes lisibles dans le journal :

1. **`[AUDIO] DirectSoundCreate -> 0x00000000`** — le périphérique existe. Une ligne absente veut
   dire que le rappel de l'ordinal 790 n'a jamais été déclenché ; un `0x80070057` sur la ligne
   `SetCooperativeLevel` veut dire qu'on lui a donné une fenêtre nulle.
2. **`[AUDIO] <chemin> : … statut 0x5`** — `IDirectSoundBuffer::GetStatus` rend
   `DSBSTATUS_PLAYING | DSBSTATUS_LOOPING`. C'est la preuve que le tampon **joue**, pas seulement
   qu'il a été créé. `0x0` ou `0x1` seul est un échec.
3. Pour les bruitages, une sonde jetable dans `PlaySoundEffect` qui imprime `decalage` et
   `longueur` de l'entrée du répertoire : des valeurs **non nulles** prouvent que la banque `Snd\`
   est chargée et que l'échantillon existe. Zéro des deux signifie répertoire vide — le défaut
   d'aliasing de `s_SoundBankHeader_005e8520` pour les identifiants `0x3c`..`0x4c`.

Le menu ne se laisse pas observer par `-Entry -1` (voir plus haut : on reste sur l'attract, que
rien ne congédie). Pour entendre le thème du menu il faut `-Entry 0`, qui traverse la liste au
clavier : les frappes de navigation sortent de l'attract, et `Title01.WAV` démarre pendant que le
curseur se déplace, avant la validation.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 20 -WorkingDir OpenForesia -Then WAIT5
```

Repère du 2026-08-24 : `Title01.WAV` au menu, `Menu.WAV` à la constitution du groupe,
`Mel00_01.WAV` à la carte du monde — trois lignes `statut 0x5`, tampon de 323 840 octets à chaque
fois, atténuation −1250 pour `CdVol=64`.

Dernière vérification, gratuite : **le titre de la fenêtre**. Il vaut `OpenForesia` tant que
l'initialisation du son n'a pas eu lieu, et `Blaze & Blade` dès qu'elle a lieu — c'est
`SetWindowTextA`, la première instruction utile de `LAB_00512ef0`. La capture le montre.

---

## Se déplacer dans l'auberge, et la boîte de dialogue qui l'en empêche (2026-08-24)

Pour atteindre une autre pièce que la salle d'arrivée, il faut **d'abord refermer la boîte de
dialogue**. La séquence habituelle de `TODO.md` se termine par deux `ENTER` qui ouvrent la boîte
« - La réception - », et cette boîte est **modale** : elle gèle le monde, si bien que tout
`HOLD:RIGHT` qui la suit ne déplace rien du tout. La capture montre alors le personnage exactement
où il était, ce qui se lit à tort comme « le déplacement ne marche pas ».

Trois `ENTER` puis `ESC` la referment. Ensuite `HOLD:`/`FREE:` déplacent réellement :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 70 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT6,ENTER,WAIT2,ENTER,WAIT2,ESC,WAIT3,`
HOLD:UP,WAIT8,FREE:UP,WAIT2,HOLD:RIGHT,WAIT10,FREE:RIGHT,WAIT4
```

rend **la boutique de l'herboriste** : quatre PNJ, un comptoir, des étagères, et le dialogue
« L'herboriste: Bonjour ! ». Le débit passe de 557 à **1051 primitives par image** — une pièce plus
chargée que la salle d'arrivée, ce qui est en soi la confirmation qu'on a changé de pièce.

### Ce que ce trajet ne permet PAS de faire

**Atteindre une pièce précise n'est pas fiable en frappes minutées.** Deux exécutions de la même
séquence n'aboutissent pas au même endroit — 1051 primitives par image la première fois, 698 la
seconde. Le personnage part d'une orientation qui dépend de ce qui a précédé, et les murs arrêtent
la marche à des instants différents.

Conséquence pratique : ce trajet sert à **exercer du code de zone**, pas à **reproduire un
symptôme localisé**. Quand un plantage est signalé « dans la seconde grande pièce à droite », ne pas
conclure de son absence ici qu'il est corrigé — la séquence n'établit pas qu'on y est allé. Le dire
plutôt que de le laisser croire.

---

## Les tests de navigation en jeu : c'est Vincent qui les fait (2026-08-24)

**Décision de Vincent, prise le 2026-08-24 et valable pour la suite** : dès qu'un test demande de
**se déplacer** dans une carte — atteindre une pièce de l'auberge, changer de zone, reproduire un
symptôme localisé — **c'est lui qui l'exécute**. Conduire le personnage par frappes minutées est
devenu trop compliqué et trop long ; il va plus vite à la main, et cela économise du temps de
session.

Ce n'est pas une préférence de confort : la section précédente le mesure. Deux exécutions de la même
séquence `-Then HOLD:/FREE:` finissent à des endroits différents, et une absence de plantage sur un
tel trajet **n'établit rien** sur la pièce visée.

### Le partage, concrètement

| À la charge de l'agent | À la charge de Vincent |
|---|---|
| `dotnet build` | tout ce qui demande de conduire le personnage |
| les lancements de `playtest.ps1` **sans déplacement** — un écran s'ouvre, le rendu tourne, le démarrage ne casse pas | atteindre une pièce précise, changer de carte |
| lire la capture et le verdict | reproduire un symptôme localisé, et confirmer qu'il a disparu |

Quand une correction ne se vérifie qu'en jouant : la finir, la documenter, **dire franchement
qu'elle n'est pas exercée**, et demander à Vincent de refaire le trajet. Ne pas essayer de le
rejouer au clavier.

---

## Ouvrir le menu de jeu : **l'agent peut le faire** (2026-08-25)

**F2 est le jeton `START` de ce script** — action 11 de la section `[Controls]` d'`OpenForesia.cfg`,
scancode `0x3C`. Il n'y a rien a ajouter au script.

Et surtout : **le menu s'ouvre sur place, sans conduire le personnage.** Il ne tombe donc pas dans
le partage du 2026-08-24 — c'est un test que l'agent execute lui-meme, comme un ecran atteint depuis
le menu principal.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 40 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT6,ENTER,WAIT2,ENTER,WAIT2,ESC,WAIT3,`
START,WAIT8
```

Les quatre `ENTER` puis `ESC` referment la boite de dialogue de la reception — elle est modale et
gele le monde, donc le `START` final ne serait pas vu autrement. Le `START` du debut, lui, est celui
qui lance la partie depuis le menu principal : le meme jeton sert aux deux, a deux instants
differents.

**Ce que cela debloque** : les six pages du menu (`FUN_0052a850`) et leurs sous-onglets se
verifient a l'ecran par l'agent, une par une. C'est le premier gros morceau de reconstruction depuis
la carte du monde dont l'oracle soit a portee sans deplacement.

### Ce qu'on voit aujourd'hui

La charpente du menu tourne — navigation entre pages, fermeture, cadrage — et la page demandee leve
en se nommant. Un `PLANTAGE` dont le message dit `FUN_0052a850, page N` est donc le comportement
attendu tant que les corps de page ne sont pas transcrits, **pas** une regression.

### Ce qu'on voit depuis le 2026-08-25 (suite)

La **fiche de personnage** s'affiche : portrait, nom, classe, élément, niveau, or, PV/PM, les sept
caractéristiques, les quatre résistances, l'expérience. Trois sequences, trois verdicts attendus :

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT10` | aucun plantage, ~509 primitives par image, la fiche est a l'ecran |
| `START,WAIT4,RIGHT,WAIT6` | `PLANTAGE : … page 0, onglet 1` — c'est le selecteur d'onglets qui marche |
| `START,WAIT5,START,WAIT8` | aucun plantage, le menu se referme, le monde reprend a ~1105 |

**Un `PLANTAGE` dont le message nomme une page ou un onglet est le comportement attendu**, pas une
regression. Ce qui serait une regression : un `0xC0000005`, un debit nul, ou un message nommant
autre chose.

### Le fond du menu n'est pas dessine par le menu

La branche courte **ne redessine pas le monde** : ce qu'on voit derriere les panneaux est l'image
precedente restee dans le tampon, et elle apparait teintee. Ne pas le lire comme un defaut de
rendu du menu — et ne pas conclure non plus que c'est juste. **La fidelite de cette teinte contre le
jeu de 1998 n'est pas verifiee**, et elle demande le jeu d'origine : c'est un test pour Vincent.

### L'inventaire, depuis le 2026-08-25 (suite)

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,RIGHT,WAIT8` | aucun plantage, ~498 primitives par image, la grille d'objets et la fiche de l'objet vise |
| `START,WAIT4,RIGHT,WAIT4,ENTER,WAIT4,RIGHT,WAIT8` | aucun plantage, ~499 — le curseur entre dans la grille et se deplace, la fiche suit |

**Les deux mouvements ne se pilotent pas par le meme mot de manette.** Le changement d'onglet lit
`pad[1]` (le mot a `+2`), le deplacement dans la grille lit `pad[3]` (celui a `+6`). Un `RIGHT`
n'agit donc pas au meme endroit selon qu'on a valide ou non — c'est le comportement de l'original,
et c'est ce qui rend la seconde sequence lisible : le premier `RIGHT` change d'onglet, `ENTER` entre
dans la grille, le second `RIGHT` y deplace le curseur.

### L'equipement, depuis le 2026-08-25 (suite)

| Fin de `-Then` | Verdict attendu |
|---|---|
| `RIGHT,WAIT3,RIGHT,WAIT8` | ~470 primitives par image — les cinq rangees d'equipement |
| `RIGHT,RIGHT,ENTER,WAIT3,DOWN,WAIT3,ENTER,WAIT8` | ~498 — la grille s'ouvre sur « Choisis ce que tu aimerais echanger. » |
| `RIGHT,RIGHT,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT8` | ~471 — l'epee est **retiree**, la rangee repasse a « ­ARME­ » |

### La configuration de la manette (onglet 5), depuis le 2026-08-25 (suite 6)

| Fin de `-Then` | Verdict attendu |
|---|---|
| `RIGHT,WAIT3,RIGHT,WAIT3,RIGHT,WAIT10` | aucun plantage, ~487 primitives par image — trois rangees (« CONTROLE », « DIRECTION », « JOUEUR »), « Type A », « Activé », « Activé », et le panneau « Fenêtre Contrôle » |
| `…,RIGHT,WAIT4,ENTER,WAIT3,DOWN,WAIT2,DOWN,WAIT2` | ~488 — le curseur (une main) apparait a gauche et descend jusqu'a la troisieme rangee |
| `…,ENTER,WAIT3,RIGHT,WAIT2,DOWN,WAIT2,RIGHT,WAIT2,DOWN,WAIT2,LEFT,WAIT6` | ~488 — « Type B », et les **deux bascules passent a « Désactivé »** |

**`ENTER` ne change rien dans cet onglet, et c'est normal.** Les trois rangees se modifient a
**gauche/droite** (masques `0x80` et `0x20` du mot presse), pas au bouton de validation : `ENTER`
vaut `0x8000`, qui n'est dans aucun des deux masques. La sortie de l'onglet est `ESC` (`0x4000`),
pas `ENTER` non plus. Une sequence qui martele `ENTER` ici rend une capture identique a la
precedente et se lit a tort comme « rien ne repond ».

### La page « Objets » (page 1), depuis le 2026-08-25 (suite 7)

**Elle ne s'atteint pas sur une partie neuve.** Le prologue saute la page tant que
`DAT_005e9840` est nul, et sur une partie qui commence le groupe n'a aucun objet de quete : un
`PGUP` depuis la page 0 tombe alors sur la **page 3** (la page 2 se saute aussi, faute du bit `0x10`
de `DAT_005e9001`). Ce verdict est utile en soi — il mesure les deux conditions d'un coup.

**Pour la voir, forcer l'etat le temps d'un run.** C'est ce qui a nomme la page :

```csharp
// en tete de RunPartyMenu_0052A850, A RETIRER apres le run
*(int*)Mem.Arena(0x005e9840) |= 0x0f;
```

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,PGUP,WAIT10` | ~383 primitives par image, « - Objets - » et la liste des objets de quete |
| `…,PGUP,WAIT4,ENTER,WAIT3,RIGHT,WAIT3,RIGHT,WAIT8` | ~384 — l'entree visee passe au **jaune** et le panneau du bas donne sa description |

La selection n'est pas un curseur dessine mais un changement de couleur pose par deux codes de
controle dans la chaine. Une capture ou rien ne semble selectionne veut donc dire que la couleur
n'a pas ete emise, pas qu'un sprite manque.

**Retirer la sonde et refaire un run** avant de conclure : `START,WAIT10` doit rendre ~509
primitives par image et la fiche de personnage.

### La page « Table » (page 2), depuis le 2026-08-25 (suite 8)

Meme situation que la page 1 : elle se saute sur une partie neuve, faute du bit `0x10` de
`DAT_005e9001`. Mais **la sonde n'est pas au meme endroit**, et c'est le piege :

```csharp
// PAS ICI : DAT_005e9000 est reconstruit par FUN_00535710, que le prologue rappelle
// juste avant de tester le saut de page. Une sonde posee dans le menu est effacee.
// La bonne place est dans RefreshPartyAbilityMask (SaveSlots.cs), apres la remise a zero :
accumulator[0] |= 0x13ff;   // bits 0..9 = les cristaux + la dalle, bit 12 = la page
```

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,PGUP,WAIT8` | ~391 primitives par image, « - Table - », « Page Cristaux », la dalle gravee et ses neuf cristaux |

La page ne lit **aucune touche** : une capture identique apres un `-Then` charge de frappes est le
comportement normal, pas un signe que les touches se perdent.

**Regle generale que ce run a coutee** : avant de sonder une adresse pour atteindre un ecran,
chercher qui la reecrit dans la meme image. Un cumul recalcule par frame ne se force pas depuis
l'exterieur — il faut sonder dans la fonction qui le calcule.

### La page « Organisation du groupe » (page 3), depuis le 2026-08-25 (suite 9)

**Elle s'atteint sans sonde** : sur une partie neuve, un `PGUP` depuis la page 0 y tombe
directement, les pages 1 et 2 se sautant.

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,PGUP,WAIT8` | ~388 primitives par image, « - Organisation du groupe - », le portrait, la marque de meneur, « ◀1P▶ », « Couleur 0 » |
| `…,PGUP,WAIT4,UP,WAIT2,UP,WAIT2,ESC,WAIT6` | ~388, « Couleur 2 » et **les cheveux du portrait passent de bleu a brun** ; la croix ne fait rien |

Les deux verdicts se lisent ensemble : le second est ce qui a prouve que l'octet `+0x2bd` est une
**couleur** et non un rang de formation. Un compteur qui monte de 0 a 2 n'aurait rien prouve tout
seul — c'est le portrait qui change qui tranche. Meme lecon que le carrousel de selection : la
capture est la mesure, pas le nombre.

**La croix qui ne fait rien est le comportement attendu** avec un groupe d'un seul membre : elle est
refusee tant que `DAT_005de034` est sous 2.

### La carte du monde (page 4), depuis le 2026-08-25 (suite 10)

**Deux `PGUP` depuis la page 0** sur une partie neuve : les pages 1 et 2 se sautent, le premier
`PGUP` donne la page 3 et le second la page 4.

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,PGUP,WAIT3,PGUP,WAIT8` | ~314 primitives par image, le continent, la fleche rouge du groupe, la rangee des huit emplacements de repere |
| `…,PGUP,WAIT4,HOLD:RIGHT,WAIT3,FREE:RIGHT,WAIT6` | ~313 — **le continent sort du cadre par la gauche** et la fleche du groupe disparait avec lui |
| `…,PGUP,WAIT4,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT8` | ~316 — le curseur de liste apparait et un repere **orange** se pose sur le groupe |

**Cette page se pilote au `HOLD:`, pas au tapotement.** Ses quatre directions sont les seules du jeu
lues sur le mot **0** de la manette — l'etat *tenu* — a raison de quatre unites par image. Un `RIGHT`
de 150 ms ne deplace la carte que de trente-six unites, ce qui ne se voit pas ; trois secondes de
`HOLD:RIGHT` la sortent du cadre.

**Le nombre de primitives est plus bas que sur les autres pages, et c'est normal** : la carte n'a ni
portrait ni texte. ~314 contre ~509 sur la fiche.

### La page « Options » (page 5), depuis le 2026-08-25 (suite 11)

**Trois `PGUP` depuis la page 0** sur une partie neuve. C'est la derniere page du menu.

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,PGUP,WAIT4,PGUP,WAIT4,PGUP,WAIT10` | ~407 primitives par image, « - Options - », « Sauvegarde », « Quitter », le portrait et le nom du membre |
| `…,PGUP,WAIT4,DOWN,WAIT2,ENTER,WAIT2` | « Retour au menu d'accueil ? » et le curseur oui/non ; **ne pas repondre oui**, le jeu quitterait |
| `…,PGUP,WAIT4,ENTER,WAIT4,ENTER,WAIT4` | « Mémorisation achevée. » — et **un fichier neuf dans `OpenForesia/save/`** |

**La sauvegarde ecrit un vrai fichier.** Ce port remplace la carte memoire par des fichiers de
`save\` : apres le troisieme run, `ls OpenForesia/save/` doit montrer un `Blaze-Saving*` de
**24576 octets** (0x6000) dont l'horodatage est celui du run, et dont les quatre premiers octets
sont `53 43 11 03`. C'est la meilleure verification de la page — un artefact sur le disque plutot
qu'une capture qui ressemble a ce qu'il faut.

### Le nombre de primitives identifie la page

Utile quand un `-Then` semble n'avoir rien fait : chaque page du menu a sa signature.

| Page ou onglet | Primitives par image |
|---|---|
| page 0, la fiche (onglet 0) | ~509 |
| onglet 4, les herbes | ~531 |
| onglet 3, les sorts | ~484 a 490 |
| onglet 5, la manette | ~487 |
| page 5, « Options » | ~407 |
| page 2, la dalle aux cristaux | ~391 |
| page 3, l'organisation du groupe | ~388 |
| page 1, les objets de quete | ~383 |
| page 4, la carte du monde | ~314 |

Un verdict « aucun plantage, 509 par image » sur une sequence qui demandait trois `PGUP` veut dire
que **la sequence n'a jamais ete vue** — le jeu est reste sur la page 0. C'est arrive le
2026-08-25 **deux fois** ; chercher aussi `relance detectee` dans le journal, et relancer plutot que
conclure.

### Les onglets 3 et 4, depuis le 2026-08-25 (suite 12)

**Ils ne s'atteignent pas avec le personnage de depart** : le Guerrier n'a que les onglets 0, 1, 2
et 5. Une sonde d'une ligne suffit, et elle est plus honnete que de forcer l'onglet directement —
seul le choix d'onglet change, le personnage reste un Guerrier et lit ses vraies donnees :

```csharp
// dans RunCharacterSheetPage, juste apres `int classRow = character[0x150] & 7;`
classRow = 1;   // la ligne 1 donne {0,1,2,3,5} -> un troisieme RIGHT mene a l'onglet 3
classRow = 4;   // la ligne 4 donne {0,1,2,4,5} -> un troisieme RIGHT mene a l'onglet 4
```

| Fin de `-Then` | Verdict attendu |
|---|---|
| `START,WAIT4,RIGHT,WAIT3,RIGHT,WAIT3,RIGHT,WAIT8` (sonde 1) | ~490 primitives par image — « Niveau Magie 1/6 », « Balle de feu », « MA 2 », « Fenêtre Spécialité » |
| `…,ENTER,WAIT3,UP,WAIT2,RIGHT,WAIT6` (sonde 1) | ~484 — « 2/6 », cinq « ? » et « Cette capacité n'a pas encore été développée » |
| `…,ENTER,WAIT3,DOWN,WAIT2,ENTER,WAIT6` (sonde 4) | ~531 — les sept herbes, leur compte, le marqueur et la description |
| `START,WAIT4,RIGHT,WAIT3,RIGHT,WAIT3,RIGHT,WAIT8` (sonde retiree) | ~487 — l'onglet 5, la configuration de la manette |

Le dernier est la **verification de retrait** : sans la sonde, le troisieme `RIGHT` du Guerrier donne
l'onglet 5, pas le 3.

### Compter les `RIGHT` : la classe decide, pas la position

**Le personnage de depart (Guerrier) n'a que quatre onglets — 0, 1, 2 et 5.** Un troisieme `RIGHT`
depuis l'onglet 0 ne mene donc PAS a l'onglet 3 : il mene au **5**. Mesure le 2026-08-25, et
conforme a ce que `s_ClassTabs_0056a898` annonce.

Consequence pratique : **les onglets 3 et 4 ne sont pas atteignables avec ce personnage.** Il faut
une autre classe — la ligne 1, 3, 5 ou 7 de la table pour le 3, la ligne 4 pour le 4. Avant de
compter des `RIGHT`, lire la ligne de la classe dans `s_ClassTabs_0056a898`
(`Screens/State03/WorldMapMenu.cs`).

---

## Une sonde de comptage vaut mieux qu'une deduction sur le trajet (2026-08-25)

En corrigeant les racines de ressources du moteur de texte, il fallait savoir si le trajet de
l'auberge exercait bien le code touche. La deduction naturelle — « la boite " - La reception - "
est du texte, donc elle passe par la », — **est fausse**. Une sonde d'une ligne dans la fonction
corrigee l'a montre en un run :

| sequence `-Then` | appels a la fonction corrigee |
|---|---|
| `…,ENTER,WAIT2,ENTER,WAIT4` (la boite de reception seule) | **0** |
| `…,ENTER,WAIT2,ESC,WAIT3,START,WAIT8` (+ le menu de jeu) | 2 358 |

La boite de reception traverse le moteur de texte sans jamais indexer de racine ; ce sont les pages
du **menu de jeu** qui le font. Un « aucun plantage » sur le premier trajet n'aurait donc rien
prouve du tout.

**La regle**: quand une correction doit etre *exercee* et pas seulement *compilee*, poser une sonde
qui **compte les passages**, et lire ce compte. Un verdict `aucun plantage` ne dit pas que le code
corrige a tourne — il dit que rien n'a plante, ce qui est vrai aussi quand il n'a pas tourne.

**Le corollaire honnete** : si la sonde rend 0 sur tous les trajets accessibles a l'agent, la
correction n'est pas verifiee, et il faut le dire — voir le partage du 2026-08-24.

---

## Forcer l'etat plutot que d'essayer de l'atteindre (2026-08-25)

Suite directe de la section precedente. Le plantage de `DrawChunk` (LB-013) ne survient que dans un
**octant de cap de camera** sur huit. Le trajet de l'auberge le montre en deux runs : le chemin
concerne tourne bien, ~4 400 appels, mais **toujours dans l'octant 2**. Amener la camera dans
l'octant 7 au clavier n'est pas fiable — c'est le constat du 2026-08-24.

Une **sonde d'une ligne** le donne directement, juste apres le calcul :

```csharp
int octant = (((ushort)pose[1] + 0x100) >> 9) & 7;
octant = 7;   // SONDE JETABLE
```

Le rendu devient faux — ce n'est pas le bon cone de vue — mais ce n'est pas ce qu'on mesure : on
mesure qu'un chemin s'execute sans lever.

### Les deux sondes, et laquelle prouve quelque chose

| sonde | ce qu'elle rend | ce qu'elle prouve |
|---|---|---|
| forcer l'etat + compter les appels | ~2 800 appels, aucun plantage | que rien ne casse **si** le chemin est bien celui-la |
| tracer **dans la branche corrigee** | 2 314 lectures, toutes au meme curseur | que la branche corrigee est **empruntee** |

La seconde est la bonne. La premiere peut se satisfaire d'un chemin voisin ; la seconde nomme la
ligne. Quand une correction tient en une branche, **poser la sonde dans la branche**, pas a l'entree
de la fonction.

### Ce qu'il faut dire ensuite

Forcer un etat prouve que le code ne plante plus **dans cet etat**. Cela ne prouve pas que le jeu
atteignait cet etat-la. Les deux phrases sont differentes et il faut ecrire la seconde : ici, c'est
a Vincent de refaire le dialogue de biais.

---

## Verifier une cascade par extraction des deux cotes (2026-08-25)

Certains corps ne se verifient ni a l'ecran ni par une sonde : ils sont atteints dans une piece que
l'agent ne peut pas atteindre, et leur contenu est une **cascade de constantes**. Les corps de reveil
d'entites de scene en sont le type meme :

```c
if (etat == 1) FUN_00404fe0(puVar1, 0);
if (etat == 2) FUN_00404fe0(puVar1, 1);
…
```

Relire ca a l'oeil est exactement la tache ou l'on intervertit deux nombres sans le voir. Extraire les
triplets `(etat, primitive, argument)` **des deux sources** et les comparer coute deux commandes :

```bash
# cote Blaze.c
sed -n '2746,2784p' Legacy/Decompiled/Blaze.c |
  grep -oE "0x3fffffff\) == [0-9]+|FUN_00404fe0\(puVar1,[0-9]+\)|FUN_00405420\(puVar1,[0-9]+\)" | paste - -

# cote reconstruction
awk '/private static void WakeSceneEntity_00405DA0\(int index\)/,/^    }$/' \
    OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs |
  grep -oE "== [0-9]+\)|AdvanceSceneEntityState_00404FE0\(record, [0-9]+\)" | paste - -
```

Les deux colonnes se lisent cote a cote en un coup d'oeil, et une inversion saute aux yeux.

**Quand s'en servir** : des qu'un corps est une table deguisee en code — cascades d'etats, tables de
sauts, listes de constantes — et que l'oracle a l'ecran n'est pas a portee. Ce n'est pas un
remplacement du test en jeu ; c'est ce qui evite de livrer une cascade fausse **en disant** qu'elle
n'est pas verifiee.

---

## `-Trace VERBS` : savoir quel verbe un decor declenche (2026-08-25)

**Un script de zone ne se lit ni dans `Blaze.c` ni au desassembleur.** Les tables de verbes sont
installees par zone, et les scripts eux-memes vivent dans les donnees du disque. Quand un decor
« ne fait rien » ou fait autre chose que prevu, aucune lecture statique ne dit quel verbe il appelle.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 60 -WorkingDir OpenForesia -Trace VERBS -Then …
```

Chaque appel a l'opcode 0 imprime une ligne :

```
[VERBE] 3 -> 0x442F60  operande 0x0000
[VERBE] 10 -> 0x4441F0  operande 0x0000
```

— le numero du verbe, l'adresse que la table de la zone courante lui donne, et l'operande de 16 bits
que le script passe. Sur le trajet d'arrivee de l'auberge, exactement deux verbes s'allument.

**A quoi ca sert concretement** : poser le geste (parler, ouvrir un coffre, toucher un panneau), lire
la ou les lignes qui apparaissent, et on sait **quelle fonction transcrire**. C'est la seule facon
d'aller du symptome au corps sans deviner — et deviner coute cher : le panneau de
`FUN_0053e0a0` a ete pris pour le coffre de stockage sur la seule foi de son voisinage dans la table.

La trace est **permanente**, pas jetable : elle repond a une question qui reviendra a chaque decor
nouveau.

---

## Les cinq sondes permanentes : du geste en jeu au corps a ecrire (2026-08-25)

Le coffre de la chambre « ne faisait rien ». Aucune lecture statique ne pouvait le dire : le
declencheur est un script du disque, la table de verbes est installee par zone, et le symptome etait
un silence. Cinq sondes ont ete posees, chacune parce que la precedente ne suffisait pas, et **chacune
a corrige une conclusion trop rapide**. Elles restent dans le code.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 40 -WorkingDir OpenForesia -Trace VERBS,SCRIPT,ENTITY -KeepOpen
```

| ligne | source | ce qu'elle repond |
|---|---|---|
| `[VERBE] n -> 0x…` | `ScriptOpAreaVerb` | quel verbe de zone l'opcode 0 appelle |
| `[SCRIPT] <appelant> index n` | `RunScript` | quel script part, et **d'ou** — le nom de l'appelant est pris par `CallerMemberName` |
| `[OPCODE] n` | `RunScript` | ce que le script execute |
| `[ETAT] image=… pas=… x=… z=…` | la boucle de monde | si le monde tourne, ou l'on est, si le meneur bouge |
| `[TOUCHE] image=… boutons=…` | la boucle de monde | **quand le joueur appuie** |
| `[ALU]` · `[SAUT]` · `[CHOIX]` | l'ALU de script, le saut, la validation de panneau | ce que le script a lu et decide |
| `[SOUCHE] <creneau>` | `DispatchEntityHandler` | quels creneaux retombent sur la fonction vide |

### L'ordre dans lequel on les lit

1. **`[TOUCHE]`** — l'appui est-il vu ? Sans lui, « rien ne se passe » et « l'appui n'arrive pas »
   ont exactement la meme trace. C'est la sonde qui manquait le plus longtemps.
2. **`[SCRIPT]` / `[OPCODE]`** — quelque chose part-il ?
3. **`[ALU]` / `[SAUT]` / `[CHOIX]`** — que decide-t-il, et sur quelle valeur ?
4. **`[SOUCHE]`** — le cas silencieux : un creneau qui ne fait rien *et ne le dit pas*.
5. **`[ETAT]`** — le contexte, quand rien de ce qui precede ne s'allume.

### Deux pieges payes en route

**Une sonde a l'entree d'une fonction ne compte pas les images.** `RunWorldMapTick` contient
elle-meme la boucle d'images : elle n'est entree qu'une fois par chargement de zone. La sonde
posee a son entree ne s'est allumee qu'une fois, et m'a fait croire un moment que la fonction ne
tournait pas. **Verifier ou est la boucle avant de compter.**

**« Aucun script » ne veut pas dire « le monde est arrete ».** Une salle sans script en boucle donne
le meme silence qu'une simulation figee. C'est `[ETAT]` avec la position du meneur qui departage, pas
le comptage des scripts.
