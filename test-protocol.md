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

1. Construit si on lui passe `-Build` (avec le bon `DOTNET_ROOT` — le `dotnet` du PATH n'a pas de SDK).
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
| `-Then A,B` | touches à jouer **une fois l'écran atteint**, dans l'ordre. Jetons : `ENTER` `ESC` `UP` `DOWN` `LEFT` `RIGHT`, et `WAIT<n>` pour patienter n secondes. Exemple : `-Then DOWN,WAIT2,ENTER` |
| `-Trace A,B` | pose `OPENFORESIA_TRACE_A=1`, `OPENFORESIA_TRACE_B=1` (voir plus bas) |
| `-KeepOpen` | laisse le jeu ouvert |
| `-MenuDelay`, `-TapMs`, `-MaxTaps` | réglages fins, rarement utiles |

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

## Pièges d'outillage, hors du jeu

- **`dotnet run --project OpenForesia` ne marche pas.** L'app est en x86, le SDK du PATH n'a que le
  runtime x64. Lancer l'exe directement (`OpenForesia/bin/Debug/net9.0/OpenForesia.exe`), il trouve
  le runtime x86. Pour construire : `$env:DOTNET_ROOT = "C:\Users\Sagara\.dotnet"` d'abord.
- **…et le retirer aussitôt après.** Un processus **x86** qui hérite de ce `DOTNET_ROOT` (x64) meurt
  avant d'ouvrir sa fenêtre, sur `Failed to load hostfxr.dll … 0x800700C1`. `playtest.ps1 -Build`
  restaure donc l'environnement entre la construction et le lancement — piège rencontré, pas
  théorique.
- **`OpenForesia.Tools` ne tourne pas du tout**, même cause. Palliatif pour la table d'exports :
  `OpenForesia.Tools/exports.ps1`.
- **Lire le binaire ne demande aucun outil.** `od -A x -t x4 -j <offset> -N <taille> Legacy/Blaze.exe`
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
