Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00489ca0  section .text  offset fichier 0x890a0  4320 octets

  00489ca0  81ec88000000          sub       esp,88h
  00489ca6  53                    push      ebx
  00489ca7  55                    push      ebp
  00489ca8  56                    push      esi
  00489ca9  8bb42498000000        mov       esi,[esp+98h]
  00489cb0  b850000000            mov       eax,50h
  00489cb5  33ed                  xor       ebp,ebp
  00489cb7  668944242c            mov       [esp+2Ch],ax
  00489cbc  6689442432            mov       [esp+32h],ax
  00489cc1  8a4641                mov       al,[esi+41h]
  00489cc4  57                    push      edi
  00489cc5  84c0                  test      al,al
  00489cc7  66896c242c            mov       [esp+2Ch],bp
  00489ccc  66c744242e1400        mov       word [esp+2Eh],14h
  00489cd3  66c74424326400        mov       word [esp+32h],64h
  00489cda  66896c2434            mov       [esp+34h],bp
  00489cdf  7533                  jne       short 00489D14h
  00489ce1  55                    push      ebp
  00489ce2  6a03                  push      3
  00489ce4  56                    push      esi
  00489ce5  c6464101              mov       byte [esi+41h],1
  00489ce9  e8f2fbffff            call      004898E0h
  00489cee  8b4620                mov       eax,[esi+20h]
  00489cf1  83c40c                add       esp,0Ch
  00489cf4  894648                mov       [esi+48h],eax
  00489cf7  668b4616              mov       ax,[esi+16h]
  00489cfb  663d0a00              cmp       ax,0Ah
  00489cff  7c0d                  jl        short 00489D0Eh
  00489d01  83c0f6                add       eax,0FFFFFFF6h
  00489d04  66894616              mov       [esi+16h],ax
  00489d08  8b06                  mov       eax,[esi]
  00489d0a  24df                  and       al,0DFh
  00489d0c  eb04                  jmp       short 00489D12h

LAB_00489d0e:
  00489d0e  8b06                  mov       eax,[esi]
  00489d10  0c20                  or        al,20h

LAB_00489d12:
  00489d12  8906                  mov       [esi],eax

LAB_00489d14:
  00489d14  8b16                  mov       edx,[esi]
  00489d16  8b4e1c                mov       ecx,[esi+1Ch]
  00489d19  8b4664                mov       eax,[esi+64h]
  00489d1c  8d7e1c                lea       edi,[esi+1Ch]
  00489d1f  83e2fe                and       edx,0FFFFFFFEh
  00489d22  894c245c              mov       [esp+5Ch],ecx
  00489d26  8b4e60                mov       ecx,[esi+60h]
  00489d29  8916                  mov       [esi],edx
  00489d2b  8b5624                mov       edx,[esi+24h]
  00489d2e  8944243c              mov       [esp+3Ch],eax
  00489d32  8b467c                mov       eax,[esi+7Ch]
  00489d35  89542464              mov       [esp+64h],edx
  00489d39  668b5632              mov       dx,[esi+32h]
  00489d3d  894c2440              mov       [esp+40h],ecx
  00489d41  668b8e90000000        mov       cx,[esi+90h]
  00489d48  6689942486000000      mov       [esp+86h],dx
  00489d50  8b9680000000          mov       edx,[esi+80h]
  00489d56  8b5e6c                mov       ebx,[esi+6Ch]
  00489d59  89442458              mov       [esp+58h],eax
  00489d5d  8b8684000000          mov       eax,[esi+84h]
  00489d63  66894c2448            mov       [esp+48h],cx
  00489d68  8b8e88000000          mov       ecx,[esi+88h]
  00489d6e  89542438              mov       [esp+38h],edx
  00489d72  8b968c000000          mov       edx,[esi+8Ch]
  00489d78  8944244c              mov       [esp+4Ch],eax
  00489d7c  894c2454              mov       [esp+54h],ecx
  00489d80  8d442428              lea       eax,[esp+28h]
  00489d84  89542450              mov       [esp+50h],edx
  00489d88  8d4c2468              lea       ecx,[esp+68h]
  00489d8c  50                    push      eax
  00489d8d  8d542470              lea       edx,[esp+70h]
  00489d91  51                    push      ecx
  00489d92  52                    push      edx
  00489d93  56                    push      esi
  00489d94  895c2434              mov       [esp+34h],ebx
  00489d98  e8e3eb0a00            call      00538980h
  00489d9d  83c410                add       esp,10h
  00489da0  8d442444              lea       eax,[esp+44h]
  00489da4  8d4c241c              lea       ecx,[esp+1Ch]
  00489da8  8d542414              lea       edx,[esp+14h]
  00489dac  50                    push      eax
  00489dad  51                    push      ecx
  00489dae  8d442420              lea       eax,[esp+20h]
  00489db2  52                    push      edx
  00489db3  8d4c241c              lea       ecx,[esp+1Ch]
  00489db7  50                    push      eax
  00489db8  51                    push      ecx
  00489db9  56                    push      esi
  00489dba  e861c10a00            call      00535F20h
  00489dbf  8b4c2428              mov       ecx,[esp+28h]
  00489dc3  8b54245c              mov       edx,[esp+5Ch]
  00489dc7  83c418                add       esp,18h
  00489dca  b840000000            mov       eax,40h
  00489dcf  2bc1                  sub       eax,ecx
  00489dd1  52                    push      edx
  00489dd2  56                    push      esi
  00489dd3  89442428              mov       [esp+28h],eax
  00489dd7  e804c00400            call      004D5DE0h
  00489ddc  8b07                  mov       eax,[edi]
  00489dde  8b531c                mov       edx,[ebx+1Ch]
  00489de1  8b4e24                mov       ecx,[esi+24h]
  00489de4  2bc2                  sub       eax,edx
  00489de6  8b5324                mov       edx,[ebx+24h]
  00489de9  83c408                add       esp,8
  00489dec  c1f80c                sar       eax,0Ch
  00489def  2bca                  sub       ecx,edx
  00489df1  8bd0                  mov       edx,eax
  00489df3  c1f90c                sar       ecx,0Ch
  00489df6  0fafd0                imul      edx,eax
  00489df9  8bc1                  mov       eax,ecx
  00489dfb  0fafc1                imul      eax,ecx
  00489dfe  03d0                  add       edx,eax
  00489e00  52                    push      edx
  00489e01  e80abc0b00            call      00545A10h
  00489e06  89442418              mov       [esp+18h],eax
  00489e0a  8b44241c              mov       eax,[esp+1Ch]
  00489e0e  3bc5                  cmp       eax,ebp
  00489e10  0f842e090000          je        near 0048A744h
  00489e16  8b0e                  mov       ecx,[esi]
  00489e18  33d2                  xor       edx,edx
  00489e1a  8a5604                mov       dl,[esi+4]
  00489e1d  81e1fffffffd          and       ecx,0FDFFFFFFh
  00489e23  0fbf6e18              movsx     ebp,word [esi+18h]
  00489e27  890e                  mov       [esi],ecx
  00489e29  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00489e30  f7c100000001          test      ecx,1000000h
  00489e36  8b14aa                mov       edx,[edx+ebp*4]
  00489e39  741d                  je        short 00489E58h
  00489e3b  66ff461a              inc       word [esi+1Ah]
  00489e3f  668b4e1a              mov       cx,[esi+1Ah]
  00489e43  33ed                  xor       ebp,ebp
  00489e45  668b6a16              mov       bp,[edx+16h]
  00489e49  0fbfc9                movsx     ecx,cx
  00489e4c  3bcd                  cmp       ecx,ebp
  00489e4e  7c2e                  jl        short 00489E7Eh
  00489e50  66c7461a0000          mov       word [esi+1Ah],0
  00489e56  eb26                  jmp       short 00489E7Eh

LAB_00489e58:
  00489e58  66ff461a              inc       word [esi+1Ah]
  00489e5c  668b6e1a              mov       bp,[esi+1Ah]
  00489e60  33db                  xor       ebx,ebx
  00489e62  668b5a16              mov       bx,[edx+16h]
  00489e66  0fbfd5                movsx     edx,bp
  00489e69  3bd3                  cmp       edx,ebx
  00489e6b  7c0d                  jl        short 00489E7Ah
  00489e6d  4d                    dec       ebp
  00489e6e  81c900000002          or        ecx,2000000h
  00489e74  66896e1a              mov       [esi+1Ah],bp
  00489e78  890e                  mov       [esi],ecx

LAB_00489e7a:
  00489e7a  8b5c2424              mov       ebx,[esp+24h]

LAB_00489e7e:
  00489e7e  f60602                test      byte [esi],2
  00489e81  7475                  je        short 00489EF8h
  00489e83  807e420d              cmp       byte [esi+42h],0Dh
  00489e87  0f841e0e0000          je        near 0048ACABh
  00489e8d  8d44242c              lea       eax,[esp+2Ch]
  00489e91  6a00                  push      0
  00489e93  50                    push      eax
  00489e94  57                    push      edi
  00489e95  56                    push      esi
  00489e96  e895fd0800            call      00519C30h
  00489e9b  8be8                  mov       ebp,eax
  00489e9d  a1b8e05d00            mov       eax,[5DE0B8h]
  00489ea2  83c410                add       esp,10h
  00489ea5  85c0                  test      eax,eax
  00489ea7  7434                  je        short 00489EDDh
  00489ea9  8b4d00                mov       ecx,[ebp]
  00489eac  55                    push      ebp
  00489ead  83c940                or        ecx,40h
  00489eb0  56                    push      esi
  00489eb1  66c785960000000400    mov       word [ebp+96h],4
  00489eba  894d00                mov       [ebp],ecx
  00489ebd  e8befc0a00            call      00539B80h
  00489ec2  83c408                add       esp,8
  00489ec5  83c51c                add       ebp,1Ch
  00489ec8  55                    push      ebp
  00489ec9  6800180000            push      1800h
  00489ece  6800030000            push      300h
  00489ed3  6a03                  push      3
  00489ed5  e8463c0b00            call      0053DB20h
  00489eda  83c410                add       esp,10h

LAB_00489edd:
  00489edd  8b06                  mov       eax,[esi]
  00489edf  a900000002            test      eax,2000000h
  00489ee4  740e                  je        short 00489EF4h
  00489ee6  807e420c              cmp       byte [esi+42h],0Ch
  00489eea  7408                  je        short 00489EF4h
  00489eec  24fd                  and       al,0FDh
  00489eee  c6464300              mov       byte [esi+43h],0
  00489ef2  8906                  mov       [esi],eax

LAB_00489ef4:
  00489ef4  8b44241c              mov       eax,[esp+1Ch]

LAB_00489ef8:
  00489ef8  33c9                  xor       ecx,ecx
  00489efa  8a4e42                mov       cl,[esi+42h]
  00489efd  83f90d                cmp       ecx,0Dh
  00489f00  0f8784070000          ja        near 0048A68Ah
  00489f06  ff248d40ad4800        jmp       dword [ecx*4+48AD40h]
  00489f0d  33c9                  xor       ecx,ecx
  00489f0f  66394e12              cmp       [esi+12h],cx
  00489f13  751a                  jne       short 00489F2Fh
  00489f15  8b06                  mov       eax,[esi]
  00489f17  66894e18              mov       [esi+18h],cx
  00489f1b  0d00000001            or        eax,1000000h
  00489f20  66894e1a              mov       [esi+1Ah],cx
  00489f24  8906                  mov       [esi],eax
  00489f26  66c746120100          mov       word [esi+12h],1
  00489f2c  894e5c                mov       [esi+5Ch],ecx

LAB_00489f2f:
  00489f2f  0fbf4610              movsx     eax,word [esi+10h]
  00489f33  39442418              cmp       [esp+18h],eax
  00489f37  7d09                  jge       short 00489F42h
  00489f39  c6464300              mov       byte [esi+43h],0
  00489f3d  e948070000            jmp       0048A68Ah

LAB_00489f42:
  00489f42  8a4643                mov       al,[esi+43h]
  00489f45  84c0                  test      al,al
  00489f47  0f845e020000          je        near 0048A1ABh
  00489f4d  fec8                  dec       al
  00489f4f  894e5c                mov       [esi+5Ch],ecx
  00489f52  884643                mov       [esi+43h],al
  00489f55  e930070000            jmp       0048A68Ah
  00489f5a  33c9                  xor       ecx,ecx
  00489f5c  66394e12              cmp       [esi+12h],cx
  00489f60  7517                  jne       short 00489F79h
  00489f62  8b06                  mov       eax,[esi]
  00489f64  66894e18              mov       [esi+18h],cx
  00489f68  0d00000001            or        eax,1000000h
  00489f6d  66894e1a              mov       [esi+1Ah],cx
  00489f71  8906                  mov       [esi],eax
  00489f73  66c746120100          mov       word [esi+12h],1

LAB_00489f79:
  00489f79  8b442414              mov       eax,[esp+14h]
  00489f7d  8d90c8000000          lea       edx,[eax+0C8h]
  00489f83  81e2ff0f0000          and       edx,0FFFh
  00489f89  81fa90010000          cmp       edx,190h
  00489f8f  7f0c                  jg        short 00489F9Dh
  00489f91  c6464300              mov       byte [esi+43h],0
  00489f95  894e5c                mov       [esi+5Ch],ecx
  00489f98  e9ed060000            jmp       0048A68Ah

LAB_00489f9d:
  00489f9d  25ff0f0000            and       eax,0FFFh
  00489fa2  3d00080000            cmp       eax,800h
  00489fa7  7d15                  jge       short 00489FBEh
  00489fa9  8b442410              mov       eax,[esp+10h]
  00489fad  894e5c                mov       [esi+5Ch],ecx
  00489fb0  0580000000            add       eax,80h
  00489fb5  66014632              add       [esi+32h],ax
  00489fb9  e9cc060000            jmp       0048A68Ah

LAB_00489fbe:
  00489fbe  8b542410              mov       edx,[esp+10h]
  00489fc2  b880ffffff            mov       eax,0FFFFFF80h
  00489fc7  2bc2                  sub       eax,edx
  00489fc9  894e5c                mov       [esi+5Ch],ecx
  00489fcc  66014632              add       [esi+32h],ax
  00489fd0  e9b5060000            jmp       0048A68Ah
  00489fd5  8b4c2410              mov       ecx,[esp+10h]
  00489fd9  33d2                  xor       edx,edx
  00489fdb  66395612              cmp       [esi+12h],dx
  00489fdf  b801000000            mov       eax,1
  00489fe4  7525                  jne       short 0048A00Bh
  00489fe6  8b3e                  mov       edi,[esi]
  00489fe8  66894618              mov       [esi+18h],ax
  00489fec  81cf00000001          or        edi,1000000h
  00489ff2  6689561a              mov       [esi+1Ah],dx
  00489ff6  893e                  mov       [esi],edi
  00489ff8  8d3cc9                lea       edi,[ecx+ecx*8]
  00489ffb  c1e704                shl       edi,4
  00489ffe  81c700600000          add       edi,6000h
  0048a004  66894612              mov       [esi+12h],ax
  0048a008  897e5c                mov       [esi+5Ch],edi

LAB_0048a00b:
  0048a00b  66394612              cmp       [esi+12h],ax
  0048a00f  0f8575060000          jne       near 0048A68Ah
  0048a015  0fbf4610              movsx     eax,word [esi+10h]
  0048a019  39442418              cmp       [esp+18h],eax
  0048a01d  7d0c                  jge       short 0048A02Bh
  0048a01f  c6464300              mov       byte [esi+43h],0
  0048a023  89565c                mov       [esi+5Ch],edx
  0048a026  e95f060000            jmp       0048A68Ah

LAB_0048a02b:
  0048a02b  8a4643                mov       al,[esi+43h]
  0048a02e  84c0                  test      al,al
  0048a030  7405                  je        short 0048A037h
  0048a032  fec8                  dec       al
  0048a034  884643                mov       [esi+43h],al

LAB_0048a037:
  0048a037  8b442414              mov       eax,[esp+14h]
  0048a03b  8d9000010000          lea       edx,[eax+100h]
  0048a041  81e2ff0f0000          and       edx,0FFFh
  0048a047  81fa00020000          cmp       edx,200h
  0048a04d  0f8e37060000          jle       near 0048A68Ah
  0048a053  25ff0f0000            and       eax,0FFFh
  0048a058  3d00080000            cmp       eax,800h
  0048a05d  7d0d                  jge       short 0048A06Ch
  0048a05f  8d440928              lea       eax,[ecx+ecx+28h]
  0048a063  66014632              add       [esi+32h],ax
  0048a067  e91e060000            jmp       0048A68Ah

LAB_0048a06c:
  0048a06c  03c9                  add       ecx,ecx
  0048a06e  b8d8ffffff            mov       eax,0FFFFFFD8h
  0048a073  2bc1                  sub       eax,ecx
  0048a075  66014632              add       [esi+32h],ax
  0048a079  e90c060000            jmp       0048A68Ah
  0048a07e  66837e1201            cmp       word [esi+12h],1
  0048a083  0f859c000000          jne       near 0048A125h
  0048a089  8a4e43                mov       cl,[esi+43h]
  0048a08c  84c9                  test      cl,cl
  0048a08e  7405                  je        short 0048A095h
  0048a090  fec9                  dec       cl
  0048a092  884e43                mov       [esi+43h],cl

LAB_0048a095:
  0048a095  8b4c2410              mov       ecx,[esp+10h]
  0048a099  8d14c9                lea       edx,[ecx+ecx*8]
  0048a09c  c1e204                shl       edx,4
  0048a09f  8d8a00500000          lea       ecx,[edx+5000h]
  0048a0a5  0fafc1                imul      eax,ecx
  0048a0a8  c1f80c                sar       eax,0Ch
  0048a0ab  8be8                  mov       ebp,eax
  0048a0ad  894e5c                mov       [esi+5Ch],ecx
  0048a0b0  0fbf4610              movsx     eax,word [esi+10h]
  0048a0b4  50                    push      eax
  0048a0b5  e862b90b00            call      00545A1Ch
  0048a0ba  0fafc5                imul      eax,ebp
  0048a0bd  8b0f                  mov       ecx,[edi]
  0048a0bf  c1f80c                sar       eax,0Ch
  0048a0c2  03c8                  add       ecx,eax
  0048a0c4  890f                  mov       [edi],ecx
  0048a0c6  0fbf4e10              movsx     ecx,word [esi+10h]
  0048a0ca  51                    push      ecx
  0048a0cb  e846b90b00            call      00545A16h
  0048a0d0  0fafc5                imul      eax,ebp
  0048a0d3  8b6e24                mov       ebp,[esi+24h]
  0048a0d6  c7465c00000000        mov       dword [esi+5Ch],0
  0048a0dd  c1f80c                sar       eax,0Ch
  0048a0e0  03e8                  add       ebp,eax
  0048a0e2  8b442414              mov       eax,[esp+14h]
  0048a0e6  896e24                mov       [esi+24h],ebp
  0048a0e9  8d9000010000          lea       edx,[eax+100h]
  0048a0ef  81e2ff0f0000          and       edx,0FFFh
  0048a0f5  81fa00020000          cmp       edx,200h
  0048a0fb  7e28                  jle       short 0048A125h
  0048a0fd  25ff0f0000            and       eax,0FFFh
  0048a102  3d00080000            cmp       eax,800h
  0048a107  7d0a                  jge       short 0048A113h
  0048a109  8b442410              mov       eax,[esp+10h]
  0048a10d  8d440032              lea       eax,[eax+eax+32h]
  0048a111  eb0e                  jmp       short 0048A121h

LAB_0048a113:
  0048a113  8b4c2410              mov       ecx,[esp+10h]
  0048a117  b8ceffffff            mov       eax,0FFFFFFCEh
  0048a11c  8d1409                lea       edx,[ecx+ecx]
  0048a11f  2bc2                  sub       eax,edx

LAB_0048a121:
  0048a121  66014632              add       [esi+32h],ax

LAB_0048a125:
  0048a125  66837e1200            cmp       word [esi+12h],0
  0048a12a  0f855a050000          jne       near 0048A68Ah
  0048a130  8b0e                  mov       ecx,[esi]
  0048a132  b801000000            mov       eax,1
  0048a137  81c900000001          or        ecx,1000000h
  0048a13d  66894618              mov       [esi+18h],ax
  0048a141  66c7461a0000          mov       word [esi+1Ah],0
  0048a147  890e                  mov       [esi],ecx
  0048a149  66894612              mov       [esi+12h],ax
  0048a14d  e938050000            jmp       0048A68Ah
  0048a152  668b4e12              mov       cx,[esi+12h]
  0048a156  ba01000000            mov       edx,1
  0048a15b  663bca                cmp       cx,dx
  0048a15e  751c                  jne       short 0048A17Ch
  0048a160  0fbf4610              movsx     eax,word [esi+10h]
  0048a164  39442418              cmp       [esp+18h],eax
  0048a168  7d06                  jge       short 0048A170h
  0048a16a  c6464300              mov       byte [esi+43h],0
  0048a16e  eb0c                  jmp       short 0048A17Ch

LAB_0048a170:
  0048a170  8a4643                mov       al,[esi+43h]
  0048a173  84c0                  test      al,al
  0048a175  7405                  je        short 0048A17Ch
  0048a177  fec8                  dec       al
  0048a179  884643                mov       [esi+43h],al

LAB_0048a17c:
  0048a17c  6685c9                test      cx,cx
  0048a17f  0f8505050000          jne       near 0048A68Ah
  0048a185  8b442410              mov       eax,[esp+10h]
  0048a189  8b3e                  mov       edi,[esi]
  0048a18b  66894e1a              mov       [esi+1Ah],cx
  0048a18f  81cf00000001          or        edi,1000000h
  0048a195  8d0cc0                lea       ecx,[eax+eax*8]
  0048a198  66895618              mov       [esi+18h],dx
  0048a19c  c1e104                shl       ecx,4
  0048a19f  893e                  mov       [esi],edi
  0048a1a1  66895612              mov       [esi+12h],dx
  0048a1a5  81c100a00000          add       ecx,0A000h

LAB_0048a1ab:
  0048a1ab  894e5c                mov       [esi+5Ch],ecx
  0048a1ae  e9d7040000            jmp       0048A68Ah
  0048a1b3  33ff                  xor       edi,edi
  0048a1b5  66397e12              cmp       [esi+12h],di
  0048a1b9  7534                  jne       short 0048A1EFh
  0048a1bb  e880cf0b00            call      00547140h
  0048a1c0  99                    cdq
  0048a1c1  33c2                  xor       eax,edx
  0048a1c3  66816632ff0f          and       word [esi+32h],0FFFh
  0048a1c9  2bc2                  sub       eax,edx
  0048a1cb  66c746120100          mov       word [esi+12h],1
  0048a1d1  25ff0f0000            and       eax,0FFFh
  0048a1d6  66897e18              mov       [esi+18h],di
  0048a1da  33c2                  xor       eax,edx
  0048a1dc  66897e1a              mov       [esi+1Ah],di
  0048a1e0  2bc2                  sub       eax,edx
  0048a1e2  66894610              mov       [esi+10h],ax
  0048a1e6  8b06                  mov       eax,[esi]
  0048a1e8  0d00000001            or        eax,1000000h
  0048a1ed  8906                  mov       [esi],eax

LAB_0048a1ef:
  0048a1ef  66837e1201            cmp       word [esi+12h],1
  0048a1f4  0f8590040000          jne       near 0048A68Ah
  0048a1fa  0fbf4610              movsx     eax,word [esi+10h]
  0048a1fe  668b4e32              mov       cx,[esi+32h]
  0048a202  8bf8                  mov       edi,eax
  0048a204  0fbfd1                movsx     edx,cx
  0048a207  2bfa                  sub       edi,edx
  0048a209  c1ff03                sar       edi,3
  0048a20c  03cf                  add       ecx,edi
  0048a20e  66894e32              mov       [esi+32h],cx
  0048a212  0fbfc9                movsx     ecx,cx
  0048a215  2bc1                  sub       eax,ecx
  0048a217  83f814                cmp       eax,14h
  0048a21a  0f8d6a040000          jge       near 0048A68Ah
  0048a220  e81bcf0b00            call      00547140h
  0048a225  99                    cdq
  0048a226  b914000000            mov       ecx,14h
  0048a22b  c6464207              mov       byte [esi+42h],7
  0048a22f  f7f9                  idiv      ecx
  0048a231  80c21e                add       dl,1Eh
  0048a234  885643                mov       [esi+43h],dl
  0048a237  e94e040000            jmp       0048A68Ah
  0048a23c  8b4e20                mov       ecx,[esi+20h]
  0048a23f  8b4320                mov       eax,[ebx+20h]
  0048a242  8d9100400b00          lea       edx,[ecx+0B4000h]
  0048a248  3bd0                  cmp       edx,eax
  0048a24a  7d0f                  jge       short 0048A25Bh
  0048a24c  8b06                  mov       eax,[esi]
  0048a24e  c6464300              mov       byte [esi+43h],0
  0048a252  24fd                  and       al,0FDh
  0048a254  8906                  mov       [esi],eax
  0048a256  e92f040000            jmp       0048A68Ah

LAB_0048a25b:
  0048a25b  2bc1                  sub       eax,ecx
  0048a25d  0500800200            add       eax,28000h
  0048a262  c1f802                sar       eax,2
  0048a265  03c1                  add       eax,ecx
  0048a267  668b4e12              mov       cx,[esi+12h]
  0048a26b  6683f901              cmp       cx,1
  0048a26f  894620                mov       [esi+20h],eax
  0048a272  7511                  jne       short 0048A285h
  0048a274  8b06                  mov       eax,[esi]
  0048a276  a900000002            test      eax,2000000h
  0048a27b  7408                  je        short 0048A285h
  0048a27d  24fd                  and       al,0FDh
  0048a27f  c6464300              mov       byte [esi+43h],0
  0048a283  8906                  mov       [esi],eax

LAB_0048a285:
  0048a285  6685c9                test      cx,cx
  0048a288  0f85fc030000          jne       near 0048A68Ah
  0048a28e  8b06                  mov       eax,[esi]
  0048a290  66c746120100          mov       word [esi+12h],1
  0048a296  25fffffffe            and       eax,0FEFFFFFFh
  0048a29b  8906                  mov       [esi],eax
  0048a29d  e9e8030000            jmp       0048A68Ah
  0048a2a2  8b7c2410              mov       edi,[esp+10h]
  0048a2a6  8b542414              mov       edx,[esp+14h]
  0048a2aa  bd01000000            mov       ebp,1
  0048a2af  66396e12              cmp       [esi+12h],bp
  0048a2b3  756d                  jne       short 0048A322h
  0048a2b5  8b4620                mov       eax,[esi+20h]
  0048a2b8  8b4e60                mov       ecx,[esi+60h]
  0048a2bb  0500300000            add       eax,3000h
  0048a2c0  894620                mov       [esi+20h],eax
  0048a2c3  2bc1                  sub       eax,ecx
  0048a2c5  c1f80c                sar       eax,0Ch
  0048a2c8  3dc8000000            cmp       eax,0C8h
  0048a2cd  7e1d                  jle       short 0048A2ECh
  0048a2cf  33c0                  xor       eax,eax
  0048a2d1  66c746120200          mov       word [esi+12h],2
  0048a2d7  66894610              mov       [esi+10h],ax
  0048a2db  6689461a              mov       [esi+1Ah],ax
  0048a2df  8b06                  mov       eax,[esi]
  0048a2e1  66896e18              mov       [esi+18h],bp
  0048a2e5  0d02000001            or        eax,1000002h
  0048a2ea  8906                  mov       [esi],eax

LAB_0048a2ec:
  0048a2ec  8d8200010000          lea       eax,[edx+100h]
  0048a2f2  25ff0f0000            and       eax,0FFFh
  0048a2f7  3d00020000            cmp       eax,200h
  0048a2fc  7e24                  jle       short 0048A322h
  0048a2fe  8bca                  mov       ecx,edx
  0048a300  81e1ff0f0000          and       ecx,0FFFh
  0048a306  81f900080000          cmp       ecx,800h
  0048a30c  7d06                  jge       short 0048A314h
  0048a30e  8d443f28              lea       eax,[edi+edi+28h]
  0048a312  eb0a                  jmp       short 0048A31Eh

LAB_0048a314:
  0048a314  8d0c3f                lea       ecx,[edi+edi]
  0048a317  b8d8ffffff            mov       eax,0FFFFFFD8h
  0048a31c  2bc1                  sub       eax,ecx

LAB_0048a31e:
  0048a31e  66014632              add       [esi+32h],ax

LAB_0048a322:
  0048a322  66837e1202            cmp       word [esi+12h],2
  0048a327  0f858f000000          jne       near 0048A3BCh
  0048a32d  8b4e20                mov       ecx,[esi+20h]
  0048a330  c7465c00900100        mov       dword [esi+5Ch],19000h
  0048a337  8b4320                mov       eax,[ebx+20h]
  0048a33a  8da900400b00          lea       ebp,[ecx+0B4000h]
  0048a340  3be8                  cmp       ebp,eax
  0048a342  7d16                  jge       short 0048A35Ah
  0048a344  8b06                  mov       eax,[esi]
  0048a346  c6464300              mov       byte [esi+43h],0
  0048a34a  24fd                  and       al,0FDh
  0048a34c  c7465c00000000        mov       dword [esi+5Ch],0
  0048a353  8906                  mov       [esi],eax
  0048a355  e930030000            jmp       0048A68Ah

LAB_0048a35a:
  0048a35a  2bc1                  sub       eax,ecx
  0048a35c  0500800200            add       eax,28000h
  0048a361  c1f802                sar       eax,2
  0048a364  03c1                  add       eax,ecx
  0048a366  66ff4610              inc       word [esi+10h]
  0048a36a  668b4e10              mov       cx,[esi+10h]
  0048a36e  894620                mov       [esi+20h],eax
  0048a371  8d8200010000          lea       eax,[edx+100h]
  0048a377  25ff0f0000            and       eax,0FFFh
  0048a37c  3d00020000            cmp       eax,200h
  0048a381  7e22                  jle       short 0048A3A5h
  0048a383  81e2ff0f0000          and       edx,0FFFh
  0048a389  81fa00080000          cmp       edx,800h
  0048a38f  7d06                  jge       short 0048A397h
  0048a391  8d443f28              lea       eax,[edi+edi+28h]
  0048a395  eb0a                  jmp       short 0048A3A1h

LAB_0048a397:
  0048a397  8d143f                lea       edx,[edi+edi]
  0048a39a  b8d8ffffff            mov       eax,0FFFFFFD8h
  0048a39f  2bc2                  sub       eax,edx

LAB_0048a3a1:
  0048a3a1  66014632              add       [esi+32h],ax

LAB_0048a3a5:
  0048a3a5  6683f921              cmp       cx,21h
  0048a3a9  7c0c                  jl        short 0048A3B7h
  0048a3ab  8b06                  mov       eax,[esi]
  0048a3ad  66c746120300          mov       word [esi+12h],3
  0048a3b3  24fd                  and       al,0FDh
  0048a3b5  8906                  mov       [esi],eax

LAB_0048a3b7:
  0048a3b7  bd01000000            mov       ebp,1

LAB_0048a3bc:
  0048a3bc  668b4e12              mov       cx,[esi+12h]
  0048a3c0  6683f903              cmp       cx,3
  0048a3c4  751d                  jne       short 0048A3E3h
  0048a3c6  8b465c                mov       eax,[esi+5Ch]
  0048a3c9  8bd0                  mov       edx,eax
  0048a3cb  c1fa03                sar       edx,3
  0048a3ce  2bc2                  sub       eax,edx
  0048a3d0  89465c                mov       [esi+5Ch],eax
  0048a3d3  2500f0ffff            and       eax,0FFFFF000h
  0048a3d8  3d00a00000            cmp       eax,0A000h
  0048a3dd  7d04                  jge       short 0048A3E3h
  0048a3df  c6464300              mov       byte [esi+43h],0

LAB_0048a3e3:
  0048a3e3  33c0                  xor       eax,eax
  0048a3e5  663bc8                cmp       cx,ax
  0048a3e8  0f859c020000          jne       near 0048A68Ah
  0048a3ee  6689461a              mov       [esi+1Ah],ax
  0048a3f2  89465c                mov       [esi+5Ch],eax
  0048a3f5  8b06                  mov       eax,[esi]
  0048a3f7  66896e12              mov       [esi+12h],bp
  0048a3fb  0d00000001            or        eax,1000000h
  0048a400  66896e18              mov       [esi+18h],bp
  0048a404  8906                  mov       [esi],eax
  0048a406  e97f020000            jmp       0048A68Ah
  0048a40b  33d2                  xor       edx,edx
  0048a40d  66395612              cmp       [esi+12h],dx
  0048a411  7548                  jne       short 0048A45Bh
  0048a413  b901000000            mov       ecx,1
  0048a418  66394e16              cmp       [esi+16h],cx
  0048a41c  7408                  je        short 0048A426h
  0048a41e  885643                mov       [esi+43h],dl
  0048a421  e964020000            jmp       0048A68Ah

LAB_0048a426:
  0048a426  8b442414              mov       eax,[esp+14h]
  0048a42a  25ff0f0000            and       eax,0FFFh
  0048a42f  663bc2                cmp       ax,dx
  0048a432  66894612              mov       [esi+12h],ax
  0048a436  7504                  jne       short 0048A43Ch
  0048a438  66894e12              mov       [esi+12h],cx

LAB_0048a43c:
  0048a43c  668b4612              mov       ax,[esi+12h]
  0048a440  89565c                mov       [esi+5Ch],edx
  0048a443  66f7d8                neg       ax
  0048a446  66894612              mov       [esi+12h],ax
  0048a44a  8b06                  mov       eax,[esi]
  0048a44c  0d00000001            or        eax,1000000h
  0048a451  66894e18              mov       [esi+18h],cx
  0048a455  6689561a              mov       [esi+1Ah],dx
  0048a459  8906                  mov       [esi],eax

LAB_0048a45b:
  0048a45b  668b4612              mov       ax,[esi+12h]
  0048a45f  663bc2                cmp       ax,dx
  0048a462  0f8447010000          je        near 0048A5AFh
  0048a468  6685c0                test      ax,ax
  0048a46b  0f8d3e010000          jge       near 0048A5AFh
  0048a471  8b2f                  mov       ebp,[edi]
  0048a473  0fbfc8                movsx     ecx,ax
  0048a476  f7d9                  neg       ecx
  0048a478  51                    push      ecx
  0048a479  e89eb50b00            call      00545A1Ch
  0048a47e  8d0440                lea       eax,[eax+eax*2]
  0048a481  8d1480                lea       edx,[eax+eax*4]
  0048a484  8b431c                mov       eax,[ebx+1Ch]
  0048a487  c1e202                shl       edx,2
  0048a48a  2bd5                  sub       edx,ebp
  0048a48c  03d0                  add       edx,eax
  0048a48e  0fbf4612              movsx     eax,word [esi+12h]
  0048a492  c1fa03                sar       edx,3
  0048a495  f7d8                  neg       eax
  0048a497  03d5                  add       edx,ebp
  0048a499  8b6e24                mov       ebp,[esi+24h]
  0048a49c  50                    push      eax
  0048a49d  8917                  mov       [edi],edx
  0048a49f  e872b50b00            call      00545A16h
  0048a4a4  8b5324                mov       edx,[ebx+24h]
  0048a4a7  8d0440                lea       eax,[eax+eax*2]
  0048a4aa  8d0c80                lea       ecx,[eax+eax*4]
  0048a4ad  8b4620                mov       eax,[esi+20h]
  0048a4b0  c1e102                shl       ecx,2
  0048a4b3  2bcd                  sub       ecx,ebp
  0048a4b5  03ca                  add       ecx,edx
  0048a4b7  c1f903                sar       ecx,3
  0048a4ba  03cd                  add       ecx,ebp
  0048a4bc  894e24                mov       [esi+24h],ecx
  0048a4bf  8b5320                mov       edx,[ebx+20h]
  0048a4c2  2bd0                  sub       edx,eax
  0048a4c4  81c200800200          add       edx,28000h
  0048a4ca  c1fa03                sar       edx,3
  0048a4cd  03d0                  add       edx,eax
  0048a4cf  0fbf4612              movsx     eax,word [esi+12h]
  0048a4d3  f7d8                  neg       eax
  0048a4d5  50                    push      eax
  0048a4d6  895620                mov       [esi+20h],edx
  0048a4d9  e83eb50b00            call      00545A1Ch
  0048a4de  8b2f                  mov       ebp,[edi]
  0048a4e0  8b531c                mov       edx,[ebx+1Ch]
  0048a4e3  8d0c80                lea       ecx,[eax+eax*4]
  0048a4e6  c1e104                shl       ecx,4
  0048a4e9  2bcd                  sub       ecx,ebp
  0048a4eb  03ca                  add       ecx,edx
  0048a4ed  f7c1f8ffffff          test      ecx,0FFFFFFF8h
  0048a4f3  7c20                  jl        short 0048A515h
  0048a4f5  0fbf5612              movsx     edx,word [esi+12h]
  0048a4f9  f7da                  neg       edx
  0048a4fb  52                    push      edx
  0048a4fc  e81bb50b00            call      00545A1Ch
  0048a501  8b2f                  mov       ebp,[edi]
  0048a503  8b531c                mov       edx,[ebx+1Ch]
  0048a506  8d0480                lea       eax,[eax+eax*4]
  0048a509  c1e004                shl       eax,4
  0048a50c  2bc5                  sub       eax,ebp
  0048a50e  03c2                  add       eax,edx
  0048a510  c1f803                sar       eax,3
  0048a513  eb20                  jmp       short 0048A535h

LAB_0048a515:
  0048a515  0fbf4612              movsx     eax,word [esi+12h]
  0048a519  f7d8                  neg       eax
  0048a51b  50                    push      eax
  0048a51c  e8fbb40b00            call      00545A1Ch
  0048a521  8b0f                  mov       ecx,[edi]
  0048a523  8b6b1c                mov       ebp,[ebx+1Ch]
  0048a526  8d0480                lea       eax,[eax+eax*4]
  0048a529  c1e004                shl       eax,4
  0048a52c  2bc1                  sub       eax,ecx
  0048a52e  03c5                  add       eax,ebp
  0048a530  c1f803                sar       eax,3
  0048a533  f7d8                  neg       eax

LAB_0048a535:
  0048a535  2500f0ffff            and       eax,0FFFFF000h
  0048a53a  3d00500000            cmp       eax,5000h
  0048a53f  7d1d                  jge       short 0048A55Eh
  0048a541  668b4e12              mov       cx,[esi+12h]
  0048a545  8b06                  mov       eax,[esi]
  0048a547  66f7d9                neg       cx
  0048a54a  0c02                  or        al,2
  0048a54c  66894e12              mov       [esi+12h],cx
  0048a550  8906                  mov       [esi],eax
  0048a552  66c746180100          mov       word [esi+18h],1
  0048a558  66c7461a0000          mov       word [esi+1Ah],0

LAB_0048a55e:
  0048a55e  8b442414              mov       eax,[esp+14h]
  0048a562  8d9000010000          lea       edx,[eax+100h]
  0048a568  81e2ff0f0000          and       edx,0FFFh
  0048a56e  81fa00020000          cmp       edx,200h
  0048a574  7e2b                  jle       short 0048A5A1h
  0048a576  25ff0f0000            and       eax,0FFFh
  0048a57b  3d00080000            cmp       eax,800h
  0048a580  7d0d                  jge       short 0048A58Fh
  0048a582  8b442410              mov       eax,[esp+10h]
  0048a586  8d840080000000        lea       eax,[eax+eax+80h]
  0048a58d  eb0e                  jmp       short 0048A59Dh

LAB_0048a58f:
  0048a58f  8b4c2410              mov       ecx,[esp+10h]
  0048a593  b880ffffff            mov       eax,0FFFFFF80h
  0048a598  8d1409                lea       edx,[ecx+ecx]
  0048a59b  2bc2                  sub       eax,edx

LAB_0048a59d:
  0048a59d  66014632              add       [esi+32h],ax

LAB_0048a5a1:
  0048a5a1  8a4643                mov       al,[esi+43h]
  0048a5a4  fec8                  dec       al
  0048a5a6  884643                mov       [esi+43h],al
  0048a5a9  7504                  jne       short 0048A5AFh
  0048a5ab  c6464300              mov       byte [esi+43h],0

LAB_0048a5af:
  0048a5af  668b4612              mov       ax,[esi+12h]
  0048a5b3  6685c0                test      ax,ax
  0048a5b6  0f84ce000000          je        near 0048A68Ah
  0048a5bc  0f8ec8000000          jle       near 0048A68Ah
  0048a5c2  0fbfc0                movsx     eax,ax
  0048a5c5  50                    push      eax
  0048a5c6  e851b40b00            call      00545A1Ch
  0048a5cb  8b531c                mov       edx,[ebx+1Ch]
  0048a5ce  8d0c80                lea       ecx,[eax+eax*4]
  0048a5d1  c1e104                shl       ecx,4
  0048a5d4  03ca                  add       ecx,edx
  0048a5d6  0fbf5612              movsx     edx,word [esi+12h]
  0048a5da  52                    push      edx
  0048a5db  890f                  mov       [edi],ecx
  0048a5dd  e834b40b00            call      00545A16h
  0048a5e2  8b7b24                mov       edi,[ebx+24h]
  0048a5e5  8d0480                lea       eax,[eax+eax*4]
  0048a5e8  c1e004                shl       eax,4
  0048a5eb  03c7                  add       eax,edi
  0048a5ed  894624                mov       [esi+24h],eax
  0048a5f0  8b4b20                mov       ecx,[ebx+20h]
  0048a5f3  8a4643                mov       al,[esi+43h]
  0048a5f6  81c100800200          add       ecx,28000h
  0048a5fc  fec8                  dec       al
  0048a5fe  894e20                mov       [esi+20h],ecx
  0048a601  884643                mov       [esi+43h],al
  0048a604  7504                  jne       short 0048A60Ah
  0048a606  c6464300              mov       byte [esi+43h],0

LAB_0048a60a:
  0048a60a  8b442414              mov       eax,[esp+14h]
  0048a60e  8d9000010000          lea       edx,[eax+100h]
  0048a614  81e2ff0f0000          and       edx,0FFFh
  0048a61a  81fa00020000          cmp       edx,200h
  0048a620  7e2b                  jle       short 0048A64Dh
  0048a622  25ff0f0000            and       eax,0FFFh
  0048a627  3d00080000            cmp       eax,800h
  0048a62c  7d0d                  jge       short 0048A63Bh
  0048a62e  8b442410              mov       eax,[esp+10h]
  0048a632  8d840080000000        lea       eax,[eax+eax+80h]
  0048a639  eb0e                  jmp       short 0048A649h

LAB_0048a63b:
  0048a63b  8b4c2410              mov       ecx,[esp+10h]
  0048a63f  b880ffffff            mov       eax,0FFFFFF80h
  0048a644  8d1409                lea       edx,[ecx+ecx]
  0048a647  2bc2                  sub       eax,edx

LAB_0048a649:
  0048a649  66014632              add       [esi+32h],ax

LAB_0048a64d:
  0048a64d  f70300000040          test      dword [ebx],40000000h
  0048a653  7404                  je        short 0048A659h
  0048a655  c6464300              mov       byte [esi+43h],0

LAB_0048a659:
  0048a659  33c0                  xor       eax,eax
  0048a65b  8a4308                mov       al,[ebx+8]
  0048a65e  8d0cc0                lea       ecx,[eax+eax*8]
  0048a661  8d1448                lea       edx,[eax+ecx*2]
  0048a664  33c0                  xor       eax,eax
  0048a666  8a049588915e00        mov       al,[edx*4+5E9188h]
  0048a66d  c1e004                shl       eax,4
  0048a670  f680224a6000f0        test      byte [eax+604A22h],0F0h
  0048a677  7411                  je        short 0048A68Ah
  0048a679  33c0                  xor       eax,eax
  0048a67b  8a4643                mov       al,[esi+43h]
  0048a67e  83e805                sub       eax,5
  0048a681  85c0                  test      eax,eax
  0048a683  7f02                  jg        short 0048A687h
  0048a685  33c0                  xor       eax,eax

LAB_0048a687:
  0048a687  884643                mov       [esi+43h],al

LAB_0048a68a:
  0048a68a  8b0e                  mov       ecx,[esi]
  0048a68c  8a4642                mov       al,[esi+42h]
  0048a68f  f6c120                test      cl,20h
  0048a692  745e                  je        short 0048A6F2h
  0048a694  3c0c                  cmp       al,0Ch
  0048a696  7445                  je        short 0048A6DDh
  0048a698  e8a3ca0b00            call      00547140h
  0048a69d  99                    cdq
  0048a69e  b932000000            mov       ecx,32h
  0048a6a3  8b7e20                mov       edi,[esi+20h]
  0048a6a6  f7f9                  idiv      ecx
  0048a6a8  66035614              add       dx,[esi+14h]
  0048a6ac  03d1                  add       edx,ecx
  0048a6ae  81e2ff0f0000          and       edx,0FFFh
  0048a6b4  66895614              mov       [esi+14h],dx
  0048a6b8  0fbfd2                movsx     edx,dx
  0048a6bb  52                    push      edx
  0048a6bc  e85bb30b00            call      00545A1Ch
  0048a6c1  8b4e48                mov       ecx,[esi+48h]
  0048a6c4  8d0440                lea       eax,[eax+eax*2]
  0048a6c7  8d0480                lea       eax,[eax+eax*4]
  0048a6ca  8d1481                lea       edx,[ecx+eax*4]
  0048a6cd  2bd7                  sub       edx,edi
  0048a6cf  81c200c00800          add       edx,8C000h
  0048a6d5  c1fa03                sar       edx,3
  0048a6d8  03d7                  add       edx,edi
  0048a6da  895620                mov       [esi+20h],edx

LAB_0048a6dd:
  0048a6dd  8b4364                mov       eax,[ebx+64h]
  0048a6e0  8b4b20                mov       ecx,[ebx+20h]
  0048a6e3  33c8                  xor       ecx,eax
  0048a6e5  f7c100f0ffff          test      ecx,0FFFFF000h
  0048a6eb  7557                  jne       short 0048A744h
  0048a6ed  894648                mov       [esi+48h],eax
  0048a6f0  eb52                  jmp       short 0048A744h

LAB_0048a6f2:
  0048a6f2  3c0c                  cmp       al,0Ch
  0048a6f4  7404                  je        short 0048A6FAh
  0048a6f6  3c0a                  cmp       al,0Ah
  0048a6f8  7505                  jne       short 0048A6FFh

LAB_0048a6fa:
  0048a6fa  f6c102                test      cl,2
  0048a6fd  7445                  je        short 0048A744h

LAB_0048a6ff:
  0048a6ff  e83cca0b00            call      00547140h
  0048a704  99                    cdq
  0048a705  b932000000            mov       ecx,32h
  0048a70a  8b7e20                mov       edi,[esi+20h]
  0048a70d  f7f9                  idiv      ecx
  0048a70f  66035614              add       dx,[esi+14h]
  0048a713  03d1                  add       edx,ecx
  0048a715  81e2ff0f0000          and       edx,0FFFh
  0048a71b  66895614              mov       [esi+14h],dx
  0048a71f  0fbfd2                movsx     edx,dx
  0048a722  52                    push      edx
  0048a723  e8f4b20b00            call      00545A1Ch
  0048a728  8b4e60                mov       ecx,[esi+60h]
  0048a72b  8d0440                lea       eax,[eax+eax*2]
  0048a72e  8d0480                lea       eax,[eax+eax*4]
  0048a731  8d1481                lea       edx,[ecx+eax*4]
  0048a734  2bd7                  sub       edx,edi
  0048a736  81c200a00a00          add       edx,0AA000h
  0048a73c  c1fa03                sar       edx,3
  0048a73f  03d7                  add       edx,edi
  0048a741  895620                mov       [esi+20h],edx

LAB_0048a744:
  0048a744  0fbf4632              movsx     eax,word [esi+32h]
  0048a748  50                    push      eax
  0048a749  e8ceb20b00            call      00545A1Ch
  0048a74e  0fbf4e32              movsx     ecx,word [esi+32h]
  0048a752  51                    push      ecx
  0048a753  89442474              mov       [esp+74h],eax
  0048a757  e8bab20b00            call      00545A16h
  0048a75c  8b5650                mov       edx,[esi+50h]
  0048a75f  89442478              mov       [esp+78h],eax
  0048a763  8bc2                  mov       eax,edx
  0048a765  8b4c241c              mov       ecx,[esp+1Ch]
  0048a769  c1f803                sar       eax,3
  0048a76c  0faf4e5c              imul      ecx,[esi+5Ch]
  0048a770  8bfa                  mov       edi,edx
  0048a772  56                    push      esi
  0048a773  2bf8                  sub       edi,eax
  0048a775  c1fa08                sar       edx,8
  0048a778  2bfa                  sub       edi,edx
  0048a77a  8b5654                mov       edx,[esi+54h]
  0048a77d  8bc2                  mov       eax,edx
  0048a77f  8bea                  mov       ebp,edx
  0048a781  c1f803                sar       eax,3
  0048a784  2be8                  sub       ebp,eax
  0048a786  897e50                mov       [esi+50h],edi
  0048a789  c1fa08                sar       edx,8
  0048a78c  2bea                  sub       ebp,edx
  0048a78e  8b5658                mov       edx,[esi+58h]
  0048a791  8bda                  mov       ebx,edx
  0048a793  8bc2                  mov       eax,edx
  0048a795  c1fb03                sar       ebx,3
  0048a798  2bc3                  sub       eax,ebx
  0048a79a  8b5e20                mov       ebx,[esi+20h]
  0048a79d  c1fa08                sar       edx,8
  0048a7a0  c1f90c                sar       ecx,0Ch
  0048a7a3  2bc2                  sub       eax,edx
  0048a7a5  8bd1                  mov       edx,ecx
  0048a7a7  0faf542474            imul      edx,[esp+74h]
  0048a7ac  0faf4c247c            imul      ecx,[esp+7Ch]
  0048a7b1  c1fa0c                sar       edx,0Ch
  0048a7b4  c1f90c                sar       ecx,0Ch
  0048a7b7  894658                mov       [esi+58h],eax
  0048a7ba  03d7                  add       edx,edi
  0048a7bc  8b7e1c                mov       edi,[esi+1Ch]
  0048a7bf  03c8                  add       ecx,eax
  0048a7c1  8b4624                mov       eax,[esi+24h]
  0048a7c4  03fa                  add       edi,edx
  0048a7c6  03dd                  add       ebx,ebp
  0048a7c8  03c1                  add       eax,ecx
  0048a7ca  896e54                mov       [esi+54h],ebp
  0048a7cd  897e1c                mov       [esi+1Ch],edi
  0048a7d0  895e20                mov       [esi+20h],ebx
  0048a7d3  894624                mov       [esi+24h],eax
  0048a7d6  e865910a00            call      00533940h
  0048a7db  8bbe90000000          mov       edi,[esi+90h]
  0048a7e1  8b06                  mov       eax,[esi]
  0048a7e3  83c404                add       esp,4
  0048a7e6  81e7fff8ffff          and       edi,0FFFFF8FFh
  0048a7ec  a820                  test      al,20h
  0048a7ee  89be90000000          mov       [esi+90h],edi
  0048a7f4  8bcf                  mov       ecx,edi
  0048a7f6  740c                  je        short 0048A804h
  0048a7f8  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  0048a7fe  898e90000000          mov       [esi+90h],ecx

LAB_0048a804:
  0048a804  8b4e60                mov       ecx,[esi+60h]
  0048a807  8b5620                mov       edx,[esi+20h]
  0048a80a  3bd1                  cmp       edx,ecx
  0048a80c  7f19                  jg        short 0048A827h
  0048a80e  8b9690000000          mov       edx,[esi+90h]
  0048a814  f6c680                test      dh,80h
  0048a817  750e                  jne       short 0048A827h
  0048a819  24fe                  and       al,0FEh
  0048a81b  33db                  xor       ebx,ebx
  0048a81d  894e20                mov       [esi+20h],ecx
  0048a820  8906                  mov       [esi],eax
  0048a822  895e54                mov       [esi+54h],ebx
  0048a825  eb02                  jmp       short 0048A829h

LAB_0048a827:
  0048a827  33db                  xor       ebx,ebx

LAB_0048a829:
  0048a829  f7869000000082a80020  test      dword [esi+90h],2000A882h
  0048a833  0f84c0000000          je        near 0048A8F9h
  0048a839  8a4e42                mov       cl,[esi+42h]
  0048a83c  b007                  mov       al,7
  0048a83e  3ac8                  cmp       cl,al
  0048a840  7420                  je        short 0048A862h
  0048a842  884642                mov       [esi+42h],al
  0048a845  e8f6c80b00            call      00547140h
  0048a84a  99                    cdq
  0048a84b  b928000000            mov       ecx,28h
  0048a850  66895e12              mov       [esi+12h],bx
  0048a854  f7f9                  idiv      ecx
  0048a856  66c746105000          mov       word [esi+10h],50h
  0048a85c  80c214                add       dl,14h
  0048a85f  885643                mov       [esi+43h],dl

LAB_0048a862:
  0048a862  8b16                  mov       edx,[esi]
  0048a864  8b442464              mov       eax,[esp+64h]
  0048a868  8b4c2458              mov       ecx,[esp+58h]
  0048a86c  83e2fd                and       edx,0FFFFFFFDh
  0048a86f  894624                mov       [esi+24h],eax
  0048a872  8b442448              mov       eax,[esp+48h]
  0048a876  8916                  mov       [esi],edx
  0048a878  8b54245c              mov       edx,[esp+5Ch]
  0048a87c  25ffff0000            and       eax,0FFFFh
  0048a881  89561c                mov       [esi+1Ch],edx
  0048a884  8b542438              mov       edx,[esp+38h]
  0048a888  894e7c                mov       [esi+7Ch],ecx
  0048a88b  8b4c2454              mov       ecx,[esp+54h]
  0048a88f  898690000000          mov       [esi+90h],eax
  0048a895  8b44244c              mov       eax,[esp+4Ch]
  0048a899  899680000000          mov       [esi+80h],edx
  0048a89f  8b542450              mov       edx,[esp+50h]
  0048a8a3  898684000000          mov       [esi+84h],eax
  0048a8a9  8b44243c              mov       eax,[esp+3Ch]
  0048a8ad  898e88000000          mov       [esi+88h],ecx
  0048a8b3  8b4c2440              mov       ecx,[esp+40h]
  0048a8b7  6882a80020            push      2000A882h
  0048a8bc  56                    push      esi
  0048a8bd  89968c000000          mov       [esi+8Ch],edx
  0048a8c3  894664                mov       [esi+64h],eax
  0048a8c6  894e60                mov       [esi+60h],ecx
  0048a8c9  e892b50400            call      004D5E60h
  0048a8ce  83c408                add       esp,8
  0048a8d1  663bc3                cmp       ax,bx
  0048a8d4  7515                  jne       short 0048A8EBh
  0048a8d6  e865c80b00            call      00547140h
  0048a8db  99                    cdq
  0048a8dc  33c2                  xor       eax,edx
  0048a8de  2bc2                  sub       eax,edx
  0048a8e0  83e001                and       eax,1
  0048a8e3  33c2                  xor       eax,edx
  0048a8e5  2bc2                  sub       eax,edx
  0048a8e7  8d4400ff              lea       eax,[eax+eax-1]

LAB_0048a8eb:
  0048a8eb  8b542410              mov       edx,[esp+10h]
  0048a8ef  83c240                add       edx,40h
  0048a8f2  0fafd0                imul      edx,eax
  0048a8f5  66015632              add       [esi+32h],dx

LAB_0048a8f9:
  0048a8f9  6aff                  push      0FFFFFFFFh
  0048a8fb  6aff                  push      0FFFFFFFFh
  0048a8fd  6aff                  push      0FFFFFFFFh
  0048a8ff  56                    push      esi
  0048a900  e80b8f0300            call      004C3810h
  0048a905  8b44242c              mov       eax,[esp+2Ch]
  0048a909  83c410                add       esp,10h
  0048a90c  3bc3                  cmp       eax,ebx
  0048a90e  7517                  jne       short 0048A927h
  0048a910  668b842486000000      mov       ax,[esp+86h]
  0048a918  66894632              mov       [esi+32h],ax
  0048a91c  5f                    pop       edi
  0048a91d  5e                    pop       esi
  0048a91e  5d                    pop       ebp
  0048a91f  5b                    pop       ebx
  0048a920  81c488000000          add       esp,88h
  0048a926  c3                    ret

LAB_0048a927:
  0048a927  385e43                cmp       [esi+43h],bl
  0048a92a  0f8570030000          jne       near 0048ACA0h
  0048a930  8b466c                mov       eax,[esi+6Ch]
  0048a933  8b4e1c                mov       ecx,[esi+1Ch]
  0048a936  8b5624                mov       edx,[esi+24h]
  0048a939  8b781c                mov       edi,[eax+1Ch]
  0048a93c  8b6824                mov       ebp,[eax+24h]
  0048a93f  2bf9                  sub       edi,ecx
  0048a941  2bea                  sub       ebp,edx
  0048a943  c1ff0c                sar       edi,0Ch
  0048a946  c1fd0c                sar       ebp,0Ch
  0048a949  8bcf                  mov       ecx,edi
  0048a94b  8bd5                  mov       edx,ebp
  0048a94d  0fafcf                imul      ecx,edi
  0048a950  0fafd5                imul      edx,ebp
  0048a953  03ca                  add       ecx,edx
  0048a955  51                    push      ecx
  0048a956  e8b5b00b00            call      00545A10h
  0048a95b  55                    push      ebp
  0048a95c  57                    push      edi
  0048a95d  89442420              mov       [esp+20h],eax
  0048a961  e8bcb00b00            call      00545A22h
  0048a966  668b4e32              mov       cx,[esi+32h]
  0048a96a  66895e12              mov       [esi+12h],bx
  0048a96e  2bc1                  sub       eax,ecx
  0048a970  25ff0f0000            and       eax,0FFFh
  0048a975  89442414              mov       [esp+14h],eax
  0048a979  8b442418              mov       eax,[esp+18h]
  0048a97d  3d96000000            cmp       eax,96h
  0048a982  0f8d38010000          jge       near 0048AAC0h
  0048a988  e8b3c70b00            call      00547140h
  0048a98d  99                    cdq
  0048a98e  b964000000            mov       ecx,64h
  0048a993  f7f9                  idiv      ecx
  0048a995  8bfa                  mov       edi,edx
  0048a997  83ff32                cmp       edi,32h
  0048a99a  0f8dbb000000          jge       near 0048AA5Bh
  0048a9a0  8b542414              mov       edx,[esp+14h]
  0048a9a4  8b6c2420              mov       ebp,[esp+20h]
  0048a9a8  8d84aa00010000        lea       eax,[edx+ebp*4+100h]
  0048a9af  8d0ced00020000        lea       ecx,[ebp*8+200h]
  0048a9b6  25ff0f0000            and       eax,0FFFh
  0048a9bb  3bc1                  cmp       eax,ecx
  0048a9bd  7f6b                  jg        short 0048AA2Ah
  0048a9bf  e87cc70b00            call      00547140h
  0048a9c4  99                    cdq
  0048a9c5  8d4c2d01              lea       ecx,[ebp+ebp+1]
  0048a9c9  f7f9                  idiv      ecx
  0048a9cb  8b442418              mov       eax,[esp+18h]
  0048a9cf  83c264                add       edx,64h
  0048a9d2  3bc2                  cmp       eax,edx
  0048a9d4  7d54                  jge       short 0048AA2Ah
  0048a9d6  83ff1e                cmp       edi,1Eh
  0048a9d9  7d27                  jge       short 0048AA02h
  0048a9db  8b16                  mov       edx,[esi]
  0048a9dd  c646420b              mov       byte [esi+42h],0Bh
  0048a9e1  81e2fffffffe          and       edx,0FEFFFFFFh
  0048a9e7  c6464301              mov       byte [esi+43h],1
  0048a9eb  83ca02                or        edx,2
  0048a9ee  895e5c                mov       [esi+5Ch],ebx
  0048a9f1  8916                  mov       [esi],edx
  0048a9f3  66c746180100          mov       word [esi+18h],1
  0048a9f9  66895e1a              mov       [esi+1Ah],bx
  0048a9fd  e9c2000000            jmp       0048AAC4h

LAB_0048aa02:
  0048aa02  66837e1601            cmp       word [esi+16h],1
  0048aa07  7509                  jne       short 0048AA12h
  0048aa09  8a4e42                mov       cl,[esi+42h]
  0048aa0c  b00d                  mov       al,0Dh
  0048aa0e  3ac8                  cmp       cl,al
  0048aa10  7528                  jne       short 0048AA3Ah

LAB_0048aa12:
  0048aa12  885e42                mov       [esi+42h],bl
  0048aa15  e826c70b00            call      00547140h
  0048aa1a  99                    cdq
  0048aa1b  b928000000            mov       ecx,28h
  0048aa20  f7f9                  idiv      ecx
  0048aa22  885643                mov       [esi+43h],dl
  0048aa25  e99a000000            jmp       0048AAC4h

LAB_0048aa2a:
  0048aa2a  66837e1601            cmp       word [esi+16h],1
  0048aa2f  7520                  jne       short 0048AA51h
  0048aa31  8a4e42                mov       cl,[esi+42h]
  0048aa34  b00d                  mov       al,0Dh
  0048aa36  3ac8                  cmp       cl,al
  0048aa38  7417                  je        short 0048AA51h

LAB_0048aa3a:
  0048aa3a  884642                mov       [esi+42h],al
  0048aa3d  e8fec60b00            call      00547140h
  0048aa42  99                    cdq
  0048aa43  b928000000            mov       ecx,28h
  0048aa48  f7f9                  idiv      ecx
  0048aa4a  02d1                  add       dl,cl
  0048aa4c  885643                mov       [esi+43h],dl
  0048aa4f  eb73                  jmp       short 0048AAC4h

LAB_0048aa51:
  0048aa51  c6464209              mov       byte [esi+42h],9
  0048aa55  c6464301              mov       byte [esi+43h],1
  0048aa59  eb69                  jmp       short 0048AAC4h

LAB_0048aa5b:
  0048aa5b  83ff46                cmp       edi,46h
  0048aa5e  7d3e                  jge       short 0048AA9Eh
  0048aa60  c6464203              mov       byte [esi+42h],3
  0048aa64  66895e12              mov       [esi+12h],bx
  0048aa68  e8d3c60b00            call      00547140h
  0048aa6d  99                    cdq
  0048aa6e  b90a000000            mov       ecx,0Ah
  0048aa73  8b6c2420              mov       ebp,[esp+20h]
  0048aa77  f7f9                  idiv      ecx
  0048aa79  8bc5                  mov       eax,ebp
  0048aa7b  d1f8                  sar       eax,1
  0048aa7d  02d0                  add       dl,al
  0048aa7f  80c214                add       dl,14h
  0048aa82  885643                mov       [esi+43h],dl
  0048aa85  e8b6c60b00            call      00547140h
  0048aa8a  99                    cdq
  0048aa8b  33c2                  xor       eax,edx
  0048aa8d  2bc2                  sub       eax,edx
  0048aa8f  25ff0f0000            and       eax,0FFFh
  0048aa94  33c2                  xor       eax,edx
  0048aa96  2bc2                  sub       eax,edx
  0048aa98  66894610              mov       [esi+10h],ax
  0048aa9c  eb26                  jmp       short 0048AAC4h

LAB_0048aa9e:
  0048aa9e  8b4c2410              mov       ecx,[esp+10h]
  0048aaa2  ba64000000            mov       edx,64h
  0048aaa7  d1f9                  sar       ecx,1
  0048aaa9  83c10a                add       ecx,0Ah
  0048aaac  2bd7                  sub       edx,edi
  0048aaae  3bd1                  cmp       edx,ecx
  0048aab0  7d06                  jge       short 0048AAB8h
  0048aab2  c646420c              mov       byte [esi+42h],0Ch
  0048aab6  eb04                  jmp       short 0048AABCh

LAB_0048aab8:
  0048aab8  c6464209              mov       byte [esi+42h],9

LAB_0048aabc:
  0048aabc  c6464301              mov       byte [esi+43h],1

LAB_0048aac0:
  0048aac0  8b6c2420              mov       ebp,[esp+20h]

LAB_0048aac4:
  0048aac4  8b442418              mov       eax,[esp+18h]
  0048aac8  3d58020000            cmp       eax,258h
  0048aacd  0f8de9000000          jge       near 0048ABBCh
  0048aad3  3d96000000            cmp       eax,96h
  0048aad8  0f8cde000000          jl        near 0048ABBCh
  0048aade  e85dc60b00            call      00547140h
  0048aae3  99                    cdq
  0048aae4  b964000000            mov       ecx,64h
  0048aae9  f7f9                  idiv      ecx
  0048aaeb  83fa14                cmp       edx,14h
  0048aaee  7d32                  jge       short 0048AB22h
  0048aaf0  8b542414              mov       edx,[esp+14h]
  0048aaf4  81c200040000          add       edx,400h
  0048aafa  81e2ff0f0000          and       edx,0FFFh
  0048ab00  81fa00080000          cmp       edx,800h
  0048ab06  7c0d                  jl        short 0048AB15h
  0048ab08  c6464201              mov       byte [esi+42h],1
  0048ab0c  c6464301              mov       byte [esi+43h],1
  0048ab10  e9a7000000            jmp       0048ABBCh

LAB_0048ab15:
  0048ab15  c646420c              mov       byte [esi+42h],0Ch
  0048ab19  c6464301              mov       byte [esi+43h],1
  0048ab1d  e99a000000            jmp       0048ABBCh

LAB_0048ab22:
  0048ab22  83fa28                cmp       edx,28h
  0048ab25  7d25                  jge       short 0048AB4Ch
  0048ab27  c6464202              mov       byte [esi+42h],2
  0048ab2b  e810c60b00            call      00547140h
  0048ab30  99                    cdq
  0048ab31  b90a000000            mov       ecx,0Ah
  0048ab36  66c746105000          mov       word [esi+10h],50h
  0048ab3c  f7f9                  idiv      ecx
  0048ab3e  8bc5                  mov       eax,ebp
  0048ab40  d1f8                  sar       eax,1
  0048ab42  02d0                  add       dl,al
  0048ab44  80c214                add       dl,14h
  0048ab47  885643                mov       [esi+43h],dl
  0048ab4a  eb70                  jmp       short 0048ABBCh

LAB_0048ab4c:
  0048ab4c  83fa50                cmp       edx,50h
  0048ab4f  7d3a                  jge       short 0048AB8Bh
  0048ab51  c6464203              mov       byte [esi+42h],3
  0048ab55  66895e12              mov       [esi+12h],bx
  0048ab59  e8e2c50b00            call      00547140h
  0048ab5e  99                    cdq
  0048ab5f  b90a000000            mov       ecx,0Ah
  0048ab64  f7f9                  idiv      ecx
  0048ab66  8bc5                  mov       eax,ebp
  0048ab68  d1f8                  sar       eax,1
  0048ab6a  02d0                  add       dl,al
  0048ab6c  80c214                add       dl,14h
  0048ab6f  885643                mov       [esi+43h],dl
  0048ab72  e8c9c50b00            call      00547140h
  0048ab77  99                    cdq
  0048ab78  33c2                  xor       eax,edx
  0048ab7a  2bc2                  sub       eax,edx
  0048ab7c  25ff0f0000            and       eax,0FFFh
  0048ab81  33c2                  xor       eax,edx
  0048ab83  2bc2                  sub       eax,edx
  0048ab85  66894610              mov       [esi+10h],ax
  0048ab89  eb31                  jmp       short 0048ABBCh

LAB_0048ab8b:
  0048ab8b  8b4c2410              mov       ecx,[esp+10h]
  0048ab8f  b864000000            mov       eax,64h
  0048ab94  d1f9                  sar       ecx,1
  0048ab96  83c10a                add       ecx,0Ah
  0048ab99  2bc2                  sub       eax,edx
  0048ab9b  3bc1                  cmp       eax,ecx
  0048ab9d  7d0a                  jge       short 0048ABA9h
  0048ab9f  c6464209              mov       byte [esi+42h],9
  0048aba3  c6464301              mov       byte [esi+43h],1
  0048aba7  eb13                  jmp       short 0048ABBCh

LAB_0048aba9:
  0048aba9  885e42                mov       [esi+42h],bl
  0048abac  e88fc50b00            call      00547140h
  0048abb1  99                    cdq
  0048abb2  b928000000            mov       ecx,28h
  0048abb7  f7f9                  idiv      ecx
  0048abb9  885643                mov       [esi+43h],dl

LAB_0048abbc:
  0048abbc  817c241858020000      cmp       dword [esp+18h],258h
  0048abc4  0f8cd6000000          jl        near 0048ACA0h
  0048abca  e871c50b00            call      00547140h
  0048abcf  99                    cdq
  0048abd0  b964000000            mov       ecx,64h
  0048abd5  f7f9                  idiv      ecx
  0048abd7  83fa32                cmp       edx,32h
  0048abda  7d6f                  jge       short 0048AC4Bh
  0048abdc  8b442410              mov       eax,[esp+10h]
  0048abe0  8d4c000a              lea       ecx,[eax+eax+0Ah]
  0048abe4  3bd1                  cmp       edx,ecx
  0048abe6  7d2e                  jge       short 0048AC16h
  0048abe8  c6464202              mov       byte [esi+42h],2
  0048abec  e84fc50b00            call      00547140h
  0048abf1  99                    cdq
  0048abf2  b90a000000            mov       ecx,0Ah
  0048abf7  66c746106400          mov       word [esi+10h],64h
  0048abfd  f7f9                  idiv      ecx
  0048abff  8bc5                  mov       eax,ebp
  0048ac01  d1f8                  sar       eax,1
  0048ac03  02d0                  add       dl,al
  0048ac05  80c214                add       dl,14h
  0048ac08  885643                mov       [esi+43h],dl
  0048ac0b  5f                    pop       edi
  0048ac0c  5e                    pop       esi
  0048ac0d  5d                    pop       ebp
  0048ac0e  5b                    pop       ebx
  0048ac0f  81c488000000          add       esp,88h
  0048ac15  c3                    ret

LAB_0048ac16:
  0048ac16  885e42                mov       [esi+42h],bl
  0048ac19  e822c50b00            call      00547140h
  0048ac1e  99                    cdq
  0048ac1f  b90a000000            mov       ecx,0Ah
  0048ac24  66895e12              mov       [esi+12h],bx
  0048ac28  f7f9                  idiv      ecx
  0048ac2a  8b4c2410              mov       ecx,[esp+10h]
  0048ac2e  8bc5                  mov       eax,ebp
  0048ac30  c1f802                sar       eax,2
  0048ac33  02d0                  add       dl,al
  0048ac35  885643                mov       [esi+43h],dl
  0048ac38  8d540950              lea       edx,[ecx+ecx+50h]
  0048ac3c  66895610              mov       [esi+10h],dx
  0048ac40  5f                    pop       edi
  0048ac41  5e                    pop       esi
  0048ac42  5d                    pop       ebp
  0048ac43  5b                    pop       ebx
  0048ac44  81c488000000          add       esp,88h
  0048ac4a  c3                    ret

LAB_0048ac4b:
  0048ac4b  83fa4b                cmp       edx,4Bh
  0048ac4e  7d43                  jge       short 0048AC93h
  0048ac50  c6464203              mov       byte [esi+42h],3
  0048ac54  66895e12              mov       [esi+12h],bx
  0048ac58  e8e3c40b00            call      00547140h
  0048ac5d  99                    cdq
  0048ac5e  b90a000000            mov       ecx,0Ah
  0048ac63  f7f9                  idiv      ecx
  0048ac65  8bc5                  mov       eax,ebp
  0048ac67  d1f8                  sar       eax,1
  0048ac69  02d0                  add       dl,al
  0048ac6b  80c214                add       dl,14h
  0048ac6e  885643                mov       [esi+43h],dl
  0048ac71  e8cac40b00            call      00547140h
  0048ac76  99                    cdq
  0048ac77  33c2                  xor       eax,edx
  0048ac79  2bc2                  sub       eax,edx
  0048ac7b  25ff0f0000            and       eax,0FFFh
  0048ac80  33c2                  xor       eax,edx
  0048ac82  2bc2                  sub       eax,edx
  0048ac84  66894610              mov       [esi+10h],ax
  0048ac88  5f                    pop       edi
  0048ac89  5e                    pop       esi
  0048ac8a  5d                    pop       ebp
  0048ac8b  5b                    pop       ebx
  0048ac8c  81c488000000          add       esp,88h
  0048ac92  c3                    ret

LAB_0048ac93:
  0048ac93  83fa3c                cmp       edx,3Ch
  0048ac96  7d08                  jge       short 0048ACA0h
  0048ac98  c6464201              mov       byte [esi+42h],1
  0048ac9c  c6464301              mov       byte [esi+43h],1

LAB_0048aca0:
  0048aca0  5f                    pop       edi
  0048aca1  5e                    pop       esi
  0048aca2  5d                    pop       ebp
  0048aca3  5b                    pop       ebx
  0048aca4  81c488000000          add       esp,88h
  0048acaa  c3                    ret

LAB_0048acab:
  0048acab  66837e1a02            cmp       word [esi+1Ah],2
  0048acb0  0f8542f2ffff          jne       near 00489EF8h
  0048acb6  8d4c242c              lea       ecx,[esp+2Ch]
  0048acba  6a00                  push      0
  0048acbc  51                    push      ecx
  0048acbd  57                    push      edi
  0048acbe  56                    push      esi
  0048acbf  e86cef0800            call      00519C30h
  0048acc4  8be8                  mov       ebp,eax
  0048acc6  a1b8e05d00            mov       eax,[5DE0B8h]
  0048accb  83c410                add       esp,10h
  0048acce  85c0                  test      eax,eax
  0048acd0  0f841ef2ffff          je        near 00489EF4h
  0048acd6  66837e1a02            cmp       word [esi+1Ah],2
  0048acdb  0f8513f2ffff          jne       near 00489EF4h
  0048ace1  8b5d00                mov       ebx,[ebp]
  0048ace4  66c785960000000400    mov       word [ebp+96h],4
  0048aced  83cb40                or        ebx,40h
  0048acf0  55                    push      ebp
  0048acf1  895d00                mov       [ebp],ebx
  0048acf4  8b4c242c              mov       ecx,[esp+2Ch]
  0048acf8  56                    push      esi
  0048acf9  668b4130              mov       ax,[ecx+30h]
  0048acfd  0fbfd8                movsx     ebx,ax
  0048ad00  66c1f802              sar       ax,2
  0048ad04  66894130              mov       [ecx+30h],ax
  0048ad08  e873ee0a00            call      00539B80h
  0048ad0d  8b542430              mov       edx,[esp+30h]
  0048ad11  83c408                add       esp,8
  0048ad14  83c51c                add       ebp,1Ch
  0048ad17  55                    push      ebp
  0048ad18  6800180000            push      1800h
  0048ad1d  6800030000            push      300h
  0048ad22  6a03                  push      3
  0048ad24  66895a30              mov       [edx+30h],bx
  0048ad28  e8f32d0b00            call      0053DB20h
  0048ad2d  8b44242c              mov       eax,[esp+2Ch]
  0048ad31  8b5c2434              mov       ebx,[esp+34h]
  0048ad35  83c410                add       esp,10h
  0048ad38  e9bbf1ffff            jmp       00489EF8h
  0048ad3d  90                    nop
  0048ad3e  8bff                  mov       edi,edi
  0048ad40  0d9f48005a            or        eax,5A00489Fh
  0048ad45  9f                    lahf
  0048ad46  48                    dec       eax
  0048ad47  00d5                  add       ch,dl
  0048ad49  9f                    lahf
  0048ad4a  48                    dec       eax
  0048ad4b  007ea0                add       [esi-60h],bh
  0048ad4e  48                    dec       eax
  0048ad4f  008aa648008a          add       [edx-75FFB75Ah],cl
  0048ad55  a6                    cmpsb
  0048ad56  48                    dec       eax
  0048ad57  008aa6480052          add       [edx+520048A6h],cl
  0048ad5d  a148008aa6            mov       eax,[0A68A0048h]
  0048ad62  48                    dec       eax
  0048ad63  00b3a148008a          add       [ebx-75FFB75Fh],dh
  0048ad69  a6                    cmpsb
  0048ad6a  48                    dec       eax
  0048ad6b  003ca2                add       [edx],bh
  0048ad6e  48                    dec       eax
  0048ad6f  00a2a248000b          add       [edx+0B0048A2h],ah
  0048ad75  a4                    movsb
  0048ad76  48                    dec       eax
  0048ad77  009090909090          add       [eax-6F6F6F70h],dl
  0048ad7d  90                    nop
  0048ad7e  90                    nop
  0048ad7f  90                    nop

90 cible(s) de saut interne : 0x00489d0e, 0x00489d12, 0x00489d14, 0x00489e58, 0x00489e7a, 0x00489e7e, 0x00489edd, 0x00489ef4, 0x00489ef8, 0x00489f2f, 0x00489f42, 0x00489f79, 0x00489f9d, 0x00489fbe, 0x0048a00b, 0x0048a02b, 0x0048a037, 0x0048a06c, 0x0048a095, 0x0048a113, 0x0048a121, 0x0048a125, 0x0048a170, 0x0048a17c, 0x0048a1ab, 0x0048a1ef, 0x0048a25b, 0x0048a285, 0x0048a2ec, 0x0048a314, 0x0048a31e, 0x0048a322, 0x0048a35a, 0x0048a397, 0x0048a3a1, 0x0048a3a5, 0x0048a3b7, 0x0048a3bc, 0x0048a3e3, 0x0048a426, 0x0048a43c, 0x0048a45b, 0x0048a515, 0x0048a535, 0x0048a55e, 0x0048a58f, 0x0048a59d, 0x0048a5a1, 0x0048a5af, 0x0048a60a, 0x0048a63b, 0x0048a649, 0x0048a64d, 0x0048a659, 0x0048a687, 0x0048a68a, 0x0048a6dd, 0x0048a6f2, 0x0048a6fa, 0x0048a6ff, 0x0048a744, 0x0048a804, 0x0048a827, 0x0048a829, 0x0048a862, 0x0048a8eb, 0x0048a8f9, 0x0048a927, 0x0048aa02, 0x0048aa12, 0x0048aa2a, 0x0048aa3a, 0x0048aa51, 0x0048aa5b, 0x0048aa9e, 0x0048aab8, 0x0048aabc, 0x0048aac0, 0x0048aac4, 0x0048ab15, 0x0048ab22, 0x0048ab4c, 0x0048ab8b, 0x0048aba9, 0x0048abbc, 0x0048ac16, 0x0048ac4b, 0x0048ac93, 0x0048aca0, 0x0048acab
