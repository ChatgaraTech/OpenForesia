; Function: FUN_00544150
; Entry:    00544150
; Body:     [[00544150, 0054434e]]

; --------------------------------------------------
; Range: 00544150 - 0054434e
; --------------------------------------------------
00544150     A1 D0 FF 5D 00           MOV        EAX,[0x005dffd0]
00544155     53                       PUSH       EBX
00544156     BB 01 00 00 00           MOV        EBX,0x1
0054415b     3B C3                    CMP        EAX,EBX
0054415d     0F 84 EA 01 00 00        JZ         0x0054434d
00544163     8B 4C 24 0C              MOV        ECX,dword ptr [ESP + 0xc]
00544167     57                       PUSH       EDI
00544168     81 E1 FF FF 00 00        AND        ECX,0xffff
0054416e     56                       PUSH       ESI
0054416f     8B C1                    MOV        EAX,ECX
00544171     8B D1                    MOV        EDX,ECX
00544173     C1 E8 08                 SHR        EAX,0x8
00544176     C1 E2 08                 SHL        EDX,0x8
00544179     0B C2                    OR         EAX,EDX
0054417b     8B 15 BC E9 56 00        MOV        EDX,dword ptr [0x0056e9bc]
00544181     81 E2 FF FF 00 00        AND        EDX,0xffff
00544187     55                       PUSH       EBP
00544188     3B C2                    CMP        EAX,EDX
0054418a     C7 05 D0 FF 5D 00 02 00 00 00 MOV        dword ptr [0x005dffd0],0x2
00544194     74 24                    JZ         0x005441ba
00544196     8B 15 B8 E9 56 00        MOV        EDX,dword ptr [0x0056e9b8]
0054419c     81 E2 FF FF 00 00        AND        EDX,0xffff
005441a2     3B C2                    CMP        EAX,EDX
005441a4     74 14                    JZ         0x005441ba
005441a6     8B 15 B4 E9 56 00        MOV        EDX,dword ptr [0x0056e9b4]
005441ac     81 E2 FF FF 00 00        AND        EDX,0xffff
005441b2     3B C2                    CMP        EAX,EDX
005441b4     74 04                    JZ         0x005441ba
005441b6     33 ED                    XOR        EBP,EBP
005441b8     EB 02                    JMP        0x005441bc
005441ba     8B EB                    MOV        EBP,EBX
005441bc     51                       PUSH       ECX
005441bd     E8 A0 1A 00 00           CALL       0x00545c62
005441c2     8B F8                    MOV        EDI,EAX
005441c4     BA 0F 00 00 00           MOV        EDX,0xf
005441c9     BE D8 FF 5D 00           MOV        ESI,0x5dffd8
005441ce     66 8B 07                 MOV        AX,word ptr [EDI]
005441d1     33 C9                    XOR        ECX,ECX
005441d3     89 44 24 18              MOV        dword ptr [ESP + 0x18],EAX
005441d7     8A 4C 24 19              MOV        CL,byte ptr [ESP + 0x19]
005441db     8A 6C 24 18              MOV        CH,byte ptr [ESP + 0x18]
005441df     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
005441e3     66 8B 4C 24 18           MOV        CX,word ptr [ESP + 0x18]
005441e8     85 ED                    TEST       EBP,EBP
005441ea     74 08                    JZ         0x005441f4
005441ec     66 D1 E9                 SHR        CX,0x1
005441ef     66 89 4C 24 18           MOV        word ptr [ESP + 0x18],CX
005441f4     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
005441f8     23 C3                    AND        EAX,EBX
005441fa     66 D1 E9                 SHR        CX,0x1
005441fd     0B C8                    OR         ECX,EAX
005441ff     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
00544203     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
00544207     D1 E9                    SHR        ECX,0x1
00544209     81 E1 FE 7F 00 00        AND        ECX,0x7ffe
0054420f     83 E0 03                 AND        EAX,0x3
00544212     0B C8                    OR         ECX,EAX
00544214     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
00544218     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
0054421c     D1 E9                    SHR        ECX,0x1
0054421e     81 E1 FC 7F 00 00        AND        ECX,0x7ffc
00544224     83 E0 07                 AND        EAX,0x7
00544227     0B C8                    OR         ECX,EAX
00544229     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
0054422d     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
00544231     D1 E9                    SHR        ECX,0x1
00544233     81 E1 F8 7F 00 00        AND        ECX,0x7ff8
00544239     83 E0 0F                 AND        EAX,0xf
0054423c     0B C8                    OR         ECX,EAX
0054423e     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
00544242     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
00544246     D1 E9                    SHR        ECX,0x1
00544248     81 E1 F0 7F 00 00        AND        ECX,0x7ff0
0054424e     83 E0 1F                 AND        EAX,0x1f
00544251     0B C8                    OR         ECX,EAX
00544253     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
00544257     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
0054425b     D1 E9                    SHR        ECX,0x1
0054425d     81 E1 E0 7F 00 00        AND        ECX,0x7fe0
00544263     83 E0 3F                 AND        EAX,0x3f
00544266     0B C8                    OR         ECX,EAX
00544268     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
0054426c     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
00544270     D1 E9                    SHR        ECX,0x1
00544272     81 E1 C0 7F 00 00        AND        ECX,0x7fc0
00544278     83 E0 7F                 AND        EAX,0x7f
0054427b     0B C8                    OR         ECX,EAX
0054427d     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
00544281     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
00544285     D1 E9                    SHR        ECX,0x1
00544287     81 E1 80 7F 00 00        AND        ECX,0x7f80
0054428d     25 FF 00 00 00           AND        EAX,0xff
00544292     0B C8                    OR         ECX,EAX
00544294     83 FA 04                 CMP        EDX,0x4
00544297     89 4C 24 18              MOV        dword ptr [ESP + 0x18],ECX
0054429b     74 10                    JZ         0x005442ad
0054429d     83 FA 08                 CMP        EDX,0x8
005442a0     74 0B                    JZ         0x005442ad
005442a2     83 FA 0C                 CMP        EDX,0xc
005442a5     74 06                    JZ         0x005442ad
005442a7     8A 4C 24 18              MOV        CL,byte ptr [ESP + 0x18]
005442ab     EB 0A                    JMP        0x005442b7
005442ad     8A 44 24 18              MOV        AL,byte ptr [ESP + 0x18]
005442b1     8A 4E FF                 MOV        CL,byte ptr [ESI + -0x1]
005442b4     4E                       DEC        ESI
005442b5     0A C8                    OR         CL,AL
005442b7     88 0E                    MOV        byte ptr [ESI],CL
005442b9     4A                       DEC        EDX
005442ba     46                       INC        ESI
005442bb     83 C7 02                 ADD        EDI,0x2
005442be     85 D2                    TEST       EDX,EDX
005442c0     0F 85 08 FF FF FF        JNZ        0x005441ce
005442c6     8B 4C 24 18              MOV        ECX,dword ptr [ESP + 0x18]
005442ca     B8 0F 00 00 00           MOV        EAX,0xf
005442cf     BA D8 FF 5D 00           MOV        EDX,0x5dffd8
005442d4     66 0F B6 32              MOVZX      SI,byte ptr [EDX]
005442d8     89 74 24 18              MOV        dword ptr [ESP + 0x18],ESI
005442dc     BE 08 00 00 00           MOV        ESI,0x8
005442e1     C1 E1 04                 SHL        ECX,0x4
005442e4     84 5C 24 18              TEST       byte ptr [ESP + 0x18],BL
005442e8     74 02                    JZ         0x005442ec
005442ea     0B C8                    OR         ECX,EAX
005442ec     66 D1 6C 24 18           SHR        word ptr [ESP + 0x18],0x1
005442f1     4E                       DEC        ESI
005442f2     75 ED                    JNZ        0x005442e1
005442f4     8B 3D 04 83 5E 00        MOV        EDI,dword ptr [0x005e8304]
005442fa     8B F1                    MOV        ESI,ECX
005442fc     F7 D6                    NOT        ESI
005442fe     89 37                    MOV        dword ptr [EDI],ESI
00544300     8B 3D 04 83 5E 00        MOV        EDI,dword ptr [0x005e8304]
00544306     48                       DEC        EAX
00544307     42                       INC        EDX
00544308     81 C7 80 00 00 00        ADD        EDI,0x80
0054430e     85 C0                    TEST       EAX,EAX
00544310     89 3D 04 83 5E 00        MOV        dword ptr [0x005e8304],EDI
00544316     75 BC                    JNZ        0x005442d4
00544318     8B 44 24 14              MOV        EAX,dword ptr [ESP + 0x14]
0054431c     8B EF                    MOV        EBP,EDI
0054431e     81 ED 7C 07 00 00        SUB        EBP,0x77c
00544324     89 2D 04 83 5E 00        MOV        dword ptr [0x005e8304],EBP
0054432a     8A 58 0E                 MOV        BL,byte ptr [EAX + 0xe]
0054432d     80 C3 08                 ADD        BL,0x8
00544330     5D                       POP        EBP
00544331     88 58 0E                 MOV        byte ptr [EAX + 0xe],BL
00544334     8B 15 20 83 5E 00        MOV        EDX,dword ptr [0x005e8320]
0054433a     83 C2 08                 ADD        EDX,0x8
0054433d     5E                       POP        ESI
0054433e     89 15 20 83 5E 00        MOV        dword ptr [0x005e8320],EDX
00544344     8A 48 10                 MOV        CL,byte ptr [EAX + 0x10]
00544347     FE C1                    INC        CL
00544349     5F                       POP        EDI
0054434a     88 48 10                 MOV        byte ptr [EAX + 0x10],CL
0054434d     5B                       POP        EBX
0054434e     C3                       RET        

