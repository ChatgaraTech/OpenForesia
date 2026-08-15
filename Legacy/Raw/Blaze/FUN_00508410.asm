; Function: FUN_00508410
; Entry:    00508410

00508410     A1 48 2A 56 00           MOV EAX,[0x00562a48]
00508415     83 EC 14                 SUB ESP,0x14
00508418     53                       PUSH EBX
00508419     33 DB                    XOR EBX,EBX
0050841b     56                       PUSH ESI
0050841c     3B C3                    CMP EAX,EBX
0050841e     57                       PUSH EDI
0050841f     74 54                    JZ 0x00508475
00508421     89 1D 48 2A 56 00        MOV dword ptr [0x00562a48],EBX
00508427     89 1D F8 8D 57 00        MOV dword ptr [0x00578df8],EBX
0050842d     89 1D FC 8D 57 00        MOV dword ptr [0x00578dfc],EBX
00508433     66 C7 05 1C 35 57 00 28 00 MOV word ptr [0x0057351c],0x28
0050843c     66 C7 05 1E 35 57 00 38 00 MOV word ptr [0x0057351e],0x38
00508445     66 C7 05 20 35 57 00 E0 00 MOV word ptr [0x00573520],0xe0
0050844e     66 C7 05 22 35 57 00 48 00 MOV word ptr [0x00573522],0x48
00508457     C7 05 38 35 57 00 24 0C 07 01 MOV dword ptr [0x00573538],0x1070c24
00508461     C7 05 3C 35 57 00 00 00 00 01 MOV dword ptr [0x0057353c],0x1000000
0050846b     C7 05 50 2A 56 00 02 00 00 00 MOV dword ptr [0x00562a50],0x2
00508475     A1 38 33 57 00           MOV EAX,[0x00573338]
0050847a     66 0F B6 80 50 01 00 00  MOVZX AX,byte ptr [EAX + 0x150]
00508482     83 E8 08                 SUB EAX,0x8
00508485     C1 E0 08                 SHL EAX,0x8
00508488     66 2B 05 2A 49 60 00     SUB AX,word ptr [0x0060492a]
0050848f     25 FF 0F 00 00           AND EAX,0xfff
00508494     66 3D 00 08              CMP AX,0x800
00508498     7D 08                    JGE 0x005084a2
0050849a     0F BF C0                 MOVSX EAX,AX
0050849d     C1 F8 02                 SAR EAX,0x2
005084a0     EB 0F                    JMP 0x005084b1
005084a2     0F BF C8                 MOVSX ECX,AX
005084a5     B8 00 10 00 00           MOV EAX,0x1000
005084aa     2B C1                    SUB EAX,ECX
005084ac     C1 F8 02                 SAR EAX,0x2
005084af     F7 D8                    NEG EAX
005084b1     66 01 05 2A 49 60 00     ADD word ptr [0x0060492a],AX
005084b8     66 A1 A0 93 5E 00        MOV AX,[0x005e93a0]
005084be     0F BF D0                 MOVSX EDX,AX
005084c1     B9 60 00 00 00           MOV ECX,0x60
005084c6     68 E0 08 60 00           PUSH 0x6008e0
005084cb     2B CA                    SUB ECX,EDX
005084cd     68 28 49 60 00           PUSH 0x604928
005084d2     C1 F9 02                 SAR ECX,0x2
005084d5     66 03 C1                 ADD AX,CX
005084d8     B9 60 00 00 00           MOV ECX,0x60
005084dd     66 A3 A0 93 5E 00        MOV [0x005e93a0],AX
005084e3     66 A1 A2 93 5E 00        MOV AX,[0x005e93a2]
005084e9     0F BF D0                 MOVSX EDX,AX
005084ec     2B CA                    SUB ECX,EDX
005084ee     C1 F9 02                 SAR ECX,0x2
005084f1     66 03 C1                 ADD AX,CX
005084f4     B9 60 00 00 00           MOV ECX,0x60
005084f9     66 A3 A2 93 5E 00        MOV [0x005e93a2],AX
005084ff     66 A1 A4 93 5E 00        MOV AX,[0x005e93a4]
00508505     0F BF D0                 MOVSX EDX,AX
00508508     2B CA                    SUB ECX,EDX
0050850a     C1 F9 02                 SAR ECX,0x2
0050850d     66 03 C1                 ADD AX,CX
00508510     66 A3 A4 93 5E 00        MOV [0x005e93a4],AX
00508516     E8 E5 3D 01 00           CALL 0x0051c300
0050851b     0F BF 15 A4 93 5E 00     MOVSX EDX,word ptr [0x005e93a4]
00508522     0F BF 05 A2 93 5E 00     MOVSX EAX,word ptr [0x005e93a2]
00508529     0F BF 0D A0 93 5E 00     MOVSX ECX,word ptr [0x005e93a0]
00508530     83 C4 08                 ADD ESP,0x8
00508533     52                       PUSH EDX
00508534     50                       PUSH EAX
00508535     51                       PUSH ECX
00508536     E8 65 D5 03 00           CALL 0x00545aa0
0050853b     66 A1 7A 2B 57 00        MOV AX,[0x00572b7a]
00508541     8B 0D 38 33 57 00        MOV ECX,dword ptr [0x00573338]
00508547     33 D2                    XOR EDX,EDX
00508549     8A 91 50 01 00 00        MOV DL,byte ptr [ECX + 0x150]
0050854f     0F BF F0                 MOVSX ESI,AX
00508552     C1 E2 08                 SHL EDX,0x8
00508555     F7 DE                    NEG ESI
00508557     2B F2                    SUB ESI,EDX
00508559     C1 FE 02                 SAR ESI,0x2
0050855c     66 03 C6                 ADD AX,SI
0050855f     66 A3 7A 2B 57 00        MOV [0x00572b7a],AX
00508565     A1 4C 2A 56 00           MOV EAX,[0x00562a4c]
0050856a     3B C3                    CMP EAX,EBX
0050856c     0F 85 C2 01 00 00        JNZ 0x00508734
00508572     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
00508577     BE 5A 00 00 00           MOV ESI,0x5a
0050857c     2B C6                    SUB EAX,ESI
0050857e     0F 84 E2 00 00 00        JZ 0x00508666
00508584     48                       DEC EAX
00508585     0F 84 85 00 00 00        JZ 0x00508610
0050858b     48                       DEC EAX
0050858c     0F 85 A2 01 00 00        JNZ 0x00508734
00508592     F6 05 EF 0F 56 00 04     TEST byte ptr [0x00560fef],0x4
00508599     75 45                    JNZ 0x005085e0
0050859b     38 1D 9F 90 5E 00        CMP byte ptr [0x005e909f],BL
005085a1     74 0B                    JZ 0x005085ae
005085a3     89 35 F0 8D 57 00        MOV dword ptr [0x00578df0],ESI
005085a9     E9 86 01 00 00           JMP 0x00508734
005085ae     A0 FC 8D 57 00           MOV AL,[0x00578dfc]
005085b3     8B 3D E8 8D 57 00        MOV EDI,dword ptr [0x00578de8]
005085b9     8A 15 F8 8D 57 00        MOV DL,byte ptr [0x00578df8]
005085bf     47                       INC EDI
005085c0     C0 E0 02                 SHL AL,0x2
005085c3     0A C2                    OR AL,DL
005085c5     C7 05 F0 8D 57 00 5F 00 00 00 MOV dword ptr [0x00578df0],0x5f
005085cf     89 3D E8 8D 57 00        MOV dword ptr [0x00578de8],EDI
005085d5     88 81 52 01 00 00        MOV byte ptr [ECX + 0x152],AL
005085db     E9 54 01 00 00           JMP 0x00508734
005085e0     8B 4C 24 30              MOV ECX,dword ptr [ESP + 0x30]
005085e4     F6 41 03 40              TEST byte ptr [ECX + 0x3],0x40
005085e8     0F 84 46 01 00 00        JZ 0x00508734
005085ee     A0 EF 0F 56 00           MOV AL,[0x00560fef]
005085f3     88 1D 63 07 56 00        MOV byte ptr [0x00560763],BL
005085f9     24 FB                    AND AL,0xfb
005085fb     A2 EF 0F 56 00           MOV [0x00560fef],AL
00508600     E8 9B 55 03 00           CALL 0x0053dba0
00508605     89 35 F0 8D 57 00        MOV dword ptr [0x00578df0],ESI
0050860b     E9 24 01 00 00           JMP 0x00508734
00508610     F6 05 EF 0F 56 00 02     TEST byte ptr [0x00560fef],0x2
00508617     75 1A                    JNZ 0x00508633
00508619     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
0050861d     66 81 60 02 FF 7F        AND word ptr [EAX + 0x2],0x7fff
00508623     A1 F0 8D 57 00           MOV EAX,[0x00578df0]
00508628     40                       INC EAX
00508629     A3 F0 8D 57 00           MOV [0x00578df0],EAX
0050862e     E9 01 01 00 00           JMP 0x00508734
00508633     8B 54 24 30              MOV EDX,dword ptr [ESP + 0x30]
00508637     F6 42 03 40              TEST byte ptr [EDX + 0x3],0x40
0050863b     0F 84 F3 00 00 00        JZ 0x00508734
00508641     8A 0D EF 0F 56 00        MOV CL,byte ptr [0x00560fef]
00508647     88 1D 5B 07 56 00        MOV byte ptr [0x0056075b],BL
0050864d     80 E1 FD                 AND CL,0xfd
00508650     88 0D EF 0F 56 00        MOV byte ptr [0x00560fef],CL
00508656     E8 45 55 03 00           CALL 0x0053dba0
0050865b     FF 0D F0 8D 57 00        DEC dword ptr [0x00578df0]
00508661     E9 CE 00 00 00           JMP 0x00508734
00508666     8B 74 24 30              MOV ESI,dword ptr [ESP + 0x30]
0050866a     66 8B 46 02              MOV AX,word ptr [ESI + 0x2]
0050866e     F6 C4 40                 TEST AH,0x40
00508671     74 1F                    JZ 0x00508692
00508673     E8 28 55 03 00           CALL 0x0053dba0
00508678     A1 E8 8D 57 00           MOV EAX,[0x00578de8]
0050867d     C7 05 F0 8D 57 00 13 00 00 00 MOV dword ptr [0x00578df0],0x13
00508687     48                       DEC EAX
00508688     A3 E8 8D 57 00           MOV [0x00578de8],EAX
0050868d     E9 A2 00 00 00           JMP 0x00508734
00508692     F6 C4 80                 TEST AH,0x80
00508695     74 19                    JZ 0x005086b0
00508697     25 FF 7F 00 00           AND EAX,0x7fff
0050869c     66 89 46 02              MOV word ptr [ESI + 0x2],AX
005086a0     E8 DB 54 03 00           CALL 0x0053db80
005086a5     FF 05 F0 8D 57 00        INC dword ptr [0x00578df0]
005086ab     E9 84 00 00 00           JMP 0x00508734
005086b0     A1 FC 8D 57 00           MOV EAX,[0x00578dfc]
005086b5     8B 0D F8 8D 57 00        MOV ECX,dword ptr [0x00578df8]
005086bb     8B D0                    MOV EDX,EAX
005086bd     C1 E2 08                 SHL EDX,0x8
005086c0     0B D1                    OR EDX,ECX
005086c2     F6 46 06 10              TEST byte ptr [ESI + 0x6],0x10
005086c6     74 0A                    JZ 0x005086d2
005086c8     3B C3                    CMP EAX,EBX
005086ca     74 06                    JZ 0x005086d2
005086cc     48                       DEC EAX
005086cd     A3 FC 8D 57 00           MOV [0x00578dfc],EAX
005086d2     F6 46 06 40              TEST byte ptr [ESI + 0x6],0x40
005086d6     74 0B                    JZ 0x005086e3
005086d8     83 F8 03                 CMP EAX,0x3
005086db     7D 06                    JGE 0x005086e3
005086dd     40                       INC EAX
005086de     A3 FC 8D 57 00           MOV [0x00578dfc],EAX
005086e3     F6 46 06 80              TEST byte ptr [ESI + 0x6],0x80
005086e7     74 1C                    JZ 0x00508705
005086e9     3B CB                    CMP ECX,EBX
005086eb     74 03                    JZ 0x005086f0
005086ed     49                       DEC ECX
005086ee     EB 0F                    JMP 0x005086ff
005086f0     3B C3                    CMP EAX,EBX
005086f2     74 11                    JZ 0x00508705
005086f4     48                       DEC EAX
005086f5     B9 03 00 00 00           MOV ECX,0x3
005086fa     A3 FC 8D 57 00           MOV [0x00578dfc],EAX
005086ff     89 0D F8 8D 57 00        MOV dword ptr [0x00578df8],ECX
00508705     F6 46 06 20              TEST byte ptr [ESI + 0x6],0x20
00508709     74 1B                    JZ 0x00508726
0050870b     83 F9 03                 CMP ECX,0x3
0050870e     7D 03                    JGE 0x00508713
00508710     41                       INC ECX
00508711     EB 0D                    JMP 0x00508720
00508713     83 F8 03                 CMP EAX,0x3
00508716     7D 0E                    JGE 0x00508726
00508718     40                       INC EAX
00508719     33 C9                    XOR ECX,ECX
0050871b     A3 FC 8D 57 00           MOV [0x00578dfc],EAX
00508720     89 0D F8 8D 57 00        MOV dword ptr [0x00578df8],ECX
00508726     C1 E0 08                 SHL EAX,0x8
00508729     0B C1                    OR EAX,ECX
0050872b     3B D0                    CMP EDX,EAX
0050872d     74 05                    JZ 0x00508734
0050872f     E8 2C 54 03 00           CALL 0x0053db60
00508734     8B 7C 24 24              MOV EDI,dword ptr [ESP + 0x24]
00508738     8D 44 24 2C              LEA EAX,[ESP + 0x2c]
0050873c     50                       PUSH EAX
0050873d     57                       PUSH EDI
0050873e     E8 DD 36 01 00           CALL 0x0051be20
00508743     8B 0D C0 97 5E 00        MOV ECX,dword ptr [0x005e97c0]
00508749     83 C4 08                 ADD ESP,0x8
0050874c     8B 91 94 00 00 00        MOV EDX,dword ptr [ECX + 0x94]
00508752     53                       PUSH EBX
00508753     53                       PUSH EBX
00508754     8B 02                    MOV EAX,dword ptr [EDX]
00508756     8B 15 1C 35 57 00        MOV EDX,dword ptr [0x0057351c]
0050875c     89 44 24 14              MOV dword ptr [ESP + 0x14],EAX
00508760     A1 F8 8D 57 00           MOV EAX,[0x00578df8]
00508765     66 C7 44 24 24 18 00     MOV word ptr [ESP + 0x24],0x18
0050876c     C7 44 24 18 00 00 00 01  MOV dword ptr [ESP + 0x18],0x1000000
00508774     8D 0C 40                 LEA ECX,[EAX + EAX*0x2]
00508777     C1 E1 04                 SHL ECX,0x4
0050877a     8D 44 11 14              LEA EAX,[ECX + EDX*0x1 + 0x14]
0050877e     8B 0D FC 8D 57 00        MOV ECX,dword ptr [0x00578dfc]
00508784     C1 E1 04                 SHL ECX,0x4
00508787     66 03 0D 1E 35 57 00     ADD CX,word ptr [0x0057351e]
0050878e     66 89 44 24 1C           MOV word ptr [ESP + 0x1c],AX
00508793     8B 44 24 34              MOV EAX,dword ptr [ESP + 0x34]
00508797     8D 54 24 14              LEA EDX,[ESP + 0x14]
0050879b     52                       PUSH EDX
0050879c     83 C1 0C                 ADD ECX,0xc
0050879f     50                       PUSH EAX
005087a0     57                       PUSH EDI
005087a1     66 89 4C 24 2A           MOV word ptr [ESP + 0x2a],CX
005087a6     E8 45 96 01 00           CALL 0x00521df0
005087ab     83 C4 14                 ADD ESP,0x14
005087ae     8D 4C 24 0C              LEA ECX,[ESP + 0xc]
005087b2     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
005087b6     51                       PUSH ECX
005087b7     50                       PUSH EAX
005087b8     57                       PUSH EDI
005087b9     E8 02 72 01 00           CALL 0x0051f9c0
005087be     83 C4 0C                 ADD ESP,0xc
005087c1     8D 54 24 2C              LEA EDX,[ESP + 0x2c]
005087c5     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
005087c9     52                       PUSH EDX
005087ca     57                       PUSH EDI
005087cb     E8 E0 35 01 00           CALL 0x0051bdb0
005087d0     83 C4 08                 ADD ESP,0x8
005087d3     53                       PUSH EBX
005087d4     68 18 35 57 00           PUSH 0x573518
005087d9     53                       PUSH EBX
005087da     68 F8 06 56 00           PUSH 0x5606f8
005087df     E8 6C BB 03 00           CALL 0x00544350
005087e4     8B 44 24 3C              MOV EAX,dword ptr [ESP + 0x3c]
005087e8     83 C4 10                 ADD ESP,0x10
005087eb     68 18 35 57 00           PUSH 0x573518
005087f0     50                       PUSH EAX
005087f1     E8 AA BB 03 00           CALL 0x005443a0
005087f6     83 C4 08                 ADD ESP,0x8
005087f9     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
005087fd     BE D8 04 00 00           MOV ESI,0x4d8
00508802     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
00508808     53                       PUSH EBX
00508809     68 18 35 57 00           PUSH 0x573518
0050880e     53                       PUSH EBX
0050880f     8B 51 70                 MOV EDX,dword ptr [ECX + 0x70]
00508812     8B 04 32                 MOV EAX,dword ptr [EDX + ESI*0x1]
00508815     50                       PUSH EAX
00508816     E8 35 BB 03 00           CALL 0x00544350
0050881b     8B 4C 24 3C              MOV ECX,dword ptr [ESP + 0x3c]
0050881f     83 C4 10                 ADD ESP,0x10
00508822     68 18 35 57 00           PUSH 0x573518
00508827     51                       PUSH ECX
00508828     E8 73 BB 03 00           CALL 0x005443a0
0050882d     83 C6 04                 ADD ESI,0x4
00508830     83 C4 08                 ADD ESP,0x8
00508833     81 FE E8 04 00 00        CMP ESI,0x4e8
00508839     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
0050883d     7C C3                    JL 0x00508802
0050883f     68 18 35 57 00           PUSH 0x573518
00508844     50                       PUSH EAX
00508845     57                       PUSH EDI
00508846     E8 F5 CA 03 00           CALL 0x00545340
0050884b     83 C4 0C                 ADD ESP,0xc
0050884e     5F                       POP EDI
0050884f     5E                       POP ESI
00508850     5B                       POP EBX
00508851     83 C4 14                 ADD ESP,0x14
00508854     C3                       RET
