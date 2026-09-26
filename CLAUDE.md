# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this project is

OpenForesia is a **reverse-engineering reconstruction** of *Blaze & Blade* (`Blaze.exe`, a 1998
32-bit native Windows game) into readable C#, built up function by function along the call graph
from Ghidra pseudocode.

**Since 2026-09-26 the project is in « legacy ++ » mode**: the goal is a playable, self-contained,
turnkey game — installed from the player's own ISO, with no CD, no 1998 DLL and no prerequisite.
Fixing defects of the 1998 game and removing its dependencies on the 1998 environment are now part
of the work, **under the rules of `Strategy.md`, section « Mode legacy ++ »**: transcribe faithfully
first, deviate second, never in the same gesture; declare every deviation in place
(`// Écart legacy ++ (LB-NNN) : …`) and in `legacy-bug.md`; put it behind a switch when the player
sees or experiences the game differently; apply a defect fix only when Vincent asks for or approves
it. The game's data is never in the repository or in a release.

> Until 2026-09-26 the rule was: « There is no product to ship: the deliverable is a faithful,
> understandable C# transcription of Ghidra pseudocode », and original defects were reproduced, not
> repaired. `Strategy.md` keeps the history of the superseded rules.

`Documentation/Methode/Prompt.md` and `Strategy.md` (both in French) are the authoritative methodology
documents — read them before doing reconstruction work. The summary below is not a substitute.

## Interdiction absolue : aucune commande git

**L'agent n'a pas le droit d'exécuter une commande `git` qui écrit.** Le seul responsable du
dépôt, c'est l'utilisateur (Vincent) : lui seul fait `git add`, `git commit`, `git push`,
`git checkout`, `git reset`, `git stash`, `git merge`, `git rebase`, `git tag`, `git branch`.
Cela vaut aussi quand la commande semble anodine, quand un travail est « fini », quand une séance
se termine, ou quand l'historique paraît sale : **on ne commite pas, on prépare et on laisse la
main.**

Ce qui reste permis, en **lecture seule** : `git status`, `git log`, `git diff`, `git show`,
`git blame` — pour se repérer, jamais pour modifier quoi que ce soit.

En fin de travail : décrire ce qui a changé, mettre à jour `HISTORY.md`, et laisser l'utilisateur
commiter lui-même.

## Build and run

**Whether the `dotnet` on PATH carries an SDK is machine-dependent — check before assuming.** On
the current machine, `C:\Program Files (x86)\dotnet` is on PATH and already ships an x86 SDK
(9.0.317 and 10.0.401; the projects target `net10.0` since 2026-09-24), so a plain `dotnet build OpenForesia.sln` / `dotnet run --project OpenForesia` works
with no `DOTNET_ROOT` juggling. An earlier machine used for this project had only an x64
*runtime* on PATH, with the actual SDK installed separately under the user profile; on a machine
like that, every `dotnet` invocation needs to point at the real SDK first:

```powershell
$env:DOTNET_ROOT = "<path to the real SDK>"
$env:PATH = "<path to the real SDK>;$env:PATH"
dotnet build OpenForesia.sln
```

Verify with `dotnet --list-sdks`: an empty/failing result means PATH is runtime-only and the SDK
needs to be located and pointed at explicitly. `playtest.ps1`'s `-Build` switch already detects
this (tries a plain build first, falls back to a known SDK path if PATH has none) — extend that
fallback path if it's stale rather than hardcoding a new one elsewhere.

Two `NETSDK1189` warnings about `Prefer32Bit` are expected and harmless. Verification of the
transcribed logic is `dotnet build` plus reading the reconstruction against `Blaze.c`, and then the
screen (see "Running the game" below) — there is no behavioural test suite and there is not meant
to be one.

`OpenForesia.Tests` (`dotnet test OpenForesia.Tests`) is the one exception, and it tests **the
memory map, not behaviour**: six assertions that read the *sources* looking for the aliasing defect
— two names for one address of `.data`, modelled as two separate allocations. **Some of them are
red on purpose.** A red assertion there names a real defect the repo carries and has chosen not to
fix yet (fixing changes behaviour); the header of `MemoryLayoutTests.cs` says which and why. Do not
"make the tests pass" — read what they name, and fix it only as a deliberate change.

`dotnet run --project OpenForesia` can fail from the shell on machines where the SDK on PATH only
carries an x64 runtime while the game is x86 — in that case launch
`OpenForesia/bin/Debug/net10.0/OpenForesia.exe` directly, or use `playtest.ps1` below, which handles
it either way.

**Neither the SDK nor `node` is on the agent's PATH**, even when both are installed — same trap,
twice. `node` lives on `D:\Logiciels\nodejs` and is needed by the `openspec` CLI. Prefix once per
shell:

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;D:\Logiciels\nodejs;$env:PATH"
```

**Smart App Control — turned OFF by the user on 2026-08-25, so this no longer bites.** The registry
key below now reads `VerifiedAndReputablePolicyState = 0`, `dotnet run --project OpenForesia.Tools`
works straight out of `bin\Debug\`, and the Release-publish workaround is no longer needed. The user
disabled it because SAC was refusing to load one of the game's own DLLs — not on our advice, and it
cannot be re-enabled. **Check the key before assuming either state**, and keep the recipe below: it
is the answer if this project is ever cloned onto a machine where SAC is enforced.

Historically, on this machine with SAC in enforced mode
(`HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy` → `VerifiedAndReputablePolicyState = 1`), it
periodically refused to load the freshly-built, unsigned `OpenForesia.Tools.dll` out of `bin\Debug\`:

```
System.IO.FileLoadException: ... Une stratégie de contrôle d'application a bloqué ce fichier. (0x800711C7)
```

It is **not** Mark-of-the-Web — the file carries no `Zone.Identifier` stream, so `Unblock-File`
does nothing. The working answer is a **Release publish**, which SAC accepts:

```powershell
dotnet publish OpenForesia.Tools -c Release -o OpenForesia.Tools\bin\publish
OpenForesia.Tools\bin\publish\OpenForesia.Tools.exe disasm Legacy/Binaries/Blaze.exe 0x00516a7d 12
```

On such a machine, do this rather than reaching for the security setting: **turning Smart App
Control off is irreversible** — Microsoft does not allow re-enabling it without reinstalling
Windows. Never flip it without the user asking for that specific change, in those terms.

If even the publish is refused, the fallback that needs no tooling at all is reading the binary's
bytes directly — the `.text` file offset is `VA - 0x400C00`:

```bash
od -An -tx1 -j $((0x115E7D)) -N 24 Legacy/Binaries/Blaze.exe   # VA 0x00516a7d
```

## The game's data — no CD since 2026-09-26

The game no longer looks for a CD (`openspec/changes/archive/2026-09-26-install-from-iso`, legacy ++). It moves into
its own executable's folder at startup and resolves everything there, relatively: the data
(`French\`, `Snd\`, `Music\`), `save\` and `OpenForesia.cfg`. Before opening its window,
`Host/GameDataCheck.cs` checks the 133 files listed in `OpenForesia.Installer/Editions/editions.json`
and stops on a message if one is missing. **The data is never in the repository** — the project has
no rights to it.

In development, extract it once from the ISO into the game's build output (two seconds; `bin` is
ignored by git and a rebuild does not remove it):

```powershell
OpenForesia.Installer\bin\Debug\net10.0-windows\OpenForesia-Setup.exe --extraire-donnees <iso> OpenForesia\bin\Debug\net10.0
```

The reference image is `D:\Open Foresia Project\Abandonware executable\jeu-01974-Legend_of_Foresia-pcwin\Legend of Forezia.ISO`.
Its directory table lies: `CD.IDX` declares 640 MB (the copy protection, LB-053), and the movies
`OPENING.AVI`/`ANI01.AVI` declare sizes that run to the last sector — the installer cuts them at
their RIFF size. `FRENCH2\` is a byte-identical duplicate of `FRENCH\`.

**The player's installer** is `OpenForesia.Installer` (`OpenForesia-Setup.exe`): pick the ISO, pick
a folder, done — shortcuts, an "installed apps" entry and `Uninstall.exe` included. Double-clicked
from its Debug build, it installs the repo's Debug game. **The release** is built by
`OpenForesia.Tools\publish-release.ps1`: the game published self-contained x86 **and trimmed**
(108 → 21 MB on disk), published as a single `OpenForesia.exe` (no DLL, no `createdump.exe`) and zipped inside a trimmed single-file installer (**~15.5 MB**), with a check that
no game data slipped in. Trim warnings are errors there: a trim warning is code the trimmer may break
silently. Hence two rules — **the installer has no WinForms** (it does not trim; its window is native
Win32 in `Interface/`, its COM calls `[GeneratedComInterface]`), and **JSON goes through a generated
`JsonSerializerContext`** in both projects. Output:
`OpenForesia.Installer\bin\Release\publie\OpenForesia-Setup.exe`.

## Running the game — always via `test-protocol.md`

**Before exercising the game in any way — launching it, reaching a screen, reproducing a crash,
capturing a screenshot — read `Documentation/Tests/test-protocol.md` and drive the run with
`OpenForesia.Tools/playtest.ps1`.** This is not optional and does not need to be asked for.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 25 -Shot   # character select, with a capture
```

The game's keyboard goes through DirectInput and its menu cannot be driven by timed keystrokes, so
every ad-hoc launcher written from scratch has had to rediscover the same half-dozen traps.
`playtest.ps1` navigates the menu in closed loop, confirms the validation was seen, captures the
window and prints a verdict (crash with its stack, or primitives-per-frame). Read the capture it
writes to `scratchpad/`.

If a run needs something the script does not do, **extend the script and note it in
`test-protocol.md`** rather than working around it. Anything a test session learns about *how to
test* belongs in that file, the same way findings about the binary belong in `Documentation/`.

PE inspector. A bare path keeps the original export-table dump (ordinal → RVA → name); the two
verbs read the binary's data sections:

```powershell
dotnet run --project OpenForesia.Tools -- Legacy/Binaries/winsys3d.dll         # exports
dotnet run --project OpenForesia.Tools -- sections Legacy/Binaries/Blaze.exe           # VA ranges
dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005631f8 32   # bytes at a Ghidra VA
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724  # x86-32
```

Both verbs take a **Ghidra virtual address** (hex, `0x` optional); byte counts are decimal.

**Reading the game's own data — `bestiaire`.** A fourth verb reads not the binary but the game data
(installed next to the game, see "The game's data" below), by replaying the reconstructed load chain
(`SYSTEM.INI` → state → zone → sub-area) offline. Without a folder argument it reads
`OpenForesia\bin\Debug\net10.0`:

```powershell
dotnet run --project OpenForesia.Tools -- bestiaire --sortie Documentation\Reference\Bestiaire
```

It writes the enemy cartography — species, sheets (name, HP, MP, characteristics), formations, loot
tables, the per-sub-area item catalogue with **each item's 24×24 icon** (`vignettes.csv` — kept
out of `objets.csv`, whose `octets` must stay the last field), the reverse index (item → what drops
it, where, at what chance), and **the chests** (`coffres.csv`). A chest draws from the *same* `+0xb4` table as a
monster, at a row its placement record carries — so 112 items exist only in chests. Identifying one
requires resolving the sub-area's own entity jump table out of `Blaze.exe`: **a subtype number
means nothing on its own**, the 0x0056adc8 table is reloaded per sub-area.

It also writes **the spell sheets** (`sorts.csv`) — the `+0x9c` table of the state directory, the
one `CastSpell_0053A980` addresses as `famille * 4` then `indice * 0x30`. Eight families, of which
the first four are the schools: 29 / 24 / 20 / 7 sheets, and the first three counts match
`DAT_00564600` in the binary exactly, which is what validates reading each list's length from where
the next one starts. `census-spells.sh` turns that CSV into `Documentation/Etat/SortsAtteignables.md`.

`--regions` adds each directory's 60-pointer region map, which is the general tool for
**finding a new table in a resource blob**: the regions follow one another without gaps, so a
table's end is the next header pointer, and a region's first bytes usually say what it holds.

**Checking the Indeo 4 decoder — `indeo`.** `indeo OpenForesia\bin\Debug\net10.0\French\Teps.AVI` decodes every frame of a
film twice — through `ir41_32.ax` in `IF09` output (native YVU9; its RGB24 is dithered, useless as a
reference) and through `Host/Indeo4/`, the C# port of FFmpeg's decoder (LGPL 2.1+, read its `LISEZMOI.md`) —
and names the first differing byte. The port is byte-identical on all four films (8 849 frames).
`Indeo4Tables.cs` is generated by `OpenForesia.Tools/gen-indeo4-tables.js`; never edit it by hand.
`--rgb` measures what the game actually shows — `Host/Yvu9Rgb.cs` against Intel's dithered RGB24 —
and must stay within the dither's amplitude (max 4-5). `--reference-seule` runs two instances of
the Intel codec against each other. See `Documentation/DLL/ir41_32.md`.

**Simulating character progression — `montee`.** A fifth verb reads neither the binary nor the CD.
It opens a window (`OpenForesia.Tools/MonteeDeNiveau/`, or double-click `tests\montee.bat`) where
you compose a character the way the creation screen does — class, a pool of 15 + 3d6, seven lines
floored at their class value and capped at 99 — pick a target level and a seed, and it replays
`FUN_005391f0` one level at a time on a real 0x2000-byte character record:

```powershell
dotnet run --project OpenForesia.Tools -- montee
```

Out come nine curves with their ceilings dashed in, a per-level table of what each level-up changed,
and a per-characteristic summary (start, final, ceiling, the level the ceiling was hit, how often it
moved). **The same seed replays the same game exactly** — that is what makes the per-level table
meaningful. Two things the offline run cannot have, both documented on `Simulateur`: the starting
equipment (its bonuses land in `+0x120`, never in the `+0x280`/`+0x2a0` pair the progression reads)
and spell learning (the spell directory is on the CD) — neither changes a curve, though the second
shifts the RNG *stream* for a casting class. Read `Documentation/Reference/MonteeDeNiveau.md` for
what the numbers mean.

`OpenForesia.Tools\build-bestiaire-page.ps1` then joins the CSVs into
`Documentation/Reference/Bestiaire/bestiaire.html`, a standalone browsable page. That file is
**generated** — edit `bestiaire-page.template.html` instead. See
`Documentation/Reference/Bestiaire/LISEZMOI.md`.

**A `.ps1` added to this repo needs a UTF-8 BOM.** Windows PowerShell 5.1 reads a BOM-less file as
ANSI, and the first accented character or em-dash in a string breaks parsing.

`at` prints the resolved file offset and section alongside hex, C-string and dword renderings. An
address landing in `.data`'s uninitialised tail is reported as such — that means "zero at load,
written only by code", which is an answer, not an error.

`disasm` prints x86-32 instructions with internal jump targets labelled `LAB_…`. **Il marche aussi sur les cinq DLL d'aide** — la base de chargement sort de l'`ImageBase` du PE, rien n'est code en dur pour `Blaze.exe` : `disasm Legacy/Binaries/wsyskern.dll 0x100044a0 112` sort les corps que Ghidra n'a pas decompiles. Garder les releves sous `Legacy/Raw/<Dll>/`. **This is what
makes a body Ghidra never decompiled reachable** — see "When `Blaze.c` doesn't have the answer"
below. Keep any dump you rely on under `Legacy/Raw/Blaze/`.

## Layout

**The repository root holds exactly two files** — `CLAUDE.md` and `OpenForesia.sln`. Everything
else lives under one of the directories below. Reorganised 2026-08-21; before that, the
decompilations, the DLL binaries and eleven `.md` files were all loose at the root.

```
Legacy/          tout ce qui vient de 1998, rien d'ecrit par nous
  Binaries/      Blaze.exe, Blaze.cfg, et les six DLL d'aide (dont ir41_32.ax, le codec des films)
  Decompiled/    Blaze.c et les cinq .c de DLL — sortie Ghidra, LECTURE SEULE
  Raw/           relevés bruts par fonction (desassemblage, exports Ghidra)

Documentation/   tout ce que le projet a appris
  ARCHITECTURE.md
  DLL/           un fichier par DLL d'aide
  Ennemies/      Comportement.md — la carte des 166 gestionnaires de comportement
  Etat/          TODO.md, REVERSE_STATUS.md, HISTORY.md, NOTE_DEVELOPPEUR.md
  Methode/       Prompt.md, Strategy.md
  Reference/     DataSymbols.md, legacy-bug.md, MonteeDeNiveau.md, Sorts.md, OpenForesia.cfg.md
  Tests/         test-protocol.md, test-todo-later.md, et les analyses ponctuelles

tests/           les raccourcis de lancement a double-cliquer (banc.bat : la salle d’essai)
```

| Path | Role |
|---|---|
| `Legacy/Decompiled/Blaze.c` (98k lines) | Ghidra decompilation of `Blaze.exe`. **Source of truth. Never modify.** |
| `Legacy/Binaries/Blaze.exe` | The game binary itself (MD5 `46d90d1b4526f4a9a441f851c77179b3`). Same build as `Blaze.c`: image base `0x400000` + entry RVA `0x147D40` = `0x00547D40` = `entry`. |
| `Legacy/Decompiled/winsys3d.c`, `wsyskern.c`, `playst.c`, `3dhelp.c`, `gldhelp.c` | Ghidra decompilations of the five helper DLLs. **Secondary references, read-only.** |
| `Legacy/Binaries/*.dll` | The actual DLL binaries — this is what makes ordinal-only imports resolvable. |
| `Documentation/ARCHITECTURE.md` | **Read this before adding or moving code.** The directory map, the routing rule that decides where a function goes, and the state rule that protects against the aliasing defect. |
| `Documentation/Etat/REVERSE_STATUS.md` | **Read this first.** Call-graph coverage, per-function status, numbered `TODO(REVERSE)` items, current blocking point. |
| `Documentation/Etat/TODO.md` | The between-sessions entry point: current state and what to do next. |
| `Documentation/Etat/HISTORY.md` | **Update it at the end of every work session — see below.** One line per change, newest first. |
| `Documentation/Etat/NOTE_DEVELOPPEUR.md` | **Les traces humaines restées dans le binaire** — le mot à un collègue, l'écran d'outil laissé dans la version vendue, la faute de frappe partie en pressage. Aucune utilité technique ; y ajouter une entrée quand on en croise une. |
| `Documentation/Methode/Prompt.md`, `Strategy.md` | The authoritative methodology documents, both in French. |
| `Documentation/Reference/DataSymbols.md` | Values recovered from the binary's data sections, keyed by Ghidra VA. Also carries the **map of reconstructed symbols**: every VA-suffixed C# symbol, sorted by address, with its gap to the next and the file it lives in. |
| `Documentation/Ennemies/Comportement.md` | **La carte des comportements d'ennemi** : combien de gestionnaires la table `0x0056ad48` installe, dans quelle zone, lesquels sont écrits, et la recette pour le suivant. À lire avant de transcrire un gestionnaire d'entité de scène. |
| `Documentation/Etat/GestionnairesEntites.md` | **Le recensement des 1 430 gestionnaires d'entité atteignables**, croisé en trois états — ABSENT (à transcrire), ÉCRIT (branché, jamais appelé), EXERCÉ (la sonde l'a vu passer en jeu). **Généré** par `sh OpenForesia.Tools/census-entity-handlers.sh` ; ne pas éditer à la main. C’est la liste de courses et le tableau de bord. **Le regenerer apres chaque implementation** — et si le gestionnaire etait `implemente=non, teste=oui`, retirer d’abord ses lignes de `handlers-exerces.tsv` : ce « teste » ne prouvait que le plantage. |
| `Documentation/Etat/ObjetsAtteignables.md` | **Ce qu’utiliser un objet peut atteindre** : les 21 gestionnaires d’entité que les scripts des 2 370 fiches d’objet du CD font naître, triés par nombre d’emplois, plus les genres d’effet et les fiches de sort qu’ils désignent. **Généré** par `sh OpenForesia.Tools/census-item-effects.sh` — il déroule les scripts **hors ligne**, depuis `Bestiaire/objets.csv` ; aucune partie n’est jouée. Deux tableaux : les **21 gestionnaires** que les objets font naître, et les **607 objets** un par un — chacun avec `implemente` (statique) et `teste` (la sonde d’objets `objets-exerces.tsv`), la même lecture en deux colonnes que `GestionnairesEntites.md`. |
| `Documentation/Etat/SortsAtteignables.md` | **La feuille de route de la magie** : les 123 fiches de sort du CD, une par ligne, avec `implemente` (le gestionnaire d’entité que le sort fait naître existe) et `teste` (la sonde l’a vu partir en jeu) — la même lecture en deux colonnes que les deux documents ci-dessus. **Généré** par `sh OpenForesia.Tools/census-spells.sh` depuis `Bestiaire/sorts.csv` ; aucune partie n’est jouée. Un sort fait naître le sous-type `+0x10` de sa fiche par le **même mécanisme que les opcodes 9/10 du script d’objet**, et aucune fiche n’emploie un sous-type `>= 0xc0` : **un sort fait donc exactement la même chose partout**. Les quatre familles au-delà des écoles sont comptées à part — ce sont vraisemblablement les sorts des monstres, et leur étendue n’est pas établie. |
| `Documentation/Etat/DLLAtteignables.md` | **La feuille de route des cinq DLL d’aide de 1998** : pour chacune, ce que la reconstruction en appelle vraiment — les points d’entrée que `Interop/` importe, la **clôture** de leurs appels, et donc le devis pour s’en passer. **Généré** par `sh OpenForesia.Tools/census-dll-surface.sh` (+ `census-dll-surface.awk`) ; aucune partie n’est jouée. Il lit la **table d’exports directement dans les binaires**, ce qui résout les ordinaux que le commentaire de corps de Ghidra ne porte pas. Le chiffre qui compte : **17 832 lignes sur 135 623**, soit 13 % — le reste n’est atteint par rien. `playst.dll` (41 713 l.) n’est nommée par aucun binaire ; `3dhelp` et `gldhelp` n’exportent qu’une fonction chacune, tout le reste passant par la **vtable du device**, une arête que ce relevé ne suit pas. La colonne `implemente` exige la **forme canonique** `// Ghidra: winsys3d.c FUN_00a05060 @ 0x00a05060` : une mention en prose ne compte pas, et une règle plus lâche comptait 14 corps portés là où il y en avait 5. |
| `Documentation/Etat/DLLGlobales.md` | **Quels corps d'une DLL sont obligés de partir ensemble.** Là où `DLLAtteignables.md` dit *ce qu'il faut* transcrire, celui-ci dit *dans quel ordre c'est possible* : il soude les corps par deux relations — un appel vers une fonction **non exportée** (elle ne peut pas rester derrière) et une globale qu'un corps **écrit** pendant qu'un autre la touche (deux lecteurs ne se gênent pas) — puis sort les **composantes**. Une composante se porte entière ou pas du tout. **Généré** par `sh OpenForesia.Tools/census-dll-globals.sh <dll>` (+ `census-dll-globals.awk`). La classification lecture/écriture est **volontairement pessimiste** — `&DAT_x` hors accès indicé compte comme écriture possible, parce qu'un alias est indécidable dans la ligne —, donc les composantes sont un **majorant** : détacher un corps se vérifie à la main. Sur `winsys3d` : une masse de 425 corps (le chemin d'affichage, `DrawOTag` et `InitTrigTables`) et **treize composantes indépendantes** de 21 corps au total. |
| `Documentation/Etat/Zone<N>ParComplexite.md` | **Le tri d'une zone par complexité**, pour choisir l'ordre des lots : les gestionnaires à écrire rangés en cinq catégories — A déjà écrit ailleurs (copie LOOSE ou amorce d'une appelée écrite), B simple (≤ 100 instructions, sans table de sauts ni appelée inconnue), C moyen (≤ 250), D gros, E intelligence —, avec le nombre de **lectures** (formes distinctes) et le nom des bêtes. **Généré** par `sh OpenForesia.Tools/census-zone-complexity.sh <zone>` ; il relance `census-duplicates.sh` (mettre `TMP_DUP` sur un cache persistant, le premier désassemblage prend cinq minutes). Régénérer `GestionnairesEntites.md` d'abord. |
| `Documentation/Etat/CheminAffichage.md` | **Le dernier lot de `winsys3d`** : le chemin d'affichage, délimité et chiffré. Le périmètre est fermé par une graine — tout corps qui touche le tampon de commandes ou un mémo d'état — et sa fermeture descendante : **157 corps, 12 408 lignes, 70 formes, et UN SEUL `[DllImport]` dedans** (l'ordinal 701, `DrawOTag`). **Il est indivisible**, et pour une raison précise : à état inchangé un producteur **allonge l'enregistrement précédent** du paquet (`paquet[curseur - 1] += n`) et range ses sommets à la suite, donc deux moitiés ne peuvent pas coexister. **Généré** par `sh OpenForesia.Tools/census-display-path.sh` (+ `census-display-path.awk`). L'ordre de travail y est **par forme de squelette de contrôle**, pas par taille — la leçon de `FUN_004747F0`. Trois pistes plus courtes ont été ouvertes puis fermées par la mesure (`DAT_011be760`, le tampon seul, le rejeu seul) ; le document dit lesquelles, pour qu'on ne refasse pas le chemin. |
| `Documentation/Etat/handlers-exerces.tsv` | **La preuve accumulée** : un couple `(gestionnaire, zone, sous-zone)` par ligne, écrit par la sonde de `Screens/State03/Dispatch/CoverageProbes.cs` à la fermeture du jeu, en union avec ce qu'il portait déjà. Suivi par git ; il ne perd jamais rien. `OPENFORESIA_NO_PROBE=1` désarme la sonde. |
| `Documentation/Etat/objets-exerces.tsv` | **La jumelle de la précédente, sur les objets** : un couple `(clé de fiche, zone, sous-zone)` par ligne, écrit dès qu’un objet est employé en jeu. La **clé est l’hexa des seize premiers octets de la fiche**, pas son nom : les noms d’objet ne sont pas de l’ASCII. `census-item-effects.sh` la recalcule depuis `objets.csv` pour retrouver le nom lisible. Même garanties : union, écriture immédiate, suivi par git, `OPENFORESIA_NO_PROBE=1` la désarme. |
| `Documentation/Etat/sorts-exerces.tsv` | **La troisième sonde, sur les sorts** : un couple `(famille, indice, zone, sous-zone)` par ligne, écrit dès qu’une fiche de sort est déclenchée. La paire famille/indice est ce que `CastSpell_0053A980` reçoit ; `census-spells.sh` retrouve le nom lisible dans `Bestiaire/sorts.csv`. **Le lanceur n’est pas relevé** — une incantation, l’opcode 14 d’un script d’objet et le mot `+0x06` d’une entité de scène entrent par la même fonction, et c’est la *fiche* qu’on vérifie. Mêmes garanties, même `OPENFORESIA_NO_PROBE=1`. |
| `Documentation/Reference/legacy-bug.md` | **Bugs of the 1998 game itself**, observed in play and traced in the decompilation. Check here before "fixing" an oddity: it may be a faithful transcription of an original defect. Until 2026-09-26 those were reproduced, not repaired; in « legacy ++ » mode they may be fixed, as a declared deviation approved by Vincent — each entry carries its status (`Reproduit`, `Contourné`, `Corrigé`, `Retiré`). |
| `Documentation/Reference/FormatSauvegarde.md` | **Le fichier `Blaze-Saving…`, octet par octet** : les quatre blocs (fiche, dépôt, copie de `0x3f00`, vignettes), la somme de contrôle signée **et sa copie** — sans laquelle le dépôt se croit vide —, la carte des champs de la fiche et le format d'un emplacement d'objet. À lire avant d'écrire dans une sauvegarde. |
| `Documentation/Reference/MonteeDeNiveau.md` | **Ce que le jeu calcule à une montée de niveau** : le palier, les sept gains, les trois familles de probabilités par classe, le plafond fixé à la création (culminant à 510 pour un départ de 80 à 83), et la chance — la seule statistique qui peut baisser. Tableaux par classe et cas limites de répartition. À lire avant de toucher à `LevelUpCharacter_005391F0` ou de répondre à une question sur la progression. |
| `Documentation/Reference/Sorts.md` | **Le système de sorts** : quelles classes lancent, les trois écoles jouables (plus l’herboristerie inatteignable du Chasseur), la fiche de sort et ses champs, l'apprentissage à la montée de niveau — le seul en jeu normal —, les sorts hors d'atteinte, la combinaison de boutons, le coût en PM. Compagnon de `MonteeDeNiveau.md`. |
| `Documentation/Tests/test-protocol.md` | How to exercise the game. Read it before any run — see "Running the game" above. |
| `Documentation/DLL/<dll>.md` | Accumulated knowledge per DLL (ordinals, signatures, confirmed vs. hypothesis). |
| `OpenForesia/src/Blaze/**` | The reconstruction, split into `Platform/`, `Memory/`, `Engine/`, `Screens/` — **see `Documentation/ARCHITECTURE.md`**. |
| `OpenForesia/src/Libraries/**` | **Les cinq DLL d'aide de 1998, reconstruites** — un répertoire par DLL (`wsyskern/`, `winsys3d/`, `3dhelp/`, `gldhelp/`). **Lire `src/Libraries/LISEZMOI.md` avant d'y ajouter du code.** Leur source de vérité est `winsys3d.c`/`wsyskern.c`/…, **jamais `Blaze.c`** — c'est la raison d'être du répertoire séparé. Quatre règles : la traçabilité porte les trois pièces (`// Ghidra: wsyskern.c LoadBitmapEx @ 0x1000bac0`, forme lue par le recensement) ; les globales d'une DLL ne rejoignent **jamais** `GameMemory` (autre espace d'adressage) ; une classe ne peut pas s'appeler `3dhelp` (répertoire `3dhelp/`, classe `Help3d`) ; la façade `Interop/` ne bouge pas, on y remplace un `[DllImport]` par un renvoi. Et la règle découverte au premier lot : **ne pas couper une représentation en deux** — le tas, les vtables et les sections critiques imposent de porter par sous-arbre complet d'un point d'entrée, pas par sujet. |
| `OpenForesia/src/Host/**` | Our 2026 scaffolding — the Direct3D 11 replacement for the 1998 display chain. **Not bound by fidelity.** |
| `OpenForesia.Installer/` | **The player's installer** (`OpenForesia-Setup.exe`, since 2026-09-26) — ISO reader (`Iso/`), `Editions/editions.json` (the expected files: paths, sizes, MD5 — generated by `--generer-edition`, never by hand, also embedded in the game), extraction, shortcuts, uninstaller (`Uninstall.exe`, a copy of the installer run with `--desinstaller`), native Win32 window (no WinForms: it does not trim). Hidden developer modes: `--extraire-donnees <iso> <dossier>`, `--generer-edition <iso> <editions.json>`, `--jeu <dossier>`. Present-day code, same rules as `src/Host/`. See "The game's data" above. |
| `OpenForesia.Tools/` | PE export-table dumper, **x86-32 disassembler** (`disasm`, via Iced), the **level-up simulator** (`montee`, `MonteeDeNiveau/`), `playtest.ps1`, the census scripts (`census-*.sh`) — dont `census-dll-surface.sh` et son compagnon `census-dll-surface.awk`, qui lit les tables d'exports PE sans passer par `dotnet` — et `census-dll-globals.sh` + `.awk`, la carte des globales —, **`check-descriptors.sh`** (see below), and the split tooling (`split-manifest.awk`, `apply-split.ps1`, `verify-split.ps1`, `blaze-baseline.txt`, `bodies-baseline.txt`). |
| `OpenForesia/src/Host/Indeo4/` | **The Indeo 4.1 decoder of the cinematics, in C#** — FFmpeg's decoder ported, **LGPL 2.1+** (read its `LISEZMOI.md`), byte-identical to Intel's `ir41_32.ax` on all 8 849 frames of the four films; `Host/Yvu9Rgb.cs` converts its YVU9 to RGB as Intel did, minus the dithering. **No 1998 binary is shipped any more** (2026-09-24): the five helper DLLs went on 2026-09-23 — their code is C# (`src/Libraries/`), and the data still read from them is extracted into `src/Libraries/Donnees/` (`OpenForesia.Tools extraire-donnees`) and embedded in the assembly — and `ir41_32.ax` the next day. The originals stay in `Legacy/Binaries/`, `ir41_32.ax` as the oracle of `OpenForesia.Tools indeo`. |
| `OpenForesia.Tools/Html/` | **Les outils qu'on ouvre dans un navigateur** — un `.html` autonome par outil, sans serveur. Le premier est **l'éditeur de sauvegarde** (`editeur-sauvegarde.html`, raccourci `tests\editeur-sauvegarde.bat`) : caractéristiques, niveau, inventaire depuis les 397 fiches françaises du CD avec leurs vignettes, équipement, sorts, destinations et cristaux ; **pensé pour un joueur : aucun décalage ni octet à l'écran**, le contrôle y veille. La page est **générée** : éditer `*.template.html`, relancer `build-editeur-sauvegarde.ps1`, puis `node check-editeur-sauvegarde.mjs`, qui l'exécute sur les vraies sauvegardes. Le second est **le guide des essais** (`guide-gestionnaires.html`, raccourci `tests\guide-essais.bat`) : pour chaque gestionnaire d'entité jamais vu en jeu, où aller (QUEST / MAP), quoi faire et un plan du lieu — ou pourquoi c'est **impossible** dans le jeu d'origine (décor qu'aucune liste `+0x30` ne pose, comportement qu'aucune espèce ne porte ; règle contrôlée sur les 871 déjà vus, `guide-gestionnaires.analyse.mjs --controle`). **Lire son `LISEZMOI.md` avant d'ajouter un outil HTML** — il fixe les règles. |
| `Analyse/` | Notes d'observation du jeu original (comportement, sessions x32dbg). |
| `tests/` | Raccourcis `.bat` a double-cliquer. `tournee-auto.bat` promene la salle d’essai (`OPENFORESIA_AI_BENCH_TOUR`) de sous-zone en sous-zone sur toutes celles qui portent un gestionnaire non teste, sans jouer, et reprend ou elle s’etait arretee (`tournee-faite.txt`) ; `montee.bat` ouvre le simulateur de montee de niveau ; `feuille-de-route.bat` regenere `GestionnairesEntites.md` apres une seance et reaffiche l’avancement par zone ; `objets.bat` fait le meme office pour `ObjetsAtteignables.md` — le compte, les gestionnaires a ecrire, la dette, et le **controle croise** qui signale les objets que la sonde connait mais qu’`objets.csv` ignore ; `sorts.bat` fait le troisieme, pour `SortsAtteignables.md` ; `update-DLLAtteignables.bat` fait le quatrieme, pour `DLLAtteignables.md` — le releve, les neuf points d'entree de `wsyskern` (le premier lot), les dix plus gros corps restants et le compte ecrits/restants ; `editeur-sauvegarde.bat` ouvre l'editeur de sauvegarde ; `guide-essais.bat` refait et ouvre le guide des essais ; `lot-suivant.bat` met le lot suivant de personnages de test (`node OpenForesia.Tools/generer-personnages-test.mjs`) dans `save` et ouvre sa fiche. ASCII pur — `cmd.exe` ne lit pas l’UTF-8, et **aucun `!` dans les commandes** : avec `enabledelayedexpansion`, `cmd.exe` le mange avant que `bash` ne le voie (le `!~` d’awk disparaissait, et le compte sortait a zero). |

`openspec/` holds the change workflow: several completed changes, one archived, and the
`blaze-intro-attract` spec. Use `openspec list` to see the current state.

## Closing a session — always update `HISTORY.md`

**At the end of every work session, add your entries to `Documentation/Etat/HISTORY.md`.** This is
not optional and does not need to be asked for. It is the one file that shows overall progress at a
glance: `REVERSE_STATUS.md` is too detailed for that, and `TODO.md` only describes the current
state.

Keep it **simple** — one line per change, prefixed by its kind:

```markdown
## 2026-08-23

- feat : la carte du monde s'affiche et tient — l'auberge, 2101 images sans plantage
- fix : `ResolveEntityBlocks` lisait les statistiques d'entité par une vue d'arène quand le reste de
  la simulation les lit par leur allocation propre — toute entité de scène naissait à 0 PV
```

`feat :` for an addition, `fix :` for a correction, `doc :` for something learned and written down.
Newest date at the top. No analysis paragraphs — the reasoning belongs in `REVERSE_STATUS.md`;
`HISTORY.md` keeps only the line of progress.

## Working rules (condensed from Strategy.md)

- **Never modify** `Blaze.c` or any decompiled `.c` DLL source. Never treat an earlier C#
  translation as more reliable than `Blaze.c`.
- **Fidelity beats compilability.** Never substitute `0`, `null`, `false`, or an empty body just to
  make the compiler happy; never delete a hard-to-translate branch or an unknown call. When
  something is genuinely unknown, write `// TODO(REVERSE): <precisely what remains to determine>`
  and record it in `REVERSE_STATUS.md`.
- **« Legacy ++ » deviations are a second, separate step.** Transcribe faithfully first; only then
  fix a 1998 defect, remove a 1998 dependency (CD, copy protection, DLLs) or add player comfort —
  keeping the `// Ghidra:` anchor, adding `// Écart legacy ++ (LB-NNN) : <what the original did>`,
  recording it in `legacy-bug.md`, and behind a `true`-by-default switch when the change is visible
  and debatable. Removed code leaves a `// Ghidra: … — retiré (legacy ++, <raison>)` line. Never
  change the game's content or balance without Vincent asking. Full rules: `Strategy.md`.
- **Keep Ghidra traceability** on every reconstructed function: `// Ghidra: FUN_00513900 @ 0x00513900`.
  Rename to a human name only when the role is actually established; otherwise keep `FUN_…` or
  `Unknown_00513900`. Same for `DAT_…`/`PTR_…` globals.
- **Reconstruct control flow, don't transliterate it.** `LAB_…`/`goto`/`joined_r0…` are artifacts —
  rebuild them as `if`/`switch`/`while`/`for`/`break`/`return`. Do not mechanically turn a label
  into a method; extract a method only for a genuinely independent unit.
- **Respect x86-32 semantics**: pointer arithmetic, signed/unsigned, `byte`/`short`/`ushort`/`int`/`uint`,
  struct layouts, vtables, calling conventions. A cast that feels wrong usually means a wrongly
  reconstructed *type*, not a missing cast — fix the root cause.
- Use P/Invoke for recognized Win32/CRT calls rather than transcribing runtime assembly, keeping a
  comment naming the original. For DLL calls, read the DLL's decompiled body before inferring a
  signature from call sites.
- **Read the DLL's own decompilation before reconstructing a call into it — not only for the
  signature, but for the data conventions it implies.** `winsys3d.c`, `wsyskern.c` etc. are sources
  of truth on the same footing as `Blaze.c`; they are just not reconstructed. A worked example:
  `DrawFlatPrimitive`'s `*prim = *otEntry | 0x2000000` was transcribed faithfully and still produced
  a native crash, because the convention it belongs to — the display list is addressed on 24 bits —
  lives in `ClearOTagR`/`DrawOTag` on the DLL side. Reading those two functions first would have
  caught it immediately; a day of debugging did not.
- Build in coherent groups of functions, rebuilding often. If a change causes error counts to
  explode or demands casts everywhere, revert and re-examine the type decision instead of patching.
- Gaps throw a descriptive `NotImplementedException` rather than silently no-op'ing, so a missing
  subsystem is loud at runtime.

## Layout of the reconstruction

`Blaze.cs` was split on 2026-08-19 into 30 files under `OpenForesia/src/Blaze/`, plus
`OpenForesia/src/Host/` for the scaffolding. **`ARCHITECTURE.md` is the authority** — read it before
adding or moving code. The short version:

```
   Platform/   ce qui n'est pas le jeu : interop OS, CRT, winsys3d, demarrage/extinction
   Memory/     l'espace d'adressage : les huit arenes, les tables .data (GameMemory)
   Engine/     appele par plusieurs ecrans : Audio, Video, Input, Resources, SaveData,
               Model3d, UiText, UiWindow, Primitives2d
   Screens/    un repertoire par etat de jeu (State00 .. State06), plus le repartiteur
```

**`Screens/State03/` is the one screen with sub-directories.** `WorldMapTick.cs` had reached
74 072 lines and was split on 2026-09-16 into **70 files** across eleven directories, byte for byte
(`openspec/changes/split-worldmaptick-by-subject`). The seven screen-level files stay at the root of
`State03/`; the directories are what the screen *reaches*:

```
   Tick/       la boucle par image, son dessin, son pas de simulation
   Dispatch/   le carrefour adresse -> methode (426 cases), les tables statiques, les 3 sondes
   Combat/     ce qui decide qui perd des points de vie
   Physics/    ce que TOUTE entite subit -- ne consulte jamais son sous-type
   Entities/   l'infrastructure commune : eveil, ciblage, naissance
   Effects/    ce qui nait, scintille et meurt -- ne decide de rien
   Items/      l'objet et ce qu'il declenche
   Spells/     la magie, une ecole par fichier
   Cutscene/   les scenes et leurs 32 verbes
   Ai/         l'intelligence, UNE CREATURE PAR FICHIER, par zone
   Handlers/   decor et scenario : coffres, radeau, portes, sons
```

`Ai/` names come from `Documentation/Ennemies/Comportement.md`, **never from a guess** — and beware:
there are **two « Goblin »** in forest block A (subtypes 2 and 8, unrelated bodies) and **two
« Abeille tueuse »** (blocks A and C). The species name does not determine the behaviour; the block
and the subtype do.

**Where a newly-named function goes** — the rule, in order:

1. its VA falls in a known engine address range → that `Engine/` file;
2. a **single** screen calls it → that `Screens/StateNN/` file;
3. **several** screens call it → promote to `Engine/` and give it a subsystem name;
4. otherwise → **it stays with its caller** until the role is established. There is no dumping
   ground file, and deliberately so.

Inside `State03/`, once rule (2) or (4) has fired, the table in `ARCHITECTURE.md` picks the
sub-directory **by its membership test, not by the apparent subject**.

**Keep the dynamic — no second 74 000-line file.** `WorldMapTick.cs` did not swell through
negligence: nobody ever *decided* it would. Rule (4) filled it one justified function at a time for
a year, and only the cumulative result was wrong. So in `State03/`:

- **file it straight away** in the right directory — rule (4) is no longer an acceptable default
  here, the eleven directories cover the ground;
- **never pile up "for now"**: if no existing file really fits, that is the signal a new one is
  needed, not that one should be forced;
- **a new file is proposed, never created unilaterally** — put up the name and what it would hold,
  and let the user decide. A new *directory*, all the more so;
- **watch what grows**: `Spells/School0Wizard.cs` (9 137 l.) and `Handlers/Forest.cs` (2 969 l.) are
  the two to look at first. See `ARCHITECTURE.md` for the dial already prepared for the first.

**Naming**: neutral, descriptive, all `static`. **Never** `Manager`, `Service`, `Controller`,
`Provider` — those names promise an object with a lifecycle that the transcription does not have,
and invite drift towards a design the source of truth does not contain.

**No maximum file size.** `Screens/State06/Inventory.cs` is 2 937 lines for three sub-screens of one
menu, and that is coherent. The metric is coherence, not size.

**Never move work in progress.** A finished piece gets filed; if nothing fits, it stays with its
caller.

### Two traps the split itself uncovered

**A global never moves to make a file self-contained.** It moves only when the address map says so;
arenas move as one block. Consult the map of reconstructed symbols in `Documentation/Reference/DataSymbols.md`
before declaring an isolated `DAT_`: 107 pairs of address-adjacent symbols now live in two different
files, and that adjacency is no longer visible in the code.

**Splitting a partial class across files is not semantically neutral.** Static field initialisers run
in the order the compiler receives the *files*, which is unspecified. A `static readonly` field whose
initialiser reads a static member living in **another** file must become a computed `=>` property.
Intra-file dependencies are safe — textual order is guaranteed within one file. This one bit: eight
fields of the form `= Arena(0x…)` returned `null + offset` and the game threw on the first frame.

## Resolving an ordinal-only DLL import

`Blaze.c` calls unnamed DLL exports as `Ordinal_NNN(...)`. To find the implementation:

1. Check `Documentation/DLL/<dll>.md` first — it may already be documented.
2. Fastest path for `winsys3d.c`: it embeds the ordinal in a comment inside each function body
   (e.g. `/* 0x2490  129 */`), so grep for the number *with surrounding spaces*.
3. Otherwise dump the export table (`OpenForesia.Tools`), then add the DLL's base address to the
   export RVA to get the `@ 0x…` address used in the decompiled `.c`:
   `winsys3d.dll` → `0x00a00000`, `wsyskern.dll` → `0x10000000`.
4. Write up whatever is learned back into `Documentation/DLL/<dll>.md`, separating confirmed
   behavior from hypotheses.

## When `Blaze.c` doesn't have the answer — read the binary

`Blaze.c` is a **code-only** decompilation: Ghidra emitted function bodies but not the
`.data`/`.rdata` sections, and it left some bodies undecompiled entirely. Two gap classes follow,
and **both are now solvable inside the repo** — the binary is committed at
`Legacy/Binaries/Blaze.exe`.

**Values that live in initialized data** (string literals, lookup tables, statically initialized
function pointers such as `PTR_FUN_0056c96c`) are not in `Blaze.c` at any price. Read them out of
the binary:

```powershell
dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005631f8 32
```

Record what you recover in `Documentation/Reference/DataSymbols.md`, keyed by VA. **Never invent
plausible content** for one of these — an address that lands in `.data`'s uninitialised tail is
reported as such, and that is an answer.

**And a lookup table's LENGTH is as much a finding as its content.** Consumers index forward from
the address they are handed — the sprite renderer reads `*(byte**)(e + 0x3c) + e[0x40] * 8` — so a
range transcribed too short returns uninitialised heap, with no compile error and no exception: the
sprite simply comes out the wrong size, or invisible, or as scattered garbage. A change of texture
plate is a *hypothesis* about where a table ends, never a proof. This defect shipped three times
(`0x00551b00` on Boule de feu, `0x00551528` on Marteau). Run
**`sh OpenForesia.Tools/check-descriptors.sh`** after touching any `s_Descriptors_…`: it re-reads
every table out of `Blaze.exe` and prints how many records each install site has left after its
offset. A margin of 1 is only safe when the entity's body leaves `+0x40` at zero (a margin of 2,
at 0 or 1) — check that by hand, the script cannot. **Mind the spawner**: `FUN_0047ac40` zeroes
`+0x40`, but `FUN_0047a9d0` and `FUN_0047aae0` copy it from the parent, so "the body never writes
`+0x40`" is only proof for an entity born through the first. In the binary this is harmless: of the
430 handlers those two pools (`0x005ff400`, `0x005edee0`) can run, **none installs a planche on its
own record** (checked 2026-09-16 against `Blaze.c`, the dumps and the binary), so the inherited
`+0x40` always comes with the parent's own `+0x3c`. A C# body in one of those pools that writes
`(e + 0x3c)` is therefore a transcription error, not a new case — the script's last section reads
those pools' handlers out of `Blaze.exe` and flags any such write. Once proven, add the site to
`OpenForesia.Tools/marges-verifiees.txt` with the bodies the proof depends on, and run the script
with `--sceller`: it fingerprints those bodies, shows the site as `prouve`, and brings the warning
back as `PREUVE PERIMEE` the moment one of them changes. All 25 short margins were proven on
2026-09-16.

**Bodies Ghidra did not decompile** — address-taken-only code (window procedures, thread procedures,
vtable slots appearing only as `LAB_…`) and whole functions missing from `Blaze.c`. Until
2026-08-21 the standing advice was "this needs a targeted Ghidra re-run", which meant an action
outside the repository — so in practice it meant *blocked forever*. It no longer does:

```powershell
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
```

`disasm` prints x86-32 with internal jump targets labelled. It is **not** a decompiler — no
reconstructed control flow, no types, no names — but it is enough to transcribe a function by hand,
and `FUN_004747F0` (723 bytes, absent from `Blaze.c`, blocking game state 3 for three days) came out
of it in one pass. Keep the dump under `Legacy/Raw/Blaze/` next to the transcription.

**A byte count is not a difficulty estimate.** That same function had been written off as
"723 bytes with nine convergent branches — that's a decompiler's job". It turned out to be a
perfectly regular cascade of eight near-identical blocks, and the nine "convergent exits" were nine
`return`s. What predicts the cost is the number of *distinct shapes*, not the size.

**Check for a duplicate before transcribing.** `LAB_00474320` sat on the gap list for two days as
"the callback that actually stores a chest's contents". Its 87 bytes turn out to be *identical* to
already-transcribed `FUN_00450700` — one difference, the `call`'s relative displacement, and both
resolve to the same target. The compiler emitted the same body twice. Two `od` dumps and a
subtraction settle it, so run them whenever two addresses play the same role:

```bash
od -An -tx1 -j $((VA - 0x400C00)) -N <len> Legacy/Binaries/Blaze.exe
```

**What is still genuinely blocked**: functions whose parameters Ghidra failed to recover
(`unaff_ESI`, `extraout_var`, fewer parameters than call sites pass). Disassembly resolves those too
in principle, but reconstructing a calling convention from register traffic is real work — flag it
inline rather than guessing a signature.

<!-- rtk-instructions v2 -->
# RTK (Rust Token Killer) - Token-Optimized Commands

## Golden Rule

**Always prefix commands with `rtk`**. If RTK has a dedicated filter, it uses it. If not, it passes through unchanged. This means RTK is always safe to use.

**Important**: Even in command chains with `&&`, use `rtk`:
```bash
# ❌ Wrong
git add . && git commit -m "msg" && git push

# ✅ Correct
rtk git add . && rtk git commit -m "msg" && rtk git push
```

## RTK Commands by Workflow

### Build & Compile (80-90% savings)
```bash
rtk cargo build         # Cargo build output
rtk cargo check         # Cargo check output
rtk cargo clippy        # Clippy warnings grouped by file (80%)
rtk tsc                 # TypeScript errors grouped by file/code (83%)
rtk lint                # ESLint/Biome violations grouped (84%)
rtk prettier --check    # Files needing format only (70%)
rtk next build          # Next.js build with route metrics (87%)
```

### Test (60-99% savings)
```bash
rtk cargo test          # Cargo test failures only (90%)
rtk go test             # Go test failures only (90%)
rtk jest                # Jest failures only (99.5%)
rtk vitest              # Vitest failures only (99.5%)
rtk playwright test     # Playwright failures only (94%)
rtk pytest              # Python test failures only (90%)
rtk rake test           # Ruby test failures only (90%)
rtk rspec               # RSpec test failures only (60%)
rtk test <cmd>          # Generic test wrapper - failures only
```

### Git (59-80% savings)
```bash
rtk git status          # Compact status
rtk git log             # Compact log (works with all git flags)
rtk git diff            # Compact diff (80%)
rtk git show            # Compact show (80%)
rtk git add             # Ultra-compact confirmations (59%)
rtk git commit          # Ultra-compact confirmations (59%)
rtk git push            # Ultra-compact confirmations
rtk git pull            # Ultra-compact confirmations
rtk git branch          # Compact branch list
rtk git fetch           # Compact fetch
rtk git stash           # Compact stash
rtk git worktree        # Compact worktree
```

Note: Git passthrough works for ALL subcommands, even those not explicitly listed.

### GitHub (26-87% savings)
```bash
rtk gh pr view <num>    # Compact PR view (87%)
rtk gh pr checks        # Compact PR checks (79%)
rtk gh run list         # Compact workflow runs (82%)
rtk gh issue list       # Compact issue list (80%)
rtk gh api              # Compact API responses (26%)
```

### JavaScript/TypeScript Tooling (70-90% savings)
```bash
rtk pnpm list           # Compact dependency tree (70%)
rtk pnpm outdated       # Compact outdated packages (80%)
rtk pnpm install        # Compact install output (90%)
rtk npm run <script>    # Compact npm script output
rtk npx <cmd>           # Compact npx command output
rtk prisma              # Prisma without ASCII art (88%)
rtk uv run <cmd>        # Compact uv project command output
```

### Files & Search (60-75% savings)
```bash
rtk ls <path>           # Tree format, compact (65%)
rtk read <file>         # Code reading with filtering (60%)
rtk grep <pattern>      # Search grouped by file (75%). Format flags (-c, -l, -L, -o, -Z) run raw.
rtk find <pattern>      # Find grouped by directory (70%)
```

### Analysis & Debug (70-90% savings)
```bash
rtk err <cmd>           # Filter errors only from any command
rtk log <file>          # Deduplicated logs with counts
rtk json <file>         # JSON structure without values
rtk deps                # Dependency overview
rtk env                 # Environment variables compact
rtk summary <cmd>       # Smart summary of command output
rtk diff                # Ultra-compact diffs
```

### Infrastructure (85% savings)
```bash
rtk docker ps           # Compact container list
rtk docker images       # Compact image list
rtk docker logs <c>     # Deduplicated logs
rtk kubectl get         # Compact resource list
rtk kubectl logs        # Deduplicated pod logs
```

### Network (65-70% savings)
```bash
rtk curl <url>          # Compact HTTP responses (70%)
rtk wget <url>          # Compact download output (65%)
```

### Meta Commands
```bash
rtk gain                # View token savings statistics
rtk gain --history      # View command history with savings
rtk discover            # Analyze Claude Code sessions for missed RTK usage
rtk proxy <cmd>         # Run command without filtering (for debugging)
rtk init                # Add RTK instructions to CLAUDE.md
rtk init --global       # Add RTK to ~/.claude/CLAUDE.md
```

## Token Savings Overview

| Category | Commands | Typical Savings |
|----------|----------|-----------------|
| Tests | vitest, playwright, cargo test | 90-99% |
| Build | next, tsc, lint, prettier | 70-87% |
| Git | status, log, diff, add, commit | 59-80% |
| GitHub | gh pr, gh run, gh issue | 26-87% |
| Package Managers | pnpm, npm, npx | 70-90% |
| Files | ls, read, grep, find | 60-75% |
| Infrastructure | docker, kubectl | 85% |
| Network | curl, wget | 65-70% |

Overall average: **60-90% token reduction** on common development operations.
<!-- /rtk-instructions -->
