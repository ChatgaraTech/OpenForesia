; Function: FUN_00507010
; Entry:    00507010

00507010     A1 A8 33 56 00           MOV EAX,[0x005633a8]
00507015     83 EC 48                 SUB ESP,0x48
00507018     53                       PUSH EBX
00507019     55                       PUSH EBP
0050701a     33 ED                    XOR EBP,EBP
0050701c     56                       PUSH ESI
0050701d     8B 74 24 64              MOV ESI,dword ptr [ESP + 0x64]
00507021     3B C5                    CMP EAX,EBP
00507023     57                       PUSH EDI
00507024     0F 85 E9 00 00 00        JNZ 0x00507113
0050702a     66 8B 06                 MOV AX,word ptr [ESI]
0050702d     A8 01                    TEST AL,0x1
0050702f     74 71                    JZ 0x005070a2
00507031     F6 C4 04                 TEST AH,0x4
00507034     B9 F8 FF FF FF           MOV ECX,0xfffffff8
00507039     74 07                    JZ 0x00507042
0050703b     66 01 0D A2 93 5E 00     ADD word ptr [0x005e93a2],CX
00507042     F6 46 01 08              TEST byte ptr [ESI + 0x1],0x8
00507046     74 08                    JZ 0x00507050
00507048     66 83 05 A2 93 5E 00 08  ADD word ptr [0x005e93a2],0x8
00507050     F6 46 01 01              TEST byte ptr [ESI + 0x1],0x1
00507054     74 07                    JZ 0x0050705d
00507056     66 01 0D A4 93 5E 00     ADD word ptr [0x005e93a4],CX
0050705d     F6 46 01 02              TEST byte ptr [ESI + 0x1],0x2
00507061     74 08                    JZ 0x0050706b
00507063     66 83 05 A4 93 5E 00 08  ADD word ptr [0x005e93a4],0x8
0050706b     F6 46 01 10              TEST byte ptr [ESI + 0x1],0x10
0050706f     74 08                    JZ 0x00507079
00507071     66 83 05 A0 93 5E 00 08  ADD word ptr [0x005e93a0],0x8
00507079     F6 46 01 40              TEST byte ptr [ESI + 0x1],0x40
0050707d     74 07                    JZ 0x00507086
0050707f     66 01 0D A0 93 5E 00     ADD word ptr [0x005e93a0],CX
00507086     B8 FF 00 00 00           MOV EAX,0xff
0050708b     66 21 05 A0 93 5E 00     AND word ptr [0x005e93a0],AX
00507092     66 21 05 A2 93 5E 00     AND word ptr [0x005e93a2],AX
00507099     66 21 05 A4 93 5E 00     AND word ptr [0x005e93a4],AX
005070a0     EB 71                    JMP 0x00507113
005070a2     F6 C4 04                 TEST AH,0x4
005070a5     B9 C0 FF FF FF           MOV ECX,0xffffffc0
005070aa     74 07                    JZ 0x005070b3
005070ac     66 01 0D 0A 93 5E 00     ADD word ptr [0x005e930a],CX
005070b3     F6 46 01 08              TEST byte ptr [ESI + 0x1],0x8
005070b7     B8 40 00 00 00           MOV EAX,0x40
005070bc     74 07                    JZ 0x005070c5
005070be     66 01 05 0A 93 5E 00     ADD word ptr [0x005e930a],AX
005070c5     F6 46 01 01              TEST byte ptr [ESI + 0x1],0x1
005070c9     74 07                    JZ 0x005070d2
005070cb     66 01 0D 0C 93 5E 00     ADD word ptr [0x005e930c],CX
005070d2     F6 46 01 02              TEST byte ptr [ESI + 0x1],0x2
005070d6     74 07                    JZ 0x005070df
005070d8     66 01 05 0C 93 5E 00     ADD word ptr [0x005e930c],AX
005070df     F6 46 01 10              TEST byte ptr [ESI + 0x1],0x10
005070e3     74 07                    JZ 0x005070ec
005070e5     66 01 05 08 93 5E 00     ADD word ptr [0x005e9308],AX
005070ec     F6 46 01 40              TEST byte ptr [ESI + 0x1],0x40
005070f0     74 07                    JZ 0x005070f9
005070f2     66 01 0D 08 93 5E 00     ADD word ptr [0x005e9308],CX
005070f9     B8 FF 0F 00 00           MOV EAX,0xfff
005070fe     66 21 05 08 93 5E 00     AND word ptr [0x005e9308],AX
00507105     66 21 05 0A 93 5E 00     AND word ptr [0x005e930a],AX
0050710c     66 21 05 0C 93 5E 00     AND word ptr [0x005e930c],AX
00507113     39 2D 48 2A 56 00        CMP dword ptr [0x00562a48],EBP
00507119     0F 84 2C 01 00 00        JZ 0x0050724b
0050711f     B8 60 00 00 00           MOV EAX,0x60
00507124     89 2D 48 2A 56 00        MOV dword ptr [0x00562a48],EBP
0050712a     66 A3 A0 93 5E 00        MOV [0x005e93a0],AX
00507130     66 A3 A2 93 5E 00        MOV [0x005e93a2],AX
00507136     66 A3 A4 93 5E 00        MOV [0x005e93a4],AX
0050713c     A1 38 33 57 00           MOV EAX,[0x00573338]
00507141     66 C7 05 08 93 5E 00 00 02 MOV word ptr [0x005e9308],0x200
0050714a     66 C7 05 0A 93 5E 00 80 03 MOV word ptr [0x005e930a],0x380
00507153     66 89 2D 0C 93 5E 00     MOV word ptr [0x005e930c],BP
0050715a     66 C7 05 28 49 60 00 5E 01 MOV word ptr [0x00604928],0x15e
00507163     66 0F B6 88 50 01 00 00  MOVZX CX,byte ptr [EAX + 0x150]
0050716b     83 E9 08                 SUB ECX,0x8
0050716e     66 89 2D 2C 49 60 00     MOV word ptr [0x0060492c],BP
00507175     C1 E1 08                 SHL ECX,0x8
00507178     81 E1 FF 0F 00 00        AND ECX,0xfff
0050717e     89 2D D0 08 60 00        MOV dword ptr [0x006008d0],EBP
00507184     66 89 0D 2A 49 60 00     MOV word ptr [0x0060492a],CX
0050718b     89 2D D4 08 60 00        MOV dword ptr [0x006008d4],EBP
00507191     89 2D D8 08 60 00        MOV dword ptr [0x006008d8],EBP
00507197     89 2D F4 08 60 00        MOV dword ptr [0x006008f4],EBP
0050719d     89 2D F8 08 60 00        MOV dword ptr [0x006008f8],EBP
005071a3     89 2D FC 08 60 00        MOV dword ptr [0x006008fc],EBP
005071a9     33 DB                    XOR EBX,EBX
005071ab     BE 54 84 57 00           MOV ESI,0x578454
005071b0     8B FB                    MOV EDI,EBX
005071b2     66 89 6E FC              MOV word ptr [ESI + -0x4],BP
005071b6     C1 E7 08                 SHL EDI,0x8
005071b9     57                       PUSH EDI
005071ba     66 89 2E                 MOV word ptr [ESI],BP
005071bd     E8 5A E8 03 00           CALL 0x00545a1c
005071c2     8B C8                    MOV ECX,EAX
005071c4     57                       PUSH EDI
005071c5     C1 E1 04                 SHL ECX,0x4
005071c8     2B C8                    SUB ECX,EAX
005071ca     89 6E EC                 MOV dword ptr [ESI + -0x14],EBP
005071cd     F7 D9                    NEG ECX
005071cf     8D 14 89                 LEA EDX,[ECX + ECX*0x4]
005071d2     D1 E2                    SHL EDX,0x1
005071d4     89 56 E8                 MOV dword ptr [ESI + -0x18],EDX
005071d7     E8 3A E8 03 00           CALL 0x00545a16
005071dc     8D 04 40                 LEA EAX,[EAX + EAX*0x2]
005071df     8A CB                    MOV CL,BL
005071e1     80 E1 03                 AND CL,0x3
005071e4     88 5E D0                 MOV byte ptr [ESI + -0x30],BL
005071e7     8D 04 80                 LEA EAX,[EAX + EAX*0x4]
005071ea     66 89 6E F4              MOV word ptr [ESI + -0xc],BP
005071ee     66 89 6E F6              MOV word ptr [ESI + -0xa],BP
005071f2     66 89 6E F8              MOV word ptr [ESI + -0x8],BP
005071f6     8D 04 80                 LEA EAX,[EAX + EAX*0x4]
005071f9     C7 46 04 FF FF FF 00     MOV dword ptr [ESI + 0x4],0xffffff
00507200     D1 E0                    SHL EAX,0x1
00507202     89 46 F0                 MOV dword ptr [ESI + -0x10],EAX
00507205     66 C7 46 02 01 00        MOV word ptr [ESI + 0x2],0x1
0050720b     66 89 6E DC              MOV word ptr [ESI + -0x24],BP
0050720f     66 89 6E DE              MOV word ptr [ESI + -0x22],BP
00507213     66 89 6E E4              MOV word ptr [ESI + -0x1c],BP
00507217     66 89 6E E6              MOV word ptr [ESI + -0x1a],BP
0050721b     C6 46 D6 00              MOV byte ptr [ESI + -0x2a],0x0
0050721f     88 4E D7                 MOV byte ptr [ESI + -0x29],CL
00507222     C7 46 CC 00 00 80 00     MOV dword ptr [ESI + -0x34],0x800000
00507229     C6 46 D4 00              MOV byte ptr [ESI + -0x2c],0x0
0050722d     43                       INC EBX
0050722e     81 C6 9C 00 00 00        ADD ESI,0x9c
00507234     83 FB 10                 CMP EBX,0x10
00507237     0F 8C 73 FF FF FF        JL 0x005071b0
0050723d     8B 74 24 68              MOV ESI,dword ptr [ESP + 0x68]
00507241     C7 05 50 2A 56 00 01 00 00 00 MOV dword ptr [0x00562a50],0x1
0050724b     39 2D 4C 2A 56 00        CMP dword ptr [0x00562a4c],EBP
00507251     0F 85 EC 01 00 00        JNZ 0x00507443
00507257     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
0050725c     83 F8 77                 CMP EAX,0x77
0050725f     0F 87 DE 01 00 00        JA 0x00507443
00507265     33 D2                    XOR EDX,EDX
00507267     8A 90 F8 77 50 00        MOV DL,byte ptr [EAX + 0x5077f8]
0050726d     FF 24 95 E0 77 50 00     JMP dword ptr [EDX*0x4 + 0x5077e0]
00507443     8B 15 38 33 57 00        MOV EDX,dword ptr [0x00573338]
00507449     66 0F B6 82 50 01 00 00  MOVZX AX,byte ptr [EDX + 0x150]
00507451     83 E8 08                 SUB EAX,0x8
00507454     C1 E0 08                 SHL EAX,0x8
00507457     66 2B 05 2A 49 60 00     SUB AX,word ptr [0x0060492a]
0050745e     25 FF 0F 00 00           AND EAX,0xfff
00507463     66 3D 00 08              CMP AX,0x800
00507467     66 89 44 24 46           MOV word ptr [ESP + 0x46],AX
0050746c     7D 08                    JGE 0x00507476
0050746e     0F BF C0                 MOVSX EAX,AX
00507471     C1 F8 02                 SAR EAX,0x2
00507474     EB 0F                    JMP 0x00507485
00507476     0F BF C8                 MOVSX ECX,AX
00507479     B8 00 10 00 00           MOV EAX,0x1000
0050747e     2B C1                    SUB EAX,ECX
00507480     C1 F8 02                 SAR EAX,0x2
00507483     F7 D8                    NEG EAX
00507485     66 01 05 2A 49 60 00     ADD word ptr [0x0060492a],AX
0050748c     68 E0 08 60 00           PUSH 0x6008e0
00507491     68 28 49 60 00           PUSH 0x604928
00507496     E8 65 4E 01 00           CALL 0x0051c300
0050749b     0F BF 15 A4 93 5E 00     MOVSX EDX,word ptr [0x005e93a4]
005074a2     0F BF 05 A2 93 5E 00     MOVSX EAX,word ptr [0x005e93a2]
005074a9     0F BF 0D A0 93 5E 00     MOVSX ECX,word ptr [0x005e93a0]
005074b0     83 C4 08                 ADD ESP,0x8
005074b3     52                       PUSH EDX
005074b4     50                       PUSH EAX
005074b5     51                       PUSH ECX
005074b6     E8 E5 E5 03 00           CALL 0x00545aa0
005074bb     8B 5C 24 64              MOV EBX,dword ptr [ESP + 0x64]
005074bf     33 FF                    XOR EDI,EDI
005074c1     BE 4A 84 57 00           MOV ESI,0x57844a
005074c6     89 6C 24 68              MOV dword ptr [ESP + 0x68],EBP
005074ca     C7 44 24 10 00 20 00 00  MOV dword ptr [ESP + 0x10],0x2000
005074d2     A1 38 33 57 00           MOV EAX,[0x00573338]
005074d7     33 D2                    XOR EDX,EDX
005074d9     8A 90 50 01 00 00        MOV DL,byte ptr [EAX + 0x150]
005074df     66 8B 46 08              MOV AX,word ptr [ESI + 0x8]
005074e3     3B FA                    CMP EDI,EDX
005074e5     75 7D                    JNZ 0x00507564
005074e7     8B 54 24 68              MOV EDX,dword ptr [ESP + 0x68]
005074eb     0F BF C8                 MOVSX ECX,AX
005074ee     03 CA                    ADD ECX,EDX
005074f0     F7 D9                    NEG ECX
005074f2     C1 F9 02                 SAR ECX,0x2
005074f5     03 C8                    ADD ECX,EAX
005074f7     66 8B 46 FE              MOV AX,word ptr [ESI + -0x2]
005074fb     0F BF D0                 MOVSX EDX,AX
005074fe     66 89 4E 08              MOV word ptr [ESI + 0x8],CX
00507502     B9 00 18 00 00           MOV ECX,0x1800
00507507     2B CA                    SUB ECX,EDX
00507509     C1 F9 02                 SAR ECX,0x2
0050750c     03 C8                    ADD ECX,EAX
0050750e     66 8B 06                 MOV AX,word ptr [ESI]
00507511     0F BF D0                 MOVSX EDX,AX
00507514     66 89 4E FE              MOV word ptr [ESI + -0x2],CX
00507518     B9 00 18 00 00           MOV ECX,0x1800
0050751d     2B CA                    SUB ECX,EDX
0050751f     C1 F9 02                 SAR ECX,0x2
00507522     03 C8                    ADD ECX,EAX
00507524     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
00507528     0F BF D0                 MOVSX EDX,AX
0050752b     66 89 0E                 MOV word ptr [ESI],CX
0050752e     B9 00 18 00 00           MOV ECX,0x1800
00507533     2B CA                    SUB ECX,EDX
00507535     C1 F9 02                 SAR ECX,0x2
00507538     03 C8                    ADD ECX,EAX
0050753a     66 8B 46 E6              MOV AX,word ptr [ESI + -0x1a]
0050753e     0F BF D0                 MOVSX EDX,AX
00507541     F7 DA                    NEG EDX
00507543     C1 FA 02                 SAR EDX,0x2
00507546     03 D0                    ADD EDX,EAX
00507548     66 8B 46 E8              MOV AX,word ptr [ESI + -0x18]
0050754c     66 89 4E 02              MOV word ptr [ESI + 0x2],CX
00507550     66 89 56 E6              MOV word ptr [ESI + -0x1a],DX
00507554     0F BF C8                 MOVSX ECX,AX
00507557     F7 D9                    NEG ECX
00507559     C1 F9 02                 SAR ECX,0x2
0050755c     03 C8                    ADD ECX,EAX
0050755e     66 89 4E E8              MOV word ptr [ESI + -0x18],CX
00507562     EB 74                    JMP 0x005075d8
00507564     8B 4C 24 10              MOV ECX,dword ptr [ESP + 0x10]
00507568     0F BF D0                 MOVSX EDX,AX
0050756b     2B CA                    SUB ECX,EDX
0050756d     C1 F9 02                 SAR ECX,0x2
00507570     03 C8                    ADD ECX,EAX
00507572     66 8B 46 FE              MOV AX,word ptr [ESI + -0x2]
00507576     0F BF D0                 MOVSX EDX,AX
00507579     F7 DA                    NEG EDX
0050757b     C1 FA 02                 SAR EDX,0x2
0050757e     03 D0                    ADD EDX,EAX
00507580     66 8B 06                 MOV AX,word ptr [ESI]
00507583     66 89 4E 08              MOV word ptr [ESI + 0x8],CX
00507587     66 89 56 FE              MOV word ptr [ESI + -0x2],DX
0050758b     0F BF C8                 MOVSX ECX,AX
0050758e     F7 D9                    NEG ECX
00507590     C1 F9 02                 SAR ECX,0x2
00507593     03 C8                    ADD ECX,EAX
00507595     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
00507599     0F BF D0                 MOVSX EDX,AX
0050759c     F7 DA                    NEG EDX
0050759e     C1 FA 02                 SAR EDX,0x2
005075a1     03 D0                    ADD EDX,EAX
005075a3     66 8B 46 E6              MOV AX,word ptr [ESI + -0x1a]
005075a7     66 89 0E                 MOV word ptr [ESI],CX
005075aa     66 89 56 02              MOV word ptr [ESI + 0x2],DX
005075ae     0F BF C8                 MOVSX ECX,AX
005075b1     BA 00 10 00 00           MOV EDX,0x1000
005075b6     2B D1                    SUB EDX,ECX
005075b8     C1 FA 02                 SAR EDX,0x2
005075bb     03 D0                    ADD EDX,EAX
005075bd     66 8B 46 E8              MOV AX,word ptr [ESI + -0x18]
005075c1     0F BF C8                 MOVSX ECX,AX
005075c4     66 89 56 E6              MOV word ptr [ESI + -0x1a],DX
005075c8     BA 00 10 00 00           MOV EDX,0x1000
005075cd     2B D1                    SUB EDX,ECX
005075cf     C1 FA 02                 SAR EDX,0x2
005075d2     03 D0                    ADD EDX,EAX
005075d4     66 89 56 E8              MOV word ptr [ESI + -0x18],DX
005075d8     66 81 7E FE 00 01        CMP word ptr [ESI + -0x2],0x100
005075de     7C 67                    JL 0x00507647
005075e0     0F BF 4E EE              MOVSX ECX,word ptr [ESI + -0x12]
005075e4     33 C0                    XOR EAX,EAX
005075e6     8A 46 DA                 MOV AL,byte ptr [ESI + -0x26]
005075e9     8B 14 85 40 96 5E 00     MOV EDX,dword ptr [EAX*0x4 + 0x5e9640]
005075f0     8B 04 8A                 MOV EAX,dword ptr [EDX + ECX*0x4]
005075f3     66 FF 46 F0              INC word ptr [ESI + -0x10]
005075f7     66 8B 4E F0              MOV CX,word ptr [ESI + -0x10]
005075fb     33 D2                    XOR EDX,EDX
005075fd     66 8B 50 16              MOV DX,word ptr [EAX + 0x16]
00507601     0F BF C1                 MOVSX EAX,CX
00507604     3B C2                    CMP EAX,EDX
00507606     7C 04                    JL 0x0050760c
00507608     66 89 6E F0              MOV word ptr [ESI + -0x10],BP
0050760c     66 8B 4E E0              MOV CX,word ptr [ESI + -0x20]
00507610     33 C0                    XOR EAX,EAX
00507612     8A 46 DA                 MOV AL,byte ptr [ESI + -0x26]
00507615     51                       PUSH ECX
00507616     0F BF 56 F0              MOVSX EDX,word ptr [ESI + -0x10]
0050761a     C1 E0 02                 SHL EAX,0x2
0050761d     8D 4E F2                 LEA ECX,[ESI + -0xe]
00507620     52                       PUSH EDX
00507621     8B 90 A0 96 5E 00        MOV EDX,dword ptr [EAX + 0x5e96a0]
00507627     51                       PUSH ECX
00507628     0F BF 4E EE              MOVSX ECX,word ptr [ESI + -0x12]
0050762c     52                       PUSH EDX
0050762d     8B 90 40 96 5E 00        MOV EDX,dword ptr [EAX + 0x5e9640]
00507633     8B 04 8A                 MOV EAX,dword ptr [EDX + ECX*0x4]
00507636     8B 4C 24 6C              MOV ECX,dword ptr [ESP + 0x6c]
0050763a     50                       PUSH EAX
0050763b     53                       PUSH EBX
0050763c     51                       PUSH ECX
0050763d     E8 AE 5B 01 00           CALL 0x0051d1f0
00507642     83 C4 1C                 ADD ESP,0x1c
00507645     8B D8                    MOV EBX,EAX
00507647     8D 54 24 30              LEA EDX,[ESP + 0x30]
0050764b     6A 0A                    PUSH 0xa
0050764d     8D 46 F2                 LEA EAX,[ESI + -0xe]
00507650     52                       PUSH EDX
00507651     50                       PUSH EAX
00507652     E8 89 DC 02 00           CALL 0x005352e0
00507657     8B 44 24 40              MOV EAX,dword ptr [ESP + 0x40]
0050765b     83 C4 0C                 ADD ESP,0xc
0050765e     8D 4C 24 14              LEA ECX,[ESP + 0x14]
00507662     8D 54 24 30              LEA EDX,[ESP + 0x30]
00507666     05 00 C0 03 00           ADD EAX,0x3c000
0050766b     51                       PUSH ECX
0050766c     52                       PUSH EDX
0050766d     89 44 24 3C              MOV dword ptr [ESP + 0x3c],EAX
00507671     E8 DA 7F 01 00           CALL 0x0051f650
00507676     83 C4 08                 ADD ESP,0x8
00507679     85 C0                    TEST EAX,EAX
0050767b     0F 84 CE 00 00 00        JZ 0x0050774f
00507681     A1 C4 97 5E 00           MOV EAX,[0x005e97c4]
00507686     55                       PUSH EBP
00507687     0F BF D7                 MOVSX EDX,DI
0050768a     8B 88 94 00 00 00        MOV ECX,dword ptr [EAX + 0x94]
00507690     6A 02                    PUSH 0x2
00507692     8B 41 28                 MOV EAX,dword ptr [ECX + 0x28]
00507695     66 89 7C 24 34           MOV word ptr [ESP + 0x34],DI
0050769a     89 44 24 24              MOV dword ptr [ESP + 0x24],EAX
0050769e     8A 4C D0 02              MOV CL,byte ptr [EAX + EDX*0x8 + 0x2]
005076a2     8D 44 D0 02              LEA EAX,[EAX + EDX*0x8 + 0x2]
005076a6     FE C9                    DEC CL
005076a8     88 08                    MOV byte ptr [EAX],CL
005076aa     8B 4C 24 24              MOV ECX,dword ptr [ESP + 0x24]
005076ae     0F BF 44 24 34           MOVSX EAX,word ptr [ESP + 0x34]
005076b3     8A 54 C1 03              MOV DL,byte ptr [ECX + EAX*0x8 + 0x3]
005076b7     8D 44 C1 03              LEA EAX,[ECX + EAX*0x8 + 0x3]
005076bb     FE CA                    DEC DL
005076bd     88 10                    MOV byte ptr [EAX],DL
005076bf     66 8B 46 E8              MOV AX,word ptr [ESI + -0x18]
005076c3     66 8B 4C 24 1C           MOV CX,word ptr [ESP + 0x1c]
005076c8     66 8B 56 E6              MOV DX,word ptr [ESI + -0x1a]
005076cc     66 89 44 24 32           MOV word ptr [ESP + 0x32],AX
005076d1     8D 44 24 24              LEA EAX,[ESP + 0x24]
005076d5     66 89 4C 24 2C           MOV word ptr [ESP + 0x2c],CX
005076da     50                       PUSH EAX
005076db     0F BF 4C 24 24           MOVSX ECX,word ptr [ESP + 0x24]
005076e0     66 89 54 24 34           MOV word ptr [ESP + 0x34],DX
005076e5     66 8B 54 24 22           MOV DX,word ptr [ESP + 0x22]
005076ea     53                       PUSH EBX
005076eb     8B 5C 24 6C              MOV EBX,dword ptr [ESP + 0x6c]
005076ef     66 89 54 24 36           MOV word ptr [ESP + 0x36],DX
005076f4     C7 44 24 30 00 00 00 01  MOV dword ptr [ESP + 0x30],0x1000000
005076fc     8D 54 8B 50              LEA EDX,[EBX + ECX*0x4 + 0x50]
00507700     52                       PUSH EDX
00507701     E8 1A A8 01 00           CALL 0x00521f20
00507706     0F BF 54 24 2C           MOVSX EDX,word ptr [ESP + 0x2c]
0050770b     83 C4 14                 ADD ESP,0x14
0050770e     66 FF 4C 24 24           DEC word ptr [ESP + 0x24]
00507713     8D 4C 24 1C              LEA ECX,[ESP + 0x1c]
00507717     55                       PUSH EBP
00507718     6A 02                    PUSH 0x2
0050771a     51                       PUSH ECX
0050771b     50                       PUSH EAX
0050771c     8D 44 93 50              LEA EAX,[EBX + EDX*0x4 + 0x50]
00507720     50                       PUSH EAX
00507721     E8 FA A7 01 00           CALL 0x00521f20
00507726     0F BF 4C 24 40           MOVSX ECX,word ptr [ESP + 0x40]
0050772b     8B 54 24 30              MOV EDX,dword ptr [ESP + 0x30]
0050772f     8B D8                    MOV EBX,EAX
00507731     83 C4 14                 ADD ESP,0x14
00507734     8D 44 CA 02              LEA EAX,[EDX + ECX*0x8 + 0x2]
00507738     8A 54 CA 02              MOV DL,byte ptr [EDX + ECX*0x8 + 0x2]
0050773c     FE C2                    INC DL
0050773e     88 10                    MOV byte ptr [EAX],DL
00507740     8B 4C 24 1C              MOV ECX,dword ptr [ESP + 0x1c]
00507744     0F BF 44 24 2C           MOVSX EAX,word ptr [ESP + 0x2c]
00507749     8D 44 C1 03              LEA EAX,[ECX + EAX*0x8 + 0x3]
0050774d     FE 00                    INC byte ptr [EAX]
0050774f     8B 44 24 10              MOV EAX,dword ptr [ESP + 0x10]
00507753     8B 54 24 68              MOV EDX,dword ptr [ESP + 0x68]
00507757     2D 00 01 00 00           SUB EAX,0x100
0050775c     47                       INC EDI
0050775d     81 C2 00 01 00 00        ADD EDX,0x100
00507763     81 C6 9C 00 00 00        ADD ESI,0x9c
00507769     3D 00 10 00 00           CMP EAX,0x1000
0050776e     89 54 24 68              MOV dword ptr [ESP + 0x68],EDX
00507772     89 44 24 10              MOV dword ptr [ESP + 0x10],EAX
00507776     0F 8F 56 FD FF FF        JG 0x005074d2
0050777c     55                       PUSH EBP
0050777d     68 F0 0F 56 00           PUSH 0x560ff0
00507782     55                       PUSH EBP
00507783     68 F8 06 56 00           PUSH 0x5606f8
00507788     E8 C3 CB 03 00           CALL 0x00544350
0050778d     83 C4 10                 ADD ESP,0x10
00507790     68 F0 0F 56 00           PUSH 0x560ff0
00507795     53                       PUSH EBX
00507796     E8 05 CC 03 00           CALL 0x005443a0
0050779b     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
005077a1     8B F0                    MOV ESI,EAX
005077a3     A1 38 33 57 00           MOV EAX,[0x00573338]
005077a8     33 D2                    XOR EDX,EDX
005077aa     83 C4 08                 ADD ESP,0x8
005077ad     8A 90 50 01 00 00        MOV DL,byte ptr [EAX + 0x150]
005077b3     8B 41 70                 MOV EAX,dword ptr [ECX + 0x70]
005077b6     55                       PUSH EBP
005077b7     68 F0 0F 56 00           PUSH 0x560ff0
005077bc     8B 4C 90 0C              MOV ECX,dword ptr [EAX + EDX*0x4 + 0xc]
005077c0     55                       PUSH EBP
005077c1     51                       PUSH ECX
005077c2     E8 89 CB 03 00           CALL 0x00544350
005077c7     83 C4 10                 ADD ESP,0x10
005077ca     68 F0 0F 56 00           PUSH 0x560ff0
005077cf     56                       PUSH ESI
005077d0     E8 CB CB 03 00           CALL 0x005443a0
005077d5     83 C4 08                 ADD ESP,0x8
005077d8     5F                       POP EDI
005077d9     5E                       POP ESI
005077da     5D                       POP EBP
005077db     5B                       POP EBX
005077dc     83 C4 48                 ADD ESP,0x48
005077df     C3                       RET
