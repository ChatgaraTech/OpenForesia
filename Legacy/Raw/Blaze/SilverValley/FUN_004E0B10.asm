Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e0b10  section .text  offset fichier 0xdff10  2160 octets

  004e0b10  83ec2c                sub       esp,2Ch
  004e0b13  53                    push      ebx
  004e0b14  55                    push      ebp
  004e0b15  56                    push      esi
  004e0b16  8b74243c              mov       esi,[esp+3Ch]
  004e0b1a  8d44241c              lea       eax,[esp+1Ch]
  004e0b1e  57                    push      edi
  004e0b1f  8d4c2438              lea       ecx,[esp+38h]
  004e0b23  50                    push      eax
  004e0b24  8d542438              lea       edx,[esp+38h]
  004e0b28  51                    push      ecx
  004e0b29  52                    push      edx
  004e0b2a  56                    push      esi
  004e0b2b  e8507e0500            call      00538980h
  004e0b30  83c410                add       esp,10h
  004e0b33  8d442424              lea       eax,[esp+24h]
  004e0b37  8d4c241c              lea       ecx,[esp+1Ch]
  004e0b3b  8d542440              lea       edx,[esp+40h]
  004e0b3f  50                    push      eax
  004e0b40  51                    push      ecx
  004e0b41  8d44241c              lea       eax,[esp+1Ch]
  004e0b45  52                    push      edx
  004e0b46  8d4c241c              lea       ecx,[esp+1Ch]
  004e0b4a  50                    push      eax
  004e0b4b  51                    push      ecx
  004e0b4c  56                    push      esi
  004e0b4d  e8ce530500            call      00535F20h
  004e0b52  8b4c2428              mov       ecx,[esp+28h]
  004e0b56  8b442434              mov       eax,[esp+34h]
  004e0b5a  bb40000000            mov       ebx,40h
  004e0b5f  83c418                add       esp,18h
  004e0b62  2bd9                  sub       ebx,ecx
  004e0b64  bf01000000            mov       edi,1
  004e0b69  85c0                  test      eax,eax
  004e0b6b  895c2418              mov       [esp+18h],ebx
  004e0b6f  0f8403020000          je        near 004E0D78h
  004e0b75  56                    push      esi
  004e0b76  e825aaffff            call      004DB5A0h
  004e0b7b  8b06                  mov       eax,[esi]
  004e0b7d  83c404                add       esp,4
  004e0b80  a802                  test      al,2
  004e0b82  0f84f3050000          je        near 004E117Bh
  004e0b88  f644242410            test      byte [esp+24h],10h
  004e0b8d  740d                  je        short 004E0B9Ch
  004e0b8f  24fd                  and       al,0FDh
  004e0b91  c6464300              mov       byte [esi+43h],0
  004e0b95  8906                  mov       [esi],eax
  004e0b97  e9dc010000            jmp       004E0D78h

LAB_004e0b9c:
  004e0b9c  33c9                  xor       ecx,ecx
  004e0b9e  8a4e42                mov       cl,[esi+42h]
  004e0ba1  83e900                sub       ecx,0
  004e0ba4  0f84c1040000          je        near 004E106Bh
  004e0baa  49                    dec       ecx
  004e0bab  0f85c7010000          jne       near 004E0D78h
  004e0bb1  668b5610              mov       dx,[esi+10h]
  004e0bb5  0fbfca                movsx     ecx,dx
  004e0bb8  83e900                sub       ecx,0
  004e0bbb  0f8490000000          je        near 004E0C51h
  004e0bc1  49                    dec       ecx
  004e0bc2  742f                  je        short 004E0BF3h
  004e0bc4  49                    dec       ecx
  004e0bc5  0f85ad010000          jne       near 004E0D78h
  004e0bcb  8b465c                mov       eax,[esi+5Ch]
  004e0bce  8bc8                  mov       ecx,eax
  004e0bd0  c1f904                sar       ecx,4
  004e0bd3  2bc1                  sub       eax,ecx
  004e0bd5  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004e0bda  89465c                mov       [esi+5Ch],eax
  004e0bdd  7505                  jne       short 004E0BE4h
  004e0bdf  4a                    dec       edx
  004e0be0  66895610              mov       [esi+10h],dx

LAB_004e0be4:
  004e0be4  8b442410              mov       eax,[esp+10h]
  004e0be8  c1f802                sar       eax,2
  004e0beb  83c020                add       eax,20h
  004e0bee  e944070000            jmp       004E1337h

LAB_004e0bf3:
  004e0bf3  8b442410              mov       eax,[esp+10h]
  004e0bf7  0fbf4e32              movsx     ecx,word [esi+32h]
  004e0bfb  0540040000            add       eax,440h
  004e0c00  51                    push      ecx
  004e0c01  c1e007                shl       eax,7
  004e0c04  89465c                mov       [esi+5Ch],eax
  004e0c07  e8104e0600            call      00545A1Ch
  004e0c0c  0faf465c              imul      eax,[esi+5Ch]
  004e0c10  0fbf5632              movsx     edx,word [esi+32h]
  004e0c14  8b4e50                mov       ecx,[esi+50h]
  004e0c17  52                    push      edx
  004e0c18  c1f80c                sar       eax,0Ch
  004e0c1b  03c8                  add       ecx,eax
  004e0c1d  894e50                mov       [esi+50h],ecx
  004e0c20  e8f14d0600            call      00545A16h
  004e0c25  0faf465c              imul      eax,[esi+5Ch]
  004e0c29  8b5658                mov       edx,[esi+58h]
  004e0c2c  8b0e                  mov       ecx,[esi]
  004e0c2e  c1f80c                sar       eax,0Ch
  004e0c31  03d0                  add       edx,eax
  004e0c33  0bcf                  or        ecx,edi
  004e0c35  66ff4e10              dec       word [esi+10h]
  004e0c39  895658                mov       [esi+58h],edx
  004e0c3c  c7465400400200        mov       dword [esi+54h],24000h
  004e0c43  890e                  mov       [esi],ecx
  004e0c45  c7465c00000000        mov       dword [esi+5Ch],0
  004e0c4c  e927010000            jmp       004E0D78h

LAB_004e0c51:
  004e0c51  a900000002            test      eax,2000000h
  004e0c56  7408                  je        short 004E0C60h
  004e0c58  a801                  test      al,1
  004e0c5a  7504                  jne       short 004E0C60h
  004e0c5c  24dd                  and       al,0DDh
  004e0c5e  8906                  mov       [esi],eax

LAB_004e0c60:
  004e0c60  f60620                test      byte [esi],20h
  004e0c63  0f850f010000          jne       near 004E0D78h
  004e0c69  8d6e1c                lea       ebp,[esi+1Ch]
  004e0c6c  6a00                  push      0
  004e0c6e  68cc2d5500            push      552DCCh
  004e0c73  55                    push      ebp
  004e0c74  56                    push      esi
  004e0c75  e8b68f0300            call      00519C30h
  004e0c7a  8bf8                  mov       edi,eax
  004e0c7c  83c410                add       esp,10h
  004e0c7f  85ff                  test      edi,edi
  004e0c81  7406                  je        short 004E0C89h
  004e0c83  8b06                  mov       eax,[esi]
  004e0c85  0c20                  or        al,20h
  004e0c87  8906                  mov       [esi],eax

LAB_004e0c89:
  004e0c89  a1b8e05d00            mov       eax,[5DE0B8h]
  004e0c8e  85c0                  test      eax,eax
  004e0c90  0f84e2000000          je        near 004E0D78h
  004e0c96  8b1f                  mov       ebx,[edi]
  004e0c98  66c787960000001000    mov       word [edi+96h],10h
  004e0ca1  83cb40                or        ebx,40h
  004e0ca4  57                    push      edi
  004e0ca5  891f                  mov       [edi],ebx
  004e0ca7  8b4c2424              mov       ecx,[esp+24h]
  004e0cab  56                    push      esi
  004e0cac  668b4130              mov       ax,[ecx+30h]
  004e0cb0  0fbfd8                movsx     ebx,ax
  004e0cb3  66d1f8                sar       ax,1
  004e0cb6  66894130              mov       [ecx+30h],ax
  004e0cba  e8c18e0500            call      00539B80h
  004e0cbf  8b442428              mov       eax,[esp+28h]
  004e0cc3  83c408                add       esp,8
  004e0cc6  66895830              mov       [eax+30h],bx
  004e0cca  8d5f1c                lea       ebx,[edi+1Ch]
  004e0ccd  53                    push      ebx
  004e0cce  6800180000            push      1800h
  004e0cd3  6800030000            push      300h
  004e0cd8  6a03                  push      3
  004e0cda  e841ce0500            call      0053DB20h
  004e0cdf  8b07                  mov       eax,[edi]
  004e0ce1  83c410                add       esp,10h
  004e0ce4  a900000040            test      eax,40000000h
  004e0ce9  0f8585000000          jne       near 004E0D74h
  004e0cef  0fbf4e32              movsx     ecx,word [esi+32h]
  004e0cf3  51                    push      ecx
  004e0cf4  e8234d0600            call      00545A1Ch
  004e0cf9  8d0440                lea       eax,[eax+eax*2]
  004e0cfc  8d1480                lea       edx,[eax+eax*4]
  004e0cff  d1e2                  shl       edx,1
  004e0d01  895750                mov       [edi+50h],edx
  004e0d04  0fbf4632              movsx     eax,word [esi+32h]
  004e0d08  50                    push      eax
  004e0d09  e8084d0600            call      00545A16h
  004e0d0e  8d0440                lea       eax,[eax+eax*2]
  004e0d11  8d0c80                lea       ecx,[eax+eax*4]
  004e0d14  8b07                  mov       eax,[edi]
  004e0d16  d1e1                  shl       ecx,1
  004e0d18  a801                  test      al,1
  004e0d1a  894f58                mov       [edi+58h],ecx
  004e0d1d  750b                  jne       short 004E0D2Ah
  004e0d1f  0c01                  or        al,1
  004e0d21  c7475400800100        mov       dword [edi+54h],18000h
  004e0d28  8907                  mov       [edi],eax

LAB_004e0d2a:
  004e0d2a  8b5500                mov       edx,[ebp]
  004e0d2d  8b4620                mov       eax,[esi+20h]
  004e0d30  8b4e24                mov       ecx,[esi+24h]
  004e0d33  89542428              mov       [esp+28h],edx
  004e0d37  8b13                  mov       edx,[ebx]
  004e0d39  8944242c              mov       [esp+2Ch],eax
  004e0d3d  895500                mov       [ebp],edx
  004e0d40  8b4720                mov       eax,[edi+20h]
  004e0d43  6aff                  push      0FFFFFFFFh
  004e0d45  894c2434              mov       [esp+34h],ecx
  004e0d49  894620                mov       [esi+20h],eax
  004e0d4c  8b4f24                mov       ecx,[edi+24h]
  004e0d4f  6a00                  push      0
  004e0d51  6a39                  push      39h
  004e0d53  56                    push      esi
  004e0d54  894e24                mov       [esi+24h],ecx
  004e0d57  e8749cf9ff            call      0047A9D0h
  004e0d5c  8b542438              mov       edx,[esp+38h]
  004e0d60  8b44243c              mov       eax,[esp+3Ch]
  004e0d64  8b4c2440              mov       ecx,[esp+40h]
  004e0d68  83c410                add       esp,10h
  004e0d6b  895500                mov       [ebp],edx
  004e0d6e  894620                mov       [esi+20h],eax
  004e0d71  894e24                mov       [esi+24h],ecx

LAB_004e0d74:
  004e0d74  8b5c2418              mov       ebx,[esp+18h]

LAB_004e0d78:
  004e0d78  8b44241c              mov       eax,[esp+1Ch]
  004e0d7c  66c7869a0000000060    mov       word [esi+9Ah],6000h
  004e0d85  50                    push      eax
  004e0d86  56                    push      esi
  004e0d87  e8d473faff            call      00488160h
  004e0d8c  83c408                add       esp,8
  004e0d8f  56                    push      esi
  004e0d90  e8ab2b0500            call      00533940h
  004e0d95  83c404                add       esp,4
  004e0d98  56                    push      esi
  004e0d99  e852edffff            call      004DFAF0h
  004e0d9e  8b4c2438              mov       ecx,[esp+38h]
  004e0da2  83c404                add       esp,4
  004e0da5  51                    push      ecx
  004e0da6  56                    push      esi
  004e0da7  e8c4290000            call      004E3770h
  004e0dac  8b442424              mov       eax,[esp+24h]
  004e0db0  83c408                add       esp,8
  004e0db3  85c0                  test      eax,eax
  004e0db5  0f84a8020000          je        near 004E1063h
  004e0dbb  f70617080000          test      dword [esi],817h
  004e0dc1  0f859c020000          jne       near 004E1063h
  004e0dc7  8a4643                mov       al,[esi+43h]
  004e0dca  84c0                  test      al,al
  004e0dcc  0f8591020000          jne       near 004E1063h
  004e0dd2  8b466c                mov       eax,[esi+6Ch]
  004e0dd5  8b561c                mov       edx,[esi+1Ch]
  004e0dd8  8b781c                mov       edi,[eax+1Ch]
  004e0ddb  8b6824                mov       ebp,[eax+24h]
  004e0dde  8b4624                mov       eax,[esi+24h]
  004e0de1  2bfa                  sub       edi,edx
  004e0de3  2be8                  sub       ebp,eax
  004e0de5  c1ff0c                sar       edi,0Ch
  004e0de8  c1fd0c                sar       ebp,0Ch
  004e0deb  8bd7                  mov       edx,edi
  004e0ded  8bc5                  mov       eax,ebp
  004e0def  0fafd7                imul      edx,edi
  004e0df2  0fafc5                imul      eax,ebp
  004e0df5  03d0                  add       edx,eax
  004e0df7  52                    push      edx
  004e0df8  e8134c0600            call      00545A10h
  004e0dfd  55                    push      ebp
  004e0dfe  57                    push      edi
  004e0dff  8944241c              mov       [esp+1Ch],eax
  004e0e03  e81a4c0600            call      00545A22h
  004e0e08  668b4e32              mov       cx,[esi+32h]
  004e0e0c  2bc1                  sub       eax,ecx
  004e0e0e  25ff0f0000            and       eax,0FFFh
  004e0e13  89442440              mov       [esp+40h],eax
  004e0e17  8b442414              mov       eax,[esp+14h]
  004e0e1b  3dfa000000            cmp       eax,0FAh
  004e0e20  0f8dfe000000          jge       near 004E0F24h
  004e0e26  e815630600            call      00547140h
  004e0e2b  99                    cdq
  004e0e2c  b964000000            mov       ecx,64h
  004e0e31  f7f9                  idiv      ecx
  004e0e33  83fa32                cmp       edx,32h
  004e0e36  7d61                  jge       short 004E0E99h
  004e0e38  8b542440              mov       edx,[esp+40h]
  004e0e3c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004e0e43  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004e0e4a  25ff0f0000            and       eax,0FFFh
  004e0e4f  3bc1                  cmp       eax,ecx
  004e0e51  0f8f02010000          jg        near 004E0F59h
  004e0e57  e8e4620600            call      00547140h
  004e0e5c  99                    cdq
  004e0e5d  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004e0e61  f7f9                  idiv      ecx
  004e0e63  8b442414              mov       eax,[esp+14h]
  004e0e67  83c264                add       edx,64h
  004e0e6a  3bc2                  cmp       eax,edx
  004e0e6c  0f8de7000000          jge       near 004E0F59h
  004e0e72  8b16                  mov       edx,[esi]
  004e0e74  c7461803000000        mov       dword [esi+18h],3
  004e0e7b  81e2fffffffe          and       edx,0FEFFFFFFh
  004e0e81  c6464200              mov       byte [esi+42h],0
  004e0e85  83ca02                or        edx,2
  004e0e88  c7465c00000000        mov       dword [esi+5Ch],0
  004e0e8f  8916                  mov       [esi],edx
  004e0e91  5f                    pop       edi
  004e0e92  5e                    pop       esi
  004e0e93  5d                    pop       ebp
  004e0e94  5b                    pop       ebx
  004e0e95  83c42c                add       esp,2Ch
  004e0e98  c3                    ret

LAB_004e0e99:
  004e0e99  83fa4b                cmp       edx,4Bh
  004e0e9c  0f8c3f010000          jl        near 004E0FE1h
  004e0ea2  83fa5a                cmp       edx,5Ah
  004e0ea5  7d2d                  jge       short 004E0ED4h
  004e0ea7  8b542440              mov       edx,[esp+40h]
  004e0eab  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004e0eb2  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004e0eb9  25ff0f0000            and       eax,0FFFh
  004e0ebe  3bc1                  cmp       eax,ecx
  004e0ec0  7f12                  jg        short 004E0ED4h
  004e0ec2  8b16                  mov       edx,[esi]
  004e0ec4  81e2fffffffe          and       edx,0FEFFFFFFh
  004e0eca  83ca02                or        edx,2
  004e0ecd  8916                  mov       [esi],edx
  004e0ecf  e9a0000000            jmp       004E0F74h

LAB_004e0ed4:
  004e0ed4  c6464203              mov       byte [esi+42h],3
  004e0ed8  e863620600            call      00547140h
  004e0edd  99                    cdq
  004e0ede  b90a000000            mov       ecx,0Ah
  004e0ee3  f7f9                  idiv      ecx
  004e0ee5  d1fb                  sar       ebx,1
  004e0ee7  02d3                  add       dl,bl
  004e0ee9  80c214                add       dl,14h
  004e0eec  885643                mov       [esi+43h],dl
  004e0eef  8b542410              mov       edx,[esp+10h]
  004e0ef3  8dba80000000          lea       edi,[edx+80h]
  004e0ef9  c1e704                shl       edi,4
  004e0efc  e83f620600            call      00547140h
  004e0f01  99                    cdq
  004e0f02  f7ff                  idiv      edi
  004e0f04  d1ff                  sar       edi,1
  004e0f06  2bd7                  sub       edx,edi
  004e0f08  8b7c2440              mov       edi,[esp+40h]
  004e0f0c  03d7                  add       edx,edi
  004e0f0e  66035632              add       dx,[esi+32h]
  004e0f12  81c200080000          add       edx,800h
  004e0f18  66895610              mov       [esi+10h],dx
  004e0f1c  5f                    pop       edi
  004e0f1d  5e                    pop       esi
  004e0f1e  5d                    pop       ebp
  004e0f1f  5b                    pop       ebx
  004e0f20  83c42c                add       esp,2Ch
  004e0f23  c3                    ret

LAB_004e0f24:
  004e0f24  3df4010000            cmp       eax,1F4h
  004e0f29  0f8ddb000000          jge       near 004E100Ah
  004e0f2f  e80c620600            call      00547140h
  004e0f34  99                    cdq
  004e0f35  b964000000            mov       ecx,64h
  004e0f3a  f7f9                  idiv      ecx
  004e0f3c  83fa2d                cmp       edx,2Dh
  004e0f3f  7d4c                  jge       short 004E0F8Dh
  004e0f41  8b542440              mov       edx,[esp+40h]
  004e0f45  81c200040000          add       edx,400h
  004e0f4b  81e2ff0f0000          and       edx,0FFFh
  004e0f51  81fa00080000          cmp       edx,800h
  004e0f57  7c10                  jl        short 004E0F69h

LAB_004e0f59:
  004e0f59  c6464201              mov       byte [esi+42h],1
  004e0f5d  c6464301              mov       byte [esi+43h],1
  004e0f61  5f                    pop       edi
  004e0f62  5e                    pop       esi
  004e0f63  5d                    pop       ebp
  004e0f64  5b                    pop       ebx
  004e0f65  83c42c                add       esp,2Ch
  004e0f68  c3                    ret

LAB_004e0f69:
  004e0f69  8b06                  mov       eax,[esi]
  004e0f6b  25fffffffe            and       eax,0FEFFFFFFh
  004e0f70  0c02                  or        al,2
  004e0f72  8906                  mov       [esi],eax

LAB_004e0f74:
  004e0f74  c7461804000000        mov       dword [esi+18h],4
  004e0f7b  c6464201              mov       byte [esi+42h],1
  004e0f7f  66c746100200          mov       word [esi+10h],2
  004e0f85  5f                    pop       edi
  004e0f86  5e                    pop       esi
  004e0f87  5d                    pop       ebp
  004e0f88  5b                    pop       ebx
  004e0f89  83c42c                add       esp,2Ch
  004e0f8c  c3                    ret

LAB_004e0f8d:
  004e0f8d  83fa46                cmp       edx,46h
  004e0f90  7d4a                  jge       short 004E0FDCh
  004e0f92  c6464203              mov       byte [esi+42h],3
  004e0f96  e8a5610600            call      00547140h
  004e0f9b  99                    cdq
  004e0f9c  b90a000000            mov       ecx,0Ah
  004e0fa1  f7f9                  idiv      ecx
  004e0fa3  d1fb                  sar       ebx,1
  004e0fa5  02d3                  add       dl,bl
  004e0fa7  80c214                add       dl,14h
  004e0faa  885643                mov       [esi+43h],dl
  004e0fad  8b542410              mov       edx,[esp+10h]
  004e0fb1  8dba80000000          lea       edi,[edx+80h]
  004e0fb7  c1e704                shl       edi,4
  004e0fba  e881610600            call      00547140h
  004e0fbf  99                    cdq
  004e0fc0  f7ff                  idiv      edi
  004e0fc2  8b4c2440              mov       ecx,[esp+40h]
  004e0fc6  d1ff                  sar       edi,1
  004e0fc8  2bd7                  sub       edx,edi
  004e0fca  03d1                  add       edx,ecx
  004e0fcc  66035632              add       dx,[esi+32h]
  004e0fd0  66895610              mov       [esi+10h],dx
  004e0fd4  5f                    pop       edi
  004e0fd5  5e                    pop       esi
  004e0fd6  5d                    pop       ebp
  004e0fd7  5b                    pop       ebx
  004e0fd8  83c42c                add       esp,2Ch
  004e0fdb  c3                    ret

LAB_004e0fdc:
  004e0fdc  83fa5a                cmp       edx,5Ah
  004e0fdf  7d3b                  jge       short 004E101Ch

LAB_004e0fe1:
  004e0fe1  c6464202              mov       byte [esi+42h],2
  004e0fe5  e856610600            call      00547140h
  004e0fea  99                    cdq
  004e0feb  b90a000000            mov       ecx,0Ah
  004e0ff0  66c746106400          mov       word [esi+10h],64h
  004e0ff6  f7f9                  idiv      ecx
  004e0ff8  d1fb                  sar       ebx,1
  004e0ffa  02d3                  add       dl,bl
  004e0ffc  80c214                add       dl,14h
  004e0fff  885643                mov       [esi+43h],dl
  004e1002  5f                    pop       edi
  004e1003  5e                    pop       esi
  004e1004  5d                    pop       ebp
  004e1005  5b                    pop       ebx
  004e1006  83c42c                add       esp,2Ch
  004e1009  c3                    ret

LAB_004e100a:
  004e100a  e831610600            call      00547140h
  004e100f  99                    cdq
  004e1010  b964000000            mov       ecx,64h
  004e1015  f7f9                  idiv      ecx
  004e1017  83fa32                cmp       edx,32h
  004e101a  7d23                  jge       short 004E103Fh

LAB_004e101c:
  004e101c  c6464200              mov       byte [esi+42h],0
  004e1020  e81b610600            call      00547140h
  004e1025  99                    cdq
  004e1026  b90a000000            mov       ecx,0Ah
  004e102b  f7f9                  idiv      ecx
  004e102d  d1fb                  sar       ebx,1
  004e102f  02d3                  add       dl,bl
  004e1031  80c214                add       dl,14h
  004e1034  885643                mov       [esi+43h],dl
  004e1037  5f                    pop       edi
  004e1038  5e                    pop       esi
  004e1039  5d                    pop       ebp
  004e103a  5b                    pop       ebx
  004e103b  83c42c                add       esp,2Ch
  004e103e  c3                    ret

LAB_004e103f:
  004e103f  c6464203              mov       byte [esi+42h],3
  004e1043  e8f8600600            call      00547140h
  004e1048  99                    cdq
  004e1049  b90a000000            mov       ecx,0Ah
  004e104e  f7f9                  idiv      ecx
  004e1050  d1fb                  sar       ebx,1
  004e1052  02d3                  add       dl,bl
  004e1054  80c214                add       dl,14h
  004e1057  885643                mov       [esi+43h],dl
  004e105a  e8e1600600            call      00547140h
  004e105f  66894610              mov       [esi+10h],ax

LAB_004e1063:
  004e1063  5f                    pop       edi
  004e1064  5e                    pop       esi
  004e1065  5d                    pop       ebp
  004e1066  5b                    pop       ebx
  004e1067  83c42c                add       esp,2Ch
  004e106a  c3                    ret

LAB_004e106b:
  004e106b  668b561a              mov       dx,[esi+1Ah]
  004e106f  8b7c2410              mov       edi,[esp+10h]
  004e1073  6683fa09              cmp       dx,9
  004e1077  7d39                  jge       short 004E10B2h
  004e1079  8b4c2440              mov       ecx,[esp+40h]
  004e107d  8bc7                  mov       eax,edi
  004e107f  d1f8                  sar       eax,1
  004e1081  83c020                add       eax,20h
  004e1084  8d1c08                lea       ebx,[eax+ecx]
  004e1087  8d2c00                lea       ebp,[eax+eax]
  004e108a  81e3ff0f0000          and       ebx,0FFFh
  004e1090  3bdd                  cmp       ebx,ebp
  004e1092  7f06                  jg        short 004E109Ah
  004e1094  66014e32              add       [esi+32h],cx
  004e1098  eb14                  jmp       short 004E10AEh

LAB_004e109a:
  004e109a  81e1ff0f0000          and       ecx,0FFFh
  004e10a0  81f900080000          cmp       ecx,800h
  004e10a6  7c02                  jl        short 004E10AAh
  004e10a8  f7d8                  neg       eax

LAB_004e10aa:
  004e10aa  66014632              add       [esi+32h],ax

LAB_004e10ae:
  004e10ae  8b5c2418              mov       ebx,[esp+18h]

LAB_004e10b2:
  004e10b2  6683fa09              cmp       dx,9
  004e10b6  7545                  jne       short 004E10FDh
  004e10b8  0fbf5632              movsx     edx,word [esi+32h]
  004e10bc  81c700030000          add       edi,300h
  004e10c2  52                    push      edx
  004e10c3  c1e707                shl       edi,7
  004e10c6  897e5c                mov       [esi+5Ch],edi
  004e10c9  e84e490600            call      00545A1Ch
  004e10ce  0faf465c              imul      eax,[esi+5Ch]
  004e10d2  8b6e50                mov       ebp,[esi+50h]
  004e10d5  c1f80c                sar       eax,0Ch
  004e10d8  03e8                  add       ebp,eax
  004e10da  0fbf4632              movsx     eax,word [esi+32h]
  004e10de  50                    push      eax
  004e10df  896e50                mov       [esi+50h],ebp
  004e10e2  e82f490600            call      00545A16h
  004e10e7  0faf465c              imul      eax,[esi+5Ch]
  004e10eb  8b4e58                mov       ecx,[esi+58h]
  004e10ee  c7465c00000000        mov       dword [esi+5Ch],0
  004e10f5  c1f80c                sar       eax,0Ch
  004e10f8  03c8                  add       ecx,eax
  004e10fa  894e58                mov       [esi+58h],ecx

LAB_004e10fd:
  004e10fd  668b461a              mov       ax,[esi+1Ah]
  004e1101  663d0a00              cmp       ax,0Ah
  004e1105  7c57                  jl        short 004E115Eh
  004e1107  663d0d00              cmp       ax,0Dh
  004e110b  7f51                  jg        short 004E115Eh
  004e110d  6a00                  push      0
  004e110f  8d4e1c                lea       ecx,[esi+1Ch]
  004e1112  68c02d5500            push      552DC0h
  004e1117  51                    push      ecx
  004e1118  56                    push      esi
  004e1119  e8128b0300            call      00519C30h
  004e111e  8bf8                  mov       edi,eax
  004e1120  a1b8e05d00            mov       eax,[5DE0B8h]
  004e1125  83c410                add       esp,10h
  004e1128  85c0                  test      eax,eax
  004e112a  7432                  je        short 004E115Eh
  004e112c  8b2f                  mov       ebp,[edi]
  004e112e  57                    push      edi
  004e112f  83cd40                or        ebp,40h
  004e1132  56                    push      esi
  004e1133  66c787960000001000    mov       word [edi+96h],10h
  004e113c  892f                  mov       [edi],ebp
  004e113e  e83d8a0500            call      00539B80h
  004e1143  83c408                add       esp,8
  004e1146  83c71c                add       edi,1Ch
  004e1149  57                    push      edi
  004e114a  6800180000            push      1800h
  004e114f  6800030000            push      300h
  004e1154  6a03                  push      3
  004e1156  e8c5c90500            call      0053DB20h
  004e115b  83c410                add       esp,10h

LAB_004e115e:
  004e115e  8b06                  mov       eax,[esi]
  004e1160  a900000002            test      eax,2000000h
  004e1165  0f84c8000000          je        near 004E1233h
  004e116b  24fd                  and       al,0FDh
  004e116d  c7465c00000000        mov       dword [esi+5Ch],0
  004e1174  8906                  mov       [esi],eax
  004e1176  e9fdfbffff            jmp       004E0D78h

LAB_004e117b:
  004e117b  a811                  test      al,11h
  004e117d  0f85f5fbffff          jne       near 004E0D78h
  004e1183  a904080000            test      eax,804h
  004e1188  0f85eafbffff          jne       near 004E0D78h
  004e118e  33c9                  xor       ecx,ecx
  004e1190  8a4e42                mov       cl,[esi+42h]
  004e1193  83f903                cmp       ecx,3
  004e1196  0f87dcfbffff          ja        near 004E0D78h
  004e119c  ff248d70134e00        jmp       dword [ecx*4+4E1370h]
  004e11a3  33c9                  xor       ecx,ecx
  004e11a5  66394e18              cmp       [esi+18h],cx
  004e11a9  740a                  je        short 004E11B5h
  004e11ab  0d00000001            or        eax,1000000h
  004e11b0  894e18                mov       [esi+18h],ecx
  004e11b3  8906                  mov       [esi],eax

LAB_004e11b5:
  004e11b5  817c24142c010000      cmp       dword [esp+14h],12Ch
  004e11bd  7d09                  jge       short 004E11C8h
  004e11bf  c6464300              mov       byte [esi+43h],0
  004e11c3  e9b0fbffff            jmp       004E0D78h

LAB_004e11c8:
  004e11c8  8a4643                mov       al,[esi+43h]
  004e11cb  84c0                  test      al,al
  004e11cd  7405                  je        short 004E11D4h
  004e11cf  fec8                  dec       al
  004e11d1  884643                mov       [esi+43h],al

LAB_004e11d4:
  004e11d4  894e5c                mov       [esi+5Ch],ecx
  004e11d7  e99cfbffff            jmp       004E0D78h
  004e11dc  66397e18              cmp       [esi+18h],di
  004e11e0  740a                  je        short 004E11ECh
  004e11e2  0d00000001            or        eax,1000000h
  004e11e7  897e18                mov       [esi+18h],edi
  004e11ea  8906                  mov       [esi],eax

LAB_004e11ec:
  004e11ec  8b542410              mov       edx,[esp+10h]
  004e11f0  8b4c2440              mov       ecx,[esp+40h]
  004e11f4  8d841280000000        lea       eax,[edx+edx+80h]
  004e11fb  8d1408                lea       edx,[eax+ecx]
  004e11fe  8d3c00                lea       edi,[eax+eax]
  004e1201  81e2ff0f0000          and       edx,0FFFh
  004e1207  3bd7                  cmp       edx,edi
  004e1209  7f14                  jg        short 004E121Fh
  004e120b  66014e32              add       [esi+32h],cx
  004e120f  c6464300              mov       byte [esi+43h],0
  004e1213  c7465c00000000        mov       dword [esi+5Ch],0
  004e121a  e959fbffff            jmp       004E0D78h

LAB_004e121f:
  004e121f  81e1ff0f0000          and       ecx,0FFFh
  004e1225  81f900080000          cmp       ecx,800h
  004e122b  7c02                  jl        short 004E122Fh
  004e122d  f7d8                  neg       eax

LAB_004e122f:
  004e122f  66014632              add       [esi+32h],ax

LAB_004e1233:
  004e1233  c7465c00000000        mov       dword [esi+5Ch],0
  004e123a  e939fbffff            jmp       004E0D78h
  004e123f  66397e18              cmp       [esi+18h],di
  004e1243  740a                  je        short 004E124Fh
  004e1245  0d00000001            or        eax,1000000h
  004e124a  897e18                mov       [esi+18h],edi
  004e124d  8906                  mov       [esi],eax

LAB_004e124f:
  004e124f  0fbf4610              movsx     eax,word [esi+10h]
  004e1253  39442414              cmp       [esp+14h],eax
  004e1257  7d09                  jge       short 004E1262h
  004e1259  c6464300              mov       byte [esi+43h],0
  004e125d  e916fbffff            jmp       004E0D78h

LAB_004e1262:
  004e1262  8a4643                mov       al,[esi+43h]
  004e1265  84c0                  test      al,al
  004e1267  7405                  je        short 004E126Eh
  004e1269  fec8                  dec       al
  004e126b  884643                mov       [esi+43h],al

LAB_004e126e:
  004e126e  8b542410              mov       edx,[esp+10h]
  004e1272  8b4c2440              mov       ecx,[esp+40h]
  004e1276  8d841280000000        lea       eax,[edx+edx+80h]
  004e127d  8d3c08                lea       edi,[eax+ecx]
  004e1280  8d2c00                lea       ebp,[eax+eax]
  004e1283  81e7ff0f0000          and       edi,0FFFh
  004e1289  3bfd                  cmp       edi,ebp
  004e128b  7f15                  jg        short 004E12A2h
  004e128d  66014e32              add       [esi+32h],cx
  004e1291  81c2c0000000          add       edx,0C0h
  004e1297  c1e207                shl       edx,7
  004e129a  89565c                mov       [esi+5Ch],edx
  004e129d  e9d6faffff            jmp       004E0D78h

LAB_004e12a2:
  004e12a2  81e1ff0f0000          and       ecx,0FFFh
  004e12a8  81f900080000          cmp       ecx,800h
  004e12ae  7c02                  jl        short 004E12B2h
  004e12b0  f7d8                  neg       eax

LAB_004e12b2:
  004e12b2  66014632              add       [esi+32h],ax
  004e12b6  81c2c0000000          add       edx,0C0h
  004e12bc  c1e207                shl       edx,7
  004e12bf  89565c                mov       [esi+5Ch],edx
  004e12c2  e9b1faffff            jmp       004E0D78h
  004e12c7  8a4e43                mov       cl,[esi+43h]
  004e12ca  84c9                  test      cl,cl
  004e12cc  7405                  je        short 004E12D3h
  004e12ce  fec9                  dec       cl
  004e12d0  884e43                mov       [esi+43h],cl

LAB_004e12d3:
  004e12d3  66397e18              cmp       [esi+18h],di
  004e12d7  740a                  je        short 004E12E3h
  004e12d9  0d00000001            or        eax,1000000h
  004e12de  897e18                mov       [esi+18h],edi
  004e12e1  8906                  mov       [esi],eax

LAB_004e12e3:
  004e12e3  8b4c2410              mov       ecx,[esp+10h]
  004e12e7  0fbf5610              movsx     edx,word [esi+10h]
  004e12eb  81c180000000          add       ecx,80h
  004e12f1  52                    push      edx
  004e12f2  c1e107                shl       ecx,7
  004e12f5  894e5c                mov       [esi+5Ch],ecx
  004e12f8  e81f470600            call      00545A1Ch
  004e12fd  0faf465c              imul      eax,[esi+5Ch]
  004e1301  8b7e1c                mov       edi,[esi+1Ch]
  004e1304  c1f80c                sar       eax,0Ch
  004e1307  03f8                  add       edi,eax
  004e1309  0fbf4610              movsx     eax,word [esi+10h]
  004e130d  50                    push      eax
  004e130e  897e1c                mov       [esi+1Ch],edi
  004e1311  e800470600            call      00545A16h
  004e1316  0faf465c              imul      eax,[esi+5Ch]
  004e131a  8b6e24                mov       ebp,[esi+24h]
  004e131d  8b4c2410              mov       ecx,[esp+10h]
  004e1321  c1f80c                sar       eax,0Ch
  004e1324  03e8                  add       ebp,eax
  004e1326  c7465c00000000        mov       dword [esi+5Ch],0
  004e132d  896e24                mov       [esi+24h],ebp
  004e1330  8d048d32000000        lea       eax,[ecx*4+32h]

LAB_004e1337:
  004e1337  8b4c2440              mov       ecx,[esp+40h]
  004e133b  8d3c00                lea       edi,[eax+eax]
  004e133e  8d1408                lea       edx,[eax+ecx]
  004e1341  81e2ff0f0000          and       edx,0FFFh
  004e1347  3bd7                  cmp       edx,edi
  004e1349  7f09                  jg        short 004E1354h
  004e134b  66014e32              add       [esi+32h],cx
  004e134f  e924faffff            jmp       004E0D78h

LAB_004e1354:
  004e1354  81e1ff0f0000          and       ecx,0FFFh
  004e135a  81f900080000          cmp       ecx,800h
  004e1360  7c02                  jl        short 004E1364h
  004e1362  f7d8                  neg       eax

LAB_004e1364:
  004e1364  66014632              add       [esi+32h],ax
  004e1368  e90bfaffff            jmp       004E0D78h
  004e136d  90                    nop
  004e136e  8bff                  mov       edi,edi
  004e1370  a3114e00dc            mov       [0DC004E11h],eax
  004e1375  114e00                adc       [esi],ecx
  004e1378  3f                    aas
  004e1379  124e00                adc       cl,[esi]
  004e137c  c712                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
