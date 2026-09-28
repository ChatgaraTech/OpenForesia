## Context

Exploration du 2026-09-28 (voir `proposal.md`) :

- **Les quatre apparences sont des classes jouables**, pas des monstres : le jeu nomme ses classes
  Guerrier, Prêtre, Voleur, Magicien, Chasseur, Elfe, **Kobold**, **Lutin** au masculin et
  **Sylphide** l'Elfe au féminin (`BLAZE.ALL`, `HISTORY.md` 2026-09). Identité = `genre << 3 |
  classe` : Lutin 7, Voleur 2, Kobold 6, Sylphide 13.
- **Le jeu ne charge que les classes du groupe.** `LoadPartyMemberResources` (Ghidra: FUN_00536ba0)
  charge, pour chacun des quatre emplacements `i`, depuis le répertoire de l'état 3
  (`Mem.s_MainResourceDirectory_005e97c4`, en mémoire pendant toute la carte) et **indexé par
  l'identité** : deux textures (`+0x84`, pas 8, puis 0x80 octets plus loin), un paquet de modèle
  (`+0x2c`, pas 8) et un paquet d'animation (`+0x7c`, pas 4). Les textures vont en VRAM à une place
  **calculée d'après `i`** (rectangle, ligne de palette, table `0x005e8f00 + 0x80/0x90 + i*4`), et la
  pagination du modèle est corrigée d'autant (`pageBits`, octets 1 et 2).
- **Deux jeux de tables de rendu.** Le groupe : animations `0x005e9680[i]`, modèles `0x005e9800[i]`.
  Les entités de scène animées : `0x005e9640[e[4]]` et `0x005e96a0[e[4]]` (`WorldMapHud.cs`). Un PNJ
  ne peut donc pas emprunter le modèle d'un membre, et une classe absente du groupe n'est nulle part.
  La 82 ne charge aucun modèle d'entité : ces tables sont libres dans le hub.
- **Le rendu est à portée de couture.** `DessinSurImpression` (LP-008) reçoit l'ordering table et le
  curseur de primitives à chaque image ; `Mdl.DrawAnimatedModel(ot, curseur, animation, modèle,
  transformée, image, drapeaux)` est ce que le jeu appelle pour un membre du groupe.
- **La catégorie d'un objet est dans sa fiche** : `+0x12`, octet haut — `0x100` arme, `0x200` armure,
  `0x400` bouclier, `0x800` accessoire (`FormatSauvegarde.md`). Aucun des quatre : objet utilisable.
- **Le reste de LP-008 est réutilisable** : `BoutiqueEcran` (l'écran, la manette lue pendant le gel),
  `BoutiqueAchat` (l'achat, la remise comme un coffre), `CatalogueObjets` (fiche et vignette lues dans
  les fichiers du jeu).

## Goals / Non-Goals

**Goals :** quatre PNJ au repos, aux apparences demandées, quelles que soient les classes du groupe ;
quatre boutiques filtrées par catégorie ; quatre fichiers dans `LegacyPlus\` ; l'armoire retirée.

**Non-Goals :** dialogue, déplacement ou regard des PNJ ; vente d'objets par le joueur ; une collision du moteur avec les PNJ (Vincent l'a demandée au premier
essai : un membre qui entre dans un cercle de 48 unités autour d'un marchand est ramené à son bord,
à l'entrée de son pas — `PasDuMembre`) ; le Magicien, le
Guerrier et les autres classes.

## Decisions

### D1. Étape 1 d'abord : mesurer

1. **La VRAM du hub** : un relevé (version Debug) des rectangles occupés une fois le hub chargé,
   groupe de quatre compris — textures de la 82, quatre emplacements du groupe, vignettes
   d'inventaire (`FUN_00535500`), police, interface. On en tire quatre places libres pour deux
   textures chacune, et leurs lignes de palette.
2. **Les paquets de classe** : taille des modèles et animations des identités 2, 6, 7, 13 (verbe hors
   ligne, comme `terrain`), et nombre d'images de l'animation de repos.
3. **L'animation de repos** : l'indice de clip qu'un membre immobile joue (`+0x18` de son
   enregistrement quand il ne bouge pas), relevé en jeu.

**Résultat de la mesure (2026-09-28, détail dans `legacy.md`, LP-010)** : les ressources de classe
sont dans le répertoire de l'**état 1**, qu'il faudra relire depuis l'archive ; un modèle n'emploie
que sa texture 1, par une entrée de `0x005e8f00` et un décalage V ; la bande x 832..1023, y 256..511
est libre dans le hub et reçoit les quatre textures, les palettes vont en x 0..255, y 256..271 ;
l'animation de repos est le clip 0.

### D2. Charger les quatre classes dans le hub, à nous

À l'entrée dans le hub, le tweak charge pour chaque marchand actif, depuis le répertoire de l'état 1
(relu depuis l'archive dans un tampon à lui, `LoadResourceEntry` + `FixupResourceHeader`), la
texture 1, le modèle et les animations que `LoadPartyMemberResources` prend, mais : dans **sa propre mémoire native**
(un paquet de modèle est lu par le processeur, pas par la liste d'affichage), dans **ses propres
tables** (jamais `0x005e9680` ni `0x005e9800`, qui sont celles du groupe), et avec la place de VRAM
trouvée en D1 : pour le marchand `k`, la texture en (832, 256 + 64·k), ses palettes en
(0, 256 + 4·k), envoyées par `LoadImage` ; l'entrée `0xcc + 4·k` de `0x005e8f00` reçoit la page
(832, 256, 8 bits) et la palette ; la pagination du modèle vaut octet 1 = `0x40·k` (le décalage V que
`AdjustModelTexturePages` applique, comme pour un membre) et octet 2 = `0x33 + k`. Les outils de chargement du jeu (`LoadResourceEntry`,
`DecodeResourceChunk`, `RelocateModelBundle`, `RelocateAnimationBundle`) sont appelés tels quels.

*Écarté* : faire des marchands des entités de scène (`e[4]` dans `0x005e96a0`). Il faudrait un
gestionnaire du jeu pour faire défiler l'animation ; celui du PNJ de l'auberge (`0x00486EE0`) lance
un script de dialogue de sa carte.

### D3. Les dessiner par la couture `DessinSurImpression`

À chaque image dessinée dans le hub, le tweak appelle `DrawAnimatedModel` pour chaque marchand : sa
transformée (position fixe, cap 1024), l'image courante de son animation de repos, qu'il fait
avancer lui-même. Aucune couture nouvelle. Le budget de 2 500 primitives : le hub vidé en consomme
peu ; le coût de quatre personnages est relevé en D1.

### D4. L'ouverture par `PasDuMembre`

Comme la porte du hub : un membre debout devant un marchand (bande de sol devant lui), tourné vers
lui à 45° près, bouton d'action consommé, aucune boîte ouverte → `BoutiqueEcran.Ouvrir` avec le
catalogue de ce marchand.

### D5. Quatre fichiers, une lecture commune

`LegacyPlus\BoutiqueObjets.ini`, `BoutiqueAccessoires.ini`, `BoutiqueArmures.ini`,
`BoutiqueArmes.ini` — noms à valider par Vincent. Même contrat que `Boutique.ini` : `[Boutique]
Actif`, `[Catalogue]` nom = prix, lus une fois, jamais écrits. `ReglagesBoutique` devient la lecture
d'un fichier donné ; le filtre de catégorie (D6) s'applique après.

### D6. Le filtre de catégorie, par la fiche

À la construction du catalogue d'un marchand, chaque nom est cherché dans `CatalogueObjets` ; la
fiche trouvée donne la catégorie (`+0x13`). Un objet d'une autre catégorie est écarté et journalisé.

### D6 bis. Le filtre de classe (demande de Vincent, 2026-09-28)

Chez la Sylphide (armes) et le Kobold (armures, boucliers), l'acheteur ne voit que ce que sa classe
peut porter : l'octet `+0x12` de la fiche porte un bit par classe (`1 << classe`), la classe de
l'acheteur est `perChar[0x150] & 7`. Le filtre s'applique à chaque ouverture, puisque deux membres de
classes différentes voient deux listes. Liste vide : l'écran s'ouvre avec un message.

### D7. Le générateur

`generer-boutique.mjs` écrit les quatre modèles : l'équipement réparti par catégorie (lue dans la
colonne `octets` d'`objets.csv`), au prix de LP-008 ; les objets utilisables à un **barème par
famille**, reconnue dans la description (herbes 20, soins 60, combat 150, soins complets 250,
résurrection 300, cristaux d'arme 400, effets permanents 1 500, divers 100) — la zone ne disait rien :
les objets courants sont au catalogue de presque toutes les zones. Les prix déjà écrits dans les
fichiers sont repris à chaque relance, pour garder les retouches.

### D8. L'armoire retirée

`Boutique.cs` (la détection de l'armoire) est supprimé ; `Boutique.ini` n'est plus lu, et
l'installeur ne le dépose plus. Un `Boutique.ini` déjà présent chez un joueur reste sur son disque,
ignoré.

## Risks / Trade-offs

- **[VRAM]** Pas de place pour quatre personnages → Mitigation : D1 d'abord ; au pire, réduire les
  textures en réutilisant celles d'un membre du groupe de même classe, ou moins de marchands visibles.
- **[Pagination]** La correction de page d'un modèle dépend de sa place : une erreur donne un
  personnage aux textures brouillées, sans plantage → vérifier contre une capture.
- **[Membres de même classe]** Un Voleur dans le groupe et le Voleur marchand chargent la même
  classe deux fois : sans effet, chacun a ses tables.
- **[Collision maison]** La poussée agit avant le déplacement du pas : un membre peut entrer d'un pas
  dans le cercle avant d'en être ressorti à l'image suivante — un léger tremblement contre le
  marchand est possible.
- **[Profondeur]** Le mot `+0x1a` de la transformée doit valoir 1, comme le `+0x36` d'un membre :
  sinon les parties posées (drapeau `0x400`) tombent dans l'emplacement 0 de l'OT, devant tout.
