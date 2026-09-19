Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042fc10  section .text  offset fichier 0x2f010  176 octets

  0042fc10  56                    push      esi
  0042fc11  8b742408              mov       esi,[esp+8]
  0042fc15  b901000000            mov       ecx,1
  0042fc1a  66837e1000            cmp       word [esi+10h],0
  0042fc1f  7536                  jne       short 0042FC57h
  0042fc21  66894e10              mov       [esi+10h],cx
  0042fc25  a0c4de5e00            mov       al,[5EDEC4h]
  0042fc2a  884604                mov       [esi+4],al
  0042fc2d  b8b80b0000            mov       eax,0BB8h
  0042fc32  66894628              mov       [esi+28h],ax
  0042fc36  6689462a              mov       [esi+2Ah],ax
  0042fc3a  6689462c              mov       [esi+2Ch],ax
  0042fc3e  b0ff                  mov       al,0FFh
  0042fc40  c6460a03              mov       byte [esi+0Ah],3
  0042fc44  c6460b00              mov       byte [esi+0Bh],0
  0042fc48  66c746128403          mov       word [esi+12h],384h
  0042fc4e  884638                mov       [esi+38h],al
  0042fc51  884639                mov       [esi+39h],al
  0042fc54  88463a                mov       [esi+3Ah],al

LAB_0042fc57:
  0042fc57  66394e10              cmp       [esi+10h],cx
  0042fc5b  7552                  jne       short 0042FCAFh
  0042fc5d  6af4                  push      0FFFFFFF4h
  0042fc5f  6af4                  push      0FFFFFFF4h
  0042fc61  6af4                  push      0FFFFFFF4h
  0042fc63  56                    push      esi
  0042fc64  e87751fdff            call      00404DE0h
  0042fc69  668b4612              mov       ax,[esi+12h]
  0042fc6d  83c410                add       esp,10h
  0042fc70  66014628              add       [esi+28h],ax
  0042fc74  6601462a              add       [esi+2Ah],ax
  0042fc78  6601462c              add       [esi+2Ch],ax
  0042fc7c  83c028                add       eax,28h
  0042fc7f  663de803              cmp       ax,3E8h
  0042fc83  66894612              mov       [esi+12h],ax
  0042fc87  7c06                  jl        short 0042FC8Fh
  0042fc89  66c74612e803          mov       word [esi+12h],3E8h

LAB_0042fc8f:
  0042fc8f  b830750000            mov       eax,7530h
  0042fc94  66394628              cmp       [esi+28h],ax
  0042fc98  7c15                  jl        short 0042FCAFh
  0042fc9a  66894628              mov       [esi+28h],ax
  0042fc9e  6689462a              mov       [esi+2Ah],ax
  0042fca2  6689462c              mov       [esi+2Ch],ax
  0042fca6  8b06                  mov       eax,[esi]
  0042fca8  25ffffff7f            and       eax,7FFFFFFFh
  0042fcad  8906                  mov       [esi],eax

LAB_0042fcaf:
  0042fcaf  5e                    pop       esi
  0042fcb0  c3                    ret
  0042fcb1  90                    nop
  0042fcb2  90                    nop
  0042fcb3  90                    nop
  0042fcb4  90                    nop
  0042fcb5  90                    nop
  0042fcb6  90                    nop
  0042fcb7  90                    nop
  0042fcb8  90                    nop
  0042fcb9  90                    nop
  0042fcba  90                    nop
  0042fcbb  90                    nop
  0042fcbc  90                    nop
  0042fcbd  90                    nop
  0042fcbe  90                    nop
  0042fcbf  90                    nop

3 cible(s) de saut interne : 0x0042fc57, 0x0042fc8f, 0x0042fcaf
