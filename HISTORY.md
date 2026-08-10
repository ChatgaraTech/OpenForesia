# Historique

Une ligne par fonctionnalité ou correction. Le plus récent en haut.

---

## 2026-08-10

- fix : **écran noir intermittent résolu** — les quatre structures DRAWENV/DISPENV sont contiguës dans le binaire et `FrameEnd` lit le DISPENV à `DrawEnv + 0x5C` ; les allouer séparément faisait lire du tas quelconque. 10 lancements sur 10 affichent le menu, contre 3 sur 10 avant
- feat : mesure automatisée du taux d'écran noir (capture de la zone cliente, comptage des pixels allumés)
- feat : bascule fenêtre / plein écran sans bordure par Alt+Entrée, position et dimensions restituées au retour
- fix : Alt+Entrée retiré à DXGI (`MakeWindowAssociation`) — il aurait basculé en plein écran exclusif, le mécanisme même que cette change supprime
- feat : `3dhelp` débranché de l'affichage — sa fenêtre est masquée et le mode d'affichage restauré ; le bureau n'est plus jamais modifié, y compris après terminaison forcée
- feat : la géométrie du menu s'affiche dans la fenêtre du backend, en fenêtré, sans changement de résolution
- fix : combinaison texture/diffuse en mode décalque — le jeu émet ses quads texturés avec une diffuse noire, les multiplier donnait un écran noir
- doc : le cadrage en haut à gauche vient du jeu lui-même, qui émet sa géométrie dans un espace d'environ 320x240 sans que rien ne la mette à l'échelle de la cible
- feat : chemin chaud du jeu redirigé vers le backend D3D11 (effacements, scènes, `DrawPrimitive`, ombrage plat/Gouraud, viewport) — bascule `OPENFORESIA_LEGACY_3DHELP=1` pour revenir à 3dhelp
- feat : dépliage des bandes et éventails de triangles en listes, et ombrage plat reproduit à la copie de la couleur du premier sommet
- fix : création du device D3D11 déplacée dans la boucle de rendu — la faire pendant l'init du jeu l'empêchait d'atteindre sa boucle
- fix : exceptions des thunks capturées et journalisées — une exception managée traversant une frame native tuait le processus sans trace
- doc : correction de la cartographie de 3dhelp par comptage au runtime — `+0xDC` (Flip) et `+0x60` (Clear Z) ne sont jamais appelés en phase menu, la présentation se fait à `EndScene`
- feat : backend Direct3D 11 via Silk.NET — fenêtre propre, swap chain DXGI fenêtrée, cible de rendu hors écran, shaders pour sommets pré-transformés, présentation à l'échelle avec rapport d'aspect préservé
- feat : test de bout en bout du backend (`--d3d11-selftest`) — damier plein cadre et interpolation de couleurs, sans démarrer le jeu
- doc : mode d'affichage mesuré — le jeu rend au plus 1024x768 dans un mode 1440x900 négocié par `3dhelp`, indépendant de `Resolution` (cause du cadrage en haut à gauche)
- doc : `Resolution` indexe la table de décalages de `winsys3d`, pas le mode d'affichage ; 5 entrées valides (0..4), l'index 5 produit des valeurs parasites
- fix : clé ini `DAT_00563080` = `Size` (confirmée dans le binaire) — la reconstruction écrivait un doublon `UNKNOWN_REVERSE_005630a0` et relisait sa propre copie, d'où un rendu en 640x480 letterboxé au lieu du plein 1024x768
- doc : `Size` identifié comme le pas de zoom du rendu, réglable en jeu par F11/F12 et plafonné par résolution
- fix : format des clés manette `Joy%d` sans espace — les réglages joystick retombaient silencieusement sur leurs valeurs par défaut
- doc : table de bridage par résolution `DAT_00562e00` confirmée `{1,7,11,16,23,0}` par recoupement avec `BLAZE.cfg` (Resolution=4 ↔ Size=23)
- feat : façade en pass-through devant le device 3D (`DeviceShim.cs`) — échange de vtable, journalisation des appels réels de winsys3d
- feat : décompilation Ghidra en mode headless pilotée depuis le dépôt (`Legacy/Raw/3dhelp/ExportVtableFuncs.java`) — débloque les corps atteints seulement par adresse
- doc : cartographie des 34 slots de vtable du device 3D dans `Documentation/DLL/3dhelp.md` — six méthodes portent l'affichage, les grosses fonctions ne sont pas sur le chemin chaud
- doc : `ANALYSE_ECRAN_NOIR_MENU.md` — synthèse du bug d'écran noir intermittent, mesures et pistes écartées
- fix : relance bornée à 3 tentatives et réclamation de l'arène dès l'initialiseur de module (placement fiable)
- fix : index de slot joueur non borné dans `DrawUiPanel` qui servait d'offset d'écriture (corruption du tas managé, `0x80131506`)
- fix : débordement de pile dans la collecte des échappements `$` du moteur de texte (`Fatal error` CLR aléatoire)
- fix : table de largeurs de glyphes prise dans `FUN_00507890` et non dans la copie périmée de `.data`
- fix : double avance du pointeur d'écriture dans le chemin « espace » du rastériseur
- fix : crayon remis à zéro entre les lignes — `DAT_005e8320` confondu avec `DAT_005e8328` en quatre endroits
- fix : `DAT_00566cc4` est la même globale que `s_ResolutionIndex`, désormais transmise à `Ordinal_789` (viewport correctement positionné)
- fix : `DAT_005e8fc0` est `DAT_005e8f00` décalée de 48 entrées, pas une seconde table (textures du menu enfin résolues)
- fix : `DAT_0056e61f` est `menuContext[0x8c7]`, pas une globale distincte (le menu ne confirme plus dès la première frame)
- fix : descripteur de panneau `DAT_0056dd58` initialisé depuis `.data` au lieu d'être zéroé
- feat : renderer de panneau UI `FUN_00545340` et ses quatre émetteurs de primitives
- feat : rendu de glyphes DBCS `FUN_00544150`, signature confirmée par désassemblage
- feat : rastériseur de glyphes `FUN_00543b20` avec césure et césure arrière
- feat : moteur de texte UI `FUN_005443a0` — deux passes, deux tables de dispatch de 32 entrées
- fix : `MainMem` retargeté vers une arène allouée en mémoire basse, contrainte 24 bits de la display list
