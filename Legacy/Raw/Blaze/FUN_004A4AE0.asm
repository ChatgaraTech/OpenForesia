C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a4ae0  section .text  offset fichier 0xa3ee0  2542 octets

  004a4ae0  83ec28                sub       esp,28h
  004a4ae3  53                    push      ebx
  004a4ae4  55                    push      ebp
  004a4ae5  56                    push      esi
  004a4ae6  8b742438              mov       esi,[esp+38h]
  004a4aea  8d44241c              lea       eax,[esp+1Ch]
  004a4aee  57                    push      edi
  004a4aef  8d4c2434              lea       ecx,[esp+34h]
  004a4af3  50                    push      eax
  004a4af4  8d542434              lea       edx,[esp+34h]
  004a4af8  51                    push      ecx
  004a4af9  52                    push      edx
  004a4afa  56                    push      esi
  004a4afb  e8803e0900            call      00538980h
  004a4b00  83c410                add       esp,10h
  004a4b03  8d442424              lea       eax,[esp+24h]
  004a4b07  8d4c241c              lea       ecx,[esp+1Ch]
  004a4b0b  8d542410              lea       edx,[esp+10h]
  004a4b0f  50                    push      eax
  004a4b10  51                    push      ecx
  004a4b11  8d442420              lea       eax,[esp+20h]
  004a4b15  52                    push      edx
  004a4b16  8d4c2448              lea       ecx,[esp+48h]
  004a4b1a  50                    push      eax
  004a4b1b  51                    push      ecx
  004a4b1c  56                    push      esi
  004a4b1d  e8fe130900            call      00535F20h
  004a4b22  8b4c2454              mov       ecx,[esp+54h]
  004a4b26  b840000000            mov       eax,40h
  004a4b2b  2bc1                  sub       eax,ecx
  004a4b2d  83c418                add       esp,18h
  004a4b30  89442414              mov       [esp+14h],eax
  004a4b34  8b44241c              mov       eax,[esp+1Ch]
  004a4b38  85c0                  test      eax,eax
  004a4b3a  0f849f050000          je        near 004A50DFh
  004a4b40  56                    push      esi
  004a4b41  e85a6a0300            call      004DB5A0h
  004a4b46  8b06                  mov       eax,[esi]
  004a4b48  83c404                add       esp,4
  004a4b4b  a802                  test      al,2
  004a4b4d  0f8499030000          je        near 004A4EECh
  004a4b53  8a4c2424              mov       cl,[esp+24h]
  004a4b57  bb10000000            mov       ebx,10h
  004a4b5c  84cb                  test      bl,cl
  004a4b5e  740d                  je        short 004A4B6Dh
  004a4b60  24fd                  and       al,0FDh
  004a4b62  c6464300              mov       byte [esi+43h],0
  004a4b66  8906                  mov       [esi],eax
  004a4b68  e972050000            jmp       004A50DFh

LAB_004a4b6d:
  004a4b6d  33c9                  xor       ecx,ecx
  004a4b6f  8a4e42                mov       cl,[esi+42h]
  004a4b72  83e900                sub       ecx,0
  004a4b75  0f8456020000          je        near 004A4DD1h
  004a4b7b  49                    dec       ecx
  004a4b7c  0f84c4000000          je        near 004A4C46h
  004a4b82  49                    dec       ecx
  004a4b83  0f8556050000          jne       near 004A50DFh
  004a4b89  0fbf4e10              movsx     ecx,word [esi+10h]
  004a4b8d  33db                  xor       ebx,ebx
  004a4b8f  2bcb                  sub       ecx,ebx
  004a4b91  0f8499000000          je        near 004A4C30h
  004a4b97  49                    dec       ecx
  004a4b98  0f85a0000000          jne       near 004A4C3Eh
  004a4b9e  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004a4ba3  7518                  jne       short 004A4BBDh
  004a4ba5  8d561c                lea       edx,[esi+1Ch]
  004a4ba8  52                    push      edx
  004a4ba9  6800180000            push      1800h
  004a4bae  682a030000            push      32Ah
  004a4bb3  6a3f                  push      3Fh
  004a4bb5  e8668f0900            call      0053DB20h
  004a4bba  83c410                add       esp,10h

LAB_004a4bbd:
  004a4bbd  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004a4bc2  7518                  jne       short 004A4BDCh
  004a4bc4  8d461c                lea       eax,[esi+1Ch]
  004a4bc7  50                    push      eax
  004a4bc8  6800180000            push      1800h
  004a4bcd  6884030000            push      384h
  004a4bd2  6a3f                  push      3Fh
  004a4bd4  e8478f0900            call      0053DB20h
  004a4bd9  83c410                add       esp,10h

LAB_004a4bdc:
  004a4bdc  f70600000002          test      dword [esi],2000000h
  004a4be2  745a                  je        short 004A4C3Eh
  004a4be4  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004a4beb  66899e9a000000        mov       [esi+9Ah],bx
  004a4bf2  8b0dcc975e00          mov       ecx,[5E97CCh]
  004a4bf8  6aff                  push      0FFFFFFFFh
  004a4bfa  6aff                  push      0FFFFFFFFh
  004a4bfc  8b91cc000000          mov       edx,[ecx+0CCh]
  004a4c02  33c9                  xor       ecx,ecx
  004a4c04  8b02                  mov       eax,[edx]
  004a4c06  8a4809                mov       cl,[eax+9]
  004a4c09  81c1c0000000          add       ecx,0C0h
  004a4c0f  51                    push      ecx
  004a4c10  56                    push      esi
  004a4c11  e8ba5dfdff            call      0047A9D0h
  004a4c16  83c410                add       esp,10h
  004a4c19  66ff4e10              dec       word [esi+10h]
  004a4c1d  6689be9a000000        mov       [esi+9Ah],di
  004a4c24  c646430a              mov       byte [esi+43h],0Ah
  004a4c28  895e5c                mov       [esi+5Ch],ebx
  004a4c2b  e9af040000            jmp       004A50DFh

LAB_004a4c30:
  004a4c30  8a4e43                mov       cl,[esi+43h]
  004a4c33  fec9                  dec       cl
  004a4c35  884e43                mov       [esi+43h],cl
  004a4c38  7504                  jne       short 004A4C3Eh
  004a4c3a  24fd                  and       al,0FDh
  004a4c3c  8906                  mov       [esi],eax

LAB_004a4c3e:
  004a4c3e  895e5c                mov       [esi+5Ch],ebx
  004a4c41  e999040000            jmp       004A50DFh

LAB_004a4c46:
  004a4c46  8a4e43                mov       cl,[esi+43h]
  004a4c49  84c9                  test      cl,cl
  004a4c4b  740b                  je        short 004A4C58h
  004a4c4d  fec9                  dec       cl
  004a4c4f  884e43                mov       [esi+43h],cl
  004a4c52  7504                  jne       short 004A4C58h
  004a4c54  24dd                  and       al,0DDh
  004a4c56  8906                  mov       [esi],eax

LAB_004a4c58:
  004a4c58  8b44243c              mov       eax,[esp+3Ch]
  004a4c5c  8b4e5c                mov       ecx,[esi+5Ch]
  004a4c5f  8d9000040000          lea       edx,[eax+400h]
  004a4c65  c1e207                shl       edx,7
  004a4c68  2bd1                  sub       edx,ecx
  004a4c6a  c1fa02                sar       edx,2
  004a4c6d  c1f802                sar       eax,2
  004a4c70  03d1                  add       edx,ecx
  004a4c72  8b4c2410              mov       ecx,[esp+10h]
  004a4c76  83c020                add       eax,20h
  004a4c79  89565c                mov       [esi+5Ch],edx
  004a4c7c  8d1408                lea       edx,[eax+ecx]
  004a4c7f  8d3c00                lea       edi,[eax+eax]
  004a4c82  81e2ff0f0000          and       edx,0FFFh
  004a4c88  3bd7                  cmp       edx,edi
  004a4c8a  7f06                  jg        short 004A4C92h
  004a4c8c  66014e32              add       [esi+32h],cx
  004a4c90  eb14                  jmp       short 004A4CA6h

LAB_004a4c92:
  004a4c92  81e1ff0f0000          and       ecx,0FFFh
  004a4c98  81f900080000          cmp       ecx,800h
  004a4c9e  7c02                  jl        short 004A4CA2h
  004a4ca0  f7d8                  neg       eax

LAB_004a4ca2:
  004a4ca2  66014632              add       [esi+32h],ax

LAB_004a4ca6:
  004a4ca6  f60620                test      byte [esi],20h
  004a4ca9  0f8530040000          jne       near 004A50DFh
  004a4caf  8d461c                lea       eax,[esi+1Ch]
  004a4cb2  6a00                  push      0
  004a4cb4  680c2a5500            push      552A0Ch
  004a4cb9  50                    push      eax
  004a4cba  56                    push      esi
  004a4cbb  e8704f0700            call      00519C30h
  004a4cc0  8bf8                  mov       edi,eax
  004a4cc2  83c410                add       esp,10h
  004a4cc5  85ff                  test      edi,edi
  004a4cc7  7406                  je        short 004A4CCFh
  004a4cc9  8b06                  mov       eax,[esi]
  004a4ccb  0c20                  or        al,20h
  004a4ccd  8906                  mov       [esi],eax

LAB_004a4ccf:
  004a4ccf  a1b8e05d00            mov       eax,[5DE0B8h]
  004a4cd4  85c0                  test      eax,eax
  004a4cd6  0f8403040000          je        near 004A50DFh
  004a4cdc  66899f96000000        mov       [edi+96h],bx
  004a4ce3  8b1f                  mov       ebx,[edi]
  004a4ce5  83cb40                or        ebx,40h
  004a4ce8  57                    push      edi
  004a4ce9  891f                  mov       [edi],ebx
  004a4ceb  8b4c2424              mov       ecx,[esp+24h]
  004a4cef  56                    push      esi
  004a4cf0  668b4130              mov       ax,[ecx+30h]
  004a4cf4  0fbfe8                movsx     ebp,ax
  004a4cf7  66d1f8                sar       ax,1
  004a4cfa  66894130              mov       [ecx+30h],ax
  004a4cfe  e87d4e0900            call      00539B80h
  004a4d03  8b442428              mov       eax,[esp+28h]
  004a4d07  83c408                add       esp,8
  004a4d0a  8d5f1c                lea       ebx,[edi+1Ch]
  004a4d0d  53                    push      ebx
  004a4d0e  6800180000            push      1800h
  004a4d13  6800030000            push      300h
  004a4d18  6a03                  push      3
  004a4d1a  66896830              mov       [eax+30h],bp
  004a4d1e  e8fd8d0900            call      0053DB20h
  004a4d23  8b07                  mov       eax,[edi]
  004a4d25  83c410                add       esp,10h
  004a4d28  a900000040            test      eax,40000000h
  004a4d2d  0f85ac030000          jne       near 004A50DFh
  004a4d33  33c9                  xor       ecx,ecx
  004a4d35  81fd00080000          cmp       ebp,800h
  004a4d3b  0f9dc1                setge     cl
  004a4d3e  0fbf5632              movsx     edx,word [esi+32h]
  004a4d42  49                    dec       ecx
  004a4d43  81e100080000          and       ecx,800h
  004a4d49  81c100fcffff          add       ecx,0FFFFFC00h
  004a4d4f  8be9                  mov       ebp,ecx
  004a4d51  8bc5                  mov       eax,ebp
  004a4d53  03c2                  add       eax,edx
  004a4d55  50                    push      eax
  004a4d56  e8c10c0a00            call      00545A1Ch
  004a4d5b  8d0c80                lea       ecx,[eax+eax*4]
  004a4d5e  c1e103                shl       ecx,3
  004a4d61  894f50                mov       [edi+50h],ecx
  004a4d64  0fbf5632              movsx     edx,word [esi+32h]
  004a4d68  03ea                  add       ebp,edx
  004a4d6a  55                    push      ebp
  004a4d6b  e8a60c0a00            call      00545A16h
  004a4d70  8d0480                lea       eax,[eax+eax*4]
  004a4d73  c1e003                shl       eax,3
  004a4d76  894758                mov       [edi+58h],eax
  004a4d79  8b07                  mov       eax,[edi]
  004a4d7b  a801                  test      al,1
  004a4d7d  750b                  jne       short 004A4D8Ah
  004a4d7f  0c01                  or        al,1
  004a4d81  c7475400800100        mov       dword [edi+54h],18000h
  004a4d88  8907                  mov       [edi],eax

LAB_004a4d8a:
  004a4d8a  8b4e20                mov       ecx,[esi+20h]
  004a4d8d  8b03                  mov       eax,[ebx]
  004a4d8f  8b6e1c                mov       ebp,[esi+1Ch]
  004a4d92  8b5624                mov       edx,[esi+24h]
  004a4d95  894c2428              mov       [esp+28h],ecx
  004a4d99  89461c                mov       [esi+1Ch],eax
  004a4d9c  8b4f20                mov       ecx,[edi+20h]
  004a4d9f  6aff                  push      0FFFFFFFFh
  004a4da1  89542430              mov       [esp+30h],edx
  004a4da5  894e20                mov       [esi+20h],ecx
  004a4da8  8b5724                mov       edx,[edi+24h]
  004a4dab  6a00                  push      0
  004a4dad  6a39                  push      39h
  004a4daf  56                    push      esi
  004a4db0  895624                mov       [esi+24h],edx
  004a4db3  e8185cfdff            call      0047A9D0h
  004a4db8  8b442438              mov       eax,[esp+38h]
  004a4dbc  8b4c243c              mov       ecx,[esp+3Ch]
  004a4dc0  83c410                add       esp,10h
  004a4dc3  896e1c                mov       [esi+1Ch],ebp
  004a4dc6  894620                mov       [esi+20h],eax
  004a4dc9  894e24                mov       [esi+24h],ecx
  004a4dcc  e90e030000            jmp       004A50DFh

LAB_004a4dd1:
  004a4dd1  668b561a              mov       dx,[esi+1Ah]
  004a4dd5  8b7c243c              mov       edi,[esp+3Ch]
  004a4dd9  6683fa0c              cmp       dx,0Ch
  004a4ddd  7d35                  jge       short 004A4E14h
  004a4ddf  8b4c2410              mov       ecx,[esp+10h]
  004a4de3  8bc7                  mov       eax,edi
  004a4de5  d1f8                  sar       eax,1
  004a4de7  83c020                add       eax,20h
  004a4dea  8d1c08                lea       ebx,[eax+ecx]
  004a4ded  8d2c00                lea       ebp,[eax+eax]
  004a4df0  81e3ff0f0000          and       ebx,0FFFh
  004a4df6  3bdd                  cmp       ebx,ebp
  004a4df8  7f06                  jg        short 004A4E00h
  004a4dfa  66014e32              add       [esi+32h],cx
  004a4dfe  eb14                  jmp       short 004A4E14h

LAB_004a4e00:
  004a4e00  81e1ff0f0000          and       ecx,0FFFh
  004a4e06  81f900080000          cmp       ecx,800h
  004a4e0c  7c02                  jl        short 004A4E10h
  004a4e0e  f7d8                  neg       eax

LAB_004a4e10:
  004a4e10  66014632              add       [esi+32h],ax

LAB_004a4e14:
  004a4e14  6683fa0c              cmp       dx,0Ch
  004a4e18  755d                  jne       short 004A4E77h
  004a4e1a  0fbf5632              movsx     edx,word [esi+32h]
  004a4e1e  81c700030000          add       edi,300h
  004a4e24  52                    push      edx
  004a4e25  c1e707                shl       edi,7
  004a4e28  897e5c                mov       [esi+5Ch],edi
  004a4e2b  e8ec0b0a00            call      00545A1Ch
  004a4e30  0faf465c              imul      eax,[esi+5Ch]
  004a4e34  8b7e50                mov       edi,[esi+50h]
  004a4e37  c1f80c                sar       eax,0Ch
  004a4e3a  03f8                  add       edi,eax
  004a4e3c  0fbf4632              movsx     eax,word [esi+32h]
  004a4e40  50                    push      eax
  004a4e41  897e50                mov       [esi+50h],edi
  004a4e44  e8cd0b0a00            call      00545A16h
  004a4e49  0faf465c              imul      eax,[esi+5Ch]
  004a4e4d  8b6e58                mov       ebp,[esi+58h]
  004a4e50  8d4e1c                lea       ecx,[esi+1Ch]
  004a4e53  c1f80c                sar       eax,0Ch
  004a4e56  51                    push      ecx
  004a4e57  6800180000            push      1800h
  004a4e5c  03e8                  add       ebp,eax
  004a4e5e  68aa040000            push      4AAh
  004a4e63  6a3f                  push      3Fh
  004a4e65  896e58                mov       [esi+58h],ebp
  004a4e68  c7465c00000000        mov       dword [esi+5Ch],0
  004a4e6f  e8ac8c0900            call      0053DB20h
  004a4e74  83c410                add       esp,10h

LAB_004a4e77:
  004a4e77  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004a4e7c  7c51                  jl        short 004A4ECFh
  004a4e7e  6a00                  push      0
  004a4e80  8d561c                lea       edx,[esi+1Ch]
  004a4e83  68002a5500            push      552A00h
  004a4e88  52                    push      edx
  004a4e89  56                    push      esi
  004a4e8a  e8a14d0700            call      00519C30h
  004a4e8f  8bf8                  mov       edi,eax
  004a4e91  a1b8e05d00            mov       eax,[5DE0B8h]
  004a4e96  83c410                add       esp,10h
  004a4e99  85c0                  test      eax,eax
  004a4e9b  7432                  je        short 004A4ECFh
  004a4e9d  8b17                  mov       edx,[edi]
  004a4e9f  57                    push      edi
  004a4ea0  83ca40                or        edx,40h
  004a4ea3  56                    push      esi
  004a4ea4  66c787960000001000    mov       word [edi+96h],10h
  004a4ead  8917                  mov       [edi],edx
  004a4eaf  e8cc4c0900            call      00539B80h
  004a4eb4  83c408                add       esp,8
  004a4eb7  83c71c                add       edi,1Ch
  004a4eba  57                    push      edi
  004a4ebb  6800180000            push      1800h
  004a4ec0  6800030000            push      300h
  004a4ec5  6a03                  push      3
  004a4ec7  e8548c0900            call      0053DB20h
  004a4ecc  83c410                add       esp,10h

LAB_004a4ecf:
  004a4ecf  8b06                  mov       eax,[esi]
  004a4ed1  a900000002            test      eax,2000000h
  004a4ed6  0f84cd000000          je        near 004A4FA9h
  004a4edc  24fd                  and       al,0FDh
  004a4ede  c7465c00000000        mov       dword [esi+5Ch],0
  004a4ee5  8906                  mov       [esi],eax
  004a4ee7  e9f3010000            jmp       004A50DFh

LAB_004a4eec:
  004a4eec  a811                  test      al,11h
  004a4eee  0f85eb010000          jne       near 004A50DFh
  004a4ef4  a904080000            test      eax,804h
  004a4ef9  0f85e0010000          jne       near 004A50DFh
  004a4eff  33c9                  xor       ecx,ecx
  004a4f01  8a4e42                mov       cl,[esi+42h]
  004a4f04  83f903                cmp       ecx,3
  004a4f07  0f87d2010000          ja        near 004A50DFh
  004a4f0d  ff248dd0544a00        jmp       dword [ecx*4+4A54D0h]
  004a4f14  33c9                  xor       ecx,ecx
  004a4f16  66394e18              cmp       [esi+18h],cx
  004a4f1a  740a                  je        short 004A4F26h
  004a4f1c  0d00000001            or        eax,1000000h
  004a4f21  894e18                mov       [esi+18h],ecx
  004a4f24  8906                  mov       [esi],eax

LAB_004a4f26:
  004a4f26  817c24182c010000      cmp       dword [esp+18h],12Ch
  004a4f2e  7d09                  jge       short 004A4F39h
  004a4f30  c6464300              mov       byte [esi+43h],0
  004a4f34  e9a6010000            jmp       004A50DFh

LAB_004a4f39:
  004a4f39  8a4643                mov       al,[esi+43h]
  004a4f3c  84c0                  test      al,al
  004a4f3e  7405                  je        short 004A4F45h
  004a4f40  fec8                  dec       al
  004a4f42  884643                mov       [esi+43h],al

LAB_004a4f45:
  004a4f45  894e5c                mov       [esi+5Ch],ecx
  004a4f48  e992010000            jmp       004A50DFh
  004a4f4d  b901000000            mov       ecx,1
  004a4f52  66394e18              cmp       [esi+18h],cx
  004a4f56  740a                  je        short 004A4F62h
  004a4f58  0d00000001            or        eax,1000000h
  004a4f5d  894e18                mov       [esi+18h],ecx
  004a4f60  8906                  mov       [esi],eax

LAB_004a4f62:
  004a4f62  8b44243c              mov       eax,[esp+3Ch]
  004a4f66  8b4c2410              mov       ecx,[esp+10h]
  004a4f6a  8d840080000000        lea       eax,[eax+eax+80h]
  004a4f71  8d1408                lea       edx,[eax+ecx]
  004a4f74  8d3c00                lea       edi,[eax+eax]
  004a4f77  81e2ff0f0000          and       edx,0FFFh
  004a4f7d  3bd7                  cmp       edx,edi
  004a4f7f  7f14                  jg        short 004A4F95h
  004a4f81  66014e32              add       [esi+32h],cx
  004a4f85  c6464300              mov       byte [esi+43h],0
  004a4f89  c7465c00000000        mov       dword [esi+5Ch],0
  004a4f90  e94a010000            jmp       004A50DFh

LAB_004a4f95:
  004a4f95  81e1ff0f0000          and       ecx,0FFFh
  004a4f9b  81f900080000          cmp       ecx,800h
  004a4fa1  7c02                  jl        short 004A4FA5h
  004a4fa3  f7d8                  neg       eax

LAB_004a4fa5:
  004a4fa5  66014632              add       [esi+32h],ax

LAB_004a4fa9:
  004a4fa9  c7465c00000000        mov       dword [esi+5Ch],0
  004a4fb0  e92a010000            jmp       004A50DFh
  004a4fb5  b901000000            mov       ecx,1
  004a4fba  66394e18              cmp       [esi+18h],cx
  004a4fbe  740a                  je        short 004A4FCAh
  004a4fc0  0d00000001            or        eax,1000000h
  004a4fc5  894e18                mov       [esi+18h],ecx
  004a4fc8  8906                  mov       [esi],eax

LAB_004a4fca:
  004a4fca  0fbf4610              movsx     eax,word [esi+10h]
  004a4fce  39442418              cmp       [esp+18h],eax
  004a4fd2  7d09                  jge       short 004A4FDDh
  004a4fd4  c6464300              mov       byte [esi+43h],0
  004a4fd8  e902010000            jmp       004A50DFh

LAB_004a4fdd:
  004a4fdd  8a4643                mov       al,[esi+43h]
  004a4fe0  84c0                  test      al,al
  004a4fe2  7405                  je        short 004A4FE9h
  004a4fe4  fec8                  dec       al
  004a4fe6  884643                mov       [esi+43h],al

LAB_004a4fe9:
  004a4fe9  8b54243c              mov       edx,[esp+3Ch]
  004a4fed  8b4c2410              mov       ecx,[esp+10h]
  004a4ff1  8d441240              lea       eax,[edx+edx+40h]
  004a4ff5  8d3c08                lea       edi,[eax+ecx]
  004a4ff8  8d1c00                lea       ebx,[eax+eax]
  004a4ffb  81e7ff0f0000          and       edi,0FFFh
  004a5001  3bfb                  cmp       edi,ebx
  004a5003  7f15                  jg        short 004A501Ah
  004a5005  66014e32              add       [esi+32h],cx
  004a5009  81c220010000          add       edx,120h
  004a500f  c1e207                shl       edx,7
  004a5012  89565c                mov       [esi+5Ch],edx
  004a5015  e9c5000000            jmp       004A50DFh

LAB_004a501a:
  004a501a  81e1ff0f0000          and       ecx,0FFFh
  004a5020  81f900080000          cmp       ecx,800h
  004a5026  7c02                  jl        short 004A502Ah
  004a5028  f7d8                  neg       eax

LAB_004a502a:
  004a502a  66014632              add       [esi+32h],ax
  004a502e  81c220010000          add       edx,120h
  004a5034  c1e207                shl       edx,7
  004a5037  89565c                mov       [esi+5Ch],edx
  004a503a  e9a0000000            jmp       004A50DFh
  004a503f  8a4e43                mov       cl,[esi+43h]
  004a5042  84c9                  test      cl,cl
  004a5044  7405                  je        short 004A504Bh
  004a5046  fec9                  dec       cl
  004a5048  884e43                mov       [esi+43h],cl

LAB_004a504b:
  004a504b  b901000000            mov       ecx,1
  004a5050  66394e18              cmp       [esi+18h],cx
  004a5054  740a                  je        short 004A5060h
  004a5056  0d00000001            or        eax,1000000h
  004a505b  894e18                mov       [esi+18h],ecx
  004a505e  8906                  mov       [esi],eax

LAB_004a5060:
  004a5060  8b4c243c              mov       ecx,[esp+3Ch]
  004a5064  0fbf5610              movsx     edx,word [esi+10h]
  004a5068  81c100010000          add       ecx,100h
  004a506e  52                    push      edx
  004a506f  c1e107                shl       ecx,7
  004a5072  894e5c                mov       [esi+5Ch],ecx
  004a5075  e8a2090a00            call      00545A1Ch
  004a507a  0faf465c              imul      eax,[esi+5Ch]
  004a507e  8b5e1c                mov       ebx,[esi+1Ch]
  004a5081  c1f80c                sar       eax,0Ch
  004a5084  03d8                  add       ebx,eax
  004a5086  0fbf4610              movsx     eax,word [esi+10h]
  004a508a  50                    push      eax
  004a508b  895e1c                mov       [esi+1Ch],ebx
  004a508e  e883090a00            call      00545A16h
  004a5093  0faf465c              imul      eax,[esi+5Ch]
  004a5097  8b4e24                mov       ecx,[esi+24h]
  004a509a  c7465c00000000        mov       dword [esi+5Ch],0
  004a50a1  c1f80c                sar       eax,0Ch
  004a50a4  03c8                  add       ecx,eax
  004a50a6  894e24                mov       [esi+24h],ecx
  004a50a9  8b4c243c              mov       ecx,[esp+3Ch]
  004a50ad  8d440940              lea       eax,[ecx+ecx+40h]
  004a50b1  8b4c2410              mov       ecx,[esp+10h]
  004a50b5  8d1408                lea       edx,[eax+ecx]
  004a50b8  8d3c00                lea       edi,[eax+eax]
  004a50bb  81e2ff0f0000          and       edx,0FFFh
  004a50c1  3bd7                  cmp       edx,edi
  004a50c3  7f06                  jg        short 004A50CBh
  004a50c5  66014e32              add       [esi+32h],cx
  004a50c9  eb14                  jmp       short 004A50DFh

LAB_004a50cb:
  004a50cb  81e1ff0f0000          and       ecx,0FFFh
  004a50d1  81f900080000          cmp       ecx,800h
  004a50d7  7c02                  jl        short 004A50DBh
  004a50d9  f7d8                  neg       eax

LAB_004a50db:
  004a50db  66014632              add       [esi+32h],ax

LAB_004a50df:
  004a50df  668b4e18              mov       cx,[esi+18h]
  004a50e3  bb04000000            mov       ebx,4
  004a50e8  6683f901              cmp       cx,1
  004a50ec  750f                  jne       short 004A50FDh
  004a50ee  668b461a              mov       ax,[esi+1Ah]
  004a50f2  663bc3                cmp       ax,bx
  004a50f5  7412                  je        short 004A5109h
  004a50f7  663d0b00              cmp       ax,0Bh
  004a50fb  740c                  je        short 004A5109h

LAB_004a50fd:
  004a50fd  663bcb                cmp       cx,bx
  004a5100  7529                  jne       short 004A512Bh
  004a5102  66837e1a08            cmp       word [esi+1Ah],8
  004a5107  7522                  jne       short 004A512Bh

LAB_004a5109:
  004a5109  8d461c                lea       eax,[esi+1Ch]
  004a510c  c705ace05d00f8ffffff  mov       dword [5DE0ACh],0FFFFFFF8h
  004a5116  50                    push      eax
  004a5117  6800180000            push      1800h
  004a511c  6800020000            push      200h
  004a5121  6a35                  push      35h
  004a5123  e8f8890900            call      0053DB20h
  004a5128  83c410                add       esp,10h

LAB_004a512b:
  004a512b  8b0dace05d00          mov       ecx,[5DE0ACh]
  004a5131  85c9                  test      ecx,ecx
  004a5133  741e                  je        short 004A5153h
  004a5135  8bc1                  mov       eax,ecx
  004a5137  c1f802                sar       eax,2
  004a513a  2bc1                  sub       eax,ecx
  004a513c  a3ace05d00            mov       [5DE0ACh],eax
  004a5141  7902                  jns       short 004A5145h
  004a5143  f7d8                  neg       eax

LAB_004a5145:
  004a5145  3bc3                  cmp       eax,ebx
  004a5147  7d0a                  jge       short 004A5153h
  004a5149  c705ace05d0000000000  mov       dword [5DE0ACh],0

LAB_004a5153:
  004a5153  8b4c241c              mov       ecx,[esp+1Ch]
  004a5157  66c7869a0000000070    mov       word [esi+9Ah],7000h
  004a5160  51                    push      ecx
  004a5161  56                    push      esi
  004a5162  e8f92ffeff            call      00488160h
  004a5167  83c408                add       esp,8
  004a516a  56                    push      esi
  004a516b  e8d0e70800            call      00533940h
  004a5170  83c404                add       esp,4
  004a5173  56                    push      esi
  004a5174  e877a90300            call      004DFAF0h
  004a5179  8b542434              mov       edx,[esp+34h]
  004a517d  83c404                add       esp,4
  004a5180  52                    push      edx
  004a5181  56                    push      esi
  004a5182  e8e9e50300            call      004E3770h
  004a5187  8b442424              mov       eax,[esp+24h]
  004a518b  83c408                add       esp,8
  004a518e  85c0                  test      eax,eax
  004a5190  0f8430030000          je        near 004A54C6h
  004a5196  f70617080000          test      dword [esi],817h
  004a519c  0f8524030000          jne       near 004A54C6h
  004a51a2  8a4643                mov       al,[esi+43h]
  004a51a5  84c0                  test      al,al
  004a51a7  0f8519030000          jne       near 004A54C6h
  004a51ad  8b466c                mov       eax,[esi+6Ch]
  004a51b0  8b6e1c                mov       ebp,[esi+1Ch]
  004a51b3  8b4e24                mov       ecx,[esi+24h]
  004a51b6  8b781c                mov       edi,[eax+1Ch]
  004a51b9  2bfd                  sub       edi,ebp
  004a51bb  8b6824                mov       ebp,[eax+24h]
  004a51be  2be9                  sub       ebp,ecx
  004a51c0  c1ff0c                sar       edi,0Ch
  004a51c3  c1fd0c                sar       ebp,0Ch
  004a51c6  8bc7                  mov       eax,edi
  004a51c8  8bcd                  mov       ecx,ebp
  004a51ca  0fafc7                imul      eax,edi
  004a51cd  0fafcd                imul      ecx,ebp
  004a51d0  03c1                  add       eax,ecx
  004a51d2  50                    push      eax
  004a51d3  e838080a00            call      00545A10h
  004a51d8  55                    push      ebp
  004a51d9  57                    push      edi
  004a51da  89442420              mov       [esp+20h],eax
  004a51de  e83f080a00            call      00545A22h
  004a51e3  668b5632              mov       dx,[esi+32h]
  004a51e7  2bc2                  sub       eax,edx
  004a51e9  25ff0f0000            and       eax,0FFFh
  004a51ee  89442410              mov       [esp+10h],eax
  004a51f2  8b442418              mov       eax,[esp+18h]
  004a51f6  3dfa000000            cmp       eax,0FAh
  004a51fb  0f8d4d010000          jge       near 004A534Eh
  004a5201  e83a1f0a00            call      00547140h
  004a5206  99                    cdq
  004a5207  b964000000            mov       ecx,64h
  004a520c  f7f9                  idiv      ecx
  004a520e  83fa3c                cmp       edx,3Ch
  004a5211  7d68                  jge       short 004A527Bh
  004a5213  8b542410              mov       edx,[esp+10h]
  004a5217  8b7c2414              mov       edi,[esp+14h]
  004a521b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a5222  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a5229  25ff0f0000            and       eax,0FFFh
  004a522e  3bc1                  cmp       eax,ecx
  004a5230  0f8f4d010000          jg        near 004A5383h
  004a5236  e8051f0a00            call      00547140h
  004a523b  99                    cdq
  004a523c  8d4c3f01              lea       ecx,[edi+edi+1]
  004a5240  f7f9                  idiv      ecx
  004a5242  8b442418              mov       eax,[esp+18h]
  004a5246  81c2c8000000          add       edx,0C8h
  004a524c  3bc2                  cmp       eax,edx
  004a524e  0f8d2f010000          jge       near 004A5383h
  004a5254  8b16                  mov       edx,[esi]
  004a5256  c7461803000000        mov       dword [esi+18h],3
  004a525d  81e2fffffffe          and       edx,0FEFFFFFFh
  004a5263  c6464200              mov       byte [esi+42h],0
  004a5267  83ca02                or        edx,2
  004a526a  c7465c00000000        mov       dword [esi+5Ch],0
  004a5271  8916                  mov       [esi],edx
  004a5273  5f                    pop       edi
  004a5274  5e                    pop       esi
  004a5275  5d                    pop       ebp
  004a5276  5b                    pop       ebx
  004a5277  83c428                add       esp,28h
  004a527a  c3                    ret

LAB_004a527b:
  004a527b  83fa4b                cmp       edx,4Bh
  004a527e  7d2d                  jge       short 004A52ADh
  004a5280  c6464202              mov       byte [esi+42h],2
  004a5284  e8b71e0a00            call      00547140h
  004a5289  99                    cdq
  004a528a  b90a000000            mov       ecx,0Ah
  004a528f  66c746106400          mov       word [esi+10h],64h
  004a5295  f7f9                  idiv      ecx
  004a5297  8b442414              mov       eax,[esp+14h]
  004a529b  d1f8                  sar       eax,1
  004a529d  02d0                  add       dl,al
  004a529f  80c214                add       dl,14h
  004a52a2  885643                mov       [esi+43h],dl
  004a52a5  5f                    pop       edi
  004a52a6  5e                    pop       esi
  004a52a7  5d                    pop       ebp
  004a52a8  5b                    pop       ebx
  004a52a9  83c428                add       esp,28h
  004a52ac  c3                    ret

LAB_004a52ad:
  004a52ad  83fa5a                cmp       edx,5Ah
  004a52b0  7d46                  jge       short 004A52F8h
  004a52b2  8b4c2410              mov       ecx,[esp+10h]
  004a52b6  8b7c2414              mov       edi,[esp+14h]
  004a52ba  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004a52c1  8d04fd00020000        lea       eax,[edi*8+200h]
  004a52c8  81e2ff0f0000          and       edx,0FFFh
  004a52ce  3bd0                  cmp       edx,eax
  004a52d0  7f2a                  jg        short 004A52FCh
  004a52d2  8b4c243c              mov       ecx,[esp+3Ch]
  004a52d6  8b3e                  mov       edi,[esi]
  004a52d8  c1f903                sar       ecx,3
  004a52db  81cf02000001          or        edi,1000002h
  004a52e1  80c118                add       cl,18h
  004a52e4  893e                  mov       [esi],edi
  004a52e6  895e18                mov       [esi+18h],ebx
  004a52e9  c6464201              mov       byte [esi+42h],1
  004a52ed  884e43                mov       [esi+43h],cl
  004a52f0  5f                    pop       edi
  004a52f1  5e                    pop       esi
  004a52f2  5d                    pop       ebp
  004a52f3  5b                    pop       ebx
  004a52f4  83c428                add       esp,28h
  004a52f7  c3                    ret

LAB_004a52f8:
  004a52f8  8b7c2414              mov       edi,[esp+14h]

LAB_004a52fc:
  004a52fc  c6464203              mov       byte [esi+42h],3
  004a5300  e83b1e0a00            call      00547140h
  004a5305  99                    cdq
  004a5306  b90a000000            mov       ecx,0Ah
  004a530b  f7f9                  idiv      ecx
  004a530d  8b4c243c              mov       ecx,[esp+3Ch]
  004a5311  8bc7                  mov       eax,edi
  004a5313  d1f8                  sar       eax,1
  004a5315  8db980000000          lea       edi,[ecx+80h]
  004a531b  c1e704                shl       edi,4
  004a531e  02d0                  add       dl,al
  004a5320  80c214                add       dl,14h
  004a5323  885643                mov       [esi+43h],dl
  004a5326  e8151e0a00            call      00547140h
  004a532b  99                    cdq
  004a532c  f7ff                  idiv      edi
  004a532e  d1ff                  sar       edi,1
  004a5330  2bd7                  sub       edx,edi
  004a5332  8b7c2410              mov       edi,[esp+10h]
  004a5336  03d7                  add       edx,edi
  004a5338  66035632              add       dx,[esi+32h]
  004a533c  81c200080000          add       edx,800h
  004a5342  66895610              mov       [esi+10h],dx
  004a5346  5f                    pop       edi
  004a5347  5e                    pop       esi
  004a5348  5d                    pop       ebp
  004a5349  5b                    pop       ebx
  004a534a  83c428                add       esp,28h
  004a534d  c3                    ret

LAB_004a534e:
  004a534e  3df4010000            cmp       eax,1F4h
  004a5353  0f8de1000000          jge       near 004A543Ah
  004a5359  e8e21d0a00            call      00547140h
  004a535e  99                    cdq
  004a535f  b964000000            mov       ecx,64h
  004a5364  f7f9                  idiv      ecx
  004a5366  83fa32                cmp       edx,32h
  004a5369  7d4d                  jge       short 004A53B8h
  004a536b  8b542410              mov       edx,[esp+10h]
  004a536f  81c200040000          add       edx,400h
  004a5375  81e2ff0f0000          and       edx,0FFFh
  004a537b  81fa00080000          cmp       edx,800h
  004a5381  7c10                  jl        short 004A5393h

LAB_004a5383:
  004a5383  c6464201              mov       byte [esi+42h],1
  004a5387  c6464301              mov       byte [esi+43h],1
  004a538b  5f                    pop       edi
  004a538c  5e                    pop       esi
  004a538d  5d                    pop       ebp
  004a538e  5b                    pop       ebx
  004a538f  83c428                add       esp,28h
  004a5392  c3                    ret

LAB_004a5393:
  004a5393  8b44243c              mov       eax,[esp+3Ch]
  004a5397  8b3e                  mov       edi,[esi]
  004a5399  c1f803                sar       eax,3
  004a539c  81cf02000001          or        edi,1000002h
  004a53a2  0418                  add       al,18h
  004a53a4  893e                  mov       [esi],edi
  004a53a6  895e18                mov       [esi+18h],ebx
  004a53a9  c6464201              mov       byte [esi+42h],1
  004a53ad  884643                mov       [esi+43h],al
  004a53b0  5f                    pop       edi
  004a53b1  5e                    pop       esi
  004a53b2  5d                    pop       ebp
  004a53b3  5b                    pop       ebx
  004a53b4  83c428                add       esp,28h
  004a53b7  c3                    ret

LAB_004a53b8:
  004a53b8  83fa46                cmp       edx,46h
  004a53bb  7d26                  jge       short 004A53E3h
  004a53bd  8b0e                  mov       ecx,[esi]
  004a53bf  c7461805000000        mov       dword [esi+18h],5
  004a53c6  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a53cc  c6464202              mov       byte [esi+42h],2
  004a53d0  83c902                or        ecx,2
  004a53d3  66c746100100          mov       word [esi+10h],1
  004a53d9  890e                  mov       [esi],ecx
  004a53db  5f                    pop       edi
  004a53dc  5e                    pop       esi
  004a53dd  5d                    pop       ebp
  004a53de  5b                    pop       ebx
  004a53df  83c428                add       esp,28h
  004a53e2  c3                    ret

LAB_004a53e3:
  004a53e3  83fa5a                cmp       edx,5Ah
  004a53e6  0f8d8b000000          jge       near 004A5477h
  004a53ec  c6464203              mov       byte [esi+42h],3
  004a53f0  e84b1d0a00            call      00547140h
  004a53f5  99                    cdq
  004a53f6  b90a000000            mov       ecx,0Ah
  004a53fb  f7f9                  idiv      ecx
  004a53fd  8b442414              mov       eax,[esp+14h]
  004a5401  8b4c243c              mov       ecx,[esp+3Ch]
  004a5405  d1f8                  sar       eax,1
  004a5407  8db980000000          lea       edi,[ecx+80h]
  004a540d  c1e704                shl       edi,4
  004a5410  02d0                  add       dl,al
  004a5412  80c214                add       dl,14h
  004a5415  885643                mov       [esi+43h],dl
  004a5418  e8231d0a00            call      00547140h
  004a541d  99                    cdq
  004a541e  f7ff                  idiv      edi
  004a5420  8b4c2410              mov       ecx,[esp+10h]
  004a5424  d1ff                  sar       edi,1
  004a5426  2bd7                  sub       edx,edi
  004a5428  03d1                  add       edx,ecx
  004a542a  66035632              add       dx,[esi+32h]
  004a542e  66895610              mov       [esi+10h],dx
  004a5432  5f                    pop       edi
  004a5433  5e                    pop       esi
  004a5434  5d                    pop       ebp
  004a5435  5b                    pop       ebx
  004a5436  83c428                add       esp,28h
  004a5439  c3                    ret

LAB_004a543a:
  004a543a  e8011d0a00            call      00547140h
  004a543f  99                    cdq
  004a5440  b964000000            mov       ecx,64h
  004a5445  f7f9                  idiv      ecx
  004a5447  83fa1e                cmp       edx,1Eh
  004a544a  7d26                  jge       short 004A5472h
  004a544c  8b16                  mov       edx,[esi]
  004a544e  895e18                mov       [esi+18h],ebx
  004a5451  81ca02000001          or        edx,1000002h
  004a5457  c6464201              mov       byte [esi+42h],1
  004a545b  8916                  mov       [esi],edx
  004a545d  8b54243c              mov       edx,[esp+3Ch]
  004a5461  c1fa03                sar       edx,3
  004a5464  80c218                add       dl,18h
  004a5467  885643                mov       [esi+43h],dl
  004a546a  5f                    pop       edi
  004a546b  5e                    pop       esi
  004a546c  5d                    pop       ebp
  004a546d  5b                    pop       ebx
  004a546e  83c428                add       esp,28h
  004a5471  c3                    ret

LAB_004a5472:
  004a5472  83fa3c                cmp       edx,3Ch
  004a5475  7d27                  jge       short 004A549Eh

LAB_004a5477:
  004a5477  c6464200              mov       byte [esi+42h],0
  004a547b  e8c01c0a00            call      00547140h
  004a5480  99                    cdq
  004a5481  b90a000000            mov       ecx,0Ah
  004a5486  f7f9                  idiv      ecx
  004a5488  8b442414              mov       eax,[esp+14h]
  004a548c  d1f8                  sar       eax,1
  004a548e  02d0                  add       dl,al
  004a5490  80c214                add       dl,14h
  004a5493  885643                mov       [esi+43h],dl
  004a5496  5f                    pop       edi
  004a5497  5e                    pop       esi
  004a5498  5d                    pop       ebp
  004a5499  5b                    pop       ebx
  004a549a  83c428                add       esp,28h
  004a549d  c3                    ret

LAB_004a549e:
  004a549e  c6464203              mov       byte [esi+42h],3
  004a54a2  e8991c0a00            call      00547140h
  004a54a7  99                    cdq
  004a54a8  b90a000000            mov       ecx,0Ah
  004a54ad  f7f9                  idiv      ecx
  004a54af  8b442414              mov       eax,[esp+14h]
  004a54b3  d1f8                  sar       eax,1
  004a54b5  02d0                  add       dl,al
  004a54b7  80c214                add       dl,14h
  004a54ba  885643                mov       [esi+43h],dl
  004a54bd  e87e1c0a00            call      00547140h
  004a54c2  66894610              mov       [esi+10h],ax

LAB_004a54c6:
  004a54c6  5f                    pop       edi
  004a54c7  5e                    pop       esi
  004a54c8  5d                    pop       ebp
  004a54c9  5b                    pop       ebx
  004a54ca  83c428                add       esp,28h
  004a54cd  c3                    ret

55 cible(s) de saut interne : 0x004a4b6d, 0x004a4bbd, 0x004a4bdc, 0x004a4c30, 0x004a4c3e, 0x004a4c46, 0x004a4c58, 0x004a4c92, 0x004a4ca2, 0x004a4ca6, 0x004a4ccf, 0x004a4d8a, 0x004a4dd1, 0x004a4e00, 0x004a4e10, 0x004a4e14, 0x004a4e77, 0x004a4ecf, 0x004a4eec, 0x004a4f26, 0x004a4f39, 0x004a4f45, 0x004a4f62, 0x004a4f95, 0x004a4fa5, 0x004a4fa9, 0x004a4fca, 0x004a4fdd, 0x004a4fe9, 0x004a501a, 0x004a502a, 0x004a504b, 0x004a5060, 0x004a50cb, 0x004a50db, 0x004a50df, 0x004a50fd, 0x004a5109, 0x004a512b, 0x004a5145, 0x004a5153, 0x004a527b, 0x004a52ad, 0x004a52f8, 0x004a52fc, 0x004a534e, 0x004a5383, 0x004a5393, 0x004a53b8, 0x004a53e3, 0x004a543a, 0x004a5472, 0x004a5477, 0x004a549e, 0x004a54c6
