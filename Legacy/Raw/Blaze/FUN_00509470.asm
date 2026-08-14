; Function: FUN_00509470
; Entry:    00509470
; Body:     [[00509470, 005098d7]]

; --------------------------------------------------
; Range: 00509470 - 005098d7
; --------------------------------------------------
00509470     A1 48 2A 56 00           MOV        EAX,[0x00562a48]
00509475     83 EC 14                 SUB        ESP,0x14
00509478     53                       PUSH       EBX
00509479     33 DB                    XOR        EBX,EBX
0050947b     55                       PUSH       EBP
0050947c     56                       PUSH       ESI
0050947d     3B C3                    CMP        EAX,EBX
0050947f     57                       PUSH       EDI
00509480     BD 00 10 00 00           MOV        EBP,0x1000
00509485     BE 00 00 00 01           MOV        ESI,0x1000000
0050948a     0F 84 88 00 00 00        JZ         0x00509518
00509490     33 FF                    XOR        EDI,EDI
00509492     89 1D 48 2A 56 00        MOV        dword ptr [0x00562a48],EBX
00509498     C6 05 6A 90 5E 00 0F     MOV        byte ptr [0x005e906a],0xf
0050949f     88 1D 9F 90 5E 00        MOV        byte ptr [0x005e909f],BL
005094a5     89 3D 04 8E 57 00        MOV        dword ptr [0x00578e04],EDI
005094ab     B8 6A 33 57 00           MOV        EAX,0x57336a
005094b0     B9 03 00 00 00           MOV        ECX,0x3
005094b5     66 89 68 FE              MOV        word ptr [EAX + -0x2],BP
005094b9     66 89 28                 MOV        word ptr [EAX],BP
005094bc     89 70 0E                 MOV        dword ptr [EAX + 0xe],ESI
005094bf     C6 40 16 00              MOV        byte ptr [EAX + 0x16],0x0
005094c3     66 89 58 E6              MOV        word ptr [EAX + -0x1a],BX
005094c7     05 9C 00 00 00           ADD        EAX,0x9c
005094cc     49                       DEC        ECX
005094cd     75 E6                    JNZ        0x005094b5
005094cf     A1 38 33 57 00           MOV        EAX,[0x00573338]
005094d4     8B 15 C4 97 5E 00        MOV        EDX,dword ptr [0x005e97c4]
005094da     8A 88 50 01 00 00        MOV        CL,byte ptr [EAX + 0x150]
005094e0     8B 42 74                 MOV        EAX,dword ptr [EDX + 0x74]
005094e3     83 E1 07                 AND        ECX,0x7
005094e6     8B 10                    MOV        EDX,dword ptr [EAX]
005094e8     33 C0                    XOR        EAX,EAX
005094ea     8D 0C CA                 LEA        ECX,[EDX + ECX*0x8]
005094ed     89 0D E4 8D 57 00        MOV        dword ptr [0x00578de4],ECX
005094f3     81 E9 60 90 5E 00        SUB        ECX,0x5e9060
005094f9     8A 94 01 60 90 5E 00     MOV        DL,byte ptr [ECX + EAX*0x1 + 0x5e9060]
00509500     88 90 60 90 5E 00        MOV        byte ptr [EAX + 0x5e9060],DL
00509506     40                       INC        EAX
00509507     83 F8 07                 CMP        EAX,0x7
0050950a     7C ED                    JL         0x005094f9
0050950c     C7 05 50 2A 56 00 03 00 00 00 MOV        dword ptr [0x00562a50],0x3
00509516     EB 06                    JMP        0x0050951e
00509518     8B 3D 04 8E 57 00        MOV        EDI,dword ptr [0x00578e04]
0050951e     39 1D 4C 2A 56 00        CMP        dword ptr [0x00562a4c],EBX
00509524     0F 85 34 01 00 00        JNZ        0x0050965e
0050952a     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
0050952f     83 C0 C7                 ADD        EAX,-0x39
00509532     83 F8 03                 CMP        EAX,0x3
00509535     0F 87 23 01 00 00        JA         0x0050965e
0050953b     FF 24 85 D8 98 50 00     JMP        dword ptr [EAX*0x4 + 0x5098d8]
00509542     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
00509546     66 8B 41 02              MOV        AX,word ptr [ECX + 0x2]
0050954a     F6 C4 40                 TEST       AH,0x40
0050954d     74 23                    JZ         0x00509572
0050954f     3B FB                    CMP        EDI,EBX
00509551     75 1F                    JNZ        0x00509572
00509553     E8 48 46 03 00           CALL       0x0053dba0
00509558     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
0050955d     C7 05 F0 8D 57 00 2E 00 00 00 MOV        dword ptr [0x00578df0],0x2e
00509567     48                       DEC        EAX
00509568     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
0050956d     E9 EC 00 00 00           JMP        0x0050965e
00509572     F6 C4 80                 TEST       AH,0x80
00509575     74 2C                    JZ         0x005095a3
00509577     83 FF 03                 CMP        EDI,0x3
0050957a     7D 27                    JGE        0x005095a3
0050957c     E8 FF 45 03 00           CALL       0x0053db80
00509581     A1 04 8E 57 00           MOV        EAX,[0x00578e04]
00509586     8D 14 80                 LEA        EDX,[EAX + EAX*0x4]
00509589     C1 E2 03                 SHL        EDX,0x3
0050958c     2B D0                    SUB        EDX,EAX
0050958e     66 89 1C 95 50 33 57 00  MOV        word ptr [EDX*0x4 + 0x573350],BX
00509596     8B 3D 04 8E 57 00        MOV        EDI,dword ptr [0x00578e04]
0050959c     47                       INC        EDI
0050959d     89 3D 04 8E 57 00        MOV        dword ptr [0x00578e04],EDI
005095a3     85 FF                    TEST       EDI,EDI
005095a5     BD 00 08 00 00           MOV        EBP,0x800
005095aa     7E 4C                    JLE        0x005095f8
005095ac     BE 50 33 57 00           MOV        ESI,0x573350
005095b1     0F BF 06                 MOVSX      EAX,word ptr [ESI]
005095b4     50                       PUSH       EAX
005095b5     E8 5C C4 03 00           CALL       0x00545a16
005095ba     66 81 3E 00 04           CMP        word ptr [ESI],0x400
005095bf     66 89 46 18              MOV        word ptr [ESI + 0x18],AX
005095c3     75 12                    JNZ        0x005095d7
005095c5     E8 76 DB 03 00           CALL       0x00547140
005095ca     99                       CDQ        
005095cb     B9 06 00 00 00           MOV        ECX,0x6
005095d0     F7 F9                    IDIV       ECX
005095d2     FE C2                    INC        DL
005095d4     88 56 30                 MOV        byte ptr [ESI + 0x30],DL
005095d7     66 8B 06                 MOV        AX,word ptr [ESI]
005095da     66 3B C5                 CMP        AX,BP
005095dd     7D 08                    JGE        0x005095e7
005095df     05 00 01 00 00           ADD        EAX,0x100
005095e4     66 89 06                 MOV        word ptr [ESI],AX
005095e7     8B 3D 04 8E 57 00        MOV        EDI,dword ptr [0x00578e04]
005095ed     43                       INC        EBX
005095ee     81 C6 9C 00 00 00        ADD        ESI,0x9c
005095f4     3B DF                    CMP        EBX,EDI
005095f6     7C B9                    JL         0x005095b1
005095f8     83 FF 03                 CMP        EDI,0x3
005095fb     75 5F                    JNZ        0x0050965c
005095fd     33 F6                    XOR        ESI,ESI
005095ff     33 C9                    XOR        ECX,ECX
00509601     B8 80 33 57 00           MOV        EAX,0x573380
00509606     66 39 68 D0              CMP        word ptr [EAX + -0x30],BP
0050960a     75 11                    JNZ        0x0050961d
0050960c     33 D2                    XOR        EDX,EDX
0050960e     8A 10                    MOV        DL,byte ptr [EAX]
00509610     05 9C 00 00 00           ADD        EAX,0x9c
00509615     03 CA                    ADD        ECX,EDX
00509617     46                       INC        ESI
00509618     83 FE 03                 CMP        ESI,0x3
0050961b     7C E9                    JL         0x00509606
0050961d     83 FE 03                 CMP        ESI,0x3
00509620     75 3A                    JNZ        0x0050965c
00509622     A0 6A 90 5E 00           MOV        AL,[0x005e906a]
00509627     8A 15 80 33 57 00        MOV        DL,byte ptr [0x00573380]
0050962d     02 C1                    ADD        AL,CL
0050962f     8A 0D 1C 34 57 00        MOV        CL,byte ptr [0x0057341c]
00509635     3A D1                    CMP        DL,CL
00509637     A2 6A 90 5E 00           MOV        [0x005e906a],AL
0050963c     0F 85 69 01 00 00        JNZ        0x005097ab
00509642     3A 0D B8 34 57 00        CMP        CL,byte ptr [0x005734b8]
00509648     0F 85 5D 01 00 00        JNZ        0x005097ab
0050964e     3C 63                    CMP        AL,0x63
00509650     0F 83 57 01 00 00        JNC        0x005097ad
00509656     FF 05 F0 8D 57 00        INC        dword ptr [0x00578df0]
0050965c     33 DB                    XOR        EBX,EBX
0050965e     66 A1 A0 93 5E 00        MOV        AX,[0x005e93a0]
00509664     B9 60 00 00 00           MOV        ECX,0x60
00509669     0F BF D0                 MOVSX      EDX,AX
0050966c     2B CA                    SUB        ECX,EDX
0050966e     BE 60 00 00 00           MOV        ESI,0x60
00509673     C1 F9 02                 SAR        ECX,0x2
00509676     66 03 C1                 ADD        AX,CX
00509679     66 8B 0D A2 93 5E 00     MOV        CX,word ptr [0x005e93a2]
00509680     0F BF D1                 MOVSX      EDX,CX
00509683     2B F2                    SUB        ESI,EDX
00509685     66 8B 15 A4 93 5E 00     MOV        DX,word ptr [0x005e93a4]
0050968c     C1 FE 02                 SAR        ESI,0x2
0050968f     66 03 CE                 ADD        CX,SI
00509692     BF 60 00 00 00           MOV        EDI,0x60
00509697     0F BF F2                 MOVSX      ESI,DX
0050969a     2B FE                    SUB        EDI,ESI
0050969c     66 89 0D A2 93 5E 00     MOV        word ptr [0x005e93a2],CX
005096a3     C1 FF 02                 SAR        EDI,0x2
005096a6     66 03 D7                 ADD        DX,DI
005096a9     66 A3 A0 93 5E 00        MOV        [0x005e93a0],AX
005096af     66 89 15 A4 93 5E 00     MOV        word ptr [0x005e93a4],DX
005096b6     0F BF D2                 MOVSX      EDX,DX
005096b9     52                       PUSH       EDX
005096ba     0F BF C9                 MOVSX      ECX,CX
005096bd     0F BF D0                 MOVSX      EDX,AX
005096c0     51                       PUSH       ECX
005096c1     52                       PUSH       EDX
005096c2     E8 D9 C3 03 00           CALL       0x00545aa0
005096c7     A1 C4 97 5E 00           MOV        EAX,[0x005e97c4]
005096cc     8B 5C 24 30              MOV        EBX,dword ptr [ESP + 0x30]
005096d0     33 FF                    XOR        EDI,EDI
005096d2     33 ED                    XOR        EBP,EBP
005096d4     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
005096da     BE 6A 33 57 00           MOV        ESI,0x57336a
005096df     8B 51 04                 MOV        EDX,dword ptr [ECX + 0x4]
005096e2     89 54 24 10              MOV        dword ptr [ESP + 0x10],EDX
005096e6     66 8B 46 E6              MOV        AX,word ptr [ESI + -0x1a]
005096ea     66 8B 4E FE              MOV        CX,word ptr [ESI + -0x2]
005096ee     8B 56 0E                 MOV        EDX,dword ptr [ESI + 0xe]
005096f1     66 D1 F8                 SAR        AX,0x1
005096f4     66 2B C8                 SUB        CX,AX
005096f7     66 03 06                 ADD        AX,word ptr [ESI]
005096fa     66 89 4C 24 1C           MOV        word ptr [ESP + 0x1c],CX
005096ff     8D 0C 7F                 LEA        ECX,[EDI + EDI*0x2]
00509702     66 89 44 24 1E           MOV        word ptr [ESP + 0x1e],AX
00509707     89 54 24 14              MOV        dword ptr [ESP + 0x14],EDX
0050970b     66 0F B6 46 16           MOVZX      AX,byte ptr [ESI + 0x16]
00509710     C1 E1 04                 SHL        ECX,0x4
00509713     8D 95 70 FE FF FF        LEA        EDX,[EBP + 0xfffffe70]
00509719     83 C1 6E                 ADD        ECX,0x6e
0050971c     52                       PUSH       EDX
0050971d     66 89 44 24 24           MOV        word ptr [ESP + 0x24],AX
00509722     66 89 4C 24 1C           MOV        word ptr [ESP + 0x1c],CX
00509727     E8 F0 C2 03 00           CALL       0x00545a1c
0050972c     0F BF 4E E6              MOVSX      ECX,word ptr [ESI + -0x1a]
00509730     0F AF C1                 IMUL       EAX,ECX
00509733     C1 F8 13                 SAR        EAX,0x13
00509736     66 01 44 24 18           ADD        word ptr [ESP + 0x18],AX
0050973b     8B 44 24 28              MOV        EAX,dword ptr [ESP + 0x28]
0050973f     6A 00                    PUSH       0x0
00509741     8D 54 24 14              LEA        EDX,[ESP + 0x14]
00509745     6A 00                    PUSH       0x0
00509747     52                       PUSH       EDX
00509748     53                       PUSH       EBX
00509749     50                       PUSH       EAX
0050974a     66 C7 44 24 2E 78 00     MOV        word ptr [ESP + 0x2e],0x78
00509751     E8 CA 87 01 00           CALL       0x00521f20
00509756     83 C4 14                 ADD        ESP,0x14
00509759     81 C5 90 01 00 00        ADD        EBP,0x190
0050975f     47                       INC        EDI
00509760     81 C6 9C 00 00 00        ADD        ESI,0x9c
00509766     81 FD B0 04 00 00        CMP        EBP,0x4b0
0050976c     8B D8                    MOV        EBX,EAX
0050976e     0F 8C 72 FF FF FF        JL         0x005096e6
00509774     8B 0D C4 97 5E 00        MOV        ECX,dword ptr [0x005e97c4]
0050977a     6A 00                    PUSH       0x0
0050977c     68 80 21 56 00           PUSH       0x562180
00509781     6A 00                    PUSH       0x0
00509783     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
00509786     8B 82 F4 00 00 00        MOV        EAX,dword ptr [EDX + 0xf4]
0050978c     50                       PUSH       EAX
0050978d     E8 BE AB 03 00           CALL       0x00544350
00509792     83 C4 10                 ADD        ESP,0x10
00509795     68 80 21 56 00           PUSH       0x562180
0050979a     53                       PUSH       EBX
0050979b     E8 00 AC 03 00           CALL       0x005443a0
005097a0     83 C4 08                 ADD        ESP,0x8
005097a3     5F                       POP        EDI
005097a4     5E                       POP        ESI
005097a5     5D                       POP        EBP
005097a6     5B                       POP        EBX
005097a7     83 C4 14                 ADD        ESP,0x14
005097aa     C3                       RET        
005097ab     3C 63                    CMP        AL,0x63
005097ad     76 07                    JBE        0x005097b6
005097af     C6 05 6A 90 5E 00 63     MOV        byte ptr [0x005e906a],0x63
005097b6     83 05 F0 8D 57 00 02     ADD        dword ptr [0x00578df0],0x2
005097bd     E9 9A FE FF FF           JMP        0x0050965c
005097c2     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
005097c9     0F 85 8F FE FF FF        JNZ        0x0050965e
005097cf     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
005097d3     B9 03 00 00 00           MOV        ECX,0x3
005097d8     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
005097de     C7 05 F0 8D 57 00 39 00 00 00 MOV        dword ptr [0x00578df0],0x39
005097e8     89 1D 04 8E 57 00        MOV        dword ptr [0x00578e04],EBX
005097ee     B8 6A 33 57 00           MOV        EAX,0x57336a
005097f3     66 89 68 FE              MOV        word ptr [EAX + -0x2],BP
005097f7     66 89 28                 MOV        word ptr [EAX],BP
005097fa     89 70 0E                 MOV        dword ptr [EAX + 0xe],ESI
005097fd     C6 40 16 00              MOV        byte ptr [EAX + 0x16],0x0
00509801     66 89 58 E6              MOV        word ptr [EAX + -0x1a],BX
00509805     05 9C 00 00 00           ADD        EAX,0x9c
0050980a     49                       DEC        ECX
0050980b     75 E6                    JNZ        0x005097f3
0050980d     E9 4C FE FF FF           JMP        0x0050965e
00509812     F6 05 EF 0F 56 00 02     TEST       byte ptr [0x00560fef],0x2
00509819     8B 44 24 34              MOV        EAX,dword ptr [ESP + 0x34]
0050981d     75 16                    JNZ        0x00509835
0050981f     66 81 60 02 FF 7F        AND        word ptr [EAX + 0x2],0x7fff
00509825     A1 F0 8D 57 00           MOV        EAX,[0x00578df0]
0050982a     40                       INC        EAX
0050982b     A3 F0 8D 57 00           MOV        [0x00578df0],EAX
00509830     E9 29 FE FF FF           JMP        0x0050965e
00509835     F6 40 03 40              TEST       byte ptr [EAX + 0x3],0x40
00509839     0F 84 1F FE FF FF        JZ         0x0050965e
0050983f     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
00509844     C6 05 5B 07 56 00 00     MOV        byte ptr [0x0056075b],0x0
0050984b     24 FD                    AND        AL,0xfd
0050984d     A2 EF 0F 56 00           MOV        [0x00560fef],AL
00509852     EB 66                    JMP        0x005098ba
00509854     F6 05 EF 0F 56 00 04     TEST       byte ptr [0x00560fef],0x4
0050985b     75 3C                    JNZ        0x00509899
0050985d     A0 9F 90 5E 00           MOV        AL,[0x005e909f]
00509862     84 C0                    TEST       AL,AL
00509864     74 19                    JZ         0x0050987f
00509866     C7 05 F0 8D 57 00 39 00 00 00 MOV        dword ptr [0x00578df0],0x39
00509870     C7 05 48 2A 56 00 01 00 00 00 MOV        dword ptr [0x00562a48],0x1
0050987a     E9 DF FD FF FF           JMP        0x0050965e
0050987f     A1 E8 8D 57 00           MOV        EAX,[0x00578de8]
00509884     C7 05 F0 8D 57 00 3E 00 00 00 MOV        dword ptr [0x00578df0],0x3e
0050988e     40                       INC        EAX
0050988f     A3 E8 8D 57 00           MOV        [0x00578de8],EAX
00509894     E9 C5 FD FF FF           JMP        0x0050965e
00509899     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
0050989d     F6 41 03 40              TEST       byte ptr [ECX + 0x3],0x40
005098a1     0F 84 B7 FD FF FF        JZ         0x0050965e
005098a7     A0 EF 0F 56 00           MOV        AL,[0x00560fef]
005098ac     C6 05 63 07 56 00 00     MOV        byte ptr [0x00560763],0x0
005098b3     24 FB                    AND        AL,0xfb
005098b5     A2 EF 0F 56 00           MOV        [0x00560fef],AL
005098ba     E8 E1 42 03 00           CALL       0x0053dba0
005098bf     C7 05 F0 8D 57 00 39 00 00 00 MOV        dword ptr [0x00578df0],0x39
005098c9     C7 05 48 2A 56 00 01 00 00 00 MOV        dword ptr [0x00562a48],0x1
005098d3     E9 86 FD FF FF           JMP        0x0050965e

