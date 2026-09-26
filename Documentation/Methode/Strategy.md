# OpenForesia — Reverse engineering de Blaze

## Objectif général

Nous reconstruisons en C# le comportement d'un ancien exécutable Windows x86 natif.

Le fichier `Blaze.c` est un pseudo-code généré par Ghidra à partir du binaire original.

`Blaze.c` est notre **source de référence immuable**.

Il ne doit JAMAIS être modifié.

Le nouveau code C# doit être écrit dans le projet OpenForesia, sous :

`src/Blaze/`

La reconstruction y est répartie en `Platform/`, `Memory/`, `Engine/` et `Screens/`, tous parts
d'une même classe partielle `Blaze`. Le fichier qui accueille une fonction est déterminé par la
règle de routage — voir « Organisation » plus bas, et `ARCHITECTURE.md` pour la carte complète.

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

## Mode « legacy ++ » (depuis le 2026-09-26)

Jusqu'au 2026-09-26, le livrable était une transcription fidèle, défauts compris (voir
« Historique des règles » en fin de document). **Le projet vise désormais un jeu jouable, autonome
et clé en main** : corriger les bugs du jeu de 1998 et retirer ses dépendances à l'environnement de
1998 fait partie du travail.

Ce qui ne change pas : `Blaze.c` reste immuable et reste la source de vérité de **ce qu'était** le
jeu. Toutes les règles de transcription de ce document restent en vigueur.

### 1. Deux temps, jamais dans le même geste

On **transcrit d'abord**, fidèlement, selon les règles ci-dessous. On **corrige ensuite**, dans un
geste séparé. Un corps jamais transcrit ne naît pas corrigé : sans la version fidèle, personne ne
peut plus dire ce que le correctif a changé.

### 2. Trois familles d'écarts légitimes

| famille | exemples | interrupteur |
|---|---|---|
| **corriger un défaut du jeu de 1998** | plantage, blocage, lecture hors table, calcul faux, drapeau jamais réarmé | non s'il s'agit d'un plantage ou d'un blocage ; **oui** si le joueur voit ou vit le jeu autrement (voir 4) |
| **retirer une dépendance à l'environnement de 1998** | CD-ROM, protection anticopie, DLL d'aide, pilotes, matériel | non |
| **confort du joueur d'aujourd'hui** | fondu au lieu d'un aplat blanc, touche Alt, fenêtre | oui si l'effet visible change |

N'est **pas** légitime sans demande explicite de Vincent : changer le contenu ou l'équilibre du jeu
(dégâts, difficulté, probabilités, textes), ou « embellir » du code transcrit.

### 3. Tout écart est déclaré, à deux endroits

* **Sur place** : l'ancre `// Ghidra: …` est conservée, et un commentaire `// Écart legacy ++
  (LB-NNN) : …` dit ce que faisait l'original et pourquoi on s'en écarte. Un lecteur qui pose
  `Blaze.c` à côté doit voir l'écart en trente secondes, pas le découvrir.
* **Dans `Documentation/Reference/legacy-bug.md`** : une entrée LB avec son statut (`Corrigé`,
  `Contourné`, `Retiré`) et l'emplacement du correctif. Pour un retrait de dépendance, la
  documentation du changement OpenSpec correspondant peut tenir lieu d'entrée, à condition que
  `legacy-bug.md` y renvoie.

### 4. L'interrupteur

Quand un écart change ce que le joueur voit ou vit d'une façon **discutable** — le fidèle a ses
défenseurs —, il passe derrière un booléen nommé (`s_Comfort…`, `s_Fix…`), **à `true` par défaut**,
qui rend le comportement d'origine à `false`. Un plantage corrigé ou une dépendance retirée n'en a
pas : l'original n'y est pas souhaitable.

### 5. Retirer du code

Retirer une dépendance autorise à **supprimer** du code transcrit (le balayage des lecteurs, par
exemple). À la place, une ligne `// Ghidra: <ce qu'il y avait> — retiré (legacy ++, <raison>)`
garde la trace. Hors de ce cas, l'interdiction de supprimer une branche ou un appel reste entière.

### 6. Qui décide

Une correction de défaut s'applique quand **Vincent la demande ou la valide**. L'agent en propose,
en citant l'entrée LB concernée, mais ne corrige pas un défaut d'origine de son propre chef.

### 7. Le produit

Le jeu doit se lancer sans CD, sans DLL de 1998 et sans prérequis à installer. Les **données du
jeu ne sont jamais dans le dépôt ni dans une release** : le joueur fournit son ISO, l'installeur en
extrait ce qu'il faut (`openspec/changes/install-from-iso`).

---

## Sources de vérité

Toujours utiliser `Blaze.c` comme référence principale lors de la reconstruction de Blaze.

Ne jamais modifier `Blaze.c`.

Ne jamais considérer une précédente traduction C# comme plus fiable que `Blaze.c`.

Les DLL utilisées par Blaze ont également été décompilées avec Ghidra.

Ces fichiers décompilés sont des **sources de référence secondaires en lecture seule**.

Ils ne doivent JAMAIS être modifiés.

Ils doivent être consultés lorsqu'un appel externe, un ordinal, une signature native ou un comportement provenant d'une DLL doit être compris.

Lorsqu'une ambiguïté apparaît :

1. revenir au code correspondant dans `Blaze.c`;
2. analyser les appels et les variables environnantes ;
3. si une DLL externe est impliquée, consulter sa version décompilée ;
4. conserver autant que possible la sémantique du pseudo-code Ghidra ;
5. utiliser un `TODO(REVERSE)` si le comportement exact reste impossible à déterminer.

---

## Documentation des DLL

Les connaissances acquises pendant l'analyse des DLL doivent être documentées progressivement dans :

`Documentation/DLL/<nom-de-la-dll>.md`

Cette documentation doit servir de mémoire technique du reverse engineering.

Lorsqu'un export, un ordinal, une fonction ou un comportement d'une DLL est suffisamment compris :

1. vérifier si une documentation existe déjà pour cette DLL ;
2. la créer si nécessaire ;
3. ajouter ou mettre à jour les informations découvertes ;
4. conserver les références utiles vers les noms et adresses générés par Ghidra.

Avant de réanalyser une fonction externe déjà rencontrée, consulter d'abord la documentation de la DLL correspondante.

La documentation doit distinguer les comportements confirmés des hypothèses restant à vérifier.

---

## Point de départ

Commencer exclusivement par la fonction :

`entry`

Analyser son rôle et reconstruire son équivalent C# dans le fichier que la règle de routage désigne — pour `entry`, c'est `src/Blaze/Platform/Startup.cs`.

Ensuite, suivre les fonctions appelées par `entry`.

Procéder progressivement selon le graphe d'appels :

`entry`
→ fonctions directement appelées
→ fonctions appelées par celles-ci
→ etc.

Ne pas commencer à traduire aléatoirement des fonctions éloignées du point d'entrée.

Lorsque l'exécution atteint une fonction encore non reconstruite, reprendre naturellement la reconstruction depuis cette fonction et poursuivre autant que raisonnablement possible dans ses dépendances.

---

## Méthode de reconstruction

Pour chaque fonction Ghidra rencontrée :

1. lire entièrement la fonction dans `Blaze.c`;
2. identifier autant que possible son rôle ;
3. analyser les fonctions qu'elle appelle ;
4. lorsqu'une DLL externe est impliquée, consulter sa décompilation et sa documentation ;
5. analyser les données globales qu'elle utilise ;
6. déterminer les types C# les plus plausibles ;
7. reconstruire son contrôle de flux ;
8. écrire son équivalent dans le fichier que la règle de routage désigne ;
9. compiler le projet ;
10. corriger uniquement les erreurs liées au code nouvellement ajouté ;
11. continuer avec les dépendances nécessaires.

Travailler par groupes cohérents de fonctions.

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

Pour les fonctions provenant d'une DLL décompilée, analyser leur implémentation avant d'inférer leur comportement ou leur signature depuis les seuls call-sites.

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

### Le contrôle d'adjacence, avant de déclarer un symbole isolé

Le **défaut d'aliasing** est le défaut le plus fréquent de ce projet : corrigé seize fois. Il naît
toujours du même geste — séparer ce que le binaire garde ensemble. Deux symboles voisins d'adresse
déclarés séparément ne partagent plus leur mémoire ; une écriture faite par un bout n'est jamais
lue par l'autre. Aucune erreur, aucun plantage : une valeur qui reste à zéro.

Avant de déclarer un `DAT_` isolé, **chercher son adresse dans la carte des symboles reconstruits**,
en fin de `Documentation/Reference/DataSymbols.md` :

* s'il tombe dans l'étendue d'une arène, il est déjà couvert — l'adresser par l'accesseur de
  l'arène, pas par un champ neuf ;
* s'il a un voisin à quelques octets, vérifier que la taille déclarée du voisin ne le recouvre pas ;
* s'il n'a pas de voisin proche, un champ isolé est légitime.

Ce contrôle est devenu indispensable depuis le découpage : **107 paires de symboles voisins
d'adresse vivent maintenant dans deux fichiers différents**. Leur adjacence ne se voit plus dans le
code, seulement dans cette carte.

Un signe qui ne trompe pas : **deux allocations indépendantes à la même adresse**. Le dépôt en
porte encore une, `0x00566c70`, constatée et non corrigée.

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
* modifier les sources décompilées des DLL ;
* réécrire massivement le fichier avec des regex ;
* ajouter des centaines de casts simplement pour compiler ;
* remplacer arbitrairement une valeur inconnue par `0`, `null` ou `false`;
* supprimer une branche parce qu'elle est difficile à traduire ;
* supprimer un appel de fonction inconnu ;
* transformer tous les `goto` en appels de méthodes ;
* inventer une architecture métier qui n'est pas encore visible dans le code ;
* effectuer une refactorisation importante avant d'avoir compris le comportement correspondant ;
* déplacer une globale pour rendre un fichier autonome ;
* éclater une arène entre plusieurs fichiers ;
* déplacer du code dont la reconstruction est en cours.

Ces interdictions portent sur la **transcription**. Un écart « legacy ++ » déclaré selon les règles
de la section du même nom n'en relève pas — mais il ne les contourne pas non plus : on transcrit
d'abord, on s'écarte ensuite.

---

## Compilation

Compiler régulièrement avec :

`dotnet build`

La compilation sert à vérifier la traduction, mais **un code qui compile n'est pas nécessairement correct**.

La fidélité au comportement de `Blaze.c` reste prioritaire.

Lorsqu'une fonction vient d'être traduite, privilégier la correction de cette fonction et de ses dépendances directes avant de continuer.

---

## Organisation

La phase 1 — toute la reconstruction dans `src/Blaze/Blaze.cs` — **s'est achevée le 2026-08-19**.
Le fichier faisait 23 680 lignes ; il est découpé en 30 fichiers.

La carte détaillée est dans **`Documentation/ARCHITECTURE.md`**. À lire avant d'ajouter ou de
déplacer du code. Ce qui suit n'en est que les principes.

### Les deux moitiés du dépôt

`src/Blaze/` est la **transcription**. `src/Host/` est **notre échafaudage** — le remplacement de
la chaîne d'affichage de 1998 par Direct3D 11.

Les deux obéissent à des règles opposées :

| | `src/Blaze/` | `src/Host/` |
|---|---|---|
| Règle | la fidélité prime sur la compilabilité ; un écart « legacy ++ » est déclaré | ça doit **marcher** |
| Source de vérité | `Blaze.c` | aucune |
| Un `TODO(REVERSE)` y a un sens ? | oui | non |
| Refactorisable librement ? | **non** | oui |

C'est la distinction que le chemin d'un fichier doit rendre évidente. Rien d'autre ne dit à un
lecteur laquelle des deux règles s'applique à ce qu'il a sous les yeux.

### Où va une fonction

Quand le rôle d'une `FUN_00XXXXXX` est établi, dans cet ordre :

1. son adresse tombe dans une plage moteur connue → ce fichier de `Engine/` ;
2. un **seul** écran l'appelle → ce fichier de `Screens/StateNN/` ;
3. **plusieurs** écrans l'appellent → promotion en `Engine/`, avec un nom de sous-système ;
4. sinon → **elle reste chez son appelant** jusqu'à ce qu'on sache.

La règle est mesurée, pas choisie : les définitions du moteur sont concentrées à 90–100 % dans une
plage d'adresses, celles des écrans à 38–55 % seulement. Deux populations, deux critères.

Le passage de (2) à (3) est le seul événement qui crée un nouveau fichier moteur. **L'architecture
grandit sur preuve.**

Il n'y a **aucun fichier de rebut**, délibérément. La branche (4) couvre même les corps seulement
adressés : leur « appelant » est le code qui installe leur adresse.

### Le nommage des fichiers

Noms neutres et descriptifs, tout `static`.

Ne jamais employer :

* `Manager`
* `Service`
* `Controller`
* `Provider`

Ces noms promettent un objet avec un cycle de vie que la transcription n'a pas, et invitent à
dériver vers un design que la source de vérité ne contient pas. C'est la même interdiction qu'avant
le découpage, pour la même raison — seule sa formulation change.

### Ce qui n'est pas un critère

**La taille d'un fichier.** `Screens/State06/Inventory.cs` fait près de 3 000 lignes pour trois
sous-écrans d'un même menu : c'est cohérent. La métrique est la cohérence, jamais la taille.

**Le confort d'un fichier.** Voir la règle d'état ci-dessous.

### Ne pas déplacer un chantier en cours

Un chantier terminé se range. Un chantier ouvert reste où il est. Déplacer du code pendant qu'on le
reconstruit revient à retirer l'échafaudage sous quelqu'un.

---

## Organisation — la règle d'état

Le code se range par **responsabilité**. L'état se range par **adresse**. Les deux axes sont
orthogonaux et ne doivent jamais être alignés de force.

D'où la règle :

**Une globale ne se déplace jamais pour rendre un fichier autonome.**

Elle se déplace uniquement quand la carte d'adresses le dit. Les arènes se déplacent **en un seul
bloc**.

Un symbole qui tombe dans l'étendue d'une arène modélisée :

* s'il passe déjà par l'accesseur de l'arène, il **reste avec son code** — il ne peut pas aliaser ;
* s'il a sa **propre allocation**, il part dans le fichier de l'arène et devient un décalage.

La règle protège **le stockage, pas les vues**.

### Un piège propre aux classes partielles

Le découpage en fichiers **n'est pas sémantiquement neutre**.

Les initialiseurs de champs statiques s'exécutent dans l'ordre où le compilateur reçoit les
**fichiers**, ordre qui n'est pas spécifié. Un champ `static readonly` dont l'initialiseur lit un
membre statique vivant dans un **autre** fichier peut donc s'initialiser trop tôt.

Il faut alors en faire une **propriété calculée** `=>`.

Les dépendances à l'intérieur d'un même fichier sont sûres : l'ordre textuel y est garanti.

Ce piège n'est pas théorique : huit champs de la forme `= Arena(0x…)` ont rendu `null + décalage`
au moment du découpage, et le jeu levait une `NullReferenceException` dès la première image.

---

## Définition de terminé pour une fonction

Une fonction peut être considérée comme reconstruite lorsque :

* son équivalent C# est présent ;
* son contrôle de flux est compréhensible ;
* les types utilisés sont raisonnablement cohérents ;
* son rôle est nommé correctement ou marqué comme inconnu ;
* sa provenance Ghidra reste identifiable ;
* ses dépendances externes ont été analysées lorsque nécessaire ;
* elle ne contient pas de modification arbitraire destinée uniquement à compiler ;
* les incertitudes restantes sont indiquées par `TODO(REVERSE)`.

---

## Ce que le refactor n'a pas le droit de faire

Le dépôt porte **1 834 mentions** `FUN_`/`DAT_`/`LAB_`. Sa valeur tient à ce qu'on puisse poser
`Blaze.c` et le C# côte à côte. Le critère d'acceptation d'une transformation n'est donc pas
« est-ce du beau C# ? » mais :

**Peut-on encore vérifier cette fonction contre `Blaze.c` en trente secondes ?**

Trois pratiques par ailleurs saines sont **interdites dans `src/Blaze/`**, parce qu'elles détruisent
cette correspondance :

* **l'extraction de méthode** — une méthode = une `FUN_`. Découper une fonction de 1 300 lignes en
  huit méthodes « propres » casse la correspondance 1:1 qui fait toute la valeur du projet ;
* **le remplacement des conditions par du polymorphisme** — les `switch` sur sous-état **sont** la
  machine à états du binaire ;
* **le renommage sans conserver l'ancre** `// Ghidra: FUN_00XXXXXX`.

Dans `src/Host/` (notre échafaudage, une seule mention Ghidra sur 3 477 lignes), les trois sont
autorisées.

### Changer la forme, pas la logique

Un refactor qui déplace ou transforme du code doit pouvoir le **prouver**. Deux outils s'en
chargent, dans `OpenForesia.Tools/` :

* `verify-split.ps1` compare **octet à octet** contre une référence figée. Il vaut pour un
  déplacement pur ;
* `compare-bodies.ps1` compare le **corps** de chaque méthode en normalisant ce que le refactor a
  le droit de changer — `static`, les modificateurs d'accès, les qualifications ajoutées,
  l'indentation. Il vaut dès que la forme change.

Un outil de vérification doit lui-même être vérifié : `compare-bodies.ps1 -SelfTest` prouve qu'il
ignore bien un changement de forme et attrape bien un changement de logique.

### Ce que ces deux outils ne voient pas

Ils comparent du **texte**. Ils ne voient donc rien de ce qui relève du *moment* où le code
s'exécute : ordre d'initialisation, coût placé devant une course, allocation déplacée d'une phase à
une autre. Le 2026-08-19, la conversion de `Engine/Video.cs` a fait passer la course pour la
mémoire basse de 25 % à 62 % d'échec sans que l'oracle, le build, les tests de disposition ni le
débit par image ne bougent d'un iota. Tous verts.

Ce que la vérification par oracle établit, c'est : « la logique écrite est la même ». Pas : « le
programme se comporte pareil ». Pour cette seconde question il n'y a que l'exécution répétée — et
il faut la mesurer sur assez d'essais quand le comportement est non déterministe (ici : 8 essais
n'auraient pas suffi à conclure, 16 ont donné les 25 % attendus).

### Le composition root est devant une course

`TryPrepareDisplayListArena()` est appelée par un `[ModuleInitializer]`, et son premier geste lit un
champ statique de `Blaze` : cela déclenche **toute** l'initialisation statique de `Blaze` avant que
l'arène basse ne soit réclamée. Tout ce qui est construit à ce moment-là est du travail placé devant
la course.

D'où la règle : **dans `Subsystems.cs`, `Mem` est immédiat, les sous-systèmes sont paresseux.** `Mem`
l'est parce que la réclamation le lit vraiment (`s_UiDescriptors_00560ff0`,
`s_StatusDescriptor_00560728`, qui portent leur ordering table masquée sur 24 bits). Les autres, non
— rien dans le chemin de réclamation ne les touche.

Cette paresse **ne contredit pas** la leçon « pris paresseusement, le descripteur échouait » :
celle-ci porte sur les descripteurs de mémoire basse, pas sur les objets de sous-système.

---

## Historique des règles

Les règles remplacées ne sont pas effacées : elles expliquent pourquoi le code existant a la forme
qu'il a, et pourquoi `legacy-bug.md` décrit des défauts reproduits plutôt que réparés.

### Phase fidèle (du début du projet au 2026-09-26)

**Le livrable.** « Il n'y a pas de produit à livrer : le livrable est une transcription C# fidèle
et compréhensible du pseudo-code Ghidra, construite fonction par fonction le long du graphe
d'appels. » (`CLAUDE.md`)

**Les défauts d'origine.** « Ces bugs sont reproduits tels quels. La fidélité prime, et un
correctif serait une déviation à déclarer explicitement. Chaque entrée note tout de même où
porterait le correctif, si l'envie venait un jour d'un mode « corrigé ». » (`legacy-bug.md`)

**Avant de corriger une bizarrerie.** « Elle peut être une transcription fidèle d'un défaut
d'origine, et le projet reproduit ceux-là plutôt que de les réparer. » (`CLAUDE.md`)

**Les exceptions de l'époque.** Seul l'hôte (`src/Host/`) échappait à la fidélité. Trois écarts de
confort ont été admis à la fin de cette phase, tous derrière un interrupteur et déclarés : les
stroboscopes des boss (LB-030, LB-051) et le voile blanc opaque (LB-052). Ils ont servi de modèle
aux règles « legacy ++ ».

**Ce qui a motivé le changement.** Le 2026-09-26, Vincent décide de rendre le dépôt public et de
publier un installeur : le projet doit devenir un jeu qu'on installe et qu'on joue. Le premier
chantier du nouveau mode est l'abandon du CD (`openspec/changes/install-from-iso`).
