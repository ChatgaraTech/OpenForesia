Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048cbb0  section .text  offset fichier 0x8bfb0  1872 octets

  0048cbb0  83ec2c                sub       esp,2Ch
  0048cbb3  53                    push      ebx
  0048cbb4  55                    push      ebp
  0048cbb5  56                    push      esi
  0048cbb6  8b74243c              mov       esi,[esp+3Ch]
  0048cbba  8d442420              lea       eax,[esp+20h]
  0048cbbe  57                    push      edi
  0048cbbf  8d4c2428              lea       ecx,[esp+28h]
  0048cbc3  50                    push      eax
  0048cbc4  8d542430              lea       edx,[esp+30h]
  0048cbc8  51                    push      ecx
  0048cbc9  33ed                  xor       ebp,ebp
  0048cbcb  52                    push      edx
  0048cbcc  56                    push      esi
  0048cbcd  66896c2440            mov       [esp+40h],bp
  0048cbd2  66896c2442            mov       [esp+42h],bp
  0048cbd7  66c74424442800        mov       word [esp+44h],28h
  0048cbde  66c74424463200        mov       word [esp+46h],32h
  0048cbe5  66896c2448            mov       [esp+48h],bp
  0048cbea  66c744244a3c00        mov       word [esp+4Ah],3Ch
  0048cbf1  e88abd0a00            call      00538980h
  0048cbf6  83c410                add       esp,10h
  0048cbf9  8d442420              lea       eax,[esp+20h]
  0048cbfd  8d4c2418              lea       ecx,[esp+18h]
  0048cc01  8d542410              lea       edx,[esp+10h]
  0048cc05  50                    push      eax
  0048cc06  51                    push      ecx
  0048cc07  8d44241c              lea       eax,[esp+1Ch]
  0048cc0b  52                    push      edx
  0048cc0c  8d4c244c              lea       ecx,[esp+4Ch]
  0048cc10  50                    push      eax
  0048cc11  51                    push      ecx
  0048cc12  56                    push      esi
  0048cc13  e808930a00            call      00535F20h
  0048cc18  8b4c2458              mov       ecx,[esp+58h]
  0048cc1c  b840000000            mov       eax,40h
  0048cc21  2bc1                  sub       eax,ecx
  0048cc23  83c418                add       esp,18h
  0048cc26  8944241c              mov       [esp+1Ch],eax
  0048cc2a  8b442418              mov       eax,[esp+18h]
  0048cc2e  3bc5                  cmp       eax,ebp
  0048cc30  bf01000000            mov       edi,1
  0048cc35  0f8411040000          je        near 0048D04Ch
  0048cc3b  56                    push      esi
  0048cc3c  e85fe90400            call      004DB5A0h
  0048cc41  8b06                  mov       eax,[esi]
  0048cc43  83c404                add       esp,4
  0048cc46  a802                  test      al,2
  0048cc48  0f8440010000          je        near 0048CD8Eh
  0048cc4e  8a4c2420              mov       cl,[esp+20h]
  0048cc52  bb10000000            mov       ebx,10h
  0048cc57  84cb                  test      bl,cl
  0048cc59  7415                  je        short 0048CC70h
  0048cc5b  24fd                  and       al,0FDh
  0048cc5d  c6464300              mov       byte [esi+43h],0
  0048cc61  8906                  mov       [esi],eax
  0048cc63  8a4640                mov       al,[esi+40h]
  0048cc66  24fe                  and       al,0FEh
  0048cc68  884640                mov       [esi+40h],al
  0048cc6b  e9dc030000            jmp       0048D04Ch

LAB_0048cc70:
  0048cc70  8a4640                mov       al,[esi+40h]
  0048cc73  33ff                  xor       edi,edi
  0048cc75  a801                  test      al,1
  0048cc77  7416                  je        short 0048CC8Fh
  0048cc79  66ff4e14              dec       word [esi+14h]
  0048cc7d  66396e14              cmp       [esi+14h],bp
  0048cc81  0f85a2000000          jne       near 0048CD29h
  0048cc87  8b7e6c                mov       edi,[esi+6Ch]
  0048cc8a  e99a000000            jmp       0048CD29h

LAB_0048cc8f:
  0048cc8f  8d542430              lea       edx,[esp+30h]
  0048cc93  8d6e1c                lea       ebp,[esi+1Ch]
  0048cc96  6a00                  push      0
  0048cc98  52                    push      edx
  0048cc99  55                    push      ebp
  0048cc9a  56                    push      esi
  0048cc9b  e890cf0800            call      00519C30h
  0048cca0  8bf8                  mov       edi,eax
  0048cca2  83c410                add       esp,10h
  0048cca5  85ff                  test      edi,edi
  0048cca7  747e                  je        short 0048CD27h
  0048cca9  8b4624                mov       eax,[esi+24h]
  0048ccac  8b4f24                mov       ecx,[edi+24h]
  0048ccaf  2bc1                  sub       eax,ecx
  0048ccb1  8b4d00                mov       ecx,[ebp]
  0048ccb4  c1f80c                sar       eax,0Ch
  0048ccb7  50                    push      eax
  0048ccb8  8b471c                mov       eax,[edi+1Ch]
  0048ccbb  2bc8                  sub       ecx,eax
  0048ccbd  c1f90c                sar       ecx,0Ch
  0048ccc0  51                    push      ecx
  0048ccc1  e85c8d0b00            call      00545A22h
  0048ccc6  8be8                  mov       ebp,eax
  0048ccc8  55                    push      ebp
  0048ccc9  e84e8d0b00            call      00545A1Ch
  0048ccce  8d0440                lea       eax,[eax+eax*2]
  0048ccd1  55                    push      ebp
  0048ccd2  8d1480                lea       edx,[eax+eax*4]
  0048ccd5  8b4620                mov       eax,[esi+20h]
  0048ccd8  c1e202                shl       edx,2
  0048ccdb  895644                mov       [esi+44h],edx
  0048ccde  8b4f20                mov       ecx,[edi+20h]
  0048cce1  2bc1                  sub       eax,ecx
  0048cce3  894648                mov       [esi+48h],eax
  0048cce6  e82b8d0b00            call      00545A16h
  0048cceb  8d0440                lea       eax,[eax+eax*2]
  0048ccee  8d0c80                lea       ecx,[eax+eax*4]
  0048ccf1  c1e102                shl       ecx,2
  0048ccf4  894e4c                mov       [esi+4Ch],ecx
  0048ccf7  e844a40b00            call      00547140h
  0048ccfc  8b542440              mov       edx,[esp+40h]
  0048cd00  8a4e40                mov       cl,[esi+40h]
  0048cd03  83e007                and       eax,7
  0048cd06  80c901                or        cl,1
  0048cd09  c1fa02                sar       edx,2
  0048cd0c  884e40                mov       [esi+40h],cl
  0048cd0f  8d441008              lea       eax,[eax+edx+8]
  0048cd13  66894610              mov       [esi+10h],ax
  0048cd17  8b07                  mov       eax,[edi]
  0048cd19  0c40                  or        al,40h
  0048cd1b  66899f96000000        mov       [edi+96h],bx
  0048cd22  8907                  mov       [edi],eax
  0048cd24  897e6c                mov       [esi+6Ch],edi

LAB_0048cd27:
  0048cd27  33ed                  xor       ebp,ebp

LAB_0048cd29:
  0048cd29  3bfd                  cmp       edi,ebp
  0048cd2b  7442                  je        short 0048CD6Fh
  0048cd2d  57                    push      edi
  0048cd2e  56                    push      esi
  0048cd2f  e84cce0a00            call      00539B80h
  0048cd34  83c408                add       esp,8
  0048cd37  8d4f1c                lea       ecx,[edi+1Ch]
  0048cd3a  51                    push      ecx
  0048cd3b  6800180000            push      1800h
  0048cd40  6800030000            push      300h
  0048cd45  6a03                  push      3
  0048cd47  e8d40d0b00            call      0053DB20h
  0048cd4c  8b07                  mov       eax,[edi]
  0048cd4e  83c410                add       esp,10h
  0048cd51  a900000040            test      eax,40000000h
  0048cd56  7411                  je        short 0048CD69h
  0048cd58  8a4e40                mov       cl,[esi+40h]
  0048cd5b  8b06                  mov       eax,[esi]
  0048cd5d  80e1fe                and       cl,0FEh
  0048cd60  24fd                  and       al,0FDh
  0048cd62  884e40                mov       [esi+40h],cl
  0048cd65  8906                  mov       [esi],eax
  0048cd67  eb06                  jmp       short 0048CD6Fh

LAB_0048cd69:
  0048cd69  66c746141e00          mov       word [esi+14h],1Eh

LAB_0048cd6f:
  0048cd6f  8b06                  mov       eax,[esi]
  0048cd71  a801                  test      al,1
  0048cd73  0f85d3020000          jne       near 0048D04Ch
  0048cd79  24fd                  and       al,0FDh
  0048cd7b  c6464300              mov       byte [esi+43h],0
  0048cd7f  8906                  mov       [esi],eax
  0048cd81  8a4640                mov       al,[esi+40h]
  0048cd84  24fe                  and       al,0FEh
  0048cd86  884640                mov       [esi+40h],al
  0048cd89  e9bb020000            jmp       0048D049h

LAB_0048cd8e:
  0048cd8e  a811                  test      al,11h
  0048cd90  0f85b6020000          jne       near 0048D04Ch
  0048cd96  a904080000            test      eax,804h
  0048cd9b  0f85ab020000          jne       near 0048D04Ch
  0048cda1  33c9                  xor       ecx,ecx
  0048cda3  8a4e42                mov       cl,[esi+42h]
  0048cda6  2bcd                  sub       ecx,ebp
  0048cda8  0f846f020000          je        near 0048D01Dh
  0048cdae  49                    dec       ecx
  0048cdaf  0f8419020000          je        near 0048CFCEh
  0048cdb5  49                    dec       ecx
  0048cdb6  0f8590020000          jne       near 0048D04Ch
  0048cdbc  8b8e90000000          mov       ecx,[esi+90h]
  0048cdc2  f6c502                test      ch,2
  0048cdc5  0f8452010000          je        near 0048CF1Dh
  0048cdcb  66397e18              cmp       [esi+18h],di
  0048cdcf  740a                  je        short 0048CDDBh
  0048cdd1  0d00000001            or        eax,1000000h
  0048cdd6  897e18                mov       [esi+18h],edi
  0048cdd9  8906                  mov       [esi],eax

LAB_0048cddb:
  0048cddb  33c0                  xor       eax,eax
  0048cddd  8a4643                mov       al,[esi+43h]
  0048cde0  48                    dec       eax
  0048cde1  0f848e000000          je        near 0048CE75h
  0048cde7  48                    dec       eax
  0048cde8  0f855e020000          jne       near 0048D04Ch
  0048cdee  8b542440              mov       edx,[esp+40h]
  0048cdf2  8b4c2410              mov       ecx,[esp+10h]
  0048cdf6  8d841280000000        lea       eax,[edx+edx+80h]
  0048cdfd  8d3c08                lea       edi,[eax+ecx]
  0048ce00  8d1c00                lea       ebx,[eax+eax]
  0048ce03  81e7ff0f0000          and       edi,0FFFh
  0048ce09  3bfb                  cmp       edi,ebx
  0048ce0b  7f06                  jg        short 0048CE13h
  0048ce0d  66014e32              add       [esi+32h],cx
  0048ce11  eb14                  jmp       short 0048CE27h

LAB_0048ce13:
  0048ce13  81e1ff0f0000          and       ecx,0FFFh
  0048ce19  81f900080000          cmp       ecx,800h
  0048ce1f  7c02                  jl        short 0048CE23h
  0048ce21  f7d8                  neg       eax

LAB_0048ce23:
  0048ce23  66014632              add       [esi+32h],ax

LAB_0048ce27:
  0048ce27  81c280020000          add       edx,280h
  0048ce2d  c1e207                shl       edx,7
  0048ce30  89565c                mov       [esi+5Ch],edx
  0048ce33  0fbf5632              movsx     edx,word [esi+32h]
  0048ce37  52                    push      edx
  0048ce38  e8df8b0b00            call      00545A1Ch
  0048ce3d  0faf465c              imul      eax,[esi+5Ch]
  0048ce41  8b5e50                mov       ebx,[esi+50h]
  0048ce44  c1f80c                sar       eax,0Ch
  0048ce47  03d8                  add       ebx,eax
  0048ce49  0fbf4632              movsx     eax,word [esi+32h]
  0048ce4d  50                    push      eax
  0048ce4e  895e50                mov       [esi+50h],ebx
  0048ce51  e8c08b0b00            call      00545A16h
  0048ce56  0faf465c              imul      eax,[esi+5Ch]
  0048ce5a  8b4e58                mov       ecx,[esi+58h]
  0048ce5d  896e5c                mov       [esi+5Ch],ebp
  0048ce60  c1f80c                sar       eax,0Ch
  0048ce63  03c8                  add       ecx,eax
  0048ce65  8a4643                mov       al,[esi+43h]
  0048ce68  fec8                  dec       al
  0048ce6a  894e58                mov       [esi+58h],ecx
  0048ce6d  884643                mov       [esi+43h],al
  0048ce70  e9d7010000            jmp       0048D04Ch

LAB_0048ce75:
  0048ce75  8b4650                mov       eax,[esi+50h]
  0048ce78  8b4e58                mov       ecx,[esi+58h]
  0048ce7b  c1f80c                sar       eax,0Ch
  0048ce7e  8bd0                  mov       edx,eax
  0048ce80  c1f90c                sar       ecx,0Ch
  0048ce83  0fafd0                imul      edx,eax
  0048ce86  8bc1                  mov       eax,ecx
  0048ce88  0fafc1                imul      eax,ecx
  0048ce8b  03d0                  add       edx,eax
  0048ce8d  52                    push      edx
  0048ce8e  e87d8b0b00            call      00545A10h
  0048ce93  8b4c2440              mov       ecx,[esp+40h]
  0048ce97  8bd1                  mov       edx,ecx
  0048ce99  c1fa03                sar       edx,3
  0048ce9c  42                    inc       edx
  0048ce9d  3bc2                  cmp       eax,edx
  0048ce9f  7f03                  jg        short 0048CEA4h
  0048cea1  fe4e43                dec       byte [esi+43h]

LAB_0048cea4:
  0048cea4  0fbf4610              movsx     eax,word [esi+10h]
  0048cea8  39442414              cmp       [esp+14h],eax
  0048ceac  0f8d9a010000          jge       near 0048D04Ch
  0048ceb2  8b16                  mov       edx,[esi]
  0048ceb4  81c100020000          add       ecx,200h
  0048ceba  c1e107                shl       ecx,7
  0048cebd  894e5c                mov       [esi+5Ch],ecx
  0048cec0  81ca02000001          or        edx,1000002h
  0048cec6  0fbf4e32              movsx     ecx,word [esi+32h]
  0048ceca  51                    push      ecx
  0048cecb  8916                  mov       [esi],edx
  0048cecd  c7461803000000        mov       dword [esi+18h],3
  0048ced4  e8438b0b00            call      00545A1Ch
  0048ced9  0faf465c              imul      eax,[esi+5Ch]
  0048cedd  0fbf5632              movsx     edx,word [esi+32h]
  0048cee1  8b4e50                mov       ecx,[esi+50h]
  0048cee4  52                    push      edx
  0048cee5  c1f80c                sar       eax,0Ch
  0048cee8  03c8                  add       ecx,eax
  0048ceea  894e50                mov       [esi+50h],ecx
  0048ceed  e8248b0b00            call      00545A16h
  0048cef2  0faf465c              imul      eax,[esi+5Ch]
  0048cef6  8b4e58                mov       ecx,[esi+58h]
  0048cef9  c1f80c                sar       eax,0Ch
  0048cefc  03c8                  add       ecx,eax
  0048cefe  894e58                mov       [esi+58h],ecx
  0048cf01  e83aa20b00            call      00547140h
  0048cf06  83e007                and       eax,7
  0048cf09  83c020                add       eax,20h
  0048cf0c  c1e00c                shl       eax,0Ch
  0048cf0f  894654                mov       [esi+54h],eax
  0048cf12  8b06                  mov       eax,[esi]
  0048cf14  0bc7                  or        eax,edi
  0048cf16  8906                  mov       [esi],eax
  0048cf18  e92c010000            jmp       0048D049h

LAB_0048cf1d:
  0048cf1d  8b7c2440              mov       edi,[esp+40h]
  0048cf21  8b542410              mov       edx,[esp+10h]
  0048cf25  8d8c3f80000000        lea       ecx,[edi+edi+80h]
  0048cf2c  8d1c11                lea       ebx,[ecx+edx]
  0048cf2f  8d2c09                lea       ebp,[ecx+ecx]
  0048cf32  81e3ff0f0000          and       ebx,0FFFh
  0048cf38  3bdd                  cmp       ebx,ebp
  0048cf3a  7f06                  jg        short 0048CF42h
  0048cf3c  66015632              add       [esi+32h],dx
  0048cf40  eb14                  jmp       short 0048CF56h

LAB_0048cf42:
  0048cf42  81e2ff0f0000          and       edx,0FFFh
  0048cf48  81fa00080000          cmp       edx,800h
  0048cf4e  7c02                  jl        short 0048CF52h
  0048cf50  f7d9                  neg       ecx

LAB_0048cf52:
  0048cf52  66014e32              add       [esi+32h],cx

LAB_0048cf56:
  0048cf56  0d02000001            or        eax,1000002h
  0048cf5b  81c740010000          add       edi,140h
  0048cf61  8906                  mov       [esi],eax
  0048cf63  c7461803000000        mov       dword [esi+18h],3
  0048cf6a  0fbf4632              movsx     eax,word [esi+32h]
  0048cf6e  c1e707                shl       edi,7
  0048cf71  50                    push      eax
  0048cf72  897e5c                mov       [esi+5Ch],edi
  0048cf75  e8a28a0b00            call      00545A1Ch
  0048cf7a  0faf465c              imul      eax,[esi+5Ch]
  0048cf7e  8b4e50                mov       ecx,[esi+50h]
  0048cf81  c1f80c                sar       eax,0Ch
  0048cf84  03c8                  add       ecx,eax
  0048cf86  894e50                mov       [esi+50h],ecx
  0048cf89  0fbf4e32              movsx     ecx,word [esi+32h]
  0048cf8d  51                    push      ecx
  0048cf8e  e8838a0b00            call      00545A16h
  0048cf93  0faf465c              imul      eax,[esi+5Ch]
  0048cf97  8b7e58                mov       edi,[esi+58h]
  0048cf9a  c1f80c                sar       eax,0Ch
  0048cf9d  03f8                  add       edi,eax
  0048cf9f  897e58                mov       [esi+58h],edi
  0048cfa2  e899a10b00            call      00547140h
  0048cfa7  8b54241c              mov       edx,[esp+1Ch]
  0048cfab  83e007                and       eax,7
  0048cfae  8b4e54                mov       ecx,[esi+54h]
  0048cfb1  83c010                add       eax,10h
  0048cfb4  c1e004                shl       eax,4
  0048cfb7  83e2f0                and       edx,0FFFFFFF0h
  0048cfba  03c2                  add       eax,edx
  0048cfbc  c1e008                shl       eax,8
  0048cfbf  03c8                  add       ecx,eax
  0048cfc1  8b06                  mov       eax,[esi]
  0048cfc3  0c01                  or        al,1
  0048cfc5  894e54                mov       [esi+54h],ecx
  0048cfc8  8906                  mov       [esi],eax
  0048cfca  33ed                  xor       ebp,ebp
  0048cfcc  eb7b                  jmp       short 0048D049h

LAB_0048cfce:
  0048cfce  66397e18              cmp       [esi+18h],di
  0048cfd2  740a                  je        short 0048CFDEh
  0048cfd4  0d00000001            or        eax,1000000h
  0048cfd9  897e18                mov       [esi+18h],edi
  0048cfdc  8906                  mov       [esi],eax

LAB_0048cfde:
  0048cfde  8b442440              mov       eax,[esp+40h]
  0048cfe2  8b4c2410              mov       ecx,[esp+10h]
  0048cfe6  8d840080000000        lea       eax,[eax+eax+80h]
  0048cfed  8d1408                lea       edx,[eax+ecx]
  0048cff0  8d3c00                lea       edi,[eax+eax]
  0048cff3  81e2ff0f0000          and       edx,0FFFh
  0048cff9  3bd7                  cmp       edx,edi
  0048cffb  7f0a                  jg        short 0048D007h
  0048cffd  66014e32              add       [esi+32h],cx
  0048d001  c6464300              mov       byte [esi+43h],0
  0048d005  eb42                  jmp       short 0048D049h

LAB_0048d007:
  0048d007  81e1ff0f0000          and       ecx,0FFFh
  0048d00d  81f900080000          cmp       ecx,800h
  0048d013  7c02                  jl        short 0048D017h
  0048d015  f7d8                  neg       eax

LAB_0048d017:
  0048d017  66014632              add       [esi+32h],ax
  0048d01b  eb2c                  jmp       short 0048D049h

LAB_0048d01d:
  0048d01d  66396e18              cmp       [esi+18h],bp
  0048d021  740a                  je        short 0048D02Dh
  0048d023  0d00000001            or        eax,1000000h
  0048d028  896e18                mov       [esi+18h],ebp
  0048d02b  8906                  mov       [esi],eax

LAB_0048d02d:
  0048d02d  817c24142c010000      cmp       dword [esp+14h],12Ch
  0048d035  7d06                  jge       short 0048D03Dh
  0048d037  c6464300              mov       byte [esi+43h],0
  0048d03b  eb0f                  jmp       short 0048D04Ch

LAB_0048d03d:
  0048d03d  8a4643                mov       al,[esi+43h]
  0048d040  84c0                  test      al,al
  0048d042  7405                  je        short 0048D049h
  0048d044  fec8                  dec       al
  0048d046  884643                mov       [esi+43h],al

LAB_0048d049:
  0048d049  896e5c                mov       [esi+5Ch],ebp

LAB_0048d04c:
  0048d04c  8a4e40                mov       cl,[esi+40h]
  0048d04f  f6c101                test      cl,1
  0048d052  0f84b0000000          je        near 0048D108h
  0048d058  8b466c                mov       eax,[esi+6Ch]
  0048d05b  8b5644                mov       edx,[esi+44h]
  0048d05e  03501c                add       edx,[eax+1Ch]
  0048d061  89561c                mov       [esi+1Ch],edx
  0048d064  8b5648                mov       edx,[esi+48h]
  0048d067  035020                add       edx,[eax+20h]
  0048d06a  895620                mov       [esi+20h],edx
  0048d06d  8b564c                mov       edx,[esi+4Ch]
  0048d070  035024                add       edx,[eax+24h]
  0048d073  895624                mov       [esi+24h],edx
  0048d076  8b10                  mov       edx,[eax]
  0048d078  f7c200000100          test      edx,10000h
  0048d07e  7409                  je        short 0048D089h
  0048d080  33c0                  xor       eax,eax
  0048d082  a038e55d00            mov       al,[5DE538h]
  0048d087  eb07                  jmp       short 0048D090h

LAB_0048d089:
  0048d089  33d2                  xor       edx,edx
  0048d08b  8a5008                mov       dl,[eax+8]
  0048d08e  8bc2                  mov       eax,edx

LAB_0048d090:
  0048d090  8d14c0                lea       edx,[eax+eax*8]
  0048d093  8d0450                lea       eax,[eax+edx*2]
  0048d096  33d2                  xor       edx,edx
  0048d098  8a148588915e00        mov       dl,[eax*4+5E9188h]
  0048d09f  8bc2                  mov       eax,edx
  0048d0a1  c1e004                shl       eax,4
  0048d0a4  05204a6000            add       eax,604A20h
  0048d0a9  f64002f0              test      byte [eax+2],0F0h
  0048d0ad  7467                  je        short 0048D116h
  0048d0af  66ff4e10              dec       word [esi+10h]
  0048d0b3  66396e10              cmp       [esi+10h],bp
  0048d0b7  755d                  jne       short 0048D116h
  0048d0b9  0fbf4632              movsx     eax,word [esi+32h]
  0048d0bd  8b16                  mov       edx,[esi]
  0048d0bf  0500080000            add       eax,800h
  0048d0c4  83e2fd                and       edx,0FFFFFFFDh
  0048d0c7  80e1fe                and       cl,0FEh
  0048d0ca  50                    push      eax
  0048d0cb  8916                  mov       [esi],edx
  0048d0cd  c6464300              mov       byte [esi+43h],0
  0048d0d1  884e40                mov       [esi+40h],cl
  0048d0d4  e843890b00            call      00545A1Ch
  0048d0d9  8b4e50                mov       ecx,[esi+50h]
  0048d0dc  c1e005                shl       eax,5
  0048d0df  03c8                  add       ecx,eax
  0048d0e1  894e50                mov       [esi+50h],ecx
  0048d0e4  0fbf4e32              movsx     ecx,word [esi+32h]
  0048d0e8  81c100080000          add       ecx,800h
  0048d0ee  51                    push      ecx
  0048d0ef  e822890b00            call      00545A16h
  0048d0f4  8b4e58                mov       ecx,[esi+58h]
  0048d0f7  c7465400a00000        mov       dword [esi+54h],0A000h
  0048d0fe  c1e005                shl       eax,5
  0048d101  03c8                  add       ecx,eax
  0048d103  894e58                mov       [esi+58h],ecx
  0048d106  eb0e                  jmp       short 0048D116h

LAB_0048d108:
  0048d108  8b542418              mov       edx,[esp+18h]
  0048d10c  52                    push      edx
  0048d10d  56                    push      esi
  0048d10e  e84db0ffff            call      00488160h
  0048d113  83c408                add       esp,8

LAB_0048d116:
  0048d116  56                    push      esi
  0048d117  e824680a00            call      00533940h
  0048d11c  8b9690000000          mov       edx,[esi+90h]
  0048d122  8b4660                mov       eax,[esi+60h]
  0048d125  8b4e20                mov       ecx,[esi+20h]
  0048d128  83c404                add       esp,4
  0048d12b  81e2ffdbffdf          and       edx,0DFFFDBFFh
  0048d131  3bc8                  cmp       ecx,eax
  0048d133  899690000000          mov       [esi+90h],edx
  0048d139  7f24                  jg        short 0048D15Fh
  0048d13b  894620                mov       [esi+20h],eax
  0048d13e  8b06                  mov       eax,[esi]
  0048d140  a803                  test      al,3
  0048d142  7410                  je        short 0048D154h
  0048d144  24fd                  and       al,0FDh
  0048d146  c6464300              mov       byte [esi+43h],0
  0048d14a  8906                  mov       [esi],eax
  0048d14c  8a4640                mov       al,[esi+40h]
  0048d14f  24fe                  and       al,0FEh
  0048d151  884640                mov       [esi+40h],al

LAB_0048d154:
  0048d154  8b06                  mov       eax,[esi]
  0048d156  896e54                mov       [esi+54h],ebp
  0048d159  24fe                  and       al,0FEh
  0048d15b  8906                  mov       [esi],eax
  0048d15d  eb0a                  jmp       short 0048D169h

LAB_0048d15f:
  0048d15f  8b06                  mov       eax,[esi]
  0048d161  a801                  test      al,1
  0048d163  7504                  jne       short 0048D169h
  0048d165  0c01                  or        al,1
  0048d167  8906                  mov       [esi],eax

LAB_0048d169:
  0048d169  8a4640                mov       al,[esi+40h]
  0048d16c  a801                  test      al,1
  0048d16e  7411                  je        short 0048D181h
  0048d170  8b4e6c                mov       ecx,[esi+6Ch]
  0048d173  f70100200050          test      dword [ecx],50002000h
  0048d179  750e                  jne       short 0048D189h
  0048d17b  66396912              cmp       [ecx+12h],bp
  0048d17f  7508                  jne       short 0048D189h

LAB_0048d181:
  0048d181  f7c282880000          test      edx,8882h
  0048d187  7417                  je        short 0048D1A0h

LAB_0048d189:
  0048d189  8b16                  mov       edx,[esi]
  0048d18b  c6464201              mov       byte [esi+42h],1
  0048d18f  83e2fd                and       edx,0FFFFFFFDh
  0048d192  24fe                  and       al,0FEh
  0048d194  8916                  mov       [esi],edx
  0048d196  c6464301              mov       byte [esi+43h],1
  0048d19a  884640                mov       [esi+40h],al
  0048d19d  896e5c                mov       [esi+5Ch],ebp

LAB_0048d1a0:
  0048d1a0  6639ae96000000        cmp       [esi+96h],bp
  0048d1a7  741b                  je        short 0048D1C4h
  0048d1a9  8b06                  mov       eax,[esi]
  0048d1ab  a840                  test      al,40h
  0048d1ad  740a                  je        short 0048D1B9h
  0048d1af  a843                  test      al,43h
  0048d1b1  7511                  jne       short 0048D1C4h
  0048d1b3  c6464300              mov       byte [esi+43h],0
  0048d1b7  eb0b                  jmp       short 0048D1C4h

LAB_0048d1b9:
  0048d1b9  c6464200              mov       byte [esi+42h],0
  0048d1bd  c6464301              mov       byte [esi+43h],1
  0048d1c1  896e5c                mov       [esi+5Ch],ebp

LAB_0048d1c4:
  0048d1c4  396c2418              cmp       [esp+18h],ebp
  0048d1c8  0f841e010000          je        near 0048D2ECh
  0048d1ce  f70617080000          test      dword [esi],817h
  0048d1d4  0f8512010000          jne       near 0048D2ECh
  0048d1da  8a4643                mov       al,[esi+43h]
  0048d1dd  84c0                  test      al,al
  0048d1df  0f8507010000          jne       near 0048D2ECh
  0048d1e5  f6464001              test      byte [esi+40h],1
  0048d1e9  0f85fd000000          jne       near 0048D2ECh
  0048d1ef  8b466c                mov       eax,[esi+6Ch]
  0048d1f2  8b561c                mov       edx,[esi+1Ch]
  0048d1f5  8b781c                mov       edi,[eax+1Ch]
  0048d1f8  8b6824                mov       ebp,[eax+24h]
  0048d1fb  8b4624                mov       eax,[esi+24h]
  0048d1fe  2bfa                  sub       edi,edx
  0048d200  2be8                  sub       ebp,eax
  0048d202  c1ff0c                sar       edi,0Ch
  0048d205  c1fd0c                sar       ebp,0Ch
  0048d208  8bc7                  mov       eax,edi
  0048d20a  8bcd                  mov       ecx,ebp
  0048d20c  0fafc7                imul      eax,edi
  0048d20f  0fafcd                imul      ecx,ebp
  0048d212  03c1                  add       eax,ecx
  0048d214  50                    push      eax
  0048d215  e8f6870b00            call      00545A10h
  0048d21a  55                    push      ebp
  0048d21b  57                    push      edi
  0048d21c  8944241c              mov       [esp+1Ch],eax
  0048d220  e8fd870b00            call      00545A22h
  0048d225  668b5632              mov       dx,[esi+32h]
  0048d229  2bc2                  sub       eax,edx
  0048d22b  25ff0f0000            and       eax,0FFFh
  0048d230  89442410              mov       [esp+10h],eax
  0048d234  8b442414              mov       eax,[esp+14h]
  0048d238  3dfa000000            cmp       eax,0FAh
  0048d23d  7d28                  jge       short 0048D267h
  0048d23f  e8fc9e0b00            call      00547140h
  0048d244  99                    cdq
  0048d245  b964000000            mov       ecx,64h
  0048d24a  f7f9                  idiv      ecx
  0048d24c  83fa32                cmp       edx,32h
  0048d24f  7d6c                  jge       short 0048D2BDh
  0048d251  c6464202              mov       byte [esi+42h],2
  0048d255  c6464302              mov       byte [esi+43h],2
  0048d259  66c746102c01          mov       word [esi+10h],12Ch
  0048d25f  5f                    pop       edi
  0048d260  5e                    pop       esi
  0048d261  5d                    pop       ebp
  0048d262  5b                    pop       ebx
  0048d263  83c42c                add       esp,2Ch
  0048d266  c3                    ret

LAB_0048d267:
  0048d267  3df4010000            cmp       eax,1F4h
  0048d26c  7d3d                  jge       short 0048D2ABh
  0048d26e  e8cd9e0b00            call      00547140h
  0048d273  99                    cdq
  0048d274  b964000000            mov       ecx,64h
  0048d279  f7f9                  idiv      ecx
  0048d27b  83fa2d                cmp       edx,2Dh
  0048d27e  7d10                  jge       short 0048D290h
  0048d280  c6464201              mov       byte [esi+42h],1
  0048d284  c6464301              mov       byte [esi+43h],1
  0048d288  5f                    pop       edi
  0048d289  5e                    pop       esi
  0048d28a  5d                    pop       ebp
  0048d28b  5b                    pop       ebx
  0048d28c  83c42c                add       esp,2Ch
  0048d28f  c3                    ret

LAB_0048d290:
  0048d290  83fa5a                cmp       edx,5Ah
  0048d293  7d38                  jge       short 0048D2CDh
  0048d295  c6464202              mov       byte [esi+42h],2
  0048d299  c6464302              mov       byte [esi+43h],2
  0048d29d  66c746102c01          mov       word [esi+10h],12Ch
  0048d2a3  5f                    pop       edi
  0048d2a4  5e                    pop       esi
  0048d2a5  5d                    pop       ebp
  0048d2a6  5b                    pop       ebx
  0048d2a7  83c42c                add       esp,2Ch
  0048d2aa  c3                    ret

LAB_0048d2ab:
  0048d2ab  e8909e0b00            call      00547140h
  0048d2b0  99                    cdq
  0048d2b1  b964000000            mov       ecx,64h
  0048d2b6  f7f9                  idiv      ecx
  0048d2b8  83fa32                cmp       edx,32h
  0048d2bb  7d10                  jge       short 0048D2CDh

LAB_0048d2bd:
  0048d2bd  c6464201              mov       byte [esi+42h],1
  0048d2c1  c6464301              mov       byte [esi+43h],1
  0048d2c5  5f                    pop       edi
  0048d2c6  5e                    pop       esi
  0048d2c7  5d                    pop       ebp
  0048d2c8  5b                    pop       ebx
  0048d2c9  83c42c                add       esp,2Ch
  0048d2cc  c3                    ret

LAB_0048d2cd:
  0048d2cd  c6464200              mov       byte [esi+42h],0
  0048d2d1  e86a9e0b00            call      00547140h
  0048d2d6  99                    cdq
  0048d2d7  b90a000000            mov       ecx,0Ah
  0048d2dc  f7f9                  idiv      ecx
  0048d2de  8b44241c              mov       eax,[esp+1Ch]
  0048d2e2  d1f8                  sar       eax,1
  0048d2e4  02d0                  add       dl,al
  0048d2e6  80c214                add       dl,14h
  0048d2e9  885643                mov       [esi+43h],dl

LAB_0048d2ec:
  0048d2ec  5f                    pop       edi
  0048d2ed  5e                    pop       esi
  0048d2ee  5d                    pop       ebp
  0048d2ef  5b                    pop       ebx
  0048d2f0  83c42c                add       esp,2Ch
  0048d2f3  c3                    ret
  0048d2f4  90                    nop
  0048d2f5  90                    nop
  0048d2f6  90                    nop
  0048d2f7  90                    nop
  0048d2f8  90                    nop
  0048d2f9  90                    nop
  0048d2fa  90                    nop
  0048d2fb  90                    nop
  0048d2fc  90                    nop
  0048d2fd  90                    nop
  0048d2fe  90                    nop
  0048d2ff  90                    nop

44 cible(s) de saut interne : 0x0048cc70, 0x0048cc8f, 0x0048cd27, 0x0048cd29, 0x0048cd69, 0x0048cd6f, 0x0048cd8e, 0x0048cddb, 0x0048ce13, 0x0048ce23, 0x0048ce27, 0x0048ce75, 0x0048cea4, 0x0048cf1d, 0x0048cf42, 0x0048cf52, 0x0048cf56, 0x0048cfce, 0x0048cfde, 0x0048d007, 0x0048d017, 0x0048d01d, 0x0048d02d, 0x0048d03d, 0x0048d049, 0x0048d04c, 0x0048d089, 0x0048d090, 0x0048d108, 0x0048d116, 0x0048d154, 0x0048d15f, 0x0048d169, 0x0048d181, 0x0048d189, 0x0048d1a0, 0x0048d1b9, 0x0048d1c4, 0x0048d267, 0x0048d290, 0x0048d2ab, 0x0048d2bd, 0x0048d2cd, 0x0048d2ec
