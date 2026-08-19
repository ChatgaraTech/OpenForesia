> **Ne pas commit.** Seul l'utilisateur commit, même après une tâche terminée.
>
> **Jamais `Get-Content` / `Set-Content`** sur les sources : la région à encodage double (868 lignes)
> a déjà été aggravée une fois par là. Utiliser `[IO.File]::ReadAllBytes` / `WriteAllBytes` ou les
> outils d'édition.
>
> **Le SDK et node ne sont pas dans le `PATH` de l'agent.**
> `$env:PATH = "C:\Program Files (x86)\dotnet;D:\Logiciels\nodejs;$env:PATH"`
>
> **Le critère d'acceptation de chaque étape** : peut-on encore vérifier cette fonction contre
> `Blaze.c` en trente secondes ? Voir D7 — pas d'extraction de méthode, pas de polymorphisme à la
> place d'un `switch`, pas de renommage sans l'ancre `// Ghidra:`.

## 0. L'oracle — avant de transformer quoi que ce soit

- [x] 0.1 Écrire `OpenForesia.Tools/compare-bodies.ps1` : extrait le corps de chaque méthode de
      premier niveau d'un fichier, le **normalise** (retire `static`, les modificateurs d'accès, les
      qualifications ajoutées du type `mem.`/`gfx.`, l'indentation) et le compare à une référence.
      ASCII pur, sans BOM, lecture/écriture en ISO-8859-1 — mêmes contraintes que `verify-split.ps1`.
      → **Les corps sont délimités par l'INDENTATION** (`    {` .. `    }`), pas par comptage
      d'accolades : 66 chaînes du dépôt contiennent des accolades et perdraient un compteur.
      398 blocs + 153 corps-expression. **La clé est le nom de méthode seul, pas fichier+nom** —
      une méthode qui déménage vers une nouvelle classe est le geste normal de ce refactor et ne
      doit pas compter comme une différence. Vérifié : **0 homonyme** sur les 509 corps, donc
      aucune clé n'est fragile au déménagement.
- [x] 0.2 **Vérifier l'oracle lui-même**, comme on l'a fait pour `verify-split.ps1` : un fichier
      inchangé passe ; un `static` retiré passe ; une qualification ajoutée passe ; **une instruction
      changée échoue**, avec la ligne exacte. Sans ce contrôle, l'oracle ne vaut rien.
      → `-SelfTest` : **9 cas, 9 réussis**. Inchangé, `static` retiré, qualification ajoutée,
      commentaire ajouté, indentation changée → identiques ; instruction changée, instruction
      supprimée, corps-expression modifié → détectés.
      **Puis validé sur du vrai code** (`Platform/Startup.cs`) : retirer `static` et ajouter une
      qualification `mem.` passe ; modifier une longueur d'allocation est attrapé dans les deux
      méthodes concernées, avec la ligne de corps et les deux versions. Fichier restauré, l'arbre
      repasse.
- [x] 0.3 Figer la référence des corps sur l'état actuel de `src/Blaze/`, à la manière de
      `blaze-baseline.txt`. **Attention** : elle n'existe qu'en dehors de git tant que le changement
      n'est pas commité — elle a été détruite une fois au changement précédent.
      → `OpenForesia.Tools/bodies-baseline.txt`, **509 corps de méthode** (arbre `OpenForesia/src`
      entier, `Host/` compris).

## 1. A — `GameMemory`

- [x] 1.1 Relever l'inventaire exact des 99 membres des quatre fichiers de `Memory/` : nom, type,
      arène d'appartenance ou symbole isolé, adresse. Source : la carte des symboles reconstruits en
      fin de `Documentation/DataSymbols.md`.
      → **`Memory/` contenait du comportement**, dont la boucle de démarrage. `RunMainLoop()`
      (`FUN_00529c10`, 97 l.) et `LoadUiFontFromCd()` (31 l.) y avaient atterri par un point de
      coupe trop large au changement précédent. Déplacées vers `Platform/Startup.cs`, aux côtés de
      `RunApplication` qui est leur appelant. **Ça contredit la règle de routage (1)** — le VA
      `0x00529c10` tombe dans la plage de `Engine/Resources.cs` — et la règle a tort ici : la
      proximité d'adresse est un artefact de l'éditeur de liens sur le code de démarrage. Validé
      par l'utilisateur.
      En revanche `s_CdDataRootPath`, `s_CdCheckFileSize_0056321c`, `s_RandSeed_0056e9c0` et
      `s_DebugModeLatch_005633a8` **restent** : ce sont des globales `.data` du binaire.
- [x] 1.2 Créer `sealed class GameMemory` : les 8 blocs adressés par décalage, les tables `.data`
      recouvrées, les globales isolées, en **champs d'instance**. Les allocations natives se font
      dans le constructeur, dans l'ordre où les fichiers actuels les font.
      → `Memory/GameMemory.cs`, **97 membres** (79 d'instance + 18 `const`), fusionnant
      `Arena.GameState.cs`, `Arenas.cs` et `DataTables.cs`, désormais supprimés.
      Trois ajustements imposés par le compilateur, tous consignés dans le fichier :
      **(a)** les 4 vues `s_DrawEnv*/s_DispEnv*` deviennent des propriétés `=>` — un initialiseur
      de champ ne peut pas lire un membre d'instance ;
      **(b)** les 3 tables construites par appel (`BuildSelectSubStateBytes`,
      `BuildLabelDescriptor005606f8`, `BuildSceneToneTable`) sont montées dans le **constructeur** —
      en faire des propriétés `=>` reconstruirait la table à chaque accès, ce qui serait un
      changement de comportement et non de forme ;
      **(c)** `s_PanelSelection_005e9060` est rapatrié depuis `Engine/UiWindow.cs` : `0x005e9060`
      tombe dans l'étendue de `GameArena`, c'est donc une **vue d'arène**, pas un stockage propre.
      `AllocLowZeroed` passe `internal` pour rester joignable.
- [x] 1.3 ~~Le reste de `src/Blaze/` accède par un pont statique temporaire.~~ →
      **Le pont est abandonné, et c'est une amélioration.** Mesuré : **595 lignes hors `Memory/`**
      référencent les 94 membres, en 642 occurrences. Or **l'oracle normalise justement les
      qualifications** : écrire `Mem.X` au site d'appel lui est **invisible**, alors que le pont
      aurait changé les 94 corps d'accesseurs et déclenché 94 signalements légitimes.
      Le pont était donc pire sur les deux tableaux — architecture et vérifiabilité. On qualifie
      directement, **et la liste blanche de 95 entrées n'a plus lieu d'être**.
      Vérifié : PowerShell retire bien `Mem.` (insensible à la casse) sans toucher à `GameMem.`,
      grâce au retour arrière négatif du motif.
      Deux qualifications, selon la nature du membre : `Mem.X` pour les membres d'instance,
      `GameMemory.X` pour les **18 `const`** — une constante ne s'accède pas via une instance en C#.
      Vérifié qu'aucune des 18 n'est utilisée en position exigeant une constante (`case`, taille de
      tableau, attribut), donc elles peuvent rester `const` plutôt que devenir des champs
      `readonly` : plus fidèle à une adresse qui ne change jamais.
- [x] 1.4 Build + oracle 0.1 + `playtest.ps1` sur les trois écrans de référence, captures comparées.
      **L'ordre d'initialisation est le point de vigilance** : le changement précédent a fait planter
      le jeu là-dessus. Un constructeur supprime le risque, mais la conversion doit être surveillée.
      → **Build 0 erreur. Oracle : « Aucune logique modifiée ».** C'est la preuve recherchée :
      déplacer 97 membres dans une nouvelle classe et qualifier 675 lignes n'a touché aucun corps
      de méthode. La seule méthode nouvelle est le constructeur `GameMemory()`.
      **382,3 et 387 primitives par image**, captures identiques aux références.
      L'ordre d'initialisation n'a rien cassé, comme prévu : un constructeur s'exécute à un moment
      déterminé, contrairement aux initialiseurs de champs d'une classe partielle.

## 2. A′ — les premiers tests du dépôt

- [x] 2.1 Créer `OpenForesia.Tests` (xUnit), ajouté à la solution. **Aucun test de comportement** :
      l'oracle de la logique transcrite reste l'écran.
- [x] 2.2 Test 1 — **tout symbole tombe dans l'étendue de l'arène qui le couvre.**
- [x] 2.3 Test 2 — **aucune allocation indépendante à une adresse déjà couverte par une arène.**
      Doit **échouer** sur les quatre cas connus : `s_EntityList_005de558`, `s_ModelResultB_005f7b40`,
      `s_ModelResultA_005fa588`, `s_CameraMatrix_006008e0`.
- [x] 2.4 Test 3 — **la taille déclarée d'un symbole ne déborde pas sur son voisin d'adresse.**
- [x] 2.5 Test 4 — **deux symboles à la même adresse partagent bien leur mémoire.**
      Doit **échouer** sur `0x00566c70` (`s_MenuRenderBlock` / `s_ViewRect`, deux allocations
      indépendantes) et passer sur `0x005e9180` et `0x00562c9c`, qui sont bénins.
- [x] 2.6 **Documenter les échecs attendus** — dans le projet de tests et dans `REVERSE_STATUS.md`.
      Un test qui passerait tout de suite ne prouverait rien ; ceux-ci démarrent en échec parce
      qu'ils détectent des défauts réels, dont la correction relève d'un changement séparé.
      → `OpenForesia.Tests/README.md` + section datée dans `REVERSE_STATUS.md`.

> **Résultat du groupe 2 : 5 tests, 2 verts, 3 rouges — et les 3 rouges sont des défauts réels.**
>
> Les deux échecs **attendus** tombent exactement sur les symboles prédits au découpage : les
> 4 allocations logées dans une arène, et `0x00566c70`.
>
> **Le troisième n'était pas attendu, et c'est la vraie prise.** L'assertion sur les tailles
> déclarées relève **deux recouvrements que personne n'avait vus** :
> `s_MenuScreenBlock_00566c48` déclare `0x40` pour un écart de `0x28` (recouvrement 0x18, deux
> lignes voisines du même fichier), et `s_CardPathDescriptor_00604940` déclare `0x100` pour un
> écart de `0x4c` — il **avale entièrement** `s_CardIconFrameCount_0060498c`, et les deux vivent
> dans des fichiers différents.
>
> Les seize défauts d'aliasing précédents avaient tous été trouvés **à l'écran, après coup**.
> Ceux-ci ont été trouvés **en une seconde, sans lancer le jeu**. C'est précisément ce que ce
> groupe devait démontrer.
>
> **Choix technique à noter** : les tests lisent les **sources**, pas l'exécution. Le défaut visé
> est un défaut de déclaration, donc entièrement visible dans le texte ; et réfléchir sur les
> statiques de `Blaze` déclencherait `AllocLowZeroed`, qui dispute une fenêtre mémoire basse et
> échoue par intermittence — des tests flakies pour une raison sans rapport n'auraient rien valu.

## 3. B — l'interop derrière une interface, là où un double existe

> **3.4 a été faite EN PREMIER, et elle annule 3.1 et 3.2.**
>
> La tâche 3.4 demandait de vérifier que `SyntheticDevice` est bien une seconde implémentation
> d'`IWinsys3d` — l'argument qui **justifie** l'interface. Vérification faite : **c'est faux.**
>
> `SyntheticDevice` détourne `GetProcAddress` **dans la table d'imports de winsys3d**, pour
> remplacer `3dhelp.dll::Create3DDevice`. Il substitue donc le **device 3D de 3dhelp**, jamais
> winsys3d. `winsys3d.dll` est toujours la vraie DLL, appelée par **173 `Ordinal_NNN(...)`
> directs**, tous des `static extern`.
>
> La couture réellement substituable est **entre winsys3d et 3dhelp**, et elle est **déjà traitée** —
> par `SyntheticDevice` et `DeviceShim`, qui sont déjà de vraies classes dans `src/Host/`.
>
> Créer `IWinsys3d` serait donc exactement l'« indirection sans second implémenteur » que **D2
> interdit**. Le faire au nom de D2 aurait été violer D2. Même raisonnement pour `IWsyskern`.
>
> **Ce que le groupe 3 devient** : pas d'interface, mais l'extraction des 160 P/Invoke de
> `src/Blaze` hors de `partial class Blaze`, en vraies classes statiques par DLL. C'est un gain réel
> et justifié — au site d'appel, `Winsys3d.Ordinal_688(…)` dit ce que `Ordinal_688(…)` taisait.

- [x] 3.1 ~~Extraire `IWinsys3d` des 85 P/Invoke de `winsys3d.dll`~~ → **annulée**, voir ci-dessus.
      Aucun second implémenteur ; l'interface serait de l'indirection pure.
- [x] 3.2 ~~Extraire `IWsyskern` des 9 P/Invoke de `wsyskern.dll`.~~ → **annulée**, même raison.
- [x] 3.3 Regrouper les P/Invoke en classes statiques **sans interface** — aucun second
      implémenteur, voir D2.
      → **160 déclarations sorties de `partial class Blaze`** vers `src/Blaze/Interop/` :
      `Winsys3d.cs` (85), `Win32.cs` (66, huit DLL système), `Wsyskern.cs` (9).
      **381 lignes qualifiées** dans 22 fichiers ; les commentaires d'ordinal partent avec leur
      déclaration — ils sont la trace vers `winsys3d.c`, au même titre qu'un `// Ghidra:` l'est vers
      `Blaze.c`.
      Relevé au passage : **`Platform/Win32.cs` contenait 42 P/Invoke `winsys3d.dll`** — le fichier
      était mal nommé pour son contenu depuis le découpage.
      Cinq structures d'interop (`STARTUPINFOA`, `WNDCLASSA`, `MSG`,
      `MEMORY_BASIC_INFORMATION`, `TimImage`) passent `internal` et sont qualifiées `Blaze.X` :
      elles restent chez `Blaze`, à côté de leur documentation.
- [x] 3.4 Vérifier que `SyntheticDevice` se pose bien comme seconde implémentation d'`IWinsys3d`,
      ou consigner pourquoi non. C'est l'argument qui justifie l'interface ; s'il ne tient pas,
      l'interface ne se justifie plus.
      → **Faite en premier, et elle a annulé 3.1 et 3.2.** Voir l'encadré ci-dessus et `design.md`,
      D2. L'argument ne tient pas : `SyntheticDevice` substitue le device 3D de `3dhelp`, jamais
      `winsys3d`.
- [x] 3.5 Build + oracle + `playtest.ps1`.
      → Build **0 erreur**. Oracle : **« aucune logique modifiée »**. **382,3 et 387** primitives par
      image, captures identiques. Les tests de disposition tiennent : toujours 3 échecs / 2
      réussites, les mêmes trois défauts réels — le garde-fou `L_analyseur_releve_bien_la_disposition`
      confirme que l'analyse voit encore la carte malgré le déplacement de 160 déclarations.
      **Deux pièges payés à blanc**, tous deux consignés en tête des scripts :
      **(a)** la fin d'une déclaration ne se teste pas par `';\s*$'` — la plupart de ces lignes
      finissent par un commentaire (`…); // real: winsys3d.c FUN_00a053e0`), donc la consommation
      avalait les déclarations suivantes : 110 déplacées au lieu de 160, et des fichiers mutilés si
      on avait écrit ;
      **(b)** l'analyse des collisions de noms est **par fichier**, pas par répertoire.
      `D3D11Window.cs` déclare ses propres `GetModuleHandleA`, `ShowWindow`… tandis que
      `DeviceShim.cs` et `SyntheticDevice.cs` les empruntaient à `Blaze`. Traiter `Host/` en bloc
      aurait laissé quatre appels non résolus.

## 4. E — composition root et `IDisposable`

- [x] 4.1 **Mesurer d'abord** : les 10 rappels `[UnmanagedCallersOnly]` de `src/Blaze/`
      (`SaveData` 8, `Input` 1, `Video` 1) touchent-ils de l'état de jeu, ou ne relaient-ils que des
      handles ? Si c'est la seconde réponse, le pont statique de D4 est inutile — ne pas l'écrire.
- [x] 4.2 Écrire le composition root dans `Program.cs` : instancie `Win32`, `Winsys3d`,
      `GameMemory`, et pose le pont statique **si et seulement si** 4.1 le justifie.
- [x] 4.3 `IDisposable` sur `src/Host/` **uniquement** — D3D11, la fenêtre, la DIB section.
      **Ne rien ajouter dans `src/Blaze/`** : la séquence d'extinction transcrite
      (`ShutdownStage1/3/4/5`) existe déjà et est fidèle ; un second mécanisme la concurrencerait.
- [x] 4.4 Build + oracle + `playtest.ps1` sur **tous** les écrans atteignables, comme au changement
      précédent : écran-titre, sélection des personnages, menu objets, liste des sauvegardes, liste
      d'objets, options.

> **Résultat du groupe 4 : les trois tâches ont été mesurées, et aucune n'a été écrite.**
>
> **4.1 — le pont statique n'est pas nécessaire.** `CountJoystickCallback` utilise son paramètre
> `context` (`(*(int*)context)++`) et ne touche aucun statique. Les huit gestionnaires de carte
> écrivent deux `private static` de `Engine/SaveData.cs`, qui restent statiques. Tout ce que les
> rappels touchent est joignable statiquement aujourd'hui, `Blaze.Mem` compris. La question ne se
> posera qu'à l'étape C.
>
> **4.2 — le composition root a été écrit, a fait planter le jeu, et a été reverté.** La
> réclamation de l'arène de liste d'affichage, dans le module initializer, touche **délibérément**
> `Mem.s_UiDescriptors_00560ff0` et `Mem.s_StatusDescriptor_00560728` : ils portent leur ordering
> table à `+0xa4`, masquée sur 24 bits, donc ils doivent être pris sous `0x01000000` **au même
> instant précoce que l'arène** — mesuré, pris paresseusement « l'arène réussissait et le
> descripteur échouait ensuite ». `GameMemory` doit donc naître **pendant** l'initialisation
> statique de `Blaze`.
> **Ma justification initiale était fausse** : j'avais avancé que les 16 allocations précédant la
> réclamation étaient une régression du refactor. Non — avant lui, ces mêmes allocations étaient
> déjà des initialiseurs de champ de `Blaze`. Rien n'avait changé.
> Un composition root qui ne composerait rien serait un rituel vide. `Program.cs` porte la
> contrainte en commentaire pour qu'elle ne soit pas redécouverte à l'étape C.
>
> **4.3 — `IDisposable` n'est ni implémentable, ni nécessaire.** Les cinq classes de `Host/` sont
> `internal static` : une classe statique ne peut pas implémenter une interface. Et la libération
> existe déjà sous une forme mieux adaptée — `StartupSurface.Retire()` et
> `D3D11Window.ReleaseLegacyDisplay()` sont appelées **en cours de partie**, quand le chemin
> Direct3D 11 prend le relais : un événement de cycle de vie que `Dispose()` ne modélise pas.
> Relevé au passage : `D3D11Backend.Shutdown()` n'est appelé que par l'autotest, jamais à la sortie.
> **C'est correct** — le système reclame GPU, fenêtre et DIB section à la fin du processus.
>
> **4.4 — vérification** : build 0 erreur, oracle « aucune logique modifiée », **382,3 et 387**
> primitives par image, tests de disposition inchangés (3 rouges / 2 verts).

## 5. Clôture du socle

- [x] 5.1 `openspec validate extract-real-classes-from-partial`.
- [x] 5.2 Mettre à jour `ARCHITECTURE.md` : les trois couches (interop / mémoire / comportement), le
      critère d'interface (« un double existe déjà »), et le fait que `GameMemory` est un objet
      unique **par fidélité**, pas par facilité.
- [x] 5.3 Mettre à jour `Strategy.md` et `Prompt.md` : la règle « une méthode = une `FUN_` » et les
      trois pratiques interdites de D7 y ont leur place, dans les sections déjà écrites au changement
      précédent.
- [x] 5.4 Consigner dans `REVERSE_STATUS.md` une section datée : ce que le socle a changé, ce que les
      tests de disposition ont révélé, et l'état des défauts connus.
- [ ] 5.5 **Faire le point avec l'utilisateur avant d'engager C.** Le socle a de la valeur seul ; C
      touche ~7 700 lignes de transcription et D ~13 800. La décision de continuer se prend sur
      pièces, pas d'avance.
      → **Le socle est livré et vérifié. En attente de décision.**

> **Relevé au titre de 5.2/5.4** : la carte des symboles de `Documentation/DataSymbols.md` pointait
> encore vers `Arena.GameState.cs`, `Arenas.cs` et `DataTables.cs`, supprimés à l'étape A. Or son
> objet même est de dire **où** chaque symbole est modélisé. Colonne fichier régénérée depuis
> l'arbre courant pour les 398 symboles, plus les 8 lignes du tableau des arènes. Zéro référence
> périmée.

## 6. Esquissé — hors de ce changement

> Volontairement non détaillé : planifier C avant d'avoir fait A serait de la planification en
> aveugle.
>
> **C — `Engine/` en 9 classes d'instance** (~7 700 l.), **fichier par fichier**, oracle à chaque
> fichier. Ordre suggéré, du plus fermé au moins fermé : `Audio` (29 réf. d'état sortantes),
> `UiText` (31), `Resources` (31), `Video` (25), `Input` (29), `SaveData` (18), `Model3d` (43),
> `UiWindow` (25), `Primitives2d` (14).
>
> **D — `Screens/` en 12 classes** (~13 800 l.), **en dernier**, quand les écrans cesseront de
> bouger. `Screens/State03/WorldMap.cs` est le front actif de la priorité 2 : on n'y touche pas tant
> que `FUN_00533940` n'est pas terminée.
