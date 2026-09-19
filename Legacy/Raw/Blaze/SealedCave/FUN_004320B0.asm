Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004320b0  section .text  offset fichier 0x314b0  224 octets

  004320b0  56                    push      esi
  004320b1  8b742408              mov       esi,[esp+8]
  004320b5  66837e1000            cmp       word [esi+10h],0
  004320ba  757c                  jne       short 00432138h
  004320bc  c7463cd00b5500        mov       dword [esi+3Ch],550BD0h
  004320c3  c605d50b550002        mov       byte [550BD5h],2
  004320ca  c6464000              mov       byte [esi+40h],0
  004320ce  e86d501100            call      00547140h
  004320d3  99                    cdq
  004320d4  33c2                  xor       eax,edx
  004320d6  c6463894              mov       byte [esi+38h],94h
  004320da  2bc2                  sub       eax,edx
  004320dc  c646398a              mov       byte [esi+39h],8Ah
  004320e0  25ff070000            and       eax,7FFh
  004320e5  c6463a80              mov       byte [esi+3Ah],80h
  004320e9  33c2                  xor       eax,edx
  004320eb  2bc2                  sub       eax,edx
  004320ed  0500040000            add       eax,400h
  004320f2  66894628              mov       [esi+28h],ax
  004320f6  6689462a              mov       [esi+2Ah],ax
  004320fa  e841501100            call      00547140h
  004320ff  99                    cdq
  00432100  33c2                  xor       eax,edx
  00432102  66c746100100          mov       word [esi+10h],1
  00432108  2bc2                  sub       eax,edx
  0043210a  83e003                and       eax,3
  0043210d  33c2                  xor       eax,edx
  0043210f  2bc2                  sub       eax,edx
  00432111  83c003                add       eax,3
  00432114  66894612              mov       [esi+12h],ax
  00432118  66894614              mov       [esi+14h],ax
  0043211c  e81f501100            call      00547140h
  00432121  99                    cdq
  00432122  b906000000            mov       ecx,6
  00432127  c6460a04              mov       byte [esi+0Ah],4
  0043212b  f7f9                  idiv      ecx
  0043212d  c6460b00              mov       byte [esi+0Bh],0
  00432131  83c203                add       edx,3
  00432134  66895616              mov       [esi+16h],dx

LAB_00432138:
  00432138  66837e1001            cmp       word [esi+10h],1
  0043213d  7545                  jne       short 00432184h
  0043213f  0fbf5616              movsx     edx,word [esi+16h]
  00432143  8b4e20                mov       ecx,[esi+20h]
  00432146  6afc                  push      0FFFFFFFCh
  00432148  c1e20c                shl       edx,0Ch
  0043214b  6afc                  push      0FFFFFFFCh
  0043214d  03ca                  add       ecx,edx
  0043214f  6afc                  push      0FFFFFFFCh
  00432151  56                    push      esi
  00432152  894e20                mov       [esi+20h],ecx
  00432155  e8862cfdff            call      00404DE0h
  0043215a  83c410                add       esp,10h
  0043215d  66ff4e14              dec       word [esi+14h]
  00432161  66837e1400            cmp       word [esi+14h],0
  00432166  7f1c                  jg        short 00432184h
  00432168  8a4e40                mov       cl,[esi+40h]
  0043216b  668b4612              mov       ax,[esi+12h]
  0043216f  fec1                  inc       cl
  00432171  66894614              mov       [esi+14h],ax
  00432175  8ac1                  mov       al,cl
  00432177  884e40                mov       [esi+40h],cl
  0043217a  3c03                  cmp       al,3
  0043217c  7506                  jne       short 00432184h
  0043217e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00432184:
  00432184  5e                    pop       esi
  00432185  c3                    ret
  00432186  90                    nop
  00432187  90                    nop
  00432188  90                    nop
  00432189  90                    nop
  0043218a  90                    nop
  0043218b  90                    nop
  0043218c  90                    nop
  0043218d  90                    nop
  0043218e  90                    nop
  0043218f  90                    nop

2 cible(s) de saut interne : 0x00432138, 0x00432184
