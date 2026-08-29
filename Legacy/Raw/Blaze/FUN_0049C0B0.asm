C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049c0b0  section .text  offset fichier 0x9b4b0  2096 octets

  0049c0b0  83ec38                sub       esp,38h
  0049c0b3  53                    push      ebx
  0049c0b4  55                    push      ebp
  0049c0b5  56                    push      esi
  0049c0b6  8b742448              mov       esi,[esp+48h]
  0049c0ba  8d44241c              lea       eax,[esp+1Ch]
  0049c0be  57                    push      edi
  0049c0bf  8d4c2438              lea       ecx,[esp+38h]
  0049c0c3  50                    push      eax
  0049c0c4  8d542438              lea       edx,[esp+38h]
  0049c0c8  51                    push      ecx
  0049c0c9  33db                  xor       ebx,ebx
  0049c0cb  52                    push      edx
  0049c0cc  56                    push      esi
  0049c0cd  66895c244c            mov       [esp+4Ch],bx
  0049c0d2  66c744244e3200        mov       word [esp+4Eh],32h
  0049c0d9  66c74424503c00        mov       word [esp+50h],3Ch
  0049c0e0  66c74424526400        mov       word [esp+52h],64h
  0049c0e7  66895c2454            mov       [esp+54h],bx
  0049c0ec  66c74424565000        mov       word [esp+56h],50h
  0049c0f3  e888c80900            call      00538980h
  0049c0f8  83c410                add       esp,10h
  0049c0fb  8d442424              lea       eax,[esp+24h]
  0049c0ff  8d4c241c              lea       ecx,[esp+1Ch]
  0049c103  8d542410              lea       edx,[esp+10h]
  0049c107  50                    push      eax
  0049c108  51                    push      ecx
  0049c109  8d442420              lea       eax,[esp+20h]
  0049c10d  52                    push      edx
  0049c10e  8d4c2458              lea       ecx,[esp+58h]
  0049c112  50                    push      eax
  0049c113  51                    push      ecx
  0049c114  56                    push      esi
  0049c115  e8069e0900            call      00535F20h
  0049c11a  8b4c2464              mov       ecx,[esp+64h]
  0049c11e  b840000000            mov       eax,40h
  0049c123  2bc1                  sub       eax,ecx
  0049c125  83c418                add       esp,18h
  0049c128  89442414              mov       [esp+14h],eax
  0049c12c  8b44241c              mov       eax,[esp+1Ch]
  0049c130  3bc3                  cmp       eax,ebx
  0049c132  bf01000000            mov       edi,1
  0049c137  0f849a040000          je        near 0049C5D7h
  0049c13d  56                    push      esi
  0049c13e  e85df40300            call      004DB5A0h
  0049c143  8b06                  mov       eax,[esi]
  0049c145  83c404                add       esp,4
  0049c148  a802                  test      al,2
  0049c14a  0f849d020000          je        near 0049C3EDh
  0049c150  8a4c2424              mov       cl,[esp+24h]
  0049c154  bb10000000            mov       ebx,10h
  0049c159  84cb                  test      bl,cl
  0049c15b  740d                  je        short 0049C16Ah
  0049c15d  24fd                  and       al,0FDh
  0049c15f  c6464300              mov       byte [esi+43h],0
  0049c163  8906                  mov       [esi],eax
  0049c165  e96d040000            jmp       0049C5D7h

LAB_0049c16a:
  0049c16a  33c9                  xor       ecx,ecx
  0049c16c  8a4e42                mov       cl,[esi+42h]
  0049c16f  83e900                sub       ecx,0
  0049c172  0f84f9010000          je        near 0049C371h
  0049c178  49                    dec       ecx
  0049c179  0f8558040000          jne       near 0049C5D7h
  0049c17f  668b5610              mov       dx,[esi+10h]
  0049c183  0fbfca                movsx     ecx,dx
  0049c186  83e900                sub       ecx,0
  0049c189  0f84b2000000          je        near 0049C241h
  0049c18f  49                    dec       ecx
  0049c190  745e                  je        short 0049C1F0h
  0049c192  49                    dec       ecx
  0049c193  0f853e040000          jne       near 0049C5D7h
  0049c199  8b465c                mov       eax,[esi+5Ch]
  0049c19c  8bc8                  mov       ecx,eax
  0049c19e  c1f904                sar       ecx,4
  0049c1a1  2bc1                  sub       eax,ecx
  0049c1a3  66837e1a0e            cmp       word [esi+1Ah],0Eh
  0049c1a8  89465c                mov       [esi+5Ch],eax
  0049c1ab  7505                  jne       short 0049C1B2h
  0049c1ad  4a                    dec       edx
  0049c1ae  66895610              mov       [esi+10h],dx

LAB_0049c1b2:
  0049c1b2  8b44244c              mov       eax,[esp+4Ch]
  0049c1b6  8b4c2410              mov       ecx,[esp+10h]
  0049c1ba  c1f802                sar       eax,2
  0049c1bd  83c020                add       eax,20h
  0049c1c0  8d1408                lea       edx,[eax+ecx]
  0049c1c3  8d3c00                lea       edi,[eax+eax]
  0049c1c6  81e2ff0f0000          and       edx,0FFFh
  0049c1cc  3bd7                  cmp       edx,edi
  0049c1ce  7f09                  jg        short 0049C1D9h
  0049c1d0  66014e32              add       [esi+32h],cx
  0049c1d4  e9fe030000            jmp       0049C5D7h

LAB_0049c1d9:
  0049c1d9  81e1ff0f0000          and       ecx,0FFFh
  0049c1df  81f900080000          cmp       ecx,800h
  0049c1e5  0f8ce8030000          jl        near 0049C5D3h
  0049c1eb  e9e1030000            jmp       0049C5D1h

LAB_0049c1f0:
  0049c1f0  8b44244c              mov       eax,[esp+4Ch]
  0049c1f4  0fbf4e32              movsx     ecx,word [esi+32h]
  0049c1f8  0580030000            add       eax,380h
  0049c1fd  51                    push      ecx
  0049c1fe  c1e007                shl       eax,7
  0049c201  89465c                mov       [esi+5Ch],eax
  0049c204  e813980a00            call      00545A1Ch
  0049c209  0faf465c              imul      eax,[esi+5Ch]
  0049c20d  0fbf5632              movsx     edx,word [esi+32h]
  0049c211  8b5e50                mov       ebx,[esi+50h]
  0049c214  52                    push      edx
  0049c215  c1f80c                sar       eax,0Ch
  0049c218  03d8                  add       ebx,eax
  0049c21a  895e50                mov       [esi+50h],ebx
  0049c21d  e8f4970a00            call      00545A16h
  0049c222  0faf465c              imul      eax,[esi+5Ch]
  0049c226  8b4e58                mov       ecx,[esi+58h]
  0049c229  c7465c00000000        mov       dword [esi+5Ch],0
  0049c230  c1f80c                sar       eax,0Ch
  0049c233  03c8                  add       ecx,eax
  0049c235  66ff4e10              dec       word [esi+10h]
  0049c239  894e58                mov       [esi+58h],ecx
  0049c23c  e996030000            jmp       0049C5D7h

LAB_0049c241:
  0049c241  a900000002            test      eax,2000000h
  0049c246  7412                  je        short 0049C25Ah
  0049c248  8a4e43                mov       cl,[esi+43h]
  0049c24b  84c9                  test      cl,cl
  0049c24d  740b                  je        short 0049C25Ah
  0049c24f  fec9                  dec       cl
  0049c251  884e43                mov       [esi+43h],cl
  0049c254  7504                  jne       short 0049C25Ah
  0049c256  24dd                  and       al,0DDh
  0049c258  8906                  mov       [esi],eax

LAB_0049c25a:
  0049c25a  f60620                test      byte [esi],20h
  0049c25d  0f8574030000          jne       near 0049C5D7h
  0049c263  8d44243c              lea       eax,[esp+3Ch]
  0049c267  8d6e1c                lea       ebp,[esi+1Ch]
  0049c26a  6a00                  push      0
  0049c26c  50                    push      eax
  0049c26d  55                    push      ebp
  0049c26e  56                    push      esi
  0049c26f  e8bcd90700            call      00519C30h
  0049c274  8bf8                  mov       edi,eax
  0049c276  83c410                add       esp,10h
  0049c279  85ff                  test      edi,edi
  0049c27b  7406                  je        short 0049C283h
  0049c27d  8b06                  mov       eax,[esi]
  0049c27f  0c20                  or        al,20h
  0049c281  8906                  mov       [esi],eax

LAB_0049c283:
  0049c283  a1b8e05d00            mov       eax,[5DE0B8h]
  0049c288  85c0                  test      eax,eax
  0049c28a  0f8447030000          je        near 0049C5D7h
  0049c290  66899f96000000        mov       [edi+96h],bx
  0049c297  8b1f                  mov       ebx,[edi]
  0049c299  83cb40                or        ebx,40h
  0049c29c  57                    push      edi
  0049c29d  891f                  mov       [edi],ebx
  0049c29f  8b4c2424              mov       ecx,[esp+24h]
  0049c2a3  56                    push      esi
  0049c2a4  668b4130              mov       ax,[ecx+30h]
  0049c2a8  0fbfd8                movsx     ebx,ax
  0049c2ab  66d1f8                sar       ax,1
  0049c2ae  66894130              mov       [ecx+30h],ax
  0049c2b2  e8c9d80900            call      00539B80h
  0049c2b7  8b4c2428              mov       ecx,[esp+28h]
  0049c2bb  83c408                add       esp,8
  0049c2be  66895930              mov       [ecx+30h],bx
  0049c2c2  8d5f1c                lea       ebx,[edi+1Ch]
  0049c2c5  53                    push      ebx
  0049c2c6  6800180000            push      1800h
  0049c2cb  6800030000            push      300h
  0049c2d0  6a03                  push      3
  0049c2d2  e849180a00            call      0053DB20h
  0049c2d7  8b07                  mov       eax,[edi]
  0049c2d9  83c410                add       esp,10h
  0049c2dc  a900000040            test      eax,40000000h
  0049c2e1  0f85f0020000          jne       near 0049C5D7h
  0049c2e7  0fbf5632              movsx     edx,word [esi+32h]
  0049c2eb  52                    push      edx
  0049c2ec  e82b970a00            call      00545A1Ch
  0049c2f1  8d0440                lea       eax,[eax+eax*2]
  0049c2f4  8d0480                lea       eax,[eax+eax*4]
  0049c2f7  d1e0                  shl       eax,1
  0049c2f9  894750                mov       [edi+50h],eax
  0049c2fc  0fbf4e32              movsx     ecx,word [esi+32h]
  0049c300  51                    push      ecx
  0049c301  e810970a00            call      00545A16h
  0049c306  8d0440                lea       eax,[eax+eax*2]
  0049c309  8d1480                lea       edx,[eax+eax*4]
  0049c30c  8b07                  mov       eax,[edi]
  0049c30e  d1e2                  shl       edx,1
  0049c310  a801                  test      al,1
  0049c312  895758                mov       [edi+58h],edx
  0049c315  750b                  jne       short 0049C322h
  0049c317  0c01                  or        al,1
  0049c319  c7475400800100        mov       dword [edi+54h],18000h
  0049c320  8907                  mov       [edi],eax

LAB_0049c322:
  0049c322  8b4500                mov       eax,[ebp]
  0049c325  8b4e20                mov       ecx,[esi+20h]
  0049c328  8b5624                mov       edx,[esi+24h]
  0049c32b  89442428              mov       [esp+28h],eax
  0049c32f  8b03                  mov       eax,[ebx]
  0049c331  894c242c              mov       [esp+2Ch],ecx
  0049c335  894500                mov       [ebp],eax
  0049c338  8b4f20                mov       ecx,[edi+20h]
  0049c33b  6aff                  push      0FFFFFFFFh
  0049c33d  89542434              mov       [esp+34h],edx
  0049c341  894e20                mov       [esi+20h],ecx
  0049c344  8b5724                mov       edx,[edi+24h]
  0049c347  6a00                  push      0
  0049c349  6a39                  push      39h
  0049c34b  56                    push      esi
  0049c34c  895624                mov       [esi+24h],edx
  0049c34f  e87ce6fdff            call      0047A9D0h
  0049c354  8b442438              mov       eax,[esp+38h]
  0049c358  8b4c243c              mov       ecx,[esp+3Ch]
  0049c35c  8b542440              mov       edx,[esp+40h]
  0049c360  83c410                add       esp,10h
  0049c363  894500                mov       [ebp],eax
  0049c366  894e20                mov       [esi+20h],ecx
  0049c369  895624                mov       [esi+24h],edx
  0049c36c  e966020000            jmp       0049C5D7h

LAB_0049c371:
  0049c371  668b461a              mov       ax,[esi+1Ah]
  0049c375  663d0c00              cmp       ax,0Ch
  0049c379  7c55                  jl        short 0049C3D0h
  0049c37b  663d0f00              cmp       ax,0Fh
  0049c37f  7f4f                  jg        short 0049C3D0h
  0049c381  8d44243c              lea       eax,[esp+3Ch]
  0049c385  6a00                  push      0
  0049c387  8d4e1c                lea       ecx,[esi+1Ch]
  0049c38a  50                    push      eax
  0049c38b  51                    push      ecx
  0049c38c  56                    push      esi
  0049c38d  e89ed80700            call      00519C30h
  0049c392  8bf8                  mov       edi,eax
  0049c394  a1b8e05d00            mov       eax,[5DE0B8h]
  0049c399  83c410                add       esp,10h
  0049c39c  85c0                  test      eax,eax
  0049c39e  7430                  je        short 0049C3D0h
  0049c3a0  8b17                  mov       edx,[edi]
  0049c3a2  57                    push      edi
  0049c3a3  83ca40                or        edx,40h
  0049c3a6  56                    push      esi
  0049c3a7  66899f96000000        mov       [edi+96h],bx
  0049c3ae  8917                  mov       [edi],edx
  0049c3b0  e8cbd70900            call      00539B80h
  0049c3b5  83c408                add       esp,8
  0049c3b8  83c71c                add       edi,1Ch
  0049c3bb  57                    push      edi
  0049c3bc  6800180000            push      1800h
  0049c3c1  6800030000            push      300h
  0049c3c6  6a03                  push      3
  0049c3c8  e853170a00            call      0053DB20h
  0049c3cd  83c410                add       esp,10h

LAB_0049c3d0:
  0049c3d0  8b06                  mov       eax,[esi]
  0049c3d2  a900000002            test      eax,2000000h
  0049c3d7  0f84c8000000          je        near 0049C4A5h
  0049c3dd  24fd                  and       al,0FDh
  0049c3df  c7465c00000000        mov       dword [esi+5Ch],0
  0049c3e6  8906                  mov       [esi],eax
  0049c3e8  e9ea010000            jmp       0049C5D7h

LAB_0049c3ed:
  0049c3ed  a811                  test      al,11h
  0049c3ef  0f85e2010000          jne       near 0049C5D7h
  0049c3f5  a904080000            test      eax,804h
  0049c3fa  0f85d7010000          jne       near 0049C5D7h
  0049c400  33c9                  xor       ecx,ecx
  0049c402  8a4e42                mov       cl,[esi+42h]
  0049c405  83f903                cmp       ecx,3
  0049c408  0f87c9010000          ja        near 0049C5D7h
  0049c40e  ff248dccc84900        jmp       dword [ecx*4+49C8CCh]
  0049c415  33c9                  xor       ecx,ecx
  0049c417  66394e18              cmp       [esi+18h],cx
  0049c41b  740a                  je        short 0049C427h
  0049c41d  0d00000001            or        eax,1000000h
  0049c422  894e18                mov       [esi+18h],ecx
  0049c425  8906                  mov       [esi],eax

LAB_0049c427:
  0049c427  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049c42f  7d09                  jge       short 0049C43Ah
  0049c431  c6464300              mov       byte [esi+43h],0
  0049c435  e99d010000            jmp       0049C5D7h

LAB_0049c43a:
  0049c43a  8a4643                mov       al,[esi+43h]
  0049c43d  84c0                  test      al,al
  0049c43f  7405                  je        short 0049C446h
  0049c441  fec8                  dec       al
  0049c443  884643                mov       [esi+43h],al

LAB_0049c446:
  0049c446  894e5c                mov       [esi+5Ch],ecx
  0049c449  e989010000            jmp       0049C5D7h
  0049c44e  66397e18              cmp       [esi+18h],di
  0049c452  740a                  je        short 0049C45Eh
  0049c454  0d00000001            or        eax,1000000h
  0049c459  897e18                mov       [esi+18h],edi
  0049c45c  8906                  mov       [esi],eax

LAB_0049c45e:
  0049c45e  8b54244c              mov       edx,[esp+4Ch]
  0049c462  8b4c2410              mov       ecx,[esp+10h]
  0049c466  8d841280000000        lea       eax,[edx+edx+80h]
  0049c46d  8d1408                lea       edx,[eax+ecx]
  0049c470  8d3c00                lea       edi,[eax+eax]
  0049c473  81e2ff0f0000          and       edx,0FFFh
  0049c479  3bd7                  cmp       edx,edi
  0049c47b  7f14                  jg        short 0049C491h
  0049c47d  66014e32              add       [esi+32h],cx
  0049c481  c6464300              mov       byte [esi+43h],0
  0049c485  c7465c00000000        mov       dword [esi+5Ch],0
  0049c48c  e946010000            jmp       0049C5D7h

LAB_0049c491:
  0049c491  81e1ff0f0000          and       ecx,0FFFh
  0049c497  81f900080000          cmp       ecx,800h
  0049c49d  7c02                  jl        short 0049C4A1h
  0049c49f  f7d8                  neg       eax

LAB_0049c4a1:
  0049c4a1  66014632              add       [esi+32h],ax

LAB_0049c4a5:
  0049c4a5  c7465c00000000        mov       dword [esi+5Ch],0
  0049c4ac  e926010000            jmp       0049C5D7h
  0049c4b1  66397e18              cmp       [esi+18h],di
  0049c4b5  740a                  je        short 0049C4C1h
  0049c4b7  0d00000001            or        eax,1000000h
  0049c4bc  897e18                mov       [esi+18h],edi
  0049c4bf  8906                  mov       [esi],eax

LAB_0049c4c1:
  0049c4c1  0fbf4610              movsx     eax,word [esi+10h]
  0049c4c5  39442418              cmp       [esp+18h],eax
  0049c4c9  7d09                  jge       short 0049C4D4h
  0049c4cb  c6464300              mov       byte [esi+43h],0
  0049c4cf  e903010000            jmp       0049C5D7h

LAB_0049c4d4:
  0049c4d4  8a4643                mov       al,[esi+43h]
  0049c4d7  84c0                  test      al,al
  0049c4d9  7405                  je        short 0049C4E0h
  0049c4db  fec8                  dec       al
  0049c4dd  884643                mov       [esi+43h],al

LAB_0049c4e0:
  0049c4e0  8b54244c              mov       edx,[esp+4Ch]
  0049c4e4  8b4c2410              mov       ecx,[esp+10h]
  0049c4e8  8d841280000000        lea       eax,[edx+edx+80h]
  0049c4ef  8d3c08                lea       edi,[eax+ecx]
  0049c4f2  8d1c00                lea       ebx,[eax+eax]
  0049c4f5  81e7ff0f0000          and       edi,0FFFh
  0049c4fb  3bfb                  cmp       edi,ebx
  0049c4fd  7f15                  jg        short 0049C514h
  0049c4ff  66014e32              add       [esi+32h],cx
  0049c503  81c2c0000000          add       edx,0C0h
  0049c509  c1e207                shl       edx,7
  0049c50c  89565c                mov       [esi+5Ch],edx
  0049c50f  e9c3000000            jmp       0049C5D7h

LAB_0049c514:
  0049c514  81e1ff0f0000          and       ecx,0FFFh
  0049c51a  81f900080000          cmp       ecx,800h
  0049c520  7c02                  jl        short 0049C524h
  0049c522  f7d8                  neg       eax

LAB_0049c524:
  0049c524  66014632              add       [esi+32h],ax
  0049c528  81c2c0000000          add       edx,0C0h
  0049c52e  c1e207                shl       edx,7
  0049c531  89565c                mov       [esi+5Ch],edx
  0049c534  e99e000000            jmp       0049C5D7h
  0049c539  8a4e43                mov       cl,[esi+43h]
  0049c53c  84c9                  test      cl,cl
  0049c53e  7405                  je        short 0049C545h
  0049c540  fec9                  dec       cl
  0049c542  884e43                mov       [esi+43h],cl

LAB_0049c545:
  0049c545  66397e18              cmp       [esi+18h],di
  0049c549  740a                  je        short 0049C555h
  0049c54b  0d00000001            or        eax,1000000h
  0049c550  897e18                mov       [esi+18h],edi
  0049c553  8906                  mov       [esi],eax

LAB_0049c555:
  0049c555  8b4c244c              mov       ecx,[esp+4Ch]
  0049c559  0fbf5610              movsx     edx,word [esi+10h]
  0049c55d  81c180000000          add       ecx,80h
  0049c563  52                    push      edx
  0049c564  c1e107                shl       ecx,7
  0049c567  894e5c                mov       [esi+5Ch],ecx
  0049c56a  e8ad940a00            call      00545A1Ch
  0049c56f  0faf465c              imul      eax,[esi+5Ch]
  0049c573  8b5e1c                mov       ebx,[esi+1Ch]
  0049c576  c1f80c                sar       eax,0Ch
  0049c579  03d8                  add       ebx,eax
  0049c57b  0fbf4610              movsx     eax,word [esi+10h]
  0049c57f  50                    push      eax
  0049c580  895e1c                mov       [esi+1Ch],ebx
  0049c583  e88e940a00            call      00545A16h
  0049c588  0faf465c              imul      eax,[esi+5Ch]
  0049c58c  8b4e24                mov       ecx,[esi+24h]
  0049c58f  c7465c00000000        mov       dword [esi+5Ch],0
  0049c596  c1f80c                sar       eax,0Ch
  0049c599  03c8                  add       ecx,eax
  0049c59b  894e24                mov       [esi+24h],ecx
  0049c59e  8b4c244c              mov       ecx,[esp+4Ch]
  0049c5a2  8d048d32000000        lea       eax,[ecx*4+32h]
  0049c5a9  8b4c2410              mov       ecx,[esp+10h]
  0049c5ad  8d1408                lea       edx,[eax+ecx]
  0049c5b0  8d3c00                lea       edi,[eax+eax]
  0049c5b3  81e2ff0f0000          and       edx,0FFFh
  0049c5b9  3bd7                  cmp       edx,edi
  0049c5bb  7f06                  jg        short 0049C5C3h
  0049c5bd  66014e32              add       [esi+32h],cx
  0049c5c1  eb14                  jmp       short 0049C5D7h

LAB_0049c5c3:
  0049c5c3  81e1ff0f0000          and       ecx,0FFFh
  0049c5c9  81f900080000          cmp       ecx,800h
  0049c5cf  7c02                  jl        short 0049C5D3h

LAB_0049c5d1:
  0049c5d1  f7d8                  neg       eax

LAB_0049c5d3:
  0049c5d3  66014632              add       [esi+32h],ax

LAB_0049c5d7:
  0049c5d7  8b44241c              mov       eax,[esp+1Ch]
  0049c5db  50                    push      eax
  0049c5dc  56                    push      esi
  0049c5dd  e87ebbfeff            call      00488160h
  0049c5e2  83c408                add       esp,8
  0049c5e5  56                    push      esi
  0049c5e6  e855730900            call      00533940h
  0049c5eb  83c404                add       esp,4
  0049c5ee  56                    push      esi
  0049c5ef  e8fc340400            call      004DFAF0h
  0049c5f4  8b4c2438              mov       ecx,[esp+38h]
  0049c5f8  83c404                add       esp,4
  0049c5fb  51                    push      ecx
  0049c5fc  56                    push      esi
  0049c5fd  e86e710400            call      004E3770h
  0049c602  8b442424              mov       eax,[esp+24h]
  0049c606  83c408                add       esp,8
  0049c609  85c0                  test      eax,eax
  0049c60b  0f84b0020000          je        near 0049C8C1h
  0049c611  f70617080000          test      dword [esi],817h
  0049c617  0f85a4020000          jne       near 0049C8C1h
  0049c61d  8a4643                mov       al,[esi+43h]
  0049c620  84c0                  test      al,al
  0049c622  0f8599020000          jne       near 0049C8C1h
  0049c628  8b466c                mov       eax,[esi+6Ch]
  0049c62b  8b5e1c                mov       ebx,[esi+1Ch]
  0049c62e  8b4e24                mov       ecx,[esi+24h]
  0049c631  8b781c                mov       edi,[eax+1Ch]
  0049c634  8b6824                mov       ebp,[eax+24h]
  0049c637  2bfb                  sub       edi,ebx
  0049c639  2be9                  sub       ebp,ecx
  0049c63b  c1ff0c                sar       edi,0Ch
  0049c63e  c1fd0c                sar       ebp,0Ch
  0049c641  8bd7                  mov       edx,edi
  0049c643  8bc5                  mov       eax,ebp
  0049c645  0fafd7                imul      edx,edi
  0049c648  0fafc5                imul      eax,ebp
  0049c64b  03d0                  add       edx,eax
  0049c64d  52                    push      edx
  0049c64e  e8bd930a00            call      00545A10h
  0049c653  55                    push      ebp
  0049c654  57                    push      edi
  0049c655  89442420              mov       [esp+20h],eax
  0049c659  e8c4930a00            call      00545A22h
  0049c65e  668b4e32              mov       cx,[esi+32h]
  0049c662  2bc1                  sub       eax,ecx
  0049c664  25ff0f0000            and       eax,0FFFh
  0049c669  89442410              mov       [esp+10h],eax
  0049c66d  8b442418              mov       eax,[esp+18h]
  0049c671  3dfa000000            cmp       eax,0FAh
  0049c676  0f8dd4000000          jge       near 0049C750h
  0049c67c  e8bfaa0a00            call      00547140h
  0049c681  99                    cdq
  0049c682  b964000000            mov       ecx,64h
  0049c687  f7f9                  idiv      ecx
  0049c689  83fa1e                cmp       edx,1Eh
  0049c68c  7d65                  jge       short 0049C6F3h
  0049c68e  8b542410              mov       edx,[esp+10h]
  0049c692  8b7c2414              mov       edi,[esp+14h]
  0049c696  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049c69d  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049c6a4  25ff0f0000            and       eax,0FFFh
  0049c6a9  3bc1                  cmp       eax,ecx
  0049c6ab  0f8fd8010000          jg        near 0049C889h
  0049c6b1  e88aaa0a00            call      00547140h
  0049c6b6  99                    cdq
  0049c6b7  8d4c3f01              lea       ecx,[edi+edi+1]
  0049c6bb  f7f9                  idiv      ecx
  0049c6bd  8b442418              mov       eax,[esp+18h]
  0049c6c1  83c264                add       edx,64h
  0049c6c4  3bc2                  cmp       eax,edx
  0049c6c6  0f8dbd010000          jge       near 0049C889h
  0049c6cc  8b16                  mov       edx,[esi]
  0049c6ce  c7461803000000        mov       dword [esi+18h],3
  0049c6d5  81e2fffffffe          and       edx,0FEFFFFFFh
  0049c6db  c6464200              mov       byte [esi+42h],0
  0049c6df  83ca02                or        edx,2
  0049c6e2  c7465c00000000        mov       dword [esi+5Ch],0
  0049c6e9  8916                  mov       [esi],edx
  0049c6eb  5f                    pop       edi
  0049c6ec  5e                    pop       esi
  0049c6ed  5d                    pop       ebp
  0049c6ee  5b                    pop       ebx
  0049c6ef  83c438                add       esp,38h
  0049c6f2  c3                    ret

LAB_0049c6f3:
  0049c6f3  83fa3c                cmp       edx,3Ch
  0049c6f6  0f8c22010000          jl        near 0049C81Eh
  0049c6fc  c6464203              mov       byte [esi+42h],3
  0049c700  e83baa0a00            call      00547140h
  0049c705  99                    cdq
  0049c706  b90a000000            mov       ecx,0Ah
  0049c70b  f7f9                  idiv      ecx
  0049c70d  8b442414              mov       eax,[esp+14h]
  0049c711  8b4c244c              mov       ecx,[esp+4Ch]
  0049c715  d1f8                  sar       eax,1
  0049c717  8db980000000          lea       edi,[ecx+80h]
  0049c71d  c1e704                shl       edi,4
  0049c720  02d0                  add       dl,al
  0049c722  80c214                add       dl,14h
  0049c725  885643                mov       [esi+43h],dl
  0049c728  e813aa0a00            call      00547140h
  0049c72d  99                    cdq
  0049c72e  f7ff                  idiv      edi
  0049c730  d1ff                  sar       edi,1
  0049c732  2bd7                  sub       edx,edi
  0049c734  8b7c2410              mov       edi,[esp+10h]
  0049c738  03d7                  add       edx,edi
  0049c73a  66035632              add       dx,[esi+32h]
  0049c73e  81c200080000          add       edx,800h
  0049c744  66895610              mov       [esi+10h],dx
  0049c748  5f                    pop       edi
  0049c749  5e                    pop       esi
  0049c74a  5d                    pop       ebp
  0049c74b  5b                    pop       ebx
  0049c74c  83c438                add       esp,38h
  0049c74f  c3                    ret

LAB_0049c750:
  0049c750  3df4010000            cmp       eax,1F4h
  0049c755  0f8df0000000          jge       near 0049C84Bh
  0049c75b  e8e0a90a00            call      00547140h
  0049c760  99                    cdq
  0049c761  b964000000            mov       ecx,64h
  0049c766  f7f9                  idiv      ecx
  0049c768  83fa2d                cmp       edx,2Dh
  0049c76b  7d59                  jge       short 0049C7C6h
  0049c76d  8b542410              mov       edx,[esp+10h]
  0049c771  81c200040000          add       edx,400h
  0049c777  81e2ff0f0000          and       edx,0FFFh
  0049c77d  81fa00080000          cmp       edx,800h
  0049c783  7c10                  jl        short 0049C795h
  0049c785  c6464201              mov       byte [esi+42h],1
  0049c789  c6464301              mov       byte [esi+43h],1
  0049c78d  5f                    pop       edi
  0049c78e  5e                    pop       esi
  0049c78f  5d                    pop       ebp
  0049c790  5b                    pop       ebx
  0049c791  83c438                add       esp,38h
  0049c794  c3                    ret

LAB_0049c795:
  0049c795  8b06                  mov       eax,[esi]
  0049c797  8b4c2414              mov       ecx,[esp+14h]
  0049c79b  25fffffffe            and       eax,0FEFFFFFFh
  0049c7a0  c7461804000000        mov       dword [esi+18h],4
  0049c7a7  c1f908                sar       ecx,8
  0049c7aa  0c02                  or        al,2
  0049c7ac  80c108                add       cl,8
  0049c7af  8906                  mov       [esi],eax
  0049c7b1  c6464201              mov       byte [esi+42h],1
  0049c7b5  884e43                mov       [esi+43h],cl
  0049c7b8  66c746100200          mov       word [esi+10h],2
  0049c7be  5f                    pop       edi
  0049c7bf  5e                    pop       esi
  0049c7c0  5d                    pop       ebp
  0049c7c1  5b                    pop       ebx
  0049c7c2  83c438                add       esp,38h
  0049c7c5  c3                    ret

LAB_0049c7c6:
  0049c7c6  83fa46                cmp       edx,46h
  0049c7c9  7d4e                  jge       short 0049C819h
  0049c7cb  c6464203              mov       byte [esi+42h],3
  0049c7cf  e86ca90a00            call      00547140h
  0049c7d4  99                    cdq
  0049c7d5  b90a000000            mov       ecx,0Ah
  0049c7da  f7f9                  idiv      ecx
  0049c7dc  8b442414              mov       eax,[esp+14h]
  0049c7e0  8b4c244c              mov       ecx,[esp+4Ch]
  0049c7e4  d1f8                  sar       eax,1
  0049c7e6  8db980000000          lea       edi,[ecx+80h]
  0049c7ec  c1e704                shl       edi,4
  0049c7ef  02d0                  add       dl,al
  0049c7f1  80c214                add       dl,14h
  0049c7f4  885643                mov       [esi+43h],dl
  0049c7f7  e844a90a00            call      00547140h
  0049c7fc  99                    cdq
  0049c7fd  f7ff                  idiv      edi
  0049c7ff  8b4c2410              mov       ecx,[esp+10h]
  0049c803  d1ff                  sar       edi,1
  0049c805  2bd7                  sub       edx,edi
  0049c807  03d1                  add       edx,ecx
  0049c809  66035632              add       dx,[esi+32h]
  0049c80d  66895610              mov       [esi+10h],dx
  0049c811  5f                    pop       edi
  0049c812  5e                    pop       esi
  0049c813  5d                    pop       ebp
  0049c814  5b                    pop       ebx
  0049c815  83c438                add       esp,38h
  0049c818  c3                    ret

LAB_0049c819:
  0049c819  83fa5a                cmp       edx,5Ah
  0049c81c  7d3f                  jge       short 0049C85Dh

LAB_0049c81e:
  0049c81e  c6464202              mov       byte [esi+42h],2
  0049c822  e819a90a00            call      00547140h
  0049c827  99                    cdq
  0049c828  b90a000000            mov       ecx,0Ah
  0049c82d  66c746106400          mov       word [esi+10h],64h
  0049c833  f7f9                  idiv      ecx
  0049c835  8b442414              mov       eax,[esp+14h]
  0049c839  d1f8                  sar       eax,1
  0049c83b  02d0                  add       dl,al
  0049c83d  80c214                add       dl,14h
  0049c840  885643                mov       [esi+43h],dl
  0049c843  5f                    pop       edi
  0049c844  5e                    pop       esi
  0049c845  5d                    pop       ebp
  0049c846  5b                    pop       ebx
  0049c847  83c438                add       esp,38h
  0049c84a  c3                    ret

LAB_0049c84b:
  0049c84b  e8f0a80a00            call      00547140h
  0049c850  99                    cdq
  0049c851  b964000000            mov       ecx,64h
  0049c856  f7f9                  idiv      ecx
  0049c858  83fa1e                cmp       edx,1Eh
  0049c85b  7d27                  jge       short 0049C884h

LAB_0049c85d:
  0049c85d  c6464200              mov       byte [esi+42h],0
  0049c861  e8daa80a00            call      00547140h
  0049c866  99                    cdq
  0049c867  b90a000000            mov       ecx,0Ah
  0049c86c  f7f9                  idiv      ecx
  0049c86e  8b442414              mov       eax,[esp+14h]
  0049c872  d1f8                  sar       eax,1
  0049c874  02d0                  add       dl,al
  0049c876  80c214                add       dl,14h
  0049c879  885643                mov       [esi+43h],dl
  0049c87c  5f                    pop       edi
  0049c87d  5e                    pop       esi
  0049c87e  5d                    pop       ebp
  0049c87f  5b                    pop       ebx
  0049c880  83c438                add       esp,38h
  0049c883  c3                    ret

LAB_0049c884:
  0049c884  83fa3c                cmp       edx,3Ch
  0049c887  7d10                  jge       short 0049C899h

LAB_0049c889:
  0049c889  c6464201              mov       byte [esi+42h],1
  0049c88d  c6464301              mov       byte [esi+43h],1
  0049c891  5f                    pop       edi
  0049c892  5e                    pop       esi
  0049c893  5d                    pop       ebp
  0049c894  5b                    pop       ebx
  0049c895  83c438                add       esp,38h
  0049c898  c3                    ret

LAB_0049c899:
  0049c899  c6464203              mov       byte [esi+42h],3
  0049c89d  e89ea80a00            call      00547140h
  0049c8a2  99                    cdq
  0049c8a3  b90a000000            mov       ecx,0Ah
  0049c8a8  f7f9                  idiv      ecx
  0049c8aa  8b442414              mov       eax,[esp+14h]
  0049c8ae  d1f8                  sar       eax,1
  0049c8b0  02d0                  add       dl,al
  0049c8b2  80c214                add       dl,14h
  0049c8b5  885643                mov       [esi+43h],dl
  0049c8b8  e883a80a00            call      00547140h
  0049c8bd  66894610              mov       [esi+10h],ax

LAB_0049c8c1:
  0049c8c1  5f                    pop       edi
  0049c8c2  5e                    pop       esi
  0049c8c3  5d                    pop       ebp
  0049c8c4  5b                    pop       ebx
  0049c8c5  83c438                add       esp,38h
  0049c8c8  c3                    ret
  0049c8c9  90                    nop
  0049c8ca  8bff                  mov       edi,edi
  0049c8cc  15c449004e            adc       eax,4E0049C4h
  0049c8d1  c44900                les       ecx,[ecx]
  0049c8d4  b1c4                  mov       cl,0C4h
  0049c8d6  49                    dec       ecx
  0049c8d7  0039                  add       [ecx],bh
  0049c8d9  c54900                lds       ecx,[ecx]
  0049c8dc  90                    nop
  0049c8dd  90                    nop
  0049c8de  90                    nop
  0049c8df  90                    nop

41 cible(s) de saut interne : 0x0049c16a, 0x0049c1b2, 0x0049c1d9, 0x0049c1f0, 0x0049c241, 0x0049c25a, 0x0049c283, 0x0049c322, 0x0049c371, 0x0049c3d0, 0x0049c3ed, 0x0049c427, 0x0049c43a, 0x0049c446, 0x0049c45e, 0x0049c491, 0x0049c4a1, 0x0049c4a5, 0x0049c4c1, 0x0049c4d4, 0x0049c4e0, 0x0049c514, 0x0049c524, 0x0049c545, 0x0049c555, 0x0049c5c3, 0x0049c5d1, 0x0049c5d3, 0x0049c5d7, 0x0049c6f3, 0x0049c750, 0x0049c795, 0x0049c7c6, 0x0049c819, 0x0049c81e, 0x0049c84b, 0x0049c85d, 0x0049c884, 0x0049c889, 0x0049c899, 0x0049c8c1
