## Why

Changer de sort passe aujourd'hui par le menu △ (l'anneau) ou par le menu START : le sort mémorisé
est le seul que le bouton de classe lance. Le jeu de 1998 a bien un second chemin — tenir SELECT,
taper une combinaison de △ ○ ✕ □, relâcher —, qui lance n'importe quel sort appris sans toucher au
sort mémorisé ; il marche depuis le 2026-10-02 (complément de LB-058). Mais ses combinaisons sont
imposées, longues (jusqu'à treize coups) et impossibles à retenir. Vincent veut que chaque joueur
**choisisse lui-même**, par personnage, le raccourci de chaque sort : « ✕ » pour la Boule de feu,
« ✕ ✕ ✕ » pour une autre. C'est un écart de confort, donc un tweak legacy ++ (LP-016), inactif par
défaut.

## What Changes

- **Un interrupteur** : `LegacyPlus.ini`, `[Magie] CombinaisonsLibres`, `oui` / `non`, `non` par
  défaut — le jeu de 1998.
- **Des raccourcis par personnage**, rangés dans sa fiche, donc dans sa sauvegarde : un par sort de
  son école, de **un à huit coups**, **vides au départ**. Quand le tweak est actif, le mode
  combinaison ne reconnaît **que** les raccourcis du personnage qui incante ; les combinaisons
  d'origine ne servent plus.
- **La saisie dans le menu START**, onglet magie : sur un sort appris, SELECT ouvre la saisie ; △ ○ ✕
  □ s'ajoutent et s'affichent à mesure ; SELECT valide. Valider sans aucun coup vide le raccourci.
  Si un autre sort du même personnage avait déjà la même suite, son raccourci devient vide.
- **L'affichage** : l'onglet magie dessine le raccourci du personnage à la place de la combinaison
  d'origine (rien quand il est vide).
- **Le bestiaire** : les lettres des combinaisons de `Bestiaire/sorts.csv` suivent désormais les
  pictogrammes du jeu (`0x20` = croix, `0x40` = rond, établi en jeu le 2026-10-02).

## Capabilities

### New Capabilities
- `gameplay-custom-combos` : les raccourcis de sort par personnage — l'interrupteur, leur saisie
  dans le menu, leur reconnaissance au lancer, leur affichage, leur rangement dans la sauvegarde et
  la règle de conflit.

### Modified Capabilities
(aucune)

## Impact

- `OpenForesia/src/LegacyPlus/CombinaisonsLibres.cs` (nouveau, LP-016), `Coutures.cs`,
  `ReglagesLegacyPlus.cs`.
- `OpenForesia/src/Blaze/Screens/State03/Entities/Targeting.cs` (`PickCastTarget_0051A5D0`) et
  `WorldMapMenu.cs` (`DrawSpellTab`, le dessin de la combinaison) : des lignes de couture.
- La fiche de personnage : `+0x1c00..+0x1cff`, jamais lus ni écrits par le jeu de 1998 ;
  `Documentation/Reference/FormatSauvegarde.md`.
- `OpenForesia.Tools/Bestiaire.cs` (les lettres), `Bestiaire/sorts.csv` et `SortsAtteignables.md`
  régénérés.
- `OpenForesia.Installer/Reglages/LegacyPlus.ini`, `LegacyPlus.ini.md`, `legacy.md` (LP-016),
  `HISTORY.md`.
