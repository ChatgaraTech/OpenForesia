## 1. Remettre le bloc en état de compiler

- [x] 1.1 Parenthéser les six casts `nint` mal lus par le parseur : `(int*)(nint)*entry` →
      `(int*)(nint)(*entry)` (lignes 5418, 5432, 5445, 5457) et de même pour
      `(ushort*)(nint)*offsets` et `(byte*)(nint)*offsets` (5479, 5480)
- [x] 1.2 Convertir les deux appels `NativeMemory.AllocZeroed(0x40, sizeof(nint))` vers la surcharge
      `(nuint, nuint)` (5593, 5594)
- [x] 1.3 `dotnet build OpenForesia.sln` : zéro erreur, seuls les deux avertissements `NETSDK1189`
      attendus subsistent

## 2. Corriger la portée des deux tables globales

- [x] 2.1 Renommer `s_SelectSpriteTable_005e96a0` → `s_ModelTable_005e96a0` et
      `s_SelectPaletteTable_005e9640` → `s_AnimationTable_005e9640`
- [x] 2.2 Renommer `RelocateSpriteBundle` / `RelocateSpriteEntry` / `AdjustSpritePages` →
      `RelocateModelBundle` / `RelocateModelEntry` / `AdjustModelTexturePages`
- [x] 2.3 Renommer `RelocatePaletteBundle` / `RelocatePaletteEntry` → `RelocateAnimationBundle` /
      `RelocateAnimationEntry`
- [x] 2.4 Ramener la capacité des deux tables de `0x40` à 24 entrées, avec en commentaire la mesure
      qui la justifie (`0x005e96a0 - 0x005e9640 = 0x60`, soit 24 pointeurs 32 bits)
- [x] 2.5 Écrire au-dessus de chaque table le commentaire qui établit sa portée globale : les sites
      d'accès (`Blaze.c` 2043, 14900, 19190, 23424, 25455, 52332, 64765, 79042) et le second
      remplisseur `FUN_005069e0` (`Blaze.c:78876`)
- [x] 2.6 Documenter en commentaire la preuve du rôle de chacune : longueur d'animation lue à `+0x16`
      (`Blaze.c:55840`), compte lu à `[-4]` (`Blaze.c:79042`), enregistrements de 16 octets et
      sommets divisés par trois côté modèle

## 3. Assumer les deux écarts avec l'original

- [x] 3.1 Commenter les gardes `null` sur `directoryBase` et `mainMem` comme un ajout délibéré,
      l'original déréférençant sans filet
- [x] 3.2 Commenter l'omission de `FUN_0040a600()` : corps décompilé vide (`Blaze.c:4319`), donc
      neutre — mais signature Ghidra fausse, puisqu'il est appelé avec des arguments ailleurs

## 4. Consigner les trouvailles

- [x] 4.1 `Documentation/Reference/DataSymbols.md` : la table `PTR_LAB_00560708`, ses huit entrées
      (`0x00507010`, `0x00507cb0`, `0x00508410`, `0x00508860`, `0x00508e00`, `0x00509470`,
      `0x005098f0`, `0x0050a910`), le décalage fichier `0x15ED08` d'où elles sortent, et la borne
      supérieure donnée par `DAT_00560728`
- [x] 4.2 `Documentation/Reference/DataSymbols.md` : la table d'octets `DAT_00562a98`, neuf rangées de quatre
- [x] 4.3 `Documentation/Reference/DataSymbols.md` : la disposition de l'entrée de modèle déduite de
      `FUN_00532450` et `FUN_00532530` (compteurs `+0x10`…`+0x1e`, pointeurs `+0x24`…`+0x38`, bits
      de page `+0x3c`) et celle du paquet d'animations
- [x] 4.4 `Documentation/Reference/DataSymbols.md` : noter que les sept handlers sur huit ouvrent sur
      `mov eax, [0x00562a48]`, ce qui identifie ce drapeau comme « première frame du sous-état »
- [x] 4.5 `REVERSE_STATUS.md` : l'état 4 passe de « non couvert » à « chargement reconstruit, boucle
      de frame `FUN_00506880` à venir »
- [x] 4.6 `REVERSE_STATUS.md` : lever le blocage `PTR_LAB_00560708` et le requalifier en « re-run
      Ghidra ciblé », les huit adresses étant désormais connues

## 5. Vérification

- [x] 5.1 Relire une dernière fois les six fonctions contre `Blaze.c` (`FUN_005066e0`,
      `FUN_00532640`, `FUN_00532450`, `FUN_00532530`, `FUN_005326d0`, `FUN_005326b0`)
- [x] 5.2 `dotnet build OpenForesia.sln` vert
- [x] 5.3 `openspec validate --change finalize-character-select-loading` vert
