C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049c8e0  section .text  offset fichier 0x9bce0  1872 octets

  0049c8e0  83ec2c                sub       esp,2Ch
  0049c8e3  53                    push      ebx
  0049c8e4  55                    push      ebp
  0049c8e5  56                    push      esi
  0049c8e6  8b74243c              mov       esi,[esp+3Ch]
  0049c8ea  8d442420              lea       eax,[esp+20h]
  0049c8ee  57                    push      edi
  0049c8ef  8d4c2428              lea       ecx,[esp+28h]
  0049c8f3  50                    push      eax
  0049c8f4  8d542430              lea       edx,[esp+30h]
  0049c8f8  51                    push      ecx
  0049c8f9  33ed                  xor       ebp,ebp
  0049c8fb  52                    push      edx
  0049c8fc  56                    push      esi
  0049c8fd  66896c2440            mov       [esp+40h],bp
  0049c902  66896c2442            mov       [esp+42h],bp
  0049c907  66c74424442800        mov       word [esp+44h],28h
  0049c90e  66c74424463200        mov       word [esp+46h],32h
  0049c915  66896c2448            mov       [esp+48h],bp
  0049c91a  66c744244a3c00        mov       word [esp+4Ah],3Ch
  0049c921  e85ac00900            call      00538980h
  0049c926  83c410                add       esp,10h
  0049c929  8d442420              lea       eax,[esp+20h]
  0049c92d  8d4c2418              lea       ecx,[esp+18h]
  0049c931  8d542410              lea       edx,[esp+10h]
  0049c935  50                    push      eax
  0049c936  51                    push      ecx
  0049c937  8d44241c              lea       eax,[esp+1Ch]
  0049c93b  52                    push      edx
  0049c93c  8d4c244c              lea       ecx,[esp+4Ch]
  0049c940  50                    push      eax
  0049c941  51                    push      ecx
  0049c942  56                    push      esi
  0049c943  e8d8950900            call      00535F20h
  0049c948  8b4c2458              mov       ecx,[esp+58h]
  0049c94c  b840000000            mov       eax,40h
  0049c951  2bc1                  sub       eax,ecx
  0049c953  83c418                add       esp,18h
  0049c956  8944241c              mov       [esp+1Ch],eax
  0049c95a  8b442418              mov       eax,[esp+18h]
  0049c95e  3bc5                  cmp       eax,ebp
  0049c960  bf01000000            mov       edi,1
  0049c965  0f8411040000          je        near 0049CD7Ch
  0049c96b  56                    push      esi
  0049c96c  e82fec0300            call      004DB5A0h
  0049c971  8b06                  mov       eax,[esi]
  0049c973  83c404                add       esp,4
  0049c976  a802                  test      al,2
  0049c978  0f8440010000          je        near 0049CABEh
  0049c97e  8a4c2420              mov       cl,[esp+20h]
  0049c982  bb10000000            mov       ebx,10h
  0049c987  84cb                  test      bl,cl
  0049c989  7415                  je        short 0049C9A0h
  0049c98b  24fd                  and       al,0FDh
  0049c98d  c6464300              mov       byte [esi+43h],0
  0049c991  8906                  mov       [esi],eax
  0049c993  8a4640                mov       al,[esi+40h]
  0049c996  24fe                  and       al,0FEh
  0049c998  884640                mov       [esi+40h],al
  0049c99b  e9dc030000            jmp       0049CD7Ch

LAB_0049c9a0:
  0049c9a0  8a4640                mov       al,[esi+40h]
  0049c9a3  33ff                  xor       edi,edi
  0049c9a5  a801                  test      al,1
  0049c9a7  7416                  je        short 0049C9BFh
  0049c9a9  66ff4e14              dec       word [esi+14h]
  0049c9ad  66396e14              cmp       [esi+14h],bp
  0049c9b1  0f85a2000000          jne       near 0049CA59h
  0049c9b7  8b7e6c                mov       edi,[esi+6Ch]
  0049c9ba  e99a000000            jmp       0049CA59h

LAB_0049c9bf:
  0049c9bf  8d542430              lea       edx,[esp+30h]
  0049c9c3  8d6e1c                lea       ebp,[esi+1Ch]
  0049c9c6  6a00                  push      0
  0049c9c8  52                    push      edx
  0049c9c9  55                    push      ebp
  0049c9ca  56                    push      esi
  0049c9cb  e860d20700            call      00519C30h
  0049c9d0  8bf8                  mov       edi,eax
  0049c9d2  83c410                add       esp,10h
  0049c9d5  85ff                  test      edi,edi
  0049c9d7  747e                  je        short 0049CA57h
  0049c9d9  8b4624                mov       eax,[esi+24h]
  0049c9dc  8b4f24                mov       ecx,[edi+24h]
  0049c9df  2bc1                  sub       eax,ecx
  0049c9e1  8b4d00                mov       ecx,[ebp]
  0049c9e4  c1f80c                sar       eax,0Ch
  0049c9e7  50                    push      eax
  0049c9e8  8b471c                mov       eax,[edi+1Ch]
  0049c9eb  2bc8                  sub       ecx,eax
  0049c9ed  c1f90c                sar       ecx,0Ch
  0049c9f0  51                    push      ecx
  0049c9f1  e82c900a00            call      00545A22h
  0049c9f6  8be8                  mov       ebp,eax
  0049c9f8  55                    push      ebp
  0049c9f9  e81e900a00            call      00545A1Ch
  0049c9fe  8d0440                lea       eax,[eax+eax*2]
  0049ca01  55                    push      ebp
  0049ca02  8d1480                lea       edx,[eax+eax*4]
  0049ca05  8b4620                mov       eax,[esi+20h]
  0049ca08  c1e202                shl       edx,2
  0049ca0b  895644                mov       [esi+44h],edx
  0049ca0e  8b4f20                mov       ecx,[edi+20h]
  0049ca11  2bc1                  sub       eax,ecx
  0049ca13  894648                mov       [esi+48h],eax
  0049ca16  e8fb8f0a00            call      00545A16h
  0049ca1b  8d0440                lea       eax,[eax+eax*2]
  0049ca1e  8d0c80                lea       ecx,[eax+eax*4]
  0049ca21  c1e102                shl       ecx,2
  0049ca24  894e4c                mov       [esi+4Ch],ecx
  0049ca27  e814a70a00            call      00547140h
  0049ca2c  8b542440              mov       edx,[esp+40h]
  0049ca30  8a4e40                mov       cl,[esi+40h]
  0049ca33  83e007                and       eax,7
  0049ca36  80c901                or        cl,1
  0049ca39  c1fa02                sar       edx,2
  0049ca3c  884e40                mov       [esi+40h],cl
  0049ca3f  8d441008              lea       eax,[eax+edx+8]
  0049ca43  66894610              mov       [esi+10h],ax
  0049ca47  8b07                  mov       eax,[edi]
  0049ca49  0c40                  or        al,40h
  0049ca4b  66899f96000000        mov       [edi+96h],bx
  0049ca52  8907                  mov       [edi],eax
  0049ca54  897e6c                mov       [esi+6Ch],edi

LAB_0049ca57:
  0049ca57  33ed                  xor       ebp,ebp

LAB_0049ca59:
  0049ca59  3bfd                  cmp       edi,ebp
  0049ca5b  7442                  je        short 0049CA9Fh
  0049ca5d  57                    push      edi
  0049ca5e  56                    push      esi
  0049ca5f  e81cd10900            call      00539B80h
  0049ca64  83c408                add       esp,8
  0049ca67  8d4f1c                lea       ecx,[edi+1Ch]
  0049ca6a  51                    push      ecx
  0049ca6b  6800180000            push      1800h
  0049ca70  6800030000            push      300h
  0049ca75  6a03                  push      3
  0049ca77  e8a4100a00            call      0053DB20h
  0049ca7c  8b07                  mov       eax,[edi]
  0049ca7e  83c410                add       esp,10h
  0049ca81  a900000040            test      eax,40000000h
  0049ca86  7411                  je        short 0049CA99h
  0049ca88  8a4e40                mov       cl,[esi+40h]
  0049ca8b  8b06                  mov       eax,[esi]
  0049ca8d  80e1fe                and       cl,0FEh
  0049ca90  24fd                  and       al,0FDh
  0049ca92  884e40                mov       [esi+40h],cl
  0049ca95  8906                  mov       [esi],eax
  0049ca97  eb06                  jmp       short 0049CA9Fh

LAB_0049ca99:
  0049ca99  66c746141e00          mov       word [esi+14h],1Eh

LAB_0049ca9f:
  0049ca9f  8b06                  mov       eax,[esi]
  0049caa1  a801                  test      al,1
  0049caa3  0f85d3020000          jne       near 0049CD7Ch
  0049caa9  24fd                  and       al,0FDh
  0049caab  c6464300              mov       byte [esi+43h],0
  0049caaf  8906                  mov       [esi],eax
  0049cab1  8a4640                mov       al,[esi+40h]
  0049cab4  24fe                  and       al,0FEh
  0049cab6  884640                mov       [esi+40h],al
  0049cab9  e9bb020000            jmp       0049CD79h

LAB_0049cabe:
  0049cabe  a811                  test      al,11h
  0049cac0  0f85b6020000          jne       near 0049CD7Ch
  0049cac6  a904080000            test      eax,804h
  0049cacb  0f85ab020000          jne       near 0049CD7Ch
  0049cad1  33c9                  xor       ecx,ecx
  0049cad3  8a4e42                mov       cl,[esi+42h]
  0049cad6  2bcd                  sub       ecx,ebp
  0049cad8  0f846f020000          je        near 0049CD4Dh
  0049cade  49                    dec       ecx
  0049cadf  0f8419020000          je        near 0049CCFEh
  0049cae5  49                    dec       ecx
  0049cae6  0f8590020000          jne       near 0049CD7Ch
  0049caec  8b8e90000000          mov       ecx,[esi+90h]
  0049caf2  f6c502                test      ch,2
  0049caf5  0f8452010000          je        near 0049CC4Dh
  0049cafb  66397e18              cmp       [esi+18h],di
  0049caff  740a                  je        short 0049CB0Bh
  0049cb01  0d00000001            or        eax,1000000h
  0049cb06  897e18                mov       [esi+18h],edi
  0049cb09  8906                  mov       [esi],eax

LAB_0049cb0b:
  0049cb0b  33c0                  xor       eax,eax
  0049cb0d  8a4643                mov       al,[esi+43h]
  0049cb10  48                    dec       eax
  0049cb11  0f848e000000          je        near 0049CBA5h
  0049cb17  48                    dec       eax
  0049cb18  0f855e020000          jne       near 0049CD7Ch
  0049cb1e  8b542440              mov       edx,[esp+40h]
  0049cb22  8b4c2410              mov       ecx,[esp+10h]
  0049cb26  8d841280000000        lea       eax,[edx+edx+80h]
  0049cb2d  8d3c08                lea       edi,[eax+ecx]
  0049cb30  8d1c00                lea       ebx,[eax+eax]
  0049cb33  81e7ff0f0000          and       edi,0FFFh
  0049cb39  3bfb                  cmp       edi,ebx
  0049cb3b  7f06                  jg        short 0049CB43h
  0049cb3d  66014e32              add       [esi+32h],cx
  0049cb41  eb14                  jmp       short 0049CB57h

LAB_0049cb43:
  0049cb43  81e1ff0f0000          and       ecx,0FFFh
  0049cb49  81f900080000          cmp       ecx,800h
  0049cb4f  7c02                  jl        short 0049CB53h
  0049cb51  f7d8                  neg       eax

LAB_0049cb53:
  0049cb53  66014632              add       [esi+32h],ax

LAB_0049cb57:
  0049cb57  81c280020000          add       edx,280h
  0049cb5d  c1e207                shl       edx,7
  0049cb60  89565c                mov       [esi+5Ch],edx
  0049cb63  0fbf5632              movsx     edx,word [esi+32h]
  0049cb67  52                    push      edx
  0049cb68  e8af8e0a00            call      00545A1Ch
  0049cb6d  0faf465c              imul      eax,[esi+5Ch]
  0049cb71  8b5e50                mov       ebx,[esi+50h]
  0049cb74  c1f80c                sar       eax,0Ch
  0049cb77  03d8                  add       ebx,eax
  0049cb79  0fbf4632              movsx     eax,word [esi+32h]
  0049cb7d  50                    push      eax
  0049cb7e  895e50                mov       [esi+50h],ebx
  0049cb81  e8908e0a00            call      00545A16h
  0049cb86  0faf465c              imul      eax,[esi+5Ch]
  0049cb8a  8b4e58                mov       ecx,[esi+58h]
  0049cb8d  896e5c                mov       [esi+5Ch],ebp
  0049cb90  c1f80c                sar       eax,0Ch
  0049cb93  03c8                  add       ecx,eax
  0049cb95  8a4643                mov       al,[esi+43h]
  0049cb98  fec8                  dec       al
  0049cb9a  894e58                mov       [esi+58h],ecx
  0049cb9d  884643                mov       [esi+43h],al
  0049cba0  e9d7010000            jmp       0049CD7Ch

LAB_0049cba5:
  0049cba5  8b4650                mov       eax,[esi+50h]
  0049cba8  8b4e58                mov       ecx,[esi+58h]
  0049cbab  c1f80c                sar       eax,0Ch
  0049cbae  8bd0                  mov       edx,eax
  0049cbb0  c1f90c                sar       ecx,0Ch
  0049cbb3  0fafd0                imul      edx,eax
  0049cbb6  8bc1                  mov       eax,ecx
  0049cbb8  0fafc1                imul      eax,ecx
  0049cbbb  03d0                  add       edx,eax
  0049cbbd  52                    push      edx
  0049cbbe  e84d8e0a00            call      00545A10h
  0049cbc3  8b4c2440              mov       ecx,[esp+40h]
  0049cbc7  8bd1                  mov       edx,ecx
  0049cbc9  c1fa03                sar       edx,3
  0049cbcc  42                    inc       edx
  0049cbcd  3bc2                  cmp       eax,edx
  0049cbcf  7f03                  jg        short 0049CBD4h
  0049cbd1  fe4e43                dec       byte [esi+43h]

LAB_0049cbd4:
  0049cbd4  0fbf4610              movsx     eax,word [esi+10h]
  0049cbd8  39442414              cmp       [esp+14h],eax
  0049cbdc  0f8d9a010000          jge       near 0049CD7Ch
  0049cbe2  8b16                  mov       edx,[esi]
  0049cbe4  81c100020000          add       ecx,200h
  0049cbea  c1e107                shl       ecx,7
  0049cbed  894e5c                mov       [esi+5Ch],ecx
  0049cbf0  81ca02000001          or        edx,1000002h
  0049cbf6  0fbf4e32              movsx     ecx,word [esi+32h]
  0049cbfa  51                    push      ecx
  0049cbfb  8916                  mov       [esi],edx
  0049cbfd  c7461803000000        mov       dword [esi+18h],3
  0049cc04  e8138e0a00            call      00545A1Ch
  0049cc09  0faf465c              imul      eax,[esi+5Ch]
  0049cc0d  0fbf5632              movsx     edx,word [esi+32h]
  0049cc11  8b4e50                mov       ecx,[esi+50h]
  0049cc14  52                    push      edx
  0049cc15  c1f80c                sar       eax,0Ch
  0049cc18  03c8                  add       ecx,eax
  0049cc1a  894e50                mov       [esi+50h],ecx
  0049cc1d  e8f48d0a00            call      00545A16h
  0049cc22  0faf465c              imul      eax,[esi+5Ch]
  0049cc26  8b4e58                mov       ecx,[esi+58h]
  0049cc29  c1f80c                sar       eax,0Ch
  0049cc2c  03c8                  add       ecx,eax
  0049cc2e  894e58                mov       [esi+58h],ecx
  0049cc31  e80aa50a00            call      00547140h
  0049cc36  83e007                and       eax,7
  0049cc39  83c020                add       eax,20h
  0049cc3c  c1e00c                shl       eax,0Ch
  0049cc3f  894654                mov       [esi+54h],eax
  0049cc42  8b06                  mov       eax,[esi]
  0049cc44  0bc7                  or        eax,edi
  0049cc46  8906                  mov       [esi],eax
  0049cc48  e92c010000            jmp       0049CD79h

LAB_0049cc4d:
  0049cc4d  8b7c2440              mov       edi,[esp+40h]
  0049cc51  8b542410              mov       edx,[esp+10h]
  0049cc55  8d8c3f80000000        lea       ecx,[edi+edi+80h]
  0049cc5c  8d1c11                lea       ebx,[ecx+edx]
  0049cc5f  8d2c09                lea       ebp,[ecx+ecx]
  0049cc62  81e3ff0f0000          and       ebx,0FFFh
  0049cc68  3bdd                  cmp       ebx,ebp
  0049cc6a  7f06                  jg        short 0049CC72h
  0049cc6c  66015632              add       [esi+32h],dx
  0049cc70  eb14                  jmp       short 0049CC86h

LAB_0049cc72:
  0049cc72  81e2ff0f0000          and       edx,0FFFh
  0049cc78  81fa00080000          cmp       edx,800h
  0049cc7e  7c02                  jl        short 0049CC82h
  0049cc80  f7d9                  neg       ecx

LAB_0049cc82:
  0049cc82  66014e32              add       [esi+32h],cx

LAB_0049cc86:
  0049cc86  0d02000001            or        eax,1000002h
  0049cc8b  81c740010000          add       edi,140h
  0049cc91  8906                  mov       [esi],eax
  0049cc93  c7461803000000        mov       dword [esi+18h],3
  0049cc9a  0fbf4632              movsx     eax,word [esi+32h]
  0049cc9e  c1e707                shl       edi,7
  0049cca1  50                    push      eax
  0049cca2  897e5c                mov       [esi+5Ch],edi
  0049cca5  e8728d0a00            call      00545A1Ch
  0049ccaa  0faf465c              imul      eax,[esi+5Ch]
  0049ccae  8b4e50                mov       ecx,[esi+50h]
  0049ccb1  c1f80c                sar       eax,0Ch
  0049ccb4  03c8                  add       ecx,eax
  0049ccb6  894e50                mov       [esi+50h],ecx
  0049ccb9  0fbf4e32              movsx     ecx,word [esi+32h]
  0049ccbd  51                    push      ecx
  0049ccbe  e8538d0a00            call      00545A16h
  0049ccc3  0faf465c              imul      eax,[esi+5Ch]
  0049ccc7  8b7e58                mov       edi,[esi+58h]
  0049ccca  c1f80c                sar       eax,0Ch
  0049cccd  03f8                  add       edi,eax
  0049cccf  897e58                mov       [esi+58h],edi
  0049ccd2  e869a40a00            call      00547140h
  0049ccd7  8b54241c              mov       edx,[esp+1Ch]
  0049ccdb  83e007                and       eax,7
  0049ccde  8b4e54                mov       ecx,[esi+54h]
  0049cce1  83c010                add       eax,10h
  0049cce4  c1e004                shl       eax,4
  0049cce7  83e2f0                and       edx,0FFFFFFF0h
  0049ccea  03c2                  add       eax,edx
  0049ccec  c1e008                shl       eax,8
  0049ccef  03c8                  add       ecx,eax
  0049ccf1  8b06                  mov       eax,[esi]
  0049ccf3  0c01                  or        al,1
  0049ccf5  894e54                mov       [esi+54h],ecx
  0049ccf8  8906                  mov       [esi],eax
  0049ccfa  33ed                  xor       ebp,ebp
  0049ccfc  eb7b                  jmp       short 0049CD79h

LAB_0049ccfe:
  0049ccfe  66397e18              cmp       [esi+18h],di
  0049cd02  740a                  je        short 0049CD0Eh
  0049cd04  0d00000001            or        eax,1000000h
  0049cd09  897e18                mov       [esi+18h],edi
  0049cd0c  8906                  mov       [esi],eax

LAB_0049cd0e:
  0049cd0e  8b442440              mov       eax,[esp+40h]
  0049cd12  8b4c2410              mov       ecx,[esp+10h]
  0049cd16  8d840080000000        lea       eax,[eax+eax+80h]
  0049cd1d  8d1408                lea       edx,[eax+ecx]
  0049cd20  8d3c00                lea       edi,[eax+eax]
  0049cd23  81e2ff0f0000          and       edx,0FFFh
  0049cd29  3bd7                  cmp       edx,edi
  0049cd2b  7f0a                  jg        short 0049CD37h
  0049cd2d  66014e32              add       [esi+32h],cx
  0049cd31  c6464300              mov       byte [esi+43h],0
  0049cd35  eb42                  jmp       short 0049CD79h

LAB_0049cd37:
  0049cd37  81e1ff0f0000          and       ecx,0FFFh
  0049cd3d  81f900080000          cmp       ecx,800h
  0049cd43  7c02                  jl        short 0049CD47h
  0049cd45  f7d8                  neg       eax

LAB_0049cd47:
  0049cd47  66014632              add       [esi+32h],ax
  0049cd4b  eb2c                  jmp       short 0049CD79h

LAB_0049cd4d:
  0049cd4d  66396e18              cmp       [esi+18h],bp
  0049cd51  740a                  je        short 0049CD5Dh
  0049cd53  0d00000001            or        eax,1000000h
  0049cd58  896e18                mov       [esi+18h],ebp
  0049cd5b  8906                  mov       [esi],eax

LAB_0049cd5d:
  0049cd5d  817c24142c010000      cmp       dword [esp+14h],12Ch
  0049cd65  7d06                  jge       short 0049CD6Dh
  0049cd67  c6464300              mov       byte [esi+43h],0
  0049cd6b  eb0f                  jmp       short 0049CD7Ch

LAB_0049cd6d:
  0049cd6d  8a4643                mov       al,[esi+43h]
  0049cd70  84c0                  test      al,al
  0049cd72  7405                  je        short 0049CD79h
  0049cd74  fec8                  dec       al
  0049cd76  884643                mov       [esi+43h],al

LAB_0049cd79:
  0049cd79  896e5c                mov       [esi+5Ch],ebp

LAB_0049cd7c:
  0049cd7c  8a4e40                mov       cl,[esi+40h]
  0049cd7f  f6c101                test      cl,1
  0049cd82  0f84b6000000          je        near 0049CE3Eh
  0049cd88  8b466c                mov       eax,[esi+6Ch]
  0049cd8b  8b7e44                mov       edi,[esi+44h]
  0049cd8e  8b5e48                mov       ebx,[esi+48h]
  0049cd91  8b501c                mov       edx,[eax+1Ch]
  0049cd94  03d7                  add       edx,edi
  0049cd96  8b7e4c                mov       edi,[esi+4Ch]
  0049cd99  89561c                mov       [esi+1Ch],edx
  0049cd9c  8b5020                mov       edx,[eax+20h]
  0049cd9f  03d3                  add       edx,ebx
  0049cda1  895620                mov       [esi+20h],edx
  0049cda4  8b5024                mov       edx,[eax+24h]
  0049cda7  03d7                  add       edx,edi
  0049cda9  895624                mov       [esi+24h],edx
  0049cdac  8b10                  mov       edx,[eax]
  0049cdae  f7c200000100          test      edx,10000h
  0049cdb4  7409                  je        short 0049CDBFh
  0049cdb6  33c0                  xor       eax,eax
  0049cdb8  a038e55d00            mov       al,[5DE538h]
  0049cdbd  eb07                  jmp       short 0049CDC6h

LAB_0049cdbf:
  0049cdbf  33d2                  xor       edx,edx
  0049cdc1  8a5008                mov       dl,[eax+8]
  0049cdc4  8bc2                  mov       eax,edx

LAB_0049cdc6:
  0049cdc6  8d14c0                lea       edx,[eax+eax*8]
  0049cdc9  8d0450                lea       eax,[eax+edx*2]
  0049cdcc  33d2                  xor       edx,edx
  0049cdce  8a148588915e00        mov       dl,[eax*4+5E9188h]
  0049cdd5  8bc2                  mov       eax,edx
  0049cdd7  c1e004                shl       eax,4
  0049cdda  05204a6000            add       eax,604A20h
  0049cddf  f64002f0              test      byte [eax+2],0F0h
  0049cde3  7467                  je        short 0049CE4Ch
  0049cde5  66ff4e10              dec       word [esi+10h]
  0049cde9  66396e10              cmp       [esi+10h],bp
  0049cded  755d                  jne       short 0049CE4Ch
  0049cdef  0fbf4632              movsx     eax,word [esi+32h]
  0049cdf3  8b16                  mov       edx,[esi]
  0049cdf5  0500080000            add       eax,800h
  0049cdfa  83e2fd                and       edx,0FFFFFFFDh
  0049cdfd  80e1fe                and       cl,0FEh
  0049ce00  50                    push      eax
  0049ce01  8916                  mov       [esi],edx
  0049ce03  c6464300              mov       byte [esi+43h],0
  0049ce07  884e40                mov       [esi+40h],cl
  0049ce0a  e80d8c0a00            call      00545A1Ch
  0049ce0f  8b4e50                mov       ecx,[esi+50h]
  0049ce12  c1e005                shl       eax,5
  0049ce15  03c8                  add       ecx,eax
  0049ce17  894e50                mov       [esi+50h],ecx
  0049ce1a  0fbf4e32              movsx     ecx,word [esi+32h]
  0049ce1e  81c100080000          add       ecx,800h
  0049ce24  51                    push      ecx
  0049ce25  e8ec8b0a00            call      00545A16h
  0049ce2a  8b4e58                mov       ecx,[esi+58h]
  0049ce2d  c7465400a00000        mov       dword [esi+54h],0A000h
  0049ce34  c1e005                shl       eax,5
  0049ce37  03c8                  add       ecx,eax
  0049ce39  894e58                mov       [esi+58h],ecx
  0049ce3c  eb0e                  jmp       short 0049CE4Ch

LAB_0049ce3e:
  0049ce3e  8b542418              mov       edx,[esp+18h]
  0049ce42  52                    push      edx
  0049ce43  56                    push      esi
  0049ce44  e817b3feff            call      00488160h
  0049ce49  83c408                add       esp,8

LAB_0049ce4c:
  0049ce4c  56                    push      esi
  0049ce4d  e8ee6a0900            call      00533940h
  0049ce52  8b9690000000          mov       edx,[esi+90h]
  0049ce58  8b4660                mov       eax,[esi+60h]
  0049ce5b  8b4e20                mov       ecx,[esi+20h]
  0049ce5e  83c404                add       esp,4
  0049ce61  81e2ffdbffdf          and       edx,0DFFFDBFFh
  0049ce67  3bc8                  cmp       ecx,eax
  0049ce69  899690000000          mov       [esi+90h],edx
  0049ce6f  7f24                  jg        short 0049CE95h
  0049ce71  894620                mov       [esi+20h],eax
  0049ce74  8b06                  mov       eax,[esi]
  0049ce76  a803                  test      al,3
  0049ce78  7410                  je        short 0049CE8Ah
  0049ce7a  24fd                  and       al,0FDh
  0049ce7c  c6464300              mov       byte [esi+43h],0
  0049ce80  8906                  mov       [esi],eax
  0049ce82  8a4640                mov       al,[esi+40h]
  0049ce85  24fe                  and       al,0FEh
  0049ce87  884640                mov       [esi+40h],al

LAB_0049ce8a:
  0049ce8a  8b06                  mov       eax,[esi]
  0049ce8c  896e54                mov       [esi+54h],ebp
  0049ce8f  24fe                  and       al,0FEh
  0049ce91  8906                  mov       [esi],eax
  0049ce93  eb0a                  jmp       short 0049CE9Fh

LAB_0049ce95:
  0049ce95  8b06                  mov       eax,[esi]
  0049ce97  a801                  test      al,1
  0049ce99  7504                  jne       short 0049CE9Fh
  0049ce9b  0c01                  or        al,1
  0049ce9d  8906                  mov       [esi],eax

LAB_0049ce9f:
  0049ce9f  8a4640                mov       al,[esi+40h]
  0049cea2  a801                  test      al,1
  0049cea4  7411                  je        short 0049CEB7h
  0049cea6  8b4e6c                mov       ecx,[esi+6Ch]
  0049cea9  f70100200050          test      dword [ecx],50002000h
  0049ceaf  750e                  jne       short 0049CEBFh
  0049ceb1  66396912              cmp       [ecx+12h],bp
  0049ceb5  7508                  jne       short 0049CEBFh

LAB_0049ceb7:
  0049ceb7  f7c282880000          test      edx,8882h
  0049cebd  7417                  je        short 0049CED6h

LAB_0049cebf:
  0049cebf  8b16                  mov       edx,[esi]
  0049cec1  c6464201              mov       byte [esi+42h],1
  0049cec5  83e2fd                and       edx,0FFFFFFFDh
  0049cec8  24fe                  and       al,0FEh
  0049ceca  8916                  mov       [esi],edx
  0049cecc  c6464301              mov       byte [esi+43h],1
  0049ced0  884640                mov       [esi+40h],al
  0049ced3  896e5c                mov       [esi+5Ch],ebp

LAB_0049ced6:
  0049ced6  6639ae96000000        cmp       [esi+96h],bp
  0049cedd  741b                  je        short 0049CEFAh
  0049cedf  8b06                  mov       eax,[esi]
  0049cee1  a840                  test      al,40h
  0049cee3  740a                  je        short 0049CEEFh
  0049cee5  a843                  test      al,43h
  0049cee7  7511                  jne       short 0049CEFAh
  0049cee9  c6464300              mov       byte [esi+43h],0
  0049ceed  eb0b                  jmp       short 0049CEFAh

LAB_0049ceef:
  0049ceef  c6464200              mov       byte [esi+42h],0
  0049cef3  c6464301              mov       byte [esi+43h],1
  0049cef7  896e5c                mov       [esi+5Ch],ebp

LAB_0049cefa:
  0049cefa  396c2418              cmp       [esp+18h],ebp
  0049cefe  0f841e010000          je        near 0049D022h
  0049cf04  f70617080000          test      dword [esi],817h
  0049cf0a  0f8512010000          jne       near 0049D022h
  0049cf10  8a4643                mov       al,[esi+43h]
  0049cf13  84c0                  test      al,al
  0049cf15  0f8507010000          jne       near 0049D022h
  0049cf1b  f6464001              test      byte [esi+40h],1
  0049cf1f  0f85fd000000          jne       near 0049D022h
  0049cf25  8b466c                mov       eax,[esi+6Ch]
  0049cf28  8b561c                mov       edx,[esi+1Ch]
  0049cf2b  8b781c                mov       edi,[eax+1Ch]
  0049cf2e  8b6824                mov       ebp,[eax+24h]
  0049cf31  8b4624                mov       eax,[esi+24h]
  0049cf34  2bfa                  sub       edi,edx
  0049cf36  2be8                  sub       ebp,eax
  0049cf38  c1ff0c                sar       edi,0Ch
  0049cf3b  c1fd0c                sar       ebp,0Ch
  0049cf3e  8bc7                  mov       eax,edi
  0049cf40  8bcd                  mov       ecx,ebp
  0049cf42  0fafc7                imul      eax,edi
  0049cf45  0fafcd                imul      ecx,ebp
  0049cf48  03c1                  add       eax,ecx
  0049cf4a  50                    push      eax
  0049cf4b  e8c08a0a00            call      00545A10h
  0049cf50  55                    push      ebp
  0049cf51  57                    push      edi
  0049cf52  8944241c              mov       [esp+1Ch],eax
  0049cf56  e8c78a0a00            call      00545A22h
  0049cf5b  668b5632              mov       dx,[esi+32h]
  0049cf5f  2bc2                  sub       eax,edx
  0049cf61  25ff0f0000            and       eax,0FFFh
  0049cf66  89442410              mov       [esp+10h],eax
  0049cf6a  8b442414              mov       eax,[esp+14h]
  0049cf6e  3dfa000000            cmp       eax,0FAh
  0049cf73  7d28                  jge       short 0049CF9Dh
  0049cf75  e8c6a10a00            call      00547140h
  0049cf7a  99                    cdq
  0049cf7b  b964000000            mov       ecx,64h
  0049cf80  f7f9                  idiv      ecx
  0049cf82  83fa32                cmp       edx,32h
  0049cf85  7d6c                  jge       short 0049CFF3h
  0049cf87  c6464202              mov       byte [esi+42h],2
  0049cf8b  c6464302              mov       byte [esi+43h],2
  0049cf8f  66c746102c01          mov       word [esi+10h],12Ch
  0049cf95  5f                    pop       edi
  0049cf96  5e                    pop       esi
  0049cf97  5d                    pop       ebp
  0049cf98  5b                    pop       ebx
  0049cf99  83c42c                add       esp,2Ch
  0049cf9c  c3                    ret

LAB_0049cf9d:
  0049cf9d  3df4010000            cmp       eax,1F4h
  0049cfa2  7d3d                  jge       short 0049CFE1h
  0049cfa4  e897a10a00            call      00547140h
  0049cfa9  99                    cdq
  0049cfaa  b964000000            mov       ecx,64h
  0049cfaf  f7f9                  idiv      ecx
  0049cfb1  83fa2d                cmp       edx,2Dh
  0049cfb4  7d10                  jge       short 0049CFC6h
  0049cfb6  c6464201              mov       byte [esi+42h],1
  0049cfba  c6464301              mov       byte [esi+43h],1
  0049cfbe  5f                    pop       edi
  0049cfbf  5e                    pop       esi
  0049cfc0  5d                    pop       ebp
  0049cfc1  5b                    pop       ebx
  0049cfc2  83c42c                add       esp,2Ch
  0049cfc5  c3                    ret

LAB_0049cfc6:
  0049cfc6  83fa5a                cmp       edx,5Ah
  0049cfc9  7d38                  jge       short 0049D003h
  0049cfcb  c6464202              mov       byte [esi+42h],2
  0049cfcf  c6464302              mov       byte [esi+43h],2
  0049cfd3  66c746102c01          mov       word [esi+10h],12Ch
  0049cfd9  5f                    pop       edi
  0049cfda  5e                    pop       esi
  0049cfdb  5d                    pop       ebp
  0049cfdc  5b                    pop       ebx
  0049cfdd  83c42c                add       esp,2Ch
  0049cfe0  c3                    ret

LAB_0049cfe1:
  0049cfe1  e85aa10a00            call      00547140h
  0049cfe6  99                    cdq
  0049cfe7  b964000000            mov       ecx,64h
  0049cfec  f7f9                  idiv      ecx
  0049cfee  83fa32                cmp       edx,32h
  0049cff1  7d10                  jge       short 0049D003h

LAB_0049cff3:
  0049cff3  c6464201              mov       byte [esi+42h],1
  0049cff7  c6464301              mov       byte [esi+43h],1
  0049cffb  5f                    pop       edi
  0049cffc  5e                    pop       esi
  0049cffd  5d                    pop       ebp
  0049cffe  5b                    pop       ebx
  0049cfff  83c42c                add       esp,2Ch
  0049d002  c3                    ret

LAB_0049d003:
  0049d003  c6464200              mov       byte [esi+42h],0
  0049d007  e834a10a00            call      00547140h
  0049d00c  99                    cdq
  0049d00d  b90a000000            mov       ecx,0Ah
  0049d012  f7f9                  idiv      ecx
  0049d014  8b44241c              mov       eax,[esp+1Ch]
  0049d018  d1f8                  sar       eax,1
  0049d01a  02d0                  add       dl,al
  0049d01c  80c214                add       dl,14h
  0049d01f  885643                mov       [esi+43h],dl

LAB_0049d022:
  0049d022  5f                    pop       edi
  0049d023  5e                    pop       esi
  0049d024  5d                    pop       ebp
  0049d025  5b                    pop       ebx
  0049d026  83c42c                add       esp,2Ch
  0049d029  c3                    ret
  0049d02a  90                    nop
  0049d02b  90                    nop
  0049d02c  90                    nop
  0049d02d  90                    nop
  0049d02e  90                    nop
  0049d02f  90                    nop

44 cible(s) de saut interne : 0x0049c9a0, 0x0049c9bf, 0x0049ca57, 0x0049ca59, 0x0049ca99, 0x0049ca9f, 0x0049cabe, 0x0049cb0b, 0x0049cb43, 0x0049cb53, 0x0049cb57, 0x0049cba5, 0x0049cbd4, 0x0049cc4d, 0x0049cc72, 0x0049cc82, 0x0049cc86, 0x0049ccfe, 0x0049cd0e, 0x0049cd37, 0x0049cd47, 0x0049cd4d, 0x0049cd5d, 0x0049cd6d, 0x0049cd79, 0x0049cd7c, 0x0049cdbf, 0x0049cdc6, 0x0049ce3e, 0x0049ce4c, 0x0049ce8a, 0x0049ce95, 0x0049ce9f, 0x0049ceb7, 0x0049cebf, 0x0049ced6, 0x0049ceef, 0x0049cefa, 0x0049cf9d, 0x0049cfc6, 0x0049cfe1, 0x0049cff3, 0x0049d003, 0x0049d022
