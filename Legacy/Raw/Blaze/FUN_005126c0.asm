; Function: FUN_005126c0
; Entry:    005126c0

005126c0     A1 5C 31 56 00               MOV EAX,[0x0056315c]
005126c5     83 EC 0C                     SUB ESP,0xc
005126c8     C7 05 BC 49 60 00 50 9E 5D 00 MOV dword ptr [0x006049bc],0x5d9e50
005126d2     C6 05 82 49 60 00 0F         MOV byte ptr [0x00604982],0xf
005126d9     53                           PUSH EBX
005126da     55                           PUSH EBP
005126db     56                           PUSH ESI
005126dc     57                           PUSH EDI
005126dd     33 DB                        XOR EBX,EBX
005126df     50                           PUSH EAX
005126e0     89 1D 60 49 60 00            MOV dword ptr [0x00604960],EBX
005126e6     88 1D 81 49 60 00            MOV byte ptr [0x00604981],BL
005126ec     E8 0F 02 00 00               CALL 0x00512900
005126f1     83 C4 04                     ADD ESP,0x4
005126f4     68 80 49 60 00               PUSH 0x604980
005126f9     E8 E2 01 00 00               CALL 0x005128e0
005126fe     8B 35 BC 49 60 00            MOV ESI,dword ptr [0x006049bc]
00512704     83 C4 04                     ADD ESP,0x4
00512707     56                           PUSH ESI
00512708     68 40 49 60 00               PUSH 0x604940
0051270d     E8 18 34 03 00               CALL 0x00545b2a
00512712     85 C0                        TEST EAX,EAX
00512714     74 2B                        JZ 0x00512741
00512716     8B 4E 18                     MOV ECX,dword ptr [ESI + 0x18]
00512719     83 C6 28                     ADD ESI,0x28
0051271c     C1 F9 0D                     SAR ECX,0xd
0051271f     89 4E F0                     MOV dword ptr [ESI + -0x10],ECX
00512722     A0 81 49 60 00               MOV AL,[0x00604981]
00512727     FE C0                        INC AL
00512729     56                           PUSH ESI
0051272a     A2 81 49 60 00               MOV [0x00604981],AL
0051272f     E8 F0 33 03 00               CALL 0x00545b24
00512734     85 C0                        TEST EAX,EAX
00512736     74 09                        JZ 0x00512741
00512738     80 3D 81 49 60 00 20         CMP byte ptr [0x00604981],0x20
0051273f     72 D5                        JC 0x00512716
00512741     A0 81 49 60 00               MOV AL,[0x00604981]
00512746     8B 0D BC 49 60 00            MOV ECX,dword ptr [0x006049bc]
0051274c     33 ED                        XOR EBP,EBP
0051274e     89 4C 24 10                  MOV dword ptr [ESP + 0x10],ECX
00512752     84 C0                        TEST AL,AL
00512754     0F 86 78 01 00 00            JBE 0x005128d2
0051275a     8B 54 24 10                  MOV EDX,dword ptr [ESP + 0x10]
0051275e     52                           PUSH EDX
0051275f     E8 9C 01 00 00               CALL 0x00512900
00512764     83 C4 04                     ADD ESP,0x4
00512767     6A 01                        PUSH 0x1
00512769     68 40 49 60 00               PUSH 0x604940
0051276e     E8 03 33 03 00               CALL 0x00545a76
00512773     8B F0                        MOV ESI,EAX
00512775     85 F6                        TEST ESI,ESI
00512777     0F 8C 55 01 00 00            JL 0x005128d2
0051277d     6A 00                        PUSH 0x0
0051277f     6A 00                        PUSH 0x0
00512781     56                           PUSH ESI
00512782     E8 FB 32 03 00               CALL 0x00545a82
00512787     A1 B4 87 60 00               MOV EAX,[0x006087b4]
0051278c     68 00 01 00 00               PUSH 0x100
00512791     8B 08                        MOV ECX,dword ptr [EAX]
00512793     8D 94 0B 00 00 1C 00         LEA EDX,[EBX + ECX*0x1 + 0x1c0000]
0051279a     52                           PUSH EDX
0051279b     56                           PUSH ESI
0051279c     E8 E7 32 03 00               CALL 0x00545a88
005127a1     8B F8                        MOV EDI,EAX
005127a3     85 FF                        TEST EDI,EDI
005127a5     7C 3B                        JL 0x005127e2
005127a7     A1 B4 87 60 00               MOV EAX,[0x006087b4]
005127ac     8B 08                        MOV ECX,dword ptr [EAX]
005127ae     8A 84 0B 02 00 1C 00         MOV AL,byte ptr [EBX + ECX*0x1 + 0x1c0002]
005127b5     24 0F                        AND AL,0xf
005127b7     88 85 8C 49 60 00            MOV byte ptr [EBP + 0x60498c],AL
005127bd     25 FF 00 00 00               AND EAX,0xff
005127c2     48                           DEC EAX
005127c3     74 1D                        JZ 0x005127e2
005127c5     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
005127cb     C1 E0 07                     SHL EAX,0x7
005127ce     8B 3A                        MOV EDI,dword ptr [EDX]
005127d0     50                           PUSH EAX
005127d1     8D 83 00 01 1C 00            LEA EAX,[EBX + 0x1c0100]
005127d7     03 C7                        ADD EAX,EDI
005127d9     50                           PUSH EAX
005127da     56                           PUSH ESI
005127db     E8 A8 32 03 00               CALL 0x00545a88
005127e0     8B F8                        MOV EDI,EAX
005127e2     56                           PUSH ESI
005127e3     E8 94 32 03 00               CALL 0x00545a7c
005127e8     85 FF                        TEST EDI,EDI
005127ea     0F 8C E2 00 00 00            JL 0x005128d2
005127f0     8D 93 7E 00 1C 00            LEA EDX,[EBX + 0x1c007e]
005127f6     BE 10 00 00 00               MOV ESI,0x10
005127fb     A1 B4 87 60 00               MOV EAX,[0x006087b4]
00512800     8B 08                        MOV ECX,dword ptr [EAX]
00512802     66 8B 04 11                  MOV AX,word ptr [ECX + EDX*0x1]
00512806     03 CA                        ADD ECX,EDX
00512808     66 85 C0                     TEST AX,AX
0051280b     74 06                        JZ 0x00512813
0051280d     80 CC 80                     OR AH,0x80
00512810     66 89 01                     MOV word ptr [ECX],AX
00512813     83 EA 02                     SUB EDX,0x2
00512816     4E                           DEC ESI
00512817     75 E2                        JNZ 0x005127fb
00512819     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
0051281f     8B FD                        MOV EDI,EBP
00512821     83 E7 0F                     AND EDI,0xf
00512824     8B 02                        MOV EAX,dword ptr [EDX]
00512826     8B D7                        MOV EDX,EDI
00512828     C1 E2 04                     SHL EDX,0x4
0051282b     8D 8C 03 60 00 1C 00         LEA ECX,[EBX + EAX*0x1 + 0x1c0060]
00512832     8B C5                        MOV EAX,EBP
00512834     66 89 54 24 14               MOV word ptr [ESP + 0x14],DX
00512839     51                           PUSH ECX
0051283a     99                           CDQ
0051283b     83 E2 0F                     AND EDX,0xf
0051283e     8D 4C 24 18                  LEA ECX,[ESP + 0x18]
00512842     03 C2                        ADD EAX,EDX
00512844     51                           PUSH ECX
00512845     8B F0                        MOV ESI,EAX
00512847     66 C7 44 24 20 10 00         MOV word ptr [ESP + 0x20],0x10
0051284e     C1 FE 04                     SAR ESI,0x4
00512851     66 C7 44 24 22 01 00         MOV word ptr [ESP + 0x22],0x1
00512858     8D 86 FE 01 00 00            LEA EAX,[ESI + 0x1fe]
0051285e     66 89 44 24 1E               MOV word ptr [ESP + 0x1e],AX
00512863     E8 B6 32 03 00               CALL 0x00545b1e
00512868     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
0051286e     8D 0C BD C0 03 00 00         LEA ECX,[EDI*0x4 + 0x3c0]
00512875     C1 E6 06                     SHL ESI,0x6
00512878     8B 02                        MOV EAX,dword ptr [EDX]
0051287a     83 C6 20                     ADD ESI,0x20
0051287d     66 89 4C 24 14               MOV word ptr [ESP + 0x14],CX
00512882     66 89 74 24 16               MOV word ptr [ESP + 0x16],SI
00512887     66 C7 44 24 18 04 00         MOV word ptr [ESP + 0x18],0x4
0051288e     8D 84 03 80 00 1C 00         LEA EAX,[EBX + EAX*0x1 + 0x1c0080]
00512895     66 0F B6 95 8C 49 60 00      MOVZX DX,byte ptr [EBP + 0x60498c]
0051289d     50                           PUSH EAX
0051289e     8D 44 24 18                  LEA EAX,[ESP + 0x18]
005128a2     C1 E2 04                     SHL EDX,0x4
005128a5     50                           PUSH EAX
005128a6     66 89 54 24 22               MOV word ptr [ESP + 0x22],DX
005128ab     E8 6E 32 03 00               CALL 0x00545b1e
005128b0     8B 4C 24 10                  MOV ECX,dword ptr [ESP + 0x10]
005128b4     45                           INC EBP
005128b5     83 C1 28                     ADD ECX,0x28
005128b8     81 C3 00 01 00 00            ADD EBX,0x100
005128be     89 4C 24 10                  MOV dword ptr [ESP + 0x10],ECX
005128c2     33 C9                        XOR ECX,ECX
005128c4     8A 0D 81 49 60 00            MOV CL,byte ptr [0x00604981]
005128ca     3B E9                        CMP EBP,ECX
005128cc     0F 8C 88 FE FF FF            JL 0x0051275a
005128d2     5F                           POP EDI
005128d3     5E                           POP ESI
005128d4     5D                           POP EBP
005128d5     33 C0                        XOR EAX,EAX
005128d7     5B                           POP EBX
005128d8     83 C4 0C                     ADD ESP,0xc
005128db     C3                           RET
