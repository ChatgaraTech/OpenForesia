## 1. Établir les formats

- [x] 1.1 Trancher la lecture des faces des quatre listes `+0x14..+0x20` : 0x20 octets (`DrawChunk`) contre 16 octets (`terrain`, `TryPlaceEntity`) — lire `TryPlaceEntity` et `RelocateModelEntry`, vérifier sur forêt 0 qu'aucun indice ne sort des sommets ; corriger `terrain` si c'est lui qui se trompe
- [x] 1.2 Relever ce que chaque liste est (terrain balayé, deuxième liste, troisième sans grille, quatrième) et qui dessine les paquets `+0x24`, avec quels drapeaux
- [x] 1.3 Trouver le bloc de placement passé à `DrawChunk` pour un morceau de carte (position, angles, échelle) chez son appelant
- [x] 1.4 Relever le chemin d'une TIM vers la mémoire vidéo (`DecodeResourceChunk`) : rectangle image, rectangle palette, mot rangé dans la table `0x005e8f00`
- [x] 1.5 Écrire ce qui est établi dans `Conception/08-Exports.md`

## 2. Lecture hors jeu

- [x] 2.1 Réutiliser la lecture d'archive de `Bestiaire.cs` (`Blob`, `LoadEntry`, `EntryList`) depuis des fichiers `partial` du même type, sans toucher au code de `bestiaire`
- [x] 2.2 Lire une sous-zone : textures, quatre listes, paquets `+0x24`, avec un commentaire `Miroir de :` par lecteur
- [x] 2.3 Reconstituer la mémoire vidéo 1024 × 512 depuis les TIM, et résoudre page et palette de chaque face

## 3. Écriture

- [x] 3.1 Écrire le PNG (zlib de .NET) : une texture 256 × 256 par couple page + palette, `0x0000` transparent
- [x] 3.2 Écrire le glTF 2.0 : un nœud « affichage » (un primitif par matériau, UV, couleurs de sommet, axe Y retourné), un nœud « collision » (sol / genre en `extras`)
- [x] 3.3 Écrire les placements en JSON : formations, décor, coffres, entrées, dans les coordonnées du modèle
- [x] 3.4 Rendre la vue du dessus : rastériseur logiciel orthographique texturé (test alpha, tampon de profondeur), cadré sur l'emprise, PNG ; et une seconde image annotée des placements
- [x] 3.5 Écrire la page de visite autonome : visionneuse WebGL faite main, orbite, calque collision, marqueurs nommés au survol, données et PNG embarqués

## 4. Le verbe

- [x] 4.1 Ajouter `exporter-carte <dossier-jeu> [zone] [sous-zone] --sortie <dossier>` à `Program.cs` ; refuser un dossier de sortie dans le dépôt ; messages d'erreur pour données absentes, zone ou sous-zone inconnue
- [x] 4.2 Un dossier par zone nommé comme le jeu (`03 - La forêt`), un sous-dossier par sous-zone (numéro et nom de lieu)
- [x] 4.3 Mode « toute la zone » et « tout le jeu », avec un résumé par sous-zone (faces, textures, placements, échecs et leur raison)

## 5. Vérification

- [x] 5.1 Forêt 0 : aucun indice hors sommets, aucune face hors page, les coffres égaux à `coffres.csv`
- [x] 5.2 Ouvrir le glTF dans un validateur glTF (ou Blender si présent) : zéro erreur — fait par un contrôle de structure (tampons, accesseurs, min/max, images) sur les 160 fichiers : zéro erreur ; Blender et Unreal non essayés sur cette machine
- [ ] 5.3 Rendre la page de visite en Chrome sans tête et la comparer à une capture du jeu au même endroit ; noter les écarts — rendu en Chrome sans tête fait (forêt 0 : terrain, arbres debout, pont, falaises) ; reste la capture du jeu à demander à Vincent
- [x] 5.4 Exporter tout le jeu ; lire le résumé ; regarder une sous-zone par zone

## 6. Documentation

- [x] 6.1 `Conception/08-Exports.md` : l'usage du verbe, ce que contient chaque fichier, ce qui n'est pas rendu (éclairage, brouillard, palettes animées)
- [x] 6.2 `CLAUDE.md` : le verbe `exporter-carte` à côté de `bestiaire`
- [x] 6.3 `Documentation/Etat/HISTORY.md`
