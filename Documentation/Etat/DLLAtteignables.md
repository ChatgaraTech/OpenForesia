# Les DLL d'aide, et ce que la reconstruction en atteint

**Genere** par `sh OpenForesia.Tools/census-dll-surface.sh` — ne pas editer a la main.
**Le regenerer apres chaque corps de DLL transcrit.**

Le jeu de 1998 se repose sur cinq DLL d'aide, livrees sur le CD et jamais reconstruites : elles
tournent encore en natif, appelees par `[DllImport]` depuis `OpenForesia/src/Blaze/Interop/`. Ce
document est la feuille de route pour s'en passer.

**Il ne mesure pas la taille des DLL, il mesure ce que le jeu en atteint.** La distinction est tout
l'objet du releve : la somme des cinq decompilations est un devis faux d'un ordre de grandeur. Ce
qui compte est la CLOTURE — les points d'entree que `Interop/` importe, plus tout ce que leurs corps
appellent, de proche en proche. Le reste de chaque DLL est du code mort pour `Blaze.exe`.

Les deux colonnes d'etat sont celles de `GestionnairesEntites.md`, et se lisent pareil :

| colonne | ce qu'elle dit |
|---|---|
| **employe** | la reconstruction importe ce point d'entree — il est atteint |
| **implemente** | un corps C# le porte, hors de `Interop/` — il est ecrit |

## Le graphe de dependances

Releve dans les binaires eux-memes — les noms de DLL qu'ils portent en clair, que ce soit par
table d'imports ou par `LoadLibraryA`. Une DLL que personne ne nomme n'est chargee par
personne.

```
Blaze.exe      --> winsys3d.dll, wsyskern.dll
winsys3d.dll   --> 3dhelp.dll, gldhelp.dll, glide2x.dll, wsyskern.dll
wsyskern.dll   --> (rien)
3dhelp.dll     --> wsyskern.dll
gldhelp.dll    --> glide2x.dll, wsyskern.dll
playst.dll     --> 3dhelp.dll
```

## Le releve

L'ordre des lignes est l'ORDRE DE TRAVAIL recommande, pas un tri : la feuille du graphe
d'abord, le gros morceau ensuite, les deux devices en dernier — voir `Documentation/Etat/TODO.md`.

| DLL | fonctions | lignes | employes | cloture | lignes | dont CRT | part | portees |
|---|---:|---:|---:|---:|---:|---:|---:|---:|
| `wsyskern` | 1 386 | 62 159 | — | — | — | — | — | — |
| `winsys3d` | 604 | 27 092 | — | — | — | — | — | — |
| `3dhelp` | 97 | 3 518 | — | — | — | — | — | — |
| `gldhelp` | 54 | 1 141 | — | — | — | — | — | — |
| `playst` | 1 977 | 41 713 | — | — | — | — | — | — |
| **total** | | **135 623** | | | **0** | | **0 %** | |

La colonne **cloture** compte les fonctions a transcrire, **portees** celles qui le sont deja, **lignes** leur taille cumulee en lignes de
Ghidra, et **dont CRT** ce que la cloture traverse de bibliotheque standard liee statiquement — qui
se rebranche au lieu de se transcrire, et qui est deja retranche du devis. La colonne **part**
rapporte le devis a la taille de la DLL entiere.


---

## `wsyskern` — 244 exports, 1 386 fonctions, 62 159 lignes

La feuille du graphe : tout le monde en depend, elle ne depend d'aucune des notres. Services
generaux — bitmaps et DIB, gamma, tableaux de chaines, memoire.

**Aucun point d'entree employe.** Ni `Blaze.exe` ni la reconstruction n'importe quoi que ce soit
de cette DLL. Rien a transcrire tant que cela reste vrai.


---

## `winsys3d` — 830 exports, 604 fonctions, 27 092 lignes

Le portage sur Win32 de la BIBLIOTHEQUE PSY-Q de la PlayStation : libgpu (liste d'affichage),
libgte (le coprocesseur geometrique), le son, la carte memoire, le clavier. C'est le vrai sujet du
chantier.

**Aucun point d'entree employe.** Ni `Blaze.exe` ni la reconstruction n'importe quoi que ce soit
de cette DLL. Rien a transcrire tant que cela reste vrai.

### Appeles par `Blaze.c`, pas encore importes (3)

Ces ordinaux apparaissent dans la decompilation du jeu mais aucun `[DllImport]` ne les
declare : la fonction appelante n'est pas encore transcrite. Ils rejoindront la
cloture le jour ou elle le sera — c'est le compteur qui dit que ce releve n'est pas
un plafond.

| ordinal | adresse | fonction | lignes |
|---:|---|---|---:|
| 28 | `0x00a046f0` | `FUN_00a046f0` | 13 |
| 334 | `0x00a262a0` | `FUN_00a262a0` | 15 |
| 336 | `0x00a26300` | `FUN_00a26300` | 13 |


---

## `3dhelp` — 1 export, 97 fonctions, 3 518 lignes

Le device DirectDraw / Direct3D, charge dynamiquement par winsys3d (`LoadLibraryA` +
`GetProcAddress("Create3DDevice")`, winsys3d.c:5854). UN SEUL EXPORT. Deja remplace en pratique
par `Host/SyntheticDevice.cs`.

**Aucun point d'entree employe, et c'est trompeur.** `Create3DDevice` est son unique export ; tout
le reste de la DLL s'appelle par la VTABLE du device qu'il rend, un chemin que ce releve ne suit
pas. `winsys3d` y adresse le rendu entier. Ce que la reconstruction en fait est deja tranche
ailleurs : `Host/SyntheticDevice.cs` rend son propre device a la place, et `Host/DeviceShim.cs`
detourne les slots du chemin chaud vers Direct3D 11. Le jour ou `winsys3d` sera en C#, plus
personne ne chargera cette DLL — elle sortira d'elle-meme.


---

## `gldhelp` — 1 export, 54 fonctions, 1 141 lignes

Le meme contrat que 3dhelp, mais sur Glide — les cartes 3Dfx Voodoo. UN SEUL EXPORT, et le corps
n'est guere que des relais vers `glide2x.dll`, qui n'est pas livree. Inatteignable sur une machine
d'aujourd'hui.

**Aucun point d'entree employe.** Meme forme que `3dhelp` — un seul export, le reste par vtable
— mais sans l'equivalent de `SyntheticDevice` parce que rien ne l'atteint : `glide2x.dll` n'est
pas livree avec le jeu et aucune machine d'aujourd'hui ne porte de carte 3Dfx. Transcriptible a
peu de frais si on veut la completude, jamais executable.


---

## `playst` — 758 exports, 1 977 fonctions, 41 713 lignes

Le SDK PSY-Q de Sony, porte sur PC : les noms d'exports sont ceux de la documentation PlayStation
(`GsSortObject5`, `RotTransPers`, `SsSeqOpenJ`, `CdSearchFile`, `card_read`). Selon toute
vraisemblance la generation qui a precede winsys3d, restee sur le CD.

**Aucun point d'entree employe, et personne ne la charge.** Ni `Blaze.exe` ni `winsys3d.dll` ne
nomme `playst.dll` — voir le graphe de dependances plus haut, releve dans les binaires. Elle
n'exporte pas non plus `Create3DDevice`, donc elle ne peut pas servir de device. **Rien a
transcrire**, et le jour ou cela change, ce document le dira.

---

## Ce que ce releve ne dit pas

**Il ne dit pas que la cloture est complete.** Elle suit les appels que Ghidra a su nommer. Un appel
par POINTEUR — la vtable d'un device, un rappel installe par `Ordinal_790` — n'y figure pas, et il y
en a. La cloture est donc un PLANCHER : ce qu'il faut transcrire au minimum, jamais un plafond. Le
chemin d'affichage, en particulier, sort de `winsys3d` par la vtable de `3dhelp` et cette arete est
invisible ici.

**Il ne dit pas qu'un corps hors cloture est mort.** Il dit que rien de ce que la reconstruction
importe AUJOURD'HUI n'y mene. Un ecran non encore atteint peut faire apparaitre un ordinal de plus —
la liste « appeles par `Blaze.c`, pas encore importes » de chaque section est precisement ce
compteur-la.

**Il ne dit pas qu'une fonction implementee est juste.** `implemente` lit un commentaire de
tracabilite, pas un comportement. C'est la meme lecture que `implemente` dans
`GestionnairesEntites.md` : le corps existe, rien de plus.

**Il ne compte pas `ir41_32.ax`.** Ce n'est pas une DLL du jeu mais le codec Indeo 4.1 d'Intel,
livre avec, et il n'en existe aucune decompilation dans le depot. S'en passer, ce n'est pas
transcrire du code : c'est implementer un codec video. Le chantier est reel, mais il est autre —
voir `Documentation/DLL/ir41_32.md`.

**Une fonction se transcrit ENTIERE.** Ne transcrire d'une DLL que ce qu'on en atteint est un choix
delibere et ce document le sert. Ne transcrire d'une FONCTION que la branche deja vue en jeu n'en
est pas un : c'est ainsi qu'on obtient un corps qui compile, qui tourne, et qui repond faux sur le
cas d'a cote. Partielle a l'echelle de la DLL, complete a l'echelle du corps.
