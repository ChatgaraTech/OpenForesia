# x32dbg — traçage de `confirmGate` (`0x005E909F`)

## Objectif
Identifier l'instruction qui écrit `confirmGate` pendant l'écran de choix du style de langage. Test effectué au moment de valider **« Oui »**.

## Breakpoint
```text
bphws 5E909F,w,1
```
Breakpoint matériel en écriture, taille 1 octet, sur `0x005E909F`.

## Résultat
x32dbg s'arrête à :
```asm
005455FA | E8 8185FFFF | call blaze.53DB80
```
L'instruction immédiatement précédente est :
```asm
005455F4 | 8882 60905E00 | mov byte ptr ds:[edx+5E9060],al
```

Registres pertinents :
```text
EAX = 00000000
EDX = 0000003F
EBP = 00560728
EIP = 005455FA
DR0 = 005E909F
```

La destination est donc :
```text
0x005E9060 + 0x3F = 0x005E909F
```
Cela confirme que `005455F4` écrit directement sur l'octet surveillé. `AL = 0`, donc l'écriture observée est :
```c
DAT_005E909F = 0;
// soit confirmGate = 0; si ce nom est confirmé
```

## Séquence pertinente
```asm
005455DD | F685 C7080000 C0 | test byte ptr ss:[ebp+8C7],C0
005455E4 | 75 14            | jne blaze.5455FA
005455E6 | 8A85 C4080000    | mov al,byte ptr ss:[ebp+8C4]
005455EC | 33D2             | xor edx,edx
005455EE | 8A95 C5080000    | mov dl,byte ptr ss:[ebp+8C5]
005455F4 | 8882 60905E00    | mov byte ptr ds:[edx+5E9060],al
005455FA | E8 8185FFFF      | call blaze.53DB80
005455FF | 80A5 C7080000 3F | and byte ptr ss:[ebp+8C7],3F
```

Interprétation : la routine utilise `+0x8C5` comme index dans une table basée à `0x005E9060`, et `+0x8C4` comme valeur. Pour ce hit : index `0x3F`, valeur `0`, donc entrée `0x005E909F = 0`.

La même zone contient aussi :
```asm
005455AA | mov byte ptr ds:[ecx+5E9060],al
005455D7 | mov byte ptr ds:[ecx+5E9060],dl
005455F4 | mov byte ptr ds:[edx+5E9060],al
```
Il est donc pertinent d'étudier `0x005E9060` comme une table indexée, dont `confirmGate` serait ici l'entrée `0x3F`.

## Call Stack
Le Main Thread est le thread `2928`. L'export brut montre `blaze.005455FA` comme frame courante. La reconstruction de pile est peu exploitable au-delà : les valeurs suivantes ne ressemblent pas à une chaîne de frames Blaze classique. Les nombreux autres blocs de l'export correspondent à d'autres threads système/audio/vidéo/input et ne doivent pas être confondus avec le Main Thread.

## Conclusion / suite Ghidra
Le débogage dynamique a identifié une écriture relative que la recherche statique par adresse littérale pouvait manquer :
```asm
mov byte ptr [edx+0x005E9060], al
```
avec `EDX=0x3F`, soit `0x005E909F`.

À corréler dans Ghidra :
- bloc/fonction englobant `0x005455F4` ;
- callers et chemin depuis l'écran de style de langage ;
- `FUN_0053DB80` appelée juste après ;
- rôle des champs `+0x8C4`, `+0x8C5`, `+0x8C7` ;
- nature de la table `0x005E9060` et confirmation que l'entrée `0x3F` correspond bien à `confirmGate`.

## Fichiers joints
- `confirmGate_005E909F_assembly.txt` — ASM brut autour du hit.
- `confirmGate_005E909F_registers.txt` — registres utiles relevés au breakpoint.
- `confirmGate_005E909F_callstack.txt` — Call Stack brute complète x32dbg.
