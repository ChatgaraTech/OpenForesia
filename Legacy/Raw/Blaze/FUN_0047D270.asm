
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d270  section .text  offset fichier 0x7c670  35 octets

  0047d270  8b442404              mov       eax,[esp+4]
  0047d274  8b4838                mov       ecx,[eax+38h]
  0047d277  6681403290fe          add       word [eax+32h],0FE90h
  0047d27d  81c1f0efefff          add       ecx,0FFEFEFF0h
  0047d283  894838                mov       [eax+38h],ecx
  0047d286  7506                  jne       short 0047D28Eh
  0047d288  c70000000000          mov       dword [eax],0

LAB_0047d28e:
  0047d28e  66ff4010              inc       word [eax+10h]
  0047d292  c3                    ret

1 cible(s) de saut interne : 0x0047d28e
