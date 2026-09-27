# `LegacyPlus.ini` — les tweaks de gameplay

Le fichier à côté de l'exécutable où le joueur active les **tweaks** (registre :
`Documentation/LegacyPlus/legacy.md`). Même contrat qu'`Affichage.ini` :

- Le jeu le lit **une fois**, au premier coup ou à la première naissance de monstre, et ne l'écrit
  **jamais**. Il ne passe ni par les menus du jeu ni par `OpenForesia.cfg`.
- Absent, clé absente, valeur refusée : la valeur par défaut, clé par clé. Chaque refus fait une
  ligne `[LEGACY++]` dans le journal ; rien n'empêche le démarrage.
- **Tous les défauts rendent le jeu de 1998.**
- L'installeur en dépose un exemplaire commenté s'il n'y en a pas, et ne l'écrase jamais. Le modèle
  est `OpenForesia.Installer/Reglages/LegacyPlus.ini`. En développement, le copier à la main dans
  `OpenForesia\bin\Debug\net10.0\`.

## Les clés

| clé | valeurs | défaut | effet | tweak |
|---|---|---|---|---|
| `[Difficulte] PvMonstres` | multiplicateur, 0.1 à 10 | 1 | PV maximaux et courants de toute entité de scène à sa naissance | LP-002 |
| `[Difficulte] DegatsMonstres` | multiplicateur, 0.1 à 10 | 1 | dégâts des coups (mêlée et sorts) reçus par un membre du groupe | LP-001 |

`1` = jeu d'origine, `2` = deux fois plus. Les décimales s'écrivent avec un point ou une virgule
(`1.5` ou `1,5`), à deux chiffres au plus (le reste est arrondi).

## Ce qu'elles ne couvrent pas

- **`PvMonstres` s'applique à toute entité de scène**, décors cassables compris : le jeu ne
  distingue pas un monstre d'un tonneau à cet endroit.
- **`DegatsMonstres` ne touche que les coups.** Les dégâts en pourcentage des PV (`FUN_005397b0`), le
  poison et les ponctions de `WorldSimulationStep` ne changent pas.
- **Bornes** : le résultat est ramené entre 1 et 32 767. PV et dégâts sont des `short` signés dans le
  jeu, et au-delà un monstre naîtrait avec des PV négatifs, donc mort. Le plancher à 1 garde la règle
  d'origine : un coup ne fait jamais zéro.
- **La difficulté du jeu de 1998** (`s_DifficultyBonus_0056d2c0`, +0 à +30 % sur huit
  caractéristiques des entités marquées) reste en place ; les tweaks s'appliquent par-dessus.
