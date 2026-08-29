C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049d030  section .text  offset fichier 0x9c430  2576 octets

  0049d030  83ec44                sub       esp,44h
  0049d033  33c0                  xor       eax,eax
  0049d035  53                    push      ebx
  0049d036  55                    push      ebp
  0049d037  6689442434            mov       [esp+34h],ax
  0049d03c  668944243c            mov       [esp+3Ch],ax
  0049d041  6689442440            mov       [esp+40h],ax
  0049d046  6689442442            mov       [esp+42h],ax
  0049d04b  6689442448            mov       [esp+48h],ax
  0049d050  56                    push      esi
  0049d051  8b742454              mov       esi,[esp+54h]
  0049d055  8d442430              lea       eax,[esp+30h]
  0049d059  57                    push      edi
  0049d05a  8d4c2438              lea       ecx,[esp+38h]
  0049d05e  50                    push      eax
  0049d05f  8d542434              lea       edx,[esp+34h]
  0049d063  51                    push      ecx
  0049d064  bf64000000            mov       edi,64h
  0049d069  52                    push      edx
  0049d06a  56                    push      esi
  0049d06b  66c744244e2800        mov       word [esp+4Eh],28h
  0049d072  66c74424503200        mov       word [esp+50h],32h
  0049d079  66897c2452            mov       [esp+52h],di
  0049d07e  66c74424567800        mov       word [esp+56h],78h
  0049d085  66c744245c3c00        mov       word [esp+5Ch],3Ch
  0049d08c  66897c245e            mov       [esp+5Eh],di
  0049d091  66c74424625a00        mov       word [esp+62h],5Ah
  0049d098  e8e3b80900            call      00538980h
  0049d09d  83c410                add       esp,10h
  0049d0a0  8d442420              lea       eax,[esp+20h]
  0049d0a4  8d4c241c              lea       ecx,[esp+1Ch]
  0049d0a8  8d542410              lea       edx,[esp+10h]
  0049d0ac  50                    push      eax
  0049d0ad  51                    push      ecx
  0049d0ae  8d442420              lea       eax,[esp+20h]
  0049d0b2  52                    push      edx
  0049d0b3  8d4c2464              lea       ecx,[esp+64h]
  0049d0b7  50                    push      eax
  0049d0b8  51                    push      ecx
  0049d0b9  56                    push      esi
  0049d0ba  e8618e0900            call      00535F20h
  0049d0bf  8b4c2470              mov       ecx,[esp+70h]
  0049d0c3  b840000000            mov       eax,40h
  0049d0c8  2bc1                  sub       eax,ecx
  0049d0ca  83c418                add       esp,18h
  0049d0cd  89442414              mov       [esp+14h],eax
  0049d0d1  8b44241c              mov       eax,[esp+1Ch]
  0049d0d5  85c0                  test      eax,eax
  0049d0d7  bd03000000            mov       ebp,3
  0049d0dc  bb01000000            mov       ebx,1
  0049d0e1  0f84b8050000          je        near 0049D69Fh
  0049d0e7  56                    push      esi
  0049d0e8  e8b3e40300            call      004DB5A0h
  0049d0ed  8b06                  mov       eax,[esi]
  0049d0ef  83c404                add       esp,4
  0049d0f2  a802                  test      al,2
  0049d0f4  0f84b6020000          je        near 0049D3B0h
  0049d0fa  f644242010            test      byte [esp+20h],10h
  0049d0ff  740d                  je        short 0049D10Eh
  0049d101  24fd                  and       al,0FDh
  0049d103  c6464300              mov       byte [esi+43h],0
  0049d107  8906                  mov       [esi],eax
  0049d109  e991050000            jmp       0049D69Fh

LAB_0049d10e:
  0049d10e  33c9                  xor       ecx,ecx
  0049d110  8a4e42                mov       cl,[esi+42h]
  0049d113  83e900                sub       ecx,0
  0049d116  0f8482010000          je        near 0049D29Eh
  0049d11c  49                    dec       ecx
  0049d11d  0f857c050000          jne       near 0049D69Fh
  0049d123  8b4650                mov       eax,[esi+50h]
  0049d126  8b4e58                mov       ecx,[esi+58h]
  0049d129  c1f80c                sar       eax,0Ch
  0049d12c  8bd0                  mov       edx,eax
  0049d12e  c1f90c                sar       ecx,0Ch
  0049d131  0fafd0                imul      edx,eax
  0049d134  8bc1                  mov       eax,ecx
  0049d136  0fafc1                imul      eax,ecx
  0049d139  03d0                  add       edx,eax
  0049d13b  52                    push      edx
  0049d13c  e8cf880a00            call      00545A10h
  0049d141  b908000000            mov       ecx,8
  0049d146  66394e1a              cmp       [esi+1Ah],cx
  0049d14a  7e1b                  jle       short 0049D167h
  0049d14c  66894e1a              mov       [esi+1Ah],cx
  0049d150  8b4c2458              mov       ecx,[esp+58h]
  0049d154  c1f903                sar       ecx,3
  0049d157  41                    inc       ecx
  0049d158  3bc1                  cmp       eax,ecx
  0049d15a  7f0b                  jg        short 0049D167h
  0049d15c  8b06                  mov       eax,[esi]
  0049d15e  24fd                  and       al,0FDh
  0049d160  8906                  mov       [esi],eax
  0049d162  e938050000            jmp       0049D69Fh

LAB_0049d167:
  0049d167  66396e1a              cmp       [esi+1Ah],bp
  0049d16b  0f8ee0000000          jle       near 0049D251h
  0049d171  83f806                cmp       eax,6
  0049d174  0f8cd3000000          jl        near 0049D24Dh
  0049d17a  8d542448              lea       edx,[esp+48h]
  0049d17e  8d5e1c                lea       ebx,[esi+1Ch]
  0049d181  6a00                  push      0
  0049d183  52                    push      edx
  0049d184  53                    push      ebx
  0049d185  56                    push      esi
  0049d186  e8a5ca0700            call      00519C30h
  0049d18b  8bf8                  mov       edi,eax
  0049d18d  a1b8e05d00            mov       eax,[5DE0B8h]
  0049d192  83c410                add       esp,10h
  0049d195  85c0                  test      eax,eax
  0049d197  0f84b0000000          je        near 0049D24Dh
  0049d19d  8b0f                  mov       ecx,[edi]
  0049d19f  8d6f1c                lea       ebp,[edi+1Ch]
  0049d1a2  55                    push      ebp
  0049d1a3  6800180000            push      1800h
  0049d1a8  83c940                or        ecx,40h
  0049d1ab  6800030000            push      300h
  0049d1b0  6a03                  push      3
  0049d1b2  66c787960000001000    mov       word [edi+96h],10h
  0049d1bb  890f                  mov       [edi],ecx
  0049d1bd  e85e090a00            call      0053DB20h
  0049d1c2  0fbf4632              movsx     eax,word [esi+32h]
  0049d1c6  83c410                add       esp,10h
  0049d1c9  50                    push      eax
  0049d1ca  e84d880a00            call      00545A1Ch
  0049d1cf  8d0440                lea       eax,[eax+eax*2]
  0049d1d2  8d0c80                lea       ecx,[eax+eax*4]
  0049d1d5  d1e1                  shl       ecx,1
  0049d1d7  894f50                mov       [edi+50h],ecx
  0049d1da  0fbf5632              movsx     edx,word [esi+32h]
  0049d1de  52                    push      edx
  0049d1df  e832880a00            call      00545A16h
  0049d1e4  8d0440                lea       eax,[eax+eax*2]
  0049d1e7  8d0480                lea       eax,[eax+eax*4]
  0049d1ea  d1e0                  shl       eax,1
  0049d1ec  894758                mov       [edi+58h],eax
  0049d1ef  8b07                  mov       eax,[edi]
  0049d1f1  a801                  test      al,1
  0049d1f3  750b                  jne       short 0049D200h
  0049d1f5  0c01                  or        al,1
  0049d1f7  c7475400800100        mov       dword [edi+54h],18000h
  0049d1fe  8907                  mov       [edi],eax

LAB_0049d200:
  0049d200  8b0b                  mov       ecx,[ebx]
  0049d202  8b5620                mov       edx,[esi+20h]
  0049d205  8b4624                mov       eax,[esi+24h]
  0049d208  894c2424              mov       [esp+24h],ecx
  0049d20c  8b4d00                mov       ecx,[ebp]
  0049d20f  89542428              mov       [esp+28h],edx
  0049d213  890b                  mov       [ebx],ecx
  0049d215  8b5720                mov       edx,[edi+20h]
  0049d218  6aff                  push      0FFFFFFFFh
  0049d21a  89442430              mov       [esp+30h],eax
  0049d21e  895620                mov       [esi+20h],edx
  0049d221  8b4724                mov       eax,[edi+24h]
  0049d224  6a00                  push      0
  0049d226  6a39                  push      39h
  0049d228  56                    push      esi
  0049d229  894624                mov       [esi+24h],eax
  0049d22c  e89fd7fdff            call      0047A9D0h
  0049d231  8b4c2434              mov       ecx,[esp+34h]
  0049d235  8b542438              mov       edx,[esp+38h]
  0049d239  8b44243c              mov       eax,[esp+3Ch]
  0049d23d  83c410                add       esp,10h
  0049d240  890b                  mov       [ebx],ecx
  0049d242  895620                mov       [esi+20h],edx
  0049d245  894624                mov       [esi+24h],eax
  0049d248  bd03000000            mov       ebp,3

LAB_0049d24d:
  0049d24d  66396e1a              cmp       [esi+1Ah],bp

LAB_0049d251:
  0049d251  0f8541040000          jne       near 0049D698h
  0049d257  8b4c2458              mov       ecx,[esp+58h]
  0049d25b  0fbf5632              movsx     edx,word [esi+32h]
  0049d25f  81c160030000          add       ecx,360h
  0049d265  52                    push      edx
  0049d266  c1e107                shl       ecx,7
  0049d269  894e5c                mov       [esi+5Ch],ecx
  0049d26c  e8ab870a00            call      00545A1Ch
  0049d271  0faf465c              imul      eax,[esi+5Ch]
  0049d275  8b5e50                mov       ebx,[esi+50h]
  0049d278  c1f80c                sar       eax,0Ch
  0049d27b  03d8                  add       ebx,eax
  0049d27d  0fbf4632              movsx     eax,word [esi+32h]
  0049d281  50                    push      eax
  0049d282  895e50                mov       [esi+50h],ebx
  0049d285  e88c870a00            call      00545A16h
  0049d28a  0faf465c              imul      eax,[esi+5Ch]
  0049d28e  8b4e58                mov       ecx,[esi+58h]
  0049d291  c1f80c                sar       eax,0Ch
  0049d294  03c8                  add       ecx,eax
  0049d296  894e58                mov       [esi+58h],ecx
  0049d299  e9fa030000            jmp       0049D698h

LAB_0049d29e:
  0049d29e  668b4e1a              mov       cx,[esi+1Ah]
  0049d2a2  6683f90e              cmp       cx,0Eh
  0049d2a6  7c60                  jl        short 0049D308h
  0049d2a8  6683f910              cmp       cx,10h
  0049d2ac  7f5a                  jg        short 0049D308h
  0049d2ae  8d4c243c              lea       ecx,[esp+3Ch]
  0049d2b2  6a00                  push      0
  0049d2b4  8d561c                lea       edx,[esi+1Ch]
  0049d2b7  51                    push      ecx
  0049d2b8  52                    push      edx
  0049d2b9  56                    push      esi
  0049d2ba  e871c90700            call      00519C30h
  0049d2bf  8bf8                  mov       edi,eax
  0049d2c1  a1b8e05d00            mov       eax,[5DE0B8h]
  0049d2c6  83c410                add       esp,10h
  0049d2c9  85c0                  test      eax,eax
  0049d2cb  7431                  je        short 0049D2FEh
  0049d2cd  8b1f                  mov       ebx,[edi]
  0049d2cf  57                    push      edi
  0049d2d0  83cb40                or        ebx,40h
  0049d2d3  56                    push      esi
  0049d2d4  66c787960000001000    mov       word [edi+96h],10h
  0049d2dd  891f                  mov       [edi],ebx
  0049d2df  e89cc80900            call      00539B80h
  0049d2e4  83c408                add       esp,8
  0049d2e7  83c71c                add       edi,1Ch
  0049d2ea  57                    push      edi
  0049d2eb  6800180000            push      1800h
  0049d2f0  6800030000            push      300h
  0049d2f5  55                    push      ebp
  0049d2f6  e825080a00            call      0053DB20h
  0049d2fb  83c410                add       esp,10h

LAB_0049d2fe:
  0049d2fe  8b06                  mov       eax,[esi]
  0049d300  a801                  test      al,1
  0049d302  740c                  je        short 0049D310h
  0049d304  66ff4e1a              dec       word [esi+1Ah]

LAB_0049d308:
  0049d308  a801                  test      al,1
  0049d30a  0f858f030000          jne       near 0049D69Fh

LAB_0049d310:
  0049d310  668b561a              mov       dx,[esi+1Ah]
  0049d314  8b7c2458              mov       edi,[esp+58h]
  0049d318  6683fa0d              cmp       dx,0Dh
  0049d31c  7d31                  jge       short 0049D34Fh
  0049d31e  8b4c2410              mov       ecx,[esp+10h]
  0049d322  8d4720                lea       eax,[edi+20h]
  0049d325  8d1c08                lea       ebx,[eax+ecx]
  0049d328  8d2c00                lea       ebp,[eax+eax]
  0049d32b  81e3ff0f0000          and       ebx,0FFFh
  0049d331  3bdd                  cmp       ebx,ebp
  0049d333  7f06                  jg        short 0049D33Bh
  0049d335  66014e32              add       [esi+32h],cx
  0049d339  eb14                  jmp       short 0049D34Fh

LAB_0049d33b:
  0049d33b  81e1ff0f0000          and       ecx,0FFFh
  0049d341  81f900080000          cmp       ecx,800h
  0049d347  7c02                  jl        short 0049D34Bh
  0049d349  f7d8                  neg       eax

LAB_0049d34b:
  0049d34b  66014632              add       [esi+32h],ax

LAB_0049d34f:
  0049d34f  6683fa0d              cmp       dx,0Dh
  0049d353  7545                  jne       short 0049D39Ah
  0049d355  0fbf4632              movsx     eax,word [esi+32h]
  0049d359  81c780020000          add       edi,280h
  0049d35f  50                    push      eax
  0049d360  c1e707                shl       edi,7
  0049d363  897e5c                mov       [esi+5Ch],edi
  0049d366  e8b1860a00            call      00545A1Ch
  0049d36b  0faf465c              imul      eax,[esi+5Ch]
  0049d36f  0fbf4e32              movsx     ecx,word [esi+32h]
  0049d373  8b5e50                mov       ebx,[esi+50h]
  0049d376  51                    push      ecx
  0049d377  c1f80c                sar       eax,0Ch
  0049d37a  03d8                  add       ebx,eax
  0049d37c  895e50                mov       [esi+50h],ebx
  0049d37f  e892860a00            call      00545A16h
  0049d384  0faf465c              imul      eax,[esi+5Ch]
  0049d388  8b4e58                mov       ecx,[esi+58h]
  0049d38b  c7465c00000000        mov       dword [esi+5Ch],0
  0049d392  c1f80c                sar       eax,0Ch
  0049d395  03c8                  add       ecx,eax
  0049d397  894e58                mov       [esi+58h],ecx

LAB_0049d39a:
  0049d39a  8b06                  mov       eax,[esi]
  0049d39c  a900000002            test      eax,2000000h
  0049d3a1  0f84f8020000          je        near 0049D69Fh
  0049d3a7  24fd                  and       al,0FDh
  0049d3a9  8906                  mov       [esi],eax
  0049d3ab  e9ef020000            jmp       0049D69Fh

LAB_0049d3b0:
  0049d3b0  a810                  test      al,10h
  0049d3b2  7425                  je        short 0049D3D9h
  0049d3b4  a900000002            test      eax,2000000h
  0049d3b9  7412                  je        short 0049D3CDh
  0049d3bb  8a4e43                mov       cl,[esi+43h]
  0049d3be  84c9                  test      cl,cl
  0049d3c0  740b                  je        short 0049D3CDh
  0049d3c2  fec9                  dec       cl
  0049d3c4  884e43                mov       [esi+43h],cl
  0049d3c7  7504                  jne       short 0049D3CDh
  0049d3c9  24ef                  and       al,0EFh
  0049d3cb  8906                  mov       [esi],eax

LAB_0049d3cd:
  0049d3cd  8b542458              mov       edx,[esp+58h]
  0049d3d1  8d4240                lea       eax,[edx+40h]
  0049d3d4  e939020000            jmp       0049D612h

LAB_0049d3d9:
  0049d3d9  a904080000            test      eax,804h
  0049d3de  0f85bb020000          jne       near 0049D69Fh
  0049d3e4  a801                  test      al,1
  0049d3e6  7430                  je        short 0049D418h
  0049d3e8  e8539d0a00            call      00547140h
  0049d3ed  99                    cdq
  0049d3ee  f7ff                  idiv      edi
  0049d3f0  8b442458              mov       eax,[esp+58h]
  0049d3f4  c1f802                sar       eax,2
  0049d3f7  3bd0                  cmp       edx,eax
  0049d3f9  0f8da0020000          jge       near 0049D69Fh
  0049d3ff  8b0e                  mov       ecx,[esi]
  0049d401  896e18                mov       [esi+18h],ebp
  0049d404  81e1fffffffe          and       ecx,0FEFFFFFFh
  0049d40a  c6464200              mov       byte [esi+42h],0
  0049d40e  83c902                or        ecx,2
  0049d411  890e                  mov       [esi],ecx
  0049d413  e987020000            jmp       0049D69Fh

LAB_0049d418:
  0049d418  33c9                  xor       ecx,ecx
  0049d41a  8a4e42                mov       cl,[esi+42h]
  0049d41d  83f904                cmp       ecx,4
  0049d420  0f8779020000          ja        near 0049D69Fh
  0049d426  ff248d24da4900        jmp       dword [ecx*4+49DA24h]
  0049d42d  33c9                  xor       ecx,ecx
  0049d42f  66394e18              cmp       [esi+18h],cx
  0049d433  740a                  je        short 0049D43Fh
  0049d435  0d00000001            or        eax,1000000h
  0049d43a  894e18                mov       [esi+18h],ecx
  0049d43d  8906                  mov       [esi],eax

LAB_0049d43f:
  0049d43f  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049d447  7d09                  jge       short 0049D452h
  0049d449  c6464300              mov       byte [esi+43h],0
  0049d44d  e94d020000            jmp       0049D69Fh

LAB_0049d452:
  0049d452  8a4643                mov       al,[esi+43h]
  0049d455  84c0                  test      al,al
  0049d457  7405                  je        short 0049D45Eh
  0049d459  fec8                  dec       al
  0049d45b  884643                mov       [esi+43h],al

LAB_0049d45e:
  0049d45e  894e5c                mov       [esi+5Ch],ecx
  0049d461  e939020000            jmp       0049D69Fh
  0049d466  66395e18              cmp       [esi+18h],bx
  0049d46a  740a                  je        short 0049D476h
  0049d46c  0d00000001            or        eax,1000000h
  0049d471  895e18                mov       [esi+18h],ebx
  0049d474  8906                  mov       [esi],eax

LAB_0049d476:
  0049d476  8b542458              mov       edx,[esp+58h]
  0049d47a  8b4c2410              mov       ecx,[esp+10h]
  0049d47e  8d841280000000        lea       eax,[edx+edx+80h]
  0049d485  8d1408                lea       edx,[eax+ecx]
  0049d488  8d3c00                lea       edi,[eax+eax]
  0049d48b  81e2ff0f0000          and       edx,0FFFh
  0049d491  3bd7                  cmp       edx,edi
  0049d493  7f0d                  jg        short 0049D4A2h
  0049d495  66014e32              add       [esi+32h],cx
  0049d499  c6464300              mov       byte [esi+43h],0
  0049d49d  e9f6010000            jmp       0049D698h

LAB_0049d4a2:
  0049d4a2  81e1ff0f0000          and       ecx,0FFFh
  0049d4a8  81f900080000          cmp       ecx,800h
  0049d4ae  7c02                  jl        short 0049D4B2h
  0049d4b0  f7d8                  neg       eax

LAB_0049d4b2:
  0049d4b2  66014632              add       [esi+32h],ax
  0049d4b6  e9dd010000            jmp       0049D698h
  0049d4bb  66395e18              cmp       [esi+18h],bx
  0049d4bf  740a                  je        short 0049D4CBh
  0049d4c1  0d00000001            or        eax,1000000h
  0049d4c6  895e18                mov       [esi+18h],ebx
  0049d4c9  8906                  mov       [esi],eax

LAB_0049d4cb:
  0049d4cb  0fbf4610              movsx     eax,word [esi+10h]
  0049d4cf  39442418              cmp       [esp+18h],eax
  0049d4d3  7d09                  jge       short 0049D4DEh
  0049d4d5  c6464300              mov       byte [esi+43h],0
  0049d4d9  e9c1010000            jmp       0049D69Fh

LAB_0049d4de:
  0049d4de  8a4643                mov       al,[esi+43h]
  0049d4e1  84c0                  test      al,al
  0049d4e3  7405                  je        short 0049D4EAh
  0049d4e5  fec8                  dec       al
  0049d4e7  884643                mov       [esi+43h],al

LAB_0049d4ea:
  0049d4ea  8b542458              mov       edx,[esp+58h]
  0049d4ee  8b4c2410              mov       ecx,[esp+10h]
  0049d4f2  8d841280000000        lea       eax,[edx+edx+80h]
  0049d4f9  8d3c08                lea       edi,[eax+ecx]
  0049d4fc  8d1c00                lea       ebx,[eax+eax]
  0049d4ff  81e7ff0f0000          and       edi,0FFFh
  0049d505  3bfb                  cmp       edi,ebx
  0049d507  7f15                  jg        short 0049D51Eh
  0049d509  66014e32              add       [esi+32h],cx
  0049d50d  81c200010000          add       edx,100h
  0049d513  c1e207                shl       edx,7
  0049d516  89565c                mov       [esi+5Ch],edx
  0049d519  e981010000            jmp       0049D69Fh

LAB_0049d51e:
  0049d51e  81e1ff0f0000          and       ecx,0FFFh
  0049d524  81f900080000          cmp       ecx,800h
  0049d52a  7c02                  jl        short 0049D52Eh
  0049d52c  f7d8                  neg       eax

LAB_0049d52e:
  0049d52e  66014632              add       [esi+32h],ax
  0049d532  81c200010000          add       edx,100h
  0049d538  c1e207                shl       edx,7
  0049d53b  89565c                mov       [esi+5Ch],edx
  0049d53e  e95c010000            jmp       0049D69Fh
  0049d543  8a4e43                mov       cl,[esi+43h]
  0049d546  84c9                  test      cl,cl
  0049d548  7405                  je        short 0049D54Fh
  0049d54a  fec9                  dec       cl
  0049d54c  884e43                mov       [esi+43h],cl

LAB_0049d54f:
  0049d54f  66395e18              cmp       [esi+18h],bx
  0049d553  740a                  je        short 0049D55Fh
  0049d555  0d00000001            or        eax,1000000h
  0049d55a  895e18                mov       [esi+18h],ebx
  0049d55d  8906                  mov       [esi],eax

LAB_0049d55f:
  0049d55f  e8dc9b0a00            call      00547140h
  0049d564  99                    cdq
  0049d565  f7ff                  idiv      edi
  0049d567  8b442458              mov       eax,[esp+58h]
  0049d56b  8bc8                  mov       ecx,eax
  0049d56d  c1f904                sar       ecx,4
  0049d570  3bd1                  cmp       edx,ecx
  0049d572  7d4f                  jge       short 0049D5C3h
  0049d574  0fbf5610              movsx     edx,word [esi+10h]
  0049d578  0540010000            add       eax,140h
  0049d57d  52                    push      edx
  0049d57e  c1e007                shl       eax,7
  0049d581  89465c                mov       [esi+5Ch],eax
  0049d584  e893840a00            call      00545A1Ch
  0049d589  0faf465c              imul      eax,[esi+5Ch]
  0049d58d  8b6e50                mov       ebp,[esi+50h]
  0049d590  c1f80c                sar       eax,0Ch
  0049d593  03e8                  add       ebp,eax
  0049d595  0fbf4610              movsx     eax,word [esi+10h]
  0049d599  50                    push      eax
  0049d59a  896e50                mov       [esi+50h],ebp
  0049d59d  e874840a00            call      00545A16h
  0049d5a2  0faf465c              imul      eax,[esi+5Ch]
  0049d5a6  8b4e58                mov       ecx,[esi+58h]
  0049d5a9  c7465400400100        mov       dword [esi+54h],14000h
  0049d5b0  c1f80c                sar       eax,0Ch
  0049d5b3  03c8                  add       ecx,eax
  0049d5b5  8b06                  mov       eax,[esi]
  0049d5b7  0bc3                  or        eax,ebx
  0049d5b9  894e58                mov       [esi+58h],ecx
  0049d5bc  8906                  mov       [esi],eax
  0049d5be  e9dc000000            jmp       0049D69Fh

LAB_0049d5c3:
  0049d5c3  0fbf4e10              movsx     ecx,word [esi+10h]
  0049d5c7  05c0000000            add       eax,0C0h
  0049d5cc  51                    push      ecx
  0049d5cd  c1e007                shl       eax,7
  0049d5d0  89465c                mov       [esi+5Ch],eax
  0049d5d3  e844840a00            call      00545A1Ch
  0049d5d8  0faf465c              imul      eax,[esi+5Ch]
  0049d5dc  0fbf5610              movsx     edx,word [esi+10h]
  0049d5e0  8b5e1c                mov       ebx,[esi+1Ch]
  0049d5e3  52                    push      edx
  0049d5e4  c1f80c                sar       eax,0Ch
  0049d5e7  03d8                  add       ebx,eax
  0049d5e9  895e1c                mov       [esi+1Ch],ebx
  0049d5ec  e825840a00            call      00545A16h
  0049d5f1  0faf465c              imul      eax,[esi+5Ch]
  0049d5f5  8b4e24                mov       ecx,[esi+24h]
  0049d5f8  c7465c00000000        mov       dword [esi+5Ch],0
  0049d5ff  c1f80c                sar       eax,0Ch
  0049d602  03c8                  add       ecx,eax
  0049d604  8b442458              mov       eax,[esp+58h]
  0049d608  894e24                mov       [esi+24h],ecx
  0049d60b  8d048532000000        lea       eax,[eax*4+32h]

LAB_0049d612:
  0049d612  8b4c2410              mov       ecx,[esp+10h]
  0049d616  8d3c00                lea       edi,[eax+eax]
  0049d619  8d1408                lea       edx,[eax+ecx]
  0049d61c  81e2ff0f0000          and       edx,0FFFh
  0049d622  3bd7                  cmp       edx,edi
  0049d624  7f06                  jg        short 0049D62Ch
  0049d626  66014e32              add       [esi+32h],cx
  0049d62a  eb73                  jmp       short 0049D69Fh

LAB_0049d62c:
  0049d62c  81e1ff0f0000          and       ecx,0FFFh
  0049d632  81f900080000          cmp       ecx,800h
  0049d638  7c02                  jl        short 0049D63Ch
  0049d63a  f7d8                  neg       eax

LAB_0049d63c:
  0049d63c  66014632              add       [esi+32h],ax
  0049d640  eb5d                  jmp       short 0049D69Fh
  0049d642  25fffffffe            and       eax,0FEFFFFFFh
  0049d647  8b7c241c              mov       edi,[esp+1Ch]
  0049d64b  0bc3                  or        eax,ebx
  0049d64d  c7461805000000        mov       dword [esi+18h],5
  0049d654  8906                  mov       [esi],eax
  0049d656  8b442458              mov       eax,[esp+58h]
  0049d65a  0580020000            add       eax,280h
  0049d65f  c1e007                shl       eax,7
  0049d662  0fbf4e10              movsx     ecx,word [esi+10h]
  0049d666  0faff8                imul      edi,eax
  0049d669  51                    push      ecx
  0049d66a  89465c                mov       [esi+5Ch],eax
  0049d66d  c1ff0c                sar       edi,0Ch
  0049d670  e8a7830a00            call      00545A1Ch
  0049d675  0fafc7                imul      eax,edi
  0049d678  0fbf5610              movsx     edx,word [esi+10h]
  0049d67c  c1f80c                sar       eax,0Ch
  0049d67f  52                    push      edx
  0049d680  894650                mov       [esi+50h],eax
  0049d683  e88e830a00            call      00545A16h
  0049d688  0fafc7                imul      eax,edi
  0049d68b  c1f80c                sar       eax,0Ch
  0049d68e  894658                mov       [esi+58h],eax
  0049d691  c7465400c00100        mov       dword [esi+54h],1C000h

LAB_0049d698:
  0049d698  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0049d69f:
  0049d69f  8b44241c              mov       eax,[esp+1Ch]
  0049d6a3  50                    push      eax
  0049d6a4  56                    push      esi
  0049d6a5  e8b6aafeff            call      00488160h
  0049d6aa  83c408                add       esp,8
  0049d6ad  56                    push      esi
  0049d6ae  e88d620900            call      00533940h
  0049d6b3  83c404                add       esp,4
  0049d6b6  56                    push      esi
  0049d6b7  e834240400            call      004DFAF0h
  0049d6bc  8b4c2434              mov       ecx,[esp+34h]
  0049d6c0  83c404                add       esp,4
  0049d6c3  51                    push      ecx
  0049d6c4  56                    push      esi
  0049d6c5  e8a6600400            call      004E3770h
  0049d6ca  8b442424              mov       eax,[esp+24h]
  0049d6ce  83c408                add       esp,8
  0049d6d1  85c0                  test      eax,eax
  0049d6d3  0f8441030000          je        near 0049DA1Ah
  0049d6d9  f70617080000          test      dword [esi],817h
  0049d6df  0f8535030000          jne       near 0049DA1Ah
  0049d6e5  8a4643                mov       al,[esi+43h]
  0049d6e8  84c0                  test      al,al
  0049d6ea  0f852a030000          jne       near 0049DA1Ah
  0049d6f0  8b466c                mov       eax,[esi+6Ch]
  0049d6f3  8b5e1c                mov       ebx,[esi+1Ch]
  0049d6f6  8b4e24                mov       ecx,[esi+24h]
  0049d6f9  8b781c                mov       edi,[eax+1Ch]
  0049d6fc  8b6824                mov       ebp,[eax+24h]
  0049d6ff  2bfb                  sub       edi,ebx
  0049d701  2be9                  sub       ebp,ecx
  0049d703  c1ff0c                sar       edi,0Ch
  0049d706  c1fd0c                sar       ebp,0Ch
  0049d709  8bd7                  mov       edx,edi
  0049d70b  8bc5                  mov       eax,ebp
  0049d70d  0fafd7                imul      edx,edi
  0049d710  0fafc5                imul      eax,ebp
  0049d713  03d0                  add       edx,eax
  0049d715  52                    push      edx
  0049d716  e8f5820a00            call      00545A10h
  0049d71b  55                    push      ebp
  0049d71c  57                    push      edi
  0049d71d  89442420              mov       [esp+20h],eax
  0049d721  e8fc820a00            call      00545A22h
  0049d726  668b4e32              mov       cx,[esi+32h]
  0049d72a  2bc1                  sub       eax,ecx
  0049d72c  25ff0f0000            and       eax,0FFFh
  0049d731  89442410              mov       [esp+10h],eax
  0049d735  8b442418              mov       eax,[esp+18h]
  0049d739  3dfa000000            cmp       eax,0FAh
  0049d73e  0f8dae010000          jge       near 0049D8F2h
  0049d744  e8f7990a00            call      00547140h
  0049d749  99                    cdq
  0049d74a  b964000000            mov       ecx,64h
  0049d74f  f7f9                  idiv      ecx
  0049d751  83fa32                cmp       edx,32h
  0049d754  0f8dd0000000          jge       near 0049D82Ah
  0049d75a  8b7c2414              mov       edi,[esp+14h]
  0049d75e  8b542410              mov       edx,[esp+10h]
  0049d762  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049d769  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049d770  25ff0f0000            and       eax,0FFFh
  0049d775  3bc1                  cmp       eax,ecx
  0049d777  7f41                  jg        short 0049D7BAh
  0049d779  e8c2990a00            call      00547140h
  0049d77e  99                    cdq
  0049d77f  8d4c3f01              lea       ecx,[edi+edi+1]
  0049d783  f7f9                  idiv      ecx
  0049d785  8b442418              mov       eax,[esp+18h]
  0049d789  81c2c8000000          add       edx,0C8h
  0049d78f  3bc2                  cmp       eax,edx
  0049d791  7d27                  jge       short 0049D7BAh
  0049d793  8b16                  mov       edx,[esi]
  0049d795  c7461803000000        mov       dword [esi+18h],3
  0049d79c  81e2fffffffe          and       edx,0FEFFFFFFh
  0049d7a2  c6464200              mov       byte [esi+42h],0
  0049d7a6  83ca02                or        edx,2
  0049d7a9  c7465c00000000        mov       dword [esi+5Ch],0
  0049d7b0  8916                  mov       [esi],edx
  0049d7b2  5f                    pop       edi
  0049d7b3  5e                    pop       esi
  0049d7b4  5d                    pop       ebp
  0049d7b5  5b                    pop       ebx
  0049d7b6  83c444                add       esp,44h
  0049d7b9  c3                    ret

LAB_0049d7ba:
  0049d7ba  8b442410              mov       eax,[esp+10h]
  0049d7be  8d14fd00040000        lea       edx,[edi*8+400h]
  0049d7c5  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  0049d7cc  81e1ff0f0000          and       ecx,0FFFh
  0049d7d2  3bca                  cmp       ecx,edx
  0049d7d4  0f8f08020000          jg        near 0049D9E2h
  0049d7da  e861990a00            call      00547140h
  0049d7df  99                    cdq
  0049d7e0  b964000000            mov       ecx,64h
  0049d7e5  f7f9                  idiv      ecx
  0049d7e7  d1ff                  sar       edi,1
  0049d7e9  83c720                add       edi,20h
  0049d7ec  3bd7                  cmp       edx,edi
  0049d7ee  0f8dee010000          jge       near 0049D9E2h
  0049d7f4  8b16                  mov       edx,[esi]
  0049d7f6  c7461804000000        mov       dword [esi+18h],4
  0049d7fd  81e2fffffffe          and       edx,0FEFFFFFFh
  0049d803  83ca10                or        edx,10h
  0049d806  8916                  mov       [esi],edx
  0049d808  e833990a00            call      00547140h
  0049d80d  99                    cdq
  0049d80e  b905000000            mov       ecx,5
  0049d813  c7465c00000000        mov       dword [esi+5Ch],0
  0049d81a  f7f9                  idiv      ecx
  0049d81c  80c20a                add       dl,0Ah
  0049d81f  885643                mov       [esi+43h],dl
  0049d822  5f                    pop       edi
  0049d823  5e                    pop       esi
  0049d824  5d                    pop       ebp
  0049d825  5b                    pop       ebx
  0049d826  83c444                add       esp,44h
  0049d829  c3                    ret

LAB_0049d82a:
  0049d82a  83fa46                cmp       edx,46h
  0049d82d  7d28                  jge       short 0049D857h
  0049d82f  8b442458              mov       eax,[esp+58h]
  0049d833  8b4c2418              mov       ecx,[esp+18h]
  0049d837  83c064                add       eax,64h
  0049d83a  3bc8                  cmp       ecx,eax
  0049d83c  7e19                  jle       short 0049D857h
  0049d83e  c6464204              mov       byte [esi+42h],4
  0049d842  c6464301              mov       byte [esi+43h],1
  0049d846  e8f5980a00            call      00547140h
  0049d84b  66894610              mov       [esi+10h],ax
  0049d84f  5f                    pop       edi
  0049d850  5e                    pop       esi
  0049d851  5d                    pop       ebp
  0049d852  5b                    pop       ebx
  0049d853  83c444                add       esp,44h
  0049d856  c3                    ret

LAB_0049d857:
  0049d857  83fa5a                cmp       edx,5Ah
  0049d85a  7d40                  jge       short 0049D89Ch
  0049d85c  8b4c2410              mov       ecx,[esp+10h]
  0049d860  8b7c2414              mov       edi,[esp+14h]
  0049d864  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  0049d86b  8d04fd00020000        lea       eax,[edi*8+200h]
  0049d872  81e2ff0f0000          and       edx,0FFFh
  0049d878  3bd0                  cmp       edx,eax
  0049d87a  7f24                  jg        short 0049D8A0h
  0049d87c  8b0e                  mov       ecx,[esi]
  0049d87e  c7461804000000        mov       dword [esi+18h],4
  0049d885  81e1fffffffe          and       ecx,0FEFFFFFFh
  0049d88b  c6464201              mov       byte [esi+42h],1
  0049d88f  83c902                or        ecx,2
  0049d892  890e                  mov       [esi],ecx
  0049d894  5f                    pop       edi
  0049d895  5e                    pop       esi
  0049d896  5d                    pop       ebp
  0049d897  5b                    pop       ebx
  0049d898  83c444                add       esp,44h
  0049d89b  c3                    ret

LAB_0049d89c:
  0049d89c  8b7c2414              mov       edi,[esp+14h]

LAB_0049d8a0:
  0049d8a0  c6464203              mov       byte [esi+42h],3
  0049d8a4  e897980a00            call      00547140h
  0049d8a9  99                    cdq
  0049d8aa  b90a000000            mov       ecx,0Ah
  0049d8af  f7f9                  idiv      ecx
  0049d8b1  8b4c2458              mov       ecx,[esp+58h]
  0049d8b5  8bc7                  mov       eax,edi
  0049d8b7  d1f8                  sar       eax,1
  0049d8b9  8db980000000          lea       edi,[ecx+80h]
  0049d8bf  c1e704                shl       edi,4
  0049d8c2  02d0                  add       dl,al
  0049d8c4  80c214                add       dl,14h
  0049d8c7  885643                mov       [esi+43h],dl
  0049d8ca  e871980a00            call      00547140h
  0049d8cf  99                    cdq
  0049d8d0  f7ff                  idiv      edi
  0049d8d2  d1ff                  sar       edi,1
  0049d8d4  2bd7                  sub       edx,edi
  0049d8d6  8b7c2410              mov       edi,[esp+10h]
  0049d8da  03d7                  add       edx,edi
  0049d8dc  66035632              add       dx,[esi+32h]
  0049d8e0  81c200080000          add       edx,800h
  0049d8e6  66895610              mov       [esi+10h],dx
  0049d8ea  5f                    pop       edi
  0049d8eb  5e                    pop       esi
  0049d8ec  5d                    pop       ebp
  0049d8ed  5b                    pop       ebx
  0049d8ee  83c444                add       esp,44h
  0049d8f1  c3                    ret

LAB_0049d8f2:
  0049d8f2  3df4010000            cmp       eax,1F4h
  0049d8f7  0f8da7000000          jge       near 0049D9A4h
  0049d8fd  e83e980a00            call      00547140h
  0049d902  99                    cdq
  0049d903  b964000000            mov       ecx,64h
  0049d908  f7f9                  idiv      ecx
  0049d90a  83fa2d                cmp       edx,2Dh
  0049d90d  7d10                  jge       short 0049D91Fh
  0049d90f  c6464201              mov       byte [esi+42h],1
  0049d913  c6464301              mov       byte [esi+43h],1
  0049d917  5f                    pop       edi
  0049d918  5e                    pop       esi
  0049d919  5d                    pop       ebp
  0049d91a  5b                    pop       ebx
  0049d91b  83c444                add       esp,44h
  0049d91e  c3                    ret

LAB_0049d91f:
  0049d91f  83fa46                cmp       edx,46h
  0049d922  7d4e                  jge       short 0049D972h
  0049d924  c6464203              mov       byte [esi+42h],3
  0049d928  e813980a00            call      00547140h
  0049d92d  99                    cdq
  0049d92e  b90a000000            mov       ecx,0Ah
  0049d933  f7f9                  idiv      ecx
  0049d935  8b442414              mov       eax,[esp+14h]
  0049d939  8b4c2458              mov       ecx,[esp+58h]
  0049d93d  d1f8                  sar       eax,1
  0049d93f  8db980000000          lea       edi,[ecx+80h]
  0049d945  c1e704                shl       edi,4
  0049d948  02d0                  add       dl,al
  0049d94a  80c214                add       dl,14h
  0049d94d  885643                mov       [esi+43h],dl
  0049d950  e8eb970a00            call      00547140h
  0049d955  99                    cdq
  0049d956  f7ff                  idiv      edi
  0049d958  8b4c2410              mov       ecx,[esp+10h]
  0049d95c  d1ff                  sar       edi,1
  0049d95e  2bd7                  sub       edx,edi
  0049d960  03d1                  add       edx,ecx
  0049d962  66035632              add       dx,[esi+32h]
  0049d966  66895610              mov       [esi+10h],dx
  0049d96a  5f                    pop       edi
  0049d96b  5e                    pop       esi
  0049d96c  5d                    pop       ebp
  0049d96d  5b                    pop       ebx
  0049d96e  83c444                add       esp,44h
  0049d971  c3                    ret

LAB_0049d972:
  0049d972  83fa5a                cmp       edx,5Ah
  0049d975  7d3f                  jge       short 0049D9B6h
  0049d977  c6464202              mov       byte [esi+42h],2
  0049d97b  e8c0970a00            call      00547140h
  0049d980  99                    cdq
  0049d981  b90a000000            mov       ecx,0Ah
  0049d986  66c746106400          mov       word [esi+10h],64h
  0049d98c  f7f9                  idiv      ecx
  0049d98e  8b442414              mov       eax,[esp+14h]
  0049d992  d1f8                  sar       eax,1
  0049d994  02d0                  add       dl,al
  0049d996  80c214                add       dl,14h
  0049d999  885643                mov       [esi+43h],dl
  0049d99c  5f                    pop       edi
  0049d99d  5e                    pop       esi
  0049d99e  5d                    pop       ebp
  0049d99f  5b                    pop       ebx
  0049d9a0  83c444                add       esp,44h
  0049d9a3  c3                    ret

LAB_0049d9a4:
  0049d9a4  e897970a00            call      00547140h
  0049d9a9  99                    cdq
  0049d9aa  b964000000            mov       ecx,64h
  0049d9af  f7f9                  idiv      ecx
  0049d9b1  83fa1e                cmp       edx,1Eh
  0049d9b4  7d27                  jge       short 0049D9DDh

LAB_0049d9b6:
  0049d9b6  c6464200              mov       byte [esi+42h],0
  0049d9ba  e881970a00            call      00547140h
  0049d9bf  99                    cdq
  0049d9c0  b90a000000            mov       ecx,0Ah
  0049d9c5  f7f9                  idiv      ecx
  0049d9c7  8b442414              mov       eax,[esp+14h]
  0049d9cb  d1f8                  sar       eax,1
  0049d9cd  02d0                  add       dl,al
  0049d9cf  80c214                add       dl,14h
  0049d9d2  885643                mov       [esi+43h],dl
  0049d9d5  5f                    pop       edi
  0049d9d6  5e                    pop       esi
  0049d9d7  5d                    pop       ebp
  0049d9d8  5b                    pop       ebx
  0049d9d9  83c444                add       esp,44h
  0049d9dc  c3                    ret

LAB_0049d9dd:
  0049d9dd  83fa3c                cmp       edx,3Ch
  0049d9e0  7d10                  jge       short 0049D9F2h

LAB_0049d9e2:
  0049d9e2  c6464201              mov       byte [esi+42h],1
  0049d9e6  c6464301              mov       byte [esi+43h],1
  0049d9ea  5f                    pop       edi
  0049d9eb  5e                    pop       esi
  0049d9ec  5d                    pop       ebp
  0049d9ed  5b                    pop       ebx
  0049d9ee  83c444                add       esp,44h
  0049d9f1  c3                    ret

LAB_0049d9f2:
  0049d9f2  c6464203              mov       byte [esi+42h],3
  0049d9f6  e845970a00            call      00547140h
  0049d9fb  99                    cdq
  0049d9fc  b90a000000            mov       ecx,0Ah
  0049da01  f7f9                  idiv      ecx
  0049da03  8b442414              mov       eax,[esp+14h]
  0049da07  d1f8                  sar       eax,1
  0049da09  02d0                  add       dl,al
  0049da0b  80c214                add       dl,14h
  0049da0e  885643                mov       [esi+43h],dl
  0049da11  e82a970a00            call      00547140h
  0049da16  66894610              mov       [esi+10h],ax

LAB_0049da1a:
  0049da1a  5f                    pop       edi
  0049da1b  5e                    pop       esi
  0049da1c  5d                    pop       ebp
  0049da1d  5b                    pop       ebx
  0049da1e  83c444                add       esp,44h
  0049da21  c3                    ret
  0049da22  8bff                  mov       edi,edi
  0049da24  2dd4490066            sub       eax,660049D4h
  0049da29  d449                  aam       49h
  0049da2b  00bbd4490043          add       [ebx+430049D4h],bh
  0049da31  d549                  aad       49h
  0049da33  0042d6                add       [edx-2Ah],al
  0049da36  49                    dec       ecx
  0049da37  009090909090          add       [eax-6F6F6F70h],dl
  0049da3d  90                    nop
  0049da3e  90                    nop
  0049da3f  90                    nop

50 cible(s) de saut interne : 0x0049d10e, 0x0049d167, 0x0049d200, 0x0049d24d, 0x0049d251, 0x0049d29e, 0x0049d2fe, 0x0049d308, 0x0049d310, 0x0049d33b, 0x0049d34b, 0x0049d34f, 0x0049d39a, 0x0049d3b0, 0x0049d3cd, 0x0049d3d9, 0x0049d418, 0x0049d43f, 0x0049d452, 0x0049d45e, 0x0049d476, 0x0049d4a2, 0x0049d4b2, 0x0049d4cb, 0x0049d4de, 0x0049d4ea, 0x0049d51e, 0x0049d52e, 0x0049d54f, 0x0049d55f, 0x0049d5c3, 0x0049d612, 0x0049d62c, 0x0049d63c, 0x0049d698, 0x0049d69f, 0x0049d7ba, 0x0049d82a, 0x0049d857, 0x0049d89c, 0x0049d8a0, 0x0049d8f2, 0x0049d91f, 0x0049d972, 0x0049d9a4, 0x0049d9b6, 0x0049d9dd, 0x0049d9e2, 0x0049d9f2, 0x0049da1a
