Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e770  section .text  offset fichier 0x6db70  288 octets

  0046e770  a038e05d00            mov       al,[5DE038h]
  0046e775  56                    push      esi
  0046e776  84c0                  test      al,al
  0046e778  57                    push      edi
  0046e779  0f8508010000          jne       near 0046E887h
  0046e77f  8b74240c              mov       esi,[esp+0Ch]
  0046e783  a168e05d00            mov       eax,[5DE068h]
  0046e788  c1f80c                sar       eax,0Ch
  0046e78b  8b4e1c                mov       ecx,[esi+1Ch]
  0046e78e  8b5620                mov       edx,[esi+20h]
  0046e791  c1f90c                sar       ecx,0Ch
  0046e794  8b7e24                mov       edi,[esi+24h]
  0046e797  2bc1                  sub       eax,ecx
  0046e799  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  0046e79f  c1fa0c                sar       edx,0Ch
  0046e7a2  c1f90c                sar       ecx,0Ch
  0046e7a5  2bca                  sub       ecx,edx
  0046e7a7  8b1570e05d00          mov       edx,[5DE070h]
  0046e7ad  c1ff0c                sar       edi,0Ch
  0046e7b0  c1fa0c                sar       edx,0Ch
  0046e7b3  2bd7                  sub       edx,edi
  0046e7b5  8bf8                  mov       edi,eax
  0046e7b7  0faff8                imul      edi,eax
  0046e7ba  8bc1                  mov       eax,ecx
  0046e7bc  0fafc1                imul      eax,ecx
  0046e7bf  8bca                  mov       ecx,edx
  0046e7c1  03f8                  add       edi,eax
  0046e7c3  0fafca                imul      ecx,edx
  0046e7c6  03f9                  add       edi,ecx
  0046e7c8  57                    push      edi
  0046e7c9  e842720d00            call      00545A10h
  0046e7ce  3db80b0000            cmp       eax,0BB8h
  0046e7d3  bf01000000            mov       edi,1
  0046e7d8  7d13                  jge       short 0046E7EDh
  0046e7da  66837e1000            cmp       word [esi+10h],0
  0046e7df  7512                  jne       short 0046E7F3h
  0046e7e1  66897e10              mov       [esi+10h],di
  0046e7e5  66c746120000          mov       word [esi+12h],0
  0046e7eb  eb06                  jmp       short 0046E7F3h

LAB_0046e7ed:
  0046e7ed  66c746100000          mov       word [esi+10h],0

LAB_0046e7f3:
  0046e7f3  66397e10              cmp       [esi+10h],di
  0046e7f7  7561                  jne       short 0046E85Ah
  0046e7f9  66ff4612              inc       word [esi+12h]
  0046e7fd  668b4612              mov       ax,[esi+12h]
  0046e801  663d1e00              cmp       ax,1Eh
  0046e805  7e18                  jle       short 0046E81Fh
  0046e807  663d2a00              cmp       ax,2Ah
  0046e80b  7d12                  jge       short 0046E81Fh
  0046e80d  6aff                  push      0FFFFFFFFh
  0046e80f  6a00                  push      0
  0046e811  68cb000000            push      0CBh
  0046e816  56                    push      esi
  0046e817  e8b4c10000            call      0047A9D0h
  0046e81c  83c410                add       esp,10h

LAB_0046e81f:
  0046e81f  e81c890d00            call      00547140h
  0046e824  99                    cdq
  0046e825  b964000000            mov       ecx,64h
  0046e82a  f7f9                  idiv      ecx
  0046e82c  668b4612              mov       ax,[esi+12h]
  0046e830  663bc1                cmp       ax,cx
  0046e833  7f0b                  jg        short 0046E840h
  0046e835  663d3200              cmp       ax,32h
  0046e839  7e1f                  jle       short 0046E85Ah
  0046e83b  663bd7                cmp       dx,di
  0046e83e  751a                  jne       short 0046E85Ah

LAB_0046e840:
  0046e840  e8fb880d00            call      00547140h
  0046e845  99                    cdq
  0046e846  b905000000            mov       ecx,5
  0046e84b  66c746100200          mov       word [esi+10h],2
  0046e851  f7f9                  idiv      ecx
  0046e853  83c20a                add       edx,0Ah
  0046e856  66895612              mov       [esi+12h],dx

LAB_0046e85a:
  0046e85a  66837e1002            cmp       word [esi+10h],2
  0046e85f  7526                  jne       short 0046E887h
  0046e861  66ff4e12              dec       word [esi+12h]
  0046e865  66837e1200            cmp       word [esi+12h],0
  0046e86a  7c15                  jl        short 0046E881h
  0046e86c  6aff                  push      0FFFFFFFFh
  0046e86e  6a00                  push      0
  0046e870  68ca000000            push      0CAh
  0046e875  56                    push      esi
  0046e876  e855c10000            call      0047A9D0h
  0046e87b  83c410                add       esp,10h
  0046e87e  5f                    pop       edi
  0046e87f  5e                    pop       esi
  0046e880  c3                    ret

LAB_0046e881:
  0046e881  66c746100000          mov       word [esi+10h],0

LAB_0046e887:
  0046e887  5f                    pop       edi
  0046e888  5e                    pop       esi
  0046e889  c3                    ret
  0046e88a  90                    nop
  0046e88b  90                    nop
  0046e88c  90                    nop
  0046e88d  90                    nop
  0046e88e  90                    nop
  0046e88f  90                    nop

7 cible(s) de saut interne : 0x0046e7ed, 0x0046e7f3, 0x0046e81f, 0x0046e840, 0x0046e85a, 0x0046e881, 0x0046e887
