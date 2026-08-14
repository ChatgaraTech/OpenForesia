; Function: FUN_00508860
; Entry:    00508860
; Body:     [[00508860, 00508de0]]

; --------------------------------------------------
; Range: 00508860 - 00508de0
; --------------------------------------------------
00508860     A1 48 2A 56 00           MOV        EAX,[0x00562a48]
00508865     83 EC 14                 SUB        ESP,0x14
00508868     53                       PUSH       EBX
00508869     33 DB                    XOR        EBX,EBX
0050886b     56                       PUSH       ESI
0050886c     3B C3                    CMP        EAX,EBX
0050886e     57                       PUSH       EDI
0050886f     0F 84 B1 00 00 00        JZ         0x00508926
00508875     B8 28 00 00 00           MOV        EAX,0x28
0050887a     33 C9                    XOR        ECX,ECX
0050887c     66 A3 1C 35 57 00        MOV        [0x0057351c],AX
00508882     66 A3 22 35 57 00        MOV        [0x00573522],AX
00508888     B8 68 00 00 00           MOV        EAX,0x68
0050888d     89 1D 48 2A 56 00        MOV        dword ptr [0x00562a48],EBX
00508893     66 A3 E4 3D 57 00        MOV        [0x00573de4],AX
00508899     66 A3 E6 3D 57 00        MOV        [0x00573de6],AX
0050889f     A1 38 33 57 00           MOV        EAX,[0x00573338]
005088a4     89 1D F8 8D 57 00        MOV        dword ptr [0x00578df8],EBX
005088aa     89 1D FC 8D 57 00        MOV        dword ptr [0x00578dfc],EBX
005088b0     66 C7 05 1E 35 57 00 38 00 MOV        word ptr [0x0057351e],0x38
005088b9     66 C7 05 20 35 57 00 E0 00 MOV        word ptr [0x00573520],0xe0
005088c2     C7 05 38 35 57 00 24 0C 07 01 MOV        dword ptr [0x00573538],0x1070c24
005088cc     C7 05 3C 35 57 00 00 00 00 01 MOV        dword ptr [0x0057353c],0x1000000
005088d6     66 C7 05 E8 3D 57 00 60 00 MOV        word ptr [0x00573de8],0x60
005088df     C6 05 F2 3D 57 00 0B     MOV        byte ptr [0x00573df2],0xb
005088e6     66 C7 05 EA 3D 57 00 18 00 MOV        word ptr [0x00573dea],0x18
005088ef     C6 05 F4 3D 57 00 05     MOV        byte ptr [0x00573df4],0x5
005088f6     C7 05 00 3E 57 00 10 0C 07 01 MOV        dword ptr [0x00573e00],0x1070c10
00508900     C7 05 04 3E 57 00 00 00 00 01 MOV        dword ptr [0x00573e04],0x1000000
0050890a     C7 05 50 2A 56 00 02 00 00 00 MOV        dword ptr [0x00562a50],0x2
00508914     8A 88 50 01 00 00        MOV        CL,byte ptr [EAX + 0x150]
0050891a     8A 91 70 2B 56 00        MOV        DL,byte ptr [ECX + 0x562b70]
00508920     88 90 53 01 00 00        MOV        byte ptr [EAX + 0x153],DL
00508926     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050892b     66 0F B6 80 50 01 00 00  MOVZX      AX,byte ptr [EAX + 0x150]
00508933     83 E8 08                 SUB        EAX,0x8
00508936     C1 E0 08                 SHL        EAX,0x8
00508939     66 2B 05 2A 49 60 00     SUB        AX,word ptr [0x0060492a]
00508940     25 FF 0F 00 00           AND        EAX,0xfff
00508945     66 3D 00 08              CMP        AX,0x800
00508949     7D 08                    JGE        0x00508953
0050894b     0F BF C0                 MOVSX      EAX,AX
0050894e     C1 F8 02                 SAR        EAX,0x2
00508951     EB 0F                    JMP        0x00508962
00508953     0F BF C8                 MOVSX      ECX,AX
00508956     B8 00 10 00 00           MOV        EAX,0x1000
0050895b     2B C1                    SUB        EAX,ECX
0050895d     C1 F8 02                 SAR        EAX,0x2
00508960     F7 D8                    NEG        EAX
00508962     66 01 05 2A 49 60 00     ADD        word ptr [0x0060492a],AX
00508969     66 A1 A0 93 5E 00        MOV        AX,[0x005e93a0]
0050896f     0F BF D0                 MOVSX      EDX,AX
00508972     B9 60 00 00 00           MOV        ECX,0x60
00508977     68 E0 08 60 00           PUSH       0x6008e0
0050897c     2B CA                    SUB        ECX,EDX
0050897e     68 28 49 60 00           PUSH       0x604928
00508983     C1 F9 02                 SAR        ECX,0x2
00508986     66 03 C1                 ADD        AX,CX
00508989     B9 60 00 00 00           MOV        ECX,0x60
0050898e     66 A3 A0 93 5E 00        MOV        [0x005e93a0],AX
00508994     66 A1 A2 93 5E 00        MOV        AX,[0x005e93a2]
0050899a     0F BF D0                 MOVSX      EDX,AX
0050899d     2B CA                    SUB        ECX,EDX
0050899f     C1 F9 02                 SAR        ECX,0x2
005089a2     66 03 C1                 ADD        AX,CX
005089a5     B9 60 00 00 00           MOV        ECX,0x60
005089aa     66 A3 A2 93 5E 00        MOV        [0x005e93a2],AX
005089b0     66 A1 A4 93 5E 00        MOV        AX,[0x005e93a4]
005089b6     0F BF D0                 MOVSX      EDX,AX
005089b9     2B CA                    SUB        ECX,EDX
005089bb     C1 F9 02                 SAR        ECX,0x2
005089be     66 03 C1                 ADD        AX,CX
005089c1     66 A3 A4 93 5E 00        MOV        [0x005e93a4],AX
005089c7     E8 34 39 01 00           CALL       0x0051c300
005089cc     0F BF 15 A4 93 5E 00     MOVSX      EDX,word ptr [0x005e93a4]
005089d3     0F BF 05 A2 93 5E 00     MOVSX      EAX,word ptr [0x005e93a2]
005089da     0F BF 0D A0 93 5E 00     MOVSX      ECX,word ptr [0x005e93a0]
005089e1     83 C4 08                 ADD        ESP,0x8
005089e4     52                       PUSH       EDX
005089e5     50                       PUSH       EAX
005089e6     51                       PUSH       ECX
005089e7     E8 B4 D0 03 00           CALL       0x00545aa0
005089ec     A1 38 33 57 00           MOV        EAX,[0x00573338]
005089f1     33 D2                    XOR        EDX,EDX
005089f3     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
005089f9     66 A1 7A 2B 57 00        MOV        AX,[0x00572b7a]
005089ff     0F BF C8                 MOVSX      ECX,AX
00508a02     C1 E2 08                 SHL        EDX,0x8
00508a05     F7 D9                    NEG        ECX
00508a07     2B CA                    SUB        ECX,EDX
00508a09     C1 F9 02                 SAR        ECX,0x2
00508a0c     66 03 C1                 ADD        AX,CX
00508a0f     66 A3 7A 2B 57 00        MOV        [0x00572b7a],AX
00508a15     A1 4C 2A 56 00           MOV        EAX,[0x00562a4c]
00508a1a     3B C3                    CMP        EAX,EBX
00508a1c     0F 85 49 02 00 00        JNZ        0x00508c6b
00508a22     8B 15 F0 8D 57 00        MOV        EDX,dword ptr [0x00578df0]
00508a28     8D 42 A1                 LEA        EAX,[EDX + -0x5f]
00508a2b     83 F8 03                 CMP        EAX,0x3
00508a2e     0F 87 37 02 00 00        JA         0x00508c6b
00508a34     FF 24 85 E4 8D 50 00     JMP        dword ptr [EAX*0x4 + 0x508de4]
00508a3b     8B 74 24 30              MOV        ESI,dword ptr [ESP + 0x30]
00508a3f     66 8B 46 02              MOV        AX,word ptr [ESI + 0x2]
00508a43     A8 08                    TEST       AL,0x8
00508a45     74 14                    JZ         0x00508a5b
00508a47     E8 34 51 03 00           CALL       0x0053db80
00508a4c     C7 05 F0 8D 57 00 61 00 00 00 MOV        dword ptr [0x00578df0],0x61
00508a56     E9 10 02 00 00           JMP        0x00508c6b
00508a5b     F6 C4 40                 TEST       AH,0x40
00508a5e     74 21                    JZ         0x00508a81
00508a60     E8 3B 51 03 00           CALL       0x0053dba0
00508a65     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00508a6a     C7 05 F0 8D 57 00 13 00 00 00 MOV        dword ptr [0x00578df0],0x13
00508a74     83 E8 02                 SUB        EAX,0x2
00508a77     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
00508a7c     E9 EA 01 00 00           JMP        0x00508c6b
00508a81     F6 C4 80                 TEST       AH,0x80
00508a84     74 46                    JZ         0x00508acc
00508a86     25 FF 7F 00 00           AND        EAX,0x7fff
00508a8b     66 89 46 02              MOV        word ptr [ESI + 0x2],AX
00508a8f     E8 EC 50 03 00           CALL       0x0053db80
00508a94     8A 0D FC 8D 57 00        MOV        CL,byte ptr [0x00578dfc]
00508a9a     A1 38 33 57 00           MOV        EAX,[0x00573338]
00508a9f     8A 15 F8 8D 57 00        MOV        DL,byte ptr [0x00578df8]
00508aa5     C7 05 F0 8D 57 00 61 00 00 00 MOV        dword ptr [0x00578df0],0x61
00508aaf     C0 E1 02                 SHL        CL,0x2
00508ab2     0A CA                    OR         CL,DL
00508ab4     8A 90 53 01 00 00        MOV        DL,byte ptr [EAX + 0x153]
00508aba     D0 E1                    SHL        CL,0x1
00508abc     80 E2 01                 AND        DL,0x1
00508abf     0A CA                    OR         CL,DL
00508ac1     88 88 53 01 00 00        MOV        byte ptr [EAX + 0x153],CL
00508ac7     E9 9F 01 00 00           JMP        0x00508c6b
00508acc     A1 FC 8D 57 00           MOV        EAX,[0x00578dfc]
00508ad1     8B 0D F8 8D 57 00        MOV        ECX,dword ptr [0x00578df8]
00508ad7     8B D0                    MOV        EDX,EAX
00508ad9     C1 E2 08                 SHL        EDX,0x8
00508adc     0B D1                    OR         EDX,ECX
00508ade     F6 46 06 10              TEST       byte ptr [ESI + 0x6],0x10
00508ae2     74 0A                    JZ         0x00508aee
00508ae4     3B C3                    CMP        EAX,EBX
00508ae6     74 06                    JZ         0x00508aee
00508ae8     48                       DEC        EAX
00508ae9     A3 FC 8D 57 00           MOV        [0x00578dfc],EAX
00508aee     F6 46 06 40              TEST       byte ptr [ESI + 0x6],0x40
00508af2     74 0B                    JZ         0x00508aff
00508af4     83 F8 01                 CMP        EAX,0x1
00508af7     7D 06                    JGE        0x00508aff
00508af9     40                       INC        EAX
00508afa     A3 FC 8D 57 00           MOV        [0x00578dfc],EAX
00508aff     F6 46 06 80              TEST       byte ptr [ESI + 0x6],0x80
00508b03     74 1C                    JZ         0x00508b21
00508b05     3B CB                    CMP        ECX,EBX
00508b07     74 03                    JZ         0x00508b0c
00508b09     49                       DEC        ECX
00508b0a     EB 0F                    JMP        0x00508b1b
00508b0c     3B C3                    CMP        EAX,EBX
00508b0e     74 11                    JZ         0x00508b21
00508b10     48                       DEC        EAX
00508b11     B9 03 00 00 00           MOV        ECX,0x3
00508b16     A3 FC 8D 57 00           MOV        [0x00578dfc],EAX
00508b1b     89 0D F8 8D 57 00        MOV        dword ptr [0x00578df8],ECX
00508b21     F6 46 06 20              TEST       byte ptr [ESI + 0x6],0x20
00508b25     74 1B                    JZ         0x00508b42
00508b27     83 F9 03                 CMP        ECX,0x3
00508b2a     7D 03                    JGE        0x00508b2f
00508b2c     41                       INC        ECX
00508b2d     EB 0D                    JMP        0x00508b3c
00508b2f     83 F8 01                 CMP        EAX,0x1
00508b32     7D 0E                    JGE        0x00508b42
00508b34     40                       INC        EAX
00508b35     33 C9                    XOR        ECX,ECX
00508b37     A3 FC 8D 57 00           MOV        [0x00578dfc],EAX
00508b3c     89 0D F8 8D 57 00        MOV        dword ptr [0x00578df8],ECX
00508b42     C1 E0 08                 SHL        EAX,0x8
00508b45     0B C1                    OR         EAX,ECX
00508b47     3B D0                    CMP        EDX,EAX
00508b49     0F 84 1C 01 00 00        JZ         0x00508c6b
00508b4f     E8 0C 50 03 00           CALL       0x0053db60
00508b54     E9 12 01 00 00           JMP        0x00508c6b
00508b59     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
00508b60     75 42                    JNZ        0x00508ba4
00508b62     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
00508b66     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
00508b6c     A0 FC 8D 57 00           MOV        AL,[0x00578dfc]
00508b71     8A 0D F8 8D 57 00        MOV        CL,byte ptr [0x00578df8]
00508b77     8B 35 F0 8D 57 00        MOV        ESI,dword ptr [0x00578df0]
00508b7d     C0 E0 02                 SHL        AL,0x2
00508b80     0A C1                    OR         AL,CL
00508b82     8A 0D 9F 90 5E 00        MOV        CL,byte ptr [0x005e909f]
00508b88     D0 E0                    SHL        AL,0x1
00508b8a     46                       INC        ESI
00508b8b     0A C1                    OR         AL,CL
00508b8d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
00508b93     89 35 F0 8D 57 00        MOV        dword ptr [0x00578df0],ESI
00508b99     88 81 53 01 00 00        MOV        byte ptr [ECX + 0x153],AL
00508b9f     E9 C7 00 00 00           JMP        0x00508c6b
00508ba4     8B 54 24 30              MOV        EDX,dword ptr [ESP + 0x30]
00508ba8     F6 42 03 40              TEST       byte ptr [EDX + 0x3],0x40
00508bac     0F 84 B9 00 00 00        JZ         0x00508c6b
00508bb2     8A 0D EF 0F 56 00        MOV        CL,byte ptr [0x00560fef]
00508bb8     88 1D 63 07 56 00        MOV        byte ptr [0x00560763],BL
00508bbe     80 E1 FB                 AND        CL,0xfb
00508bc1     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
00508bc7     E8 D4 4F 03 00           CALL       0x0053dba0
00508bcc     FF 0D F0 8D 57 00        DEC        dword ptr [0x00578df0]
00508bd2     E9 94 00 00 00           JMP        0x00508c6b
00508bd7     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
00508bde     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
00508be2     75 13                    JNZ        0x00508bf7
00508be4     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
00508bea     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
00508bef     40                       INC        EAX
00508bf0     A3 F0 8D 57 00           MOV        [0x00578df0],EAX
00508bf5     EB 74                    JMP        0x00508c6b
00508bf7     F6 40 03 40              TEST       byte ptr [EAX + 0x3],0x40
00508bfb     74 6E                    JZ         0x00508c6b
00508bfd     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
00508c02     88 1D 5B 07 56 00        MOV        byte ptr [0x0056075b],BL
00508c08     24 FD                    AND        AL,0xfd
00508c0a     A2 EF 0F 56 00           MOV        [0x00560fef],AL
00508c0f     EB 4B                    JMP        0x00508c5c
00508c11     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
00508c18     75 26                    JNZ        0x00508c40
00508c1a     38 1D 9F 90 5E 00        CMP        byte ptr [0x005e909f],BL
00508c20     75 3F                    JNZ        0x00508c61
00508c22     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00508c27     C6 05 F4 3D 57 00 04     MOV        byte ptr [0x00573df4],0x4
00508c2e     40                       INC        EAX
00508c2f     C7 05 F0 8D 57 00 2E 00 00 00 MOV        dword ptr [0x00578df0],0x2e
00508c39     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
00508c3e     EB 2B                    JMP        0x00508c6b
00508c40     8B 4C 24 30              MOV        ECX,dword ptr [ESP + 0x30]
00508c44     F6 41 03 40              TEST       byte ptr [ECX + 0x3],0x40
00508c48     74 21                    JZ         0x00508c6b
00508c4a     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
00508c4f     88 1D 63 07 56 00        MOV        byte ptr [0x00560763],BL
00508c55     24 FB                    AND        AL,0xfb
00508c57     A2 EF 0F 56 00           MOV        [0x00560fef],AL
00508c5c     E8 3F 4F 03 00           CALL       0x0053dba0
00508c61     C7 05 F0 8D 57 00 5F 00 00 00 MOV        dword ptr [0x00578df0],0x5f
00508c6b     8B 7C 24 24              MOV        EDI,dword ptr [ESP + 0x24]
00508c6f     8D 54 24 2C              LEA        EDX,[ESP + 0x2c]
00508c73     52                       PUSH       EDX
00508c74     57                       PUSH       EDI
00508c75     E8 A6 31 01 00           CALL       0x0051be20
00508c7a     A1 C0 97 5E 00           MOV        EAX,[0x005e97c0]
00508c7f     83 C4 08                 ADD        ESP,0x8
00508c82     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
00508c88     A1 F8 8D 57 00           MOV        EAX,[0x00578df8]
00508c8d     53                       PUSH       EBX
00508c8e     53                       PUSH       EBX
00508c8f     8B 11                    MOV        EDX,dword ptr [ECX]
00508c91     8B 0D 1C 35 57 00        MOV        ECX,dword ptr [0x0057351c]
00508c97     8D 04 40                 LEA        EAX,[EAX + EAX*0x2]
00508c9a     89 54 24 14              MOV        dword ptr [ESP + 0x14],EDX
00508c9e     C1 E0 04                 SHL        EAX,0x4
00508ca1     66 C7 44 24 24 18 00     MOV        word ptr [ESP + 0x24],0x18
00508ca8     C7 44 24 18 00 00 00 01  MOV        dword ptr [ESP + 0x18],0x1000000
00508cb0     8D 54 08 14              LEA        EDX,[EAX + ECX*0x1 + 0x14]
00508cb4     A1 FC 8D 57 00           MOV        EAX,[0x00578dfc]
00508cb9     C1 E0 04                 SHL        EAX,0x4
00508cbc     66 03 05 1E 35 57 00     ADD        AX,word ptr [0x0057351e]
00508cc3     66 89 54 24 1C           MOV        word ptr [ESP + 0x1c],DX
00508cc8     8B 54 24 34              MOV        EDX,dword ptr [ESP + 0x34]
00508ccc     8D 4C 24 14              LEA        ECX,[ESP + 0x14]
00508cd0     51                       PUSH       ECX
00508cd1     83 C0 0A                 ADD        EAX,0xa
00508cd4     52                       PUSH       EDX
00508cd5     57                       PUSH       EDI
00508cd6     66 89 44 24 2A           MOV        word ptr [ESP + 0x2a],AX
00508cdb     E8 10 91 01 00           CALL       0x00521df0
00508ce0     83 C4 14                 ADD        ESP,0x14
00508ce3     8D 4C 24 0C              LEA        ECX,[ESP + 0xc]
00508ce7     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508ceb     51                       PUSH       ECX
00508cec     50                       PUSH       EAX
00508ced     57                       PUSH       EDI
00508cee     E8 CD 6C 01 00           CALL       0x0051f9c0
00508cf3     83 C4 0C                 ADD        ESP,0xc
00508cf6     8D 54 24 2C              LEA        EDX,[ESP + 0x2c]
00508cfa     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508cfe     52                       PUSH       EDX
00508cff     57                       PUSH       EDI
00508d00     E8 AB 30 01 00           CALL       0x0051bdb0
00508d05     83 C4 08                 ADD        ESP,0x8
00508d08     53                       PUSH       EBX
00508d09     68 18 35 57 00           PUSH       0x573518
00508d0e     53                       PUSH       EBX
00508d0f     68 F8 06 56 00           PUSH       0x5606f8
00508d14     E8 37 B6 03 00           CALL       0x00544350
00508d19     8B 44 24 3C              MOV        EAX,dword ptr [ESP + 0x3c]
00508d1d     83 C4 10                 ADD        ESP,0x10
00508d20     68 18 35 57 00           PUSH       0x573518
00508d25     50                       PUSH       EAX
00508d26     E8 75 B6 03 00           CALL       0x005443a0
00508d2b     83 C4 08                 ADD        ESP,0x8
00508d2e     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508d32     BE 00 05 00 00           MOV        ESI,0x500
00508d37     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
00508d3d     53                       PUSH       EBX
00508d3e     68 18 35 57 00           PUSH       0x573518
00508d43     53                       PUSH       EBX
00508d44     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
00508d47     8B 04 32                 MOV        EAX,dword ptr [EDX + ESI*0x1]
00508d4a     50                       PUSH       EAX
00508d4b     E8 00 B6 03 00           CALL       0x00544350
00508d50     8B 4C 24 3C              MOV        ECX,dword ptr [ESP + 0x3c]
00508d54     83 C4 10                 ADD        ESP,0x10
00508d57     68 18 35 57 00           PUSH       0x573518
00508d5c     51                       PUSH       ECX
00508d5d     E8 3E B6 03 00           CALL       0x005443a0
00508d62     83 C6 04                 ADD        ESI,0x4
00508d65     83 C4 08                 ADD        ESP,0x8
00508d68     81 FE 08 05 00 00        CMP        ESI,0x508
00508d6e     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508d72     7C C3                    JL         0x00508d37
00508d74     68 18 35 57 00           PUSH       0x573518
00508d79     50                       PUSH       EAX
00508d7a     57                       PUSH       EDI
00508d7b     E8 C0 C5 03 00           CALL       0x00545340
00508d80     8B 0D F0 8D 57 00        MOV        ECX,dword ptr [0x00578df0]
00508d86     83 C4 0C                 ADD        ESP,0xc
00508d89     83 F9 2E                 CMP        ECX,0x2e
00508d8c     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508d90     74 48                    JZ         0x00508dda
00508d92     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
00508d98     53                       PUSH       EBX
00508d99     68 E0 3D 57 00           PUSH       0x573de0
00508d9e     68 48 2B 57 00           PUSH       0x572b48
00508da3     8B 42 70                 MOV        EAX,dword ptr [EDX + 0x70]
00508da6     8B 88 8C 01 00 00        MOV        ECX,dword ptr [EAX + 0x18c]
00508dac     51                       PUSH       ECX
00508dad     E8 9E B5 03 00           CALL       0x00544350
00508db2     8B 54 24 3C              MOV        EDX,dword ptr [ESP + 0x3c]
00508db6     83 C4 10                 ADD        ESP,0x10
00508db9     68 E0 3D 57 00           PUSH       0x573de0
00508dbe     52                       PUSH       EDX
00508dbf     E8 DC B5 03 00           CALL       0x005443a0
00508dc4     83 C4 08                 ADD        ESP,0x8
00508dc7     89 44 24 2C              MOV        dword ptr [ESP + 0x2c],EAX
00508dcb     68 E0 3D 57 00           PUSH       0x573de0
00508dd0     50                       PUSH       EAX
00508dd1     57                       PUSH       EDI
00508dd2     E8 69 C5 03 00           CALL       0x00545340
00508dd7     83 C4 0C                 ADD        ESP,0xc
00508dda     5F                       POP        EDI
00508ddb     5E                       POP        ESI
00508ddc     5B                       POP        EBX
00508ddd     83 C4 14                 ADD        ESP,0x14
00508de0     C3                       RET        

