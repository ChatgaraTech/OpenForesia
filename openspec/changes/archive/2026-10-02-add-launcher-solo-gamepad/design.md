## Context

Voir proposal.md pour le pourquoi. L'état dont part ce design :

- **Le jeu** cumule, pour chaque joueur, trois tables de `[Controls]` en un mot PS de 16 bits
  (`FoldKeyboardIntoPadState`) : table 0 et table 1 au clavier (scancodes DirectInput), table 2 aux
  boutons de joystick. La table 2 est morte, aucun joystick n'étant créé. Le jeu lit `[Controls]`
  **une fois**, au démarrage, et réécrit le fichier depuis la mémoire à la sortie des Options.
- **L'hôte** (`OpenForesia.Multiplayer`) écrit déjà les touches fantômes des joueurs 2 à 4 dans leur
  table 0, sauvegarde l'ancien contenu dans `OpenForesia.cfg.multijoueur`, lance le jeu en processus
  enfant (`Jeu`), rejoue les mots reçus par `Injection.Appliquer` (`SendInput`, seulement les
  boutons qui changent, seulement jeu au premier plan) et remet le cfg à la fin (`Session`).
- **L'invité** lit ses entrées par `Entrees.Relever` (XInput, puis WinMM ; le clavier seulement si
  sa fenêtre est au premier plan), les traduit par `ConfigManette.Mot` et se configure sur
  `EcranManette`. La configuration vit dans `%APPDATA%\OpenForesia\manette.json`.

Toutes les pièces existent. Le joueur 1, c'est « l'invité sans le réseau ».

## Goals / Non-Goals

**Goals :**
- Une manette pour le joueur 1, seul ou en hébergeant, avec les pièces de l'invité.
- Un seul programme, `OpenForesiaLanceur.exe`, comme porte d'entrée.

**Non-Goals :**
- Reconstruire `FUN_00523230`/`00523340`/`00522fe0`, ou ranimer la page manette d'Options. Si cela se
  fait un jour, voir D3 : la table 2 du joueur 1 devra alors être neutralisée pendant une session
  du lanceur.
- Un second joueur local (une deuxième manette chez l'hôte pour le joueur 2). Les pièces le
  permettraient ; on ne ferme pas la porte, mais ce n'est pas dans ce change.
- Ouvrir la configuration par un raccourci global par-dessus le jeu. C'est un bouton du lanceur,
  atteint par Alt+Tab (décision de Vincent, 2026-10-02).
- Toucher au jeu, ou faire marcher la manette quand `OpenForesia.exe` est lancé directement.

## Decisions

### D1 — Renommer le projet entier, en premier, à comportement constant

L'exe devient `OpenForesiaLanceur.exe`. Le projet suit : dossier `OpenForesia.Lanceur/`, csproj,
`AssemblyName`, espace de noms `OpenForesia.Lanceur`, entrée de la solution. On garde le dossier
`OpenForesia.Multiplayer/` avec un exe renommé ? Ce serait un nom de dossier qui ment, dans un dépôt
où `CLAUDE.md` décrit chaque répertoire par son rôle.

Le renommage est un lot à part, **sans aucun changement de comportement**, fait avant le reste, pour
que le diff des lots suivants ne soit pas noyé dans des renommages. Le titre de l'accueil passe de
« OpenForesia — multijoueur » à « OpenForesia ». Les dossiers internes (`Hote/`, `Invite/`,
`Commun/`…) ne bougent pas. Le code commun aux trois modes reste où il est : `Entrees`,
`ConfigManette` et `EcranManette` restent dans `Invite/` tant qu'aucun lot ne l'exige. Les déplacer
vers `Commun/` est permis, et seulement par déplacement, sans réécriture.

### D2 — Les touches fantômes du joueur 1, dans sa table 1

Quatorze codes nouveaux, pris parmi les 45 que la mesure du 2026-09-30 (`--mesurer-touches`) a
déclarés propres sans les retenir. On les choisit à la suite des 42 premiers, dans l'ordre du
rapport. La mesure est relancée une fois pour confirmer ces quatorze sur la machine de Vincent, puis
la liste est consignée dans `OpenForesia.cfg.md` à côté de celles des joueurs 2 à 4.

**Table 1, pas table 0.** La table 0 du joueur 1 est le clavier principal du joueur, qui doit rester
actif. La table 1 est le clavier de secours (pavé numérique, Inser/Suppr, etc. par défaut) : elle est
sauvegardée puis remise, comme les tables des invités. Pendant une session du lanceur, ce clavier de
secours ne marche donc pas. Le prix est accepté : la table 2 est la seule autre place, et elle est
lue sur le joystick, pas le clavier.

Écrire la table 1 du joueur 1 entre dans la même sauvegarde que celle des joueurs 2 à 4. Le fichier
de sauvegarde devient `OpenForesia.cfg.lanceur`. `Remettre` lit **aussi** l'ancien
`OpenForesia.cfg.multijoueur` s'il existe, pour une installation mise à jour au milieu d'une
session interrompue.

`ConfigTouches.Ecrire` prend la liste des joueurs à écrire : `{1}` en solo, `{1, 2, 3, 4}` en
hébergement. On n'écrit jamais ce qui ne sert pas.

### D3 — Le relais du joueur 1 : une boucle locale, la même chaîne que l'invité

Un fil de relais tourne tant que le jeu tourne, toutes les 4 ms (plus vite qu'une image de jeu à
60 Hz, assez lent pour être négligeable en charge) :

```
Entrees.Relever(actives, fenetre: IntPtr.Zero)    // manettes seules, jamais le clavier
mot = ecranOuvert || !jeu.AuPremierPlan() ? 0 : config.Mot(actives)
applique = Injection.Appliquer(1, applique, mot)
```

- **`fenetre: IntPtr.Zero`** : `Relever` ne lit le clavier que si la fenêtre donnée est au premier
  plan, donc jamais ici. Le clavier du joueur 1 est lu par le jeu lui-même ; le relire ferait
  doublon. Une liaison clavier présente dans `manette.json`, faite pour rejoindre un ami, est donc
  ignorée en local, ce qui est voulu.
- **Mot nul au lieu de « pas d'injection »** : `Injection.Appliquer` relâche ce qui était enfoncé.
  C'est ce qui donne l'exigence « The controller acts only on the game ».
- `Injection.Appliquer` et `ConfigTouches.CodePour` acceptent le joueur 1 (aujourd'hui 2 à 4).
- En hébergement, le relais du joueur 1 tourne à côté du serveur, dans la même `Session`. Le
  premier plan se lit par le même `jeu.AuPremierPlan`.

**Pourquoi pas la table 2 et un « joystick virtuel » dans le jeu ?** C'était la piste du début de
l'exploration. Elle exige de reconstruire puis de dévier dans `Input.cs`. La configuration vivrait
dans le cfg, que le jeu ne relit pas en cours de partie, et l'interface ne pourrait lier que 38
pseudo-touches. Le relais ne touche pas au jeu, accepte une reconfiguration en pleine partie (elle
est lue à chaque tour de boucle), et réutilise un écran déjà éprouvé.

### D4 — Une seule configuration, « ma manette »

Le joueur 1 lit le même `manette.json` que l'invité. Sur un PC, il y a une manette et une façon de
la tenir, qu'on héberge, joue seul ou rejoigne. L'écran écrit à chaque changement ; le relais le lit
en mémoire. L'écran et le relais partagent la même instance `ConfigManette` dans le processus, ce
qui évite de recharger le fichier.

### D5 — Les fenêtres

- **Accueil** : Jouer seul, Héberger, Rejoindre (code, dernier code retenu), Configurer ma manette,
  avec une ligne d'état : « Manette : <libellé de la première manette vue> » ou « aucune manette
  configurée ». Sans le jeu à côté (D8), Jouer seul et Héberger ne sont pas affichés (masqués, pas
  grisés) et la fenêtre se resserre autour de Rejoindre.
- **Partie solo** : petite fenêtre native (mêmes règles que le reste : Win32, sans WinForms) qui
  indique que la partie est en cours et si le relais voit la manette. Elle porte un bouton
  « Configurer ma manette » et se ferme quand le jeu se ferme. La fermer pendant la partie arrête le
  relais (le jeu continue au clavier) et **ne remet pas** le cfg tout de suite (voir D6).
- **Hôte** : la fenêtre existante gagne le bouton « Configurer ma manette » et la ligne d'état du
  joueur 1.
- **L'écran de configuration** est `EcranManette`, ouvert dans une fenêtre du lanceur. Tant qu'il
  est ouvert, le relais envoie le mot nul. Le libellé de fermeture est « Fermer ».

### D6 — Remettre le cfg seulement après la fin du jeu

Le jeu réécrit `[Controls]` depuis sa mémoire quand on quitte ses Options. Si le lanceur remettait le
cfg pendant que le jeu tourne, un passage par les Options y réécrirait les codes fantômes, cette
fois sans sauvegarde. La remise se fait donc **après la fin du processus du jeu**, comme aujourd'hui
pour l'hôte. Si le lanceur s'arrête avant, c'est le lancement suivant du lanceur qui la fait, avant
tout le reste (`Remettre` au démarrage de `Program`, quel que soit le mode).

### D7 — L'installeur

Deux raccourcis (bureau et menu Démarrer) : « OpenForesia » cible `OpenForesia.exe`, comme
aujourd'hui (le jeu, au clavier), et « OpenForesia Lanceur » cible `OpenForesiaLanceur.exe`
(décision de Vincent, 2026-10-02 : les deux programmes restent visibles). À l'installation
par-dessus une version précédente, `OpenForesiaMultiplayer.exe` et les raccourcis « OpenForesia
Multijoueur » sont supprimés. La désinstallation retire les deux noms d'exe et les deux noms de
raccourci du lanceur, pour couvrir une installation jamais mise à jour. `publish-release.ps1` attend
`OpenForesia.exe` et `OpenForesiaLanceur.exe` dans le zip du jeu.

### D8 — Un seul exe, qui se réduit à « Rejoindre » sans le jeu

Le lanceur décide de ses choix au démarrage : `OpenForesia.exe` **dans son propre dossier** donne
les quatre choix, sinon seulement Rejoindre et Configurer ma manette. En développement,
`Jeu.Trouver` garde son repli sur la build Debug du dépôt (remonter jusqu'à `OpenForesia.sln`) :
l'exe lancé depuis `bin\` du dépôt offre donc aussi les quatre choix. Un ami n'a jamais de dépôt
au-dessus de son exe, donc ce repli ne change rien pour lui. L'option `--invite-seul` force le
mode réduit, pour l'essayer dans le dépôt.

**Pourquoi pas un `OpenForesiaLanceurClient.exe` à part ?** Pour la taille, et la mesure le tranche.
L'exe actuel, publié comme le fait `publish-release.ps1` (autonome, élagué, un seul fichier
compressé), pèse **10,3 Mo** (mesuré le 2026-10-02), hôte et invité compris. Tout le code source du
programme fait 285 Ko, et la partie propre à l'hôte (capture, encodeur H.264, son, UPnP, serveur,
touches fantômes) ne fait que du code d'appel vers des API de Windows : quelques dizaines de Ko une
fois compilée. Les 10 Mo sont le runtime .NET élagué, qu'un exe client embarquerait de la même
façon. Un second projet coûterait un second exe à publier, à documenter et à garder cohérent avec le
premier, pour gagner moins de 1 Mo.

**Le levier qui réduirait vraiment la taille** est la compilation native AOT : plus de runtime
embarqué, sans doute quelques Mo. Elle exige les outils de compilation C++ sur la machine qui
publie, et son support de win-x86 est à vérifier. Le code est déjà prêt pour elle (pas de
réflexion, COM généré, JSON généré). C'est hors de ce change ; à mesurer à part si les 10 Mo gênent.

**La publication** sort le lanceur deux fois : dans le zip du jeu (pour l'installeur) et seul, à
côté du Setup (`OpenForesia.Installer\bin\Release\publie\OpenForesiaLanceur.exe`), pour l'ami qui
veut seulement rejoindre. Le même contrôle « aucune donnée du jeu » s'applique aux deux.

## Risks / Trade-offs

- **Le clavier de secours du joueur 1 est coupé pendant une session du lanceur** (D2). → Documenté
  dans le LISEZMOI ; la table 0, que le joueur règle dans les Options, reste entière.
- **Une session interrompue laisse les codes fantômes dans le cfg.** Si le joueur lance ensuite
  `OpenForesia.exe` directement, il garde son clavier principal mais perd son clavier de secours.
  → Le prochain lancement du lanceur remet tout (D6) ; aucun code fantôme ne correspond à une touche
  physique, donc rien ne se déclenche par erreur.
- **Le joueur passe par les Options pendant une session** : les codes fantômes y apparaissent comme
  des touches inconnues, et le jeu les réécrit dans le cfg. → Déjà le cas pour les joueurs 2 à 4 ;
  la remise après la fin du jeu (D6) écrase ce qu'il a écrit.
- **Une manette Xbox paraît à la fois en XInput et en WinMM.** → Déjà géré par l'écran (XInput
  d'abord). Le relais n'applique que les entrées liées, donc pas de doublon.
- **Si un jour le chemin DirectInput du jeu est reconstruit**, la même manette serait lue deux fois
  (table 2 du jeu, avec ses liaisons par défaut, plus les fantômes). → Le lanceur devra alors vider
  la table 2 du joueur 1 dans la même sauvegarde. Le changement qui reconstruira ce chemin doit
  porter la tâche.
- **Les touches injectées dépendent du premier plan**, comme pour l'hôte : l'overlay d'une autre
  application qui prend le focus coupe la manette. → Même règle et même message que l'hôte (« la
  fenêtre du jeu doit rester au premier plan »).

## Migration Plan

1. Le renommage seul (D1) ; Vincent vérifie que Héberger et Rejoindre marchent comme avant.
2. Les codes du joueur 1 et le relais (D2, D3), essayés en solo par `--solo` en ligne de commande.
3. Les fenêtres (D5), puis l'hôte.
4. L'installeur et la publication (D7). Une installation existante est mise à jour par le nouveau
   Setup.

Retour arrière : réinstaller la version précédente. `Remettre` lit les deux noms de sauvegarde, donc
le cfg revient entier dans les deux sens.
