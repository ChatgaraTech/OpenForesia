## Context

Voir `proposal.md`, et `add-legacy-plus-tweaks-difficulty/design.md` pour le cadre. Relevé :

- **Le verrou** est le `ushort` `+0x96` de l'enregistrement d'entité. Non nul, un coup est refusé.
  Pour une entité de scène, il est décompté d'une unité par image dans `WorldSimulationStep`
  (`record + 0x96`), avec le clignotement ; à zéro, une entité morte déroule sa fin.
- **Tout ce qui cherche un ennemi à toucher du côté du groupe passe par deux balayages** :
  - `SweepSceneEntitiesForMeleeHit_00519940` (`Physics/TriggerVolumes.cs`), la mêlée. Il est appelé
    **une fois par geste**, sur la dernière image de l'animation (`PartyMemberStep.cs:1442`). Il
    saute une victime verrouillée, puis pose `victim[0x96] = 0x10` après le coup.
  - `SweepSceneEntitiesForMeleeHit_00519AF0` (`Combat/Projectiles.cs`), qui cherche une victime
    pour une entité du groupe (projectile, sort) : 50 sites d'appel, par `ProbeMeleeTarget_00474CA0`
    et `SweepOpposingSideForMeleeHit_00474C70`. Il saute une victime verrouillée et **rend la
    première touchable** ; l'appelant la frappe et pose le verrou, **à une durée qui lui est
    propre** (0x10, 8, 4 ou 0x3c selon le projectile).
- **Il y a un second verrou**, manqué à la première rédaction et révélé par le premier essai en jeu
  (les projectiles traversaient les ennemis) : dans les deux balayages, le filtre d'état
  `mot & 0x50102040` écarte aussi le bit **`0x40`** (« encaisse un coup »). Chaque coup le pose
  (`*victim |= 0x40`), et il ne retombe **qu'à la fin du clignotement** (`WorldSimulationStep`,
  quand `+0x96` atteint zéro). Lever `+0x96` seul ne changeait donc rien.
- Un projectile qui dure plusieurs images **compte sur le verrou** pour ne pas frapper à chaque
  image. C'est la contrainte « une attaque ne compte pas plusieurs fois ».
- L'invulnérabilité scriptée de la Vieille Tour (garde de salle) passe par une défense de 30 000,
  **pas** par `+0x96` : le tweak ne la touche pas.
- Le jeu n'a pas de compteur d'images global exploitable.

## Goals / Non-Goals

**Goals :** plusieurs attaquants frappent un ennemi qui clignote ; une même attaque garde le
rythme d'origine contre une même victime.

**Non-Goals :** les frames d'invulnérabilité du groupe ; les corps qui frappent sans passer par
les deux balayages (ils ne testent pas le verrou, rien à changer) ; le choix de cible d'un
projectile à tête chercheuse (`PickTargetInCone_00474F00`, qui préfère une cible qui ne clignote pas).

## Decisions

### D1. Un verrou par couple (source, victime), tenu dans `LegacyPlus`

La **source** est ce que le balayage reçoit comme attaquant : le membre du groupe pour la mêlée,
l'entité projectile ou sort pour l'autre balayage. Un couple enregistré au coup porté bloque cette
source contre cette victime jusqu'à expiration. Rien n'est écrit dans la mémoire du jeu : `+0x96`
continue d'être posé et décompté comme à l'origine, donc le clignotement, le recul, l'interruption
et la fin d'une entité morte sont intacts.

*Écartés* : retirer simplement le refus (un projectile durable frapperait à chaque image) ;
mémoriser les attaquants tant que le clignotement dure (quatre joueurs qui se relaient réarment
le clignotement sans fin, et chacun resterait bloqué pour toujours après son premier coup).

### D2. Sept coutures

```csharp
// TriggerVolumes.cs — mêlée
if (LegacyPlus.Coutures.VictimeExclue(attacker, victim, flags & 0x50102040) != 0) continue;           // LP-004
if (LegacyPlus.Coutures.VictimeProtegee(attacker, victim, *(short*)(victim + 0x96) != 0)) continue;   // LP-004
...
LegacyPlus.Coutures.CoupPorte(attacker, victim);                                                         // LP-004, juste avant victim[0x96] = 0x10

// Projectiles.cs — FUN_00519af0
if (LegacyPlus.Coutures.VictimeExclue(entity, found, *(uint*)found & 0x50102040) != 0) continue;      // LP-004
if (LegacyPlus.Coutures.VictimeProtegee(entity, found, *(short*)(found + 0x96) != 0)) continue;       // LP-004
...
LegacyPlus.Coutures.CoupPorte(entity, found);                                                           // LP-004, juste avant return found

// WorldMapTick.cs — en tête du PAS DE SIMULATION, dans la boucle par image (voir D4)
LegacyPlus.Coutures.ImageSuivante();                                                                    // LP-004
```

Par défaut, `VictimeExclue` et `VictimeProtegee` rendent la valeur d'origine, et les deux autres ne
font rien. Les coutures de condition gardent le test d'origine lisible dans la ligne.

`VictimeExclue` ne lève **que** le bit `0x40`, et seulement pendant une fenêtre de coup (`+0x96`
non nul) pour une source qui ne l'a pas ouverte. Hors fenêtre, un `0x40` garde son sens d'origine,
et les autres bits (mort `0x40000000`, etc.) écartent toujours la victime.

### D3. La durée d'un couple est celle du verrou que le coup a posé

Au coup, le balayage ne connaît pas la durée : c'est l'appelant qui l'écrit ensuite. Le couple est
donc créé **en attente**, et `ImageSuivante` lit `+0x96` de la victime à l'image suivante pour fixer
sa durée (lecture + 1, pour la décrémentation déjà passée ; 16 si le verrou est déjà retombé). Un
projectile garde ainsi exactement son rythme d'origine contre une victime.

### D3 bis. La mêlée : un geste tenu, pas une durée

La première version appliquait D3 à la mêlée aussi. Au premier essai en jeu, Vincent a constaté
qu'un guerrier ne pouvait pas enchaîner : pour la mêlée, la source est le **membre du groupe**, donc
son deuxième geste était pris pour la même attaque et refusé pendant 16 images, comme à l'origine.

Or en mêlée, « un coup ne compte pas deux fois » est déjà presque garanti par le jeu : le balayage ne
tourne que sur la **dernière image** d'un geste (`PartyMemberStep.cs:1437`). Presque, parce que le
jeu peut **garder** cette image plusieurs tours (`PartyMemberStep.cs:183-193`, quand l'état porte
`0x40000005`), et le coup repartirait alors à chaque tour. La règle retenue : un couple
(membre, victime) est refusé tant qu'il y a eu un contact **au tour précédent** — le geste est tenu —,
et chaque refus prolonge le contact. Un nouveau geste passe par d'autres images, laisse donc au moins
un tour sans contact, et frappe.

Les coutures portent pour cela le balayage qui appelle (`Coutures.Balayage.Melee` ou
`.Projectile`). C'est le nom d'un emplacement, pas celui d'un tweak.

### D4. L'horloge

`ImageSuivante`, appelée une fois par image de la carte du monde, avance un compteur propre au tweak
et purge les couples expirés. Elle est posée **en tête du pas de simulation**, juste avant les passes
sur les tables d'entités, là où le jeu décompte ses propres verrous : les deux horloges avancent
ensemble, et s'arrêtent ensemble quand la simulation est suspendue.

**Première pose, fausse** : à l'entrée de `RunWorldMapTick`. Cette fonction contient elle-même la
boucle par image et n'est entrée qu'**une fois par chargement de zone** — son propre commentaire
le signale. Le journal `TraceCombat` de Vincent l'a montré : l'image valait 2 sur toutes les lignes.
L'horloge figée faisait prendre chaque coup pour « le même geste », et seuls passaient les coups dont
le verrou d'origine était déjà retombé, d'où un comportement « parfois oui, parfois non ».

### D5. `[Combat] InvulnerabiliteEnnemis = oui | non`

Un booléen lisible : `oui` = jeu d'origine. `non` installe le tweak.

## Risks / Trade-offs

- **[Emplacement réutilisé]** : un couple est indexé par l'adresse de la source. Un nouveau
  projectile qui renaît dans l'emplacement d'un projectile mort, contre la même victime, avant la
  fin de la fenêtre (au plus 60 images), serait bloqué à tort. → C'est rare, et l'effet est celui
  du jeu d'origine (un coup refusé).
- **[Dégâts plus élevés]** : un joueur seul qui enchaîne vite n'est pas concerné (même source, même
  verrou), mais un joueur accompagné d'un projectile ou d'un sort durable touche davantage. C'est
  le but.
- **[Ennemis enchaînés]** : quatre joueurs peuvent interrompre un ennemi ordinaire sans arrêt. Les
  boss ne sont pas interrompus dans le jeu d'origine (`InterruptSceneEntity_004E3770`), donc ils
  restent dangereux.
