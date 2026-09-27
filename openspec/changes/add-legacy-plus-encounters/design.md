## Context

Voir `proposal.md`, et `add-legacy-plus-tweaks-difficulty/design.md` pour le cadre. Relevé :

- **`TrySpawnRandomEncounter`** (`FUN_0050ed70`, `Ai/Forest/Goblin.cs`), appelée à chaque pas de
  simulation (`WorldMapTick.cs`), en fin de pas. Ses gardes, dans l'ordre :
  1. le minuteur `s_Unknown_00564540` : non nul, il est décrémenté et la fonction rend la main ;
  2. `s_Unknown_005de544 == taille du groupe` ; la sous-zone sans table de rencontre (`-1`) ;
  3. **`*Mem.Entity(0x005f2d00) != 0`** : l'octet bas de l'en-tête du pool des entités de scène,
     c'est-à-dire le **nombre d'emplacements occupés** (`AllocEntitySlot`) — il faut qu'il n'y ait
     **aucune** entité de scène vivante ;
  4. un membre tiré au sort, debout.

  Puis elle tire une formation parmi les huit de la sous-zone (`rand() & 7`), en essaie chaque
  monstre à 400 unités du membre, et ne la fait naître (`SpawnEntityFormation`, puis le son `0x1b`)
  que si **tous** trouvent place.
- **Le minuteur** est rechargé à `(rand()%6 + 6) × 20` à l'entrée de la sous-zone (`WorldMap.cs`),
  et à `(rand()%6 + 10) × 20` par **chaque entité de scène vivante** qui le trouve à zéro
  (`TickEntityTable_005ecaa0`, qui tourne avant). Tant qu'un monstre vit, il est donc rechargé à
  l'image qui suit son passage à zéro, et la rencontre n'arrive jamais : c'est le « tout nettoyer
  d'abord ».
- **La table des entités de scène** compte 32 emplacements ; `SpawnEntityFormation` s'arrête sans
  erreur quand elle est pleine. Les 1 019 formations du CD comptent 1 à 11 monstres (moyenne ~3,4).
- **Les scripts** font naître leurs monstres par l'opcode 13 (`ScriptOpSpawnFormation`), un autre
  chemin, non touché.

## Goals / Non-Goals

**Goals :** cadence, chevauchement et taille des rencontres aléatoires, par le code d'origine.

**Non-Goals :** les monstres de script, les boss, les scènes ; la vitesse des monstres (abandonnée).

## Decisions

### D1. Un aiguillage autour de l'appel, deux gardes levables

```csharp
// WorldMapTick.cs — le pas de simulation
LegacyPlus.Coutures.Rencontre(TrySpawnRandomEncounter, Mem.Entity(0x005f2d00));                 // LP-007

// Goblin.cs — TrySpawnRandomEncounter
if (LegacyPlus.Coutures.RencontreEnAttente(s_Unknown_00564540 != 0)) { s_Unknown_00564540--; return; }   // LP-007
if (LegacyPlus.Coutures.RencontreBloquee(*Mem.Entity(0x005f2d00) != 0)) return;                          // LP-007
```

Par défaut, `Rencontre` appelle la fonction une fois, et les deux gardes rendent leur condition
d'origine. Le tweak peut **rappeler** la fonction en levant ces deux gardes pendant ce rappel
(un drapeau interne) ; toutes les autres gardes — groupe, table de la sous-zone, membre debout,
placement « tout ou rien » — restent celles d'origine. *Écarté* : réécrire le tirage et le placement
dans le tweak (une seconde copie d'une règle du jeu).

Le rappel ne décrémente pas le minuteur (la garde levée saute son corps) : le rythme d'origine
reprend tel quel dès que la sous-zone est nettoyée.

### D2. La fréquence : une couture de valeur sur les deux rechargements

```csharp
s_Unknown_00564540 = LegacyPlus.Coutures.DelaiRencontre((CrtRand() % 6 + 10) * 0x14);   // WorldSimulationStep.cs
s_Unknown_00564540 = LegacyPlus.Coutures.DelaiRencontre((roll % 6 + 6) * 0x14);         // WorldMap.cs
```

Le tirage reste dans la ligne d'origine : la suite aléatoire ne bouge pas quand le tweak est
inactif. Le tweak divise par `Frequence` (centièmes), au moins 1.

### D3. Le chevauchement : un minuteur propre au tweak

Avec `MonstresMax = N > 0`, à chaque image où des entités vivent (le pool n'est pas vide) mais moins
de N, le tweak décompte son minuteur ; à zéro, il rappelle la rencontre (D1) et le recharge à
**250 / Frequence** images — la moyenne d'origine, sans tirage (un tweak ne tire pas de nombre
aléatoire lui-même). Si la rencontre n'a rien fait naître (placement refusé), il réessaie
20 images plus tard. Quand la sous-zone est vide, c'est le chemin d'origine qui joue.

### D4. La quantité : des rappels après une rencontre réussie

Après tout appel qui a fait monter le nombre d'entités vivantes — une rencontre a eu lieu —, le
tweak rappelle `Formations - 1` fois la rencontre (D1). Chacune tire sa formation et son placement
par le code d'origine. Le plafond de la table (32) arrête proprement ce qui ne tient plus.

### D5. Les réglages

`[Rencontres] Frequence` (0.1 à 10, défaut 1), `MonstresMax` (0 à 12, défaut 0), `Formations`
(1 à 4, défaut 1). Tous les défauts = le jeu d'origine.

### D6. Un plafond absolu de 12 entités vivantes

Au premier essai en jeu, Vincent a vu **la carte se dégrader** quand trop de monstres étaient
présents. La cause est le **budget de 2 500 primitives par image** du moteur de 1998
(`s_Unknown_005e9818 = 0x9c4`, posé par `FrameBegin`) : toutes les boucles de dessin
(`Engine/Model3d.cs`, `Engine/Primitives2d.cs`) s'arrêtent quand il est épuisé, et ce qui vient
ensuite — des morceaux de décor — n'est pas dessiné. Vincent a choisi de **ne pas** relever la limite
pour l'instant et de plafonner le tweak à **12** : aucun chevauchement ni aucune formation
supplémentaire ne commence au-delà de 12 entités vivantes (`Rencontres.LimiteRendu`), et
`MonstresMax` est borné à 12. Relever la limite de rendu est consigné comme préconisation dans
`Documentation/LegacyPlus/legacy.md`.

## Risks / Trade-offs

- **[La suite aléatoire]** : actifs, ces tweaks font tirer plus de nombres au jeu (chaque rappel
  tire sa formation) ; c'est inhérent à « plus de monstres ». Inactifs, rien ne change.
- **[Chevauchement pendant un combat scripté]** : le pool compte **toutes** les entités de scène,
  boss compris. Avec `MonstresMax > 0`, une rencontre aléatoire peut arriver pendant un combat de
  boss, là où l'original l'interdisait (une sous-zone sans table de rencontre reste épargnée). →
  À vérifier en jeu ; si c'est gênant, le tweak pourra ne chevaucher qu'en l'absence d'entité
  marquée boss (`stat + 0x14 & 0x80000000`), sans toucher aux coutures.
- **[Les autres pools]** : plus de monstres, c'est plus de butin et d'effets ; ces pools refusent
  proprement quand ils sont pleins (un butin peut ne pas tomber).
