Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466e60  section .text  offset fichier 0x66260  112 octets

  00466e60  56                    push      esi
  00466e61  8b742408              mov       esi,[esp+8]
  00466e65  b801000000            mov       eax,1
  00466e6a  66837e1000            cmp       word [esi+10h],0
  00466e6f  7511                  jne       short 00466E82h
  00466e71  8b0e                  mov       ecx,[esi]
  00466e73  66894610              mov       [esi+10h],ax
  00466e77  80cd40                or        ch,40h
  00466e7a  66c746120000          mov       word [esi+12h],0
  00466e80  890e                  mov       [esi],ecx

LAB_00466e82:
  00466e82  66394610              cmp       [esi+10h],ax
  00466e86  753b                  jne       short 00466EC3h
  00466e88  66ff4612              inc       word [esi+12h]
  00466e8c  66837e120a            cmp       word [esi+12h],0Ah
  00466e91  7530                  jne       short 00466EC3h
  00466e93  a1cc975e00            mov       eax,[5E97CCh]
  00466e98  6800080000            push      800h
  00466e9d  6800080000            push      800h
  00466ea2  6800040000            push      400h
  00466ea7  8b88a8000000          mov       ecx,[eax+0A8h]
  00466ead  33c0                  xor       eax,eax
  00466eaf  8b11                  mov       edx,[ecx]
  00466eb1  8a4213                mov       al,[edx+13h]
  00466eb4  50                    push      eax
  00466eb5  e8e65f0d00            call      0053CEA0h
  00466eba  83c410                add       esp,10h
  00466ebd  66c746100000          mov       word [esi+10h],0

LAB_00466ec3:
  00466ec3  5e                    pop       esi
  00466ec4  c3                    ret
  00466ec5  90                    nop
  00466ec6  90                    nop
  00466ec7  90                    nop
  00466ec8  90                    nop
  00466ec9  90                    nop
  00466eca  90                    nop
  00466ecb  90                    nop
  00466ecc  90                    nop
  00466ecd  90                    nop
  00466ece  90                    nop
  00466ecf  90                    nop

2 cible(s) de saut interne : 0x00466e82, 0x00466ec3
