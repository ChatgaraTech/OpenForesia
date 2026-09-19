Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046c800  section .text  offset fichier 0x6bc00  48 octets

  0046c800  56                    push      esi
  0046c801  8b742408              mov       esi,[esp+8]
  0046c805  66837e1000            cmp       word [esi+10h],0
  0046c80a  7518                  jne       short 0046C824h
  0046c80c  6aff                  push      0FFFFFFFFh
  0046c80e  6a00                  push      0
  0046c810  68fd000000            push      0FDh
  0046c815  56                    push      esi
  0046c816  e8c5e20000            call      0047AAE0h
  0046c81b  83c410                add       esp,10h
  0046c81e  66c746100100          mov       word [esi+10h],1

LAB_0046c824:
  0046c824  5e                    pop       esi
  0046c825  c3                    ret
  0046c826  90                    nop
  0046c827  90                    nop
  0046c828  90                    nop
  0046c829  90                    nop
  0046c82a  90                    nop
  0046c82b  90                    nop
  0046c82c  90                    nop
  0046c82d  90                    nop
  0046c82e  90                    nop
  0046c82f  90                    nop

1 cible(s) de saut interne : 0x0046c824
