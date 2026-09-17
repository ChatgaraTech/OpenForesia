Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460d10  section .text  offset fichier 0x60110  78 octets

  00460d10  a0ff965e00            mov       al,[5E96FFh]
  00460d15  56                    push      esi
  00460d16  a804                  test      al,4
  00460d18  7437                  je        short 00460D51h
  00460d1a  8b742408              mov       esi,[esp+8]
  00460d1e  0fbf4612              movsx     eax,word [esi+12h]
  00460d22  50                    push      eax
  00460d23  e8f44c0e00            call      00545A1Ch
  00460d28  6683461250            add       word [esi+12h],50h
  00460d2d  2d00a00000            sub       eax,0A000h
  00460d32  8bc8                  mov       ecx,eax
  00460d34  c1e103                shl       ecx,3
  00460d37  2bc8                  sub       ecx,eax
  00460d39  668b4612              mov       ax,[esi+12h]
  00460d3d  663d0010              cmp       ax,1000h
  00460d41  894e20                mov       [esi+20h],ecx
  00460d44  7c16                  jl        short 00460D5Ch
  00460d46  0500f0ffff            add       eax,0FFFFF000h
  00460d4b  66894612              mov       [esi+12h],ax
  00460d4f  5e                    pop       esi
  00460d50  c3                    ret

LAB_00460d51:
  00460d51  8b542408              mov       edx,[esp+8]
  00460d55  c742200060f5ff        mov       dword [edx+20h],0FFF56000h

LAB_00460d5c:
  00460d5c  5e                    pop       esi
  00460d5d  c3                    ret

2 cible(s) de saut interne : 0x00460d51, 0x00460d5c
