C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a2c90  section .text  offset fichier 0xa2090  2308 octets

  004a2c90  83ec2c                sub       esp,2Ch
  004a2c93  53                    push      ebx
  004a2c94  55                    push      ebp
  004a2c95  56                    push      esi
  004a2c96  8b74243c              mov       esi,[esp+3Ch]
  004a2c9a  8d44241c              lea       eax,[esp+1Ch]
  004a2c9e  57                    push      edi
  004a2c9f  8d4c2438              lea       ecx,[esp+38h]
  004a2ca3  50                    push      eax
  004a2ca4  8d542438              lea       edx,[esp+38h]
  004a2ca8  51                    push      ecx
  004a2ca9  52                    push      edx
  004a2caa  56                    push      esi
  004a2cab  e8d05c0900            call      00538980h
  004a2cb0  83c410                add       esp,10h
  004a2cb3  8d442424              lea       eax,[esp+24h]
  004a2cb7  8d4c241c              lea       ecx,[esp+1Ch]
  004a2cbb  8d542440              lea       edx,[esp+40h]
  004a2cbf  50                    push      eax
  004a2cc0  51                    push      ecx
  004a2cc1  8d44241c              lea       eax,[esp+1Ch]
  004a2cc5  52                    push      edx
  004a2cc6  8d4c241c              lea       ecx,[esp+1Ch]
  004a2cca  50                    push      eax
  004a2ccb  51                    push      ecx
  004a2ccc  56                    push      esi
  004a2ccd  e84e320900            call      00535F20h
  004a2cd2  8b4c2428              mov       ecx,[esp+28h]
  004a2cd6  8b442434              mov       eax,[esp+34h]
  004a2cda  bb40000000            mov       ebx,40h
  004a2cdf  83c418                add       esp,18h
  004a2ce2  2bd9                  sub       ebx,ecx
  004a2ce4  85c0                  test      eax,eax
  004a2ce6  895c2418              mov       [esp+18h],ebx
  004a2cea  0f840d020000          je        near 004A2EFDh
  004a2cf0  56                    push      esi
  004a2cf1  e8aa880300            call      004DB5A0h
  004a2cf6  8b06                  mov       eax,[esi]
  004a2cf8  83c404                add       esp,4
  004a2cfb  a802                  test      al,2
  004a2cfd  0f8491060000          je        near 004A3394h
  004a2d03  f644242410            test      byte [esp+24h],10h
  004a2d08  740d                  je        short 004A2D17h
  004a2d0a  24fd                  and       al,0FDh
  004a2d0c  c6464300              mov       byte [esi+43h],0
  004a2d10  8906                  mov       [esi],eax
  004a2d12  e9e6010000            jmp       004A2EFDh

LAB_004a2d17:
  004a2d17  33c9                  xor       ecx,ecx
  004a2d19  8a4e42                mov       cl,[esi+42h]
  004a2d1c  83e900                sub       ecx,0
  004a2d1f  0f842a050000          je        near 004A324Fh
  004a2d25  49                    dec       ecx
  004a2d26  0f85d1010000          jne       near 004A2EFDh
  004a2d2c  0fbf4e10              movsx     ecx,word [esi+10h]
  004a2d30  83e900                sub       ecx,0
  004a2d33  747d                  je        short 004A2DB2h
  004a2d35  49                    dec       ecx
  004a2d36  0f85c1010000          jne       near 004A2EFDh
  004a2d3c  33ff                  xor       edi,edi
  004a2d3e  66837e1a07            cmp       word [esi+1Ah],7
  004a2d43  897e5c                mov       [esi+5Ch],edi
  004a2d46  755c                  jne       short 004A2DA4h
  004a2d48  807e4302              cmp       byte [esi+43h],2
  004a2d4c  7552                  jne       short 004A2DA0h
  004a2d4e  8b542410              mov       edx,[esp+10h]
  004a2d52  0fbf4632              movsx     eax,word [esi+32h]
  004a2d56  81c240040000          add       edx,440h
  004a2d5c  50                    push      eax
  004a2d5d  c1e207                shl       edx,7
  004a2d60  89565c                mov       [esi+5Ch],edx
  004a2d63  e8b42c0a00            call      00545A1Ch
  004a2d68  0faf465c              imul      eax,[esi+5Ch]
  004a2d6c  8b4e50                mov       ecx,[esi+50h]
  004a2d6f  c1f80c                sar       eax,0Ch
  004a2d72  03c8                  add       ecx,eax
  004a2d74  894e50                mov       [esi+50h],ecx
  004a2d77  0fbf4e32              movsx     ecx,word [esi+32h]
  004a2d7b  51                    push      ecx
  004a2d7c  e8952c0a00            call      00545A16h
  004a2d81  0faf465c              imul      eax,[esi+5Ch]
  004a2d85  8b4e58                mov       ecx,[esi+58h]
  004a2d88  c7465400400100        mov       dword [esi+54h],14000h
  004a2d8f  c1f80c                sar       eax,0Ch
  004a2d92  03c8                  add       ecx,eax
  004a2d94  8b06                  mov       eax,[esi]
  004a2d96  0c01                  or        al,1
  004a2d98  894e58                mov       [esi+58h],ecx
  004a2d9b  8906                  mov       [esi],eax
  004a2d9d  897e5c                mov       [esi+5Ch],edi

LAB_004a2da0:
  004a2da0  66ff4e10              dec       word [esi+10h]

LAB_004a2da4:
  004a2da4  8b442410              mov       eax,[esp+10h]
  004a2da8  d1f8                  sar       eax,1
  004a2daa  83c040                add       eax,40h
  004a2dad  e9ac070000            jmp       004A355Eh

LAB_004a2db2:
  004a2db2  a900000002            test      eax,2000000h
  004a2db7  7408                  je        short 004A2DC1h
  004a2db9  a801                  test      al,1
  004a2dbb  7504                  jne       short 004A2DC1h
  004a2dbd  24dd                  and       al,0DDh
  004a2dbf  8906                  mov       [esi],eax

LAB_004a2dc1:
  004a2dc1  668b461a              mov       ax,[esi+1Ah]
  004a2dc5  663d0a00              cmp       ax,0Ah
  004a2dc9  0f8c2e010000          jl        near 004A2EFDh
  004a2dcf  663d0e00              cmp       ax,0Eh
  004a2dd3  0f8f24010000          jg        near 004A2EFDh
  004a2dd9  f60620                test      byte [esi],20h
  004a2ddc  0f851b010000          jne       near 004A2EFDh
  004a2de2  8d6e1c                lea       ebp,[esi+1Ch]
  004a2de5  6a00                  push      0
  004a2de7  68dc295500            push      5529DCh
  004a2dec  55                    push      ebp
  004a2ded  56                    push      esi
  004a2dee  e83d6e0700            call      00519C30h
  004a2df3  8bf8                  mov       edi,eax
  004a2df5  83c410                add       esp,10h
  004a2df8  85ff                  test      edi,edi
  004a2dfa  7406                  je        short 004A2E02h
  004a2dfc  8b06                  mov       eax,[esi]
  004a2dfe  0c20                  or        al,20h
  004a2e00  8906                  mov       [esi],eax

LAB_004a2e02:
  004a2e02  a1b8e05d00            mov       eax,[5DE0B8h]
  004a2e07  85c0                  test      eax,eax
  004a2e09  0f84ee000000          je        near 004A2EFDh
  004a2e0f  8b1f                  mov       ebx,[edi]
  004a2e11  66c787960000001000    mov       word [edi+96h],10h
  004a2e1a  83cb40                or        ebx,40h
  004a2e1d  57                    push      edi
  004a2e1e  891f                  mov       [edi],ebx
  004a2e20  8b4c2424              mov       ecx,[esp+24h]
  004a2e24  56                    push      esi
  004a2e25  668b4130              mov       ax,[ecx+30h]
  004a2e29  0fbfd8                movsx     ebx,ax
  004a2e2c  66d1f8                sar       ax,1
  004a2e2f  66894130              mov       [ecx+30h],ax
  004a2e33  e8486d0900            call      00539B80h
  004a2e38  8b442428              mov       eax,[esp+28h]
  004a2e3c  83c408                add       esp,8
  004a2e3f  66895830              mov       [eax+30h],bx
  004a2e43  8d5f1c                lea       ebx,[edi+1Ch]
  004a2e46  53                    push      ebx
  004a2e47  6800180000            push      1800h
  004a2e4c  6800030000            push      300h
  004a2e51  6a03                  push      3
  004a2e53  e8c8ac0900            call      0053DB20h
  004a2e58  8b07                  mov       eax,[edi]
  004a2e5a  83c410                add       esp,10h
  004a2e5d  a900000040            test      eax,40000000h
  004a2e62  0f8591000000          jne       near 004A2EF9h
  004a2e68  0fbf4e32              movsx     ecx,word [esi+32h]
  004a2e6c  8b542440              mov       edx,[esp+40h]
  004a2e70  03d1                  add       edx,ecx
  004a2e72  52                    push      edx
  004a2e73  e8a42b0a00            call      00545A1Ch
  004a2e78  8d0440                lea       eax,[eax+eax*2]
  004a2e7b  8d0480                lea       eax,[eax+eax*4]
  004a2e7e  d1e0                  shl       eax,1
  004a2e80  894750                mov       [edi+50h],eax
  004a2e83  8b542440              mov       edx,[esp+40h]
  004a2e87  0fbf4e32              movsx     ecx,word [esi+32h]
  004a2e8b  03d1                  add       edx,ecx
  004a2e8d  52                    push      edx
  004a2e8e  e8832b0a00            call      00545A16h
  004a2e93  8d0440                lea       eax,[eax+eax*2]
  004a2e96  8d0480                lea       eax,[eax+eax*4]
  004a2e99  d1e0                  shl       eax,1
  004a2e9b  894758                mov       [edi+58h],eax
  004a2e9e  8b07                  mov       eax,[edi]
  004a2ea0  a801                  test      al,1
  004a2ea2  750b                  jne       short 004A2EAFh
  004a2ea4  0c01                  or        al,1
  004a2ea6  c7475400800100        mov       dword [edi+54h],18000h
  004a2ead  8907                  mov       [edi],eax

LAB_004a2eaf:
  004a2eaf  8b4d00                mov       ecx,[ebp]
  004a2eb2  8b5620                mov       edx,[esi+20h]
  004a2eb5  8b4624                mov       eax,[esi+24h]
  004a2eb8  894c2428              mov       [esp+28h],ecx
  004a2ebc  8b0b                  mov       ecx,[ebx]
  004a2ebe  8954242c              mov       [esp+2Ch],edx
  004a2ec2  894d00                mov       [ebp],ecx
  004a2ec5  8b5720                mov       edx,[edi+20h]
  004a2ec8  6aff                  push      0FFFFFFFFh
  004a2eca  89442434              mov       [esp+34h],eax
  004a2ece  895620                mov       [esi+20h],edx
  004a2ed1  8b4724                mov       eax,[edi+24h]
  004a2ed4  6a00                  push      0
  004a2ed6  6a39                  push      39h
  004a2ed8  56                    push      esi
  004a2ed9  894624                mov       [esi+24h],eax
  004a2edc  e8ef7afdff            call      0047A9D0h
  004a2ee1  8b4c2438              mov       ecx,[esp+38h]
  004a2ee5  8b54243c              mov       edx,[esp+3Ch]
  004a2ee9  8b442440              mov       eax,[esp+40h]
  004a2eed  83c410                add       esp,10h
  004a2ef0  894d00                mov       [ebp],ecx
  004a2ef3  895620                mov       [esi+20h],edx
  004a2ef6  894624                mov       [esi+24h],eax

LAB_004a2ef9:
  004a2ef9  8b5c2418              mov       ebx,[esp+18h]

LAB_004a2efd:
  004a2efd  8b44241c              mov       eax,[esp+1Ch]
  004a2f01  66c7869a0000000060    mov       word [esi+9Ah],6000h
  004a2f0a  50                    push      eax
  004a2f0b  56                    push      esi
  004a2f0c  e84f52feff            call      00488160h
  004a2f11  83c408                add       esp,8
  004a2f14  56                    push      esi
  004a2f15  e8260a0900            call      00533940h
  004a2f1a  83c404                add       esp,4
  004a2f1d  56                    push      esi
  004a2f1e  e8cdcb0300            call      004DFAF0h
  004a2f23  8b4c2438              mov       ecx,[esp+38h]
  004a2f27  83c404                add       esp,4
  004a2f2a  51                    push      ecx
  004a2f2b  56                    push      esi
  004a2f2c  e83f080400            call      004E3770h
  004a2f31  8b442424              mov       eax,[esp+24h]
  004a2f35  83c408                add       esp,8
  004a2f38  85c0                  test      eax,eax
  004a2f3a  0f8407030000          je        near 004A3247h
  004a2f40  f70617080000          test      dword [esi],817h
  004a2f46  0f85fb020000          jne       near 004A3247h
  004a2f4c  8a4643                mov       al,[esi+43h]
  004a2f4f  84c0                  test      al,al
  004a2f51  0f85f0020000          jne       near 004A3247h
  004a2f57  8b466c                mov       eax,[esi+6Ch]
  004a2f5a  8b561c                mov       edx,[esi+1Ch]
  004a2f5d  8b781c                mov       edi,[eax+1Ch]
  004a2f60  8b6824                mov       ebp,[eax+24h]
  004a2f63  8b4624                mov       eax,[esi+24h]
  004a2f66  2bfa                  sub       edi,edx
  004a2f68  2be8                  sub       ebp,eax
  004a2f6a  c1ff0c                sar       edi,0Ch
  004a2f6d  c1fd0c                sar       ebp,0Ch
  004a2f70  8bd7                  mov       edx,edi
  004a2f72  8bc5                  mov       eax,ebp
  004a2f74  0fafd7                imul      edx,edi
  004a2f77  0fafc5                imul      eax,ebp
  004a2f7a  03d0                  add       edx,eax
  004a2f7c  52                    push      edx
  004a2f7d  e88e2a0a00            call      00545A10h
  004a2f82  55                    push      ebp
  004a2f83  57                    push      edi
  004a2f84  8944241c              mov       [esp+1Ch],eax
  004a2f88  e8952a0a00            call      00545A22h
  004a2f8d  668b4e32              mov       cx,[esi+32h]
  004a2f91  2bc1                  sub       eax,ecx
  004a2f93  25ff0f0000            and       eax,0FFFh
  004a2f98  89442440              mov       [esp+40h],eax
  004a2f9c  8b442414              mov       eax,[esp+14h]
  004a2fa0  3d2c010000            cmp       eax,12Ch
  004a2fa5  0f8d4a010000          jge       near 004A30F5h
  004a2fab  e890410a00            call      00547140h
  004a2fb0  99                    cdq
  004a2fb1  b964000000            mov       ecx,64h
  004a2fb6  f7f9                  idiv      ecx
  004a2fb8  83fa32                cmp       edx,32h
  004a2fbb  0f8ddb000000          jge       near 004A309Ch
  004a2fc1  8b542440              mov       edx,[esp+40h]
  004a2fc5  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004a2fcc  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004a2fd3  25ff0f0000            and       eax,0FFFh
  004a2fd8  3bc1                  cmp       eax,ecx
  004a2fda  7f4f                  jg        short 004A302Bh
  004a2fdc  e85f410a00            call      00547140h
  004a2fe1  99                    cdq
  004a2fe2  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004a2fe6  f7f9                  idiv      ecx
  004a2fe8  8b442414              mov       eax,[esp+14h]
  004a2fec  81c22c010000          add       edx,12Ch
  004a2ff2  3bc2                  cmp       eax,edx
  004a2ff4  7d35                  jge       short 004A302Bh
  004a2ff6  8b16                  mov       edx,[esi]
  004a2ff8  33c9                  xor       ecx,ecx
  004a2ffa  81e2fffffffe          and       edx,0FEFFFFFFh
  004a3000  c7461803000000        mov       dword [esi+18h],3
  004a3007  83ca02                or        edx,2
  004a300a  3d96000000            cmp       eax,96h
  004a300f  0f9dc1                setge     cl
  004a3012  41                    inc       ecx
  004a3013  8916                  mov       [esi],edx
  004a3015  c6464200              mov       byte [esi+42h],0
  004a3019  884e43                mov       [esi+43h],cl
  004a301c  c7465c00000000        mov       dword [esi+5Ch],0
  004a3023  5f                    pop       edi
  004a3024  5e                    pop       esi
  004a3025  5d                    pop       ebp
  004a3026  5b                    pop       ebx
  004a3027  83c42c                add       esp,2Ch
  004a302a  c3                    ret

LAB_004a302b:
  004a302b  8b542440              mov       edx,[esp+40h]
  004a302f  8d0cdd00060000        lea       ecx,[ebx*8+600h]
  004a3036  8d849a00030000        lea       eax,[edx+ebx*4+300h]
  004a303d  25ff0f0000            and       eax,0FFFh
  004a3042  3bc1                  cmp       eax,ecx
  004a3044  0f8fe0000000          jg        near 004A312Ah
  004a304a  e8f1400a00            call      00547140h
  004a304f  99                    cdq
  004a3050  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004a3054  f7f9                  idiv      ecx
  004a3056  8b442414              mov       eax,[esp+14h]
  004a305a  81c22c010000          add       edx,12Ch
  004a3060  3bc2                  cmp       eax,edx
  004a3062  0f8dc2000000          jge       near 004A312Ah
  004a3068  8b16                  mov       edx,[esi]
  004a306a  33c9                  xor       ecx,ecx
  004a306c  81e2fffffffe          and       edx,0FEFFFFFFh
  004a3072  c7461805000000        mov       dword [esi+18h],5
  004a3079  83ca02                or        edx,2
  004a307c  3d96000000            cmp       eax,96h
  004a3081  0f9dc1                setge     cl
  004a3084  41                    inc       ecx
  004a3085  8916                  mov       [esi],edx
  004a3087  c6464201              mov       byte [esi+42h],1
  004a308b  884e43                mov       [esi+43h],cl
  004a308e  66c746100100          mov       word [esi+10h],1
  004a3094  5f                    pop       edi
  004a3095  5e                    pop       esi
  004a3096  5d                    pop       ebp
  004a3097  5b                    pop       ebx
  004a3098  83c42c                add       esp,2Ch
  004a309b  c3                    ret

LAB_004a309c:
  004a309c  83fa4b                cmp       edx,4Bh
  004a309f  0f8c20010000          jl        near 004A31C5h
  004a30a5  c6464203              mov       byte [esi+42h],3
  004a30a9  e892400a00            call      00547140h
  004a30ae  99                    cdq
  004a30af  b90a000000            mov       ecx,0Ah
  004a30b4  f7f9                  idiv      ecx
  004a30b6  d1fb                  sar       ebx,1
  004a30b8  02d3                  add       dl,bl
  004a30ba  80c214                add       dl,14h
  004a30bd  885643                mov       [esi+43h],dl
  004a30c0  8b542410              mov       edx,[esp+10h]
  004a30c4  8dba80000000          lea       edi,[edx+80h]
  004a30ca  c1e704                shl       edi,4
  004a30cd  e86e400a00            call      00547140h
  004a30d2  99                    cdq
  004a30d3  f7ff                  idiv      edi
  004a30d5  d1ff                  sar       edi,1
  004a30d7  2bd7                  sub       edx,edi
  004a30d9  8b7c2440              mov       edi,[esp+40h]
  004a30dd  03d7                  add       edx,edi
  004a30df  66035632              add       dx,[esi+32h]
  004a30e3  81c200080000          add       edx,800h
  004a30e9  66895610              mov       [esi+10h],dx
  004a30ed  5f                    pop       edi
  004a30ee  5e                    pop       esi
  004a30ef  5d                    pop       ebp
  004a30f0  5b                    pop       ebx
  004a30f1  83c42c                add       esp,2Ch
  004a30f4  c3                    ret

LAB_004a30f5:
  004a30f5  3d58020000            cmp       eax,258h
  004a30fa  0f8dee000000          jge       near 004A31EEh
  004a3100  e83b400a00            call      00547140h
  004a3105  99                    cdq
  004a3106  b964000000            mov       ecx,64h
  004a310b  f7f9                  idiv      ecx
  004a310d  83fa2d                cmp       edx,2Dh
  004a3110  7d5f                  jge       short 004A3171h
  004a3112  8b542440              mov       edx,[esp+40h]
  004a3116  81c200040000          add       edx,400h
  004a311c  81e2ff0f0000          and       edx,0FFFh
  004a3122  81fa00080000          cmp       edx,800h
  004a3128  7c10                  jl        short 004A313Ah

LAB_004a312a:
  004a312a  c6464201              mov       byte [esi+42h],1
  004a312e  c6464301              mov       byte [esi+43h],1
  004a3132  5f                    pop       edi
  004a3133  5e                    pop       esi
  004a3134  5d                    pop       ebp
  004a3135  5b                    pop       ebx
  004a3136  83c42c                add       esp,2Ch
  004a3139  c3                    ret

LAB_004a313a:
  004a313a  8b06                  mov       eax,[esi]
  004a313c  8b542414              mov       edx,[esp+14h]
  004a3140  25fffffffe            and       eax,0FEFFFFFFh
  004a3145  33c9                  xor       ecx,ecx
  004a3147  0c02                  or        al,2
  004a3149  81fa96000000          cmp       edx,96h
  004a314f  0f9dc1                setge     cl
  004a3152  41                    inc       ecx
  004a3153  8906                  mov       [esi],eax
  004a3155  c7461805000000        mov       dword [esi+18h],5
  004a315c  c6464201              mov       byte [esi+42h],1
  004a3160  884e43                mov       [esi+43h],cl
  004a3163  66c746100100          mov       word [esi+10h],1
  004a3169  5f                    pop       edi
  004a316a  5e                    pop       esi
  004a316b  5d                    pop       ebp
  004a316c  5b                    pop       ebx
  004a316d  83c42c                add       esp,2Ch
  004a3170  c3                    ret

LAB_004a3171:
  004a3171  83fa46                cmp       edx,46h
  004a3174  7d4a                  jge       short 004A31C0h
  004a3176  c6464203              mov       byte [esi+42h],3
  004a317a  e8c13f0a00            call      00547140h
  004a317f  99                    cdq
  004a3180  b90a000000            mov       ecx,0Ah
  004a3185  f7f9                  idiv      ecx
  004a3187  d1fb                  sar       ebx,1
  004a3189  02d3                  add       dl,bl
  004a318b  80c214                add       dl,14h
  004a318e  885643                mov       [esi+43h],dl
  004a3191  8b542410              mov       edx,[esp+10h]
  004a3195  8dba80000000          lea       edi,[edx+80h]
  004a319b  c1e704                shl       edi,4
  004a319e  e89d3f0a00            call      00547140h
  004a31a3  99                    cdq
  004a31a4  f7ff                  idiv      edi
  004a31a6  8b4c2440              mov       ecx,[esp+40h]
  004a31aa  d1ff                  sar       edi,1
  004a31ac  2bd7                  sub       edx,edi
  004a31ae  03d1                  add       edx,ecx
  004a31b0  66035632              add       dx,[esi+32h]
  004a31b4  66895610              mov       [esi+10h],dx
  004a31b8  5f                    pop       edi
  004a31b9  5e                    pop       esi
  004a31ba  5d                    pop       ebp
  004a31bb  5b                    pop       ebx
  004a31bc  83c42c                add       esp,2Ch
  004a31bf  c3                    ret

LAB_004a31c0:
  004a31c0  83fa5a                cmp       edx,5Ah
  004a31c3  7d3b                  jge       short 004A3200h

LAB_004a31c5:
  004a31c5  c6464202              mov       byte [esi+42h],2
  004a31c9  e8723f0a00            call      00547140h
  004a31ce  99                    cdq
  004a31cf  b90a000000            mov       ecx,0Ah
  004a31d4  66c746106400          mov       word [esi+10h],64h
  004a31da  f7f9                  idiv      ecx
  004a31dc  d1fb                  sar       ebx,1
  004a31de  02d3                  add       dl,bl
  004a31e0  80c214                add       dl,14h
  004a31e3  885643                mov       [esi+43h],dl
  004a31e6  5f                    pop       edi
  004a31e7  5e                    pop       esi
  004a31e8  5d                    pop       ebp
  004a31e9  5b                    pop       ebx
  004a31ea  83c42c                add       esp,2Ch
  004a31ed  c3                    ret

LAB_004a31ee:
  004a31ee  e84d3f0a00            call      00547140h
  004a31f3  99                    cdq
  004a31f4  b964000000            mov       ecx,64h
  004a31f9  f7f9                  idiv      ecx
  004a31fb  83fa32                cmp       edx,32h
  004a31fe  7d23                  jge       short 004A3223h

LAB_004a3200:
  004a3200  c6464200              mov       byte [esi+42h],0
  004a3204  e8373f0a00            call      00547140h
  004a3209  99                    cdq
  004a320a  b90a000000            mov       ecx,0Ah
  004a320f  f7f9                  idiv      ecx
  004a3211  d1fb                  sar       ebx,1
  004a3213  02d3                  add       dl,bl
  004a3215  80c214                add       dl,14h
  004a3218  885643                mov       [esi+43h],dl
  004a321b  5f                    pop       edi
  004a321c  5e                    pop       esi
  004a321d  5d                    pop       ebp
  004a321e  5b                    pop       ebx
  004a321f  83c42c                add       esp,2Ch
  004a3222  c3                    ret

LAB_004a3223:
  004a3223  c6464203              mov       byte [esi+42h],3
  004a3227  e8143f0a00            call      00547140h
  004a322c  99                    cdq
  004a322d  b90a000000            mov       ecx,0Ah
  004a3232  f7f9                  idiv      ecx
  004a3234  d1fb                  sar       ebx,1
  004a3236  02d3                  add       dl,bl
  004a3238  80c214                add       dl,14h
  004a323b  885643                mov       [esi+43h],dl
  004a323e  e8fd3e0a00            call      00547140h
  004a3243  66894610              mov       [esi+10h],ax

LAB_004a3247:
  004a3247  5f                    pop       edi
  004a3248  5e                    pop       esi
  004a3249  5d                    pop       ebp
  004a324a  5b                    pop       ebx
  004a324b  83c42c                add       esp,2Ch
  004a324e  c3                    ret

LAB_004a324f:
  004a324f  668b561a              mov       dx,[esi+1Ah]
  004a3253  8b7c2410              mov       edi,[esp+10h]
  004a3257  6683fa0c              cmp       dx,0Ch
  004a325b  7d39                  jge       short 004A3296h
  004a325d  8b4c2440              mov       ecx,[esp+40h]
  004a3261  8bc7                  mov       eax,edi
  004a3263  d1f8                  sar       eax,1
  004a3265  83c040                add       eax,40h
  004a3268  8d1c08                lea       ebx,[eax+ecx]
  004a326b  8d2c00                lea       ebp,[eax+eax]
  004a326e  81e3ff0f0000          and       ebx,0FFFh
  004a3274  3bdd                  cmp       ebx,ebp
  004a3276  7f06                  jg        short 004A327Eh
  004a3278  66014e32              add       [esi+32h],cx
  004a327c  eb14                  jmp       short 004A3292h

LAB_004a327e:
  004a327e  81e1ff0f0000          and       ecx,0FFFh
  004a3284  81f900080000          cmp       ecx,800h
  004a328a  7c02                  jl        short 004A328Eh
  004a328c  f7d8                  neg       eax

LAB_004a328e:
  004a328e  66014632              add       [esi+32h],ax

LAB_004a3292:
  004a3292  8b5c2418              mov       ebx,[esp+18h]

LAB_004a3296:
  004a3296  6683fa0c              cmp       dx,0Ch
  004a329a  7558                  jne       short 004A32F4h
  004a329c  807e4302              cmp       byte [esi+43h],2
  004a32a0  7552                  jne       short 004A32F4h
  004a32a2  0fbf4e32              movsx     ecx,word [esi+32h]
  004a32a6  81c700030000          add       edi,300h
  004a32ac  51                    push      ecx
  004a32ad  c1e707                shl       edi,7
  004a32b0  897e5c                mov       [esi+5Ch],edi
  004a32b3  e864270a00            call      00545A1Ch
  004a32b8  0faf465c              imul      eax,[esi+5Ch]
  004a32bc  0fbf5632              movsx     edx,word [esi+32h]
  004a32c0  8b6e50                mov       ebp,[esi+50h]
  004a32c3  52                    push      edx
  004a32c4  c1f80c                sar       eax,0Ch
  004a32c7  03e8                  add       ebp,eax
  004a32c9  896e50                mov       [esi+50h],ebp
  004a32cc  e845270a00            call      00545A16h
  004a32d1  0faf465c              imul      eax,[esi+5Ch]
  004a32d5  8b4e58                mov       ecx,[esi+58h]
  004a32d8  c7465400700100        mov       dword [esi+54h],17000h
  004a32df  c1f80c                sar       eax,0Ch
  004a32e2  03c8                  add       ecx,eax
  004a32e4  8b06                  mov       eax,[esi]
  004a32e6  0c01                  or        al,1
  004a32e8  894e58                mov       [esi+58h],ecx
  004a32eb  8906                  mov       [esi],eax
  004a32ed  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004a32f4:
  004a32f4  668b461a              mov       ax,[esi+1Ah]
  004a32f8  bd11000000            mov       ebp,11h
  004a32fd  663d1000              cmp       ax,10h
  004a3301  7c56                  jl        short 004A3359h
  004a3303  663bc5                cmp       ax,bp
  004a3306  7f51                  jg        short 004A3359h
  004a3308  6a00                  push      0
  004a330a  8d461c                lea       eax,[esi+1Ch]
  004a330d  68d0295500            push      5529D0h
  004a3312  50                    push      eax
  004a3313  56                    push      esi
  004a3314  e817690700            call      00519C30h
  004a3319  8bf8                  mov       edi,eax
  004a331b  a1b8e05d00            mov       eax,[5DE0B8h]
  004a3320  83c410                add       esp,10h
  004a3323  85c0                  test      eax,eax
  004a3325  7432                  je        short 004A3359h
  004a3327  8b0f                  mov       ecx,[edi]
  004a3329  57                    push      edi
  004a332a  83c940                or        ecx,40h
  004a332d  56                    push      esi
  004a332e  66c787960000001000    mov       word [edi+96h],10h
  004a3337  890f                  mov       [edi],ecx
  004a3339  e842680900            call      00539B80h
  004a333e  83c408                add       esp,8
  004a3341  83c71c                add       edi,1Ch
  004a3344  57                    push      edi
  004a3345  6800180000            push      1800h
  004a334a  6800030000            push      300h
  004a334f  6a03                  push      3
  004a3351  e8caa70900            call      0053DB20h
  004a3356  83c410                add       esp,10h

LAB_004a3359:
  004a3359  8b06                  mov       eax,[esi]
  004a335b  a801                  test      al,1
  004a335d  741a                  je        short 004A3379h
  004a335f  66396e1a              cmp       [esi+1Ah],bp
  004a3363  0f8ee8000000          jle       near 004A3451h
  004a3369  66896e1a              mov       [esi+1Ah],bp
  004a336d  c7465c00000000        mov       dword [esi+5Ch],0
  004a3374  e984fbffff            jmp       004A2EFDh

LAB_004a3379:
  004a3379  a900000002            test      eax,2000000h
  004a337e  0f84cd000000          je        near 004A3451h
  004a3384  24fd                  and       al,0FDh
  004a3386  c7465c00000000        mov       dword [esi+5Ch],0
  004a338d  8906                  mov       [esi],eax
  004a338f  e969fbffff            jmp       004A2EFDh

LAB_004a3394:
  004a3394  a811                  test      al,11h
  004a3396  0f8561fbffff          jne       near 004A2EFDh
  004a339c  a904080000            test      eax,804h
  004a33a1  0f8556fbffff          jne       near 004A2EFDh
  004a33a7  33c9                  xor       ecx,ecx
  004a33a9  8a4e42                mov       cl,[esi+42h]
  004a33ac  83f903                cmp       ecx,3
  004a33af  0f8748fbffff          ja        near 004A2EFDh
  004a33b5  ff248d94354a00        jmp       dword [ecx*4+4A3594h]
  004a33bc  33c9                  xor       ecx,ecx
  004a33be  66394e18              cmp       [esi+18h],cx
  004a33c2  740a                  je        short 004A33CEh
  004a33c4  0d00000001            or        eax,1000000h
  004a33c9  894e18                mov       [esi+18h],ecx
  004a33cc  8906                  mov       [esi],eax

LAB_004a33ce:
  004a33ce  817c24142c010000      cmp       dword [esp+14h],12Ch
  004a33d6  7d09                  jge       short 004A33E1h
  004a33d8  c6464300              mov       byte [esi+43h],0
  004a33dc  e91cfbffff            jmp       004A2EFDh

LAB_004a33e1:
  004a33e1  8a4643                mov       al,[esi+43h]
  004a33e4  84c0                  test      al,al
  004a33e6  7405                  je        short 004A33EDh
  004a33e8  fec8                  dec       al
  004a33ea  884643                mov       [esi+43h],al

LAB_004a33ed:
  004a33ed  894e5c                mov       [esi+5Ch],ecx
  004a33f0  e908fbffff            jmp       004A2EFDh
  004a33f5  b901000000            mov       ecx,1
  004a33fa  66394e18              cmp       [esi+18h],cx
  004a33fe  740a                  je        short 004A340Ah
  004a3400  0d00000001            or        eax,1000000h
  004a3405  894e18                mov       [esi+18h],ecx
  004a3408  8906                  mov       [esi],eax

LAB_004a340a:
  004a340a  8b4c2410              mov       ecx,[esp+10h]
  004a340e  8d840980000000        lea       eax,[ecx+ecx+80h]
  004a3415  8b4c2440              mov       ecx,[esp+40h]
  004a3419  8d1408                lea       edx,[eax+ecx]
  004a341c  8d3c00                lea       edi,[eax+eax]
  004a341f  81e2ff0f0000          and       edx,0FFFh
  004a3425  3bd7                  cmp       edx,edi
  004a3427  7f14                  jg        short 004A343Dh
  004a3429  66014e32              add       [esi+32h],cx
  004a342d  c6464300              mov       byte [esi+43h],0
  004a3431  c7465c00000000        mov       dword [esi+5Ch],0
  004a3438  e9c0faffff            jmp       004A2EFDh

LAB_004a343d:
  004a343d  81e1ff0f0000          and       ecx,0FFFh
  004a3443  81f900080000          cmp       ecx,800h
  004a3449  7c02                  jl        short 004A344Dh
  004a344b  f7d8                  neg       eax

LAB_004a344d:
  004a344d  66014632              add       [esi+32h],ax

LAB_004a3451:
  004a3451  c7465c00000000        mov       dword [esi+5Ch],0
  004a3458  e9a0faffff            jmp       004A2EFDh
  004a345d  b901000000            mov       ecx,1
  004a3462  66394e18              cmp       [esi+18h],cx
  004a3466  740a                  je        short 004A3472h
  004a3468  0d00000001            or        eax,1000000h
  004a346d  894e18                mov       [esi+18h],ecx
  004a3470  8906                  mov       [esi],eax

LAB_004a3472:
  004a3472  0fbf4610              movsx     eax,word [esi+10h]
  004a3476  39442414              cmp       [esp+14h],eax
  004a347a  7d09                  jge       short 004A3485h
  004a347c  c6464300              mov       byte [esi+43h],0
  004a3480  e978faffff            jmp       004A2EFDh

LAB_004a3485:
  004a3485  8a4643                mov       al,[esi+43h]
  004a3488  84c0                  test      al,al
  004a348a  7405                  je        short 004A3491h
  004a348c  fec8                  dec       al
  004a348e  884643                mov       [esi+43h],al

LAB_004a3491:
  004a3491  8b542410              mov       edx,[esp+10h]
  004a3495  8b4c2440              mov       ecx,[esp+40h]
  004a3499  8d841280000000        lea       eax,[edx+edx+80h]
  004a34a0  8d3c08                lea       edi,[eax+ecx]
  004a34a3  8d2c00                lea       ebp,[eax+eax]
  004a34a6  81e7ff0f0000          and       edi,0FFFh
  004a34ac  3bfd                  cmp       edi,ebp
  004a34ae  7f15                  jg        short 004A34C5h
  004a34b0  66014e32              add       [esi+32h],cx
  004a34b4  81c200010000          add       edx,100h
  004a34ba  c1e207                shl       edx,7
  004a34bd  89565c                mov       [esi+5Ch],edx
  004a34c0  e938faffff            jmp       004A2EFDh

LAB_004a34c5:
  004a34c5  81e1ff0f0000          and       ecx,0FFFh
  004a34cb  81f900080000          cmp       ecx,800h
  004a34d1  7c02                  jl        short 004A34D5h
  004a34d3  f7d8                  neg       eax

LAB_004a34d5:
  004a34d5  66014632              add       [esi+32h],ax
  004a34d9  81c200010000          add       edx,100h
  004a34df  c1e207                shl       edx,7
  004a34e2  89565c                mov       [esi+5Ch],edx
  004a34e5  e913faffff            jmp       004A2EFDh
  004a34ea  8a4e43                mov       cl,[esi+43h]
  004a34ed  84c9                  test      cl,cl
  004a34ef  7405                  je        short 004A34F6h
  004a34f1  fec9                  dec       cl
  004a34f3  884e43                mov       [esi+43h],cl

LAB_004a34f6:
  004a34f6  b901000000            mov       ecx,1
  004a34fb  66394e18              cmp       [esi+18h],cx
  004a34ff  740a                  je        short 004A350Bh
  004a3501  0d00000001            or        eax,1000000h
  004a3506  894e18                mov       [esi+18h],ecx
  004a3509  8906                  mov       [esi],eax

LAB_004a350b:
  004a350b  8b4c2410              mov       ecx,[esp+10h]
  004a350f  0fbf5610              movsx     edx,word [esi+10h]
  004a3513  81c1e0000000          add       ecx,0E0h
  004a3519  52                    push      edx
  004a351a  c1e107                shl       ecx,7
  004a351d  894e5c                mov       [esi+5Ch],ecx
  004a3520  e8f7240a00            call      00545A1Ch
  004a3525  0faf465c              imul      eax,[esi+5Ch]
  004a3529  8b7e1c                mov       edi,[esi+1Ch]
  004a352c  c1f80c                sar       eax,0Ch
  004a352f  03f8                  add       edi,eax
  004a3531  0fbf4610              movsx     eax,word [esi+10h]
  004a3535  50                    push      eax
  004a3536  897e1c                mov       [esi+1Ch],edi
  004a3539  e8d8240a00            call      00545A16h
  004a353e  0faf465c              imul      eax,[esi+5Ch]
  004a3542  8b6e24                mov       ebp,[esi+24h]
  004a3545  8b4c2410              mov       ecx,[esp+10h]
  004a3549  c1f80c                sar       eax,0Ch
  004a354c  03e8                  add       ebp,eax
  004a354e  c7465c00000000        mov       dword [esi+5Ch],0
  004a3555  896e24                mov       [esi+24h],ebp
  004a3558  8d8180000000          lea       eax,[ecx+80h]

LAB_004a355e:
  004a355e  8b4c2440              mov       ecx,[esp+40h]
  004a3562  8d3c00                lea       edi,[eax+eax]
  004a3565  8d1408                lea       edx,[eax+ecx]
  004a3568  81e2ff0f0000          and       edx,0FFFh
  004a356e  3bd7                  cmp       edx,edi
  004a3570  7f09                  jg        short 004A357Bh
  004a3572  66014e32              add       [esi+32h],cx
  004a3576  e982f9ffff            jmp       004A2EFDh

LAB_004a357b:
  004a357b  81e1ff0f0000          and       ecx,0FFFh
  004a3581  81f900080000          cmp       ecx,800h
  004a3587  7c02                  jl        short 004A358Bh
  004a3589  f7d8                  neg       eax

LAB_004a358b:
  004a358b  66014632              add       [esi+32h],ax
  004a358f  e969f9ffff            jmp       004A2EFDh

47 cible(s) de saut interne : 0x004a2d17, 0x004a2da0, 0x004a2da4, 0x004a2db2, 0x004a2dc1, 0x004a2e02, 0x004a2eaf, 0x004a2ef9, 0x004a2efd, 0x004a302b, 0x004a309c, 0x004a30f5, 0x004a312a, 0x004a313a, 0x004a3171, 0x004a31c0, 0x004a31c5, 0x004a31ee, 0x004a3200, 0x004a3223, 0x004a3247, 0x004a324f, 0x004a327e, 0x004a328e, 0x004a3292, 0x004a3296, 0x004a32f4, 0x004a3359, 0x004a3379, 0x004a3394, 0x004a33ce, 0x004a33e1, 0x004a33ed, 0x004a340a, 0x004a343d, 0x004a344d, 0x004a3451, 0x004a3472, 0x004a3485, 0x004a3491, 0x004a34c5, 0x004a34d5, 0x004a34f6, 0x004a350b, 0x004a355e, 0x004a357b, 0x004a358b
