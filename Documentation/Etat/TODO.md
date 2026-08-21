# TODO

Point d'entrée entre deux sessions. Mis à jour le **2026-08-21**. L'historique détaillé est dans
`REVERSE_STATUS.md` (sections datées) ; ce fichier ne garde que l'état courant et la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

**État git au 2026-08-21.** Commité jusqu'à `2d5bb36` (« Refacto Audio.cs ») : les six premières
vraies classes, `OpenForesia.Tests/`, et les références figées `blaze-baseline.txt` /
`bodies-baseline.txt`.
**Non commité** : les conversions de `Resources.cs`, `Model3d.cs` et `UiWindow.cs` — l'étape C est
terminée —, le rapatriement `.data` du son, le retrait intérieur des points d'entrée, la correction
des huit défauts d'aliasing (qui **change le comportement**), la transcription de `FUN_00533940` et
`FUN_00533450`, **la réorganisation du dépôt**, et cette mise à jour de documentation.

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
qu'à l'étape D.

## Reprendre ici — `FUN_005350b0`, la palette de zone (95 lignes)

**Le front a bougé le 2026-08-21.** `FUN_00533940` et sa dépendance `FUN_00533450` sont transcrites
et s'exécutent sur les vraies données du maillage. La pile de la reproduction documentée s'arrête
maintenant une étape plus loin, et c'est un fait mesuré, pas une prévision :

```
NotImplementedException: FUN_005350b0 : la mise en place de palette de zone (95 lignes)
   at ApplyAreaPalette(Byte)        WorldMap.cs:1300
   at PlaceWorldMapParty(Byte*)     WorldMap.cs:1548
   at RunWorldMapFrame()            RunWorldMapState()   RunGameStateTick()
```

`FUN_005350b0` est petite et sur le chemin chaud. Ce qu'un premier coup d'œil montre : elle remplit
des entités à partir de `&DAT_005fa5e0 + DAT_005e9ae8 * 0x27` (donc des enregistrements de 0x9c
octets dans `EntityArena`), en lisant une liste prise dans `*(DAT_005e97cc + 0x30)`. Vérifier
lesquelles de `DAT_005e9ae8`, `DAT_00600838` et `DAT_005fa5e0` sont déjà modélisées avant d'écrire.

**Ensuite** : `FUN_00516fc0` (1226 lignes), la logique par image de la carte du monde — c'est elle
qui pose `DAT_005645f8`, décide des sorties, et fera enfin apparaître quelque chose à l'écran.
Mesurer ses dépendances manquantes avant de s'y mettre.

### Reproduire l'arrêt — et un piège du banc d'essai

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"
$env:OPENFORESIA_SKIP_AREA_HANDLER = '1'
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 30 -WorkingDir OpenForesia -Shot `
  -Then WAIT4,ENTER,WAIT1,START,WAIT4,ENTER,WAIT3,ENTER,WAIT12
```

**Si ça ne reproduit rien, chercher `relance detectee` dans la sortie avant de soupçonner la
séquence.** Une course perdue pour la mémoire basse relance le jeu en processus fils et décale tout
le minutage des touches : la séquence part dans le vide et le verdict affiche « aucun plantage »,
ce qui est trompeur. Relancer suffit (environ une fois sur cinq). Écrit dans `test-protocol.md`.

**Prérequis** : au moins une sauvegarde dans `OpenForesia\save\`. Il y en a quatre.

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
      │  └─ CallAreaHandler(8) → 0x004747F0   [ÉCHAFAUDÉ, voir plus bas]
      ├─ RunWorldMapFrame      FUN_00516350   [corps FAIT]
      │  ├─ LoadWorldMapSubArea                [FAIT]
      │  │  ├─ InstallEntityVTables            [FAIT — 7 tables + 11 initialiseurs]
      │  │  └─ UploadRawAreaBlocks FUN_005355f0 [FAIT]
      │  └─ PlaceWorldMapParty                 [FAIT]
      │     ├─ TryPlaceEntity   FUN_005330d0   [FAIT]
      │     ├─ FinalizeEntityPlacement2       [FAIT le 2026-08-21 — FUN_00533940, 375 l.]
      │     │  └─ GroundHeightOnFace          [FAIT le 2026-08-21 — FUN_00533450, 52 l.]
      │     └─ ApplyAreaPalette FUN_005350b0   ←←← ON S'ARRÊTE ICI (95 lignes)
      ├─ ReInitializeAllTextures / Ordinal_674 [FAIT]
      └─ RunWorldMapTick       FUN_00516fc0    [à faire, 1226 lignes — le gros morceau]
```

## Comment reproduire l'arrêt en trente secondes

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"     # le SDK n'est pas dans le PATH du shell
$env:OPENFORESIA_SKIP_AREA_HANDLER = '1'
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

## L'échafaudage à retirer — dette assumée

`OPENFORESIA_SKIP_AREA_HANDLER=1` neutralise l'appel à `FUN_004747F0` (`0x004747F0..0x00474AC3`,
~723 octets, neuf sorties convergentes), que **Ghidra n'a pas décompilée** : corps uniquement
adressé, aucune fonction entre `FUN_004742b0` et `FUN_00474ad0` dans `Blaze.c`.

C'est **une déviation de comportement**, pas une omission forcée par la plateforme. La fonction
sautée est une machine de transition de sous-zone (elle éteint des bits dans `DAT_005E9840/41` et
`DAT_005E9000/01/06`, compte les bits levés de `DAT_005E9000`, compare `DAT_005DE52C`/`DAT_005DE530`
à zéro). **Tout ce qui a été observé sous ce drapeau est à re-vérifier** une fois la fonction
décompilée.

→ **Ça demande une re-passe Ghidra ciblée sur `0x004747F0`.** Décoder 723 octets à la main avec neuf
branches convergentes serait déraisonnable — à ne pas confondre avec les deux « re-passes requises »
de cette semaine qui se sont révélées faire vingt octets chacune.

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
