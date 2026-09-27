## 1. Le tweak

- [x] 1.1 `ReglagesLegacyPlus.cs` : `[Magie] DeplacementEnIncantation`, 0 à 1 (centièmes), défaut 0
- [x] 1.2 `Coutures.cs` : `VitesseEnIncantation(membre, vitesse)`, défaut : 0, rétablie en cas d'échec
- [x] 1.3 `DeplacementEnIncantation.cs` (LP-006) : `vitesse × coef / 100`, sans nombre aléatoire
- [x] 1.4 La couture dans `ApplyGroundMotion_0051A020`, marquée `// Couture legacy ++ (LP-006)` ; `git diff` ne montre qu'elle
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Installeur et documentation

- [x] 2.1 `LegacyPlus.ini` de l'installeur : la clé dans `[Magie]`, commentée ; copie dans `bin\Debug\net10.0\`
- [x] 2.2 `legacy.md`, `LegacyPlus.ini.md`, `ARCHITECTURE.md`, `src/LegacyPlus/LISEZMOI.md`
- [x] 2.3 `HISTORY.md`

## 3. La vérification en jeu (Vincent)

- [x] 3.1 `0.5` : un lanceur avance à mi-vitesse en tapant sa combinaison, et le sort part normalement
- [x] 3.2 `0.5` : sans direction tenue, il reste sur place
- [x] 3.3 `0` : il est cloué, comme avant
