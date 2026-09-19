Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470260  section .text  offset fichier 0x6f660  112 octets

  00470260  56                    push      esi
  00470261  8b742408              mov       esi,[esp+8]
  00470265  66837e1000            cmp       word [esi+10h],0
  0047026a  7521                  jne       short 0047028Dh
  0047026c  0fbf4630              movsx     eax,word [esi+30h]
  00470270  0fbf4e14              movsx     ecx,word [esi+14h]
  00470274  0fbf5616              movsx     edx,word [esi+16h]
  00470278  66c746106300          mov       word [esi+10h],63h
  0047027e  894654                mov       [esi+54h],eax
  00470281  66c746300000          mov       word [esi+30h],0
  00470287  894e50                mov       [esi+50h],ecx
  0047028a  895658                mov       [esi+58h],edx

LAB_0047028d:
  0047028d  66837e1001            cmp       word [esi+10h],1
  00470292  7531                  jne       short 004702C5h
  00470294  6800150000            push      1500h
  00470299  6800150000            push      1500h
  0047029e  68dc050000            push      5DCh
  004702a3  6a26                  push      26h
  004702a5  e8f6cb0c00            call      0053CEA0h
  004702aa  83c410                add       esp,10h
  004702ad  6aff                  push      0FFFFFFFFh
  004702af  6a00                  push      0
  004702b1  68ff000000            push      0FFh
  004702b6  56                    push      esi
  004702b7  e824a80000            call      0047AAE0h
  004702bc  83c410                add       esp,10h
  004702bf  66c746106300          mov       word [esi+10h],63h

LAB_004702c5:
  004702c5  5e                    pop       esi
  004702c6  c3                    ret
  004702c7  90                    nop
  004702c8  90                    nop
  004702c9  90                    nop
  004702ca  90                    nop
  004702cb  90                    nop
  004702cc  90                    nop
  004702cd  90                    nop
  004702ce  90                    nop
  004702cf  90                    nop

2 cible(s) de saut interne : 0x0047028d, 0x004702c5
