Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00495c90  section .text  offset fichier 0x95090  2336 octets

  00495c90  83ec2c                sub       esp,2Ch
  00495c93  53                    push      ebx
  00495c94  55                    push      ebp
  00495c95  56                    push      esi
  00495c96  8b74243c              mov       esi,[esp+3Ch]
  00495c9a  8d44241c              lea       eax,[esp+1Ch]
  00495c9e  57                    push      edi
  00495c9f  8d4c2438              lea       ecx,[esp+38h]
  00495ca3  50                    push      eax
  00495ca4  8d542438              lea       edx,[esp+38h]
  00495ca8  51                    push      ecx
  00495ca9  52                    push      edx
  00495caa  56                    push      esi
  00495cab  e8d02c0a00            call      00538980h
  00495cb0  83c410                add       esp,10h
  00495cb3  8d442424              lea       eax,[esp+24h]
  00495cb7  8d4c241c              lea       ecx,[esp+1Ch]
  00495cbb  8d542440              lea       edx,[esp+40h]
  00495cbf  50                    push      eax
  00495cc0  51                    push      ecx
  00495cc1  8d44241c              lea       eax,[esp+1Ch]
  00495cc5  52                    push      edx
  00495cc6  8d4c241c              lea       ecx,[esp+1Ch]
  00495cca  50                    push      eax
  00495ccb  51                    push      ecx
  00495ccc  56                    push      esi
  00495ccd  e84e020a00            call      00535F20h
  00495cd2  8b4c2428              mov       ecx,[esp+28h]
  00495cd6  8b442434              mov       eax,[esp+34h]
  00495cda  bb40000000            mov       ebx,40h
  00495cdf  83c418                add       esp,18h
  00495ce2  2bd9                  sub       ebx,ecx
  00495ce4  85c0                  test      eax,eax
  00495ce6  895c2418              mov       [esp+18h],ebx
  00495cea  0f840d020000          je        near 00495EFDh
  00495cf0  56                    push      esi
  00495cf1  e8aa580400            call      004DB5A0h
  00495cf6  8b06                  mov       eax,[esi]
  00495cf8  83c404                add       esp,4
  00495cfb  a802                  test      al,2
  00495cfd  0f8491060000          je        near 00496394h
  00495d03  f644242410            test      byte [esp+24h],10h
  00495d08  740d                  je        short 00495D17h
  00495d0a  24fd                  and       al,0FDh
  00495d0c  c6464300              mov       byte [esi+43h],0
  00495d10  8906                  mov       [esi],eax
  00495d12  e9e6010000            jmp       00495EFDh

LAB_00495d17:
  00495d17  33c9                  xor       ecx,ecx
  00495d19  8a4e42                mov       cl,[esi+42h]
  00495d1c  83e900                sub       ecx,0
  00495d1f  0f842a050000          je        near 0049624Fh
  00495d25  49                    dec       ecx
  00495d26  0f85d1010000          jne       near 00495EFDh
  00495d2c  0fbf4e10              movsx     ecx,word [esi+10h]
  00495d30  83e900                sub       ecx,0
  00495d33  747d                  je        short 00495DB2h
  00495d35  49                    dec       ecx
  00495d36  0f85c1010000          jne       near 00495EFDh
  00495d3c  33ff                  xor       edi,edi
  00495d3e  66837e1a07            cmp       word [esi+1Ah],7
  00495d43  897e5c                mov       [esi+5Ch],edi
  00495d46  755c                  jne       short 00495DA4h
  00495d48  807e4302              cmp       byte [esi+43h],2
  00495d4c  7552                  jne       short 00495DA0h
  00495d4e  8b542410              mov       edx,[esp+10h]
  00495d52  0fbf4632              movsx     eax,word [esi+32h]
  00495d56  81c240040000          add       edx,440h
  00495d5c  50                    push      eax
  00495d5d  c1e207                shl       edx,7
  00495d60  89565c                mov       [esi+5Ch],edx
  00495d63  e8b4fc0a00            call      00545A1Ch
  00495d68  0faf465c              imul      eax,[esi+5Ch]
  00495d6c  8b4e50                mov       ecx,[esi+50h]
  00495d6f  c1f80c                sar       eax,0Ch
  00495d72  03c8                  add       ecx,eax
  00495d74  894e50                mov       [esi+50h],ecx
  00495d77  0fbf4e32              movsx     ecx,word [esi+32h]
  00495d7b  51                    push      ecx
  00495d7c  e895fc0a00            call      00545A16h
  00495d81  0faf465c              imul      eax,[esi+5Ch]
  00495d85  8b4e58                mov       ecx,[esi+58h]
  00495d88  c7465400400100        mov       dword [esi+54h],14000h
  00495d8f  c1f80c                sar       eax,0Ch
  00495d92  03c8                  add       ecx,eax
  00495d94  8b06                  mov       eax,[esi]
  00495d96  0c01                  or        al,1
  00495d98  894e58                mov       [esi+58h],ecx
  00495d9b  8906                  mov       [esi],eax
  00495d9d  897e5c                mov       [esi+5Ch],edi

LAB_00495da0:
  00495da0  66ff4e10              dec       word [esi+10h]

LAB_00495da4:
  00495da4  8b442410              mov       eax,[esp+10h]
  00495da8  d1f8                  sar       eax,1
  00495daa  83c040                add       eax,40h
  00495dad  e9ac070000            jmp       0049655Eh

LAB_00495db2:
  00495db2  a900000002            test      eax,2000000h
  00495db7  7408                  je        short 00495DC1h
  00495db9  a801                  test      al,1
  00495dbb  7504                  jne       short 00495DC1h
  00495dbd  24dd                  and       al,0DDh
  00495dbf  8906                  mov       [esi],eax

LAB_00495dc1:
  00495dc1  668b461a              mov       ax,[esi+1Ah]
  00495dc5  663d0a00              cmp       ax,0Ah
  00495dc9  0f8c2e010000          jl        near 00495EFDh
  00495dcf  663d0e00              cmp       ax,0Eh
  00495dd3  0f8f24010000          jg        near 00495EFDh
  00495dd9  f60620                test      byte [esi],20h
  00495ddc  0f851b010000          jne       near 00495EFDh
  00495de2  8d6e1c                lea       ebp,[esi+1Ch]
  00495de5  6a00                  push      0
  00495de7  68fc285500            push      5528FCh
  00495dec  55                    push      ebp
  00495ded  56                    push      esi
  00495dee  e83d3e0800            call      00519C30h
  00495df3  8bf8                  mov       edi,eax
  00495df5  83c410                add       esp,10h
  00495df8  85ff                  test      edi,edi
  00495dfa  7406                  je        short 00495E02h
  00495dfc  8b06                  mov       eax,[esi]
  00495dfe  0c20                  or        al,20h
  00495e00  8906                  mov       [esi],eax

LAB_00495e02:
  00495e02  a1b8e05d00            mov       eax,[5DE0B8h]
  00495e07  85c0                  test      eax,eax
  00495e09  0f84ee000000          je        near 00495EFDh
  00495e0f  8b1f                  mov       ebx,[edi]
  00495e11  66c787960000001000    mov       word [edi+96h],10h
  00495e1a  83cb40                or        ebx,40h
  00495e1d  57                    push      edi
  00495e1e  891f                  mov       [edi],ebx
  00495e20  8b4c2424              mov       ecx,[esp+24h]
  00495e24  56                    push      esi
  00495e25  668b4130              mov       ax,[ecx+30h]
  00495e29  0fbfd8                movsx     ebx,ax
  00495e2c  66d1f8                sar       ax,1
  00495e2f  66894130              mov       [ecx+30h],ax
  00495e33  e8483d0a00            call      00539B80h
  00495e38  8b442428              mov       eax,[esp+28h]
  00495e3c  83c408                add       esp,8
  00495e3f  66895830              mov       [eax+30h],bx
  00495e43  8d5f1c                lea       ebx,[edi+1Ch]
  00495e46  53                    push      ebx
  00495e47  6800180000            push      1800h
  00495e4c  6800030000            push      300h
  00495e51  6a03                  push      3
  00495e53  e8c87c0a00            call      0053DB20h
  00495e58  8b07                  mov       eax,[edi]
  00495e5a  83c410                add       esp,10h
  00495e5d  a900000040            test      eax,40000000h
  00495e62  0f8591000000          jne       near 00495EF9h
  00495e68  0fbf4e32              movsx     ecx,word [esi+32h]
  00495e6c  8b542440              mov       edx,[esp+40h]
  00495e70  03d1                  add       edx,ecx
  00495e72  52                    push      edx
  00495e73  e8a4fb0a00            call      00545A1Ch
  00495e78  8d0440                lea       eax,[eax+eax*2]
  00495e7b  8d0480                lea       eax,[eax+eax*4]
  00495e7e  d1e0                  shl       eax,1
  00495e80  894750                mov       [edi+50h],eax
  00495e83  8b542440              mov       edx,[esp+40h]
  00495e87  0fbf4e32              movsx     ecx,word [esi+32h]
  00495e8b  03d1                  add       edx,ecx
  00495e8d  52                    push      edx
  00495e8e  e883fb0a00            call      00545A16h
  00495e93  8d0440                lea       eax,[eax+eax*2]
  00495e96  8d0480                lea       eax,[eax+eax*4]
  00495e99  d1e0                  shl       eax,1
  00495e9b  894758                mov       [edi+58h],eax
  00495e9e  8b07                  mov       eax,[edi]
  00495ea0  a801                  test      al,1
  00495ea2  750b                  jne       short 00495EAFh
  00495ea4  0c01                  or        al,1
  00495ea6  c7475400800100        mov       dword [edi+54h],18000h
  00495ead  8907                  mov       [edi],eax

LAB_00495eaf:
  00495eaf  8b4d00                mov       ecx,[ebp]
  00495eb2  8b5620                mov       edx,[esi+20h]
  00495eb5  8b4624                mov       eax,[esi+24h]
  00495eb8  894c2428              mov       [esp+28h],ecx
  00495ebc  8b0b                  mov       ecx,[ebx]
  00495ebe  8954242c              mov       [esp+2Ch],edx
  00495ec2  894d00                mov       [ebp],ecx
  00495ec5  8b5720                mov       edx,[edi+20h]
  00495ec8  6aff                  push      0FFFFFFFFh
  00495eca  89442434              mov       [esp+34h],eax
  00495ece  895620                mov       [esi+20h],edx
  00495ed1  8b4724                mov       eax,[edi+24h]
  00495ed4  6a00                  push      0
  00495ed6  6a39                  push      39h
  00495ed8  56                    push      esi
  00495ed9  894624                mov       [esi+24h],eax
  00495edc  e8ef4afeff            call      0047A9D0h
  00495ee1  8b4c2438              mov       ecx,[esp+38h]
  00495ee5  8b54243c              mov       edx,[esp+3Ch]
  00495ee9  8b442440              mov       eax,[esp+40h]
  00495eed  83c410                add       esp,10h
  00495ef0  894d00                mov       [ebp],ecx
  00495ef3  895620                mov       [esi+20h],edx
  00495ef6  894624                mov       [esi+24h],eax

LAB_00495ef9:
  00495ef9  8b5c2418              mov       ebx,[esp+18h]

LAB_00495efd:
  00495efd  8b44241c              mov       eax,[esp+1Ch]
  00495f01  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00495f0a  50                    push      eax
  00495f0b  56                    push      esi
  00495f0c  e84f22ffff            call      00488160h
  00495f11  83c408                add       esp,8
  00495f14  56                    push      esi
  00495f15  e826da0900            call      00533940h
  00495f1a  83c404                add       esp,4
  00495f1d  56                    push      esi
  00495f1e  e8cd9b0400            call      004DFAF0h
  00495f23  8b4c2438              mov       ecx,[esp+38h]
  00495f27  83c404                add       esp,4
  00495f2a  51                    push      ecx
  00495f2b  56                    push      esi
  00495f2c  e83fd80400            call      004E3770h
  00495f31  8b442424              mov       eax,[esp+24h]
  00495f35  83c408                add       esp,8
  00495f38  85c0                  test      eax,eax
  00495f3a  0f8407030000          je        near 00496247h
  00495f40  f70617080000          test      dword [esi],817h
  00495f46  0f85fb020000          jne       near 00496247h
  00495f4c  8a4643                mov       al,[esi+43h]
  00495f4f  84c0                  test      al,al
  00495f51  0f85f0020000          jne       near 00496247h
  00495f57  8b466c                mov       eax,[esi+6Ch]
  00495f5a  8b561c                mov       edx,[esi+1Ch]
  00495f5d  8b781c                mov       edi,[eax+1Ch]
  00495f60  8b6824                mov       ebp,[eax+24h]
  00495f63  8b4624                mov       eax,[esi+24h]
  00495f66  2bfa                  sub       edi,edx
  00495f68  2be8                  sub       ebp,eax
  00495f6a  c1ff0c                sar       edi,0Ch
  00495f6d  c1fd0c                sar       ebp,0Ch
  00495f70  8bd7                  mov       edx,edi
  00495f72  8bc5                  mov       eax,ebp
  00495f74  0fafd7                imul      edx,edi
  00495f77  0fafc5                imul      eax,ebp
  00495f7a  03d0                  add       edx,eax
  00495f7c  52                    push      edx
  00495f7d  e88efa0a00            call      00545A10h
  00495f82  55                    push      ebp
  00495f83  57                    push      edi
  00495f84  8944241c              mov       [esp+1Ch],eax
  00495f88  e895fa0a00            call      00545A22h
  00495f8d  668b4e32              mov       cx,[esi+32h]
  00495f91  2bc1                  sub       eax,ecx
  00495f93  25ff0f0000            and       eax,0FFFh
  00495f98  89442440              mov       [esp+40h],eax
  00495f9c  8b442414              mov       eax,[esp+14h]
  00495fa0  3d2c010000            cmp       eax,12Ch
  00495fa5  0f8d4a010000          jge       near 004960F5h
  00495fab  e890110b00            call      00547140h
  00495fb0  99                    cdq
  00495fb1  b964000000            mov       ecx,64h
  00495fb6  f7f9                  idiv      ecx
  00495fb8  83fa32                cmp       edx,32h
  00495fbb  0f8ddb000000          jge       near 0049609Ch
  00495fc1  8b542440              mov       edx,[esp+40h]
  00495fc5  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  00495fcc  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  00495fd3  25ff0f0000            and       eax,0FFFh
  00495fd8  3bc1                  cmp       eax,ecx
  00495fda  7f4f                  jg        short 0049602Bh
  00495fdc  e85f110b00            call      00547140h
  00495fe1  99                    cdq
  00495fe2  8d4c1b01              lea       ecx,[ebx+ebx+1]
  00495fe6  f7f9                  idiv      ecx
  00495fe8  8b442414              mov       eax,[esp+14h]
  00495fec  81c22c010000          add       edx,12Ch
  00495ff2  3bc2                  cmp       eax,edx
  00495ff4  7d35                  jge       short 0049602Bh
  00495ff6  8b16                  mov       edx,[esi]
  00495ff8  33c9                  xor       ecx,ecx
  00495ffa  81e2fffffffe          and       edx,0FEFFFFFFh
  00496000  c7461803000000        mov       dword [esi+18h],3
  00496007  83ca02                or        edx,2
  0049600a  3d96000000            cmp       eax,96h
  0049600f  0f9dc1                setge     cl
  00496012  41                    inc       ecx
  00496013  8916                  mov       [esi],edx
  00496015  c6464200              mov       byte [esi+42h],0
  00496019  884e43                mov       [esi+43h],cl
  0049601c  c7465c00000000        mov       dword [esi+5Ch],0
  00496023  5f                    pop       edi
  00496024  5e                    pop       esi
  00496025  5d                    pop       ebp
  00496026  5b                    pop       ebx
  00496027  83c42c                add       esp,2Ch
  0049602a  c3                    ret

LAB_0049602b:
  0049602b  8b542440              mov       edx,[esp+40h]
  0049602f  8d0cdd00060000        lea       ecx,[ebx*8+600h]
  00496036  8d849a00030000        lea       eax,[edx+ebx*4+300h]
  0049603d  25ff0f0000            and       eax,0FFFh
  00496042  3bc1                  cmp       eax,ecx
  00496044  0f8fe0000000          jg        near 0049612Ah
  0049604a  e8f1100b00            call      00547140h
  0049604f  99                    cdq
  00496050  8d4c1b01              lea       ecx,[ebx+ebx+1]
  00496054  f7f9                  idiv      ecx
  00496056  8b442414              mov       eax,[esp+14h]
  0049605a  81c22c010000          add       edx,12Ch
  00496060  3bc2                  cmp       eax,edx
  00496062  0f8dc2000000          jge       near 0049612Ah
  00496068  8b16                  mov       edx,[esi]
  0049606a  33c9                  xor       ecx,ecx
  0049606c  81e2fffffffe          and       edx,0FEFFFFFFh
  00496072  c7461805000000        mov       dword [esi+18h],5
  00496079  83ca02                or        edx,2
  0049607c  3d96000000            cmp       eax,96h
  00496081  0f9dc1                setge     cl
  00496084  41                    inc       ecx
  00496085  8916                  mov       [esi],edx
  00496087  c6464201              mov       byte [esi+42h],1
  0049608b  884e43                mov       [esi+43h],cl
  0049608e  66c746100100          mov       word [esi+10h],1
  00496094  5f                    pop       edi
  00496095  5e                    pop       esi
  00496096  5d                    pop       ebp
  00496097  5b                    pop       ebx
  00496098  83c42c                add       esp,2Ch
  0049609b  c3                    ret

LAB_0049609c:
  0049609c  83fa4b                cmp       edx,4Bh
  0049609f  0f8c20010000          jl        near 004961C5h
  004960a5  c6464203              mov       byte [esi+42h],3
  004960a9  e892100b00            call      00547140h
  004960ae  99                    cdq
  004960af  b90a000000            mov       ecx,0Ah
  004960b4  f7f9                  idiv      ecx
  004960b6  d1fb                  sar       ebx,1
  004960b8  02d3                  add       dl,bl
  004960ba  80c214                add       dl,14h
  004960bd  885643                mov       [esi+43h],dl
  004960c0  8b542410              mov       edx,[esp+10h]
  004960c4  8dba80000000          lea       edi,[edx+80h]
  004960ca  c1e704                shl       edi,4
  004960cd  e86e100b00            call      00547140h
  004960d2  99                    cdq
  004960d3  f7ff                  idiv      edi
  004960d5  d1ff                  sar       edi,1
  004960d7  2bd7                  sub       edx,edi
  004960d9  8b7c2440              mov       edi,[esp+40h]
  004960dd  03d7                  add       edx,edi
  004960df  66035632              add       dx,[esi+32h]
  004960e3  81c200080000          add       edx,800h
  004960e9  66895610              mov       [esi+10h],dx
  004960ed  5f                    pop       edi
  004960ee  5e                    pop       esi
  004960ef  5d                    pop       ebp
  004960f0  5b                    pop       ebx
  004960f1  83c42c                add       esp,2Ch
  004960f4  c3                    ret

LAB_004960f5:
  004960f5  3d58020000            cmp       eax,258h
  004960fa  0f8dee000000          jge       near 004961EEh
  00496100  e83b100b00            call      00547140h
  00496105  99                    cdq
  00496106  b964000000            mov       ecx,64h
  0049610b  f7f9                  idiv      ecx
  0049610d  83fa2d                cmp       edx,2Dh
  00496110  7d5f                  jge       short 00496171h
  00496112  8b542440              mov       edx,[esp+40h]
  00496116  81c200040000          add       edx,400h
  0049611c  81e2ff0f0000          and       edx,0FFFh
  00496122  81fa00080000          cmp       edx,800h
  00496128  7c10                  jl        short 0049613Ah

LAB_0049612a:
  0049612a  c6464201              mov       byte [esi+42h],1
  0049612e  c6464301              mov       byte [esi+43h],1
  00496132  5f                    pop       edi
  00496133  5e                    pop       esi
  00496134  5d                    pop       ebp
  00496135  5b                    pop       ebx
  00496136  83c42c                add       esp,2Ch
  00496139  c3                    ret

LAB_0049613a:
  0049613a  8b06                  mov       eax,[esi]
  0049613c  8b542414              mov       edx,[esp+14h]
  00496140  25fffffffe            and       eax,0FEFFFFFFh
  00496145  33c9                  xor       ecx,ecx
  00496147  0c02                  or        al,2
  00496149  81fa96000000          cmp       edx,96h
  0049614f  0f9dc1                setge     cl
  00496152  41                    inc       ecx
  00496153  8906                  mov       [esi],eax
  00496155  c7461805000000        mov       dword [esi+18h],5
  0049615c  c6464201              mov       byte [esi+42h],1
  00496160  884e43                mov       [esi+43h],cl
  00496163  66c746100100          mov       word [esi+10h],1
  00496169  5f                    pop       edi
  0049616a  5e                    pop       esi
  0049616b  5d                    pop       ebp
  0049616c  5b                    pop       ebx
  0049616d  83c42c                add       esp,2Ch
  00496170  c3                    ret

LAB_00496171:
  00496171  83fa46                cmp       edx,46h
  00496174  7d4a                  jge       short 004961C0h
  00496176  c6464203              mov       byte [esi+42h],3
  0049617a  e8c10f0b00            call      00547140h
  0049617f  99                    cdq
  00496180  b90a000000            mov       ecx,0Ah
  00496185  f7f9                  idiv      ecx
  00496187  d1fb                  sar       ebx,1
  00496189  02d3                  add       dl,bl
  0049618b  80c214                add       dl,14h
  0049618e  885643                mov       [esi+43h],dl
  00496191  8b542410              mov       edx,[esp+10h]
  00496195  8dba80000000          lea       edi,[edx+80h]
  0049619b  c1e704                shl       edi,4
  0049619e  e89d0f0b00            call      00547140h
  004961a3  99                    cdq
  004961a4  f7ff                  idiv      edi
  004961a6  8b4c2440              mov       ecx,[esp+40h]
  004961aa  d1ff                  sar       edi,1
  004961ac  2bd7                  sub       edx,edi
  004961ae  03d1                  add       edx,ecx
  004961b0  66035632              add       dx,[esi+32h]
  004961b4  66895610              mov       [esi+10h],dx
  004961b8  5f                    pop       edi
  004961b9  5e                    pop       esi
  004961ba  5d                    pop       ebp
  004961bb  5b                    pop       ebx
  004961bc  83c42c                add       esp,2Ch
  004961bf  c3                    ret

LAB_004961c0:
  004961c0  83fa5a                cmp       edx,5Ah
  004961c3  7d3b                  jge       short 00496200h

LAB_004961c5:
  004961c5  c6464202              mov       byte [esi+42h],2
  004961c9  e8720f0b00            call      00547140h
  004961ce  99                    cdq
  004961cf  b90a000000            mov       ecx,0Ah
  004961d4  66c746106400          mov       word [esi+10h],64h
  004961da  f7f9                  idiv      ecx
  004961dc  d1fb                  sar       ebx,1
  004961de  02d3                  add       dl,bl
  004961e0  80c214                add       dl,14h
  004961e3  885643                mov       [esi+43h],dl
  004961e6  5f                    pop       edi
  004961e7  5e                    pop       esi
  004961e8  5d                    pop       ebp
  004961e9  5b                    pop       ebx
  004961ea  83c42c                add       esp,2Ch
  004961ed  c3                    ret

LAB_004961ee:
  004961ee  e84d0f0b00            call      00547140h
  004961f3  99                    cdq
  004961f4  b964000000            mov       ecx,64h
  004961f9  f7f9                  idiv      ecx
  004961fb  83fa32                cmp       edx,32h
  004961fe  7d23                  jge       short 00496223h

LAB_00496200:
  00496200  c6464200              mov       byte [esi+42h],0
  00496204  e8370f0b00            call      00547140h
  00496209  99                    cdq
  0049620a  b90a000000            mov       ecx,0Ah
  0049620f  f7f9                  idiv      ecx
  00496211  d1fb                  sar       ebx,1
  00496213  02d3                  add       dl,bl
  00496215  80c214                add       dl,14h
  00496218  885643                mov       [esi+43h],dl
  0049621b  5f                    pop       edi
  0049621c  5e                    pop       esi
  0049621d  5d                    pop       ebp
  0049621e  5b                    pop       ebx
  0049621f  83c42c                add       esp,2Ch
  00496222  c3                    ret

LAB_00496223:
  00496223  c6464203              mov       byte [esi+42h],3
  00496227  e8140f0b00            call      00547140h
  0049622c  99                    cdq
  0049622d  b90a000000            mov       ecx,0Ah
  00496232  f7f9                  idiv      ecx
  00496234  d1fb                  sar       ebx,1
  00496236  02d3                  add       dl,bl
  00496238  80c214                add       dl,14h
  0049623b  885643                mov       [esi+43h],dl
  0049623e  e8fd0e0b00            call      00547140h
  00496243  66894610              mov       [esi+10h],ax

LAB_00496247:
  00496247  5f                    pop       edi
  00496248  5e                    pop       esi
  00496249  5d                    pop       ebp
  0049624a  5b                    pop       ebx
  0049624b  83c42c                add       esp,2Ch
  0049624e  c3                    ret

LAB_0049624f:
  0049624f  668b561a              mov       dx,[esi+1Ah]
  00496253  8b7c2410              mov       edi,[esp+10h]
  00496257  6683fa0c              cmp       dx,0Ch
  0049625b  7d39                  jge       short 00496296h
  0049625d  8b4c2440              mov       ecx,[esp+40h]
  00496261  8bc7                  mov       eax,edi
  00496263  d1f8                  sar       eax,1
  00496265  83c040                add       eax,40h
  00496268  8d1c08                lea       ebx,[eax+ecx]
  0049626b  8d2c00                lea       ebp,[eax+eax]
  0049626e  81e3ff0f0000          and       ebx,0FFFh
  00496274  3bdd                  cmp       ebx,ebp
  00496276  7f06                  jg        short 0049627Eh
  00496278  66014e32              add       [esi+32h],cx
  0049627c  eb14                  jmp       short 00496292h

LAB_0049627e:
  0049627e  81e1ff0f0000          and       ecx,0FFFh
  00496284  81f900080000          cmp       ecx,800h
  0049628a  7c02                  jl        short 0049628Eh
  0049628c  f7d8                  neg       eax

LAB_0049628e:
  0049628e  66014632              add       [esi+32h],ax

LAB_00496292:
  00496292  8b5c2418              mov       ebx,[esp+18h]

LAB_00496296:
  00496296  6683fa0c              cmp       dx,0Ch
  0049629a  7558                  jne       short 004962F4h
  0049629c  807e4302              cmp       byte [esi+43h],2
  004962a0  7552                  jne       short 004962F4h
  004962a2  0fbf4e32              movsx     ecx,word [esi+32h]
  004962a6  81c700030000          add       edi,300h
  004962ac  51                    push      ecx
  004962ad  c1e707                shl       edi,7
  004962b0  897e5c                mov       [esi+5Ch],edi
  004962b3  e864f70a00            call      00545A1Ch
  004962b8  0faf465c              imul      eax,[esi+5Ch]
  004962bc  0fbf5632              movsx     edx,word [esi+32h]
  004962c0  8b6e50                mov       ebp,[esi+50h]
  004962c3  52                    push      edx
  004962c4  c1f80c                sar       eax,0Ch
  004962c7  03e8                  add       ebp,eax
  004962c9  896e50                mov       [esi+50h],ebp
  004962cc  e845f70a00            call      00545A16h
  004962d1  0faf465c              imul      eax,[esi+5Ch]
  004962d5  8b4e58                mov       ecx,[esi+58h]
  004962d8  c7465400700100        mov       dword [esi+54h],17000h
  004962df  c1f80c                sar       eax,0Ch
  004962e2  03c8                  add       ecx,eax
  004962e4  8b06                  mov       eax,[esi]
  004962e6  0c01                  or        al,1
  004962e8  894e58                mov       [esi+58h],ecx
  004962eb  8906                  mov       [esi],eax
  004962ed  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004962f4:
  004962f4  668b461a              mov       ax,[esi+1Ah]
  004962f8  bd11000000            mov       ebp,11h
  004962fd  663d1000              cmp       ax,10h
  00496301  7c56                  jl        short 00496359h
  00496303  663bc5                cmp       ax,bp
  00496306  7f51                  jg        short 00496359h
  00496308  6a00                  push      0
  0049630a  8d461c                lea       eax,[esi+1Ch]
  0049630d  68f0285500            push      5528F0h
  00496312  50                    push      eax
  00496313  56                    push      esi
  00496314  e817390800            call      00519C30h
  00496319  8bf8                  mov       edi,eax
  0049631b  a1b8e05d00            mov       eax,[5DE0B8h]
  00496320  83c410                add       esp,10h
  00496323  85c0                  test      eax,eax
  00496325  7432                  je        short 00496359h
  00496327  8b0f                  mov       ecx,[edi]
  00496329  57                    push      edi
  0049632a  83c940                or        ecx,40h
  0049632d  56                    push      esi
  0049632e  66c787960000001000    mov       word [edi+96h],10h
  00496337  890f                  mov       [edi],ecx
  00496339  e842380a00            call      00539B80h
  0049633e  83c408                add       esp,8
  00496341  83c71c                add       edi,1Ch
  00496344  57                    push      edi
  00496345  6800180000            push      1800h
  0049634a  6800030000            push      300h
  0049634f  6a03                  push      3
  00496351  e8ca770a00            call      0053DB20h
  00496356  83c410                add       esp,10h

LAB_00496359:
  00496359  8b06                  mov       eax,[esi]
  0049635b  a801                  test      al,1
  0049635d  741a                  je        short 00496379h
  0049635f  66396e1a              cmp       [esi+1Ah],bp
  00496363  0f8ee8000000          jle       near 00496451h
  00496369  66896e1a              mov       [esi+1Ah],bp
  0049636d  c7465c00000000        mov       dword [esi+5Ch],0
  00496374  e984fbffff            jmp       00495EFDh

LAB_00496379:
  00496379  a900000002            test      eax,2000000h
  0049637e  0f84cd000000          je        near 00496451h
  00496384  24fd                  and       al,0FDh
  00496386  c7465c00000000        mov       dword [esi+5Ch],0
  0049638d  8906                  mov       [esi],eax
  0049638f  e969fbffff            jmp       00495EFDh

LAB_00496394:
  00496394  a811                  test      al,11h
  00496396  0f8561fbffff          jne       near 00495EFDh
  0049639c  a904080000            test      eax,804h
  004963a1  0f8556fbffff          jne       near 00495EFDh
  004963a7  33c9                  xor       ecx,ecx
  004963a9  8a4e42                mov       cl,[esi+42h]
  004963ac  83f903                cmp       ecx,3
  004963af  0f8748fbffff          ja        near 00495EFDh
  004963b5  ff248d94654900        jmp       dword [ecx*4+496594h]
  004963bc  33c9                  xor       ecx,ecx
  004963be  66394e18              cmp       [esi+18h],cx
  004963c2  740a                  je        short 004963CEh
  004963c4  0d00000001            or        eax,1000000h
  004963c9  894e18                mov       [esi+18h],ecx
  004963cc  8906                  mov       [esi],eax

LAB_004963ce:
  004963ce  817c24142c010000      cmp       dword [esp+14h],12Ch
  004963d6  7d09                  jge       short 004963E1h
  004963d8  c6464300              mov       byte [esi+43h],0
  004963dc  e91cfbffff            jmp       00495EFDh

LAB_004963e1:
  004963e1  8a4643                mov       al,[esi+43h]
  004963e4  84c0                  test      al,al
  004963e6  7405                  je        short 004963EDh
  004963e8  fec8                  dec       al
  004963ea  884643                mov       [esi+43h],al

LAB_004963ed:
  004963ed  894e5c                mov       [esi+5Ch],ecx
  004963f0  e908fbffff            jmp       00495EFDh
  004963f5  b901000000            mov       ecx,1
  004963fa  66394e18              cmp       [esi+18h],cx
  004963fe  740a                  je        short 0049640Ah
  00496400  0d00000001            or        eax,1000000h
  00496405  894e18                mov       [esi+18h],ecx
  00496408  8906                  mov       [esi],eax

LAB_0049640a:
  0049640a  8b4c2410              mov       ecx,[esp+10h]
  0049640e  8d840980000000        lea       eax,[ecx+ecx+80h]
  00496415  8b4c2440              mov       ecx,[esp+40h]
  00496419  8d1408                lea       edx,[eax+ecx]
  0049641c  8d3c00                lea       edi,[eax+eax]
  0049641f  81e2ff0f0000          and       edx,0FFFh
  00496425  3bd7                  cmp       edx,edi
  00496427  7f14                  jg        short 0049643Dh
  00496429  66014e32              add       [esi+32h],cx
  0049642d  c6464300              mov       byte [esi+43h],0
  00496431  c7465c00000000        mov       dword [esi+5Ch],0
  00496438  e9c0faffff            jmp       00495EFDh

LAB_0049643d:
  0049643d  81e1ff0f0000          and       ecx,0FFFh
  00496443  81f900080000          cmp       ecx,800h
  00496449  7c02                  jl        short 0049644Dh
  0049644b  f7d8                  neg       eax

LAB_0049644d:
  0049644d  66014632              add       [esi+32h],ax

LAB_00496451:
  00496451  c7465c00000000        mov       dword [esi+5Ch],0
  00496458  e9a0faffff            jmp       00495EFDh
  0049645d  b901000000            mov       ecx,1
  00496462  66394e18              cmp       [esi+18h],cx
  00496466  740a                  je        short 00496472h
  00496468  0d00000001            or        eax,1000000h
  0049646d  894e18                mov       [esi+18h],ecx
  00496470  8906                  mov       [esi],eax

LAB_00496472:
  00496472  0fbf4610              movsx     eax,word [esi+10h]
  00496476  39442414              cmp       [esp+14h],eax
  0049647a  7d09                  jge       short 00496485h
  0049647c  c6464300              mov       byte [esi+43h],0
  00496480  e978faffff            jmp       00495EFDh

LAB_00496485:
  00496485  8a4643                mov       al,[esi+43h]
  00496488  84c0                  test      al,al
  0049648a  7405                  je        short 00496491h
  0049648c  fec8                  dec       al
  0049648e  884643                mov       [esi+43h],al

LAB_00496491:
  00496491  8b542410              mov       edx,[esp+10h]
  00496495  8b4c2440              mov       ecx,[esp+40h]
  00496499  8d841280000000        lea       eax,[edx+edx+80h]
  004964a0  8d3c08                lea       edi,[eax+ecx]
  004964a3  8d2c00                lea       ebp,[eax+eax]
  004964a6  81e7ff0f0000          and       edi,0FFFh
  004964ac  3bfd                  cmp       edi,ebp
  004964ae  7f15                  jg        short 004964C5h
  004964b0  66014e32              add       [esi+32h],cx
  004964b4  81c200010000          add       edx,100h
  004964ba  c1e207                shl       edx,7
  004964bd  89565c                mov       [esi+5Ch],edx
  004964c0  e938faffff            jmp       00495EFDh

LAB_004964c5:
  004964c5  81e1ff0f0000          and       ecx,0FFFh
  004964cb  81f900080000          cmp       ecx,800h
  004964d1  7c02                  jl        short 004964D5h
  004964d3  f7d8                  neg       eax

LAB_004964d5:
  004964d5  66014632              add       [esi+32h],ax
  004964d9  81c200010000          add       edx,100h
  004964df  c1e207                shl       edx,7
  004964e2  89565c                mov       [esi+5Ch],edx
  004964e5  e913faffff            jmp       00495EFDh
  004964ea  8a4e43                mov       cl,[esi+43h]
  004964ed  84c9                  test      cl,cl
  004964ef  7405                  je        short 004964F6h
  004964f1  fec9                  dec       cl
  004964f3  884e43                mov       [esi+43h],cl

LAB_004964f6:
  004964f6  b901000000            mov       ecx,1
  004964fb  66394e18              cmp       [esi+18h],cx
  004964ff  740a                  je        short 0049650Bh
  00496501  0d00000001            or        eax,1000000h
  00496506  894e18                mov       [esi+18h],ecx
  00496509  8906                  mov       [esi],eax

LAB_0049650b:
  0049650b  8b4c2410              mov       ecx,[esp+10h]
  0049650f  0fbf5610              movsx     edx,word [esi+10h]
  00496513  81c1e0000000          add       ecx,0E0h
  00496519  52                    push      edx
  0049651a  c1e107                shl       ecx,7
  0049651d  894e5c                mov       [esi+5Ch],ecx
  00496520  e8f7f40a00            call      00545A1Ch
  00496525  0faf465c              imul      eax,[esi+5Ch]
  00496529  8b7e1c                mov       edi,[esi+1Ch]
  0049652c  c1f80c                sar       eax,0Ch
  0049652f  03f8                  add       edi,eax
  00496531  0fbf4610              movsx     eax,word [esi+10h]
  00496535  50                    push      eax
  00496536  897e1c                mov       [esi+1Ch],edi
  00496539  e8d8f40a00            call      00545A16h
  0049653e  0faf465c              imul      eax,[esi+5Ch]
  00496542  8b6e24                mov       ebp,[esi+24h]
  00496545  8b4c2410              mov       ecx,[esp+10h]
  00496549  c1f80c                sar       eax,0Ch
  0049654c  03e8                  add       ebp,eax
  0049654e  c7465c00000000        mov       dword [esi+5Ch],0
  00496555  896e24                mov       [esi+24h],ebp
  00496558  8d8180000000          lea       eax,[ecx+80h]

LAB_0049655e:
  0049655e  8b4c2440              mov       ecx,[esp+40h]
  00496562  8d3c00                lea       edi,[eax+eax]
  00496565  8d1408                lea       edx,[eax+ecx]
  00496568  81e2ff0f0000          and       edx,0FFFh
  0049656e  3bd7                  cmp       edx,edi
  00496570  7f09                  jg        short 0049657Bh
  00496572  66014e32              add       [esi+32h],cx
  00496576  e982f9ffff            jmp       00495EFDh

LAB_0049657b:
  0049657b  81e1ff0f0000          and       ecx,0FFFh
  00496581  81f900080000          cmp       ecx,800h
  00496587  7c02                  jl        short 0049658Bh
  00496589  f7d8                  neg       eax

LAB_0049658b:
  0049658b  66014632              add       [esi+32h],ax
  0049658f  e969f9ffff            jmp       00495EFDh
  00496594  bc634900f5            mov       esp,0F5004963h
  00496599  634900                arpl      [ecx],cx
  0049659c  5d                    pop       ebp
  0049659d  6449                  dec       ecx
  0049659f  00ea                  add       dl,ch
  004965a1  6449                  dec       ecx
  004965a3  009090909090          add       [eax-6F6F6F70h],dl
  004965a9  90                    nop
  004965aa  90                    nop
  004965ab  90                    nop
  004965ac  90                    nop
  004965ad  90                    nop
  004965ae  90                    nop
  004965af  90                    nop

47 cible(s) de saut interne : 0x00495d17, 0x00495da0, 0x00495da4, 0x00495db2, 0x00495dc1, 0x00495e02, 0x00495eaf, 0x00495ef9, 0x00495efd, 0x0049602b, 0x0049609c, 0x004960f5, 0x0049612a, 0x0049613a, 0x00496171, 0x004961c0, 0x004961c5, 0x004961ee, 0x00496200, 0x00496223, 0x00496247, 0x0049624f, 0x0049627e, 0x0049628e, 0x00496292, 0x00496296, 0x004962f4, 0x00496359, 0x00496379, 0x00496394, 0x004963ce, 0x004963e1, 0x004963ed, 0x0049640a, 0x0049643d, 0x0049644d, 0x00496451, 0x00496472, 0x00496485, 0x00496491, 0x004964c5, 0x004964d5, 0x004964f6, 0x0049650b, 0x0049655e, 0x0049657b, 0x0049658b
