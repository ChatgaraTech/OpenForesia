Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469930  section .text  offset fichier 0x68d30  192 octets

  00469930  a12c185700            mov       eax,[57182Ch]
  00469935  56                    push      esi
  00469936  8b742408              mov       esi,[esp+8]
  0046993a  8a4e14                mov       cl,[esi+14h]
  0046993d  d3e8                  shr       eax,cl
  0046993f  a801                  test      al,1
  00469941  0f849c000000          je        near 004699E3h
  00469947  66837e1000            cmp       word [esi+10h],0
  0046994c  755c                  jne       short 004699AAh
  0046994e  66c746100100          mov       word [esi+10h],1
  00469954  66c746120000          mov       word [esi+12h],0
  0046995a  e8e1d70d00            call      00547140h
  0046995f  99                    cdq
  00469960  b90a000000            mov       ecx,0Ah
  00469965  f7f9                  idiv      ecx
  00469967  83c208                add       edx,8
  0046996a  89565c                mov       [esi+5Ch],edx
  0046996d  e8ced70d00            call      00547140h
  00469972  99                    cdq
  00469973  b903000000            mov       ecx,3
  00469978  6a00                  push      0
  0046997a  f7f9                  idiv      ecx
  0046997c  6a00                  push      0
  0046997e  6800100000            push      1000h
  00469983  6800020000            push      200h
  00469988  4a                    dec       edx
  00469989  66895616              mov       [esi+16h],dx
  0046998d  8b15cc975e00          mov       edx,[5E97CCh]
  00469993  8b82a8000000          mov       eax,[edx+0A8h]
  00469999  8b08                  mov       ecx,[eax]
  0046999b  660fb6512c            movzx     dx,byte [ecx+2Ch]
  004699a0  52                    push      edx
  004699a1  56                    push      esi
  004699a2  e8d9e6ffff            call      00468080h
  004699a7  83c418                add       esp,18h

LAB_004699aa:
  004699aa  66837e1001            cmp       word [esi+10h],1
  004699af  7532                  jne       short 004699E3h
  004699b1  66ff4612              inc       word [esi+12h]
  004699b5  8b4e20                mov       ecx,[esi+20h]
  004699b8  0fbf4612              movsx     eax,word [esi+12h]
  004699bc  668b5616              mov       dx,[esi+16h]
  004699c0  66015632              add       [esi+32h],dx
  004699c4  c1e00c                shl       eax,0Ch
  004699c7  2bc8                  sub       ecx,eax
  004699c9  894e20                mov       [esi+20h],ecx
  004699cc  8bc1                  mov       eax,ecx
  004699ce  668b4e5c              mov       cx,[esi+5Ch]
  004699d2  66014e34              add       [esi+34h],cx
  004699d6  3d004025ff            cmp       eax,0FF254000h
  004699db  7f06                  jg        short 004699E3h
  004699dd  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004699e3:
  004699e3  5e                    pop       esi
  004699e4  c3                    ret
  004699e5  90                    nop
  004699e6  90                    nop
  004699e7  90                    nop
  004699e8  90                    nop
  004699e9  90                    nop
  004699ea  90                    nop
  004699eb  90                    nop
  004699ec  90                    nop
  004699ed  90                    nop
  004699ee  90                    nop
  004699ef  90                    nop

2 cible(s) de saut interne : 0x004699aa, 0x004699e3
