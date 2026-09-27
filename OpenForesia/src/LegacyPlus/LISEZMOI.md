# `src/LegacyPlus/` — les tweaks de gameplay

Ce répertoire n'est pas le jeu de 1998. Il porte les **tweaks** : des écarts à l'équilibre ou aux
règles du jeu d'origine, choisis par le joueur dans `LegacyPlus.ini`, **tous inactifs par défaut**.
Le registre des tweaks est `Documentation/LegacyPlus/legacy.md`, et chacun y a un numéro `LP-NNN`.

## La règle : aucune condition d'option dans `src/Blaze/`

Le code transcrit ne sait pas quels tweaks existent. Il n'appelle que des **coutures** : des
fonctions de `Coutures.cs` dont le défaut ne change rien, soit le jeu de 1998. Dans le legacy, une
couture tient en une ligne, marquée sur place :

```csharp
damage = Coutures.DegatsSubis(attacker, victim, damage);   // Couture legacy ++ (LP-001)
```

Les `if` sur les options, et le code des tweaks, ne vivent qu'ici.

## La forme

```
src/LegacyPlus/
   LISEZMOI.md              ce fichier
   Coutures.cs              les points d'accroche ; défaut = identité ; installe les tweaks
   ReglagesLegacyPlus.cs    lecture de LegacyPlus.ini (lu une fois, jamais écrit)
   Difficulte.cs            LP-001, LP-002 — un fichier par tweak
   ScalingMagique.cs        LP-003
   VerrouParAttaquant.cs    LP-004
   TraceCombat.cs           diagnostic, pas un tweak : le journal des coups de mêlée
```

Un tweak est une classe statique avec une méthode `Installer()`. Si son option est active, il
**s'empile** sur une couture : il garde le délégué précédent, l'appelle, puis applique son effet.
Plusieurs tweaks partagent donc une couture, dans l'ordre d'installation (`Coutures`, constructeur
statique).

## Trois interdits

- **Pas de nombre aléatoire.** Un tweak ne tire jamais de `CrtRand` : il décalerait toute la suite
  du jeu, et la partie divergerait même là où le tweak n'a rien changé.
- **Pas de nouvelle couture sans la déclarer.** Chaque couture porte `// Couture legacy ++ (LP-NNN)`
  à sa ligne dans `src/Blaze/` et figure dans le registre.
- **Pas d'écriture de `LegacyPlus.ini`.** Le jeu le lit, et le joueur (ou un futur configurateur)
  l'écrit.
