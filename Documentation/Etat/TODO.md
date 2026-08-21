# TODO

Point d'entrée entre deux sessions. Mis à jour le **2026-08-21**. L'historique détaillé est dans
`REVERSE_STATUS.md` (sections datées) ; ce fichier ne garde que l'état courant et la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

**État git au 2026-08-21.** Commité jusqu'à `2d5bb36` (« Refacto Audio.cs ») : les six premières
vraies classes, `OpenForesia.Tests/`, et les références figées `blaze-baseline.txt` /
`bodies-baseline.txt`.
**Non commité** : les conversions de `Resources.cs`, `Model3d.cs` et `UiWindow.cs` — l'étape C est
terminée —, le rapatriement `.data` du son, le retrait intérieur des points d'entrée, la correction
des huit défauts d'aliasing (qui **change le comportement**), la transcription de `FUN_00533940`,
`FUN_00533450`, `FUN_004747F0` (celle-ci **absente de `Blaze.c`**, désassemblée sur place) et
`FUN_005350b0`, le verbe `disasm` d'`OpenForesia.Tools`, les vingt désassemblages d'opcodes de
`Legacy/Raw/Blaze/ScriptOps/`, **la réorganisation du dépôt**, et cette mise à jour de documentation.
S'y ajoute, le 2026-08-21 au soir : l'arène des paramètres de vue (étape 1), **tout
`Screens/State03/WorldMapScript.cs`** (étape 2), la correction du défaut de `RvaToFileOffset` dans
`OpenForesia.Tools`, la rétractation des notes qui en découlaient, la correction de l'arité de
`Winsys3d.Ordinal_45` et l'ajout d'`Ordinal_46`.

**Où en est le jeu.** Le chemin « Début du jeu → constitution du groupe → carte du monde » va
maintenant jusqu'au bout et s'arrête **exactement sur `FUN_00516fc0`**, c'est-à-dire l'étape (4).
Recette de test :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 50 -Shot -WorkingDir OpenForesia `
    -Then WAIT4,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT2,START,WAIT4,ENTER,WAIT4,ENTER,WAIT10
```

La recette de `test-protocol.md` (un seul `ENTER` avant `START`) ne suffit plus : la sauvegarde de
test porte quatre personnages, il faut les retenir un par un.

**Le dépôt a été réorganisé le 2026-08-21** — la racine ne porte plus que `CLAUDE.md` et
`OpenForesia.sln`. Ce fichier vit maintenant dans `Documentation/Etat/`. Les décompilations sont
dans `Legacy/Decompiled/`, les binaires dans `Legacy/Binaries/`. Tout est passé par `git mv`, donc
l'historique de chaque fichier suit. Détail dans `REVERSE_STATUS.md`, section du même jour.

**`bodies-baseline.txt` a été refigé** à **513 corps** le 2026-08-21, après les corrections
d'aliasing puis après `FUN_00533940` et les deux messages d'exception dont le chemin a changé.
L'ancienne référence d'avant les corrections reste dans l'historique, au commit `95344c7`.

Les deux fichiers de référence de `OpenForesia.Tools/` sont l'oracle — les perdre le perdrait.
Ils sont désormais dans l'historique, donc récupérables ; ce n'était pas le cas le 2026-08-19.

---

# Priorité 1 — **ÉTAPE C TERMINÉE**, et la moitié intérieure des points d'entrée retirée

Les neuf fichiers d'`Engine/` sont des classes d'instance, et **aucune n'atteint plus une autre par
`Subsystems.cs`** : les 28 qualifications croisées sont devenues des paramètres de constructeur.

```
UiWindow(GameMemory, UiText, Input, Audio)
Primitives2d(GameMemory, UiText, UiWindow)
Resources(GameMemory, Primitives2d, Input, Audio)
Video(GameMemory, Resources)
```

Les cinq autres gardent `(GameMemory mem)`. Les sept `Vid.` restants dans `Engine/Video.cs` sont des
**auto-références** depuis des membres statiques (`[UnmanagedCallersOnly]`, lambdas `static`) : elles
ne relèvent pas du retrait, elles sont la raison d'être du mécanisme.

**Il reste 665 lecteurs**, tous dans `Screens/` et `Platform/`. Ce sont encore des fichiers de
`partial class Blaze` : pas de constructeur, donc rien à injecter. `Subsystems.cs` ne disparaîtra
## Reprendre ici — objectif : **le jeu atteint et exécute le prologue de `FUN_00516fc0`**

Plan arrêté le 2026-08-21, en quatre étapes indépendamment vérifiables. **Cocher au fur et à
mesure**, et re-mesurer avant chaque étape — les estimations se sont révélées fausses **trois**
fois, dont une à l'étape (1) de ce plan même (voir « Cinq avertissements payés comptant »).

- [x] **(1) FAITE le 2026-08-21 — mais pas comme le plan le disait.** Le plan annonçait « étendre
      `EntityState` vers le bas sur `0x005710f0..0x005714ff`, 231 adresses ». **Trois quarts de
      cette région n'appartiennent pas au jeu**, et l'étendre ainsi aurait fait deux dégâts :
      * `0x005710f0..0x00571258` est du **CRT** — `DAT_00571258` est lu par `WideCharToMultiByte`
        (`FUN_0054B190`) et `DAT_005710F0` est sa table de pages de code, parcourue par paires.
        Une arène d'état de jeu l'aurait avalée.
      * ~~`0x00571400..0x00571500` est une **table initialisée** de valeurs `0x00208xxx`~~ —
        **faux, rétracté le même jour** : défaut de l'outil `at`, voir `REVERSE_STATUS.md`.
        `0x00571400` reste la bonne borne, mais parce que c'est la fin du contenu de `.data`
        présent dans le fichier.
      * `0x0057125c..0x0057126c` n'est cité nulle part.

      Le bloc réel est **`0x00571270..0x00571400`**, soit 0x190 octets et **69 symboles**, vérifié
      **intégralement nul au chargement** (384 octets relevés, zéro dword non nul). Créé sous
      `s_ViewParams_00571270` / `ViewParams(va)` — il alimente `s_ViewBlock_006067c0` par addition,
      reçoit la caméra en deux exemplaires parallèles, et porte des drapeaux à `0x0057139c`.
      **Neutre en comportement** : aucune des 123 adresses n'était un champ C#, donc rien à aliaser.
      Les deux tables flanquantes sont notées dans `Documentation/Reference/DataSymbols.md`.
      Vérifié : build 0 erreur, avertissements inchangés, **5 tests de disposition verts**,
      non-régression `-Entry 1` à 382,3.
- [x] **(2) L'interpréteur de script — FAITE le 2026-08-21, la machine est complète.**
      `OpenForesia/src/Blaze/Screens/State03/WorldMapScript.cs`, ~1 000 lignes.

      Écrit : la boucle (`FUN_00513dd0` + `FUN_00513e40`), le répartiteur, **22 opcodes sur 39**
      (1, 2, 3–10, 11, 12, 13, 16, 18, 19, 23–26, 28, 29) et **huit appelées**
      (`FUN_00532740`, `FUN_00538980`, `FUN_005389e0`, `FUN_0051b850`, `FUN_0047a9d0`,
      `FUN_005035c0`, `FUN_00535300`, `FUN_00535400`).

      **Les opcodes 1 et 2 sont la pièce qui rendait la boucle lisible** : saut, et saut
      conditionnel qui *remet le résultat à zéro*. C'est pour ça que la boucle laisse passer
      l'opcode 2 alors qu'un résultat est posé — sans lui, un test de script arrêterait le script
      pour de bon. Aucun des deux n'est dans `Blaze.c`.

      **Les 17 opcodes restants lèvent une lacune nommée**, pas un trou muet. Deux ont un blocage
      établi, écrit dans le message d'exception :

      * **opcode 0** — appelle `(&PTR_FUN_0056aec8)[script[1]]`. Les 256 entrées du binaire pointent
        toutes vers `FUN_00401fa0`, un `ret` nu, **mais ce n'est que le défaut** : `Blaze.c` l. 75143
        remplit la table à l'exécution, 0x40 entrées à la fois, depuis `(&PTR_PTR_00567318)[zone]`.
        Il faut ce chargeur et les jeux de fonctions par zone, pas une relecture du gestionnaire.
      * **opcode 27** — laissé de côté sciemment : tire `FUN_00530f20` (153 l.) et `FUN_005312d0`
        (109 l., criblée d'artefacts de pile), qui tirent à leur tour `FUN_00544350`, `FUN_0051bcc0`,
        `FUN_00530ed0`, `FUN_0053db20`, `FUN_00532710`. C'est la cascade d'affichage des nombres de
        dégâts, hors du chemin vers le prologue.

      Les quinze autres (11 écrit, restent 14, 15, 17, 20, 21, 22, 30–38) n'attendent qu'une reprise
      du désassemblage : les relevés banqués de `Legacy/Raw/Blaze/ScriptOps/` sont **des fenêtres de
      96 octets** et certains corps débordent — reprendre au verbe `disasm` avec une taille plus
      large.

      Trois arités de Ghidra ont été démenties au niveau machine avant transcription (`FUN_0053a920`
      en prend 2 et non 3, `FUN_00530f20` en prend 3 et non 2, `FUN_00532740` en reçoit 2 dont une
      morte), et un défaut d'aliasing a été évité de justesse : `s_PartySize_005e9020` est un
      `static int` **hors arène**, donc lire `0x005e9020` par adresse aurait rendu zéro en silence.
- [x] **(3) `FUN_00401000` et `FUN_00401080` — FAITE le 2026-08-21.** Transcrites dans
      `Screens/State03/WorldMap.cs` sous `SnapshotViewVector` et `PublishFrameView` : elles ne sont
      appelées que par le prologue de (4). Comme annoncé, purs brassages de globales une fois (1)
      faite — le seul calcul est `0x005de06c + 0xc8000`, appliqué deux fois.
- [x] **(4) Le prologue de `FUN_00516fc0` — FAITE le 2026-08-21. L'exception a disparu.**
      Écrit contre `Legacy/Raw/Blaze/FUN_00516FC0-prologue.asm`, pas contre la pseudo-C seule.
      **La clé `piStack_74` est maintenant confirmée au niveau machine** : la valeur posée avant un
      appel est bien le *dernier* argument, celui que Ghidra a perdu (`FUN_0052a1e0` en prend trois,
      `add esp,0Ch`) — sauf quand c'est une adresse de code, auquel cas c'est l'adresse de retour et
      l'appel ne prend rien.

      **La boucle par image reste un trou sonore**, comme prévu : le `do { … } while` de
      `0x00517163`, ~1 100 lignes.

### La frontière est maintenant la chaîne de verbes de l'opcode 0

Le prologue s'exécute et lance l'interpréteur, qui bute sur l'opcode 0 — l'appel de verbe de zone.
**Ce n'était pas le mur annoncé.** La note de l'étape (2) disait « il faut d'abord le chargeur et
les jeux de fonctions par zone » ; en réalité le chargeur existe déjà
(`InstallEntityVTables`, `Engine/Resources.cs`) et la table de l'index 0 y est relevée depuis
longtemps sous `s_VtSource_0056aec8`. Il ne manquait que le répartiteur, écrit sur le patron de
`CallAreaHandler`.

Le travail restant est **mécanique et répétitif** : le jeu nomme un verbe, on le transcrit, on
relance, il en nomme un autre.

- [x] verbe 3 = `FUN_00442F60` (336 o., **absente de `Blaze.c`**, désassemblée — dump dans
      `Legacy/Raw/Blaze/AreaVerbs/`). Décale d'un cran les bits du banc `0x005e9000` vers
      `0x005e9840`, choisit une piste musicale d'après `0x005e96fe` et la lance. Rend 0.
- [x] verbe 10 = `FUN_004441F0` — **souche** `xor eax,eax ; ret`. À ne pas confondre avec
      `FUN_00401FA0`, qui est un `ret` **nu** : celui-là laisse `EAX` intact, donc le résultat de
      condition reçoit l'indice du verbe, tandis que celui-ci rend 0. Les confondre donnait un
      script qui s'arrête là où il devrait continuer.
- [x] opcode 14 = `0x005158f0` — affichage d'un dialogue, absent de `Blaze.c`, transcrit.
- [ ] les treize autres verbes réels de l'index 0 (`0x00443100`, `0x00443260`, `0x004430B0`,
      `0x00442F20`, `0x00442E70`, `0x00442EA0`, `0x00442E20`, `0x00445840`, `0x004431F0`,
      `0x00442E10`, `0x00442DB0`, `0x004457F0`, `0x00442D90`), plus `0x0053DD20/DEA0/DF20` aux
      indices 56–58. **Aucun n'est demandé pour l'instant** — le prologue va au bout sans eux.
      Un balayage a confirmé que seul `0x004441F0` est une souche ; les quinze autres sont de
      vraies fonctions.

**Le retour d'un verbe devient le résultat de condition**, donc il ne peut pas être deviné : un
verbe non transcrit lève en se nommant plutôt que de rendre une valeur plausible.

### (5) La boucle par image — le prochain chantier, et il est d'un autre ordre

**C'est la frontière depuis le 2026-08-21 au soir.** Le prologue s'exécute en entier, ses deux
lancements de script vont au bout, et l'exécution s'arrête sur la lacune posée en fin de prologue.

Forme : `Blaze.c` l. 63835–64929, **~1 095 lignes**, un seul `do { … } while(true)` avec **quatre
labels** (`0x00518473`, `0x005193ff`, `0x00519455`, `0x00517a64`) — donc à reconstruire en flot, pas
à translittérer. **Une seule sortie**, ligne 64096, gardée par `DAT_005de02c & 2` : elle relance
deux fois `FUN_0040a600`, pose `DAT_005e9898`/`DAT_005e9908` à 1 et rend la main.

L'arbre d'appelées de la seule tête de boucle :

| fonction | état | taille |
|---|---|---|
| `FUN_0040a600` | **no-op**, `ret` nu vérifié au désassembleur | — |
| `FUN_00533000` | déjà transcrite | — |
| `FUN_0053dc30` | à écrire | 34 l. (tire `FUN_0053db20`, 11 l.) |
| `FUN_0050e2b0` | à écrire | 107 l. |
| `FUN_0050e520` | à écrire | 41 l. |

Puis, plus loin dans le corps : `FUN_004011c0` (**358 l.**), `FUN_00532b40` (24 l.),
`FUN_005320f0` (48 l.), et `FUN_0051be90` — **absente de `Blaze.c`**, à désassembler.

**Découpage proposé** : (a) la tête de boucle et ses quatre appelées ; (b) l'affichage de mise au
point (~150 l., gardé par `DAT_005633a8 == 0 && DAT_005de020 != 0`) — vérifier d'abord s'il est
seulement atteint en jeu normal, auquel cas une lacune gardée suffit ; (c) la sortie et ce qui la
précède, pour que le tick puisse au moins rendre la main ; (d) le reste.

**Attention `FUN_0050e520`** : elle lit `DAT_005de034`, le symbole du défaut d'aliasing latent
signalé plus bas. Passer par `s_Unknown_005de034` comme `CharacterCreation.cs`, pas par
`Camera(0x005de034)`, tant que le doublon n'est pas résorbé.

### Ce que la vérification a déjà corrigé dans ce plan

J'espérais que le prologue saute l'interpréteur. **Faux, vérifié** : `DAT_005645f8` vaut **2** au
chargement (lu dans le binaire à `0x005645f8`) et `RunWorldMapState` le remet à 2, donc la branche
`== 2` est prise dès la première image et appelle `FUN_00513dd0` deux fois. L'interpréteur est
incontournable — d'où sa place en (2) et non en option.

### Et ce qui rend l'objectif plus intéressant qu'il n'en a l'air

Juste après le prologue, la boucle appelle `FUN_0040a600` (corps vide, vérifié), `FUN_00533000`
(déjà écrite), puis **`FUN_0053dc30` (34 l.)** et **`FUN_0050e2b0` (107 l.)**. Ce ne sont pas les
monstres de 999 et 591 lignes — ceux-là sont plus loin dans la boucle.

Une fois le prologue franchi, on est donc probablement à **~150 lignes des premières images de la
carte du monde**, pas à 5900. **L'estimation « ~5900 lignes » compte tout l'arbre, pas le chemin
chaud** — et l'expérience du 2026-08-21 montre que le chemin chaud est bien plus court (une seule
fonction, `FUN_00533940`, a débloqué trois étapes d'affilée).

### La clé de lecture, sans laquelle la pseudo-C ment

**`piStack_74 = X;` juste avant un appel est le DERNIER ARGUMENT EMPILÉ, pas une affectation.**
Vérifié au désassembleur : `FUN_00513dd0` reçoit **trois** arguments là où Ghidra en montre deux, et
`piStack_74 = (int *)0x190; Ordinal_379();` est en réalité `Ordinal_379(0x190)`.

Contrôler chaque appel précédé d'une telle affectation — le compte se lit sur le `add esp,N` qui
suit le `call` (`N/4` arguments, cdecl) :

```powershell
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe <va du site> 60
```

Même chose pour les `unaff_EBX` / `unaff_ESI` / `unaff_EBP` / `unaff_EDI` (60 lignes) : ce sont les
**curseurs de primitives**, reconnaissables au motif `curseur = Draw(otEntry, curseur, …)`.
`local_60` est l'entrée d'ordering table ; `FUN_0052a1e0` (`FrameBegin`) prend **trois** paramètres
là où Ghidra en montre deux — précédent déjà documenté du dépôt.

### Reproduire l'arrêt

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 36 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT5,ENTER,WAIT4,ENTER,WAIT16
```

Aucun drapeau d'échafaudage. Si ça ne reproduit rien : chercher `relance detectee`, et vérifier le
**débit** — ~434 primitives/image veut dire qu'on est resté sur l'écran de groupe, l'état 3 atteint
donne ~1. Relancer suffit.

## Cinq avertissements payés comptant

**Vérifier sa propre note avant de planifier dessus.** Ce fichier annonçait que `Resources.cs`
« porte `ToAnsiZ` » et que sa conversion demanderait de décider où vit cette aide. C'était faux :
`ToAnsiZ` est dans `Platform/Startup.cs:310`, déjà `internal static`, et n'a jamais bougé. Une
difficulté annoncée qui n'existait pas a failli dicter l'ordre de travail.

**Le suffixe VA n'est pas la preuve, c'est l'ancre `// Ghidra: DAT_…` qui tranche.** La règle de
tri de 7.9 (« pas de suffixe VA → échafaudage ») a mal classé quatre symboles en deux jours :
`s_BaseSoundBank`, `s_LevelSoundBank`, `s_SoundDirectory` et `s_GameStateId` (`DAT_005de510`) sont
du `.data` pur dont le nom C# ne dit pas l'adresse. Les trois premiers sont maintenant dans
`GameMemory` ; `s_GameStateId` reste chez `GameStates.cs`, exposé `internal`.

Et ce n'est pas qu'une question de rangement : **un `.data` sans suffixe VA est hors de portée de
l'outillage.** Les tests de disposition n'indexent que par le suffixe du nom, donc ils étaient
verts sur un recouvrement bien réel (voir la priorité 1 bis). Le nom est l'index, pas une décoration.

**Un outil de mesure qui range mal fabrique un plan faux, et personne ne le relit.** Le script de
clôture n'avait pas de branche pour `Platform/` : ses symboles tombaient dans « autre Engine ».
`Model3d` a donc porté pendant trois jours l'étiquette « 18 emprunts à `Engine/`, à faire quand
ceux-ci seront convertis », alors que 17 des 18 étaient les registres GTE de `Platform/Winsys3d.cs`
— de l'interop vers `winsys3d.dll`, qui n'allait jamais devenir une classe. Le fichier était en
réalité le **plus fermé** des deux restants. Script corrigé ci-dessous ; la leçon générale est de
se méfier d'un seau fourre-tout dans un `case`, parce qu'il ne se signale jamais.

**Une référence datée ne vaut pas contrôle.** Le 2026-08-21, la conversion d'`UiWindow` a rendu
**5/16 puis 6/16** au taux de course, contre les 22–25 % documentés : alarme franche sur la mesure
même qui avait attrapé la régression du 19. Il n'y avait **aucune** régression — 32 essais plus
tard, le même code rendait **1/32 = 3 %**, contre un contrôle de l'arbre d'avant à **5/32 = 16 %**
mesuré juste à côté.

Le taux **dérive au fil d'une session**, même machine au repos. Le seul contrôle valable est
`git stash` + build + 32 essais, **adjacent dans le temps**. Procédure écrite dans
`test-protocol.md`. Et raisonner sur le mécanisme d'abord : une conversion qui rend des champs
paresseux ne peut que *réduire* le travail placé devant la course — quand la mesure dit l'inverse
de ce que le code impose, suspecter la mesure, mais la refaire plutôt que l'ignorer.

**Trois défauts d'outillage en une journée, et aucun ne produisait d'erreur** : le seau fourre-tout
du script de clôture, l'angle mort des tests de disposition (voir priorité 1 bis), et le filtre
« hors commentaire » qui prenait `*(uint*)…` pour une continuation de bloc. Chacun rendait un
chiffre plausible ou un test vert. **C'est la forme normale d'une panne d'outil dans ce dépôt.**

**Et le troisième a resservi le jour même, sur la décision la plus structurante.** La matrice de
dépendances entre classes, mesurée avant le retrait des points d'entrée, comptait d'abord une arête
`Input → Video` — qui fermait un cycle `Input → Video → Resources → Input`. Un cycle veut dire
« injection par constructeur impossible, garder les points d'entrée ». **Ce `Vid.` était dans un
commentaire.** Avec le filtre corrigé, l'arête disparaît et le graphe est un DAG propre.

Une conclusion architecturale entière tenait sur une ligne de commentaire mal classée. **Un chiffre
qui ferme une porte mérite d'être vérifié à la main avant qu'on renonce.**

## Mesurer les dépendances entre classes — la matrice

Écrite le 2026-08-21 pour le retrait des points d'entrée, et **réutilisable telle quelle à
l'étape D** : elle dit, pour chaque classe, laquelle elle atteint et combien de fois. Ce qui reste
sur la diagonale est une **auto-référence** — un membre statique qui rejoint son instance — et ne
relève pas du retrait.

```bash
cd OpenForesia/src/Blaze
POINTS="Ui Prims Saves Vid Inp Aud Res Mdl UiWin"
printf "%-16s" "depuis \\ vers"; for e in $POINTS; do printf "%-7s" "$e"; done; echo
for f in Engine/*.cs; do
  printf "%-16s" "$(basename $f .cs)"
  for e in $POINTS; do
    n=$(grep -nE "(^|[^A-Za-z0-9_.])$e\." "$f" \
        | grep -vE "^[0-9]+:\s*(//|///)" | grep -vE "^[0-9]+:\s*\*\s" | wc -l)
    [ "$n" = "0" ] && printf "%-7s" "." || printf "%-7s" "$n"
  done
  echo
done
```

**Le filtre à commentaires n'est pas décoratif ici.** Sans lui, la matrice comptait une arête
`Input → Video` qui fermait un cycle et faisait conclure « injection impossible ». Ce `Vid.` était
dans un commentaire. Toujours ouvrir les lignes d'une arête avant d'en tirer une conclusion
d'architecture — une seule ligne mal classée suffit à inverser la décision.

## Mesurer la clôture — la version corrigée du script

Refaire la mesure avant de partir : elle a changé à chaque conversion. **Utiliser cette version-ci**,
celle du 2026-08-21 avec les branches `Platform/` et `Engine/` séparées.

```bash
cd OpenForesia/src/Blaze
for f in Screens/State03/WorldMap.cs; do   # etape D : remplacer par le fichier vise
  gm=0; ic=0; en=0; pf=0; sc=0
  for s in $(grep -oE "\bs_[A-Za-z0-9_]+" $f | sort -u); do
    if grep -qE "^    (private|internal).*\b$s\b" $f; then ic=$((ic+1)); continue; fi
    src=$(grep -rlE "^    (private|internal).*\b$s\b" --include=*.cs . | head -1 | sed 's|^\./||')
    case "$src" in
      Memory/GameMemory.cs) gm=$((gm+1));;
      Platform/*|Interop/*)  pf=$((pf+1));;
      Screens/*)             sc=$((sc+1));;
      Engine/*)              en=$((en+1));;
    esac
  done
  printf "%-22s %5s | GameMem %-3s ici %-3s Engine %-3s Platform %-3s SCREENS %s\n" \
    "$f" "$(wc -l < $f)" "$gm" "$ic" "$en" "$pf" "$sc"
done
```

**Et le compte agrégé ment sur un point** : il inclut les symboles cités **en commentaire**. Lister
le détail avant de conclure (boucle identique, en imprimant `$s` et sa source au lieu de compter).

**Pour distinguer code et commentaire, ne pas filtrer sur `*` en début de ligne.** Le réflexe est
d'écrire `grep -vE "^[0-9]+:\s*(//|///|\*)"` pour retirer les lignes de commentaire, `*` visant les
continuations de bloc `/** … */`. Dans ce dépôt c'est **faux** : une ligne de code qui commence par
un déréférencement — `*(uint*)(atlas + …)` — est alors comptée comme un commentaire. Ça a fait
déclarer `s_FadeCounter_005e9960` « cité en commentaire seulement » alors qu'il est lu par
`DrawUiPanel` ; le compilateur l'a rattrapé, pas la mesure. Filtrer ainsi :

```bash
grep -vE "^[0-9]+:\s*(//|///)" | grep -vE "^[0-9]+:\s*\*\s"
```

`* ` **suivi d'une espace** est une continuation de commentaire ; `*(` est un déréférencement.
Dans du code à pointeurs, la nuance n'est pas cosmétique.

**La taille d'un fichier ne dit rien de son coût de conversion ; sa clôture, si — encore faut-il
la mesurer juste.** Les neuf fichiers d'`Engine/` sont convertis ; le script reste ici parce qu'il
sert tel quel à l'étape D, sur les douze fichiers de `Screens/`.

| classe | point d'entrée | membres | statiques | champs |
|---|---|---:|---:|---:|
| `UiText` | `Ui` | 55 | 0 | — |
| `Primitives2d` | `Prims` | 5 | 0 | — |
| `SaveData` | `Saves` | 33 | **8** (rappels natifs) | — |
| `Video` | `Vid` | 59 | 2 | — |
| `Input` | `Inp` | 61 | 1 | — |
| `Audio` | `Aud` | 76 | 1 (par **portée**) | — |
| `Resources` | `Res` | 67 | 0 | — |
| `Model3d` | `Mdl` | 7 | 0 | **0** |
| `UiWindow` | `UiWin` | 5 | 0 | **0** |

## La recette, éprouvée neuf fois — elle resservira à l'étape D

1. **Mesurer** : membres, `[UnmanagedCallersOnly]`, `delegate*`, et d'où vient chaque `s_*` lu.
2. **Trancher les globales empruntées.** La règle porte sur l'**ancre `// Ghidra:`**, pas sur le
   nom C# — corrigé le 2026-08-21 après quatre mauvais classements :
   * le commentaire ancre un `DAT_…`/`PTR_…` → c'est du `.data`, ça va dans `GameMemory`, **même
     si le nom C# n'a pas de suffixe VA**. Le suffixe est un indice commode, pas la preuve ;
     `s_BaseSoundBank` **est** `DAT_00604920` et `s_GameStateId` **est** `DAT_005de510` ;
   * aucune ancre `DAT_…` → c'est notre échafaudage, ça reste chez son propriétaire, exposé
     `internal` (les tables d'options `s_KeyBindings`, `s_JoystickSlot`, …) ;
   * **un symbole qui en aliase un autre déménage avec lui**, et un bloc déménage entier
     (les trois clés d'ini `s_SoundVolumeIni`/`s_CdVolumeIni`/`s_GammaIni` sont parties ensemble
     parce que `s_GammaIni` **est** `s_Unknown_005d9de0`) ;
   * **exception** : un symbole engagé dans un défaut d'aliasing recensé ne bouge pas — le
     déplacer obligerait à trancher l'alias, donc à changer le comportement. `0x00566c70` est
     dans ce cas.

   **Et si le nom manque son suffixe VA, ne pas le renommer en passant** : l'oracle compare du
   texte, un renommage y est indiscernable d'un changement de logique et ferait sonner ses lignes
   à chaque exécution future, sauf à refiger la référence. Le renommage va avec un changement qui
   refige de toute façon.
3. Déclaration de classe → `internal sealed unsafe class X(GameMemory mem)`, puis retirer `static`
   des membres. Le champ injecté s'appelle **`mem`, sans underscore** : le retour arrière négatif
   de l'oracle exclut `_`, donc `_mem.` serait lu comme un changement de logique.
4. Les membres appelés depuis l'extérieur passent `internal`. Vérifier que chaque membre exposé a un
   usage **réel** — plusieurs candidats n'étaient cités qu'en commentaire.
5. `Mem.` → `mem.`, sauf dans les membres restés statiques.
6. Ajouter `using static OpenForesia.Blaze.Blaze;` pour les aides encore dans `Blaze`.
7. Point d'entrée **paresseux** dans `Subsystems.cs` : `internal static X Xx => s_xx ??= new X(Mem);`
8. Qualifier les sites d'appel. **Une constante se qualifie par le nom de type**
   (`Input.s_GammaScale_0054d000`), pas par l'instance — sinon CS0176.
9. Vérifier : build, oracle, tests, playtest **et le taux de course** (voir plus bas).

## Choisir le nom du point d'entrée — ce n'est pas cosmétique

La liste blanche de `compare-bodies.ps1` est **insensible à la casse**. Y inscrire un nom qui
ressemble à une variable locale courante rend l'oracle aveugle à de vrais changements sur ces
lignes. `Prims` a été choisi plutôt que `Prim` parce que `prim` est un local dans 196 lignes du code
de dessin. Vérifier avant de nommer :

```bash
grep -rn --include=*.cs -iE "\bnomcandidat\b" OpenForesia/src | wc -l
```

Les occurrences **en commentaire** sont sans risque : l'oracle retire les commentaires avant de
normaliser. Noms pris : `Ui`, `Prims`, `Saves`, `Vid`, `Inp`, `Aud`, `Res`, `Mdl`, `UiWin`.

**Et ne jamais donner au point d'entrée le nom de son propre type.** `Audio` était disponible et
lisible ; il a été écarté au profit de `Aud` parce qu'une propriété homonyme de son type déclenche
la règle « Color Color » de C# — `Audio.PlaySoundEffect(...)` se résout alors tantôt comme un accès
statique, tantôt comme un accès d'instance, **selon la classe où la ligne est écrite**. Ça compile,
et c'est exactement le genre d'ambiguïté qu'une transcription vérifiée à la main ne doit pas avoir
à porter. Les abréviations ne sont donc pas qu'un confort de frappe.

## Un membre peut rester statique par PORTÉE, pas seulement par contrainte

La liste des trois motifs ci-dessus était incomplète ; `Audio` en a ajouté un quatrième, et c'est le
plus facile à manquer parce que rien ne le force :

* **Un membre qu'un autre sous-système appelle sans avoir rien à faire de celui-ci.**
  `Audio.InvokeComMethod` est une invocation COM générique de sept lignes. `Platform/Startup.cs`
  s'en sert pour libérer le **périphérique DirectInput** — qui n'a aucun rapport avec le son. Le
  rendre d'instance ferait construire tout le sous-système audio (quatre allocations natives) pour
  libérer une manette, et placerait ce travail devant la course pour la mémoire basse.
  Le garder `internal static` coûte une qualification par nom de type ; l'alternative coûte une
  dépendance fausse. **Se poser la question « qui l'appelle, et est-ce que ça a un sens qu'il
  dépende de ce sous-système ? » avant de retirer un `static` par réflexe.**

## Un déménagement de fonction est gratuit pour l'oracle — s'en servir

`ShutdownStage3` (`FUN_00512ff0`) vivait dans `Platform/Startup.cs` avec les trois autres étapes
d'extinction, et il était **le seul écrivain externe de `s_DirectSound`**. Deux issues :

* l'exposer `internal` et modifiable de loin — c'est-à-dire garder la fuite que la conversion existe
  pour supprimer ;
* le déménager dans `Audio`, où son adresse le place de toute façon (règle de routage 1).

Le deuxième choix a été pris, et il n'a **rien coûté** : l'oracle indexe par **nom seul**, un corps
inchangé qui change de classe ne sonne pas — vérifié, zéro méthode « absente de l'arbre courant ».
**Quand un membre doit être exposé uniquement pour une fonction extérieure, regarder d'abord si
c'est la fonction qui est mal placée.** C'est souvent le cas, et le déménagement est vérifiable.

## Ce qui reste statique, et pourquoi

Une classe mixte n'est pas un compromis raté. Trois motifs rencontrés, tous légitimes :

* **`[UnmanagedCallersOnly]`** exige une méthode statique. Si son corps est une une-ligne qui écrit
  une globale (`SaveData`), rien d'autre à faire ; si son corps appelle d'autres membres
  (`Video.MoviePlayerWindowProc`), il rejoint l'instance par le point d'entrée statique (`Vid.`) ;
  s'il n'utilise que ses paramètres (`Input.CountJoystickCallback`), il reste autonome.
* **Un lambda `static`** garde son mot-clé et passe par le point d'entrée : le `static` d'un lambda
  est une garantie de non-capture, on ne la brade pas pour économiser une qualification.
* **Une décision d'hôte** lue par `src/Host/` à l'initialisation de ses propres types
  (`Video.s_WindowedStartup`) reste statique : en faire un champ d'instance coupleraient cette init
  à la construction complète des sous-systèmes.

## Le piège qui a coûté le plus cher — à ne pas rouvrir

Les sous-systèmes de `Subsystems.cs` sont **paresseux**, `Mem` est **immédiat**. Ce n'est pas un
détail de style :

```
[ModuleInitializer] → TryPrepareDisplayListArena() → lit un champ statique de Blaze
                    → construit tout ce qui est immédiat      ← devant la course
                    → PUIS réclame l'arène basse              ← la course
```

La réclamation ne lit que `Mem`. Tant que les quatre points d'entrée étaient `static readonly`,
chaque conversion ajoutait son travail d'allocation devant une course non déterministe : mesuré
**5 échecs sur 8** contre 1 sur 4 documenté. Rendus paresseux : retour à 25 %.

**Toute nouvelle conversion doit donc être vérifiée sur le taux de course**, pas seulement sur le
build et le débit — ce dernier était vert pendant toute la régression. La boucle de mesure est dans
`test-protocol.md`, section « relance détectée ». **Seize essais minimum, machine au repos** : à ce
taux, huit essais ne distinguent pas 25 % de 40 %, et un build concurrent fausse le résultat
(mesuré 44 % sous charge, 12 % au repos sur le même binaire).

## Ce que l'étape C laisse ouvert

* **L'étape D** — `Screens/` en 12 classes (~13 800 l.), à ne commencer que quand les écrans
  cesseront de bouger. `Screens/State03/WorldMap.cs` est le front de la priorité 2.
* **`GameMemory` reste incomplet** : il modélise ~115 des 398 symboles `.data`. Le reste est
  dispersé dans les fichiers qui les utilisent. Ce n'est pas bloquant, mais chaque conversion en
  rapatrie une poignée.
* **Les défauts d'aliasing.** Cinq sont signalés par `OpenForesia.Tests` (3 tests rouges
  **attendus**) ; un **sixième** a été trouvé le 2026-08-21 et les tests étaient verts dessus —
  voir la priorité 1 bis. Les corriger change le comportement → changement séparé. `0x00566c70`
  doit être traité **avant** la reprise de la carte du monde, et `0x005e8520` va avec le
  renommage qui refige la référence de l'oracle.
* **Huit globales `.data` traversent encore la frontière d'une classe convertie** sans passer par
  `GameMemory`, exposées `internal` chez leur propriétaire :

  | symbole | vit dans | lue par |
  |---|---|---|
  | `s_GameStateId` (`DAT_005de510`) | `GameStates.cs` | `Resources` |
  | `s_MenuRenderBlock_00566c70` | `MainMenu.cs` | `Resources` |
  | `s_Unknown_005e97c8` | `MainMenu.cs` | `Resources` |
  | `s_Unknown_005e9818` | `MainMenu.cs` | `Model3d` |
  | `s_Unknown_005de52c` | `Destination.cs` | `Resources` |
  | `s_Unknown_005e92b0` | `DebugParty.cs` | `Resources` |
  | `s_Unknown_005de154/158/15c` | `MainMenu.cs` | `UiWindow` |
  | `s_FadeCounter_005e9960` | `MainMenu.cs` | `UiWindow` |

  `s_MenuRenderBlock_00566c70` est **bloquée** par le défaut d'aliasing de la même adresse ; les
  cinq autres relèvent de l'étape A ordinaire, à rapatrier en un seul geste vérifié plutôt qu'en
  marge d'une conversion — c'est ce qui a bien marché pour la dette du son (tâche 7.12).
* La réparation des **~870 lignes à encodage double** (`repair-double-encoded-comments`, ouvert,
  non commencé). Commentaires et journaux seulement, rien de visible en jeu.


---

# Priorité 1 bis — **SOLDÉE le 2026-08-21**

Les huit défauts d'aliasing sont corrigés et **les cinq tests de disposition passent**. Le détail
complet — chaque étendue, comment elle a été déduite, et ce que le binaire a démenti — est dans
`REVERSE_STATUS.md`, section du 2026-08-21. Résumé :

| # | défaut | correction |
|---|---|---|
| 1 | `0x00566c70` alloué deux fois | une allocation dans `GameMemory`, 0x14 |
| 2 | `s_MenuScreenBlock_00566c48` 0x40 pour un écart de 0x28 | 0x28 |
| 3 | `s_SoundBankHeader_005e8520` détaché du répertoire de sons | vue dans `s_SoundDirectory_005e8340` |
| 4 | `s_EntityList_005de558` avalé par `ItemMenuArena` | arène 0x460 → **0x410** |
| 5-7 | trois allocations propres dans `EntityArena` | devenues des vues |
| 8 | `s_CardPathDescriptor_00604940` 0x100 pour un écart de 0x20 | 0x20 |

**Deux de ces huit étaient invisibles aux tests**, et c'est le résultat le plus utile :
`s_SoundBankHeader_005e8520` (nom sans suffixe VA) et surtout `DAT_006008d0/d4/d8`, le décalage
monde — écrit par `WorldMap` **à travers l'arène**, lu par `Model3d` dans un **champ séparé**. Les
deux mémoires ne se rencontraient jamais.

La référence de l'oracle est **refigée à 511 corps**. L'ancienne est dans l'historique (`95344c7`).

## Ce que ça laisse ouvert — deux élargissements de l'outillage

Les deux valent mieux que de corriger ce que l'outil voit déjà : ils élargissent ce qu'il **peut**
voir.

1. **Étendre `Aucune_allocation_propre_dans_l_etendue_d_une_arene` aux champs de valeur.** Le test
   ne considère que `SymbolKind.OwnAllocation` ; un `int` à une adresse d'arène passe — c'est
   exactement `DAT_006008d0`. Attention : la première exécution signalera plusieurs cas, dont
   certains légitimes (un champ peut modéliser une adresse d'arène volontairement si aucun site ne
   l'atteint par l'arène). Vérifier chacun.
2. **Auditer les `.data` nommés sans suffixe VA.** Quatre ont été renommés parce qu'on avait établi
   que leur nom mentait ; un balayage systématique des déclarations ancrées `// Ghidra: DAT_…` mais
   nommées sans adresse en trouverait d'autres, et chacune est par construction invisible aux tests.

## Et un chantier de reconstruction, mesuré

**L'arène de carte mémoire.** `0x00604940..0x006049bc` est une région contiguë de globales de carte
— 9 symboles, **81 sites relevés** — qui mérite le traitement « un bloc, chaque symbole une vue »
de l'arène `0x005e9000`.

Tant qu'elle n'existe pas, un nom de fichier de plus de 26 octets déborderait sur le **tas** au lieu
de déborder sur `DAT_00604960` comme dans l'original. Le plus long nom du jeu de 1998 fait 19
octets, donc la marge tient — mais elle est plus mince qu'avant, et c'est le prix de la fidélité.

À faire séparément : ça touche le sous-système de sauvegarde, le plus difficile à vérifier à
l'écran.

---

# Priorité 2 — Finir l'état 3, la carte du monde

## Où on en est exactement

L'**état de jeu 3, la carte du monde**, est le front. Le chemin d'appel :

```
RunGameStateTick (état 3)
└─ RunWorldMapState            FUN_00516020   [FAIT]
   ├─ LoadWorldMapResources    FUN_005160e0   [FAIT]
   └─ boucle par image
      ├─ LoadWorldMapArea      FUN_00516250   [FAIT]
      │  └─ CallAreaHandler(8) → 0x004747F0   [FAIT le 2026-08-21 — absente de Blaze.c,
      │        RunArea0Handler8                  desassemblee sur place ; 724 octets]
      │        ├─ ApplyPartyScriptOp             [FAIT — FUN_00514000 + FUN_00515d70]
      │        └─ ApplyScriptOp                  [FAIT — FUN_00514040, l'ALU du script, 190 opcodes]
      ├─ RunWorldMapFrame      FUN_00516350   [corps FAIT]
      │  ├─ LoadWorldMapSubArea                [FAIT]
      │  │  ├─ InstallEntityVTables            [FAIT — 7 tables + 11 initialiseurs]
      │  │  └─ UploadRawAreaBlocks FUN_005355f0 [FAIT]
      │  └─ PlaceWorldMapParty                 [FAIT]
      │     ├─ TryPlaceEntity   FUN_005330d0   [FAIT]
      │     ├─ FinalizeEntityPlacement2       [FAIT le 2026-08-21 — FUN_00533940, 375 l.]
      │     │  └─ GroundHeightOnFace          [FAIT le 2026-08-21 — FUN_00533450, 52 l.]
      │     └─ SpawnSubAreaEntities FUN_005350b0 [FAIT le 2026-08-21 — le peuplement de
      │           sous-zone ; l'ancien nom ApplyAreaPalette etait faux]
      ├─ ReInitializeAllTextures / Ordinal_674 [FAIT]
      └─ RunWorldMapTick       FUN_00516fc0    ←←← ON S'ARRÊTE ICI (1226 l. + ~5900 d'appelees)
```

## Comment reproduire l'arrêt en trente secondes

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"     # le SDK n'est pas dans le PATH du shell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 24 -WorkingDir OpenForesia `
  -Then WAIT4,ENTER,WAIT1,START,WAIT4,ENTER,WAIT3,ENTER,WAIT8
```

Séquence : « Début du jeu » → un personnage retenu (`ENTER`) → `START` → « Veux-tu jouer avec ce
groupe? » → `ENTER` → « Oui » → `ENTER`. L'exception nommée tombe avec la pile complète.

**Prérequis** : au moins une sauvegarde dans `OpenForesia\save\`. Il y en a quatre.

## `FUN_00533940` — **FAITE le 2026-08-21**, et ce qu'elle a appris

Transcrite sous le nom `FinalizeEntityPlacement2`, avec sa dépendance `FUN_00533450`
(`GroundHeightOnFace`). Le détail est dans `REVERSE_STATUS.md`. Trois choses valent d'être retenues
ici parce qu'elles resserviront :

**Le graphe n'était pas irréductible.** Ce fichier annonçait « cinq labels sautés depuis plusieurs
endroits : il faut reconstruire le flot ». C'est vrai, mais la reconstruction est simple une fois
qu'on voit ce que les labels sont : **une boucle sur les arêtes de la face**.

```
triangle : (i0,i2)  (i2,i1)  (i1,i0)
quad     : (i0,i2)  (i2,i1)  (i1,i3)  (i3,i0)
```

Chaque arête est testée par un `NormalClip` ; un signe négatif fait franchir **cette** arête vers la
face voisine, puis on reprend à l'arête suivante. `LAB_00533af7`, `LAB_00533b96` et `LAB_00533e9f`
sont exactement « reprendre à l'arête 1, 2, ou 2-quad ». La comparaison de clés d'arête qui suit la
marche est une **table de correspondance déguisée** : elle se résume à
`arête suivante = arête franchie + 1`.

**Une expression inlinée peut être une fonction déjà reconstruite.** Les six termes que Ghidra
répète six fois sont le `NormalClip` du SDK, déjà écrit dans `Platform/Winsys3d.cs` sous
`FUN_0051c280`. Recouper terme à terme avant d'appeler : Ghidra les émet dans un autre ordre.

**Un remplacement « plus fidèle » peut changer le sens.** Ghidra écrit `(… / N.y - y0) * 0x1000`.
Écrit `… - y0 << 0xc`, c'est juste. Remplacer `<< 0xc` par `* 0x1000` pour coller au texte de Ghidra
casse tout : `*` lie plus **fort** que `-`, `<<` plus lâche. **Vérifier la classe de précédence
avant de substituer un opérateur à un autre.**

### Ce que ce fichier annonçait et qui s'est révélé faux

> `_DAT_005de730` / `_DAT_005de734` … **Pas encore modélisés, et ils tombent hors de tout ce qui
> existe** … Il faudra une arène de plus.

**Non.** C'est un bloc de 8 octets, et la taille se déduit de trois côtés qui concordent : deux
écritures de dword mais des relectures en shorts (donc quatre shorts), un indice borné à 0..3 des
deux façons dont il est calculé, et le symbole suivant `s_Unknown_005de738` à exactement 8 octets.
Ajouté à `GameMemory` sous `s_FaceWalkHistory_005de730`.

**C'est la troisième note de ce fichier démentie par la vérification en trois jours.** Les deux
autres sont dans « Cinq avertissements payés comptant ».

### Le modèle à suivre

`TryPlaceEntity` (`FUN_005330d0`), transcrite aujourd'hui, fait **exactement la même géométrie** :
même disposition de sommet, même produit vectoriel dans 799/800/801, même saturation dans 813. La
relire d'abord — elle donne les bons types et les bons décalages, et elle est **vérifiée par le
comportement** (sa boucle de tirage aléatoire sort, donc elle trouve bien des faces praticables sur
les données réelles).

### Les ordinaux GTE

799 et 800 ont été **identifiés aujourd'hui** (les deux vecteurs d'entrée du produit vectoriel) et
sont résolus dans `EnsureGteRegisters`. 801 = `MAC1..3`, 808 = `MAC0` (où le NCLIP dépose son
signe), 813 = `IR1..3`, 805/806/807 = les trois sommets projetés. Voir
`Documentation/DLL/winsys3d.md`, section « The emulated GTE register file ».

---

## L'échafaudage `OPENFORESIA_SKIP_AREA_HANDLER` — **RETIRÉ le 2026-08-21**

Il n'existe plus, et rien ne le remplace : `FUN_004747F0` est transcrite. L'état 3 traverse la
transition de sous-zone pour de vrai.

**Ce que ce blocage a appris, et qui vaut au-delà de lui.** Ce fichier disait : « ça demande une
re-passe Ghidra ciblée — décoder 723 octets à la main serait déraisonnable ». Deux erreurs dans une
phrase :

1. **« Re-passe Ghidra » désignait une action hors du dépôt**, que personne ne pouvait faire depuis
   une session de travail. Le verrou était donc permanent en pratique, pas temporaire. Or le binaire
   est dans le dépôt : il ne manquait qu'un décodeur. `Iced` est ajouté à `OpenForesia.Tools`, et le
   verbe `disasm` existe désormais :

   ```powershell
   dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
   ```

   **Toute future « re-passe Ghidra requise » est un travail interne.** Les corps que Ghidra n'a pas
   décompilés — procédures de fenêtre, corps seulement adressés, fentes de vtable — cessent d'être
   des impasses.

2. **723 octets ne mesurent pas une difficulté.** La fonction s'est révélée être une cascade
   parfaitement régulière : huit blocs identiques au bit près, et les « neuf sorties convergentes »
   sont neuf `return`. Transcription : 60 lignes. Ce qui mesure la difficulté, c'est le **nombre de
   formes distinctes** — ici deux.

Relevé conservé dans `Legacy/Raw/Blaze/FUN_004747F0.asm`.

---


## Le reste, par ordre de valeur

1. **`FUN_005350b0`** (95 lignes) — la palette de zone, appelée en fin de `PlaceWorldMapParty`.
   Petite, sur le chemin chaud.
2. **`FUN_00516fc0`** (1226 lignes) — la logique par image de la carte du monde. C'est **elle** qui
   pose `DAT_005645f8` et décide des sorties, et c'est elle qui fera enfin apparaître quelque chose
   à l'écran. Mesurer ses dépendances manquantes avant de s'y mettre, comme on l'a fait pour les
   autres.
3. **`FUN_00549380`** (424 lignes) — le vrai moteur `sprintf`. Aujourd'hui le seul format utilisé
   (`"q:\snd\Q%02d_%02d.snd"`) est posé en dur dans `LoadWorldMapSubArea`. À généraliser le jour où
   un second format apparaît, **pas avant**.
4. Les phases 9 et 10 de `FUN_00524880` et toute la boucle de sauvegarde du menu objets sont
   transcrites mais **injoignables dans le jeu de 1998** (voir `legacy-bug.md`, LB-004) : rien à y
   faire, ne pas les « réparer ».

---

## Rappels de méthode qui ont servi cette semaine

- **Mesurer avant d'écrire.** Un balayage transitif du sous-graphe (index des fonctions construit en
  une passe `awk` sur `Blaze.c`, croisé avec les `FUN_` déjà présents dans `Blaze.cs`) donne en
  quelques secondes « N fonctions manquantes, M lignes ». C'est ce qui a évité de partir sur les
  17 400 lignes de l'état 3 d'un bloc.
- **Quand la pseudo-C est ambiguë, lire le désassemblage** — pas renoncer. Trois « re-passe Ghidra
  requise » de cette semaine faisaient vingt octets. La distinction utile est la **taille**.
- **Une constante 64 bits dans du code 32 bits est un indice, pas un mur** :
  `pbVar14[-0xffffffff00000008]` est l'indice `-8`.
- **Un `DAT_` isolé au milieu d'une région modélisée est presque toujours un champ de cette
  région.** Le défaut d'aliasing s'est reproduit une vingtaine de fois ; le dernier en date
  (`DAT_005e9080`, « Au prix de: 0 ») ne s'est vu qu'à l'écran, le compte de primitives étant
  identique.
- **Vérifier contre l'écran, pas contre un nombre.** Voir la mémoire du projet à ce sujet.
- Le `save\` suit le **répertoire de travail**, le `.cfg` suit le chemin de l'**exe**. Un `save\`
  vide fait rebondir « Invertaire » et « Enchère » vers l'écran racine — c'est le comportement de
  l'original, pas un lien mort.
