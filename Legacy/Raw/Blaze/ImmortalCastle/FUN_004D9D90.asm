C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d9d90  section .text  offset fichier 0xd9190  3757 octets

  004d9d90  81ec84000000          sub       esp,84h
  004d9d96  53                    push      ebx
  004d9d97  55                    push      ebp
  004d9d98  56                    push      esi
  004d9d99  8bb42494000000        mov       esi,[esp+94h]
  004d9da0  b832000000            mov       eax,32h
  004d9da5  33db                  xor       ebx,ebx
  004d9da7  6689442430            mov       [esp+30h],ax
  004d9dac  6689442436            mov       [esp+36h],ax
  004d9db1  8a4641                mov       al,[esi+41h]
  004d9db4  57                    push      edi
  004d9db5  84c0                  test      al,al
  004d9db7  66895c2430            mov       [esp+30h],bx
  004d9dbc  66c74424321400        mov       word [esp+32h],14h
  004d9dc3  66c74424366400        mov       word [esp+36h],64h
  004d9dca  66895c2438            mov       [esp+38h],bx
  004d9dcf  7527                  jne       short 004D9DF8h
  004d9dd1  53                    push      ebx
  004d9dd2  6a06                  push      6
  004d9dd4  56                    push      esi
  004d9dd5  c6464101              mov       byte [esi+41h],1
  004d9dd9  e842bcffff            call      004D5A20h
  004d9dde  83c40c                add       esp,0Ch
  004d9de1  e85ad30600            call      00547140h
  004d9de6  99                    cdq
  004d9de7  33c2                  xor       eax,edx
  004d9de9  2bc2                  sub       eax,edx
  004d9deb  25ff0f0000            and       eax,0FFFh
  004d9df0  33c2                  xor       eax,edx
  004d9df2  2bc2                  sub       eax,edx
  004d9df4  66894614              mov       [esi+14h],ax

LAB_004d9df8:
  004d9df8  8b461c                mov       eax,[esi+1Ch]
  004d9dfb  8b4e20                mov       ecx,[esi+20h]
  004d9dfe  8b5624                mov       edx,[esi+24h]
  004d9e01  8d6e1c                lea       ebp,[esi+1Ch]
  004d9e04  89442424              mov       [esp+24h],eax
  004d9e08  8b4664                mov       eax,[esi+64h]
  004d9e0b  894c2428              mov       [esp+28h],ecx
  004d9e0f  8b4e60                mov       ecx,[esi+60h]
  004d9e12  8954242c              mov       [esp+2Ch],edx
  004d9e16  668b5632              mov       dx,[esi+32h]
  004d9e1a  89442444              mov       [esp+44h],eax
  004d9e1e  8b467c                mov       eax,[esi+7Ch]
  004d9e21  894c2440              mov       [esp+40h],ecx
  004d9e25  668b8e90000000        mov       cx,[esi+90h]
  004d9e2c  6689942482000000      mov       [esp+82h],dx
  004d9e34  8b9680000000          mov       edx,[esi+80h]
  004d9e3a  8944245c              mov       [esp+5Ch],eax
  004d9e3e  8b8684000000          mov       eax,[esi+84h]
  004d9e44  66894c2448            mov       [esp+48h],cx
  004d9e49  8b8e88000000          mov       ecx,[esi+88h]
  004d9e4f  8b7e6c                mov       edi,[esi+6Ch]
  004d9e52  8954244c              mov       [esp+4Ch],edx
  004d9e56  8b968c000000          mov       edx,[esi+8Ch]
  004d9e5c  8944243c              mov       [esp+3Ch],eax
  004d9e60  894c2458              mov       [esp+58h],ecx
  004d9e64  8d442460              lea       eax,[esp+60h]
  004d9e68  89542450              mov       [esp+50h],edx
  004d9e6c  8d4c2468              lea       ecx,[esp+68h]
  004d9e70  50                    push      eax
  004d9e71  8d542468              lea       edx,[esp+68h]
  004d9e75  51                    push      ecx
  004d9e76  52                    push      edx
  004d9e77  56                    push      esi
  004d9e78  e803eb0500            call      00538980h
  004d9e7d  83c410                add       esp,10h
  004d9e80  8d442454              lea       eax,[esp+54h]
  004d9e84  8d4c241c              lea       ecx,[esp+1Ch]
  004d9e88  8d542418              lea       edx,[esp+18h]
  004d9e8c  50                    push      eax
  004d9e8d  51                    push      ecx
  004d9e8e  8d44241c              lea       eax,[esp+1Ch]
  004d9e92  52                    push      edx
  004d9e93  8d4c241c              lea       ecx,[esp+1Ch]
  004d9e97  50                    push      eax
  004d9e98  51                    push      ecx
  004d9e99  56                    push      esi
  004d9e9a  e881c00500            call      00535F20h
  004d9e9f  8b4c2428              mov       ecx,[esp+28h]
  004d9ea3  8b54246c              mov       edx,[esp+6Ch]
  004d9ea7  83c418                add       esp,18h
  004d9eaa  b840000000            mov       eax,40h
  004d9eaf  2bc1                  sub       eax,ecx
  004d9eb1  52                    push      edx
  004d9eb2  56                    push      esi
  004d9eb3  89442428              mov       [esp+28h],eax
  004d9eb7  e824bfffff            call      004D5DE0h
  004d9ebc  8b4500                mov       eax,[ebp]
  004d9ebf  8b571c                mov       edx,[edi+1Ch]
  004d9ec2  8b4e20                mov       ecx,[esi+20h]
  004d9ec5  2bc2                  sub       eax,edx
  004d9ec7  8b5720                mov       edx,[edi+20h]
  004d9eca  83c408                add       esp,8
  004d9ecd  2bca                  sub       ecx,edx
  004d9ecf  8b5624                mov       edx,[esi+24h]
  004d9ed2  2b5724                sub       edx,[edi+24h]
  004d9ed5  c1f80c                sar       eax,0Ch
  004d9ed8  8bf8                  mov       edi,eax
  004d9eda  c1f90c                sar       ecx,0Ch
  004d9edd  0faff8                imul      edi,eax
  004d9ee0  8bc1                  mov       eax,ecx
  004d9ee2  c1fa0c                sar       edx,0Ch
  004d9ee5  0fafc1                imul      eax,ecx
  004d9ee8  8bca                  mov       ecx,edx
  004d9eea  03f8                  add       edi,eax
  004d9eec  0fafca                imul      ecx,edx
  004d9eef  03f9                  add       edi,ecx
  004d9ef1  57                    push      edi
  004d9ef2  e819bb0600            call      00545A10h
  004d9ef7  8b4c241c              mov       ecx,[esp+1Ch]
  004d9efb  89442414              mov       [esp+14h],eax
  004d9eff  3bcb                  cmp       ecx,ebx
  004d9f01  0f845a070000          je        near 004DA661h
  004d9f07  8b16                  mov       edx,[esi]
  004d9f09  33c0                  xor       eax,eax
  004d9f0b  8a4604                mov       al,[esi+4]
  004d9f0e  81e2fffffffd          and       edx,0FDFFFFFFh
  004d9f14  0fbf7e18              movsx     edi,word [esi+18h]
  004d9f18  8916                  mov       [esi],edx
  004d9f1a  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004d9f21  f7c200000001          test      edx,1000000h
  004d9f27  8b3cb8                mov       edi,[eax+edi*4]
  004d9f2a  741d                  je        short 004D9F49h
  004d9f2c  66ff461a              inc       word [esi+1Ah]
  004d9f30  668b461a              mov       ax,[esi+1Ah]
  004d9f34  33d2                  xor       edx,edx
  004d9f36  668b5716              mov       dx,[edi+16h]
  004d9f3a  0fbfc0                movsx     eax,ax
  004d9f3d  3bc2                  cmp       eax,edx
  004d9f3f  7c2a                  jl        short 004D9F6Bh
  004d9f41  66c7461a0000          mov       word [esi+1Ah],0
  004d9f47  eb22                  jmp       short 004D9F6Bh

LAB_004d9f49:
  004d9f49  66ff461a              inc       word [esi+1Ah]
  004d9f4d  668b461a              mov       ax,[esi+1Ah]
  004d9f51  33db                  xor       ebx,ebx
  004d9f53  668b5f16              mov       bx,[edi+16h]
  004d9f57  0fbff8                movsx     edi,ax
  004d9f5a  3bfb                  cmp       edi,ebx
  004d9f5c  7c0d                  jl        short 004D9F6Bh
  004d9f5e  48                    dec       eax
  004d9f5f  81ca00000002          or        edx,2000000h
  004d9f65  6689461a              mov       [esi+1Ah],ax
  004d9f69  8916                  mov       [esi],edx

LAB_004d9f6b:
  004d9f6b  f60602                test      byte [esi],2
  004d9f6e  0f84e9000000          je        near 004DA05Dh
  004d9f74  bb0d000000            mov       ebx,0Dh
  004d9f79  66395e1a              cmp       [esi+1Ah],bx
  004d9f7d  0f85bb000000          jne       near 004DA03Eh
  004d9f83  8d4c2430              lea       ecx,[esp+30h]
  004d9f87  6a00                  push      0
  004d9f89  51                    push      ecx
  004d9f8a  55                    push      ebp
  004d9f8b  56                    push      esi
  004d9f8c  e89ffc0300            call      00519C30h
  004d9f91  8bf8                  mov       edi,eax
  004d9f93  a1b8e05d00            mov       eax,[5DE0B8h]
  004d9f98  83c410                add       esp,10h
  004d9f9b  85c0                  test      eax,eax
  004d9f9d  0f8497000000          je        near 004DA03Ah
  004d9fa3  66395e1a              cmp       [esi+1Ah],bx
  004d9fa7  7531                  jne       short 004D9FDAh
  004d9fa9  8b07                  mov       eax,[edi]
  004d9fab  57                    push      edi
  004d9fac  0c40                  or        al,40h
  004d9fae  56                    push      esi
  004d9faf  66c787960000000300    mov       word [edi+96h],3
  004d9fb8  8907                  mov       [edi],eax
  004d9fba  e8c1fb0500            call      00539B80h
  004d9fbf  83c408                add       esp,8
  004d9fc2  8d571c                lea       edx,[edi+1Ch]
  004d9fc5  52                    push      edx
  004d9fc6  6800180000            push      1800h
  004d9fcb  6800030000            push      300h
  004d9fd0  6a03                  push      3
  004d9fd2  e8493b0600            call      0053DB20h
  004d9fd7  83c410                add       esp,10h

LAB_004d9fda:
  004d9fda  a1b8e05d00            mov       eax,[5DE0B8h]
  004d9fdf  85c0                  test      eax,eax
  004d9fe1  7457                  je        short 004DA03Ah
  004d9fe3  66837e1a13            cmp       word [esi+1Ah],13h
  004d9fe8  7550                  jne       short 004DA03Ah
  004d9fea  8b0f                  mov       ecx,[edi]
  004d9fec  57                    push      edi
  004d9fed  83c940                or        ecx,40h
  004d9ff0  56                    push      esi
  004d9ff1  66c787960000001000    mov       word [edi+96h],10h
  004d9ffa  890f                  mov       [edi],ecx
  004d9ffc  e87ffb0500            call      00539B80h
  004da001  83c408                add       esp,8
  004da004  8d471c                lea       eax,[edi+1Ch]
  004da007  50                    push      eax
  004da008  6800180000            push      1800h
  004da00d  6800030000            push      300h
  004da012  6a03                  push      3
  004da014  e8073b0600            call      0053DB20h
  004da019  83c410                add       esp,10h
  004da01c  e81fd10600            call      00547140h
  004da021  99                    cdq
  004da022  b964000000            mov       ecx,64h
  004da027  f7f9                  idiv      ecx
  004da029  83fa21                cmp       edx,21h
  004da02c  7d0c                  jge       short 004DA03Ah
  004da02e  6a00                  push      0
  004da030  57                    push      edi
  004da031  56                    push      esi
  004da032  e8490c0000            call      004DAC80h
  004da037  83c40c                add       esp,0Ch

LAB_004da03a:
  004da03a  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004da03e:
  004da03e  8b16                  mov       edx,[esi]
  004da040  f7c200000002          test      edx,2000000h
  004da046  7415                  je        short 004DA05Dh
  004da048  f6c201                test      dl,1
  004da04b  750b                  jne       short 004DA058h
  004da04d  83e2fd                and       edx,0FFFFFFFDh
  004da050  c6464300              mov       byte [esi+43h],0
  004da054  8916                  mov       [esi],edx
  004da056  eb05                  jmp       short 004DA05Dh

LAB_004da058:
  004da058  83e2fd                and       edx,0FFFFFFFDh
  004da05b  8916                  mov       [esi],edx

LAB_004da05d:
  004da05d  33c0                  xor       eax,eax
  004da05f  8a4642                mov       al,[esi+42h]
  004da062  83f80c                cmp       eax,0Ch
  004da065  0f87b4050000          ja        near 004DA61Fh
  004da06b  ff248540ac4d00        jmp       dword [eax*4+4DAC40h]
  004da072  33c9                  xor       ecx,ecx
  004da074  66394e12              cmp       [esi+12h],cx
  004da078  751a                  jne       short 004DA094h
  004da07a  8b06                  mov       eax,[esi]
  004da07c  66894e18              mov       [esi+18h],cx
  004da080  0d00000001            or        eax,1000000h
  004da085  66894e1a              mov       [esi+1Ah],cx
  004da089  8906                  mov       [esi],eax
  004da08b  66c746120100          mov       word [esi+12h],1
  004da091  894e5c                mov       [esi+5Ch],ecx

LAB_004da094:
  004da094  0fbf5610              movsx     edx,word [esi+10h]
  004da098  39542414              cmp       [esp+14h],edx
  004da09c  0f8c79050000          jl        near 004DA61Bh
  004da0a2  8a4643                mov       al,[esi+43h]
  004da0a5  84c0                  test      al,al
  004da0a7  0f84ff020000          je        near 004DA3ACh
  004da0ad  fec8                  dec       al
  004da0af  894e5c                mov       [esi+5Ch],ecx
  004da0b2  884643                mov       [esi+43h],al
  004da0b5  e965050000            jmp       004DA61Fh
  004da0ba  33c9                  xor       ecx,ecx
  004da0bc  66394e12              cmp       [esi+12h],cx
  004da0c0  751b                  jne       short 004DA0DDh
  004da0c2  8b16                  mov       edx,[esi]
  004da0c4  b801000000            mov       eax,1
  004da0c9  81ca00000001          or        edx,1000000h
  004da0cf  66894618              mov       [esi+18h],ax
  004da0d3  66894e1a              mov       [esi+1Ah],cx
  004da0d7  8916                  mov       [esi],edx
  004da0d9  66894612              mov       [esi+12h],ax

LAB_004da0dd:
  004da0dd  8b442418              mov       eax,[esp+18h]
  004da0e1  8d90c8000000          lea       edx,[eax+0C8h]
  004da0e7  81e2ff0f0000          and       edx,0FFFh
  004da0ed  81fa90010000          cmp       edx,190h
  004da0f3  7f0c                  jg        short 004DA101h
  004da0f5  c6464300              mov       byte [esi+43h],0
  004da0f9  894e5c                mov       [esi+5Ch],ecx
  004da0fc  e91e050000            jmp       004DA61Fh

LAB_004da101:
  004da101  25ff0f0000            and       eax,0FFFh
  004da106  3d00080000            cmp       eax,800h
  004da10b  7d15                  jge       short 004DA122h
  004da10d  8b442410              mov       eax,[esp+10h]
  004da111  894e5c                mov       [esi+5Ch],ecx
  004da114  0580000000            add       eax,80h
  004da119  66014632              add       [esi+32h],ax
  004da11d  e9fd040000            jmp       004DA61Fh

LAB_004da122:
  004da122  8b542410              mov       edx,[esp+10h]
  004da126  b880ffffff            mov       eax,0FFFFFF80h
  004da12b  2bc2                  sub       eax,edx
  004da12d  894e5c                mov       [esi+5Ch],ecx
  004da130  66014632              add       [esi+32h],ax
  004da134  e9e6040000            jmp       004DA61Fh
  004da139  8b4c2410              mov       ecx,[esp+10h]
  004da13d  33d2                  xor       edx,edx
  004da13f  66395612              cmp       [esi+12h],dx
  004da143  b801000000            mov       eax,1
  004da148  7525                  jne       short 004DA16Fh
  004da14a  8b3e                  mov       edi,[esi]
  004da14c  66894618              mov       [esi+18h],ax
  004da150  81cf00000001          or        edi,1000000h
  004da156  6689561a              mov       [esi+1Ah],dx
  004da15a  893e                  mov       [esi],edi
  004da15c  8d3cc9                lea       edi,[ecx+ecx*8]
  004da15f  c1e704                shl       edi,4
  004da162  81c700700000          add       edi,7000h
  004da168  66894612              mov       [esi+12h],ax
  004da16c  897e5c                mov       [esi+5Ch],edi

LAB_004da16f:
  004da16f  66394612              cmp       [esi+12h],ax
  004da173  0f85a6040000          jne       near 004DA61Fh
  004da179  0fbf4610              movsx     eax,word [esi+10h]
  004da17d  39442414              cmp       [esp+14h],eax
  004da181  7d0c                  jge       short 004DA18Fh
  004da183  c6464300              mov       byte [esi+43h],0
  004da187  89565c                mov       [esi+5Ch],edx
  004da18a  e990040000            jmp       004DA61Fh

LAB_004da18f:
  004da18f  8a4643                mov       al,[esi+43h]
  004da192  84c0                  test      al,al
  004da194  7405                  je        short 004DA19Bh
  004da196  fec8                  dec       al
  004da198  884643                mov       [esi+43h],al

LAB_004da19b:
  004da19b  8b442418              mov       eax,[esp+18h]
  004da19f  8d9000010000          lea       edx,[eax+100h]
  004da1a5  81e2ff0f0000          and       edx,0FFFh
  004da1ab  81fa00020000          cmp       edx,200h
  004da1b1  0f8e68040000          jle       near 004DA61Fh
  004da1b7  25ff0f0000            and       eax,0FFFh
  004da1bc  3d00080000            cmp       eax,800h
  004da1c1  7d0f                  jge       short 004DA1D2h
  004da1c3  8d8180000000          lea       eax,[ecx+80h]
  004da1c9  66014632              add       [esi+32h],ax
  004da1cd  e94d040000            jmp       004DA61Fh

LAB_004da1d2:
  004da1d2  b880ffffff            mov       eax,0FFFFFF80h
  004da1d7  2bc1                  sub       eax,ecx
  004da1d9  66014632              add       [esi+32h],ax
  004da1dd  e93d040000            jmp       004DA61Fh
  004da1e2  66837e1201            cmp       word [esi+12h],1
  004da1e7  0f859d000000          jne       near 004DA28Ah
  004da1ed  8a4643                mov       al,[esi+43h]
  004da1f0  84c0                  test      al,al
  004da1f2  7405                  je        short 004DA1F9h
  004da1f4  fec8                  dec       al
  004da1f6  884643                mov       [esi+43h],al

LAB_004da1f9:
  004da1f9  8b442410              mov       eax,[esp+10h]
  004da1fd  8d04c0                lea       eax,[eax+eax*8]
  004da200  c1e004                shl       eax,4
  004da203  0500600000            add       eax,6000h
  004da208  0fafc8                imul      ecx,eax
  004da20b  c1f90c                sar       ecx,0Ch
  004da20e  8bf9                  mov       edi,ecx
  004da210  89465c                mov       [esi+5Ch],eax
  004da213  0fbf4e10              movsx     ecx,word [esi+10h]
  004da217  51                    push      ecx
  004da218  e8ffb70600            call      00545A1Ch
  004da21d  0fafc7                imul      eax,edi
  004da220  0fbf5610              movsx     edx,word [esi+10h]
  004da224  8b4d00                mov       ecx,[ebp]
  004da227  52                    push      edx
  004da228  c1f80c                sar       eax,0Ch
  004da22b  03c8                  add       ecx,eax
  004da22d  894d00                mov       [ebp],ecx
  004da230  e8e1b70600            call      00545A16h
  004da235  0fafc7                imul      eax,edi
  004da238  8b5e24                mov       ebx,[esi+24h]
  004da23b  c7465c00000000        mov       dword [esi+5Ch],0
  004da242  c1f80c                sar       eax,0Ch
  004da245  03d8                  add       ebx,eax
  004da247  8b442418              mov       eax,[esp+18h]
  004da24b  895e24                mov       [esi+24h],ebx
  004da24e  8d8800010000          lea       ecx,[eax+100h]
  004da254  81e1ff0f0000          and       ecx,0FFFh
  004da25a  81f900020000          cmp       ecx,200h
  004da260  7e28                  jle       short 004DA28Ah
  004da262  25ff0f0000            and       eax,0FFFh
  004da267  3d00080000            cmp       eax,800h
  004da26c  7d0a                  jge       short 004DA278h
  004da26e  8b542410              mov       edx,[esp+10h]
  004da272  8d441232              lea       eax,[edx+edx+32h]
  004da276  eb0e                  jmp       short 004DA286h

LAB_004da278:
  004da278  8b442410              mov       eax,[esp+10h]
  004da27c  8d0c00                lea       ecx,[eax+eax]
  004da27f  b8ceffffff            mov       eax,0FFFFFFCEh
  004da284  2bc1                  sub       eax,ecx

LAB_004da286:
  004da286  66014632              add       [esi+32h],ax

LAB_004da28a:
  004da28a  66837e1200            cmp       word [esi+12h],0
  004da28f  0f858a030000          jne       near 004DA61Fh
  004da295  8b0e                  mov       ecx,[esi]
  004da297  b801000000            mov       eax,1
  004da29c  81c900000001          or        ecx,1000000h
  004da2a2  66894618              mov       [esi+18h],ax
  004da2a6  66c7461a0000          mov       word [esi+1Ah],0
  004da2ac  890e                  mov       [esi],ecx
  004da2ae  66894612              mov       [esi+12h],ax
  004da2b2  e968030000            jmp       004DA61Fh
  004da2b7  66837e1200            cmp       word [esi+12h],0
  004da2bc  0f8583000000          jne       near 004DA345h
  004da2c2  8b06                  mov       eax,[esi]
  004da2c4  c6464300              mov       byte [esi+43h],0
  004da2c8  a801                  test      al,1
  004da2ca  7406                  je        short 004DA2D2h
  004da2cc  c6464300              mov       byte [esi+43h],0
  004da2d0  eb73                  jmp       short 004DA345h

LAB_004da2d2:
  004da2d2  8b542410              mov       edx,[esp+10h]
  004da2d6  25fffffffe            and       eax,0FEFFFFFFh
  004da2db  8906                  mov       [esi],eax
  004da2dd  66c746180400          mov       word [esi+18h],4
  004da2e3  8d8200010000          lea       eax,[edx+100h]
  004da2e9  66c7461a0000          mov       word [esi+1Ah],0
  004da2ef  c1e006                shl       eax,6
  004da2f2  0fafc8                imul      ecx,eax
  004da2f5  89465c                mov       [esi+5Ch],eax
  004da2f8  0fbf4610              movsx     eax,word [esi+10h]
  004da2fc  c1f90c                sar       ecx,0Ch
  004da2ff  50                    push      eax
  004da300  8bf9                  mov       edi,ecx
  004da302  e815b70600            call      00545A1Ch
  004da307  0fafc7                imul      eax,edi
  004da30a  0fbf4e10              movsx     ecx,word [esi+10h]
  004da30e  8b6e50                mov       ebp,[esi+50h]
  004da311  51                    push      ecx
  004da312  c1f80c                sar       eax,0Ch
  004da315  03e8                  add       ebp,eax
  004da317  896e50                mov       [esi+50h],ebp
  004da31a  e8f7b60600            call      00545A16h
  004da31f  0fafc7                imul      eax,edi
  004da322  8b5658                mov       edx,[esi+58h]
  004da325  8b4e54                mov       ecx,[esi+54h]
  004da328  c1f80c                sar       eax,0Ch
  004da32b  03d0                  add       edx,eax
  004da32d  8b06                  mov       eax,[esi]
  004da32f  81c100e00100          add       ecx,1E000h
  004da335  0c01                  or        al,1
  004da337  895658                mov       [esi+58h],edx
  004da33a  894e54                mov       [esi+54h],ecx
  004da33d  8906                  mov       [esi],eax
  004da33f  66c746120100          mov       word [esi+12h],1

LAB_004da345:
  004da345  f60601                test      byte [esi],1
  004da348  0f85d1020000          jne       near 004DA61Fh
  004da34e  e9c8020000            jmp       004DA61Bh
  004da353  668b4e12              mov       cx,[esi+12h]
  004da357  ba01000000            mov       edx,1
  004da35c  663bca                cmp       cx,dx
  004da35f  751c                  jne       short 004DA37Dh
  004da361  0fbf4610              movsx     eax,word [esi+10h]
  004da365  39442414              cmp       [esp+14h],eax
  004da369  7d06                  jge       short 004DA371h
  004da36b  c6464300              mov       byte [esi+43h],0
  004da36f  eb0c                  jmp       short 004DA37Dh

LAB_004da371:
  004da371  8a4643                mov       al,[esi+43h]
  004da374  84c0                  test      al,al
  004da376  7405                  je        short 004DA37Dh
  004da378  fec8                  dec       al
  004da37a  884643                mov       [esi+43h],al

LAB_004da37d:
  004da37d  6685c9                test      cx,cx
  004da380  0f8599020000          jne       near 004DA61Fh
  004da386  8b442410              mov       eax,[esp+10h]
  004da38a  8b3e                  mov       edi,[esi]
  004da38c  66894e1a              mov       [esi+1Ah],cx
  004da390  81cf00000001          or        edi,1000000h
  004da396  8d0cc0                lea       ecx,[eax+eax*8]
  004da399  66895618              mov       [esi+18h],dx
  004da39d  c1e104                shl       ecx,4
  004da3a0  893e                  mov       [esi],edi
  004da3a2  66895612              mov       [esi+12h],dx
  004da3a6  81c100700000          add       ecx,7000h

LAB_004da3ac:
  004da3ac  894e5c                mov       [esi+5Ch],ecx
  004da3af  e96b020000            jmp       004DA61Fh
  004da3b4  33db                  xor       ebx,ebx
  004da3b6  66395e12              cmp       [esi+12h],bx
  004da3ba  0f85b7000000          jne       near 004DA477h
  004da3c0  b800f45f00            mov       eax,5FF400h

LAB_004da3c5:
  004da3c5  39706c                cmp       [eax+6Ch],esi
  004da3c8  750c                  jne       short 004DA3D6h
  004da3ca  f70000000080          test      dword [eax],80000000h
  004da3d0  0f8545020000          jne       near 004DA61Bh

LAB_004da3d6:
  004da3d6  059c000000            add       eax,9Ch
  004da3db  3d80076000            cmp       eax,600780h
  004da3e0  7ce3                  jl        short 004DA3C5h
  004da3e2  8b06                  mov       eax,[esi]
  004da3e4  a904080000            test      eax,804h
  004da3e9  7410                  je        short 004DA3FBh
  004da3eb  25fbf7ffff            and       eax,0FFFFF7FBh
  004da3f0  c6464300              mov       byte [esi+43h],0
  004da3f4  8906                  mov       [esi],eax
  004da3f6  e924020000            jmp       004DA61Fh

LAB_004da3fb:
  004da3fb  6aff                  push      0FFFFFFFFh
  004da3fd  53                    push      ebx
  004da3fe  68bd000000            push      0BDh
  004da403  56                    push      esi
  004da404  e8d706faff            call      0047AAE0h
  004da409  bf01000000            mov       edi,1
  004da40e  83c410                add       esp,10h
  004da411  66897e12              mov       [esi+12h],di
  004da415  e826cd0600            call      00547140h
  004da41a  99                    cdq
  004da41b  b90f000000            mov       ecx,0Fh
  004da420  f7f9                  idiv      ecx
  004da422  33c0                  xor       eax,eax
  004da424  668b0455a02c5500      mov       ax,[edx*2+552CA0h]
  004da42c  66894606              mov       [esi+6],ax
  004da430  8b0dc4975e00          mov       ecx,[5E97C4h]
  004da436  8bd0                  mov       edx,eax
  004da438  25ff000000            and       eax,0FFh
  004da43d  8b899c000000          mov       ecx,[ecx+9Ch]
  004da443  c1fa08                sar       edx,8
  004da446  81e2ff000000          and       edx,0FFh
  004da44c  8d0440                lea       eax,[eax+eax*2]
  004da44f  c1e004                shl       eax,4
  004da452  8b0c91                mov       ecx,[ecx+edx*4]
  004da455  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004da45b  8b06                  mov       eax,[esi]
  004da45d  66895610              mov       [esi+10h],dx
  004da461  0d04080001            or        eax,1000804h
  004da466  895e5c                mov       [esi+5Ch],ebx
  004da469  66c746180500          mov       word [esi+18h],5
  004da46f  66895e1a              mov       [esi+1Ah],bx
  004da473  8906                  mov       [esi],eax
  004da475  eb05                  jmp       short 004DA47Ch

LAB_004da477:
  004da477  bf01000000            mov       edi,1

LAB_004da47c:
  004da47c  66397e12              cmp       [esi+12h],di
  004da480  7523                  jne       short 004DA4A5h
  004da482  66ff4e10              dec       word [esi+10h]
  004da486  66397e10              cmp       [esi+10h],di
  004da48a  7d19                  jge       short 004DA4A5h
  004da48c  8b06                  mov       eax,[esi]
  004da48e  66c746120200          mov       word [esi+12h],2
  004da494  25fbfffffe            and       eax,0FEFFFFFBh
  004da499  66c746180600          mov       word [esi+18h],6
  004da49f  66895e1a              mov       [esi+1Ah],bx
  004da4a3  8906                  mov       [esi],eax

LAB_004da4a5:
  004da4a5  66837e1202            cmp       word [esi+12h],2
  004da4aa  7532                  jne       short 004DA4DEh
  004da4ac  66837e1a09            cmp       word [esi+1Ah],9
  004da4b1  752b                  jne       short 004DA4DEh
  004da4b3  33c0                  xor       eax,eax
  004da4b5  6aff                  push      0FFFFFFFFh
  004da4b7  668b4606              mov       ax,[esi+6]
  004da4bb  6aff                  push      0FFFFFFFFh
  004da4bd  8bc8                  mov       ecx,eax
  004da4bf  c1f808                sar       eax,8
  004da4c2  81e1ff000000          and       ecx,0FFh
  004da4c8  25ff000000            and       eax,0FFh
  004da4cd  51                    push      ecx
  004da4ce  50                    push      eax
  004da4cf  56                    push      esi
  004da4d0  e8ab040600            call      0053A980h
  004da4d5  83c414                add       esp,14h
  004da4d8  66c746120300          mov       word [esi+12h],3

LAB_004da4de:
  004da4de  66837e1203            cmp       word [esi+12h],3
  004da4e3  0f8536010000          jne       near 004DA61Fh
  004da4e9  8b06                  mov       eax,[esi]
  004da4eb  a900000002            test      eax,2000000h
  004da4f0  0f8429010000          je        near 004DA61Fh
  004da4f6  80e4f7                and       ah,0F7h
  004da4f9  c6464300              mov       byte [esi+43h],0
  004da4fd  8906                  mov       [esi],eax
  004da4ff  e91b010000            jmp       004DA61Fh
  004da504  668b4e12              mov       cx,[esi+12h]
  004da508  6683f901              cmp       cx,1
  004da50c  7511                  jne       short 004DA51Fh
  004da50e  8b06                  mov       eax,[esi]
  004da510  a900000002            test      eax,2000000h
  004da515  7408                  je        short 004DA51Fh
  004da517  24fd                  and       al,0FDh
  004da519  c6464300              mov       byte [esi+43h],0
  004da51d  8906                  mov       [esi],eax

LAB_004da51f:
  004da51f  6685c9                test      cx,cx
  004da522  0f85f7000000          jne       near 004DA61Fh
  004da528  8b06                  mov       eax,[esi]
  004da52a  66c746120100          mov       word [esi+12h],1
  004da530  25fffffffe            and       eax,0FEFFFFFFh
  004da535  8906                  mov       [esi],eax
  004da537  e9e3000000            jmp       004DA61Fh
  004da53c  33ff                  xor       edi,edi
  004da53e  66397e12              cmp       [esi+12h],di
  004da542  7568                  jne       short 004DA5ACh
  004da544  b900f45f00            mov       ecx,5FF400h

LAB_004da549:
  004da549  39716c                cmp       [ecx+6Ch],esi
  004da54c  750c                  jne       short 004DA55Ah
  004da54e  f70100000080          test      dword [ecx],80000000h
  004da554  0f85c1000000          jne       near 004DA61Bh

LAB_004da55a:
  004da55a  81c19c000000          add       ecx,9Ch
  004da560  81f980076000          cmp       ecx,600780h
  004da566  7ce1                  jl        short 004DA549h
  004da568  33d2                  xor       edx,edx
  004da56a  b9e2ca5e00            mov       ecx,5ECAE2h

LAB_004da56f:
  004da56f  80390c                cmp       byte [ecx],0Ch
  004da572  750d                  jne       short 004DA581h
  004da574  33c0                  xor       eax,eax
  004da576  8a4608                mov       al,[esi+8]
  004da579  3bd0                  cmp       edx,eax
  004da57b  0f859a000000          jne       near 004DA61Bh

LAB_004da581:
  004da581  81c19c000000          add       ecx,9Ch
  004da587  42                    inc       edx
  004da588  81f962de5e00          cmp       ecx,5EDE62h
  004da58e  7cdf                  jl        short 004DA56Fh
  004da590  8b06                  mov       eax,[esi]
  004da592  66c746180300          mov       word [esi+18h],3
  004da598  25fffffffc            and       eax,0FCFFFFFFh
  004da59d  66897e1a              mov       [esi+1Ah],di
  004da5a1  66c746120100          mov       word [esi+12h],1
  004da5a7  897e5c                mov       [esi+5Ch],edi
  004da5aa  8906                  mov       [esi],eax

LAB_004da5ac:
  004da5ac  66837e1201            cmp       word [esi+12h],1
  004da5b1  756c                  jne       short 004DA61Fh
  004da5b3  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004da5bb  7f5e                  jg        short 004DA61Bh
  004da5bd  66837e1a09            cmp       word [esi+1Ah],9
  004da5c2  754f                  jne       short 004DA613h
  004da5c4  66837e1600            cmp       word [esi+16h],0
  004da5c9  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004da5d0  7522                  jne       short 004DA5F4h
  004da5d2  e869cb0600            call      00547140h
  004da5d7  99                    cdq
  004da5d8  b90a000000            mov       ecx,0Ah
  004da5dd  f7f9                  idiv      ecx
  004da5df  33c0                  xor       eax,eax
  004da5e1  83fa04                cmp       edx,4
  004da5e4  0f9ec0                setle     al
  004da5e7  48                    dec       eax
  004da5e8  24fe                  and       al,0FEh
  004da5ea  83c002                add       eax,2
  004da5ed  6689869a000000        mov       [esi+9Ah],ax

LAB_004da5f4:
  004da5f4  8b4e6c                mov       ecx,[esi+6Ch]
  004da5f7  6aff                  push      0FFFFFFFFh
  004da5f9  6a00                  push      0
  004da5fb  68db000000            push      0DBh
  004da600  56                    push      esi
  004da601  894e78                mov       [esi+78h],ecx
  004da604  e8c703faff            call      0047A9D0h
  004da609  83c410                add       esp,10h
  004da60c  6689be9a000000        mov       [esi+9Ah],di

LAB_004da613:
  004da613  f70600000002          test      dword [esi],2000000h
  004da619  7404                  je        short 004DA61Fh

LAB_004da61b:
  004da61b  c6464300              mov       byte [esi+43h],0

LAB_004da61f:
  004da61f  66837e1801            cmp       word [esi+18h],1
  004da624  753b                  jne       short 004DA661h
  004da626  0fbf5614              movsx     edx,word [esi+14h]
  004da62a  52                    push      edx
  004da62b  e8ecb30600            call      00545A1Ch
  004da630  8b4e60                mov       ecx,[esi+60h]
  004da633  8bf8                  mov       edi,eax
  004da635  81c700280000          add       edi,2800h
  004da63b  c1e705                shl       edi,5
  004da63e  03f9                  add       edi,ecx
  004da640  e8fbca0600            call      00547140h
  004da645  99                    cdq
  004da646  b91e000000            mov       ecx,1Eh
  004da64b  f7f9                  idiv      ecx
  004da64d  8b4620                mov       eax,[esi+20h]
  004da650  2bf8                  sub       edi,eax
  004da652  c1ff03                sar       edi,3
  004da655  83c246                add       edx,46h
  004da658  66015614              add       [esi+14h],dx
  004da65c  03f8                  add       edi,eax
  004da65e  897e20                mov       [esi+20h],edi

LAB_004da661:
  004da661  0fbf5632              movsx     edx,word [esi+32h]
  004da665  52                    push      edx
  004da666  e8b1b30600            call      00545A1Ch
  004da66b  8bf8                  mov       edi,eax
  004da66d  0fbf4632              movsx     eax,word [esi+32h]
  004da671  50                    push      eax
  004da672  e89fb30600            call      00545A16h
  004da677  8b4c241c              mov       ecx,[esp+1Ch]
  004da67b  8b5e24                mov       ebx,[esi+24h]
  004da67e  0faf4e5c              imul      ecx,[esi+5Ch]
  004da682  c1f90c                sar       ecx,0Ch
  004da685  8bd1                  mov       edx,ecx
  004da687  0fafc8                imul      ecx,eax
  004da68a  0fafd7                imul      edx,edi
  004da68d  8b4650                mov       eax,[esi+50h]
  004da690  8b7e1c                mov       edi,[esi+1Ch]
  004da693  c1f90c                sar       ecx,0Ch
  004da696  c1fa0c                sar       edx,0Ch
  004da699  03d9                  add       ebx,ecx
  004da69b  8bc8                  mov       ecx,eax
  004da69d  03fa                  add       edi,edx
  004da69f  8bd0                  mov       edx,eax
  004da6a1  c1f908                sar       ecx,8
  004da6a4  2bd1                  sub       edx,ecx
  004da6a6  897e1c                mov       [esi+1Ch],edi
  004da6a9  c1f803                sar       eax,3
  004da6ac  2bd0                  sub       edx,eax
  004da6ae  66837e1801            cmp       word [esi+18h],1
  004da6b3  895e24                mov       [esi+24h],ebx
  004da6b6  8beb                  mov       ebp,ebx
  004da6b8  895650                mov       [esi+50h],edx
  004da6bb  7513                  jne       short 004DA6D0h
  004da6bd  8b4654                mov       eax,[esi+54h]
  004da6c0  8bc8                  mov       ecx,eax
  004da6c2  8bd8                  mov       ebx,eax
  004da6c4  c1f908                sar       ecx,8
  004da6c7  2bd9                  sub       ebx,ecx
  004da6c9  c1f803                sar       eax,3
  004da6cc  2bd8                  sub       ebx,eax
  004da6ce  eb1d                  jmp       short 004DA6EDh

LAB_004da6d0:
  004da6d0  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004da6d7  8b4e54                mov       ecx,[esi+54h]
  004da6da  8bd8                  mov       ebx,eax
  004da6dc  c1f908                sar       ecx,8
  004da6df  c1fb09                sar       ebx,9
  004da6e2  2bd9                  sub       ebx,ecx
  004da6e4  d1f8                  sar       eax,1
  004da6e6  2bd8                  sub       ebx,eax
  004da6e8  8b4654                mov       eax,[esi+54h]
  004da6eb  03d8                  add       ebx,eax

LAB_004da6ed:
  004da6ed  8b4658                mov       eax,[esi+58h]
  004da6f0  895e54                mov       [esi+54h],ebx
  004da6f3  8bd8                  mov       ebx,eax
  004da6f5  8bc8                  mov       ecx,eax
  004da6f7  c1fb03                sar       ebx,3
  004da6fa  2bcb                  sub       ecx,ebx
  004da6fc  8b5e20                mov       ebx,[esi+20h]
  004da6ff  c1f808                sar       eax,8
  004da702  03d7                  add       edx,edi
  004da704  2bc8                  sub       ecx,eax
  004da706  89561c                mov       [esi+1Ch],edx
  004da709  8b5654                mov       edx,[esi+54h]
  004da70c  894e58                mov       [esi+58h],ecx
  004da70f  03da                  add       ebx,edx
  004da711  03cd                  add       ecx,ebp
  004da713  56                    push      esi
  004da714  895e20                mov       [esi+20h],ebx
  004da717  894e24                mov       [esi+24h],ecx
  004da71a  e821920500            call      00533940h
  004da71f  8b8e90000000          mov       ecx,[esi+90h]
  004da725  8b4620                mov       eax,[esi+20h]
  004da728  80e5fc                and       ch,0FCh
  004da72b  83c404                add       esp,4
  004da72e  898e90000000          mov       [esi+90h],ecx
  004da734  8bd1                  mov       edx,ecx
  004da736  8b4e60                mov       ecx,[esi+60h]
  004da739  3bc1                  cmp       eax,ecx
  004da73b  7f27                  jg        short 004DA764h
  004da73d  f6c680                test      dh,80h
  004da740  7522                  jne       short 004DA764h
  004da742  8b06                  mov       eax,[esi]
  004da744  894e20                mov       [esi+20h],ecx
  004da747  a801                  test      al,1
  004da749  740e                  je        short 004DA759h
  004da74b  24fe                  and       al,0FEh
  004da74d  33db                  xor       ebx,ebx
  004da74f  8906                  mov       [esi],eax
  004da751  895e54                mov       [esi+54h],ebx
  004da754  e929010000            jmp       004DA882h

LAB_004da759:
  004da759  24fe                  and       al,0FEh
  004da75b  33db                  xor       ebx,ebx
  004da75d  8906                  mov       [esi],eax
  004da75f  895e54                mov       [esi+54h],ebx
  004da762  eb02                  jmp       short 004DA766h

LAB_004da764:
  004da764  33db                  xor       ebx,ebx

LAB_004da766:
  004da766  66837e1801            cmp       word [esi+18h],1
  004da76b  740f                  je        short 004DA77Ch
  004da76d  8b06                  mov       eax,[esi]
  004da76f  a801                  test      al,1
  004da771  7509                  jne       short 004DA77Ch
  004da773  394e20                cmp       [esi+20h],ecx
  004da776  7e04                  jle       short 004DA77Ch
  004da778  0c01                  or        al,1
  004da77a  8906                  mov       [esi],eax

LAB_004da77c:
  004da77c  f7c282ad0020          test      edx,2000AD82h
  004da782  0f84fa000000          je        near 004DA882h
  004da788  f60601                test      byte [esi],1
  004da78b  0f85c9000000          jne       near 004DA85Ah
  004da791  8a4e42                mov       cl,[esi+42h]
  004da794  b007                  mov       al,7
  004da796  3ac8                  cmp       cl,al
  004da798  7420                  je        short 004DA7BAh
  004da79a  884642                mov       [esi+42h],al
  004da79d  e89ec90600            call      00547140h
  004da7a2  99                    cdq
  004da7a3  b928000000            mov       ecx,28h
  004da7a8  66895e12              mov       [esi+12h],bx
  004da7ac  f7f9                  idiv      ecx
  004da7ae  66c746105000          mov       word [esi+10h],50h
  004da7b4  80c214                add       dl,14h
  004da7b7  885643                mov       [esi+43h],dl

LAB_004da7ba:
  004da7ba  8b16                  mov       edx,[esi]
  004da7bc  8b442428              mov       eax,[esp+28h]
  004da7c0  8b4c242c              mov       ecx,[esp+2Ch]
  004da7c4  83e2fd                and       edx,0FFFFFFFDh
  004da7c7  8916                  mov       [esi],edx
  004da7c9  8b542424              mov       edx,[esp+24h]
  004da7cd  894620                mov       [esi+20h],eax
  004da7d0  8b442448              mov       eax,[esp+48h]
  004da7d4  89561c                mov       [esi+1Ch],edx
  004da7d7  8b54245c              mov       edx,[esp+5Ch]
  004da7db  25ffff0000            and       eax,0FFFFh
  004da7e0  894e24                mov       [esi+24h],ecx
  004da7e3  8b4c243c              mov       ecx,[esp+3Ch]
  004da7e7  89567c                mov       [esi+7Ch],edx
  004da7ea  8b542458              mov       edx,[esp+58h]
  004da7ee  898690000000          mov       [esi+90h],eax
  004da7f4  8b44244c              mov       eax,[esp+4Ch]
  004da7f8  898e84000000          mov       [esi+84h],ecx
  004da7fe  8b4c2444              mov       ecx,[esp+44h]
  004da802  898680000000          mov       [esi+80h],eax
  004da808  8b442450              mov       eax,[esp+50h]
  004da80c  899688000000          mov       [esi+88h],edx
  004da812  8b542440              mov       edx,[esp+40h]
  004da816  6882ad0020            push      2000AD82h
  004da81b  56                    push      esi
  004da81c  89868c000000          mov       [esi+8Ch],eax
  004da822  894e64                mov       [esi+64h],ecx
  004da825  895660                mov       [esi+60h],edx
  004da828  e833b6ffff            call      004D5E60h
  004da82d  83c408                add       esp,8
  004da830  663bc3                cmp       ax,bx
  004da833  7515                  jne       short 004DA84Ah
  004da835  e806c90600            call      00547140h
  004da83a  99                    cdq
  004da83b  33c2                  xor       eax,edx
  004da83d  2bc2                  sub       eax,edx
  004da83f  83e001                and       eax,1
  004da842  33c2                  xor       eax,edx
  004da844  2bc2                  sub       eax,edx
  004da846  8d4400ff              lea       eax,[eax+eax-1]

LAB_004da84a:
  004da84a  8b4c2410              mov       ecx,[esp+10h]
  004da84e  83c140                add       ecx,40h
  004da851  0fafc8                imul      ecx,eax
  004da854  66014e32              add       [esi+32h],cx
  004da858  eb28                  jmp       short 004DA882h

LAB_004da85a:
  004da85a  8b542424              mov       edx,[esp+24h]
  004da85e  8b44242c              mov       eax,[esp+2Ch]
  004da862  895e5c                mov       [esi+5Ch],ebx
  004da865  895e50                mov       [esi+50h],ebx
  004da868  895e58                mov       [esi+58h],ebx
  004da86b  89561c                mov       [esi+1Ch],edx
  004da86e  894624                mov       [esi+24h],eax
  004da871  885e42                mov       [esi+42h],bl
  004da874  c6464328              mov       byte [esi+43h],28h
  004da878  66895e12              mov       [esi+12h],bx
  004da87c  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004da882:
  004da882  6aff                  push      0FFFFFFFFh
  004da884  6aff                  push      0FFFFFFFFh
  004da886  6aff                  push      0FFFFFFFFh
  004da888  56                    push      esi
  004da889  e8828ffeff            call      004C3810h
  004da88e  8b44242c              mov       eax,[esp+2Ch]
  004da892  83c410                add       esp,10h
  004da895  3bc3                  cmp       eax,ebx
  004da897  7517                  jne       short 004DA8B0h
  004da899  668b8c2482000000      mov       cx,[esp+82h]
  004da8a1  66894e32              mov       [esi+32h],cx
  004da8a5  5f                    pop       edi
  004da8a6  5e                    pop       esi
  004da8a7  5d                    pop       ebp
  004da8a8  5b                    pop       ebx
  004da8a9  81c484000000          add       esp,84h
  004da8af  c3                    ret

LAB_004da8b0:
  004da8b0  385e43                cmp       [esi+43h],bl
  004da8b3  0f8579030000          jne       near 004DAC32h
  004da8b9  8b466c                mov       eax,[esi+6Ch]
  004da8bc  8b561c                mov       edx,[esi+1Ch]
  004da8bf  8b781c                mov       edi,[eax+1Ch]
  004da8c2  8b6824                mov       ebp,[eax+24h]
  004da8c5  8b4624                mov       eax,[esi+24h]
  004da8c8  2bfa                  sub       edi,edx
  004da8ca  2be8                  sub       ebp,eax
  004da8cc  c1ff0c                sar       edi,0Ch
  004da8cf  c1fd0c                sar       ebp,0Ch
  004da8d2  8bd7                  mov       edx,edi
  004da8d4  8bc5                  mov       eax,ebp
  004da8d6  0fafd7                imul      edx,edi
  004da8d9  0fafc5                imul      eax,ebp
  004da8dc  03d0                  add       edx,eax
  004da8de  52                    push      edx
  004da8df  e82cb10600            call      00545A10h
  004da8e4  55                    push      ebp
  004da8e5  57                    push      edi
  004da8e6  8944241c              mov       [esp+1Ch],eax
  004da8ea  e833b10600            call      00545A22h
  004da8ef  668b4e32              mov       cx,[esi+32h]
  004da8f3  66895e12              mov       [esi+12h],bx
  004da8f7  2bc1                  sub       eax,ecx
  004da8f9  25ff0f0000            and       eax,0FFFh
  004da8fe  89442418              mov       [esp+18h],eax
  004da902  8b442414              mov       eax,[esp+14h]
  004da906  3dfa000000            cmp       eax,0FAh
  004da90b  0f8d11010000          jge       near 004DAA22h
  004da911  e82ac80600            call      00547140h
  004da916  99                    cdq
  004da917  b964000000            mov       ecx,64h
  004da91c  f7f9                  idiv      ecx
  004da91e  8bea                  mov       ebp,edx
  004da920  83fd32                cmp       ebp,32h
  004da923  0f8d90000000          jge       near 004DA9B9h
  004da929  8b542418              mov       edx,[esp+18h]
  004da92d  8b7c2420              mov       edi,[esp+20h]
  004da931  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004da938  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004da93f  25ff0f0000            and       eax,0FFFh
  004da944  3bc1                  cmp       eax,ecx
  004da946  7f67                  jg        short 004DA9AFh
  004da948  e8f3c70600            call      00547140h
  004da94d  99                    cdq
  004da94e  8d4c3f01              lea       ecx,[edi+edi+1]
  004da952  f7f9                  idiv      ecx
  004da954  8b442414              mov       eax,[esp+14h]
  004da958  83c264                add       edx,64h
  004da95b  3bc2                  cmp       eax,edx
  004da95d  7d50                  jge       short 004DA9AFh
  004da95f  83fd1e                cmp       ebp,1Eh
  004da962  7d27                  jge       short 004DA98Bh
  004da964  8b16                  mov       edx,[esi]
  004da966  c646420b              mov       byte [esi+42h],0Bh
  004da96a  81e2fffffffe          and       edx,0FEFFFFFFh
  004da970  c6464301              mov       byte [esi+43h],1
  004da974  83ca02                or        edx,2
  004da977  895e5c                mov       [esi+5Ch],ebx
  004da97a  8916                  mov       [esi],edx
  004da97c  66c746180300          mov       word [esi+18h],3
  004da982  66895e1a              mov       [esi+1Ah],bx
  004da986  e99b000000            jmp       004DAA26h

LAB_004da98b:
  004da98b  885e42                mov       [esi+42h],bl
  004da98e  e8adc70600            call      00547140h
  004da993  99                    cdq
  004da994  b928000000            mov       ecx,28h
  004da999  66c746105000          mov       word [esi+10h],50h
  004da99f  f7f9                  idiv      ecx
  004da9a1  8bc7                  mov       eax,edi
  004da9a3  d1f8                  sar       eax,1
  004da9a5  02d0                  add       dl,al
  004da9a7  80c214                add       dl,14h
  004da9aa  885643                mov       [esi+43h],dl
  004da9ad  eb77                  jmp       short 004DAA26h

LAB_004da9af:
  004da9af  c6464201              mov       byte [esi+42h],1
  004da9b3  c6464301              mov       byte [esi+43h],1
  004da9b7  eb6d                  jmp       short 004DAA26h

LAB_004da9b9:
  004da9b9  83fd3c                cmp       ebp,3Ch
  004da9bc  7d3e                  jge       short 004DA9FCh
  004da9be  c6464203              mov       byte [esi+42h],3
  004da9c2  66895e12              mov       [esi+12h],bx
  004da9c6  e875c70600            call      00547140h
  004da9cb  99                    cdq
  004da9cc  b90a000000            mov       ecx,0Ah
  004da9d1  8b7c2420              mov       edi,[esp+20h]
  004da9d5  f7f9                  idiv      ecx
  004da9d7  8bc7                  mov       eax,edi
  004da9d9  d1f8                  sar       eax,1
  004da9db  02d0                  add       dl,al
  004da9dd  80c214                add       dl,14h
  004da9e0  885643                mov       [esi+43h],dl
  004da9e3  e858c70600            call      00547140h
  004da9e8  99                    cdq
  004da9e9  33c2                  xor       eax,edx
  004da9eb  2bc2                  sub       eax,edx
  004da9ed  25ff0f0000            and       eax,0FFFh
  004da9f2  33c2                  xor       eax,edx
  004da9f4  2bc2                  sub       eax,edx
  004da9f6  66894610              mov       [esi+10h],ax
  004da9fa  eb2a                  jmp       short 004DAA26h

LAB_004da9fc:
  004da9fc  83fd46                cmp       ebp,46h
  004da9ff  7d15                  jge       short 004DAA16h
  004daa01  8b4c2410              mov       ecx,[esp+10h]
  004daa05  8b442414              mov       eax,[esp+14h]
  004daa09  83c164                add       ecx,64h
  004daa0c  3bc1                  cmp       eax,ecx
  004daa0e  7e06                  jle       short 004DAA16h
  004daa10  c646420c              mov       byte [esi+42h],0Ch
  004daa14  eb04                  jmp       short 004DAA1Ah

LAB_004daa16:
  004daa16  c646420a              mov       byte [esi+42h],0Ah

LAB_004daa1a:
  004daa1a  c6464301              mov       byte [esi+43h],1
  004daa1e  66895e12              mov       [esi+12h],bx

LAB_004daa22:
  004daa22  8b7c2420              mov       edi,[esp+20h]

LAB_004daa26:
  004daa26  8b442414              mov       eax,[esp+14h]
  004daa2a  3d58020000            cmp       eax,258h
  004daa2f  0f8dd9000000          jge       near 004DAB0Eh
  004daa35  3dfa000000            cmp       eax,0FAh
  004daa3a  0f8cce000000          jl        near 004DAB0Eh
  004daa40  e8fbc60600            call      00547140h
  004daa45  99                    cdq
  004daa46  b964000000            mov       ecx,64h
  004daa4b  f7f9                  idiv      ecx
  004daa4d  83fa14                cmp       edx,14h
  004daa50  7d25                  jge       short 004DAA77h
  004daa52  8b542418              mov       edx,[esp+18h]
  004daa56  81c200040000          add       edx,400h
  004daa5c  81e2ff0f0000          and       edx,0FFFh
  004daa62  81fa00080000          cmp       edx,800h
  004daa68  7c3c                  jl        short 004DAAA6h
  004daa6a  c6464201              mov       byte [esi+42h],1
  004daa6e  c6464301              mov       byte [esi+43h],1
  004daa72  e997000000            jmp       004DAB0Eh

LAB_004daa77:
  004daa77  83fa28                cmp       edx,28h
  004daa7a  7d25                  jge       short 004DAAA1h
  004daa7c  c6464202              mov       byte [esi+42h],2
  004daa80  e8bbc60600            call      00547140h
  004daa85  99                    cdq
  004daa86  b90a000000            mov       ecx,0Ah
  004daa8b  66c746105000          mov       word [esi+10h],50h
  004daa91  f7f9                  idiv      ecx
  004daa93  8bc7                  mov       eax,edi
  004daa95  d1f8                  sar       eax,1
  004daa97  02d0                  add       dl,al
  004daa99  80c214                add       dl,14h
  004daa9c  885643                mov       [esi+43h],dl
  004daa9f  eb6d                  jmp       short 004DAB0Eh

LAB_004daaa1:
  004daaa1  83fa46                cmp       edx,46h
  004daaa4  7d0e                  jge       short 004DAAB4h

LAB_004daaa6:
  004daaa6  c646420a              mov       byte [esi+42h],0Ah
  004daaaa  c6464301              mov       byte [esi+43h],1
  004daaae  66895e12              mov       [esi+12h],bx
  004daab2  eb5a                  jmp       short 004DAB0Eh

LAB_004daab4:
  004daab4  8b4c2410              mov       ecx,[esp+10h]
  004daab8  b864000000            mov       eax,64h
  004daabd  d1f9                  sar       ecx,1
  004daabf  83c10a                add       ecx,0Ah
  004daac2  2bc2                  sub       eax,edx
  004daac4  3bc1                  cmp       eax,ecx
  004daac6  7d0e                  jge       short 004DAAD6h
  004daac8  c646420c              mov       byte [esi+42h],0Ch
  004daacc  c6464301              mov       byte [esi+43h],1
  004daad0  66895e12              mov       [esi+12h],bx
  004daad4  eb38                  jmp       short 004DAB0Eh

LAB_004daad6:
  004daad6  c6464203              mov       byte [esi+42h],3
  004daada  66895e12              mov       [esi+12h],bx
  004daade  e85dc60600            call      00547140h
  004daae3  99                    cdq
  004daae4  b90a000000            mov       ecx,0Ah
  004daae9  f7f9                  idiv      ecx
  004daaeb  8bc7                  mov       eax,edi
  004daaed  d1f8                  sar       eax,1
  004daaef  02d0                  add       dl,al
  004daaf1  80c214                add       dl,14h
  004daaf4  885643                mov       [esi+43h],dl
  004daaf7  e844c60600            call      00547140h
  004daafc  99                    cdq
  004daafd  33c2                  xor       eax,edx
  004daaff  2bc2                  sub       eax,edx
  004dab01  25ff0f0000            and       eax,0FFFh
  004dab06  33c2                  xor       eax,edx
  004dab08  2bc2                  sub       eax,edx
  004dab0a  66894610              mov       [esi+10h],ax

LAB_004dab0e:
  004dab0e  817c241458020000      cmp       dword [esp+14h],258h
  004dab16  0f8c16010000          jl        near 004DAC32h
  004dab1c  e81fc60600            call      00547140h
  004dab21  99                    cdq
  004dab22  b964000000            mov       ecx,64h
  004dab27  f7f9                  idiv      ecx
  004dab29  83fa1e                cmp       edx,1Eh
  004dab2c  7d35                  jge       short 004DAB63h
  004dab2e  885e42                mov       [esi+42h],bl
  004dab31  e80ac60600            call      00547140h
  004dab36  99                    cdq
  004dab37  b90a000000            mov       ecx,0Ah
  004dab3c  66895e12              mov       [esi+12h],bx
  004dab40  f7f9                  idiv      ecx
  004dab42  8b4c2410              mov       ecx,[esp+10h]
  004dab46  8bc7                  mov       eax,edi
  004dab48  c1f802                sar       eax,2
  004dab4b  02d0                  add       dl,al
  004dab4d  885643                mov       [esi+43h],dl
  004dab50  8d540950              lea       edx,[ecx+ecx+50h]
  004dab54  66895610              mov       [esi+10h],dx
  004dab58  5f                    pop       edi
  004dab59  5e                    pop       esi
  004dab5a  5d                    pop       ebp
  004dab5b  5b                    pop       ebx
  004dab5c  81c484000000          add       esp,84h
  004dab62  c3                    ret

LAB_004dab63:
  004dab63  83fa28                cmp       edx,28h
  004dab66  7d43                  jge       short 004DABABh
  004dab68  c6464203              mov       byte [esi+42h],3
  004dab6c  66895e12              mov       [esi+12h],bx
  004dab70  e8cbc50600            call      00547140h
  004dab75  99                    cdq
  004dab76  b90a000000            mov       ecx,0Ah
  004dab7b  f7f9                  idiv      ecx
  004dab7d  8bc7                  mov       eax,edi
  004dab7f  d1f8                  sar       eax,1
  004dab81  02d0                  add       dl,al
  004dab83  80c214                add       dl,14h
  004dab86  885643                mov       [esi+43h],dl
  004dab89  e8b2c50600            call      00547140h
  004dab8e  99                    cdq
  004dab8f  33c2                  xor       eax,edx
  004dab91  2bc2                  sub       eax,edx
  004dab93  25ff0f0000            and       eax,0FFFh
  004dab98  33c2                  xor       eax,edx
  004dab9a  2bc2                  sub       eax,edx
  004dab9c  66894610              mov       [esi+10h],ax
  004daba0  5f                    pop       edi
  004daba1  5e                    pop       esi
  004daba2  5d                    pop       ebp
  004daba3  5b                    pop       ebx
  004daba4  81c484000000          add       esp,84h
  004dabaa  c3                    ret

LAB_004dabab:
  004dabab  83fa3c                cmp       edx,3Ch
  004dabae  7d13                  jge       short 004DABC3h
  004dabb0  c6464201              mov       byte [esi+42h],1
  004dabb4  c6464301              mov       byte [esi+43h],1
  004dabb8  5f                    pop       edi
  004dabb9  5e                    pop       esi
  004dabba  5d                    pop       ebp
  004dabbb  5b                    pop       ebx
  004dabbc  81c484000000          add       esp,84h
  004dabc2  c3                    ret

LAB_004dabc3:
  004dabc3  83fa50                cmp       edx,50h
  004dabc6  7d59                  jge       short 004DAC21h
  004dabc8  8b4c2410              mov       ecx,[esp+10h]
  004dabcc  b864000000            mov       eax,64h
  004dabd1  d1f9                  sar       ecx,1
  004dabd3  83c10a                add       ecx,0Ah
  004dabd6  2bc2                  sub       eax,edx
  004dabd8  3bc1                  cmp       eax,ecx
  004dabda  7d17                  jge       short 004DABF3h
  004dabdc  c646420a              mov       byte [esi+42h],0Ah
  004dabe0  c6464301              mov       byte [esi+43h],1
  004dabe4  66895e12              mov       [esi+12h],bx
  004dabe8  5f                    pop       edi
  004dabe9  5e                    pop       esi
  004dabea  5d                    pop       ebp
  004dabeb  5b                    pop       ebx
  004dabec  81c484000000          add       esp,84h
  004dabf2  c3                    ret

LAB_004dabf3:
  004dabf3  c6464202              mov       byte [esi+42h],2
  004dabf7  e844c50600            call      00547140h
  004dabfc  99                    cdq
  004dabfd  b90a000000            mov       ecx,0Ah
  004dac02  66c746106400          mov       word [esi+10h],64h
  004dac08  f7f9                  idiv      ecx
  004dac0a  8bc7                  mov       eax,edi
  004dac0c  d1f8                  sar       eax,1
  004dac0e  02d0                  add       dl,al
  004dac10  80c214                add       dl,14h
  004dac13  885643                mov       [esi+43h],dl
  004dac16  5f                    pop       edi
  004dac17  5e                    pop       esi
  004dac18  5d                    pop       ebp
  004dac19  5b                    pop       ebx
  004dac1a  81c484000000          add       esp,84h
  004dac20  c3                    ret

LAB_004dac21:
  004dac21  c6464206              mov       byte [esi+42h],6
  004dac25  c6464301              mov       byte [esi+43h],1
  004dac29  e812c50600            call      00547140h
  004dac2e  66894610              mov       [esi+10h],ax

LAB_004dac32:
  004dac32  5f                    pop       edi
  004dac33  5e                    pop       esi
  004dac34  5d                    pop       ebp
  004dac35  5b                    pop       ebx
  004dac36  81c484000000          add       esp,84h
  004dac3c  c3                    ret

74 cible(s) de saut interne : 0x004d9df8, 0x004d9f49, 0x004d9f6b, 0x004d9fda, 0x004da03a, 0x004da03e, 0x004da058, 0x004da05d, 0x004da094, 0x004da0dd, 0x004da101, 0x004da122, 0x004da16f, 0x004da18f, 0x004da19b, 0x004da1d2, 0x004da1f9, 0x004da278, 0x004da286, 0x004da28a, 0x004da2d2, 0x004da345, 0x004da371, 0x004da37d, 0x004da3ac, 0x004da3c5, 0x004da3d6, 0x004da3fb, 0x004da477, 0x004da47c, 0x004da4a5, 0x004da4de, 0x004da51f, 0x004da549, 0x004da55a, 0x004da56f, 0x004da581, 0x004da5ac, 0x004da5f4, 0x004da613, 0x004da61b, 0x004da61f, 0x004da661, 0x004da6d0, 0x004da6ed, 0x004da759, 0x004da764, 0x004da766, 0x004da77c, 0x004da7ba, 0x004da84a, 0x004da85a, 0x004da882, 0x004da8b0, 0x004da98b, 0x004da9af, 0x004da9b9, 0x004da9fc, 0x004daa16, 0x004daa1a, 0x004daa22, 0x004daa26, 0x004daa77, 0x004daaa1, 0x004daaa6, 0x004daab4, 0x004daad6, 0x004dab0e, 0x004dab63, 0x004dabab, 0x004dabc3, 0x004dabf3, 0x004dac21, 0x004dac32
