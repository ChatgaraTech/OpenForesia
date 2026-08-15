; Function: FUN_0050a910
; Entry:    0050a910

0050a910     A1 48 2A 56 00           MOV EAX,[0x00562a48]
0050a915     83 EC 1C                 SUB ESP,0x1c
0050a918     83 CA FF                 OR EDX,0xffffffff
0050a91b     53                       PUSH EBX
0050a91c     55                       PUSH EBP
0050a91d     56                       PUSH ESI
0050a91e     57                       PUSH EDI
0050a91f     33 FF                    XOR EDI,EDI
0050a921     3B C7                    CMP EAX,EDI
0050a923     0F 84 9C 00 00 00        JZ 0x0050a9c5
0050a929     B9 18 00 00 00           MOV ECX,0x18
0050a92e     B8 88 00 00 00           MOV EAX,0x88
0050a933     89 3D 48 2A 56 00        MOV dword ptr [0x00562a48],EDI
0050a939     66 C7 05 1C 35 57 00 A0 00 MOV word ptr [0x0057351c],0xa0
0050a942     66 89 0D 1E 35 57 00     MOV word ptr [0x0057351e],CX
0050a949     66 C7 05 20 35 57 00 68 00 MOV word ptr [0x00573520],0x68
0050a952     66 A3 22 35 57 00        MOV [0x00573522],AX
0050a958     C6 05 3B 35 57 00 00     MOV byte ptr [0x0057353b],0x0
0050a95f     66 C7 05 E4 3D 57 00 28 00 MOV word ptr [0x00573de4],0x28
0050a968     66 89 0D E6 3D 57 00     MOV word ptr [0x00573de6],CX
0050a96f     66 C7 05 E8 3D 57 00 78 00 MOV word ptr [0x00573de8],0x78
0050a978     66 A3 EA 3D 57 00        MOV [0x00573dea],AX
0050a97e     C6 05 F4 3D 57 00 03     MOV byte ptr [0x00573df4],0x3
0050a985     C6 05 F2 3D 57 00 1A     MOV byte ptr [0x00573df2],0x1a
0050a98c     C6 05 F3 3D 57 00 06     MOV byte ptr [0x00573df3],0x6
0050a993     C6 05 02 3E 57 00 07     MOV byte ptr [0x00573e02],0x7
0050a99a     C6 05 03 3E 57 00 01     MOV byte ptr [0x00573e03],0x1
0050a9a1     89 3D 50 2A 56 00        MOV dword ptr [0x00562a50],EDI
0050a9a7     89 15 F0 8D 57 00        MOV dword ptr [0x00578df0],EDX
0050a9ad     89 15 68 2B 56 00        MOV dword ptr [0x00562b68],EDX
0050a9b3     89 3D 0C 8E 57 00        MOV dword ptr [0x00578e0c],EDI
0050a9b9     89 3D 10 8E 57 00        MOV dword ptr [0x00578e10],EDI
0050a9bf     89 3D 14 8E 57 00        MOV dword ptr [0x00578e14],EDI
0050a9c5     8B 0D 68 2B 56 00        MOV ECX,dword ptr [0x00562b68]
0050a9cb     A1 18 8E 57 00           MOV EAX,[0x00578e18]
0050a9d0     3B CF                    CMP ECX,EDI
0050a9d2     8B 4C 24 3C              MOV ECX,dword ptr [ESP + 0x3c]
0050a9d6     8B F0                    MOV ESI,EAX
0050a9d8     7D 31                    JGE 0x0050aa0b
0050a9da     F6 41 06 80              TEST byte ptr [ECX + 0x6],0x80
0050a9de     74 06                    JZ 0x0050a9e6
0050a9e0     48                       DEC EAX
0050a9e1     A3 18 8E 57 00           MOV [0x00578e18],EAX
0050a9e6     F6 41 06 20              TEST byte ptr [ECX + 0x6],0x20
0050a9ea     74 06                    JZ 0x0050a9f2
0050a9ec     40                       INC EAX
0050a9ed     A3 18 8E 57 00           MOV [0x00578e18],EAX
0050a9f2     57                       PUSH EDI
0050a9f3     68 28 07 56 00           PUSH 0x560728
0050a9f8     57                       PUSH EDI
0050a9f9     68 40 2B 57 00           PUSH 0x572b40
0050a9fe     E8 4D 99 03 00           CALL 0x00544350
0050aa03     A1 18 8E 57 00           MOV EAX,[0x00578e18]
0050aa08     83 C4 10                 ADD ESP,0x10
0050aa0b     BA 00 80 00 00           MOV EDX,0x8000
0050aa10     33 C9                    XOR ECX,ECX
0050aa12     66 39 15 0A 4B 60 00     CMP word ptr [0x00604b0a],DX
0050aa19     89 0D 20 8E 57 00        MOV dword ptr [0x00578e20],ECX
0050aa1f     75 0B                    JNZ 0x0050aa2c
0050aa21     B9 01 00 00 00           MOV ECX,0x1
0050aa26     89 0D 20 8E 57 00        MOV dword ptr [0x00578e20],ECX
0050aa2c     66 39 15 2E 4B 60 00     CMP word ptr [0x00604b2e],DX
0050aa33     75 09                    JNZ 0x0050aa3e
0050aa35     83 C1 02                 ADD ECX,0x2
0050aa38     89 0D 20 8E 57 00        MOV dword ptr [0x00578e20],ECX
0050aa3e     3B C7                    CMP EAX,EDI
0050aa40     7D 10                    JGE 0x0050aa52
0050aa42     8B 04 8D 30 2B 56 00     MOV EAX,dword ptr [ECX*0x4 + 0x562b30]
0050aa49     0F BE 00                 MOVSX EAX,byte ptr [EAX]
0050aa4c     48                       DEC EAX
0050aa4d     A3 18 8E 57 00           MOV [0x00578e18],EAX
0050aa52     8B 0C 8D 30 2B 56 00     MOV ECX,dword ptr [ECX*0x4 + 0x562b30]
0050aa59     0F BE 11                 MOVSX EDX,byte ptr [ECX]
0050aa5c     3B C2                    CMP EAX,EDX
0050aa5e     7C 07                    JL 0x0050aa67
0050aa60     33 C0                    XOR EAX,EAX
0050aa62     A3 18 8E 57 00           MOV [0x00578e18],EAX
0050aa67     3B F0                    CMP ESI,EAX
0050aa69     74 0A                    JZ 0x0050aa75
0050aa6b     E8 F0 30 03 00           CALL 0x0053db60
0050aa70     A1 18 8E 57 00           MOV EAX,[0x00578e18]
0050aa75     39 3D 68 2B 56 00        CMP dword ptr [0x00562b68],EDI
0050aa7b     7D 29                    JGE 0x0050aaa6
0050aa7d     A3 68 2B 56 00           MOV [0x00562b68],EAX
0050aa82     89 3D 3C 33 57 00        MOV dword ptr [0x0057333c],EDI
0050aa88     89 3D E0 8D 57 00        MOV dword ptr [0x00578de0],EDI
0050aa8e     89 3D 60 49 60 00        MOV dword ptr [0x00604960],EDI
0050aa94     89 3D 0C 8E 57 00        MOV dword ptr [0x00578e0c],EDI
0050aa9a     66 89 3D 8A 49 60 00     MOV word ptr [0x0060498a],DI
0050aaa1     E8 DA 30 03 00           CALL 0x0053db80
0050aaa6     A1 0C 8E 57 00           MOV EAX,[0x00578e0c]
0050aaab     8B 0D 10 8E 57 00        MOV ECX,dword ptr [0x00578e10]
0050aab1     3B C8                    CMP ECX,EAX
0050aab3     74 0F                    JZ 0x0050aac4
0050aab5     A3 10 8E 57 00           MOV [0x00578e10],EAX
0050aaba     C7 05 14 8E 57 00 01 00 00 00 MOV dword ptr [0x00578e14],0x1
0050aac4     A1 68 2B 56 00           MOV EAX,[0x00562b68]
0050aac9     8B 6C 24 30              MOV EBP,dword ptr [ESP + 0x30]
0050aacd     3B C7                    CMP EAX,EDI
0050aacf     0F 8C 2F 0F 00 00        JL 0x0050ba04
0050aad5     39 3D 4C 2A 56 00        CMP dword ptr [0x00562a4c],EDI
0050aadb     0F 85 3A 0C 00 00        JNZ 0x0050b71b
0050aae1     A1 3C 33 57 00           MOV EAX,[0x0057333c]
0050aae6     2B C7                    SUB EAX,EDI
0050aae8     0F 84 A1 00 00 00        JZ 0x0050ab8f
0050aaee     48                       DEC EAX
0050aaef     74 6B                    JZ 0x0050ab5c
0050aaf1     48                       DEC EAX
0050aaf2     0F 85 97 00 00 00        JNZ 0x0050ab8f
0050aaf8     A1 80 49 60 00           MOV EAX,[0x00604980]
0050aafd     25 FF 00 00 00           AND EAX,0xff
0050ab02     8B C8                    MOV ECX,EAX
0050ab04     83 E0 04                 AND EAX,0x4
0050ab07     83 E1 03                 AND ECX,0x3
0050ab0a     8D 14 81                 LEA EDX,[ECX + EAX*0x4]
0050ab0d     52                       PUSH EDX
0050ab0e     E8 45 AF 03 00           CALL 0x00545a58
0050ab13     85 C0                    TEST EAX,EAX
0050ab15     75 1F                    JNZ 0x0050ab36
0050ab17     A1 80 49 60 00           MOV EAX,[0x00604980]
0050ab1c     25 FF 00 00 00           AND EAX,0xff
0050ab21     8B C8                    MOV ECX,EAX
0050ab23     83 E0 04                 AND EAX,0x4
0050ab26     83 E1 03                 AND ECX,0x3
0050ab29     8D 14 81                 LEA EDX,[ECX + EAX*0x4]
0050ab2c     52                       PUSH EDX
0050ab2d     E8 26 AF 03 00           CALL 0x00545a58
0050ab32     85 C0                    TEST EAX,EAX
0050ab34     74 E1                    JZ 0x0050ab17
0050ab36     A1 80 49 60 00           MOV EAX,[0x00604980]
0050ab3b     57                       PUSH EDI
0050ab3c     25 FF 00 00 00           AND EAX,0xff
0050ab41     8B C8                    MOV ECX,EAX
0050ab43     83 E0 04                 AND EAX,0x4
0050ab46     83 E1 03                 AND ECX,0x3
0050ab49     8D 14 81                 LEA EDX,[ECX + EAX*0x4]
0050ab4c     52                       PUSH EDX
0050ab4d     E8 CE 9C FF FF           CALL 0x00504820
0050ab52     83 C4 08                 ADD ESP,0x8
0050ab55     A3 E0 8D 57 00           MOV [0x00578de0],EAX
0050ab5a     EB 33                    JMP 0x0050ab8f
0050ab5c     A1 80 49 60 00           MOV EAX,[0x00604980]
0050ab61     57                       PUSH EDI
0050ab62     25 FF 00 00 00           AND EAX,0xff
0050ab67     8B C8                    MOV ECX,EAX
0050ab69     83 E0 04                 AND EAX,0x4
0050ab6c     83 E1 03                 AND ECX,0x3
0050ab6f     8D 14 81                 LEA EDX,[ECX + EAX*0x4]
0050ab72     52                       PUSH EDX
0050ab73     E8 A8 9C FF FF           CALL 0x00504820
0050ab78     83 C4 08                 ADD ESP,0x8
0050ab7b     83 F8 FE                 CMP EAX,-0x2
0050ab7e     A3 E0 8D 57 00           MOV [0x00578de0],EAX
0050ab83     7F 0A                    JG 0x0050ab8f
0050ab85     C7 05 0C 8E 57 00 FC FF FF FF MOV dword ptr [0x00578e0c],0xfffffffc
0050ab8f     A1 0C 8E 57 00           MOV EAX,[0x00578e0c]
0050ab94     83 C0 05                 ADD EAX,0x5
0050ab97     83 F8 68                 CMP EAX,0x68
0050ab9a     0F 87 7B 0B 00 00        JA 0x0050b71b
0050aba0     8A 0D EF 0F 56 00        MOV CL,byte ptr [0x00560fef]
0050aba6     8A 15 9F 90 5E 00        MOV DL,byte ptr [0x005e909f]
0050abac     8B 74 24 3C              MOV ESI,dword ptr [ESP + 0x3c]
0050abb0     BF 00 40 00 00           MOV EDI,0x4000
0050abb5     33 DB                    XOR EBX,EBX
0050abb7     8A 98 A8 BE 50 00        MOV BL,byte ptr [EAX + 0x50bea8]
0050abbd     FF 24 9D 44 BE 50 00     JMP dword ptr [EBX*0x4 + 0x50be44]
0050b71b     A1 68 2B 56 00           MOV EAX,[0x00562b68]
0050b720     33 FF                    XOR EDI,EDI
0050b722     3B C7                    CMP EAX,EDI
0050b724     0F 8C DA 02 00 00        JL 0x0050ba04
0050b72a     A1 0C 8E 57 00           MOV EAX,[0x00578e0c]
0050b72f     83 F8 0A                 CMP EAX,0xa
0050b732     0F 8C CC 02 00 00        JL 0x0050ba04
0050b738     83 F8 0D                 CMP EAX,0xd
0050b73b     0F 8F C3 02 00 00        JG 0x0050ba04
0050b741     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
0050b746     C7 44 24 1C 00 00 00 01  MOV dword ptr [ESP + 0x1c],0x1000000
0050b74e     57                       PUSH EDI
0050b74f     57                       PUSH EDI
0050b750     8B 88 94 00 00 00        MOV ECX,dword ptr [EAX + 0x94]
0050b756     8D 44 24 20              LEA EAX,[ESP + 0x20]
0050b75a     50                       PUSH EAX
0050b75b     8B 51 38                 MOV EDX,dword ptr [ECX + 0x38]
0050b75e     8B 4C 24 44              MOV ECX,dword ptr [ESP + 0x44]
0050b762     51                       PUSH ECX
0050b763     55                       PUSH EBP
0050b764     89 54 24 2C              MOV dword ptr [ESP + 0x2c],EDX
0050b768     66 89 7C 24 3C           MOV word ptr [ESP + 0x3c],DI
0050b76d     66 C7 44 24 34 B4 00     MOV word ptr [ESP + 0x34],0xb4
0050b774     66 C7 44 24 36 30 00     MOV word ptr [ESP + 0x36],0x30
0050b77b     E8 70 66 01 00           CALL 0x00521df0
0050b780     83 C4 14                 ADD ESP,0x14
0050b783     8D 54 24 18              LEA EDX,[ESP + 0x18]
0050b787     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050b78b     66 C7 44 24 28 01 00     MOV word ptr [ESP + 0x28],0x1
0050b792     57                       PUSH EDI
0050b793     57                       PUSH EDI
0050b794     52                       PUSH EDX
0050b795     50                       PUSH EAX
0050b796     55                       PUSH EBP
0050b797     66 C7 44 24 34 E8 00     MOV word ptr [ESP + 0x34],0xe8
0050b79e     E8 4D 66 01 00           CALL 0x00521df0
0050b7a3     83 C4 14                 ADD ESP,0x14
0050b7a6     8D 4C 24 18              LEA ECX,[ESP + 0x18]
0050b7aa     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050b7ae     51                       PUSH ECX
0050b7af     50                       PUSH EAX
0050b7b0     55                       PUSH EBP
0050b7b1     E8 0A 42 01 00           CALL 0x0051f9c0
0050b7b6     8B 0D 84 49 60 00        MOV ECX,dword ptr [0x00604984]
0050b7bc     83 C4 0C                 ADD ESP,0xc
0050b7bf     81 E1 FF 00 00 00        AND ECX,0xff
0050b7c5     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050b7c9     C7 44 24 18 30 6C 56 00  MOV dword ptr [ESP + 0x18],0x566c30
0050b7d1     66 C7 44 24 20 BC 00     MOV word ptr [ESP + 0x20],0xbc
0050b7d8     8D 14 89                 LEA EDX,[ECX + ECX*0x4]
0050b7db     8B 0D BC 49 60 00        MOV ECX,dword ptr [0x006049bc]
0050b7e1     66 C7 44 24 22 38 00     MOV word ptr [ESP + 0x22],0x38
0050b7e8     57                       PUSH EDI
0050b7e9     8B 54 D1 18              MOV EDX,dword ptr [ECX + EDX*0x8 + 0x18]
0050b7ed     68 82 00 00 00           PUSH 0x82
0050b7f2     8D 4C 24 20              LEA ECX,[ESP + 0x20]
0050b7f6     52                       PUSH EDX
0050b7f7     51                       PUSH ECX
0050b7f8     50                       PUSH EAX
0050b7f9     55                       PUSH EBP
0050b7fa     E8 91 06 01 00           CALL 0x0051be90
0050b7ff     83 C4 18                 ADD ESP,0x18
0050b802     33 D2                    XOR EDX,EDX
0050b804     8A 15 82 49 60 00        MOV DL,byte ptr [0x00604982]
0050b80a     8D 4C 24 18              LEA ECX,[ESP + 0x18]
0050b80e     57                       PUSH EDI
0050b80f     68 82 00 00 00           PUSH 0x82
0050b814     52                       PUSH EDX
0050b815     51                       PUSH ECX
0050b816     50                       PUSH EAX
0050b817     55                       PUSH EBP
0050b818     89 44 24 50              MOV dword ptr [ESP + 0x50],EAX
0050b81c     66 C7 44 24 38 EC 00     MOV word ptr [ESP + 0x38],0xec
0050b823     E8 68 06 01 00           CALL 0x0051be90
0050b828     33 D2                    XOR EDX,EDX
0050b82a     8B F0                    MOV ESI,EAX
0050b82c     8A 15 81 49 60 00        MOV DL,byte ptr [0x00604981]
0050b832     66 89 7C 24 40           MOV word ptr [ESP + 0x40],DI
0050b837     B8 00 10 00 00           MOV EAX,0x1000
0050b83c     8B FA                    MOV EDI,EDX
0050b83e     99                       CDQ
0050b83f     83 C4 18                 ADD ESP,0x18
0050b842     8B 0D 84 49 60 00        MOV ECX,dword ptr [0x00604984]
0050b848     F7 FF                    IDIV EDI
0050b84a     89 74 24 38              MOV dword ptr [ESP + 0x38],ESI
0050b84e     66 C7 05 F8 8F 5E 00 0F 00 MOV word ptr [0x005e8ff8],0xf
0050b857     66 C7 05 FA 8F 5E 00 80 7F MOV word ptr [0x005e8ffa],0x7f80
0050b860     C7 44 24 18 48 31 56 00  MOV dword ptr [ESP + 0x18],0x563148
0050b868     8B D0                    MOV EDX,EAX
0050b86a     8B C1                    MOV EAX,ECX
0050b86c     25 FF 00 00 00           AND EAX,0xff
0050b871     0F AF D0                 IMUL EDX,EAX
0050b874     A1 88 49 60 00           MOV EAX,[0x00604988]
0050b879     2B C2                    SUB EAX,EDX
0050b87b     25 FF 0F 00 00           AND EAX,0xfff
0050b880     3D D0 07 00 00           CMP EAX,0x7d0
0050b885     7D 07                    JGE 0x0050b88e
0050b887     C1 F8 02                 SAR EAX,0x2
0050b88a     8B D0                    MOV EDX,EAX
0050b88c     EB 0C                    JMP 0x0050b89a
0050b88e     BA 00 10 00 00           MOV EDX,0x1000
0050b893     2B D0                    SUB EDX,EAX
0050b895     C1 FA 02                 SAR EDX,0x2
0050b898     F7 DA                    NEG EDX
0050b89a     66 A1 8A 49 60 00        MOV AX,[0x0060498a]
0050b8a0     66 29 15 88 49 60 00     SUB word ptr [0x00604988],DX
0050b8a7     0F BF D0                 MOVSX EDX,AX
0050b8aa     BB 00 18 00 00           MOV EBX,0x1800
0050b8af     89 7C 24 3C              MOV dword ptr [ESP + 0x3c],EDI
0050b8b3     2B DA                    SUB EBX,EDX
0050b8b5     C1 FB 02                 SAR EBX,0x2
0050b8b8     66 03 C3                 ADD AX,BX
0050b8bb     85 FF                    TEST EDI,EDI
0050b8bd     66 A3 8A 49 60 00        MOV [0x0060498a],AX
0050b8c3     0F 84 28 01 00 00        JZ 0x0050b9f1
0050b8c9     EB 06                    JMP 0x0050b8d1
0050b8cb     8B 0D 84 49 60 00        MOV ECX,dword ptr [0x00604984]
0050b8d1     33 C0                    XOR EAX,EAX
0050b8d3     81 E1 FF 00 00 00        AND ECX,0xff
0050b8d9     A0 81 49 60 00           MOV AL,[0x00604981]
0050b8de     8B F0                    MOV ESI,EAX
0050b8e0     8B 44 24 3C              MOV EAX,dword ptr [ESP + 0x3c]
0050b8e4     03 C1                    ADD EAX,ECX
0050b8e6     99                       CDQ
0050b8e7     F7 FE                    IDIV ESI
0050b8e9     B8 00 10 00 00           MOV EAX,0x1000
0050b8ee     0F BF 0D 88 49 60 00     MOVSX ECX,word ptr [0x00604988]
0050b8f5     8B DA                    MOV EBX,EDX
0050b8f7     99                       CDQ
0050b8f8     F7 FE                    IDIV ESI
0050b8fa     8B F0                    MOV ESI,EAX
0050b8fc     0F AF F3                 IMUL ESI,EBX
0050b8ff     2B F1                    SUB ESI,ECX
0050b901     56                       PUSH ESI
0050b902     E8 15 A1 03 00           CALL 0x00545a1c
0050b907     0F BF 15 8A 49 60 00     MOVSX EDX,word ptr [0x0060498a]
0050b90e     0F AF C2                 IMUL EAX,EDX
0050b911     C1 F8 14                 SAR EAX,0x14
0050b914     05 D3 00 00 00           ADD EAX,0xd3
0050b919     56                       PUSH ESI
0050b91a     66 89 44 24 24           MOV word ptr [ESP + 0x24],AX
0050b91f     E8 F2 A0 03 00           CALL 0x00545a16
0050b924     0F BF 0D 8A 49 60 00     MOVSX ECX,word ptr [0x0060498a]
0050b92b     0F AF C1                 IMUL EAX,ECX
0050b92e     8B 35 60 99 5E 00        MOV ESI,dword ptr [0x005e9960]
0050b934     BF 64 00 00 00           MOV EDI,0x64
0050b939     C1 F8 14                 SAR EAX,0x14
0050b93c     8A D3                    MOV DL,BL
0050b93e     2B F8                    SUB EDI,EAX
0050b940     C0 E2 04                 SHL DL,0x4
0050b943     33 C9                    XOR ECX,ECX
0050b945     8B C6                    MOV EAX,ESI
0050b947     8A 8B 8C 49 60 00        MOV CL,byte ptr [EBX + 0x60498c]
0050b94d     88 15 48 31 56 00        MOV byte ptr [0x00563148],DL
0050b953     C1 E8 02                 SHR EAX,0x2
0050b956     33 D2                    XOR EDX,EDX
0050b958     66 89 7C 24 22           MOV word ptr [ESP + 0x22],DI
0050b95d     F7 F1                    DIV ECX
0050b95f     8B 44 24 3C              MOV EAX,dword ptr [ESP + 0x3c]
0050b963     80 C2 02                 ADD DL,0x2
0050b966     C0 E2 04                 SHL DL,0x4
0050b969     88 15 49 31 56 00        MOV byte ptr [0x00563149],DL
0050b96f     33 D2                    XOR EDX,EDX
0050b971     8A 15 81 49 60 00        MOV DL,byte ptr [0x00604981]
0050b977     3B C2                    CMP EAX,EDX
0050b979     75 4B                    JNZ 0x0050b9c6
0050b97b     66 83 44 24 20 08        ADD word ptr [ESP + 0x20],0x8
0050b981     83 E6 07                 AND ESI,0x7
0050b984     C1 E6 09                 SHL ESI,0x9
0050b987     66 83 C7 08              ADD DI,0x8
0050b98b     56                       PUSH ESI
0050b98c     66 89 7C 24 26           MOV word ptr [ESP + 0x26],DI
0050b991     E8 86 A0 03 00           CALL 0x00545a1c
0050b996     8B 54 24 38              MOV EDX,dword ptr [ESP + 0x38]
0050b99a     53                       PUSH EBX
0050b99b     C1 F8 02                 SAR EAX,0x2
0050b99e     05 00 14 00 00           ADD EAX,0x1400
0050b9a3     8D 4C 24 1C              LEA ECX,[ESP + 0x1c]
0050b9a7     6A 00                    PUSH 0x0
0050b9a9     66 89 44 24 2C           MOV word ptr [ESP + 0x2c],AX
0050b9ae     66 89 44 24 2E           MOV word ptr [ESP + 0x2e],AX
0050b9b3     A1 14 98 5E 00           MOV EAX,[0x005e9814]
0050b9b8     51                       PUSH ECX
0050b9b9     52                       PUSH EDX
0050b9ba     55                       PUSH EBP
0050b9bb     89 44 24 30              MOV dword ptr [ESP + 0x30],EAX
0050b9bf     E8 5C 65 01 00           CALL 0x00521f20
0050b9c4     EB 13                    JMP 0x0050b9d9
0050b9c6     8B 4C 24 38              MOV ECX,dword ptr [ESP + 0x38]
0050b9ca     53                       PUSH EBX
0050b9cb     8D 44 24 1C              LEA EAX,[ESP + 0x1c]
0050b9cf     6A 03                    PUSH 0x3
0050b9d1     50                       PUSH EAX
0050b9d2     51                       PUSH ECX
0050b9d3     55                       PUSH EBP
0050b9d4     E8 17 64 01 00           CALL 0x00521df0
0050b9d9     8B F0                    MOV ESI,EAX
0050b9db     8B 44 24 50              MOV EAX,dword ptr [ESP + 0x50]
0050b9df     83 C4 14                 ADD ESP,0x14
0050b9e2     48                       DEC EAX
0050b9e3     89 74 24 38              MOV dword ptr [ESP + 0x38],ESI
0050b9e7     89 44 24 3C              MOV dword ptr [ESP + 0x3c],EAX
0050b9eb     0F 85 DA FE FF FF        JNZ 0x0050b8cb
0050b9f1     8D 54 24 18              LEA EDX,[ESP + 0x18]
0050b9f5     52                       PUSH EDX
0050b9f6     56                       PUSH ESI
0050b9f7     55                       PUSH EBP
0050b9f8     E8 C3 3F 01 00           CALL 0x0051f9c0
0050b9fd     83 C4 0C                 ADD ESP,0xc
0050ba00     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050ba04     8D 44 24 38              LEA EAX,[ESP + 0x38]
0050ba08     50                       PUSH EAX
0050ba09     55                       PUSH EBP
0050ba0a     E8 11 04 01 00           CALL 0x0051be20
0050ba0f     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050ba15     83 C4 08                 ADD ESP,0x8
0050ba18     8B 91 94 00 00 00        MOV EDX,dword ptr [ECX + 0x94]
0050ba1e     8B 42 34                 MOV EAX,dword ptr [EDX + 0x34]
0050ba21     89 44 24 18              MOV dword ptr [ESP + 0x18],EAX
0050ba25     A1 68 2B 56 00           MOV EAX,[0x00562b68]
0050ba2a     85 C0                    TEST EAX,EAX
0050ba2c     7C 17                    JL 0x0050ba45
0050ba2e     A1 0C 8E 57 00           MOV EAX,[0x00578e0c]
0050ba33     83 F8 0A                 CMP EAX,0xa
0050ba36     7C 0D                    JL 0x0050ba45
0050ba38     83 F8 0D                 CMP EAX,0xd
0050ba3b     C7 44 24 1C 40 40 40 00  MOV dword ptr [ESP + 0x1c],0x404040
0050ba43     7E 08                    JLE 0x0050ba4d
0050ba45     C7 44 24 1C 00 00 00 01  MOV dword ptr [ESP + 0x1c],0x1000000
0050ba4d     BF 00 80 00 00           MOV EDI,0x8000
0050ba52     BE 6E 00 00 00           MOV ESI,0x6e
0050ba57     66 39 3D 0A 4B 60 00     CMP word ptr [0x00604b0a],DI
0050ba5e     75 2E                    JNZ 0x0050ba8e
0050ba60     8B 54 24 38              MOV EDX,dword ptr [ESP + 0x38]
0050ba64     6A 00                    PUSH 0x0
0050ba66     8D 4C 24 1C              LEA ECX,[ESP + 0x1c]
0050ba6a     6A 00                    PUSH 0x0
0050ba6c     51                       PUSH ECX
0050ba6d     52                       PUSH EDX
0050ba6e     55                       PUSH EBP
0050ba6f     66 C7 44 24 3C 01 00     MOV word ptr [ESP + 0x3c],0x1
0050ba76     66 C7 44 24 34 A9 00     MOV word ptr [ESP + 0x34],0xa9
0050ba7d     66 89 74 24 36           MOV word ptr [ESP + 0x36],SI
0050ba82     E8 69 63 01 00           CALL 0x00521df0
0050ba87     83 C4 14                 ADD ESP,0x14
0050ba8a     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050ba8e     66 39 3D 2E 4B 60 00     CMP word ptr [0x00604b2e],DI
0050ba95     75 2E                    JNZ 0x0050bac5
0050ba97     8B 4C 24 38              MOV ECX,dword ptr [ESP + 0x38]
0050ba9b     6A 00                    PUSH 0x0
0050ba9d     8D 44 24 1C              LEA EAX,[ESP + 0x1c]
0050baa1     6A 00                    PUSH 0x0
0050baa3     50                       PUSH EAX
0050baa4     51                       PUSH ECX
0050baa5     55                       PUSH EBP
0050baa6     66 C7 44 24 3C 02 00     MOV word ptr [ESP + 0x3c],0x2
0050baad     66 C7 44 24 34 DD 00     MOV word ptr [ESP + 0x34],0xdd
0050bab4     66 89 74 24 36           MOV word ptr [ESP + 0x36],SI
0050bab9     E8 32 63 01 00           CALL 0x00521df0
0050babe     83 C4 14                 ADD ESP,0x14
0050bac1     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bac5     8D 54 24 38              LEA EDX,[ESP + 0x38]
0050bac9     66 C7 44 24 28 00 00     MOV word ptr [ESP + 0x28],0x0
0050bad0     52                       PUSH EDX
0050bad1     55                       PUSH EBP
0050bad2     66 C7 44 24 28 BD 00     MOV word ptr [ESP + 0x28],0xbd
0050bad9     66 C7 44 24 2A 48 00     MOV word ptr [ESP + 0x2a],0x48
0050bae0     E8 CB 02 01 00           CALL 0x0051bdb0
0050bae5     8B 44 24 40              MOV EAX,dword ptr [ESP + 0x40]
0050bae9     83 C4 08                 ADD ESP,0x8
0050baec     68 18 35 57 00           PUSH 0x573518
0050baf1     50                       PUSH EAX
0050baf2     55                       PUSH EBP
0050baf3     E8 48 98 03 00           CALL 0x00545340
0050baf8     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050bafe     83 C4 0C                 ADD ESP,0xc
0050bb01     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bb05     8B 51 70                 MOV EDX,dword ptr [ECX + 0x70]
0050bb08     6A 00                    PUSH 0x0
0050bb0a     68 E0 3D 57 00           PUSH 0x573de0
0050bb0f     6A 00                    PUSH 0x0
0050bb11     8B 82 58 01 00 00        MOV EAX,dword ptr [EDX + 0x158]
0050bb17     50                       PUSH EAX
0050bb18     E8 33 88 03 00           CALL 0x00544350
0050bb1d     8B 4C 24 48              MOV ECX,dword ptr [ESP + 0x48]
0050bb21     83 C4 10                 ADD ESP,0x10
0050bb24     68 E0 3D 57 00           PUSH 0x573de0
0050bb29     51                       PUSH ECX
0050bb2a     E8 71 88 03 00           CALL 0x005443a0
0050bb2f     8B 15 38 33 57 00        MOV EDX,dword ptr [0x00573338]
0050bb35     83 C4 08                 ADD ESP,0x8
0050bb38     81 C2 00 01 00 00        ADD EDX,0x100
0050bb3e     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bb42     6A 00                    PUSH 0x0
0050bb44     68 E0 3D 57 00           PUSH 0x573de0
0050bb49     6A 00                    PUSH 0x0
0050bb4b     52                       PUSH EDX
0050bb4c     E8 FF 87 03 00           CALL 0x00544350
0050bb51     8B 44 24 48              MOV EAX,dword ptr [ESP + 0x48]
0050bb55     83 C4 10                 ADD ESP,0x10
0050bb58     68 E0 3D 57 00           PUSH 0x573de0
0050bb5d     50                       PUSH EAX
0050bb5e     E8 3D 88 03 00           CALL 0x005443a0
0050bb63     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050bb69     83 C4 08                 ADD ESP,0x8
0050bb6c     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bb70     8B 51 70                 MOV EDX,dword ptr [ECX + 0x70]
0050bb73     6A 00                    PUSH 0x0
0050bb75     68 E0 3D 57 00           PUSH 0x573de0
0050bb7a     6A 00                    PUSH 0x0
0050bb7c     8B 82 5C 01 00 00        MOV EAX,dword ptr [EDX + 0x15c]
0050bb82     50                       PUSH EAX
0050bb83     E8 C8 87 03 00           CALL 0x00544350
0050bb88     8B 4C 24 48              MOV ECX,dword ptr [ESP + 0x48]
0050bb8c     83 C4 10                 ADD ESP,0x10
0050bb8f     68 E0 3D 57 00           PUSH 0x573de0
0050bb94     51                       PUSH ECX
0050bb95     E8 06 88 03 00           CALL 0x005443a0
0050bb9a     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
0050bba0     89 44 24 40              MOV dword ptr [ESP + 0x40],EAX
0050bba4     A1 38 33 57 00           MOV EAX,[0x00573338]
0050bba9     33 D2                    XOR EDX,EDX
0050bbab     83 C4 08                 ADD ESP,0x8
0050bbae     8A 90 50 01 00 00        MOV DL,byte ptr [EAX + 0x150]
0050bbb4     8B 41 70                 MOV EAX,dword ptr [ECX + 0x70]
0050bbb7     6A 00                    PUSH 0x0
0050bbb9     68 E0 3D 57 00           PUSH 0x573de0
0050bbbe     8B 4C 90 0C              MOV ECX,dword ptr [EAX + EDX*0x4 + 0xc]
0050bbc2     6A 00                    PUSH 0x0
0050bbc4     51                       PUSH ECX
0050bbc5     E8 86 87 03 00           CALL 0x00544350
0050bbca     8B 54 24 48              MOV EDX,dword ptr [ESP + 0x48]
0050bbce     83 C4 10                 ADD ESP,0x10
0050bbd1     68 E0 3D 57 00           PUSH 0x573de0
0050bbd6     52                       PUSH EDX
0050bbd7     E8 C4 87 03 00           CALL 0x005443a0
0050bbdc     89 44 24 40              MOV dword ptr [ESP + 0x40],EAX
0050bbe0     A1 60 99 5E 00           MOV EAX,[0x005e9960]
0050bbe5     83 C4 08                 ADD ESP,0x8
0050bbe8     C6 05 9A 90 5E 00 01     MOV byte ptr [0x005e909a],0x1
0050bbef     C6 05 9B 90 5E 00 0F     MOV byte ptr [0x005e909b],0xf
0050bbf6     C1 E8 02                 SHR EAX,0x2
0050bbf9     25 FF FF FF 1F           AND EAX,0x1fffffff
0050bbfe     33 D2                    XOR EDX,EDX
0050bc00     B9 03 00 00 00           MOV ECX,0x3
0050bc05     8B 35 38 33 57 00        MOV ESI,dword ptr [0x00573338]
0050bc0b     F7 F1                    DIV ECX
0050bc0d     8A 86 51 01 00 00        MOV AL,byte ptr [ESI + 0x151]
0050bc13     8A C9                    MOV CL,CL
0050bc15     F6 E9                    IMUL CL
0050bc17     C6 05 9D 90 5E 00 10     MOV byte ptr [0x005e909d],0x10
0050bc1e     6A 00                    PUSH 0x0
0050bc20     68 E0 3D 57 00           PUSH 0x573de0
0050bc25     6A 00                    PUSH 0x0
0050bc27     02 D0                    ADD DL,AL
0050bc29     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
0050bc2e     88 15 9C 90 5E 00        MOV byte ptr [0x005e909c],DL
0050bc34     8A 96 50 01 00 00        MOV DL,byte ptr [ESI + 0x150]
0050bc3a     88 15 9E 90 5E 00        MOV byte ptr [0x005e909e],DL
0050bc40     8B 48 70                 MOV ECX,dword ptr [EAX + 0x70]
0050bc43     8B 91 60 01 00 00        MOV EDX,dword ptr [ECX + 0x160]
0050bc49     52                       PUSH EDX
0050bc4a     E8 01 87 03 00           CALL 0x00544350
0050bc4f     8B 44 24 48              MOV EAX,dword ptr [ESP + 0x48]
0050bc53     83 C4 10                 ADD ESP,0x10
0050bc56     68 E0 3D 57 00           PUSH 0x573de0
0050bc5b     50                       PUSH EAX
0050bc5c     E8 3F 87 03 00           CALL 0x005443a0
0050bc61     66 8B 0D E6 3D 57 00     MOV CX,word ptr [0x00573de6]
0050bc68     83 C4 08                 ADD ESP,0x8
0050bc6b     66 83 C1 52              ADD CX,0x52
0050bc6f     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bc73     C7 44 24 1C 00 00 00 01  MOV dword ptr [ESP + 0x1c],0x1000000
0050bc7b     66 89 4C 24 22           MOV word ptr [ESP + 0x22],CX
0050bc80     33 F6                    XOR ESI,ESI
0050bc82     BF 1C 00 00 00           MOV EDI,0x1c
0050bc87     BB 30 6C 56 00           MOV EBX,0x566c30
0050bc8c     8D 54 24 38              LEA EDX,[ESP + 0x38]
0050bc90     52                       PUSH EDX
0050bc91     55                       PUSH EBP
0050bc92     E8 89 01 01 00           CALL 0x0051be20
0050bc97     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
0050bc9c     83 C4 08                 ADD ESP,0x8
0050bc9f     8B 88 94 00 00 00        MOV ECX,dword ptr [EAX + 0x94]
0050bca5     8D 46 0B                 LEA EAX,[ESI + 0xb]
0050bca8     6A 00                    PUSH 0x0
0050bcaa     6A 00                    PUSH 0x0
0050bcac     8B 51 30                 MOV EDX,dword ptr [ECX + 0x30]
0050bcaf     8B CE                    MOV ECX,ESI
0050bcb1     80 E1 01                 AND CL,0x1
0050bcb4     66 89 44 24 30           MOV word ptr [ESP + 0x30],AX
0050bcb9     A1 E4 3D 57 00           MOV EAX,[0x00573de4]
0050bcbe     89 54 24 20              MOV dword ptr [ESP + 0x20],EDX
0050bcc2     F6 D9                    NEG CL
0050bcc4     1B C9                    SBB ECX,ECX
0050bcc6     8D 54 24 20              LEA EDX,[ESP + 0x20]
0050bcca     83 E1 38                 AND ECX,0x38
0050bccd     52                       PUSH EDX
0050bcce     83 C1 12                 ADD ECX,0x12
0050bcd1     03 C8                    ADD ECX,EAX
0050bcd3     8B 44 24 44              MOV EAX,dword ptr [ESP + 0x44]
0050bcd7     50                       PUSH EAX
0050bcd8     55                       PUSH EBP
0050bcd9     66 89 4C 24 34           MOV word ptr [ESP + 0x34],CX
0050bcde     E8 0D 61 01 00           CALL 0x00521df0
0050bce3     83 C4 14                 ADD ESP,0x14
0050bce6     8D 4C 24 18              LEA ECX,[ESP + 0x18]
0050bcea     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bcee     51                       PUSH ECX
0050bcef     50                       PUSH EAX
0050bcf0     55                       PUSH EBP
0050bcf1     E8 CA 3C 01 00           CALL 0x0051f9c0
0050bcf6     83 C4 0C                 ADD ESP,0xc
0050bcf9     8D 54 24 38              LEA EDX,[ESP + 0x38]
0050bcfd     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bd01     52                       PUSH EDX
0050bd02     55                       PUSH EBP
0050bd03     E8 A8 00 01 00           CALL 0x0051bdb0
0050bd08     8B 54 24 40              MOV EDX,dword ptr [ESP + 0x40]
0050bd0c     83 C4 08                 ADD ESP,0x8
0050bd0f     66 01 7C 24 20           ADD word ptr [ESP + 0x20],DI
0050bd14     33 C0                    XOR EAX,EAX
0050bd16     8A 86 67 90 5E 00        MOV AL,byte ptr [ESI + 0x5e9067]
0050bd1c     6A 00                    PUSH 0x0
0050bd1e     68 82 00 00 00           PUSH 0x82
0050bd23     8D 4C 24 20              LEA ECX,[ESP + 0x20]
0050bd27     50                       PUSH EAX
0050bd28     51                       PUSH ECX
0050bd29     52                       PUSH EDX
0050bd2a     55                       PUSH EBP
0050bd2b     89 5C 24 30              MOV dword ptr [ESP + 0x30],EBX
0050bd2f     E8 5C 01 01 00           CALL 0x0051be90
0050bd34     83 C4 18                 ADD ESP,0x18
0050bd37     46                       INC ESI
0050bd38     66 FF 44 24 28           INC word ptr [ESP + 0x28]
0050bd3d     83 FE 02                 CMP ESI,0x2
0050bd40     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bd44     0F 8C 42 FF FF FF        JL 0x0050bc8c
0050bd4a     66 A1 E6 3D 57 00        MOV AX,[0x00573de6]
0050bd50     66 05 60 00              ADD AX,0x60
0050bd54     33 FF                    XOR EDI,EDI
0050bd56     66 89 44 24 22           MOV word ptr [ESP + 0x22],AX
0050bd5b     8D 4C 24 38              LEA ECX,[ESP + 0x38]
0050bd5f     51                       PUSH ECX
0050bd60     55                       PUSH EBP
0050bd61     E8 BA 00 01 00           CALL 0x0051be20
0050bd66     8B 15 C4 97 5E 00        MOV EDX,dword ptr [0x005e97c4]
0050bd6c     8B F7                    MOV ESI,EDI
0050bd6e     83 E6 01                 AND ESI,0x1
0050bd71     83 C4 08                 ADD ESP,0x8
0050bd74     8B 82 94 00 00 00        MOV EAX,dword ptr [EDX + 0x94]
0050bd7a     8D 57 04                 LEA EDX,[EDI + 0x4]
0050bd7d     6A 00                    PUSH 0x0
0050bd7f     6A 00                    PUSH 0x0
0050bd81     8B 48 30                 MOV ECX,dword ptr [EAX + 0x30]
0050bd84     8B C6                    MOV EAX,ESI
0050bd86     F7 D8                    NEG EAX
0050bd88     1B C0                    SBB EAX,EAX
0050bd8a     66 89 54 24 30           MOV word ptr [ESP + 0x30],DX
0050bd8f     8B 15 E4 3D 57 00        MOV EDX,dword ptr [0x00573de4]
0050bd95     83 E0 38                 AND EAX,0x38
0050bd98     83 C0 12                 ADD EAX,0x12
0050bd9b     89 4C 24 20              MOV dword ptr [ESP + 0x20],ECX
0050bd9f     03 C2                    ADD EAX,EDX
0050bda1     8B 54 24 40              MOV EDX,dword ptr [ESP + 0x40]
0050bda5     8D 4C 24 20              LEA ECX,[ESP + 0x20]
0050bda9     66 89 44 24 28           MOV word ptr [ESP + 0x28],AX
0050bdae     51                       PUSH ECX
0050bdaf     52                       PUSH EDX
0050bdb0     55                       PUSH EBP
0050bdb1     E8 3A 60 01 00           CALL 0x00521df0
0050bdb6     83 C4 14                 ADD ESP,0x14
0050bdb9     8D 4C 24 18              LEA ECX,[ESP + 0x18]
0050bdbd     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bdc1     51                       PUSH ECX
0050bdc2     50                       PUSH EAX
0050bdc3     55                       PUSH EBP
0050bdc4     E8 F7 3B 01 00           CALL 0x0051f9c0
0050bdc9     83 C4 0C                 ADD ESP,0xc
0050bdcc     8D 54 24 38              LEA EDX,[ESP + 0x38]
0050bdd0     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050bdd4     52                       PUSH EDX
0050bdd5     55                       PUSH EBP
0050bdd6     E8 D5 FF 00 00           CALL 0x0051bdb0
0050bddb     8B 54 24 40              MOV EDX,dword ptr [ESP + 0x40]
0050bddf     83 C4 08                 ADD ESP,0x8
0050bde2     66 83 44 24 20 1C        ADD word ptr [ESP + 0x20],0x1c
0050bde8     33 C0                    XOR EAX,EAX
0050bdea     8A 87 60 90 5E 00        MOV AL,byte ptr [EDI + 0x5e9060]
0050bdf0     6A 00                    PUSH 0x0
0050bdf2     68 82 00 00 00           PUSH 0x82
0050bdf7     8D 4C 24 20              LEA ECX,[ESP + 0x20]
0050bdfb     50                       PUSH EAX
0050bdfc     51                       PUSH ECX
0050bdfd     52                       PUSH EDX
0050bdfe     55                       PUSH EBP
0050bdff     89 5C 24 30              MOV dword ptr [ESP + 0x30],EBX
0050be03     E8 88 00 01 00           CALL 0x0051be90
0050be08     83 C4 18                 ADD ESP,0x18
0050be0b     89 44 24 38              MOV dword ptr [ESP + 0x38],EAX
0050be0f     F7 DE                    NEG ESI
0050be11     1B F6                    SBB ESI,ESI
0050be13     83 E6 09                 AND ESI,0x9
0050be16     66 01 74 24 22           ADD word ptr [ESP + 0x22],SI
0050be1b     47                       INC EDI
0050be1c     66 FF 44 24 28           INC word ptr [ESP + 0x28]
0050be21     83 FF 07                 CMP EDI,0x7
0050be24     0F 8C 31 FF FF FF        JL 0x0050bd5b
0050be2a     68 E0 3D 57 00           PUSH 0x573de0
0050be2f     50                       PUSH EAX
0050be30     55                       PUSH EBP
0050be31     E8 0A 95 03 00           CALL 0x00545340
0050be36     83 C4 0C                 ADD ESP,0xc
0050be39     5F                       POP EDI
0050be3a     5E                       POP ESI
0050be3b     5D                       POP EBP
0050be3c     5B                       POP EBX
0050be3d     83 C4 1C                 ADD ESP,0x1c
0050be40     C3                       RET
