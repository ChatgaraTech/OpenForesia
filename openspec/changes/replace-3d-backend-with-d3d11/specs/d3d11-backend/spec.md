## Purpose

Exécuter les primitives de dessin émises par le jeu sur une API graphique moderne, en présentant à
l'appelant exactement le contrat binaire qu'il attend du device de `3dhelp.dll`, afin que ni la
reconstruction du jeu ni `winsys3d.dll` n'aient à être modifiés.

## ADDED Requirements

### Requirement: Contrat binaire du device préservé

Le backend SHALL exposer une table de méthodes virtuelles dont la disposition, les conventions
d'appel et les signatures sont identiques à celles du device de `3dhelp.dll`, telles que relevées
dans `Documentation/DLL/3dhelp.md`.

#### Scenario: winsys3d appelle une méthode portée

- **WHEN** `winsys3d.dll` appelle un slot dont le backend fournit l'implémentation
- **THEN** l'appel s'exécute sans adaptation côté appelant
- **AND** la valeur de retour respecte la convention du slot d'origine (`HRESULT` négatif en cas
  d'échec)

#### Scenario: winsys3d appelle un slot non porté

- **WHEN** `winsys3d.dll` appelle un slot dont l'implémentation n'a pas encore été écrite
- **THEN** le programme lève une exception nommant le slot et son décalage dans la vtable
- **AND** l'appel ne retourne pas silencieusement un succès

### Requirement: Sommets déjà transformés

Le backend SHALL traiter les sommets reçus comme étant déjà projetés en coordonnées écran
(`D3DTLVERTEX` : `x`, `y`, `z`, `rhw`, couleur diffuse, couleur spéculaire, coordonnées de texture)
et SHALL NOT leur appliquer de transformation de modèle, de vue ou de projection.

Cette contrainte vient du portage : le jeu est issu de PlayStation, où la projection est faite par
le GTE côté processeur. Elle vaut pour les menus comme pour les scènes 3D.

#### Scenario: Primitive rendue aux coordonnées demandées

- **WHEN** le jeu émet une primitive dont les sommets portent des coordonnées écran données
- **THEN** la primitive apparaît dans la cible de rendu à ces coordonnées, au pixel près

#### Scenario: Composante rhw respectée

- **WHEN** une primitive porte une composante `rhw` différente de 1
- **THEN** l'interpolation des coordonnées de texture en tient compte

### Requirement: Cible de rendu aux dimensions demandées par le jeu

Le backend SHALL rendre dans une cible hors écran dont les dimensions sont celles que le jeu
demande, sans les contraindre à celles de la fenêtre ni à celles de l'écran.

Le jeu choisit ces dimensions par la combinaison des réglages `Resolution` et `Size` ; elles vont
de 320x240 à 1024x768.

#### Scenario: Le jeu change de taille de rendu en cours d'exécution

- **WHEN** le jeu modifie sa taille de rendu (touches F11 / F12)
- **THEN** la cible de rendu suit la nouvelle taille
- **AND** aucun changement de mode d'affichage n'a lieu

### Requirement: Effacement et tampon de profondeur

Le backend SHALL fournir l'effacement séparé de la cible de couleur et du tampon de profondeur, et
SHALL maintenir un tampon de profondeur aux dimensions de la cible de rendu.

#### Scenario: Effacement de la cible de couleur

- **WHEN** le jeu demande l'effacement de la cible de couleur
- **THEN** la cible est remplie avec la couleur demandée
- **AND** le tampon de profondeur est laissé intact

#### Scenario: Effacement du tampon de profondeur

- **WHEN** le jeu demande l'effacement du tampon de profondeur
- **THEN** le tampon de profondeur est réinitialisé
- **AND** la cible de couleur est laissée intacte

### Requirement: Aucun changement de mode d'affichage

Le backend SHALL NOT modifier le mode d'affichage du système, ni prendre l'affichage en mode
coopératif exclusif.

#### Scenario: Résolution du bureau préservée

- **WHEN** le jeu démarre, s'exécute, puis se termine — y compris par une terminaison anormale
- **THEN** la résolution, la profondeur de couleur et la fréquence de rafraîchissement du bureau
  sont inchangées
