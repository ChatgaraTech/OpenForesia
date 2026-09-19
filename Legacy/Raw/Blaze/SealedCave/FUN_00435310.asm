Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00435310  section .text  offset fichier 0x34710  224 octets

  00435310  56                    push      esi
  00435311  8b742408              mov       esi,[esp+8]
  00435315  66837e1000            cmp       word [esi+10h],0
  0043531a  757c                  jne       short 00435398h
  0043531c  c7463cd00d5500        mov       dword [esi+3Ch],550DD0h
  00435323  c605d50d550002        mov       byte [550DD5h],2
  0043532a  c6464000              mov       byte [esi+40h],0
  0043532e  e80d1e1100            call      00547140h
  00435333  99                    cdq
  00435334  33c2                  xor       eax,edx
  00435336  c6463894              mov       byte [esi+38h],94h
  0043533a  2bc2                  sub       eax,edx
  0043533c  c646398a              mov       byte [esi+39h],8Ah
  00435340  25ff070000            and       eax,7FFh
  00435345  c6463a80              mov       byte [esi+3Ah],80h
  00435349  33c2                  xor       eax,edx
  0043534b  2bc2                  sub       eax,edx
  0043534d  0500040000            add       eax,400h
  00435352  66894628              mov       [esi+28h],ax
  00435356  6689462a              mov       [esi+2Ah],ax
  0043535a  e8e11d1100            call      00547140h
  0043535f  99                    cdq
  00435360  33c2                  xor       eax,edx
  00435362  66c746100100          mov       word [esi+10h],1
  00435368  2bc2                  sub       eax,edx
  0043536a  83e003                and       eax,3
  0043536d  33c2                  xor       eax,edx
  0043536f  2bc2                  sub       eax,edx
  00435371  83c003                add       eax,3
  00435374  66894612              mov       [esi+12h],ax
  00435378  66894614              mov       [esi+14h],ax
  0043537c  e8bf1d1100            call      00547140h
  00435381  99                    cdq
  00435382  b906000000            mov       ecx,6
  00435387  c6460a04              mov       byte [esi+0Ah],4
  0043538b  f7f9                  idiv      ecx
  0043538d  c6460b00              mov       byte [esi+0Bh],0
  00435391  83c203                add       edx,3
  00435394  66895616              mov       [esi+16h],dx

LAB_00435398:
  00435398  66837e1001            cmp       word [esi+10h],1
  0043539d  7545                  jne       short 004353E4h
  0043539f  0fbf5616              movsx     edx,word [esi+16h]
  004353a3  8b4e20                mov       ecx,[esi+20h]
  004353a6  6afc                  push      0FFFFFFFCh
  004353a8  c1e20c                shl       edx,0Ch
  004353ab  6afc                  push      0FFFFFFFCh
  004353ad  03ca                  add       ecx,edx
  004353af  6afc                  push      0FFFFFFFCh
  004353b1  56                    push      esi
  004353b2  894e20                mov       [esi+20h],ecx
  004353b5  e826fafcff            call      00404DE0h
  004353ba  83c410                add       esp,10h
  004353bd  66ff4e14              dec       word [esi+14h]
  004353c1  66837e1400            cmp       word [esi+14h],0
  004353c6  7f1c                  jg        short 004353E4h
  004353c8  8a4e40                mov       cl,[esi+40h]
  004353cb  668b4612              mov       ax,[esi+12h]
  004353cf  fec1                  inc       cl
  004353d1  66894614              mov       [esi+14h],ax
  004353d5  8ac1                  mov       al,cl
  004353d7  884e40                mov       [esi+40h],cl
  004353da  3c03                  cmp       al,3
  004353dc  7506                  jne       short 004353E4h
  004353de  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004353e4:
  004353e4  5e                    pop       esi
  004353e5  c3                    ret
  004353e6  90                    nop
  004353e7  90                    nop
  004353e8  90                    nop
  004353e9  90                    nop
  004353ea  90                    nop
  004353eb  90                    nop
  004353ec  90                    nop
  004353ed  90                    nop
  004353ee  90                    nop
  004353ef  90                    nop

2 cible(s) de saut interne : 0x00435398, 0x004353e4
