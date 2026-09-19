Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445760  section .text  offset fichier 0x44b60  64 octets

  00445760  33d2                  xor       edx,edx

LAB_00445762:
  00445762  0fbfca                movsx     ecx,dx
  00445765  8d0489                lea       eax,[ecx+ecx*4]
  00445768  c1e003                shl       eax,3
  0044576b  2bc1                  sub       eax,ecx
  0044576d  c1e002                shl       eax,2
  00445770  8b88c0935e00          mov       ecx,[eax+5E93C0h]
  00445776  f7c100000080          test      ecx,80000000h
  0044577c  7412                  je        short 00445790h
  0044577e  f7c100000050          test      ecx,50000000h
  00445784  750a                  jne       short 00445790h
  00445786  6683b8d2935e0000      cmp       word [eax+5E93D2h],0
  0044578e  740d                  je        short 0044579Dh

LAB_00445790:
  00445790  42                    inc       edx
  00445791  6683fa03              cmp       dx,3
  00445795  7ecb                  jle       short 00445762h
  00445797  b801000000            mov       eax,1
  0044579c  c3                    ret

LAB_0044579d:
  0044579d  33c0                  xor       eax,eax
  0044579f  c3                    ret

3 cible(s) de saut interne : 0x00445762, 0x00445790, 0x0044579d
