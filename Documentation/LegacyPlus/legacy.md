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
| LP-001 | Dégâts des monstres, multipliés | `[Difficulte] DegatsMonstres` | `DegatsSubis`, entrée d'`ApplyDamage_00539A20` | validé en jeu (2026-09-27) — `src/LegacyPlus/Difficulte.cs` |
| LP-002 | PV des monstres, multipliés | `[Difficulte] PvMonstres` | `PvALaNaissance`, `ResetEntityStats` (`FUN_005389e0`) | validé en jeu (2026-09-27) — `src/LegacyPlus/Difficulte.cs` |
| LP-003 | Montant des sorts du groupe × (1 + coef × MAG / 100) | `[Magie] ScalingMagique` | `MontantDeSort`, ajout du montant dans `ResolveSpellHit_0053A2B0` | validé en jeu (2026-09-27) — `src/LegacyPlus/ScalingMagique.cs` |
| LP-005 | Ramassage automatique des pièces et des cristaux d'expérience (pas les coffres) | `[Butin] RamassageAuto` | `ButinAtteint`, l'entité que la collision désigne dans `PartyMemberStep.cs` | validé en jeu (2026-09-27) — `src/LegacyPlus/RamassageAuto.cs` |
| LP-006 | Se déplacer en lançant un sort, à une part de la vitesse de marche | `[Magie] DeplacementEnIncantation` | `VitesseEnIncantation`, le zéro de vitesse des postures d'incantation dans `ApplyGroundMotion_0051A020` | validé en jeu (2026-09-27) — `src/LegacyPlus/DeplacementEnIncantation.cs` |
| LP-004 | Frames d'invulnérabilité des ennemis tenues par attaquant | `[Combat] InvulnerabiliteEnnemis` | `VictimeExclue` (bit `0x40`), `VictimeProtegee` (`+0x96`), `CoupPorte` dans les deux balayages (`FUN_00519940`, `FUN_00519af0`), `ImageSuivante` en tête du pas de simulation (boucle par image de `RunWorldMapTick`) | validé en jeu (2026-09-27) — `src/LegacyPlus/VerrouParAttaquant.cs` |
| LP-008 | La boutique de l'auberge : l'armoire de la carte 82 vend le catalogue de `Boutique.ini` | `Boutique.ini` : `[Boutique] Actif`, `[Catalogue]` | `PasDuMembre` (l'ouverture), `DessinSurImpression` (l'écran) | validé en jeu (2026-09-27) — `src/LegacyPlus/Boutique*.cs`, `CatalogueObjets.cs` |
| — | Vitesse des monstres | — | — | **abandonné** par Vincent le 2026-09-27 — les vitesses sont écrites en dur dans chacun des 166 gestionnaires de comportement |
| LP-007 | Rencontres aléatoires : cadence, chevauchement pendant un combat, plusieurs formations | `[Rencontres] Frequence`, `MonstresMax`, `Formations` | `Rencontre` (l'appel de `TrySpawnRandomEncounter`), `RencontreEnAttente` et `RencontreBloquee` (ses deux gardes), `DelaiRencontre` (les deux rechargements du minuteur) | validé en jeu (2026-09-27) — `src/LegacyPlus/Rencontres.cs` |

## LP-008, la boutique de l'auberge (validée en jeu le 2026-09-27)

Demande de Vincent (2026-09-27) : une boutique dans l'auberge — quête 0, carte 82 « Le coffre », à
gauche du coffre, près de l'armoire — qui vend des équipements. Catalogue et prix :
`Boutique.ini` (modèle : `OpenForesia.Installer/Reglages/Boutique.ini`, généré par
`node OpenForesia.Tools/generer-boutique.mjs`, puis retouché à la main).

**Décidé** : le joueur qui ouvre la boutique paie avec sa bourse (`+0x26c`) et reçoit l'objet ;
l'objet est remis comme par un coffre (`GrantChestContents_00530F20`) ; **pas de personnage** — au vu
de l'étape 1, le point de vente est **l'armoire** de la carte 82 : on s'en approche et on appuie sur
le bouton d'action.

**Étape 1, ce qui est établi :**
- **L'or ne sert à rien dans le jeu de 1998** : la bourse se remplit et s'affiche, rien ne la dépense.
- **Aucun prix dans les données** : les fiches d'objets n'en portent pas. Le catalogue les déduit de
  la puissance (ATT `+0x30`, DEF `+0x32`, bonus `+0x20`), de la zone d'apparition et de l'or
  gagné par monstre dans cette zone (`(rang + 9) / 10` par pièce, 1,5 pièce en moyenne).
  272 équipements obtenables sont en vente ; 116 introuvables en jeu sont désactivés.
- **Les modèles 3D sont chargés par carte** : `LoadWorldMapArea` charge le répertoire de la quête,
  puis `LoadWorldMapSubArea` recharge par-dessus, **pour chaque carte**, son bloc propre — espèces
  (`+0x4c`), modèles (`+0x98`), maillages, **textures** décodées dans des emplacements numérotés
  depuis 0. La carte 82 n'a **aucun** personnage ; le PNJ de l'auberge (modèle « ｵﾔ… », 1 PV,
  sous-type 0 : `EntityHandler_00486EE0`, qui lance son script quand un membre le voit) vit dans
  les blocs des cartes **81, 83, 84 et 85**. L'amener en 82 demande de charger le bloc d'une autre
  carte, et de placer ses textures dans des emplacements libres sans écraser celles de la 82.
- **Le numéro d'objet n'est pas global** : chaque carte a son catalogue d'une quarantaine d'objets,
  et `GrantChestContents` reçoit un indice dans **celui de la carte courante**. Vendre n'importe
  quel objet demandera de remettre la **fiche brute** (colonne `octets` d'`objets.csv`), comme
  le fait déjà l'éditeur de sauvegarde.
- **Position** : `[Diagnostic] TracePosition = oui` écrit la position du joueur 1 à chaque arrêt
  d'une seconde (`legacyplus-position.log`). Le coffre de la carte 82 est vers x = −556, z = 1000.
- **L'armoire** (relevé de Vincent, 2026-09-27) : debout devant elle, face à elle, le joueur 1 est
  à x = −1285, y = 400, z = 900, cap 3072 (il regarde vers les x décroissants). Dans cette direction,
  à 87 unités, `decor.csv` place un décor ordinaire (entrée 7 de la carte 82, gestionnaire
  `0x004734b0`) à **(−1372, 400, 900)** : l'armoire. Déclenchement proposé : à moins de ~120 unités
  de ce point, face à lui à 45° près, bouton d'action.
- **À vérifier à l'étape 2** : le relevé, accroché à la couture `ButinAtteint`, n'a écrit aucune ligne
  périodique ; cette partie du pas d'un membre n'est peut-être pas atteinte à chaque image. La
  détection de l'armoire devra s'accrocher à un point atteint à coup sûr.

**Étape 2, écrite (à tester)** — change `openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop` : une couture
`PasDuMembre` à l'entrée de `TickPartyMember` ; `Boutique.cs` ouvre une boîte du jeu (`BindMenuLabels`,
mise en scène d'un coffre, rappel `RestorePartyReactions_00530ED0`) quand un membre debout, à moins de
120 unités de l'armoire et face à elle à 45° près, appuie sur son bouton d'action ; l'appui est
consommé. Le programme de texte reprend les codes d'`AnnounceItem` (`labels + 0x2a8` / `+ 0x2ac`), et
ses octets sont écrits dans `legacyplus-boutique.log` pour vérification.

## Préconisations en attente

### La limite de rendu : 2 500 primitives par image (constatée avec LP-007, le 2026-09-27)

**Le constat.** Avec beaucoup de monstres à l'écran, la carte se dégrade : des morceaux de décor
manquent, et changent d'une image à l'autre. La cause est un budget du moteur de 1998, hérité de la
PlayStation : `FrameBegin` (`Screens/State00/MainMenu.cs`) pose `s_Unknown_005e9818 = 0x9c4`, soit
**2 500 primitives par image**, et toutes les boucles de dessin (`Engine/Model3d.cs`, 8 boucles ;
`Engine/Primitives2d.cs`) s'arrêtent net quand il tombe à zéro. Ce qui vient ensuite n'est pas
dessiné. Le jeu d'origine n'a jamais qu'une formation à la fois et reste sous le budget.

**Ce qui a été fait.** Rien sur le rendu, à la demande de Vincent. LP-007 est plafonné à **12
entités de scène vivantes** (`Rencontres.LimiteRendu`) : au-delà, le tweak ne fait plus naître de
rencontre ni de formation supplémentaire.

**La préconisation, pour le jour où l'on veut plus de monstres.** Relever la limite est un écart
legacy ++ (une contrainte matérielle de 1998, entrée `LB-NNN`), en trois pièces, aucune n'ayant de
marge aujourd'hui :

1. **le budget** `0x9c4` dans `FrameBegin` — une ligne ;
2. **les deux tampons de primitives**, 112 Ko chacun (`MainMem + 0x1c8000` et `+ 0x1e4000`), à
   **déplacer** dans une allocation plus grande : l'arène `MainMem` est pleine (la DLL d'affichage
   occupe `+0x200000`), et la nouvelle doit rester **sous 16 Mo** (liste d'affichage adressée sur
   24 bits, voir `AllocLowZeroed` dans `Platform/Winsys3d.cs`) ;
3. **l'arène de travail de la DLL d'affichage** (`src/Libraries/winsys3d/CheminAffichage.cs`,
   2 × 256 Ko à `MainMem + 0x200000`), qui traduit ces primitives et déborderait à son tour.

**Première étape recommandée : mesurer** — combien de primitives consomment le décor et un monstre
typique par zone, et combien d'octets l'arène de travail utilise par primitive —, pour choisir le
nouveau budget (×2 ou ×4) et les tailles en connaissance de cause. Ensuite, un change OpenSpec à
part, sur le chemin d'affichage.
