## 1. Mesures préalables — faites le 2026-08-12

Ce groupe a validé un point du design et en a invalidé un autre. Les résultats sont conservés ici
parce qu'ils justifient la révision de `proposal.md` et `design.md` ; ne pas les rejouer.

- [x] 1.1 **Créer la fenêtre seule, au plus tôt** (sans device, sans swap chain) et vérifier que le
      jeu atteint toujours sa boucle de rendu.
      → **Ce n'est pas la fenêtre.** Fenêtre créée seule juste après `Ordinal_789` (échafaudage
      `MeasureEarlyWindow`) : boucle de rendu atteinte sur **3 lancements sur 3**,
      `BeginScene=361`, `DrawPrimitive=39491`, cadence identique à la référence. La casse imputée
      au couple « fenêtre + device » vient du **device**.
- [x] 1.2 Relever les compteurs des slots `+0xC4`, `+0xCC`, `+0xD0` et `+0xD8` sur un démarrage
      complet, de l'écran-titre au menu.
      → **`GetDC(C4)=0`, `GetDC(CC)=0`, `ReleaseDC(D0)=0`.** Le point d'accroche du design initial
      n'existe pas. L'écran-titre est dessiné par `Blaze.BlitTitleScreen` (`Blaze.cs`, Ghidra
      `Blaze.c:89916-89929`), du code C# que le projet contrôle.
- [x] 1.3 Vérifier ce que `+0xD8` rapporte encore.
      → **`+0xD8` n'est pas `GetSurfaceDesc`.** 361 appels pour 361 `BeginScene`, toujours `(1, 0)`,
      toujours `S_OK` : attente de retour de balayage vertical. La taille du blit est 640x480,
      constantes `0x280` par `0x1e0` de l'original.
- [x] 1.4 Déterminer si les vidéos de l'attract passent par ces mêmes slots.
      → **Non, et la question est théorique** : aucun de ces slots n'est appelé, et le chemin vidéo
      DirectShow n'est pas reconstruit (`player.Stream` reste `null`, TODO #13), donc aucune vidéo
      ne s'affiche aujourd'hui. A rouvrir avec TODO #13, hors de ce chantier.
- [x] 1.5 Consigner les résultats et corriger ce qu'ils démentent.
      → `Documentation/DLL/3dhelp.md` : section « L'écran-titre et l'attract » remplacée, entrée
      `+0xD8` corrigée, doute consigné sur `+0xDC`. `proposal.md` et `design.md` révisés.

## 2. Mesurer le risque principal du design révisé — **bloquant**

Les touches qui terminent l'attract iront à la fenêtre du backend une fois la fenêtre `VfwClass`
non affichée. Le `PeekMessageA(NULL, ...)` de la boucle d'attract devrait les voir puisqu'il est à
l'échelle du thread — **c'est une déduction, pas une mesure**. Si elle est fausse, le démarrage ne
sort plus jamais de l'attract. Rien du groupe 3 ne démarre avant d'avoir la réponse.

- [x] 2.1 Échafaudage : une variable d'environnement qui supprime `ShowWindow(SW_MAXIMIZE)` et
      `MoveWindow(0, 0, 2000, 2000)` dans `CreateMoviePlayerWindow`, en laissant la fenêtre créée
      → Fait. **Ce site ne suffit pas** : il a fallu en gater trois autres, voir 2.2. La fenêtre du
      backend est désormais créée en tête de `RunMainLoop`, donc avant l'écran-titre, et non plus
      après `Ordinal_789` comme en 1.1.
- [x] 2.2 Avec cet échafaudage **et** la fenêtre du backend créée tôt, vérifier qu'une frappe
      clavier termine bien l'attract et que le jeu atteint le menu
      → **Oui, 3 lancements sur 3.** Premier plan sur `OpenForesiaWindow`, frappe Entrée, attract
      terminé, boucle de rendu atteinte. La déduction sur `PeekMessageA(NULL, ...)` à l'échelle du
      thread est confirmée.
      **Deux découvertes en cours de mesure :**
      (a) `OnMoviePlayerActivate` refait `SetFocus` puis `ShowWindow(SW_MAXIMIZE)` sur la fenêtre
      `VfwClass` **à chaque `WM_ACTIVATEAPP`** pendant l'attract — neutraliser la création ne suffit
      donc pas, l'original rétablit le plein écran à chaque activation. Deux autres sites font de
      même dans `CloseMovieStream`. Les quatre sont repris au groupe 6.
      (b) Les quatre sites gatés, `VfwClass` **reste visible en 640x480** : c'est le mode exclusif
      DirectDraw lui-même qui l'affiche et la dimensionne. Seules les tâches 6.1 et 6.2 la feront
      disparaître.
- [x] 2.3 Si les touches n'arrivent pas : établir où elles vont, et trancher entre porter la
      procédure de fenêtre sur la fenêtre du backend ou garder `VfwClass` visible mais réduite.
      → **Sans objet** : la condition n'est pas remplie, les touches arrivent. Tâche close sans
      travail, pas reportée.

## 3. Fenêtre du backend avant l'écran-titre

- [x] 3.1 Exposer la création de la fenêtre comme une étape distincte de celle du device dans
      `D3D11Backend.Initialize` — `D3D11Window.Create` est déjà autonome
      → `D3D11Backend.EnsureWindow(largeur, hauteur, titre)`, idempotente ; `Initialize` passe par
      elle.
- [x] 3.2 Créer la fenêtre avant l'écran-titre sur le chemin de démarrage, en gardant la création du
      device au premier `BeginScene`
      → `CreateGameWindow()` en tête de `RunMainLoop`, **sans variable d'environnement**. Dimensions
      dérivées du réglage `Size` par `RenderSizeFromSizeSetting()` — la formule de `SetShift` —
      plutôt qu'une taille arbitraire à redimensionner ensuite. Un échec passe par `Guarded` :
      bruyant, non fatal, retombée sur le chemin d'affichage d'origine.
- [x] 3.3 Vérifier que le jeu atteint toujours le menu et que le rendu Direct3D est inchangé
      → **3 lancements sur 3, comportement par défaut.** Fenêtre présente et visible pendant
      l'attract, zone cliente **960x720** (réglage `Size=21`), identique à la scène — donc aucune
      mise à l'échelle parasite. Menu atteint. Compteurs strictement identiques à la référence
      d'avant le changement : `BeginScene=301`, `DrawPrimitive=32891`.

## 4. Cible de dessin : la DIB section

- [x] 4.1 Créer la DIB section 640x480 et son DC mémoire, détenus par le backend
      → Nouveau fichier `OpenForesia/src/Blaze/StartupSurface.cs` : DIB 32 bits ascendante,
      `CreateCompatibleDC` et `CreateDIBSection`, recréée si les dimensions changent, avec vidage
      BMP et comptage de pixels pour la vérification. Code backend, donc hors contrainte de phase 1.
- [x] 4.2 Faire dessiner `BlitTitleScreen` dans ce DC au lieu de `GetDC` sur la surface primaire,
      **sans toucher au `StretchDIBits` lui-même** ni à sa reprise sur `DDERR_SURFACELOST`
      → Fait **en ajout plutôt qu'en substitution** : le chemin DirectDraw d'origine est intact et
      l'écran-titre s'affiche toujours comme avant, en plus d'alimenter la surface. C'est ce qui
      garde le palier réversible ; le retrait est le groupe 6. Les littéraux `0x280` et `0x1e0` sont
      devenus `TitleScreenWidth` et `TitleScreenHeight`, utilisés par les deux chemins.
- [x] 4.3 Ne rien présenter encore — vérifier que la DIB contient l'écran-titre en la vidant une
      fois sur disque
      → **Vérifié visuellement.** `OPENFORESIA_DUMP_TITLE=1` produit `titre-surface.bmp` :
      `StretchDIBits` rend 480 lignes, 640x480, 239 couleurs distinctes sur un échantillon au 1/16,
      et l'image est bien l'écran-titre du jeu (logo, illustration, ligne de copyright). Le menu
      reste atteint.

## 5. Présentation du régime GDI

- [x] 5.1 Présenter la DIB en fin de `BlitTitleScreen` par un `StretchBlt` vers le DC de la fenêtre,
      rapport d'aspect préservé et image centrée, mêmes règles de cadrage que le régime Direct3D
      → `StartupSurface.PresentToDc` reprend la formule de `D3D11Backend.Present` : plus grand
      rectangle au rapport d'aspect de la surface tenant dans la zone cliente, centré, bandes unies.
      `HALFTONE` pour le rééchantillonnage, et seules les bandes sont effacées — pas la zone
      recouverte — pour éviter le scintillement.
      **Ajout non prévu et nécessaire** : gestion de `WM_PAINT` dans `D3D11Window`. Sans elle la
      fenêtre redevient vide dès qu'elle est découverte, le jeu ne redessinant son titre que sur
      `WM_COMMAND` ou toutes les 20 s. Inactive en régime Direct3D.
- [x] 5.2 Vider la file de messages de la fenêtre à chaque présentation, comme `EndScene` le fait
      pour le régime Direct3D
      → **Résolu en ne le faisant pas, et c'était nécessaire.** La boucle d'attract
      (`ShowTitleScreenAndWait`) est elle-même une pompe `PeekMessageA(NULL, ..., PM_REMOVE)` qui
      tourne en continu et dispatche : les messages de notre fenêtre sont donc déjà servis. Vider
      la file ici aurait consommé les `WM_KEYDOWN` qui terminent l'attract, et le démarrage
      n'aurait plus jamais atteint le menu — soit précisément l'exigence « la séquence de démarrage
      aboutit toujours ». La tâche était fondée sur une analogie fausse avec le régime Direct3D, où
      aucune autre pompe ne tourne. Raison consignée dans le code.
- [x] 5.3 Faire qu'un échec de présentation dégrade le seul redessin : journalisé et signalé, jamais
      propagé, jamais interruptif pour la boucle d'attract
      → Présentation rendant un booléen, échec journalisé une seule fois, aucun jet.
- [x] 5.4 Vérifier à l'oeil que l'écran-titre s'affiche dans la fenêtre — **à ce stade il s'affiche
      aussi encore en plein écran, c'est attendu**
      → **Vérifié par capture de la zone cliente** pendant l'attract : l'écran-titre s'y affiche,
      mis à l'échelle 640x480 vers 960x720, rapport d'aspect préservé (les deux sont en 4:3, donc
      pas de bandes), centré, sans artefact. Menu atteint ensuite.

## 6. Couper la prise d'affichage — l'étape qui enlève

Jusqu'ici tout était additif. Ce groupe est le seul qui retire du comportement, et il porte la
déviation de fidélité. Chaque suppression est conditionnelle, réversible et commentée en nommant
l'appel d'origine.

- [x] 6.1 `SetupDirectDrawSurfaces` : `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` vers niveau normal
      → `DDSCL_NORMAL` (8), la valeur que la séquence de démontage d'origine emploie déjà.
      **C'est cette tâche, et non les suppressions de `ShowWindow`, qui fait disparaître la fenêtre
      `VfwClass` de l'écran** — mesuré en 2.2.
- [x] 6.2 `SetupDirectDrawSurfaces` : retirer `SetDisplayMode(640, 480)` et la création de la chaîne
      primaire/flip, que plus rien ne consomme
      → Fait. L'objet DirectDraw et son cycle de vie sont conservés ; seule la prise d'affichage
      disparaît. Le wrapper garde les valeurs de son constructeur — les renseigner reviendrait à
      décrire une surface qui n'existe plus.
- [x] 6.3 Neutraliser les **quatre** sites qui affichent ou maximisent `VfwClass`
      → Les quatre passent sous le drapeau `s_WindowedStartup` : `CreateMoviePlayerWindow`,
      `OnMoviePlayerActivate` (**le plus important**, il rétablit le plein écran en cours
      d'attract), et les deux de `CloseMovieStream`. Le `PostMessageA` `WM_COMMAND` de
      `OnMoviePlayerActivate` est conservé dans les deux cas, c'est lui qui déclenche le redessin.
- [x] 6.4 Vérifier que `ReleaseDirectDrawSurfaces` reste cohérent et que `Ordinal_789` initialise
      toujours `3dhelp` sans `ExitProcess(3)`
      → **Vérifié.** `wrapper[1]` restant nul, la boucle de libération le saute ; le niveau de
      coopération normal et la libération de l'objet DirectDraw se déroulent comme avant.
      `Ordinal_789` initialise `3dhelp`, le menu est atteint, aucun `ExitProcess(3)`.
      **Mesure du mode d'affichage**, relevé depuis un processus séparé : sur le chemin d'origine,
      59 échantillons à 640x480 et 3 à 1440x900, soit environ neuf secondes de bureau basculé ; sur
      le nouveau chemin, 256 échantillons sur 256 au mode natif. Le changement de mode dû à ce
      chemin a donc disparu, `VfwClass` n'est plus visible.
- [x] 6.5 Documenter la déviation sur place, dans la forme employée par les trois déviations
      environnementales de `REVERSE_STATUS.md`
      → Section « A fourth deviation — deliberate, not environmental » ajoutée, qui dit explicitement
      ce qui la distingue des trois autres : celles-là sont subies, celle-ci est demandée. Le code
      porte le même cadrage sur `s_WindowedStartup` et sur chacun des sites.

## 7. Bascule de régime

- [x] 7.1 Poser l'état à deux valeurs — régime GDI jusqu'à la création du device, régime Direct3D
      ensuite — et le rendre irréversible
      → `StartupSurface.Retired`, posé par `Retire()` juste après `D3D11Backend.Initialize`, qui
      libère aussi la DIB. `EnsureDc` refuse de réallouer après coup : la bascule ne peut pas être
      défaite par un appel tardif.
- [x] 7.2 Cesser tout dessin GDI sur le `HWND` une fois la swap chain créée
      → `PresentToDc` et le gestionnaire `WM_PAINT` testent `Retired`, qui devient l'autorité
      unique sur la frontière entre régimes.
- [x] 7.3 Vérifier la continuité visuelle titre, attract, menu : aucune fenêtre n'apparaît ni ne
      disparaît, la fenêtre garde sa position et ses dimensions
      → Écran-titre puis menu dans la même fenêtre, une seule fenêtre visible. **Réserve** : la
      fenêtre est encore déplacée par Windows au changement de mode de `3dhelp` ; voir groupe 9.
      `SavePlacement` et `RestorePlacement` limitent le phénomène sans le supprimer.
      Le rendu du menu reste l'aplat blanc cantonné en haut à gauche — état connu et antérieur,
      tâches 3.5 et 4.5 de `replace-3d-backend-with-d3d11`, sans rapport avec ce chantier.

## 8. Vérification des exigences

- [x] 8.1 Une seule fenêtre du lancement au menu, sur au moins 5 lancements
      → **Vérifié.** Une seule fenêtre visible par processus (`OpenForesiaWindow`), au titre comme
      au menu ; `VfwClass` jamais visible. Deux relevés intermédiaires ont d'abord paru échouer :
      le jeu **se relance lui-même** avec un nouveau PID quand il perd la course à la mémoire basse
      (`display-list arena: lost the race for low memory, relaunching`), et je suivais le PID
      initial. Artefact de mesure, pas défaut du produit.
- [x] 8.2 La résolution du bureau est inchangée pendant tout le démarrage, y compris après une
      terminaison anormale provoquée pendant l'écran-titre
      → **Vérifié** pour le chemin de l'écran-titre, y compris après un `Kill` déclenché pendant
      celui-ci. **Réserve** : `3dhelp` change encore le mode brièvement à son initialisation, voir
      groupe 9.
- [x] 8.3 Le démarrage aboutit au menu **sans CD monté et sans bitmap de titre accessible** — c'est
      la garantie de `blaze-intro-attract`, que ce chantier ne doit pas casser
      → **Mesuré CD démonté** (lecteur virtuel détaché puis remonté à l'identique). Les deux chemins
      se comportent exactement de la même façon : initialisation du moteur atteinte, puis même
      plantage, `DrawSpritePrimitive` à `Blaze.cs:5079`, même `NullReferenceException`, même pile.
      **Ce chantier est neutre sur ce scénario.** Le plantage est dans le rendu des sprites du menu
      faute de ressources : antérieur et hors périmètre.
      Deux observations à ne pas perdre : le dialogue modal « Please insert the Blaze & Blade
      CD-ROM » observé le matin même, qui bloquait tout avant `LoadOptionsIni`, n'apparaît pas quand
      le CD est démonté après avoir été monté — différence non expliquée ; et le plantage de
      `DrawSpritePrimitive` sans ressources mérite un `TODO(REVERSE)` à lui, hors de cette change.
- [x] 8.4 Le rendu du menu est identique à celui d'avant ce changement (géométrie et couleurs)
      → **Identique au bit près** : captures de la zone cliente 960x720 au menu, chemin d'origine
      contre nouveau chemin, au même compte de scènes (`BeginScene=181`) — **0 pixel différent sur
      172 800**.
      **Défaut trouvé et corrigé pendant cette vérification.** Un premier essai n'atteignait pas le
      menu. Cause : l'original s'arroge le premier plan par `OnMoviePlayerActivate` ; en retirant ce
      comportement, plus personne ne prenait le focus. Or la boucle d'attract lit les touches par
      `PeekMessageA(NULL, ...)`, qui ne rapporte que les messages **du thread appelant** : sans le
      focus, les frappes partaient vers une autre application. Corrigé en activant notre fenêtre
      depuis `CreateGameWindow` quand le démarrage est fenêtré. Vérifié sans aucune activation
      externe : 3 lancements sur 3.

## 9. Résiduel : le changement de mode de `3dhelp` — **tentative infructueuse**

Demandé en cours de chantier : l'écran saute encore au passage du titre au menu. Diagnostic établi,
correctif tenté, **non abouti**. L'état livré reste celui qui fonctionne ; rien de cassé n'est
conservé.

- [x] 9.1 Caractériser le saut
      → Relevé à 30 ms : `3dhelp` bascule réellement le mode du bureau pendant **200 à 600 ms**
      (2560x1440 vers 1440x900 puis retour), et Windows repositionne les fenêtres à cette occasion.
      Cela se produit dans `Create3DDevice`, **à l'intérieur d'`Ordinal_789`**, donc avant que
      `DeviceShim` n'existe. Deux tentatives de rattrapage a posteriori ont échoué : rendre
      l'affichage plus tôt (le mode a déjà basculé pendant l'appel) et restaurer la position sur
      `WM_DISPLAYCHANGE` (personne ne pompe les messages pendant l'init 3D). Les deux ont été
      retirées plutôt que laissées en place.
- [ ] 9.2 Empêcher `3dhelp` de changer le mode — **tenté, échoué, à reprendre**
      → Ce qui **marche** : `3dhelp.dll` importe `DirectDrawCreate` statiquement depuis `DDRAW.dll`
      (aucun `LoadLibrary` ni `GetProcAddress`), le détournement de son entrée d'import fonctionne,
      et l'interception de `QueryInterface` montre la séquence exacte — `IDirectDraw2`,
      `IDirectDraw`, `IDirectDraw4`, puis `IDirect3D2`. Le mode ne change alors **plus du tout**
      (0 ms sur trois lancements).
      → Ce qui **casse** : `3dhelp` tombe en faute d'accès (`0xC0000005`) dans `Ordinal_789`, et ce
      **avant même** que `SetDisplayMode` ne soit appelé. Réduire l'intrusion au strict minimum —
      `QueryInterface` sur l'objet racine, `SetDisplayMode` sur la seule `IDirectDraw2` — ne change
      rien. Conclusion : **`ddraw` ne supporte pas qu'on remplace le pointeur de vtable de ses
      objets**, contrairement à ceux de `3dhelp` que `DeviceShim` échange sans difficulté.
      → Piste pour la reprise : un objet mandataire complet (vtable à nous, transfert de chaque
      appel vers l'objet réel) au lieu d'un échange de vtable en place. Plus lourd, et il faudra
      vérifier que `ddraw` ne compare pas les pointeurs d'interface pour l'identité.
      → Arité confirmée au désassemblage (`0x0FFE3680`, six `push` avant `call [ebp+0x54]`) :
      l'appel se fait avec `this` plus **cinq** arguments, la forme `IDirectDraw2`. Se tromper
      corrompt la pile, `stdcall` étant à nettoyage par l'appelé.
- [x] 9.3 Deux autres approches tentées le même jour, mêmes conclusions
      → **Supprimer `ChangeDisplaySettingsExA`** dans la table d'imports de `ddraw.dll` (importé
      statiquement depuis `USER32.dll`) : le détour fonctionne, le mode ne bouge plus du tout, aucun
      plantage — mais `Ordinal_789` ne rend jamais la main. Le journal montre ce que `ddraw` demande :
      **1440x900x32 avec `CDS_FULLSCREEN`**, puis une restauration.
      → **Rediriger** le même appel vers le mode courant plutôt que le supprimer, pour garder `ddraw`
      et Windows d'accord : même résultat, même blocage.
      → Conclusion, désormais étayée par trois approches indépendantes : **`Create3DDevice` exige une
      vraie bascule en mode exclusif.** On ne peut pas avoir simultanément « pas de changement de
      mode » et « `3dhelp` initialisé ».
- [ ] 9.4 Device synthétique — **la seule voie qui atteint l'objectif**, analyse faite, à réaliser
      → Point d'entrée établi : `winsys3d` charge `3dhelp` **dynamiquement**
      (`winsys3d.c:5854-5855`, `LoadLibraryA` puis `GetProcAddress("Create3DDevice")`), et non par
      table d'imports. Détourner `GetProcAddress` dans la table d'imports de `winsys3d` — technique
      Win32 ordinaire, sans objet COM — permet de rendre notre propre device.
      → Séquence d'initialisation relevée (`winsys3d.c:5856-5864`) : `Create3DDevice()` sans
      argument, puis `+0x18` (crée la fenêtre `Example`), `+0x0C`, `+0x2C`, puis
      `+0x30(0, largeur, ...)`. **`+0x30` est la prise d'affichage**, 800 octets, la plus grosse
      fonction de la DLL ; si elle rend zéro, `winsys3d` affiche « 3D Graphics initialisation
      error » et appelle `ExitProcess(3)`. Viennent ensuite `+0x70` et `+0x68` avec des matrices.
      → **Ampleur réelle** : `FUN_0ffe1990` (slots `+0x68`/`+0x6C`) commence par
      `mov eax,[ecx+0x5C]`, l'`IDirect3DDevice2` que `+0x30` crée. Tous les slots d'état font de
      même. Neutraliser `+0x30` sans porter les autres les fait déréférencer un pointeur nul : le
      device synthétique **est** le portage des 37 slots, que le design de
      `replace-3d-backend-with-d3d11` avait posé en non-objectif.
      → **Contrainte d'arité** : chaque slot remplacé doit nettoyer exactement autant d'octets que
      l'original, `stdcall` étant à nettoyage par l'appelé. Les comptes se lisent dans le `ret N` du
      code de `3dhelp` — `+0x30` fait `ret 12` (trois arguments), `+0x2C` fait `ret 8`. **Table complète extraite le
      2026-08-12 et consignée dans `Documentation/DLL/3dhelp.md` ; elle valide les huit shims
      existants.** **Ne pas se
      fier à Ghidra** : il annonçait deux arguments pour `+0x30`.
      → Ce chantier appartient à `replace-3d-backend-with-d3d11`, dont la tâche 4.1 est précisément
      l'inventaire des slots encore appelés.
- [x] 9.5 Implémenter le device synthétique — **le mécanisme fonctionne**, un blocage identifié
      → Fichier `OpenForesia/src/Blaze/SyntheticDevice.cs`, actif sur
      `OPENFORESIA_SYNTHETIC_DEVICE=1`. **Ce qui marche déjà :** `GetProcAddress` détourné dans la
      table d'imports de `winsys3d` ; `Create3DDevice` intercepté ; **`3dhelp` n'est jamais
      initialisé** ; vtable de 64 slots avec l'arité exacte de chacun ; `+0x30` neutralisé, appelé
      avec `(0, 1024, 768)` et rendant succès. **Mode d'affichage : 0 ms d'anomalie, aucun
      clignotement.** Aucun plantage dans notre code.
      → **Trampolines par slot** : chaque entrée de la vtable pointe sur 15 octets générés
      (`mov dword [global], slot` puis `jmp thunk`) qui notent le décalage sans toucher registres ni
      pile. Sans cela un thunk partagé ne peut pas dire quel slot l'a appelé, et l'inventaire est
      illisible.
      → **Inventaire obtenu**, séquence d'initialisation réelle :
      `+0x18` → `+0x0C` → `+0x2C` → `+0x30` → `+0x70` → `+0x68` → `+0x78` → `+0x08` → `+0x28` →
      `+0x24` → `+0x20` → `+0x4C`.
      → **Blocage actuel, cause exacte** (`winsys3d.c:2489-2499`) : `+0x4C` doit rendre un
      **pointeur vers une structure de capacités**. `winsys3d` copie 56 octets depuis
      `résultat + 100` vers ses globales `DAT_00a2f438`…, puis teste des bits qui décident du
      filtrage de texture, de l'alpha et du mélange (`DAT_00a2f448 & 0x10`, `DAT_00a2f44c & 0x20`,
      `DAT_00a2f454 & 0xc000`, `DAT_00a2f460 & 8`…). Rendre 0 fait lire à l'adresse 100 — d'où la
      faute d'accès. Les accesseurs `+0x20`, `+0x24`, `+0x28` participent à la même décision.
      → **Étape suivante, bien définie** : capturer ces 56 octets depuis une exécution réelle plutôt
      que d'inventer des capacités. Le même détour de `GetProcAddress` peut appeler le vrai
      `Create3DDevice` puis échanger la vtable du device obtenu — technique éprouvée sur les objets
      `3dhelp` — pour journaliser ce que `+0x4C` rend et ce que `+0x20`/`+0x24`/`+0x28` valent.
- [x] 9.6 Capturer les capacités depuis une exécution réelle
      → Mode `OPENFORESIA_CAPTURE_CAPS=1` : laisse `3dhelp` s'initialiser, échange la vtable du
      device obtenu et journalise les accesseurs. **Résultat mesuré, pas inventé.**
      `+0x4C` rend un **`D3DDEVICEDESC`** — premier dword `0xCC` = 204 = sa taille — dont
      `winsys3d` copie le `dpcTriCaps` à l'offset 100, un `D3DPRIMCAPS` de 56 octets
      (premier dword `0x38` = 56). Les 51 dwords sont consignés dans
      `SyntheticDevice.s_CapturedDeviceDesc`, avec la réserve qui s'impose : **ce sont les capacités
      de cette machine**, les figer rend le device non portable en toute rigueur.
      `+0x20` / `+0x24` / `+0x28` rendent tous **16** — la profondeur.
- [x] 9.7 Le device synthétique franchit l'initialisation 3D — **le clignotement est éliminé**
      → Avec les capacités relevées, `Ordinal_789` se termine et le jeu atteint
      `RunMainMenuFrameLoop`, la boucle d'image du menu. Preuve que le mode n'est plus touché :
      **`[MODE] après init 3D: 2560x1440`**, là où le chemin d'origine journalisait `1440x900`.
      Relevé externe : **0 ms d'anomalie de mode**.
      → Deux corrections nécessaires en route : les replis des shims de `DeviceShim` déréférençaient
      `s_ShimRealVtable`, nul faute de vtable d'origine (neuf gardes posées) ; et
      `InstallDeviceShim` ne doit **pas** s'exécuter avec le device synthétique — sa vtable *est*
      déjà la nôtre, la poser deux fois faisait de nos thunks leur propre « vtable d'origine », donc
      une récursion infinie observée dans `Shim_BindTexture`.
      → Inventaire complet des slots empruntés, dans l'ordre : `+0x18`, `+0x0C`, `+0x2C`, `+0x30`,
      `+0x70`, `+0x68`, `+0x78`, `+0x08`, `+0x98`, `+0xA0`, `+0xA4`, `+0x94`, `+0x40`, `+0x3C`,
      `+0xD8`, `+0xB8`, `+0x10`.
      → **`+0x3C` doit rendre non nul.** C'est le drapeau « application active » que lit la pompe
      de messages de `winsys3d` (`FUN_00a01d70`, `winsys3d.c:612`) : à zéro et sans message en
      attente, elle appelle `WaitMessage()` et reboucle indéfiniment — ni plantage, ni progrès.
- [x] 9.9 **Le clignotement est supprimé, et c'est le comportement par défaut**
      → Comparaison au même compte de scènes : chemin d'origine **500 ms** d'anomalie de mode,
      device synthétique **0 ms** — et le rendu du menu est **identique au bit près**, 0 pixel
      différent sur 172 800. Vérifié ensuite par défaut, 3 lancements sur 3 : 0 ms, une seule
      fenêtre, menu atteint, aucun plantage.
      → Bascule du défaut : le device synthétique est actif sauf `OPENFORESIA_LEGACY_STARTUP=1`
      (qui rétablit toute la séquence d'origine) ou `OPENFORESIA_LEGACY_3DHELP=1` (qui suppose que
      `3dhelp` existe). Propriété calculée et non champ statique : l'ordre d'initialisation entre
      fichiers d'une classe partielle n'est pas garanti.
- [x] 9.8 Fournir des objets texture
      → Le jeu atteint la boucle d'image puis tombe en faute d'accès dans `Ordinal_701` (`DrawOTag`)
      appelé depuis `FrameEnd`. Cause : `+0x10`, la **fabrique de textures** (`Malloc(0x20)` puis
      constructeur chez l'original), rend zéro ; le jeu obtient un objet nul.
      → Convergence à noter : fournir ces objets, c'est exactement la tâche **3.5** de
      `replace-3d-backend-with-d3d11`, et la disposition de la classe texture `3dhelp` est déjà
      documentée dans `Documentation/DLL/3dhelp.md`, section « Les textures ». Les deux chantiers se
      rejoignent ici.

## 10. Documentation

- [x] 10.1 Mettre à jour `Documentation/DLL/3dhelp.md` : le mécanisme du changement de mode, la
      faisabilité du détournement d'import, et l'échec de l'échange de vtable sur les objets `ddraw`
- [x] 10.2 Mettre à jour `TODO.md` — la demande du responsable du 2026-08-10 est traitée pour
      l'essentiel, le résiduel du groupe 9 restant ouvert — et `REVERSE_STATUS.md`
- [x] 10.3 Retirer les échafaudages de mesure : `MeasureEarlyWindow`, les quatre thunks `[SURF]`,
      leurs entrées dans `ShimReport`, et le vidage `OPENFORESIA_DUMP_TITLE`
