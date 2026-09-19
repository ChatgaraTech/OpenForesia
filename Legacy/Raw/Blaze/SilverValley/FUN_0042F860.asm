Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f860  section .text  offset fichier 0x2ec60  272 octets

  0042f860  53                    push      ebx
  0042f861  56                    push      esi
  0042f862  8b74240c              mov       esi,[esp+0Ch]
  0042f866  33db                  xor       ebx,ebx
  0042f868  66395e10              cmp       [esi+10h],bx
  0042f86c  0f8585000000          jne       near 0042F8F7h
  0042f872  b800080000            mov       eax,800h
  0042f877  66c746100100          mov       word [esi+10h],1
  0042f87d  c7463cd00a5500        mov       dword [esi+3Ch],550AD0h
  0042f884  885e40                mov       [esi+40h],bl
  0042f887  c6460a01              mov       byte [esi+0Ah],1
  0042f88b  885e0b                mov       [esi+0Bh],bl
  0042f88e  66894628              mov       [esi+28h],ax
  0042f892  6689462a              mov       [esi+2Ah],ax
  0042f896  66895e12              mov       [esi+12h],bx
  0042f89a  885e38                mov       [esi+38h],bl
  0042f89d  885e39                mov       [esi+39h],bl
  0042f8a0  885e3a                mov       [esi+3Ah],bl
  0042f8a3  e898781100            call      00547140h
  0042f8a8  99                    cdq
  0042f8a9  b9c8000000            mov       ecx,0C8h
  0042f8ae  f7f9                  idiv      ecx
  0042f8b0  8b461c                mov       eax,[esi+1Ch]
  0042f8b3  83ea64                sub       edx,64h
  0042f8b6  c1e20c                shl       edx,0Ch
  0042f8b9  03c2                  add       eax,edx
  0042f8bb  89461c                mov       [esi+1Ch],eax
  0042f8be  e87d781100            call      00547140h
  0042f8c3  99                    cdq
  0042f8c4  b9c8000000            mov       ecx,0C8h
  0042f8c9  f7f9                  idiv      ecx
  0042f8cb  8b4e24                mov       ecx,[esi+24h]
  0042f8ce  83ea64                sub       edx,64h
  0042f8d1  c1e20c                shl       edx,0Ch
  0042f8d4  03ca                  add       ecx,edx
  0042f8d6  894e24                mov       [esi+24h],ecx
  0042f8d9  e862781100            call      00547140h
  0042f8de  99                    cdq
  0042f8df  b932000000            mov       ecx,32h
  0042f8e4  f7f9                  idiv      ecx
  0042f8e6  8b4620                mov       eax,[esi+20h]
  0042f8e9  81c2c8000000          add       edx,0C8h
  0042f8ef  c1e20c                shl       edx,0Ch
  0042f8f2  03c2                  add       eax,edx
  0042f8f4  894620                mov       [esi+20h],eax

LAB_0042f8f7:
  0042f8f7  66837e1001            cmp       word [esi+10h],1
  0042f8fc  7566                  jne       short 0042F964h
  0042f8fe  807e4002              cmp       byte [esi+40h],2
  0042f902  770f                  ja        short 0042F913h
  0042f904  6a20                  push      20h
  0042f906  6a20                  push      20h
  0042f908  6a20                  push      20h
  0042f90a  56                    push      esi
  0042f90b  e8d054fdff            call      00404DE0h
  0042f910  83c410                add       esp,10h

LAB_0042f913:
  0042f913  807e4003              cmp       byte [esi+40h],3
  0042f917  720f                  jb        short 0042F928h
  0042f919  6aec                  push      0FFFFFFECh
  0042f91b  6aec                  push      0FFFFFFECh
  0042f91d  6aec                  push      0FFFFFFECh
  0042f91f  56                    push      esi
  0042f920  e8bb54fdff            call      00404DE0h
  0042f925  83c410                add       esp,10h

LAB_0042f928:
  0042f928  66ff4612              inc       word [esi+12h]
  0042f92c  66837e1201            cmp       word [esi+12h],1
  0042f931  7c31                  jl        short 0042F964h
  0042f933  8a4e40                mov       cl,[esi+40h]
  0042f936  66895e12              mov       [esi+12h],bx
  0042f93a  fec1                  inc       cl
  0042f93c  8ac1                  mov       al,cl
  0042f93e  884e40                mov       [esi+40h],cl
  0042f941  3c07                  cmp       al,7
  0042f943  751f                  jne       short 0042F964h
  0042f945  6aff                  push      0FFFFFFFFh
  0042f947  53                    push      ebx
  0042f948  68c1000000            push      0C1h
  0042f94d  8d561c                lea       edx,[esi+1Ch]
  0042f950  53                    push      ebx
  0042f951  52                    push      edx
  0042f952  56                    push      esi
  0042f953  e8e8b20400            call      0047AC40h
  0042f958  8b06                  mov       eax,[esi]
  0042f95a  83c418                add       esp,18h
  0042f95d  25ffffff7f            and       eax,7FFFFFFFh
  0042f962  8906                  mov       [esi],eax

LAB_0042f964:
  0042f964  5e                    pop       esi
  0042f965  5b                    pop       ebx
  0042f966  c3                    ret
  0042f967  90                    nop
  0042f968  90                    nop
  0042f969  90                    nop
  0042f96a  90                    nop
  0042f96b  90                    nop
  0042f96c  90                    nop
  0042f96d  90                    nop
  0042f96e  90                    nop
  0042f96f  90                    nop

4 cible(s) de saut interne : 0x0042f8f7, 0x0042f913, 0x0042f928, 0x0042f964
