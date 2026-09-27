## Context

Voir `proposal.md`, et `add-legacy-plus-tweaks-difficulty/design.md` pour le cadre. Relevé, dans le
pas d'un membre du groupe (`Tick/PartyMemberStep.cs`) :

1. la vitesse au sol `+0x5c` est remise à **zéro** en tête de chaque image (l. 254) ;
2. le gestionnaire de commande (`DispatchInputHandler`) — l'un des trois gestionnaires de marche,
   `0x0050E140`, `0x0050DE90`, `0x0050DD00`, selon le schéma — lit la croix, tourne le cap et, **si
   une direction est tenue**, pose la vitesse de marche par `ApplyWalkSpeed` ; il tourne aussi
   pendant l'incantation, mais ne bascule pas l'animation en marche (le bit `4` l'en empêche) ;
3. `ApplyGroundMotion_0051A020` (`Spells/SpellCasting.cs`), cas des **postures d'incantation**
   (1, 3, 5, 7), remet `+0x5c` à **zéro** dès que l'incantation est en cours (`*slot & 4`) : c'est ce
   zéro qui cloue le lanceur.

La combinaison se tape aux **boutons d'action** (`pad + 2 & 0xf000`, les quatre boutons de la
manette PlayStation), jamais à la croix : se déplacer ne vole aucune touche à la combinaison.

## Goals / Non-Goals

**Goals :** se déplacer pendant l'incantation, à une part réglable de la vitesse de marche.

**Non-Goals :** l'animation de marche pendant l'incantation (le lanceur garde sa pose et glisse) ;
les autres postures (mêlée, charge, objet) ; les sorts lancés par un objet ou un script.

## Decisions

### D1. Une couture de valeur sur le zéro

```csharp
*(int*)(s + 0x5c) = LegacyPlus.Coutures.VitesseEnIncantation(slot, *(int*)(s + 0x5c));   // Couture legacy ++ (LP-006)
```

La couture reçoit la vitesse que la croix vient de poser (zéro si aucune direction n'est tenue,
grâce à la remise à zéro de tête d'image) et rend celle à garder. **Par défaut, zéro** : exactement
la ligne d'origine. *Écartés* : sauter l'appel de `ApplyGroundMotion` (il porte aussi toute la
machine d'incantation) ; retoucher `ApplyWalkSpeed` (elle ne sait pas qu'on incante).

### D2. `[Magie] DeplacementEnIncantation`, de 0 à 1

Un multiplicateur en centièmes, comme les autres : `vitesse × coef / 100`, en entiers. 1 au plus :
lancer un sort ne doit pas faire courir plus vite que marcher.

## Risks / Trade-offs

- **[Le lanceur glisse]** : il garde sa pose d'incantation en avançant. Faire jouer une marche
  demanderait de toucher à la machine d'animation ; c'est hors de ce lot.
- **[Le cap]** : la croix tourne déjà le lanceur pendant l'incantation dans le jeu d'origine ; un
  sort dirigé part donc dans la direction où il avance.
