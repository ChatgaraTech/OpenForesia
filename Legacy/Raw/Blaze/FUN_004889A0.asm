Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004889a0  section .text  offset fichier 0x87da0  4864 octets

  004889a0  81ec90000000          sub       esp,90h
  004889a6  53                    push      ebx
  004889a7  55                    push      ebp
  004889a8  56                    push      esi
  004889a9  8bb424a0000000        mov       esi,[esp+0A0h]
  004889b0  b864000000            mov       eax,64h
  004889b5  33db                  xor       ebx,ebx
  004889b7  6689442436            mov       [esp+36h],ax
  004889bc  668944243a            mov       [esp+3Ah],ax
  004889c1  6689442442            mov       [esp+42h],ax
  004889c6  6689442446            mov       [esp+46h],ax
  004889cb  8a4641                mov       al,[esi+41h]
  004889ce  57                    push      edi
  004889cf  84c0                  test      al,al
  004889d1  66895c2434            mov       [esp+34h],bx
  004889d6  66c74424361e00        mov       word [esp+36h],1Eh
  004889dd  66c74424383200        mov       word [esp+38h],32h
  004889e4  66895c243c            mov       [esp+3Ch],bx
  004889e9  66895c2440            mov       [esp+40h],bx
  004889ee  66895c2442            mov       [esp+42h],bx
  004889f3  66c74424445000        mov       word [esp+44h],50h
  004889fa  66895c2448            mov       [esp+48h],bx
  004889ff  7510                  jne       short 00488A11h
  00488a01  53                    push      ebx
  00488a02  6a03                  push      3
  00488a04  56                    push      esi
  00488a05  c6464101              mov       byte [esi+41h],1
  00488a09  e8d20e0000            call      004898E0h
  00488a0e  83c40c                add       esp,0Ch

LAB_00488a11:
  00488a11  8b461c                mov       eax,[esi+1Ch]
  00488a14  8b4e20                mov       ecx,[esi+20h]
  00488a17  8b5624                mov       edx,[esi+24h]
  00488a1a  8d6e1c                lea       ebp,[esi+1Ch]
  00488a1d  89442428              mov       [esp+28h],eax
  00488a21  8b4664                mov       eax,[esi+64h]
  00488a24  894c242c              mov       [esp+2Ch],ecx
  00488a28  8b4e60                mov       ecx,[esi+60h]
  00488a2b  89542430              mov       [esp+30h],edx
  00488a2f  668b5632              mov       dx,[esi+32h]
  00488a33  89442450              mov       [esp+50h],eax
  00488a37  8b467c                mov       eax,[esi+7Ch]
  00488a3a  894c2454              mov       [esp+54h],ecx
  00488a3e  668b8e90000000        mov       cx,[esi+90h]
  00488a45  668994248e000000      mov       [esp+8Eh],dx
  00488a4d  8b9680000000          mov       edx,[esi+80h]
  00488a53  89442464              mov       [esp+64h],eax
  00488a57  8b8684000000          mov       eax,[esi+84h]
  00488a5d  66894c246c            mov       [esp+6Ch],cx
  00488a62  8b8e88000000          mov       ecx,[esi+88h]
  00488a68  8b7e6c                mov       edi,[esi+6Ch]
  00488a6b  89542460              mov       [esp+60h],edx
  00488a6f  8b968c000000          mov       edx,[esi+8Ch]
  00488a75  8944244c              mov       [esp+4Ch],eax
  00488a79  894c2458              mov       [esp+58h],ecx
  00488a7d  8d442424              lea       eax,[esp+24h]
  00488a81  89542468              mov       [esp+68h],edx
  00488a85  8d4c2470              lea       ecx,[esp+70h]
  00488a89  50                    push      eax
  00488a8a  8d542478              lea       edx,[esp+78h]
  00488a8e  51                    push      ecx
  00488a8f  52                    push      edx
  00488a90  56                    push      esi
  00488a91  e8eafe0a00            call      00538980h
  00488a96  83c410                add       esp,10h
  00488a99  8d44245c              lea       eax,[esp+5Ch]
  00488a9d  8d4c2420              lea       ecx,[esp+20h]
  00488aa1  8d542418              lea       edx,[esp+18h]
  00488aa5  50                    push      eax
  00488aa6  51                    push      ecx
  00488aa7  8d44241c              lea       eax,[esp+1Ch]
  00488aab  52                    push      edx
  00488aac  8d4c241c              lea       ecx,[esp+1Ch]
  00488ab0  50                    push      eax
  00488ab1  51                    push      ecx
  00488ab2  56                    push      esi
  00488ab3  e868d40a00            call      00535F20h
  00488ab8  8b4c2428              mov       ecx,[esp+28h]
  00488abc  8b542474              mov       edx,[esp+74h]
  00488ac0  83c418                add       esp,18h
  00488ac3  b840000000            mov       eax,40h
  00488ac8  2bc1                  sub       eax,ecx
  00488aca  52                    push      edx
  00488acb  56                    push      esi
  00488acc  89442424              mov       [esp+24h],eax
  00488ad0  e80bd30400            call      004D5DE0h
  00488ad5  8b4500                mov       eax,[ebp]
  00488ad8  8b571c                mov       edx,[edi+1Ch]
  00488adb  8b4e20                mov       ecx,[esi+20h]
  00488ade  2bc2                  sub       eax,edx
  00488ae0  8b5720                mov       edx,[edi+20h]
  00488ae3  83c408                add       esp,8
  00488ae6  2bca                  sub       ecx,edx
  00488ae8  8b5624                mov       edx,[esi+24h]
  00488aeb  2b5724                sub       edx,[edi+24h]
  00488aee  c1f80c                sar       eax,0Ch
  00488af1  8bf8                  mov       edi,eax
  00488af3  c1f90c                sar       ecx,0Ch
  00488af6  0faff8                imul      edi,eax
  00488af9  8bc1                  mov       eax,ecx
  00488afb  c1fa0c                sar       edx,0Ch
  00488afe  0fafc1                imul      eax,ecx
  00488b01  8bca                  mov       ecx,edx
  00488b03  03f8                  add       edi,eax
  00488b05  0fafca                imul      ecx,edx
  00488b08  03f9                  add       edi,ecx
  00488b0a  57                    push      edi
  00488b0b  e800cf0b00            call      00545A10h
  00488b10  89442414              mov       [esp+14h],eax
  00488b14  8b442420              mov       eax,[esp+20h]
  00488b18  3bc3                  cmp       eax,ebx
  00488b1a  0f84c9070000          je        near 004892E9h
  00488b20  8b3e                  mov       edi,[esi]
  00488b22  33d2                  xor       edx,edx
  00488b24  8a5604                mov       dl,[esi+4]
  00488b27  81e7fffffffd          and       edi,0FDFFFFFFh
  00488b2d  0fbf4e18              movsx     ecx,word [esi+18h]
  00488b31  893e                  mov       [esi],edi
  00488b33  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00488b3a  8bc7                  mov       eax,edi
  00488b3c  8b0c8a                mov       ecx,[edx+ecx*4]
  00488b3f  a900000001            test      eax,1000000h
  00488b44  741b                  je        short 00488B61h
  00488b46  66ff461a              inc       word [esi+1Ah]
  00488b4a  668b461a              mov       ax,[esi+1Ah]
  00488b4e  33d2                  xor       edx,edx
  00488b50  668b5116              mov       dx,[ecx+16h]
  00488b54  0fbfc0                movsx     eax,ax
  00488b57  3bc2                  cmp       eax,edx
  00488b59  7c27                  jl        short 00488B82h
  00488b5b  66895e1a              mov       [esi+1Ah],bx
  00488b5f  eb21                  jmp       short 00488B82h

LAB_00488b61:
  00488b61  66ff461a              inc       word [esi+1Ah]
  00488b65  668b561a              mov       dx,[esi+1Ah]
  00488b69  33ff                  xor       edi,edi
  00488b6b  668b7916              mov       di,[ecx+16h]
  00488b6f  0fbfca                movsx     ecx,dx
  00488b72  3bcf                  cmp       ecx,edi
  00488b74  7c0c                  jl        short 00488B82h
  00488b76  4a                    dec       edx
  00488b77  0d00000002            or        eax,2000000h
  00488b7c  6689561a              mov       [esi+1Ah],dx
  00488b80  8906                  mov       [esi],eax

LAB_00488b82:
  00488b82  8a4642                mov       al,[esi+42h]
  00488b85  8b0e                  mov       ecx,[esi]
  00488b87  3c05                  cmp       al,5
  00488b89  bb10000000            mov       ebx,10h
  00488b8e  7504                  jne       short 00488B94h
  00488b90  0bcb                  or        ecx,ebx
  00488b92  eb03                  jmp       short 00488B97h

LAB_00488b94:
  00488b94  83e1ef                and       ecx,0FFFFFFEFh

LAB_00488b97:
  00488b97  f6c102                test      cl,2
  00488b9a  890e                  mov       [esi],ecx
  00488b9c  0f840b020000          je        near 00488DADh
  00488ba2  3c0c                  cmp       al,0Ch
  00488ba4  7479                  je        short 00488C1Fh
  00488ba6  3c0d                  cmp       al,0Dh
  00488ba8  7475                  je        short 00488C1Fh
  00488baa  66837e1a08            cmp       word [esi+1Ah],8
  00488baf  7553                  jne       short 00488C04h
  00488bb1  8d542434              lea       edx,[esp+34h]
  00488bb5  6a00                  push      0
  00488bb7  52                    push      edx
  00488bb8  55                    push      ebp
  00488bb9  56                    push      esi
  00488bba  e871100900            call      00519C30h
  00488bbf  8bf8                  mov       edi,eax
  00488bc1  a1b8e05d00            mov       eax,[5DE0B8h]
  00488bc6  83c410                add       esp,10h
  00488bc9  85c0                  test      eax,eax
  00488bcb  7437                  je        short 00488C04h
  00488bcd  66837e1a08            cmp       word [esi+1Ah],8
  00488bd2  7530                  jne       short 00488C04h
  00488bd4  8b0f                  mov       ecx,[edi]
  00488bd6  57                    push      edi
  00488bd7  83c940                or        ecx,40h
  00488bda  56                    push      esi
  00488bdb  66899f96000000        mov       [edi+96h],bx
  00488be2  890f                  mov       [edi],ecx
  00488be4  e8970f0b00            call      00539B80h
  00488be9  83c408                add       esp,8
  00488bec  83c71c                add       edi,1Ch
  00488bef  57                    push      edi
  00488bf0  6800180000            push      1800h
  00488bf5  6800030000            push      300h
  00488bfa  6a03                  push      3
  00488bfc  e81f4f0b00            call      0053DB20h
  00488c01  83c410                add       esp,10h

LAB_00488c04:
  00488c04  8b06                  mov       eax,[esi]
  00488c06  a900000002            test      eax,2000000h
  00488c0b  7412                  je        short 00488C1Fh
  00488c0d  a801                  test      al,1
  00488c0f  750a                  jne       short 00488C1Bh
  00488c11  24fd                  and       al,0FDh
  00488c13  c6464300              mov       byte [esi+43h],0
  00488c17  8906                  mov       [esi],eax
  00488c19  eb04                  jmp       short 00488C1Fh

LAB_00488c1b:
  00488c1b  24fd                  and       al,0FDh
  00488c1d  8906                  mov       [esi],eax

LAB_00488c1f:
  00488c1f  807e420c              cmp       byte [esi+42h],0Ch
  00488c23  0f85ae000000          jne       near 00488CD7h
  00488c29  668b461a              mov       ax,[esi+1Ah]
  00488c2d  663d0800              cmp       ax,8
  00488c31  7507                  jne       short 00488C3Ah
  00488c33  66837e1803            cmp       word [esi+18h],3
  00488c38  7415                  je        short 00488C4Fh

LAB_00488c3a:
  00488c3a  663d0a00              cmp       ax,0Ah
  00488c3e  0f8593000000          jne       near 00488CD7h
  00488c44  66837e1805            cmp       word [esi+18h],5
  00488c49  0f8588000000          jne       near 00488CD7h

LAB_00488c4f:
  00488c4f  8d442434              lea       eax,[esp+34h]
  00488c53  6a00                  push      0
  00488c55  50                    push      eax
  00488c56  55                    push      ebp
  00488c57  56                    push      esi
  00488c58  e8d30f0900            call      00519C30h
  00488c5d  8b0db8e05d00          mov       ecx,[5DE0B8h]
  00488c63  83c410                add       esp,10h
  00488c66  85c9                  test      ecx,ecx
  00488c68  746d                  je        short 00488CD7h
  00488c6a  668b4e1a              mov       cx,[esi+1Ah]
  00488c6e  6683f908              cmp       cx,8
  00488c72  7507                  jne       short 00488C7Bh
  00488c74  66837e1803            cmp       word [esi+18h],3
  00488c79  740d                  je        short 00488C88h

LAB_00488c7b:
  00488c7b  6683f90a              cmp       cx,0Ah
  00488c7f  7556                  jne       short 00488CD7h
  00488c81  66837e1805            cmp       word [esi+18h],5
  00488c86  754f                  jne       short 00488CD7h

LAB_00488c88:
  00488c88  66837e1201            cmp       word [esi+12h],1
  00488c8d  750b                  jne       short 00488C9Ah
  00488c8f  66c780960000000000    mov       word [eax+96h],0
  00488c98  eb0e                  jmp       short 00488CA8h

LAB_00488c9a:
  00488c9a  8b08                  mov       ecx,[eax]
  00488c9c  66899896000000        mov       [eax+96h],bx
  00488ca3  83c940                or        ecx,40h
  00488ca6  8908                  mov       [eax],ecx

LAB_00488ca8:
  00488ca8  8b542424              mov       edx,[esp+24h]
  00488cac  50                    push      eax
  00488cad  56                    push      esi
  00488cae  668b4a30              mov       cx,[edx+30h]
  00488cb2  668bd9                mov       bx,cx
  00488cb5  66c1fb02              sar       bx,2
  00488cb9  0fbff9                movsx     edi,cx
  00488cbc  2bcb                  sub       ecx,ebx
  00488cbe  66894a30              mov       [edx+30h],cx
  00488cc2  e8b90e0b00            call      00539B80h
  00488cc7  8b4c242c              mov       ecx,[esp+2Ch]
  00488ccb  83c408                add       esp,8
  00488cce  bb10000000            mov       ebx,10h
  00488cd3  66897930              mov       [ecx+30h],di

LAB_00488cd7:
  00488cd7  807e420d              cmp       byte [esi+42h],0Dh
  00488cdb  0f850f010000          jne       near 00488DF0h
  00488ce1  8d542440              lea       edx,[esp+40h]
  00488ce5  6a00                  push      0
  00488ce7  52                    push      edx
  00488ce8  55                    push      ebp
  00488ce9  56                    push      esi
  00488cea  e8410f0900            call      00519C30h
  00488cef  8bf8                  mov       edi,eax
  00488cf1  a1b8e05d00            mov       eax,[5DE0B8h]
  00488cf6  83c410                add       esp,10h
  00488cf9  85c0                  test      eax,eax
  00488cfb  0f8405010000          je        near 00488E06h
  00488d01  66899f96000000        mov       [edi+96h],bx
  00488d08  8b1f                  mov       ebx,[edi]
  00488d0a  83cb40                or        ebx,40h
  00488d0d  57                    push      edi
  00488d0e  891f                  mov       [edi],ebx
  00488d10  8b4c2428              mov       ecx,[esp+28h]
  00488d14  56                    push      esi
  00488d15  668b4130              mov       ax,[ecx+30h]
  00488d19  0fbfd8                movsx     ebx,ax
  00488d1c  66d1f8                sar       ax,1
  00488d1f  66894130              mov       [ecx+30h],ax
  00488d23  e8580e0b00            call      00539B80h
  00488d28  8b44242c              mov       eax,[esp+2Ch]
  00488d2c  83c408                add       esp,8
  00488d2f  8d4f1c                lea       ecx,[edi+1Ch]
  00488d32  51                    push      ecx
  00488d33  6800180000            push      1800h
  00488d38  6800030000            push      300h
  00488d3d  6a03                  push      3
  00488d3f  66895830              mov       [eax+30h],bx
  00488d43  e8d84d0b00            call      0053DB20h
  00488d48  8b07                  mov       eax,[edi]
  00488d4a  83c410                add       esp,10h
  00488d4d  a900000040            test      eax,40000000h
  00488d52  0f85a5000000          jne       near 00488DFDh
  00488d58  6aff                  push      0FFFFFFFFh
  00488d5a  6a00                  push      0
  00488d5c  6a39                  push      39h
  00488d5e  56                    push      esi
  00488d5f  e86c1cffff            call      0047A9D0h
  00488d64  8b07                  mov       eax,[edi]
  00488d66  83c410                add       esp,10h
  00488d69  0c01                  or        al,1
  00488d6b  8907                  mov       [edi],eax
  00488d6d  0fbf5632              movsx     edx,word [esi+32h]
  00488d71  52                    push      edx
  00488d72  e8a5cc0b00            call      00545A1Ch
  00488d77  8b4e5c                mov       ecx,[esi+5Ch]
  00488d7a  c1f90d                sar       ecx,0Dh
  00488d7d  0fafc1                imul      eax,ecx
  00488d80  894750                mov       [edi+50h],eax
  00488d83  0fbf5632              movsx     edx,word [esi+32h]
  00488d87  52                    push      edx
  00488d88  e889cc0b00            call      00545A16h
  00488d8d  8b4e5c                mov       ecx,[esi+5Ch]
  00488d90  c7475400800100        mov       dword [edi+54h],18000h
  00488d97  c1f90d                sar       ecx,0Dh
  00488d9a  0fafc1                imul      eax,ecx
  00488d9d  894758                mov       [edi+58h],eax
  00488da0  8b4c2410              mov       ecx,[esp+10h]
  00488da4  33db                  xor       ebx,ebx
  00488da6  bf01000000            mov       edi,1
  00488dab  eb64                  jmp       short 00488E11h

LAB_00488dad:
  00488dad  f6c101                test      cl,1
  00488db0  743e                  je        short 00488DF0h
  00488db2  66837e1a03            cmp       word [esi+1Ah],3
  00488db7  7d37                  jge       short 00488DF0h
  00488db9  e882e30b00            call      00547140h
  00488dbe  99                    cdq
  00488dbf  b93c000000            mov       ecx,3Ch
  00488dc4  f7f9                  idiv      ecx
  00488dc6  8b4c2410              mov       ecx,[esp+10h]
  00488dca  3bd1                  cmp       edx,ecx
  00488dcc  7d26                  jge       short 00488DF4h
  00488dce  8b16                  mov       edx,[esi]
  00488dd0  66c746180500          mov       word [esi+18h],5
  00488dd6  81e2fffffffe          and       edx,0FEFFFFFFh
  00488ddc  66c7461a0000          mov       word [esi+1Ah],0
  00488de2  83ca02                or        edx,2
  00488de5  33db                  xor       ebx,ebx
  00488de7  8916                  mov       [esi],edx
  00488de9  bf01000000            mov       edi,1
  00488dee  eb21                  jmp       short 00488E11h

LAB_00488df0:
  00488df0  8b4c2410              mov       ecx,[esp+10h]

LAB_00488df4:
  00488df4  33db                  xor       ebx,ebx
  00488df6  bf01000000            mov       edi,1
  00488dfb  eb14                  jmp       short 00488E11h

LAB_00488dfd:
  00488dfd  33db                  xor       ebx,ebx
  00488dff  bf01000000            mov       edi,1
  00488e04  eb07                  jmp       short 00488E0Dh

LAB_00488e06:
  00488e06  bf01000000            mov       edi,1
  00488e0b  33db                  xor       ebx,ebx

LAB_00488e0d:
  00488e0d  8b4c2410              mov       ecx,[esp+10h]

LAB_00488e11:
  00488e11  33c0                  xor       eax,eax
  00488e13  8a4642                mov       al,[esi+42h]
  00488e16  83f80d                cmp       eax,0Dh
  00488e19  0f87ca040000          ja        near 004892E9h
  00488e1f  ff2485a0984800        jmp       dword [eax*4+4898A0h]
  00488e26  66395e12              cmp       [esi+12h],bx
  00488e2a  7518                  jne       short 00488E44h
  00488e2c  8b06                  mov       eax,[esi]
  00488e2e  66895e18              mov       [esi+18h],bx
  00488e32  0d00000001            or        eax,1000000h
  00488e37  66895e1a              mov       [esi+1Ah],bx
  00488e3b  8906                  mov       [esi],eax
  00488e3d  66897e12              mov       [esi+12h],di
  00488e41  895e5c                mov       [esi+5Ch],ebx

LAB_00488e44:
  00488e44  0fbf4610              movsx     eax,word [esi+10h]
  00488e48  39442414              cmp       [esp+14h],eax
  00488e4c  7d09                  jge       short 00488E57h
  00488e4e  c6464300              mov       byte [esi+43h],0
  00488e52  e992040000            jmp       004892E9h

LAB_00488e57:
  00488e57  8a4643                mov       al,[esi+43h]
  00488e5a  84c0                  test      al,al
  00488e5c  0f8484040000          je        near 004892E6h
  00488e62  fec8                  dec       al
  00488e64  884643                mov       [esi+43h],al
  00488e67  e97a040000            jmp       004892E6h
  00488e6c  66395e12              cmp       [esi+12h],bx
  00488e70  7515                  jne       short 00488E87h
  00488e72  8b06                  mov       eax,[esi]
  00488e74  66897e18              mov       [esi+18h],di
  00488e78  0d00000001            or        eax,1000000h
  00488e7d  66895e1a              mov       [esi+1Ah],bx
  00488e81  8906                  mov       [esi],eax
  00488e83  66897e12              mov       [esi+12h],di

LAB_00488e87:
  00488e87  8b442418              mov       eax,[esp+18h]
  00488e8b  8d90c8000000          lea       edx,[eax+0C8h]
  00488e91  81e2ff0f0000          and       edx,0FFFh
  00488e97  81fa90010000          cmp       edx,190h
  00488e9d  7f09                  jg        short 00488EA8h
  00488e9f  c6464300              mov       byte [esi+43h],0
  00488ea3  e93e040000            jmp       004892E6h

LAB_00488ea8:
  00488ea8  25ff0f0000            and       eax,0FFFh
  00488ead  3d00080000            cmp       eax,800h
  00488eb2  7d0f                  jge       short 00488EC3h
  00488eb4  8d8180000000          lea       eax,[ecx+80h]
  00488eba  66014632              add       [esi+32h],ax
  00488ebe  e923040000            jmp       004892E6h

LAB_00488ec3:
  00488ec3  b880ffffff            mov       eax,0FFFFFF80h
  00488ec8  2bc1                  sub       eax,ecx
  00488eca  66014632              add       [esi+32h],ax
  00488ece  e913040000            jmp       004892E6h
  00488ed3  66395e12              cmp       [esi+12h],bx
  00488ed7  7523                  jne       short 00488EFCh
  00488ed9  8b06                  mov       eax,[esi]
  00488edb  66897e18              mov       [esi+18h],di
  00488edf  0d00000001            or        eax,1000000h
  00488ee4  66895e1a              mov       [esi+1Ah],bx
  00488ee8  8906                  mov       [esi],eax
  00488eea  8d04c9                lea       eax,[ecx+ecx*8]
  00488eed  c1e004                shl       eax,4
  00488ef0  0500500000            add       eax,5000h
  00488ef5  66897e12              mov       [esi+12h],di
  00488ef9  89465c                mov       [esi+5Ch],eax

LAB_00488efc:
  00488efc  66397e12              cmp       [esi+12h],di
  00488f00  0f85e3030000          jne       near 004892E9h
  00488f06  0fbf5610              movsx     edx,word [esi+10h]
  00488f0a  39542414              cmp       [esp+14h],edx
  00488f0e  7d09                  jge       short 00488F19h
  00488f10  c6464300              mov       byte [esi+43h],0
  00488f14  e9cd030000            jmp       004892E6h

LAB_00488f19:
  00488f19  8a4643                mov       al,[esi+43h]
  00488f1c  84c0                  test      al,al
  00488f1e  7405                  je        short 00488F25h
  00488f20  fec8                  dec       al
  00488f22  884643                mov       [esi+43h],al

LAB_00488f25:
  00488f25  8b442418              mov       eax,[esp+18h]
  00488f29  8d9000010000          lea       edx,[eax+100h]
  00488f2f  81e2ff0f0000          and       edx,0FFFh
  00488f35  81fa00020000          cmp       edx,200h
  00488f3b  0f8ea8030000          jle       near 004892E9h
  00488f41  25ff0f0000            and       eax,0FFFh
  00488f46  3d00080000            cmp       eax,800h
  00488f4b  7d10                  jge       short 00488F5Dh
  00488f4d  8d840980000000        lea       eax,[ecx+ecx+80h]
  00488f54  66014632              add       [esi+32h],ax
  00488f58  e98c030000            jmp       004892E9h

LAB_00488f5d:
  00488f5d  03c9                  add       ecx,ecx
  00488f5f  b880ffffff            mov       eax,0FFFFFF80h
  00488f64  2bc1                  sub       eax,ecx
  00488f66  66014632              add       [esi+32h],ax
  00488f6a  e97a030000            jmp       004892E9h
  00488f6f  66397e12              cmp       [esi+12h],di
  00488f73  0f859d000000          jne       near 00489016h
  00488f79  8a4643                mov       al,[esi+43h]
  00488f7c  84c0                  test      al,al
  00488f7e  7405                  je        short 00488F85h
  00488f80  fec8                  dec       al
  00488f82  884643                mov       [esi+43h],al

LAB_00488f85:
  00488f85  8b7c2420              mov       edi,[esp+20h]
  00488f89  8d14c9                lea       edx,[ecx+ecx*8]
  00488f8c  c1e204                shl       edx,4
  00488f8f  8d8200500000          lea       eax,[edx+5000h]
  00488f95  0faff8                imul      edi,eax
  00488f98  89465c                mov       [esi+5Ch],eax
  00488f9b  0fbf4610              movsx     eax,word [esi+10h]
  00488f9f  50                    push      eax
  00488fa0  c1ff0c                sar       edi,0Ch
  00488fa3  e874ca0b00            call      00545A1Ch
  00488fa8  0fafc7                imul      eax,edi
  00488fab  8b4d00                mov       ecx,[ebp]
  00488fae  c1f80c                sar       eax,0Ch
  00488fb1  03c8                  add       ecx,eax
  00488fb3  894d00                mov       [ebp],ecx
  00488fb6  0fbf4e10              movsx     ecx,word [esi+10h]
  00488fba  51                    push      ecx
  00488fbb  e856ca0b00            call      00545A16h
  00488fc0  0fafc7                imul      eax,edi
  00488fc3  8b6e24                mov       ebp,[esi+24h]
  00488fc6  895e5c                mov       [esi+5Ch],ebx
  00488fc9  c1f80c                sar       eax,0Ch
  00488fcc  03e8                  add       ebp,eax
  00488fce  8b442418              mov       eax,[esp+18h]
  00488fd2  896e24                mov       [esi+24h],ebp
  00488fd5  8d9000010000          lea       edx,[eax+100h]
  00488fdb  81e2ff0f0000          and       edx,0FFFh
  00488fe1  81fa00020000          cmp       edx,200h
  00488fe7  7e28                  jle       short 00489011h
  00488fe9  25ff0f0000            and       eax,0FFFh
  00488fee  3d00080000            cmp       eax,800h
  00488ff3  7d0a                  jge       short 00488FFFh
  00488ff5  8b442410              mov       eax,[esp+10h]
  00488ff9  8d440032              lea       eax,[eax+eax+32h]
  00488ffd  eb0e                  jmp       short 0048900Dh

LAB_00488fff:
  00488fff  8b4c2410              mov       ecx,[esp+10h]
  00489003  b8ceffffff            mov       eax,0FFFFFFCEh
  00489008  8d1409                lea       edx,[ecx+ecx]
  0048900b  2bc2                  sub       eax,edx

LAB_0048900d:
  0048900d  66014632              add       [esi+32h],ax

LAB_00489011:
  00489011  bf01000000            mov       edi,1

LAB_00489016:
  00489016  66395e12              cmp       [esi+12h],bx
  0048901a  0f85c9020000          jne       near 004892E9h
  00489020  8b06                  mov       eax,[esi]
  00489022  66897e18              mov       [esi+18h],di
  00489026  0d00000001            or        eax,1000000h
  0048902b  66895e1a              mov       [esi+1Ah],bx
  0048902f  8906                  mov       [esi],eax
  00489031  66897e12              mov       [esi+12h],di
  00489035  e9af020000            jmp       004892E9h
  0048903a  66395e12              cmp       [esi+12h],bx
  0048903e  757c                  jne       short 004890BCh
  00489040  8b06                  mov       eax,[esi]
  00489042  a801                  test      al,1
  00489044  7406                  je        short 0048904Ch
  00489046  c6464300              mov       byte [esi+43h],0
  0048904a  eb70                  jmp       short 004890BCh

LAB_0048904c:
  0048904c  8b7c2420              mov       edi,[esp+20h]
  00489050  25fffffffe            and       eax,0FEFFFFFFh
  00489055  8906                  mov       [esi],eax
  00489057  8d8100010000          lea       eax,[ecx+100h]
  0048905d  c1e006                shl       eax,6
  00489060  0faff8                imul      edi,eax
  00489063  89465c                mov       [esi+5Ch],eax
  00489066  bd01000000            mov       ebp,1
  0048906b  0fbf4610              movsx     eax,word [esi+10h]
  0048906f  50                    push      eax
  00489070  66896e18              mov       [esi+18h],bp
  00489074  66895e1a              mov       [esi+1Ah],bx
  00489078  c1ff0c                sar       edi,0Ch
  0048907b  e89cc90b00            call      00545A1Ch
  00489080  0fafc7                imul      eax,edi
  00489083  8b4e50                mov       ecx,[esi+50h]
  00489086  c1f80c                sar       eax,0Ch
  00489089  03c8                  add       ecx,eax
  0048908b  894e50                mov       [esi+50h],ecx
  0048908e  0fbf4e10              movsx     ecx,word [esi+10h]
  00489092  51                    push      ecx
  00489093  e87ec90b00            call      00545A16h
  00489098  0fafc7                imul      eax,edi
  0048909b  8b5658                mov       edx,[esi+58h]
  0048909e  8b4e54                mov       ecx,[esi+54h]
  004890a1  c1f80c                sar       eax,0Ch
  004890a4  03d0                  add       edx,eax
  004890a6  8b06                  mov       eax,[esi]
  004890a8  81c100e00100          add       ecx,1E000h
  004890ae  0bc5                  or        eax,ebp
  004890b0  895658                mov       [esi+58h],edx
  004890b3  894e54                mov       [esi+54h],ecx
  004890b6  8906                  mov       [esi],eax
  004890b8  66896e12              mov       [esi+12h],bp

LAB_004890bc:
  004890bc  f60601                test      byte [esi],1
  004890bf  0f8524020000          jne       near 004892E9h
  004890c5  c6464300              mov       byte [esi+43h],0
  004890c9  e91b020000            jmp       004892E9h
  004890ce  668b5612              mov       dx,[esi+12h]
  004890d2  663bd7                cmp       dx,di
  004890d5  751c                  jne       short 004890F3h
  004890d7  0fbf4610              movsx     eax,word [esi+10h]
  004890db  39442414              cmp       [esp+14h],eax
  004890df  7d06                  jge       short 004890E7h
  004890e1  c6464300              mov       byte [esi+43h],0
  004890e5  eb0c                  jmp       short 004890F3h

LAB_004890e7:
  004890e7  8a4643                mov       al,[esi+43h]
  004890ea  84c0                  test      al,al
  004890ec  7405                  je        short 004890F3h
  004890ee  fec8                  dec       al
  004890f0  884643                mov       [esi+43h],al

LAB_004890f3:
  004890f3  663bd3                cmp       dx,bx
  004890f6  0f85ed010000          jne       near 004892E9h
  004890fc  66895e1a              mov       [esi+1Ah],bx
  00489100  8b1e                  mov       ebx,[esi]
  00489102  8d0cc9                lea       ecx,[ecx+ecx*8]
  00489105  81cb00000001          or        ebx,1000000h
  0048910b  c1e104                shl       ecx,4
  0048910e  81c100500000          add       ecx,5000h
  00489114  66897e18              mov       [esi+18h],di
  00489118  891e                  mov       [esi],ebx
  0048911a  66897e12              mov       [esi+12h],di
  0048911e  894e5c                mov       [esi+5Ch],ecx
  00489121  e9c3010000            jmp       004892E9h
  00489126  66397e12              cmp       [esi+12h],di
  0048912a  7537                  jne       short 00489163h
  0048912c  66837e1a08            cmp       word [esi+1Ah],8
  00489131  7530                  jne       short 00489163h
  00489133  66397e16              cmp       [esi+16h],di
  00489137  7512                  jne       short 0048914Bh
  00489139  6aff                  push      0FFFFFFFFh
  0048913b  53                    push      ebx
  0048913c  6a36                  push      36h
  0048913e  56                    push      esi
  0048913f  e88c18ffff            call      0047A9D0h
  00489144  8b4c2420              mov       ecx,[esp+20h]
  00489148  83c410                add       esp,10h

LAB_0048914b:
  0048914b  66395e16              cmp       [esi+16h],bx
  0048914f  7512                  jne       short 00489163h
  00489151  6aff                  push      0FFFFFFFFh
  00489153  53                    push      ebx
  00489154  6a4b                  push      4Bh
  00489156  56                    push      esi
  00489157  e87418ffff            call      0047A9D0h
  0048915c  8b4c2420              mov       ecx,[esp+20h]
  00489160  83c410                add       esp,10h

LAB_00489163:
  00489163  8b06                  mov       eax,[esi]
  00489165  a900000002            test      eax,2000000h
  0048916a  7404                  je        short 00489170h
  0048916c  c6464300              mov       byte [esi+43h],0

LAB_00489170:
  00489170  66395e12              cmp       [esi+12h],bx
  00489174  0f856f010000          jne       near 004892E9h
  0048917a  25fffffffe            and       eax,0FEFFFFFFh
  0048917f  83f91e                cmp       ecx,1Eh
  00489182  66897e12              mov       [esi+12h],di
  00489186  66c746180500          mov       word [esi+18h],5
  0048918c  66895e1a              mov       [esi+1Ah],bx
  00489190  8906                  mov       [esi],eax
  00489192  0f8d51010000          jge       near 004892E9h
  00489198  e949010000            jmp       004892E6h
  0048919d  668b4e12              mov       cx,[esi+12h]
  004891a1  663bcf                cmp       cx,di
  004891a4  7511                  jne       short 004891B7h
  004891a6  8b06                  mov       eax,[esi]
  004891a8  a900000002            test      eax,2000000h
  004891ad  7408                  je        short 004891B7h
  004891af  24fd                  and       al,0FDh
  004891b1  c6464300              mov       byte [esi+43h],0
  004891b5  8906                  mov       [esi],eax

LAB_004891b7:
  004891b7  663bcb                cmp       cx,bx
  004891ba  0f8529010000          jne       near 004892E9h
  004891c0  8b06                  mov       eax,[esi]
  004891c2  66897e12              mov       [esi+12h],di
  004891c6  25fffffffe            and       eax,0FEFFFFFFh
  004891cb  8906                  mov       [esi],eax
  004891cd  e917010000            jmp       004892E9h
  004891d2  668b4e12              mov       cx,[esi+12h]
  004891d6  6683f902              cmp       cx,2
  004891da  7511                  jne       short 004891EDh
  004891dc  8b06                  mov       eax,[esi]
  004891de  a900000002            test      eax,2000000h
  004891e3  7408                  je        short 004891EDh
  004891e5  24fd                  and       al,0FDh
  004891e7  c6464300              mov       byte [esi+43h],0
  004891eb  8906                  mov       [esi],eax

LAB_004891ed:
  004891ed  663bcf                cmp       cx,di
  004891f0  7522                  jne       short 00489214h
  004891f2  8b06                  mov       eax,[esi]
  004891f4  a900000002            test      eax,2000000h
  004891f9  7419                  je        short 00489214h
  004891fb  25fffffffe            and       eax,0FEFFFFFFh
  00489200  66c746120200          mov       word [esi+12h],2
  00489206  8906                  mov       [esi],eax
  00489208  66c746180500          mov       word [esi+18h],5
  0048920e  66c7461a0700          mov       word [esi+1Ah],7

LAB_00489214:
  00489214  66395e12              cmp       [esi+12h],bx
  00489218  0f85cb000000          jne       near 004892E9h
  0048921e  8b16                  mov       edx,[esi]
  00489220  66897e12              mov       [esi+12h],di
  00489224  81e2fffffffe          and       edx,0FEFFFFFFh
  0048922a  66c746180300          mov       word [esi+18h],3
  00489230  83ca02                or        edx,2
  00489233  66895e1a              mov       [esi+1Ah],bx
  00489237  8916                  mov       [esi],edx
  00489239  e9a8000000            jmp       004892E6h
  0048923e  668b5612              mov       dx,[esi+12h]
  00489242  6683fa02              cmp       dx,2
  00489246  7521                  jne       short 00489269h
  00489248  8b465c                mov       eax,[esi+5Ch]
  0048924b  8be8                  mov       ebp,eax
  0048924d  c1fd05                sar       ebp,5
  00489250  2bc5                  sub       eax,ebp
  00489252  3d00a00000            cmp       eax,0A000h
  00489257  89465c                mov       [esi+5Ch],eax
  0048925a  7d0d                  jge       short 00489269h
  0048925c  8b06                  mov       eax,[esi]
  0048925e  c6464300              mov       byte [esi+43h],0
  00489262  24fd                  and       al,0FDh
  00489264  895e5c                mov       [esi+5Ch],ebx
  00489267  8906                  mov       [esi],eax

LAB_00489269:
  00489269  663bd7                cmp       dx,di
  0048926c  755b                  jne       short 004892C9h
  0048926e  8b442418              mov       eax,[esp+18h]
  00489272  8d9000010000          lea       edx,[eax+100h]
  00489278  81e2ff0f0000          and       edx,0FFFh
  0048927e  81fa00020000          cmp       edx,200h
  00489284  7e20                  jle       short 004892A6h
  00489286  25ff0f0000            and       eax,0FFFh
  0048928b  3d00080000            cmp       eax,800h
  00489290  7d06                  jge       short 00489298h
  00489292  8d4c0932              lea       ecx,[ecx+ecx+32h]
  00489296  eb0a                  jmp       short 004892A2h

LAB_00489298:
  00489298  8d0409                lea       eax,[ecx+ecx]
  0048929b  b9ceffffff            mov       ecx,0FFFFFFCEh
  004892a0  2bc8                  sub       ecx,eax

LAB_004892a2:
  004892a2  66014e32              add       [esi+32h],cx

LAB_004892a6:
  004892a6  66837e1a06            cmp       word [esi+1Ah],6
  004892ab  7c1c                  jl        short 004892C9h
  004892ad  0fbf4616              movsx     eax,word [esi+16h]
  004892b1  83c004                add       eax,4
  004892b4  66c746120200          mov       word [esi+12h],2
  004892ba  8d0c80                lea       ecx,[eax+eax*4]
  004892bd  8b06                  mov       eax,[esi]
  004892bf  c1e10c                shl       ecx,0Ch
  004892c2  0c02                  or        al,2
  004892c4  894e5c                mov       [esi+5Ch],ecx
  004892c7  8906                  mov       [esi],eax

LAB_004892c9:
  004892c9  66395e12              cmp       [esi+12h],bx
  004892cd  751a                  jne       short 004892E9h
  004892cf  8b06                  mov       eax,[esi]
  004892d1  66897e12              mov       [esi+12h],di
  004892d5  25fffffffe            and       eax,0FEFFFFFFh
  004892da  66c746180400          mov       word [esi+18h],4
  004892e0  66895e1a              mov       [esi+1Ah],bx
  004892e4  8906                  mov       [esi],eax

LAB_004892e6:
  004892e6  895e5c                mov       [esi+5Ch],ebx

LAB_004892e9:
  004892e9  0fbf5632              movsx     edx,word [esi+32h]
  004892ed  52                    push      edx
  004892ee  e829c70b00            call      00545A1Ch
  004892f3  89442478              mov       [esp+78h],eax
  004892f7  0fbf4632              movsx     eax,word [esi+32h]
  004892fb  50                    push      eax
  004892fc  e815c70b00            call      00545A16h
  00489301  8b5650                mov       edx,[esi+50h]
  00489304  89842480000000        mov       [esp+80h],eax
  0048930b  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  00489312  8bc2                  mov       eax,edx
  00489314  8bfa                  mov       edi,edx
  00489316  c1f803                sar       eax,3
  00489319  8b4c2420              mov       ecx,[esp+20h]
  0048931d  2bf8                  sub       edi,eax
  0048931f  c1fa08                sar       edx,8
  00489322  0faf4e5c              imul      ecx,[esi+5Ch]
  00489326  2bfa                  sub       edi,edx
  00489328  8b5654                mov       edx,[esi+54h]
  0048932b  8bc5                  mov       eax,ebp
  0048932d  8bda                  mov       ebx,edx
  0048932f  c1f808                sar       eax,8
  00489332  c1fb08                sar       ebx,8
  00489335  2bc3                  sub       eax,ebx
  00489337  897e50                mov       [esi+50h],edi
  0048933a  2bc5                  sub       eax,ebp
  0048933c  56                    push      esi
  0048933d  03c2                  add       eax,edx
  0048933f  8b5658                mov       edx,[esi+58h]
  00489342  8bda                  mov       ebx,edx
  00489344  8bea                  mov       ebp,edx
  00489346  c1fb08                sar       ebx,8
  00489349  2beb                  sub       ebp,ebx
  0048934b  8b5e1c                mov       ebx,[esi+1Ch]
  0048934e  c1fa03                sar       edx,3
  00489351  c1f90c                sar       ecx,0Ch
  00489354  2bea                  sub       ebp,edx
  00489356  8bd1                  mov       edx,ecx
  00489358  0faf54247c            imul      edx,[esp+7Ch]
  0048935d  0faf8c2484000000      imul      ecx,[esp+84h]
  00489365  c1fa0c                sar       edx,0Ch
  00489368  c1f90c                sar       ecx,0Ch
  0048936b  896e58                mov       [esi+58h],ebp
  0048936e  03d7                  add       edx,edi
  00489370  8b7e20                mov       edi,[esi+20h]
  00489373  03cd                  add       ecx,ebp
  00489375  8b6e24                mov       ebp,[esi+24h]
  00489378  03da                  add       ebx,edx
  0048937a  03f8                  add       edi,eax
  0048937c  03e9                  add       ebp,ecx
  0048937e  894654                mov       [esi+54h],eax
  00489381  895e1c                mov       [esi+1Ch],ebx
  00489384  897e20                mov       [esi+20h],edi
  00489387  896e24                mov       [esi+24h],ebp
  0048938a  e8b1a50a00            call      00533940h
  0048938f  8b9690000000          mov       edx,[esi+90h]
  00489395  8b4660                mov       eax,[esi+60h]
  00489398  8b4e20                mov       ecx,[esi+20h]
  0048939b  83c404                add       esp,4
  0048939e  80e6fc                and       dh,0FCh
  004893a1  3bc8                  cmp       ecx,eax
  004893a3  899690000000          mov       [esi+90h],edx
  004893a9  7f2a                  jg        short 004893D5h
  004893ab  f6c680                test      dh,80h
  004893ae  7525                  jne       short 004893D5h
  004893b0  8b0e                  mov       ecx,[esi]
  004893b2  894620                mov       [esi+20h],eax
  004893b5  f6c101                test      cl,1
  004893b8  740f                  je        short 004893C9h
  004893ba  83e1fe                and       ecx,0FFFFFFFEh
  004893bd  33db                  xor       ebx,ebx
  004893bf  890e                  mov       [esi],ecx
  004893c1  895e54                mov       [esi+54h],ebx
  004893c4  e925010000            jmp       004894EEh

LAB_004893c9:
  004893c9  83e1fe                and       ecx,0FFFFFFFEh
  004893cc  33db                  xor       ebx,ebx
  004893ce  890e                  mov       [esi],ecx
  004893d0  895e54                mov       [esi+54h],ebx
  004893d3  eb02                  jmp       short 004893D7h

LAB_004893d5:
  004893d5  33db                  xor       ebx,ebx

LAB_004893d7:
  004893d7  8b0e                  mov       ecx,[esi]
  004893d9  f6c101                test      cl,1
  004893dc  750a                  jne       short 004893E8h
  004893de  394620                cmp       [esi+20h],eax
  004893e1  7e05                  jle       short 004893E8h
  004893e3  83c901                or        ecx,1
  004893e6  890e                  mov       [esi],ecx

LAB_004893e8:
  004893e8  f7c282ad0020          test      edx,2000AD82h
  004893ee  0f84fa000000          je        near 004894EEh
  004893f4  f60601                test      byte [esi],1
  004893f7  0f85c9000000          jne       near 004894C6h
  004893fd  8a4e42                mov       cl,[esi+42h]
  00489400  b007                  mov       al,7
  00489402  3ac8                  cmp       cl,al
  00489404  7420                  je        short 00489426h
  00489406  884642                mov       [esi+42h],al
  00489409  e832dd0b00            call      00547140h
  0048940e  99                    cdq
  0048940f  b928000000            mov       ecx,28h
  00489414  66895e12              mov       [esi+12h],bx
  00489418  f7f9                  idiv      ecx
  0048941a  66c746105000          mov       word [esi+10h],50h
  00489420  80c214                add       dl,14h
  00489423  885643                mov       [esi+43h],dl

LAB_00489426:
  00489426  8b16                  mov       edx,[esi]
  00489428  8b44242c              mov       eax,[esp+2Ch]
  0048942c  8b4c2430              mov       ecx,[esp+30h]
  00489430  83e2fd                and       edx,0FFFFFFFDh
  00489433  8916                  mov       [esi],edx
  00489435  8b542428              mov       edx,[esp+28h]
  00489439  894620                mov       [esi+20h],eax
  0048943c  8b44246c              mov       eax,[esp+6Ch]
  00489440  89561c                mov       [esi+1Ch],edx
  00489443  8b542464              mov       edx,[esp+64h]
  00489447  25ffff0000            and       eax,0FFFFh
  0048944c  894e24                mov       [esi+24h],ecx
  0048944f  8b4c244c              mov       ecx,[esp+4Ch]
  00489453  89567c                mov       [esi+7Ch],edx
  00489456  8b542458              mov       edx,[esp+58h]
  0048945a  898690000000          mov       [esi+90h],eax
  00489460  8b442460              mov       eax,[esp+60h]
  00489464  898e84000000          mov       [esi+84h],ecx
  0048946a  8b4c2450              mov       ecx,[esp+50h]
  0048946e  898680000000          mov       [esi+80h],eax
  00489474  8b442468              mov       eax,[esp+68h]
  00489478  899688000000          mov       [esi+88h],edx
  0048947e  8b542454              mov       edx,[esp+54h]
  00489482  6882ad0020            push      2000AD82h
  00489487  56                    push      esi
  00489488  89868c000000          mov       [esi+8Ch],eax
  0048948e  894e64                mov       [esi+64h],ecx
  00489491  895660                mov       [esi+60h],edx
  00489494  e8c7c90400            call      004D5E60h
  00489499  83c408                add       esp,8
  0048949c  663bc3                cmp       ax,bx
  0048949f  7515                  jne       short 004894B6h
  004894a1  e89adc0b00            call      00547140h
  004894a6  99                    cdq
  004894a7  33c2                  xor       eax,edx
  004894a9  2bc2                  sub       eax,edx
  004894ab  83e001                and       eax,1
  004894ae  33c2                  xor       eax,edx
  004894b0  2bc2                  sub       eax,edx
  004894b2  8d4400ff              lea       eax,[eax+eax-1]

LAB_004894b6:
  004894b6  8b4c2410              mov       ecx,[esp+10h]
  004894ba  83c140                add       ecx,40h
  004894bd  0fafc8                imul      ecx,eax
  004894c0  66014e32              add       [esi+32h],cx
  004894c4  eb28                  jmp       short 004894EEh

LAB_004894c6:
  004894c6  8b542428              mov       edx,[esp+28h]
  004894ca  8b442430              mov       eax,[esp+30h]
  004894ce  895e5c                mov       [esi+5Ch],ebx
  004894d1  895e50                mov       [esi+50h],ebx
  004894d4  895e58                mov       [esi+58h],ebx
  004894d7  89561c                mov       [esi+1Ch],edx
  004894da  894624                mov       [esi+24h],eax
  004894dd  885e42                mov       [esi+42h],bl
  004894e0  c6464328              mov       byte [esi+43h],28h
  004894e4  66895e12              mov       [esi+12h],bx
  004894e8  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004894ee:
  004894ee  6aff                  push      0FFFFFFFFh
  004894f0  6aff                  push      0FFFFFFFFh
  004894f2  6aff                  push      0FFFFFFFFh
  004894f4  56                    push      esi
  004894f5  e816a30300            call      004C3810h
  004894fa  8b442430              mov       eax,[esp+30h]
  004894fe  83c410                add       esp,10h
  00489501  3bc3                  cmp       eax,ebx
  00489503  7517                  jne       short 0048951Ch
  00489505  668b8c248e000000      mov       cx,[esp+8Eh]
  0048950d  66894e32              mov       [esi+32h],cx
  00489511  5f                    pop       edi
  00489512  5e                    pop       esi
  00489513  5d                    pop       ebp
  00489514  5b                    pop       ebx
  00489515  81c490000000          add       esp,90h
  0048951b  c3                    ret

LAB_0048951c:
  0048951c  385e43                cmp       [esi+43h],bl
  0048951f  0f8544030000          jne       near 00489869h
  00489525  8b466c                mov       eax,[esi+6Ch]
  00489528  8b561c                mov       edx,[esi+1Ch]
  0048952b  8b781c                mov       edi,[eax+1Ch]
  0048952e  8b6824                mov       ebp,[eax+24h]
  00489531  8b4624                mov       eax,[esi+24h]
  00489534  2bfa                  sub       edi,edx
  00489536  2be8                  sub       ebp,eax
  00489538  c1ff0c                sar       edi,0Ch
  0048953b  c1fd0c                sar       ebp,0Ch
  0048953e  8bd7                  mov       edx,edi
  00489540  8bc5                  mov       eax,ebp
  00489542  0fafd7                imul      edx,edi
  00489545  0fafc5                imul      eax,ebp
  00489548  03d0                  add       edx,eax
  0048954a  52                    push      edx
  0048954b  e8c0c40b00            call      00545A10h
  00489550  55                    push      ebp
  00489551  57                    push      edi
  00489552  8944241c              mov       [esp+1Ch],eax
  00489556  e8c7c40b00            call      00545A22h
  0048955b  668b4e32              mov       cx,[esi+32h]
  0048955f  66895e12              mov       [esi+12h],bx
  00489563  2bc1                  sub       eax,ecx
  00489565  25ff0f0000            and       eax,0FFFh
  0048956a  89442418              mov       [esp+18h],eax
  0048956e  8b442414              mov       eax,[esp+14h]
  00489572  3dfa000000            cmp       eax,0FAh
  00489577  0f8dc5000000          jge       near 00489642h
  0048957d  e8bedb0b00            call      00547140h
  00489582  99                    cdq
  00489583  b964000000            mov       ecx,64h
  00489588  f7f9                  idiv      ecx
  0048958a  8bea                  mov       ebp,edx
  0048958c  83fd32                cmp       ebp,32h
  0048958f  7d77                  jge       short 00489608h
  00489591  8b542418              mov       edx,[esp+18h]
  00489595  8b7c241c              mov       edi,[esp+1Ch]
  00489599  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004895a0  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004895a7  25ff0f0000            and       eax,0FFFh
  004895ac  3bc1                  cmp       eax,ecx
  004895ae  7f4e                  jg        short 004895FEh
  004895b0  e88bdb0b00            call      00547140h
  004895b5  99                    cdq
  004895b6  8d4c3f01              lea       ecx,[edi+edi+1]
  004895ba  f7f9                  idiv      ecx
  004895bc  8b442414              mov       eax,[esp+14h]
  004895c0  83c264                add       edx,64h
  004895c3  3bc2                  cmp       eax,edx
  004895c5  7d37                  jge       short 004895FEh
  004895c7  83fd1e                cmp       ebp,1Eh
  004895ca  7d24                  jge       short 004895F0h
  004895cc  8b16                  mov       edx,[esi]
  004895ce  c646420b              mov       byte [esi+42h],0Bh
  004895d2  81e2fffffffe          and       edx,0FEFFFFFFh
  004895d8  c6464301              mov       byte [esi+43h],1
  004895dc  83ca02                or        edx,2
  004895df  895e5c                mov       [esi+5Ch],ebx
  004895e2  8916                  mov       [esi],edx
  004895e4  66c746180300          mov       word [esi+18h],3
  004895ea  66895e1a              mov       [esi+1Ah],bx
  004895ee  eb56                  jmp       short 00489646h

LAB_004895f0:
  004895f0  c646420c              mov       byte [esi+42h],0Ch
  004895f4  c6464301              mov       byte [esi+43h],1
  004895f8  66895e12              mov       [esi+12h],bx
  004895fc  eb48                  jmp       short 00489646h

LAB_004895fe:
  004895fe  c6464201              mov       byte [esi+42h],1
  00489602  c6464301              mov       byte [esi+43h],1
  00489606  eb3e                  jmp       short 00489646h

LAB_00489608:
  00489608  83fd3c                cmp       ebp,3Ch
  0048960b  0f8c63020000          jl        near 00489874h
  00489611  83fd46                cmp       ebp,46h
  00489614  0f8d5a020000          jge       near 00489874h
  0048961a  8b4c2410              mov       ecx,[esp+10h]
  0048961e  8b442414              mov       eax,[esp+14h]
  00489622  83c164                add       ecx,64h
  00489625  3bc1                  cmp       eax,ecx
  00489627  0f8e47020000          jle       near 00489874h
  0048962d  c6464206              mov       byte [esi+42h],6
  00489631  c6464301              mov       byte [esi+43h],1
  00489635  e806db0b00            call      00547140h
  0048963a  66894610              mov       [esi+10h],ax
  0048963e  66895e12              mov       [esi+12h],bx

LAB_00489642:
  00489642  8b7c241c              mov       edi,[esp+1Ch]

LAB_00489646:
  00489646  8b442414              mov       eax,[esp+14h]
  0048964a  3d58020000            cmp       eax,258h
  0048964f  0f8dd8000000          jge       near 0048972Dh
  00489655  3dfa000000            cmp       eax,0FAh
  0048965a  0f8ccd000000          jl        near 0048972Dh
  00489660  e8dbda0b00            call      00547140h
  00489665  99                    cdq
  00489666  b964000000            mov       ecx,64h
  0048966b  f7f9                  idiv      ecx
  0048966d  83fa14                cmp       edx,14h
  00489670  7d2e                  jge       short 004896A0h
  00489672  8b542418              mov       edx,[esp+18h]
  00489676  81c200040000          add       edx,400h
  0048967c  81e2ff0f0000          and       edx,0FFFh
  00489682  81fa00080000          cmp       edx,800h
  00489688  7c0d                  jl        short 00489697h
  0048968a  c6464201              mov       byte [esi+42h],1
  0048968e  c6464301              mov       byte [esi+43h],1
  00489692  e996000000            jmp       0048972Dh

LAB_00489697:
  00489697  c646420d              mov       byte [esi+42h],0Dh
  0048969b  e985000000            jmp       00489725h

LAB_004896a0:
  004896a0  83fa28                cmp       edx,28h
  004896a3  7d25                  jge       short 004896CAh
  004896a5  c6464202              mov       byte [esi+42h],2
  004896a9  e892da0b00            call      00547140h
  004896ae  99                    cdq
  004896af  b90a000000            mov       ecx,0Ah
  004896b4  66c746105000          mov       word [esi+10h],50h
  004896ba  f7f9                  idiv      ecx
  004896bc  8bc7                  mov       eax,edi
  004896be  d1f8                  sar       eax,1
  004896c0  02d0                  add       dl,al
  004896c2  80c214                add       dl,14h
  004896c5  885643                mov       [esi+43h],dl
  004896c8  eb63                  jmp       short 0048972Dh

LAB_004896ca:
  004896ca  83fa3c                cmp       edx,3Ch
  004896cd  7d13                  jge       short 004896E2h
  004896cf  c6464206              mov       byte [esi+42h],6
  004896d3  c6464301              mov       byte [esi+43h],1
  004896d7  e864da0b00            call      00547140h
  004896dc  66894610              mov       [esi+10h],ax
  004896e0  eb47                  jmp       short 00489729h

LAB_004896e2:
  004896e2  83fa50                cmp       edx,50h
  004896e5  7d3a                  jge       short 00489721h
  004896e7  c6464203              mov       byte [esi+42h],3
  004896eb  66895e12              mov       [esi+12h],bx
  004896ef  e84cda0b00            call      00547140h
  004896f4  99                    cdq
  004896f5  b90a000000            mov       ecx,0Ah
  004896fa  f7f9                  idiv      ecx
  004896fc  8bc7                  mov       eax,edi
  004896fe  d1f8                  sar       eax,1
  00489700  02d0                  add       dl,al
  00489702  80c214                add       dl,14h
  00489705  885643                mov       [esi+43h],dl
  00489708  e833da0b00            call      00547140h
  0048970d  99                    cdq
  0048970e  33c2                  xor       eax,edx
  00489710  2bc2                  sub       eax,edx
  00489712  25ff0f0000            and       eax,0FFFh
  00489717  33c2                  xor       eax,edx
  00489719  2bc2                  sub       eax,edx
  0048971b  66894610              mov       [esi+10h],ax
  0048971f  eb0c                  jmp       short 0048972Dh

LAB_00489721:
  00489721  c646420a              mov       byte [esi+42h],0Ah

LAB_00489725:
  00489725  c6464301              mov       byte [esi+43h],1

LAB_00489729:
  00489729  66895e12              mov       [esi+12h],bx

LAB_0048972d:
  0048972d  817c241458020000      cmp       dword [esp+14h],258h
  00489735  0f8c2e010000          jl        near 00489869h
  0048973b  e800da0b00            call      00547140h
  00489740  bd64000000            mov       ebp,64h
  00489745  99                    cdq
  00489746  8bcd                  mov       ecx,ebp
  00489748  f7f9                  idiv      ecx
  0048974a  83fa1e                cmp       edx,1Eh
  0048974d  7d45                  jge       short 00489794h
  0048974f  8b442410              mov       eax,[esp+10h]
  00489753  8d4c000a              lea       ecx,[eax+eax+0Ah]
  00489757  3bd1                  cmp       edx,ecx
  00489759  0f8ccd000000          jl        near 0048982Ch
  0048975f  885e42                mov       [esi+42h],bl
  00489762  e8d9d90b00            call      00547140h
  00489767  99                    cdq
  00489768  b90a000000            mov       ecx,0Ah
  0048976d  66895e12              mov       [esi+12h],bx
  00489771  f7f9                  idiv      ecx
  00489773  8b4c2410              mov       ecx,[esp+10h]
  00489777  8bc7                  mov       eax,edi
  00489779  c1f802                sar       eax,2
  0048977c  02d0                  add       dl,al
  0048977e  885643                mov       [esi+43h],dl
  00489781  8d540950              lea       edx,[ecx+ecx+50h]
  00489785  66895610              mov       [esi+10h],dx
  00489789  5f                    pop       edi
  0048978a  5e                    pop       esi
  0048978b  5d                    pop       ebp
  0048978c  5b                    pop       ebx
  0048978d  81c490000000          add       esp,90h
  00489793  c3                    ret

LAB_00489794:
  00489794  83fa28                cmp       edx,28h
  00489797  7d43                  jge       short 004897DCh
  00489799  c6464203              mov       byte [esi+42h],3
  0048979d  66895e12              mov       [esi+12h],bx
  004897a1  e89ad90b00            call      00547140h
  004897a6  99                    cdq
  004897a7  b90a000000            mov       ecx,0Ah
  004897ac  f7f9                  idiv      ecx
  004897ae  8bc7                  mov       eax,edi
  004897b0  d1f8                  sar       eax,1
  004897b2  02d0                  add       dl,al
  004897b4  80c214                add       dl,14h
  004897b7  885643                mov       [esi+43h],dl
  004897ba  e881d90b00            call      00547140h
  004897bf  99                    cdq
  004897c0  33c2                  xor       eax,edx
  004897c2  2bc2                  sub       eax,edx
  004897c4  25ff0f0000            and       eax,0FFFh
  004897c9  33c2                  xor       eax,edx
  004897cb  2bc2                  sub       eax,edx
  004897cd  66894610              mov       [esi+10h],ax
  004897d1  5f                    pop       edi
  004897d2  5e                    pop       esi
  004897d3  5d                    pop       ebp
  004897d4  5b                    pop       ebx
  004897d5  81c490000000          add       esp,90h
  004897db  c3                    ret

LAB_004897dc:
  004897dc  83fa3c                cmp       edx,3Ch
  004897df  7d13                  jge       short 004897F4h
  004897e1  c6464201              mov       byte [esi+42h],1
  004897e5  c6464301              mov       byte [esi+43h],1
  004897e9  5f                    pop       edi
  004897ea  5e                    pop       esi
  004897eb  5d                    pop       ebp
  004897ec  5b                    pop       ebx
  004897ed  81c490000000          add       esp,90h
  004897f3  c3                    ret

LAB_004897f4:
  004897f4  83fa50                cmp       edx,50h
  004897f7  7d5f                  jge       short 00489858h
  004897f9  8b4c2418              mov       ecx,[esp+18h]
  004897fd  8d04fd00020000        lea       eax,[edi*8+200h]
  00489804  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  0048980b  81e2ff0f0000          and       edx,0FFFh
  00489811  3bd0                  cmp       edx,eax
  00489813  7f17                  jg        short 0048982Ch
  00489815  c646420a              mov       byte [esi+42h],0Ah
  00489819  c6464301              mov       byte [esi+43h],1
  0048981d  66895e12              mov       [esi+12h],bx
  00489821  5f                    pop       edi
  00489822  5e                    pop       esi
  00489823  5d                    pop       ebp
  00489824  5b                    pop       ebx
  00489825  81c490000000          add       esp,90h
  0048982b  c3                    ret

LAB_0048982c:
  0048982c  c6464202              mov       byte [esi+42h],2
  00489830  e80bd90b00            call      00547140h
  00489835  99                    cdq
  00489836  b90a000000            mov       ecx,0Ah
  0048983b  66896e10              mov       [esi+10h],bp
  0048983f  f7f9                  idiv      ecx
  00489841  8bc7                  mov       eax,edi
  00489843  d1f8                  sar       eax,1
  00489845  02d0                  add       dl,al
  00489847  80c214                add       dl,14h
  0048984a  885643                mov       [esi+43h],dl
  0048984d  5f                    pop       edi
  0048984e  5e                    pop       esi
  0048984f  5d                    pop       ebp
  00489850  5b                    pop       ebx
  00489851  81c490000000          add       esp,90h
  00489857  c3                    ret

LAB_00489858:
  00489858  c6464206              mov       byte [esi+42h],6
  0048985c  c6464301              mov       byte [esi+43h],1
  00489860  e8dbd80b00            call      00547140h
  00489865  66894610              mov       [esi+10h],ax

LAB_00489869:
  00489869  5f                    pop       edi
  0048986a  5e                    pop       esi
  0048986b  5d                    pop       ebp
  0048986c  5b                    pop       ebx
  0048986d  81c490000000          add       esp,90h
  00489873  c3                    ret

LAB_00489874:
  00489874  c6464202              mov       byte [esi+42h],2
  00489878  e8c3d80b00            call      00547140h
  0048987d  99                    cdq
  0048987e  b90a000000            mov       ecx,0Ah
  00489883  8b7c241c              mov       edi,[esp+1Ch]
  00489887  f7f9                  idiv      ecx
  00489889  8bc7                  mov       eax,edi
  0048988b  66c746105000          mov       word [esi+10h],50h
  00489891  d1f8                  sar       eax,1
  00489893  02d0                  add       dl,al
  00489895  80c214                add       dl,14h
  00489898  885643                mov       [esi+43h],dl
  0048989b  e9a6fdffff            jmp       00489646h
  004898a0  268e4800              (bad)
  (octet invalide — la plage demandee deborde probablement du code)
