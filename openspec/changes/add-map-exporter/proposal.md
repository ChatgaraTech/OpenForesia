## Why

Vincent envisage un **jeu neuf fortement inspiré** de *Blaze & Blade*, sous Unreal Engine, avec des
assets entièrement refaits (`Conception/`). Le dépôt sait déjà tout des règles ; il ne sait rien
**montrer** des lieux. Une équipe de remake a besoin d'une **base visuelle** de chaque carte — sa
forme, ses volumes, ses textures, et où vivent monstres, coffres et portes — pour bloquer le level
design à la bonne échelle et garder l'esprit des lieux. Le reconstructeur lit déjà ces cartes pour
les afficher ; il manque seulement de les écrire dans un format qu'Unreal et Blender ouvrent.

## What Changes

- **Un nouveau verbe `exporter-carte` dans `OpenForesia.Tools`**, qui lit une sous-zone (ou toutes)
  depuis les données installées du joueur, hors jeu, en rejouant la chaîne de chargement d'origine,
  et écrit :
  - la **géométrie affichée** de la sous-zone en **glTF 2.0**, avec ses **textures** reconstituées
    depuis les images de la PlayStation (TIM, mémoire vidéo, pages et palettes) et ses couleurs de
    sommet ;
  - la **géométrie de collision** (sols et murs, avec leur genre) dans le même fichier, en calque
    séparé et masquable ;
  - les **placements** en **JSON** : formations de monstres, décor actif, coffres, points d'entrée —
    position, orientation, ce que c'est ;
  - une **vue complète du dessus** en PNG, texturée, et la même annotée des placements ;
  - une **page de visite** (`.html` autonome) par sous-zone, pour regarder la carte texturée dans un
    navigateur sans rien installer.
- **Un dossier par zone**, nommé comme le jeu (`00 - L'auberge`, `01 - La mine abandonnée`…), un
  sous-dossier par sous-zone.
- **Toutes les sorties vont hors du dépôt**, dans un dossier choisi par l'utilisateur : ce sont des
  données du jeu, et le projet n'en a pas les droits.
- **Aucun changement au jeu** : `src/Blaze/` n'est pas modifié ; l'outil réutilise la lecture
  d'archive de `bestiaire` et recopie, sans les appeler, les formules de lecture du jeu.
- `Conception/08-Exports.md` est tenu à jour de ce que l'export établit sur les formats.

## Capabilities

### New Capabilities
- `map-export`: exporter les cartes du jeu (géométrie texturée, collisions, placements) vers des
  formats ouverts, hors jeu, depuis la copie du joueur.

### Modified Capabilities
<!-- aucune -->

## Impact

- `OpenForesia.Tools/` : un verbe, quelques fichiers (lecture des morceaux, mémoire vidéo et TIM,
  écriture glTF/PNG/JSON, page de visite). Aucune dépendance nouvelle : glTF et PNG s'écrivent à la
  main, le JSON par `System.Text.Json`.
- `Conception/08-Exports.md`, `CLAUDE.md` (le verbe), `Documentation/Etat/HISTORY.md`.
- Ni le jeu, ni l'installeur, ni le lanceur ne changent.
