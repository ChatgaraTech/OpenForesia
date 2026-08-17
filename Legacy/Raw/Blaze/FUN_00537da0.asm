; Function: FUN_00537da0
; Entry:    00537da0

00537da0     83 EC 44                     SUB ESP,0x44
00537da3     53                           PUSH EBX
00537da4     55                           PUSH EBP
00537da5     56                           PUSH ESI
00537da6     8D 44 24 10                  LEA EAX,[ESP + 0x10]
00537daa     57                           PUSH EDI
00537dab     8D 4C 24 1C                  LEA ECX,[ESP + 0x1c]
00537daf     50                           PUSH EAX
00537db0     8D 54 24 24                  LEA EDX,[ESP + 0x24]
00537db4     33 DB                        XOR EBX,EBX
00537db6     51                           PUSH ECX
00537db7     52                           PUSH EDX
00537db8     89 5C 24 30                  MOV dword ptr [ESP + 0x30],EBX
00537dbc     E8 1F 24 FF FF               CALL 0x0052a1e0
00537dc1     83 C4 0C                     ADD ESP,0xc
00537dc4     6A 05                        PUSH 0x5
00537dc6     E8 05 1D FF FF               CALL 0x00529ad0
00537dcb     83 C4 04                     ADD ESP,0x4
00537dce     BE 28 E8 5D 00               MOV ESI,0x5de828
00537dd3     BF 20 00 00 00               MOV EDI,0x20
00537dd8     E8 63 F3 00 00               CALL 0x00547140
00537ddd     25 FF 03 00 00               AND EAX,0x3ff
00537de2     2D FF 01 00 00               SUB EAX,0x1ff
00537de7     C1 E0 0C                     SHL EAX,0xc
00537dea     89 46 FC                     MOV dword ptr [ESI + -0x4],EAX
00537ded     E8 4E F3 00 00               CALL 0x00547140
00537df2     25 FF 03 00 00               AND EAX,0x3ff
00537df7     2D FF 01 00 00               SUB EAX,0x1ff
00537dfc     C1 E0 0C                     SHL EAX,0xc
00537dff     89 06                        MOV dword ptr [ESI],EAX
00537e01     E8 3A F3 00 00               CALL 0x00547140
00537e06     25 FF 0F 00 00               AND EAX,0xfff
00537e0b     89 5E 10                     MOV dword ptr [ESI + 0x10],EBX
00537e0e     2D FF 07 00 00               SUB EAX,0x7ff
00537e13     89 5E 14                     MOV dword ptr [ESI + 0x14],EBX
00537e16     C1 E0 0C                     SHL EAX,0xc
00537e19     89 46 04                     MOV dword ptr [ESI + 0x4],EAX
00537e1c     B8 00 10 00 00               MOV EAX,0x1000
00537e21     66 89 46 08                  MOV word ptr [ESI + 0x8],AX
00537e25     66 89 46 0A                  MOV word ptr [ESI + 0xa],AX
00537e29     66 89 46 0C                  MOV word ptr [ESI + 0xc],AX
00537e2d     66 89 5E 0E                  MOV word ptr [ESI + 0xe],BX
00537e31     89 5E 18                     MOV dword ptr [ESI + 0x18],EBX
00537e34     E8 07 F3 00 00               CALL 0x00547140
00537e39     99                           CDQ
00537e3a     B9 07 00 00 00               MOV ECX,0x7
00537e3f     81 C6 9C 00 00 00            ADD ESI,0x9c
00537e45     F7 F9                        IDIV ECX
00537e47     4F                           DEC EDI
00537e48     88 56 84                     MOV byte ptr [ESI + -0x7c],DL
00537e4b     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
00537e51     8B 82 94 00 00 00            MOV EAX,dword ptr [EDX + 0x94]
00537e57     8B 48 0C                     MOV ECX,dword ptr [EAX + 0xc]
00537e5a     89 4E 80                     MOV dword ptr [ESI + -0x80],ECX
00537e5d     0F 85 75 FF FF FF            JNZ 0x00537dd8
00537e63     53                           PUSH EBX
00537e64     E8 A7 CC FC FF               CALL 0x00504b10
00537e69     33 C9                        XOR ECX,ECX
00537e6b     83 C4 04                     ADD ESP,0x4
00537e6e     8A 0D 1E 90 5E 00            MOV CL,byte ptr [0x005e901e]
00537e74     89 5C 24 10                  MOV dword ptr [ESP + 0x10],EBX
00537e78     8B C1                        MOV EAX,ECX
00537e7a     83 E1 03                     AND ECX,0x3
00537e7d     25 FC 00 00 00               AND EAX,0xfc
00537e82     D1 E0                        SHL EAX,0x1
00537e84     0B C1                        OR EAX,ECX
00537e86     D1 E0                        SHL EAX,0x1
00537e88     0C 09                        OR AL,0x9
00537e8a     A8 01                        TEST AL,0x1
00537e8c     89 44 24 30                  MOV dword ptr [ESP + 0x30],EAX
00537e90     74 08                        JZ 0x00537e9a
00537e92     C7 44 24 10 01 00 00 00      MOV dword ptr [ESP + 0x10],0x1
00537e9a     A8 02                        TEST AL,0x2
00537e9c     74 05                        JZ 0x00537ea3
00537e9e     83 4C 24 10 02               OR dword ptr [ESP + 0x10],0x2
00537ea3     8B C8                        MOV ECX,EAX
00537ea5     83 E1 04                     AND ECX,0x4
00537ea8     89 4C 24 34                  MOV dword ptr [ESP + 0x34],ECX
00537eac     74 05                        JZ 0x00537eb3
00537eae     83 4C 24 10 10               OR dword ptr [ESP + 0x10],0x10
00537eb3     A8 08                        TEST AL,0x8
00537eb5     74 05                        JZ 0x00537ebc
00537eb7     83 4C 24 10 08               OR dword ptr [ESP + 0x10],0x8
00537ebc     A8 10                        TEST AL,0x10
00537ebe     74 05                        JZ 0x00537ec5
00537ec0     83 4C 24 10 04               OR dword ptr [ESP + 0x10],0x4
00537ec5     A8 20                        TEST AL,0x20
00537ec7     74 05                        JZ 0x00537ece
00537ec9     83 4C 24 10 20               OR dword ptr [ESP + 0x10],0x20
00537ece     A8 40                        TEST AL,0x40
00537ed0     74 0B                        JZ 0x00537edd
00537ed2     8B 4C 24 10                  MOV ECX,dword ptr [ESP + 0x10]
00537ed6     80 C9 80                     OR CL,0x80
00537ed9     89 4C 24 10                  MOV dword ptr [ESP + 0x10],ECX
00537edd     A8 80                        TEST AL,0x80
00537edf     74 0B                        JZ 0x00537eec
00537ee1     8B 4C 24 10                  MOV ECX,dword ptr [ESP + 0x10]
00537ee5     80 CD 02                     OR CH,0x2
00537ee8     89 4C 24 10                  MOV dword ptr [ESP + 0x10],ECX
00537eec     F6 C4 01                     TEST AH,0x1
00537eef     74 05                        JZ 0x00537ef6
00537ef1     83 4C 24 10 40               OR dword ptr [ESP + 0x10],0x40
00537ef6     F6 C4 02                     TEST AH,0x2
00537ef9     74 0B                        JZ 0x00537f06
00537efb     8B 44 24 10                  MOV EAX,dword ptr [ESP + 0x10]
00537eff     80 CC 01                     OR AH,0x1
00537f02     89 44 24 10                  MOV dword ptr [ESP + 0x10],EAX
00537f06     33 D2                        XOR EDX,EDX
00537f08     33 C0                        XOR EAX,EAX
00537f0a     8A 15 28 E5 5D 00            MOV DL,byte ptr [0x005de528]
00537f10     8A 82 A0 D2 56 00            MOV AL,byte ptr [EDX + 0x56d2a0]
00537f16     8B F8                        MOV EDI,EAX
00537f18     33 C0                        XOR EAX,EAX
00537f1a     89 7C 24 18                  MOV dword ptr [ESP + 0x18],EDI
00537f1e     8A 87 B0 D2 56 00            MOV AL,byte ptr [EDI + 0x56d2b0]
00537f24     C1 E0 02                     SHL EAX,0x2
00537f27     0F BF 88 72 D2 56 00         MOVSX ECX,word ptr [EAX + 0x56d272]
00537f2e     0F BF A8 70 D2 56 00         MOVSX EBP,word ptr [EAX + 0x56d270]
00537f35     89 4C 24 28                  MOV dword ptr [ESP + 0x28],ECX
00537f39     EB 08                        JMP 0x00537f43
00537f3b     8B 6C 24 2C                  MOV EBP,dword ptr [ESP + 0x2c]
00537f3f     8B 7C 24 18                  MOV EDI,dword ptr [ESP + 0x18]
00537f43     A1 38 4A 56 00               MOV EAX,[0x00564a38]
00537f48     C7 05 5C E1 5D 00 01 00 00 00 MOV dword ptr [0x005de15c],0x1
00537f52     3B C3                        CMP EAX,EBX
00537f54     A1 60 E1 5D 00               MOV EAX,[0x005de160]
00537f59     75 1C                        JNZ 0x00537f77
00537f5b     83 F8 08                     CMP EAX,0x8
00537f5e     7C 17                        JL 0x00537f77
00537f60     83 C0 F5                     ADD EAX,-0xb
00537f63     89 1D 54 E1 5D 00            MOV dword ptr [0x005de154],EBX
00537f69     8D 14 40                     LEA EDX,[EAX + EAX*0x2]
00537f6c     C1 E2 02                     SHL EDX,0x2
00537f6f     89 15 58 E1 5D 00            MOV dword ptr [0x005de158],EDX
00537f75     EB 1A                        JMP 0x00537f91
00537f77     8D 04 40                     LEA EAX,[EAX + EAX*0x2]
00537f7a     C7 05 54 E1 5D 00 A0 00 00 00 MOV dword ptr [0x005de154],0xa0
00537f84     8D 0C 85 76 00 00 00         LEA ECX,[EAX*0x4 + 0x76]
00537f8b     89 0D 58 E1 5D 00            MOV dword ptr [0x005de158],ECX
00537f91     6A 05                        PUSH 0x5
00537f93     66 C7 44 24 50 00 14         MOV word ptr [ESP + 0x50],0x1400
00537f9a     66 C7 44 24 52 49 11         MOV word ptr [ESP + 0x52],0x1149
00537fa1     E8 5A 26 ED FF               CALL 0x0040a600
00537fa6     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
00537fac     83 C4 04                     ADD ESP,0x4
00537faf     52                           PUSH EDX
00537fb0     E8 4B B0 FF FF               CALL 0x00533000
00537fb5     33 C0                        XOR EAX,EAX
00537fb7     33 D2                        XOR EDX,EDX
00537fb9     A0 38 E5 5D 00               MOV AL,[0x005de538]
00537fbe     83 C4 04                     ADD ESP,0x4
00537fc1     89 7C 24 2C                  MOV dword ptr [ESP + 0x2c],EDI
00537fc5     8D 0C 80                     LEA ECX,[EAX + EAX*0x4]
00537fc8     C1 E1 03                     SHL ECX,0x3
00537fcb     2B C8                        SUB ECX,EAX
00537fcd     33 C0                        XOR EAX,EAX
00537fcf     8A 14 8D C8 93 5E 00         MOV DL,byte ptr [ECX*0x4 + 0x5e93c8]
00537fd6     6B D2 4C                     IMUL EDX,EDX,0x4c
00537fd9     8A 82 88 91 5E 00            MOV AL,byte ptr [EDX + 0x5e9188]
00537fdf     8B F0                        MOV ESI,EAX
00537fe1     C1 E6 04                     SHL ESI,0x4
00537fe4     81 C6 20 4A 60 00            ADD ESI,0x604a20
00537fea     66 8B 46 06                  MOV AX,word ptr [ESI + 0x6]
00537fee     A8 C0                        TEST AL,0xc0
00537ff0     74 24                        JZ 0x00538016
00537ff2     4F                           DEC EDI
00537ff3     89 7C 24 18                  MOV dword ptr [ESP + 0x18],EDI
00537ff7     79 0C                        JNS 0x00538005
00537ff9     C7 44 24 18 0B 00 00 00      MOV dword ptr [ESP + 0x18],0xb
00538001     8B 7C 24 18                  MOV EDI,dword ptr [ESP + 0x18]
00538005     BA 01 00 00 00               MOV EDX,0x1
0053800a     8B CF                        MOV ECX,EDI
0053800c     D3 E2                        SHL EDX,CL
0053800e     8B 4C 24 10                  MOV ECX,dword ptr [ESP + 0x10]
00538012     85 D1                        TEST ECX,EDX
00538014     74 DC                        JZ 0x00537ff2
00538016     A8 30                        TEST AL,0x30
00538018     74 21                        JZ 0x0053803b
0053801a     47                           INC EDI
0053801b     83 FF 0B                     CMP EDI,0xb
0053801e     89 7C 24 18                  MOV dword ptr [ESP + 0x18],EDI
00538022     7C 06                        JL 0x0053802a
00538024     89 5C 24 18                  MOV dword ptr [ESP + 0x18],EBX
00538028     8B FB                        MOV EDI,EBX
0053802a     8B 44 24 10                  MOV EAX,dword ptr [ESP + 0x10]
0053802e     BA 01 00 00 00               MOV EDX,0x1
00538033     8B CF                        MOV ECX,EDI
00538035     D3 E2                        SHL EDX,CL
00538037     85 D0                        TEST EAX,EDX
00538039     74 DF                        JZ 0x0053801a
0053803b     39 7C 24 2C                  CMP dword ptr [ESP + 0x2c],EDI
0053803f     74 05                        JZ 0x00538046
00538041     E8 1A 5B 00 00               CALL 0x0053db60
00538046     66 F7 46 02 08 80            TEST word ptr [ESI + 0x2],0x8008
0053804c     0F 85 2F 06 00 00            JNZ 0x00538681
00538052     33 C0                        XOR EAX,EAX
00538054     8B 74 24 28                  MOV ESI,dword ptr [ESP + 0x28]
00538058     8A 87 B0 D2 56 00            MOV AL,byte ptr [EDI + 0x56d2b0]
0053805e     8B D5                        MOV EDX,EBP
00538060     C1 E0 02                     SHL EAX,0x2
00538063     68 90 01 00 00               PUSH 0x190
00538068     66 C7 05 28 49 60 00 00 02   MOV word ptr [0x00604928],0x200
00538071     0F BF 88 70 D2 56 00         MOVSX ECX,word ptr [EAX + 0x56d270]
00538078     0F BF 80 72 D2 56 00         MOVSX EAX,word ptr [EAX + 0x56d272]
0053807f     2B D1                        SUB EDX,ECX
00538081     8B CE                        MOV ECX,ESI
00538083     2B C8                        SUB ECX,EAX
00538085     66 C7 05 2A 49 60 00 00 08   MOV word ptr [0x0060492a],0x800
0053808e     C1 FA 02                     SAR EDX,0x2
00538091     C1 F9 02                     SAR ECX,0x2
00538094     2B EA                        SUB EBP,EDX
00538096     2B F1                        SUB ESI,ECX
00538098     66 89 1D 2C 49 60 00         MOV word ptr [0x0060492c],BX
0053809f     89 1D D0 08 60 00            MOV dword ptr [0x006008d0],EBX
005380a5     89 1D D4 08 60 00            MOV dword ptr [0x006008d4],EBX
005380ab     89 1D D8 08 60 00            MOV dword ptr [0x006008d8],EBX
005380b1     89 1D F4 08 60 00            MOV dword ptr [0x006008f4],EBX
005380b7     89 1D F8 08 60 00            MOV dword ptr [0x006008f8],EBX
005380bd     C7 05 FC 08 60 00 DC 05 00 00 MOV dword ptr [0x006008fc],0x5dc
005380c7     89 6C 24 30                  MOV dword ptr [ESP + 0x30],EBP
005380cb     89 74 24 2C                  MOV dword ptr [ESP + 0x2c],ESI
005380cf     E8 D8 D9 00 00               CALL 0x00545aac
005380d4     56                           PUSH ESI
005380d5     55                           PUSH EBP
005380d6     E8 CB D9 00 00               CALL 0x00545aa6
005380db     68 E0 08 60 00               PUSH 0x6008e0
005380e0     68 28 49 60 00               PUSH 0x604928
005380e5     E8 16 42 FE FF               CALL 0x0051c300
005380ea     83 C4 08                     ADD ESP,0x8
005380ed     81 FD 80 00 00 00            CMP EBP,0x80
005380f3     7F 21                        JG 0x00538116
005380f5     0F BF 15 80 C9 56 00         MOVSX EDX,word ptr [0x0056c980]
005380fc     66 A1 7C C9 56 00            MOV AX,[0x0056c97c]
00538102     B9 E8 00 00 00               MOV ECX,0xe8
00538107     2B CA                        SUB ECX,EDX
00538109     0F BF D0                     MOVSX EDX,AX
0053810c     2B CA                        SUB ECX,EDX
0053810e     C1 F9 02                     SAR ECX,0x2
00538111     66 03 C1                     ADD AX,CX
00538114     EB 16                        JMP 0x0053812c
00538116     66 A1 7C C9 56 00            MOV AX,[0x0056c97c]
0053811c     BA 08 00 00 00               MOV EDX,0x8
00538121     0F BF C8                     MOVSX ECX,AX
00538124     2B D1                        SUB EDX,ECX
00538126     C1 FA 02                     SAR EDX,0x2
00538129     66 03 C2                     ADD AX,DX
0053812c     66 A3 7C C9 56 00            MOV [0x0056c97c],AX
00538132     33 C0                        XOR EAX,EAX
00538134     A0 38 E5 5D 00               MOV AL,[0x005de538]
00538139     BA 01 00 00 00               MOV EDX,0x1
0053813e     33 ED                        XOR EBP,EBP
00538140     8D 0C 80                     LEA ECX,[EAX + EAX*0x4]
00538143     C1 E1 03                     SHL ECX,0x3
00538146     2B C8                        SUB ECX,EAX
00538148     8D 04 8D C0 93 5E 00         LEA EAX,[ECX*0x4 + 0x5e93c0]
0053814f     B9 00 10 00 00               MOV ECX,0x1000
00538154     66 89 48 28                  MOV word ptr [EAX + 0x28],CX
00538158     66 89 48 2A                  MOV word ptr [EAX + 0x2a],CX
0053815c     66 89 48 2C                  MOV word ptr [EAX + 0x2c],CX
00538160     33 C9                        XOR ECX,ECX
00538162     8A 48 04                     MOV CL,byte ptr [EAX + 0x4]
00538165     8D 70 1C                     LEA ESI,[EAX + 0x1c]
00538168     C1 E1 02                     SHL ECX,0x2
0053816b     66 89 50 36                  MOV word ptr [EAX + 0x36],DX
0053816f     66 89 58 30                  MOV word ptr [EAX + 0x30],BX
00538173     66 89 58 32                  MOV word ptr [EAX + 0x32],BX
00538177     66 89 58 34                  MOV word ptr [EAX + 0x34],BX
0053817b     89 1E                        MOV dword ptr [ESI],EBX
0053817d     89 58 20                     MOV dword ptr [EAX + 0x20],EBX
00538180     89 58 24                     MOV dword ptr [EAX + 0x24],EBX
00538183     C7 40 38 00 00 00 01         MOV dword ptr [EAX + 0x38],0x1000000
0053818a     66 89 50 18                  MOV word ptr [EAX + 0x18],DX
0053818e     8B 91 80 96 5E 00            MOV EDX,dword ptr [ECX + 0x5e9680]
00538194     8B 52 04                     MOV EDX,dword ptr [EDX + 0x4]
00538197     66 FF 40 1A                  INC word ptr [EAX + 0x1a]
0053819b     66 8B 78 1A                  MOV DI,word ptr [EAX + 0x1a]
0053819f     66 8B 6A 16                  MOV BP,word ptr [EDX + 0x16]
005381a3     0F BF D7                     MOVSX EDX,DI
005381a6     3B D5                        CMP EDX,EBP
005381a8     7C 04                        JL 0x005381ae
005381aa     66 89 58 1A                  MOV word ptr [EAX + 0x1a],BX
005381ae     66 8B 50 0A                  MOV DX,word ptr [EAX + 0xa]
005381b2     0F BF 40 1A                  MOVSX EAX,word ptr [EAX + 0x1a]
005381b6     52                           PUSH EDX
005381b7     8B 91 00 98 5E 00            MOV EDX,dword ptr [ECX + 0x5e9800]
005381bd     50                           PUSH EAX
005381be     8B 81 80 96 5E 00            MOV EAX,dword ptr [ECX + 0x5e9680]
005381c4     56                           PUSH ESI
005381c5     52                           PUSH EDX
005381c6     8B 48 04                     MOV ECX,dword ptr [EAX + 0x4]
005381c9     8B 54 24 24                  MOV EDX,dword ptr [ESP + 0x24]
005381cd     8B 44 24 30                  MOV EAX,dword ptr [ESP + 0x30]
005381d1     51                           PUSH ECX
005381d2     52                           PUSH EDX
005381d3     50                           PUSH EAX
005381d4     E8 17 50 FE FF               CALL 0x0051d1f0
005381d9     83 C4 1C                     ADD ESP,0x1c
005381dc     89 44 24 14                  MOV dword ptr [ESP + 0x14],EAX
005381e0     66 C7 05 28 49 60 00 BC 02   MOV word ptr [0x00604928],0x2bc
005381e9     66 C7 05 2A 49 60 00 F4 01   MOV word ptr [0x0060492a],0x1f4
005381f2     68 90 01 00 00               PUSH 0x190
005381f7     66 89 1D 2C 49 60 00         MOV word ptr [0x0060492c],BX
005381fe     89 1D D0 08 60 00            MOV dword ptr [0x006008d0],EBX
00538204     89 1D D4 08 60 00            MOV dword ptr [0x006008d4],EBX
0053820a     89 1D D8 08 60 00            MOV dword ptr [0x006008d8],EBX
00538210     89 1D F4 08 60 00            MOV dword ptr [0x006008f4],EBX
00538216     89 1D F8 08 60 00            MOV dword ptr [0x006008f8],EBX
0053821c     C7 05 FC 08 60 00 DC 05 00 00 MOV dword ptr [0x006008fc],0x5dc
00538226     E8 81 D8 00 00               CALL 0x00545aac
0053822b     6A 78                        PUSH 0x78
0053822d     68 A0 00 00 00               PUSH 0xa0
00538232     E8 6F D8 00 00               CALL 0x00545aa6
00538237     68 E0 08 60 00               PUSH 0x6008e0
0053823c     68 28 49 60 00               PUSH 0x604928
00538241     E8 BA 40 FE FF               CALL 0x0051c300
00538246     83 C4 08                     ADD ESP,0x8
00538249     BE 2C E8 5D 00               MOV ESI,0x5de82c
0053824e     BD 20 00 00 00               MOV EBP,0x20
00538253     8D 4C 24 38                  LEA ECX,[ESP + 0x38]
00538257     8D 7E F8                     LEA EDI,[ESI + -0x8]
0053825a     51                           PUSH ECX
0053825b     57                           PUSH EDI
0053825c     E8 EF 73 FE FF               CALL 0x0051f650
00538261     83 C4 08                     ADD ESP,0x8
00538264     85 C0                        TEST EAX,EAX
00538266     74 12                        JZ 0x0053827a
00538268     8B 46 14                     MOV EAX,dword ptr [ESI + 0x14]
0053826b     3D 80 80 80 00               CMP EAX,0x808080
00538270     74 08                        JZ 0x0053827a
00538272     05 08 08 08 00               ADD EAX,0x80808
00538277     89 46 14                     MOV dword ptr [ESI + 0x14],EAX
0053827a     8B 54 24 14                  MOV EDX,dword ptr [ESP + 0x14]
0053827e     8B 44 24 20                  MOV EAX,dword ptr [ESP + 0x20]
00538282     53                           PUSH EBX
00538283     53                           PUSH EBX
00538284     57                           PUSH EDI
00538285     52                           PUSH EDX
00538286     50                           PUSH EAX
00538287     E8 54 88 FE FF               CALL 0x00520ae0
0053828c     8B 0E                        MOV ECX,dword ptr [ESI]
0053828e     89 44 24 28                  MOV dword ptr [ESP + 0x28],EAX
00538292     81 C1 00 40 00 00            ADD ECX,0x4000
00538298     83 C4 14                     ADD ESP,0x14
0053829b     8B C1                        MOV EAX,ECX
0053829d     89 0E                        MOV dword ptr [ESI],ECX
0053829f     3D 00 00 80 00               CMP EAX,0x800000
005382a4     7C 45                        JL 0x005382eb
005382a6     E8 95 EE 00 00               CALL 0x00547140
005382ab     25 FF 03 00 00               AND EAX,0x3ff
005382b0     2D FF 01 00 00               SUB EAX,0x1ff
005382b5     C1 E0 0C                     SHL EAX,0xc
005382b8     89 07                        MOV dword ptr [EDI],EAX
005382ba     E8 81 EE 00 00               CALL 0x00547140
005382bf     25 FF 03 00 00               AND EAX,0x3ff
005382c4     2D FF 01 00 00               SUB EAX,0x1ff
005382c9     C1 E0 0C                     SHL EAX,0xc
005382cc     89 47 04                     MOV dword ptr [EDI + 0x4],EAX
005382cf     8B 06                        MOV EAX,dword ptr [ESI]
005382d1     05 00 00 00 FF               ADD EAX,0xff000000
005382d6     89 06                        MOV dword ptr [ESI],EAX
005382d8     E8 63 EE 00 00               CALL 0x00547140
005382dd     99                           CDQ
005382de     B9 07 00 00 00               MOV ECX,0x7
005382e3     89 5E 14                     MOV dword ptr [ESI + 0x14],EBX
005382e6     F7 F9                        IDIV ECX
005382e8     88 56 1C                     MOV byte ptr [ESI + 0x1c],DL
005382eb     81 C6 9C 00 00 00            ADD ESI,0x9c
005382f1     4D                           DEC EBP
005382f2     0F 85 5B FF FF FF            JNZ 0x00538253
005382f8     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
005382fe     C7 44 24 44 00 00 00 01      MOV dword ptr [ESP + 0x44],0x1000000
00538306     33 ED                        XOR EBP,EBP
00538308     BF 40 D2 56 00               MOV EDI,0x56d240
0053830d     8B 82 94 00 00 00            MOV EAX,dword ptr [EDX + 0x94]
00538313     8B 48 04                     MOV ECX,dword ptr [EAX + 0x4]
00538316     89 4C 24 40                  MOV dword ptr [ESP + 0x40],ECX
0053831a     8B 44 24 30                  MOV EAX,dword ptr [ESP + 0x30]
0053831e     BA 01 00 00 00               MOV EDX,0x1
00538323     8B CD                        MOV ECX,EBP
00538325     D3 E2                        SHL EDX,CL
00538327     85 D0                        TEST EAX,EDX
00538329     0F 84 19 01 00 00            JZ 0x00538448
0053832f     8B 44 24 40                  MOV EAX,dword ptr [ESP + 0x40]
00538333     8B 0F                        MOV ECX,dword ptr [EDI]
00538335     0F BF D5                     MOVSX EDX,BP
00538338     66 89 6C 24 50               MOV word ptr [ESP + 0x50],BP
0053833d     89 4C 24 48                  MOV dword ptr [ESP + 0x48],ECX
00538341     8A 4C D0 02                  MOV CL,byte ptr [EAX + EDX*0x8 + 0x2]
00538345     8D 34 D0                     LEA ESI,[EAX + EDX*0x8]
00538348     66 8B 44 24 48               MOV AX,word ptr [ESP + 0x48]
0053834d     66 33 D2                     XOR DX,DX
00538350     D0 E9                        SHR CL,0x1
00538352     8A D1                        MOV DL,CL
00538354     66 33 C9                     XOR CX,CX
00538357     66 03 C2                     ADD AX,DX
0053835a     0F BF C0                     MOVSX EAX,AX
0053835d     2D 80 00 00 00               SUB EAX,0x80
00538362     8D 04 80                     LEA EAX,[EAX + EAX*0x4]
00538365     C1 E0 06                     SHL EAX,0x6
00538368     99                           CDQ
00538369     81 E2 FF 00 00 00            AND EDX,0xff
0053836f     03 C2                        ADD EAX,EDX
00538371     8B 54 24 4A                  MOV EDX,dword ptr [ESP + 0x4a]
00538375     C1 F8 08                     SAR EAX,0x8
00538378     05 80 00 00 00               ADD EAX,0x80
0053837d     66 89 44 24 48               MOV word ptr [ESP + 0x48],AX
00538382     8A 46 03                     MOV AL,byte ptr [ESI + 0x3]
00538385     D0 E8                        SHR AL,0x1
00538387     8A C8                        MOV CL,AL
00538389     66 C7 44 24 4C 00 14         MOV word ptr [ESP + 0x4c],0x1400
00538390     66 C7 44 24 4E 49 11         MOV word ptr [ESP + 0x4e],0x1149
00538397     8D 44 11 FF                  LEA EAX,[ECX + EDX*0x1 + -0x1]
0053839b     0F BF C0                     MOVSX EAX,AX
0053839e     83 E8 70                     SUB EAX,0x70
005383a1     8D 0C 40                     LEA ECX,[EAX + EAX*0x2]
005383a4     8D 0C 89                     LEA ECX,[ECX + ECX*0x4]
005383a7     8D 0C C8                     LEA ECX,[EAX + ECX*0x8]
005383aa     B8 93 24 49 92               MOV EAX,0x92492493
005383af     D1 E1                        SHL ECX,0x1
005383b1     F7 E9                        IMUL ECX
005383b3     03 D1                        ADD EDX,ECX
005383b5     C1 FA 07                     SAR EDX,0x7
005383b8     8B C2                        MOV EAX,EDX
005383ba     C1 E8 1F                     SHR EAX,0x1f
005383bd     81 FF 50 D2 56 00            CMP EDI,0x56d250
005383c3     8D 54 02 70                  LEA EDX,[EDX + EAX*0x1 + 0x70]
005383c7     66 89 54 24 4A               MOV word ptr [ESP + 0x4a],DX
005383cc     74 10                        JZ 0x005383de
005383ce     81 FF 58 D2 56 00            CMP EDI,0x56d258
005383d4     74 08                        JZ 0x005383de
005383d6     81 FF 5C D2 56 00            CMP EDI,0x56d25c
005383dc     75 0E                        JNZ 0x005383ec
005383de     66 42                        INC DX
005383e0     66 C7 44 24 4E 6D 11         MOV word ptr [ESP + 0x4e],0x116d
005383e7     66 89 54 24 4A               MOV word ptr [ESP + 0x4a],DX
005383ec     8A 46 02                     MOV AL,byte ptr [ESI + 0x2]
005383ef     3A C3                        CMP AL,BL
005383f1     74 0D                        JZ 0x00538400
005383f3     FE C8                        DEC AL
005383f5     88 46 02                     MOV byte ptr [ESI + 0x2],AL
005383f8     8A 46 03                     MOV AL,byte ptr [ESI + 0x3]
005383fb     FE C8                        DEC AL
005383fd     88 46 03                     MOV byte ptr [ESI + 0x3],AL
00538400     8B 54 24 14                  MOV EDX,dword ptr [ESP + 0x14]
00538404     8B 44 24 1C                  MOV EAX,dword ptr [ESP + 0x1c]
00538408     53                           PUSH EBX
00538409     8D 4C 24 44                  LEA ECX,[ESP + 0x44]
0053840d     53                           PUSH EBX
0053840e     51                           PUSH ECX
0053840f     52                           PUSH EDX
00538410     50                           PUSH EAX
00538411     E8 0A 9B FE FF               CALL 0x00521f20
00538416     8B 54 24 30                  MOV EDX,dword ptr [ESP + 0x30]
0053841a     83 C4 14                     ADD ESP,0x14
0053841d     8D 4C 24 40                  LEA ECX,[ESP + 0x40]
00538421     89 44 24 14                  MOV dword ptr [ESP + 0x14],EAX
00538425     51                           PUSH ECX
00538426     50                           PUSH EAX
00538427     52                           PUSH EDX
00538428     E8 93 75 FE FF               CALL 0x0051f9c0
0053842d     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
00538431     8A 46 02                     MOV AL,byte ptr [ESI + 0x2]
00538434     83 C4 0C                     ADD ESP,0xc
00538437     3A C3                        CMP AL,BL
00538439     74 0D                        JZ 0x00538448
0053843b     FE C0                        INC AL
0053843d     88 46 02                     MOV byte ptr [ESI + 0x2],AL
00538440     8A 46 03                     MOV AL,byte ptr [ESI + 0x3]
00538443     FE C0                        INC AL
00538445     88 46 03                     MOV byte ptr [ESI + 0x3],AL
00538448     83 C7 04                     ADD EDI,0x4
0053844b     45                           INC EBP
0053844c     81 FF 6C D2 56 00            CMP EDI,0x56d26c
00538452     0F 8E C2 FE FF FF            JLE 0x0053831a
00538458     39 5C 24 34                  CMP dword ptr [ESP + 0x34],EBX
0053845c     0F 84 F1 00 00 00            JZ 0x00538553
00538462     8B 44 24 24                  MOV EAX,dword ptr [ESP + 0x24]
00538466     8B 0D C4 97 5E 00            MOV ECX,dword ptr [0x005e97c4]
0053846c     C1 F8 02                     SAR EAX,0x2
0053846f     66 89 44 24 50               MOV word ptr [ESP + 0x50],AX
00538474     66 C7 44 24 48 A0 00         MOV word ptr [ESP + 0x48],0xa0
0053847b     66 C7 44 24 4A FF FF         MOV word ptr [ESP + 0x4a],0xffff
00538482     8B 91 94 00 00 00            MOV EDX,dword ptr [ECX + 0x94]
00538488     0F BF C0                     MOVSX EAX,AX
0053848b     8B 72 08                     MOV ESI,dword ptr [EDX + 0x8]
0053848e     53                           PUSH EBX
0053848f     89 74 24 44                  MOV dword ptr [ESP + 0x44],ESI
00538493     53                           PUSH EBX
00538494     8A 4C C6 02                  MOV CL,byte ptr [ESI + EAX*0x8 + 0x2]
00538498     8D 34 C6                     LEA ESI,[ESI + EAX*0x8]
0053849b     D0 E9                        SHR CL,0x1
0053849d     66 33 C0                     XOR AX,AX
005384a0     8A C1                        MOV AL,CL
005384a2     05 A0 00 00 00               ADD EAX,0xa0
005384a7     0F BF C0                     MOVSX EAX,AX
005384aa     2D 80 00 00 00               SUB EAX,0x80
005384af     8D 04 80                     LEA EAX,[EAX + EAX*0x4]
005384b2     C1 E0 06                     SHL EAX,0x6
005384b5     99                           CDQ
005384b6     81 E2 FF 00 00 00            AND EDX,0xff
005384bc     03 C2                        ADD EAX,EDX
005384be     C1 F8 08                     SAR EAX,0x8
005384c1     05 80 00 00 00               ADD EAX,0x80
005384c6     66 89 44 24 50               MOV word ptr [ESP + 0x50],AX
005384cb     8A 56 03                     MOV DL,byte ptr [ESI + 0x3]
005384ce     D0 EA                        SHR DL,0x1
005384d0     66 33 C0                     XOR AX,AX
005384d3     8A C2                        MOV AL,DL
005384d5     48                           DEC EAX
005384d6     0F BF C0                     MOVSX EAX,AX
005384d9     83 E8 70                     SUB EAX,0x70
005384dc     8D 0C 40                     LEA ECX,[EAX + EAX*0x2]
005384df     8D 0C 89                     LEA ECX,[ECX + ECX*0x4]
005384e2     8D 0C C8                     LEA ECX,[EAX + ECX*0x8]
005384e5     B8 93 24 49 92               MOV EAX,0x92492493
005384ea     D1 E1                        SHL ECX,0x1
005384ec     F7 E9                        IMUL ECX
005384ee     03 D1                        ADD EDX,ECX
005384f0     C1 FA 07                     SAR EDX,0x7
005384f3     8B C2                        MOV EAX,EDX
005384f5     C1 E8 1F                     SHR EAX,0x1f
005384f8     8D 4C 02 70                  LEA ECX,[EDX + EAX*0x1 + 0x70]
005384fc     8D 54 24 48                  LEA EDX,[ESP + 0x48]
00538500     66 89 4C 24 52               MOV word ptr [ESP + 0x52],CX
00538505     8A 4E 02                     MOV CL,byte ptr [ESI + 0x2]
00538508     8A 46 03                     MOV AL,byte ptr [ESI + 0x3]
0053850b     FE C9                        DEC CL
0053850d     FE C8                        DEC AL
0053850f     88 4E 02                     MOV byte ptr [ESI + 0x2],CL
00538512     88 46 03                     MOV byte ptr [ESI + 0x3],AL
00538515     8B 44 24 1C                  MOV EAX,dword ptr [ESP + 0x1c]
00538519     8B 4C 24 24                  MOV ECX,dword ptr [ESP + 0x24]
0053851d     52                           PUSH EDX
0053851e     50                           PUSH EAX
0053851f     51                           PUSH ECX
00538520     E8 FB 99 FE FF               CALL 0x00521f20
00538525     83 C4 14                     ADD ESP,0x14
00538528     8D 54 24 40                  LEA EDX,[ESP + 0x40]
0053852c     89 44 24 14                  MOV dword ptr [ESP + 0x14],EAX
00538530     52                           PUSH EDX
00538531     50                           PUSH EAX
00538532     8B 44 24 24                  MOV EAX,dword ptr [ESP + 0x24]
00538536     50                           PUSH EAX
00538537     E8 84 74 FE FF               CALL 0x0051f9c0
0053853c     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
00538540     8A 4E 02                     MOV CL,byte ptr [ESI + 0x2]
00538543     8A 46 03                     MOV AL,byte ptr [ESI + 0x3]
00538546     83 C4 0C                     ADD ESP,0xc
00538549     FE C1                        INC CL
0053854b     FE C0                        INC AL
0053854d     88 4E 02                     MOV byte ptr [ESI + 0x2],CL
00538550     88 46 03                     MOV byte ptr [ESI + 0x3],AL
00538553     8B 0D C4 97 5E 00            MOV ECX,dword ptr [0x005e97c4]
00538559     66 C7 44 24 48 80 00         MOV word ptr [ESP + 0x48],0x80
00538560     66 C7 44 24 4A 70 00         MOV word ptr [ESP + 0x4a],0x70
00538567     66 89 5C 24 50               MOV word ptr [ESP + 0x50],BX
0053856c     8B 91 94 00 00 00            MOV EDX,dword ptr [ECX + 0x94]
00538572     53                           PUSH EBX
00538573     8D 4C 24 44                  LEA ECX,[ESP + 0x44]
00538577     53                           PUSH EBX
00538578     8B 02                        MOV EAX,dword ptr [EDX]
0053857a     8B 54 24 1C                  MOV EDX,dword ptr [ESP + 0x1c]
0053857e     89 44 24 48                  MOV dword ptr [ESP + 0x48],EAX
00538582     8B 44 24 24                  MOV EAX,dword ptr [ESP + 0x24]
00538586     51                           PUSH ECX
00538587     52                           PUSH EDX
00538588     50                           PUSH EAX
00538589     E8 92 99 FE FF               CALL 0x00521f20
0053858e     8B 54 24 30                  MOV EDX,dword ptr [ESP + 0x30]
00538592     83 C4 14                     ADD ESP,0x14
00538595     8D 4C 24 40                  LEA ECX,[ESP + 0x40]
00538599     89 44 24 14                  MOV dword ptr [ESP + 0x14],EAX
0053859d     51                           PUSH ECX
0053859e     50                           PUSH EAX
0053859f     52                           PUSH EDX
005385a0     E8 1B 74 FE FF               CALL 0x0051f9c0
005385a5     8B 4C 24 24                  MOV ECX,dword ptr [ESP + 0x24]
005385a9     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
005385af     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
005385b3     33 C0                        XOR EAX,EAX
005385b5     8A 81 B0 D2 56 00            MOV AL,byte ptr [ECX + 0x56d2b0]
005385bb     8B 4A 70                     MOV ECX,dword ptr [EDX + 0x70]
005385be     83 C4 0C                     ADD ESP,0xc
005385c1     8B 14 81                     MOV EDX,dword ptr [ECX + EAX*0x4]
005385c4     53                           PUSH EBX
005385c5     68 78 C9 56 00               PUSH 0x56c978
005385ca     53                           PUSH EBX
005385cb     52                           PUSH EDX
005385cc     E8 7F BD 00 00               CALL 0x00544350
005385d1     8B 44 24 24                  MOV EAX,dword ptr [ESP + 0x24]
005385d5     83 C4 10                     ADD ESP,0x10
005385d8     68 78 C9 56 00               PUSH 0x56c978
005385dd     50                           PUSH EAX
005385de     E8 BD BD 00 00               CALL 0x005443a0
005385e3     8B 4C 24 28                  MOV ECX,dword ptr [ESP + 0x28]
005385e7     83 C4 08                     ADD ESP,0x8
005385ea     89 44 24 14                  MOV dword ptr [ESP + 0x14],EAX
005385ee     68 78 C9 56 00               PUSH 0x56c978
005385f3     50                           PUSH EAX
005385f4     51                           PUSH ECX
005385f5     E8 46 CD 00 00               CALL 0x00545340
005385fa     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
005385fe     B8 00 01 00 00               MOV EAX,0x100
00538603     83 C4 0C                     ADD ESP,0xc
00538606     66 A3 50 99 5E 00            MOV [0x005e9950],AX
0053860c     66 A3 E0 98 5E 00            MOV [0x005e98e0],AX
00538612     B8 E0 00 00 00               MOV EAX,0xe0
00538617     66 A3 52 99 5E 00            MOV [0x005e9952],AX
0053861d     66 A3 E2 98 5E 00            MOV [0x005e98e2],AX
00538623     6A 01                        PUSH 0x1
00538625     8D 54 24 18                  LEA EDX,[ESP + 0x18]
00538629     6A 03                        PUSH 0x3
0053862b     8D 44 24 24                  LEA EAX,[ESP + 0x24]
0053862f     52                           PUSH EDX
00538630     8D 4C 24 2C                  LEA ECX,[ESP + 0x2c]
00538634     50                           PUSH EAX
00538635     51                           PUSH ECX
00538636     89 1D 5C E1 5D 00            MOV dword ptr [0x005de15c],EBX
0053863c     E8 3F 1C FF FF               CALL 0x0052a280
00538641     B8 40 01 00 00               MOV EAX,0x140
00538646     83 C4 14                     ADD ESP,0x14
00538649     66 A3 50 99 5E 00            MOV [0x005e9950],AX
0053864f     66 A3 E0 98 5E 00            MOV [0x005e98e0],AX
00538655     B8 F0 00 00 00               MOV EAX,0xf0
0053865a     66 A3 52 99 5E 00            MOV [0x005e9952],AX
00538660     66 A3 E2 98 5E 00            MOV [0x005e98e2],AX
00538666     8B 44 24 24                  MOV EAX,dword ptr [ESP + 0x24]
0053866a     40                           INC EAX
0053866b     83 F8 18                     CMP EAX,0x18
0053866e     89 44 24 24                  MOV dword ptr [ESP + 0x24],EAX
00538672     0F 8C C3 F8 FF FF            JL 0x00537f3b
00538678     89 5C 24 24                  MOV dword ptr [ESP + 0x24],EBX
0053867c     E9 BA F8 FF FF               JMP 0x00537f3b
00538681     E8 FA 54 00 00               CALL 0x0053db80
00538686     8A 87 B0 D2 56 00            MOV AL,byte ptr [EDI + 0x56d2b0]
0053868c     33 D2                        XOR EDX,EDX
0053868e     A2 28 E5 5D 00               MOV [0x005de528],AL
00538693     8A D0                        MOV DL,AL
00538695     A1 C4 97 5E 00               MOV EAX,[0x005e97c4]
0053869a     C6 05 84 6C 56 00 FF         MOV byte ptr [0x00566c84],0xff
005386a1     6A 01                        PUSH 0x1
005386a3     6A 03                        PUSH 0x3
005386a5     8B 48 74                     MOV ECX,dword ptr [EAX + 0x74]
005386a8     8B 04 91                     MOV EAX,dword ptr [ECX + EDX*0x4]
005386ab     C6 05 88 6C 56 00 FF         MOV byte ptr [0x00566c88],0xff
005386b2     8D 4C 24 1C                  LEA ECX,[ESP + 0x1c]
005386b6     8A 10                        MOV DL,byte ptr [EAX]
005386b8     51                           PUSH ECX
005386b9     88 15 2C E5 5D 00            MOV byte ptr [0x005de52c],DL
005386bf     8A 40 01                     MOV AL,byte ptr [EAX + 0x1]
005386c2     A2 30 E5 5D 00               MOV [0x005de530],AL
005386c7     8D 54 24 28                  LEA EDX,[ESP + 0x28]
005386cb     8D 44 24 2C                  LEA EAX,[ESP + 0x2c]
005386cf     52                           PUSH EDX
005386d0     50                           PUSH EAX
005386d1     E8 AA 1B FF FF               CALL 0x0052a280
005386d6     83 C4 14                     ADD ESP,0x14
005386d9     B9 10 00 00 00               MOV ECX,0x10
005386de     33 C0                        XOR EAX,EAX
005386e0     BF 40 98 5E 00               MOV EDI,0x5e9840
005386e5     F3 AB                        STOSD.REP ES:EDI
005386e7     B9 10 00 00 00               MOV ECX,0x10
005386ec     BF 60 90 5E 00               MOV EDI,0x5e9060
005386f1     F3 AB                        STOSD.REP ES:EDI
005386f3     B9 10 00 00 00               MOV ECX,0x10
005386f8     BF 80 97 5E 00               MOV EDI,0x5e9780
005386fd     F3 AB                        STOSD.REP ES:EDI
005386ff     B9 10 00 00 00               MOV ECX,0x10
00538704     BF E0 96 5E 00               MOV EDI,0x5e96e0
00538709     F3 AB                        STOSD.REP ES:EDI
0053870b     B8 24 93 5E 00               MOV EAX,0x5e9324
00538710     89 58 FC                     MOV dword ptr [EAX + -0x4],EBX
00538713     C7 00 FF FF FF FF            MOV dword ptr [EAX],0xffffffff
00538719     83 C0 08                     ADD EAX,0x8
0053871c     3D 64 93 5E 00               CMP EAX,0x5e9364
00538721     7C ED                        JL 0x00538710
00538723     38 1D 28 E5 5D 00            CMP byte ptr [0x005de528],BL
00538729     75 4C                        JNZ 0x00538777
0053872b     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
00538731     8B 35 20 90 5E 00            MOV ESI,dword ptr [0x005e9020]
00538737     8B 01                        MOV EAX,dword ptr [ECX]
00538739     05 00 00 0F 00               ADD EAX,0xf0000
0053873e     3B F3                        CMP ESI,EBX
00538740     74 35                        JZ 0x00538777
00538742     8D 90 40 01 00 00            LEA EDX,[EAX + 0x140]
00538748     8B 3A                        MOV EDI,dword ptr [EDX]
0053874a     66 8B 42 08                  MOV AX,word ptr [EDX + 0x8]
0053874e     66 8B 4A 0A                  MOV CX,word ptr [EDX + 0xa]
00538752     81 E7 FF FF FF BF            AND EDI,0xbfffffff
00538758     89 3A                        MOV dword ptr [EDX],EDI
0053875a     66 89 42 0C                  MOV word ptr [EDX + 0xc],AX
0053875e     66 89 4A 0E                  MOV word ptr [EDX + 0xe],CX
00538762     8D 7A 40                     LEA EDI,[EDX + 0x40]
00538765     B9 20 00 00 00               MOV ECX,0x20
0053876a     33 C0                        XOR EAX,EAX
0053876c     81 C2 00 20 00 00            ADD EDX,0x2000
00538772     4E                           DEC ESI
00538773     F3 AB                        STOSD.REP ES:EDI
00538775     75 D1                        JNZ 0x00538748
00538777     5F                           POP EDI
00538778     5E                           POP ESI
00538779     5D                           POP EBP
0053877a     5B                           POP EBX
0053877b     83 C4 44                     ADD ESP,0x44
0053877e     C3                           RET
