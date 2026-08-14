; Function: FUN_00506880
; Entry:    00506880
; Body:     [[00506880, 0050700e]]

; --------------------------------------------------
; Range: 00506880 - 0050700e
; --------------------------------------------------
00506880     83 EC 20                 SUB        ESP,0x20
00506883     8D 44 24 00              LEA        EAX,[ESP]
00506887     8D 4C 24 08              LEA        ECX,[ESP + 0x8]
0050688b     8D 54 24 04              LEA        EDX,[ESP + 0x4]
0050688f     53                       PUSH       EBX
00506890     55                       PUSH       EBP
00506891     56                       PUSH       ESI
00506892     57                       PUSH       EDI
00506893     50                       PUSH       EAX
00506894     51                       PUSH       ECX
00506895     52                       PUSH       EDX
00506896     E8 45 39 02 00           CALL       0x0052a1e0
0050689b     83 C4 0C                 ADD        ESP,0xc
0050689e     68 90 01 00 00           PUSH       0x190
005068a3     E8 04 F2 03 00           CALL       0x00545aac
005068a8     6A 78                    PUSH       0x78
005068aa     68 A0 00 00 00           PUSH       0xa0
005068af     E8 F2 F1 03 00           CALL       0x00545aa6
005068b4     6A 04                    PUSH       0x4
005068b6     E8 15 32 02 00           CALL       0x00529ad0
005068bb     83 C4 04                 ADD        ESP,0x4
005068be     33 D2                    XOR        EDX,EDX
005068c0     33 F6                    XOR        ESI,ESI
005068c2     B8 20 35 57 00           MOV        EAX,0x573520
005068c7     BF 00 00 0F 00           MOV        EDI,0xf0000
005068cc     BD 00 0C 0F 00           MOV        EBP,0xf0c00
005068d1     BB 00 00 0F 01           MOV        EBX,0x10f0000
005068d6     EB 02                    JMP        0x005068da
005068d8     33 D2                    XOR        EDX,EDX
005068da     8D 4E 03                 LEA        ECX,[ESI + 0x3]
005068dd     89 50 FC                 MOV        dword ptr [EAX + -0x4],EDX
005068e0     89 48 0C                 MOV        dword ptr [EAX + 0xc],ECX
005068e3     B9 00 00 00 01           MOV        ECX,0x1000000
005068e8     89 10                    MOV        dword ptr [EAX],EDX
005068ea     89 50 04                 MOV        dword ptr [EAX + 0x4],EDX
005068ed     89 48 1C                 MOV        dword ptr [EAX + 0x1c],ECX
005068f0     89 50 20                 MOV        dword ptr [EAX + 0x20],EDX
005068f3     89 48 24                 MOV        dword ptr [EAX + 0x24],ECX
005068f6     89 48 2C                 MOV        dword ptr [EAX + 0x2c],ECX
005068f9     89 48 34                 MOV        dword ptr [EAX + 0x34],ECX
005068fc     89 48 3C                 MOV        dword ptr [EAX + 0x3c],ECX
005068ff     C7 40 F8 31 00 00 00     MOV        dword ptr [EAX + -0x8],0x31
00506906     C7 40 08 00 00 18 10     MOV        dword ptr [EAX + 0x8],0x10180000
0050690d     C7 40 10 08 08 0F 01     MOV        dword ptr [EAX + 0x10],0x10f0808
00506914     C7 40 14 20 10 CF 02     MOV        dword ptr [EAX + 0x14],0x2cf1020
0050691b     C7 40 18 10 0C 07 01     MOV        dword ptr [EAX + 0x18],0x1070c10
00506922     89 78 28                 MOV        dword ptr [EAX + 0x28],EDI
00506925     89 68 30                 MOV        dword ptr [EAX + 0x30],EBP
00506928     89 58 38                 MOV        dword ptr [EAX + 0x38],EBX
0050692b     8D 48 44                 LEA        ECX,[EAX + 0x44]
0050692e     BA 04 00 00 00           MOV        EDX,0x4
00506933     89 79 FC                 MOV        dword ptr [ECX + -0x4],EDI
00506936     C7 01 00 00 00 01        MOV        dword ptr [ECX],0x1000000
0050693c     83 C1 08                 ADD        ECX,0x8
0050693f     4A                       DEC        EDX
00506940     75 F1                    JNZ        0x00506933
00506942     46                       INC        ESI
00506943     05 C8 08 00 00           ADD        EAX,0x8c8
00506948     83 FE 09                 CMP        ESI,0x9
0050694b     7C 8B                    JL         0x005068d8
0050694d     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
00506953     33 ED                    XOR        EBP,EBP
00506955     8B 0A                    MOV        ECX,dword ptr [EDX]
00506957     03 CF                    ADD        ECX,EDI
00506959     33 C0                    XOR        EAX,EAX
0050695b     89 0D 38 33 57 00        MOV        dword ptr [0x00573338],ECX
00506961     EB 06                    JMP        0x00506969
00506963     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00506969     89 2C 01                 MOV        dword ptr [ECX + EAX*0x1],EBP
0050696c     83 C0 04                 ADD        EAX,0x4
0050696f     3D 00 20 00 00           CMP        EAX,0x2000
00506974     7C ED                    JL         0x00506963
00506976     55                       PUSH       EBP
00506977     E8 94 E1 FF FF           CALL       0x00504b10
0050697c     83 C4 04                 ADD        ESP,0x4
0050697f     BB 40 06 00 00           MOV        EBX,0x640
00506984     A1 38 4A 56 00           MOV        EAX,[0x00564a38]
00506989     8B 0D 60 E1 5D 00        MOV        ECX,dword ptr [0x005de160]
0050698f     BE 01 00 00 00           MOV        ESI,0x1
00506994     3B C5                    CMP        EAX,EBP
00506996     89 35 5C E1 5D 00        MOV        dword ptr [0x005de15c],ESI
0050699c     75 13                    JNZ        0x005069b1
0050699e     83 F9 08                 CMP        ECX,0x8
005069a1     7C 0E                    JL         0x005069b1
005069a3     89 2D 58 E1 5D 00        MOV        dword ptr [0x005de158],EBP
005069a9     89 2D 54 E1 5D 00        MOV        dword ptr [0x005de154],EBP
005069af     EB 1C                    JMP        0x005069cd
005069b1     B8 0B 00 00 00           MOV        EAX,0xb
005069b6     C7 05 54 E1 5D 00 A0 00 00 00 MOV        dword ptr [0x005de154],0xa0
005069c0     2B C1                    SUB        EAX,ECX
005069c2     8D 04 40                 LEA        EAX,[EAX + EAX*0x2]
005069c5     C1 E0 02                 SHL        EAX,0x2
005069c8     A3 58 E1 5D 00           MOV        [0x005de158],EAX
005069cd     6A 04                    PUSH       0x4
005069cf     E8 2C 3C F0 FF           CALL       0x0040a600
005069d4     33 C0                    XOR        EAX,EAX
005069d6     33 D2                    XOR        EDX,EDX
005069d8     A0 38 E5 5D 00           MOV        AL,[0x005de538]
005069dd     83 C4 04                 ADD        ESP,0x4
005069e0     8D 0C 80                 LEA        ECX,[EAX + EAX*0x4]
005069e3     C1 E1 03                 SHL        ECX,0x3
005069e6     2B C8                    SUB        ECX,EAX
005069e8     33 C0                    XOR        EAX,EAX
005069ea     8A 14 8D C8 93 5E 00     MOV        DL,byte ptr [ECX*0x4 + 0x5e93c8]
005069f1     6B D2 4C                 IMUL       EDX,EDX,0x4c
005069f4     8A 82 88 91 5E 00        MOV        AL,byte ptr [EDX + 0x5e9188]
005069fa     C1 E0 04                 SHL        EAX,0x4
005069fd     05 20 4A 60 00           ADD        EAX,0x604a20
00506a02     8A 48 02                 MOV        CL,byte ptr [EAX + 0x2]
00506a05     80 E1 09                 AND        CL,0x9
00506a08     80 F9 09                 CMP        CL,0x9
00506a0b     8B 0D E8 8D 57 00        MOV        ECX,dword ptr [0x00578de8]
00506a11     75 15                    JNZ        0x00506a28
00506a13     83 F9 07                 CMP        ECX,0x7
00506a16     0F 85 C5 05 00 00        JNZ        0x00506fe1
00506a1c     39 2D 68 2B 56 00        CMP        dword ptr [0x00562b68],EBP
00506a22     0F 8C B9 05 00 00        JL         0x00506fe1
00506a28     39 0D EC 8D 57 00        CMP        dword ptr [0x00578dec],ECX
00506a2e     74 12                    JZ         0x00506a42
00506a30     89 0D EC 8D 57 00        MOV        dword ptr [0x00578dec],ECX
00506a36     89 35 48 2A 56 00        MOV        dword ptr [0x00562a48],ESI
00506a3c     89 35 4C 2A 56 00        MOV        dword ptr [0x00562a4c],ESI
00506a42     8B 54 24 10              MOV        EDX,dword ptr [ESP + 0x10]
00506a46     50                       PUSH       EAX
00506a47     8B 44 24 1C              MOV        EAX,dword ptr [ESP + 0x1c]
00506a4b     52                       PUSH       EDX
00506a4c     8B 54 24 1C              MOV        EDX,dword ptr [ESP + 0x1c]
00506a50     50                       PUSH       EAX
00506a51     52                       PUSH       EDX
00506a52     FF 14 8D 08 07 56 00     CALL       dword ptr [ECX*0x4 + 0x560708]
00506a59     89 44 24 20              MOV        dword ptr [ESP + 0x20],EAX
00506a5d     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00506a62     83 C4 10                 ADD        ESP,0x10
00506a65     83 F8 FF                 CMP        EAX,-0x1
00506a68     0F 84 79 05 00 00        JZ         0x00506fe7
00506a6e     3B C5                    CMP        EAX,EBP
00506a70     89 2D 4C 2A 56 00        MOV        dword ptr [0x00562a4c],EBP
00506a76     0F 84 45 03 00 00        JZ         0x00506dc1
00506a7c     83 F8 07                 CMP        EAX,0x7
00506a7f     0F 8D 3C 03 00 00        JGE        0x00506dc1
00506a85     83 F8 05                 CMP        EAX,0x5
00506a88     0F 8C FB 01 00 00        JL         0x00506c89
00506a8e     A1 38 33 57 00           MOV        EAX,[0x00573338]
00506a93     33 C9                    XOR        ECX,ECX
00506a95     8A 88 51 01 00 00        MOV        CL,byte ptr [EAX + 0x151]
00506a9b     0F BF 14 CD 58 2A 56 00  MOVSX      EDX,word ptr [ECX*0x8 + 0x562a58]
00506aa3     66 8B 0D A0 93 5E 00     MOV        CX,word ptr [0x005e93a0]
00506aaa     0F BF F1                 MOVSX      ESI,CX
00506aad     2B D6                    SUB        EDX,ESI
00506aaf     66 8B 35 A2 93 5E 00     MOV        SI,word ptr [0x005e93a2]
00506ab6     C1 FA 02                 SAR        EDX,0x2
00506ab9     66 03 CA                 ADD        CX,DX
00506abc     33 D2                    XOR        EDX,EDX
00506abe     66 89 0D A0 93 5E 00     MOV        word ptr [0x005e93a0],CX
00506ac5     8A 90 51 01 00 00        MOV        DL,byte ptr [EAX + 0x151]
00506acb     0F BF FE                 MOVSX      EDI,SI
00506ace     0F BF 14 D5 5A 2A 56 00  MOVSX      EDX,word ptr [EDX*0x8 + 0x562a5a]
00506ad6     2B D7                    SUB        EDX,EDI
00506ad8     C1 FA 02                 SAR        EDX,0x2
00506adb     66 03 F2                 ADD        SI,DX
00506ade     33 D2                    XOR        EDX,EDX
00506ae0     66 89 35 A2 93 5E 00     MOV        word ptr [0x005e93a2],SI
00506ae7     8A 90 51 01 00 00        MOV        DL,byte ptr [EAX + 0x151]
00506aed     66 A1 A4 93 5E 00        MOV        AX,[0x005e93a4]
00506af3     0F BF 14 D5 5C 2A 56 00  MOVSX      EDX,word ptr [EDX*0x8 + 0x562a5c]
00506afb     0F BF F8                 MOVSX      EDI,AX
00506afe     2B D7                    SUB        EDX,EDI
00506b00     C1 FA 02                 SAR        EDX,0x2
00506b03     66 03 C2                 ADD        AX,DX
00506b06     66 A3 A4 93 5E 00        MOV        [0x005e93a4],AX
00506b0c     0F BF C0                 MOVSX      EAX,AX
00506b0f     50                       PUSH       EAX
00506b10     0F BF D6                 MOVSX      EDX,SI
00506b13     0F BF C1                 MOVSX      EAX,CX
00506b16     52                       PUSH       EDX
00506b17     50                       PUSH       EAX
00506b18     E8 83 EF 03 00           CALL       0x00545aa0
00506b1d     BE 34 2C 57 00           MOV        ESI,0x572c34
00506b22     8D 7E CC                 LEA        EDI,[ESI + -0x34]
00506b25     E8 16 06 04 00           CALL       0x00547140
00506b2a     8B 0F                    MOV        ECX,dword ptr [EDI]
00506b2c     8B 15 64 2B 57 00        MOV        EDX,dword ptr [0x00572b64]
00506b32     2B CA                    SUB        ECX,EDX
00506b34     81 C1 FF 3F 00 00        ADD        ECX,0x3fff
00506b3a     2B C8                    SUB        ECX,EAX
00506b3c     8B 06                    MOV        EAX,dword ptr [ESI]
00506b3e     79 0E                    JNS        0x00506b4e
00506b40     3D 00 90 01 00           CMP        EAX,0x19000
00506b45     7D 15                    JGE        0x00506b5c
00506b47     05 00 20 00 00           ADD        EAX,0x2000
00506b4c     EB 0C                    JMP        0x00506b5a
00506b4e     3D 00 70 FE FF           CMP        EAX,0xfffe7000
00506b53     7E 07                    JLE        0x00506b5c
00506b55     05 00 E0 FF FF           ADD        EAX,0xffffe000
00506b5a     89 06                    MOV        dword ptr [ESI],EAX
00506b5c     E8 DF 05 04 00           CALL       0x00547140
00506b61     8B 56 D0                 MOV        EDX,dword ptr [ESI + -0x30]
00506b64     8B 0D 68 2B 57 00        MOV        ECX,dword ptr [0x00572b68]
00506b6a     2B D1                    SUB        EDX,ECX
00506b6c     81 EA 01 80 08 00        SUB        EDX,0x88001
00506b72     2B D0                    SUB        EDX,EAX
00506b74     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
00506b77     79 0E                    JNS        0x00506b87
00506b79     3D 00 90 01 00           CMP        EAX,0x19000
00506b7e     7D 16                    JGE        0x00506b96
00506b80     05 00 20 00 00           ADD        EAX,0x2000
00506b85     EB 0C                    JMP        0x00506b93
00506b87     3D 00 70 FE FF           CMP        EAX,0xfffe7000
00506b8c     7E 08                    JLE        0x00506b96
00506b8e     05 00 E0 FF FF           ADD        EAX,0xffffe000
00506b93     89 46 04                 MOV        dword ptr [ESI + 0x4],EAX
00506b96     E8 A5 05 04 00           CALL       0x00547140
00506b9b     8B 4E D4                 MOV        ECX,dword ptr [ESI + -0x2c]
00506b9e     8B 15 6C 2B 57 00        MOV        EDX,dword ptr [0x00572b6c]
00506ba4     2B CA                    SUB        ECX,EDX
00506ba6     81 C1 FF 3F 00 00        ADD        ECX,0x3fff
00506bac     2B C8                    SUB        ECX,EAX
00506bae     8B 46 08                 MOV        EAX,dword ptr [ESI + 0x8]
00506bb1     79 0E                    JNS        0x00506bc1
00506bb3     3D 00 90 01 00           CMP        EAX,0x19000
00506bb8     7D 16                    JGE        0x00506bd0
00506bba     05 00 20 00 00           ADD        EAX,0x2000
00506bbf     EB 0C                    JMP        0x00506bcd
00506bc1     3D 00 70 FE FF           CMP        EAX,0xfffe7000
00506bc6     7E 08                    JLE        0x00506bd0
00506bc8     05 00 E0 FF FF           ADD        EAX,0xffffe000
00506bcd     89 46 08                 MOV        dword ptr [ESI + 0x8],EAX
00506bd0     8B 16                    MOV        EDX,dword ptr [ESI]
00506bd2     8B 07                    MOV        EAX,dword ptr [EDI]
00506bd4     03 C2                    ADD        EAX,EDX
00506bd6     55                       PUSH       EBP
00506bd7     89 07                    MOV        dword ptr [EDI],EAX
00506bd9     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
00506bdc     8B 4E D0                 MOV        ECX,dword ptr [ESI + -0x30]
00506bdf     55                       PUSH       EBP
00506be0     03 C8                    ADD        ECX,EAX
00506be2     A1 60 99 5E 00           MOV        EAX,[0x005e9960]
00506be7     89 4E D0                 MOV        dword ptr [ESI + -0x30],ECX
00506bea     8B 4E 08                 MOV        ECX,dword ptr [ESI + 0x8]
00506bed     8B 56 D4                 MOV        EDX,dword ptr [ESI + -0x2c]
00506bf0     57                       PUSH       EDI
00506bf1     03 D1                    ADD        EDX,ECX
00506bf3     B9 06 00 00 00           MOV        ECX,0x6
00506bf8     89 56 D4                 MOV        dword ptr [ESI + -0x2c],EDX
00506bfb     33 D2                    XOR        EDX,EDX
00506bfd     D1 E8                    SHR        EAX,0x1
00506bff     F7 F1                    DIV        ECX
00506c01     8B 44 24 20              MOV        EAX,dword ptr [ESP + 0x20]
00506c05     66 89 5E D8              MOV        word ptr [ESI + -0x28],BX
00506c09     66 89 5E DA              MOV        word ptr [ESI + -0x26],BX
00506c0d     66 89 5E DC              MOV        word ptr [ESI + -0x24],BX
00506c11     83 C0 14                 ADD        EAX,0x14
00506c14     88 56 F0                 MOV        byte ptr [ESI + -0x10],DL
00506c17     8B 54 24 1C              MOV        EDX,dword ptr [ESP + 0x1c]
00506c1b     52                       PUSH       EDX
00506c1c     50                       PUSH       EAX
00506c1d     E8 BE 9E 01 00           CALL       0x00520ae0
00506c22     81 C6 9C 00 00 00        ADD        ESI,0x9c
00506c28     83 C4 14                 ADD        ESP,0x14
00506c2b     81 FE A4 2E 57 00        CMP        ESI,0x572ea4
00506c31     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506c35     0F 8C E7 FE FF FF        JL         0x00506b22
00506c3b     A1 64 2B 57 00           MOV        EAX,[0x00572b64]
00506c40     8B 0D 00 2C 57 00        MOV        ECX,dword ptr [0x00572c00]
00506c46     8B 35 08 2C 57 00        MOV        ESI,dword ptr [0x00572c08]
00506c4c     2B C1                    SUB        EAX,ECX
00506c4e     8B 0D 6C 2B 57 00        MOV        ECX,dword ptr [0x00572b6c]
00506c54     66 89 2D 08 93 5E 00     MOV        word ptr [0x005e9308],BP
00506c5b     2B CE                    SUB        ECX,ESI
00506c5d     C1 F9 0C                 SAR        ECX,0xc
00506c60     C1 F8 0C                 SAR        EAX,0xc
00506c63     66 89 4C 24 26           MOV        word ptr [ESP + 0x26],CX
00506c68     66 89 44 24 24           MOV        word ptr [ESP + 0x24],AX
00506c6d     0F BF C9                 MOVSX      ECX,CX
00506c70     0F BF D0                 MOVSX      EDX,AX
00506c73     51                       PUSH       ECX
00506c74     52                       PUSH       EDX
00506c75     E8 A8 ED 03 00           CALL       0x00545a22
00506c7a     F7 D8                    NEG        EAX
00506c7c     66 A3 0A 93 5E 00        MOV        [0x005e930a],AX
00506c82     66 89 2D 0C 93 5E 00     MOV        word ptr [0x005e930c],BP
00506c89     66 A1 70 2B 57 00        MOV        AX,[0x00572b70]
00506c8f     BA 00 1E 00 00           MOV        EDX,0x1e00
00506c94     0F BF C8                 MOVSX      ECX,AX
00506c97     2B D1                    SUB        EDX,ECX
00506c99     C1 FA 02                 SAR        EDX,0x2
00506c9c     66 03 C2                 ADD        AX,DX
00506c9f     BA 00 1E 00 00           MOV        EDX,0x1e00
00506ca4     66 A3 70 2B 57 00        MOV        [0x00572b70],AX
00506caa     66 A1 72 2B 57 00        MOV        AX,[0x00572b72]
00506cb0     0F BF C8                 MOVSX      ECX,AX
00506cb3     2B D1                    SUB        EDX,ECX
00506cb5     C1 FA 02                 SAR        EDX,0x2
00506cb8     66 03 C2                 ADD        AX,DX
00506cbb     BA 00 1E 00 00           MOV        EDX,0x1e00
00506cc0     66 A3 72 2B 57 00        MOV        [0x00572b72],AX
00506cc6     66 A1 74 2B 57 00        MOV        AX,[0x00572b74]
00506ccc     0F BF C8                 MOVSX      ECX,AX
00506ccf     2B D1                    SUB        EDX,ECX
00506cd1     C1 FA 02                 SAR        EDX,0x2
00506cd4     66 03 C2                 ADD        AX,DX
00506cd7     BA 00 C0 F9 FF           MOV        EDX,0xfff9c000
00506cdc     66 A3 74 2B 57 00        MOV        [0x00572b74],AX
00506ce2     A1 64 2B 57 00           MOV        EAX,[0x00572b64]
00506ce7     8B C8                    MOV        ECX,EAX
00506ce9     F7 D9                    NEG        ECX
00506ceb     C1 F9 02                 SAR        ECX,0x2
00506cee     03 C1                    ADD        EAX,ECX
00506cf0     8B 0D 68 2B 57 00        MOV        ECX,dword ptr [0x00572b68]
00506cf6     2B D1                    SUB        EDX,ECX
00506cf8     A3 64 2B 57 00           MOV        [0x00572b64],EAX
00506cfd     C1 FA 02                 SAR        EDX,0x2
00506d00     03 CA                    ADD        ECX,EDX
00506d02     89 0D 68 2B 57 00        MOV        dword ptr [0x00572b68],ECX
00506d08     8B 0D 6C 2B 57 00        MOV        ECX,dword ptr [0x00572b6c]
00506d0e     8B D1                    MOV        EDX,ECX
00506d10     F7 DA                    NEG        EDX
00506d12     C1 FA 02                 SAR        EDX,0x2
00506d15     03 CA                    ADD        ECX,EDX
00506d17     3B C5                    CMP        EAX,EBP
00506d19     89 0D 6C 2B 57 00        MOV        dword ptr [0x00572b6c],ECX
00506d1f     7D 02                    JGE        0x00506d23
00506d21     F7 D8                    NEG        EAX
00506d23     3D 00 10 00 00           CMP        EAX,0x1000
00506d28     7D 06                    JGE        0x00506d30
00506d2a     89 2D 64 2B 57 00        MOV        dword ptr [0x00572b64],EBP
00506d30     3B CD                    CMP        ECX,EBP
00506d32     7D 02                    JGE        0x00506d36
00506d34     F7 D9                    NEG        ECX
00506d36     81 F9 00 10 00 00        CMP        ECX,0x1000
00506d3c     7D 06                    JGE        0x00506d44
00506d3e     89 2D 6C 2B 57 00        MOV        dword ptr [0x00572b6c],EBP
00506d44     A1 4C 2B 57 00           MOV        EAX,[0x00572b4c]
00506d49     33 FF                    XOR        EDI,EDI
00506d4b     0F BF 0D 60 2B 57 00     MOVSX      ECX,word ptr [0x00572b60]
00506d52     25 FF 00 00 00           AND        EAX,0xff
00506d57     C1 E0 02                 SHL        EAX,0x2
00506d5a     8B 90 40 96 5E 00        MOV        EDX,dword ptr [EAX + 0x5e9640]
00506d60     8B 34 8A                 MOV        ESI,dword ptr [EDX + ECX*0x4]
00506d63     8D 14 8A                 LEA        EDX,[EDX + ECX*0x4]
00506d66     66 8B 0D 62 2B 57 00     MOV        CX,word ptr [0x00572b62]
00506d6d     66 41                    INC        CX
00506d6f     66 89 0D 62 2B 57 00     MOV        word ptr [0x00572b62],CX
00506d76     66 8B 7E 16              MOV        DI,word ptr [ESI + 0x16]
00506d7a     0F BF F1                 MOVSX      ESI,CX
00506d7d     3B F7                    CMP        ESI,EDI
00506d7f     7C 0A                    JL         0x00506d8b
00506d81     66 33 C9                 XOR        CX,CX
00506d84     66 89 0D 62 2B 57 00     MOV        word ptr [0x00572b62],CX
00506d8b     66 8B 35 52 2B 57 00     MOV        SI,word ptr [0x00572b52]
00506d92     8B 80 A0 96 5E 00        MOV        EAX,dword ptr [EAX + 0x5e96a0]
00506d98     0F BF C9                 MOVSX      ECX,CX
00506d9b     56                       PUSH       ESI
00506d9c     51                       PUSH       ECX
00506d9d     8B 0A                    MOV        ECX,dword ptr [EDX]
00506d9f     8B 54 24 18              MOV        EDX,dword ptr [ESP + 0x18]
00506da3     68 64 2B 57 00           PUSH       0x572b64
00506da8     50                       PUSH       EAX
00506da9     8B 44 24 24              MOV        EAX,dword ptr [ESP + 0x24]
00506dad     51                       PUSH       ECX
00506dae     52                       PUSH       EDX
00506daf     50                       PUSH       EAX
00506db0     E8 3B 64 01 00           CALL       0x0051d1f0
00506db5     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00506db9     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00506dbe     83 C4 1C                 ADD        ESP,0x1c
00506dc1     8B 0D FC 08 60 00        MOV        ECX,dword ptr [0x006008fc]
00506dc7     BA 20 03 00 00           MOV        EDX,0x320
00506dcc     2B D1                    SUB        EDX,ECX
00506dce     33 FF                    XOR        EDI,EDI
00506dd0     C1 FA 02                 SAR        EDX,0x2
00506dd3     03 CA                    ADD        ECX,EDX
00506dd5     89 0D FC 08 60 00        MOV        dword ptr [0x006008fc],ECX
00506ddb     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
00506de1     8B 91 94 00 00 00        MOV        EDX,dword ptr [ECX + 0x94]
00506de7     8B 4A 30                 MOV        ECX,dword ptr [EDX + 0x30]
00506dea     89 4C 24 1C              MOV        dword ptr [ESP + 0x1c],ECX
00506dee     8B 0D 50 2A 56 00        MOV        ECX,dword ptr [0x00562a50]
00506df4     3B CD                    CMP        ECX,EBP
00506df6     0F 8E C1 00 00 00        JLE        0x00506ebd
00506dfc     BE F6 0F 56 00           MOV        ESI,0x560ff6
00506e01     EB 05                    JMP        0x00506e08
00506e03     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00506e08     66 8B 56 FE              MOV        DX,word ptr [ESI + -0x2]
00506e0c     66 8B 0E                 MOV        CX,word ptr [ESI]
00506e0f     66 83 C2 0C              ADD        DX,0xc
00506e13     66 83 C1 04              ADD        CX,0x4
00506e17     66 89 54 24 24           MOV        word ptr [ESP + 0x24],DX
00506e1c     66 89 4C 24 26           MOV        word ptr [ESP + 0x26],CX
00506e21     66 0F BE 94 87 98 2A 56 00 MOVSX      DX,byte ptr [EDI + EAX*0x4 + 0x562a98]
00506e2a     8B 4C 24 14              MOV        ECX,dword ptr [ESP + 0x14]
00506e2e     8D 44 24 10              LEA        EAX,[ESP + 0x10]
00506e32     50                       PUSH       EAX
00506e33     51                       PUSH       ECX
00506e34     C7 44 24 28 00 00 00 01  MOV        dword ptr [ESP + 0x28],0x1000000
00506e3c     66 89 54 24 34           MOV        word ptr [ESP + 0x34],DX
00506e41     E8 DA 4F 01 00           CALL       0x0051be20
00506e46     8B 44 24 18              MOV        EAX,dword ptr [ESP + 0x18]
00506e4a     8B 4C 24 1C              MOV        ECX,dword ptr [ESP + 0x1c]
00506e4e     83 C4 08                 ADD        ESP,0x8
00506e51     8D 54 24 1C              LEA        EDX,[ESP + 0x1c]
00506e55     55                       PUSH       EBP
00506e56     55                       PUSH       EBP
00506e57     52                       PUSH       EDX
00506e58     50                       PUSH       EAX
00506e59     51                       PUSH       ECX
00506e5a     E8 91 AF 01 00           CALL       0x00521df0
00506e5f     83 C4 14                 ADD        ESP,0x14
00506e62     8D 54 24 1C              LEA        EDX,[ESP + 0x1c]
00506e66     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506e6a     52                       PUSH       EDX
00506e6b     50                       PUSH       EAX
00506e6c     8B 44 24 1C              MOV        EAX,dword ptr [ESP + 0x1c]
00506e70     50                       PUSH       EAX
00506e71     E8 4A 8B 01 00           CALL       0x0051f9c0
00506e76     8B 54 24 20              MOV        EDX,dword ptr [ESP + 0x20]
00506e7a     83 C4 0C                 ADD        ESP,0xc
00506e7d     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
00506e81     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506e85     51                       PUSH       ECX
00506e86     52                       PUSH       EDX
00506e87     E8 24 4F 01 00           CALL       0x0051bdb0
00506e8c     8B 4C 24 18              MOV        ECX,dword ptr [ESP + 0x18]
00506e90     8B 54 24 1C              MOV        EDX,dword ptr [ESP + 0x1c]
00506e94     83 C4 08                 ADD        ESP,0x8
00506e97     8D 46 FA                 LEA        EAX,[ESI + -0x6]
00506e9a     50                       PUSH       EAX
00506e9b     51                       PUSH       ECX
00506e9c     52                       PUSH       EDX
00506e9d     E8 9E E4 03 00           CALL       0x00545340
00506ea2     83 C4 0C                 ADD        ESP,0xc
00506ea5     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506ea9     A1 50 2A 56 00           MOV        EAX,[0x00562a50]
00506eae     47                       INC        EDI
00506eaf     81 C6 C8 08 00 00        ADD        ESI,0x8c8
00506eb5     3B F8                    CMP        EDI,EAX
00506eb7     0F 8C 46 FF FF FF        JL         0x00506e03
00506ebd     A1 38 4A 56 00           MOV        EAX,[0x00564a38]
00506ec2     3B C5                    CMP        EAX,EBP
00506ec4     A1 60 E1 5D 00           MOV        EAX,[0x005de160]
00506ec9     75 17                    JNZ        0x00506ee2
00506ecb     83 F8 08                 CMP        EAX,0x8
00506ece     7C 12                    JL         0x00506ee2
00506ed0     8D 04 40                 LEA        EAX,[EAX + EAX*0x2]
00506ed3     8D 0C 85 90 FF FF FF     LEA        ECX,[EAX*0x4 + 0xffffff90]
00506eda     89 0D 58 E1 5D 00        MOV        dword ptr [0x005de158],ECX
00506ee0     EB 1C                    JMP        0x00506efe
00506ee2     3B C5                    CMP        EAX,EBP
00506ee4     7E 11                    JLE        0x00506ef7
00506ee6     83 C0 09                 ADD        EAX,0x9
00506ee9     8D 14 40                 LEA        EDX,[EAX + EAX*0x2]
00506eec     C1 E2 02                 SHL        EDX,0x2
00506eef     89 15 58 E1 5D 00        MOV        dword ptr [0x005de158],EDX
00506ef5     EB 07                    JMP        0x00506efe
00506ef7     83 2D 58 E1 5D 00 08     SUB        dword ptr [0x005de158],0x8
00506efe     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
00506f03     3B C5                    CMP        EAX,EBP
00506f05     7C 1C                    JL         0x00506f23
00506f07     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
00506f0d     55                       PUSH       EBP
00506f0e     68 28 07 56 00           PUSH       0x560728
00506f13     55                       PUSH       EBP
00506f14     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
00506f17     8B 04 82                 MOV        EAX,dword ptr [EDX + EAX*0x4]
00506f1a     50                       PUSH       EAX
00506f1b     E8 30 D4 03 00           CALL       0x00544350
00506f20     83 C4 10                 ADD        ESP,0x10
00506f23     8B 4C 24 10              MOV        ECX,dword ptr [ESP + 0x10]
00506f27     68 28 07 56 00           PUSH       0x560728
00506f2c     51                       PUSH       ECX
00506f2d     E8 6E D4 03 00           CALL       0x005443a0
00506f32     8B 54 24 1C              MOV        EDX,dword ptr [ESP + 0x1c]
00506f36     83 C4 08                 ADD        ESP,0x8
00506f39     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506f3d     68 28 07 56 00           PUSH       0x560728
00506f42     50                       PUSH       EAX
00506f43     52                       PUSH       EDX
00506f44     E8 F7 E3 03 00           CALL       0x00545340
00506f49     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
00506f4f     89 44 24 1C              MOV        dword ptr [ESP + 0x1c],EAX
00506f53     89 2D 5C E1 5D 00        MOV        dword ptr [0x005de15c],EBP
00506f59     66 C7 44 24 30 A0 00     MOV        word ptr [ESP + 0x30],0xa0
00506f60     66 C7 44 24 32 78 00     MOV        word ptr [ESP + 0x32],0x78
00506f67     66 C7 44 24 34 20 14     MOV        word ptr [ESP + 0x34],0x1420
00506f6e     66 C7 44 24 36 49 11     MOV        word ptr [ESP + 0x36],0x1149
00506f75     66 89 6C 24 38           MOV        word ptr [ESP + 0x38],BP
00506f7a     8B 91 94 00 00 00        MOV        EDX,dword ptr [ECX + 0x94]
00506f80     83 C4 0C                 ADD        ESP,0xc
00506f83     8B 0A                    MOV        ECX,dword ptr [EDX]
00506f85     55                       PUSH       EBP
00506f86     8D 54 24 20              LEA        EDX,[ESP + 0x20]
00506f8a     55                       PUSH       EBP
00506f8b     52                       PUSH       EDX
00506f8c     50                       PUSH       EAX
00506f8d     8B 44 24 28              MOV        EAX,dword ptr [ESP + 0x28]
00506f91     89 4C 24 2C              MOV        dword ptr [ESP + 0x2c],ECX
00506f95     50                       PUSH       EAX
00506f96     C7 44 24 34 00 00 00 01  MOV        dword ptr [ESP + 0x34],0x1000000
00506f9e     E8 7D AF 01 00           CALL       0x00521f20
00506fa3     8B 54 24 2C              MOV        EDX,dword ptr [ESP + 0x2c]
00506fa7     83 C4 14                 ADD        ESP,0x14
00506faa     8D 4C 24 1C              LEA        ECX,[ESP + 0x1c]
00506fae     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506fb2     51                       PUSH       ECX
00506fb3     50                       PUSH       EAX
00506fb4     52                       PUSH       EDX
00506fb5     E8 06 8A 01 00           CALL       0x0051f9c0
00506fba     83 C4 0C                 ADD        ESP,0xc
00506fbd     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
00506fc1     8D 44 24 10              LEA        EAX,[ESP + 0x10]
00506fc5     8D 4C 24 18              LEA        ECX,[ESP + 0x18]
00506fc9     6A 01                    PUSH       0x1
00506fcb     6A 03                    PUSH       0x3
00506fcd     50                       PUSH       EAX
00506fce     8D 54 24 20              LEA        EDX,[ESP + 0x20]
00506fd2     51                       PUSH       ECX
00506fd3     52                       PUSH       EDX
00506fd4     E8 A7 32 02 00           CALL       0x0052a280
00506fd9     83 C4 14                 ADD        ESP,0x14
00506fdc     E9 A3 F9 FF FF           JMP        0x00506984
00506fe1     89 2D 10 E5 5D 00        MOV        dword ptr [0x005de510],EBP
00506fe7     56                       PUSH       ESI
00506fe8     8D 44 24 14              LEA        EAX,[ESP + 0x14]
00506fec     6A 03                    PUSH       0x3
00506fee     8D 4C 24 20              LEA        ECX,[ESP + 0x20]
00506ff2     50                       PUSH       EAX
00506ff3     8D 54 24 20              LEA        EDX,[ESP + 0x20]
00506ff7     51                       PUSH       ECX
00506ff8     52                       PUSH       EDX
00506ff9     E8 82 32 02 00           CALL       0x0052a280
00506ffe     83 C4 14                 ADD        ESP,0x14
00507001     89 2D 5C E1 5D 00        MOV        dword ptr [0x005de15c],EBP
00507007     5F                       POP        EDI
00507008     5E                       POP        ESI
00507009     5D                       POP        EBP
0050700a     5B                       POP        EBX
0050700b     83 C4 20                 ADD        ESP,0x20
0050700e     C3                       RET        

