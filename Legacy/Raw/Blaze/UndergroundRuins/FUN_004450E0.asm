Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004450e0  section .text  offset fichier 0x444e0  64 octets

  004450e0  53                    push      ebx
  004450e1  32db                  xor       bl,bl
  004450e3  881d61905e00          mov       [5E9061h],bl
  004450e9  b9c0935e00            mov       ecx,5E93C0h
  004450ee  ba04000000            mov       edx,4

LAB_004450f3:
  004450f3  8b01                  mov       eax,[ecx]
  004450f5  a900000080            test      eax,80000000h
  004450fa  7409                  je        short 00445105h
  004450fc  a900000050            test      eax,50000000h
  00445101  7502                  jne       short 00445105h
  00445103  fec3                  inc       bl

LAB_00445105:
  00445105  81c19c000000          add       ecx,9Ch
  0044510b  4a                    dec       edx
  0044510c  75e5                  jne       short 004450F3h
  0044510e  881d61905e00          mov       [5E9061h],bl
  00445114  33c0                  xor       eax,eax
  00445116  5b                    pop       ebx
  00445117  c3                    ret
  00445118  90                    nop
  00445119  90                    nop
  0044511a  90                    nop
  0044511b  90                    nop
  0044511c  90                    nop
  0044511d  90                    nop
  0044511e  90                    nop
  0044511f  90                    nop

2 cible(s) de saut interne : 0x004450f3, 0x00445105
