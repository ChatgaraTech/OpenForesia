; Function: FUN_00508860
; Entry:    00508860

00508860     A1 48 2A 56 00           MOV EAX,[0x00562a48]
00508865     83 EC 14                 SUB ESP,0x14
00508868     53                       PUSH EBX
00508869     33 DB                    XOR EBX,EBX
0050886b     56                       PUSH ESI
0050886c     3B C3                    CMP EAX,EBX
0050886e     57                       PUSH EDI
0050886f     0F 84 B1 00 00 00        JZ 0x00508926
00508875     B8 28 00 00 00           MOV EAX,0x28
0050887a     33 C9                    XOR ECX,ECX
0050887c     66 A3 1C 35 57 00        MOV [0x0057351c],AX
00508882     66 A3 22 35 57 00        MOV [0x00573522],AX
00508888     B8 68 00 00 00           MOV EAX,0x68
0050888d     89 1D 48 2A 56 00        MOV dword ptr [0x00562a48],EBX
00508893     66 A3 E4 3D 57 00        MOV [0x00573de4],AX
00508899     66 A3 E6 3D 57 00        MOV [0x00573de6],AX
0050889f     A1 38 33 57 00           MOV EAX,[0x00573338]
005088a4     89 1D F8 8D 57 00        MOV dword ptr [0x00578df8],EBX
005088aa     89 1D FC 8D 57 00        MOV dword ptr [0x00578dfc],EBX
005088b0     66 C7 05 1E 35 57 00 38 00 MOV word ptr [0x0057351e],0x38
005088b9     66 C7 05 20 35 57 00 E0 00 MOV word ptr [0x00573520],0xe0
005088c2     C7 05 38 35 57 00 24 0C 07 01 MOV dword ptr [0x00573538],0x1070c24
005088cc     C7 05 3C 35 57 00 00 00 00 01 MOV dword ptr [0x0057353c],0x1000000
005088d6     66 C7 05 E8 3D 57 00 60 00 MOV word ptr [0x00573de8],0x60
005088df     C6 05 F2 3D 57 00 0B     MOV byte ptr [0x00573df2],0xb
005088e6     66 C7 05 EA 3D 57 00 18 00 MOV word ptr [0x00573dea],0x18
005088ef     C6 05 F4 3D 57 00 05     MOV byte ptr [0x00573df4],0x5
005088f6     C7 05 00 3E 57 00 10 0C 07 01 MOV dword ptr [0x00573e00],0x1070c10
00508900     C7 05 04 3E 57 00 00 00 00 01 MOV dword ptr [0x00573e04],0x1000000
0050890a     C7 05 50 2A 56 00 02 00 00 00 MOV dword ptr [0x00562a50],0x2
00508914     8A 88 50 01 00 00        MOV CL,byte ptr [EAX + 0x150]
0050891a     8A 91 70 2B 56 00        MOV DL,byte ptr [ECX + 0x562b70]
00508920     88 90 53 01 00 00        MOV byte ptr [EAX + 0x153],DL
00508926     A1 38 33 57 00           MOV EAX,[0x00573338]
0050892b     66 0F B6 80 50 01 00 00  MOVZX AX,byte ptr [EAX + 0x150]
00508933     83 E8 08                 SUB EAX,0x8
00508936     C1 E0 08                 SHL EAX,0x8
00508939     66 2B 05 2A 49 60 00     SUB AX,word ptr [0x0060492a]
00508940     25 FF 0F 00 00           AND EAX,0xfff
00508945     66 3D 00 08              CMP AX,0x800
00508949     7D 08                    JGE 0x00508953
0050894b     0F BF C0                 MOVSX EAX,AX
0050894e     C1 F8 02                 SAR EAX,0x2
00508951     EB 0F                    JMP 0x00508962
00508953     0F BF C8                 MOVSX ECX,AX
00508956     B8 00 10 00 00           MOV EAX,0x1000
0050895b     2B C1                    SUB EAX,ECX
0050895d     C1 F8 02                 SAR EAX,0x2
00508960     F7 D8                    NEG EAX
00508962     66 01 05 2A 49 60 00     ADD word ptr [0x0060492a],AX
00508969     66 A1 A0 93 5E 00        MOV AX,[0x005e93a0]
0050896f     0F BF D0                 MOVSX EDX,AX
00508972     B9 60 00 00 00           MOV ECX,0x60
00508977     68 E0 08 60 00           PUSH 0x6008e0
0050897c     2B CA                    SUB ECX,EDX
0050897e     68 28 49 60 00           PUSH 0x604928
00508983     C1 F9 02                 SAR ECX,0x2
00508986     66 03 C1                 ADD AX,CX
00508989     B9 60 00 00 00           MOV ECX,0x60
0050898e     66 A3 A0 93 5E 00        MOV [0x005e93a0],AX
00508994     66 A1 A2 93 5E 00        MOV AX,[0x005e93a2]
0050899a     0F BF D0                 MOVSX EDX,AX
0050899d     2B CA                    SUB ECX,EDX
0050899f     C1 F9 02                 SAR ECX,0x2
005089a2     66 03 C1                 ADD AX,CX
005089a5     B9 60 00 00 00           MOV ECX,0x60
005089aa     66 A3 A2 93 5E 00        MOV [0x005e93a2],AX
005089b0     66 A1 A4 93 5E 00        MOV AX,[0x005e93a4]
005089b6     0F BF D0                 MOVSX EDX,AX
005089b9     2B CA                    SUB ECX,EDX
005089bb     C1 F9 02                 SAR ECX,0x2
005089be     66 03 C1                 ADD AX,CX
005089c1     66 A3 A4 93 5E 00        MOV [0x005e93a4],AX
005089c7     E8 34 39 01 00           CALL 0x0051c300
005089cc     0F BF 15 A4 93 5E 00     MOVSX EDX,word ptr [0x005e93a4]
005089d3     0F BF 05 A2 93 5E 00     MOVSX EAX,word ptr [0x005e93a2]
005089da     0F BF 0D A0 93 5E 00     MOVSX ECX,word ptr [0x005e93a0]
005089e1     83 C4 08                 ADD ESP,0x8
005089e4     52                       PUSH EDX
005089e5     50                       PUSH EAX
005089e6     51                       PUSH ECX
005089e7     E8 B4 D0 03 00           CALL 0x00545aa0
005089ec     A1 38 33 57 00           MOV EAX,[0x00573338]
005089f1     33 D2                    XOR EDX,EDX
005089f3     8A 90 50 01 00 00        MOV DL,byte ptr [EAX + 0x150]
005089f9     66 A1 7A 2B 57 00        MOV AX,[0x00572b7a]
005089ff     0F BF C8                 MOVSX ECX,AX
00508a02     C1 E2 08                 SHL EDX,0x8
00508a05     F7 D9                    NEG ECX
00508a07     2B CA                    SUB ECX,EDX
00508a09     C1 F9 02                 SAR ECX,0x2
00508a0c     66 03 C1                 ADD AX,CX
00508a0f     66 A3 7A 2B 57 00        MOV [0x00572b7a],AX
00508a15     A1 4C 2A 56 00           MOV EAX,[0x00562a4c]
00508a1a     3B C3                    CMP EAX,EBX
00508a1c     0F 85 49 02 00 00        JNZ 0x00508c6b
00508a22     8B 15 F0 8D 57 00        MOV EDX,dword ptr [0x00578df0]
00508a28     8D 42 A1                 LEA EAX,[EDX + -0x5f]
00508a2b     83 F8 03                 CMP EAX,0x3
00508a2e     0F 87 37 02 00 00        JA 0x00508c6b
00508a34     FF 24 85 E4 8D 50 00     JMP dword ptr [EAX*0x4 + 0x508de4]
00508c6b     8B 7C 24 24              MOV EDI,dword ptr [ESP + 0x24]
00508c6f     8D 54 24 2C              LEA EDX,[ESP + 0x2c]
00508c73     52                       PUSH EDX
00508c74     57                       PUSH EDI
00508c75     E8 A6 31 01 00           CALL 0x0051be20
00508c7a     A1 C0 97 5E 00           MOV EAX,[0x005e97c0]
00508c7f     83 C4 08                 ADD ESP,0x8
00508c82     8B 88 94 00 00 00        MOV ECX,dword ptr [EAX + 0x94]
00508c88     A1 F8 8D 57 00           MOV EAX,[0x00578df8]
00508c8d     53                       PUSH EBX
00508c8e     53                       PUSH EBX
00508c8f     8B 11                    MOV EDX,dword ptr [ECX]
00508c91     8B 0D 1C 35 57 00        MOV ECX,dword ptr [0x0057351c]
00508c97     8D 04 40                 LEA EAX,[EAX + EAX*0x2]
00508c9a     89 54 24 14              MOV dword ptr [ESP + 0x14],EDX
00508c9e     C1 E0 04                 SHL EAX,0x4
00508ca1     66 C7 44 24 24 18 00     MOV word ptr [ESP + 0x24],0x18
00508ca8     C7 44 24 18 00 00 00 01  MOV dword ptr [ESP + 0x18],0x1000000
00508cb0     8D 54 08 14              LEA EDX,[EAX + ECX*0x1 + 0x14]
00508cb4     A1 FC 8D 57 00           MOV EAX,[0x00578dfc]
00508cb9     C1 E0 04                 SHL EAX,0x4
00508cbc     66 03 05 1E 35 57 00     ADD AX,word ptr [0x0057351e]
00508cc3     66 89 54 24 1C           MOV word ptr [ESP + 0x1c],DX
00508cc8     8B 54 24 34              MOV EDX,dword ptr [ESP + 0x34]
00508ccc     8D 4C 24 14              LEA ECX,[ESP + 0x14]
00508cd0     51                       PUSH ECX
00508cd1     83 C0 0A                 ADD EAX,0xa
00508cd4     52                       PUSH EDX
00508cd5     57                       PUSH EDI
00508cd6     66 89 44 24 2A           MOV word ptr [ESP + 0x2a],AX
00508cdb     E8 10 91 01 00           CALL 0x00521df0
00508ce0     83 C4 14                 ADD ESP,0x14
00508ce3     8D 4C 24 0C              LEA ECX,[ESP + 0xc]
00508ce7     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508ceb     51                       PUSH ECX
00508cec     50                       PUSH EAX
00508ced     57                       PUSH EDI
00508cee     E8 CD 6C 01 00           CALL 0x0051f9c0
00508cf3     83 C4 0C                 ADD ESP,0xc
00508cf6     8D 54 24 2C              LEA EDX,[ESP + 0x2c]
00508cfa     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508cfe     52                       PUSH EDX
00508cff     57                       PUSH EDI
00508d00     E8 AB 30 01 00           CALL 0x0051bdb0
00508d05     83 C4 08                 ADD ESP,0x8
00508d08     53                       PUSH EBX
00508d09     68 18 35 57 00           PUSH 0x573518
00508d0e     53                       PUSH EBX
00508d0f     68 F8 06 56 00           PUSH 0x5606f8
00508d14     E8 37 B6 03 00           CALL 0x00544350
00508d19     8B 44 24 3C              MOV EAX,dword ptr [ESP + 0x3c]
00508d1d     83 C4 10                 ADD ESP,0x10
00508d20     68 18 35 57 00           PUSH 0x573518
00508d25     50                       PUSH EAX
00508d26     E8 75 B6 03 00           CALL 0x005443a0
00508d2b     83 C4 08                 ADD ESP,0x8
00508d2e     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508d32     BE 00 05 00 00           MOV ESI,0x500
00508d37     8B 0D C4 97 5E 00        MOV ECX,dword ptr [0x005e97c4]
00508d3d     53                       PUSH EBX
00508d3e     68 18 35 57 00           PUSH 0x573518
00508d43     53                       PUSH EBX
00508d44     8B 51 70                 MOV EDX,dword ptr [ECX + 0x70]
00508d47     8B 04 32                 MOV EAX,dword ptr [EDX + ESI*0x1]
00508d4a     50                       PUSH EAX
00508d4b     E8 00 B6 03 00           CALL 0x00544350
00508d50     8B 4C 24 3C              MOV ECX,dword ptr [ESP + 0x3c]
00508d54     83 C4 10                 ADD ESP,0x10
00508d57     68 18 35 57 00           PUSH 0x573518
00508d5c     51                       PUSH ECX
00508d5d     E8 3E B6 03 00           CALL 0x005443a0
00508d62     83 C6 04                 ADD ESI,0x4
00508d65     83 C4 08                 ADD ESP,0x8
00508d68     81 FE 08 05 00 00        CMP ESI,0x508
00508d6e     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508d72     7C C3                    JL 0x00508d37
00508d74     68 18 35 57 00           PUSH 0x573518
00508d79     50                       PUSH EAX
00508d7a     57                       PUSH EDI
00508d7b     E8 C0 C5 03 00           CALL 0x00545340
00508d80     8B 0D F0 8D 57 00        MOV ECX,dword ptr [0x00578df0]
00508d86     83 C4 0C                 ADD ESP,0xc
00508d89     83 F9 2E                 CMP ECX,0x2e
00508d8c     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508d90     74 48                    JZ 0x00508dda
00508d92     8B 15 C4 97 5E 00        MOV EDX,dword ptr [0x005e97c4]
00508d98     53                       PUSH EBX
00508d99     68 E0 3D 57 00           PUSH 0x573de0
00508d9e     68 48 2B 57 00           PUSH 0x572b48
00508da3     8B 42 70                 MOV EAX,dword ptr [EDX + 0x70]
00508da6     8B 88 8C 01 00 00        MOV ECX,dword ptr [EAX + 0x18c]
00508dac     51                       PUSH ECX
00508dad     E8 9E B5 03 00           CALL 0x00544350
00508db2     8B 54 24 3C              MOV EDX,dword ptr [ESP + 0x3c]
00508db6     83 C4 10                 ADD ESP,0x10
00508db9     68 E0 3D 57 00           PUSH 0x573de0
00508dbe     52                       PUSH EDX
00508dbf     E8 DC B5 03 00           CALL 0x005443a0
00508dc4     83 C4 08                 ADD ESP,0x8
00508dc7     89 44 24 2C              MOV dword ptr [ESP + 0x2c],EAX
00508dcb     68 E0 3D 57 00           PUSH 0x573de0
00508dd0     50                       PUSH EAX
00508dd1     57                       PUSH EDI
00508dd2     E8 69 C5 03 00           CALL 0x00545340
00508dd7     83 C4 0C                 ADD ESP,0xc
00508dda     5F                       POP EDI
00508ddb     5E                       POP ESI
00508ddc     5B                       POP EBX
00508ddd     83 C4 14                 ADD ESP,0x14
00508de0     C3                       RET
