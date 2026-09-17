Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c1c30  section .text  offset fichier 0xc1030  2880 octets

  004c1c30  81ec84000000          sub       esp,84h
  004c1c36  53                    push      ebx
  004c1c37  55                    push      ebp
  004c1c38  56                    push      esi
  004c1c39  8bb42494000000        mov       esi,[esp+94h]
  004c1c40  33db                  xor       ebx,ebx
  004c1c42  57                    push      edi
  004c1c43  8a4641                mov       al,[esi+41h]
  004c1c46  66895c2430            mov       [esp+30h],bx
  004c1c4b  3ac3                  cmp       al,bl
  004c1c4d  66c74424321e00        mov       word [esp+32h],1Eh
  004c1c54  66c74424343200        mov       word [esp+34h],32h
  004c1c5b  66c74424366400        mov       word [esp+36h],64h
  004c1c62  66895c2438            mov       [esp+38h],bx
  004c1c67  66c744243a5000        mov       word [esp+3Ah],50h
  004c1c6e  7510                  jne       short 004C1C80h
  004c1c70  53                    push      ebx
  004c1c71  6a04                  push      4
  004c1c73  56                    push      esi
  004c1c74  c6464101              mov       byte [esi+41h],1
  004c1c78  e8c3c1ffff            call      004BDE40h
  004c1c7d  83c40c                add       esp,0Ch

LAB_004c1c80:
  004c1c80  8b461c                mov       eax,[esi+1Ch]
  004c1c83  8b4e20                mov       ecx,[esi+20h]
  004c1c86  8b5624                mov       edx,[esi+24h]
  004c1c89  8d6e1c                lea       ebp,[esi+1Ch]
  004c1c8c  89442424              mov       [esp+24h],eax
  004c1c90  8b4664                mov       eax,[esi+64h]
  004c1c93  894c2428              mov       [esp+28h],ecx
  004c1c97  8b4e60                mov       ecx,[esi+60h]
  004c1c9a  8954242c              mov       [esp+2Ch],edx
  004c1c9e  668b5632              mov       dx,[esi+32h]
  004c1ca2  89442440              mov       [esp+40h],eax
  004c1ca6  8b467c                mov       eax,[esi+7Ch]
  004c1ca9  894c2448              mov       [esp+48h],ecx
  004c1cad  668b8e90000000        mov       cx,[esi+90h]
  004c1cb4  6689942482000000      mov       [esp+82h],dx
  004c1cbc  8b9680000000          mov       edx,[esi+80h]
  004c1cc2  89442454              mov       [esp+54h],eax
  004c1cc6  8b8684000000          mov       eax,[esi+84h]
  004c1ccc  66894c2458            mov       [esp+58h],cx
  004c1cd1  8b8e88000000          mov       ecx,[esi+88h]
  004c1cd7  89542444              mov       [esp+44h],edx
  004c1cdb  8b968c000000          mov       edx,[esi+8Ch]
  004c1ce1  89442450              mov       [esp+50h],eax
  004c1ce5  894c243c              mov       [esp+3Ch],ecx
  004c1ce9  8d44245c              lea       eax,[esp+5Ch]
  004c1ced  8954244c              mov       [esp+4Ch],edx
  004c1cf1  8d4c2460              lea       ecx,[esp+60h]
  004c1cf5  50                    push      eax
  004c1cf6  8d542468              lea       edx,[esp+68h]
  004c1cfa  51                    push      ecx
  004c1cfb  52                    push      edx
  004c1cfc  56                    push      esi
  004c1cfd  e87e6c0700            call      00538980h
  004c1d02  83c410                add       esp,10h
  004c1d05  8d442468              lea       eax,[esp+68h]
  004c1d09  8d4c2420              lea       ecx,[esp+20h]
  004c1d0d  8d542410              lea       edx,[esp+10h]
  004c1d11  50                    push      eax
  004c1d12  51                    push      ecx
  004c1d13  8d442424              lea       eax,[esp+24h]
  004c1d17  52                    push      edx
  004c1d18  8d4c2420              lea       ecx,[esp+20h]
  004c1d1c  50                    push      eax
  004c1d1d  51                    push      ecx
  004c1d1e  56                    push      esi
  004c1d1f  e8fc410700            call      00535F20h
  004c1d24  8b7c242c              mov       edi,[esp+2Ch]
  004c1d28  b840000000            mov       eax,40h
  004c1d2d  2bc7                  sub       eax,edi
  004c1d2f  83c418                add       esp,18h
  004c1d32  89442418              mov       [esp+18h],eax
  004c1d36  8b442420              mov       eax,[esp+20h]
  004c1d3a  3bc3                  cmp       eax,ebx
  004c1d3c  0f84ad040000          je        near 004C21EFh
  004c1d42  8b0e                  mov       ecx,[esi]
  004c1d44  33d2                  xor       edx,edx
  004c1d46  8a5604                mov       dl,[esi+4]
  004c1d49  81e1fffffffd          and       ecx,0FDFFFFFFh
  004c1d4f  0fbf5e18              movsx     ebx,word [esi+18h]
  004c1d53  890e                  mov       [esi],ecx
  004c1d55  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c1d5c  f7c100000001          test      ecx,1000000h
  004c1d62  8b149a                mov       edx,[edx+ebx*4]
  004c1d65  741d                  je        short 004C1D84h
  004c1d67  66ff461a              inc       word [esi+1Ah]
  004c1d6b  668b4e1a              mov       cx,[esi+1Ah]
  004c1d6f  33db                  xor       ebx,ebx
  004c1d71  668b5a16              mov       bx,[edx+16h]
  004c1d75  0fbfc9                movsx     ecx,cx
  004c1d78  3bcb                  cmp       ecx,ebx
  004c1d7a  7c2b                  jl        short 004C1DA7h
  004c1d7c  66c7461a0000          mov       word [esi+1Ah],0
  004c1d82  eb23                  jmp       short 004C1DA7h

LAB_004c1d84:
  004c1d84  66ff461a              inc       word [esi+1Ah]
  004c1d88  33db                  xor       ebx,ebx
  004c1d8a  668b5a16              mov       bx,[edx+16h]
  004c1d8e  0fbf561a              movsx     edx,word [esi+1Ah]
  004c1d92  3bd3                  cmp       edx,ebx
  004c1d94  7c11                  jl        short 004C1DA7h
  004c1d96  668b561a              mov       dx,[esi+1Ah]
  004c1d9a  4a                    dec       edx
  004c1d9b  81c900000002          or        ecx,2000000h
  004c1da1  6689561a              mov       [esi+1Ah],dx
  004c1da5  890e                  mov       [esi],ecx

LAB_004c1da7:
  004c1da7  8a0e                  mov       cl,[esi]
  004c1da9  33db                  xor       ebx,ebx
  004c1dab  f6c102                test      cl,2
  004c1dae  747b                  je        short 004C1E2Bh
  004c1db0  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004c1db5  7406                  je        short 004C1DBDh
  004c1db7  807e4207              cmp       byte [esi+42h],7
  004c1dbb  7555                  jne       short 004C1E12h

LAB_004c1dbd:
  004c1dbd  8d442430              lea       eax,[esp+30h]
  004c1dc1  53                    push      ebx
  004c1dc2  50                    push      eax
  004c1dc3  55                    push      ebp
  004c1dc4  56                    push      esi
  004c1dc5  e8667e0500            call      00519C30h
  004c1dca  8bf8                  mov       edi,eax
  004c1dcc  a1b8e05d00            mov       eax,[5DE0B8h]
  004c1dd1  83c410                add       esp,10h
  004c1dd4  3bc3                  cmp       eax,ebx
  004c1dd6  7432                  je        short 004C1E0Ah
  004c1dd8  8b0f                  mov       ecx,[edi]
  004c1dda  57                    push      edi
  004c1ddb  83c940                or        ecx,40h
  004c1dde  56                    push      esi
  004c1ddf  66c787960000001000    mov       word [edi+96h],10h
  004c1de8  890f                  mov       [edi],ecx
  004c1dea  e8917d0700            call      00539B80h
  004c1def  83c408                add       esp,8
  004c1df2  83c71c                add       edi,1Ch
  004c1df5  57                    push      edi
  004c1df6  6800180000            push      1800h
  004c1dfb  6800030000            push      300h
  004c1e00  6a03                  push      3
  004c1e02  e819bd0700            call      0053DB20h
  004c1e07  83c410                add       esp,10h

LAB_004c1e0a:
  004c1e0a  8b442420              mov       eax,[esp+20h]
  004c1e0e  8b7c2414              mov       edi,[esp+14h]

LAB_004c1e12:
  004c1e12  8b0e                  mov       ecx,[esi]
  004c1e14  f7c100000002          test      ecx,2000000h
  004c1e1a  740f                  je        short 004C1E2Bh
  004c1e1c  807e4207              cmp       byte [esi+42h],7
  004c1e20  7409                  je        short 004C1E2Bh
  004c1e22  83e1fd                and       ecx,0FFFFFFFDh
  004c1e25  c6464300              mov       byte [esi+43h],0
  004c1e29  890e                  mov       [esi],ecx

LAB_004c1e2b:
  004c1e2b  8b0e                  mov       ecx,[esi]
  004c1e2d  f6c102                test      cl,2
  004c1e30  740a                  je        short 004C1E3Ch
  004c1e32  807e4207              cmp       byte [esi+42h],7
  004c1e36  0f85b3030000          jne       near 004C21EFh

LAB_004c1e3c:
  004c1e3c  33d2                  xor       edx,edx
  004c1e3e  8a5642                mov       dl,[esi+42h]
  004c1e41  83fa08                cmp       edx,8
  004c1e44  0f87a5030000          ja        near 004C21EFh
  004c1e4a  ff249540274c00        jmp       dword [edx*4+4C2740h]
  004c1e51  817c241c2c010000      cmp       dword [esp+1Ch],12Ch
  004c1e59  7d09                  jge       short 004C1E64h
  004c1e5b  c6464300              mov       byte [esi+43h],0
  004c1e5f  e98b030000            jmp       004C21EFh

LAB_004c1e64:
  004c1e64  8a4643                mov       al,[esi+43h]
  004c1e67  84c0                  test      al,al
  004c1e69  7466                  je        short 004C1ED1h
  004c1e6b  fec8                  dec       al
  004c1e6d  895e5c                mov       [esi+5Ch],ebx
  004c1e70  884643                mov       [esi+43h],al
  004c1e73  e977030000            jmp       004C21EFh
  004c1e78  8b442410              mov       eax,[esp+10h]
  004c1e7c  8d90c8000000          lea       edx,[eax+0C8h]
  004c1e82  81e2ff0f0000          and       edx,0FFFh
  004c1e88  81fa90010000          cmp       edx,190h
  004c1e8e  7f0c                  jg        short 004C1E9Ch
  004c1e90  c6464300              mov       byte [esi+43h],0
  004c1e94  895e5c                mov       [esi+5Ch],ebx
  004c1e97  e953030000            jmp       004C21EFh

LAB_004c1e9c:
  004c1e9c  25ff0f0000            and       eax,0FFFh
  004c1ea1  3d00080000            cmp       eax,800h
  004c1ea6  7d08                  jge       short 004C1EB0h
  004c1ea8  8d8780000000          lea       eax,[edi+80h]
  004c1eae  eb07                  jmp       short 004C1EB7h

LAB_004c1eb0:
  004c1eb0  b880ffffff            mov       eax,0FFFFFF80h
  004c1eb5  2bc7                  sub       eax,edi

LAB_004c1eb7:
  004c1eb7  66014632              add       [esi+32h],ax
  004c1ebb  b801000000            mov       eax,1
  004c1ec0  66394618              cmp       [esi+18h],ax
  004c1ec4  740b                  je        short 004C1ED1h
  004c1ec6  81c900000001          or        ecx,1000000h
  004c1ecc  894618                mov       [esi+18h],eax
  004c1ecf  890e                  mov       [esi],ecx

LAB_004c1ed1:
  004c1ed1  895e5c                mov       [esi+5Ch],ebx
  004c1ed4  e916030000            jmp       004C21EFh
  004c1ed9  0fbf4610              movsx     eax,word [esi+10h]
  004c1edd  3944241c              cmp       [esp+1Ch],eax
  004c1ee1  7d09                  jge       short 004C1EECh
  004c1ee3  c6464300              mov       byte [esi+43h],0
  004c1ee7  e903030000            jmp       004C21EFh

LAB_004c1eec:
  004c1eec  8a4643                mov       al,[esi+43h]
  004c1eef  84c0                  test      al,al
  004c1ef1  7405                  je        short 004C1EF8h
  004c1ef3  fec8                  dec       al
  004c1ef5  884643                mov       [esi+43h],al

LAB_004c1ef8:
  004c1ef8  b801000000            mov       eax,1
  004c1efd  66394618              cmp       [esi+18h],ax
  004c1f01  740b                  je        short 004C1F0Eh
  004c1f03  81c900000001          or        ecx,1000000h
  004c1f09  894618                mov       [esi+18h],eax
  004c1f0c  890e                  mov       [esi],ecx

LAB_004c1f0e:
  004c1f0e  8b442410              mov       eax,[esp+10h]
  004c1f12  8d8800010000          lea       ecx,[eax+100h]
  004c1f18  81e1ff0f0000          and       ecx,0FFFh
  004c1f1e  81f900020000          cmp       ecx,200h
  004c1f24  7e23                  jle       short 004C1F49h
  004c1f26  25ff0f0000            and       eax,0FFFh
  004c1f2b  3d00080000            cmp       eax,800h
  004c1f30  7d09                  jge       short 004C1F3Bh
  004c1f32  8d843f80000000        lea       eax,[edi+edi+80h]
  004c1f39  eb0a                  jmp       short 004C1F45h

LAB_004c1f3b:
  004c1f3b  8d143f                lea       edx,[edi+edi]
  004c1f3e  b880ffffff            mov       eax,0FFFFFF80h
  004c1f43  2bc2                  sub       eax,edx

LAB_004c1f45:
  004c1f45  66014632              add       [esi+32h],ax

LAB_004c1f49:
  004c1f49  8d8780010000          lea       eax,[edi+180h]
  004c1f4f  8d0480                lea       eax,[eax+eax*4]
  004c1f52  c1e005                shl       eax,5
  004c1f55  89465c                mov       [esi+5Ch],eax
  004c1f58  e992020000            jmp       004C21EFh
  004c1f5d  8a5643                mov       dl,[esi+43h]
  004c1f60  84d2                  test      dl,dl
  004c1f62  7405                  je        short 004C1F69h
  004c1f64  feca                  dec       dl
  004c1f66  885643                mov       [esi+43h],dl

LAB_004c1f69:
  004c1f69  ba01000000            mov       edx,1
  004c1f6e  66395618              cmp       [esi+18h],dx
  004c1f72  740b                  je        short 004C1F7Fh
  004c1f74  81c900000001          or        ecx,1000000h
  004c1f7a  895618                mov       [esi+18h],edx
  004c1f7d  890e                  mov       [esi],ecx

LAB_004c1f7f:
  004c1f7f  8d8fa0000000          lea       ecx,[edi+0A0h]
  004c1f85  c1e108                shl       ecx,8
  004c1f88  0fafc1                imul      eax,ecx
  004c1f8b  894e5c                mov       [esi+5Ch],ecx
  004c1f8e  0fbf4e10              movsx     ecx,word [esi+10h]
  004c1f92  c1f80c                sar       eax,0Ch
  004c1f95  51                    push      ecx
  004c1f96  8bf8                  mov       edi,eax
  004c1f98  e87f3a0800            call      00545A1Ch
  004c1f9d  0fafc7                imul      eax,edi
  004c1fa0  0fbf5610              movsx     edx,word [esi+10h]
  004c1fa4  8b4d00                mov       ecx,[ebp]
  004c1fa7  52                    push      edx
  004c1fa8  c1f80c                sar       eax,0Ch
  004c1fab  03c8                  add       ecx,eax
  004c1fad  894d00                mov       [ebp],ecx
  004c1fb0  e8613a0800            call      00545A16h
  004c1fb5  0fafc7                imul      eax,edi
  004c1fb8  8b6e24                mov       ebp,[esi+24h]
  004c1fbb  895e5c                mov       [esi+5Ch],ebx
  004c1fbe  c1f80c                sar       eax,0Ch
  004c1fc1  03e8                  add       ebp,eax
  004c1fc3  8b442410              mov       eax,[esp+10h]
  004c1fc7  896e24                mov       [esi+24h],ebp
  004c1fca  8d8800010000          lea       ecx,[eax+100h]
  004c1fd0  81e1ff0f0000          and       ecx,0FFFh
  004c1fd6  81f900020000          cmp       ecx,200h
  004c1fdc  0f8e0d020000          jle       near 004C21EFh
  004c1fe2  25ff0f0000            and       eax,0FFFh
  004c1fe7  3d00080000            cmp       eax,800h
  004c1fec  7d14                  jge       short 004C2002h
  004c1fee  8b542414              mov       edx,[esp+14h]
  004c1ff2  8d04d532000000        lea       eax,[edx*8+32h]
  004c1ff9  66014632              add       [esi+32h],ax
  004c1ffd  e9ed010000            jmp       004C21EFh

LAB_004c2002:
  004c2002  8b442414              mov       eax,[esp+14h]
  004c2006  8d0cc500000000        lea       ecx,[eax*8]
  004c200d  b8ceffffff            mov       eax,0FFFFFFCEh
  004c2012  2bc1                  sub       eax,ecx
  004c2014  66014632              add       [esi+32h],ax
  004c2018  e9d2010000            jmp       004C21EFh
  004c201d  f6c101                test      cl,1
  004c2020  0f85c9010000          jne       near 004C21EFh
  004c2026  81c900000001          or        ecx,1000000h
  004c202c  c7461801000000        mov       dword [esi+18h],1
  004c2033  890e                  mov       [esi],ecx
  004c2035  8d8ff0000000          lea       ecx,[edi+0F0h]
  004c203b  c1e108                shl       ecx,8
  004c203e  0fafc1                imul      eax,ecx
  004c2041  0fbf5610              movsx     edx,word [esi+10h]
  004c2045  c1f80c                sar       eax,0Ch
  004c2048  52                    push      edx
  004c2049  894e5c                mov       [esi+5Ch],ecx
  004c204c  8bf8                  mov       edi,eax
  004c204e  e8c9390800            call      00545A1Ch
  004c2053  0fafc7                imul      eax,edi
  004c2056  8b6e50                mov       ebp,[esi+50h]
  004c2059  c1f80c                sar       eax,0Ch
  004c205c  03e8                  add       ebp,eax
  004c205e  0fbf4610              movsx     eax,word [esi+10h]
  004c2062  50                    push      eax
  004c2063  896e50                mov       [esi+50h],ebp
  004c2066  e8ab390800            call      00545A16h
  004c206b  0fafc7                imul      eax,edi
  004c206e  8b5658                mov       edx,[esi+58h]
  004c2071  8b4e54                mov       ecx,[esi+54h]
  004c2074  c1f80c                sar       eax,0Ch
  004c2077  03d0                  add       edx,eax
  004c2079  8b06                  mov       eax,[esi]
  004c207b  81c100e00100          add       ecx,1E000h
  004c2081  0c01                  or        al,1
  004c2083  895658                mov       [esi+58h],edx
  004c2086  894e54                mov       [esi+54h],ecx
  004c2089  8906                  mov       [esi],eax
  004c208b  e95f010000            jmp       004C21EFh
  004c2090  668b4612              mov       ax,[esi+12h]
  004c2094  663bc3                cmp       ax,bx
  004c2097  7503                  jne       short 004C209Ch
  004c2099  895e5c                mov       [esi+5Ch],ebx

LAB_004c209c:
  004c209c  663d0a00              cmp       ax,0Ah
  004c20a0  bb05000000            mov       ebx,5
  004c20a5  0f8d9e000000          jge       near 004C2149h
  004c20ab  66837e1a06            cmp       word [esi+1Ah],6
  004c20b0  66895e18              mov       [esi+18h],bx
  004c20b4  7c04                  jl        short 004C20BAh
  004c20b6  66895e1a              mov       [esi+1Ah],bx

LAB_004c20ba:
  004c20ba  40                    inc       eax
  004c20bb  663d0700              cmp       ax,7
  004c20bf  66894612              mov       [esi+12h],ax
  004c20c3  7505                  jne       short 004C20CAh
  004c20c5  83c902                or        ecx,2
  004c20c8  890e                  mov       [esi],ecx

LAB_004c20ca:
  004c20ca  663d0a00              cmp       ax,0Ah
  004c20ce  7541                  jne       short 004C2111h
  004c20d0  81c700010000          add       edi,100h
  004c20d6  8b5654                mov       edx,[esi+54h]
  004c20d9  55                    push      ebp
  004c20da  6800120000            push      1200h
  004c20df  8d0cbf                lea       ecx,[edi+edi*4]
  004c20e2  81c200400100          add       edx,14000h
  004c20e8  c1e108                shl       ecx,8
  004c20eb  894e5c                mov       [esi+5Ch],ecx
  004c20ee  8b0e                  mov       ecx,[esi]
  004c20f0  83c903                or        ecx,3
  004c20f3  6800060000            push      600h
  004c20f8  6a24                  push      24h
  004c20fa  66c746126400          mov       word [esi+12h],64h
  004c2100  895654                mov       [esi+54h],edx
  004c2103  890e                  mov       [esi],ecx
  004c2105  e816ba0700            call      0053DB20h
  004c210a  8b7c2424              mov       edi,[esp+24h]
  004c210e  83c410                add       esp,10h

LAB_004c2111:
  004c2111  8b442410              mov       eax,[esp+10h]
  004c2115  8d9000010000          lea       edx,[eax+100h]
  004c211b  81e2ff0f0000          and       edx,0FFFh
  004c2121  81fa00020000          cmp       edx,200h
  004c2127  7e20                  jle       short 004C2149h
  004c2129  25ff0f0000            and       eax,0FFFh
  004c212e  3d00080000            cmp       eax,800h
  004c2133  7d06                  jge       short 004C213Bh
  004c2135  8d7c3f32              lea       edi,[edi+edi+32h]
  004c2139  eb0a                  jmp       short 004C2145h

LAB_004c213b:
  004c213b  8d043f                lea       eax,[edi+edi]
  004c213e  bfceffffff            mov       edi,0FFFFFFCEh
  004c2143  2bf8                  sub       edi,eax

LAB_004c2145:
  004c2145  66017e32              add       [esi+32h],di

LAB_004c2149:
  004c2149  66837e1264            cmp       word [esi+12h],64h
  004c214e  b965000000            mov       ecx,65h
  004c2153  752e                  jne       short 004C2183h
  004c2155  8b465c                mov       eax,[esi+5Ch]
  004c2158  66895e18              mov       [esi+18h],bx
  004c215c  8bd0                  mov       edx,eax
  004c215e  c1fa02                sar       edx,2
  004c2161  2bc2                  sub       eax,edx
  004c2163  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004c2168  89465c                mov       [esi+5Ch],eax
  004c216b  7c06                  jl        short 004C2173h
  004c216d  66c7461a0a00          mov       word [esi+1Ah],0Ah

LAB_004c2173:
  004c2173  2500f0ffff            and       eax,0FFFFF000h
  004c2178  3d00a00000            cmp       eax,0A000h
  004c217d  7d04                  jge       short 004C2183h
  004c217f  66894e12              mov       [esi+12h],cx

LAB_004c2183:
  004c2183  66394e12              cmp       [esi+12h],cx
  004c2187  7566                  jne       short 004C21EFh
  004c2189  8b0e                  mov       ecx,[esi]
  004c218b  66895e18              mov       [esi+18h],bx
  004c218f  83e1fd                and       ecx,0FFFFFFFDh
  004c2192  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004c2197  890e                  mov       [esi],ecx
  004c2199  7554                  jne       short 004C21EFh
  004c219b  c6464300              mov       byte [esi+43h],0
  004c219f  eb4e                  jmp       short 004C21EFh
  004c21a1  668b5612              mov       dx,[esi+12h]
  004c21a5  bd01000000            mov       ebp,1
  004c21aa  663bd5                cmp       dx,bp
  004c21ad  751c                  jne       short 004C21CBh
  004c21af  0fbf4610              movsx     eax,word [esi+10h]
  004c21b3  3944241c              cmp       [esp+1Ch],eax
  004c21b7  7d06                  jge       short 004C21BFh
  004c21b9  c6464300              mov       byte [esi+43h],0
  004c21bd  eb0c                  jmp       short 004C21CBh

LAB_004c21bf:
  004c21bf  8a4643                mov       al,[esi+43h]
  004c21c2  84c0                  test      al,al
  004c21c4  7405                  je        short 004C21CBh
  004c21c6  fec8                  dec       al
  004c21c8  884643                mov       [esi+43h],al

LAB_004c21cb:
  004c21cb  663bd3                cmp       dx,bx
  004c21ce  751f                  jne       short 004C21EFh
  004c21d0  d1ff                  sar       edi,1
  004c21d2  81c900000001          or        ecx,1000000h
  004c21d8  81c700400000          add       edi,4000h
  004c21de  66896e18              mov       [esi+18h],bp
  004c21e2  66895e1a              mov       [esi+1Ah],bx
  004c21e6  890e                  mov       [esi],ecx
  004c21e8  66896e12              mov       [esi+12h],bp
  004c21ec  897e5c                mov       [esi+5Ch],edi

LAB_004c21ef:
  004c21ef  0fbf4e32              movsx     ecx,word [esi+32h]
  004c21f3  51                    push      ecx
  004c21f4  e823380800            call      00545A1Ch
  004c21f9  0fbf5632              movsx     edx,word [esi+32h]
  004c21fd  52                    push      edx
  004c21fe  89442470              mov       [esp+70h],eax
  004c2202  e80f380800            call      00545A16h
  004c2207  8b5650                mov       edx,[esi+50h]
  004c220a  8b4c2420              mov       ecx,[esp+20h]
  004c220e  89442474              mov       [esp+74h],eax
  004c2212  8b5e54                mov       ebx,[esi+54h]
  004c2215  0faf4e5c              imul      ecx,[esi+5Ch]
  004c2219  8bc2                  mov       eax,edx
  004c221b  8bfa                  mov       edi,edx
  004c221d  c1f808                sar       eax,8
  004c2220  2bf8                  sub       edi,eax
  004c2222  8beb                  mov       ebp,ebx
  004c2224  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004c222b  c1fa03                sar       edx,3
  004c222e  2bfa                  sub       edi,edx
  004c2230  8bd0                  mov       edx,eax
  004c2232  c1fd08                sar       ebp,8
  004c2235  c1fa08                sar       edx,8
  004c2238  2bd5                  sub       edx,ebp
  004c223a  8b6e58                mov       ebp,[esi+58h]
  004c223d  03d3                  add       edx,ebx
  004c223f  8bdd                  mov       ebx,ebp
  004c2241  2bd0                  sub       edx,eax
  004c2243  8bc5                  mov       eax,ebp
  004c2245  c1fb03                sar       ebx,3
  004c2248  c1f90c                sar       ecx,0Ch
  004c224b  2bc3                  sub       eax,ebx
  004c224d  8bd9                  mov       ebx,ecx
  004c224f  0faf5c246c            imul      ebx,[esp+6Ch]
  004c2254  0faf4c2474            imul      ecx,[esp+74h]
  004c2259  c1fd08                sar       ebp,8
  004c225c  2bc5                  sub       eax,ebp
  004c225e  8b6e20                mov       ebp,[esi+20h]
  004c2261  c1fb0c                sar       ebx,0Ch
  004c2264  c1f90c                sar       ecx,0Ch
  004c2267  897e50                mov       [esi+50h],edi
  004c226a  895654                mov       [esi+54h],edx
  004c226d  03df                  add       ebx,edi
  004c226f  8b7e1c                mov       edi,[esi+1Ch]
  004c2272  03ea                  add       ebp,edx
  004c2274  8b5624                mov       edx,[esi+24h]
  004c2277  03c8                  add       ecx,eax
  004c2279  03fb                  add       edi,ebx
  004c227b  03d1                  add       edx,ecx
  004c227d  56                    push      esi
  004c227e  894658                mov       [esi+58h],eax
  004c2281  897e1c                mov       [esi+1Ch],edi
  004c2284  896e20                mov       [esi+20h],ebp
  004c2287  895624                mov       [esi+24h],edx
  004c228a  e8b1160700            call      00533940h
  004c228f  8b06                  mov       eax,[esi]
  004c2291  8b9e90000000          mov       ebx,[esi+90h]
  004c2297  8bc8                  mov       ecx,eax
  004c2299  83c404                add       esp,4
  004c229c  80e7fc                and       bh,0FCh
  004c229f  83e101                and       ecx,1
  004c22a2  899e90000000          mov       [esi+90h],ebx
  004c22a8  8bd3                  mov       edx,ebx
  004c22aa  740a                  je        short 004C22B6h
  004c22ac  8b7e60                mov       edi,[esi+60h]
  004c22af  8b5e20                mov       ebx,[esi+20h]
  004c22b2  3bdf                  cmp       ebx,edi
  004c22b4  7e18                  jle       short 004C22CEh

LAB_004c22b6:
  004c22b6  33db                  xor       ebx,ebx
  004c22b8  3bcb                  cmp       ecx,ebx
  004c22ba  7533                  jne       short 004C22EFh
  004c22bc  8b6e20                mov       ebp,[esi+20h]
  004c22bf  8b7e60                mov       edi,[esi+60h]
  004c22c2  81ed00c00000          sub       ebp,0C000h
  004c22c8  3bef                  cmp       ebp,edi
  004c22ca  7f23                  jg        short 004C22EFh
  004c22cc  eb02                  jmp       short 004C22D0h

LAB_004c22ce:
  004c22ce  33db                  xor       ebx,ebx

LAB_004c22d0:
  004c22d0  f6c6a0                test      dh,0A0h
  004c22d3  751a                  jne       short 004C22EFh
  004c22d5  a802                  test      al,2
  004c22d7  897e20                mov       [esi+20h],edi
  004c22da  750c                  jne       short 004C22E8h
  004c22dc  3bcb                  cmp       ecx,ebx
  004c22de  7408                  je        short 004C22E8h
  004c22e0  385e43                cmp       [esi+43h],bl
  004c22e3  7403                  je        short 004C22E8h
  004c22e5  885e43                mov       [esi+43h],bl

LAB_004c22e8:
  004c22e8  24fe                  and       al,0FEh
  004c22ea  895e54                mov       [esi+54h],ebx
  004c22ed  8906                  mov       [esi],eax

LAB_004c22ef:
  004c22ef  f7c282ad0020          test      edx,2000AD82h
  004c22f5  0f840a010000          je        near 004C2405h
  004c22fb  f60601                test      byte [esi],1
  004c22fe  0f85d9000000          jne       near 004C23DDh
  004c2304  8a4e42                mov       cl,[esi+42h]
  004c2307  b008                  mov       al,8
  004c2309  3ac8                  cmp       cl,al
  004c230b  7420                  je        short 004C232Dh
  004c230d  884642                mov       [esi+42h],al
  004c2310  e82b4e0800            call      00547140h
  004c2315  99                    cdq
  004c2316  b928000000            mov       ecx,28h
  004c231b  66895e12              mov       [esi+12h],bx
  004c231f  f7f9                  idiv      ecx
  004c2321  66c746105000          mov       word [esi+10h],50h
  004c2327  80c214                add       dl,14h
  004c232a  885643                mov       [esi+43h],dl

LAB_004c232d:
  004c232d  8b16                  mov       edx,[esi]
  004c232f  8b442428              mov       eax,[esp+28h]
  004c2333  8b4c242c              mov       ecx,[esp+2Ch]
  004c2337  83e2fd                and       edx,0FFFFFFFDh
  004c233a  8916                  mov       [esi],edx
  004c233c  8b542424              mov       edx,[esp+24h]
  004c2340  894620                mov       [esi+20h],eax
  004c2343  8b442458              mov       eax,[esp+58h]
  004c2347  89561c                mov       [esi+1Ch],edx
  004c234a  8b542454              mov       edx,[esp+54h]
  004c234e  25ffff0000            and       eax,0FFFFh
  004c2353  894e24                mov       [esi+24h],ecx
  004c2356  8b4c2450              mov       ecx,[esp+50h]
  004c235a  89567c                mov       [esi+7Ch],edx
  004c235d  8b54243c              mov       edx,[esp+3Ch]
  004c2361  898690000000          mov       [esi+90h],eax
  004c2367  8b442444              mov       eax,[esp+44h]
  004c236b  898e84000000          mov       [esi+84h],ecx
  004c2371  8b4c2440              mov       ecx,[esp+40h]
  004c2375  898680000000          mov       [esi+80h],eax
  004c237b  8b44244c              mov       eax,[esp+4Ch]
  004c237f  899688000000          mov       [esi+88h],edx
  004c2385  8b542448              mov       edx,[esp+48h]
  004c2389  6882ad0020            push      2000AD82h
  004c238e  56                    push      esi
  004c238f  89868c000000          mov       [esi+8Ch],eax
  004c2395  894e64                mov       [esi+64h],ecx
  004c2398  895660                mov       [esi+60h],edx
  004c239b  e8c03a0100            call      004D5E60h
  004c23a0  83c408                add       esp,8
  004c23a3  663bc3                cmp       ax,bx
  004c23a6  7525                  jne       short 004C23CDh
  004c23a8  e8934d0800            call      00547140h
  004c23ad  99                    cdq
  004c23ae  33c2                  xor       eax,edx
  004c23b0  8b4c2414              mov       ecx,[esp+14h]
  004c23b4  2bc2                  sub       eax,edx
  004c23b6  83c140                add       ecx,40h
  004c23b9  83e001                and       eax,1
  004c23bc  33c2                  xor       eax,edx
  004c23be  2bc2                  sub       eax,edx
  004c23c0  8d4400ff              lea       eax,[eax+eax-1]
  004c23c4  0fafc8                imul      ecx,eax
  004c23c7  66014e32              add       [esi+32h],cx
  004c23cb  eb38                  jmp       short 004C2405h

LAB_004c23cd:
  004c23cd  8b542414              mov       edx,[esp+14h]
  004c23d1  83c240                add       edx,40h
  004c23d4  0fafd0                imul      edx,eax
  004c23d7  66015632              add       [esi+32h],dx
  004c23db  eb28                  jmp       short 004C2405h

LAB_004c23dd:
  004c23dd  8b442424              mov       eax,[esp+24h]
  004c23e1  8b4c242c              mov       ecx,[esp+2Ch]
  004c23e5  895e5c                mov       [esi+5Ch],ebx
  004c23e8  895e50                mov       [esi+50h],ebx
  004c23eb  895e58                mov       [esi+58h],ebx
  004c23ee  89461c                mov       [esi+1Ch],eax
  004c23f1  894e24                mov       [esi+24h],ecx
  004c23f4  885e42                mov       [esi+42h],bl
  004c23f7  c6464328              mov       byte [esi+43h],28h
  004c23fb  66895e12              mov       [esi+12h],bx
  004c23ff  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c2405:
  004c2405  6aff                  push      0FFFFFFFFh
  004c2407  6aff                  push      0FFFFFFFFh
  004c2409  6aff                  push      0FFFFFFFFh
  004c240b  56                    push      esi
  004c240c  e8ff130000            call      004C3810h
  004c2411  8b442430              mov       eax,[esp+30h]
  004c2415  83c410                add       esp,10h
  004c2418  3bc3                  cmp       eax,ebx
  004c241a  7517                  jne       short 004C2433h
  004c241c  668b942482000000      mov       dx,[esp+82h]
  004c2424  66895632              mov       [esi+32h],dx
  004c2428  5f                    pop       edi
  004c2429  5e                    pop       esi
  004c242a  5d                    pop       ebp
  004c242b  5b                    pop       ebx
  004c242c  81c484000000          add       esp,84h
  004c2432  c3                    ret

LAB_004c2433:
  004c2433  385e43                cmp       [esi+43h],bl
  004c2436  0f85f7020000          jne       near 004C2733h
  004c243c  8b466c                mov       eax,[esi+6Ch]
  004c243f  8b6e1c                mov       ebp,[esi+1Ch]
  004c2442  8b4e24                mov       ecx,[esi+24h]
  004c2445  8b781c                mov       edi,[eax+1Ch]
  004c2448  2bfd                  sub       edi,ebp
  004c244a  8b6824                mov       ebp,[eax+24h]
  004c244d  2be9                  sub       ebp,ecx
  004c244f  c1ff0c                sar       edi,0Ch
  004c2452  c1fd0c                sar       ebp,0Ch
  004c2455  8bc7                  mov       eax,edi
  004c2457  8bcd                  mov       ecx,ebp
  004c2459  0fafc7                imul      eax,edi
  004c245c  0fafcd                imul      ecx,ebp
  004c245f  03c1                  add       eax,ecx
  004c2461  50                    push      eax
  004c2462  e8a9350800            call      00545A10h
  004c2467  55                    push      ebp
  004c2468  57                    push      edi
  004c2469  89442424              mov       [esp+24h],eax
  004c246d  e8b0350800            call      00545A22h
  004c2472  668b5632              mov       dx,[esi+32h]
  004c2476  2bc2                  sub       eax,edx
  004c2478  25ff0f0000            and       eax,0FFFh
  004c247d  89442410              mov       [esp+10h],eax
  004c2481  8b44241c              mov       eax,[esp+1Ch]
  004c2485  3dfa000000            cmp       eax,0FAh
  004c248a  0f8d22010000          jge       near 004C25B2h
  004c2490  e8ab4c0800            call      00547140h
  004c2495  99                    cdq
  004c2496  b964000000            mov       ecx,64h
  004c249b  f7f9                  idiv      ecx
  004c249d  83fa32                cmp       edx,32h
  004c24a0  0f8da3000000          jge       near 004C2549h
  004c24a6  8b7c2418              mov       edi,[esp+18h]
  004c24aa  8b542410              mov       edx,[esp+10h]
  004c24ae  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004c24b5  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004c24bc  25ff0f0000            and       eax,0FFFh
  004c24c1  3bc1                  cmp       eax,ecx
  004c24c3  7f3d                  jg        short 004C2502h
  004c24c5  e8764c0800            call      00547140h
  004c24ca  99                    cdq
  004c24cb  8d4c3f01              lea       ecx,[edi+edi+1]
  004c24cf  f7f9                  idiv      ecx
  004c24d1  8b44241c              mov       eax,[esp+1Ch]
  004c24d5  83c264                add       edx,64h
  004c24d8  3bc2                  cmp       eax,edx
  004c24da  7d26                  jge       short 004C2502h
  004c24dc  8b16                  mov       edx,[esi]
  004c24de  c7461803000000        mov       dword [esi+18h],3
  004c24e5  81e2fffffffe          and       edx,0FEFFFFFFh
  004c24eb  c6464301              mov       byte [esi+43h],1
  004c24ef  83ca02                or        edx,2
  004c24f2  895e5c                mov       [esi+5Ch],ebx
  004c24f5  8916                  mov       [esi],edx
  004c24f7  5f                    pop       edi
  004c24f8  5e                    pop       esi
  004c24f9  5d                    pop       ebp
  004c24fa  5b                    pop       ebx
  004c24fb  81c484000000          add       esp,84h
  004c2501  c3                    ret

LAB_004c2502:
  004c2502  8b442410              mov       eax,[esp+10h]
  004c2506  8d14fd00040000        lea       edx,[edi*8+400h]
  004c250d  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  004c2514  81e1ff0f0000          and       ecx,0FFFh
  004c251a  3bca                  cmp       ecx,edx
  004c251c  0f8fb5010000          jg        near 004C26D7h
  004c2522  8b06                  mov       eax,[esi]
  004c2524  c6464207              mov       byte [esi+42h],7
  004c2528  25fffffffe            and       eax,0FEFFFFFFh
  004c252d  c6464301              mov       byte [esi+43h],1
  004c2531  8906                  mov       [esi],eax
  004c2533  c7461803000000        mov       dword [esi+18h],3
  004c253a  66895e12              mov       [esi+12h],bx
  004c253e  5f                    pop       edi
  004c253f  5e                    pop       esi
  004c2540  5d                    pop       ebp
  004c2541  5b                    pop       ebx
  004c2542  81c484000000          add       esp,84h
  004c2548  c3                    ret

LAB_004c2549:
  004c2549  83fa46                cmp       edx,46h
  004c254c  0f8c0b010000          jl        near 004C265Dh
  004c2552  83fa50                cmp       edx,50h
  004c2555  0f8dc7010000          jge       near 004C2722h
  004c255b  c6464203              mov       byte [esi+42h],3
  004c255f  e8dc4b0800            call      00547140h
  004c2564  99                    cdq
  004c2565  b90a000000            mov       ecx,0Ah
  004c256a  f7f9                  idiv      ecx
  004c256c  8b442418              mov       eax,[esp+18h]
  004c2570  8b4c2414              mov       ecx,[esp+14h]
  004c2574  d1f8                  sar       eax,1
  004c2576  8db980000000          lea       edi,[ecx+80h]
  004c257c  c1e704                shl       edi,4
  004c257f  02d0                  add       dl,al
  004c2581  80c214                add       dl,14h
  004c2584  885643                mov       [esi+43h],dl
  004c2587  e8b44b0800            call      00547140h
  004c258c  99                    cdq
  004c258d  f7ff                  idiv      edi
  004c258f  d1ff                  sar       edi,1
  004c2591  2bd7                  sub       edx,edi
  004c2593  8b7c2410              mov       edi,[esp+10h]
  004c2597  03d7                  add       edx,edi
  004c2599  66035632              add       dx,[esi+32h]
  004c259d  81c200080000          add       edx,800h
  004c25a3  66895610              mov       [esi+10h],dx
  004c25a7  5f                    pop       edi
  004c25a8  5e                    pop       esi
  004c25a9  5d                    pop       ebp
  004c25aa  5b                    pop       ebx
  004c25ab  81c484000000          add       esp,84h
  004c25b1  c3                    ret

LAB_004c25b2:
  004c25b2  3d58020000            cmp       eax,258h
  004c25b7  0f8dd0000000          jge       near 004C268Dh
  004c25bd  e87e4b0800            call      00547140h
  004c25c2  99                    cdq
  004c25c3  b964000000            mov       ecx,64h
  004c25c8  f7f9                  idiv      ecx
  004c25ca  83fa1e                cmp       edx,1Eh
  004c25cd  7d2f                  jge       short 004C25FEh
  004c25cf  8b542410              mov       edx,[esp+10h]
  004c25d3  81c200040000          add       edx,400h
  004c25d9  81e2ff0f0000          and       edx,0FFFh
  004c25df  81fa00080000          cmp       edx,800h
  004c25e5  0f8cb4000000          jl        near 004C269Fh
  004c25eb  c6464201              mov       byte [esi+42h],1
  004c25ef  c6464301              mov       byte [esi+43h],1
  004c25f3  5f                    pop       edi
  004c25f4  5e                    pop       esi
  004c25f5  5d                    pop       ebp
  004c25f6  5b                    pop       ebx
  004c25f7  81c484000000          add       esp,84h
  004c25fd  c3                    ret

LAB_004c25fe:
  004c25fe  83fa32                cmp       edx,32h
  004c2601  7d51                  jge       short 004C2654h
  004c2603  c6464203              mov       byte [esi+42h],3
  004c2607  e8344b0800            call      00547140h
  004c260c  99                    cdq
  004c260d  b90a000000            mov       ecx,0Ah
  004c2612  f7f9                  idiv      ecx
  004c2614  8b442418              mov       eax,[esp+18h]
  004c2618  8b4c2414              mov       ecx,[esp+14h]
  004c261c  d1f8                  sar       eax,1
  004c261e  8db980000000          lea       edi,[ecx+80h]
  004c2624  c1e704                shl       edi,4
  004c2627  02d0                  add       dl,al
  004c2629  80c214                add       dl,14h
  004c262c  885643                mov       [esi+43h],dl
  004c262f  e80c4b0800            call      00547140h
  004c2634  99                    cdq
  004c2635  f7ff                  idiv      edi
  004c2637  8b4c2410              mov       ecx,[esp+10h]
  004c263b  d1ff                  sar       edi,1
  004c263d  2bd7                  sub       edx,edi
  004c263f  03d1                  add       edx,ecx
  004c2641  66035632              add       dx,[esi+32h]
  004c2645  66895610              mov       [esi+10h],dx
  004c2649  5f                    pop       edi
  004c264a  5e                    pop       esi
  004c264b  5d                    pop       ebp
  004c264c  5b                    pop       ebx
  004c264d  81c484000000          add       esp,84h
  004c2653  c3                    ret

LAB_004c2654:
  004c2654  83fa5a                cmp       edx,5Ah
  004c2657  0f8dc5000000          jge       near 004C2722h

LAB_004c265d:
  004c265d  c6464202              mov       byte [esi+42h],2
  004c2661  e8da4a0800            call      00547140h
  004c2666  99                    cdq
  004c2667  b90a000000            mov       ecx,0Ah
  004c266c  66c746106400          mov       word [esi+10h],64h
  004c2672  f7f9                  idiv      ecx
  004c2674  8b442418              mov       eax,[esp+18h]
  004c2678  d1f8                  sar       eax,1
  004c267a  02d0                  add       dl,al
  004c267c  80c214                add       dl,14h
  004c267f  885643                mov       [esi+43h],dl
  004c2682  5f                    pop       edi
  004c2683  5e                    pop       esi
  004c2684  5d                    pop       ebp
  004c2685  5b                    pop       ebx
  004c2686  81c484000000          add       esp,84h
  004c268c  c3                    ret

LAB_004c268d:
  004c268d  e8ae4a0800            call      00547140h
  004c2692  99                    cdq
  004c2693  b964000000            mov       ecx,64h
  004c2698  f7f9                  idiv      ecx
  004c269a  83fa0a                cmp       edx,0Ah
  004c269d  7d33                  jge       short 004C26D2h

LAB_004c269f:
  004c269f  885e42                mov       [esi+42h],bl
  004c26a2  e8994a0800            call      00547140h
  004c26a7  99                    cdq
  004c26a8  b90a000000            mov       ecx,0Ah
  004c26ad  895e18                mov       [esi+18h],ebx
  004c26b0  f7f9                  idiv      ecx
  004c26b2  8b442418              mov       eax,[esp+18h]
  004c26b6  c1f802                sar       eax,2
  004c26b9  02d0                  add       dl,al
  004c26bb  8b06                  mov       eax,[esi]
  004c26bd  0d00000001            or        eax,1000000h
  004c26c2  885643                mov       [esi+43h],dl
  004c26c5  8906                  mov       [esi],eax
  004c26c7  5f                    pop       edi
  004c26c8  5e                    pop       esi
  004c26c9  5d                    pop       ebp
  004c26ca  5b                    pop       ebx
  004c26cb  81c484000000          add       esp,84h
  004c26d1  c3                    ret

LAB_004c26d2:
  004c26d2  83fa32                cmp       edx,32h
  004c26d5  7d13                  jge       short 004C26EAh

LAB_004c26d7:
  004c26d7  c6464201              mov       byte [esi+42h],1
  004c26db  c6464301              mov       byte [esi+43h],1
  004c26df  5f                    pop       edi
  004c26e0  5e                    pop       esi
  004c26e1  5d                    pop       ebp
  004c26e2  5b                    pop       ebx
  004c26e3  81c484000000          add       esp,84h
  004c26e9  c3                    ret

LAB_004c26ea:
  004c26ea  83fa5a                cmp       edx,5Ah
  004c26ed  7d33                  jge       short 004C2722h
  004c26ef  c6464203              mov       byte [esi+42h],3
  004c26f3  e8484a0800            call      00547140h
  004c26f8  99                    cdq
  004c26f9  b90a000000            mov       ecx,0Ah
  004c26fe  f7f9                  idiv      ecx
  004c2700  8b442418              mov       eax,[esp+18h]
  004c2704  d1f8                  sar       eax,1
  004c2706  02d0                  add       dl,al
  004c2708  80c214                add       dl,14h
  004c270b  885643                mov       [esi+43h],dl
  004c270e  e82d4a0800            call      00547140h
  004c2713  66894610              mov       [esi+10h],ax
  004c2717  5f                    pop       edi
  004c2718  5e                    pop       esi
  004c2719  5d                    pop       ebp
  004c271a  5b                    pop       ebx
  004c271b  81c484000000          add       esp,84h
  004c2721  c3                    ret

LAB_004c2722:
  004c2722  c6464206              mov       byte [esi+42h],6
  004c2726  c6464301              mov       byte [esi+43h],1
  004c272a  e8114a0800            call      00547140h
  004c272f  66894610              mov       [esi+10h],ax

LAB_004c2733:
  004c2733  5f                    pop       edi
  004c2734  5e                    pop       esi
  004c2735  5d                    pop       ebp
  004c2736  5b                    pop       ebx
  004c2737  81c484000000          add       esp,84h
  004c273d  c3                    ret
  004c273e  8bff                  mov       edi,edi
  004c2740  51                    push      ecx
  004c2741  1e                    push      ds
  004c2742  4c                    dec       esp
  004c2743  00781e                add       [eax+1Eh],bh
  004c2746  4c                    dec       esp
  004c2747  00d9                  add       cl,bl
  004c2749  1e                    push      ds
  004c274a  4c                    dec       esp
  004c274b  005d1f                add       [ebp+1Fh],bl
  004c274e  4c                    dec       esp
  004c274f  00ef                  add       bh,ch
  004c2751  214c00ef              and       [eax+eax-11h],ecx
  004c2755  214c001d              and       [eax+eax+1Dh],ecx
  004c2759  204c0090              and       [eax+eax-70h],cl
  004c275d  204c00a1              and       [eax+eax-5Fh],cl
  004c2761  214c0090              and       [eax+eax-70h],ecx
  004c2765  90                    nop
  004c2766  90                    nop
  004c2767  90                    nop
  004c2768  90                    nop
  004c2769  90                    nop
  004c276a  90                    nop
  004c276b  90                    nop
  004c276c  90                    nop
  004c276d  90                    nop
  004c276e  90                    nop
  004c276f  90                    nop

57 cible(s) de saut interne : 0x004c1c80, 0x004c1d84, 0x004c1da7, 0x004c1dbd, 0x004c1e0a, 0x004c1e12, 0x004c1e2b, 0x004c1e3c, 0x004c1e64, 0x004c1e9c, 0x004c1eb0, 0x004c1eb7, 0x004c1ed1, 0x004c1eec, 0x004c1ef8, 0x004c1f0e, 0x004c1f3b, 0x004c1f45, 0x004c1f49, 0x004c1f69, 0x004c1f7f, 0x004c2002, 0x004c209c, 0x004c20ba, 0x004c20ca, 0x004c2111, 0x004c213b, 0x004c2145, 0x004c2149, 0x004c2173, 0x004c2183, 0x004c21bf, 0x004c21cb, 0x004c21ef, 0x004c22b6, 0x004c22ce, 0x004c22d0, 0x004c22e8, 0x004c22ef, 0x004c232d, 0x004c23cd, 0x004c23dd, 0x004c2405, 0x004c2433, 0x004c2502, 0x004c2549, 0x004c25b2, 0x004c25fe, 0x004c2654, 0x004c265d, 0x004c268d, 0x004c269f, 0x004c26d2, 0x004c26d7, 0x004c26ea, 0x004c2722, 0x004c2733
