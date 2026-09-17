Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b6b90  section .text  offset fichier 0xb5f90  2480 octets

  004b6b90  83ec28                sub       esp,28h
  004b6b93  8d442408              lea       eax,[esp+8]
  004b6b97  8d4c2420              lea       ecx,[esp+20h]
  004b6b9b  8d54241c              lea       edx,[esp+1Ch]
  004b6b9f  53                    push      ebx
  004b6ba0  55                    push      ebp
  004b6ba1  56                    push      esi
  004b6ba2  8b742438              mov       esi,[esp+38h]
  004b6ba6  57                    push      edi
  004b6ba7  50                    push      eax
  004b6ba8  51                    push      ecx
  004b6ba9  52                    push      edx
  004b6baa  56                    push      esi
  004b6bab  e8d01d0800            call      00538980h
  004b6bb0  83c410                add       esp,10h
  004b6bb3  8d44241c              lea       eax,[esp+1Ch]
  004b6bb7  8d4c2414              lea       ecx,[esp+14h]
  004b6bbb  8d54243c              lea       edx,[esp+3Ch]
  004b6bbf  50                    push      eax
  004b6bc0  51                    push      ecx
  004b6bc1  8d442418              lea       eax,[esp+18h]
  004b6bc5  52                    push      edx
  004b6bc6  8d4c2440              lea       ecx,[esp+40h]
  004b6bca  50                    push      eax
  004b6bcb  51                    push      ecx
  004b6bcc  56                    push      esi
  004b6bcd  e84ef30700            call      00535F20h
  004b6bd2  8b44242c              mov       eax,[esp+2Ch]
  004b6bd6  83c418                add       esp,18h
  004b6bd9  85c0                  test      eax,eax
  004b6bdb  0f84fb060000          je        near 004B72DCh
  004b6be1  56                    push      esi
  004b6be2  e8b9490200            call      004DB5A0h
  004b6be7  8b06                  mov       eax,[esi]
  004b6be9  83c404                add       esp,4
  004b6bec  a802                  test      al,2
  004b6bee  0f84d0040000          je        near 004B70C4h
  004b6bf4  8a4c241c              mov       cl,[esp+1Ch]
  004b6bf8  bb10000000            mov       ebx,10h
  004b6bfd  84cb                  test      bl,cl
  004b6bff  740d                  je        short 004B6C0Eh
  004b6c01  24fd                  and       al,0FDh
  004b6c03  c6464300              mov       byte [esi+43h],0
  004b6c07  8906                  mov       [esi],eax
  004b6c09  e9ce060000            jmp       004B72DCh

LAB_004b6c0e:
  004b6c0e  33c9                  xor       ecx,ecx
  004b6c10  8a4e42                mov       cl,[esi+42h]
  004b6c13  83e900                sub       ecx,0
  004b6c16  0f8465020000          je        near 004B6E81h
  004b6c1c  49                    dec       ecx
  004b6c1d  0f84f5000000          je        near 004B6D18h
  004b6c23  49                    dec       ecx
  004b6c24  0f85b2060000          jne       near 004B72DCh
  004b6c2a  668b5610              mov       dx,[esi+10h]
  004b6c2e  0fbfca                movsx     ecx,dx
  004b6c31  83e900                sub       ecx,0
  004b6c34  0f84c4000000          je        near 004B6CFEh
  004b6c3a  49                    dec       ecx
  004b6c3b  0f85cb000000          jne       near 004B6D0Ch
  004b6c41  a900000002            test      eax,2000000h
  004b6c46  7409                  je        short 004B6C51h
  004b6c48  4a                    dec       edx
  004b6c49  c646430f              mov       byte [esi+43h],0Fh
  004b6c4d  66895610              mov       [esi+10h],dx

LAB_004b6c51:
  004b6c51  66837e1a12            cmp       word [esi+1Ah],12h
  004b6c56  0f85b0000000          jne       near 004B6D0Ch
  004b6c5c  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004b6c63  0fbf6e34              movsx     ebp,word [esi+34h]
  004b6c67  8b5e5c                mov       ebx,[esi+5Ch]
  004b6c6a  6a00                  push      0
  004b6c6c  56                    push      esi
  004b6c6d  66c7869a0000000000    mov       word [esi+9Ah],0
  004b6c76  66c74634d40e          mov       word [esi+34h],0ED4h
  004b6c7c  c7465c3c000000        mov       dword [esi+5Ch],3Ch
  004b6c83  e8e8ea0700            call      00535770h
  004b6c88  0fbf10                movsx     edx,word [eax]
  004b6c8b  c1e20c                shl       edx,0Ch
  004b6c8e  895644                mov       [esi+44h],edx
  004b6c91  83c408                add       esp,8
  004b6c94  0fbf4802              movsx     ecx,word [eax+2]
  004b6c98  c1e10c                shl       ecx,0Ch
  004b6c9b  894e48                mov       [esi+48h],ecx
  004b6c9e  6a02                  push      2
  004b6ca0  0fbf5004              movsx     edx,word [eax+4]
  004b6ca4  0fbf4616              movsx     eax,word [esi+16h]
  004b6ca8  c1e20c                shl       edx,0Ch
  004b6cab  89564c                mov       [esi+4Ch],edx
  004b6cae  8b0dcc975e00          mov       ecx,[5E97CCh]
  004b6cb4  6a00                  push      0
  004b6cb6  8b91cc000000          mov       edx,[ecx+0CCh]
  004b6cbc  8b0a                  mov       ecx,[edx]
  004b6cbe  33d2                  xor       edx,edx
  004b6cc0  8a1408                mov       dl,[eax+ecx]
  004b6cc3  81c2c0000000          add       edx,0C0h
  004b6cc9  52                    push      edx
  004b6cca  56                    push      esi
  004b6ccb  e8003dfcff            call      0047A9D0h
  004b6cd0  83c410                add       esp,10h
  004b6cd3  8d461c                lea       eax,[esi+1Ch]
  004b6cd6  6689be9a000000        mov       [esi+9Ah],di
  004b6cdd  66896e34              mov       [esi+34h],bp
  004b6ce1  50                    push      eax
  004b6ce2  68e82a5500            push      552AE8h
  004b6ce7  895e5c                mov       [esi+5Ch],ebx
  004b6cea  e8f16e0800            call      0053DBE0h
  004b6cef  83c408                add       esp,8
  004b6cf2  c7465c00000000        mov       dword [esi+5Ch],0
  004b6cf9  e9de050000            jmp       004B72DCh

LAB_004b6cfe:
  004b6cfe  8a4e43                mov       cl,[esi+43h]
  004b6d01  fec9                  dec       cl
  004b6d03  884e43                mov       [esi+43h],cl
  004b6d06  7504                  jne       short 004B6D0Ch
  004b6d08  24fd                  and       al,0FDh
  004b6d0a  8906                  mov       [esi],eax

LAB_004b6d0c:
  004b6d0c  c7465c00000000        mov       dword [esi+5Ch],0
  004b6d13  e9c4050000            jmp       004B72DCh

LAB_004b6d18:
  004b6d18  66837e1a01            cmp       word [esi+1Ah],1
  004b6d1d  7518                  jne       short 004B6D37h
  004b6d1f  8d4e1c                lea       ecx,[esi+1Ch]
  004b6d22  51                    push      ecx
  004b6d23  6800180000            push      1800h
  004b6d28  682a030000            push      32Ah
  004b6d2d  6a02                  push      2
  004b6d2f  e8ec6d0800            call      0053DB20h
  004b6d34  83c410                add       esp,10h

LAB_004b6d37:
  004b6d37  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004b6d3c  7518                  jne       short 004B6D56h
  004b6d3e  8d561c                lea       edx,[esi+1Ch]
  004b6d41  52                    push      edx
  004b6d42  6800180000            push      1800h
  004b6d47  6800040000            push      400h
  004b6d4c  6a24                  push      24h
  004b6d4e  e8cd6d0800            call      0053DB20h
  004b6d53  83c410                add       esp,10h

LAB_004b6d56:
  004b6d56  668b461a              mov       ax,[esi+1Ah]
  004b6d5a  663d0900              cmp       ax,9
  004b6d5e  7f57                  jg        short 004B6DB7h
  004b6d60  6683461004            add       word [esi+10h],4
  004b6d65  8b5e20                mov       ebx,[esi+20h]
  004b6d68  0fbf4610              movsx     eax,word [esi+10h]
  004b6d6c  8b0e                  mov       ecx,[esi]
  004b6d6e  c1e00c                shl       eax,0Ch
  004b6d71  03d8                  add       ebx,eax
  004b6d73  8b44243c              mov       eax,[esp+3Ch]
  004b6d77  80cd04                or        ch,4
  004b6d7a  895e20                mov       [esi+20h],ebx
  004b6d7d  890e                  mov       [esi],ecx
  004b6d7f  8d4828                lea       ecx,[eax+28h]
  004b6d82  81e1ff0f0000          and       ecx,0FFFh
  004b6d88  83f950                cmp       ecx,50h
  004b6d8b  7f09                  jg        short 004B6D96h
  004b6d8d  66014632              add       [esi+32h],ax
  004b6d91  e946050000            jmp       004B72DCh

LAB_004b6d96:
  004b6d96  25ff0f0000            and       eax,0FFFh
  004b6d9b  33d2                  xor       edx,edx
  004b6d9d  3d00080000            cmp       eax,800h
  004b6da2  0f9dc2                setge     dl
  004b6da5  4a                    dec       edx
  004b6da6  83e250                and       edx,50h
  004b6da9  83c2d8                add       edx,0FFFFFFD8h
  004b6dac  8bc2                  mov       eax,edx
  004b6dae  66014632              add       [esi+32h],ax
  004b6db2  e925050000            jmp       004B72DCh

LAB_004b6db7:
  004b6db7  663d0c00              cmp       ax,0Ch
  004b6dbb  0f8c92000000          jl        near 004B6E53h
  004b6dc1  8d6e1c                lea       ebp,[esi+1Ch]
  004b6dc4  6a00                  push      0
  004b6dc6  68d82a5500            push      552AD8h
  004b6dcb  55                    push      ebp
  004b6dcc  56                    push      esi
  004b6dcd  e85e2e0600            call      00519C30h
  004b6dd2  8bf8                  mov       edi,eax
  004b6dd4  a1b8e05d00            mov       eax,[5DE0B8h]
  004b6dd9  83c410                add       esp,10h
  004b6ddc  85c0                  test      eax,eax
  004b6dde  7473                  je        short 004B6E53h
  004b6de0  8b07                  mov       eax,[edi]
  004b6de2  57                    push      edi
  004b6de3  0c40                  or        al,40h
  004b6de5  56                    push      esi
  004b6de6  66899f96000000        mov       [edi+96h],bx
  004b6ded  8907                  mov       [edi],eax
  004b6def  e88c2d0800            call      00539B80h
  004b6df4  83c408                add       esp,8
  004b6df7  e844030900            call      00547140h
  004b6dfc  99                    cdq
  004b6dfd  b964000000            mov       ecx,64h
  004b6e02  f7f9                  idiv      ecx
  004b6e04  83fa14                cmp       edx,14h
  004b6e07  7d0f                  jge       short 004B6E18h
  004b6e09  6a00                  push      0
  004b6e0b  57                    push      edi
  004b6e0c  56                    push      esi
  004b6e0d  c6460707              mov       byte [esi+7],7
  004b6e11  e80a560800            call      0053C420h
  004b6e16  eb12                  jmp       short 004B6E2Ah

LAB_004b6e18:
  004b6e18  83fa28                cmp       edx,28h
  004b6e1b  7d10                  jge       short 004B6E2Dh
  004b6e1d  6a00                  push      0
  004b6e1f  57                    push      edi
  004b6e20  56                    push      esi
  004b6e21  c6460707              mov       byte [esi+7],7
  004b6e25  e896450800            call      0053B3C0h

LAB_004b6e2a:
  004b6e2a  83c40c                add       esp,0Ch

LAB_004b6e2d:
  004b6e2d  83c71c                add       edi,1Ch
  004b6e30  57                    push      edi
  004b6e31  6800180000            push      1800h
  004b6e36  6800030000            push      300h
  004b6e3b  6a03                  push      3
  004b6e3d  e8de6c0800            call      0053DB20h
  004b6e42  83c410                add       esp,10h
  004b6e45  55                    push      ebp
  004b6e46  68082b5500            push      552B08h
  004b6e4b  e8906d0800            call      0053DBE0h
  004b6e50  83c408                add       esp,8

LAB_004b6e53:
  004b6e53  8b06                  mov       eax,[esi]
  004b6e55  a900000002            test      eax,2000000h
  004b6e5a  7407                  je        short 004B6E63h
  004b6e5c  25fdfbffff            and       eax,0FFFFFBFDh
  004b6e61  8906                  mov       [esi],eax

LAB_004b6e63:
  004b6e63  8b4648                mov       eax,[esi+48h]
  004b6e66  8b4e60                mov       ecx,[esi+60h]
  004b6e69  3bc1                  cmp       eax,ecx
  004b6e6b  7d02                  jge       short 004B6E6Fh
  004b6e6d  8bc1                  mov       eax,ecx

LAB_004b6e6f:
  004b6e6f  8b4e20                mov       ecx,[esi+20h]
  004b6e72  2bc1                  sub       eax,ecx
  004b6e74  c1f802                sar       eax,2
  004b6e77  03c1                  add       eax,ecx
  004b6e79  894620                mov       [esi+20h],eax
  004b6e7c  e95b040000            jmp       004B72DCh

LAB_004b6e81:
  004b6e81  0fbf4610              movsx     eax,word [esi+10h]
  004b6e85  83e800                sub       eax,0
  004b6e88  0f84b6000000          je        near 004B6F44h
  004b6e8e  48                    dec       eax
  004b6e8f  0f8547040000          jne       near 004B72DCh
  004b6e95  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004b6e9a  c7465c00000000        mov       dword [esi+5Ch],0
  004b6ea1  755c                  jne       short 004B6EFFh
  004b6ea3  0fbf5632              movsx     edx,word [esi+32h]
  004b6ea7  52                    push      edx
  004b6ea8  c7465c00800200        mov       dword [esi+5Ch],28000h
  004b6eaf  e868eb0800            call      00545A1Ch
  004b6eb4  0faf465c              imul      eax,[esi+5Ch]
  004b6eb8  8b5e50                mov       ebx,[esi+50h]
  004b6ebb  c1f80c                sar       eax,0Ch
  004b6ebe  03d8                  add       ebx,eax
  004b6ec0  0fbf4632              movsx     eax,word [esi+32h]
  004b6ec4  50                    push      eax
  004b6ec5  895e50                mov       [esi+50h],ebx
  004b6ec8  e849eb0800            call      00545A16h
  004b6ecd  0faf465c              imul      eax,[esi+5Ch]
  004b6ed1  8b4e58                mov       ecx,[esi+58h]
  004b6ed4  c7465c00500000        mov       dword [esi+5Ch],5000h
  004b6edb  c1f80c                sar       eax,0Ch
  004b6ede  03c8                  add       ecx,eax
  004b6ee0  66ff4e10              dec       word [esi+10h]
  004b6ee4  894e58                mov       [esi+58h],ecx
  004b6ee7  8d4e1c                lea       ecx,[esi+1Ch]
  004b6eea  51                    push      ecx
  004b6eeb  6800180000            push      1800h
  004b6ef0  6800100000            push      1000h
  004b6ef5  6a25                  push      25h
  004b6ef7  e8246c0800            call      0053DB20h
  004b6efc  83c410                add       esp,10h

LAB_004b6eff:
  004b6eff  8b44243c              mov       eax,[esp+3Ch]
  004b6f03  8d9080000000          lea       edx,[eax+80h]
  004b6f09  81e2ff0f0000          and       edx,0FFFh
  004b6f0f  81fa00010000          cmp       edx,100h
  004b6f15  7f09                  jg        short 004B6F20h
  004b6f17  66014632              add       [esi+32h],ax
  004b6f1b  e9bc030000            jmp       004B72DCh

LAB_004b6f20:
  004b6f20  25ff0f0000            and       eax,0FFFh
  004b6f25  33c9                  xor       ecx,ecx
  004b6f27  3d00080000            cmp       eax,800h
  004b6f2c  0f9dc1                setge     cl
  004b6f2f  49                    dec       ecx
  004b6f30  81e100010000          and       ecx,100h
  004b6f36  83c180                add       ecx,0FFFFFF80h
  004b6f39  8bc1                  mov       eax,ecx
  004b6f3b  66014632              add       [esi+32h],ax
  004b6f3f  e998030000            jmp       004B72DCh

LAB_004b6f44:
  004b6f44  8b4650                mov       eax,[esi+50h]
  004b6f47  8b4e58                mov       ecx,[esi+58h]
  004b6f4a  c1f80c                sar       eax,0Ch
  004b6f4d  8bd0                  mov       edx,eax
  004b6f4f  c1f90c                sar       ecx,0Ch
  004b6f52  0fafd0                imul      edx,eax
  004b6f55  8bc1                  mov       eax,ecx
  004b6f57  0fafc1                imul      eax,ecx
  004b6f5a  03d0                  add       edx,eax
  004b6f5c  52                    push      edx
  004b6f5d  e8aeea0800            call      00545A10h
  004b6f62  8b0e                  mov       ecx,[esi]
  004b6f64  f7c100000002          test      ecx,2000000h
  004b6f6a  740a                  je        short 004B6F76h
  004b6f6c  83f808                cmp       eax,8
  004b6f6f  7f05                  jg        short 004B6F76h
  004b6f71  83e1dd                and       ecx,0FFFFFFDDh
  004b6f74  890e                  mov       [esi],ecx

LAB_004b6f76:
  004b6f76  83f803                cmp       eax,3
  004b6f79  0f8c5d030000          jl        near 004B72DCh
  004b6f7f  f60620                test      byte [esi],20h
  004b6f82  0f8554030000          jne       near 004B72DCh
  004b6f88  8b44243c              mov       eax,[esp+3Ch]
  004b6f8c  8d4828                lea       ecx,[eax+28h]
  004b6f8f  81e1ff0f0000          and       ecx,0FFFh
  004b6f95  83f950                cmp       ecx,50h
  004b6f98  7e18                  jle       short 004B6FB2h
  004b6f9a  25ff0f0000            and       eax,0FFFh
  004b6f9f  33d2                  xor       edx,edx
  004b6fa1  3d00080000            cmp       eax,800h
  004b6fa6  0f9dc2                setge     dl
  004b6fa9  4a                    dec       edx
  004b6faa  83e250                and       edx,50h
  004b6fad  83c2d8                add       edx,0FFFFFFD8h
  004b6fb0  8bc2                  mov       eax,edx

LAB_004b6fb2:
  004b6fb2  66014632              add       [esi+32h],ax
  004b6fb6  8d6e1c                lea       ebp,[esi+1Ch]
  004b6fb9  6a00                  push      0
  004b6fbb  68d82a5500            push      552AD8h
  004b6fc0  55                    push      ebp
  004b6fc1  56                    push      esi
  004b6fc2  e8692c0600            call      00519C30h
  004b6fc7  8bf8                  mov       edi,eax
  004b6fc9  83c410                add       esp,10h
  004b6fcc  85ff                  test      edi,edi
  004b6fce  7406                  je        short 004B6FD6h
  004b6fd0  8b06                  mov       eax,[esi]
  004b6fd2  0c20                  or        al,20h
  004b6fd4  8906                  mov       [esi],eax

LAB_004b6fd6:
  004b6fd6  a1b8e05d00            mov       eax,[5DE0B8h]
  004b6fdb  85c0                  test      eax,eax
  004b6fdd  0f84f9020000          je        near 004B72DCh
  004b6fe3  66899f96000000        mov       [edi+96h],bx
  004b6fea  8b1f                  mov       ebx,[edi]
  004b6fec  83cb40                or        ebx,40h
  004b6fef  57                    push      edi
  004b6ff0  891f                  mov       [edi],ebx
  004b6ff2  8b4c241c              mov       ecx,[esp+1Ch]
  004b6ff6  56                    push      esi
  004b6ff7  668b4130              mov       ax,[ecx+30h]
  004b6ffb  0fbfd8                movsx     ebx,ax
  004b6ffe  66d1f8                sar       ax,1
  004b7001  66894130              mov       [ecx+30h],ax
  004b7005  e8762b0800            call      00539B80h
  004b700a  8b442420              mov       eax,[esp+20h]
  004b700e  83c408                add       esp,8
  004b7011  66895830              mov       [eax+30h],bx
  004b7015  8d5f1c                lea       ebx,[edi+1Ch]
  004b7018  53                    push      ebx
  004b7019  6800180000            push      1800h
  004b701e  6800030000            push      300h
  004b7023  6a03                  push      3
  004b7025  e8f66a0800            call      0053DB20h
  004b702a  8b07                  mov       eax,[edi]
  004b702c  83c410                add       esp,10h
  004b702f  a900000040            test      eax,40000000h
  004b7034  0f85a2020000          jne       near 004B72DCh
  004b703a  0fbf4e32              movsx     ecx,word [esi+32h]
  004b703e  51                    push      ecx
  004b703f  e8d8e90800            call      00545A1Ch
  004b7044  8d0440                lea       eax,[eax+eax*2]
  004b7047  8d1480                lea       edx,[eax+eax*4]
  004b704a  d1e2                  shl       edx,1
  004b704c  895750                mov       [edi+50h],edx
  004b704f  0fbf4632              movsx     eax,word [esi+32h]
  004b7053  50                    push      eax
  004b7054  e8bde90800            call      00545A16h
  004b7059  8d0440                lea       eax,[eax+eax*2]
  004b705c  8d0c80                lea       ecx,[eax+eax*4]
  004b705f  8b07                  mov       eax,[edi]
  004b7061  d1e1                  shl       ecx,1
  004b7063  a801                  test      al,1
  004b7065  894f58                mov       [edi+58h],ecx
  004b7068  750b                  jne       short 004B7075h
  004b706a  0c01                  or        al,1
  004b706c  c7475400800100        mov       dword [edi+54h],18000h
  004b7073  8907                  mov       [edi],eax

LAB_004b7075:
  004b7075  8b5500                mov       edx,[ebp]
  004b7078  8b4620                mov       eax,[esi+20h]
  004b707b  8b4e24                mov       ecx,[esi+24h]
  004b707e  89542420              mov       [esp+20h],edx
  004b7082  8b13                  mov       edx,[ebx]
  004b7084  89442424              mov       [esp+24h],eax
  004b7088  895500                mov       [ebp],edx
  004b708b  8b4720                mov       eax,[edi+20h]
  004b708e  6aff                  push      0FFFFFFFFh
  004b7090  894c242c              mov       [esp+2Ch],ecx
  004b7094  894620                mov       [esi+20h],eax
  004b7097  8b4f24                mov       ecx,[edi+24h]
  004b709a  6a00                  push      0
  004b709c  6a39                  push      39h
  004b709e  56                    push      esi
  004b709f  894e24                mov       [esi+24h],ecx
  004b70a2  e82939fcff            call      0047A9D0h
  004b70a7  8b542430              mov       edx,[esp+30h]
  004b70ab  8b442434              mov       eax,[esp+34h]
  004b70af  8b4c2438              mov       ecx,[esp+38h]
  004b70b3  83c410                add       esp,10h
  004b70b6  895500                mov       [ebp],edx
  004b70b9  894620                mov       [esi+20h],eax
  004b70bc  894e24                mov       [esi+24h],ecx
  004b70bf  e918020000            jmp       004B72DCh

LAB_004b70c4:
  004b70c4  a810                  test      al,10h
  004b70c6  0f8510020000          jne       near 004B72DCh
  004b70cc  a904080000            test      eax,804h
  004b70d1  0f8505020000          jne       near 004B72DCh
  004b70d7  33c9                  xor       ecx,ecx
  004b70d9  8a4e42                mov       cl,[esi+42h]
  004b70dc  83f903                cmp       ecx,3
  004b70df  0f87d8010000          ja        near 004B72BDh
  004b70e5  ff248d2c754b00        jmp       dword [ecx*4+4B752Ch]
  004b70ec  66837e1800            cmp       word [esi+18h],0
  004b70f1  740e                  je        short 004B7101h
  004b70f3  0d00000001            or        eax,1000000h
  004b70f8  c7461800000000        mov       dword [esi+18h],0
  004b70ff  8906                  mov       [esi],eax

LAB_004b7101:
  004b7101  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b7109  7d09                  jge       short 004B7114h
  004b710b  c6464300              mov       byte [esi+43h],0
  004b710f  e9a9010000            jmp       004B72BDh

LAB_004b7114:
  004b7114  8a4643                mov       al,[esi+43h]
  004b7117  84c0                  test      al,al
  004b7119  7405                  je        short 004B7120h
  004b711b  fec8                  dec       al
  004b711d  884643                mov       [esi+43h],al

LAB_004b7120:
  004b7120  8b465c                mov       eax,[esi+5Ch]
  004b7123  8bd0                  mov       edx,eax
  004b7125  c1fa04                sar       edx,4
  004b7128  2bc2                  sub       eax,edx
  004b712a  89465c                mov       [esi+5Ch],eax
  004b712d  e98b010000            jmp       004B72BDh
  004b7132  66837e1800            cmp       word [esi+18h],0
  004b7137  740e                  je        short 004B7147h
  004b7139  0d00000001            or        eax,1000000h
  004b713e  c7461800000000        mov       dword [esi+18h],0
  004b7145  8906                  mov       [esi],eax

LAB_004b7147:
  004b7147  8b44243c              mov       eax,[esp+3Ch]
  004b714b  8d8880000000          lea       ecx,[eax+80h]
  004b7151  81e1ff0f0000          and       ecx,0FFFh
  004b7157  81f900010000          cmp       ecx,100h
  004b715d  7f1a                  jg        short 004B7179h
  004b715f  66014632              add       [esi+32h],ax
  004b7163  8b465c                mov       eax,[esi+5Ch]
  004b7166  8bc8                  mov       ecx,eax
  004b7168  c6464300              mov       byte [esi+43h],0
  004b716c  c1f904                sar       ecx,4
  004b716f  2bc1                  sub       eax,ecx
  004b7171  89465c                mov       [esi+5Ch],eax
  004b7174  e944010000            jmp       004B72BDh

LAB_004b7179:
  004b7179  25ff0f0000            and       eax,0FFFh
  004b717e  33d2                  xor       edx,edx
  004b7180  3d00080000            cmp       eax,800h
  004b7185  0f9dc2                setge     dl
  004b7188  4a                    dec       edx
  004b7189  81e200010000          and       edx,100h
  004b718f  83c280                add       edx,0FFFFFF80h
  004b7192  8bc2                  mov       eax,edx
  004b7194  66014632              add       [esi+32h],ax
  004b7198  8b465c                mov       eax,[esi+5Ch]
  004b719b  8bc8                  mov       ecx,eax
  004b719d  c1f904                sar       ecx,4
  004b71a0  2bc1                  sub       eax,ecx
  004b71a2  89465c                mov       [esi+5Ch],eax
  004b71a5  e913010000            jmp       004B72BDh
  004b71aa  b901000000            mov       ecx,1
  004b71af  66394e18              cmp       [esi+18h],cx
  004b71b3  740a                  je        short 004B71BFh
  004b71b5  0d00000001            or        eax,1000000h
  004b71ba  894e18                mov       [esi+18h],ecx
  004b71bd  8906                  mov       [esi],eax

LAB_004b71bf:
  004b71bf  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b71c7  7d09                  jge       short 004B71D2h
  004b71c9  c6464300              mov       byte [esi+43h],0
  004b71cd  e9eb000000            jmp       004B72BDh

LAB_004b71d2:
  004b71d2  8a4643                mov       al,[esi+43h]
  004b71d5  84c0                  test      al,al
  004b71d7  7405                  je        short 004B71DEh
  004b71d9  fec8                  dec       al
  004b71db  884643                mov       [esi+43h],al

LAB_004b71de:
  004b71de  8b44243c              mov       eax,[esp+3Ch]
  004b71e2  8d9080000000          lea       edx,[eax+80h]
  004b71e8  81e2ff0f0000          and       edx,0FFFh
  004b71ee  81fa00010000          cmp       edx,100h
  004b71f4  7e1b                  jle       short 004B7211h
  004b71f6  25ff0f0000            and       eax,0FFFh
  004b71fb  33c9                  xor       ecx,ecx
  004b71fd  3d00080000            cmp       eax,800h
  004b7202  0f9dc1                setge     cl
  004b7205  49                    dec       ecx
  004b7206  81e100010000          and       ecx,100h
  004b720c  83c180                add       ecx,0FFFFFF80h
  004b720f  8bc1                  mov       eax,ecx

LAB_004b7211:
  004b7211  66014632              add       [esi+32h],ax
  004b7215  8b465c                mov       eax,[esi+5Ch]
  004b7218  ba00000100            mov       edx,10000h
  004b721d  2bd0                  sub       edx,eax
  004b721f  c1fa04                sar       edx,4
  004b7222  03d0                  add       edx,eax
  004b7224  89565c                mov       [esi+5Ch],edx
  004b7227  e991000000            jmp       004B72BDh
  004b722c  33ff                  xor       edi,edi
  004b722e  66397e18              cmp       [esi+18h],di
  004b7232  740a                  je        short 004B723Eh
  004b7234  0d00000001            or        eax,1000000h
  004b7239  897e18                mov       [esi+18h],edi
  004b723c  8906                  mov       [esi],eax

LAB_004b723e:
  004b723e  8a4643                mov       al,[esi+43h]
  004b7241  84c0                  test      al,al
  004b7243  7405                  je        short 004B724Ah
  004b7245  fec8                  dec       al
  004b7247  884643                mov       [esi+43h],al

LAB_004b724a:
  004b724a  0fbf4610              movsx     eax,word [esi+10h]
  004b724e  50                    push      eax
  004b724f  c7465c00400000        mov       dword [esi+5Ch],4000h
  004b7256  e8c1e70800            call      00545A1Ch
  004b725b  0faf465c              imul      eax,[esi+5Ch]
  004b725f  8b4e50                mov       ecx,[esi+50h]
  004b7262  c1f80c                sar       eax,0Ch
  004b7265  03c8                  add       ecx,eax
  004b7267  894e50                mov       [esi+50h],ecx
  004b726a  0fbf4e10              movsx     ecx,word [esi+10h]
  004b726e  51                    push      ecx
  004b726f  e8a2e70800            call      00545A16h
  004b7274  0faf465c              imul      eax,[esi+5Ch]
  004b7278  8b5e58                mov       ebx,[esi+58h]
  004b727b  897e5c                mov       [esi+5Ch],edi
  004b727e  c1f80c                sar       eax,0Ch
  004b7281  03d8                  add       ebx,eax
  004b7283  8b44243c              mov       eax,[esp+3Ch]
  004b7287  895e58                mov       [esi+58h],ebx
  004b728a  8d9080000000          lea       edx,[eax+80h]
  004b7290  81e2ff0f0000          and       edx,0FFFh
  004b7296  81fa00010000          cmp       edx,100h
  004b729c  7e1b                  jle       short 004B72B9h
  004b729e  25ff0f0000            and       eax,0FFFh
  004b72a3  33c9                  xor       ecx,ecx
  004b72a5  3d00080000            cmp       eax,800h
  004b72aa  0f9dc1                setge     cl
  004b72ad  49                    dec       ecx
  004b72ae  81e100010000          and       ecx,100h
  004b72b4  83c180                add       ecx,0FFFFFF80h
  004b72b7  8bc1                  mov       eax,ecx

LAB_004b72b9:
  004b72b9  66014632              add       [esi+32h],ax

LAB_004b72bd:
  004b72bd  66837e1801            cmp       word [esi+18h],1
  004b72c2  7518                  jne       short 004B72DCh
  004b72c4  66837e1a09            cmp       word [esi+1Ah],9
  004b72c9  7511                  jne       short 004B72DCh
  004b72cb  8d561c                lea       edx,[esi+1Ch]
  004b72ce  52                    push      edx
  004b72cf  68f82a5500            push      552AF8h
  004b72d4  e807690800            call      0053DBE0h
  004b72d9  83c408                add       esp,8

LAB_004b72dc:
  004b72dc  8b442414              mov       eax,[esp+14h]
  004b72e0  50                    push      eax
  004b72e1  56                    push      esi
  004b72e2  e8f935feff            call      0049A8E0h
  004b72e7  83c408                add       esp,8
  004b72ea  56                    push      esi
  004b72eb  e850c60700            call      00533940h
  004b72f0  8b9690000000          mov       edx,[esi+90h]
  004b72f6  8b466c                mov       eax,[esi+6Ch]
  004b72f9  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004b72ff  83c404                add       esp,4
  004b7302  899690000000          mov       [esi+90h],edx
  004b7308  8a08                  mov       cl,[eax]
  004b730a  f6c101                test      cl,1
  004b730d  7506                  jne       short 004B7315h
  004b730f  8b4860                mov       ecx,[eax+60h]
  004b7312  894e48                mov       [esi+48h],ecx

LAB_004b7315:
  004b7315  e826fe0800            call      00547140h
  004b731a  8b7e60                mov       edi,[esi+60h]
  004b731d  25ff000000            and       eax,0FFh
  004b7322  0500010000            add       eax,100h
  004b7327  66014614              add       [esi+14h],ax
  004b732b  8b4648                mov       eax,[esi+48h]
  004b732e  668b4e14              mov       cx,[esi+14h]
  004b7332  3bc7                  cmp       eax,edi
  004b7334  7c02                  jl        short 004B7338h
  004b7336  8bf8                  mov       edi,eax

LAB_004b7338:
  004b7338  8b06                  mov       eax,[esi]
  004b733a  a802                  test      al,2
  004b733c  741a                  je        short 004B7358h
  004b733e  f6c404                test      ah,4
  004b7341  7535                  jne       short 004B7378h
  004b7343  8b4620                mov       eax,[esi+20h]
  004b7346  2bf8                  sub       edi,eax
  004b7348  81c700800200          add       edi,28000h
  004b734e  c1ff03                sar       edi,3
  004b7351  03f8                  add       edi,eax
  004b7353  897e20                mov       [esi+20h],edi
  004b7356  eb20                  jmp       short 004B7378h

LAB_004b7358:
  004b7358  8b5e20                mov       ebx,[esi+20h]
  004b735b  0fbfd1                movsx     edx,cx
  004b735e  52                    push      edx
  004b735f  e8b8e60800            call      00545A1Ch
  004b7364  0500410000            add       eax,4100h
  004b7369  c1e005                shl       eax,5
  004b736c  03c7                  add       eax,edi
  004b736e  2bc3                  sub       eax,ebx
  004b7370  c1f804                sar       eax,4
  004b7373  03c3                  add       eax,ebx
  004b7375  894620                mov       [esi+20h],eax

LAB_004b7378:
  004b7378  8b44242c              mov       eax,[esp+2Ch]
  004b737c  50                    push      eax
  004b737d  56                    push      esi
  004b737e  e86da9feff            call      004A1CF0h
  004b7383  8b44241c              mov       eax,[esp+1Ch]
  004b7387  83c408                add       esp,8
  004b738a  85c0                  test      eax,eax
  004b738c  0f8492010000          je        near 004B7524h
  004b7392  f70616080000          test      dword [esi],816h
  004b7398  0f8586010000          jne       near 004B7524h
  004b739e  8a4643                mov       al,[esi+43h]
  004b73a1  84c0                  test      al,al
  004b73a3  0f857b010000          jne       near 004B7524h
  004b73a9  8b466c                mov       eax,[esi+6Ch]
  004b73ac  8b5e1c                mov       ebx,[esi+1Ch]
  004b73af  8b4e24                mov       ecx,[esi+24h]
  004b73b2  8b781c                mov       edi,[eax+1Ch]
  004b73b5  8b6824                mov       ebp,[eax+24h]
  004b73b8  2bfb                  sub       edi,ebx
  004b73ba  2be9                  sub       ebp,ecx
  004b73bc  c1ff0c                sar       edi,0Ch
  004b73bf  c1fd0c                sar       ebp,0Ch
  004b73c2  8bcf                  mov       ecx,edi
  004b73c4  8bd5                  mov       edx,ebp
  004b73c6  0fafcf                imul      ecx,edi
  004b73c9  0fafd5                imul      edx,ebp
  004b73cc  03ca                  add       ecx,edx
  004b73ce  51                    push      ecx
  004b73cf  e83ce60800            call      00545A10h
  004b73d4  55                    push      ebp
  004b73d5  57                    push      edi
  004b73d6  89442418              mov       [esp+18h],eax
  004b73da  e843e60800            call      00545A22h
  004b73df  668b4e32              mov       cx,[esi+32h]
  004b73e3  2bc1                  sub       eax,ecx
  004b73e5  25ff0f0000            and       eax,0FFFh
  004b73ea  8944243c              mov       [esp+3Ch],eax
  004b73ee  e84dfd0800            call      00547140h
  004b73f3  99                    cdq
  004b73f4  b964000000            mov       ecx,64h
  004b73f9  f7f9                  idiv      ecx
  004b73fb  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b7403  7d49                  jge       short 004B744Eh
  004b7405  83fa3c                cmp       edx,3Ch
  004b7408  0f8ddf000000          jge       near 004B74EDh
  004b740e  8b54243c              mov       edx,[esp+3Ch]
  004b7412  81c200010000          add       edx,100h
  004b7418  81e2ff0f0000          and       edx,0FFFh
  004b741e  81fa00020000          cmp       edx,200h
  004b7424  0f8f8f000000          jg        near 004B74B9h
  004b742a  8b06                  mov       eax,[esi]
  004b742c  c7461805000000        mov       dword [esi+18h],5
  004b7433  25fffffffe            and       eax,0FEFFFFFFh
  004b7438  c6464201              mov       byte [esi+42h],1
  004b743c  0c02                  or        al,2
  004b743e  66c746100000          mov       word [esi+10h],0
  004b7444  8906                  mov       [esi],eax
  004b7446  5f                    pop       edi
  004b7447  5e                    pop       esi
  004b7448  5d                    pop       ebp
  004b7449  5b                    pop       ebx
  004b744a  83c428                add       esp,28h
  004b744d  c3                    ret

LAB_004b744e:
  004b744e  83fa1e                cmp       edx,1Eh
  004b7451  7d76                  jge       short 004B74C9h
  004b7453  8b44243c              mov       eax,[esp+3Ch]
  004b7457  0500010000            add       eax,100h
  004b745c  25ff0f0000            and       eax,0FFFh
  004b7461  3d00020000            cmp       eax,200h
  004b7466  7f51                  jg        short 004B74B9h
  004b7468  f6c201                test      dl,1
  004b746b  7426                  je        short 004B7493h
  004b746d  8b0e                  mov       ecx,[esi]
  004b746f  c7461803000000        mov       dword [esi+18h],3
  004b7476  81e1fffffffe          and       ecx,0FEFFFFFFh
  004b747c  c6464200              mov       byte [esi+42h],0
  004b7480  83c902                or        ecx,2
  004b7483  66c746100100          mov       word [esi+10h],1
  004b7489  890e                  mov       [esi],ecx
  004b748b  5f                    pop       edi
  004b748c  5e                    pop       esi
  004b748d  5d                    pop       ebp
  004b748e  5b                    pop       ebx
  004b748f  83c428                add       esp,28h
  004b7492  c3                    ret

LAB_004b7493:
  004b7493  8b16                  mov       edx,[esi]
  004b7495  c7461804000000        mov       dword [esi+18h],4
  004b749c  81e2fffffffe          and       edx,0FEFFFFFFh
  004b74a2  c6464202              mov       byte [esi+42h],2
  004b74a6  83ca02                or        edx,2
  004b74a9  66c746100100          mov       word [esi+10h],1
  004b74af  8916                  mov       [esi],edx
  004b74b1  5f                    pop       edi
  004b74b2  5e                    pop       esi
  004b74b3  5d                    pop       ebp
  004b74b4  5b                    pop       ebx
  004b74b5  83c428                add       esp,28h
  004b74b8  c3                    ret

LAB_004b74b9:
  004b74b9  c6464201              mov       byte [esi+42h],1
  004b74bd  c6464301              mov       byte [esi+43h],1
  004b74c1  5f                    pop       edi
  004b74c2  5e                    pop       esi
  004b74c3  5d                    pop       ebp
  004b74c4  5b                    pop       ebx
  004b74c5  83c428                add       esp,28h
  004b74c8  c3                    ret

LAB_004b74c9:
  004b74c9  83fa50                cmp       edx,50h
  004b74cc  7d1f                  jge       short 004B74EDh
  004b74ce  c6464202              mov       byte [esi+42h],2
  004b74d2  e869fc0800            call      00547140h
  004b74d7  99                    cdq
  004b74d8  b90a000000            mov       ecx,0Ah
  004b74dd  f7f9                  idiv      ecx
  004b74df  80c214                add       dl,14h
  004b74e2  885643                mov       [esi+43h],dl
  004b74e5  5f                    pop       edi
  004b74e6  5e                    pop       esi
  004b74e7  5d                    pop       ebp
  004b74e8  5b                    pop       ebx
  004b74e9  83c428                add       esp,28h
  004b74ec  c3                    ret

LAB_004b74ed:
  004b74ed  c6464203              mov       byte [esi+42h],3
  004b74f1  e84afc0800            call      00547140h
  004b74f6  99                    cdq
  004b74f7  b90a000000            mov       ecx,0Ah
  004b74fc  f7f9                  idiv      ecx
  004b74fe  02d1                  add       dl,cl
  004b7500  885643                mov       [esi+43h],dl
  004b7503  e838fc0800            call      00547140h
  004b7508  99                    cdq
  004b7509  b9000c0000            mov       ecx,0C00h
  004b750e  8b7c243c              mov       edi,[esp+3Ch]
  004b7512  f7f9                  idiv      ecx
  004b7514  03d7                  add       edx,edi
  004b7516  66035632              add       dx,[esi+32h]
  004b751a  81ea00060000          sub       edx,600h
  004b7520  66895610              mov       [esi+10h],dx

LAB_004b7524:
  004b7524  5f                    pop       edi
  004b7525  5e                    pop       esi
  004b7526  5d                    pop       ebp
  004b7527  5b                    pop       ebx
  004b7528  83c428                add       esp,28h
  004b752b  c3                    ret
  004b752c  ec                    in        al,dx
  004b752d  704b                  jo        short 004B757Ah
  004b752f  0032                  add       [edx],dh
  004b7531  714b                  jno       short 004B757Eh
  004b7533  00aa714b002c          add       [edx+2C004B71h],ch
  004b7539  724b                  jb        short 004B7586h
  004b753b  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
