Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00443100  section .text  offset fichier 0x42500  108 octets

  00443100  b900f45f00            mov       ecx,5FF400h
  00443105  ba20000000            mov       edx,20h

LAB_0044310a:
  0044310a  8b01                  mov       eax,[ecx]
  0044310c  a900000010            test      eax,10000000h
  00443111  7507                  jne       short 0044311Ah
  00443113  25fffffffb            and       eax,0FBFFFFFFh
  00443118  8901                  mov       [ecx],eax

LAB_0044311a:
  0044311a  81c19c000000          add       ecx,9Ch
  00443120  4a                    dec       edx
  00443121  75e7                  jne       short 0044310Ah
  00443123  b9402d5f00            mov       ecx,5F2D40h
  00443128  ba80000000            mov       edx,80h

LAB_0044312d:
  0044312d  8b01                  mov       eax,[ecx]
  0044312f  a900000010            test      eax,10000000h
  00443134  7507                  jne       short 0044313Dh
  00443136  25fffffffb            and       eax,0FBFFFFFFh
  0044313b  8901                  mov       [ecx],eax

LAB_0044313d:
  0044313d  81c19c000000          add       ecx,9Ch
  00443143  4a                    dec       edx
  00443144  75e7                  jne       short 0044312Dh
  00443146  b9e0de5e00            mov       ecx,5EDEE0h
  0044314b  ba80000000            mov       edx,80h

LAB_00443150:
  00443150  8b01                  mov       eax,[ecx]
  00443152  a900000010            test      eax,10000000h
  00443157  7507                  jne       short 00443160h
  00443159  25fffffffb            and       eax,0FBFFFFFFh
  0044315e  8901                  mov       [ecx],eax

LAB_00443160:
  00443160  81c19c000000          add       ecx,9Ch
  00443166  4a                    dec       edx
  00443167  75e7                  jne       short 00443150h
  00443169  33c0                  xor       eax,eax
  0044316b  c3                    ret

6 cible(s) de saut interne : 0x0044310a, 0x0044311a, 0x0044312d, 0x0044313d, 0x00443150, 0x00443160
