Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e2c0  section .text  offset fichier 0x6d6c0  160 octets

  0046e2c0  56                    push      esi
  0046e2c1  8b742408              mov       esi,[esp+8]
  0046e2c5  66837e1000            cmp       word [esi+10h],0
  0046e2ca  750c                  jne       short 0046E2D8h
  0046e2cc  66c746120000          mov       word [esi+12h],0
  0046e2d2  66c746100100          mov       word [esi+10h],1

LAB_0046e2d8:
  0046e2d8  66837e1001            cmp       word [esi+10h],1
  0046e2dd  7579                  jne       short 0046E358h
  0046e2df  66ff4612              inc       word [esi+12h]
  0046e2e3  e8588e0d00            call      00547140h
  0046e2e8  99                    cdq
  0046e2e9  b9c8000000            mov       ecx,0C8h
  0046e2ee  f7f9                  idiv      ecx
  0046e2f0  6683fa01              cmp       dx,1
  0046e2f4  7508                  jne       short 0046E2FEh
  0046e2f6  66817e129600          cmp       word [esi+12h],96h
  0046e2fc  7f08                  jg        short 0046E306h

LAB_0046e2fe:
  0046e2fe  66817e12fa00          cmp       word [esi+12h],0FAh
  0046e304  7e52                  jle       short 0046E358h

LAB_0046e306:
  0046e306  66c746120000          mov       word [esi+12h],0
  0046e30c  e82f8e0d00            call      00547140h
  0046e311  99                    cdq
  0046e312  b9c8000000            mov       ecx,0C8h
  0046e317  f7f9                  idiv      ecx
  0046e319  8bf2                  mov       esi,edx
  0046e31b  81c6f4010000          add       esi,1F4h
  0046e321  e81a8e0d00            call      00547140h
  0046e326  99                    cdq
  0046e327  b9d0070000            mov       ecx,7D0h
  0046e32c  f7f9                  idiv      ecx
  0046e32e  81c2c4090000          add       edx,9C4h
  0046e334  0fbfc2                movsx     eax,dx
  0046e337  50                    push      eax
  0046e338  50                    push      eax
  0046e339  a1cc975e00            mov       eax,[5E97CCh]
  0046e33e  0fbfd6                movsx     edx,si
  0046e341  8b88a8000000          mov       ecx,[eax+0A8h]
  0046e347  52                    push      edx
  0046e348  33c0                  xor       eax,eax
  0046e34a  8b11                  mov       edx,[ecx]
  0046e34c  8a4219                mov       al,[edx+19h]
  0046e34f  50                    push      eax
  0046e350  e84beb0c00            call      0053CEA0h
  0046e355  83c410                add       esp,10h

LAB_0046e358:
  0046e358  5e                    pop       esi
  0046e359  c3                    ret
  0046e35a  90                    nop
  0046e35b  90                    nop
  0046e35c  90                    nop
  0046e35d  90                    nop
  0046e35e  90                    nop
  0046e35f  90                    nop

4 cible(s) de saut interne : 0x0046e2d8, 0x0046e2fe, 0x0046e306, 0x0046e358
