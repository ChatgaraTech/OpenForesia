; Function: FUN_0052a1e0
; Entry:    0052a1e0
; Body:     [[0052a1e0, 0052a276]]

; --------------------------------------------------
; Range: 0052a1e0 - 0052a276
; --------------------------------------------------
0052a1e0     81 3D 00 93 5E 00 80 98 5E 00 CMP        dword ptr [0x005e9300],0x5e9880
0052a1ea     75 0E                    JNZ        0x0052a1fa
0052a1ec     A1 B4 87 60 00           MOV        EAX,[0x006087b4]
0052a1f1     8B 00                    MOV        EAX,dword ptr [EAX]
0052a1f3     05 00 70 1C 00           ADD        EAX,0x1c7000
0052a1f8     EB 0D                    JMP        0x0052a207
0052a1fa     8B 0D B4 87 60 00        MOV        ECX,dword ptr [0x006087b4]
0052a200     8B 01                    MOV        EAX,dword ptr [ECX]
0052a202     05 00 60 1C 00           ADD        EAX,0x1c6000
0052a207     8B 4C 24 04              MOV        ECX,dword ptr [ESP + 0x4]
0052a20b     8B 54 24 08              MOV        EDX,dword ptr [ESP + 0x8]
0052a20f     68 00 04 00 00           PUSH       0x400
0052a214     89 01                    MOV        dword ptr [ECX],EAX
0052a216     05 FC 0F 00 00           ADD        EAX,0xffc
0052a21b     89 02                    MOV        dword ptr [EDX],EAX
0052a21d     8B 01                    MOV        EAX,dword ptr [ECX]
0052a21f     50                       PUSH       EAX
0052a220     E8 01 BA 01 00           CALL       0x00545c26
0052a225     A1 00 93 5E 00           MOV        EAX,[0x005e9300]
0052a22a     C7 05 10 83 5E 00 00 00 00 00 MOV        dword ptr [0x005e8310],0x0
0052a234     3D 80 98 5E 00           CMP        EAX,0x5e9880
0052a239     75 0F                    JNZ        0x0052a24a
0052a23b     8B 0D B4 87 60 00        MOV        ECX,dword ptr [0x006087b4]
0052a241     8B 01                    MOV        EAX,dword ptr [ECX]
0052a243     05 00 40 1E 00           ADD        EAX,0x1e4000
0052a248     EB 0D                    JMP        0x0052a257
0052a24a     8B 15 B4 87 60 00        MOV        EDX,dword ptr [0x006087b4]
0052a250     8B 02                    MOV        EAX,dword ptr [EDX]
0052a252     05 00 80 1C 00           ADD        EAX,0x1c8000
0052a257     8B 4C 24 0C              MOV        ECX,dword ptr [ESP + 0xc]
0052a25b     89 01                    MOV        dword ptr [ECX],EAX
0052a25d     8B 15 C0 97 5E 00        MOV        EDX,dword ptr [0x005e97c0]
0052a263     52                       PUSH       EDX
0052a264     C7 05 18 98 5E 00 C4 09 00 00 MOV        dword ptr [0x005e9818],0x9c4
0052a26e     E8 8D 8D 00 00           CALL       0x00533000
0052a273     83 C4 04                 ADD        ESP,0x4
0052a276     C3                       RET        

