
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f570  section .text  offset fichier 0x7e970  1425 octets

  0047f570  83ec24                sub       esp,24h
  0047f573  53                    push      ebx
  0047f574  55                    push      ebp
  0047f575  56                    push      esi
  0047f576  8b742434              mov       esi,[esp+34h]
  0047f57a  b832000000            mov       eax,32h
  0047f57f  57                    push      edi
  0047f580  6689442420            mov       [esp+20h],ax
  0047f585  6689442426            mov       [esp+26h],ax
  0047f58a  8b06                  mov       eax,[esi]
  0047f58c  33ff                  xor       edi,edi
  0047f58e  a900000002            test      eax,2000000h
  0047f593  66897c241c            mov       [esp+1Ch],di
  0047f598  66c744241eceff        mov       word [esp+1Eh],0FFCEh
  0047f59f  66c74424226400        mov       word [esp+22h],64h
  0047f5a6  66897c2424            mov       [esp+24h],di
  0047f5ab  66897c2428            mov       [esp+28h],di
  0047f5b0  66c744242a00ff        mov       word [esp+2Ah],0FF00h
  0047f5b7  66c744242c0001        mov       word [esp+2Ch],100h
  0047f5be  66c744242e9001        mov       word [esp+2Eh],190h
  0047f5c5  66897c2430            mov       [esp+30h],di
  0047f5ca  66c74424320002        mov       word [esp+32h],200h
  0047f5d1  0f84f0000000          je        near 0047F6C7h
  0047f5d7  66837e100a            cmp       word [esi+10h],0Ah
  0047f5dc  0f8dd2000000          jge       near 0047F6B4h
  0047f5e2  a900000040            test      eax,40000000h
  0047f5e7  0f85c7000000          jne       near 0047F6B4h
  0047f5ed  66c7461a0700          mov       word [esi+1Ah],7
  0047f5f3  897e70                mov       [esi+70h],edi
  0047f5f6  c7463860606001        mov       dword [esi+38h],1606060h
  0047f5fd  e83e7b0c00            call      00547140h
  0047f602  8be8                  mov       ebp,eax
  0047f604  55                    push      ebp
  0047f605  e812640c00            call      00545A1Ch
  0047f60a  c1e006                shl       eax,6
  0047f60d  55                    push      ebp
  0047f60e  894650                mov       [esi+50h],eax
  0047f611  c7465400000400        mov       dword [esi+54h],40000h
  0047f618  e8f9630c00            call      00545A16h
  0047f61d  c1e006                shl       eax,6
  0047f620  894658                mov       [esi+58h],eax
  0047f623  66c746120300          mov       word [esi+12h],3
  0047f629  c7462800200020        mov       dword [esi+28h],20002000h
  0047f630  66c746140050          mov       word [esi+14h],5000h
  0047f636  66c746160200          mov       word [esi+16h],2
  0047f63c  e8ff7a0c00            call      00547140h
  0047f641  8b561c                mov       edx,[esi+1Ch]
  0047f644  25ff010000            and       eax,1FFh
  0047f649  2d00010000            sub       eax,100h
  0047f64e  c1e00c                shl       eax,0Ch
  0047f651  03c2                  add       eax,edx
  0047f653  89442410              mov       [esp+10h],eax
  0047f657  8b4620                mov       eax,[esi+20h]
  0047f65a  89442414              mov       [esp+14h],eax
  0047f65e  e8dd7a0c00            call      00547140h
  0047f663  8b4e24                mov       ecx,[esi+24h]
  0047f666  25ff010000            and       eax,1FFh
  0047f66b  2d00010000            sub       eax,100h
  0047f670  6a02                  push      2
  0047f672  c1e00c                shl       eax,0Ch
  0047f675  03c1                  add       eax,ecx
  0047f677  57                    push      edi
  0047f678  68be000000            push      0BEh
  0047f67d  8d4c241c              lea       ecx,[esp+1Ch]
  0047f681  68081a5500            push      551A08h
  0047f686  51                    push      ecx
  0047f687  56                    push      esi
  0047f688  89442430              mov       [esp+30h],eax
  0047f68c  e8afb5ffff            call      0047AC40h
  0047f691  a1ace05d00            mov       eax,[5DE0ACh]
  0047f696  83c418                add       esp,18h
  0047f699  3bc7                  cmp       eax,edi
  0047f69b  740d                  je        short 0047F6AAh
  0047f69d  8bd0                  mov       edx,eax
  0047f69f  c1fa02                sar       edx,2
  0047f6a2  2bd0                  sub       edx,eax
  0047f6a4  8915ace05d00          mov       [5DE0ACh],edx

LAB_0047f6aa:
  0047f6aa  b801000000            mov       eax,1
  0047f6af  e9df030000            jmp       0047FA93h

LAB_0047f6b4:
  0047f6b4  893e                  mov       [esi],edi
  0047f6b6  8b7674                mov       esi,[esi+74h]
  0047f6b9  8b06                  mov       eax,[esi]
  0047f6bb  247f                  and       al,7Fh
  0047f6bd  8906                  mov       [esi],eax
  0047f6bf  5f                    pop       edi
  0047f6c0  5e                    pop       esi
  0047f6c1  5d                    pop       ebp
  0047f6c2  5b                    pop       ebx
  0047f6c3  83c424                add       esp,24h
  0047f6c6  c3                    ret

LAB_0047f6c7:
  0047f6c7  66397e10              cmp       [esi+10h],di
  0047f6cb  0f85bd000000          jne       near 0047F78Eh
  0047f6d1  8b4674                mov       eax,[esi+74h]
  0047f6d4  8b28                  mov       ebp,[eax]
  0047f6d6  81cd80000000          or        ebp,80h
  0047f6dc  8928                  mov       [eax],ebp
  0047f6de  8b1e                  mov       ebx,[esi]
  0047f6e0  8b4674                mov       eax,[esi+74h]
  0047f6e3  80cb80                or        bl,80h
  0047f6e6  0fbf6e32              movsx     ebp,word [esi+32h]
  0047f6ea  50                    push      eax
  0047f6eb  891e                  mov       [esi],ebx
  0047f6ed  897670                mov       [esi+70h],esi
  0047f6f0  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047f6f6  e8c5940b00            call      00538BC0h
  0047f6fb  8bd8                  mov       ebx,eax
  0047f6fd  8b4620                mov       eax,[esi+20h]
  0047f700  d1fb                  sar       ebx,1
  0047f702  8bcb                  mov       ecx,ebx
  0047f704  83c404                add       esp,4
  0047f707  c1e10c                shl       ecx,0Ch
  0047f70a  03c1                  add       eax,ecx
  0047f70c  55                    push      ebp
  0047f70d  894620                mov       [esi+20h],eax
  0047f710  e807630c00            call      00545A1Ch
  0047f715  0fafc3                imul      eax,ebx
  0047f718  8b4e1c                mov       ecx,[esi+1Ch]
  0047f71b  55                    push      ebp
  0047f71c  03c8                  add       ecx,eax
  0047f71e  894e1c                mov       [esi+1Ch],ecx
  0047f721  e8f0620c00            call      00545A16h
  0047f726  0fafc3                imul      eax,ebx
  0047f729  8b4e24                mov       ecx,[esi+24h]
  0047f72c  6a02                  push      2
  0047f72e  57                    push      edi
  0047f72f  6a51                  push      51h
  0047f731  8d5e1c                lea       ebx,[esi+1Ch]
  0047f734  68281b5500            push      551B28h
  0047f739  03c8                  add       ecx,eax
  0047f73b  53                    push      ebx
  0047f73c  56                    push      esi
  0047f73d  894e24                mov       [esi+24h],ecx
  0047f740  897e28                mov       [esi+28h],edi
  0047f743  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047f74a  66c7461a0300          mov       word [esi+1Ah],3
  0047f750  e8ebb4ffff            call      0047AC40h
  0047f755  83c418                add       esp,18h
  0047f758  c74628ff7f0000        mov       dword [esi+28h],7FFFh
  0047f75f  66c7462c0004          mov       word [esi+2Ch],400h
  0047f765  897e38                mov       [esi+38h],edi
  0047f768  53                    push      ebx
  0047f769  6800120000            push      1200h
  0047f76e  6855010000            push      155h
  0047f773  6a16                  push      16h
  0047f775  66c74612f401          mov       word [esi+12h],1F4h
  0047f77b  897e44                mov       [esi+44h],edi
  0047f77e  897e48                mov       [esi+48h],edi
  0047f781  897e4c                mov       [esi+4Ch],edi
  0047f784  e897e30b00            call      0053DB20h
  0047f789  83c410                add       esp,10h
  0047f78c  eb04                  jmp       short 0047F792h

LAB_0047f78e:
  0047f78e  8b6c2438              mov       ebp,[esp+38h]

LAB_0047f792:
  0047f792  668b4610              mov       ax,[esi+10h]
  0047f796  663d1400              cmp       ax,14h
  0047f79a  0f8da1000000          jge       near 0047F841h
  0047f7a0  663d0a00              cmp       ax,0Ah
  0047f7a4  7d31                  jge       short 0047F7D7h
  0047f7a6  0fbf6e32              movsx     ebp,word [esi+32h]
  0047f7aa  e891790c00            call      00547140h
  0047f7af  0500040000            add       eax,400h
  0047f7b4  66894632              mov       [esi+32h],ax
  0047f7b8  e883790c00            call      00547140h
  0047f7bd  6a02                  push      2
  0047f7bf  57                    push      edi
  0047f7c0  6a70                  push      70h
  0047f7c2  56                    push      esi
  0047f7c3  66894634              mov       [esi+34h],ax
  0047f7c7  c646043f              mov       byte [esi+4],3Fh
  0047f7cb  e810b3ffff            call      0047AAE0h
  0047f7d0  83c410                add       esp,10h
  0047f7d3  66896e32              mov       [esi+32h],bp

LAB_0047f7d7:
  0047f7d7  66837e1013            cmp       word [esi+10h],13h
  0047f7dc  0f859b020000          jne       near 0047FA7Dh
  0047f7e2  668b4632              mov       ax,[esi+32h]
  0047f7e6  8b5e70                mov       ebx,[esi+70h]
  0047f7e9  6a04                  push      4
  0047f7eb  57                    push      edi
  0047f7ec  0fbfe8                movsx     ebp,ax
  0047f7ef  b900100000            mov       ecx,1000h
  0047f7f4  0500fcffff            add       eax,0FFFFFC00h
  0047f7f9  6a6c                  push      6Ch
  0047f7fb  56                    push      esi
  0047f7fc  897e70                mov       [esi+70h],edi
  0047f7ff  897e44                mov       [esi+44h],edi
  0047f802  897e48                mov       [esi+48h],edi
  0047f805  897e4c                mov       [esi+4Ch],edi
  0047f808  894e50                mov       [esi+50h],ecx
  0047f80b  894e54                mov       [esi+54h],ecx
  0047f80e  894e58                mov       [esi+58h],ecx
  0047f811  c7463880808000        mov       dword [esi+38h],808080h
  0047f818  897e28                mov       [esi+28h],edi
  0047f81b  66897e2c              mov       [esi+2Ch],di
  0047f81f  66894632              mov       [esi+32h],ax
  0047f823  66c746340004          mov       word [esi+34h],400h
  0047f829  c6460407              mov       byte [esi+4],7
  0047f82d  e8aeb2ffff            call      0047AAE0h
  0047f832  83c410                add       esp,10h
  0047f835  66896e32              mov       [esi+32h],bp
  0047f839  895e70                mov       [esi+70h],ebx
  0047f83c  e93c020000            jmp       0047FA7Dh

LAB_0047f841:
  0047f841  a801                  test      al,1
  0047f843  745c                  je        short 0047F8A1h
  0047f845  0fbf5e32              movsx     ebx,word [esi+32h]
  0047f849  e8f2780c00            call      00547140h
  0047f84e  83e07f                and       eax,7Fh
  0047f851  c7462800200020        mov       dword [esi+28h],20002000h
  0047f858  05c8000000            add       eax,0C8h
  0047f85d  897e38                mov       [esi+38h],edi
  0047f860  66894612              mov       [esi+12h],ax
  0047f864  66c7461a0700          mov       word [esi+1Ah],7
  0047f86a  e8d1780c00            call      00547140h
  0047f86f  66894632              mov       [esi+32h],ax
  0047f873  e8c8780c00            call      00547140h
  0047f878  6a04                  push      4
  0047f87a  57                    push      edi
  0047f87b  25ff010000            and       eax,1FFh
  0047f880  6a50                  push      50h
  0047f882  8d561c                lea       edx,[esi+1Ch]
  0047f885  68081a5500            push      551A08h
  0047f88a  2d00010000            sub       eax,100h
  0047f88f  52                    push      edx
  0047f890  56                    push      esi
  0047f891  66894634              mov       [esi+34h],ax
  0047f895  e8a6b3ffff            call      0047AC40h
  0047f89a  83c418                add       esp,18h
  0047f89d  66895e32              mov       [esi+32h],bx

LAB_0047f8a1:
  0047f8a1  0fbf6e32              movsx     ebp,word [esi+32h]
  0047f8a5  55                    push      ebp
  0047f8a6  8d5e1c                lea       ebx,[esi+1Ch]
  0047f8a9  e86e610c00            call      00545A1Ch
  0047f8ae  8b0b                  mov       ecx,[ebx]
  0047f8b0  8d0480                lea       eax,[eax+eax*4]
  0047f8b3  c1e002                shl       eax,2
  0047f8b6  03c8                  add       ecx,eax
  0047f8b8  55                    push      ebp
  0047f8b9  890b                  mov       [ebx],ecx
  0047f8bb  e856610c00            call      00545A16h
  0047f8c0  8b5624                mov       edx,[esi+24h]
  0047f8c3  8b6e20                mov       ebp,[esi+20h]
  0047f8c6  8d0c80                lea       ecx,[eax+eax*4]
  0047f8c9  56                    push      esi
  0047f8ca  c1e102                shl       ecx,2
  0047f8cd  03d1                  add       edx,ecx
  0047f8cf  895624                mov       [esi+24h],edx
  0047f8d2  e869400b00            call      00533940h
  0047f8d7  8b4660                mov       eax,[esi+60h]
  0047f8da  83c404                add       esp,4
  0047f8dd  3be8                  cmp       ebp,eax
  0047f8df  896e20                mov       [esi+20h],ebp
  0047f8e2  7f0d                  jg        short 0047F8F1h
  0047f8e4  894620                mov       [esi+20h],eax
  0047f8e7  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047f8f1:
  0047f8f1  f7869000000002880000  test      dword [esi+90h],8802h
  0047f8fb  7406                  je        short 0047F903h
  0047f8fd  810e00000002          or        dword [esi],2000000h

LAB_0047f903:
  0047f903  668b4e10              mov       cx,[esi+10h]
  0047f907  6683f93c              cmp       cx,3Ch
  0047f90b  7c06                  jl        short 0047F913h
  0047f90d  810e00000002          or        dword [esi],2000000h

LAB_0047f913:
  0047f913  8b06                  mov       eax,[esi]
  0047f915  a900008000            test      eax,800000h
  0047f91a  89442438              mov       [esp+38h],eax
  0047f91e  743c                  je        short 0047F95Ch
  0047f920  8b5674                mov       edx,[esi+74h]
  0047f923  33c0                  xor       eax,eax
  0047f925  8a4208                mov       al,[edx+8]
  0047f928  8d0cc0                lea       ecx,[eax+eax*8]
  0047f92b  8d1448                lea       edx,[eax+ecx*2]
  0047f92e  33c9                  xor       ecx,ecx
  0047f930  c1e00d                shl       eax,0Dh
  0047f933  8a0c9588915e00        mov       cl,[edx*4+5E9188h]
  0047f93a  8b15b4876000          mov       edx,[6087B4h]
  0047f940  c1e104                shl       ecx,4
  0047f943  8b12                  mov       edx,[edx]
  0047f945  668b89204a6000        mov       cx,[ecx+604A20h]
  0047f94c  66858c10c41f0f00      test      [eax+edx+0F1FC4h],cx
  0047f954  7513                  jne       short 0047F969h
  0047f956  8b442438              mov       eax,[esp+38h]
  0047f95a  eb06                  jmp       short 0047F962h

LAB_0047f95c:
  0047f95c  6683f928              cmp       cx,28h
  0047f960  7c07                  jl        short 0047F969h

LAB_0047f962:
  0047f962  0d00000002            or        eax,2000000h
  0047f967  8906                  mov       [esi],eax

LAB_0047f969:
  0047f969  56                    push      esi
  0047f96a  e8a157ffff            call      00475110h
  0047f96f  83c404                add       esp,4
  0047f972  85c0                  test      eax,eax
  0047f974  7406                  je        short 0047F97Ch
  0047f976  810e00000002          or        dword [esi],2000000h

LAB_0047f97c:
  0047f97c  f70600000002          test      dword [esi],2000000h
  0047f982  0f84f5000000          je        near 0047FA7Dh
  0047f988  6a03                  push      3
  0047f98a  57                    push      edi
  0047f98b  b800400000            mov       eax,4000h
  0047f990  6a6c                  push      6Ch
  0047f992  56                    push      esi
  0047f993  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  0047f999  66897e34              mov       [esi+34h],di
  0047f99d  897e44                mov       [esi+44h],edi
  0047f9a0  897e48                mov       [esi+48h],edi
  0047f9a3  897e4c                mov       [esi+4Ch],edi
  0047f9a6  894650                mov       [esi+50h],eax
  0047f9a9  c7465400200000        mov       dword [esi+54h],2000h
  0047f9b0  894658                mov       [esi+58h],eax
  0047f9b3  c7463880808000        mov       dword [esi+38h],808080h
  0047f9ba  897e28                mov       [esi+28h],edi
  0047f9bd  66897e2c              mov       [esi+2Ch],di
  0047f9c1  c6460407              mov       byte [esi+4],7
  0047f9c5  e816b1ffff            call      0047AAE0h
  0047f9ca  83c410                add       esp,10h
  0047f9cd  b800200000            mov       eax,2000h
  0047f9d2  894650                mov       [esi+50h],eax
  0047f9d5  894654                mov       [esi+54h],eax
  0047f9d8  6a0e                  push      0Eh
  0047f9da  6a01                  push      1
  0047f9dc  6a6c                  push      6Ch
  0047f9de  56                    push      esi
  0047f9df  894658                mov       [esi+58h],eax
  0047f9e2  c6460401              mov       byte [esi+4],1
  0047f9e6  e8f5b0ffff            call      0047AAE0h
  0047f9eb  8b5670                mov       edx,[esi+70h]
  0047f9ee  83c410                add       esp,10h
  0047f9f1  89542438              mov       [esp+38h],edx
  0047f9f5  897e70                mov       [esi+70h],edi
  0047f9f8  6a07                  push      7
  0047f9fa  57                    push      edi
  0047f9fb  68be000000            push      0BEh
  0047fa00  68a01b5500            push      551BA0h
  0047fa05  53                    push      ebx
  0047fa06  56                    push      esi
  0047fa07  897e50                mov       [esi+50h],edi
  0047fa0a  897e54                mov       [esi+54h],edi
  0047fa0d  897e58                mov       [esi+58h],edi
  0047fa10  66c746121f00          mov       word [esi+12h],1Fh
  0047fa16  897e44                mov       [esi+44h],edi
  0047fa19  897e48                mov       [esi+48h],edi
  0047fa1c  897e4c                mov       [esi+4Ch],edi
  0047fa1f  66c74614ff7f          mov       word [esi+14h],7FFFh
  0047fa25  66c746160200          mov       word [esi+16h],2
  0047fa2b  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047fa32  66c7461a0100          mov       word [esi+1Ah],1
  0047fa38  e803b2ffff            call      0047AC40h
  0047fa3d  8b442450              mov       eax,[esp+50h]
  0047fa41  83c418                add       esp,18h
  0047fa44  894670                mov       [esi+70h],eax
  0047fa47  c705a0935e00ff004000  mov       dword [5E93A0h],4000FFh
  0047fa51  53                    push      ebx
  0047fa52  6800120000            push      1200h
  0047fa57  66c705a4935e004000    mov       word [5E93A4h],40h
  0047fa60  6829030000            push      329h
  0047fa65  66897e10              mov       [esi+10h],di
  0047fa69  6a0b                  push      0Bh
  0047fa6b  c705ace05d0008000000  mov       dword [5DE0ACh],8
  0047fa75  e8a6e00b00            call      0053DB20h
  0047fa7a  83c410                add       esp,10h

LAB_0047fa7d:
  0047fa7d  8b06                  mov       eax,[esi]
  0047fa7f  a900000040            test      eax,40000000h
  0047fa84  740b                  je        short 0047FA91h
  0047fa86  0d00000002            or        eax,2000000h
  0047fa8b  66897e10              mov       [esi+10h],di
  0047fa8f  8906                  mov       [esi],eax

LAB_0047fa91:
  0047fa91  33c0                  xor       eax,eax

LAB_0047fa93:
  0047fa93  8d0c40                lea       ecx,[eax+eax*2]
  0047fa96  8d548c1c              lea       edx,[esp+ecx*4+1Ch]
  0047fa9a  52                    push      edx
  0047fa9b  56                    push      esi
  0047fa9c  e8ff51ffff            call      00474CA0h
  0047faa1  8bd8                  mov       ebx,eax
  0047faa3  83c408                add       esp,8
  0047faa6  3bdf                  cmp       ebx,edi
  0047faa8  744b                  je        short 0047FAF5h
  0047faaa  8b5674                mov       edx,[esi+74h]
  0047faad  33c0                  xor       eax,eax
  0047faaf  8a4606                mov       al,[esi+6]
  0047fab2  33c9                  xor       ecx,ecx
  0047fab4  8a4e07                mov       cl,[esi+7]
  0047fab7  57                    push      edi
  0047fab8  50                    push      eax
  0047fab9  51                    push      ecx
  0047faba  53                    push      ebx
  0047fabb  52                    push      edx
  0047fabc  e8efa70b00            call      0053A2B0h
  0047fac1  83c414                add       esp,14h
  0047fac4  55                    push      ebp
  0047fac5  e8525f0c00            call      00545A1Ch
  0047faca  8b4b50                mov       ecx,[ebx+50h]
  0047facd  55                    push      ebp
  0047face  c1e005                shl       eax,5
  0047fad1  03c8                  add       ecx,eax
  0047fad3  894b50                mov       [ebx+50h],ecx
  0047fad6  e83b5f0c00            call      00545A16h
  0047fadb  8b4b58                mov       ecx,[ebx+58h]
  0047fade  66c783960000001000    mov       word [ebx+96h],10h
  0047fae7  c1e005                shl       eax,5
  0047faea  03c8                  add       ecx,eax
  0047faec  8b03                  mov       eax,[ebx]
  0047faee  0c40                  or        al,40h
  0047faf0  894b58                mov       [ebx+58h],ecx
  0047faf3  8903                  mov       [ebx],eax

LAB_0047faf5:
  0047faf5  66ff4610              inc       word [esi+10h]
  0047faf9  5f                    pop       edi
  0047fafa  5e                    pop       esi
  0047fafb  5d                    pop       ebp
  0047fafc  5b                    pop       ebx
  0047fafd  83c424                add       esp,24h
  0047fb00  c3                    ret

19 cible(s) de saut interne : 0x0047f6aa, 0x0047f6b4, 0x0047f6c7, 0x0047f78e, 0x0047f792, 0x0047f7d7, 0x0047f841, 0x0047f8a1, 0x0047f8f1, 0x0047f903, 0x0047f913, 0x0047f95c, 0x0047f962, 0x0047f969, 0x0047f97c, 0x0047fa7d, 0x0047fa91, 0x0047fa93, 0x0047faf5
