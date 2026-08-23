# Historique

Une ligne par fonctionnalité ou correction. Le plus récent en haut.

---

## 2026-08-23

- feat : code de contrôle de texte `0x0a` (`FUN_00544bc0`) — un dialogue peut lancer un script d'entité, et le script peut détourner le flux de texte. L'auberge tient 2341 images à 1017 primitives par image, deux exécutions
- doc : la table de balayage `PTR_LAB_0056e870` donne la taille de l'opérande de n'importe quel code de contrôle de texte, sans lire le corps de rendu
- feat : **le premier dialogue de PNJ du jeu** — « L'herboriste : Bonjour ! » s'affiche à l'auberge, 2221 images sans plantage, 956 primitives par image
- feat : opcode 22 de l'interpréteur (`FUN_00515b40`) — l'opération arithmétique sur le bloc de statistiques de l'entité, absent de `Blaze.c`
- fix : le message de lacune des codes de contrôle de texte conseillait encore une re-passe Ghidra ; il renvoie maintenant au verbe `disasm`, qui résout ce cas depuis le 2026-08-21
- feat : **la caméra de scénette au complet** — la boucle de verbes `FUN_005036e0` et les trente-deux verbes de `PTR_LAB_0055eb68`, tous absents de `Blaze.c` et relevés au désassembleur
- feat : les trois verbes de pas de la scénette — saut immédiat (3), glissement par quarts avec chemin d'angle le plus court (4), déplacement en N images (5)
- feat : les verbes de flot de la scénette — étiquette et saut (7, 8), répétition (9, 10), appel de sous-script (2), sortie et rebranchement de la caméra libre (1)
- feat : les verbes de consigne — visée et pose sur entité (13 à 16), littéraux absolus et relatifs (17 à 22), rappel de la caméra libre (23 à 26), secousses tirées au hasard (29 à 31)
- doc : l'écart entre deux entrées d'une table de pointeurs ne mesure pas la fonction — le verbe 31 tenait en 158 octets là où l'écart annonçait 2240
- doc : le verbe 6 de la scénette boucle indéfiniment sur une durée nulle, blocage latent du jeu de 1998 conservé tel quel
- feat : verbe 0 de la table de zone (`FUN_00443100`) — le dégel des trois tables d'effets qu'un dialogue de PNJ demande
- feat : cascade de réveil des entités de scène — `FUN_00404f70`, `FUN_004051d0`, `FUN_00404fe0`, `FUN_004050d0`
- fix : **caméra de l'auberge conforme au legacy** — `FUN_00401000` restitue `ViewAngle` depuis `SavedViewAngle` avant de semer le but ; Ghidra avait supprimé ce bloc comme code mort, à tort (le pointeur `DAT_005E9AE0` aliase la destination). Élévation 597 et cap 1024 comme l'original, capture identique à `legacy.png`
- doc : `DAT_00604928` est (élévation, **cap**, roulis) et non (cap, tangage, roulis) — la rose des vents et l'octant du balayage 7x7 lisent tous deux `+2`
- doc : écart de 90° de la caméra à l'auberge identifié comme le cap à 1024 ; semer `ViewAngleGoal` le corrige et rend la rose des vents identique au legacy
- doc : tangage de la caméra à l'auberge résolu — `ViewAngleGoal` (0x006067e0) n'est jamais semé ; le semer donne 1024 comme l'original et triple le débit (388 -> 1037 primitives/image)
- fix : `MeasureCameraToParty_00402710` comptait `dx * dx` deux fois dans la distance caméra-groupe
- fix : les trois décalages de caméra `&DAT_00564650 + 8/0xc/0x10` sont des entiers, lus sur seize bits dans trois fonctions
- doc : divergence de caméra à l'auberge localisée dans `ViewAngleGoal` (0x006067e0) — le reste du relevé x32dbg en découle arithmétiquement
- doc : `OpenForesia.Tools` se lance via `dotnet publish -c Release` quand Smart App Control refuse le build Debug
- feat : **la carte du monde s'affiche et tient** — l'auberge, 2101 images sans plantage, 388 primitives par image. Première image du jeu en 3D
- feat : boîte de dialogue « - La réception - » à l'entrée de l'auberge, et immobilisation du joueur tant qu'elle est affichée
- feat : cadre des jauges PV/PM, au même style de fenêtre que la boîte de dialogue
- feat : `DrawWorldCells` — le balayage 7×7 des cellules de décor, les deux grilles d'entités, le ciel
- feat : `FUN_0051e700` — l'émission de modèle triée par face
- feat : les trois listes de morceaux `FUN_0051fa40` / `FUN_00521000` / `FUN_00522140`, réunies en un seul `DrawChunk` paramétré
- feat : `FUN_0051f940` — le voile plein écran du fondu
- feat : `FUN_0053aa10` — le pas d'états d'une entité de scène (poisons, minuteurs, régénérations, seuils de zéro)
- feat : `FUN_00486ee0` et `FUN_004db5a0` — la bestiole errante et l'avance d'image d'animation, absentes de `Blaze.c`, désassemblées sur place
- feat : `FUN_0047ac40` et `FUN_00404ef0` — la naissance d'entité dans `DAT_005f2d40` et le réveil des entités de scène
- feat : codes de contrôle de texte `0x08` (gel/dégel) et `0x1f` (étendue du panneau), décodés à la main
- fix : `ResolveEntityBlocks` lisait les blocs de statistiques par une vue d'arène quand toute la simulation les lit par leur allocation propre — chaque entité de scène naissait à 0 PV et se faisait tuer à la première image
- fix : `DAT_00563c5c` et `DAT_00563c77` sont des champs du descripteur `0x005633b0` (+0x8ac et +0x8c7), pas des globales — la boîte de dialogue liait son texte et ne s'affichait jamais
- fix : `&DAT_005dbc68` portait deux noms, l'un rempli au chargement, l'autre lu au dessin
- fix : la boucle d'initialisation des quatre panneaux avait des décalages faux, dont `+0x08` — l'étendue que `DrawUiPanel` lit — jamais écrite
- fix : les trois décalages de caméra `&DAT_00564650 + 8/0xc/0x10` sont des entiers, lus et écrits sur seize bits dans trois fonctions
- fix : `GameArenaSize` ramenée de `0x1600` à `0x1560` — l'arène recouvrait la table des blocs de statistiques
- doc : deux défauts d'origine catalogués — LB-009 (le balayage ne saute qu'une cellule sur huit) et LB-010 (un indice de couleur réduit à un booléen)
- doc : tables `&DAT_00564548`, `PTR_DAT_005645d8` et `&DAT_0056d340` relevées dans le binaire

> Les séances du 11 au 22 août ne sont pas consignées ici : elles ont été documentées dans
> `REVERSE_STATUS.md` au fil de l'eau, mais la reprise d'`HISTORY.md` date du 23 août et
> reconstituer ces onze jours après coup reviendrait à les deviner.

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
