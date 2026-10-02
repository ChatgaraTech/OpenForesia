## Why

*Blaze & Blade* se joue à quatre sur un même PC. Vincent veut y jouer avec trois amis à distance,
**clé en main**, à la manière du streaming de RomStation : l'hôte clique « Héberger », le jeu se
lance, les amis rejoignent avec un code et jouent avec leur manette ou leur clavier. Sans logiciel
tiers (Parsec, Moonlight, Steam), sans serveur à nous, et **sans rien changer au jeu**.

L'exploration du 2026-09-30 a établi que c'est possible : le jeu lit, pour chacun des quatre joueurs,
des touches clavier configurables (`[Controls]`, tables 0 et 1) qu'il traduit en manette PlayStation
(16 bits : croix, rond, triangle…). Un outil qui attribue des touches aux joueurs 2 à 4 et en
simule l'appui pilote ces joueurs exactement comme le ferait un clavier. Le seul verrou du jeu sur
les joueurs 2 à 4 (`DAT_005de168`) ne s'applique qu'au Menu objets de l'écran titre, comme en 1998
avec de vraies manettes.

## What Changes

- **Un nouveau programme, `OpenForesiaMultiplayer.exe`**, hors reconstruction : un seul exe pour
  héberger et rejoindre, natif Windows, sans dépendance hors de Windows et de .NET.
- **Héberger** : attribuer aux joueurs 2 à 4 des touches « fantômes » (codes clavier qu'aucun
  clavier français ne porte et qui ne produisent aucun caractère) dans `OpenForesia.cfg`, lancer
  le jeu, capturer sa fenêtre et son son, les encoder (H.264 matériel, PCM) et les envoyer aux amis ;
  recevoir leurs manettes et simuler l'appui des touches du joueur correspondant.
- **Rejoindre** : saisir le code d'invitation, configurer sa manette ou son clavier une fois à la
  manière d'un émulateur PS1 (« appuie sur CROIX… »), voir et entendre le jeu, jouer.
- **Internet sans serveur** : le port de l'hôte ouvert par UPnP (repli : redirection manuelle
  expliquée), un code d'invitation portant l'adresse, le port et une clé de session qui chiffre le
  trafic.
- **Jusqu'à trois invités**, soit les joueurs 2, 3 et 4 ; l'hôte joue le joueur 1 sur le PC.
- **Cinq lots**, chacun utilisable seul : les manettes (réseau local, image par un partage d'écran
  tiers), l'image, le son, internet, la finition et l'intégration à l'installeur.
- **Le jeu n'est pas modifié.** Si une mesure le rendait nécessaire, une couture LegacyPlus d'une
  ligne (lire les 16 bits d'un joueur depuis l'outil) est prévue en réserve, comme écart déclaré.

## Capabilities

### New Capabilities
- `multiplayer-streaming` : jouer à plusieurs à distance par le streaming du jeu de l'hôte — héberger,
  rejoindre, configurer sa manette, voir, entendre et jouer ; l'invitation et la connexion par
  internet ; les limites (fenêtre au premier plan chez l'hôte, trois invités).

### Modified Capabilities
(aucune : le jeu et l'installeur gardent leurs exigences ; l'installeur dépose un exe de plus, sans
changer ce qu'il promet)

## Impact

- Nouveau projet `OpenForesia.Multiplayer/` (exe `OpenForesiaMultiplayer.exe`), ajouté à
  `OpenForesia.sln`.
- `OpenForesia.Tools/publish-release.ps1` et `OpenForesia.Installer` : publier l'exe élagué avec le
  jeu, un raccourci « OpenForesia Multijoueur » (lot 5).
- `OpenForesia.cfg` de l'hôte : les tables 0 et 1 des joueurs 2 à 4 réécrites par l'outil en mode
  hôte ; celles du joueur 1 jamais touchées.
- Documentation : un `LISEZMOI.md` du projet, `Documentation/Reference/OpenForesia.cfg.md` (les touches
  fantômes), `HISTORY.md`.
- Aucun fichier de `OpenForesia/src/Blaze/` ; aucune donnée du jeu dans le réseau autre que l'image
  et le son de la partie en cours.
