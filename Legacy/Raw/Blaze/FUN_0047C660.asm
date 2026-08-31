
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c660  section .text  offset fichier 0x7ba60  450 octets

  0047c660  83ec0c                sub       esp,0Ch
  0047c663  53                    push      ebx
  0047c664  55                    push      ebp
  0047c665  56                    push      esi
  0047c666  8b74241c              mov       esi,[esp+1Ch]
  0047c66a  33ed                  xor       ebp,ebp
  0047c66c  57                    push      edi
  0047c66d  66396e10              cmp       [esi+10h],bp
  0047c671  8b7e74                mov       edi,[esi+74h]
  0047c674  66896c2410            mov       [esp+10h],bp
  0047c679  66c7442412baff        mov       word [esp+12h],0FFBAh
  0047c680  66c74424144600        mov       word [esp+14h],46h
  0047c687  66c74424166400        mov       word [esp+16h],64h
  0047c68e  66896c2418            mov       [esp+18h],bp
  0047c693  66c744241a8c00        mov       word [esp+1Ah],8Ch
  0047c69a  0f85a3000000          jne       near 0047C743h
  0047c6a0  8b17                  mov       edx,[edi]
  0047c6a2  6a04                  push      4
  0047c6a4  80ca80                or        dl,80h
  0047c6a7  55                    push      ebp
  0047c6a8  b800180000            mov       eax,1800h
  0047c6ad  6a6c                  push      6Ch
  0047c6af  8917                  mov       [edi],edx
  0047c6b1  804e3606              or        byte [esi+36h],6
  0047c6b5  56                    push      esi
  0047c6b6  897e70                mov       [esi+70h],edi
  0047c6b9  896e28                mov       [esi+28h],ebp
  0047c6bc  66896e2c              mov       [esi+2Ch],bp
  0047c6c0  c7463880808000        mov       dword [esi+38h],808080h
  0047c6c7  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047c6cd  896e44                mov       [esi+44h],ebp
  0047c6d0  896e48                mov       [esi+48h],ebp
  0047c6d3  896e4c                mov       [esi+4Ch],ebp
  0047c6d6  894650                mov       [esi+50h],eax
  0047c6d9  c74654001c0000        mov       dword [esi+54h],1C00h
  0047c6e0  894658                mov       [esi+58h],eax
  0047c6e3  c6460407              mov       byte [esi+4],7
  0047c6e7  e8f4e3ffff            call      0047AAE0h
  0047c6ec  83c410                add       esp,10h
  0047c6ef  896e44                mov       [esi+44h],ebp
  0047c6f2  57                    push      edi
  0047c6f3  e8c8c40b00            call      00538BC0h
  0047c6f8  24fe                  and       al,0FEh
  0047c6fa  83c404                add       esp,4
  0047c6fd  c1e00b                shl       eax,0Bh
  0047c700  894648                mov       [esi+48h],eax
  0047c703  b800100000            mov       eax,1000h
  0047c708  894650                mov       [esi+50h],eax
  0047c70b  894654                mov       [esi+54h],eax
  0047c70e  894658                mov       [esi+58h],eax
  0047c711  33c0                  xor       eax,eax
  0047c713  8a460b                mov       al,[esi+0Bh]
  0047c716  6aff                  push      0FFFFFFFFh
  0047c718  50                    push      eax
  0047c719  6a6c                  push      6Ch
  0047c71b  56                    push      esi
  0047c71c  896e4c                mov       [esi+4Ch],ebp
  0047c71f  c6460402              mov       byte [esi+4],2
  0047c723  e8b8e3ffff            call      0047AAE0h
  0047c728  83c410                add       esp,10h
  0047c72b  8d4e1c                lea       ecx,[esi+1Ch]
  0047c72e  51                    push      ecx
  0047c72f  6800120000            push      1200h
  0047c734  684a190000            push      194Ah
  0047c739  6a23                  push      23h
  0047c73b  e8e0130c00            call      0053DB20h
  0047c740  83c410                add       esp,10h

LAB_0047c743:
  0047c743  8b571c                mov       edx,[edi+1Ch]
  0047c746  89561c                mov       [esi+1Ch],edx
  0047c749  8b4720                mov       eax,[edi+20h]
  0047c74c  8d542410              lea       edx,[esp+10h]
  0047c750  894620                mov       [esi+20h],eax
  0047c753  8b4f24                mov       ecx,[edi+24h]
  0047c756  66814632cc00          add       word [esi+32h],0CCh
  0047c75c  52                    push      edx
  0047c75d  56                    push      esi
  0047c75e  894e24                mov       [esi+24h],ecx
  0047c761  e83a85ffff            call      00474CA0h
  0047c766  8bf8                  mov       edi,eax
  0047c768  83c408                add       esp,8
  0047c76b  3bfd                  cmp       edi,ebp
  0047c76d  0f8488000000          je        near 0047C7FBh
  0047c773  8b5674                mov       edx,[esi+74h]
  0047c776  33c0                  xor       eax,eax
  0047c778  8a4606                mov       al,[esi+6]
  0047c77b  33c9                  xor       ecx,ecx
  0047c77d  8a4e07                mov       cl,[esi+7]
  0047c780  55                    push      ebp
  0047c781  50                    push      eax
  0047c782  51                    push      ecx
  0047c783  57                    push      edi
  0047c784  52                    push      edx
  0047c785  e826db0b00            call      0053A2B0h
  0047c78a  8b4724                mov       eax,[edi+24h]
  0047c78d  8b5e24                mov       ebx,[esi+24h]
  0047c790  8b4f1c                mov       ecx,[edi+1Ch]
  0047c793  2bc3                  sub       eax,ebx
  0047c795  83c414                add       esp,14h
  0047c798  c1f80c                sar       eax,0Ch
  0047c79b  50                    push      eax
  0047c79c  8b461c                mov       eax,[esi+1Ch]
  0047c79f  2bc8                  sub       ecx,eax
  0047c7a1  c1f90c                sar       ecx,0Ch
  0047c7a4  51                    push      ecx
  0047c7a5  e878920c00            call      00545A22h
  0047c7aa  8bd8                  mov       ebx,eax
  0047c7ac  81c300080000          add       ebx,800h
  0047c7b2  53                    push      ebx
  0047c7b3  e864920c00            call      00545A1Ch
  0047c7b8  8b4f50                mov       ecx,[edi+50h]
  0047c7bb  53                    push      ebx
  0047c7bc  c1e004                shl       eax,4
  0047c7bf  03c8                  add       ecx,eax
  0047c7c1  894f50                mov       [edi+50h],ecx
  0047c7c4  e84d920c00            call      00545A16h
  0047c7c9  8b4f58                mov       ecx,[edi+58h]
  0047c7cc  66c787960000001000    mov       word [edi+96h],10h
  0047c7d5  c1e004                shl       eax,4
  0047c7d8  03c8                  add       ecx,eax
  0047c7da  8b07                  mov       eax,[edi]
  0047c7dc  0c40                  or        al,40h
  0047c7de  894f58                mov       [edi+58h],ecx
  0047c7e1  8907                  mov       [edi],eax
  0047c7e3  8d461c                lea       eax,[esi+1Ch]
  0047c7e6  50                    push      eax
  0047c7e7  6800120000            push      1200h
  0047c7ec  6800080000            push      800h
  0047c7f1  6a1e                  push      1Eh
  0047c7f3  e828130c00            call      0053DB20h
  0047c7f8  83c410                add       esp,10h

LAB_0047c7fb:
  0047c7fb  66837e100d            cmp       word [esi+10h],0Dh
  0047c800  7d08                  jge       short 0047C80Ah
  0047c802  f70600000040          test      dword [esi],40000000h
  0047c808  740c                  je        short 0047C816h

LAB_0047c80a:
  0047c80a  8b4674                mov       eax,[esi+74h]
  0047c80d  8b08                  mov       ecx,[eax]
  0047c80f  80e17f                and       cl,7Fh
  0047c812  8908                  mov       [eax],ecx
  0047c814  892e                  mov       [esi],ebp

LAB_0047c816:
  0047c816  66ff4610              inc       word [esi+10h]
  0047c81a  5f                    pop       edi
  0047c81b  5e                    pop       esi
  0047c81c  5d                    pop       ebp
  0047c81d  5b                    pop       ebx
  0047c81e  83c40c                add       esp,0Ch
  0047c821  c3                    ret

4 cible(s) de saut interne : 0x0047c743, 0x0047c7fb, 0x0047c80a, 0x0047c816
