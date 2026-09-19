Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468810  section .text  offset fichier 0x67c10  64 octets

  00468810  e82be90d00            call      00547140h
  00468815  99                    cdq
  00468816  b91e000000            mov       ecx,1Eh
  0046881b  f7f9                  idiv      ecx
  0046881d  6683fa01              cmp       dx,1
  00468821  7523                  jne       short 00468846h
  00468823  57                    push      edi
  00468824  56                    push      esi
  00468825  8b74240c              mov       esi,[esp+0Ch]
  00468829  bf09000000            mov       edi,9

LAB_0046882e:
  0046882e  6a00                  push      0
  00468830  6a00                  push      0
  00468832  6aff                  push      0FFFFFFFFh
  00468834  6a00                  push      0
  00468836  6a33                  push      33h
  00468838  56                    push      esi
  00468839  e8d2f8ffff            call      00468110h
  0046883e  83c418                add       esp,18h
  00468841  4f                    dec       edi
  00468842  75ea                  jne       short 0046882Eh
  00468844  5e                    pop       esi
  00468845  5f                    pop       edi

LAB_00468846:
  00468846  c3                    ret
  00468847  90                    nop
  00468848  90                    nop
  00468849  90                    nop
  0046884a  90                    nop
  0046884b  90                    nop
  0046884c  90                    nop
  0046884d  90                    nop
  0046884e  90                    nop
  0046884f  90                    nop

2 cible(s) de saut interne : 0x0046882e, 0x00468846
