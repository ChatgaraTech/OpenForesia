; Function: FUN_00545340
; Entry:    00545340
; Body:     [[00545340, 00545a08]]

; --------------------------------------------------
; Range: 00545340 - 00545a08
; --------------------------------------------------
00545340     83 EC 18                 SUB        ESP,0x18
00545343     8D 44 24 20              LEA        EAX,[ESP + 0x20]
00545347     53                       PUSH       EBX
00545348     55                       PUSH       EBP
00545349     56                       PUSH       ESI
0054534a     8B 74 24 28              MOV        ESI,dword ptr [ESP + 0x28]
0054534e     57                       PUSH       EDI
0054534f     50                       PUSH       EAX
00545350     56                       PUSH       ESI
00545351     E8 CA 6A FD FF           CALL       0x0051be20
00545356     8B 6C 24 3C              MOV        EBP,dword ptr [ESP + 0x3c]
0054535a     8A 15 38 E5 5D 00        MOV        DL,byte ptr [0x005de538]
00545360     83 C4 08                 ADD        ESP,0x8
00545363     8B 85 B0 08 00 00        MOV        EAX,dword ptr [EBP + 0x8b0]
00545369     85 C0                    TEST       EAX,EAX
0054536b     74 42                    JZ         0x005453af
0054536d     8B 08                    MOV        ECX,dword ptr [EAX]
0054536f     81 E1 00 00 FC 00        AND        ECX,0xfc0000
00545375     81 F9 00 00 20 00        CMP        ECX,0x200000
0054537b     74 22                    JZ         0x0054539f
0054537d     81 F9 00 00 40 00        CMP        ECX,0x400000
00545383     74 11                    JZ         0x00545396
00545385     81 F9 00 00 80 00        CMP        ECX,0x800000
0054538b     75 22                    JNZ        0x005453af
0054538d     33 C9                    XOR        ECX,ECX
0054538f     8A 48 08                 MOV        CL,byte ptr [EAX + 0x8]
00545392     8B C1                    MOV        EAX,ECX
00545394     EB 2E                    JMP        0x005453c4
00545396     33 C9                    XOR        ECX,ECX
00545398     8A 48 09                 MOV        CL,byte ptr [EAX + 0x9]
0054539b     8B C1                    MOV        EAX,ECX
0054539d     EB 25                    JMP        0x005453c4
0054539f     8B 40 6C                 MOV        EAX,dword ptr [EAX + 0x6c]
005453a2     85 C0                    TEST       EAX,EAX
005453a4     74 09                    JZ         0x005453af
005453a6     33 C9                    XOR        ECX,ECX
005453a8     8A 48 08                 MOV        CL,byte ptr [EAX + 0x8]
005453ab     8B C1                    MOV        EAX,ECX
005453ad     EB 15                    JMP        0x005453c4
005453af     33 C0                    XOR        EAX,EAX
005453b1     8A C2                    MOV        AL,DL
005453b3     8D 0C 80                 LEA        ECX,[EAX + EAX*0x4]
005453b6     C1 E1 03                 SHL        ECX,0x3
005453b9     2B C8                    SUB        ECX,EAX
005453bb     33 C0                    XOR        EAX,EAX
005453bd     8A 04 8D C8 93 5E 00     MOV        AL,byte ptr [ECX*0x4 + 0x5e93c8]
005453c4     8D 0C 80                 LEA        ECX,[EAX + EAX*0x4]
005453c7     C1 E1 03                 SHL        ECX,0x3
005453ca     2B C8                    SUB        ECX,EAX
005453cc     F7 04 8D C0 93 5E 00 00 00 01 00 TEST       dword ptr [ECX*0x4 + 0x5e93c0],0x10000
005453d7     74 15                    JZ         0x005453ee
005453d9     33 C0                    XOR        EAX,EAX
005453db     8A C2                    MOV        AL,DL
005453dd     8D 14 80                 LEA        EDX,[EAX + EAX*0x4]
005453e0     C1 E2 03                 SHL        EDX,0x3
005453e3     2B D0                    SUB        EDX,EAX
005453e5     33 C0                    XOR        EAX,EAX
005453e7     8A 04 95 C8 93 5E 00     MOV        AL,byte ptr [EDX*0x4 + 0x5e93c8]
005453ee     6B C0 4C                 IMUL       EAX,EAX,0x4c
005453f1     05 80 91 5E 00           ADD        EAX,0x5e9180
005453f6     33 C9                    XOR        ECX,ECX
005453f8     33 D2                    XOR        EDX,EDX
005453fa     8A 48 09                 MOV        CL,byte ptr [EAX + 0x9]
005453fd     8A 50 08                 MOV        DL,byte ptr [EAX + 0x8]
00545400     A1 C0 97 5E 00           MOV        EAX,[0x005e97c0]
00545405     89 4C 24 1C              MOV        dword ptr [ESP + 0x1c],ECX
00545409     8B FA                    MOV        EDI,EDX
0054540b     8B 88 94 00 00 00        MOV        ECX,dword ptr [EAX + 0x94]
00545411     33 C0                    XOR        EAX,EAX
00545413     66 8B 45 00              MOV        AX,word ptr [EBP]
00545417     8B 11                    MOV        EDX,dword ptr [ECX]
00545419     8B 0C 85 00 8F 5E 00     MOV        ECX,dword ptr [EAX*0x4 + 0x5e8f00]
00545420     8B 45 08                 MOV        EAX,dword ptr [EBP + 0x8]
00545423     C1 E7 04                 SHL        EDI,0x4
00545426     89 44 24 18              MOV        dword ptr [ESP + 0x18],EAX
0054542a     8B 45 38                 MOV        EAX,dword ptr [EBP + 0x38]
0054542d     89 54 24 2C              MOV        dword ptr [ESP + 0x2c],EDX
00545431     8B 55 04                 MOV        EDX,dword ptr [EBP + 0x4]
00545434     81 C7 20 4A 60 00        ADD        EDI,0x604a20
0054543a     89 4C 24 34              MOV        dword ptr [ESP + 0x34],ECX
0054543e     A9 00 00 00 01           TEST       EAX,0x1000000
00545443     89 54 24 10              MOV        dword ptr [ESP + 0x10],EDX
00545447     89 44 24 14              MOV        dword ptr [ESP + 0x14],EAX
0054544b     0F 84 34 02 00 00        JZ         0x00545685
00545451     8A 8D C7 08 00 00        MOV        CL,byte ptr [EBP + 0x8c7]
00545457     F6 C1 20                 TEST       CL,0x20
0054545a     0F 85 25 02 00 00        JNZ        0x00545685
00545460     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
00545466     B3 40                    MOV        BL,0x40
00545468     84 C0                    TEST       AL,AL
0054546a     74 17                    JZ         0x00545483
0054546c     F6 47 06 10              TEST       byte ptr [EDI + 0x6],0x10
00545470     74 11                    JZ         0x00545483
00545472     84 CB                    TEST       BL,CL
00545474     74 0D                    JZ         0x00545483
00545476     2C 02                    SUB        AL,0x2
00545478     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
0054547e     E8 DD 86 FF FF           CALL       0x0053db60
00545483     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
00545489     84 C0                    TEST       AL,AL
0054548b     74 1C                    JZ         0x005454a9
0054548d     F6 47 06 10              TEST       byte ptr [EDI + 0x6],0x10
00545491     74 16                    JZ         0x005454a9
00545493     F6 85 C7 08 00 00 80     TEST       byte ptr [EBP + 0x8c7],0x80
0054549a     74 0D                    JZ         0x005454a9
0054549c     2C 03                    SUB        AL,0x3
0054549e     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
005454a4     E8 B7 86 FF FF           CALL       0x0053db60
005454a9     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
005454af     84 C0                    TEST       AL,AL
005454b1     74 1C                    JZ         0x005454cf
005454b3     F6 47 06 10              TEST       byte ptr [EDI + 0x6],0x10
005454b7     74 16                    JZ         0x005454cf
005454b9     F6 85 C7 08 00 00 C0     TEST       byte ptr [EBP + 0x8c7],0xc0
005454c0     75 0D                    JNZ        0x005454cf
005454c2     FE C8                    DEC        AL
005454c4     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
005454ca     E8 91 86 FF FF           CALL       0x0053db60
005454cf     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
005454d5     33 C9                    XOR        ECX,ECX
005454d7     8A 4D 11                 MOV        CL,byte ptr [EBP + 0x11]
005454da     8B D0                    MOV        EDX,EAX
005454dc     83 E9 02                 SUB        ECX,0x2
005454df     81 E2 FF 00 00 00        AND        EDX,0xff
005454e5     3B D1                    CMP        EDX,ECX
005454e7     7D 1A                    JGE        0x00545503
005454e9     84 5F 06                 TEST       byte ptr [EDI + 0x6],BL
005454ec     74 15                    JZ         0x00545503
005454ee     84 9D C7 08 00 00        TEST       byte ptr [EBP + 0x8c7],BL
005454f4     74 0D                    JZ         0x00545503
005454f6     04 02                    ADD        AL,0x2
005454f8     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
005454fe     E8 5D 86 FF FF           CALL       0x0053db60
00545503     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
00545509     33 C9                    XOR        ECX,ECX
0054550b     8A 4D 11                 MOV        CL,byte ptr [EBP + 0x11]
0054550e     8B D0                    MOV        EDX,EAX
00545510     83 E9 03                 SUB        ECX,0x3
00545513     81 E2 FF 00 00 00        AND        EDX,0xff
00545519     3B D1                    CMP        EDX,ECX
0054551b     7D 1B                    JGE        0x00545538
0054551d     84 5F 06                 TEST       byte ptr [EDI + 0x6],BL
00545520     74 16                    JZ         0x00545538
00545522     F6 85 C7 08 00 00 80     TEST       byte ptr [EBP + 0x8c7],0x80
00545529     74 0D                    JZ         0x00545538
0054552b     04 03                    ADD        AL,0x3
0054552d     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
00545533     E8 28 86 FF FF           CALL       0x0053db60
00545538     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
0054553e     33 C9                    XOR        ECX,ECX
00545540     8A 4D 11                 MOV        CL,byte ptr [EBP + 0x11]
00545543     8B D0                    MOV        EDX,EAX
00545545     49                       DEC        ECX
00545546     81 E2 FF 00 00 00        AND        EDX,0xff
0054554c     3B D1                    CMP        EDX,ECX
0054554e     7D 1B                    JGE        0x0054556b
00545550     84 5F 06                 TEST       byte ptr [EDI + 0x6],BL
00545553     74 16                    JZ         0x0054556b
00545555     F6 85 C7 08 00 00 C0     TEST       byte ptr [EBP + 0x8c7],0xc0
0054555c     75 0D                    JNZ        0x0054556b
0054555e     FE C0                    INC        AL
00545560     88 85 C4 08 00 00        MOV        byte ptr [EBP + 0x8c4],AL
00545566     E8 F5 85 FF FF           CALL       0x0053db60
0054556b     66 8B 47 02              MOV        AX,word ptr [EDI + 0x2]
0054556f     F6 C4 80                 TEST       AH,0x80
00545572     0F 84 8E 00 00 00        JZ         0x00545606
00545578     25 FF 7F 00 00           AND        EAX,0x7fff
0054557d     66 89 47 02              MOV        word ptr [EDI + 0x2],AX
00545581     8A 95 C7 08 00 00        MOV        DL,byte ptr [EBP + 0x8c7]
00545587     80 E2 FB                 AND        DL,0xfb
0054558a     C6 45 3B 00              MOV        byte ptr [EBP + 0x3b],0x0
0054558e     8A C2                    MOV        AL,DL
00545590     88 95 C7 08 00 00        MOV        byte ptr [EBP + 0x8c7],DL
00545596     84 C3                    TEST       BL,AL
00545598     74 16                    JZ         0x005455b0
0054559a     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
005455a0     D0 E8                    SHR        AL,0x1
005455a2     33 C9                    XOR        ECX,ECX
005455a4     8A 8D C5 08 00 00        MOV        CL,byte ptr [EBP + 0x8c5]
005455aa     88 81 60 90 5E 00        MOV        byte ptr [ECX + 0x5e9060],AL
005455b0     F6 85 C7 08 00 00 80     TEST       byte ptr [EBP + 0x8c7],0x80
005455b7     74 24                    JZ         0x005455dd
005455b9     33 C9                    XOR        ECX,ECX
005455bb     B8 56 55 55 55           MOV        EAX,0x55555556
005455c0     8A 8D C4 08 00 00        MOV        CL,byte ptr [EBP + 0x8c4]
005455c6     F7 E9                    IMUL       ECX
005455c8     8B C2                    MOV        EAX,EDX
005455ca     C1 E8 1F                 SHR        EAX,0x1f
005455cd     03 D0                    ADD        EDX,EAX
005455cf     33 C9                    XOR        ECX,ECX
005455d1     8A 8D C5 08 00 00        MOV        CL,byte ptr [EBP + 0x8c5]
005455d7     88 91 60 90 5E 00        MOV        byte ptr [ECX + 0x5e9060],DL
005455dd     F6 85 C7 08 00 00 C0     TEST       byte ptr [EBP + 0x8c7],0xc0
005455e4     75 14                    JNZ        0x005455fa
005455e6     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
005455ec     33 D2                    XOR        EDX,EDX
005455ee     8A 95 C5 08 00 00        MOV        DL,byte ptr [EBP + 0x8c5]
005455f4     88 82 60 90 5E 00        MOV        byte ptr [EDX + 0x5e9060],AL
005455fa     E8 81 85 FF FF           CALL       0x0053db80
005455ff     80 A5 C7 08 00 00 3F     AND        byte ptr [EBP + 0x8c7],0x3f
00545606     33 C0                    XOR        EAX,EAX
00545608     8B 54 24 2C              MOV        EDX,dword ptr [ESP + 0x2c]
0054560c     8A 85 C4 08 00 00        MOV        AL,byte ptr [EBP + 0x8c4]
00545612     8B 5D 3C                 MOV        EBX,dword ptr [EBP + 0x3c]
00545615     05 55 55 15 00           ADD        EAX,0x155555
0054561a     8D 0C 40                 LEA        ECX,[EAX + EAX*0x2]
0054561d     8B 44 24 14              MOV        EAX,dword ptr [ESP + 0x14]
00545621     C1 E1 0A                 SHL        ECX,0xa
00545624     03 C1                    ADD        EAX,ECX
00545626     8B 4C 24 1C              MOV        ECX,dword ptr [ESP + 0x1c]
0054562a     89 44 24 14              MOV        dword ptr [ESP + 0x14],EAX
0054562e     8B 94 CA D8 00 00 00     MOV        EDX,dword ptr [EDX + ECX*0x8 + 0xd8]
00545635     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
00545639     52                       PUSH       EDX
0054563a     8B 54 24 34              MOV        EDX,dword ptr [ESP + 0x34]
0054563e     53                       PUSH       EBX
0054563f     50                       PUSH       EAX
00545640     8B 44 24 1C              MOV        EAX,dword ptr [ESP + 0x1c]
00545644     50                       PUSH       EAX
00545645     51                       PUSH       ECX
00545646     52                       PUSH       EDX
00545647     56                       PUSH       ESI
00545648     E8 13 FB FF FF           CALL       0x00545160
0054564d     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
00545651     89 44 24 4C              MOV        dword ptr [ESP + 0x4c],EAX
00545655     83 C4 1C                 ADD        ESP,0x1c
00545658     8B 91 C0 00 00 00        MOV        EDX,dword ptr [ECX + 0xc0]
0054565e     8B 4C 24 14              MOV        ECX,dword ptr [ESP + 0x14]
00545662     52                       PUSH       EDX
00545663     8B 54 24 14              MOV        EDX,dword ptr [ESP + 0x14]
00545667     81 C1 00 04 00 00        ADD        ECX,0x400
0054566d     53                       PUSH       EBX
0054566e     51                       PUSH       ECX
0054566f     8B 4C 24 40              MOV        ECX,dword ptr [ESP + 0x40]
00545673     52                       PUSH       EDX
00545674     51                       PUSH       ECX
00545675     50                       PUSH       EAX
00545676     56                       PUSH       ESI
00545677     E8 E4 FA FF FF           CALL       0x00545160
0054567c     83 C4 1C                 ADD        ESP,0x1c
0054567f     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545683     EB 04                    JMP        0x00545689
00545685     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
00545689     8B 5D 30                 MOV        EBX,dword ptr [EBP + 0x30]
0054568c     F7 C3 00 00 00 01        TEST       EBX,0x1000000
00545692     0F 84 AC 00 00 00        JZ         0x00545744
00545698     F6 85 C7 08 00 00 20     TEST       byte ptr [EBP + 0x8c7],0x20
0054569f     0F 85 9F 00 00 00        JNZ        0x00545744
005456a5     66 8B 4F 02              MOV        CX,word ptr [EDI + 0x2]
005456a9     F6 C5 80                 TEST       CH,0x80
005456ac     74 25                    JZ         0x005456d3
005456ae     81 E1 FF 7F 00 00        AND        ECX,0x7fff
005456b4     66 89 4F 02              MOV        word ptr [EDI + 0x2],CX
005456b8     8A 85 C7 08 00 00        MOV        AL,byte ptr [EBP + 0x8c7]
005456be     24 FD                    AND        AL,0xfd
005456c0     C6 45 33 00              MOV        byte ptr [EBP + 0x33],0x0
005456c4     88 85 C7 08 00 00        MOV        byte ptr [EBP + 0x8c7],AL
005456ca     E8 B1 84 FF FF           CALL       0x0053db80
005456cf     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
005456d3     8B 4C 24 2C              MOV        ECX,dword ptr [ESP + 0x2c]
005456d7     33 D2                    XOR        EDX,EDX
005456d9     8A 74 24 1A              MOV        DH,byte ptr [ESP + 0x1a]
005456dd     8B 7D 34                 MOV        EDI,dword ptr [EBP + 0x34]
005456e0     80 E7 00                 AND        BH,0x0
005456e3     0B DA                    OR         EBX,EDX
005456e5     8B 54 24 1C              MOV        EDX,dword ptr [ESP + 0x1c]
005456e9     81 C3 0C 04 00 00        ADD        EBX,0x40c
005456ef     8B 8C D1 D8 00 00 00     MOV        ECX,dword ptr [ECX + EDX*0x8 + 0xd8]
005456f6     8B 54 24 10              MOV        EDX,dword ptr [ESP + 0x10]
005456fa     51                       PUSH       ECX
005456fb     8B 4C 24 38              MOV        ECX,dword ptr [ESP + 0x38]
005456ff     57                       PUSH       EDI
00545700     53                       PUSH       EBX
00545701     52                       PUSH       EDX
00545702     51                       PUSH       ECX
00545703     50                       PUSH       EAX
00545704     56                       PUSH       ESI
00545705     E8 56 FA FF FF           CALL       0x00545160
0054570a     8B 15 60 99 5E 00        MOV        EDX,dword ptr [0x005e9960]
00545710     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
00545714     D1 EA                    SHR        EDX,0x1
00545716     83 E2 03                 AND        EDX,0x3
00545719     83 C4 1C                 ADD        ESP,0x1c
0054571c     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545720     81 C3 F4 FB FF FF        ADD        EBX,0xfffffbf4
00545726     8B 14 D1                 MOV        EDX,dword ptr [ECX + EDX*0x8]
00545729     8B 4C 24 10              MOV        ECX,dword ptr [ESP + 0x10]
0054572d     52                       PUSH       EDX
0054572e     8B 54 24 38              MOV        EDX,dword ptr [ESP + 0x38]
00545732     57                       PUSH       EDI
00545733     53                       PUSH       EBX
00545734     51                       PUSH       ECX
00545735     52                       PUSH       EDX
00545736     50                       PUSH       EAX
00545737     56                       PUSH       ESI
00545738     E8 23 FA FF FF           CALL       0x00545160
0054573d     83 C4 1C                 ADD        ESP,0x1c
00545740     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545744     8B 4C 24 2C              MOV        ECX,dword ptr [ESP + 0x2c]
00545748     8D 5D 4C                 LEA        EBX,[EBP + 0x4c]
0054574b     C7 44 24 1C 04 00 00 00  MOV        dword ptr [ESP + 0x1c],0x4
00545753     8D 79 20                 LEA        EDI,[ECX + 0x20]
00545756     8B 4B FC                 MOV        ECX,dword ptr [EBX + -0x4]
00545759     F7 C1 00 00 00 01        TEST       ECX,0x1000000
0054575f     74 1F                    JZ         0x00545780
00545761     8B 17                    MOV        EDX,dword ptr [EDI]
00545763     52                       PUSH       EDX
00545764     8B 13                    MOV        EDX,dword ptr [EBX]
00545766     52                       PUSH       EDX
00545767     8B 54 24 3C              MOV        EDX,dword ptr [ESP + 0x3c]
0054576b     51                       PUSH       ECX
0054576c     8B 4C 24 1C              MOV        ECX,dword ptr [ESP + 0x1c]
00545770     51                       PUSH       ECX
00545771     52                       PUSH       EDX
00545772     50                       PUSH       EAX
00545773     56                       PUSH       ESI
00545774     E8 E7 F9 FF FF           CALL       0x00545160
00545779     83 C4 1C                 ADD        ESP,0x1c
0054577c     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545780     8B 4C 24 1C              MOV        ECX,dword ptr [ESP + 0x1c]
00545784     83 C7 08                 ADD        EDI,0x8
00545787     83 C3 08                 ADD        EBX,0x8
0054578a     49                       DEC        ECX
0054578b     89 4C 24 1C              MOV        dword ptr [ESP + 0x1c],ECX
0054578f     75 C5                    JNZ        0x00545756
00545791     8B 5D 40                 MOV        EBX,dword ptr [EBP + 0x40]
00545794     F7 C3 00 00 00 01        TEST       EBX,0x1000000
0054579a     0F 84 92 01 00 00        JZ         0x00545932
005457a0     8B 4C 24 2C              MOV        ECX,dword ptr [ESP + 0x2c]
005457a4     8B 7D 44                 MOV        EDI,dword ptr [EBP + 0x44]
005457a7     8B 51 60                 MOV        EDX,dword ptr [ECX + 0x60]
005457aa     8B 4C 24 10              MOV        ECX,dword ptr [ESP + 0x10]
005457ae     52                       PUSH       EDX
005457af     8B 54 24 38              MOV        EDX,dword ptr [ESP + 0x38]
005457b3     57                       PUSH       EDI
005457b4     53                       PUSH       EBX
005457b5     51                       PUSH       ECX
005457b6     52                       PUSH       EDX
005457b7     50                       PUSH       EAX
005457b8     56                       PUSH       ESI
005457b9     E8 A2 F9 FF FF           CALL       0x00545160
005457be     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
005457c2     8B 54 24 2C              MOV        EDX,dword ptr [ESP + 0x2c]
005457c6     81 E1 FF FF 00 00        AND        ECX,0xffff
005457cc     83 C4 1C                 ADD        ESP,0x1c
005457cf     89 4C 24 20              MOV        dword ptr [ESP + 0x20],ECX
005457d3     03 CA                    ADD        ECX,EDX
005457d5     8B 54 24 2C              MOV        EDX,dword ptr [ESP + 0x2c]
005457d9     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
005457dd     89 4C 24 14              MOV        dword ptr [ESP + 0x14],ECX
005457e1     8B 52 68                 MOV        EDX,dword ptr [EDX + 0x68]
005457e4     52                       PUSH       EDX
005457e5     57                       PUSH       EDI
005457e6     53                       PUSH       EBX
005457e7     51                       PUSH       ECX
005457e8     8B 4C 24 44              MOV        ECX,dword ptr [ESP + 0x44]
005457ec     51                       PUSH       ECX
005457ed     50                       PUSH       EAX
005457ee     56                       PUSH       ESI
005457ef     E8 6C F9 FF FF           CALL       0x00545160
005457f4     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
005457f8     8B 54 24 30              MOV        EDX,dword ptr [ESP + 0x30]
005457fc     81 E1 00 00 FF FF        AND        ECX,0xffff0000
00545802     83 C4 1C                 ADD        ESP,0x1c
00545805     89 4C 24 1C              MOV        dword ptr [ESP + 0x1c],ECX
00545809     03 D1                    ADD        EDX,ECX
0054580b     8B 4C 24 2C              MOV        ECX,dword ptr [ESP + 0x2c]
0054580f     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545813     89 54 24 14              MOV        dword ptr [ESP + 0x14],EDX
00545817     8B 49 78                 MOV        ECX,dword ptr [ECX + 0x78]
0054581a     51                       PUSH       ECX
0054581b     57                       PUSH       EDI
0054581c     53                       PUSH       EBX
0054581d     52                       PUSH       EDX
0054581e     8B 54 24 44              MOV        EDX,dword ptr [ESP + 0x44]
00545822     52                       PUSH       EDX
00545823     50                       PUSH       EAX
00545824     56                       PUSH       ESI
00545825     E8 36 F9 FF FF           CALL       0x00545160
0054582a     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
0054582e     83 C4 1C                 ADD        ESP,0x1c
00545831     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545835     8B 51 70                 MOV        EDX,dword ptr [ECX + 0x70]
00545838     8B 4C 24 14              MOV        ECX,dword ptr [ESP + 0x14]
0054583c     52                       PUSH       EDX
0054583d     8B 54 24 24              MOV        EDX,dword ptr [ESP + 0x24]
00545841     57                       PUSH       EDI
00545842     2B CA                    SUB        ECX,EDX
00545844     53                       PUSH       EBX
00545845     51                       PUSH       ECX
00545846     8B 4C 24 44              MOV        ECX,dword ptr [ESP + 0x44]
0054584a     51                       PUSH       ECX
0054584b     50                       PUSH       EAX
0054584c     56                       PUSH       ESI
0054584d     E8 0E F9 FF FF           CALL       0x00545160
00545852     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
00545856     8B 54 24 2C              MOV        EDX,dword ptr [ESP + 0x2c]
0054585a     83 C1 F0                 ADD        ECX,-0x10
0054585d     83 C4 1C                 ADD        ESP,0x1c
00545860     81 E1 FF FF 00 00        AND        ECX,0xffff
00545866     83 C2 10                 ADD        EDX,0x10
00545869     81 C9 00 00 10 00        OR         ECX,0x100000
0054586f     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545873     89 4C 24 24              MOV        dword ptr [ESP + 0x24],ECX
00545877     51                       PUSH       ECX
00545878     8B 4C 24 30              MOV        ECX,dword ptr [ESP + 0x30]
0054587c     89 54 24 18              MOV        dword ptr [ESP + 0x18],EDX
00545880     8B 49 40                 MOV        ECX,dword ptr [ECX + 0x40]
00545883     51                       PUSH       ECX
00545884     57                       PUSH       EDI
00545885     53                       PUSH       EBX
00545886     52                       PUSH       EDX
00545887     8B 54 24 48              MOV        EDX,dword ptr [ESP + 0x48]
0054588b     52                       PUSH       EDX
0054588c     50                       PUSH       EAX
0054588d     56                       PUSH       ESI
0054588e     E8 8D F9 FF FF           CALL       0x00545220
00545893     8B 4C 24 44              MOV        ECX,dword ptr [ESP + 0x44]
00545897     8B 54 24 4C              MOV        EDX,dword ptr [ESP + 0x4c]
0054589b     83 C4 20                 ADD        ESP,0x20
0054589e     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
005458a2     51                       PUSH       ECX
005458a3     8B 4A 48                 MOV        ECX,dword ptr [EDX + 0x48]
005458a6     8B 54 24 18              MOV        EDX,dword ptr [ESP + 0x18]
005458aa     51                       PUSH       ECX
005458ab     8B 4C 24 24              MOV        ECX,dword ptr [ESP + 0x24]
005458af     57                       PUSH       EDI
005458b0     03 D1                    ADD        EDX,ECX
005458b2     53                       PUSH       EBX
005458b3     52                       PUSH       EDX
005458b4     8B 54 24 48              MOV        EDX,dword ptr [ESP + 0x48]
005458b8     52                       PUSH       EDX
005458b9     50                       PUSH       EAX
005458ba     56                       PUSH       ESI
005458bb     E8 60 F9 FF FF           CALL       0x00545220
005458c0     8B 4C 24 30              MOV        ECX,dword ptr [ESP + 0x30]
005458c4     83 C4 20                 ADD        ESP,0x20
005458c7     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
005458cb     8D 91 00 00 10 00        LEA        EDX,[ECX + 0x100000]
005458d1     8B 4C 24 1C              MOV        ECX,dword ptr [ESP + 0x1c]
005458d5     81 C1 00 00 F0 FF        ADD        ECX,0xfff00000
005458db     89 54 24 14              MOV        dword ptr [ESP + 0x14],EDX
005458df     83 C9 10                 OR         ECX,0x10
005458e2     89 4C 24 24              MOV        dword ptr [ESP + 0x24],ECX
005458e6     51                       PUSH       ECX
005458e7     8B 4C 24 30              MOV        ECX,dword ptr [ESP + 0x30]
005458eb     8B 49 50                 MOV        ECX,dword ptr [ECX + 0x50]
005458ee     51                       PUSH       ECX
005458ef     57                       PUSH       EDI
005458f0     53                       PUSH       EBX
005458f1     52                       PUSH       EDX
005458f2     8B 54 24 48              MOV        EDX,dword ptr [ESP + 0x48]
005458f6     52                       PUSH       EDX
005458f7     50                       PUSH       EAX
005458f8     56                       PUSH       ESI
005458f9     E8 22 F9 FF FF           CALL       0x00545220
005458fe     8B 4C 24 34              MOV        ECX,dword ptr [ESP + 0x34]
00545902     8B 54 24 40              MOV        EDX,dword ptr [ESP + 0x40]
00545906     83 C4 20                 ADD        ESP,0x20
00545909     03 CA                    ADD        ECX,EDX
0054590b     8B 54 24 24              MOV        EDX,dword ptr [ESP + 0x24]
0054590f     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545913     52                       PUSH       EDX
00545914     8B 54 24 30              MOV        EDX,dword ptr [ESP + 0x30]
00545918     8B 52 58                 MOV        EDX,dword ptr [EDX + 0x58]
0054591b     52                       PUSH       EDX
0054591c     57                       PUSH       EDI
0054591d     53                       PUSH       EBX
0054591e     51                       PUSH       ECX
0054591f     8B 4C 24 48              MOV        ECX,dword ptr [ESP + 0x48]
00545923     51                       PUSH       ECX
00545924     50                       PUSH       EAX
00545925     56                       PUSH       ESI
00545926     E8 F5 F8 FF FF           CALL       0x00545220
0054592b     83 C4 20                 ADD        ESP,0x20
0054592e     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
00545932     F7 45 20 00 00 00 01     TEST       dword ptr [EBP + 0x20],0x1000000
00545939     74 7C                    JZ         0x005459b7
0054593b     8B 8D A4 00 00 00        MOV        ECX,dword ptr [EBP + 0xa4]
00545941     81 E1 FF FF FF 00        AND        ECX,0xffffff
00545947     81 F9 FF FF FF 00        CMP        ECX,0xffffff
0054594d     74 68                    JZ         0x005459b7
0054594f     8B 54 24 10              MOV        EDX,dword ptr [ESP + 0x10]
00545953     F7 01 00 00 00 FF        TEST       dword ptr [ECX],0xff000000
00545959     74 4A                    JZ         0x005459a5
0054595b     8B 79 04                 MOV        EDI,dword ptr [ECX + 0x4]
0054595e     89 78 04                 MOV        dword ptr [EAX + 0x4],EDI
00545961     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
00545965     8B 41 08                 MOV        EAX,dword ptr [ECX + 0x8]
00545968     89 47 08                 MOV        dword ptr [EDI + 0x8],EAX
0054596b     8B 41 0C                 MOV        EAX,dword ptr [ECX + 0xc]
0054596e     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
00545972     03 C2                    ADD        EAX,EDX
00545974     89 47 0C                 MOV        dword ptr [EDI + 0xc],EAX
00545977     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
0054597b     8B 41 10                 MOV        EAX,dword ptr [ECX + 0x10]
0054597e     89 47 10                 MOV        dword ptr [EDI + 0x10],EAX
00545981     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
00545985     8B 41 14                 MOV        EAX,dword ptr [ECX + 0x14]
00545988     89 47 14                 MOV        dword ptr [EDI + 0x14],EAX
0054598b     8B 06                    MOV        EAX,dword ptr [ESI]
0054598d     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
00545991     0D 00 00 00 05           OR         EAX,0x5000000
00545996     89 07                    MOV        dword ptr [EDI],EAX
00545998     8B 44 24 30              MOV        EAX,dword ptr [ESP + 0x30]
0054599c     89 06                    MOV        dword ptr [ESI],EAX
0054599e     83 C0 18                 ADD        EAX,0x18
005459a1     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
005459a5     8B 09                    MOV        ECX,dword ptr [ECX]
005459a7     81 E1 FF FF FF 00        AND        ECX,0xffffff
005459ad     81 F9 FF FF FF 00        CMP        ECX,0xffffff
005459b3     75 9E                    JNZ        0x00545953
005459b5     EB 04                    JMP        0x005459bb
005459b7     8B 54 24 10              MOV        EDX,dword ptr [ESP + 0x10]
005459bb     8B 4D 18                 MOV        ECX,dword ptr [EBP + 0x18]
005459be     F7 C1 00 00 00 01        TEST       ECX,0x1000000
005459c4     74 29                    JZ         0x005459ef
005459c6     8B 7C 24 18              MOV        EDI,dword ptr [ESP + 0x18]
005459ca     57                       PUSH       EDI
005459cb     8B 7C 24 30              MOV        EDI,dword ptr [ESP + 0x30]
005459cf     8B BF 80 00 00 00        MOV        EDI,dword ptr [EDI + 0x80]
005459d5     57                       PUSH       EDI
005459d6     8B 7D 1C                 MOV        EDI,dword ptr [EBP + 0x1c]
005459d9     57                       PUSH       EDI
005459da     51                       PUSH       ECX
005459db     8B 4C 24 44              MOV        ECX,dword ptr [ESP + 0x44]
005459df     52                       PUSH       EDX
005459e0     51                       PUSH       ECX
005459e1     50                       PUSH       EAX
005459e2     56                       PUSH       ESI
005459e3     E8 38 F8 FF FF           CALL       0x00545220
005459e8     83 C4 20                 ADD        ESP,0x20
005459eb     89 44 24 30              MOV        dword ptr [ESP + 0x30],EAX
005459ef     8D 54 24 30              LEA        EDX,[ESP + 0x30]
005459f3     52                       PUSH       EDX
005459f4     56                       PUSH       ESI
005459f5     E8 B6 63 FD FF           CALL       0x0051bdb0
005459fa     8B 44 24 38              MOV        EAX,dword ptr [ESP + 0x38]
005459fe     83 C4 08                 ADD        ESP,0x8
00545a01     5F                       POP        EDI
00545a02     5E                       POP        ESI
00545a03     5D                       POP        EBP
00545a04     5B                       POP        EBX
00545a05     83 C4 18                 ADD        ESP,0x18
00545a08     C3                       RET        

