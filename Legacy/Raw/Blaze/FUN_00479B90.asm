Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479b90  section .text  offset fichier 0x78f90  34 octets

  00479b90  8b4c2404              mov       ecx,[esp+4]
  00479b94  668b412a              mov       ax,[ecx+2Ah]
  00479b98  668bd0                mov       dx,ax
  00479b9b  66c1fa02              sar       dx,2
  00479b9f  2bc2                  sub       eax,edx
  00479ba1  663d8000              cmp       ax,80h
  00479ba5  6689412a              mov       [ecx+2Ah],ax
  00479ba9  7d06                  jge       short 00479BB1h
  00479bab  c70100000000          mov       dword [ecx],0

LAB_00479bb1:
  00479bb1  c3                    ret

1 cible(s) de saut interne : 0x00479bb1
