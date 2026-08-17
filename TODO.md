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

## Fait en fin de journée (voir `REVERSE_STATUS.md`, section « suite »)

- **`FUN_00535500` → `UploadPartyIconTextures`** : téléverse les dix vignettes (palette 16 couleurs +
  image 24x24 en 4 bpp) de chacun des quatre membres. Il fallait le désassemblage brut — même piège
  du `PUSH` avant les écritures de champs que dans `FUN_005126c0`. Le chemin de mise au point va
  maintenant jusqu'à l'**état de jeu 2**.
- **`FUN_00538780` → `GetEntityStatBlock`** : quatre lignes, choisit entre le bloc d'un membre du
  groupe et la table compacte des autres entités (`DAT_005ea560`, 32 entrées de 0x120).
- **L'arène `0x5e9000`–`0x5e9a00` est posée** (l'ancienne étape 1 ci-dessous). Elle a mis au jour
  quatre défauts d'un coup : un symbole déclaré **deux fois** (`s_EntitySlotTable_005e9180` /
  `s_SlotArea_005e9180` — l'initialisation des emplacements manette n'atteignait pas son lecteur) et
  trois bornes de blocs trop larges, dont `AssetTableCapacity` qui vaut **16** et non 24.

Non-régression vérifiée sur les trois chemins exerçables après chaque étape.

---

## La suite, dans l'ordre

### 1. `FUN_00536ea0` — le chemin normal de l'état de jeu 1

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

### ~~2. `FUN_00537da0`~~ — **fait**, confirmé à l'écran

La carte du monde s'affiche, le modèle du groupe se tient dessus, et le curseur passe bien de
« L'auberge » à « La mine abandonnée » en faisant glisser la caméra. La borne suivante sur ce chemin
est l'**état de jeu 3** (`thunk_FUN_00516020`). Détail dans `REVERSE_STATUS.md`.

<details><summary>notes de préparation, conservées</summary>

`Blaze.c` 83427–83782 (356 lignes). C'est là que débouche maintenant le chemin de mise au point. Son
autre moitié, `FUN_00542160`, existe déjà (`LoadMenuResources`), et sa seule dépendance manquante
(`FUN_00538780`) est faite : **plus aucun blocage indirect**.

Ce qu'il fait : 32 particules animées initialisées au hasard (bloc `DAT_005de824`, 32 entrées de
0x9c), une caméra qui glisse par quarts vers la destination visée, un masque de destinations
débloquées construit bit à bit depuis `DAT_005e901e`, le modèle 3D animé du groupe, les icônes de
destination, puis un panneau d'interface. Sortie sur START ou CARRÉ : pose `DAT_005de528`, remet
`DAT_00566c84`/`88` à 0xff, nettoie quatre des huit banques et les paires à `0x5e9320`, et — si la
zone vaut 0 et que le groupe n'est pas vide — remet à zéro quelques champs par membre. C'est
l'appelant qui pose l'état de jeu 3.

Tables `.data` à relever avant de commencer (aucune n'est encore déclarée) : `DAT_0056d240`
(positions des icônes), `DAT_0056d270`/`272` (caméra par zone, pas de 4), `DAT_0056d2a0`
(zone → index de menu), `DAT_0056d2b0` (index → zone), `DAT_0056c978` (un descripteur d'interface de
plus — vérifier d'abord s'il n'est pas voisin d'un `0x8c8` déjà connu), `DAT_00604928` et
`DAT_006008d0..fc` (le bloc de caméra que `FUN_0051c300` consomme).

Le bloc de dessin sur pile est **le même à sept champs** que celui de `RunDebugPartySelect` et de
`FUN_00536ea0` — s'en inspirer directement.

</details>

### 3. Plus loin

- **L'état de jeu 3 (`thunk_FUN_00516020`)** — la borne où s'arrête maintenant le chemin de mise au
  point, et où `FUN_00536ea0` débouchera aussi. C'est donc le prochain candidat naturel.
- `FUN_00537a20`, la branche `DAT_005de51c != 0` de `RunGameState1Entry`, jamais lue.
- Voir les icônes de destination séparément : avec une progression nulle, les deux seules dessinées
  ont la même position dans la table. Il faut une sauvegarde avancée ou forcer `DAT_005e901e`.
- Ramener dans l'arène les scalaires isolés de `0x5e97c0..0x5e9824` et `0x5e9960` — sans urgence,
  aucun bloc ne les recouvre.

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
