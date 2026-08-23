Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00450700  section .text  offset fichier 0x4fb00  96 octets

  00450700  8b4c2404              mov       ecx,[esp+4]
  00450704  33c0                  xor       eax,eax
  00450706  56                    push      esi
  00450707  51                    push      ecx
  00450708  8a4141                mov       al,[ecx+41h]
  0045070b  c7416c00000000        mov       dword [ecx+6Ch],0
  00450712  c60560905e0000        mov       byte [5E9060h],0
  00450719  8d1480                lea       edx,[eax+eax*4]
  0045071c  c1e203                shl       edx,3
  0045071f  2bd0                  sub       edx,eax
  00450721  8d3495e0a55f00        lea       esi,[edx*4+5FA5E0h]
  00450728  e8a30b0e00            call      005312D0h
  0045072d  83c404                add       esp,4
  00450730  85c0                  test      eax,eax
  00450732  7411                  je        short 00450745h
  00450734  8b4e44                mov       ecx,[esi+44h]
  00450737  66c74610c800          mov       word [esi+10h],0C8h
  0045073d  888160985e00          mov       [ecx+5E9860h],al
  00450743  5e                    pop       esi
  00450744  c3                    ret

LAB_00450745:
  00450745  8b5644                mov       edx,[esi+44h]
  00450748  66c746106400          mov       word [esi+10h],64h
  0045074e  5e                    pop       esi
  0045074f  c68260985e0000        mov       byte [edx+5E9860h],0
  00450756  c3                    ret
  00450757  90                    nop
  00450758  90                    nop
  00450759  90                    nop
  0045075a  90                    nop
  0045075b  90                    nop
  0045075c  90                    nop
  0045075d  90                    nop
  0045075e  90                    nop
  0045075f  90                    nop

1 cible(s) de saut interne : 0x00450745
