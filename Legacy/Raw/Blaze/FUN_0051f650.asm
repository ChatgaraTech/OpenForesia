; Function: FUN_0051f650
; Entry:    0051f650
; Body:     [[0051f650, 0051f93a]]

; --------------------------------------------------
; Range: 0051f650 - 0051f93a
; --------------------------------------------------
0051f650     83 EC 08                 SUB        ESP,0x8
0051f653     53                       PUSH       EBX
0051f654     55                       PUSH       EBP
0051f655     56                       PUSH       ESI
0051f656     57                       PUSH       EDI
0051f657     68 E0 08 60 00           PUSH       0x6008e0
0051f65c     E8 69 64 02 00           CALL       0x00545aca
0051f661     68 E0 08 60 00           PUSH       0x6008e0
0051f666     E8 25 65 02 00           CALL       0x00545b90
0051f66b     8B 44 24 1C              MOV        EAX,dword ptr [ESP + 0x1c]
0051f66f     8B 1D D0 08 60 00        MOV        EBX,dword ptr [0x006008d0]
0051f675     8B 15 D0 86 60 00        MOV        EDX,dword ptr [0x006086d0]
0051f67b     8B 08                    MOV        ECX,dword ptr [EAX]
0051f67d     03 CB                    ADD        ECX,EBX
0051f67f     C1 F9 0C                 SAR        ECX,0xc
0051f682     66 89 0A                 MOV        word ptr [EDX],CX
0051f685     8B 0D D4 08 60 00        MOV        ECX,dword ptr [0x006008d4]
0051f68b     8B 70 04                 MOV        ESI,dword ptr [EAX + 0x4]
0051f68e     8B 15 D0 86 60 00        MOV        EDX,dword ptr [0x006086d0]
0051f694     2B CE                    SUB        ECX,ESI
0051f696     C1 F9 0C                 SAR        ECX,0xc
0051f699     66 89 4A 02              MOV        word ptr [EDX + 0x2],CX
0051f69d     8B 40 08                 MOV        EAX,dword ptr [EAX + 0x8]
0051f6a0     8B 2D D8 08 60 00        MOV        EBP,dword ptr [0x006008d8]
0051f6a6     8B 0D D0 86 60 00        MOV        ECX,dword ptr [0x006086d0]
0051f6ac     03 C5                    ADD        EAX,EBP
0051f6ae     C1 F8 0C                 SAR        EAX,0xc
0051f6b1     66 89 41 04              MOV        word ptr [ECX + 0x4],AX
0051f6b5     8B 0D D0 86 60 00        MOV        ECX,dword ptr [0x006086d0]
0051f6bb     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051f6c0     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051f6c4     0F BF 70 02              MOVSX      ESI,word ptr [EAX + 0x2]
0051f6c8     0F BF 78 04              MOVSX      EDI,word ptr [EAX + 0x4]
0051f6cc     0F AF D6                 IMUL       EDX,ESI
0051f6cf     0F BF 71 04              MOVSX      ESI,word ptr [ECX + 0x4]
0051f6d3     0F AF F7                 IMUL       ESI,EDI
0051f6d6     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051f6d9     03 D6                    ADD        EDX,ESI
0051f6db     8B 58 14                 MOV        EBX,dword ptr [EAX + 0x14]
0051f6de     0F BF 30                 MOVSX      ESI,word ptr [EAX]
0051f6e1     0F AF F1                 IMUL       ESI,ECX
0051f6e4     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051f6e9     03 D6                    ADD        EDX,ESI
0051f6eb     C1 FA 0C                 SAR        EDX,0xc
0051f6ee     03 D3                    ADD        EDX,EBX
0051f6f0     89 10                    MOV        dword ptr [EAX],EDX
0051f6f2     8B 0D D0 86 60 00        MOV        ECX,dword ptr [0x006086d0]
0051f6f8     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051f6fd     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051f701     0F BF 70 08              MOVSX      ESI,word ptr [EAX + 0x8]
0051f705     0F BF 78 0A              MOVSX      EDI,word ptr [EAX + 0xa]
0051f709     0F AF D6                 IMUL       EDX,ESI
0051f70c     0F BF 71 04              MOVSX      ESI,word ptr [ECX + 0x4]
0051f710     0F AF F7                 IMUL       ESI,EDI
0051f713     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051f716     03 D6                    ADD        EDX,ESI
0051f718     8B 58 18                 MOV        EBX,dword ptr [EAX + 0x18]
0051f71b     0F BF 70 06              MOVSX      ESI,word ptr [EAX + 0x6]
0051f71f     0F AF F1                 IMUL       ESI,ECX
0051f722     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051f727     03 D6                    ADD        EDX,ESI
0051f729     C1 FA 0C                 SAR        EDX,0xc
0051f72c     03 D3                    ADD        EDX,EBX
0051f72e     89 50 04                 MOV        dword ptr [EAX + 0x4],EDX
0051f731     8B 0D D0 86 60 00        MOV        ECX,dword ptr [0x006086d0]
0051f737     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051f73c     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051f740     0F BF 70 0E              MOVSX      ESI,word ptr [EAX + 0xe]
0051f744     0F BF 78 10              MOVSX      EDI,word ptr [EAX + 0x10]
0051f748     0F AF D6                 IMUL       EDX,ESI
0051f74b     0F BF 71 04              MOVSX      ESI,word ptr [ECX + 0x4]
0051f74f     0F AF F7                 IMUL       ESI,EDI
0051f752     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051f755     03 D6                    ADD        EDX,ESI
0051f757     8B 78 1C                 MOV        EDI,dword ptr [EAX + 0x1c]
0051f75a     0F BF 70 0C              MOVSX      ESI,word ptr [EAX + 0xc]
0051f75e     0F AF F1                 IMUL       ESI,ECX
0051f761     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051f766     03 D6                    ADD        EDX,ESI
0051f768     C1 FA 0C                 SAR        EDX,0xc
0051f76b     03 D7                    ADD        EDX,EDI
0051f76d     89 50 08                 MOV        dword ptr [EAX + 0x8],EDX
0051f770     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051f776     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051f77b     66 8B 11                 MOV        DX,word ptr [ECX]
0051f77e     66 89 10                 MOV        word ptr [EAX],DX
0051f781     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051f787     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051f78c     66 8B 51 04              MOV        DX,word ptr [ECX + 0x4]
0051f790     66 89 50 02              MOV        word ptr [EAX + 0x2],DX
0051f794     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051f79a     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051f79f     66 8B 51 08              MOV        DX,word ptr [ECX + 0x8]
0051f7a3     66 89 50 04              MOV        word ptr [EAX + 0x4],DX
0051f7a7     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051f7ad     33 C0                    XOR        EAX,EAX
0051f7af     66 89 01                 MOV        word ptr [ECX],AX
0051f7b2     8B 15 DC 86 60 00        MOV        EDX,dword ptr [0x006086dc]
0051f7b8     66 89 42 02              MOV        word ptr [EDX + 0x2],AX
0051f7bc     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051f7c2     66 89 41 04              MOV        word ptr [ECX + 0x4],AX
0051f7c6     8B 15 D4 86 60 00        MOV        EDX,dword ptr [0x006086d4]
0051f7cc     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051f7d2     8B 02                    MOV        EAX,dword ptr [EDX]
0051f7d4     89 41 14                 MOV        dword ptr [ECX + 0x14],EAX
0051f7d7     8B 15 D4 86 60 00        MOV        EDX,dword ptr [0x006086d4]
0051f7dd     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051f7e3     8B 42 04                 MOV        EAX,dword ptr [EDX + 0x4]
0051f7e6     89 41 18                 MOV        dword ptr [ECX + 0x18],EAX
0051f7e9     8B 15 D4 86 60 00        MOV        EDX,dword ptr [0x006086d4]
0051f7ef     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051f7f5     8B 42 08                 MOV        EAX,dword ptr [EDX + 0x8]
0051f7f8     89 41 1C                 MOV        dword ptr [ECX + 0x1c],EAX
0051f7fb     A1 DC 86 60 00           MOV        EAX,[0x006086dc]
0051f800     0F BF 78 02              MOVSX      EDI,word ptr [EAX + 0x2]
0051f804     0F BF 58 04              MOVSX      EBX,word ptr [EAX + 0x4]
0051f808     0F BF 28                 MOVSX      EBP,word ptr [EAX]
0051f80b     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051f810     8B CD                    MOV        ECX,EBP
0051f812     0F BF 10                 MOVSX      EDX,word ptr [EAX]
0051f815     0F AF CA                 IMUL       ECX,EDX
0051f818     0F BF 50 04              MOVSX      EDX,word ptr [EAX + 0x4]
0051f81c     0F AF D3                 IMUL       EDX,EBX
0051f81f     03 CA                    ADD        ECX,EDX
0051f821     0F BF 50 02              MOVSX      EDX,word ptr [EAX + 0x2]
0051f825     0F AF D7                 IMUL       EDX,EDI
0051f828     0F BF 70 06              MOVSX      ESI,word ptr [EAX + 0x6]
0051f82c     03 CA                    ADD        ECX,EDX
0051f82e     0F AF F5                 IMUL       ESI,EBP
0051f831     0F BF 50 0A              MOVSX      EDX,word ptr [EAX + 0xa]
0051f835     0F AF D3                 IMUL       EDX,EBX
0051f838     03 D6                    ADD        EDX,ESI
0051f83a     0F BF 70 08              MOVSX      ESI,word ptr [EAX + 0x8]
0051f83e     0F AF F7                 IMUL       ESI,EDI
0051f841     03 D6                    ADD        EDX,ESI
0051f843     0F BF 70 10              MOVSX      ESI,word ptr [EAX + 0x10]
0051f847     0F AF F3                 IMUL       ESI,EBX
0051f84a     0F BF 58 0E              MOVSX      EBX,word ptr [EAX + 0xe]
0051f84e     0F AF DF                 IMUL       EBX,EDI
0051f851     0F BF 78 0C              MOVSX      EDI,word ptr [EAX + 0xc]
0051f855     0F AF FD                 IMUL       EDI,EBP
0051f858     03 F3                    ADD        ESI,EBX
0051f85a     03 F7                    ADD        ESI,EDI
0051f85c     C1 FE 0C                 SAR        ESI,0xc
0051f85f     66 03 70 1C              ADD        SI,word ptr [EAX + 0x1c]
0051f863     C1 F9 0C                 SAR        ECX,0xc
0051f866     66 03 48 14              ADD        CX,word ptr [EAX + 0x14]
0051f86a     66 89 74 24 14           MOV        word ptr [ESP + 0x14],SI
0051f86f     C1 FA 0C                 SAR        EDX,0xc
0051f872     66 03 50 18              ADD        DX,word ptr [EAX + 0x18]
0051f876     0F BF C6                 MOVSX      EAX,SI
0051f879     8B 35 9C 87 60 00        MOV        ESI,dword ptr [0x0060879c]
0051f87f     0F BF D2                 MOVSX      EDX,DX
0051f882     89 06                    MOV        dword ptr [ESI],EAX
0051f884     8B 44 24 14              MOV        EAX,dword ptr [ESP + 0x14]
0051f888     8B 35 A4 87 60 00        MOV        ESI,dword ptr [0x006087a4]
0051f88e     25 FF FF 00 00           AND        EAX,0xffff
0051f893     0F BF 04 46              MOVSX      EAX,word ptr [ESI + EAX*0x2]
0051f897     8B 35 8C 87 60 00        MOV        ESI,dword ptr [0x0060878c]
0051f89d     0F AF D0                 IMUL       EDX,EAX
0051f8a0     8B 36                    MOV        ESI,dword ptr [ESI]
0051f8a2     81 E2 00 F0 FF FF        AND        EDX,0xfffff000
0051f8a8     C1 E6 0C                 SHL        ESI,0xc
0051f8ab     0F BF C9                 MOVSX      ECX,CX
0051f8ae     0F AF C8                 IMUL       ECX,EAX
0051f8b1     A1 EC 86 60 00           MOV        EAX,[0x006086ec]
0051f8b6     03 D6                    ADD        EDX,ESI
0051f8b8     C1 E2 04                 SHL        EDX,0x4
0051f8bb     8B 18                    MOV        EBX,dword ptr [EAX]
0051f8bd     C1 F9 0C                 SAR        ECX,0xc
0051f8c0     03 D1                    ADD        EDX,ECX
0051f8c2     8B 0D F0 86 60 00        MOV        ECX,dword ptr [0x006086f0]
0051f8c8     03 D3                    ADD        EDX,EBX
0051f8ca     89 11                    MOV        dword ptr [ECX],EDX
0051f8cc     8B 15 9C 87 60 00        MOV        EDX,dword ptr [0x0060879c]
0051f8d2     8B 0D F4 86 60 00        MOV        ECX,dword ptr [0x006086f4]
0051f8d8     8B 02                    MOV        EAX,dword ptr [EDX]
0051f8da     C1 F8 02                 SAR        EAX,0x2
0051f8dd     89 01                    MOV        dword ptr [ECX],EAX
0051f8df     8B 15 F0 86 60 00        MOV        EDX,dword ptr [0x006086f0]
0051f8e5     8B 44 24 20              MOV        EAX,dword ptr [ESP + 0x20]
0051f8e9     8B 0A                    MOV        ECX,dword ptr [EDX]
0051f8eb     89 08                    MOV        dword ptr [EAX],ECX
0051f8ed     8B 15 F4 86 60 00        MOV        EDX,dword ptr [0x006086f4]
0051f8f3     8B 0A                    MOV        ECX,dword ptr [EDX]
0051f8f5     89 48 04                 MOV        dword ptr [EAX + 0x4],ECX
0051f8f8     66 8B 10                 MOV        DX,word ptr [EAX]
0051f8fb     66 83 C2 18              ADD        DX,0x18
0051f8ff     66 81 FA 70 01           CMP        DX,0x170
0051f904     73 2B                    JNC        0x0051f931
0051f906     66 8B 48 02              MOV        CX,word ptr [EAX + 0x2]
0051f90a     66 83 C1 18              ADD        CX,0x18
0051f90e     66 81 F9 20 01           CMP        CX,0x120
0051f913     73 1C                    JNC        0x0051f931
0051f915     0F BF 50 04              MOVSX      EDX,word ptr [EAX + 0x4]
0051f919     83 EA 58                 SUB        EDX,0x58
0051f91c     81 FA A0 03 00 00        CMP        EDX,0x3a0
0051f922     73 0D                    JNC        0x0051f931
0051f924     B8 01 00 00 00           MOV        EAX,0x1
0051f929     5F                       POP        EDI
0051f92a     5E                       POP        ESI
0051f92b     5D                       POP        EBP
0051f92c     5B                       POP        EBX
0051f92d     83 C4 08                 ADD        ESP,0x8
0051f930     C3                       RET        
0051f931     5F                       POP        EDI
0051f932     5E                       POP        ESI
0051f933     5D                       POP        EBP
0051f934     33 C0                    XOR        EAX,EAX
0051f936     5B                       POP        EBX
0051f937     83 C4 08                 ADD        ESP,0x8
0051f93a     C3                       RET        

