Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e37d0  section .text  offset fichier 0xe2bd0  2320 octets

  004e37d0  83ec2c                sub       esp,2Ch
  004e37d3  53                    push      ebx
  004e37d4  55                    push      ebp
  004e37d5  56                    push      esi
  004e37d6  8b74243c              mov       esi,[esp+3Ch]
  004e37da  8d44241c              lea       eax,[esp+1Ch]
  004e37de  57                    push      edi
  004e37df  8d4c2438              lea       ecx,[esp+38h]
  004e37e3  50                    push      eax
  004e37e4  8d542428              lea       edx,[esp+28h]
  004e37e8  51                    push      ecx
  004e37e9  52                    push      edx
  004e37ea  56                    push      esi
  004e37eb  e890510500            call      00538980h
  004e37f0  83c410                add       esp,10h
  004e37f3  8d442428              lea       eax,[esp+28h]
  004e37f7  8d4c241c              lea       ecx,[esp+1Ch]
  004e37fb  8d542410              lea       edx,[esp+10h]
  004e37ff  50                    push      eax
  004e3800  51                    push      ecx
  004e3801  8d442420              lea       eax,[esp+20h]
  004e3805  52                    push      edx
  004e3806  8d4c244c              lea       ecx,[esp+4Ch]
  004e380a  50                    push      eax
  004e380b  51                    push      ecx
  004e380c  56                    push      esi
  004e380d  e80e270500            call      00535F20h
  004e3812  8b4c2458              mov       ecx,[esp+58h]
  004e3816  b840000000            mov       eax,40h
  004e381b  2bc1                  sub       eax,ecx
  004e381d  83c418                add       esp,18h
  004e3820  89442414              mov       [esp+14h],eax
  004e3824  8b44241c              mov       eax,[esp+1Ch]
  004e3828  85c0                  test      eax,eax
  004e382a  bb03000000            mov       ebx,3
  004e382f  bd10000000            mov       ebp,10h
  004e3834  0f845e050000          je        near 004E3D98h
  004e383a  56                    push      esi
  004e383b  e8607dffff            call      004DB5A0h
  004e3840  8b06                  mov       eax,[esi]
  004e3842  83c404                add       esp,4
  004e3845  a802                  test      al,2
  004e3847  0f844a030000          je        near 004E3B97h
  004e384d  f644242810            test      byte [esp+28h],10h
  004e3852  740d                  je        short 004E3861h
  004e3854  24fd                  and       al,0FDh
  004e3856  c6464300              mov       byte [esi+43h],0
  004e385a  8906                  mov       [esi],eax
  004e385c  e937050000            jmp       004E3D98h

LAB_004e3861:
  004e3861  33c9                  xor       ecx,ecx
  004e3863  8a4e42                mov       cl,[esi+42h]
  004e3866  83e900                sub       ecx,0
  004e3869  0f8458020000          je        near 004E3AC7h
  004e386f  49                    dec       ecx
  004e3870  0f8522050000          jne       near 004E3D98h
  004e3876  0fbf4610              movsx     eax,word [esi+10h]
  004e387a  83e800                sub       eax,0
  004e387d  0f84a6000000          je        near 004E3929h
  004e3883  48                    dec       eax
  004e3884  0f850e050000          jne       near 004E3D98h
  004e388a  66396e1a              cmp       [esi+1Ah],bp
  004e388e  c7465c00000000        mov       dword [esi+5Ch],0
  004e3895  7555                  jne       short 004E38ECh
  004e3897  8b542440              mov       edx,[esp+40h]
  004e389b  0fbf4632              movsx     eax,word [esi+32h]
  004e389f  81c200050000          add       edx,500h
  004e38a5  50                    push      eax
  004e38a6  c1e207                shl       edx,7
  004e38a9  89565c                mov       [esi+5Ch],edx
  004e38ac  e86b210600            call      00545A1Ch
  004e38b1  0faf465c              imul      eax,[esi+5Ch]
  004e38b5  8b4e50                mov       ecx,[esi+50h]
  004e38b8  c1f80c                sar       eax,0Ch
  004e38bb  03c8                  add       ecx,eax
  004e38bd  894e50                mov       [esi+50h],ecx
  004e38c0  0fbf4e32              movsx     ecx,word [esi+32h]
  004e38c4  51                    push      ecx
  004e38c5  e84c210600            call      00545A16h
  004e38ca  0faf465c              imul      eax,[esi+5Ch]
  004e38ce  8b5e58                mov       ebx,[esi+58h]
  004e38d1  c1f80c                sar       eax,0Ch
  004e38d4  03d8                  add       ebx,eax
  004e38d6  8b442440              mov       eax,[esp+40h]
  004e38da  895e58                mov       [esi+58h],ebx
  004e38dd  8d5060                lea       edx,[eax+60h]
  004e38e0  c1e207                shl       edx,7
  004e38e3  66ff4e10              dec       word [esi+10h]
  004e38e7  89565c                mov       [esi+5Ch],edx
  004e38ea  eb04                  jmp       short 004E38F0h

LAB_004e38ec:
  004e38ec  8b442440              mov       eax,[esp+40h]

LAB_004e38f0:
  004e38f0  8b4c2410              mov       ecx,[esp+10h]
  004e38f4  d1f8                  sar       eax,1
  004e38f6  83c040                add       eax,40h
  004e38f9  8d1408                lea       edx,[eax+ecx]
  004e38fc  8d3c00                lea       edi,[eax+eax]
  004e38ff  81e2ff0f0000          and       edx,0FFFh
  004e3905  3bd7                  cmp       edx,edi
  004e3907  7f09                  jg        short 004E3912h
  004e3909  66014e32              add       [esi+32h],cx
  004e390d  e986040000            jmp       004E3D98h

LAB_004e3912:
  004e3912  81e1ff0f0000          and       ecx,0FFFh
  004e3918  81f900080000          cmp       ecx,800h
  004e391e  0f8c70040000          jl        near 004E3D94h
  004e3924  e969040000            jmp       004E3D92h

LAB_004e3929:
  004e3929  8b4650                mov       eax,[esi+50h]
  004e392c  8b4e58                mov       ecx,[esi+58h]
  004e392f  c1f80c                sar       eax,0Ch
  004e3932  8bd0                  mov       edx,eax
  004e3934  c1f90c                sar       ecx,0Ch
  004e3937  0fafd0                imul      edx,eax
  004e393a  8bc1                  mov       eax,ecx
  004e393c  0fafc1                imul      eax,ecx
  004e393f  03d0                  add       edx,eax
  004e3941  52                    push      edx
  004e3942  e8c9200600            call      00545A10h
  004e3947  8b16                  mov       edx,[esi]
  004e3949  8b4c2440              mov       ecx,[esp+40h]
  004e394d  f7c200000002          test      edx,2000000h
  004e3953  740f                  je        short 004E3964h
  004e3955  8bf9                  mov       edi,ecx
  004e3957  c1ff03                sar       edi,3
  004e395a  47                    inc       edi
  004e395b  3bc7                  cmp       eax,edi
  004e395d  7f05                  jg        short 004E3964h
  004e395f  83e2dd                and       edx,0FFFFFFDDh
  004e3962  8916                  mov       [esi],edx

LAB_004e3964:
  004e3964  3bc3                  cmp       eax,ebx
  004e3966  0f8c2c040000          jl        near 004E3D98h
  004e396c  f60620                test      byte [esi],20h
  004e396f  0f8523040000          jne       near 004E3D98h
  004e3975  c1f903                sar       ecx,3
  004e3978  83c120                add       ecx,20h
  004e397b  8bc1                  mov       eax,ecx
  004e397d  8b4c2410              mov       ecx,[esp+10h]
  004e3981  8d1408                lea       edx,[eax+ecx]
  004e3984  8d3c00                lea       edi,[eax+eax]
  004e3987  81e2ff0f0000          and       edx,0FFFh
  004e398d  3bd7                  cmp       edx,edi
  004e398f  7f06                  jg        short 004E3997h
  004e3991  66014e32              add       [esi+32h],cx
  004e3995  eb14                  jmp       short 004E39ABh

LAB_004e3997:
  004e3997  81e1ff0f0000          and       ecx,0FFFh
  004e399d  81f900080000          cmp       ecx,800h
  004e39a3  7c02                  jl        short 004E39A7h
  004e39a5  f7d8                  neg       eax

LAB_004e39a7:
  004e39a7  66014632              add       [esi+32h],ax

LAB_004e39ab:
  004e39ab  8d6e1c                lea       ebp,[esi+1Ch]
  004e39ae  6a00                  push      0
  004e39b0  68e42d5500            push      552DE4h
  004e39b5  55                    push      ebp
  004e39b6  56                    push      esi
  004e39b7  e874620300            call      00519C30h
  004e39bc  8bf8                  mov       edi,eax
  004e39be  83c410                add       esp,10h
  004e39c1  85ff                  test      edi,edi
  004e39c3  7406                  je        short 004E39CBh
  004e39c5  8b06                  mov       eax,[esi]
  004e39c7  0c20                  or        al,20h
  004e39c9  8906                  mov       [esi],eax

LAB_004e39cb:
  004e39cb  a1b8e05d00            mov       eax,[5DE0B8h]
  004e39d0  85c0                  test      eax,eax
  004e39d2  0f84c0030000          je        near 004E3D98h
  004e39d8  8b1f                  mov       ebx,[edi]
  004e39da  66c787960000001000    mov       word [edi+96h],10h
  004e39e3  83cb40                or        ebx,40h
  004e39e6  57                    push      edi
  004e39e7  891f                  mov       [edi],ebx
  004e39e9  8b4c2424              mov       ecx,[esp+24h]
  004e39ed  56                    push      esi
  004e39ee  668b4130              mov       ax,[ecx+30h]
  004e39f2  0fbfd8                movsx     ebx,ax
  004e39f5  66d1f8                sar       ax,1
  004e39f8  66894130              mov       [ecx+30h],ax
  004e39fc  e87f610500            call      00539B80h
  004e3a01  8b442428              mov       eax,[esp+28h]
  004e3a05  83c408                add       esp,8
  004e3a08  66895830              mov       [eax+30h],bx
  004e3a0c  8d5f1c                lea       ebx,[edi+1Ch]
  004e3a0f  53                    push      ebx
  004e3a10  6800180000            push      1800h
  004e3a15  6800030000            push      300h
  004e3a1a  6a03                  push      3
  004e3a1c  e8ffa00500            call      0053DB20h
  004e3a21  8b07                  mov       eax,[edi]
  004e3a23  83c410                add       esp,10h
  004e3a26  a900000040            test      eax,40000000h
  004e3a2b  0f8567030000          jne       near 004E3D98h
  004e3a31  0fbf4e32              movsx     ecx,word [esi+32h]
  004e3a35  8b542410              mov       edx,[esp+10h]
  004e3a39  03d1                  add       edx,ecx
  004e3a3b  52                    push      edx
  004e3a3c  e8db1f0600            call      00545A1Ch
  004e3a41  8d0440                lea       eax,[eax+eax*2]
  004e3a44  8d0480                lea       eax,[eax+eax*4]
  004e3a47  d1e0                  shl       eax,1
  004e3a49  894750                mov       [edi+50h],eax
  004e3a4c  8b542410              mov       edx,[esp+10h]
  004e3a50  0fbf4e32              movsx     ecx,word [esi+32h]
  004e3a54  03d1                  add       edx,ecx
  004e3a56  52                    push      edx
  004e3a57  e8ba1f0600            call      00545A16h
  004e3a5c  8d0440                lea       eax,[eax+eax*2]
  004e3a5f  8d0480                lea       eax,[eax+eax*4]
  004e3a62  d1e0                  shl       eax,1
  004e3a64  894758                mov       [edi+58h],eax
  004e3a67  8b07                  mov       eax,[edi]
  004e3a69  a801                  test      al,1
  004e3a6b  750b                  jne       short 004E3A78h
  004e3a6d  0c01                  or        al,1
  004e3a6f  c7475400800100        mov       dword [edi+54h],18000h
  004e3a76  8907                  mov       [edi],eax

LAB_004e3a78:
  004e3a78  8b4d00                mov       ecx,[ebp]
  004e3a7b  8b5620                mov       edx,[esi+20h]
  004e3a7e  8b4624                mov       eax,[esi+24h]
  004e3a81  894c242c              mov       [esp+2Ch],ecx
  004e3a85  8b0b                  mov       ecx,[ebx]
  004e3a87  89542430              mov       [esp+30h],edx
  004e3a8b  894d00                mov       [ebp],ecx
  004e3a8e  8b5720                mov       edx,[edi+20h]
  004e3a91  6aff                  push      0FFFFFFFFh
  004e3a93  89442438              mov       [esp+38h],eax
  004e3a97  895620                mov       [esi+20h],edx
  004e3a9a  8b4724                mov       eax,[edi+24h]
  004e3a9d  6a00                  push      0
  004e3a9f  6a39                  push      39h
  004e3aa1  56                    push      esi
  004e3aa2  894624                mov       [esi+24h],eax
  004e3aa5  e8266ff9ff            call      0047A9D0h
  004e3aaa  8b4c243c              mov       ecx,[esp+3Ch]
  004e3aae  8b542440              mov       edx,[esp+40h]
  004e3ab2  8b442444              mov       eax,[esp+44h]
  004e3ab6  83c410                add       esp,10h
  004e3ab9  894d00                mov       [ebp],ecx
  004e3abc  895620                mov       [esi+20h],edx
  004e3abf  894624                mov       [esi+24h],eax
  004e3ac2  e9d1020000            jmp       004E3D98h

LAB_004e3ac7:
  004e3ac7  66395e18              cmp       [esi+18h],bx
  004e3acb  740a                  je        short 004E3AD7h
  004e3acd  25fffffffe            and       eax,0FEFFFFFFh
  004e3ad2  895e18                mov       [esi+18h],ebx
  004e3ad5  8906                  mov       [esi],eax

LAB_004e3ad7:
  004e3ad7  8b442440              mov       eax,[esp+40h]
  004e3adb  8b4c2410              mov       ecx,[esp+10h]
  004e3adf  d1f8                  sar       eax,1
  004e3ae1  83c040                add       eax,40h
  004e3ae4  8d1408                lea       edx,[eax+ecx]
  004e3ae7  8d3c00                lea       edi,[eax+eax]
  004e3aea  81e2ff0f0000          and       edx,0FFFh
  004e3af0  3bd7                  cmp       edx,edi
  004e3af2  7f06                  jg        short 004E3AFAh
  004e3af4  66014e32              add       [esi+32h],cx
  004e3af8  eb14                  jmp       short 004E3B0Eh

LAB_004e3afa:
  004e3afa  81e1ff0f0000          and       ecx,0FFFh
  004e3b00  81f900080000          cmp       ecx,800h
  004e3b06  7c02                  jl        short 004E3B0Ah
  004e3b08  f7d8                  neg       eax

LAB_004e3b0a:
  004e3b0a  66014632              add       [esi+32h],ax

LAB_004e3b0e:
  004e3b0e  6a00                  push      0
  004e3b10  8d461c                lea       eax,[esi+1Ch]
  004e3b13  68d82d5500            push      552DD8h
  004e3b18  50                    push      eax
  004e3b19  56                    push      esi
  004e3b1a  e811610300            call      00519C30h
  004e3b1f  8bf8                  mov       edi,eax
  004e3b21  a1b8e05d00            mov       eax,[5DE0B8h]
  004e3b26  83c410                add       esp,10h
  004e3b29  85c0                  test      eax,eax
  004e3b2b  742f                  je        short 004E3B5Ch
  004e3b2d  6689af96000000        mov       [edi+96h],bp
  004e3b34  8b2f                  mov       ebp,[edi]
  004e3b36  83cd40                or        ebp,40h
  004e3b39  57                    push      edi
  004e3b3a  56                    push      esi
  004e3b3b  892f                  mov       [edi],ebp
  004e3b3d  e83e600500            call      00539B80h
  004e3b42  83c408                add       esp,8
  004e3b45  83c71c                add       edi,1Ch
  004e3b48  57                    push      edi
  004e3b49  6800180000            push      1800h
  004e3b4e  6800030000            push      300h
  004e3b53  53                    push      ebx
  004e3b54  e8c79f0500            call      0053DB20h
  004e3b59  83c410                add       esp,10h

LAB_004e3b5c:
  004e3b5c  8b4c2440              mov       ecx,[esp+40h]
  004e3b60  8b465c                mov       eax,[esi+5Ch]
  004e3b63  81c180020000          add       ecx,280h
  004e3b69  c1e107                shl       ecx,7
  004e3b6c  2bc8                  sub       ecx,eax
  004e3b6e  c1f904                sar       ecx,4
  004e3b71  03c8                  add       ecx,eax
  004e3b73  8a4643                mov       al,[esi+43h]
  004e3b76  84c0                  test      al,al
  004e3b78  894e5c                mov       [esi+5Ch],ecx
  004e3b7b  0f8417020000          je        near 004E3D98h
  004e3b81  fec8                  dec       al
  004e3b83  884643                mov       [esi+43h],al
  004e3b86  0f850c020000          jne       near 004E3D98h
  004e3b8c  8b06                  mov       eax,[esi]
  004e3b8e  24fd                  and       al,0FDh
  004e3b90  8906                  mov       [esi],eax
  004e3b92  e901020000            jmp       004E3D98h

LAB_004e3b97:
  004e3b97  a810                  test      al,10h
  004e3b99  0f85f9010000          jne       near 004E3D98h
  004e3b9f  a904080000            test      eax,804h
  004e3ba4  0f85ee010000          jne       near 004E3D98h
  004e3baa  33c9                  xor       ecx,ecx
  004e3bac  8a4e42                mov       cl,[esi+42h]
  004e3baf  3bcb                  cmp       ecx,ebx
  004e3bb1  0f87e1010000          ja        near 004E3D98h
  004e3bb7  ff248dc4404e00        jmp       dword [ecx*4+4E40C4h]
  004e3bbe  66837e1800            cmp       word [esi+18h],0
  004e3bc3  740e                  je        short 004E3BD3h
  004e3bc5  0d00000001            or        eax,1000000h
  004e3bca  c7461800000000        mov       dword [esi+18h],0
  004e3bd1  8906                  mov       [esi],eax

LAB_004e3bd3:
  004e3bd3  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e3bdb  7d09                  jge       short 004E3BE6h
  004e3bdd  c6464300              mov       byte [esi+43h],0
  004e3be1  e9b2010000            jmp       004E3D98h

LAB_004e3be6:
  004e3be6  8a4643                mov       al,[esi+43h]
  004e3be9  84c0                  test      al,al
  004e3beb  7405                  je        short 004E3BF2h
  004e3bed  fec8                  dec       al
  004e3bef  884643                mov       [esi+43h],al

LAB_004e3bf2:
  004e3bf2  8b465c                mov       eax,[esi+5Ch]
  004e3bf5  8bd0                  mov       edx,eax
  004e3bf7  c1fa04                sar       edx,4
  004e3bfa  2bc2                  sub       eax,edx
  004e3bfc  89465c                mov       [esi+5Ch],eax
  004e3bff  e994010000            jmp       004E3D98h
  004e3c04  66837e1800            cmp       word [esi+18h],0
  004e3c09  740e                  je        short 004E3C19h
  004e3c0b  0d00000001            or        eax,1000000h
  004e3c10  c7461800000000        mov       dword [esi+18h],0
  004e3c17  8906                  mov       [esi],eax

LAB_004e3c19:
  004e3c19  8b442440              mov       eax,[esp+40h]
  004e3c1d  8b4c2410              mov       ecx,[esp+10h]
  004e3c21  c1f802                sar       eax,2
  004e3c24  83c038                add       eax,38h
  004e3c27  8d1408                lea       edx,[eax+ecx]
  004e3c2a  8d3c00                lea       edi,[eax+eax]
  004e3c2d  81e2ff0f0000          and       edx,0FFFh
  004e3c33  3bd7                  cmp       edx,edi
  004e3c35  7f1a                  jg        short 004E3C51h
  004e3c37  8b465c                mov       eax,[esi+5Ch]
  004e3c3a  66014e32              add       [esi+32h],cx
  004e3c3e  8bc8                  mov       ecx,eax
  004e3c40  c6464300              mov       byte [esi+43h],0
  004e3c44  c1f904                sar       ecx,4
  004e3c47  2bc1                  sub       eax,ecx
  004e3c49  89465c                mov       [esi+5Ch],eax
  004e3c4c  e947010000            jmp       004E3D98h

LAB_004e3c51:
  004e3c51  81e1ff0f0000          and       ecx,0FFFh
  004e3c57  81f900080000          cmp       ecx,800h
  004e3c5d  7c02                  jl        short 004E3C61h
  004e3c5f  f7d8                  neg       eax

LAB_004e3c61:
  004e3c61  66014632              add       [esi+32h],ax
  004e3c65  8b465c                mov       eax,[esi+5Ch]
  004e3c68  8bc8                  mov       ecx,eax
  004e3c6a  c1f904                sar       ecx,4
  004e3c6d  2bc1                  sub       eax,ecx
  004e3c6f  89465c                mov       [esi+5Ch],eax
  004e3c72  e921010000            jmp       004E3D98h
  004e3c77  b901000000            mov       ecx,1
  004e3c7c  66394e18              cmp       [esi+18h],cx
  004e3c80  740a                  je        short 004E3C8Ch
  004e3c82  0d00000001            or        eax,1000000h
  004e3c87  894e18                mov       [esi+18h],ecx
  004e3c8a  8906                  mov       [esi],eax

LAB_004e3c8c:
  004e3c8c  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e3c94  7d09                  jge       short 004E3C9Fh
  004e3c96  c6464300              mov       byte [esi+43h],0
  004e3c9a  e9f9000000            jmp       004E3D98h

LAB_004e3c9f:
  004e3c9f  8a4643                mov       al,[esi+43h]
  004e3ca2  84c0                  test      al,al
  004e3ca4  7405                  je        short 004E3CABh
  004e3ca6  fec8                  dec       al
  004e3ca8  884643                mov       [esi+43h],al

LAB_004e3cab:
  004e3cab  8b542440              mov       edx,[esp+40h]
  004e3caf  8b4c2410              mov       ecx,[esp+10h]
  004e3cb3  8bc2                  mov       eax,edx
  004e3cb5  c1f802                sar       eax,2
  004e3cb8  83c038                add       eax,38h
  004e3cbb  8d3c08                lea       edi,[eax+ecx]
  004e3cbe  8d1c00                lea       ebx,[eax+eax]
  004e3cc1  81e7ff0f0000          and       edi,0FFFh
  004e3cc7  3bfb                  cmp       edi,ebx
  004e3cc9  7f06                  jg        short 004E3CD1h
  004e3ccb  66014e32              add       [esi+32h],cx
  004e3ccf  eb14                  jmp       short 004E3CE5h

LAB_004e3cd1:
  004e3cd1  81e1ff0f0000          and       ecx,0FFFh
  004e3cd7  81f900080000          cmp       ecx,800h
  004e3cdd  7c02                  jl        short 004E3CE1h
  004e3cdf  f7d8                  neg       eax

LAB_004e3ce1:
  004e3ce1  66014632              add       [esi+32h],ax

LAB_004e3ce5:
  004e3ce5  8b465c                mov       eax,[esi+5Ch]
  004e3ce8  81c2a0010000          add       edx,1A0h
  004e3cee  c1e207                shl       edx,7
  004e3cf1  2bd0                  sub       edx,eax
  004e3cf3  c1fa04                sar       edx,4
  004e3cf6  03d0                  add       edx,eax
  004e3cf8  89565c                mov       [esi+5Ch],edx
  004e3cfb  e998000000            jmp       004E3D98h
  004e3d00  33ff                  xor       edi,edi
  004e3d02  66397e18              cmp       [esi+18h],di
  004e3d06  740a                  je        short 004E3D12h
  004e3d08  0d00000001            or        eax,1000000h
  004e3d0d  897e18                mov       [esi+18h],edi
  004e3d10  8906                  mov       [esi],eax

LAB_004e3d12:
  004e3d12  8a4643                mov       al,[esi+43h]
  004e3d15  84c0                  test      al,al
  004e3d17  7405                  je        short 004E3D1Eh
  004e3d19  fec8                  dec       al
  004e3d1b  884643                mov       [esi+43h],al

LAB_004e3d1e:
  004e3d1e  8b542440              mov       edx,[esp+40h]
  004e3d22  0fbf4610              movsx     eax,word [esi+10h]
  004e3d26  83c240                add       edx,40h
  004e3d29  50                    push      eax
  004e3d2a  c1e207                shl       edx,7
  004e3d2d  89565c                mov       [esi+5Ch],edx
  004e3d30  e8e71c0600            call      00545A1Ch
  004e3d35  0faf465c              imul      eax,[esi+5Ch]
  004e3d39  8b4e50                mov       ecx,[esi+50h]
  004e3d3c  c1f80c                sar       eax,0Ch
  004e3d3f  03c8                  add       ecx,eax
  004e3d41  894e50                mov       [esi+50h],ecx
  004e3d44  0fbf4e10              movsx     ecx,word [esi+10h]
  004e3d48  51                    push      ecx
  004e3d49  e8c81c0600            call      00545A16h
  004e3d4e  0faf465c              imul      eax,[esi+5Ch]
  004e3d52  8b5e58                mov       ebx,[esi+58h]
  004e3d55  8b4c2410              mov       ecx,[esp+10h]
  004e3d59  c1f80c                sar       eax,0Ch
  004e3d5c  03d8                  add       ebx,eax
  004e3d5e  8b442440              mov       eax,[esp+40h]
  004e3d62  c1f802                sar       eax,2
  004e3d65  83c040                add       eax,40h
  004e3d68  897e5c                mov       [esi+5Ch],edi
  004e3d6b  895e58                mov       [esi+58h],ebx
  004e3d6e  8d1408                lea       edx,[eax+ecx]
  004e3d71  8d3c00                lea       edi,[eax+eax]
  004e3d74  81e2ff0f0000          and       edx,0FFFh
  004e3d7a  3bd7                  cmp       edx,edi
  004e3d7c  7f06                  jg        short 004E3D84h
  004e3d7e  66014e32              add       [esi+32h],cx
  004e3d82  eb14                  jmp       short 004E3D98h

LAB_004e3d84:
  004e3d84  81e1ff0f0000          and       ecx,0FFFh
  004e3d8a  81f900080000          cmp       ecx,800h
  004e3d90  7c02                  jl        short 004E3D94h

LAB_004e3d92:
  004e3d92  f7d8                  neg       eax

LAB_004e3d94:
  004e3d94  66014632              add       [esi+32h],ax

LAB_004e3d98:
  004e3d98  8b44241c              mov       eax,[esp+1Ch]
  004e3d9c  50                    push      eax
  004e3d9d  56                    push      esi
  004e3d9e  e83d6bfbff            call      0049A8E0h
  004e3da3  83c408                add       esp,8
  004e3da6  56                    push      esi
  004e3da7  e894fb0400            call      00533940h
  004e3dac  8b466c                mov       eax,[esi+6Ch]
  004e3daf  83c404                add       esp,4
  004e3db2  f60001                test      byte [eax],1
  004e3db5  7506                  jne       short 004E3DBDh
  004e3db7  8b4860                mov       ecx,[eax+60h]
  004e3dba  894e48                mov       [esi+48h],ecx

LAB_004e3dbd:
  004e3dbd  e87e330600            call      00547140h
  004e3dc2  8b7e60                mov       edi,[esi+60h]
  004e3dc5  25ff000000            and       eax,0FFh
  004e3dca  0500010000            add       eax,100h
  004e3dcf  66014614              add       [esi+14h],ax
  004e3dd3  8b4648                mov       eax,[esi+48h]
  004e3dd6  668b4e14              mov       cx,[esi+14h]
  004e3dda  3bc7                  cmp       eax,edi
  004e3ddc  7c02                  jl        short 004E3DE0h
  004e3dde  8bf8                  mov       edi,eax

LAB_004e3de0:
  004e3de0  f60602                test      byte [esi],2
  004e3de3  7415                  je        short 004E3DFAh
  004e3de5  8b4620                mov       eax,[esi+20h]
  004e3de8  2bf8                  sub       edi,eax
  004e3dea  81c700800200          add       edi,28000h
  004e3df0  c1ff03                sar       edi,3
  004e3df3  03f8                  add       edi,eax
  004e3df5  897e20                mov       [esi+20h],edi
  004e3df8  eb20                  jmp       short 004E3E1Ah

LAB_004e3dfa:
  004e3dfa  8b6e20                mov       ebp,[esi+20h]
  004e3dfd  0fbfd1                movsx     edx,cx
  004e3e00  52                    push      edx
  004e3e01  e8161c0600            call      00545A1Ch
  004e3e06  0500320000            add       eax,3200h
  004e3e0b  c1e006                shl       eax,6
  004e3e0e  03c7                  add       eax,edi
  004e3e10  2bc5                  sub       eax,ebp
  004e3e12  c1f804                sar       eax,4
  004e3e15  03c5                  add       eax,ebp
  004e3e17  894620                mov       [esi+20h],eax

LAB_004e3e1a:
  004e3e1a  66837e160a            cmp       word [esi+16h],0Ah
  004e3e1f  7c0d                  jl        short 004E3E2Eh
  004e3e21  8b442424              mov       eax,[esp+24h]
  004e3e25  50                    push      eax
  004e3e26  56                    push      esi
  004e3e27  e844f9ffff            call      004E3770h
  004e3e2c  eb1d                  jmp       short 004E3E4Bh

LAB_004e3e2e:
  004e3e2e  8bbe90000000          mov       edi,[esi+90h]
  004e3e34  8b4c2424              mov       ecx,[esp+24h]
  004e3e38  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004e3e3e  51                    push      ecx
  004e3e3f  56                    push      esi
  004e3e40  89be90000000          mov       [esi+90h],edi
  004e3e46  e8a5defbff            call      004A1CF0h

LAB_004e3e4b:
  004e3e4b  8b442424              mov       eax,[esp+24h]
  004e3e4f  83c408                add       esp,8
  004e3e52  85c0                  test      eax,eax
  004e3e54  0f8461020000          je        near 004E40BBh
  004e3e5a  f70616080000          test      dword [esi],816h
  004e3e60  0f8555020000          jne       near 004E40BBh
  004e3e66  8a4643                mov       al,[esi+43h]
  004e3e69  84c0                  test      al,al
  004e3e6b  0f854a020000          jne       near 004E40BBh
  004e3e71  8b466c                mov       eax,[esi+6Ch]
  004e3e74  8b5e1c                mov       ebx,[esi+1Ch]
  004e3e77  8b4e24                mov       ecx,[esi+24h]
  004e3e7a  8b781c                mov       edi,[eax+1Ch]
  004e3e7d  8b6824                mov       ebp,[eax+24h]
  004e3e80  2bfb                  sub       edi,ebx
  004e3e82  2be9                  sub       ebp,ecx
  004e3e84  c1ff0c                sar       edi,0Ch
  004e3e87  c1fd0c                sar       ebp,0Ch
  004e3e8a  8bd7                  mov       edx,edi
  004e3e8c  8bc5                  mov       eax,ebp
  004e3e8e  0fafd7                imul      edx,edi
  004e3e91  0fafc5                imul      eax,ebp
  004e3e94  03d0                  add       edx,eax
  004e3e96  52                    push      edx
  004e3e97  e8741b0600            call      00545A10h
  004e3e9c  55                    push      ebp
  004e3e9d  57                    push      edi
  004e3e9e  89442420              mov       [esp+20h],eax
  004e3ea2  e87b1b0600            call      00545A22h
  004e3ea7  668b4e32              mov       cx,[esi+32h]
  004e3eab  2bc1                  sub       eax,ecx
  004e3ead  25ff0f0000            and       eax,0FFFh
  004e3eb2  89442410              mov       [esp+10h],eax
  004e3eb6  8b442418              mov       eax,[esp+18h]
  004e3eba  3dfa000000            cmp       eax,0FAh
  004e3ebf  0f8da3000000          jge       near 004E3F68h
  004e3ec5  e876320600            call      00547140h
  004e3eca  99                    cdq
  004e3ecb  b964000000            mov       ecx,64h
  004e3ed0  f7f9                  idiv      ecx
  004e3ed2  83fa3c                cmp       edx,3Ch
  004e3ed5  7d60                  jge       short 004E3F37h
  004e3ed7  8b542410              mov       edx,[esp+10h]
  004e3edb  8b7c2414              mov       edi,[esp+14h]
  004e3edf  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e3ee6  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e3eed  25ff0f0000            and       eax,0FFFh
  004e3ef2  3bc1                  cmp       eax,ecx
  004e3ef4  0f8f89010000          jg        near 004E4083h
  004e3efa  e841320600            call      00547140h
  004e3eff  99                    cdq
  004e3f00  8d4c3f01              lea       ecx,[edi+edi+1]
  004e3f04  f7f9                  idiv      ecx
  004e3f06  8b4c2418              mov       ecx,[esp+18h]
  004e3f0a  b82c010000            mov       eax,12Ch
  004e3f0f  2bc2                  sub       eax,edx
  004e3f11  3bc8                  cmp       ecx,eax
  004e3f13  0f8d6a010000          jge       near 004E4083h
  004e3f19  8b1e                  mov       ebx,[esi]
  004e3f1b  8bcf                  mov       ecx,edi
  004e3f1d  c1f903                sar       ecx,3
  004e3f20  83cb02                or        ebx,2
  004e3f23  80c110                add       cl,10h
  004e3f26  891e                  mov       [esi],ebx
  004e3f28  c6464200              mov       byte [esi+42h],0
  004e3f2c  884e43                mov       [esi+43h],cl
  004e3f2f  5f                    pop       edi
  004e3f30  5e                    pop       esi
  004e3f31  5d                    pop       ebp
  004e3f32  5b                    pop       ebx
  004e3f33  83c42c                add       esp,2Ch
  004e3f36  c3                    ret

LAB_004e3f37:
  004e3f37  c6464203              mov       byte [esi+42h],3
  004e3f3b  e800320600            call      00547140h
  004e3f40  99                    cdq
  004e3f41  b90a000000            mov       ecx,0Ah
  004e3f46  f7f9                  idiv      ecx
  004e3f48  8b442414              mov       eax,[esp+14h]
  004e3f4c  c1f802                sar       eax,2
  004e3f4f  02d0                  add       dl,al
  004e3f51  02d1                  add       dl,cl
  004e3f53  8b4c2440              mov       ecx,[esp+40h]
  004e3f57  885643                mov       [esi+43h],dl
  004e3f5a  8db980000000          lea       edi,[ecx+80h]
  004e3f60  c1e704                shl       edi,4
  004e3f63  e98b000000            jmp       004E3FF3h

LAB_004e3f68:
  004e3f68  3df4010000            cmp       eax,1F4h
  004e3f6d  0f8dd2000000          jge       near 004E4045h
  004e3f73  e8c8310600            call      00547140h
  004e3f78  99                    cdq
  004e3f79  b964000000            mov       ecx,64h
  004e3f7e  f7f9                  idiv      ecx
  004e3f80  83fa3c                cmp       edx,3Ch
  004e3f83  0f8d8c000000          jge       near 004E4015h
  004e3f89  8b542410              mov       edx,[esp+10h]
  004e3f8d  8b7c2414              mov       edi,[esp+14h]
  004e3f91  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e3f98  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e3f9f  25ff0f0000            and       eax,0FFFh
  004e3fa4  3bc1                  cmp       eax,ecx
  004e3fa6  7f26                  jg        short 004E3FCEh
  004e3fa8  8b16                  mov       edx,[esi]
  004e3faa  c7461804000000        mov       dword [esi+18h],4
  004e3fb1  81e2fffffffe          and       edx,0FEFFFFFFh
  004e3fb7  c6464201              mov       byte [esi+42h],1
  004e3fbb  83ca02                or        edx,2
  004e3fbe  66c746100100          mov       word [esi+10h],1
  004e3fc4  8916                  mov       [esi],edx
  004e3fc6  5f                    pop       edi
  004e3fc7  5e                    pop       esi
  004e3fc8  5d                    pop       ebp
  004e3fc9  5b                    pop       ebx
  004e3fca  83c42c                add       esp,2Ch
  004e3fcd  c3                    ret

LAB_004e3fce:
  004e3fce  c6464203              mov       byte [esi+42h],3
  004e3fd2  e869310600            call      00547140h
  004e3fd7  99                    cdq
  004e3fd8  b90a000000            mov       ecx,0Ah
  004e3fdd  f7f9                  idiv      ecx
  004e3fdf  8bc7                  mov       eax,edi
  004e3fe1  8b7c2440              mov       edi,[esp+40h]
  004e3fe5  c1f802                sar       eax,2
  004e3fe8  c1e704                shl       edi,4
  004e3feb  02d0                  add       dl,al
  004e3fed  02d1                  add       dl,cl
  004e3fef  47                    inc       edi
  004e3ff0  885643                mov       [esi+43h],dl

LAB_004e3ff3:
  004e3ff3  e848310600            call      00547140h
  004e3ff8  99                    cdq
  004e3ff9  f7ff                  idiv      edi
  004e3ffb  8b4c2410              mov       ecx,[esp+10h]
  004e3fff  d1ff                  sar       edi,1
  004e4001  2bd7                  sub       edx,edi
  004e4003  03d1                  add       edx,ecx
  004e4005  66035632              add       dx,[esi+32h]
  004e4009  66895610              mov       [esi+10h],dx
  004e400d  5f                    pop       edi
  004e400e  5e                    pop       esi
  004e400f  5d                    pop       ebp
  004e4010  5b                    pop       ebx
  004e4011  83c42c                add       esp,2Ch
  004e4014  c3                    ret

LAB_004e4015:
  004e4015  83fa50                cmp       edx,50h
  004e4018  0f8d9d000000          jge       near 004E40BBh
  004e401e  c6464202              mov       byte [esi+42h],2
  004e4022  e819310600            call      00547140h
  004e4027  99                    cdq
  004e4028  b90a000000            mov       ecx,0Ah
  004e402d  f7f9                  idiv      ecx
  004e402f  8b442414              mov       eax,[esp+14h]
  004e4033  c1f802                sar       eax,2
  004e4036  02d0                  add       dl,al
  004e4038  02d1                  add       dl,cl
  004e403a  885643                mov       [esi+43h],dl
  004e403d  5f                    pop       edi
  004e403e  5e                    pop       esi
  004e403f  5d                    pop       ebp
  004e4040  5b                    pop       ebx
  004e4041  83c42c                add       esp,2Ch
  004e4044  c3                    ret

LAB_004e4045:
  004e4045  e8f6300600            call      00547140h
  004e404a  99                    cdq
  004e404b  b964000000            mov       ecx,64h
  004e4050  f7f9                  idiv      ecx
  004e4052  83fa1e                cmp       edx,1Eh
  004e4055  7d27                  jge       short 004E407Eh
  004e4057  c6464200              mov       byte [esi+42h],0
  004e405b  e8e0300600            call      00547140h
  004e4060  99                    cdq
  004e4061  b90a000000            mov       ecx,0Ah
  004e4066  f7f9                  idiv      ecx
  004e4068  8b442414              mov       eax,[esp+14h]
  004e406c  c1f802                sar       eax,2
  004e406f  02d0                  add       dl,al
  004e4071  02d1                  add       dl,cl
  004e4073  885643                mov       [esi+43h],dl
  004e4076  5f                    pop       edi
  004e4077  5e                    pop       esi
  004e4078  5d                    pop       ebp
  004e4079  5b                    pop       ebx
  004e407a  83c42c                add       esp,2Ch
  004e407d  c3                    ret

LAB_004e407e:
  004e407e  83fa3c                cmp       edx,3Ch
  004e4081  7d10                  jge       short 004E4093h

LAB_004e4083:
  004e4083  c6464201              mov       byte [esi+42h],1
  004e4087  c6464301              mov       byte [esi+43h],1
  004e408b  5f                    pop       edi
  004e408c  5e                    pop       esi
  004e408d  5d                    pop       ebp
  004e408e  5b                    pop       ebx
  004e408f  83c42c                add       esp,2Ch
  004e4092  c3                    ret

LAB_004e4093:
  004e4093  c6464203              mov       byte [esi+42h],3
  004e4097  e8a4300600            call      00547140h
  004e409c  99                    cdq
  004e409d  b90a000000            mov       ecx,0Ah
  004e40a2  f7f9                  idiv      ecx
  004e40a4  8b442414              mov       eax,[esp+14h]
  004e40a8  c1f802                sar       eax,2
  004e40ab  02d0                  add       dl,al
  004e40ad  02d1                  add       dl,cl
  004e40af  885643                mov       [esi+43h],dl
  004e40b2  e889300600            call      00547140h
  004e40b7  66894610              mov       [esi+10h],ax

LAB_004e40bb:
  004e40bb  5f                    pop       edi
  004e40bc  5e                    pop       esi
  004e40bd  5d                    pop       ebp
  004e40be  5b                    pop       ebx
  004e40bf  83c42c                add       esp,2Ch
  004e40c2  c3                    ret
  004e40c3  90                    nop
  004e40c4  be3b4e0004            mov       esi,4004E3Bh
  004e40c9  3c4e                  cmp       al,4Eh
  004e40cb  00773c                add       [edi+3Ch],dh
  004e40ce  4e                    dec       esi
  004e40cf  0000                  add       [eax],al
  004e40d1  3d4e009090            cmp       eax,9090004Eh
  004e40d6  90                    nop
  004e40d7  90                    nop
  004e40d8  90                    nop
  004e40d9  90                    nop
  004e40da  90                    nop
  004e40db  90                    nop
  004e40dc  90                    nop
  004e40dd  90                    nop
  004e40de  90                    nop
  004e40df  90                    nop

52 cible(s) de saut interne : 0x004e3861, 0x004e38ec, 0x004e38f0, 0x004e3912, 0x004e3929, 0x004e3964, 0x004e3997, 0x004e39a7, 0x004e39ab, 0x004e39cb, 0x004e3a78, 0x004e3ac7, 0x004e3ad7, 0x004e3afa, 0x004e3b0a, 0x004e3b0e, 0x004e3b5c, 0x004e3b97, 0x004e3bd3, 0x004e3be6, 0x004e3bf2, 0x004e3c19, 0x004e3c51, 0x004e3c61, 0x004e3c8c, 0x004e3c9f, 0x004e3cab, 0x004e3cd1, 0x004e3ce1, 0x004e3ce5, 0x004e3d12, 0x004e3d1e, 0x004e3d84, 0x004e3d92, 0x004e3d94, 0x004e3d98, 0x004e3dbd, 0x004e3de0, 0x004e3dfa, 0x004e3e1a, 0x004e3e2e, 0x004e3e4b, 0x004e3f37, 0x004e3f68, 0x004e3fce, 0x004e3ff3, 0x004e4015, 0x004e4045, 0x004e407e, 0x004e4083, 0x004e4093, 0x004e40bb
