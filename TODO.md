# TODO

Point d'entrée entre deux sessions. Remis à plat le 2026-08-17 en fin de journée — l'historique
détaillé complet est dans `REVERSE_STATUS.md` (section datée 2026-08-17), ce fichier ne garde qu'un
résumé et surtout la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

---

## Ce qui a été fait aujourd'hui (résumé — détail complet dans `REVERSE_STATUS.md`)

1. **`FUN_00536270` → `RunDebugPartySelect`, transcrite et confirmée à l'écran** (les deux moitiés :
   navigation/ajout/retrait, puis l'initialisation des quatre personnages et le passage à l'état 2).
   Ce n'est pas un écran du jeu vendu : c'est un outil de développeur qui compose un groupe en deux
   clics, avec 9000 PV et 200 PM par personnage.
2. **La clé qui l'ouvre est le mot « Gurkha »** — `DAT_00564a40`, lu dans le binaire. `CheatCodeProbe`
   file cette séquence sur chaque caractère **dessiné** (pas saisi), donc afficher ce mot quelque
   part — nommer un personnage ainsi — met `DAT_005633a8` à 0 et allume le mode développeur.
3. Quatre corrections de fond entraînées par le point 2 :
   - `DAT_005633a8` était déclaré **deux fois** (lectures d'un côté, écriture de l'autre) et valait 0
     par défaut au lieu de **1** — donc le jeu prenait le chemin de mise au point. Fusionné en
     `s_DebugModeLatch_005633a8`.
   - `CheatCodeProbe` avait un `return` que l'original n'a pas.
   - Deux pas de manette encore à `* 4` au lieu de `* 0x10` (`PollInputAndAudio`,
     `RunGameState1Entry`), les deux confirmés par le code machine.
   - `SetClassHighlightColour` → **`SetClassBaseHpMp`** : `+0x148`/`+0x14a` sont les PV et PM de
     départ, pas une teinte.
4. **Un vrai bug trouvé en exécutant `LoadPartyMemberResources` pour la première fois** : un
   déréférencement manquant sur la table de textures (`*(int*)(dir + 0x84)`, pas `dir + 0x84`).
   Cette fonction avait été écrite hier mais n'était atteignable depuis nulle part ; ouvrir un
   chemin d'appel a valu autant qu'une relecture.
5. `FUN_005352e0` (`CopyDwords`) : hypothèse confirmée par le désassemblage, `TODO(REVERSE)` levé.
6. **Huit « globales » de plus étaient en fait des champs de descripteur** (`0x56b558` et `0x56be20`,
   deux instances contiguës du type déjà connu) — septième à quatorzième instances du défaut
   d'aliasing corrigé six fois hier. Recâblées ; huit avertissements CS0414 disparaissent avec.
7. `FUN_00536ea0` (chemin B) : **intégralement analysée, pas encore écrite**. L'analyse est dans
   `REVERSE_STATUS.md` — s'en servir, ne pas la refaire.

Build vert, non-régression vérifiée à chaque étape (382.3 primitives par image sur l'écran de
sélection des personnages, capture identique à la référence).

---

## La suite, dans l'ordre

### 1. Poser le bloc de fond `0x5e9060`–`0x5e99c0` — **préalable à tout le reste**

C'est le seul obstacle réel devant `FUN_00536ea0`. Son bloc de restauration d'état du monde recopie
huit banques de 0x40 octets vers `DAT_005e96e0`, `005e9780`, `005e9060`, `005e9840`, `005e9720`,
`005e92c0`, `005e9980`, `005e9360`. Dans l'original c'est **une seule région contiguë** ; ici elle
est éparpillée en allocations indépendantes (`s_PanelSelection_005e9060`, `s_SlotArea_005e9180`,
`s_PartyAnimationResult_005e9680`, `s_PartyModelResult_005e9800`…), dont quatre banques tombent à
l'intérieur et quatre à cheval.

Déclarer huit nouveaux blocs isolés reproduirait exactement le défaut d'aliasing corrigé quatorze
fois à ce jour. La bonne étape est **un seul bloc de fond, et les symboles existants réexprimés
comme des décalages dedans** — le modèle est déjà là :
`s_SlotFlags_005e93c0 => s_SlotArea_005e9180 + 0x240`.

Remaniement mesuré, à faire **avant** `FUN_00536ea0` et pas pendant. Reconstruire souvent ; si le
nombre d'erreurs explose, revenir en arrière et réexaminer le découpage plutôt que de rustiner.

### 2. `FUN_00536ea0` — le chemin normal de l'état de jeu 1

`Blaze.c` 82764–83257 (494 lignes). C'est **lui** que le jeu prend (le verrou vaut 1). Un écran de
chargement de sauvegarde : liste des fichiers, sélection numérotée de 1 à 4, puis chargement et
passage à l'état de jeu 3.

Toutes ses dépendances existent déjà côté C#, sauf `FUN_00547960` (le `sprintf` du CRT) — et son
format est maintenant connu : `DAT_005630c4` vaut `"%d"`, donc le site d'appel se transcrit en un
formatage décimal, pas en moteur variadique. **Ne pas généraliser sprintf.**

L'analyse complète (structure du prologue, disposition des lignes d'affichage de 0x16 octets, la
machine à états sur `local_38`, le bloc de chargement, la sortie vers l'état 3) est dans
`REVERSE_STATUS.md`, section 2026-08-17. Les deux descripteurs qu'il dessine sont déjà déclarés
(`s_LoadScreenDescriptors_0056b558`).

Sa fin est **presque identique** à celle de `RunDebugPartySelect` (mêmes drapeaux d'emplacement, même
garde-fou de dépassement, même boucle « BUFFER OVER ») à l'état de jeu final près — 3 au lieu de 2.
Regarder `RunDebugPartySelect` avant d'écrire cette partie.

### 3. `FUN_00535500` — la borne où s'arrête maintenant le chemin A

Trente-cinq lignes seulement, mais **il faut le désassemblage brut** : la pseudo-C passe `&local_c`
au premier `Ordinal_674` alors que le rectangle est écrit dans `local_8`/`local_6`/`local_4`/`local_2`
(décalage de 4), et le second appel reçoit un `&stack0xffffffe8` que rien ne remplit visiblement.
Deux rectangles, un seul lisible. Recette Ghidra plus bas.

### 4. Plus loin

- `FUN_00537a20`, la branche `DAT_005de51c != 0` de `RunGameState1Entry`, jamais lue.
- L'état de jeu 2 (`FUN_00542160`/`FUN_00537da0`), où `RunDebugPartySelect` débouche maintenant.
- L'état de jeu 3 (`thunk_FUN_00516020`), où `FUN_00536ea0` débouchera.

---

## Pièges et repères (cumulés — ceux d'hier restent valables)

**Nouveaux aujourd'hui :**

- **Une fonction transcrite avec soin depuis l'assembleur peut contenir un défaut d'une ligne, et
  seule l'exécution le montre.** `LoadPartyMemberResources` avait été relue et documentée ; elle a
  fauté à la première seconde d'exécution. Ouvrir un chemin d'appel vaut autant qu'une relecture.
- **Un plantage parfaitement reproductible qui n'apparaît qu'à partir du deuxième lancement** vient
  probablement de ce que le premier a laissé sur le disque, pas du code. Nettoyer
  `OpenForesia/save/TEST` entre deux essais (détaillé dans `test-protocol.md`).
- **Un avertissement CS0414 « assigné mais jamais utilisé » sur une globale reconstruite est un
  indice d'aliasing**, pas du bruit : huit d'entre eux désignaient exactement les huit champs de
  descripteur mal déclarés.
- **Un symbole `DAT_` dont la valeur `.data` au chargement n'est pas zéro doit être initialisé.**
  `DAT_005633a8` valait 1 dans le binaire et 0 en C# — assez pour inverser une branche entière.
- Le PATH d'une fenêtre PowerShell n'est pas celui d'une fenêtre Bash. `dotnet` peut être
  introuvable d'un côté et présent de l'autre dans la même session.
- **Les heredocs de bash restent inutilisables ici**, et `Get-Content`/`Add-Content` de PowerShell 5.1
  double-encodent l'UTF-8 sans BOM. Pour ajouter du texte accentué à un fichier :
  `[System.IO.File]::ReadAllText($src, [Text.Encoding]::UTF8)` puis
  `[System.IO.File]::AppendAllText($dst, $text, (New-Object Text.UTF8Encoding($false)))`.

**Toujours valables (2026-08-16) :**

- Pseudo-C habillée SEH (`puStack_NN`, adresses de continuation littérales) : l'ordre des arguments
  y est trompeur, désassembler plutôt que deviner.
- Nettoyage de pile différé après un `CALL` cdecl : calculer le delta ESP instruction par instruction.
- Un `PUSH` entre l'écriture d'un champ et le `LEA` qui calcule l'adresse de la structure décale tout.
- Les appels stdcall vers `winsys3d.dll` n'ont aucun `ADD ESP`.
- Un `PTR_xxx` mérite d'être résolu comme un `DAT_` : lire les octets réels peut révéler qu'il pointe
  vers quelque chose déjà nommé.
- **Grep systématiquement chaque `FUN_xxx`/`Ordinal_xx` dans `Blaze.cs` avant toute lecture
  d'assembleur.** Pour `FUN_00536ea0`, ça a montré d'un coup que 30 dépendances sur 31 existaient.
- Le binaire est dans le dépôt (`Legacy/Blaze.exe`) — la section « blocage systémique » de
  `CLAUDE.md` est obsolète sur ce point.

---

## Comment reprendre

1. **Lire `REVERSE_STATUS.md`**, section datée 2026-08-17, en entier : elle contient l'analyse de
   `FUN_00536ea0` qu'il serait coûteux de refaire.
2. Build : `dotnet build OpenForesia.sln`. Si `dotnet` n'est pas trouvé dans la fenêtre courante :
   `$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"` (le PATH ne persiste pas entre les appels
   d'outil, et diffère entre PowerShell et Bash).
3. Test : lire `test-protocol.md` avant tout lancement, piloter via `OpenForesia.Tools\playtest.ps1`.
   Non-régression rapide :
   ```powershell
   Remove-Item 'OpenForesia\save\TEST' -Force -ErrorAction SilentlyContinue
   OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 15 -Shot -WorkingDir OpenForesia -Build
   ```
   Attendu : aucun plantage, 382.3 primitives par image. Pour l'écran de mise au point, voir la
   section dédiée de `test-protocol.md`.
4. Désassembler une fonction dense avant de la transcrire : éditer
   `C:\Users\Vincent\ghidra_scripts\ForceDecompileConfirmDialog.java`, changer `TARGETS` pour
   l'adresse voulue, puis :
   ```powershell
   $gh = "D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
   & $gh "D:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
         -noanalysis -readOnly -scriptPath "C:\Users\Vincent\ghidra_scripts" `
         -postScript ForceDecompileConfirmDialog.java
   ```
   Sort `.c`/`.asm` dans `Legacy/Raw/Blaze/`. **Le script pointe actuellement sur `0x005126c0`** — à
   retargeter (`0x00535500` est le prochain candidat). Non commité.
5. Lire un octet/dword précis du binaire sans Ghidra :
   ```powershell
   dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe <adresse_hex> <octets>
   ```
6. **Ne pas commit** — seul l'utilisateur commit.

---

## Contexte accumulé

| fichier | contenu |
|---|---|
| `OpenForesia/src/Blaze/REVERSE_STATUS.md` | le journal détaillé, sections datées — tout le raisonnement y est |
| `Documentation/DataSymbols.md` | valeurs relevées dans le binaire, par VA Ghidra |
| `Documentation/DLL/winsys3d.md` | libgpu / libgte / registres GTE |
| `legacy-bug.md` | bugs du jeu de 1998, à vérifier avant de « corriger » une bizarrerie |
| `test-protocol.md` | comment exercer le jeu — toujours le lire avant un lancement |
| `Analyse/Comportement Legacy/Selection des personnages.md` | comportement exact du jeu officiel, écran par écran |
| `Legacy/Raw/Blaze/*.asm`/`.c` | désassemblages bruts déjà exportés — réutilisables tels quels |

Deux inventaires utiles dans `Blaze.cs` :

```bash
grep -n "TODO(REVERSE)" OpenForesia/src/Blaze/Blaze.cs        # ce qui reste à établir
grep -n "NotImplementedException" OpenForesia/src/Blaze/Blaze.cs   # ce qui lève à l'exécution
```
