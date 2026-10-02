## Why

Le joueur 1 ne peut pas jouer à la manette. La page « Configurer » du menu Options ne capture rien :
dans la reconstruction, aucune manette physique n'arrive jusqu'au jeu (`FUN_00523230`, la création
des joysticks DirectInput, et `FUN_00523340`/`FUN_00522fe0`, leur lecture, ne sont pas reconstruites,
`Input.cs:330`). Même reconstruit fidèlement, ce chemin de 1998 ignorerait la croix directionnelle
(chapeau POV jamais lu) et les gâchettes (axe Z sauté par la page de capture).

L'invité du multijoueur, lui, joue déjà avec n'importe quelle manette : `OpenForesiaMultiplayer.exe`
lit XInput et WinMM, la configure sur un écran visuel (`EcranManette`) et la rejoue chez l'hôte en
touches fantômes. Ce chemin se résume à « une manette, un mot de 16 bits, des touches fantômes » ;
seul le réseau est propre à l'invité. Il suffit de le faire servir aussi au joueur 1, en local, sans
toucher au jeu (exploration du 2026-10-02).

## What Changes

- **BREAKING : `OpenForesiaMultiplayer.exe` devient `OpenForesiaLanceur.exe`** : Jouer seul,
  Héberger, Rejoindre, Configurer ma manette. `OpenForesia.exe`, le jeu, reste à côté, avec son
  propre raccourci.
- **Le lanceur se suffit pour rejoindre.** Sans `OpenForesia.exe` dans son dossier, il n'offre que
  Rejoindre et Configurer ma manette : un ami qui veut seulement rejoindre reçoit ce seul exe
  (environ 10 Mo), sans le jeu, sans l'ISO, sans installation. La publication le sort aussi à part,
  à côté du Setup.
- **Nouveau mode « Jouer seul »** : le lanceur démarre le jeu et relaie la manette du joueur 1 tant
  que la partie dure.
- **Héberger** relaie aussi la manette du joueur 1, en plus des invités.
- **Le joueur 1 a ses propres touches fantômes** (quatorze codes de plus, pris parmi les 45 que la
  mesure du 2026-09-30 a validés sans les retenir), écrites dans **sa table 1** de `[Controls]`. Sa
  table 0, le clavier principal, n'est pas touchée : clavier et manette marchent ensemble.
- **Un bouton « Configurer ma manette »** dans l'accueil, dans la fenêtre de la partie solo et dans
  celle de l'hôte : l'écran de l'invité, inchangé, sur la même configuration (`manette.json`).
- **L'installeur** : deux raccourcis, « OpenForesia » (le jeu, au clavier) et « OpenForesia
  Lanceur » ; l'ancien exe et le raccourci « OpenForesia Multijoueur » sont retirés à la mise à jour.
- **Aucune modification du jeu.** `OpenForesia.exe` lancé directement reste au clavier seul, et la
  page manette du menu Options reste inopérante.

## Capabilities

### New Capabilities

- `game-launcher` : le programme qui lance le jeu (seul, en hébergeant, en rejoignant) et donne au
  joueur 1 une manette configurable, sans modifier le jeu.

### Modified Capabilities

Aucune dans `openspec/specs/`. La capacité `multiplayer-streaming` n'existe encore que dans le
change `add-multiplayer-streaming`, non archivé, et son exigence « One program hosts and joins »
nomme `OpenForesiaMultiplayer.exe`. Elle sera mise à jour (nom du programme) quand ce change sera
archivé : voir tasks.md.

## Impact

- `OpenForesia.Multiplayer/` → `OpenForesia.Lanceur/` : projet, assembly, espace de noms, entrée
  de `OpenForesia.sln`.
- `Hote/ConfigTouches.cs` (table 1 du joueur 1, sauvegarde et remise), `Hote/Injection.cs` (joueur 1),
  `Hote/Session.cs` (relais du joueur 1), `Interface/FenetreAccueil.cs`, `Interface/FenetreHote.cs`,
  une fenêtre de partie solo, `Program.cs`.
- `OpenForesia.Installer` : `Installation.cs`, `Desinstallation.cs` (nom de l'exe, raccourcis,
  retrait de l'ancien) ; `OpenForesia.Tools/publish-release.ps1` (exe attendus).
- Documentation : `OpenForesia.cfg.md` (codes du joueur 1), le LISEZMOI du lanceur, `CLAUDE.md`,
  `HISTORY.md`.
- Aucune modification de `OpenForesia/src/`.
