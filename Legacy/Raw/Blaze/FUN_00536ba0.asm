; Function: FUN_00536ba0
; Entry:    00536ba0

00536ba0     83 EC 34                     SUB ESP,0x34
00536ba3     A1 B4 87 60 00               MOV EAX,[0x006087b4]
00536ba8     53                           PUSH EBX
00536ba9     55                           PUSH EBP
00536baa     56                           PUSH ESI
00536bab     8B 00                        MOV EAX,dword ptr [EAX]
00536bad     57                           PUSH EDI
00536bae     33 DB                        XOR EBX,EBX
00536bb0     BF C4 93 5E 00               MOV EDI,0x5e93c4
00536bb5     8D A8 00 80 0F 00            LEA EBP,[EAX + 0xf8000]
00536bbb     05 BD 02 0F 00               ADD EAX,0xf02bd
00536bc0     BA 80 96 5E 00               MOV EDX,0x5e9680
00536bc5     89 7C 24 14                  MOV dword ptr [ESP + 0x14],EDI
00536bc9     89 44 24 18                  MOV dword ptr [ESP + 0x18],EAX
00536bcd     89 54 24 10                  MOV dword ptr [ESP + 0x10],EDX
00536bd1     33 C9                        XOR ECX,ECX
00536bd3     8A 88 93 FE FF FF            MOV CL,byte ptr [EAX + 0xfffffe93]
00536bd9     8B F1                        MOV ESI,ECX
00536bdb     8B 4F FC                     MOV ECX,dword ptr [EDI + -0x4]
00536bde     81 E1 F9 DF FF EF            AND ECX,0xefffdff9
00536be4     89 74 24 1C                  MOV dword ptr [ESP + 0x1c],ESI
00536be8     81 C9 00 8A 80 81            OR ECX,0x81808a00
00536bee     89 4F FC                     MOV dword ptr [EDI + -0x4],ECX
00536bf1     8A 00                        MOV AL,byte ptr [EAX]
00536bf3     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
00536bf9     88 47 07                     MOV byte ptr [EDI + 0x7],AL
00536bfc     8B 01                        MOV EAX,dword ptr [ECX]
00536bfe     33 C9                        XOR ECX,ECX
00536c00     05 00 00 0F 00               ADD EAX,0xf0000
00536c05     81 FA 80 96 5E 00            CMP EDX,0x5e9680
00536c0b     7E 1E                        JLE 0x00536c2b
00536c0d     05 50 01 00 00               ADD EAX,0x150
00536c12     33 D2                        XOR EDX,EDX
00536c14     8A 10                        MOV DL,byte ptr [EAX]
00536c16     3B D6                        CMP EDX,ESI
00536c18     74 0C                        JZ 0x00536c26
00536c1a     41                           INC ECX
00536c1b     05 00 20 00 00               ADD EAX,0x2000
00536c20     3B CB                        CMP ECX,EBX
00536c22     7C EE                        JL 0x00536c12
00536c24     EB 05                        JMP 0x00536c2b
00536c26     88 0F                        MOV byte ptr [EDI],CL
00536c28     83 C9 FF                     OR ECX,0xffffffff
00536c2b     83 F9 FF                     CMP ECX,-0x1
00536c2e     0F 84 2A 02 00 00            JZ 0x00536e5e
00536c34     A1 C4 97 5E 00               MOV EAX,[0x005e97c4]
00536c39     8D 3C F5 00 00 00 00         LEA EDI,[ESI*0x8 + 0x0]
00536c40     8A D3                        MOV DL,BL
00536c42     8B B0 84 00 00 00            MOV ESI,dword ptr [EAX + 0x84]
00536c48     8B 04 3E                     MOV EAX,dword ptr [ESI + EDI*0x1]
00536c4b     03 F7                        ADD ESI,EDI
00536c4d     89 44 24 20                  MOV dword ptr [ESP + 0x20],EAX
00536c51     C0 E2 02                     SHL DL,0x2
00536c54     8B 4E 04                     MOV ECX,dword ptr [ESI + 0x4]
00536c57     89 4C 24 24                  MOV dword ptr [ESP + 0x24],ECX
00536c5b     8A 4C 24 25                  MOV CL,byte ptr [ESP + 0x25]
00536c5f     02 CA                        ADD CL,DL
00536c61     88 5C 24 26                  MOV byte ptr [ESP + 0x26],BL
00536c65     88 4C 24 25                  MOV byte ptr [ESP + 0x25],CL
00536c69     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
00536c6f     8B 11                        MOV EDX,dword ptr [ECX]
00536c71     81 C2 00 00 1B 00            ADD EDX,0x1b0000
00536c77     52                           PUSH EDX
00536c78     50                           PUSH EAX
00536c79     E8 D2 2E FF FF               CALL 0x00529b50
00536c7e     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
00536c84     83 C4 08                     ADD ESP,0x8
00536c87     8D 44 24 24                  LEA EAX,[ESP + 0x24]
00536c8b     8D 0C 9D 80 8F 5E 00         LEA ECX,[EBX*0x4 + 0x5e8f80]
00536c92     50                           PUSH EAX
00536c93     8B 02                        MOV EAX,dword ptr [EDX]
00536c95     05 00 00 1B 00               ADD EAX,0x1b0000
00536c9a     51                           PUSH ECX
00536c9b     50                           PUSH EAX
00536c9c     E8 4F B6 FF FF               CALL 0x005322f0
00536ca1     8B 86 80 00 00 00            MOV EAX,dword ptr [ESI + 0x80]
00536ca7     81 C6 80 00 00 00            ADD ESI,0x80
00536cad     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
00536cb3     89 44 24 2C                  MOV dword ptr [ESP + 0x2c],EAX
00536cb7     8B 4E 04                     MOV ECX,dword ptr [ESI + 0x4]
00536cba     83 C4 0C                     ADD ESP,0xc
00536cbd     89 4C 24 24                  MOV dword ptr [ESP + 0x24],ECX
00536cc1     8A 4C 24 25                  MOV CL,byte ptr [ESP + 0x25]
00536cc5     80 C1 10                     ADD CL,0x10
00536cc8     C6 44 24 26 03               MOV byte ptr [ESP + 0x26],0x3
00536ccd     88 4C 24 25                  MOV byte ptr [ESP + 0x25],CL
00536cd1     8B 0A                        MOV ECX,dword ptr [EDX]
00536cd3     81 C1 00 00 1B 00            ADD ECX,0x1b0000
00536cd9     51                           PUSH ECX
00536cda     50                           PUSH EAX
00536cdb     E8 70 2E FF FF               CALL 0x00529b50
00536ce0     8B 15 B4 87 60 00            MOV EDX,dword ptr [0x006087b4]
00536ce6     83 C4 08                     ADD ESP,0x8
00536ce9     8B 02                        MOV EAX,dword ptr [EDX]
00536ceb     05 00 00 1B 00               ADD EAX,0x1b0000
00536cf0     50                           PUSH EAX
00536cf1     E8 5A EF 00 00               CALL 0x00545c50
00536cf6     8D 4C 24 30                  LEA ECX,[ESP + 0x30]
00536cfa     51                           PUSH ECX
00536cfb     E8 4A EF 00 00               CALL 0x00545c4a
00536d00     8B 44 24 34                  MOV EAX,dword ptr [ESP + 0x34]
00536d04     8B D3                        MOV EDX,EBX
00536d06     C1 E2 06                     SHL EDX,0x6
00536d09     66 89 10                     MOV word ptr [EAX],DX
00536d0c     8B 54 24 34                  MOV EDX,dword ptr [ESP + 0x34]
00536d10     66 0F B6 4C 24 25            MOVZX CX,byte ptr [ESP + 0x25]
00536d16     81 C1 C0 01 00 00            ADD ECX,0x1c0
00536d1c     66 89 4A 02                  MOV word ptr [EDX + 0x2],CX
00536d20     8B 44 24 34                  MOV EAX,dword ptr [ESP + 0x34]
00536d24     66 C7 40 04 40 00            MOV word ptr [EAX + 0x4],0x40
00536d2a     8B 4C 24 34                  MOV ECX,dword ptr [ESP + 0x34]
00536d2e     66 C7 41 06 01 00            MOV word ptr [ECX + 0x6],0x1
00536d34     8B 54 24 38                  MOV EDX,dword ptr [ESP + 0x38]
00536d38     8B 44 24 34                  MOV EAX,dword ptr [ESP + 0x34]
00536d3c     52                           PUSH EDX
00536d3d     50                           PUSH EAX
00536d3e     E8 DB ED 00 00               CALL 0x00545b1e
00536d43     8A 4C 24 24                  MOV CL,byte ptr [ESP + 0x24]
00536d47     66 33 D2                     XOR DX,DX
00536d4a     80 E1 0F                     AND CL,0xf
00536d4d     8B C3                        MOV EAX,EBX
00536d4f     8A D1                        MOV DL,CL
00536d51     C1 E0 06                     SHL EAX,0x6
00536d54     C1 E2 06                     SHL EDX,0x6
00536d57     C1 F8 02                     SAR EAX,0x2
00536d5a     03 D0                        ADD EDX,EAX
00536d5c     8B 4C 24 3C                  MOV ECX,dword ptr [ESP + 0x3c]
00536d60     66 89 11                     MOV word ptr [ECX],DX
00536d63     8A 44 24 24                  MOV AL,byte ptr [ESP + 0x24]
00536d67     66 0F B6 54 24 26            MOVZX DX,byte ptr [ESP + 0x26]
00536d6d     24 10                        AND AL,0x10
00536d6f     66 33 C9                     XOR CX,CX
00536d72     8A C8                        MOV CL,AL
00536d74     8B 44 24 3C                  MOV EAX,dword ptr [ESP + 0x3c]
00536d78     8D 14 91                     LEA EDX,[ECX + EDX*0x4]
00536d7b     C1 E2 04                     SHL EDX,0x4
00536d7e     66 89 50 02                  MOV word ptr [EAX + 0x2],DX
00536d82     8B 4C 24 40                  MOV ECX,dword ptr [ESP + 0x40]
00536d86     8B 54 24 3C                  MOV EDX,dword ptr [ESP + 0x3c]
00536d8a     51                           PUSH ECX
00536d8b     52                           PUSH EDX
00536d8c     E8 8D ED 00 00               CALL 0x00545b1e
00536d91     8B 44 24 3C                  MOV EAX,dword ptr [ESP + 0x3c]
00536d95     55                           PUSH EBP
00536d96     66 8B 10                     MOV DX,word ptr [EAX]
00536d99     66 8B 48 02                  MOV CX,word ptr [EAX + 0x2]
00536d9d     8A 44 24 34                  MOV AL,byte ptr [ESP + 0x34]
00536da1     81 E1 00 01 00 00            AND ECX,0x100
00536da7     C1 FA 02                     SAR EDX,0x2
00536daa     81 E2 F0 00 00 00            AND EDX,0xf0
00536db0     83 E0 03                     AND EAX,0x3
00536db3     0B CA                        OR ECX,EDX
00536db5     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
00536dbb     80 CD 20                     OR CH,0x20
00536dbe     66 C1 F9 04                  SAR CX,0x4
00536dc2     C1 E0 07                     SHL EAX,0x7
00536dc5     0B C8                        OR ECX,EAX
00536dc7     66 89 0C 9D 90 8F 5E 00      MOV word ptr [EBX*0x4 + 0x5e8f90],CX
00536dcf     66 0F B6 4C 24 29            MOVZX CX,byte ptr [ESP + 0x29]
00536dd5     81 C1 C0 01 00 00            ADD ECX,0x1c0
00536ddb     C1 E1 06                     SHL ECX,0x6
00536dde     66 89 0C 9D 92 8F 5E 00      MOV word ptr [EBX*0x4 + 0x5e8f92],CX
00536de6     8B 42 2C                     MOV EAX,dword ptr [EDX + 0x2c]
00536de9     03 C7                        ADD EAX,EDI
00536deb     8B 08                        MOV ECX,dword ptr [EAX]
00536ded     89 4C 24 2C                  MOV dword ptr [ESP + 0x2c],ECX
00536df1     8B 40 04                     MOV EAX,dword ptr [EAX + 0x4]
00536df4     89 44 24 30                  MOV dword ptr [ESP + 0x30],EAX
00536df8     8A 54 24 32                  MOV DL,byte ptr [ESP + 0x32]
00536dfc     02 D3                        ADD DL,BL
00536dfe     51                           PUSH ECX
00536dff     88 54 24 36                  MOV byte ptr [ESP + 0x36],DL
00536e03     8A D3                        MOV DL,BL
00536e05     C0 E2 06                     SHL DL,0x6
00536e08     88 54 24 35                  MOV byte ptr [ESP + 0x35],DL
00536e0c     E8 3F 2D FF FF               CALL 0x00529b50
00536e11     83 C4 08                     ADD ESP,0x8
00536e14     8B F0                        MOV ESI,EAX
00536e16     8D 04 9D 00 98 5E 00         LEA EAX,[EBX*0x4 + 0x5e9800]
00536e1d     8D 4C 24 28                  LEA ECX,[ESP + 0x28]
00536e21     50                           PUSH EAX
00536e22     51                           PUSH ECX
00536e23     55                           PUSH EBP
00536e24     E8 17 B8 FF FF               CALL 0x00532640
00536e29     8B 15 C4 97 5E 00            MOV EDX,dword ptr [0x005e97c4]
00536e2f     8B 4C 24 28                  MOV ECX,dword ptr [ESP + 0x28]
00536e33     83 C4 0C                     ADD ESP,0xc
00536e36     8B 42 7C                     MOV EAX,dword ptr [EDX + 0x7c]
00536e39     56                           PUSH ESI
00536e3a     8B 14 88                     MOV EDX,dword ptr [EAX + ECX*0x4]
00536e3d     52                           PUSH EDX
00536e3e     E8 0D 2D FF FF               CALL 0x00529b50
00536e43     83 C4 08                     ADD ESP,0x8
00536e46     8B E8                        MOV EBP,EAX
00536e48     8B 44 24 10                  MOV EAX,dword ptr [ESP + 0x10]
00536e4c     50                           PUSH EAX
00536e4d     56                           PUSH ESI
00536e4e     E8 7D B8 FF FF               CALL 0x005326d0
00536e53     8B 4C 24 1C                  MOV ECX,dword ptr [ESP + 0x1c]
00536e57     83 C4 08                     ADD ESP,0x8
00536e5a     8B F9                        MOV EDI,ECX
00536e5c     88 19                        MOV byte ptr [ECX],BL
00536e5e     8B 54 24 10                  MOV EDX,dword ptr [ESP + 0x10]
00536e62     8B 44 24 18                  MOV EAX,dword ptr [ESP + 0x18]
00536e66     83 C2 04                     ADD EDX,0x4
00536e69     43                           INC EBX
00536e6a     81 C7 9C 00 00 00            ADD EDI,0x9c
00536e70     05 00 20 00 00               ADD EAX,0x2000
00536e75     81 FA 90 96 5E 00            CMP EDX,0x5e9690
00536e7b     89 54 24 10                  MOV dword ptr [ESP + 0x10],EDX
00536e7f     89 7C 24 14                  MOV dword ptr [ESP + 0x14],EDI
00536e83     89 44 24 18                  MOV dword ptr [ESP + 0x18],EAX
00536e87     0F 8C 44 FD FF FF            JL 0x00536bd1
00536e8d     89 2D 38 E7 5D 00            MOV dword ptr [0x005de738],EBP
00536e93     E8 68 E6 FF FF               CALL 0x00535500
00536e98     5F                           POP EDI
00536e99     5E                           POP ESI
00536e9a     5D                           POP EBP
00536e9b     5B                           POP EBX
00536e9c     83 C4 34                     ADD ESP,0x34
00536e9f     C3                           RET
