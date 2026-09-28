## Context

Ce que l'exploration du 2026-09-28 a établi (voir `proposal.md` pour le pourquoi) :

- **QUEST / MAP du menu de mise au point sont la zone et la sous-zone, sans décalage**
  (`WorldMapDebugMenu.cs:327`). « QUEST 4 MAP 9 » est donc 4/9, « Parmi les Elus » (la vieille tour).
- **4/9 aujourd'hui** (`Bestiaire/*.csv`) : formation 0 = l'**Enchanteresse** (espèce 1, 910 PV,
  rang 140) en (100, −2200) avec deux Ténébreux ; formation 1 = trois Platinum. Le décor porte, entre
  autres, la **scénette de sortie** (sous-type 22, `0x00474380`, `Handlers/Forest.cs:1450`) en
  (100, 205, −2700), un coffre (sous-type 28, `0x00473ED0`) en (600, 240, 2900) et un effet
  d'ambiance (sous-type 56, `0x004746C0`).
- **La scénette de sortie** attend qu'on lui pose l'état 1 ; elle détourne alors la caméra, fait
  grandir le portail, l'écrase, fond l'image, rend caméra et musique. C'est la cinématique que Vincent
  a vue à la mort du boss de la tour. Le même gestionnaire sert d'autres zones.
- **La zone 4 n'a aucun emplacement libre** : 11 sous-zones, toutes occupées (`bestiaire.md`), à
  la différence de la zone 0 où le hub a pris la 86.
- **La défaite** : `Tick/WorldMapHud.cs:175-182` — tous tombés, le drapeau du monde `0x005de02c |= 2`
  lance le fondu de mort ; quand `s_Unknown_005de518` atteint la taille du groupe, le jeu passe à
  l'état 9 (`RunGameOverState_0051BB00`).
- **Ce qui existe côté LegacyPlus** : le hub (LP-009) et ses coutures (`EntreeSousZone`,
  `ListeDApparition`, `DecorNe`, `PasDuMembre`, `TransfertScripte`, `PointDEntree`, `LieuSauve`) ;
  l'écran de boutique et sa question Oui / Non (LP-010, LP-011) ; le chargement d'un modèle hors
  zone (`MarchandsHub`).

## Goals / Non-Goals

**Goals :** toute la mécanique de l'arène, validée sur un boss natif de la tour ; un directeur de
vagues assez général pour un boss rush ; zéro trace dans le scénario et la sauvegarde.

**Non-Goals :** faire venir un monstre d'une autre zone (le Grizzly : `add-legacy-plus-boss-graft`) ;
le butin des coffres (défini à part) ; un classement, un chronomètre, une récompense graduée par
palier.

## Decisions

### D1 — L'arène est 4/9 elle-même, en « mode arène », pas un clone numéroté

La zone 4 n'a pas d'emplacement vide. Agrandir sa table des sous-zones, c'est modifier une donnée du
jeu relue par des chemins qu'on ne maîtrise pas tous. On charge donc **4/9 sous son propre numéro**,
et un interrupteur à nous (`ModeArene`), levé seulement par le téléporteur du hub et baissé en
sortant, décide de tout le reste. *Écarté* : une entrée 11 ajoutée à la table de la zone 4 ; le hub
de la zone 0 (sa géométrie n'est pas une salle de boss).

Prix : tout ce que le jeu attache au couple (4, 9) vaut aussi pour l'arène. Le contenu étant retiré
(D2), il reste la musique, l'éclairage et la caméra — c'est ce qu'on veut —, et les drapeaux (D6).

### D2 — Vider la salle à la naissance, comme le hub vide la 82

En mode arène : les **formations** ne font naître personne (couture sur la naissance des monstres ou
sur la liste de formations de la sous-zone), et le **décor** ne garde que ce que l'arène emploie —
la scénette de sortie (sous-type 22), le sol, les murs. Le coffre d'origine, l'effet 56 et tout décor
scripté sont rendus au néant dès leur naissance, **sans décaler le rang** des entités gardées : c'est
la technique déjà éprouvée par le hub (LP-009, décision D3).

### D3 — Le directeur de l'arène et son programme

Un programme est une liste de **vagues** ; une vague, une liste de créatures (espèce, place, cap) et
un multiplicateur. Le directeur, appelé à chaque image en mode arène, fait naître la vague courante,
compte ses créatures vivantes, passe à la suivante quand elles sont toutes mortes, et déclenche la fin
après la dernière. Ce changement n'a qu'**un** programme : une vague, l'Enchanteresse seule, à sa
place d'origine. Le menu (D5) liste les programmes par leur nom.

*Pourquoi un programme et pas « un boss »* : Vincent veut plus tard des boss rush et des monstres à la
chaîne ; ce seront des programmes plus longs, sans toucher au directeur.

La naissance réutilise le chemin du jeu (celui des formations), pour que la créature ait son IA, sa
fiche, ses collisions et son statut de boss exactement comme d'origine. **À mesurer** (tâche 1) :
quelle fonction fait naître une formation, et ce qu'elle attend.

### D4 — Le palier : une couture sur la naissance du boss, et une sur ses coups

À la naissance d'une créature du programme, ses PV (courants et maximum) sont multipliés par le
palier. Ses **dégâts** sont multipliés au moment où ils sont infligés — multiplier la statistique
d'attaque ne multiplierait pas les dégâts, que la formule réduit par la défense. Aucun tirage
aléatoire (règle des tweaks).

*Mesuré (tâche 1.5)* : les deux coutures **existent déjà** — `PvALaNaissance` (LP-002) sur les PV
maximum et courants, des `ushort` (65 535 : tout boss × 10 tient), et `DegatsSubis` (LP-001) à
l'entrée d'`ApplyDamage`, un `short` à borner à 32 767. Le directeur s'y empile, comme les autres
tweaks, et ne multiplie que pour les créatures qu'il a fait naître (la naissance est marquée
pendant son appel à `SpawnEntityFormation`).

### D5 — Le menu : l'écran de boutique, rendu générique

L'écran de boutique a la liste, le curseur, le titre, la pagination, la question Oui / Non et la
lecture de la manette du bon joueur. Le menu de l'arène a besoin de **trois pas** : les modes, le
palier (une ligne « < 4 > » qu'on règle à gauche/droite), la confirmation. Plutôt que de copier
l'écran, on en tire un **écran de menu** (liste + actions par pas), dont la boutique devient un
usage. À proposer à Vincent au moment de l'écrire : le découpage exact des fichiers.

### D6 — Aucune trace : drapeaux et sauvegarde

- **Drapeaux** : le script de 4/9 est **neutralisé** en mode arène (il ne tourne pas, ou ses
  écritures de drapeau sont ignorées) ; la scénette, le portail et les coffres sont posés par le
  directeur, pas par le script. *À mesurer* (tâche 1) : comment le script de la sous-zone est lancé,
  et s'il existe un point unique pour le taire.
- **Coffres** : le numéro de coffre indexe un champ de bits « déjà ouvert » **persistant**
  (`DAT_005e96e0`, `Bestiaire/LISEZMOI.md`). Les deux coffres de l'arène prennent donc des numéros
  qu'aucun coffre du jeu n'emploie, et leur bit est **remis à zéro** à chaque entrée dans l'arène.
- **Sauvegarde** : même mécanisme que le hub (couture `LieuSauve`) : en mode arène, le lieu de
  reprise écrit est celui du hub. *À mesurer* : s'il existe un moyen de sauvegarder dans 4/9 ; sinon
  cette couture suffit comme filet.

### D7 — Aller et retour

- **Aller** : un transfert de zone du jeu, 0/86 → 4/9, le chemin du menu de mise au point
  (`CommitDebugWarp`) ou de l'opcode 17, avec un point d'entrée choisi par nous.
- **Retour, victoire** : le portail de la fin est un volume de déclenchement à nous au centre de la
  salle ; y entrer transfère vers 0/86, **centre du hub**, et baisse le mode arène.
- **Retour, défaite** : la couture sur le passage à l'état 9 (`WorldMapHud.cs:181`) : en mode arène,
  au lieu de l'état 9, chaque membre est ressuscité **avec tous ses PV** (Vincent, 2026-09-28), le
  transfert vers le hub est lancé et le message du panda est affiché à l'arrivée. Le fondu, qui
  précède, reste celui du jeu.
- **Le lieu d'origine** (la réception d'où l'on est entré dans le hub) reste retenu par LP-009 :
  l'arène ne le touche pas, et ressortir du hub mène toujours à la bonne réception.

### D8 — Le téléporteur du hub

*Mesuré (tâche 1.1, relevé et captures de Vincent, `Tests/11. Teleporteur`)* : le téléporteur du
Labyrinthe est en 5/9, section 2, **au centre** d'une salle à quatre piliers lumineux : le décor de
rang 6, le **modèle 6** du paquet de décor (cercle magique rouge et voile rose, semi-transparent), en
(−2500, 55, −100). C'est un **modèle de la zone 5** : le hub (zone 0) ne le charge pas. *Écartés
après essai* : les deux effets natifs de la 82 (une flamme), puis un pilier (modèle 1) et sa lueur.

**Greffe de décor**, sur le modèle des marchands : un décor fixe désigne sa texture par **une entrée
de la table de décodage `0x005e8f00`** (octet 2 du mot `+0x3c` du modèle, `DrawStaticModel`,
`Model3d.cs:758`) — comme un modèle de classe. On relit donc le répertoire de la zone 5, on en tire
le modèle (le dernier paquet de la liste `+0x24`, indice `record[7]` de l'enregistrement de 5/9), on
envoie la texture de leur entrée dans une place libre de la VRAM du hub, on écrit une entrée de table
à nous et on y fait pointer le mot `+0x3c` de nos copies ; on les dessine par `DrawStaticModel` dans
la couture `DessinSurImpression`, et le voile reçoit la respiration de couleur de l'original. *À
mesurer* : d'où la zone 5 charge la texture de cette entrée, et la place libre en VRAM (la bande
des marchands est prise). La même greffe servira au portail de l'arène et aux boss greffés.

La détection (« à son approche ») reprend la forme de `Parler` : membre debout, dans un rayon,
bouton d'action non requis.

## Risks / Trade-offs

- [Le couple (4, 9) déclenche un comportement caché ailleurs dans le jeu] → la tâche de mesure
  relève tous les tests `zone == 4 && sous-zone == 9` du legacy avant d'écrire.
- [Le script de 4/9 est lancé par plusieurs chemins et un seul est tu] → le journal `[ARENE]`
  (Debug) note chaque écriture de drapeau pendant le mode arène ; zéro attendu.
- [La naissance d'une formation hors de son déroulé normal oublie un état du boss (caméra de boss,
  musique de combat)] → on valide sur l'Enchanteresse, native, avant toute greffe.
- [Un membre ressuscité dans le hub garde un état de combat (poison, gel, statut de mort résiduel)]
  → la résurrection reprend celle du sort du jeu, et l'essai de défaite est une tâche à part.
- [Quitter pendant le mode arène] → rien n'est écrit dans la sauvegarde en arène ; la reprise est
  celle de la dernière sauvegarde, comme partout.

## La suite, décidée (Vincent, 2026-09-28)

Trois temps : l'arène validée avec l'**Enchanteresse** (ce changement) ; un premier boss **extérieur**,
le Grizzly (`add-legacy-plus-boss-graft`) ; puis **tous les boss, l'un après l'autre**, chacun un
programme de plus. Le mode ENCHANTERESSE **reste au menu** une fois le Grizzly arrivé.
