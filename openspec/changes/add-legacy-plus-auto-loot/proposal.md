## Why

Dans le jeu de 1998, un monstre tué lâche un cristal d'expérience et des pièces, qu'il faut aller
ramasser en marchant dessus avant qu'ils ne s'estompent. À quatre, en plein combat, c'est une corvée,
et une part du butin se perd. Vincent veut qu'ils soient ramassés automatiquement — **pièces et
cristaux seulement, pas les coffres** que certains monstres font apparaître.

## What Changes

- **LP-005 — le ramassage automatique des pièces et des cristaux.** Une fois posé au sol, fini de
  rebondir, un butin est ramassé sans qu'on marche dessus, par le code de ramassage d'origine :
  l'expérience du cristal est partagée entre les membres debout, les pièces vont dans la bourse de
  celui qui les ramasse, le son et la libération de l'emplacement sont ceux du jeu.
- **Les pièces se partagent à tour de rôle** entre les membres debout (1 à 4) ; l'expérience du
  cristal est de toute façon partagée par le jeu.
- **Les coffres ne sont jamais concernés** : leur butin porte le bit `0x8000`, écarté.
- **Une clé** `[Butin] RamassageAuto = non` (jeu d'origine) ou `oui`, dans `LegacyPlus.ini`.
- **Une ligne dans le legacy** (`PartyMemberStep.cs`) : l'entité que la collision désigne passe par
  une couture `ButinAtteint`, marquée `// Couture legacy ++ (LP-005)`.

## Capabilities

### New Capabilities

- `gameplay-auto-loot` : le ramassage automatique des pièces et des cristaux d'expérience.

### Modified Capabilities

(aucune)

## Impact

- **Legacy** : `Screens/State03/Tick/PartyMemberStep.cs`, une ligne.
- **LegacyPlus** : `Coutures.cs` (la couture), `ReglagesLegacyPlus.cs` (la clé), un nouveau
  `RamassageAuto.cs`.
- **Installeur** : `Reglages/LegacyPlus.ini`, section `[Butin]`.
- **Dépend de** `add-legacy-plus-tweaks-difficulty` (le cadre).
