; Function: FUN_00507cb0
; Entry:    00507cb0
; Body:     [[00507cb0, 00508406]]

; --------------------------------------------------
; Range: 00507cb0 - 00508406
; --------------------------------------------------
00507cb0     A1 48 2A 56 00           MOV        EAX,[0x00562a48]
00507cb5     83 EC 28                 SUB        ESP,0x28
00507cb8     53                       PUSH       EBX
00507cb9     55                       PUSH       EBP
00507cba     33 ED                    XOR        EBP,EBP
00507cbc     56                       PUSH       ESI
00507cbd     3B C5                    CMP        EAX,EBP
00507cbf     57                       PUSH       EDI
00507cc0     74 6D                    JZ         0x00507d2f
00507cc2     6A 01                    PUSH       0x1
00507cc4     E8 EF DD 03 00           CALL       0x00545ab8
00507cc9     E8 62 B8 01 00           CALL       0x00523530
00507cce     89 2D 48 2A 56 00        MOV        dword ptr [0x00562a48],EBP
00507cd4     89 2D F8 8D 57 00        MOV        dword ptr [0x00578df8],EBP
00507cda     89 2D FC 8D 57 00        MOV        dword ptr [0x00578dfc],EBP
00507ce0     89 2D F4 8D 57 00        MOV        dword ptr [0x00578df4],EBP
00507ce6     66 C7 05 1C 35 57 00 28 00 MOV        word ptr [0x0057351c],0x28
00507cef     66 C7 05 1E 35 57 00 38 00 MOV        word ptr [0x0057351e],0x38
00507cf8     66 C7 05 20 35 57 00 E0 00 MOV        word ptr [0x00573520],0xe0
00507d01     66 C7 05 22 35 57 00 68 00 MOV        word ptr [0x00573522],0x68
00507d0a     C6 05 2A 35 57 00 20     MOV        byte ptr [0x0057352a],0x20
00507d11     C7 05 38 35 57 00 28 0C 07 01 MOV        dword ptr [0x00573538],0x1070c28
00507d1b     C7 05 3C 35 57 00 00 00 00 01 MOV        dword ptr [0x0057353c],0x1000000
00507d25     C7 05 50 2A 56 00 02 00 00 00 MOV        dword ptr [0x00562a50],0x2
00507d2f     A1 38 33 57 00           MOV        EAX,[0x00573338]
00507d34     BE 08 00 00 00           MOV        ESI,0x8
00507d39     66 0F B6 80 50 01 00 00  MOVZX      AX,byte ptr [EAX + 0x150]
00507d41     2B C6                    SUB        EAX,ESI
00507d43     C1 E0 08                 SHL        EAX,0x8
00507d46     66 2B 05 2A 49 60 00     SUB        AX,word ptr [0x0060492a]
00507d4d     25 FF 0F 00 00           AND        EAX,0xfff
00507d52     66 3D 00 08              CMP        AX,0x800
00507d56     7D 08                    JGE        0x00507d60
00507d58     0F BF C0                 MOVSX      EAX,AX
00507d5b     C1 F8 02                 SAR        EAX,0x2
00507d5e     EB 0F                    JMP        0x00507d6f
00507d60     0F BF C8                 MOVSX      ECX,AX
00507d63     B8 00 10 00 00           MOV        EAX,0x1000
00507d68     2B C1                    SUB        EAX,ECX
00507d6a     C1 F8 02                 SAR        EAX,0x2
00507d6d     F7 D8                    NEG        EAX
00507d6f     66 01 05 2A 49 60 00     ADD        word ptr [0x0060492a],AX
00507d76     66 A1 A0 93 5E 00        MOV        AX,[0x005e93a0]
00507d7c     0F BF D0                 MOVSX      EDX,AX
00507d7f     B9 60 00 00 00           MOV        ECX,0x60
00507d84     68 E0 08 60 00           PUSH       0x6008e0
00507d89     2B CA                    SUB        ECX,EDX
00507d8b     68 28 49 60 00           PUSH       0x604928
00507d90     C1 F9 02                 SAR        ECX,0x2
00507d93     66 03 C1                 ADD        AX,CX
00507d96     B9 60 00 00 00           MOV        ECX,0x60
00507d9b     66 A3 A0 93 5E 00        MOV        [0x005e93a0],AX
00507da1     66 A1 A2 93 5E 00        MOV        AX,[0x005e93a2]
00507da7     0F BF D0                 MOVSX      EDX,AX
00507daa     2B CA                    SUB        ECX,EDX
00507dac     C1 F9 02                 SAR        ECX,0x2
00507daf     66 03 C1                 ADD        AX,CX
00507db2     B9 60 00 00 00           MOV        ECX,0x60
00507db7     66 A3 A2 93 5E 00        MOV        [0x005e93a2],AX
00507dbd     66 A1 A4 93 5E 00        MOV        AX,[0x005e93a4]
00507dc3     0F BF D0                 MOVSX      EDX,AX
00507dc6     2B CA                    SUB        ECX,EDX
00507dc8     C1 F9 02                 SAR        ECX,0x2
00507dcb     66 03 C1                 ADD        AX,CX
00507dce     66 A3 A4 93 5E 00        MOV        [0x005e93a4],AX
00507dd4     E8 27 45 01 00           CALL       0x0051c300
00507dd9     0F BF 15 A4 93 5E 00     MOVSX      EDX,word ptr [0x005e93a4]
00507de0     0F BF 05 A2 93 5E 00     MOVSX      EAX,word ptr [0x005e93a2]
00507de7     0F BF 0D A0 93 5E 00     MOVSX      ECX,word ptr [0x005e93a0]
00507dee     83 C4 08                 ADD        ESP,0x8
00507df1     52                       PUSH       EDX
00507df2     50                       PUSH       EAX
00507df3     51                       PUSH       ECX
00507df4     E8 A7 DC 03 00           CALL       0x00545aa0
00507df9     A1 38 33 57 00           MOV        EAX,[0x00573338]
00507dfe     33 D2                    XOR        EDX,EDX
00507e00     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
00507e06     66 A1 7A 2B 57 00        MOV        AX,[0x00572b7a]
00507e0c     0F BF C8                 MOVSX      ECX,AX
00507e0f     C1 E2 08                 SHL        EDX,0x8
00507e12     F7 D9                    NEG        ECX
00507e14     2B CA                    SUB        ECX,EDX
00507e16     C1 F9 02                 SAR        ECX,0x2
00507e19     66 03 C1                 ADD        AX,CX
00507e1c     66 A3 7A 2B 57 00        MOV        [0x00572b7a],AX
00507e22     A1 4C 2A 56 00           MOV        EAX,[0x00562a4c]
00507e27     3B C5                    CMP        EAX,EBP
00507e29     0F 85 DB 03 00 00        JNZ        0x0050820a
00507e2f     E8 7E DC 03 00           CALL       0x00545ab2
00507e34     8B D8                    MOV        EBX,EAX
00507e36     3B DD                    CMP        EBX,EBP
00507e38     0F 84 37 01 00 00        JZ         0x00507f75
00507e3e     83 FB 0D                 CMP        EBX,0xd
00507e41     0F 85 91 00 00 00        JNZ        0x00507ed8
00507e47     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
00507e4c     C7 05 FC 8D 57 00 07 00 00 00 MOV        dword ptr [0x00578dfc],0x7
00507e56     3B C5                    CMP        EAX,EBP
00507e58     75 50                    JNZ        0x00507eaa
00507e5a     A1 38 33 57 00           MOV        EAX,[0x00573338]
00507e5f     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
00507e65     33 D2                    XOR        EDX,EDX
00507e67     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
00507e6d     8B 41 70                 MOV        EAX,dword ptr [ECX + 0x70]
00507e70     33 C9                    XOR        ECX,ECX
00507e72     8B 94 90 08 02 00 00     MOV        EDX,dword ptr [EAX + EDX*0x4 + 0x208]
00507e79     8A 02                    MOV        AL,byte ptr [EDX]
00507e7b     84 C0                    TEST       AL,AL
00507e7d     74 15                    JZ         0x00507e94
00507e7f     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
00507e85     41                       INC        ECX
00507e86     42                       INC        EDX
00507e87     88 84 31 FF 00 00 00     MOV        byte ptr [ECX + ESI*0x1 + 0xff],AL
00507e8e     8A 02                    MOV        AL,byte ptr [EDX]
00507e90     84 C0                    TEST       AL,AL
00507e92     75 EB                    JNZ        0x00507e7f
00507e94     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00507e9a     C6 84 11 00 01 00 00 00  MOV        byte ptr [ECX + EDX*0x1 + 0x100],0x0
00507ea2     D1 F9                    SAR        ECX,0x1
00507ea4     89 0D 00 8E 57 00        MOV        dword ptr [0x00578e00],ECX
00507eaa     E8 D1 5C 03 00           CALL       0x0053db80
00507eaf     8B 0D E8 8D 57 00        MOV        ECX,dword ptr [0x00578de8]
00507eb5     55                       PUSH       EBP
00507eb6     83 C1 02                 ADD        ECX,0x2
00507eb9     C7 05 F0 8D 57 00 5F 00 00 00 MOV        dword ptr [0x00578df0],0x5f
00507ec3     89 0D E8 8D 57 00        MOV        dword ptr [0x00578de8],ECX
00507ec9     E8 EA DB 03 00           CALL       0x00545ab8
00507ece     E8 4D B6 01 00           CALL       0x00523520
00507ed3     E9 32 03 00 00           JMP        0x0050820a
00507ed8     3B DE                    CMP        EBX,ESI
00507eda     75 2B                    JNZ        0x00507f07
00507edc     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
00507ee1     3B C5                    CMP        EAX,EBP
00507ee3     0F 84 21 03 00 00        JZ         0x0050820a
00507ee9     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00507eef     48                       DEC        EAX
00507ef0     A3 00 8E 57 00           MOV        [0x00578e00],EAX
00507ef5     C6 84 01 00 01 00 00 00  MOV        byte ptr [ECX + EAX*0x1 + 0x100],0x0
00507efd     E8 9E 5C 03 00           CALL       0x0053dba0
00507f02     E9 03 03 00 00           JMP        0x0050820a
00507f07     83 FB 1B                 CMP        EBX,0x1b
00507f0a     0F 84 DC 02 00 00        JZ         0x005081ec
00507f10     83 FB 20                 CMP        EBX,0x20
00507f13     0F 8C F1 02 00 00        JL         0x0050820a
00507f19     81 FB 80 00 00 00        CMP        EBX,0x80
00507f1f     7C 0C                    JL         0x00507f2d
00507f21     81 FB A0 00 00 00        CMP        EBX,0xa0
00507f27     0F 8C DD 02 00 00        JL         0x0050820a
00507f2d     39 35 00 8E 57 00        CMP        dword ptr [0x00578e00],ESI
00507f33     0F 8D D1 02 00 00        JGE        0x0050820a
00507f39     E8 42 5C 03 00           CALL       0x0053db80
00507f3e     8B 15 00 8E 57 00        MOV        EDX,dword ptr [0x00578e00]
00507f44     A1 38 33 57 00           MOV        EAX,[0x00573338]
00507f49     88 9C 10 00 01 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x100],BL
00507f50     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
00507f55     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00507f5b     40                       INC        EAX
00507f5c     A3 00 8E 57 00           MOV        [0x00578e00],EAX
00507f61     C6 84 01 00 01 00 00 00  MOV        byte ptr [ECX + EAX*0x1 + 0x100],0x0
00507f69     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
00507f6e     3B C6                    CMP        EAX,ESI
00507f70     E9 2E 02 00 00           JMP        0x005081a3
00507f75     8B 3D F4 8D 57 00        MOV        EDI,dword ptr [0x00578df4]
00507f7b     8B 0D FC 8D 57 00        MOV        ECX,dword ptr [0x00578dfc]
00507f81     8B 74 24 48              MOV        ESI,dword ptr [ESP + 0x48]
00507f85     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
00507f8a     8B D7                    MOV        EDX,EDI
00507f8c     8A 5E 02                 MOV        BL,byte ptr [ESI + 0x2]
00507f8f     C1 E2 08                 SHL        EDX,0x8
00507f92     0B D1                    OR         EDX,ECX
00507f94     C1 E2 08                 SHL        EDX,0x8
00507f97     0B D0                    OR         EDX,EAX
00507f99     F6 C3 08                 TEST       BL,0x8
00507f9c     74 12                    JZ         0x00507fb0
00507f9e     33 C0                    XOR        EAX,EAX
00507fa0     B9 07 00 00 00           MOV        ECX,0x7
00507fa5     A3 F8 8D 57 00           MOV        [0x00578df8],EAX
00507faa     89 0D FC 8D 57 00        MOV        dword ptr [0x00578dfc],ECX
00507fb0     F6 46 06 80              TEST       byte ptr [ESI + 0x6],0x80
00507fb4     74 33                    JZ         0x00507fe9
00507fb6     3B C5                    CMP        EAX,EBP
00507fb8     74 08                    JZ         0x00507fc2
00507fba     48                       DEC        EAX
00507fbb     A3 F8 8D 57 00           MOV        [0x00578df8],EAX
00507fc0     EB 27                    JMP        0x00507fe9
00507fc2     3B CD                    CMP        ECX,EBP
00507fc4     75 04                    JNZ        0x00507fca
00507fc6     3B FD                    CMP        EDI,EBP
00507fc8     74 1F                    JZ         0x00507fe9
00507fca     C7 05 F8 8D 57 00 09 00 00 00 MOV        dword ptr [0x00578df8],0x9
00507fd4     80 4E 06 10              OR         byte ptr [ESI + 0x6],0x10
00507fd8     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
00507fdd     8B 0D FC 8D 57 00        MOV        ECX,dword ptr [0x00578dfc]
00507fe3     8B 3D F4 8D 57 00        MOV        EDI,dword ptr [0x00578df4]
00507fe9     F6 46 06 20              TEST       byte ptr [ESI + 0x6],0x20
00507fed     BB 40 00 00 00           MOV        EBX,0x40
00507ff2     74 32                    JZ         0x00508026
00507ff4     83 F8 09                 CMP        EAX,0x9
00507ff7     7D 08                    JGE        0x00508001
00507ff9     40                       INC        EAX
00507ffa     A3 F8 8D 57 00           MOV        [0x00578df8],EAX
00507fff     EB 25                    JMP        0x00508026
00508001     83 F9 07                 CMP        ECX,0x7
00508004     7C 05                    JL         0x0050800b
00508006     83 FF 07                 CMP        EDI,0x7
00508009     7D 1B                    JGE        0x00508026
0050800b     89 2D F8 8D 57 00        MOV        dword ptr [0x00578df8],EBP
00508011     66 09 5E 06              OR         word ptr [ESI + 0x6],BX
00508015     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
0050801a     8B 0D FC 8D 57 00        MOV        ECX,dword ptr [0x00578dfc]
00508020     8B 3D F4 8D 57 00        MOV        EDI,dword ptr [0x00578df4]
00508026     F6 46 06 10              TEST       byte ptr [ESI + 0x6],0x10
0050802a     74 18                    JZ         0x00508044
0050802c     3B CD                    CMP        ECX,EBP
0050802e     74 09                    JZ         0x00508039
00508030     49                       DEC        ECX
00508031     89 0D FC 8D 57 00        MOV        dword ptr [0x00578dfc],ECX
00508037     EB 0B                    JMP        0x00508044
00508039     3B FD                    CMP        EDI,EBP
0050803b     74 07                    JZ         0x00508044
0050803d     4F                       DEC        EDI
0050803e     89 3D F4 8D 57 00        MOV        dword ptr [0x00578df4],EDI
00508044     84 5E 06                 TEST       byte ptr [ESI + 0x6],BL
00508047     74 0C                    JZ         0x00508055
00508049     83 F9 07                 CMP        ECX,0x7
0050804c     7D 0A                    JGE        0x00508058
0050804e     41                       INC        ECX
0050804f     89 0D FC 8D 57 00        MOV        dword ptr [0x00578dfc],ECX
00508055     83 F9 07                 CMP        ECX,0x7
00508058     75 07                    JNZ        0x00508061
0050805a     33 C0                    XOR        EAX,EAX
0050805c     A3 F8 8D 57 00           MOV        [0x00578df8],EAX
00508061     8B DF                    MOV        EBX,EDI
00508063     C1 E3 08                 SHL        EBX,0x8
00508066     0B D9                    OR         EBX,ECX
00508068     C1 E3 08                 SHL        EBX,0x8
0050806b     0B D8                    OR         EBX,EAX
0050806d     3B D3                    CMP        EDX,EBX
0050806f     74 16                    JZ         0x00508087
00508071     E8 EA 5A 03 00           CALL       0x0053db60
00508076     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
0050807b     8B 0D FC 8D 57 00        MOV        ECX,dword ptr [0x00578dfc]
00508081     8B 3D F4 8D 57 00        MOV        EDI,dword ptr [0x00578df4]
00508087     66 8B 76 02              MOV        SI,word ptr [ESI + 0x2]
0050808b     F7 C6 00 80 00 00        TEST       ESI,0x8000
00508091     0F 84 20 01 00 00        JZ         0x005081b7
00508097     83 F9 07                 CMP        ECX,0x7
0050809a     75 75                    JNZ        0x00508111
0050809c     39 2D 00 8E 57 00        CMP        dword ptr [0x00578e00],EBP
005080a2     75 50                    JNZ        0x005080f4
005080a4     A1 38 33 57 00           MOV        EAX,[0x00573338]
005080a9     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
005080af     33 D2                    XOR        EDX,EDX
005080b1     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
005080b7     8B 41 70                 MOV        EAX,dword ptr [ECX + 0x70]
005080ba     33 C9                    XOR        ECX,ECX
005080bc     8B 94 90 08 02 00 00     MOV        EDX,dword ptr [EAX + EDX*0x4 + 0x208]
005080c3     8A 02                    MOV        AL,byte ptr [EDX]
005080c5     84 C0                    TEST       AL,AL
005080c7     74 15                    JZ         0x005080de
005080c9     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
005080cf     41                       INC        ECX
005080d0     42                       INC        EDX
005080d1     88 84 31 FF 00 00 00     MOV        byte ptr [ECX + ESI*0x1 + 0xff],AL
005080d8     8A 02                    MOV        AL,byte ptr [EDX]
005080da     84 C0                    TEST       AL,AL
005080dc     75 EB                    JNZ        0x005080c9
005080de     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
005080e4     C6 84 11 00 01 00 00 00  MOV        byte ptr [ECX + EDX*0x1 + 0x100],0x0
005080ec     D1 F9                    SAR        ECX,0x1
005080ee     89 0D 00 8E 57 00        MOV        dword ptr [0x00578e00],ECX
005080f4     E8 87 5A 03 00           CALL       0x0053db80
005080f9     8B 0D E8 8D 57 00        MOV        ECX,dword ptr [0x00578de8]
005080ff     C7 05 F0 8D 57 00 5F 00 00 00 MOV        dword ptr [0x00578df0],0x5f
00508109     83 C1 02                 ADD        ECX,0x2
0050810c     E9 ED 00 00 00           JMP        0x005081fe
00508111     8B 15 00 8E 57 00        MOV        EDX,dword ptr [0x00578e00]
00508117     83 FA 08                 CMP        EDX,0x8
0050811a     0F 8D EA 00 00 00        JGE        0x0050820a
00508120     8B 35 C4 97 5E 00        MOV        ESI,dword ptr [0x005e97c4]
00508126     03 CF                    ADD        ECX,EDI
00508128     8D 3C 40                 LEA        EDI,[EAX + EAX*0x2]
0050812b     33 DB                    XOR        EBX,EBX
0050812d     8B 76 70                 MOV        ESI,dword ptr [ESI + 0x70]
00508130     8B 4C 8E 50              MOV        ECX,dword ptr [ESI + ECX*0x4 + 0x50]
00508134     03 CF                    ADD        ECX,EDI
00508136     83 F8 05                 CMP        EAX,0x5
00508139     0F 9C C3                 SETL       BL
0050813c     4B                       DEC        EBX
0050813d     33 C0                    XOR        EAX,EAX
0050813f     83 E3 02                 AND        EBX,0x2
00508142     8A 04 19                 MOV        AL,byte ptr [ECX + EBX*0x1]
00508145     8B D8                    MOV        EBX,EAX
00508147     81 E3 FF FF 00 00        AND        EBX,0xffff
0050814d     3B D5                    CMP        EDX,EBP
0050814f     75 1A                    JNZ        0x0050816b
00508151     8B 0D 9C 2C 56 00        MOV        ECX,dword ptr [0x00562c9c]
00508157     81 E1 FF FF 00 00        AND        ECX,0xffff
0050815d     3B D9                    CMP        EBX,ECX
0050815f     75 0A                    JNZ        0x0050816b
00508161     E8 5A 5A 03 00           CALL       0x0053dbc0
00508166     E9 9F 00 00 00           JMP        0x0050820a
0050816b     E8 10 5A 03 00           CALL       0x0053db80
00508170     8B 15 00 8E 57 00        MOV        EDX,dword ptr [0x00578e00]
00508176     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050817b     88 9C 10 00 01 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x100],BL
00508182     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
00508187     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050818d     40                       INC        EAX
0050818e     A3 00 8E 57 00           MOV        [0x00578e00],EAX
00508193     C6 84 01 00 01 00 00 00  MOV        byte ptr [ECX + EAX*0x1 + 0x100],0x0
0050819b     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
005081a0     83 F8 08                 CMP        EAX,0x8
005081a3     75 65                    JNZ        0x0050820a
005081a5     89 2D F8 8D 57 00        MOV        dword ptr [0x00578df8],EBP
005081ab     C7 05 FC 8D 57 00 07 00 00 00 MOV        dword ptr [0x00578dfc],0x7
005081b5     EB 53                    JMP        0x0050820a
005081b7     F7 C6 00 40 00 00        TEST       ESI,0x4000
005081bd     74 4B                    JZ         0x0050820a
005081bf     A1 00 8E 57 00           MOV        EAX,[0x00578e00]
005081c4     3B C5                    CMP        EAX,EBP
005081c6     74 1B                    JZ         0x005081e3
005081c8     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
005081ce     48                       DEC        EAX
005081cf     A3 00 8E 57 00           MOV        [0x00578e00],EAX
005081d4     C6 84 02 00 01 00 00 00  MOV        byte ptr [EDX + EAX*0x1 + 0x100],0x0
005081dc     E8 BF 59 03 00           CALL       0x0053dba0
005081e1     EB 27                    JMP        0x0050820a
005081e3     83 F9 07                 CMP        ECX,0x7
005081e6     75 22                    JNZ        0x0050820a
005081e8     3B F9                    CMP        EDI,ECX
005081ea     75 1E                    JNZ        0x0050820a
005081ec     E8 AF 59 03 00           CALL       0x0053dba0
005081f1     8B 0D E8 8D 57 00        MOV        ECX,dword ptr [0x00578de8]
005081f7     89 2D F0 8D 57 00        MOV        dword ptr [0x00578df0],EBP
005081fd     49                       DEC        ECX
005081fe     55                       PUSH       EBP
005081ff     89 0D E8 8D 57 00        MOV        dword ptr [0x00578de8],ECX
00508205     E8 AE D8 03 00           CALL       0x00545ab8
0050820a     55                       PUSH       EBP
0050820b     68 B8 18 56 00           PUSH       0x5618b8
00508210     55                       PUSH       EBP
00508211     68 F8 06 56 00           PUSH       0x5606f8
00508216     E8 35 C1 03 00           CALL       0x00544350
0050821b     8B 44 24 54              MOV        EAX,dword ptr [ESP + 0x54]
0050821f     83 C4 10                 ADD        ESP,0x10
00508222     68 B8 18 56 00           PUSH       0x5618b8
00508227     50                       PUSH       EAX
00508228     E8 73 C1 03 00           CALL       0x005443a0
0050822d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00508233     89 44 24 4C              MOV        dword ptr [ESP + 0x4c],EAX
00508237     33 C0                    XOR        EAX,EAX
00508239     83 C4 08                 ADD        ESP,0x8
0050823c     8D B9 00 01 00 00        LEA        EDI,[ECX + 0x100]
00508242     83 C9 FF                 OR         ECX,0xffffffff
00508245     F2 AE                    SCASB.REPNE ES:EDI
00508247     F7 D1                    NOT        ECX
00508249     2B F9                    SUB        EDI,ECX
0050824b     8D 54 24 24              LEA        EDX,[ESP + 0x24]
0050824f     8B C1                    MOV        EAX,ECX
00508251     8B F7                    MOV        ESI,EDI
00508253     8B FA                    MOV        EDI,EDX
00508255     C1 E9 02                 SHR        ECX,0x2
00508258     F3 A5                    MOVSD.REP  ES:EDI,ESI
0050825a     8B C8                    MOV        ECX,EAX
0050825c     A1 FC 8D 57 00           MOV        EAX,[0x00578dfc]
00508261     83 E1 03                 AND        ECX,0x3
00508264     83 F8 07                 CMP        EAX,0x7
00508267     F3 A4                    MOVSB.REP  ES:EDI,ESI
00508269     74 2E                    JZ         0x00508299
0050826b     BF 98 2C 56 00           MOV        EDI,0x562c98
00508270     83 C9 FF                 OR         ECX,0xffffffff
00508273     33 C0                    XOR        EAX,EAX
00508275     8D 54 24 24              LEA        EDX,[ESP + 0x24]
00508279     F2 AE                    SCASB.REPNE ES:EDI
0050827b     F7 D1                    NOT        ECX
0050827d     2B F9                    SUB        EDI,ECX
0050827f     8B F7                    MOV        ESI,EDI
00508281     8B D9                    MOV        EBX,ECX
00508283     8B FA                    MOV        EDI,EDX
00508285     83 C9 FF                 OR         ECX,0xffffffff
00508288     F2 AE                    SCASB.REPNE ES:EDI
0050828a     8B CB                    MOV        ECX,EBX
0050828c     4F                       DEC        EDI
0050828d     C1 E9 02                 SHR        ECX,0x2
00508290     F3 A5                    MOVSD.REP  ES:EDI,ESI
00508292     8B CB                    MOV        ECX,EBX
00508294     83 E1 03                 AND        ECX,0x3
00508297     F3 A4                    MOVSB.REP  ES:EDI,ESI
00508299     55                       PUSH       EBP
0050829a     68 B8 18 56 00           PUSH       0x5618b8
0050829f     8D 44 24 2C              LEA        EAX,[ESP + 0x2c]
005082a3     55                       PUSH       EBP
005082a4     50                       PUSH       EAX
005082a5     E8 A6 C0 03 00           CALL       0x00544350
005082aa     8B 4C 24 54              MOV        ECX,dword ptr [ESP + 0x54]
005082ae     83 C4 10                 ADD        ESP,0x10
005082b1     68 B8 18 56 00           PUSH       0x5618b8
005082b6     51                       PUSH       ECX
005082b7     E8 E4 C0 03 00           CALL       0x005443a0
005082bc     8B 15 C0 97 5E 00        MOV        EDX,dword ptr [0x005e97c0]
005082c2     89 44 24 4C              MOV        dword ptr [ESP + 0x4c],EAX
005082c6     83 C4 08                 ADD        ESP,0x8
005082c9     8B 7C 24 3C              MOV        EDI,dword ptr [ESP + 0x3c]
005082cd     8B 82 94 00 00 00        MOV        EAX,dword ptr [EDX + 0x94]
005082d3     8B 08                    MOV        ECX,dword ptr [EAX]
005082d5     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
005082da     8B D0                    MOV        EDX,EAX
005082dc     89 4C 24 10              MOV        dword ptr [ESP + 0x10],ECX
005082e0     C1 E2 04                 SHL        EDX,0x4
005082e3     33 C9                    XOR        ECX,ECX
005082e5     83 F8 05                 CMP        EAX,0x5
005082e8     A1 1C 35 57 00           MOV        EAX,[0x0057351c]
005082ed     66 C7 44 24 20 18 00     MOV        word ptr [ESP + 0x20],0x18
005082f4     0F 9C C1                 SETL       CL
005082f7     49                       DEC        ECX
005082f8     03 C2                    ADD        EAX,EDX
005082fa     8B 15 FC 8D 57 00        MOV        EDX,dword ptr [0x00578dfc]
00508300     83 E1 08                 AND        ECX,0x8
00508303     C7 44 24 14 00 00 00 01  MOV        dword ptr [ESP + 0x14],0x1000000
0050830b     8D 4C 01 18              LEA        ECX,[ECX + EAX*0x1 + 0x18]
0050830f     8D 42 01                 LEA        EAX,[EDX + 0x1]
00508312     66 89 4C 24 18           MOV        word ptr [ESP + 0x18],CX
00508317     8D 4C 24 44              LEA        ECX,[ESP + 0x44]
0050831b     8D 04 40                 LEA        EAX,[EAX + EAX*0x2]
0050831e     51                       PUSH       ECX
0050831f     C1 E0 02                 SHL        EAX,0x2
00508322     66 03 05 1E 35 57 00     ADD        AX,word ptr [0x0057351e]
00508329     57                       PUSH       EDI
0050832a     66 89 44 24 22           MOV        word ptr [ESP + 0x22],AX
0050832f     E8 EC 3A 01 00           CALL       0x0051be20
00508334     8B 44 24 4C              MOV        EAX,dword ptr [ESP + 0x4c]
00508338     83 C4 08                 ADD        ESP,0x8
0050833b     8D 54 24 10              LEA        EDX,[ESP + 0x10]
0050833f     55                       PUSH       EBP
00508340     55                       PUSH       EBP
00508341     52                       PUSH       EDX
00508342     50                       PUSH       EAX
00508343     57                       PUSH       EDI
00508344     E8 A7 9A 01 00           CALL       0x00521df0
00508349     83 C4 14                 ADD        ESP,0x14
0050834c     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
00508350     89 44 24 44              MOV        dword ptr [ESP + 0x44],EAX
00508354     51                       PUSH       ECX
00508355     50                       PUSH       EAX
00508356     57                       PUSH       EDI
00508357     E8 64 76 01 00           CALL       0x0051f9c0
0050835c     83 C4 0C                 ADD        ESP,0xc
0050835f     8D 54 24 44              LEA        EDX,[ESP + 0x44]
00508363     89 44 24 44              MOV        dword ptr [ESP + 0x44],EAX
00508367     52                       PUSH       EDX
00508368     57                       PUSH       EDI
00508369     E8 42 3A 01 00           CALL       0x0051bdb0
0050836e     83 C4 08                 ADD        ESP,0x8
00508371     55                       PUSH       EBP
00508372     68 18 35 57 00           PUSH       0x573518
00508377     55                       PUSH       EBP
00508378     68 F8 06 56 00           PUSH       0x5606f8
0050837d     E8 CE BF 03 00           CALL       0x00544350
00508382     8B 44 24 54              MOV        EAX,dword ptr [ESP + 0x54]
00508386     83 C4 10                 ADD        ESP,0x10
00508389     68 18 35 57 00           PUSH       0x573518
0050838e     50                       PUSH       EAX
0050838f     E8 0C C0 03 00           CALL       0x005443a0
00508394     83 C4 08                 ADD        ESP,0x8
00508397     89 44 24 44              MOV        dword ptr [ESP + 0x44],EAX
0050839b     E8 D0 F4 FF FF           CALL       0x00507870
005083a0     33 F6                    XOR        ESI,ESI
005083a2     8B 0D F4 8D 57 00        MOV        ECX,dword ptr [0x00578df4]
005083a8     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
005083ad     55                       PUSH       EBP
005083ae     68 18 35 57 00           PUSH       0x573518
005083b3     8D 14 0E                 LEA        EDX,[ESI + ECX*0x1]
005083b6     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
005083b9     55                       PUSH       EBP
005083ba     8B 54 91 50              MOV        EDX,dword ptr [ECX + EDX*0x4 + 0x50]
005083be     52                       PUSH       EDX
005083bf     E8 8C BF 03 00           CALL       0x00544350
005083c4     8B 44 24 54              MOV        EAX,dword ptr [ESP + 0x54]
005083c8     83 C4 10                 ADD        ESP,0x10
005083cb     68 18 35 57 00           PUSH       0x573518
005083d0     50                       PUSH       EAX
005083d1     E8 CA BF 03 00           CALL       0x005443a0
005083d6     83 C4 08                 ADD        ESP,0x8
005083d9     46                       INC        ESI
005083da     83 FE 08                 CMP        ESI,0x8
005083dd     89 44 24 44              MOV        dword ptr [ESP + 0x44],EAX
005083e1     7C BF                    JL         0x005083a2
005083e3     68 18 35 57 00           PUSH       0x573518
005083e8     50                       PUSH       EAX
005083e9     57                       PUSH       EDI
005083ea     E8 51 CF 03 00           CALL       0x00545340
005083ef     83 C4 0C                 ADD        ESP,0xc
005083f2     89 44 24 44              MOV        dword ptr [ESP + 0x44],EAX
005083f6     E8 95 F4 FF FF           CALL       0x00507890
005083fb     8B 44 24 44              MOV        EAX,dword ptr [ESP + 0x44]
005083ff     5F                       POP        EDI
00508400     5E                       POP        ESI
00508401     5D                       POP        EBP
00508402     5B                       POP        EBX
00508403     83 C4 28                 ADD        ESP,0x28
00508406     C3                       RET        

