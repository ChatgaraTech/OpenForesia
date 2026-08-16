; Function: FUN_0053df20
; Entry:    0053df20

0053df20     8B 44 24 04                  MOV EAX,dword ptr [ESP + 0x4]
0053df24     8A 48 08                     MOV CL,byte ptr [EAX + 0x8]
0053df27     88 0D 40 E5 5D 00            MOV byte ptr [0x005de540],CL
0053df2d     8A 50 08                     MOV DL,byte ptr [EAX + 0x8]
0053df30     A1 2C E0 5D 00               MOV EAX,[0x005de02c]
0053df35     88 15 28 FF 5D 00            MOV byte ptr [0x005dff28],DL
0053df3b     0C 40                        OR AL,0x40
0053df3d     C6 05 48 E5 5D 00 01         MOV byte ptr [0x005de548],0x1
0053df44     A3 2C E0 5D 00               MOV [0x005de02c],EAX
0053df49     33 C0                        XOR EAX,EAX
0053df4b     A3 5C FF 5D 00               MOV [0x005dff5c],EAX
0053df50     A3 60 FF 5D 00               MOV [0x005dff60],EAX
0053df55     A3 64 FF 5D 00               MOV [0x005dff64],EAX
0053df5a     A3 68 FF 5D 00               MOV [0x005dff68],EAX
0053df5f     A3 6C FF 5D 00               MOV [0x005dff6c],EAX
0053df64     A3 70 FF 5D 00               MOV [0x005dff70],EAX
0053df69     A3 74 FF 5D 00               MOV [0x005dff74],EAX
0053df6e     C7 05 90 DC 56 00 FF FF FF FF MOV dword ptr [0x0056dc90],0xffffffff
0053df78     A3 78 FF 5D 00               MOV [0x005dff78],EAX
0053df7d     A3 7C FF 5D 00               MOV [0x005dff7c],EAX
0053df82     C3                           RET
