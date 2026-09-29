## 1. La mesure (lecture seule)

- [x] 1.1 Prérequis : `add-legacy-plus-arena` terminé (directeur, programme, palier, fin, défaite)
- [x] 1.2 Lire le Grizzly et le Méchant ours et leurs appelées : chaque référence dépendante de la zone — le combat de boss de la forêt est la formation 0 de 3/5 (l'ours et deux Grizzlys), choisi entier par Vincent
- [x] 1.3 Le son 3 et l'effet 0x39 : communs à toutes les zones (banc `Base.snd`, table statique) ; le son `0x3f` et le souffle de l'ours dépendent de la zone
- [x] 1.4 Où vivent, dans le répertoire de 3/5, l'espèce, la fiche, le maillage, les animations et la texture de l'ours et du Grizzly ; leurs tailles (`OpenForesia.Tools greffe 3 5`)
- [x] 1.5 Relevé VRAM de 4/9 (`legacyplus-vram-z4-s9.txt`, 2026-09-28) : x 128..255 × y 16..223 libres, palettes lignes 288..319
- [x] 1.6 Résultats dans `legacy.md` (LP-013, étape 1) ; fichier `AreneGreffe.cs` validé par Vincent

## 2. La greffe générique

- [x] 2.1 Relire le répertoire d'une sous-zone quelconque dans l'archive, une fois, et le garder
- [x] 2.2 Charger et reloger le maillage et les animations d'une espèce dans une mémoire à nous, sous notre pagination
- [x] 2.3 Envoyer sa texture en VRAM à chaque entrée dans l'arène, et écrire l'entrée de décodage qui la désigne
- [x] 2.4 Prêter à l'espèce greffée un rang d'espèce et de fiche (tables recopiées, répertoire repointé), un maillage, un créneau d'IA ; la faire naître par le directeur depuis la formation d'origine
- [x] 2.5 Reconnaître les créatures greffées (rang d'espèce ≥ 16 dans la table de scène, en arène)

## 3. La traduction

- [x] 3.1 Couture `SonPositionnel` : un son de niveau d'une créature greffée joué sous son id greffé (échantillons ajoutés au banc de 4/9), coupé sans traduction
- [x] 3.2 Couture `EffetCoupe` : un effet `>= 0xc0` d'une créature greffée coupé (lot 1)
- [x] 3.3 Journal `[GREFFE]` : chaque référence coupée, une fois par chargement
- [x] 3.4 Couture `ButinNe` : ni objet ni pièces pour une créature greffée ; les éclats d'expérience restent

## 4. Le mode MECHANT OURS

- [x] 4.1 Le programme MECHANT OURS : une vague, l'ours et ses deux Grizzlys, à la place du boss de la tour
- [x] 4.2 Le menu de l'arène liste MECHANT OURS après ENCHANTERESSE, qui reste
- [x] 4.3 Essai en jeu par Vincent : aspect (textures), déplacement, frappe, prise (son, éclat), cri de l'ours (le son de la forêt), souffle coupé sans plantage, PV aux paliers 1, 3 et 10, victoire (cinématique, portail, bourse), défaite, aucun butin ; journal `[GREFFE]` — validé en jeu (2026-09-29 ; le souffle ensuite greffé, 5.3)
- [x] 4.4 Essai en jeu : la forêt, par l'histoire, après un combat d'arène — son combat de l'ours intact ; ENCHANTERESSE inchangée — validé en jeu (2026-09-29)

## 4 bis. Le mode BEHEMOTH (2026-09-29)

- [x] 4b.1 Mesure (`OpenForesia.Tools greffe 3 9`) : espèce 0, maillage 0, IA 17 = `0x004A54E0`, fiche 0 (2 160 PV, rang 150) ; texture entrée 5, 92 × 48 mots, 8 bits ; sons 3, `0x20`, `0x35` communs ; effets `0xbd` et `0x39` statiques ; l'atterrissage du bond `0xc0 + 0x1f` = `0x00483650`, par sous-zone
- [x] 4b.2 L'espèce `AreneGreffe.Behemoth` : la place VRAM, la palette et l'entrée de décodage du Méchant ours (jamais dans le même programme)
- [x] 4b.3 Le programme BEHEMOTH au menu, après MECHANT OURS
- [x] 4b.4 Essai en jeu par Vincent : aspect, déplacement, coups, sorts (familles 0, indices 13/19/23 — et leurs sons), bond coupé sans plantage, PV aux paliers 1 et 10, victoire, défaite ; journal `[GREFFE]`

## 4 ter. La bourse à la place des coffres (2026-09-29)

- [x] 4t.1 Les deux coffres de l'arène retirés (copie de la liste 3, mise de côté, bits 510/511 de la banque `0x005e96e0`)
- [x] 4t.2 Chaque programme porte sa bourse (MECHANT OURS 40, ENCHANTERESSE 150, BEHEMOTH 300) ; à l'ouverture du portail, bourse × palier répartie entre tous les membres (reste : une pièce aux premiers), bornée à 99 999 ; message « Le groupe remporte N pièces d'or »
- [x] 4t.3 Montants de l'ENCHANTERESSE et du BEHEMOTH confirmés par Vincent (2026-09-29)
- [x] 4t.4 Essai en jeu : les bourses des quatre membres avant / après (menu Statistiques), le message, aucun coffre, le journal `[ARENE] bourse`

## 5. Le souffle (lot 2)

- [x] 5.1 Greffer `0x00413330` et `0x004134C0` — fait autrement (2026-09-29) : pas de créneau dans 4/9, mais le **contexte** de la sous-zone d'origine. Pendant le gestionnaire de la lignée d'une créature greffée, les catalogues `+0xcc`/`+0xd0`/`+0xd4` et les sons `+0xa8` de 3/5, et un indice `>= 0xc0` résolu dans les tables de 3/5 (coutures `AvantGestionnaire`, `ApresGestionnaire`, `GestionnaireInstalle` ; `Resources.SubAreaEffectHandler`) ; ouvert pour le seul Méchant ours (`EffetsGreffes`)
- [x] 5.2 Les modèles 7 et 48 sont communs (état 3, mesure 8.1.4) : rien à greffer ; le son `+0xa8[0x2c]` passe par le contexte et est greffé au banc de 4/9 à sa première demande
- [x] 5.3 Essai en jeu : le souffle comme dans la forêt (voile rouge, anneaux, effet sur le groupe) — validé en jeu (2026-09-29)

## 6. Le recensement

- [x] 6.1 Le script de recensement des dépendances de zone de chaque IA de monstre, en suivant les appelées (G5) — sons de niveau (id ≥ 60), effets `>= 0xc0`, sous-entités — fait autrement : `OpenForesia.Tools effets-boss` (`EffetsBoss.cs`), en C# sur le désassemblage (2026-09-29)
- [x] 6.2 Son document généré dans `Documentation/Etat/`, un boss par ligne avec son prix de greffe ; la ligne du Méchant ours confrontée à la mesure 1.2 — `Documentation/Etat/EffetsDesBoss.md` ; le Méchant ours retrouve le souffle et les anneaux (`0x00413330`, `0x004134c0`, `+0xa8`), mais ses modèles 7 et 48 sont communs (état 3), pas de 3/5
- [x] 6.3 Mention dans `CLAUDE.md` (tableau des documents générés)

## 7. Clôture

- [x] 7.1 `legacy.md` (LP-013), `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md` — à jour (2026-09-29)
- [x] 7.2 `publish-release.ps1` : la version publiée passe — 2026-09-29 : jeu autonome (6,4 Mo zippé, aucune donnée du jeu), installeur de 16,1 Mo

## 8. Les boss restants (relevé du 2026-09-29, `legacy.md` LP-013)

### 8.1 Le travail commun

- [x] 8.1.1 VRAM par programme : attribuer les bandes `0x40`, `0x80`, `0xC0` de la page (128, 0) à chaque entrée, textures de plus de 64 lignes sur bandes contiguës ; revérifier y 224..255 sur une tour sans greffe — `AreneGreffe.Placer`, `ChargerModele` ; validé en jeu (2026-09-29 : deux bandes avec Eifridt, trois avec Belzébuth et le Dragon zombie ; cinq bandes sur deux pages avec le Roi-zombie)
- [x] 8.1.2 Greffe générique des effets par sous-zone (le lot 2 de l'ours, généralisé) : gestionnaire dans un créneau libre de `0x0056b0c8`, sous-type traduit, modèles de décor, textures et sons qu'il emploie
  - mesure (`EffetsDesBoss.md`, 2026-09-29) : toutes les planches et tous les modèles des effets désignent des textures **communes** (entrées `0x10`, `0x30`). Il ne reste à greffer que les gestionnaires, les sous-types (trois tables, trois catalogues, toute la descendance d'une créature greffée) et les sons `+0xa8`. La couture du rendu (option A) n'est donc pas nécessaire en l'état ; à revoir si un essai montre un effet mal texturé
  - écrit (2026-09-29) : le contexte de la sous-zone d'origine (voir 5.1), générique, ouvert espèce par espèce (`Espece.EffetsGreffes`) après essai — le Méchant ours d'abord
  - [x] ouvert pour toutes les espèces (2026-09-29, Vincent) : Behemoth (`0x00483650`, `0x00483760`), Wybarn (`0x00483930`, `0x00483a30`), Fougre (`0x0042f2a0`, `0x0042f3b0`), gros poilu (aucun effet par sous-zone mesuré, un sous-type lu dans l'entité) — validé en jeu, puis sur chaque boss ajouté
- [x] 8.1.3 Traduction de la table de sons `+0xa8` — par le contexte (5.1) ; les ids de niveau de la sous-zone d'origine sont greffés au banc de 4/9 à leur première demande (`Son`, `GrefferSon`), clés par sous-zone
- [x] 8.1.4 `greffe` imprime le premier bloc de `+0xcc` en entier (octets `0x12` à `0x19` employés) ; le recensement (tâche 6) suit les effets greffés — `effets-boss` lit les trois catalogues (`+0xcc`, `+0xd0`, `+0xd4`) sur 64 octets et suit toute la descendance
- [x] 8.1.5 Scripts : décider boss par boss ceux que l'arène laisse passer (Belzébuth, script `0x00553278`) — sans objet : le script de Belzébuth est un script de sons différés (`QueueDelayedSoundScript`), que l'arène ne tait pas

### 8.2 Sans le travail commun (effets coupés en lot 1)

- [x] 8.2.1 SYLPHIDE (4/7, formation 1 : Sylphide et Wybarn) — (bourse 180, cri 0x47 greffé), validé en jeu (2026-09-29)
- [x] 8.2.2 GROS POILU (10/12) — (bourse 330), validé en jeu (2026-09-29)
- [x] 8.2.3 FOUGRE (8/1, et deux Grifons) — (bourse 180), validé en jeu (2026-09-29)
- [x] 8.2.4 Le menu de l'arène trié par PV du combat au palier 1, boss et escorte (Vincent, 2026-09-29)

### 8.3 Après 8.1.1 et 8.1.2

- [x] 8.3.1 JEUNE DRAGON (1/4) — bourse 100, cri 0x41, effets de la mine par le contexte ; validé en jeu (2026-09-29)
- [x] 8.3.2 TROLL (1/9, et deux Géants verts) — bourse 230, cri 0x40, éclair de 1/9 par le contexte ; validé en jeu (2026-09-29)
- [x] 8.3.3 DURAHAN (5/4, et trois Sanguinaires) — bourse 220, cri 0x47, effet de 5/4 par le contexte ; validé en jeu (2026-09-29)
- [x] 8.3.4 LOUP-GAROU et TIGRE (6/5 ; 8.1.3) — bourse 140 ; le son +0xa8[0x2c] joué par PlaySoundEffect, d'où la couture SonJoue ; validé en jeu (2026-09-29)
- [x] 8.3.5 NOCTURIO (6/11, et deux Sacubas) — formation 1, bourse 170, sept effets du château par le contexte ; validé en jeu (2026-09-29)
- [x] 8.3.6 EIFRIDT (10/11) — bourse 340, deux effets de 10/11 par le contexte ; texture 128 × 128 sur deux bandes, la première ; validé en jeu (2026-09-29)
- [x] 8.3.7 LORDEMON (10/13) — bourse 400, six effets de 10/13 par le contexte ; validé en jeu (2026-09-29)
- [x] 8.3.8 ANGE NOIR (10/14) — bourse 340, huit effets de la salle des démons par le contexte, la pluie comprise ; validé en jeu (2026-09-29)
- [x] 8.3.9 CERBERE (10/9, et quatre Groklebs) — bourse 400, neuf effets de 10/9 par le contexte, dont un sous-type lu en jeu seulement ; validé en jeu (2026-09-29)
- [x] 8.3.10 DRAGON ROUGE (2/0, et deux Salamandres feu) — bourse 1 200, cri 0x45, sept effets de 2/0 par le contexte ; PV bornés à 65 535 dès le palier 9 ; validé en jeu (2026-09-29)

### 8.4 En dernier, un à un

- [x] 8.4.1 BELZEBUTH (10/15) : toute la VRAM libre, un script — bourse 430, cri 0x3c, onze effets de 10/15 par le contexte ; le « script » est un script de sons différés, pas tu ; texture sur les trois bandes ; validé en jeu (2026-09-29)
- [x] 8.4.2 DRAGON ZOMBIE (10/4, et deux Maudites) : texture de 96 lignes — bourse 780, cri 0x42, effets de 10/4 par le contexte ; le dragon sur deux bandes, les Maudites sur la troisième ; validé en jeu (2026-09-29)
- [x] 8.4.3 ROI-ZOMBIE (5/9) : quatre textures d'escorte, des invocations — validé en jeu (2026-09-29) (bourse 330, treize effets de 5/9 par le contexte) ; la VRAM passe à cinq bandes sur deux pages (y 320 et 384 dans la page (128, 256)) ; pas d'invocation (ZombieKingSummonSubtype n'est qu'une lecture de catalogue) ; la texture en plus de la mesure était une lecture trop longue de planche ; à tester en jeu
- [x] 8.4.4 KRAAKEN — **abandonné** par Vincent (2026-09-29). Pour mémoire : son combat est en 9/0 (formation 2 : deux bras et six pieds ; le corps, espèce 4, n'y est pas), et ses trois routines écrasent leur hauteur par la constante `0xffe70000` (la surface du lac, −400) à chaque image — dans l'arène il flotterait à 600 au-dessus du sol sans une couture de plus
