## Context

Voir `proposal.md` pour le pourquoi, et `add-legacy-plus-tweaks-difficulty/design.md` pour le cadre
(coutures, empilement, `LegacyPlus.ini`). Faits relevés pour ce lot :

- **Tous les dégâts de sort passent par `ResolveSpellHit_0053A2B0`** (27 sites d'appel). Le montant
  de la fiche (`fiche + 0x18 + variante * 2`) s'y ajoute à **une seule ligne**, sous la condition
  d'élément `element == 0 || element != DominantElement_005399E0(victimBlock)`.
- La même valeur est lue **une autre fois** plus haut (`attackerBlock + 0x10c + element * 2 += …`),
  pour renforcer l'élément du lanceur, pas pour les dégâts. Elle **n'est pas touchée** : la
  multiplier changerait l'échange élémentaire, ce que Vincent n'a pas demandé.
- **MAG** est `attackerBlock + 0x38`, celle que la formule d'origine divise déjà. Pour un membre du
  groupe : `(niveau + caractéristique) / 4`, plus l'équipement, bornée à 999.
- Relevé des 81 fiches des écoles : le montant va de 0 à 200. Les diviseurs 3 et 4 sont ceux des
  petits sorts (Balle de feu, Rocher, Ténèbres…), 1 celui des gros (Boule de feu, Météores).

## Goals / Non-Goals

**Goals :** faire monter tous les sorts de dégâts du groupe avec la MAG, en gardant la hiérarchie
entre sorts et le début de partie d'origine.

**Non-Goals :** retoucher `MAG / diviseur − DEF` ; les sorts des monstres ; les soins et sorts
d'état, dont le montant n'arrive pas par cette ligne.

## Decisions

### D1. La couture porte sur le montant, pas sur le total

```csharp
damage += LegacyPlus.Coutures.MontantDeSort(attacker, attackerBlock, *(ushort*)(sheet + 0x18 + variant * 2));   // Couture legacy ++ (LP-003)
```

La ligne reste **dans** le `if` d'élément du legacy. Le tweak n'a donc pas à reproduire cette règle,
et ne voit que ce qu'il multiplie. *Écarté* : une couture sur le total, qui aurait dû soit recalculer
la condition d'élément, soit multiplier aussi la partie `MAG − DEF`.

### D2. Formule en entiers

`montant + montant × coef × MAG / 10000`, avec `coef` en centièmes (1 = 100). C'est la même
convention que les multiplicateurs de difficulté : pas de flottant, donc pas d'écart d'arrondi
d'une machine à l'autre. Exemple : 10 + 10 × 100 × 130 / 10000 = 23. Le plus grand produit
intermédiaire (200 × 500 × 999 ≈ 10⁸) tient dans un `int`.

### D3. Borne à 30 000

Le total `(attaque − défense) + échange élémentaire + montant` est ensuite converti en `short` par
l'appel d'`ApplyDamage`. En bornant le montant à 30 000, la somme reste sous 32 767 pour toute
attaque bornée à 999 et tout échange élémentaire réaliste. Le jeu d'origine n'approche pas cette
limite : son plus gros montant vaut 200.

### D4. Groupe seulement

Le tweak teste le bit `0x800000` du lanceur (`*attaquant`), comme `Difficulte` le fait pour la
victime.

### D5. `[Magie] ScalingMagique`, de 0 à 5

C'est une nouvelle section, parce que la clé règle la puissance du groupe, pas la difficulté.
0 = jeu d'origine. Le plafond est 5 : à MAG 130, ×7,5 sur le montant, c'est déjà très au-delà de ce
qui rend le jeu jouable. Le lecteur partage `LireMultiplicateur` avec la difficulté, avec des bornes
propres.

## Risks / Trade-offs

- **[Sorts d'état à montant non nul]** : Zombie (30), Pardon (5), Coup de pouce (4) passent peut-être
  par la même ligne. → Leur montant n'est multiplié que s'ils font vraiment des dégâts par
  `ResolveSpellHit`. Vincent vérifie en jeu qu'un sort d'état ne change pas de comportement visible.
- **[Équilibre]** : à coef 1 et MAG élevée, un gros sort double à peu près (~97 → ~191). C'est voulu :
  la valeur conseillée est un point de départ, et le joueur règle.
