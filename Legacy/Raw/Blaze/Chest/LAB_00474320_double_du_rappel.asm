Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00474320  section .text  offset fichier 0x73720  87 octets

  00474320  8b4c2404              mov       ecx,[esp+4]
  00474324  33c0                  xor       eax,eax
  00474326  56                    push      esi
  00474327  51                    push      ecx
  00474328  8a4141                mov       al,[ecx+41h]
  0047432b  c7416c00000000        mov       dword [ecx+6Ch],0
  00474332  c60560905e0000        mov       byte [5E9060h],0
  00474339  8d1480                lea       edx,[eax+eax*4]
  0047433c  c1e203                shl       edx,3
  0047433f  2bd0                  sub       edx,eax
  00474341  8d3495e0a55f00        lea       esi,[edx*4+5FA5E0h]
  00474348  e883cf0b00            call      005312D0h
  0047434d  83c404                add       esp,4
  00474350  85c0                  test      eax,eax
  00474352  7411                  je        short 00474365h
  00474354  8b4e44                mov       ecx,[esi+44h]
  00474357  66c74610c800          mov       word [esi+10h],0C8h
  0047435d  888160985e00          mov       [ecx+5E9860h],al
  00474363  5e                    pop       esi
  00474364  c3                    ret

LAB_00474365:
  00474365  8b5644                mov       edx,[esi+44h]
  00474368  66c746106400          mov       word [esi+10h],64h
  0047436e  5e                    pop       esi
  0047436f  c68260985e0000        mov       byte [edx+5E9860h],0
  00474376  c3                    ret

1 cible(s) de saut interne : 0x00474365
