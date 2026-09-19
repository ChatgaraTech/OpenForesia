Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043a600  section .text  offset fichier 0x39a00  304 octets

  0043a600  56                    push      esi
  0043a601  8b742408              mov       esi,[esp+8]
  0043a605  66837e1000            cmp       word [esi+10h],0
  0043a60a  0f85c5000000          jne       near 0043A6D5h
  0043a610  66c746100100          mov       word [esi+10h],1
  0043a616  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043a61d  c6464000              mov       byte [esi+40h],0
  0043a621  c6460a04              mov       byte [esi+0Ah],4
  0043a625  c6460b00              mov       byte [esi+0Bh],0
  0043a629  e812cb1000            call      00547140h
  0043a62e  99                    cdq
  0043a62f  b996000000            mov       ecx,96h
  0043a634  f7f9                  idiv      ecx
  0043a636  8b4e1c                mov       ecx,[esi+1Ch]
  0043a639  83ea4b                sub       edx,4Bh
  0043a63c  c1e20c                shl       edx,0Ch
  0043a63f  03ca                  add       ecx,edx
  0043a641  894e1c                mov       [esi+1Ch],ecx
  0043a644  e8f7ca1000            call      00547140h
  0043a649  99                    cdq
  0043a64a  b996000000            mov       ecx,96h
  0043a64f  f7f9                  idiv      ecx
  0043a651  8b4624                mov       eax,[esi+24h]
  0043a654  83ea4b                sub       edx,4Bh
  0043a657  c1e20c                shl       edx,0Ch
  0043a65a  03c2                  add       eax,edx
  0043a65c  894624                mov       [esi+24h],eax
  0043a65f  e8dcca1000            call      00547140h
  0043a664  99                    cdq
  0043a665  b928000000            mov       ecx,28h
  0043a66a  f7f9                  idiv      ecx
  0043a66c  8b4e20                mov       ecx,[esi+20h]
  0043a66f  83ea14                sub       edx,14h
  0043a672  c1e20c                shl       edx,0Ch
  0043a675  03ca                  add       ecx,edx
  0043a677  894e20                mov       [esi+20h],ecx
  0043a67a  e8c1ca1000            call      00547140h
  0043a67f  99                    cdq
  0043a680  b906000000            mov       ecx,6
  0043a685  f7f9                  idiv      ecx
  0043a687  83c203                add       edx,3
  0043a68a  66895612              mov       [esi+12h],dx
  0043a68e  e8adca1000            call      00547140h
  0043a693  99                    cdq
  0043a694  33c2                  xor       eax,edx
  0043a696  2bc2                  sub       eax,edx
  0043a698  83e003                and       eax,3
  0043a69b  33c2                  xor       eax,edx
  0043a69d  2bc2                  sub       eax,edx
  0043a69f  668b5612              mov       dx,[esi+12h]
  0043a6a3  89465c                mov       [esi+5Ch],eax
  0043a6a6  66895614              mov       [esi+14h],dx
  0043a6aa  e891ca1000            call      00547140h
  0043a6af  99                    cdq
  0043a6b0  33c2                  xor       eax,edx
  0043a6b2  2bc2                  sub       eax,edx
  0043a6b4  25ff070000            and       eax,7FFh
  0043a6b9  33c2                  xor       eax,edx
  0043a6bb  2bc2                  sub       eax,edx
  0043a6bd  0500080000            add       eax,800h
  0043a6c2  66894628              mov       [esi+28h],ax
  0043a6c6  6689462a              mov       [esi+2Ah],ax
  0043a6ca  b040                  mov       al,40h
  0043a6cc  884638                mov       [esi+38h],al
  0043a6cf  884639                mov       [esi+39h],al
  0043a6d2  88463a                mov       [esi+3Ah],al

LAB_0043a6d5:
  0043a6d5  66837e1001            cmp       word [esi+10h],1
  0043a6da  7544                  jne       short 0043A720h
  0043a6dc  8b465c                mov       eax,[esi+5Ch]
  0043a6df  8b5620                mov       edx,[esi+20h]
  0043a6e2  c1e00c                shl       eax,0Ch
  0043a6e5  03d0                  add       edx,eax
  0043a6e7  66ff4e14              dec       word [esi+14h]
  0043a6eb  66837e1400            cmp       word [esi+14h],0
  0043a6f0  895620                mov       [esi+20h],edx
  0043a6f3  752b                  jne       short 0043A720h
  0043a6f5  6af6                  push      0FFFFFFF6h
  0043a6f7  6af6                  push      0FFFFFFF6h
  0043a6f9  6af6                  push      0FFFFFFF6h
  0043a6fb  56                    push      esi
  0043a6fc  e8dfa6fcff            call      00404DE0h
  0043a701  668b4e12              mov       cx,[esi+12h]
  0043a705  83c410                add       esp,10h
  0043a708  66894e14              mov       [esi+14h],cx
  0043a70c  8a4e40                mov       cl,[esi+40h]
  0043a70f  fec1                  inc       cl
  0043a711  8ac1                  mov       al,cl
  0043a713  884e40                mov       [esi+40h],cl
  0043a716  3c04                  cmp       al,4
  0043a718  7506                  jne       short 0043A720h
  0043a71a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043a720:
  0043a720  5e                    pop       esi
  0043a721  c3                    ret
  0043a722  90                    nop
  0043a723  90                    nop
  0043a724  90                    nop
  0043a725  90                    nop
  0043a726  90                    nop
  0043a727  90                    nop
  0043a728  90                    nop
  0043a729  90                    nop
  0043a72a  90                    nop
  0043a72b  90                    nop
  0043a72c  90                    nop
  0043a72d  90                    nop
  0043a72e  90                    nop
  0043a72f  90                    nop

2 cible(s) de saut interne : 0x0043a6d5, 0x0043a720
