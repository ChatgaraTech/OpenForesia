; Function: FUN_0052a280
; Entry:    0052a280
; Body:     [[0052a280, 0052a3cc]]

; --------------------------------------------------
; Range: 0052a280 - 0052a3cc
; --------------------------------------------------
0052a280     83 EC 14                 SUB        ESP,0x14
0052a283     56                       PUSH       ESI
0052a284     57                       PUSH       EDI
0052a285     E8 B6 CE 01 00           CALL       0x00547140
0052a28a     A1 60 99 5E 00           MOV        EAX,[0x005e9960]
0052a28f     40                       INC        EAX
0052a290     A3 60 99 5E 00           MOV        [0x005e9960],EAX
0052a295     C1 E0 09                 SHL        EAX,0x9
0052a298     50                       PUSH       EAX
0052a299     E8 7E B7 01 00           CALL       0x00545a1c
0052a29e     C1 F8 06                 SAR        EAX,0x6
0052a2a1     04 80                    ADD        AL,0x80
0052a2a3     6A 00                    PUSH       0x0
0052a2a5     25 FF 00 00 00           AND        EAX,0xff
0052a2aa     C7 05 30 83 5E 00 FF FF FF FF MOV        dword ptr [0x005e8330],0xffffffff
0052a2b4     8B C8                    MOV        ECX,EAX
0052a2b6     C7 05 2C 83 5E 00 00 00 00 00 MOV        dword ptr [0x005e832c],0x0
0052a2c0     C1 E1 08                 SHL        ECX,0x8
0052a2c3     0B C8                    OR         ECX,EAX
0052a2c5     C1 E1 08                 SHL        ECX,0x8
0052a2c8     0B C8                    OR         ECX,EAX
0052a2ca     89 0D 14 98 5E 00        MOV        dword ptr [0x005e9814],ECX
0052a2d0     E8 EF B7 01 00           CALL       0x00545ac4
0052a2d5     8B 0D 00 93 5E 00        MOV        ECX,dword ptr [0x005e9300]
0052a2db     B8 80 98 5E 00           MOV        EAX,0x5e9880
0052a2e0     3B C8                    CMP        ECX,EAX
0052a2e2     75 05                    JNZ        0x0052a2e9
0052a2e4     B8 F0 98 5E 00           MOV        EAX,0x5e98f0
0052a2e9     A3 00 93 5E 00           MOV        [0x005e9300],EAX
0052a2ee     83 C0 5C                 ADD        EAX,0x5c
0052a2f1     50                       PUSH       EAX
0052a2f2     E8 41 B9 01 00           CALL       0x00545c38
0052a2f7     8B 15 00 93 5E 00        MOV        EDX,dword ptr [0x005e9300]
0052a2fd     52                       PUSH       EDX
0052a2fe     E8 2F B9 01 00           CALL       0x00545c32
0052a303     E8 38 AE 01 00           CALL       0x00545140
0052a308     8B 7C 24 24              MOV        EDI,dword ptr [ESP + 0x24]
0052a30c     8B 07                    MOV        EAX,dword ptr [EDI]
0052a30e     50                       PUSH       EAX
0052a30f     E8 18 B9 01 00           CALL       0x00545c2c
0052a314     8B 4C 24 2C              MOV        ECX,dword ptr [ESP + 0x2c]
0052a318     51                       PUSH       ECX
0052a319     E8 DC B7 01 00           CALL       0x00545afa
0052a31e     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
0052a322     85 C0                    TEST       EAX,EAX
0052a324     74 47                    JZ         0x0052a36d
0052a326     A1 38 E5 5D 00           MOV        EAX,[0x005de538]
0052a32b     33 C9                    XOR        ECX,ECX
0052a32d     25 FF 00 00 00           AND        EAX,0xff
0052a332     8D 14 80                 LEA        EDX,[EAX + EAX*0x4]
0052a335     C1 E2 03                 SHL        EDX,0x3
0052a338     2B D0                    SUB        EDX,EAX
0052a33a     33 C0                    XOR        EAX,EAX
0052a33c     8A 04 95 C8 93 5E 00     MOV        AL,byte ptr [EDX*0x4 + 0x5e93c8]
0052a343     6B C0 4C                 IMUL       EAX,EAX,0x4c
0052a346     8A 88 88 91 5E 00        MOV        CL,byte ptr [EAX + 0x5e9188]
0052a34c     C1 E1 04                 SHL        ECX,0x4
0052a34f     81 C1 20 4A 60 00        ADD        ECX,0x604a20
0052a355     51                       PUSH       ECX
0052a356     E8 C5 81 FE FF           CALL       0x00512520
0052a35b     83 C4 04                 ADD        ESP,0x4
0052a35e     E8 8D A5 FD FF           CALL       0x005048f0
0052a363     E8 C8 A6 FD FF           CALL       0x00504a30
0052a368     E8 33 B4 00 00           CALL       0x005357a0
0052a36d     8B 74 24 28              MOV        ESI,dword ptr [ESP + 0x28]
0052a371     56                       PUSH       ESI
0052a372     57                       PUSH       EDI
0052a373     8B 7C 24 28              MOV        EDI,dword ptr [ESP + 0x28]
0052a377     57                       PUSH       EDI
0052a378     E8 63 FE FF FF           CALL       0x0052a1e0
0052a37d     A1 3C 1A 57 00           MOV        EAX,[0x00571a3c]
0052a382     83 C4 0C                 ADD        ESP,0xc
0052a385     85 C0                    TEST       EAX,EAX
0052a387     74 3E                    JZ         0x0052a3c7
0052a389     8B 06                    MOV        EAX,dword ptr [ESI]
0052a38b     8B 0F                    MOV        ECX,dword ptr [EDI]
0052a38d     6A FF                    PUSH       -0x1
0052a38f     6A 00                    PUSH       0x0
0052a391     6A 00                    PUSH       0x0
0052a393     8D 54 24 14              LEA        EDX,[ESP + 0x14]
0052a397     68 10 85 56 00           PUSH       0x568510
0052a39c     52                       PUSH       EDX
0052a39d     50                       PUSH       EAX
0052a39e     51                       PUSH       ECX
0052a39f     C7 44 24 24 28 6C 56 00  MOV        dword ptr [ESP + 0x24],0x566c28
0052a3a7     66 C7 44 24 2E 64 00     MOV        word ptr [ESP + 0x2e],0x64
0052a3ae     C7 44 24 28 00 00 00 01  MOV        dword ptr [ESP + 0x28],0x1000000
0052a3b6     66 C7 44 24 2C 10 00     MOV        word ptr [ESP + 0x2c],0x10
0052a3bd     E8 5E 1D FF FF           CALL       0x0051c120
0052a3c2     83 C4 1C                 ADD        ESP,0x1c
0052a3c5     89 06                    MOV        dword ptr [ESI],EAX
0052a3c7     5F                       POP        EDI
0052a3c8     5E                       POP        ESI
0052a3c9     83 C4 14                 ADD        ESP,0x14
0052a3cc     C3                       RET        

