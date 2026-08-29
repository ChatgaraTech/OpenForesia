C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049b220  section .text  offset fichier 0x9a620  1568 octets

  0049b220  83ec2c                sub       esp,2Ch
  0049b223  53                    push      ebx
  0049b224  55                    push      ebp
  0049b225  56                    push      esi
  0049b226  8b74243c              mov       esi,[esp+3Ch]
  0049b22a  8d442424              lea       eax,[esp+24h]
  0049b22e  57                    push      edi
  0049b22f  8d4c242c              lea       ecx,[esp+2Ch]
  0049b233  50                    push      eax
  0049b234  8d542428              lea       edx,[esp+28h]
  0049b238  51                    push      ecx
  0049b239  33ed                  xor       ebp,ebp
  0049b23b  52                    push      edx
  0049b23c  56                    push      esi
  0049b23d  66896c2440            mov       [esp+40h],bp
  0049b242  66c74424421e00        mov       word [esp+42h],1Eh
  0049b249  66c74424443200        mov       word [esp+44h],32h
  0049b250  66c74424466400        mov       word [esp+46h],64h
  0049b257  66896c2448            mov       [esp+48h],bp
  0049b25c  66c744244a5000        mov       word [esp+4Ah],50h
  0049b263  e818d70900            call      00538980h
  0049b268  83c410                add       esp,10h
  0049b26b  8d442420              lea       eax,[esp+20h]
  0049b26f  8d4c241c              lea       ecx,[esp+1Ch]
  0049b273  8d542440              lea       edx,[esp+40h]
  0049b277  50                    push      eax
  0049b278  51                    push      ecx
  0049b279  8d44241c              lea       eax,[esp+1Ch]
  0049b27d  52                    push      edx
  0049b27e  8d4c241c              lea       ecx,[esp+1Ch]
  0049b282  50                    push      eax
  0049b283  51                    push      ecx
  0049b284  56                    push      esi
  0049b285  e896ac0900            call      00535F20h
  0049b28a  8b4c2428              mov       ecx,[esp+28h]
  0049b28e  8b442434              mov       eax,[esp+34h]
  0049b292  bf40000000            mov       edi,40h
  0049b297  83c418                add       esp,18h
  0049b29a  2bf9                  sub       edi,ecx
  0049b29c  3bc5                  cmp       eax,ebp
  0049b29e  897c2418              mov       [esp+18h],edi
  0049b2a2  0f84a2020000          je        near 0049B54Ah
  0049b2a8  56                    push      esi
  0049b2a9  e8f2020400            call      004DB5A0h
  0049b2ae  8b06                  mov       eax,[esi]
  0049b2b0  83c404                add       esp,4
  0049b2b3  a802                  test      al,2
  0049b2b5  0f8488000000          je        near 0049B343h
  0049b2bb  8a4c2420              mov       cl,[esp+20h]
  0049b2bf  bb10000000            mov       ebx,10h
  0049b2c4  84cb                  test      bl,cl
  0049b2c6  740d                  je        short 0049B2D5h
  0049b2c8  24fd                  and       al,0FDh
  0049b2ca  c6464300              mov       byte [esi+43h],0
  0049b2ce  8906                  mov       [esi],eax
  0049b2d0  e975020000            jmp       0049B54Ah

LAB_0049b2d5:
  0049b2d5  66395e1a              cmp       [esi+1Ah],bx
  0049b2d9  754e                  jne       short 0049B329h
  0049b2db  8d542430              lea       edx,[esp+30h]
  0049b2df  55                    push      ebp
  0049b2e0  8d461c                lea       eax,[esi+1Ch]
  0049b2e3  52                    push      edx
  0049b2e4  50                    push      eax
  0049b2e5  56                    push      esi
  0049b2e6  e845e90700            call      00519C30h
  0049b2eb  8bf8                  mov       edi,eax
  0049b2ed  a1b8e05d00            mov       eax,[5DE0B8h]
  0049b2f2  83c410                add       esp,10h
  0049b2f5  3bc5                  cmp       eax,ebp
  0049b2f7  7430                  je        short 0049B329h
  0049b2f9  66899f96000000        mov       [edi+96h],bx
  0049b300  8b1f                  mov       ebx,[edi]
  0049b302  83cb40                or        ebx,40h
  0049b305  57                    push      edi
  0049b306  56                    push      esi
  0049b307  891f                  mov       [edi],ebx
  0049b309  e872e80900            call      00539B80h
  0049b30e  83c408                add       esp,8
  0049b311  83c71c                add       edi,1Ch
  0049b314  57                    push      edi
  0049b315  6800180000            push      1800h
  0049b31a  6800030000            push      300h
  0049b31f  6a03                  push      3
  0049b321  e8fa270a00            call      0053DB20h
  0049b326  83c410                add       esp,10h

LAB_0049b329:
  0049b329  8b06                  mov       eax,[esi]
  0049b32b  a900000002            test      eax,2000000h
  0049b330  0f8414020000          je        near 0049B54Ah
  0049b336  24fd                  and       al,0FDh
  0049b338  c6464300              mov       byte [esi+43h],0
  0049b33c  8906                  mov       [esi],eax
  0049b33e  e907020000            jmp       0049B54Ah

LAB_0049b343:
  0049b343  a810                  test      al,10h
  0049b345  0f85ff010000          jne       near 0049B54Ah
  0049b34b  a904080000            test      eax,804h
  0049b350  0f85f4010000          jne       near 0049B54Ah
  0049b356  33c9                  xor       ecx,ecx
  0049b358  8a4e42                mov       cl,[esi+42h]
  0049b35b  83f903                cmp       ecx,3
  0049b35e  0f87e6010000          ja        near 0049B54Ah
  0049b364  ff248d28b84900        jmp       dword [ecx*4+49B828h]
  0049b36b  66396e18              cmp       [esi+18h],bp
  0049b36f  740a                  je        short 0049B37Bh
  0049b371  0d00000001            or        eax,1000000h
  0049b376  896e18                mov       [esi+18h],ebp
  0049b379  8906                  mov       [esi],eax

LAB_0049b37b:
  0049b37b  817c24142c010000      cmp       dword [esp+14h],12Ch
  0049b383  7d09                  jge       short 0049B38Eh
  0049b385  c6464300              mov       byte [esi+43h],0
  0049b389  e9bc010000            jmp       0049B54Ah

LAB_0049b38e:
  0049b38e  8a4643                mov       al,[esi+43h]
  0049b391  84c0                  test      al,al
  0049b393  7479                  je        short 0049B40Eh
  0049b395  fec8                  dec       al
  0049b397  884643                mov       [esi+43h],al
  0049b39a  8b465c                mov       eax,[esi+5Ch]
  0049b39d  8bc8                  mov       ecx,eax
  0049b39f  c1f904                sar       ecx,4
  0049b3a2  2bc1                  sub       eax,ecx
  0049b3a4  89465c                mov       [esi+5Ch],eax
  0049b3a7  e99e010000            jmp       0049B54Ah
  0049b3ac  b901000000            mov       ecx,1
  0049b3b1  66394e18              cmp       [esi+18h],cx
  0049b3b5  740a                  je        short 0049B3C1h
  0049b3b7  0d00000001            or        eax,1000000h
  0049b3bc  894e18                mov       [esi+18h],ecx
  0049b3bf  8906                  mov       [esi],eax

LAB_0049b3c1:
  0049b3c1  8b542410              mov       edx,[esp+10h]
  0049b3c5  8b4c2440              mov       ecx,[esp+40h]
  0049b3c9  8d841280000000        lea       eax,[edx+edx+80h]
  0049b3d0  8d1408                lea       edx,[eax+ecx]
  0049b3d3  8d3c00                lea       edi,[eax+eax]
  0049b3d6  81e2ff0f0000          and       edx,0FFFh
  0049b3dc  3bd7                  cmp       edx,edi
  0049b3de  7f1a                  jg        short 0049B3FAh
  0049b3e0  8b465c                mov       eax,[esi+5Ch]
  0049b3e3  66014e32              add       [esi+32h],cx
  0049b3e7  8bc8                  mov       ecx,eax
  0049b3e9  c6464300              mov       byte [esi+43h],0
  0049b3ed  c1f904                sar       ecx,4
  0049b3f0  2bc1                  sub       eax,ecx
  0049b3f2  89465c                mov       [esi+5Ch],eax
  0049b3f5  e950010000            jmp       0049B54Ah

LAB_0049b3fa:
  0049b3fa  81e1ff0f0000          and       ecx,0FFFh
  0049b400  81f900080000          cmp       ecx,800h
  0049b406  7c02                  jl        short 0049B40Ah
  0049b408  f7d8                  neg       eax

LAB_0049b40a:
  0049b40a  66014632              add       [esi+32h],ax

LAB_0049b40e:
  0049b40e  8b465c                mov       eax,[esi+5Ch]
  0049b411  8bc8                  mov       ecx,eax
  0049b413  c1f904                sar       ecx,4
  0049b416  2bc1                  sub       eax,ecx
  0049b418  89465c                mov       [esi+5Ch],eax
  0049b41b  e92a010000            jmp       0049B54Ah
  0049b420  b901000000            mov       ecx,1
  0049b425  66394e18              cmp       [esi+18h],cx
  0049b429  740a                  je        short 0049B435h
  0049b42b  0d00000001            or        eax,1000000h
  0049b430  894e18                mov       [esi+18h],ecx
  0049b433  8906                  mov       [esi],eax

LAB_0049b435:
  0049b435  0fbf5610              movsx     edx,word [esi+10h]
  0049b439  39542414              cmp       [esp+14h],edx
  0049b43d  7d09                  jge       short 0049B448h
  0049b43f  c6464300              mov       byte [esi+43h],0
  0049b443  e902010000            jmp       0049B54Ah

LAB_0049b448:
  0049b448  8a4643                mov       al,[esi+43h]
  0049b44b  84c0                  test      al,al
  0049b44d  7405                  je        short 0049B454h
  0049b44f  fec8                  dec       al
  0049b451  884643                mov       [esi+43h],al

LAB_0049b454:
  0049b454  8b542410              mov       edx,[esp+10h]
  0049b458  8b4c2440              mov       ecx,[esp+40h]
  0049b45c  8d841280000000        lea       eax,[edx+edx+80h]
  0049b463  8d3c08                lea       edi,[eax+ecx]
  0049b466  8d1c00                lea       ebx,[eax+eax]
  0049b469  81e7ff0f0000          and       edi,0FFFh
  0049b46f  3bfb                  cmp       edi,ebx
  0049b471  7f06                  jg        short 0049B479h
  0049b473  66014e32              add       [esi+32h],cx
  0049b477  eb14                  jmp       short 0049B48Dh

LAB_0049b479:
  0049b479  81e1ff0f0000          and       ecx,0FFFh
  0049b47f  81f900080000          cmp       ecx,800h
  0049b485  7c02                  jl        short 0049B489h
  0049b487  f7d8                  neg       eax

LAB_0049b489:
  0049b489  66014632              add       [esi+32h],ax

LAB_0049b48d:
  0049b48d  8b465c                mov       eax,[esi+5Ch]
  0049b490  81c200020000          add       edx,200h
  0049b496  c1e207                shl       edx,7
  0049b499  2bd0                  sub       edx,eax
  0049b49b  c1fa04                sar       edx,4
  0049b49e  03d0                  add       edx,eax
  0049b4a0  89565c                mov       [esi+5Ch],edx
  0049b4a3  e9a2000000            jmp       0049B54Ah
  0049b4a8  b901000000            mov       ecx,1
  0049b4ad  66394e18              cmp       [esi+18h],cx
  0049b4b1  740a                  je        short 0049B4BDh
  0049b4b3  0d00000001            or        eax,1000000h
  0049b4b8  894e18                mov       [esi+18h],ecx
  0049b4bb  8906                  mov       [esi],eax

LAB_0049b4bd:
  0049b4bd  33c0                  xor       eax,eax
  0049b4bf  8a4643                mov       al,[esi+43h]
  0049b4c2  48                    dec       eax
  0049b4c3  744e                  je        short 0049B513h
  0049b4c5  48                    dec       eax
  0049b4c6  7579                  jne       short 0049B541h
  0049b4c8  0fbf4610              movsx     eax,word [esi+10h]
  0049b4cc  81c720010000          add       edi,120h
  0049b4d2  50                    push      eax
  0049b4d3  c1e708                shl       edi,8
  0049b4d6  897e5c                mov       [esi+5Ch],edi
  0049b4d9  e83ea50a00            call      00545A1Ch
  0049b4de  0faf465c              imul      eax,[esi+5Ch]
  0049b4e2  0fbf4e10              movsx     ecx,word [esi+10h]
  0049b4e6  8b5e50                mov       ebx,[esi+50h]
  0049b4e9  51                    push      ecx
  0049b4ea  c1f80c                sar       eax,0Ch
  0049b4ed  03d8                  add       ebx,eax
  0049b4ef  895e50                mov       [esi+50h],ebx
  0049b4f2  e81fa50a00            call      00545A16h
  0049b4f7  0faf465c              imul      eax,[esi+5Ch]
  0049b4fb  8b4e58                mov       ecx,[esi+58h]
  0049b4fe  896e5c                mov       [esi+5Ch],ebp
  0049b501  c1f80c                sar       eax,0Ch
  0049b504  03c8                  add       ecx,eax
  0049b506  8a4643                mov       al,[esi+43h]
  0049b509  fec8                  dec       al
  0049b50b  894e58                mov       [esi+58h],ecx
  0049b50e  884643                mov       [esi+43h],al
  0049b511  eb2e                  jmp       short 0049B541h

LAB_0049b513:
  0049b513  8b4650                mov       eax,[esi+50h]
  0049b516  8b4e58                mov       ecx,[esi+58h]
  0049b519  c1f80c                sar       eax,0Ch
  0049b51c  8bd0                  mov       edx,eax
  0049b51e  c1f90c                sar       ecx,0Ch
  0049b521  0fafd0                imul      edx,eax
  0049b524  8bc1                  mov       eax,ecx
  0049b526  0fafc1                imul      eax,ecx
  0049b529  03d0                  add       edx,eax
  0049b52b  52                    push      edx
  0049b52c  e8dfa40a00            call      00545A10h
  0049b531  8b4c2410              mov       ecx,[esp+10h]
  0049b535  c1f903                sar       ecx,3
  0049b538  41                    inc       ecx
  0049b539  3bc1                  cmp       eax,ecx
  0049b53b  7f04                  jg        short 0049B541h
  0049b53d  c6464300              mov       byte [esi+43h],0

LAB_0049b541:
  0049b541  668b542440            mov       dx,[esp+40h]
  0049b546  66015632              add       [esi+32h],dx

LAB_0049b54a:
  0049b54a  8b44241c              mov       eax,[esp+1Ch]
  0049b54e  50                    push      eax
  0049b54f  56                    push      esi
  0049b550  e88bf3ffff            call      0049A8E0h
  0049b555  83c408                add       esp,8
  0049b558  56                    push      esi
  0049b559  e8e2830900            call      00533940h
  0049b55e  8b9690000000          mov       edx,[esi+90h]
  0049b564  8b466c                mov       eax,[esi+6Ch]
  0049b567  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  0049b56d  83c404                add       esp,4
  0049b570  899690000000          mov       [esi+90h],edx
  0049b576  8a08                  mov       cl,[eax]
  0049b578  f6c101                test      cl,1
  0049b57b  7506                  jne       short 0049B583h
  0049b57d  8b4860                mov       ecx,[eax+60h]
  0049b580  894e48                mov       [esi+48h],ecx

LAB_0049b583:
  0049b583  e8b8bb0a00            call      00547140h
  0049b588  8b7e60                mov       edi,[esi+60h]
  0049b58b  25ff000000            and       eax,0FFh
  0049b590  0500010000            add       eax,100h
  0049b595  66014614              add       [esi+14h],ax
  0049b599  8b4648                mov       eax,[esi+48h]
  0049b59c  668b4e14              mov       cx,[esi+14h]
  0049b5a0  3bc7                  cmp       eax,edi
  0049b5a2  7c02                  jl        short 0049B5A6h
  0049b5a4  8bf8                  mov       edi,eax

LAB_0049b5a6:
  0049b5a6  f60602                test      byte [esi],2
  0049b5a9  7415                  je        short 0049B5C0h
  0049b5ab  8b4620                mov       eax,[esi+20h]
  0049b5ae  2bf8                  sub       edi,eax
  0049b5b0  81c700800200          add       edi,28000h
  0049b5b6  c1ff03                sar       edi,3
  0049b5b9  03f8                  add       edi,eax
  0049b5bb  897e20                mov       [esi+20h],edi
  0049b5be  eb22                  jmp       short 0049B5E2h

LAB_0049b5c0:
  0049b5c0  8b6e20                mov       ebp,[esi+20h]
  0049b5c3  0fbfd1                movsx     edx,cx
  0049b5c6  52                    push      edx
  0049b5c7  e850a40a00            call      00545A1Ch
  0049b5cc  0500320000            add       eax,3200h
  0049b5d1  c1e006                shl       eax,6
  0049b5d4  03c7                  add       eax,edi
  0049b5d6  2bc5                  sub       eax,ebp
  0049b5d8  c1f804                sar       eax,4
  0049b5db  03c5                  add       eax,ebp
  0049b5dd  33ed                  xor       ebp,ebp
  0049b5df  894620                mov       [esi+20h],eax

LAB_0049b5e2:
  0049b5e2  8b442424              mov       eax,[esp+24h]
  0049b5e6  50                    push      eax
  0049b5e7  56                    push      esi
  0049b5e8  e803670000            call      004A1CF0h
  0049b5ed  8b442424              mov       eax,[esp+24h]
  0049b5f1  83c408                add       esp,8
  0049b5f4  3bc5                  cmp       eax,ebp
  0049b5f6  0f8422020000          je        near 0049B81Eh
  0049b5fc  f70616080000          test      dword [esi],816h
  0049b602  0f8516020000          jne       near 0049B81Eh
  0049b608  8a4643                mov       al,[esi+43h]
  0049b60b  84c0                  test      al,al
  0049b60d  0f850b020000          jne       near 0049B81Eh
  0049b613  8b466c                mov       eax,[esi+6Ch]
  0049b616  8b5e1c                mov       ebx,[esi+1Ch]
  0049b619  8b4e24                mov       ecx,[esi+24h]
  0049b61c  8b781c                mov       edi,[eax+1Ch]
  0049b61f  8b6824                mov       ebp,[eax+24h]
  0049b622  2bfb                  sub       edi,ebx
  0049b624  2be9                  sub       ebp,ecx
  0049b626  c1ff0c                sar       edi,0Ch
  0049b629  c1fd0c                sar       ebp,0Ch
  0049b62c  8bcf                  mov       ecx,edi
  0049b62e  8bd5                  mov       edx,ebp
  0049b630  0fafcf                imul      ecx,edi
  0049b633  0fafd5                imul      edx,ebp
  0049b636  03ca                  add       ecx,edx
  0049b638  51                    push      ecx
  0049b639  e8d2a30a00            call      00545A10h
  0049b63e  55                    push      ebp
  0049b63f  57                    push      edi
  0049b640  8944241c              mov       [esp+1Ch],eax
  0049b644  e8d9a30a00            call      00545A22h
  0049b649  668b4e32              mov       cx,[esi+32h]
  0049b64d  2bc1                  sub       eax,ecx
  0049b64f  25ff0f0000            and       eax,0FFFh
  0049b654  89442440              mov       [esp+40h],eax
  0049b658  8b442414              mov       eax,[esp+14h]
  0049b65c  3dfa000000            cmp       eax,0FAh
  0049b661  0f8dbd000000          jge       near 0049B724h
  0049b667  e8d4ba0a00            call      00547140h
  0049b66c  99                    cdq
  0049b66d  b964000000            mov       ecx,64h
  0049b672  f7f9                  idiv      ecx
  0049b674  83fa1e                cmp       edx,1Eh
  0049b677  7d65                  jge       short 0049B6DEh
  0049b679  8b542440              mov       edx,[esp+40h]
  0049b67d  8b7c2418              mov       edi,[esp+18h]
  0049b681  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049b688  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049b68f  25ff0f0000            and       eax,0FFFh
  0049b694  3bc1                  cmp       eax,ecx
  0049b696  0f8f61010000          jg        near 0049B7FDh
  0049b69c  e89fba0a00            call      00547140h
  0049b6a1  99                    cdq
  0049b6a2  8d4c3f01              lea       ecx,[edi+edi+1]
  0049b6a6  f7f9                  idiv      ecx
  0049b6a8  8b442414              mov       eax,[esp+14h]
  0049b6ac  83c264                add       edx,64h
  0049b6af  3bc2                  cmp       eax,edx
  0049b6b1  0f8d46010000          jge       near 0049B7FDh
  0049b6b7  8b16                  mov       edx,[esi]
  0049b6b9  c7461803000000        mov       dword [esi+18h],3
  0049b6c0  81e2fffffffe          and       edx,0FEFFFFFFh
  0049b6c6  c6464301              mov       byte [esi+43h],1
  0049b6ca  83ca02                or        edx,2
  0049b6cd  c7465c00000000        mov       dword [esi+5Ch],0
  0049b6d4  8916                  mov       [esi],edx
  0049b6d6  5f                    pop       edi
  0049b6d7  5e                    pop       esi
  0049b6d8  5d                    pop       ebp
  0049b6d9  5b                    pop       ebx
  0049b6da  83c42c                add       esp,2Ch
  0049b6dd  c3                    ret

LAB_0049b6de:
  0049b6de  83fa3c                cmp       edx,3Ch
  0049b6e1  0f8cab000000          jl        near 0049B792h
  0049b6e7  8b4c2410              mov       ecx,[esp+10h]
  0049b6eb  c6464203              mov       byte [esi+42h],3
  0049b6ef  c6464302              mov       byte [esi+43h],2
  0049b6f3  8db980000000          lea       edi,[ecx+80h]
  0049b6f9  c1e704                shl       edi,4
  0049b6fc  e83fba0a00            call      00547140h
  0049b701  99                    cdq
  0049b702  f7ff                  idiv      edi
  0049b704  d1ff                  sar       edi,1
  0049b706  2bd7                  sub       edx,edi
  0049b708  8b7c2440              mov       edi,[esp+40h]
  0049b70c  03d7                  add       edx,edi
  0049b70e  66035632              add       dx,[esi+32h]
  0049b712  81c200080000          add       edx,800h
  0049b718  66895610              mov       [esi+10h],dx
  0049b71c  5f                    pop       edi
  0049b71d  5e                    pop       esi
  0049b71e  5d                    pop       ebp
  0049b71f  5b                    pop       ebx
  0049b720  83c42c                add       esp,2Ch
  0049b723  c3                    ret

LAB_0049b724:
  0049b724  3df4010000            cmp       eax,1F4h
  0049b729  0f8d90000000          jge       near 0049B7BFh
  0049b72f  e80cba0a00            call      00547140h
  0049b734  99                    cdq
  0049b735  b964000000            mov       ecx,64h
  0049b73a  f7f9                  idiv      ecx
  0049b73c  83fa2d                cmp       edx,2Dh
  0049b73f  7d10                  jge       short 0049B751h
  0049b741  c6464201              mov       byte [esi+42h],1
  0049b745  c6464301              mov       byte [esi+43h],1
  0049b749  5f                    pop       edi
  0049b74a  5e                    pop       esi
  0049b74b  5d                    pop       ebp
  0049b74c  5b                    pop       ebx
  0049b74d  83c42c                add       esp,2Ch
  0049b750  c3                    ret

LAB_0049b751:
  0049b751  83fa46                cmp       edx,46h
  0049b754  7d37                  jge       short 0049B78Dh
  0049b756  8b542410              mov       edx,[esp+10h]
  0049b75a  c6464203              mov       byte [esi+42h],3
  0049b75e  c6464302              mov       byte [esi+43h],2
  0049b762  8dba80000000          lea       edi,[edx+80h]
  0049b768  c1e704                shl       edi,4
  0049b76b  e8d0b90a00            call      00547140h
  0049b770  99                    cdq
  0049b771  f7ff                  idiv      edi
  0049b773  8b4c2440              mov       ecx,[esp+40h]
  0049b777  d1ff                  sar       edi,1
  0049b779  2bd7                  sub       edx,edi
  0049b77b  03d1                  add       edx,ecx
  0049b77d  66035632              add       dx,[esi+32h]
  0049b781  66895610              mov       [esi+10h],dx
  0049b785  5f                    pop       edi
  0049b786  5e                    pop       esi
  0049b787  5d                    pop       ebp
  0049b788  5b                    pop       ebx
  0049b789  83c42c                add       esp,2Ch
  0049b78c  c3                    ret

LAB_0049b78d:
  0049b78d  83fa5a                cmp       edx,5Ah
  0049b790  7d3f                  jge       short 0049B7D1h

LAB_0049b792:
  0049b792  c6464202              mov       byte [esi+42h],2
  0049b796  e8a5b90a00            call      00547140h
  0049b79b  99                    cdq
  0049b79c  b90a000000            mov       ecx,0Ah
  0049b7a1  66c746106400          mov       word [esi+10h],64h
  0049b7a7  f7f9                  idiv      ecx
  0049b7a9  8b442418              mov       eax,[esp+18h]
  0049b7ad  d1f8                  sar       eax,1
  0049b7af  02d0                  add       dl,al
  0049b7b1  80c214                add       dl,14h
  0049b7b4  885643                mov       [esi+43h],dl
  0049b7b7  5f                    pop       edi
  0049b7b8  5e                    pop       esi
  0049b7b9  5d                    pop       ebp
  0049b7ba  5b                    pop       ebx
  0049b7bb  83c42c                add       esp,2Ch
  0049b7be  c3                    ret

LAB_0049b7bf:
  0049b7bf  e87cb90a00            call      00547140h
  0049b7c4  99                    cdq
  0049b7c5  b964000000            mov       ecx,64h
  0049b7ca  f7f9                  idiv      ecx
  0049b7cc  83fa1e                cmp       edx,1Eh
  0049b7cf  7d27                  jge       short 0049B7F8h

LAB_0049b7d1:
  0049b7d1  c6464200              mov       byte [esi+42h],0
  0049b7d5  e866b90a00            call      00547140h
  0049b7da  99                    cdq
  0049b7db  b90a000000            mov       ecx,0Ah
  0049b7e0  f7f9                  idiv      ecx
  0049b7e2  8b442418              mov       eax,[esp+18h]
  0049b7e6  d1f8                  sar       eax,1
  0049b7e8  02d0                  add       dl,al
  0049b7ea  80c214                add       dl,14h
  0049b7ed  885643                mov       [esi+43h],dl
  0049b7f0  5f                    pop       edi
  0049b7f1  5e                    pop       esi
  0049b7f2  5d                    pop       ebp
  0049b7f3  5b                    pop       ebx
  0049b7f4  83c42c                add       esp,2Ch
  0049b7f7  c3                    ret

LAB_0049b7f8:
  0049b7f8  83fa3c                cmp       edx,3Ch
  0049b7fb  7d10                  jge       short 0049B80Dh

LAB_0049b7fd:
  0049b7fd  c6464201              mov       byte [esi+42h],1
  0049b801  c6464301              mov       byte [esi+43h],1
  0049b805  5f                    pop       edi
  0049b806  5e                    pop       esi
  0049b807  5d                    pop       ebp
  0049b808  5b                    pop       ebx
  0049b809  83c42c                add       esp,2Ch
  0049b80c  c3                    ret

LAB_0049b80d:
  0049b80d  c6464203              mov       byte [esi+42h],3
  0049b811  c6464302              mov       byte [esi+43h],2
  0049b815  e826b90a00            call      00547140h
  0049b81a  66894610              mov       [esi+10h],ax

LAB_0049b81e:
  0049b81e  5f                    pop       edi
  0049b81f  5e                    pop       esi
  0049b820  5d                    pop       ebp
  0049b821  5b                    pop       ebx
  0049b822  83c42c                add       esp,2Ch
  0049b825  c3                    ret
  0049b826  8bff                  mov       edi,edi
  0049b828  6bb34900acb349        imul      esi,[ebx-4C53FFB7h],49h
  0049b82f  0020                  add       [eax],ah
  0049b831  b449                  mov       ah,49h
  0049b833  00a8b4490090          add       [eax-6FFFB64Ch],ch
  0049b839  90                    nop
  0049b83a  90                    nop
  0049b83b  90                    nop
  0049b83c  90                    nop
  0049b83d  90                    nop
  0049b83e  90                    nop
  0049b83f  90                    nop

34 cible(s) de saut interne : 0x0049b2d5, 0x0049b329, 0x0049b343, 0x0049b37b, 0x0049b38e, 0x0049b3c1, 0x0049b3fa, 0x0049b40a, 0x0049b40e, 0x0049b435, 0x0049b448, 0x0049b454, 0x0049b479, 0x0049b489, 0x0049b48d, 0x0049b4bd, 0x0049b513, 0x0049b541, 0x0049b54a, 0x0049b583, 0x0049b5a6, 0x0049b5c0, 0x0049b5e2, 0x0049b6de, 0x0049b724, 0x0049b751, 0x0049b78d, 0x0049b792, 0x0049b7bf, 0x0049b7d1, 0x0049b7f8, 0x0049b7fd, 0x0049b80d, 0x0049b81e
