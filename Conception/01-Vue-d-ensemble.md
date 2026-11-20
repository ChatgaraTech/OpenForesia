# Le jeu en une page

## Le genre

Un **action-RPG en 3D, temps réel, jouable à quatre** sur un même écran (PlayStation et PC, 1998).
Pas de combat au tour par tour : on se déplace, on frappe, on lance des sorts en direct, et les
monstres font de même. La caméra suit le groupe dans des cartes en 3D.

## La boucle de jeu

```
   ┌────────────────────────────────────────────────────────────────────┐
   │  Le hub (l'auberge) : se soigner, sauvegarder, gérer l'inventaire  │
   └──────────────┬─────────────────────────────────────────────────────┘
                  │  choisir une destination sur la carte du monde
                  ▼
   ┌────────────────────────────────────────────────────────────────────┐
   │  Une zone (mine, forêt, tour…) = jusqu'à 16 cartes (sous-zones)    │
   │  explorer · combattre · ouvrir les coffres · résoudre les énigmes   │
   │  battre le boss                                                     │
   └──────────────┬─────────────────────────────────────────────────────┘
                  │  la progression débloque de nouvelles destinations
                  ▼
              retour au hub, destination suivante
```

**Établi** :
- l'écran de destination ne laisse le curseur s'arrêter que sur les destinations débloquées, et le
  déblocage tient dans **un seul octet de progression** de la sauvegarde
  (`Screens/State02/Destination.cs`) ;
- une zone compte **au plus seize sous-zones** (l'auberge mise à part), et c'est la sous-zone, pas la zone, qui charge
  monstres, décor et comportements (`Documentation/Ennemies/Comportement.md`).

## Les piliers

1. **Le groupe de quatre.** Quatre personnages, quatre joueurs possibles. Huit classes, très
   différenciées : deux de mêlée pure, trois hybrides, trois lanceurs de sorts
   ([02-Personnages](02-Personnages.md)).
2. **La progression lente et aléatoire, mais bornée.** À chaque niveau, chaque caractéristique a une
   *probabilité* de monter d'un petit montant ; le plafond de chacune est fixé **une fois pour
   toutes à la création**. Les choix de création pèsent toute la partie.
3. **La magie par combinaison de boutons.** On ne choisit pas un sort dans un menu en combat : on
   tient un bouton et on tape une séquence (△ ✕ ○ □) — jusqu'à treize coups pour les plus gros
   sorts ([04-Magie](04-Magie.md)).
4. **Huit éléments en quatre couples contraires** (Feu/Eau, Terre/Air, Lumière/Ténèbres, Sacré/Mal),
   qui jouent partout : dégâts, résistances, croissance des caractéristiques, objets
   ([03-Combat](03-Combat.md)).
5. **Le butin comme moteur.** 397 objets, des pouvoirs déclenchés par les coups donnés ou reçus, des
   tirages où l'écart de niveau ouvre le haut de la table et où la chance fait tomber plus souvent
   ([05-Objets](05-Objets.md)).

## Les chiffres du contenu

| | |
|---|---|
| classes jouables | 8 (chacune a un nom masculin et un nom féminin) |
| zones d'aventure | 10, plus le hub |
| fiches de monstre | 281 (dont des variantes de la même bête) |
| objets (version française) | 397 : 136 armes, 47 armures, 10 boucliers, 120 accessoires, 84 consommables et objets de quête ; 132 portent un pouvoir |
| sorts jouables | 73 sur trois écoles (29 + 24 + 20), plus 7 plantes d'une quatrième école débranchée |
| coffres | 161 |
| comportements d'entité distincts | 1 429 (IA, décor, effets, scènes) |

Sources : `Documentation/Reference/Bestiaire/LISEZMOI.md`, `Documentation/Reference/Sorts.md`,
`Documentation/Etat/GestionnairesEntites.md`.

> **Pour un jeu neuf.** Les piliers 1, 2, 4 et 5 tiennent sans la PlayStation. Le pilier 3 est le plus
> singulier, et le plus discutable : des séquences de treize boutons se mémorisent mal. Le portage
> actuel a déjà essayé de le rendre personnalisable (LP-016, voir
> [07-Heritage-PS1](07-Heritage-PS1.md)).
