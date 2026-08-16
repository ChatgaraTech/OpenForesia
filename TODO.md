# TODO

Point d'entrée entre deux sessions. Remis à plat le 2026-08-16 en fin de journée (dixième→treizième
passe de la journée) — l'historique détaillé complet reste dans `REVERSE_STATUS.md` (sections datées
2026-08-16), ce fichier ne garde qu'un résumé et surtout la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

---

## Ce qui a été fait aujourd'hui (résumé — détail complet dans `REVERSE_STATUS.md`)

1. **Bug d'aliasing systémique trouvé et corrigé** (confirmé en jeu, commité par l'utilisateur) :
   cinq puis six champs (`dialogGate`/`confirmGate` et voisins, le compteur « Bonus ») étaient des
   `byte` isolés au lieu d'être des décalages dans `s_StatusDescriptor_00560728`/
   `s_PanelSelection_005e9060` — corrigés en propriétés `ref byte`. Touchait tous les sous-états de
   sélection de personnage.
2. Trois bugs visuels corrigés et confirmés : control code texte `0x14` (`Handler_00544fb0`, texte
   doré « Bonus spécial ! »), curseur du clavier de saisie du nom (mauvais symbole, décalé de 4
   octets), et le tirage des dés du bonus qui n'affichait pas son total.
3. **`RunGameState1Entry`** (`thunk_FUN_00536180`, jamais touché avant) transcrite et confirmée en
   jeu — la chaîne complète création + sauvegarde va maintenant jusqu'à l'état de jeu 1 (le vrai
   jeu), qui se scinde en deux chemins : `FUN_00536270` (chemin A, personnage déjà en mémoire) et
   `FUN_00536ea0` (chemin B, sinon).
4. Toutes les dépendances de ces deux orchestrateurs ont été résolues une à une :
   - `FUN_0051c120` → **`DrawTextRun`** : primitive de texte générique (~50 sites d'appel dans tout
     le jeu), mal câblée depuis une séance antérieure comme `ControllerBannerStub` à usage unique.
     Corrigée après désassemblage brut (le pointeur passé à `DrawSpriteVariant`/`DrawFlatPrimitive`
     est en fait 8 octets sous ce que suggère la pseudo-C Ghidra, avec une auto-référence entre les
     deux). Câblée sur son seul site d'appel testable pour l'instant (bannière manette dans
     `FrameEnd`) ; **le déclenchement réel de cette bannière n'est pas encore vérifié visuellement**.
   - `FUN_00535710` → **`RefreshPartyAbilityMask`** : recalcule un masque de compétences du groupe.
   - `FUN_00536ba0` → **`LoadPartyMemberResources`** : charge/relocalise deux textures + un modèle +
     une animation par emplacement du groupe. La plus dense des trois — déléguée à un fork pour la
     traque assembleur (deux pièges trouvés : nettoyage de pile différé après un `CALL` cdecl, et
     deux appels stdcall sans nettoyage explicite), vérifiée à la main sur les blocs de texture.
   - `FUN_00529ad0` — déjà fait (`PlayMusicTrack`), aucun travail nécessaire.
   - `FUN_005126c0` → **`LoadSaveDirectory`** : énumère les fichiers de sauvegarde. Bonus inattendu :
     c'est le **rédacteur manquant** d'un champ déjà référencé ailleurs (`s_CardEntryCount_00604981`,
     jusqu'ici figé à 0 avec un commentaire « jamais posé ») — corrigé au passage dans
     `RenderSaveScreen`, case `0xa`.
   - `FUN_00547960` — identifiée comme l'équivalent CRT de `sprintf` (variadique). **Pas
     implémentée** : le bon endroit est au moment de transcrire son unique site d'appel dans
     `FUN_00536ea0`, une fois la chaîne de format connue. Voir « Pièges et repères » ci-dessous.

**Build vert, testé sans régression après chaque étape** (`playtest.ps1 -Entry 1 -Seconds 15 -Shot`).

---

## La suite : `FUN_00536270` et `FUN_00536ea0`

C'est le morceau qui reste. Toutes leurs dépendances sont maintenant résolues (voir ci-dessus) —
plus aucun blocage indirect, seulement le travail de transcription des deux fonctions elles-mêmes.

| fonction | rôle | lignes `Blaze.c` | état |
|---|---|---|---|
| `FUN_00536270` | chemin A (personnage déjà en mémoire) | `82277`–`82637` (361 lignes) | 140 premières lignes lues directement (voir ci-dessous) ; le reste (`82416`–`82637`) pas encore lu |
| `FUN_00536ea0` | chemin B (sinon — probablement : charger une sauvegarde) | `82764`–`83257` (494 lignes) | pas encore lu directement, seulement survolé par un agent de recherche pour les cibles d'appel |

**Ce qu'on sait déjà de `FUN_00536270`** (les 140 premières lignes lues) : un écran de sélection de
groupe — rendu du genre (« FEMALE ») et de la classe (table `PTR_s_Fighter_0056c7c0`) via
`FUN_0051c120` (= `DrawTextRun` maintenant), jusqu'à 4 membres du groupe (`local_24` plafonné à 4),
navigation manette par ligne/colonne (`local_18`/`uVar8`), sortie en posant `DAT_005de510 = 0`
(retour au menu principal). Cohérent avec `FUN_00536ea0` = « charger un groupe sauvegardé » vu que
`LoadSaveDirectory` est dans ses dépendances.

**Point d'attention pour la suite de `FUN_00536270` et pour `FUN_00536ea0`** :
- `FUN_0051c120`/`DrawTextRun` prend un `int* cursor` — un bloc de 3 int construit par l'appelant :
  `cursor[0]` = pointeur vers un descripteur d'atlas de 2 dwords, `cursor[1]` = valeur forwardée
  telle quelle, `cursor[2]` = position d'écran packée (X bas, Y haut). Voir le docstring de
  `DrawTextRun` dans `Blaze.cs` pour le détail complet avant de câbler un nouveau site d'appel.
- `FUN_00547960` (sprintf CRT) sera probablement rencontrée dans `FUN_00536ea0` — ne pas essayer de
  la généraliser, juste transcrire le formatage exact du site d'appel une fois connu (voir §
  précédente).
- Grep systématiquement chaque `FUN_xxx`/`Ordinal_xx` rencontré dans `Blaze.cs` **avant** toute
  lecture d'assembleur — plusieurs fonctions « nouvelles » cette séance se sont révélées déjà faites
  (`FrameBegin`, `FrameEnd`, `CheatCodeProbe`, `RelocateModelBundle`, `RelocateAnimationBundle`,
  `PlayMusicTrack`, `DecodeResourceChunk`...). Ça a fait gagner énormément de temps une fois adopté
  systématiquement.

**Stratégie possible pour demain** : ces deux fonctions sont plus grandes que tout ce qui a été fait
aujourd'hui individuellement (361 et 494 lignes contre 110–120 pour `FUN_00536ba0`/`FUN_005126c0`).
Envisager de les découper en sous-unités lisibles (comme `FUN_00536270`'s boucle de rendu par membre
du groupe) et de déléguer les portions les plus denses à un fork si un piège d'arité/désassemblage
apparaît — ça a bien fonctionné pour `FUN_00536ba0` cette séance (traçage mécanique du delta ESP,
vérifié à la main ensuite sur un échantillon).

---

## Pièges et repères trouvés aujourd'hui (à garder en tête)

- **Pseudo-C habillée SEH** (`puStack_NN`, adresses de continuation littérales genre `0x5126f1`) :
  artefact de décompilation pour les fonctions compilées `/EHa`, **rend l'ordre des arguments
  trompeur à la lecture**. L'assembleur brut n'a aucune gestion d'exception réelle — désassembler
  directement plutôt que de deviner l'ordre depuis la pseudo-C.
- **Nettoyage de pile différé après un `CALL` cdecl** : plusieurs instructions peuvent s'exécuter
  entre le `CALL` et son `ADD ESP` de nettoyage. Calculer le delta ESP instruction par instruction,
  ne jamais supposer un cadre stable après le prologue pour toute la fonction.
- **Un `PUSH` entre l'écriture d'un champ de structure et le `LEA` qui calcule l'adresse de cette
  structure décale tout de la taille du `PUSH`** — piège dans lequel je suis moi-même tombé en
  retraçant `FUN_005126c0` avant de me corriger : un champ qui semblait « avant » la structure était
  en fait son premier champ, une fois le `PUSH` intercalé correctement pris en compte.
- **Deux appels stdcall vers `winsys3d.dll` n'ont aucun `ADD ESP` du tout** (l'appelé dépile
  lui-même) — à ne pas confondre avec le nettoyage différé cdecl ci-dessus.
- **Un symbole `PTR_xxx` de la pseudo-C Ghidra mérite d'être résolu comme un `DAT_`** : lire les
  octets réels à cette adresse (`at Legacy/Blaze.exe <va>`) peut révéler qu'il pointe simplement vers
  quelque chose déjà nommé côté C#.
- **Recette pour retrouver un ordinal DLL sans nom dans un re-run Ghidra `-noanalysis`** : les
  adresses de thunk brutes (`FF 25 xx xx xx xx`, un `JMP [IAT]`) ne portent pas de nom dans un projet
  minimal. Faire correspondre l'ORDRE des sites d'appel dans le `Blaze.c` déjà analysé (qui, lui,
  nomme déjà `Ordinal_NN`) avec l'ordre des `CALL` dans l'assembleur brut résout la correspondance
  sans re-analyse complète.
- Le binaire est bien dans le dépôt (`Legacy/Blaze.exe`) — la section « blocage systémique » de
  `CLAUDE.md` est obsolète sur ce point précis ; `Documentation/DataSymbols.md`/l'outil `at` peuvent
  résoudre des adresses `.data` qui semblaient auparavant hors de portée.

---

## Comment reprendre demain

1. **Lire `REVERSE_STATUS.md`**, dernières sections datées 2026-08-16 (celles à partir de « root
   cause found and fixed » jusqu'à la fin) pour le raisonnement complet derrière chaque point du
   résumé ci-dessus.
2. Build : `dotnet build OpenForesia.sln` — marche tel quel avec le `dotnet` du PATH sur cette
   machine (SDK x86 9.0.317, `C:\Program Files (x86)\dotnet`). Si `dotnet` n'est pas trouvé dans une
   nouvelle fenêtre PowerShell/Bash, faire `$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"`
   d'abord (PATH ne persiste pas entre les appels d'outil).
3. Test : lire `test-protocol.md` avant tout lancement, piloter via
   `OpenForesia.Tools\playtest.ps1`. Pour un test rapide de non-régression :
   ```powershell
   OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 15 -Shot -WorkingDir OpenForesia -Build
   ```
   Nettoyer `OpenForesia/save/TEST` avant/après si le test touche la sauvegarde.
4. Pour désassembler une fonction dense avant de la transcrire (recette utilisée plusieurs fois
   cette séance, sur `FUN_0051c120`/`FUN_00536ba0`/`FUN_005126c0`) : éditer
   `C:\Users\Vincent\ghidra_scripts\ForceDecompileConfirmDialog.java`, changer `TARGETS` pour
   l'adresse voulue, puis :
   ```powershell
   $gh = "D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
   & $gh "D:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
         -noanalysis -readOnly -scriptPath "C:\Users\Vincent\ghidra_scripts" `
         -postScript ForceDecompileConfirmDialog.java
   ```
   Sort `.c`/`.asm` dans `Legacy/Raw/Blaze/`. **Le script pointe actuellement sur `0x005126c0`** —
   à retargeter avant réutilisation. Non commité (comme les autres scripts Ghidra de ce projet).
5. Lire un octet/dword précis du binaire sans Ghidra :
   ```powershell
   dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe <adresse_hex> <octets>
   ```
6. **Ne pas commit** — seul l'utilisateur commit.

---

## Contexte accumulé

| fichier | contenu |
|---|---|
| `OpenForesia/src/Blaze/REVERSE_STATUS.md` | le journal détaillé, sections datées — tout le raisonnement de la séance y est |
| `Documentation/DataSymbols.md` | valeurs relevées dans le binaire, par VA Ghidra |
| `Documentation/DLL/winsys3d.md` | libgpu / libgte / registres GTE |
| `legacy-bug.md` | bugs du jeu de 1998, à vérifier avant de « corriger » une bizarrerie |
| `test-protocol.md` | comment exercer le jeu — toujours le lire avant un lancement |
| `Analyse/Comportement Legacy/Selection des personnages.md` | comportement exact du jeu officiel, écran par écran — référence utilisée pour confirmer plusieurs correctifs |
| `Legacy/Raw/Blaze/*.asm`/`.c` | désassemblages bruts déjà exportés cette séance (`FUN_0051c120`, `FUN_00536ba0`, `FUN_005126c0`) — réutilisables tels quels |

Deux inventaires utiles dans `Blaze.cs` :

```bash
grep -n "TODO(REVERSE)" OpenForesia/src/Blaze/Blaze.cs        # ce qui reste à établir
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # ce qui lève à l'exécution — les vrais murs, chacun nommé précisément
```
