## 1. L'interrupteur (D7)

- [x] 1.1 `ReglagesLegacyPlus` : `[Magie] SoutienFee`, `oui`/`true` et `non`/`false` sans casse, `non` par défaut, refus journalisé `[LEGACY++]`, valeur reprise dans la ligne de résumé
- [x] 1.2 `OpenForesia.Installer/Reglages/LegacyPlus.ini` : la clé commentée, à `non`

## 2. Les coutures (D2, D3, D5)

- [x] 2.1 `Coutures` : `EffetDeFee(lanceur, victime, effet)` (défaut : un appel, résultat rendu), `MalusForce()` (défaut `false`), `DureeDEtat(duree)` (défaut identité), `ProjectileDeFee(graine, engendrer)` (défaut un engendrement) ; leur remise à zéro avec les autres
- [x] 2.2 `School2Elementalist.cs` : les onze poses d'effet du tableau de `design.md` passent par `EffetDeFee`, une ligne chacune, `// Couture legacy ++ (LP-015)`, l'ancre `// Ghidra:` gardée
- [x] 2.3 Effets 8, 10, 11 (`CombatPairing.cs`, `StatusEffects.cs`) et 20 : `MalusForce()` en court-circuit devant le masque d'immunité et devant le jet de sauvegarde — sans tirage quand il est vrai
- [x] 2.4 Effets 19 et 20 : `DureeDEtat` juste après le calcul de la durée, avant la comparaison

## 3. Les quatre projectiles (D6)

- [x] 3.1 Vérifier dans `SpawnStatusEffectEntity_0047AC40` et la bulle de Dodo que l'enfant prend son cap `+0x32` dans la recopie de la graine, et que le verrou `0x80` posé sur le lanceur se lève à la mort de la bulle ; noter le résultat dans `design.md`
- [x] 3.2 `TickSleep_00476C70`, `TickSirenSong_00477A20`, `TickCrush_00477D20` : l'engendrement passe par `ProjectileDeFee`

## 4. Le tweak (D1, D2, D4)

- [x] 4.1 `src/LegacyPlus/SoutienFee.cs` : la reconnaissance de la Fée (membre du groupe, classe 7, famille 2, indice dans la liste)
- [x] 4.2 Le contexte : « malus forcé » (indices 0, 1, 7, 8, 12), « durée ÷ 2 » (indices 1, 2), posé autour de la pose et de la diffusion, toujours effacé
- [x] 4.3 La diffusion des bonus (indices 2 à 6, 10, 14 ; pas 13) : les quatre emplacements du groupe, filtre de Coup de pouce, victime d'origine sautée
- [x] 4.4 La diffusion des malus (indices 0, 1, 7, 8, 12) : les `0x20` entités de scène, filtre de Zombie sans espèce ni `+0x74`, distance au lanceur `< 0x5dc` en trois dimensions, victime d'origine sautée
- [x] 4.5 Les quatre projectiles : cap tourné de `0x400` trois fois, cap d'origine rendu
- [x] 4.6 `Installer()` appelé depuis le constructeur statique de `Coutures`, actif seulement si `SoutienFee`

## 5. Vérification et documentation

- [x] 5.1 `dotnet build OpenForesia.sln` sans erreur
- [x] 5.2 `LegacyPlus.ini.md` (la clé), `legacy.md` (LP-015, ses coutures), `src/LegacyPlus/LISEZMOI.md` (le fichier), `HISTORY.md`
- [x] 5.3 En jeu (Vincent) : interrupteur à `non` → jeu d'origine ; Vitesse et Superman sur quatre membres, Vitesse plus courte que Turbo ; Dodo — quatre bulles, un groupe endormi, réveil au coup, un boss endormi ; Chant sirènes et Écrase sur un groupe ; Gros sabots plus court que Ralenti ; le Prêtre inchangé ; aucun marchand du hub ni PNJ endormi

## 6. Après le premier essai (D8)

- [x] 6.1 `Coutures.CibleDeFee` et ses huit sites (Vitesse, Matéria ×4, Coussin d'air, Superman, Gros sabots) ; `AllocEntitySlot` rendue `internal`
- [x] 6.2 `SoutienFee` : copie du sort par membre (bonus) et par ennemi à portée (Gros sabots), cible réservée et vérifiée ; diffusion retirée pour les bonus, Gros sabots et Dodo
- [x] 6.3 Spec et design accordés ; `dotnet build` sans erreur
- [x] 6.4 En jeu (Vincent) : anneaux de Vitesse et de Superman sur chaque membre, flammes des Matéria, souffle de Coussin d'air ; un sabot sur chaque monstre ; une bulle de Dodo n'endort qu'un monstre
- [x] 6.5 Chant sirènes et Écrase comme Dodo : chaque bulle n'agit que sur ce qu'elle touche (diffusion réservée à Ralenti) ; spec et design accordés
