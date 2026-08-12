## Purpose

Garantit que tout ce que le jeu affiche, depuis le lancement du binaire jusqu'au menu principal,
apparaît dans une seule et même fenêtre du bureau : écran-titre, séquence d'attract et menu, sans
changement de mode d'affichage ni bascule visible entre deux surfaces d'affichage distinctes.

## ADDED Requirements

### Requirement: Une seule fenêtre du lancement au menu

Le système SHALL afficher l'écran-titre, la séquence d'attract et le menu principal dans la même
fenêtre du bureau.

Le système NE SHALL PAS créer de seconde fenêtre visible, ni faire apparaître successivement deux
surfaces d'affichage différentes, pendant la séquence de démarrage.

#### Scenario: Démarrage jusqu'au menu

- **WHEN** le jeu est lancé et laissé jusqu'à l'affichage du menu principal
- **THEN** une seule fenêtre apparaît sur le bureau
- **AND** l'écran-titre s'affiche dans cette fenêtre
- **AND** l'attract s'affiche dans cette même fenêtre
- **AND** le menu s'affiche dans cette même fenêtre

#### Scenario: Aucune bascule visible

- **WHEN** l'affichage passe de l'écran-titre à l'attract, puis de l'attract au menu
- **THEN** aucune fenêtre n'apparaît ni ne disparaît à ces transitions
- **AND** la fenêtre conserve sa position et ses dimensions

### Requirement: Aucun changement de mode d'affichage au démarrage

Le système NE SHALL PAS modifier la résolution ni le mode d'affichage du bureau à aucun moment de
la séquence de démarrage, y compris pendant l'écran-titre et l'attract.

Cette exigence étend à la phase de démarrage la garantie que la présentation en fenêtre apporte
déjà au reste de l'exécution. Elle existe parce que l'écran-titre empruntait un chemin d'affichage
distinct de celui du menu, resté en mode exclusif après le passage au backend Direct3D 11.

#### Scenario: Résolution du bureau pendant l'écran-titre

- **WHEN** l'écran-titre est affiché
- **THEN** la résolution du bureau est celle qui précédait le lancement
- **AND** les autres fenêtres du bureau restent visibles et accessibles

#### Scenario: Terminaison anormale pendant le démarrage

- **WHEN** le processus se termine anormalement pendant l'écran-titre ou l'attract
- **THEN** la résolution du bureau reste inchangée

### Requirement: Fidélité de l'image de démarrage

Le système SHALL afficher l'écran-titre et l'attract avec le contenu que le jeu d'origine y dessine,
sans substitution ni omission.

L'image SHALL occuper la fenêtre selon les mêmes règles de cadrage que le reste de l'affichage :
mise à l'échelle, rapport d'aspect préservé, image centrée.

#### Scenario: Bitmap de titre disponible

- **WHEN** le bitmap d'écran-titre du jeu est accessible
- **THEN** il s'affiche dans la fenêtre
- **AND** son rapport d'aspect est préservé
- **AND** il est centré dans la zone de présentation

#### Scenario: Dessins successifs sur la même image

- **WHEN** le jeu effectue plusieurs opérations de dessin avant de rendre l'image complète
- **THEN** l'image présentée reflète l'ensemble de ces opérations
- **AND** aucun état intermédiaire partiel n'est présenté

### Requirement: La séquence de démarrage aboutit toujours

Le système SHALL atteindre le menu principal quelles que soient la présence et l'accessibilité des
médias de démarrage.

Une opération de dessin de l'écran-titre ou de l'attract qui échoue SHALL dégrader uniquement son
propre rendu, et NE SHALL PAS empêcher la séquence de se poursuivre.

Cette exigence reprend, pour le chemin d'affichage repris par ce changement, la garantie que la
capacité `blaze-intro-attract` établit pour la séquence de démarrage.

#### Scenario: Médias de démarrage absents

- **WHEN** le jeu est lancé sans CD monté et sans bitmap d'écran-titre accessible
- **THEN** la fenêtre apparaît malgré tout
- **AND** la séquence de démarrage se poursuit jusqu'au menu principal

#### Scenario: Échec d'une opération de dessin

- **WHEN** une opération de dessin de l'écran-titre échoue
- **THEN** l'échec est signalé de façon visible dans les journaux
- **AND** la séquence de démarrage se poursuit

### Requirement: Le chemin d'affichage du menu est inchangé

Le système SHALL laisser inchangés le rendu Direct3D du menu et la liaison de texture.

Aucune exigence de ce changement NE SHALL être satisfaite en modifiant le comportement observable
du chemin de rendu tridimensionnel.

#### Scenario: Rendu du menu après démarrage

- **WHEN** le menu s'affiche à l'issue de la séquence de démarrage
- **THEN** sa géométrie et ses couleurs sont celles obtenues avant ce changement
