> **Ne pas commit.** Seul l'utilisateur commit, même après une tâche terminée.
>
> **Jamais `Get-Content` / `Set-Content`** sur `Blaze.cs` ou ses descendants : la région à encodage
> double (871 lignes) a déjà été aggravée une fois par là. Utiliser `[IO.File]::ReadAllBytes` /
> `WriteAllBytes` ou les outils d'édition.
>
> **Le SDK et node ne sont pas dans le `PATH` de l'agent.**
> `$env:PATH = "C:\Program Files (x86)\dotnet;D:\Logiciels\nodejs;$env:PATH"`

## 1. Inventaire et outillage — rien ne bouge encore

- [x] 1.1 Générer l'inventaire des fonctions de `Blaze.cs` : nom C#, adresse virtuelle Ghidra
      (depuis le commentaire `// Ghidra:`), ligne de début, ligne de fin, bandeau courant, fichier
      cible proposé. Sortie dans le scratchpad, format tableau relisible.
      → `inventory.awk`, `inventaire.tsv`, `inventaire.md`. **1 257 membres : 600 fonctions**
      (85 P/Invoke + 515 reconstruites), 640 champs, 17 types imbriqués ; 23 647 lignes couvertes.
      **Le chiffre de « 623 fonctions » des artefacts était faux** — il comptait les champs dont
      l'initialiseur appelle une fonction. Corrigé dans `proposal.md` et `design.md`.
- [x] 1.2 Générer l'index des globales `s_` : nom, adresse virtuelle si le suffixe en porte une,
      fichier cible, et **tri par adresse croissante**. Marquer celles qui tombent dans une arène
      existante.
      → `globals.awk`, `globales.tsv`, `globales_ecarts.tsv`, `globales.md`. **398 déclarateurs sur
      375 adresses**, dont **23 adresses portant plusieurs symboles** et surtout **107 paires
      adjacentes (≤ 64 octets) atterrissant dans deux fichiers différents** — la justification
      chiffrée de D2 et D7.
- [x] 1.3 Croiser 1.1 et 1.2 avec les 37 bandeaux et **lister tous les écarts** — les trois déjà
      connus (`L9018`, `L11696`, `L20172`) plus ceux que l'inventaire révèle.
      → `ecarts.md`. **Aucune quatrième dérive** : le taux reste 3 sur 37. 64 fonctions à VA hors
      plage, dont **14 à déplacer réellement**. Une modification de conception est requise (D2 doit
      couvrir les scalaires d'arène) — à trancher en 1.4.
- [x] 1.4 **Faire valider l'inventaire par l'utilisateur.** Aucune ligne ne bouge avant cet accord.
      → Validé. Trois décisions prises : **(1)** la règle d'étendue d'arène est adoptée, affinée en
      « protéger le stockage, pas les vues » après mesure (61 vues sans risque, 11 allocations
      propres dont 4 suspects réels) et écrite dans D2 ; **(2)** les 14 fonctions à VA hors plage
      sont redirigées, table dans D5 ; **(3)** les adresses à double déclaration sont constatées et
      non corrigées — une seule est un vrai défaut (`0x00566c70`, deux allocations indépendantes),
      les deux autres sont un doublon de nom et deux constantes identiques. Consignées en 8.3.
      `Memory/` gagne `Arenas.cs` : les sept blocs adressés par décalage autres que `GameArena`.
- [x] 1.5 Écrire le script de vérification : extrait une plage de lignes de `Blaze.cs` dans
      `git HEAD`, la compare octet à octet au corps du fichier cible, et signale toute différence
      autre que le préambule (`using`, `namespace`, déclaration de classe, accolades).
      → `OpenForesia.Tools/verify-split.ps1` + `split-manifest.tsv` (54 plages contiguës) +
      `blaze-baseline.txt` (1 203 908 octets, 23 673 lignes, figé avant tout déplacement).
      Référence figée depuis le fichier de travail plutôt que `git show` : ce dernier passe par un
      décodage texte, ce qui est inacceptable ici. Tout le script lit en **ISO-8859-1** (un octet ↔
      un caractère), donc la comparaison de lignes reste une comparaison d'octets malgré la région
      à encodage double. **Script en ASCII pur, sans BOM**, comme `playtest.ps1` : PowerShell 5.1
      lit les `.ps1` en ANSI et un tiret cadratin UTF-8 casse l'analyseur.
      **Vérificateur lui-même testé** : fichier conforme → OK ; une lettre changée → détectée avec
      la ligne exacte ; une ligne supprimée → détectée. Contrôle 1 passe déjà : les 23 647 lignes
      sont revendiquées une fois et une seule, sans trou ni doublon.
- [x] 1.6 Capturer la référence d'avant-découpage : `playtest.ps1` sur le menu principal, la
      sélection de personnages et le menu objets. Conserver les captures — ce sont elles qui servent
      de comparaison, pas les compteurs de primitives.
      → `scratchpad/reference-avant-decoupage/` : trois PNG + `VERDICTS.txt`. Build propre
      (0 erreur), aucun plantage sur les trois. **Sélection des personnages : 382,3 par image**,
      concordant avec le repère historique (~382) ; **menu objets : 387**.
      Appris au passage, consigné dans `test-protocol.md` : `-Entry -1` reste sur l'**écran-titre**
      (image identique à 3 s et à 25 s, donc pas un cycle d'attract mal attrapé) et n'y produit
      **aucun bilan `[SHIM]`** — le régime titre passe par `StartupSurface` en GDI et n'émet aucune
      primitive 3D. Ce n'est pas un échec, et pas un effet du device synthétique. Pour cet écran, la
      capture est le seul repère.

## 2. `src/Host/` — l'extraction de l'échafaudage

- [x] 2.1 Créer `src/Host/` et y déplacer `D3D11Backend.cs`, `D3D11Window.cs`, `StartupSurface.cs`
      sans modification. Build.
- [x] 2.2 Déplacer `SyntheticDevice.cs` et le sortir de `partial class Blaze` :
      `internal static unsafe class SyntheticDevice`.
- [x] 2.3 Déplacer `DeviceShim.cs` et le sortir de `partial class Blaze` :
      `internal static unsafe class DeviceShim`.
- [x] 2.4 ~~Migrer la déclaration de `s_WindowedStartup` de `Blaze.cs` vers `Host/`~~ →
      **décision inversée par la mesure.** Le symbole a **dix sites de lecture, dont huit dans
      `Blaze.cs`** (niveau de coopération DirectDraw, les quatre sites `VfwClass`) et deux seulement
      dans `Host/`. Le migrer aurait fait lire `Blaze` depuis `Host`, soit l'inverse exact de la
      dépendance voulue. Il **reste dans `Blaze.cs`** et passe `internal`. `design.md` D4 est à
      corriger sur ce point.
- [x] 2.5 Passer `ToAnsiZ` et `RenderSizeFromSizeSetting` en `internal`. ~~Vérifier qu'il n'existe
      aucun autre appel de `Host` vers `Blaze` : la dépendance doit être à sens unique.~~ →
      **la dépendance n'est pas à sens unique, et ne peut pas l'être.** Le compilateur a donné la
      vraie surface, 25 symboles en trois groupes :
      **(a) Host → Blaze, 5** — `ToAnsiZ`, `RenderSizeFromSizeSetting`, `s_WindowedStartup`,
      `GetModuleHandleA`, `VirtualAlloc`, passés `internal` et importés par `using static`.
      **(b) Blaze → Host, 4** — `InstallSyntheticDevice`, `InstallDeviceShim`, `CreateGameWindow`,
      `LogDisplayMode`. **Inhérent** : une façade s'installe depuis le chemin qu'elle détourne.
      Qualifiés explicitement au site d'appel (`DeviceShim.LogDisplayMode(…)`) pour que la couture
      soit visible.
      **(c) Host ↔ Host, 16** — les thunks `Shim_*`, `Guarded`, `DeviceVtableSlots`,
      `s_SyntheticDevice` : ils étaient une seule classe partielle, `using static` préserve la
      lecture. Seul `s_SyntheticDevice` va de `DeviceShim` vers `SyntheticDevice`, qualifié à son
      unique site.
      La bonne propriété n'est donc pas « sens unique » mais **« couture étroite »** : les 4 appels
      de `Blaze` vers `Host` sont tous des points d'installation, pas du couplage diffus.
- [x] 2.6 Supprimer le contournement en propriété calculée de `s_UseSyntheticDevice` devenu inutile
      — l'ordre d'initialisation est désormais garanti au sein de la classe.
- [x] 2.7 Vérification : `dotnet build`, `playtest.ps1` sur les trois écrans de 1.6, comparaison des
      captures. Le rendu doit être **identique**, pas seulement plausible.

## 3. `src/Blaze/Platform/`

> **Deux corrections d'outillage, trouvées avant le premier déplacement.**
>
> **(a) La trivia de tête appartient au membre qui SUIT.** Les plages partaient de la ligne de
> déclaration, donc le bandeau et le commentaire de doc qui *introduisent* un membre tombaient dans
> la plage du membre précédent. Au bord d'un fichier c'est faux : le bandeau `Winsys3d` (L242‑261)
> serait parti dans `Win32.cs`, et les lignes 17‑26 n'étaient revendiquées par personne.
> `inventory.awk` remonte désormais sur le bloc contigu de lignes vides, commentaires et attributs.
> Les recherches de VA, de coupe et de redirection restent branchées sur la **ligne de déclaration**,
> pas sur le début de plage. Après correction : **23 664 lignes revendiquées + 16 de préambule =
> 23 680**, soit la référence exactement.
>
> **(b) `apply-split.ps1` redérive toujours depuis la référence figée.** Supprimer des lignes de
> `Blaze.cs` décalerait la numérotation et périmerait le manifeste à chaque étape. L'outil
> reconstruit donc l'état complet à chaque appel : chaque fichier extrait depuis ses plages de la
> référence, et `Blaze.cs` = préambule + toutes les plages non encore parties. Aucune édition en
> place, donc aucune dérive possible.

- [x] 3.1 `Platform/Win32.cs` (226 l.) — interop OS. Build + script de vérification 1.5.
- [x] 3.2 `Platform/Crt.cs` (372 l.) — runtime MSVC 6. Build + vérification.
- [x] 3.3 `Platform/Winsys3d.cs` (668 l.) — file de caractères tapés, arène de liste d'affichage,
      banc de registres GTE. Build + vérification.
- [x] 3.4 `Platform/Startup.cs` (496 l.) — `entry`, `RunApplication`, `RunMainLoop`, **plus
      l'extinction** (`DestroyJoystickDevice`, `ShutdownStage1/3/4/5`) que le bandeau « Not yet
      reconstructed » masquait. Build + vérification.
- [x] 3.5 `playtest.ps1` sur les trois écrans de référence, captures comparées.
      → Aucun plantage. **382,3 et 387 par image**, identiques à la référence ; captures identiques.
      `Blaze.cs` passe de 23 680 à **21 858 lignes**. Les quatre fichiers `Platform/` sont vérifiés
      octet à octet (`Win32` 225, `Crt` 378, `Winsys3d` 722, `Startup` 497 lignes de corps).
      Au passage : un comptage par `Get-Content` a rendu 19 221 lignes au lieu de 21 858 — il casse
      sur la région à encodage double. Démonstration directe de l'utilité de la règle en tête de
      fichier ; tout comptage passe par `[IO.File]::ReadAllBytes` + ISO-8859-1.

## 4. `src/Blaze/Memory/`

- [x] 4.1 `Memory/Arena.GameState.cs` (259 l.) — l'arène `0x005e9000` déplacée **en un seul bloc**,
      avec son commentaire de disposition. Ne rien réordonner, ne rien scinder.
- [x] 4.2 `Memory/DataTables.cs` (336 l.) — les `Build*` qui reconstituent les tables `.data`
      (`BuildUiDescriptors`, `BuildStatusDescriptor`, `BuildLoadScreenDescriptors`,
      `BuildSelectSubStateBytes`, `BuildLabelDescriptor005606f8`, `BuildSceneToneTable`).
- [x] 4.3 Créer `Memory/Globals.cs` **vide**, avec en en-tête la règle : une globale n'y entre que
      si un contrôle d'adjacence l'y envoie, jamais pour fermer un fichier.
- [x] 4.4 Ajouter à `Documentation/Reference/DataSymbols.md` la section « carte des symboles reconstruits » :
      l'index de 1.2, trié par adresse, colonnes VA / symbole C# / fichier. Documenter en tête que
      le contrôle anti-aliasing se fait en lisant les deux tables du fichier ensemble.
- [x] 4.5 Build + vérification + `playtest.ps1`.
      → **Une régression réelle est apparue ici, puis a été corrigée.** Le premier `playtest` après
      l'extraction de `Memory/` a levé une `NullReferenceException` dans `CurrentPadState()` dès la
      première image du menu. Cause : `s_SlotArea_005e9180 = Arena(0x005e9180)` est un champ
      `static readonly` de `Blaze.cs`, alors que `Arena()` et l'arène qu'il adresse sont partis
      dans `Memory/Arena.GameState.cs`. **Les initialiseurs de champs statiques d'une classe
      partielle s'exécutent dans l'ordre des fichiers, qui n'est pas spécifié** : `Blaze.cs` passe
      avant `Memory/`, donc `Arena()` rendait `null + décalage`.
      Correctif : les **8 champs** de la forme `static readonly … = Arena(0x…)` deviennent des
      propriétés `=> Arena(0x…)`. Sémantiquement identique — la base d'arène est allouée une fois
      et ne change jamais — et **le nombre de lignes ne bouge pas**, donc le manifeste reste valide.
      Après correctif : build propre, **382,3 et 387 par image**, captures identiques.
      **Conséquence pour les groupes 5 et 6**, écrite dans D1 : un champ `static readonly` dont
      l'initialiseur lit un membre statique partant dans un **autre** fichier doit devenir une
      propriété. Restent 41 champs initialisés par un appel de méthode, à surveiller.
      Cette découverte **invalide l'affirmation « le découpage en fichiers est sémantiquement
      nul »** de D1, qui est corrigée en conséquence.

## 5. `src/Blaze/Engine/` — un fichier par étape, build entre chaque

- [x] 5.1 `Engine/Audio.cs` (1 422 l.) — DirectSound, tampons d'effets, musique en flux, couche
      d'effets sonores. Plages `0x00513` et `0x0053d`.
- [x] 5.2 `Engine/Video.cs` (978 l.) — DirectDraw et lecteur de film. Plages `0x00512`, `0x00542`.
- [x] 5.3 `Engine/Input.cs` (1 169 l.) — DirectInput et scrutation par image, **sans** le
      répartiteur d'états qui part en 6.1.
- [x] 5.4 `Engine/Resources.cs` (1 147 l.) — chargeur de fichiers de ressources, plus
      `ReloadStateResources` / `ReloadGameplayResources` / `LoadMenuResources` / `CopyDwords`
      récupérés du bandeau `L11696`.
- [x] 5.5 `Engine/SaveData.cs` (595 l.) — l'E/S carte mémoire **seule** (`Card*`, `NameEncodeChar`,
      `WriteCardSaveFile`). Les 2 082 lignes de sous-états qui la suivaient partent en 6.5.
- [x] 5.6 `Engine/Model3d.cs` (568 l.) — `DrawAnimatedModel`, `ProjectPoint`. Plage `0x0051d`.
- [x] 5.7 `Engine/UiText.cs` (1 334 l.) — moteur de texte. Plage `0x00544`.
- [x] 5.8 `Engine/UiWindow.cs` (350 l.) — rendu de fenêtres. Plage `0x00545`.
- [x] 5.9 `Engine/Primitives2d.cs` (245 l.) — `DrawFlatPrimitive`, `DrawSpritePrimitive`,
      `DrawSpriteVariant`, `DrawTextRun`, `InitialiseEntitySlots`.
- [x] 5.10 `playtest.ps1` sur les trois écrans de référence, captures comparées.
      → Neuf fichiers extraits, **build propre à chacune des neuf étapes**, les 16 fichiers vérifiés
      octet à octet, **382,3 et 387 par image**, captures identiques. `Blaze.cs` : 21 142 → **13 618
      lignes**.
      **Le piège d'ordre d'initialisation a été cherché avant d'extraire, pas subi.** Les 41 champs
      initialisés par un appel de méthode ont été croisés avec le manifeste : **chaque méthode
      atterrit dans le même fichier que le champ qu'elle initialise**, donc aucun risque d'ordre.
      `ToAnsiZ`, `ToAnsiBlock` et `AllocBytes` — appelés depuis 26 initialiseurs répartis partout —
      ont été relus : ils ne lisent **aucun statique**, ce sont des aides pures. Zéro régression.
      Tailles réelles, plus grosses que l'estimation du design parce que la trivia de tête suit
      désormais son membre : Audio 1 238, UiText 1 328, Resources 1 102, Video 1 095, Input 1 007,
      SaveData 585, Model3d 566, UiWindow 349, Primitives2d 249.

## 6. `src/Blaze/Screens/`

- [x] 6.1 `Screens/GameStates.cs` (89 l.) — extraire `RunGameStateTick` (`FUN_00537ca0`) et son
      rappel `s_MainLoopTickCallback` de la queue de la scrutation d'entrées où ils sont mal rangés.
- [x] 6.2 `Screens/State00/` — `MainMenu.cs` (516 l.) et `Options.cs` (2 435 l.). La page options
      est un sous-écran de l'état 0 : `RunMainMenuFrameLoop` appelle `OptionsPage()`.
- [x] 6.3 `Screens/State01/PartySetup.cs` (536 l.) et `Screens/State02/Destination.cs` (435 l.).
- [x] 6.4 `Screens/State03/WorldMap.cs` (1 109 l.). **Ne rien reconstruire** : `FUN_00533940` et
      `FUN_00516fc0` restent en l'état, ce changement ne fait que déplacer.
- [x] 6.5 `Screens/State04/` — `CharacterSelect.cs` (~600 l., chargeur, relocalisations, boucle),
      `CharacterCreation.cs` (~1 490 l., sous-états 1/2/5/6 : dés, classe, statistiques,
      équipement), `SaveSlots.cs` (2 082 l., sous-états 0/3/4/7 : saisie de nom, sauvegarde, écran
      de sauvegarde) — ces derniers récupérés de la seconde moitié du bandeau `L9018`.
- [x] 6.6 `Screens/State06/` — `ItemMenu.cs` (495 l.) et `Inventory.cs` (2 920 l. : inventaire,
      enchère, utiliser).
- [x] 6.7 `Screens/DebugParty.cs` (737 l.) à la racine de `Screens/` — ce n'est pas un état numéroté,
      il est verrouillé par `s_DebugModeLatch_005633a8`.
- [x] 6.8 Réduire `Blaze.cs` à son en-tête, ses `using`, la déclaration `partial` et un pointeur vers
      `ARCHITECTURE.md`. **Vérifier qu'il ne reste aucune ligne de code non déplacée.**
- [x] 6.9 Vérification finale du découpage : la somme des lignes des 34 fichiers doit correspondre
      aux 23 673 lignes d'origine augmentées des seuls préambules. Tout écart est une perte ou une
      duplication, à traiter avant d'aller plus loin.
      → **Contrôle par somme abandonné au profit d'un contrôle plus fort.** La somme oblige à
      modéliser le préambule de chaque fichier, et `Memory/Globals.cs` (créé, hors manifeste) n'a
      pas le même que les autres : l'arithmétique ne prouve alors rien d'utile.
      Contrôle retenu : **réassembler les 28 corps dans l'ordre du manifeste et comparer à la
      référence, ligne par ligne**. Résultat : **23 664 lignes contre 23 664, identiques jusqu'à
      l'index 23 663 inclus**. La seule différence est le dernier élément — la référence y porte
      l'accolade fermante de la classe, que l'extracteur repositionne délibérément en postambule de
      `Screens/State06/Inventory.cs`. Rien de perdu, rien d'altéré.
      Un défaut du **vérificateur** a été corrigé au passage : il attendait cette accolade dans le
      corps du fichier qui porte la fin du fichier d'origine, et déclarait donc `Inventory.cs` une
      ligne trop court. Il la retire maintenant des deux côtés de la comparaison.
      Fausse alerte écartée : les fins de ligne. `grep -c $'\r'` donne 23 680/23 680 sur la
      référence comme sur les sorties — tout est en CRLF. C'est `git show` qui normalise en LF,
      pas le fichier.

## 7. Documentation

- [x] 7.1 Déplacer `OpenForesia/src/Blaze/REVERSE_STATUS.md` à la racine du dépôt. Corriger les
      ~~**deux**~~ références de chemin explicite : `CLAUDE.md:89` et l'en-tête de `Blaze.cs:6`.
      **Ne pas toucher** aux cinq références dans les changements openspec archivés ou terminés :
      ce sont des documents historiques.
      → **Il y en avait 29, pas 2.** L'estimation datait d'avant le découpage : le préambule recopié
      dans les 28 fichiers portait la référence périmée. Corrigé **à la source**, dans les lignes 1-6
      de la référence figée, puis propagé par une relance d'`apply-split` — donc reproductible. Le
      nouvel en-tête pointe vers `ARCHITECTURE.md` et `REVERSE_STATUS.md` à la racine, et son
      mojibake d'origine disparaît au passage (0 sur 29 fichiers).
      **Incident** : en réécrivant ce préambule j'ai détruit la référence figée — un `-split` a rendu
      un seul élément et la boucle a écrasé le fichier (90 octets au lieu de 1,2 Mo). Restaurée
      depuis la copie prise avant les conversions de champs, puis les 8 conversions réappliquées à
      l'identique : **−64 octets = 8 × (−9 pour `readonly ` +1 pour `=>`)**, et les 28 fichiers
      revalidés octet à octet. La leçon vaut d'être gardée : la référence figée n'a **aucune** copie
      dans git tant que le changement n'est pas commité.
- [x] 7.2 Créer `ARCHITECTURE.md` à la racine : l'arborescence, l'organigramme de routage (D5), la
      règle d'état (D2), et la distinction `src/Host/` / `src/Blaze/` avec le tableau des deux règles
      opposées.
- [x] 7.3 Réécrire `CLAUDE.md` : supprimer la « Phase 1 constraint » et la remplacer par « Layout of
      the reconstruction » ; remplacer la ligne `Blaze.cs` du tableau *Layout* par l'arborescence ;
      supprimer la mention `GraphicsService` / `AudioManager` au profit de la règle de nommage
      (`static`, noms neutres, jamais `Manager` / `Service`) ; ajouter les trois règles de travail
      (état, taille de fichier, chantier en cours).
- [x] 7.4 Corriger dans `CLAUDE.md` les deux erreurs de fait relevées : le dépôt openspec **n'est pas
      vide** (11 changements actifs, 1 archivé, 1 spec `blaze-intro-attract`), et `node` vit sur
      `D:\Logiciels\nodejs`, hors du `PATH` — à documenter au même endroit que le piège du SDK dotnet.
- [x] 7.5 Mettre à jour `TODO.md` : la priorité 1 est faite, la priorité 2 (`FUN_00533940`) redevient
      le front, et le rappel que `State03/` est prêt à recevoir `WorldMapTick.cs`.
      → Priorité 1 réécrite en bilan. Y figure l'avertissement le plus utile pour la suite :
      **le défaut d'aliasing dormant de `0x00566c70` mordra quand la carte du monde affichera
      quelque chose**, donc à traiter **avant** la priorité 2.

- [x] 7.6 **`Strategy.md` mis à jour** — hors périmètre initial, ajouté sur demande de l'utilisateur
      le 2026-08-19. La proposition le listait « non touché » parce que c'est un document de
      méthode ; mais il contredisait le dépôt (§ « Organisation — Phase 1 » exigeait que *toute* la
      reconstruction tienne dans `Blaze.cs`), exactement le risque que ce changement anticipait
      pour `CLAUDE.md`.
      Ce qui a changé, dans le style du document :
      **(a)** § « Organisation — Phase 1 » → § « Organisation » : les deux moitiés du dépôt et leurs
      règles opposées, la règle de routage en quatre branches, le nommage (l'interdiction de
      `Manager`/`Service` est **conservée**, seule sa formulation change), et les deux non-critères
      (taille de fichier, confort d'un fichier).
      **(b)** Nouvelle § « Organisation — la règle d'état » : le code par responsabilité, l'état par
      adresse, la règle d'étendue d'arène, et le piège d'ordre d'initialisation des classes
      partielles.
      **(c)** § « Données globales Ghidra » gagne « Le contrôle d'adjacence » : chercher l'adresse
      dans la carte de `DataSymbols.md` avant de déclarer un `DAT_` isolé, avec les 107 paires
      inter-fichiers comme justification.
      **(d)** § « Interdictions » gagne trois entrées : déplacer une globale pour rendre un fichier
      autonome, éclater une arène, déplacer un chantier en cours.
      **(e)** Trois consignes opérationnelles qui pointaient vers `src/Blaze/Blaze.cs` renvoient
      maintenant à la règle de routage. 353 → 466 lignes, zéro mojibake.

> **`Prompt.md` porte le même défaut, et n'a pas été touché.** Six références y disent encore
> d'écrire dans `Blaze/Blaze.cs`, dont « conserve pour l'instant le code dans `Blaze.cs` » et
> « Crée `Blaze/Blaze.cs` ». C'est l'autre document de méthode faisant autorité. **Décision
> utilisateur requise** avant de le modifier.

## 8. Clôture

- [x] 8.1 `openspec validate split-blaze-into-subsystems`.
- [x] 8.2 Build propre, puis `playtest.ps1` sur **tous** les écrans atteignables — menu principal,
      options, sélection de personnages, création de personnage, menu objets, inventaire, enchère —
      et comparaison des captures à celles de 1.6.
      → Aucune régression. Écran-titre ; sélection des personnages **382,3** ; menu objets **387** ;
      liste des sauvegardes **434** ; liste d'objets **374** (repère du protocole : ~374) ; page
      d'options, dont les 16 primitives par image s'expliquent par son chemin d'interface propre.
      Captures relues, toutes conformes.
      **Un plantage est survenu et n'est PAS une régression** : `AllocLowZeroed` a levé
      « No free block of 0x2000 bytes below 0x01000000 ». La commande identique, relancée sans
      rien changer, passe. C'est une **course perdue pour la fenêtre basse**, déjà documentée dans
      ce dépôt (§ « The UI descriptors carry an ordering table, so they must live low ») : ces
      descripteurs sont des propriétés paresseuses précisément pour que la course perdue soit
      rattrapable par une relance au lieu d'une `TypeInitializationException` fatale. Consigné dans
      `test-protocol.md` avec la conduite à tenir.
- [x] 8.3 Consigner dans `REVERSE_STATUS.md` une section datée : ce que le découpage a révélé (les
      bandeaux faux, `Primitives2d`, le répartiteur mal rangé, l'extinction masquée), et ce qui reste
      ouvert.
- [x] 8.4 Ouvrir le changement suivant pour la réparation de l'encodage double (871 lignes,
      commentaires et journaux seulement) — **hors périmètre ici**, délibérément.
