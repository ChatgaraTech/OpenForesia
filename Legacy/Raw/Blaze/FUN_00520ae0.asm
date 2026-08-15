; Function: FUN_00520ae0
; Entry:    00520ae0

00520ae0     83 EC 18                 SUB ESP,0x18
00520ae3     53                       PUSH EBX
00520ae4     55                       PUSH EBP
00520ae5     56                       PUSH ESI
00520ae6     57                       PUSH EDI
00520ae7     8B 7C 24 34              MOV EDI,dword ptr [ESP + 0x34]
00520aeb     8B 47 20                 MOV EAX,dword ptr [EDI + 0x20]
00520aee     85 C0                    TEST EAX,EAX
00520af0     89 44 24 18              MOV dword ptr [ESP + 0x18],EAX
00520af4     75 0C                    JNZ 0x00520b02
00520af6     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
00520afa     5F                       POP EDI
00520afb     5E                       POP ESI
00520afc     5D                       POP EBP
00520afd     5B                       POP EBX
00520afe     83 C4 18                 ADD ESP,0x18
00520b01     C3                       RET
00520b02     8B 70 04                 MOV ESI,dword ptr [EAX + 0x4]
00520b05     A1 48 4A 56 00           MOV EAX,[0x00564a48]
00520b0a     85 C0                    TEST EAX,EAX
00520b0c     74 23                    JZ 0x00520b31
00520b0e     F7 C6 00 02 00 00        TEST ESI,0x200
00520b14     74 1B                    JZ 0x00520b31
00520b16     A1 4C 4A 56 00           MOV EAX,[0x00564a4c]
00520b1b     85 C0                    TEST EAX,EAX
00520b1d     75 0C                    JNZ 0x00520b2b
00520b1f     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
00520b23     5F                       POP EDI
00520b24     5E                       POP ESI
00520b25     5D                       POP EBP
00520b26     5B                       POP EBX
00520b27     83 C4 18                 ADD ESP,0x18
00520b2a     C3                       RET
00520b2b     81 E6 FF FD FF FF        AND ESI,0xfffffdff
00520b31     68 E0 08 60 00           PUSH 0x6008e0
00520b36     E8 8F 4F 02 00           CALL 0x00545aca
00520b3b     68 E0 08 60 00           PUSH 0x6008e0
00520b40     E8 4B 50 02 00           CALL 0x00545b90
00520b45     8B 07                    MOV EAX,dword ptr [EDI]
00520b47     8B 15 D0 08 60 00        MOV EDX,dword ptr [0x006008d0]
00520b4d     8B 0D D0 86 60 00        MOV ECX,dword ptr [0x006086d0]
00520b53     03 C2                    ADD EAX,EDX
00520b55     C1 F8 0C                 SAR EAX,0xc
00520b58     66 89 01                 MOV word ptr [ECX],AX
00520b5b     8B 15 D4 08 60 00        MOV EDX,dword ptr [0x006008d4]
00520b61     8B 6F 04                 MOV EBP,dword ptr [EDI + 0x4]
00520b64     A1 D0 86 60 00           MOV EAX,[0x006086d0]
00520b69     2B D5                    SUB EDX,EBP
00520b6b     C1 FA 0C                 SAR EDX,0xc
00520b6e     66 89 50 02              MOV word ptr [EAX + 0x2],DX
00520b72     8B 4F 08                 MOV ECX,dword ptr [EDI + 0x8]
00520b75     8B 15 D8 08 60 00        MOV EDX,dword ptr [0x006008d8]
00520b7b     03 CA                    ADD ECX,EDX
00520b7d     8B 15 D0 86 60 00        MOV EDX,dword ptr [0x006086d0]
00520b83     C1 F9 0C                 SAR ECX,0xc
00520b86     66 89 4A 04              MOV word ptr [EDX + 0x4],CX
00520b8a     8B 0D D0 86 60 00        MOV ECX,dword ptr [0x006086d0]
00520b90     A1 10 88 60 00           MOV EAX,[0x00608810]
00520b95     0F BF 51 02              MOVSX EDX,word ptr [ECX + 0x2]
00520b99     0F BF 58 02              MOVSX EBX,word ptr [EAX + 0x2]
00520b9d     0F BF 68 04              MOVSX EBP,word ptr [EAX + 0x4]
00520ba1     0F AF D3                 IMUL EDX,EBX
00520ba4     0F BF 59 04              MOVSX EBX,word ptr [ECX + 0x4]
00520ba8     0F AF DD                 IMUL EBX,EBP
00520bab     0F BF 09                 MOVSX ECX,word ptr [ECX]
00520bae     03 D3                    ADD EDX,EBX
00520bb0     0F BF 18                 MOVSX EBX,word ptr [EAX]
00520bb3     0F AF D9                 IMUL EBX,ECX
00520bb6     8B 48 14                 MOV ECX,dword ptr [EAX + 0x14]
00520bb9     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520bbe     03 D3                    ADD EDX,EBX
00520bc0     C1 FA 0C                 SAR EDX,0xc
00520bc3     03 D1                    ADD EDX,ECX
00520bc5     89 10                    MOV dword ptr [EAX],EDX
00520bc7     8B 0D D0 86 60 00        MOV ECX,dword ptr [0x006086d0]
00520bcd     A1 10 88 60 00           MOV EAX,[0x00608810]
00520bd2     0F BF 51 02              MOVSX EDX,word ptr [ECX + 0x2]
00520bd6     0F BF 58 08              MOVSX EBX,word ptr [EAX + 0x8]
00520bda     0F BF 68 0A              MOVSX EBP,word ptr [EAX + 0xa]
00520bde     0F AF D3                 IMUL EDX,EBX
00520be1     0F BF 59 04              MOVSX EBX,word ptr [ECX + 0x4]
00520be5     0F AF DD                 IMUL EBX,EBP
00520be8     0F BF 09                 MOVSX ECX,word ptr [ECX]
00520beb     03 D3                    ADD EDX,EBX
00520bed     0F BF 58 06              MOVSX EBX,word ptr [EAX + 0x6]
00520bf1     0F AF D9                 IMUL EBX,ECX
00520bf4     8B 48 18                 MOV ECX,dword ptr [EAX + 0x18]
00520bf7     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520bfc     03 D3                    ADD EDX,EBX
00520bfe     C1 FA 0C                 SAR EDX,0xc
00520c01     03 D1                    ADD EDX,ECX
00520c03     89 50 04                 MOV dword ptr [EAX + 0x4],EDX
00520c06     8B 0D D0 86 60 00        MOV ECX,dword ptr [0x006086d0]
00520c0c     A1 10 88 60 00           MOV EAX,[0x00608810]
00520c11     0F BF 51 02              MOVSX EDX,word ptr [ECX + 0x2]
00520c15     0F BF 58 0E              MOVSX EBX,word ptr [EAX + 0xe]
00520c19     0F BF 68 10              MOVSX EBP,word ptr [EAX + 0x10]
00520c1d     0F AF D3                 IMUL EDX,EBX
00520c20     0F BF 59 04              MOVSX EBX,word ptr [ECX + 0x4]
00520c24     0F AF DD                 IMUL EBX,EBP
00520c27     0F BF 09                 MOVSX ECX,word ptr [ECX]
00520c2a     03 D3                    ADD EDX,EBX
00520c2c     0F BF 58 0C              MOVSX EBX,word ptr [EAX + 0xc]
00520c30     0F AF D9                 IMUL EBX,ECX
00520c33     03 D3                    ADD EDX,EBX
00520c35     8B 58 1C                 MOV EBX,dword ptr [EAX + 0x1c]
00520c38     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520c3d     C1 FA 0C                 SAR EDX,0xc
00520c40     03 D3                    ADD EDX,EBX
00520c42     89 50 08                 MOV dword ptr [EAX + 0x8],EDX
00520c45     8B 0D D4 86 60 00        MOV ECX,dword ptr [0x006086d4]
00520c4b     A1 D8 86 60 00           MOV EAX,[0x006086d8]
00520c50     66 8B 11                 MOV DX,word ptr [ECX]
00520c53     66 89 10                 MOV word ptr [EAX],DX
00520c56     8B 0D D4 86 60 00        MOV ECX,dword ptr [0x006086d4]
00520c5c     A1 D8 86 60 00           MOV EAX,[0x006086d8]
00520c61     66 8B 51 04              MOV DX,word ptr [ECX + 0x4]
00520c65     66 89 50 02              MOV word ptr [EAX + 0x2],DX
00520c69     8B 0D D4 86 60 00        MOV ECX,dword ptr [0x006086d4]
00520c6f     A1 D8 86 60 00           MOV EAX,[0x006086d8]
00520c74     66 8B 51 08              MOV DX,word ptr [ECX + 0x8]
00520c78     66 89 50 04              MOV word ptr [EAX + 0x4],DX
00520c7c     8B 15 DC 86 60 00        MOV EDX,dword ptr [0x006086dc]
00520c82     0F BF 4F 1A              MOVSX ECX,word ptr [EDI + 0x1a]
00520c86     33 C0                    XOR EAX,EAX
00520c88     89 4C 24 10              MOV dword ptr [ESP + 0x10],ECX
00520c8c     66 89 02                 MOV word ptr [EDX],AX
00520c8f     8B 0D DC 86 60 00        MOV ECX,dword ptr [0x006086dc]
00520c95     66 89 41 02              MOV word ptr [ECX + 0x2],AX
00520c99     8B 15 DC 86 60 00        MOV EDX,dword ptr [0x006086dc]
00520c9f     66 89 42 04              MOV word ptr [EDX + 0x4],AX
00520ca3     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520ca8     8B 15 10 88 60 00        MOV EDX,dword ptr [0x00608810]
00520cae     8B 08                    MOV ECX,dword ptr [EAX]
00520cb0     89 4A 14                 MOV dword ptr [EDX + 0x14],ECX
00520cb3     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520cb8     8B 15 10 88 60 00        MOV EDX,dword ptr [0x00608810]
00520cbe     8B 48 04                 MOV ECX,dword ptr [EAX + 0x4]
00520cc1     89 4A 18                 MOV dword ptr [EDX + 0x18],ECX
00520cc4     A1 D4 86 60 00           MOV EAX,[0x006086d4]
00520cc9     8B 15 10 88 60 00        MOV EDX,dword ptr [0x00608810]
00520ccf     8B 48 08                 MOV ECX,dword ptr [EAX + 0x8]
00520cd2     89 4A 1C                 MOV dword ptr [EDX + 0x1c],ECX
00520cd5     A1 DC 86 60 00           MOV EAX,[0x006086dc]
00520cda     0F BF 58 02              MOVSX EBX,word ptr [EAX + 0x2]
00520cde     0F BF 78 04              MOVSX EDI,word ptr [EAX + 0x4]
00520ce2     0F BF 28                 MOVSX EBP,word ptr [EAX]
00520ce5     A1 10 88 60 00           MOV EAX,[0x00608810]
00520cea     8B CD                    MOV ECX,EBP
00520cec     89 7C 24 14              MOV dword ptr [ESP + 0x14],EDI
00520cf0     0F BF 10                 MOVSX EDX,word ptr [EAX]
00520cf3     0F AF CA                 IMUL ECX,EDX
00520cf6     0F BF 50 04              MOVSX EDX,word ptr [EAX + 0x4]
00520cfa     0F AF D7                 IMUL EDX,EDI
00520cfd     03 CA                    ADD ECX,EDX
00520cff     0F BF 50 02              MOVSX EDX,word ptr [EAX + 0x2]
00520d03     0F AF D3                 IMUL EDX,EBX
00520d06     03 CA                    ADD ECX,EDX
00520d08     0F BF 50 0A              MOVSX EDX,word ptr [EAX + 0xa]
00520d0c     0F AF D7                 IMUL EDX,EDI
00520d0f     0F BF 78 06              MOVSX EDI,word ptr [EAX + 0x6]
00520d13     0F AF FD                 IMUL EDI,EBP
00520d16     03 D7                    ADD EDX,EDI
00520d18     0F BF 78 08              MOVSX EDI,word ptr [EAX + 0x8]
00520d1c     0F AF FB                 IMUL EDI,EBX
00520d1f     03 D7                    ADD EDX,EDI
00520d21     0F BF 78 0E              MOVSX EDI,word ptr [EAX + 0xe]
00520d25     0F AF FB                 IMUL EDI,EBX
00520d28     0F BF 58 0C              MOVSX EBX,word ptr [EAX + 0xc]
00520d2c     0F AF DD                 IMUL EBX,EBP
00520d2f     03 FB                    ADD EDI,EBX
00520d31     0F BF 58 10              MOVSX EBX,word ptr [EAX + 0x10]
00520d35     0F AF 5C 24 14           IMUL EBX,dword ptr [ESP + 0x14]
00520d3a     03 FB                    ADD EDI,EBX
00520d3c     C1 FF 0C                 SAR EDI,0xc
00520d3f     66 03 78 1C              ADD DI,word ptr [EAX + 0x1c]
00520d43     C1 F9 0C                 SAR ECX,0xc
00520d46     66 03 48 14              ADD CX,word ptr [EAX + 0x14]
00520d4a     66 89 7C 24 24           MOV word ptr [ESP + 0x24],DI
00520d4f     C1 FA 0C                 SAR EDX,0xc
00520d52     66 03 50 18              ADD DX,word ptr [EAX + 0x18]
00520d56     0F BF C7                 MOVSX EAX,DI
00520d59     8B 3D 9C 87 60 00        MOV EDI,dword ptr [0x0060879c]
00520d5f     0F BF D2                 MOVSX EDX,DX
00520d62     89 07                    MOV dword ptr [EDI],EAX
00520d64     8B 44 24 24              MOV EAX,dword ptr [ESP + 0x24]
00520d68     8B 3D A4 87 60 00        MOV EDI,dword ptr [0x006087a4]
00520d6e     25 FF FF 00 00           AND EAX,0xffff
00520d73     0F BF 04 47              MOVSX EAX,word ptr [EDI + EAX*0x2]
00520d77     8B 3D 8C 87 60 00        MOV EDI,dword ptr [0x0060878c]
00520d7d     0F AF D0                 IMUL EDX,EAX
00520d80     8B 3F                    MOV EDI,dword ptr [EDI]
00520d82     81 E2 00 F0 FF FF        AND EDX,0xfffff000
00520d88     C1 E7 0C                 SHL EDI,0xc
00520d8b     03 D7                    ADD EDX,EDI
00520d8d     C1 E2 04                 SHL EDX,0x4
00520d90     0F BF C9                 MOVSX ECX,CX
00520d93     0F AF C8                 IMUL ECX,EAX
00520d96     A1 EC 86 60 00           MOV EAX,[0x006086ec]
00520d9b     C1 F9 0C                 SAR ECX,0xc
00520d9e     03 D1                    ADD EDX,ECX
00520da0     8B 08                    MOV ECX,dword ptr [EAX]
00520da2     03 D1                    ADD EDX,ECX
00520da4     8B 0D F0 86 60 00        MOV ECX,dword ptr [0x006086f0]
00520daa     89 11                    MOV dword ptr [ECX],EDX
00520dac     8B 15 9C 87 60 00        MOV EDX,dword ptr [0x0060879c]
00520db2     8B 0D F4 86 60 00        MOV ECX,dword ptr [0x006086f4]
00520db8     8B 02                    MOV EAX,dword ptr [EDX]
00520dba     C1 F8 02                 SAR EAX,0x2
00520dbd     89 01                    MOV dword ptr [ECX],EAX
00520dbf     8B 15 F0 86 60 00        MOV EDX,dword ptr [0x006086f0]
00520dc5     8B 0D 9C 87 60 00        MOV ECX,dword ptr [0x0060879c]
00520dcb     8B 02                    MOV EAX,dword ptr [EDX]
00520dcd     8B 11                    MOV EDX,dword ptr [ECX]
00520dcf     89 44 24 20              MOV dword ptr [ESP + 0x20],EAX
00520dd3     89 54 24 24              MOV dword ptr [ESP + 0x24],EDX
00520dd7     66 8B 54 24 24           MOV DX,word ptr [ESP + 0x24]
00520ddc     66 C1 FA 02              SAR DX,0x2
00520de0     0F BF EA                 MOVSX EBP,DX
00520de3     66 89 54 24 24           MOV word ptr [ESP + 0x24],DX
00520de8     8D 4D A8                 LEA ECX,[EBP + -0x58]
00520deb     81 F9 A0 03 00 00        CMP ECX,0x3a0
00520df1     72 0C                    JC 0x00520dff
00520df3     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
00520df7     5F                       POP EDI
00520df8     5E                       POP ESI
00520df9     5D                       POP EBP
00520dfa     5B                       POP EBX
00520dfb     83 C4 18                 ADD ESP,0x18
00520dfe     C3                       RET
00520dff     F6 44 24 10 01           TEST byte ptr [ESP + 0x10],0x1
00520e04     74 22                    JZ 0x00520e28
00520e06     83 C0 18                 ADD EAX,0x18
00520e09     66 3D 70 01              CMP AX,0x170
00520e0d     73 0D                    JNC 0x00520e1c
00520e0f     8B 44 24 22              MOV EAX,dword ptr [ESP + 0x22]
00520e13     83 C0 18                 ADD EAX,0x18
00520e16     66 3D 20 01              CMP AX,0x120
00520e1a     72 0C                    JC 0x00520e28
00520e1c     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
00520e20     5F                       POP EDI
00520e21     5E                       POP ESI
00520e22     5D                       POP EBP
00520e23     5B                       POP EBX
00520e24     83 C4 18                 ADD ESP,0x18
00520e27     C3                       RET
00520e28     8B 4C 24 34              MOV ECX,dword ptr [ESP + 0x34]
00520e2c     8B 7C 24 18              MOV EDI,dword ptr [ESP + 0x18]
00520e30     8B 41 1C                 MOV EAX,dword ptr [ECX + 0x1c]
00520e33     89 44 24 1C              MOV dword ptr [ESP + 0x1c],EAX
00520e37     33 C0                    XOR EAX,EAX
00520e39     8A 41 24                 MOV AL,byte ptr [ECX + 0x24]
00520e3c     8B 1C C7                 MOV EBX,dword ptr [EDI + EAX*0x8]
00520e3f     33 C0                    XOR EAX,EAX
00520e41     89 5C 24 18              MOV dword ptr [ESP + 0x18],EBX
00520e45     8B FB                    MOV EDI,EBX
00520e47     8A 44 24 1A              MOV AL,byte ptr [ESP + 0x1a]
00520e4b     C1 EF 18                 SHR EDI,0x18
00520e4e     48                       DEC EAX
00520e4f     4F                       DEC EDI
00520e50     89 44 24 10              MOV dword ptr [ESP + 0x10],EAX
00520e54     B8 00 00 19 00           MOV EAX,0x190000
00520e59     66 85 D2                 TEST DX,DX
00520e5c     74 03                    JZ 0x00520e61
00520e5e     99                       CDQ
00520e5f     F7 FD                    IDIV EBP
00520e61     0F BF 49 0C              MOVSX ECX,word ptr [ECX + 0xc]
00520e65     0F AF 4C 24 10           IMUL ECX,dword ptr [ESP + 0x10]
00520e6a     8B 54 24 34              MOV EDX,dword ptr [ESP + 0x34]
00520e6e     C1 E9 0C                 SHR ECX,0xc
00520e71     0F BF 52 0E              MOVSX EDX,word ptr [EDX + 0xe]
00520e75     0F AF C8                 IMUL ECX,EAX
00520e78     0F AF D7                 IMUL EDX,EDI
00520e7b     C1 EA 0C                 SHR EDX,0xc
00520e7e     C1 E9 0C                 SHR ECX,0xc
00520e81     0F AF D0                 IMUL EDX,EAX
00520e84     0F BF C1                 MOVSX EAX,CX
00520e87     8B CE                    MOV ECX,ESI
00520e89     89 44 24 34              MOV dword ptr [ESP + 0x34],EAX
00520e8d     C1 E9 10                 SHR ECX,0x10
00520e90     81 E1 FF 00 00 00        AND ECX,0xff
00520e96     C1 E7 08                 SHL EDI,0x8
00520e99     8B 04 8D 00 8F 5E 00     MOV EAX,dword ptr [ECX*0x4 + 0x5e8f00]
00520ea0     8B 4C 24 2C              MOV ECX,dword ptr [ESP + 0x2c]
00520ea4     89 44 24 14              MOV dword ptr [ESP + 0x14],EAX
00520ea8     8B 44 24 30              MOV EAX,dword ptr [ESP + 0x30]
00520eac     8B 4C A9 D8              MOV ECX,dword ptr [ECX + EBP*0x4 + -0x28]
00520eb0     81 C9 00 00 00 09        OR ECX,0x9000000
00520eb6     89 08                    MOV dword ptr [EAX],ECX
00520eb8     8B 4C 24 2C              MOV ECX,dword ptr [ESP + 0x2c]
00520ebc     C1 EA 0C                 SHR EDX,0xc
00520ebf     89 44 A9 D8              MOV dword ptr [ECX + EBP*0x4 + -0x28],EAX
00520ec3     8B 6C 24 38              MOV EBP,dword ptr [ESP + 0x38]
00520ec7     81 E5 FF 00 00 00        AND EBP,0xff
00520ecd     8B CE                    MOV ECX,ESI
00520ecf     83 CD 2C                 OR EBP,0x2c
00520ed2     80 E1 00                 AND CL,0x0
00520ed5     C1 E5 08                 SHL EBP,0x8
00520ed8     0B E9                    OR EBP,ECX
00520eda     8B 4C 24 1C              MOV ECX,dword ptr [ESP + 0x1c]
00520ede     C1 E5 10                 SHL EBP,0x10
00520ee1     81 E1 FF FF FF 03        AND ECX,0x3ffffff
00520ee7     0B E9                    OR EBP,ECX
00520ee9     8B 4C 24 10              MOV ECX,dword ptr [ESP + 0x10]
00520eed     89 68 04                 MOV dword ptr [EAX + 0x4],EBP
00520ef0     8B EB                    MOV EBP,EBX
00520ef2     81 E5 FF 00 00 00        AND EBP,0xff
00520ef8     03 CD                    ADD ECX,EBP
00520efa     89 4C 24 2C              MOV dword ptr [ESP + 0x2c],ECX
00520efe     8B CB                    MOV ECX,EBX
00520f00     81 E1 00 FF 00 00        AND ECX,0xff00
00520f06     03 F9                    ADD EDI,ECX
00520f08     89 4C 24 38              MOV dword ptr [ESP + 0x38],ECX
00520f0c     8B 4C 24 20              MOV ECX,dword ptr [ESP + 0x20]
00520f10     89 7C 24 18              MOV dword ptr [ESP + 0x18],EDI
00520f14     8B 7C 24 34              MOV EDI,dword ptr [ESP + 0x34]
00520f18     D1 EF                    SHR EDI,0x1
00520f1a     2B CF                    SUB ECX,EDI
00520f1c     8B 7C 24 34              MOV EDI,dword ptr [ESP + 0x34]
00520f20     81 E1 FF FF 00 00        AND ECX,0xffff
00520f26     89 4C 24 30              MOV dword ptr [ESP + 0x30],ECX
00520f2a     03 CF                    ADD ECX,EDI
00520f2c     0F BF D2                 MOVSX EDX,DX
00520f2f     81 E1 FF FF 00 00        AND ECX,0xffff
00520f35     8B FA                    MOV EDI,EDX
00520f37     89 4C 24 34              MOV dword ptr [ESP + 0x34],ECX
00520f3b     0F BF 4C 24 22           MOVSX ECX,word ptr [ESP + 0x22]
00520f40     D1 EF                    SHR EDI,0x1
00520f42     2B CF                    SUB ECX,EDI
00520f44     8B 7C 24 30              MOV EDI,dword ptr [ESP + 0x30]
00520f48     C1 E1 10                 SHL ECX,0x10
00520f4b     C1 E2 10                 SHL EDX,0x10
00520f4e     03 D1                    ADD EDX,ECX
00520f50     89 54 24 1C              MOV dword ptr [ESP + 0x1c],EDX
00520f54     8B D1                    MOV EDX,ECX
00520f56     0B D7                    OR EDX,EDI
00520f58     89 50 08                 MOV dword ptr [EAX + 0x8],EDX
00520f5b     8B 54 24 3C              MOV EDX,dword ptr [ESP + 0x3c]
00520f5f     66 8B FA                 MOV DI,DX
00520f62     81 E2 FF 00 00 00        AND EDX,0xff
00520f68     66 C1 FF 02              SAR DI,0x2
00520f6c     81 E7 C0 FF 00 00        AND EDI,0xffc0
00520f72     03 FA                    ADD EDI,EDX
00520f74     8B 54 24 14              MOV EDX,dword ptr [ESP + 0x14]
00520f78     C1 E7 10                 SHL EDI,0x10
00520f7b     81 E2 00 00 FF FF        AND EDX,0xffff0000
00520f81     03 FA                    ADD EDI,EDX
00520f83     8B D6                    MOV EDX,ESI
00520f85     81 E3 FF FF 00 00        AND EBX,0xffff
00520f8b     C1 EA 08                 SHR EDX,0x8
00520f8e     81 E2 00 00 FF 00        AND EDX,0xff0000
00520f94     81 E6 FF 00 00 00        AND ESI,0xff
00520f9a     03 FA                    ADD EDI,EDX
00520f9c     0B FB                    OR EDI,EBX
00520f9e     8B 5C 24 34              MOV EBX,dword ptr [ESP + 0x34]
00520fa2     8B D3                    MOV EDX,EBX
00520fa4     89 78 0C                 MOV dword ptr [EAX + 0xc],EDI
00520fa7     8B 7C 24 2C              MOV EDI,dword ptr [ESP + 0x2c]
00520fab     0B D1                    OR EDX,ECX
00520fad     8B 4C 24 14              MOV ECX,dword ptr [ESP + 0x14]
00520fb1     89 50 10                 MOV dword ptr [EAX + 0x10],EDX
00520fb4     C1 E6 05                 SHL ESI,0x5
00520fb7     8B 54 24 1C              MOV EDX,dword ptr [ESP + 0x1c]
00520fbb     0B F1                    OR ESI,ECX
00520fbd     8B 4C 24 38              MOV ECX,dword ptr [ESP + 0x38]
00520fc1     C1 E6 10                 SHL ESI,0x10
00520fc4     0B F7                    OR ESI,EDI
00520fc6     0B F1                    OR ESI,ECX
00520fc8     8B CA                    MOV ECX,EDX
00520fca     89 70 14                 MOV dword ptr [EAX + 0x14],ESI
00520fcd     8B 74 24 30              MOV ESI,dword ptr [ESP + 0x30]
00520fd1     0B CE                    OR ECX,ESI
00520fd3     0B D3                    OR EDX,EBX
00520fd5     89 48 18                 MOV dword ptr [EAX + 0x18],ECX
00520fd8     8B 4C 24 18              MOV ECX,dword ptr [ESP + 0x18]
00520fdc     8B F1                    MOV ESI,ECX
00520fde     0B CF                    OR ECX,EDI
00520fe0     0B F5                    OR ESI,EBP
00520fe2     5F                       POP EDI
00520fe3     89 70 1C                 MOV dword ptr [EAX + 0x1c],ESI
00520fe6     5E                       POP ESI
00520fe7     89 50 20                 MOV dword ptr [EAX + 0x20],EDX
00520fea     89 48 24                 MOV dword ptr [EAX + 0x24],ECX
00520fed     5D                       POP EBP
00520fee     83 C0 28                 ADD EAX,0x28
00520ff1     5B                       POP EBX
00520ff2     83 C4 18                 ADD ESP,0x18
00520ff5     C3                       RET
