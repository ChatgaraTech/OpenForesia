; Function: FUN_00508e00
; Entry:    00508e00

00508e00     A1 48 2A 56 00           MOV EAX,[0x00562a48]
00508e05     83 EC 44                 SUB ESP,0x44
00508e08     53                       PUSH EBX
00508e09     55                       PUSH EBP
00508e0a     33 ED                    XOR EBP,EBP
00508e0c     56                       PUSH ESI
00508e0d     3B C5                    CMP EAX,EBP
00508e0f     57                       PUSH EDI
00508e10     0F 84 C9 00 00 00        JZ 0x00508edf
00508e16     A1 38 33 57 00           MOV EAX,[0x00573338]
00508e1b     89 2D 48 2A 56 00        MOV dword ptr [0x00562a48],EBP
00508e21     66 C7 05 28 49 60 00 5E 01 MOV word ptr [0x00604928],0x15e
00508e2a     66 B9 00 08              MOV CX,0x800
00508e2e     66 0F B6 80 51 01 00 00  MOVZX AX,byte ptr [EAX + 0x151]
00508e36     83 E8 04                 SUB EAX,0x4
00508e39     66 89 2D 2C 49 60 00     MOV word ptr [0x0060492c],BP
00508e40     C1 E0 09                 SHL EAX,0x9
00508e43     25 FF 0F 00 00           AND EAX,0xfff
00508e48     66 89 2D 78 2B 57 00     MOV word ptr [0x00572b78],BP
00508e4f     66 2B C8                 SUB CX,AX
00508e52     66 A3 2A 49 60 00        MOV [0x0060492a],AX
00508e58     66 89 0D 7A 2B 57 00     MOV word ptr [0x00572b7a],CX
00508e5f     66 89 2D 7C 2B 57 00     MOV word ptr [0x00572b7c],BP
00508e66     BE 78 2E 57 00           MOV ESI,0x572e78
00508e6b     BB 01 00 00 00           MOV EBX,0x1
00508e70     8B FD                    MOV EDI,EBP
00508e72     C1 E7 09                 SHL EDI,0x9
00508e75     57                       PUSH EDI
00508e76     E8 A1 CB 03 00           CALL 0x00545a1c
00508e7b     8B D0                    MOV EDX,EAX
00508e7d     57                       PUSH EDI
00508e7e     C1 E2 04                 SHL EDX,0x4
00508e81     2B D0                    SUB EDX,EAX
00508e83     C7 06 00 00 00 00        MOV dword ptr [ESI],0x0
00508e89     F7 DA                    NEG EDX
00508e8b     C1 E2 03                 SHL EDX,0x3
00508e8e     89 56 FC                 MOV dword ptr [ESI + -0x4],EDX
00508e91     E8 80 CB 03 00           CALL 0x00545a16
00508e96     8D 04 40                 LEA EAX,[EAX + EAX*0x2]
00508e99     C7 46 18 00 00 00 01     MOV dword ptr [ESI + 0x18],0x1000000
00508ea0     66 89 5E 16              MOV word ptr [ESI + 0x16],BX
00508ea4     81 C6 9C 00 00 00        ADD ESI,0x9c
00508eaa     8D 04 80                 LEA EAX,[EAX + EAX*0x4]
00508ead     C1 E0 03                 SHL EAX,0x3
00508eb0     89 86 68 FF FF FF        MOV dword ptr [ESI + 0xffffff68],EAX
00508eb6     33 C0                    XOR EAX,EAX
00508eb8     66 89 86 6C FF FF FF     MOV word ptr [ESI + 0xffffff6c],AX
00508ebf     66 89 86 6E FF FF FF     MOV word ptr [ESI + 0xffffff6e],AX
00508ec6     66 89 86 70 FF FF FF     MOV word ptr [ESI + 0xffffff70],AX
00508ecd     45                       INC EBP
00508ece     83 FD 08                 CMP EBP,0x8
00508ed1     7C 9D                    JL 0x00508e70
00508ed3     C7 05 50 2A 56 00 03 00 00 00 MOV dword ptr [0x00562a50],0x3
00508edd     33 ED                    XOR EBP,EBP
00508edf     39 2D 4C 2A 56 00        CMP dword ptr [0x00562a4c],EBP
00508ee5     0F 85 4B 02 00 00        JNZ 0x00509136
00508eeb     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
00508ef0     BE 2E 00 00 00           MOV ESI,0x2e
00508ef5     2B C6                    SUB EAX,ESI
00508ef7     0F 84 6B 01 00 00        JZ 0x00509068
00508efd     48                       DEC EAX
00508efe     0F 84 14 01 00 00        JZ 0x00509018
00508f04     48                       DEC EAX
00508f05     0F 85 2B 02 00 00        JNZ 0x00509136
00508f0b     F6 05 EF 0F 56 00 04     TEST byte ptr [0x00560fef],0x4
00508f12     0F 85 CF 00 00 00        JNZ 0x00508fe7
00508f18     A0 9F 90 5E 00           MOV AL,[0x005e909f]
00508f1d     84 C0                    TEST AL,AL
00508f1f     74 0B                    JZ 0x00508f2c
00508f21     89 35 F0 8D 57 00        MOV dword ptr [0x00578df0],ESI
00508f27     E9 0A 02 00 00           JMP 0x00509136
00508f2c     C7 05 F0 8D 57 00 39 00 00 00 MOV dword ptr [0x00578df0],0x39
00508f36     BE 20 2C 57 00           MOV ESI,0x572c20
00508f3b     8B 15 38 33 57 00        MOV EDX,dword ptr [0x00573338]
00508f41     33 C0                    XOR EAX,EAX
00508f43     8D 4E C4                 LEA ECX,[ESI + -0x3c]
00508f46     6A 27                    PUSH 0x27
00508f48     8A 82 51 01 00 00        MOV AL,byte ptr [EDX + 0x151]
00508f4e     51                       PUSH ECX
00508f4f     8D 0C 80                 LEA ECX,[EAX + EAX*0x4]
00508f52     C1 E1 03                 SHL ECX,0x3
00508f55     2B C8                    SUB ECX,EAX
00508f57     8D 14 8D 58 2E 57 00     LEA EDX,[ECX*0x4 + 0x572e58]
00508f5e     52                       PUSH EDX
00508f5f     E8 7C C3 02 00           CALL 0x005352e0
00508f64     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
00508f6a     8B 15 C4 97 5E 00        MOV EDX,dword ptr [0x005e97c4]
00508f70     33 C0                    XOR EAX,EAX
00508f72     83 C4 0C                 ADD ESP,0xc
00508f75     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
00508f7b     8B 8A 94 00 00 00        MOV ECX,dword ptr [EDX + 0x94]
00508f81     8B 54 81 08              MOV EDX,dword ptr [ECX + EAX*0x4 + 0x8]
00508f85     89 16                    MOV dword ptr [ESI],EDX
00508f87     E8 B4 E1 03 00           CALL 0x00547140
00508f8c     66 89 44 24 42           MOV word ptr [ESP + 0x42],AX
00508f91     0F BF C0                 MOVSX EAX,AX
00508f94     50                       PUSH EAX
00508f95     E8 82 CA 03 00           CALL 0x00545a1c
00508f9a     8D 04 40                 LEA EAX,[EAX + EAX*0x2]
00508f9d     8D 0C 80                 LEA ECX,[EAX + EAX*0x4]
00508fa0     D1 E1                    SHL ECX,0x1
00508fa2     89 4E 14                 MOV dword ptr [ESI + 0x14],ECX
00508fa5     E8 96 E1 03 00           CALL 0x00547140
00508faa     8D 14 C5 08 00 FE FF     LEA EDX,[EAX*0x8 + 0xfffe0008]
00508fb1     0F BF 44 24 42           MOVSX EAX,word ptr [ESP + 0x42]
00508fb6     50                       PUSH EAX
00508fb7     89 56 18                 MOV dword ptr [ESI + 0x18],EDX
00508fba     E8 57 CA 03 00           CALL 0x00545a16
00508fbf     8D 04 40                 LEA EAX,[EAX + EAX*0x2]
00508fc2     81 C6 9C 00 00 00        ADD ESI,0x9c
00508fc8     8D 0C 80                 LEA ECX,[EAX + EAX*0x4]
00508fcb     D1 E1                    SHL ECX,0x1
00508fcd     89 4E 80                 MOV dword ptr [ESI + -0x80],ECX
00508fd0     81 FE 90 2E 57 00        CMP ESI,0x572e90
00508fd6     0F 8C 5F FF FF FF        JL 0x00508f3b
00508fdc     FF 05 E8 8D 57 00        INC dword ptr [0x00578de8]
00508fe2     E9 4F 01 00 00           JMP 0x00509136
00508fe7     8B 54 24 64              MOV EDX,dword ptr [ESP + 0x64]
00508feb     F6 42 03 40              TEST byte ptr [EDX + 0x3],0x40
00508fef     0F 84 41 01 00 00        JZ 0x00509136
00508ff5     A0 EF 0F 56 00           MOV AL,[0x00560fef]
00508ffa     C6 05 63 07 56 00 00     MOV byte ptr [0x00560763],0x0
00509001     24 FB                    AND AL,0xfb
00509003     A2 EF 0F 56 00           MOV [0x00560fef],AL
00509008     E8 93 4B 03 00           CALL 0x0053dba0
0050900d     89 35 F0 8D 57 00        MOV dword ptr [0x00578df0],ESI
00509013     E9 1E 01 00 00           JMP 0x00509136
00509018     F6 05 EF 0F 56 00 02     TEST byte ptr [0x00560fef],0x2
0050901f     8B 44 24 64              MOV EAX,dword ptr [ESP + 0x64]
00509023     75 16                    JNZ 0x0050903b
00509025     66 81 60 02 FF 7F        AND word ptr [EAX + 0x2],0x7fff
0050902b     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
00509030     40                       INC EAX
00509031     A3 F0 8D 57 00           MOV [0x00578df0],EAX
00509036     E9 FB 00 00 00           JMP 0x00509136
0050903b     F6 40 03 40              TEST byte ptr [EAX + 0x3],0x40
0050903f     0F 84 F1 00 00 00        JZ 0x00509136
00509045     A0 EF 0F 56 00           MOV AL,[0x00560fef]
0050904a     C6 05 5B 07 56 00 00     MOV byte ptr [0x0056075b],0x0
00509051     24 FD                    AND AL,0xfd
00509053     A2 EF 0F 56 00           MOV [0x00560fef],AL
00509058     E8 43 4B 03 00           CALL 0x0053dba0
0050905d     89 35 F0 8D 57 00        MOV dword ptr [0x00578df0],ESI
00509063     E9 CE 00 00 00           JMP 0x00509136
00509068     8B 74 24 64              MOV ESI,dword ptr [ESP + 0x64]
0050906c     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
00509070     F6 C4 80                 TEST AH,0x80
00509073     74 19                    JZ 0x0050908e
00509075     25 FF 7F 00 00           AND EAX,0x7fff
0050907a     66 89 46 02              MOV word ptr [ESI + 0x2],AX
0050907e     E8 FD 4A 03 00           CALL 0x0053db80
00509083     FF 05 F0 8D 57 00        INC dword ptr [0x00578df0]
00509089     E9 A8 00 00 00           JMP 0x00509136
0050908e     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
00509094     F6 C4 40                 TEST AH,0x40
00509097     74 39                    JZ 0x005090d2
00509099     66 0F B6 81 50 01 00 00  MOVZX AX,byte ptr [ECX + 0x150]
005090a1     83 C0 08                 ADD EAX,0x8
005090a4     66 BA 00 08              MOV DX,0x800
005090a8     C1 E0 08                 SHL EAX,0x8
005090ab     66 2B D0                 SUB DX,AX
005090ae     66 A3 2A 49 60 00        MOV [0x0060492a],AX
005090b4     66 89 15 7A 2B 57 00     MOV word ptr [0x00572b7a],DX
005090bb     C7 05 F0 8D 57 00 5F 00 00 00 MOV dword ptr [0x00578df0],0x5f
005090c5     E8 D6 4A 03 00           CALL 0x0053dba0
005090ca     FF 0D E8 8D 57 00        DEC dword ptr [0x00578de8]
005090d0     EB 64                    JMP 0x00509136
005090d2     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
005090d8     8A 5E 06                 MOV BL,byte ptr [ESI + 0x6]
005090db     8B D0                    MOV EDX,EAX
005090dd     81 E2 FF 00 00 00        AND EDX,0xff
005090e3     F6 C3 80                 TEST BL,0x80
005090e6     74 0E                    JZ 0x005090f6
005090e8     FE C8                    DEC AL
005090ea     88 81 51 01 00 00        MOV byte ptr [ECX + 0x151],AL
005090f0     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
005090f6     F6 46 06 20              TEST byte ptr [ESI + 0x6],0x20
005090fa     74 14                    JZ 0x00509110
005090fc     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
00509102     FE C0                    INC AL
00509104     88 81 51 01 00 00        MOV byte ptr [ECX + 0x151],AL
0050910a     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
00509110     8A 99 51 01 00 00        MOV BL,byte ptr [ECX + 0x151]
00509116     33 C0                    XOR EAX,EAX
00509118     80 E3 07                 AND BL,0x7
0050911b     88 99 51 01 00 00        MOV byte ptr [ECX + 0x151],BL
00509121     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
00509127     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
0050912d     3B D0                    CMP EDX,EAX
0050912f     74 05                    JZ 0x00509136
00509131     E8 2A 4A 03 00           CALL 0x0053db60
00509136     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
0050913c     66 0F B6 81 51 01 00 00  MOVZX AX,byte ptr [ECX + 0x151]
00509144     83 E8 04                 SUB EAX,0x4
00509147     C1 E0 09                 SHL EAX,0x9
0050914a     66 2B 05 2A 49 60 00     SUB AX,word ptr [0x0060492a]
00509151     25 FF 0F 00 00           AND EAX,0xfff
00509156     66 3D 00 08              CMP AX,0x800
0050915a     66 89 44 24 42           MOV word ptr [ESP + 0x42],AX
0050915f     7D 08                    JGE 0x00509169
00509161     0F BF C0                 MOVSX EAX,AX
00509164     C1 F8 02                 SAR EAX,0x2
00509167     EB 0F                    JMP 0x00509178
00509169     0F BF D0                 MOVSX EDX,AX
0050916c     B8 00 10 00 00           MOV EAX,0x1000
00509171     2B C2                    SUB EAX,EDX
00509173     C1 F8 02                 SAR EAX,0x2
00509176     F7 D8                    NEG EAX
00509178     66 01 05 2A 49 60 00     ADD word ptr [0x0060492a],AX
0050917f     66 A1 08 93 5E 00        MOV AX,[0x005e9308]
00509185     0F BF D0                 MOVSX EDX,AX
00509188     BE 00 02 00 00           MOV ESI,0x200
0050918d     68 E0 08 60 00           PUSH 0x6008e0
00509192     2B F2                    SUB ESI,EDX
00509194     68 28 49 60 00           PUSH 0x604928
00509199     C1 FE 02                 SAR ESI,0x2
0050919c     66 03 C6                 ADD AX,SI
0050919f     BE 80 03 00 00           MOV ESI,0x380
005091a4     66 A3 08 93 5E 00        MOV [0x005e9308],AX
005091aa     66 A1 0A 93 5E 00        MOV AX,[0x005e930a]
005091b0     0F BF D0                 MOVSX EDX,AX
005091b3     2B F2                    SUB ESI,EDX
005091b5     C1 FE 02                 SAR ESI,0x2
005091b8     66 03 C6                 ADD AX,SI
005091bb     66 A3 0A 93 5E 00        MOV [0x005e930a],AX
005091c1     66 A1 0C 93 5E 00        MOV AX,[0x005e930c]
005091c7     0F BF D0                 MOVSX EDX,AX
005091ca     F7 DA                    NEG EDX
005091cc     C1 FA 02                 SAR EDX,0x2
005091cf     66 03 C2                 ADD AX,DX
005091d2     66 A3 0C 93 5E 00        MOV [0x005e930c],AX
005091d8     33 C0                    XOR EAX,EAX
005091da     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
005091e0     0F BF 14 C5 58 2A 56 00  MOVSX EDX,word ptr [EAX*0x8 + 0x562a58]
005091e8     66 A1 A0 93 5E 00        MOV AX,[0x005e93a0]
005091ee     0F BF F0                 MOVSX ESI,AX
005091f1     2B D6                    SUB EDX,ESI
005091f3     C1 FA 02                 SAR EDX,0x2
005091f6     66 03 C2                 ADD AX,DX
005091f9     66 A3 A0 93 5E 00        MOV [0x005e93a0],AX
005091ff     33 C0                    XOR EAX,EAX
00509201     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
00509207     0F BF 14 C5 5A 2A 56 00  MOVSX EDX,word ptr [EAX*0x8 + 0x562a5a]
0050920f     66 A1 A2 93 5E 00        MOV AX,[0x005e93a2]
00509215     0F BF F0                 MOVSX ESI,AX
00509218     2B D6                    SUB EDX,ESI
0050921a     C1 FA 02                 SAR EDX,0x2
0050921d     66 03 C2                 ADD AX,DX
00509220     66 A3 A2 93 5E 00        MOV [0x005e93a2],AX
00509226     33 C0                    XOR EAX,EAX
00509228     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
0050922e     0F BF 0C C5 5C 2A 56 00  MOVSX ECX,word ptr [EAX*0x8 + 0x562a5c]
00509236     66 A1 A4 93 5E 00        MOV AX,[0x005e93a4]
0050923c     0F BF D0                 MOVSX EDX,AX
0050923f     2B CA                    SUB ECX,EDX
00509241     C1 F9 02                 SAR ECX,0x2
00509244     66 03 C1                 ADD AX,CX
00509247     66 A3 A4 93 5E 00        MOV [0x005e93a4],AX
0050924d     E8 AE 30 01 00           CALL 0x0051c300
00509252     0F BF 05 A4 93 5E 00     MOVSX EAX,word ptr [0x005e93a4]
00509259     0F BF 0D A2 93 5E 00     MOVSX ECX,word ptr [0x005e93a2]
00509260     0F BF 15 A0 93 5E 00     MOVSX EDX,word ptr [0x005e93a0]
00509267     83 C4 08                 ADD ESP,0x8
0050926a     50                       PUSH EAX
0050926b     51                       PUSH ECX
0050926c     52                       PUSH EDX
0050926d     E8 2E C8 03 00           CALL 0x00545aa0
00509272     66 B8 00 08              MOV AX,0x800
00509276     89 6C 24 64              MOV dword ptr [ESP + 0x64],EBP
0050927a     66 2B 05 2A 49 60 00     SUB AX,word ptr [0x0060492a]
00509281     8B 6C 24 60              MOV EBP,dword ptr [ESP + 0x60]
00509285     BE 80 2E 57 00           MOV ESI,0x572e80
0050928a     BF 08 00 00 00           MOV EDI,0x8
0050928f     66 A3 7A 2B 57 00        MOV [0x00572b7a],AX
00509295     8D 4C 24 2C              LEA ECX,[ESP + 0x2c]
00509299     6A 0A                    PUSH 0xa
0050929b     8D 56 F4                 LEA EDX,[ESI + -0xc]
0050929e     51                       PUSH ECX
0050929f     52                       PUSH EDX
005092a0     E8 3B C0 02 00           CALL 0x005352e0
005092a5     8B 54 24 3C              MOV EDX,dword ptr [ESP + 0x3c]
005092a9     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
005092af     81 C2 00 80 02 00        ADD EDX,0x28000
005092b5     33 C0                    XOR EAX,EAX
005092b7     89 54 24 3C              MOV dword ptr [ESP + 0x3c],EDX
005092bb     8A 81 51 01 00 00        MOV AL,byte ptr [ECX + 0x151]
005092c1     8B 4C 24 70              MOV ECX,dword ptr [ESP + 0x70]
005092c5     83 C4 0C                 ADD ESP,0xc
005092c8     3B C8                    CMP ECX,EAX
005092ca     66 8B 06                 MOV AX,word ptr [ESI]
005092cd     0F BF D0                 MOVSX EDX,AX
005092d0     75 2F                    JNZ 0x00509301
005092d2     B9 00 18 00 00           MOV ECX,0x1800
005092d7     C7 46 10 00 00 00 01     MOV dword ptr [ESI + 0x10],0x1000000
005092de     2B CA                    SUB ECX,EDX
005092e0     C1 F9 02                 SAR ECX,0x2
005092e3     03 C8                    ADD ECX,EAX
005092e5     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
005092e9     0F BF D0                 MOVSX EDX,AX
005092ec     66 89 0E                 MOV word ptr [ESI],CX
005092ef     B9 00 18 00 00           MOV ECX,0x1800
005092f4     2B CA                    SUB ECX,EDX
005092f6     C1 F9 02                 SAR ECX,0x2
005092f9     03 C8                    ADD ECX,EAX
005092fb     66 89 4E 02              MOV word ptr [ESI + 0x2],CX
005092ff     EB 2D                    JMP 0x0050932e
00509301     B9 00 10 00 00           MOV ECX,0x1000
00509306     C7 46 10 00 00 00 03     MOV dword ptr [ESI + 0x10],0x3000000
0050930d     2B CA                    SUB ECX,EDX
0050930f     C1 F9 02                 SAR ECX,0x2
00509312     03 C8                    ADD ECX,EAX
00509314     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
00509318     0F BF D0                 MOVSX EDX,AX
0050931b     66 89 0E                 MOV word ptr [ESI],CX
0050931e     B9 00 10 00 00           MOV ECX,0x1000
00509323     2B CA                    SUB ECX,EDX
00509325     C1 F9 02                 SAR ECX,0x2
00509328     03 C8                    ADD ECX,EAX
0050932a     66 89 4E 02              MOV word ptr [ESI + 0x2],CX
0050932e     8D 54 24 10              LEA EDX,[ESP + 0x10]
00509332     8D 44 24 2C              LEA EAX,[ESP + 0x2c]
00509336     52                       PUSH EDX
00509337     50                       PUSH EAX
00509338     E8 13 63 01 00           CALL 0x0051f650
0050933d     83 C4 08                 ADD ESP,0x8
00509340     85 C0                    TEST EAX,EAX
00509342     0F 84 C3 00 00 00        JZ 0x0050940b
00509348     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050934e     A1 60 99 5E 00           MOV EAX,[0x005e9960]
00509353     D1 E8                    SHR EAX,0x1
00509355     8B 91 94 00 00 00        MOV EDX,dword ptr [ECX + 0x94]
0050935b     BB 06 00 00 00           MOV EBX,0x6
00509360     6A 00                    PUSH 0x0
00509362     6A 00                    PUSH 0x0
00509364     8B 0C 3A                 MOV ECX,dword ptr [EDX + EDI*0x1]
00509367     33 D2                    XOR EDX,EDX
00509369     F7 F3                    DIV EBX
0050936b     89 4C 24 20              MOV dword ptr [ESP + 0x20],ECX
0050936f     0F BF C2                 MOVSX EAX,DX
00509372     66 89 54 24 30           MOV word ptr [ESP + 0x30],DX
00509377     8A 54 C1 02              MOV DL,byte ptr [ECX + EAX*0x8 + 0x2]
0050937b     8D 44 C1 02              LEA EAX,[ECX + EAX*0x8 + 0x2]
0050937f     FE CA                    DEC DL
00509381     88 10                    MOV byte ptr [EAX],DL
00509383     8B 54 24 20              MOV EDX,dword ptr [ESP + 0x20]
00509387     0F BF 4C 24 30           MOVSX ECX,word ptr [ESP + 0x30]
0050938c     8D 44 CA 03              LEA EAX,[EDX + ECX*0x8 + 0x3]
00509390     8A 4C CA 03              MOV CL,byte ptr [EDX + ECX*0x8 + 0x3]
00509394     FE C9                    DEC CL
00509396     88 08                    MOV byte ptr [EAX],CL
00509398     66 8B 06                 MOV AX,word ptr [ESI]
0050939b     66 8B 4E 02              MOV CX,word ptr [ESI + 0x2]
0050939f     66 8B 54 24 18           MOV DX,word ptr [ESP + 0x18]
005093a4     66 89 44 24 2C           MOV word ptr [ESP + 0x2c],AX
005093a9     66 8B 44 24 1A           MOV AX,word ptr [ESP + 0x1a]
005093ae     66 89 4C 24 2E           MOV word ptr [ESP + 0x2e],CX
005093b3     8B 4E 10                 MOV ECX,dword ptr [ESI + 0x10]
005093b6     66 89 44 24 2A           MOV word ptr [ESP + 0x2a],AX
005093bb     66 89 54 24 28           MOV word ptr [ESP + 0x28],DX
005093c0     0F BF 44 24 1C           MOVSX EAX,word ptr [ESP + 0x1c]
005093c5     89 4C 24 24              MOV dword ptr [ESP + 0x24],ECX
005093c9     8B 4C 24 60              MOV ECX,dword ptr [ESP + 0x60]
005093cd     8D 54 24 20              LEA EDX,[ESP + 0x20]
005093d1     52                       PUSH EDX
005093d2     8D 14 81                 LEA EDX,[ECX + EAX*0x4]
005093d5     55                       PUSH EBP
005093d6     52                       PUSH EDX
005093d7     E8 44 8B 01 00           CALL 0x00521f20
005093dc     8B 4C 24 2C              MOV ECX,dword ptr [ESP + 0x2c]
005093e0     8B E8                    MOV EBP,EAX
005093e2     0F BF 44 24 3C           MOVSX EAX,word ptr [ESP + 0x3c]
005093e7     8A 54 C1 02              MOV DL,byte ptr [ECX + EAX*0x8 + 0x2]
005093eb     83 C4 14                 ADD ESP,0x14
005093ee     8D 44 C1 02              LEA EAX,[ECX + EAX*0x8 + 0x2]
005093f2     FE C2                    INC DL
005093f4     88 10                    MOV byte ptr [EAX],DL
005093f6     8B 44 24 18              MOV EAX,dword ptr [ESP + 0x18]
005093fa     0F BF 54 24 28           MOVSX EDX,word ptr [ESP + 0x28]
005093ff     8A 4C D0 03              MOV CL,byte ptr [EAX + EDX*0x8 + 0x3]
00509403     8D 44 D0 03              LEA EAX,[EAX + EDX*0x8 + 0x3]
00509407     FE C1                    INC CL
00509409     88 08                    MOV byte ptr [EAX],CL
0050940b     8B 5C 24 64              MOV EBX,dword ptr [ESP + 0x64]
0050940f     83 C7 04                 ADD EDI,0x4
00509412     43                       INC EBX
00509413     81 C6 9C 00 00 00        ADD ESI,0x9c
00509419     83 FF 28                 CMP EDI,0x28
0050941c     89 5C 24 64              MOV dword ptr [ESP + 0x64],EBX
00509420     0F 8C 6F FE FF FF        JL 0x00509295
00509426     8B 15 38 33 57 00        MOV EDX,dword ptr [0x00573338]
0050942c     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
00509431     33 C9                    XOR ECX,ECX
00509433     6A 00                    PUSH 0x0
00509435     8A 8A 51 01 00 00        MOV CL,byte ptr [EDX + 0x151]
0050943b     8B 50 70                 MOV EDX,dword ptr [EAX + 0x70]
0050943e     68 80 21 56 00           PUSH 0x562180
00509443     6A 00                    PUSH 0x0
00509445     8B 84 8A C4 00 00 00     MOV EAX,dword ptr [EDX + ECX*0x4 + 0xc4]
0050944c     50                       PUSH EAX
0050944d     E8 FE AE 03 00           CALL 0x00544350
00509452     83 C4 10                 ADD ESP,0x10
00509455     68 80 21 56 00           PUSH 0x562180
0050945a     55                       PUSH EBP
0050945b     E8 40 AF 03 00           CALL 0x005443a0
00509460     83 C4 08                 ADD ESP,0x8
00509463     5F                       POP EDI
00509464     5E                       POP ESI
00509465     5D                       POP EBP
00509466     5B                       POP EBX
00509467     83 C4 44                 ADD ESP,0x44
0050946a     C3                       RET
