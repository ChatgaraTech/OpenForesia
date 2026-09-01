C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a3c30  section .text  offset fichier 0xa3030  1583 octets

  004a3c30  83ec2c                sub       esp,2Ch
  004a3c33  53                    push      ebx
  004a3c34  55                    push      ebp
  004a3c35  56                    push      esi
  004a3c36  8b74243c              mov       esi,[esp+3Ch]
  004a3c3a  8d442424              lea       eax,[esp+24h]
  004a3c3e  57                    push      edi
  004a3c3f  8d4c242c              lea       ecx,[esp+2Ch]
  004a3c43  50                    push      eax
  004a3c44  8d542428              lea       edx,[esp+28h]
  004a3c48  51                    push      ecx
  004a3c49  33ed                  xor       ebp,ebp
  004a3c4b  52                    push      edx
  004a3c4c  56                    push      esi
  004a3c4d  66896c2440            mov       [esp+40h],bp
  004a3c52  66c74424421e00        mov       word [esp+42h],1Eh
  004a3c59  66c74424443200        mov       word [esp+44h],32h
  004a3c60  66c74424466400        mov       word [esp+46h],64h
  004a3c67  66896c2448            mov       [esp+48h],bp
  004a3c6c  66c744244a5000        mov       word [esp+4Ah],50h
  004a3c73  e8084d0900            call      00538980h
  004a3c78  83c410                add       esp,10h
  004a3c7b  8d442420              lea       eax,[esp+20h]
  004a3c7f  8d4c241c              lea       ecx,[esp+1Ch]
  004a3c83  8d542440              lea       edx,[esp+40h]
  004a3c87  50                    push      eax
  004a3c88  51                    push      ecx
  004a3c89  8d44241c              lea       eax,[esp+1Ch]
  004a3c8d  52                    push      edx
  004a3c8e  8d4c241c              lea       ecx,[esp+1Ch]
  004a3c92  50                    push      eax
  004a3c93  51                    push      ecx
  004a3c94  56                    push      esi
  004a3c95  e886220900            call      00535F20h
  004a3c9a  8b4c2428              mov       ecx,[esp+28h]
  004a3c9e  8b442434              mov       eax,[esp+34h]
  004a3ca2  bf40000000            mov       edi,40h
  004a3ca7  83c418                add       esp,18h
  004a3caa  2bf9                  sub       edi,ecx
  004a3cac  3bc5                  cmp       eax,ebp
  004a3cae  897c2418              mov       [esp+18h],edi
  004a3cb2  0f84a2020000          je        near 004A3F5Ah
  004a3cb8  56                    push      esi
  004a3cb9  e8e2780300            call      004DB5A0h
  004a3cbe  8b06                  mov       eax,[esi]
  004a3cc0  83c404                add       esp,4
  004a3cc3  a802                  test      al,2
  004a3cc5  0f8488000000          je        near 004A3D53h
  004a3ccb  8a4c2420              mov       cl,[esp+20h]
  004a3ccf  bb10000000            mov       ebx,10h
  004a3cd4  84cb                  test      bl,cl
  004a3cd6  740d                  je        short 004A3CE5h
  004a3cd8  24fd                  and       al,0FDh
  004a3cda  c6464300              mov       byte [esi+43h],0
  004a3cde  8906                  mov       [esi],eax
  004a3ce0  e975020000            jmp       004A3F5Ah

LAB_004a3ce5:
  004a3ce5  66395e1a              cmp       [esi+1Ah],bx
  004a3ce9  754e                  jne       short 004A3D39h
  004a3ceb  8d542430              lea       edx,[esp+30h]
  004a3cef  55                    push      ebp
  004a3cf0  8d461c                lea       eax,[esi+1Ch]
  004a3cf3  52                    push      edx
  004a3cf4  50                    push      eax
  004a3cf5  56                    push      esi
  004a3cf6  e8355f0700            call      00519C30h
  004a3cfb  8bf8                  mov       edi,eax
  004a3cfd  a1b8e05d00            mov       eax,[5DE0B8h]
  004a3d02  83c410                add       esp,10h
  004a3d05  3bc5                  cmp       eax,ebp
  004a3d07  7430                  je        short 004A3D39h
  004a3d09  66899f96000000        mov       [edi+96h],bx
  004a3d10  8b1f                  mov       ebx,[edi]
  004a3d12  83cb40                or        ebx,40h
  004a3d15  57                    push      edi
  004a3d16  56                    push      esi
  004a3d17  891f                  mov       [edi],ebx
  004a3d19  e8625e0900            call      00539B80h
  004a3d1e  83c408                add       esp,8
  004a3d21  83c71c                add       edi,1Ch
  004a3d24  57                    push      edi
  004a3d25  6800180000            push      1800h
  004a3d2a  6800030000            push      300h
  004a3d2f  6a03                  push      3
  004a3d31  e8ea9d0900            call      0053DB20h
  004a3d36  83c410                add       esp,10h

LAB_004a3d39:
  004a3d39  8b06                  mov       eax,[esi]
  004a3d3b  a900000002            test      eax,2000000h
  004a3d40  0f8414020000          je        near 004A3F5Ah
  004a3d46  24fd                  and       al,0FDh
  004a3d48  c6464300              mov       byte [esi+43h],0
  004a3d4c  8906                  mov       [esi],eax
  004a3d4e  e907020000            jmp       004A3F5Ah

LAB_004a3d53:
  004a3d53  a810                  test      al,10h
  004a3d55  0f85ff010000          jne       near 004A3F5Ah
  004a3d5b  a904080000            test      eax,804h
  004a3d60  0f85f4010000          jne       near 004A3F5Ah
  004a3d66  33c9                  xor       ecx,ecx
  004a3d68  8a4e42                mov       cl,[esi+42h]
  004a3d6b  83f903                cmp       ecx,3
  004a3d6e  0f87e6010000          ja        near 004A3F5Ah
  004a3d74  ff248d60424a00        jmp       dword [ecx*4+4A4260h]
  004a3d7b  66396e18              cmp       [esi+18h],bp
  004a3d7f  740a                  je        short 004A3D8Bh
  004a3d81  0d00000001            or        eax,1000000h
  004a3d86  896e18                mov       [esi+18h],ebp
  004a3d89  8906                  mov       [esi],eax

LAB_004a3d8b:
  004a3d8b  817c24142c010000      cmp       dword [esp+14h],12Ch
  004a3d93  7d09                  jge       short 004A3D9Eh
  004a3d95  c6464300              mov       byte [esi+43h],0
  004a3d99  e9bc010000            jmp       004A3F5Ah

LAB_004a3d9e:
  004a3d9e  8a4643                mov       al,[esi+43h]
  004a3da1  84c0                  test      al,al
  004a3da3  7479                  je        short 004A3E1Eh
  004a3da5  fec8                  dec       al
  004a3da7  884643                mov       [esi+43h],al
  004a3daa  8b465c                mov       eax,[esi+5Ch]
  004a3dad  8bc8                  mov       ecx,eax
  004a3daf  c1f904                sar       ecx,4
  004a3db2  2bc1                  sub       eax,ecx
  004a3db4  89465c                mov       [esi+5Ch],eax
  004a3db7  e99e010000            jmp       004A3F5Ah
  004a3dbc  b901000000            mov       ecx,1
  004a3dc1  66394e18              cmp       [esi+18h],cx
  004a3dc5  740a                  je        short 004A3DD1h
  004a3dc7  0d00000001            or        eax,1000000h
  004a3dcc  894e18                mov       [esi+18h],ecx
  004a3dcf  8906                  mov       [esi],eax

LAB_004a3dd1:
  004a3dd1  8b542410              mov       edx,[esp+10h]
  004a3dd5  8b4c2440              mov       ecx,[esp+40h]
  004a3dd9  8d841280000000        lea       eax,[edx+edx+80h]
  004a3de0  8d1408                lea       edx,[eax+ecx]
  004a3de3  8d3c00                lea       edi,[eax+eax]
  004a3de6  81e2ff0f0000          and       edx,0FFFh
  004a3dec  3bd7                  cmp       edx,edi
  004a3dee  7f1a                  jg        short 004A3E0Ah
  004a3df0  8b465c                mov       eax,[esi+5Ch]
  004a3df3  66014e32              add       [esi+32h],cx
  004a3df7  8bc8                  mov       ecx,eax
  004a3df9  c6464300              mov       byte [esi+43h],0
  004a3dfd  c1f904                sar       ecx,4
  004a3e00  2bc1                  sub       eax,ecx
  004a3e02  89465c                mov       [esi+5Ch],eax
  004a3e05  e950010000            jmp       004A3F5Ah

LAB_004a3e0a:
  004a3e0a  81e1ff0f0000          and       ecx,0FFFh
  004a3e10  81f900080000          cmp       ecx,800h
  004a3e16  7c02                  jl        short 004A3E1Ah
  004a3e18  f7d8                  neg       eax

LAB_004a3e1a:
  004a3e1a  66014632              add       [esi+32h],ax

LAB_004a3e1e:
  004a3e1e  8b465c                mov       eax,[esi+5Ch]
  004a3e21  8bc8                  mov       ecx,eax
  004a3e23  c1f904                sar       ecx,4
  004a3e26  2bc1                  sub       eax,ecx
  004a3e28  89465c                mov       [esi+5Ch],eax
  004a3e2b  e92a010000            jmp       004A3F5Ah
  004a3e30  b901000000            mov       ecx,1
  004a3e35  66394e18              cmp       [esi+18h],cx
  004a3e39  740a                  je        short 004A3E45h
  004a3e3b  0d00000001            or        eax,1000000h
  004a3e40  894e18                mov       [esi+18h],ecx
  004a3e43  8906                  mov       [esi],eax

LAB_004a3e45:
  004a3e45  0fbf5610              movsx     edx,word [esi+10h]
  004a3e49  39542414              cmp       [esp+14h],edx
  004a3e4d  7d09                  jge       short 004A3E58h
  004a3e4f  c6464300              mov       byte [esi+43h],0
  004a3e53  e902010000            jmp       004A3F5Ah

LAB_004a3e58:
  004a3e58  8a4643                mov       al,[esi+43h]
  004a3e5b  84c0                  test      al,al
  004a3e5d  7405                  je        short 004A3E64h
  004a3e5f  fec8                  dec       al
  004a3e61  884643                mov       [esi+43h],al

LAB_004a3e64:
  004a3e64  8b542410              mov       edx,[esp+10h]
  004a3e68  8b4c2440              mov       ecx,[esp+40h]
  004a3e6c  8d841280000000        lea       eax,[edx+edx+80h]
  004a3e73  8d3c08                lea       edi,[eax+ecx]
  004a3e76  8d1c00                lea       ebx,[eax+eax]
  004a3e79  81e7ff0f0000          and       edi,0FFFh
  004a3e7f  3bfb                  cmp       edi,ebx
  004a3e81  7f06                  jg        short 004A3E89h
  004a3e83  66014e32              add       [esi+32h],cx
  004a3e87  eb14                  jmp       short 004A3E9Dh

LAB_004a3e89:
  004a3e89  81e1ff0f0000          and       ecx,0FFFh
  004a3e8f  81f900080000          cmp       ecx,800h
  004a3e95  7c02                  jl        short 004A3E99h
  004a3e97  f7d8                  neg       eax

LAB_004a3e99:
  004a3e99  66014632              add       [esi+32h],ax

LAB_004a3e9d:
  004a3e9d  8b465c                mov       eax,[esi+5Ch]
  004a3ea0  81c200020000          add       edx,200h
  004a3ea6  c1e207                shl       edx,7
  004a3ea9  2bd0                  sub       edx,eax
  004a3eab  c1fa04                sar       edx,4
  004a3eae  03d0                  add       edx,eax
  004a3eb0  89565c                mov       [esi+5Ch],edx
  004a3eb3  e9a2000000            jmp       004A3F5Ah
  004a3eb8  b901000000            mov       ecx,1
  004a3ebd  66394e18              cmp       [esi+18h],cx
  004a3ec1  740a                  je        short 004A3ECDh
  004a3ec3  0d00000001            or        eax,1000000h
  004a3ec8  894e18                mov       [esi+18h],ecx
  004a3ecb  8906                  mov       [esi],eax

LAB_004a3ecd:
  004a3ecd  33c0                  xor       eax,eax
  004a3ecf  8a4643                mov       al,[esi+43h]
  004a3ed2  48                    dec       eax
  004a3ed3  744e                  je        short 004A3F23h
  004a3ed5  48                    dec       eax
  004a3ed6  7579                  jne       short 004A3F51h
  004a3ed8  0fbf4610              movsx     eax,word [esi+10h]
  004a3edc  81c720010000          add       edi,120h
  004a3ee2  50                    push      eax
  004a3ee3  c1e708                shl       edi,8
  004a3ee6  897e5c                mov       [esi+5Ch],edi
  004a3ee9  e82e1b0a00            call      00545A1Ch
  004a3eee  0faf465c              imul      eax,[esi+5Ch]
  004a3ef2  0fbf4e10              movsx     ecx,word [esi+10h]
  004a3ef6  8b5e50                mov       ebx,[esi+50h]
  004a3ef9  51                    push      ecx
  004a3efa  c1f80c                sar       eax,0Ch
  004a3efd  03d8                  add       ebx,eax
  004a3eff  895e50                mov       [esi+50h],ebx
  004a3f02  e80f1b0a00            call      00545A16h
  004a3f07  0faf465c              imul      eax,[esi+5Ch]
  004a3f0b  8b4e58                mov       ecx,[esi+58h]
  004a3f0e  896e5c                mov       [esi+5Ch],ebp
  004a3f11  c1f80c                sar       eax,0Ch
  004a3f14  03c8                  add       ecx,eax
  004a3f16  8a4643                mov       al,[esi+43h]
  004a3f19  fec8                  dec       al
  004a3f1b  894e58                mov       [esi+58h],ecx
  004a3f1e  884643                mov       [esi+43h],al
  004a3f21  eb2e                  jmp       short 004A3F51h

LAB_004a3f23:
  004a3f23  8b4650                mov       eax,[esi+50h]
  004a3f26  8b4e58                mov       ecx,[esi+58h]
  004a3f29  c1f80c                sar       eax,0Ch
  004a3f2c  8bd0                  mov       edx,eax
  004a3f2e  c1f90c                sar       ecx,0Ch
  004a3f31  0fafd0                imul      edx,eax
  004a3f34  8bc1                  mov       eax,ecx
  004a3f36  0fafc1                imul      eax,ecx
  004a3f39  03d0                  add       edx,eax
  004a3f3b  52                    push      edx
  004a3f3c  e8cf1a0a00            call      00545A10h
  004a3f41  8b4c2410              mov       ecx,[esp+10h]
  004a3f45  c1f903                sar       ecx,3
  004a3f48  41                    inc       ecx
  004a3f49  3bc1                  cmp       eax,ecx
  004a3f4b  7f04                  jg        short 004A3F51h
  004a3f4d  c6464300              mov       byte [esi+43h],0

LAB_004a3f51:
  004a3f51  668b542440            mov       dx,[esp+40h]
  004a3f56  66015632              add       [esi+32h],dx

LAB_004a3f5a:
  004a3f5a  8b44241c              mov       eax,[esp+1Ch]
  004a3f5e  50                    push      eax
  004a3f5f  56                    push      esi
  004a3f60  e87b69ffff            call      0049A8E0h
  004a3f65  83c408                add       esp,8
  004a3f68  56                    push      esi
  004a3f69  e8d2f90800            call      00533940h
  004a3f6e  8b9690000000          mov       edx,[esi+90h]
  004a3f74  8b466c                mov       eax,[esi+6Ch]
  004a3f77  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004a3f7d  83c404                add       esp,4
  004a3f80  899690000000          mov       [esi+90h],edx
  004a3f86  8a08                  mov       cl,[eax]
  004a3f88  f6c101                test      cl,1
  004a3f8b  7506                  jne       short 004A3F93h
  004a3f8d  8b4860                mov       ecx,[eax+60h]
  004a3f90  894e48                mov       [esi+48h],ecx

LAB_004a3f93:
  004a3f93  e8a8310a00            call      00547140h
  004a3f98  8b7e60                mov       edi,[esi+60h]
  004a3f9b  25ff000000            and       eax,0FFh
  004a3fa0  0500010000            add       eax,100h
  004a3fa5  66014614              add       [esi+14h],ax
  004a3fa9  8b4648                mov       eax,[esi+48h]
  004a3fac  668b4e14              mov       cx,[esi+14h]
  004a3fb0  3bc7                  cmp       eax,edi
  004a3fb2  7c02                  jl        short 004A3FB6h
  004a3fb4  8bf8                  mov       edi,eax

LAB_004a3fb6:
  004a3fb6  f60602                test      byte [esi],2
  004a3fb9  7415                  je        short 004A3FD0h
  004a3fbb  8b4620                mov       eax,[esi+20h]
  004a3fbe  2bf8                  sub       edi,eax
  004a3fc0  81c700800200          add       edi,28000h
  004a3fc6  c1ff03                sar       edi,3
  004a3fc9  03f8                  add       edi,eax
  004a3fcb  897e20                mov       [esi+20h],edi
  004a3fce  eb22                  jmp       short 004A3FF2h

LAB_004a3fd0:
  004a3fd0  8b6e20                mov       ebp,[esi+20h]
  004a3fd3  0fbfd1                movsx     edx,cx
  004a3fd6  52                    push      edx
  004a3fd7  e8401a0a00            call      00545A1Ch
  004a3fdc  0500320000            add       eax,3200h
  004a3fe1  c1e006                shl       eax,6
  004a3fe4  03c7                  add       eax,edi
  004a3fe6  2bc5                  sub       eax,ebp
  004a3fe8  c1f804                sar       eax,4
  004a3feb  03c5                  add       eax,ebp
  004a3fed  33ed                  xor       ebp,ebp
  004a3fef  894620                mov       [esi+20h],eax

LAB_004a3ff2:
  004a3ff2  8b442424              mov       eax,[esp+24h]
  004a3ff6  50                    push      eax
  004a3ff7  56                    push      esi
  004a3ff8  e8f3dcffff            call      004A1CF0h
  004a3ffd  8b442424              mov       eax,[esp+24h]
  004a4001  83c408                add       esp,8
  004a4004  3bc5                  cmp       eax,ebp
  004a4006  0f844b020000          je        near 004A4257h
  004a400c  f70616080000          test      dword [esi],816h
  004a4012  0f853f020000          jne       near 004A4257h
  004a4018  8a4643                mov       al,[esi+43h]
  004a401b  84c0                  test      al,al
  004a401d  0f8534020000          jne       near 004A4257h
  004a4023  8b466c                mov       eax,[esi+6Ch]
  004a4026  8b5e1c                mov       ebx,[esi+1Ch]
  004a4029  8b4e24                mov       ecx,[esi+24h]
  004a402c  8b781c                mov       edi,[eax+1Ch]
  004a402f  8b6824                mov       ebp,[eax+24h]
  004a4032  2bfb                  sub       edi,ebx
  004a4034  2be9                  sub       ebp,ecx
  004a4036  c1ff0c                sar       edi,0Ch
  004a4039  c1fd0c                sar       ebp,0Ch
  004a403c  8bcf                  mov       ecx,edi
  004a403e  8bd5                  mov       edx,ebp
  004a4040  0fafcf                imul      ecx,edi
  004a4043  0fafd5                imul      edx,ebp
  004a4046  03ca                  add       ecx,edx
  004a4048  51                    push      ecx
  004a4049  e8c2190a00            call      00545A10h
  004a404e  55                    push      ebp
  004a404f  57                    push      edi
  004a4050  8944241c              mov       [esp+1Ch],eax
  004a4054  e8c9190a00            call      00545A22h
  004a4059  668b4e32              mov       cx,[esi+32h]
  004a405d  2bc1                  sub       eax,ecx
  004a405f  25ff0f0000            and       eax,0FFFh
  004a4064  89442440              mov       [esp+40h],eax
  004a4068  8b442414              mov       eax,[esp+14h]
  004a406c  3dfa000000            cmp       eax,0FAh
  004a4071  0f8de6000000          jge       near 004A415Dh
  004a4077  e8c4300a00            call      00547140h
  004a407c  99                    cdq
  004a407d  b964000000            mov       ecx,64h
  004a4082  f7f9                  idiv      ecx
  004a4084  83fa1e                cmp       edx,1Eh
  004a4087  7d65                  jge       short 004A40EEh
  004a4089  8b542440              mov       edx,[esp+40h]
  004a408d  8b7c2418              mov       edi,[esp+18h]
  004a4091  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a4098  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a409f  25ff0f0000            and       eax,0FFFh
  004a40a4  3bc1                  cmp       eax,ecx
  004a40a6  0f8f8a010000          jg        near 004A4236h
  004a40ac  e88f300a00            call      00547140h
  004a40b1  99                    cdq
  004a40b2  8d4c3f01              lea       ecx,[edi+edi+1]
  004a40b6  f7f9                  idiv      ecx
  004a40b8  8b442414              mov       eax,[esp+14h]
  004a40bc  83c264                add       edx,64h
  004a40bf  3bc2                  cmp       eax,edx
  004a40c1  0f8d6f010000          jge       near 004A4236h
  004a40c7  8b16                  mov       edx,[esi]
  004a40c9  c7461803000000        mov       dword [esi+18h],3
  004a40d0  81e2fffffffe          and       edx,0FEFFFFFFh
  004a40d6  c6464301              mov       byte [esi+43h],1
  004a40da  83ca02                or        edx,2
  004a40dd  c7465c00000000        mov       dword [esi+5Ch],0
  004a40e4  8916                  mov       [esi],edx
  004a40e6  5f                    pop       edi
  004a40e7  5e                    pop       esi
  004a40e8  5d                    pop       ebp
  004a40e9  5b                    pop       ebx
  004a40ea  83c42c                add       esp,2Ch
  004a40ed  c3                    ret

LAB_004a40ee:
  004a40ee  83fa3c                cmp       edx,3Ch
  004a40f1  7d2d                  jge       short 004A4120h
  004a40f3  c6464202              mov       byte [esi+42h],2
  004a40f7  e844300a00            call      00547140h
  004a40fc  99                    cdq
  004a40fd  b90a000000            mov       ecx,0Ah
  004a4102  66c746106400          mov       word [esi+10h],64h
  004a4108  f7f9                  idiv      ecx
  004a410a  8b442418              mov       eax,[esp+18h]
  004a410e  d1f8                  sar       eax,1
  004a4110  02d0                  add       dl,al
  004a4112  80c214                add       dl,14h
  004a4115  885643                mov       [esi+43h],dl
  004a4118  5f                    pop       edi
  004a4119  5e                    pop       esi
  004a411a  5d                    pop       ebp
  004a411b  5b                    pop       ebx
  004a411c  83c42c                add       esp,2Ch
  004a411f  c3                    ret

LAB_004a4120:
  004a4120  8b4c2410              mov       ecx,[esp+10h]
  004a4124  c6464203              mov       byte [esi+42h],3
  004a4128  c6464302              mov       byte [esi+43h],2
  004a412c  8db980000000          lea       edi,[ecx+80h]
  004a4132  c1e704                shl       edi,4
  004a4135  e806300a00            call      00547140h
  004a413a  99                    cdq
  004a413b  f7ff                  idiv      edi
  004a413d  d1ff                  sar       edi,1
  004a413f  2bd7                  sub       edx,edi
  004a4141  8b7c2440              mov       edi,[esp+40h]
  004a4145  03d7                  add       edx,edi
  004a4147  66035632              add       dx,[esi+32h]
  004a414b  81c200080000          add       edx,800h
  004a4151  66895610              mov       [esi+10h],dx
  004a4155  5f                    pop       edi
  004a4156  5e                    pop       esi
  004a4157  5d                    pop       ebp
  004a4158  5b                    pop       ebx
  004a4159  83c42c                add       esp,2Ch
  004a415c  c3                    ret

LAB_004a415d:
  004a415d  3df4010000            cmp       eax,1F4h
  004a4162  0f8d90000000          jge       near 004A41F8h
  004a4168  e8d32f0a00            call      00547140h
  004a416d  99                    cdq
  004a416e  b964000000            mov       ecx,64h
  004a4173  f7f9                  idiv      ecx
  004a4175  83fa2d                cmp       edx,2Dh
  004a4178  7d10                  jge       short 004A418Ah
  004a417a  c6464201              mov       byte [esi+42h],1
  004a417e  c6464301              mov       byte [esi+43h],1
  004a4182  5f                    pop       edi
  004a4183  5e                    pop       esi
  004a4184  5d                    pop       ebp
  004a4185  5b                    pop       ebx
  004a4186  83c42c                add       esp,2Ch
  004a4189  c3                    ret

LAB_004a418a:
  004a418a  83fa46                cmp       edx,46h
  004a418d  7d37                  jge       short 004A41C6h
  004a418f  8b542410              mov       edx,[esp+10h]
  004a4193  c6464203              mov       byte [esi+42h],3
  004a4197  c6464302              mov       byte [esi+43h],2
  004a419b  8dba80000000          lea       edi,[edx+80h]
  004a41a1  c1e704                shl       edi,4
  004a41a4  e8972f0a00            call      00547140h
  004a41a9  99                    cdq
  004a41aa  f7ff                  idiv      edi
  004a41ac  8b4c2440              mov       ecx,[esp+40h]
  004a41b0  d1ff                  sar       edi,1
  004a41b2  2bd7                  sub       edx,edi
  004a41b4  03d1                  add       edx,ecx
  004a41b6  66035632              add       dx,[esi+32h]
  004a41ba  66895610              mov       [esi+10h],dx
  004a41be  5f                    pop       edi
  004a41bf  5e                    pop       esi
  004a41c0  5d                    pop       ebp
  004a41c1  5b                    pop       ebx
  004a41c2  83c42c                add       esp,2Ch
  004a41c5  c3                    ret

LAB_004a41c6:
  004a41c6  83fa5a                cmp       edx,5Ah
  004a41c9  7d3f                  jge       short 004A420Ah
  004a41cb  c6464202              mov       byte [esi+42h],2
  004a41cf  e86c2f0a00            call      00547140h
  004a41d4  99                    cdq
  004a41d5  b90a000000            mov       ecx,0Ah
  004a41da  66c746106400          mov       word [esi+10h],64h
  004a41e0  f7f9                  idiv      ecx
  004a41e2  8b442418              mov       eax,[esp+18h]
  004a41e6  d1f8                  sar       eax,1
  004a41e8  02d0                  add       dl,al
  004a41ea  80c214                add       dl,14h
  004a41ed  885643                mov       [esi+43h],dl
  004a41f0  5f                    pop       edi
  004a41f1  5e                    pop       esi
  004a41f2  5d                    pop       ebp
  004a41f3  5b                    pop       ebx
  004a41f4  83c42c                add       esp,2Ch
  004a41f7  c3                    ret

LAB_004a41f8:
  004a41f8  e8432f0a00            call      00547140h
  004a41fd  99                    cdq
  004a41fe  b964000000            mov       ecx,64h
  004a4203  f7f9                  idiv      ecx
  004a4205  83fa1e                cmp       edx,1Eh
  004a4208  7d27                  jge       short 004A4231h

LAB_004a420a:
  004a420a  c6464200              mov       byte [esi+42h],0
  004a420e  e82d2f0a00            call      00547140h
  004a4213  99                    cdq
  004a4214  b90a000000            mov       ecx,0Ah
  004a4219  f7f9                  idiv      ecx
  004a421b  8b442418              mov       eax,[esp+18h]
  004a421f  d1f8                  sar       eax,1
  004a4221  02d0                  add       dl,al
  004a4223  80c214                add       dl,14h
  004a4226  885643                mov       [esi+43h],dl
  004a4229  5f                    pop       edi
  004a422a  5e                    pop       esi
  004a422b  5d                    pop       ebp
  004a422c  5b                    pop       ebx
  004a422d  83c42c                add       esp,2Ch
  004a4230  c3                    ret

LAB_004a4231:
  004a4231  83fa3c                cmp       edx,3Ch
  004a4234  7d10                  jge       short 004A4246h

LAB_004a4236:
  004a4236  c6464201              mov       byte [esi+42h],1
  004a423a  c6464301              mov       byte [esi+43h],1
  004a423e  5f                    pop       edi
  004a423f  5e                    pop       esi
  004a4240  5d                    pop       ebp
  004a4241  5b                    pop       ebx
  004a4242  83c42c                add       esp,2Ch
  004a4245  c3                    ret

LAB_004a4246:
  004a4246  c6464203              mov       byte [esi+42h],3
  004a424a  c6464302              mov       byte [esi+43h],2
  004a424e  e8ed2e0a00            call      00547140h
  004a4253  66894610              mov       [esi+10h],ax

LAB_004a4257:
  004a4257  5f                    pop       edi
  004a4258  5e                    pop       esi
  004a4259  5d                    pop       ebp
  004a425a  5b                    pop       ebx
  004a425b  83c42c                add       esp,2Ch
  004a425e  c3                    ret

34 cible(s) de saut interne : 0x004a3ce5, 0x004a3d39, 0x004a3d53, 0x004a3d8b, 0x004a3d9e, 0x004a3dd1, 0x004a3e0a, 0x004a3e1a, 0x004a3e1e, 0x004a3e45, 0x004a3e58, 0x004a3e64, 0x004a3e89, 0x004a3e99, 0x004a3e9d, 0x004a3ecd, 0x004a3f23, 0x004a3f51, 0x004a3f5a, 0x004a3f93, 0x004a3fb6, 0x004a3fd0, 0x004a3ff2, 0x004a40ee, 0x004a4120, 0x004a415d, 0x004a418a, 0x004a41c6, 0x004a41f8, 0x004a420a, 0x004a4231, 0x004a4236, 0x004a4246, 0x004a4257
