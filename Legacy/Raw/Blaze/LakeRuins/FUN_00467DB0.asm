Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00467db0  section .text  offset fichier 0x671b0  208 octets

  00467db0  53                    push      ebx
  00467db1  56                    push      esi
  00467db2  8b74240c              mov       esi,[esp+0Ch]
  00467db6  33db                  xor       ebx,ebx
  00467db8  66395e10              cmp       [esi+10h],bx
  00467dbc  7512                  jne       short 00467DD0h
  00467dbe  66c746100100          mov       word [esi+10h],1
  00467dc4  66895e12              mov       [esi+12h],bx
  00467dc8  66895e14              mov       [esi+14h],bx
  00467dcc  66895e16              mov       [esi+16h],bx

LAB_00467dd0:
  00467dd0  66837e1001            cmp       word [esi+10h],1
  00467dd5  751e                  jne       short 00467DF5h
  00467dd7  e864f30d00            call      00547140h
  00467ddc  99                    cdq
  00467ddd  b9a0000000            mov       ecx,0A0h
  00467de2  66c746100200          mov       word [esi+10h],2
  00467de8  f7f9                  idiv      ecx
  00467dea  66895e14              mov       [esi+14h],bx
  00467dee  83c250                add       edx,50h
  00467df1  66895612              mov       [esi+12h],dx

LAB_00467df5:
  00467df5  66837e1002            cmp       word [esi+10h],2
  00467dfa  752f                  jne       short 00467E2Bh
  00467dfc  66ff4614              inc       word [esi+14h]
  00467e00  668b4614              mov       ax,[esi+14h]
  00467e04  66394612              cmp       [esi+12h],ax
  00467e08  7521                  jne       short 00467E2Bh
  00467e0a  66c746100300          mov       word [esi+10h],3
  00467e10  66895e12              mov       [esi+12h],bx
  00467e14  e827f30d00            call      00547140h
  00467e19  99                    cdq
  00467e1a  33c2                  xor       eax,edx
  00467e1c  2bc2                  sub       eax,edx
  00467e1e  25ff030000            and       eax,3FFh
  00467e23  33c2                  xor       eax,edx
  00467e25  2bc2                  sub       eax,edx
  00467e27  66894614              mov       [esi+14h],ax

LAB_00467e2b:
  00467e2b  66837e1003            cmp       word [esi+10h],3
  00467e30  7545                  jne       short 00467E77h
  00467e32  66395e12              cmp       [esi+12h],bx
  00467e36  752c                  jne       short 00467E64h
  00467e38  0fbf4614              movsx     eax,word [esi+14h]
  00467e3c  8b15cc975e00          mov       edx,[5E97CCh]
  00467e42  0500030000            add       eax,300h
  00467e47  50                    push      eax
  00467e48  50                    push      eax
  00467e49  8b82a8000000          mov       eax,[edx+0A8h]
  00467e4f  33d2                  xor       edx,edx
  00467e51  6800030000            push      300h
  00467e56  8b08                  mov       ecx,[eax]
  00467e58  8a5111                mov       dl,[ecx+11h]
  00467e5b  52                    push      edx
  00467e5c  e83f500d00            call      0053CEA0h
  00467e61  83c410                add       esp,10h

LAB_00467e64:
  00467e64  66ff4612              inc       word [esi+12h]
  00467e68  b801000000            mov       eax,1
  00467e6d  66394612              cmp       [esi+12h],ax
  00467e71  7504                  jne       short 00467E77h
  00467e73  66894610              mov       [esi+10h],ax

LAB_00467e77:
  00467e77  5e                    pop       esi
  00467e78  5b                    pop       ebx
  00467e79  c3                    ret
  00467e7a  90                    nop
  00467e7b  90                    nop
  00467e7c  90                    nop
  00467e7d  90                    nop
  00467e7e  90                    nop
  00467e7f  90                    nop

5 cible(s) de saut interne : 0x00467dd0, 0x00467df5, 0x00467e2b, 0x00467e64, 0x00467e77
