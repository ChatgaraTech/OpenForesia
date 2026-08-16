; Function: FUN_0051c120
; Entry:    0051c120

0051c120     83 EC 20                     SUB ESP,0x20
0051c123     8B 4C 24 24                  MOV ECX,dword ptr [ESP + 0x24]
0051c127     53                           PUSH EBX
0051c128     55                           PUSH EBP
0051c129     56                           PUSH ESI
0051c12a     8D 44 24 34                  LEA EAX,[ESP + 0x34]
0051c12e     57                           PUSH EDI
0051c12f     50                           PUSH EAX
0051c130     51                           PUSH ECX
0051c131     E8 EA FC FF FF               CALL 0x0051be20
0051c136     8B 74 24 44                  MOV ESI,dword ptr [ESP + 0x44]
0051c13a     33 C9                        XOR ECX,ECX
0051c13c     83 C4 08                     ADD ESP,0x8
0051c13f     8B 2E                        MOV EBP,dword ptr [ESI]
0051c141     8B 5D 00                     MOV EBX,dword ptr [EBP]
0051c144     8B C3                        MOV EAX,EBX
0051c146     8B D3                        MOV EDX,EBX
0051c148     C1 E8 08                     SHR EAX,0x8
0051c14b     88 44 24 13                  MOV byte ptr [ESP + 0x13],AL
0051c14f     8B C3                        MOV EAX,EBX
0051c151     C1 EA 14                     SHR EDX,0x14
0051c154     C1 E8 1B                     SHR EAX,0x1b
0051c157     66 0F B6 FA                  MOVZX DI,DL
0051c15b     8A E8                        MOV CH,AL
0051c15d     88 44 24 12                  MOV byte ptr [ESP + 0x12],AL
0051c161     0B CF                        OR ECX,EDI
0051c163     88 54 24 3C                  MOV byte ptr [ESP + 0x3c],DL
0051c167     66 89 4C 24 16               MOV word ptr [ESP + 0x16],CX
0051c16c     8B 45 04                     MOV EAX,dword ptr [EBP + 0x4]
0051c16f     8B 4E 08                     MOV ECX,dword ptr [ESI + 0x8]
0051c172     89 44 24 18                  MOV dword ptr [ESP + 0x18],EAX
0051c176     8B 46 04                     MOV EAX,dword ptr [ESI + 0x4]
0051c179     8B 74 24 40                  MOV ESI,dword ptr [ESP + 0x40]
0051c17d     89 4C 24 24                  MOV dword ptr [ESP + 0x24],ECX
0051c181     8D 4C 24 14                  LEA ECX,[ESP + 0x14]
0051c185     89 4C 24 1C                  MOV dword ptr [ESP + 0x1c],ECX
0051c189     8A 0E                        MOV CL,byte ptr [ESI]
0051c18b     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
0051c18f     B0 07                        MOV AL,0x7
0051c191     84 C9                        TEST CL,CL
0051c193     C7 44 24 2C 00 00 00 00      MOV dword ptr [ESP + 0x2c],0x0
0051c19b     88 44 24 17                  MOV byte ptr [ESP + 0x17],AL
0051c19f     88 44 24 16                  MOV byte ptr [ESP + 0x16],AL
0051c1a3     74 6B                        JZ 0x0051c210
0051c1a5     8B 6C 24 4C                  MOV EBP,dword ptr [ESP + 0x4c]
0051c1a9     EB 04                        JMP 0x0051c1af
0051c1ab     8A 54 24 3C                  MOV DL,byte ptr [ESP + 0x3c]
0051c1af     85 ED                        TEST EBP,EBP
0051c1b1     74 5D                        JZ 0x0051c210
0051c1b3     8A C1                        MOV AL,CL
0051c1b5     24 0F                        AND AL,0xf
0051c1b7     F6 EA                        IMUL DL
0051c1b9     02 C3                        ADD AL,BL
0051c1bb     8B 54 24 48                  MOV EDX,dword ptr [ESP + 0x48]
0051c1bf     88 44 24 14                  MOV byte ptr [ESP + 0x14],AL
0051c1c3     8A C1                        MOV AL,CL
0051c1c5     C0 E8 04                     SHR AL,0x4
0051c1c8     F6 6C 24 12                  IMUL byte ptr [ESP + 0x12]
0051c1cc     8A 4C 24 13                  MOV CL,byte ptr [ESP + 0x13]
0051c1d0     52                           PUSH EDX
0051c1d1     8B 54 24 3C                  MOV EDX,dword ptr [ESP + 0x3c]
0051c1d5     02 C1                        ADD AL,CL
0051c1d7     88 44 24 19                  MOV byte ptr [ESP + 0x19],AL
0051c1db     8B 44 24 48                  MOV EAX,dword ptr [ESP + 0x48]
0051c1df     8D 4C 24 20                  LEA ECX,[ESP + 0x20]
0051c1e3     50                           PUSH EAX
0051c1e4     8B 44 24 3C                  MOV EAX,dword ptr [ESP + 0x3c]
0051c1e8     51                           PUSH ECX
0051c1e9     52                           PUSH EDX
0051c1ea     50                           PUSH EAX
0051c1eb     E8 00 5C 00 00               CALL 0x00521df0
0051c1f0     8A 0E                        MOV CL,byte ptr [ESI]
0051c1f2     66 01 7C 24 38               ADD word ptr [ESP + 0x38],DI
0051c1f7     83 C4 14                     ADD ESP,0x14
0051c1fa     89 44 24 38                  MOV dword ptr [ESP + 0x38],EAX
0051c1fe     51                           PUSH ECX
0051c1ff     E8 DC FE FF FF               CALL 0x0051c0e0
0051c204     8A 4E 01                     MOV CL,byte ptr [ESI + 0x1]
0051c207     83 C4 04                     ADD ESP,0x4
0051c20a     46                           INC ESI
0051c20b     4D                           DEC EBP
0051c20c     84 C9                        TEST CL,CL
0051c20e     75 9B                        JNZ 0x0051c1ab
0051c210     8B 44 24 38                  MOV EAX,dword ptr [ESP + 0x38]
0051c214     8B 74 24 34                  MOV ESI,dword ptr [ESP + 0x34]
0051c218     8D 54 24 1C                  LEA EDX,[ESP + 0x1c]
0051c21c     52                           PUSH EDX
0051c21d     50                           PUSH EAX
0051c21e     56                           PUSH ESI
0051c21f     E8 9C 37 00 00               CALL 0x0051f9c0
0051c224     83 C4 0C                     ADD ESP,0xc
0051c227     8D 4C 24 38                  LEA ECX,[ESP + 0x38]
0051c22b     89 44 24 38                  MOV dword ptr [ESP + 0x38],EAX
0051c22f     51                           PUSH ECX
0051c230     56                           PUSH ESI
0051c231     E8 7A FB FF FF               CALL 0x0051bdb0
0051c236     8B 44 24 40                  MOV EAX,dword ptr [ESP + 0x40]
0051c23a     83 C4 08                     ADD ESP,0x8
0051c23d     5F                           POP EDI
0051c23e     5E                           POP ESI
0051c23f     5D                           POP EBP
0051c240     5B                           POP EBX
0051c241     83 C4 20                     ADD ESP,0x20
0051c244     C3                           RET
