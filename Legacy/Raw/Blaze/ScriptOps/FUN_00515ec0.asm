
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515ec0  section .text  offset fichier 0x1152c0  96 octets

  00515ec0  e8fb5b0000            call      0051BAC0h
  00515ec5  8b44240c              mov       eax,[esp+0Ch]
  00515ec9  40                    inc       eax
  00515eca  c3                    ret
  00515ecb  90                    nop
  00515ecc  90                    nop
  00515ecd  90                    nop
  00515ece  90                    nop
  00515ecf  90                    nop
  00515ed0  8a0d8c6c5600          mov       cl,[566C8Ch]
  00515ed6  56                    push      esi
  00515ed7  8b742410              mov       esi,[esp+10h]
  00515edb  8a4601                mov       al,[esi+1]
  00515ede  3ac8                  cmp       cl,al
  00515ee0  741b                  je        short 00515EFDh
  00515ee2  33d2                  xor       edx,edx
  00515ee4  a234e55d00            mov       [5DE534h],al
  00515ee9  8a4e01                mov       cl,[esi+1]
  00515eec  8ad0                  mov       dl,al
  00515eee  52                    push      edx
  00515eef  880d8c6c5600          mov       [566C8Ch],cl
  00515ef5  e8d63b0100            call      00529AD0h
  00515efa  83c404                add       esp,4

LAB_00515efd:
  00515efd  8d4602                lea       eax,[esi+2]
  00515f00  5e                    pop       esi
  00515f01  c3                    ret
  00515f02  90                    nop
  00515f03  90                    nop
  00515f04  90                    nop
  00515f05  90                    nop
  00515f06  90                    nop
  00515f07  90                    nop
  00515f08  90                    nop
  00515f09  90                    nop
  00515f0a  90                    nop
  00515f0b  90                    nop
  00515f0c  90                    nop
  00515f0d  90                    nop
  00515f0e  90                    nop
  00515f0f  90                    nop
  00515f10  8b44240c              mov       eax,[esp+0Ch]
  00515f14  83c002                add       eax,2
  00515f17  c3                    ret
  00515f18  90                    nop
  00515f19  90                    nop
  00515f1a  90                    nop
  00515f1b  90                    nop
  00515f1c  90                    nop
  00515f1d  90                    nop
  00515f1e  90                    nop
  00515f1f  90                    nop

1 cible(s) de saut interne : 0x00515efd
