## Context

Voir `proposal.md` pour le pourquoi, et `add-legacy-plus-arena/design.md` pour l'arène elle-même.

- **Correction de la mesure (2026-09-28).** L'« espèce 3 de 3/5, formation 1 » que ce paragraphe
  désignait est une Abeille tueuse « simple » (IA 19, `0x004A5F10`). **Le combat de boss de la forêt
  est la formation 0 de 3/5** : le Méchant ours (espèce 0, IA 16 = `0x004A4AE0`, 300 PV, rang 38)
  et deux Grizzlys (espèce 4, IA 4 = `0x004A4270`, 80 PV, rang 25). Vincent a choisi le combat
  entier. Détail et plan : `Documentation/LegacyPlus/legacy.md`, LP-013.
- **Le son 3 et l'effet 0x39 du Grizzly sont communs** : id < 60 = banc `Base.snd` ; sous-type
  < `0xc0` = table statique `0x00551c30`. **Le Méchant ours** a deux références de zone : le son de
  niveau `0x3f` et le souffle (sous-type `0xc0 + bloc[9]`, table par sous-zone `0x0056b0c8`, une
  chaîne de trois gestionnaires, deux modèles de décor et un son de la table `+0xa8`).
- **La mort** tire le butin dans la table `+0xb4` de la sous-zone courante.
- **Ce qui sait déjà charger hors zone** : `MarchandsHub` relit le répertoire de l'état 1 dans
  l'archive (`LireEtat1`), charge et reloge un paquet de modèle et un paquet d'animations
  (`RelocateModelBundle`, `RelocateAnimationBundle`), envoie une texture en VRAM à une place libre
  relevée et écrit l'entrée de la table de décodage `0x005e8f00` qui la désigne.
- **Le coût des boss suivants**, mesuré par appels directs (exploration du 2026-09-28) : une à quatre
  naissances d'effet et un à quatre sons par boss (Wyrm, Trent, Behemoth, Dragon rouge, Chevalier
  pourpre, Harpie…). Les appelées, les sorts et les boss à plusieurs corps n'y sont pas comptés.

## Goals / Non-Goals

**Goals :** une greffe générique (une espèce d'une zone, dans l'arène) ; le Grizzly qui combat comme
dans la forêt ; le recensement qui chiffre les boss suivants.

**Non-Goals :** le souffle de l'ours au lot 1 (coupé, greffé au lot 2) ; le butin des créatures
greffées (éteint) ; les autres boss — ils viendront **l'un après l'autre** (Vincent, 2026-09-28), chacun
un programme de plus, dans l'ordre que le recensement (G5) rendra le moins cher ; un boss rush
(plusieurs vagues de zones différentes) — rendu possible, pas livré ; les boss à plusieurs corps ou à
script de carte ; greffer un décor.

## Decisions

### G1 — Greffer le monstre dans la tour, pas envoyer le groupe dans la zone du monstre

*Écarté* : charger la zone du boss (tout y marche nativement) et y greffer la salle de la tour. Une
seule greffe, mais de **décor** — sol, collisions, textures, caméra —, dans une VRAM différente à
chaque zone, et le directeur de l'arène devrait vivre dans onze zones. Greffer le **monstre** garde
l'arène unique, et le prix se paie boss par boss, mesurable par le recensement (G5).

### G2 — La greffe : répertoire, paquets, texture, fiche

Pour une espèce (zone, sous-zone, espèce) : relire le répertoire d'état de sa zone dans l'archive
(comme `LireEtat1` pour l'état 1), en tirer le modèle et les animations de l'espèce, relogés dans une
mémoire à nous une fois pour toutes ; envoyer sa texture en VRAM à chaque entrée dans l'arène, à une
place que le relevé VRAM de 4/9 dit libre ; recopier sa **fiche** (PV, PM, rang, caractéristiques,
`+0x22`) dans l'emplacement d'espèce que l'arène lui prête. La créature naît ensuite par le chemin des
formations de l'arène (arène, D3), avec l'**adresse de son IA** d'origine.

### G3 — La traduction des sons et des effets

Tant qu'une créature greffée agit, deux coutures traduisent ses numéros : le son positionnel (numéro
de la zone d'origine → un son chargé à nous, ou coupé) et la naissance d'un effet (sous-type de la
zone d'origine → le même gestionnaire d'effet et sa texture, greffés, ou coupé). La table vit avec
le programme du boss. *Pour le Grizzly* : le son 3 de la forêt et l'effet 0x39 de la forêt.

*À mesurer* (tâche 1) : ce que sont exactement ce son et cet effet dans la forêt (un fichier du banc
`Q03_xx.snd` ; un gestionnaire d'effet et sa planche de texture) ; si l'effet se greffe comme le
modèle ou s'il vaut mieux le couper dans un premier temps (la spec autorise « laissé de côté », pas
« remplacé »).

### G4 — Savoir qu'une créature est greffée

Le directeur marque les créatures qu'il fait naître (leur rang d'entité) : les coutures de G3 ne
traduisent que pour elles, et tout le reste de la zone 4 garde ses numéros. Aucune condition ne va
dans `src/Blaze/` : la couture reçoit l'entité et le numéro, et rend le numéro traduit.

### G5 — Le recensement des dépendances de zone

Un script `census-*` (même famille que `census-zone-complexity.sh`) qui, pour chaque IA de monstre
écrite, relève ses naissances d'effet, ses sons, ses naissances de sous-entités et ses appelées
propres à une zone, **en suivant les appelées** — le compte direct du Context en sous-estime
certains. Il écrit un document généré dans `Documentation/Etat/` : un boss par ligne, son prix de
greffe. Généré, jamais édité à la main.

## Risks / Trade-offs

- [La texture du Grizzly ne tient pas dans la VRAM libre de 4/9] → relevé VRAM d'abord (tâche 1) ;
  si la place manque, les marchands (qui ne sont pas dans l'arène) ne comptent pas, mais la salle,
  elle, garde toutes ses pages.
- [L'IA lit une donnée de sa zone que les appels directs ne montrent pas (une table d'animation par
  zone, une variable de script)] → lecture complète de `Grizzly.cs` et de ses appelées avant la greffe ;
  le journal `[GREFFE]` (Debug) note toute référence non traduite.
- [Le son ou l'effet d'origine ne se greffe pas simplement] → la spec autorise de le laisser de côté ;
  un Grizzly silencieux sur sa prise vaut mieux qu'un effet de la tour.
- [Le modèle greffé et le Grizzly d'une vraie forêt se partagent un état global] → la forêt n'est
  jamais chargée pendant l'arène ; les paquets greffés sont à nous, pas ceux de la forêt.
