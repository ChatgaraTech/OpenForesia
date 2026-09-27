## Why

Dans le jeu de 1998, une rencontre aléatoire n'arrive que lorsque la précédente est **entièrement
nettoyée**, puis après 200 à 300 images ; et elle ne fait naître qu'une formation (1 à 11 monstres,
le plus souvent 2 à 4). Vincent veut régler la difficulté par le nombre de monstres : **des spawns
plus fréquents, et plus de monstres par spawn**. Il a choisi le 2026-09-27 : délai raccourci **et**
chevauchement (une rencontre peut arriver pendant un combat), et plusieurs formations par rencontre.

## What Changes

- **LP-007 — les rencontres aléatoires réglables**, dans `[Rencontres]` de `LegacyPlus.ini` :
  - `Frequence` : un multiplicateur de la cadence ; le délai entre rencontres est divisé d'autant.
  - `MonstresMax` : 0 (jeu d'origine : il faut tout tuer avant la rencontre suivante) ou un plafond ;
    une nouvelle rencontre peut alors arriver pendant un combat tant que la sous-zone compte moins
    d'entités vivantes que ce plafond.
  - `Formations` : le nombre de formations qu'une rencontre fait naître, chacune tirée parmi les
    huit de la sous-zone et placée à part.
- **Les rencontres aléatoires seulement** : les monstres placés par script, les boss et les scènes
  (opcode 13) ne sont pas touchés.
- **Tout passe par le code d'origine** : le tirage de la formation, son placement « tout ou rien »,
  la naissance et le son. Le tweak ne fait que rappeler la fonction et lever deux de ses gardes.
- **Cinq lignes dans le legacy**, marquées `// Couture legacy ++ (LP-007)`.

## Capabilities

### New Capabilities

- `gameplay-encounters` : la cadence, le chevauchement et la taille des rencontres aléatoires.

### Modified Capabilities

(aucune)

## Impact

- **Legacy** : `Tick/WorldMapTick.cs` (l'appel), `Ai/Forest/Goblin.cs` (deux gardes de
  `TrySpawnRandomEncounter`), `Tick/WorldSimulationStep.cs` et `WorldMap.cs` (le rechargement du
  minuteur).
- **LegacyPlus** : `Coutures.cs`, `ReglagesLegacyPlus.cs`, un nouveau `Rencontres.cs`.
- **Installeur** : `Reglages/LegacyPlus.ini`, section `[Rencontres]`.
