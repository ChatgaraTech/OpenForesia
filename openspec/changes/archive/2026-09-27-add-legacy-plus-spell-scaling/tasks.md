## 1. Le tweak

- [x] 1.1 `ReglagesLegacyPlus.cs` : clé `[Magie] ScalingMagique`, multiplicateur de 0 à 5 (centièmes), défaut 0 ; `LireMultiplicateur` prend ses bornes en paramètre
- [x] 1.2 `Coutures.cs` : le délégué `MontantDeSort(attaquant, blocAttaquant, montant)`, identité par défaut, rétabli aussi en cas d'échec d'installation
- [x] 1.3 `ScalingMagique.cs` (LP-003) : si le coef ≠ 0, s'empiler sur `MontantDeSort` — lanceur membre du groupe seulement, `montant + montant × coef × MAG / 10000`, borne 30 000 ; installé depuis `Coutures`
- [x] 1.4 `HitResolution.cs` : la ligne du montant dans `ResolveSpellHit_0053A2B0` passe par la couture, marquée `// Couture legacy ++ (LP-003)` ; `git diff` ne montre que cette ligne en plus du lot précédent
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Installeur et documentation

- [x] 2.1 `OpenForesia.Installer/Reglages/LegacyPlus.ini` : section `[Magie]`, `ScalingMagique = 0`, commentaire (effet, valeurs, 1 conseillé) ; copie dans `bin\Debug\net10.0\`
- [x] 2.2 `Documentation/LegacyPlus/legacy.md` : LP-003 ; `Documentation/Reference/LegacyPlus.ini.md` : la clé ; `ARCHITECTURE.md` : la couture dans le tableau ; `src/LegacyPlus/LISEZMOI.md` : le fichier
- [x] 2.3 `HISTORY.md`

## 3. La vérification en jeu (Vincent)

- [x] 3.1 `ScalingMagique = 0` : les dégâts d'une Balle de feu sont ceux d'avant
- [x] 3.2 `ScalingMagique = 1` avec un lanceur de haut niveau : la Balle de feu fait nettement plus ; un monstre lanceur de sorts ne tape pas plus fort
- [x] 3.3 Un sort d'état (Zombie, Pardon) garde son comportement
