Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00493c70  section .text  offset fichier 0x93070  2160 octets

  00493c70  83ec2c                sub       esp,2Ch
  00493c73  53                    push      ebx
  00493c74  55                    push      ebp
  00493c75  56                    push      esi
  00493c76  8b74243c              mov       esi,[esp+3Ch]
  00493c7a  8d44241c              lea       eax,[esp+1Ch]
  00493c7e  57                    push      edi
  00493c7f  8d4c2438              lea       ecx,[esp+38h]
  00493c83  50                    push      eax
  00493c84  8d542438              lea       edx,[esp+38h]
  00493c88  51                    push      ecx
  00493c89  52                    push      edx
  00493c8a  56                    push      esi
  00493c8b  e8f04c0a00            call      00538980h
  00493c90  83c410                add       esp,10h
  00493c93  8d442424              lea       eax,[esp+24h]
  00493c97  8d4c241c              lea       ecx,[esp+1Ch]
  00493c9b  8d542440              lea       edx,[esp+40h]
  00493c9f  50                    push      eax
  00493ca0  51                    push      ecx
  00493ca1  8d44241c              lea       eax,[esp+1Ch]
  00493ca5  52                    push      edx
  00493ca6  8d4c241c              lea       ecx,[esp+1Ch]
  00493caa  50                    push      eax
  00493cab  51                    push      ecx
  00493cac  56                    push      esi
  00493cad  e86e220a00            call      00535F20h
  00493cb2  8b4c2428              mov       ecx,[esp+28h]
  00493cb6  8b442434              mov       eax,[esp+34h]
  00493cba  bb40000000            mov       ebx,40h
  00493cbf  83c418                add       esp,18h
  00493cc2  2bd9                  sub       ebx,ecx
  00493cc4  bf01000000            mov       edi,1
  00493cc9  85c0                  test      eax,eax
  00493ccb  895c2418              mov       [esp+18h],ebx
  00493ccf  0f8403020000          je        near 00493ED8h
  00493cd5  56                    push      esi
  00493cd6  e8c5780400            call      004DB5A0h
  00493cdb  8b06                  mov       eax,[esi]
  00493cdd  83c404                add       esp,4
  00493ce0  a802                  test      al,2
  00493ce2  0f84f3050000          je        near 004942DBh
  00493ce8  f644242410            test      byte [esp+24h],10h
  00493ced  740d                  je        short 00493CFCh
  00493cef  24fd                  and       al,0FDh
  00493cf1  c6464300              mov       byte [esi+43h],0
  00493cf5  8906                  mov       [esi],eax
  00493cf7  e9dc010000            jmp       00493ED8h

LAB_00493cfc:
  00493cfc  33c9                  xor       ecx,ecx
  00493cfe  8a4e42                mov       cl,[esi+42h]
  00493d01  83e900                sub       ecx,0
  00493d04  0f84c1040000          je        near 004941CBh
  00493d0a  49                    dec       ecx
  00493d0b  0f85c7010000          jne       near 00493ED8h
  00493d11  668b5610              mov       dx,[esi+10h]
  00493d15  0fbfca                movsx     ecx,dx
  00493d18  83e900                sub       ecx,0
  00493d1b  0f8490000000          je        near 00493DB1h
  00493d21  49                    dec       ecx
  00493d22  742f                  je        short 00493D53h
  00493d24  49                    dec       ecx
  00493d25  0f85ad010000          jne       near 00493ED8h
  00493d2b  8b465c                mov       eax,[esi+5Ch]
  00493d2e  8bc8                  mov       ecx,eax
  00493d30  c1f904                sar       ecx,4
  00493d33  2bc1                  sub       eax,ecx
  00493d35  66837e1a0e            cmp       word [esi+1Ah],0Eh
  00493d3a  89465c                mov       [esi+5Ch],eax
  00493d3d  7505                  jne       short 00493D44h
  00493d3f  4a                    dec       edx
  00493d40  66895610              mov       [esi+10h],dx

LAB_00493d44:
  00493d44  8b442410              mov       eax,[esp+10h]
  00493d48  c1f802                sar       eax,2
  00493d4b  83c020                add       eax,20h
  00493d4e  e944070000            jmp       00494497h

LAB_00493d53:
  00493d53  8b442410              mov       eax,[esp+10h]
  00493d57  0fbf4e32              movsx     ecx,word [esi+32h]
  00493d5b  0540040000            add       eax,440h
  00493d60  51                    push      ecx
  00493d61  c1e007                shl       eax,7
  00493d64  89465c                mov       [esi+5Ch],eax
  00493d67  e8b01c0b00            call      00545A1Ch
  00493d6c  0faf465c              imul      eax,[esi+5Ch]
  00493d70  0fbf5632              movsx     edx,word [esi+32h]
  00493d74  8b4e50                mov       ecx,[esi+50h]
  00493d77  52                    push      edx
  00493d78  c1f80c                sar       eax,0Ch
  00493d7b  03c8                  add       ecx,eax
  00493d7d  894e50                mov       [esi+50h],ecx
  00493d80  e8911c0b00            call      00545A16h
  00493d85  0faf465c              imul      eax,[esi+5Ch]
  00493d89  8b5658                mov       edx,[esi+58h]
  00493d8c  8b0e                  mov       ecx,[esi]
  00493d8e  c1f80c                sar       eax,0Ch
  00493d91  03d0                  add       edx,eax
  00493d93  0bcf                  or        ecx,edi
  00493d95  66ff4e10              dec       word [esi+10h]
  00493d99  895658                mov       [esi+58h],edx
  00493d9c  c7465400400200        mov       dword [esi+54h],24000h
  00493da3  890e                  mov       [esi],ecx
  00493da5  c7465c00000000        mov       dword [esi+5Ch],0
  00493dac  e927010000            jmp       00493ED8h

LAB_00493db1:
  00493db1  a900000002            test      eax,2000000h
  00493db6  7408                  je        short 00493DC0h
  00493db8  a801                  test      al,1
  00493dba  7504                  jne       short 00493DC0h
  00493dbc  24dd                  and       al,0DDh
  00493dbe  8906                  mov       [esi],eax

LAB_00493dc0:
  00493dc0  f60620                test      byte [esi],20h
  00493dc3  0f850f010000          jne       near 00493ED8h
  00493dc9  8d6e1c                lea       ebp,[esi+1Ch]
  00493dcc  6a00                  push      0
  00493dce  68cc285500            push      5528CCh
  00493dd3  55                    push      ebp
  00493dd4  56                    push      esi
  00493dd5  e8565e0800            call      00519C30h
  00493dda  8bf8                  mov       edi,eax
  00493ddc  83c410                add       esp,10h
  00493ddf  85ff                  test      edi,edi
  00493de1  7406                  je        short 00493DE9h
  00493de3  8b06                  mov       eax,[esi]
  00493de5  0c20                  or        al,20h
  00493de7  8906                  mov       [esi],eax

LAB_00493de9:
  00493de9  a1b8e05d00            mov       eax,[5DE0B8h]
  00493dee  85c0                  test      eax,eax
  00493df0  0f84e2000000          je        near 00493ED8h
  00493df6  8b1f                  mov       ebx,[edi]
  00493df8  66c787960000001000    mov       word [edi+96h],10h
  00493e01  83cb40                or        ebx,40h
  00493e04  57                    push      edi
  00493e05  891f                  mov       [edi],ebx
  00493e07  8b4c2424              mov       ecx,[esp+24h]
  00493e0b  56                    push      esi
  00493e0c  668b4130              mov       ax,[ecx+30h]
  00493e10  0fbfd8                movsx     ebx,ax
  00493e13  66d1f8                sar       ax,1
  00493e16  66894130              mov       [ecx+30h],ax
  00493e1a  e8615d0a00            call      00539B80h
  00493e1f  8b442428              mov       eax,[esp+28h]
  00493e23  83c408                add       esp,8
  00493e26  66895830              mov       [eax+30h],bx
  00493e2a  8d5f1c                lea       ebx,[edi+1Ch]
  00493e2d  53                    push      ebx
  00493e2e  6800180000            push      1800h
  00493e33  6800030000            push      300h
  00493e38  6a03                  push      3
  00493e3a  e8e19c0a00            call      0053DB20h
  00493e3f  8b07                  mov       eax,[edi]
  00493e41  83c410                add       esp,10h
  00493e44  a900000040            test      eax,40000000h
  00493e49  0f8585000000          jne       near 00493ED4h
  00493e4f  0fbf4e32              movsx     ecx,word [esi+32h]
  00493e53  51                    push      ecx
  00493e54  e8c31b0b00            call      00545A1Ch
  00493e59  8d0440                lea       eax,[eax+eax*2]
  00493e5c  8d1480                lea       edx,[eax+eax*4]
  00493e5f  d1e2                  shl       edx,1
  00493e61  895750                mov       [edi+50h],edx
  00493e64  0fbf4632              movsx     eax,word [esi+32h]
  00493e68  50                    push      eax
  00493e69  e8a81b0b00            call      00545A16h
  00493e6e  8d0440                lea       eax,[eax+eax*2]
  00493e71  8d0c80                lea       ecx,[eax+eax*4]
  00493e74  8b07                  mov       eax,[edi]
  00493e76  d1e1                  shl       ecx,1
  00493e78  a801                  test      al,1
  00493e7a  894f58                mov       [edi+58h],ecx
  00493e7d  750b                  jne       short 00493E8Ah
  00493e7f  0c01                  or        al,1
  00493e81  c7475400800100        mov       dword [edi+54h],18000h
  00493e88  8907                  mov       [edi],eax

LAB_00493e8a:
  00493e8a  8b5500                mov       edx,[ebp]
  00493e8d  8b4620                mov       eax,[esi+20h]
  00493e90  8b4e24                mov       ecx,[esi+24h]
  00493e93  89542428              mov       [esp+28h],edx
  00493e97  8b13                  mov       edx,[ebx]
  00493e99  8944242c              mov       [esp+2Ch],eax
  00493e9d  895500                mov       [ebp],edx
  00493ea0  8b4720                mov       eax,[edi+20h]
  00493ea3  6aff                  push      0FFFFFFFFh
  00493ea5  894c2434              mov       [esp+34h],ecx
  00493ea9  894620                mov       [esi+20h],eax
  00493eac  8b4f24                mov       ecx,[edi+24h]
  00493eaf  6a00                  push      0
  00493eb1  6a39                  push      39h
  00493eb3  56                    push      esi
  00493eb4  894e24                mov       [esi+24h],ecx
  00493eb7  e8146bfeff            call      0047A9D0h
  00493ebc  8b542438              mov       edx,[esp+38h]
  00493ec0  8b44243c              mov       eax,[esp+3Ch]
  00493ec4  8b4c2440              mov       ecx,[esp+40h]
  00493ec8  83c410                add       esp,10h
  00493ecb  895500                mov       [ebp],edx
  00493ece  894620                mov       [esi+20h],eax
  00493ed1  894e24                mov       [esi+24h],ecx

LAB_00493ed4:
  00493ed4  8b5c2418              mov       ebx,[esp+18h]

LAB_00493ed8:
  00493ed8  8b44241c              mov       eax,[esp+1Ch]
  00493edc  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00493ee5  50                    push      eax
  00493ee6  56                    push      esi
  00493ee7  e87442ffff            call      00488160h
  00493eec  83c408                add       esp,8
  00493eef  56                    push      esi
  00493ef0  e84bfa0900            call      00533940h
  00493ef5  83c404                add       esp,4
  00493ef8  56                    push      esi
  00493ef9  e8f2bb0400            call      004DFAF0h
  00493efe  8b4c2438              mov       ecx,[esp+38h]
  00493f02  83c404                add       esp,4
  00493f05  51                    push      ecx
  00493f06  56                    push      esi
  00493f07  e864f80400            call      004E3770h
  00493f0c  8b442424              mov       eax,[esp+24h]
  00493f10  83c408                add       esp,8
  00493f13  85c0                  test      eax,eax
  00493f15  0f84a8020000          je        near 004941C3h
  00493f1b  f70617080000          test      dword [esi],817h
  00493f21  0f859c020000          jne       near 004941C3h
  00493f27  8a4643                mov       al,[esi+43h]
  00493f2a  84c0                  test      al,al
  00493f2c  0f8591020000          jne       near 004941C3h
  00493f32  8b466c                mov       eax,[esi+6Ch]
  00493f35  8b561c                mov       edx,[esi+1Ch]
  00493f38  8b781c                mov       edi,[eax+1Ch]
  00493f3b  8b6824                mov       ebp,[eax+24h]
  00493f3e  8b4624                mov       eax,[esi+24h]
  00493f41  2bfa                  sub       edi,edx
  00493f43  2be8                  sub       ebp,eax
  00493f45  c1ff0c                sar       edi,0Ch
  00493f48  c1fd0c                sar       ebp,0Ch
  00493f4b  8bd7                  mov       edx,edi
  00493f4d  8bc5                  mov       eax,ebp
  00493f4f  0fafd7                imul      edx,edi
  00493f52  0fafc5                imul      eax,ebp
  00493f55  03d0                  add       edx,eax
  00493f57  52                    push      edx
  00493f58  e8b31a0b00            call      00545A10h
  00493f5d  55                    push      ebp
  00493f5e  57                    push      edi
  00493f5f  8944241c              mov       [esp+1Ch],eax
  00493f63  e8ba1a0b00            call      00545A22h
  00493f68  668b4e32              mov       cx,[esi+32h]
  00493f6c  2bc1                  sub       eax,ecx
  00493f6e  25ff0f0000            and       eax,0FFFh
  00493f73  89442440              mov       [esp+40h],eax
  00493f77  8b442414              mov       eax,[esp+14h]
  00493f7b  3dfa000000            cmp       eax,0FAh
  00493f80  0f8dfe000000          jge       near 00494084h
  00493f86  e8b5310b00            call      00547140h
  00493f8b  99                    cdq
  00493f8c  b964000000            mov       ecx,64h
  00493f91  f7f9                  idiv      ecx
  00493f93  83fa32                cmp       edx,32h
  00493f96  7d61                  jge       short 00493FF9h
  00493f98  8b542440              mov       edx,[esp+40h]
  00493f9c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  00493fa3  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  00493faa  25ff0f0000            and       eax,0FFFh
  00493faf  3bc1                  cmp       eax,ecx
  00493fb1  0f8f02010000          jg        near 004940B9h
  00493fb7  e884310b00            call      00547140h
  00493fbc  99                    cdq
  00493fbd  8d4c1b01              lea       ecx,[ebx+ebx+1]
  00493fc1  f7f9                  idiv      ecx
  00493fc3  8b442414              mov       eax,[esp+14h]
  00493fc7  83c264                add       edx,64h
  00493fca  3bc2                  cmp       eax,edx
  00493fcc  0f8de7000000          jge       near 004940B9h
  00493fd2  8b16                  mov       edx,[esi]
  00493fd4  c7461803000000        mov       dword [esi+18h],3
  00493fdb  81e2fffffffe          and       edx,0FEFFFFFFh
  00493fe1  c6464200              mov       byte [esi+42h],0
  00493fe5  83ca02                or        edx,2
  00493fe8  c7465c00000000        mov       dword [esi+5Ch],0
  00493fef  8916                  mov       [esi],edx
  00493ff1  5f                    pop       edi
  00493ff2  5e                    pop       esi
  00493ff3  5d                    pop       ebp
  00493ff4  5b                    pop       ebx
  00493ff5  83c42c                add       esp,2Ch
  00493ff8  c3                    ret

LAB_00493ff9:
  00493ff9  83fa4b                cmp       edx,4Bh
  00493ffc  0f8c3f010000          jl        near 00494141h
  00494002  83fa5a                cmp       edx,5Ah
  00494005  7d2d                  jge       short 00494034h
  00494007  8b542440              mov       edx,[esp+40h]
  0049400b  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  00494012  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  00494019  25ff0f0000            and       eax,0FFFh
  0049401e  3bc1                  cmp       eax,ecx
  00494020  7f12                  jg        short 00494034h
  00494022  8b16                  mov       edx,[esi]
  00494024  81e2fffffffe          and       edx,0FEFFFFFFh
  0049402a  83ca02                or        edx,2
  0049402d  8916                  mov       [esi],edx
  0049402f  e9a0000000            jmp       004940D4h

LAB_00494034:
  00494034  c6464203              mov       byte [esi+42h],3
  00494038  e803310b00            call      00547140h
  0049403d  99                    cdq
  0049403e  b90a000000            mov       ecx,0Ah
  00494043  f7f9                  idiv      ecx
  00494045  d1fb                  sar       ebx,1
  00494047  02d3                  add       dl,bl
  00494049  80c214                add       dl,14h
  0049404c  885643                mov       [esi+43h],dl
  0049404f  8b542410              mov       edx,[esp+10h]
  00494053  8dba80000000          lea       edi,[edx+80h]
  00494059  c1e704                shl       edi,4
  0049405c  e8df300b00            call      00547140h
  00494061  99                    cdq
  00494062  f7ff                  idiv      edi
  00494064  d1ff                  sar       edi,1
  00494066  2bd7                  sub       edx,edi
  00494068  8b7c2440              mov       edi,[esp+40h]
  0049406c  03d7                  add       edx,edi
  0049406e  66035632              add       dx,[esi+32h]
  00494072  81c200080000          add       edx,800h
  00494078  66895610              mov       [esi+10h],dx
  0049407c  5f                    pop       edi
  0049407d  5e                    pop       esi
  0049407e  5d                    pop       ebp
  0049407f  5b                    pop       ebx
  00494080  83c42c                add       esp,2Ch
  00494083  c3                    ret

LAB_00494084:
  00494084  3df4010000            cmp       eax,1F4h
  00494089  0f8ddb000000          jge       near 0049416Ah
  0049408f  e8ac300b00            call      00547140h
  00494094  99                    cdq
  00494095  b964000000            mov       ecx,64h
  0049409a  f7f9                  idiv      ecx
  0049409c  83fa2d                cmp       edx,2Dh
  0049409f  7d4c                  jge       short 004940EDh
  004940a1  8b542440              mov       edx,[esp+40h]
  004940a5  81c200040000          add       edx,400h
  004940ab  81e2ff0f0000          and       edx,0FFFh
  004940b1  81fa00080000          cmp       edx,800h
  004940b7  7c10                  jl        short 004940C9h

LAB_004940b9:
  004940b9  c6464201              mov       byte [esi+42h],1
  004940bd  c6464301              mov       byte [esi+43h],1
  004940c1  5f                    pop       edi
  004940c2  5e                    pop       esi
  004940c3  5d                    pop       ebp
  004940c4  5b                    pop       ebx
  004940c5  83c42c                add       esp,2Ch
  004940c8  c3                    ret

LAB_004940c9:
  004940c9  8b06                  mov       eax,[esi]
  004940cb  25fffffffe            and       eax,0FEFFFFFFh
  004940d0  0c02                  or        al,2
  004940d2  8906                  mov       [esi],eax

LAB_004940d4:
  004940d4  c7461804000000        mov       dword [esi+18h],4
  004940db  c6464201              mov       byte [esi+42h],1
  004940df  66c746100200          mov       word [esi+10h],2
  004940e5  5f                    pop       edi
  004940e6  5e                    pop       esi
  004940e7  5d                    pop       ebp
  004940e8  5b                    pop       ebx
  004940e9  83c42c                add       esp,2Ch
  004940ec  c3                    ret

LAB_004940ed:
  004940ed  83fa46                cmp       edx,46h
  004940f0  7d4a                  jge       short 0049413Ch
  004940f2  c6464203              mov       byte [esi+42h],3
  004940f6  e845300b00            call      00547140h
  004940fb  99                    cdq
  004940fc  b90a000000            mov       ecx,0Ah
  00494101  f7f9                  idiv      ecx
  00494103  d1fb                  sar       ebx,1
  00494105  02d3                  add       dl,bl
  00494107  80c214                add       dl,14h
  0049410a  885643                mov       [esi+43h],dl
  0049410d  8b542410              mov       edx,[esp+10h]
  00494111  8dba80000000          lea       edi,[edx+80h]
  00494117  c1e704                shl       edi,4
  0049411a  e821300b00            call      00547140h
  0049411f  99                    cdq
  00494120  f7ff                  idiv      edi
  00494122  8b4c2440              mov       ecx,[esp+40h]
  00494126  d1ff                  sar       edi,1
  00494128  2bd7                  sub       edx,edi
  0049412a  03d1                  add       edx,ecx
  0049412c  66035632              add       dx,[esi+32h]
  00494130  66895610              mov       [esi+10h],dx
  00494134  5f                    pop       edi
  00494135  5e                    pop       esi
  00494136  5d                    pop       ebp
  00494137  5b                    pop       ebx
  00494138  83c42c                add       esp,2Ch
  0049413b  c3                    ret

LAB_0049413c:
  0049413c  83fa5a                cmp       edx,5Ah
  0049413f  7d3b                  jge       short 0049417Ch

LAB_00494141:
  00494141  c6464202              mov       byte [esi+42h],2
  00494145  e8f62f0b00            call      00547140h
  0049414a  99                    cdq
  0049414b  b90a000000            mov       ecx,0Ah
  00494150  66c746106400          mov       word [esi+10h],64h
  00494156  f7f9                  idiv      ecx
  00494158  d1fb                  sar       ebx,1
  0049415a  02d3                  add       dl,bl
  0049415c  80c214                add       dl,14h
  0049415f  885643                mov       [esi+43h],dl
  00494162  5f                    pop       edi
  00494163  5e                    pop       esi
  00494164  5d                    pop       ebp
  00494165  5b                    pop       ebx
  00494166  83c42c                add       esp,2Ch
  00494169  c3                    ret

LAB_0049416a:
  0049416a  e8d12f0b00            call      00547140h
  0049416f  99                    cdq
  00494170  b964000000            mov       ecx,64h
  00494175  f7f9                  idiv      ecx
  00494177  83fa32                cmp       edx,32h
  0049417a  7d23                  jge       short 0049419Fh

LAB_0049417c:
  0049417c  c6464200              mov       byte [esi+42h],0
  00494180  e8bb2f0b00            call      00547140h
  00494185  99                    cdq
  00494186  b90a000000            mov       ecx,0Ah
  0049418b  f7f9                  idiv      ecx
  0049418d  d1fb                  sar       ebx,1
  0049418f  02d3                  add       dl,bl
  00494191  80c214                add       dl,14h
  00494194  885643                mov       [esi+43h],dl
  00494197  5f                    pop       edi
  00494198  5e                    pop       esi
  00494199  5d                    pop       ebp
  0049419a  5b                    pop       ebx
  0049419b  83c42c                add       esp,2Ch
  0049419e  c3                    ret

LAB_0049419f:
  0049419f  c6464203              mov       byte [esi+42h],3
  004941a3  e8982f0b00            call      00547140h
  004941a8  99                    cdq
  004941a9  b90a000000            mov       ecx,0Ah
  004941ae  f7f9                  idiv      ecx
  004941b0  d1fb                  sar       ebx,1
  004941b2  02d3                  add       dl,bl
  004941b4  80c214                add       dl,14h
  004941b7  885643                mov       [esi+43h],dl
  004941ba  e8812f0b00            call      00547140h
  004941bf  66894610              mov       [esi+10h],ax

LAB_004941c3:
  004941c3  5f                    pop       edi
  004941c4  5e                    pop       esi
  004941c5  5d                    pop       ebp
  004941c6  5b                    pop       ebx
  004941c7  83c42c                add       esp,2Ch
  004941ca  c3                    ret

LAB_004941cb:
  004941cb  668b561a              mov       dx,[esi+1Ah]
  004941cf  8b7c2410              mov       edi,[esp+10h]
  004941d3  6683fa09              cmp       dx,9
  004941d7  7d39                  jge       short 00494212h
  004941d9  8b4c2440              mov       ecx,[esp+40h]
  004941dd  8bc7                  mov       eax,edi
  004941df  d1f8                  sar       eax,1
  004941e1  83c020                add       eax,20h
  004941e4  8d1c08                lea       ebx,[eax+ecx]
  004941e7  8d2c00                lea       ebp,[eax+eax]
  004941ea  81e3ff0f0000          and       ebx,0FFFh
  004941f0  3bdd                  cmp       ebx,ebp
  004941f2  7f06                  jg        short 004941FAh
  004941f4  66014e32              add       [esi+32h],cx
  004941f8  eb14                  jmp       short 0049420Eh

LAB_004941fa:
  004941fa  81e1ff0f0000          and       ecx,0FFFh
  00494200  81f900080000          cmp       ecx,800h
  00494206  7c02                  jl        short 0049420Ah
  00494208  f7d8                  neg       eax

LAB_0049420a:
  0049420a  66014632              add       [esi+32h],ax

LAB_0049420e:
  0049420e  8b5c2418              mov       ebx,[esp+18h]

LAB_00494212:
  00494212  6683fa09              cmp       dx,9
  00494216  7545                  jne       short 0049425Dh
  00494218  0fbf5632              movsx     edx,word [esi+32h]
  0049421c  81c700030000          add       edi,300h
  00494222  52                    push      edx
  00494223  c1e707                shl       edi,7
  00494226  897e5c                mov       [esi+5Ch],edi
  00494229  e8ee170b00            call      00545A1Ch
  0049422e  0faf465c              imul      eax,[esi+5Ch]
  00494232  8b6e50                mov       ebp,[esi+50h]
  00494235  c1f80c                sar       eax,0Ch
  00494238  03e8                  add       ebp,eax
  0049423a  0fbf4632              movsx     eax,word [esi+32h]
  0049423e  50                    push      eax
  0049423f  896e50                mov       [esi+50h],ebp
  00494242  e8cf170b00            call      00545A16h
  00494247  0faf465c              imul      eax,[esi+5Ch]
  0049424b  8b4e58                mov       ecx,[esi+58h]
  0049424e  c7465c00000000        mov       dword [esi+5Ch],0
  00494255  c1f80c                sar       eax,0Ch
  00494258  03c8                  add       ecx,eax
  0049425a  894e58                mov       [esi+58h],ecx

LAB_0049425d:
  0049425d  668b461a              mov       ax,[esi+1Ah]
  00494261  663d0a00              cmp       ax,0Ah
  00494265  7c57                  jl        short 004942BEh
  00494267  663d0d00              cmp       ax,0Dh
  0049426b  7f51                  jg        short 004942BEh
  0049426d  6a00                  push      0
  0049426f  8d4e1c                lea       ecx,[esi+1Ch]
  00494272  68c0285500            push      5528C0h
  00494277  51                    push      ecx
  00494278  56                    push      esi
  00494279  e8b2590800            call      00519C30h
  0049427e  8bf8                  mov       edi,eax
  00494280  a1b8e05d00            mov       eax,[5DE0B8h]
  00494285  83c410                add       esp,10h
  00494288  85c0                  test      eax,eax
  0049428a  7432                  je        short 004942BEh
  0049428c  8b2f                  mov       ebp,[edi]
  0049428e  57                    push      edi
  0049428f  83cd40                or        ebp,40h
  00494292  56                    push      esi
  00494293  66c787960000001000    mov       word [edi+96h],10h
  0049429c  892f                  mov       [edi],ebp
  0049429e  e8dd580a00            call      00539B80h
  004942a3  83c408                add       esp,8
  004942a6  83c71c                add       edi,1Ch
  004942a9  57                    push      edi
  004942aa  6800180000            push      1800h
  004942af  6800030000            push      300h
  004942b4  6a03                  push      3
  004942b6  e865980a00            call      0053DB20h
  004942bb  83c410                add       esp,10h

LAB_004942be:
  004942be  8b06                  mov       eax,[esi]
  004942c0  a900000002            test      eax,2000000h
  004942c5  0f84c8000000          je        near 00494393h
  004942cb  24fd                  and       al,0FDh
  004942cd  c7465c00000000        mov       dword [esi+5Ch],0
  004942d4  8906                  mov       [esi],eax
  004942d6  e9fdfbffff            jmp       00493ED8h

LAB_004942db:
  004942db  a811                  test      al,11h
  004942dd  0f85f5fbffff          jne       near 00493ED8h
  004942e3  a904080000            test      eax,804h
  004942e8  0f85eafbffff          jne       near 00493ED8h
  004942ee  33c9                  xor       ecx,ecx
  004942f0  8a4e42                mov       cl,[esi+42h]
  004942f3  83f903                cmp       ecx,3
  004942f6  0f87dcfbffff          ja        near 00493ED8h
  004942fc  ff248dd0444900        jmp       dword [ecx*4+4944D0h]
  00494303  33c9                  xor       ecx,ecx
  00494305  66394e18              cmp       [esi+18h],cx
  00494309  740a                  je        short 00494315h
  0049430b  0d00000001            or        eax,1000000h
  00494310  894e18                mov       [esi+18h],ecx
  00494313  8906                  mov       [esi],eax

LAB_00494315:
  00494315  817c24142c010000      cmp       dword [esp+14h],12Ch
  0049431d  7d09                  jge       short 00494328h
  0049431f  c6464300              mov       byte [esi+43h],0
  00494323  e9b0fbffff            jmp       00493ED8h

LAB_00494328:
  00494328  8a4643                mov       al,[esi+43h]
  0049432b  84c0                  test      al,al
  0049432d  7405                  je        short 00494334h
  0049432f  fec8                  dec       al
  00494331  884643                mov       [esi+43h],al

LAB_00494334:
  00494334  894e5c                mov       [esi+5Ch],ecx
  00494337  e99cfbffff            jmp       00493ED8h
  0049433c  66397e18              cmp       [esi+18h],di
  00494340  740a                  je        short 0049434Ch
  00494342  0d00000001            or        eax,1000000h
  00494347  897e18                mov       [esi+18h],edi
  0049434a  8906                  mov       [esi],eax

LAB_0049434c:
  0049434c  8b542410              mov       edx,[esp+10h]
  00494350  8b4c2440              mov       ecx,[esp+40h]
  00494354  8d841280000000        lea       eax,[edx+edx+80h]
  0049435b  8d1408                lea       edx,[eax+ecx]
  0049435e  8d3c00                lea       edi,[eax+eax]
  00494361  81e2ff0f0000          and       edx,0FFFh
  00494367  3bd7                  cmp       edx,edi
  00494369  7f14                  jg        short 0049437Fh
  0049436b  66014e32              add       [esi+32h],cx
  0049436f  c6464300              mov       byte [esi+43h],0
  00494373  c7465c00000000        mov       dword [esi+5Ch],0
  0049437a  e959fbffff            jmp       00493ED8h

LAB_0049437f:
  0049437f  81e1ff0f0000          and       ecx,0FFFh
  00494385  81f900080000          cmp       ecx,800h
  0049438b  7c02                  jl        short 0049438Fh
  0049438d  f7d8                  neg       eax

LAB_0049438f:
  0049438f  66014632              add       [esi+32h],ax

LAB_00494393:
  00494393  c7465c00000000        mov       dword [esi+5Ch],0
  0049439a  e939fbffff            jmp       00493ED8h
  0049439f  66397e18              cmp       [esi+18h],di
  004943a3  740a                  je        short 004943AFh
  004943a5  0d00000001            or        eax,1000000h
  004943aa  897e18                mov       [esi+18h],edi
  004943ad  8906                  mov       [esi],eax

LAB_004943af:
  004943af  0fbf4610              movsx     eax,word [esi+10h]
  004943b3  39442414              cmp       [esp+14h],eax
  004943b7  7d09                  jge       short 004943C2h
  004943b9  c6464300              mov       byte [esi+43h],0
  004943bd  e916fbffff            jmp       00493ED8h

LAB_004943c2:
  004943c2  8a4643                mov       al,[esi+43h]
  004943c5  84c0                  test      al,al
  004943c7  7405                  je        short 004943CEh
  004943c9  fec8                  dec       al
  004943cb  884643                mov       [esi+43h],al

LAB_004943ce:
  004943ce  8b542410              mov       edx,[esp+10h]
  004943d2  8b4c2440              mov       ecx,[esp+40h]
  004943d6  8d841280000000        lea       eax,[edx+edx+80h]
  004943dd  8d3c08                lea       edi,[eax+ecx]
  004943e0  8d2c00                lea       ebp,[eax+eax]
  004943e3  81e7ff0f0000          and       edi,0FFFh
  004943e9  3bfd                  cmp       edi,ebp
  004943eb  7f15                  jg        short 00494402h
  004943ed  66014e32              add       [esi+32h],cx
  004943f1  81c2c0000000          add       edx,0C0h
  004943f7  c1e207                shl       edx,7
  004943fa  89565c                mov       [esi+5Ch],edx
  004943fd  e9d6faffff            jmp       00493ED8h

LAB_00494402:
  00494402  81e1ff0f0000          and       ecx,0FFFh
  00494408  81f900080000          cmp       ecx,800h
  0049440e  7c02                  jl        short 00494412h
  00494410  f7d8                  neg       eax

LAB_00494412:
  00494412  66014632              add       [esi+32h],ax
  00494416  81c2c0000000          add       edx,0C0h
  0049441c  c1e207                shl       edx,7
  0049441f  89565c                mov       [esi+5Ch],edx
  00494422  e9b1faffff            jmp       00493ED8h
  00494427  8a4e43                mov       cl,[esi+43h]
  0049442a  84c9                  test      cl,cl
  0049442c  7405                  je        short 00494433h
  0049442e  fec9                  dec       cl
  00494430  884e43                mov       [esi+43h],cl

LAB_00494433:
  00494433  66397e18              cmp       [esi+18h],di
  00494437  740a                  je        short 00494443h
  00494439  0d00000001            or        eax,1000000h
  0049443e  897e18                mov       [esi+18h],edi
  00494441  8906                  mov       [esi],eax

LAB_00494443:
  00494443  8b4c2410              mov       ecx,[esp+10h]
  00494447  0fbf5610              movsx     edx,word [esi+10h]
  0049444b  81c180000000          add       ecx,80h
  00494451  52                    push      edx
  00494452  c1e107                shl       ecx,7
  00494455  894e5c                mov       [esi+5Ch],ecx
  00494458  e8bf150b00            call      00545A1Ch
  0049445d  0faf465c              imul      eax,[esi+5Ch]
  00494461  8b7e1c                mov       edi,[esi+1Ch]
  00494464  c1f80c                sar       eax,0Ch
  00494467  03f8                  add       edi,eax
  00494469  0fbf4610              movsx     eax,word [esi+10h]
  0049446d  50                    push      eax
  0049446e  897e1c                mov       [esi+1Ch],edi
  00494471  e8a0150b00            call      00545A16h
  00494476  0faf465c              imul      eax,[esi+5Ch]
  0049447a  8b6e24                mov       ebp,[esi+24h]
  0049447d  8b4c2410              mov       ecx,[esp+10h]
  00494481  c1f80c                sar       eax,0Ch
  00494484  03e8                  add       ebp,eax
  00494486  c7465c00000000        mov       dword [esi+5Ch],0
  0049448d  896e24                mov       [esi+24h],ebp
  00494490  8d048d32000000        lea       eax,[ecx*4+32h]

LAB_00494497:
  00494497  8b4c2440              mov       ecx,[esp+40h]
  0049449b  8d3c00                lea       edi,[eax+eax]
  0049449e  8d1408                lea       edx,[eax+ecx]
  004944a1  81e2ff0f0000          and       edx,0FFFh
  004944a7  3bd7                  cmp       edx,edi
  004944a9  7f09                  jg        short 004944B4h
  004944ab  66014e32              add       [esi+32h],cx
  004944af  e924faffff            jmp       00493ED8h

LAB_004944b4:
  004944b4  81e1ff0f0000          and       ecx,0FFFh
  004944ba  81f900080000          cmp       ecx,800h
  004944c0  7c02                  jl        short 004944C4h
  004944c2  f7d8                  neg       eax

LAB_004944c4:
  004944c4  66014632              add       [esi+32h],ax
  004944c8  e90bfaffff            jmp       00493ED8h
  004944cd  90                    nop
  004944ce  8bff                  mov       edi,edi
  004944d0  034349                add       eax,[ebx+49h]
  004944d3  003c43                add       [ebx+eax*2],bh
  004944d6  49                    dec       ecx
  004944d7  009f43490027          add       [edi+27004943h],bl
  004944dd  44                    inc       esp
  004944de  49                    dec       ecx
  004944df  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
