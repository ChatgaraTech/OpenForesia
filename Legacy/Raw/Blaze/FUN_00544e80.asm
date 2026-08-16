; Function: FUN_00544e80
; Entry:    00544e80

00544e80     8B 44 24 08                  MOV EAX,dword ptr [ESP + 0x8]
00544e84     53                           PUSH EBX
00544e85     8B 88 B0 08 00 00            MOV ECX,dword ptr [EAX + 0x8b0]
00544e8b     85 C9                        TEST ECX,ECX
00544e8d     74 2D                        JZ 0x00544ebc
00544e8f     8B 11                        MOV EDX,dword ptr [ECX]
00544e91     81 E2 00 00 FC 00            AND EDX,0xfc0000
00544e97     81 FA 00 00 80 00            CMP EDX,0x800000
00544e9d     75 1D                        JNZ 0x00544ebc
00544e9f     33 D2                        XOR EDX,EDX
00544ea1     8A 51 08                     MOV DL,byte ptr [ECX + 0x8]
00544ea4     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
00544eaa     C1 E2 0D                     SHL EDX,0xd
00544ead     8B 09                        MOV ECX,dword ptr [ECX]
00544eaf     8A 94 0A 53 01 0F 00         MOV DL,byte ptr [EDX + ECX*0x1 + 0xf0153]
00544eb6     88 54 24 0C                  MOV byte ptr [ESP + 0xc],DL
00544eba     EB 05                        JMP 0x00544ec1
00544ebc     C6 44 24 0C 00               MOV byte ptr [ESP + 0xc],0x0
00544ec1     8B 4C 24 08                  MOV ECX,dword ptr [ESP + 0x8]
00544ec5     33 DB                        XOR EBX,EBX
00544ec7     8A 58 15                     MOV BL,byte ptr [EAX + 0x15]
00544eca     8D 51 03                     LEA EDX,[ECX + 0x3]
00544ecd     89 94 98 B4 08 00 00         MOV dword ptr [EAX + EBX*0x4 + 0x8b4],EDX
00544ed4     8A 58 15                     MOV BL,byte ptr [EAX + 0x15]
00544ed7     8B 54 24 0C                  MOV EDX,dword ptr [ESP + 0xc]
00544edb     FE C3                        INC BL
00544edd     88 58 15                     MOV byte ptr [EAX + 0x15],BL
00544ee0     33 C0                        XOR EAX,EAX
00544ee2     8A 61 02                     MOV AH,byte ptr [ECX + 0x2]
00544ee5     81 E2 FF 00 00 00            AND EDX,0xff
00544eeb     8A 41 01                     MOV AL,byte ptr [ECX + 0x1]
00544eee     5B                           POP EBX
00544eef     03 C2                        ADD EAX,EDX
00544ef1     33 D2                        XOR EDX,EDX
00544ef3     8A 11                        MOV DL,byte ptr [ECX]
00544ef5     8B 0C 95 C0 97 5E 00         MOV ECX,dword ptr [EDX*0x4 + 0x5e97c0]
00544efc     8B 51 70                     MOV EDX,dword ptr [ECX + 0x70]
00544eff     8B 04 82                     MOV EAX,dword ptr [EDX + EAX*0x4]
00544f02     C3                           RET
