# Historique

Une ligne par fonctionnalité ou correction. Le plus récent en haut.

---

## 2026-09-28

- fix : LB-056 — le jeu ne s'ouvrait parfois pas (« No free block of 0x280000 bytes … below 0x01000000 », trois essais perdus) ; le processus lancé devient un lanceur qui crée le jeu suspendu, lui réserve l'arène et le bassin des descripteurs sous 16 Mo avant que le runtime ne démarre, puis l'attend ; l'ancienne recherche reste le repli ; à tester en jeu
- feat : plus de clé `[Boutique] Actif` dans les `Boutique*.ini` — `[Auberge] Hub = oui` suffit, les cinq marchands sont là ; un ancien fichier qui la porte encore est lu, la clé ignorée ; fichier absent : catalogue vide
- feat : l'écran de boutique demande « Ton choix se porte sur cet objet ? Oui / Non » avant d'acheter ou de vendre (flèches, action pour valider, menu = non)
- fix : la Voleuse tourne d'un quart de tour à droite (cap 1024 → 2048)
- fix : `generer-boutique.mjs` compte aussi les rangées de table de butin des zones 1 à 10 (`butin.csv`) — le verbe 12 de zone et l'opcode 27 des scripts y puisent sans monstre ni coffre posé ; Excalibur (rangée 0 de la grotte scellée, obtenu en jeu), les six autres armes de ses rangées 1 à 7, la Baguette guérisseuse et le Réflector passent en vente, donc deviennent rachetables ; les tables de l'auberge (objets de test) restent exclues
- feat : Béquille (inutile) et Debacle (ATT 999, arme de test) retirées de la boutique d'armes, et du générateur ; elles ne sortent que du coffre 235 de l'auberge
- feat : LP-011 — la Voleuse (identité 10) se tient dans le hub, tournée vers la porte, texture dans la seconde zone libre de la VRAM (x 0, y 320) ; elle rachète l'inventaire hors équipement et le coffre, à 50 % du prix d'achat (`BoutiqueVente.ini`, `[Vente] Taux`), piles entières ; une vente depuis le coffre écrit la sauvegarde ; à tester en jeu
- feat : l'écran de boutique a un mode rachat ; la place en VRAM devient propre à chaque marchand
- feat : LP-010 — l'écran de boutique s'agrandit (304 × 224) et montre, pour l'objet sous le curseur, sa description et ses valeurs non nulles lues dans la fiche : ATT, DEF, MATT, MDEF, les sept caractéristiques, les huit éléments
- feat : LP-010 — le Lutin filtre aussi par la classe de l'acheteur : les sept herbes ne sont proposées qu'au Chasseur, le seul que leur fiche autorise
- feat : LP-010 — le Voleur filtre aussi ses accessoires par la classe de l'acheteur (33 des 119 sont réservés, gantelets de fer, objets d'elfe…), à la demande de Vincent
- doc : les marchands du hub (LP-010) archivés, validés en jeu par Vincent ; spec `gameplay-hub-merchants` créée, `gameplay-inn-shop` perd l'armoire, `game-data-install` dépose les quatre boutiques (5.5 et 5.6 non essayés en jeu)
- feat : LP-010 — les quatre boutiques des marchands du hub : un fichier chacune dans `LegacyPlus\` (installeur compris), catégorie relue dans la fiche, armes et armures filtrées par la classe de l'acheteur (demande de Vincent), ouverture face au marchand ; à tester en jeu
- feat : `generer-boutique.mjs` écrit les quatre catalogues — l'équipement au prix de LP-008, les objets utilisables à un barème par famille ; les prix déjà écrits sont repris
- feat : la boutique de l'armoire (LP-008) est retirée ; `Boutique.ini` n'est plus lu ni déposé
- feat : LP-010 — les quatre marchands se tiennent dans le hub (Lutin validé en jeu ; Voleur, Kobold et Sylphide ajoutés à 150 unités d'intervalle, à tester)
- fix : LP-010 — le Lutin passait devant tout : sa transformée n'avait pas le bit 1 de `+0x1a` (le `+0x36` qu'un membre porte), qui fait calculer la profondeur des parties posées ; les membres ne le traversent plus (poussée hors d'un cercle de 48 unités)
- feat : LP-010, premier lot — le Lutin se tient dans le hub, au repos : sa classe relue dans le répertoire de l'état 1, texture posée dans la bande libre de la VRAM, dessiné par la couture `DessinSurImpression` ; `RelocateModelBundle` et `RelocateAnimationBundle` passent en `internal` ; à tester en jeu
- doc : LP-010, étape 1 faite — un modèle de classe n'emploie qu'une texture, désignée par une entrée de `0x005e8f00` et un décalage V ; dans le hub, la bande x 832..1023, y 256..511 de la VRAM est libre et reçoit les quatre marchands ; leur repos est le clip 0
- feat : `OpenForesia.Tools classes` — les ressources des seize identités de classe (modèle, animations, deux textures) ; diagnostic Debug `[Diagnostic] ReleveVram` (image et carte de la VRAM à l'arrivée dans chaque carte) ; le relevé de position donne aussi l'animation en cours
- doc : LP-010 — les ressources de classe vivent dans le répertoire de l'état 1 ; un cinquième emplacement de personnage tomberait hors de la VRAM
- doc : change `add-legacy-plus-hub-merchants` (LP-010) proposé — quatre marchands dans le hub sous des modèles de classes jouables (Lutin, Voleur, Kobold, Sylphide), une boutique par catégorie d'objet, quatre fichiers dans `LegacyPlus\`, l'armoire LP-008 retirée ; étape 1 = mesure de la VRAM
- doc : le jeu ne charge que les classes du groupe, dans quatre emplacements dont la place en VRAM dépend du numéro ; membres du groupe et entités de scène ont des tables de modèles distinctes
- doc : le hub de l'auberge (LP-009) archivé, validé en jeu par Vincent ; spec `gameplay-inn-hub` créée (5.1, 5.6 et 5.7 non essayés en jeu)
- feat : LP-009 — le coffre du hub est déplacé en (-1337, 886), à la demande de Vincent ; la liste de la 82 est recopiée, les rangs ne bougent pas
- fix : LP-009 — en sortant du hub, le groupe se retrouvait perché sur l'estrade (le jeu prend une face de sol sous le point, et l'estrade passait au-dessus) ; retour en (-109, 504), juste après elle
- feat : LP-009 — dans le hub, les collisions des meubles disparaissent (leurs faces de sol surélevées sont ramenées au plancher au chargement), plus de boîte « - Le coffre - » à l'arrivée ; arrivée et retour aux places et caps relevés par Vincent
- feat : `OpenForesia.Tools terrain <zone> <sous-zone>` — les faces de collision d'une sous-zone en CSV (attributs, sol, étendue)
- doc : les meubles de la 82 sont des faces de sol surélevées de son maillage de collision ; le titre d'une pièce est le script d'arrivée de son point d'entrée
- fix : les journaux de la console partent en UTF-8 — la fenêtre d'exécution de Rider affichait « pr�s » pour « près »
- fix : LP-009 — la porte ajoutée ne réagit plus au contact (un battant de l'auberge s'ouvre quand on le pousse, et il s'ouvrait sur le mur) ; elle se passe depuis une bande de sol devant le battant, tourné vers le mur, et non plus en visant son gond
- feat : LP-009, le hub de l'auberge — une porte dans la salle de chaque réception mène à la sous-zone 86, la carte 82 vidée hormis porte et coffre ; sa porte ramène devant la nôtre, dans la réception d'origine ; une sauvegarde faite dans le hub reprend dans la réception ; huit coutures, `[Auberge] Hub`, à tester en jeu
- feat : les diagnostics `TraceCombat` et `TracePosition` ne sont installés que dans la version Debug ; `TracePosition` écrit aussi dans la console (`[POSITION]`, avec zone et carte), visible dans Rider
- doc : change `add-legacy-plus-inn-hub` (LP-009) proposé — une porte dans chaque réception de l'auberge vers un hub, la carte 82 vidée sous un numéro à nous, retour à la réception d'origine ; étape 1 = mesure
- doc : LP-009, étape 1 — la zone 0 a 105 sous-zones ; 86 et 87 sont des emplacements vides (hub : 86) ; 88 à 104 sont dix-sept copies inutilisées de la 82 ; le coffre de stockage écrit la sauvegarde lui-même, avec la sous-zone chargée ; seul l'opcode 17 fait sortir d'une pièce
- doc : le numéro d'une carte de l'auberge = palier de l'histoire (0, 8 … 80, `FUN_004747F0`) + pièce ; la carte 80 n'est que la réception du dernier palier, et la boutique LP-008 (carte 82) n'existerait donc qu'en fin de partie — à confirmer

## 2026-09-27

- doc : la boutique de l'auberge (LP-008) archivée ; spec `gameplay-inn-shop` créée, `game-data-install` gagne le dépôt de `Boutique.ini`
- feat : l'installeur dépose `Boutique.ini` (boutique désactivée, catalogue), sans écraser celui du joueur
- doc : LP-008, étape 3c validée en jeu par Vincent — achat, inventaire, icône, sauvegarde
- feat : LP-008, étape 3c — l'achat réel : la bourse est débitée, l'objet rangé dans l'inventaire par les règles du coffre (empilement, dernier emplacement libre, inventaire plein refusé), son icône envoyée à la place du personnage en mémoire vidéo, identifié ; à tester en jeu
- doc : LP-008, étape 3b validée en jeu par Vincent
- feat : LP-008 — le pied de page de la boutique ne garde que « Page x/y », à la demande de Vincent
- feat : LP-008, étape 3b — l'index des objets (fiche et vignette) lu dans `FrenchSYSTEM.INI` et `BLAZE.ALL` au premier accès à la boutique, par le lecteur de l'outil `bestiaire` ; l'écran montre l'icône de l'objet sous le curseur, agrandie deux fois, à tester en jeu
- doc : LP-008, étape 3a validée en jeu par Vincent
- feat : LP-008, étape 3a — l'écran de la boutique : 10 objets par page, curseur, prix, bourse de l'acheteur, dessinés avec la police 8×8 du jeu sur un fond semi-transparent ; la manette est lue pendant le gel ; l'achat est vérifié et journalisé seulement ; une couture `DessinSurImpression` dans le legacy, à tester en jeu
- doc : LP-008, étape 2 validée en jeu par Vincent
- fix : LP-008 — ouvrir la boutique plantait le rendu du texte : le code `0x0b` (appel de sous-libellé) porte trois octets d'opérande, et le préfixe était coupé au premier octet imprimable, en plein opérande ; seule l'instruction `0b 01 a0 00` est maintenant reprise
- feat : LP-008, étape 2 — l'armoire de la carte 82 ouvre une boîte du jeu (« Bienvenue à la boutique de l'auberge ! ») quand un joueur face à elle appuie sur son bouton d'action ; `Boutique.ini` lu (`[Boutique] Actif`, `[Catalogue]`) ; une ligne dans le legacy, à tester en jeu
- doc : LP-008 — l'armoire de la carte 82 est le décor en (−1372, 400, 900), relevé en jeu par `TracePosition` ; le relevé tolère désormais l'oscillation d'un personnage immobile
- doc : LP-008 — décision de Vincent : pas de PNJ, l'armoire de la carte 82 est le point de vente
- feat : `Boutique.ini` — le catalogue du futur marchand de l'auberge (LP-008), 272 équipements à prix déduits de leur puissance, de leur zone et de l'or gagné par monstre ; généré par `OpenForesia.Tools/generer-boutique.mjs`
- feat : `[Diagnostic] TracePosition = oui` — la position du joueur 1 à chaque arrêt, pour placer le marchand
- doc : LP-008, étape 1 — l'or ne sert à rien en 1998 ; les modèles et textures sont chargés par carte, et la carte 82 n'a aucun personnage (le PNJ de l'auberge vit en 81, 83, 84, 85) ; le numéro d'objet est propre à chaque carte
- doc : les six changes legacy ++ (LP-001 à LP-007) validés en jeu par Vincent et archivés ; sept specs `gameplay-*` créées, et `game-data-install` gagne le dépôt de `LegacyPlus.ini`
- fix : LP-007 — trop de monstres faisait se dégrader la carte ; le tweak ne fait plus rien naître au-delà de 12 entités vivantes (`MonstresMax` borné à 12)
- doc : le moteur de 1998 a un budget de 2 500 primitives par image (`s_Unknown_005e9818`, posé par `FrameBegin`) ; épuisé, il ne dessine plus rien — préconisation pour le relever notée dans `Documentation/LegacyPlus/legacy.md`
- feat : LP-007 — les rencontres aléatoires réglables (`[Rencontres] Frequence`, `MonstresMax`, `Formations`) : délai divisé, rencontres possibles en plein combat sous un plafond, plusieurs formations par rencontre ; tout passe par le tirage et le placement d'origine ; cinq lignes dans le legacy, à tester en jeu
- doc : une rencontre aléatoire n'arrive que si AUCUNE entité de scène ne vit (l'octet bas du pool `0x005f2d00`) — chaque monstre vivant recharge le minuteur ; la table des monstres compte 32 emplacements, les formations du CD 1 à 11 monstres
- doc : la vitesse des monstres est abandonnée comme tweak, à la demande de Vincent
- feat : LP-006 — se déplacer en lançant un sort (`[Magie] DeplacementEnIncantation`, 0.5 = mi-vitesse) : la croix calculait déjà la vitesse pendant l'incantation, le tweak en garde une part au lieu du zéro d'origine ; une ligne dans le legacy, à tester en jeu
- doc : LP-005 validé en jeu par Vincent, partage des pièces à quatre compris
- fix : LP-005 — le joueur 1 ramassait toutes les pièces (le « tueur » `+9` n'est pas fiable) ; elles se partagent maintenant à tour de rôle entre les membres debout, de 1 à 4
- feat : LP-005 — ramassage automatique des pièces et des cristaux d'expérience une fois posés (`[Butin] RamassageAuto = oui`), par le code de ramassage d'origine ; jamais les coffres ; une ligne dans le legacy, à tester en jeu
- doc : le « rien que de l'argent » de `DespawnSceneEntity_0051a740` est le cristal d'expérience (`0x80081000`) ; les « éclats » (`0x90081000`) sont les pièces
- doc : LP-004 validé en jeu par Vincent, mêlée à quatre joueurs comprise
- fix : LP-004 — l'horloge du tweak ne tournait pas : `ImageSuivante` était posée à l'entrée de `RunWorldMapTick`, qui n'est entrée qu'une fois par zone ; elle est maintenant en tête du pas de simulation (trouvé par le journal `TraceCombat` : image = 2 sur toutes les lignes)
- feat : `[Diagnostic] TraceCombat = oui` dans `LegacyPlus.ini` — journal `legacyplus-combat.log` de chaque coup de mêlée du groupe (écarté, refusé, touché, raté), pour comprendre pourquoi un coup ne passe pas
- fix : LP-004 en mêlée — un guerrier ne pouvait pas enchaîner ses coups, son deuxième geste étant pris pour la même attaque ; en mêlée, le refus ne vaut plus que tant que le même geste est tenu sur son image de frappe
- fix : LP-004 n'avait aucun effet — les deux balayages écartent aussi une victime qui porte le bit `0x40` (« encaisse un coup »), posé à chaque coup et retombé seulement à la fin du clignotement : second verrou, désormais levé par la couture `VictimeExclue`
- feat : LP-004 — les frames d'invulnérabilité des ennemis tenues par attaquant (`[Combat] InvulnerabiliteEnnemis = non`) : à plusieurs, chaque joueur, flèche ou sort touche un ennemi qui clignote, et une même attaque ne compte jamais deux fois ; sept lignes dans le legacy, à tester en jeu
- doc : le verrou d'invulnérabilité `+0x96` d'un ennemi est partagé par tous les attaquants, et tout ce que le groupe lance contre un ennemi passe par deux balayages (`FUN_00519940` pour la mêlée, `FUN_00519af0` pour projectiles et sorts)
- feat : LP-003 — le montant des sorts du groupe monte avec la MAG du lanceur (`[Magie] ScalingMagique`, 0 = jeu d'origine, 1 conseillé : Balle de feu ~7 → ~23 au niveau 200), une ligne dans le legacy, à tester en jeu
- doc : pourquoi un petit sort ne progresse pas dans le jeu de 1998 — `MAG / diviseur` (3 ou 4) ne dépasse jamais la défense magique d'un monstre de fin de jeu, seul le montant fixe reste
- feat : les tweaks de gameplay ont leur cadre — `src/LegacyPlus/`, les coutures (trois lignes dans le legacy, aucune condition d'option) et `LegacyPlus.ini`, lu sans être écrit, déposé par l'installeur, tout inactif par défaut
- feat : LP-001 et LP-002 — dégâts reçus par le groupe et PV des monstres multipliés, de ×0,1 à ×10 (`[Difficulte] DegatsMonstres`, `PvMonstres` ; 1 = jeu d'origine), à tester en jeu
- doc : proposition OpenSpec `add-legacy-plus-tweaks-difficulty` — le cadre des tweaks de gameplay (répertoire `src/LegacyPlus/`, coutures sans condition dans le legacy, `LegacyPlus.ini`) et son premier tweak, PV et dégâts des monstres en % ; registre des tweaks ouvert dans `Documentation/LegacyPlus/legacy.md`
- feat : l'installeur dépose un `OpenForesia.cfg` par défaut (filtre actif, 1024x768 : `Resolution=4`, `Size=23`) et son `Affichage.ini` ouvre le jeu en `fenetre-sans-bordure` — jamais par-dessus les réglages du joueur

## 2026-09-26

- doc : `simplify-display-settings` validé en jeu par Vincent — réinstallation sans traits sous les textes, trois modes de fenêtre, second écran — et archivé ; spec `display-enhancements` mis à jour
- feat : `Mode = plein-ecran` devient un vrai plein écran **exclusif** (swap chain DXGI, définition du bureau), quitté quand le jeu perd le focus et repris au retour ; `fenetre-sans-bordure` reste le mode où l'on travaille sur un second écran sans que le jeu s'efface
- fix : `Fenetre = auto` restait à 1024x768 (multiple entier de `Size=23`, ×2 ne tient pas en 1080p) — c'est désormais le plus grand 4:3 qui tient, le multiple entier seulement en `MiseAEchelle = entiere` ; `fenetre-sans-bordure` se mesure sur l'écran entier et non la zone de travail (un 1920x1080 devenait 1834x1032) — en `auto`, elle couvre l'écran
- feat : `Affichage.ini` simplifié — `Rendu = moderne` (taille affichée, MSAA 4, filtrage selon `Filter=`, interface nette) ou `origine` (l'image d'avant LB-055) ; les quatre clés fines de la première version sont ignorées comme « retirées »
- feat : la fenêtre s'ouvre dans le mode choisi (`Mode` : fenêtre, fenêtre sans bordure, plein écran sans bordure) et à la taille `Fenetre` (`auto` = plus grand multiple entier qui tient à l'écran, 1280x960 au lieu de 640x480) ; Alt+Entrée bascule du mode de départ au plein écran
- doc : proposition `simplify-display-settings` — `Affichage.ini` ramené à `Rendu` (moderne / origine), `Mode` (fenêtre, sans bordure, plein écran), `Fenetre` et `MiseAEchelle` ; constat : les traits sous les textes de la version installée venaient d'un Setup publié à 16:11, avant leurs correctifs (16:38)
- feat : `Affichage.ini` — résolution interne (`auto` = taille de l'écran, ou ×1 à ×8), MSAA 1/2/4/8, mise à l'échelle ajustée ou entière ; le jeu le lit, ne l'écrit jamais, et ses menus n'en savent rien (LB-055)
- feat : le filtrage de texture du jeu arrive enfin — slot device `+0x90` branché, donc `Filter=` agit ; filtrage bilinéaire fait dans le shader, sans liseré de couleur-clé ni débord de page ; interface réglable à part (`net` / `lisse`)
- feat : l'installeur dépose un `Affichage.ini` commenté, sans jamais écraser celui du joueur
- doc : `enhance-display-resolution-filtering` vérifié en jeu par Vincent — rendu d'origine identique à ×1, aucun défaut en menus, donjon et combat aux réglages par défaut, réglage conservé à la réinstallation (27/27 tâches)
- fix : traits fins le long des textes et des cadres en haute résolution — deux causes : le MSAA interpolait les uv hors du quad (interpolation `centroid`), et le décalage de +0,1 texel des uv de sprite de winsys3d débordait sur la rangée suivante de la planche dès que la cible est agrandie (échantillon borné au rectangle du triangle, seulement hors de la taille d'origine)
- doc : F10 (bascule du filtrage) et F8 (rechargement des textures) sont inertes dès 1998 — winsys3d ne les lit que si `DAT_00dedc40` est posé, et `Blaze.exe` n'appelle jamais les ordinaux 36/759 qui le posent (NOTE_DEVELOPPEUR § 9)
- doc : le « jamais filtré » du moteur n'est pas l'interface — il couvre aussi les personnages 3D (pages 8 bits zoomées) ; le filtrage de l'interface se décide désormais par sprite / polygone (le genre de la liaison)

- doc : proposition `enhance-display-resolution-filtering` — résolution interne jusqu'à la taille de l'écran, MSAA, mise à l'échelle entière ou ajustée, filtrage des textures 3D et de l'interface, réglés par un `Affichage.ini` que le jeu lit sans l'écrire ; constat : le slot device `+0x90` (filtrage demandé par le moteur, `Filter=` et F10) n'était branché nulle part

- feat : `OpenForesia.exe` a son icône (`foresia.ico` de Vincent, centré au carré, 16 à 256 px), reprise
  par la fenêtre du jeu (barre de titre, barre des tâches) ; plus de console au lancement (`WinExe`) ;
  `VS.Log`, le journal de synchronisation verticale que `winsys3d` écrivait à chaque fermeture, ne
  s'écrit plus que sous `OPENFORESIA_VSLOG=1` (LB-054)
- doc : changement `install-from-iso` **terminé et archivé** (40/40, validé par Vincent sur machine
  propre) ; nouvelles specs `game-data-install` et `game-data-location`, `blaze-intro-attract` mise à jour
  (sans données, le démarrage s'arrête sur un message) ; le désinstalleur s'appelle `Uninstall.exe`
- feat : le dossier installé est en ordre — le jeu publié tient en un seul `OpenForesia.exe` (ni DLL ni
  `createdump.exe`), et `Uninstall.exe`, une copie de l'installeur qui se relance depuis `%TEMP%` pour
  s'effacer, remplace `Desinstaller.ps1` ; l'installeur publié descend à 15,5 Mo
- feat : **l'installeur passe de 82 à 18 Mo** — le jeu est publié élagué (108 → 21 Mo sur le disque du
  joueur ; les liaisons Direct3D de Silk.NET réduites à ce qu'on appelle), et l'installeur perd WinForms,
  qui ne s'élague pas : même fenêtre, refaite en contrôles Win32 natifs, COM et JSON générés à la
  compilation ; un avertissement d'élagage bloque désormais la release
- feat : la release (`install-from-iso` lot 5) — `OpenForesia.Tools/publish-release.ps1` publie le jeu en
  autonome x86, l'embarque dans un `OpenForesia-Setup.exe` à fichier unique compressé (82 Mo) et contrôle
  qu'aucune donnée du jeu n'y est entrée
- doc : `CLAUDE.md` (les données du jeu, l'installeur, la release), `ARCHITECTURE.md`, `DataSymbols.md`
  (`cd.idx`, la racine des données, la carte des symboles), `REVERSE_STATUS.md`
- feat : la fenêtre de l'installeur (`install-from-iso` lot 4) — image du disque reconnue, dossier
  (par défaut `%LOCALAPPDATA%\Programs\OpenForesia`, alerte sous `Program Files`, 200 caractères au plus,
  place vérifiée), installation avec barre et annulation qui ne laisse rien, raccourcis bureau et menu
  Démarrer, entrée « Applications installées », désinstallation par `Desinstaller.ps1` qui propose de
  garder les sauvegardes
- feat : les outils lisent les données installées (`install-from-iso` lot 3) — `bestiaire` sans argument
  lit `OpenForesia\bin\Debug\net10.0` (ses 11 CSV sortent identiques à ceux tirés du CD), les scripts de
  films n'ont plus `E:\` par défaut, `playtest.ps1` refuse de lancer sans données et donne la commande
  d'extraction ; `test-protocol.md` et `OpenForesia.cfg.md` : `save\` et le `.cfg` sont toujours à côté
  de l'exe, `OpenForesia\save\` ne sert plus
- feat : **le jeu ne connaît plus de CD** (legacy ++, `install-from-iso` lot 2) — plus de balayage des
  lecteurs ni de « insérez le CD » ; le répertoire courant est le dossier de l'exe, tous les chemins
  sont relatifs (données, `save\`, `.cfg`), et `Host/GameDataCheck.cs` vérifie au démarrage les 133
  fichiers attendus ; la protection `cd.idx` (multiplicateur de curseur de texte), `CdSearchFile` et
  la branche `cdrom:` de la carte mémoire sont retirées ; les sondes se taisent hors du dépôt
- feat : `OpenForesia.Installer` (lot 1) — lecteur ISO 9660 / Joliet, `editions.json` généré depuis
  l'ISO de référence (133 fichiers, 281 Mo, films coupés à leur taille RIFF), et deux modes cachés :
  `--generer-edition` et `--extraire-donnees` (extraction vérifiée au MD5, 2 s)
- doc : passage en mode **« legacy ++ »** — le projet vise un jeu autonome et clé en main ; corriger un
  défaut de 1998 ou retirer une dépendance (CD, protection, DLL) devient légitime, en second temps,
  déclaré et validé par Vincent. Règles dans `Strategy.md` (section « Mode legacy ++ » et « Historique
  des règles »), reprises dans `CLAUDE.md`, `Prompt.md` et l'en-tête de `legacy-bug.md` (statuts
  `Reproduit` / `Contourné` / `Corrigé` / `Retiré`) ; les anciennes règles y sont historisées
- doc : changement OpenSpec `install-from-iso` — abandon du CD, installeur depuis l'ISO du joueur ;
  relevé de l'ISO de référence (fausses tailles de `CD.IDX` et des AVI, `FRENCH2` doublon exact)
- fix : appuyer sur Alt (la croix du jeu) ouvrait le menu système de la fenêtre et figeait le jeu — la
  fenêtre de l'hôte avale désormais `SC_KEYMENU`, sauf Alt+Espace
- feat : les types de manette 1 et 2 du menu de configuration marchent — `0x0050DE90` (cap posé à chaque
  image) et `0x0050DD00` (désassemblée : le personnage tourne vers le cap d'un seizième de tour par image) ;
  l'entrée 3, `0x0050DFF0`, reste non écrite car aucun menu ne l'atteint (non exercé en jeu)
- feat : `FUN_00537a20` transcrite (`StartWithCreatedCharacter`, `State01/PartySetup.cs`) — la branche
  `DAT_005de51c != 0` de l'état 1, atteinte par la sortie « annuler » de la création : on part seul avec
  le personnage tout juste créé, vers l'état 3 ; le `NotImplementedException` disparaît (non exercé en jeu)
- doc : audit des 59 `NotImplementedException` du code — deux vrais trous atteignables (annuler la création
  d'un personnage en nouvelle partie appelle `FUN_00537a20`, non transcrite ; les types de manette B et C,
  `0x0050DE90`/`0x0050DD00`, ne sont pas branchés), l'occlusion de caméra probablement atteignable, et
  deux contresens de doc : `FUN_00536170` n'est pas un « arrêt fatal » (elle pose un octet et rend 6), et
  la branche `DAT_005de51c` n'est pas inatteignable ; le reste est défensif ou du code mort de 1998
- doc : les scènes de mort « le membre aspiré » (`0xcb`, `0x004424B0`) et « dans la lumière » (`0xca`,
  `0x00441EB0`) des salles 13 à 15 sont vraisemblablement inatteignables — le script de chaque salle
  lance une seule scène (`0xcf` en 13, `0xcc` en 14, `0xd0` = la fin du jeu en 15), et ni catalogue,
  ni sort, ni coffre ne porte `0xcb`/`0xca`
- feat : `bestiaire --regions` relève aussi les trois catalogues d'effets `+0xcc/+0xd0/+0xd4` (en
  sous-types `0xc0 + n`) et, heuristiquement, les opcodes 16 des scripts `+0x8c` vers un sous-type `>= 0xc0`
- fix : les gerbes de la scène de mort « le membre aspiré » (`0x00442870`, sous-type `0xc5`, grotte
  scellée 13 à 15) effaçaient le bit `0x400000` au lieu de `0x4000` (`and dh,0BFh`) et restaient
  cachées ; trouvé par l'audit des octets hauts — scène vraisemblablement inatteignable, voir ci-dessus
- doc : audit des opérations sur octet haut de registre (`or/and/xor/test ah|bh|ch|dh`) — les 436
  sites des 962 relevés de `Legacy/Raw/` comparés au C# ; un seul défaut de plus : la
  couronne de la grotte scellée `0x00442870`, corrigée ci-dessus ;
  `0x00541DC0` (Ruines souterraines) n'est pas transcrite
- fix : le « rectangle arc-en-ciel » à la mort du Grizzly et du Behemoth — `TickPartySummon`
  (`0x004153F0` et ses trois copies) posait le bit `0x100000` sur le boss au lieu de `0x1000`
  (`or dh,10h`), si bien que la grille le dessinait comme le modèle 0 du paquet d'effets ; trouvé
  par `trace-modeles`
- feat : `tests\trace-modeles.bat` (`OPENFORESIA_TRACE_MODELES=1`) — une ligne [MODELE] par entité de
  la grille nouvellement dessinée (gestionnaire, modèle, décalage de texture, planche), pour nommer
  le rectangle arc-en-ciel à la mort des boss de la forêt ; la première hypothèse (le noyau de
  l'explosion) est réfutée par la capture
- fix : le flash blanc du portail qui suit un boss (Eifrit et les autres) devient un vrai fondu — la
  scénette de sortie `FUN_00474380` poussait le second voile, que la DLL de 1998 dessine opaque ; même
  voile de confort que le cristal, même cadence que le binaire (LB-052), trouvé par `trace-voile`
- feat : `trace-voile` suit aussi la couleur d'ambiance directe `0x005e9160..64` (modes 1 et 2), que la
  première version ne voyait pas, et l'opcode 21 du script de monde signe sa propre ligne — pour
  trouver d'où vient le blanc sans fondu de la fin d'Eifrit
- doc : l'état 5 (`FUN_00504c90`) est un gestionnaire de cartes mémoire hérité de la PlayStation, inatteignable ;
  laissé non transcrit par choix — l'éditeur de sauvegarde HTML couvre le besoin
- feat : `tests\visionneuse-modeles.bat` (`OPENFORESIA_FORCE_STATE=8`) — ouvre la visionneuse de modèles
  directement, faute de chemin dans le jeu
- feat : l'état 8, la visionneuse de modèles 3D (`FUN_005315d0`, `Screens/State08/ModelViewer.cs`) —
  un outil d'atelier que rien n'atteint : aucune instruction du binaire n'écrit 8 dans le numéro d'état
- feat : l'état 9, la fin de partie (`FUN_0051bb00`, `Screens/State09/GameOver.cs`) — mourir avec tout
  le groupe ne plante plus : image fondue, piste 0x26, 500 images ou START/CARRÉ, puis le menu principal
- feat : `tests\trace-voile.bat` (`OPENFORESIA_TRACE_VOILE=1`) — une ligne [VOILE] par changement des deux
  voiles plein cadre ou de la couleur d'ambiance, avec le gestionnaire et la sous-zone ; la fin
  d'Eifrit (10:11) blanchit l'écran par un chemin qu'aucun des correctifs de LB-052 ne couvre
- fix : le cristal restait tout blanc — c'était la scène du cristal (`ShiftCutsceneVeils`, forêt et
  montagne du dragon), pas le flash de fin de zone ; même fondu de confort, opacité 1 à l'image où
  la pose du groupe change (LB-052)
- fix : le flash blanc du cristal et de fin de boss, et la transition de la forêt (tablette des
  anciens, supposée), deviennent un vrai fondu vers le blanc — voile de confort de l'hôte avec alpha,
  au lieu du voile que la DLL de 1998 dessine opaque (LB-052, `s_ComfortWhiteFlash`)

## 2026-09-25

- fix : le stroboscope rouge des invocations de boss (Grizzly et second boss de la forêt, Labyrinthe,
  mine) est éteint lui aussi — même interrupteur, voile laissé à couleur nulle (LB-051)
- fix : le sort « spirale et éclair » des boss (Mort volante, grotte scellée 13-15, château de
  l'immortel) ne fait plus clignoter l'écran — écart déclaré, `s_BossDebuffScreenFlash` ; cercles,
  dégâts et effet 11 inchangés (LB-051)
- doc : passe guidée — 1 137 gestionnaires testés sur 1 429 (292 restants) ; 13 des 42 pistes restantes sont
  la « poussière du pas » (`0x005413E0`, effet `0xFE`), qui ne naît que sur une face de sol portant
  l'attribut `0x200` : le mécanisme marche (8 zones l'ont vu), il manque la carte de ces sols
- doc : se téléporter par le menu de débogage pendant une scénette (le wagonnet) laisse le curseur de
  scène dans les données de l'ancienne zone — « verbe 32 », qui n'existe pas ; mis en garde dans
  `mode-debug.md`, non corrigé
- feat : le guide des essais (`OpenForesia.Tools/Html/guide-gestionnaires.html`, `tests\guide-essais.bat`) —
  pour les 304 gestionnaires jamais vus : 51 avec un lieu, une consigne de joueur et un plan, 77
  impossibles dans le jeu d'origine, 176 sans piste ; relu depuis la sonde à chaque lancement
- doc : 77 gestionnaires ne peuvent pas tourner dans le jeu d'origine — 71 décors qu'aucune liste
  `+0x30` ne pose (la table `0x005fa5e0` n'est remplie que par elles) et 6 comportements qu'aucune
  espèce ne porte ; règle rejouée sur les 871 déjà vus, une seule exception expliquée
- feat : `bestiaire` écrit `decor.csv` — tout ce que la table `+0x30` pose, avec le gestionnaire
  résolu et la position (13 459 placements)
- feat : première tournée complète — 77 sous-zones sur 78 (1:7 jamais atteinte), 47 gestionnaires
  de plus testés : 1 125 sur 1 429, 304 restants, dont 6 IA seulement
- fix : le banc donnait 0 aux paramètres de placement (`+0x18`) — il reprend ceux d'un vrai placement
  de l'espèce ; le Durahan de 5:4 y lit ce qu'il fait naître, et avec 0 lâchait une graine de Balle
  de feu sans lanceur (LEVEE `0x47ADD0`, artefact du banc, pas du jeu)
- feat : `tests\tournee-auto.bat` — la tournée automatique du banc : téléportation de sous-zone en
  sous-zone (78 à ce jour, celles qui portent un gestionnaire non testé), peuplement par les
  vraies espèces (sans greffe), 40 s chacune, avancement repris dans `tournee-faite.txt` ; la liste vient de
  `OpenForesia.Tools/tournee-auto.sh`
- fix : le transfert du banc (`AI_BENCH=z:s`) plantait en `PlaceWorldMapParty` — il imitait
  l'opcode 17 sans remettre à `0xff` les mémoires de zone chargée ; il rejoue désormais la séquence
  du menu de débogage — vérifié : premier essai posé dans l'auberge
- fix : la tournée rejouait sans fin l'étape qui plantait — `tournee-faite.txt` note le départ
  (`en-cours`), une étape restée ouverte est marquée `plante` et sautée ; la greffe, qui avait fait
  tomber `DrawAnimatedModel` dans l'auberge, est coupée dans la tournée
- fix : le rapport des objets compte les herbes du Chasseur comme testées quand la sonde des sorts
  les a vues (famille 3) — 396 objets employés
- fix : `census-entity-handlers.sh` exclut `0x0040A600`, un second corps réduit à `ret` — 1 429
  gestionnaires, 351 restants
- fix : entrer dans les ruines anciennes plantait au chargement de sous-zone — l'appel
  `(*PTR_FUN_0056afc8)()` de `FUN_00516350` supposait l'entrée 0 toujours vide et passait par
  `Res.CallEntityHandler`, qui ne connaît que la souche ; le bloc `0x0055a3a8` y met `0x00541C90`
  (la fabrique des Sosies, déjà transcrite). L'appel passe désormais par `DispatchEntityHandler`,
  sans enregistrement (le corps ne lit pas d'argument)
- fix : l'opcode de script 8 (`FUN_00515610`, entrée de sous-zone) lisait la racine
  `(&DAT_005e97c0)[s[1]]` par `Mem.Arena(0x005e97c0)`, une cellule jamais écrite — les cinq
  racines vivent dans des champs distincts ; pointeur nul peu après l'arrivée dans les ruines.
  Il passe désormais par `UiText.ResourceRoot`, rendu `internal`
- fix : l'envol du Dragon rouge (`0x0040E3A0`) posait le bit `0x100000` sur le dragon au lieu de
  `0x1000` (`or dh,10h` mal lu) — le rendu le prenait pour une particule et dessinait un rectangle
  arc-en-ciel à sa place ; toutes les scènes cousines portaient déjà `0x1000`
- fix : éditeur de sauvegarde — « Enregistrer » sans accès au dossier téléchargeait un
  `Blaze-Saving….txt` (Blob sans type) ; le fichier sort désormais sans extension, prêt à recopier
- fix : éditeur de sauvegarde — le catalogue montrait 213 noms illisibles : des fiches **japonaises**
  jamais traduites, toutes en zone 0 (Shift-JIS lu en Latin-1) ; écartées au build, 397 fiches
  restent, et le contrôle vérifie qu'aucun nom illisible ne passe
- feat : éditeur de sauvegarde allégé à la demande — plus de provenance dans le catalogue, plus de
  sous-titres techniques ni de carte « États » dans l'onglet Personnage
- feat : dernier lot de personnages de test (15, les 49 objets restants) — objets déclenchés par un
  sort confiés aux lanceurs, qui connaissent tous leurs sorts ; consignes par événement précisées
  (17 à 19 : subir un sort ou une altération — Varan et Trent, forêt, sous-zones 6 à 8) ; jumeaux
  signalés ; modèles gardés dans `save-tests\modeles\`
- doc : `ObjetsAtteignables.md` régénéré après les lots de test — **387 objets français employés en
  jeu sur 396** : restent les 7 herbes (sonde des sorts) et 2 accessoires (Pendentif vaporeux,
  Chaussures à plumes) ; 18 des 21 gestionnaires que les objets font naître sont exercés
- feat : personnages de test à 9000 PV, départ au **Labyrinthe, sous-zone 5** — 12 Momies en
  variante 0 (effet de piège, jet A : événements 17 et 18) et 18 Sanglantes en variante 2 (état 9,
  jet B : 17 et 19), chacune appliquant son effet sur un coup sur vingt
- feat : `formations.csv` gagne `param_0x14` / `param_0x16`, le mot que la bête reçoit en
  `+0x14` / `+0x16` — sa variante ; il montre que le Crotale est placé partout en variante 0 et
  n'empoisonne donc jamais (d'où l'échec du lot précédent)
- doc : l'événement 14 d'un objet porté est la **parade** — Guerrier ou Nain en garde, face au
  monstre qui frappe (`SceneEntityAiCommon.cs`)
- feat : personnages de test — départ en forêt, **sous-zone 2**, la seule où le CD place des Crotales
  (sept placements, `formations.csv`) ; lieu réglable par `--depart zone/sous-zone`
- doc : les altérations qu'un objet porté peut attendre, par chemin — toute application d'état
  ouvre `PrepareCombatPair` (événement 17) ; le jet A (18) sert aux pièges, poisons et états
  `0x0053B3C0` — le Crotale de la forêt, sous-zones 0 à 4, en fait trois fois sur dix ; le jet B
  (19) aux effets 8 à 10 de la table, et parmi les bêtes à la seule Momie du Labyrinthe ; consignes
  des personnages de test réécrites en conséquence
- doc : l'événement `0x15` du script déclenché d'un objet porté part à la **mort** du porteur
  (`0x40000000`), pas quand il est « porté » — d'où la Poupée Dummy et la Plume dominicale
- doc : `ObjetsAtteignables.md` — les 7 herbes du Chasseur marquées « herbe, sonde des sorts » :
  elles partent par la posture d'herboriste, la sonde des objets ne peut pas les voir
- doc : bilan des lots de test — 340 objets français sur 396 employés en jeu
- feat : **les personnages de test** — `OpenForesia.Tools/generer-personnages-test.mjs` clone les
  huit modèles (un par classe) en 132 personnages à 100 PV, chacun avec jusqu'à cinq équipements
  jamais essayés (classe et type d'emplacement respectés) et cinq consommables, départ dans la
  forêt ; six lots de 24 (l'écran du groupe n'en liste que 32) avec leur `FICHE.txt` — le geste à
  faire pour chaque objet, lu dans l'événement `+0x84` ; `tests\lot-suivant.bat` les fait passer
- fix : `check-editeur-sauvegarde.mjs` ignorait le dossier passé en argument quand `--png` manquait
- fix : le film de fin s'affichait en blanc, le son seul — joué après la bascule en Direct3D, il
  dessinait encore en GDI sur une fenêtre que la chaîne d'échange possède. `MoviePlayback` passe
  désormais, une fois le régime GDI clos, par `D3D11Backend.PresentImage` (nouveau : une image
  entière présentée au cadrage de la scène)
- feat : **l'état de jeu 7, la fin** (`FUN_00531b40`, `Screens/State07/Ending.cs`) — à la mort de
  Belzébuth : device 3D suspendu (`Ordinal_680(-1)`, ajouté à la façade), film `ENDING.AVI`, device
  rouvert comme au démarrage, retour au menu principal. Première partie menée jusqu'au bout
- fix : le traîneau de la grotte scellée (`0x00471150`, grande descente `0x00471f80`) sortait de
  ses rails — le premier virage de la descente passe la piste à la route 5
  (`mov dword [571930h],5`), écriture que `TrackTurn` avait avalée ; route bloquée à 4, freinages
  et chute finale manqués, et le compte des virages remis à zéro au virage `0x46`. Seule écriture
  perdue parmi les appels de virage des trois corps (vérifié au désassemblage)
- fix : **écart déclaré**, LB-050 — le jeu se figeait au Labyrinthe des morts : une Ombre qui
  s'éveille pendant que le seul membre du groupe est en transfert de sous-zone tirait sans fin une
  proie disponible (`FUN_004a1020`, boucle sans borne dans le binaire). Faute de candidat, le
  monstre garde désormais sa cible ; diagnostiqué à chaud (`dotnet-stack`, image mémoire)
- fix : les poussées du terrain (vent de la vieille tour, sable du désert, plateformes glissantes)
  partaient de travers — `PartyMemberStep.cs` lisait la matière de la face à l'adresse de la
  globale `DAT_005fa508` au lieu de la déréférencer : c'est un pointeur vers la table des matières
  (`Blaze.c` 63402), et la poussée venait de l'arène voisine. Seul lecteur fautif sur les quatre
- feat : `FUN_005335e0` transcrite (`GroundSlopeAngle_005335E0`, `Physics/MotionAndContact.cs`) —
  l'angle de l'arête de terrain que franchit la sonde d'un membre qui suit le meneur ; le groupe
  plantait quand un membre butait contre un bord
- doc : LB-049 — la ligne PLAYER du menu de mise au point agrandit le groupe sans créer la fiche des
  membres ajoutés (0 PV max) ; la jauge de vie divise alors par zéro, dans le port comme en 1998
  (`Blaze.c` 64570). Observé au Labyrinthe des morts, confirmé sur le jeu officiel ; `mode-debug.md`
  corrigé en conséquence
- feat : éditeur de sauvegarde — **toutes les vignettes du CD** : le catalogue d'objets de chaque
  sous-zone porte l'icône juste après la fiche ; le verbe `bestiaire` l'écrit désormais dans
  `Bestiaire/vignettes.csv` (2 370 lignes, les autres CSV inchangés), et les 397 objets du
  catalogue ont la leur — identique octet pour octet à celle que le jeu met dans une sauvegarde
- feat : éditeur de sauvegarde repensé pour un joueur — plus aucun décalage ni octet à l'écran (le
  contrôle échoue s'il en trouve), triches rapides réduites à leur titre, onglet Sorts seulement
  pour les classes qui incantent (Herbes pour le Chasseur), Progression en cases à cocher
  (« La mine abandonnée », « Cristal 1 »…) ; onglets Banques d'état et Hexa supprimés
- doc : `FormatSauvegarde.md` — les huit bits de destination de `+0x2fe` (bit 0 la mine … bit 7 les
  ruines du lac), lus dans `navMask` de `Destination.cs`
- doc : l'envol du Dragon rouge (`0x0040E3A0`) validé en jeu après le correctif `0x1000`
- feat : **l'éditeur de sauvegarde**, premier outil HTML (`OpenForesia.Tools/Html/`) — une page
  autonome qui ouvre les `Blaze-Saving…`, modifie caractéristiques, niveau, PV/PM, or, inventaire
  (397 fiches du CD, icônes décodées), équipement, sorts, progression, banques d'état, lieu de
  reprise, avec triches rapides et vue hexa annotée ; somme et copie de `0x3f00` recalculées,
  réécriture en place avec copie dans `save\copies-editeur\` ; `check-editeur-sauvegarde.mjs`
  l'exécute sur les quatre sauvegardes réelles (vert) ; raccourci `tests\editeur-sauvegarde.bat`
- doc : `Reference/FormatSauvegarde.md` — le fichier de sauvegarde octet par octet : quatre blocs,
  la somme signée et sa copie de `0x3f00` (dont dépend le dépôt), la carte de la fiche, le format
  d'un emplacement d'objet, et le jeu qui jette un objet en ne mettant que son premier octet à zéro
- doc : `DebugParty.cs` — `+0x160..+0x17e` du groupe de mise au point n'est pas « un équipement de
  sept objets » : ce sont les masques de sorts des écoles 0 à 2 (32 sorts connus chacune) et les
  sept compteurs d'herbes du Chasseur
- doc : `Documentation/mode-debug.md` — le mode « Gurkha » : l'activer, les touches, les treize
  lignes du menu de mise au point, et ce que le mode change hors du menu (soin à volonté, sorts,
  bandeau de combat, MONITOR)

## 2026-09-24

- feat : **les trois projets passent en .NET 10** (`net10.0`, `net10.0-windows` pour les outils) sur
  le runtime x86 10.0.12 — build 0 erreur, `OpenForesia.Tools` tourne ; chemins `bin\Debug\net9.0`
  mis à jour dans `playtest.ps1`, les deux `census-*.sh`, le `.bat` du simulateur et la doc
- doc : l'en-tête de `MemoryLayoutTests.cs` disait « trois rouges » — il en reste deux (tests 3 et
  5, ce dernier à 39 champs au lieu de 42) ; le test 2 est vert

- feat : **les cinématiques se décodent en C#, `ir41_32.ax` n'est plus livré** — `MoviePlayback` lit
  par `Host/Indeo4/`, la couleur passe par `Host/Yvu9Rgb.cs` (BT.601, chrominance répétée comme
  Intel, sans son tramage : écart ≤ 5 mesuré par `indeo --rgb`) ; `Platform/IndeoCodec.cs` et
  `src/DLL/` retirés. Plus aucun binaire de 1998 dans le dossier de sortie. Vérifié en jeu : les
  films passent

- feat : **le décodeur Indeo 4 en C# (`Host/Indeo4/`), identique à `ir41_32.ax` à l'octet près sur
  les 8 849 images des quatre films** — portage du décodeur de FFmpeg (LGPL 2.1+), tables générées
  depuis ses sources par `gen-indeo4-tables.js` ; pas encore branché dans le jeu

- feat : **`OpenForesia.Tools indeo <film.avi>`, l'oracle du futur décodeur Indeo 4 en C#** — chaque
  image décodée par `ir41_32.ax` (IF09) et par `Host/Indeo4/Indeo4Decoder.cs` (encore vide, lève
  `NotImplementedException`), comparée à l'octet près ; `--reference-seule` prouve le codec d'Intel
  déterministe sur les quatre films (8 849 images)
- doc : les films n'emploient que quatre genres d'image — `INTRA`, `INTRA1`, `INTER`,
  `INTER_NOREF` — : c'est le périmètre du portage de FFmpeg

- doc : **`ir41_32.ax` rend sa sortie native en `IF09`** (YVU9 + plan d'indicateurs), ce qui en fait
  l'oracle à l'octet près pour un décodeur Indeo 4 en C# porté de FFmpeg — `YVU9` seul est refusé,
  et son RGB24 est **tramé** (motif 4×4), inutilisable comme référence exacte

## 2026-09-23

- feat : **les données de 1998 extraites des DLL sont du C# lisible** — `ConstantesRdata.cs`,
  `ValeursInitiales.cs`, `Exports.cs`, `ModeleDialogue.cs`, `Curseur.cs` (318 lignes), générés par
  `extraire-donnees` et vérifiés octet par octet contre les DLL ; plus de liste de relocations.
  Seules les deux polices restent en binaire (`src/Libraries/Donnees/`, avec un `LISEZMOI.md`)
- doc : le curseur de la fenêtre des films est entièrement transparent — il cache la souris
  pendant les cinématiques

- doc : plus aucune DLL livrée (lot 19) — testé en jeu par Vincent

- feat : **plus aucune DLL de 1998 n'est livrée** — les données qu'on lisait encore dans
  `winsys3d.dll` et `wsyskern.dll` sont extraites (`OpenForesia.Tools extraire-donnees`) dans
  `src/Libraries/Donnees/` et embarquées dans l'assemblage. Seul `ir41_32.ax` (Indeo) reste
- fix : le curseur de la fenêtre des films, chargé dans `wsyskern.dll`, sortait nul depuis que la
  DLL n'est plus chargée ; il est reconstitué depuis son image extraite
- feat : les modes `OPENFORESIA_LEGACY_3DHELP`, `LEGACY_STARTUP` et `CAPTURE_CAPS` sont retirés —
  ils exigeaient le vrai `3dhelp.dll`

- feat : **`winsys3d.dll` n'est plus chargée** — sa mémoire est une image construite en C# depuis
  le fichier (sections de données, relocations, exports), son point d'entrée est transcrit, `VS.Log`
  compris (`Libraries/winsys3d/Image.cs`). L'import de 701 et `OPENFORESIA_LEGACY_DRAWOTAG` sont
  retirés. **Pas encore exercé**

- doc : la procédure du dialogue de choix du pilote (lot 17) testée en jeu par Vincent

- feat : **la procédure du dialogue de choix du pilote est en C#** — le dernier code natif de
  `winsys3d` que le jeu exécute, hors son point d'entrée. **Pas encore exercé**
- doc : les rappels natifs et le tas de `wsyskern` (lot 16) testés en jeu par Vincent

- feat : **le C# n'importe plus rien de `wsyskern.dll`** — `Malloc` et `Free` sont portés, après
  les rappels natifs de `winsys3d` qui partageaient son tas : la procédure de fenêtre, le minuteur de
  17 ms et les deux énumérations DirectDraw, en `[UnmanagedCallersOnly]`. **Pas encore exercé**
- doc : la section critique (correctif) testée en jeu par Vincent

- fix : **la section critique des ordinaux 40/41 n'excluait plus le minuteur natif** — le port du
  2026-09-21 créait sa propre section, en croyant que rien dans la DLL ne prenait la sienne ; le
  rappel du minuteur (`LAB_00a06360`, jamais décompilé) la prend autour de `DeliverEvent`. Le port
  désigne maintenant la section de la DLL
- doc : les passerelles de tas de `wsyskern` ne peuvent pas encore tomber — la procédure de fenêtre
  et le rappel d'énumération des pilotes, restés natifs, libèrent ou allouent dans ce tas ce que le
  C# alloue ou libère
- doc : `InitTrigTables` (lot 15) testé en jeu par Vincent

- feat : **`InitTrigTables` (337) est en C#** — la table de sinus et, ce que Ghidra cachait, les
  quatre tables de racines carrées de `SquareRoot0`. `winsys3d` n'a plus qu'un import, 701, gardé
  pour `OPENFORESIA_LEGACY_DRAWOTAG`. **Pas encore exercé**
- doc : `Setup3DDevice` (lot 14) testé en jeu par Vincent

- feat : **toute la clôture de `winsys3d` est en C# — 247 corps sur 247** : `Setup3DDevice` (823),
  le lanceur et sa liste de pilotes, dans `Device.cs`. Restent deux imports, 337 et 701, aux corps
  déjà portés. **Pas encore exercé**
- doc : **LB-048** — le nom du pilote 3D tient sur 20 octets mais se copie sans borne ; un nom long
  part corrompu dans `Blaze.cfg`
- fix : la façade déclarait `Setup3DDevice` rendant un `int` ; il ne rend rien
- doc : l'initialisation du device (lot 13) testée en jeu par Vincent

- feat : **l'initialisation du device de `winsys3d` est en C#** — 789 et 750, avec la préparation
  du device, la construction du cache de textures et, côté `wsyskern`, `IdentityMatrix` et
  `ScaleMatrix`. `winsys3d` passe à 242 corps, 3 points d'entrée. **Pas encore exercé**
- doc : `FUN_10006a40` initialise les matrices nulle et unité de `wsyskern` — la note « que rien
  n'écrit » du lot 7 était fausse (sans effet : c'est des zéros)
- doc : `CreateTranslationTables` (lot 12) testé en jeu par Vincent

- feat : **`CreateTranslationTables` (756) est en C#** — les tables de couleurs des textures, dans
  `ChargementTextures.cs`. `winsys3d` passe à 233 corps, 5 points d'entrée. **Pas encore exercé**
- doc : `0x00add078` est la moitié haute de la table du genre 2 — la semi-transparence PS émulée
  par l'alpha
- doc : les polices (lot 11) testées en jeu par Vincent

- feat : **les polices de `winsys3d` sont en C#** — `FntLoad` (710), `FntOpen` (668) et
  `Krom2RawAdd` (57) dans `TexteEcran.cs`, avec `GetTimInfo`, `LoadTPage` et `LoadClut`.
  `winsys3d` passe à 232 corps, 6 points d'entrée. **Pas encore exercé**
- fix : la façade décrivait 710 comme la création d'un bitmap de taille donnée et 668 comme un
  canal de console inutilisé ; ce sont `FntLoad(tx, ty)` et `FntOpen`
- doc : l'attente d'image (lot 10) testée en jeu par Vincent

- feat : **l'attente d'image est en C#** — le `VSync(n)` de la libgpu (132) et la pompe de messages
  `PollMessages` (784) dans `Cadence.cs`. `winsys3d` passe à 226 corps, 9 points d'entrée.
  **Pas encore exercé**
- doc : **LB-047** — le journal des attentes d'image a 100 000 places et un indice sans borne : à
  la 100 000ᵉ image (~28 min), il écrase `VSyncDelayed` et la cadence. Transcrit tel quel
- doc : les réglages du device (lot 9) testés en jeu par Vincent

- feat : **les réglages du device et la cadence sont en C#** — `SetTextureFilterMode` (798),
  `ChangeResolution` (749) et le rappel de perte du device (790) dans `winsys3d/Device.cs`
  (fichier neuf), `SetTimingMode` (822) dans `Cadence.cs`. `winsys3d` passe à 222 corps,
  10 points d'entrée. **Pas encore exercé**
- doc : l'éclairage (lot 8) testé en jeu par Vincent

- feat : **l'éclairage du GTE émulé est en C#** — la passe par triangles (793), `NormalColorCol3`
  et ses appelées dans `winsys3d/Eclairage.cs` (fichier neuf), `ApplyMatrixLV` dans
  `Matrices.cs`. `winsys3d` passe à 218 corps, 14 points d'entrée. **Pas encore exercé**
- doc : `SetGeomScreen` (lot 7) testé en jeu par Vincent

- feat : **tout le GTE importé est en C#** — `SetGeomScreen` (379) et la projection du device
  (564) dans `winsys3d/Matrices.cs`, `ZeroMatrix` et `ProjectionMatrix` de `wsyskern` dans
  `wsyskern/Matrices.cs` (fichier neuf). `winsys3d` passe à 212 corps, 15 points d'entrée.
  **Pas encore exercé**
- doc : le lot 6 (360, 418, 795) testé en jeu par Vincent

- feat : **la fin du GTE émulé est en C#, sauf `SetGeomScreen`** — `CompMatrix` (360), la
  projection de sommets par lots (418) et sa jumelle sans profondeur (795), avec leurs deux
  branches MMX / scalaire, dans `Matrices.cs`. `winsys3d` passe à 210 corps, 16 points d'entrée.
  **Pas encore exercé**
- doc : **LB-046** — la branche MMX de 418/795, celle qui tourne, ne garde que 16 bits de la
  translation et arrondit deux fois ; la branche scalaire ne fait ni l'un ni l'autre
- fix : la façade déclarait `Ordinal_360(dest, a, b)` ; c'est `(a, b, dest)`. Les sites d'appel
  passaient déjà le bon ordre

- feat : **les événements `libapi` de `winsys3d` sont en C#** — `OpenEvent`, `TestEvent`,
  `DeliverEvent` et les livraisons de fin d'opération carte (ordinaux 6, 9, 12, 67–69), dans
  `Evenements.cs`. `winsys3d` passe à 205 corps, 19 points d'entrée. **Pas encore exercé**
- doc : **LB-045** — `OpenEvent` n'enchaîne pas le nouvel événement ; `DeliverEvent` ne voit que
  le premier ouvert. Sans effet : la carte mémoire est déclarée finie d'office. Transcrit tel quel

- feat : **neuf corps du GTE émulé sont en C#** — les réglages `SetRotMatrix`, `SetLightMatrix`,
  `SetColorMatrix`, `SetTransMatrix`, `SetBackColor`, `SetGeomOffset` et les calculs
  `VectorNormal`, `SquareRoot0` et la rotation-translation de vecteurs courts (ordinaux 366–369,
  376, 378, 439, 442, 792), dans `Matrices.cs`. `winsys3d` passe à 200 corps, 24 points
  d'entrée. **Pas encore exercé**
- doc : la VRAM (lot 3) testée en jeu par Vincent, jusqu'à la montagne du dragon de feu

- feat : **la VRAM PlayStation émulée est en C#** — `ClearImage`, `LoadImage`, `MoveImage`,
  `StoreImage` (665, 674, 676, 684) et l'invalidation du cache qu'ils déclenchent
  (`winsys3d/Vram.cs`, fichier neuf), plus `ReInitializeAllTextures` (785) et `ReleaseTexture`
  (788) dans `CacheTextures.cs`. `winsys3d` passe à 191 corps, 33 points d'entrée. **Pas encore
  exercé**
- doc : **LB-044** — la couleur de `ClearImage` décale la composante bleue au lieu d'assembler
  le pixel ; juste pour le noir, le seul cas du jeu. Le `TODO(REVERSE)` de l'ordinal 665 est levé
- doc : `PutDispEnv` et les manettes (lot 2) testés en jeu par Vincent, combats compris

- feat : **`PutDispEnv` (657) et la lecture des manettes sont en C#** — la fin d'image
  (recentrage, attente du balayage, présentation) dans `ListeAffichage.cs` avec `GetDispEnv`
  (656), le clavier et les joysticks vers les deux tampons de manette dans `winsys3d/Manettes.cs`
  (fichier neuf). `winsys3d` passe à 181 corps, 39 points d'entrée. **Pas encore exercé**
- doc : **LB-043** — la manette 2 ignore son joystick (test inversé) et hérite des boutons de la
  manette 1 (registre jamais remis à zéro). Transcrit tel quel
- doc : les décalages d'écran (lot 1) testés en jeu par Vincent, sans défaut vu

- feat : **les décalages d'écran de `winsys3d` sont en C#** — `SetShift`, `SetAbsShift`,
  `GetAbsShift`, `ShiftVertices` (`winsys3d/DecalageEcran.cs`, fichier neuf), premier lot hors du
  chemin d'affichage. `winsys3d` passe à 178 corps portés, 40 points d'entrée restants. **Pas
  encore exercé**

- fix : **l'image n'était jamais effacée** — le `PutDrawEnv` porté (ordinal 660) rangeait le
  `DRAWENV` du jeu dans un tableau C# privé, alors que `DrawOTag` lit son octet `isbg` dans la
  mémoire de la DLL (`DAT_011be6f0`, +0x18 du bloc, sous un autre nom Ghidra). Tout ce que la scène
  ne recouvrait pas gardait les images précédentes : l'écran de chargement dans le noir de la
  mine, la traînée des fenêtres de texte. Le bloc est maintenant aliasé (`Data.DrawEnv_011be6d8`)

- feat : **le chemin d'affichage de `winsys3d` est entièrement en C#** — étape 10, 22 corps dans
  `ConstructionPaquets.cs` : la reconstruction `c4f0` (755), le choix d'objet `06a40` (678), la
  résolution `1cff0` (563), `07ca0` et les 18 producteurs, un par mode de primitive TMD.
  `DrawOTag` et tout ce qu'il atteint sont portés (134 corps sur 157, les 23 autres ne sont
  atteints par rien) ; `Natif.cs` est supprimé, sa dette à zéro. `winsys3d` passe à 174 corps.
  **Pas encore exercé**
- doc : les paquets de modèles de `winsys3d` sont des **TMD PlayStation** (identifiant 0x41,
  table d'objets de sept mots, modes F3..GT4 et segments) ; Ghidra range la couleur des
  producteurs en flottant là où le binaire l'écrit en entier — transcrits sur le désassemblage
- doc : **LB-041** — le quadrilatère TMD plat sans lumière est émis en liste de triangles, un
  seul triangle tracé ; **LB-042** — les producteurs texturés divisent chaque `u v` par ~140
  millions, donc toujours 0. Transcrits tels quels
- doc : l'écart assumé de l'étape 9 est levé : aucun producteur ne lit un champ que son
  adaptateur n'écrit pas

- feat : **le constructeur de paquets est en C#** — étape 9, 17 corps :
  `winsys3d/ConstructionPaquets.cs` (fichier neuf), avec `ConstruirePaquet` (`dde0`, ordinal
  752), `AllouerPaquet` (`c480`) et 15 adaptateurs qui font des enregistrements de modèle des
  paquets de primitive pour 8 producteurs, encore natifs (étape 10). `winsys3d` passe à 152
  corps portés, le chemin d'affichage à 112 sur 157. **Pas encore exercé**
- doc : les 15 adaptateurs sont **engendrés** depuis `winsys3d.c` par un script (six formes
  d'affectation, aucune laissée de côté), puis vérifiés au désassembleur sur trois d'entre eux ;
  les mémos de couleur `DAT_011be764/768/76c` sont des mots de 16 bits

- feat : **le chargement des textures est en C# — plus rien du sous-système des textures n'est
  natif.** Étape 8, 6 corps : `winsys3d/ChargementTextures.cs` (fichier neuf) pour la conversion
  VRAM PS → objet texture (`271e0`, `03460` image DIB 4/8/24 bits, `03980` 16 bits par table), et
  le `qsort` de la CRT transcrit dans `CacheTextures.cs`, pour garder l'ordre des dates égales.
  `winsys3d` passe à 135 corps portés, le chemin d'affichage à 95 sur 157. **Pas encore exercé**
- doc : **LB-040** — une page 16 bits en mode soustractif noircit la fin de ses lignes : le pixel
  semi-transparent est effacé par un mot de 32 bits et la source n'avance plus. Transcrit tel quel
- doc : l'argument anonyme de Ghidra dans `FUN_00a03980` est une couleur-clé `{0, 0}` construite
  sur la pile ; la clé d'une page 16 bits vaut 0, pas -1. Relevés sous `Legacy/Raw/Winsys3d/`

- feat : **l'intérieur du cache de textures et la liaison de texture sont en C#** — étape 7,
  24 corps : relevés par genre de primitive, enregistrement des demandes, emplacements (attribution,
  arrondi, recherche, placement, libération, éviction), liaison (`27880`/`27430`) et choix du
  mélange selon `abr` (`03400`/`03430`/`03c80`, dans `Primitives.cs`). `winsys3d` passe à 129
  corps portés, le chemin d'affichage à 89 sur 157. **Pas encore exercé en jeu**
- fix : Ghidra perd la branche semi-transparente de `FUN_00a264f0` (« bad instruction data ») et y
  écrit `genre = genre` au lieu de `genre |= nouveau` ; `FUN_00a278c0` rend 1999 quand tout est pris,
  un chemin sans retour chez Ghidra. Transcrits sur le désassemblage
- doc : **23 corps du chemin d'affichage ne sont jamais atteints** — les exports 548 à 555, 704, 753
  et 754 écrivent le tampon mais rien ne les appelle : ~3 300 lignes retirées du devis. Il reste 45
  corps atteints (étapes 8 à 10) au chemin d'affichage, puis 72 hors de lui. Compte dans
  `byebye-dll.md`

- feat : **`DrawOTag` n'appelle plus directement aucun corps natif** — étape 6, ses six
  dernières appelées. Deux fichiers neufs : `winsys3d/CacheTextures.cs` (ouvrir le relevé,
  relever les pages de texture de l'OT, les charger en évinçant les plus anciennes) et
  `winsys3d/TexteEcran.cs` (fenêtres de texte, console de 30 lignes, textes GDI de
  `WinDrawText` — porté en entier avec ses cinq sous-corps). `winsys3d` passe à 105 corps portés,
  le périmètre d'affichage à 65 sur 157. **Pas encore exercé en jeu**
- fix : Ghidra fait passer à `FUN_00a01f00` l'adresse d'une locale ; le binaire lui passe le HDC
  qu'elle contient (`FUN_00a02820`, 0x00a02846). Transcrit d'après le binaire
- doc : trois `__thiscall` de plus sur `DAT_012141d4` (`FUN_00a27920`, `FUN_00a27850`,
  `FUN_00a27820`) et un tri `cdecl` (`FUN_00a27e00`) à comparaison passée par adresse ; la liste
  d'éviction du cache (500 entrées) déborde sans garde sur la table qui la suit — reproduit tel
  quel. Relevé sous `Legacy/Raw/Winsys3d/FUN_00a26f20.disasm.txt`

- feat : **le répartiteur `RejouerNoeud` n'appelle plus rien de natif** — étape 5, les cinq
  dernières appelées : les rejeux `FUN_00a0e000`, `FUN_00a133e0`, `FUN_00a0e040`
  (`CheminAffichage.cs`), le sprite tourné `FUN_00a0e0c0` (`Primitives.SpriteTourne`) et le fond
  en tuiles `FUN_00a142a0` (`winsys3d/FondTuiles.cs`, fichier neuf). `winsys3d` passe à 94 corps
  portés, le périmètre d'affichage à 54 sur 157. **Pas encore exercé en jeu**
- doc : les deux gros corps transcrits **sur le désassemblage**, pile x87 suivie instruction par
  instruction — relevés sous `Legacy/Raw/Winsys3d/FUN_00a0e0c0.disasm.txt` et
  `FUN_00a142a0.disasm.txt`. Un écart assumé : leurs sommets sur pile n'ont ni `z` ni `rhw` dans
  l'original (ce que la pile contenait) ; le C# y met zéro
- doc : **LB-038** — le sprite tourné prend la largeur pour l'amplitude de texture en v ;
  **LB-039** — une cellule `0xffff` fait boucler le fond en tuiles pour toujours. Trouvés à la
  lecture, transcrits tels quels

- feat : **les 42 primitives GPU du répartiteur sont en C#** — étape 4 du chemin d'affichage,
  `src/Libraries/winsys3d/Primitives.cs`. Six familles de forme : plates (9), texturées
  semi-transparentes brutes (7) et opaques (7), modulées semi-transparentes (5) et opaques (5),
  Gouraud (9), plus `FUN_00a03db0` (`ChoisirMelange`). 42 des 47 cas de `RejouerNoeud` ne passent
  plus par la DLL ; `winsys3d` passe de 46 à 89 corps portés. **Pas encore exercé en jeu**
- doc : **LB-036** — `LINE_G3` et `LINE_G4` ne tracent que leur premier segment (`LINELIST` sur
  trois sommets) ; **LB-037** — `POLY_G4` croise les positions des coins 2 et 3 mais pas leurs
  couleurs. Deux défauts de `winsys3d.dll` trouvés à la lecture, transcrits tels quels
- fix : `DAT_00a2c488` s'appelait « le type de primitive courant » ; c'est **la page de texture
  liée** (-1 et -2 : aucune). Renommée `PageTexture_00a2c488`, dans `Data.cs`, `winsys3d.md` et
  `census-display-path.awk`
- doc : `Data.cs` désigne le second tampon de sommets `&DAT_011be7e8`, les quatre tables de
  coordonnées de texture `DAT_011cb1c0..1cc`, la table d'alpha par mode `abr` `DAT_00a2c5a8`
  (`7f ff ff 3f`, relue dans le binaire) et les tables de conversion écran de `SetShift`

- feat : **le consommateur du tampon de commandes est en C#** — `FUN_00a0cdd0` et `FUN_00a0cd20`
  (`CheminAffichage.RejouerPaquet` / `RejouerPaquetCourant`), étape 3 du chemin d'affichage.
  C'est là que le paquet devient des appels `DrawPrimitive`. Vérifiés au désassembleur, table
  de sauts comprise ; **pas encore exercés en jeu**
- doc : `FUN_00a27880` est un **`__thiscall`** dont `this` est `DAT_012141d4`
  (`mov ecx,[12141D4h]` avant le `call`, `ret 10h`) — Ghidra le déclare à la définition et
  l'oublie à ses 30 sites d'appel. Ajouté à `Natif.cs` avec `FUN_00a0c4f0` ; `DAT_012141d4`
  aliasé dans `Data.cs`
- doc : `byebye-dll.md` enregistre la décision prise (l'aliasing) et l'état du chemin
  d'affichage étape par étape ; `DLLAtteignables.md` et `CheminAffichage.md` régénérés
  (44 → 46 corps portés, 4 → 6 dans le périmètre)

## 2026-09-22

- fix : **`DrawOTag` plantait a la premiere image (0xC0000005)** — `DeviceVtable` n'avait qu'UNE
  indirection la ou il en faut deux. L'expression `*(void***)x` venait d'`Options.cs`, ou elle est
  juste parce que `x` y est la **valeur** que rend l'ordinal 750 ; appliquee a l'**adresse** de la
  globale, elle rendait le device lui-meme et `vtable[0x2c]` lisait a `device + 0xb0`, dans
  l'objet. Le binaire le dit en trois temps : `mov ecx,[11BE760h]`, `mov eax,[ecx]`,
  `call [eax+0B0h]` — deux `mov` avant le `call`, donc deux indirections
- doc : cinquieme garde-fou de l'aliasing dans `src/Libraries/LISEZMOI.md` — **compter les
  indirections sur le desassemblage, pas sur une expression qui marche ailleurs**. Les 33 autres
  globales aliasees ont ete reverifiees une par une : toutes en `*(T*)At(va)`, une seule
  indirection, ce qui est correct puisqu'elles nomment une variable A cette adresse
- feat : **`FUN_00a13400`, le repartiteur de primitives, est en C#** — 118 etiquettes de `case`,
  42 gestionnaires, le pavage des sprites et les cinq blocs de matrice.
  `Libraries/winsys3d/CheminAffichage.RejouerNoeud`. Les 48 appelees restent natives, appelees par
  adresse. **Controle mecanique : 118 etiquettes des deux cotes, aucun code de primitive en ecart**
- doc : `Legacy/Raw/Winsys3d/FUN_00a13400.arites.txt` — les arites des 49 appelees, lues dans le
  binaire (`ret N`) puis **recoupees avec les sites d'appel**. 47 concordaient du premier coup ;
  deux ont demande le desassembleur, dont `FUN_00a0e040` que le releve automatique donnait a zero
  argument alors qu'il en prend un (le releve avait franchi la fin du corps). **Un releve
  automatique d'arite n'est pas une preuve**
- doc : quatre points que Ghidra seul ne tranche pas sur ce corps, tous resolus — `local_50` du cas
  0xe3 est `param_1 + 2` (`lea eax,[esi+8]` a 0x00a13a12) ; le cas 0xe2 **chute** dans les cas 0..3
  sans `break` ; le drapeau `Elo_NoLighting` est **OU-e dans la cle du switch**, il redirige vers la
  variante non eclairee au lieu de tester ; et `DAT_00a2c5a0` vaut **-1 sans jamais etre ecrit**,
  c'est le marqueur « ce paquet ne porte pas de matrice »
- doc : `_DAT_00a2a0b8` vaut `0x39800000`, soit **1/4096** — la virgule fixe 4.12 de la libgte, la
  meme que le `>> 12` de `ScaleMatrix`. La matrice 3x3 du paquet part vers le device **transposee**
  en 4x4 de flottants : la PlayStation range par lignes, Direct3D attend l'autre sens
- feat : **le chemin d'affichage est ouvert — `DrawOTag` (ordinal 701) est en C#.** Premiere pierre
  d'un perimetre de 157 corps : `Libraries/winsys3d/CheminAffichage.cs`, avec ses sept appelees
  encore natives et ses globales aliasees. **`OPENFORESIA_LEGACY_DRAWOTAG=1` rebranche l'original**
  pour comparer les deux a l'ecran
- feat : **`Libraries/winsys3d/Data.cs` — les globales de `winsys3d` aliasees sur le `.data` reel
  de la DLL chargee** (base par `GetModuleHandleA`, RVA = VA - 0x00a00000). Il n'y a plus deux
  copies, il n'y en a qu'une : le lot redevient divisible corps par corps. Sixieme regle ecrite
  dans `src/Libraries/LISEZMOI.md`, avec ses quatre garde-fous
- feat : `Libraries/winsys3d/Natif.cs` — les corps pas encore portes, appeles **par adresse**
  (ils ne sont pas exportes, aucun `[DllImport]` ne les atteint). Echafaudage et tableau de bord :
  il doit avoir disparu quand le perimetre sera porte. Arites prouvees au desassembleur
- doc : le desassemblage de `DrawOTag` corrige Ghidra sur trois points — `VSyncDelayed` est
  `DAT_011be67c`, l'arene est un **double tampon** (`x = 0x40000 - x`, pas une accumulation), et
  les deux ecritures d'arene sont dans l'ordre inverse
- fix : `Natif.cs` portait la **vignette canonique** `winsys3d.c <nom> @ <adresse>` sur ses sept
  entrees, et le recensement a compte huit corps portes la ou il y en avait un. Forme neutralisee,
  piege documente dans le fichier — c'est la lecon « une mention n'est pas une transcription »
  prise par l'autre bout
- doc : **le rejeu n'est pas séparable non plus — troisième piste fermée par la mesure.** La
  clôture de `DrawOTag` contient **20 des 38 producteurs** (1 883 l.) et 68 de ses 134 corps
  touchent un mémo d'état ; `DrawOTag` lui-même remet `DAT_00a2c484/488/48c` à -1 à chaque image.
  Le porter seul laisserait les producteurs natifs allonger un enregistrement mort
- feat : **`census-display-path.sh` + `.awk`**, et `Documentation/Etat/CheminAffichage.md` — la
  carte du dernier lot. Graine (tampon ou mémo) + fermeture descendante : **157 corps, 12 408
  lignes, 70 formes, 26 coutures externes toutes de 13 à 123 lignes, et UN SEUL `[DllImport]`
  dedans** — l'ordinal 701. L'ordre de travail est par forme de squelette, pas par taille
- doc : `CLAUDE.md` référence le nouveau document ; `byebye-dll.md` garde la trace des **trois**
  pistes ouvertes puis fermées dans la journée (`DAT_011be760`, le tampon seul, le rejeu seul),
  pour qu'on ne refasse pas le chemin
- doc : **le consommateur du tampon de commandes est `FUN_00a0cd20`** (46 lignes), atteint par
  `DrawOTag` (701) -> `FUN_00a13400` -> `FUN_00a0cdd0`. La chaîne est fermée : `FUN_00a0dde0`
  (ordinal 752) construit un paquet et le lie dans l'OT du jeu avec le tag `| 0x80000000` ;
  `DrawOTag` parcourt l'OT deux fois — textures, puis rejeu — entre `BeginScene` et `EndScene`
- doc : **le format du paquet** — en-tête de 8 octets, enregistrements de 0x10, sentinelle
  `0xffffffff` sur le mot d'opcode suivant. Quatre opcodes négatifs et un cas par défaut qui **est
  un `DrawPrimitive` sérialisé** : `[type prim][type sommet][sommets][nombre]`, slot `+0xbc`
- doc : **le mémo d'état sert à GROUPER, pas à dédoublonner** — à état inchangé le producteur
  incrémente sur place le mot « nombre » de l'enregistrement précédent et range ses sommets à la
  suite. Deux primitives deviennent un `DrawPrimitive`. C'est la vraie raison pour laquelle les
  38 producteurs sont indivisibles
- fix : `DAT_011be7ac` **n'est pas un symbole réutilisé** — ma note du même jour était fausse.
  `winsys3d.c:8718` est `FUN_00a0c440` (ordinal 754) qui **libère un paquet** en lisant son en-tête
  pour recoudre l'OT. La globale veut dire « le paquet courant », partout
- doc : le devis mesuré — `FUN_00a0cd20` seul 46 l. (mais non exporté), le rejeu 97 corps /
  7 022 l., `DrawOTag` entier 134 corps / 9 091 l., les producteurs 38 corps / 4 886 l. et
  11 formes. **L'ordre est le rejeu d'abord** : c'est le seul lot qui franchisse une frontière déjà
  matérielle
- doc : **`DAT_011be760` n'est pas le levier de la masse — mesuré, et ma note de la veille était
  fausse.** En retirant la globale de la carte, la masse passe de 425 à 419 corps : le seul qui se
  détache est l'ordinal 750, qui est `return DAT_011be760;`. Et ce n'en était pas une, de soudure :
  la valeur pointe déjà sur un objet C# (`SyntheticDevice` détourne `GetProcAddress`), et
  `Options.cs` appelle sa vtable directement depuis `src/Blaze/` depuis le 2026-08-12
- doc : **le chemin d'affichage de `winsys3d` est en deux familles DISJOINTES** — 38 producteurs
  qui écrivent le tampon de commandes (`DAT_011be7dc`, 4 886 l., **11 squelettes**) et 73 appelants
  de device (`DAT_011be760`, 4 696 l., 36 squelettes), **zéro recouvrement**. Les producteurs sont
  le lot identifié : cohérent, fermé, et sans besoin ni du device ni de la vtable
- doc : ce qui soude vraiment la masse est le **cache d'état** du tampon (`DAT_00a2c480/484/488/48c`,
  `DAT_00a2c578/57c`) : deux caches feraient sauter une commande d'état posée par l'autre moitié —
  une primitive avec le mélange de la précédente, par intermittence
- doc : **`DAT_011be7ac` est un symbole réutilisé** (nœud de liste chaînée en `winsys3d.c:8718`,
  base du tampon en 8737). Les comptes de soudure de `DLLGlobales.md` sont donc surévalués pour
  cette globale, et probablement pour d'autres — lire les sites avant de bâtir un lot sur un chiffre
- feat : **les quatre derniers corps isolés de `winsys3d` sont portés — il ne reste que la masse.**
  `Cdrom.cs` (92, `CdSearchFile`), `Matrices.cs` (794, libgte `ScaleMatrix`), `Evenements.cs`
  (10, libapi `EnableEvent`) et `Cadence.cs` (135/136, un casier et ses deux accesseurs). Quatre
  familles PSY-Q, un fichier chacune, chacun amorce du lot que la masse rendra un jour
- doc : **l'ordinal 92 est `CdSearchFile`**, établi par trois preuves concordantes — `DAT_00dedd58`
  fait 0x18 octets, soit exactement une `CdlFILE` ; la signature `(CdlFILE*, char*) -> CdlFILE*` ;
  et le site d'appel qui passe `"BLAZE_ALL.1"`. C'est l'ordinal 94 (`CdComstr`, non importé) qui
  nomme la famille : son `switch` rend `"CdlNop"`, `"CdlSetloc"`, `"CdlPlay"`…
- doc : la `CdlFILE` que rend l'ordinal 92 **n'est écrite par personne** — une seule référence à
  `DAT_00dedd58` dans les 27 000 lignes de `winsys3d.c`. Vingt-quatre octets de zéros dont
  l'adresse circule, et dont l'unique appelant jette le retour
- doc : `byebye-dll.md` n'a plus de liste de courses mais un front unique. Le levier de la masse
  est `DAT_011be760`, le pointeur de device : **touché par 73 corps, écrit par un seul**, et c'est
  la couture que `Host/SyntheticDevice.cs` occupe déjà côté 3dhelp
- feat : **le lecteur de conteneur TIM quitte `winsys3d.dll`** — les ordinaux 677 et 662
  (`OpenTIM`/`ReadTIM`) dans `src/Libraries/winsys3d/Tim.cs`, c'était la composante 3 : deux corps,
  une seule globale (`DAT_011be790`) dont les dix références tiennent dans la paire
- feat : les ordinaux **765 et 767** partent avec eux — ils se sont révélés être deux **souches
  vides** de plus, `FUN_00a02e10` (`xor eax,eax ; ret 8`) et `FUN_00a02e40` (`ret 8`)
- doc : le relevé des souches de `Souches.cs` était incomplet — **huit corps et 223 exports**, pas
  quatre et 149. Les quatre manquants ont deux ou trois arguments, là où le relevé n'avait cherché
  que des arités 0 et 1 ; la recette pour les trouver toutes (compter les corps, pas les ordinaux)
  est écrite dans le pavé de tête
- feat : **la famille de fichiers de la carte mémoire quitte `winsys3d.dll`** — les ordinaux 18 à
  25 (`firstfile`, `nextfile`, `erase`, `open`, `close`, `lseek`, `read`, `write`) sont transcrits
  dans `src/Libraries/winsys3d/CarteMemoire.cs`, avec leur pivot `FUN_00a03f80` et les ordinaux 29
  et 30 que la composante lui soude. **8 imports retirés** ; les composantes 2, 9, 10, 12 et 13 de
  `DLLGlobales.md` disparaissent d'un coup
- feat : `_splitpath` et `_makepath` (wsyskern, ordinaux 4 et 5) dans
  `src/Libraries/wsyskern/Chemins.cs` — c'est `winsys3d` qui les importe, pas le jeu, donc le
  relevé ne les voyait pas
- feat : l'ordinal 97 rejoint `Souches.cs` — `FUN_00a046e0` est une **cinquième souche vide**
  (`xor eax,eax ; ret 0Ch`, 9 ordinaux), trouvée au milieu du `libapi` et non avec les quatre
  autres ; une sixième existe (`FUN_00a046f0`, ordinaux 28 et 73), que personne n'importe
- doc : **LB-035** — l'ordinal 29 de `winsys3d` résout ses deux descripteurs dans le même casier
  statique et renomme donc un fichier en lui-même. Cassé dans le binaire vendu, reproduit tel quel
- doc : `Documentation/DLL/winsys3d.md` — la convention `~` des noms de carte mémoire (écrite par
  `FUN_00a03f80`, relue par 18 et 19), les drapeaux de l'ordinal 21, et les cinq ordinaux de
  `wsyskern` que `winsys3d` importe
- doc : `byebye-dll.md`, `DLLAtteignables.md` et `DLLGlobales.md` remis à jour — `winsys3d` passe
  de 61 à 44 points d'entrée et de 21 à 42 corps portés sur la journée ; **des quatorze
  composantes, il n'en reste qu'une** — la masse, 425 corps et 22 415 lignes


## 2026-09-21

- feat : **`census-dll-globals.sh` — la carte des globales**, avec `census-dll-globals.awk` et
  `Documentation/Etat/DLLGlobales.md`. Il soude les corps par deux relations — un appel vers une
  fonction non exportée, et une globale qu'un corps **écrit** pendant qu'un autre la touche — et
  sort les composantes. Une composante se porte entière ou pas du tout
- doc : **la conclusion « il n'y a plus de petit lot » était fausse.** La carte trouve, à côté de
  la masse soudée (composante 1 : **425 corps, 22 415 lignes, 33 points d'entrée**), **treize
  composantes indépendantes** totalisant 21 corps et ~530 lignes : les ordinaux 10, 18-25, 92, 97,
  136, 662, 677, 765, 767, 794. Portables tout de suite, une par une
- fix : `ListeAffichage.cs` donnait 677 et 662 pour bloqués sur « dix lecteurs natifs ». C'étaient
  dix **références** réparties sur **deux corps** — ces deux-là. La paire est une composante
  fermée. Compter des références quand la question porte sur des corps : c'est précisément
  l'erreur que la carte existe pour ne plus refaire à la main
- feat : **`winsys3d` — ce qui se détache de la liste d'affichage** (65 → 61 imports) :
  `SetDefDispEnv` (658), `SetDefDrawEnv` (661), `ClearOTagR` (688) et le casier d'environnement
  (659/660), dans `Libraries/winsys3d/ListeAffichage.cs`
- doc : **`DrawOTag` (701) n'est pas portable, et le relevé dit pourquoi** — treize globales de la
  DLL, sept appels internes, **quatre emplacements de la vtable du device de 3dhelp**. Le tableau
  corps par corps des quatorze entrées libgpu est dans l'en-tête du fichier : 4 portables, 10
  bloquées. Le reste de `winsys3d` est essentiellement **un seul lot indivisible**
- doc : `ClearOTagR` confirme que **la liste est adressée sur 24 bits** — l'octet de poids fort
  porte la taille de la primitive. La pseudo-C écrit une danse de XOR que le compilateur a bien
  émise (`and 0FFFFFFh ; xor ; and 0FFFFFFh ; xor`, forme MSVC 1996 d'un champ de bits) et qui se
  réduit à `précédent & 0xffffff` — prouvé algébriquement et sur le code machine
- feat : **`winsys3d` — la section critique portée** (67 → 65 imports) : ordinaux 40 et 41,
  `Libraries/winsys3d/SectionCritique.cs`. Elle ne protège **rien à l'intérieur de la DLL** —
  `DAT_00b6d6d8` n'apparaît que dans ces deux corps et son initialiseur, sur 27 000 lignes : elle
  est prêtée au jeu, pas interne. C'est ce qui rend le portage possible là où l'ordinal 337 reste
  bloqué
- doc : **le jeu relâche la section une fois de trop.** `Startup.cs` la prend une fois (l. 384) et
  la relâche deux (l. 406 et l. 541, à l'extinction, sans prise correspondante). Win32 ne dit rien
  sur un `LeaveCriticalSection` non possédé ; `Monitor.Exit` lève. Le portage passe donc par les
  **vraies fonctions kernel32** sur notre propre `CRITICAL_SECTION` — un `lock` C# aurait fait
  planter l'extinction. L'appel en trop est dans `Blaze.c`, donc reproduit
- feat : **`winsys3d` — la trigonométrie libgte portée** (70 → 67 imports) : `rcos` (450/452),
  `rsin` (451/453) et `ratan2` (457). Table de 5 120 entrées, **4 096 pas par tour et 4 096 pour
  1.0** — la convention PSX de bout en bout, les trois constantes relevées dans le binaire tombant
  juste au dernier chiffre
- fix : **les deux `fmul` d'échelle manquaient à la pseudo-C.** Ghidra rend `fsin(...)` puis
  `__ftol()` à la suite, ce qui aurait donné une table de zéros et de ±1. Le désassemblage montre
  `fmul [0A2A0D8h]` avant et `fmul [0A2A0E0h]` après. Transcrire depuis `winsys3d.c` seul aurait
  produit un corps qui compile et rend faux partout
- doc : **`winsys3d` ne se porte pas au détail comme `wsyskern`.** `InitTrigTables` (337), et les
  ordinaux 376, 378 et 379 écrivent des globales de la DLL que 21 corps encore natifs relisent :
  les porter isolément laisserait la copie native à zéro. Ils restent natifs jusqu'à ce que leurs
  consommateurs le soient. Les trois portés ici ne le sont que parce que leur table est une
  **fonction pure de constantes** — deux copies identiques par construction, pas de l'état partagé
- feat : **`winsys3d` — les souches vides, 21 imports en moins** (91 → 70). Quatre corps seulement :
  `0x00a02490` et `0x00a26470` sans argument, `0x00a02e20` et `0x00a02870` à un argument. Sur les
  **830 exports** de la DLL, **149 pointent sur l'un de ces quatre** — le portage PSY-Q a gardé les
  exports pour que le source du jeu compile, et leur a donné un corps vide
- doc : les quatre `ret N`, relus au désassembleur, confirment que les 21 signatures d'`Interop/`
  concordent avec le corps qu'elles atteignent. Contrôle déjà fait le 2026-08-21 — il avait alors
  trouvé `Ordinal_45` déclaré à deux arguments pour un corps en `ret 4`, soit quatre octets laissés
  sur la pile à chaque appel
- feat : **la famille tableau est portée — `wsyskern` n'a plus que deux `[DllImport]`**, tous deux
  la passerelle de tas. `Tableaux.cs` : l'objet C++ de vingt octets, ses trois classes concrètes
  (entiers, chaînes, flottants), les quatre emplacements de vtable employés, le flux de lecture de
  lignes, le remplacement d'échappements, et `strtolong`. **49 corps sur 83 portés**
- doc : **le recensement sous-estimait, et il l'annonçait** — les douze corps de vtable sont
  atteints par POINTEUR, donc invisibles à la clôture ; six manquaient même à `wsyskern.c`, Ghidra
  ne décompilant pas le code atteint par sa seule adresse. Transcrits au désassembleur, relevés
  sous `Legacy/Raw/Wsyskern/`. La clôture de `wsyskern` passe de 53 à 83 corps une fois les
  transcriptions vues
- doc : `disasm` marche sur les DLL d'aide autant que sur `Blaze.exe` — la base de chargement sort
  de l'`ImageBase` du PE, rien n'est codé en dur pour `Blaze.exe`
- doc : `strtolong` (0x10011f50) **n'est pas `strtol`** : `$` force l'hexadécimal à la Pascal, `0b`
  le binaire, et surtout **un zéro suivi d'un chiffre se lit en base dix** — « 010 » vaut dix, pas
  huit. Une donnée du CD lue avec `strtol` sortirait fausse sans rien signaler
- feat : **le codec de `wsyskern` est transcrit** — `Codec.cs`, `Fichiers.cs`, `Data.cs` : les dix-huit
  corps de décompression, `decrypt`, `LoadFile` et `LoadBitmapFile`. **35 corps sur 53 portés**, et
  il ne reste que la famille tableau (4 exports)
- doc : **le décompresseur est LHA `-lh5-` (LZHUF)**, établi sur les constantes et non sur une
  ressemblance : `NC` 510, `NT` 19, `NP` 14, seuil 3, dictionnaire 8 192, `left`/`right` à
  `2 * NC - 1`. Un seul écart avec LHA d'origine — `fillbuf` consulte **deux** compteurs d'entrée,
  donc un en-tête qui ment fait entrer des zéros au lieu de lire au-delà du tampon. Correspondance
  des corps et disposition de l'en-tête dans `Documentation/DLL/wsyskern.md`
- doc : la table de `decrypt` (`DAT_10049b88`) relevée dans le binaire — **neuf octets**,
  `ea 7b 22 25 0c 35 2d 1f 47`. Neuf et non huit : la rotation prend `rang & 7`, le ou exclusif
  prend `rang % 9`. À huit octets, un octet sur neuf serait déchiffré faux
- doc : `LB-033` la double fermeture de handle de `LoadBitmapFile` sur le chemin compressé, `LB-034`
  le mode « stocké » du décompresseur, inatteignable parce que sa garde n'accepte pas son propre
  code de format — code mort dans le binaire vendu
- feat : **`src/Libraries/` existe, et `wsyskern` y entre** — un répertoire par DLL, sa source de
  vérité étant `wsyskern.c` et non `Blaze.c` ; `LISEZMOI.md` pose les cinq règles. Premier lot :
  `Gamma`, `DIBCopyBlock`, `LoadBitmapEx`, `PaletteFromDIB` et les sept corps de messages —
  **trois `[DllImport]` en moins**, aucun site d'appel touché, la façade `Interop/` renvoyant
  désormais vers `Libraries/`
- doc : **la règle découverte en écrivant le lot — ne pas couper une représentation en deux.** Le
  tas (`Malloc` sert, `Free` rend), les vtables de `CreateArray`, et l'état du codec sous section
  critique interdisent de porter par sujet : on porte par **sous-arbre complet d'un point
  d'entrée**. `Malloc` et `Free` restent donc des passerelles vers le natif — un seul tas — jusqu'à
  ce que la DLL soit portée en entier
- fix : le recensement comptait la CRT liée statiquement comme du travail à faire. Ghidra n'en
  préfixe qu'une partie d'un souligné ; sur `wsyskern` **40 fonctions et 1 655 lignes** restaient en
  `FUN_` anonyme, dont `_NMSG_WRITE` et le tas à petits blocs de MSVC. `crt-dll.txt` porte les
  amorces avec leur preuve, et le script en prend la descendance : le devis de `wsyskern` passe de
  93 corps à **53**, celui des cinq DLL de 17 832 lignes à **15 319**

- feat : **`census-dll-surface.sh` — la feuille de route des cinq DLL d'aide**, avec son compagnon
  `census-dll-surface.awk`, son raccourci `tests/update-DLLAtteignables.bat` et le document généré
  `Documentation/Etat/DLLAtteignables.md`. Il lit la **table d'exports directement dans les
  binaires** (aucun `dotnet`), résout chaque `[DllImport]` en adresse, et calcule la **clôture**
  des appels : le devis réel est de **17 832 lignes sur 135 623 — 13 %**, pas la somme des cinq
  décompilations
- doc : `playst.dll` (41 713 l.) n'est nommée par aucun binaire du jeu — c'est le SDK PSY-Q de Sony,
  vraisemblablement la génération qui a précédé `winsys3d`, restée sur le CD ; `3dhelp` et `gldhelp`
  n'exportent qu'une fonction chacune, tout le reste passant par la vtable du device
- feat : forme canonique de traçabilité pour un corps de DLL transcrit —
  `// Ghidra: winsys3d.c FUN_00a05060 @ 0x00a05060`, les trois pièces ensemble. Les cinq corps déjà
  écrits (`EnableKBInput`, `GetKBChar`, `FUN_00a03f50`, `FUN_00a261e0`, `FUN_00a26220`) y sont
  ramenés. Une règle plus lâche comptait 14 corps portés là où il y en avait 5 : le dépôt cite ces
  fonctions en prose, et une mention n'est pas une transcription

---

## 2026-09-20

- feat : **la grotte scellée est complète — 349 sur 349, et le dépôt avec elle** : les 1 430
  gestionnaires d'entité atteignables du jeu sont transcrits, plus aucun « à écrire » dans aucune
  zone. Les six derniers : le projectile à tête chercheuse, le sort à trois rôles, les deux souffles
  accrochés au lanceur, le souffle élémentaire à neuf effets tirés au sort, et **la scène finale**,
  qui bascule l'état de jeu au lieu de rendre la main à la carte
- fix : **`ratan2` était appelé à arguments inversés sur cinq sites** — la tourelle de la grotte
  (trois) et le projectile `0x00485F50` (deux). Le binaire pousse `dz` puis `dx`, donc `dx` est le
  premier argument, comme aux 106 autres sites du dépôt ; la tourelle visait en miroir de la
  diagonale
- doc : la planche `0x00551160` relevée en entier (quatre tuiles) ; deux marges courtes prouvées et
  scellées, une preuve devenue sans objet retirée
- feat : **grotte scellée, lot 12 — 343 sur 349** : trois des cinq isolés des sous-zones 13-15 —
  la **huitième scène** (l'embrasement du fond, la seule qui ne tue personne), la **neuvième**
  (le membre aspiré, six ou douze projections et une sirène qui s'éloigne), et l'étincelle qui
  spirale sur une sphère qui se resserre puis s'en échappe
- fix : **les trois balayages de victime marquaient le mauvais octet** — Ghidra ancre son pointeur
  sur `0x005ecadb`, soit l'enregistrement **+ 0x3b**, et son `*pbVar |= 2` s'y lit ; le code posait
  le bit dans le `+0x36` du mot de sprite, que l'ouverture venait de mettre à 1. Confirmé dans le
  binaire aux trois sites
- feat : **grotte scellée, lot 11 — 340 sur 349** : les quatre gros corps de la sous-zone 0 — le
  rocher qui roule sur deux axes, le radeau qui s'éloigne et se dissout, le traîneau que le groupe
  monte, le rouleau qui écrase — et leurs trois feuilles partagées
- feat : **les trois traîneaux de la grotte scellée sont écrits**, dont `0x00471150`, **le plus gros
  corps du dépôt** (610 instructions) et ses deux feuilles de piste : cinquante virages décrits par
  des fenêtres de distance, cinq routes, trois bits d'aiguillage
- doc : **la sous-zone 1 rejoue la sous-zone 0, 0x150 plus haut** — ses trois feuilles sont les
  copies exactes des siennes, toutes globales décalées de `0x150`, une seule autre différence : la
  bouffée fait naître `0x32` au lieu de `0x34`
- fix : le rayon de la bulle d'effet d'état se lisait `+0x28 / 100` ; la magie `0x51EB851F` avec
  `sar 4` divise par **50**, pas par 100 — la bulle touchait deux fois trop loin
- feat : **grotte scellée, lot 10 — 334 sur 349** : les trois familles de « D », dix corps pour trois
  lectures — la tourelle qui suit un membre du groupe, la navette qui le porte d'un bout à l'autre,
  et la bulle d'effet d'état. **Plus un seul jumeau dans le dépôt**
- doc : **les vingt et une « appelées inconnues » des deux tables de sauts n'en étaient pas** — leurs
  deux tables sont celle de `0x0040a30c`, mêmes douze effets dans le même ordre ;
  `AfflictMember_00409F40` prend un `keep`, que la grotte remplit avec le numéro d'emplacement
- doc : la navette empaquette **deux positions par dword** (`+0x44`, `+0x48`, `+0x4c`), départ en
  haut et arrivée en bas, et obtient l'arrivée en déroulant tout son voyage à la première image
- feat : **grotte scellée, lot 9 — 324 sur 349, la catégorie « C » est vide** : les vingt et un
  derniers des salles 11-12 et 13-15 — la gerbe tournante, la colonne, la braise en hélice, le
  tourbillon qui fauche, les deux gestes dont l'un vise la cible de sa cible
- feat : **cinq morts de membre de plus** — par le froid, par le cri, par la foudre, dans la lumière
  et en chantant ; `Cutscene/SealedCaveScenes.cs` passe de deux à sept scènes
- fix : `EntityHandler_00437790` devient `TickBurningDeath`, paramétrée — la combustion des salles
  11-12 ne marque pas la victime, rend la caméra et garde son script sur la sous-zone
- doc : la reconnaissance du lot 10 est faite et notée dans `TODO.md` — les dix-sept « appelées
  inconnues » des deux « D » à table de sauts sont toutes la même enveloppe gardée, et `0x0046AD70`
  empile deux positions dans trois dwords
- doc : six planches, neuf scripts de sons qui se chevauchent, et **une planche de cinq
  enregistrements dont le lecteur en lit sept** — relevée entière pour que le C# lise les mêmes
  octets que le binaire (`DataSymbols.md`) ; trois marges prouvées et scellées
- feat : **grotte scellée, lot 8 — 303 sur 349** : les « C » des blocs 4-9 et 6-7, vingt-sept corps —
  la boule qui éclate en douze, le pilon qui fait trembler la salle, la détonation qui rend la main à
  son porteur, l'impact qui empoisonne, le coup qui tourne avec son rôle d'effet d'état
- feat : **`Cutscene/SealedCaveScenes.cs` ouvert** avec les deux morts de membre — la dessiccation et
  la combustion, chacune gelant la partie, animant la victime et repeignant le fond
- feat : le décor — l'effondrement en quatre temps, la chose qui remonte le couloir en flottant,
  l'escalier tournant qui attend tout le groupe, deux colonnes qui s'élèvent, l'anneau qui se
  referme, la grande flamme qui éclaire la salle et marque le groupe
- doc : sept planches, deux scripts de sons, le voile plein cadre `0x005de024` et l'octet de
  sous-zone `0x005de52c` employé comme sélecteur de rendu (`DataSymbols.md`) ; quatre marges prouvées
  et trois re-scellées après paramétrisation
- feat : **grotte scellée, lot 7 — 276 sur 349** : les « C » du bloc de sous-zones 0 / 2-3 / 5-8-10,
  trente-six corps pour vingt-deux lectures — l'étincelle qui rentre au centre, la traînée en
  spirale, le halo qui tourne à plat, le mur qui balaie le couloir, le vide sous la salle, le nuage
  acide, la pierre oblique qui éclate, le coup qui tourne et s'enfonce
- feat : le décor de la grotte — le pendule elliptique, l'éboulement qui secoue, la porte qui suit
  son drapeau, le lien qui tient tant que son voisin reste près, le nid qui lâche ses bêtes, deux
  errants qui rebondissent sur les murs, le rocher qui roule, le piston
- feat : les mécanismes — le levier qui **bascule** son bit (là où la dalle ne fait que le poser),
  la plate-forme oblique, l'ascenseur muet, et ce qu'on actionne et qui gèle la partie
- doc : cinq planches de plus, le fond violet de la salle (`0x005e9160`) et la montée `0x82000`
  écrite en décalage variable qui ne varie jamais (`DataSymbols.md`) ; neuf marges courtes prouvées
  et scellées


## 2026-09-19

- feat : **grotte scellée, lot 6 terminé — 240 sur 349, plus aucun « B »** : les quatorze derniers,
  dont l'explosion qui secoue la salle, les ondes qui frappent en s'étendant, la gerbe qui envoûte,
  le satellite qui se resserre, le feu follet et la spore
- feat : **grotte scellée, lot 6 (moitié) — 226 sur 349** : treize « B » des salles 11 à 15 —
  tourbillons, éventail, jets depuis un porteur, éclair qui frappe en pâlissant, l'éclosion qui
  réveille son propriétaire ; l'anneau et la braise branchés sur leurs parents. Restent 14 « B »
- feat : **grotte scellée, lot 5 — 213 sur 349** : les quarante « B » des sous-zones 4 à 10 — herse
  qui tombe et secoue la caméra, monte-charge, piston, geyser, bruit qui rôde, vague qui frappe,
  pierre lancée, vol qui traverse la salle, verbe qui pousse une entité d'un pas
- feat : sept copies d'autres zones branchées — la salve de la mine, la braise de la forêt, l'onde
  de froid de la vallée, l'hélice et la goutte de la tour, la dalle de la forêt, la vague qui marque ;
  leurs parents prennent les écarts en paramètres, défauts inchangés
- doc : trois planches, trois scripts de sons et trois marges prouvées de plus (`DataSymbols.md`)
- feat : **grotte scellée, lot 4 — 166 sur 349** : les vingt-huit « B » des sous-zones 0 à 3 et
  cinq corps de plus — roues, balancier, bloc qui culbute, tremplins, pierres qui tombent, dalles
  qui comptent le groupe, la minuterie, le levier et le battant de la sous-zone 1, un verbe de vue
- feat : les quatre bouffées obliques de la grotte sont celle de la tour — `TickSlantedPuff` prend la
  planche et la montée en paramètres
- doc : les trois mots d'état de la sous-zone 1 (`0x00571828`, `0x0057182c`, `0x00571978`), qui les
  pose et qui les lit ; cinq planches, six marges prouvées et scellées
- feat : **grotte scellée, lot 3 — 133 sur 349 : toutes les intelligences du jeu sont écrites** ;
  sept bêtes, une par fichier dans `Ai/SealedCave/` — le Dragon zombie, le Cerbère et ses trois têtes,
  la Gorgone, Eifridt, Lordémon, l'Ange noir, Belzébuth et sa disparition
- feat : huit copies branchées sans corps neuf (Maudite, Bras gris, Gremlin, Salamandre, trois Sale
  rampant qui sont le Rampant de la tour) et la bête qui ne fait que s'animer des salles 11 à 15
- feat : `SettleSceneEntityOnGroundAndWalls` prend la distance de la marche de contournement en
  paramètre — la Gorgone tient `0xc8` là où le Varan tient `0x50`
- fix : `census-duplicates.sh` divisait par zéro une fois la dernière routine d'IA écrite, et le tri
  par complexité sortait vide
- doc : les sondes, sorts et scripts de sons des bêtes de la grotte (`0x00552f30`..`0x00553288`) dans
  `DataSymbols.md` ; le script `0x00553230`, qui ne se termine jamais
- feat : **grotte scellée, lot 2 — 118 sur 349** : onze familles de jumeaux (35 corps) et six verbes,
  dont la dalle de pression, le battant qui frappe, la fumée qui monte et la sortie vers l'état de
  jeu 10 ; deux familles de cinq étaient déjà écrites (la lueur de la tour, le grain du Labyrinthe) ;
  `Handlers/SealedCaveMecanismes.cs` ouvert
- feat : **grotte scellée, lot 1 — 77 sur 349** : les vingt-deux copies de corps déjà écrits
  (Maudite, Noir goblin, Bras gris, Basirisk, grains, éclats, ondes, émetteur, deux amorces) ;
  fichiers `Effects/SealedCaveParticles.cs` et `Handlers/SealedCave.cs` ouverts
- fix : la ligne de la grotte dans `Comportement.md` portait 40 IA pour 39 gestionnaires — recomptée
  à 39, dont 24 écrites
- feat : **la vallée d'argent est complète — 46 sur 46**, le onzième ensemble ; il ne reste que la
  grotte scellée
- feat : **la luge** (`0x00467440`) — un parcours scripté par portes, virages et pente, qui
  s'achève sur les scripts des quatre membres ; et **le radeau** (`0x00466F50`), qui part quand tout
  le groupe est à bord et finit par sombrer
- feat : **le Grifon** (`0x004E1380`) et **Fougre** (`0x004E20F0`), deux volantes sœurs — accroche,
  piqué qui projette, souffle ; nouveau répertoire `Ai/SilverValley/`
- feat : la neige de la vallée (flocons doubles, étoiles de givre, cristal, bouffées, ondes, jet de
  glace), son décor, une scène et le verbe qui rappelle les membres écartés ; l'Ours blanc et le
  Loup blanc branchés sur le Grizzly et le Loup
- doc : les planches de la zone 8, le tracé de la luge (`0x00467b98`) et son bloc de caméra
  (`0x00571760`, lecteur à trouver) relevés dans `DataSymbols.md`
- feat : **les ruines dans le lac sont complètes — 41 sur 41**, le dixième ensemble entièrement
  implémenté, en un lot de vingt-trois gestionnaires
- feat : **le Kraaken** (`0x004E4F20`, `0x004E40E0`, `0x004E4890`) — le corps, le bras et le pied,
  une bête qui nage à la surface : ruée, charge, demi-coup qui projette, un sort ; nouveau répertoire
  `Ai/LakeRuins/`
- feat : les cinq piliers qui basculent, le poisson qui saute, deux sons, une scène, la vague et sa
  couronne, les gerbes et les bulles du lac, quatre verbes de dialogue ; le Wyrm et le Lézard noir
  branchés sur leurs corps existants
- doc : les planches `0x00550b20` / `0x00550b60` / `0x00550b88`, les sondes et les mots de sort du
  Kraaken, et le mot des piliers `0x005717c8` relevés dans `DataSymbols.md`
- feat : **les ruines souterraines sont complètes — 35 sur 35**, le neuvième ensemble entièrement
  implémenté, en un seul lot de quatorze gestionnaires
- feat : **le Sosie** (`0x004DF680`) — le double d'un membre du groupe : il frappe avec la sonde de
  sa classe, ou tire comme le Chasseur et la Fée ; nouveau répertoire `Ai/UndergroundRuins/`
- feat : **la fabrique des Sosies** (`0x00541C90`) — en sous-zone 2, le groupe est recopié à chaque
  image dans les quatre modèles « Sosie » du CD
- feat : Balle métal (`0x004DC890`) branchée sur Willie ; six particules, un son d'ambiance, un
  réveil de scène, une scène et deux verbes de script des ruines — quatre fichiers neufs
- doc : `0x00550958`, `0x00550980` et la sonde du Sosie `0x00552ce8` relevées dans `DataSymbols.md` ;
  trois marges courtes prouvées et scellées
- feat : **le château de l'immortel est complet — 170 sur 170**, le huitième ensemble entièrement
  implémenté
- feat : **le Tigre** (`0x004D80D0`) — le frère du Loup-garou, plus vif : un coup de patte en deux
  temps, une griffe qui porte l'effet 12, un rugissement ; nouveau fichier `Ai/ImmortalCastle/Tiger.cs`
- feat : **le Loup-garou** (`0x004D7200`) branché — sans hardiesse, une projection qui écrit la vitesse
  de sa victime, un hurlement
- fix : le Loup-garou tirait au hasard avant de regarder s'il faisait face, et oubliait de couper
  sa poussée en commençant à hurler — deux écarts au binaire, trouvés en lisant le Tigre
- doc : LB-032 — le second coup de la Sacuba, écrit sous la garde de l'image 13, ne part jamais

## 2026-09-18

- feat : **la Sacuba** (`0x004D9D90`) et ses trois copies, dont deux dans la grotte scellée — la
  charpente du Vampire, un vol stationnaire, quinze tirages de sort ; `Ai/ImmortalCastle/Sacuba.cs`
- feat : `SettleSceneEntityOnGroundAndWalls` prend une animation de vol stationnaire facultative
- feat : **la Chimère** (`0x004D2FC0`) et sa copie des ruines souterraines — un recul, un geste nu,
  deux sorts, une replanification qui se relance ; `Ai/ImmortalCastle/Chimera.cs`
- feat : **le Nocturio** (`0x004DACC0`) — l'Enchanteresse avec les deux appels du Roi-zombie et une
  ruée qui empoisonne ; `Ai/ImmortalCastle/Nocturio.cs`
- feat : **le Fantôme** (`0x004C9E00`) — la charpente du Vampire, et une teinte et une échelle qui
  palpitent à chaque image ; `Ai/ImmortalCastle/Ghost.cs`

- feat : **la Harpie** (`0x004CB4F0`, `0x004D60A0`) et sa copie de la grotte scellée
  (`0x004FDFE0`) — la charpente de la Crapouille, un sort, et un cri qui sème la famille à trois
  vies du château jusqu'à l'éclair rouge. Zone 6 : 163 sur 170 ; `Ai/ImmortalCastle/Harpy.cs`
- feat : la queue volante de la Crapouille sortie en `SettleCrapouilleFlight`, partagée avec la
  Harpie
- feat : **le Loup et le Loup gris** (`0x004CC650`, `0x004D4E10`) et leur copie des ruines
  souterraines (`0x004DDE30`) — une lecture neuve : il court, vire sans s'arrêter, attaque en
  bondissant et tourne autour de sa cible. Zone 6 : 161 sur 170 ; nouveau fichier
  `Ai/ImmortalCastle/Wolf.cs`
- feat : `SettleSceneEntityOnGroundAndWalls` prend un test d'espèce facultatif au heurt de mur
- feat : **le Vampire et le Petit vampire** (`0x004CF3A0`, `0x004D8F70`) — la charpente du Rampant,
  et une posture 12 qui invoque le nuage à effet 32 du château. Zone 6 : 159 sur 170 ; nouveau
  fichier `Ai/ImmortalCastle/Vampire.cs`
- feat : **le palier 4 du château de l'immortel est écrit — 148 sur 170 devient 157 sur 170** ;
  il ne reste que les 13 routines d'intelligence
- feat : la famille à trois vies (feu follet, projectile qui rebondit, éclair rouge), les semis
  qui chantent (`TickCloudSeed` plus des sons) et les nappes enfants de la gerbe des nuages
  (`TickOwnerFrontSweep` à la mise en place du château)
- feat : le battant à trois positions du créneau 29, et `WriteScriptFlag`, l'écriture d'un
  drapeau de banque 0 dont `ScriptFlagSet` était la lecture
- doc : le tampon `+0x48` du nuage `0x004271B0` sert à ce que **seul le premier des onze semis
  chante** ; et le cas 2 des nappes n'existe que là où la gerbe marque ses enfants `+0x9a = 2`
- doc : cinq planches relevées, trois marges courtes scellées ; la famille reçoit ses deux planches
  séparément pour que `check-descriptors.sh` les mesure
- feat : **le palier 3 du château de l'immortel est écrit — 125 sur 170 devient 148 sur 170**, et
  la catégorie C du tri est vide comme la A et la B ; la grotte scellée gagne `0x00442AC0`
- feat : les quatre nuages à effet 32 du château — `StepStatusCloud` du Labyrinthe plus une gerbe
  propre au château (`StepCastleCloudBurst`), et les trois « appelées inconnues » étaient
  `ApplyStatusEffect32IfAllowed_00421D80` à l'octet près
- feat : trois scènes des sous-zones 5 et 11 (`Cutscene/ImmortalCastleScenes.cs`), l'éclair qui se
  relance maillon par maillon (`0x0042D770`), et deux verbes de script de plus — un effet infligé au
  membre désigné, et la sortie du rectangle
- doc : **un sous-type `0xc0 + n` va au créneau n de `0x0056b0c8`, `b1c8` ou `b2c8` selon la
  fonction qui le fait naître** — c'est ce qui dit qui sème quoi
- doc : deux corps du décor (`0x00463850`, `0x00464E70`) sont des corps déjà écrits **plus un
  ajout**, invisibles au tri : comparer au même créneau de l'autre bloc miroir
- doc : trois planches relevées (`0x005507e0`, `0x00550828`, `0x00551410`), deux marges courtes
  scellées
- feat : **le palier 2 du château de l'immortel est écrit — 63 sur 170 devient 125 sur 170**, et la
  catégorie B du tri est vide comme la A
- feat : la **famille de six** des créneaux 22, 23 et 24 — six navettes pour une lecture, à un
  nombre près : l'image où le freinage commence (`0x19`, `0x29`, `0x39`)
- feat : `Handlers/ImmortalCastleMecanismes.cs`, ouvert à quatre corps — ceux qui lancent un script
  de zone ; et quatre **verbes de script** de la table `0x0056aec8` dans `WorldMapScript.cs`, dont
  un don d'objet à rappel de fin de boîte (`0x00444AB0`, branché dans `UiText.cs`)
- doc : **aucune des 49 formes du palier n'avait de parent déjà écrit** — la recherche LOOSE contre
  les 957 corps du dépôt est revenue vide, sauf `0x0042A350`. Un palier « simple » n'est pas un
  palier gratuit
- feat : **le palier 1 du château de l'immortel est écrit — 31 sur 170 devient 63 sur 170**, et
  deux gestionnaires de la grotte scellée tombent en prime (`0x00439170`, `0x00442C20`)
- feat : trois fichiers neufs pour la zone 6 — `Effects/ImmortalCastleParticles.cs`,
  `Handlers/ImmortalCastle.cs`, `Cutscene/ImmortalCastleScenes.cs` — et l'enregistrement de caméra
  `0x005714e0`, le cinquième de la suite au pas de `0x30` ouverte par la forêt
- doc : dix planches relevées (`0x00550480`, `0x005505d0`, `0x00550740` — vingt enregistrements
  chacune et identiques octet pour octet —, plus sept autres), quatre marges courtes scellées
- fix : `0x00463E80` appelle `0x00468080` et non `0x004580e0` — **la normalisation qui apparie deux
  corps masque les cibles d'appel**, et deux sons différents s'y ressemblaient à six lignes près
- doc : **la zone 6, le château de l'immortel, est triée** — `Zone6ParComplexite.md` généré, et
  les 139 corps restants classés en cinq paliers dans `REVERSE_STATUS.md` : 33 déjà écrits ou
  presque, aucune appelée réellement inconnue
- feat : **LA ZONE 5, LE LABYRINTHE DES MORTS, EST COMPLÈTE — 160 sur 160**, le septième ensemble
  entièrement implémenté
- feat : le lot 13 — le projectile à tête chercheuse (`0x00484190`) et le battant qui bascule en
  entraînant sa voisine (`0x00462A80`), les deux derniers corps de la zone
- doc : **LB-031** — le projectile déréférence la cible qu'il vient d'effacer quand elle meurt en
  vol ; tracé, pas observé, transcrit tel quel
- doc : LB-030 s'étend au Labyrinthe — `0x0041FCA0` porte le même stroboscope rouge que la forêt
- feat : **le lot 12 du Labyrinthe des morts** — `0x00420480` et `0x00422E50`, deux corps pour une
  lecture : l'onde qui s'étale et frappe une fois chaque membre dans son rayon, ou sa gerbe qui
  tourne et s'éteint. Zone 5 : 158 sur 160
- feat : **le lot 11 du Labyrinthe des morts** — les quatre semis des nuages (`0xed` et `0xdf`),
  quatre corps pour une lecture : l'étincelle qui va chercher la cible, ou qui revient sur son
  porteur. Zone 5 : 156 sur 160
- doc : **ce qui déclenche l'effet 32 des nuages** — leurs propres semis, qui posent `+0x44 = 1`
  sur le nuage quand ils atteignent la cible
- doc : l'étincelle vise au-dessus de la cible mais mesure sa distance vers un point au-dessous —
  un `add` de 1998, transcrit tel quel
- feat : **le lot 10 du Labyrinthe des morts** — `0x0041C900` et `0x0041EDD0` sont le corps du lot
  9 à leur repli près (un tampon nul sur la pile, aucun repli) : deux corps sans lecture neuve. Zone
  5 : 152 sur 160
- doc : les huit « D » restants ne font que **quatre lectures** — une famille de quatre, une de
  deux dont les appelées inconnues sont déjà écrites, et deux isolés
- feat : **le lot 9 du Labyrinthe des morts** — `0x004224F0` et son jumeau de la grotte scellée
  `0x0043F4E0`, deux corps pour une lecture : la nappe qui se déploie devant son propriétaire et
  marque qui s'y trouve d'un des neuf effets d'état. Zone 5 : 150 sur 160 ; grotte : 48 sur 349
- doc : les neuf « appelées inconnues » de `0x004224F0` sont neuf corps identiques — la garde,
  puis un effet déjà écrit
- feat : **le lot 8 du Labyrinthe des morts — la catégorie C est vide** : zone 5, 133 → 149 sur
  160. Seize corps pour douze lectures ; il ne reste que les onze « D »
- feat : deux verbes de script de `0x0056aec8` — le silence net du bloc de son, et « tout le groupe
  a-t-il passé la ligne ? » qui lance le script sur le chef
- feat : le nuage calé sur son porteur qui porte l'effet 32, en quatre émissions ; la quatrième
  (`0x0041FCA0`) est aussi le stroboscope de la forêt, sorti en `StepCarrierStrobe`
- feat : deux corps sans une ligne propre — `0x0041C740` et `0x0041EC20` sont `TickHostDeathBurst`,
  à la conduite près quand le porteur est nul
- feat : six effets — l'escarbille, la ligne oblique, la fumée qui tourbillonne, le modèle qui
  s'ouvre en tournant, le tireur qui vise le porteur de son porteur, le tourbillon qui frappe
- feat : deux scènes dans `Cutscene/LabyrinthScenes.cs` — cent images sur l'espèce 1, deux cents
  sur l'espèce 2 avec le fond peint en rouge
- doc : **`Ordinal_762` ne fait rien** — son export est le `ret 4` nu de `0x00a02870` ; publier le
  bloc d'écran `0x00566c48` est sans effet dans cette DLL
- doc : ce qu'un corps fait d'un porteur nul **n'est pas une règle de bloc** — deux corps voisins
  des sous-zones 0 à 3 font l'un « continuer », l'autre « sortir »
- doc : le paramètre perdu de `0x00483F30` (`unaff_EBP`) est la distance, rangée dans
  l'emplacement de l'argument ; le second `Ordinal_457` est un tangage
- doc : `0x00550388`, `0x00550410`, `0x00550430` et `0x005526a8` relevés dans `DataSymbols.md` ;
  la marge de `0x00424C70` prouvée et scellée
- feat : **le lot 7 du Labyrinthe des morts — la catégorie C du décor est vide** : zone 5, 124 → 133
  sur 160. C passe de 25 à 16 corps, et la table `0x0056adc8` n'a plus que son unique « D »
- feat : six corps de décor — le porte-voix des **quatorze** points fixes, ce qui tourne puis fait
  trembler la carte cinquante images, le pilon qui descend / écrase / remonte, le chemin en huit
  temps qui entraîne ses deux voisines, et les **deux sœurs** qui tombent du plafond et rebondissent
- feat : trois corps dans `Handlers/LabyrinthMecanismes.cs` — celle qui guette le groupe puis file
  en ligne droite et lance un script, le mécanisme à usage unique qui fige la partie pour dire une
  phrase, et ce qui roule dans un couloir en **poussant** les membres qu'il rencontre
- doc : `0x00463150` écrit l'état de ses **deux voisines** par `+0xac` et `+0x148` — le même
  adressage relatif que les portes à deux battants de la mine, ici à trois
- doc : `0x00462330` et `0x004625B0` ont des **gardes de caméra inverses** (`jl` contre `jg` sur
  `0x005de068`) : les deux sœurs ne tournent jamais ensemble, elles se partagent la carte
- doc : dans ces deux corps, **`+0x10` n'est pas l'état** mais la vitesse de rotation, qui gagne
  `0x20` à chaque rebond ; l'état est `+0x16`
- doc : `0x005e9065` relevé dans `DataSymbols.md` — l'autorisation de `0x004633C0`, voisine de
  `0x005e9062` sans qu'on sache encore si les deux appartiennent à la même structure
- feat : **le lot 6 du Labyrinthe des morts — la catégorie B est VIDE** : zone 5, 112 → 124 sur 160.
  Il ne reste que du « moyen » (25) et du « gros » (11, 10 lectures)
- feat : neuf effets dans `Effects/LabyrinthParticles.cs` — celle qui se transforme et meurt en une
  image, le halo qui s'ouvre puis s'éteint, le même qui entraîne son jumeau de `+0x70`, les deux
  bulles qui enflent, l'étincelle à deux ancrages en version gardée et non gardée, le débris qui
  attend puis tombe, et la planche de quatre images
- feat : deux secousses de caméra dans `Handlers/LabyrinthMecanismes.cs` — même corps au même
  créneau de deux blocs, l'une finissant sur une phrase et l'autre sur un script
- feat : **`AreaVerb_00444340`** dans `WorldMapScript.cs` — un **verbe de script**, pas un
  gestionnaire d'entité : il rend à chaque membre désigné par un masque l'état de contact d'une
  entité neuve (`0xA000` dans `+0x82`/`+0x86`/`+0x8a`/`+0x8e`, `+0x94` à zéro)
- doc : `0x00550060`, `0x00550268`, `0x00550360` et le script de sons `0x00552690` relevés dans
  `DataSymbols.md`. **`0x00550360` est le premier cas où la donnée invite à transcrire trop LONG** :
  son cinquième enregistrement a la même forme que les quatre premiers, et c'est le corps qui borne
- doc : `0x005e9062` — deux corps identiques instruction pour instruction lisent **deux globales
  différentes** pour le même rôle : `0x005e9062` en sous-zones 0 à 3, `0x005de538` en 5 à 8
- doc : marge courte de `EntityHandler_004249D0` (`0x00550350 +0x08`) prouvée et scellée dans
  `marges-verifiees.txt` — 63 des 66 marges courtes du dépôt sont maintenant prouvées
- feat : **le lot 5 du Labyrinthe des morts** — la fin de la catégorie B du décor et ses trois plus
  simples « C » : zone 5, 102 → 112 sur 160 ; B passe de 19 à 12 corps, C de 28 à 25
- feat : huit corps de décor — le demi-tour verrouillé par un drapeau, ce qui frappe et projette en
  l'air, le bloc dont la position survit au changement de carte, le balancier qui change de bord
  tous les cinq à neuf coups, la navette qui va et vient sans fin, ce qui s'effondre quand on monte
  dessus, l'écroulement à quatre compteurs empaquetés dans deux demi-mots, et le porte-voix des
  trois points fixes
- feat : deux corps dans `Handlers/LabyrinthMecanismes.cs` — l'escalier qui se dérobe marche par
  marche en lançant un script à chacune, et ce qui guette l'approche du groupe puis lance un script
- doc : **`0x005517a8`, `0x005517c8`, `0x005517e8`** relevées dans `DataSymbols.md` — trois tables
  de huit `int` (X, hauteur, Z en cases) qui gardent la position d'un bloc poussé **entre deux
  cartes** ; le contrôle de sanité de `0x00461980` renvoie le bloc à une position que son propre
  test rejette
- doc : quatre corps absents de `Blaze.c` relevés au désassembleur sous `Legacy/Raw/Blaze/` —
  `FUN_00461260`, `FUN_00460AC0`, `FUN_00460800`, `FUN_00461F50` — plus `FUN_00460C00`, décompilée
  mais à laquelle Ghidra avait perdu un argument
- fix : `0x00462860` et `0x00463640` sont au créneau **47** de leurs blocs, pas 30 — le lot 3 avait
  écrit 30, et le créneau 30 du bloc `0x00558128` est `0x00461980`
- feat : **le lot 4 du Labyrinthe des morts — les dix plus simples qui restaient** : zone 5, 92 → 102
  sur 160, et la catégorie B du tri par complexité passe de 29 à 19 corps
- feat : sept corps de décor dans `Handlers/Labyrinth.cs` — la lueur rouge que son drapeau éteint,
  la porte à un battant des sous-zones 4 et 9, ce qui descend une fois et se tait, la chute
  déclenchée au contact qui marque deux voisines, la respiration suivie de quarante images de
  clignotement, le bloc qu'on pousse (son propre à la sous-zone 6), et la source d'effets qui sème
  à ±100 cases autour d'elle sans bouger
- feat : **`Handlers/LabyrinthMecanismes.cs` ouvert** pour les trois corps du lot qui consultent le
  groupe — ce qui ne part que si le groupe entier est à bord, la porte tenue ouverte à la manette,
  et le panneau qui dit une phrase, tourne neuf images et meurt
- doc : quatre corps absents de `Blaze.c` relevés au désassembleur et conservés sous
  `Legacy/Raw/Blaze/` — `FUN_00460310`, `FUN_004618B0`, `FUN_00462990`, `FUN_00460990`
- doc : `0x004618B0` compte les emplacements **en clair dans son corps**, avec le masque interdit
  `0x50010000` et non le `0x50000000` d'`ActivePartySlotCount_00466D70` ; le bit `0x10000` de
  différence est le cœur de sa condition — avec un membre à terre, elle ne part jamais

## 2026-09-17 (suite 13)

- feat : **le lot 3 du Labyrinthe — le banc des sous-zones 4 et 9**, 12 corps pour 4 lectures,
  repartis sur **trois zones** : zone 5, 87 → 92 ; zone 6, 29 → 31 ; zone 10, 44 → 47
- feat : la luciole accrochee a deux porteurs, l'invocation en onze avec son tremblement d'ecran, la
  mort du porteur en trois temps, et le decor qui respire en couleur
- fix : **correction d'une affirmation de la suite 11** — `0x0041C900`, `0x0041EDD0` et `0x004224F0`
  ne sont PAS le meme corps emis trois fois : 396 / 380 / 389 instructions, 236 lignes d'ecart entre
  les deux premiers. Ils partagent une charpente, pas un corps. Trois lectures, pas une
- doc : **le banc des sous-zones 4 et 9 n'est pas une copie du banc 0-3** — sur ses trente corps,
  cinq seulement ont un parent proche, et ces parents vivent au chateau de l'immortel et dans la
  grotte scellee : ecrire un corps de ce banc en ferme deux a quatre, sur trois zones
- doc : **`0x004224F0` a un jumeau hors zone a UNE instruction pres** — `0x0043F4E0` (grotte
  scellee) : le plus gros corps restant du Labyrinthe est deux corps pour une lecture
- doc : la branche `+0x98 == 1` de la mort du porteur est **morte dans le cours normal** — l'etat 1
  met le champ a zero et l'etat 2 tourne dans la meme image ; transcrite telle quelle

## 2026-09-17 (suite 12)

- feat : **le lot 2 du Labyrinthe des morts — les neuf paires miroir**, 19 corps pour 8 lectures ;
  zone 5, 68 → 87 sur 160
- feat : `Effects/LabyrinthParticles.cs` — la bouffee (deux etalements), le grain qui monte, le grain
  projete dans le cap, le grain qui attend puis tombe, la braise, la nappe qui tombe puis se pose,
  le flocon de cendre et l'etincelle d'eventail
- feat : treize planches de plus relevees dans `Blaze.exe`, chacune bornee deux fois — par l'adresse
  suivante ET par l'indice maximal que son corps mene en `+0x40`
- fix : les cinq marges courtes du lot sont **prouvees et scellees** dans `marges-verifiees.txt`
- doc : **la planche `0x0054fee0` fait CINQ enregistrements, pas huit** — l'adresse suivante en
  laissait huit, mais les octets changent de forme des `0x0054ff08` et le corps fait tourner `+0x40`
  de 0 a 4 : les deux lectures se recoupent
- doc : **la braise n'est pas la meme des deux cotes** — deux `sub edx,eax` du bloc 0-3 sont des
  `add edx,eax` dans le bloc 5-8 : elle part dans le cap opposee. Deux instructions sur 154, que le
  diff LOOSE ne rapprochait pas
- doc : **le binaire tire le hasard une fois pour rien** dans la braise — deux `call 00547140h` qui
  se suivent, le premier resultat jamais lu ; reproduit, parce qu'il decale le flux
- doc : **le banc de planches des sous-zones 4 et 9 est a +0x400 du banc 0-3**, la ou celui des
  sous-zones 5 a 8 est a +0x1e8

## 2026-09-17 (suite 11)

- feat : **le lot 1 du Labyrinthe des morts hors intelligence** — les dix gestionnaires les plus
  simples de la zone 5, 58 → 68 sur 160
- feat : `Screens/State03/Handlers/Labyrinth.cs` — fichier neuf, le decor de la zone : l'echelle
  reposee a chaque image, les deux decors qu'un drapeau de script rend visibles, la source sonore
  toutes les vingt et une images, la hauteur qui flotte tant que le drapeau 250 tient, le
  balancement a phase tiree au hasard, la chute de dix images
- feat : trois verbes de script de plus dans `WorldMapScript.cs` — la lecture des quatre boutons de
  face d'une manette (creneau 5, commun aux trois blocs de la zone) et le creneau 7, emis DEUX fois
  par le compilateur, qui fait naitre l'effet 0x39 sur les membres qui passent quatre filtres
- fix : `census-zone-complexity.sh` sortait en silence avec 1 quand la derniere amorce a ecrire de
  la zone n'avait pas de jumeau ecrit — une derniere iteration finissant sur un test faux, sous
  `set -e`
- doc : **le bloc 5-8 de la table 0x0056b2c8 est le bloc 0-3 recopie a +0x2ff0** — neuf paires
  verifiees, comptes d'instructions identiques : les 31 particules restantes ne sont qu'une
  vingtaine de lectures
- doc : **trois des onze gros gestionnaires de la zone partagent une CHARPENTE** (0x0041C900 en 0-3,
  0x0041EDD0 en 5-8, 0x004224F0 en 4-9) — une table de sauts et neuf feuilles chacun. **Ce ne sont
  PAS trois copies** : 396 / 380 / 389 instructions, 236 lignes d ecart entre les deux premiers
- doc : **le verbe 0x00444620 rend le mot des boutons, pas la reponse qu'il range dans la banque** —
  le binaire ne remet jamais EAX a zero, et le dernier registre ecrit est la lecture de la table
  d'entree

## 2026-09-17 (suite 10)

- feat : **les seize routines d'intelligence du Labyrinthe des morts, en dix lectures** — zone 5,
  42 → 58 sur 160, et la categorie « intelligence » du tri de zone est VIDE
- feat : `Ai/Labyrinth/` — dix fichiers neufs : Persil, Sanguinaire (+ le Tas d'os), Mort-vivant,
  Ombre (+ l'Ombre rouge), Dunœud, Revenant, Zombie, Durahan (+ le Durahan-noir), Roi-zombie, Momie
  (+ la Reine momie, la Sanglante et la Vieille momie)
- feat : **trois corps fermes hors zone** — l'Ombre ferme le groupe « 3 corps, 970 » du releve de
  doublons (grotte scellee, sous-zones 0 puis 2 et 3) et le Zombie sa copie stricte du chateau
- doc : **le Sanguinaire PARE** — toute replanification finit par un examen qui jette la posture
  choisie pour la garde si la proie frappe, si la bete lui fait face, si elle est au sol a moins de
  0xc8, et si le tirage est tombe sous sa timidite. Ses deux louvoiements detruisent ce tirage en
  reutilisant son registre : apres eux la parade ne peut plus partir
- doc : **le Durahan enchaine un combo a trois temps** a pile ou face — coup, coup, bond —, et son
  bond frappe a demi-puissance en divisant par deux le mot +0x30 du bloc de modele le temps de l'appel
- doc : **le Roi-zombie ne sait pas ce qu'il invoque** : les cinq sous-types de ses cinq appels
  viennent tous du bloc que le repertoire de zone 0x005e97cc designe en +0xcc
- doc : **la variante 3 de la Momie se reecrit en variante 0** des la premiere image ou elle est vue,
  apres s'etre donne son echelle et sa teinte — seule bete du depot a changer de variante hors coup
- doc : **le Zombie est arrete par les portes**, meme barriere de salle que la Belle bleue de la mine,
  et son attente ne le fait PAS rendre la main : l'examen de proximite tourne quand meme
- doc : **les quatre copies de l'Ombre ne different que par un mot** — celui que la variante 2 et
  au-dela atteignent seize places apres la fin de leur liste de sorts, par un registre non charge
- fix : `SettleSceneEntityOnGroundAndWalls` parametree sans changer ses appels — le Sanguinaire est
  le seul de la famille a lacher le bit d'attaque sur un heurt en l'air

---

## 2026-09-17 (suite 9)

- feat : **le Labyrinthe des morts, premier lot — zone 5, 23 → 42 sur 160**, catégorie A vide : dix-neuf
  corps branchés sur des corps déjà écrits, à la planche ou à quelques constantes près
- feat : `Effects/LabyrinthParticles.cs` — douze planches relevées et bornées, et les renvois : grain,
  escarbille lourde, deux éclats, gerbe serrée, fumée violette, deux amorces de la gerbe à cinq phases
- feat : `Cutscene/LabyrinthScenes.cs` — les deux amorces de la scène parlante (bloc `0x005714b0`,
  piste 0x17) ; `AreaExitFlash.cs` — l'amorce du flash, décalée en X et non en Z
- feat : **l'Agonisant `0x004BEE40` branché sur le Pourpre preux** — mêmes instructions, intégration
  réordonnée ; la lueur `0x00460F30`/`0x004635B0` sur `0x00452A90`, copie stricte
- fix : quatre corps modèles paramétrés sans changer leurs appels (`TickHeavySpark_00407800`,
  `TickWideSpray_0040F8E0`, `TickDriftingShard`, `TickProjectedShard`) ; sept marges courtes
  prouvées et scellées, aucune preuve périmée
- doc : **la fumée et la gerbe du Labyrinthe partagent une seule planche**, `0x00550280`, là où la
  tour en avait deux — la gerbe écrit dans ce que la fumée lit

---

## 2026-09-17 (suite 8)

- doc : **le Labyrinthe des morts est trié** — `Zone5ParComplexite.md` : 137 corps à écrire, 97 formes
  neuves, 15 corps proches d'un corps déjà écrit
- doc : **l'Agonisant `0x004BEE40` est le Pourpre preux à 25 lignes près**, et les 17 IA de la zone
  font 11 lectures ; leurs trois « appelées inconnues » sont des copies du chercheur de proie
- doc : un pré-filtre par recouvrement de lignes remplace le `diff` corps contre corps : 137 cibles
  contre 777 corps écrits en trois secondes, le `diff` ne tournant que sur le meilleur candidat

---

## 2026-09-17 (suite 7)

- feat : **la vieille tour est complète — zone 4, 176 sur 176**. Les 4 corps de la catégorie D et
  les 16 routines d'intelligence, plus trois copies hors zone (château, ruines souterraines, grotte)
- feat : `Handlers/OldTower.cs` — les deux wagonnets de la tour (`0x0045C870`, `0x0045ECC0`) et le
  maître de salle qui rend la garde invulnérable (`0x0045C2E0`, défenses à 30 000)
- feat : `WorldMapScript.cs` — le verbe minuteur `0x00443D00`, qui retourne les vues des sous-zones
  0 à 3 et se désarme au bout de 1 200 appels
- feat : `Ai/OldTower/` — dix fichiers neufs : Chevalier noir (+ Platinum), Sorcier (×2), Garde
  golem (+ deux Armures volantes), Sylphide, Wybarn, Ténébreux (+ sa copie de la grotte),
  Enchanteresse, Rampant, Sabre volant, Golem-squelette
- feat : la Belle verte `0x004A8510` et la Crapouille `0x004AAA80` branchées sur la Belle bleue et
  la Crapouille déjà écrites — copies sémantiques à l'ordre des registres près
- doc : **`0x00514020` n'était pas une appelée inconnue** : c'est l'enveloppe de l'opcode 8 du script
  (`ScriptOpEnterSubArea`), appelée avec des instructions bâties sur la pile
- doc : **seize routines pour onze lectures** — le diff contre les corps déjà écrits a désigné cinq
  groupes avant toute transcription ; le Chevalier noir est le Pourpre preux sous 408 lignes de
  registres renumérotés
- doc : **le Sabre volant recycle sa variante `+0x16` en phase d'ondulation**, et **le
  Golem-squelette a une replanification qui se relance elle-même** — deux pièges transcrits tels quels
- doc : un `bodies.tsv` de `census-duplicates.sh` resté dans `/tmp` peut être périmé : le régénérer
  avant de comparer deux corps

---

## 2026-09-17 (suite 6)

- feat : **les 16 derniers corps de la catégorie C de la vieille tour — la catégorie est VIDE**.
  Zone 4 : 140 → 156 sur 176 ; il ne reste que D (4 corps) et les 16 routines d'intelligence
- feat : `Handlers/OldTower.cs` — dix décors de `0x0056adc8` : le plateau à verrou, le mur qui se
  dérobe, **le bloc poussé dont la position tient dans six octets de la banque 0**, les deux bouches
  à vapeur, la bouche à portée, le maître de la salle des gardes, **le pendule qui fauche**,
  **la plateforme qui détourne la caméra** et la passerelle qui plie
- feat : `Cutscene/OldTowerScenes.cs` — les trois fins de scène (`0x0041BB20`, `0x0041AC80`,
  `0x0041B460`) : même famille que celles de la mine, mais pas des copies — le diff en rend 240 à 323
- feat : `Effects/OldTowerParticles.cs` — le halo qui suit son porteur, la masse qui tombe et la
  traîne à trois temps
- doc : **`FUN_005037b0` n'a PAS d'argument** — c'est une enveloppe qui appelle `FUN_005037e0` avec
  deux pointeurs de pile. Le paramètre que Ghidra lui prête est fantôme, et le `2` lu à côté d'un de
  ses sites appartenait à l'appel suivant. Corrigé dans les quatre sites neufs
- doc : **le bloc `0x0045E300` range sa position dans six octets de la banque 0**
  (`0x005e9706`..`0x005e970b`), deux par axe, reconstitués par `(haut << 8 | bas) << 16 >> 4` — le
  mot signé remis en unités de `0x1000`. Un seul bloc de ce genre par sous-zone, là où `0x0045AE90`
  en indexe plusieurs dans `.data`
- doc : **`0x0045BB10` détourne les trois pointeurs de caméra vers un bloc à elle** (`0x00571680`,
  `0x00571668`, `0x00571670`) et les rend en reconstituant l'écart `0x00564658` — le chemin du verbe
  de scénette `0x00504200`, écrit à l'envers
- doc : **`0x0045A4D0` lit ses quatre verrous à l'envers** : le compte s'incrémente pour chaque bit
  *absent*, donc zéro veut dire « les quatre sont posés »
- doc : **`0x0045F7E0` inline `ActivePartySlot_00472770` avec un autre repli** — la fonction nommée
  rend l'indice 4 quand le balayage échoue, celle-ci rend le chef. Les deux coexistent

---

## 2026-09-17 (suite 5)

- feat : **22 des 38 corps restants de la catégorie C de la vieille tour** — 18 formes. Zone 4 :
  118 → 140 sur 176 ; il reste 16 corps, les plus lourds (2 681 instructions), listés dans `TODO.md`
- feat : `Effects/OldTowerParticles.cs` — neuf formes de la table `0x0056b2c8` : la spirale dressée,
  le **scintillement double** (une entité, deux points, une image sur deux chacun), l'orbite
  clignotante, le feu follet invoqué, la braise, la volute qui s'ouvre, l'éclat en ligne droite, le
  panache à deux visages et la bouffée de biais
- feat : `Combat/Projectiles.cs` — la nappe qui descend en brûlant (`0x00416C40`) et le projectile
  qui grossit en tombant et frappe (`0x004850C0`)
- feat : `Handlers/OldTower.cs` — sept décors de `0x0056adc8` : le brasier qui bat, la trappe qui
  bascule et secoue la tour, les deux jets de braise, **le bloc poussé dont la position vit dans
  `.data`**, le piston, le pilier qui s'enfonce et le voile qui fige le groupe
- feat : `SpawnInheritedEffectNearCamera_0045C000` — la **quatrième sœur** de `FUN_00468110` : même
  rayon de 1800, même signature perdue par Ghidra, mais elle fait naître par `FUN_0047a9d0`
- doc : **la borne d'un corps est la première adresse connue qui le suit** — comparer deux jumeaux
  sur une fenêtre fixe fait apparaître des différences massives qui n'existent pas, parce que la
  fenêtre déborde sur la fonction suivante. `spans.tsv` de `census-duplicates.sh` porte les vraies
  bornes ; la comparaison ne vaut qu'après troncature au dernier `ret`
- doc : **quatre paires inter-blocs de plus sont des quasi-copies** — planche seule, ou planche plus
  une constante. `0x00417CA0`/`0x00417E10` échangent en outre **l'axe long de leur ellipse** : le
  terme qui prend le rayon entier va dans la hauteur pour l'un, dans la profondeur pour l'autre
- doc : **le bloc poussé garde sa place hors de son créneau** — `0x0045AE90` lit et réécrit sa
  position dans trois tables de `.data` (`0x00551660`, `0x00551680`, `0x005516a0`) indexées par son
  numéro. La table survit au rechargement de la sous-zone ; le créneau non
- doc : `s_Descriptors_0054FE00` est **déclarée d'un seul tenant sur douze enregistrements** parce
  que `0x0041B7C0` mène `+0x40` jusqu'à 6 sur une planche qui n'en a que cinq : le débordement lit
  les deux premières tuiles de `0x0054fe28`, contiguë dans le binaire
- doc : **trois marges courtes neuves restent à prouver** — `0x0054FA50`, `0x0054FBF0` et
  `0x0054FBF8` n'ont qu'un enregistrement. La troisième est sûre (le corps met `+0x40` à zéro) ; les
  deux premières appartiennent à `TickTwinSparkle_00417B10`, qui **n'écrit jamais `+0x40`** et
  l'hérite donc de son géniteur — identifier le géniteur avant de sceller

---

## 2026-09-17 (suite 4)

- feat : **les huit formes à jumeaux de la catégorie C de la vieille tour** — 17 créneaux pour
  8 corps. Zone 4 : 101 → 118 sur 176, et **la catégorie C n'a plus un seul jumeau**
- feat : `Handlers/OldTower.cs` — les deux battants d'une ouverture (`0x0045DF60` sonore et à cap
  croissant, `0x0045E040` silencieux et à cap décroissant), ce qui attend leur verrou puis l'efface
  (`0x0045E100`), le souffleur à deux bouffées (`0x0045CF70`) et **la balance à six plateaux**
  (`0x0045A630`)
- feat : `Effects/OldTowerParticles.cs` — la spirale qui se resserre (`0x00417E10`) et
  l'éclaboussure cardinale (`0x00417F80`) ; planche neuve `0x0054FA18`
- feat : `Combat/Projectiles.cs` — **le feu follet** (`0x00417110`), le seul triplet de la zone ;
  trois planches neuves, `0x0054FAC0`, `0x0054FC68`, `0x0054FDC0`
- doc : **un banc de verrous de cinq bits relie trois gestionnaires** — `0x005716E0` (sous-zones 5,
  6, 8, 10) et `0x00571730` (3, 7, 9). Les deux pivots posent leur bit en s'ouvrant, le troisième
  l'attend pour grandir puis l'efface. Le bit est **posé à l'octet et effacé au mot** ; les deux
  largeurs sont conservées
- doc : **deux paires n'ont aucun paramètre** — `0x0045CF70`/`0x0045F3B0` et
  `0x0045A630`/`0x0045E4E0` ne diffèrent que par les déplacements relatifs de leurs `call`, qui
  résolvent vers les mêmes cibles. Un seul corps, deux enveloppes nues — le cas de `LAB_00474320`
- doc : **le souffleur se sert de lui-même comme point d'émission** — il met de côté position, ancre
  et cap, se transporte à 120 unités et 200 de haut, fait naître quatre fois `0x39`, puis remet les
  sept valeurs. Deux fois, à `cap + 0x80` et `cap + 0x880`
- doc : **la balance ne gouverne pas une entité mais six**, prises à la file : les rangs pairs
  contre les rangs impairs, ce qu'elle fait à la hauteur d'un camp elle le fait à l'opposé sur
  l'autre. Sa hauteur de repos est lue à la naissance et **forcée à 1 si elle est nulle**, parce que
  `+0x10` nul est justement le marqueur de naissance
- doc : **le feu follet brûle hors de tout état** — l'appel à `DamagePartyNearPoint_00418F50` est
  après les quatre états, y compris pendant son attente de départ et l'image où il meurt. Et il ne
  se pose qu'une image sur cinq : le reste du temps il traverse les murs
- doc : **la spirale de `0x00417E10` n'est pas un cercle** — X et Y suivent la phase `+0x16`, mais Z
  suit le cap `+0x32`, et son rayon n'est pas divisé par quatre. Une ellipse orientée dont l'axe
  long vaut quatre fois les deux autres
- doc : quatre planches neuves, **toutes de quatre enregistrements et toutes à marge 0** —
  `check-descriptors.sh` les confirme, et chaque corps borne `+0x40` explicitement à 3

---

## 2026-09-17 (suite 3)

- feat : **les quatre verbes simples de la vieille tour** (table `0x0056aec8`, catégorie B de
  `Zone4ParComplexite.md`) — « le groupe est-il encore en transition ? » (`0x00443C60`), l'effet
  `0x2c` posé sur tout membre au sol qui touche (`0x00443CB0`), deux drapeaux d'avancement rangés
  en un nombre de 0 à 3 dans la banque 2 (`0x004442F0`), et le rangement du groupe en V derrière
  son premier emplacement, suivi du gel (`0x00444200`). Zone 4 : 97 → 101 sur 176, la catégorie B
  est vide
- doc : **les verbes de la tour vont par paires de créneau, pas par sujet** — deux bancs selon la
  sous-zone, et les deux occupants d'un créneau n'ont rien à voir l'un avec l'autre : `0x00443D00`
  et `0x004442F0` partagent le créneau 11, `0x00443CB0` et `0x00444200` le créneau 12. « Le verbe
  12 » ne veut rien dire sans dire de quelle sous-zone
- doc : **le premier verbe dont le résultat de condition soit un reste de registre NON constant** —
  `0x00443CB0` ne finit pas par `xor eax,eax` : au `ret`, `EAX` porte le mot d'état de
  l'emplacement 3, ou le retour de `FUN_00468110` si ce dernier tour a appelé. Un emplacement actif
  portant `0x80000000`, rendre 0 aurait été un contresens. `TODO(REVERSE)` posé sur la moitié haute
  du reste, que `mov ax,cx` laisse intacte et que le portage perd
- doc : `0x004442F0` **inverse les poids** de ses deux bits — `0x10`, le plus bas, pèse 2 ;
  `0x20` pèse 1. Ce n'est pas un décalage mais quatre `test` qui posent la constante

---

## 2026-09-17 (suite 2)

- feat : **les six decors de la montagne du dragon de feu** (`Handlers/FireMountain.cs`, nouveau) —
  deux sons d'ambiance, deux decors sur sinusoide, un jet intermittent, et l'evenement unique
  `0x00456310` qui leve son drapeau de script, fait trembler la camera et tombe
- feat : **le trio des tables de sous-zone** (`Effects/MiscMarkers.cs`) — le pere `0x004830F0` lance
  ce qui tombe `0x00483360` ; a l'impact, 39 eclats `0x00483260` tournent et frappent dix images.
  Zone 2 : 30 -> 39 sur 42
- doc : les scripts de sons differes `0x00552600` et `0x00552610` releves ; le premier attendait un
  consommateur depuis le 2026-08-31, la file est transcrite sans trou jusqu'a `0x0055268f`
- doc : aucun des neuf n'a de jumeau (diff STRICT et LOOSE relances avant d'ecrire)
- feat : **le Dragon rouge** `0x00499140` (`Ai/FireMountain/RedDragon.cs`, nouveau repertoire), 8 000 PV —
  deux coups dissemblables, un bond, deux souffles, et l'incantation du Behemoth sur Superman, Boule de
  feu et Meteores. Zone 2 : 39 -> 40 sur 42, il ne reste que les deux scenes cinema
- doc : `ModelAnchorRow_00535770` rejoint `SceneEntityAiCommon.cs` — le Dragon rouge est son second
  appelant ; le projete du Troll est reemploye tel quel
- feat : **la montagne du dragon de feu est complete — 42 / 42**, cinquieme ensemble entier ; ses deux
  scenes cinema sont ecrites (`Cutscene/FireMountainScenes.cs`) : l'envol du Dragon rouge `0x0040E3A0`
  fait naitre la scene muette `0x0040D930`, qui lance le script 0
- fix : `s_ViewParams_00571270` etendue de `0x190` a `0x1b0` — l'enregistrement de scene `0x005713f0`
  tombait sur un trou au-dela de `0x00571400` ; la borne est desormais celle du bloc de la foret
- doc : recouvrement latent releve entre `s_ForestSceneBlock_00571420` et `s_EntityState_00571500`
  (`0x00571500..0x00571667`) — aucune adresse partagee aujourd'hui, invisible pour `MemoryLayoutTests`
- feat : `census-zone-complexity.sh` et `Zone4ParComplexite.md` — les 148 gestionnaires a ecrire de la
  vieille tour tries par complexite : 16 deja ecrits ailleurs, 57 simples, 55 moyens, 4 gros, 16 IA
- doc : quatre amorces de la tour (`0x00419F40`, `0x0041B3F0`, `0x0041A560`, `0x0041B420`) appellent
  les corps des scenes parlantes deja ecrites — une amorce se juge a son appelee, pas a sa taille
- feat : **la vieille tour, categorie A — seize corps deja ecrits ailleurs** : bulles, grains, fumee
  violette et gerbe (`Effects/OldTowerParticles.cs`, nouveau), scene parlante
  (`Cutscene/OldTowerScenes.cs`, nouveau), Goblin-sorcier, Homme-lezard et trois decors. Zone 4 :
  28 -> 44 sur 176
- feat : `TickSpellcasterAi_00487260` prend sa table de sorts en parametre (celle de la tour est a
  `0x00552a38`)
- doc : six planches de la tour bornees par balayage du binaire ; sept marges courtes prouvees et
  scellees (48 / 48)
- feat : **la vieille tour, categorie B lot 1 — seize decors des sous-zones 5, 6, 8 et 10**
  (`Handlers/OldTower.cs`, nouveau), qui couvrent vingt-trois gestionnaires avec leurs jumeaux des
  autres blocs. Zone 4 : 44 -> 67 sur 176
- doc : `0x0045C6E0` n'ouvre « un bruit, au loin » que si l'octet de sous-zone `0x005de52c` est nul,
  alors qu'il n'est installe que hors de la sous-zone 0 — a verifier en jeu
- feat : **la vieille tour, categorie B lot 2 — douze decors des sous-zones 0, 1, 2, 4 et 3, 7, 9** :
  tout le decor simple de la tour est ecrit. Zone 4 : 67 -> 79 sur 176
- doc : deux corps mis en commun que le diff LOOSE ne voyait pas (`TickScrapingPushable`, masque
  `0x800` / `0x882` ; `TickFlaggedMechanism`, deux boites de dialogue) ; le jumeau des sous-zones
  0-4 explique pourquoi le message n'est ouvrable que depuis la sous-zone 0
- fix : `handlers-exerces.tsv` — la ligne de `0x0045AA20` retiree, elle ne prouvait que le plantage
- feat : **la vieille tour, categorie B lot 3 — dix-huit effets simples** : treize particules
  (`OldTowerParticles.cs`), deux amorces du flash de sortie de zone, deux emetteurs a lanceur et la
  bulle empoisonnee. Zone 4 : 79 -> 97 sur 176
- doc : sept planches de la tour bornees par balayage ; deux corps ecrivent la page d'une planche,
  dont celle de la bulle ; deux marges d'un enregistrement prouvees et scellees (50 / 50)

## 2026-09-17 (suite)

- feat : **huit particules de la montagne du dragon de feu** (`Effects/FireMountainParticles.cs`) —
  zone 2 : 22 -> 30 sur 42
- feat : la braise `0x0040DDE0`, quatre variantes sans rapport dans un seul corps ; les deux
  bouffees `0x0040E6F0` / `0x0040E860`, qui brulent le groupe ; l'etincelle `0x0040EBE0`, la seule
  particule du depot qui garde un point de reference
- doc : **la planche `0x0054f680` fait sept enregistrements, pas quatre** — la forme change au
  cinquieme, mais un balayage du binaire entier ne trouve aucune reference a `0x0054f6a0` : personne
  ne l'installe, donc la plage continue
- doc : deux marges courtes prouvees et scellees dans `marges-verifiees.txt` (`0x0054F630`,
  `0x0054F658`)
- doc : **`0x0040D930` et `0x0040E3A0` mises de cote** — ce sont deux scenes cinema, pas des
  particules, et elles emploient l'enregistrement `0x005713f0` que `MineScenes.cs` signale comme
  chevauchant une frontiere d'allocation. Il leur faut un fichier et une decision de plan memoire

## 2026-09-17

- feat : **la mine est complete — 157 / 157 gestionnaires implementes**, deuxieme zone entiere
  apres la foret et l'auberge ; les sept dernieres routines d'IA sont ecrites
- feat : **le Jeune dragon** (`0x004944E0`) : deux souffles qui ne touchent personne, dont l'un part
  de sa gueule — la rangee 0 du tableau d'attaches de son modele
- feat : **le Troll** (`0x004965B0`) : cinq gestes, une charge qui s'enchaine seule sur un bond, et
  **la seule regeneration ecrite dans une IA du depot** — 8 PV une image sur vingt, sans condition
- feat : **le Scorpion** (`0x004921A0`) : deux armes, et un dard qui tire son effet d'etat au sort
- feat : **l'Araignee** (`0x00497370`) : le Scorpion a six differences pres, une seule arme
- feat : **le Mille-patte** (`0x00491650`) : il ne mord qu'en l'air — le prelude efface le bit de
  geste a chaque image passee au sol
- feat : **le Chef-Goblin** (`0x004889A0`) : le corps de l'Ogre a sept ecarts pres, dont la seule
  charge amortie du depot
- fix : **l'Ours gris** (`0x0048DB30`) n'avait pas de corps a ecrire : c'est le Grizzly, 660
  instructions identiques sur 660, a deux gabarits et une table de sauts pres — tous trois verifies
- doc : `FUN_00535770` nommee et transcrite (`ModelAnchorRow_00535770`) : une rangee du tableau
  d'attaches du bloc de modele, sans aucun appelant dans `Blaze.c`

## 2026-09-16 (suite 26)

- feat : **`Ai/Mine/` ouvert — quatre creatures, douze corps**. Mine : 144 -> 150 sur 157 ; les
  copies ferment aussi deux corps au Labyrinthe, un au chateau, un a la montagne et deux a la grotte
- feat : **la Belle bleue** (`0x0048B530` + 2 copies) : frappe par contact a chaque image, nage,
  enfle, et s'accroche a sa proie tant que le joueur ne se debat pas
- feat : **la Salamandre** (`0x0048E3A0` + 1) : une volante qui choisit ses sorts par variante et
  crache l'effet `0xDF`
- feat : **l'Ogre** (`0x0048F230` + 3) : la charpente du Pourpre preux sans la parade, un combo et
  une charge bondissante qui repousse la victime
- feat : **la Chauve-souris** (`0x00489CA0` + 2) : pique depuis `0xc8` unites, et tourne en spirale
  autour de sa proie avant de s'y accrocher
- doc : les noms des sept routines restantes de la mine, relevés au bestiaire — Chef-Goblin, Ours
  gris (un second corps), Mille-patte, Scorpion, Jeune dragon, Troll, Araignee

## 2026-09-16 (suite 25)

- feat : **dix routines d'IA de la mine, et une de la vieille tour, sans une ligne de comportement
  neuve** — toutes sont des corps deja ecrits : Willie (x2), le Crabe geant, le Grizzly, le Geant,
  le Crotale (x2), le Poisson volant, le Goblin (+ `0x004A6290` en tour) et le lanceur de
  l'auberge (`0x00488210`, par sa table de sorts). Mine : 127 -> 144 sur 157
- doc : prouver une copie demande deux temps apres le diff — lire la donnee que designent les
  immediates, et verifier qu'un chargement deplace en registre n'est pas relu avant reecriture
- doc : la table de sorts de l'auberge a vraisemblablement quatre rangees, pas huit — la seconde
  moitie est celle de la mine ; et le Goblin-sorcier `0x0049E190` est le meme corps que
  `0x00487260`, transcrit deux fois

## 2026-09-16 (suite 24)

- feat : **dix gestionnaires d'un coup — la mine n'a plus d'effet a ecrire**, il n'y reste que ses
  vingt-trois routines d'IA
- feat : `0x0040BCA0`, la spirale qui s'ouvre et monte, et `0x004380C0` dans la grotte scellee —
  le meme corps a six differences, dont une image qui n'avance jamais
- feat : `0x00483B50` et `0x00486DE0`, l'emetteur accroche a son parent (copie conforme) ; debloque
  par `FUN_00536090`, la rotation de vecteur sur 64 bits que Ghidra lisait a deux parametres
- feat : `0x0040BF50` et `0x0043E170`, l'entite de scene qui s'agite, enfle et se dissout ;
  `0x0040B940`, la saisie de l'entite de scene 2 avec le fond qui vire et la gamme qui monte
- feat : `0x00408EF0`, le rocher qui tombe, eclate et blesse ; `FUN_00418F50`, les degats en
  pourcentage autour d'un point
- feat : `0x005413E0`, la poussiere du pas des dix autres zones — sortie de `TickGroundContact`, qui
  la portait recopiee en ligne ; `0x00443170`, le verbe de dialogue sans garde-fou
- doc : la recherche de copies passe aux **quasi-copies** (diff ligne a ligne contre les corps de
  taille voisine) : trois des dix en viennent
- doc : `0x0043E170` range son clip dans la case de l'argument — sans cible, il lit l'entite
  elle-meme comme un clip

## 2026-09-16 (suite 23)

- feat : `0x004084F0`, la grande spirale des sous-zones 5 a 8 — copie conforme de `0x00406680` a la
  planche pres ; avec elle `0x00408AB0` (= `0x00406C40`) et `0x00409120` (= `0x00406F60`), trois
  corps desormais parametres par leur planche
- feat : `0x0040C5D0`, l'eclat scintillant des sous-zones 3, 4 et 9 — le corps de `0x00406810`, mais
  son compte a rebours est tire par `rand % 3 + 3` et non `% 4 + 4`
- feat : **la bouffee qui blesse**, `0x00407D70`, un corps pour trois zones (`0x0040D6C0` montagne du
  dragon, `0x00439D30` grotte scellee) : frappe le groupe a 75 % de l'attaque du proprietaire, et s'eteint
  sur un membre en garde tourne vers elle — `FUN_004375A0`, transcrite
- feat : **l'onde qui afflige**, `0x00409F40` et `0x0040CEA0` : la cousine de l'onde de la foret, qui
  applique l'effet d'etat `+0x44` par une table de douze enveloppes, au nom du grand-parent
- feat : `0x00482FF0`, la gerbe qui frappe en grossissant — une adresse pour la mine, la vieille tour et la grotte scellee ; `0x0040C910`, l'eclat qui attend puis
  part en biais ; `0x0040BE50`, la tuile qui monte puis se retracte
- doc : les corps qui frappent quittent `Effects/` pour `Combat/Projectiles.cs` — test
  d'appartenance « ne decide de rien » ; placement a confirmer
- doc : `0x00483B50` reste bloquee par `FUN_00536090`, une rotation de vecteur dont Ghidra a perdu
  les parametres
- fix : quatre preuves de marge perimees par le parametrage de `TickMineSparkle_00406810`, relues et
  rescellees ; deux marges de 1 nouvelles prouvees

## 2026-09-16 (suite 22)

- feat : **neuf particules de la mine**, `Effects/MineParticles.cs`. Zone 1 : 109 -> 118. La table
  `0x0056adc8` etant finie, le lot vient des tables d'effets `0056B0C8` / `0056B1C8` / `0056B2C8`
- feat : **trois corps obtenus sans en ecrire une ligne** — `0x00409320` et `0x0040C730` sont
  `0x004070F0`, `0x00409410` est `0x004071E0` (475 instructions), a l'adresse de planche pres. Les
  deux corps existants prennent desormais leur planche en parametre, comme le reste du fichier
- doc : **les trois planches de la mine sont identiques octet pour octet** — `0x0054f1b0`,
  `0x0054f308` et `0x0054f4f0` portent les memes onze enregistrements, bornes des deux cotes de la
  meme facon, et les deux decalages employes sont les memes dans les trois (enregistrements 3 et
  7). Le compilateur a emis la meme particule une fois par bloc de sous-zones
- feat : `0x0040A610`, la cinquieme amorce du flash de sortie — ancre 4 et genre `0x33`, la ou ses
  deux sœurs prennent l'ancre 6 et le genre `0x39` : ce n'est pas une copie de plus
- feat : `0x004092B0`, l'etincelle qui se repose sur le **plancher fige** `+0x64` de son parent et
  non sur sa hauteur courante ; `0x004087E0`, l'escarbille qui monte en tournant et **borne
  elle-meme** son image de planche a 3
- feat : `0x00408DD0`, le semeur qui fait naitre six entites `0xff` en deux images autour du point
  vise par la camera ; `0x00409E40`, l'eclair qui bat le voile plein cadre une image sur deux ;
  `0x0040C460`, la braise a deux variantes qui ne noircit qu'en retombant
- doc : quatre relevés au desassembleur gardes sous `Legacy/Raw/Blaze/` ;
  `sh OpenForesia.Tools/check-descriptors.sh` repasse au vert, 0 marge a verifier

## 2026-09-16 (suite 21)

- refactor : **`Handlers/Mine.cs` coupe en deux** a 3 734 lignes — `Mine.cs` (2 093 l.) et
  `Handlers/MineMecanismes.cs` (1 693 l.). Aucun code n'a change : 1 951 lignes de code avant,
  1 951 apres, les memes 90 membres, verification par tri et `diff`
- doc : la coupure n'est pas une coupure de taille mais **de role**, avec un test d'appartenance —
  le decor (ce qui sonne, bouge, brille, tombe, **et frappe**) d'un cote ; de l'autre ce qui prend
  le groupe ou la partie en charge (compte les membres, lit la table des emplacements, lance un
  script, coupe les manettes, change de carte). **Abimer n'est pas decider** : le pilon et les deux
  masses restent du cote decor, la tremie passe du cote mecanismes parce qu'elle change de carte
- refactor : `ScrapeSoundTick` remis aupres de `TickScrapingBlock`, son autre appelant — il vivait
  dans la section de la tremie, qui est partie
- doc : `ARCHITECTURE.md` porte desormais le test `<Zone>.cs` / `<Zone>Mecanismes.cs`, et note que
  `Forest.cs` s'y preterait de la meme facon
- doc : ni `Mine.cs` ni `MineMecanismes.cs` ne declarent de champ statique — la coupure est donc a
  l'abri du piege d'ordre d'initialisation entre fichiers d'une classe partielle

## 2026-09-16 (suite 20)

- feat : `0x00451B40`, **l'effondrement** — le dernier creneau de la table `0x0056adc8` de la mine.
  **La table est complete** : 68 sur 68. Zone 1 : 108 -> 109
- feat : huit etats enchaines, dont quatre deplacent l'entite **en coordonnees polaires** — cap
  `+0x14`, rayon `+0x16` divise par deux — en faisant varier le cap (etat 3), le pas de cap
  (etat 4), le rayon seul (etat 6) ou le rayon a rebours (etat 7)
- doc : le compteur `+0x9a` **court a travers les etats 3, 4, 6 et 7 sans remise a zero** ; ce sont
  ses echeances (245 et 285 images), et non les etats, qui decident des enchainements
- doc : la surveillance du membre tombe essaie **trois candidats** — l'emplacement que designe
  l'octet `0x005e9062`, puis le meneur `0x005de538`, puis le **plus petit** emplacement actif (la
  boucle de repli descend de 3 a 0 et garde la derniere trouvaille)
- doc : relevé au desassembleur garde sous `Legacy/Raw/Blaze/FUN_00451B40.asm`

## 2026-09-16 (suite 19)

- feat : **dix autres gestionnaires de la mine**, `Handlers/Mine.cs`. Zone 1 : 98 -> 108. Il ne
  reste qu'un creneau a la table `0x0056adc8` de la mine, `0x00451B40` (1354 octets)
- feat : `0x00451460`, `0x00451630` et `0x00452E40`, **la saisie d'un personnage** — animation 6,
  monde gele le temps qu'elle se joue, puis bit `0x2000000` sur la victime et bouffee d'effets
- doc : **`0x00451460` ne reagit qu'au Nain** — elle remonte a l'enregistrement de personnage
  (`MainMem + 0xf0150 + emplacement * 0x2000`) et exige que les trois bits bas de l'octet
  d'identite valent 6. Seul declencheur de la mine qui lise une classe
- feat : `0x00451790` et `0x00453070`, **les portes a deux battants** : chacune fait tourner une
  entite voisine atteinte par `this + n * 0x9c`, le numero arrivant par un champ de placement
- feat : `0x004518D0`, la seconde chute de pierres — jumelle de `0x00451A00`, sans l'attente et
  avec une rotation tiree au sort
- feat : `0x00454610`, les cinq jets des sous-zones 3, 4 et 9 ; `EmitJetPair` resservait tel quel
- feat : `0x004533A0`, **la catapulte** — elle coupe les manettes, ecrit `+0x54` avec la puissance
  de `+0x16` et pousse deux voisines distinctes
- feat : `0x00454ED0`, le battant qui pivote aux deux bits bas de `DAT_00571570`, chaque moitie
  consommant le sien ; `0x004551D0`, **la nacelle du gouffre** qui se range au quai le plus proche
  du groupe
- fix : la comparaison de quai de `0x004551D0` doit etre **signee** — `& 0xfffff000` met le bit de
  signe en jeu, et en non signe la nacelle ne serait jamais allee chercher le groupe
- doc : neuf relevés au desassembleur gardes sous `Legacy/Raw/Blaze/`

## 2026-09-16 (suite 18)

- feat : **dix autres gestionnaires de la mine**, `Handlers/Mine.cs`. Zone 1 : 88 -> 98
- feat : `0x00452090` et `0x00454800`, **les deux ascenseurs** — meme forme, meme boite de comptage
  en (x, z), meme grondement toutes les dix images ; le premier est garde par le bit `0x40` de
  `0x005e96fc`, sans lequel il ne bouge pas et rend la main au script
- feat : `0x00452790` et `0x00453F60`, **le pilon** du creneau 11 : celui des sous-zones 0 a 2
  frappe a chaque image, celui des sous-zones 5 a 8 ne frappe qu'a l'arrivee et marque d'abord les
  membres touches (`0x20000000`, mot de contact `0x8000`)
- feat : `0x004538C0` et `0x00455570`, **le wagonnet** — 0x511 octets identiques a l'octet pres, un
  seul corps ; il joue toute sa course a la naissance pour en connaitre les deux bouts
- feat : `0x00454360` et `0x00455EB0`, **la tremie qui change de carte** ; la seconde borne le
  numero de sous-zone a 4 et refuse de partir tant qu'un membre est sous `0x12c000`
- feat : `0x00452410` et `0x00453650`, **les deux masses** : l'une tombe quand le groupe approche a
  600 unites, l'autre rebondit pendant dix-neuf images puis revient se pendre a sa pose
- fix : `TickScrapingBlock` appelle desormais `ScrapeSoundTick`, les cinq memes lignes que le
  creneau 37 portait en double
- doc : trois relevés au desassembleur gardes sous `Legacy/Raw/Blaze/` ; Ghidra rend le `0x514000`
  de `FUN_00453650` comme `FUN_00514000` — l'immediat tombe sur l'adresse d'une fonction


## 2026-09-16 (suite 17)

- feat : **dix autres decors de la mine**, `Handlers/Mine.cs`. Zone 1 : 78 -> 88
- feat : `0x00454E10`, le mecanisme qui bascule au signal — il attend le bit 4 de `0x00571570` que
  `0x00454D90` pose quand le groupe s'y rattache : une paire declencheur / mecanisme
- feat : `0x00451A00`, la chute de pierres au bit `0x10` de la banque de script 3 ; `0x00452670`, le
  debris qui se detache et tombe ; `0x00453E60`, le decor qui s'effondre dans le sol
- feat : `0x00454130`, le decor qui respire (echelle verticale sur un sinus) ; `0x004528D0`, celui qui
  file droit le long de son cap ; `0x00454BB0`, la navette et son circuit de huit etapes
- feat : `0x00453280`, le decor a deux places, qui se tient toujours a la plus proche du point vise
  par la camera (racine carree de winsys3d)
- feat : `0x00451250` et `0x00452BD0`, les emetteurs a positions fixes — l'entite saute un instant
  sur des coordonnees codees en dur (3 x 2 points, 12 points), y fait naitre l'effet `0x3c` et revient
- doc : six relevés au desassembleur gardes sous `Legacy/Raw/Blaze/` ; `FUN_004551d0` lit aussi le
  mot `0x00571570`, non transcrit

## 2026-09-16 (suite 16)

- feat : **dix autres decors de la mine**, `Handlers/Mine.cs`. Zone 1 : 68 -> 78
- feat : les creneaux 40, 41 et 43 portent le meme decor dans les trois blocs de la mine — neuf
  corps pour trois : `TickBobbingSpinner` (danse sur place, `0x00452950` / `0x00454220` /
  `0x004544E0`), `TickScrapingBlock` (le bloc qui racle quand on le pousse, `0x004529F0` /
  `0x004542C0` / `0x00455A90`, masque de deplacement `0x882` ou `0x800`), `TickFlickeringGlow`
  (la lueur grise qui palpite, `0x00452A90` / `0x00454450` / `0x00456010`, absents de `Blaze.c`)
- feat : `0x00454D90`, le decor que le groupe arme en s'y rattachant ; et `FUN_00454580`,
  `AddEntityVelocity_00454580`, qui debloque aussi `0x004528D0` et `0x00453E60`
- doc : `TickScrapingBlock` remet son delai de son a zero a chaque tour tant que son etat vaut 0,
  et rien dans le corps ne le fait changer — le son peut repartir a chaque image de poussee

## 2026-09-16 (suite 15)

- feat : **dix decors de la mine** (table `0x0056adc8`), dans le nouveau fichier `Handlers/Mine.cs`.
  Zone 1 : 58 -> 68
- feat : `0x004523A0`, `0x00454B40`, `0x00453000`, `0x00454AD0` — le decor qui sonne a intervalle
  fixe, attenue par la distance. Quatre corps identiques instruction pour instruction a cinq
  constantes pres (periode, son, hauteur, gain, portee) : un seul corps,
  `TickRepeatingDistanceSound`
- feat : `0x004511F0`, sa cousine dont la hauteur et le gain viennent de l'entite ; `0x00453DE0`, le
  balancier qui grince aux deux bouts de sa course
- feat : `0x00452B60`, le decor qui flotte sur un sinus ; `0x00452B20`, le souffle qui fait naitre
  l'effet `0x3d` une image sur deux
- feat : `0x00451170` et `0x004545B0`, les decors gardes par un bit de script de la banque 0 — le
  premier s'efface avant de poser `0x6000`, le second le pose d'un coup
- doc : six relevés au desassembleur gardes sous `Legacy/Raw/Blaze/` ; les quatre autres corps
  sont decompiles

## 2026-09-16 (suite 14)

- feat : **dix gestionnaires de la mine, aucun corps neuf** — zone 1 : 43 -> 53 implementes.
  Recherche de copies sur les textes de `Blaze.c` (adresses masquees), confirmee par `diff` : chaque
  copie ne differe de son modele que par l'adresse de planche
- feat : `0x00408040` et `0x0040A670`, amorces de la fumee violette (`TickPurpleSmoke`), sous-zones
  5 a 8 et 3, 4, 9
- feat : `0x0040B1B0` et `0x0040B900`, amorces de la gerbe aux cinq phases (`TickFivePhaseBurst`),
  meme forme que `0x00411860` ; elles se reproduisent elles-memes (`+0x98` = 0 et 1)
- feat : `0x00409C10` et `0x0040C820`, la bulle (`TickRisingBubble`) ; `0x00409A30` et
  `0x0040CA80`, l'eclat (`TickBurstingSpark`)
- feat : `0x0040A600`, la seconde souche `ret` nu — branchee au repartiteur et aux verbes de zone
  (elle rend l'indice du verbe, comme `0x00401FA0`) ; elle compte dans presque toutes les zones
- feat : `0x004431B0`, verbe de zone — le nombre de membres du groupe encore debout, range dans la
  banque de script 2
- doc : **les planches de la mine sont recopiees par blocs** — sous-zones 5 a 8 = sous-zones 0 a 2
  decalees de `0x158`, octet pour octet ; sous-zones 3, 4, 9 = decalage `0x340`, a partir de
  `0x0054f190` seulement. Six planches relevees (`0x0054F2D8`, `F308`, `F450`, `F4D0`, `F4F0`,
  `F620`), toutes conformes au binaire ; neuf nouvelles marges courtes, toutes prouvees et scellees
- feat : `0x00409D00`, `0x0040CC60` et `0x00408C60` — l'etincelle qui tremble et l'eclat qui
  retombe, sous-zones 5 a 8 et 3, 4, 9. Leurs modeles sont factorises, planche en parametre :
  `TickJitterSpark_00407AD0` et `TickFallingGlint_00406DF0`. Planche `0x0054F388` relevee (jumelle
  de `0x0054F230`). Zone 1 : 53 -> 56
- feat : `0x0040AAC0` et `0x0040B8D0`, les deux amorces de la scene parlante de la mine — le corps
  est `TickForestCutsceneWithDialogue` (`FUN_0040aaf0` = `FUN_00411270` au bloc prive pres). Nouveau
  fichier `Cutscene/MineScenes.cs`. Zone 1 : 56 -> 58 ; plus aucune copie directe dans la mine
- doc : la premiere amorce passe la **piste 8** — le changement de musique de la scene parlante,
  jamais demande en foret, l'est ici
- doc : **le bloc prive de la mine, `0x005713c0`, est DANS `s_ViewParams_00571270`** — la borne
  `0x00571400` de cette allocation est la fin de `.data` dans le fichier, pas une frontiere de
  structure. Adresse par `Mem.ViewParams` pour ne pas creer un alias. L'enregistrement suivant,
  `0x005713f0`, chevaucherait deux allocations et un trou (`0x00571400..0x0057141f`) : pas encore employe

## 2026-09-16 (suite 13)

- feat : `check-descriptors.sh` garde l'heritage de `+0x40` — il relit dans `Blaze.exe` les 430
  gestionnaires des pools `0x005ff400` / `0x005edee0`, examine les 235 transcrits, et signale tout
  corps C# qui pose `+0x3c` sur son propre enregistrement (alias, forme `uint*` et delegation `=>`
  suivis). Aujourd'hui : aucun

- doc : le `+0x40` herite par `FUN_0047a9d0` / `FUN_0047aae0` ne peut deborder nulle part — sur les
  430 gestionnaires des pools `0x005ff400` et `0x005edee0`, aucun ne pose de planche sur son propre
  enregistrement (Blaze.c, releves et binaire) : l'enfant garde la paire `+0x3c` / `+0x40` du parent

- feat : `check-descriptors.sh` connait les marges deja prouvees — `OpenForesia.Tools/marges-verifiees.txt`,
  une ligne par site, scellee sur l'empreinte des corps dont la preuve depend (`--sceller`). Le site
  s'affiche `prouve` ; un corps modifie rend `PREUVE PERIMEE`, un corps renomme `PREUVE CASSEE`, un
  site disparu une preuve ORPHELINE. Les 25 marges courtes y sont : 25 prouvees, 0 a verifier

- doc : les **quinze** marges a 2 sont sures aussi — onze corps n'ecrivent que des zeros dans
  `+0x40`, les deux etincelles de la mine le tirent a `rand % 2` (0 ou 1), et les deux semeurs
  (`00445CF0`, `TickSleepAura_004761D0`) passent leur planche a un enfant (`00477600`, `00476290`)
  ne par `FUN_0047ac40` et qui ne touche jamais `+0x40`

- doc : les huit marges a 1 de `check-descriptors.sh` sont **toutes sures**, verifiees contre
  `Blaze.c` et le binaire — cinq corps posent `+0x40 = 0` sans jamais l'avancer, trois ne
  l'ecrivent pas et naissent tous par `FUN_0047ac40`, qui le remet a zero (`DAT_005f2d80`)
- doc : `FUN_0047a9d0` et `FUN_0047aae0` **ne remettent pas** `+0x40` a zero : l'enfant herite
  celui du parent. « Le corps n'ecrit jamais +0x40 » n'est une preuve que dans le pool `0x005f2d40`
- fix : `check-descriptors.sh` ne voyait pas le decalage pris DANS un corps factorise —
  `TickFivePhaseBurst` role 3 pose `atlas + 8` (`0x0054f810` / `0x0054f928`), deux marges a 1 de
  plus, sures elles aussi. Il les mesure desormais, et liste a part tout emploi d'une planche recue
  qu'il ne sait pas mesurer (passage a un tiers, locale) au lieu de l'ignorer

## 2026-09-16 (suite 12)

- fix : **`check-descriptors.sh` ratait deux familles entieres, en silence.** Ses deux passes
  rendaient une liste plus courte, jamais une erreur — le mode d'echec le plus couteux de ce depot,
  et cette fois c'est le garde-fou lui-meme qui l'avait
- fix : la passe 2 ne reconnaissait que le motif LITTERAL `(e + 0x3c) = Descriptors...()`. Tout
  site qui passe la planche AUTREMENT lui echappait : les corps factorises qui la prennent en
  parametre (`TickRisingBubble(e, Descriptors0054F6F8())`) et les planches passees a un semeur
  (`SpawnStatusEffectEntity_0047AC40(..., Descriptors00551BC8(), ...)`). Desormais tout appel
  compte, ou qu'il apparaisse
- fix : les **accesseurs alias** — `DescriptorsA() => DescriptorsB() + off` — sont resolus en
  chaine. Sans cela leur marge sortait a zero, ce qui se lisait comme un defaut grave alors que
  c'est un simple renvoi
- fix : la passe 1 n'acceptait que le nom `s_Descriptors_<VA>`. Les tableaux qualifies
  (`s_SplashDescriptors_...`, `s_DustDescriptors_...`, `s_TrapBurstDescriptors_...`) n'etaient donc
  **pas relus dans le binaire du tout**
- doc : mesure avant / apres — **53 -> 62 planches verifiees, 55 -> 155 sites mesures**, 0 erreur de
  contenu, 0 plage non relevee. La correction est **strictement additive** : aucune ligne de
  l'ancienne sortie n'a disparu
- doc : le controle elargi signale **23 marges courtes, dont huit a UN seul enregistrement
  restant** — `0x004038F0`, `0x004079E0`, `0x0040EF40`, `0x0040F260`, `0x00411860`, `0x00414CD0`,
  `0x004459E0`, `0x004769A0`. Sept d'entre elles etaient invisibles jusqu'ici. Une marge de 1 n'est
  sure que si le corps laisse `+0x40` a zero : a verifier corps par corps

---


## 2026-09-16 (suite 11)

- feat : `0x004071E0`, **l'eventail** — sous-type 51, 1 568 octets absents de `Blaze.c`, le plus
  gros corps de la mine. Sa taille trompe : c'est une cascade de QUATRE blocs quasi identiques,
  meme forme que `FUN_004747F0`. Le geste : un tirage choisit un SECTEUR de 45 degres autour du
  cap, ses deux points sont calcules a rayon 150 avec gigue, et un second tirage place l'entite a
  un CRAN sur le segment qui les joint. Trois etats, trois genres, et une bande de quatre images
- feat : `0x00408680` et `0x00408910` — les jumeaux de `0x00406810` et `0x00406AA0` pour les
  sous-zones 5 a 8. Compares ligne par ligne : deux differences pour le premier, UNE pour le
  second, et ce sont des adresses de planche. Zone 1 : 40 -> 43 implementes
- doc : **les planches sont dupliquees comme les corps** — `0x0054f3d8`, `0x0054f3f8` et
  `0x0054f408` reprennent octet pour octet `0x0054f280`, `0x0054f2a0` et `0x0054f2b0`. Le code est
  factorise (`TickMineSparkle_00406810`, `TickMineWallDust_00406AA0`), la donnee non : ce sont des
  adresses distinctes dans `.data`
- doc : **QUATRE gestionnaires se partagent le bloc `0x0054f1b0`**, a quatre decalages —
  `0x00406680` (+0), `0x00407800` (+1), `0x004071E0` (+3), `0x004070F0` (+7). Avec l'eventail, les
  DEUX suites de quatre tuiles `8 x 8` de ce bloc ont enfin leur consommateur
- fix : **point aveugle mesure dans `check-descriptors.sh`** — sa section 2 detecte les sites
  d'installation sur le motif LITTERAL `(e + 0x3c) = DescriptorsXXXX()`. Des qu'une planche passe
  en parametre, le site devient invisible et la marge n'est plus verifiee. Cinq planches de la mine
  sont dans ce cas depuis la factorisation d'aujourd'hui, et les **dix corps parametres de la foret
  y echappaient deja**. Le CONTENU reste verifie par la section 1. Marges consignees a la main dans
  les bandeaux ; le script gagnerait a suivre un niveau de parametre

---


## 2026-09-16 (suite 10)

- feat : **six gestionnaires de la mine d'un coup** — `0x00407800`, `0x004079E0`, `0x00407AD0`,
  `0x00407C10` (les quatre decompiles dans `Blaze.c`), plus `0x00407FE0` et `0x00408490` (96 octets
  chacun, au desassembleur). Zone 1 : 34 -> 40 implementes
- fix : **la plage `0x0054f180` etait relevee TROP COURTE — cinq enregistrements au lieu de six.**
  Le sixieme, `0x0054f1a8`, avait ete pris pour la borne parce qu'il porte le motif separateur ;
  `0x004079E0` le pose en `+0x3c`, donc c'est une tuile. La lecon se repete : un motif separateur
  est un enregistrement, et quelqu'un finit toujours par s'en servir
- doc : **TROIS gestionnaires se partagent `0x0054f180`**, chacun a son decalage et aucun ne lit ce
  que lit un autre — `0x00406130` au decalage 0, `0x00407AD0` au 2 (il tire son image entre 0 et 2
  sur les trois tuiles de la page 03), `0x004079E0` au 5. Un quatrieme, `0x00407800`, prend
  `0x0054f1b0` au decalage 1. Le cas d'ecole de la regle de la plage, quatre fois sur deux blocs
- doc : `0x00407FE0` est **la QUATRIEME amorce du flash de sortie de zone**, et une copie octet a
  octet de `0x004060C0` sur ses 49 premiers octets. Le compilateur a emis la meme amorce **une par
  groupe de sous-zones** : la foret en a deux, la mine en a deux
- doc : `0x00408490` est `0x00406570` reemis pour les sous-zones 5 a 8, a TROIS differences — sa
  planche `0x0054f418` est la copie conforme de `0x0054f2c0` (donnee dupliquee comme le code), sa
  teinte nait a `0x96` et non `0x20`, et sa hauteur est relue du `+0x64` et non du `+0x60`
- doc : `0x00407800` est la soeur de `0x00406F60` — memes deux signatures, l'inversion sinus/cosinus
  et la double ecriture d'echelle —, mais sous une gravite DOUBLE et avec **deux portes pour
  mourir** : une chance sur six passe la dixieme image, et la seizieme de toute facon
- fix : sous-type de `0x004070F0` corrige, **62 et non 51** — l'indice 51 est `0x004071E0`
- doc : `0x004071E0` (1 568 octets) mis de cote pour une seance dediee. Son etat 0 porte quatre
  blocs quasi identiques qui parcourent huit secteurs de 45 degres autour du cap, deux points par
  bloc a rayon 150 ; cinq coordonnees y sont portees en registres et en pile

---


## 2026-09-16 (suite 9)

- feat : `0x00406DF0`, **l'eclat qui retombe en scintillant** — decompile. Meme balistique que
  `0x00406C40`, vitesses plus molles ; ce qui les separe est l'image, tiree AU HASARD a chaque
  tour au lieu d'avancer. Elle ne touche jamais sa teinte
- feat : `0x00406F60`, **la gerbe qui monte haut et noircit** — absente de `Blaze.c`, 400 octets.
  La plus haute des trois balistiques. **Elle inverse sinus et cosinus** a la naissance : Z recoit
  le sinus et X le cosinus, l'inverse de toutes ses voisines — son placement est tourne d'un quart
  de tour. Elle noircit par DEBORDEMENT d'octet (`+0xdc` sans borne a zero)
- feat : `0x004070F0`, **la petite animation qui ne bouge pas** — absente de `Blaze.c`, le corps le
  plus simple du fichier : aucune vitesse, huit images de vie, deux echelles tirees separement et
  par soustraction. Zone 1 : 31 -> 34 implementes
- doc : **LA REGLE DE LA PLAGE PRISE SUR LE FAIT.** `0x004070F0` pose `&DAT_0054f1e8`, qui est la
  planche `0x0054f1b0` AU DECALAGE 7. Les quatre images que le relevé de `0x00406680` portait sans
  consommateur connu sont exactement celles qu'il joue. Un relevé au plus juste sur le premier
  corps aurait casse le second. `check-descriptors.sh` le confirme de lui-meme :
  « 0x0054F1B0 +0x38 4 restant(s) »
- doc : deux nouvelles bandes de quatre images relevees, `0x0054f230` (bornee par la planche de
  `0x00406C40`) et `0x0054f208` (bornee par un changement d'attribut, et qui ferme aussi
  `0x0054f1b0` par l'autre bout)

---

## 2026-09-16 (suite 8)

- feat : `0x00406AA0`, **la poussiere posee contre une paroi** — sous-type 55, decompile dans
  `Blaze.c`. Sa naissance choisit un AXE a pile ou face : l'axe tire recoit un pas de
  `(rand % 3 - 1) * 0x64000`, l'axe NON tire recoit un etalement de -100 a +99 cases. Trois etats,
  le bit `0x4000` tenant l'attente comme dans `EntityHandler_0040F650`
- feat : `0x00406C40`, **l'eclat qui retombe** — sous-type 54, absent de `Blaze.c`, 432 octets au
  desassembleur. **Premier corps vraiment balistique du fichier** : vitesse sur les trois axes et
  gravite de `0x1000` par image sur la verticale. Vingt-quatre images exactement, l'image avancant
  aux images 6, 12 et 18. Zone 1 : 29 -> 31 implementes
- doc : **un cas de diagonale** dans `0x00406AA0` — quand l'axe tire sort un `0`, les DEUX axes
  recoivent l'etalement, et le meme : l'entite se pose sur la diagonale exacte. Le binaire teste
  les deux independamment
- doc : **troisieme ecriture morte de la seance** — `0x00406C40` ecrit `+0x28` et `+0x2a` deux fois
  a la naissance, le second tirage ecrasant le premier. Il ne reste des premiers que leur
  consommation du generateur
- doc : planche `0x0054f250`, quatre enregistrements, bornee par le **debut de la bande suivante**
  (le `y` retombe de `0xc0` a `0x20`) — une quatrieme espece de borne apres le motif separateur, le
  changement d'attribut et l'adresse referencee par du code

---

## 2026-09-16 (suite 7)

- feat : `0x00406970`, **la bouffee qui part de biais, enfle et ralentit** — sous-type 56 du bloc
  `0x00553ea8`, sous-zones 0-2 de la mine. Absente de `Blaze.c` alors que sa voisine immediate
  `FUN_00406aa0` y est ; 304 octets au desassembleur. Zone 1 : 28 -> 29 implementes
- doc : **un seul champ porte deux roles** — `+0x14` est a la fois la vitesse et le compte a
  rebours : l'entite avance de `sin/cos(+0x12) * (+0x14 * 2)` puis `+0x14` perd un, donc elle part
  vite et **ralentit jusqu'a l'arret, ou elle meurt**. Le cap `+0x12` est tire une fois et jamais
  retouche : c'est une droite, pas une spirale
- doc : planche `0x0054f280`, **quatre enregistrements et c'est une vraie bande d'animation** —
  quatre tuiles `0x1f x 0x1f` sur la ligne `y = 0xa0`, `x` avancant de `0x20`. Le corps confirme :
  `+0x40` va de 0 a 3 puis **s'arrete sur 3**, il ne reboucle pas comme `FUN_00404c80`. Bornee par
  `0x0054f2a0`, encore une borne prouvee par le code. Partagee avec `FUN_00406aa0`
- doc : **la mort ne coupe pas le tour** — le bit de vie tombe au milieu du corps et tout ce qui
  suit (teinte, echelle, avance d'image) s'execute quand meme sur cette derniere image

---

## 2026-09-16 (suite 6)

- feat : `0x00406810`, **l'eclat bref qui scintille** — sous-type 60 du bloc `0x00553ea8`,
  sous-zones 0-2 de la mine, decompile dans `Blaze.c`. Deux variantes par `+0x14`, une planche
  chacune ; elle se place en deux temps (le long du cap recu, puis une boite de dispersion) et
  **scintille en tirant son image a pile ou face a chaque tour**. Elle meurt de vieillesse, pas
  d'extinction : 4 a 7 images. Zone 1 : 27 -> 28 implementes
- doc : planches `0x0054f2a0` et `0x0054f2b0`, **deux enregistrements chacune, bornees par le CODE
  et non par la donnee** — toute la region est une suite uniforme de tuiles `0x1f x 0x1f` sur la
  meme page, sans motif separateur ni changement d'attribut ; ce qui coupe, ce sont les adresses
  que du code reference
- doc : `check-descriptors.sh` signale « marge courte » sur ces deux lignes et **c'est correct** :
  le corps tire `+0x40` entre 0 et 1, donc deux enregistrements sont exactement ce qu'il faut. Ne
  pas allonger ces plages pour faire taire le drapeau

---

## 2026-09-16 (suite 5)

- feat : `0x00406680`, **la grande spirale qui rentre** — sous-type 61 du bloc `0x00553ea8`,
  sous-zones 0-2 de la mine. Absente de `Blaze.c`, 400 octets au desassembleur.
  Zone 1 : 26 -> 27 implementes
- doc : c'est la **cousine** de la variante 0 de `0x00406130`, pas sa copie — meme geste, aucune
  constante en commun : elle se disperse EN HAUTEUR a la naissance (-380 a +379), tourne cinq fois
  plus vite (`pas * 20` contre `* 4`), sa houle vaut `sin * 50` contre `sin * 0x28`, elle meurt
  sous un rayon de `0x32` contre `0x50`, et ses trois canaux sont bornes a `0x80` ENSEMBLE la ou
  l'autre bornait chacun a sa valeur
- doc : planche `0x0054f1b0`, onze enregistrements, fin `0x54f208` — **troisieme plage de cette zone
  de `.data` qui commence sur le motif separateur**, mais la premiere dont la borne haute est un
  changement de famille d'attributs et non le motif
- doc : `0x00545a1c` et `0x00545a16` sont les sauts par la table d'imports vers `Ordinal_451`
  (sinus) et `Ordinal_450` (cosinus) — recoupe sur `FUN_00404c80`, que Ghidra decompile et nomme

---

## 2026-09-16 (suite 4)

- feat : `0x004065D0`, **la jumelle du 0x00406570, pilotee par l'etat de son porteur** — sous-type
  47 du bloc `0x00553ea8`, sous-zones 0-2 de la mine. Absente de `Blaze.c`. Meme planche, meme
  `+0x2e`, meme hauteur ecrite deux fois ; ce qui la distingue tient en une recopie et trois tests :
  elle copie le `+0x10` du porteur dans son `+0x12` et s'en sert comme consigne — 1 elle
  s'eclaircit de 2, 2 elle s'assombrit de 3, 0x65 elle est remise a noir d'un coup.
  Zone 1 : 25 -> 26 implementes
- doc : elle **nait noire** la ou sa jumelle nait a `0x20` — elle est donc invisible tant que son
  porteur n'est pas passe par l'etat 1. Et comme sa jumelle, **elle ne meurt jamais d'elle-meme**

---

## 2026-09-16 (suite 3)

- feat : `0x00406570`, **la marque accrochee a son porteur** — sous-type 48 du bloc `0x00553ea8`,
  sous-zones 0-2 de la mine. Absente de `Blaze.c`, dix-sept instructions au desassembleur. Elle
  suit son porteur par `+0x6c` et ne meurt jamais d'elle-meme. Zone 1 : 24 -> 25 implementes
- doc : **sa hauteur est ecrite deux fois** — `Y` est pris au `+0x60` du PORTEUR (et non a son
  `+0x20`), puis aussitot ecrase par le `+0x60` de l'entite elle-meme des que l'etat vaut 1. La
  valeur prise au porteur est morte ; transcrite telle quelle
- doc : planche `0x0054f2c0`, trois enregistrements, fin `0x54f2d8` — et **ce qui suit son
  separateur est la copie conforme de ce qui suit celui de `0x0054f180`** : la donnee est dupliquee
  comme les corps le sont

---

## 2026-09-16 (suite 2)

- feat : `0x00406120` / `0x00406130`, **trois particules en une fonction** — sous-type 57 du bloc
  `0x00553ea8`, sous-zones 0-2 de la mine. L'amorce est absente de `Blaze.c`, le corps y est.
  Variante 0 : nait noire, spirale vers l'INTERIEUR, s'eclaircit, meurt sous un rayon de 0x50.
  Variante 1 : nait claire, spirale vers l'EXTERIEUR, s'assombrit. Variante 2 : dispersee, monte
  en accelerant, s'eteint apres un decompte. Zone 1 : 23 -> 24 implementes
- feat : nouveau fichier `Effects/MineParticles.cs` — les effets de la zone 1, sur le patron de
  `ForestParticlesA/B.cs`
- doc : **le separateur `0x0054f180` EST une tuile, et ce corps s'en sert** — un motif qui borne une
  plage peut etre le premier enregistrement de la suivante. Plage relevee : cinq enregistrements,
  fin `0x54f1a8`, confirmee par `check-descriptors.sh`
- doc : bizarrerie fidele de la variante 2 — `+0x12` est ecrit DEUX fois a la naissance, un tirage
  sur douze bits puis `rand() % 5 + 5` qui l'ecrase. Du premier il ne reste que sa consommation du
  generateur, qui decale tous les tirages suivants

---

## 2026-09-16 (suite)

- feat : `0x004060C0`, **la troisieme amorce du flash blanc de sortie de zone** — celle de la mine
  abandonnee (zone 1, sous-zones 0-2, sous-type 33 du bloc `0x00553ca8`). Absente de `Blaze.c`,
  relevee au desassembleur : dix-sept instructions, le meme corps que les deux amorces de la foret
  a DEUX differences pres — le genre `+0x5c` vaut `0x39` et non `0x3a`, et l'ancre lue est
  l'enregistrement **6** de la table `0x005fa5e0` et non le **0**. Zone 1 : 22 -> 23 implementes
- feat : nouveau fichier `Effects/AreaExitFlash.cs` — le corps `0x004183d0` et ses trois amorces
  sortent de `ForestParticlesA.cs` : le flash n'est pas un effet de la foret, la mine vient de le
  prouver
- fix : `DAT_005fa9a4` et ses cinq voisins ne sont pas des globales isolees —
  `0x005fa9a4 - 0x1c - 0x005fa5e0 = 0x3a8 = 6 x 0x9c` : ce sont les champs de pose du septieme
  enregistrement de la troisieme table d'entites
- fix : **mes appels aux trois recensements pendant le decoupage ecrivaient dans `/dev/null`** — ces
  scripts ecrivent sur la sortie standard, c'est l'appelant qui redirige. Le `git status` vide ne
  prouvait donc rien pour eux. Rejoues correctement : `ObjetsAtteignables.md` et
  `SortsAtteignables.md` sont bien inchanges, aucune regression n'avait ete masquee

---

## 2026-09-16

- feat : `Screens/State03/WorldMapTick.cs` — **74 072 lignes** — est découpé en **70 fichiers** et
  onze répertoires (`Tick/`, `Dispatch/`, `Combat/`, `Physics/`, `Entities/`, `Effects/`, `Items/`,
  `Spells/`, `Cutscene/`, `Ai/`, `Handlers/`), **à l'octet près** : 70 vérifiés, 0 en échec,
  0 ligne modifiée. Un fichier par créature dans `Ai/`, une école par fichier dans `Spells/`
- feat : `apply-split.ps1` gagne `-Residual`, `-PreambleRanges`, `-ResidualPreambleRanges` et
  `-KeepDone` — sans ce dernier, la redérivation efface en silence les bandeaux écrits à la main
- feat : `worldmaptick-inventory.awk` et `worldmaptick-manifest.awk`, avec un garde-fou qui refuse
  de générer le manifeste depuis le fichier de travail plutôt que depuis la référence figée
- fix : quatre scripts codaient `WorldMapTick.cs` en dur (`census-entity-handlers.sh`,
  `census-item-effects.sh`, `census-spells.sh`, `check-descriptors.sh`) et seraient **tombés à zéro
  sans erreur ni message** après le découpage ; corrigés et prouvés à diff vide avant le premier
  déplacement
- fix : trois noms de créature étaient faux et deux cachaient une bête entière — le
  **Goblin-sorcier** `0x0049E190` et le **Crotale** `0x0049E920` (ex-`BlockAPatrol`), la **Fourmi
  géante** `0x00499DE0` (rangée sous `Ladybug`), et l'**Abeille tueuse** `0x004A3C30`
  (ex-`FlyingBeast`)
- doc : sur les **130 bandeaux** `// ====` du fichier, 14 débordaient sur plusieurs sujets, dont
  cinq couvraient jusqu'à six fichiers cibles sous un seul titre — « LES TROIS DERNIÈRES POSTURES »
  cachait la résolution de combat entière
- doc : le piège de l'ordre d'initialisation des champs statiques **ne se rejoue pas** — un appel de
  méthode n'est pas une lecture de champ ; les sept champs suspects sont sûrs, zéro ligne de code
  modifiée
- doc : `ARCHITECTURE.md` porte la carte des onze répertoires de `State03/` et leur test
  d'appartenance ; `CLAUDE.md`, `DataSymbols.md` et `REVERSE_STATUS.md` suivent
- doc : la regle qui evite le prochain fichier de 74 000 lignes — dans `State03/`, on range tout de
  suite, on n'entasse pas « en attendant », et **un fichier neuf se propose, il ne se cree pas tout
  seul**. Les deux a surveiller : `Spells/School0Wizard.cs` (9 137 l.) et `Handlers/Forest.cs`
- doc : `worldmaptick-baseline.txt` (3,1 Mo) est ignore par git — regenerable de l'historique, avec
  le piege `core.autocrlf=true` ecrit dans le `.gitignore`, `ARCHITECTURE.md` et `verify-split.ps1`

---

## 2026-09-15 (suite)

- doc : **LB-030**, le souffle du Mechant ours fait clignoter l'ecran en rouge — confirme sur le jeu officiel. Le voile plein ecran est un rectangle semi-transparent (`0x62`) que `winsys3d.dll` peint opaque, et l'effet `0x00413330` l'allume une image sur deux
- feat : **l'interpreteur de script est complet, 39 opcodes sur 39** — les dix derniers d'un bloc : son joue (15), camera (20), couleur de fond (21), objet tire au sort (27), degats en pourcentage (31), tuer ou retirer toutes les entites de scene (32, 33), no-op (35), rentrer dans la sous-zone par une autre entree (36), echanger les deux jeux de banques d'etat (37), statistiques d'entite +0x100 (38)
- fix : l'opcode 27 (`FUN_00515d30`) etait dans Blaze.c mais n'avait jamais ete branche au repartiteur
- fix : **l'opcode 34 de l'interpreteur de script** (`0x00515ed0`, changer la musique) — la porte avant le Grizzly de la foret plantait dessus. Les feuilles de route comptent les gestionnaires d'entite, pas les opcodes : « foret complete » ne couvrait pas les scripts
- feat : **le Gremlin de la grotte scellee**, `0x004F3350`, `0x004F5630`, `0x004FC9B0` — un terrestre qui frappe a chaque image de son geste, bondit, charge et fait naitre l'effet 0xdc
- doc : **deux especes du meme nom, deux comportements sans rapport** — le Gremlin de la vieille tour vole et incante, celui de la grotte scellee court et charge. Le nom d'espece ne designe pas une routine
- feat : **la Crapouille, la Gargouille et le Crapaud**, `0x004ACC20`, `0x004CE120`, `0x004DB610` — trois especes, UNE routine : une volante qui ondule, s'accroche, se pose au sol et en repart en pique
- doc : la poussee de pique de ces trois especes vaut toujours 0x14000 — un decalage dont le compte est masque sur cinq bits annule la hardiesse
- feat : **le Pourpre preux**, `0x004D1F70` — et ses deux copies conformes de la grotte scellee (`0x004E67C0`, `0x004EB530`) : un chevalier a cinq attaques (coup, combo, charge qui projette, coup en l'air, effet) qui PARE quand sa cible frappe
- doc : la parade du Pourpre preux compare la timidite a un REGISTRE — le dernier tirage de la replanification, ou a defaut le pas d'avance de l'integration
- feat : **le Gremlin**, `0x004A7190` — et **cinq gestionnaires d'un coup** (`0x004B22D0`, `0x004E8DF0`, `0x004EDB60`, `0x004F8270`) : les cinq corps ne different que par l'adresse de leur table de sorts, et les cinq tables portent les memes octets. Une volante qui ondule, pique, s'accroche a sa proie et incante
- doc : le Gremlin lance `0x0020` (famille 0 indice 32, hors des 29 fiches) quand sa variante depasse 1 — le registre du sort sort d'une boucle sans etre recharge. Defaut de 1998, non observe en jeu
- feat : **l'etat de jeu 10**, `FUN_00513ba0` (`Screens/State10/RebootScreen.cs`) — ce que la ligne REBOOT du menu de mise au point lance : deux images qui defilent, une animation superposee, puis retour a l'etat 0. Plus aucune ligne du menu ne leve
- feat : **MONITOR est transcrit** — les cinq sites que la ligne MONITOR allume sont transcrits : l'en-tete ANG/LOC/DPT/CHG, le **journal des drapeaux** (chaque bit de FLAG, ITEM ou SYSTEM qui bouge pendant l'image, dans une boite), le trace de portee du corps a corps en eclats, et le bandeau de calcul des coups et des sorts
- fix : deux releves de ces lacunes etaient faux — le trace fait naitre le sous-type 0 (pas 4 a 7), et le bandeau de sort emporte l'attaque et la defense (pas la famille et l'indice : `FUN_0053a2b0` reutilise ces emplacements de pile)
- feat : **`FUN_0051aa30`, le MENU DE MISE AU POINT de la carte du monde** — ouvert par SELECT une fois « Gurkha » trace : treize lignes (QUEST, MAP, PLAYER, FLAG, WORK, TEMP, ITEM, SYSTEM, TEX, REBOOT, MENU, MONITOR, FONT), teleportation, taille du groupe, edition bit a bit des banques d'etat, visionneuse de pages de texture. Transcrit contre ses 3 616 octets desassembles
- doc : **les banques d'etat ont leurs noms d'atelier** — `0x005e96e0` FLAG, `0x005e9780` WORK, `0x005e9060` TEMP, `0x005e9840` ITEM ; et `0x005e9000` (SYSTEM) est recopie a `+0x2e0` de chaque fiche de personnage
- doc : `DAT_005dbc14` / `DAT_005ddfd0` / `DAT_005dbc18` sont les lignes QUEST, MAP et PLAYER — renommes
- feat : **LA ZONE 0, L'AUBERGE, EST COMPLETE — 48 sur 48** : troisieme ensemble entierement implemente apres les tables statiques et la foret. Treize creneaux restaient ce matin ; quatre n'avaient besoin d'aucun code
- feat : **`0x00487260`, le QUATRIEME LANCEUR DE SORTS du depot** — 1904 octets absents de `Blaze.c`, la seconde et derniere routine d'intelligence de l'auberge. Elle porte le mecanisme d'incantation du Behemoth **au complet** : meme bit `0x800`, memes quatre etats dans `+0x10`, meme entite `0xbd`, meme masque de traits, meme duree lue dans la fiche. Troisieme emploi de ce bloc, et le premier hors de la foret
- doc : **ses sorts viennent du PLACEMENT, pas de l'espece** — la table `0x00552868` a huit rangees, et la rangee est `+0x14`, c'est-a-dire l'octet 6 du releve de placement que la naissance recopie. **Deux exemplaires de la meme bete posees sur la meme carte peuvent n'avoir pas les memes sorts.** Aucune autre routine ecrite ne fait ca
- doc : **elle FUIT** — dans la plage de corps a corps, sa sortie la plus probable (40 %) prend un cap autour de la cible **plus un demi-tour**. Et sa « charge » n'est pas une posture : elle leve `0x1000804` et entre en incantation. Elle n'a ni bond, ni posture 4, son recul ne fait rien du tout, et sa marche est la seule de la famille a ne pas multiplier son pas par sa vitesse d'animation
- feat : **les six verbes de zone de l'auberge**, pris en une passe comme le releve du 2026-08-23 le recommandait : la remise d'un objet a tout le groupe (12), la recherche de l'**Ecu du destin** dans l'inventaire (7), les deux lectures du plancher de niveau (5 et 6), la recharge des sept compteurs de sorts (4) et le compte des neuf drapeaux (2)
- doc : **le mot `0x2efb` que le verbe 7 cherche en `+0x86` a un nom** — `objets.csv` le donne : l'**Ecu du destin**, la meme fiche dans toutes les zones ou elle apparait. Le releve de 2026-08-23 s'arretait au numero
- doc : **quatre verbes rendent leur resultat dans la BANQUE `0x005e9060`, pas dans leur retour** — ils y lisent aussi leur argument. Seul le verbe 4 rend une vraie valeur : 1 si aucun compteur n'avait besoin d'etre remonte, 0 sinon — la forme exacte d'un service d'auberge qu'on ne facture que s'il a servi
- fix : **les verbes 57 et 58 n'etaient pas branches** — `AreaVerb_0053DEA0` et `AreaVerb_0053DF20` existaient depuis le 2026-08-25, avec tout le panneau du coffre (`WorldMapDepot.cs`), mais `ScriptOpAreaVerb` levait sur les deux. Deux lignes
- fix : **`census-entity-handlers.sh` etait aveugle aux deux crochets par image** — `CallEntityHook_0056afe8` et `CallEntityHook_0056aff0` ont leur propre aiguillage, comme `CallSubAreaMoveProbe`. `0x0053DCB0` et `0x0053DF90` se lisaient « a transcrire » alors qu'ils sont ecrits depuis le 2026-08-25
- feat : **`0x00404B90` et son etincelle `0x00404C80`** — le sous-type `0xfe` se colle a l'entite de scene 0 (trois adresses absolues, pas un pointeur qu'on lui passe), seme trois etincelles par image pendant trente images, et joue un **arpege de trois notes** : le meme son `0x20` a trois hauteurs montantes, aux images 30, 22 et 14
- doc : **l'atlas `0x0054f160` est releve** — quatre enregistrements, et les deux bouts se demontrent : il commence ou finit `0x0054f158` et il est clos par la **reapparition du separateur** `00 00 08 08` a `0x0054f180`. Le corps confirme, son `+0x40` rebouclant a 4

---

## 2026-09-15

- feat : **LES TABLES STATIQUES SONT COMPLETES — 274 sur 274** : les treize derniers gestionnaires communs sont ecrits d'un bloc (`0x00403E80`, `0x00404000`, `0x00404460`, `0x004045F0`, `0x00404730`, `0x00447A90`, `0x00447D50`, `0x0044FC90`, `0x0044FF40`, `0x00476950`, `0x00476970`, `0x0047A950`, `0x00482FA0`). Six absents de `Blaze.c`, sortis du desassembleur ; releves gardes sous `Legacy/Raw/Blaze/`. Deuxieme ensemble entierement implemente apres la foret, et le seul qui vaille dans les onze zones
- feat : **la fratrie 80-85 de `0x00551c30` est bouclee** — les six effets de sort portes par leur victime, dont les deux qui manquaient : `0x51` (deux sons dates par le compteur d'images, une entite `0x33` une image sur deux) et `0x53`, **la seule de la fratrie qui se joue en deux temps** — elle seme du `0x35` pendant 6 a 10 images, puis lache onze `0x36` d'un coup a la dixieme image de son attente
- feat : **`0x00447A90`, un sort de pretre qui rampe au sol en spirale** et frappe a chaque image, avec les onze traineaux `0xa1` qu'il lache a sa naissance. Il recopie la garde de `EndSpellOnTerrain_00448040` dans son corps au lieu de l'appeler, et porte son coup **au nom du lanceur** (`+0x6c`), pas du sort
- feat : **le bloc des cinq enfants de benediction (`0xa5` a `0xa9`) est complet** — `0xa8`, l'anneau dont l'elevation n'est pas un champ mais une fonction du cap (`sin(cap) x +0x12 >> 12`), donc qui ondule ; et `0xa6`, la bulle qui monte en accelerant une image sur deux
- doc : **deux relais rendent le recensement statique aveugle** — `0x00476950` et `0x00476970` lisent le **sous-type de leur enfant dans leur propre `+0x0b`**. Un seul creneau de table de sauts ouvre donc sur n'importe quel gestionnaire de l'autre table, et `GestionnairesEntites.md` ne peut pas dire ce qu'ils atteignent
- doc : **`0x00404000` a un rayon d'orbite fige a zero** — les deux appels aux tables de sinus et de cosinus sont bien la, l'angle avance de `0x64` par image, mais `+0x98` est mis a zero a la naissance et rien ne le rouvre. La bouffee retombe donc exactement sur son ancre. Transcrit tel quel, pas repare
- doc : **`0x00482FA0` est le SOUS-TYPE ZERO, et ce n'est pas une souche** — ce qu'une entite de `0x005f2d40` fait quand personne ne lui a donne de sous-type : un eclat qui perd un quart de sa taille par image et s'efface a la onzieme. Les creneaux vraiment vides, eux, tiennent `0x0047adc0`
- doc : **`0x0047A950` pulse au rythme du compteur de fondu GLOBAL** (`s_FadeCounter_005e9960`), pas d'un champ a elle : toutes les bulles d'etat a l'ecran respirent en phase. Elle ne vit que deux images et est donc reengendree en continu tant que l'etat dure
- doc : **l'atlas `0x0054f138` est releve et borne** — quatre enregistrements jusqu'a `0x0054f158`, la table voisine ; `0x00404730` n'en atteint que trois, son `+0x40` rebouclant a 3. `check-descriptors.sh` le verifie desormais, le nom suivant enfin la convention que le script reconnait

---

## 2026-09-02

- doc : **le TAUX DE DOUBLON est mesure, plus estime** — `OpenForesia.Tools/census-duplicates.sh` desassemble les 1 430 corps, les tronque a leur dernier `ret`, et compare deux normalisations. **24 % des 1 012 restants sont des copies exactes**, et **38 % sur la filiere des IA** — c'est-a-dire la ou le volume est
- doc : **la paire de Grizzlys sert de CONTROLE, et le script refuse de conclure sans elle** — une mesure correcte doit rendre STRICT different et LOOSE identique. La premiere version echouait : sans troncature au dernier `ret`, deux emissions du meme corps ne se ressemblent pas, leurs fenetres traînant un bourrage de longueur differente
- doc : **la foret a deja paye pour 21 routines d'IA d'autres zones** — le Varan ecrit ce matin avait un jumeau exact dans la grotte scellee (`0x004F14F0`), le Wyrm en couvre quatre, Willie quatre, le Goblin-sorcier trois. L'argument le plus net pour finir une zone avant d'en ouvrir une autre : le travail se rembourse ailleurs a l'insu de celui qui le fait
- doc : **il reste 32 IA qui sont des copies d'un frere non ecrit** — les ecrire par representant et non par zone economise 32 corps ; les six meilleurs groupes font 20 corps pour 6 lectures
- doc : **la charge restante est chiffree : ~44 seances** — 159 004 instructions nettes de doublon, au rythme observe de 5 128 instructions par seance sur les onze seances du 22 aout au 2 septembre. L'estimation a vue du meme jour donnait 40 : la mesure ne l'a pas deplacee, elle a supprime l'incertitude — **l'economie de 29 % est annulee par un effet de composition**, le reste etant deux fois plus dense en IA que ce qui est fait
- feat : **`0x004A0480`, la FLEUR DU MAL — ET LA FORET EST COMPLETE** : 3 936 octets absents de `Blaze.c`, la derniere des 136. Zone 3 passe a **136 des 136**, et c'est la **premiere zone entierement implementee du depot** (la zone 0 en est a 261 sur 274)
- doc : **la prediction de la veille etait bonne, et elle a coute une commande** — le releve du Varan disait « si elle n'appelle pas `FUN_00488160` ni `FUN_004E3770`, les cinq feuilles ecrites aujourd'hui la couvrent deja ». C'est exactement cela : rien n'a eu besoin d'etre ecrit pour elle
- doc : **la charpente du Varan est une VRAIE FAMILLE, pas un cas isole** — deux membres, memes feuilles propres, et **exactement la meme sonde batie sur la pile**, `{0, 0x1e, 0x32, 0x64, 0, 0x64}`, mot pour mot
- fix : **le bloc de heurt des murs est desormais partage** — les deux emissions ne different que par le masque de contact (`0x2000ad82` contre `0x2000af82`, un bit), qui devient un parametre : `SettleSceneEntityOnGroundAndWalls`
- doc : **son geste seme des BRAISES** — la posture `0x0b` fait naitre une image sur trois le sous-type `0xEF`, qui se resout en `0x004101D0`, la braise qui derive, deja ecrite le 2026-08-27. Une fleur qui crache des etincelles
- doc : **le descripteur d'atlas qu'elle passe est NUL, et ce n'est pas un oubli** — la braise s'en donne un elle-meme a sa premiere image. Il a suffi de relire le consommateur plutot que de traiter le zero comme une donnee manquante
- doc : **elle ne choisit pas sa proie, elle la tire au sort** — meme chercheur que le Varan mais avec le mode 0, qui n'a pas de `case` : seule la piece a 60 % qui bascule sur « n'importe qui de vivant » agit. Le meme chercheur sert donc a deux intentions opposees
- doc : **ni sort, ni prise, ni poison** — douze cases de table de sauts dont **cinq vides**, et un geste qui porte a pleine attaque la ou toute la famille ampute le bloc de modele
- doc : **sa posture 7 n'est joignable que par un mur**, et sa bande du milieu porte a la place une branche qui NE FAIT RIEN — le second temps mort trouve le meme jour, apres celui du Wyrm, dans une routine sans rapport
- doc : **ses deux dernieres bandes ne se recouvrent PAS** — celle du milieu s'arrete a `0x258`, ou commence celle du lointain. Deux routines de la meme charpente, une seule avec le defaut : le recouvrement du Varan est donc une singularite de son corps, pas un trait de famille
- doc : **le comptage de lignes ne descend pas quand on ecrit la vraie sœur** — annoncee a 1 262 lignes de la Fourmi geante, elle est encore a 1 164 du Varan une fois celui-ci ecrit. L'ecart mesure la duplication du compilateur, pas la parente ; la liste d'appels repondait en une seconde. Troisieme cas en trois transcriptions
- doc : **la dette qui reste en foret est celle de l'EPREUVE** — 24 des 136 seulement ont ete vus tourner en jeu. Premier lieu ou une seance de test se lit comme une mesure et non comme une chasse au plantage
- feat : **`0x004A13E0`, le WYRM — cinquieme bete volante, et la seule des cinq a porter DEUX gestes** : 2 416 octets absents de `Blaze.c`. Zone 3 passe a **135 des 136**, et il ne reste que la Fleur du mal
- fix : **le crible s'etait trompe de sœur, et la liste d'appels l'a corrige en une commande** — annonce a 583 lignes du Crabe (une terrestre), le Wyrm appelle `FUN_0049A8E0` et `FUN_004A1CF0` : c'est une VOLANTE, sœur de Willie. Recompte apres coup : 584 lignes de Willie contre 594 du Crabe, une egalite que le comptage ne peut pas trancher. La lecon du Varan a servi le jour meme
- doc : **sa charge est le geste du Geant greffe sur le corps du Trent** — deux phases dans `+0x10`, une seule image d'elan a `(hardiesse + 0x500) << 7`, puis un balayage a chaque image tant que la vitesse a plat depasse 3 ; et la victime part a 30 unites vers la cible, a `0x18000` de haut, l'effet naissant a SA position
- doc : **et cette charge ne blesse qu'UNE fois** — le bit `0x20` se pose au premier contact et barre tout second balayage jusqu'a la fermeture du geste. Les deux bits tombent ensemble
- doc : **il frappe SOUS lui** — la sonde du coup (`0x005529b0`) porte le decalage vertical `-0x100` de la Coccinelle, celle de la charge (`0x005529bc`) est plate et porte deux fois plus loin
- doc : **ses quatre postures amortissent leur poussee d'un SEIZIEME** au lieu de la poser ou de l'effacer d'un coup — aucune terrestre ne fait cela. Et il garde l'animation 0 pour se tourner, la ou toute la famille passe a 1 : il ne marche pas, il derive
- doc : **une fois sur cinq sa replanification a moyenne distance ne change RIEN** — un tirage de `0x50` a `0x63` rend la main sans toucher ni la posture ni le rebours, donc le replanificateur retire a l'image suivante. Une image perdue, pas une branche oubliee
- doc : **son seuil d'engagement est toujours au-dessus de sa plage** — `0x12c - hasard % (timidite * 2 + 1)` alors que la plage s'arrete a `0xfa` : un Wyrm dont la timidite est sous 25 mord sans condition de distance
- doc : **la suite `0x00552978` → `0x005529f4` est CLOSE** — les deux sondes du Wyrm occupaient les vingt-quatre octets restants entre les sorts du Trent et le sort du Varan. Neuf tables consecutives, chacune bornee par sa voisine des deux cotes
- doc : **le premier geste devant une IA inconnue est desormais `grep -o "call .*"`** — `FUN_0049A8E0` + `FUN_004A1CF0` veut dire volante, `FUN_00488160` + `FUN_004DFAF0` terrestre, aucun des deux une charpente propre. Deux corps qui n'appellent pas les memes feuilles ne sont pas comparables ligne a ligne
- feat : **`0x004A1D50`, le VARAN — la premiere intelligence batie sur une AUTRE charpente** : 3 904 octets absents de `Blaze.c`, et la seule des quatorze qui n'appelle AUCUNE des quatre feuilles partagees. Zone 3 passe a **134 des 136**
- feat : cinq feuilles neuves avec elle — `FUN_004A1020` (le chercheur de proie), `FUN_004D5DE0` (l'annulation de geste par les traits), `FUN_004D5E60` (de quel cote passer), `FUN_004C3810` (l'assommement) et `FUN_004A2BF0` + `FUN_004A2C30` (l'effet de piege filtre)
- doc : **il CHOISIT sa proie, et par classe** — l'octet `+0x41` sert de drapeau de naissance, et la premiere image appelle `FUN_004A1020` en mode 5, celui qui cherche la classe 3. Aucune des treize autres ne touche a `+0x6c`. Mais le mode n'est respecte que **deux fois sur dix** : le chercheur tire deux pieces avant tout choix
- doc : **il CONTOURNE les murs, le premier du depot** — sur contact `0x2000ad82` toute l'image est annulee (les onze champs de placement remis), puis `FUN_004D5E60` plante deux sondes reelles, un quart de tour de part et d'autre, et dit quel cote est libre. Pile ou face quand les deux se valent
- doc : **son geste n'interrompt pas sa posture** — `flags & 2` n'ouvre qu'un prelude, et la case de `+0x42` tourne la meme image. Treize cases dans la table de sauts au lieu de quatre, dont la posture 5 qui ne fait rien et pose le bit `0x10`
- doc : **le cone qui decide n'est jamais le pas qui tourne** — 0x100 juge et `hardiesse * 2 + 0x80` tourne, 0xc8 juge et `hardiesse + 0x32` tourne. Le corps ne rattrape jamais son cap d'un coup, ce qu'aucune de ses sœurs ne fait
- doc : **troisieme lanceur de sort de la foret, avec UNE seule fiche** — `0x005529c8`, famille 0 indice 19, et c'est exactement celle que le Behemoth et le Trent ont en commun. Le mecanisme d'incantation, lui, n'est pas partage : pas de bit `0x800`, pas de seconde machine a postures
- doc : **sa morsure empoisonne une fois sur quinze** — `FUN_004A2BF0`, donc un effet de piege applique par une bete. Aucune des treize routines precedentes n'y touche ; c'etait le chemin des coffres et des pieges de sol
- doc : **sa posture 9 met une VITESSE ANGULAIRE dans `+0x10`** — `(hasard % 10 - 5) * 10` ajoute au cap a chaque image pendant qu'il fonce : une course en arc de cercle. Partout ailleurs ce champ porte un cap absolu ou une distance
- doc : **sa PRISE est inatteignable** — la posture 8 arme une charge dont le prelude empoigne, divise l'attaque du modele par quatre et projette la victime, mais rien n'y pose le bit de geste qui le garde. Defaut de 1998, transcrit tel quel et signale sur place
- doc : **ses deux dernieres plages de replanification se CHEVAUCHENT** — de 0x258 a 0x2bb le corps choisit une posture, puis en rechoisit une qui l'ecrase. Transcrit en trois `if` successifs pour que le recouvrement reste lisible
- doc : **la limite du crible du doublon est trouvee** — il mesure des lignes d'assembleur, et un corps qui INLINE ses feuilles en a mecaniquement beaucoup plus. L'ecart de 1 171 lignes n'annoncait pas une bete exotique : il additionnait un choix de compilation et un vrai changement de charpente. La correction tient en une commande, comparer les listes d'appels avant de compter les lignes

## 2026-09-01
- feat : **`0x0049FAD0`, le TRENT — la SECONDE bete du depot qui lance un sort** — 2437 octets absents de `Blaze.c`. Zone 3 passe a **133 des 136**
- doc : **le mecanisme d'incantation n'appartient pas au Behemoth** — le Trent le porte au complet : meme bit `0x800`, meme seconde machine a postures dans `+0x10`, meme entite `0xbd`, meme duree lue dans la fiche de sort (`+0x13`), meme masque de traits qui l'annule. Deux constantes les separent. Premier morceau de comportement de cette taille dont on constate qu'il est REUTILISE
- doc : **les deux tables de sorts se recouvrent** — Trent 3/13/19, Behemoth 13/19/23, tous famille 0. Deux sorts sur trois en commun, alors qu'ils sont dans deux blocs differents
- doc : **sa charge ne frappe qu'en MOUVEMENT** — le geste mesure sa propre vitesse a plat, n'ouvre le balayage qu'au-dessus de 6, et ne se ferme que quand elle retombe sous `hardiesse/8 + 1`. Son impulsion `(hardiesse + 0x360) << 7` est la plus forte poussee horizontale de la famille
- doc : **elle saute, et la HARDIESSE decide de la frequence** — `hasard % 100 < hardiesse / 8` : un Trent hardi bondit huit fois sur cent, un Trent timide jamais. Premiere routine ou une statistique decide de la frequence d'un mouvement et non de son amplitude
- fix : **la fin de `s_SpellGestures_00552978` est ETABLIE** — le releve du 2026-08-29 s'arretait sur un indice (« au-dela on lit des triplets `0x14 / 0x3c / 0x64` »), sans preuve. Ces trois nombres sont les termes `[1]`/`[2]`/`[3]` du gabarit du Trent a `0x00552990` : la table tient en douze mots exactement, et elle est bornee des deux cotes
- feat : **`0x004A35B0`, WILLIE — la quatrieme bete volante** — 1636 octets absents de `Blaze.c`, la sœur de la Coccinelle que le crible annoncait a 417 lignes. Zone 3 passe a **132 des 136**
- doc : **elle ENFLE pour frapper** — les trois echelles `+0x28`/`+0x2a`/`+0x2c` convergent vers `0x3000`, TROIS FOIS sa taille, des que le rebours du geste passe sous 8. Aucune des dix-huit routines precedentes ne touche a son echelle pour se battre
- doc : **sa sonde est centree sur son propre corps** — `probe[1] = -0x3c`, le seul decalage avant NEGATIF du depot : la boite de frappe l'englobe au lieu de se poser devant. Le gabarit d'une chose qui blesse par contact, et il va avec l'enflure et le balayage a chaque image
- doc : **elle pulse, et tout bat sur la meme phase** — taille, couleur (`+0x38`, un gris de `0x20` a `0xe0`) et altitude de vol partagent l'accumulateur `+0x14`. Premier corps du depot ou trois canaux visuels sortent d'un seul compteur
- doc : **elle ne change JAMAIS d'animation** — aucune de ses quatre postures n'ecrit `+0x18`, ni son engagement ; toutes ses sœurs y posent 0 ou 1. Et **son interruption est conditionnelle** : `+0x16` au-dessus de `0x0a` lui donne l'interruption terrestre, en dessous la volante
- fix : **`BeatWings_0049A68F` scinde en deux** — le masque `0xdfffd8ff` d'un cote, le vol (`HoverOnWings`) de l'autre : Willie emet le vol mais n'efface les drapeaux de vue que dans une de ses deux branches d'interruption
- feat : **`0x004A2C90`, le GEANT** — 2308 octets absents de `Blaze.c`, la premiere routine choisie par le crible plutot que par la taille, et **le crible avait raison** : 283 lignes du Grizzly annoncees, squelette du Grizzly trouve. Zone 3 passe a **131 des 136**
- doc : **elle arme un elan qui ne part pas toujours** — `+0x43` vaut 2 si la cible etait a `0x96` ou plus a l'engagement, 1 sinon, et les deux gestes ne s'elancent QUE sur 2. Un Geant qui engage de trop pres joue son animation sans le bond ; la distance est mesuree une fois et ne se relit jamais
- doc : **ses DEUX gestes quittent le sol**, et tant qu'elle est en l'air l'image d'animation du coup de patte est **bloquee a `0x11`** : le geste se fige sur sa derniere image et n'en sort qu'a l'atterrissage. C'est ce qui remplace le compte a rebours des autres
- doc : **sa replanification a un SECOND ESSAI** — si le cone du coup de patte echoue, elle retente le bond sur un cone trois fois plus large avant d'hesiter. Aucune des quinze routines precedentes ne fait cela
- doc : **le cout d'une transcription a ete estime AVANT de la commencer, et l'estimation a tenu** — la regle « ce qui predit le cout, c'est le nombre de formes distinctes » a maintenant un instrument
- doc : **les six IA qui restent en zone 3 passees au crible du doublon, avant d'en ecrire une seule** — aucun doublon, six corps reels. Mais le crible donne le **plus proche voisin deja ecrit** de chacune, ce qui rend mesurable la recette « calquer sur une sœur » de `Comportement.md`
- doc : **le crible change l'ordre de travail** — par la taille il fallait commencer par le Willie (1664 o), par l'ecart il faut commencer par le **Geant** (`0x004A2C90`, 2336 o mais 283 lignes du Grizzly). Le cout se mesure au nombre de formes distinctes, pas au nombre d'octets — et on le sait maintenant AVANT de commencer
- feat : **`0x004A54E0`, le BEHEMOTH — et le BLOC C EST FERME** : les sous-zones 5 et 9 de la foret sont entierement couvertes, IA comprises. 2571 octets absents de `Blaze.c`. Zone 3 passe a **130 des 136**, et les six qui restent sont toutes du bloc B
- doc : **la seule bete du depot qui ait DEUX machines a postures** — le bit `0x800` choisit : degage, quatre postures dans `+0x42` ; pose, quatre etats d'incantation dans `+0x10`. Ce que le desassembleur montrait comme une table de huit entrees est deux tables de quatre, et c'est le controle de borne qui le dit
- doc : **LE PREMIER SORT LANCE PAR UNE BETE** — elle en tire un sur trois dans `0x00552A30` (famille 0, indices 13/19/23), et **va lire la duree de l'incantation dans la fiche de sort elle-meme** (`+0x13`). Le temps d'incantation est une propriete du SORT, pas de la bete
- doc : **le premier bloc du repertoire de zone est un catalogue d'effets adresse par rang** — le sillage du Goblin lit `[1]`, le souffle du Mechant ours `[9]`, l'onde de choc du Behemoth `[2]`. Trois routines, trois rangs, la meme table par sous-zone
- doc : **sa gravite est `0x7fff`, le maximum d'un short signe** — le Grizzly a `0x6000`, le Mechant ours `0x7000`. La bete la plus lourde du jeu, et le binaire le dit en une constante
- doc : **ses cones sont des CONSTANTES et son rythme ignore la timidite** — la seule de la famille qui ne calcule jamais `0x40 - hardiesse`. Et **plus on s'eloigne d'elle, plus elle a de moyens** : deux choix au corps a corps, quatre au loin — l'inverse de toutes ses voisines
- feat : **`0x004A4AE0`, le MECHANT OURS** — 2542 octets absents de `Blaze.c`, le corps le plus riche de la famille des intelligences d'entite de scene. Zone 3 passe a **129 des 136**
- doc : **TROIS gestes la ou le Crabe et le Grizzly en ont deux** — la griffe, la PRISE (elle empoigne, inflige la moitie de l'attaque, projette la victime a 40x et la fait decoller), et le SOUFFLE, la seule attaque a distance de la famille
- doc : **le souffle vise une table PAR SOUS-ZONE** — son sous-type est `0xc0` + un octet du repertoire de zone, donc `>= 0xc0`, donc la table `0x0056adc8` et non la statique. Premiere routine d'IA dont l'effet depend de la carte ; les objets, eux, n'en emploient aucun
- doc : **elle secoue l'ecran quand elle marche** — la seule des quatorze qui touche a la camera : `-8` dans `DAT_005de0ac` a deux images precises, puis le meme amortissement `a <- (a/4) - a` que l'explosion — mais **avec un plancher**, la ou l'explosion laisse un decalage residuel pour toujours
- doc : **le sens de la projection se lit sur l'attaque du MODELE, pas sur la geometrie** — `-0x400` au-dessus de `0x800`, `+0x400` sinon. Transcrit tel quel, sans lui preter d'intention
- doc : **le crible du doublon passe AVANT la lecture** — ecart minimal 533 lignes sur les treize IA deja ecrites. La lecon de `0x004A4270` a servi des la fonction suivante, et coute une minute
- feat : **`0x004A4270`, le Grizzly du bloc C — le meme que celui du bloc A, emis deux fois** : 705 lignes d'assembleur identiques sur 705, a deux immediates pres. Aucune transcription, un renvoi d'une ligne. Zone 3 passe a **128 des 136**
- doc : **la regle « chercher un doublon avant de transcrire » s'enonce mieux** — quand deux corps ne different que par des immediates qui sont des ADRESSES, la comparaison n'est pas finie : il faut comparer ce qu'elles designent. Ici les deux gabarits de portee (`0x00552960` et `0x005529e8`) portent les memes 24 octets, et un `od` du seul code aurait conclu « fonction differente »
- fix : **`0x004A3C30` est la TROISIEME bete volante, pas la seconde** — l'Abeille `0x0049B220` partage deja la queue de la Coccinelle. Corrige dans le code, `HISTORY.md` et `Comportement.md`
- feat : **`0x004A3C30`, la TROISIEME BETE VOLANTE de la foret** — 1583 octets absents de `Blaze.c`, la premiere des dix intelligences d'ennemi qui restaient. Zone 3 passe a **127 des 136**
- doc : **sa posture 3 est une RUEE, pas une errance** — une impulsion d'une seule image, `(timidite + 0x120) << 8` versee d'un coup dans `+0x50`/`+0x58` puis remise a zero ; l'image suivante attend que la vitesse retombe sous `hardiesse/8 + 1`. Aucune des cinq intelligences precedentes ne fait cela
- doc : **elle recule en vous regardant** — la posture 3 finit par `+0x32 += cap relatif` a chaque image, et la ruee du corps a corps part a `+0x800` du cap vise. Le meme mecanisme sert au bond en avant et au bond en arriere, seul le biais change
- doc : **son geste finit quand l'ANIMATION finit** — le bit `0x2000000` d'`AdvanceEntityAnimation_004DB5A0`, et non un compte a rebours ; et il n'a qu'une image de frappe, la seizieme, la ou la Coccinelle balaie a chaque image
- doc : **le battement d'ailes est identique au mot pres a celui de la Coccinelle** — `BeatWings_0049A68F` est partage tel quel, sans parametre de differenciation : les deux desassemblages coincident sur tout le bloc
- doc : **une inversion de seuil qui n'est pas une erreur de lecture** — plus cette bete est timide, PLUS LOIN elle accepte d'engager ; la Coccinelle fait l'inverse. Meme place, meme operande, sens oppose
- feat : **`0x00541B80`, le crochet de contact au sol des sous-zones 5 et 9** — 259 octets absents de `Blaze.c`, le corps le moins cher du depot. Zone 3 passe a **126 des 136**, et toute la plomberie de la foret (terrain, embarcation, sol) est desormais ecrite : les dix qui restent sont dix comportements d'ennemi
- fix : **le depot appelait `0x00541B80` « le jumeau du pas de collision » — c'est faux** — ce n'est pas le creneau 63 des sept tables mais le creneau **32**, la meme case que `0x00541440`. Sa signature le disait deja : un seul argument, la ou le creneau 63 en recoit trois. Une adresse voisine n'est pas une preuve de role
- doc : **`0x00541550` est le seul creneau 63 non vide de la foret** — les autres blocs du lieu, celui des sous-zones 5 et 9 compris, y laissent la souche vide. Le pas de collision equipe l'embarcation, pas la zone : `CallSubAreaMoveProbe` ne levera jamais en foret
- doc : **`0x00541440` et `0x00541B80` ne different que par trois instructions** — le `test dword [esi],50000000h`, son saut, et le `je` qui passe de court a long : exactement les douze octets d'ecart. Corps ecrit une fois, deux enveloppes d'une ligne
- doc : **en sous-zones 5 et 9 le sol brulant frappe meme une entite retiree ou morte** — l'effet `0x2A` nait et le mini-script part quand meme, la ou le reste de la foret coupe court. Transcrit tel quel
- feat : **`0x0049E920`, la bete qui EMPOIGNE** — 1932 octets absents de `Blaze.c`, la quatrieme intelligence d'entite de scene de la foret. C'etait **la seule ligne `implemente=non, teste=oui` de la zone 3** : le jeu y allait vraiment et plantait. Zone 3 passe a **125 des 136**, et plus aucune ligne « ca plante » n'y subsiste
- doc : **on s'en debarrasse en secouant la manette** — la bete se colle a sa victime et la frappe a chaque image ; les quatre bits `0xf0` de l'octet `+2` de la manette de la VICTIME font descendre un compteur, et a zero la bete est projetee a l'oppose de son cap. Le compteur vaut `hasard % 8 + hardiesse / 4 + 8` : une bete hardie tient plus longtemps
- doc : **premier mecanisme du depot ou l'entree du joueur agit sur le comportement d'un ennemi** — partout ailleurs la manette pilote le personnage ; ici elle decide de la duree d'une prise
- doc : **la plus agressive de la famille** — six fois sur dix au corps a corps contre trois pour ses sœurs, une sonde a decalage vertical (la seule), et l'echec du cone la fait avancer au lieu de la figer
- fix : **la ligne de sonde de `0x0049E920` retiree de `handlers-exerces.tsv`** — ce « teste » ne prouvait que le plantage, la sonde notant l'adresse avant d'appeler. Le gestionnaire est maintenant `implemente=oui, teste=non` : une dette honnete plutot qu'un acquis usurpe
- feat : **`0x00541550`, le pas sur le maillage du terrain** — 1584 octets absents de `Blaze.c`, la piece que l'embarcation attendait. Zone 3 passe a **124 des 136**. Il ne deplace rien : il dit sur quelles faces l'entite se retrouve, quel plancher et quel plafond elle a, et ce qui l'a arretee
- doc : **une troisieme table sur le morceau de terrain, `morceau + 0x30`** — la liste d'adjacence par sommet, une suite de `short` terminee par un negatif. C'est elle qui rend la traversee d'arete possible sans balayer toutes les faces
- doc : **le bobinage des faces identifie la face d'en face** — la voisine est celle dont une arete est le couple de sommets INVERSE, compare comme une simple egalite de dwords. Aucune recherche geometrique
- doc : **le masque de traversee et le test « qui est a bord » sont le meme bit `0x2000`** — les deux bouts du mecanisme, ecrits sans se connaitre, se recoupent dessus. C'est le meilleur controle qu'on ait sur la lecture des deux corps
- fix : **`census-entity-handlers.sh` ne voyait pas `CallSubAreaMoveProbe`** — le creneau 63 de `0x0056afc8` n'est pas appele comme un gestionnaire d'entite (trois arguments, pas un), donc son aiguillage vit ailleurs que dans le repartiteur. Sans cette ligne le corps restait marque « a transcrire » alors qu'il est branche
- feat : **l'embarcation de la foret**, `0x00458200` — 2088 octets absents de `Blaze.c`, le plus gros corps d'entite de scene transcrit a ce jour, plus son balayage prive `0x00458a30` (943 octets). Zone 3 passe de 122 a **123 des 136** gestionnaires
- doc : **le groupe la pilote a la pagaie** — quatre gachettes en couples opposes (rotation, poussee), un sillage de deux entites par image, et elle ne bouge que si tout le monde est a bord. Plus il y a de rameurs, moins chaque coup porte mais plus le plafond de vitesse est haut : 8/4 pour un seul, 2/1 pour quatre
- doc : **`+0x10` n'est pas un etat mais un renvoi vers la fiche de placement de la sous-zone** — la premiere image y range `(+0x14 << 8) | +0x16`, puis les deux champs changent de metier. Chaque image y reecrit position et cap : l'embarcation est encore la au retour
- doc : **`PTR_FUN_0056b0c4` est l'entree 63 de la table `0x0056afc8`**, pas une globale isolee — la table installee par sous-zone fait 0x40 entrees. Ce creneau-la est appele avec **trois** arguments la ou les sept tables en portent partout ailleurs a un seul : il est reserve au pas de collision, `0x00541550` en foret
- fix : **le bloc prive de la foret s'arretait un mot trop tot** — `s_ForestSceneBlock_00571420` allouait `0x240`, et le nouveau corps ecrit un `int` a `0x00571660`, exactement le premier octet dehors. Porte a `0x248`. Meme classe de defaut que les tables de descripteurs trop courtes, et aussi invisible a la compilation
- feat : **les objets, 21 sur 21 — la liste est close** : `0x00447630`, `0x00404920` et `0x00446A90`, les trois derniers, sortis du désassembleur. `ObjetsAtteignables.md` ne porte plus une seule ligne `implemente = non`, et les **1 440 emplois** sont couverts
- feat : **deux enfants de plus**, décompilés : `0x00446C90` (l'écaille `0x9b`) et `0x00404A50` (la braise `0x37`). Comme chaque fois, ils auraient planté à l'image suivant leur parent
- doc : **`0x00447630` REPOUSSE sa victime** — `(300 - moyenne des dimensions) / 8` sur le cap du frappeur, posé dans les vitesses de la victime avec le bit `1` : une grosse bête est moins repoussée qu'une petite, et au-delà de 900 unités elle ne bouge plus
- doc : **`0x00446A90` est `0x00446F30` tiré au sort** — mêmes trois mesures du modèle du porteur, mais chacune passée par un tirage, pour que les cinq filles ne se superposent pas
- doc : **les trois enregistrements orphelins de `0x005515e0` ont leur consommateur** : `0x00446C90` pose `&DAT_00551618` et tire `hasard % 3`. Trois relevés, trois consommés — la longueur se vérifie des deux côtés
- feat : **une table de descripteurs de plus**, `0x0054f098` (4 enregistrements), celle de la braise ; bornée par le corps (`+0x40` retenu à 3) et par le binaire (le cinquième change de bande)
- doc : **trois traces de 1998 transcrites telles quelles** — une branche morte dans `0x00446A90` (le son à `0xf4e` ne part jamais, `+0x98` ne vaut jamais zéro au test), trois divisions non gardées par une mesure du modèle, et le calcul mort-né de `0x00447630` écrasé par `0xaa`
- feat : **les objets, 14 gestionnaires sur 17** — `ObjetsAtteignables.md` passe de 4 à **18 des 21** gestionnaires atteignables par un script d'objet, soit **1 412 des 1 440 emplois** couverts. Dix corps décompilés, quatre sortis du désassembleur
- feat : **cinq petits-enfants** que le recensement des objets ne montrait pas : `0x00446E20` (la bulle `0xa6`), `0x00446060` (l'étincelle `0x9c`), `0x00476410` (l'anneau `0x6e`), `0x00447050` (la spirale `0x9a`), `0x00446620` (la sphère `0xa7`). Chacun aurait planté à l'image suivant son parent
- feat : **deux tables de descripteurs relevées**, `0x005515e0` (10 enregistrements) et `0x00551630` (3) — elles comblent le trou entre `0x005515c0` et `0x00551648`, qui se suivent maintenant sans interstice
- doc : **`0x00446f30` MESURE son porteur** et passe la taille à sa fille par la recopie des 0x27 mots : `(largeur + profondeur) / 4` d'un côté, `(l + h + p) / 3` plafonné à 600 puis × 50 de l'autre. Un gros monstre a un gros effet. Un membre du groupe n'est pas mesuré — le bit `0x800000` impose deux constantes
- doc : **`0x00446620` intervertit sa taille de départ et sa taille d'arrivée à la naissance**, ce qui permet à sa mère de décrire quatre sphères différentes avec les mêmes champs
- fix : **les étoiles de Marteau sortaient gigantesques** — `TickHammerDebris_0044A320` fait tourner son image d'atlas sur **trois**, et la table `0x00551528` s'arrêtait à la première : deux images sur trois lisaient huit octets de tas jamais écrits. Table portée de 16 à 18 enregistrements (`0x00551528`-`0x005515b7`)
- feat : **`OpenForesia.Tools/check-descriptors.sh`** — le contrôle des tables d'atlas, en deux passes : le contenu de chaque tableau relu dans `Blaze.exe`, et la marge de chaque site qui en installe une. Trois fois le même défaut (`0x00551b00`, `0x00551528`) suffisent à l'outiller
- doc : les 35 tables sont **conformes au binaire au dernier octet** ; ce n'était jamais le contenu qui était faux, toujours la longueur. Les cinq marges courtes signalées par l'outil ont été vérifiées à la main et sont bonnes
- feat : **les deux aures d'état et leurs enfants** : `0x004761D0` (le sommeil, sous-type `0x47`) et sa bulle `0x00476290`, plus le jumeau `0x004760C0` (l'état `0x80`, sous-type `0x46`). Endormir un ennemi avec la magie du Lutin ne plante plus
- fix : **`0x00476140` était rangé sous le mauvais sort**. Il était écrit et nommé `TickHerbSeed` — sous-type `0x2d`, herboristerie — alors que c'est le sous-type `0x2e`, l'enfant de la gerbe d'état `0x004760C0` : un décalage d'un cran dans la table `0x00552230`. Le seul engendreur de `0x2e` dans tout le binaire est `0x004760C0`, et le corps le disait déjà — il meurt sur le bit `0x80`, celui de l'effet 12, qu'une herbe ne pose jamais. Renommé `TickStatusMote_00476140`
- doc : **une aure d'état se pose sur la VICTIME**, pas sur le lanceur, et les deux se distinguent par leur durée : celle de l'état `0x80` sème quatre enfants en croix puis s'éteint dans l'image, celle du sommeil vit et en lâche un toutes les huit images tant que la victime dort
- feat : **les trois dernières postures**, `0x0051A6B0` (4), `0x0051A700` (6) et `0x0051A710` (7) — **absentes de `Blaze.c`**, sorties du désassembleur, 208 octets à elles trois. La table `0x00562ca0` est maintenant entièrement écrite : quatre corps vides et quatre gestionnaires
- fix : **jouer le Lutin plantait à l'entrée en forêt** — il vole, la posture 7, et son gestionnaire n'était pas transcrit
- doc : **une posture peut lancer un sort**. Les postures 4 et 7 appellent `CastSpell_0053A980` — familles **5** et **6**, indice 0 — à une image précise de l'état d'animation 3, le moment où le pied touche ou l'aile bat. Deux des quatre familles hors école ont donc un déclencheur qui n'est ni une incantation, ni un objet, ni une entité de scène
- doc : la posture 4 vise **deux images différentes** selon le bit 8 de `+0x150` du bloc par personnage (0x10 éteint, 0x0c allumé) : deux cycles de marche de longueurs différentes, le même effet posé au bon moment dans chacun
- fix : **frapper une entité plantait encore**, deux instructions plus loin — `DrawIconGlyph_0051BE90` levait `NotImplementedException` alors que `FUN_0051be90` était **déjà transcrite** sous le nom `DrawDecimalNumber`, appelée quatre lignes plus bas dans le même fichier. Le stub mort supprimé, ses quatre sites branchés sur la vraie fonction
- doc : **`FUN_0051be90` existait en trois exemplaires** dans la reconstruction — le stub qui plantait, `Primitives2d.DrawDecimalNumber` et `SaveSlots.EmitCardGlyphTile`. Les deux corps vivants sont identiques ligne à ligne ; la fusion reste à faire. Avant d'écrire un corps, chercher son VA dans **tout le dépôt**, pas seulement dans `Blaze.c`
- fix : **frapper une entité plantait le jeu** (`0xC0000005` dans `DrawTextRun`) — le bandeau nom/`HP`/points de vie passait la chaîne `0x00564a14` en **littéral d'adresse**, recopié tel quel de la pseudo-C où Ghidra ne l'a pas nommée. Relevée dans le binaire (`"HP"`) et posée en symbole `s_HpLabel_00564a14`
- doc : **un littéral d'adresse dans `Blaze.c` n'est jamais un pointeur**. Balayage de tout le dépôt : c'était le seul, mais le chemin n'avait jamais été exercé — il faut porter un coup pour poser `DAT_005de0b4`. Les huit chaînes voisines de `0x00564a00` sont relevées dans `DataSymbols.md`
- fix : **Boule de feu naissait sans boule**, entourée d'éclats parasites convergents — la table de descripteurs d'atlas `0x00551b00` était transcrite sur cinq enregistrements, et le sort en désigne un **sixième** (`push 551B28h`) : la coque `0x51` recevait huit octets de tas jamais écrits comme descripteur. Table portée à ses huit enregistrements réels, `0x00551b00`-`0x00551b3f`
- doc : **la borne DROITE d'un atlas ne se lit pas plus dans la donnée que la gauche**. Un changement de planche est une hypothèse de fin, pas une preuve ; une plage de `.data` transcrite trop court ne se voit pas à la compilation, elle se voit à l'écran en géométrie parasite. Réflexe à prendre : comparer la longueur du tableau au plus grand décalage que le code lui applique
- feat : **les trois derniers trous de la magie** : la colonne `0x0044f600` et le grain `0x0044f750` de **Pardon**, l'étincelle `0x0044ef40` d'**Ange**. La fermeture des engendrements est maintenant complète côté sorts — plus un seul corps atteignable depuis une fiche ne manque
- doc : **c'étaient des PETITS-ENFANTS, et c'est le point aveugle du recensement**. `SortsAtteignables.md` résout le sous-type `+0x10` d'une fiche — son **enfant direct** — et s'arrête là. Pardon et Ange s'y lisaient « implémenté » alors que les lancer plantait dès que le trait ou la plume semait. Trouvés en dépliant à la main tous les engendrements du code écrit, et l'avertissement est maintenant écrit en tête du document généré
- doc : **la colonne de Pardon est posée au SOL sous le trait** — elle relit sa position à chaque image mais prend son `+0x60` et non son `+0x20` —, sa largeur vient de la stature de la victime, sa hauteur monte d'un huitième de ce qui la sépare de `0x7fff`, et son compte à rebours `+0x48` retourne sa teinte : dix de plus par image tant qu'il est positif, dix de moins ensuite
- doc : **le grain de Pardon écrit dans la table de descripteurs, et sans retour** : sa première image pose `2` dans l'octet `+4` du cinquième enregistrement de `0x00551528`, qui vaut `1` au chargement. Rien ne le remet jamais à `1` — un verrou à sens unique sur une table **partagée**, que d'autres entités atteignent par leur `+0x9a`
- doc : **l'étincelle d'Ange grandit en s'éteignant**, et sa durée tient en quatre battements de un ou deux images : le même tirage sert de longueur de battement et de recharge, si bien qu'elle vit **quatre ou huit images, jamais entre les deux**
- fix : la note de Pardon annonçait trois sous-types « aucun décompilé » ; `0x0044f750` l'était, et `0x93` était déjà venu avec une autre fiche. Corrigée, et la note d'Ange gagne son `0x95`
- feat : **les deux sorts de monstre, familles 4 et 6** : le sort `0x004477d0` et son trait `0x004478f0`, le sort `0x004769a0` et sa boule `0x004769f0` — **trois des quatre absents de `Blaze.c`**, sortis du désassembleur. **Plus une seule fiche de sort du CD n'est sans gestionnaire** : les 80 des écoles et les 43 des familles hors école sont toutes couvertes, et les 74 gestionnaires de sort distincts existent
- doc : **sur les 43 fiches des familles 4 à 7, quatre seulement désignent un vrai corps** — les 39 autres tombent sur `DespawnEntity_0047adc0`, le bouche-trou. Des quatre, la famille 5 emploie `0x0047a100` et la famille 7 le corps de **Météores**, tous deux déjà écrits. Et le détail qui compte : les sous-types sont `0x49`, `0x4a`, `0x4b` — **trois créneaux consécutifs sur la première fiche des familles 5, 6 et 4**. Trois emplacements voisins réservés à trois sorts de monstre ; **l'étendue réelle de ces familles reste à établir**, le reste des 43 étant vraisemblablement du sur-relevé
- doc : **le trait de la famille 4 retrouve son lanceur par la voie longue** — son `+0x6c` est le sort, et c'est le `+0x6c` du sort qui porte le lanceur. Ça se recoupe avec `CastSpell_0053a980`, qui pose `+0x74 = soi` sur le lanceur le temps de l'engendrement : **une entité de sort porte donc le lanceur des deux côtés**, en `+0x6c` et en `+0x74`
- doc : **la boule de la famille 6 est le premier appelant connu du huitième descripteur de `0x00551ba0`**. La note portée sur la plage disait que huit étaient relevés « parce que huit sont lisibles, pas parce qu'une borne les désigne » ; le huitième a maintenant un emploi
- doc : **LB-029 — le trait appelle le balayage des entités de scène avec trois arguments là où il en lit quatre**. Le quatrième décide si la parade compte ; il tombe sur le mot où le prologue a sauvé `edi`, donc sur une valeur quelconque. Les six autres sites d'appel du dépôt en passent bien quatre. **Divergence déclarée** : ce port passe `0`, la valeur de six sites sur sept et celle qui respecte la parade
- feat : **l'herboristerie, école 3 — les sept plantes** : le sort `0x0047a5d0` et ses trois enfants (le halo `0x0047a7d0`, le grain `0x0047a730`, la graine `0x00476140`), plus les **sept effets** `0x0053ace0` à `0x0053af60` et leur répartiteur. **Les 80 fiches des quatre écoles ont maintenant un gestionnaire ; il n'en reste aucune à transcrire.** L'école est injouable — ces onze corps ne tournent dans aucune partie
- doc : **`0x0056d4c0` n'est pas le début de la table des 48 effets, c'est une AUTRE table** — sept pointeurs, un dword **nul**, puis seulement `0x0056d4e0` et les effets d'objet. Les sept sont l'herboristerie, aucun autre appelant du binaire ne les atteint, et elles n'écrivent pas au même endroit : les effets d'objet appareillent par `FUN_00538870` et travaillent sur le bloc de combat, les plantes se contentent de `FUN_0053a860` et écrivent **directement dans la fiche de personnage**
- doc : **la fiche de personnage et le bloc de combat sont la même mémoire à `0x100` octets d'écart** — `FUN_005387c0` rend `fiche + 0x100`, et ça se vérifie champ par champ : PV en `+0x4c`/`+0x14c`, mot d'état en `+0x40`/`+0x140`. C'est ce qui permet de reconnaître dans chaque plante la jumelle exacte d'un effet d'objet
- fix : `Sorts.md` disait le corps des plantes « paramétré par l'octet `+0x12` ». **C'est `+0x06`** — l'indice de la plante dans son école, que `SpawnEffectEntity` recopie depuis le lanceur — et il ne sert qu'à deux choses : choisir l'effet dans `0x0056d4c0` et l'atlas du halo dans le répertoire de ressources. Le tableau des sept effets est ajouté au passage
- doc : **deux plantes lèvent leur état sans effacer sa durée** — Hysopius (bit `0x2`) et Belladone (bit `0x10`) —, là où leurs jumelles d'objet (entrées 36 et 39) font les deux. Ce n'est pas une lecture approximative : leurs corps font 70 octets contre 79 pour les trois autres levées, et les neuf octets d'écart sont exactement le `mov word [eax+…],0` qui leur manque. Une réapplication prend le maximum des deux durées, donc un état réappliqué après elles peut durer ce que durait le précédent. Sans conséquence en jeu, l'école étant injouable
- doc : **`FUN_00476140` avait deux paramètres perdus par Ghidra** (`unaff_EBX`, `unaff_retaddr`) : le désassembleur tranche, **les deux sont la même valeur** — le rayon `(short)+0x12 >> 2`, calculé une fois et rangé en `[esp+0x18]` avant les deux `push` qui décalent le cadre
- feat : **Terre, école 2 indice 19** : le sort `0x00479c50` (392 octets, **absent de `Blaze.c`**, sorti du désassembleur) et le script de sons différés `0x00551940`. **L'école 2 est finie — et avec elle les trois écoles jouables : les 73 fiches ont toutes un gestionnaire.** Ne restent que les sept de l'herboristerie du Chasseur, inatteignables en jeu normal, qui partagent `0x0047a5d0`
- doc : **le quatrième et dernier jumeau** — encore les 392 octets de Liquide, à six déplacements d'appel près qui diffèrent tous exactement du décalage des deux fonctions (`0x320`). **Les quatre fiches partagent un unique corps, recopié quatre fois par le compilateur**, et à elles quatre elles n'auront apporté que **deux** corps neufs : l'élémentaire `0x00479490` et la braise `0x00479bc0`. Terre n'en demandait aucun — sa motte `0x57` est la braise de Flamme
- doc : **le débordement du script de Terre est maintenant reproduit**, contrairement à celui de Liquide. Il est borné : les cinq pas de Terre sont suivis sans trou par les deux de Zéphé, qui eux se terminent. Les sept sont donc déclarés d'un bloc, et `DelayedSoundScript00551968` est une **vue à `+0x28`** de ce même bloc — la façon de faire de `Descriptors00551BC0`. Terre joue bien ses deux sons de trop, comme en 1998 ; LB-027 mis à jour, seul Liquide reste une divergence déclarée
- doc : **Terre est le seul des quatre à naître déjà teinté** (`+0x38 = 0x40000000` là où les trois autres partent de zéro), et le test d'éclaircissement de l'élémentaire masque justement ce poids fort. Sa branche sème une motte **une image sur quatre**, au ras du sol et dispersée en X et Z seulement : la seule des quatre à ne rien disperser en hauteur
- feat : **Zéphé, école 2 indice 18** : le sort `0x00479de0` (392 octets, **absent de `Blaze.c`**, sorti du désassembleur) et le script de sons différés `0x00551968`, deux pas — **le plus court des quatre élémentaires**
- doc : **le troisième jumeau du même corps** — comme Flamme, les 392 octets sont ceux de Liquide à six déplacements d'appel près, et **les six diffèrent exactement du décalage des deux fonctions** (`0x190`), donc désignent les mêmes cibles ; ne reste qu'un mot, le pointeur de script. Trois des quatre élémentaires partagent ainsi un corps que le compilateur a recopié trois fois
- doc : **c'est le seul des quatre qui n'ait demandé aucun corps neuf**, et pour une raison qui traverse deux écoles : la bourrasque `0x50` qu'engendre sa branche de l'élémentaire est `0x00482ef0`, **la traînée de Boule de feu** — déjà écrite pour l'école 0. Le corps convient parce qu'il ne lit que ce que le semeur lui met : un rayon en `+0x12`, un cap et un tangage en `+0x32`/`+0x34`, une teinte en `+0x38`, tous tirés au sort à chaque bourrasque
- doc : **et cette teinte-là part de zéro**, contrairement au `0x01000000` de la braise : le même test non masqué qui rend l'éclaircissement de la braise inatteignable (LB-028) fonctionne ici sans problème. Le défaut est dans ce que les semeurs écrivent, pas dans le test
- fix : la note de Flamme disait « cinq déplacements d'appel » là où le corps en compte six
- feat : **Flamme, école 2 indice 17** : le sort `0x00479300` (392 octets) et **la braise `0x00479bc0`** (131 octets, sous-type `0x57` de `0x00552230`) — **tous deux absents de `Blaze.c`**, sortis du désassembleur ; plus le script de sons différés `0x005518f8`, neuf pas
- doc : **le sort est le jumeau exact de Liquide, octet pour octet** — les 392 octets des deux corps sont identiques à cinq déplacements d'appel près, qui résolvent tous vers les mêmes cibles, et à **un seul mot** : le pointeur de script de sons empilé, `0x005518f8` contre `0x00551978`. Deux `od` et une soustraction l'établissent ; le compilateur a émis deux fois le même corps, et les deux adresses sont transcrites séparément pour garder leur traçabilité
- doc : **il ne manquait donc que la braise** — l'élémentaire `0x00479490` se branche seul sur l'élément par son `+0x06`, et sa branche « Flamme » était déjà écrite. Elle sème une braise **une image sur deux**, deux fois plus dense que la colonne d'eau de Liquide, dispersée de `±0x100` unités sur les **trois** axes : la seule des quatre à le faire en hauteur aussi
- doc : **la braise est aspirée par l'élémentaire qui l'a semée** — un **quart** de l'écart par image sur les trois axes, une convergence géométrique qui la fait lécher l'élémentaire sans l'atteindre. Et **elle meurt de trop près** : le pas vertical qu'elle vient de faire est comparé à `0x400` **en signe**, donc une braise née au-dessus de son parent ne vit qu'une image — la moitié d'entre elles. Ce qui monte est ce qui reste
- doc : **LB-028 — la braise ne peut jamais s'éclaircir** : elle ajoute `0x202020` à `+0x38` tant que le mot est sous `0xe0e0e0`, mais elle compare le mot **entier** là où l'élémentaire, pour le même champ, masque l'octet de poids fort. Les deux seuls semeurs la font naître à `0x01000000`, déjà au-dessus de la borne : l'addition est du code mort pour tous ses appelants. Reproduit tel quel
- doc : **LB-027 corrigé — le script sans terminateur n'est pas celui « des quatre élémentaires »** : les quatre sorts empilent quatre scripts différents, relevés un par un. Flamme (`0x005518f8`) et Zéphé (`0x00551968`) se terminent proprement ; Terre (`0x00551940`) déborde mais tombe sur le script de Zéphé et s'arrête au bout de deux sons de trop. **Seul Liquide sort de la zone des scripts** et marche dans `.data`
- feat : **Liquide, école 2 indice 16** : le sort `0x00479f70` (392 octets), l'élémentaire `0x00479490` (**1 766 octets**, le plus gros corps de l'école) et la colonne d'eau `0x00479b90` (34 octets, le plus court) — **tous trois absents de `Blaze.c`**, sortis du désassembleur ; plus le script de sons différés `0x00551978`
- doc : **les quatre dernières fiches de l'école partagent UN SEUL corps d'enfant**, et c'est une fratrie d'une autre espèce que les Matéria — celles-là étaient quatre copies d'un même corps ; ici il n'y en a qu'un, et c'est lui qui se branche : `0x00479490` lit son `+0x06` (l'**indice** du sort dans son école, `0x10` à `0x13`) et saute dans une table de quatre entrées relevée à `0x00479b78`. Liquide prend l'entrée 0, Flamme la 1, Zéphé la 2, Terre la 3
- doc : **l'élémentaire est le premier vrai familier du dépôt** — il grossit sur place, **prévient son père** quand il est prêt (il pose le bit `0x2` sur l'enregistrement du père, qui n'a aucun autre moyen de le savoir), puis chasse : il cherche un adversaire, le retient par `+0x74`, vole vers lui à coups de poussée de huit unités amortie d'un huitième, le frappe au contact par `ResolveSpellHit` avec la famille et l'indice de son propre sort, et le lâche au bout de quarante images. Sans proie il tourne autour du lanceur, à la phase que le sort lui a donnée
- doc : **le sort ne fait que payer** — trois élémentaires à la première image, puis **deux PM toutes les cinq images**, la cadence la plus chère de l'école, quatre fois celle de Champ magique. Son âge reste à 1 : rien ne l'incrémente, donc le repli des cent-vingt images prévu pour un lanceur hors groupe est inatteignable
- doc : **LB-027 — le script de sons `0x00551978` n'a pas de terminateur** : aucun de ses cinq enregistrements ne porte un octet de délai nul, et `TickDelayedSounds` continue donc dans la table de descripteurs qui suit, qu'il lit comme « seize sons d'un coup, puis 192 images d'attente ». **Ce port ne reproduit pas la marche** — les deux tables sont ici deux allocations distinctes ; divergence déclarée
- feat : **Blindé, école 2 indice 15** : le sort `0x004790c0` et sa coque `0x00479250`, **tous deux décompilés dans `Blaze.c`**
- doc : **c'est le seul sort de l'école qui se lance sur lui-même** — toutes les autres fiches cherchent une cible, adversaire ou allié ; celle-ci passe le lanceur **des deux côtés** à l'effet 25, assaillant et victime. Et **si l'effet ne passe pas, le sort n'a pas lieu** : le corps rend la main sur-le-champ, sans rien engendrer ni même une image de décor. C'est le seul de l'école qui puisse échouer complètement
- doc : **sa durée lui est rendue par l'effet lui-même** — l'effet 25 range `attaque / 8 + 0x19` dans le `+0xb2` du bloc de statistiques, et le sort relit ce même `+0xb2` juste après pour s'en faire un compte de charges, décrémenté toutes les vingt images sur **l'horloge du monde** et non sur son propre âge. Les deux bouts se rejoignent : ce que l'effet écrit est ce qui fait vivre le sort
- doc : **la teinte de la coque est un compte à rebours DANS LES DEUX SENS**, et c'est le seul corps du dépôt à le faire — elle monte de `0x101010` par image jusqu'à `0xf0f0f0` tant que le père tient (la coque apparaît en fondu), puis redescend du même pas dès que le père lève son `0x2000000`, et s'efface sur un zéro exact. La flamme des Matéria, la marque d'Écrase et le grain de Superman n'en font que la moitié
- doc : **la coque relit le bit du père à chaque image**, pas une seule fois — c'est ce qui lui permet de survivre au sort et de s'éteindre proprement après lui. Quatre coques naissent, deux à l'image 0 et deux à l'image 4, à hauteur de tête et à vingt unités du sol, le cap tournant d'un demi-tour entre chacune
- feat : **Superman, école 2 indice 14** : le sort `0x00478df0` (décompilé), son anneau `0x00479030` (décompilé) et son grain de poussière `0x00478fc0` (97 octets, **absent de `Blaze.c`**, sorti du désassembleur) — **trois corps**, après cinq fiches d'affilée qui n'en demandaient aucun
- doc : **c'est Vitesse en plus grand et en plus long** — sa première image est celle de Vitesse mot pour mot, aux deux mêmes anneaux et au même couple de tailles, à deux écarts près : la division est par **100** et non par 150 (les anneaux sont une fois et demie plus larges) et le sous-type engendré est `0x82`. Puis il ne meurt pas : il vit **vingt-quatre images** et sème à chacune un grain de poussière dans un cube d'une stature de côté autour des pieds du porteur — X et Z centrés, Y jamais décalé, donc toujours au-dessus du sol
- doc : **l'anneau `0x82` est celui de Vitesse à deux différences près**, et le diff des deux désassemblages ne montre rien d'autre : l'effet 26 au lieu du 19, et **pas de script de sons différés** — la gamme montante que l'anneau de Vitesse met en file en se fermant n'existe pas ici, il se ferme en silence
- doc : **le sort relit la stature** — `StatusBubbleHeight` est appelée une seconde fois sur la même cible, pour en tirer la taille des grains, celle-là divisée par 75. Les deux appels rendent la même chose ; le second n'est qu'une relecture, transcrite telle quelle
- doc : **le grain ne compte pas ses images non plus** — troisième corps de l'école à mourir sur sa teinte (`0xf0f0f0` moins `0x101010`, zéro exact, quinze images), après la flamme des Matéria et la marque d'Écrase. Il rétrécit d'un quart par image en largeur et en profondeur seulement : `+0x2a` n'est touché ni par lui ni par le sort
- feat : **Turbo, école 2 indice 13** : le sort `0x00478d10` (décompilé) — aucun enfant à écrire, le **cinquième d'affilée**. Les deux anneaux `0x81` sont ceux de Vitesse
- doc : **c'est Vitesse dans une boucle** — le même corps mot pour mot (deux anneaux, division par 150, tailles `(base, 1,5x)` puis `(1,5x, 0,75x)`, `+0x16` qui distingue lequel pose l'effet, son `0x0d` à `0x2d4`), enveloppé dans un `do…while` qui le rejoue tant qu'il trouve un allié de plus. **Vitesse hâte un compagnon, Turbo tout le groupe, en une seule image**
- doc : **et c'est le marqueur `+0x74` qui fait avancer la boucle** — la recherche employée n'est pas celle de Vitesse mais `FindNearestOpponent_00474EB0` en mode 0, qui descend dans `FindNearestPartyMember_00474CD0`, laquelle **ignore tout membre dont `+0x74` est non nul**. En posant `allié+0x74 = soi` sur chaque allié servi, le sort le retire de la recherche suivante ; quand il ne reste personne la recherche rend `null`, le corps retombe sur le lanceur et la condition `trouvé != lanceur` ferme la boucle
- doc : **les marqueurs ne sont jamais effacés, et ce n'est pas un oubli** — le pas par image d'un membre du groupe oublie sa cible `+0x74` dès que l'emplacement visé cesse de porter `0x80000000`. Turbo met son mot d'état à zéro en sortant de la boucle : tous les alliés marqués se libèrent d'eux-mêmes à leur prochain pas. Le marquage ne dure que le temps de l'image où la boucle tourne
- feat : **Ralenti, école 2 indice 12** : le sort `0x00478c20` (décompilé) — aucun enfant à écrire, le **quatrième d'affilée** dans ce cas
- doc : **c'est Gros sabots, répété** — elle engendre le même sabot `0x7e` (`TickHeavyHoofStomp_004770A0`) et lui remplit les mêmes champs, mais **toutes les huit images** au lieu d'une fois, et **rien ne la borne** : elle ne s'arrête que si le balayage ne trouve plus personne en face, ou si le lanceur porte `0x40000000`
- doc : **trois écarts avec Gros sabots**, tous lus au désassembleur : le sabot naît depuis le **sol** (`+0x60`) et non depuis le `+0x20` du sort ; la taille visée est divisée par **100** (`0x51EB851F` puis `sar 5`) et non par 75, donc ses sabots sont plus petits d'un quart ; et `+0x14` reçoit `0x80` au lieu de zéro
- doc : **une conséquence des deux corps, notée sans avoir été observée en jeu** — le sabot hérite le bit `0x2000000` de l'enregistrement de son père et ne s'en sert que pour mourir après une résistance. Gros sabots pose ce bit juste après avoir engendré ; Ralenti ne le pose qu'à la fin du sort. Un sabot de Ralenti dont la victime résiste attend donc cette fin-là pour disparaître
- feat : **Escargot, école 2 indice 11** : le sort `0x004786c0` (1 364 octets, vingt-sept cibles de saut, **absent de `Blaze.c`**, sorti du désassembleur) — aucun enfant à écrire, le troisième d'affilée dans ce cas
- doc : **c'est la jumelle de Champ magique**, et le diff des deux désassemblages donne les trois seules différences : le son de la première image (`0x31` à `0x195` au lieu de `0x0d` à `0x800`), **un second son à la douzième image exactement** (`0x16` à `0x653`, sur un test d'égalité) que Champ magique n'a pas, et la classe d'entités chassée
- doc : **et c'est cette dernière qui compte** — `(état & 0xe80) == 0x680` au lieu de `(état & 0x880) == 0x880` : Escargot exige `0x400` et `0x200` et **refuse** `0x800`, que Champ magique exige au contraire. Les deux sentinelles ne guettent pas la même chose, et c'est tout ce qui les sépare en jeu
- doc : **elle est recompilée, pas recopiée** — `ecx` et `edx` y jouent des rôles échangés par rapport à sa jumelle, le même signe que la boule d'Écrase porte vis-à-vis de la bulle de Dodo. Elle hérite aussi de LB-026 : la visée porte sur le dernier candidat balayé, pas sur le plus proche
- feat : **Coussin d'air, école 2 indice 10** : le sort `0x004785f0` (205 octets, un seul saut interne, **absent de `Blaze.c`**, sorti du désassembleur) — le plus court corps de l'école, et le **second d'affilée sans enfant à écrire** : sa seule créature est l'étincelle `0xbf`, déjà transcrite
- doc : **c'est un souffle sous les pieds d'un allié** — il se pose par `FindTargetAround` en mode 0, se donne une gravité `0x4000` que les étincelles héritent, et sème **une étincelle par image** à la position du porteur pendant vingt images : seize unités de vitesse horizontale dans une direction tirée au sort, et une poussée verticale fixe de `0x1e000`. À la vingtième il pose l'effet 28 et s'efface
- doc : **l'effet 28 n'a ni masque d'immunité ni jet de sauvegarde**, comme celui de Vitesse — c'est un bienfait, il ne se refuse pas. Sa durée va en `+0xb8`, son bit est `0x400000`
- doc : `+0x12` reçoit la stature du porteur à la première image et **rien ne la relit** — ni le sort, ni l'étincelle. Transcrit tel quel
- feat : **Champ magique, école 2 indice 9** : le sort `0x004780b0` (1 333 octets, vingt-six cibles de saut, **absent de `Blaze.c`**, sorti du désassembleur) — le plus gros corps de l'école, et **le premier sans enfant à lui** : ses deux créatures sont déjà écrites, l'éclaboussure `0xbf` et le halo `0x84` (`TickExorcismHalo_0047F290`)
- doc : **ce n'est pas un sort qui frappe, c'est une sentinelle** — il tourne autour d'un allié et balaie **les trois tables d'entités** à chaque image (`0x005ff400`, `0x005edee0`, `0x005f2d40`), retient ce qui porte les marques `0x880` sans `0x40000000`, vise le plus proche, tourne d'un quart de l'écart par image, et quand il est à portée (les trois quarts de la stature du porteur) et bien pointé (±`0x200` dans les deux axes) il lâche quatre étincelles dessus et lui pose `0x40000000`. Un pare-projectiles
- doc : **il est entretenu comme Ange, à la cadence près** — passé la seizième image le lanceur est relâché et le sort coûte **quatre PM toutes les vingt images** tant que le bouton reste tenu (le mot `+0x1fc4` de la fiche croisé avec l'état de la manette). Ange prélevait deux PM toutes les dix. Un lanceur hors groupe tient cent-vingt images
- doc : **et il ne s'efface pas d'un coup** — le bit `0x2000000` ne le tue pas, il le fait rétrécir d'un quart par image, et c'est en passant sous `0x100` qu'il disparaît. Tant qu'il rétrécit il se place et émet encore, mais ne cherche plus rien
- doc : **LB-026 — Champ magique vise le dernier candidat balayé, pas le plus proche** : le balayage range les trois écarts de position **avant** la comparaison de distance, et la branche « celui-ci est meilleur » ne garde que le pointeur et la distance. Il tire bien sur la bonne cible, mais s'oriente vers la mauvaise — et comme les deux conditions d'angle portent sur cette orientation, le **moment** du tir en dépend
- feat : **Écrase, école 2 indice 8** : le sort `0x00477d20` (décompilé), sa boule `0x00477e40` (618 octets, **absente de `Blaze.c`**, sortie du désassembleur) et sa marque `0x00477da0` (décompilée) — **trois corps pour une fiche**, une première dans l'école
- doc : **le sort d'Écrase est celui de Dodo, octet pour octet** — le désassemblage des deux corps ne diffère qu'en **deux** points : le sous-type engendré (`0x55` au lieu de `0x53`) et l'atlas passé (`0x00551a78` au lieu de `0x00551b60`). Rien d'autre. C'est le troisième mode de réemploi que l'école 2 montre, et le plus radical : le même code émis deux fois entre deux sorts qui n'ont rien à voir
- doc : **la boule, elle, est recompilée et non recopiée** — même charpente que la bulle de Dodo, mais `edi` et `ebp` y jouent des rôles échangés, ce qu'une copie n'aurait pas produit. Trois écarts : l'éclaboussure prête `+0x1a = 4` au lieu de `1`, l'effet est le 10 au lieu du 11, et surtout, quand l'effet passe, **elle engendre**
- doc : **la marque `0x7c` est la PREMIÈRE PETITE-FILLE de l'école 2** — jusqu'ici aucun enfant de sort n'avait engendré à son tour. Elle part par `SpawnCastEntity_0047AAE0`, donc dans l'**autre** table d'entités, celle que la passe raccroche à la grille
- doc : **la marque rétrécit d'un seizième par image et vit quinze images** — le pas de retrait est figé à la première image (`taille / 16`) et ne se recalcule pas ; sa teinte part de `0xf0f0f0` et perd `0x101010` par image, comme la flamme des Matéria. Les deux bornes se répondent : elle finit à un seizième de sa taille de départ au moment où elle disparaît
- feat : **Chant sirènes, école 2 indice 7** : le sort `0x00477a20` (décompilé, dix lignes) et sa note `0x00477a90` (656 octets, **absente de `Blaze.c`**, sortie du désassembleur) ; plus le script de sons différés `0x005518d8` — une gamme montante de quatre notes espacées de trois images
- doc : **c'est Dodo réécrit, pas Dodo recopié** — la note et la bulle de Dodo ont la même charpente instruction par instruction sur les deux tiers de leur longueur (même boîte de détection `{0, -60, 60, 100, 0, 120}`, même saisie du lanceur, même tête chercheuse à zone morte de ±`0x40`, même dix unités par image, même ondulation, même anneau d'images, même bruit toutes les quatorze images, même mort à la quarantième) — mais **ce ne sont pas les mêmes octets**, et le binaire porte les deux corps. Après la fratrie des Matéria, c'est la seconde forme de réemploi de l'école 2, et elle est d'une autre nature
- doc : **la bulle semait, la note respire** — là où Dodo lâchait une éclaboussure par image, la note ne lâche rien et fait battre sa propre taille : `0x1800` plus la moitié d'un sinus de son âge pris sur **neuf** bits, donc deux fois plus vite que l'ondulation qui en prend huit
- doc : **sur la victime elle lâche six éclats au lieu d'un halo** — chacun avec sa propre vitesse à deux angles, et l'ordre des tirages est celui du binaire : le **premier** appel au générateur donne le tangage, le second le lacet. L'écrire dans l'ordre des arguments C# inverserait deux tirages du même flux
- doc : **l'atlas `0x00551bc0` est le cinquième enregistrement de la plage `0x00551ba0`**, pas une table à lui — et c'est celui qui n'a pas les octets `01 02` de ses voisins, la particularité que la note portée sur la plage signalait sans savoir qui s'en servait
- feat : **Matéria eau, école 2 indice 6 — et la FRATRIE DES QUATRE MATÉRIA EST CLOSE** : le sort `0x004778e0` (décompilé), son `0x18` à la hauteur `0x384`, effet 6 (`+0x8c` / `+0xea`). L'école 2 passe à **7 fiches sur 20**
- doc : **les quatre copies sont relevées, et il n'y a pas de quatrième différence** — ce que le diff établissait pour terre puis pour air vaut pour la fratrie entière : un seul comportement et quatre tables de trois constantes (le son de la première image, sa hauteur, l'effet de la dernière)
- doc : **ni l'un ni l'autre des deux enfants n'est propre à un élément** — les quatre Matéria lâchent le même embrasement `0x4c` et la même flamme `0x56`, avec les mêmes atlas. À l'écran elles sont **identiques** ; seuls le son de départ et l'effet rangé dans le bloc de statistiques changent
- feat : **Matéria air, école 2 indice 5** : le sort `0x004777a0` (décompilé) — la troisième de la fratrie, et la parenté est de nouveau vérifiée au désassembleur : hors adresses de saut, trois constantes d'écart avec feu et rien d'autre. Son `9` à la hauteur `0x100` (la plus grave des trois), effet 5 (`+0x8a` / `+0xe8`)
- doc : **les quatre enchantements élémentaires occupent quatre paires consécutives** — `+0x86`/`+0xe4`, `+0x88`/`+0xe6`, `+0x8a`/`+0xe8` pour feu, terre et air : ils se cumulent tous, aucun ne remplace l'autre
- feat : **Matéria terre, école 2 indice 4** : le sort `0x00477660` (décompilé) — la deuxième de la fratrie. Aucun corps nouveau à écrire : elle réemploie l'embrasement `0x4c` et la flamme `0x56` de Matéria feu
- doc : **la fratrie diffère par TROIS constantes, pas deux** — le désassemblage de feu et de terre, adresses de saut mises à part, ne diffère qu'en trois points : l'identifiant du son de la première image (`0x20` / `4`), **sa hauteur** (`0x400` / `0x190`), et l'effet posé à la dernière (3 / 4). Le compte annoncé hier était incomplet, il est corrigé dans l'en-tête de la fratrie
- doc : **les effets 3 et 4 rangent leur durée et leur intensité à des emplacements voisins mais distincts** — `+0x86`/`+0xe4` pour feu, `+0x88`/`+0xe6` pour terre : les deux enchantements élémentaires se cumulent au lieu de se remplacer
- feat : **Matéria feu, école 2 indice 3** : le sort `0x004774c0` (décompilé) et sa flamme `0x00477600` (96 octets, **absente de `Blaze.c`**, sortie du désassembleur) ; son embrasement `0x4c` est celui de Feu d'enfer, réemployé tel quel. Plus le script de sons différés `0x005518a8` relevé dans le binaire
- doc : **Matéria feu est la première d'une fratrie de quatre** — `0x00477660` (terre), `0x004777a0` (air) et `0x004778e0` (eau) ont le même corps instruction pour instruction, et ne diffèrent que par deux constantes : le son de la première image et l'indice de l'effet posé à la dernière (3, 4, 5, 6, les quatre effets « à intensité »). Ce sont quatre enchantements élémentaires, pas quatre sorts
- doc : **la flamme ne compte pas ses images, c'est sa teinte qui la tue** — partie de `0xf0f0f0`, elle perd `0x101010` par image et le corps s'efface quand la soustraction tombe **pile sur zéro** : le `jne` final lit les indicateurs de cette addition-là, pas un compteur. Quinze images, donc, et rien d'autre ne la borne
- doc : **le script `0x005518a8` est le premier du dépôt dont un pas joue trois sons d'un coup** — son `[2]` vaut 3, et `TickDelayedSounds` avance alors de trois enregistrements : six enregistrements pour quatre pas
- fix : **la division magique `0x1B4E81B5` avec `sar 3` vaut 75, pas 150** — c'est `sar 4` qui vaut 150, ce que la décompilation Ghidra de Matéria feu et de Vitesse confirme indépendamment. Le halo de Dodo et la taille visée du sabot de Gros sabots divisaient tous deux par 150 alors que leur relevé porte `sar 3` : les deux naissaient à la moitié de leur taille
- feat : **Vitesse, école 2 indice 2** : le sort `0x00477360` et son anneau `0x00477420`, **tous deux décompilés dans `Blaze.c`** — les deux premiers corps de l'école 2 à l'être ; plus le script de sons différés `0x00551888` relevé dans le binaire — quatre notes `0x0c` de plus en plus aiguës, espacées de deux images
- doc : **c'est le premier sort bienfaisant de l'école 2** — `FindTargetAround_00474BA0` en mode 0 fait balayer au lanceur **son propre camp**, et faute de voisin elle rend le lanceur lui-même : Vitesse ne rate jamais, elle se lance sur soi. L'effet 19 le confirme, c'est le seul de la table des quarante-huit à n'avoir **ni masque d'immunité ni jet de sauvegarde**
- doc : **il naît DEUX anneaux, pas un** — la même image, tailles réglées sur la stature de la cible : le premier haut et étroit (largeur x, hauteur 1,5x), le second large et plat (largeur 1,5x, hauteur 0,75x). Seul le mot `+0x16` les distingue, et **seul le second pose l'effet** ; le premier n'est que du décor
- doc : **l'anneau se colle aux PIEDS de sa cible** — sa hauteur vient de `cible+0x60`, la cote du sol sous elle, pas de son `+0x20`. Il tourne de `âge * 8` par image, donc de plus en plus vite, et sa teinte est un sinus de son âge posé sur les trois canaux. À l'âge `0x20` il se ferme
- feat : **Gros sabots, école 2 indice 1** : le sort `0x00476fd0` (199 octets) et son sabot `0x004770a0` (702 octets), **tous deux absents de `Blaze.c`** et sortis du désassembleur ; plus le script de sons différés `0x00551878` relevé dans le binaire — deux pas, le choc puis le rebond cinq images plus tard
- doc : **la deuxième fiche de l'école 2 confirme la première** — le sort ne vit qu'une image et tout est dans son enfant. Mais là où Dodo lâchait un projectile qui cherche sa cible, Gros sabots la choisit **lui-même** par `FindNearestOpponent_00474EB0`, puis fait naître le sabot **trois cents unités au-dessus de la tête** de cette victime, à l'aplomb exact de ses X/Z
- doc : **le sabot n'a aucune vitesse propre** : le sort lui pose une gravité par entité (`+0x9a = 0x6000`) et `IntegrateWithDrag_00535880` fait tout le reste. Il grossit en tombant d'un quart de l'écart par image, vers une taille réglée sur la stature de la victime — `(hauteur << 12) / 150`, la même division magique `0x1B4E81B5` que le halo de Dodo
- doc : **à l'impact il APLATIT sa victime** — échelles `0x1800 / 0 / 0x1800`, étalée au sol et sans épaisseur. C'est le seul corps transcrit qui écrive les échelles d'une **autre** entité ; il la tient d'ailleurs dès sa première image, `victime+0x74` pointant sur lui. Six éclaboussures partent en étoile, chacune avec sa propre vitesse écrite sur le sabot avant sa naissance, et l'écran tremble de 8. Puis il se dandine quarante images avant de rétrécir d'un quart par image, pendant que la victime regagne `0x1000` — il ne la relâche qu'à ce moment-là
- doc : **si elle résiste, rien n'est aplati** : l'effet 20 (`ApplyStatusEffect_0053BDA0`, le masque d'immunité le plus étroit de la table des quarante-huit) rend non nul, le sabot se marque `0x1004000` et meurt à l'image suivante
- feat : **Dodo, la première fiche de l'ÉCOLE 2** : le sort `0x00476c70` (décompilé, dix-huit lignes) et sa bulle `0x00476cf0` (727 octets), **absente de `Blaze.c`** et sortie du désassembleur ; plus l'atlas `0x00551b60` relevé dans le binaire — quatre tuiles `0x10 x 0x10`, exactement là où la note portée sur `s_Descriptors_00551B40` l'annonçait
- doc : **l'école 2 ne ressemble pas à l'école 1, et la première fiche le dit déjà** — les vingt-quatre fiches de l'école 1 vivent trente à soixante images, comptent, engendrent du décor et posent leur effet à une image fixée. Dodo ne vit qu'**une** image : il remplit une douzaine de champs pour son enfant, l'engendre, et met son propre mot d'état à zéro. Tout le sort est dans l'enfant
- doc : **et cet enfant est un projectile à tête chercheuse** — il cherche sa cible seul par `PickTargetInCone_00474F00`, avance de dix unités par image dans l'axe de son cap et tourne de `0x40` vers elle, mais **avec une zone morte** : l'écart replié sur douze bits et décalé de `0x40` doit dépasser `0x80`. Le sens du virage sort d'un `setge`/`dec`/`and 0x80`. Il ondule en plus verticalement, sa hauteur valant `sin(âge << 8) * 32` autour de celle du départ
- doc : **il pose l'effet d'état 11** sur sa victime, et lui prête la paire famille/indice **en un seul mot** (`+0x06` et `+0x07` ensemble) là où les vingt-quatre fiches de l'école 1 les écrivent octet par octet
- feat : **Ange, le cinquante-troisième sort — et l'ÉCOLE 1 EST COMPLÈTE, 24 fiches sur 24** : le sort `0x0044dc60` (614 octets, décompilé), sa silhouette `0x0044ded0` (2 919 octets) et sa plume `0x0044ea60` (1 216 octets), ces deux-là **absentes de `Blaze.c`** et sorties du désassembleur. La silhouette est le plus gros corps de l'école
- doc : **c'est le seul sort du dépôt qui dure tant qu'on tient le bouton** — à partir de la soixantième image il vérifie quatre conditions par image : le bouton d'action remappé du lanceur (`+0x1fc4` de sa fiche, croisé avec l'état vif de sa manette), les PM (**deux toutes les dix images**, prélevés sur `+0x14e`), le mot d'état `+0x140` (aucun des bits `0x3e8`) et la vie. La première qui tombe pose `+0x12` et passe en état 3. Son compteur d'images **boucle** de 30000 à 3000 : aucune autre fiche n'en a besoin, aucune autre ne peut durer
- doc : **quatre silhouettes naissent, une seule agit** — elles se distinguent par le couple `(+0x5c, +0x9a)` posé avant chaque naissance, et `+0x5c` sert de **décalage** au pas de croissance (3 ou 4). Trois se figent dans l'état `0x63` ; seule celle qui monte en bleu jusqu'à `0xff` passe en état 1
- doc : **l'ange cherche six cibles et les distribue à ses six plumes** — il retient jusqu'à six proies dans un tableau de pile, puis un saut à sept branches emballe leurs indices **par paires** dans `+0x44`/`+0x48`/`+0x4c`. Chaque plume lit sa moitié de paire d'après son propre `+0x14`, posé à sa naissance. La plume s'étire de sa queue à sa tête, son échelle verticale valant `(distance << 12) / 140` — multiplication magique `0xEA0EA0EB` puis `sar 7`, le même idiome que le `/150` de l'auréole d'incantation
- doc : **LB-023 — l'éviction d'Ange marque une proie et en démarque une autre** : quand une septième proie évince une retenue, l'index et les deux marquages `+0x74` partent dans deux directions opposées. Les deux moitiés du corps portent la même inversion
- doc : **LB-024 — Ange ne balaie que trois des quatre membres du groupe**, et **LB-025 — sa moitié « groupe » relit ses indices dans la table de scène**. Les deux ne touchent que la moitié empruntée par un lanceur hors groupe, donc vraisemblablement rien en jeu ; la plume, elle, choisit la bonne table
- feat : **Résurection, le cinquante-deuxième sort** : le sort `0x0044cd00` (475 octets), sa colonne `0x0044cf30` (249), son éclat montant `0x0044d030` (315) et son voile `0x0044d170` (507), **tous quatre absents de `Blaze.c`** et sortis du désassembleur ; plus son enveloppe d'effet `0x0044cee0`, décompilée
- doc : **son enveloppe a dû être écrite à part, et la raison est logique** — les cinq fiches précédentes partagent un corps de 58 octets qui commence par `IsEffectPairBlocked_004364B0`, lequel **refuse** une cible portant `0x50002000` (mourante, immobilisée, prise). Une cible morte le porte. Résurection a donc son propre garde, qui ne dit rien de `0x40000000` et refuse au contraire l'entité consommée (`0x10000040`), en recul (`+0x12`) ou clignotante (`+0x96`). Seule fiche de la série à ne pas recopier le patron, et la seule qui avait besoin d'autre chose
- doc : **et c'est le seul appelant du dépôt à passer `1` à `AcquireHolyTarget`** — ce mode efface temporairement `0x40000000` et `0x2000` sur les quatre membres du groupe, lance le balayage, puis repose les deux bits : il rend les **morts visibles** à la recherche de cible, le temps de la faire. Le mode n'existait que pour cette fiche
- doc : **le plus long sort de l'école** — 180 images, trois secondes, contre 60 à 70 pour ses voisines ; et le seul à tenir sa **cible** autant que son lanceur par le bit `0x80`
- doc : **son voile écrête là où `ShiftScreenVeil` enroule** — `0x0044d170` pilote `0x005de024` canal par canal, seize images à `+15` bornées à `0xff` puis seize à `-15` planchées à zéro. L'aide des fondus de transition, elle, laisse déborder le cast en octet. Les deux ne sont pas interchangeables, et c'est le seul corps du dépôt à monter le voile par paliers plutôt qu'à lui poser une couleur d'un bloc
- feat : **Miracle, le cinquante et unième sort** : le sort `0x0044c8a0`, son enveloppe d'effet `0x0044ca80`, sa colonne `0x0044cac0` et son auréole `0x0044cbd0`. **Toute la fiche est dans `Blaze.c`** — la première de la série à ne rien devoir au désassembleur depuis Prière
- doc : **il lève le seul effet qui compte au lieu de poser un bit** — `ClearStatusEffect_0053CA30` ne touche aucun bit du mot d'état : elle remet à zéro la durée `+0xc0` et le **compteur** `+0xec`, parce que l'effet 32 (`ApplyStatusEffect_0053C5E0`) s'empile jusqu'à trois fois au lieu de se marquer d'un bit. Miracle efface donc les trois marques d'un coup
- doc : **cinquième copie de la même enveloppe** — `0x0044b910`, `0x0044bc80`, `0x0044c0d0`, `0x0044c6f0` et `0x0044ca80` : cinq corps de 58 octets identiques instruction pour instruction, aux déplacements de leurs deux `call` près, le premier résolvant vers `0x004364b0` dans les cinq cas. Cinq fiches consécutives, cinq copies
- doc : **la colonne et l'auréole sont deux réglages du même corps** — mêmes trois états, même approche d'un quart, même résorption d'un huitième, tous deux posés sur le plancher `+0x60` du porteur. Trois différences : la colonne fait viser `+0x9a` à ses échelles **horizontales** et fige la verticale à 30000, l'auréole fait l'inverse ; l'auréole **tourne** (cap tiré au sort, `+0x96` par image), ce qui sépare ses trois exemplaires nés dans la même image ; et la colonne rabat sa teinte à `0xa0` par un plafond **non signé** que l'auréole n'a pas, laissant la sienne saturer à `0xff`
- doc : **les deux champs que Miracle repose avant chaque étincelle sont ceux qu'elle lit** — `+0x14 = 0x50` sert de borne au rayon de `EntityHandler_0044FB10` (`tirage % +0x14`) et `+0x9a = 1` devient son `+0x0a` d'affichage. Une étincelle par image, cinquante images durant
- feat : **Remontant, le cinquantième sort** : le sort `0x0044c3a0` (837 octets), **absent de `Blaze.c`** et sorti du désassembleur ; plus son enveloppe d'effet `0x0044c6f0` et son rai `0x0044c730`, tous deux décompilés
- doc : **c'est la grande levée de l'école** — Délivrance enlève l'état 9, Micro l'état 10 ; Remontant appelle `ClearStatusEffect_0053C980`, qui efface les bits `0x37f` du mot d'état et remet **neuf** durées à zéro. 62 PM contre 40, et la seule fiche de l'école à lever plus d'un effet
- doc : **quatrième copie de la même enveloppe** — `0x0044b910`, `0x0044bc80`, `0x0044c0d0` et `0x0044c6f0` : quatre corps de 58 octets identiques instruction pour instruction, aux déplacements de leurs deux `call` près, le premier résolvant vers `0x004364b0` dans les quatre cas. Le patron a été émis une fois par fiche
- doc : **un seul corps neuf sur quatre sous-types engendrés** — il fait naître `0x94`, `0x95`, `0x93` et `0x92`, mais trois étaient déjà écrits : `0x95` est l'anneau de Calgon, `0x93` l'étincelle de Coup de pouce et de Pardon, `0x92` l'éclair de Micro. L'école commence à se replier sur son propre stock
- doc : **son rai se range sur le PLANCHER de la cible, pas sur son corps** — `0x0044c730` prend sa position horizontale de `+0x1c`/`+0x24` du porteur mais sa hauteur de `+0x60`, la cote du sol que `FinalizeEntityPlacement2` dépose sous chaque entité. Seul corps de l'école à lire ce champ, et c'est ce qui range les huit exemplaires au sol autour de la cible
- doc : **et Remontant relâche son lanceur après l'extinction** — le bit `0x80` est retiré *après* `EndSpellAndResetDecor`, et non à une image fixée d'avance comme chez Parole sainte. Le binaire relit `+0x74` pour l'occasion, une fois le sort déjà éteint
- feat : **Coup de pouce, le quarante-neuvième sort** : le sort `0x0044bf00`, son enveloppe d'effet `0x0044c0d0` et sa spirale `0x0044c260`, tous trois décompilés ; plus son étincelle `0x0044c110` (333 octets), **absente de `Blaze.c`** et sortie du désassembleur
- doc : **le premier sort de l'école qui prend tout le groupe** — les quarante-huit précédents visent une entité (celle qu'`AcquireHolyTarget` retient, ou toutes les entités de *scène* pour Zombie) ; celui-ci balaie les quatre emplacements de `0x005e93c0`, la table du **groupe**, et pose l'effet d'état 33 sur chaque membre vivant. Il colle aussi à son **lanceur** (`+0x74`) et non à sa cible, seul de l'école à le faire : `+0x78` ne lui sert que de curseur de balayage
- doc : **troisième copie de la même enveloppe** — `0x0044b910` (Délivrance), `0x0044bc80` (Micro) et `0x0044c0d0` sont trois corps de 58 octets identiques instruction pour instruction, aux déplacements de leurs deux `call` près, le premier des deux résolvant vers `0x004364b0` dans les trois. Seul l'effet appelé change, et celui de Coup de pouce est le seul **poseur** des trois
- doc : **il débouche un gestionnaire que Pardon attendait** — le sous-type `0x93` (`0x0044c110`) est celui que le trait de Pardon engendre par paires depuis sa transcription, sans corps derrière. Il surveille son canal **bleu** `+0x3a` pour mourir, là où ses sœurs surveillent le rouge, et son dégradé `-7/-7/-4` est le seul asymétrique de la famille
- doc : **et le `0x91` de Coup de pouce n'est pas celui de Délivrance** — le pétale `0x0044ba00` est le `0x91` de `0x00551f30`, que `SpawnCastEntity` atteint ; la spirale `0x0044c260` est le `0x91` de `0x00552230`, que `SpawnStatusEffectEntity` atteint. Même numéro, deux tables, rien de commun
- feat : **Micro, le quarante-huitième sort** : le sort `0x0044baf0` (391 octets) et son éclair `0x0044be40` (177 octets), tous deux **absents de `Blaze.c`** et sortis du désassembleur ; plus son enveloppe d'effet `0x0044bc80` et sa bille en orbite `0x0044bcc0`, tous deux décompilés
- doc : **Micro et Délivrance sont deux jumeaux exacts, et le CD le dit avant le binaire** — même coût (40 PM), même palier (63), même portée (36), même longueur de combinaison (onze coups), posés côte à côte dans la table de sorts *et* dans le binaire. Leurs deux enveloppes d'effet font 58 octets et ne diffèrent que par les déplacements de leurs deux `call`, dont le premier résout vers le même `0x004364b0`. La seule vraie différence est ce qu'ils lèvent : l'état 9 (bit `0x10`, `+0x92`) pour Délivrance, l'état 10 (bit `0x20`, `+0x94`) pour Micro. Second couple de ce genre dans l'école, après Lapêche et Prière
- doc : **mais sa mise en scène n'a pas de jumelle** — Micro est la **seule** fiche de l'école à reprendre sa cible à chaque image (`AcquireHolyTarget` sans garde) et la seule à mettre `EndSpellOnTerrain` **sous** condition. Les deux choix sont exactement inverses de la norme de l'école
- doc : **il se téléporte à l'origine pour engendrer son éclair** — aux images 0 et 4 il pose `+0x1c = +0x20 = +0x24 = 0xdac` avant `SpawnCastEntity` et ne les repose pas. Sans conséquence visible : l'éclair recalcule sa position depuis `+0x78` dès sa première image, et la visée passe par `FindTargetAround_00474BA0`, qui balaie autour du **lanceur** et non autour du sort
- doc : **la bille orbite autour de deux centres à la fois** — sa position est calculée autour de l'instantané pris à la naissance, puis ramenée d'un quart vers la même orbite prise autour du porteur vivant. L'instantané n'est jamais réécrit : elle reste donc à un quart du chemin et ne rattrape jamais
- feat : **Délivrance, le quarante-septième sort** : le sort `0x0044b7b0` (352 octets) et son pétale de couronne `0x0044ba00` (226 octets), tous deux **absents de `Blaze.c`** et sortis du désassembleur ; plus son enveloppe d'effet `0x0044b910` et son halo `0x0044b950`, tous deux décompilés
- doc : **c'est Sang pur à l'envers** — même forme, même enveloppe à garde-fou (`IsEffectPairBlocked_004364B0` puis l'effet), même prêt de la paire famille/indice au lanceur juste avant de frapper. Mais Sang pur soigne dans son bloc de naissance et passe les quarante images suivantes à n'être qu'un décor, là où Délivrance passe le décor d'abord et frappe en sortant, à la trentième image
- doc : **tout le sort tient en un appel** — `ClearStatusEffect_0053C8E0`, qui lève l'effet d'état 9 (le bit `0x10` de `+0x40`, durée en `+0x92`), celui que `ApplyStatusEffect_0053B580` pose. Les trente images qui précèdent ne sont que quatre bruits, un halo et deux couronnes
- doc : **la couronne est un tour entier, et le cap est rendu** — aux images 5 et 10, le sort met son cap à zéro, engendre huit pétales en avançant de `0x200` entre chacun, puis **repose** le cap qu'il avait. Le même prêt-restitution que Parole sainte, en plus court
- doc : **le minuteur du pétale est une échelle** — `+0x34` part de `0x400` et perd 20 par image ; sous `0x96` le pétale entre dans sa fin, et il s'éteint quand `+0x34` devient négatif. Le champ est **relu** après la remise à zéro, si bien que l'image de l'extinction passe aussi par la branche de résorption
- feat : **Parole sainte, le quarante-sixième sort** : le sort `0x0044d370` (1 384 octets), son éclat de départ `0x0044d8e0` (208 octets) et l'éclat posé sur chaque victime `0x0044db30` (304 octets), tous trois **absents de `Blaze.c`** et sortis du désassembleur ; plus sa poussière `0x0044d9b0`, décompilée. Quatre corps, quatre créneaux gagnés
- doc : **le premier sort de l'école 1 qui vise un cône, et non une cible** — les quinze précédents choisissent leur victime par `AcquireHolyTarget_00447CB0` ou par un balayage à la ronde ; celui-ci retient tout ce qui tient à la fois dans un rayon de `0x320` et dans une ouverture de `±0x320` autour de son cap, soit 45 degrés de chaque côté. L'écart nul est refusé : le test demande `> 0` d'un côté et `> 0xce0` de l'autre, et zéro rate les deux
- doc : **et le seul corps transcrit qui vérifie la vue** — une candidate retenue n'est frappée que si le sort peut l'**atteindre** : il déplace sa propre position en quatre pas d'un quart de l'écart, la repose par `FinalizeEntityPlacement2` à chaque pas, et abandonne dès que le contact `0x8800` apparaît en `+0x90`. Un mur entre les deux protège l'ennemi
- doc : **le prix en est une danse de sauvegarde** — puisqu'il se sert de lui-même comme sonde, neuf champs partent sur la pile en tête d'itération et la position dans le triplet `+0x44`/`+0x48`/`+0x4c`, tout revient en queue. `+0x48` est écrit à chaque tour et **jamais relu** : la hauteur revient de la pile. Et la restitution de `+0x90` passe par `and eax,0FFFFh` — le mot haut est effacé, 32 fois par image
- doc : **son critère d'espèce est `0x1000080`** là où Pardon et Zombie demandent `0x3000000` — une autre famille de bestiaire, la première relevée dans l'école
- doc : **la poussière `0x94` se compte en teinte négative** — les trois canaux sont tirés dans `[-76, -37]` et remontent de `-30` par image ; l'entité rend la main quand `+0x38`, **lu comme un octet signé**, atteint exactement zéro. Sa durée de vie dépend donc de son propre tirage

## 2026-08-31
- feat : **Zombie, le quarante-cinquième sort** : un seul corps, `0x0044f930` (477 octets, absent de `Blaze.c`). **Aucun enfant neuf** — son trait est le `0x9e` de Pardon, déjà écrit
- doc : **c'est Pardon sur tout le monde** — même balayage des 32 entités de scène, même critère d'espèce (`statistiques[0x14] & 0x3000000`), même portée de `0x5dc` depuis le lanceur, même rattachement de la victime au père du sort, même trait. La seule différence : Pardon mesure une **seconde** distance pour ne garder que la plus proche, Zombie n'en mesure qu'une et frappe **toutes** les candidates
- fix : **LB-019 n'était pas propre à Pardon** — Zombie porte la même chute, au même endroit : la branche « aucune cible » pose `+0x10 = 0x63` puis tombe dans l'instruction qui pose `+0x10 = 2`. Les deux corps ont été écrits sur le même patron, défaut compris. L'entrée de `legacy-bug.md` est corrigée
- doc : **et Zombie ajoute un second appel d'extinction sans garde** — le `jl` ne saute que le premier des deux `FUN_00448020`. Sans conséquence : l'état `0x63` n'est atteint que par ce même test, donc la garde est toujours vraie quand le bloc s'exécute
- feat : **Calgon, le quarante-quatrième sort** : le sort `0x0044b510` (416 octets), son enveloppe d'effet `0x0044b6b0` et sa goutte `0x0044b6f0` (544 octets, une toutes les trois images), tous trois décompilés ; plus son anneau `0x0044c800` (145 octets, deux exemplaires concentriques), sorti du désassembleur
- doc : **il ne se place nulle part** — aucune de ses 416 instructions ne touche à `+0x1c`, `+0x20` ni `+0x24` : il reste où `SpawnEffectEntity` l'a mis, sur le lanceur. Premier corps de sort de l'école à ne pas recopier la position de sa cible, et donc le seul à ne jamais consulter le repli `DAT_005de558`
- doc : **son décor monte** — les autres fiches de l'école posent trois constantes dans `0x005e9160` une fois pour toutes ; Calgon les réécrit **à chaque image**, le troisième canal valant `(compteur + 0x4b) × 2`, de `0x96` à `0xd2` en trente images
- doc : **deux compteurs, deux rôles** — `+0x16` est l'âge (les sons, la teinte, les deux anneaux, la fin) ; `+0x14` n'espace que les gouttes et **n'est jamais comparé à la durée** : le semis ne s'arrête que parce que le corps cesse d'être appelé
- doc : **la goutte plonge puis remonte** — `+0x12` part de `-8` et gagne huit par image, la hauteur avançant de `+0x12 × 0x1000` : une parabole inversée écrite sans gravité ni vitesse. Elle respire en même temps, ses deux échelles horizontales valant `sin(+0x14) / 2 + 400`
- feat : **Prière, le quarante-troisième sort** : le sort `0x0044ae90` (224 octets) et son dôme `0x0044af70` (1 440 octets), tous deux décompilés dans `Blaze.c`. Aucun corps d'enfant neuf — l'étincelle de prise était écrite avec Lapêche
- doc : **les deux nasses sont complémentaires** — le dôme de Lapêche attrape les entités d'effet qui n'ont **pas** le bit `0x800` ; celui de Prière attrape celles qui l'**ont**. Deux sorts au même prix (22 PM), au même palier, de même longueur de combinaison, posés côte à côte dans le binaire, et qui se partagent exactement le champ des cibles. Aucun autre couple du dépôt ne fait ça
- doc : **et Prière ne repousse pas les monstres, il les nettoie** — il retire les bits `0x880` du mot d'état des entités de scène et ne fait rien naître, là où Lapêche leur donne une vitesse fuyante, un `+0x96` à `0x10` et une étincelle
- doc : **LB-022 — le dôme de Lapêche ne balaie que la moitié de la quatrième table** : `0x10` créneaux de `0x005ff400` au lieu de `0x20`. Sa jumelle Prière en balaie `0x20`, et tout le reste du jeu traite cette table comme `0x20`. Les quatre autres comptes de boucle des deux corps sont identiques ; le `0x10` est isolé
- feat : **Lapêche, le quarante-deuxième sort** : le sort `0x0044a7a0` (224 octets), son dôme `0x0044a880` (1 552 octets — le plus gros corps d'enfant de l'école) et son étincelle de prise `0x004500b0` (288 octets), tous trois décompilés dans `Blaze.c`
- doc : **le premier sort du dépôt qui dure tant qu'on le paie** — le dôme ponctionne deux points au `+0x14e` de la fiche de personnage toutes les dix images et s'arrête dès que ce compteur tombe sous deux. Trois autres conditions le coupent : la combinaison de boutons relâchée (`+0x1fc4` de la fiche contre l'entrée `0x00604a20 + créneau * 0x10`), un état du `+0x140`, et la mort du lanceur
- doc : **le premier sort de l'école sans enveloppe d'effet** — il ne passe jamais par `IsEffectPairBlocked_004364B0` ni par la table des 48 : son effet **est** le dôme, qui travaille directement sur les quatre tables d'entités
- doc : **le dôme est une nasse** — à chaque image il balaie `0x005ecaa0`, `0x005f2d40`, `0x005edee0` et `0x005ff400` et attrape tout ce qui passe à moins de `0x136`. Les entités de scène sont **repoussées** (vitesse = vecteur dôme→elles divisé par huit) ; les trois tables d'effet sont **marquées `0x40000000`**, donc détruites
- doc : **attraper trop de choses tue la nasse** — chaque prise coûte cent au canal bleu du dôme, et le dôme meurt quand ce bleu touche zéro. C'est la seule limite en dehors des points de magie
- doc : **le sort ne sait pas quand il finit** — son état 2, celui qui l'éteint, n'est jamais posé par son propre corps : c'est le dôme qui, en mourant, l'écrit dans le `+0x10` de son père par le `+0x6c`. Signal remontant, comme chez Fusion et Assaut magique
- doc : **quatrième collision de numéro de sous-type dans l'école** — son dôme est le `0x8e` de `0x00551f30` là où l'éclat de Marteau est le `0x8e` de `0x00552230`, et son étincelle est le `0xa5` de `0x00552230` là où la masse de Marteau est le `0xa5` de `0x00551f30`. **Les deux numéros de Marteau, dans l'autre table**
- feat : **Sang d'ange, le quarante et unième sort** : le sort `0x0044a4b0` (419 octets, absent de `Blaze.c`), plus son enveloppe d'effet `0x0044a660` et son aile `0x0044a6a0` (256 octets), tous deux décompilés. Aucun corps d'enfant neuf à sortir du désassembleur
- doc : **malgré son nom, il ne ressuscite pas** — il pose l'entrée 45, la **guérison complète** (points de vie au maximum, le nombre affiché étant ce qui manquait). La résurrection est l'entrée 46, et elle appartient à Résurection (`0x0044cd00`)
- doc : **et il ne sait pas viser un mort** — il appelle `AcquireHolyTarget_00447CB0` en **mode 0**. Le mode 1, celui qui efface `0x40002000` sur les quatre emplacements du groupe le temps de la recherche, n'a **qu'un seul appelant dans tout le binaire** : Résurection. Les quatorze appels décompilés passent 0, et `0x0044cd20` est le seul `push 1`
- fix : **la note de `AcquireHolyTarget_00447CB0` attribuait le mode 1 à Résurection *et* à Sang d'ange** — c'était une déduction sur le nom du sort, pas un relevé. Corrigée, avec la vérification qui la tranche
- doc : **troisième collision de numéro de sous-type dans l'école** — son aile est le `0x8d` de la table `0x00551f30`, là où la spirale d'Antisort est le `0x8d` de la `0x00552230`. Après le `0xa9` d'Antidote et le `0x8c` de Lourdes
- doc : **il résout à la naissance et recopie sa paire famille/indice à la mort**, l'ordre inverse de toutes ses sœurs. Sans conséquence : `CastSpell_0053A980` a déjà écrit cette paire sur le lanceur à l'incantation, donc la fiche lue à l'image 0 est la bonne
- feat : **Marteau, le quarantième sort — LA MOITIÉ DES QUATRE ÉCOLES EST ÉCRITE** (40 fiches sur 80) : le sort `0x00449f90` (176 octets), son enveloppe d'effet `0x0044a040` et sa masse `0x0044a080` (662 octets), tous trois décompilés ; plus son éclat `0x0044a320` (386 octets, six d'un coup), sorti du désassembleur
- doc : **le corps de sort le plus court de l'école** — 176 octets qui n'engendrent que la masse et attendent trente images. Ni son, ni décor, ni immobilisation, ni semis : **tout le spectacle appartient à l'enfant**, qui survit d'ailleurs au sort
- doc : **la masse écrase sa victime pour de bon** — elle sauvegarde le `+0x2a` de la cible (son échelle verticale) à la naissance, le force à `0x800` à l'impact, le laisse revenir d'un quart par image, et le **repose exactement** en mourant. Aucun autre corps du dépôt ne déforme sa victime
- doc : **le premier sort qui fait clignoter l'écran** — à l'impact il pose le voile plein cadre `DAT_005de024` en rouge sombre (`ff/96/96`), et deux images plus tard le remet à zéro. Seule écriture de ce champ par un corps de sort
- doc : **l'éclat jaillit puis s'arrête presque sur place** — son rayon gagne `+0x5c` par image, mais `+0x5c` perd un quart de lui-même à chaque image : six images suffisent à épuiser la vitesse. Son tangage est tiré **dans un secteur** (`rand % 0x4b0 - 0xc8`), pas sur le tour entier — les six montent en gerbe
- fix : **une note de la section Pardon attribuait le sous-type `0x96` à `0x0044a320`** — c'était un index compté à la main sur la table imprimée. Le créneau `0x96` de `0x00552230` vaut `0x0044f750` ; `0x0044a320` est le créneau `0x8e`, l'éclat de Marteau. Une entrée de table se relève, elle ne se compte pas
- feat : **Antisort, le trente-neuvième sort** : le sort `0x00449c30` (448 octets) et son enveloppe d'effet `0x00449df0`, décompilés ; plus sa spirale `0x00449e30` (341 octets, vingt exemplaires par paires), sortie du désassembleur
- doc : **la troisième du même moule, et la dernière** — Antidote, Lourdes et Antisort partagent l'auréole `0xa9`, les mêmes cinq réglages, les mêmes deux premiers sons, la même fin à la centième image et le même décor laissé tranquille. Elles posent **trois entrées consécutives** de la table des 48 — 34, 35, 36 —, toutes trois de la famille qui efface des états
- doc : **son calendrier est le seul des trois à être creux aux deux bouts** — ses vingt spirales sortent au **milieu**, une paire toutes les deux images de la 21ᵉ à la 39ᵉ seulement. Antidote et Lourdes semaient sans interruption de la première à la soixantième
- doc : **sa spirale est la cousine de celle de Renfort** — `0x00449e30` et `TickHolySpark_0044FE00` ont la même forme au demi-octet près : même chute de `0xfa000` par `0xa000`, même descripteur `0x00551550`, même orbite non recadrée, même `+0x14` charnière. La différence tient en une ligne : ici le **rayon se referme** d'une unité par image, plancher à `0x28`
- doc : **les mêmes champs servent deux espèces d'enfant** — `+0x14`, `+0x44`, `+0x5c` et `+0x9a` sont posés une fois pour l'auréole à l'image 0, puis reposés avec d'autres valeurs avant chaque paire de spirales
- feat : **Lourdes, le trente-huitième sort** : le sort `0x004498b0` (384 octets) et son enveloppe d'effet `0x00449a30`, décompilés ; plus son grain `0x00449a70` (435 octets, soixante exemplaires), sorti du désassembleur. L'auréole était déjà écrite avec Antidote
- doc : **le même piège qu'Antidote, un sort plus tard** — Antidote sème un sous-type `0x8c` par `SpawnStatusEffectEntity_0047AC40` (table `0x00552230`, une bulle qui monte) ; Lourdes sème le **même numéro** par `SpawnCastEntity_0047AAE0` (table `0x00551f30`, un grain qui tombe). Deux fiches voisines, deux corps différents sous le même numéro — après le `0xa9` de la fiche d'avant. **La table est la moitié du sous-type**
- doc : **le grain tombe vers le centre au lieu de tourner autour** — position sphérique jamais intégrée, rayon qui **diminue** de `rand % 30 + 20` par image depuis `rand % 200 + 100` jusqu'à zéro. Son `+0x32`/`+0x34` le tourne vers le centre sur lequel il tombe
- doc : **son bloc de naissance efface le centre que l'en-tête vient de calculer** — et l'effet ne dure qu'une image, l'en-tête le recalculant dès la suivante. Transcrit tel quel
- doc : **pour le reste c'est Antidote à quatre valeurs près** : le son répété (`0x27` à `0x1000` contre `0x21` à `0x200`), l'enfant semé, sa cadence (une par image contre une sur deux) et l'effet posé (35 au lieu de 34)
- feat : **Antidote, le trente-septième sort** : le sort `0x00449590` (400 octets) et son enveloppe d'effet `0x00449720`, décompilés ; plus deux corps neufs sortis du désassembleur, la bulle `0x00449760` (335 octets, trente exemplaires) et l'auréole `0x004501b0` (275 octets)
- doc : **un sous-type ne veut rien dire sans sa table** — les sorts précédents semaient un `0xa9` par `SpawnStatusEffectEntity_0047AC40`, donc dans la table `0x00552230` où il désigne l'étincelle de la bouffée. Antidote sonne le **même numéro** par `SpawnCastEntity_0047AAE0`, donc dans la `0x00551f30`, où il désigne `0x004501b0` — une auréole qui n'a rien à voir
- doc : **l'auréole est partagée par trois fiches** — Antidote et ses deux voisines du binaire (`0x004498b0`, `0x00449c30`) l'appellent à l'identique. Elle monte de `+0x2a × 150` par image, `+0x2a` croissant lui-même jusqu'à un plafond, et `+0x48` est à la fois son compte à rebours et son seul aiguillage : positif elle s'éclaircit, négatif elle s'éteint
- doc : **c'est le premier sort de l'école 1 qui laisse le décor tranquille** — aucune écriture dans `0x005e9160`, aucune immobilisation du lanceur, aucune recherche de victime. Il colle à sa cible, y compris en reprenant son cap `+0x32` à chaque image, et pose l'entrée 34 à la centième — la seule des 48 qui **efface** des altérations d'état au lieu d'en poser une
- doc : **il énumère neuf valeurs de compteur en clair** pour rejouer le même son toutes les dix images de la 2ᵉ à la 82ᵉ, là où un reste aurait suffi. Aucun autre corps de sort du dépôt ne fait cela
- feat : **3ème œil, le trente-sixième sort** : le sort `0x00448ef0` (701 octets) et son enveloppe d'effet `0x004491b0`, tous deux décompilés dans `Blaze.c`. **Aucun corps d'enfant neuf** — ses quatre sous-types sont ceux de Sonde
- doc : **c'est Sonde à cinq valeurs près, et au même prix de deux PM** — la teinte des traits (`+0x5c = 0x0d` contre `0x0e`), celle de l'embrasement (2 contre 9), la teinte du décor (`0xdc/0xa0/0xa0` contre `0xa0/0xa0/0xdc`), l'effet posé (2 au lieu de 1), et le rythme des étincelles : **une par image** de la 80ᵉ à la 139ᵉ, là où Sonde teste la parité de son compteur et n'en sème qu'une sur deux — soixante contre trente
- doc : **il réveille une branche morte** — le semis d'étincelle du trait teste `+0x0a` contre `0x0e` puis contre `0x0d` ; le second test était inatteignable tant que Sonde était le seul appelant. Les étincelles de l'un naissent avec la teinte 9, celles de l'autre avec la teinte 2
- feat : **Bobos finis, le trente-cinquième sort** : le sort `0x004491f0` (822 octets, absent de `Blaze.c`), plus son enveloppe de soin `0x00449530` et le comptage `0x00449570`, tous deux décompilés. **Aucun corps d'enfant neuf** — l'étincelle `0xa9` et la bille `0x70` étaient déjà écrites pour Sang pur
- doc : **c'est Sang pur sur les quatre emplacements du groupe** — sa seconde moitié est le bloc de bille de `TickPureBlood_00447DF0` instruction pour instruction, enroulé dans une boucle sur les membres. Le sort a été écrit en recopiant son cadet de trois fiches
- doc : **deux balayages du groupe, et un masque entre les deux** — le premier tente le soin sur chaque emplacement et note dans `+0x98` le bit de ceux qui l'ont **refusé** ; le second, une image sur deux pendant quarante images, saute ceux dont le bit est posé. Un membre que le soin n'a pas pris n'est pas décoré non plus
- doc : **le soin est le soin PARTAGÉ, pas le soin simple** — `0x00449530` mène à `HealPartyShare_0053CB30`, l'entrée 44, celle qui **divise** le montant par le nombre de soignables, et elle est appelée quatre fois de suite. Quarante-deux PM pour cela
- doc : **son premier appel est mort** — `0x00449570` compte les entités d'effet vivantes, le résultat est rangé dans un emplacement de pile et jamais relu ; la fonction ne lisant que de la mémoire, l'appel entier est sans effet. L'intention lisible est un plafond au nombre d'étincelles, jamais câblé
- doc : **LB-021 — une étincelle décidée par de la pile morte** : le prologue réserve huit octets de locaux dont un seul est écrit ; l'autre est lu une fois, en mot, et comparé à `0x3c` pour décider d'un semis. Le port le déclare à zéro, sur le même principe que LB-015
- feat : **Sonde, le trente-quatrième sort — le plus gros de l'école 1** : cinq corps, 2 500 octets pour deux PM. Le sort `0x004484f0` (704 octets), son enveloppe d'effet `0x004487b0`, son trait `0x004487f0` (1 104 octets, huit exemplaires), son embrasement `0x00448c40` (510 octets) — tous décompilés — et son étincelle `0x00448e20` (194 octets), sortie du désassembleur
- doc : **le sort se sert de ses propres champs comme d'un passe-plat** — `+0x14`, `+0x16`, `+0x48` et `+0x9a` sont écrits juste avant chaque `SpawnCastEntity_0047AAE0` pour que la copie des `0x27` dwords les porte dans l'enfant, puis remis à leur rôle. Le cas de `+0x16` est le plus net : c'est le **compteur d'images du sort**, et il transporte quatre angles tirés au hasard avant d'être remis à zéro
- doc : **huit traits en quatre paires, sur les quatre quarts de tour** — les deux traits d'une paire partagent le cap et ne diffèrent que par leur modèle, `0x18` ou `0x19` ; c'est cette seule valeur qui décide, cent images plus tard, de laquelle des deux fins le trait prend
- doc : **le trait est en coordonnées sphériques et ne s'intègre jamais** — sa position vaut `ancre + vecteur(cap, tangage, rayon)` recalculé à chaque image, l'ancre étant **le sort** (`+0x6c`) et non la cible. Quatre états sur un seul rayon qui converge de `0x3e8000` vers `0x50000` d'un seizième par image — et le rayon avance **deux fois** par image dans l'état 1, une fois dans le bloc commun et une fois dans le bloc d'état
- doc : **l'embrasement lit `+0x60` de sa victime pour se placer**, pas `+0x20` — le seul corps de sort transcrit qui se cale sur ce champ-là. Il immobilise la cible par `0x1000` et la relâche à sa `0x96`ᵉ image
- doc : **les deux `CONCAT22` de la décompilation du trait sont des demi-registres résiduels** — la moitié haute laissée par un `movsx` précédent. Elle vaut toujours zéro ici : l'état 1 masque `+0x32` et `+0x34` sur douze bits à chaque passe et s'exécute forcément avant l'état 2, y compris dans la passe où il y bascule
- doc : **LB-020 — le fusible de secours du trait ne peut pas sauter** : sa dernière instruction incrémente le compteur **octet** `+0x43` et compare le résultat, masqué à `0xff`, à 5 000. La branche d'extinction est inatteignable ; seul le chemin normal éteint un trait
- feat : **Gnoukoulé, le trente-troisième sort** : le sort `0x00448300` (418 octets) et son enveloppe d'effet `0x004484b0`, tous deux décompilés dans `Blaze.c`. **Aucun corps d'enfant neuf** — l'étincelle `0xa7` et l'éclat `0xa0` avaient été écrits avec Renfort
- doc : **le binaire porte deux fois le même corps** — Gnoukoulé est Renfort à **quatre valeurs près** : la teinte `+0x9a` posée à 2 au lieu de 6, un son `0x0d` qui accompagne l'éclat de la 30ᵉ image, le couple de sons des images 35 et 37 remplacé par un seul à la 32ᵉ, et l'effet 23 (`0x0053bfb0`) au lieu du 17. Calendrier, bouffées, rayon, pas angulaire, durée de croissance et fin sont identiques. Ce ne sont pas des paramètres : ce sont 418 octets recopiés, et les quatre différences sont des immédiats
- doc : **la même enveloppe d'effet en trois exemplaires** — `0x00447fe0`, `0x00448210` et `0x004484b0` ont le même corps à l'effet appelé près, chacune posant `IsEffectPairBlocked_004364B0` devant. Aucune des trois n'est lue par son appelant
- feat : **Renfort, le trente-deuxième sort** : le sort `0x00448060` (425 octets), son enveloppe d'effet `0x00448210` et son éclat `0x00448250` (168 octets) — **tous trois décompilés dans `Blaze.c`** —, plus son étincelle `0x0044fe00` (315 octets, absente de `Blaze.c`), sortie du désassembleur
- doc : **il pose son effet à la soixantième image, Sang pur à la première** — deux voisins immédiats dans le binaire, de forme identique, opposés par le moment. Les cinquante-neuf premières images de Renfort sont entièrement décoratives
- doc : **ses étincelles naissent par paires opposées** — le sort pose `+0x48`, l'angle de départ que l'enfant hérite par la copie, à `0` puis à `0x800` entre les deux appels : chaque bouffée est deux étincelles à un demi-tour d'écart sur le même cercle. Quatre bouffées aux images 1, 3, 6 et 9, huit étincelles en tout
- doc : **l'étincelle `0x0044fe00` tombe en tournant, et sa position n'est jamais intégrée** — elle vaut `centre + sin/cos(+0x48) × +0x44` à chaque image, le centre suivant la cible `0x3c000` plus haut, tandis que sa hauteur descend de `0xa000` par image depuis `0xfa000` au-dessus. **Le produit n'est pas recadré** : `imul` sans `>> 0xc`, contrairement au reste du dépôt
- doc : **un seul compteur commande la croissance, la rétraction et la mort de l'étincelle** — `+0x14`, posé à `0x28` par le sort, perd un par image : positif, l'échelle monte jusqu'à `0x800`/`0x1000` ; négatif, elle redescend du même pas et l'entité s'éteint quand `+0x28` passe sous zéro
- doc : **quatre champs du sort n'existent que pour ses enfants** — `+0x44` le rayon, `+0x5c` le pas angulaire, `+0x14` la durée de croissance, `+0x9a` la teinte : aucun ne sert à Renfort lui-même, tous sont recopiés dans chaque étincelle par `SpawnStatusEffectEntity_0047AC40`
- doc : **Gnoukoulé (`0x00448300`, école 1 indice 3) est le même corps à quatre valeurs près** — même trio de tête, mêmes bouffées, même éclat à la 30ᵉ image, même fin à la 60ᵉ. Il ne diffère que par le `+0x9a` posé à 2 au lieu de 6, deux sons et l'effet appliqué. **Les deux enfants écrits ici sont donc les siens aussi**
- feat : **Sang pur, le trente et unième sort — et le premier du dépôt qui n'apporte aucun corps d'enfant** : le sort `0x00447df0` (487 octets, absent de `Blaze.c`), plus le soin sous condition `0x00447fe0` et le garde-fou partagé `0x004364b0`, tous deux décompilés. Ses deux sous-types engendrés étaient déjà écrits — l'étincelle `0xa9` est celle de la bouffée, la bille `0x70` celle d'Enchant
- doc : **il soigne à la première image, pas à la dernière** — Renfort, son voisin immédiat dans le binaire et de forme identique, attend la soixantième image pour poser son effet ; Sang pur appelle le soin dans son bloc de naissance et passe les quarante images suivantes à n'être plus qu'un décor
- doc : **`+0x14` porte deux conventions dans le même corps** — posé à `0x50` avant chaque étincelle `0xa9`, où il vaut le rayon de la spirale, et remis à zéro avant chaque bille `0x70`, où il vaut vitesse angulaire. Un seul champ, réglé à chaque appel selon l'espèce engendrée
- doc : **il colle à sa cible sans se relever** — il recopie chaque image la position de `+0x78` dans la sienne, mais **sans le `+0x50000` vertical** que les autres fiches de l'école ajoutent en `+0x20`. La compensation est reportée sur le `+0x48` de ses billes
- doc : **les angles de ses billes sont tirés modulo, ceux d'Enchant ne l'étaient pas** — `+0x32` vaut `rand % 0x400 + 0x400` et `+0x34` vaut `rand % 0x1000`, là où Enchant écrit le tirage brut : les billes de Sang pur tombent dans un quart de tour, celles d'Enchant sur le tour entier
- doc : **le garde-fou `0x004364b0` n'est pas pur** — avant tout examen il écrit `lanceur[7] = 7` lorsque le lanceur porte `0x400000` sans `0x800`, c'est-à-dire qu'il force l'école de sa paire famille/indice à la huitième, celle qu'aucune classe ne peut apprendre. L'effet de bord a lieu même quand le garde-fou refuse ensuite
- feat : **Pardon, le trentième sort — et l'école 1 commence** : le sort `0x0044f060` (558 octets, absent de `Blaze.c`) et son trait `0x0044f290` (874 octets, décompilé). Plus **les trois fonctions partagées du Prêtre**, décompilées elles aussi : `0x00448020` l'extinction avec remise du décor, `0x00448040` l'extinction sur contact, `0x00447cb0` le choix de la cible
- doc : **l'école 1 n'est pas bâtie comme l'école 0** — ses vingt-quatre fiches commencent presque toutes par le même trio d'appels, là où les vingt-neuf de l'école 0 repartaient chacune de zéro. Écrire ce trio d'abord était le vrai travail de cette séance
- doc : **`0x00447cb0` sait viser un mort** — dans son mode 1 il efface `0x40002000` sur les quatre emplacements du groupe avant d'appeler `FindTargetAround_00474BA0`, puis repose les bits retirés. C'est le mécanisme de Résurection et de Sang d'ange, et aucune autre recherche du dépôt ne l'autorise. La boucle est écrite en dur sur quatre membres, sans consulter `s_PartySize_005e9020`
- doc : **le premier sort qui choisit sa victime sur une fiche et non sur une distance** — Pardon balaie les `0x20` entités de scène, résout leurs trois blocs par `ResolveEntityBlocks`, et n'accepte que celles dont le bloc de statistiques porte `0x3000000` en `+0x14`. Il mesure ensuite **en trois dimensions**, deux fois : moins de `0x5dc` depuis le lanceur pour être éligible, la plus courte depuis le sort pour être choisie — là où tout le reste du dépôt mesure à plat
- doc : **LB-019, une écriture morte** — la branche « aucune cible » pose `+0x10 = 0x63`, l'état de fin, puis **tombe** dans l'instruction suivante qui pose `+0x10 = 2`. Le `+0x16 = 0x46` de la même branche survit, ce qui ramène l'attente de 90 à 20 images au lieu de la supprimer. Transcrit tel quel
- doc : **trois sous-types restent à écrire derrière lui** — le trait `0x9e` fait naître `0x9f` (`0x0044f600`), `0x93` (`0x0044c110`) et `0x96` (`0x0044a320`) ; aucun n'est décompilé, et les deux derniers appartiennent à d'autres fiches de l'école
- feat : **Fusion, le vingt-neuvième sort — et L'ÉCOLE 0 EST COMPLÈTE** : ses 29 fiches ont toutes leur gestionnaire. Deux corps neufs seulement : le sort `0x004828f0` (1260 octets) et sa bille `0x00482de0` (234 octets, neuf exemplaires, **décompilée dans `Blaze.c`**), plus l'étincelle `0x00482ed0` — **25 octets, le plus court corps de sort du dépôt**, devant les 30 du halo d'Exorciste
- doc : **son compteur de vitesse est son compteur de mise à feu** — `+0x16` est à la fois l'avance en unités par image et le décompte avant la détonation : parti à `0x19` il perd un par image, et le corps saute tout le bloc central tant qu'il n'est pas nul. La boule roule donc en ralentissant sur vingt-cinq images, et **c'est son immobilité qui l'allume**. Un mur (`+0x90` sur `0x8802`) le met à zéro d'un coup : elle explose où elle s'arrête
- doc : **elle brasse tout un camp** — à chaque image elle balaie les `0x20` entités de scène (`0x005ecaa0`) ou les membres du groupe (`0x005e93c0`) selon le camp du lanceur, et ajoute à la vitesse de chaque corps à moins de `0x320` un vecteur pris au cap qui va d'elle vers lui **moins `0x600`** : la correction usuelle étant `0x400`, c'est le rayon sortant tourné d'un huitième de tour — un tourbillon, pas une poussée droite. Aucun autre corps de sort transcrit ne fait cela
- doc : **la moitié de ses enfants était déjà écrite** — la traînée `0x50` est celle de Boule de feu, la bourrasque `0x83` celle de Ventilo. Et son étincelle `0x7a` est **la bourrasque de Ventilo moins une instruction** : `TickGaleGust_0047FE40` fait exactement la même chose et pose en plus le bit `4` de `+0x36`, d'où ses quatre octets de plus. Le compilateur a émis deux fois la même idée sans la partager
- doc : **il ne meurt pas de lui-même** — passé `0xc8` images il se pose `0x2000000` et remet son âge à zéro, mais il attend ensuite le `0x1000000` que sa bille lui pose en achevant son fondu. Même signal remontant qu'Assaut magique, et ici la **seule** condition de fin
- feat : **Météores, le vingt-huitième sort** — trois corps absents de `Blaze.c` : le sort `0x00482370` (381 octets), le météore `0x004824f0` (954 octets, **huit exemplaires**) et sa traîne `0x004828b0` (58 octets, deux par météore)
- doc : **le premier sort qui n'immobilise pas son lanceur** — aucun de ses 381 octets n'écrit dans le mot d'état du lanceur : il ne pose jamais `0x80`. Les vingt-sept sorts précédents le faisaient tous. Il ne lit pas la manette non plus et ne ponctionne aucun PM : il dure `0x50` images et s'arrête, et le joueur reste libre pendant toute la pluie
- doc : **et le premier qui sonde le décor avant d'engendrer** — une image sur huit il se transporte lui-même à un point tiré dans un carré de 256 unités autour d'une ancre posée devant le lanceur, se recopie les six mots de contexte de collision du lanceur (`+0x7c`), appelle `FinalizeEntityPlacement2` **trois fois de suite** en accumulant `+0x90`, et n'engendre le météore que si `0x8800` est resté absent. Aucun autre corps transcrit ne fait ni l'un ni l'autre
- doc : **le premier enfant de sort qui partage la table de son père** — le météore naît par `SpawnEffectEntity` et non `SpawnCastEntity`, donc dans `0x005ff400`, et son sous-type `0x65` est résolu par `0x00551c30`, la table des sorts eux-mêmes. Tous les enfants transcrits jusqu'ici vivaient dans `0x00551f30` ou `0x00552230`
- doc : **l'arbre le plus profond du dépôt** — le sort engendre le météore, qui engendre à son tour quatre espèces : deux traînes `0x78` pendant la chute, trois anneaux `0x6c` et quatre éclats `0xbf` à l'impact, puis une éclaboussure `0xbe` par image pendant ses dix images de résolution
- feat : **Colère, le vingt-septième sort** — **cinq corps neufs**, tous absents de `Blaze.c`, le sort le plus fourni du dépôt : le sort `0x004819d0` (883 octets), le halo `0x00481d50` (quatre exemplaires), la tête `0x00481e30` (876 octets), l'anneau `0x004821a0` (**sept exemplaires**) et la colonne `0x00482300`. Plus le huitième script de sons différés, `0x005525c0`, **huit pas** — les huit scripts de l'école 0 se suivent maintenant sans trou depuis `0x00552530`, mais la file continue à `0x00552600`
- doc : **un serpent de huit anneaux, chaîné par numéro d'emplacement** — le sort réserve dix places d'un coup, puis avant chaque naissance lit l'indice libre courant du pool `0x006007a0`, engendre, **puis** range cet indice dans son `+0x16` : chaque anneau reçoit donc le numéro de son *prédécesseur*, pas le sien. Il retrouve son devancier par `0x005edee0 + indice × 0x9c` et se place derrière lui. Deuxième communication par indice du dépôt après Démon polaire, et la première qui forme une **chaîne**
- doc : **le seul appel du dépôt avec une variante non nulle** — la tête passe `1` en cinquième argument de `ResolveSpellHit_0053A2B0`, ce qui lui fait lire `+0x17` et `+0x1a` de la fiche au lieu de `+0x16` et `+0x18`. La documentation de cette fonction disait « vaut 0 pour tous les appelants transcrits » ; corrigée
- doc : **le plus cher à entretenir, de loin** — 2 PM toutes les **cinq** images, contre 1 PM par dix pour Éclair, le précédent record. Hors joueur il dure 300 images, et il balaie : sa sonde fait `0x190` de profondeur sur `0x200` d'ouverture et son cap tourne de `0x190` par image
- doc : **la tête a un limiteur à deux étages, seul cas du dépôt** — les angles voulus sont d'abord rangés dans `+0x14`/`+0x16` en ne bougeant que de `0x100` par image quand l'écart dépasse `0x100`, puis `+0x32`/`+0x34` rejoignent ceux-là d'un quart de l'écart par le plus court chemin. Sa poussée s'ajoute à la vitesse, qui perd ensuite un quart
- doc : **le halo meurt par fondu** — sur `0x2000000` il retire `+0x44` de sa teinte par image et ne se coupe qu'une fois les vingt-quatre bits bas à zéro. Seul corps du dépôt dont la fin soit un fondu et non un seuil. C'est aussi lui qui déclenche le second temps : arrivé à sa teinte de consigne `+0x48`, il décrémente le `+0x16` du sort ; à zéro le sort se pose le bit 2 et le serpent naît
- feat : **Au sec, le vingt-sixième sort** — le sort `0x00481670` (169 octets, **décompilé dans `Blaze.c`** — le premier de l'école 0 dans ce cas depuis Balle de feu) et sa spirale `0x00481720` (680 octets, absente). Plus le septième script de sons différés, `0x00552598`, **le plus long du dépôt** : cinq pas
- doc : **ce n'est pas un sort de combat, c'est la sortie de donjon** — à sa soixante-quinzième image la spirale pose `s_GameStateId_005de510 = 2` (l'écran de destination) et `s_WorldMapStep_005645f8 = 0xfe`, ce qui arrête la boucle de la carte du monde. Seul corps du dépôt qui change d'écran, et le nom le disait
- doc : **et le premier qui s'adresse au groupe entier** — le sort balaie `Inp.s_SlotFlags_005e93c0` et engendre **deux spirales par membre vivant**, l'une au cap courant l'autre à l'opposé, puis s'éteint dans la même image. Ni âge, ni sonde, ni fin : tout le sort tient en une passe, la durée appartient à ses enfants. Hors d'un lanceur du groupe (bit `0x800000`) il ne fait rien du tout
- doc : **la spirale rétrécit le membre lui-même** — de l'âge 32 à 47 elle écrit `0x1000 - sin` dans les deux échelles du membre en même temps que dans les siennes, puis de 48 à 63 elle l'emporte vers le haut de `0x3c000` par image. Sa position est toujours celle du membre plus un rayon `échelle × 15 >> 12`, et sous l'âge 48 sa hauteur est le **sol du membre**
- doc : **la transition est faite deux fois par membre** — les deux spirales ne diffèrent que par leur cap, et le `+0x16` que le sort leur distingue (0 puis 1) **n'est jamais relu par leur corps** : toutes deux atteignent l'âge 0x30 et refont le bloc en entier. Le `+0x12` du membre et le compteur `0x005de544` sont incrémentés deux fois, l'anneau `0x6c` naît en double. Transcrit tel quel
- feat : **Satanas, le vingt-cinquième sort** — trois corps absents de `Blaze.c` : le sort `0x00480b90` (688 octets), le sceau `0x00480e40` (1038 octets, **douze exemplaires**) et le démon `0x00481250` (1050 octets). Plus le sixième script de sons différés, `0x00552588`
- doc : **le seul sort du dépôt qui tue sans passer par les dégâts** — son démon n'appelle pas `ResolveSpellHit_0053A2B0` mais `SlayTarget_0053CDF0`, l'entrée 47 et dernière de la table des 48 : la mort immédiate, la seule qui exige **deux jets de sauvegarde d'affilée**. Les vingt-quatre sorts précédents retiraient des points de vie ; celui-ci en demande zéro
- doc : **et le seul qui se déroule en trois actes, chacun gardé par un bit** — douze sceaux posés en cercle autour du lanceur, espacés de `0x155` et retardés de deux images de plus chacun, grossissent sur place ; le premier arrivé à la teinte `0xf0` pose `0x1000000` sur le sort, ce qui **ouvre la chasse** (`FindNearestOpponent_00474EB0`, 0x3c images pour trouver) ; les sceaux volent alors vers la proie et s'y **accrochent**, et quand les douze tiennent (`+0x14 == 12`) le démon naît et fonce
- doc : **le sort lui-même ne touche jamais personne** — ni sonde, ni appel de résolution : il n'est que l'ordonnanceur de ses deux enfants, et il compte de trois façons à la fois (`+0x16` les sceaux vivants, `+0x14` ceux accrochés, `+0x12` la charge du bouton). Premier corps de sort du dépôt dans ce rôle
- doc : **il lit la manette par la proie, pas par le lanceur** — l'indice de personnage vient de l'entité liée en `+0x78` (de `DAT_005de538` si elle porte `0x10000`, de son `+0x08` sinon), et le masque est `0xf0` sur l'octet `+2` de l'état de manette. Aucun autre corps ne s'adresse à la manette par ce chemin
- doc : **une garde hors d'atteinte, écrite telle quelle** — pendant l'approche, le démon s'éteint si `+0x12` du sort divisé par dix atteint 12, soit 120 ; or ce compteur est plafonné à `+0x14 × 5`, et `+0x14` ne dépasse pas douze — donc 60. La branche est transcrite : c'est une borne du binaire, pas un oubli
- doc : **le démon recopie sa fiche de sort sur le lanceur** — juste avant de frapper il écrit son `+0x06` (famille/indice) dans le `+0x06` du lanceur, **avant** l'appel, donc `SlayTarget` la lit. Seule écriture du dépôt d'un enfant vers ce champ de son invocateur
- feat : **Stalagtite, le vingt-quatrième sort** — trois corps absents de `Blaze.c` : le sort `0x00480600` (1048 octets), l'aiguille `0x00480a20` (79 octets) et la pierre `0x00480a70`. L'éclat `0xbf` que sème l'éboulement est celui de Flêches magiques, déjà écrit
- doc : **le premier sort qui vive deux vies dans un seul corps** — son mot d'état commence par tester `0x2000000` et se branche sur **deux corps complets**, pas sur une fin de vie. Bit absent : la pointe rampe, vingt unités par image sur son cap, la hauteur relevée avant le placement au sol et rendue après — elle glisse à plat au lieu d'épouser le relief. Bit posé : la position devient l'ancre, le sol devient la hauteur, l'âge repart de zéro et le même corps devient un éboulement de 0x50 images. Tous les autres sorts du dépôt s'éteignent sur ce bit ; celui-ci s'en sert de sablier
- doc : **ses six aiguilles sont un chapelet, pas une gerbe** — une sans écart (modèle `0x15`), puis cinq (modèle `0x16`) à 30, 60, 90, 120 et 150 unités sur le cap **du lanceur**, chacune `0x200` plus petite que la précédente sur les trois axes. Aucune ne calcule son déplacement : `0x00480a20` relit l'ancre de son père à chaque image et y ajoute son écart figé à la naissance, puis meurt du bit `0x2000000` **lu chez le père** — le sens inverse d'Assaut magique
- doc : **et il ne coûte rien à maintenir** — même code de manette que les sept sorts entretenus, même garde `CanSustainChannel_00475110`, mais **aucune ponction de PM** : il dure tant que le bouton tient, ou vingt images si le lanceur n'est pas un joueur. Seul cas du dépôt
- doc : **son atlas de poussière donne la borne gauche d'une table déjà relevée** — il tire `rand % 3` dans `0x00551af8`, dont les deux derniers enregistrements sont les **deux premiers** de `s_Descriptors_00551B00`. Les deux consommateurs lisent le même bloc de `.data` à deux bases distantes de huit octets, et se recoupent
- feat : **Démon polaire, le vingt-troisième sort** — trois corps absents de `Blaze.c` : le sort `0x0047fe60`, la tête `0x00480080` (1026 octets) et la queue `0x00480490`. Septième sort entretenu, à 5 PM par vingt images, et **le plus gourmand du dépôt : six entités d'un coup**
- doc : **le seul sort qui vérifie ses ressources avant de partir** — sa première instruction utile compare le curseur du pool `0x006007a0` à `0x76` ; au-delà, il s'éteint sans immobiliser le lanceur ni jouer de son. Aucun autre corps du dépôt ne fait ce contrôle
- doc : **ses deux moitiés se retrouvent par leur numéro d'emplacement, pas par un pointeur** — le sort réserve deux emplacements par `FUN_00475090` (qui trie les deux plus petits indices libres en tête **sans avancer le curseur**), les range dans `+0x16` et `+0x17`, puis engendre la tête et la queue ; la queue recalcule l'adresse de sa tête par `0x005edee0 + (+0x16) × 0x9c`. Sixième forme de dialogue parent-enfant relevée, et la seule par indice
- doc : **la tête bat des ailes sur huit images** — la phase `(âge & 7) << 7` décale le tangage visé, et la même quantité décalée de cinq bits donne sa vitesse : l'ondulation et la poussée sortent du même compteur. Elle tourne ses **deux** angles par le plus court chemin, là où le fantôme de Trouille n'en tournait qu'un
- feat : **Ventilo, le vingt-deuxième sort** — deux corps neufs seulement : le sort `0x0047fb80` et sa bourrasque `0x0047fe40` (29 octets). Plus le quatrième script de sons différés, `0x00552568`
- doc : **c'est Coup de jus trois fois** — il réemploie tel quel le segment `0x75` du sort 11 et l'éclat `0xbe` déjà écrit. Sa boucle est double : **trois éclairs de trois segments**, tous partis du **même point** — la position est mise de côté et rendue entre chaque —, avec des caps de départ espacés de `0x200` qui ouvrent l'éventail
- feat : **Boule de feu, le vingt-et-unième sort** — trois corps neufs absents de `Blaze.c` : le sort `0x0047f570` (**1425 octets, le plus gros du dépôt**), sa coque `0x0047fb10` et sa traînée `0x00482ef0`. Le quatrième enfant, l'éclat `0xbe`, était déjà écrit et exercé
- doc : **le seul sort qui choisisse entre deux sondes** — deux blocs de six mots se suivent sur sa pile, et la fin du corps calcule `esp + eax*12 + 0x1c` : une sonde étroite (50 de portée) tant que la boule vole, une large (512) pendant l'explosion. Les trois sondes variables du dépôt *modifiaient* leurs mots ; celle-ci en **sélectionne** une
- doc : **et le seul qui secoue l'écran** — l'explosion pose 8 dans `DAT_005de0ac`, le décalage vertical de caméra, puis le sort le ronge lui-même de trois quarts par image. Il peint aussi le fond en rouge **directement** dans `s_BackColour*_005e93a0`, sans passer par le mode 2 des autres sorts
- doc : **il lit la manette sans ponctionner de PM** — le joueur peut relâcher pour déclencher l'explosion, mais aucun prix supplémentaire n'est prélevé. Seul cas du dépôt
- doc : **LB-018, quatrième défaut de la même famille** — entre ses images 10 et 19, le cap de la poussée est lu dans l'emplacement de son propre argument, jamais écrit sur ce chemin : la victime part dans la direction de l'adresse de l'entité. Après LB-008, LB-015 et LB-017, et comme elles dans un corps que Ghidra n'a pas décompilé
- feat : **Durdur, le vingtième sort** — trois corps absents de `Blaze.c` : le sort `0x0047f2b0`, sa coquille `0x0047f400` et son onde `0x0047f470`. **Un quart de l'école 0 est écrit**
- doc : **il emprunte la branche « sans ancre » de l'anneau d'Onde de choc** — il pose `+0x70` à zéro avant d'engendrer le sous-type `0x6c`, ce qui fait replier à l'anneau sa position dans son écart et devenir sa propre référence. Cette branche avait été transcrite « parce qu'elle est là », sans appelant connu : elle en a un
- doc : **la coquille signale par une taille atteinte, pas par une horloge** — dès qu'elle arrive à moins de `0x10` de la taille visée, elle pose `0x2000000` sur son père et s'éteint. Troisième signal remontant du dépôt, après la lame d'Assaut magique et le fantôme de Trouille, et le seul déclenché par une grandeur physique
- doc : **quand l'effet prend, la victime est clouée sur place** — `ApplyStatusEffect_0053B8F0` rend non nul, et l'onde met alors ses deux vitesses horizontales à zéro. Sa profondeur de sonde suit sa propre hauteur (`hauteur × 150 >> 12 + 0x28`) : quatrième sonde variable du dépôt
- feat : **Exorciste, le dix-neuvième sort** — deux corps absents de `Blaze.c` : le sort `0x0047f070` et son halo `0x0047f290`, **30 octets — le plus court corps de sort du dépôt**. Plus le troisième script de sons différés, `0x00552558`
- doc : **il ne frappe pas, il nettoie** — à chaque image il balaie autour de lui et appelle `ClearAllStatusEffects_0053C330`, l'entrée 29 de la table des 48. **Faute de cible il se rabat sur le lanceur** : c'est ce qui en fait un sort de soin autant que d'attaque
- doc : **sa sonde pulse au lieu de croître** — l'échelle bat au rythme d'un cosinus, et l'avance et la profondeur de la sonde en sont tirées à chaque image (sept dixièmes en arrière, quatorze fois plus en avant). Troisième sonde variable du dépôt, après l'ombre de Ténèbres et le segment de Coup de jus
- doc : **le balayage est écrit sur place**, pas via `ProbeMeleeTarget_00474CA0` — la sonde est particulière (un demi-tour de décalage de cap, aucune ouverture) et l'acteur est le **sort lui-même**, pas son `+0x74`
- feat : **Trouille, le dix-huitième sort** — trois corps absents de `Blaze.c` : le sort `0x0047eae0`, le fantôme `0x0047ec30` (808 octets) et la luciole `0x0047ef60`. Cinquième sort entretenu, à 3 PM par vingt images
- doc : **le seul sort qui ne puisse même pas mourir sans le feu vert de son enfant** — le fantôme naît gros, rétrécit, et quand son échelle Y passe sous `0x2040` il se pose le bit `0x02`, **le pose sur son père** et relâche le lanceur. Tant que le sort ne l'a pas, il ne lit ni la manette ni les PM — et sa branche de fin commence par tester ce bit et rend la main s'il manque
- doc : **le fantôme tourne par le plus court chemin** — l'écart d'angle est ramené sur douze bits puis comparé à `0x800` ; au-delà, le huitième est **nié** plutôt que de faire presque un tour complet. Seul corps transcrit à faire ce choix
- doc : deux détails du binaire écrits tels quels dans la luciole : son échelle Y est `taille << 17` et non `<< 16` (donc le double de la X), et son sol est lu en **`+0x64`** et non en `+0x60` comme dans tous les autres corps
- feat : **Eclair, le dix-septième sort** — trois corps absents de `Blaze.c` : le sort `0x0047e590`, le filament `0x0047e6f0` (901 octets, quatre exemplaires) et l'étincelle `0x0047ea80`. Plus le second script de sons différés, `0x00552548`
- doc : **le quatrième sort entretenu, et le plus vorace** — 1 PM toutes les **dix** images, là où Wonder, Atchoum et Lumière ponctionnent toutes les vingt (2, 4 et 6 PM). Même code de manette et même garde pour les quatre
- doc : **son compte de références se fait sur les naissances réussies** — les quatre appels à `SpawnEffectEntity` rendent 1 ou 0 selon qu'un emplacement était libre, et le sort **additionne** ces retours dans `+0x16`. Un pool plein ne le fait donc pas attendre un enfant qui n'existe pas, là où Rocher posait un 4 en dur
- doc : **le filament a deux vies, séparées par le bit `0x40` de son propre mot d'état** — libre il vole (vers sa cible à la vitesse `0x10`, ou en errant près du lanceur à 8 avec un bruit de ±256) ; dès qu'il touche il pose `0x40`, reste accroché et décharge **une image sur huit**. C'est aussi le seul corps du dépôt qui hérite du signal de fin de son père *et* décrémente son compte
- feat : **Lumière, le seizième sort** — deux corps absents de `Blaze.c` : le sort `0x0047e260` et son globe `0x0047e370`. Son projectile est la **flèche magique** du sort 8, déjà écrite : le sort le moins coûteux en travail depuis le début
- doc : **les trois sorts entretenus forment une échelle** — Wonder 2 PM par vingt images, Atchoum 4, Lumière 6. Même lecture de manette, même garde `CanSustainChannel_00475110`, même cadence sur l'horloge du monde `0x005f2cf0` : seul le prix change
- doc : **le globe est un lance-flèches** — sa visée est mot pour mot celle de `TickMagicArrow_0047C910` (`ratan2` sur les écarts, `SquareRoot0` pour la distance à plat, cap dispersé sur douze bits) mais refaite **à chaque image** au lieu d'une seule fois. Trois dwords lui sont prêtés puis rendus — `+0x30` emporte le cap `+0x32`, `+0x04` le modèle — et le globe reprend ensuite son apparence
- feat : **Assaut magique, le quinzième sort** — trois corps absents de `Blaze.c` : le sort `0x0047df70`, sa lame `0x0047e150` (six exemplaires) et son anneau `0x0047e200` (deux). Comme Enchant, il **ne frappe pas** : tout se résout à la soixantième image par `ApplyStatusEffect_0053BE70`, l'entrée 21 de la table des 48
- doc : **le seul sort où le signal de fin remonte** — dès que l'horloge `+0x34` d'une lame tombe sous 8, elle pose `0x2000000` sur le mot d'état de son **père** par `+0x6c`. Le sort n'a aucun compteur : il attend ce signal, se marque `0x1000000` pour ne réagir qu'une fois, et lâche alors ses deux anneaux. C'est l'inverse exact de Wonder, où le père faisait signe à ses fils
- doc : **trois divisions par multiplication magique dans le même corps** — `/60` (`0x88888889` avec la correction `add edx,edi` que réclame un multiplicateur au-delà de 2³¹), `/10` (`0x66666667`) et `/75` (`0x1B4E81B5` avec un décalage de 3 — le même multiplicateur que l'auréole d'incantation, qui l'emploie avec 4 pour diviser par 150)
- feat : **Atchoum, le quatorzième sort** — deux corps absents de `Blaze.c` : le nuage `0x0047db50` et sa bouffée `0x0047de10`. Second sort **entretenu** après Wonder : même lecture de manette, même garde `CanSustainChannel_00475110`, mais **4 PM toutes les vingt images** au lieu de 2
- doc : **trois âges qui ne se ressemblent pas** — images 0 à 7 il se tient au lanceur et laisse tomber une bouffée par image, chacune au **sol** à un écart tiré sur 256 unités ; 8 à 15 il relâche le lanceur ; à partir de 16 il **poursuit** l'adversaire le plus proche, d'un huitième par axe, avec le même lien réciproque que la flèche magique
- doc : **une seule bouffée sur huit peut frapper** — le nuage écrit son propre âge dans le `+0x12` de chaque enfant, et la bouffée ne cherche de victime que si ce champ est **nul**. Seule celle de la première image mord, et la résorption incrémente le champ, ce qui la fait taire aussi. L'âge sert de laissez-passer, pas de garde
- feat : **Congèlo, le treizième sort** — quatre corps absents de `Blaze.c` : le sort `0x0047d600`, la colonne `0x0047d670` (730 octets), le bloc `0x0047d950` et l'éclat `0x0047da50`
- feat : **la file des sons différés a enfin un écrivain** — `0x005dfb90` restait vide et `TickDelayedSounds` tournait à vide depuis sa transcription ; la colonne de glace y met le script `0x00552530` à l'instant où elle finit de monter. Trois craquements espacés de vingt images, relevés au format que le consommateur lit. Le `TODO(REVERSE)` d'`Audio.cs` est levé
- doc : **la teinte de la colonne est son horloge de phase** — tant que `+0x38` n'a pas atteint `0xf0f0f0` (approché d'un huitième par image) elle monte sur place ; l'image exacte où elle y arrive, elle **relâche le lanceur** et met le craquement en file. Ensuite elle balaie : le cap tourne de `0x60` par image mais le déplacement se fait sur le cap **de départ**, ce qui donne une spirale et non un cercle
- doc : **le décor vire au bleu** (`0x40`/`0x40`/`0x80`) à chaque image, le pendant froid de l'orange de Mur de flammes. L'éclatement final sème quatre étincelles de Flêches magiques puis force le compteur à `0x50` — même astuce que Mur de flammes pour mourir par le chemin normal
- feat : **Coup de jus, le douzième sort** — deux corps neufs absents de `Blaze.c` : l'arc `0x0047d330` et son segment `0x0047d470`. Le halo posé sur la victime est celui de Boule de lumière, déjà écrit — troisième sort d'affilée à réutiliser un corps du dépôt
- doc : **la foudre est une polyligne, pas un projectile** — l'arc vit une seule image et **marche** : à chaque tour de sa boucle de trois il tire une déviation, tire une longueur, engendre un segment, puis avance de cette longueur sur le nouveau cap. La déviation est **relative** à la précédente, ce qui fait zigzaguer l'arc au lieu de le courber toujours du même côté
- doc : **la portée d'un segment est sa longueur** — son échelle X vaut `longueur << 12 / 250` (une division par magie `0x10624DD3` puis `sar 4`), et sa sonde s'en déduit : avance `-(échelle >> 5)`, profondeur `échelle >> 4`. Demi-largeur et ouverture suivent en plus les deux échelles retirées **à chaque image**, donc la zone frappée épouse le trait qui grouille
- feat : **Mur de flammes, le onzième sort** — trois corps absents de `Blaze.c` : le sort `0x0047cff0`, sa braise `0x0047d270` (**35 octets**, le plus court corps de sort du dépôt) et sa colonne `0x0047d2a0`. Plus l'atlas `0x00551b40`, borné pour une fois par l'**engendreur** et non par le consommateur
- doc : **c'est Pieds mouillés refait en feu** — même chute, même gravité de `0x6000` en `+0x9a`, même avance de 20 sur le cap. Trois différences : le décor vire à l'orange (`0xc0`/`0x80`/`0x60`, réécrit **à chaque image** et non à la naissance), les enfants naissent éparpillés jusqu'à 127 unités autour du trajet, et le contact avec le décor **n'éteint pas le sort mais force son compteur à `0x28`** — il passe donc par sa fin normale, en frappant une dernière fois et en rendant sa couleur au décor
- doc : **la hauteur de la colonne est son échelle** — `+0x20 = +0x48 + (échelle << 6)`, l'échelle montant huit images puis retombant huit. C'est le seul corps transcrit qui lie ainsi la position à la taille
- feat : **Enchant, le dixième sort** — deux corps neufs seulement, absents de `Blaze.c` : le sort `0x0047ce40` et sa bille `0x0047cf30`. L'embrasement `0x4c` est celui de Feu d'enfer, et l'effet posé était déjà transcrit
- doc : **le premier sort qui ne frappe pas** — aucun de ses corps n'appelle `ResolveSpellHit_0053A2B0` ni ne cherche de victime. Il se résout d'un seul coup à la soixantième image par `ApplyStatusEffect_0053AFB0`, l'entrée 0 de la table des 48 — celle qui n'a **ni masque d'immunité, ni jet de sauvegarde, ni marque visible** : Enchant ne rate jamais
- doc : **la bille est en coordonnées polaires** — sa position n'est jamais intégrée : elle vaut à chaque image `ancre + écart + vecteur(cap, tangage, rayon)`, et c'est le **rayon** qui se resserre d'un quart par image jusqu'à la tuer. Les vingt-quatre billes ne tournent pas — leur vitesse angulaire est posée à zéro : la spirale est collective, pas individuelle
- feat : **Flêches magiques, le neuvième sort** — trois corps absents de `Blaze.c` : le sort `0x0047c910`, la flèche `0x0047ca50` (**1007 octets**, le plus gros corps de sort du dépôt) et son étincelle de traînée `0x00475f60`
- doc : **le premier projectile à tête chercheuse** — la flèche recalcule à chaque image les deux angles vers sa cible et **ajoute** le vecteur à sa vitesse au lieu de la remplacer : c'est une poussée continue, pas une correction de cap, donc elle décrit une courbe et peut dépasser. Son orientation vient de son **déplacement réel**, pas du cap visé
- doc : **la cible est tenue des deux côtés** — la flèche range son adversaire en `+0x78` et s'écrit elle-même dans le `+0x74` de celui-ci ; elle le relâche en mourant, au contact comme sur le décor. Premier sort à marquer sa victime *avant* de la toucher
- doc : **LB-017, un défaut de 1998 de plus** — une flèche dont la cible meurt en vol se réoriente sur **son propre pointeur** : le chemin « cible morte » lit deux fois un emplacement de pile qu'il n'a jamais écrit, celui de son propre argument. Même famille que LB-008 et LB-015, et toutes trois dans des corps que Ghidra n'a pas décompilés — ce sont ceux que personne n'a relus
- feat : **`SetVectorFromAngles_00532AD0`** et sa feuille `ScaleAngleComponents3_00532010` — la version à deux angles de `SetVectorFromAngle_00532B10`, avec l'idiome de division signée du compilateur de 1998 ; et l'atlas `0x00551a08`, relevé dans `DataSymbols.md` depuis le 2026-08-29 et enfin employé
- feat : **Onde de choc, le huitième sort** — deux corps absents de `Blaze.c` : le sort `0x0047c660`, qui reste collé au lanceur treize images, et son anneau `0x0047c830`
- doc : **la première poussée radiale** — les sept sorts précédents poussaient leur victime sur le cap du projectile ; celui-ci calcule l'angle qui va de l'onde **vers** la victime par `Ordinal_457` (le `ratan2` de libgte) et y ajoute un demi-tour. Chaque victime part donc sur son propre angle — vers le centre, le demi-tour renversant le sens
- doc : **l'anneau s'accroche par un écart, pas par une copie** — sa position vaut `ancre + (+0x44, +0x48, +0x4c)`, recalculée à chaque image. C'est ce qui laisse **un seul corps servir les deux anneaux** : celui du sol a un écart nul, celui de tête un écart vertical. Ils ne diffèrent que par six champs posés avant l'engendrement
- doc : le binaire calcule les trois approches d'échelle dans un registre dont les seize bits hauts portent encore le pointeur d'ancre ; seule la moitié basse est rangée, donc le résultat est juste — c'est pourquoi la pseudo-C de ces corps est illisible
- feat : **Ténèbres, le septième sort** — deux corps absents de `Blaze.c` : le sort `0x0047c3a0`, qui lâche **cinq ombres en éventail** sur un quart de tour, et l'ombre `0x0047c450`
- doc : **le premier corps qui annule son propre déplacement** — l'ombre met de côté sa position **et les cinq dwords de `+0x80` à `+0x90`**, l'état de collision que `FinalizeEntityPlacement2` va écrire ; si le contact mord, elle remet les sept valeurs, fait demi-tour et **nie ses deux vitesses**. L'image du choc est exactement celle d'avant, vue à l'envers
- doc : **et le premier dont la portée grossit avec lui** — les mots 2 et 3 de la sonde valent `(échelle >> 7) + 0x1e` et `(échelle >> 6) + 0x1e`, recalculés à chaque image pendant que l'échelle approche `0x3000`. Aucun des six sorts précédents ne faisait varier sa sonde ; sa poussée est aussi le double des autres (`<< 5`)
- doc : **Ténèbres assombrit le décor** — `0x60` sur les trois canaux de fond, là où Wonder les monte à `0xc4`. Le sort ne remet pas le mode à zéro : ce sont ses ombres qui le font en mourant
- feat : **Wonder, le sixième sort** — trois corps absents de `Blaze.c` : le sort `0x0047bf00` (724 octets), son aura `0x0047c1e0` et son anneau `0x0047c2c0`
- doc : **le premier sort entretenu** — passé la seizième image il relit **la manette du joueur qui tient le lanceur** (`s_PlayerSlots_005e9188[indice * 0x4c]` puis `s_InputStateBase_00604a20 + emplacement * 0x10`, comparée au masque `+0x1fc4`) et s'arrête au relâchement. Il coûte **2 PM toutes les vingt images**, cadencé sur l'horloge du monde `0x005f2cf0` et non sur son propre âge
- doc : **le bit `0x2000000` est un signal de fin qui se propage** — le sort se le pose, et ses deux enfants **héritent** des deux bits `0x3000000` de leur père par `+0x6c` à chaque image, puis basculent en résorption d'eux-mêmes. C'est le pendant du compte de références de Rocher : ici le père ne compte pas ses fils, il leur fait signe
- doc : **Wonder peint le fond de l'écran** — la naissance écrit `0x005e9160`..`0x005e9166` (trois canaux à `0xc4`, mode 2), la fin remet le mode à zéro. C'est la couleur de fond que `WorldMap.cs` fait converger à chaque image : le décor blanchit tant que le sort dure
- feat : **`CanSustainChannel_00475110`** — la garde des sorts entretenus : le lanceur doit être vivant, ni mort ni mourant, et sans aucune altération du masque `0x400003e8`. Étourdi ou endormi, on ne tient pas un sort
- feat : **Feu d'enfer, le cinquième sort** — trois corps absents de `Blaze.c` : le sort `0x0047bb50`, sa flamme `0x0047bc40` et son embrasement `0x0047bdf0`. Le quatrième maillon, la braise `0x0047bd00`, **existait déjà et était déjà exercé en forêt** : c'est le premier sort à réutiliser un corps que le dépôt avait
- doc : **le premier sort qui vise** — les quatre précédents partaient devant le lanceur ou tournaient autour de lui ; celui-ci choisit une cible dès sa naissance, la range en `+0x78`, et **tous ses enfants la suivent** par la recopie des `0x27` dwords. Les six flammes et l'embrasement se recollent sur elle à chaque image
- doc : **et le premier à différer son effet** — soixante images après, il recopie sa paire famille/indice sur le lanceur et appelle `ApplyStatusEffect_0053BA90`, l'entrée 15 de la table des 48. Il n'y a pas de nouvelle recherche de cible : c'est celle d'il y a soixante images qui brûle
- doc : **la position vient de la cible, l'altitude du lanceur** — les deux enfants lisent `+0x78` pour X et Z mais mesurent la tête de `+0x74` : la couronne se pose à hauteur d'homme quelle que soit la taille du monstre visé
- feat : **`FindTargetAround_00474BA0`** — la jumelle de `PickTargetBySearch_00474AD0`, même corps à la queue près : elle rend le pointeur au lieu de le ranger, et rend l'acteur lui-même faute de cible. Décompilée dans `Blaze.c`
- feat : **Rocher, le quatrième sort** — trois corps absents de `Blaze.c` : le cercle `0x0047b7a0`, le rocher `0x0047b850` et sa poussière `0x0047bb10`
- doc : **le premier compte de références du dépôt** — le cercle pose `+0x14 = +0x16 = 4`, chaque rocher décrémente `+0x14` du cercle (par son `+0x6c`) en quittant l'orbite et `+0x16` en mourant. Le cercle relâche la marque `0x80` du lanceur quand le premier tombe à zéro, et ne s'éteint que quand le second y arrive : c'est ce qui tient le lanceur immobile le temps du sort
- doc : **le rocher a deux vies en une** — il orbite à 90 unités du lanceur, son orbite perd un huitième de sa vitesse par image, et dès qu'elle passe sous `0x10` il est projeté et vole librement par `IntegrateWithDrag_00535880`. Le bit `0x2000000` sépare les deux, et sert de garde pour que le compte reste juste si un rocher meurt avant d'être lancé
- doc : **le test de sol est inversé entre deux sorts** — Pieds mouillés traite le sol quand `+0x20 >= +0x60`, Rocher quand `+0x20 <= +0x60`. Les deux sont dans le binaire et transcrits tels quels ; `TODO(REVERSE)` posé sur `TickRock_0047B850`
- feat : **l'atlas `0x00551a98`** (quatre tuiles 32×32), borné par la poussière qui pose `+0x40 = compteur >> 1` et s'éteint à 4. La série reprend en `0x00551ab8` avec un **dernier octet différent** : c'est une seconde table, pas la suite
- feat : **Pieds mouillés, le troisième sort** — trois corps absents de `Blaze.c` : la vague `0x0047b460`, sa crête `0x0047b6b0` et son éclaboussure `0x0047b750`
- doc : **le premier sort sans graine, et le premier qui tombe** — le sous-type 2 *est* la vague, là où les deux précédents naissaient d'un corps d'une seule image. Elle porte une vitesse verticale en `+0x54`, une gravité de `0x6000` réécrite à chaque image en `+0x9a`, et **colle au sol** ou se marque `0x800` selon la distance parcourue pendant le placement
- doc : **la crête ondule par sinusoïde amortie** — son échelle Y vaut `|sin(+0x12 << 8)| * (0x1000 − âge * 0x200) >> 11`, et le `+0x12` vient de la vague : chaque crête naît un cran plus loin dans l'onde. Le binaire calcule la valeur absolue en **appelant la table deux fois** ; la table étant une lecture pure, c'est écrit comme la valeur absolue qu'il calcule
- doc : **l'éclaboussure n'a pas de compteur, c'est sa teinte qui lui sert d'horloge** — elle part de `0xf0f0f0`, perd `0x101010` par image, et le test de fin est une **égalité exacte à zéro** : elle ne tient que parce que les trois canaux partent de la même valeur et descendent du même pas
- doc : **les trois champs de vitesse sont prêtés à l'enfant** — la vague écrase `+0x50`/`+0x54`/`+0x58` avec une impulsion tirée au hasard, laisse la recopie des `0x27` dwords l'emporter, puis les rend. Même geste que `CastSpell_0053A980` sur `+0x74`/`+0x78` : passer un argument par un champ, faute de paramètre
- fix : **le relevé `0x00551ba8` de Boule de lumière doublonnait `s_Descriptors_00551BA0`** — deux tables pour les mêmes octets, et surtout l'indexation en avant n'aurait plus rien trouvé au-delà du second. Les deux atlas se lisent désormais par décalage dans la plage
- feat : **Boule de lumière, le deuxième sort** — quatre corps, tous absents de `Blaze.c` : la graine `0x0047b0b0`, la boule `0x0047b150`, son étincelle `0x0047b340` et son halo `0x0047b3a0`. Le socle `ResolveSpellHit_0053A2B0` a servi tel quel : le deuxième sort n'a coûté que ses propres corps
- doc : **la boule de lumière ne meurt pas au contact** — là où Balle de feu s'éteint dans la branche du coup, celle-ci y retombe sur le vieillissement commun : elle traverse et peut frapper à chaque image de ses douze. Le binaire n'a aucune garde contre le coup répété
- doc : **le halo se recolle, il ne se déplace pas** — il reprend à chaque image la position de l'entité de `+0x78`, qui est la boule d'abord puis **la victime** dès le premier contact. Sa taille est celle de la tête de son ancre, `(hauteur << 12) / 150`, la même division par `0x1B4E81B5` que l'auréole d'incantation
- feat : **l'atlas `0x00551ba8`** (deux tuiles 24×24). Les deux autres atlas du sort, `0x005519a0` et `0x00551a60`, étaient déjà relevés — et le halo les borne à trois exactement comme leur premier consommateur
- fix : **la section des tables statiques de `GestionnairesEntites.md` affichait « 0 testés » quoi qu'il arrive** — ses lignes portent zone −1 / sous-zone −1 alors que la sonde note toujours une zone réelle, si bien que le rapprochement par triplet ne pouvait jamais aboutir. 274 gestionnaires étaient en angle mort ; **28 étaient déjà exercés**. Une table gravée dans `.data` est la même dans les onze zones : l'avoir vue passer quelque part suffit
- feat : **l'étincelle `0x0047b050`**, le troisième maillon de Balle de feu — sous-type `0x46` de `0x00552230`, 91 octets, absente de `Blaze.c`. Huit images, quatre carreaux d'atlas à deux images chacun : c'est **son corps qui borne** `0x00551ad8` à quatre enregistrements, mieux que le changement de planche
- doc : **Balle de feu est lancée en jeu et va au bout** — graine, projectile, étincelle. Première ligne de `sorts-exerces.tsv`
- feat : **Balle de feu, le premier sort** — la graine `0x0047add0` (sous-type 0, table statique `0x00551c30`) et le projectile `0x0047ae80` (sous-type `0x64`, table `0x00551f30`), tous deux **absents de `Blaze.c`** et transcrits au désassembleur. Relevés gardés sous `Legacy/Raw/Blaze/`
- feat : **`ResolveSpellHit_0053A2B0`, la résolution du coup d'un sort** — le pendant magique de `ResolveMeleeHit_00539B80`, et le socle que les 79 sorts restants partagent. Attaque en `+0x38` divisée par `fiche[0x1e]`, défense en `+0x3a`, jet de sauvegarde au lieu du coup critique, et l'élément de la fiche qui **efface toutes les autres cases élémentaires des deux côtés**
- doc : **le quart de tour de la graine se compense** — elle ajoute `0x400` à `+0x32` avant de lâcher le projectile, qui le retranche avant d'avancer : la balle part bien droit devant, le détour ne sert qu'au dessin
- doc : **le projectile vole à hauteur de tête** — `FinalizeEntityPlacement2` le poserait au sol, et le binaire sauve puis rend `+0x20` autour de l'appel : le placement ne sert qu'à la détection du décor
- feat : **l'atlas `0x00551ad8`**, les quatre étincelles que la balle traîne — borné par le changement de planche à `0x00551af8`
- fix : `0x0047add0` était marqué « testé » dans `handlers-exerces.tsv` alors qu'il n'était pas implémenté ; sa ligne est retirée, ce « testé » ne prouvait que le plantage
- feat : **`SortsAtteignables.md`, la feuille de route de la magie** — le troisième document de la famille, après `GestionnairesEntites.md` et `ObjetsAtteignables.md` : les 123 fiches de sort du CD une par une, avec leur combinaison, leur coût, le corps qu'elles font naître, et les deux colonnes `implemente` / `teste`. Bilan de départ : **80 sorts d'école, 74 corps distincts, aucun écrit**
- feat : **une troisième sonde, sur les sorts** (`sorts-exerces.tsv`) — accrochée à `CastSpell_0053A980`, elle relève la paire famille/indice avec la zone et la sous-zone. Elle ne distingue pas le lanceur : incantation, script d'objet et entité de scène entrent par la même fonction, et c'est la fiche qu'on vérifie
- feat : **`census-spells.sh`** croise les fiches du CD, la table de sauts statique de `Blaze.exe`, les `case` du répartiteur et les deux sondes. Comme pour les objets, **aucune partie n'est jouée** pour produire le document
- feat : **le verbe `bestiaire` écrit `sorts.csv`** — les huit familles de la table `+0x9c`, lues en rejouant hors ligne la chaîne de chargement. Le compte des trois écoles jouables (29 / 24 / 20) recoupe exactement `DAT_00564600`, ce qui valide la lecture des bornes de liste
- doc : **un sort fait la même chose où qu'on soit** — aucune des 123 fiches n'emploie un sous-type `≥ 0xc0` : tout passe par la table statique `0x00551c30`, comme pour les objets. Et c'est **presque un corps par sort** : 74 corps distincts pour 80 fiches, pas un répartiteur à branches partagées
- doc : **la combinaison d'un sort n'est pas faite de directions mais des quatre boutons de face** — le masque testé est `0xf000`, soit triangle, rond, croix et carré dans `s_PadButtonMasks_00564f18`, pas la croix directionnelle. `Sorts.md` disait le contraire depuis le 2026-08-29
- doc : **l'école 3 n'est pas vide, elle est herboriste** — le CD y porte sept plantes (Lavande, Sauge, Verveine, Mercuriale, Fenouil, Hysopius, Belladone), toutes sur un seul corps `0x0047a5d0` paramétré par `+0x12`. `DAT_00564600` n'en déclare qu'une : l'herboristerie du Chasseur a été écrite puis débranchée, pas jamais remplie
- doc : **le nom des sorts est relevé** — ce sont les seize premiers octets de la fiche, et ils sont en clair dans le CD français. Les quatre familles au-delà des écoles gardent, elles, leurs noms d'origine en katakana : ce sont vraisemblablement les sorts des monstres

## 2026-08-30
- feat : **`tests/objets.bat`**, le pendant de `feuille-de-route.bat` pour `ObjetsAtteignables.md` : il regenere, puis reaffiche le compte, les gestionnaires a ecrire, la dette et le controle croise
- doc : **`objets.csv` ne porte pas l'equipement de depart** — decouvert par le controle croise des la premiere partie : la sonde a releve `Epee normale`, `Armure cuir` et `Bouclier cuir`, absents du catalogue. Le bestiaire dumpe les catalogues **par sous-zone**, pas ce que la creation de personnage accorde. Le rapport signale desormais ces angles morts tout seul
- fix : **la sonde d'objets ecrivait un BOM** — `Encoding.UTF8` en pose un, si bien que la premiere ligne ne commencait plus par `#` et que l'en-tete etait comptee comme une cle d'objet. `UTF8Encoding(false)` cote jeu, et le lecteur le retire par precaution
- feat : **une seconde sonde, sur les objets** (`objets-exerces.tsv`) — accrochée à `RunItemScript_00523AA0`, elle voit les deux scripts. `ObjetsAtteignables.md` porte désormais la même lecture en deux colonnes que `GestionnairesEntites.md`, sur les 21 gestionnaires **et** sur les 607 objets un par un : **369 pleinement implémentés**
- doc : **la clé de la sonde d'objets est l'hexa de la fiche, pas le nom** — les noms d'objet ne sont pas de l'ASCII (le disque français porte encore des fiches en katakana demi-chasse), et une clé lisible sortait en mojibake. Le recensement recalcule la même clé depuis `objets.csv` pour retrouver le nom
- feat : **`census-item-effects.sh`, la liste de courses des objets** — il déroule hors ligne les deux scripts des 2 370 fiches d'objet du CD et sort `ObjetsAtteignables.md` : **21 gestionnaires atteignables par un objet, 3 écrits, 18 à transcrire**, triés par nombre d'emplois. Plus besoin de découvrir par plantage
- doc : **le dérouleur a prédit les deux plantages de la séance avant qu'on les rencontre** — la Petite potion se décode en opcode 17 puis sous-type `0x40`, exactement dans cet ordre. C'est ce qui autorise à croire le reste du relevé
- doc : **un objet fait la même chose où qu'on soit** — sur les 1 440 naissances relevées dans les scripts, **zéro** emploie un sous-type `≥ 0xc0` : tout passe par la table statique `0x00551c30`, commune aux onze zones. Les dix-huit corps restants n'ont donc pas de variante par carte
- doc : **une potion ne soigne pas par la table des 48 effets** — elle soigne par l'**opcode 6**, l'unité arithmétique, sur `+0x4c` du bloc de la cible. La table des 48 est celle des altérations d'état ; seuls 13 de ses 48 genres sont atteignables par un objet
- feat : **la bouffée de l'objet utilisé** (`0x00446790`) et **son étincelle** (`0xa9` → `0x0044fb10`) — les deux corps que le script d'une potion fait naître. L'étincelle tire tout son état au hasard en sept appels au générateur, et son `+0x14` sert successivement de borne au rayon puis de durée de vie
- doc : **utiliser un objet ne demande pas un corps par objet** — les effets visibles viennent des **tables statiques** (274 gestionnaires pour les onze zones, 49 écrits), pas des tables de zone. Le plafond est 225, pas 1238, et le même corps sert la potion, le sort et le piège
- doc : **l'étendue de `0x00551528` se déduit de la signature** — les quatre octets de queue valent `01 02 10 00` sur trente-cinq enregistrements puis changent : c'est la borne, aucun corps ne borne l'indice
- feat : **la table des 48 effets** (`0x0056d4e0`, `0x0053afb0`..`0x0053cdf0`) — les 43 qui manquaient sont transcrites, cinq l'étaient déjà par les pièges de coffre. C'est le sous-système que les objets et les sorts partagent : altérations d'état, levées, guérisons, résurrection, mort immédiate
- feat : **la recherche de cible** (`0x00474ad0`) — le mode 0 balaie **le même camp**, le mode non nul celui d'en face. Elle pose le bit `0x2000` sur l'acteur le temps du balayage pour qu'il ne se rende pas lui-même. Prendre une potion dans le menu rapide passe maintenant par un chemin entier
- doc : **une souche qui décrit son propre coût vieillit** — celle de `0x00474ad0` annonçait un sous-arbre de cinq fonctions à ouvrir ; les cinq avaient été écrites entre-temps par le chemin du combat, et il ne restait que vingt lignes à transcrire
- doc : **`FUN_00538870` a deux sorties écrites par adresse** — après l'appel, `param_2` n'est plus la victime mais le bloc de l'assaillant, et `param_3` n'est plus le drapeau mais celui de la victime. C'est l'idiome à reconnaître pour lire n'importe lequel des 48 effets
- doc : **`+0x06` de l'entité est le passage de main entre les opcodes 14 et 18** du script d'objet — le 14 y range la paire famille-indice, le 18 la relit pour aller chercher dans la fiche le montant de base. C'est là que vit le nombre de points qu'une potion rend, et c'est pourquoi la « fiche de sort » sert aussi aux objets
- doc : **la division par 24 des effets 3 à 6 ne se lit que dans le binaire** — Ghidra en rend les trois morceaux sans les recomposer ; `imul 0x2AAAAAAB ; sar edx,2 ; shr eax,31 ; add` est la division signée que `/` fait déjà en C#
- feat : **le verbe 13 de la forêt** (`0x004433b0`, 1150 octets, absent de `Blaze.c`) — « tout le groupe est-il rassemblé sur l'un des quatre emplacements ? ». **La zone 3 n'a plus de verbe manquant** : les treize de la table `0x0056aec8` sont couverts
- doc : **deux clairières, deux emplacements chacune** — quatre réunions de trois rectangles, la sud en `z` −200..780, la nord en `z` 2020..3000, toutes entre `x` −4200 et −3220. Dans une clairière les deux emplacements découpent le même sol dans deux sens perpendiculaires
- doc : **le réamorçage des emplacements est croisé** — désarmer l'un remet l'**autre** de la paire à l'état prêt, jamais lui-même. Un appariement direct aurait fermé l'énigme au lieu de la faire basculer
- feat : **`0x0049e190`, la troisième intelligence d'entité de scène** — 572 instructions, absente de `Blaze.c`, relevée au désassembleur. Zone 3 à **121 sur 136**. Même découpage en trois temps que ses deux sœurs, mais c'est bien un autre corps : comparée par la seule forme des instructions, elle en diffère sur 416 lignes de 672
- feat : **elle lance des sorts**, ce qu'aucune des deux autres ne fait — sa posture 1 attend l'image 7 de l'animation puis appelle `CastSpell_0053A980` avec la paire famille-indice rangée en `+0x06`, et sa posture 3 tire cette paire dans une table de trois mots par espèce (`0x00552978`)
- doc : **le délai d'après-sort ne vient pas d'un tirage mais du répertoire** — l'octet `+0x13` de la fiche du sort, atteinte par `DAT_005e97c4 + 0x9c` puis la famille puis l'indice, au pas de `0x30`. C'est le sort lui-même qui dit combien de temps la bête reste immobile après l'avoir lancé
- doc : **aucun doublon parmi les vingt routines d'intelligence** — les neuf restantes comparées aux onze déjà branchées donnent vingt empreintes distinctes. Il n'y a plus de raccourci dans cette famille
- feat : **sept gestionnaires de la forêt de plus**, la zone 3 passe de 113 à **120 sur 136**. Il ne reste que les seize plus grosses, dont les neuf routines d'intelligence des espèces
- feat : **le verbe « joue l'animation de la statue »** (`0x00443b20`) — cinq phases gouvernées par le drapeau de script 208, dont l'état vit dans le bloc privé de la forêt et survit d'une image à l'autre. L'animation avance **une image sur deux**, et seulement passé la huitième
- feat : **le geyser et sa partition** (`0x0040ef40`) — un jet qui s'étire plus haut que large (`0x9c4` contre `0x800`), puis ondule au sinus. Sa partition est en dur : neuf notes, deux accords de trois montantes suivis d'une basse. Elle tourne à chaque image **sauf une**, celle du changement d'état
- feat : **la braise qui dérive** (`0x004101d0`) — elle part sur la **deuxième** image d'atlas, sa vitesse est projetée une fois pour toutes sur le cap, et elle grossit de trois paliers en vieillissant
- feat : **le stroboscope rouge** (`0x00413330`) — il ne dessine rien : il pousse le canal rouge du voile plein cadre à `0x1e` une image sur deux. Il colle à son porteur, meurt avec lui, et **rend l'écran à sa couleur** en partant
- feat : **la charge qui se matérialise, frappe et s'efface** (`0x00483760`) — la teinte monte de `0x80808` par image jusqu'au blanc plein, et c'est l'image où elle y arrive qui porte le coup ; ensuite elle redescend du même pas jusqu'à zéro
- feat : **le débris qui retombe en clignotant** (`0x00459840`) — trajectoire balistique, gravité de `0x1000` par image, et un écho de cinq sons dont le gain décroît, écrit non pas en comparaisons mais en **table de sauts** : treize octets d'indices pointant six entrées
- feat : **le pilier tournant à quatre faces** (`0x00459fa0`) — son orientation vit sur **deux bits d'un drapeau de script**, quatre champs dans un octet, un par pilier. C'est pour cela qu'elle survit à une sauvegarde : elle n'est pas dans l'entité, elle est dans l'état de la partie
- feat : **`FUN_00474ca0` transcrite**, l'aiguillage des deux sondes de mêlée — le bit `0x800000` décide du camp : posé, on balaie les entités de scène ; absent, on balaie le groupe. Ghidra y avait perdu les arguments, le désassemblage montre qu'ils sont empilés **une seule fois** avant le branchement et servent aux deux appels
- doc : **`DAT_005de0b8` n'est pas « a-t-on trouvé quelqu'un » mais « le coup porte-t-il »** — c'est la parade qui introduit la distinction : la sonde rend bien la cible mais remet le drapeau à zéro. Tester le retour au lieu du drapeau ferait passer les parades pour des coups réussis
- feat : **les deux scènes cinéma de la forêt et leurs deux amorces** (`0x00410820`, `0x00413c90`, `0x00411240`, `0x004146b0`) — quatre créneaux, deux corps, la zone 3 passe à **113 sur 136**. Les deux exemplaires de chaque scène ne diffèrent que par la **base du bloc privé** : l'enregistrement 0 (`0x00571420`) contre le 1 (`0x00571450`)
- feat : **la scène muette** (`0x00410820`) — désarmement des quatre emplacements, attente qu'ils soient tous posés, détournement de la caméra vers le bloc privé sur un cadrage tiré au sort, puis 202 images : les deux voiles montent de dix et de trois par canal, la pose de fin est posée à l'image `0x9b` — celle où l'écran est le plus clair, donc celle où le changement ne se voit pas — les voiles redescendent, et le script 1 part sur le membre visé
- feat : **la scène parlante** (`0x00411240`) — le même squelette plus cinq sons, une boîte de dialogue ouverte à l'image 200 sur la fiche du membre visé, un script lancé **sans entité**, et un changement de musique optionnel que les deux amorces connues n'activent jamais
- feat : **`FUN_00423d10` et `FUN_0040b0e0` transcrites** — quinze et quarante lignes de `Blaze.c`. La seconde est l'attente : chaque membre tombe dans un des trois cas — à préparer, hors jeu, encore occupé — et la scène n'avance que lorsque les quatre portent le bit `0x4000000`
- doc : **les trois raisons qui bloquaient ces scènes étaient toutes fausses** — le bloc privé est modélisé depuis hier, et les deux appelées « manquantes » sont dans `Blaze.c` depuis le début. Ce qui les avait mises de côté, c'est de ne pas avoir vérifié
- doc : **la table de pose de fin relevée** — seize couples (pose, image) choisis par les trois bits bas de l'octet d'identité `+0x150`, le bit 3 départageant les deux moitiés. Aucun n'est calculable à partir de la classe : c'est une table, écrite en toutes lettres dans chacune des deux scènes
- feat : **onze gestionnaires de la forêt de plus**, la zone 3 passe de 98 à 109 implémentés sur 136 — dont **cinq verbes de zone** : l'effet `0x59` posé sur tout le groupe (`0x004432a0`), la classe du locuteur rangée dans la banque 2 (`0x004431f0`), « tout le groupe est-il sous le niveau N ? » (`0x00443830`), le réveil de la scène (`0x00444e60`) et la boîte de dialogue attachée au membre que la banque 2 désigne (`0x00443300`)
- feat : **le panache qui enfle et s'éteint, en deux exemplaires** (`0x00415d40`, `0x004157a0`) — première famille de la forêt dont les membres ne diffèrent **par aucune donnée** : le diff normalisé tient en une ligne, et c'est un `mov [eax+0Bh],cl` que le compilateur a émis deux fois de suite
- feat : **le halo accroché à son porteur** (`0x0040f260`) — le halo de la gerbe, mais qui suit son porteur en hauteur et dont la planche de couleurs est une constante ; il meurt au temps, deux cents images, pas à la teinte
- feat : **le décor qui range sa pose et la reprend** (`0x00458de0`) — `+0x30`/`+0x32`/`+0x34` n'y sont pas une orientation mais une **position de rechange**, et le binaire teste deux fois de suite la même globale sans que rien ne la modifie entre les deux
- feat : **la dalle qui s'enfonce sous le poids** (`0x00459b20`) — elle descend tant qu'un membre est dessus, remonte d'un coup sinon, et pose un drapeau de script qu'elle ne retire jamais
- feat : **le décor qui flotte et replonge quand on le lâche** (`0x00458e90`) — le bit `0x40000000` y partage le corps en deux comportements qui ne se rencontrent jamais ; l'enfoncement ralentit (neuf unités, puis huit, puis sept) avant que la remontée ne le recolle net à sa hauteur de repos
- feat : **`0x00483920`, trois instructions**, et **`0x00444e60`, les trois premières boucles de `0x0040e640`** — le bloc partagé est maintenant écrit une fois et appelé des deux endroits
- fix : **`0x00571420` élargi de `0xf0` à `0x240` octets** — le verbe `0x00443b20` écrit à `0x0057150c` et `0x00571510`, et `0x00458de0` lit `0x00571654` qu'aucune source décompilée ne nomme. Le pas de `0x30` est net sur les premiers enregistrements mais leur **nombre n'est pas établi** : tout ce qui vit dans ce domaine passe désormais par `ForestSceneField`, adresse par adresse, plutôt que par un compte inventé
- doc : **`BindMenuLabels` tronque son sujet** — il le range sur quatre octets (fidèle au x86) mais les quatre sites qui le relisent le relisent en pointeur, donc sur huit. Les 46 sites qui passent `(uint)(nint)pointeur` rendent un sujet tronqué. Une surcharge à `byte*` est ajoutée et employée par le nouveau verbe ; la migration des 46 autres reste à décider
- doc : **la taille d'une fonction ne se mesure pas au premier `ret`** — `0x00458e90` en a un au milieu de son corps, et `0x0040ef40` aussi. La mesure juste est « le premier `ret` qu'aucun saut interne ne dépasse » ; six des tailles relevées la veille étaient fausses, dont `0x00458200` donnée pour 232 instructions alors qu'elle en fait 569
- feat : **vingt-deux gestionnaires de la forêt d'un bloc**, la zone 3 passe de 76 à 98 implémentés sur 136. Aucun n'était décompilé dans `Blaze.c` ; tous relevés au désassembleur
- feat : **les braises qui montent, en quatre exemplaires** (`0x00412330`, `0x00412810`, `0x00415860`, `0x00415e00`) — deux familles de deux : le cap est tiré une fois et ne tourne jamais, c'est le rayon qui grandit, et la famille B **rougit** en perdant un de plus sur le vert et le bleu
- feat : **la projection large, en quatre exemplaires** (`0x004156b0`, `0x00415c50`, `0x00412240`, `0x00412720`) — un carré parfait, deux planches croisées avec deux valeurs de `+0x0a`, aucune autre différence sur 77 lignes
- feat : **l'invocation qui emporte un membre, en quatre exemplaires** (`0x00411f80`, `0x00412460`, `0x004153f0`, `0x00415990`) — encore un carré : le « rythme » déplace ensemble le début de la fanfare, sa fin, la tenue d'image et les **trois** sous-types semés
- feat : **l'éclat posé sur une corde d'arc, en trois exemplaires** (`0x0040fbb0`, `0x00412d10`, `0x00416300`) — 520 instructions, la plus grosse lacune du bloc `0x0040`/`0x0041`, et une seule ligne d'écart entre les trois. Il ne se pose pas sur l'arc mais **quelque part sur le segment qui joint deux directions** prises à ±61 et ±114 degrés du cap
- feat : **la fumée violette aux trois rôles** (`0x004103d0`, `0x00413840`) — spirale rentrante, spirale sortante, bouffée qui monte ; son rôle 2 tire `+0x12` **deux fois**, le premier tirage ne servant qu'à avancer le flux de l'aléatoire
- feat : **la gerbe aux cinq phases** (`0x00411860`, `0x00414cd0`) — `+0x14` n'y est pas un état mais un **rôle**, et le corps se sème lui-même dans quatre rôles différents en prêtant ce champ le temps d'un appel. Ses phases 2 et 3 **repeignent la planche de couleurs en place**, dans l'enregistrement d'atlas lui-même
- feat : **le flash blanc de sortie de zone** (`0x00410370`, `0x004137e0`) — gel, voile qui monte de 8 pendant trente images puis redescend de 6, quatre sons dont un compteur retient lesquels ont servi, et retour des trois pointeurs de caméra sur la vue de marche
- feat : **`0x00483920`, le plus court gestionnaire du jeu** — trois instructions, `*(uint*)e = 0`. Il efface le mot de drapeaux **en entier**, là où les autres se contentent de `&= 0x7fffffff`
- doc : **un troisième cas de figure derrière les lacunes** — après les corps dupliqués et les corps jamais décompilés, l'**amorce** : six à dix-neuf instructions qui posent trois champs puis sautent dans un corps que Ghidra a, lui, parfaitement rendu. Six des vingt-deux gestionnaires de ce lot étaient de celles-là. Vérifier où pointe le `call` d'une amorce coûte deux minutes
- fix : **`0x00571420` modélisé d'un seul bloc** — cinq exemplaires de `0x30` octets. Les deux mémoires d'emplacement `0x00571448` et `0x00571478` avaient d'abord été déclarées comme deux octets isolés ; ce sont le `+0x28` des exemplaires 0 et 1, et les laisser séparés aurait posé le défaut d'aliasing que `MemoryLayoutTests.cs` surveille
- doc : **deux planches déjà relevées pointaient au milieu d'un bloc partagé** — `0x0054f8f0` est le *deuxième* enregistrement de `0x0054f8e8`, dont un autre gestionnaire ne lit que le premier. La règle « relever la plage, pas le descripteur » a payé pour de bon : relevés au plus juste, ces quatre enregistrements n'auraient laissé aucune trace
- doc : **plus aucun doublon parmi les 37 gestionnaires restants de la zone 3** — les 37 empreintes normalisées sont toutes distinctes. Les familles sont épuisées ; ce qui reste se transcrit un par un, les quatorze routines d'intelligence en tête
- doc : **deux défauts de 1998 relevés et reproduits** — la gerbe aux cinq phases passe comme descripteur un tampon de pile jamais rempli (inoffensif, tous les corps d'effet posent leur planche à l'état 0) ; l'invocation lit `[ebp+0x16]` par un registre laissé non initialisé quand aucune entité de scène ne répond au balayage
- feat : **l'éclaboussure du pas existe elle aussi en trois exemplaires** (`0x0040f7f0`, `0x00412b30`, `0x00416120`) — 77 instructions comparées, **une seule ligne diffère** : l'adresse de la planche. Sixième famille de la forêt bâtie ainsi
- feat : **`0x004a5f10` est un duplicata de `0x00486ee0`** — 299 instructions, zéro différence
- feat : **la convocation, en deux exemplaires** (`0x00410cd0`, `0x00414140`) — quatre effets dans un corps, dont un **chef** qui se téléporte, lâche trente-et-une spirales rentrantes, s'éclaircit quarante-trois images, puis redescend en semant une bouffée par image avant de lâcher trente-et-une spirales sortantes et de s'éteindre. Les enfants reçoivent leur variante parce que la naissance **recopie l'entité entière** : le chef pose `+0x14` juste avant chaque appel et le restaure après
- feat : **`0x004593c0` est un duplicata du tourniquet** (`0x00457770`) — 293 instructions comparées une à une, **zéro différence**, et les deux pseudo-C de `Blaze.c` sont identiques ligne pour ligne
- doc : `0x00410cd0` et `0x00414140` sont **encore le même corps**, à quatre adresses de planche près — trois variantes sur `+0x14`, deux planches chacune. Quatrième occurrence de ce motif dans la forêt
- feat : **le tourniquet de la forêt** (`0x00457770`) — deux états superposés, un délai qui suspend tout, une rotation qui accélère d'un huitième pendant douze images puis freine du même huitième pendant vingt-huit. Sa phase de retour **corrige son propre arrondi** en rendant le dépassement au cap. Verrouillé, il affiche « ça ne s'ouvre pas ! » (`0x00551648`) ; ouvert, l'étiquette que la sous-zone désigne
- feat : **l'ascenseur de la forêt** (`0x00458fc0`) — deux courses dans un corps, choisies par `+0x44` figé à la première image. Il part quand le compte des membres rattachés égale le groupe **moins ceux déjà arrivés**, et redescend si l'un d'eux tombe de plus de `0x28000`. Son toutes les cinq images pendant la course, une fois plus grave à l'arrêt
- doc : **`0x00410820` et `0x00413c90` mis de côté** — deux cinématiques qui détournent trois pointeurs de caméra vers un bloc privé non modélisé, et dont deux appelées (`FUN_00423d10`, `FUN_0040b0e0`) manquent. Ouvrir cette chaîne est un travail en soi
- feat : **`0x004134c0`, deux effets dans un seul corps** — `+0x5c` n'est pas un état mais un aiguillage. Variante 0 : une onde qui s'ouvre par huitièmes et **tient le compte des membres qu'elle a touchés** dans un masque de quatre bits que rien n'efface. Variante 1 : une étincelle qui rejoint `0xfa` par moitiés en six images, puis retombe par huitièmes en vingt-et-une
- feat : **l'explosion de la forêt** (`0x00483650`) — six éclats en couronne dont le sous-type **vient de la sous-zone**, un noyau qui écrase tout ce que la couronne avait hérité, une secousse d'écran amortie en une ligne (`(n >> 2) - n`) et un script de onze sons différés relevé à `0x00552638`. Tout à la première image, dix images de vie
- feat : **les deux mécanismes de décor de la forêt**, miroirs l'un de l'autre — `0x00459c20` monte en accélérant jusqu'à `0x4af` puis clignote et se fige, `0x00459dc0` tombe en accélérant tout en tournant et basculant, sur deux vitesses tirées au départ. Tous deux dorment jusqu'à ce qu'un bit de la banque de script 0 les arme, et accompagnent leur course d'une série sonore dont le gain se divise par deux à chaque étape
- feat : **l'éclat qui jaillit existe lui aussi en trois exemplaires** (`0x0040f470`, `0x00412950`, `0x00415f40`) — les trois pseudo-C ne diffèrent que d'une ligne, celle qui pose la planche. Corps écrit une fois, planche en paramètre
- feat : **`0x00459a70`, le troisième rocher qui roule** — compteur de sa sœur maigre, extinction de sa sœur riche, masque `0x8880`, le plus permissif des trois
- feat : **`0x0049f0c0` est un duplicata relocalisé de l'Homme-lézard** (`0x0049d030`) — 648 instructions comparées une à une, une seule différence : l'adresse de sa propre table de sauts, dont les cinq cibles sont décalées de `0x2090`, exactement l'écart entre les deux fonctions. Un `case` au lieu d'un corps de 1 800 octets
- feat : **la bulle existe en trois exemplaires** (`0x0040fac0`, `0x00412c20`, `0x00416210`) — même corps émis trois fois, mêmes cinq descripteurs dupliqués à trois adresses. Corps écrit une fois, planche en paramètre
- feat : **`0x00457e20`, le rocher qui roule** — sœur maigre de `0x00456a30` : même déplacement avec collision, mais son compteur s'arrête à zéro au lieu de se recharger
- feat : **`0x00458070` est un doublon octet pour octet de `0x004566c0`** — trois caractères hexadécimaux d'écart, le déplacement du `call`. Un `case` suffit, aucun corps à écrire
- feat : **`0x0040f650`, la gerbe reculée** — la ligne du Z que Ghidra avait *inventée* (`param_1 + iVar5 * -0x28`) récupérée au désassembleur : c'est le cosinus de `cap + 0x400`, rangé dans un mot de pile et relu vingt lignes plus bas. Trois états, dont une attente
- feat : **trois verbes de zone** — `0x00443960` (combien de membres debout, rangé dans une variable de script), `0x004439a0` (le compte de neuf drapeaux comparé à l'argument), `0x00443a00` (**la porte** : cinq états qui ouvrent puis referment l'animation d'une entité de sous-type 3)
- feat : **quatre effets de la forêt d'un bloc** — `0x0040f190` (l'étincelle grise qui retombe), `0x0040f470` (l'éclat qui jaillit, mort à deux portes dont une aléatoire), `0x0040f8e0` (la gerbe large, vie fixe de 24 images), `0x0040fac0` (la bulle dont la vitesse sert d'horloge). Tous décompilés dans `Blaze.c`
- doc : leurs quatre planches relevées **entières** (`0x0054f6f8`, `0x0054f778`, `0x0054f7c8`, `0x0054f7f8`) — trois d'entre elles contiennent plusieurs groupes dont un seul est atteignable
- feat : **le fondu au blanc de la transition** (`0x0040edf0`, table `0x0056b0c8`, forêt) — un compte à rebours de 220 images : rien pendant 170, montée des voiles pendant 25, descente pendant 25, puis extinction et retour de la main au porteur
- doc : **le rôle de `0x005de024` et `0x005de028` est établi** — ce sont deux **voiles plein cadre**, trois octets de couleur et un octet de commande. `GameMemory.cs` les disait « non établi » : on n'en voyait que la lecture, jamais l'animation
- feat : **les deux verbes les plus employés du jeu** (`0x004457f0` et `0x00445840`, 78 emplois chacun) — vingt-huit octets, deux effets sonores, `return 0`. Le 8 est celui de la porte. C'est exactement ce que le classement par emplois devait faire ressortir
- fix : **la sonde ignorait le répartiteur de verbes** — `ScriptOpAreaVerb` est le second point d'entrée d'une table de sauts, et les 67 verbes de `0x0056aec8` ne pouvaient donc jamais passer à « testé ». Le verbe 8 avait planté sans laisser de trace dans le TSV
- feat : **la poussière du bloc poussé** (`0x0040f370`, sous-type `0xfd` de la forêt) — quatre images d'atlas tenues 5 à 9 images chacune, teinte sombre `0x3c3c3c` qui s'assombrit d'un cran par image. Décompilée dans `Blaze.c` ; la plage `0x0054f720` relevée sur sept enregistrements, deux groupes
- doc : **règle** — après chaque implémentation, régénérer `GestionnairesEntites.md` ; et si le gestionnaire était `implémenté=non, testé=oui`, retirer d'abord ses lignes du TSV. Ce « testé » ne prouvait que le plantage
- feat : `tests\feuille-de-route.bat` — régénère `GestionnairesEntites.md` d'un double-clic et réaffiche l'avancement par zone plus les gestionnaires vus en jeu mais pas écrits. Il cherche `bash.exe` dans le PATH puis aux emplacements habituels de Git pour Windows, absent du PATH `cmd` sur cette machine
- feat : `GestionnairesEntites.md` devient une **feuille de route groupée par zone**, avec deux colonnes indépendantes — *implémenté* (un `case` le branche) et *testé* (la sonde l'a vu tourner). La combinaison « non / oui » désigne la priorité : le jeu y va vraiment et plante en y allant
- fix : la sonde écrivait l'adresse sur **six** chiffres alors que le recensement la lit sur huit — aucun gestionnaire n'aurait jamais été marqué EXERCÉ. Format aligné, fichier existant normalisé
- doc : premier relevé réel — **20 exercés, 93 écrits jamais appelés, 1 317 absents**, zones 0:8 et 3:0
- fix : **la sonde n'écrivait rien** — elle ne s'accrochait qu'à `ProcessExit`, que la terminaison par exception non rattrapée ne lève pas. Or c'est précisément le cas d'un gestionnaire absent, le plus intéressant à relever. Elle écrit maintenant dès qu'un couple est nouveau, et signale chaque nouveauté sur stderr
- feat : **la sonde de couverture** — `DispatchEntityHandler` note chaque gestionnaire réellement appelé en jeu et fusionne le relevé dans `Documentation/Etat/handlers-exerces.tsv` à la fermeture. Un corps qui compile n'est pas un corps éprouvé ; le dépôt distingue désormais les deux
- feat : **`census-entity-handlers.sh`** — le recensement complet des dix tables de sauts, croisé avec le répartiteur et la sonde. **1 430 gestionnaires distincts atteignables** : 112 écrits, 1 318 à transcrire, 0 encore exercé. Sort `Documentation/Etat/GestionnairesEntites.md`
- feat : **la carte suivante ne plante plus** — sous-type 29 (`0x00456a30`, 692 o.), une entité qui roule, garde sa position dans six octets de l'arène et désigne le membre le plus proche
- feat : **`MoveSceneEntity_00455B30`**, le déplacement avec collision que partagent **seize** sites d'appel. Décompilée mais mal recouvrée : second argument perdu (lu à `[esp+0x40]`, d'où l'`unaff_retaddr` de Ghidra) et bloc de restauration apparié de travers sur onze champs — résolu au désassembleur
- doc : `0x00545a10` n'est qu'un thunk d'import vers `Ordinal_442`, la racine carrée de winsys3d
- feat : **le rocher qui donne un titre** — verbe 5 de la table de zone, `0x004438a0`, absente de `Blaze.c` (179 octets). Il convertit le compteur de mises à mort (`bloc+0x15c`) en un rang de 0 à 13 sur treize seuils doublants, et le publie en `bloc+0x1ff8`, l'indice de libellé que le pavé de statistiques relit
- fix : **les trois modes de mélange ADDITIFS n'étaient pas portés** — le shim n'acceptait que `(5, 6)` SRCALPHA/INVSRCALPHA et rendait opaque `(2, 2)`, `(5, 2)` et `(3, 2)`, que `FUN_00a03c80` demande pour tout effet lumineux. D'où les nuages de la carte du monde en taches sombres et l'onde de choc en croissant noir
- doc : `winsys3d.md` — la table des cinq couples `SRCBLEND`/`DESTBLEND` de `FUN_00a03c80`, et les deux garde-fous de capacité (tramage, puis abandon de la primitive) qui la précèdent
- fix : **l'onde de choc du Guerrier est noire parce que TOUTE la semi-transparence était désactivée** — le slot `+0x40` du périphérique synthétique rendait zéro par la souche neutre, or il répond « suis-je le matériel Direct3D ? » ; le jeu en concluait « rastériseur logiciel » et effaçait le bit `0x200` de tout modèle transparent, depuis toujours
- doc : `3dhelp.md` — le slot `+0x40` est le getter du champ `+0xA4`, posé par comparaison du GUID du pilote avec `IID_IDirect3DHALDevice` ; c'est lui qui commande `DAT_00564a48`, relu dans une trentaine d'endroits du rendu
- doc : **troisième relevé `TRACE_FX`** — la texture de l'onde est une planche `32x32` dont les texels échantillonnés sont transparents ou **presque blancs** (`0xEEEEEE`), sans aucun noir opaque : la chaîne complète est correcte et l'onde devrait se peindre en blanc
- feat : le relevé écrit la texture liée en **PNG** dans `scratchpad/` et compte les texels opaques par seuils de clarté — le compte par égalité à zéro ne mesurait pas une couleur
- doc : **deuxième relevé `TRACE_FX`** — la couleur de l'onde arrive **intacte** jusqu'à Direct3D (`0x787878` émis, `0xFFF0F0F0` reçu : le doublement de winsys3d marche), et les indices de couleur des seize faces tiennent dans les trois entrées remplies. Par élimination, le noir vient des **texels**
- feat : le relevé décrit maintenant la texture liée — dimensions, texel échantillonné, partage transparents / noirs opaques — et les quatre coordonnées du dessin
- doc : **premier relevé `TRACE_FX`** — la chaîne `Blaze` est confirmée juste jusqu'à la primitive (teinte `0x787878` décroissante, code `0x24`, CLUT `0x7382`) ; piste ouverte : `mot8=0x00010006` ne remplit que **trois** entrées de couleur pour un modèle de **seize** faces
- fix : le relevé côté shim visait à côté — `DrawOTag` dessine en fin d'image, pas à l'émission. Il trie maintenant sur la diffuse non blanche au lieu de la proximité temporelle
- feat : **`tests\trace-fx.bat`** (`OPENFORESIA_TRACE_FX=1`) — suit la couleur d'un effet de bout en bout : ce que `Blaze` émet, puis ce que `winsys3d` en rend au backend. L'onde de choc reste noire malgré une chaîne `Blaze` vérifiée fidèle ; ce relevé dira si le noir vient de la diffuse ou des texels
- fix : **l'onde de choc du Guerrier ne touchait presque jamais** — les deux mots de portée de sa sonde (`EntityHandler_004767E0`) étaient rangés en `sonde[0]`/`sonde[3]` au lieu de `sonde[2]`/`sonde[5]` : demi-largeur et profondeur nulles, quadrilatère de frappe réduit à un point
- doc : **winsys3d double les trois octets de couleur d'une primitive et les borne à `0xff`** — `0x808080` est donc du blanc plein, pas du gris ; convention PlayStation conservée, relevée dans `winsys3d.md`
- feat : **l'éclaboussure du pas dans l'eau** — `0x0040f7f0`, absente de `Blaze.c`, relevée au désassembleur (231 octets). Sous-type `0xfe` de la table installée par zone, semé un pas sur deux par le crochet de contact au sol dès que le sol porte `0x200` : c'est le plantage à l'approche d'un point d'eau
- doc : les quatre images de l'éclaboussure relevées à `0x0054f7a8` — quatre tuiles `8 x 8` aux coins d'un carré de `0x10`, bornées par `DAT_0054f7c8`

## 2026-08-29
- fix : **les statistiques de départ des classes ne sont pas dans `Blaze.exe`, elles sont sur le CD** — l'écran de création les résout par `**(int **)(DAT_005e97c4 + 0x74) + classe * 8` (`FUN_00509470` à `0x005094e0`). `DAT_0056c7e0`, la table du binaire, n'est lue que par l'écran de groupe de mise au point. Un Guerrier démarre à **13** de force, pas 24 ; relevé par Vincent à l'écran, retrouvé dans `BLAZE.ALL` à `0x0090f0b4` où le motif n'apparaît qu'une fois sur 46 Mo
- doc : **les sept caractéristiques portent enfin leur nom** — FO, INT, VO, AGI, CON, PM, CH. Les physiques 0, 3, 4 sont FO, AGI, CON et les mentales 1, 2, 5 sont INT, VO, PM ; les profils de classe le confirment un par un (Nain CON 15, Sorcier PM 14, Fée CH 16)
- fix : **la classe chanceuse est la Fée (CH 16), pas le Voleur (12)** — la déduction inverse raisonnait sur la table de mise au point
- fix : **l'élément se choisit** (`FUN_00508e00`, sélecteur gauche/droite masqué `& 7` sur `+0x151`), il ne découle pas de la classe. La huitième colonne de la table du CD est nulle partout
- doc : `MonteeDeNiveau.md` **entièrement recalculé sur la vraie table** — les huit tableaux par classe, les huit cas limites, l'amplitude des gains, les probabilités, le recul de la chance et la courbe des plafonds sur la plage réelle (6 à 16). Espérances recoupées contre 2 000 parties simulées par classe, estimations de niveau contre 300
- doc : **en partie ordinaire, aucun plafond n'est jamais atteint** — il faut 107 à 233 niveaux aux valeurs de classe, contre « vers 110 » sur la table de mise au point. Ce que la création achète n'est donc pas un plafond mais de la vitesse, sauf répartition extrême
- doc : **la Fée est la classe la plus rapide du jeu** (7,78 points par niveau contre 6,78 pour le Guerrier), et la seule dont une caractéristique bute avant le niveau 110. Elle le paie par ses deux 6 en FO et CON, bornés à 158
- doc : **l'élément est le seul choix de création qui achète du plafond gratuitement** — un cinquième sur une ligne, soit 44 points sur 221, sans toucher au pool
- doc : **les huit éléments sont nommés** — Feu, Eau, Terre, Air, Lumière, Nuit, Divin, Maléfique, relevés dans `BLAZE.ALL` à `0x0090e325`. Ils poussent respectivement FO, INT, CON, AGI, VO et PM d'un cinquième ; Divin et Maléfique poussent les six d'un vingtième. L'ordre est recoupé par la huitième colonne de `DAT_0056c7e0` — Prêtre → Divin, Voleur → Nuit, Kobold → Terre, Chasseur → Air : les huit tombent juste
- feat : le simulateur nomme les éléments dans sa liste déroulante et **montre la ligne poussée** — la colonne « plafond » affiche la prime (`265 (+44)`), en gras et à la couleur de la courbe, et le panneau du bas donne le total rapporté
- doc : **Divin et Maléfique rapportent plus de plafond au total** (54 points sur un Guerrier) qu'un élément focalisé (35 à 44) — un vingtième sur six lignes bat un cinquième sur une seule
- doc : **l'élément ne fait qu'une chose chiffrée, le plafond** — ses trois autres lecteurs sont la teinte de fond de l'écran de sélection (`DAT_00562a58 + élément*8`), la couleur du nom (`élément*3`), et un masque `(1 << élément) | 0x800` posé dans `bloc+0x14` que **rien ne relit** : les huit masques d'immunité qui interrogent ce champ ont tous l'octet bas nul et le bit `0x800` éteint, et le seul test à masque variable est appelé avec `0xffffffff`
- doc : **les noms de classes du jeu ne sont pas ceux du projet** — `BLAZE.ALL` porte Guerrier, Prêtre, Voleur, **Magicien**, Chasseur, Elfe, **Kobold**, **Lutin** au masculin (`0x0090dea4`) et Guerrière, Prêtresse, Voleuse, Magicienne, Chasseresse, **Sylphide**, **Naine**, **Fée** au féminin (`0x0090def0`). Les trois derniers couples sont des mots différents, pas des accords
- fix : le bouton « Tout remettre aux valeurs de classe » du simulateur passait sous le cadre suivant — le cadre 1 portait une hauteur en dur trop courte de 24 pixels. Les deux cadres la calculent maintenant depuis leur contenu (`AjusterHauteur`), donc ajouter une ligne ne peut plus rogner la dernière
- feat : **`OpenForesia.Tools montee` — le simulateur de montée de niveau**, une fenêtre autonome (`tests\montee.bat`) : on compose un personnage comme à la création (classe, pool de 15 + 3d6, sept lignes bornées à 99), on choisit un niveau visé et une graine, et l'outil rejoue `FUN_005391f0` niveau par niveau. Sortie : neuf courbes avec leurs plafonds en pointillés, le tableau des écarts par niveau et une synthèse par caractéristique
- doc : le simulateur recoupe `MonteeDeNiveau.md` chiffre pour chiffre — gains moyens du Guerrier 2,61 / 1,12 / 2,20 contre 2,59 / 1,13 / 2,22 annoncés, plafond culminant à 510 pour un départ de 80 à 83 et retombant à 490 à 99, chance à 28 % / 41 % / 32 % contre 27 / 40 / 33
- fix : **le plafond de caractéristique se calcule sur les valeurs de DÉPART (`+0x2a0`), pas sur les courantes** — `psVar9 = (short *)(param_2 + 0x2a0)` juste avant la boucle. Il est donc constant pour toute la partie, et la limite de 157 annoncée la veille était fausse : Vincent a signalé en jeu un Nain de niveau 110 à **496 de force**
- doc : **la courbe des plafonds culmine à 510 pour un départ de 80 à 83, et redescend** — un départ de 99 plafonne à 490, soit 20 de moins pour 19 points de plus. Le seul endroit du système où dépenser davantage donne strictement moins
- doc : **les points de création achètent des plafonds, la chance achète de la vitesse** — reformulation qui sépare enfin les deux répartitions. Une caractéristique laissée à 8 ne dépassera jamais 177, quel que soit le nombre de niveaux
- doc : **le Voleur ne progresse pas mieux par nature** — à chance égale sa famille (56 % partout) est derrière celle du Guerrier et du Nain (74 %/45 %). Ses 24 de chance de départ le ramènent tout juste à égalité ; tout son avantage est dans ce champ
- doc : **l'amplitude visible d'un gain (« +1 » puis « +4 ») vient de DEUX tirages** — l'un dimensionné par la caractéristique, l'autre par la chance, plus un `+1` fixe. Tableau des fourchettes ajouté
- doc : MonteeDeNiveau.md — tableaux par classe refaits avec le plafond réel de chaque caractéristique, et les cas limites rejoués sur trois répartitions (99 dans la signature, 99 en chance, 81 dans la signature)
- doc : **`Documentation/Reference/Sorts.md`** — le système de sorts de bout en bout : les quatre classes lanceuses, les trois écoles réelles, la fiche de `0x30` octets et ses huit champs, l'apprentissage à la montée de niveau, la combinaison de directions, le coût en PM. Ajouté à la carte de `CLAUDE.md`
- doc : **la montée de niveau est le SEUL moyen d'apprendre un sort en jeu normal** — un unique écrivain du masque `+0x160` dans tout `Blaze.c`. Ni parchemin, ni maître, ni objet
- doc : **la triche « Gurkha » apprend tout en ouvrant le menu** — le listeur de sorts pose le bit de chaque sort qu'il affiche quand `DAT_005633a8` vaut 0, ce qui n'arrive qu'après la séquence. C'est le second et dernier écrivain du masque
- doc : **certains sorts ne s'apprennent jamais** — les paliers s'arrêtent sous le compte réel des écoles : 26 sur 29, 22 sur 24, 16 sur 20. Leur combinaison est pourtant reconnue par le balayage d'incantation
- fix : **l'Elfe est plafonnée au palier 3, pas 2** — son niveau est ramené à 80 au-delà de 109, et 80 franchit encore le troisième seuil. Elle s'arrête à 20 sorts là où le Sorcier, même école, en atteint 26. Le commentaire de `TryLearnSpell` disait « palier 2 »
- doc : **MonteeDeNiveau.md complété** — un tableau par classe (probabilité, gain moyen et espérance par caractéristique), les bornes de l'écran de création (pool de 15 + trois dés, relancer sur triple, 99 par ligne), et les cas limites « 99 dans la signature » contre « 99 en chance » pour les huit classes
- doc : **deux probabilités seulement peuvent saturer à 100 %** — le physique du Guerrier/Nain à partir de 68 de chance de départ, le mental des lanceurs à partir de 78. Aucune autre combinaison n'y arrive, les hybrides plafonnant à 84 %
- doc : **les points dépensés entre deux multiples de 8 n'accélèrent pas la progression** — le gain tire dans `rand() % ((départ >> 3) + 1)`, donc 16 et 23 donnent exactement le même tirage
- doc : **`Documentation/Reference/MonteeDeNiveau.md`** — ce que le jeu calcule à une montée de niveau, de bout en bout : le palier, les sept gains, les trois familles de probabilités par classe, le plafond calculé et la chance. Écrit sur demande de Vincent, ajouté à la carte de `CLAUDE.md`
- doc : **les gains d'une montée sont dimensionnés par les statistiques de DÉPART**, pas par les courantes — `+0x2a0` est la copie figée à la création, et rien ne la réécrit jamais. Répartir ses points à la création accélère donc la progression pour toute la partie
- doc : **le plafond de caractéristique est une courbe qui redescend** (`v/8 - v²/16 + (v+10)*10`) et croise `v` vers **157** : la borne dure de 999 n'est jamais atteinte, la progression s'éteint d'elle-même
- doc : **les huit classes se rangent en trois familles de progression** — Guerrier/Nain (74 % physique), Prêtre/Sorcier/Fée (69 % mentale), Voleur/Chasseur/Elfe (56 % des deux). Le même regroupement apparaît ailleurs dans le jeu
- fix : `DAT_0056d308` est indexée par **l'élément** (`+0x151`), pas par une sous-classe — le champ est rempli depuis la huitième colonne de `s_ClassBaseStats_0056c7e0`. `s_SubClassFavouredStat_0056D308` renommée `s_ElementFavouredStat_0056D308` : l'ancien nom était une supposition
- fix : le commentaire de `LevelUpCharacter_005391F0` affirmait qu'un coup pouvait faire gagner plusieurs niveaux. **La jauge est remise à zéro à l'intérieur de la boucle** et le palier vaut au moins `0x30` : le second tour ne repasse jamais le test, et le surplus d'expérience est perdu
- feat : **la montée de niveau ne plante plus** — `FUN_004038F0` (`0x3b`, le grain qui monte en hélice) et `FUN_00403A90` (`0x59` de la table `0x00551f30`, la gerbe qui s'allume puis s'éteint), plus l'atlas `0x0054f158`. La logique de montée elle-même était déjà écrite : il ne manquait que ses deux entités visuelles
- doc : **le même `0x59` désigne deux corps** — `RestoreVitalsEntity_00403710` dans `0x00551c30` et `FUN_00403A90` dans `0x00551f30` ; le père lâche le fils sous son propre numéro, seule la fonction d'engendrement les distingue
- doc : `FUN_00403A90` **n'a pas de compteur d'âge** : ses deux seuils et sa mort lisent le **vert seul**, dont la vitesse est tirée au hasard — chaque grain vit une durée différente et garde sa propre couleur
- doc : `FUN_004038F0` **tire son échelle deux fois** et la seconde écrase la première ; le premier tirage ne sert qu'à consommer un nombre de la suite pseudo-aléatoire. Transcrit tel quel, la suite étant partagée
- doc : TODO — **tous les pièges de coffre ne sont pas testés**. Trois vus sur douze ; les autres, les trois applications d'effet `0x0053B4B0`/`B580`/`B660` et le cas d'un piège résolu par la table de sous-zone restent à exercer en jeu
- feat : **les douze pièges de coffre `0xb4`..`0xbf` sont tous transcrits** — la famille close d'un coup au lieu d'un corps par plantage. Six naissances (`0x00475A30`, `0x00475B90`, `0x00475760`, `0x00476730`, `0x00475170`, `0x004753D0`), sept enfants, trois applications d'effet (`0x0053B4B0`, `0x0053B580`, `0x0053B660`) et le second jet de sauvegarde `0x0053A790`
- doc : **le coffre nomme son piège par l'octet haut de son `+0x34`, plus `0xc0` replié sur huit bits** — `0xf4`..`0xff` retombent sur les entrées `0xb4`..`0xbf` de la table statique, `0x01`..`0x3f` vont dans la table de sous-zone. C'est ce qui borne enfin le nombre de pièges
- doc : **toute la famille a la même charpente** — une naissance qui retient sa victime en `+0x78` et lâche un enfant, l'enfant qui tient la durée puis emprunte `0x005ede20` et applique un effet. Cinq effets voisins (`7`, `8`, `9`, `10`, `0x1d`) écrivent cinq durées consécutives `+0x8e`..`+0x94`, `+0xe0`
- doc : **deux horloges sans compteur** — `0x00475BE0` meurt quand son mot de teinte atteint exactement zéro (`0xf0f0f0 / 0x080808 = 30`) et `0x0047A880` de même (`/ 0x101010 = 15`). Aucune constante ne nomme la durée : elle tient parce que la division tombe juste
- fix : **le relevé de `0x005519b8` était décalé d'un enregistrement** — trois tuiles `00d0`, `10d0`, `20d0` et non `20c0`, `00d0`, `10d0`. Deux tables (`0x005519a0` et `0x005519b8`) se partagent une seule plage contiguë de six, chacune bornée par son propre consommateur
- feat : **la branche « effet d'état » close de bout en bout** — `FUN_00475910` (`0x3f`), `FUN_0053B3C0` (l'application d'un effet d'état), `FUN_00475D40` (`0xbf`, le coup qui ne passe pas) et `FUN_004459E0` (`0x99`, la poussière). Plus aucun corps non transcrit sur le chemin d'un coffre piégé
- fix : **le coffre piégé ne plante plus du tout** — la naissance `0xb6` menait à `0x3f`, qui menait à toute la chaîne restée ouverte depuis la suite 10
- doc : **`+0x44` de la victime décide de l'aspect de la poussière** — `FUN_004459E0` a deux moitiés exclusives, une pour `+0x44 == 0` (violet, une image) et une pour `+0x44 == 1` (cyan, quatre images). C'est ce que `0x0053c420` mettait à 0 et `0x0053b3c0` met à 1 : le champ que le commentaire de la suite 12 disait « non établi »
- doc : `FUN_0053b3c0` est `FUN_0053c420` à cinq valeurs près — événement, masque d'immunité, champ de durée, bit de `+0x40`, atlas. Les durées d'état sont un bloc de mots consécutifs `+0x8e`, `+0x90`, `+0x92`, `+0x94`, une par effet
- doc : trois atlas de plus relevés — `0x005515b8` (1 image), `0x005515c0` (4), `0x005519b8` (3) — et le script de son `0x00551868`. Les trois se bornent par le corps de leur consommateur, dont un plafond `si l'index atteint 4, remettre 3`
- feat : **le second piège de coffre** — `FUN_004754C0` (`0xb4`) transcrite, avec sa gerbe `FUN_004755C0` (`0x3c`) et son atlas `0x00551ab8`. Seize images, une gerbe à chaque image, puis 5 % des points de vie de la victime
- fix : **le coffre piégé ne plante plus une fois sur deux** — le tirage du coffre désigne `0xb4` ou `0xb5`, et seul `0xb5` était écrit
- doc : `0xb4` ne s'emprunte **pas** d'assaillant, à la différence de `0xb5` : il passe par `ApplyPercentDamage_005397B0`, donc sans jet de sauvegarde — le coup porte toujours
- doc : **la vitesse de la gerbe se pose sur le père** — `0x004754C0` écrit son propre `+0x50/+0x54/+0x58` juste avant l'engendrement, et la copie des `0x27` mots la transmet à l'enfant ; `FUN_004755C0` ne fait qu'intégrer ce qu'elle trouve déjà écrit
- fix : **prendre l'objet d'un coffre ne plante plus** — le corps de `0x005312D0` était transcrit depuis longtemps (`StoreItemInInventory_005312D0`), c'est le `case` du `switch` de `UiText.RunTextEngine` qui manquait. Cinq lignes, pas 734 octets : la suite 14 s'était trompée de diagnostic
- doc : **devant une levée qui nomme une adresse, la chercher dans `OpenForesia/src` AVANT `Blaze.c`** — le suffixe VA des noms reconstruits la donne en une ligne. Le message de la levée porte maintenant cet avertissement
- doc : `FUN_005312D0`, **la remise d'objet** (rappel de fin de texte du coffre) — 734 octets analysés et relevés, pas encore transcrits. Ses trois appels inconnus sont identifiés comme `StoreImage` / `MoveImage` / `DrawSync` de winsys3d, **déjà câblés** ; `FUN_00532710` est la remise d'emplacement dans la pile libre du butin
- doc : **lire un import par ordinal** — le dword du slot IAT porte le bit haut, ôter `0x80000000` donne l'ordinal à chercher dans le dump d'export de la DLL. C'est ainsi que les trois thunks `0x00545xxx` ont été nommés
- doc : **le filet `AI_STUB` ne couvre pas les rappels du moteur de texte**, seulement les gestionnaires d'entité — un coffre accepté tue le run dans les deux modes. Le `switch` de `RunTextEngine` est le seul endroit à toucher si on veut l'y étendre
- feat : **la branche `0xbe` close d'un coup** — `FUN_00475EA0` (la naissance signalée en frappant un scarabée), plus ses deux enfants `FUN_00475FC0` (`0xbe`) et `FUN_0047BD00` (`0x68`), tous deux des feuilles franches
- doc : `FUN_00475FC0` **vole en repère relatif** — un décalage sur une ancre `+0x70`, et sans ancre elle s'ancre à elle-même, ce qui rend le calcul identité. C'est ainsi que l'original écrit « repère absolu » sans dupliquer le code
- doc : `0x00551ba0` **n'est pas une animation** mais une réserve de descripteurs de planches différentes ; sa longueur n'est pas établie, chaque engendreur y désigne son départ et borne par `+0x1a`
- doc : **lister les cibles de `call` avant d'écrire** — c'est ce qui a montré que les deux enfants étaient des feuilles et permis de fermer la branche entière au lieu de s'arrêter au corps signalé
- feat : **le piège de coffre** — `FUN_004755F0` (`0xb5`) transcrite, avec sa gerbe `FUN_00475730` (`0x3d`), son atlas `0x005519d0` et son script de sons `0x00551858`. Huit images, puis le coup part
- feat : **la chaîne d'application des dégâts** — `FUN_005387c0`, `FUN_00538830`, `FUN_00538870`, `FUN_0053a6c0` (le jet de sauvegarde) et `FUN_0053c420` (l'effet de piège). Elle débloque aussi `FUN_0053b3c0`, donc `0x00475910` laissée ouverte en suite 10
- feat : `Audio.QueueDelayedSoundScript` (`FUN_0053dbe0`) et le curseur d'anneau `0x005dfb8c` — la file des sons différés a enfin son écrivain
- feat : les deux entités visuelles du coup, `FUN_00475400` (`0x29`) et `FUN_00445950` (`0x3a`)
- doc : **une borne « d'un emplacement de trop » qui n'en était pas une** — le piège se fabrique un assaillant à `0x005ede20`, l'adresse suivant la table de scène, et `ValidateCombatants_0053A860` l'accepte *parce que* sa borne haute est inclusive. L'adjacence est le mécanisme, pas un débordement
- doc : **mesurer la profondeur réelle avant de renoncer** — la chaîne des dégâts avait été écartée deux fois sur son apparence ; six de ses dépendances profondes étaient déjà écrites, il ne restait que cinq corps courts
- doc : **la chance ne touche pas le tirage de rang du butin** — elle mène deux tirages voisins : la probabilité qu'un objet tombe plutôt que de l'argent (`odds + chance/2` sur mille) et l'état de l'objet (`0x3c + chance/2`, plafonné à 95). Le jeu sépare **quel** objet (écart de niveau seul), **si** il tombe et **dans quel état**
- doc : `+0x2ac` lu comme la **chance** sur une déduction assumée — septième champ du bloc `+0x2a0`, il vaut 16 pour les huit classes sauf le **Voleur à 24**. Aucun libellé du binaire ne le nomme
- doc : **« plage 100 » n'est pas « 100 % de chance »** — la plage est le dénominateur du tirage, les huit rangs se partagent toujours 100 %. Tous les rangs sont atteignables dès **+41** ; les trois derniers niveaux ne font que porter le rang 7 de 1 % à 4 %. Précisé dans `LISEZMOI.md` et sur la page
- fix : **la loi du butin était fausse dans le bestiaire** — « une plage de 56 ne dépasse jamais le rang 4, les trois derniers rangs hors d'atteinte ». C'est le rang **2**, et les **cinq** derniers. Le tirage est `rand() % plage`, donc le maximum est `plage - 1` = 55, et le rang 3 demande 57. Corrigé dans `LISEZMOI.md`, le gabarit et la page régénérée
- doc : l'écart de niveau du butin est **borné en bas à zéro** : être sous-niveau donne exactement la même distribution qu'être à niveau. Il n'y a pas de malus, seulement un bonus qui commence à **deux** niveaux d'avance, et le rang 7 demande **41** niveaux d'écart
- feat : `FUN_004834D0` transcrite — l'enfant de `FUN_00483450`, un **projectile qui roule et rebondit** sur le plancher `+0x60`, et qui ne survit qu'aux images où son balayage ne touche personne. Il jette délibérément la hauteur que `FinalizeEntityPlacement2` calcule
- doc : **la table de sous-zone observée de bout en bout** — `0xc0 + 0x1f = 0xdf` en forêt désigne `0x004834D0`, une adresse qu'aucune table statique ne pouvait donner. La prédiction de la suite 10 vérifiée sur pièce
- doc : son intégrateur n'est **pas** `FUN_00535880` — frottement `1/8 + 1/4096` au lieu de `1/8 + 1/256`. Écrit en clair plutôt que factorisé
- feat : **les trois lacunes de la seconde série de runs**, toutes décompilées et toutes de la même forme — `FUN_00476770` (`0xba`), `FUN_004758C0` (`0xb6`) et `FUN_00483450` (`0xdf`) : des **naissances qui s'effacent**, une entité fille et `*e = 0`. Avec elles trois feuilles : `FUN_00531fe0`, `FUN_00532b10` et `FUN_00535880`, l'intégrateur à frottement
- feat : `FUN_004767E0` transcrite au désassembleur — **c'est elle qui porte le coup** du `0xba` : elle avance, grossit, balaie le camp d'en face et impute les dégâts au lanceur rangé en `+0x74`
- doc : **le père et le fils portent le même sous-type `0xba`** et sont deux corps différents — `SpawnCastEntity_0047AAE0` dépose dans `0x005edee0`, dont la table de sauts est `0x00551f30` et non `0x00551c30`
- doc : `FUN_00483450` engendre `0xc0 + n`, **au-delà des 192 créneaux statiques** : son enfant est toujours résolu par la table de sous-zone. La règle « un sous-type ne veut rien dire tout seul » y est structurelle, pas accidentelle
- doc : `+0x9a` est une **gravité par entité** — `FUN_00535880` n'amortit que l'horizontale ; ce que `0x00476770` lance a sa gravité mise à zéro et ne tombe donc pas
- doc : `0x00475910` (`0x3f`) laissée ouverte à dessein — elle appelle `FUN_0053b3c0`, l'application des dégâts, qui est un sous-système et mérite sa propre pièce
- feat : **trois corps de plus sur le chemin de la frappe**, tous absents de `Blaze.c` — `FUN_00404310` (la retombée du `0x34`), `FUN_00403C10` (l'effet `0x50` qui suit sa cible et la mesure selon son genre) et `FUN_00403D60` (la bouffée `0x32` qu'il sème). La chaîne est close : aucun des trois n'appelle plus rien de non transcrit
- fix : `s_DustDescriptors_0054F118` portait les octets de `0x0054f120` — le grain de poussière pointait une image trop loin depuis sa transcription. Découvert en lisant la table voisine `0x0054f0e0`, vérifié au verbe `at` puis à `od`
- doc : `0x0054f0e0` relevé — sept images, **la même séquence que `0x00551a08` dans une autre planche** : mêmes sept déplacements, seule la moitié haute change (`1717` contre `1818`)
- doc : **la première récolte par récapitulatif et non par plantage** — la sortie en forêt de Vincent sous `filet.bat` a rendu les deux lacunes du parcours *et* leur fréquence (3272 et 1203 appels), là où une levée n'en nomme qu'une
- feat : **`FUN_004041C0` transcrite — la sœur sombre de `FUN_00402FC0`**, entrée 82 de la table `0x00551c30`, absente de `Blaze.c` : elle tient sa victime, la **noircit de quinze par image** jusqu'au noir, lui impose l'animation 2 défilée à la main et lâche une entité `0x34` par image. Levée signalée en jeu par Vincent en frappant
- doc : **le même sous-type `0x52` désigne deux gestionnaires différents** — `0x0047A270` (la trajectoire du jet) par la table `0x00551f30`, `0x004041C0` par la table `0x00551c30`. La règle « un sous-type ne veut rien dire tout seul » vérifiée sur deux corps écrits à une heure d'intervalle
- feat : `tests\filet.bat` — le jeu normal sous `OPENFORESIA_AI_STUB=1`, pendant de `banc.bat` : une lacune se journalise en `[LACUNE]` au lieu de tuer le processus, et le récapitulatif sort à la fermeture. C'est le raccourci pour « ça plante quand je frappe, qui manque ? »
- doc : le transfert `AI_BENCH=<zone>:<sous-zone>` meurt encore en `PlaceWorldMapParty` (`0xC0000005`) **une partie chargée**, points d'entrée 0 et 2 confondus — la garde sur `s_PartySize_005e9020` ne couvrait que l'écran-titre. L'agent n'a donc aucun moyen de mettre des monstres devant le groupe sans conduire le personnage ; voir `Documentation/Tests/test-protocol.md`
- feat : **la chaîne de combat tient de bout en bout, observée en jeu le 2026-08-29** — apparition, intelligence, parade, coup du joueur, dégâts, mort, butin, expérience, montée de niveau. Les monstres bloquent et attaquent. Quatre corps manquaient sur ce chemin ; ils sont écrits
- feat : **`FUN_0049A360` transcrite — la Coccinelle**, sous-type 1 du bloc A de la forêt, quatrième routine d'intelligence écrite et **première bête volante**. Avec elle deux auxiliaires : `FUN_0049a8e0` (intégration du mouvement sans le rappel au sol) et `FUN_004a1cf0` (interruption, deux masques de moins que sa sœur terrestre)
- doc : **la Coccinelle est reconnue en jeu** — elle vole en oscillant et fonce en prenant de la hauteur, conforme au souvenir qu'a Vincent du jeu de 1998. Première routine d'intelligence dont le **comportement** est confirmé, et pas seulement la compilation
- feat : **`FUN_0049C8E0` transcrite — le Poisson volant**, sous-type 6 du bloc A, cinquième routine d'intelligence. **Il ne vole pas : il bondit** — il appelle l'intégration terrestre `FUN_00488160`, et le tri par cette fonction, écrit la veille dans la recette, l'a dit en une ligne
- doc : **il s'accroche**, ce qu'aucune des quatre précédentes ne fait — la position est recopiée de la victime plus un écart rangé en `+0x44`/`+0x48`/`+0x4c`, morsure toutes les `0x1e` images, et **le joueur s'en débarrasse en remuant** : la routine lit son pad et n'use le compteur de prise que sur les images où une direction est tenue
- doc : **aucune table de sauts** — l'aiguillage est une cascade de comparaisons, trois postures, ni repos ni errance. L'étape 2 de la recette ne s'applique donc pas partout : chercher d'abord un `jmp dword [reg*4+…]`
- doc : **ne rien prédire à partir du nom de la bête.** J'avais annoncé un calque de la Coccinelle, moins cher ; c'est une terrestre, et la plus riche des cinq. Trois lectures d'une minute décident : fonction d'intégration, table de sauts, sonde
- feat : **`FUN_0049C0B0` transcrite — le Crabe géant**, sous-type 5 du bloc A, sixième routine d'intelligence. Terrestre et proche parente de `0x0049a980` : mêmes quatre postures, mêmes cônes de virage, même replanification à deux branches près
- doc : **elle a deux attaques, une première** — la pince (posture 0 en geste) balaie le gabarit à chacune des images 12 à 15, donc jusqu'à quatre coups dans un seul geste ; la ruée (posture 1) est une machine à trois temps portée par `+0x10`, qui verse `(hardiesse + 0x380) << 7` d'un coup dans ses vitesses
- doc : **la ruée ne porte qu'à moitié**, et c'est la première modulation de dégâts rencontrée : la routine divise par deux l'attaque du bloc de **modèle** (`+0x30`) le temps de l'appel à `FUN_00539b80`, puis la remet. Le détour par le modèle partagé rend l'astuce invisible à la lecture de la résolution seule
- doc : **lire le geste avant la posture.** La charpente d'une routine se recopie d'une sœur de même milieu ; ce qui coûte, c'est ce que ses gestes font. Deux écarts de constante relevés au passage : sa marche ignore la vitesse rendue par la lecture de situation, et le compte à rebours de la ruée vaut `(timidité >> 8) + 8`, donc toujours 8
- feat : **`FUN_0049B840` transcrite — le Grizzly**, sous-type 4 du bloc A, septième routine d'intelligence et jumeau du Crabe. Les quatre postures tenues et les trois plages de replanification se sont recopiées mot pour mot ; seuls les deux gestes ont demandé du travail
- doc : **son bond quitte le sol** — vitesse verticale `0x24000`, bit « en l'air » allumé, et son geste se ferme à l'**atterrissage** et non sur un compte à rebours. Il écrit en outre sa gravité (`+0x9a = 0x6000`) à chaque image, ce qu'aucune routine précédente ne faisait
- doc : **ses deux gabarits de portée sont dans `.data`** (`0x00552960` la griffe, `0x0055296c` le bond), un cas nouveau — les six précédentes les construisaient sur la pile. La griffe porte `probe[4] = -0x100` : elle s'abat d'en haut, le bond frappe au ras du sol. Relevé dans `DataSymbols.md`
- doc : **la demi-attaque n'était pas une singularité du Crabe** — le Grizzly divise la même valeur du bloc de modèle autour de `FUN_00539b80`. C'est un idiome de la famille, à chercher dans les suivantes
- doc : **la recette gagne un « tri à trois lectures »** avant d'écrire une ligne : fonction d'intégration, présence d'une table de sauts, et **d'où vient la sonde** — un `push <adresse>` avant `call 00519C30` veut dire qu'elle est dans `.data`
- feat : **`FUN_0049B220` transcrite — l'Abeille**, sous-type 3 du bloc A, huitième routine d'intelligence et deuxième bête volante. Sept des vingt-deux routines d'IA de la forêt sont écrites, dont sept des onze du bloc d'arrivée
- doc : **sa posture 3 est un trait, pas une marche** — `+0x43` y sert de machine à deux temps : à 2 elle verse `(timidité + 0x120) << 8` dans ses vitesses, à 1 elle attend que la vitesse retombe sous `hardiesse / 8 + 1`, et à chaque image elle s'oriente plein cap sur la cible sans cône. Le vol d'insecte : des traits rectilignes séparés par des arrêts face au joueur
- doc : **sa poussée se prend sur la timidité et non sur la hardiesse** — une abeille craintive file plus vite. Unique parmi les huit routines écrites ; registre vérifié deux fois au désassembleur avant transcription
- doc : **milieu et charpente sont deux axes indépendants.** L'Abeille est volante (intégration, interruption et bloc de vol de la Coccinelle) et pourtant bâtie comme une terrestre (quatre postures, trois plages, `SceneEntityDelay`). L'étape 4 de la recette calque désormais sur **deux** sœurs, pas une
- feat : **`FUN_0049D030` transcrite — l'Homme-lézard**, sous-type 7 du bloc A, neuvième routine d'intelligence et la plus riche : les cinq postures de la Fourmi géante, recul, relance et esquive compris, **et** deux gestes. Huit des vingt-deux routines d'IA de la forêt sont écrites
- doc : **sa charge ne fait aucun dégât** — `FUN_00539b80` n'est appelé qu'une fois dans les 2 546 octets, depuis son autre geste. La course croise pourtant le gabarit à chaque image : elle projette, décolle et pose l'effet `0x39`, sans résoudre de coup. **Deux balayages ne font pas deux attaques**
- doc : **sa course tourne en boucle** — l'image d'animation est remise à 8 tant que la bête file, et le geste s'arrête quand la vitesse horizontale retombe sous `hardiesse / 8 + 1`. Une bête hardie court donc *moins* longtemps
- doc : **quatre façons de terminer un geste** sur neuf routines — compte à rebours, contact avec le sol, seuil de vitesse au `sqrt`, ou la seule fin d'animation. Rien ne permet de deviner laquelle ; la recette gagne une étape qui l'impose en lecture
- doc : **le bit 0 retient le coup** — une bête en l'air voit son image d'animation *reculer* d'un cran au lieu d'avancer quand le geste atteint sa fenêtre de contact. Le motif venait de la Fourmi géante et n'avait pas été compris ; il prend son sens chez une bête dont la posture 3 bondit
- fix : **`FUN_00402FC0` transcrite — la levée signalée en jeu en frappant un goblin.** Entrée 84 de la table statique `0x00551c30`, 500 octets absents de `Blaze.c`. C'est un effet de sort qui **tient sa cible** : animation 2 imposée et défilée à la main une image sur deux, gerbe de quatre `0x38` et six `0x5a`, son `0x25`, quinze à dix-neuf images
- doc : **`FUN_004031c0` est une seconde émission de `FUN_00468080`** — même source, allocation de registres différente, donc pas d'octets identiques. `TryPlaySoundNearEntity` est réemployée telle quelle plutôt que dupliquée ; `Blaze.c` et le désassemblage concordent argument pour argument
- fix : **la gerbe de coup est complète — cinq gestionnaires d'effet de plus**, après une seconde levée signalée en jeu : `FUN_004034A0` (l'étincelle en spirale), `FUN_00403250` (le grain de poussière, sa sœur), `FUN_00447280` (**l'éclat d'impact que les dix routines d'IA font naître à chaque coup**), `FUN_00447380` (l'anneau) et `FUN_004474C0` (les huit étincelles de sa couronne). Avec eux `FUN_00404DE0`, l'éclaircissement de teinte
- doc : **`FUN_00447280` porte trois lignes mort-nées** — l'encombrement de la victime est calculé, borné deux fois, puis écrasé par la constante `0xb4` trois instructions plus loin. Transcrit tel quel
- feat : **`FUN_0049DA40` transcrite — le Goblin**, sous-type 8 du bloc A, dixième routine d'intelligence. **La première bête qui attaque de loin** : son second geste ne croise aucun gabarit, il fait naître le projectile `0x49` à la septième image et rend la main
- doc : **son jet part des deux plages lointaines**, approche et errance, ce qu'aucune des neuf précédentes ne fait — et le seuil du tirage se prend sur la **hardiesse** (`hardiesse / 2 + 8`), seul cas de la famille où le seuil monte avec l'état de la bête au lieu de descendre avec sa timidité
- feat : **le TIR est écrit — premier sous-système d'attaque à distance du jeu.** Les trois corps du jet du Goblin, tous absents de `Blaze.c` : `0x0047A100` (le lanceur), `0x0047A270` (la trajectoire) et `0x00476320` (l'éclaboussure). Avec eux trois feuilles : `FUN_00474f00` (choisir une cible dans un cône), `FUN_00519af0` (le balayage des entités de scène par la grille spatiale) et `FUN_00474c70`
- doc : **le lanceur vise en trois dimensions** — azimut en `+0x32`, site en `+0x34` —, ce qu'aucune routine de bête ne fait, et il ajoute `distance / 4` au site : un tir long part plus haut
- doc : **la trajectoire avance en huit sous-pas** d'un huitième de vitesse, en appelant le placement à chacun et en accumulant les drapeaux de collision — c'est ce qui empêche un projectile rapide de traverser un mur. Son virage est plafonné à `±0x20` par image, jamais plus
- doc : **c'est le propriétaire qui frappe, pas la flèche** — le lanceur range le tireur en `+0x74`, et c'est lui que la trajectoire passe à `FUN_00539b80`. Les dégâts et l'expérience reviennent au Goblin
- doc : **la longueur d'une table de descripteurs se lit dans le code**, pas seulement dans la mise en page : le compteur `+0x40` du corps dit sept images pour l'éclaboussure (`0x00551a08`), trois pour l'étincelle (`0x005515a0`), une seule pour le grain de poussière (`0x0054f118`) qui ne l'incrémente jamais
- doc : **l'oracle manquant est humain.** Aucune vérification automatique ne peut dire si un comportement est fidèle ; quelqu'un qui connaît le jeu, si — pour la *silhouette* (vole ou marche, charge ou tourne autour), jamais pour les constantes. Faire tourner chaque routine neuve devant lui, une fois, dans sa sous-zone d'origine et sans greffe
- doc : **le vol tient en dix lignes** — `+0x14` accumule une phase, son sinus fait osciller la hauteur autour d'un plafond, et ce plafond se cale sur l'altitude de la cible sans jamais descendre sous le sol de la bête. Pendant un geste, l'oscillation cesse et elle prend de la hauteur
- doc : **la Coccinelle casse trois hypothèses** léguées par les deux premières routines — pas de fin de geste, pas d'image de frappe (elle balaie à chaque image du geste), poussée amortie au lieu d'être remise à zéro, et son cap d'errance vit en `+0x12` et non `+0x10`
- doc : **la recette « substituer cinq constantes » ne vaut qu'entre bêtes du même milieu.** Le tri le moins cher est de regarder quelle fonction d'intégration la routine appelle — une seule ligne du désassemblage
- feat : **le filet** — `OPENFORESIA_AI_STUB=1` : un gestionnaire d'entité non transcrit ne tue plus le processus, il se journalise une fois et rend la main. Une partie rapporte donc **toutes** les lacunes de son parcours, là où une levée n'en donnait jamais qu'une
- feat : le journal `[LACUNE]` compte les appels par couple *(gestionnaire, sous-zone)* et les récapitule à la sortie propre, du plus sollicité au moins — **ce compte est l'ordre de transcription**, il ne contient que ce que le joueur croise vraiment
- feat : `playtest.ps1 -EnvVar A=1,B` — le pendant de `-Trace` pour les drapeaux qui changent le comportement du jeu au lieu de le décrire
- feat : **la salle d'essai** — `OPENFORESIA_AI_BENCH` : à l'arrivée dans une sous-zone, le banc lit sa table d'espèces, retient **une espèce par sous-type**, et les fait toutes naître autour du groupe par `SpawnEntityFormation`. Un lieu, une vingtaine de secondes, et tous les comportements qu'il installe ont tourné
- feat : `AI_BENCH=3:0` se transfère d'abord — par le chemin du jeu : les quatre globales de destination de l'opcode 17, le pas de boucle à 2, le bit 2 des drapeaux de monde
- feat : `AI_BENCH_ORPHANS=1` **greffe** les sous-types installés qu'aucune espèce du lieu ne porte, en remplaçant le seul octet `+5` de l'entité — les onze comportements du bloc A de la forêt dans une seule salle
- feat : chaque appel de gestionnaire est compté et chaque levée retenue au lieu de tuer le run ; le bilan périodique nomme la bête, en lisant les seize premiers octets de son modèle
- fix : le banc partait pendant la **démonstration de l'écran-titre** — `RunWorldMapTick` y tourne sans partie chargée, et le transfert mourait en `PlaceWorldMapParty` (`0xC0000005`) avant la première frappe. Garde sur `s_PartySize_005e9020`
- feat : `tests\banc.bat` — un double-clic lance le jeu avec la salle d'essai et réaffiche les lignes `[BANC]` à la fermeture. `banc.bat 3:0` pour se transférer, `banc.bat 1 0` pour désactiver la greffe
- feat : **`FUN_00519940` transcrite** — c'est **le coup du groupe contre les entités de scène**, le miroir de `FUN_00519c30` : balayage 3×3 de la grille spatiale, `TestMeleeReach`, `ResolveMeleeHit`, recul de seize fois le sinus du cap et clignotement. C'était le plantage « au moment de taper », relevé par le premier vrai run du banc
- fix : **`EmitFootstep_00519940` était un nom inventé**, et `FootstepParameters` / `s_FootstepTable_00562cf0` avec lui. La table de `0x00562cf0` ne porte pas des paramètres de pas sonore mais les **gabarits de portée par posture** — six shorts, la forme exacte de la sonde des routines d'IA — et ses quatre lignes nulles (4, 7, 12, 15) sont exactement les postures que le site d'appel écarte
- doc : la borne de ligne de `FUN_00519940` laisse passer l'indice 0x20, une ligne de plus que la grille n'en a — `TODO(REVERSE)` posé, transcrit tel quel
- feat : **`FUN_00539190` et `FUN_005391f0` transcrites** — c'est **la montée de niveau**, pas « appliquer des dégâts à un membre » : palier `niveau/4 + 0x30 + niveau*2`, gains de PV/PM, croissance des six caractéristiques sous plafond, et l'apprentissage d'un sort pour les classes 1, 3, 5 et 7. C'était le plantage du deuxième run du banc, au premier coup qui tue
- fix : **troisième nom inventé tombé le même jour** — `ApplyDamageToMember_00539190`, après `EmitFootstep_00519940` et ses deux voisines. Le commentaire « dégâts répartis sur tout le groupe » du second site d'appel était faux lui aussi : c'est l'**expérience** qui se répartit
- doc : `FUN_00539190` est la même enveloppe que `RunEntityScript` — bascule de l'allocateur de la DLL sur le tampon de travail. Ghidra rendait son second appel avec `unaff_retaddr` ; le désassemblage montre que les deux branches passent `(param_1, param_2)`
- doc : trois tables relevées dans le binaire — `0x0056d308` (la caractéristique favorisée par sous-classe), `0x0056d310` (les chances de progression, trois familles pour huit classes), `0x0056d330` (les sorts accessibles par palier, trois écoles) — plus le descripteur d'effet `0x005648a8`, onze enregistrements de huit octets
- doc : **la septième valeur de `+0x2a0` mène toute la progression** — elle élargit les tirages de gain, divise dans les deux chances, et pèse dans le tirage de `+0x28c`, le seul champ du bloc qui peut reculer à une montée de niveau
- feat : **`FUN_0051a740` transcrite** — **la mise à mort d'une entité de scène et son butin** : l'entité morte est recopiée dans la table des objets tombés (`0x005f7de0`), d'où le butin qui naît là où la bête est tombée, puis zéro à trois éclats partent en étoile autour. C'était le plantage du troisième run du banc, au premier monstre tué
- doc : **la septième caractéristique du tueur (`+0x2ac`) mène aussi le butin** — elle ajoute sa moitié à la chance de l'espèce (`modèle + 0x22`) pour décider entre argent et objet, et elle pèse une seconde fois sur la classe de l'objet. C'est la même valeur qui mène toute la montée de niveau : une seule caractéristique décide du rythme ET des trouvailles
- doc : la classe d'objet se tire en `0xb4..0xb9` par une cascade de cinq seuils ; deux descripteurs de butin relevés dans le binaire (`0x005646e0` l'argent, `0x005646e8` les éclats), huit octets chacun
- doc : `TODO(REVERSE)` — `FUN_0051a740` lit le tueur en `+9` là où tout le reste de la simulation lit `+8`. Transcrit tel quel ; si le butin dépend du mauvais personnage, c'est là
- doc : **la forêt a un nom sur chaque comportement** — les trois tableaux de blocs de `Comportement.md` portent désormais la bête (Fourmi géante, Coccinelle, Goblin, Grizzly, Crabe géant, Behemoth…), par croisement `especes.csv` × `modeles.csv`. Les trois de la sous-zone 0 sont confirmées en jeu par le banc
- doc : `test-protocol.md` — la boucle de monde n'implique pas une partie en cours ; la leçon vaut pour tout échafaudage branché là
- doc : **une sous-zone ne porte que trois à huit espèces** alors que sa table en installe jusqu'à onze — c'est ce fait, relevé sur `especes.csv`, qui rend la greffe nécessaire
- doc : `espèce[1]` **est** le numéro de comportement, donc la colonne `ia` d'`especes.csv` porte déjà le croisement espèce ↔ sous-type que `Comportement.md` disait manquant ; seul le tableau d'ensemble reste à écrire
- doc : `test-protocol.md`, sections « Le filet » et « La salle d’essai » ; `Comportement.md`, deux sections neuves ; `ARCHITECTURE.md`, la seule exception d’échafaudage dans `src/Blaze/`

## 2026-08-28
- doc : **le compte exact des comportements d'ennemi** — `Documentation/Ennemies/Comportement.md`. La table `0x0056ad48` installe **166 gestionnaires distincts** sur 509 emplacements, dont **112 routines d'intelligence** ; trois sont écrites. Ma première estimation (« une quarantaine ») était un plancher tiré d'un balayage trop étroit
- doc : **ce n'est pas une fonction par ennemi** — 555 espèces pour 112 comportements. Une routine est un comportement partagé, pas un monstre
- doc : **la forêt (zone 3) utilise 23 gestionnaires, dont 22 d'IA**, répartis sur trois blocs seulement ; deux sont écrits, et les neuf autres du bloc d'arrivée sont ceux qui se réveilleront ensuite
- feat : `OpenForesia.Tools/census-ai-handlers.sh` — le relevé, reproductible, sans le CD : tout vient de `Blaze.exe`
- feat : **`FUN_00499DE0` transcrite** (1377 octets, absente de `Blaze.c`) — le sous-type 0 de la table `0x0056ad48`, sœur de `FUN_0049A980`. C'est le plantage à l'apparition d'un groupe, celui d'après
- doc : **c'est une FAMILLE, pas deux fonctions** — un balayage du binaire trouve **au moins 39** appels à `FUN_00535F20` entre `0x00491745` et `0x004aba7d`, donc une quarantaine de comportements d'espèce bâtis sur le même squelette. Deux sont écrits ; chaque espèce nouvelle en réveillera un autre
- doc : **le squelette est identique, seules cinq constantes changent** — la sonde, l'image où le coup porte, le nombre de postures, la largeur des cônes de virage, et les seuils de tirage. `FUN_00499DE0` est une bête plus petite et plus prudente : portée `0x3c` au lieu de `0x50`, coup à la 12ᵉ image au lieu de la 8ᵉ, quatre postures au lieu de cinq, cônes deux fois plus étroits, et **ni bond, ni recul, ni esquive**
- doc : **sa posture 3 tourne au lieu de marcher** — seule différence de forme et non de constante : elle pose une poussée et pivote vers un cap rangé en `+0x10`, à `0x20 + hardiesse / 2` par image
- feat : **le combat au corps à corps est complet** — `FUN_00539B80` transcrite avec ses cinq dépendances manquantes (`FUN_0053a860`, `FUN_005398a0`, `FUN_005398e0`, `FUN_005399e0`, `FUN_00539a20`). Un monstre peut frapper, blesser et tuer ; le chemin qui plantait depuis trois signalements est vivant de bout en bout
- doc : **les huit éléments sont rangés par couples contraires** — l'opposé se désigne par `indice ^ 1`, et `FUN_005398e0` annule chaque couple dans les DEUX blocs avant de compter. Feu contre eau, terre contre air, lumière contre ténèbres, sacré contre mal
- doc : **attaquer par un élément que la victime ne porte pas ne paie qu'au quart** si elle ne porte pas non plus son contraire ; plein tarif si elle porte le contraire ; et l'élément est **soustrait** si elle porte le même
- doc : **le nombre de dégâts fait un aller-retour par le bloc de statistiques** — écrit en `+0x100`, l'événement d'objet équipé part, puis il est **relu**. C'est le mécanisme qui permet à un objet de modifier les dégâts, pas un détour
- doc : **la fiche des deux combattants est refaite à chaque coup** — les cinq passes `FUN_00538ab0`..`FUN_00538fa0` reconstruisent équipement et dérivées depuis zéro, si bien qu'un changement d'arme est pris en compte sans que rien n'ait à le notifier
- doc : **personne ne fait jamais zéro dégât** — le résultat est borné à 1 après le bonus d'un sixième de l'attaque moins un trente-deuxième de la défense
- fix : le bloc de caméra passe de 0x84 à 0x88 octets — `FUN_00539b80` écrit l'octet `DAT_005de0b4`, qui tombait exactement sur l'ancienne borne. Même cas que `DAT_005de0b0` le 2026-08-21
- feat : **`FUN_00535A90` transcrite au désassembleur** (1158 octets) avec `FUN_005358f0`, `FUN_00535a40` et `FUN_00538c10` — le test de portée du corps à corps. La branche du coup qui porte est vivante ; il ne reste que `FUN_00539B80`, la résolution des dégâts
- doc : **le rôle de `DAT_0056d440` +0 et +4 est établi** — ce sont les deux demi-étendues de la boîte du corps, pas des inconnues. Le `TODO(REVERSE)` posé dessus tombe, et le symbole est renommé `s_ClassBodyMetrics_0056d440`
- doc : **la table de classe et le bloc d'entité portent le même enregistrement de 8 octets** — `FUN_00538c10` rend `0x0056d440 + classe*8` pour un membre et `stat + 0x58` pour une entité de scène. `StatusBubbleHeight` lisant déjà `stat + 0x5a` là où l'autre lit `+2`, les deux lectures se confirment
- doc : **le test de portée croise deux quadrilatères, dans les deux sens** — le geste contre le corps, puis le corps contre le geste. Le second sens rattrape le cas où l'une des figures contient l'autre sans qu'aucun sommet ne franchisse d'arête
- doc : **le produit en croix passe par le GTE** — `NCLIP` détourné en test de côté, et le décalage du masque est **en retard d'un tour** : c'est la latence du coprocesseur d'origine, pas une maladresse
- feat : **les monstres pensent** — `FUN_0049A980` (2175 octets, absente de `Blaze.c`) transcrite au désassembleur avec ses quatre dépendances directes : `FUN_00535F20`, `FUN_00488160`, `FUN_004DFAF0` et `FUN_004E3770`. C'est le plantage juste après l'apparition d'un groupe d'ennemis
- doc : **la fonction se lit en trois temps** — décider, appliquer, replanifier. Les 43 cibles de saut ne sont que les sorties de la première partie, qui convergent toutes sur la deuxième ; le découpage rend abordable ce qu'un comptage d'octets faisait passer pour infaisable
- doc : **tout le comportement tient sur un seul nombre** — la « hardiesse », de 0 à 0x40, tirée de l'écart de niveau et de l'état de santé. Elle élargit les cônes de virage et allonge les pas ; son complément à 0x40 élargit les seuils de tirage. Une bête faible face à un groupe fort hésite, tourne moins vite et frappe moins souvent, et c'est une soustraction qui le produit
- doc : **deux champs portent la posture** — `+0x42` la posture (0 attendre, 1 se tourner, 2 tenir la distance, 3 marcher ou bondir, 4 foncer, table de sauts relevée à `0x0049b200`) et `+0x43` le compte à rebours qui la maintient
- doc : **`+0x10` sert à deux choses selon la posture** — la distance voulue en posture 2, le cap tiré au sort en posture 3. Jamais les deux à la fois
- fix : **deux signatures que `Blaze.c` donne fausses** — `FUN_00535F20` écrit ses **cinq** sorties sur le même `*param_2` chez Ghidra (ce sont cinq pointeurs distincts) et `FUN_00488160` est déclarée à un paramètre alors qu'elle en prend deux. Les deux se règlent à l'arithmétique de pile du désassemblage
- feat : **`FUN_0051a5d0` transcrite** — la reconnaissance de la combinaison de directions saisie pendant l'incantation, le plantage au **relâchement** de Select. Les combinaisons sont dans la table de sorts elle-même : longueur en `+0x1f` de la fiche, directions attendues à partir de `+0x20`
- doc : **les deux bornes de la combinaison se répondent** — `ApplyGroundMotion` plafonne la saisie à `0x10` coups, et seize octets tiennent exactement entre `+0x20` et la fin de la fiche de `0x30`. Chaque lecture confirme l'autre
- fix : **appuyer sur Select en forêt ne plante plus** — le sous-type `0xbd` de la table statique `0x00551f30`, `FUN_004765D0` (350 octets, absente de `Blaze.c`), est transcrit depuis le désassembleur : c'est l'auréole d'incantation que `ApplyGroundMotion` fait naître à chaque posture de sort
- doc : **la première entité `0xbd` n'est qu'une graine** — elle se recopie deux fois en avançant sa rotation de `0x800`, puis s'éteint. Le tri se fait sur le bit `0x100000` de la source : un personnage ne le porte pas, mais `SpawnCastEntity` le force sur tout ce qu'elle engendre, donc une copie reconnaît sa jumelle et se raccroche au lanceur
- doc : **l'auréole meurt quand le lanceur quitte la posture 5** — la même valeur que `ApplyGroundMotion` écrit en `+0x18` pour incanter. Deux autres conditions l'éteignent : le bit `0x50000000` du lanceur, et `0x400000` sur le masque `0x400004`

---

## 2026-08-26

- doc : **les quatre valeurs dérivées non affichées sont probablement des résistances** — établi sur le contenu, pas sur le code : les objets qui ne posent qu'elles sont tous des objets de protection (« système immunitaire », « anti-sortilèges »), la série des Médaillons les pose à 6/12/24 sur les indices 11 et 15 à la fois, 66 des 128 objets concernés posent toutes les leurs à la même valeur, et aucune arme pure ne les touche. Hypothèse, faute de lecteur identifié
- doc : **le système d'états ne touche que quatre des huit valeurs dérivées** — les emplacements 0, 1, 4 et 5, une paire de bits hausse/baisse chacun dans `+0x140`, plus le `0x200` de la rage qui monte l'ATT et descend la DEF. C'est la confirmation par le code des noms ATT/DEF/MATT/MDEF relevés à l'écran
- doc : **les huit dérivées se répondent deux à deux** — chaque paire sort des mêmes caractéristiques, et les quatre contreparties (emplacements 2, 3, 6, 7) ne sont dessinées qu'après « Gurkha », en colonne nue et **sans libellé**
- doc : **rien d'autre dans `Blaze.c` ne lit ces huit valeurs** — quatre mentions de `+0x130`, toutes rendues compte ; les copies de travail `+0x292`/`+0x296`/`+0x29a`/`+0x29e` n'apparaissent qu'à leur propre écriture ; et seules les deux passes de recalcul prennent la base `+0x100`
- doc : **les quatre valeurs sous les caractéristiques sont ATT, DEF, MATT et MDEF**, pas « quatre résistances » — relevées à l'écran par Vincent. Ce sont les indices 8, 9, 12 et 13 du bloc `+0x20` d'un objet, que `s_StatOrder_0056a8d0` = `[0,1,4,5,…]` envoie sur les emplacements 0, 1, 4 et 5 de `+0x130`
- doc : **les indices 10, 11, 14 et 15 (`+0x34`, `+0x36`, `+0x3c`, `+0x3e`) ne s'affichent pas** en jeu normal — ce sont les rangs 4 à 7 de la même table, dessinés seulement dans le bloc gardé par `DAT_005633a8`, ouvert par « Gurkha »
- doc : **l'indice 7 (`+0x2e`) est une case morte** — la huitième d'un tableau de caractéristiques rempli à sept. Une seule occurrence dans `Blaze.c`, et c'est une écriture ; la Cuirasse de maître y met +5 sans que rien ne bouge
- doc : **les malus d'équipement ne se voient jamais** — `FinalizeEquipmentEffects` écrête seize `short` à partir de `+0x120` à `[0, 999]`, donc une valeur négative écrase son emplacement à zéro au lieu de le décrémenter
- feat : `bestiaire.html` nomme les quatre valeurs de combat sur une même rangée et marque les emplacements sans rôle (« jamais lue », « non affichée ») au lieu de les numéroter
- feat : **les coffres sont cartographiés** — 161 coffres dans les zones 1 à 10, avec leur contenu chiffré, leur position et leur numéro. Nouvel onglet dans `bestiaire.html`, nouveau `coffres.csv`, et la fiche d'objet dit désormais quels coffres le contiennent
- doc : **un coffre tire dans la même table `+0xb4` qu'un monstre** (`PickWeightedTableEntry`, FUN_0053a920) — seule la rangée change, et c'est pourquoi **112 objets ne s'obtiennent qu'en coffre**. Sa fiche voyage dans les champs de hauteur et de rotation de l'entité (`+0x30`, `+0x34`), que l'état 0 du gestionnaire vide dans `+0x9a`, `+0x44` et `+0x48`
- fix : **un numéro de sous-type d'entité ne veut rien dire tout seul** — la table 0x0056adc8 est rechargée à chaque sous-zone. Filtrer sur « sous-type 13 » donnait 36 coffres aux champs tous nuls ; résoudre la table de la sous-zone dans `Blaze.exe` en donne 161 cohérents, et montre que le coffre de la zone 1 est le sous-type 22
- feat : **la fiche d'objet nomme les classes qui peuvent l'équiper** au lieu d'aligner huit chiffres — le masque de `+0x12` se lit désormais « Voleur » sur le Glaive, « Nain » sur les haches
- doc : **les huit bits de `+0x12` sont identifiés**, par `&PTR_s_Fighter_0056c7c0` (bloc `0x0056c894`) et non par déduction : 0 Guerrier, 1 Prêtre, 2 Voleur, 3 Sorcier, 4 Chasseur, 5 Elfe, 6 Nain, 7 Fée. `SetClassBaseHpMp` et le contenu du jeu — le bit 6 ne porte que des haches, dont Gulwick — recoupent l'ordre
- feat : **la cartographie des ennemis, lue hors-jeu sur le CD** — `OpenForesia.Tools bestiaire` rejoue la chaîne de chargement (`SYSTEM.INI` → état → zone → sous-zone) et sort les quatre tables du répertoire de zone : 281 fiches et 121 noms dans les zones 1 à 10, 913 formations, 3 018 placements, 434 classes de butin. Voir `Documentation/Reference/Bestiaire/`
- doc : **le nom du monstre est dans sa fiche** — les seize octets de tête du bloc de 0x60, ceux que `ResetEntityStats` recopie « tels quels » en `stats+0x00`. Le premier modèle lu s'appelle « Goblin »
- doc : **chaque monstre nommé porte le numéro de son IA** (`espèce+1`) — le chantier des 166 gestionnaires de la table `0x0056ad48` a enfin des noms en face de ses numéros
- fix : **les deux jauges du modèle étaient inversées** — `+0x14` est la vie et `+0x16` la magie, pas l'inverse. Les trois usages qui « confirmaient » `+0x16` portent tous sur la même paire `(+0x4a, +0x4e)` ; c'est la paire symétrique `(+0x48, +0x4c)` que la pétrification draine. La Coccinelle avait 1 point de vie sous l'ancienne lecture, elle en a 8
- feat : **le butin est nommé, et l'index inverse existe** — la valeur tirée est un identifiant d'objet dans le **catalogue de la sous-zone** (`areaDir + 0xb0`, fiches de 0x140 octets montées par `UploadRawAreaBlocks`) : 386 objets, dont 265 obtenables sur un monstre. `butin_par_objet.csv` répond à « où trouver l'Armure du dragon ? »
- doc : **le vrai nom d'un objet est le nom long de `+0x41`**, terminé par une barre oblique ; le nom court de `+0x00` n'est qu'un repli tronqué à seize octets. Et son octet de tête n'est pas une lettre : au-dessus de `0x5f` c'est un pictogramme du moteur de texte, ce qui explique les `w`/`m`/`s` d'une lecture naïve — et pourquoi « La devoreuse » garde son L
- fix : **la première lecture du butin confondait rangement et sens** — la valeur écrite en `+7` de l'entité de butin avait été prise pour un index de liste parce qu'une autre fonction y range ça. Les trois autres appelants la passent directement à `FUN_00530f20`, la remise d'objet : c'est un identifiant d'objet
- feat : `bestiaire.html`, page autonome à deux vues (monstres, objets), générée par `build-bestiaire-page.ps1` depuis les CSV
- feat : **la description d'un objet était dans sa fiche depuis le début** — le champ `+0x41` ne porte pas un nom mais `« nom/description »`, et la barre oblique que `FUN_0051bd50` coupe est le séparateur. **Elle est facultative** : sans elle le champ entier est la description et le nom retombe sur le nom court (« Potion med » / « Petite Potion »). Les 1 618 fiches en ont une ; la première lecture en manquait 242
- doc : **ATT, DEF et descriptions recoupées contre l'inventaire du jeu** par le Responsable — Glaive 9/0, Plastron 0/24, Potion med 0/0 : concordance exacte sur les trois
- fix : **la vue Objets affichait `undefined` en description, ATT et DEF** — la ligne construite par `render()` ne transportait que le nom et les sources, et c'est cet objet-là que le dépliage reçoit, pas celui du jeu de données
- fix : **une puce de zone « Zundefined » qui vidait les deux vues** — même cause que les barres `NaN` : la liste des zones se lisait sur un champ que le regroupement par nom ne porte plus. Le contrôle ne le voyait pas parce que les puces sont construites par `createElement` et non par `innerHTML` ; il aplatit désormais aussi les éléments créés par script, et exerce chaque filtre de zone dans les deux vues
- fix : **les barres de PV et PM de la vue Monstres étaient en `NaN`** — les maxima se lisaient encore sur les fiches après le regroupement par nom, où le champ n'existe plus
- feat : `check-bestiaire-page.mjs` — le contrôle **exécute** le rendu dans un DOM minimal et échoue sur tout `undefined`/`NaN` dans le HTML produit. Un contrôle qui relit la charge utile passait pendant que la page affichait `undefined` ; c'est lui qui a trouvé les deux défauts ci-dessus
- feat : ATT et DEF deviennent des colonnes triables de la vue Objets
- fix : l'en-tête de tableau de la page n'est plus collant — sous une barre de contrôles elle-même collante, il finissait toujours par masquer une ligne, quelle que soit la valeur de son décalage
- feat : **les bonus et malus des objets**, nommés par l'écran d'objet du jeu et non déduits — `DrawItemStatRow` affiche huit octets signés de `+0x18` puis deux valeurs longues de `+0x30` et `+0x32` : ce sont l'**ATT** et la **DEF**. Vérifié sur les données : les armes ont l'ATT, les armures la DEF
- feat : **les dix zones portent leur nom** — relevés dans les textes du CD (`« - Le labyrinthe des morts - »`), donc avec l'orthographe du jeu. La liste apportée par le Responsable est confirmée sur les dix ; le relevé y ajoute l'orthographe et l'association à l'indice, seule chose indevinable de l'extérieur
- feat : **les huit éléments sont nommés** — les octets de `objet+0x18` sont Feu, Eau, Terre, Air, Lumière, Ténèbres, Sacré, Mal. Le jeu se documente lui-même : une « Chaîne de X » par emplacement, décrite « Protection des élémentaux de X », plus les quatre capes saisonnières et Gulwick (25 en Terre, ce que la FAQ du jeu annonce aussi)
- feat : **`modèle+0x28` porte les mêmes huit éléments** — le `TODO(REVERSE)` sur ces huit octets tombe : Triton glace en Eau, Grifon en Air, Fantôme en Ténèbres et Mal. 214 fiches sur 281 ont au moins une affinité
- fix : **`+0x12` n'est pas un masque d'éléments mais les classes qui peuvent équiper l'objet** — `DrawItemElementRow` porte un nom posé sur une hypothèse, là où `GrantChestContents_00530F20` le documentait déjà juste. Le « Simple bâton » n'allume qu'un bit, le « Manteau de cuir » les huit
- fix : la page étiquetait les indices 8 et 9 des seize bonus « résistance 1 et 2 » ; ce sont l'ATT et la DEF
- fix : dans la page, la description débordait à droite (`tbody td` impose `nowrap`, dont le dépliage héritait) et l'en-tête collant recouvrait la première ligne après un tri — hauteur de la barre de contrôles mesurée, et recalage du défilement au tri
- feat : la vue Monstres affiche **un monstre par ligne** et non une fiche — le Goblin en avait neuf ; les variantes, le seuil, les placements et l'IA sont passés dans le dépliage
- fix : **la vue Objets affichait « Zundefined »** — `ForEach-Object { @(...) }` déroule le tableau interne dans le pipeline, donc les sources arrivaient aplaties en une liste de six fois N valeurs. La virgule de tête (`,@(...)`) les garde imbriquées, et un garde-fou lève désormais plutôt que de publier une page muette
- fix : **la chance d'obtention était sous-estimée** — un même objet occupe souvent plusieurs des huit rangs d'une classe, et les rangs se cumulent : l'Abeille lâche l'Aiguille médicinale à 57 % (rangs 0+1+2), pas à 25 %
- doc : **la loi de tirage du butin est exacte** — sept seuils dans `FUN_0053a920`, donc 25/17/15/13/11/9/6/4 % sur une plage de 100 ; à la mise à mort la plage vaut 56 à 100 selon l'écart de niveau, et une plage de 56 rend les trois derniers rangs inatteignables
- doc : **la fiche des monstres existe déjà dans les données** — le bloc modèle de 0x60 octets (`areaDir + 0x98`, indexé par `species[4]`) porte les PV (`+0x16`) et tout le reste ; `ResetEntityStats` ignore le bloc d'espèce et remplit l'instance depuis le seul modèle. Une classe par ennemi n'aurait rien à contenir : la variation est dans les données, le code ne varie que par l'IA
- doc : **les onze monstres de la forêt sont un chantier d'un autre ordre** — table `0x0056ad48`, 1 400 à 2 550 octets et 24 à 45 appels chacun (contre 100–950 octets et presque aucun appel pour les dix-sept décors) ; carte des appelées relevée, six manquantes dont une aux paramètres perdus par Ghidra
- fix : **le texte de la forêt était centré ligne par ligne** au lieu d'être aligné à gauche — `FUN_00544d90` (retour à la ligne) prend son **premier argument** comme drapeau de remise à zéro, et la table des codes de contrôle lui passe le curseur de texte, jamais nul ; la reconstruction passait `0` en dur, donc l'état « espace de tête retiré » survivait d'une ligne à l'autre et `FlushLine` centrait tout le bloc
- feat : le verbe 62 de la forêt (`FUN_00445480`) — « suis-je le dernier membre vivant du groupe ? »
- feat : **le crochet de contact au sol de la forêt** — `FUN_00541440` (entrée 32 de la table `0x0056afc8`, souche vide en zone 0) : la poussière du pas cadencée par la période du clip d'animation, et l'effet de terrain brûlant
- doc : **le jeu fabrique une instruction de script sur la pile** — sept octets locaux passés à `FUN_00513FE0`, qui n'est qu'une enveloppe autour du gestionnaire de l'opcode 10. Les gestionnaires d'opcode ne sont pas que des cibles de table : ce sont des primitives appelables
- feat : **le sous-type 27, le seul décor de la forêt qui blesse** — `FUN_00456CF0`, via `FUN_0045DA40` (frappe les membres que l'octet de contact désigne) et `FUN_005397B0` ; quinze gestionnaires sur dix-sept
- fix : **`SetEntityPose_005397B0` n'a jamais posé de pose** — c'est `ApplyPercentDamage_005397B0`, les dégâts en pourcentage des PV max ; ses deux appels `(slot, 2, 0)` voulaient dire **2 % des PV**, pas « pose 2, mode 0 ». Un nom posé sur une hypothèse empêche de relire
- feat : **six gestionnaires de plus en forêt** — le radeau (`FUN_00456750`), l'ascenseur (`FUN_00457370`), la scénette de sortie qui détourne la caméra (`FUN_00474380`), le projectile à cinq échos (`FUN_00457BF0`), le décor qui attend un bit de script (`FUN_004746F0`) et celui qui traverse et éclate (`FUN_00456F40`) — quatorze sur dix-sept
- doc : **le banc de script est un bus, et on en voit les deux bouts** — la dalle écrit un bit dans `0x005e96e0`, le sous-type 26 en lit un dans `0x005e9840`, le 16 ne démarre pas sans `0x005e96fb`, le radeau exige deux bits de `0x005e96ff`
- feat : **huit gestionnaires d'entité de la forêt** — la dalle de pression (`FUN_00457EC0`, qui pose un bit dans le banc de script `0x005e96e0`), le décor qui balance, celui qui respire, celui qui tombe et flotte, celui qui tremble, celui qui chuchote, celui qui lance un script sur le meneur ; plus `CountPartyMembersMatching` et `TryPlayDistanceSound`, la troisième sœur de la famille « distance à la caméra »
- doc : **trois « appelées manquantes » étaient des thunks d'import** — `0x00545A10`/`A16`/`A1C` sont des `jmp dword [IAT]` vers les ordinaux winsys3d 442, 450 et 451, déjà liés au dépôt
- feat : **le sous-type 31 de la forêt** — `FUN_00458170`, un décor qui parle au contact : machine à trois états sur `+0x10`, avec repli sur `s_EntityList_005de558` quand `+0x70` est nul
- doc : **la trace `-Trace SCRIPT` mesurée en jeu** — quinze opcodes tournent du trajet auberge → forêt, tous déjà transcrits ; l'opcode 30 tourne **une seule fois** et c'est le seul chemin hors du bâtiment. Ce qui bloque n'est ni les opcodes ni les verbes, mais les gestionnaires d'entité
- feat : **le premier gestionnaire d'entité propre à une zone** — `FUN_00456730`, sous-type 32 en forêt (échelle à 0x0C00 sur les trois axes, soit trois quarts) ; la zone 0 renvoyait au gestionnaire par défaut, donc c'est le relevé des onze zones qui se vérifie au runtime
- feat : **le premier verbe de la forêt** — `FUN_00443260`, le « tout effacer » des quatre bancs de variables de script (banques 3, 2, 1, 0), relevé au désassembleur ; plus `FUN_00442E10` (test du bit 0 de `+0x8c7` du descripteur) et `FUN_004432E0`, pris dans la foulée
- feat : **les onze zones ont leurs tables de sauts d'entités** — `Engine/EntityVTables.cs`, généré par `OpenForesia.Tools/dump-entity-vtables.sh` : 7 × 176 adresses et 197 blocs relevés dans `Blaze.exe`, là où seul l'index 0 était écrit à la main. Contrôle : les sept blocs de l'index 0 sont identiques, entrée pour entrée, aux tables `s_VtSource_*` d'avant
- doc : **`0x0056B3C8` n'est pas la souche vide** — c'est soixante-quatre fois `FUN_00536170`, l'arrêt fatal, le bloc que le jeu installe pour les sous-zones qui n'existent pas
- feat : **on peut sortir de l'auberge** — opcode 30 de l'interpréteur de script (`FUN_00515e20`, absent de `Blaze.c`, relevé au désassembleur) : jumeau de l'opcode 17, il marque le membre comme partant puis pose le pas de boucle à `0xfe` et l'état de jeu à **2**, soit la sortie par l'écran de choix de destination
- doc : **l'utilisation des objets est gelée** — chantier suspendu à la demande du Responsable, le test (boire une potion, un équipement qui réagit) est noté dans `TODO.md` **pour le premier donjon et à la charge du Responsable**
- feat : **le son des cinématiques se joue** — MS-ADPCM décodé par `Host/MsAdpcm.cs`, sorti par `Host/WaveOut.cs` (winmm) ; mesuré sur `TEPS.AVI` : 324 images sur 324, 21,5 s de son, **98 ms d'écart d'horloge** (la latence du tampon)
- fix : **le son était tronqué de deux tiers, en silence** — un bloc AVI porte autant de blocs ADPCM que le monteur a voulu, et `TEPS.AVI` en met **une seconde entière** (21 668 échantillons) là où `ANI01.AVI` en met une image ; le tampon de décodage était fixe à 8 192. Il se dimensionne désormais sur le bloc réel, et une troncature parle au lieu de se taire
- fix : **l'horloge du film n'est plus la position du périphérique audio** — elle ne dérive pas, mais elle se **fige** quand la sortie se vide, et une horloge figée gèle le lecteur : plus d'image due, donc plus d'image consommée, donc la réserve reste pleine, donc plus rien n'est lu, donc le son ne repart jamais. Un hoquet devenait un blocage définitif au bout de trois secondes. Le chronomètre avance quoi qu'il arrive, et sa dérive est de 0,01 %
- fix : **les lots audio en attente sont une file, pas un tampon unique** — le lot non accepté était gardé dans le tampon de décodage, que le lot suivant écrasait : le son sautait des morceaux
- feat : `OpenForesia.Tools/check-movie-audio.ps1` — compare notre décodeur à **`msadp32.acm`, celui de Windows**, échantillon par échantillon ; verdict « IDENTIQUE » sur 476 652 échantillons
- fix : **`>> 8` et non `/ 256` dans le prédicteur MS-ADPCM** — les deux ne diffèrent que d'une unité sur les négatifs, mais le résultat repart dans le prédicteur suivant : **64 % des échantillons divergeaient**, jusqu'à 309 d'écart
- doc : la leçon — **un décodeur audio ne se relit ni à l'œil ni à l'oreille**, et une troncature muette est le pire des deux mondes : le son paraît plausible et ne l'est pas. Il faut une référence et un compte
- feat : les images sont mises en réserve **compressées** en attendant leur tour — anticiper le son oblige à traverser les blocs vidéo entrelacés, et les décoder au passage ferait courir l'image devant le son
- feat : **LES CINÉMATIQUES SE JOUENT** — `TEPS.AVI` puis `OPENING.AVI` au démarrage, image par image à la cadence du film, une touche les passe ; le décodeur Indeo 4 du CD (`ir41_32.ax`) est intégré comme les cinq autres DLL de 1998, ni installé ni enregistré dans le système
- feat : `Host/AviReader.cs` — démultiplexeur AVI **en flux** : `opening.avi` pèse 463 Mo sur le CD pour 28 Mo déclarés, le charger entier coûterait un demi-gigaoctet sur le chemin de démarrage
- feat : `Platform/IndeoCodec.cs` et `Host/MoviePlayback.cs` — le codec par `ICOpenFunction`, la cadence sur l'horloge du film (15 im/s, pas le minuteur de 100 ms de l'original), l'affichage par la surface GDI de démarrage en 640×480
- feat : **la boucle de lecture a enfin son signal de fin** — `TrySetupMovieStream` rendait toujours faux pour tenir la boucle inaccessible, faute de rappel de fin ; `MoviePlayback.Step` le fournit, et l'abandon sur touche passe par la fenêtre du jeu, seule à avoir le focus en démarrage fenêtré
- doc : `Documentation/DLL/ir41_32.md`, et `cinematiques.md` mis à jour — **le son MS-ADPCM n'est pas encore joué**, c'est le morceau qui reste
- doc : `playtest.ps1` attend désormais **30 s** avant de chercher le menu au lieu de 12 : `TEPS.AVI` dure 21 s
- feat : **les cinématiques se décodent** — `OpenForesia.Tools/decode-movie.ps1` sort une image en PNG à partir des `.avi` du CD, avec le décodeur Indeo 4 **d'origine** (`ir41_32.ax`, extrait des cabinets de `DXMedia\DXMEDIA.exe` du CD) ; démultiplexage AVI et écriture PNG écrits sans dépendance, vérifié sur le logo THQ et sur le texte d'introduction français
- doc : **`ir41_32.ax` exporte `DriverProc`** — c'est aussi un codec Video for Windows, donc utilisable par `ICOpenFunction` **sans installation ni enregistrement**, dans le seul processus du jeu ; même geste que d'appeler `winsys3d.dll` plutôt que de la reconstruire
- doc : **le piège `DRV_LOAD`/`DRV_ENABLE`** — `ICOpenFunction` n'envoie que `DRV_OPEN`, et Indeo 4 prépare ses tables à la charge : sans eux le codec accepte `QUERY` et `BEGIN` puis rend `ICERR_ERROR` au premier décodage. Établi en constatant que **seize variantes de format échouaient identiquement** — quand toutes les variantes d'un paramètre échouent pareil, ce n'est pas ce paramètre
- doc : `Documentation/Reference/cinematiques.md` — les quatre films, leur format, le séquencement déjà transcrit, et ce qu'il reste à faire pour que le jeu les joue
- doc : **les cinématiques sont en Indeo Video 4.1 (`IV41`), 256x224, 15 im/s, son MS-ADPCM mono 22 kHz** — relevé dans les en-têtes de `E:\French\*.avi` ; Windows n'a plus de décodeur Indeo (`ir41_32.ax` et `ir50_32.dll` absents, seul Cinepak survit), ce qui explique que le jeu d'origine lève et passe à la suite. Le séquencement, lui, est déjà transcrit
- fix : **agrandir la fenêtre tient désormais** — le passage à l'écran suivant la ramenait à sa taille de départ tout en la laissant marquée « agrandie », d'où le double clic nécessaire sur le bouton ; `RestorePlacement` posait un rectangle par `SetWindowPos` au lieu de rétablir un `WINDOWPLACEMENT`, qui porte le régime de la fenêtre
- feat : le relevé de géométrie suit maintenant **les redimensionnements de l'utilisateur** — `WM_EXITSIZEMOVE` et `WM_SYSCOMMAND`/`SC_MAXIMIZE`/`SC_RESTORE` le rafraîchissent, là où un repositionnement subi lors d'un changement de mode d'affichage n'en émet aucun ; deux traces `[FEN] relevé` / `[FEN] rétabli` au journal
- feat : `playtest.ps1` apprend `-Maximize` (le vrai message du bouton de la barre de titre, posé dès l'écran-titre) et imprime la géométrie de fin de run
- doc : **deux pièges de fenêtre, deux faux négatifs** — `Process.MainWindowHandle` désigne la fenêtre `VfwClass` du lecteur vidéo tant que la nôtre n'existe pas (chercher par classe `OpenForesiaWindow`), et `SW_RESTORE` **dé-agrandit** une fenêtre agrandie, ce que faisait la prise de focus du script
- fix : **le carré noir en haut à droite de l'écran, à l'auberge, est l'indicateur de caméra `MAN`** — le « mot de base » du bloc de sprite valait zéro, ce qui choisit la commande winsys3d 0x2c (teinte par les 24 bits bas, donc noir) au lieu de 0x2d (texture sans teinte) ; `DrawWorldMapFrame` pose désormais le `0x1000000` que l'image précédente laisse sur la pile, valeur relevée au désassembleur en `0x0051833d`
- doc : **LB-008 réécrit — il était classé « bénin » à tort** ; le bit 24 du mot de base tombe dans l'octet de commande de la primitive, il ne teinte pas. Un défaut « latent » raisonné mais jamais regardé à l'écran l'est resté quatre jours alors qu'il se voyait
- doc : **Smart App Control est désactivé** sur cette machine depuis le 25/08 (fait par le Responsable, `VerifiedAndReputablePolicyState = 0`) — la construction Debug de `OpenForesia.Tools` se charge de nouveau, le publish Release n'est plus nécessaire ; `CLAUDE.md` corrigé
- doc : **vingt exécutions du Responsable sans un seul `0xC0000374`** — le taux d'un sur six ne tient plus ; hypothèse retenue : Smart App Control, désactivé depuis. Surveillance seulement, pas de chantier
- feat : `OpenForesia.Tools/dump-info.ps1` — lit un vidage `.dmp` (format MINIDUMP) sans aucun outil de débogage installé : durée de vie du processus, code d'exception, modules chargés, threads, balayage de pile
- doc : **Windows garde un vidage complet de chaque mort** dans `%LOCALAPPDATA%\CrashDumps`, `SORTIE SILENCIEUSE` comprise — onze dormaient sans avoir été ouverts
- doc : **le `0xC0000374` du 26/08 10:32 situé** — 70 s d'exécution, thread principal dans la boucle par image de `winsys3d` (ordinaux 701 et 132), levée sur un thread COM/RPC qui allouait ; ce n'est donc ni la constitution du groupe ni un chargement, et le thread qui lève n'est pas le coupable
- doc : **la corruption de tas intermittente est mise de côté** — plus de chantier ouvert dessus, la section de `TODO.md` passe en « EN ATTENTE »
- doc : **une corruption de tas intermittente (`0xC0000374`) est antérieure à aujourd'hui** — cinq occurrences le 25/08 dans le journal des événements Windows, une le 26/08 ; environ une exécution sur six. Ni le menu rapide ni le script d'objet n'en sont la cause, une trace fichier posée à l'entrée des deux ne s'est jamais écrite. À ouvrir comme chantier à part
- feat : **la machine à opcodes des objets tourne** — `FUN_00523aa0` et ses **23 opcodes** transcrits, dont **onze corps que Ghidra n'avait pas décompilés** (des `LAB_`, vus seulement pris en adresse dans `PTR_LAB_00564f80`) ; désassemblage gardé dans `Legacy/Raw/Blaze/FUN_00523aa0-handlers.disasm.txt`
- feat : **le second point d'entrée de cette machine** — `FUN_005238e0` transcrite en `TriggerEquippedItems_005238E0` : chaque objet équipé porte un script *déclenché* en `item[0x82]`, distinct de celui de `item[0x80]` qu'on lance en s'en servant. Son ancien nom, `SetEntityAnimationState`, était faux — rien dans ce corps ne touche à une animation
- feat : trois aides que le tout demandait, écrites au passage — `FUN_0053a980` (le déclenchement de sort, qui était une levée), `FUN_00523a20` et `FUN_0050f070` (la particule)
- doc : `item[0x84]` est un **identifiant d'événement**, pas une catégorie — deux sites le recoupent, et la table `DAT_0052403c` le réduit à un bit par un `switch` de neuf bras dont sept sont des doublons
- doc : **correction d'une affirmation de ce matin** — « les 23 gestionnaires sont tous dans `Blaze.c` » était faux ; elle avait été déduite de la bande d'adresses au lieu d'être vérifiée nom par nom
- doc : un `CONCAT31` de Ghidra tronqué par un cast empile **les deux** arguments dans le mot ; lu vite il n'en montre qu'un, et `FUN_0053a980` avait perdu la famille de sort au premier jet
- doc : `TODO(REVERSE)` sur l'opcode 22 — il rend un **curseur de liste d'affichage** là où la boucle attend un curseur de script, défaut de 1998 latent ; non versé dans `legacy-bug.md`, qui ne recense que l'observé
- feat : **le menu rapide fonctionne** — `FUN_00530070` transcrite (`TickQuickMenu_00530070`, 2 262 octets, deux étages, dix genres de catégorie) ; espace ouvre l'anneau au-dessus du personnage, les flèches changent de catégorie et d'entrée, la validation équipe et recalcule les statistiques, et la croix referme
- fix : **la bulle du menu se dessinait dans le coin haut gauche** — `DrawSpeechOverlay_005309B0` posait son origine (`0x005de154`/`_158`/`_15c`) et lisait son zoom (`0x005de160`) *par l'arène du menu d'objets*, alors que `UiWindow` lit les champs nommés à ces mêmes adresses ; l'écriture n'atteignait jamais le lecteur
- fix : **`stackalloc short[2]` pour un `ProjectPoint` qui écrit huit octets**, deux fois (la surimpression et le balayage de cellules) — le témoin de pile que le JIT pose sur toute méthode à `stackalloc` tuait le processus par `0xC0000409` dans l'épilogue, sans une ligne de journal
- doc : **le chevauchement supposé de `DAT_0056a851` n'existe pas** — `0x0056a850` est l'octet de compte de sa rangée, il borne l'index sous 7, et rend du même coup ses deux `-1` inatteignables ; les deux symboles sont une seule table
- doc : `DAT_00564600`, le nombre de sorts par école — **trois écoles**, 29/24/20, établi par la table de pointeurs voisine et par la rangée cumulée `DAT_00564608`
- feat : `playtest.ps1` apprend le jeton `SPACE` (triangle, l'ouverture du menu rapide) et dit désormais **« SORTIE SILENCIEUSE »** quand le jeu meurt de lui-même sans rien écrire — le verdict annonçait « aucun plantage » sur un processus mort

## 2026-08-25

- fix : **la pastille de classe autorisée sortait en rectangle blanc** — `(-(bit) & 0xfffffff9) + 9` vaut **2** quand le bit est levé, pas 0 : `0xfffffff9` est **−7**, pas un masque ; corrigé dans la fiche d'objet du menu **et** dans le coffre, qui portaient la même lecture
- doc : le réflexe — un `& 0xfffffffN` suivi d'un `+ N'` est un `SETcc` déguisé en arithmétique, jamais un masque ; calculer les deux cas avant d'écrire le ternaire
- fix : **deux fautes de transcription du coffre, qui ont corrompu une sauvegarde** — `DAT_005dff3c[1]` est l'octet de **+4** (Ghidra type le pointeur en dword, `movzx cx,byte [eax+4]` le confirme), pas de +1 : d'où le portrait bariolé ; et le succès de l'écriture pose l'état **9**, pas 99 — poser 99 rebouclait sur l'écriture à chaque image
- doc : la règle qui manquait — avant d'indexer un `DAT_` de Ghidra, chercher un `*DAT_ = *DAT_ & …` dans le même corps : c'est lui qui donne la largeur du pointeur, donc le pas de l'indice
- doc : et l'autre — quand deux chemins d'un `switch` partagent leurs dernières lignes, vérifier qu'ils partagent aussi celles d'avant ; ici le `break` du succès était à l'intérieur du `if`
- feat : **LE COFFRE EST COMPLET** — les états 0, 1, 5 et 8 écrits, dont les trois qui touchent le disque ; `FUN_0053e0a0` ne porte plus aucune levée, ses deux modes et ses vingt états sont transcrits
- doc : la somme de contrôle porte sur des octets **signés** (`0x80` y compte pour −128), les trois écritures sont combinées par **ou binaire** et testées une seule fois, et le drapeau de succès est posé **en dernier** — le poser plus tôt rendrait la sortie destructrice
- doc : le compactage réécrit les cinq octets d'équipement de `+0x2b0` qui désignaient les anciens indices — sans ça, refermer le coffre déséquiperait ce que le joueur portait
- doc : troisième enchaînement sans `break` dans la même fonction (le cas 0 de l'état 5 tombe dans son cas −1) — c'est la façon de faire de ce code, pas un accident
- feat : **les états 0x61, 0x62 et les cinq négatifs du coffre** — l'attente de périphérique, l'échec franc et le retour au choix ; il ne reste que l'entrée (0) et les trois écritures de fichier (1, 5, 8)
- doc : les **deux automates de la fonction communiquent par une seule globale**, `DAT_005dff24` — le `-3` que quatre états testent est ce que rend `FUN_00504820`, rangé là par l'automate d'attente
- doc : reposer `DAT_0056dc90` à -1 est **le seul chemin de retour** vers le choix de périphérique — ce qui éclaire rétrospectivement la branche morte du prologue : c'est un état à part, entré et quitté par cette valeur
- doc : `-5` tombe dans `-4` comme `0x5f` tombe dans `0x60` — le même motif deux fois dans la même fonction, donc une façon de faire et non un `break` oublié
- fix : **les quatre descripteurs d'interface sont pré-réclamés au démarrage** — leur allocation paresseuse échouait à l'ouverture du coffre (`0x19B000` libres mais `0xF000` d'un tenant) ; après correction le plus grand intervalle passe à `0x640000`
- doc : sixième occurrence de la même leçon — **le moment où une allocation basse est prise fait partie de sa définition** : au démarrage elle rend `false` et le processus se relance, en cours de partie elle tue la session
- feat : **six états du coffre de plus** — 6 et 7 (les confirmations), 9 (la sortie), 0x5f et 0x60 (l'abandon), 99 (la reprise après échec)
- doc : le `0x5f` **tombe dans** le `0x60`, `break` manquant compris — les deux états distinguent « il y avait un transfert » de « il n'y en avait pas » ; et l'état 7 est le seul à s'arrêter tant que l'état n'a *pas* changé
- feat : **l'état 4 du coffre** — trois affectations, l'aiguillage vers l'écriture ; la remise à zéro des deux drapeaux synchronise les deux automates pour que l'état 8 entre par sa branche de travail dès le premier tour, pas un tour plus tard
- feat : **l'état 3 du coffre** — la liste des objets rangés, sa fenêtre de cinq lignes, la fiche de l'objet visé (huit valeurs, deux portées, huit pastilles de classe) et le basculement du drapeau « garde »
- doc : le `CONCAT31` de Ghidra sur l'`extra` des sprites **n'existe pas** — deux `xor cx,cx` / `xor dx,dx` de seize bits laissent la moitié haute des registres, et `DrawSpriteVariant` ne lit que les seize bits bas ; troisième artefact du même genre dans la journée
- doc : trois idiomes MSVC mis en ligne (`strlen`, `strcpy`, `strcat`) rendus par ce qu'ils font ; la ligne visée est dessinée **quatre fois** décalée d'un pixel, c'est son liseré
- doc : annuler va directement à l'état 9 **si rien n'a changé** — même compte et mêmes quantités que la copie posée par l'état 2, ce qui évite de réécrire le fichier pour rien
- feat : **l'état 2 du coffre** — la lecture du magasin : mise de côté des objets du joueur, lecture du fichier sous contrôle de somme, fusion des piles identiques, vingt-deux vignettes en VRAM, recensement des deux côtés
- doc : le prédicat d'« objet identique » (seize octets + la clé de `+0x86`) apparaît pour la **troisième** fois — expertise, transfert entre membres, et maintenant la fusion du coffre : c'est *la* définition du jeu, pas une coïncidence
- doc : le total fusionné est borné à 99 par **deux écritures d'octet**, pas par une saturation de `short` — la différence se verrait au-delà de 255
- doc : écart déclaré — les deux rectangles VRAM sont des locales de pile dans l'original, ici deux blocs propres ; reproduire le pointeur pendant serait du comportement indéfini, pas de la fidélité
- feat : **la queue d'affichage du coffre** — le curseur de périphérique, le portrait, les icônes, les quatre lignes de texte et les **quatre** panneaux ; c'est elle qui produit le cadre du cliché de référence
- doc : `DAT_00569f88` n'est pas une globale — c'est `MenuDescriptor(3)`, à `0x1a58` de `0x00568530`, soit trois descripteurs pile ; le mode 0 en dessine quatre là où le mode 1 n'en dessine que deux
- doc : le quatrième libellé est **animé** — `(compteur d'images / 4) % 3` ajouté à `+0x151` fois trois, ce qui ne saute pas aux yeux dans la pseudo-C
- doc : `DAT_005dff7c` est **signé** (états -1 à -5), et l'original n'a **pas de `default`** dans son switch : une valeur hors liste ne fait rien, ce que la transcription reproduit
- feat : le socle du mode 0 (le coffre) — trente-neuf globales et **huit tables de données relevées dans le binaire** : identifiants de périphérique, listes de lignes avec leur compte, positions d'écran, deux listes d'icônes, deux enregistrements d'atlas et les dix puissances de dix
- doc : le prologue du mode 0 teste `DAT_0056dc90 < 0` trois fois et le premier bloc met la globale à zéro — les deux tests suivants et leur `goto` sont **morts dans tous les cas**, établi au désassembleur (`mov [56DC90h],esi`, `esi` à 0)
- doc : le corps du mode 0 n'est **pas** écrit et n'est pas déclaré fait — vingt états, des sauts d'un cas à l'autre, et trois chemins qui écrivent le fichier de sauvegarde ; la pseudo-C ne suffit pas, il a fallu le désassemblage pour un seul point de flot sur soixante lignes
- fix : **le coffre ouvre enfin son panneau** — l'appel `PTR_FUN_0056afac` de l'état 4 de `EntityHandler_00450760` avait été **supprimé sur la foi d'une souche `ret` nu** ; c'est l'entrée 57 de la table de verbes, et pour la zone 0 elle vaut `0x0053DEA0`
- doc : une entrée de table n'est une souche que **pour une zone donnée** — ces tables sont installées par zone, et figer « rien à émettre » généralise ce qui n'a été vérifié que pour une
- fix : **les deux modes de `FUN_0053e0a0` étaient inversés** — le mode 0 est le coffre de stockage (sa persistance sur fichier est son magasin), le mode 1 une boutique ; j'avais conclu sur ce que le corps *appelle* sans regarder ce qu'il *dessine*
- feat : cinq sondes permanentes pour aller d'un geste en jeu au corps à écrire — `-Trace VERBS`, `-Trace SCRIPT` (scripts, opcodes, `[ETAT]`, `[TOUCHE]`, `[ALU]`, `[SAUT]`, `[CHOIX]`) et `-Trace ENTITY` (`[SOUCHE]`)
- doc : `RunWorldMapTick` n'est **pas** appelée une fois par image — elle contient la boucle d'images ; une sonde posée à son entrée ne s'allume qu'une fois, ce qui a failli me faire conclure qu'elle ne tournait pas
- doc : **l'écran du coffre est identifié** — c'est l'onglet 1 de la fiche de personnage, établi par comparaison capture contre capture : mêmes quatre objets, même `ATT 7 / DEF 0`, mêmes huit colonnes `Gue Pre Vol Mag Cha Elf Nai Lut` (les huit **classes**, pas des résistances)
- doc : deux écarts de notre rendu sur ce même onglet — la liste est horizontale là où l'original l'empile, et la description affiche le libellé de remplissage « Fenêtre Objets » au lieu du texte de l'objet
- feat : `-Trace SCRIPT` — chaque script lancé et chaque opcode exécuté ; avec `-Trace VERBS`, un seul run sépare « le script ne tourne pas » de « il tourne sans effet » et de « il appelle tel verbe »
- fix : **le mode 1 de `FUN_0053e0a0` n'est pas le coffre de stockage** — le cliché du jeu d'origine le montre : quatre lignes, `ATT`/`DEF` et une rangée des huit classes, là où le mode 1 dessine cinq rangées avec une étoile et un nombre contre le bord droit ; et sa logique **paie un prix** pour recevoir un objet, donc un achat
- doc : le voisinage dans une table de verbes n'est **pas** une preuve de rôle — le corps était juste, c'est le nom qui affirmait ce que rien n'avait établi
- feat : `-Trace VERBS` (permanent) — chaque appel à l'opcode 0 imprime le verbe, l'adresse résolue et l'opérande ; c'est la seule façon d'aller d'un geste en jeu à la fonction à transcrire, un script de zone ne se lisant ni dans `Blaze.c` ni au désassembleur
- feat : **le crochet du coffre** (`0x0053DF90`) et son émetteur d'icônes (`FUN_005412f0`), tous deux absents de `Blaze.c`
- feat : **le coffre s'ouvre** — mode 1 de `FUN_0053e0a0` transcrit : la liste des dix emplacements, la grille de cinq rangées, le paiement, la remise de l'objet et les trois sous-états
- doc : `FUN_0053e0a0` est **deux écrans**, pas un — le mode 1 est le coffre (195 lignes, fait), le mode 0 la **carte mémoire** (1150 lignes, non transcrit, lève en se nommant)
- doc : onze des 39 adresses du corps **ne sont pas des globales** mais des champs des descripteurs d'interface, au pas de `0x8c8` — les déclarer aurait donné onze paires « deux noms pour une adresse »
- doc : Ghidra rattache trois arguments au mauvais appel (`add esp,4` puis `add esp,10h`) ; le binaire tranche, et c'est la deuxième fois que ce piège se présente
- feat : **les verbes 57 et 58** (`FUN_0053dea0`, `FUN_0053df20`) — ils ouvrent le panneau de `FUN_0053e0a0` : membre, mode, bit `0x40` des drapeaux de monde, neuf dwords de curseur remis à neuf
- fix : **ma lecture de la carte des symboles était fausse** — la colonne est l'**écart au symbole suivant**, pas la taille ; `s_Particles_005de824` alloue 4992 octets et non 6060, et aucune des onze globales des deux verbes ne tombait dedans
- doc : le vrai chevauchement est ailleurs — la **file de sons différés** (`0x005dfb90`, 32 × 0x14) commence 0x14 octets avant la fin nominale des particules, mais celles-ci n'écrivent jamais au-delà de l'offset 0x27 de leurs entrées de 0x9c : 97 octets de marge
- doc : ne jamais modéliser cette file comme une vue dans `s_Particles_005de824` — ce serait deux noms pour une adresse
- feat : **le verbe 56, l'expertise d'un objet** (`FUN_0053dd20`) — il trouve le premier objet non identifié, le nomme dans le libellé de zone, remplit le panneau, puis propage l'identification à toute la pile
- doc : le relevé de cette fonction dormait dans `Legacy/Raw/` depuis le 2026-08-16 — **y regarder avant de relancer un dump**
- doc : le `CONCAT22` de Ghidra cache **deux écritures de largeurs différentes** (un dword puis une moitié haute en 16 bits) ; le drapeau `DAT_005e909a` est posé **puis rétracté** ; les trois seuils de valeur sont **non signés**
- doc : les verbes voisins 57 et 58 sont triviaux mais **pas écrits** — neuf de leurs dix globales tombent **à l'intérieur** de `s_Particles_005de824`, et les déclarer en globales propres recréerait le défaut d'aliasing que `MemoryLayoutTests` surveille
- feat : **les six balayeurs d'entités de scène sont complets** — les trois derniers (`0x00405D60`, `FUN_00405e70`, `FUN_00405fe0`) et leurs trois corps de réveil manquants, transcrits d'un bloc au lieu d'un par salle
- doc : le binaire les range lui-même — entrées 32 à 37 de `0x0056b0c8`, sous-types `0xe0` à `0xe5`, les deux suivantes retombant sur la souche vide ; la borne vient de la table
- doc : `0x00405D60` se cache **dans** ce qu'une lecture rapide prend pour la fin de `FUN_00405d20` — sept `nop` d'alignement les séparent
- doc : les quinze triplets `(état, primitive, argument)` des trois corps vérifiés par **extraction des deux côtés** et comparaison, pas à l'œil
- doc : `FUN_00405f50` est le seul des neuf appelants de `FUN_004052f0` à passer une pause d'inactivité **fixe** ; les huit autres tirent au hasard
- feat : **le changement de musique de la femme à la petite harpe** (`FUN_00404e50`, absent de `Blaze.c`) — un décompte de 300 par image sur deux champs du bloc d'écran, puis la piste, puis le désarmement
- fix : le bloc d'écran `0x00566c48` était alloué **à zéro** ; son contenu `.data` initial est relevé et modélisé — sans lui, la transition musicale serait déjà finie à la première image
- doc : un bloc `.data` alloué à zéro n'est faux que le jour où quelqu'un le **lit** avant de l'écrire ; tant qu'il n'a qu'un écrivain complet, rien ne le montre
- doc : le choix de piste s'écrit ici en cascade `cmp al,1` / `cmp al,2` là où le verbe de zone emploie un `setne / dec / and 2` — deux codegens pour la même identité bornée 0/1/2
- feat : **le troisième balayeur d'entités de scène** (`FUN_00405a70`) et ses quatre corps de réveil manquants — c'est ce que la salle derrière le barman réclamait après l'opcode 17
- doc : le numéro de type est **relatif au balayeur**, pas au corps : `FUN_00405680` est le type 3 de l'un et le type 0 de l'autre ; les nouveaux corps ne portent donc pas de numéro
- doc : `FUN_00405be0` est mot pour mot `FUN_00405770` — vérifié, pas supposé : même taille, aucun `call` différent, seuls les sauts internes bougent
- doc : le corps était **dans `Blaze.c`** (ligne 2598) alors que le message de levée proposait le désassembleur ; sa condition « s'il est absent de Blaze.c » se lit avant de suivre sa suggestion
- doc : les dialogues de biais confirmés en jeu par Vincent — la racine de ressources 3 et LB-013 sont closes, toutes deux tombaient sur ce geste
- feat : **l'opcode 17 de l'interpréteur de script est transcrit** — c'est le changement de salle : il note zone, sous-zone et point d'entrée, marque l'entité comme partante et demande le rechargement
- doc : `script[1]` et `script[2]` portent `0xff` pour « ne pas changer », `script[3]` **non** — deux `cmp cl,0FFh` dans le binaire et pas de troisième
- doc : le transfert scripté pose le pas de boucle à **2**, la sortie marchée à **1** ; seul le 2 fait replacer le groupe au point d'entrée — les deux écritures ne se comprennent que l'une par l'autre
- doc : `FUN_00515a10.asm` était tronqué à 96 octets (corps de 110) ; deuxième fois qu'un dump repris plus large débloque un corps d'un coup
- fix : **le plantage en parlant de biais à un PNJ** — le balayage par cellules de `DrawChunk` lit une clé de saut deux octets après la fin de sa table, et seul l'octant 7 en sort ; `GameMemory.NextCellSkipKey` rend désormais les vrais octets de `PTR_DAT_005645d8`
- doc : LB-013 au catalogue — premier défaut d'origine qui **plante la reconstruction sans avoir jamais rien cassé en 1998** ; `ReadOnlySpan` lève là où x86 lisait paisiblement chez le voisin
- doc : LB-009 protège `DrawWorldCells` de LB-013 — sa lecture d'un seul octet arrête le curseur avant la fin de la tranche ; un bug de 1998 en masque un autre
- doc : les huit octants font **tous** huit sauts et lisent chacun d'avance la première clé de la tranche suivante ; c'est la simulation hors jeu qui l'a établi, la lecture à l'œil de la table s'était trompée
- fix : le moteur de texte ne connaissait que **deux** racines de ressources sur cinq — toute boîte de dialogue passant par la racine 3, le répertoire de zone, levait `NotImplementedException` dans `FUN_00544a80`
- doc : les cinq dwords à partir de `0x005e97c0` sont cinq racines, toutes issues de `FUN_00529b50` ; la sixième, `0x005e97d4`, n'en est pas une — c'est ce qui borne la levée à 5 et non à 256
- doc : les quatre gestionnaires qui indexaient `&DAT_005e97c0` avaient chacun leur copie du calcul ; remontées dans `UiText.ResourceRoot`, une seule reconstruction pour une seule séquence du binaire
- doc : les appels à `ResourceRoot` du trajet de l'auberge viennent **du menu de jeu**, pas de la boîte « - La réception - » — une sonde le montre, la lecture du run ne le disait pas

- feat : **le menu de jeu est entièrement transcrit** — les onglets 3 (les sorts) et 4 (les herbes) faits ; six pages et six onglets, plus aucun corps ne manque à `FUN_0052a850`
- feat : **l'onglet des sorts marche** — « Niveau Magie 1/6 », les cinq pictogrammes, « Balle de feu », son coût « MA 2 » et ses pictogrammes d'élément ; le changement de page et le curseur répondent
- feat : **l'onglet des herbes marche** — les sept entrées (Lavande, Sauge, Verveine, Mercuriale, Fenouil, Hysopius, Belladone), leur compte, le marqueur de sélection et la description en bas
- doc : la liste courte de `DAT_005de6a0` **encode l'état dans le signe** — un sort inconnu y entre par son opposé ; le numéro 0 est donc ambigu et se lit toujours comme appris, ce que l'écran montre
- doc : les **mêmes huit octets** de `+0x160 + école * 8` sont un champ de bits pour l'onglet 3 et un octet par entrée pour l'onglet 4 — c'est la classe qui décide, et la table d'onglets fait qu'un personnage n'a jamais les deux
- doc : le verrou de mise au point n'affiche pas des triches, il **écrit** — à zéro, la boucle de l'onglet 3 apprend chaque sort qu'elle parcourt
- feat : les tables `0x00564608`, `0x00564610`..`0x00564640` et `0x0056a978` relevées — trois écoles, six pages chacune, et le pointeur nul qui borne le compte
- feat : **le menu de jeu est complet** — la page 5 (« Options ») transcrite, et avec elle les six pages de `FUN_0052a850` ; il ne reste que deux onglets de la fiche, qui demandent une autre classe
- feat : **la sauvegarde écrit pour de bon** — « Mémorisation achevée. » à l'écran et un fichier de `0x6000` octets neuf dans `save\` : énumérer, ouvrir, comparer les sommes, fusionner la table d'icônes, coller le niveau, recopier huit banques, capturer la palette, écrire en trois morceaux
- feat : « Retour au menu d'accueil ? » marche aussi — la boîte de confirmation s'ouvre et attend
- doc : le titre du fichier de sauvegarde se termine par un **mot sentinelle**, `0x6b82` relevé en `0x0056ac2c` ; le niveau s'écrit après lui en glyphes de seize bits, pas en ASCII
- doc : `DAT_0060498a` était déclaré `byte` avec la note « aucun lecteur trouvé » — le lecteur est cette page, qui lit les deux moitiés de `0x00604988` **en seize bits** ; élargi
- feat : les tables d'emplacements de carte `0x0056a98c`..`0x0056aa18` relevées — quatre configurations de multitap, leurs comptes, et les dix positions ; la plage se referme sur `s_ConfigRowOffsets_0056aa18`
- feat : **la carte du monde du menu marche** — page 4 : l'image défile, le groupe s'y dessine en modèle 3d, et les huit repères se posent et s'effacent
- doc : le défilement se fait **dans le descripteur d'atlas**, pas sur le sprite — les deux premiers octets de `**(DAT_005e97cc + 0x94)` sont réécrits à chaque image ; c'est la fenêtre lue dans la texture qui bouge
- doc : les quatre directions de cette page sont lues sur le mot **0** de la manette, l'état *tenu*, et non sur un front — c'est ce qui rend le défilement continu, et c'est la seule page qui fasse ça
- doc : un repère fait huit octets ; son `short` de `+4` négatif veut dire « vide », mais l'effacement en écrit **quatre**, pas deux
- feat : le recadrage de la carte sur le meneur extrait en `RecentreMapOnLeader` — le binaire l'écrit **quatre fois en toute lettre**, une dans le prologue et trois dans la page 4
- feat : **la page « Organisation du groupe » marche** — page 3 du menu : le choix du membre, sa couleur, le passage du rôle de meneur et la mise de côté ; c'est la première page atteignable sans sonde
- doc : l'octet `+0x2bd` du personnage est sa **couleur**, pas un rang de formation — le panneau affiche « Couleur 0 » et deux appuis sur haut font passer les cheveux du portrait de bleu à brun
- doc : donner le rôle de meneur **échange** deux sièges (`+8` et `+9` de l'entrée `DAT_005e9180`), il ne les copie pas — c'est l'ancien meneur qui hérite de la mise à l'écart
- doc : trois indexations différentes dans la même boucle de dessin — l'abscisse et les enregistrements de manette par l'**index de personnage**, la couleur par le **rang de la boucle**
- feat : **la page « Table » marche** — page 2 du menu : la dalle de pierre gravée et ses neuf cristaux, une image par bit levé ; c'est la seule des six pages qui ne lit aucune touche
- doc : la dixième tuile de la dalle **est la dalle elle-même** (0x80 × 0x70 en `0x00564740`), posée en dernier donc derrière les cristaux — le bit 9 n'est pas un cristal de plus
- doc : `0x005646f8`..`0x0056476f` nommé — neuf descripteurs de cristal, celui de la dalle, et les dix positions ; la borne de boucle du binaire est une **adresse**, `0x00564770`, pas un compte
- doc : une sonde posée sur `DAT_005e9000` ne tient pas — le prologue rappelle `FUN_00535710`, qui reconstruit la banque par OU des blocs `+0x2e0` de chaque membre ; il faut sonder dans cette fonction-là
- feat : **la page « Objets » marche** — page 1 du menu : la liste des objets de quête, l'entrée sélectionnée colorée et sa description en dessous (« Pierre étrange qui émet une lumière blafarde »)
- doc : les trente-deux premiers bits de `DAT_005e9840` sont les **objets de quête** du groupe — établi à l'écran par une sonde qui en lève quatre, pas déduit ; c'est pourquoi la page se saute tant que le dword est nul
- doc : le défilement de cette page est un **remplacement, pas un cadrage** — au-delà de douze entrées, les `compte - 12` premières s'écrivent `"..."` et gardent leur place
- feat : `playtest.ps1` connaît **`PGUP` et `PGDN`**, les deux gâchettes qui changent de page dans le menu ; `PGUP` avance et `PGDN` recule, l'inverse de ce que les noms suggèrent
- feat : **la configuration de la manette marche** — onglet 5 de la fiche : les trois rangées avec leurs pictogrammes, le curseur qui les parcourt, le type de manette en cycle (« Type A/B/C ») et les deux bascules « Activé » / « Désactivé »
- doc : les quatre gestes de l'onglet 5 lisent **trois mots de manette différents** là où `Blaze.c` les écrit tous pareil — la rangée change sur le mot répété, le type recule sur le mot pressé et avance sur le répété
- doc : le `case 3` du saut indexé de `0x0052fa4c` est **inatteignable** (la borne 0..2 est deux instructions avant) mais transcrit quand même : c'est le remappage des quatre boutons, vingt-quatre permutations en `0x0056aa38` et quatre masques en `0x0056aa98`
- doc : deux racines de ressources dans le même corps — le curseur de rangée lit `DAT_005e97c0`, les cadres `DAT_005e97c4` ; seul le désassembleur le dit
- feat : les six étiquettes de `0x0056abe8` relevées, et les deux tables de pointeurs qui y mènent ; « ␣␣␣Activé » porte trois espaces de tête, c'est ce qui aligne la colonne
- feat : **l'équipement marche** — onglet 2 de la fiche : les cinq rangées avec leurs pictogrammes et leurs noms, le choix d'un emplacement, l'ouverture de la grille, et l'échange lui-même (valider un objet déjà porté le retire)
- feat : les six flux d'étiquettes de `0x0056ac30` relevés — quatre octets chacun, pris **à l'envers** par la boucle de dessin
- doc : le nombre d'emplacements dépend de la classe (quatre pour les classes 0 et 6, trois sinon), et **l'emplacement retenu n'est pas la position du curseur** : au-delà de la deuxième ligne une classe à trois emplacements saute d'un cran, et la boucle de dessin saute la même rangée sans avancer son indice de libellé
- doc : poser un objet est refusé par **deux** tests — le bit que l'emplacement exige dans les drapeaux de l'objet, **et** le même bit dans le mot `+0x112` du personnage ; les deux mains partagent `0x800` et se refusent l'une l'autre
- doc : le `switch` qui choisit ce bit n'a **pas** de branche par défaut dans le binaire — au-delà de 4 le registre garderait sa valeur précédente ; la transcription lève plutôt que d'inventer
- feat : **l'inventaire s'affiche et répond** — onglet 1 de la fiche : la grille d'objets, les marqueurs « équipé », les quantités, le curseur de case, la fiche de l'objet visé, ses huit pictogrammes d'élément et sa description
- feat : `FUN_0051bd50`, `FUN_0052fba0` et `FUN_0052fdc0` — **les quarante-trois appelées du menu sont maintenant toutes transcrites** : ce qui reste est du corps, plus aucune dépendance
- feat : le bloc de sprites `0x0056aae0`..`0x0056abe8` relevé, ses **deux bornes mesurées** — table de mots en dessous, chaîne cp1252 au-dessus ; et `0x0056a8d8`, qui referme le trou laissé entre deux tables voisines
- doc : `FUN_0051bd50` recopie la suite du nom **à partir de l'indice 1**, sans condition — le format est `<un caractère>/<le reste>`, ce n'est pas un défaut mais une convention de données
- doc : Ghidra ne récupère qu'un paramètre à `FUN_0051bd50` alors que le site en pousse quatre — `__cdecl`, l'appelant nettoie, les trois autres sont ignorés parce que le site partage sa forme avec cinq chemins voisins
- doc : le curseur d'inventaire vaut **99** quand rien n'est visé, et les quatre directions y reviennent dès qu'elles sortent de la liste ; elles ne s'excluent pas, une diagonale prend la dernière
- doc : recouvrement latent relevé et **non corrigé** — `DAT_005de0c8` finit à `0x005de109` et mord de neuf octets sur `s_ItemMenuArena_005de100` ; la divergence ne se voit qu'avec un nom de 63 caractères
- feat : **la fiche de personnage s'affiche** — page 0 du menu, son onglet 0 et l'épilogue partagé des six pages : portrait, nom, classe, élément, niveau, or, PV/PM, sept caractéristiques, quatre résistances, expérience
- feat : `FUN_0052fab0`, la liste de décorations — la première des quatre appelées qui manquaient au menu
- feat : la rangée de quatre descripteurs d'interface `0x00568530` semée depuis le binaire ; le compte se déduit de deux bornes qui se rejoignent, il n'est pas deviné
- fix : `s_Unknown_005de708` était déclarée `uint` alors que le binaire la teste en signé (`test eax,eax ; jge`) — c'est ce qui fait boucler la sélection de membre sur le dernier
- doc : troisième piège de Ghidra dans le même corps — `if ((a == 1) || (X = 0, a == 2)) X = 1;` n'est **pas** un `if/else` : la remise à zéro n'a lieu que sur le chemin `a != 1`
- doc : les cinq passes de statistiques tournent **deux fois par image** sur la fiche, une fois dans la tête de page et une fois dans l'onglet — c'est dans le binaire aux deux endroits
- doc : le bloc `+0x100` du personnage est un **flux d'étiquettes**, pas une structure de nombres : c'est ainsi que les statistiques arrivent à l'écran
- doc : l'onglet affiché n'est pas la position du curseur — `DAT_0056a898` donne à chaque classe sa propre liste d'onglets ; vérifié en jeu
- feat : la branche courte d'affichage de `FUN_00516fc0` (0x005179da..0x00517a77) — le dernier trou de la boucle par image : la touche de menu ne plante plus dessus
- feat : `FUN_0052a7d0` et la charpente de `FUN_0052a850` (nouveau `Screens/State03/WorldMapMenu.cs`) — navigation entre les six pages, sauts conditionnels, fermeture, cadrage ; chaque page restante lève en se nommant avec sa plage de lignes
- doc : **l'agent peut ouvrir le menu lui-même** — F2 est le jeton `START` de `playtest.ps1` et le menu s'ouvre sur place, sans conduire le personnage : les six pages sont vérifiables à l'écran, une par une
- doc : la chaîne qui trouve l'emplacement de manette a **une indirection de plus** dans la branche courte que dans celle de `PTR_FUN_0056aff0`, vingt lignes plus haut — deux écritures différentes du même geste, transcrites telles quelles
- doc : `DAT_005e9840` est comparé en **dword** et `DAT_005e9001` en octet, là où `Blaze.c` écrit les deux pareil ; et Ghidra réutilise `iVar29` de part et d'autre du switch de pages
- doc : `DAT_0056a898` — huit lignes de six octets, une par classe : le nombre d'onglets de la fiche et leurs identifiants ; toutes commencent par l'onglet 0
- doc : il ne manque que **quatre appelées** au menu (`FUN_0052fab0`, `FUN_0052fba0`, `FUN_0052fdc0`, `FUN_0051bd50`, 287 lignes en tout) — les trente-neuf autres sont déjà transcrites
- feat : `FUN_004055C0` et ses cinq réveils (types 3 à 7) — le second balayeur d'entités de scène : c'est le plantage de la salle du barman
- fix : l'affirmation de ce matin, « `0x00403710` est le **seul** créneau atteignable de `FUN_0047aa70` », était à moitié fausse — vraie pour la branche basse, muette sur la branche haute, où six sites écrivent directement dans `DAT_005ff400` et posent des sous-types `>= 0xC0`
- doc : compter les écrivains d'un **symbole** ne compte pas les écrivains d'une **adresse** — un `grep` sur `DAT_005ff405` ne voit ni un `memcpy` vers `DAT_005ff400`, ni un enregistrement posé en bloc par le chargeur de zone
- doc : le type 3 est le seul des huit réveils qui **déplace** son entité (aller-retour de 0x22 sur `+0x32`), et le type 4 en est la copie exacte sans ce déplacement
- doc : `Documentation/Etat/NOTE_DEVELOPPEUR.md` — le registre des traces humaines restées dans le binaire : la note à Imai, les deux phrases japonaises oubliées, « Invertaire », « Gurkha » et l'écran d'outil qu'il ouvre
- feat : `FUN_00403710`, l'effet de soin — le seul créneau que la **branche basse** de `FUN_0047aa70` puisse atteindre : `&DAT_005ff405` n'a qu'un écrivain, et ses six sites d'appel passent tous `0x59`
- feat : `FUN_00445860`, l'afficheur d'énoncé — le premier des 121 corps décompilés des trois tables statiques, celui que le TODO désignait
- fix : `s_EntityParams_00551640` était une `AllocZeroed` alors que le binaire porte ses 0x200 octets initialisés — `FUN_00445860` y pose un pointeur de donnée, qui rendait 512 octets nuls
- doc : deux flux d'étiquettes inédits relevés dans cette plage — « 開かない！？ » (« ça ne s'ouvre pas ! ») en `0x00551648` et « 遠くで音がした。 » (« un bruit, au loin ») en `0x00551720`
- doc : une plage sous `0x00571400` modélisée par `AllocZeroed` est une hypothèse, pas un fait — le binaire y porte du contenu, et `at` le dit en une commande
- doc : la division par 100 que `Blaze.c` écrit en somme de trois termes est l'idiome `imul 0x51EB851F` ordinaire — les deux termes correctifs s'annulent, c'est `/ 100`
- doc : `+0x148`/`+0x14a` sont bien le maximum et `+0x14c`/`+0x14e` le courant, tenu maintenant par deux témoins ; mais `FUN_0053dcb0` recopie l'un dans l'autre à chaque image, ce qui reste inexpliqué — TODO(REVERSE)

## 2026-08-24

- feat : la table statique `PTR_LAB_00552230` de `FUN_0047acf0` relevée — les trois passes de `0x0047axxx` ont leur branche basse câblée, et **303 des 576 créneaux** se comportent correctement
- doc : `PTR_LAB_` ne veut **pas** dire « absent de `Blaze.c` » — 121 des 273 corps y sont décompilés ; l'affirmation contraire, écrite le même jour, généralisait un échantillon de sept adresses
- feat : la table statique `PTR_LAB_00551f30` de `FUN_0047ab70` relevée — 192 entrées, 84 cibles distinctes, dont **109 créneaux** qui pointent sur la souche de désengendrement et deviennent corrects d'un coup
- doc : les deux tables statiques relevées sont **disjointes** — une seule cible commune, la souche, et pas un seul gestionnaire réel partagé : ce sont deux familles d'entités, pas deux vues d'un même jeu
- doc : les trois tables de 0x300 octets (`0x00551c30`, `0x00551f30`, `0x00552230`) se suivent sans trou, ce qui borne chacune sans avoir à deviner sa taille

- feat : la table statique `PTR_LAB_00551c30` de `FUN_0047aa70` relevée en entier — 192 entrées, 124 cibles distinctes, toutes absentes de `Blaze.c`
- feat : `0x0047ADC0`, le créneau « sous-type inconnu » de cette table, transcrit — il **éteint** l'entité au lieu de l'ignorer, et 69 des 192 créneaux y pointent : autant qui passent d'un plantage à un comportement juste
- fix : le message d'exception de cette branche affirmait que le gestionnaire ne prend aucun argument — le `push esi` est fait avant le branchement, les deux tables reçoivent l'enregistrement en `__cdecl`
- doc : les tests de navigation en jeu (auberge et autres cartes) passent à Vincent — un trajet en frappes minutées n'atteint pas une pièce choisie de façon reproductible
- fix : `TestTriggerVolumes_00534CD0` lisait le compte des morceaux en attente sur un `short` alors que le binaire fait `movzx ax,byte` — le compte avalait la première entrée de la liste, la boucle sortait de la liste et déréférençait des pointeurs inventés ; cause probable du `0xC0000005` signalé par Vincent, non reproduite
- feat : la marche sur les boîtes de déclenchement porte une borne de 0x400 — l'original n'en a aucune, et une liste atteinte par un mauvais indice plantait sans nommer sa cause
- doc : `FUN_0041be60` vérifiée — rayon 1500, trois valeurs publiées, arguments lus en double mot là où sa sœur sonore les lit en `movsx word`
- doc : la boîte de dialogue de l'auberge est modale et gèle le monde ; il faut la refermer avant tout `HOLD:` de déplacement, et un trajet en frappes minutées n'atteint pas une pièce choisie de façon reproductible
- doc : `Ordinal_442` est **stdcall** — aucun de ses sites d'appel ne nettoie la pile, et c'est ce qui fait que `[esp+14h]` désigne l'argument 2 dans les trois sœurs `FUN_00468080` / `FUN_00468110` / `FUN_0041be60` ; en cdecl la famille entière serait décalée d'un cran
- doc : `FUN_00468080` et `FUN_00468110` vérifiées contre le binaire — rayons 2000 et 1800, gain doublé, `+0xC0` portant bien sur `param_2`
- doc : `FUN_004727d0` et sa suite `EntityAnimCommon` vérifiées ligne à ligne contre le binaire — le test inversé corrigé plus tôt est bien dans le bon sens, et `or ch,80h` posait bien 0x8000
- fix : les **trois** sites qui lisent `DAT_005de538` sur un octet (`ActivePartySlot_00472770`, `EntityAnimCommon` état 0, `EntityAnimTail_00472E80` état 200) indexaient la table des emplacements sans masquer — défaut latent, tous les écrivains restant sous 256
- doc : la relance en processus fils peut épuiser son budget de trois tentatives — un lancement sur deux est mort au démarrage sur la fenêtre de mémoire basse, les trois relances ayant perdu la course
- doc : `0x005de538` est lu à trois largeurs selon le site (octet, double mot masqué, double mot nu) ; les trois se transcrivent telles quelles, c'est le binaire qui varie
- feat : le bloc d'étiquettes `0x00551814` relevé en entier — 66 octets, une scénette complète, et non un simple libellé
- doc : le texte de ce bloc est **un message de développeur** resté dans la version vendue — « la porte à un battant n'est pas utilisable, contactez Imai » ; 今井 est un nom propre, et les sous-types 6/7 sont une porte inachevée
- doc : un bloc d'étiquettes n'a pas d'en-tête de longueur — c'est un flux auto-terminant, et la table de la passe 1 donne le nombre d'opérandes de chaque code de contrôle
- fix : le coffre range réellement son contenu — `LAB_00474320` n'était pas une lacune mais un double octet pour octet de `0x00450700`, déjà transcrite ; les deux adresses sont aiguillées sur le même corps
- doc : avant de transcrire un corps absent de `Blaze.c`, vérifier qu'il n'est pas le double d'un corps déjà transcrit — deux `od` et une soustraction suffisent
- feat : `FUN_00472e80`, la queue de l'automate commun des entités de zone (six états, 1584 octets), plus sa dépendance `FUN_00466d70`
- doc : LB-012 — l'état 0xd2 de cette queue rend une valeur jamais écrite, que son appelant compare pourtant à 1
- doc : Ghidra perd la signature de `FUN_00472e80` parce que l'état 0xcd recycle les emplacements d'arguments en variables locales — le prologue suffit à le voir, la convention d'appel n'est pas à reconstruire
- feat : `FUN_004502E0` (sous-type 17) transcrite depuis le désassemblage — la table d'entités 0x0056adc8 n'a plus aucune lacune, ses 17 cibles ont un corps réel
- feat : `FUN_0040e640`, la variante longue de `FUN_004742b0` — mêmes trois passes, plus deux sous-types épinglés et la remise à blanc des quatre fiches d'emplacement du groupe
- doc : le sous-type 17 est le coffre à un détail près — au lieu de marquer une banque de zone, il applique l'opération de script `|=` au bloc de drapeaux de chaque membre du groupe : un acquis permanent, pas du butin
- feat : le son marche — `LAB_00512ef0`, un corps que Ghidra n'avait jamais décompilé, crée le périphérique DirectSound ; menu, options et carte du monde jouent enfin leur musique et leurs bruitages
- feat : `LAB_00513660`, le thread de flux de musique, transcrit depuis le binaire — sans lui la musique rejouait sans fin ses 80 blocs d'amorce
- feat : la table des 40 noms de morceaux (`PTR_..._00566cc8`) relevée en entier ; son entrée 6, `Title01.WAV`, est le thème du menu et manquait
- fix : `DAT_005db85c`, la fenêtre principale, n'était écrite nulle part — c'est winsys3d qui la rend, par le rappel de l'ordinal 790 ; `DestroyWindow` à l'extinction est rétabli
- fix : `FUN_005135d0` ne fermait que 8 des 9 poignées d'événement et concluait à une fuite qui n'existe pas — la borne `0x5db85c` en compte neuf
- fix : la panne aléatoire `No free block of 0x2000 bytes below 0x01000000` ne peut plus tuer une session — les cinq derniers sites d'allocation basse étaient pris en cours de partie, hors de portée de la relance ; ils sont pré-réclamés au démarrage
- fix : le message d'échec de `AllocLowZeroed` nomme le site (`[CallerMemberName]`) et chiffre la fenêtre restante — trois sites arrondissent à `0x2000`, et la table taille → site du protocole de test était fausse
- feat : chaque lancement imprime `[BAS] fenetre <0x01000000 apres reservations` — la marge basse varie de 3,4 à 11,5 Mo d'un lancement à l'autre, et c'est le mécanisme de la panne
- doc : LB-011 — au bouclage d'un morceau, la seconde passe de décodage écrit par-dessus la première
- doc : balayer `.text` à la recherche des `call` vers un thunk d'import retrouve les appelants que Ghidra a perdus — c'est ainsi que `DirectSoundCreate` a livré son unique site
- feat : `FUN_004727d0`, l'automate commun des entités de zone, plus `FUN_00472770` et `FUN_005037e0` — la charpente que quatre gestionnaires partagent
- feat : les gestionnaires `0x004734B0`, `0x004738A0`, `0x00473A90` et `0x00473CB0` de la table 0x0056adc8 — le saut sur place couvre à lui seul 47 des 64 sous-types
- doc : la table de sauts 0x0056adc8 relevée en entier dans `REVERSE_STATUS.md` — d'une adresse de plantage au sous-type, et du sous-type au travail restant
- doc : lire un corps décompilé par tranches fait manquer des branches ; `FUN_004734b0` en a six, pas trois, et l'une est une chute, pas un saut
- feat : les trois derniers gestionnaires d'entité que `Blaze.c` contenait — `0x00450C60` (l'entité qui palpite), `0x00450760` (le déclencheur de script) et `0x00473ED0`, **le coffre**
- doc : `PTR_FUN_0056afac` vaut `0x00401FA0`, la souche `ret` nu — l'appel indirect du déclencheur de script est un no-op vérifié, pas une lacune
- doc : `DAT_00571990` tombe dans `s_EntityState_00571500` et a déjà son accesseur — consulter la carte des symboles avant de déclarer un `DAT_` isolé
- feat : cinq gestionnaires d'entité **absents de `Blaze.c`** tirés du désassemblage — `0x004508B0`, `0x00473A70`, `0x00450BD0`, `0x004746C0`, `0x004736D0`, plus `FUN_00468110`
- doc : `0x004736D0` n'est **pas** le miroir de `0x004738A0` — les signes sont bien inversés, mais le drapeau `0x40000000` ne se pose pas aux mêmes états ; deux corps qui se ressemblent se transcrivent séparément d'abord
- doc : Ghidra rate la même signature pour la **troisième** fois dans cette famille — `param_1` servant à la fois de pointeur et de scalaire est toujours une attribution de registre ratée
- doc : `or ch,40h` pose `0x4000`, pas `0x40` ; et l'idiome `cdq/xor/sub/and/xor/sub` est un modulo **signé**, pas un masque

---

## 2026-08-23

- feat : `FUN_005309b0` — la surimpression de dialogue au-dessus d'un membre du groupe, la dernière des lacunes secondaires du bloc de dessin
- fix : `DAT_0056aaa0` et `DAT_0056aae0` sont réunies en une seule allocation — le choix d'icône est un octet **signé** et son entrée `-1` lit la fin de la table voisine
- doc : un `&LAB_00xxxxxx` utilisé comme valeur est presque toujours une constante que Ghidra a prise pour une adresse ; `&LAB_00404040` est la couleur `0x404040`
- feat : la bulle d'état au-dessus du membre (`FUN_00538bc0` et le sous-bloc de `FUN_00516fc0`) — poison, sommeil, silence s'affichent enfin
- doc : `DAT_0056d440` relevée — seize entrées de huit octets, une hauteur de tête par classe de personnage
- feat : **la table des trente-deux codes de contrôle de texte est complète** — les dix-huit qui manquaient, dont le saut de chaîne à index calculé (`0x07`), l'attente de fin de page (`0x0e`) et l'insertion du nom du locuteur (`0x1d`). Le joueur circule à l'étage de l'auberge, 2101 images
- fix : le code `0x05` écrit **deux** octets, pas un — il remet au passage à zéro la profondeur de la pile de sous-chaînes ; l'écriture évidente aurait fait reprendre le texte au mauvais endroit
- feat : **le coffre est complet** — `FUN_005312d0` (le rangement en inventaire, avec le découpage VRAM de l'icône), `FUN_00450700` (le rappel de fin de boîte) et le code de contrôle de texte `0x0b` (`FUN_00544c10`, le saut de sous-chaîne)
- doc : `FUN_005312d0` avait été portée deux fois au TODO comme difficile alors que quatre de ses cinq dépendances étaient déjà transcrites sous d'autres noms — vérifier ce que deviennent les appelées avant d'estimer le coût
- doc : les sept compteurs à `+0x178` du bloc de personnage sont confirmés par deux relevés indépendants, `FUN_005312d0` et le verbe de zone `0x00442F20`
- feat : `FUN_00530f20` — la remise d'un objet au groupe : réactions du groupe, parcours d'inventaire avec empilement, et la phrase qui l'annonce. Avec `FUN_00530ed0`, son miroir de fin de boîte
- feat : le rappel de fin de texte (`+0x8a8` du descripteur) est branché — un `TODO(REVERSE)` de signature inconnue fermé dès que le premier site d'affectation est apparu
- doc : `DAT_0056aaa0` relevée — seize dwords, une réaction (animation, image) par classe de personnage
- feat : verbe 14 de la table de zone (`FUN_00442D90`) — **l'accès au coffre** : il désigne l'entité appelante comme porteur du script de l'entité de scène visée
- doc : cinq voisins du verbe 14 relevés dans la même fenêtre de désassemblage, tous sous soixante octets — les prendre en une passe le jour où l'un sera demandé
- feat : `FUN_00405510` et `FUN_00405240` — les deux derniers réveils d'entité de scène (types 2 et 1), plus les deux pas d'animation qu'ils appellent, `FUN_004052f0` (parcours d'une plage avec pause tirée au hasard) et `FUN_00405420` (le décompte). 6361 images sans plantage
- doc : la signature `__fastcall` à six paramètres que Ghidra donne à `FUN_004052f0` est fausse — `add esp,0x10` après l'appel dit cdecl à quatre arguments, et ses `param_1`/`param_2` sont des registres fantômes
- doc : l'échec `AllocLowZeroed` peut aussi survenir **pendant un chargement**, là où la relance en processus fils ne rattrape rien ; six des neuf sites d'allocation basse ne sont pas pré-réclamés
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
