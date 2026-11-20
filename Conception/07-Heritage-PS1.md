# Ce qui vient de la machine, pas du dessein

Le jeu a été écrit pour une PlayStation (2 Mo de mémoire, un lecteur de CD, une manette sans
joystick analogique au départ), puis porté sur PC. Beaucoup de ses règles en portent la marque. Ce
document les sépare en trois : **les contraintes de la machine** — à réinterpréter, pas à porter ;
**les défauts** — à ne pas reproduire ; **les étrangetés de conception** — à trancher. Il finit par
ce que le portage actuel a déjà essayé (Legacy Plus).

Sources : `Documentation/Reference/legacy-bug.md` (60 défauts, chacun prouvé), 
`Documentation/LegacyPlus/legacy.md` (les améliorations), `Documentation/ARCHITECTURE.md`.

## Les contraintes de la machine — à réinterpréter

| dans le jeu de 1998 | pourquoi | dans un jeu neuf |
|---|---|---|
| **Tout se compte en images** : durées, incantations, gestes, effets (« le coup porte à l'image 8 ») | pas d'horloge, une cadence fixe | des secondes, et des *notifies* d'animation pour les fenêtres de coup ; garder les chiffres comme **proportions** entre gestes |
| **Arithmétique entière** : divisions qui tronquent, décalages de bits (`/4`, `/32`, `>>3`) | pas de flottants rapides | des flottants ; les formules gardent leur forme, et les arrondis cessent de créer des paliers invisibles (les multiples de 8, [02](02-Personnages.md)) |
| **32 entités de scène au plus** par carte, des réserves séparées pour les effets | mémoire | aucune limite technique ; décider de la taille des groupes de monstres **par dessein** |
| **Les comportements sont rechargés à chaque carte**, et un numéro de sous-type ne veut rien dire hors de sa carte | mémoire : chaque carte n'embarque que ses routines | un acteur par espèce et par objet de décor, réutilisable partout |
| **Les monstres sont des copies** : la même bête est recompilée dans chaque zone, avec ses constantes (112 routines pour bien moins de comportements) | chargement par carte | une IA paramétrée par espèce ([03](03-Combat.md)) |
| **Le coup est une « sonde »** : une portée et une tolérance en hauteur, testées à une image donnée | pas de moteur physique | des volumes de collision sur l'arme et le corps, actifs pendant une fenêtre |
| **Textures 4 bits à palette**, vignettes 24×24, **palettes animées** (eau, feu par rotation de couleurs) | mémoire vidéo | matériaux modernes ; l'animation de palette devient un matériau animé |
| **Planches de sprites** pour les effets (le sort est une suite d'images 2D) | pas de particules | des systèmes de particules (Niagara) ; la planche dit seulement la **durée** et la **forme** |
| **Sauvegarde au format carte mémoire** (en-tête, somme de contrôle, blocs) | la carte mémoire PS1 | un fichier de sauvegarde ordinaire ; `FormatSauvegarde.md` reste utile pour **la liste** des données à sauver |
| **Les sorts par séquences de boutons**, SELECT pour combiner | une manette à boutons, pas de souris | à repenser : raccourcis, roue, ou combinaisons courtes et personnalisables |
| **Les cinématiques en vidéo** (Indeo 4) | pas de moteur de scènes temps réel suffisant | des scènes en moteur (Sequencer) |
| **Le texte en Latin-1 avec codes de contrôle** (pictogrammes, nom du locuteur inséré) | pas d'Unicode | des chaînes localisables ; les codes deviennent des balises |

## Les défauts — à ne pas reproduire

Le dépôt en recense **60**, chacun tracé dans le code. La plupart concernent le moteur de rendu ou le
portage PC et n'ont aucun sens pour un jeu neuf. Ceux qui touchent au **jeu** :

- **LB-024** — le sort « Ange » ne balaie que trois des quatre membres du groupe ;
- **LB-026** — « Champ magique » et « Escargot » visent le **dernier** candidat trouvé, pas le plus
  proche ;
- **LB-017, LB-018** — une flèche magique qui perd sa cible, une boule de feu qui repousse, partent
  dans une direction arbitraire ;
- **LB-032** — le second coup d'un boss du château ne part jamais ;
- **LB-050** — un monstre qui cherche une proie dans un groupe indisponible tourne sans fin (le jeu
  se fige) ;
- **LB-030, LB-051, LB-052** — des souffles et des sorts de boss font clignoter tout l'écran, un fondu
  vers le blanc donne un écran blanc ;
- **LB-043** — la seconde manette ignore son joystick ;
- **LB-001, LB-059, LB-060** — la saisie du nom bloque ou ne répond qu'à une vraie manette.

Le reste de la liste se lit dans `legacy-bug.md` ; chaque entrée dit ce que le jeu aurait dû faire.

## Les étrangetés de conception — à trancher

Pas des bogues : le code fait bien ce qu'il fait. Mais un joueur ne peut pas le deviner.

| règle | où |
|---|---|
| les bonus de caractéristiques d'un objet ne changent ni l'ATT, ni la DEF, ni la MATT | [02](02-Personnages.md) |
| les affinités élémentaires d'un objet ne comptent que dans les deux emplacements d'accessoire | [05](05-Objets.md) |
| le surplus d'expérience est perdu à chaque niveau | [02](02-Personnages.md) |
| investir trop dans une caractéristique abaisse son plafond | [02](02-Personnages.md) |
| seuls les multiples de 8 des valeurs de départ accélèrent la croissance | [02](02-Personnages.md) |
| un sort sans assez de PM part en animation et ne fait rien, sans message | [04](04-Magie.md) |
| la durée d'incantation est celle du sort mémorisé, pas du sort combiné | [04](04-Magie.md) |
| l'Elfe partage l'école du Sorcier mais s'arrête à 20 sorts sur 26 | [04](04-Magie.md) |
| une école entière (l'herboristerie du Chasseur) est écrite et inaccessible | [04](04-Magie.md) |
| être sous-niveau ne pénalise pas le butin ; seule l'avance l'améliore | [05](05-Objets.md) |

## Ce que le portage a déjà essayé — Legacy Plus

Le portage actuel ajoute, en option et éteintes par défaut, seize améliorations. Plusieurs ont été
validées en jeu ; ce sont des **pistes éprouvées** pour un jeu neuf :

| | amélioration |
|---|---|
| LP-001, LP-002 | multiplicateurs de dégâts et de PV des monstres (difficulté) |
| LP-003 | les dégâts des sorts du groupe croissent avec la puissance magique |
| LP-004 | invulnérabilité brève des ennemis **par attaquant**, pour que quatre joueurs ne s'annulent pas |
| LP-005 | ramassage automatique de l'argent et des cristaux d'expérience |
| LP-006 | se déplacer en incantant, à vitesse réduite |
| LP-007 | rencontres aléatoires (cadence, nombre de monstres) |
| LP-008, LP-010, LP-011 | une boutique, des marchands au hub, le rachat de l'inventaire |
| LP-009, LP-012, LP-013, LP-014 | un hub étendu avec une arène : boss d'autres zones, *boss rush*, quatre paliers de difficulté |
| LP-015 | les sorts de soutien de la Fée s'appliquent à tout le groupe |
| LP-016 | des raccourcis de sort saisis par personnage, au lieu des combinaisons fixes |

Le détail et la mesure de chacun sont dans `Documentation/LegacyPlus/legacy.md`.
