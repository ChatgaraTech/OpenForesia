; Function: FUN_0051d1f0
; Entry:    0051d1f0
; Body:     [[0051d1f0, 0051e6f7]]

; --------------------------------------------------
; Range: 0051d1f0 - 0051e6f7
; --------------------------------------------------
0051d1f0     55                       PUSH       EBP
0051d1f1     8B EC                    MOV        EBP,ESP
0051d1f3     81 EC CC 00 00 00        SUB        ESP,0xcc
0051d1f9     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051d1ff     8B 15 E0 08 60 00        MOV        EDX,dword ptr [0x006008e0]
0051d205     33 C0                    XOR        EAX,EAX
0051d207     53                       PUSH       EBX
0051d208     89 45 CA                 MOV        dword ptr [EBP + -0x36],EAX
0051d20b     56                       PUSH       ESI
0051d20c     66 89 45 CE              MOV        word ptr [EBP + -0x32],AX
0051d210     89 11                    MOV        dword ptr [ECX],EDX
0051d212     A1 E4 08 60 00           MOV        EAX,[0x006008e4]
0051d217     8B 75 18                 MOV        ESI,dword ptr [EBP + 0x18]
0051d21a     89 41 04                 MOV        dword ptr [ECX + 0x4],EAX
0051d21d     8B 15 E8 08 60 00        MOV        EDX,dword ptr [0x006008e8]
0051d223     57                       PUSH       EDI
0051d224     89 51 08                 MOV        dword ptr [ECX + 0x8],EDX
0051d227     A1 EC 08 60 00           MOV        EAX,[0x006008ec]
0051d22c     89 41 0C                 MOV        dword ptr [ECX + 0xc],EAX
0051d22f     66 8B 15 F0 08 60 00     MOV        DX,word ptr [0x006008f0]
0051d236     66 89 51 10              MOV        word ptr [ECX + 0x10],DX
0051d23a     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051d23f     8B 0D F4 08 60 00        MOV        ECX,dword ptr [0x006008f4]
0051d245     89 48 14                 MOV        dword ptr [EAX + 0x14],ECX
0051d248     8B 15 10 88 60 00        MOV        EDX,dword ptr [0x00608810]
0051d24e     A1 F8 08 60 00           MOV        EAX,[0x006008f8]
0051d253     89 42 18                 MOV        dword ptr [EDX + 0x18],EAX
0051d256     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051d25c     8B 15 FC 08 60 00        MOV        EDX,dword ptr [0x006008fc]
0051d262     89 51 1C                 MOV        dword ptr [ECX + 0x1c],EDX
0051d265     8B 06                    MOV        EAX,dword ptr [ESI]
0051d267     8B 15 D0 08 60 00        MOV        EDX,dword ptr [0x006008d0]
0051d26d     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051d273     03 C2                    ADD        EAX,EDX
0051d275     C1 F8 0C                 SAR        EAX,0xc
0051d278     66 89 01                 MOV        word ptr [ECX],AX
0051d27b     8B 15 D4 08 60 00        MOV        EDX,dword ptr [0x006008d4]
0051d281     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
0051d284     2B D0                    SUB        EDX,EAX
0051d286     A1 DC 86 60 00           MOV        EAX,[0x006086dc]
0051d28b     C1 FA 0C                 SAR        EDX,0xc
0051d28e     66 89 50 02              MOV        word ptr [EAX + 0x2],DX
0051d292     8B 4E 08                 MOV        ECX,dword ptr [ESI + 0x8]
0051d295     8B 3D D8 08 60 00        MOV        EDI,dword ptr [0x006008d8]
0051d29b     8B 15 DC 86 60 00        MOV        EDX,dword ptr [0x006086dc]
0051d2a1     03 CF                    ADD        ECX,EDI
0051d2a3     C1 F9 0C                 SAR        ECX,0xc
0051d2a6     66 89 4A 04              MOV        word ptr [EDX + 0x4],CX
0051d2aa     66 8B 46 14              MOV        AX,word ptr [ESI + 0x14]
0051d2ae     66 03 05 28 49 60 00     ADD        AX,word ptr [0x00604928]
0051d2b5     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051d2bb     66 89 45 C8              MOV        word ptr [EBP + -0x38],AX
0051d2bf     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051d2c4     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051d2c8     0F BF 78 02              MOVSX      EDI,word ptr [EAX + 0x2]
0051d2cc     0F BF 58 04              MOVSX      EBX,word ptr [EAX + 0x4]
0051d2d0     0F AF D7                 IMUL       EDX,EDI
0051d2d3     0F BF 79 04              MOVSX      EDI,word ptr [ECX + 0x4]
0051d2d7     0F AF FB                 IMUL       EDI,EBX
0051d2da     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051d2dd     03 D7                    ADD        EDX,EDI
0051d2df     0F BF 38                 MOVSX      EDI,word ptr [EAX]
0051d2e2     0F AF F9                 IMUL       EDI,ECX
0051d2e5     8B 48 14                 MOV        ECX,dword ptr [EAX + 0x14]
0051d2e8     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051d2ed     03 D7                    ADD        EDX,EDI
0051d2ef     C1 FA 0C                 SAR        EDX,0xc
0051d2f2     03 D1                    ADD        EDX,ECX
0051d2f4     89 10                    MOV        dword ptr [EAX],EDX
0051d2f6     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051d2fc     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051d301     66 8B 11                 MOV        DX,word ptr [ECX]
0051d304     66 89 10                 MOV        word ptr [EAX],DX
0051d307     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051d30d     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051d312     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051d316     0F BF 78 08              MOVSX      EDI,word ptr [EAX + 0x8]
0051d31a     0F BF 58 0A              MOVSX      EBX,word ptr [EAX + 0xa]
0051d31e     0F AF D7                 IMUL       EDX,EDI
0051d321     0F BF 79 04              MOVSX      EDI,word ptr [ECX + 0x4]
0051d325     0F AF FB                 IMUL       EDI,EBX
0051d328     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051d32b     03 D7                    ADD        EDX,EDI
0051d32d     0F BF 78 06              MOVSX      EDI,word ptr [EAX + 0x6]
0051d331     0F AF F9                 IMUL       EDI,ECX
0051d334     8B 48 18                 MOV        ECX,dword ptr [EAX + 0x18]
0051d337     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051d33c     03 D7                    ADD        EDX,EDI
0051d33e     C1 FA 0C                 SAR        EDX,0xc
0051d341     03 D1                    ADD        EDX,ECX
0051d343     89 50 04                 MOV        dword ptr [EAX + 0x4],EDX
0051d346     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051d34c     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051d351     66 8B 51 04              MOV        DX,word ptr [ECX + 0x4]
0051d355     66 89 50 02              MOV        word ptr [EAX + 0x2],DX
0051d359     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051d35f     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051d364     0F BF 51 02              MOVSX      EDX,word ptr [ECX + 0x2]
0051d368     0F BF 78 0E              MOVSX      EDI,word ptr [EAX + 0xe]
0051d36c     0F BF 58 10              MOVSX      EBX,word ptr [EAX + 0x10]
0051d370     0F AF D7                 IMUL       EDX,EDI
0051d373     0F BF 79 04              MOVSX      EDI,word ptr [ECX + 0x4]
0051d377     0F AF FB                 IMUL       EDI,EBX
0051d37a     0F BF 09                 MOVSX      ECX,word ptr [ECX]
0051d37d     03 D7                    ADD        EDX,EDI
0051d37f     0F BF 78 0C              MOVSX      EDI,word ptr [EAX + 0xc]
0051d383     0F AF F9                 IMUL       EDI,ECX
0051d386     03 D7                    ADD        EDX,EDI
0051d388     8B 78 1C                 MOV        EDI,dword ptr [EAX + 0x1c]
0051d38b     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051d390     C1 FA 0C                 SAR        EDX,0xc
0051d393     03 D7                    ADD        EDX,EDI
0051d395     89 50 08                 MOV        dword ptr [EAX + 0x8],EDX
0051d398     8B 0D D4 86 60 00        MOV        ECX,dword ptr [0x006086d4]
0051d39e     A1 D8 86 60 00           MOV        EAX,[0x006086d8]
0051d3a3     66 8B 51 08              MOV        DX,word ptr [ECX + 0x8]
0051d3a7     66 89 50 04              MOV        word ptr [EAX + 0x4],DX
0051d3ab     66 8B 4E 16              MOV        CX,word ptr [ESI + 0x16]
0051d3af     66 8B 56 18              MOV        DX,word ptr [ESI + 0x18]
0051d3b3     66 03 0D 2A 49 60 00     ADD        CX,word ptr [0x0060492a]
0051d3ba     66 03 15 2C 49 60 00     ADD        DX,word ptr [0x0060492c]
0051d3c1     A1 D4 86 60 00           MOV        EAX,[0x006086d4]
0051d3c6     66 89 4D CA              MOV        word ptr [EBP + -0x36],CX
0051d3ca     66 89 55 CC              MOV        word ptr [EBP + -0x34],DX
0051d3ce     8B 08                    MOV        ECX,dword ptr [EAX]
0051d3d0     89 8D 48 FF FF FF        MOV        dword ptr [EBP + 0xffffff48],ECX
0051d3d6     8B 50 04                 MOV        EDX,dword ptr [EAX + 0x4]
0051d3d9     89 95 4C FF FF FF        MOV        dword ptr [EBP + 0xffffff4c],EDX
0051d3df     8B 40 08                 MOV        EAX,dword ptr [EAX + 0x8]
0051d3e2     8D 8D 34 FF FF FF        LEA        ECX,[EBP + 0xffffff34]
0051d3e8     8D 55 C8                 LEA        EDX,[EBP + -0x38]
0051d3eb     51                       PUSH       ECX
0051d3ec     52                       PUSH       EDX
0051d3ed     89 85 50 FF FF FF        MOV        dword ptr [EBP + 0xffffff50],EAX
0051d3f3     E8 08 EF FF FF           CALL       0x0051c300
0051d3f8     83 C4 08                 ADD        ESP,0x8
0051d3fb     83 C6 0C                 ADD        ESI,0xc
0051d3fe     8D 85 34 FF FF FF        LEA        EAX,[EBP + 0xffffff34]
0051d404     56                       PUSH       ESI
0051d405     50                       PUSH       EAX
0051d406     E8 A5 53 01 00           CALL       0x005327b0
0051d40b     83 C4 08                 ADD        ESP,0x8
0051d40e     8D 4D 9C                 LEA        ECX,[EBP + -0x64]
0051d411     51                       PUSH       ECX
0051d412     68 08 93 5E 00           PUSH       0x5e9308
0051d417     E8 E4 EE FF FF           CALL       0x0051c300
0051d41c     8B 45 10                 MOV        EAX,dword ptr [EBP + 0x10]
0051d41f     33 C9                    XOR        ECX,ECX
0051d421     83 C4 08                 ADD        ESP,0x8
0051d424     8B 50 20                 MOV        EDX,dword ptr [EAX + 0x20]
0051d427     66 8B 48 12              MOV        CX,word ptr [EAX + 0x12]
0051d42b     89 55 F4                 MOV        dword ptr [EBP + -0xc],EDX
0051d42e     8B 55 1C                 MOV        EDX,dword ptr [EBP + 0x1c]
0051d431     8B 78 24                 MOV        EDI,dword ptr [EAX + 0x24]
0051d434     0F AF D1                 IMUL       EDX,ECX
0051d437     8B 40 28                 MOV        EAX,dword ptr [EAX + 0x28]
0051d43a     89 4D C4                 MOV        dword ptr [EBP + -0x3c],ECX
0051d43d     89 7D B4                 MOV        dword ptr [EBP + -0x4c],EDI
0051d440     8D 0C 50                 LEA        ECX,[EAX + EDX*0x2]
0051d443     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0051d449     8B 45 14                 MOV        EAX,dword ptr [EBP + 0x14]
0051d44c     89 4D F8                 MOV        dword ptr [EBP + -0x8],ECX
0051d44f     8B 1A                    MOV        EBX,dword ptr [EDX]
0051d451     8B 00                    MOV        EAX,dword ptr [EAX]
0051d453     81 C3 00 A0 1B 00        ADD        EBX,0x1ba000
0051d459     33 C9                    XOR        ECX,ECX
0051d45b     8B 70 28                 MOV        ESI,dword ptr [EAX + 0x28]
0051d45e     66 8B 48 14              MOV        CX,word ptr [EAX + 0x14]
0051d462     8B D1                    MOV        EDX,ECX
0051d464     33 C9                    XOR        ECX,ECX
0051d466     66 8B 48 16              MOV        CX,word ptr [EAX + 0x16]
0051d46a     03 D1                    ADD        EDX,ECX
0051d46c     33 C9                    XOR        ECX,ECX
0051d46e     66 8B 48 10              MOV        CX,word ptr [EAX + 0x10]
0051d472     03 D1                    ADD        EDX,ECX
0051d474     33 C9                    XOR        ECX,ECX
0051d476     66 8B 48 12              MOV        CX,word ptr [EAX + 0x12]
0051d47a     8B 40 3C                 MOV        EAX,dword ptr [EAX + 0x3c]
0051d47d     03 D1                    ADD        EDX,ECX
0051d47f     33 C9                    XOR        ECX,ECX
0051d481     C1 E2 04                 SHL        EDX,0x4
0051d484     66 8B 4C 32 08           MOV        CX,word ptr [EDX + ESI*0x1 + 0x8]
0051d489     8B 15 48 4A 56 00        MOV        EDX,dword ptr [0x00564a48]
0051d48f     85 D2                    TEST       EDX,EDX
0051d491     74 1C                    JZ         0x0051d4af
0051d493     F6 C4 02                 TEST       AH,0x2
0051d496     74 17                    JZ         0x0051d4af
0051d498     8B 15 4C 4A 56 00        MOV        EDX,dword ptr [0x00564a4c]
0051d49e     85 D2                    TEST       EDX,EDX
0051d4a0     75 0A                    JNZ        0x0051d4ac
0051d4a2     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051d4a5     5F                       POP        EDI
0051d4a6     5E                       POP        ESI
0051d4a7     5B                       POP        EBX
0051d4a8     8B E5                    MOV        ESP,EBP
0051d4aa     5D                       POP        EBP
0051d4ab     C3                       RET        
0051d4ac     80 E4 FD                 AND        AH,0xfd
0051d4af     8B D0                    MOV        EDX,EAX
0051d4b1     25 FF 00 00 00           AND        EAX,0xff
0051d4b6     C1 EA 10                 SHR        EDX,0x10
0051d4b9     0D C0 01 00 00           OR         EAX,0x1c0
0051d4be     81 E2 FF 00 00 00        AND        EDX,0xff
0051d4c4     81 E1 00 F8 FF FF        AND        ECX,0xfffff800
0051d4ca     8B 14 95 00 8F 5E 00     MOV        EDX,dword ptr [EDX*0x4 + 0x5e8f00]
0051d4d1     C1 E0 05                 SHL        EAX,0x5
0051d4d4     0B C8                    OR         ECX,EAX
0051d4d6     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051d4d9     0B CA                    OR         ECX,EDX
0051d4db     C1 E1 10                 SHL        ECX,0x10
0051d4de     89 4D B0                 MOV        dword ptr [EBP + -0x50],ECX
0051d4e1     66 8B C8                 MOV        CX,AX
0051d4e4     66 C1 F9 02              SAR        CX,0x2
0051d4e8     0F BF C9                 MOVSX      ECX,CX
0051d4eb     C1 EA 10                 SHR        EDX,0x10
0051d4ee     83 E1 C0                 AND        ECX,0xffffffc0
0051d4f1     25 FF 00 00 00           AND        EAX,0xff
0051d4f6     03 D1                    ADD        EDX,ECX
0051d4f8     03 D0                    ADD        EDX,EAX
0051d4fa     81 FA C0 7E 00 00        CMP        EDX,0x7ec0
0051d500     76 05                    JBE        0x0051d507
0051d502     BA C0 7E 00 00           MOV        EDX,0x7ec0
0051d507     8B 45 C4                 MOV        EAX,dword ptr [EBP + -0x3c]
0051d50a     C1 E2 10                 SHL        EDX,0x10
0051d50d     89 55 B8                 MOV        dword ptr [EBP + -0x48],EDX
0051d510     8D 95 34 FF FF FF        LEA        EDX,[EBP + 0xffffff34]
0051d516     85 C0                    TEST       EAX,EAX
0051d518     89 55 1C                 MOV        dword ptr [EBP + 0x1c],EDX
0051d51b     0F 84 A2 00 00 00        JZ         0x0051d5c3
0051d521     89 45 20                 MOV        dword ptr [EBP + 0x20],EAX
0051d524     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051d527     8B 4D F4                 MOV        ECX,dword ptr [EBP + -0xc]
0051d52a     33 F6                    XOR        ESI,ESI
0051d52c     66 8B 30                 MOV        SI,word ptr [EAX]
0051d52f     C1 E6 04                 SHL        ESI,0x4
0051d532     03 F1                    ADD        ESI,ECX
0051d534     8D 8D 7C FF FF FF        LEA        ECX,[EBP + 0xffffff7c]
0051d53a     51                       PUSH       ECX
0051d53b     56                       PUSH       ESI
0051d53c     E8 BF ED FF FF           CALL       0x0051c300
0051d541     66 8B 46 0E              MOV        AX,word ptr [ESI + 0xe]
0051d545     83 C4 08                 ADD        ESP,0x8
0051d548     66 89 45 C0              MOV        word ptr [EBP + -0x40],AX
0051d54c     66 89 45 BE              MOV        word ptr [EBP + -0x42],AX
0051d550     66 89 45 BC              MOV        word ptr [EBP + -0x44],AX
0051d554     8D 55 BC                 LEA        EDX,[EBP + -0x44]
0051d557     8D 85 7C FF FF FF        LEA        EAX,[EBP + 0xffffff7c]
0051d55d     52                       PUSH       EDX
0051d55e     50                       PUSH       EAX
0051d55f     E8 3E 86 02 00           CALL       0x00545ba2
0051d564     0F BF 4E 08              MOVSX      ECX,word ptr [ESI + 0x8]
0051d568     89 4D 90                 MOV        dword ptr [EBP + -0x70],ECX
0051d56b     8D 8D 7C FF FF FF        LEA        ECX,[EBP + 0xffffff7c]
0051d571     0F BF 56 0A              MOVSX      EDX,word ptr [ESI + 0xa]
0051d575     89 55 94                 MOV        dword ptr [EBP + -0x6c],EDX
0051d578     8B 55 1C                 MOV        EDX,dword ptr [EBP + 0x1c]
0051d57b     0F BF 46 0C              MOVSX      EAX,word ptr [ESI + 0xc]
0051d57f     53                       PUSH       EBX
0051d580     51                       PUSH       ECX
0051d581     52                       PUSH       EDX
0051d582     89 45 98                 MOV        dword ptr [EBP + -0x68],EAX
0051d585     E8 12 86 02 00           CALL       0x00545b9c
0051d58a     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051d58d     8B 0D B4 87 60 00        MOV        ECX,dword ptr [0x006087b4]
0051d593     83 C0 02                 ADD        EAX,0x2
0051d596     83 C7 02                 ADD        EDI,0x2
0051d599     8B 11                    MOV        EDX,dword ptr [ECX]
0051d59b     89 45 F8                 MOV        dword ptr [EBP + -0x8],EAX
0051d59e     33 C0                    XOR        EAX,EAX
0051d5a0     83 C3 20                 ADD        EBX,0x20
0051d5a3     66 8B 07                 MOV        AX,word ptr [EDI]
0051d5a6     05 00 DD 00 00           ADD        EAX,0xdd00
0051d5ab     C1 E0 05                 SHL        EAX,0x5
0051d5ae     03 C2                    ADD        EAX,EDX
0051d5b0     89 45 1C                 MOV        dword ptr [EBP + 0x1c],EAX
0051d5b3     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051d5b6     48                       DEC        EAX
0051d5b7     89 45 20                 MOV        dword ptr [EBP + 0x20],EAX
0051d5ba     0F 85 64 FF FF FF        JNZ        0x0051d524
0051d5c0     89 7D B4                 MOV        dword ptr [EBP + -0x4c],EDI
0051d5c3     8B 55 18                 MOV        EDX,dword ptr [EBP + 0x18]
0051d5c6     33 C0                    XOR        EAX,EAX
0051d5c8     0F BF 4A 1A              MOVSX      ECX,word ptr [EDX + 0x1a]
0051d5cc     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051d5cf     89 8D 6C FF FF FF        MOV        dword ptr [EBP + 0xffffff6c],ECX
0051d5d5     66 8B 42 14              MOV        AX,word ptr [EDX + 0x14]
0051d5d9     85 C0                    TEST       EAX,EAX
0051d5db     89 45 C4                 MOV        dword ptr [EBP + -0x3c],EAX
0051d5de     0F 84 08 11 00 00        JZ         0x0051e6ec
0051d5e4     8B 5D 0C                 MOV        EBX,dword ptr [EBP + 0xc]
0051d5e7     8D 85 7C FF FF FF        LEA        EAX,[EBP + 0xffffff7c]
0051d5ed     8D 55 9C                 LEA        EDX,[EBP + -0x64]
0051d5f0     83 E1 01                 AND        ECX,0x1
0051d5f3     89 85 78 FF FF FF        MOV        dword ptr [EBP + 0xffffff78],EAX
0051d5f9     89 95 74 FF FF FF        MOV        dword ptr [EBP + 0xffffff74],EDX
0051d5ff     89 8D 70 FF FF FF        MOV        dword ptr [EBP + 0xffffff70],ECX
0051d605     EB 03                    JMP        0x0051d60a
0051d607     8B 7D B4                 MOV        EDI,dword ptr [EBP + -0x4c]
0051d60a     8B 0D B4 87 60 00        MOV        ECX,dword ptr [0x006087b4]
0051d610     33 C0                    XOR        EAX,EAX
0051d612     66 8B 07                 MOV        AX,word ptr [EDI]
0051d615     8B 11                    MOV        EDX,dword ptr [ECX]
0051d617     05 00 DD 00 00           ADD        EAX,0xdd00
0051d61c     C1 E0 05                 SHL        EAX,0x5
0051d61f     03 C2                    ADD        EAX,EDX
0051d621     8B 15 C4 86 60 00        MOV        EDX,dword ptr [0x006086c4]
0051d627     89 45 D0                 MOV        dword ptr [EBP + -0x30],EAX
0051d62a     83 3A 00                 CMP        dword ptr [EDX],0x0
0051d62d     0F 85 9B 01 00 00        JNZ        0x0051d7ce
0051d633     0F BF 75 9C              MOVSX      ESI,word ptr [EBP + -0x64]
0051d637     0F BF 4D 9E              MOVSX      ECX,word ptr [EBP + -0x62]
0051d63b     0F BF 55 A0              MOVSX      EDX,word ptr [EBP + -0x60]
0051d63f     89 75 0C                 MOV        dword ptr [EBP + 0xc],ESI
0051d642     8B F9                    MOV        EDI,ECX
0051d644     0F BF 70 06              MOVSX      ESI,word ptr [EAX + 0x6]
0051d648     0F AF FE                 IMUL       EDI,ESI
0051d64b     0F BF 70 0C              MOVSX      ESI,word ptr [EAX + 0xc]
0051d64f     89 55 20                 MOV        dword ptr [EBP + 0x20],EDX
0051d652     0F AF D6                 IMUL       EDX,ESI
0051d655     03 FA                    ADD        EDI,EDX
0051d657     0F BF 10                 MOVSX      EDX,word ptr [EAX]
0051d65a     0F AF 55 0C              IMUL       EDX,dword ptr [EBP + 0xc]
0051d65e     03 FA                    ADD        EDI,EDX
0051d660     0F BF 75 A6              MOVSX      ESI,word ptr [EBP + -0x5a]
0051d664     C1 FF 0C                 SAR        EDI,0xc
0051d667     66 89 BD 7C FF FF FF     MOV        word ptr [EBP + 0xffffff7c],DI
0051d66e     89 75 F0                 MOV        dword ptr [EBP + -0x10],ESI
0051d671     0F BF 7D A2              MOVSX      EDI,word ptr [EBP + -0x5e]
0051d675     0F BF 55 A4              MOVSX      EDX,word ptr [EBP + -0x5c]
0051d679     89 7D 10                 MOV        dword ptr [EBP + 0x10],EDI
0051d67c     89 55 F4                 MOV        dword ptr [EBP + -0xc],EDX
0051d67f     0F BF 78 0C              MOVSX      EDI,word ptr [EAX + 0xc]
0051d683     0F AF F7                 IMUL       ESI,EDI
0051d686     0F BF 78 06              MOVSX      EDI,word ptr [EAX + 0x6]
0051d68a     0F AF D7                 IMUL       EDX,EDI
0051d68d     03 F2                    ADD        ESI,EDX
0051d68f     0F BF 10                 MOVSX      EDX,word ptr [EAX]
0051d692     0F AF 55 10              IMUL       EDX,dword ptr [EBP + 0x10]
0051d696     0F BF 7D A8              MOVSX      EDI,word ptr [EBP + -0x58]
0051d69a     03 F2                    ADD        ESI,EDX
0051d69c     89 7D 1C                 MOV        dword ptr [EBP + 0x1c],EDI
0051d69f     C1 FE 0C                 SAR        ESI,0xc
0051d6a2     0F BF 55 AA              MOVSX      EDX,word ptr [EBP + -0x56]
0051d6a6     66 89 75 82              MOV        word ptr [EBP + -0x7e],SI
0051d6aa     89 55 D4                 MOV        dword ptr [EBP + -0x2c],EDX
0051d6ad     0F BF 75 AC              MOVSX      ESI,word ptr [EBP + -0x54]
0051d6b1     0F BF 78 0C              MOVSX      EDI,word ptr [EAX + 0xc]
0051d6b5     89 75 DC                 MOV        dword ptr [EBP + -0x24],ESI
0051d6b8     0F AF F7                 IMUL       ESI,EDI
0051d6bb     0F BF 78 06              MOVSX      EDI,word ptr [EAX + 0x6]
0051d6bf     0F AF D7                 IMUL       EDX,EDI
0051d6c2     03 F2                    ADD        ESI,EDX
0051d6c4     8B F9                    MOV        EDI,ECX
0051d6c6     0F BF 10                 MOVSX      EDX,word ptr [EAX]
0051d6c9     0F AF 55 1C              IMUL       EDX,dword ptr [EBP + 0x1c]
0051d6cd     03 F2                    ADD        ESI,EDX
0051d6cf     C1 FE 0C                 SAR        ESI,0xc
0051d6d2     66 89 75 88              MOV        word ptr [EBP + -0x78],SI
0051d6d6     8B 75 20                 MOV        ESI,dword ptr [EBP + 0x20]
0051d6d9     0F BF 50 0E              MOVSX      EDX,word ptr [EAX + 0xe]
0051d6dd     0F AF F2                 IMUL       ESI,EDX
0051d6e0     0F BF 50 08              MOVSX      EDX,word ptr [EAX + 0x8]
0051d6e4     0F AF FA                 IMUL       EDI,EDX
0051d6e7     0F BF 50 02              MOVSX      EDX,word ptr [EAX + 0x2]
0051d6eb     03 F7                    ADD        ESI,EDI
0051d6ed     8B 7D 0C                 MOV        EDI,dword ptr [EBP + 0xc]
0051d6f0     0F AF FA                 IMUL       EDI,EDX
0051d6f3     03 F7                    ADD        ESI,EDI
0051d6f5     8B 7D 10                 MOV        EDI,dword ptr [EBP + 0x10]
0051d6f8     C1 FE 0C                 SAR        ESI,0xc
0051d6fb     66 89 B5 7E FF FF FF     MOV        word ptr [EBP + 0xffffff7e],SI
0051d702     8B 75 F4                 MOV        ESI,dword ptr [EBP + -0xc]
0051d705     0F BF 50 08              MOVSX      EDX,word ptr [EAX + 0x8]
0051d709     0F AF F2                 IMUL       ESI,EDX
0051d70c     0F BF 50 02              MOVSX      EDX,word ptr [EAX + 0x2]
0051d710     0F AF FA                 IMUL       EDI,EDX
0051d713     0F BF 50 0E              MOVSX      EDX,word ptr [EAX + 0xe]
0051d717     03 F7                    ADD        ESI,EDI
0051d719     8B 7D F0                 MOV        EDI,dword ptr [EBP + -0x10]
0051d71c     0F AF FA                 IMUL       EDI,EDX
0051d71f     03 F7                    ADD        ESI,EDI
0051d721     8B 7D DC                 MOV        EDI,dword ptr [EBP + -0x24]
0051d724     C1 FE 0C                 SAR        ESI,0xc
0051d727     66 89 75 84              MOV        word ptr [EBP + -0x7c],SI
0051d72b     8B 75 D4                 MOV        ESI,dword ptr [EBP + -0x2c]
0051d72e     0F BF 50 08              MOVSX      EDX,word ptr [EAX + 0x8]
0051d732     0F AF F2                 IMUL       ESI,EDX
0051d735     0F BF 50 0E              MOVSX      EDX,word ptr [EAX + 0xe]
0051d739     0F AF FA                 IMUL       EDI,EDX
0051d73c     03 F7                    ADD        ESI,EDI
0051d73e     0F BF 50 02              MOVSX      EDX,word ptr [EAX + 0x2]
0051d742     8B 7D 1C                 MOV        EDI,dword ptr [EBP + 0x1c]
0051d745     0F AF FA                 IMUL       EDI,EDX
0051d748     03 F7                    ADD        ESI,EDI
0051d74a     C1 FE 0C                 SAR        ESI,0xc
0051d74d     66 89 75 8A              MOV        word ptr [EBP + -0x76],SI
0051d751     0F BF 50 0A              MOVSX      EDX,word ptr [EAX + 0xa]
0051d755     0F BF 70 04              MOVSX      ESI,word ptr [EAX + 0x4]
0051d759     0F AF CA                 IMUL       ECX,EDX
0051d75c     8B 55 0C                 MOV        EDX,dword ptr [EBP + 0xc]
0051d75f     0F AF D6                 IMUL       EDX,ESI
0051d762     0F BF 70 10              MOVSX      ESI,word ptr [EAX + 0x10]
0051d766     03 CA                    ADD        ECX,EDX
0051d768     8B 55 20                 MOV        EDX,dword ptr [EBP + 0x20]
0051d76b     0F AF D6                 IMUL       EDX,ESI
0051d76e     03 CA                    ADD        ECX,EDX
0051d770     C1 F9 0C                 SAR        ECX,0xc
0051d773     66 89 4D 80              MOV        word ptr [EBP + -0x80],CX
0051d777     8B 4D F4                 MOV        ECX,dword ptr [EBP + -0xc]
0051d77a     0F BF 50 0A              MOVSX      EDX,word ptr [EAX + 0xa]
0051d77e     0F BF 70 04              MOVSX      ESI,word ptr [EAX + 0x4]
0051d782     0F AF CA                 IMUL       ECX,EDX
0051d785     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051d788     0F AF D6                 IMUL       EDX,ESI
0051d78b     0F BF 70 10              MOVSX      ESI,word ptr [EAX + 0x10]
0051d78f     03 CA                    ADD        ECX,EDX
0051d791     8B 55 F0                 MOV        EDX,dword ptr [EBP + -0x10]
0051d794     0F AF D6                 IMUL       EDX,ESI
0051d797     03 CA                    ADD        ECX,EDX
0051d799     C1 F9 0C                 SAR        ECX,0xc
0051d79c     66 89 4D 86              MOV        word ptr [EBP + -0x7a],CX
0051d7a0     8B 4D DC                 MOV        ECX,dword ptr [EBP + -0x24]
0051d7a3     0F BF 50 10              MOVSX      EDX,word ptr [EAX + 0x10]
0051d7a7     0F BF 70 04              MOVSX      ESI,word ptr [EAX + 0x4]
0051d7ab     0F AF CA                 IMUL       ECX,EDX
0051d7ae     8B 55 1C                 MOV        EDX,dword ptr [EBP + 0x1c]
0051d7b1     0F AF D6                 IMUL       EDX,ESI
0051d7b4     03 CA                    ADD        ECX,EDX
0051d7b6     0F BF 50 0A              MOVSX      EDX,word ptr [EAX + 0xa]
0051d7ba     8B 45 D4                 MOV        EAX,dword ptr [EBP + -0x2c]
0051d7bd     0F AF C2                 IMUL       EAX,EDX
0051d7c0     03 C8                    ADD        ECX,EAX
0051d7c2     C1 F9 0C                 SAR        ECX,0xc
0051d7c5     66 89 4D 8C              MOV        word ptr [EBP + -0x74],CX
0051d7c9     E9 BB 01 00 00           JMP        0x0051d989
0051d7ce     B8 FF FF 00 00           MOV        EAX,0xffff
0051d7d3     C7 85 5C FF FF FF FF FF FF FF MOV        dword ptr [EBP + 0xffffff5c],0xffffffff
0051d7dd     89 85 60 FF FF FF        MOV        dword ptr [EBP + 0xffffff60],EAX
0051d7e3     89 85 64 FF FF FF        MOV        dword ptr [EBP + 0xffffff64],EAX
0051d7e9     C7 85 68 FF FF FF 00 00 00 00 MOV        dword ptr [EBP + 0xffffff68],0x0
0051d7f3     0F 6F A5 5C FF FF FF     MOVQ       MM4,qword ptr [EBP + 0xffffff5c]
0051d7fa     8B B5 74 FF FF FF        MOV        ESI,dword ptr [EBP + 0xffffff74]
0051d800     0F 73 F3 40              PSLLQ      MM3,0x40
0051d804     0F 6F 06                 MOVQ       MM0,qword ptr [ESI]
0051d807     8B 45 D0                 MOV        EAX,dword ptr [EBP + -0x30]
0051d80a     0F 6F 4E 06              MOVQ       MM1,qword ptr [ESI + 0x6]
0051d80e     0F DB C4                 PAND       MM0,MM4
0051d811     8B BD 78 FF FF FF        MOV        EDI,dword ptr [EBP + 0xffffff78]
0051d817     0F 6F 56 0C              MOVQ       MM2,qword ptr [ESI + 0xc]
0051d81b     0F 6E 68 06              MOVD       MM5,dword ptr [EAX + 0x6]
0051d81f     0F 6F B5 64 FF FF FF     MOVQ       MM6,qword ptr [EBP + 0xffffff64]
0051d826     0F DB CC                 PAND       MM1,MM4
0051d829     0F 6E 18                 MOVD       MM3,dword ptr [EAX]
0051d82c     0F 72 F5 10              PSLLD      MM5,0x10
0051d830     0F DB DE                 PAND       MM3,MM6
0051d833     0F 6E 78 0C              MOVD       MM7,dword ptr [EAX + 0xc]
0051d837     0F EB DD                 POR        MM3,MM5
0051d83a     0F DB FE                 PAND       MM7,MM6
0051d83d     0F DB D4                 PAND       MM2,MM4
0051d840     0F 73 F7 20              PSLLQ      MM7,0x20
0051d844     0F 7F DC                 MOVQ       MM4,MM3
0051d847     0F EB DF                 POR        MM3,MM7
0051d84a     0F F5 D8                 PMADDWD    MM3,MM0
0051d84d     0F EB E7                 POR        MM4,MM7
0051d850     0F 7F DF                 MOVQ       MM7,MM3
0051d853     0F 73 D3 20              PSRLQ      MM3,0x20
0051d857     0F 7F E5                 MOVQ       MM5,MM4
0051d85a     0F FE FB                 PADDD      MM7,MM3
0051d85d     0F F5 E9                 PMADDWD    MM5,MM1
0051d860     0F 72 E7 0C              PSRAD      MM7,0xc
0051d864     0F 7F EE                 MOVQ       MM6,MM5
0051d867     0F F5 E2                 PMADDWD    MM4,MM2
0051d86a     0F 73 D5 20              PSRLQ      MM5,0x20
0051d86e     0F 7E FA                 MOVD       EDX,MM7
0051d871     0F FE F5                 PADDD      MM6,MM5
0051d874     66 89 17                 MOV        word ptr [EDI],DX
0051d877     0F 72 E6 0C              PSRAD      MM6,0xc
0051d87b     0F 7F E7                 MOVQ       MM7,MM4
0051d87e     0F 73 D4 20              PSRLQ      MM4,0x20
0051d882     0F 7E F1                 MOVD       ECX,MM6
0051d885     0F FE FC                 PADDD      MM7,MM4
0051d888     66 89 4F 06              MOV        word ptr [EDI + 0x6],CX
0051d88c     0F 72 E7 0C              PSRAD      MM7,0xc
0051d890     0F 7E FA                 MOVD       EDX,MM7
0051d893     66 89 57 0C              MOV        word ptr [EDI + 0xc],DX
0051d897     0F 6E 68 08              MOVD       MM5,dword ptr [EAX + 0x8]
0051d89b     0F 6F B5 64 FF FF FF     MOVQ       MM6,qword ptr [EBP + 0xffffff64]
0051d8a2     0F 6E 58 02              MOVD       MM3,dword ptr [EAX + 0x2]
0051d8a6     0F 72 F5 10              PSLLD      MM5,0x10
0051d8aa     0F DB DE                 PAND       MM3,MM6
0051d8ad     0F 6E 78 0E              MOVD       MM7,dword ptr [EAX + 0xe]
0051d8b1     0F EB DD                 POR        MM3,MM5
0051d8b4     0F DB FE                 PAND       MM7,MM6
0051d8b7     0F 73 F7 20              PSLLQ      MM7,0x20
0051d8bb     0F 7F DC                 MOVQ       MM4,MM3
0051d8be     0F EB DF                 POR        MM3,MM7
0051d8c1     0F F5 D8                 PMADDWD    MM3,MM0
0051d8c4     0F EB E7                 POR        MM4,MM7
0051d8c7     0F 7F DF                 MOVQ       MM7,MM3
0051d8ca     0F 73 D3 20              PSRLQ      MM3,0x20
0051d8ce     0F 7F E5                 MOVQ       MM5,MM4
0051d8d1     0F FE FB                 PADDD      MM7,MM3
0051d8d4     0F F5 E9                 PMADDWD    MM5,MM1
0051d8d7     0F 72 E7 0C              PSRAD      MM7,0xc
0051d8db     0F 7F EE                 MOVQ       MM6,MM5
0051d8de     0F F5 E2                 PMADDWD    MM4,MM2
0051d8e1     0F 73 D5 20              PSRLQ      MM5,0x20
0051d8e5     0F 7E FA                 MOVD       EDX,MM7
0051d8e8     0F FE F5                 PADDD      MM6,MM5
0051d8eb     66 89 57 02              MOV        word ptr [EDI + 0x2],DX
0051d8ef     0F 72 E6 0C              PSRAD      MM6,0xc
0051d8f3     0F 7F E7                 MOVQ       MM7,MM4
0051d8f6     0F 73 D4 20              PSRLQ      MM4,0x20
0051d8fa     0F 7E F1                 MOVD       ECX,MM6
0051d8fd     0F FE FC                 PADDD      MM7,MM4
0051d900     66 89 4F 08              MOV        word ptr [EDI + 0x8],CX
0051d904     0F 72 E7 0C              PSRAD      MM7,0xc
0051d908     0F 7E FA                 MOVD       EDX,MM7
0051d90b     66 89 57 0E              MOV        word ptr [EDI + 0xe],DX
0051d90f     0F 6E 68 0A              MOVD       MM5,dword ptr [EAX + 0xa]
0051d913     0F 6F B5 64 FF FF FF     MOVQ       MM6,qword ptr [EBP + 0xffffff64]
0051d91a     0F 6E 58 04              MOVD       MM3,dword ptr [EAX + 0x4]
0051d91e     0F 72 F5 10              PSLLD      MM5,0x10
0051d922     0F DB DE                 PAND       MM3,MM6
0051d925     0F 6E 78 10              MOVD       MM7,dword ptr [EAX + 0x10]
0051d929     0F EB DD                 POR        MM3,MM5
0051d92c     0F DB FE                 PAND       MM7,MM6
0051d92f     0F 73 F7 20              PSLLQ      MM7,0x20
0051d933     0F 7F DC                 MOVQ       MM4,MM3
0051d936     0F EB DF                 POR        MM3,MM7
0051d939     0F F5 D8                 PMADDWD    MM3,MM0
0051d93c     0F EB E7                 POR        MM4,MM7
0051d93f     0F 7F DF                 MOVQ       MM7,MM3
0051d942     0F 73 D3 20              PSRLQ      MM3,0x20
0051d946     0F 7F E5                 MOVQ       MM5,MM4
0051d949     0F FE FB                 PADDD      MM7,MM3
0051d94c     0F F5 E9                 PMADDWD    MM5,MM1
0051d94f     0F 72 E7 0C              PSRAD      MM7,0xc
0051d953     0F 7F EE                 MOVQ       MM6,MM5
0051d956     0F F5 E2                 PMADDWD    MM4,MM2
0051d959     0F 73 D5 20              PSRLQ      MM5,0x20
0051d95d     0F 7E FA                 MOVD       EDX,MM7
0051d960     0F FE F5                 PADDD      MM6,MM5
0051d963     66 89 57 04              MOV        word ptr [EDI + 0x4],DX
0051d967     0F 72 E6 0C              PSRAD      MM6,0xc
0051d96b     0F 7F E7                 MOVQ       MM7,MM4
0051d96e     0F 73 D4 20              PSRLQ      MM4,0x20
0051d972     0F 7E F1                 MOVD       ECX,MM6
0051d975     0F FE FC                 PADDD      MM7,MM4
0051d978     66 89 4F 0A              MOV        word ptr [EDI + 0xa],CX
0051d97c     0F 72 E7 0C              PSRAD      MM7,0xc
0051d980     0F 7E FA                 MOVD       EDX,MM7
0051d983     66 89 57 10              MOV        word ptr [EDI + 0x10],DX
0051d987     0F 77                    EMMS       
0051d989     8B 3D F8 86 60 00        MOV        EDI,dword ptr [0x006086f8]
0051d98f     8B 45 D0                 MOV        EAX,dword ptr [EBP + -0x30]
0051d992     B9 08 00 00 00           MOV        ECX,0x8
0051d997     8D B5 7C FF FF FF        LEA        ESI,[EBP + 0xffffff7c]
0051d99d     F3 A5                    MOVSD.REP  ES:EDI,ESI
0051d99f     8B 15 10 88 60 00        MOV        EDX,dword ptr [0x00608810]
0051d9a5     8B C8                    MOV        ECX,EAX
0051d9a7     8B 31                    MOV        ESI,dword ptr [ECX]
0051d9a9     89 32                    MOV        dword ptr [EDX],ESI
0051d9ab     8B 71 04                 MOV        ESI,dword ptr [ECX + 0x4]
0051d9ae     89 72 04                 MOV        dword ptr [EDX + 0x4],ESI
0051d9b1     8B 71 08                 MOV        ESI,dword ptr [ECX + 0x8]
0051d9b4     89 72 08                 MOV        dword ptr [EDX + 0x8],ESI
0051d9b7     8B 71 0C                 MOV        ESI,dword ptr [ECX + 0xc]
0051d9ba     89 72 0C                 MOV        dword ptr [EDX + 0xc],ESI
0051d9bd     66 8B 49 10              MOV        CX,word ptr [ECX + 0x10]
0051d9c1     66 89 4A 10              MOV        word ptr [EDX + 0x10],CX
0051d9c5     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051d9cb     8B 50 14                 MOV        EDX,dword ptr [EAX + 0x14]
0051d9ce     89 51 14                 MOV        dword ptr [ECX + 0x14],EDX
0051d9d1     8B 0D 10 88 60 00        MOV        ECX,dword ptr [0x00608810]
0051d9d7     8B 50 18                 MOV        EDX,dword ptr [EAX + 0x18]
0051d9da     89 51 18                 MOV        dword ptr [ECX + 0x18],EDX
0051d9dd     8B 50 1C                 MOV        EDX,dword ptr [EAX + 0x1c]
0051d9e0     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051d9e5     8B 8D 70 FF FF FF        MOV        ECX,dword ptr [EBP + 0xffffff70]
0051d9eb     89 50 1C                 MOV        dword ptr [EAX + 0x1c],EDX
0051d9ee     33 C0                    XOR        EAX,EAX
0051d9f0     3B C8                    CMP        ECX,EAX
0051d9f2     0F 84 69 01 00 00        JZ         0x0051db61
0051d9f8     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051d9fe     66 89 01                 MOV        word ptr [ECX],AX
0051da01     8B 15 DC 86 60 00        MOV        EDX,dword ptr [0x006086dc]
0051da07     66 89 42 02              MOV        word ptr [EDX + 0x2],AX
0051da0b     8B 0D DC 86 60 00        MOV        ECX,dword ptr [0x006086dc]
0051da11     66 89 41 04              MOV        word ptr [ECX + 0x4],AX
0051da15     A1 DC 86 60 00           MOV        EAX,[0x006086dc]
0051da1a     0F BF 50 04              MOVSX      EDX,word ptr [EAX + 0x4]
0051da1e     0F BF 70 02              MOVSX      ESI,word ptr [EAX + 0x2]
0051da22     89 55 1C                 MOV        dword ptr [EBP + 0x1c],EDX
0051da25     89 75 0C                 MOV        dword ptr [EBP + 0xc],ESI
0051da28     0F BF 10                 MOVSX      EDX,word ptr [EAX]
0051da2b     A1 10 88 60 00           MOV        EAX,[0x00608810]
0051da30     89 55 10                 MOV        dword ptr [EBP + 0x10],EDX
0051da33     0F BF 48 02              MOVSX      ECX,word ptr [EAX + 0x2]
0051da37     0F BF 38                 MOVSX      EDI,word ptr [EAX]
0051da3a     0F AF CE                 IMUL       ECX,ESI
0051da3d     0F AF D7                 IMUL       EDX,EDI
0051da40     8B 7D 1C                 MOV        EDI,dword ptr [EBP + 0x1c]
0051da43     03 CA                    ADD        ECX,EDX
0051da45     0F BF 50 04              MOVSX      EDX,word ptr [EAX + 0x4]
0051da49     0F AF D7                 IMUL       EDX,EDI
0051da4c     03 CA                    ADD        ECX,EDX
0051da4e     0F BF 50 08              MOVSX      EDX,word ptr [EAX + 0x8]
0051da52     0F AF D6                 IMUL       EDX,ESI
0051da55     0F BF 70 0A              MOVSX      ESI,word ptr [EAX + 0xa]
0051da59     0F AF F7                 IMUL       ESI,EDI
0051da5c     03 D6                    ADD        EDX,ESI
0051da5e     0F BF 70 06              MOVSX      ESI,word ptr [EAX + 0x6]
0051da62     0F AF 75 10              IMUL       ESI,dword ptr [EBP + 0x10]
0051da66     03 D6                    ADD        EDX,ESI
0051da68     0F BF 70 0E              MOVSX      ESI,word ptr [EAX + 0xe]
0051da6c     0F AF 75 0C              IMUL       ESI,dword ptr [EBP + 0xc]
0051da70     66 89 75 0C              MOV        word ptr [EBP + 0xc],SI
0051da74     0F BF 70 0C              MOVSX      ESI,word ptr [EAX + 0xc]
0051da78     0F AF 75 10              IMUL       ESI,dword ptr [EBP + 0x10]
0051da7c     8B 7D 0C                 MOV        EDI,dword ptr [EBP + 0xc]
0051da7f     03 FE                    ADD        EDI,ESI
0051da81     66 89 7D 0C              MOV        word ptr [EBP + 0xc],DI
0051da85     0F BF 78 10              MOVSX      EDI,word ptr [EAX + 0x10]
0051da89     0F AF 7D 1C              IMUL       EDI,dword ptr [EBP + 0x1c]
0051da8d     66 8B 75 0C              MOV        SI,word ptr [EBP + 0xc]
0051da91     03 F7                    ADD        ESI,EDI
0051da93     C1 FE 0C                 SAR        ESI,0xc
0051da96     66 03 70 1C              ADD        SI,word ptr [EAX + 0x1c]
0051da9a     C1 F9 0C                 SAR        ECX,0xc
0051da9d     66 03 48 14              ADD        CX,word ptr [EAX + 0x14]
0051daa1     66 89 B5 58 FF FF FF     MOV        word ptr [EBP + 0xffffff58],SI
0051daa8     C1 FA 0C                 SAR        EDX,0xc
0051daab     66 03 50 18              ADD        DX,word ptr [EAX + 0x18]
0051daaf     0F BF C6                 MOVSX      EAX,SI
0051dab2     8B 35 9C 87 60 00        MOV        ESI,dword ptr [0x0060879c]
0051dab8     0F BF D2                 MOVSX      EDX,DX
0051dabb     89 06                    MOV        dword ptr [ESI],EAX
0051dabd     8B 85 58 FF FF FF        MOV        EAX,dword ptr [EBP + 0xffffff58]
0051dac3     8B 35 A4 87 60 00        MOV        ESI,dword ptr [0x006087a4]
0051dac9     25 FF FF 00 00           AND        EAX,0xffff
0051dace     0F BF C9                 MOVSX      ECX,CX
0051dad1     0F BF 04 46              MOVSX      EAX,word ptr [ESI + EAX*0x2]
0051dad5     8B F0                    MOV        ESI,EAX
0051dad7     0F AF C1                 IMUL       EAX,ECX
0051dada     0F AF F2                 IMUL       ESI,EDX
0051dadd     8B 15 8C 87 60 00        MOV        EDX,dword ptr [0x0060878c]
0051dae3     81 E6 00 F0 FF FF        AND        ESI,0xfffff000
0051dae9     C1 F8 0C                 SAR        EAX,0xc
0051daec     8B 12                    MOV        EDX,dword ptr [EDX]
0051daee     C1 E2 0C                 SHL        EDX,0xc
0051daf1     03 F2                    ADD        ESI,EDX
0051daf3     8B 15 EC 86 60 00        MOV        EDX,dword ptr [0x006086ec]
0051daf9     C1 E6 04                 SHL        ESI,0x4
0051dafc     8B 3A                    MOV        EDI,dword ptr [EDX]
0051dafe     03 F0                    ADD        ESI,EAX
0051db00     A1 F0 86 60 00           MOV        EAX,[0x006086f0]
0051db05     03 F7                    ADD        ESI,EDI
0051db07     89 30                    MOV        dword ptr [EAX],ESI
0051db09     8B 0D 9C 87 60 00        MOV        ECX,dword ptr [0x0060879c]
0051db0f     A1 F4 86 60 00           MOV        EAX,[0x006086f4]
0051db14     8B 11                    MOV        EDX,dword ptr [ECX]
0051db16     C1 FA 02                 SAR        EDX,0x2
0051db19     89 10                    MOV        dword ptr [EAX],EDX
0051db1b     8B 0D F0 86 60 00        MOV        ECX,dword ptr [0x006086f0]
0051db21     8B 01                    MOV        EAX,dword ptr [ECX]
0051db23     8B 15 F4 86 60 00        MOV        EDX,dword ptr [0x006086f4]
0051db29     89 45 C8                 MOV        dword ptr [EBP + -0x38],EAX
0051db2c     83 C0 18                 ADD        EAX,0x18
0051db2f     8B 0A                    MOV        ECX,dword ptr [EDX]
0051db31     66 3D 70 01              CMP        AX,0x170
0051db35     89 4D CC                 MOV        dword ptr [EBP + -0x34],ECX
0051db38     0F 83 84 0B 00 00        JNC        0x0051e6c2
0051db3e     8B 55 CA                 MOV        EDX,dword ptr [EBP + -0x36]
0051db41     83 C2 18                 ADD        EDX,0x18
0051db44     66 81 FA 20 01           CMP        DX,0x120
0051db49     0F 83 73 0B 00 00        JNC        0x0051e6c2
0051db4f     0F BF 45 CC              MOVSX      EAX,word ptr [EBP + -0x34]
0051db53     83 E8 58                 SUB        EAX,0x58
0051db56     3D A0 03 00 00           CMP        EAX,0x3a0
0051db5b     0F 83 61 0B 00 00        JNC        0x0051e6c2
0051db61     8B 4D 14                 MOV        ECX,dword ptr [EBP + 0x14]
0051db64     A1 48 4A 56 00           MOV        EAX,[0x00564a48]
0051db69     85 C0                    TEST       EAX,EAX
0051db6b     8B 39                    MOV        EDI,dword ptr [ECX]
0051db6d     89 7D D0                 MOV        dword ptr [EBP + -0x30],EDI
0051db70     8B 77 3C                 MOV        ESI,dword ptr [EDI + 0x3c]
0051db73     74 1B                    JZ         0x0051db90
0051db75     F7 C6 00 00 00 02        TEST       ESI,0x2000000
0051db7b     74 13                    JZ         0x0051db90
0051db7d     A1 4C 4A 56 00           MOV        EAX,[0x00564a4c]
0051db82     85 C0                    TEST       EAX,EAX
0051db84     0F 84 38 0B 00 00        JZ         0x0051e6c2
0051db8a     81 E6 FF FF FF FD        AND        ESI,0xfdffffff
0051db90     8B 47 08                 MOV        EAX,dword ptr [EDI + 0x8]
0051db93     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0051db99     8B C8                    MOV        ECX,EAX
0051db9b     C1 E8 10                 SHR        EAX,0x10
0051db9e     89 45 10                 MOV        dword ptr [EBP + 0x10],EAX
0051dba1     8B 02                    MOV        EAX,dword ptr [EDX]
0051dba3     81 E1 FF FF 00 00        AND        ECX,0xffff
0051dba9     8D 90 00 00 1B 00        LEA        EDX,[EAX + 0x1b0000]
0051dbaf     51                       PUSH       ECX
0051dbb0     89 55 1C                 MOV        dword ptr [EBP + 0x1c],EDX
0051dbb3     8B D6                    MOV        EDX,ESI
0051dbb5     81 E2 00 04 00 00        AND        EDX,0x400
0051dbbb     89 55 0C                 MOV        dword ptr [EBP + 0xc],EDX
0051dbbe     74 0F                    JZ         0x0051dbcf
0051dbc0     8B 45 1C                 MOV        EAX,dword ptr [EBP + 0x1c]
0051dbc3     50                       PUSH       EAX
0051dbc4     8D 47 40                 LEA        EAX,[EDI + 0x40]
0051dbc7     50                       PUSH       EAX
0051dbc8     E8 BD 7F 02 00           CALL       0x00545b8a
0051dbcd     EB 14                    JMP        0x0051dbe3
0051dbcf     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051dbd2     50                       PUSH       EAX
0051dbd3     05 00 40 1B 00           ADD        EAX,0x1b4000
0051dbd8     50                       PUSH       EAX
0051dbd9     8D 47 40                 LEA        EAX,[EDI + 0x40]
0051dbdc     51                       PUSH       ECX
0051dbdd     50                       PUSH       EAX
0051dbde     E8 B3 7F 02 00           CALL       0x00545b96
0051dbe3     8B 45 18                 MOV        EAX,dword ptr [EBP + 0x18]
0051dbe6     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0051dbec     8A 8D 6C FF FF FF        MOV        CL,byte ptr [EBP + 0xffffff6c]
0051dbf2     81 E6 00 03 00 00        AND        ESI,0x300
0051dbf8     8B 40 1C                 MOV        EAX,dword ptr [EAX + 0x1c]
0051dbfb     8B 12                    MOV        EDX,dword ptr [EDX]
0051dbfd     25 FF FF FF 03           AND        EAX,0x3ffffff
0051dc02     81 C2 00 80 1B 00        ADD        EDX,0x1b8000
0051dc08     C1 E6 10                 SHL        ESI,0x10
0051dc0b     33 C6                    XOR        EAX,ESI
0051dc0d     F6 C1 04                 TEST       CL,0x4
0051dc10     74 10                    JZ         0x0051dc22
0051dc12     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051dc15     8D 0C 49                 LEA        ECX,[ECX + ECX*0x2]
0051dc18     85 C9                    TEST       ECX,ECX
0051dc1a     74 1C                    JZ         0x0051dc38
0051dc1c     8B FA                    MOV        EDI,EDX
0051dc1e     F3 AB                    STOSD.REP  ES:EDI
0051dc20     EB 16                    JMP        0x0051dc38
0051dc22     8B 0D 00 87 60 00        MOV        ECX,dword ptr [0x00608700]
0051dc28     89 01                    MOV        dword ptr [ECX],EAX
0051dc2a     8B 45 10                 MOV        EAX,dword ptr [EBP + 0x10]
0051dc2d     8B 4F 24                 MOV        ECX,dword ptr [EDI + 0x24]
0051dc30     50                       PUSH       EAX
0051dc31     52                       PUSH       EDX
0051dc32     51                       PUSH       ECX
0051dc33     E8 4C 7F 02 00           CALL       0x00545b84
0051dc38     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0051dc3e     8B 45 D0                 MOV        EAX,dword ptr [EBP + -0x30]
0051dc41     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051dc44     8B 3A                    MOV        EDI,dword ptr [EDX]
0051dc46     8B 70 28                 MOV        ESI,dword ptr [EAX + 0x28]
0051dc49     81 C7 00 40 1B 00        ADD        EDI,0x1b4000
0051dc4f     85 C9                    TEST       ECX,ECX
0051dc51     0F 84 E7 04 00 00        JZ         0x0051e13e
0051dc57     0F BF 4D CC              MOVSX      ECX,word ptr [EBP + -0x34]
0051dc5b     8B 55 08                 MOV        EDX,dword ptr [EBP + 0x8]
0051dc5e     8D 0C 8A                 LEA        ECX,[EDX + ECX*0x4]
0051dc61     33 D2                    XOR        EDX,EDX
0051dc63     66 8B 50 14              MOV        DX,word ptr [EAX + 0x14]
0051dc67     89 4D F0                 MOV        dword ptr [EBP + -0x10],ECX
0051dc6a     33 C9                    XOR        ECX,ECX
0051dc6c     66 8B 48 16              MOV        CX,word ptr [EAX + 0x16]
0051dc70     03 D1                    ADD        EDX,ECX
0051dc72     33 C9                    XOR        ECX,ECX
0051dc74     66 8B 48 10              MOV        CX,word ptr [EAX + 0x10]
0051dc78     03 D1                    ADD        EDX,ECX
0051dc7a     33 C9                    XOR        ECX,ECX
0051dc7c     66 8B 48 12              MOV        CX,word ptr [EAX + 0x12]
0051dc80     03 D1                    ADD        EDX,ECX
0051dc82     33 C9                    XOR        ECX,ECX
0051dc84     66 8B 48 1A              MOV        CX,word ptr [EAX + 0x1a]
0051dc88     C1 E2 04                 SHL        EDX,0x4
0051dc8b     03 F2                    ADD        ESI,EDX
0051dc8d     89 4D E4                 MOV        dword ptr [EBP + -0x1c],ECX
0051dc90     85 C9                    TEST       ECX,ECX
0051dc92     0F 84 20 02 00 00        JZ         0x0051deb8
0051dc98     A1 18 98 5E 00           MOV        EAX,[0x005e9818]
0051dc9d     85 C0                    TEST       EAX,EAX
0051dc9f     0F 84 13 02 00 00        JZ         0x0051deb8
0051dca5     8B 06                    MOV        EAX,dword ptr [ESI]
0051dca7     8B D0                    MOV        EDX,EAX
0051dca9     81 E2 FF 00 00 00        AND        EDX,0xff
0051dcaf     8B 8C 97 00 C0 FF FF     MOV        ECX,dword ptr [EDI + EDX*0x4 + 0xffffc000]
0051dcb6     8B D0                    MOV        EDX,EAX
0051dcb8     C1 EA 08                 SHR        EDX,0x8
0051dcbb     81 E2 FF 00 00 00        AND        EDX,0xff
0051dcc1     89 4D 1C                 MOV        dword ptr [EBP + 0x1c],ECX
0051dcc4     8B 94 97 00 C0 FF FF     MOV        EDX,dword ptr [EDI + EDX*0x4 + 0xffffc000]
0051dccb     89 55 10                 MOV        dword ptr [EBP + 0x10],EDX
0051dcce     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051dcd4     89 0A                    MOV        dword ptr [EDX],ECX
0051dcd6     8B C8                    MOV        ECX,EAX
0051dcd8     C1 E9 10                 SHR        ECX,0x10
0051dcdb     81 E1 FF 00 00 00        AND        ECX,0xff
0051dce1     C1 E8 18                 SHR        EAX,0x18
0051dce4     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051dceb     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051dcee     89 55 20                 MOV        dword ptr [EBP + 0x20],EDX
0051dcf1     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051dcf7     89 0A                    MOV        dword ptr [EDX],ECX
0051dcf9     8B 84 87 00 C0 FF FF     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0xffffc000]
0051dd00     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051dd06     89 45 0C                 MOV        dword ptr [EBP + 0xc],EAX
0051dd09     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051dd0c     89 02                    MOV        dword ptr [EDX],EAX
0051dd0e     BA 40 01 00 00           MOV        EDX,0x140
0051dd13     66 39 55 1C              CMP        word ptr [EBP + 0x1c],DX
0051dd17     72 14                    JC         0x0051dd2d
0051dd19     66 3B CA                 CMP        CX,DX
0051dd1c     72 0F                    JC         0x0051dd2d
0051dd1e     66 3B C2                 CMP        AX,DX
0051dd21     72 0A                    JC         0x0051dd2d
0051dd23     66 39 55 0C              CMP        word ptr [EBP + 0xc],DX
0051dd27     0F 83 79 01 00 00        JNC        0x0051dea6
0051dd2d     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051dd33     A1 10 87 60 00           MOV        EAX,[0x00608710]
0051dd38     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051dd3b     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051dd3e     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051dd44     0F BF 48 02              MOVSX      ECX,word ptr [EAX + 0x2]
0051dd48     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051dd4c     89 55 E8                 MOV        dword ptr [EBP + -0x18],EDX
0051dd4f     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051dd55     0F BF 00                 MOVSX      EAX,word ptr [EAX]
0051dd58     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051dd5b     89 55 E0                 MOV        dword ptr [EBP + -0x20],EDX
0051dd5e     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051dd64     89 45 EC                 MOV        dword ptr [EBP + -0x14],EAX
0051dd67     89 4D D8                 MOV        dword ptr [EBP + -0x28],ECX
0051dd6a     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051dd6e     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051dd71     0F AF C2                 IMUL       EAX,EDX
0051dd74     8B 55 FC                 MOV        EDX,dword ptr [EBP + -0x4]
0051dd77     0F AF D1                 IMUL       EDX,ECX
0051dd7a     8B 4D E0                 MOV        ECX,dword ptr [EBP + -0x20]
0051dd7d     03 C2                    ADD        EAX,EDX
0051dd7f     0F AF 4D E8              IMUL       ECX,dword ptr [EBP + -0x18]
0051dd83     03 C1                    ADD        EAX,ECX
0051dd85     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051dd88     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051dd8b     8B 45 E0                 MOV        EAX,dword ptr [EBP + -0x20]
0051dd8e     0F AF 45 D8              IMUL       EAX,dword ptr [EBP + -0x28]
0051dd92     8B 55 F4                 MOV        EDX,dword ptr [EBP + -0xc]
0051dd95     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051dd9b     2B D0                    SUB        EDX,EAX
0051dd9d     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051dda0     0F AF 45 E8              IMUL       EAX,dword ptr [EBP + -0x18]
0051dda4     2B D0                    SUB        EDX,EAX
0051dda6     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051dda9     0F AF 45 FC              IMUL       EAX,dword ptr [EBP + -0x4]
0051ddad     2B D0                    SUB        EDX,EAX
0051ddaf     A1 14 87 60 00           MOV        EAX,[0x00608714]
0051ddb4     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051ddba     89 10                    MOV        dword ptr [EAX],EDX
0051ddbc     72 35                    JC         0x0051ddf3
0051ddbe     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051ddc1     81 E2 00 00 FF FF        AND        EDX,0xffff0000
0051ddc7     81 FA 00 00 F0 00        CMP        EDX,0xf00000
0051ddcd     72 24                    JC         0x0051ddf3
0051ddcf     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051ddd2     25 00 00 FF FF           AND        EAX,0xffff0000
0051ddd7     3D 00 00 F0 00           CMP        EAX,0xf00000
0051dddc     72 15                    JC         0x0051ddf3
0051ddde     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051dde1     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051dde7     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051dded     0F 83 B3 00 00 00        JNC        0x0051dea6
0051ddf3     E8 F8 E4 FF FF           CALL       0x0051c2f0
0051ddf8     85 C0                    TEST       EAX,EAX
0051ddfa     7C 18                    JL         0x0051de14
0051ddfc     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051de02     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051de05     89 02                    MOV        dword ptr [EDX],EAX
0051de07     E8 74 E4 FF FF           CALL       0x0051c280
0051de0c     85 C0                    TEST       EAX,EAX
0051de0e     0F 8D 92 00 00 00        JGE        0x0051dea6
0051de14     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
0051de17     8B 56 0C                 MOV        EDX,dword ptr [ESI + 0xc]
0051de1a     25 FF 00 00 00           AND        EAX,0xff
0051de1f     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051de22     8B 4E 08                 MOV        ECX,dword ptr [ESI + 0x8]
0051de25     8B 94 87 00 40 00 00     MOV        EDX,dword ptr [EDI + EAX*0x4 + 0x4000]
0051de2c     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051de2f     8B 55 F0                 MOV        EDX,dword ptr [EBP + -0x10]
0051de32     8B 02                    MOV        EAX,dword ptr [EDX]
0051de34     89 1A                    MOV        dword ptr [EDX],EBX
0051de36     8B 55 1C                 MOV        EDX,dword ptr [EBP + 0x1c]
0051de39     0D 00 00 00 09           OR         EAX,0x9000000
0051de3e     89 03                    MOV        dword ptr [EBX],EAX
0051de40     8B 45 10                 MOV        EAX,dword ptr [EBP + 0x10]
0051de43     89 43 08                 MOV        dword ptr [EBX + 0x8],EAX
0051de46     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051de49     89 53 10                 MOV        dword ptr [EBX + 0x10],EDX
0051de4c     8B 55 0C                 MOV        EDX,dword ptr [EBP + 0xc]
0051de4f     89 43 18                 MOV        dword ptr [EBX + 0x18],EAX
0051de52     89 53 20                 MOV        dword ptr [EBX + 0x20],EDX
0051de55     A1 48 4A 56 00           MOV        EAX,[0x00564a48]
0051de5a     85 C0                    TEST       EAX,EAX
0051de5c     8B 45 FC                 MOV        EAX,dword ptr [EBP + -0x4]
0051de5f     74 05                    JZ         0x0051de66
0051de61     25 FF FF FF FD           AND        EAX,0xfdffffff
0051de66     8B 55 B8                 MOV        EDX,dword ptr [EBP + -0x48]
0051de69     0D 00 00 00 2C           OR         EAX,0x2c000000
0051de6e     89 43 04                 MOV        dword ptr [EBX + 0x4],EAX
0051de71     8B C1                    MOV        EAX,ECX
0051de73     C1 E8 10                 SHR        EAX,0x10
0051de76     0B C2                    OR         EAX,EDX
0051de78     8B 55 B0                 MOV        EDX,dword ptr [EBP + -0x50]
0051de7b     89 43 0C                 MOV        dword ptr [EBX + 0xc],EAX
0051de7e     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051de81     81 E1 FF FF 00 00        AND        ECX,0xffff
0051de87     83 C3 28                 ADD        EBX,0x28
0051de8a     0B D1                    OR         EDX,ECX
0051de8c     8B C8                    MOV        ECX,EAX
0051de8e     81 E1 FF FF 00 00        AND        ECX,0xffff
0051de94     89 53 EC                 MOV        dword ptr [EBX + -0x14],EDX
0051de97     C1 E8 10                 SHR        EAX,0x10
0051de9a     89 4B F4                 MOV        dword ptr [EBX + -0xc],ECX
0051de9d     89 43 FC                 MOV        dword ptr [EBX + -0x4],EAX
0051dea0     FF 0D 18 98 5E 00        DEC        dword ptr [0x005e9818]
0051dea6     8B 45 E4                 MOV        EAX,dword ptr [EBP + -0x1c]
0051dea9     83 C6 10                 ADD        ESI,0x10
0051deac     48                       DEC        EAX
0051dead     85 C0                    TEST       EAX,EAX
0051deaf     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051deb2     0F 85 E0 FD FF FF        JNZ        0x0051dc98
0051deb8     8B 55 D0                 MOV        EDX,dword ptr [EBP + -0x30]
0051debb     33 C0                    XOR        EAX,EAX
0051debd     66 8B 42 1E              MOV        AX,word ptr [EDX + 0x1e]
0051dec1     85 C0                    TEST       EAX,EAX
0051dec3     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051dec6     0F 84 F6 07 00 00        JZ         0x0051e6c2
0051decc     A1 18 98 5E 00           MOV        EAX,[0x005e9818]
0051ded1     85 C0                    TEST       EAX,EAX
0051ded3     0F 84 E9 07 00 00        JZ         0x0051e6c2
0051ded9     8B 06                    MOV        EAX,dword ptr [ESI]
0051dedb     8B D0                    MOV        EDX,EAX
0051dedd     8B C8                    MOV        ECX,EAX
0051dedf     C1 EA 08                 SHR        EDX,0x8
0051dee2     81 E2 FF 00 00 00        AND        EDX,0xff
0051dee8     81 E1 FF 00 00 00        AND        ECX,0xff
0051deee     8B 94 97 00 C0 FF FF     MOV        EDX,dword ptr [EDI + EDX*0x4 + 0xffffc000]
0051def5     8B 8C 8F 00 C0 FF FF     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051defc     89 55 10                 MOV        dword ptr [EBP + 0x10],EDX
0051deff     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051df05     89 4D 1C                 MOV        dword ptr [EBP + 0x1c],ECX
0051df08     89 0A                    MOV        dword ptr [EDX],ECX
0051df0a     8B C8                    MOV        ECX,EAX
0051df0c     C1 E9 10                 SHR        ECX,0x10
0051df0f     81 E1 FF 00 00 00        AND        ECX,0xff
0051df15     C1 E8 18                 SHR        EAX,0x18
0051df18     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051df1f     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051df22     89 55 20                 MOV        dword ptr [EBP + 0x20],EDX
0051df25     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051df2b     89 0A                    MOV        dword ptr [EDX],ECX
0051df2d     8B 84 87 00 C0 FF FF     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0xffffc000]
0051df34     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051df3a     89 45 0C                 MOV        dword ptr [EBP + 0xc],EAX
0051df3d     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051df40     89 02                    MOV        dword ptr [EDX],EAX
0051df42     BA 40 01 00 00           MOV        EDX,0x140
0051df47     66 39 55 1C              CMP        word ptr [EBP + 0x1c],DX
0051df4b     72 14                    JC         0x0051df61
0051df4d     66 3B CA                 CMP        CX,DX
0051df50     72 0F                    JC         0x0051df61
0051df52     66 3B C2                 CMP        AX,DX
0051df55     72 0A                    JC         0x0051df61
0051df57     66 39 55 0C              CMP        word ptr [EBP + 0xc],DX
0051df5b     0F 83 C6 01 00 00        JNC        0x0051e127
0051df61     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051df67     A1 10 87 60 00           MOV        EAX,[0x00608710]
0051df6c     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051df6f     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051df72     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051df78     0F BF 48 02              MOVSX      ECX,word ptr [EAX + 0x2]
0051df7c     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051df80     89 55 E8                 MOV        dword ptr [EBP + -0x18],EDX
0051df83     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051df89     0F BF 00                 MOVSX      EAX,word ptr [EAX]
0051df8c     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051df8f     89 55 E0                 MOV        dword ptr [EBP + -0x20],EDX
0051df92     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051df98     89 45 EC                 MOV        dword ptr [EBP + -0x14],EAX
0051df9b     89 4D D8                 MOV        dword ptr [EBP + -0x28],ECX
0051df9e     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051dfa2     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051dfa5     0F AF C2                 IMUL       EAX,EDX
0051dfa8     8B 55 FC                 MOV        EDX,dword ptr [EBP + -0x4]
0051dfab     0F AF D1                 IMUL       EDX,ECX
0051dfae     8B 4D E0                 MOV        ECX,dword ptr [EBP + -0x20]
0051dfb1     03 C2                    ADD        EAX,EDX
0051dfb3     0F AF 4D E8              IMUL       ECX,dword ptr [EBP + -0x18]
0051dfb7     03 C1                    ADD        EAX,ECX
0051dfb9     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051dfbc     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051dfbf     8B 45 E0                 MOV        EAX,dword ptr [EBP + -0x20]
0051dfc2     0F AF 45 D8              IMUL       EAX,dword ptr [EBP + -0x28]
0051dfc6     8B 55 F4                 MOV        EDX,dword ptr [EBP + -0xc]
0051dfc9     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051dfcf     2B D0                    SUB        EDX,EAX
0051dfd1     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051dfd4     0F AF 45 E8              IMUL       EAX,dword ptr [EBP + -0x18]
0051dfd8     2B D0                    SUB        EDX,EAX
0051dfda     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051dfdd     0F AF 45 FC              IMUL       EAX,dword ptr [EBP + -0x4]
0051dfe1     2B D0                    SUB        EDX,EAX
0051dfe3     A1 14 87 60 00           MOV        EAX,[0x00608714]
0051dfe8     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051dfee     89 10                    MOV        dword ptr [EAX],EDX
0051dff0     72 35                    JC         0x0051e027
0051dff2     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051dff5     81 E2 00 00 FF FF        AND        EDX,0xffff0000
0051dffb     81 FA 00 00 F0 00        CMP        EDX,0xf00000
0051e001     72 24                    JC         0x0051e027
0051e003     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e006     25 00 00 FF FF           AND        EAX,0xffff0000
0051e00b     3D 00 00 F0 00           CMP        EAX,0xf00000
0051e010     72 15                    JC         0x0051e027
0051e012     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051e015     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051e01b     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051e021     0F 83 00 01 00 00        JNC        0x0051e127
0051e027     E8 C4 E2 FF FF           CALL       0x0051c2f0
0051e02c     85 C0                    TEST       EAX,EAX
0051e02e     7C 18                    JL         0x0051e048
0051e030     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051e036     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051e039     89 02                    MOV        dword ptr [EDX],EAX
0051e03b     E8 40 E2 FF FF           CALL       0x0051c280
0051e040     85 C0                    TEST       EAX,EAX
0051e042     0F 8D DF 00 00 00        JGE        0x0051e127
0051e048     8B 4E 0C                 MOV        ECX,dword ptr [ESI + 0xc]
0051e04b     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
0051e04e     89 4D F8                 MOV        dword ptr [EBP + -0x8],ECX
0051e051     8B C8                    MOV        ECX,EAX
0051e053     81 E1 FF 00 00 00        AND        ECX,0xff
0051e059     8B 56 08                 MOV        EDX,dword ptr [ESI + 0x8]
0051e05c     8B 8C 8F 00 40 00 00     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0x4000]
0051e063     89 4D FC                 MOV        dword ptr [EBP + -0x4],ECX
0051e066     8B C8                    MOV        ECX,EAX
0051e068     C1 E9 08                 SHR        ECX,0x8
0051e06b     81 E1 FF 00 00 00        AND        ECX,0xff
0051e071     8B 8C 8F 00 40 00 00     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0x4000]
0051e078     89 4D EC                 MOV        dword ptr [EBP + -0x14],ECX
0051e07b     8B C8                    MOV        ECX,EAX
0051e07d     C1 E8 18                 SHR        EAX,0x18
0051e080     C1 E9 10                 SHR        ECX,0x10
0051e083     8B 84 87 00 40 00 00     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0x4000]
0051e08a     81 E1 FF 00 00 00        AND        ECX,0xff
0051e090     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051e093     8B 45 F0                 MOV        EAX,dword ptr [EBP + -0x10]
0051e096     8B 8C 8F 00 40 00 00     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0x4000]
0051e09d     89 4D DC                 MOV        dword ptr [EBP + -0x24],ECX
0051e0a0     8B 08                    MOV        ECX,dword ptr [EAX]
0051e0a2     81 C9 00 00 00 0C        OR         ECX,0xc000000
0051e0a8     89 18                    MOV        dword ptr [EAX],EBX
0051e0aa     8B 45 1C                 MOV        EAX,dword ptr [EBP + 0x1c]
0051e0ad     89 0B                    MOV        dword ptr [EBX],ECX
0051e0af     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051e0b2     89 43 14                 MOV        dword ptr [EBX + 0x14],EAX
0051e0b5     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051e0b8     89 4B 08                 MOV        dword ptr [EBX + 0x8],ECX
0051e0bb     8B 4D 20                 MOV        ECX,dword ptr [EBP + 0x20]
0051e0be     89 43 2C                 MOV        dword ptr [EBX + 0x2c],EAX
0051e0c1     89 4B 20                 MOV        dword ptr [EBX + 0x20],ECX
0051e0c4     A1 48 4A 56 00           MOV        EAX,[0x00564a48]
0051e0c9     85 C0                    TEST       EAX,EAX
0051e0cb     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051e0ce     74 05                    JZ         0x0051e0d5
0051e0d0     25 FF FF FF FD           AND        EAX,0xfdffffff
0051e0d5     8B 4D FC                 MOV        ECX,dword ptr [EBP + -0x4]
0051e0d8     0D 00 00 00 3C           OR         EAX,0x3c000000
0051e0dd     89 43 04                 MOV        dword ptr [EBX + 0x4],EAX
0051e0e0     8B 45 DC                 MOV        EAX,dword ptr [EBP + -0x24]
0051e0e3     89 4B 10                 MOV        dword ptr [EBX + 0x10],ECX
0051e0e6     8B 4D F4                 MOV        ECX,dword ptr [EBP + -0xc]
0051e0e9     89 43 1C                 MOV        dword ptr [EBX + 0x1c],EAX
0051e0ec     8B C2                    MOV        EAX,EDX
0051e0ee     89 4B 28                 MOV        dword ptr [EBX + 0x28],ECX
0051e0f1     8B 4D B8                 MOV        ECX,dword ptr [EBP + -0x48]
0051e0f4     C1 E8 10                 SHR        EAX,0x10
0051e0f7     0B C1                    OR         EAX,ECX
0051e0f9     8B 4D B0                 MOV        ECX,dword ptr [EBP + -0x50]
0051e0fc     89 43 0C                 MOV        dword ptr [EBX + 0xc],EAX
0051e0ff     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051e102     81 E2 FF FF 00 00        AND        EDX,0xffff
0051e108     83 C3 34                 ADD        EBX,0x34
0051e10b     0B CA                    OR         ECX,EDX
0051e10d     8B D0                    MOV        EDX,EAX
0051e10f     81 E2 FF FF 00 00        AND        EDX,0xffff
0051e115     89 4B E4                 MOV        dword ptr [EBX + -0x1c],ECX
0051e118     C1 E8 10                 SHR        EAX,0x10
0051e11b     89 53 F0                 MOV        dword ptr [EBX + -0x10],EDX
0051e11e     89 43 FC                 MOV        dword ptr [EBX + -0x4],EAX
0051e121     FF 0D 18 98 5E 00        DEC        dword ptr [0x005e9818]
0051e127     8B 45 E4                 MOV        EAX,dword ptr [EBP + -0x1c]
0051e12a     83 C6 10                 ADD        ESI,0x10
0051e12d     48                       DEC        EAX
0051e12e     85 C0                    TEST       EAX,EAX
0051e130     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051e133     0F 85 93 FD FF FF        JNZ        0x0051decc
0051e139     E9 84 05 00 00           JMP        0x0051e6c2
0051e13e     33 C9                    XOR        ECX,ECX
0051e140     33 D2                    XOR        EDX,EDX
0051e142     66 8B 48 14              MOV        CX,word ptr [EAX + 0x14]
0051e146     66 8B 50 16              MOV        DX,word ptr [EAX + 0x16]
0051e14a     03 CA                    ADD        ECX,EDX
0051e14c     33 D2                    XOR        EDX,EDX
0051e14e     66 8B 50 10              MOV        DX,word ptr [EAX + 0x10]
0051e152     03 CA                    ADD        ECX,EDX
0051e154     33 D2                    XOR        EDX,EDX
0051e156     66 8B 50 12              MOV        DX,word ptr [EAX + 0x12]
0051e15a     03 CA                    ADD        ECX,EDX
0051e15c     C1 E1 04                 SHL        ECX,0x4
0051e15f     03 F1                    ADD        ESI,ECX
0051e161     33 C9                    XOR        ECX,ECX
0051e163     66 8B 48 1A              MOV        CX,word ptr [EAX + 0x1a]
0051e167     85 C9                    TEST       ECX,ECX
0051e169     89 4D E4                 MOV        dword ptr [EBP + -0x1c],ECX
0051e16c     0F 84 77 02 00 00        JZ         0x0051e3e9
0051e172     A1 18 98 5E 00           MOV        EAX,[0x005e9818]
0051e177     85 C0                    TEST       EAX,EAX
0051e179     0F 84 6A 02 00 00        JZ         0x0051e3e9
0051e17f     8B 06                    MOV        EAX,dword ptr [ESI]
0051e181     8B C8                    MOV        ECX,EAX
0051e183     81 E1 FF 00 00 00        AND        ECX,0xff
0051e189     89 4D D4                 MOV        dword ptr [EBP + -0x2c],ECX
0051e18c     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e193     8B C8                    MOV        ECX,EAX
0051e195     C1 E9 08                 SHR        ECX,0x8
0051e198     81 E1 FF 00 00 00        AND        ECX,0xff
0051e19e     89 55 1C                 MOV        dword ptr [EBP + 0x1c],EDX
0051e1a1     89 4D DC                 MOV        dword ptr [EBP + -0x24],ECX
0051e1a4     8B 8C 8F 00 C0 FF FF     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e1ab     89 4D 10                 MOV        dword ptr [EBP + 0x10],ECX
0051e1ae     8B 0D 0C 87 60 00        MOV        ECX,dword ptr [0x0060870c]
0051e1b4     89 11                    MOV        dword ptr [ECX],EDX
0051e1b6     8B C8                    MOV        ECX,EAX
0051e1b8     C1 E9 10                 SHR        ECX,0x10
0051e1bb     81 E1 FF 00 00 00        AND        ECX,0xff
0051e1c1     89 4D F0                 MOV        dword ptr [EBP + -0x10],ECX
0051e1c4     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e1cb     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051e1ce     89 55 20                 MOV        dword ptr [EBP + 0x20],EDX
0051e1d1     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e1d7     C1 E8 18                 SHR        EAX,0x18
0051e1da     89 0A                    MOV        dword ptr [EDX],ECX
0051e1dc     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051e1e2     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051e1e5     8B 84 87 00 C0 FF FF     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0xffffc000]
0051e1ec     89 45 0C                 MOV        dword ptr [EBP + 0xc],EAX
0051e1ef     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e1f2     89 02                    MOV        dword ptr [EDX],EAX
0051e1f4     BA 40 01 00 00           MOV        EDX,0x140
0051e1f9     66 39 55 1C              CMP        word ptr [EBP + 0x1c],DX
0051e1fd     72 14                    JC         0x0051e213
0051e1ff     66 3B CA                 CMP        CX,DX
0051e202     72 0F                    JC         0x0051e213
0051e204     66 3B C2                 CMP        AX,DX
0051e207     72 0A                    JC         0x0051e213
0051e209     66 39 55 0C              CMP        word ptr [EBP + 0xc],DX
0051e20d     0F 83 C4 01 00 00        JNC        0x0051e3d7
0051e213     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e219     A1 10 87 60 00           MOV        EAX,[0x00608710]
0051e21e     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051e221     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051e224     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e22a     0F BF 48 02              MOVSX      ECX,word ptr [EAX + 0x2]
0051e22e     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051e232     89 55 E8                 MOV        dword ptr [EBP + -0x18],EDX
0051e235     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051e23b     0F BF 00                 MOVSX      EAX,word ptr [EAX]
0051e23e     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051e241     89 55 E0                 MOV        dword ptr [EBP + -0x20],EDX
0051e244     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051e24a     89 45 EC                 MOV        dword ptr [EBP + -0x14],EAX
0051e24d     89 4D D8                 MOV        dword ptr [EBP + -0x28],ECX
0051e250     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051e254     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051e257     0F AF C2                 IMUL       EAX,EDX
0051e25a     8B 55 FC                 MOV        EDX,dword ptr [EBP + -0x4]
0051e25d     0F AF D1                 IMUL       EDX,ECX
0051e260     8B 4D E0                 MOV        ECX,dword ptr [EBP + -0x20]
0051e263     03 C2                    ADD        EAX,EDX
0051e265     0F AF 4D E8              IMUL       ECX,dword ptr [EBP + -0x18]
0051e269     03 C1                    ADD        EAX,ECX
0051e26b     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051e26e     89 45 C0                 MOV        dword ptr [EBP + -0x40],EAX
0051e271     8B 45 E0                 MOV        EAX,dword ptr [EBP + -0x20]
0051e274     0F AF 45 D8              IMUL       EAX,dword ptr [EBP + -0x28]
0051e278     8B 55 C0                 MOV        EDX,dword ptr [EBP + -0x40]
0051e27b     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051e281     2B D0                    SUB        EDX,EAX
0051e283     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051e286     0F AF 45 E8              IMUL       EAX,dword ptr [EBP + -0x18]
0051e28a     2B D0                    SUB        EDX,EAX
0051e28c     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051e28f     0F AF 45 FC              IMUL       EAX,dword ptr [EBP + -0x4]
0051e293     2B D0                    SUB        EDX,EAX
0051e295     A1 14 87 60 00           MOV        EAX,[0x00608714]
0051e29a     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051e2a0     89 10                    MOV        dword ptr [EAX],EDX
0051e2a2     72 35                    JC         0x0051e2d9
0051e2a4     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051e2a7     81 E2 00 00 FF FF        AND        EDX,0xffff0000
0051e2ad     81 FA 00 00 F0 00        CMP        EDX,0xf00000
0051e2b3     72 24                    JC         0x0051e2d9
0051e2b5     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e2b8     25 00 00 FF FF           AND        EAX,0xffff0000
0051e2bd     3D 00 00 F0 00           CMP        EAX,0xf00000
0051e2c2     72 15                    JC         0x0051e2d9
0051e2c4     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051e2c7     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051e2cd     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051e2d3     0F 83 FE 00 00 00        JNC        0x0051e3d7
0051e2d9     E8 12 E0 FF FF           CALL       0x0051c2f0
0051e2de     85 C0                    TEST       EAX,EAX
0051e2e0     7C 18                    JL         0x0051e2fa
0051e2e2     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051e2e8     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051e2eb     89 02                    MOV        dword ptr [EDX],EAX
0051e2ed     E8 8E DF FF FF           CALL       0x0051c280
0051e2f2     85 C0                    TEST       EAX,EAX
0051e2f4     0F 8D DD 00 00 00        JGE        0x0051e3d7
0051e2fa     8B 4D D4                 MOV        ECX,dword ptr [EBP + -0x2c]
0051e2fd     8B 55 DC                 MOV        EDX,dword ptr [EBP + -0x24]
0051e300     8B 04 8F                 MOV        EAX,dword ptr [EDI + ECX*0x4]
0051e303     8B 0C 97                 MOV        ECX,dword ptr [EDI + EDX*0x4]
0051e306     8B 55 F0                 MOV        EDX,dword ptr [EBP + -0x10]
0051e309     3B C8                    CMP        ECX,EAX
0051e30b     8B 14 97                 MOV        EDX,dword ptr [EDI + EDX*0x4]
0051e30e     89 55 F0                 MOV        dword ptr [EBP + -0x10],EDX
0051e311     8B 55 F4                 MOV        EDX,dword ptr [EBP + -0xc]
0051e314     8B 14 97                 MOV        EDX,dword ptr [EDI + EDX*0x4]
0051e317     7E 02                    JLE        0x0051e31b
0051e319     8B C1                    MOV        EAX,ECX
0051e31b     8B 4D F0                 MOV        ECX,dword ptr [EBP + -0x10]
0051e31e     3B C8                    CMP        ECX,EAX
0051e320     7E 02                    JLE        0x0051e324
0051e322     8B C1                    MOV        EAX,ECX
0051e324     3B D0                    CMP        EDX,EAX
0051e326     7E 02                    JLE        0x0051e32a
0051e328     8B C2                    MOV        EAX,EDX
0051e32a     8D 88 C0 FE FF FF        LEA        ECX,[EAX + 0xfffffec0]
0051e330     81 F9 C0 0E 00 00        CMP        ECX,0xec0
0051e336     0F 83 9B 00 00 00        JNC        0x0051e3d7
0051e33c     8B 4E 0C                 MOV        ECX,dword ptr [ESI + 0xc]
0051e33f     8B 56 08                 MOV        EDX,dword ptr [ESI + 0x8]
0051e342     89 4D F8                 MOV        dword ptr [EBP + -0x8],ECX
0051e345     8B 4E 04                 MOV        ECX,dword ptr [ESI + 0x4]
0051e348     81 E1 FF 00 00 00        AND        ECX,0xff
0051e34e     25 FC FF 00 00           AND        EAX,0xfffc
0051e353     8B 8C 8F 00 40 00 00     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0x4000]
0051e35a     89 4D FC                 MOV        dword ptr [EBP + -0x4],ECX
0051e35d     8B 4D 08                 MOV        ECX,dword ptr [EBP + 0x8]
0051e360     03 C1                    ADD        EAX,ECX
0051e362     8B 08                    MOV        ECX,dword ptr [EAX]
0051e364     89 18                    MOV        dword ptr [EAX],EBX
0051e366     8B 45 10                 MOV        EAX,dword ptr [EBP + 0x10]
0051e369     81 C9 00 00 00 09        OR         ECX,0x9000000
0051e36f     89 0B                    MOV        dword ptr [EBX],ECX
0051e371     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051e374     89 43 08                 MOV        dword ptr [EBX + 0x8],EAX
0051e377     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e37a     89 4B 10                 MOV        dword ptr [EBX + 0x10],ECX
0051e37d     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051e380     89 43 18                 MOV        dword ptr [EBX + 0x18],EAX
0051e383     89 4B 20                 MOV        dword ptr [EBX + 0x20],ECX
0051e386     A1 48 4A 56 00           MOV        EAX,[0x00564a48]
0051e38b     85 C0                    TEST       EAX,EAX
0051e38d     8B 45 FC                 MOV        EAX,dword ptr [EBP + -0x4]
0051e390     74 05                    JZ         0x0051e397
0051e392     25 FF FF FF FD           AND        EAX,0xfdffffff
0051e397     8B 4D B8                 MOV        ECX,dword ptr [EBP + -0x48]
0051e39a     0D 00 00 00 2C           OR         EAX,0x2c000000
0051e39f     89 43 04                 MOV        dword ptr [EBX + 0x4],EAX
0051e3a2     8B C2                    MOV        EAX,EDX
0051e3a4     C1 E8 10                 SHR        EAX,0x10
0051e3a7     0B C1                    OR         EAX,ECX
0051e3a9     8B 4D B0                 MOV        ECX,dword ptr [EBP + -0x50]
0051e3ac     89 43 0C                 MOV        dword ptr [EBX + 0xc],EAX
0051e3af     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051e3b2     81 E2 FF FF 00 00        AND        EDX,0xffff
0051e3b8     83 C3 28                 ADD        EBX,0x28
0051e3bb     0B CA                    OR         ECX,EDX
0051e3bd     8B D0                    MOV        EDX,EAX
0051e3bf     81 E2 FF FF 00 00        AND        EDX,0xffff
0051e3c5     89 4B EC                 MOV        dword ptr [EBX + -0x14],ECX
0051e3c8     C1 E8 10                 SHR        EAX,0x10
0051e3cb     89 53 F4                 MOV        dword ptr [EBX + -0xc],EDX
0051e3ce     89 43 FC                 MOV        dword ptr [EBX + -0x4],EAX
0051e3d1     FF 0D 18 98 5E 00        DEC        dword ptr [0x005e9818]
0051e3d7     8B 45 E4                 MOV        EAX,dword ptr [EBP + -0x1c]
0051e3da     83 C6 10                 ADD        ESI,0x10
0051e3dd     48                       DEC        EAX
0051e3de     85 C0                    TEST       EAX,EAX
0051e3e0     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051e3e3     0F 85 89 FD FF FF        JNZ        0x0051e172
0051e3e9     8B 4D D0                 MOV        ECX,dword ptr [EBP + -0x30]
0051e3ec     33 C0                    XOR        EAX,EAX
0051e3ee     66 8B 41 1E              MOV        AX,word ptr [ECX + 0x1e]
0051e3f2     85 C0                    TEST       EAX,EAX
0051e3f4     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051e3f7     0F 84 C5 02 00 00        JZ         0x0051e6c2
0051e3fd     A1 18 98 5E 00           MOV        EAX,[0x005e9818]
0051e402     85 C0                    TEST       EAX,EAX
0051e404     0F 84 B8 02 00 00        JZ         0x0051e6c2
0051e40a     8B 06                    MOV        EAX,dword ptr [ESI]
0051e40c     8B C8                    MOV        ECX,EAX
0051e40e     81 E1 FF 00 00 00        AND        ECX,0xff
0051e414     89 4D D4                 MOV        dword ptr [EBP + -0x2c],ECX
0051e417     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e41e     8B C8                    MOV        ECX,EAX
0051e420     C1 E9 08                 SHR        ECX,0x8
0051e423     81 E1 FF 00 00 00        AND        ECX,0xff
0051e429     89 55 1C                 MOV        dword ptr [EBP + 0x1c],EDX
0051e42c     89 4D DC                 MOV        dword ptr [EBP + -0x24],ECX
0051e42f     8B 8C 8F 00 C0 FF FF     MOV        ECX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e436     89 4D 10                 MOV        dword ptr [EBP + 0x10],ECX
0051e439     8B 0D 0C 87 60 00        MOV        ECX,dword ptr [0x0060870c]
0051e43f     89 11                    MOV        dword ptr [ECX],EDX
0051e441     8B C8                    MOV        ECX,EAX
0051e443     C1 E9 10                 SHR        ECX,0x10
0051e446     81 E1 FF 00 00 00        AND        ECX,0xff
0051e44c     89 4D F0                 MOV        dword ptr [EBP + -0x10],ECX
0051e44f     8B 94 8F 00 C0 FF FF     MOV        EDX,dword ptr [EDI + ECX*0x4 + 0xffffc000]
0051e456     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051e459     89 55 20                 MOV        dword ptr [EBP + 0x20],EDX
0051e45c     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e462     C1 E8 18                 SHR        EAX,0x18
0051e465     89 0A                    MOV        dword ptr [EDX],ECX
0051e467     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051e46d     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051e470     8B 84 87 00 C0 FF FF     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0xffffc000]
0051e477     89 45 0C                 MOV        dword ptr [EBP + 0xc],EAX
0051e47a     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e47d     89 02                    MOV        dword ptr [EDX],EAX
0051e47f     BA 40 01 00 00           MOV        EDX,0x140
0051e484     66 39 55 1C              CMP        word ptr [EBP + 0x1c],DX
0051e488     72 14                    JC         0x0051e49e
0051e48a     66 3B CA                 CMP        CX,DX
0051e48d     72 0F                    JC         0x0051e49e
0051e48f     66 3B C2                 CMP        AX,DX
0051e492     72 0A                    JC         0x0051e49e
0051e494     66 39 55 0C              CMP        word ptr [EBP + 0xc],DX
0051e498     0F 83 12 02 00 00        JNC        0x0051e6b0
0051e49e     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e4a4     A1 10 87 60 00           MOV        EAX,[0x00608710]
0051e4a9     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051e4ac     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051e4af     8B 15 08 87 60 00        MOV        EDX,dword ptr [0x00608708]
0051e4b5     0F BF 48 02              MOVSX      ECX,word ptr [EAX + 0x2]
0051e4b9     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051e4bd     89 55 E8                 MOV        dword ptr [EBP + -0x18],EDX
0051e4c0     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051e4c6     0F BF 00                 MOVSX      EAX,word ptr [EAX]
0051e4c9     0F BF 12                 MOVSX      EDX,word ptr [EDX]
0051e4cc     89 55 E0                 MOV        dword ptr [EBP + -0x20],EDX
0051e4cf     8B 15 0C 87 60 00        MOV        EDX,dword ptr [0x0060870c]
0051e4d5     89 45 EC                 MOV        dword ptr [EBP + -0x14],EAX
0051e4d8     89 4D D8                 MOV        dword ptr [EBP + -0x28],ECX
0051e4db     0F BF 52 02              MOVSX      EDX,word ptr [EDX + 0x2]
0051e4df     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051e4e2     0F AF C2                 IMUL       EAX,EDX
0051e4e5     8B 55 FC                 MOV        EDX,dword ptr [EBP + -0x4]
0051e4e8     0F AF D1                 IMUL       EDX,ECX
0051e4eb     8B 4D E0                 MOV        ECX,dword ptr [EBP + -0x20]
0051e4ee     03 C2                    ADD        EAX,EDX
0051e4f0     0F AF 4D E8              IMUL       ECX,dword ptr [EBP + -0x18]
0051e4f4     03 C1                    ADD        EAX,ECX
0051e4f6     8B 4D 1C                 MOV        ECX,dword ptr [EBP + 0x1c]
0051e4f9     89 45 C0                 MOV        dword ptr [EBP + -0x40],EAX
0051e4fc     8B 45 E0                 MOV        EAX,dword ptr [EBP + -0x20]
0051e4ff     0F AF 45 D8              IMUL       EAX,dword ptr [EBP + -0x28]
0051e503     8B 55 C0                 MOV        EDX,dword ptr [EBP + -0x40]
0051e506     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051e50c     2B D0                    SUB        EDX,EAX
0051e50e     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051e511     0F AF 45 E8              IMUL       EAX,dword ptr [EBP + -0x18]
0051e515     2B D0                    SUB        EDX,EAX
0051e517     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051e51a     0F AF 45 FC              IMUL       EAX,dword ptr [EBP + -0x4]
0051e51e     2B D0                    SUB        EDX,EAX
0051e520     A1 14 87 60 00           MOV        EAX,[0x00608714]
0051e525     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051e52b     89 10                    MOV        dword ptr [EAX],EDX
0051e52d     72 35                    JC         0x0051e564
0051e52f     8B 55 10                 MOV        EDX,dword ptr [EBP + 0x10]
0051e532     81 E2 00 00 FF FF        AND        EDX,0xffff0000
0051e538     81 FA 00 00 F0 00        CMP        EDX,0xf00000
0051e53e     72 24                    JC         0x0051e564
0051e540     8B 45 20                 MOV        EAX,dword ptr [EBP + 0x20]
0051e543     25 00 00 FF FF           AND        EAX,0xffff0000
0051e548     3D 00 00 F0 00           CMP        EAX,0xf00000
0051e54d     72 15                    JC         0x0051e564
0051e54f     8B 4D 0C                 MOV        ECX,dword ptr [EBP + 0xc]
0051e552     81 E1 00 00 FF FF        AND        ECX,0xffff0000
0051e558     81 F9 00 00 F0 00        CMP        ECX,0xf00000
0051e55e     0F 83 4C 01 00 00        JNC        0x0051e6b0
0051e564     E8 87 DD FF FF           CALL       0x0051c2f0
0051e569     85 C0                    TEST       EAX,EAX
0051e56b     7C 18                    JL         0x0051e585
0051e56d     8B 15 10 87 60 00        MOV        EDX,dword ptr [0x00608710]
0051e573     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051e576     89 02                    MOV        dword ptr [EDX],EAX
0051e578     E8 03 DD FF FF           CALL       0x0051c280
0051e57d     85 C0                    TEST       EAX,EAX
0051e57f     0F 8D 2B 01 00 00        JGE        0x0051e6b0
0051e585     8B 55 DC                 MOV        EDX,dword ptr [EBP + -0x24]
0051e588     8B 4D D4                 MOV        ECX,dword ptr [EBP + -0x2c]
0051e58b     8B 04 97                 MOV        EAX,dword ptr [EDI + EDX*0x4]
0051e58e     8B 55 F0                 MOV        EDX,dword ptr [EBP + -0x10]
0051e591     8B 0C 8F                 MOV        ECX,dword ptr [EDI + ECX*0x4]
0051e594     8B 14 97                 MOV        EDX,dword ptr [EDI + EDX*0x4]
0051e597     3B C1                    CMP        EAX,ECX
0051e599     89 55 F0                 MOV        dword ptr [EBP + -0x10],EDX
0051e59c     8B 55 F4                 MOV        EDX,dword ptr [EBP + -0xc]
0051e59f     8B 14 97                 MOV        EDX,dword ptr [EDI + EDX*0x4]
0051e5a2     7E 02                    JLE        0x0051e5a6
0051e5a4     8B C8                    MOV        ECX,EAX
0051e5a6     8B 45 F0                 MOV        EAX,dword ptr [EBP + -0x10]
0051e5a9     3B C1                    CMP        EAX,ECX
0051e5ab     7E 02                    JLE        0x0051e5af
0051e5ad     8B C8                    MOV        ECX,EAX
0051e5af     3B D1                    CMP        EDX,ECX
0051e5b1     7E 02                    JLE        0x0051e5b5
0051e5b3     8B CA                    MOV        ECX,EDX
0051e5b5     8D 81 C0 FE FF FF        LEA        EAX,[ECX + 0xfffffec0]
0051e5bb     3D C0 0E 00 00           CMP        EAX,0xec0
0051e5c0     0F 83 EA 00 00 00        JNC        0x0051e6b0
0051e5c6     8B 56 08                 MOV        EDX,dword ptr [ESI + 0x8]
0051e5c9     8B 46 04                 MOV        EAX,dword ptr [ESI + 0x4]
0051e5cc     89 55 C0                 MOV        dword ptr [EBP + -0x40],EDX
0051e5cf     8B 56 0C                 MOV        EDX,dword ptr [ESI + 0xc]
0051e5d2     89 55 F8                 MOV        dword ptr [EBP + -0x8],EDX
0051e5d5     8B D0                    MOV        EDX,EAX
0051e5d7     81 E2 FF 00 00 00        AND        EDX,0xff
0051e5dd     81 E1 FC FF 00 00        AND        ECX,0xfffc
0051e5e3     8B 94 97 00 40 00 00     MOV        EDX,dword ptr [EDI + EDX*0x4 + 0x4000]
0051e5ea     89 55 FC                 MOV        dword ptr [EBP + -0x4],EDX
0051e5ed     8B D0                    MOV        EDX,EAX
0051e5ef     C1 EA 08                 SHR        EDX,0x8
0051e5f2     81 E2 FF 00 00 00        AND        EDX,0xff
0051e5f8     8B 94 97 00 40 00 00     MOV        EDX,dword ptr [EDI + EDX*0x4 + 0x4000]
0051e5ff     89 55 EC                 MOV        dword ptr [EBP + -0x14],EDX
0051e602     8B D0                    MOV        EDX,EAX
0051e604     C1 E8 18                 SHR        EAX,0x18
0051e607     C1 EA 10                 SHR        EDX,0x10
0051e60a     8B 84 87 00 40 00 00     MOV        EAX,dword ptr [EDI + EAX*0x4 + 0x4000]
0051e611     81 E2 FF 00 00 00        AND        EDX,0xff
0051e617     89 45 F4                 MOV        dword ptr [EBP + -0xc],EAX
0051e61a     8B 45 08                 MOV        EAX,dword ptr [EBP + 0x8]
0051e61d     8B 94 97 00 40 00 00     MOV        EDX,dword ptr [EDI + EDX*0x4 + 0x4000]
0051e624     03 C8                    ADD        ECX,EAX
0051e626     8B 01                    MOV        EAX,dword ptr [ECX]
0051e628     89 19                    MOV        dword ptr [ECX],EBX
0051e62a     8B 4D 10                 MOV        ECX,dword ptr [EBP + 0x10]
0051e62d     0D 00 00 00 0C           OR         EAX,0xc000000
0051e632     89 03                    MOV        dword ptr [EBX],EAX
0051e634     8B 45 1C                 MOV        EAX,dword ptr [EBP + 0x1c]
0051e637     89 4B 08                 MOV        dword ptr [EBX + 0x8],ECX
0051e63a     8B 4D 20                 MOV        ECX,dword ptr [EBP + 0x20]
0051e63d     89 43 14                 MOV        dword ptr [EBX + 0x14],EAX
0051e640     8B 45 0C                 MOV        EAX,dword ptr [EBP + 0xc]
0051e643     89 4B 20                 MOV        dword ptr [EBX + 0x20],ECX
0051e646     89 43 2C                 MOV        dword ptr [EBX + 0x2c],EAX
0051e649     A1 48 4A 56 00           MOV        EAX,[0x00564a48]
0051e64e     85 C0                    TEST       EAX,EAX
0051e650     8B 45 EC                 MOV        EAX,dword ptr [EBP + -0x14]
0051e653     74 05                    JZ         0x0051e65a
0051e655     25 FF FF FF FD           AND        EAX,0xfdffffff
0051e65a     8B 4D FC                 MOV        ECX,dword ptr [EBP + -0x4]
0051e65d     0D 00 00 00 3C           OR         EAX,0x3c000000
0051e662     89 43 04                 MOV        dword ptr [EBX + 0x4],EAX
0051e665     8B 45 C0                 MOV        EAX,dword ptr [EBP + -0x40]
0051e668     89 53 1C                 MOV        dword ptr [EBX + 0x1c],EDX
0051e66b     8B 55 F4                 MOV        EDX,dword ptr [EBP + -0xc]
0051e66e     89 4B 10                 MOV        dword ptr [EBX + 0x10],ECX
0051e671     8B C8                    MOV        ECX,EAX
0051e673     89 53 28                 MOV        dword ptr [EBX + 0x28],EDX
0051e676     8B 55 B8                 MOV        EDX,dword ptr [EBP + -0x48]
0051e679     C1 E9 10                 SHR        ECX,0x10
0051e67c     0B CA                    OR         ECX,EDX
0051e67e     8B 55 B0                 MOV        EDX,dword ptr [EBP + -0x50]
0051e681     25 FF FF 00 00           AND        EAX,0xffff
0051e686     89 4B 0C                 MOV        dword ptr [EBX + 0xc],ECX
0051e689     0B D0                    OR         EDX,EAX
0051e68b     8B 45 F8                 MOV        EAX,dword ptr [EBP + -0x8]
0051e68e     8B C8                    MOV        ECX,EAX
0051e690     89 53 18                 MOV        dword ptr [EBX + 0x18],EDX
0051e693     81 E1 FF FF 00 00        AND        ECX,0xffff
0051e699     C1 E8 10                 SHR        EAX,0x10
0051e69c     89 4B 24                 MOV        dword ptr [EBX + 0x24],ECX
0051e69f     89 43 30                 MOV        dword ptr [EBX + 0x30],EAX
0051e6a2     A1 18 98 5E 00           MOV        EAX,[0x005e9818]
0051e6a7     83 C3 34                 ADD        EBX,0x34
0051e6aa     48                       DEC        EAX
0051e6ab     A3 18 98 5E 00           MOV        [0x005e9818],EAX
0051e6b0     8B 45 E4                 MOV        EAX,dword ptr [EBP + -0x1c]
0051e6b3     83 C6 10                 ADD        ESI,0x10
0051e6b6     48                       DEC        EAX
0051e6b7     85 C0                    TEST       EAX,EAX
0051e6b9     89 45 E4                 MOV        dword ptr [EBP + -0x1c],EAX
0051e6bc     0F 85 3B FD FF FF        JNZ        0x0051e3fd
0051e6c2     8B 45 C4                 MOV        EAX,dword ptr [EBP + -0x3c]
0051e6c5     8B 75 B4                 MOV        ESI,dword ptr [EBP + -0x4c]
0051e6c8     8B 55 14                 MOV        EDX,dword ptr [EBP + 0x14]
0051e6cb     48                       DEC        EAX
0051e6cc     83 C6 02                 ADD        ESI,0x2
0051e6cf     83 C2 04                 ADD        EDX,0x4
0051e6d2     85 C0                    TEST       EAX,EAX
0051e6d4     89 45 C4                 MOV        dword ptr [EBP + -0x3c],EAX
0051e6d7     89 75 B4                 MOV        dword ptr [EBP + -0x4c],ESI
0051e6da     89 55 14                 MOV        dword ptr [EBP + 0x14],EDX
0051e6dd     0F 85 24 EF FF FF        JNZ        0x0051d607
0051e6e3     8B C3                    MOV        EAX,EBX
0051e6e5     5F                       POP        EDI
0051e6e6     5E                       POP        ESI
0051e6e7     5B                       POP        EBX
0051e6e8     8B E5                    MOV        ESP,EBP
0051e6ea     5D                       POP        EBP
0051e6eb     C3                       RET        
0051e6ec     8B 5D 0C                 MOV        EBX,dword ptr [EBP + 0xc]
0051e6ef     5F                       POP        EDI
0051e6f0     8B C3                    MOV        EAX,EBX
0051e6f2     5E                       POP        ESI
0051e6f3     5B                       POP        EBX
0051e6f4     8B E5                    MOV        ESP,EBP
0051e6f6     5D                       POP        EBP
0051e6f7     C3                       RET        

