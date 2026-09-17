Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048db30  section .text  offset fichier 0x8cf30  2160 octets

  0048db30  83ec2c                sub       esp,2Ch
  0048db33  53                    push      ebx
  0048db34  55                    push      ebp
  0048db35  56                    push      esi
  0048db36  8b74243c              mov       esi,[esp+3Ch]
  0048db3a  8d44241c              lea       eax,[esp+1Ch]
  0048db3e  57                    push      edi
  0048db3f  8d4c2438              lea       ecx,[esp+38h]
  0048db43  50                    push      eax
  0048db44  8d542438              lea       edx,[esp+38h]
  0048db48  51                    push      ecx
  0048db49  52                    push      edx
  0048db4a  56                    push      esi
  0048db4b  e830ae0a00            call      00538980h
  0048db50  83c410                add       esp,10h
  0048db53  8d442424              lea       eax,[esp+24h]
  0048db57  8d4c241c              lea       ecx,[esp+1Ch]
  0048db5b  8d542440              lea       edx,[esp+40h]
  0048db5f  50                    push      eax
  0048db60  51                    push      ecx
  0048db61  8d44241c              lea       eax,[esp+1Ch]
  0048db65  52                    push      edx
  0048db66  8d4c241c              lea       ecx,[esp+1Ch]
  0048db6a  50                    push      eax
  0048db6b  51                    push      ecx
  0048db6c  56                    push      esi
  0048db6d  e8ae830a00            call      00535F20h
  0048db72  8b4c2428              mov       ecx,[esp+28h]
  0048db76  8b442434              mov       eax,[esp+34h]
  0048db7a  bb40000000            mov       ebx,40h
  0048db7f  83c418                add       esp,18h
  0048db82  2bd9                  sub       ebx,ecx
  0048db84  bf01000000            mov       edi,1
  0048db89  85c0                  test      eax,eax
  0048db8b  895c2418              mov       [esp+18h],ebx
  0048db8f  0f8403020000          je        near 0048DD98h
  0048db95  56                    push      esi
  0048db96  e805da0400            call      004DB5A0h
  0048db9b  8b06                  mov       eax,[esi]
  0048db9d  83c404                add       esp,4
  0048dba0  a802                  test      al,2
  0048dba2  0f84f3050000          je        near 0048E19Bh
  0048dba8  f644242410            test      byte [esp+24h],10h
  0048dbad  740d                  je        short 0048DBBCh
  0048dbaf  24fd                  and       al,0FDh
  0048dbb1  c6464300              mov       byte [esi+43h],0
  0048dbb5  8906                  mov       [esi],eax
  0048dbb7  e9dc010000            jmp       0048DD98h

LAB_0048dbbc:
  0048dbbc  33c9                  xor       ecx,ecx
  0048dbbe  8a4e42                mov       cl,[esi+42h]
  0048dbc1  83e900                sub       ecx,0
  0048dbc4  0f84c1040000          je        near 0048E08Bh
  0048dbca  49                    dec       ecx
  0048dbcb  0f85c7010000          jne       near 0048DD98h
  0048dbd1  668b5610              mov       dx,[esi+10h]
  0048dbd5  0fbfca                movsx     ecx,dx
  0048dbd8  83e900                sub       ecx,0
  0048dbdb  0f8490000000          je        near 0048DC71h
  0048dbe1  49                    dec       ecx
  0048dbe2  742f                  je        short 0048DC13h
  0048dbe4  49                    dec       ecx
  0048dbe5  0f85ad010000          jne       near 0048DD98h
  0048dbeb  8b465c                mov       eax,[esi+5Ch]
  0048dbee  8bc8                  mov       ecx,eax
  0048dbf0  c1f904                sar       ecx,4
  0048dbf3  2bc1                  sub       eax,ecx
  0048dbf5  66837e1a0e            cmp       word [esi+1Ah],0Eh
  0048dbfa  89465c                mov       [esi+5Ch],eax
  0048dbfd  7505                  jne       short 0048DC04h
  0048dbff  4a                    dec       edx
  0048dc00  66895610              mov       [esi+10h],dx

LAB_0048dc04:
  0048dc04  8b442410              mov       eax,[esp+10h]
  0048dc08  c1f802                sar       eax,2
  0048dc0b  83c020                add       eax,20h
  0048dc0e  e944070000            jmp       0048E357h

LAB_0048dc13:
  0048dc13  8b442410              mov       eax,[esp+10h]
  0048dc17  0fbf4e32              movsx     ecx,word [esi+32h]
  0048dc1b  0540040000            add       eax,440h
  0048dc20  51                    push      ecx
  0048dc21  c1e007                shl       eax,7
  0048dc24  89465c                mov       [esi+5Ch],eax
  0048dc27  e8f07d0b00            call      00545A1Ch
  0048dc2c  0faf465c              imul      eax,[esi+5Ch]
  0048dc30  0fbf5632              movsx     edx,word [esi+32h]
  0048dc34  8b4e50                mov       ecx,[esi+50h]
  0048dc37  52                    push      edx
  0048dc38  c1f80c                sar       eax,0Ch
  0048dc3b  03c8                  add       ecx,eax
  0048dc3d  894e50                mov       [esi+50h],ecx
  0048dc40  e8d17d0b00            call      00545A16h
  0048dc45  0faf465c              imul      eax,[esi+5Ch]
  0048dc49  8b5658                mov       edx,[esi+58h]
  0048dc4c  8b0e                  mov       ecx,[esi]
  0048dc4e  c1f80c                sar       eax,0Ch
  0048dc51  03d0                  add       edx,eax
  0048dc53  0bcf                  or        ecx,edi
  0048dc55  66ff4e10              dec       word [esi+10h]
  0048dc59  895658                mov       [esi+58h],edx
  0048dc5c  c7465400400200        mov       dword [esi+54h],24000h
  0048dc63  890e                  mov       [esi],ecx
  0048dc65  c7465c00000000        mov       dword [esi+5Ch],0
  0048dc6c  e927010000            jmp       0048DD98h

LAB_0048dc71:
  0048dc71  a900000002            test      eax,2000000h
  0048dc76  7408                  je        short 0048DC80h
  0048dc78  a801                  test      al,1
  0048dc7a  7504                  jne       short 0048DC80h
  0048dc7c  24dd                  and       al,0DDh
  0048dc7e  8906                  mov       [esi],eax

LAB_0048dc80:
  0048dc80  f60620                test      byte [esi],20h
  0048dc83  0f850f010000          jne       near 0048DD98h
  0048dc89  8d6e1c                lea       ebp,[esi+1Ch]
  0048dc8c  6a00                  push      0
  0048dc8e  68a4285500            push      5528A4h
  0048dc93  55                    push      ebp
  0048dc94  56                    push      esi
  0048dc95  e896bf0800            call      00519C30h
  0048dc9a  8bf8                  mov       edi,eax
  0048dc9c  83c410                add       esp,10h
  0048dc9f  85ff                  test      edi,edi
  0048dca1  7406                  je        short 0048DCA9h
  0048dca3  8b06                  mov       eax,[esi]
  0048dca5  0c20                  or        al,20h
  0048dca7  8906                  mov       [esi],eax

LAB_0048dca9:
  0048dca9  a1b8e05d00            mov       eax,[5DE0B8h]
  0048dcae  85c0                  test      eax,eax
  0048dcb0  0f84e2000000          je        near 0048DD98h
  0048dcb6  8b1f                  mov       ebx,[edi]
  0048dcb8  66c787960000001000    mov       word [edi+96h],10h
  0048dcc1  83cb40                or        ebx,40h
  0048dcc4  57                    push      edi
  0048dcc5  891f                  mov       [edi],ebx
  0048dcc7  8b4c2424              mov       ecx,[esp+24h]
  0048dccb  56                    push      esi
  0048dccc  668b4130              mov       ax,[ecx+30h]
  0048dcd0  0fbfd8                movsx     ebx,ax
  0048dcd3  66d1f8                sar       ax,1
  0048dcd6  66894130              mov       [ecx+30h],ax
  0048dcda  e8a1be0a00            call      00539B80h
  0048dcdf  8b442428              mov       eax,[esp+28h]
  0048dce3  83c408                add       esp,8
  0048dce6  66895830              mov       [eax+30h],bx
  0048dcea  8d5f1c                lea       ebx,[edi+1Ch]
  0048dced  53                    push      ebx
  0048dcee  6800180000            push      1800h
  0048dcf3  6800030000            push      300h
  0048dcf8  6a03                  push      3
  0048dcfa  e821fe0a00            call      0053DB20h
  0048dcff  8b07                  mov       eax,[edi]
  0048dd01  83c410                add       esp,10h
  0048dd04  a900000040            test      eax,40000000h
  0048dd09  0f8585000000          jne       near 0048DD94h
  0048dd0f  0fbf4e32              movsx     ecx,word [esi+32h]
  0048dd13  51                    push      ecx
  0048dd14  e8037d0b00            call      00545A1Ch
  0048dd19  8d0440                lea       eax,[eax+eax*2]
  0048dd1c  8d1480                lea       edx,[eax+eax*4]
  0048dd1f  d1e2                  shl       edx,1
  0048dd21  895750                mov       [edi+50h],edx
  0048dd24  0fbf4632              movsx     eax,word [esi+32h]
  0048dd28  50                    push      eax
  0048dd29  e8e87c0b00            call      00545A16h
  0048dd2e  8d0440                lea       eax,[eax+eax*2]
  0048dd31  8d0c80                lea       ecx,[eax+eax*4]
  0048dd34  8b07                  mov       eax,[edi]
  0048dd36  d1e1                  shl       ecx,1
  0048dd38  a801                  test      al,1
  0048dd3a  894f58                mov       [edi+58h],ecx
  0048dd3d  750b                  jne       short 0048DD4Ah
  0048dd3f  0c01                  or        al,1
  0048dd41  c7475400800100        mov       dword [edi+54h],18000h
  0048dd48  8907                  mov       [edi],eax

LAB_0048dd4a:
  0048dd4a  8b5500                mov       edx,[ebp]
  0048dd4d  8b4620                mov       eax,[esi+20h]
  0048dd50  8b4e24                mov       ecx,[esi+24h]
  0048dd53  89542428              mov       [esp+28h],edx
  0048dd57  8b13                  mov       edx,[ebx]
  0048dd59  8944242c              mov       [esp+2Ch],eax
  0048dd5d  895500                mov       [ebp],edx
  0048dd60  8b4720                mov       eax,[edi+20h]
  0048dd63  6aff                  push      0FFFFFFFFh
  0048dd65  894c2434              mov       [esp+34h],ecx
  0048dd69  894620                mov       [esi+20h],eax
  0048dd6c  8b4f24                mov       ecx,[edi+24h]
  0048dd6f  6a00                  push      0
  0048dd71  6a39                  push      39h
  0048dd73  56                    push      esi
  0048dd74  894e24                mov       [esi+24h],ecx
  0048dd77  e854ccfeff            call      0047A9D0h
  0048dd7c  8b542438              mov       edx,[esp+38h]
  0048dd80  8b44243c              mov       eax,[esp+3Ch]
  0048dd84  8b4c2440              mov       ecx,[esp+40h]
  0048dd88  83c410                add       esp,10h
  0048dd8b  895500                mov       [ebp],edx
  0048dd8e  894620                mov       [esi+20h],eax
  0048dd91  894e24                mov       [esi+24h],ecx

LAB_0048dd94:
  0048dd94  8b5c2418              mov       ebx,[esp+18h]

LAB_0048dd98:
  0048dd98  8b44241c              mov       eax,[esp+1Ch]
  0048dd9c  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0048dda5  50                    push      eax
  0048dda6  56                    push      esi
  0048dda7  e8b4a3ffff            call      00488160h
  0048ddac  83c408                add       esp,8
  0048ddaf  56                    push      esi
  0048ddb0  e88b5b0a00            call      00533940h
  0048ddb5  83c404                add       esp,4
  0048ddb8  56                    push      esi
  0048ddb9  e8321d0500            call      004DFAF0h
  0048ddbe  8b4c2438              mov       ecx,[esp+38h]
  0048ddc2  83c404                add       esp,4
  0048ddc5  51                    push      ecx
  0048ddc6  56                    push      esi
  0048ddc7  e8a4590500            call      004E3770h
  0048ddcc  8b442424              mov       eax,[esp+24h]
  0048ddd0  83c408                add       esp,8
  0048ddd3  85c0                  test      eax,eax
  0048ddd5  0f84a8020000          je        near 0048E083h
  0048dddb  f70617080000          test      dword [esi],817h
  0048dde1  0f859c020000          jne       near 0048E083h
  0048dde7  8a4643                mov       al,[esi+43h]
  0048ddea  84c0                  test      al,al
  0048ddec  0f8591020000          jne       near 0048E083h
  0048ddf2  8b466c                mov       eax,[esi+6Ch]
  0048ddf5  8b561c                mov       edx,[esi+1Ch]
  0048ddf8  8b781c                mov       edi,[eax+1Ch]
  0048ddfb  8b6824                mov       ebp,[eax+24h]
  0048ddfe  8b4624                mov       eax,[esi+24h]
  0048de01  2bfa                  sub       edi,edx
  0048de03  2be8                  sub       ebp,eax
  0048de05  c1ff0c                sar       edi,0Ch
  0048de08  c1fd0c                sar       ebp,0Ch
  0048de0b  8bd7                  mov       edx,edi
  0048de0d  8bc5                  mov       eax,ebp
  0048de0f  0fafd7                imul      edx,edi
  0048de12  0fafc5                imul      eax,ebp
  0048de15  03d0                  add       edx,eax
  0048de17  52                    push      edx
  0048de18  e8f37b0b00            call      00545A10h
  0048de1d  55                    push      ebp
  0048de1e  57                    push      edi
  0048de1f  8944241c              mov       [esp+1Ch],eax
  0048de23  e8fa7b0b00            call      00545A22h
  0048de28  668b4e32              mov       cx,[esi+32h]
  0048de2c  2bc1                  sub       eax,ecx
  0048de2e  25ff0f0000            and       eax,0FFFh
  0048de33  89442440              mov       [esp+40h],eax
  0048de37  8b442414              mov       eax,[esp+14h]
  0048de3b  3dfa000000            cmp       eax,0FAh
  0048de40  0f8dfe000000          jge       near 0048DF44h
  0048de46  e8f5920b00            call      00547140h
  0048de4b  99                    cdq
  0048de4c  b964000000            mov       ecx,64h
  0048de51  f7f9                  idiv      ecx
  0048de53  83fa32                cmp       edx,32h
  0048de56  7d61                  jge       short 0048DEB9h
  0048de58  8b542440              mov       edx,[esp+40h]
  0048de5c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0048de63  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0048de6a  25ff0f0000            and       eax,0FFFh
  0048de6f  3bc1                  cmp       eax,ecx
  0048de71  0f8f02010000          jg        near 0048DF79h
  0048de77  e8c4920b00            call      00547140h
  0048de7c  99                    cdq
  0048de7d  8d4c1b01              lea       ecx,[ebx+ebx+1]
  0048de81  f7f9                  idiv      ecx
  0048de83  8b442414              mov       eax,[esp+14h]
  0048de87  83c264                add       edx,64h
  0048de8a  3bc2                  cmp       eax,edx
  0048de8c  0f8de7000000          jge       near 0048DF79h
  0048de92  8b16                  mov       edx,[esi]
  0048de94  c7461803000000        mov       dword [esi+18h],3
  0048de9b  81e2fffffffe          and       edx,0FEFFFFFFh
  0048dea1  c6464200              mov       byte [esi+42h],0
  0048dea5  83ca02                or        edx,2
  0048dea8  c7465c00000000        mov       dword [esi+5Ch],0
  0048deaf  8916                  mov       [esi],edx
  0048deb1  5f                    pop       edi
  0048deb2  5e                    pop       esi
  0048deb3  5d                    pop       ebp
  0048deb4  5b                    pop       ebx
  0048deb5  83c42c                add       esp,2Ch
  0048deb8  c3                    ret

LAB_0048deb9:
  0048deb9  83fa4b                cmp       edx,4Bh
  0048debc  0f8c3f010000          jl        near 0048E001h
  0048dec2  83fa5a                cmp       edx,5Ah
  0048dec5  7d2d                  jge       short 0048DEF4h
  0048dec7  8b542440              mov       edx,[esp+40h]
  0048decb  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0048ded2  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0048ded9  25ff0f0000            and       eax,0FFFh
  0048dede  3bc1                  cmp       eax,ecx
  0048dee0  7f12                  jg        short 0048DEF4h
  0048dee2  8b16                  mov       edx,[esi]
  0048dee4  81e2fffffffe          and       edx,0FEFFFFFFh
  0048deea  83ca02                or        edx,2
  0048deed  8916                  mov       [esi],edx
  0048deef  e9a0000000            jmp       0048DF94h

LAB_0048def4:
  0048def4  c6464203              mov       byte [esi+42h],3
  0048def8  e843920b00            call      00547140h
  0048defd  99                    cdq
  0048defe  b90a000000            mov       ecx,0Ah
  0048df03  f7f9                  idiv      ecx
  0048df05  d1fb                  sar       ebx,1
  0048df07  02d3                  add       dl,bl
  0048df09  80c214                add       dl,14h
  0048df0c  885643                mov       [esi+43h],dl
  0048df0f  8b542410              mov       edx,[esp+10h]
  0048df13  8dba80000000          lea       edi,[edx+80h]
  0048df19  c1e704                shl       edi,4
  0048df1c  e81f920b00            call      00547140h
  0048df21  99                    cdq
  0048df22  f7ff                  idiv      edi
  0048df24  d1ff                  sar       edi,1
  0048df26  2bd7                  sub       edx,edi
  0048df28  8b7c2440              mov       edi,[esp+40h]
  0048df2c  03d7                  add       edx,edi
  0048df2e  66035632              add       dx,[esi+32h]
  0048df32  81c200080000          add       edx,800h
  0048df38  66895610              mov       [esi+10h],dx
  0048df3c  5f                    pop       edi
  0048df3d  5e                    pop       esi
  0048df3e  5d                    pop       ebp
  0048df3f  5b                    pop       ebx
  0048df40  83c42c                add       esp,2Ch
  0048df43  c3                    ret

LAB_0048df44:
  0048df44  3df4010000            cmp       eax,1F4h
  0048df49  0f8ddb000000          jge       near 0048E02Ah
  0048df4f  e8ec910b00            call      00547140h
  0048df54  99                    cdq
  0048df55  b964000000            mov       ecx,64h
  0048df5a  f7f9                  idiv      ecx
  0048df5c  83fa2d                cmp       edx,2Dh
  0048df5f  7d4c                  jge       short 0048DFADh
  0048df61  8b542440              mov       edx,[esp+40h]
  0048df65  81c200040000          add       edx,400h
  0048df6b  81e2ff0f0000          and       edx,0FFFh
  0048df71  81fa00080000          cmp       edx,800h
  0048df77  7c10                  jl        short 0048DF89h

LAB_0048df79:
  0048df79  c6464201              mov       byte [esi+42h],1
  0048df7d  c6464301              mov       byte [esi+43h],1
  0048df81  5f                    pop       edi
  0048df82  5e                    pop       esi
  0048df83  5d                    pop       ebp
  0048df84  5b                    pop       ebx
  0048df85  83c42c                add       esp,2Ch
  0048df88  c3                    ret

LAB_0048df89:
  0048df89  8b06                  mov       eax,[esi]
  0048df8b  25fffffffe            and       eax,0FEFFFFFFh
  0048df90  0c02                  or        al,2
  0048df92  8906                  mov       [esi],eax

LAB_0048df94:
  0048df94  c7461804000000        mov       dword [esi+18h],4
  0048df9b  c6464201              mov       byte [esi+42h],1
  0048df9f  66c746100200          mov       word [esi+10h],2
  0048dfa5  5f                    pop       edi
  0048dfa6  5e                    pop       esi
  0048dfa7  5d                    pop       ebp
  0048dfa8  5b                    pop       ebx
  0048dfa9  83c42c                add       esp,2Ch
  0048dfac  c3                    ret

LAB_0048dfad:
  0048dfad  83fa46                cmp       edx,46h
  0048dfb0  7d4a                  jge       short 0048DFFCh
  0048dfb2  c6464203              mov       byte [esi+42h],3
  0048dfb6  e885910b00            call      00547140h
  0048dfbb  99                    cdq
  0048dfbc  b90a000000            mov       ecx,0Ah
  0048dfc1  f7f9                  idiv      ecx
  0048dfc3  d1fb                  sar       ebx,1
  0048dfc5  02d3                  add       dl,bl
  0048dfc7  80c214                add       dl,14h
  0048dfca  885643                mov       [esi+43h],dl
  0048dfcd  8b542410              mov       edx,[esp+10h]
  0048dfd1  8dba80000000          lea       edi,[edx+80h]
  0048dfd7  c1e704                shl       edi,4
  0048dfda  e861910b00            call      00547140h
  0048dfdf  99                    cdq
  0048dfe0  f7ff                  idiv      edi
  0048dfe2  8b4c2440              mov       ecx,[esp+40h]
  0048dfe6  d1ff                  sar       edi,1
  0048dfe8  2bd7                  sub       edx,edi
  0048dfea  03d1                  add       edx,ecx
  0048dfec  66035632              add       dx,[esi+32h]
  0048dff0  66895610              mov       [esi+10h],dx
  0048dff4  5f                    pop       edi
  0048dff5  5e                    pop       esi
  0048dff6  5d                    pop       ebp
  0048dff7  5b                    pop       ebx
  0048dff8  83c42c                add       esp,2Ch
  0048dffb  c3                    ret

LAB_0048dffc:
  0048dffc  83fa5a                cmp       edx,5Ah
  0048dfff  7d3b                  jge       short 0048E03Ch

LAB_0048e001:
  0048e001  c6464202              mov       byte [esi+42h],2
  0048e005  e836910b00            call      00547140h
  0048e00a  99                    cdq
  0048e00b  b90a000000            mov       ecx,0Ah
  0048e010  66c746106400          mov       word [esi+10h],64h
  0048e016  f7f9                  idiv      ecx
  0048e018  d1fb                  sar       ebx,1
  0048e01a  02d3                  add       dl,bl
  0048e01c  80c214                add       dl,14h
  0048e01f  885643                mov       [esi+43h],dl
  0048e022  5f                    pop       edi
  0048e023  5e                    pop       esi
  0048e024  5d                    pop       ebp
  0048e025  5b                    pop       ebx
  0048e026  83c42c                add       esp,2Ch
  0048e029  c3                    ret

LAB_0048e02a:
  0048e02a  e811910b00            call      00547140h
  0048e02f  99                    cdq
  0048e030  b964000000            mov       ecx,64h
  0048e035  f7f9                  idiv      ecx
  0048e037  83fa32                cmp       edx,32h
  0048e03a  7d23                  jge       short 0048E05Fh

LAB_0048e03c:
  0048e03c  c6464200              mov       byte [esi+42h],0
  0048e040  e8fb900b00            call      00547140h
  0048e045  99                    cdq
  0048e046  b90a000000            mov       ecx,0Ah
  0048e04b  f7f9                  idiv      ecx
  0048e04d  d1fb                  sar       ebx,1
  0048e04f  02d3                  add       dl,bl
  0048e051  80c214                add       dl,14h
  0048e054  885643                mov       [esi+43h],dl
  0048e057  5f                    pop       edi
  0048e058  5e                    pop       esi
  0048e059  5d                    pop       ebp
  0048e05a  5b                    pop       ebx
  0048e05b  83c42c                add       esp,2Ch
  0048e05e  c3                    ret

LAB_0048e05f:
  0048e05f  c6464203              mov       byte [esi+42h],3
  0048e063  e8d8900b00            call      00547140h
  0048e068  99                    cdq
  0048e069  b90a000000            mov       ecx,0Ah
  0048e06e  f7f9                  idiv      ecx
  0048e070  d1fb                  sar       ebx,1
  0048e072  02d3                  add       dl,bl
  0048e074  80c214                add       dl,14h
  0048e077  885643                mov       [esi+43h],dl
  0048e07a  e8c1900b00            call      00547140h
  0048e07f  66894610              mov       [esi+10h],ax

LAB_0048e083:
  0048e083  5f                    pop       edi
  0048e084  5e                    pop       esi
  0048e085  5d                    pop       ebp
  0048e086  5b                    pop       ebx
  0048e087  83c42c                add       esp,2Ch
  0048e08a  c3                    ret

LAB_0048e08b:
  0048e08b  668b561a              mov       dx,[esi+1Ah]
  0048e08f  8b7c2410              mov       edi,[esp+10h]
  0048e093  6683fa09              cmp       dx,9
  0048e097  7d39                  jge       short 0048E0D2h
  0048e099  8b4c2440              mov       ecx,[esp+40h]
  0048e09d  8bc7                  mov       eax,edi
  0048e09f  d1f8                  sar       eax,1
  0048e0a1  83c020                add       eax,20h
  0048e0a4  8d1c08                lea       ebx,[eax+ecx]
  0048e0a7  8d2c00                lea       ebp,[eax+eax]
  0048e0aa  81e3ff0f0000          and       ebx,0FFFh
  0048e0b0  3bdd                  cmp       ebx,ebp
  0048e0b2  7f06                  jg        short 0048E0BAh
  0048e0b4  66014e32              add       [esi+32h],cx
  0048e0b8  eb14                  jmp       short 0048E0CEh

LAB_0048e0ba:
  0048e0ba  81e1ff0f0000          and       ecx,0FFFh
  0048e0c0  81f900080000          cmp       ecx,800h
  0048e0c6  7c02                  jl        short 0048E0CAh
  0048e0c8  f7d8                  neg       eax

LAB_0048e0ca:
  0048e0ca  66014632              add       [esi+32h],ax

LAB_0048e0ce:
  0048e0ce  8b5c2418              mov       ebx,[esp+18h]

LAB_0048e0d2:
  0048e0d2  6683fa09              cmp       dx,9
  0048e0d6  7545                  jne       short 0048E11Dh
  0048e0d8  0fbf5632              movsx     edx,word [esi+32h]
  0048e0dc  81c700030000          add       edi,300h
  0048e0e2  52                    push      edx
  0048e0e3  c1e707                shl       edi,7
  0048e0e6  897e5c                mov       [esi+5Ch],edi
  0048e0e9  e82e790b00            call      00545A1Ch
  0048e0ee  0faf465c              imul      eax,[esi+5Ch]
  0048e0f2  8b6e50                mov       ebp,[esi+50h]
  0048e0f5  c1f80c                sar       eax,0Ch
  0048e0f8  03e8                  add       ebp,eax
  0048e0fa  0fbf4632              movsx     eax,word [esi+32h]
  0048e0fe  50                    push      eax
  0048e0ff  896e50                mov       [esi+50h],ebp
  0048e102  e80f790b00            call      00545A16h
  0048e107  0faf465c              imul      eax,[esi+5Ch]
  0048e10b  8b4e58                mov       ecx,[esi+58h]
  0048e10e  c7465c00000000        mov       dword [esi+5Ch],0
  0048e115  c1f80c                sar       eax,0Ch
  0048e118  03c8                  add       ecx,eax
  0048e11a  894e58                mov       [esi+58h],ecx

LAB_0048e11d:
  0048e11d  668b461a              mov       ax,[esi+1Ah]
  0048e121  663d0a00              cmp       ax,0Ah
  0048e125  7c57                  jl        short 0048E17Eh
  0048e127  663d0d00              cmp       ax,0Dh
  0048e12b  7f51                  jg        short 0048E17Eh
  0048e12d  6a00                  push      0
  0048e12f  8d4e1c                lea       ecx,[esi+1Ch]
  0048e132  6898285500            push      552898h
  0048e137  51                    push      ecx
  0048e138  56                    push      esi
  0048e139  e8f2ba0800            call      00519C30h
  0048e13e  8bf8                  mov       edi,eax
  0048e140  a1b8e05d00            mov       eax,[5DE0B8h]
  0048e145  83c410                add       esp,10h
  0048e148  85c0                  test      eax,eax
  0048e14a  7432                  je        short 0048E17Eh
  0048e14c  8b2f                  mov       ebp,[edi]
  0048e14e  57                    push      edi
  0048e14f  83cd40                or        ebp,40h
  0048e152  56                    push      esi
  0048e153  66c787960000001000    mov       word [edi+96h],10h
  0048e15c  892f                  mov       [edi],ebp
  0048e15e  e81dba0a00            call      00539B80h
  0048e163  83c408                add       esp,8
  0048e166  83c71c                add       edi,1Ch
  0048e169  57                    push      edi
  0048e16a  6800180000            push      1800h
  0048e16f  6800030000            push      300h
  0048e174  6a03                  push      3
  0048e176  e8a5f90a00            call      0053DB20h
  0048e17b  83c410                add       esp,10h

LAB_0048e17e:
  0048e17e  8b06                  mov       eax,[esi]
  0048e180  a900000002            test      eax,2000000h
  0048e185  0f84c8000000          je        near 0048E253h
  0048e18b  24fd                  and       al,0FDh
  0048e18d  c7465c00000000        mov       dword [esi+5Ch],0
  0048e194  8906                  mov       [esi],eax
  0048e196  e9fdfbffff            jmp       0048DD98h

LAB_0048e19b:
  0048e19b  a811                  test      al,11h
  0048e19d  0f85f5fbffff          jne       near 0048DD98h
  0048e1a3  a904080000            test      eax,804h
  0048e1a8  0f85eafbffff          jne       near 0048DD98h
  0048e1ae  33c9                  xor       ecx,ecx
  0048e1b0  8a4e42                mov       cl,[esi+42h]
  0048e1b3  83f903                cmp       ecx,3
  0048e1b6  0f87dcfbffff          ja        near 0048DD98h
  0048e1bc  ff248d90e34800        jmp       dword [ecx*4+48E390h]
  0048e1c3  33c9                  xor       ecx,ecx
  0048e1c5  66394e18              cmp       [esi+18h],cx
  0048e1c9  740a                  je        short 0048E1D5h
  0048e1cb  0d00000001            or        eax,1000000h
  0048e1d0  894e18                mov       [esi+18h],ecx
  0048e1d3  8906                  mov       [esi],eax

LAB_0048e1d5:
  0048e1d5  817c24142c010000      cmp       dword [esp+14h],12Ch
  0048e1dd  7d09                  jge       short 0048E1E8h
  0048e1df  c6464300              mov       byte [esi+43h],0
  0048e1e3  e9b0fbffff            jmp       0048DD98h

LAB_0048e1e8:
  0048e1e8  8a4643                mov       al,[esi+43h]
  0048e1eb  84c0                  test      al,al
  0048e1ed  7405                  je        short 0048E1F4h
  0048e1ef  fec8                  dec       al
  0048e1f1  884643                mov       [esi+43h],al

LAB_0048e1f4:
  0048e1f4  894e5c                mov       [esi+5Ch],ecx
  0048e1f7  e99cfbffff            jmp       0048DD98h
  0048e1fc  66397e18              cmp       [esi+18h],di
  0048e200  740a                  je        short 0048E20Ch
  0048e202  0d00000001            or        eax,1000000h
  0048e207  897e18                mov       [esi+18h],edi
  0048e20a  8906                  mov       [esi],eax

LAB_0048e20c:
  0048e20c  8b542410              mov       edx,[esp+10h]
  0048e210  8b4c2440              mov       ecx,[esp+40h]
  0048e214  8d841280000000        lea       eax,[edx+edx+80h]
  0048e21b  8d1408                lea       edx,[eax+ecx]
  0048e21e  8d3c00                lea       edi,[eax+eax]
  0048e221  81e2ff0f0000          and       edx,0FFFh
  0048e227  3bd7                  cmp       edx,edi
  0048e229  7f14                  jg        short 0048E23Fh
  0048e22b  66014e32              add       [esi+32h],cx
  0048e22f  c6464300              mov       byte [esi+43h],0
  0048e233  c7465c00000000        mov       dword [esi+5Ch],0
  0048e23a  e959fbffff            jmp       0048DD98h

LAB_0048e23f:
  0048e23f  81e1ff0f0000          and       ecx,0FFFh
  0048e245  81f900080000          cmp       ecx,800h
  0048e24b  7c02                  jl        short 0048E24Fh
  0048e24d  f7d8                  neg       eax

LAB_0048e24f:
  0048e24f  66014632              add       [esi+32h],ax

LAB_0048e253:
  0048e253  c7465c00000000        mov       dword [esi+5Ch],0
  0048e25a  e939fbffff            jmp       0048DD98h
  0048e25f  66397e18              cmp       [esi+18h],di
  0048e263  740a                  je        short 0048E26Fh
  0048e265  0d00000001            or        eax,1000000h
  0048e26a  897e18                mov       [esi+18h],edi
  0048e26d  8906                  mov       [esi],eax

LAB_0048e26f:
  0048e26f  0fbf4610              movsx     eax,word [esi+10h]
  0048e273  39442414              cmp       [esp+14h],eax
  0048e277  7d09                  jge       short 0048E282h
  0048e279  c6464300              mov       byte [esi+43h],0
  0048e27d  e916fbffff            jmp       0048DD98h

LAB_0048e282:
  0048e282  8a4643                mov       al,[esi+43h]
  0048e285  84c0                  test      al,al
  0048e287  7405                  je        short 0048E28Eh
  0048e289  fec8                  dec       al
  0048e28b  884643                mov       [esi+43h],al

LAB_0048e28e:
  0048e28e  8b542410              mov       edx,[esp+10h]
  0048e292  8b4c2440              mov       ecx,[esp+40h]
  0048e296  8d841280000000        lea       eax,[edx+edx+80h]
  0048e29d  8d3c08                lea       edi,[eax+ecx]
  0048e2a0  8d2c00                lea       ebp,[eax+eax]
  0048e2a3  81e7ff0f0000          and       edi,0FFFh
  0048e2a9  3bfd                  cmp       edi,ebp
  0048e2ab  7f15                  jg        short 0048E2C2h
  0048e2ad  66014e32              add       [esi+32h],cx
  0048e2b1  81c2c0000000          add       edx,0C0h
  0048e2b7  c1e207                shl       edx,7
  0048e2ba  89565c                mov       [esi+5Ch],edx
  0048e2bd  e9d6faffff            jmp       0048DD98h

LAB_0048e2c2:
  0048e2c2  81e1ff0f0000          and       ecx,0FFFh
  0048e2c8  81f900080000          cmp       ecx,800h
  0048e2ce  7c02                  jl        short 0048E2D2h
  0048e2d0  f7d8                  neg       eax

LAB_0048e2d2:
  0048e2d2  66014632              add       [esi+32h],ax
  0048e2d6  81c2c0000000          add       edx,0C0h
  0048e2dc  c1e207                shl       edx,7
  0048e2df  89565c                mov       [esi+5Ch],edx
  0048e2e2  e9b1faffff            jmp       0048DD98h
  0048e2e7  8a4e43                mov       cl,[esi+43h]
  0048e2ea  84c9                  test      cl,cl
  0048e2ec  7405                  je        short 0048E2F3h
  0048e2ee  fec9                  dec       cl
  0048e2f0  884e43                mov       [esi+43h],cl

LAB_0048e2f3:
  0048e2f3  66397e18              cmp       [esi+18h],di
  0048e2f7  740a                  je        short 0048E303h
  0048e2f9  0d00000001            or        eax,1000000h
  0048e2fe  897e18                mov       [esi+18h],edi
  0048e301  8906                  mov       [esi],eax

LAB_0048e303:
  0048e303  8b4c2410              mov       ecx,[esp+10h]
  0048e307  0fbf5610              movsx     edx,word [esi+10h]
  0048e30b  81c180000000          add       ecx,80h
  0048e311  52                    push      edx
  0048e312  c1e107                shl       ecx,7
  0048e315  894e5c                mov       [esi+5Ch],ecx
  0048e318  e8ff760b00            call      00545A1Ch
  0048e31d  0faf465c              imul      eax,[esi+5Ch]
  0048e321  8b7e1c                mov       edi,[esi+1Ch]
  0048e324  c1f80c                sar       eax,0Ch
  0048e327  03f8                  add       edi,eax
  0048e329  0fbf4610              movsx     eax,word [esi+10h]
  0048e32d  50                    push      eax
  0048e32e  897e1c                mov       [esi+1Ch],edi
  0048e331  e8e0760b00            call      00545A16h
  0048e336  0faf465c              imul      eax,[esi+5Ch]
  0048e33a  8b6e24                mov       ebp,[esi+24h]
  0048e33d  8b4c2410              mov       ecx,[esp+10h]
  0048e341  c1f80c                sar       eax,0Ch
  0048e344  03e8                  add       ebp,eax
  0048e346  c7465c00000000        mov       dword [esi+5Ch],0
  0048e34d  896e24                mov       [esi+24h],ebp
  0048e350  8d048d32000000        lea       eax,[ecx*4+32h]

LAB_0048e357:
  0048e357  8b4c2440              mov       ecx,[esp+40h]
  0048e35b  8d3c00                lea       edi,[eax+eax]
  0048e35e  8d1408                lea       edx,[eax+ecx]
  0048e361  81e2ff0f0000          and       edx,0FFFh
  0048e367  3bd7                  cmp       edx,edi
  0048e369  7f09                  jg        short 0048E374h
  0048e36b  66014e32              add       [esi+32h],cx
  0048e36f  e924faffff            jmp       0048DD98h

LAB_0048e374:
  0048e374  81e1ff0f0000          and       ecx,0FFFh
  0048e37a  81f900080000          cmp       ecx,800h
  0048e380  7c02                  jl        short 0048E384h
  0048e382  f7d8                  neg       eax

LAB_0048e384:
  0048e384  66014632              add       [esi+32h],ax
  0048e388  e90bfaffff            jmp       0048DD98h
  0048e38d  90                    nop
  0048e38e  8bff                  mov       edi,edi
  0048e390  c3                    ret
  0048e391  e148                  loope     0048E3DBh
  0048e393  00fc                  add       ah,bh
  0048e395  e148                  loope     0048E3DFh
  0048e397  005fe2                add       [edi-1Eh],bl
  0048e39a  48                    dec       eax
  0048e39b  00e7                  add       bh,ah
  0048e39d  e248                  loop      0048E3E7h
  0048e39f  00                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
