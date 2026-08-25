
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00405d60  section .text  offset fichier 0x5160  64 octets

  00405d60  56                    push      esi
  00405d61  57                    push      edi
  00405d62  33ff                  xor       edi,edi
  00405d64  bea0ca5e00            mov       esi,5ECAA0h

LAB_00405d69:
  00405d69  8a460c                mov       al,[esi+0Ch]
  00405d6c  84c0                  test      al,al
  00405d6e  7516                  jne       short 00405D86h
  00405d70  8b06                  mov       eax,[esi]
  00405d72  a900000080            test      eax,80000000h
  00405d77  740d                  je        short 00405D86h
  00405d79  a808                  test      al,8
  00405d7b  7409                  je        short 00405D86h
  00405d7d  57                    push      edi
  00405d7e  e81d000000            call      00405DA0h
  00405d83  83c404                add       esp,4

LAB_00405d86:
  00405d86  47                    inc       edi
  00405d87  81c69c000000          add       esi,9Ch
  00405d8d  6683ff20              cmp       di,20h
  00405d91  7cd6                  jl        short 00405D69h
  00405d93  8b44240c              mov       eax,[esp+0Ch]
  00405d97  5f                    pop       edi
  00405d98  5e                    pop       esi
  00405d99  8120ffffff7f          and       dword [eax],7FFFFFFFh
  00405d9f  c3                    ret

2 cible(s) de saut interne : 0x00405d69, 0x00405d86
