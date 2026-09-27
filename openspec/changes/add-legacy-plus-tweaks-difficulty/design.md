## Context

Voir `proposal.md` pour le pourquoi. Ce qui contraint la manière de faire :

- **La reconstruction est une seule `partial class Blaze` statique.** On ne peut ni surcharger ni
  remplacer une méthode : deux définitions sont une erreur de compilation, et un patch à
  l'exécution (Harmony, MonoMod) est fragile ici. La release est trimmée, un avertissement de trim
  y compte comme une erreur, et le JIT peut inliner une petite méthode statique, ce qui rend le
  patch sans effet sans rien dire.
- **Tous les coups passent par un seul point.** `ApplyDamage_00539A20` (`FUN_00539a20`) retire les
  PV pour la mêlée (`ResolveMeleeHit_00539B80`) comme pour les sorts (`ResolveSpellHit_0053A2B0`).
  Les seules autres écritures de `+0x4c` sont des dégâts en pourcentage (`FUN_005397b0`) et les
  ponctions de `WorldSimulationStep` (poison, 3 % des PV max).
- **Les PV d'une entité de scène naissent à un seul endroit.** `ResetEntityStats` (`FUN_005389e0`,
  `Screens/State03/WorldMapScript.cs`) recopie `modele + 0x14` dans `+0x48` (max) et `+0x4c`
  (courant). Ses six appelants passent tous par elle. `RebaseStatBlockOnModel_00538AF0`, rejoué à
  chaque coup, ne touche pas aux PV : un PV mis à l'échelle à la naissance le reste.
- **Le jeu de 1998 a déjà sa propre difficulté**, qu'on ne touche pas. `s_DifficultyBonus_0056d2c0`,
  indexée par `DAT_005de034`, ajoute 0 à 30 % aux huit caractéristiques de `modele + 0x30`, mais
  seulement pour les entités qui portent le bit `0x80000000` en `stat + 0x14`. Les tweaks
  s'appliquent par-dessus, sans la modifier.
- **Les PV et les dégâts sont des `short` signés** partout où le jeu les compare (`< 1`).
- **`Affichage.ini` a déjà posé le contrat** d'un fichier que le jeu lit sans l'écrire, avec un
  analyseur écrit à la main (UTF-8, compatible avec le trim) : `Host/DisplaySettings.cs`.

## Goals / Non-Goals

**Goals :**
- Un mécanisme de tweak que les lots suivants réutilisent sans le redessiner.
- Le moins de lignes possible dans le legacy, et aucune qui nomme un tweak.
- Tous les tweaks à leur défaut = le jeu d'aujourd'hui, octet pour octet dans son comportement.

**Non-Goals :**
- La substitution d'un gestionnaire entier par adresse Ghidra. Elle se branchera sur les deux appels
  de `DispatchEntityHandlerCore` au lot de l'auto-loot, pas avant qu'un tweak en ait besoin.
- Distinguer un monstre d'un décor à PV (tonneau, porte cassable) pour le tweak des PV.
- Le configurateur graphique.

## Decisions

### D1. Une couture est un champ délégué, dont le défaut est l'identité

```csharp
// src/LegacyPlus/Coutures.cs
internal static unsafe class Coutures
{
    internal delegate short Degats(uint* attaquant, uint* victime, short degats);
    internal delegate ushort Pv(byte* stats, ushort pv);

    internal static Degats DegatsSubis = (a, v, d) => d;
    internal static Pv PvALaNaissance = (s, pv) => pv;
}
```

Un tweak actif **s'empile** : il garde la valeur précédente du délégué et l'appelle avant d'appliquer
son effet. Plusieurs tweaks peuvent donc partager une couture, dans l'ordre d'installation.

*Écartés* : l'exclusion de fichiers à la compilation (le choix se ferait au build, pas par le joueur,
et la copie d'un fichier se désynchronise du legacy) ; le patch à l'exécution (voir Context) ; une
interface avec une implémentation par tweak (plus de cérémonie pour le même effet, et la règle du
projet refuse les objets à cycle de vie).

### D2. Aucune ligne au démarrage : l'installation se fait au premier appel d'une couture

Le constructeur statique de `Coutures` pose les défauts, lit `LegacyPlus.ini`, puis appelle
l'installation de chaque tweak. La première couture appelée déclenche donc tout. Le legacy n'a pas
besoin d'une ligne dans `Startup.cs`, et l'ordre d'initialisation est déterministe : c'est un
constructeur de type, pas l'initialiseur d'un champ d'une classe partielle répartie sur plusieurs
fichiers (le piège documenté dans `CLAUDE.md`).

Toute exception pendant l'installation est journalisée, et les coutures restent à l'identité. Un
fichier de réglages cassé ne doit jamais empêcher le jeu de tourner.

### D3. La couture des dégâts va à l'entrée d'`ApplyDamage_00539A20`

```csharp
damage = Coutures.DegatsSubis(attacker, victim, damage);   // Couture legacy ++ (LP-001)
```

C'est le seul point commun à la mêlée et aux sorts, et il est placé **avant** l'écriture de
`+0x100`. Le nombre affiché et les objets déclenchés (`TriggerEquippedItems_005238E0`) voient donc
la valeur retouchée, exactement comme s'ils la recevaient du jeu.

Le tweak lui-même teste `(*victime & 0x800000) != 0` (la victime est un membre du groupe), multiplie,
puis borne à `[1, 32767]`. Le plancher à 1 respecte « personne ne fait jamais zéro ».

*Écartés* : `ResolveMeleeHit` et `ResolveSpellHit` séparément (deux coutures pour un seul effet ; le
scaling magique aura sa propre couture dans `ResolveSpellHit` à son lot) ; `FUN_005397b0` et les
ponctions (dégâts en pourcentage, hors du périmètre fixé par la spec).

### D4. La couture des PV enveloppe les deux affectations de `ResetEntityStats`

```csharp
*(ushort*)(stats + 0x48) = Coutures.PvALaNaissance(stats, *(ushort*)(model + 0x14));   // LP-002
*(ushort*)(stats + 0x4c) = Coutures.PvALaNaissance(stats, *(ushort*)(model + 0x14));   // LP-002
```

Deux appels plutôt qu'une variable locale, pour que la ligne legacy garde sa forme contre
`Blaze.c`. La fonction est pure, donc les deux appels rendent la même valeur. Le tweak borne à
`[1, 32767]`.

*Écartés* : les six appelants (six coutures au lieu d'une) ; `RebaseStatBlockOnModel` (ne touche pas
aux PV, et tourne à chaque coup).

### D5. Les réglages : `LegacyPlus.ini`, même contrat qu'`Affichage.ini`

`src/LegacyPlus/ReglagesLegacyPlus.cs` lit le fichier une fois, sans réflexion (trim), en UTF-8. Si
l'analyseur de `DisplaySettings` se laisse extraire sans changer son comportement, on le partage ;
sinon, on en écrit un second, petit. Les deux fichiers restent **séparés** : l'un règle l'image,
l'autre l'équilibre du jeu, et un joueur doit pouvoir partager l'un sans l'autre.

**Les valeurs sont des multiplicateurs** (`1` = jeu d'origine, `2`, `1.5` ou `1,5`), à la demande de
Vincent : un pourcentage se lit moins bien. Le lecteur les garde en **centièmes** (arrondis au plus
proche), pour que les tweaks calculent en entiers : `d * 150 / 100`, sans flottant, donc sans écart
d'arrondi d'une machine à l'autre.

### D6. Numérotation `LP-NNN` et registre `Documentation/LegacyPlus/legacy.md`

`LB-NNN` reste réservé aux défauts de 1998 (`legacy-bug.md`). Un tweak prend un numéro `LP-NNN`, et
chacune de ses coutures porte ce numéro sur place. Le registre dit, pour chaque tweak : sa clé, sa
couture, son fichier dans `LegacyPlus/`. Ce lot : **LP-001** (dégâts des monstres) et **LP-002** (PV
des monstres).

### D7. Les défauts du fichier déposé sont tous inactifs

À l'inverse des écarts `LB` (actifs par défaut), un tweak change l'équilibre : l'installeur dépose un
`LegacyPlus.ini` où tout est à 1. Le joueur active ce qu'il veut.

## Risks / Trade-offs

- **[Un PV mis à l'échelle sert parfois de compteur]** : un décor cassable ou un boss dont le script
  lit un seuil absolu de PV peut changer de comportement. → Vincent vérifie en jeu une salle avec
  un décor cassable et un boss, à `PvMonstres = 2`. Si un cas se présente, le tweak filtre sur la
  famille (`stat + 0x14`), sans toucher la couture.
- **[Un délégué par coup]** : un appel indirect de plus par coup et par naissance, négligeable à
  l'échelle d'une image.
- **[Un tweak qui tirerait un nombre aléatoire]** décalerait toute la suite `CrtRand` et ferait
  diverger la partie. → La règle est écrite dans la spec `gameplay-tweaks` et dans le registre.
- **[L'oracle `compare-bodies.ps1`]** signalera les trois lignes comme une logique changée. → Elles
  sont déclarées. Si l'outil a une liste d'exceptions, on les y ajoute.

## Migration Plan

Aucune donnée existante n'est touchée. Une installation existante n'a pas de `LegacyPlus.ini`, donc
tous ses tweaks sont inactifs : le jeu reste celui d'aujourd'hui. Pour revenir en arrière, il suffit
de supprimer le fichier ou de remettre les clés à 1.
