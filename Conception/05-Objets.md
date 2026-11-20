# Les objets

Sources : `Documentation/Reference/Bestiaire/LISEZMOI.md` (butin, coffres, fiche d'objet, éléments),
le compendium `OpenForesia.Tools/Html/bestiairev2.html` (les 397 objets, leurs pouvoirs traduits,
les conseils par classe), `Documentation/Etat/ObjetsAtteignables.md`.

## Le catalogue

397 objets en français : **136 armes, 47 armures, 10 boucliers, 120 accessoires, 84 consommables et
objets de quête**. 132 portent un **pouvoir**. Chaque objet a un nom, une description, une vignette
24×24, la liste des classes qui peuvent l'équiper et ses bonus.

## L'équipement (établi)

**Cinq emplacements** : arme, armure, bouclier, deux accessoires. Seuls le Guerrier et le Nain ont
droit au bouclier.

Un objet d'équipement donne :
- des bonus **aux valeurs dérivées** (ATT, DEF, MATT, MDEF, et les forces et résistances
  d'altération) — c'est ce qui compte en combat ;
- des bonus **aux caractéristiques** — qui **ne font pas monter** les valeurs dérivées
  ([02-Personnages](02-Personnages.md)) ;
- des **affinités élémentaires** — prises en compte **seulement dans les deux emplacements
  d'accessoire** ;
- pour une arme, **les familles de monstres qu'elle blesse pleinement** (sinon : le quart).

## Les pouvoirs d'objet (établi)

Un pouvoir est un **petit script** attaché à l'objet, déclenché par un **événement** : coup porté,
coup reçu, sort lancé, sort reçu, chaque appariement de combat… Il peut tirer une chance, tester une
condition (PV, PM, monstre visé), puis appliquer un ou plusieurs des 48 effets du jeu : bonus
temporaire, soin, vol de PV ou de PM, altération infligée ou levée, immunité, dégâts multipliés,
annulation d'un sort…

Exemples réels, tirés du compendium :
- une arme qui paralyse la cible une fois sur dix, à coup sûr sauf immunité ;
- un bâton qui, tant qu'il reste des PM, ajoute 30 de MATT et coûte 3 PM par cible touchée ;
- un anneau qui donne +80 en MATT et MDEF et rend 3 PM… mais **maudit** son porteur, qui ne peut plus
  attaquer ;
- des gants qui lèvent la restriction de famille de monstres ;
- un masque qui donne +100 partout, et retire au porteur un vingtième de ses PV max, jusqu'à zéro.

Le compendium donne la traduction en clair des 132 pouvoirs.

## Le butin (établi)

À la mort d'un monstre :

1. **Tombe-t-il un objet ou seulement de l'argent ?** — la valeur de butin de l'espèce + la moitié
   de la **chance** du tueur, contre un tirage sur 1000.
2. **Lequel ?** — chaque espèce pointe une **table de huit rangs**. Le rang est tiré sur une plage
   qui vaut `56 + (niveau du tueur − rang du monstre)`, l'écart étant borné entre 0 et 44 :

   | rang | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
   |---|---|---|---|---|---|---|---|---|
   | à plage 100 | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
   | atteignable dès un écart de | 0 | 0 | 0 | 2 | 15 | 26 | 35 | 41 |

   À écart nul, seuls les rangs 0 à 2 existent. **Être sous-niveau ne coûte rien** ; seule l'avance
   de niveau ouvre le haut de la table.
3. **Dans quel état ?** — un second tirage sur la chance décide si l'objet tombe « propre » ou
   marqué (60 % à chance 0, plafonné à 95 %).

Trois questions, trois maîtres : **quel** objet, l'écart de niveau ; **si** il tombe, la chance et
l'espèce ; **dans quel état**, la chance.

## Les coffres (établi)

Un coffre tire dans **la même table** que les monstres, avec la plage fixe de 100, mais à la rangée
que porte son placement. **112 objets n'existent que dans des coffres.** Chaque coffre a un numéro,
mémorisé une fois ouvert. Un coffre peut aussi, au lieu de donner un objet, déclencher un effet ou
afficher un texte.

## L'inventaire et le hub

- **22 emplacements** d'objet (**établi**), et un **dépôt** à l'auberge, enregistré avec la fiche
  (`Documentation/Reference/FormatSauvegarde.md`).
- Le menu des objets a trois sous-écrans : l'inventaire, l'utilisation, et une « Enchère » dont le
  fonctionnement exact est **à établir**.
- Le portage actuel a ajouté, en option, une boutique et des marchands au hub (LP-008, LP-010,
  LP-011, voir [07-Heritage-PS1](07-Heritage-PS1.md)) ; qu'il n'y ait aucune boutique dans le jeu de 1998 est une **lecture**.

> **Pour un jeu neuf.** Le système de pouvoirs « événement → condition → effet » est la meilleure
> idée du jeu à reprendre telle quelle : c'est exactement un système de *Gameplay Effects* /
> *Gameplay Abilities* d'Unreal. Le butin à trois questions est solide aussi. À revoir : les bonus
> de caractéristiques sans effet sur le combat, et les affinités élémentaires ignorées hors
> accessoires — deux règles que le joueur ne peut pas deviner.
