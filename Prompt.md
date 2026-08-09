Nous commençons maintenant la reconstruction complète de Blaze.

Lis d'abord attentivement `CLAUDE.md` et respecte toutes ses règles pendant toute cette tâche.

## Source de référence

`Blaze.c` est le pseudo-code original généré par Ghidra depuis `Blaze.exe` (il se trouve dans le dossier solution  Legacy/Decompiled).

Il constitue notre source de référence et doit rester STRICTEMENT EN LECTURE SEULE.

NE MODIFIE JAMAIS `Blaze.c`.

Tout le nouveau code doit être écrit dans :

`Blaze/Blaze.cs`

Si ce fichier n'existe pas, crée-le.

## Objectif

Je veux reconstruire autant que possible l'intégralité du programme représenté dans `Blaze.c` en C# compréhensible, maintenable et aussi fidèle que possible au comportement original.

La reconstruction doit partir de la fonction Ghidra :

`entry`

Mais `entry` est uniquement le POINT DE DÉPART.

Ne t'arrête PAS après avoir traduit `entry`.

À partir de `entry`, suis récursivement le graphe d'appels et continue à reconstruire toutes les fonctions nécessaires et atteignables, fonction après fonction, aussi loin que possible.

Travaille de manière autonome et continue tant qu'il reste du code que tu peux raisonnablement reconstruire.

## Méthode

Commence par analyser complètement `entry`.

Ensuite :

1. reconstruis `entry` en C# ;
2. identifie toutes les fonctions qu'elle appelle ;
3. reconstruis ces fonctions ;
4. identifie à leur tour leurs dépendances ;
5. continue récursivement ;
6. répète ce processus aussi longtemps que possible.

Le processus doit donc ressembler à :

`entry`
→ dépendances directes
→ dépendances de ces dépendances
→ sous-systèmes découverts
→ fonctions utilisées par ces sous-systèmes
→ etc.

Je veux que tu progresses le plus loin possible dans `Blaze.c`, et non que tu t'arrêtes après quelques fonctions.

## Priorité absolue : préserver le comportement

Ne cherche PAS simplement à produire du C# qui compile.

La priorité est :

1. préserver la logique du programme original ;
2. rendre cette logique compréhensible ;
3. obtenir progressivement du C# compilable.

Ne remplace jamais arbitrairement :

* une valeur inconnue par `0` ;
* un pointeur par `null` ;
* une condition par `false` ;
* une fonction inconnue par une implémentation vide ;

uniquement pour satisfaire le compilateur.

Lorsqu'une information est réellement inconnue :

`// TODO(REVERSE): expliquer précisément ce qui reste à déterminer.`

## Renommage

Lorsque le rôle d'une fonction Ghidra devient suffisamment clair, renomme-la avec un nom humain.

Exemple :

`FUN_00513900`

pourrait devenir :

`RunApplication`

UNIQUEMENT si son comportement permet réellement de le déduire.

Toujours conserver la correspondance d'origine :

`// Ghidra: FUN_00513900 @ 0x00513900`

Si le rôle reste incertain :

`Unknown_00513900`

ou conserver temporairement `FUN_00513900`.

Ajouter alors :

`// TODO(REVERSE): déterminer le rôle exact de FUN_00513900.`

Ne jamais inventer un nom métier uniquement pour rendre le code joli.

## Variables

Renommer également les variables Ghidra lorsque leur rôle est identifiable.

Par exemple :

`iVar3`
`pbVar6`
`uVar4`

peuvent devenir des noms humains lorsqu'on peut raisonnablement déterminer leur signification.

Sinon conserver un nom neutre et ajouter un TODO uniquement si cela apporte quelque chose.

## Control-flow Ghidra

Les constructions suivantes :

`LAB_xxxxxxxx`
`joined_r0xxxxxxxxx`
`goto LAB_xxxxxxxx`

sont des artefacts du graphe de contrôle reconstruit par Ghidra.

Je veux autant que possible les faire disparaître du C# final.

Mais NE LES REMPLACE PAS MÉCANIQUEMENT par des appels de méthodes.

Analyse le graphe de contrôle et reconstruis avec les structures C# appropriées :

* `if / else`
* `switch`
* `while`
* `do / while`
* `for`
* `break`
* `continue`
* `return`

Créer une méthode séparée seulement lorsqu'un bloc représente réellement une unité logique indépendante.

Si une reconstruction fidèle du contrôle de flux n'est pas encore possible :

conserve provisoirement une représentation explicite et ajoute :

`// TODO(REVERSE): reconstruire fidèlement le control-flow de LAB_xxxxxxxx.`

Ne change jamais silencieusement le chemin d'exécution.

## Fonctions runtime / bibliothèque

Le point d'entrée contient probablement du code généré par le runtime C/C++ ou le compilateur.

Essaie de distinguer :

* initialisation runtime ;
* CRT ;
* wrappers Win32 ;
* bibliothèque standard ;
* vrai code spécifique à Blaze.

Lorsqu'une fonction correspond clairement à une fonction standard ou Win32 connue, utilise une abstraction C# ou un P/Invoke approprié plutôt que de réimplémenter inutilement son assembleur.

Mais conserve la correspondance avec le code original.

## Pointeurs et architecture

Le programme original est un exécutable Windows natif x86 32 bits.

Respecte donc soigneusement :

* taille des pointeurs ;
* signed / unsigned ;
* `byte`, `short`, `ushort`, `int`, `uint`;
* structures mémoire ;
* arithmétique de pointeurs ;
* appels indirects ;
* vtables éventuelles ;
* handles Win32 ;
* conventions d'appel.

Une conversion étrange doit d'abord être considérée comme le signe d'un mauvais type reconstruit, plutôt que comme une invitation à ajouter un cast.

## Compilation

Utilise fréquemment :

`dotnet build`

Mais uniquement sur le code que tu as effectivement commencé à reconstruire.

Après chaque groupe cohérent de fonctions :

1. compile ;
2. analyse les erreurs ;
3. corrige leur cause racine ;
4. recompile ;
5. continue.

Si une modification augmente fortement les erreurs :

1. examine immédiatement la modification ;
2. détermine pourquoi ;
3. annule-la si elle était incorrecte.

Ne laisse pas s'accumuler des centaines d'erreurs générées par une mauvaise décision de type.

## Progression autonome

Je veux que tu travailles aussi longtemps et aussi loin que possible.

Ne t'arrête pas après chaque fonction pour me demander si tu peux continuer.

Tu as explicitement l'autorisation de continuer automatiquement :

* lecture de `Blaze.c` ;
* création et modification de `Blaze/Blaze.cs` ;
* navigation dans le graphe d'appels ;
* renommage raisonnablement certain ;
* reconstruction du contrôle de flux ;
* lancement de `dotnet build` ;
* corrections résultant du build.

Ne demande mon intervention que lorsqu'une décision importante ne peut réellement pas être prise à partir du code disponible.

Dans ce cas, préfère généralement un `TODO(REVERSE)` et continue ailleurs plutôt que de bloquer tout le travail.

## Couverture

Maintiens en haut ou en bas de `Blaze.cs` un petit bloc de suivi, ou un fichier séparé `Blaze/REVERSE_STATUS.md`, contenant notamment :

* fonctions Ghidra analysées ;
* fonctions reconstruites ;
* fonctions renommées ;
* fonctions encore inconnues ;
* TODO importants ;
* prochaine zone du graphe d'appels à explorer.

Pour chaque fonction reconstruite, marque-la comme terminée dans ce suivi.

Cela permettra de reprendre facilement le travail lors d'une nouvelle session Claude Code.

## Phase actuelle

Pour cette phase, garde la reconstruction principalement dans :

`Blaze/Blaze.cs`

Ne cherche PAS encore à concevoir une architecture en dizaines de classes.

Si tu identifies clairement des sous-systèmes (audio, rendu, entrée, ressources, etc.), note-les dans `REVERSE_STATUS.md`, mais conserve pour l'instant le code dans `Blaze.cs`.

Le découpage architectural sera effectué dans une seconde phase.

## Démarrage

Commence maintenant.

1. Lis `entry` dans `Blaze.c`.
2. Analyse son comportement et ses appels directs.
3. Crée `Blaze/Blaze.cs`.
4. Reconstruis `entry`.
5. Suis récursivement ses dépendances.
6. Continue autant que possible sans attendre une nouvelle instruction de ma part.
7. Maintiens `REVERSE_STATUS.md` pendant ta progression.
8. Compile régulièrement.
9. Ne modifie jamais `Blaze.c`.

Ne fais pas seulement une analyse ou un plan.

Après ton analyse initiale, commence immédiatement à écrire le code et poursuis la reconstruction.
