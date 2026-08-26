Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004432e0  section .text  offset fichier 0x426e0  19 octets

  004432e0  f60501905e0010        test      byte [5E9001h],10h
  004432e7  7407                  je        short 004432F0h
  004432e9  802541985e00fe        and       byte [5E9841h],0FEh

LAB_004432f0:
  004432f0  33c0                  xor       eax,eax
  004432f2  c3                    ret

1 cible(s) de saut interne : 0x004432f0
