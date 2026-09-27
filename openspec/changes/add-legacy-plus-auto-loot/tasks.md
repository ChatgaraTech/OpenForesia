## 1. Le tweak

- [x] 1.1 `ReglagesLegacyPlus.cs` : `[Butin] RamassageAuto = non | oui`, défaut `non`
- [x] 1.2 `Coutures.cs` : `ButinAtteint(membre, touche, poolButin)`, défaut : `touche`, rétablie en cas d'échec d'installation
- [x] 1.3 `RamassageAuto.cs` (LP-005) : ramassable = `& 0x80088000 == 0x80080000` et posé (D2) ; pièces à tour de rôle entre les membres debout, cristal à tous, filet sous 60 images (D3) ; un butin par membre et par image ; sans nombre aléatoire
- [x] 1.4 La couture dans `PartyMemberStep.cs`, marquée `// Couture legacy ++ (LP-005)` ; `git diff` ne montre qu'elle
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement

## 2. Installeur et documentation

- [x] 2.1 `LegacyPlus.ini` de l'installeur : section `[Butin]` commentée ; copie dans `bin\Debug\net10.0\`
- [x] 2.2 `legacy.md`, `LegacyPlus.ini.md`, `ARCHITECTURE.md`, `src/LegacyPlus/LISEZMOI.md` ; le commentaire trompeur de `DespawnSceneEntity_0051a740` signalé dans le registre
- [x] 2.3 `HISTORY.md`

## 3. La vérification en jeu (Vincent)

- [ ] 3.1 `oui` : pièces et cristal sont ramassés une fois posés, sans marcher dessus ; l'expérience et l'argent montent
- [ ] 3.2 `oui` : un coffre lâché par un monstre reste au sol et s'ouvre à la main
- [ ] 3.3 `non` : ramassage au contact seulement, comme avant
