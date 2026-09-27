# Legacy ++ — le registre des tweaks

Les **tweaks de gameplay** changent l'équilibre ou les règles du jeu de 1998, au choix du joueur,
dans `LegacyPlus.ini`. Ils sont **tous désactivés par défaut** : sans le fichier, ou avec toutes les
clés à leur défaut, c'est le jeu de 1998.

Ce sont des numéros `LP-NNN`, distincts des `LB-NNN` de `Reference/legacy-bug.md`, qui corrigent les
défauts du jeu d'origine.

**La règle** : aucune condition d'option dans le code transcrit (`src/Blaze/`). Le legacy n'appelle
que des **coutures** (`src/LegacyPlus/Coutures.cs`), dont le défaut ne change rien. Chaque couture est
marquée sur place `// Couture legacy ++ (LP-NNN)`. Le code d'un tweak vit dans `src/LegacyPlus/`, un
fichier par tweak. Un tweak ne tire jamais de nombre aléatoire.

Les clés et leurs bornes : `Documentation/Reference/LegacyPlus.ini.md`.

## Les tweaks

> Au passage (LP-005) : dans `DespawnSceneEntity_0051a740`, le commentaire « rien que de l'argent »
> désigne en fait **le cristal d'expérience** (`0x80081000`) ; ce sont les « éclats »
> (`0x90081000`) qui sont **les pièces**. Le ramassage (`PartyMemberStep.cs`) fait foi.


| N° | Tweak | Clé (`LegacyPlus.ini`) | Couture | État |
|---|---|---|---|---|
| LP-001 | Dégâts des monstres, multipliés | `[Difficulte] DegatsMonstres` | `DegatsSubis`, entrée d'`ApplyDamage_00539A20` | écrit, à tester en jeu — `src/LegacyPlus/Difficulte.cs` |
| LP-002 | PV des monstres, multipliés | `[Difficulte] PvMonstres` | `PvALaNaissance`, `ResetEntityStats` (`FUN_005389e0`) | écrit, à tester en jeu — `src/LegacyPlus/Difficulte.cs` |
| LP-003 | Montant des sorts du groupe × (1 + coef × MAG / 100) | `[Magie] ScalingMagique` | `MontantDeSort`, ajout du montant dans `ResolveSpellHit_0053A2B0` | écrit, à tester en jeu — `src/LegacyPlus/ScalingMagique.cs` |
| LP-005 | Ramassage automatique des pièces et des cristaux d'expérience (pas les coffres) | `[Butin] RamassageAuto` | `ButinAtteint`, l'entité que la collision désigne dans `PartyMemberStep.cs` | validé en jeu (2026-09-27) — `src/LegacyPlus/RamassageAuto.cs` |
| LP-006 | Se déplacer en lançant un sort, à une part de la vitesse de marche | `[Magie] DeplacementEnIncantation` | `VitesseEnIncantation`, le zéro de vitesse des postures d'incantation dans `ApplyGroundMotion_0051A020` | écrit, à tester en jeu — `src/LegacyPlus/DeplacementEnIncantation.cs` |
| LP-004 | Frames d'invulnérabilité des ennemis tenues par attaquant | `[Combat] InvulnerabiliteEnnemis` | `VictimeExclue` (bit `0x40`), `VictimeProtegee` (`+0x96`), `CoupPorte` dans les deux balayages (`FUN_00519940`, `FUN_00519af0`), `ImageSuivante` en tête du pas de simulation (boucle par image de `RunWorldMapTick`) | validé en jeu (2026-09-27) — `src/LegacyPlus/VerrouParAttaquant.cs` |
| — | Vitesse des monstres | à définir | `IntegrateSceneEntityMotion_00488160` ? | idée — risque : vitesses en dur dans 166 gestionnaires |
| — | Monstres par spawn, fréquence des spawns | à définir | à trouver | idée — mesurer d'abord la marge des pools d'entités |
