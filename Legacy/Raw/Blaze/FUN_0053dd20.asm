; Function: FUN_0053dd20
; Entry:    0053dd20

0053dd20     53                           PUSH EBX
0053dd21     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
0053dd27     55                           PUSH EBP
0053dd28     56                           PUSH ESI
0053dd29     57                           PUSH EDI
0053dd2a     8B 7C 24 14                  MOV EDI,dword ptr [ESP + 0x14]
0053dd2e     8B 11                        MOV EDX,dword ptr [ECX]
0053dd30     33 C0                        XOR EAX,EAX
0053dd32     8A 47 08                     MOV AL,byte ptr [EDI + 0x8]
0053dd35     83 C0 78                     ADD EAX,0x78
0053dd38     C1 E0 0D                     SHL EAX,0xd
0053dd3b     03 C2                        ADD EAX,EDX
0053dd3d     33 DB                        XOR EBX,EBX
0053dd3f     8D A8 00 03 00 00            LEA EBP,[EAX + 0x300]
0053dd45     8B F5                        MOV ESI,EBP
0053dd47     80 3E 00                     CMP byte ptr [ESI],0x0
0053dd4a     74 0B                        JZ 0x0053dd57
0053dd4c     B8 00 80 00 00               MOV EAX,0x8000
0053dd51     66 85 46 12                  TEST word ptr [ESI + 0x12],AX
0053dd55     74 1D                        JZ 0x0053dd74
0053dd57     43                           INC EBX
0053dd58     81 C6 80 02 00 00            ADD ESI,0x280
0053dd5e     83 FB 0A                     CMP EBX,0xa
0053dd61     7C E4                        JL 0x0053dd47
0053dd63     C6 05 9F 90 5E 00 00         MOV byte ptr [0x005e909f],0x0
0053dd6a     B8 01 00 00 00               MOV EAX,0x1
0053dd6f     5F                           POP EDI
0053dd70     5E                           POP ESI
0053dd71     5D                           POP EBP
0053dd72     5B                           POP EBX
0053dd73     C3                           RET
0053dd74     66 09 46 12                  OR word ptr [ESI + 0x12],AX
0053dd78     56                           PUSH ESI
0053dd79     E8 B2 DF FD FF               CALL 0x0051bd30
0053dd7e     8B 15 CC 97 5E 00            MOV EDX,dword ptr [0x005e97cc]
0053dd84     83 C4 04                     ADD ESP,0x4
0053dd87     8B 4A 70                     MOV ECX,dword ptr [EDX + 0x70]
0053dd8a     8B 51 04                     MOV EDX,dword ptr [ECX + 0x4]
0053dd8d     8A 08                        MOV CL,byte ptr [EAX]
0053dd8f     84 C9                        TEST CL,CL
0053dd91     74 0A                        JZ 0x0053dd9d
0053dd93     40                           INC EAX
0053dd94     88 0A                        MOV byte ptr [EDX],CL
0053dd96     42                           INC EDX
0053dd97     8A 08                        MOV CL,byte ptr [EAX]
0053dd99     84 C9                        TEST CL,CL
0053dd9b     75 F6                        JNZ 0x0053dd93
0053dd9d     C6 02 00                     MOV byte ptr [EDX],0x0
0053dda0     8B 15 A0 8F 5E 00            MOV EDX,dword ptr [0x005e8fa0]
0053dda6     89 15 FC 8F 5E 00            MOV dword ptr [0x005e8ffc],EDX
0053ddac     BA 01 00 00 00               MOV EDX,0x1
0053ddb1     66 0F B6 47 08               MOVZX AX,byte ptr [EDI + 0x8]
0053ddb6     C1 E0 06                     SHL EAX,0x6
0053ddb9     66 03 05 A2 8F 5E 00         ADD AX,word ptr [0x005e8fa2]
0053ddc0     66 A3 FE 8F 5E 00            MOV [0x005e8ffe],AX
0053ddc6     8B 46 14                     MOV EAX,dword ptr [ESI + 0x14]
0053ddc9     F6 C4 04                     TEST AH,0x4
0053ddcc     74 0D                        JZ 0x0053dddb
0053ddce     3D 00 04 00 04               CMP EAX,0x4000400
0053ddd3     88 15 9A 90 5E 00            MOV byte ptr [0x005e909a],DL
0053ddd9     75 07                        JNZ 0x0053dde2
0053dddb     C6 05 9A 90 5E 00 00         MOV byte ptr [0x005e909a],0x0
0053dde2     66 8B 8E 88 00 00 00         MOV CX,word ptr [ESI + 0x88]
0053dde9     33 C0                        XOR EAX,EAX
0053ddeb     66 81 F9 5E 01               CMP CX,0x15e
0053ddf0     76 02                        JBE 0x0053ddf4
0053ddf2     8B C2                        MOV EAX,EDX
0053ddf4     66 81 F9 E8 03               CMP CX,0x3e8
0053ddf9     76 01                        JBE 0x0053ddfc
0053ddfb     40                           INC EAX
0053ddfc     66 81 F9 88 13               CMP CX,0x1388
0053de01     76 01                        JBE 0x0053de04
0053de03     40                           INC EAX
0053de04     8B 0D CC 97 5E 00            MOV ECX,dword ptr [0x005e97cc]
0053de0a     A2 9B 90 5E 00               MOV [0x005e909b],AL
0053de0f     C6 05 9C 90 5E 00 03         MOV byte ptr [0x005e909c],0x3
0053de16     88 15 9D 90 5E 00            MOV byte ptr [0x005e909d],DL
0053de1c     C6 05 9E 90 5E 00 00         MOV byte ptr [0x005e909e],0x0
0053de23     8B 91 94 00 00 00            MOV EDX,dword ptr [ECX + 0x94]
0053de29     8A C3                        MOV AL,BL
0053de2b     8B 4A 04                     MOV ECX,dword ptr [EDX + 0x4]
0053de2e     B2 18                        MOV DL,0x18
0053de30     F6 EA                        IMUL DL
0053de32     88 01                        MOV byte ptr [ECX],AL
0053de34     8A 47 08                     MOV AL,byte ptr [EDI + 0x8]
0053de37     F6 EA                        IMUL DL
0053de39     88 41 01                     MOV byte ptr [ECX + 0x1],AL
0053de3c     88 59 07                     MOV byte ptr [ECX + 0x7],BL
0053de3f     8B CD                        MOV ECX,EBP
0053de41     C6 05 9F 90 5E 00 01         MOV byte ptr [0x005e909f],0x1
0053de48     2B F1                        SUB ESI,ECX
0053de4a     BA 0A 00 00 00               MOV EDX,0xa
0053de4f     8B EE                        MOV EBP,ESI
0053de51     80 39 00                     CMP byte ptr [ECX],0x0
0053de54     74 2E                        JZ 0x0053de84
0053de56     66 8B 71 12                  MOV SI,word ptr [ECX + 0x12]
0053de5a     F7 C6 00 80 00 00            TEST ESI,0x8000
0053de60     75 22                        JNZ 0x0053de84
0053de62     33 FF                        XOR EDI,EDI
0053de64     8B C1                        MOV EAX,ECX
0053de66     8A 18                        MOV BL,byte ptr [EAX]
0053de68     3A 5C 05 00                  CMP BL,byte ptr [EBP + EAX*0x1]
0053de6c     75 07                        JNZ 0x0053de75
0053de6e     47                           INC EDI
0053de6f     40                           INC EAX
0053de70     83 FF 10                     CMP EDI,0x10
0053de73     7C F1                        JL 0x0053de66
0053de75     83 FF 10                     CMP EDI,0x10
0053de78     75 0A                        JNZ 0x0053de84
0053de7a     81 CE 00 80 00 00            OR ESI,0x8000
0053de80     66 89 71 12                  MOV word ptr [ECX + 0x12],SI
0053de84     81 C1 80 02 00 00            ADD ECX,0x280
0053de8a     81 ED 80 02 00 00            SUB EBP,0x280
0053de90     4A                           DEC EDX
0053de91     75 BE                        JNZ 0x0053de51
0053de93     33 C0                        XOR EAX,EAX
0053de95     5F                           POP EDI
0053de96     5E                           POP ESI
0053de97     5D                           POP EBP
0053de98     5B                           POP EBX
0053de99     C3                           RET
