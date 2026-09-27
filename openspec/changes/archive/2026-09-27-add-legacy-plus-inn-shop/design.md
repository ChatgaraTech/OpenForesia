## Context

Voir `proposal.md` et `Documentation/LegacyPlus/legacy.md` (« En cours : LP-008 ») pour l'étape 1.
Ce qui compte pour l'étape 2 :

- **`TickPartyMember`** (`FUN_0050bf20`, `Tick/PartyMemberStep.cs`) est appelé pour chaque membre à
  chaque image de simulation. Après la garde « membre inactif » (`*slot & 0x4000008`), il a sous la
  main la position (`s + 0x1c/0x20/0x24`, décalée de 12 bits dans les relevés), le cap
  (`s + 0x32`, 4096 = un tour ; avancer, c'est `+sin` en x et `+cos` en z), les boutons **pressés
  à cette image** (`pad + 2`) et le masque du bouton d'action du membre (`perChar + 0x1fc4`). C'est
  là que le jeu ouvre son propre menu (bouton 3). Le tampon de quatre images du bouton d'action
  (`pad[0x0e]`) est rempli **plus loin** dans la même fonction, depuis `pad + 2`.
- **La quête et la carte** : `Res.s_Unknown_005de528` et `s_Unknown_005de52c` (ceux que le menu de
  mise au point affiche), tous deux `internal`.
- **Le moteur de boîtes** : `BindMenuLabels(programme, sujet, descripteur, rappel)` attache un
  programme de texte au descripteur de la carte du monde (`s_WorldMapDescriptor_005633b0`,
  `private`) ; `descripteur[0x8c7] & 1` signale une boîte active. Un programme est un flux d'octets :
  codes de contrôle sous `0x20`, texte en Latin-1. `AnnounceItem` (`Handlers/Chests.cs`) en fabrique
  un à la volée : un préfixe pris dans la table de textes (`labels + 0x2a8`), le nom de l'objet,
  puis **huit octets** de fin pris ailleurs (`labels + 0x2ac`), et un zéro.
- **La mise en scène d'un coffre** (`GrantChestContents_00530F20`) : `FreezeAllEntities()`, puis
  l'animation de chaque membre mise de côté (`s[0x40] = s[0x1a]`, `s[0x42] = s[0x18]`) ;
  `RestorePartyReactions_00530ED0`, rappel de fin déjà aiguillé par `UiText`, la défait et réarme
  les manettes.

## Goals / Non-Goals

**Goals (étape 2) :** ouvrir une boîte à l'armoire, dans le langage et la mise en scène du jeu.

**Non-Goals (étape 2) :** le menu d'achat, l'achat, la remise d'objet (étape 3).

## Decisions

### D1. Une couture de notification à l'entrée du pas d'un membre

```csharp
LegacyPlus.Coutures.PasDuMembre(slot, pad, perChar, s_WorldMapDescriptor_005633b0);   // Couture legacy ++ (LP-008)
```

Posée juste après la garde « membre inactif » de `TickPartyMember`. Elle ne rend rien ; le tweak
peut effacer le bit d'action de `pad + 2` (l'appui est alors consommé avant que le jeu ne le lise).
Le descripteur, `private`, passe par la couture plutôt que d'élargir sa visibilité. *Écarté* :
la couture `ButinAtteint` (dans une partie du pas qui n'est pas atteinte dans tous les états ; le
relevé de position n'y a écrit aucune ligne périodique).

### D2. Le déclenchement

Quête 0, carte 82 ; membre debout (`0x80000000` posé, `0x50000000` absent) ; distance au plan du
sol à (−1372, 900) sous 120 unités ; écart entre le cap et la direction de l'armoire
(`atan2(dx, dz)` en 4096e de tour) sous 512 (45°) ; bit d'action pressé à cette image ; aucune
boîte active.

### D3. La boîte : les codes du jeu, le texte du tweak

Le programme = la **première instruction** du préfixe `labels + 0x2a8` — `0b 01 a0 00`, un **appel de
sous-libellé** (`UiText.Handler_00544c10` : code `0x0b`, racine de ressources, index sur 16 bits) qui
ouvre la boîte —, le texte en Latin-1, puis les **huit octets** de `labels + 0x2ac` —
`0b 01 ac 00 08 01 00 00` : l'appel du libellé de fermeture, `08 01` (dégel), fin —, puis zéro.

*Première version, fausse* : couper le préfixe au premier octet imprimable. L'opérande `0xa0` l'est ;
le programme commençait par `0b 01 42…` (« Bie » lu comme opérande), et le rendu a planté
(`AccessViolationException` dans `RunTextEngine`, premier essai de Vincent). Un préfixe ou une fin qui
ne commence pas par `0x0b` n'ouvre plus rien — la recette d'`AnnounceItem`, texte remplacé. Le tampon est une
allocation native gardée tant que le jeu tourne. Le premier appel écrit ces octets dans
`legacyplus-boutique.log`, pour vérifier le format au premier essai.

### D4. La mise en scène

Celle du coffre, reprise dans le tweak : `FreezeAllEntities()`, animations mises de côté, et le
rappel `0x00530ed0` (`RestorePartyReactions_00530ED0`) passé à `BindMenuLabels` avec le **membre**
comme sujet (surcharge à pointeur).

### D5. `Boutique.ini`

Lu une fois par `ReglagesBoutique`, sur le contrat de `LegacyPlus.ini` : `[Boutique] Actif`, et
`[Catalogue]` (nom = prix), déjà lu et compté à l'étape 2, employé à l'étape 3.

### D6. Étape 3a — un écran dessiné, pas une boîte

Le moteur de boîtes n'a **pas de code de choix** (liste, curseur) ; « Enchère », la seule vente du
jeu, vit dans le menu objets hors partie (état 6). L'écran est donc dessiné par le tweak avec les
outils du jeu : `Prims.DrawTextRun` sur la **police 8×8** (le descripteur `{0x40803000,
0x02300000}` de `s_GlyphAtlas_00566c28`, que l'écran de création du groupe emploie : origine
(0, 0x30), cases de 8×8, 16 par ligne), et un **rectangle semi-transparent** — le paquet GPU de
cinq mots de `FillScreenQuad_0051F940` (commande `0x62`, mode `0xe1000200`), émis en dernier
dans l'ordering table pour être dessiné sous le texte. Les lettres accentuées perdent leur accent
(la police n'en porte pas de sûr).

### D7. Une couture de dessin

```csharp
cursor = LegacyPlus.Coutures.DessinSurImpression(otBase, cursor);   // Couture legacy ++ (LP-008)
```

Dans `DrawWorldMapFrame`, juste après le panneau d'interface — avant la boussole, les panneaux du
groupe et le monde, pour passer avant l'épuisement du budget de 2 500 primitives (voir la
préconisation de `legacy.md`). Par défaut, le curseur tel quel.

### D8. La manette est lue dans le dessin

`FreezeAllEntities` pose `0x4000000` sur **toutes** les entités, joueurs compris : leur pas
(`TickPartyMember`) est alors sauté, et `PasDuMembre` avec. La manette de l'acheteur est donc lue
dans `DessinSurImpression`, à chaque image dessinée : `Inp.s_InputStateBase_00604a20` + type de
manette × 0x10 ; l'octet des appuis `pad[2]` en disposition PlayStation (haut `0x10`, droite
`0x20`, bas `0x40`, gauche `0x80` — confirmée par `s_PadDirection_00562dc8` ; `0x08`, le
bouton qui ouvre le menu du jeu) et le bouton d'action du membre (`perChar + 0x1fc4` sur
`pad + 2`). Haut / bas : choisir ; gauche / droite : page ; action : acheter ; menu : sortir.

### D9. L'achat, en 3a

Vérifié (la bourse `perChar + 0x26c` couvre-t-elle le prix ?) et **journalisé seulement** dans
`legacyplus-boutique.log` ; rien n'est débité ni remis avant 3b et 3c.

### D10. Étape 3b — l'index des objets, lu dans les fichiers du jeu

Chaque carte a son catalogue, et seul celui de la carte courante est en mémoire. `CatalogueObjets`
lit donc, au premier accès à la boutique, les fichiers du jeu installés (`FrenchSYSTEM.INI`,
`FrenchBLAZE.ALL` par accès direct, secteurs de 2 Ko), par la chaîne du jeu : racine `+0x48` →
état 3 `+0x48` → zones `+0x48` → répertoires de sous-zone `+0xb0` → blocs bruts (un compte, des
décalages) → un morceau par objet : **fiche `0x140`**, puis **vignette `0x140`** (palette de 16
couleurs BGR 5-5-5, image 24 × 24 en 4 bits à `+0x20`) — les `0x280` octets d'un emplacement
d'inventaire. C'est le lecteur de `OpenForesia.Tools bestiaire` porté tel quel (`Blob`,
`Fixup` de `FUN_00532070`, `ItemCatalog`, `ItemDisplayName`), donc les noms sont ceux
d'`objets.csv` et de `Boutique.ini`. Il s'arrête dès que tous les noms voulus sont trouvés.
Vérifié hors ligne : les 272 objets du catalogue ont une vignette complète.

### D11. L'aperçu : l'icône de l'objet sous le curseur, sans mémoire vidéo

Aucune place libre n'est connue en mémoire vidéo pour y téléverser des icônes. L'icône de l'objet
**sous le curseur** est donc dessinée à droite de la liste, agrandie deux fois, en rectangles plats
opaques (commande `0x60`) : une bande par suite de pixels de même couleur sur une ligne, la couleur
`0x0000` transparente. Environ deux cents petits paquets, loin de ce que le tampon supporte ; dix
icônes à la fois pèseraient trop. Demandé par Vincent au premier essai de l'écran.

### D12. Étape 3c — l'achat, par les règles du coffre reprises sur nos données

`GrantChestContents_00530F20` (le choix de l'emplacement) et `StoreItemInInventory_005312D0` (le
rangement) lisent l'objet dans le catalogue **de la carte** et son icône dans la mémoire vidéo **de
la carte** : un objet acheté n'y est pas. Leurs règles sont donc reprises dans `BoutiqueAchat`, à
l'identique, sur la fiche et la vignette de `CatalogueObjets` :

- **l'emplacement** : empilement sur un emplacement de même nom court (16 octets) si l'objet est
  empilable (`+0x13 & 0x30`), le bit `0x8000` de l'emplacement étant reporté ; sinon le **dernier**
  emplacement libre, compteur et drapeaux remis à zéro ; dix pris = refus (« Inventaire plein. ») ;
- **le rangement** : la fiche à `+0x300` de l'emplacement (`perChar + n° × 0x280`), le compteur
  `+0x310` et le bit `0x8000` de `+0x312` préservés, puis compteur + 1 (plafond 99) ; la palette
  et l'image à `+0x440` / `+0x460` ; et l'icône **envoyée** (`Winsys3d.Ordinal_674`, LoadImage)
  à la place de mémoire vidéo que le rangement d'origine lui donne — palette en
  (n° × 16, 0x1f1 + joueur), image 6 × 24 en (0x180 + n° × 6, 0x100 + joueur × 24) ; le son `0x29` ;
  la famille `0x32c9..0x32cf` en posture 4 incrémente son compteur au lieu d'entrer ;
- **la bourse** (`perChar + 0x26c`) est débitée seulement si l'objet a été rangé.

**Écart voulu** : l'objet acheté est **identifié** (`+0x12 |= 0x8000`). Un objet de coffre le perd
(`&= 0x3fff`) et reste à faire expertiser ; on sait ce qu'on achète.

## Risks / Trade-offs

- **[Format de la boîte]** : les codes pris à `+0x2a8` / `+0x2ac` sont ceux d'une boîte d'objet ;
  s'ils attendent un nom d'objet ou une icône, la boîte peut s'afficher mal. → Le journal les
  montre ; on corrige au premier essai.
- **[Les manettes pendant la boîte]** : le coffre les désarme ailleurs que dans ce qui est repris ;
  si un joueur bouge pendant la boîte, il faudra reprendre ce désarmement aussi.
