# Le combat

Sources : `OpenForesia/src/Blaze/Screens/State03/Combat/HitResolution.cs` (le coup au corps à corps
`ResolveMeleeHit_00539B80`, le coup magique `ResolveSpellHit_0053A2B0`, l'échange élémentaire),
`Combat/StatusEffects.cs`, `Documentation/Reference/Bestiaire/LISEZMOI.md` (les éléments),
`Documentation/Ennemies/Comportement.md` (l'intelligence des ennemis).

Tout est en temps réel : un coup « part » quand l'animation de l'attaquant atteint l'image où il
porte, et la résolution ci-dessous s'applique à chaque victime sous le gabarit de l'arme à cet
instant.

## Le coup au corps à corps (établi)

```
attaque  = ATT + bruit(écart)             borné à 0
défense  = DEF de la victime + bruit       borné à 0
critique : rand() % 100 < chance de critique (+5 si l'assaillant est sous 5 % de ses PV)
           → la défense est divisée par 2
dégâts   = (attaque − défense) + échange élémentaire
si l'arme ne « mord » pas sur la famille de la victime : dégâts ÷ 4
dégâts   = max(0, dégâts) + ATT/6 − DEF/32
dégâts   = max(1, dégâts)                   — personne ne fait jamais zéro
```

- **Le bruit** : chaque combattant a une valeur d'écart ; attaque et défense varient d'un quart de
  cet écart, centré.
- **Les familles de monstres** : chaque monstre porte des drapeaux de famille, chaque arme la liste
  des familles qu'elle blesse pleinement. Hors liste, le quart. Les gants « sacrés » ou « des runes »
  lèvent cette restriction (un pouvoir d'objet).
- **Côté joueur uniquement** : son, expérience, compte des victimes, effet visuel élémentaire,
  bandeau de nom.
- Un monstre frappé qui survit **retient son agresseur**, et peut lancer un script de réaction.

## Le coup magique (établi)

Même charpente, avec trois différences :

```
attaque  = MATT / diviseur du sort + bruit     (le diviseur est propre à chaque fiche de sort)
défense  = MDEF de la victime + bruit
jet de sauvegarde de la victime : s'il est RATÉ, la défense est divisée par 2
dégâts   = max(0, attaque − défense) + échange élémentaire
         + montant de la fiche du sort (sauf si la victime est déjà de l'élément du sort)
         − MDEF / 32
dégâts   = max(1, dégâts)
```

- **L'élément du sort écrase les autres** : avant le calcul, le montant de la fiche s'ajoute à
  l'élément du sort chez le lanceur, et toutes les autres affinités sont **mises à zéro des deux
  côtés**, sauf ce couple. Un sort de feu ne frappe qu'en feu.
- **Le montant de la fiche compte deux fois** : dans l'affinité du lanceur, puis en bonus final —
  sauf contre une victime de cet élément.
- **La Puissance magique apportée par un objet n'entre pas** dans la MATT (voir
  [02-Personnages](02-Personnages.md)).

## Les huit éléments

Rangés en **quatre couples contraires** : **Feu / Eau**, **Terre / Air**, **Lumière / Ténèbres**,
**Sacré / Mal**. Chaque combattant porte une valeur dans chacune des huit cases — les monstres dans
leur fiche (214 fiches sur 281 en ont au moins une), les personnages par l'équipement.

**L'échange élémentaire** (établi) :
1. dans chaque couple, les deux valeurs **s'annulent** jusqu'à ce qu'il n'en reste qu'une (30 de feu
   et 10 d'eau donnent 20 de feu) — chez l'assaillant comme chez la victime ;
2. puis, élément par élément de l'assaillant :
   - la victime porte **le même** → retranché (elle y résiste) ;
   - la victime porte **le contraire** → ajouté **en entier** ;
   - la victime ne porte **ni l'un ni l'autre** → ajouté **au quart**.

**Le penchant** : l'élément dominant d'un personnage gagne la moitié de sa valeur au moment du
combat, et son contraire en perd la moitié.

## Les altérations d'état (établi, sauf mention)

Un seul mot de bits par combattant, chaque bit une altération avec sa durée :

| altération | effet en jeu |
|---|---|
| **poison** | perte de PV régulière |
| **ensorcellement** | −3 % des PM max toutes les 30 images, plus de régénération de PM |
| **paralysie** | immobilisé |
| **confusion** | se déplace au hasard, ne peut plus lancer de sorts |
| **malédiction** | ne peut plus attaquer |
| **mutisme** | ne peut plus lancer de sorts (les objets restent permis) |
| **sommeil** | endormi |
| **étourdissement** | ne peut plus agir un moment (nom **déduit**, l'effet est lu) |
| **pétrification** | figé, perd 5 % de ses PV |
| **ralentissement** | ralenti |

Une **dissipation** retire d'un coup tous les bonus et malus de caractéristiques. Les altérations se
lancent à la **Force d'altération** (I pour les physiques, II pour les mentales) contre la
**Résistance** de même famille ; un objet peut porter une résistance « au maximum », qui vaut
immunité.

## L'expérience (établi pour le coup porté)

Chaque coup porté par un membre du groupe rapporte :

```
expérience = (niveau de la victime − niveau de l'assaillant) / 32 + 1
```

Le « niveau » d'un monstre est son **rang**, de 1 à 320 dans les fiches (le dragon de la montagne
est à 306, le boss final à 320). L'expérience récompense donc surtout de frapper plus fort que soi ;
ce qu'un coup rapporte contre beaucoup plus faible (un écart négatif) reste à vérifier dans le code. Des **cristaux d'expérience** tombent aussi
(mécanisme exact : à établir).

## L'intelligence des ennemis

**Établi** (`Documentation/Ennemies/Comportement.md`) : 112 routines d'intelligence distinctes, mais
**un seul squelette** en trois temps, appelé à chaque image :

1. **décider** — lire la situation : hardiesse, distance et cap vers la cible, vitesse, traits ;
2. **appliquer** — la posture courante : attendre, se tourner, approcher, errer, tenir à distance,
   frapper, reculer, esquiver ;
3. **replanifier** — à la fin d'un geste ou sur un coup reçu (le coup reçu interrompt et remet en
   attente).

Ce qui distingue deux monstres tient en **quelques constantes et une ou deux formes de geste** : la
portée de détection et la tolérance en hauteur, l'image du geste où le coup porte, le nombre de
postures, l'angle de virage (souvent fonction de la hardiesse), la présence d'un bond, d'un recul,
d'une esquive, d'un tir à distance. Les volants partagent une intégration du mouvement à part
(battement d'ailes).

Les formes rencontrées : la classique à trois plages de distance (loin : approcher ; moyen : tenir ;
près : frapper), la courte à deux plages, la bête à deux gestes, la complète avec recul et esquive.

> **Pour un jeu neuf.** Ce squelette se transpose directement en *Behavior Tree* d'Unreal — un
> sélecteur sur la distance, des tâches pour chaque posture, une interruption sur coup reçu — et les
> constantes en paramètres par espèce. Les formules de dégâts tiennent dans une fonction de quelques
> lignes ; ce qui demandera un vrai travail, c'est de remplacer le « coup qui porte à l'image N » par
> des fenêtres de collision d'animation, puis de rééquilibrer à la main.

## À établir

- le détail du jet de sauvegarde magique (formule complète dans le code, pas encore résumée ici) ;
- la durée de chaque altération et sa formule de réussite, cas par cas ;
- le sens précis des familles de monstres (combien, lesquelles) : les drapeaux sont lus, pas nommés.
