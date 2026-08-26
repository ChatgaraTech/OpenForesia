Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443260  section .text  offset fichier 0x42660  53 octets

  00443260  57                    push      edi
  00443261  b910000000            mov       ecx,10h
  00443266  33c0                  xor       eax,eax
  00443268  bf40985e00            mov       edi,5E9840h
  0044326d  f3ab                  rep stosd
  0044326f  b910000000            mov       ecx,10h
  00443274  bf60905e00            mov       edi,5E9060h
  00443279  f3ab                  rep stosd
  0044327b  b910000000            mov       ecx,10h
  00443280  bf80975e00            mov       edi,5E9780h
  00443285  f3ab                  rep stosd
  00443287  b910000000            mov       ecx,10h
  0044328c  bfe0965e00            mov       edi,5E96E0h
  00443291  f3ab                  rep stosd
  00443293  5f                    pop       edi
  00443294  c3                    ret
