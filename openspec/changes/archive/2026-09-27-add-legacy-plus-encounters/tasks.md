## 1. Le tweak

- [x] 1.1 `ReglagesLegacyPlus.cs` : `[Rencontres] Frequence` (0.1 à 10), `MonstresMax` (0 à 12), `Formations` (1 à 4), défauts d'origine
- [x] 1.2 `Coutures.cs` : `Rencontre`, `RencontreEnAttente`, `RencontreBloquee`, `DelaiRencontre`, défauts d'origine, rétablis en cas d'échec
- [x] 1.3 `Rencontres.cs` (LP-007) : fréquence (D2), chevauchement (D3), quantité (D4) ; aucun nombre aléatoire tiré par le tweak lui-même
- [x] 1.4 Les cinq coutures dans le legacy (D1, D2), marquées `// Couture legacy ++ (LP-007)` ; `git diff` ne montre qu'elles
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Installeur et documentation

- [x] 2.1 `LegacyPlus.ini` de l'installeur : section `[Rencontres]` commentée ; copie dans `bin\Debug\net10.0\`
- [x] 2.2 `legacy.md`, `LegacyPlus.ini.md`, `ARCHITECTURE.md`, `src/LegacyPlus/LISEZMOI.md`
- [x] 2.3 `HISTORY.md`

- [x] 2.4 Plafond absolu de 12 entités vivantes pour tout ce que le tweak fait naître (design D6)

## 3. La vérification en jeu (Vincent)

- [x] 3.1 `Frequence = 2` : après une rencontre nettoyée, la suivante vient deux fois plus vite
- [x] 3.2 `MonstresMax = 12` : une rencontre arrive pendant un combat, et plus aucune au-delà de 12 entités
- [x] 3.3 `Formations = 2` : une rencontre fait naître deux groupes, chacun à sa place
- [x] 3.4 Une salle de boss : le boss naît une fois ; noter si une rencontre aléatoire arrive pendant le combat avec `MonstresMax > 0`
- [x] 3.5 Tous les défauts : les rencontres sont celles d'origine
