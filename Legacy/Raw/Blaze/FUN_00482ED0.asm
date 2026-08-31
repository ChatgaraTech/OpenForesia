Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482ed0  section .text  offset fichier 0x822d0  25 octets

  00482ed0  8b442404              mov       eax,[esp+4]
  00482ed4  8b4838                mov       ecx,[eax+38h]
  00482ed7  81c1f0efefff          add       ecx,0FFEFEFF0h
  00482edd  894838                mov       [eax+38h],ecx
  00482ee0  7506                  jne       short 00482EE8h
  00482ee2  c70000000000          mov       dword [eax],0

LAB_00482ee8:
  00482ee8  c3                    ret

1 cible(s) de saut interne : 0x00482ee8
