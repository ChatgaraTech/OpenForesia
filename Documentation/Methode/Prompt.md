Nous commençons maintenant la reconstruction complète de Blaze.

Lis d'abord attentivement `CLAUDE.md` et respecte toutes ses règles pendant toute cette tâche.

## Source de référence

`Blaze.c` est le pseudo-code original généré par Ghidra depuis `Blaze.exe` (il se trouve dans le dossier solution  Legacy/Decompiled).

Il constitue notre source de référence et doit rester STRICTEMENT EN LECTURE SEULE.

NE MODIFIE JAMAIS `Blaze.c`.

Tout le nouveau code doit être écrit sous :

`OpenForesia/src/Blaze/`

La reconstruction y est répartie en `Platform/`, `Memory/`, `Engine/` et `Screens/`, tous parts d'une même classe partielle `Blaze`. Le fichier qui accueille une fonction n'est pas au choix : il est déterminé par la règle de routage décrite dans `Documentation/ARCHITECTURE.md`. Lis-le avant d'ajouter ou de déplacer du code.

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
* création et modification des fichiers sous `OpenForesia/src/Blaze/` ;
* navigation dans le graphe d'appels ;
* renommage raisonnablement certain ;
* reconstruction du contrôle de flux ;
* lancement de `dotnet build` ;
* corrections résultant du build.

Ne demande mon intervention que lorsqu'une décision importante ne peut réellement pas être prise à partir du code disponible.

Dans ce cas, préfère généralement un `TODO(REVERSE)` et continue ailleurs plutôt que de bloquer tout le travail.

## Couverture

Maintiens `Documentation/Etat/REVERSE_STATUS.md`, contenant notamment :

* fonctions Ghidra analysées ;
* fonctions reconstruites ;
* fonctions renommées ;
* fonctions encore inconnues ;
* TODO importants ;
* prochaine zone du graphe d'appels à explorer.

Pour chaque fonction reconstruite, marque-la comme terminée dans ce suivi.

Cela permettra de reprendre facilement le travail lors d'une nouvelle session Claude Code.

## Phase actuelle

Le découpage architectural a été effectué le **2026-08-19** : `Blaze.cs` faisait 23 680 lignes, il
est réparti en 30 fichiers. `ARCHITECTURE.md` fait autorité sur la carte et sur les règles.

Ce que cela change pour toi :

* **Ne conçois toujours PAS une architecture en dizaines de classes.** Tout `src/Blaze/` reste une
  seule classe partielle `Blaze`. Le fichier est une unité de navigation, pas d'encapsulation : le
  binaire n'a aucune encapsulation à imiter.
* **Ne crée pas de classe** nommée `Manager`, `Service`, `Controller` ou `Provider`. Ces noms
  promettent un objet à cycle de vie que la transcription n'a pas.
* **Range une fonction là où la règle de routage la met**, pas là où elle te paraît jolie : plage
  d'adresses connue → `Engine/` ; un seul écran appelant → `Screens/StateNN/` ; plusieurs écrans →
  promotion en `Engine/` ; sinon elle reste chez son appelant.
* **Ne déplace jamais une globale pour rendre un fichier autonome**, et n'éclate jamais une arène.
  Le code se range par responsabilité, l'état par adresse.
* **Ne déplace pas un chantier en cours.** Un chantier terminé se range ; un chantier ouvert reste
  où il est.

Si tu identifies un sous-système que la carte ne nomme pas encore, note-le dans `REVERSE_STATUS.md`
et laisse le code chez son appelant jusqu'à ce que plusieurs écrans l'appellent — c'est cet
événement, et lui seul, qui crée un nouveau fichier moteur.

## Démarrage

Commence maintenant.

1. Lis `REVERSE_STATUS.md` et `TODO.md` : ils disent où le travail s'est arrêté.
2. Lis `ARCHITECTURE.md` : il dit où va le code que tu vas écrire.
3. Reprends au point de blocage courant, en suivant le graphe d'appels.
4. Suis récursivement les dépendances.
5. Continue autant que possible sans attendre une nouvelle instruction de ma part.
6. Maintiens `REVERSE_STATUS.md` pendant ta progression.
7. Compile régulièrement.
8. Ne modifie jamais `Blaze.c`.

Le point de départ historique était `entry`, désormais reconstruit dans
`src/Blaze/Platform/Startup.cs`. La reconstruction couvre les états de jeu 0 à 6.

Ne fais pas seulement une analyse ou un plan.

Après ton analyse initiale, commence immédiatement à écrire le code et poursuis la reconstruction.

---

## Ce que tu n'as PAS le droit de faire en refactorisant

Le dépôt porte 1 834 mentions `FUN_`/`DAT_`/`LAB_`. Sa valeur tient à ce qu'on puisse poser
`Blaze.c` et le C# côte à côte.

Avant chaque transformation, pose-toi cette question :

**Peut-on encore vérifier cette fonction contre `Blaze.c` en trente secondes ?**

Trois pratiques par ailleurs saines sont **interdites dans `src/Blaze/`** :

* **l'extraction de méthode** — une méthode = une `FUN_` ;
* **le remplacement d'un `switch` par du polymorphisme** — ces `switch` **sont** la machine à états
  du binaire ;
* **le renommage sans conserver l'ancre** `// Ghidra: FUN_00XXXXXX`.

Dans `src/Host/`, notre échafaudage, les trois sont autorisées.

## Prouve que tu n'as changé que la forme

Deux outils, dans `OpenForesia.Tools/` :

* `verify-split.ps1` — comparaison **octet à octet** contre une référence figée, pour un
  déplacement pur ;
* `compare-bodies.ps1` — comparaison des **corps de méthode**, en normalisant ce qu'un refactor a
  le droit de changer (`static`, modificateurs d'accès, qualifications ajoutées, indentation).

Lance celui qui correspond après chaque étape. Et si tu écris un nouvel outil de vérification,
**vérifie-le d'abord** : `compare-bodies.ps1 -SelfTest` existe pour cette raison.

## Mesure avant d'écrire

Trois fois pendant le refactor de 2026-08-19, une tâche planifiée de bonne foi s'est révélée
inutile ou nuisible une fois mesurée : un pont statique que l'outillage rendait superflu, une
interface sans second implémenteur, un `IDisposable` sur des classes statiques qui ne peuvent pas
en porter.

Quand une tâche dit « mesurer d'abord », c'est qu'elle peut s'annuler elle-même. **Ne construis pas
ce que la mesure ne justifie pas**, et consigne pourquoi.

La mesure sert aussi à **choisir l'ordre du travail**, pas seulement à annuler une tâche. Le 2026-08-19,
l'ordre prévu pour convertir `Engine/` allait du plus petit fichier au plus grand ; la mesure de
**clôture** — combien de symboles un fichier possède, combien il emprunte, et à qui — a désigné
l'inverse. Le plus petit (`UiWindow.cs`, 368 l.) ne possède aucune globale et en emprunte six à des
écrans, donc le convertir aurait entamé une étape délibérément différée.

**La taille d'un fichier ne dit rien de son coût de conversion ; sa clôture, si.**

## Un indicateur vert ne prouve que ce qu'il mesure

Toujours le 2026-08-19 : la conversion de `Engine/Video.cs` a fait passer la course pour la mémoire
basse de « une fois sur quatre perdue » à cinq sur huit. Pendant toute la régression, les deux
verdicts habituels étaient **verts** — aucun plantage, 382,3 et 387 primitives par image, captures
conformes. Ce qui l'a révélée est une ligne d'information que rien n'obligeait à lire :
« relance détectée », imprimée par `playtest.ps1`.

Quand le banc d'essai dit quelque chose d'inhabituel, même sans le marquer comme une erreur,
**cherche pourquoi avant de conclure**. Compare à ce que les exécutions précédentes affichaient. Un
compteur de conformité ne surveille pas ce qu'il n'a pas été écrit pour surveiller — ici, le coût
d'initialisation placé devant une course non déterministe.
