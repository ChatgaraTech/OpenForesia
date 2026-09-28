## 1. Étape 1 — la mesure (design D1)

- [x] 1.1 Relevé de la VRAM occupée une fois le hub chargé (version Debug, groupe de quatre) — bande x 832..1023, y 256..511 libre (textures) ; x 0..255, y 256..271 (palettes) ; un modèle n'emploie que sa texture 1
- [x] 1.2 Taille des paquets de modèle et d'animation des identités 2, 6, 7, 13 (verbe hors ligne `classes`) — ressources dans le répertoire de l'état 1 ; modèle 14-22 Ko, animations 29-52 Ko (8-9 clips), textures 8 bits 128×64 mots + 4 bits 16×64
- [x] 1.3 L'indice de clip de l'animation de repos d'un membre immobile (relevé en jeu) — clip 0
- [x] 1.4 Résultats consignés dans `Documentation/LegacyPlus/legacy.md` (LP-010) ; design mis à jour si une décision change

## 2. Les réglages et le catalogue

- [x] 2.1 `ReglagesBoutique` : lecture d'un fichier donné ; les quatre fichiers de `LegacyPlus\` (noms validés par Vincent)
- [x] 2.2 Filtre de catégorie par la fiche (`+0x13`) à la première ouverture de chaque marchand (D6), écarts journalisés ; filtre de classe (`+0x12`) pour les armes et les armures (demande de Vincent)
- [x] 2.3 `generer-boutique.mjs` : quatre modèles, équipement réparti par catégorie, objets utilisables à un barème par famille (D7) ; relecture des prix par Vincent
- [x] 2.4 Installeur : les quatre modèles déposés dans `LegacyPlus\` sans écraser ceux du joueur ; `Boutique.ini` n'est plus déposé

## 3. Les marchands

- [x] 3.1 Chargement des quatre classes à l'entrée dans le hub, en mémoire et tables propres, textures aux places de 1.1, pagination corrigée (D2) — `MarchandsHub.cs` ; Lutin validé en jeu, les trois autres à voir
- [x] 3.2 Dessin par `DessinSurImpression`, animation de repos avancée par le tweak (D3), profondeur (`+0x1a` = 1) et poussée hors d'un cercle de 48 unités
- [x] 3.3 Ouverture par `PasDuMembre` devant chaque marchand, sur son catalogue (D4)
- [x] 3.4 `Boutique.cs` (l'armoire) retiré ; `Boutique.ini` n'est plus lu (D8)

## 4. Construction et documentation

- [x] 4.1 `dotnet build` sans nouvel avertissement ; les quatre fichiers actifs dans `bin\Debug\net10.0\LegacyPlus\`
- [x] 4.2 `legacy.md` (LP-008 retiré, LP-010), référence des fichiers de boutique (`LegacyPlus.ini.md`), `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md`

## 5. Vérification en jeu (Vincent, depuis Rider)

- [x] 5.1 Les quatre marchands au repos, aux bonnes places et apparences, textures nettes
- [x] 5.2 Les membres du groupe n'ont pas changé d'apparence
- [x] 5.3 Chaque marchand ouvre son catalogue, et seulement sa catégorie ; armes et armures filtrées par classe
- [x] 5.4 Achat : bourse débitée, objet dans l'inventaire avec son icône
- [ ] 5.5 Un fichier à `Actif = non` : ce marchand n'est pas là — non vérifié en jeu à l'archivage
- [ ] 5.6 La carte 82 : l'armoire n'ouvre plus rien — non vérifié en jeu à l'archivage

Validé en jeu par Vincent le 2026-09-28 (« ça marche »), archivé à sa demande avec 5.5 et 5.6 ouverts.
