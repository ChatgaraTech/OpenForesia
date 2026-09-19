Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046a780  section .text  offset fichier 0x69b80  144 octets

  0046a780  56                    push      esi
  0046a781  8b742408              mov       esi,[esp+8]
  0046a785  66837e1000            cmp       word [esi+10h],0
  0046a78a  7537                  jne       short 0046A7C3h
  0046a78c  66c746100100          mov       word [esi+10h],1
  0046a792  66c746120000          mov       word [esi+12h],0
  0046a798  66c746140000          mov       word [esi+14h],0
  0046a79e  e89dc90d00            call      00547140h
  0046a7a3  99                    cdq
  0046a7a4  b903000000            mov       ecx,3
  0046a7a9  f7f9                  idiv      ecx
  0046a7ab  83fa01                cmp       edx,1
  0046a7ae  7513                  jne       short 0046A7C3h
  0046a7b0  6a00                  push      0
  0046a7b2  6a00                  push      0
  0046a7b4  6aff                  push      0FFFFFFFFh
  0046a7b6  6a00                  push      0
  0046a7b8  6a36                  push      36h
  0046a7ba  56                    push      esi
  0046a7bb  e84018ffff            call      0045C000h
  0046a7c0  83c418                add       esp,18h

LAB_0046a7c3:
  0046a7c3  66837e1001            cmp       word [esi+10h],1
  0046a7c8  753d                  jne       short 0046A807h
  0046a7ca  66ff4612              inc       word [esi+12h]
  0046a7ce  66837e1246            cmp       word [esi+12h],46h
  0046a7d3  7e32                  jle       short 0046A807h
  0046a7d5  e866c90d00            call      00547140h
  0046a7da  99                    cdq
  0046a7db  b914000000            mov       ecx,14h
  0046a7e0  f7f9                  idiv      ecx
  0046a7e2  83fa01                cmp       edx,1
  0046a7e5  7407                  je        short 0046A7EEh
  0046a7e7  66837e1264            cmp       word [esi+12h],64h
  0046a7ec  7e19                  jle       short 0046A807h

LAB_0046a7ee:
  0046a7ee  6a00                  push      0
  0046a7f0  6a00                  push      0
  0046a7f2  6aff                  push      0FFFFFFFFh
  0046a7f4  6a00                  push      0
  0046a7f6  6a36                  push      36h
  0046a7f8  56                    push      esi
  0046a7f9  66c746120000          mov       word [esi+12h],0
  0046a7ff  e8fc17ffff            call      0045C000h
  0046a804  83c418                add       esp,18h

LAB_0046a807:
  0046a807  5e                    pop       esi
  0046a808  c3                    ret
  0046a809  90                    nop
  0046a80a  90                    nop
  0046a80b  90                    nop
  0046a80c  90                    nop
  0046a80d  90                    nop
  0046a80e  90                    nop
  0046a80f  90                    nop

3 cible(s) de saut interne : 0x0046a7c3, 0x0046a7ee, 0x0046a807
