# OpenForesia --- Trace dynamique caméra ViewAngle / ViewAngleGoal

## Objectif

Tracer dynamiquement les écritures vers `0x006067E8` dans `Blaze.exe`
avec x32dbg afin d'identifier la séquence réelle qui initialise
l'orientation caméra.

## Breakpoint matériel

Breakpoint matériel en écriture
(`Hardware Breakpoint -> Write -> DWord`) posé sur `0x006067E8`,
couvrant l'élévation et le cap.

## 1. Premier écrivain pendant le chargement

``` asm
00516A0C | mov bp,word ptr ds:[564650]
00516A13 | mov word ptr ds:[6067E8],bp
00516A1A | mov bp,word ptr ds:[564652]
00516A21 | mov word ptr ds:[6067EA],bp
```

Séquence :

``` text
[00564650] -> [006067E8]
[00564652] -> [006067EA]
```

Au chargement, les valeurs précédemment mesurées étaient `00564650=0` et
`00564652=1024`.

## 2. PublishFrameView / FUN_00401080

``` asm
00401093 | mov ax,word ptr ds:[6067E8]
00401099 | mov cx,word ptr ds:[6067EA]
004010A0 | mov dx,word ptr ds:[6067EC]
004010A7 | mov word ptr ds:[5712F8],ax

004010AD | mov ax,word ptr ds:[6067E0]
004010B3 | mov word ptr ds:[5712FA],cx
004010BA | mov cx,word ptr ds:[6067E2]
004010C1 | mov word ptr ds:[5712FC],dx
004010C8 | mov dx,word ptr ds:[6067E4]

004010CF | mov word ptr ds:[6067E8],ax
004010DA | mov word ptr ds:[6067EA],cx
004010E1 | mov word ptr ds:[6067EC],dx
```

Cela confirme dynamiquement :

``` text
ViewAngle -> SavedViewAngle
ViewAngleGoal -> ViewAngle
```

## 3. Passage crucial dans FUN_00401000

``` asm
00401000 | mov ax,word ptr ds:[5712F8]
00401006 | mov cx,word ptr ds:[5712FA]
0040100D | mov dx,word ptr ds:[5712FC]
00401014 | mov word ptr ds:[6067E8],ax

0040101A | mov eax,dword ptr ds:[5E9AE0]

0040101F | mov word ptr ds:[6067EA],cx
00401026 | mov word ptr ds:[6067EC],dx

0040102D | mov cx,word ptr ds:[eax]
00401030 | mov word ptr ds:[6067E0],cx
00401037 | mov dx,word ptr ds:[eax+2]
0040103B | mov word ptr ds:[6067E2],dx
00401042 | mov cx,word ptr ds:[eax+4]
00401046 | mov word ptr ds:[6067E4],cx

0040104D | mov dx,word ptr ds:[eax]
00401050 | mov word ptr ds:[6067E8],dx
00401057 | mov cx,word ptr ds:[eax+2]
0040105B | mov word ptr ds:[6067EA],cx
00401062 | mov dx,word ptr ds:[eax+4]
00401066 | or byte ptr ds:[57139C],20
0040106D | mov word ptr ds:[6067EC],dx
```

## 4. Valeur réelle de DAT_005E9AE0

Le debugger était arrêté avant :

``` asm
0040101A | mov eax,dword ptr ds:[5E9AE0]
```

Après un `F7` :

``` text
EAX = 006067E8
```

Donc, à cet instant :

``` text
[005E9AE0] = 006067E8
DAT_005E9AE0 -> ViewAngle
```

## 5. SavedViewAngle observé

Dump :

``` text
005712F8  58 02 00 04 00 00 00 00 00 40 38 00 00 C0 12 00
```

Les 6 premiers octets donnent :

``` text
58 02 -> 600
00 04 -> 1024
00 00 -> 0
```

Donc :

``` text
SavedViewAngle @ 005712F8 = (600, 1024, 0)
```

## 6. Séquence dynamique établie

`FUN_00401000` restaure d'abord :

``` text
SavedViewAngle -> ViewAngle
```

avec :

``` text
(600, 1024, 0) -> (600, 1024, 0)
```

Puis `DAT_005E9AE0` pointe sur `ViewAngle` (`0x006067E8`), et la
fonction effectue :

``` text
ViewAngleGoal = ViewAngle
```

La chaîne complète observée est donc :

``` text
SavedViewAngle (600,1024,0)
        |
        v
ViewAngle      (600,1024,0)
        |
        v
ViewAngleGoal  (600,1024,0)
```

## Conclusion

La graine de `ViewAngleGoal` n'est pas absente dans Blaze.

La trace dynamique montre une chaîne naturelle dans `FUN_00401000` :

``` text
SavedViewAngle -> ViewAngle -> ViewAngleGoal
```

avec :

``` text
SavedViewAngle = (600, 1024, 0)
DAT_005E9AE0   = 0x006067E8 = ViewAngle
```

À vérifier côté OpenForesia :

1.  restauration de `ViewAngle` depuis `0x005712F8`;
2.  valeur/sémantique de `DAT_005E9AE0` à cet instant;
3.  ordre exact des opérations dans `FUN_00401000`;
4.  copie du `ViewAngle` restauré vers `ViewAngleGoal`.

Le correctif expérimental `OPENFORESIA_SEED_GOAL=1` ne devrait pas être
nécessaire si cette séquence legacy est transcrite fidèlement.

## Adresses de référence

``` text
SavedViewAngle @ 005712F8 = (600, 1024, 0)
DAT_005E9AE0 -> 006067E8
ViewAngle     @ 006067E8
ViewAngleGoal @ 006067E0
```

Trace réalisée avec x32dbg sur `Blaze.exe`, via breakpoint matériel en
écriture sur `0x006067E8`.
