; Function: FUN_00544e50
; Entry:    00544e50

00544e50     8B 4C 24 08                  MOV ECX,dword ptr [ESP + 0x8]
00544e54     8A 81 C7 08 00 00            MOV AL,byte ptr [ECX + 0x8c7]
00544e5a     C6 41 33 01                  MOV byte ptr [ECX + 0x33],0x1
00544e5e     0C 02                        OR AL,0x2
00544e60     88 81 C7 08 00 00            MOV byte ptr [ECX + 0x8c7],AL
00544e66     8B 44 24 04                  MOV EAX,dword ptr [ESP + 0x4]
00544e6a     89 81 AC 08 00 00            MOV dword ptr [ECX + 0x8ac],EAX
00544e70     C3                           RET
