; Function: FUN_005098f0
; Entry:    005098f0

005098f0     A1 48 2A 56 00           MOV EAX,[0x00562a48]
005098f5     83 EC 14                 SUB ESP,0x14
005098f8     53                       PUSH EBX
005098f9     33 DB                    XOR EBX,EBX
005098fb     55                       PUSH EBP
005098fc     56                       PUSH ESI
005098fd     3B C3                    CMP EAX,EBX
005098ff     57                       PUSH EDI
00509900     0F 84 6B 01 00 00        JZ 0x00509a71
00509906     33 ED                    XOR EBP,EBP
00509908     89 1D 48 2A 56 00        MOV dword ptr [0x00562a48],EBX
0050990e     89 1D 08 8E 57 00        MOV dword ptr [0x00578e08],EBX
00509914     BE 18 35 57 00           MOV ESI,0x573518
00509919     33 FF                    XOR EDI,EDI
0050991b     57                       PUSH EDI
0050991c     E8 F5 C0 03 00           CALL 0x00545a16
00509921     8D 0C 40                 LEA ECX,[EAX + EAX*0x2]
00509924     57                       PUSH EDI
00509925     C1 E1 02                 SHL ECX,0x2
00509928     2B C8                    SUB ECX,EAX
0050992a     F7 D9                    NEG ECX
0050992c     C1 E1 03                 SHL ECX,0x3
0050992f     C1 F9 0C                 SAR ECX,0xc
00509932     81 C1 88 00 00 00        ADD ECX,0x88
00509938     66 89 4E 04              MOV word ptr [ESI + 0x4],CX
0050993c     E8 DB C0 03 00           CALL 0x00545a1c
00509941     C1 E0 06                 SHL EAX,0x6
00509944     C1 F8 0C                 SAR EAX,0xc
00509947     83 C0 48                 ADD EAX,0x48
0050994a     8D 55 03                 LEA EDX,[EBP + 0x3]
0050994d     66 89 46 06              MOV word ptr [ESI + 0x6],AX
00509951     B8 00 00 00 01           MOV EAX,0x1000000
00509956     66 C7 46 08 20 00        MOV word ptr [ESI + 0x8],0x20
0050995c     66 C7 46 0A 18 00        MOV word ptr [ESI + 0xa],0x18
00509962     C7 46 10 00 00 18 01     MOV dword ptr [ESI + 0x10],0x1180000
00509969     89 56 14                 MOV dword ptr [ESI + 0x14],EDX
0050996c     C7 46 18 08 08 0F 01     MOV dword ptr [ESI + 0x18],0x10f0808
00509973     C7 46 1C 20 10 CF 02     MOV dword ptr [ESI + 0x1c],0x2cf1020
0050997a     C7 46 20 10 0C 07 01     MOV dword ptr [ESI + 0x20],0x1070c10
00509981     89 46 24                 MOV dword ptr [ESI + 0x24],EAX
00509984     89 5E 28                 MOV dword ptr [ESI + 0x28],EBX
00509987     89 5E 30                 MOV dword ptr [ESI + 0x30],EBX
0050998a     89 5E 38                 MOV dword ptr [ESI + 0x38],EBX
0050998d     C7 46 40 00 00 0F 01     MOV dword ptr [ESI + 0x40],0x10f0000
00509994     89 46 44                 MOV dword ptr [ESI + 0x44],EAX
00509997     89 5E 48                 MOV dword ptr [ESI + 0x48],EBX
0050999a     89 5E 50                 MOV dword ptr [ESI + 0x50],EBX
0050999d     89 5E 58                 MOV dword ptr [ESI + 0x58],EBX
005099a0     89 5E 60                 MOV dword ptr [ESI + 0x60],EBX
005099a3     81 C7 55 01 00 00        ADD EDI,0x155
005099a9     45                       INC EBP
005099aa     81 C6 C8 08 00 00        ADD ESI,0x8c8
005099b0     81 FF 53 09 00 00        CMP EDI,0x953
005099b6     0F 8C 5F FF FF FF        JL 0x0050991b
005099bc     B9 38 00 00 00           MOV ECX,0x38
005099c1     B8 28 00 00 00           MOV EAX,0x28
005099c6     BA 18 00 00 00           MOV EDX,0x18
005099cb     66 C7 46 04 58 00        MOV word ptr [ESI + 0x4],0x58
005099d1     66 89 4E 06              MOV word ptr [ESI + 0x6],CX
005099d5     66 89 46 08              MOV word ptr [ESI + 0x8],AX
005099d9     66 89 56 0A              MOV word ptr [ESI + 0xa],DX
005099dd     C6 46 20 10              MOV byte ptr [ESI + 0x20],0x10
005099e1     C7 46 14 0A 00 00 00     MOV dword ptr [ESI + 0x14],0xa
005099e8     81 C6 C8 08 00 00        ADD ESI,0x8c8
005099ee     66 C7 46 04 B0 00        MOV word ptr [ESI + 0x4],0xb0
005099f4     66 89 4E 06              MOV word ptr [ESI + 0x6],CX
005099f8     66 89 46 08              MOV word ptr [ESI + 0x8],AX
005099fc     66 89 56 0A              MOV word ptr [ESI + 0xa],DX
00509a00     C6 46 20 10              MOV byte ptr [ESI + 0x20],0x10
00509a04     C7 46 14 0B 00 00 00     MOV dword ptr [ESI + 0x14],0xb
00509a0b     8B 35 38 33 57 00        MOV ESI,dword ptr [0x00573338]
00509a11     8A 86 50 01 00 00        MOV AL,byte ptr [ESI + 0x150]
00509a17     83 E0 07                 AND EAX,0x7
00509a1a     83 F8 07                 CMP EAX,0x7
00509a1d     77 58                    JA 0x00509a77
00509a1f     FF 24 85 3C A7 50 00     JMP dword ptr [EAX*0x4 + 0x50a73c]
00509a71     8B 35 38 33 57 00        MOV ESI,dword ptr [0x00573338]
00509a77     A1 4C 2A 56 00           MOV EAX,[0x00562a4c]
00509a7c     8B 6C 24 28              MOV EBP,dword ptr [ESP + 0x28]
00509a80     3B C3                    CMP EAX,EBX
00509a82     0F 85 58 0B 00 00        JNZ 0x0050a5e0
00509a88     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
00509a8d     83 C0 C2                 ADD EAX,-0x3e
00509a90     83 F8 43                 CMP EAX,0x43
00509a93     0F 87 47 0B 00 00        JA 0x0050a5e0
00509a99     33 C9                    XOR ECX,ECX
00509a9b     8A 88 84 A7 50 00        MOV CL,byte ptr [EAX + 0x50a784]
00509aa1     FF 24 8D 5C A7 50 00     JMP dword ptr [ECX*0x4 + 0x50a75c]
0050a5e0     8B 15 C4 97 5E 00        MOV EDX,dword ptr [0x005e97c4]
0050a5e6     53                       PUSH EBX
0050a5e7     68 80 21 56 00           PUSH 0x562180
0050a5ec     53                       PUSH EBX
0050a5ed     8B 42 70                 MOV EAX,dword ptr [EDX + 0x70]
0050a5f0     8B 88 0C 01 00 00        MOV ECX,dword ptr [EAX + 0x10c]
0050a5f6     51                       PUSH ECX
0050a5f7     E8 54 9D 03 00           CALL 0x00544350
0050a5fc     8B 54 24 40              MOV EDX,dword ptr [ESP + 0x40]
0050a600     83 C4 10                 ADD ESP,0x10
0050a603     68 80 21 56 00           PUSH 0x562180
0050a608     52                       PUSH EDX
0050a609     E8 92 9D 03 00           CALL 0x005443a0
0050a60e     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050a612     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
0050a617     83 C4 08                 ADD ESP,0x8
0050a61a     33 FF                    XOR EDI,EDI
0050a61c     8B 88 94 00 00 00        MOV ECX,dword ptr [EAX + 0x94]
0050a622     BE 38 35 57 00           MOV ESI,0x573538
0050a627     C7 44 24 34 10 01 00 00  MOV dword ptr [ESP + 0x34],0x110
0050a62f     8B 51 30                 MOV EDX,dword ptr [ECX + 0x30]
0050a632     C7 44 24 14 00 00 00 01  MOV dword ptr [ESP + 0x14],0x1000000
0050a63a     89 54 24 10              MOV dword ptr [ESP + 0x10],EDX
0050a63e     EB 04                    JMP 0x0050a644
0050a640     8B 6C 24 28              MOV EBP,dword ptr [ESP + 0x28]
0050a644     66 8B 46 E4              MOV AX,word ptr [ESI + -0x1c]
0050a648     66 8B 4E E6              MOV CX,word ptr [ESI + -0x1a]
0050a64c     66 05 0C 00              ADD AX,0xc
0050a650     66 83 C1 04              ADD CX,0x4
0050a654     66 89 44 24 18           MOV word ptr [ESP + 0x18],AX
0050a659     8D 44 24 30              LEA EAX,[ESP + 0x30]
0050a65d     8D 57 04                 LEA EDX,[EDI + 0x4]
0050a660     50                       PUSH EAX
0050a661     55                       PUSH EBP
0050a662     66 89 4C 24 22           MOV word ptr [ESP + 0x22],CX
0050a667     66 89 54 24 28           MOV word ptr [ESP + 0x28],DX
0050a66c     E8 AF 17 01 00           CALL 0x0051be20
0050a671     8B 54 24 38              MOV EDX,dword ptr [ESP + 0x38]
0050a675     83 C4 08                 ADD ESP,0x8
0050a678     8D 4C 24 10              LEA ECX,[ESP + 0x10]
0050a67c     53                       PUSH EBX
0050a67d     53                       PUSH EBX
0050a67e     51                       PUSH ECX
0050a67f     52                       PUSH EDX
0050a680     55                       PUSH EBP
0050a681     E8 6A 77 01 00           CALL 0x00521df0
0050a686     83 C4 14                 ADD ESP,0x14
0050a689     8D 4C 24 10              LEA ECX,[ESP + 0x10]
0050a68d     89 44 24 30              MOV dword ptr [ESP + 0x30],EAX
0050a691     51                       PUSH ECX
0050a692     50                       PUSH EAX
0050a693     55                       PUSH EBP
0050a694     E8 27 53 01 00           CALL 0x0051f9c0
0050a699     83 C4 0C                 ADD ESP,0xc
0050a69c     8D 54 24 30              LEA EDX,[ESP + 0x30]
0050a6a0     89 44 24 30              MOV dword ptr [ESP + 0x30],EAX
0050a6a4     52                       PUSH EDX
0050a6a5     55                       PUSH EBP
0050a6a6     E8 05 17 01 00           CALL 0x0051bdb0
0050a6ab     83 C4 08                 ADD ESP,0x8
0050a6ae     33 C0                    XOR EAX,EAX
0050a6b0     83 FF 07                 CMP EDI,0x7
0050a6b3     0F 9D C0                 SETGE AL
0050a6b6     48                       DEC EAX
0050a6b7     24 F8                    AND AL,0xf8
0050a6b9     83 C0 10                 ADD EAX,0x10
0050a6bc     88 06                    MOV byte ptr [ESI],AL
0050a6be     8A 87 60 90 5E 00        MOV AL,byte ptr [EDI + 0x5e9060]
0050a6c4     3C 64                    CMP AL,0x64
0050a6c6     73 03                    JNC 0x0050a6cb
0050a6c8     80 06 08                 ADD byte ptr [ESI],0x8
0050a6cb     3C 0A                    CMP AL,0xa
0050a6cd     73 03                    JNC 0x0050a6d2
0050a6cf     80 06 08                 ADD byte ptr [ESI],0x8
0050a6d2     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050a6d8     8B 44 24 34              MOV EAX,dword ptr [ESP + 0x34]
0050a6dc     8D 6E E0                 LEA EBP,[ESI + -0x20]
0050a6df     53                       PUSH EBX
0050a6e0     8B 51 70                 MOV EDX,dword ptr [ECX + 0x70]
0050a6e3     55                       PUSH EBP
0050a6e4     53                       PUSH EBX
0050a6e5     8B 0C 02                 MOV ECX,dword ptr [EDX + EAX*0x1]
0050a6e8     51                       PUSH ECX
0050a6e9     E8 62 9C 03 00           CALL 0x00544350
0050a6ee     8B 54 24 40              MOV EDX,dword ptr [ESP + 0x40]
0050a6f2     83 C4 10                 ADD ESP,0x10
0050a6f5     55                       PUSH EBP
0050a6f6     52                       PUSH EDX
0050a6f7     E8 A4 9C 03 00           CALL 0x005443a0
0050a6fc     83 C4 08                 ADD ESP,0x8
0050a6ff     89 44 24 30              MOV dword ptr [ESP + 0x30],EAX
0050a703     55                       PUSH EBP
0050a704     50                       PUSH EAX
0050a705     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
0050a709     50                       PUSH EAX
0050a70a     E8 31 AC 03 00           CALL 0x00545340
0050a70f     8B 4C 24 40              MOV ECX,dword ptr [ESP + 0x40]
0050a713     83 C4 0C                 ADD ESP,0xc
0050a716     83 C1 04                 ADD ECX,0x4
0050a719     47                       INC EDI
0050a71a     81 C6 C8 08 00 00        ADD ESI,0x8c8
0050a720     81 F9 34 01 00 00        CMP ECX,0x134
0050a726     89 44 24 30              MOV dword ptr [ESP + 0x30],EAX
0050a72a     89 4C 24 34              MOV dword ptr [ESP + 0x34],ECX
0050a72e     0F 8C 0C FF FF FF        JL 0x0050a640
0050a734     5F                       POP EDI
0050a735     5E                       POP ESI
0050a736     5D                       POP EBP
0050a737     5B                       POP EBX
0050a738     83 C4 14                 ADD ESP,0x14
0050a73b     C3                       RET
