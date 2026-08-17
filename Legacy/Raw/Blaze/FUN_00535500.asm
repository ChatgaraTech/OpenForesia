; Function: FUN_00535500
; Entry:    00535500

00535500     83 EC 0C                     SUB ESP,0xc
00535503     A1 B4 87 60 00               MOV EAX,[0x006087b4]
00535508     53                           PUSH EBX
00535509     55                           PUSH EBP
0053550a     56                           PUSH ESI
0053550b     8B 00                        MOV EAX,dword ptr [EAX]
0053550d     57                           PUSH EDI
0053550e     05 00 00 0F 00               ADD EAX,0xf0000
00535513     33 FF                        XOR EDI,EDI
00535515     8D B0 40 04 00 00            LEA ESI,[EAX + 0x440]
0053551b     89 74 24 10                  MOV dword ptr [ESP + 0x10],ESI
0053551f     33 ED                        XOR EBP,EBP
00535521     33 DB                        XOR EBX,EBX
00535523     8A 86 C0 FE FF FF            MOV AL,byte ptr [ESI + 0xfffffec0]
00535529     84 C0                        TEST AL,AL
0053552b     74 77                        JZ 0x005355a4
0053552d     8B CD                        MOV ECX,EBP
0053552f     8D 97 F1 01 00 00            LEA EDX,[EDI + 0x1f1]
00535535     C1 E1 04                     SHL ECX,0x4
00535538     6A 00                        PUSH 0x0
0053553a     66 89 4C 24 18               MOV word ptr [ESP + 0x18],CX
0053553f     66 89 54 24 1A               MOV word ptr [ESP + 0x1a],DX
00535544     66 C7 44 24 1C 10 00         MOV word ptr [ESP + 0x1c],0x10
0053554b     66 C7 44 24 1E 01 00         MOV word ptr [ESP + 0x1e],0x1
00535552     E8 6D 05 01 00               CALL 0x00545ac4
00535557     8D 44 24 14                  LEA EAX,[ESP + 0x14]
0053555b     56                           PUSH ESI
0053555c     50                           PUSH EAX
0053555d     E8 BC 05 01 00               CALL 0x00545b1e
00535562     8B CB                        MOV ECX,EBX
00535564     8D 14 7F                     LEA EDX,[EDI + EDI*0x2]
00535567     C1 F9 02                     SAR ECX,0x2
0053556a     81 C1 80 01 00 00            ADD ECX,0x180
00535570     8D 04 D5 00 01 00 00         LEA EAX,[EDX*0x8 + 0x100]
00535577     6A 00                        PUSH 0x0
00535579     66 89 4C 24 18               MOV word ptr [ESP + 0x18],CX
0053557e     66 89 44 24 1A               MOV word ptr [ESP + 0x1a],AX
00535583     66 C7 44 24 1C 06 00         MOV word ptr [ESP + 0x1c],0x6
0053558a     66 C7 44 24 1E 18 00         MOV word ptr [ESP + 0x1e],0x18
00535591     E8 2E 05 01 00               CALL 0x00545ac4
00535596     8D 4E 20                     LEA ECX,[ESI + 0x20]
00535599     8D 54 24 14                  LEA EDX,[ESP + 0x14]
0053559d     51                           PUSH ECX
0053559e     52                           PUSH EDX
0053559f     E8 7A 05 01 00               CALL 0x00545b1e
005355a4     83 C3 18                     ADD EBX,0x18
005355a7     45                           INC EBP
005355a8     81 C6 80 02 00 00            ADD ESI,0x280
005355ae     81 FB F0 00 00 00            CMP EBX,0xf0
005355b4     0F 8C 69 FF FF FF            JL 0x00535523
005355ba     8B 74 24 10                  MOV ESI,dword ptr [ESP + 0x10]
005355be     47                           INC EDI
005355bf     81 C6 00 20 00 00            ADD ESI,0x2000
005355c5     83 FF 04                     CMP EDI,0x4
005355c8     89 74 24 10                  MOV dword ptr [ESP + 0x10],ESI
005355cc     0F 8C 4D FF FF FF            JL 0x0053551f
005355d2     5F                           POP EDI
005355d3     5E                           POP ESI
005355d4     5D                           POP EBP
005355d5     66 C7 05 A0 8F 5E 00 16 02   MOV word ptr [0x005e8fa0],0x216
005355de     66 C7 05 A2 8F 5E 00 40 7C   MOV word ptr [0x005e8fa2],0x7c40
005355e7     5B                           POP EBX
005355e8     83 C4 0C                     ADD ESP,0xc
005355eb     C3                           RET
