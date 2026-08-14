# TODO

Point d'entrée entre deux sessions. État au soir du 2026-08-14.

---

## Où on en est

L'écran de sélection des personnages est **atteint pour la première fois**. Le jeu démarre, passe
le menu, entre dans l'état 4, charge ses ressources, entre dans la boucle de frame, exécute le
sous-état 0 et son carrousel — puis plante à la passe de texte finale.

Chaîne reconstruite et exercée :

```
RunGameStateTick case 4
  └─ RunCharacterSelectState
       ├─ LoadCharacterSelectResources     OK
       └─ RunCharacterSelectFrameLoop      OK (sauf le bloc « en scène », sous-états 1 à 6)
            └─ CharacterSelectSubState0    OK, zéro throw
                 ├─ DrawAnimatedModel      s'exécute sans faute
                 └─ ProjectPoint           OK
```

Rien n'est commité. Le build est vert (0 erreur, seuls les `NETSDK1189` habituels).

---

## 1. Le plantage — à corriger en premier, c'est mécanique

```
System.InvalidOperationException: FUN_005443a0 masks its descriptor to 24 bits,
but this one is at 0x02D2C3B8. Allocate it with AllocLowZeroed.

   at RunTextEngine                 Blaze.cs:8525
   at CharacterSelectSubState0      Blaze.cs:7038
   at InvokeCharacterSelectSubState Blaze.cs:6686
   at RunCharacterSelectFrameLoop   Blaze.cs:6555
```

C'est un garde-fou que le projet avait **déjà** posé, et dans lequel je suis tombé de plein fouet.
La liste d'affichage est adressée sur 24 bits (voir `CLAUDE.md`, section sur `DrawFlatPrimitive`) ;
`AllocLowZeroed` existe précisément pour ça et alloue sous `0x01000000`.

**À faire** : reprendre tous les blocs que j'ai alloués hier avec `NativeMemory.AllocZeroed` et qui
finissent dans du code de liste d'affichage, et les passer à `AllocLowZeroed` :

| symbole | pourquoi |
|---|---|
| `s_UiDescriptors_00560ff0` | passé à `RunTextEngine` — c'est celui qui plante |
| `s_StatusDescriptor_00560728` | passé à `RunTextEngine` par la boucle de frame |
| `s_Unknown_005606f8` | passé à `BindMenuLabels` |
| `s_CharacterSlots_00578420` | son `+0x1c` part dans `DrawAnimatedModel` |
| `s_SceneEntity_00572b48` | destination de la copie à la validation |
| `s_CameraMatrix_006008e0` | recopié dans le registre MATRIX du GTE |
| `s_ModelTable_005e96a0`, `s_AnimationTable_005e9640` | leurs entrées sont déréférencées par le rendu |
| `s_SelectSubStateBytes_00562a98`, `s_CameraVector_00604928` | par cohérence, à vérifier au cas par cas |

Attention : `AllocLowZeroed` prend une `uint` (une taille en octets), pas le couple
`(count, size)` de `NativeMemory.AllocZeroed`. Et elle appelle `EnsureDisplayListArena()` en
entrée — l'ordre d'initialisation des champs statiques compte, le commentaire de la fonction
l'explique.

**Ne pas** se contenter de corriger `s_UiDescriptors_00560ff0` pour faire taire l'exception : le
garde-fou n'existe que sur `RunTextEngine`, les autres blocs corrompraient silencieusement.

---

## 2. Vérifier que quelque chose se dessine vraiment

Observation à creuser dans la trace : les compteurs du shim se figent une fois l'état 4 entré.

```
[SHIM] bilan : ... BeginScene=361 EndScene=360 DrawPrimitive=34545
[SHIM] bilan : ... BeginScene=421 EndScene=420 DrawPrimitive=34545
```

`DrawPrimitive` n'augmente plus alors que les frames continuent. Deux lectures possibles, à
départager :

- les primitives sont émises mais dans un emplacement d'OT que `DrawOTag` ne parcourt pas ;
- rien n'est émis du tout — par exemple parce que `*glowR > 0xff` n'est vrai pour aucun
  emplacement, ou parce que le budget `DAT_005e9818` est déjà épuisé quand on arrive là.

Le plus rapide : poser une trace temporaire comptant les appels à `DrawAnimatedModel`, les
primitives émises par `EmitModelQuads` et la valeur de `s_Unknown_005e9818` en fin de frame. Le
projet a déjà l'idiome (`OPENFORESIA_TRACE_MENU`), s'en inspirer et **retirer l'échafaudage après**.

---

## 3. Les deux réserves connues sur `DrawAnimatedModel`

À garder en tête si l'image est fausse plutôt qu'absente :

- **Chemin MMX non transcrit.** Seul le chemin scalaire du produit de matrices est écrit ; l'original
  branche sur `MMXInstalled_exref`. L'équivalence des deux n'a **pas** été vérifiée opérande par
  opérande. `TODO(REVERSE)` posé sur place.
- **Quirk des pages de texture.** `tpageWord` et `uvWord` sont calculés une seule fois depuis le
  **premier** modèle du paquet, alors que chaque partie relit ses propres attributs. C'est dans
  l'original, transcrit tel quel — mais si les parties sortent avec la mauvaise texture, c'est là
  qu'il faut regarder d'abord.

---

## 4. Ensuite seulement : le reste de l'écran

Une fois quelque chose de visible à l'écran :

- **`FUN_00520ae0`** + le bloc de globales `0x00572b40` — c'est tout ce qui manque au bloc
  « en scène » de la boucle de frame (sous-états 1 à 6).
- **Les sept autres sous-états** (1 à 7). Tous décompilés, dans `Legacy/Raw/Blaze/FUN_*.c`.
  `InvokeCharacterSelectSubState` lève une exception qui nomme le fichier exact.
- **`FUN_005066c0`** — pointeur de code trouvé dans `DAT_005606f8`, absent de `Blaze.c`. S'il est
  appelé, ça se verra ; il faudra alors un re-run Ghidra ciblé de plus.

---

## Rappels d'outillage (ils ont coûté du temps hier)

**Lancer le jeu.** `dotnet run --project OpenForesia` **ne marche pas** depuis le shell : l'app est
en x86 et le SDK sur le PATH n'a que le runtime x64. Lancer l'exe directement, il trouve le
runtime x86 (9.0.8) de `C:\Program Files (x86)\dotnet` :

```
OpenForesia/bin/Debug/net9.0/OpenForesia.exe
```

Même cause pour `OpenForesia.Tools`, qui ne tourne pas du tout. Palliatif écrit hier :
`OpenForesia.Tools/exports.ps1`, un parseur de table d'exports PE en PowerShell.

**Re-run Ghidra.** Les scripts `.py` de `~/ghidra_scripts` sont morts (Ghidra 12 rétrograde Jython
en extension non installée). Utiliser `~/ghidra_scripts/ForceDecompileCharacterSelect.java`,
**depuis PowerShell** (le `.bat` casse son propre chemin sous Git Bash), avec `-readOnly` :

```powershell
$gh = "F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "F:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "C:\Users\Sagara\ghidra_scripts" `
      -postScript ForceDecompileCharacterSelect.java
```

Le script crée **toutes** les fonctions manquantes avant d'appeler `analyzeChanges` puis de
décompiler. Ne pas inverser : sans la passe d'analyse, les corps sortent tronqués et pleins de
`func_0x005…`.

**Avant de déclarer un symbole, greper son adresse.** Trois doublons créés en trois séances
(`DAT_00564a48`, ordinaux 450/451, `DAT_005e9818`). Le fichier a dépassé la taille où on peut s'en
passer.

**Quand le pseudo-C fait de l'arithmétique de pointeur, aller voir l'assembleur.** Deux pièges
évités hier de cette manière : le seuil de profondeur (`-0x50` sur un `uint*` = `-0x140` octets) et
le pas des manettes (`* 4` dans le pseudo-C, `SHL 4` dans le listing).

---

## Contexte accumulé

Tout le détail est dans `OpenForesia/src/Blaze/REVERSE_STATUS.md`, sections datées du 2026-08-14 —
la carte du banc de registres GTE, la structure de `FUN_0051d1f0`, les signatures reconstruites
depuis l'assembleur. `Documentation/DLL/winsys3d.md` a les familles libgpu / libgte / registres GTE.
`Documentation/DataSymbols.md` a les tables relevées dans le binaire.

Change OpenSpec close et validée : `openspec/changes/finalize-character-select-loading/`.
Le travail depuis n'est pas couvert par une change — à décider s'il en faut une.
