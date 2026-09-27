## 1. Le cadre

- [x] 1.1 Créer `OpenForesia/src/LegacyPlus/` avec un `LISEZMOI.md` court : ce que le répertoire contient, la règle « aucune condition d'option dans `src/Blaze/` », la forme d'une couture
- [x] 1.2 `ReglagesLegacyPlus.cs` : lecture de `LegacyPlus.ini` (UTF-8, sans réflexion), section `[Difficulte]`, clés `PvMonstres` et `DegatsMonstres` en multiplicateur de 0,1 à 10 (point ou virgule, gardé en centièmes), défaut 1, une ligne de journal par valeur refusée ; partager l'analyseur de `DisplaySettings` seulement si c'est possible sans changer son comportement (design D5)
- [x] 1.3 `Coutures.cs` : les délégués `DegatsSubis` et `PvALaNaissance` à l'identité, et le constructeur statique qui lit les réglages puis installe les tweaks, en cas d'échec journalisé et coutures laissées à l'identité (design D1, D2)
- [x] 1.4 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Les coutures dans le legacy

- [x] 2.1 `ApplyDamage_00539A20` : la ligne `damage = Coutures.DegatsSubis(...)` en tête, marquée `// Couture legacy ++ (LP-001)` (design D3)
- [x] 2.2 `ResetEntityStats` : les deux affectations de `+0x48` et `+0x4c` passent par `Coutures.PvALaNaissance`, marquées `// Couture legacy ++ (LP-002)` (design D4)
- [x] 2.3 Vérifier par `git diff` que `src/Blaze/` ne change que de ces trois lignes, et qu'aucune ne nomme un tweak ni une clé

## 3. Le tweak de difficulté

- [x] 3.1 `Difficulte.cs` : si `DegatsMonstres` ≠ 1, s'empiler sur `DegatsSubis` (victime membre du groupe seulement, bornes `[1, 32767]`)
- [x] 3.2 Même fichier : si `PvMonstres` ≠ 1, s'empiler sur `PvALaNaissance` (bornes `[1, 32767]`)
- [x] 3.3 Vérifier à la lecture qu'aucun des deux ne tire de nombre aléatoire

## 4. L'installeur

- [x] 4.1 `OpenForesia.Installer/Reglages/LegacyPlus.ini` : les deux clés à 1, un commentaire français par clé (effet, valeurs admises), l'avertissement que le fichier change l'équilibre du jeu
- [x] 4.2 L'embarquer (`OpenForesia.Installer.csproj`) et le déposer avec `DeposerReglage`, sans écraser celui du joueur
- [x] 4.3 Vérifier que `SourceDuJeu` écarte bien un `LegacyPlus.ini` d'essai présent dans la build (extension `.ini` déjà écartée)

## 5. La documentation

- [x] 5.1 `Documentation/LegacyPlus/legacy.md` : LP-001 et LP-002 passent de « prévu » à « fait », avec leur clé, leur couture et leur fichier
- [x] 5.2 `Documentation/Reference/LegacyPlus.ini.md` : les clés, leurs bornes, ce qu'elles couvrent et ne couvrent pas (dégâts en pourcentage, poison)
- [x] 5.3 `ARCHITECTURE.md` : le répertoire `src/LegacyPlus/` et la règle des coutures
- [x] 5.4 `Strategy.md`, section « Mode legacy ++ » : la famille des tweaks de gameplay, désactivés par défaut, numérotés `LP-NNN`, sans condition d'option dans le code transcrit
- [x] 5.5 `CLAUDE.md` : la ligne de `src/LegacyPlus/` et de `LegacyPlus.ini` dans la table des chemins
- [x] 5.6 `HISTORY.md`

## 6. La vérification en jeu (Vincent)

- [ ] 6.1 Sans `LegacyPlus.ini`, puis avec toutes les clés à 1 : un même combat donne les mêmes dégâts et les mêmes PV qu'avant le change
- [ ] 6.2 `PvMonstres = 2` : un monstre met deux fois plus de coups à tomber ; un décor cassable et un boss se comportent normalement
- [ ] 6.3 `DegatsMonstres = 2` : les chiffres de dégâts reçus par le groupe doublent ; ceux infligés aux monstres ne changent pas
- [ ] 6.4 Une valeur invalide (`PvMonstres = beaucoup`) : le jeu démarre, la ligne de journal apparaît, l'autre clé s'applique
- [ ] 6.5 Refaire le Setup (`publish-release.ps1`) et vérifier qu'une installation neuve dépose `LegacyPlus.ini`, et qu'une réinstallation garde celui du joueur
