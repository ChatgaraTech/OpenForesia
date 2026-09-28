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
| `[Magie] ScalingMagique` | 0 à 5 | 0 | montant des sorts **du groupe** × (1 + valeur × MAG / 100) ; 1 conseillé | LP-003 |
| `[Magie] DeplacementEnIncantation` | 0 à 1 | 0 | part de la vitesse de marche gardée pendant une incantation ; 0.5 conseillé ; le lanceur garde sa pose et glisse | LP-006 |
| `[Rencontres] Frequence` | 0.1 à 10 | 1 | cadence des rencontres aléatoires ; chaque délai est divisé d'autant | LP-007 |
| `[Rencontres] MonstresMax` | 0 à 12 | 0 | 0 : tout tuer avant la rencontre suivante ; N : une rencontre peut arriver en plein combat tant que moins de N entités de scène vivent | LP-007 |
| `[Rencontres] Formations` | 1 à 4 | 1 | formations par rencontre, chacune tirée parmi les huit de la sous-zone et placée à part ; jamais au-delà de 12 monstres vivants | LP-007 |
| `[Butin] RamassageAuto` | `oui`, `non` | `non` | `oui` : pièces et cristaux d'expérience ramassés une fois posés, sans marcher dessus ; pièces à tour de rôle entre les joueurs debout ; jamais les coffres | LP-005 |
| `[Auberge] Hub` | `oui`, `non` | `non` | `oui` : une porte dans la salle de chaque réception de l'auberge mène au hub (sous-zone 86, la carte 82 vidée hormis porte et coffre) ; sa porte ramène à la réception d'origine ; une sauvegarde faite dans le hub reprend dans la réception | LP-009 |
| `[Combat] InvulnerabiliteEnnemis` | `oui`, `non` | `oui` | `non` : un ennemi qui clignote ne refuse plus que l'attaque qui l'a touché, pas les autres | LP-004 |

Pour les deux clés de difficulté, `1` = jeu d'origine, `2` = deux fois plus. Pour
`ScalingMagique`, `0` = jeu d'origine. Les décimales s'écrivent avec un point ou une virgule
(`1.5` ou `1,5`), à deux chiffres au plus (le reste est arrondi).

**`ScalingMagique` en chiffres** (Sorcier niveau 200, MAG ≈ 130, monstre de fin de jeu, DEF
magique ≈ 100) : Balle de feu ~7 → ~23, Boule de feu ~97 → ~191 à la valeur 1. En début de partie
(MAG ≈ 5), presque rien ne change. Le montant ne s'ajoute toujours pas si la victime est déjà de
l'élément du sort, comme dans le jeu d'origine. La part `MAG / diviseur − DEF` de la formule n'est
pas touchée.

**`InvulnerabiliteEnnemis = non`** : dans le jeu d'origine, un ennemi touché devient intouchable
pour **tout le monde** tant qu'il clignote (le compteur `+0x96` et le bit d'état `0x40`). Avec
`non`, seule **la même attaque** est refusée : un projectile ou un sort pendant la durée du verrou
qu'il a posé, un geste de mêlée tant qu'il est tenu sur son image de frappe. Les autres joueurs,
flèches et sorts passent, et un guerrier enchaîne ses coups ; une même attaque ne compte jamais
deux fois. Le clignotement, le recul et l'interruption restent ceux d'origine. Les frames
d'invulnérabilité **du groupe** ne changent pas.

## Les boutiques des marchands du hub — `LegacyPlus\Boutique*.ini` (LP-010)

Quatre fichiers à part, dans le dossier `LegacyPlus\` à côté de l'exécutable, un par marchand du hub
(`[Auberge] Hub = oui` requis) : `BoutiqueObjets.ini` (le Lutin, objets utilisables),
`BoutiqueAccessoires.ini` (le Voleur), `BoutiqueArmures.ini` (le Kobold, armures et boucliers),
`BoutiqueArmes.ini` (la Sylphide). Même contrat : lus une fois, jamais écrits.

| clé | valeurs | défaut | effet |
|---|---|---|---|
| `[Boutique] Actif` | `oui`, `non` | `non` | `oui` : le marchand se tient dans le hub et vend ; `non` : pas de marchand |
| `[Catalogue] <nom> = <prix>` | 1 à 99 999 | — | un objet en vente, dans l'ordre du fichier ; le nom exact du jeu ; `;` devant la ligne la retire |

Le jeu relit la catégorie de chaque objet dans sa fiche : un objet d'une autre catégorie n'est pas
proposé. Chez les quatre marchands, chaque joueur ne voit que ce que sa classe peut porter ou
utiliser, d'après la fiche (33 des 119 accessoires sont réservés à certaines classes ; parmi les
objets utilisables, seules les sept herbes le sont, au Chasseur). Les
modèles sont générés par `node OpenForesia.Tools/generer-boutique.mjs`, qui reprend les prix déjà
écrits. `Boutique.ini` (l'armoire, LP-008) n'est plus lu.

**`BoutiqueVente.ini` — la Voleuse, qui rachète (LP-011).** `[Boutique] Actif` comme les autres, et
`[Vente] Taux` (1 à 100, défaut 50) : le pourcentage du prix d'achat payé au joueur. Pas de
catalogue : un objet se revend s'il figure dans l'une des quatre boutiques (active ou non), au prix
qu'il y a. Elle rachète l'inventaire du joueur sauf ce qu'il porte, et son coffre ; une pile se vend
en entier. **Vendre depuis le coffre écrit la sauvegarde du personnage tout de suite** (l'objet sort
du coffre, l'or entre dans la bourse enregistrée).

## Diagnostic (absent du modèle de l'installeur)

**Version Debug seulement** : la version publiée (`publish-release.ps1`, `-c Release`) ne les
installe jamais, même avec la clé (`#if DEBUG` dans `Coutures.cs`). `TracePosition` écrit aussi
chaque ligne dans la console, préfixée `[POSITION]` — lancé depuis Rider, elle s'affiche dans la
fenêtre d'exécution, avec la zone et la carte.

| clé | valeurs | défaut | effet |
|---|---|---|---|
| `[Diagnostic] TraceCombat` | `oui`, `non` | `non` | journalise chaque coup de mêlée du groupe dans `legacyplus-combat.log`, à côté de l'exe |
| `[Diagnostic] TracePosition` | `oui`, `non` | `non` | écrit la position du joueur 1 (unités de `formations.csv`) et son cap à chaque arrêt d'une seconde, dans `legacyplus-position.log` |

Pour chaque ennemi que le coup examine : `ÉCARTÉ` (bits d'état), `REFUSÉ` (même geste, ou verrou
d'origine), `TOUCHÉ`, ou `RATÉ` (hors de portée, ou paré ; le journal dit si l'ennemi porte la garde
`0x10`). Le fichier est réécrit à chaque lancement. Code : `src/LegacyPlus/TraceCombat.cs`.

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
