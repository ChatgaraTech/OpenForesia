
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047fe40  section .text  offset fichier 0x7f240  29 octets

  0047fe40  8b442404              mov       eax,[esp+4]
  0047fe44  8b4838                mov       ecx,[eax+38h]
  0047fe47  80483604              or        byte [eax+36h],4
  0047fe4b  81c1f0efefff          add       ecx,0FFEFEFF0h
  0047fe51  894838                mov       [eax+38h],ecx
  0047fe54  7506                  jne       short 0047FE5Ch
  0047fe56  c70000000000          mov       dword [eax],0

LAB_0047fe5c:
  0047fe5c  c3                    ret

1 cible(s) de saut interne : 0x0047fe5c
