# OpenForesia — Reverse engineering de Blaze

## Objectif général

Nous reconstruisons en C# le comportement d'un ancien exécutable Windows x86 natif.

Le fichier `Blaze.c` est un pseudo-code généré par Ghidra à partir du binaire original.

`Blaze.c` est notre **source de référence immuable**.

Il ne doit JAMAIS être modifié.

Le nouveau code C# doit être écrit dans le projet OpenForesia :

`src/Blaze/Blaze.cs`

Pour cette première phase, conserver l'ensemble de la reconstruction dans ce fichier unique. Le découpage en plusieurs classes sera effectué ultérieurement lorsque les responsabilités du code seront mieux comprises.

---

## Principe fondamental

L'objectif n'est PAS de simplement transformer le pseudo-C ligne par ligne pour obtenir du C# qui compile.

L'objectif est de reconstruire progressivement un code C# :

* compréhensible par un développeur ;
* aussi fidèle que possible au comportement du programme original ;
* structuré proprement ;
* compilable progressivement ;
* documenté lorsqu'une partie du comportement reste incertaine.

La préservation de la logique originale est plus importante que la suppression rapide des erreurs de compilation.

Ne jamais inventer arbitrairement un comportement uniquement pour faire compiler le code.

---

## Source de vérité

Toujours utiliser `Blaze.c` comme référence lors de la reconstruction.

Ne jamais modifier `Blaze.c`.

Ne jamais considérer une précédente traduction C# comme plus fiable que `Blaze.c`.

Lorsqu'une ambiguïté apparaît :

1. revenir au code correspondant dans `Blaze.c`;
2. analyser les appels et les variables environnantes ;
3. conserver autant que possible la sémantique du pseudo-code Ghidra ;
4. utiliser un `TODO(REVERSE)` si le comportement exact reste impossible à déterminer.

---

## Point de départ

Commencer exclusivement par la fonction :

`entry`

Analyser son rôle et reconstruire son équivalent C# dans `Blaze/Blaze.cs`.

Ensuite, suivre les fonctions appelées par `entry`.

Procéder progressivement selon le graphe d'appels :

`entry`
→ fonctions directement appelées
→ fonctions appelées par celles-ci
→ etc.

Ne pas commencer à traduire aléatoirement des fonctions éloignées du point d'entrée.

---

## Méthode de reconstruction

Pour chaque fonction Ghidra rencontrée :

1. lire entièrement la fonction dans `Blaze.c`;
2. identifier autant que possible son rôle ;
3. analyser les fonctions qu'elle appelle ;
4. analyser les données globales qu'elle utilise ;
5. déterminer les types C# les plus plausibles ;
6. reconstruire son contrôle de flux ;
7. écrire son équivalent dans `Blaze/Blaze.cs`;
8. compiler le projet ;
9. corriger uniquement les erreurs liées au code nouvellement ajouté ;
10. continuer avec les dépendances nécessaires.

Travailler par petits groupes cohérents de fonctions.

Relancer régulièrement :

`dotnet build`

Si une modification augmente fortement le nombre d'erreurs ou oblige à introduire des casts artificiels partout, revenir en arrière et réexaminer la traduction.

---

## Nommage des fonctions

Les fonctions Ghidra ont généralement des noms tels que :

`FUN_004025A0`

Lorsque le rôle d'une fonction est suffisamment clair, lui donner un nom C# humain et descriptif.

Exemples possibles :

`InitializeRuntime`
`ParseCommandLine`
`InitializeGraphics`
`LoadConfiguration`

Ne choisir un nom métier que lorsque le rôle est raisonnablement établi.

Si le rôle est encore incertain, conserver temporairement le nom Ghidra ou utiliser une forme telle que :

`Unknown_004025A0`

avec :

`// TODO(REVERSE): déterminer le rôle exact de cette fonction.`

Ne jamais inventer un nom métier sans éléments suffisants.

Conserver dans un commentaire le nom/adresse Ghidra d'origine afin de garder la traçabilité.

Exemple :

`// Ghidra: FUN_004025A0 @ 0x004025A0`

---

## Labels et goto

Les labels Ghidra tels que :

`LAB_004028C1`
`LAB_004028D4`

représentent des blocs du graphe de contrôle du programme natif.

Ne PAS transformer mécaniquement un label en méthode.

Ne PAS transformer mécaniquement un `goto` en appel de méthode.

Analyser d'abord le contrôle de flux.

Lorsque c'est possible, reconstruire avec les structures C# appropriées :

* `if`
* `else`
* `switch`
* `while`
* `do / while`
* `for`
* `break`
* `continue`
* `return`

Créer une méthode séparée uniquement si le bloc représente réellement une responsabilité réutilisable ou une unité logique indépendante.

Si le contrôle de flux ne peut pas encore être reconstruit avec confiance :

`// TODO(REVERSE): reconstruire le control-flow correspondant à LAB_xxxxxxxx`

et conserver temporairement une structure fidèle et explicite plutôt que d'inventer une logique.

---

## Types et pointeurs

Le programme original est un programme natif x86 32 bits.

Faire particulièrement attention à :

* pointeurs ;
* arithmétique de pointeurs ;
* tailles `byte`, `short`, `ushort`, `int`, `uint`;
* conversions signées/non signées ;
* structures ;
* tableaux ;
* handles Windows ;
* pointeurs de fonctions ;
* conventions d'appel.

Ne pas ajouter des casts uniquement pour satisfaire le compilateur.

Lorsqu'une conversion semble étrange, vérifier d'abord si le véritable problème est :

* un mauvais type de variable ;
* une mauvaise signature de fonction ;
* un pointeur mal reconstruit ;
* une structure non encore identifiée.

Préférer corriger la cause racine.

---

## Fonctions natives et Win32

Lorsque le pseudo-code utilise une API Windows connue, utiliser un P/Invoke C# approprié plutôt que de reproduire artificiellement le comportement bas niveau.

Conserver un commentaire indiquant la fonction native originale lorsque cela aide à la traçabilité.

Pour les appels indirects ou pointeurs de fonctions dont la signature n'est pas encore certaine :

`// TODO(REVERSE): confirmer la signature native et la convention d'appel.`

Ne pas inventer silencieusement une signature.

---

## Données globales Ghidra

Les symboles tels que :

`DAT_005xxxxx`
`PTR_005xxxxx`

doivent être analysés progressivement.

Lorsqu'un rôle est clairement identifié, leur donner un nom lisible.

Sinon conserver temporairement un nom lié à l'adresse avec un `TODO(REVERSE)`.

Ne jamais décider arbitrairement qu'une adresse contient un `int`, un pointeur ou une structure simplement pour faire disparaître une erreur de compilation.

---

## Comportement inconnu

Lorsqu'une instruction ne peut pas être traduite avec suffisamment de confiance :

utiliser :

`// TODO(REVERSE): ...`

Le commentaire doit expliquer précisément ce qui reste à déterminer.

Éviter les commentaires vagues comme :

`// TODO: fix`

Préférer :

`// TODO(REVERSE): déterminer si DAT_005E97CC référence une structure globale ou un pointeur vers une structure.`

---

## Interdictions

Ne jamais :

* modifier `Blaze.c`;
* réécrire massivement le fichier avec des regex ;
* ajouter des centaines de casts simplement pour compiler ;
* remplacer arbitrairement une valeur inconnue par `0`, `null` ou `false`;
* supprimer une branche parce qu'elle est difficile à traduire ;
* supprimer un appel de fonction inconnu ;
* transformer tous les `goto` en appels de méthodes ;
* inventer une architecture métier qui n'est pas encore visible dans le code ;
* effectuer une refactorisation importante avant d'avoir compris le comportement correspondant.

---

## Compilation

Compiler régulièrement avec :

`dotnet build`

La compilation sert à vérifier la traduction, mais **un code qui compile n'est pas nécessairement correct**.

La fidélité au comportement de `Blaze.c` reste prioritaire.

Lorsqu'une fonction vient d'être traduite, privilégier la correction de cette fonction et de ses dépendances directes avant de continuer.

---

## Organisation — Phase 1

Pour le moment :

`Blaze/Blaze.cs`

doit contenir toute la reconstruction.

Ne pas créer prématurément :

* GraphicsService
* AudioManager
* GameEngine
* ResourceManager
* etc.

Même si ces concepts semblent probables.

Nous effectuerons ce découpage lors d'une seconde phase, une fois que suffisamment de fonctions auront été comprises pour identifier réellement les sous-systèmes.

---

## Définition de terminé pour une fonction

Une fonction peut être considérée comme reconstruite lorsque :

* son équivalent C# est présent ;
* son contrôle de flux est compréhensible ;
* les types utilisés sont raisonnablement cohérents ;
* son rôle est nommé correctement ou marqué comme inconnu ;
* sa provenance Ghidra reste identifiable ;
* elle ne contient pas de modification arbitraire destinée uniquement à compiler ;
* les incertitudes restantes sont indiquées par `TODO(REVERSE)`.
