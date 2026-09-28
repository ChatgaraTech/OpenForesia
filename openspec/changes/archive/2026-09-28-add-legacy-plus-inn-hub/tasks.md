## 1. Étape 1 — la mesure (design D1)

- [x] 1.1 Longueur de la table des sous-zones de la zone 0 (`state + 0x48`) : numéros libres ou non ; choix du numéro du hub, consigné — 105 entrées, **86** retenu (emplacement vide)
- [x] 1.2 Recensement des lecteurs de la sous-zone (`s_Unknown_005de52c`, `& 7`, comparaisons à 82) dans les gestionnaires, scripts et verbes que la 82 exerce (D2) — LP-008, vidange de manette, sauvegarde, repères
- [x] 1.3 Les meubles de la 82 : seulement des entités, ou aussi dans les faces de sol / collisions de la sous-zone ? — **dans le terrain** (faces de sol surélevées, `terrain 0 82`) ; Vincent : les enlever → aplanies au chargement du hub
- [x] 1.4 Les entrées de décor de la 82 à garder (porte 0-1, coffre 15-16) revérifiées contre les données du palier retenu ; dépendance de la 82 au palier (0x02 … 0x52) notée — identique de 10 à 82
- [x] 1.5 La sauvegarde : ce qu'elle retient de zone, sous-zone, point d'entrée et position (`FormatSauvegarde.md`, chemin d'écriture) ; site de la couture D6 — deux écrivains (menu, coffre) + étiquette des repères
- [x] 1.6 La sortie de la 82 : volume et script (opcode 17) ou face marchée ; point d'entrée qu'elle vise ; site de la couture D5 — opcode 17 seul ; le point d'entrée d'arrivée en 82 se relève par un journal (tâche 3.2)
- [x] 1.7 Le modèle de porte (sous-type 2) peut-il naître et s'afficher en section 0 de la réception ? — oui en principe : modèles et maillages par sous-zone ; à voir en jeu (5.2)
- [x] 1.8 Relevé du mur (Vincent, `[Diagnostic] TracePosition`) dans la réception du palier 80 — (−389, 0, 583), cap 3072, face au mur ouest ; contrôle dans une réception antérieure : tâche 5.6
- [x] 1.9 Tout ce qui précède consigné dans `Documentation/LegacyPlus/legacy.md` (LP-009) ; design mis à jour si une décision change

## 2. Le hub — la pièce

- [x] 2.1 `ReglagesLegacyPlus.cs` : clé `[Auberge] Hub` (oui/non, défaut non)
- [x] 2.2 `src/LegacyPlus/HubAuberge.cs` (nom à confirmer par Vincent), installé depuis `Coutures`
- [x] 2.3 Couture `EntreeSousZone` dans `LoadWorldMapSubArea` : le numéro du hub charge le répertoire de la 82 (D2)
- [x] 2.4 Couture de placement des décors : dans le hub, seules la porte et le coffre restent — `DecorNe` rend les autres au néant après leur naissance, sans décaler les rangs (D3)
- [x] 2.5 Chaque couture marquée `// Couture legacy ++ (LP-009)`, identité par défaut ; hors coutures, `src/Blaze/` ne change que la visibilité de quatre globales (`private` → `internal`)

## 3. Le passage — aller et retour

- [x] 3.1 La porte d'entrée : la porte de la 82 recopiée en (−430, 0, 583) dans la liste de chaque réception qui la porte (`ListeDApparition`) (D4) — x à ajuster au premier essai
- [x] 3.2 Le déclenchement par `PasDuMembre` : debout, ~120 unités, face à 45°, bouton consommé, aucune boîte ouverte ; transfert vers le hub, réception d'origine mémorisée — point d'entrée 0 de la 82, à confirmer par le journal `[HUB]` d'un passage ordinaire
- [x] 3.3 Couture sur le transfert sortant du hub : destination = réception d'origine (D5)
- [x] 3.4 Couture sur `PlaceWorldMapParty` : groupe replacé devant la porte ajoutée au retour (D5) — point d'entrée recopié sans face imposée, écarts annulés
- [x] 3.5 La sauvegarde dans le hub écrit la réception d'origine (D6) : couture `LieuSauve` aux deux écrivains (menu, coffre) ; l'étiquette des repères n'est pas couverte : couture `LieuDeReprise` aux deux écrivains (`CopyGlobalBanksIntoSaveBlock`, `WorldMapDepot.cs`) et dans `CurrentMarkerTag` ; réception et point de reprise retenus au départ

## 4. Construction et documentation

- [x] 4.1 `dotnet build` du jeu sans nouvel avertissement ; `LegacyPlus.ini` avec `[Auberge] Hub = oui` dans `bin\Debug\net10.0\`
- [x] 4.2 Modèle de l'installeur `Reglages/LegacyPlus.ini` : la clé, commentée, à `non`
- [x] 4.3 `legacy.md` (tableau LP-009), `Reference/LegacyPlus.ini.md`, `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md`

## 5. Vérification en jeu (Vincent, depuis Rider)

- [ ] 5.1 `Hub = non` : la réception n'a pas de porte en plus, l'auberge est celle de 1998 — non vérifié en jeu à l'archivage
- [x] 5.2 Réception du palier 80 : la porte est visible ; face à elle, bouton d'action → le hub
- [x] 5.3 Le hub : porte et coffre seulement, aucun obstacle invisible sur tout le sol ; pas de boîte « - Le coffre - » ; coffre déplacé en (−1337, 886)
- [x] 5.4 Le coffre de stockage du hub : déposer, sortir, revenir, l'objet y est
- [x] 5.5 Sortir par la porte du hub : retour dans la même réception, en (−109, 504), après l'estrade
- [ ] 5.6 Même aller-retour depuis une réception d'un palier antérieur (sauvegarde ou menu de mise au point) — non vérifié en jeu à l'archivage
- [ ] 5.7 La carte 82 par le couloir de l'étage : meublée comme avant, boutique LP-008 intacte — non vérifié en jeu à l'archivage
- [x] 5.8 Sauvegarder dans le hub, recharger : la partie reprend dans la réception (rechargement sans le tweak non essayé)

Validé en jeu par Vincent le 2026-09-28 (« ça marche »), archivé à sa demande avec 5.1, 5.6 et 5.7 ouverts.
