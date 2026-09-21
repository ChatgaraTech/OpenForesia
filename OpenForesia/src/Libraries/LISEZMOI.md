# `src/Libraries/` — les DLL d'aide de 1998, reconstruites

Ce répertoire n'est pas le jeu. Il porte les **cinq DLL livrées avec `Blaze.exe`** en 1998, au fur
et à mesure qu'on s'en passe. Sa feuille de route est `Documentation/Etat/DLLAtteignables.md`,
générée par `sh OpenForesia.Tools/census-dll-surface.sh`.

## Pourquoi un répertoire à part, et pas `Platform/`

Parce que **la source de vérité n'est pas la même**. Tout ce qui vit sous `src/Blaze/` se relit
contre `Legacy/Decompiled/Blaze.c` ; ce qui vit ici se relit contre `winsys3d.c`, `wsyskern.c`,
`3dhelp.c`, `gldhelp.c`, `playst.c`. Ce sont des **programmes distincts**, avec leur propre espace
d'adressage, leurs propres globales et leur propre base de chargement. Les mélanger à `Platform/`
effacerait la seule distinction qui compte quand on vérifie une transcription : contre quoi la
relire.

```
src/Libraries/
   LISEZMOI.md          ce fichier
   wsyskern/            base 0x10000000 — services generaux ; la feuille du graphe
   winsys3d/            base 0x00a00000 — le portage Win32 de la bibliotheque PSY-Q
   3dhelp/              base 0x0ffe0000 — le device DirectDraw/Direct3D
   gldhelp/             base 0x10000000 — le device Glide (3Dfx)
```

Une DLL = un répertoire. Un fichier unique au départ, éclaté par sujet quand il le mérite — c'est
la règle qui a échoué une fois dans ce dépôt (`WorldMapTick.cs`, 74 072 lignes), donc **on n'attend
pas** : dès qu'un sujet est identifiable, il prend son fichier.

## Les cinq règles

**1. Le commentaire de traçabilité porte les trois pièces.** La décompilation d'origine, le nom
Ghidra, l'adresse :

```csharp
// Ghidra: wsyskern.c LoadBitmapEx @ 0x1000bac0
```

Ce n'est pas décoratif : `census-dll-surface.sh` lit exactement cette forme pour la colonne
`implemente`. Une règle plus lâche comptait 14 corps portés là où il y en avait 5 — le dépôt cite
ces fonctions en prose à longueur de commentaire, et **une mention n'est pas une transcription**.

**2. Les globales d'une DLL ne rejoignent jamais `GameMemory`.** `DAT_10057e48` vit dans l'espace
d'adressage de `wsyskern.dll`, pas dans le `.data` de `Blaze.exe`. Les y verser recréerait
exactement le défaut d'aliasing que `OpenForesia.Tests/MemoryLayoutTests.cs` traque. Chaque
répertoire porte son `Data.cs`, avec ses propres VA.

**3. Un nom de classe ne peut pas commencer par un chiffre.** Le répertoire s'appelle `3dhelp/` —
un chemin accepte tout —, la classe s'appelle `Help3d`. Même chose pour son `Data3d`.

**4. Ce ne sont pas des `partial class Blaze`.** Chaque DLL est une classe statique à elle, dans
l'espace de noms `OpenForesia.Libraries`. Rien ici ne partage d'état avec la reconstruction du jeu.

**5. La façade ne bouge pas.** `src/Blaze/Interop/Wsyskern.cs` et `Winsys3d.cs` restent le point
d'appel unique — il y a **1 970 sites d'appel `Winsys3d.*` dans 154 fichiers**. Porter une fonction
consiste à remplacer *dans la façade* un `[DllImport]` par un renvoi vers `Libraries/`. Aucun
appelant ne change, et chaque point d'entrée se débranche isolément.

## La règle qui n'était pas prévue : ne pas couper une représentation en deux

Une fonction ne se porte pas seule si elle **partage une représentation mutable** avec une fonction
restée native. Trois cas rencontrés dès le premier lot de `wsyskern` :

- **le tas.** `Malloc` sert du natif, `Free` rend au natif. Porter l'un des deux sans l'autre, ou
  porter la paire alors que `LoadBitmapFile` alloue encore côté DLL, fait rendre à un tas un bloc
  pris dans l'autre. Tant que la DLL n'est pas portée en entier, `Malloc` et `Free` sont des
  **passerelles** vers le natif — un seul tas, documenté comme provisoire ;
- **les vtables.** `CreateArray` fabrique un objet C++ dont `ArrayToString` et `DeleteArray`
  appellent les emplacements. Porter la fabrique sans les méthodes — ou l'inverse — donne un objet
  dont personne ne sait plus qui l'a construit ;
- **les sections critiques.** Le codec de `wsyskern` garde son état dans des globales protégées par
  `DAT_10057e48`. Deux moitiés, deux verrous, aucune protection.

D'où l'ordre de travail : **par sous-arbre complet d'un point d'entrée**, jamais par sujet. La
colonne « appelants » de `DLLAtteignables.md` est là pour ça.
