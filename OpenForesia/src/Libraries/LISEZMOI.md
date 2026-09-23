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
  **passerelles** vers le natif — un seul tas, documenté comme provisoire. (Tombées le
  2026-09-23, une fois établi au désassembleur qu'aucun code resté natif ne partageait plus un
  bloc avec le C# : voir `Documentation/Etat/byebye-dll.md`.) ;
- **les vtables.** `CreateArray` fabrique un objet C++ dont `ArrayToString` et `DeleteArray`
  appellent les emplacements. Porter la fabrique sans les méthodes — ou l'inverse — donne un objet
  dont personne ne sait plus qui l'a construit ;
- **les sections critiques.** Le codec de `wsyskern` garde son état dans des globales protégées par
  `DAT_10057e48`. Deux moitiés, deux verrous, aucune protection.

D'où l'ordre de travail : **par sous-arbre complet d'un point d'entrée**, jamais par sujet. La
colonne « appelants » de `DLLAtteignables.md` est là pour ça.

## La sixième règle, découverte au chemin d'affichage : **aliaser plutôt que dupliquer**

La règle 2 dit qu'une globale de DLL ne rejoint jamais `GameMemory`. Elle reste vraie. Mais elle
supposait qu'une DLL se porte **entièrement**, d'un bloc, avec ses globales recréées en C#.

Le chemin d'affichage de `winsys3d` a montré la limite : **157 corps, 12 408 lignes**, indivisible
en transcription classique parce qu'à état inchangé un producteur **allonge l'enregistrement
précédent** du tampon de commandes (`paquet[curseur - 1] += n`). Deux moitiés ne peuvent pas
coexister avec deux copies des mémos. Trois découpages plus courts ont été essayés et **mesurés**,
tous faux — le détail est dans `Documentation/Etat/CheminAffichage.md`.

**D'où le troisième cas.** Quand un lot est trop gros pour partir d'un bloc, ses globales ne se
recréent pas : elles se **désignent**, dans le `.data` réel de la DLL chargée.

```csharp
private static byte* At(nuint va) => Base + (va - ImageBase);   // Base = GetModuleHandleA
internal static ref int Curseur_011be7dc => ref *(int*)At(0x011be7dc);
```

Il n'y a alors plus deux copies : il n'y en a qu'une, celle de 1998. Le lot redevient divisible
corps par corps, et chaque étape se vérifie à l'écran. C'est `winsys3d/Data.cs`.

Son complément obligé est `winsys3d/Natif.cs` : les corps pas encore portés ne sont **pas
exportés**, donc aucun `[DllImport]` ne les atteint. On les appelle par adresse, base + RVA, avec
l'arité **prouvée au désassembleur** — un `call` sans `add esp, N` derrière prouve à la fois le
nombre d'arguments et la convention `stdcall`.

**Les cinq garde-fous, qui ne sont pas négociables :**

1. **Une adresse aliasée ne doit jamais exister en double côté C#.** Tant qu'un seul corps reste
   natif, une copie serait morte à la seconde où le natif écrirait la vraie.
2. **`Natif.cs` est un échafaudage, pas une transcription.** Chaque entrée y est une dette, et le
   fichier doit avoir disparu quand le périmètre est porté. Il sert de tableau de bord.
   **C'est fait pour le chemin d'affichage : supprimé le 2026-09-23**, à l'étape 10, sa dette à zéro.
   Un lot suivant qui porterait un point d'entrée avant ses appelées le recrée depuis l'historique
   git — ses deux règles (arité prouvée, jamais la vignette canonique) valent telles quelles.
3. **La base se résout paresseusement**, jamais dans un initialiseur statique : la DLL n'est pas
   forcément chargée quand la classe l'est. Le dépôt a déjà payé ce prix une fois.
4. **Le jour où la DLL part entièrement**, `Data.cs` se remplace par de vraies variables d'un seul
   geste, sans toucher aux appelants — c'est pour cela que tout passe par des propriétés `ref` et
   que personne ne manipule la base directement.

5. **Compter les indirections sur le désassemblage, pas sur une expression qui marche ailleurs.**
   Une globale aliasée nomme une variable *à* une adresse : `*(T*)At(va)`, une seule indirection.
   Une valeur *dérivée* en demande une de plus, et rien ne le signale — les deux compilent et les
   deux ont le même type. C'est le premier plantage du chantier, le 2026-09-22 : `DeviceVtable`
   était écrit `*(void***)At(0x011be760)`, copié depuis `Options.cs` où la même expression est
   juste parce qu'elle s'applique à la **valeur** que rend l'ordinal 750. Appliquée à l'**adresse**
   de la globale, elle rendait le device lui-même, et `vtable[0x2c]` lisait à `device + 0xb0` —
   dans l'objet. Le binaire donne la réponse en trois temps : `mov ecx,[11BE760h]` (le device),
   `mov eax,[ecx]` (sa vtable), `call [eax+0B0h]` (la case). Deux `mov` avant le `call` = deux
   indirections.

**Ceci n'autorise pas l'aliasing par confort.** Une DLL qui se porte d'un bloc se porte d'un bloc,
avec ses propres globales : c'est le cas de `wsyskern`, et c'est resté le cas par défaut. Le
troisième cas ne s'ouvre que sur un lot dont on a **mesuré** qu'il est indivisible autrement.
