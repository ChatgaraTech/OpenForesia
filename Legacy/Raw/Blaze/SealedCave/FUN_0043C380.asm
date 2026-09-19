Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043c380  section .text  offset fichier 0x3b780  288 octets

  0043c380  53                    push      ebx
  0043c381  56                    push      esi
  0043c382  8b74240c              mov       esi,[esp+0Ch]
  0043c386  33db                  xor       ebx,ebx
  0043c388  b801000000            mov       eax,1
  0043c38d  66395e10              cmp       [esi+10h],bx
  0043c391  7510                  jne       short 0043C3A3h
  0043c393  8b4e6c                mov       ecx,[esi+6Ch]
  0043c396  66894610              mov       [esi+10h],ax
  0043c39a  66c746161400          mov       word [esi+16h],14h
  0043c3a0  894e74                mov       [esi+74h],ecx

LAB_0043c3a3:
  0043c3a3  66394610              cmp       [esi+10h],ax
  0043c3a7  0f8589000000          jne       near 0043C436h
  0043c3ad  66ff4e16              dec       word [esi+16h]
  0043c3b1  66395e16              cmp       [esi+16h],bx
  0043c3b5  7f06                  jg        short 0043C3BDh
  0043c3b7  66c746100200          mov       word [esi+10h],2

LAB_0043c3bd:
  0043c3bd  8b566c                mov       edx,[esi+6Ch]
  0043c3c0  c646043f              mov       byte [esi+4],3Fh
  0043c3c4  c6460a02              mov       byte [esi+0Ah],2
  0043c3c8  885e0b                mov       [esi+0Bh],bl
  0043c3cb  895670                mov       [esi+70h],edx
  0043c3ce  895e44                mov       [esi+44h],ebx
  0043c3d1  c7464800000500        mov       dword [esi+48h],50000h
  0043c3d8  895e4c                mov       [esi+4Ch],ebx
  0043c3db  e860ad1000            call      00547140h
  0043c3e0  99                    cdq
  0043c3e1  33c2                  xor       eax,edx
  0043c3e3  2bc2                  sub       eax,edx
  0043c3e5  25ff030000            and       eax,3FFh
  0043c3ea  33c2                  xor       eax,edx
  0043c3ec  2bc2                  sub       eax,edx
  0043c3ee  0500040000            add       eax,400h
  0043c3f3  66894632              mov       [esi+32h],ax
  0043c3f7  e844ad1000            call      00547140h
  0043c3fc  99                    cdq
  0043c3fd  33c2                  xor       eax,edx
  0043c3ff  6aff                  push      0FFFFFFFFh
  0043c401  2bc2                  sub       eax,edx
  0043c403  53                    push      ebx
  0043c404  25ff0f0000            and       eax,0FFFh
  0043c409  6a70                  push      70h
  0043c40b  33c2                  xor       eax,edx
  0043c40d  56                    push      esi
  0043c40e  2bc2                  sub       eax,edx
  0043c410  66895e14              mov       [esi+14h],bx
  0043c414  66894634              mov       [esi+34h],ax
  0043c418  66c74612c800          mov       word [esi+12h],0C8h
  0043c41e  66c74628ff1f          mov       word [esi+28h],1FFFh
  0043c424  66895e2a              mov       [esi+2Ah],bx
  0043c428  66c7462c0001          mov       word [esi+2Ch],100h
  0043c42e  e8ade60300            call      0047AAE0h
  0043c433  83c410                add       esp,10h

LAB_0043c436:
  0043c436  66837e1002            cmp       word [esi+10h],2
  0043c43b  7540                  jne       short 0043C47Dh
  0043c43d  6aff                  push      0FFFFFFFFh
  0043c43f  53                    push      ebx
  0043c440  68df000000            push      0DFh
  0043c445  56                    push      esi
  0043c446  66c746100300          mov       word [esi+10h],3
  0043c44c  66c746161e00          mov       word [esi+16h],1Eh
  0043c452  66c7869a000000c800    mov       word [esi+9Ah],0C8h
  0043c45b  e880e60300            call      0047AAE0h
  0043c460  83c410                add       esp,10h
  0043c463  66c7869a00000038ff    mov       word [esi+9Ah],0FF38h
  0043c46c  6aff                  push      0FFFFFFFFh
  0043c46e  53                    push      ebx
  0043c46f  68df000000            push      0DFh
  0043c474  56                    push      esi
  0043c475  e866e60300            call      0047AAE0h
  0043c47a  83c410                add       esp,10h

LAB_0043c47d:
  0043c47d  66837e1003            cmp       word [esi+10h],3
  0043c482  7510                  jne       short 0043C494h
  0043c484  66ff4e16              dec       word [esi+16h]
  0043c488  66395e16              cmp       [esi+16h],bx
  0043c48c  7d06                  jge       short 0043C494h
  0043c48e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043c494:
  0043c494  5e                    pop       esi
  0043c495  5b                    pop       ebx
  0043c496  c3                    ret
  0043c497  90                    nop
  0043c498  90                    nop
  0043c499  90                    nop
  0043c49a  90                    nop
  0043c49b  90                    nop
  0043c49c  90                    nop
  0043c49d  90                    nop
  0043c49e  90                    nop
  0043c49f  90                    nop

5 cible(s) de saut interne : 0x0043c3a3, 0x0043c3bd, 0x0043c436, 0x0043c47d, 0x0043c494
