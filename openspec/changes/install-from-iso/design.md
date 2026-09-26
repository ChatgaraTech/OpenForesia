## Context

Voir `proposal.md` pour le pourquoi. L'état actuel, relevé le 2026-09-26 :

```
 RunApplication (Startup.cs)
   balaye C..Z → GetDriveTypeA == DRIVE_CDROM ; repli X / Z / P ; sinon « insérez le CD » en boucle
   → UNE lettre, écrite dans l'octet 0 de Mem.s_CdDataRootPath ("@:\FRENCH\")
   → GetFileSize("@:\cd.idx") → s_CdCheckFileSize_0056321c (+ 0xd8000000 plus tard)
 Chemins
   BuildCdPath      "@:\FRENCH\" + nom            (Resources, Video, Startup:615)
   BuildCdRootPath  "@:\snd\Base.snd", l'octet 0 remplacé
   table musiques   "@:/Music/Mel00_01.WAV" …, l'octet 0 remplacé (MainMenu), idem WorldMap:801
   tampons de 64 octets (Malloc(64))
 Ouverture          OpenExistingFileReadOnly → NormalizePath → CreateFileA ; wsyskern Fichiers.cs ×2 ;
                    Audio.cs:847 ; les films passent par Host/AviReader (FileStream .NET)
 Sauvegardes        "save\" relatif au RÉPERTOIRE COURANT (CarteMemoire, CreateFileA)
 cd.idx             UiText.cs:672 : (s_CdCheckFileSize + 1) * pointeur — l'identité seulement si
                    la taille vaut exactement 0x28000000
```

**Le relevé de l'ISO de référence** (`Legend of Forezia.ISO`, 709 183 488 octets = 346 281 secteurs
de 2048, étiquette `BLAZE_BLAZE_F`, un descripteur Joliet en plus du primaire). La table des
répertoires annonce **3,05 Go** de fichiers : des tailles déclarées se chevauchent.

| entrée | déclaré | réel | remarque |
|---|---|---|---|
| `CD.IDX` | 671 088 640 | un AVI de 1 870 932 | la protection : recouvre presque tout le disque |
| `FRENCH\OPENING.AVI` | 463 194 112 | 28 101 316 (RIFF) | déclaré jusqu'au dernier secteur |
| `FRENCH\ANI01.AVI` | 494 411 776 | 31 216 900 (RIFF) | idem |
| `FRENCH2\*` | — | — | doublon exact de `FRENCH\` (`BLAZE.ALL` même MD5 `d24faa2d…`) |
| `DATA\` (racine) | 211 Mo | — | lanceur de 1998 + `BBPOOL.RSC`, nommé par aucun code |

Ce que le jeu lit : `FRENCH\` (`BLAZE.ALL`, `SYSTEM.INI`, `LOGO.BMP`, `DATA\*.PIC|BMP|TXT`, les quatre
`.AVI`), `SND\` (79 fichiers, 9,8 Mo), `MUSIC\` (37 fichiers, 110,7 Mo) — soit ≈ 280 Mo.
`Host/AviReader.cs` borne déjà sa lecture par la taille RIFF (`riffEnd = min(8 + RIFF, Length)`), donc
un AVI coupé à sa taille RIFF se lit à l'identique.

## Goals / Non-Goals

**Goals :**
- une seule règle pour trouver un fichier du jeu, sans lettre de lecteur, sans couche de traduction ;
- une liste des fichiers attendus **générée** depuis l'ISO, source unique pour l'installeur et le jeu ;
- un installeur sans chaîne d'outils externe (pas d'Inno Setup / WiX / MSI), sans droits administrateur.

**Non-Goals :**
- les images `.bin/.cue`, `.mdf`, `.nrg` ; les éditions autres que la française de référence (le
  format de la liste le permettra, rien de plus) ;
- la signature de code de la release ;
- une mise à jour automatique ;
- toucher au format des sauvegardes.

## Decisions

### D1 — Le répertoire courant EST le dossier des données

Au tout début de `RunApplication`, le jeu fixe son répertoire courant au dossier de l'exe
(`AppContext.BaseDirectory`). Tous les chemins du jeu deviennent **relatifs** (`FRENCH\BLAZE.ALL`,
`snd\Base.snd`, `Music/Mel00_01.WAV`), **y compris le fichier de réglages** (`.\OpenForesia.cfg`, lu
côté jeu et côté `winsys3d` : l'original le bâtissait sur le chemin complet de `GetModuleFileNameA`,
qu'un dossier hors de la page de code ANSI aurait abîmé).

Pourquoi : c'est la règle qui règle tout d'un coup.
- `save\` est déjà relatif au répertoire courant : il devient « à côté de l'exe » sans toucher au
  code des sauvegardes.
- Les tampons de 64 octets ne portent plus que des chemins courts (≤ 26 caractères) : aucun
  débordement quelle que soit la longueur du dossier d'installation.
- **Les accents ne posent plus de problème même en `CreateFileA`** : la chaîne passée est de l'ASCII
  pur, et Windows résout le répertoire courant en Unicode. Aucun site d'ouverture n'a à changer d'API.

Alternatives écartées : (a) écrire le chemin complet dans `s_CdDataRootPath` — 7 sites à reprendre,
tampons à agrandir, `CreateFileA` à remplacer partout ; (b) garder `@:` et traduire à l'ouverture —
une couche de traduction qui n'existe que pour préserver une lettre de lecteur qu'on abandonne.

**Pas de réglage de racine.** Il était prévu (clé `[Donnees] Racine` du `.cfg`) et a été abandonné
à l'implémentation : un répertoire courant différent du dossier de l'exe aurait rendu le `.cfg`
introuvable en relatif, et obligé à convertir en Unicode tous les accès `…ProfileA`. En
développement, on extrait donc les données **dans le dossier de sortie** de la build
(`OpenForesia-Setup --extraire-donnees <iso> OpenForesia\bin\Debug\net10.0`, deux secondes ; `bin` est
ignoré par git), et `save\` y reste, comme avant.

### D2 — Le multiplicateur de `cd.idx` est retiré, pas simulé

`UiText.cs:672` rend directement `text + 3`. `s_CdCheckFileSize_0056321c` et ses deux écritures
disparaissent. Poser `0x28000000` en dur aurait gardé un calcul qui ne sert qu'à la protection ; en
« legacy ++ » on l'enlève, et l'écart est déclaré dans `legacy-bug.md` avec l'arithmétique d'origine.

### D3 — Ce qui disparaît du code

Le balayage et ses replis, `s_PleaseInsertCdMessage`, le test du lecteur `P:`, l'octet-lettre de
`s_CdDataRootPath` (qui devient `"FRENCH\"`), `BuildCdRootPath` (les modèles `@:\snd\…` et
`@:/Music/…` deviennent relatifs), les recopies de lettre de `Video.cs`, `MainMenu.cs`, `WorldMap.cs`
et `Startup.cs:615`, l'appel à `CdSearchFile` (ordinal 92) et son corps, la branche `cdrom:` de
`CarteMemoire.cs`. Chaque suppression laisse une ligne `// Ghidra: …` qui dit ce qu'il y avait, pour
que la traçabilité vers `Blaze.c` survive.

### D4 — Une liste d'édition, générée, embarquée des deux côtés

Un fichier `editions.json` décrit chaque édition connue : étiquette de volume, MD5 de `BLAZE.ALL`, et
pour chaque fichier son chemin dans l'ISO, son chemin installé, sa taille installée, son MD5, et s'il
faut le couper à sa taille RIFF. Il est **produit par l'installeur lui-même**, en mode caché
(`OpenForesia-Setup.exe --generer-edition <iso> <sortie>`), lancé sur l'ISO de référence — jamais
tapé à la main. Le fichier vit dans le projet de l'installeur (`OpenForesia.Installer/Editions/`),
qui l'embarque ; le jeu l'embarque aussi, par un lien de fichier dans son `.csproj`, pour qu'il n'y
en ait qu'un exemplaire. Le jeu ne vérifie que l'existence et la taille (instantané) ; l'installeur
vérifie aussi le MD5.

Alternative écartée : une règle « tout `FRENCH\`, `SND\`, `MUSIC\` sauf `*.EXE`/`*.DLL` ». Elle
marcherait sur cette image mais ne donnerait ni les tailles ni les sommes, et laisserait passer
`INSTALL.EXE`, `SETUP.DA_`, `GKSL_FRN.DLL` au gré des exceptions. La liste explicite ne ment pas.

### D5 — Le lecteur d'ISO vit dans l'installeur

Un sous-dossier `Iso/` de `OpenForesia.Installer` : lecture du descripteur primaire et Joliet,
parcours des répertoires, lecture d'un fichier par secteurs, coupe RIFF, reconnaissance d'édition.
Il n'a qu'un utilisateur, l'installeur (y compris son mode `--generer-edition`). Joliet sert les noms
en casse d'origine (`French`, `Snd`, `Music`) — cosmétique, Windows ignore la casse.

Alternative écartée : une bibliothèque `OpenForesia.Iso` partagée avec `OpenForesia.Tools`, pour que
les outils lisent l'ISO sans installation. Un projet de plus pour un confort dont on n'a pas besoin :
une installation est toujours sous la main, et les outils lisent le dossier installé.

### D6 — L'installeur : un seul exécutable C#

Projet `OpenForesia.Installer`, publié en fichier unique autonome x86, **élagué**. Sa fenêtre est
faite de contrôles Win32 natifs (`Interface/`), ses appels COM (sélecteurs de fichier et de dossier,
raccourcis) en interfaces générées à la compilation, son JSON en sérialiseur généré. Écrit d'abord en
WinForms, il a été réécrit ainsi le 2026-09-26 : WinForms ne s'élague pas et pesait à lui seul une
quarantaine de Mo. Le jeu embarqué est élagué lui aussi, avec les avertissements d'élagage traités
comme des erreurs, et publié **en un seul `OpenForesia.exe`** de 10 Mo : le runtime y est lié, le
dossier du joueur ne porte ni DLL ni `createdump.exe`. L'installeur publié passe de 82 à **15,5 Mo**. Une
compilation native (NativeAOT, ≈ 13 Mo) resterait possible sans toucher au code, mais demande les
outils C++ de Visual Studio. Il **embarque le jeu**
(sortie de `dotnet publish OpenForesia -c Release -r win-x86 --self-contained`, zippée en ressource).
Étapes : ISO → reconnaissance → dossier (défaut `%LOCALAPPDATA%\Programs\OpenForesia`, avertissement
sous `Program Files`, refus au-delà de 200 caractères, contrôle de place) → extraction avec barre de
progression et annulation → vérification MD5 → dépôt du jeu → `save\` → raccourcis (COM
`IShellLinkW`) → entrée `HKCU\Software\Microsoft\Windows\CurrentVersion\Uninstall\OpenForesia`.

La désinstallation est **`Desinstaller.exe`, une copie de l'installeur** déposée dans le dossier du
jeu, comme l'`unins000.exe` d'Inno Setup. Lancé par le menu Démarrer, par « Applications
installées » (`--desinstaller`) ou par un double-clic (son nom suffit), il se recopie dans `%TEMP%`
et se relance de là — un exécutable ne peut pas effacer son propre fichier —, demande
confirmation, attend que le jeu soit fermé, demande s'il faut garder `save\`, retire raccourcis,
clé et fichiers, puis confie l'effacement de sa copie temporaire à un `cmd.exe` caché (l'effacement
au redémarrage demanderait des droits d'administrateur). En développement, l'installeur n'étant pas
en fichier unique, rien n'est copié : l'entrée désigne l'installeur du dépôt avec le dossier en
argument.

Historique de ce choix : prévu d'abord ainsi, il avait été remplacé à l'implémentation du lot 4 par un
script PowerShell (`Desinstaller.ps1`), parce que l'installeur pesait alors 82 Mo. Revenu à l'exe le
2026-09-26, une fois l'installeur à 15 Mo : c'est la norme attendue, et cela évite de lancer
PowerShell avec `-ExecutionPolicy Bypass`, que des antivirus surveillent.

En développement, tant que le lot 5 n'a pas produit `jeu.zip`, `OpenForesia-Setup --jeu <dossier>`
ouvre la même fenêtre en prenant les fichiers du jeu dans un dossier de build (données, `save\`,
`.cfg` et symboles écartés). Sans l'option, un installeur sans jeu embarqué prend la build du dépôt
(`OpenForesia\bin\Debug\net10.0`, retrouvé en remontant jusqu'à `OpenForesia.sln`), pour qu'un
double-clic suffise en développement ; hors du dépôt, il dit qu'il ne contient pas le jeu.

Alternative écartée : l'import au premier lancement dans le jeu lui-même — plus simple, mais le
joueur attend un installeur, des raccourcis et une désinstallation, et cela mettrait du WinForms et de
la lecture ISO dans l'exécutable du jeu.

### D7 — Une build publiée est muette

Les sondes de couverture (`CoverageProbes.cs`), `AiBench` et les captures `scratchpad` écrivent en
remontant de quatre dossiers depuis l'exe — dans le dépôt en développement, n'importe où une fois
installé. Elles sont désarmées quand l'exécutable ne tourne pas depuis un dépôt (absence du marqueur
`OpenForesia.sln` à `..\..\..\..`), en plus de `OPENFORESIA_NO_PROBE=1`.

### D8 — La release

Un script `OpenForesia.Tools/publish-release.ps1` publie le jeu, le zippe, publie l'installeur qui
l'embarque, et sort `OpenForesia-Setup.exe`. Aucune donnée du jeu n'entre dans la release :
`editions.json` ne contient que des noms, des tailles et des sommes.

## Risks / Trade-offs

- [Exécutable non signé : SmartScreen affiche « Windows a protégé votre ordinateur »] → le dire dans
  le README de la release ; la signature est hors périmètre.
- [Le répertoire courant change pendant la partie (ordinal 30, `ChangerRepertoire`)] → il n'a aucun
  appelant en C# aujourd'hui ; ajouter une vérification au recensement pour qu'un futur appel ne
  déplace pas silencieusement la racine.
- [Un code non encore relu construit encore un chemin `@:`] → après la suppression, `grep "@:"` sur
  `src/` doit être vide ; la vérification au démarrage n'attrape que les fichiers de la liste.
- [`save\` change de place pour le développeur (répertoire de travail → dossier des données)] → le
  noter dans `test-protocol.md` et `OpenForesia.cfg.md` ; les sauvegardes existantes sont à déplacer à
  la main une fois.
- [L'installeur embarque ≈ 70 Mo de runtime .NET] → accepté : c'est le prix de « aucun prérequis ».
- [Le lecteur ISO lit une image de 700 Mo : lenteur perçue] → lecture séquentielle par blocs d'1 Mo,
  barre de progression par octets et non par fichier (les AVI dominent).

## Migration Plan

1. Créer `OpenForesia.Installer` avec son lecteur d'ISO et le mode `--generer-edition` ; générer la
   liste depuis l'ISO de référence ; en extraire une première installation, pour le développement.
2. Basculer le jeu (D1–D3, D7) ; en développement, installer une fois les données dans un dossier et
   le nommer dans `OpenForesia.cfg` de `bin\Debug\net10.0\`.
3. Adapter les outils et `playtest.ps1`.
4. L'installeur, puis le script de release.

Retour arrière : le changement est confiné à des commits distincts par lot ; revenir au lot 2 rétablit
le CD.
