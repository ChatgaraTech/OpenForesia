## Why

Dans le jeu de 1998, un ennemi touché devient intouchable pendant quelques images : un verrou
unique, `+0x96`, qui clignote et se décompte. Ce verrou est **partagé** entre tous les attaquants.
À quatre joueurs, après le coup du premier, les trois autres frappent dans le vide jusqu'à la fin
du clignotement. Vincent veut que **tout** puisse toucher un ennemi (mêlée, magie, projectiles),
**sans qu'une même attaque puisse compter plusieurs fois**.

## What Changes

- **LP-004 — un verrou par attaquant** au lieu d'un verrou partagé. Un ennemi qui clignote reste
  intouchable **pour l'attaque qui vient de le toucher**, pendant la même durée que dans le jeu
  d'origine, mais les autres attaques (un autre joueur, un autre projectile) passent.
- Portée : tout ce qui cherche un ennemi à toucher par les deux balayages du jeu — la mêlée du
  groupe (`FUN_00519940`) et les projectiles et sorts du groupe (`FUN_00519af0`, atteint par
  `ProbeMeleeTarget` et `SweepOpposingSideForMeleeHit`).
- Le clignotement, le recul et l'interruption de l'ennemi **restent** : chaque coup les relance
  comme dans le jeu d'origine.
- Les frames d'invulnérabilité **du groupe** (quand un monstre frappe un joueur) ne changent pas.
- **Une clé** `[Combat] InvulnerabiliteEnnemis = oui` (jeu d'origine) ou `non` (verrou par
  attaquant), dans `LegacyPlus.ini`.
- **Sept lignes dans le legacy**, toutes marquées `// Couture legacy ++ (LP-004)` : deux filtres d'état
  (le bit `0x40`, second verrou), deux conditions
  de verrou, deux notifications de coup porté, et une horloge d'images en tête de la boucle de la
  carte du monde.

## Capabilities

### New Capabilities

- `gameplay-enemy-iframes` : le verrou d'invulnérabilité des ennemis tenu par attaquant.

### Modified Capabilities

(aucune)

## Impact

- **Legacy** : `Physics/TriggerVolumes.cs` (3 lignes), `Combat/Projectiles.cs` (3 lignes),
  `Tick/WorldMapTick.cs` (1 ligne).
- **LegacyPlus** : `Coutures.cs` (trois coutures), `ReglagesLegacyPlus.cs` (la clé), un nouveau
  `VerrouParAttaquant.cs`.
- **Installeur** : `Reglages/LegacyPlus.ini`, section `[Combat]`.
- **Dépend de** `add-legacy-plus-tweaks-difficulty` (le cadre).
