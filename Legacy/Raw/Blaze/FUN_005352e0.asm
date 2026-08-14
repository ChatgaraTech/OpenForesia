; Function: FUN_005352e0
; Entry:    005352e0
; Body:     [[005352e0, 005352ff]]

; --------------------------------------------------
; Range: 005352e0 - 005352ff
; --------------------------------------------------
005352e0     8B 54 24 0C              MOV        EDX,dword ptr [ESP + 0xc]
005352e4     85 D2                    TEST       EDX,EDX
005352e6     74 17                    JZ         0x005352ff
005352e8     8B 4C 24 08              MOV        ECX,dword ptr [ESP + 0x8]
005352ec     8B 44 24 04              MOV        EAX,dword ptr [ESP + 0x4]
005352f0     56                       PUSH       ESI
005352f1     8B 30                    MOV        ESI,dword ptr [EAX]
005352f3     83 C0 04                 ADD        EAX,0x4
005352f6     89 31                    MOV        dword ptr [ECX],ESI
005352f8     83 C1 04                 ADD        ECX,0x4
005352fb     4A                       DEC        EDX
005352fc     75 F3                    JNZ        0x005352f1
005352fe     5E                       POP        ESI
005352ff     C3                       RET        

