Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470190  section .text  offset fichier 0x6f590  192 octets

  00470190  56                    push      esi
  00470191  8b742408              mov       esi,[esp+8]
  00470195  66837e1001            cmp       word [esi+10h],1
  0047019a  751e                  jne       short 004701BAh
  0047019c  0fbf4632              movsx     eax,word [esi+32h]
  004701a0  50                    push      eax
  004701a1  e876580d00            call      00545A1Ch
  004701a6  8a4e12                mov       cl,[esi+12h]
  004701a9  d3e0                  shl       eax,cl
  004701ab  b900c0f9ff            mov       ecx,0FFF9C000h
  004701b0  2bc8                  sub       ecx,eax
  004701b2  8b4620                mov       eax,[esi+20h]
  004701b5  03c1                  add       eax,ecx
  004701b7  894620                mov       [esi+20h],eax

LAB_004701ba:
  004701ba  66837e1000            cmp       word [esi+10h],0
  004701bf  7514                  jne       short 004701D5h
  004701c1  668b5614              mov       dx,[esi+14h]
  004701c5  66c746100100          mov       word [esi+10h],1
  004701cb  66895612              mov       [esi+12h],dx
  004701cf  66c746140000          mov       word [esi+14h],0

LAB_004701d5:
  004701d5  803d2ce55d0007        cmp       byte [5DE52Ch],7
  004701dc  7407                  je        short 004701E5h
  004701de  6842a80000            push      0A842h
  004701e3  eb05                  jmp       short 004701EAh

LAB_004701e5:
  004701e5  6802080000            push      802h

LAB_004701ea:
  004701ea  56                    push      esi
  004701eb  e84059feff            call      00455B30h
  004701f0  6683463244            add       word [esi+32h],44h
  004701f5  668b4632              mov       ax,[esi+32h]
  004701f9  83c408                add       esp,8
  004701fc  663d0010              cmp       ax,1000h
  00470200  7c09                  jl        short 0047020Bh
  00470202  0500f0ffff            add       eax,0FFFFF000h
  00470207  66894632              mov       [esi+32h],ax

LAB_0047020b:
  0047020b  0fbf4632              movsx     eax,word [esi+32h]
  0047020f  50                    push      eax
  00470210  e807580d00            call      00545A1Ch
  00470215  8a4e12                mov       cl,[esi+12h]
  00470218  56                    push      esi
  00470219  d3e0                  shl       eax,cl
  0047021b  8b4e20                mov       ecx,[esi+20h]
  0047021e  0500400600            add       eax,64000h
  00470223  03c8                  add       ecx,eax
  00470225  894e20                mov       [esi+20h],ecx
  00470228  e8a34a0c00            call      00534CD0h
  0047022d  8b06                  mov       eax,[esi]
  0047022f  83c404                add       esp,4
  00470232  a900000020            test      eax,20000000h
  00470237  7407                  je        short 00470240h
  00470239  25ffffff7f            and       eax,7FFFFFFFh
  0047023e  8906                  mov       [esi],eax

LAB_00470240:
  00470240  5e                    pop       esi
  00470241  c3                    ret
  00470242  90                    nop
  00470243  90                    nop
  00470244  90                    nop
  00470245  90                    nop
  00470246  90                    nop
  00470247  90                    nop
  00470248  90                    nop
  00470249  90                    nop
  0047024a  90                    nop
  0047024b  90                    nop
  0047024c  90                    nop
  0047024d  90                    nop
  0047024e  90                    nop
  0047024f  90                    nop

6 cible(s) de saut interne : 0x004701ba, 0x004701d5, 0x004701e5, 0x004701ea, 0x0047020b, 0x00470240
