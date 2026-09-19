Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433770  section .text  offset fichier 0x32b70  224 octets

  00433770  56                    push      esi
  00433771  8b742408              mov       esi,[esp+8]
  00433775  66837e1000            cmp       word [esi+10h],0
  0043377a  757c                  jne       short 004337F8h
  0043377c  c7463ce00c5500        mov       dword [esi+3Ch],550CE0h
  00433783  c605e50c550002        mov       byte [550CE5h],2
  0043378a  c6464000              mov       byte [esi+40h],0
  0043378e  e8ad391100            call      00547140h
  00433793  99                    cdq
  00433794  33c2                  xor       eax,edx
  00433796  c6463894              mov       byte [esi+38h],94h
  0043379a  2bc2                  sub       eax,edx
  0043379c  c646398a              mov       byte [esi+39h],8Ah
  004337a0  25ff070000            and       eax,7FFh
  004337a5  c6463a80              mov       byte [esi+3Ah],80h
  004337a9  33c2                  xor       eax,edx
  004337ab  2bc2                  sub       eax,edx
  004337ad  0500040000            add       eax,400h
  004337b2  66894628              mov       [esi+28h],ax
  004337b6  6689462a              mov       [esi+2Ah],ax
  004337ba  e881391100            call      00547140h
  004337bf  99                    cdq
  004337c0  33c2                  xor       eax,edx
  004337c2  66c746100100          mov       word [esi+10h],1
  004337c8  2bc2                  sub       eax,edx
  004337ca  83e003                and       eax,3
  004337cd  33c2                  xor       eax,edx
  004337cf  2bc2                  sub       eax,edx
  004337d1  83c003                add       eax,3
  004337d4  66894612              mov       [esi+12h],ax
  004337d8  66894614              mov       [esi+14h],ax
  004337dc  e85f391100            call      00547140h
  004337e1  99                    cdq
  004337e2  b906000000            mov       ecx,6
  004337e7  c6460a04              mov       byte [esi+0Ah],4
  004337eb  f7f9                  idiv      ecx
  004337ed  c6460b00              mov       byte [esi+0Bh],0
  004337f1  83c203                add       edx,3
  004337f4  66895616              mov       [esi+16h],dx

LAB_004337f8:
  004337f8  66837e1001            cmp       word [esi+10h],1
  004337fd  7545                  jne       short 00433844h
  004337ff  0fbf5616              movsx     edx,word [esi+16h]
  00433803  8b4e20                mov       ecx,[esi+20h]
  00433806  6afc                  push      0FFFFFFFCh
  00433808  c1e20c                shl       edx,0Ch
  0043380b  6afc                  push      0FFFFFFFCh
  0043380d  03ca                  add       ecx,edx
  0043380f  6afc                  push      0FFFFFFFCh
  00433811  56                    push      esi
  00433812  894e20                mov       [esi+20h],ecx
  00433815  e8c615fdff            call      00404DE0h
  0043381a  83c410                add       esp,10h
  0043381d  66ff4e14              dec       word [esi+14h]
  00433821  66837e1400            cmp       word [esi+14h],0
  00433826  7f1c                  jg        short 00433844h
  00433828  8a4e40                mov       cl,[esi+40h]
  0043382b  668b4612              mov       ax,[esi+12h]
  0043382f  fec1                  inc       cl
  00433831  66894614              mov       [esi+14h],ax
  00433835  8ac1                  mov       al,cl
  00433837  884e40                mov       [esi+40h],cl
  0043383a  3c03                  cmp       al,3
  0043383c  7506                  jne       short 00433844h
  0043383e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00433844:
  00433844  5e                    pop       esi
  00433845  c3                    ret
  00433846  90                    nop
  00433847  90                    nop
  00433848  90                    nop
  00433849  90                    nop
  0043384a  90                    nop
  0043384b  90                    nop
  0043384c  90                    nop
  0043384d  90                    nop
  0043384e  90                    nop
  0043384f  90                    nop

2 cible(s) de saut interne : 0x004337f8, 0x00433844
