Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044f750  section .text  offset fichier 0x4eb50  480 octets

  0044f750  56                    push      esi
  0044f751  8b742408              mov       esi,[esp+8]
  0044f755  57                    push      edi
  0044f756  8b466c                mov       eax,[esi+6Ch]
  0044f759  85c0                  test      eax,eax
  0044f75b  7505                  jne       short 0044F762h
  0044f75d  b858e55d00            mov       eax,5DE558h

LAB_0044f762:
  0044f762  8b481c                mov       ecx,[eax+1Ch]
  0044f765  894e50                mov       [esi+50h],ecx
  0044f768  8b5020                mov       edx,[eax+20h]
  0044f76b  895654                mov       [esi+54h],edx
  0044f76e  8b4024                mov       eax,[eax+24h]
  0044f771  894658                mov       [esi+58h],eax
  0044f774  8b4678                mov       eax,[esi+78h]
  0044f777  85c0                  test      eax,eax
  0044f779  7505                  jne       short 0044F780h
  0044f77b  b858e55d00            mov       eax,5DE558h

LAB_0044f780:
  0044f780  50                    push      eax
  0044f781  e83a940e00            call      00538BC0h
  0044f786  8d0440                lea       eax,[eax+eax*2]
  0044f789  83c404                add       esp,4
  0044f78c  8d0c80                lea       ecx,[eax+eax*4]
  0044f78f  d1e1                  shl       ecx,1
  0044f791  81f900100000          cmp       ecx,1000h
  0044f797  7d05                  jge       short 0044F79Eh
  0044f799  b900100000            mov       ecx,1000h

LAB_0044f79e:
  0044f79e  81f930750000          cmp       ecx,7530h
  0044f7a4  7e05                  jle       short 0044F7ABh
  0044f7a6  b930750000            mov       ecx,7530h

LAB_0044f7ab:
  0044f7ab  b81f85eb51            mov       eax,51EB851Fh
  0044f7b0  f7e9                  imul      ecx
  0044f7b2  c1fa05                sar       edx,5
  0044f7b5  8bca                  mov       ecx,edx
  0044f7b7  c1e91f                shr       ecx,1Fh
  0044f7ba  03d1                  add       edx,ecx
  0044f7bc  66837e1000            cmp       word [esi+10h],0
  0044f7c1  8bfa                  mov       edi,edx
  0044f7c3  0f85c6000000          jne       near 0044F88Fh
  0044f7c9  c7463c48155500        mov       dword [esi+3Ch],551548h
  0044f7d0  c6464000              mov       byte [esi+40h],0
  0044f7d4  b0ff                  mov       al,0FFh
  0044f7d6  c6054c15550002        mov       byte [55154Ch],2
  0044f7dd  c6460a06              mov       byte [esi+0Ah],6
  0044f7e1  c6460b00              mov       byte [esi+0Bh],0
  0044f7e5  884638                mov       [esi+38h],al
  0044f7e8  884639                mov       [esi+39h],al
  0044f7eb  88463a                mov       [esi+3Ah],al
  0044f7ee  e84d790f00            call      00547140h
  0044f7f3  99                    cdq
  0044f7f4  33c2                  xor       eax,edx
  0044f7f6  66c7462a0000          mov       word [esi+2Ah],0
  0044f7fc  2bc2                  sub       eax,edx
  0044f7fe  25ff070000            and       eax,7FFh
  0044f803  33c2                  xor       eax,edx
  0044f805  2bc2                  sub       eax,edx
  0044f807  0500080000            add       eax,800h
  0044f80c  66894628              mov       [esi+28h],ax
  0044f810  e82b790f00            call      00547140h
  0044f815  99                    cdq
  0044f816  b9c8000000            mov       ecx,0C8h
  0044f81b  f7f9                  idiv      ecx
  0044f81d  81c2f4010000          add       edx,1F4h
  0044f823  6689969a000000        mov       [esi+9Ah],dx
  0044f82a  e811790f00            call      00547140h
  0044f82f  99                    cdq
  0044f830  b905000000            mov       ecx,5
  0044f835  f7f9                  idiv      ecx
  0044f837  03d1                  add       edx,ecx
  0044f839  66895612              mov       [esi+12h],dx
  0044f83d  e8fe780f00            call      00547140h
  0044f842  99                    cdq
  0044f843  33c2                  xor       eax,edx
  0044f845  2bc2                  sub       eax,edx
  0044f847  25ff0f0000            and       eax,0FFFh
  0044f84c  33c2                  xor       eax,edx
  0044f84e  2bc2                  sub       eax,edx
  0044f850  66894614              mov       [esi+14h],ax
  0044f854  e8e7780f00            call      00547140h
  0044f859  99                    cdq
  0044f85a  f7ff                  idiv      edi
  0044f85c  8b4620                mov       eax,[esi+20h]
  0044f85f  8b4e24                mov       ecx,[esi+24h]
  0044f862  894654                mov       [esi+54h],eax
  0044f865  894e58                mov       [esi+58h],ecx
  0044f868  66895616              mov       [esi+16h],dx
  0044f86c  8b561c                mov       edx,[esi+1Ch]
  0044f86f  895650                mov       [esi+50h],edx
  0044f872  e8c9780f00            call      00547140h
  0044f877  99                    cdq
  0044f878  b90c000000            mov       ecx,0Ch
  0044f87d  66c746100100          mov       word [esi+10h],1
  0044f883  f7f9                  idiv      ecx
  0044f885  b8f6ffffff            mov       eax,0FFFFFFF6h
  0044f88a  2bc2                  sub       eax,edx
  0044f88c  89465c                mov       [esi+5Ch],eax

LAB_0044f88f:
  0044f88f  66837e1001            cmp       word [esi+10h],1
  0044f894  0f858a000000          jne       near 0044F924h
  0044f89a  0fbf4e14              movsx     ecx,word [esi+14h]
  0044f89e  51                    push      ecx
  0044f89f  e878610f00            call      00545A1Ch
  0044f8a4  0fbf5616              movsx     edx,word [esi+16h]
  0044f8a8  0fafc2                imul      eax,edx
  0044f8ab  034650                add       eax,[esi+50h]
  0044f8ae  89461c                mov       [esi+1Ch],eax
  0044f8b1  0fbf4614              movsx     eax,word [esi+14h]
  0044f8b5  50                    push      eax
  0044f8b6  e85b610f00            call      00545A16h
  0044f8bb  0fbf4e16              movsx     ecx,word [esi+16h]
  0044f8bf  0fafc1                imul      eax,ecx
  0044f8c2  8b5658                mov       edx,[esi+58h]
  0044f8c5  8b7e20                mov       edi,[esi+20h]
  0044f8c8  03c2                  add       eax,edx
  0044f8ca  894624                mov       [esi+24h],eax
  0044f8cd  668b4612              mov       ax,[esi+12h]
  0044f8d1  0fbfd0                movsx     edx,ax
  0044f8d4  83c002                add       eax,2
  0044f8d7  66894612              mov       [esi+12h],ax
  0044f8db  668b869a000000        mov       ax,[esi+9Ah]
  0044f8e2  6601462a              add       [esi+2Ah],ax
  0044f8e6  668b4e2a              mov       cx,[esi+2Ah]
  0044f8ea  c1e20c                shl       edx,0Ch
  0044f8ed  66c1f804              sar       ax,4
  0044f8f1  66294628              sub       [esi+28h],ax
  0044f8f5  03fa                  add       edi,edx
  0044f8f7  6681f93075            cmp       cx,7530h
  0044f8fc  897e20                mov       [esi+20h],edi
  0044f8ff  7e06                  jle       short 0044F907h
  0044f901  66c7462a3075          mov       word [esi+2Ah],7530h

LAB_0044f907:
  0044f907  668b465c              mov       ax,[esi+5Ch]
  0044f90b  50                    push      eax
  0044f90c  50                    push      eax
  0044f90d  50                    push      eax
  0044f90e  56                    push      esi
  0044f90f  e8cc54fbff            call      00404DE0h
  0044f914  8a4638                mov       al,[esi+38h]
  0044f917  83c410                add       esp,10h
  0044f91a  84c0                  test      al,al
  0044f91c  7506                  jne       short 0044F924h
  0044f91e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044f924:
  0044f924  5f                    pop       edi
  0044f925  5e                    pop       esi
  0044f926  c3                    ret
  0044f927  90                    nop
  0044f928  90                    nop
  0044f929  90                    nop
  0044f92a  90                    nop
  0044f92b  90                    nop
  0044f92c  90                    nop
  0044f92d  90                    nop
  0044f92e  90                    nop
  0044f92f  90                    nop

7 cible(s) de saut interne : 0x0044f762, 0x0044f780, 0x0044f79e, 0x0044f7ab, 0x0044f88f, 0x0044f907, 0x0044f924
