## Context

Voir `proposal.md` pour la motivation. État de la chaîne d'affichage au 2026-09-26 :

```
 moteur (GTE, grille 320x240)                         Host/D3D11Backend
 ────────────────────────────                         ─────────────────────────────────────
 coord. entières ─table x(Size+9)/10─▶ TlVertex (px logiques) ──▶ cible hors écran
                                                                 s_SceneWidth x s_SceneHeight
                                                                 (taille logique : 640x480…)
                                                                        │ s_PointSampler
                                                                        ▼
                                                                 back buffer = zone cliente
                                                                 (PresentView, 4:3 centré)
```

- Le shader de sommets convertit des **pixels logiques** en NDC par `InvTargetSize`
  (`D3D11Backend.cs`, `VSMain`) après retrait de `ViewportOrigin`. Rien d'autre ne dépend de la
  taille physique de la cible.
- **Aucune relecture de la cible de rendu** : `StoreImage`/`MoveImage`/`LoadImage` opèrent sur la
  VRAM PSX émulée en mémoire (`Libraries/winsys3d/Vram.cs`), y compris la vignette de sauvegarde.
  C'est le piège classique des résolutions internes ; il n'existe pas ici.
- **Le filtrage est déjà décidé par le moteur** : `LierEmplacement` (`CacheTextures.cs`, winsys3d
  `FUN_00a27430`) appelle `SetTextureFilterMode(genre == 0 && (page 16 bits || !Zoom8Bit) ? 1 : 0)`
  à chaque liaison ; `SetTextureFilterMode` (ordinal 798) passe au slot device `+0x90` la valeur 2
  (filtre) si ce mode est non nul **et** `TexFilterPossible` (`Filter=` ; F10 est inerte, voir plus bas), 1 sinon. Le slot
  `+0x90` n'est pas branché dans `SyntheticDevice.cs` : la décision se perd.
- Tout ce que ce changement touche est dans `src/Host/` (non tenu à la fidélité) et dans
  l'installeur. Aucun corps transcrit n'est modifié.

## Goals / Non-Goals

**Goals:**
- Rastériser plus fin sans que le moteur le sache ; MSAA ; mise à l'échelle finale propre.
- Recevoir enfin la décision de filtrage du moteur, et permettre de la forcer.
- Un filtrage qui ne crée ni halo de couleur-clé ni bavure de page.
- Rendu actuel reproductible au pixel près par trois réglages.

**Non-Goals:**
- Précision des sommets (tremblement PSX), écran large, filtres xBR/ScaleFX, configurateur,
  rechargement à chaud des réglages, mipmaps, filtrage anisotrope.

## Decisions

### D1. Un fichier `Affichage.ini`, lu par l'hôte, jamais écrit par le jeu

Nouveau fichier `Host/DisplaySettings.cs` (classe statique `DisplaySettings`, nom neutre) : lecture
**unique** au démarrage, avant `CreateGameWindow`, d'un INI simple (sections `[Affichage]` et
`[Textures]`, `clé = valeur`, commentaires `;`), résolu relativement au dossier de l'exécutable comme
le reste depuis `install-from-iso`. Chaque clé est validée seule ; une valeur refusée est signalée
une fois dans le journal et remplacée par son défaut.

Analyseur écrit à la main plutôt que `GetPrivateProfileStringA` : lecture UTF-8 (commentaires
accentués), aucune dépendance au comportement ANSI de 1998, compatible trim. Alternative écartée :
une section dans `OpenForesia.cfg` — deux écrivains sur un même fichier, et une remise à zéro du
`.cfg` (conseillée par `OpenForesia.cfg.md`) effacerait les réglages d'affichage.

| clé | valeurs | défaut |
|---|---|---|
| `[Affichage] ResolutionInterne` | `auto`, `1`..`8` | `auto` |
| `[Affichage] Anticrenelage` | `1`, `2`, `4`, `8` | `4` |
| `[Affichage] MiseAEchelle` | `ajustee`, `entiere` | `ajustee` |
| `[Textures] Filtrage3D` | `jeu`, `proche`, `bilineaire` | `jeu` |
| `[Textures] FiltrageInterface` | `net`, `lisse` | `net` |

`FiltrageInterface = net` par défaut malgré le choix « moderne » : une interface en pixel art
lissée au bilinéaire est plus floue, pas plus moderne ; le lissage digne de ce nom est xBR/ScaleFX,
hors périmètre.

### D2. Taille physique ≠ taille logique

Le backend distingue désormais `s_SceneWidth/Height` (logique, ce que le jeu demande, inchangé) et
`s_TargetWidth/Height` (physique). `InvTargetSize` reste `1/logique` ; seul le viewport D3D11 de la
passe de scène prend la taille physique. La rastérisation se fait donc plus fin, les positions ne
bougent pas.

- Facteur entier N : physique = logique × N.
- `auto` : physique = rectangle de destination calculé par `PresentView` (plus grand 4:3 qui tient,
  ou plus grand multiple entier en `entiere`), arrondi au pixel ; la cible est recréée quand la
  fenêtre change de taille. La présentation se fait alors à 1:1.
- La recréation passe par `EnsureSceneTarget`, qui suit déjà les changements de `Size` (F11/F12).

Alternative écartée : multiplier les coordonnées des sommets dans `DeviceShim`. Ce serait toucher à
chaque chemin d'émission et au retrait de `ViewportOrigin`, là où le shader le fait gratuitement.

À vérifier pendant l'implémentation : tout état qui s'exprime en pixels physiques côté D3D11
(viewport, scissor s'il y en a un, épaisseur de ligne) doit passer par la taille physique. Lignes et
points n'ont jamais été observés (`D3D11Backend`, erreur explicite) ; s'ils apparaissent, ils
lèveront toujours.

### D3. MSAA par cible multi-échantillonnée + résolution

La cible de scène (couleur et profondeur) est créée avec `SampleDesc.Count = Anticrenelage`, après
`CheckMultisampleQualityLevels` (repli au nombre supporté inférieur, signalé une fois). Avant la
présentation, `ResolveSubresource` vers une texture simple échantillon, que `PresentView` lit. À 1,
aucune résolution : chemin identique à aujourd'hui.

Limite assumée : le `discard` de la couleur-clé n'est pas anticrénelé (seules les arêtes de
géométrie le sont). Alpha-to-coverage est une extension possible, pas dans ce changement.

### D4. Recevoir la décision de filtrage — slot `+0x90`

`SyntheticDevice` branche `+0x90` (thiscall, un `uint`, `ret 4`) sur un thunk qui mémorise la
dernière valeur reçue (1 ou 2), comme l'état d'un device Direct3D 3. La classe de la primitive se
décide **au dessin**, en relisant aussi dans `Winsys3dData` le genre de la liaison
(`Memo_00a2c48c` — que winsys3d écrit *après* l'appel à `+0x90`, d'où la lecture au dessin) et le mode
demandé (`FiltreTexture_00a2c508`) :

| genre | mode demandé | valeur `+0x90` | classe | `jeu` | `proche` | `bilineaire` |
|---|---|---|---|---|---|---|
| ≠ 0 (sprite) | 0 | — | interface | `FiltrageInterface` | `FiltrageInterface` | `FiltrageInterface` |
| 0 (polygone) | 0 (page 8 bits zoomée) | — | 3D non filtrée | proche | proche | bilinéaire |
| 0 | ≠ 0 | 1 (`Filter=0`) | 3D non filtrée | proche | proche | bilinéaire |
| 0 | ≠ 0 | 2 | 3D filtrée | bilinéaire | proche | bilinéaire |

**Révisé après la tâche 2.3.** La première version classait par le seul mode demandé (« mode 0 =
interface »). La trace a montré que le mode 0 recouvre aussi les **personnages 3D** (polygones sur
pages 8 bits zoomées), et que des éléments à plat (portraits, fonds, AUTO/MAN, boussole) sont des
polygones filtrables. Il n'existe pas de vrai critère 2D/3D à ce niveau — tout arrive projeté, sans
profondeur ; sprite / polygone est le plus proche, et c'est celui que le moteur emploie. Vincent a
choisi un critère 2D/3D ; celui-ci en est l'approximation disponible, déclarée comme telle.

**F10 est inerte, et le reste.** La touche n'est lue que par `LireManettes` (winsys3d
`FUN_00a049c0`), qui sort d'emblée quand `DAT_00dedc40` vaut 0 ; ce drapeau est dans la partie non
initialisée de `.data` (0 au chargement), et seuls les ordinaux 36/759 (`FUN_00a04fb0`, sans nom
d'export) le posent — `Blaze.c` ne les appelle jamais. Dans le jeu de 1998, F10 ne faisait donc
rien ; constaté en jeu par Vincent le 2026-09-26. Ce changement ne la ranime pas : `Affichage.ini`
offre le même choix, de façon documentée.

### D5. Filtrage manuel dans le shader, borné à la zone UV de la primitive

Un échantillonneur bilinéaire matériel mélangerait les texels transparents (noirs, alpha 0) et
déborderait sur les voisins de page. Le shader fait donc le bilinéaire **lui-même** quand la
primitive est filtrée :

1. quatre `Load` autour du point (après `GetDimensions`), coordonnées **bornées** au rectangle
   `[uvMin, uvMax]` de la primitive, en texels ;
2. poids bilinéaires, les texels d'alpha nul pesant zéro ; renormalisation par la somme des poids
   opaques ;
3. somme des poids opaques < 0,5 → `discard` (la transparence reste tout-ou-rien, comme
   `DDCKEY_SRCBLT`) ; sinon couleur renormalisée, alpha 1 texel.

Le rectangle `[uvMin, uvMax]` est calculé côté CPU par triangle lors du dépliage (`s_Expanded`,
`DrawPrimitive`) et porté par deux attributs `nointerpolation` ajoutés au sommet **de l'hôte** (le
`TlVertex` du jeu n'est pas touché ; l'hôte a déjà son propre tampon). Un quad coupé en deux
triangles rectangles donne le même rectangle aux deux moitiés : pas de couture sur la diagonale.

Le chemin « au plus proche » reste l'appel `Sample` actuel avec l'échantillonneur point : le rendu
fidèle ne dépend pas du nouveau code. Le filtrage passe par une constante par lot (`Filtered`),
les lots étant déjà découpés par liaison de texture.

Alternatives écartées : bilinéaire matériel + seuil alpha (halo sombre et bavure de page),
prémultiplication des textures (change les données téléversées et ne règle pas la page).

### D6. Mise à l'échelle finale

`PresentView` garde le 4:3 centré. `ajustee` : échantillonneur linéaire sur le quad de présentation
(inutile en `auto`, présenté à 1:1). `entiere` : facteur `floor(min(W/w, H/h))`, échantillonneur
point ; si ce facteur vaut 0, repli sur `ajustee`. Les films (`PresentImage`) suivent la même règle.

### D7. Installeur

`OpenForesia.Installer` écrit `Affichage.ini` depuis un modèle embarqué (UTF-8, commentaires en
français) après l'extraction, **seulement s'il n'existe pas**. La désinstallation le traite comme
n'importe quel fichier installé.

### D8. Déclaration legacy ++

Famille « confort du joueur », effet visible → interrupteur : ce sont les clés du fichier. Entrée
`LB-055` dans `legacy-bug.md`, statut `Contourné`, qui renvoie à ce changement et à
`Documentation/Reference/Affichage.ini.md`. Le slot `+0x90` n'est pas un écart : il rend au jeu un
réglage qu'il avait.

## Risks / Trade-offs

- [« mode 0 » ne coïncide pas avec l'interface] → tâche 2 le mesure avant toute autre chose ; au
  pire le critère de classe change, pas l'architecture.
- [Artefacts propres à la haute résolution — primitives jointives laissant voir des fentes d'un
  pixel physique, sprites alignés au pixel logique qui se chevauchent] → captures comparées à
  `ResolutionInterne = 1` sur un jeu d'écrans fixe (titre, menu, sélection, auberge, forêt, combat) ;
  une fente trouvée se note, et se traite dans un changement suivant si elle existe.
- [Le tremblement PSX devient plus visible sur des arêtes nettes] → attendu, hors périmètre ; noté
  dans `Affichage.ini.md`.
- [Coût GPU en `auto` sur 4K avec MSAA 8] → le repli MSAA et la valeur `1` bornent le pire cas.
- [Le shader filtré est plus cher (4 lectures)] → seulement sur les primitives filtrées ; négligeable
  pour le volume du jeu.
- [Lecture de `Winsys3dData` depuis l'hôte] → couplage hôte → DLL reconstruite, accepté et commenté :
  l'hôte est la seule couche qui connaît les deux.

## Migration Plan

Aucune migration : sans `Affichage.ini`, le joueur obtient les défauts modernes ; pour retrouver
l'image actuelle, trois lignes (`ResolutionInterne = 1`, `Anticrenelage = 1`, `Filtrage3D = proche`).
Retour arrière : supprimer le fichier ne rend **pas** l'ancien rendu (défauts modernes) — c'est
voulu et documenté.
