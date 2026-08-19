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

## Les échecs attendus, au 2026-08-19

### `Aucune_allocation_propre_dans_l_etendue_d_une_arene` — 4 symboles

| Symbole | Adresse | Arène qui la couvre | Déclaré dans |
|---|---|---|---|
| `s_EntityList_005de558` | `0x005de558` | `ItemMenuArena` | `Engine/Resources.cs` |
| `s_ModelResultB_005f7b40` | `0x005f7b40` | `EntityArena` | `Screens/State03/WorldMap.cs` |
| `s_ModelResultA_005fa588` | `0x005fa588` | `EntityArena` | `Screens/State03/WorldMap.cs` |
| `s_CameraMatrix_006008e0` | `0x006008e0` | `EntityArena` | `Memory/GameMemory.cs` |

Constatés au découpage de `Blaze.cs`. Les trois derniers sont dans le voisinage que `FUN_00533940`
doit lire (`DAT_005f7bc0`, `DAT_005f7cc0`, `DAT_005fa508`) — **à traiter avant de reprendre la
priorité 2**.

### `Deux_symboles_a_la_meme_adresse_partagent_leur_memoire` — 1 adresse

`0x00566c70` porte deux allocations indépendantes :

```csharp
Screens/State00/MainMenu.cs   s_MenuRenderBlock_00566c70 = AllocZeroed(0x40);
Screens/State03/WorldMap.cs   s_ViewRect_00566c70        = AllocZeroed(0x10);
```

Le menu principal écrit dans l'une, la carte du monde lit dans l'autre. **Défaut dormant** : il
mordra quand la carte du monde affichera quelque chose.

Les deux autres adresses à double déclaration sont bénignes et passent : `0x005e9180` (deux vues de
la même arène, donc la mémoire est bien partagée) et `0x00562c9c` (deux constantes de même valeur,
aucune mémoire en jeu).

### `Une_taille_declaree_ne_deborde_pas_sur_le_voisin` — 2 recouvrements

**Trouvés par ces tests, pas connus avant.**

| Symbole | Déclare | Écart réel | Recouvrement |
|---|---|---|---|
| `s_MenuScreenBlock_00566c48` (`MainMenu.cs:269`) | `0x40` | `0x28` | **0x18** |
| `s_CardPathDescriptor_00604940` (`SaveData.cs:88`) | `0x100` | `0x4c` | **0xb4** |

Le premier : deux lignes voisines du même fichier, la taille `0x40` recopiée sans vérifier l'écart
jusqu'au voisin.

Le second est plus sérieux : le descripteur de chemin **avale entièrement** le compteur d'images
d'icône `s_CardIconFrameCount_0060498c`, et les deux vivent dans des fichiers différents.

## Pourquoi ils ne sont pas corrigés

**Corriger une taille d'allocation change le comportement.** Les changements qui ont produit ces
tests — `split-blaze-into-subsystems` et `extract-real-classes-from-partial` — s'interdisent
explicitement tout changement de comportement : ils déplacent et transforment des formes, et le
prouvent octet à octet ou corps par corps.

Chaque correction demande de retrouver la vraie taille dans `Blaze.c` ou dans le binaire, puis de
vérifier à l'écran. C'est un travail de reconstruction, pas de refactor.

## Quand un test passe au vert

Vérifier que c'est parce que le **défaut** a été corrigé, et non l'assertion affaiblie. Puis retirer
la ligne correspondante de ce fichier et consigner la correction dans `REVERSE_STATUS.md`.

Le cinquième test, `L_analyseur_releve_bien_la_disposition`, existe pour ça : si un refactor cassait
l'analyse des sources, les quatre autres passeraient sur un ensemble vide et ne prouveraient plus
rien.
