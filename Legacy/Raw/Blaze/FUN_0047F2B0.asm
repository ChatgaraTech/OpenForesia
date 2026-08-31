
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f2b0  section .text  offset fichier 0x7e6b0  336 octets

  0047f2b0  53                    push      ebx
  0047f2b1  55                    push      ebp
  0047f2b2  56                    push      esi
  0047f2b3  8b742410              mov       esi,[esp+10h]
  0047f2b7  57                    push      edi
  0047f2b8  8b0e                  mov       ecx,[esi]
  0047f2ba  8b4674                mov       eax,[esi+74h]
  0047f2bd  f7c100000002          test      ecx,2000000h
  0047f2c3  0f848a000000          je        near 0047F353h
  0047f2c9  6681463200fc          add       word [esi+32h],0FC00h
  0047f2cf  33c0                  xor       eax,eax
  0047f2d1  8a460b                mov       al,[esi+0Bh]
  0047f2d4  6aff                  push      0FFFFFFFFh
  0047f2d6  50                    push      eax
  0047f2d7  33ed                  xor       ebp,ebp
  0047f2d9  bf00100000            mov       edi,1000h
  0047f2de  6a6c                  push      6Ch
  0047f2e0  56                    push      esi
  0047f2e1  896e70                mov       [esi+70h],ebp
  0047f2e4  896e44                mov       [esi+44h],ebp
  0047f2e7  896e48                mov       [esi+48h],ebp
  0047f2ea  896e4c                mov       [esi+4Ch],ebp
  0047f2ed  897e50                mov       [esi+50h],edi
  0047f2f0  c7465400180000        mov       dword [esi+54h],1800h
  0047f2f7  897e58                mov       [esi+58h],edi
  0047f2fa  c7463880808000        mov       dword [esi+38h],808080h
  0047f301  896e28                mov       [esi+28h],ebp
  0047f304  66896e2c              mov       [esi+2Ch],bp
  0047f308  66c746340004          mov       word [esi+34h],400h
  0047f30e  c6460407              mov       byte [esi+4],7
  0047f312  e8c9b7ffff            call      0047AAE0h
  0047f317  83c410                add       esp,10h
  0047f31a  33c9                  xor       ecx,ecx
  0047f31c  8a4e0b                mov       cl,[esi+0Bh]
  0047f31f  896e50                mov       [esi+50h],ebp
  0047f322  6aff                  push      0FFFFFFFFh
  0047f324  51                    push      ecx
  0047f325  6886000000            push      86h
  0047f32a  56                    push      esi
  0047f32b  c746549d360000        mov       dword [esi+54h],369Dh
  0047f332  896e58                mov       [esi+58h],ebp
  0047f335  897e28                mov       [esi+28h],edi
  0047f338  66897e2c              mov       [esi+2Ch],di
  0047f33c  c6460405              mov       byte [esi+4],5
  0047f340  e89bb7ffff            call      0047AAE0h
  0047f345  83c410                add       esp,10h
  0047f348  66ff4610              inc       word [esi+10h]
  0047f34c  892e                  mov       [esi],ebp
  0047f34e  5f                    pop       edi
  0047f34f  5e                    pop       esi
  0047f350  5d                    pop       ebp
  0047f351  5b                    pop       ebx
  0047f352  c3                    ret

LAB_0047f353:
  0047f353  33ed                  xor       ebp,ebp
  0047f355  66396e10              cmp       [esi+10h],bp
  0047f359  0f8598000000          jne       near 0047F3F7h
  0047f35f  8b38                  mov       edi,[eax]
  0047f361  50                    push      eax
  0047f362  81cf80000000          or        edi,80h
  0047f368  8938                  mov       [eax],edi
  0047f36a  e851980b00            call      00538BC0h
  0047f36f  8bf8                  mov       edi,eax
  0047f371  83c404                add       esp,4
  0047f374  0fbf4632              movsx     eax,word [esi+32h]
  0047f378  50                    push      eax
  0047f379  89442418              mov       [esp+18h],eax
  0047f37d  d1ff                  sar       edi,1
  0047f37f  8d5e1c                lea       ebx,[esi+1Ch]
  0047f382  e895660c00            call      00545A1Ch
  0047f387  0fafc7                imul      eax,edi
  0047f38a  8b0b                  mov       ecx,[ebx]
  0047f38c  8b542414              mov       edx,[esp+14h]
  0047f390  03c8                  add       ecx,eax
  0047f392  52                    push      edx
  0047f393  890b                  mov       [ebx],ecx
  0047f395  e87c660c00            call      00545A16h
  0047f39a  0fafc7                imul      eax,edi
  0047f39d  8b5624                mov       edx,[esi+24h]
  0047f3a0  6aff                  push      0FFFFFFFFh
  0047f3a2  03d0                  add       edx,eax
  0047f3a4  8b4620                mov       eax,[esi+20h]
  0047f3a7  c1e70c                shl       edi,0Ch
  0047f3aa  03c7                  add       eax,edi
  0047f3ac  895624                mov       [esi+24h],edx
  0047f3af  894620                mov       [esi+20h],eax
  0047f3b2  b802000000            mov       eax,2
  0047f3b7  66894614              mov       [esi+14h],ax
  0047f3bb  884604                mov       [esi+4],al
  0047f3be  33c0                  xor       eax,eax
  0047f3c0  896e38                mov       [esi+38h],ebp
  0047f3c3  8a460b                mov       al,[esi+0Bh]
  0047f3c6  896e28                mov       [esi+28h],ebp
  0047f3c9  50                    push      eax
  0047f3ca  6885000000            push      85h
  0047f3cf  56                    push      esi
  0047f3d0  66896e2c              mov       [esi+2Ch],bp
  0047f3d4  66c746120008          mov       word [esi+12h],800h
  0047f3da  e801b7ffff            call      0047AAE0h
  0047f3df  83c410                add       esp,10h
  0047f3e2  53                    push      ebx
  0047f3e3  6800120000            push      1200h
  0047f3e8  6800080000            push      800h
  0047f3ed  6a10                  push      10h
  0047f3ef  e82ce70b00            call      0053DB20h
  0047f3f4  83c410                add       esp,10h

LAB_0047f3f7:
  0047f3f7  66ff4610              inc       word [esi+10h]
  0047f3fb  5f                    pop       edi
  0047f3fc  5e                    pop       esi
  0047f3fd  5d                    pop       ebp
  0047f3fe  5b                    pop       ebx
  0047f3ff  c3                    ret

2 cible(s) de saut interne : 0x0047f353, 0x0047f3f7
