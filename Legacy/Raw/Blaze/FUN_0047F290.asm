
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f290  section .text  offset fichier 0x7e690  30 octets

  0047f290  8b4c2404              mov       ecx,[esp+4]
  0047f294  8b4138                mov       eax,[ecx+38h]
  0047f297  3c20                  cmp       al,20h
  0047f299  720c                  jb        short 0047F2A7h
  0047f29b  2d20202000            sub       eax,202020h
  0047f2a0  3c20                  cmp       al,20h
  0047f2a2  894138                mov       [ecx+38h],eax
  0047f2a5  7306                  jae       short 0047F2ADh

LAB_0047f2a7:
  0047f2a7  c70100000000          mov       dword [ecx],0

LAB_0047f2ad:
  0047f2ad  c3                    ret

2 cible(s) de saut interne : 0x0047f2a7, 0x0047f2ad
