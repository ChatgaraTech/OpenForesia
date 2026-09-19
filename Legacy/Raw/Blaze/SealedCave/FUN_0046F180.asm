Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046f180  section .text  offset fichier 0x6e580  64 octets

  0046f180  e8bb7f0d00            call      00547140h
  0046f185  99                    cdq
  0046f186  b91e000000            mov       ecx,1Eh
  0046f18b  f7f9                  idiv      ecx
  0046f18d  6683fa01              cmp       dx,1
  0046f191  7523                  jne       short 0046F1B6h
  0046f193  57                    push      edi
  0046f194  56                    push      esi
  0046f195  8b74240c              mov       esi,[esp+0Ch]
  0046f199  bf09000000            mov       edi,9

LAB_0046f19e:
  0046f19e  6a00                  push      0
  0046f1a0  6a00                  push      0
  0046f1a2  6aff                  push      0FFFFFFFFh
  0046f1a4  6a00                  push      0
  0046f1a6  6a3a                  push      3Ah
  0046f1a8  56                    push      esi
  0046f1a9  e8628fffff            call      00468110h
  0046f1ae  83c418                add       esp,18h
  0046f1b1  4f                    dec       edi
  0046f1b2  75ea                  jne       short 0046F19Eh
  0046f1b4  5e                    pop       esi
  0046f1b5  5f                    pop       edi

LAB_0046f1b6:
  0046f1b6  c3                    ret
  0046f1b7  90                    nop
  0046f1b8  90                    nop
  0046f1b9  90                    nop
  0046f1ba  90                    nop
  0046f1bb  90                    nop
  0046f1bc  90                    nop
  0046f1bd  90                    nop
  0046f1be  90                    nop
  0046f1bf  90                    nop

2 cible(s) de saut interne : 0x0046f19e, 0x0046f1b6
