C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469bc0  section .text  offset fichier 0x68fc0  928 octets

  00469bc0  83ec1c                sub       esp,1Ch
  00469bc3  53                    push      ebx
  00469bc4  55                    push      ebp
  00469bc5  56                    push      esi
  00469bc6  8b74242c              mov       esi,[esp+2Ch]
  00469bca  33ed                  xor       ebp,ebp
  00469bcc  57                    push      edi
  00469bcd  8b467c                mov       eax,[esi+7Ch]
  00469bd0  8b5660                mov       edx,[esi+60h]
  00469bd3  89442410              mov       [esp+10h],eax
  00469bd7  8b4664                mov       eax,[esi+64h]
  00469bda  89542414              mov       [esp+14h],edx
  00469bde  8b561c                mov       edx,[esi+1Ch]
  00469be1  668b8e90000000        mov       cx,[esi+90h]
  00469be8  89442418              mov       [esp+18h],eax
  00469bec  8b4620                mov       eax,[esi+20h]
  00469bef  895644                mov       [esi+44h],edx
  00469bf2  8b5624                mov       edx,[esi+24h]
  00469bf5  66396e10              cmp       [esi+10h],bp
  00469bf9  894648                mov       [esi+48h],eax
  00469bfc  8b8680000000          mov       eax,[esi+80h]
  00469c02  89564c                mov       [esi+4Ch],edx
  00469c05  8b9684000000          mov       edx,[esi+84h]
  00469c0b  8944241c              mov       [esp+1Ch],eax
  00469c0f  8b8688000000          mov       eax,[esi+88h]
  00469c15  89542420              mov       [esp+20h],edx
  00469c19  8b968c000000          mov       edx,[esi+8Ch]
  00469c1f  894c2430              mov       [esp+30h],ecx
  00469c23  89442424              mov       [esp+24h],eax
  00469c27  89542428              mov       [esp+28h],edx
  00469c2b  7556                  jne       short 00469C83h
  00469c2d  66c746100100          mov       word [esi+10h],1
  00469c33  c7465c1e000000        mov       dword [esi+5Ch],1Eh
  00469c3a  6689ae9a000000        mov       [esi+9Ah],bp
  00469c41  892d1c185700          mov       [57181Ch],ebp
  00469c47  892d20185700          mov       [571820h],ebp
  00469c4d  8b4620                mov       eax,[esi+20h]
  00469c50  8bbe9c000000          mov       edi,[esi+9Ch]
  00469c56  8b9638010000          mov       edx,[esi+138h]
  00469c5c  c1f80c                sar       eax,0Ch
  00469c5f  66894612              mov       [esi+12h],ax
  00469c63  b800600000            mov       eax,6000h
  00469c68  0bf8                  or        edi,eax
  00469c6a  0bd0                  or        edx,eax
  00469c6c  66896e14              mov       [esi+14h],bp
  00469c70  89be9c000000          mov       [esi+9Ch],edi
  00469c76  899638010000          mov       [esi+138h],edx
  00469c7c  6689ae98000000        mov       [esi+98h],bp

LAB_00469c83:
  00469c83  66837e1001            cmp       word [esi+10h],1
  00469c88  0f85fc010000          jne       near 00469E8Ah
  00469c8e  66ff869a000000        inc       word [esi+9Ah]
  00469c95  55                    push      ebp
  00469c96  55                    push      ebp
  00469c97  56                    push      esi
  00469c98  e803090000            call      0046A5A0h
  00469c9d  83c40c                add       esp,0Ch
  00469ca0  66ff8698000000        inc       word [esi+98h]
  00469ca7  e894d40d00            call      00547140h
  00469cac  99                    cdq
  00469cad  b90f000000            mov       ecx,0Fh
  00469cb2  f7f9                  idiv      ecx
  00469cb4  b828000000            mov       eax,28h
  00469cb9  2bc2                  sub       eax,edx
  00469cbb  0fbf9698000000        movsx     edx,word [esi+98h]
  00469cc2  3bd0                  cmp       edx,eax
  00469cc4  7e3f                  jle       short 00469D05h
  00469cc6  55                    push      ebp
  00469cc7  55                    push      ebp
  00469cc8  6800120000            push      1200h
  00469ccd  6689ae98000000        mov       [esi+98h],bp
  00469cd4  e867d40d00            call      00547140h
  00469cd9  99                    cdq
  00469cda  b964000000            mov       ecx,64h
  00469cdf  f7f9                  idiv      ecx
  00469ce1  81c2ce020000          add       edx,2CEh
  00469ce7  52                    push      edx
  00469ce8  8b15cc975e00          mov       edx,[5E97CCh]
  00469cee  8b82a8000000          mov       eax,[edx+0A8h]
  00469cf4  8b08                  mov       ecx,[eax]
  00469cf6  660fb65114            movzx     dx,byte [ecx+14h]
  00469cfb  52                    push      edx
  00469cfc  56                    push      esi
  00469cfd  e87ee3ffff            call      00468080h
  00469d02  83c418                add       esp,18h

LAB_00469d05:
  00469d05  0fbf5e12              movsx     ebx,word [esi+12h]
  00469d09  8b7e20                mov       edi,[esi+20h]
  00469d0c  c1ff0c                sar       edi,0Ch
  00469d0f  3bdf                  cmp       ebx,edi
  00469d11  743a                  je        short 00469D4Dh
  00469d13  668b869a000000        mov       ax,[esi+9Ah]
  00469d1a  663d1e00              cmp       ax,1Eh
  00469d1e  7e06                  jle       short 00469D26h
  00469d20  663d4b00              cmp       ax,4Bh
  00469d24  7c0c                  jl        short 00469D32h

LAB_00469d26:
  00469d26  663d6400              cmp       ax,64h
  00469d2a  7e0d                  jle       short 00469D39h
  00469d2c  663d8500              cmp       ax,85h
  00469d30  7d07                  jge       short 00469D39h

LAB_00469d32:
  00469d32  6800020000            push      200h
  00469d37  eb05                  jmp       short 00469D3Eh

LAB_00469d39:
  00469d39  6800010000            push      100h

LAB_00469d3e:
  00469d3e  6a01                  push      1
  00469d40  56                    push      esi
  00469d41  e85a080000            call      0046A5A0h
  00469d46  83c40c                add       esp,0Ch
  00469d49  3bdf                  cmp       ebx,edi
  00469d4b  750c                  jne       short 00469D59h

LAB_00469d4d:
  00469d4d  55                    push      ebp
  00469d4e  6a01                  push      1
  00469d50  56                    push      esi
  00469d51  e84a080000            call      0046A5A0h
  00469d56  83c40c                add       esp,0Ch

LAB_00469d59:
  00469d59  8b4620                mov       eax,[esi+20h]
  00469d5c  56                    push      esi
  00469d5d  8bc8                  mov       ecx,eax
  00469d5f  0500800c00            add       eax,0C8000h
  00469d64  c1f90c                sar       ecx,0Ch
  00469d67  66894e12              mov       [esi+12h],cx
  00469d6b  894620                mov       [esi+20h],eax
  00469d6e  e8cd9b0c00            call      00533940h
  00469d73  8b5664                mov       edx,[esi+64h]
  00469d76  83c404                add       esp,4
  00469d79  6681be9a000000d200    cmp       word [esi+9Ah],0D2h
  00469d82  895620                mov       [esi+20h],edx
  00469d85  7e4f                  jle       short 00469DD6h
  00469d87  66ff4614              inc       word [esi+14h]
  00469d8b  66837e1401            cmp       word [esi+14h],1
  00469d90  7e68                  jle       short 00469DFAh
  00469d92  6a01                  push      1
  00469d94  56                    push      esi
  00469d95  66896e14              mov       [esi+14h],bp
  00469d99  e892080000            call      0046A630h
  00469d9e  83c408                add       esp,8
  00469da1  6a02                  push      2
  00469da3  56                    push      esi
  00469da4  e887080000            call      0046A630h
  00469da9  83c408                add       esp,8
  00469dac  6a03                  push      3
  00469dae  56                    push      esi
  00469daf  e87c080000            call      0046A630h
  00469db4  83c408                add       esp,8
  00469db7  6a04                  push      4
  00469db9  56                    push      esi
  00469dba  e871080000            call      0046A630h
  00469dbf  8b465c                mov       eax,[esi+5Ch]
  00469dc2  83c408                add       esp,8
  00469dc5  48                    dec       eax
  00469dc6  89465c                mov       [esi+5Ch],eax
  00469dc9  792f                  jns       short 00469DFAh
  00469dcb  896e5c                mov       [esi+5Ch],ebp
  00469dce  66c746100200          mov       word [esi+10h],2
  00469dd4  eb24                  jmp       short 00469DFAh

LAB_00469dd6:
  00469dd6  66ff4614              inc       word [esi+14h]
  00469dda  66837e140a            cmp       word [esi+14h],0Ah
  00469ddf  7e19                  jle       short 00469DFAh
  00469de1  8b565c                mov       edx,[esi+5Ch]
  00469de4  66896e14              mov       [esi+14h],bp
  00469de8  4a                    dec       edx
  00469de9  8bc2                  mov       eax,edx
  00469deb  89565c                mov       [esi+5Ch],edx
  00469dee  83f808                cmp       eax,8
  00469df1  7d07                  jge       short 00469DFAh
  00469df3  c7465c08000000        mov       dword [esi+5Ch],8

LAB_00469dfa:
  00469dfa  0fbf4632              movsx     eax,word [esi+32h]
  00469dfe  50                    push      eax
  00469dff  e818bc0d00            call      00545A1Ch
  00469e04  0faf465c              imul      eax,[esi+5Ch]
  00469e08  8b7e24                mov       edi,[esi+24h]
  00469e0b  0fbf4e32              movsx     ecx,word [esi+32h]
  00469e0f  03f8                  add       edi,eax
  00469e11  51                    push      ecx
  00469e12  897e24                mov       [esi+24h],edi
  00469e15  8b7e5c                mov       edi,[esi+5Ch]
  00469e18  e8f9bb0d00            call      00545A16h
  00469e1d  0fafc7                imul      eax,edi
  00469e20  8b4e1c                mov       ecx,[esi+1Ch]
  00469e23  03c8                  add       ecx,eax
  00469e25  f7c7fcffffff          test      edi,0FFFFFFFCh
  00469e2b  894e1c                mov       [esi+1Ch],ecx
  00469e2e  7507                  jne       short 00469E37h
  00469e30  bf01000000            mov       edi,1
  00469e35  eb03                  jmp       short 00469E3Ah

LAB_00469e37:
  00469e37  c1ff02                sar       edi,2

LAB_00469e3a:
  00469e3a  55                    push      ebp
  00469e3b  55                    push      ebp
  00469e3c  68e7030000            push      3E7h
  00469e41  e8fad20d00            call      00547140h
  00469e46  99                    cdq
  00469e47  f7ff                  idiv      edi
  00469e49  52                    push      edx
  00469e4a  68e7030000            push      3E7h
  00469e4f  56                    push      esi
  00469e50  e80b20fbff            call      0041BE60h
  00469e55  83c418                add       esp,18h
  00469e58  e8e3d20d00            call      00547140h
  00469e5d  99                    cdq
  00469e5e  b91e000000            mov       ecx,1Eh
  00469e63  f7f9                  idiv      ecx
  00469e65  83fa01                cmp       edx,1
  00469e68  751c                  jne       short 00469E86h
  00469e6a  e8d1d20d00            call      00547140h
  00469e6f  99                    cdq
  00469e70  33c2                  xor       eax,edx
  00469e72  2bc2                  sub       eax,edx
  00469e74  83e003                and       eax,3
  00469e77  33c2                  xor       eax,edx
  00469e79  2bc2                  sub       eax,edx
  00469e7b  40                    inc       eax
  00469e7c  50                    push      eax
  00469e7d  56                    push      esi
  00469e7e  e8ad070000            call      0046A630h
  00469e83  83c408                add       esp,8

LAB_00469e86:
  00469e86  8b4c2430              mov       ecx,[esp+30h]

LAB_00469e8a:
  00469e8a  66837e1002            cmp       word [esi+10h],2
  00469e8f  755b                  jne       short 00469EECh
  00469e91  8b15b4a75f00          mov       edx,[5FA7B4h]
  00469e97  80e6df                and       dh,0DFh
  00469e9a  8915b4a75f00          mov       [5FA7B4h],edx
  00469ea0  8b06                  mov       eax,[esi]
  00469ea2  8bae9c000000          mov       ebp,[esi+9Ch]
  00469ea8  8b9e38010000          mov       ebx,[esi+138h]
  00469eae  6683866c01000046      add       word [esi+16Ch],46h
  00469eb6  80cc60                or        ah,60h
  00469eb9  8906                  mov       [esi],eax
  00469ebb  b8ff9fffff            mov       eax,0FFFF9FFFh
  00469ec0  23e8                  and       ebp,eax
  00469ec2  23d8                  and       ebx,eax
  00469ec4  668b866c010000        mov       ax,[esi+16Ch]
  00469ecb  89ae9c000000          mov       [esi+9Ch],ebp
  00469ed1  663d1405              cmp       ax,514h
  00469ed5  899e38010000          mov       [esi+138h],ebx
  00469edb  7c0f                  jl        short 00469EECh
  00469edd  66c7866c0100001405    mov       word [esi+16Ch],514h
  00469ee6  66c746106300          mov       word [esi+10h],63h

LAB_00469eec:
  00469eec  8b8690000000          mov       eax,[esi+90h]
  00469ef2  f6c408                test      ah,8
  00469ef5  7455                  je        short 00469F4Ch
  00469ef7  8b542410              mov       edx,[esp+10h]
  00469efb  8b442414              mov       eax,[esp+14h]
  00469eff  81e1ffff0000          and       ecx,0FFFFh
  00469f05  89567c                mov       [esi+7Ch],edx
  00469f08  8b5644                mov       edx,[esi+44h]
  00469f0b  898e90000000          mov       [esi+90h],ecx
  00469f11  8b4c2418              mov       ecx,[esp+18h]
  00469f15  894660                mov       [esi+60h],eax
  00469f18  8b464c                mov       eax,[esi+4Ch]
  00469f1b  894e64                mov       [esi+64h],ecx
  00469f1e  8b4c241c              mov       ecx,[esp+1Ch]
  00469f22  89561c                mov       [esi+1Ch],edx
  00469f25  8b542420              mov       edx,[esp+20h]
  00469f29  894624                mov       [esi+24h],eax
  00469f2c  8b442424              mov       eax,[esp+24h]
  00469f30  898e80000000          mov       [esi+80h],ecx
  00469f36  8b4c2428              mov       ecx,[esp+28h]
  00469f3a  899684000000          mov       [esi+84h],edx
  00469f40  898688000000          mov       [esi+88h],eax
  00469f46  898e8c000000          mov       [esi+8Ch],ecx

LAB_00469f4c:
  00469f4c  5f                    pop       edi
  00469f4d  5e                    pop       esi
  00469f4e  5d                    pop       ebp
  00469f4f  5b                    pop       ebx
  00469f50  83c41c                add       esp,1Ch
  00469f53  c3                    ret
  00469f54  90                    nop
  00469f55  90                    nop
  00469f56  90                    nop
  00469f57  90                    nop
  00469f58  90                    nop
  00469f59  90                    nop
  00469f5a  90                    nop
  00469f5b  90                    nop
  00469f5c  90                    nop
  00469f5d  90                    nop
  00469f5e  90                    nop
  00469f5f  90                    nop

16 cible(s) de saut interne : 0x00469c83, 0x00469d05, 0x00469d26, 0x00469d32, 0x00469d39, 0x00469d3e, 0x00469d4d, 0x00469d59, 0x00469dd6, 0x00469dfa, 0x00469e37, 0x00469e3a, 0x00469e86, 0x00469e8a, 0x00469eec, 0x00469f4c
