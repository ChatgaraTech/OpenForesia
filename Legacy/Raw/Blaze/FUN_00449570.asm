Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449570  section .text  offset fichier 0x48970  31 octets

  00449570  33c0                  xor       eax,eax
  00449572  b9402d5f00            mov       ecx,5F2D40h
  00449577  ba80000000            mov       edx,80h

LAB_0044957c:
  0044957c  f70100000080          test      dword [ecx],80000000h
  00449582  7401                  je        short 00449585h
  00449584  40                    inc       eax

LAB_00449585:
  00449585  81c19c000000          add       ecx,9Ch
  0044958b  4a                    dec       edx
  0044958c  75ee                  jne       short 0044957Ch
  0044958e  c3                    ret

2 cible(s) de saut interne : 0x0044957c, 0x00449585
