; Function: FUN_005098f0
; Entry:    005098f0
; Body:     [[005098f0, 0050a73b]]

; --------------------------------------------------
; Range: 005098f0 - 0050a73b
; --------------------------------------------------
005098f0     A1 48 2A 56 00           MOV        EAX,[0x00562a48]
005098f5     83 EC 14                 SUB        ESP,0x14
005098f8     53                       PUSH       EBX
005098f9     33 DB                    XOR        EBX,EBX
005098fb     55                       PUSH       EBP
005098fc     56                       PUSH       ESI
005098fd     3B C3                    CMP        EAX,EBX
005098ff     57                       PUSH       EDI
00509900     0F 84 6B 01 00 00        JZ         0x00509a71
00509906     33 ED                    XOR        EBP,EBP
00509908     89 1D 48 2A 56 00        MOV        dword ptr [0x00562a48],EBX
0050990e     89 1D 08 8E 57 00        MOV        dword ptr [0x00578e08],EBX
00509914     BE 18 35 57 00           MOV        ESI,0x573518
00509919     33 FF                    XOR        EDI,EDI
0050991b     57                       PUSH       EDI
0050991c     E8 F5 C0 03 00           CALL       0x00545a16
00509921     8D 0C 40                 LEA        ECX,[EAX + EAX*0x2]
00509924     57                       PUSH       EDI
00509925     C1 E1 02                 SHL        ECX,0x2
00509928     2B C8                    SUB        ECX,EAX
0050992a     F7 D9                    NEG        ECX
0050992c     C1 E1 03                 SHL        ECX,0x3
0050992f     C1 F9 0C                 SAR        ECX,0xc
00509932     81 C1 88 00 00 00        ADD        ECX,0x88
00509938     66 89 4E 04              MOV        word ptr [ESI + 0x4],CX
0050993c     E8 DB C0 03 00           CALL       0x00545a1c
00509941     C1 E0 06                 SHL        EAX,0x6
00509944     C1 F8 0C                 SAR        EAX,0xc
00509947     83 C0 48                 ADD        EAX,0x48
0050994a     8D 55 03                 LEA        EDX,[EBP + 0x3]
0050994d     66 89 46 06              MOV        word ptr [ESI + 0x6],AX
00509951     B8 00 00 00 01           MOV        EAX,0x1000000
00509956     66 C7 46 08 20 00        MOV        word ptr [ESI + 0x8],0x20
0050995c     66 C7 46 0A 18 00        MOV        word ptr [ESI + 0xa],0x18
00509962     C7 46 10 00 00 18 01     MOV        dword ptr [ESI + 0x10],0x1180000
00509969     89 56 14                 MOV        dword ptr [ESI + 0x14],EDX
0050996c     C7 46 18 08 08 0F 01     MOV        dword ptr [ESI + 0x18],0x10f0808
00509973     C7 46 1C 20 10 CF 02     MOV        dword ptr [ESI + 0x1c],0x2cf1020
0050997a     C7 46 20 10 0C 07 01     MOV        dword ptr [ESI + 0x20],0x1070c10
00509981     89 46 24                 MOV        dword ptr [ESI + 0x24],EAX
00509984     89 5E 28                 MOV        dword ptr [ESI + 0x28],EBX
00509987     89 5E 30                 MOV        dword ptr [ESI + 0x30],EBX
0050998a     89 5E 38                 MOV        dword ptr [ESI + 0x38],EBX
0050998d     C7 46 40 00 00 0F 01     MOV        dword ptr [ESI + 0x40],0x10f0000
00509994     89 46 44                 MOV        dword ptr [ESI + 0x44],EAX
00509997     89 5E 48                 MOV        dword ptr [ESI + 0x48],EBX
0050999a     89 5E 50                 MOV        dword ptr [ESI + 0x50],EBX
0050999d     89 5E 58                 MOV        dword ptr [ESI + 0x58],EBX
005099a0     89 5E 60                 MOV        dword ptr [ESI + 0x60],EBX
005099a3     81 C7 55 01 00 00        ADD        EDI,0x155
005099a9     45                       INC        EBP
005099aa     81 C6 C8 08 00 00        ADD        ESI,0x8c8
005099b0     81 FF 53 09 00 00        CMP        EDI,0x953
005099b6     0F 8C 5F FF FF FF        JL         0x0050991b
005099bc     B9 38 00 00 00           MOV        ECX,0x38
005099c1     B8 28 00 00 00           MOV        EAX,0x28
005099c6     BA 18 00 00 00           MOV        EDX,0x18
005099cb     66 C7 46 04 58 00        MOV        word ptr [ESI + 0x4],0x58
005099d1     66 89 4E 06              MOV        word ptr [ESI + 0x6],CX
005099d5     66 89 46 08              MOV        word ptr [ESI + 0x8],AX
005099d9     66 89 56 0A              MOV        word ptr [ESI + 0xa],DX
005099dd     C6 46 20 10              MOV        byte ptr [ESI + 0x20],0x10
005099e1     C7 46 14 0A 00 00 00     MOV        dword ptr [ESI + 0x14],0xa
005099e8     81 C6 C8 08 00 00        ADD        ESI,0x8c8
005099ee     66 C7 46 04 B0 00        MOV        word ptr [ESI + 0x4],0xb0
005099f4     66 89 4E 06              MOV        word ptr [ESI + 0x6],CX
005099f8     66 89 46 08              MOV        word ptr [ESI + 0x8],AX
005099fc     66 89 56 0A              MOV        word ptr [ESI + 0xa],DX
00509a00     C6 46 20 10              MOV        byte ptr [ESI + 0x20],0x10
00509a04     C7 46 14 0B 00 00 00     MOV        dword ptr [ESI + 0x14],0xb
00509a0b     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
00509a11     8A 86 50 01 00 00        MOV        AL,byte ptr [ESI + 0x150]
00509a17     83 E0 07                 AND        EAX,0x7
00509a1a     83 F8 07                 CMP        EAX,0x7
00509a1d     77 58                    JA         0x00509a77
00509a1f     FF 24 85 3C A7 50 00     JMP        dword ptr [EAX*0x4 + 0x50a73c]
00509a26     66 C7 86 48 01 00 00 10 00 MOV        word ptr [ESI + 0x148],0x10
00509a2f     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509a34     66 C7 80 4A 01 00 00 02 00 MOV        word ptr [EAX + 0x14a],0x2
00509a3d     EB 32                    JMP        0x00509a71
00509a3f     66 C7 86 48 01 00 00 0F 00 MOV        word ptr [ESI + 0x148],0xf
00509a48     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509a4e     66 C7 81 4A 01 00 00 04 00 MOV        word ptr [ECX + 0x14a],0x4
00509a57     EB 18                    JMP        0x00509a71
00509a59     66 C7 86 48 01 00 00 0E 00 MOV        word ptr [ESI + 0x148],0xe
00509a62     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509a68     66 C7 82 4A 01 00 00 08 00 MOV        word ptr [EDX + 0x14a],0x8
00509a71     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
00509a77     A1 4C 2A 56 00           MOV        EAX,[0x00562a4c]
00509a7c     8B 6C 24 28              MOV        EBP,dword ptr [ESP + 0x28]
00509a80     3B C3                    CMP        EAX,EBX
00509a82     0F 85 58 0B 00 00        JNZ        0x0050a5e0
00509a88     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
00509a8d     83 C0 C2                 ADD        EAX,-0x3e
00509a90     83 F8 43                 CMP        EAX,0x43
00509a93     0F 87 47 0B 00 00        JA         0x0050a5e0
00509a99     33 C9                    XOR        ECX,ECX
00509a9b     8A 88 84 A7 50 00        MOV        CL,byte ptr [EAX + 0x50a784]
00509aa1     FF 24 8D 5C A7 50 00     JMP        dword ptr [ECX*0x4 + 0x50a75c]
00509aa8     8B 7C 24 34              MOV        EDI,dword ptr [ESP + 0x34]
00509aac     66 8B 47 02              MOV        AX,word ptr [EDI + 0x2]
00509ab0     F6 C4 40                 TEST       AH,0x40
00509ab3     0F 85 B1 09 00 00        JNZ        0x0050a46a
00509ab9     F6 C4 80                 TEST       AH,0x80
00509abc     0F 84 AC 05 00 00        JZ         0x0050a06e
00509ac2     38 1D 6A 90 5E 00        CMP        byte ptr [0x005e906a],BL
00509ac8     0F 85 A0 05 00 00        JNZ        0x0050a06e
00509ace     25 FF 7F 00 00           AND        EAX,0x7fff
00509ad3     66 89 47 02              MOV        word ptr [EDI + 0x2],AX
00509ad7     E8 A4 40 03 00           CALL       0x0053db80
00509adc     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509ae2     C7 05 F0 8D 57 00 81 00 00 00 MOV        dword ptr [0x00578df0],0x81
00509aec     88 1A                    MOV        byte ptr [EDX],BL
00509aee     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509af3     88 58 01                 MOV        byte ptr [EAX + 0x1],BL
00509af6     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509afc     88 59 02                 MOV        byte ptr [ECX + 0x2],BL
00509aff     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509b05     88 5A 03                 MOV        byte ptr [EDX + 0x3],BL
00509b08     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509b0d     BA 01 00 00 00           MOV        EDX,0x1
00509b12     89 98 40 01 00 00        MOV        dword ptr [EAX + 0x140],EBX
00509b18     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509b1e     8B F2                    MOV        ESI,EDX
00509b20     66 89 91 44 01 00 00     MOV        word ptr [ECX + 0x144],DX
00509b27     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509b2c     66 89 90 46 01 00 00     MOV        word ptr [EAX + 0x146],DX
00509b33     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509b39     89 99 58 01 00 00        MOV        dword ptr [ECX + 0x158],EBX
00509b3f     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509b44     89 98 5C 01 00 00        MOV        dword ptr [EAX + 0x15c],EBX
00509b4a     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509b4f     8A 88 51 01 00 00        MOV        CL,byte ptr [EAX + 0x151]
00509b55     D3 E6                    SHL        ESI,CL
00509b57     81 CE 00 08 00 00        OR         ESI,0x800
00509b5d     89 B0 14 01 00 00        MOV        dword ptr [EAX + 0x114],ESI
00509b63     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509b68     8B F2                    MOV        ESI,EDX
00509b6a     8A 88 50 01 00 00        MOV        CL,byte ptr [EAX + 0x150]
00509b70     83 E1 07                 AND        ECX,0x7
00509b73     D3 E6                    SHL        ESI,CL
00509b75     33 C9                    XOR        ECX,ECX
00509b77     66 89 B0 12 01 00 00     MOV        word ptr [EAX + 0x112],SI
00509b7e     B8 A0 02 00 00           MOV        EAX,0x2a0
00509b83     66 0F B6 B1 60 90 5E 00  MOVZX      SI,byte ptr [ECX + 0x5e9060]
00509b8b     8B 3D 38 33 57 00        MOV        EDI,dword ptr [0x00573338]
00509b91     83 C0 02                 ADD        EAX,0x2
00509b94     41                       INC        ECX
00509b95     3D AE 02 00 00           CMP        EAX,0x2ae
00509b9a     66 89 74 07 DE           MOV        word ptr [EDI + EAX*0x1 + -0x22],SI
00509b9f     8B 3D 38 33 57 00        MOV        EDI,dword ptr [0x00573338]
00509ba5     66 0F B6 B1 5F 90 5E 00  MOVZX      SI,byte ptr [ECX + 0x5e905f]
00509bad     66 89 74 07 FE           MOV        word ptr [EDI + EAX*0x1 + -0x2],SI
00509bb2     7C CF                    JL         0x00509b83
00509bb4     66 0F B6 0D 67 90 5E 00  MOVZX      CX,byte ptr [0x005e9067]
00509bbc     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509bc1     66 89 88 48 01 00 00     MOV        word ptr [EAX + 0x148],CX
00509bc8     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509bcd     66 0F B6 0D 68 90 5E 00  MOVZX      CX,byte ptr [0x005e9068]
00509bd5     66 89 88 4A 01 00 00     MOV        word ptr [EAX + 0x14a],CX
00509bdc     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509be1     66 8B 88 48 01 00 00     MOV        CX,word ptr [EAX + 0x148]
00509be8     66 89 88 4C 01 00 00     MOV        word ptr [EAX + 0x14c],CX
00509bef     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509bf4     66 8B 88 4A 01 00 00     MOV        CX,word ptr [EAX + 0x14a]
00509bfb     66 89 88 4E 01 00 00     MOV        word ptr [EAX + 0x14e],CX
00509c02     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509c07     89 98 6C 02 00 00        MOV        dword ptr [EAX + 0x26c],EBX
00509c0d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509c13     88 99 B0 02 00 00        MOV        byte ptr [ECX + 0x2b0],BL
00509c19     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509c1e     B9 FF 00 00 00           MOV        ECX,0xff
00509c23     88 90 B1 02 00 00        MOV        byte ptr [EAX + 0x2b1],DL
00509c29     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
00509c2f     8A 86 50 01 00 00        MOV        AL,byte ptr [ESI + 0x150]
00509c35     83 E0 07                 AND        EAX,0x7
00509c38     74 0D                    JZ         0x00509c47
00509c3a     83 F8 06                 CMP        EAX,0x6
00509c3d     74 08                    JZ         0x00509c47
00509c3f     88 8E B2 02 00 00        MOV        byte ptr [ESI + 0x2b2],CL
00509c45     EB 07                    JMP        0x00509c4e
00509c47     C6 86 B2 02 00 00 02     MOV        byte ptr [ESI + 0x2b2],0x2
00509c4e     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509c53     88 88 B3 02 00 00        MOV        byte ptr [EAX + 0x2b3],CL
00509c59     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509c5e     88 88 B4 02 00 00        MOV        byte ptr [EAX + 0x2b4],CL
00509c64     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
00509c6a     8A 8E 50 01 00 00        MOV        CL,byte ptr [ESI + 0x150]
00509c70     83 E1 07                 AND        ECX,0x7
00509c73     8A 81 B8 2A 56 00        MOV        AL,byte ptr [ECX + 0x562ab8]
00509c79     88 86 B5 02 00 00        MOV        byte ptr [ESI + 0x2b5],AL
00509c7f     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509c85     88 99 B6 02 00 00        MOV        byte ptr [ECX + 0x2b6],BL
00509c8b     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509c90     88 98 B7 02 00 00        MOV        byte ptr [EAX + 0x2b7],BL
00509c96     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509c9c     88 99 B8 02 00 00        MOV        byte ptr [ECX + 0x2b8],BL
00509ca2     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509ca7     88 98 B9 02 00 00        MOV        byte ptr [EAX + 0x2b9],BL
00509cad     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509cb3     88 99 BA 02 00 00        MOV        byte ptr [ECX + 0x2ba],BL
00509cb9     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509cbe     88 98 BB 02 00 00        MOV        byte ptr [EAX + 0x2bb],BL
00509cc4     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509cca     88 91 60 01 00 00        MOV        byte ptr [ECX + 0x160],DL
00509cd0     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509cd5     88 98 61 01 00 00        MOV        byte ptr [EAX + 0x161],BL
00509cdb     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509ce1     88 99 62 01 00 00        MOV        byte ptr [ECX + 0x162],BL
00509ce7     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509cec     88 98 63 01 00 00        MOV        byte ptr [EAX + 0x163],BL
00509cf2     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509cf8     88 91 68 01 00 00        MOV        byte ptr [ECX + 0x168],DL
00509cfe     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509d03     88 98 69 01 00 00        MOV        byte ptr [EAX + 0x169],BL
00509d09     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d0f     88 99 6A 01 00 00        MOV        byte ptr [ECX + 0x16a],BL
00509d15     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509d1a     88 98 6B 01 00 00        MOV        byte ptr [EAX + 0x16b],BL
00509d20     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d26     88 91 70 01 00 00        MOV        byte ptr [ECX + 0x170],DL
00509d2c     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509d32     88 9A 71 01 00 00        MOV        byte ptr [EDX + 0x171],BL
00509d38     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509d3d     88 98 72 01 00 00        MOV        byte ptr [EAX + 0x172],BL
00509d43     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d49     B0 03                    MOV        AL,0x3
00509d4b     88 99 73 01 00 00        MOV        byte ptr [ECX + 0x173],BL
00509d51     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509d57     88 82 78 01 00 00        MOV        byte ptr [EDX + 0x178],AL
00509d5d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d63     88 81 79 01 00 00        MOV        byte ptr [ECX + 0x179],AL
00509d69     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509d6f     88 82 7A 01 00 00        MOV        byte ptr [EDX + 0x17a],AL
00509d75     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d7b     88 81 7B 01 00 00        MOV        byte ptr [ECX + 0x17b],AL
00509d81     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509d87     88 82 7C 01 00 00        MOV        byte ptr [EDX + 0x17c],AL
00509d8d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509d93     88 81 7D 01 00 00        MOV        byte ptr [ECX + 0x17d],AL
00509d99     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509d9f     88 82 7E 01 00 00        MOV        byte ptr [EDX + 0x17e],AL
00509da5     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509daa     88 98 00 1F 00 00        MOV        byte ptr [EAX + 0x1f00],BL
00509db0     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509db6     88 99 01 1F 00 00        MOV        byte ptr [ECX + 0x1f01],BL
00509dbc     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509dc2     88 9A 02 1F 00 00        MOV        byte ptr [EDX + 0x1f02],BL
00509dc8     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509dcd     88 98 03 1F 00 00        MOV        byte ptr [EAX + 0x1f03],BL
00509dd3     33 C0                    XOR        EAX,EAX
00509dd5     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509ddb     40                       INC        EAX
00509ddc     83 F8 40                 CMP        EAX,0x40
00509ddf     88 9C 08 FF 1C 00 00     MOV        byte ptr [EAX + ECX*0x1 + 0x1cff],BL
00509de6     7C ED                    JL         0x00509dd5
00509de8     33 C0                    XOR        EAX,EAX
00509dea     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509df0     40                       INC        EAX
00509df1     83 F8 40                 CMP        EAX,0x40
00509df4     88 9C 10 3F 1D 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x1d3f],BL
00509dfb     7C ED                    JL         0x00509dea
00509dfd     33 C0                    XOR        EAX,EAX
00509dff     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509e05     40                       INC        EAX
00509e06     83 F8 40                 CMP        EAX,0x40
00509e09     88 9C 08 7F 1D 00 00     MOV        byte ptr [EAX + ECX*0x1 + 0x1d7f],BL
00509e10     7C ED                    JL         0x00509dff
00509e12     33 C0                    XOR        EAX,EAX
00509e14     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509e1a     40                       INC        EAX
00509e1b     83 F8 40                 CMP        EAX,0x40
00509e1e     88 9C 10 BF 1D 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x1dbf],BL
00509e25     7C ED                    JL         0x00509e14
00509e27     33 C0                    XOR        EAX,EAX
00509e29     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509e2f     40                       INC        EAX
00509e30     83 F8 40                 CMP        EAX,0x40
00509e33     88 9C 08 FF 1D 00 00     MOV        byte ptr [EAX + ECX*0x1 + 0x1dff],BL
00509e3a     7C ED                    JL         0x00509e29
00509e3c     33 C0                    XOR        EAX,EAX
00509e3e     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509e44     40                       INC        EAX
00509e45     83 F8 40                 CMP        EAX,0x40
00509e48     88 9C 10 3F 1E 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x1e3f],BL
00509e4f     7C ED                    JL         0x00509e3e
00509e51     33 C0                    XOR        EAX,EAX
00509e53     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509e59     40                       INC        EAX
00509e5a     83 F8 40                 CMP        EAX,0x40
00509e5d     88 9C 08 7F 1E 00 00     MOV        byte ptr [EAX + ECX*0x1 + 0x1e7f],BL
00509e64     7C ED                    JL         0x00509e53
00509e66     33 C0                    XOR        EAX,EAX
00509e68     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509e6e     40                       INC        EAX
00509e6f     83 F8 40                 CMP        EAX,0x40
00509e72     88 9C 10 BF 1E 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x1ebf],BL
00509e79     7C ED                    JL         0x00509e68
00509e7b     33 C0                    XOR        EAX,EAX
00509e7d     83 C9 FF                 OR         ECX,0xffffffff
00509e80     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509e86     83 C0 08                 ADD        EAX,0x8
00509e89     83 F8 40                 CMP        EAX,0x40
00509e8c     89 9C 02 FC 1E 00 00     MOV        dword ptr [EDX + EAX*0x1 + 0x1efc],EBX
00509e93     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509e99     89 8C 02 00 1F 00 00     MOV        dword ptr [EDX + EAX*0x1 + 0x1f00],ECX
00509ea0     7C DE                    JL         0x00509e80
00509ea2     33 F6                    XOR        ESI,ESI
00509ea4     B8 C0 1F 00 00           MOV        EAX,0x1fc0
00509ea9     33 C9                    XOR        ECX,ECX
00509eab     83 C0 02                 ADD        EAX,0x2
00509eae     8A 8E C0 2A 56 00        MOV        CL,byte ptr [ESI + 0x562ac0]
00509eb4     46                       INC        ESI
00509eb5     3D D0 1F 00 00           CMP        EAX,0x1fd0
00509eba     66 8B 14 4D C8 2A 56 00  MOV        DX,word ptr [ECX*0x2 + 0x562ac8]
00509ec2     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509ec8     66 89 54 01 FE           MOV        word ptr [ECX + EAX*0x1 + -0x2],DX
00509ecd     7C DA                    JL         0x00509ea9
00509ecf     33 C0                    XOR        EAX,EAX
00509ed1     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509ed7     40                       INC        EAX
00509ed8     83 F8 20                 CMP        EAX,0x20
00509edb     88 9C 10 DF 02 00 00     MOV        byte ptr [EAX + EDX*0x1 + 0x2df],BL
00509ee2     7C ED                    JL         0x00509ed1
00509ee4     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509ee9     68 40 06 00 00           PUSH       0x640
00509eee     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
00509ef4     8D 88 00 03 00 00        LEA        ECX,[EAX + 0x300]
00509efa     A1 B4 87 60 00           MOV        EAX,[0x006087b4]
00509eff     83 E2 07                 AND        EDX,0x7
00509f02     83 C2 7C                 ADD        EDX,0x7c
00509f05     51                       PUSH       ECX
00509f06     8B 30                    MOV        ESI,dword ptr [EAX]
00509f08     C1 E2 0D                 SHL        EDX,0xd
00509f0b     03 D6                    ADD        EDX,ESI
00509f0d     52                       PUSH       EDX
00509f0e     E8 CD B3 02 00           CALL       0x005352e0
00509f13     83 C4 0C                 ADD        ESP,0xc
00509f16     33 C0                    XOR        EAX,EAX
00509f18     BA 00 80 00 00           MOV        EDX,0x8000
00509f1d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509f23     05 80 02 00 00           ADD        EAX,0x280
00509f28     66 09 94 01 92 00 00 00  OR         word ptr [ECX + EAX*0x1 + 0x92],DX
00509f30     3D 00 19 00 00           CMP        EAX,0x1900
00509f35     7C E6                    JL         0x00509f1d
00509f37     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509f3d     51                       PUSH       ECX
00509f3e     E8 6D EB 02 00           CALL       0x00538ab0
00509f43     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509f49     83 C4 04                 ADD        ESP,0x4
00509f4c     81 C2 00 01 00 00        ADD        EDX,0x100
00509f52     52                       PUSH       EDX
00509f53     E8 F8 EF 02 00           CALL       0x00538f50
00509f58     A1 38 33 57 00           MOV        EAX,[0x00573338]
00509f5d     83 C4 04                 ADD        ESP,0x4
00509f60     50                       PUSH       EAX
00509f61     E8 EA EC 02 00           CALL       0x00538c50
00509f66     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00509f6c     83 C4 04                 ADD        ESP,0x4
00509f6f     51                       PUSH       ECX
00509f70     E8 AB EE 02 00           CALL       0x00538e20
00509f75     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
00509f7b     83 C4 04                 ADD        ESP,0x4
00509f7e     81 C2 00 01 00 00        ADD        EDX,0x100
00509f84     52                       PUSH       EDX
00509f85     E8 16 F0 02 00           CALL       0x00538fa0
00509f8a     8B 3D 38 33 57 00        MOV        EDI,dword ptr [0x00573338]
00509f90     83 C4 04                 ADD        ESP,0x4
00509f93     33 C0                    XOR        EAX,EAX
00509f95     33 F6                    XOR        ESI,ESI
00509f97     33 C9                    XOR        ECX,ECX
00509f99     8A 8C 3E 00 01 00 00     MOV        CL,byte ptr [ESI + EDI*0x1 + 0x100]
00509fa0     03 C1                    ADD        EAX,ECX
00509fa2     46                       INC        ESI
00509fa3     D1 E0                    SHL        EAX,0x1
00509fa5     83 FE 10                 CMP        ESI,0x10
00509fa8     7C ED                    JL         0x00509f97
00509faa     8D 8F 80 02 00 00        LEA        ECX,[EDI + 0x280]
00509fb0     BA 07 00 00 00           MOV        EDX,0x7
00509fb5     0F BF 31                 MOVSX      ESI,word ptr [ECX]
00509fb8     03 C6                    ADD        EAX,ESI
00509fba     83 C1 02                 ADD        ECX,0x2
00509fbd     D1 E0                    SHL        EAX,0x1
00509fbf     4A                       DEC        EDX
00509fc0     75 F3                    JNZ        0x00509fb5
00509fc2     33 D2                    XOR        EDX,EDX
00509fc4     8B 35 C0 6B 56 00        MOV        ESI,dword ptr [0x00566bc0]
00509fca     8A 97 50 01 00 00        MOV        DL,byte ptr [EDI + 0x150]
00509fd0     03 D0                    ADD        EDX,EAX
00509fd2     33 C0                    XOR        EAX,EAX
00509fd4     8A 87 51 01 00 00        MOV        AL,byte ptr [EDI + 0x151]
00509fda     8D 0C 50                 LEA        ECX,[EAX + EDX*0x2]
00509fdd     33 D2                    XOR        EDX,EDX
00509fdf     8A 97 52 01 00 00        MOV        DL,byte ptr [EDI + 0x152]
00509fe5     8D 04 4A                 LEA        EAX,[EDX + ECX*0x2]
00509fe8     33 C9                    XOR        ECX,ECX
00509fea     8A 8F 53 01 00 00        MOV        CL,byte ptr [EDI + 0x153]
00509ff0     8D 0C 41                 LEA        ECX,[ECX + EAX*0x2]
00509ff3     D1 E1                    SHL        ECX,0x1
00509ff5     33 C0                    XOR        EAX,EAX
00509ff7     8A D1                    MOV        DL,CL
00509ff9     80 E2 1F                 AND        DL,0x1f
00509ffc     80 C2 40                 ADD        DL,0x40
00509fff     88 54 30 0C              MOV        byte ptr [EAX + ESI*0x1 + 0xc],DL
0050a003     8B 35 C0 6B 56 00        MOV        ESI,dword ptr [0x00566bc0]
0050a009     B2 5C                    MOV        DL,0x5c
0050a00b     38 54 30 0C              CMP        byte ptr [EAX + ESI*0x1 + 0xc],DL
0050a00f     75 0B                    JNZ        0x0050a01c
0050a011     C6 44 30 0C 37           MOV        byte ptr [EAX + ESI*0x1 + 0xc],0x37
0050a016     8B 35 C0 6B 56 00        MOV        ESI,dword ptr [0x00566bc0]
0050a01c     40                       INC        EAX
0050a01d     C1 F9 05                 SAR        ECX,0x5
0050a020     83 F8 07                 CMP        EAX,0x7
0050a023     7C D2                    JL         0x00509ff7
0050a025     38 54 30 0C              CMP        byte ptr [EAX + ESI*0x1 + 0xc],DL
0050a029     75 0B                    JNZ        0x0050a036
0050a02b     C6 44 30 0C 37           MOV        byte ptr [EAX + ESI*0x1 + 0xc],0x37
0050a030     8B 35 C0 6B 56 00        MOV        ESI,dword ptr [0x00566bc0]
0050a036     88 5C 30 0C              MOV        byte ptr [EAX + ESI*0x1 + 0xc],BL
0050a03a     8B 0D C0 6B 56 00        MOV        ECX,dword ptr [0x00566bc0]
0050a040     33 D2                    XOR        EDX,EDX
0050a042     8A 01                    MOV        AL,byte ptr [ECX]
0050a044     3A C3                    CMP        AL,BL
0050a046     74 15                    JZ         0x0050a05d
0050a048     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050a04e     42                       INC        EDX
0050a04f     41                       INC        ECX
0050a050     88 84 32 CF 1F 00 00     MOV        byte ptr [EDX + ESI*0x1 + 0x1fcf],AL
0050a057     8A 01                    MOV        AL,byte ptr [ECX]
0050a059     3A C3                    CMP        AL,BL
0050a05b     75 EB                    JNZ        0x0050a048
0050a05d     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050a062     88 9C 02 D0 1F 00 00     MOV        byte ptr [EDX + EAX*0x1 + 0x1fd0],BL
0050a069     E9 72 05 00 00           JMP        0x0050a5e0
0050a06e     8A 57 06                 MOV        DL,byte ptr [EDI + 0x6]
0050a071     A1 08 8E 57 00           MOV        EAX,[0x00578e08]
0050a076     F6 C2 80                 TEST       DL,0x80
0050a079     8B C8                    MOV        ECX,EAX
0050a07b     74 0A                    JZ         0x0050a087
0050a07d     3B C3                    CMP        EAX,EBX
0050a07f     74 06                    JZ         0x0050a087
0050a081     48                       DEC        EAX
0050a082     A3 08 8E 57 00           MOV        [0x00578e08],EAX
0050a087     F6 47 06 20              TEST       byte ptr [EDI + 0x6],0x20
0050a08b     74 0B                    JZ         0x0050a098
0050a08d     83 F8 06                 CMP        EAX,0x6
0050a090     7D 06                    JGE        0x0050a098
0050a092     40                       INC        EAX
0050a093     A3 08 8E 57 00           MOV        [0x00578e08],EAX
0050a098     3B C8                    CMP        ECX,EAX
0050a09a     74 10                    JZ         0x0050a0ac
0050a09c     E8 BF 3A 03 00           CALL       0x0053db60
0050a0a1     A1 08 8E 57 00           MOV        EAX,[0x00578e08]
0050a0a6     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050a0ac     F6 47 06 10              TEST       byte ptr [EDI + 0x6],0x10
0050a0b0     74 3D                    JZ         0x0050a0ef
0050a0b2     38 1D 6A 90 5E 00        CMP        byte ptr [0x005e906a],BL
0050a0b8     74 35                    JZ         0x0050a0ef
0050a0ba     80 B8 60 90 5E 00 63     CMP        byte ptr [EAX + 0x5e9060],0x63
0050a0c1     73 2C                    JNC        0x0050a0ef
0050a0c3     8A 15 6A 90 5E 00        MOV        DL,byte ptr [0x005e906a]
0050a0c9     FE CA                    DEC        DL
0050a0cb     88 15 6A 90 5E 00        MOV        byte ptr [0x005e906a],DL
0050a0d1     8A 88 60 90 5E 00        MOV        CL,byte ptr [EAX + 0x5e9060]
0050a0d7     FE C1                    INC        CL
0050a0d9     88 88 60 90 5E 00        MOV        byte ptr [EAX + 0x5e9060],CL
0050a0df     E8 9C 3A 03 00           CALL       0x0053db80
0050a0e4     A1 08 8E 57 00           MOV        EAX,[0x00578e08]
0050a0e9     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050a0ef     F6 47 06 40              TEST       byte ptr [EDI + 0x6],0x40
0050a0f3     74 30                    JZ         0x0050a125
0050a0f5     8B 15 E4 8D 57 00        MOV        EDX,dword ptr [0x00578de4]
0050a0fb     8A 88 60 90 5E 00        MOV        CL,byte ptr [EAX + 0x5e9060]
0050a101     3A 0C 02                 CMP        CL,byte ptr [EDX + EAX*0x1]
0050a104     76 1F                    JBE        0x0050a125
0050a106     FE C9                    DEC        CL
0050a108     88 88 60 90 5E 00        MOV        byte ptr [EAX + 0x5e9060],CL
0050a10e     A0 6A 90 5E 00           MOV        AL,[0x005e906a]
0050a113     FE C0                    INC        AL
0050a115     A2 6A 90 5E 00           MOV        [0x005e906a],AL
0050a11a     E8 81 3A 03 00           CALL       0x0053dba0
0050a11f     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050a125     A1 60 90 5E 00           MOV        EAX,[0x005e9060]
0050a12a     8B 15 64 90 5E 00        MOV        EDX,dword ptr [0x005e9064]
0050a130     B9 FF 00 00 00           MOV        ECX,0xff
0050a135     23 C1                    AND        EAX,ECX
0050a137     23 D1                    AND        EDX,ECX
0050a139     8A 8E 48 01 00 00        MOV        CL,byte ptr [ESI + 0x148]
0050a13f     03 C2                    ADD        EAX,EDX
0050a141     D1 F8                    SAR        EAX,0x1
0050a143     02 C1                    ADD        AL,CL
0050a145     33 C9                    XOR        ECX,ECX
0050a147     8A 0D 65 90 5E 00        MOV        CL,byte ptr [0x005e9065]
0050a14d     A2 67 90 5E 00           MOV        [0x005e9067],AL
0050a152     33 C0                    XOR        EAX,EAX
0050a154     8D 54 24 30              LEA        EDX,[ESP + 0x30]
0050a158     A0 62 90 5E 00           MOV        AL,[0x005e9062]
0050a15d     52                       PUSH       EDX
0050a15e     03 C1                    ADD        EAX,ECX
0050a160     8A 8E 4A 01 00 00        MOV        CL,byte ptr [ESI + 0x14a]
0050a166     D1 F8                    SAR        EAX,0x1
0050a168     02 C1                    ADD        AL,CL
0050a16a     55                       PUSH       EBP
0050a16b     A2 68 90 5E 00           MOV        [0x005e9068],AL
0050a170     E8 AB 1C 01 00           CALL       0x0051be20
0050a175     A1 C0 97 5E 00           MOV        EAX,[0x005e97c0]
0050a17a     83 C4 08                 ADD        ESP,0x8
0050a17d     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
0050a183     53                       PUSH       EBX
0050a184     53                       PUSH       EBX
0050a185     8B 11                    MOV        EDX,dword ptr [ECX]
0050a187     8B 0D 08 8E 57 00        MOV        ECX,dword ptr [0x00578e08]
0050a18d     8B C1                    MOV        EAX,ECX
0050a18f     89 54 24 18              MOV        dword ptr [ESP + 0x18],EDX
0050a193     C1 E0 03                 SHL        EAX,0x3
0050a196     2B C1                    SUB        EAX,ECX
0050a198     66 C7 44 24 28 18 00     MOV        word ptr [ESP + 0x28],0x18
0050a19f     C7 44 24 1C 00 00 00 01  MOV        dword ptr [ESP + 0x1c],0x1000000
0050a1a7     8D 04 80                 LEA        EAX,[EAX + EAX*0x4]
0050a1aa     8D 04 C1                 LEA        EAX,[ECX + EAX*0x8]
0050a1ad     C1 E0 03                 SHL        EAX,0x3
0050a1b0     66 8B 88 1C 35 57 00     MOV        CX,word ptr [EAX + 0x57351c]
0050a1b7     66 8B 90 1E 35 57 00     MOV        DX,word ptr [EAX + 0x57351e]
0050a1be     66 89 4C 24 20           MOV        word ptr [ESP + 0x20],CX
0050a1c3     8B 4C 24 38              MOV        ECX,dword ptr [ESP + 0x38]
0050a1c7     8D 44 24 18              LEA        EAX,[ESP + 0x18]
0050a1cb     66 83 C2 0C              ADD        DX,0xc
0050a1cf     50                       PUSH       EAX
0050a1d0     51                       PUSH       ECX
0050a1d1     55                       PUSH       EBP
0050a1d2     66 89 54 24 2E           MOV        word ptr [ESP + 0x2e],DX
0050a1d7     E8 14 7C 01 00           CALL       0x00521df0
0050a1dc     83 C4 14                 ADD        ESP,0x14
0050a1df     8D 54 24 10              LEA        EDX,[ESP + 0x10]
0050a1e3     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a1e7     52                       PUSH       EDX
0050a1e8     50                       PUSH       EAX
0050a1e9     55                       PUSH       EBP
0050a1ea     E8 D1 57 01 00           CALL       0x0051f9c0
0050a1ef     83 C4 0C                 ADD        ESP,0xc
0050a1f2     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a1f6     8D 44 24 30              LEA        EAX,[ESP + 0x30]
0050a1fa     50                       PUSH       EAX
0050a1fb     55                       PUSH       EBP
0050a1fc     E8 AF 1B 01 00           CALL       0x0051bdb0
0050a201     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050a207     A1 08 8E 57 00           MOV        EAX,[0x00578e08]
0050a20c     83 C4 08                 ADD        ESP,0x8
0050a20f     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050a212     53                       PUSH       EBX
0050a213     68 28 07 56 00           PUSH       0x560728
0050a218     8B 8C 82 E0 01 00 00     MOV        ECX,dword ptr [EDX + EAX*0x4 + 0x1e0]
0050a21f     53                       PUSH       EBX
0050a220     51                       PUSH       ECX
0050a221     E8 2A A1 03 00           CALL       0x00544350
0050a226     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050a22a     83 C4 10                 ADD        ESP,0x10
0050a22d     68 28 07 56 00           PUSH       0x560728
0050a232     52                       PUSH       EDX
0050a233     E8 68 A1 03 00           CALL       0x005443a0
0050a238     83 C4 08                 ADD        ESP,0x8
0050a23b     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a23f     E9 9C 03 00 00           JMP        0x0050a5e0
0050a244     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
0050a24b     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a24f     74 57                    JZ         0x0050a2a8
0050a251     F6 40 03 40              TEST       byte ptr [EAX + 0x3],0x40
0050a255     EB 6F                    JMP        0x0050a2c6
0050a257     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
0050a25e     75 31                    JNZ        0x0050a291
0050a260     38 1D 9F 90 5E 00        CMP        byte ptr [0x005e909f],BL
0050a266     74 0F                    JZ         0x0050a277
0050a268     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a272     E9 69 03 00 00           JMP        0x0050a5e0
0050a277     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
0050a27c     C7 05 F0 8D 57 00 39 00 00 00 MOV        dword ptr [0x00578df0],0x39
0050a286     48                       DEC        EAX
0050a287     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
0050a28c     E9 4F 03 00 00           JMP        0x0050a5e0
0050a291     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
0050a295     F6 41 03 40              TEST       byte ptr [ECX + 0x3],0x40
0050a299     EB 7F                    JMP        0x0050a31a
0050a29b     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
0050a2a2     75 1A                    JNZ        0x0050a2be
0050a2a4     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a2a8     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
0050a2ae     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
0050a2b3     40                       INC        EAX
0050a2b4     A3 F0 8D 57 00           MOV        [0x00578df0],EAX
0050a2b9     E9 22 03 00 00           JMP        0x0050a5e0
0050a2be     8B 54 24 34              MOV        EDX,dword ptr [ESP + 0x34]
0050a2c2     F6 42 03 40              TEST       byte ptr [EDX + 0x3],0x40
0050a2c6     0F 84 14 03 00 00        JZ         0x0050a5e0
0050a2cc     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
0050a2d1     88 1D 5B 07 56 00        MOV        byte ptr [0x0056075b],BL
0050a2d7     24 FD                    AND        AL,0xfd
0050a2d9     A2 EF 0F 56 00           MOV        [0x00560fef],AL
0050a2de     E8 BD 38 03 00           CALL       0x0053dba0
0050a2e3     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a2ed     E9 EE 02 00 00           JMP        0x0050a5e0
0050a2f2     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
0050a2f9     75 17                    JNZ        0x0050a312
0050a2fb     38 1D 9F 90 5E 00        CMP        byte ptr [0x005e909f],BL
0050a301     74 63                    JZ         0x0050a366
0050a303     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a30d     E9 CE 02 00 00           JMP        0x0050a5e0
0050a312     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a316     F6 40 03 40              TEST       byte ptr [EAX + 0x3],0x40
0050a31a     0F 84 C0 02 00 00        JZ         0x0050a5e0
0050a320     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
0050a325     88 1D 63 07 56 00        MOV        byte ptr [0x00560763],BL
0050a32b     24 FB                    AND        AL,0xfb
0050a32d     A2 EF 0F 56 00           MOV        [0x00560fef],AL
0050a332     E8 69 38 03 00           CALL       0x0053dba0
0050a337     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a341     E9 9A 02 00 00           JMP        0x0050a5e0
0050a346     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
0050a34d     75 31                    JNZ        0x0050a380
0050a34f     38 1D 9F 90 5E 00        CMP        byte ptr [0x005e909f],BL
0050a355     74 0F                    JZ         0x0050a366
0050a357     C7 05 F0 8D 57 00 80 00 00 00 MOV        dword ptr [0x00578df0],0x80
0050a361     E9 7A 02 00 00           JMP        0x0050a5e0
0050a366     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
0050a36b     C7 05 F0 8D 57 00 6E 00 00 00 MOV        dword ptr [0x00578df0],0x6e
0050a375     40                       INC        EAX
0050a376     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
0050a37b     E9 60 02 00 00           JMP        0x0050a5e0
0050a380     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
0050a384     F6 41 03 40              TEST       byte ptr [ECX + 0x3],0x40
0050a388     0F 84 52 02 00 00        JZ         0x0050a5e0
0050a38e     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
0050a393     88 1D 63 07 56 00        MOV        byte ptr [0x00560763],BL
0050a399     24 FB                    AND        AL,0xfb
0050a39b     A2 EF 0F 56 00           MOV        [0x00560fef],AL
0050a3a0     E8 FB 37 03 00           CALL       0x0053dba0
0050a3a5     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050a3ab     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a3b5     8A 81 50 01 00 00        MOV        AL,byte ptr [ECX + 0x150]
0050a3bb     83 E0 07                 AND        EAX,0x7
0050a3be     83 F8 07                 CMP        EAX,0x7
0050a3c1     0F 87 19 02 00 00        JA         0x0050a5e0
0050a3c7     FF 24 85 C8 A7 50 00     JMP        dword ptr [EAX*0x4 + 0x50a7c8]
0050a3ce     66 C7 81 48 01 00 00 10 00 MOV        word ptr [ECX + 0x148],0x10
0050a3d7     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050a3dd     66 C7 82 4A 01 00 00 02 00 MOV        word ptr [EDX + 0x14a],0x2
0050a3e6     E9 F5 01 00 00           JMP        0x0050a5e0
0050a3eb     66 C7 81 48 01 00 00 0F 00 MOV        word ptr [ECX + 0x148],0xf
0050a3f4     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050a3f9     66 C7 80 4A 01 00 00 04 00 MOV        word ptr [EAX + 0x14a],0x4
0050a402     E9 D9 01 00 00           JMP        0x0050a5e0
0050a407     66 C7 81 48 01 00 00 0E 00 MOV        word ptr [ECX + 0x148],0xe
0050a410     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050a416     66 C7 81 4A 01 00 00 08 00 MOV        word ptr [ECX + 0x14a],0x8
0050a41f     E9 BC 01 00 00           JMP        0x0050a5e0
0050a424     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
0050a42b     75 1A                    JNZ        0x0050a447
0050a42d     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a431     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
0050a437     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
0050a43c     48                       DEC        EAX
0050a43d     A3 F0 8D 57 00           MOV        [0x00578df0],EAX
0050a442     E9 99 01 00 00           JMP        0x0050a5e0
0050a447     8B 54 24 34              MOV        EDX,dword ptr [ESP + 0x34]
0050a44b     F6 42 03 40              TEST       byte ptr [EDX + 0x3],0x40
0050a44f     0F 84 8B 01 00 00        JZ         0x0050a5e0
0050a455     8A 0D EF 0F 56 00        MOV        CL,byte ptr [0x00560fef]
0050a45b     88 1D 5B 07 56 00        MOV        byte ptr [0x0056075b],BL
0050a461     80 E1 FD                 AND        CL,0xfd
0050a464     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
0050a46a     E8 31 37 03 00           CALL       0x0053dba0
0050a46f     FF 05 F0 8D 57 00        INC        dword ptr [0x00578df0]
0050a475     E9 66 01 00 00           JMP        0x0050a5e0
0050a47a     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
0050a481     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a485     75 15                    JNZ        0x0050a49c
0050a487     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
0050a48d     C7 05 F0 8D 57 00 5D 00 00 00 MOV        dword ptr [0x00578df0],0x5d
0050a497     E9 44 01 00 00           JMP        0x0050a5e0
0050a49c     F6 40 03 40              TEST       byte ptr [EAX + 0x3],0x40
0050a4a0     0F 84 3A 01 00 00        JZ         0x0050a5e0
0050a4a6     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
0050a4ab     88 1D 5B 07 56 00        MOV        byte ptr [0x0056075b],BL
0050a4b1     24 FD                    AND        AL,0xfd
0050a4b3     A2 EF 0F 56 00           MOV        [0x00560fef],AL
0050a4b8     E9 14 01 00 00           JMP        0x0050a5d1
0050a4bd     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
0050a4c4     0F 85 EB 00 00 00        JNZ        0x0050a5b5
0050a4ca     38 1D 9F 90 5E 00        CMP        byte ptr [0x005e909f],BL
0050a4d0     74 79                    JZ         0x0050a54b
0050a4d2     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050a4dc     8A 86 50 01 00 00        MOV        AL,byte ptr [ESI + 0x150]
0050a4e2     83 E0 07                 AND        EAX,0x7
0050a4e5     83 F8 07                 CMP        EAX,0x7
0050a4e8     0F 87 F2 00 00 00        JA         0x0050a5e0
0050a4ee     FF 24 85 E8 A7 50 00     JMP        dword ptr [EAX*0x4 + 0x50a7e8]
0050a4f5     66 C7 86 48 01 00 00 10 00 MOV        word ptr [ESI + 0x148],0x10
0050a4fe     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050a504     66 C7 81 4A 01 00 00 02 00 MOV        word ptr [ECX + 0x14a],0x2
0050a50d     E9 CE 00 00 00           JMP        0x0050a5e0
0050a512     66 C7 86 48 01 00 00 0F 00 MOV        word ptr [ESI + 0x148],0xf
0050a51b     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050a521     66 C7 82 4A 01 00 00 04 00 MOV        word ptr [EDX + 0x14a],0x4
0050a52a     E9 B1 00 00 00           JMP        0x0050a5e0
0050a52f     66 C7 86 48 01 00 00 0E 00 MOV        word ptr [ESI + 0x148],0xe
0050a538     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050a53d     66 C7 80 4A 01 00 00 08 00 MOV        word ptr [EAX + 0x14a],0x8
0050a546     E9 95 00 00 00           JMP        0x0050a5e0
0050a54b     33 C0                    XOR        EAX,EAX
0050a54d     B1 30                    MOV        CL,0x30
0050a54f     8B 15 C0 6B 56 00        MOV        EDX,dword ptr [0x00566bc0]
0050a555     40                       INC        EAX
0050a556     83 F8 07                 CMP        EAX,0x7
0050a559     88 4C 10 0B              MOV        byte ptr [EAX + EDX*0x1 + 0xb],CL
0050a55d     7C F0                    JL         0x0050a54f
0050a55f     8B 0D C0 6B 56 00        MOV        ECX,dword ptr [0x00566bc0]
0050a565     33 D2                    XOR        EDX,EDX
0050a567     88 5C 08 0C              MOV        byte ptr [EAX + ECX*0x1 + 0xc],BL
0050a56b     8B 0D C0 6B 56 00        MOV        ECX,dword ptr [0x00566bc0]
0050a571     8A 01                    MOV        AL,byte ptr [ECX]
0050a573     3A C3                    CMP        AL,BL
0050a575     74 15                    JZ         0x0050a58c
0050a577     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050a57d     42                       INC        EDX
0050a57e     41                       INC        ECX
0050a57f     88 84 32 CF 1F 00 00     MOV        byte ptr [EDX + ESI*0x1 + 0x1fcf],AL
0050a586     8A 01                    MOV        AL,byte ptr [ECX]
0050a588     3A C3                    CMP        AL,BL
0050a58a     75 EB                    JNZ        0x0050a577
0050a58c     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050a591     88 9C 02 D0 1F 00 00     MOV        byte ptr [EDX + EAX*0x1 + 0x1fd0],BL
0050a598     BA 01 00 00 00           MOV        EDX,0x1
0050a59d     88 15 1C E5 5D 00        MOV        byte ptr [0x005de51c],DL
0050a5a3     C7 05 E8 8D 57 00 FF FF FF FF MOV        dword ptr [0x00578de8],0xffffffff
0050a5ad     89 15 10 E5 5D 00        MOV        dword ptr [0x005de510],EDX
0050a5b3     EB 2B                    JMP        0x0050a5e0
0050a5b5     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
0050a5b9     F6 41 03 40              TEST       byte ptr [ECX + 0x3],0x40
0050a5bd     74 21                    JZ         0x0050a5e0
0050a5bf     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
0050a5c4     88 1D 63 07 56 00        MOV        byte ptr [0x00560763],BL
0050a5ca     24 FB                    AND        AL,0xfb
0050a5cc     A2 EF 0F 56 00           MOV        [0x00560fef],AL
0050a5d1     E8 CA 35 03 00           CALL       0x0053dba0
0050a5d6     C7 05 F0 8D 57 00 81 00 00 00 MOV        dword ptr [0x00578df0],0x81
0050a5e0     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
0050a5e6     53                       PUSH       EBX
0050a5e7     68 80 21 56 00           PUSH       0x562180
0050a5ec     53                       PUSH       EBX
0050a5ed     8B 42 70                 MOV        EAX,dword ptr [EDX + 0x70]
0050a5f0     8B 88 0C 01 00 00        MOV        ECX,dword ptr [EAX + 0x10c]
0050a5f6     51                       PUSH       ECX
0050a5f7     E8 54 9D 03 00           CALL       0x00544350
0050a5fc     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050a600     83 C4 10                 ADD        ESP,0x10
0050a603     68 80 21 56 00           PUSH       0x562180
0050a608     52                       PUSH       EDX
0050a609     E8 92 9D 03 00           CALL       0x005443a0
0050a60e     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050a612     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050a617     83 C4 08                 ADD        ESP,0x8
0050a61a     33 FF                    XOR        EDI,EDI
0050a61c     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
0050a622     BE 38 35 57 00           MOV        ESI,0x573538
0050a627     C7 44 24 34 10 01 00 00  MOV        dword ptr [ESP + 0x34],0x110
0050a62f     8B 51 30                 MOV        EDX,dword ptr [ECX + 0x30]
0050a632     C7 44 24 14 00 00 00 01  MOV        dword ptr [ESP + 0x14],0x1000000
0050a63a     89 54 24 10              MOV        dword ptr [ESP + 0x10],EDX
0050a63e     EB 04                    JMP        0x0050a644
0050a640     8B 6C 24 28              MOV        EBP,dword ptr [ESP + 0x28]
0050a644     66 8B 46 E4              MOV        AX,word ptr [ESI + -0x1c]
0050a648     66 8B 4E E6              MOV        CX,word ptr [ESI + -0x1a]
0050a64c     66 05 0C 00              ADD        AX,0xc
0050a650     66 83 C1 04              ADD        CX,0x4
0050a654     66 89 44 24 18           MOV        word ptr [ESP + 0x18],AX
0050a659     8D 44 24 30              LEA        EAX,[ESP + 0x30]
0050a65d     8D 57 04                 LEA        EDX,[EDI + 0x4]
0050a660     50                       PUSH       EAX
0050a661     55                       PUSH       EBP
0050a662     66 89 4C 24 22           MOV        word ptr [ESP + 0x22],CX
0050a667     66 89 54 24 28           MOV        word ptr [ESP + 0x28],DX
0050a66c     E8 AF 17 01 00           CALL       0x0051be20
0050a671     8B 54 24 38              MOV        EDX,dword ptr [ESP + 0x38]
0050a675     83 C4 08                 ADD        ESP,0x8
0050a678     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
0050a67c     53                       PUSH       EBX
0050a67d     53                       PUSH       EBX
0050a67e     51                       PUSH       ECX
0050a67f     52                       PUSH       EDX
0050a680     55                       PUSH       EBP
0050a681     E8 6A 77 01 00           CALL       0x00521df0
0050a686     83 C4 14                 ADD        ESP,0x14
0050a689     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
0050a68d     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a691     51                       PUSH       ECX
0050a692     50                       PUSH       EAX
0050a693     55                       PUSH       EBP
0050a694     E8 27 53 01 00           CALL       0x0051f9c0
0050a699     83 C4 0C                 ADD        ESP,0xc
0050a69c     8D 54 24 30              LEA        EDX,[ESP + 0x30]
0050a6a0     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a6a4     52                       PUSH       EDX
0050a6a5     55                       PUSH       EBP
0050a6a6     E8 05 17 01 00           CALL       0x0051bdb0
0050a6ab     83 C4 08                 ADD        ESP,0x8
0050a6ae     33 C0                    XOR        EAX,EAX
0050a6b0     83 FF 07                 CMP        EDI,0x7
0050a6b3     0F 9D C0                 SETGE      AL
0050a6b6     48                       DEC        EAX
0050a6b7     24 F8                    AND        AL,0xf8
0050a6b9     83 C0 10                 ADD        EAX,0x10
0050a6bc     88 06                    MOV        byte ptr [ESI],AL
0050a6be     8A 87 60 90 5E 00        MOV        AL,byte ptr [EDI + 0x5e9060]
0050a6c4     3C 64                    CMP        AL,0x64
0050a6c6     73 03                    JNC        0x0050a6cb
0050a6c8     80 06 08                 ADD        byte ptr [ESI],0x8
0050a6cb     3C 0A                    CMP        AL,0xa
0050a6cd     73 03                    JNC        0x0050a6d2
0050a6cf     80 06 08                 ADD        byte ptr [ESI],0x8
0050a6d2     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050a6d8     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050a6dc     8D 6E E0                 LEA        EBP,[ESI + -0x20]
0050a6df     53                       PUSH       EBX
0050a6e0     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050a6e3     55                       PUSH       EBP
0050a6e4     53                       PUSH       EBX
0050a6e5     8B 0C 02                 MOV        ECX,dword ptr [EDX + EAX*0x1]
0050a6e8     51                       PUSH       ECX
0050a6e9     E8 62 9C 03 00           CALL       0x00544350
0050a6ee     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050a6f2     83 C4 10                 ADD        ESP,0x10
0050a6f5     55                       PUSH       EBP
0050a6f6     52                       PUSH       EDX
0050a6f7     E8 A4 9C 03 00           CALL       0x005443a0
0050a6fc     83 C4 08                 ADD        ESP,0x8
0050a6ff     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a703     55                       PUSH       EBP
0050a704     50                       PUSH       EAX
0050a705     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
0050a709     50                       PUSH       EAX
0050a70a     E8 31 AC 03 00           CALL       0x00545340
0050a70f     8B 4C 24 40              MOV        ECX,dword ptr [ESP + 0x40]
0050a713     83 C4 0C                 ADD        ESP,0xc
0050a716     83 C1 04                 ADD        ECX,0x4
0050a719     47                       INC        EDI
0050a71a     81 C6 C8 08 00 00        ADD        ESI,0x8c8
0050a720     81 F9 34 01 00 00        CMP        ECX,0x134
0050a726     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050a72a     89 4C 24 34              MOV        dword ptr [ESP + 0x34],ECX
0050a72e     0F 8C 0C FF FF FF        JL         0x0050a640
0050a734     5F                       POP        EDI
0050a735     5E                       POP        ESI
0050a736     5D                       POP        EBP
0050a737     5B                       POP        EBX
0050a738     83 C4 14                 ADD        ESP,0x14
0050a73b     C3                       RET        

