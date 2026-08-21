# OpenForesia.Tests — les tests de disposition mémoire

> **Trois de ces cinq tests échouent, et c'est voulu.**
> Ne les « réparez » pas en modifiant les tests. Ils échouent parce que le dépôt porte des défauts
> réels, constatés et volontairement non corrigés.

## Ce qu'ils testent, et ce qu'ils ne testent pas

Ils vérifient la **carte mémoire** : quel symbole vit à quelle adresse, qui recouvre qui, qui
s'alloue sa propre mémoire alors qu'une arène couvre déjà son adresse.

Ils ne vérifient **aucun comportement**. L'oracle de la logique transcrite reste l'écran, via
`OpenForesia.Tools/playtest.ps1` — voir `test-protocol.md`.

## Pourquoi ils lisent les sources plutôt que l'exécution

Deux raisons, dans cet ordre :

1. **Le défaut visé est un défaut de déclaration.** Deux `NativeMemory.Alloc` pour une seule adresse
   du binaire : c'est entièrement visible dans le texte, sans rien exécuter.
2. **Réfléchir sur les statiques de `Blaze` les initialiserait tous**, dont `AllocLowZeroed`, qui
   dispute une fenêtre mémoire basse et **échoue par intermittence** (voir `test-protocol.md`,
   « No free block … below 0x01000000 »). Des tests de disposition qui échouent une fois sur cinq
   pour une raison sans rapport ne valent rien.

La limite est assumée : ils prouvent ce que le code **déclare**, pas ce qu'il alloue à l'exécution.
Pour cette classe de défaut, c'est la même chose.

## Pourquoi le défaut d'aliasing mérite quatre assertions

Il a été corrigé **seize fois** dans ce dépôt. Il naît toujours du même geste : séparer ce que le
binaire garde ensemble. Deux symboles voisins d'adresse déclarés séparément ne partagent plus leur
mémoire ; une écriture faite par un bout n'est jamais lue par l'autre. **Aucune erreur, aucun
plantage : une valeur qui reste à zéro.**

Les seize ont tous été trouvés *après coup*, en regardant l'écran. Ces tests les prennent avant, en
une seconde.

## Les échecs attendus : IL N'Y EN A PLUS (2026-08-21)

**Les cinq tests passent.** Les défauts que ce fichier listait comme « attendus » depuis le
2026-08-19 ont été corrigés le 2026-08-21, dans le changement dédié aux défauts d'aliasing. Le
détail de chacun est dans `REVERSE_STATUS.md`, section du même jour ; en résumé :

| défaut | correction |
|---|---|
| `0x00566c70` alloué deux fois (`s_MenuRenderBlock` / `s_ViewRect`) | une seule allocation, dans `GameMemory`, taille 0x14 déduite des écritures et du voisin |
| `s_MenuScreenBlock_00566c48` déclarait 0x40 pour un écart de 0x28 | ramené à 0x28 |
| `s_CardPathDescriptor_00604940` déclarait 0x100 pour un écart de 0x20 | ramené à 0x20 |
| `s_EntityList_005de558` dans `ItemMenuArena` | l'arène passe de 0x460 à 0x410 — le code contredisait son propre commentaire |
| `s_CameraMatrix_006008e0`, `s_ModelResultA_005fa588`, `s_ModelResultB_005f7b40` dans `EntityArena` | devenus des **vues** dans l'arène |

Deux défauts supplémentaires, que **ces tests ne voyaient pas**, ont été trouvés et corrigés au
passage. Ils valent plus que les cinq précédents parce qu'ils disent où l'outil est aveugle :

- **`s_SoundBankHeader_005e8520`** était une allocation séparée alors que
  `0x005e8520 == 0x005e8340 + 0x1e0`, la queue du répertoire de sons. Invisible aux tests parce que
  `s_SoundDirectory` n'avait pas de suffixe VA (voir « L'angle mort de l'analyseur » plus bas).
- **`DAT_006008d0/d4/d8`**, le décalage monde appliqué avant projection. `WorldMap.cs` l'écrivait
  **à travers `EntityArena`**, `Model3d.cs` le lisait dans un **champ séparé** : les deux mémoires
  ne se rencontraient jamais. Invisible aux tests parce que le test « allocation propre dans une
  arène » ne considère que les symboles qui s'allouent (`SymbolKind.OwnAllocation`) — **un simple
  champ `int` à une adresse couverte par une arène ne déclenche rien.**

## Le second angle mort, à traiter

Le test `Aucune_allocation_propre_dans_l_etendue_d_une_arene` ne regarde que les allocations. Un
champ de valeur (`int`, `byte`, …) dont l'adresse tombe dans une arène est tout aussi cassé — c'est
exactement `DAT_006008d0` — et passe. **Étendre l'assertion aux `SymbolKind.PlainField` ferait
remonter cette famille entière**, et c'est probablement le meilleur rapport travail/valeur que ces
tests puissent encore rendre.

À faire avec précaution : la première exécution signalera vraisemblablement plusieurs cas, dont
certains légitimes (un champ peut modéliser une adresse d'arène *volontairement*, si aucun site ne
l'atteint par l'arène). Vérifier chacun avant de corriger.

## Pourquoi ils sont restés rouges deux jours, et ce que ça valide

**Corriger une taille d'allocation change le comportement.** Les changements qui ont produit ces
tests — `split-blaze-into-subsystems` et `extract-real-classes-from-partial` — s'interdisent
explicitement tout changement de comportement : ils déplacent et transforment des formes, et le
prouvent octet à octet ou corps par corps. Les défauts ont donc **attendu leur propre changement**,
au lieu d'être corrigés en marge d'un refactor où rien n'aurait pu les vérifier.

Ça s'est révélé payant : la correction a demandé de retrouver chaque étendue réelle dans `Blaze.c`
ou dans la carte des symboles, et **deux des huit corrections ont changé une taille sur la foi du
binaire, contre ce que le code affirmait** (`ItemMenuArena` 0x460 → 0x410, alors que son propre
commentaire raisonnait 0x410 ; `s_CardPathDescriptor` 0x100 → 0x20). C'est de la reconstruction,
pas du refactor, et ça se vérifie à l'écran — pas au compteur de primitives.

## Quand un test passe au vert

Vérifier que c'est parce que le **défaut** a été corrigé, et non l'assertion affaiblie. Puis mettre
à jour ce fichier et consigner la correction dans `REVERSE_STATUS.md`.

Le cinquième test, `L_analyseur_releve_bien_la_disposition`, existe pour ça : si un refactor cassait
l'analyse des sources, les quatre autres passeraient sur un ensemble vide et ne prouveraient plus
rien. **Maintenant que les quatre autres sont verts, il est la seule chose qui distingue « aucun
défaut » de « aucune donnée ».** Ne jamais le désactiver.

---
## L'angle mort de l'analyseur — trouvé le 2026-08-21, à connaître

Ces tests ne voient un symbole **que si son nom C# porte son adresse** (`s_Role_005e8340`). C'est
leur seule source d'adresse : `MemoryLayout.cs` lit les sources et extrait le suffixe VA du nom.

**Un symbole `.data` nommé sans son adresse est donc totalement invisible pour eux**, et pas
seulement absent d'un relevé : il ne peut ni entrer en collision, ni déborder, ni être signalé.
C'est exactement ce qui s'est produit avec `s_SoundDirectory` (`DAT_005e8340`, 0x268 octets) :
nommé sans suffixe, il a masqué un recouvrement réel avec `s_SoundBankHeader_005e8520`
(`Screens/State03/WorldMap.cs`), alors que `0x005e8340 + 0x1e0 == 0x005e8520` et que les tailles
se corroborent au bit près. Les quatre tests étaient verts sur ce point, et le défaut existait.

Vérifié contre la source de vérité, pas déduit — `Blaze.c` 74824 charge `&DAT_005e8340` sur 0x1e0
octets, et 63353 charge `&DAT_005e8520` sur 0x88 : deux moitiés d'un seul tableau de 77 entrées,
que `PlaySoundEffect` indexe depuis la première.

**Conséquence pratique** : un `.data` sans suffixe VA n'est pas seulement mal nommé, il est
**hors de portée de l'outillage**. Le nom n'est pas décoratif ici, c'est l'index.

**Le renommage a été fait le 2026-08-21**, dans le changement des défauts d'aliasing — et pas
avant, pour une raison précise : l'oracle `compare-bodies.ps1` compare du texte normalisé, où un
renommage est indiscernable d'un changement de logique. Il aurait sonné à **chaque** exécution
future, sauf à refiger la référence ; or ce changement-ci refigeait de toute façon.

Quatre symboles renommés, adresses pinées avant d'y toucher :

| avant | après | ancre |
|---|---|---|
| `s_SoundDirectory` | `s_SoundDirectory_005e8340` | `Blaze.c` 74824 |
| `s_BaseSoundBank` | `s_BaseSoundBank_00604920` | `Blaze.c` 61791 (790000 octets) |
| `s_LevelSoundBank` | `s_LevelSoundBank_00600900` | `Blaze.c` 61792 (260000 octets) |
| `s_GameStateId` | `s_GameStateId_005de510` | commentaire d'origine |

**L'audit complet reste à faire.** Ces quatre-là sont ceux dont on avait *établi* que le nom
mentait. Un balayage systématique des déclarations ancrées `// Ghidra: DAT_…` mais nommées sans
suffixe VA en trouverait probablement d'autres — et chacune est, par construction, invisible à ces
tests. C'est le prolongement naturel de « étendre l'assertion aux champs de valeur » ci-dessus :
les deux élargissent ce que l'outil *peut voir*, ce qui vaut mieux que de corriger ce qu'il voit
déjà.
