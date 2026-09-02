C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a1d50  section .text  offset fichier 0xa1150  3904 octets

  004a1d50  81ec84000000          sub       esp,84h
  004a1d56  53                    push      ebx
  004a1d57  55                    push      ebp
  004a1d58  56                    push      esi
  004a1d59  8bb42494000000        mov       esi,[esp+94h]
  004a1d60  b864000000            mov       eax,64h
  004a1d65  33db                  xor       ebx,ebx
  004a1d67  6689442436            mov       [esp+36h],ax
  004a1d6c  668944243a            mov       [esp+3Ah],ax
  004a1d71  8a4641                mov       al,[esi+41h]
  004a1d74  57                    push      edi
  004a1d75  84c0                  test      al,al
  004a1d77  66895c2434            mov       [esp+34h],bx
  004a1d7c  66c74424361e00        mov       word [esp+36h],1Eh
  004a1d83  66c74424383200        mov       word [esp+38h],32h
  004a1d8a  66895c243c            mov       [esp+3Ch],bx
  004a1d8f  7510                  jne       short 004A1DA1h
  004a1d91  53                    push      ebx
  004a1d92  6a05                  push      5
  004a1d94  56                    push      esi
  004a1d95  c6464101              mov       byte [esi+41h],1
  004a1d99  e882f2ffff            call      004A1020h
  004a1d9e  83c40c                add       esp,0Ch

LAB_004a1da1:
  004a1da1  8b461c                mov       eax,[esi+1Ch]
  004a1da4  8b4e20                mov       ecx,[esi+20h]
  004a1da7  8b5624                mov       edx,[esi+24h]
  004a1daa  8d6e1c                lea       ebp,[esi+1Ch]
  004a1dad  89442428              mov       [esp+28h],eax
  004a1db1  8b4664                mov       eax,[esi+64h]
  004a1db4  894c242c              mov       [esp+2Ch],ecx
  004a1db8  8b4e60                mov       ecx,[esi+60h]
  004a1dbb  89542430              mov       [esp+30h],edx
  004a1dbf  668b5632              mov       dx,[esi+32h]
  004a1dc3  8944244c              mov       [esp+4Ch],eax
  004a1dc7  8b467c                mov       eax,[esi+7Ch]
  004a1dca  894c2444              mov       [esp+44h],ecx
  004a1dce  668b8e90000000        mov       cx,[esi+90h]
  004a1dd5  6689942482000000      mov       [esp+82h],dx
  004a1ddd  8b9680000000          mov       edx,[esi+80h]
  004a1de3  89442460              mov       [esp+60h],eax
  004a1de7  8b8684000000          mov       eax,[esi+84h]
  004a1ded  66894c245c            mov       [esp+5Ch],cx
  004a1df2  8b8e88000000          mov       ecx,[esi+88h]
  004a1df8  8b7e6c                mov       edi,[esi+6Ch]
  004a1dfb  89542440              mov       [esp+40h],edx
  004a1dff  8b968c000000          mov       edx,[esi+8Ch]
  004a1e05  89442450              mov       [esp+50h],eax
  004a1e09  894c2458              mov       [esp+58h],ecx
  004a1e0d  8d442424              lea       eax,[esp+24h]
  004a1e11  89542454              mov       [esp+54h],edx
  004a1e15  8d4c2464              lea       ecx,[esp+64h]
  004a1e19  50                    push      eax
  004a1e1a  8d54246c              lea       edx,[esp+6Ch]
  004a1e1e  51                    push      ecx
  004a1e1f  52                    push      edx
  004a1e20  56                    push      esi
  004a1e21  e85a6b0900            call      00538980h
  004a1e26  83c410                add       esp,10h
  004a1e29  8d442448              lea       eax,[esp+48h]
  004a1e2d  8d4c241c              lea       ecx,[esp+1Ch]
  004a1e31  8d542418              lea       edx,[esp+18h]
  004a1e35  50                    push      eax
  004a1e36  51                    push      ecx
  004a1e37  8d44241c              lea       eax,[esp+1Ch]
  004a1e3b  52                    push      edx
  004a1e3c  8d4c241c              lea       ecx,[esp+1Ch]
  004a1e40  50                    push      eax
  004a1e41  51                    push      ecx
  004a1e42  56                    push      esi
  004a1e43  e8d8400900            call      00535F20h
  004a1e48  8b4c2428              mov       ecx,[esp+28h]
  004a1e4c  8b542460              mov       edx,[esp+60h]
  004a1e50  83c418                add       esp,18h
  004a1e53  b840000000            mov       eax,40h
  004a1e58  2bc1                  sub       eax,ecx
  004a1e5a  52                    push      edx
  004a1e5b  56                    push      esi
  004a1e5c  89442428              mov       [esp+28h],eax
  004a1e60  e87b3f0300            call      004D5DE0h
  004a1e65  8b4500                mov       eax,[ebp]
  004a1e68  8b571c                mov       edx,[edi+1Ch]
  004a1e6b  8b4e20                mov       ecx,[esi+20h]
  004a1e6e  2bc2                  sub       eax,edx
  004a1e70  8b5720                mov       edx,[edi+20h]
  004a1e73  83c408                add       esp,8
  004a1e76  2bca                  sub       ecx,edx
  004a1e78  8b5624                mov       edx,[esi+24h]
  004a1e7b  2b5724                sub       edx,[edi+24h]
  004a1e7e  c1f80c                sar       eax,0Ch
  004a1e81  8bf8                  mov       edi,eax
  004a1e83  c1f90c                sar       ecx,0Ch
  004a1e86  0faff8                imul      edi,eax
  004a1e89  8bc1                  mov       eax,ecx
  004a1e8b  c1fa0c                sar       edx,0Ch
  004a1e8e  0fafc1                imul      eax,ecx
  004a1e91  8bca                  mov       ecx,edx
  004a1e93  03f8                  add       edi,eax
  004a1e95  0fafca                imul      ecx,edx
  004a1e98  03f9                  add       edi,ecx
  004a1e9a  57                    push      edi
  004a1e9b  e8703b0a00            call      00545A10h
  004a1ea0  89442414              mov       [esp+14h],eax
  004a1ea4  8b44241c              mov       eax,[esp+1Ch]
  004a1ea8  3bc3                  cmp       eax,ebx
  004a1eaa  0f845f070000          je        near 004A260Fh
  004a1eb0  8b0e                  mov       ecx,[esi]
  004a1eb2  33d2                  xor       edx,edx
  004a1eb4  8a5604                mov       dl,[esi+4]
  004a1eb7  81e1fffffffd          and       ecx,0FDFFFFFFh
  004a1ebd  0fbf7e18              movsx     edi,word [esi+18h]
  004a1ec1  890e                  mov       [esi],ecx
  004a1ec3  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004a1eca  f7c100000001          test      ecx,1000000h
  004a1ed0  8b14ba                mov       edx,[edx+edi*4]
  004a1ed3  741b                  je        short 004A1EF0h
  004a1ed5  66ff461a              inc       word [esi+1Ah]
  004a1ed9  668b4e1a              mov       cx,[esi+1Ah]
  004a1edd  33ff                  xor       edi,edi
  004a1edf  668b7a16              mov       di,[edx+16h]
  004a1ee3  0fbfc9                movsx     ecx,cx
  004a1ee6  3bcf                  cmp       ecx,edi
  004a1ee8  7c29                  jl        short 004A1F13h
  004a1eea  66895e1a              mov       [esi+1Ah],bx
  004a1eee  eb23                  jmp       short 004A1F13h

LAB_004a1ef0:
  004a1ef0  66ff461a              inc       word [esi+1Ah]
  004a1ef4  33ff                  xor       edi,edi
  004a1ef6  668b7a16              mov       di,[edx+16h]
  004a1efa  0fbf561a              movsx     edx,word [esi+1Ah]
  004a1efe  3bd7                  cmp       edx,edi
  004a1f00  7c11                  jl        short 004A1F13h
  004a1f02  668b561a              mov       dx,[esi+1Ah]
  004a1f06  4a                    dec       edx
  004a1f07  81c900000002          or        ecx,2000000h
  004a1f0d  6689561a              mov       [esi+1Ah],dx
  004a1f11  890e                  mov       [esi],ecx

LAB_004a1f13:
  004a1f13  8a4e42                mov       cl,[esi+42h]
  004a1f16  8b16                  mov       edx,[esi]
  004a1f18  80f905                cmp       cl,5
  004a1f1b  7505                  jne       short 004A1F22h
  004a1f1d  83ca10                or        edx,10h
  004a1f20  eb03                  jmp       short 004A1F25h

LAB_004a1f22:
  004a1f22  83e2ef                and       edx,0FFFFFFEFh

LAB_004a1f25:
  004a1f25  8916                  mov       [esi],edx
  004a1f27  8a16                  mov       dl,[esi]
  004a1f29  f6c202                test      dl,2
  004a1f2c  0f8460010000          je        near 004A2092h
  004a1f32  80f90b                cmp       cl,0Bh
  004a1f35  0f8582000000          jne       near 004A1FBDh
  004a1f3b  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004a1f40  757b                  jne       short 004A1FBDh
  004a1f42  8d442434              lea       eax,[esp+34h]
  004a1f46  53                    push      ebx
  004a1f47  50                    push      eax
  004a1f48  55                    push      ebp
  004a1f49  56                    push      esi
  004a1f4a  e8e17c0700            call      00519C30h
  004a1f4f  8bf8                  mov       edi,eax
  004a1f51  a1b8e05d00            mov       eax,[5DE0B8h]
  004a1f56  83c410                add       esp,10h
  004a1f59  3bc3                  cmp       eax,ebx
  004a1f5b  745c                  je        short 004A1FB9h
  004a1f5d  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004a1f62  7555                  jne       short 004A1FB9h
  004a1f64  8b07                  mov       eax,[edi]
  004a1f66  57                    push      edi
  004a1f67  0c40                  or        al,40h
  004a1f69  56                    push      esi
  004a1f6a  66c787960000001000    mov       word [edi+96h],10h
  004a1f73  8907                  mov       [edi],eax
  004a1f75  e8067c0900            call      00539B80h
  004a1f7a  83c408                add       esp,8
  004a1f7d  8d4f1c                lea       ecx,[edi+1Ch]
  004a1f80  51                    push      ecx
  004a1f81  6800180000            push      1800h
  004a1f86  6800030000            push      300h
  004a1f8b  6a03                  push      3
  004a1f8d  e88ebb0900            call      0053DB20h
  004a1f92  83c410                add       esp,10h
  004a1f95  66837e1601            cmp       word [esi+16h],1
  004a1f9a  751d                  jne       short 004A1FB9h
  004a1f9c  e89f510a00            call      00547140h
  004a1fa1  99                    cdq
  004a1fa2  b90f000000            mov       ecx,0Fh
  004a1fa7  f7f9                  idiv      ecx
  004a1fa9  83fa01                cmp       edx,1
  004a1fac  750b                  jne       short 004A1FB9h
  004a1fae  53                    push      ebx
  004a1faf  57                    push      edi
  004a1fb0  56                    push      esi
  004a1fb1  e83a0c0000            call      004A2BF0h
  004a1fb6  83c40c                add       esp,0Ch

LAB_004a1fb9:
  004a1fb9  8b44241c              mov       eax,[esp+1Ch]

LAB_004a1fbd:
  004a1fbd  807e4208              cmp       byte [esi+42h],8
  004a1fc1  0f85cb000000          jne       near 004A2092h
  004a1fc7  8d542434              lea       edx,[esp+34h]
  004a1fcb  53                    push      ebx
  004a1fcc  52                    push      edx
  004a1fcd  55                    push      ebp
  004a1fce  56                    push      esi
  004a1fcf  e85c7c0700            call      00519C30h
  004a1fd4  8bf8                  mov       edi,eax
  004a1fd6  a1b8e05d00            mov       eax,[5DE0B8h]
  004a1fdb  83c410                add       esp,10h
  004a1fde  3bc3                  cmp       eax,ebx
  004a1fe0  0f84a8000000          je        near 004A208Eh
  004a1fe6  8b1f                  mov       ebx,[edi]
  004a1fe8  66c787960000001000    mov       word [edi+96h],10h
  004a1ff1  83cb40                or        ebx,40h
  004a1ff4  57                    push      edi
  004a1ff5  891f                  mov       [edi],ebx
  004a1ff7  8b4c2428              mov       ecx,[esp+28h]
  004a1ffb  56                    push      esi
  004a1ffc  668b4130              mov       ax,[ecx+30h]
  004a2000  0fbfd8                movsx     ebx,ax
  004a2003  66c1f802              sar       ax,2
  004a2007  66894130              mov       [ecx+30h],ax
  004a200b  e8707b0900            call      00539B80h
  004a2010  8b44242c              mov       eax,[esp+2Ch]
  004a2014  83c408                add       esp,8
  004a2017  8d4f1c                lea       ecx,[edi+1Ch]
  004a201a  51                    push      ecx
  004a201b  6800180000            push      1800h
  004a2020  6800030000            push      300h
  004a2025  6a03                  push      3
  004a2027  66895830              mov       [eax+30h],bx
  004a202b  e8f0ba0900            call      0053DB20h
  004a2030  8b07                  mov       eax,[edi]
  004a2032  83c410                add       esp,10h
  004a2035  a900000040            test      eax,40000000h
  004a203a  7550                  jne       short 004A208Ch
  004a203c  6aff                  push      0FFFFFFFFh
  004a203e  6a00                  push      0
  004a2040  6a39                  push      39h
  004a2042  56                    push      esi
  004a2043  e88889fdff            call      0047A9D0h
  004a2048  8b07                  mov       eax,[edi]
  004a204a  83c410                add       esp,10h
  004a204d  0c01                  or        al,1
  004a204f  8907                  mov       [edi],eax
  004a2051  0fbf5632              movsx     edx,word [esi+32h]
  004a2055  52                    push      edx
  004a2056  e8c1390a00            call      00545A1Ch
  004a205b  8b4e5c                mov       ecx,[esi+5Ch]
  004a205e  c1f90b                sar       ecx,0Bh
  004a2061  0fafc1                imul      eax,ecx
  004a2064  894750                mov       [edi+50h],eax
  004a2067  0fbf5632              movsx     edx,word [esi+32h]
  004a206b  52                    push      edx
  004a206c  e8a5390a00            call      00545A16h
  004a2071  8b4e5c                mov       ecx,[esi+5Ch]
  004a2074  c7475400800100        mov       dword [edi+54h],18000h
  004a207b  c1f90b                sar       ecx,0Bh
  004a207e  0fafc1                imul      eax,ecx
  004a2081  894758                mov       [edi+58h],eax
  004a2084  8b44241c              mov       eax,[esp+1Ch]
  004a2088  33db                  xor       ebx,ebx
  004a208a  eb06                  jmp       short 004A2092h

LAB_004a208c:
  004a208c  33db                  xor       ebx,ebx

LAB_004a208e:
  004a208e  8b44241c              mov       eax,[esp+1Ch]

LAB_004a2092:
  004a2092  33c9                  xor       ecx,ecx
  004a2094  8a4e42                mov       cl,[esi+42h]
  004a2097  83f90c                cmp       ecx,0Ch
  004a209a  0f876f050000          ja        near 004A260Fh
  004a20a0  ff248db82b4a00        jmp       dword [ecx*4+4A2BB8h]
  004a20a7  66395e12              cmp       [esi+12h],bx
  004a20ab  751a                  jne       short 004A20C7h
  004a20ad  8b06                  mov       eax,[esi]
  004a20af  66895e18              mov       [esi+18h],bx
  004a20b3  0d00000001            or        eax,1000000h
  004a20b8  66895e1a              mov       [esi+1Ah],bx
  004a20bc  8906                  mov       [esi],eax
  004a20be  66c746120100          mov       word [esi+12h],1
  004a20c4  895e5c                mov       [esi+5Ch],ebx

LAB_004a20c7:
  004a20c7  0fbf5610              movsx     edx,word [esi+10h]
  004a20cb  39542414              cmp       [esp+14h],edx
  004a20cf  7d09                  jge       short 004A20DAh
  004a20d1  c6464300              mov       byte [esi+43h],0
  004a20d5  e935050000            jmp       004A260Fh

LAB_004a20da:
  004a20da  8a4643                mov       al,[esi+43h]
  004a20dd  84c0                  test      al,al
  004a20df  0f84e5000000          je        near 004A21CAh
  004a20e5  fec8                  dec       al
  004a20e7  895e5c                mov       [esi+5Ch],ebx
  004a20ea  884643                mov       [esi+43h],al
  004a20ed  e91d050000            jmp       004A260Fh
  004a20f2  66395e12              cmp       [esi+12h],bx
  004a20f6  7535                  jne       short 004A212Dh
  004a20f8  8b0e                  mov       ecx,[esi]
  004a20fa  b801000000            mov       eax,1
  004a20ff  81c900000001          or        ecx,1000000h
  004a2105  66894618              mov       [esi+18h],ax
  004a2109  66895e1a              mov       [esi+1Ah],bx
  004a210d  890e                  mov       [esi],ecx
  004a210f  66894612              mov       [esi+12h],ax
  004a2113  c7465c00500000        mov       dword [esi+5Ch],5000h
  004a211a  e821500a00            call      00547140h
  004a211f  99                    cdq
  004a2120  b91e000000            mov       ecx,1Eh
  004a2125  f7f9                  idiv      ecx
  004a2127  80c214                add       dl,14h
  004a212a  885643                mov       [esi+43h],dl

LAB_004a212d:
  004a212d  8b442418              mov       eax,[esp+18h]
  004a2131  8a5e43                mov       bl,[esi+43h]
  004a2134  fecb                  dec       bl
  004a2136  8d90c8000000          lea       edx,[eax+0C8h]
  004a213c  885e43                mov       [esi+43h],bl
  004a213f  81e2ff0f0000          and       edx,0FFFh
  004a2145  81fa90010000          cmp       edx,190h
  004a214b  7f09                  jg        short 004A2156h
  004a214d  c6464300              mov       byte [esi+43h],0
  004a2151  e9b9040000            jmp       004A260Fh

LAB_004a2156:
  004a2156  25ff0f0000            and       eax,0FFFh
  004a215b  3d00080000            cmp       eax,800h
  004a2160  7d0c                  jge       short 004A216Eh
  004a2162  8b442410              mov       eax,[esp+10h]
  004a2166  83c032                add       eax,32h
  004a2169  e99d040000            jmp       004A260Bh

LAB_004a216e:
  004a216e  8b4c2410              mov       ecx,[esp+10h]
  004a2172  b8ceffffff            mov       eax,0FFFFFFCEh
  004a2177  2bc1                  sub       eax,ecx
  004a2179  e98d040000            jmp       004A260Bh
  004a217e  66395e12              cmp       [esi+12h],bx
  004a2182  8b4c2410              mov       ecx,[esp+10h]
  004a2186  b801000000            mov       eax,1
  004a218b  7525                  jne       short 004A21B2h
  004a218d  8b16                  mov       edx,[esi]
  004a218f  66894618              mov       [esi+18h],ax
  004a2193  81ca00000001          or        edx,1000000h
  004a2199  66895e1a              mov       [esi+1Ah],bx
  004a219d  8916                  mov       [esi],edx
  004a219f  8d14c9                lea       edx,[ecx+ecx*8]
  004a21a2  c1e204                shl       edx,4
  004a21a5  81c200500000          add       edx,5000h
  004a21ab  66894612              mov       [esi+12h],ax
  004a21af  89565c                mov       [esi+5Ch],edx

LAB_004a21b2:
  004a21b2  66394612              cmp       [esi+12h],ax
  004a21b6  0f8553040000          jne       near 004A260Fh
  004a21bc  0fbf4610              movsx     eax,word [esi+10h]
  004a21c0  39442414              cmp       [esp+14h],eax
  004a21c4  7d0c                  jge       short 004A21D2h
  004a21c6  c6464300              mov       byte [esi+43h],0

LAB_004a21ca:
  004a21ca  895e5c                mov       [esi+5Ch],ebx
  004a21cd  e93d040000            jmp       004A260Fh

LAB_004a21d2:
  004a21d2  8a4643                mov       al,[esi+43h]
  004a21d5  84c0                  test      al,al
  004a21d7  7405                  je        short 004A21DEh
  004a21d9  fec8                  dec       al
  004a21db  884643                mov       [esi+43h],al

LAB_004a21de:
  004a21de  8b442418              mov       eax,[esp+18h]
  004a21e2  8d9000010000          lea       edx,[eax+100h]
  004a21e8  81e2ff0f0000          and       edx,0FFFh
  004a21ee  81fa00020000          cmp       edx,200h
  004a21f4  0f8e15040000          jle       near 004A260Fh
  004a21fa  25ff0f0000            and       eax,0FFFh
  004a21ff  3d00080000            cmp       eax,800h
  004a2204  7d0c                  jge       short 004A2212h
  004a2206  8d840980000000        lea       eax,[ecx+ecx+80h]
  004a220d  e9f9030000            jmp       004A260Bh

LAB_004a2212:
  004a2212  03c9                  add       ecx,ecx
  004a2214  b880ffffff            mov       eax,0FFFFFF80h
  004a2219  2bc1                  sub       eax,ecx
  004a221b  e9eb030000            jmp       004A260Bh
  004a2220  66837e1201            cmp       word [esi+12h],1
  004a2225  0f8596000000          jne       near 004A22C1h
  004a222b  8a4e43                mov       cl,[esi+43h]
  004a222e  84c9                  test      cl,cl
  004a2230  7405                  je        short 004A2237h
  004a2232  fec9                  dec       cl
  004a2234  884e43                mov       [esi+43h],cl

LAB_004a2237:
  004a2237  8b4c2410              mov       ecx,[esp+10h]
  004a223b  8d14c9                lea       edx,[ecx+ecx*8]
  004a223e  c1e204                shl       edx,4
  004a2241  8d8a00500000          lea       ecx,[edx+5000h]
  004a2247  0fafc1                imul      eax,ecx
  004a224a  c1f80c                sar       eax,0Ch
  004a224d  8bf8                  mov       edi,eax
  004a224f  894e5c                mov       [esi+5Ch],ecx
  004a2252  0fbf4610              movsx     eax,word [esi+10h]
  004a2256  50                    push      eax
  004a2257  e8c0370a00            call      00545A1Ch
  004a225c  0fafc7                imul      eax,edi
  004a225f  8b4d00                mov       ecx,[ebp]
  004a2262  c1f80c                sar       eax,0Ch
  004a2265  03c8                  add       ecx,eax
  004a2267  894d00                mov       [ebp],ecx
  004a226a  0fbf4e10              movsx     ecx,word [esi+10h]
  004a226e  51                    push      ecx
  004a226f  e8a2370a00            call      00545A16h
  004a2274  0fafc7                imul      eax,edi
  004a2277  8b6e24                mov       ebp,[esi+24h]
  004a227a  895e5c                mov       [esi+5Ch],ebx
  004a227d  c1f80c                sar       eax,0Ch
  004a2280  03e8                  add       ebp,eax
  004a2282  8b442418              mov       eax,[esp+18h]
  004a2286  896e24                mov       [esi+24h],ebp
  004a2289  8d9000010000          lea       edx,[eax+100h]
  004a228f  81e2ff0f0000          and       edx,0FFFh
  004a2295  81fa00020000          cmp       edx,200h
  004a229b  7e24                  jle       short 004A22C1h
  004a229d  25ff0f0000            and       eax,0FFFh
  004a22a2  3d00080000            cmp       eax,800h
  004a22a7  7d09                  jge       short 004A22B2h
  004a22a9  8b442410              mov       eax,[esp+10h]
  004a22ad  83c032                add       eax,32h
  004a22b0  eb0b                  jmp       short 004A22BDh

LAB_004a22b2:
  004a22b2  8b4c2410              mov       ecx,[esp+10h]
  004a22b6  b8ceffffff            mov       eax,0FFFFFFCEh
  004a22bb  2bc1                  sub       eax,ecx

LAB_004a22bd:
  004a22bd  66014632              add       [esi+32h],ax

LAB_004a22c1:
  004a22c1  66395e12              cmp       [esi+12h],bx
  004a22c5  0f8544030000          jne       near 004A260Fh
  004a22cb  8b0e                  mov       ecx,[esi]
  004a22cd  b801000000            mov       eax,1
  004a22d2  81c900000001          or        ecx,1000000h
  004a22d8  66894618              mov       [esi+18h],ax
  004a22dc  66895e1a              mov       [esi+1Ah],bx
  004a22e0  890e                  mov       [esi],ecx
  004a22e2  66894612              mov       [esi+12h],ax
  004a22e6  e924030000            jmp       004A260Fh
  004a22eb  66395e12              cmp       [esi+12h],bx
  004a22ef  0f8580000000          jne       near 004A2375h
  004a22f5  8b0e                  mov       ecx,[esi]
  004a22f7  f6c101                test      cl,1
  004a22fa  7406                  je        short 004A2302h
  004a22fc  c6464300              mov       byte [esi+43h],0
  004a2300  eb73                  jmp       short 004A2375h

LAB_004a2302:
  004a2302  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a2308  bd01000000            mov       ebp,1
  004a230d  890e                  mov       [esi],ecx
  004a230f  8b4c2410              mov       ecx,[esp+10h]
  004a2313  81c100010000          add       ecx,100h
  004a2319  66896e18              mov       [esi+18h],bp
  004a231d  c1e106                shl       ecx,6
  004a2320  0fafc1                imul      eax,ecx
  004a2323  0fbf5610              movsx     edx,word [esi+10h]
  004a2327  c1f80c                sar       eax,0Ch
  004a232a  52                    push      edx
  004a232b  66895e1a              mov       [esi+1Ah],bx
  004a232f  894e5c                mov       [esi+5Ch],ecx
  004a2332  8bf8                  mov       edi,eax
  004a2334  e8e3360a00            call      00545A1Ch
  004a2339  0fafc7                imul      eax,edi
  004a233c  8b4e50                mov       ecx,[esi+50h]
  004a233f  c1f80c                sar       eax,0Ch
  004a2342  03c8                  add       ecx,eax
  004a2344  0fbf4610              movsx     eax,word [esi+10h]
  004a2348  50                    push      eax
  004a2349  894e50                mov       [esi+50h],ecx
  004a234c  e8c5360a00            call      00545A16h
  004a2351  0fafc7                imul      eax,edi
  004a2354  8b5658                mov       edx,[esi+58h]
  004a2357  8b4e54                mov       ecx,[esi+54h]
  004a235a  c1f80c                sar       eax,0Ch
  004a235d  03d0                  add       edx,eax
  004a235f  8b06                  mov       eax,[esi]
  004a2361  81c100e00100          add       ecx,1E000h
  004a2367  0bc5                  or        eax,ebp
  004a2369  895658                mov       [esi+58h],edx
  004a236c  894e54                mov       [esi+54h],ecx
  004a236f  8906                  mov       [esi],eax
  004a2371  66896e12              mov       [esi+12h],bp

LAB_004a2375:
  004a2375  f60601                test      byte [esi],1
  004a2378  0f8591020000          jne       near 004A260Fh
  004a237e  c6464300              mov       byte [esi+43h],0
  004a2382  e988020000            jmp       004A260Fh
  004a2387  668b4e12              mov       cx,[esi+12h]
  004a238b  ba01000000            mov       edx,1
  004a2390  663bca                cmp       cx,dx
  004a2393  751c                  jne       short 004A23B1h
  004a2395  0fbf4610              movsx     eax,word [esi+10h]
  004a2399  39442414              cmp       [esp+14h],eax
  004a239d  7d06                  jge       short 004A23A5h
  004a239f  c6464300              mov       byte [esi+43h],0
  004a23a3  eb0c                  jmp       short 004A23B1h

LAB_004a23a5:
  004a23a5  8a4643                mov       al,[esi+43h]
  004a23a8  84c0                  test      al,al
  004a23aa  7405                  je        short 004A23B1h
  004a23ac  fec8                  dec       al
  004a23ae  884643                mov       [esi+43h],al

LAB_004a23b1:
  004a23b1  663bcb                cmp       cx,bx
  004a23b4  0f8555020000          jne       near 004A260Fh
  004a23ba  8b442410              mov       eax,[esp+10h]
  004a23be  8b3e                  mov       edi,[esi]
  004a23c0  81cf00000001          or        edi,1000000h
  004a23c6  66895618              mov       [esi+18h],dx
  004a23ca  8d0cc0                lea       ecx,[eax+eax*8]
  004a23cd  66895e1a              mov       [esi+1Ah],bx
  004a23d1  c1e104                shl       ecx,4
  004a23d4  81c100500000          add       ecx,5000h
  004a23da  893e                  mov       [esi],edi
  004a23dc  66895612              mov       [esi+12h],dx
  004a23e0  894e5c                mov       [esi+5Ch],ecx
  004a23e3  e927020000            jmp       004A260Fh
  004a23e8  66395e12              cmp       [esi+12h],bx
  004a23ec  bf01000000            mov       edi,1
  004a23f1  7539                  jne       short 004A242Ch
  004a23f3  8b542410              mov       edx,[esp+10h]
  004a23f7  8b0e                  mov       ecx,[esi]
  004a23f9  83e2fc                and       edx,0FFFFFFFCh
  004a23fc  81c900000001          or        ecx,1000000h
  004a2402  83c250                add       edx,50h
  004a2405  66897e12              mov       [esi+12h],di
  004a2409  c1e20a                shl       edx,0Ah
  004a240c  89565c                mov       [esi+5Ch],edx
  004a240f  66897e18              mov       [esi+18h],di
  004a2413  66895e1a              mov       [esi+1Ah],bx
  004a2417  890e                  mov       [esi],ecx
  004a2419  e8224d0a00            call      00547140h
  004a241e  99                    cdq
  004a241f  b90a000000            mov       ecx,0Ah
  004a2424  f7f9                  idiv      ecx
  004a2426  80c21e                add       dl,1Eh
  004a2429  885643                mov       [esi+43h],dl

LAB_004a242c:
  004a242c  66397e12              cmp       [esi+12h],di
  004a2430  7554                  jne       short 004A2486h
  004a2432  8a4643                mov       al,[esi+43h]
  004a2435  3c14                  cmp       al,14h
  004a2437  763e                  jbe       short 004A2477h
  004a2439  8b4c2418              mov       ecx,[esp+18h]
  004a243d  8d9100010000          lea       edx,[ecx+100h]
  004a2443  81e2ff0f0000          and       edx,0FFFh
  004a2449  81fa00020000          cmp       edx,200h
  004a244f  7e26                  jle       short 004A2477h
  004a2451  81e1ff0f0000          and       ecx,0FFFh
  004a2457  81f900080000          cmp       ecx,800h
  004a245d  7d09                  jge       short 004A2468h
  004a245f  8b4c2410              mov       ecx,[esp+10h]
  004a2463  83c132                add       ecx,32h
  004a2466  eb0b                  jmp       short 004A2473h

LAB_004a2468:
  004a2468  8b542410              mov       edx,[esp+10h]
  004a246c  b9ceffffff            mov       ecx,0FFFFFFCEh
  004a2471  2bca                  sub       ecx,edx

LAB_004a2473:
  004a2473  66014e32              add       [esi+32h],cx

LAB_004a2477:
  004a2477  fec8                  dec       al
  004a2479  3c01                  cmp       al,1
  004a247b  884643                mov       [esi+43h],al
  004a247e  7506                  jne       short 004A2486h
  004a2480  66c746120200          mov       word [esi+12h],2

LAB_004a2486:
  004a2486  66837e1202            cmp       word [esi+12h],2
  004a248b  0f857e010000          jne       near 004A260Fh
  004a2491  8b465c                mov       eax,[esi+5Ch]
  004a2494  8bd0                  mov       edx,eax
  004a2496  f7da                  neg       edx
  004a2498  c1fa03                sar       edx,3
  004a249b  03c2                  add       eax,edx
  004a249d  3d00500000            cmp       eax,5000h
  004a24a2  89465c                mov       [esi+5Ch],eax
  004a24a5  0f8d64010000          jge       near 004A260Fh
  004a24ab  c6464300              mov       byte [esi+43h],0
  004a24af  e95b010000            jmp       004A260Fh
  004a24b4  66395e12              cmp       [esi+12h],bx
  004a24b8  7542                  jne       short 004A24FCh
  004a24ba  e8814c0a00            call      00547140h
  004a24bf  99                    cdq
  004a24c0  b90a000000            mov       ecx,0Ah
  004a24c5  f7f9                  idiv      ecx
  004a24c7  83ea05                sub       edx,5
  004a24ca  8d1492                lea       edx,[edx+edx*4]
  004a24cd  d1e2                  shl       edx,1
  004a24cf  66895610              mov       [esi+10h],dx
  004a24d3  e8684c0a00            call      00547140h
  004a24d8  99                    cdq
  004a24d9  b90a000000            mov       ecx,0Ah
  004a24de  66c746120100          mov       word [esi+12h],1
  004a24e4  f7f9                  idiv      ecx
  004a24e6  80c214                add       dl,14h
  004a24e9  885643                mov       [esi+43h],dl
  004a24ec  8b542410              mov       edx,[esp+10h]
  004a24f0  83e2fc                and       edx,0FFFFFFFCh
  004a24f3  83c214                add       edx,14h
  004a24f6  c1e20a                shl       edx,0Ah
  004a24f9  89565c                mov       [esi+5Ch],edx

LAB_004a24fc:
  004a24fc  66837e1201            cmp       word [esi+12h],1
  004a2501  0f8508010000          jne       near 004A260Fh
  004a2507  668b4610              mov       ax,[esi+10h]
  004a250b  66014632              add       [esi+32h],ax
  004a250f  8a4643                mov       al,[esi+43h]
  004a2512  fec8                  dec       al
  004a2514  884643                mov       [esi+43h],al
  004a2517  e9f3000000            jmp       004A260Fh
  004a251c  66395e12              cmp       [esi+12h],bx
  004a2520  7520                  jne       short 004A2542h
  004a2522  8b0e                  mov       ecx,[esi]
  004a2524  66c746120100          mov       word [esi+12h],1
  004a252a  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a2530  66c746180300          mov       word [esi+18h],3
  004a2536  83c902                or        ecx,2
  004a2539  66895e1a              mov       [esi+1Ah],bx
  004a253d  895e5c                mov       [esi+5Ch],ebx
  004a2540  890e                  mov       [esi],ecx

LAB_004a2542:
  004a2542  66837e1201            cmp       word [esi+12h],1
  004a2547  0f85c2000000          jne       near 004A260Fh
  004a254d  8b06                  mov       eax,[esi]
  004a254f  a900000002            test      eax,2000000h
  004a2554  0f84b5000000          je        near 004A260Fh
  004a255a  24fd                  and       al,0FDh
  004a255c  c6464300              mov       byte [esi+43h],0
  004a2560  8906                  mov       [esi],eax
  004a2562  e9a8000000            jmp       004A260Fh
  004a2567  66395e12              cmp       [esi+12h],bx
  004a256b  754f                  jne       short 004A25BCh
  004a256d  a1c8295500            mov       eax,[5529C8h]
  004a2572  8b2e                  mov       ebp,[esi]
  004a2574  25ffff0000            and       eax,0FFFFh
  004a2579  6aff                  push      0FFFFFFFFh
  004a257b  66894606              mov       [esi+6],ax
  004a257f  25ffff0000            and       eax,0FFFFh
  004a2584  8bd0                  mov       edx,eax
  004a2586  6aff                  push      0FFFFFFFFh
  004a2588  c1f808                sar       eax,8
  004a258b  81e2ff000000          and       edx,0FFh
  004a2591  25ff000000            and       eax,0FFh
  004a2596  52                    push      edx
  004a2597  81e5fffffffe          and       ebp,0FEFFFFFFh
  004a259d  50                    push      eax
  004a259e  56                    push      esi
  004a259f  895e5c                mov       [esi+5Ch],ebx
  004a25a2  66c746180400          mov       word [esi+18h],4
  004a25a8  66895e1a              mov       [esi+1Ah],bx
  004a25ac  892e                  mov       [esi],ebp
  004a25ae  66c746120100          mov       word [esi+12h],1
  004a25b4  e8c7830900            call      0053A980h
  004a25b9  83c414                add       esp,14h

LAB_004a25bc:
  004a25bc  66837e1201            cmp       word [esi+12h],1
  004a25c1  754c                  jne       short 004A260Fh
  004a25c3  f70600000002          test      dword [esi],2000000h
  004a25c9  7404                  je        short 004A25CFh
  004a25cb  c6464300              mov       byte [esi+43h],0

LAB_004a25cf:
  004a25cf  8b442418              mov       eax,[esp+18h]
  004a25d3  8d88c8000000          lea       ecx,[eax+0C8h]
  004a25d9  81e1ff0f0000          and       ecx,0FFFh
  004a25df  81f990010000          cmp       ecx,190h
  004a25e5  7e28                  jle       short 004A260Fh
  004a25e7  25ff0f0000            and       eax,0FFFh
  004a25ec  3d00080000            cmp       eax,800h
  004a25f1  7d0b                  jge       short 004A25FEh
  004a25f3  8b442410              mov       eax,[esp+10h]
  004a25f7  d1f8                  sar       eax,1
  004a25f9  83c032                add       eax,32h
  004a25fc  eb0d                  jmp       short 004A260Bh

LAB_004a25fe:
  004a25fe  8b542410              mov       edx,[esp+10h]
  004a2602  b8ceffffff            mov       eax,0FFFFFFCEh
  004a2607  d1fa                  sar       edx,1
  004a2609  2bc2                  sub       eax,edx

LAB_004a260b:
  004a260b  66014632              add       [esi+32h],ax

LAB_004a260f:
  004a260f  0fbf4632              movsx     eax,word [esi+32h]
  004a2613  50                    push      eax
  004a2614  e803340a00            call      00545A1Ch
  004a2619  0fbf4e32              movsx     ecx,word [esi+32h]
  004a261d  51                    push      ecx
  004a261e  89442470              mov       [esp+70h],eax
  004a2622  e8ef330a00            call      00545A16h
  004a2627  8b5650                mov       edx,[esi+50h]
  004a262a  89442474              mov       [esp+74h],eax
  004a262e  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004a2635  8bc2                  mov       eax,edx
  004a2637  8bfa                  mov       edi,edx
  004a2639  c1f808                sar       eax,8
  004a263c  8b4c241c              mov       ecx,[esp+1Ch]
  004a2640  2bf8                  sub       edi,eax
  004a2642  c1fa03                sar       edx,3
  004a2645  0faf4e5c              imul      ecx,[esi+5Ch]
  004a2649  2bfa                  sub       edi,edx
  004a264b  8b5654                mov       edx,[esi+54h]
  004a264e  8bc5                  mov       eax,ebp
  004a2650  8bda                  mov       ebx,edx
  004a2652  c1f808                sar       eax,8
  004a2655  c1fb08                sar       ebx,8
  004a2658  2bc3                  sub       eax,ebx
  004a265a  897e50                mov       [esi+50h],edi
  004a265d  2bc5                  sub       eax,ebp
  004a265f  56                    push      esi
  004a2660  03c2                  add       eax,edx
  004a2662  8b5658                mov       edx,[esi+58h]
  004a2665  8bda                  mov       ebx,edx
  004a2667  8bea                  mov       ebp,edx
  004a2669  c1fb03                sar       ebx,3
  004a266c  2beb                  sub       ebp,ebx
  004a266e  8b5e1c                mov       ebx,[esi+1Ch]
  004a2671  c1fa08                sar       edx,8
  004a2674  c1f90c                sar       ecx,0Ch
  004a2677  2bea                  sub       ebp,edx
  004a2679  8bd1                  mov       edx,ecx
  004a267b  0faf542470            imul      edx,[esp+70h]
  004a2680  0faf4c2478            imul      ecx,[esp+78h]
  004a2685  c1fa0c                sar       edx,0Ch
  004a2688  c1f90c                sar       ecx,0Ch
  004a268b  896e58                mov       [esi+58h],ebp
  004a268e  03d7                  add       edx,edi
  004a2690  8b7e20                mov       edi,[esi+20h]
  004a2693  03cd                  add       ecx,ebp
  004a2695  8b6e24                mov       ebp,[esi+24h]
  004a2698  03da                  add       ebx,edx
  004a269a  03f8                  add       edi,eax
  004a269c  03e9                  add       ebp,ecx
  004a269e  894654                mov       [esi+54h],eax
  004a26a1  895e1c                mov       [esi+1Ch],ebx
  004a26a4  897e20                mov       [esi+20h],edi
  004a26a7  896e24                mov       [esi+24h],ebp
  004a26aa  e891120900            call      00533940h
  004a26af  8b9690000000          mov       edx,[esi+90h]
  004a26b5  8b4660                mov       eax,[esi+60h]
  004a26b8  8b4e20                mov       ecx,[esi+20h]
  004a26bb  83c404                add       esp,4
  004a26be  80e6fc                and       dh,0FCh
  004a26c1  3bc8                  cmp       ecx,eax
  004a26c3  899690000000          mov       [esi+90h],edx
  004a26c9  7f2a                  jg        short 004A26F5h
  004a26cb  f6c680                test      dh,80h
  004a26ce  7525                  jne       short 004A26F5h
  004a26d0  8b0e                  mov       ecx,[esi]
  004a26d2  894620                mov       [esi+20h],eax
  004a26d5  f6c101                test      cl,1
  004a26d8  740f                  je        short 004A26E9h
  004a26da  83e1fe                and       ecx,0FFFFFFFEh
  004a26dd  33db                  xor       ebx,ebx
  004a26df  890e                  mov       [esi],ecx
  004a26e1  895e54                mov       [esi+54h],ebx
  004a26e4  e923010000            jmp       004A280Ch

LAB_004a26e9:
  004a26e9  83e1fe                and       ecx,0FFFFFFFEh
  004a26ec  33db                  xor       ebx,ebx
  004a26ee  890e                  mov       [esi],ecx
  004a26f0  895e54                mov       [esi+54h],ebx
  004a26f3  eb02                  jmp       short 004A26F7h

LAB_004a26f5:
  004a26f5  33db                  xor       ebx,ebx

LAB_004a26f7:
  004a26f7  8b0e                  mov       ecx,[esi]
  004a26f9  f6c101                test      cl,1
  004a26fc  750a                  jne       short 004A2708h
  004a26fe  394620                cmp       [esi+20h],eax
  004a2701  7e05                  jle       short 004A2708h
  004a2703  83c901                or        ecx,1
  004a2706  890e                  mov       [esi],ecx

LAB_004a2708:
  004a2708  f7c282ad0020          test      edx,2000AD82h
  004a270e  0f84f8000000          je        near 004A280Ch
  004a2714  f60601                test      byte [esi],1
  004a2717  0f85c7000000          jne       near 004A27E4h
  004a271d  807e4207              cmp       byte [esi+42h],7
  004a2721  7421                  je        short 004A2744h
  004a2723  c6464207              mov       byte [esi+42h],7
  004a2727  e8144a0a00            call      00547140h
  004a272c  99                    cdq
  004a272d  b928000000            mov       ecx,28h
  004a2732  66895e12              mov       [esi+12h],bx
  004a2736  f7f9                  idiv      ecx
  004a2738  66c746105000          mov       word [esi+10h],50h
  004a273e  80c214                add       dl,14h
  004a2741  885643                mov       [esi+43h],dl

LAB_004a2744:
  004a2744  8b16                  mov       edx,[esi]
  004a2746  8b44242c              mov       eax,[esp+2Ch]
  004a274a  8b4c2430              mov       ecx,[esp+30h]
  004a274e  83e2fd                and       edx,0FFFFFFFDh
  004a2751  8916                  mov       [esi],edx
  004a2753  8b542428              mov       edx,[esp+28h]
  004a2757  894620                mov       [esi+20h],eax
  004a275a  8b44245c              mov       eax,[esp+5Ch]
  004a275e  89561c                mov       [esi+1Ch],edx
  004a2761  8b542460              mov       edx,[esp+60h]
  004a2765  25ffff0000            and       eax,0FFFFh
  004a276a  894e24                mov       [esi+24h],ecx
  004a276d  8b4c2450              mov       ecx,[esp+50h]
  004a2771  89567c                mov       [esi+7Ch],edx
  004a2774  8b542458              mov       edx,[esp+58h]
  004a2778  898690000000          mov       [esi+90h],eax
  004a277e  8b442440              mov       eax,[esp+40h]
  004a2782  898e84000000          mov       [esi+84h],ecx
  004a2788  8b4c244c              mov       ecx,[esp+4Ch]
  004a278c  898680000000          mov       [esi+80h],eax
  004a2792  8b442454              mov       eax,[esp+54h]
  004a2796  899688000000          mov       [esi+88h],edx
  004a279c  8b542444              mov       edx,[esp+44h]
  004a27a0  6882ad0020            push      2000AD82h
  004a27a5  56                    push      esi
  004a27a6  89868c000000          mov       [esi+8Ch],eax
  004a27ac  894e64                mov       [esi+64h],ecx
  004a27af  895660                mov       [esi+60h],edx
  004a27b2  e8a9360300            call      004D5E60h
  004a27b7  83c408                add       esp,8
  004a27ba  663bc3                cmp       ax,bx
  004a27bd  7515                  jne       short 004A27D4h
  004a27bf  e87c490a00            call      00547140h
  004a27c4  99                    cdq
  004a27c5  33c2                  xor       eax,edx
  004a27c7  2bc2                  sub       eax,edx
  004a27c9  83e001                and       eax,1
  004a27cc  33c2                  xor       eax,edx
  004a27ce  2bc2                  sub       eax,edx
  004a27d0  8d4400ff              lea       eax,[eax+eax-1]

LAB_004a27d4:
  004a27d4  8b4c2410              mov       ecx,[esp+10h]
  004a27d8  83c140                add       ecx,40h
  004a27db  0fafc8                imul      ecx,eax
  004a27de  66014e32              add       [esi+32h],cx
  004a27e2  eb28                  jmp       short 004A280Ch

LAB_004a27e4:
  004a27e4  8b542428              mov       edx,[esp+28h]
  004a27e8  8b442430              mov       eax,[esp+30h]
  004a27ec  895e5c                mov       [esi+5Ch],ebx
  004a27ef  895e50                mov       [esi+50h],ebx
  004a27f2  895e58                mov       [esi+58h],ebx
  004a27f5  89561c                mov       [esi+1Ch],edx
  004a27f8  894624                mov       [esi+24h],eax
  004a27fb  885e42                mov       [esi+42h],bl
  004a27fe  c6464328              mov       byte [esi+43h],28h
  004a2802  66895e12              mov       [esi+12h],bx
  004a2806  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004a280c:
  004a280c  6aff                  push      0FFFFFFFFh
  004a280e  6aff                  push      0FFFFFFFFh
  004a2810  6aff                  push      0FFFFFFFFh
  004a2812  56                    push      esi
  004a2813  e8f80f0200            call      004C3810h
  004a2818  8b44242c              mov       eax,[esp+2Ch]
  004a281c  83c410                add       esp,10h
  004a281f  3bc3                  cmp       eax,ebx
  004a2821  7517                  jne       short 004A283Ah
  004a2823  668b8c2482000000      mov       cx,[esp+82h]
  004a282b  66894e32              mov       [esi+32h],cx
  004a282f  5f                    pop       edi
  004a2830  5e                    pop       esi
  004a2831  5d                    pop       ebp
  004a2832  5b                    pop       ebx
  004a2833  81c484000000          add       esp,84h
  004a2839  c3                    ret

LAB_004a283a:
  004a283a  385e43                cmp       [esi+43h],bl
  004a283d  0f850b030000          jne       near 004A2B4Eh
  004a2843  8b466c                mov       eax,[esi+6Ch]
  004a2846  8b561c                mov       edx,[esi+1Ch]
  004a2849  8b781c                mov       edi,[eax+1Ch]
  004a284c  8b6824                mov       ebp,[eax+24h]
  004a284f  8b4624                mov       eax,[esi+24h]
  004a2852  2bfa                  sub       edi,edx
  004a2854  2be8                  sub       ebp,eax
  004a2856  c1ff0c                sar       edi,0Ch
  004a2859  c1fd0c                sar       ebp,0Ch
  004a285c  8bd7                  mov       edx,edi
  004a285e  8bc5                  mov       eax,ebp
  004a2860  0fafd7                imul      edx,edi
  004a2863  0fafc5                imul      eax,ebp
  004a2866  03d0                  add       edx,eax
  004a2868  52                    push      edx
  004a2869  e8a2310a00            call      00545A10h
  004a286e  55                    push      ebp
  004a286f  57                    push      edi
  004a2870  8944241c              mov       [esp+1Ch],eax
  004a2874  e8a9310a00            call      00545A22h
  004a2879  668b4e32              mov       cx,[esi+32h]
  004a287d  66895e12              mov       [esi+12h],bx
  004a2881  2bc1                  sub       eax,ecx
  004a2883  25ff0f0000            and       eax,0FFFh
  004a2888  89442418              mov       [esp+18h],eax
  004a288c  8b442414              mov       eax,[esp+14h]
  004a2890  3dfa000000            cmp       eax,0FAh
  004a2895  0f8da0000000          jge       near 004A293Bh
  004a289b  e8a0480a00            call      00547140h
  004a28a0  99                    cdq
  004a28a1  b964000000            mov       ecx,64h
  004a28a6  f7f9                  idiv      ecx
  004a28a8  8bea                  mov       ebp,edx
  004a28aa  83fd1e                cmp       ebp,1Eh
  004a28ad  7d77                  jge       short 004A2926h
  004a28af  8b542418              mov       edx,[esp+18h]
  004a28b3  8b7c2420              mov       edi,[esp+20h]
  004a28b7  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a28be  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a28c5  25ff0f0000            and       eax,0FFFh
  004a28ca  3bc1                  cmp       eax,ecx
  004a28cc  7f4e                  jg        short 004A291Ch
  004a28ce  e86d480a00            call      00547140h
  004a28d3  99                    cdq
  004a28d4  8d4c3f01              lea       ecx,[edi+edi+1]
  004a28d8  f7f9                  idiv      ecx
  004a28da  8b442414              mov       eax,[esp+14h]
  004a28de  83c264                add       edx,64h
  004a28e1  3bc2                  cmp       eax,edx
  004a28e3  7d37                  jge       short 004A291Ch
  004a28e5  83fd14                cmp       ebp,14h
  004a28e8  7d24                  jge       short 004A290Eh
  004a28ea  8b16                  mov       edx,[esi]
  004a28ec  c646420b              mov       byte [esi+42h],0Bh
  004a28f0  81e2fffffffe          and       edx,0FEFFFFFFh
  004a28f6  c6464301              mov       byte [esi+43h],1
  004a28fa  83ca02                or        edx,2
  004a28fd  895e5c                mov       [esi+5Ch],ebx
  004a2900  8916                  mov       [esi],edx
  004a2902  66c746180300          mov       word [esi+18h],3
  004a2908  66895e1a              mov       [esi+1Ah],bx
  004a290c  eb31                  jmp       short 004A293Fh

LAB_004a290e:
  004a290e  c6464209              mov       byte [esi+42h],9
  004a2912  c6464301              mov       byte [esi+43h],1
  004a2916  66895e12              mov       [esi+12h],bx
  004a291a  eb23                  jmp       short 004A293Fh

LAB_004a291c:
  004a291c  c6464201              mov       byte [esi+42h],1
  004a2920  c6464301              mov       byte [esi+43h],1
  004a2924  eb19                  jmp       short 004A293Fh

LAB_004a2926:
  004a2926  83fd23                cmp       ebp,23h
  004a2929  0f8d2a020000          jge       near 004A2B59h
  004a292f  c646420c              mov       byte [esi+42h],0Ch
  004a2933  c6464301              mov       byte [esi+43h],1
  004a2937  66895e12              mov       [esi+12h],bx

LAB_004a293b:
  004a293b  8b7c2420              mov       edi,[esp+20h]

LAB_004a293f:
  004a293f  8b442414              mov       eax,[esp+14h]
  004a2943  3dbc020000            cmp       eax,2BCh
  004a2948  0f8de4000000          jge       near 004A2A32h
  004a294e  3dfa000000            cmp       eax,0FAh
  004a2953  0f8cd9000000          jl        near 004A2A32h
  004a2959  e8e2470a00            call      00547140h
  004a295e  99                    cdq
  004a295f  b964000000            mov       ecx,64h
  004a2964  f7f9                  idiv      ecx
  004a2966  83fa14                cmp       edx,14h
  004a2969  7d44                  jge       short 004A29AFh
  004a296b  8b542418              mov       edx,[esp+18h]
  004a296f  81c200040000          add       edx,400h
  004a2975  81e2ff0f0000          and       edx,0FFFh
  004a297b  81fa00080000          cmp       edx,800h
  004a2981  7c0d                  jl        short 004A2990h
  004a2983  c6464201              mov       byte [esi+42h],1
  004a2987  c6464301              mov       byte [esi+43h],1
  004a298b  e9a2000000            jmp       004A2A32h

LAB_004a2990:
  004a2990  c6464207              mov       byte [esi+42h],7
  004a2994  e8a7470a00            call      00547140h
  004a2999  99                    cdq
  004a299a  b914000000            mov       ecx,14h
  004a299f  66895e12              mov       [esi+12h],bx
  004a29a3  f7f9                  idiv      ecx
  004a29a5  02d1                  add       dl,cl
  004a29a7  885643                mov       [esi+43h],dl
  004a29aa  e983000000            jmp       004A2A32h

LAB_004a29af:
  004a29af  83fa28                cmp       edx,28h
  004a29b2  7c5b                  jl        short 004A2A0Fh
  004a29b4  83fa3c                cmp       edx,3Ch
  004a29b7  7d17                  jge       short 004A29D0h
  004a29b9  c6464206              mov       byte [esi+42h],6
  004a29bd  c6464301              mov       byte [esi+43h],1
  004a29c1  e87a470a00            call      00547140h
  004a29c6  66894610              mov       [esi+10h],ax
  004a29ca  66895e12              mov       [esi+12h],bx
  004a29ce  eb62                  jmp       short 004A2A32h

LAB_004a29d0:
  004a29d0  83fa50                cmp       edx,50h
  004a29d3  7d3a                  jge       short 004A2A0Fh
  004a29d5  c6464203              mov       byte [esi+42h],3
  004a29d9  66895e12              mov       [esi+12h],bx
  004a29dd  e85e470a00            call      00547140h
  004a29e2  99                    cdq
  004a29e3  b90a000000            mov       ecx,0Ah
  004a29e8  f7f9                  idiv      ecx
  004a29ea  8bc7                  mov       eax,edi
  004a29ec  d1f8                  sar       eax,1
  004a29ee  02d0                  add       dl,al
  004a29f0  80c214                add       dl,14h
  004a29f3  885643                mov       [esi+43h],dl
  004a29f6  e845470a00            call      00547140h
  004a29fb  99                    cdq
  004a29fc  33c2                  xor       eax,edx
  004a29fe  2bc2                  sub       eax,edx
  004a2a00  25ff0f0000            and       eax,0FFFh
  004a2a05  33c2                  xor       eax,edx
  004a2a07  2bc2                  sub       eax,edx
  004a2a09  66894610              mov       [esi+10h],ax
  004a2a0d  eb23                  jmp       short 004A2A32h

LAB_004a2a0f:
  004a2a0f  c6464202              mov       byte [esi+42h],2
  004a2a13  e828470a00            call      00547140h
  004a2a18  99                    cdq
  004a2a19  b90a000000            mov       ecx,0Ah
  004a2a1e  66c746105000          mov       word [esi+10h],50h
  004a2a24  f7f9                  idiv      ecx
  004a2a26  8bc7                  mov       eax,edi
  004a2a28  d1f8                  sar       eax,1
  004a2a2a  02d0                  add       dl,al
  004a2a2c  80c214                add       dl,14h
  004a2a2f  885643                mov       [esi+43h],dl

LAB_004a2a32:
  004a2a32  817c241458020000      cmp       dword [esp+14h],258h
  004a2a3a  0f8c0e010000          jl        near 004A2B4Eh
  004a2a40  e8fb460a00            call      00547140h
  004a2a45  99                    cdq
  004a2a46  b964000000            mov       ecx,64h
  004a2a4b  f7f9                  idiv      ecx
  004a2a4d  83fa1e                cmp       edx,1Eh
  004a2a50  7d6f                  jge       short 004A2AC1h
  004a2a52  8b442410              mov       eax,[esp+10h]
  004a2a56  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004a2a5a  3bd1                  cmp       edx,ecx
  004a2a5c  7d2e                  jge       short 004A2A8Ch
  004a2a5e  c6464202              mov       byte [esi+42h],2
  004a2a62  e8d9460a00            call      00547140h
  004a2a67  99                    cdq
  004a2a68  b90a000000            mov       ecx,0Ah
  004a2a6d  66c746106400          mov       word [esi+10h],64h
  004a2a73  f7f9                  idiv      ecx
  004a2a75  8bc7                  mov       eax,edi
  004a2a77  d1f8                  sar       eax,1
  004a2a79  02d0                  add       dl,al
  004a2a7b  80c214                add       dl,14h
  004a2a7e  885643                mov       [esi+43h],dl
  004a2a81  5f                    pop       edi
  004a2a82  5e                    pop       esi
  004a2a83  5d                    pop       ebp
  004a2a84  5b                    pop       ebx
  004a2a85  81c484000000          add       esp,84h
  004a2a8b  c3                    ret

LAB_004a2a8c:
  004a2a8c  885e42                mov       [esi+42h],bl
  004a2a8f  e8ac460a00            call      00547140h
  004a2a94  99                    cdq
  004a2a95  b90a000000            mov       ecx,0Ah
  004a2a9a  66895e12              mov       [esi+12h],bx
  004a2a9e  f7f9                  idiv      ecx
  004a2aa0  8b4c2410              mov       ecx,[esp+10h]
  004a2aa4  8bc7                  mov       eax,edi
  004a2aa6  c1f802                sar       eax,2
  004a2aa9  02d0                  add       dl,al
  004a2aab  885643                mov       [esi+43h],dl
  004a2aae  8d540950              lea       edx,[ecx+ecx+50h]
  004a2ab2  66895610              mov       [esi+10h],dx
  004a2ab6  5f                    pop       edi
  004a2ab7  5e                    pop       esi
  004a2ab8  5d                    pop       ebp
  004a2ab9  5b                    pop       ebx
  004a2aba  81c484000000          add       esp,84h
  004a2ac0  c3                    ret

LAB_004a2ac1:
  004a2ac1  83fa28                cmp       edx,28h
  004a2ac4  7d43                  jge       short 004A2B09h
  004a2ac6  c6464203              mov       byte [esi+42h],3
  004a2aca  66895e12              mov       [esi+12h],bx
  004a2ace  e86d460a00            call      00547140h
  004a2ad3  99                    cdq
  004a2ad4  b90a000000            mov       ecx,0Ah
  004a2ad9  f7f9                  idiv      ecx
  004a2adb  8bc7                  mov       eax,edi
  004a2add  d1f8                  sar       eax,1
  004a2adf  02d0                  add       dl,al
  004a2ae1  80c214                add       dl,14h
  004a2ae4  885643                mov       [esi+43h],dl
  004a2ae7  e854460a00            call      00547140h
  004a2aec  99                    cdq
  004a2aed  33c2                  xor       eax,edx
  004a2aef  2bc2                  sub       eax,edx
  004a2af1  25ff0f0000            and       eax,0FFFh
  004a2af6  33c2                  xor       eax,edx
  004a2af8  2bc2                  sub       eax,edx
  004a2afa  66894610              mov       [esi+10h],ax
  004a2afe  5f                    pop       edi
  004a2aff  5e                    pop       esi
  004a2b00  5d                    pop       ebp
  004a2b01  5b                    pop       ebx
  004a2b02  81c484000000          add       esp,84h
  004a2b08  c3                    ret

LAB_004a2b09:
  004a2b09  83fa3c                cmp       edx,3Ch
  004a2b0c  7d13                  jge       short 004A2B21h
  004a2b0e  c6464201              mov       byte [esi+42h],1
  004a2b12  c6464301              mov       byte [esi+43h],1
  004a2b16  5f                    pop       edi
  004a2b17  5e                    pop       esi
  004a2b18  5d                    pop       ebp
  004a2b19  5b                    pop       ebx
  004a2b1a  81c484000000          add       esp,84h
  004a2b20  c3                    ret

LAB_004a2b21:
  004a2b21  83fa50                cmp       edx,50h
  004a2b24  7d17                  jge       short 004A2B3Dh
  004a2b26  c6464208              mov       byte [esi+42h],8
  004a2b2a  c6464301              mov       byte [esi+43h],1
  004a2b2e  66895e12              mov       [esi+12h],bx
  004a2b32  5f                    pop       edi
  004a2b33  5e                    pop       esi
  004a2b34  5d                    pop       ebp
  004a2b35  5b                    pop       ebx
  004a2b36  81c484000000          add       esp,84h
  004a2b3c  c3                    ret

LAB_004a2b3d:
  004a2b3d  c6464206              mov       byte [esi+42h],6
  004a2b41  c6464301              mov       byte [esi+43h],1
  004a2b45  e8f6450a00            call      00547140h
  004a2b4a  66894610              mov       [esi+10h],ax

LAB_004a2b4e:
  004a2b4e  5f                    pop       edi
  004a2b4f  5e                    pop       esi
  004a2b50  5d                    pop       ebp
  004a2b51  5b                    pop       ebx
  004a2b52  81c484000000          add       esp,84h
  004a2b58  c3                    ret

LAB_004a2b59:
  004a2b59  83fd3c                cmp       ebp,3Ch
  004a2b5c  7c2e                  jl        short 004A2B8Ch
  004a2b5e  83fd46                cmp       ebp,46h
  004a2b61  7d29                  jge       short 004A2B8Ch
  004a2b63  8b4c2410              mov       ecx,[esp+10h]
  004a2b67  8b442414              mov       eax,[esp+14h]
  004a2b6b  83c164                add       ecx,64h
  004a2b6e  3bc1                  cmp       eax,ecx
  004a2b70  7e1a                  jle       short 004A2B8Ch
  004a2b72  c6464206              mov       byte [esi+42h],6
  004a2b76  c6464301              mov       byte [esi+43h],1
  004a2b7a  e8c1450a00            call      00547140h
  004a2b7f  66894610              mov       [esi+10h],ax
  004a2b83  66895e12              mov       [esi+12h],bx
  004a2b87  e9affdffff            jmp       004A293Bh

LAB_004a2b8c:
  004a2b8c  c6464202              mov       byte [esi+42h],2
  004a2b90  e8ab450a00            call      00547140h
  004a2b95  99                    cdq
  004a2b96  b90a000000            mov       ecx,0Ah
  004a2b9b  8b7c2420              mov       edi,[esp+20h]
  004a2b9f  f7f9                  idiv      ecx
  004a2ba1  8bc7                  mov       eax,edi
  004a2ba3  66c746105000          mov       word [esi+10h],50h
  004a2ba9  d1f8                  sar       eax,1
  004a2bab  02d0                  add       dl,al
  004a2bad  80c214                add       dl,14h
  004a2bb0  885643                mov       [esi+43h],dl
  004a2bb3  e987fdffff            jmp       004A293Fh
  004a2bb8  a7                    cmpsd
  004a2bb9  204a00                and       [edx],cl
  004a2bbc  f2204a00              and       [edx],cl
  004a2bc0  7e21                  jle       short 004A2BE3h
  004a2bc2  4a                    dec       edx
  004a2bc3  0020                  add       [eax],ah
  004a2bc5  224a00                and       cl,[edx]
  004a2bc8  0f264a                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
