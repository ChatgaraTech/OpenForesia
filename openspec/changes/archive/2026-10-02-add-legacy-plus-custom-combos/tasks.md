## 1. L'interrupteur (D5)

- [x] 1.1 `ReglagesLegacyPlus` : `[Magie] CombinaisonsLibres`, `oui` / `non` sans casse, `non` par défaut, refus journalisé, valeur reprise dans la ligne de résumé
- [x] 1.2 `OpenForesia.Installer/Reglages/LegacyPlus.ini` : la clé commentée, à `non`

## 2. Le rangement (D1)

- [x] 2.1 `CombinaisonsLibres` : lire et écrire l'entrée d'un sort dans `+0x1c00 + indice × 5` (longueur, huit quartets), entrée invalide lue vide
- [x] 2.2 `FormatSauvegarde.md` : `+0x1c00..+0x1cff`, le format, et qu'il est ignoré par le jeu de 1998

## 3. Les coutures (D2)

- [x] 3.1 `Coutures` : `CombinaisonDuSort`, `CombinaisonAffichee`, `OngletSorts`, défauts identité, remise à zéro avec les autres
- [x] 3.2 `PickCastTarget_0051A5D0` : le pointeur de combinaison de chaque fiche passe par `CombinaisonDuSort`, `// Couture legacy ++ (LP-016)`
- [x] 3.3 `DrawSpellTab` : le dessin de la combinaison passe par `CombinaisonAffichee` ; `OngletSorts` appelée avant le déplacement du curseur, avec le sort visé et s'il est appris
- [x] 3.4 Établir quel mot de `pad` porte les appuis de l'image dans `DrawSpellTab` et le bit de SELECT ; le noter dans `design.md` (D3)

## 4. Le tweak (D2, D3)

- [x] 4.1 `src/LegacyPlus/CombinaisonsLibres.cs` : tampons natifs au format de la fiche, remplis depuis la fiche du personnage ; raccourci vide = longueur nulle
- [x] 4.2 La saisie : ouverture par SELECT sur un sort appris, appuis de face par `Input.ComboButtons` (huit au plus), clôture par SELECT, appuis effacés pendant la saisie, affichage en direct
- [x] 4.3 La règle de conflit : à la clôture, tout autre sort du même personnage ayant la même suite est vidé
- [x] 4.4 L'abandon : saisie oubliée si l'onglet n'est pas dessiné à l'image suivante (compteur avancé par `DessinSurImpression` : `ImageSuivante` ne tourne pas pendant le menu)
- [x] 4.5 `Installer()` depuis le constructeur statique de `Coutures`, actif seulement si `CombinaisonsLibres`

## 5. Le bestiaire (D4)

- [x] 5.1 `Bestiaire.Buttons` : `0x20` → `X`, `0x40` → `O` ; régénérer `Bestiaire/sorts.csv` et `SortsAtteignables.md`

## 6. Vérification et documentation

- [x] 6.1 `dotnet build OpenForesia.sln` sans erreur
- [x] 6.2 `LegacyPlus.ini.md`, `legacy.md` (LP-016, ses coutures), `src/LegacyPlus/LISEZMOI.md`, `Sorts.md` (le mode combinaison et les raccourcis), `HISTORY.md`
- [x] 6.3 Vérifier que l'éditeur de sauvegarde préserve `+0x1c00..` (`node check-editeur-sauvegarde.mjs`)
- [x] 6.4 En jeu (Vincent) : clé à `non` → combinaisons d'origine ; clé à `oui` → tout vide au départ ; saisie ✕ sur Boule de feu, lancer par SELECT + ✕ ; ✕ ✕ ✕ sur un autre sort ; conflit (l'ancien vidé) ; vider un raccourci ; les boutons pendant la saisie ne valident rien ; sauvegarder, recharger, raccourcis présents ; deux mages, raccourcis distincts
