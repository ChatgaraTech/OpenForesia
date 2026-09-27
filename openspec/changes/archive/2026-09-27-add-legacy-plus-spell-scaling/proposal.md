## Why

Dans le jeu de 1998, un sort de base ne gagne presque rien avec le niveau. Un Sorcier niveau 200 bien
équipé fait ~7 points avec une Balle de feu, contre ~10 au niveau 1. La formule
(`ResolveSpellHit_0053A2B0`) est `max(0, MAG / diviseur − DEF_mag) + montant − DEF_mag / 32`. Avec
le diviseur de 3 ou 4 des petits sorts, `MAG / diviseur` (~43 à MAG 130) ne dépasse jamais la
défense magique d'un monstre de fin de jeu (50 à 240). Il ne reste donc que le montant fixe de la
fiche. Vincent a demandé un meilleur scaling, et a choisi la formule le 2026-09-27.

## What Changes

- **LP-003 — le montant d'un sort monte avec la MAG du lanceur** :
  `montant × (1 + ScalingMagique × MAG / 100)`. Le montant multiplié garde la place et les
  conditions de l'original : il ne s'ajoute toujours pas si la victime est déjà de l'élément du
  sort.
- **Groupe seulement** : un sort lancé par un monstre garde son montant d'origine. Sa force se
  règle déjà par `DegatsMonstres`.
- **Une clé** `[Magie] ScalingMagique` dans `LegacyPlus.ini` : un multiplicateur de 0 à 5, défaut 0
  (le jeu d'origine), valeur conseillée 1.
- **Une ligne dans le legacy** : l'ajout du montant dans `ResolveSpellHit_0053A2B0` passe par une
  couture `MontantDeSort`, marquée `// Couture legacy ++ (LP-003)`.
- Le modèle `LegacyPlus.ini` de l'installeur, le registre `legacy.md` et la référence
  `LegacyPlus.ini.md` gagnent la clé.

Hors de ce lot : la partie `MAG / diviseur − DEF` de la formule (inchangée), les soins, et les sorts
qui ne font pas de dégâts.

## Capabilities

### New Capabilities

- `gameplay-spell-scaling` : le montant des sorts du groupe multiplié par la MAG du lanceur.

### Modified Capabilities

(aucune — le cadre `gameplay-tweaks` et le dépôt par l'installeur sont ceux de
`add-legacy-plus-tweaks-difficulty`, et la nouvelle clé n'en change pas les exigences)

## Impact

- **Legacy** : `Screens/State03/Combat/HitResolution.cs`, une ligne.
- **LegacyPlus** : `Coutures.cs` (la couture), `ReglagesLegacyPlus.cs` (la clé), un nouveau
  `ScalingMagique.cs`.
- **Installeur** : `Reglages/LegacyPlus.ini`.
- **Dépend de** `add-legacy-plus-tweaks-difficulty` (le cadre), qui n'est pas encore archivé.
