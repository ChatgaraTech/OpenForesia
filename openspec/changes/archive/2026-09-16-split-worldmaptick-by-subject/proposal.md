## Why

`Screens/State03/WorldMapTick.cs` fait **74 072 lignes** (3,1 Mo) — 1 041 méthodes, 122 champs
`static readonly`, 103 champs mutables. C'est **83 % du répertoire `State03/`** et, à lui seul, plus
du triple de ce que pesait `Blaze.cs` entier avant le découpage de 2026-08-19 (23 673 lignes).

Le fichier n'a pas dérivé : il a grossi **exactement comme prévu**. La règle de routage (4)
d'`ARCHITECTURE.md` — « sinon, ça reste chez l'appelant tant que le rôle n'est pas établi » — a fait
son travail pendant un an, et le rôle est maintenant établi partout. Ce qui n'était pas prévu, c'est
la proportion :

| | Lignes | Part |
|---|---|---|
| La boucle par image elle-même (`RunWorldMapTick`, `FUN_00516fc0`) | 645 | 1 % |
| Dessin du HUD + pas de simulation | 3 625 | 5 % |
| Le répartiteur `DispatchEntityHandlerCore` (switch de **426 cases**) | 1 036 | 1 % |
| **Tout ce que le répartiteur atteint** | **~68 000** | **92 %** |

Et dans ces 92 %, deux sujets font à eux seuls plus de la moitié du fichier : **les sorts**
(22 837 lignes, 123 fiches) et **l'IA des monstres** (15 934 lignes, 25 routines).

Le coût est le même que celui qui avait justifié le découpage de `Blaze.cs`, en pire : les
**130 bandeaux `// ====`** servent de sommaire, et **trois d'entre eux mentent déjà** —

| Bandeau | Annonce | Contenu réel |
|---|---|---|
| `L20940` « LE TIR » | les trois corps du jet du Goblin | le tir (563 l.), **plus** la machine d'éveil des entités (1 148 l.), **plus** l'IA de l'auberge (702 l.), **plus** les coffres |
| `L24443` « L'INTELLIGENCE DES ENTITÉS DE SCÈNE » | l'IA de la famille 0x0049xxxx | **la résolution de combat entière** (`ResolveMeleeHit`, `ResolveSpellHit`, `ApplyDamage`, les éléments, la portée — 1 322 l.) avant la moindre ligne d'IA |
| `L67638` « LES TROIS DERNIÈRES POSTURES » | trois gestionnaires de posture | 2 379 l. de **physique partagée** : mouvement, contact, ombre, volumes de déclenchement, **et la montée de niveau** |

Un sommaire en commentaire qui ment dans un fichier de 74 072 lignes n'est pas rattrapable par la
relecture. C'est la leçon déjà écrite dans `split-blaze-into-subsystems` : **il faut que la structure
du dépôt porte l'information à la place du commentaire.**

**La fenêtre est maintenant.** Aucune transcription n'est ouverte dans le fichier, l'outillage du
découpage précédent (`apply-split.ps1`, `verify-split.ps1`, `split-manifest.awk`) existe, est
paramétré et a déjà prouvé un découpage à l'octet près.

## What Changes

- **`WorldMapTick.cs` est découpé en 68 fichiers répartis dans 11 sous-répertoires de
  `Screens/State03/`.** Aucun fichier ne quitte `State03/` : la règle de routage (2)
  d'`ARCHITECTURE.md` s'applique toujours — un seul écran appelle tout cela.

- **Les 7 fichiers existants de `State03/` restent à la racine du répertoire.** Ils constituent le
  niveau « écran » ; les nouveaux répertoires sont le niveau « ce que l'écran atteint ».

- **La découpe est par sujet, pas par adresse.** Onze répertoires, chacun nommé par un rôle :
  `Tick/` (la boucle), `Dispatch/` (le carrefour adresse → méthode), `Combat/`, `Physics/`,
  `Entities/`, `Effects/`, `Items/`, `Spells/`, `Cutscene/`, `Ai/`, `Handlers/`.

- **Un fichier par créature dans `Ai/`, un fichier par école dans `Spells/`.** L'unité retenue est
  « ce qu'on ouvre exprès » : on ouvre le Grizzly pour travailler sur le Grizzly, on ouvre l'école
  du Sorcier pour travailler sur un sort. `Ai/` reçoit des sous-répertoires par zone
  (`Forest/`, `OldTower/`, `ImmortalCastle/`, `SealedCave/`), comme `Comportement.md` les indexe.

- **Aucune ligne de code n'est modifiée.** Ni renommage, ni refactorisation, ni correction en
  passant. Le découpage est une opération à l'octet : la somme des fichiers cibles doit redonner le
  fichier d'origine, ligne pour ligne, en dehors du seul préambule (`using`, `namespace`,
  déclaration de classe, accolades) et du bandeau d'en-tête propre à chaque fichier.

- **Quatre scripts codent le chemin `WorldMapTick.cs` en dur** et doivent suivre :
  `census-entity-handlers.sh`, `census-item-effects.sh`, `census-spells.sh`, `check-descriptors.sh`.
  Ce sont eux qui génèrent `GestionnairesEntites.md`, `ObjetsAtteignables.md` et
  `SortsAtteignables.md` — les trois tableaux de bord du dépôt. **S'ils cassent, ils ne crient pas :
  ils comptent zéro.** Leur sortie avant/après est donc une condition de recette, pas un
  à-côté.

- **`apply-split.ps1` gagne un paramètre `-Residual`.** Il écrit aujourd'hui le reliquat dans un
  `Blaze.cs` codé en dur ; il doit pouvoir l'écrire dans `Tick/WorldMapTick.cs`. Et son préambule,
  figé aux lignes 1..`PreambleEnd`, contient ici dix lignes propres à `FUN_00516fc0` qui ne doivent
  pas être recopiées dans 56 fichiers.

- **`DataSymbols.md` est régénéré.** Il porte « la carte des symboles reconstruits » — chaque
  symbole suffixé d'une VA, son écart au suivant, et **le fichier où il vit**. Le découpage invalide
  la troisième colonne pour ~220 symboles.

## Capabilities

### New Capabilities

Aucune. C'est un découpage pur : aucun comportement du jeu ne change, aucune exigence n'est créée ni
modifiée. `.openspec.yaml` porte `skip_specs: true`, comme `split-blaze-into-subsystems` avant lui.

### Modified Capabilities

Aucune.

## Impact

**Code déplacé** — 74 048 lignes de `OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs` vers
68 fichiers sous `Screens/State03/{Tick,Dispatch,Combat,Physics,Entities,Effects,Items,Spells,Cutscene,Ai,Handlers}/`.

**Code modifié** — aucun. Le seul risque de changement sémantique est l'**ordre d'initialisation des
champs statiques entre fichiers d'une même classe partielle**, le piège qui a mordu au découpage de
2026-08-19 (huit champs `= Arena(0x…)` rendant `null + offset`). Sept champs sont concernés ici :
`s_ClassBodyMetrics_0056d440`, `s_MenuLayout_0056a850`, `s_IconTables_0056aaa0`,
`s_ReachMarkerAtlas_0056aca0`, `s_ErrorMarkerAtlas_00564808`, `s_SpellCounts_00564600`,
`s_DeveloperNotice_00551814`.

**Outillage** — `OpenForesia.Tools/apply-split.ps1` (paramètre `-Residual`, préambule non contigu),
un nouveau manifeste et une nouvelle référence figée ; les quatre scripts ci-dessus.

**Documentation** — `ARCHITECTURE.md` (la carte des répertoires de `State03/`),
`Documentation/Reference/DataSymbols.md` (colonne fichier), `CLAUDE.md` (la sonde n'est plus « de
`WorldMapTick.cs` »), `REVERSE_STATUS.md` (les renvois `WorldMapTick.cs:NNNN` deviennent faux — ils
sont historiques et ne sont pas réécrits, mais le fait est consigné), `HISTORY.md`.

**Hors périmètre** — aucune reconstruction, aucune correction de fidélité, aucun renommage de
symbole, aucun déplacement vers `Engine/`. La promotion de `Spells/`, `Combat/` ou `Physics/` vers
`Engine/` (règle de routage 3) se posera le jour où un second écran les appellera ; ce n'est pas
aujourd'hui.
