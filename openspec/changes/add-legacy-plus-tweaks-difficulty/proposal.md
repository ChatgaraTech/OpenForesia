## Why

La version 1.0.0 rejoue le jeu de 1998 tel qu'il était. Vincent ouvre maintenant la famille des
**tweaks de gameplay** : auto-loot, difficulté réglable, meilleur scaling des dégâts magiques,
déplacement pendant les sorts, frames d'invulnérabilité. Ces tweaks changent l'équilibre du jeu, ce
que `Strategy.md` réserve à une demande explicite de Vincent : c'est cette demande.

Il y a une contrainte posée d'emblée : **aucun `if (option)` dans le code transcrit existant.** La
base legacy doit rester lisible contre `Blaze.c`, et un tweak doit pouvoir disparaître sans laisser
de trace dans le code transcrit. Il faut donc un mécanisme avant les tweaks. Ce premier lot le pose
et le valide sur le tweak le plus simple : **les PV et les dégâts des monstres**.

## What Changes

- **Un répertoire `OpenForesia/src/LegacyPlus/`**, séparé de `src/Blaze/`. Il porte le code des
  tweaks, un fichier par tweak, et la lecture de leur fichier de réglages.
- **Les coutures (`Coutures`)** : les seuls points par lesquels le code legacy s'ouvre aux tweaks.
  Une couture est une fonction appelée par le code legacy, dont le défaut est l'identité, soit le
  jeu de 1998. Le legacy n'y nomme jamais un tweak. Un tweak actif s'y branche au démarrage.
- **Trois lignes touchées dans le legacy pour ce lot**, chacune marquée
  `// Couture legacy ++ (LP-NNN)` :
  - `ApplyDamage_00539A20` (`FUN_00539a20`), le point par lequel passent tous les coups, de mêlée
    comme de sort : le montant des dégâts passe par une couture ;
  - `ResetEntityStats` (`FUN_005389e0`), la naissance des statistiques d'une entité de scène : les
    PV maximaux et courants passent par une couture.
- **`LegacyPlus.ini`** à côté de l'exécutable. Le jeu le lit une fois et ne l'écrit jamais, sur le
  même contrat qu'`Affichage.ini`. Section `[Difficulte]`, deux multiplicateurs (1 = jeu d'origine) : `PvMonstres` et
  `DegatsMonstres`. Absent, ou à 1 : le jeu de 1998 exactement.
- **L'installeur dépose un `LegacyPlus.ini` commenté**, tweaks désactivés, sans jamais écraser
  celui du joueur.
- **`Documentation/LegacyPlus/legacy.md`** : le registre succinct des tweaks, avec leur numéro
  `LP-NNN`, leur clé et leur couture. Il s'enrichira tweak par tweak.

Hors de ce lot : les dégâts en pourcentage des PV (poison, pièges, `FUN_005397b0`, et les ponctions
de `WorldSimulationStep`), les autres tweaks de la liste, et le configurateur graphique.

## Capabilities

### New Capabilities

- `gameplay-tweaks` : le cadre des tweaks — le fichier de réglages lu sans être écrit, la garantie
  qu'un jeu sans tweak actif est le jeu de 1998, la règle qui interdit toute condition d'option dans
  le code transcrit.
- `gameplay-difficulty` : les PV et les dégâts des monstres multipliés, de ×0,1 à ×10.

### Modified Capabilities

- `game-data-install` : l'installeur fournit aussi un `LegacyPlus.ini` par défaut, sans écraser
  celui du joueur.

## Impact

- **Code legacy** : `Screens/State03/Combat/HitResolution.cs` (une ligne) et
  `Screens/State03/WorldMapScript.cs` (deux lignes). Rien d'autre, pas même le démarrage.
- **Code nouveau** : `OpenForesia/src/LegacyPlus/` (coutures, réglages, difficulté).
- **Installeur** : `Reglages/LegacyPlus.ini` embarqué, déposé comme `Affichage.ini`.
- **Documentation** : `Documentation/LegacyPlus/legacy.md` (nouveau), `ARCHITECTURE.md` (le
  répertoire `LegacyPlus/` et sa règle), `Strategy.md` (la famille des tweaks et la numérotation
  `LP-NNN`), `CLAUDE.md` (la table des chemins).
- **Release** : `LegacyPlus.ini` suit les règles du trim (lecture écrite à la main, pas de
  réflexion), comme `Affichage.ini`.
