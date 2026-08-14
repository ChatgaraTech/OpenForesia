; Function: FUN_00512520
; Entry:    00512520
; Body:     [[00512520, 005126bf]]

; --------------------------------------------------
; Range: 00512520 - 005126bf
; --------------------------------------------------
00512520     51                       PUSH       ECX
00512521     8B 44 24 08              MOV        EAX,dword ptr [ESP + 0x8]
00512525     56                       PUSH       ESI
00512526     8B 35 DC 85 60 00        MOV        ESI,dword ptr [0x006085dc]
0051252c     A3 70 92 57 00           MOV        [0x00579270],EAX
00512531     A1 54 E5 5D 00           MOV        EAX,[0x005de554]
00512536     57                       PUSH       EDI
00512537     85 C0                    TEST       EAX,EAX
00512539     75 7E                    JNZ        0x005125b9
0051253b     6A 72                    PUSH       0x72
0051253d     FF D6                    CALL       ESI
0051253f     66 85 C0                 TEST       AX,AX
00512542     7D 75                    JGE        0x005125b9
00512544     8B 35 E0 9D 5D 00        MOV        ESI,dword ptr [0x005d9de0]
0051254a     E8 BD 35 03 00           CALL       0x00545b0c
0051254f     8D 4C 24 10              LEA        ECX,[ESP + 0x10]
00512553     8D 54 24 08              LEA        EDX,[ESP + 0x8]
00512557     51                       PUSH       ECX
00512558     52                       PUSH       EDX
00512559     E8 BA 35 03 00           CALL       0x00545b18
0051255e     6A 00                    PUSH       0x0
00512560     6A 00                    PUSH       0x0
00512562     E8 AB 35 03 00           CALL       0x00545b12
00512567     83 3D 60 E1 5D 00 07     CMP        dword ptr [0x005de160],0x7
0051256e     7D 0A                    JGE        0x0051257a
00512570     C7 05 38 4A 56 00 01 00 00 00 MOV        dword ptr [0x00564a38],0x1
0051257a     E8 A1 FD FF FF           CALL       0x00512320
0051257f     8B 44 24 10              MOV        EAX,dword ptr [ESP + 0x10]
00512583     8B 4C 24 08              MOV        ECX,dword ptr [ESP + 0x8]
00512587     50                       PUSH       EAX
00512588     51                       PUSH       ECX
00512589     E8 84 35 03 00           CALL       0x00545b12
0051258e     E8 6D CD FF FF           CALL       0x0050f300
00512593     3B 35 E0 9D 5D 00        CMP        ESI,dword ptr [0x005d9de0]
00512599     74 15                    JZ         0x005125b0
0051259b     DB 05 E0 9D 5D 00        FILD       dword ptr [0x005d9de0]
005125a1     51                       PUSH       ECX
005125a2     D8 0D 00 D0 54 00        FMUL       float ptr [0x0054d000]
005125a8     D9 1C 24                 FSTP       float ptr [ESP]
005125ab     E8 32 35 03 00           CALL       0x00545ae2
005125b0     B8 01 00 00 00           MOV        EAX,0x1
005125b5     5F                       POP        EDI
005125b6     5E                       POP        ESI
005125b7     59                       POP        ECX
005125b8     C3                       RET        
005125b9     6A 7A                    PUSH       0x7a
005125bb     FF D6                    CALL       ESI
005125bd     66 85 C0                 TEST       AX,AX
005125c0     74 60                    JZ         0x00512622
005125c2     8B 0D 60 E1 5D 00        MOV        ECX,dword ptr [0x005de160]
005125c8     85 C9                    TEST       ECX,ECX
005125ca     0F 84 EA 00 00 00        JZ         0x005126ba
005125d0     49                       DEC        ECX
005125d1     B8 0B 00 00 00           MOV        EAX,0xb
005125d6     2B C1                    SUB        EAX,ECX
005125d8     6A 00                    PUSH       0x0
005125da     51                       PUSH       ECX
005125db     89 0D 60 E1 5D 00        MOV        dword ptr [0x005de160],ECX
005125e1     8D 14 40                 LEA        EDX,[EAX + EAX*0x2]
005125e4     C1 E2 02                 SHL        EDX,0x2
005125e7     C1 E0 04                 SHL        EAX,0x4
005125ea     52                       PUSH       EDX
005125eb     50                       PUSH       EAX
005125ec     E8 0F 35 03 00           CALL       0x00545b00
005125f1     83 3D 60 E1 5D 00 07     CMP        dword ptr [0x005de160],0x7
005125f8     7D 0A                    JGE        0x00512604
005125fa     C7 05 38 4A 56 00 01 00 00 00 MOV        dword ptr [0x00564a38],0x1
00512604     E8 F7 CC FF FF           CALL       0x0050f300
00512609     8B 3D 90 85 60 00        MOV        EDI,dword ptr [0x00608590]
0051260f     6A 64                    PUSH       0x64
00512611     FF D7                    CALL       EDI
00512613     6A 7A                    PUSH       0x7a
00512615     FF D6                    CALL       ESI
00512617     66 85 C0                 TEST       AX,AX
0051261a     7C F3                    JL         0x0051260f
0051261c     33 C0                    XOR        EAX,EAX
0051261e     5F                       POP        EDI
0051261f     5E                       POP        ESI
00512620     59                       POP        ECX
00512621     C3                       RET        
00512622     6A 7B                    PUSH       0x7b
00512624     FF D6                    CALL       ESI
00512626     66 85 C0                 TEST       AX,AX
00512629     74 53                    JZ         0x0051267e
0051262b     A1 C4 6C 56 00           MOV        EAX,[0x00566cc4]
00512630     8B 0D 60 E1 5D 00        MOV        ECX,dword ptr [0x005de160]
00512636     3B 0C 85 00 2E 56 00     CMP        ECX,dword ptr [EAX*0x4 + 0x562e00]
0051263d     7D 7B                    JGE        0x005126ba
0051263f     41                       INC        ECX
00512640     B8 0B 00 00 00           MOV        EAX,0xb
00512645     2B C1                    SUB        EAX,ECX
00512647     6A 00                    PUSH       0x0
00512649     89 0D 60 E1 5D 00        MOV        dword ptr [0x005de160],ECX
0051264f     51                       PUSH       ECX
00512650     8D 0C 40                 LEA        ECX,[EAX + EAX*0x2]
00512653     C1 E1 02                 SHL        ECX,0x2
00512656     C1 E0 04                 SHL        EAX,0x4
00512659     51                       PUSH       ECX
0051265a     50                       PUSH       EAX
0051265b     E8 A0 34 03 00           CALL       0x00545b00
00512660     E8 9B CC FF FF           CALL       0x0050f300
00512665     8B 3D 90 85 60 00        MOV        EDI,dword ptr [0x00608590]
0051266b     6A 64                    PUSH       0x64
0051266d     FF D7                    CALL       EDI
0051266f     6A 7B                    PUSH       0x7b
00512671     FF D6                    CALL       ESI
00512673     66 85 C0                 TEST       AX,AX
00512676     7C F3                    JL         0x0051266b
00512678     33 C0                    XOR        EAX,EAX
0051267a     5F                       POP        EDI
0051267b     5E                       POP        ESI
0051267c     59                       POP        ECX
0051267d     C3                       RET        
0051267e     6A 78                    PUSH       0x78
00512680     FF D6                    CALL       ESI
00512682     66 85 C0                 TEST       AX,AX
00512685     74 33                    JZ         0x005126ba
00512687     83 3D 60 E1 5D 00 07     CMP        dword ptr [0x005de160],0x7
0051268e     7C 2A                    JL         0x005126ba
00512690     A1 38 4A 56 00           MOV        EAX,[0x00564a38]
00512695     33 D2                    XOR        EDX,EDX
00512697     85 C0                    TEST       EAX,EAX
00512699     0F 94 C2                 SETZ       DL
0051269c     89 15 38 4A 56 00        MOV        dword ptr [0x00564a38],EDX
005126a2     E8 59 CC FF FF           CALL       0x0050f300
005126a7     8B 3D 90 85 60 00        MOV        EDI,dword ptr [0x00608590]
005126ad     6A 64                    PUSH       0x64
005126af     FF D7                    CALL       EDI
005126b1     6A 78                    PUSH       0x78
005126b3     FF D6                    CALL       ESI
005126b5     66 85 C0                 TEST       AX,AX
005126b8     7C F3                    JL         0x005126ad
005126ba     5F                       POP        EDI
005126bb     33 C0                    XOR        EAX,EAX
005126bd     5E                       POP        ESI
005126be     59                       POP        ECX
005126bf     C3                       RET        

