; Function: FUN_0052a440
; Entry:    0052a440
; Body:     [[0052a440, 0052a650]]

; --------------------------------------------------
; Range: 0052a440 - 0052a650
; --------------------------------------------------
0052a440     83 EC 3C                 SUB        ESP,0x3c
0052a443     53                       PUSH       EBX
0052a444     55                       PUSH       EBP
0052a445     56                       PUSH       ESI
0052a446     33 DB                    XOR        EBX,EBX
0052a448     57                       PUSH       EDI
0052a449     66 C7 44 24 28 80 03     MOV        word ptr [ESP + 0x28],0x380
0052a450     66 89 5C 24 2A           MOV        word ptr [ESP + 0x2a],BX
0052a455     66 C7 44 24 2C 80 00     MOV        word ptr [ESP + 0x2c],0x80
0052a45c     66 C7 44 24 2E E0 00     MOV        word ptr [ESP + 0x2e],0xe0
0052a463     66 89 5C 24 30           MOV        word ptr [ESP + 0x30],BX
0052a468     66 C7 44 24 32 FF 01     MOV        word ptr [ESP + 0x32],0x1ff
0052a46f     66 C7 44 24 34 00 01     MOV        word ptr [ESP + 0x34],0x100
0052a476     66 C7 44 24 36 01 00     MOV        word ptr [ESP + 0x36],0x1
0052a47d     E8 7E 01 EE FF           CALL       0x0040a600
0052a482     A1 C0 97 5E 00           MOV        EAX,[0x005e97c0]
0052a487     8B B8 84 00 00 00        MOV        EDI,dword ptr [EAX + 0x84]
0052a48d     8B C7                    MOV        EAX,EDI
0052a48f     39 18                    CMP        dword ptr [EAX],EBX
0052a491     74 0A                    JZ         0x0052a49d
0052a493     8B 48 08                 MOV        ECX,dword ptr [EAX + 0x8]
0052a496     83 C0 08                 ADD        EAX,0x8
0052a499     3B CB                    CMP        ECX,EBX
0052a49b     75 F6                    JNZ        0x0052a493
0052a49d     83 3D 10 E5 5D 00 03     CMP        dword ptr [0x005de510],0x3
0052a4a4     75 0E                    JNZ        0x0052a4b4
0052a4a6     8B 35 28 E5 5D 00        MOV        ESI,dword ptr [0x005de528]
0052a4ac     81 E6 FF 00 00 00        AND        ESI,0xff
0052a4b2     EB 05                    JMP        0x0052a4b9
0052a4b4     BE 0B 00 00 00           MOV        ESI,0xb
0052a4b9     8B 0D B4 87 60 00        MOV        ECX,dword ptr [0x006087b4]
0052a4bf     8B 04 F7                 MOV        EAX,dword ptr [EDI + ESI*0x8]
0052a4c2     8B 11                    MOV        EDX,dword ptr [ECX]
0052a4c4     81 C2 00 00 1B 00        ADD        EDX,0x1b0000
0052a4ca     52                       PUSH       EDX
0052a4cb     50                       PUSH       EAX
0052a4cc     E8 7F F6 FF FF           CALL       0x00529b50
0052a4d1     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0052a4d7     83 C4 08                 ADD        ESP,0x8
0052a4da     8D 4C F7 04              LEA        ECX,[EDI + ESI*0x8 + 0x4]
0052a4de     8B 02                    MOV        EAX,dword ptr [EDX]
0052a4e0     51                       PUSH       ECX
0052a4e1     05 00 00 1B 00           ADD        EAX,0x1b0000
0052a4e6     68 FC 8F 5E 00           PUSH       0x5e8ffc
0052a4eb     50                       PUSH       EAX
0052a4ec     E8 FF 7D 00 00           CALL       0x005322f0
0052a4f1     83 C4 0C                 ADD        ESP,0xc
0052a4f4     8D 4C 24 28              LEA        ECX,[ESP + 0x28]
0052a4f8     53                       PUSH       EBX
0052a4f9     53                       PUSH       EBX
0052a4fa     51                       PUSH       ECX
0052a4fb     E8 3E B7 01 00           CALL       0x00545c3e
0052a500     53                       PUSH       EBX
0052a501     8D 54 24 34              LEA        EDX,[ESP + 0x34]
0052a505     53                       PUSH       EBX
0052a506     52                       PUSH       EDX
0052a507     E8 32 B7 01 00           CALL       0x00545c3e
0052a50c     8D 44 24 10              LEA        EAX,[ESP + 0x10]
0052a510     8D 4C 24 14              LEA        ECX,[ESP + 0x14]
0052a514     50                       PUSH       EAX
0052a515     8D 54 24 20              LEA        EDX,[ESP + 0x20]
0052a519     51                       PUSH       ECX
0052a51a     52                       PUSH       EDX
0052a51b     88 5C 24 2C              MOV        byte ptr [ESP + 0x2c],BL
0052a51f     88 5C 24 2D              MOV        byte ptr [ESP + 0x2d],BL
0052a523     88 5C 24 2E              MOV        byte ptr [ESP + 0x2e],BL
0052a527     C6 44 24 2F E0           MOV        byte ptr [ESP + 0x2f],0xe0
0052a52c     88 5C 24 30              MOV        byte ptr [ESP + 0x30],BL
0052a530     C6 44 24 31 01           MOV        byte ptr [ESP + 0x31],0x1
0052a535     C6 44 24 32 3F           MOV        byte ptr [ESP + 0x32],0x3f
0052a53a     88 5C 24 33              MOV        byte ptr [ESP + 0x33],BL
0052a53e     E8 9D FC FF FF           CALL       0x0052a1e0
0052a543     83 C4 0C                 ADD        ESP,0xc
0052a546     C7 44 24 3C 00 00 00 01  MOV        dword ptr [ESP + 0x3c],0x1000000
0052a54e     83 CE FF                 OR         ESI,0xffffffff
0052a551     C7 44 24 18 96 00 00 00  MOV        dword ptr [ESP + 0x18],0x96
0052a559     BD 00 14 00 00           MOV        EBP,0x1400
0052a55e     BF 49 12 00 00           MOV        EDI,0x1249
0052a563     39 5C 24 18              CMP        dword ptr [ESP + 0x18],EBX
0052a567     75 08                    JNZ        0x0052a571
0052a569     3B F3                    CMP        ESI,EBX
0052a56b     0F 84 D8 00 00 00        JZ         0x0052a649
0052a571     8B 54 24 10              MOV        EDX,dword ptr [ESP + 0x10]
0052a575     8D 44 24 20              LEA        EAX,[ESP + 0x20]
0052a579     53                       PUSH       EBX
0052a57a     8D 4C 24 3C              LEA        ECX,[ESP + 0x3c]
0052a57e     89 44 24 3C              MOV        dword ptr [ESP + 0x3c],EAX
0052a582     8B 44 24 18              MOV        EAX,dword ptr [ESP + 0x18]
0052a586     53                       PUSH       EBX
0052a587     51                       PUSH       ECX
0052a588     52                       PUSH       EDX
0052a589     50                       PUSH       EAX
0052a58a     66 C7 44 24 54 A0 00     MOV        word ptr [ESP + 0x54],0xa0
0052a591     66 C7 44 24 56 80 00     MOV        word ptr [ESP + 0x56],0x80
0052a598     66 89 6C 24 58           MOV        word ptr [ESP + 0x58],BP
0052a59d     66 89 7C 24 5A           MOV        word ptr [ESP + 0x5a],DI
0052a5a2     66 89 5C 24 5C           MOV        word ptr [ESP + 0x5c],BX
0052a5a7     E8 74 79 FF FF           CALL       0x00521f20
0052a5ac     8B 54 24 28              MOV        EDX,dword ptr [ESP + 0x28]
0052a5b0     83 C4 14                 ADD        ESP,0x14
0052a5b3     8D 4C 24 38              LEA        ECX,[ESP + 0x38]
0052a5b7     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
0052a5bb     51                       PUSH       ECX
0052a5bc     50                       PUSH       EAX
0052a5bd     52                       PUSH       EDX
0052a5be     E8 FD 53 FF FF           CALL       0x0051f9c0
0052a5c3     83 C4 0C                 ADD        ESP,0xc
0052a5c6     89 44 24 10              MOV        dword ptr [ESP + 0x10],EAX
0052a5ca     8D 44 24 10              LEA        EAX,[ESP + 0x10]
0052a5ce     8D 4C 24 14              LEA        ECX,[ESP + 0x14]
0052a5d2     6A 01                    PUSH       0x1
0052a5d4     6A 01                    PUSH       0x1
0052a5d6     50                       PUSH       EAX
0052a5d7     8D 54 24 28              LEA        EDX,[ESP + 0x28]
0052a5db     51                       PUSH       ECX
0052a5dc     52                       PUSH       EDX
0052a5dd     E8 9E FC FF FF           CALL       0x0052a280
0052a5e2     A1 38 E5 5D 00           MOV        EAX,[0x005de538]
0052a5e7     33 D2                    XOR        EDX,EDX
0052a5e9     25 FF 00 00 00           AND        EAX,0xff
0052a5ee     83 C4 14                 ADD        ESP,0x14
0052a5f1     8D 0C 80                 LEA        ECX,[EAX + EAX*0x4]
0052a5f4     C1 E1 03                 SHL        ECX,0x3
0052a5f7     2B C8                    SUB        ECX,EAX
0052a5f9     33 C0                    XOR        EAX,EAX
0052a5fb     8A 14 8D C8 93 5E 00     MOV        DL,byte ptr [ECX*0x4 + 0x5e93c8]
0052a602     6B D2 4C                 IMUL       EDX,EDX,0x4c
0052a605     8A 82 88 91 5E 00        MOV        AL,byte ptr [EDX + 0x5e9188]
0052a60b     C1 E0 04                 SHL        EAX,0x4
0052a60e     05 20 4A 60 00           ADD        EAX,0x604a20
0052a613     66 8B 00                 MOV        AX,word ptr [EAX]
0052a616     66 25 08 80              AND        AX,0x8008
0052a61a     75 07                    JNZ        0x0052a623
0052a61c     83 FE FF                 CMP        ESI,-0x1
0052a61f     75 02                    JNZ        0x0052a623
0052a621     33 F6                    XOR        ESI,ESI
0052a623     66 3B C3                 CMP        AX,BX
0052a626     75 0A                    JNZ        0x0052a632
0052a628     83 FE 01                 CMP        ESI,0x1
0052a62b     74 1C                    JZ         0x0052a649
0052a62d     66 3B C3                 CMP        AX,BX
0052a630     74 0E                    JZ         0x0052a640
0052a632     3B F3                    CMP        ESI,EBX
0052a634     75 0A                    JNZ        0x0052a640
0052a636     BE 01 00 00 00           MOV        ESI,0x1
0052a63b     E8 40 35 01 00           CALL       0x0053db80
0052a640     FF 4C 24 18              DEC        dword ptr [ESP + 0x18]
0052a644     E9 1A FF FF FF           JMP        0x0052a563
0052a649     5F                       POP        EDI
0052a64a     5E                       POP        ESI
0052a64b     5D                       POP        EBP
0052a64c     5B                       POP        EBX
0052a64d     83 C4 3C                 ADD        ESP,0x3c
0052a650     C3                       RET        

