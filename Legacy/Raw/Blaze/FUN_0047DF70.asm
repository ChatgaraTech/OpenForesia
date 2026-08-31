
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047df70  section .text  offset fichier 0x7d370  477 octets

  0047df70  53                    push      ebx
  0047df71  55                    push      ebp
  0047df72  56                    push      esi
  0047df73  8b742410              mov       esi,[esp+10h]
  0047df77  57                    push      edi
  0047df78  66837e1000            cmp       word [esi+10h],0
  0047df7d  0f85f2000000          jne       near 0047E075h
  0047df83  804e3606              or        byte [esi+36h],6
  0047df87  6a00                  push      0
  0047df89  56                    push      esi
  0047df8a  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047df90  e80b6cffff            call      00474BA0h
  0047df95  83c408                add       esp,8
  0047df98  894678                mov       [esi+78h],eax
  0047df9b  894670                mov       [esi+70h],eax
  0047df9e  66c746129001          mov       word [esi+12h],190h
  0047dfa4  50                    push      eax
  0047dfa5  c7463800000000        mov       dword [esi+38h],0
  0047dfac  e80fac0b00            call      00538BC0h
  0047dfb1  8bc8                  mov       ecx,eax
  0047dfb3  b889888888            mov       eax,88888889h
  0047dfb8  8bf9                  mov       edi,ecx
  0047dfba  8bd9                  mov       ebx,ecx
  0047dfbc  c1e70c                shl       edi,0Ch
  0047dfbf  f7ef                  imul      edi
  0047dfc1  03d7                  add       edx,edi
  0047dfc3  83c404                add       esp,4
  0047dfc6  c1fa05                sar       edx,5
  0047dfc9  8bc2                  mov       eax,edx
  0047dfcb  c646043f              mov       byte [esi+4],3Fh
  0047dfcf  c1e81f                shr       eax,1Fh
  0047dfd2  03d0                  add       edx,eax
  0047dfd4  b867666666            mov       eax,66666667h
  0047dfd9  895628                mov       [esi+28h],edx
  0047dfdc  f7ea                  imul      edx
  0047dfde  c1fa02                sar       edx,2
  0047dfe1  0fbf7e32              movsx     edi,word [esi+32h]
  0047dfe5  8bc2                  mov       eax,edx
  0047dfe7  c744241402000000      mov       dword [esp+14h],2
  0047dfef  c1e81f                shr       eax,1Fh
  0047dff2  03d0                  add       edx,eax
  0047dff4  d1fb                  sar       ebx,1
  0047dff6  c1f904                sar       ecx,4
  0047dff9  6689562c              mov       [esi+2Ch],dx
  0047dffd  2bd9                  sub       ebx,ecx

LAB_0047dfff:
  0047dfff  bd03000000            mov       ebp,3

LAB_0047e004:
  0047e004  81c755050000          add       edi,555h
  0047e00a  66c746340008          mov       word [esi+34h],800h
  0047e010  57                    push      edi
  0047e011  66897e14              mov       [esi+14h],di
  0047e015  8d8f00080000          lea       ecx,[edi+800h]
  0047e01b  66894e32              mov       [esi+32h],cx
  0047e01f  e8f8790c00            call      00545A1Ch
  0047e024  0fafc3                imul      eax,ebx
  0047e027  57                    push      edi
  0047e028  894644                mov       [esi+44h],eax
  0047e02b  e8e6790c00            call      00545A16h
  0047e030  0fafc3                imul      eax,ebx
  0047e033  33d2                  xor       edx,edx
  0047e035  6aff                  push      0FFFFFFFFh
  0047e037  8a560b                mov       dl,[esi+0Bh]
  0047e03a  89464c                mov       [esi+4Ch],eax
  0047e03d  52                    push      edx
  0047e03e  6a76                  push      76h
  0047e040  56                    push      esi
  0047e041  e89acaffff            call      0047AAE0h
  0047e046  83c410                add       esp,10h
  0047e049  4d                    dec       ebp
  0047e04a  75b8                  jne       short 0047E004h
  0047e04c  8b442414              mov       eax,[esp+14h]
  0047e050  81c7aa020000          add       edi,2AAh
  0047e056  48                    dec       eax
  0047e057  89442414              mov       [esp+14h],eax
  0047e05b  75a2                  jne       short 0047DFFFh
  0047e05d  8d461c                lea       eax,[esi+1Ch]
  0047e060  50                    push      eax
  0047e061  6800120000            push      1200h
  0047e066  687e030000            push      37Eh
  0047e06b  6a2c                  push      2Ch
  0047e06d  e8aefa0b00            call      0053DB20h
  0047e072  83c410                add       esp,10h

LAB_0047e075:
  0047e075  8b06                  mov       eax,[esi]
  0047e077  a900000001            test      eax,1000000h
  0047e07c  0f8598000000          jne       near 0047E11Ah
  0047e082  a900000002            test      eax,2000000h
  0047e087  0f848d000000          je        near 0047E11Ah
  0047e08d  8b4e78                mov       ecx,[esi+78h]
  0047e090  0d00000001            or        eax,1000000h
  0047e095  51                    push      ecx
  0047e096  8906                  mov       [esi],eax
  0047e098  c646043e              mov       byte [esi+4],3Eh
  0047e09c  e81fab0b00            call      00538BC0h
  0047e0a1  8bc8                  mov       ecx,eax
  0047e0a3  b8b5814e1b            mov       eax,1B4E81B5h
  0047e0a8  c1e10c                shl       ecx,0Ch
  0047e0ab  f7e9                  imul      ecx
  0047e0ad  c1fa03                sar       edx,3
  0047e0b0  8bc2                  mov       eax,edx
  0047e0b2  83c404                add       esp,4
  0047e0b5  c1e81f                shr       eax,1Fh
  0047e0b8  03d0                  add       edx,eax
  0047e0ba  6aff                  push      0FFFFFFFFh
  0047e0bc  8bca                  mov       ecx,edx
  0047e0be  6689562c              mov       [esi+2Ch],dx
  0047e0c2  33d2                  xor       edx,edx
  0047e0c4  81c900000060          or        ecx,60000000h
  0047e0ca  8a560b                mov       dl,[esi+0Bh]
  0047e0cd  894e28                mov       [esi+28h],ecx
  0047e0d0  52                    push      edx
  0047e0d1  6a77                  push      77h
  0047e0d3  56                    push      esi
  0047e0d4  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047e0db  66c746340000          mov       word [esi+34h],0
  0047e0e1  e8fac9ffff            call      0047AAE0h
  0047e0e6  668146320008          add       word [esi+32h],800h
  0047e0ec  83c410                add       esp,10h
  0047e0ef  33c0                  xor       eax,eax
  0047e0f1  8a460b                mov       al,[esi+0Bh]
  0047e0f4  6aff                  push      0FFFFFFFFh
  0047e0f6  50                    push      eax
  0047e0f7  6a77                  push      77h
  0047e0f9  56                    push      esi
  0047e0fa  e8e1c9ffff            call      0047AAE0h
  0047e0ff  83c410                add       esp,10h
  0047e102  8d4e1c                lea       ecx,[esi+1Ch]
  0047e105  51                    push      ecx
  0047e106  6800120000            push      1200h
  0047e10b  6800080000            push      800h
  0047e110  6a0d                  push      0Dh
  0047e112  e809fa0b00            call      0053DB20h
  0047e117  83c410                add       esp,10h

LAB_0047e11a:
  0047e11a  66837e103c            cmp       word [esi+10h],3Ch
  0047e11f  7c23                  jl        short 0047E144h
  0047e121  8b5674                mov       edx,[esi+74h]
  0047e124  668b4606              mov       ax,[esi+6]
  0047e128  6a00                  push      0
  0047e12a  66894206              mov       [edx+6],ax
  0047e12e  8b4e78                mov       ecx,[esi+78h]
  0047e131  8b5674                mov       edx,[esi+74h]
  0047e134  51                    push      ecx
  0047e135  52                    push      edx
  0047e136  e835dd0b00            call      0053BE70h
  0047e13b  83c40c                add       esp,0Ch
  0047e13e  c70600000000          mov       dword [esi],0

LAB_0047e144:
  0047e144  66ff4610              inc       word [esi+10h]
  0047e148  5f                    pop       edi
  0047e149  5e                    pop       esi
  0047e14a  5d                    pop       ebp
  0047e14b  5b                    pop       ebx
  0047e14c  c3                    ret

5 cible(s) de saut interne : 0x0047dfff, 0x0047e004, 0x0047e075, 0x0047e11a, 0x0047e144
