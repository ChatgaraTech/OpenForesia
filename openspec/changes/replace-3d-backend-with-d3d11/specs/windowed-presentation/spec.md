## Purpose

Afficher la cible de rendu du jeu dans une fenêtre du bureau plutôt que par un affichage exclusif
avec changement de mode, afin de supprimer la source de non-déterminisme responsable du cadrage
erroné et, vraisemblablement, des écrans noirs intermittents.

## ADDED Requirements

### Requirement: Présentation en fenêtre par défaut

Le système SHALL présenter la cible de rendu dans une fenêtre du bureau au démarrage, sans prendre
l'affichage en exclusif.

#### Scenario: Démarrage

- **WHEN** le jeu démarre
- **THEN** une fenêtre apparaît sur le bureau
- **AND** la résolution du bureau n'est pas modifiée
- **AND** les autres fenêtres restent accessibles

#### Scenario: Le bureau reste utilisable

- **WHEN** l'utilisateur bascule vers une autre application pendant que le jeu tourne
- **THEN** la bascule s'effectue sans changement de mode d'affichage
- **AND** le jeu continue de fonctionner

### Requirement: Bascule fenêtre / plein écran

Le système SHALL permettre de basculer entre l'affichage en fenêtre et un plein écran sans bordure,
dans les deux sens, pendant l'exécution.

Le plein écran SHALL être obtenu en dimensionnant la fenêtre à l'écran, et non par un changement de
mode d'affichage.

#### Scenario: Passage en plein écran

- **WHEN** l'utilisateur demande le plein écran
- **THEN** l'affichage occupe tout l'écran
- **AND** la résolution du bureau n'est pas modifiée

#### Scenario: Retour en fenêtre

- **WHEN** l'utilisateur quitte le plein écran
- **THEN** la fenêtre retrouve sa position et ses dimensions précédentes

### Requirement: Mise à l'échelle de la cible de rendu

Le système SHALL mettre la cible de rendu à l'échelle de la zone cliente lors de la présentation,
et SHALL préserver le rapport d'aspect de la cible de rendu.

Le jeu ne SHALL PAS observer les dimensions de la fenêtre : il continue de rendre aux dimensions
qu'il a lui-même choisies.

#### Scenario: Redimensionnement de la fenêtre

- **WHEN** l'utilisateur redimensionne la fenêtre
- **THEN** l'image est mise à l'échelle pour occuper la zone cliente
- **AND** son rapport d'aspect est préservé
- **AND** la taille de rendu demandée par le jeu est inchangée

#### Scenario: Zone cliente d'un rapport d'aspect différent

- **WHEN** la zone cliente n'a pas le rapport d'aspect de la cible de rendu
- **THEN** l'image est centrée
- **AND** les bandes restantes sont remplies d'une couleur unie

### Requirement: Cadrage sans remplissage parasite

L'image rendue SHALL occuper la totalité de la zone qui lui est allouée à la présentation, sans
remplissage autre que celui du rapport d'aspect décrit ci-dessus.

Cette exigence existe parce que le chemin d'affichage remplacé produisait une image cantonnée au
coin haut gauche : le jeu rendait au plus 1024x768 dans un mode 1440x900 négocié à son insu.

#### Scenario: Image non cantonnée à un coin

- **WHEN** le jeu affiche une image quelle que soit sa taille de rendu
- **THEN** l'image est centrée dans la zone de présentation
- **AND** elle n'est pas reléguée dans un coin de l'écran
