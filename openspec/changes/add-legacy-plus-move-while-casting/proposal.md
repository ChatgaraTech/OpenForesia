## Why

Dans le jeu de 1998, un personnage qui lance un sort est cloué sur place, de la combinaison jusqu'au
départ du sort. Vincent veut qu'il puisse se déplacer pendant l'incantation, **à 50 % de sa vitesse**.

## What Changes

- **LP-006 — se déplacer en lançant un sort**, à une fraction réglable de la vitesse de marche. La
  croix directionnelle calcule déjà la vitesse pendant l'incantation ; c'est
  `ApplyGroundMotion_0051A020` qui la remet à zéro. Le tweak en garde une part.
- **Une clé** `[Magie] DeplacementEnIncantation`, de 0 (jeu d'origine) à 1 (vitesse pleine), 0.5
  conseillé.
- **Une ligne dans le legacy** (`Spells/SpellCasting.cs`), marquée `// Couture legacy ++ (LP-006)`.
- La combinaison du sort n'est pas touchée : elle se tape aux boutons d'action, pas à la croix.

## Capabilities

### New Capabilities

- `gameplay-move-while-casting` : le déplacement pendant l'incantation.

### Modified Capabilities

(aucune)

## Impact

- **Legacy** : `Screens/State03/Spells/SpellCasting.cs`, une ligne.
- **LegacyPlus** : `Coutures.cs`, `ReglagesLegacyPlus.cs`, un nouveau `DeplacementEnIncantation.cs`.
- **Installeur** : `Reglages/LegacyPlus.ini`, section `[Magie]`.
