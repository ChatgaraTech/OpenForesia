## Context

See proposal.md — Why. What the repository already knows is gathered in `Conception/08-Exports.md`;
the relevant facts:

- **The loading chain is already replayed offline** by `OpenForesia.Tools/Bestiaire.cs`:
  `SYSTEM.INI` → state 3 → zone → sub-area, blobs of `BLAZE.ALL` addressed by sector, pointers fixed
  up (`Blob.Fixup`). The `terrain` verb reuses it.
- **What a sub-area loads** (`LoadWorldMapSubArea`, `Screens/State03/WorldMap.cs`): the textures of
  `+0x10` (each decoded into video memory, filling one slot of the decode table `0x005e8f00`); the
  four chunk lists `+0x14..+0x20` (`RelocateModelEntry`), whose spans are kept per list; the model
  bundles of `+0x24` (`RelocateModelBundle`), appended to the chunk table; models `+0x2c`, animations
  `+0x7c`, second models `+0xac`.
- **How a chunk is drawn** (`DrawChunk`, `Engine/Model3d.cs`): vertices at `+0x40`, faces of 0x20
  bytes at `+0x28` (four vertex indices, colour indices, two words of `u,v` pairs, an attribute word
  adjusting texture page and palette per face), a 32 × 32 cell grid at `+0x34` for the swept lists,
  four buckets (flat/gouraud × triangle/quad), the chunk's base page and palette in `+0x3c`.
- **The `terrain` verb** reads the same four lists as faces of 16 bytes for `TryPlaceEntity`; some
  indices fall outside the vertices. The two readings disagree, and settling which is right comes
  first.
- The tools project is not trimmed and has no WinForms constraint; the game itself is untouched.

## Goals / Non-Goals

**Goals:**
- A sub-area that **looks like the game** once opened: the right faces, the right textures, the
  right colours, in the right place.
- Every value read the way the game reads it, with the C# function it mirrors named next to it.
- A format the remake team uses directly (glTF in Unreal/Blender, JSON in Data Tables).

**Non-Goals:**
- Animated models, characters, monsters as meshes (next change: `add-model-exporter`). Monsters
  appear here as markers only.
- Lighting, fog, palette animation (water, fire), sky: noted in the output, not rendered.
- The world-map screen, the menus, the cutscenes.
- Any change to the game.

## Decisions

**1. A tool verb that reads the data, not a capture of the running game.** The game projects its
vertices on the CPU into screen space before drawing; what reaches Direct3D has lost world
coordinates. Reading the chunks offline keeps them, needs no game run (Vincent runs the game, not
the agent), and exports every sub-area in one pass. *Alternative:* hooking `DrawChunk` in the game —
rejected: screen-space, one camera at a time, and it would touch `src/Blaze/`.

**2. Re-read the formats in the tool, mirroring the game, rather than linking `src/Blaze/`.** The
reconstruction works on raw pointers in emulated memory, with global state (decode table, camera).
The tool reads blobs at virtual address 0, as `bestiaire` already does. Each reader carries a
`// Miroir de : <Fonction C#> (FUN_…)` comment, so a drift is findable.

**3. Video memory is rebuilt as the game builds it.** A 1024 × 512 array of 16-bit words; each TIM
of `+0x10` is written at the rectangle its header names (image and palette), the way
`DecodeResourceChunk` uploads it, and the decode-table slot it fills is recorded. A face's texture is
then: page = the chunk's `+0x3c` low byte adjusted by the face attribute word, palette = the decode
slot of `+0x3c` byte 2 adjusted likewise — exactly the two words `DrawChunk` composes. One PNG per
distinct (page, palette, depth), 256 × 256, sampled from the rebuilt memory; texel `0x0000` is
transparent (the PlayStation rule), others opaque. *Alternative:* one texture per TIM — rejected: a
face addresses a page and a palette, not a file, and pages straddle TIMs.

**4. glTF written by hand.** A `.gltf` (JSON) with one `.bin` and the PNGs beside it; one mesh
primitive per material; positions as floats; texture coordinates normalised over 256; vertex
colours as the game's colour indices resolve (the `+0x24` table, before lighting). The PlayStation's
Y axis points down: the exporter flips it, and records the scale (game units, unscaled) in the
asset's `extras`. *Alternative:* SharpGLTF or FBX SDK — rejected: a dependency for a few hundred
lines of well-specified JSON.

**5. Collision as a second node, not a second file.** One file per sub-area keeps both layers
aligned by construction; Unreal can import the node as collision (`UCX_` naming) or ignore it.

**6. Placements from the code that `bestiaire` already has.** Formations, scenery (`+0x30`), chests,
entry points are already read for the CSVs; the exporter writes the same records for one sub-area, in
model coordinates, as JSON.

**7. The visit page is a single HTML file with its own WebGL viewer.** The spec wants it offline, so
no CDN. A small hand-written WebGL renderer (textured triangles, alpha test, orbit camera, two
toggles, hover names) is a few hundred lines; the page embeds the sub-area's geometry and its PNGs in
base64. *Alternative:* three.js inlined — rejected: 600 KB per page for what a few hundred lines do.

**8. Verification against the game, by eye, then by number.** For a reference sub-area (forest 0),
the agent renders the visit page in headless Chrome and compares it to a capture Vincent provides
(or one already in `scratchpad/`), and checks numbers: chest positions equal `coffres.csv`, every
face index within its vertices, every face's texture inside its page.

## Risks / Trade-offs

- [The 16-byte and 0x20-byte face readings disagree] → task 1 settles it on the binary and the
  `TryPlaceEntity` / `DrawChunk` code before anything is written; the losing reading is corrected in
  `terrain` too.
- [A chunk's position in the map is not in the chunk] → the draw call's placement block (position,
  angles, scale) is traced from the caller of `DrawChunk`; until it is found, chunks are exported at
  their own origin and the summary says so.
- [Texture pages adjusted per face in ways not yet understood] → per-face page and palette are
  logged; a face whose texture falls outside a loaded TIM is drawn magenta and counted, not hidden.
- [Semi-transparency modes (the attribute's top bits) have no exact glTF equivalent] → exported as
  alpha blend with the mode named in material `extras`.
- [Some sub-areas reuse another's data or load nothing] → reported in the summary, not an error.

## Open Questions

- The game-unit to centimetre factor for Unreal: measured once characters are exported; the glTF
  carries game units meanwhile.

## Relevé des formats (2026-11-20, avant l'écriture du lecteur)

- **Morceaux des listes 1 à 3** (`+0x18..+0x20`, affichage) : faces de 0x20 octets. `face[0] = i0 | i1<<16`, `face[1] = i2 | i3<<16`, `face[2]/[3]` les indices de couleur, `face[4]` = UV de v0 (bas) et v1 (haut), `face[5]` = UV de v2 (bas) et v3 (haut), `face[7]` les attributs (octet haut = code GPU). Sommets à `+0x40`, 8 octets, compte = `u16 +8` brut. Placement identité : les sommets sont des coordonnées du monde.
  - Ordre des sommets : `DrawChunk` (sans balayage) saute `+0x10..+0x16` × 0x20, puis lit les quatre seaux `+0x18..+0x1e` à la suite. Avec balayage, ce sont les cellules de la grille `+0x34` qui donnent les indices, sans les faces « sautées » de chaque cellule.
  - Page et palette d'une face : `slot = table[(attr+0x3c >> 16) & 0xff]`, puis `clut = ((faceAttr & 0xfcf0) * 0x1000 + (slot & 0xffff0000)) >> 16` et `tpage = ((faceAttr & 0xef0000) + (((slot | 0x800) & 0xffff) << 16)) >> 16`. L'attribut `+0x3c` est le dword `entrée + 4` de la liste.
- **Morceau de collision** : liste 0, morceau 0. Faces de 16 octets (`TryPlaceEntity`), compte `u16 +4`. Sol quand `matières[2 + mat*8] < 0`.
  - **Le verbe `terrain` lit à tort les quatre listes dans ce format.** Il faut le corriger.
- **Modèles de décor** : `DrawStaticModel` (genre 1) et `DrawDepthSortedModel` (genre 2), au rang `chunks[span(0x838) + octet +0x1c]`. Seuls les modèles du **dernier** paquet `+0x24` entrent dans la table.
  - Faces de 16 octets : `rec[0]` = quatre indices sur un octet, `rec[2]` = UV de v0 et v1, `rec[3]` = UV de v2 et v3.
  - `tpage = slot.bas | (attr & 0xff) << 5`, `clut = slot.haut + décalage du paramètre flags`.
  - `AdjustModelTexturePages` ajoute `(attr >> 8) & 0xc0` au **v** des quatre UV.
  - Placement : position `rec[1]` bas = x, `rec[1]` haut = y, `rec[2]` bas = z. Angles : `rec[2]` haut, `rec[3]` bas, `rec[3]` haut. Le monde s'obtient par `RotMatrix(angles) · v + pos`.
  - Le signe de y est tranché : la position d'une entité a y vers le haut, et le moteur en prend l'opposé (`TryPlaceEntity`, `LoadPointAsMatrixTranslation`). Vérifié sur le rendu : les arbres sont debout.
- **Textures** : `DecodeResourceChunk`, déjà écrit dans `OpenForesia.Tools/ExportCarte/Vram.cs`.
