## 1. Le tweak

- [x] 1.1 `ReglagesLegacyPlus.cs` : `[Combat] InvulnerabiliteEnnemis = oui | non`, défaut `oui`
- [x] 1.2 `Coutures.cs` : `VictimeProtegee` (défaut : la condition d'origine), `CoupPorte` et `ImageSuivante` (défaut : rien), rétablies en cas d'échec d'installation
- [x] 1.3 `VerrouParAttaquant.cs` (LP-004) : couples (source, victime), durée lue à l'image suivante (design D3), horloge et purge (D4), sans nombre aléatoire
- [x] 1.4 Les sept coutures dans le legacy (dont le filtre d'état, ajouté après le premier essai en jeu) (design D2), chacune marquée `// Couture legacy ++ (LP-004)` ; `git diff` ne montre qu'elles
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Installeur et documentation

- [x] 2.1 `LegacyPlus.ini` de l'installeur : section `[Combat]`, commentée ; copie dans `bin\Debug\net10.0\`
- [x] 2.2 `legacy.md`, `LegacyPlus.ini.md`, `ARCHITECTURE.md` (tableau des coutures), `src/LegacyPlus/LISEZMOI.md`
- [x] 2.3 `HISTORY.md`

## 3. La vérification en jeu (Vincent)

- [x] 3.1 `oui` : un ennemi qui clignote refuse les coups, comme avant
- [x] 3.2 `non`, à plusieurs joueurs : deux coups rapprochés de deux joueurs sur le même ennemi passent tous les deux
- [x] 3.3 `non` : un sort durable (Mur de flammes) ou une flèche ne frappe pas un même ennemi à chaque image
- [x] 3.4 `non` : un ennemi tué se comporte normalement (fin, butin) ; un monstre qui frappe un joueur le laisse clignoter et intouchable comme avant
