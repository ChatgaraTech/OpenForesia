Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042fb50  section .text  offset fichier 0x2ef50  192 octets

  0042fb50  56                    push      esi
  0042fb51  8b742408              mov       esi,[esp+8]
  0042fb55  33c0                  xor       eax,eax
  0042fb57  66394610              cmp       [esi+10h],ax
  0042fb5b  7557                  jne       short 0042FBB4h
  0042fb5d  8a0dc4de5e00          mov       cl,[5EDEC4h]
  0042fb63  6aff                  push      0FFFFFFFFh
  0042fb65  fec1                  inc       cl
  0042fb67  50                    push      eax
  0042fb68  884e04                mov       [esi+4],cl
  0042fb6b  b1be                  mov       cl,0BEh
  0042fb6d  884e39                mov       [esi+39h],cl
  0042fb70  884e3a                mov       [esi+3Ah],cl
  0042fb73  8b4e20                mov       ecx,[esi+20h]
  0042fb76  68c2000000            push      0C2h
  0042fb7b  81c100400600          add       ecx,64000h
  0042fb81  56                    push      esi
  0042fb82  c6460a05              mov       byte [esi+0Ah],5
  0042fb86  88460b                mov       [esi+0Bh],al
  0042fb89  c64638fa              mov       byte [esi+38h],0FAh
  0042fb8d  66894628              mov       [esi+28h],ax
  0042fb91  6689462a              mov       [esi+2Ah],ax
  0042fb95  6689462c              mov       [esi+2Ch],ax
  0042fb99  894e20                mov       [esi+20h],ecx
  0042fb9c  66894612              mov       [esi+12h],ax
  0042fba0  66c746100100          mov       word [esi+10h],1
  0042fba6  e835af0400            call      0047AAE0h
  0042fbab  83c410                add       esp,10h
  0042fbae  66c74612bc02          mov       word [esi+12h],2BCh

LAB_0042fbb4:
  0042fbb4  66837e1001            cmp       word [esi+10h],1
  0042fbb9  7552                  jne       short 0042FC0Dh
  0042fbbb  6af4                  push      0FFFFFFF4h
  0042fbbd  6af4                  push      0FFFFFFF4h
  0042fbbf  6af8                  push      0FFFFFFF8h
  0042fbc1  56                    push      esi
  0042fbc2  e81952fdff            call      00404DE0h
  0042fbc7  668b4612              mov       ax,[esi+12h]
  0042fbcb  83c410                add       esp,10h
  0042fbce  66014628              add       [esi+28h],ax
  0042fbd2  6601462a              add       [esi+2Ah],ax
  0042fbd6  6601462c              add       [esi+2Ch],ax
  0042fbda  83c014                add       eax,14h
  0042fbdd  663de803              cmp       ax,3E8h
  0042fbe1  66894612              mov       [esi+12h],ax
  0042fbe5  7c06                  jl        short 0042FBEDh
  0042fbe7  66c74612e803          mov       word [esi+12h],3E8h

LAB_0042fbed:
  0042fbed  b830750000            mov       eax,7530h
  0042fbf2  66394628              cmp       [esi+28h],ax
  0042fbf6  7c15                  jl        short 0042FC0Dh
  0042fbf8  66894628              mov       [esi+28h],ax
  0042fbfc  6689462a              mov       [esi+2Ah],ax
  0042fc00  6689462c              mov       [esi+2Ch],ax
  0042fc04  8b06                  mov       eax,[esi]
  0042fc06  25ffffff7f            and       eax,7FFFFFFFh
  0042fc0b  8906                  mov       [esi],eax

LAB_0042fc0d:
  0042fc0d  5e                    pop       esi
  0042fc0e  c3                    ret
  0042fc0f  90                    nop

3 cible(s) de saut interne : 0x0042fbb4, 0x0042fbed, 0x0042fc0d
