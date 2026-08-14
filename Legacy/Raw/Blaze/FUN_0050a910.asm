; Function: FUN_0050a910
; Entry:    0050a910
; Body:     [[0050a910, 0050be40]]

; --------------------------------------------------
; Range: 0050a910 - 0050be40
; --------------------------------------------------
0050a910     A1 48 2A 56 00           MOV        EAX,[0x00562a48]
0050a915     83 EC 1C                 SUB        ESP,0x1c
0050a918     83 CA FF                 OR         EDX,0xffffffff
0050a91b     53                       PUSH       EBX
0050a91c     55                       PUSH       EBP
0050a91d     56                       PUSH       ESI
0050a91e     57                       PUSH       EDI
0050a91f     33 FF                    XOR        EDI,EDI
0050a921     3B C7                    CMP        EAX,EDI
0050a923     0F 84 9C 00 00 00        JZ         0x0050a9c5
0050a929     B9 18 00 00 00           MOV        ECX,0x18
0050a92e     B8 88 00 00 00           MOV        EAX,0x88
0050a933     89 3D 48 2A 56 00        MOV        dword ptr [0x00562a48],EDI
0050a939     66 C7 05 1C 35 57 00 A0 00 MOV        word ptr [0x0057351c],0xa0
0050a942     66 89 0D 1E 35 57 00     MOV        word ptr [0x0057351e],CX
0050a949     66 C7 05 20 35 57 00 68 00 MOV        word ptr [0x00573520],0x68
0050a952     66 A3 22 35 57 00        MOV        [0x00573522],AX
0050a958     C6 05 3B 35 57 00 00     MOV        byte ptr [0x0057353b],0x0
0050a95f     66 C7 05 E4 3D 57 00 28 00 MOV        word ptr [0x00573de4],0x28
0050a968     66 89 0D E6 3D 57 00     MOV        word ptr [0x00573de6],CX
0050a96f     66 C7 05 E8 3D 57 00 78 00 MOV        word ptr [0x00573de8],0x78
0050a978     66 A3 EA 3D 57 00        MOV        [0x00573dea],AX
0050a97e     C6 05 F4 3D 57 00 03     MOV        byte ptr [0x00573df4],0x3
0050a985     C6 05 F2 3D 57 00 1A     MOV        byte ptr [0x00573df2],0x1a
0050a98c     C6 05 F3 3D 57 00 06     MOV        byte ptr [0x00573df3],0x6
0050a993     C6 05 02 3E 57 00 07     MOV        byte ptr [0x00573e02],0x7
0050a99a     C6 05 03 3E 57 00 01     MOV        byte ptr [0x00573e03],0x1
0050a9a1     89 3D 50 2A 56 00        MOV        dword ptr [0x00562a50],EDI
0050a9a7     89 15 F0 8D 57 00        MOV        dword ptr [0x00578df0],EDX
0050a9ad     89 15 68 2B 56 00        MOV        dword ptr [0x00562b68],EDX
0050a9b3     89 3D 0C 8E 57 00        MOV        dword ptr [0x00578e0c],EDI
0050a9b9     89 3D 10 8E 57 00        MOV        dword ptr [0x00578e10],EDI
0050a9bf     89 3D 14 8E 57 00        MOV        dword ptr [0x00578e14],EDI
0050a9c5     8B 0D 68 2B 56 00        MOV        ECX,dword ptr [0x00562b68]
0050a9cb     A1 18 8E 57 00           MOV        EAX,[0x00578e18]
0050a9d0     3B CF                    CMP        ECX,EDI
0050a9d2     8B 4C 24 3C              MOV        ECX,dword ptr [ESP + 0x3c]
0050a9d6     8B F0                    MOV        ESI,EAX
0050a9d8     7D 31                    JGE        0x0050aa0b
0050a9da     F6 41 06 80              TEST       byte ptr [ECX + 0x6],0x80
0050a9de     74 06                    JZ         0x0050a9e6
0050a9e0     48                       DEC        EAX
0050a9e1     A3 18 8E 57 00           MOV        [0x00578e18],EAX
0050a9e6     F6 41 06 20              TEST       byte ptr [ECX + 0x6],0x20
0050a9ea     74 06                    JZ         0x0050a9f2
0050a9ec     40                       INC        EAX
0050a9ed     A3 18 8E 57 00           MOV        [0x00578e18],EAX
0050a9f2     57                       PUSH       EDI
0050a9f3     68 28 07 56 00           PUSH       0x560728
0050a9f8     57                       PUSH       EDI
0050a9f9     68 40 2B 57 00           PUSH       0x572b40
0050a9fe     E8 4D 99 03 00           CALL       0x00544350
0050aa03     A1 18 8E 57 00           MOV        EAX,[0x00578e18]
0050aa08     83 C4 10                 ADD        ESP,0x10
0050aa0b     BA 00 80 00 00           MOV        EDX,0x8000
0050aa10     33 C9                    XOR        ECX,ECX
0050aa12     66 39 15 0A 4B 60 00     CMP        word ptr [0x00604b0a],DX
0050aa19     89 0D 20 8E 57 00        MOV        dword ptr [0x00578e20],ECX
0050aa1f     75 0B                    JNZ        0x0050aa2c
0050aa21     B9 01 00 00 00           MOV        ECX,0x1
0050aa26     89 0D 20 8E 57 00        MOV        dword ptr [0x00578e20],ECX
0050aa2c     66 39 15 2E 4B 60 00     CMP        word ptr [0x00604b2e],DX
0050aa33     75 09                    JNZ        0x0050aa3e
0050aa35     83 C1 02                 ADD        ECX,0x2
0050aa38     89 0D 20 8E 57 00        MOV        dword ptr [0x00578e20],ECX
0050aa3e     3B C7                    CMP        EAX,EDI
0050aa40     7D 10                    JGE        0x0050aa52
0050aa42     8B 04 8D 30 2B 56 00     MOV        EAX,dword ptr [ECX*0x4 + 0x562b30]
0050aa49     0F BE 00                 MOVSX      EAX,byte ptr [EAX]
0050aa4c     48                       DEC        EAX
0050aa4d     A3 18 8E 57 00           MOV        [0x00578e18],EAX
0050aa52     8B 0C 8D 30 2B 56 00     MOV        ECX,dword ptr [ECX*0x4 + 0x562b30]
0050aa59     0F BE 11                 MOVSX      EDX,byte ptr [ECX]
0050aa5c     3B C2                    CMP        EAX,EDX
0050aa5e     7C 07                    JL         0x0050aa67
0050aa60     33 C0                    XOR        EAX,EAX
0050aa62     A3 18 8E 57 00           MOV        [0x00578e18],EAX
0050aa67     3B F0                    CMP        ESI,EAX
0050aa69     74 0A                    JZ         0x0050aa75
0050aa6b     E8 F0 30 03 00           CALL       0x0053db60
0050aa70     A1 18 8E 57 00           MOV        EAX,[0x00578e18]
0050aa75     39 3D 68 2B 56 00        CMP        dword ptr [0x00562b68],EDI
0050aa7b     7D 29                    JGE        0x0050aaa6
0050aa7d     A3 68 2B 56 00           MOV        [0x00562b68],EAX
0050aa82     89 3D 3C 33 57 00        MOV        dword ptr [0x0057333c],EDI
0050aa88     89 3D E0 8D 57 00        MOV        dword ptr [0x00578de0],EDI
0050aa8e     89 3D 60 49 60 00        MOV        dword ptr [0x00604960],EDI
0050aa94     89 3D 0C 8E 57 00        MOV        dword ptr [0x00578e0c],EDI
0050aa9a     66 89 3D 8A 49 60 00     MOV        word ptr [0x0060498a],DI
0050aaa1     E8 DA 30 03 00           CALL       0x0053db80
0050aaa6     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050aaab     8B 0D 10 8E 57 00        MOV        ECX,dword ptr [0x00578e10]
0050aab1     3B C8                    CMP        ECX,EAX
0050aab3     74 0F                    JZ         0x0050aac4
0050aab5     A3 10 8E 57 00           MOV        [0x00578e10],EAX
0050aaba     C7 05 14 8E 57 00 01 00 00 00 MOV        dword ptr [0x00578e14],0x1
0050aac4     A1 68 2B 56 00           MOV        EAX,[0x00562b68]
0050aac9     8B 6C 24 30              MOV        EBP,dword ptr [ESP + 0x30]
0050aacd     3B C7                    CMP        EAX,EDI
0050aacf     0F 8C 2F 0F 00 00        JL         0x0050ba04
0050aad5     39 3D 4C 2A 56 00        CMP        dword ptr [0x00562a4c],EDI
0050aadb     0F 85 3A 0C 00 00        JNZ        0x0050b71b
0050aae1     A1 3C 33 57 00           MOV        EAX,[0x0057333c]
0050aae6     2B C7                    SUB        EAX,EDI
0050aae8     0F 84 A1 00 00 00        JZ         0x0050ab8f
0050aaee     48                       DEC        EAX
0050aaef     74 6B                    JZ         0x0050ab5c
0050aaf1     48                       DEC        EAX
0050aaf2     0F 85 97 00 00 00        JNZ        0x0050ab8f
0050aaf8     A1 80 49 60 00           MOV        EAX,[0x00604980]
0050aafd     25 FF 00 00 00           AND        EAX,0xff
0050ab02     8B C8                    MOV        ECX,EAX
0050ab04     83 E0 04                 AND        EAX,0x4
0050ab07     83 E1 03                 AND        ECX,0x3
0050ab0a     8D 14 81                 LEA        EDX,[ECX + EAX*0x4]
0050ab0d     52                       PUSH       EDX
0050ab0e     E8 45 AF 03 00           CALL       0x00545a58
0050ab13     85 C0                    TEST       EAX,EAX
0050ab15     75 1F                    JNZ        0x0050ab36
0050ab17     A1 80 49 60 00           MOV        EAX,[0x00604980]
0050ab1c     25 FF 00 00 00           AND        EAX,0xff
0050ab21     8B C8                    MOV        ECX,EAX
0050ab23     83 E0 04                 AND        EAX,0x4
0050ab26     83 E1 03                 AND        ECX,0x3
0050ab29     8D 14 81                 LEA        EDX,[ECX + EAX*0x4]
0050ab2c     52                       PUSH       EDX
0050ab2d     E8 26 AF 03 00           CALL       0x00545a58
0050ab32     85 C0                    TEST       EAX,EAX
0050ab34     74 E1                    JZ         0x0050ab17
0050ab36     A1 80 49 60 00           MOV        EAX,[0x00604980]
0050ab3b     57                       PUSH       EDI
0050ab3c     25 FF 00 00 00           AND        EAX,0xff
0050ab41     8B C8                    MOV        ECX,EAX
0050ab43     83 E0 04                 AND        EAX,0x4
0050ab46     83 E1 03                 AND        ECX,0x3
0050ab49     8D 14 81                 LEA        EDX,[ECX + EAX*0x4]
0050ab4c     52                       PUSH       EDX
0050ab4d     E8 CE 9C FF FF           CALL       0x00504820
0050ab52     83 C4 08                 ADD        ESP,0x8
0050ab55     A3 E0 8D 57 00           MOV        [0x00578de0],EAX
0050ab5a     EB 33                    JMP        0x0050ab8f
0050ab5c     A1 80 49 60 00           MOV        EAX,[0x00604980]
0050ab61     57                       PUSH       EDI
0050ab62     25 FF 00 00 00           AND        EAX,0xff
0050ab67     8B C8                    MOV        ECX,EAX
0050ab69     83 E0 04                 AND        EAX,0x4
0050ab6c     83 E1 03                 AND        ECX,0x3
0050ab6f     8D 14 81                 LEA        EDX,[ECX + EAX*0x4]
0050ab72     52                       PUSH       EDX
0050ab73     E8 A8 9C FF FF           CALL       0x00504820
0050ab78     83 C4 08                 ADD        ESP,0x8
0050ab7b     83 F8 FE                 CMP        EAX,-0x2
0050ab7e     A3 E0 8D 57 00           MOV        [0x00578de0],EAX
0050ab83     7F 0A                    JG         0x0050ab8f
0050ab85     C7 05 0C 8E 57 00 FC FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffc
0050ab8f     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050ab94     83 C0 05                 ADD        EAX,0x5
0050ab97     83 F8 68                 CMP        EAX,0x68
0050ab9a     0F 87 7B 0B 00 00        JA         0x0050b71b
0050aba0     8A 0D EF 0F 56 00        MOV        CL,byte ptr [0x00560fef]
0050aba6     8A 15 9F 90 5E 00        MOV        DL,byte ptr [0x005e909f]
0050abac     8B 74 24 3C              MOV        ESI,dword ptr [ESP + 0x3c]
0050abb0     BF 00 40 00 00           MOV        EDI,0x4000
0050abb5     33 DB                    XOR        EBX,EBX
0050abb7     8A 98 A8 BE 50 00        MOV        BL,byte ptr [EAX + 0x50bea8]
0050abbd     FF 24 9D 44 BE 50 00     JMP        dword ptr [EBX*0x4 + 0x50be44]
0050abc4     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050abc8     0F 85 C5 09 00 00        JNZ        0x0050b593
0050abce     F6 C1 04                 TEST       CL,0x4
0050abd1     0F 85 44 0B 00 00        JNZ        0x0050b71b
0050abd7     84 D2                    TEST       DL,DL
0050abd9     0F 84 D3 09 00 00        JZ         0x0050b5b2
0050abdf     C7 05 0C 8E 57 00 0A 00 00 00 MOV        dword ptr [0x00578e0c],0xa
0050abe9     E9 65 01 00 00           JMP        0x0050ad53
0050abee     A1 14 8E 57 00           MOV        EAX,[0x00578e14]
0050abf3     33 DB                    XOR        EBX,EBX
0050abf5     3B C3                    CMP        EAX,EBX
0050abf7     0F 85 C8 09 00 00        JNZ        0x0050b5c5
0050abfd     8A 0D 80 49 60 00        MOV        CL,byte ptr [0x00604980]
0050ac03     6A 01                    PUSH       0x1
0050ac05     83 E1 04                 AND        ECX,0x4
0050ac08     C1 E1 02                 SHL        ECX,0x2
0050ac0b     51                       PUSH       ECX
0050ac0c     E8 AF 9A FF FF           CALL       0x005046c0
0050ac11     83 C4 08                 ADD        ESP,0x8
0050ac14     E8 17 9B FF FF           CALL       0x00504730
0050ac19     A1 84 49 60 00           MOV        EAX,[0x00604984]
0050ac1e     25 FF 00 00 00           AND        EAX,0xff
0050ac23     8D 14 80                 LEA        EDX,[EAX + EAX*0x4]
0050ac26     A1 BC 49 60 00           MOV        EAX,[0x006049bc]
0050ac2b     8D 0C D0                 LEA        ECX,[EAX + EDX*0x8]
0050ac2e     51                       PUSH       ECX
0050ac2f     E8 CC 7C 00 00           CALL       0x00512900
0050ac34     83 C4 04                 ADD        ESP,0x4
0050ac37     68 80 49 60 00           PUSH       0x604980
0050ac3c     E8 9F 7C 00 00           CALL       0x005128e0
0050ac41     83 C4 04                 ADD        ESP,0x4
0050ac44     BE 0A 00 00 00           MOV        ESI,0xa
0050ac49     33 C0                    XOR        EAX,EAX
0050ac4b     3B C3                    CMP        EAX,EBX
0050ac4d     75 15                    JNZ        0x0050ac64
0050ac4f     68 40 49 60 00           PUSH       0x604940
0050ac54     E8 41 AE 03 00           CALL       0x00545a9a
0050ac59     4E                       DEC        ESI
0050ac5a     75 EF                    JNZ        0x0050ac4b
0050ac5c     3B C3                    CMP        EAX,EBX
0050ac5e     0F 84 1D 0A 00 00        JZ         0x0050b681
0050ac64     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
0050ac6a     53                       PUSH       EBX
0050ac6b     68 28 07 56 00           PUSH       0x560728
0050ac70     53                       PUSH       EBX
0050ac71     8B 42 70                 MOV        EAX,dword ptr [EDX + 0x70]
0050ac74     8B 88 98 01 00 00        MOV        ECX,dword ptr [EAX + 0x198]
0050ac7a     51                       PUSH       ECX
0050ac7b     E9 AF 00 00 00           JMP        0x0050ad2f
0050ac80     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050ac84     0F 85 E8 09 00 00        JNZ        0x0050b672
0050ac8a     F6 C1 04                 TEST       CL,0x4
0050ac8d     0F 85 88 0A 00 00        JNZ        0x0050b71b
0050ac93     84 D2                    TEST       DL,DL
0050ac95     0F 85 D7 09 00 00        JNZ        0x0050b672
0050ac9b     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
0050aca1     33 DB                    XOR        EBX,EBX
0050aca3     53                       PUSH       EBX
0050aca4     68 28 07 56 00           PUSH       0x560728
0050aca9     8B 42 70                 MOV        EAX,dword ptr [EDX + 0x70]
0050acac     53                       PUSH       EBX
0050acad     8B 88 CC 01 00 00        MOV        ECX,dword ptr [EAX + 0x1cc]
0050acb3     51                       PUSH       ECX
0050acb4     E8 97 96 03 00           CALL       0x00544350
0050acb9     8A 15 80 49 60 00        MOV        DL,byte ptr [0x00604980]
0050acbf     83 C4 10                 ADD        ESP,0x10
0050acc2     83 E2 04                 AND        EDX,0x4
0050acc5     C1 E2 02                 SHL        EDX,0x2
0050acc8     6A 01                    PUSH       0x1
0050acca     52                       PUSH       EDX
0050accb     E8 F0 99 FF FF           CALL       0x005046c0
0050acd0     83 C4 08                 ADD        ESP,0x8
0050acd3     E8 58 9A FF FF           CALL       0x00504730
0050acd8     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050acdd     05 D0 1F 00 00           ADD        EAX,0x1fd0
0050ace2     50                       PUSH       EAX
0050ace3     E8 18 7C 00 00           CALL       0x00512900
0050ace8     83 C4 04                 ADD        ESP,0x4
0050aceb     68 80 49 60 00           PUSH       0x604980
0050acf0     E8 EB 7B 00 00           CALL       0x005128e0
0050acf5     83 C4 04                 ADD        ESP,0x4
0050acf8     BE 0A 00 00 00           MOV        ESI,0xa
0050acfd     33 C0                    XOR        EAX,EAX
0050acff     3B C3                    CMP        EAX,EBX
0050ad01     75 15                    JNZ        0x0050ad18
0050ad03     68 40 49 60 00           PUSH       0x604940
0050ad08     E8 8D AD 03 00           CALL       0x00545a9a
0050ad0d     4E                       DEC        ESI
0050ad0e     75 EF                    JNZ        0x0050acff
0050ad10     3B C3                    CMP        EAX,EBX
0050ad12     0F 84 69 09 00 00        JZ         0x0050b681
0050ad18     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050ad1e     53                       PUSH       EBX
0050ad1f     68 28 07 56 00           PUSH       0x560728
0050ad24     53                       PUSH       EBX
0050ad25     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050ad28     8B 82 98 01 00 00        MOV        EAX,dword ptr [EDX + 0x198]
0050ad2e     50                       PUSH       EAX
0050ad2f     E8 1C 96 03 00           CALL       0x00544350
0050ad34     8A 0D EF 0F 56 00        MOV        CL,byte ptr [0x00560fef]
0050ad3a     8A 15 9F 90 5E 00        MOV        DL,byte ptr [0x005e909f]
0050ad40     8B 74 24 4C              MOV        ESI,dword ptr [ESP + 0x4c]
0050ad44     83 C4 10                 ADD        ESP,0x10
0050ad47     89 1D 3C 33 57 00        MOV        dword ptr [0x0057333c],EBX
0050ad4d     89 1D 0C 8E 57 00        MOV        dword ptr [0x00578e0c],EBX
0050ad53     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050ad58     83 C0 05                 ADD        EAX,0x5
0050ad5b     83 F8 68                 CMP        EAX,0x68
0050ad5e     0F 86 51 FE FF FF        JBE        0x0050abb5
0050ad64     E9 B2 09 00 00           JMP        0x0050b71b
0050ad69     8A 0D 80 49 60 00        MOV        CL,byte ptr [0x00604980]
0050ad6f     6A 01                    PUSH       0x1
0050ad71     83 E1 04                 AND        ECX,0x4
0050ad74     C1 E1 02                 SHL        ECX,0x2
0050ad77     51                       PUSH       ECX
0050ad78     E8 43 99 FF FF           CALL       0x005046c0
0050ad7d     83 C4 08                 ADD        ESP,0x8
0050ad80     E8 AB 99 FF FF           CALL       0x00504730
0050ad85     8A 0D EF 0F 56 00        MOV        CL,byte ptr [0x00560fef]
0050ad8b     80 E1 F9                 AND        CL,0xf9
0050ad8e     C6 05 63 07 56 00 00     MOV        byte ptr [0x00560763],0x0
0050ad95     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
0050ad9b     C6 05 5B 07 56 00 00     MOV        byte ptr [0x0056075b],0x0
0050ada2     C7 05 68 2B 56 00 FF FF FF FF MOV        dword ptr [0x00562b68],0xffffffff
0050adac     E8 EF 2D 03 00           CALL       0x0053dba0
0050adb1     E9 65 09 00 00           JMP        0x0050b71b
0050adb6     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050adba     74 0F                    JZ         0x0050adcb
0050adbc     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050adc6     E9 50 09 00 00           JMP        0x0050b71b
0050adcb     83 3D E0 8D 57 00 FD     CMP        dword ptr [0x00578de0],-0x3
0050add2     75 11                    JNZ        0x0050ade5
0050add4     33 C0                    XOR        EAX,EAX
0050add6     A3 3C 33 57 00           MOV        [0x0057333c],EAX
0050addb     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050ade0     E9 36 09 00 00           JMP        0x0050b71b
0050ade5     F6 C1 02                 TEST       CL,0x2
0050ade8     0F 85 2D 09 00 00        JNZ        0x0050b71b
0050adee     8A 15 80 49 60 00        MOV        DL,byte ptr [0x00604980]
0050adf4     6A 01                    PUSH       0x1
0050adf6     83 E2 04                 AND        EDX,0x4
0050adf9     C1 E2 02                 SHL        EDX,0x2
0050adfc     52                       PUSH       EDX
0050adfd     E8 BE 98 FF FF           CALL       0x005046c0
0050ae02     83 C4 08                 ADD        ESP,0x8
0050ae05     E8 26 99 FF FF           CALL       0x00504730
0050ae0a     E9 02 09 00 00           JMP        0x0050b711
0050ae0f     F6 C1 02                 TEST       CL,0x2
0050ae12     0F 85 03 09 00 00        JNZ        0x0050b71b
0050ae18     E9 F4 08 00 00           JMP        0x0050b711
0050ae1d     A1 D4 0F 56 00           MOV        EAX,[0x00560fd4]
0050ae22     85 C0                    TEST       EAX,EAX
0050ae24     75 2A                    JNZ        0x0050ae50
0050ae26     A1 20 8E 57 00           MOV        EAX,[0x00578e20]
0050ae2b     8B 15 18 8E 57 00        MOV        EDX,dword ptr [0x00578e18]
0050ae31     8B 0C 85 F8 2A 56 00     MOV        ECX,dword ptr [EAX*0x4 + 0x562af8]
0050ae38     8A 04 11                 MOV        AL,byte ptr [ECX + EDX*0x1]
0050ae3b     A2 80 49 60 00           MOV        [0x00604980],AL
0050ae40     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050ae45     40                       INC        EAX
0050ae46     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050ae4b     E9 CB 08 00 00           JMP        0x0050b71b
0050ae50     80 E1 FD                 AND        CL,0xfd
0050ae53     C6 05 5B 07 56 00 00     MOV        byte ptr [0x0056075b],0x0
0050ae5a     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
0050ae60     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050ae66     C7 05 84 49 60 00 00 00 00 00 MOV        dword ptr [0x00604984],0x0
0050ae70     81 C1 D0 1F 00 00        ADD        ECX,0x1fd0
0050ae76     51                       PUSH       ECX
0050ae77     E8 84 7A 00 00           CALL       0x00512900
0050ae7c     83 C4 04                 ADD        ESP,0x4
0050ae7f     68 80 49 60 00           PUSH       0x604980
0050ae84     E8 57 7A 00 00           CALL       0x005128e0
0050ae89     83 C4 04                 ADD        ESP,0x4
0050ae8c     6A 01                    PUSH       0x1
0050ae8e     68 40 49 60 00           PUSH       0x604940
0050ae93     E8 A8 9C FF FF           CALL       0x00504b40
0050ae98     83 C4 08                 ADD        ESP,0x8
0050ae9b     8B F0                    MOV        ESI,EAX
0050ae9d     56                       PUSH       ESI
0050ae9e     E8 CD 9C FF FF           CALL       0x00504b70
0050aea3     83 C4 04                 ADD        ESP,0x4
0050aea6     85 F6                    TEST       ESI,ESI
0050aea8     6A 00                    PUSH       0x0
0050aeaa     68 28 07 56 00           PUSH       0x560728
0050aeaf     6A 00                    PUSH       0x0
0050aeb1     7D 37                    JGE        0x0050aeea
0050aeb3     68 40 2B 57 00           PUSH       0x572b40
0050aeb8     E8 93 94 03 00           CALL       0x00544350
0050aebd     83 C4 10                 ADD        ESP,0x10
0050aec0     68 00 02 03 00           PUSH       0x30200
0050aec5     68 40 49 60 00           PUSH       0x604940
0050aeca     E8 71 9C FF FF           CALL       0x00504b40
0050aecf     83 C4 08                 ADD        ESP,0x8
0050aed2     50                       PUSH       EAX
0050aed3     E8 98 9C FF FF           CALL       0x00504b70
0050aed8     83 C4 04                 ADD        ESP,0x4
0050aedb     C7 05 0C 8E 57 00 06 00 00 00 MOV        dword ptr [0x00578e0c],0x6
0050aee5     E9 31 08 00 00           JMP        0x0050b71b
0050aeea     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
0050aef0     8B 42 70                 MOV        EAX,dword ptr [EDX + 0x70]
0050aef3     8B 88 AC 01 00 00        MOV        ECX,dword ptr [EAX + 0x1ac]
0050aef9     51                       PUSH       ECX
0050aefa     E8 51 94 03 00           CALL       0x00544350
0050aeff     A1 80 49 60 00           MOV        EAX,[0x00604980]
0050af04     83 C4 10                 ADD        ESP,0x10
0050af07     25 FF 00 00 00           AND        EAX,0xff
0050af0c     8B D0                    MOV        EDX,EAX
0050af0e     83 E0 04                 AND        EAX,0x4
0050af11     83 E2 03                 AND        EDX,0x3
0050af14     8D 04 82                 LEA        EAX,[EDX + EAX*0x4]
0050af17     50                       PUSH       EAX
0050af18     E8 73 97 FF FF           CALL       0x00504690
0050af1d     83 C4 04                 ADD        ESP,0x4
0050af20     C7 05 3C 33 57 00 01 00 00 00 MOV        dword ptr [0x0057333c],0x1
0050af2a     C7 05 0C 8E 57 00 0E 00 00 00 MOV        dword ptr [0x00578e0c],0xe
0050af34     E9 E2 07 00 00           JMP        0x0050b71b
0050af39     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050af3d     74 0F                    JZ         0x0050af4e
0050af3f     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050af49     E9 CD 07 00 00           JMP        0x0050b71b
0050af4e     F6 C1 02                 TEST       CL,0x2
0050af51     0F 85 C4 07 00 00        JNZ        0x0050b71b
0050af57     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050af5d     6A 00                    PUSH       0x0
0050af5f     68 28 07 56 00           PUSH       0x560728
0050af64     6A 00                    PUSH       0x0
0050af66     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050af69     8B 82 B0 01 00 00        MOV        EAX,dword ptr [EDX + 0x1b0]
0050af6f     50                       PUSH       EAX
0050af70     E9 53 07 00 00           JMP        0x0050b6c8
0050af75     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050af79     74 0F                    JZ         0x0050af8a
0050af7b     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050af85     E9 91 07 00 00           JMP        0x0050b71b
0050af8a     F6 C1 02                 TEST       CL,0x2
0050af8d     0F 85 88 07 00 00        JNZ        0x0050b71b
0050af93     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050af99     6A 00                    PUSH       0x0
0050af9b     68 28 07 56 00           PUSH       0x560728
0050afa0     6A 00                    PUSH       0x0
0050afa2     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050afa5     8B 82 90 01 00 00        MOV        EAX,dword ptr [EDX + 0x190]
0050afab     50                       PUSH       EAX
0050afac     E9 17 07 00 00           JMP        0x0050b6c8
0050afb1     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050afb5     74 0F                    JZ         0x0050afc6
0050afb7     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050afc1     E9 55 07 00 00           JMP        0x0050b71b
0050afc6     F6 C1 04                 TEST       CL,0x4
0050afc9     0F 85 4C 07 00 00        JNZ        0x0050b71b
0050afcf     8A 0D 80 49 60 00        MOV        CL,byte ptr [0x00604980]
0050afd5     6A 01                    PUSH       0x1
0050afd7     83 E1 04                 AND        ECX,0x4
0050afda     C1 E1 02                 SHL        ECX,0x2
0050afdd     51                       PUSH       ECX
0050afde     E8 DD 96 FF FF           CALL       0x005046c0
0050afe3     83 C4 08                 ADD        ESP,0x8
0050afe6     E8 45 97 FF FF           CALL       0x00504730
0050afeb     A0 9F 90 5E 00           MOV        AL,[0x005e909f]
0050aff0     84 C0                    TEST       AL,AL
0050aff2     0F 85 19 07 00 00        JNZ        0x0050b711
0050aff8     6A 00                    PUSH       0x0
0050affa     68 28 07 56 00           PUSH       0x560728
0050afff     6A 00                    PUSH       0x0
0050b001     68 40 2B 57 00           PUSH       0x572b40
0050b006     E9 BD 06 00 00           JMP        0x0050b6c8
0050b00b     8B 0D 14 8E 57 00        MOV        ECX,dword ptr [0x00578e14]
0050b011     33 C0                    XOR        EAX,EAX
0050b013     3B C8                    CMP        ECX,EAX
0050b015     74 0F                    JZ         0x0050b026
0050b017     A3 14 8E 57 00           MOV        [0x00578e14],EAX
0050b01c     A3 3C 33 57 00           MOV        [0x0057333c],EAX
0050b021     E9 F5 06 00 00           JMP        0x0050b71b
0050b026     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050b02b     A3 3C 33 57 00           MOV        [0x0057333c],EAX
0050b030     E9 E6 06 00 00           JMP        0x0050b71b
0050b035     A1 14 8E 57 00           MOV        EAX,[0x00578e14]
0050b03a     33 F6                    XOR        ESI,ESI
0050b03c     3B C6                    CMP        EAX,ESI
0050b03e     74 0B                    JZ         0x0050b04b
0050b040     89 35 14 8E 57 00        MOV        dword ptr [0x00578e14],ESI
0050b046     E9 D0 06 00 00           JMP        0x0050b71b
0050b04b     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b051     C6 02 53                 MOV        byte ptr [EDX],0x53
0050b054     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b059     C6 40 01 43              MOV        byte ptr [EAX + 0x1],0x43
0050b05d     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050b063     C6 41 02 11              MOV        byte ptr [ECX + 0x2],0x11
0050b067     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b06d     C6 42 03 03              MOV        byte ptr [EDX + 0x3],0x3
0050b071     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050b077     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b07c     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050b07f     8D 78 04                 LEA        EDI,[EAX + 0x4]
0050b082     8B 8A 34 01 00 00        MOV        ECX,dword ptr [EDX + 0x134]
0050b088     8A 01                    MOV        AL,byte ptr [ECX]
0050b08a     84 C0                    TEST       AL,AL
0050b08c     74 0B                    JZ         0x0050b099
0050b08e     46                       INC        ESI
0050b08f     41                       INC        ECX
0050b090     88 07                    MOV        byte ptr [EDI],AL
0050b092     47                       INC        EDI
0050b093     8A 01                    MOV        AL,byte ptr [ECX]
0050b095     84 C0                    TEST       AL,AL
0050b097     75 F5                    JNZ        0x0050b08e
0050b099     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b09e     C7 44 24 3C 00 00 00 00  MOV        dword ptr [ESP + 0x3c],0x0
0050b0a6     8D 98 00 01 00 00        LEA        EBX,[EAX + 0x100]
0050b0ac     8A 80 00 01 00 00        MOV        AL,byte ptr [EAX + 0x100]
0050b0b2     84 C0                    TEST       AL,AL
0050b0b4     74 27                    JZ         0x0050b0dd
0050b0b6     83 7C 24 3C 10           CMP        dword ptr [ESP + 0x3c],0x10
0050b0bb     7D 20                    JGE        0x0050b0dd
0050b0bd     50                       PUSH       EAX
0050b0be     E8 4D F7 FF FF           CALL       0x0050a810
0050b0c3     8B 4C 24 40              MOV        ECX,dword ptr [ESP + 0x40]
0050b0c7     83 C4 04                 ADD        ESP,0x4
0050b0ca     46                       INC        ESI
0050b0cb     41                       INC        ECX
0050b0cc     43                       INC        EBX
0050b0cd     66 89 07                 MOV        word ptr [EDI],AX
0050b0d0     83 C7 02                 ADD        EDI,0x2
0050b0d3     89 4C 24 3C              MOV        dword ptr [ESP + 0x3c],ECX
0050b0d7     8A 03                    MOV        AL,byte ptr [EBX]
0050b0d9     84 C0                    TEST       AL,AL
0050b0db     75 D9                    JNZ        0x0050b0b6
0050b0dd     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050b0e3     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b0e8     8A 91 50 01 00 00        MOV        DL,byte ptr [ECX + 0x150]
0050b0ee     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b0f1     83 E2 07                 AND        EDX,0x7
0050b0f4     8B 8C 91 38 01 00 00     MOV        ECX,dword ptr [ECX + EDX*0x4 + 0x138]
0050b0fb     8A 01                    MOV        AL,byte ptr [ECX]
0050b0fd     84 C0                    TEST       AL,AL
0050b0ff     74 0B                    JZ         0x0050b10c
0050b101     46                       INC        ESI
0050b102     41                       INC        ECX
0050b103     88 07                    MOV        byte ptr [EDI],AL
0050b105     47                       INC        EDI
0050b106     8A 01                    MOV        AL,byte ptr [ECX]
0050b108     84 C0                    TEST       AL,AL
0050b10a     75 F5                    JNZ        0x0050b101
0050b10c     83 FE 40                 CMP        ESI,0x40
0050b10f     7D 17                    JGE        0x0050b128
0050b111     B9 40 00 00 00           MOV        ECX,0x40
0050b116     33 C0                    XOR        EAX,EAX
0050b118     2B CE                    SUB        ECX,ESI
0050b11a     8B D1                    MOV        EDX,ECX
0050b11c     C1 E9 02                 SHR        ECX,0x2
0050b11f     F3 AB                    STOSD.REP  ES:EDI
0050b121     8B CA                    MOV        ECX,EDX
0050b123     83 E1 03                 AND        ECX,0x3
0050b126     F3 AA                    STOSB.REP  ES:EDI
0050b128     33 C0                    XOR        EAX,EAX
0050b12a     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050b130     40                       INC        EAX
0050b131     83 F8 1C                 CMP        EAX,0x1c
0050b134     C6 44 08 43 00           MOV        byte ptr [EAX + ECX*0x1 + 0x43],0x0
0050b139     7C EF                    JL         0x0050b12a
0050b13b     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b141     8A 0D 16 8F 5E 00        MOV        CL,byte ptr [0x005e8f16]
0050b147     83 E1 3F                 AND        ECX,0x3f
0050b14a     BE 10 00 00 00           MOV        ESI,0x10
0050b14f     66 0F B6 82 50 01 00 00  MOVZX      AX,byte ptr [EDX + 0x150]
0050b157     66 8B 15 16 8F 5E 00     MOV        DX,word ptr [0x005e8f16]
0050b15e     03 C1                    ADD        EAX,ECX
0050b160     C1 E0 04                 SHL        EAX,0x4
0050b163     66 C1 EA 06              SHR        DX,0x6
0050b167     6A 00                    PUSH       0x0
0050b169     66 89 44 24 14           MOV        word ptr [ESP + 0x14],AX
0050b16e     66 89 54 24 16           MOV        word ptr [ESP + 0x16],DX
0050b173     66 89 74 24 18           MOV        word ptr [ESP + 0x18],SI
0050b178     66 C7 44 24 1A 01 00     MOV        word ptr [ESP + 0x1a],0x1
0050b17f     E8 40 A9 03 00           CALL       0x00545ac4
0050b184     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b189     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
0050b18d     83 C0 60                 ADD        EAX,0x60
0050b190     50                       PUSH       EAX
0050b191     51                       PUSH       ECX
0050b192     E8 27 A9 03 00           CALL       0x00545abe
0050b197     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b19c     8A 0D 14 8F 5E 00        MOV        CL,byte ptr [0x005e8f14]
0050b1a2     8A D1                    MOV        DL,CL
0050b1a4     23 CE                    AND        ECX,ESI
0050b1a6     8A 98 50 01 00 00        MOV        BL,byte ptr [EAX + 0x150]
0050b1ac     83 E2 0F                 AND        EDX,0xf
0050b1af     80 E3 03                 AND        BL,0x3
0050b1b2     6A 00                    PUSH       0x0
0050b1b4     66 0F B6 FB              MOVZX      DI,BL
0050b1b8     8D 14 97                 LEA        EDX,[EDI + EDX*0x4]
0050b1bb     C1 E2 04                 SHL        EDX,0x4
0050b1be     66 89 54 24 14           MOV        word ptr [ESP + 0x14],DX
0050b1c3     8A 80 50 01 00 00        MOV        AL,byte ptr [EAX + 0x150]
0050b1c9     24 0C                    AND        AL,0xc
0050b1cb     66 33 D2                 XOR        DX,DX
0050b1ce     8A D0                    MOV        DL,AL
0050b1d0     66 C7 44 24 18 04 00     MOV        word ptr [ESP + 0x18],0x4
0050b1d7     03 CA                    ADD        ECX,EDX
0050b1d9     66 89 74 24 1A           MOV        word ptr [ESP + 0x1a],SI
0050b1de     C1 E1 04                 SHL        ECX,0x4
0050b1e1     66 89 4C 24 16           MOV        word ptr [ESP + 0x16],CX
0050b1e6     E8 D9 A8 03 00           CALL       0x00545ac4
0050b1eb     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b1f0     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
0050b1f4     05 80 00 00 00           ADD        EAX,0x80
0050b1f9     50                       PUSH       EAX
0050b1fa     51                       PUSH       ECX
0050b1fb     E8 BE A8 03 00           CALL       0x00545abe
0050b200     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b206     B9 00 08 00 00           MOV        ECX,0x800
0050b20b     33 C0                    XOR        EAX,EAX
0050b20d     33 F6                    XOR        ESI,ESI
0050b20f     8D BA 00 20 00 00        LEA        EDI,[EDX + 0x2000]
0050b215     F3 AB                    STOSD.REP  ES:EDI
0050b217     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b21c     BF 00 02 00 00           MOV        EDI,0x200
0050b221     8D 88 00 40 00 00        LEA        ECX,[EAX + 0x4000]
0050b227     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b22d     8A 84 32 00 03 00 00     MOV        AL,byte ptr [EDX + ESI*0x1 + 0x300]
0050b234     84 C0                    TEST       AL,AL
0050b236     74 29                    JZ         0x0050b261
0050b238     8D 86 00 03 00 00        LEA        EAX,[ESI + 0x300]
0050b23e     BA 04 00 00 00           MOV        EDX,0x4
0050b243     8B 1D 38 33 57 00        MOV        EBX,dword ptr [0x00573338]
0050b249     83 C0 04                 ADD        EAX,0x4
0050b24c     83 C1 04                 ADD        ECX,0x4
0050b24f     4A                       DEC        EDX
0050b250     8B 5C 03 FC              MOV        EBX,dword ptr [EBX + EAX*0x1 + -0x4]
0050b254     89 59 FC                 MOV        dword ptr [ECX + -0x4],EBX
0050b257     75 EA                    JNZ        0x0050b243
0050b259     81 C6 80 02 00 00        ADD        ESI,0x280
0050b25f     EB 12                    JMP        0x0050b273
0050b261     8B D1                    MOV        EDX,ECX
0050b263     33 C0                    XOR        EAX,EAX
0050b265     83 C1 10                 ADD        ECX,0x10
0050b268     89 02                    MOV        dword ptr [EDX],EAX
0050b26a     89 42 04                 MOV        dword ptr [EDX + 0x4],EAX
0050b26d     89 42 08                 MOV        dword ptr [EDX + 0x8],EAX
0050b270     89 42 0C                 MOV        dword ptr [EDX + 0xc],EAX
0050b273     4F                       DEC        EDI
0050b274     75 B1                    JNZ        0x0050b227
0050b276     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050b27c     33 C0                    XOR        EAX,EAX
0050b27e     8B CE                    MOV        ECX,ESI
0050b280     BA FC 1F 00 00           MOV        EDX,0x1ffc
0050b285     0F BE 39                 MOVSX      EDI,byte ptr [ECX]
0050b288     03 C7                    ADD        EAX,EDI
0050b28a     41                       INC        ECX
0050b28b     4A                       DEC        EDX
0050b28c     75 F7                    JNZ        0x0050b285
0050b28e     6A 02                    PUSH       0x2
0050b290     68 40 49 60 00           PUSH       0x604940
0050b295     89 86 FC 1F 00 00        MOV        dword ptr [ESI + 0x1ffc],EAX
0050b29b     E8 A0 98 FF FF           CALL       0x00504b40
0050b2a0     83 C4 08                 ADD        ESP,0x8
0050b2a3     8B F0                    MOV        ESI,EAX
0050b2a5     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b2aa     68 00 20 00 00           PUSH       0x2000
0050b2af     6A 00                    PUSH       0x0
0050b2b1     50                       PUSH       EAX
0050b2b2     56                       PUSH       ESI
0050b2b3     E8 68 99 FF FF           CALL       0x00504c20
0050b2b8     8B 0D 38 33 57 00        MOV        ECX,dword ptr [0x00573338]
0050b2be     83 C4 10                 ADD        ESP,0x10
0050b2c1     81 C1 00 20 00 00        ADD        ECX,0x2000
0050b2c7     8B F8                    MOV        EDI,EAX
0050b2c9     68 00 1F 00 00           PUSH       0x1f00
0050b2ce     68 00 20 00 00           PUSH       0x2000
0050b2d3     51                       PUSH       ECX
0050b2d4     56                       PUSH       ESI
0050b2d5     E8 46 99 FF FF           CALL       0x00504c20
0050b2da     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050b2e0     83 C4 10                 ADD        ESP,0x10
0050b2e3     81 C2 00 1F 00 00        ADD        EDX,0x1f00
0050b2e9     0B F8                    OR         EDI,EAX
0050b2eb     68 00 01 00 00           PUSH       0x100
0050b2f0     68 00 3F 00 00           PUSH       0x3f00
0050b2f5     52                       PUSH       EDX
0050b2f6     56                       PUSH       ESI
0050b2f7     E8 24 99 FF FF           CALL       0x00504c20
0050b2fc     83 C4 10                 ADD        ESP,0x10
0050b2ff     0B F8                    OR         EDI,EAX
0050b301     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050b306     68 00 20 00 00           PUSH       0x2000
0050b30b     05 00 40 00 00           ADD        EAX,0x4000
0050b310     68 00 40 00 00           PUSH       0x4000
0050b315     50                       PUSH       EAX
0050b316     56                       PUSH       ESI
0050b317     E8 04 99 FF FF           CALL       0x00504c20
0050b31c     83 C4 10                 ADD        ESP,0x10
0050b31f     0B F8                    OR         EDI,EAX
0050b321     56                       PUSH       ESI
0050b322     E8 49 98 FF FF           CALL       0x00504b70
0050b327     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050b32d     83 C4 04                 ADD        ESP,0x4
0050b330     85 FF                    TEST       EDI,EDI
0050b332     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050b335     6A 00                    PUSH       0x0
0050b337     68 28 07 56 00           PUSH       0x560728
0050b33c     6A 00                    PUSH       0x0
0050b33e     7D 1E                    JGE        0x0050b35e
0050b340     8B 82 78 01 00 00        MOV        EAX,dword ptr [EDX + 0x178]
0050b346     50                       PUSH       EAX
0050b347     E8 04 90 03 00           CALL       0x00544350
0050b34c     83 C4 10                 ADD        ESP,0x10
0050b34f     C7 05 0C 8E 57 00 63 00 00 00 MOV        dword ptr [0x00578e0c],0x63
0050b359     E9 BD 03 00 00           JMP        0x0050b71b
0050b35e     8B 82 B8 01 00 00        MOV        EAX,dword ptr [EDX + 0x1b8]
0050b364     50                       PUSH       EAX
0050b365     E8 E6 8F 03 00           CALL       0x00544350
0050b36a     83 C4 10                 ADD        ESP,0x10
0050b36d     C7 05 3C 33 57 00 00 00 00 00 MOV        dword ptr [0x0057333c],0x0
0050b377     C7 05 0C 8E 57 00 11 00 00 00 MOV        dword ptr [0x00578e0c],0x11
0050b381     E9 95 03 00 00           JMP        0x0050b71b
0050b386     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b38a     74 0F                    JZ         0x0050b39b
0050b38c     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b396     E9 80 03 00 00           JMP        0x0050b71b
0050b39b     F6 C1 02                 TEST       CL,0x2
0050b39e     0F 85 77 03 00 00        JNZ        0x0050b71b
0050b3a4     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050b3aa     6A 00                    PUSH       0x0
0050b3ac     68 28 07 56 00           PUSH       0x560728
0050b3b1     6A 00                    PUSH       0x0
0050b3b3     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050b3b6     8B 82 BC 01 00 00        MOV        EAX,dword ptr [EDX + 0x1bc]
0050b3bc     50                       PUSH       EAX
0050b3bd     E9 06 03 00 00           JMP        0x0050b6c8
0050b3c2     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b3c6     74 0F                    JZ         0x0050b3d7
0050b3c8     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b3d2     E9 44 03 00 00           JMP        0x0050b71b
0050b3d7     F6 C1 02                 TEST       CL,0x2
0050b3da     0F 85 3B 03 00 00        JNZ        0x0050b71b
0050b3e0     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050b3e6     6A 00                    PUSH       0x0
0050b3e8     68 28 07 56 00           PUSH       0x560728
0050b3ed     6A 00                    PUSH       0x0
0050b3ef     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050b3f2     8B 82 90 01 00 00        MOV        EAX,dword ptr [EDX + 0x190]
0050b3f8     50                       PUSH       EAX
0050b3f9     E9 CA 02 00 00           JMP        0x0050b6c8
0050b3fe     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b402     74 0F                    JZ         0x0050b413
0050b404     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b40e     E9 08 03 00 00           JMP        0x0050b71b
0050b413     F6 C1 04                 TEST       CL,0x4
0050b416     0F 85 FF 02 00 00        JNZ        0x0050b71b
0050b41c     84 D2                    TEST       DL,DL
0050b41e     0F 84 AC 02 00 00        JZ         0x0050b6d0
0050b424     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b42e     E9 E8 02 00 00           JMP        0x0050b71b
0050b433     66 8B 46 02              MOV        AX,word ptr [ESI + 0x2]
0050b437     85 C7                    TEST       EDI,EAX
0050b439     74 0F                    JZ         0x0050b44a
0050b43b     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b445     E9 D1 02 00 00           JMP        0x0050b71b
0050b44a     F6 C4 80                 TEST       AH,0x80
0050b44d     74 3C                    JZ         0x0050b48b
0050b44f     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050b455     6A 00                    PUSH       0x0
0050b457     68 28 07 56 00           PUSH       0x560728
0050b45c     6A 00                    PUSH       0x0
0050b45e     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050b461     8B 82 C0 01 00 00        MOV        EAX,dword ptr [EDX + 0x1c0]
0050b467     50                       PUSH       EAX
0050b468     E8 E3 8E 03 00           CALL       0x00544350
0050b46d     66 81 66 02 FF 7F        AND        word ptr [ESI + 0x2],0x7fff
0050b473     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050b478     83 C4 10                 ADD        ESP,0x10
0050b47b     40                       INC        EAX
0050b47c     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050b481     E8 FA 26 03 00           CALL       0x0053db80
0050b486     E9 90 02 00 00           JMP        0x0050b71b
0050b48b     8B 0D 84 49 60 00        MOV        ECX,dword ptr [0x00604984]
0050b491     8A 46 06                 MOV        AL,byte ptr [ESI + 0x6]
0050b494     81 E1 FF 00 00 00        AND        ECX,0xff
0050b49a     A8 80                    TEST       AL,0x80
0050b49c     A0 84 49 60 00           MOV        AL,[0x00604984]
0050b4a1     74 07                    JZ         0x0050b4aa
0050b4a3     FE C8                    DEC        AL
0050b4a5     A2 84 49 60 00           MOV        [0x00604984],AL
0050b4aa     F6 46 06 20              TEST       byte ptr [ESI + 0x6],0x20
0050b4ae     74 07                    JZ         0x0050b4b7
0050b4b0     FE C0                    INC        AL
0050b4b2     A2 84 49 60 00           MOV        [0x00604984],AL
0050b4b7     8A 15 81 49 60 00        MOV        DL,byte ptr [0x00604981]
0050b4bd     3C FF                    CMP        AL,0xff
0050b4bf     75 09                    JNZ        0x0050b4ca
0050b4c1     8A C2                    MOV        AL,DL
0050b4c3     FE C8                    DEC        AL
0050b4c5     A2 84 49 60 00           MOV        [0x00604984],AL
0050b4ca     3A C2                    CMP        AL,DL
0050b4cc     72 07                    JC         0x0050b4d5
0050b4ce     C6 05 84 49 60 00 00     MOV        byte ptr [0x00604984],0x0
0050b4d5     A1 84 49 60 00           MOV        EAX,[0x00604984]
0050b4da     25 FF 00 00 00           AND        EAX,0xff
0050b4df     3B C8                    CMP        ECX,EAX
0050b4e1     74 05                    JZ         0x0050b4e8
0050b4e3     E8 78 26 03 00           CALL       0x0053db60
0050b4e8     6A 00                    PUSH       0x0
0050b4ea     68 28 07 56 00           PUSH       0x560728
0050b4ef     6A 00                    PUSH       0x0
0050b4f1     68 F8 06 56 00           PUSH       0x5606f8
0050b4f6     E8 55 8E 03 00           CALL       0x00544350
0050b4fb     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
0050b4ff     83 C4 10                 ADD        ESP,0x10
0050b502     68 28 07 56 00           PUSH       0x560728
0050b507     51                       PUSH       ECX
0050b508     E8 93 8E 03 00           CALL       0x005443a0
0050b50d     8B 15 84 49 60 00        MOV        EDX,dword ptr [0x00604984]
0050b513     89 44 24 40              MOV        dword ptr [ESP + 0x40],EAX
0050b517     A1 B4 87 60 00           MOV        EAX,[0x006087b4]
0050b51c     81 E2 FF 00 00 00        AND        EDX,0xff
0050b522     83 C4 08                 ADD        ESP,0x8
0050b525     8B 08                    MOV        ECX,dword ptr [EAX]
0050b527     C1 E2 08                 SHL        EDX,0x8
0050b52a     6A 00                    PUSH       0x0
0050b52c     68 28 07 56 00           PUSH       0x560728
0050b531     8D 94 0A 04 00 1C 00     LEA        EDX,[EDX + ECX*0x1 + 0x1c0004]
0050b538     6A 00                    PUSH       0x0
0050b53a     52                       PUSH       EDX
0050b53b     E8 10 8E 03 00           CALL       0x00544350
0050b540     83 C4 10                 ADD        ESP,0x10
0050b543     E9 D3 01 00 00           JMP        0x0050b71b
0050b548     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b54c     74 20                    JZ         0x0050b56e
0050b54e     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050b553     80 E1 FD                 AND        CL,0xfd
0050b556     48                       DEC        EAX
0050b557     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
0050b55d     C6 05 5B 07 56 00 00     MOV        byte ptr [0x0056075b],0x0
0050b564     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050b569     E9 AD 01 00 00           JMP        0x0050b71b
0050b56e     F6 C1 02                 TEST       CL,0x2
0050b571     0F 85 A4 01 00 00        JNZ        0x0050b71b
0050b577     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b57c     6A 00                    PUSH       0x0
0050b57e     68 28 07 56 00           PUSH       0x560728
0050b583     6A 00                    PUSH       0x0
0050b585     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b588     8B 91 90 01 00 00        MOV        EDX,dword ptr [ECX + 0x190]
0050b58e     E9 34 01 00 00           JMP        0x0050b6c7
0050b593     80 E1 FB                 AND        CL,0xfb
0050b596     C6 05 63 07 56 00 00     MOV        byte ptr [0x00560763],0x0
0050b59d     88 0D EF 0F 56 00        MOV        byte ptr [0x00560fef],CL
0050b5a3     C7 05 0C 8E 57 00 0A 00 00 00 MOV        dword ptr [0x00578e0c],0xa
0050b5ad     E9 69 01 00 00           JMP        0x0050b71b
0050b5b2     6A 00                    PUSH       0x0
0050b5b4     68 28 07 56 00           PUSH       0x560728
0050b5b9     6A 00                    PUSH       0x0
0050b5bb     68 40 2B 57 00           PUSH       0x572b40
0050b5c0     E9 03 01 00 00           JMP        0x0050b6c8
0050b5c5     89 1D 14 8E 57 00        MOV        dword ptr [0x00578e14],EBX
0050b5cb     89 1D 3C 33 57 00        MOV        dword ptr [0x0057333c],EBX
0050b5d1     E9 45 01 00 00           JMP        0x0050b71b
0050b5d6     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b5da     74 0F                    JZ         0x0050b5eb
0050b5dc     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b5e6     E9 30 01 00 00           JMP        0x0050b71b
0050b5eb     F6 C1 02                 TEST       CL,0x2
0050b5ee     0F 85 27 01 00 00        JNZ        0x0050b71b
0050b5f4     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b5f9     6A 00                    PUSH       0x0
0050b5fb     68 28 07 56 00           PUSH       0x560728
0050b600     6A 00                    PUSH       0x0
0050b602     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b605     8B 91 C8 01 00 00        MOV        EDX,dword ptr [ECX + 0x1c8]
0050b60b     52                       PUSH       EDX
0050b60c     E8 3F 8D 03 00           CALL       0x00544350
0050b611     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050b616     83 C4 10                 ADD        ESP,0x10
0050b619     40                       INC        EAX
0050b61a     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050b61f     E9 F7 00 00 00           JMP        0x0050b71b
0050b624     66 85 7E 02              TEST       word ptr [ESI + 0x2],DI
0050b628     74 0F                    JZ         0x0050b639
0050b62a     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b634     E9 E2 00 00 00           JMP        0x0050b71b
0050b639     F6 C1 02                 TEST       CL,0x2
0050b63c     0F 85 D9 00 00 00        JNZ        0x0050b71b
0050b642     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b647     6A 00                    PUSH       0x0
0050b649     68 28 07 56 00           PUSH       0x560728
0050b64e     6A 00                    PUSH       0x0
0050b650     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b653     8B 91 90 01 00 00        MOV        EDX,dword ptr [ECX + 0x190]
0050b659     52                       PUSH       EDX
0050b65a     E8 F1 8C 03 00           CALL       0x00544350
0050b65f     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050b664     83 C4 10                 ADD        ESP,0x10
0050b667     40                       INC        EAX
0050b668     A3 0C 8E 57 00           MOV        [0x00578e0c],EAX
0050b66d     E9 A9 00 00 00           JMP        0x0050b71b
0050b672     C7 05 0C 8E 57 00 FB FF FF FF MOV        dword ptr [0x00578e0c],0xfffffffb
0050b67c     E9 9A 00 00 00           JMP        0x0050b71b
0050b681     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b686     53                       PUSH       EBX
0050b687     68 28 07 56 00           PUSH       0x560728
0050b68c     53                       PUSH       EBX
0050b68d     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b690     8B 91 C4 01 00 00        MOV        EDX,dword ptr [ECX + 0x1c4]
0050b696     52                       PUSH       EDX
0050b697     E8 B4 8C 03 00           CALL       0x00544350
0050b69c     83 C4 10                 ADD        ESP,0x10
0050b69f     C7 05 0C 8E 57 00 FF FF FF FF MOV        dword ptr [0x00578e0c],0xffffffff
0050b6a9     EB 70                    JMP        0x0050b71b
0050b6ab     F6 C1 02                 TEST       CL,0x2
0050b6ae     75 6B                    JNZ        0x0050b71b
0050b6b0     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b6b5     6A 00                    PUSH       0x0
0050b6b7     68 28 07 56 00           PUSH       0x560728
0050b6bc     6A 00                    PUSH       0x0
0050b6be     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050b6c1     8B 91 D8 01 00 00        MOV        EDX,dword ptr [ECX + 0x1d8]
0050b6c7     52                       PUSH       EDX
0050b6c8     E8 83 8C 03 00           CALL       0x00544350
0050b6cd     83 C4 10                 ADD        ESP,0x10
0050b6d0     FF 05 0C 8E 57 00        INC        dword ptr [0x00578e0c]
0050b6d6     EB 43                    JMP        0x0050b71b
0050b6d8     F6 C1 04                 TEST       CL,0x4
0050b6db     75 3E                    JNZ        0x0050b71b
0050b6dd     84 D2                    TEST       DL,DL
0050b6df     74 1D                    JZ         0x0050b6fe
0050b6e1     C6 05 1C E5 5D 00 00     MOV        byte ptr [0x005de51c],0x0
0050b6e8     C7 05 E8 8D 57 00 FF FF FF FF MOV        dword ptr [0x00578de8],0xffffffff
0050b6f2     C7 05 10 E5 5D 00 01 00 00 00 MOV        dword ptr [0x005de510],0x1
0050b6fc     EB 1D                    JMP        0x0050b71b
0050b6fe     33 C0                    XOR        EAX,EAX
0050b700     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
0050b705     A3 F0 8D 57 00           MOV        [0x00578df0],EAX
0050b70a     EB 0F                    JMP        0x0050b71b
0050b70c     F6 C1 02                 TEST       CL,0x2
0050b70f     75 0A                    JNZ        0x0050b71b
0050b711     C7 05 68 2B 56 00 FF FF FF FF MOV        dword ptr [0x00562b68],0xffffffff
0050b71b     A1 68 2B 56 00           MOV        EAX,[0x00562b68]
0050b720     33 FF                    XOR        EDI,EDI
0050b722     3B C7                    CMP        EAX,EDI
0050b724     0F 8C DA 02 00 00        JL         0x0050ba04
0050b72a     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050b72f     83 F8 0A                 CMP        EAX,0xa
0050b732     0F 8C CC 02 00 00        JL         0x0050ba04
0050b738     83 F8 0D                 CMP        EAX,0xd
0050b73b     0F 8F C3 02 00 00        JG         0x0050ba04
0050b741     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050b746     C7 44 24 1C 00 00 00 01  MOV        dword ptr [ESP + 0x1c],0x1000000
0050b74e     57                       PUSH       EDI
0050b74f     57                       PUSH       EDI
0050b750     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
0050b756     8D 44 24 20              LEA        EAX,[ESP + 0x20]
0050b75a     50                       PUSH       EAX
0050b75b     8B 51 38                 MOV        EDX,dword ptr [ECX + 0x38]
0050b75e     8B 4C 24 44              MOV        ECX,dword ptr [ESP + 0x44]
0050b762     51                       PUSH       ECX
0050b763     55                       PUSH       EBP
0050b764     89 54 24 2C              MOV        dword ptr [ESP + 0x2c],EDX
0050b768     66 89 7C 24 3C           MOV        word ptr [ESP + 0x3c],DI
0050b76d     66 C7 44 24 34 B4 00     MOV        word ptr [ESP + 0x34],0xb4
0050b774     66 C7 44 24 36 30 00     MOV        word ptr [ESP + 0x36],0x30
0050b77b     E8 70 66 01 00           CALL       0x00521df0
0050b780     83 C4 14                 ADD        ESP,0x14
0050b783     8D 54 24 18              LEA        EDX,[ESP + 0x18]
0050b787     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050b78b     66 C7 44 24 28 01 00     MOV        word ptr [ESP + 0x28],0x1
0050b792     57                       PUSH       EDI
0050b793     57                       PUSH       EDI
0050b794     52                       PUSH       EDX
0050b795     50                       PUSH       EAX
0050b796     55                       PUSH       EBP
0050b797     66 C7 44 24 34 E8 00     MOV        word ptr [ESP + 0x34],0xe8
0050b79e     E8 4D 66 01 00           CALL       0x00521df0
0050b7a3     83 C4 14                 ADD        ESP,0x14
0050b7a6     8D 4C 24 18              LEA        ECX,[ESP + 0x18]
0050b7aa     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050b7ae     51                       PUSH       ECX
0050b7af     50                       PUSH       EAX
0050b7b0     55                       PUSH       EBP
0050b7b1     E8 0A 42 01 00           CALL       0x0051f9c0
0050b7b6     8B 0D 84 49 60 00        MOV        ECX,dword ptr [0x00604984]
0050b7bc     83 C4 0C                 ADD        ESP,0xc
0050b7bf     81 E1 FF 00 00 00        AND        ECX,0xff
0050b7c5     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050b7c9     C7 44 24 18 30 6C 56 00  MOV        dword ptr [ESP + 0x18],0x566c30
0050b7d1     66 C7 44 24 20 BC 00     MOV        word ptr [ESP + 0x20],0xbc
0050b7d8     8D 14 89                 LEA        EDX,[ECX + ECX*0x4]
0050b7db     8B 0D BC 49 60 00        MOV        ECX,dword ptr [0x006049bc]
0050b7e1     66 C7 44 24 22 38 00     MOV        word ptr [ESP + 0x22],0x38
0050b7e8     57                       PUSH       EDI
0050b7e9     8B 54 D1 18              MOV        EDX,dword ptr [ECX + EDX*0x8 + 0x18]
0050b7ed     68 82 00 00 00           PUSH       0x82
0050b7f2     8D 4C 24 20              LEA        ECX,[ESP + 0x20]
0050b7f6     52                       PUSH       EDX
0050b7f7     51                       PUSH       ECX
0050b7f8     50                       PUSH       EAX
0050b7f9     55                       PUSH       EBP
0050b7fa     E8 91 06 01 00           CALL       0x0051be90
0050b7ff     83 C4 18                 ADD        ESP,0x18
0050b802     33 D2                    XOR        EDX,EDX
0050b804     8A 15 82 49 60 00        MOV        DL,byte ptr [0x00604982]
0050b80a     8D 4C 24 18              LEA        ECX,[ESP + 0x18]
0050b80e     57                       PUSH       EDI
0050b80f     68 82 00 00 00           PUSH       0x82
0050b814     52                       PUSH       EDX
0050b815     51                       PUSH       ECX
0050b816     50                       PUSH       EAX
0050b817     55                       PUSH       EBP
0050b818     89 44 24 50              MOV        dword ptr [ESP + 0x50],EAX
0050b81c     66 C7 44 24 38 EC 00     MOV        word ptr [ESP + 0x38],0xec
0050b823     E8 68 06 01 00           CALL       0x0051be90
0050b828     33 D2                    XOR        EDX,EDX
0050b82a     8B F0                    MOV        ESI,EAX
0050b82c     8A 15 81 49 60 00        MOV        DL,byte ptr [0x00604981]
0050b832     66 89 7C 24 40           MOV        word ptr [ESP + 0x40],DI
0050b837     B8 00 10 00 00           MOV        EAX,0x1000
0050b83c     8B FA                    MOV        EDI,EDX
0050b83e     99                       CDQ        
0050b83f     83 C4 18                 ADD        ESP,0x18
0050b842     8B 0D 84 49 60 00        MOV        ECX,dword ptr [0x00604984]
0050b848     F7 FF                    IDIV       EDI
0050b84a     89 74 24 38              MOV        dword ptr [ESP + 0x38],ESI
0050b84e     66 C7 05 F8 8F 5E 00 0F 00 MOV        word ptr [0x005e8ff8],0xf
0050b857     66 C7 05 FA 8F 5E 00 80 7F MOV        word ptr [0x005e8ffa],0x7f80
0050b860     C7 44 24 18 48 31 56 00  MOV        dword ptr [ESP + 0x18],0x563148
0050b868     8B D0                    MOV        EDX,EAX
0050b86a     8B C1                    MOV        EAX,ECX
0050b86c     25 FF 00 00 00           AND        EAX,0xff
0050b871     0F AF D0                 IMUL       EDX,EAX
0050b874     A1 88 49 60 00           MOV        EAX,[0x00604988]
0050b879     2B C2                    SUB        EAX,EDX
0050b87b     25 FF 0F 00 00           AND        EAX,0xfff
0050b880     3D D0 07 00 00           CMP        EAX,0x7d0
0050b885     7D 07                    JGE        0x0050b88e
0050b887     C1 F8 02                 SAR        EAX,0x2
0050b88a     8B D0                    MOV        EDX,EAX
0050b88c     EB 0C                    JMP        0x0050b89a
0050b88e     BA 00 10 00 00           MOV        EDX,0x1000
0050b893     2B D0                    SUB        EDX,EAX
0050b895     C1 FA 02                 SAR        EDX,0x2
0050b898     F7 DA                    NEG        EDX
0050b89a     66 A1 8A 49 60 00        MOV        AX,[0x0060498a]
0050b8a0     66 29 15 88 49 60 00     SUB        word ptr [0x00604988],DX
0050b8a7     0F BF D0                 MOVSX      EDX,AX
0050b8aa     BB 00 18 00 00           MOV        EBX,0x1800
0050b8af     89 7C 24 3C              MOV        dword ptr [ESP + 0x3c],EDI
0050b8b3     2B DA                    SUB        EBX,EDX
0050b8b5     C1 FB 02                 SAR        EBX,0x2
0050b8b8     66 03 C3                 ADD        AX,BX
0050b8bb     85 FF                    TEST       EDI,EDI
0050b8bd     66 A3 8A 49 60 00        MOV        [0x0060498a],AX
0050b8c3     0F 84 28 01 00 00        JZ         0x0050b9f1
0050b8c9     EB 06                    JMP        0x0050b8d1
0050b8cb     8B 0D 84 49 60 00        MOV        ECX,dword ptr [0x00604984]
0050b8d1     33 C0                    XOR        EAX,EAX
0050b8d3     81 E1 FF 00 00 00        AND        ECX,0xff
0050b8d9     A0 81 49 60 00           MOV        AL,[0x00604981]
0050b8de     8B F0                    MOV        ESI,EAX
0050b8e0     8B 44 24 3C              MOV        EAX,dword ptr [ESP + 0x3c]
0050b8e4     03 C1                    ADD        EAX,ECX
0050b8e6     99                       CDQ        
0050b8e7     F7 FE                    IDIV       ESI
0050b8e9     B8 00 10 00 00           MOV        EAX,0x1000
0050b8ee     0F BF 0D 88 49 60 00     MOVSX      ECX,word ptr [0x00604988]
0050b8f5     8B DA                    MOV        EBX,EDX
0050b8f7     99                       CDQ        
0050b8f8     F7 FE                    IDIV       ESI
0050b8fa     8B F0                    MOV        ESI,EAX
0050b8fc     0F AF F3                 IMUL       ESI,EBX
0050b8ff     2B F1                    SUB        ESI,ECX
0050b901     56                       PUSH       ESI
0050b902     E8 15 A1 03 00           CALL       0x00545a1c
0050b907     0F BF 15 8A 49 60 00     MOVSX      EDX,word ptr [0x0060498a]
0050b90e     0F AF C2                 IMUL       EAX,EDX
0050b911     C1 F8 14                 SAR        EAX,0x14
0050b914     05 D3 00 00 00           ADD        EAX,0xd3
0050b919     56                       PUSH       ESI
0050b91a     66 89 44 24 24           MOV        word ptr [ESP + 0x24],AX
0050b91f     E8 F2 A0 03 00           CALL       0x00545a16
0050b924     0F BF 0D 8A 49 60 00     MOVSX      ECX,word ptr [0x0060498a]
0050b92b     0F AF C1                 IMUL       EAX,ECX
0050b92e     8B 35 60 99 5E 00        MOV        ESI,dword ptr [0x005e9960]
0050b934     BF 64 00 00 00           MOV        EDI,0x64
0050b939     C1 F8 14                 SAR        EAX,0x14
0050b93c     8A D3                    MOV        DL,BL
0050b93e     2B F8                    SUB        EDI,EAX
0050b940     C0 E2 04                 SHL        DL,0x4
0050b943     33 C9                    XOR        ECX,ECX
0050b945     8B C6                    MOV        EAX,ESI
0050b947     8A 8B 8C 49 60 00        MOV        CL,byte ptr [EBX + 0x60498c]
0050b94d     88 15 48 31 56 00        MOV        byte ptr [0x00563148],DL
0050b953     C1 E8 02                 SHR        EAX,0x2
0050b956     33 D2                    XOR        EDX,EDX
0050b958     66 89 7C 24 22           MOV        word ptr [ESP + 0x22],DI
0050b95d     F7 F1                    DIV        ECX
0050b95f     8B 44 24 3C              MOV        EAX,dword ptr [ESP + 0x3c]
0050b963     80 C2 02                 ADD        DL,0x2
0050b966     C0 E2 04                 SHL        DL,0x4
0050b969     88 15 49 31 56 00        MOV        byte ptr [0x00563149],DL
0050b96f     33 D2                    XOR        EDX,EDX
0050b971     8A 15 81 49 60 00        MOV        DL,byte ptr [0x00604981]
0050b977     3B C2                    CMP        EAX,EDX
0050b979     75 4B                    JNZ        0x0050b9c6
0050b97b     66 83 44 24 20 08        ADD        word ptr [ESP + 0x20],0x8
0050b981     83 E6 07                 AND        ESI,0x7
0050b984     C1 E6 09                 SHL        ESI,0x9
0050b987     66 83 C7 08              ADD        DI,0x8
0050b98b     56                       PUSH       ESI
0050b98c     66 89 7C 24 26           MOV        word ptr [ESP + 0x26],DI
0050b991     E8 86 A0 03 00           CALL       0x00545a1c
0050b996     8B 54 24 38              MOV        EDX,dword ptr [ESP + 0x38]
0050b99a     53                       PUSH       EBX
0050b99b     C1 F8 02                 SAR        EAX,0x2
0050b99e     05 00 14 00 00           ADD        EAX,0x1400
0050b9a3     8D 4C 24 1C              LEA        ECX,[ESP + 0x1c]
0050b9a7     6A 00                    PUSH       0x0
0050b9a9     66 89 44 24 2C           MOV        word ptr [ESP + 0x2c],AX
0050b9ae     66 89 44 24 2E           MOV        word ptr [ESP + 0x2e],AX
0050b9b3     A1 14 98 5E 00           MOV        EAX,[0x005e9814]
0050b9b8     51                       PUSH       ECX
0050b9b9     52                       PUSH       EDX
0050b9ba     55                       PUSH       EBP
0050b9bb     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
0050b9bf     E8 5C 65 01 00           CALL       0x00521f20
0050b9c4     EB 13                    JMP        0x0050b9d9
0050b9c6     8B 4C 24 38              MOV        ECX,dword ptr [ESP + 0x38]
0050b9ca     53                       PUSH       EBX
0050b9cb     8D 44 24 1C              LEA        EAX,[ESP + 0x1c]
0050b9cf     6A 03                    PUSH       0x3
0050b9d1     50                       PUSH       EAX
0050b9d2     51                       PUSH       ECX
0050b9d3     55                       PUSH       EBP
0050b9d4     E8 17 64 01 00           CALL       0x00521df0
0050b9d9     8B F0                    MOV        ESI,EAX
0050b9db     8B 44 24 50              MOV        EAX,dword ptr [ESP + 0x50]
0050b9df     83 C4 14                 ADD        ESP,0x14
0050b9e2     48                       DEC        EAX
0050b9e3     89 74 24 38              MOV        dword ptr [ESP + 0x38],ESI
0050b9e7     89 44 24 3C              MOV        dword ptr [ESP + 0x3c],EAX
0050b9eb     0F 85 DA FE FF FF        JNZ        0x0050b8cb
0050b9f1     8D 54 24 18              LEA        EDX,[ESP + 0x18]
0050b9f5     52                       PUSH       EDX
0050b9f6     56                       PUSH       ESI
0050b9f7     55                       PUSH       EBP
0050b9f8     E8 C3 3F 01 00           CALL       0x0051f9c0
0050b9fd     83 C4 0C                 ADD        ESP,0xc
0050ba00     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050ba04     8D 44 24 38              LEA        EAX,[ESP + 0x38]
0050ba08     50                       PUSH       EAX
0050ba09     55                       PUSH       EBP
0050ba0a     E8 11 04 01 00           CALL       0x0051be20
0050ba0f     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050ba15     83 C4 08                 ADD        ESP,0x8
0050ba18     8B 91 94 00 00 00        MOV        EDX,dword ptr [ECX + 0x94]
0050ba1e     8B 42 34                 MOV        EAX,dword ptr [EDX + 0x34]
0050ba21     89 44 24 18              MOV        dword ptr [ESP + 0x18],EAX
0050ba25     A1 68 2B 56 00           MOV        EAX,[0x00562b68]
0050ba2a     85 C0                    TEST       EAX,EAX
0050ba2c     7C 17                    JL         0x0050ba45
0050ba2e     A1 0C 8E 57 00           MOV        EAX,[0x00578e0c]
0050ba33     83 F8 0A                 CMP        EAX,0xa
0050ba36     7C 0D                    JL         0x0050ba45
0050ba38     83 F8 0D                 CMP        EAX,0xd
0050ba3b     C7 44 24 1C 40 40 40 00  MOV        dword ptr [ESP + 0x1c],0x404040
0050ba43     7E 08                    JLE        0x0050ba4d
0050ba45     C7 44 24 1C 00 00 00 01  MOV        dword ptr [ESP + 0x1c],0x1000000
0050ba4d     BF 00 80 00 00           MOV        EDI,0x8000
0050ba52     BE 6E 00 00 00           MOV        ESI,0x6e
0050ba57     66 39 3D 0A 4B 60 00     CMP        word ptr [0x00604b0a],DI
0050ba5e     75 2E                    JNZ        0x0050ba8e
0050ba60     8B 54 24 38              MOV        EDX,dword ptr [ESP + 0x38]
0050ba64     6A 00                    PUSH       0x0
0050ba66     8D 4C 24 1C              LEA        ECX,[ESP + 0x1c]
0050ba6a     6A 00                    PUSH       0x0
0050ba6c     51                       PUSH       ECX
0050ba6d     52                       PUSH       EDX
0050ba6e     55                       PUSH       EBP
0050ba6f     66 C7 44 24 3C 01 00     MOV        word ptr [ESP + 0x3c],0x1
0050ba76     66 C7 44 24 34 A9 00     MOV        word ptr [ESP + 0x34],0xa9
0050ba7d     66 89 74 24 36           MOV        word ptr [ESP + 0x36],SI
0050ba82     E8 69 63 01 00           CALL       0x00521df0
0050ba87     83 C4 14                 ADD        ESP,0x14
0050ba8a     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050ba8e     66 39 3D 2E 4B 60 00     CMP        word ptr [0x00604b2e],DI
0050ba95     75 2E                    JNZ        0x0050bac5
0050ba97     8B 4C 24 38              MOV        ECX,dword ptr [ESP + 0x38]
0050ba9b     6A 00                    PUSH       0x0
0050ba9d     8D 44 24 1C              LEA        EAX,[ESP + 0x1c]
0050baa1     6A 00                    PUSH       0x0
0050baa3     50                       PUSH       EAX
0050baa4     51                       PUSH       ECX
0050baa5     55                       PUSH       EBP
0050baa6     66 C7 44 24 3C 02 00     MOV        word ptr [ESP + 0x3c],0x2
0050baad     66 C7 44 24 34 DD 00     MOV        word ptr [ESP + 0x34],0xdd
0050bab4     66 89 74 24 36           MOV        word ptr [ESP + 0x36],SI
0050bab9     E8 32 63 01 00           CALL       0x00521df0
0050babe     83 C4 14                 ADD        ESP,0x14
0050bac1     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bac5     8D 54 24 38              LEA        EDX,[ESP + 0x38]
0050bac9     66 C7 44 24 28 00 00     MOV        word ptr [ESP + 0x28],0x0
0050bad0     52                       PUSH       EDX
0050bad1     55                       PUSH       EBP
0050bad2     66 C7 44 24 28 BD 00     MOV        word ptr [ESP + 0x28],0xbd
0050bad9     66 C7 44 24 2A 48 00     MOV        word ptr [ESP + 0x2a],0x48
0050bae0     E8 CB 02 01 00           CALL       0x0051bdb0
0050bae5     8B 44 24 40              MOV        EAX,dword ptr [ESP + 0x40]
0050bae9     83 C4 08                 ADD        ESP,0x8
0050baec     68 18 35 57 00           PUSH       0x573518
0050baf1     50                       PUSH       EAX
0050baf2     55                       PUSH       EBP
0050baf3     E8 48 98 03 00           CALL       0x00545340
0050baf8     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050bafe     83 C4 0C                 ADD        ESP,0xc
0050bb01     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bb05     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050bb08     6A 00                    PUSH       0x0
0050bb0a     68 E0 3D 57 00           PUSH       0x573de0
0050bb0f     6A 00                    PUSH       0x0
0050bb11     8B 82 58 01 00 00        MOV        EAX,dword ptr [EDX + 0x158]
0050bb17     50                       PUSH       EAX
0050bb18     E8 33 88 03 00           CALL       0x00544350
0050bb1d     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
0050bb21     83 C4 10                 ADD        ESP,0x10
0050bb24     68 E0 3D 57 00           PUSH       0x573de0
0050bb29     51                       PUSH       ECX
0050bb2a     E8 71 88 03 00           CALL       0x005443a0
0050bb2f     8B 15 38 33 57 00        MOV        EDX,dword ptr [0x00573338]
0050bb35     83 C4 08                 ADD        ESP,0x8
0050bb38     81 C2 00 01 00 00        ADD        EDX,0x100
0050bb3e     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bb42     6A 00                    PUSH       0x0
0050bb44     68 E0 3D 57 00           PUSH       0x573de0
0050bb49     6A 00                    PUSH       0x0
0050bb4b     52                       PUSH       EDX
0050bb4c     E8 FF 87 03 00           CALL       0x00544350
0050bb51     8B 44 24 48              MOV        EAX,dword ptr [ESP + 0x48]
0050bb55     83 C4 10                 ADD        ESP,0x10
0050bb58     68 E0 3D 57 00           PUSH       0x573de0
0050bb5d     50                       PUSH       EAX
0050bb5e     E8 3D 88 03 00           CALL       0x005443a0
0050bb63     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050bb69     83 C4 08                 ADD        ESP,0x8
0050bb6c     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bb70     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
0050bb73     6A 00                    PUSH       0x0
0050bb75     68 E0 3D 57 00           PUSH       0x573de0
0050bb7a     6A 00                    PUSH       0x0
0050bb7c     8B 82 5C 01 00 00        MOV        EAX,dword ptr [EDX + 0x15c]
0050bb82     50                       PUSH       EAX
0050bb83     E8 C8 87 03 00           CALL       0x00544350
0050bb88     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
0050bb8c     83 C4 10                 ADD        ESP,0x10
0050bb8f     68 E0 3D 57 00           PUSH       0x573de0
0050bb94     51                       PUSH       ECX
0050bb95     E8 06 88 03 00           CALL       0x005443a0
0050bb9a     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050bba0     89 44 24 40              MOV        dword ptr [ESP + 0x40],EAX
0050bba4     A1 38 33 57 00           MOV        EAX,[0x00573338]
0050bba9     33 D2                    XOR        EDX,EDX
0050bbab     83 C4 08                 ADD        ESP,0x8
0050bbae     8A 90 50 01 00 00        MOV        DL,byte ptr [EAX + 0x150]
0050bbb4     8B 41 70                 MOV        EAX,dword ptr [ECX + 0x70]
0050bbb7     6A 00                    PUSH       0x0
0050bbb9     68 E0 3D 57 00           PUSH       0x573de0
0050bbbe     8B 4C 90 0C              MOV        ECX,dword ptr [EAX + EDX*0x4 + 0xc]
0050bbc2     6A 00                    PUSH       0x0
0050bbc4     51                       PUSH       ECX
0050bbc5     E8 86 87 03 00           CALL       0x00544350
0050bbca     8B 54 24 48              MOV        EDX,dword ptr [ESP + 0x48]
0050bbce     83 C4 10                 ADD        ESP,0x10
0050bbd1     68 E0 3D 57 00           PUSH       0x573de0
0050bbd6     52                       PUSH       EDX
0050bbd7     E8 C4 87 03 00           CALL       0x005443a0
0050bbdc     89 44 24 40              MOV        dword ptr [ESP + 0x40],EAX
0050bbe0     A1 60 99 5E 00           MOV        EAX,[0x005e9960]
0050bbe5     83 C4 08                 ADD        ESP,0x8
0050bbe8     C6 05 9A 90 5E 00 01     MOV        byte ptr [0x005e909a],0x1
0050bbef     C6 05 9B 90 5E 00 0F     MOV        byte ptr [0x005e909b],0xf
0050bbf6     C1 E8 02                 SHR        EAX,0x2
0050bbf9     25 FF FF FF 1F           AND        EAX,0x1fffffff
0050bbfe     33 D2                    XOR        EDX,EDX
0050bc00     B9 03 00 00 00           MOV        ECX,0x3
0050bc05     8B 35 38 33 57 00        MOV        ESI,dword ptr [0x00573338]
0050bc0b     F7 F1                    DIV        ECX
0050bc0d     8A 86 51 01 00 00        MOV        AL,byte ptr [ESI + 0x151]
0050bc13     8A C9                    MOV        CL,CL
0050bc15     F6 E9                    IMUL       CL
0050bc17     C6 05 9D 90 5E 00 10     MOV        byte ptr [0x005e909d],0x10
0050bc1e     6A 00                    PUSH       0x0
0050bc20     68 E0 3D 57 00           PUSH       0x573de0
0050bc25     6A 00                    PUSH       0x0
0050bc27     02 D0                    ADD        DL,AL
0050bc29     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050bc2e     88 15 9C 90 5E 00        MOV        byte ptr [0x005e909c],DL
0050bc34     8A 96 50 01 00 00        MOV        DL,byte ptr [ESI + 0x150]
0050bc3a     88 15 9E 90 5E 00        MOV        byte ptr [0x005e909e],DL
0050bc40     8B 48 70                 MOV        ECX,dword ptr [EAX + 0x70]
0050bc43     8B 91 60 01 00 00        MOV        EDX,dword ptr [ECX + 0x160]
0050bc49     52                       PUSH       EDX
0050bc4a     E8 01 87 03 00           CALL       0x00544350
0050bc4f     8B 44 24 48              MOV        EAX,dword ptr [ESP + 0x48]
0050bc53     83 C4 10                 ADD        ESP,0x10
0050bc56     68 E0 3D 57 00           PUSH       0x573de0
0050bc5b     50                       PUSH       EAX
0050bc5c     E8 3F 87 03 00           CALL       0x005443a0
0050bc61     66 8B 0D E6 3D 57 00     MOV        CX,word ptr [0x00573de6]
0050bc68     83 C4 08                 ADD        ESP,0x8
0050bc6b     66 83 C1 52              ADD        CX,0x52
0050bc6f     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bc73     C7 44 24 1C 00 00 00 01  MOV        dword ptr [ESP + 0x1c],0x1000000
0050bc7b     66 89 4C 24 22           MOV        word ptr [ESP + 0x22],CX
0050bc80     33 F6                    XOR        ESI,ESI
0050bc82     BF 1C 00 00 00           MOV        EDI,0x1c
0050bc87     BB 30 6C 56 00           MOV        EBX,0x566c30
0050bc8c     8D 54 24 38              LEA        EDX,[ESP + 0x38]
0050bc90     52                       PUSH       EDX
0050bc91     55                       PUSH       EBP
0050bc92     E8 89 01 01 00           CALL       0x0051be20
0050bc97     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
0050bc9c     83 C4 08                 ADD        ESP,0x8
0050bc9f     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
0050bca5     8D 46 0B                 LEA        EAX,[ESI + 0xb]
0050bca8     6A 00                    PUSH       0x0
0050bcaa     6A 00                    PUSH       0x0
0050bcac     8B 51 30                 MOV        EDX,dword ptr [ECX + 0x30]
0050bcaf     8B CE                    MOV        ECX,ESI
0050bcb1     80 E1 01                 AND        CL,0x1
0050bcb4     66 89 44 24 30           MOV        word ptr [ESP + 0x30],AX
0050bcb9     A1 E4 3D 57 00           MOV        EAX,[0x00573de4]
0050bcbe     89 54 24 20              MOV        dword ptr [ESP + 0x20],EDX
0050bcc2     F6 D9                    NEG        CL
0050bcc4     1B C9                    SBB        ECX,ECX
0050bcc6     8D 54 24 20              LEA        EDX,[ESP + 0x20]
0050bcca     83 E1 38                 AND        ECX,0x38
0050bccd     52                       PUSH       EDX
0050bcce     83 C1 12                 ADD        ECX,0x12
0050bcd1     03 C8                    ADD        ECX,EAX
0050bcd3     8B 44 24 44              MOV        EAX,dword ptr [ESP + 0x44]
0050bcd7     50                       PUSH       EAX
0050bcd8     55                       PUSH       EBP
0050bcd9     66 89 4C 24 34           MOV        word ptr [ESP + 0x34],CX
0050bcde     E8 0D 61 01 00           CALL       0x00521df0
0050bce3     83 C4 14                 ADD        ESP,0x14
0050bce6     8D 4C 24 18              LEA        ECX,[ESP + 0x18]
0050bcea     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bcee     51                       PUSH       ECX
0050bcef     50                       PUSH       EAX
0050bcf0     55                       PUSH       EBP
0050bcf1     E8 CA 3C 01 00           CALL       0x0051f9c0
0050bcf6     83 C4 0C                 ADD        ESP,0xc
0050bcf9     8D 54 24 38              LEA        EDX,[ESP + 0x38]
0050bcfd     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bd01     52                       PUSH       EDX
0050bd02     55                       PUSH       EBP
0050bd03     E8 A8 00 01 00           CALL       0x0051bdb0
0050bd08     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050bd0c     83 C4 08                 ADD        ESP,0x8
0050bd0f     66 01 7C 24 20           ADD        word ptr [ESP + 0x20],DI
0050bd14     33 C0                    XOR        EAX,EAX
0050bd16     8A 86 67 90 5E 00        MOV        AL,byte ptr [ESI + 0x5e9067]
0050bd1c     6A 00                    PUSH       0x0
0050bd1e     68 82 00 00 00           PUSH       0x82
0050bd23     8D 4C 24 20              LEA        ECX,[ESP + 0x20]
0050bd27     50                       PUSH       EAX
0050bd28     51                       PUSH       ECX
0050bd29     52                       PUSH       EDX
0050bd2a     55                       PUSH       EBP
0050bd2b     89 5C 24 30              MOV        dword ptr [ESP + 0x30],EBX
0050bd2f     E8 5C 01 01 00           CALL       0x0051be90
0050bd34     83 C4 18                 ADD        ESP,0x18
0050bd37     46                       INC        ESI
0050bd38     66 FF 44 24 28           INC        word ptr [ESP + 0x28]
0050bd3d     83 FE 02                 CMP        ESI,0x2
0050bd40     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bd44     0F 8C 42 FF FF FF        JL         0x0050bc8c
0050bd4a     66 A1 E6 3D 57 00        MOV        AX,[0x00573de6]
0050bd50     66 05 60 00              ADD        AX,0x60
0050bd54     33 FF                    XOR        EDI,EDI
0050bd56     66 89 44 24 22           MOV        word ptr [ESP + 0x22],AX
0050bd5b     8D 4C 24 38              LEA        ECX,[ESP + 0x38]
0050bd5f     51                       PUSH       ECX
0050bd60     55                       PUSH       EBP
0050bd61     E8 BA 00 01 00           CALL       0x0051be20
0050bd66     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
0050bd6c     8B F7                    MOV        ESI,EDI
0050bd6e     83 E6 01                 AND        ESI,0x1
0050bd71     83 C4 08                 ADD        ESP,0x8
0050bd74     8B 82 94 00 00 00        MOV        EAX,dword ptr [EDX + 0x94]
0050bd7a     8D 57 04                 LEA        EDX,[EDI + 0x4]
0050bd7d     6A 00                    PUSH       0x0
0050bd7f     6A 00                    PUSH       0x0
0050bd81     8B 48 30                 MOV        ECX,dword ptr [EAX + 0x30]
0050bd84     8B C6                    MOV        EAX,ESI
0050bd86     F7 D8                    NEG        EAX
0050bd88     1B C0                    SBB        EAX,EAX
0050bd8a     66 89 54 24 30           MOV        word ptr [ESP + 0x30],DX
0050bd8f     8B 15 E4 3D 57 00        MOV        EDX,dword ptr [0x00573de4]
0050bd95     83 E0 38                 AND        EAX,0x38
0050bd98     83 C0 12                 ADD        EAX,0x12
0050bd9b     89 4C 24 20              MOV        dword ptr [ESP + 0x20],ECX
0050bd9f     03 C2                    ADD        EAX,EDX
0050bda1     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050bda5     8D 4C 24 20              LEA        ECX,[ESP + 0x20]
0050bda9     66 89 44 24 28           MOV        word ptr [ESP + 0x28],AX
0050bdae     51                       PUSH       ECX
0050bdaf     52                       PUSH       EDX
0050bdb0     55                       PUSH       EBP
0050bdb1     E8 3A 60 01 00           CALL       0x00521df0
0050bdb6     83 C4 14                 ADD        ESP,0x14
0050bdb9     8D 4C 24 18              LEA        ECX,[ESP + 0x18]
0050bdbd     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bdc1     51                       PUSH       ECX
0050bdc2     50                       PUSH       EAX
0050bdc3     55                       PUSH       EBP
0050bdc4     E8 F7 3B 01 00           CALL       0x0051f9c0
0050bdc9     83 C4 0C                 ADD        ESP,0xc
0050bdcc     8D 54 24 38              LEA        EDX,[ESP + 0x38]
0050bdd0     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050bdd4     52                       PUSH       EDX
0050bdd5     55                       PUSH       EBP
0050bdd6     E8 D5 FF 00 00           CALL       0x0051bdb0
0050bddb     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
0050bddf     83 C4 08                 ADD        ESP,0x8
0050bde2     66 83 44 24 20 1C        ADD        word ptr [ESP + 0x20],0x1c
0050bde8     33 C0                    XOR        EAX,EAX
0050bdea     8A 87 60 90 5E 00        MOV        AL,byte ptr [EDI + 0x5e9060]
0050bdf0     6A 00                    PUSH       0x0
0050bdf2     68 82 00 00 00           PUSH       0x82
0050bdf7     8D 4C 24 20              LEA        ECX,[ESP + 0x20]
0050bdfb     50                       PUSH       EAX
0050bdfc     51                       PUSH       ECX
0050bdfd     52                       PUSH       EDX
0050bdfe     55                       PUSH       EBP
0050bdff     89 5C 24 30              MOV        dword ptr [ESP + 0x30],EBX
0050be03     E8 88 00 01 00           CALL       0x0051be90
0050be08     83 C4 18                 ADD        ESP,0x18
0050be0b     89 44 24 38              MOV        dword ptr [ESP + 0x38],EAX
0050be0f     F7 DE                    NEG        ESI
0050be11     1B F6                    SBB        ESI,ESI
0050be13     83 E6 09                 AND        ESI,0x9
0050be16     66 01 74 24 22           ADD        word ptr [ESP + 0x22],SI
0050be1b     47                       INC        EDI
0050be1c     66 FF 44 24 28           INC        word ptr [ESP + 0x28]
0050be21     83 FF 07                 CMP        EDI,0x7
0050be24     0F 8C 31 FF FF FF        JL         0x0050bd5b
0050be2a     68 E0 3D 57 00           PUSH       0x573de0
0050be2f     50                       PUSH       EAX
0050be30     55                       PUSH       EBP
0050be31     E8 0A 95 03 00           CALL       0x00545340
0050be36     83 C4 0C                 ADD        ESP,0xc
0050be39     5F                       POP        EDI
0050be3a     5E                       POP        ESI
0050be3b     5D                       POP        EBP
0050be3c     5B                       POP        EBX
0050be3d     83 C4 1C                 ADD        ESP,0x1c
0050be40     C3                       RET        

