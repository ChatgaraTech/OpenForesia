Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043cd70  section .text  offset fichier 0x3c170  192 octets

  0043cd70  56                    push      esi
  0043cd71  8b742408              mov       esi,[esp+8]
  0043cd75  33c0                  xor       eax,eax
  0043cd77  66394610              cmp       [esi+10h],ax
  0043cd7b  7557                  jne       short 0043CDD4h
  0043cd7d  8a0dc4de5e00          mov       cl,[5EDEC4h]
  0043cd83  6aff                  push      0FFFFFFFFh
  0043cd85  fec1                  inc       cl
  0043cd87  50                    push      eax
  0043cd88  884e04                mov       [esi+4],cl
  0043cd8b  b1be                  mov       cl,0BEh
  0043cd8d  884e38                mov       [esi+38h],cl
  0043cd90  884e39                mov       [esi+39h],cl
  0043cd93  8b4e20                mov       ecx,[esi+20h]
  0043cd96  68ca000000            push      0CAh
  0043cd9b  81c100400600          add       ecx,64000h
  0043cda1  56                    push      esi
  0043cda2  c6460a0f              mov       byte [esi+0Ah],0Fh
  0043cda6  88460b                mov       [esi+0Bh],al
  0043cda9  c6463afa              mov       byte [esi+3Ah],0FAh
  0043cdad  66894628              mov       [esi+28h],ax
  0043cdb1  6689462a              mov       [esi+2Ah],ax
  0043cdb5  6689462c              mov       [esi+2Ch],ax
  0043cdb9  894e20                mov       [esi+20h],ecx
  0043cdbc  66894612              mov       [esi+12h],ax
  0043cdc0  66c746100100          mov       word [esi+10h],1
  0043cdc6  e815dd0300            call      0047AAE0h
  0043cdcb  83c410                add       esp,10h
  0043cdce  66c74612bc02          mov       word [esi+12h],2BCh

LAB_0043cdd4:
  0043cdd4  66837e1001            cmp       word [esi+10h],1
  0043cdd9  7552                  jne       short 0043CE2Dh
  0043cddb  6af4                  push      0FFFFFFF4h
  0043cddd  6af4                  push      0FFFFFFF4h
  0043cddf  6af8                  push      0FFFFFFF8h
  0043cde1  56                    push      esi
  0043cde2  e8f97ffcff            call      00404DE0h
  0043cde7  668b4612              mov       ax,[esi+12h]
  0043cdeb  83c410                add       esp,10h
  0043cdee  66014628              add       [esi+28h],ax
  0043cdf2  6601462a              add       [esi+2Ah],ax
  0043cdf6  6601462c              add       [esi+2Ch],ax
  0043cdfa  83c014                add       eax,14h
  0043cdfd  663de803              cmp       ax,3E8h
  0043ce01  66894612              mov       [esi+12h],ax
  0043ce05  7c06                  jl        short 0043CE0Dh
  0043ce07  66c74612e803          mov       word [esi+12h],3E8h

LAB_0043ce0d:
  0043ce0d  b830750000            mov       eax,7530h
  0043ce12  66394628              cmp       [esi+28h],ax
  0043ce16  7c15                  jl        short 0043CE2Dh
  0043ce18  66894628              mov       [esi+28h],ax
  0043ce1c  6689462a              mov       [esi+2Ah],ax
  0043ce20  6689462c              mov       [esi+2Ch],ax
  0043ce24  8b06                  mov       eax,[esi]
  0043ce26  25ffffff7f            and       eax,7FFFFFFFh
  0043ce2b  8906                  mov       [esi],eax

LAB_0043ce2d:
  0043ce2d  5e                    pop       esi
  0043ce2e  c3                    ret
  0043ce2f  90                    nop

3 cible(s) de saut interne : 0x0043cdd4, 0x0043ce0d, 0x0043ce2d
