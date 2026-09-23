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
| `wsyskern` | 1 386 | 62 159 | 2 | 92 | 4 088 | 1 926 | 7 % | 58 |
| `winsys3d` | 604 | 27 092 | 1 | 247 | 13 423 | 1 181 | 50 % | 247 |
| `3dhelp` | 97 | 3 518 | — | — | — | — | — | — |
| `gldhelp` | 54 | 1 141 | — | — | — | — | — | — |
| `playst` | 1 977 | 41 713 | — | — | — | — | — | — |
| **total** | | **135 623** | | | **17 511** | | **12 %** | |

La colonne **cloture** compte les fonctions a transcrire, **portees** celles qui le sont deja, **lignes** leur taille cumulee en lignes de
Ghidra, et **dont CRT** ce que la cloture traverse de bibliotheque standard liee statiquement — qui
se rebranche au lieu de se transcrire, et qui est deja retranche du devis. La colonne **part**
rapporte le devis a la taille de la DLL entiere.


---

## `wsyskern` — 244 exports, 1 386 fonctions, 62 159 lignes

La feuille du graphe : tout le monde en depend, elle ne depend d'aucune des notres. Services
generaux — bitmaps et DIB, gamma, tableaux de chaines, memoire.

**2 points d'entree employes**, dont la cloture d'appels couvre **137 fonctions**,
soit **6 014 lignes de Ghidra sur 62 159**. Retranchee la CRT liee statiquement — 45 fonctions,
1 926 lignes, qui se rebranchent au lieu de se transcrire —, le devis est de **4 088 lignes,
7 % de la DLL**. Le reste n'est atteint par rien de ce que la reconstruction importe
aujourd'hui.

**56 corps entrent dans la cloture par la reconstruction et non par un `[DllImport]`** :
ils sont deja transcrits, donc leur import a disparu. Ils restent comptes — c'est ce qui
empeche le devis de retrecir a mesure qu'on avance.

### Les points d'entree employes (2)

| ordinal | nom d'export | adresse | fonction | lignes | implemente |
|---:|---|---|---|---:|---|
| 89 | `Free` | `0x1000bc30` | `Free` | 24 | oui |
| 124 | `Malloc` | `0x1000bbc0` | `Malloc` | 29 | oui |

### La cloture — ce qu'il faut transcrire (92 fonctions, 4 088 lignes)

Triee par taille decroissante : c'est le devis, du plus cher au moins cher. **Les lignes
sont des lignes de Ghidra**, declarations de variables comprises — une mesure de cout,
pas un compte de lignes C# a ecrire. La colonne **appelants** compte les corps de la
cloture qui menent a celui-la ; une fonction a zero appelant est une racine, appelee
directement par la reconstruction, et son nom est en gras.

| adresse | fonction | lignes | appelants | implemente |
|---|---|---:|---:|---|
| `0x1001ef00` | `FUN_1001ef00` | 413 | 1 | **non** |
| `0x10003bb0` | **`FUN_10003bb0`** | 157 | 2 | oui |
| `0x10020d20` | `FUN_10020d20` | 153 | 1 | **non** |
| `0x10013930` | `FUN_10013930` | 124 | 1 | **non** |
| `0x10013160` | **`FUN_10013160`** | 105 | 0 | oui |
| `0x10003870` | **`FUN_10003870`** | 99 | 1 | oui |
| `0x10004060` | **`FUN_10004060`** | 99 | 1 | oui |
| `0x10003480` | **`FUN_10003480`** | 97 | 2 | oui |
| `0x100039a0` | **`FUN_100039a0`** | 94 | 1 | oui |
| `0x10017670` | `FUN_10017670` | 86 | 1 | **non** |
| `0x1001c990` | `FUN_1001c990` | 82 | 1 | **non** |
| `0x10006580` | **`DIBCopyBlock`** | 77 | 0 | oui |
| `0x10010230` | **`LoadFile`** | 77 | 1 | oui |
| `0x100077c0` | **`decrypt`** | 76 | 1 | oui |
| `0x10014730` | `FUN_10014730` | 75 | 1 | **non** |
| `0x10017e80` | **`FUN_10017e80`** | 73 | 1 | oui |
| `0x1000fe80` | **`FUN_1000fe80`** | 65 | 1 | oui |
| `0x10012ef0` | **`FUN_10012ef0`** | 64 | 0 | oui |
| `0x10022570` | **`FUN_10022570`** | 60 | 1 | oui |
| `0x1001e770` | `FUN_1001e770` | 59 | 1 | **non** |
| `0x10020040` | `FUN_10020040` | 56 | 1 | **non** |
| `0x1000bac0` | **`LoadBitmapEx`** | 55 | 0 | oui |
| `0x10013060` | `FUN_10013060` | 55 | 1 | **non** |
| `0x10010500` | **`LoadBitmapFile`** | 54 | 0 | oui |
| `0x10009f00` | **`CreateArray`** | 52 | 0 | oui |
| `0x100036d0` | **`FUN_100036d0`** | 47 | 1 | oui |
| `0x10020fe0` | `FUN_10020fe0` | 47 | 1 | **non** |
| `0x10006ab0` | **`ProjectionMatrix`** | 46 | 0 | oui |
| `0x10003620` | **`FUN_10003620`** | 45 | 6 | oui |
| `0x10011f50` | **`strtolong`** | 45 | 1 | oui |
| `0x1001c8d0` | `FUN_1001c8d0` | 44 | 1 | **non** |
| `0x100033d0` | **`FUN_100033d0`** | 40 | 2 | oui |
| `0x1001b7f0` | `FUN_1001b7f0` | 40 | 1 | **non** |
| `0x100061c0` | **`Gamma`** | 39 | 0 | oui |
| `0x10003260` | **`FUN_10003260`** | 37 | 1 | oui |
| `0x10003b10` | **`FUN_10003b10`** | 37 | 1 | oui |
| `0x10003e70` | **`Unpack`** | 37 | 1 | oui |
| `0x1000bc90` | **`Strdup`** | 37 | 0 | oui |
| `0x1000c130` | **`PaletteFromDIB`** | 37 | 1 | oui |
| `0x100037b0` | **`FUN_100037b0`** | 35 | 1 | oui |
| `0x10007050` | **`ScaleMatrix`** | 35 | 0 | oui |
| `0x1001c7e0` | `FUN_1001c7e0` | 34 | 1 | **non** |
| `0x100178d0` | `FUN_100178d0` | 33 | 4 | **non** |
| `0x1001c700` | `FUN_1001c700` | 33 | 1 | **non** |
| `0x1001c770` | `FUN_1001c770` | 33 | 1 | **non** |
| `0x10008950` | **`WarningMessage`** | 32 | 2 | oui |
| `0x10012fd0` | `FUN_10012fd0` | 32 | 1 | **non** |
| `0x10006a40` | **`FUN_10006a40`** | 30 | 0 | oui |
| `0x10004400` | `FUN_10004400` | 29 | 1 | **non** |
| `0x10004a50` | `FUN_10004a50` | 29 | 1 | **non** |
| `0x1000bbc0` | **`Malloc`** | 29 | 13 | oui |
| `0x100173b0` | **`FUN_100173b0`** | 29 | 1 | oui |
| `0x100046c0` | `FUN_100046c0` | 28 | 1 | **non** |
| `0x10003450` | **`FUN_10003450`** | 26 | 1 | oui |
| `0x1001ff70` | `FUN_1001ff70` | 25 | 1 | **non** |
| `0x100088d0` | **`ErrorMessage`** | 24 | 3 | oui |
| `0x1000bc30` | **`Free`** | 24 | 9 | oui |
| `0x10012020` | **`FUN_10012020`** | 24 | 0 | oui |
| `0x100224d0` | **`FUN_100224d0`** | 23 | 1 | oui |
| `0x10022520` | **`FUN_10022520`** | 22 | 1 | oui |
| `0x100178a0` | `FUN_100178a0` | 21 | 1 | **non** |
| `0x1001c8b0` | `FUN_1001c8b0` | 21 | 1 | **non** |
| `0x1000a190` | **`LoadArray`** | 20 | 0 | oui |
| `0x1001c880` | `FUN_1001c880` | 20 | 1 | **non** |
| `0x1001ff40` | `FUN_1001ff40` | 20 | 3 | **non** |
| `0x10003340` | **`FUN_10003340`** | 19 | 2 | oui |
| `0x1000a050` | **`ArrayToString`** | 19 | 0 | oui |
| `0x1001ffe0` | `FUN_1001ffe0` | 19 | 2 | **non** |
| `0x10009fe0` | **`DeleteArray`** | 18 | 0 | oui |
| `0x10020010` | `FUN_10020010` | 17 | 1 | **non** |
| `0x1000bbac` | **`FUN_1000bbac`** | 16 | 1 | oui |
| `0x100033a0` | **`FUN_100033a0`** | 15 | 2 | oui |
| `0x100035e0` | **`FUN_100035e0`** | 15 | 1 | oui |
| `0x10003600` | **`FUN_10003600`** | 15 | 1 | oui |
| `0x10022550` | **`FUN_10022550`** | 15 | 2 | oui |
| `0x10003e40` | **`FUN_10003e40`** | 15 | 4 | oui |
| `0x10003316` | `FUN_10003316` | 14 | 1 | **non** |
| `0x10003f10` | **`FUN_10003f10`** | 14 | 3 | oui |
| `0x10004262` | `FUN_10004262` | 14 | 1 | **non** |
| `0x10010359` | `FUN_10010359` | 14 | 1 | **non** |
| `0x1001c8a0` | `FUN_1001c8a0` | 14 | 1 | **non** |
| `0x100035f0` | **`FUN_100035f0`** | 13 | 1 | oui |
| `0x10003efd` | `FUN_10003efd` | 13 | 1 | **non** |
| `0x10008a30` | **`outofmemory`** | 13 | 1 | oui |
| `0x10008a50` | **`filenotfound`** | 13 | 2 | oui |
| `0x10008a70` | **`warnfilenotfound`** | 13 | 2 | oui |
| `0x10008a90` | **`fileerror`** | 13 | 1 | oui |
| `0x10008ab0` | **`warnfileerror`** | 13 | 1 | oui |
| `0x10006a90` | **`ZeroMatrix`** | 12 | 1 | oui |
| `0x10006aa0` | **`IdentityMatrix`** | 12 | 1 | oui |
| `0x10013bc0` | `FUN_10013bc0` | 12 | 1 | **non** |
| `0x1001cb60` | `FUN_1001cb60` | 12 | 1 | **non** |

### La CRT liee statiquement (45 fonctions, 1 926 lignes) — hors devis

La DLL embarque sa bibliotheque standard, et la cloture la traverse. Ghidra la nomme
depuis sa base de signatures — d'ou le souligne en tete. **Rien de tout cela ne se
transcrit** : `__alldiv`, `__allmul` et `__allshr` sont l'arithmetique 64 bits que le
compilateur de 1998 appelait et qu'un `long` C# fait nativement ; `__ftol` est une
conversion ; `__local_unwind2` et `__amsg_exit` sont le deroulage SEH ; le reste est
`string.h`, deja porte dans `Platform/Crt.cs`.

| adresse | fonction | lignes |
|---|---|---:|
| `0x1001c390` | `FUN_1001c390` | 196 |
| `0x10018060` | `FUN_10018060` | 153 |
| `0x100183a0` | `FUN_100183a0` | 147 |
| `0x1001b360` | `FUN_1001b360` | 118 |
| `0x10012ac0` | `_strncpy` | 115 |
| `0x1001b5a0` | `FUN_1001b5a0` | 112 |
| `0x1001b000` | `FUN_1001b000` | 79 |
| `0x10012690` | `_strchr` | 77 |
| `0x1001cf40` | `FUN_1001cf40` | 61 |
| `0x1001b1d0` | `FUN_1001b1d0` | 59 |
| `0x100188c0` | `FUN_100188c0` | 57 |
| `0x100132e0` | `FUN_100132e0` | 44 |
| `0x100186d0` | `FUN_100186d0` | 41 |
| `0x1001233e` | `__local_unwind2` | 40 |
| `0x1001fc60` | `FUN_1001fc60` | 35 |
| `0x10015a10` | `FUN_10015a10` | 34 |
| `0x10014290` | `FUN_10014290` | 33 |
| `0x1001b2a0` | `FUN_1001b2a0` | 30 |
| `0x100174f0` | `FUN_100174f0` | 29 |
| `0x10017fa0` | `FUN_10017fa0` | 29 |
| `0x100144f0` | `FUN_100144f0` | 27 |
| `0x100142e0` | `FUN_100142e0` | 24 |
| `0x100187b0` | `FUN_100187b0` | 23 |
| `0x100187e0` | `FUN_100187e0` | 23 |
| `0x10018860` | `FUN_10018860` | 23 |
| `0x10018780` | `FUN_10018780` | 22 |
| `0x1001b170` | `FUN_1001b170` | 22 |
| `0x1001b300` | `FUN_1001b300` | 22 |
| `0x10014f60` | `__amsg_exit` | 21 |
| `0x10017f50` | `FUN_10017f50` | 21 |
| `0x1001af60` | `FUN_1001af60` | 19 |
| `0x10012200` | `__ftol` | 18 |
| `0x1001c350` | `FUN_1001c350` | 18 |
| `0x100123d2` | `FUN_100123d2` | 17 |
| `0x10014710` | `FUN_10014710` | 14 |
| `0x100159f0` | `FUN_100159f0` | 13 |
| `0x100189d0` | `FUN_100189d0` | 13 |
| `0x100189e0` | `FUN_100189e0` | 13 |
| `0x10018a00` | `FUN_10018a00` | 13 |
| `0x10018a30` | `FUN_10018a30` | 13 |
| `0x10014270` | `FUN_10014270` | 12 |
| `0x10017570` | `FUN_10017570` | 12 |
| `0x10018820` | `FUN_10018820` | 12 |
| `0x10017f30` | `FUN_10017f30` | 11 |
| `0x1001e590` | `FUN_1001e590` | 11 |

### Ce qui sort de la DLL (49 appels distincts)

Les appels que la cloture adresse en dehors de la DLL. Ils ne se transcrivent pas — ils
se rebranchent : sur `Platform/Win32.cs` pour Win32, sur `Platform/Crt.cs` pour la CRT,
et sur la reconstruction de la DLL soeur quand la colonne **chez** en nomme une. Cette
derniere colonne est l'argument de l'ordre de travail : une DLL dont les appels
sortants retombent chez une soeur se transcrit APRES elle, sinon le portage s'appuie
sur du natif qu'il faudra rebrancher deux fois.

| appel | sites | chez |
|---|---:|---|
| `CloseHandle` | 3 | — |
| `EnterCriticalSection` | 3 | — |
| `GetFileSize` | 3 | — |
| `HeapAlloc` | 3 | — |
| `HeapFree` | 3 | — |
| `LeaveCriticalSection` | 3 | — |
| `ROUND` | 3 | — |
| `ReadFile` | 3 | — |
| `VirtualFree` | 3 | — |
| `CreateFileA` | 2 | — |
| `MessageBoxA` | 2 | — |
| `MultiByteToWideChar` | 2 | — |
| `VirtualAlloc` | 2 | — |
| `wvsprintfA` | 2 | — |
| `CreateDIBitmap` | 1 | — |
| `CreatePalette` | 1 | — |
| `FindResourceA` | 1 | — |
| `FreeResource` | 1 | — |
| `GetCurrentThreadId` | 1 | — |
| `GetDC` | 1 | — |
| `GetLastError` | 1 | — |
| `GetModuleFileNameA` | 1 | — |
| `GetProcAddress` | 1 | — |
| `GetStdHandle` | 1 | — |
| `GetStringTypeA` | 1 | — |
| `GetStringTypeW` | 1 | — |
| `InitializeCriticalSection` | 1 | — |
| `InterlockedDecrement` | 1 | — |
| `InterlockedIncrement` | 1 | — |
| `LCMapStringA` | 1 | — |
| `LCMapStringW` | 1 | — |
| `LoadLibraryA` | 1 | — |
| `LoadResource` | 1 | — |
| `LockResource` | 1 | — |
| `RaiseException` | 1 | — |
| `RealizePalette` | 1 | — |
| `ReleaseDC` | 1 | — |
| `SelectPalette` | 1 | — |
| `SetFilePointer` | 1 | — |
| `SetLastError` | 1 | — |
| `TlsGetValue` | 1 | — |
| `TlsSetValue` | 1 | — |
| `WideCharToMultiByte` | 1 | — |
| `WriteFile` | 1 | — |
| `f2xm1` | 1 | — |
| `fcos` | 1 | — |
| `fscale` | 1 | — |
| `fsin` | 1 | — |
| `log2` | 1 | — |


---

## `winsys3d` — 830 exports, 604 fonctions, 27 092 lignes

Le portage sur Win32 de la BIBLIOTHEQUE PSY-Q de la PlayStation : libgpu (liste d'affichage),
libgte (le coprocesseur geometrique), le son, la carte memoire, le clavier. C'est le vrai sujet du
chantier.

**1 points d'entree employes**, dont la cloture d'appels couvre **272 fonctions**,
soit **14 604 lignes de Ghidra sur 27 092**. Retranchee la CRT liee statiquement — 25 fonctions,
1 181 lignes, qui se rebranchent au lieu de se transcrire —, le devis est de **13 423 lignes,
50 % de la DLL**. Le reste n'est atteint par rien de ce que la reconstruction importe
aujourd'hui.

**246 corps entrent dans la cloture par la reconstruction et non par un `[DllImport]`** :
ils sont deja transcrits, donc leur import a disparu. Ils restent comptes — c'est ce qui
empeche le devis de retrecir a mesure qu'on avance.

### Les points d'entree employes (1)

| ordinal | nom d'export | adresse | fonction | lignes | implemente |
|---:|---|---|---|---:|---|
| 701 | — | `0x00a061b0` | `FUN_00a061b0` | 59 | oui |

### La cloture — ce qu'il faut transcrire (247 fonctions, 13 423 lignes)

Triee par taille decroissante : c'est le devis, du plus cher au moins cher. **Les lignes
sont des lignes de Ghidra**, declarations de variables comprises — une mesure de cout,
pas un compte de lignes C# a ecrire. La colonne **appelants** compte les corps de la
cloture qui menent a celui-la ; une fonction a zero appelant est une racine, appelee
directement par la reconstruction, et son nom est en gras.

| adresse | fonction | lignes | appelants | implemente |
|---|---|---:|---:|---|
| `0x00a13400` | **`FUN_00a13400`** | 540 | 1 | oui |
| `0x00a03460` | **`FUN_00a03460`** | 316 | 1 | oui |
| `0x00a0c4f0` | **`FUN_00a0c4f0`** | 259 | 1 | oui |
| `0x00a142a0` | **`FUN_00a142a0`** | 235 | 1 | oui |
| `0x00a26b20` | **`FUN_00a26b20`** | 217 | 1 | oui |
| `0x00a049c0` | **`FUN_00a049c0`** | 197 | 2 | oui |
| `0x00a06420` | **`FUN_00a06420`** | 186 | 1 | oui |
| `0x00a22010` | **`FUN_00a22010`** | 181 | 0 | oui |
| `0x00a226c0` | **`FUN_00a226c0`** | 170 | 0 | oui |
| `0x00a010d0` | **`FUN_00a010d0`** | 164 | 1 | oui |
| `0x00a03980` | **`FUN_00a03980`** | 150 | 1 | oui |
| `0x00a053e0` | **`FUN_00a053e0`** | 143 | 0 | oui |
| `0x00a0aa90` | **`FUN_00a0aa90`** | 132 | 1 | oui |
| `0x00a271e0` | **`FUN_00a271e0`** | 129 | 1 | oui |
| `0x00a09980` | **`FUN_00a09980`** | 128 | 1 | oui |
| `0x00a0e0c0` | **`FUN_00a0e0c0`** | 127 | 1 | oui |
| `0x00a08080` | **`FUN_00a08080`** | 126 | 1 | oui |
| `0x00a03110` | **`FUN_00a03110`** | 123 | 1 | oui |
| `0x00a24000` | **`FUN_00a24000`** | 122 | 1 | oui |
| `0x00a01910` | **`Setup3DDevice`** | 118 | 0 | oui |
| `0x00a20630` | **`FUN_00a20630`** | 116 | 0 | oui |
| `0x00a12350` | **`FUN_00a12350`** | 115 | 1 | oui |
| `0x00a078c0` | **`FUN_00a078c0`** | 112 | 1 | oui |
| `0x00a0b390` | **`FUN_00a0b390`** | 111 | 1 | oui |
| `0x00a26f20` | **`FUN_00a26f20`** | 111 | 1 | oui |
| `0x00a094d0` | **`FUN_00a094d0`** | 104 | 1 | oui |
| `0x00a0a1a0` | **`FUN_00a0a1a0`** | 104 | 1 | oui |
| `0x00a08850` | **`FUN_00a08850`** | 100 | 1 | oui |
| `0x00a101c0` | **`FUN_00a101c0`** | 100 | 1 | oui |
| `0x00a0bdf0` | **`FUN_00a0bdf0`** | 96 | 1 | oui |
| `0x00a0c110` | **`FUN_00a0c110`** | 96 | 1 | oui |
| `0x00a11ec0` | **`FUN_00a11ec0`** | 96 | 1 | oui |
| `0x00a0b930` | **`FUN_00a0b930`** | 93 | 3 | oui |
| `0x00a09070` | **`FUN_00a09070`** | 92 | 3 | oui |
| `0x00a0b000` | **`FUN_00a0b000`** | 91 | 3 | oui |
| `0x00a08c80` | **`FUN_00a08c80`** | 90 | 3 | oui |
| `0x00a0a670` | **`FUN_00a0a670`** | 90 | 3 | oui |
| `0x00a0ff50` | **`FUN_00a0ff50`** | 89 | 1 | oui |
| `0x00a08510` | **`FUN_00a08510`** | 88 | 3 | oui |
| `0x00a09e60` | **`FUN_00a09e60`** | 88 | 3 | oui |
| `0x00a27e00` | **`FUN_00a27e00`** | 88 | 1 | oui |
| `0x00a0ecf0` | **`FUN_00a0ecf0`** | 87 | 1 | oui |
| `0x00a07d80` | **`FUN_00a07d80`** | 86 | 2 | oui |
| `0x00a0fb40` | **`FUN_00a0fb40`** | 83 | 1 | oui |
| `0x00a024e0` | **`FUN_00a024e0`** | 82 | 1 | oui |
| `0x00a02380` | **`FUN_00a02380`** | 81 | 1 | oui |
| `0x00a0dde0` | **`FUN_00a0dde0`** | 81 | 1 | oui |
| `0x00a13010` | **`FUN_00a13010`** | 80 | 1 | oui |
| `0x00a0d4b0` | **`FUN_00a0d4b0`** | 76 | 1 | oui |
| `0x00a0da80` | **`FUN_00a0da80`** | 76 | 1 | oui |
| `0x00a0eaf0` | **`FUN_00a0eaf0`** | 74 | 1 | oui |
| `0x00a0f940` | **`FUN_00a0f940`** | 74 | 1 | oui |
| `0x00a107e0` | **`FUN_00a107e0`** | 72 | 2 | oui |
| `0x00a128d0` | **`FUN_00a128d0`** | 72 | 1 | oui |
| `0x00a12c70` | **`FUN_00a12c70`** | 72 | 1 | oui |
| `0x00a26e10` | **`FUN_00a26e10`** | 72 | 1 | oui |
| `0x00a11a90` | **`FUN_00a11a90`** | 71 | 1 | oui |
| `0x00a116e0` | **`FUN_00a116e0`** | 66 | 1 | oui |
| `0x00a13220` | **`FUN_00a13220`** | 65 | 1 | oui |
| `0x00a02e20` | **`FUN_00a02e20`** | 64 | 1 | oui |
| `0x00a02f50` | **`CreateTranslationTables`** | 64 | 1 | oui |
| `0x00a0d3d0` | **`FUN_00a0d3d0`** | 64 | 1 | oui |
| `0x00a0d990` | **`FUN_00a0d990`** | 64 | 1 | oui |
| `0x00a0f570` | **`FUN_00a0f570`** | 64 | 2 | oui |
| `0x00a10df0` | **`FUN_00a10df0`** | 64 | 1 | oui |
| `0x00a11310` | **`FUN_00a11310`** | 64 | 1 | oui |
| `0x00a1df00` | **`FUN_00a1df00`** | 62 | 1 | oui |
| `0x00a03f80` | **`FUN_00a03f80`** | 61 | 5 | oui |
| `0x00a03c80` | **`FUN_00a03c80`** | 60 | 15 | oui |
| `0x00a044c0` | **`FUN_00a044c0`** | 60 | 0 | oui |
| `0x00a061b0` | **`FUN_00a061b0`** | 59 | 0 | oui |
| `0x00a0e970` | **`FUN_00a0e970`** | 59 | 1 | oui |
| `0x00a0f090` | **`FUN_00a0f090`** | 59 | 1 | oui |
| `0x00a0d310` | **`FUN_00a0d310`** | 58 | 1 | oui |
| `0x00a0d8b0` | **`FUN_00a0d8b0`** | 58 | 1 | oui |
| `0x00a109c0` | **`FUN_00a109c0`** | 58 | 1 | oui |
| `0x00a10690` | **`FUN_00a10690`** | 57 | 1 | oui |
| `0x00a11cd0` | **`FUN_00a11cd0`** | 57 | 1 | oui |
| `0x00a126d0` | **`FUN_00a126d0`** | 57 | 1 | oui |
| `0x00a12ac0` | **`FUN_00a12ac0`** | 57 | 1 | oui |
| `0x00a12e60` | **`FUN_00a12e60`** | 57 | 1 | oui |
| `0x00a267d0` | **`FUN_00a267d0`** | 57 | 1 | oui |
| `0x00a268c0` | **`FUN_00a268c0`** | 57 | 1 | oui |
| `0x00a04560` | **`FUN_00a04560`** | 56 | 0 | oui |
| `0x00a0d730` | **`FUN_00a0d730`** | 56 | 1 | oui |
| `0x00a0dce0` | **`FUN_00a0dce0`** | 56 | 1 | oui |
| `0x00a05c60` | **`FUN_00a05c60`** | 55 | 3 | oui |
| `0x00a0e7f0` | **`FUN_00a0e7f0`** | 53 | 1 | oui |
| `0x00a0f3f0` | **`FUN_00a0f3f0`** | 53 | 1 | oui |
| `0x00a10ca0` | **`FUN_00a10ca0`** | 53 | 1 | oui |
| `0x00a111c0` | **`FUN_00a111c0`** | 53 | 1 | oui |
| `0x00a118e0` | **`FUN_00a118e0`** | 53 | 1 | oui |
| `0x00a12190` | **`FUN_00a12190`** | 53 | 1 | oui |
| `0x00a02870` | **`FUN_00a02870`** | 52 | 0 | oui |
| `0x00a02e40` | **`FUN_00a02e40`** | 52 | 0 | oui |
| `0x00a05df0` | **`FUN_00a05df0`** | 52 | 0 | oui |
| `0x00a0d260` | **`FUN_00a0d260`** | 52 | 1 | oui |
| `0x00a0d7f0` | **`FUN_00a0d7f0`** | 52 | 1 | oui |
| `0x00a10b60` | **`FUN_00a10b60`** | 51 | 1 | oui |
| `0x00a264f0` | **`FUN_00a264f0`** | 51 | 10 | oui |
| `0x00a27430` | **`FUN_00a27430`** | 51 | 1 | oui |
| `0x00a0e6a0` | **`FUN_00a0e6a0`** | 50 | 1 | oui |
| `0x00a0ef40` | **`FUN_00a0ef40`** | 50 | 1 | oui |
| `0x00a0f780` | **`FUN_00a0f780`** | 50 | 1 | oui |
| `0x00a104c0` | **`FUN_00a104c0`** | 50 | 1 | oui |
| `0x00a11000` | **`FUN_00a11000`** | 50 | 1 | oui |
| `0x00a11520` | **`FUN_00a11520`** | 50 | 1 | oui |
| `0x00a27650` | **`FUN_00a27650`** | 49 | 1 | oui |
| `0x00a0d6a0` | **`FUN_00a0d6a0`** | 48 | 1 | oui |
| `0x00a0dc30` | **`FUN_00a0dc30`** | 48 | 1 | oui |
| `0x00a05d50` | **`FUN_00a05d50`** | 47 | 1 | oui |
| `0x00a0e580` | **`FUN_00a0e580`** | 47 | 1 | oui |
| `0x00a0cd20` | **`FUN_00a0cd20`** | 46 | 3 | oui |
| `0x00a0f260` | **`FUN_00a0f260`** | 46 | 1 | oui |
| `0x00a0fdc0` | **`FUN_00a0fdc0`** | 46 | 1 | oui |
| `0x00a1fba0` | **`FUN_00a1fba0`** | 45 | 2 | oui |
| `0x00a26670` | **`FUN_00a26670`** | 45 | 1 | oui |
| `0x00a26720` | **`FUN_00a26720`** | 45 | 1 | oui |
| `0x00a26470` | **`FUN_00a26470`** | 44 | 0 | oui |
| `0x00a05f50` | **`FUN_00a05f50`** | 43 | 0 | oui |
| `0x00a01f00` | **`FUN_00a01f00`** | 42 | 1 | oui |
| `0x00a05a00` | **`FUN_00a05a00`** | 42 | 0 | oui |
| `0x00a01d70` | **`FUN_00a01d70`** | 41 | 1 | oui |
| `0x00a1d8b0` | **`FUN_00a1d8b0`** | 41 | 1 | oui |
| `0x00a23cf0` | **`FUN_00a23cf0`** | 41 | 1 | oui |
| `0x00a026e0` | **`FUN_00a026e0`** | 40 | 0 | oui |
| `0x00a23dd0` | **`FUN_00a23dd0`** | 40 | 1 | oui |
| `0x00a25910` | **`FUN_00a25910`** | 40 | 0 | oui |
| `0x00a03e70` | **`FUN_00a03e70`** | 39 | 0 | oui |
| `0x00a05e90` | **`FUN_00a05e90`** | 39 | 0 | oui |
| `0x00a06a40` | **`FUN_00a06a40`** | 39 | 1 | oui |
| `0x00a0d630` | **`FUN_00a0d630`** | 38 | 1 | oui |
| `0x00a0dba0` | **`FUN_00a0dba0`** | 38 | 1 | oui |
| `0x00a26220` | **`FUN_00a26220`** | 38 | 0 | oui |
| `0x00a0d5c0` | **`FUN_00a0d5c0`** | 36 | 1 | oui |
| `0x00a1f8f0` | **`FUN_00a1f8f0`** | 36 | 3 | oui |
| `0x00a21040` | **`FUN_00a21040`** | 35 | 0 | oui |
| `0x00a26490` | **`FUN_00a26490`** | 35 | 1 | oui |
| `0x00a020a0` | **`FUN_00a020a0`** | 34 | 1 | oui |
| `0x00a05bd0` | **`FUN_00a05bd0`** | 34 | 0 | oui |
| `0x00a02490` | **`FUN_00a02490`** | 33 | 0 | oui |
| `0x00a02e10` | **`FUN_00a02e10`** | 33 | 0 | oui |
| `0x00a27770` | **`FUN_00a27770`** | 33 | 2 | oui |
| `0x00a04270` | **`FUN_00a04270`** | 31 | 2 | oui |
| `0x00a040d0` | **`FUN_00a040d0`** | 30 | 0 | oui |
| `0x00a05810` | **`SetShift`** | 30 | 1 | oui |
| `0x00a05b50` | **`FUN_00a05b50`** | 30 | 0 | oui |
| `0x00a07ca0` | **`FUN_00a07ca0`** | 30 | 3 | oui |
| `0x00a269b0` | **`FUN_00a269b0`** | 30 | 1 | oui |
| `0x00a27730` | **`FUN_00a27730`** | 30 | 1 | oui |
| `0x00a278c0` | **`FUN_00a278c0`** | 30 | 2 | oui |
| `0x00a05260` | **`SetTimingMode`** | 29 | 0 | oui |
| `0x00a1cff0` | **`FUN_00a1cff0`** | 29 | 1 | oui |
| `0x00a275e0` | **`FUN_00a275e0`** | 29 | 1 | oui |
| `0x00a06070` | **`FUN_00a06070`** | 28 | 1 | oui |
| `0x00a069c0` | **`ChangeResolution`** | 28 | 0 | oui |
| `0x00a03db0` | **`FUN_00a03db0`** | 27 | 33 | oui |
| `0x00a06160` | **`FUN_00a06160`** | 27 | 0 | oui |
| `0x00a27180` | **`FUN_00a27180`** | 27 | 1 | oui |
| `0x00a27560` | **`FUN_00a27560`** | 27 | 1 | oui |
| `0x00a027a0` | **`FUN_00a027a0`** | 26 | 1 | oui |
| `0x00a027e0` | **`FUN_00a027e0`** | 26 | 1 | oui |
| `0x00a0c480` | **`FUN_00a0c480`** | 26 | 2 | oui |
| `0x00a23810` | **`FUN_00a23810`** | 26 | 1 | oui |
| `0x00a25b50` | **`FUN_00a25b50`** | 26 | 0 | oui |
| `0x00a261e0` | **`FUN_00a261e0`** | 26 | 1 | oui |
| `0x00a057b0` | **`ShiftVertices`** | 25 | 0 | oui |
| `0x00a1dbc0` | **`FUN_00a1dbc0`** | 25 | 2 | oui |
| `0x00a02820` | **`FUN_00a02820`** | 24 | 1 | oui |
| `0x00a06020` | **`FUN_00a06020`** | 24 | 1 | oui |
| `0x00a0e040` | **`FUN_00a0e040`** | 24 | 1 | oui |
| `0x00a22dd0` | **`FUN_00a22dd0`** | 24 | 0 | oui |
| `0x00a276d0` | **`FUN_00a276d0`** | 24 | 1 | oui |
| `0x00a27920` | **`FUN_00a27920`** | 24 | 1 | oui |
| `0x00a27fb0` | **`FUN_00a27fb0`** | 24 | 1 | oui |
| `0x00a05ac0` | **`FUN_00a05ac0`** | 23 | 0 | oui |
| `0x00a27140` | **`FUN_00a27140`** | 23 | 2 | oui |
| `0x00a033c0` | **`FUN_00a033c0`** | 22 | 1 | oui |
| `0x00a03f10` | **`GetKBChar`** | 22 | 0 | oui |
| `0x00a27530` | **`FUN_00a27530`** | 22 | 1 | oui |
| `0x00a275a0` | **`FUN_00a275a0`** | 22 | 5 | oui |
| `0x00a28010` | **`FUN_00a28010`** | 22 | 2 | oui |
| `0x00a059e0` | **`FUN_00a059e0`** | 21 | 0 | oui |
| `0x00a05b10` | **`FUN_00a05b10`** | 21 | 0 | oui |
| `0x00a05b30` | **`FUN_00a05b30`** | 21 | 0 | oui |
| `0x00a063d0` | **`SetTextureFilterMode`** | 21 | 2 | oui |
| `0x00a20540` | **`FUN_00a20540`** | 21 | 0 | oui |
| `0x00a20eb0` | **`FUN_00a20eb0`** | 21 | 0 | oui |
| `0x00a20ed0` | **`FUN_00a20ed0`** | 21 | 0 | oui |
| `0x00a024a0` | **`FUN_00a024a0`** | 20 | 1 | oui |
| `0x00a046e0` | **`FUN_00a046e0`** | 20 | 0 | oui |
| `0x00a05060` | **`FUN_00a05060`** | 20 | 0 | oui |
| `0x00a017d0` | **`FUN_00a017d0`** | 19 | 1 | oui |
| `0x00a0cdd0` | **`FUN_00a0cdd0`** | 19 | 1 | oui |
| `0x00a20e70` | **`FUN_00a20e70`** | 19 | 0 | oui |
| `0x00a26ab0` | **`FUN_00a26ab0`** | 19 | 1 | oui |
| `0x00a03ef0` | **`EnableKBInput`** | 18 | 0 | oui |
| `0x00a041d0` | **`FUN_00a041d0`** | 18 | 0 | oui |
| `0x00a04680` | **`FUN_00a04680`** | 18 | 0 | oui |
| `0x00a046b0` | **`FUN_00a046b0`** | 18 | 0 | oui |
| `0x00a04700` | **`FUN_00a04700`** | 18 | 0 | oui |
| `0x00a05950` | **`SetAbsShift`** | 18 | 1 | oui |
| `0x00a06930` | **`FUN_00a06930`** | 18 | 1 | oui |
| `0x00a27700` | **`FUN_00a27700`** | 18 | 4 | oui |
| `0x00a27880` | **`FUN_00a27880`** | 18 | 31 | oui |
| `0x00a03430` | **`FUN_00a03430`** | 17 | 2 | oui |
| `0x00a045e0` | **`FUN_00a045e0`** | 17 | 0 | oui |
| `0x00a04600` | **`FUN_00a04600`** | 17 | 0 | oui |
| `0x00a04220` | **`FUN_00a04220`** | 16 | 0 | oui |
| `0x00a04730` | **`FUN_00a04730`** | 16 | 0 | oui |
| `0x00a05770` | **`FUN_00a05770`** | 16 | 0 | oui |
| `0x00a0e000` | **`FUN_00a0e000`** | 16 | 2 | oui |
| `0x00a03400` | **`FUN_00a03400`** | 15 | 4 | oui |
| `0x00a03f50` | **`FUN_00a03f50`** | 15 | 0 | oui |
| `0x00a203e0` | **`FUN_00a203e0`** | 15 | 1 | oui |
| `0x00a20ef0` | **`FUN_00a20ef0`** | 15 | 0 | oui |
| `0x00a20fe0` | **`FUN_00a20fe0`** | 15 | 0 | oui |
| `0x00a27820` | **`FUN_00a27820`** | 15 | 1 | oui |
| `0x00a27850` | **`FUN_00a27850`** | 15 | 1 | oui |
| `0x00a01e20` | **`PollMessages`** | 14 | 1 | oui |
| `0x00a02340` | **`FUN_00a02340`** | 14 | 1 | oui |
| `0x00a04660` | **`FUN_00a04660`** | 14 | 0 | oui |
| `0x00a050f0` | **`FUN_00a050f0`** | 14 | 0 | oui |
| `0x00a05930` | **`GetAbsShift`** | 14 | 0 | oui |
| `0x00a21020` | **`FUN_00a21020`** | 14 | 0 | oui |
| `0x00a25d10` | **`FUN_00a25d10`** | 14 | 0 | oui |
| `0x00a26a30` | **`FUN_00a26a30`** | 14 | 1 | oui |
| `0x00a26a70` | **`FUN_00a26a70`** | 14 | 1 | oui |
| `0x00a27120` | **`FUN_00a27120`** | 14 | 3 | oui |
| `0x00a279d0` | **`DirectDrawEnumerateA`** | 14 | 1 | oui |
| `0x00a279d6` | **`DirectDrawCreate`** | 14 | 1 | oui |
| `0x00a04650` | **`FUN_00a04650`** | 13 | 0 | oui |
| `0x00a04fd0` | **`FUN_00a04fd0`** | 13 | 0 | oui |
| `0x00a04fe0` | **`FUN_00a04fe0`** | 13 | 0 | oui |
| `0x00a050d0` | **`FUN_00a050d0`** | 13 | 0 | oui |
| `0x00a05bb0` | **`FUN_00a05bb0`** | 13 | 0 | oui |
| `0x00a06410` | **`FUN_00a06410`** | 13 | 0 | oui |
| `0x00a069b0` | **`FUN_00a069b0`** | 13 | 1 | oui |
| `0x00a133e0` | **`FUN_00a133e0`** | 13 | 1 | oui |
| `0x00a25c20` | **`FUN_00a25c20`** | 13 | 1 | oui |
| `0x00a25c40` | **`FUN_00a25c40`** | 13 | 1 | oui |
| `0x00a27980` | **`ReInitializeAllTextures`** | 13 | 1 | oui |
| `0x00a27990` | **`ReleaseTexture`** | 13 | 0 | oui |
| `0x00a01e30` | **`FUN_00a01e30`** | 12 | 0 | oui |
| `0x00a04ff0` | **`FUN_00a04ff0`** | 12 | 0 | oui |
| `0x00a05160` | **`FUN_00a05160`** | 12 | 0 | oui |
| `0x00a05760` | **`FUN_00a05760`** | 12 | 0 | oui |

### La CRT liee statiquement (25 fonctions, 1 181 lignes) — hors devis

La DLL embarque sa bibliotheque standard, et la cloture la traverse. Ghidra la nomme
depuis sa base de signatures — d'ou le souligne en tete. **Rien de tout cela ne se
transcrit** : `__alldiv`, `__allmul` et `__allshr` sont l'arithmetique 64 bits que le
compilateur de 1998 appelait et qu'un `long` C# fait nativement ; `__ftol` est une
conversion ; `__local_unwind2` et `__amsg_exit` sont le deroulage SEH ; le reste est
`string.h`, deja porte dans `Platform/Crt.cs`.

| adresse | fonction | lignes |
|---|---|---:|
| `0x00a28b60` | `FUN_00a28b60` | 118 |
| `0x00a28da0` | `FUN_00a28da0` | 112 |
| `0x00a281e0` | `FUN_00a281e0` | 86 |
| `0x00a28800` | `FUN_00a28800` | 79 |
| `0x00a27a20` | `_strchr` | 77 |
| `0x00a27ae0` | `__alldiv` | 74 |
| `0x00a28050` | `__strcmpi` | 62 |
| `0x00a289d0` | `FUN_00a289d0` | 59 |
| `0x00a286d0` | `FUN_00a286d0` | 57 |
| `0x00a29280` | `FUN_00a29280` | 57 |
| `0x00a280e0` | `FUN_00a280e0` | 55 |
| `0x00a279e0` | `_strrchr` | 44 |
| `0x00a28430` | `FUN_00a28430` | 33 |
| `0x00a28640` | `FUN_00a28640` | 33 |
| `0x00a28aa0` | `FUN_00a28aa0` | 30 |
| `0x00a27d90` | `__allshr` | 27 |
| `0x00a284d0` | `FUN_00a284d0` | 24 |
| `0x00a28970` | `FUN_00a28970` | 22 |
| `0x00a28b00` | `FUN_00a28b00` | 22 |
| `0x00a27dc0` | `__allmul` | 21 |
| `0x00a28400` | `FUN_00a28400` | 21 |
| `0x00a28690` | `FUN_00a28690` | 19 |
| `0x00a28f20` | `FUN_00a28f20` | 19 |
| `0x00a27d5c` | `__ftol` | 18 |
| `0x00a28620` | `FUN_00a28620` | 12 |

### Ce qui sort de la DLL (76 appels distincts)

Les appels que la cloture adresse en dehors de la DLL. Ils ne se transcrivent pas — ils
se rebranchent : sur `Platform/Win32.cs` pour Win32, sur `Platform/Crt.cs` pour la CRT,
et sur la reconstruction de la DLL soeur quand la colonne **chez** en nomme une. Cette
derniere colonne est l'argument de l'ordre de travail : une DLL dont les appels
sortants retombent chez une soeur se transcrit APRES elle, sinon le portage s'appuie
sur du natif qu'il faudra rebrancher deux fois.

| appel | sites | chez |
|---|---:|---|
| `Malloc` | 7 | `wsyskern` |
| `Free` | 5 | `wsyskern` |
| `ExitProcess` | 4 | — |
| `HeapAlloc` | 3 | — |
| `HeapFree` | 3 | — |
| `VirtualFree` | 3 | — |
| `pmaddwd` | 3 | — |
| `CreateFileA` | 2 | — |
| `DeleteObject` | 2 | — |
| `FreeLibrary` | 2 | — |
| `GetProcAddress` | 2 | — |
| `LoadLibraryA` | 2 | — |
| `MultiByteToWideChar` | 2 | — |
| `QueryPerformanceCounter` | 2 | — |
| `SetWindowLongA` | 2 | — |
| `VirtualAlloc` | 2 | — |
| `fpatan` | 2 | — |
| `CloseHandle` | 1 | — |
| `CreateEventA` | 1 | — |
| `DeleteFileA` | 1 | — |
| `DestroyWindow` | 1 | — |
| `DialogBoxParamA` | 1 | — |
| `DispatchMessageA` | 1 | — |
| `EnterCriticalSection` | 1 | — |
| `FindClose` | 1 | — |
| `FindFirstFileA` | 1 | — |
| `FindNextFileA` | 1 | — |
| `Gamma` | 1 | `wsyskern` |
| `GetAsyncKeyState` | 1 | — |
| `GetCommandLineA` | 1 | — |
| `GetModuleFileNameA` | 1 | — |
| `GetPrivateProfileIntA` | 1 | — |
| `GetStringTypeA` | 1 | — |
| `GetStringTypeW` | 1 | — |
| `InitializeCriticalSection` | 1 | — |
| `LCMapStringA` | 1 | — |
| `LCMapStringW` | 1 | — |
| `LeaveCriticalSection` | 1 | — |
| `MessageBoxA` | 1 | — |
| `MoveFileA` | 1 | — |
| `Ordinal_3` | 1 | — |
| `Ordinal_4` | 1 | — |
| `Ordinal_5` | 1 | — |
| `PeekMessageA` | 1 | — |
| `ProjectionMatrix` | 1 | `wsyskern` |
| `QueryPerformanceFrequency` | 1 | — |
| `ROUND` | 1 | — |
| `RaiseException` | 1 | — |
| `ReadFile` | 1 | — |
| `ResetEvent` | 1 | — |
| `ScaleMatrix` | 1 | `wsyskern, playst` |
| `SelectObject` | 1 | — |
| `SetBkMode` | 1 | — |
| `SetCurrentDirectoryA` | 1 | — |
| `SetEvent` | 1 | — |
| `SetFilePointer` | 1 | — |
| `SetTextColor` | 1 | — |
| `SetUnhandledExceptionFilter` | 1 | — |
| `Sleep` | 1 | — |
| `Strdup` | 1 | `wsyskern` |
| `SubtractRect` | 1 | — |
| `TextOutA` | 1 | — |
| `TranslateMessage` | 1 | — |
| `UnionRect` | 1 | — |
| `WaitMessage` | 1 | — |
| `WideCharToMultiByte` | 1 | — |
| `WriteFile` | 1 | — |
| `WritePrivateProfileStringA` | 1 | — |
| `fsin` | 1 | — |
| `halt_baddata` | 1 | — |
| `joyGetPos` | 1 | — |
| `lstrcatA` | 1 | — |
| `lstrcmpiA` | 1 | — |
| `timeKillEvent` | 1 | — |
| `timeSetEvent` | 1 | — |
| `wsprintfA` | 1 | — |

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
