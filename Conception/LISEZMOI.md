# Conception — le jeu de 1998, décrit pour en faire un neuf

Ce dossier décrit *Blaze & Blade* (1998) **en termes de jeu** : ses systèmes, ses règles chiffrées,
son contenu, et ce qui y relève de la PlayStation plutôt que du dessein. Il est écrit pour une
équipe qui voudrait faire un **jeu nouveau fortement inspiré** de celui-ci — sous Unreal Engine ou
ailleurs — sans avoir à lire la rétro-ingénierie.

Le reste du dépôt parle en adresses, en décalages et en fonctions Ghidra. Ici, non : chaque document
dit **ce que le jeu fait**, et renvoie à la page de `Documentation/` ou au fichier de code qui le
prouve, pour qui veut vérifier.

## Ce que ces documents sont, et ne sont pas

- **Une référence, pas un cahier des charges.** Le futur jeu sera entièrement neuf : nom, univers,
  histoire, personnages, monstres, objets, modèles, sons. Les noms de l'original apparaissent ici
  uniquement pour retrouver les données dans le dépôt et dans le jeu ; **aucun n'est destiné à être
  repris**. Les règles et les chiffres, eux, servent d'inspiration et de point d'équilibrage.
- **Trois niveaux de certitude, toujours signalés :**
  - **établi** — lu dans le code ou les données, recoupé ;
  - **lecture** — l'interprétation la plus probable, que rien ne contredit mais que rien ne prouve ;
  - **à établir** — le dépôt ne le sait pas encore. On ne comble jamais ce trou par une valeur
    plausible.
- **Les encadrés « Pour un jeu neuf »** sont des **suggestions**, pas des faits : ce qu'il serait
  sage de garder, de corriger ou d'abandonner. Ils sont toujours séparés du reste.

## Les documents

| fichier | contenu |
|---|---|
| [01-Vue-d-ensemble.md](01-Vue-d-ensemble.md) | le jeu en une page : genre, boucle de jeu, structure, ce qui le rend singulier |
| [02-Personnages.md](02-Personnages.md) | les huit classes, les sept caractéristiques, les valeurs dérivées, la création, l'élément, la montée de niveau |
| [03-Combat.md](03-Combat.md) | le coup au corps à corps, le coup magique, les éléments, les altérations d'état, l'expérience, l'intelligence des ennemis |
| [04-Magie.md](04-Magie.md) | les écoles, l'apprentissage, l'incantation par combinaison, la liste des sorts |
| [05-Objets.md](05-Objets.md) | l'équipement, les pouvoirs d'objet, le butin, les coffres, l'inventaire |
| [06-Monde.md](06-Monde.md) | le hub, les zones et leurs sous-zones, le bestiaire par zone, les boss, la progression |
| [07-Heritage-PS1.md](07-Heritage-PS1.md) | ce qui vient de la machine et pas du dessein : à réinterpréter ; les défauts à ne pas reproduire ; les améliorations déjà essayées |
| [08-Exports.md](08-Exports.md) | les exporteurs de cartes, de modèles et de scénario : format de sortie, ce qu'on sait déjà des données, l'ordre de travail |

## Les données brutes

Les chiffres complets — 281 fiches de monstre, 397 objets en français, 123 fiches de sort, 161
coffres, chaque placement de monstre et de décor — ne sont pas recopiés ici : ils sont **générés**
depuis le jeu par `OpenForesia.Tools bestiaire` dans `Documentation/Reference/Bestiaire/*.csv`, et
le compendium des objets (`OpenForesia.Tools/Html/bestiairev2.html`) les rend consultables. Ces CSV
se chargent tels quels dans des *Data Tables* d'Unreal.

Les **exporteurs** de cartes, de modèles puis de scénario, destinés à servir de gabarit au level
design et à la mise en scène, sont décrits dans [08-Exports.md](08-Exports.md).

> **Rappel.** Les données du jeu ne sont jamais dans ce dépôt : il n'en a pas les droits. Les
> exports se font sur la machine de qui possède le jeu, depuis sa propre copie.
