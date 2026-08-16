; Function: FUN_0053dea0
; Entry:    0053dea0

0053dea0     8B 4C 24 04                  MOV ECX,dword ptr [ESP + 0x4]
0053dea4     53                           PUSH EBX
0053dea5     33 DB                        XOR EBX,EBX
0053dea7     8A 41 08                     MOV AL,byte ptr [ECX + 0x8]
0053deaa     A2 40 E5 5D 00               MOV [0x005de540],AL
0053deaf     8A 51 08                     MOV DL,byte ptr [ECX + 0x8]
0053deb2     88 15 28 FF 5D 00            MOV byte ptr [0x005dff28],DL
0053deb8     8B 15 2C E0 5D 00            MOV EDX,dword ptr [0x005de02c]
0053debe     83 CA 40                     OR EDX,0x40
0053dec1     33 C0                        XOR EAX,EAX
0053dec3     89 15 2C E0 5D 00            MOV dword ptr [0x005de02c],EDX
0053dec9     33 D2                        XOR EDX,EDX
0053decb     A2 48 E5 5D 00               MOV [0x005de548],AL
0053ded0     A3 5C FF 5D 00               MOV [0x005dff5c],EAX
0053ded5     A3 60 FF 5D 00               MOV [0x005dff60],EAX
0053deda     8A 51 08                     MOV DL,byte ptr [ECX + 0x8]
0053dedd     8B 0D B4 87 60 00            MOV ECX,dword ptr [0x006087b4]
0053dee3     C1 E2 0D                     SHL EDX,0xd
0053dee6     8B 09                        MOV ECX,dword ptr [ECX]
0053dee8     8A 9C 0A BC 02 0F 00         MOV BL,byte ptr [EDX + ECX*0x1 + 0xf02bc]
0053deef     A3 68 FF 5D 00               MOV [0x005dff68],EAX
0053def4     89 1D 64 FF 5D 00            MOV dword ptr [0x005dff64],EBX
0053defa     A3 6C FF 5D 00               MOV [0x005dff6c],EAX
0053deff     A3 70 FF 5D 00               MOV [0x005dff70],EAX
0053df04     A3 74 FF 5D 00               MOV [0x005dff74],EAX
0053df09     C7 05 90 DC 56 00 FF FF FF FF MOV dword ptr [0x0056dc90],0xffffffff
0053df13     A3 78 FF 5D 00               MOV [0x005dff78],EAX
0053df18     A3 7C FF 5D 00               MOV [0x005dff7c],EAX
0053df1d     5B                           POP EBX
0053df1e     C3                           RET
