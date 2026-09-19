Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466df0  section .text  offset fichier 0x661f0  112 octets

  00466df0  56                    push      esi
  00466df1  8b742408              mov       esi,[esp+8]
  00466df5  b801000000            mov       eax,1
  00466dfa  66837e1000            cmp       word [esi+10h],0
  00466dff  7511                  jne       short 00466E12h
  00466e01  8b0e                  mov       ecx,[esi]
  00466e03  66894610              mov       [esi+10h],ax
  00466e07  80cd40                or        ch,40h
  00466e0a  66c746120000          mov       word [esi+12h],0
  00466e10  890e                  mov       [esi],ecx

LAB_00466e12:
  00466e12  66394610              cmp       [esi+10h],ax
  00466e16  753b                  jne       short 00466E53h
  00466e18  66ff4612              inc       word [esi+12h]
  00466e1c  66837e120a            cmp       word [esi+12h],0Ah
  00466e21  7530                  jne       short 00466E53h
  00466e23  a1cc975e00            mov       eax,[5E97CCh]
  00466e28  6a00                  push      0
  00466e2a  6a00                  push      0
  00466e2c  6800100000            push      1000h
  00466e31  8b88a8000000          mov       ecx,[eax+0A8h]
  00466e37  6800040000            push      400h
  00466e3c  8b11                  mov       edx,[ecx]
  00466e3e  660fb64213            movzx     ax,byte [edx+13h]
  00466e43  50                    push      eax
  00466e44  56                    push      esi
  00466e45  e836120000            call      00468080h
  00466e4a  83c418                add       esp,18h
  00466e4d  66c746100000          mov       word [esi+10h],0

LAB_00466e53:
  00466e53  5e                    pop       esi
  00466e54  c3                    ret
  00466e55  90                    nop
  00466e56  90                    nop
  00466e57  90                    nop
  00466e58  90                    nop
  00466e59  90                    nop
  00466e5a  90                    nop
  00466e5b  90                    nop
  00466e5c  90                    nop
  00466e5d  90                    nop
  00466e5e  90                    nop
  00466e5f  90                    nop

2 cible(s) de saut interne : 0x00466e12, 0x00466e53
