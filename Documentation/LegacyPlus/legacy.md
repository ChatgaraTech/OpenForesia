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

| N° | Tweak | Clé (`LegacyPlus.ini`) | Couture | État |
|---|---|---|---|---|
| LP-001 | Dégâts des monstres, multipliés | `[Difficulte] DegatsMonstres` | `DegatsSubis`, entrée d'`ApplyDamage_00539A20` | écrit, à tester en jeu — `src/LegacyPlus/Difficulte.cs` |
| LP-002 | PV des monstres, multipliés | `[Difficulte] PvMonstres` | `PvALaNaissance`, `ResetEntityStats` (`FUN_005389e0`) | écrit, à tester en jeu — `src/LegacyPlus/Difficulte.cs` |
| — | Meilleur scaling des dégâts magiques | à définir | `ResolveSpellHit_0053A2B0` (formule `MAG / fiche[0x1e]`) | idée |
| — | Auto-loot des pièces et des gemmes | à définir | gestionnaire d'entité, par adresse | idée — gestionnaire à identifier |
| — | Se déplacer en lançant un sort, à 50 % de la vitesse | à définir | posture ou vitesse du membre du groupe | idée — à investiguer |
| — | Retirer les frames d'invulnérabilité des ennemis | à définir | lecture du verrou `+0x96` | idée — point de lecture à trouver |
| — | Vitesse des monstres | à définir | `IntegrateSceneEntityMotion_00488160` ? | idée — risque : vitesses en dur dans 166 gestionnaires |
| — | Monstres par spawn, fréquence des spawns | à définir | à trouver | idée — mesurer d'abord la marge des pools d'entités |
