
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00422e50  section .text  offset fichier 0x22250  880 octets

  00422e50  51                    push      ecx
  00422e51  53                    push      ebx
  00422e52  55                    push      ebp
  00422e53  56                    push      esi
  00422e54  8b742414              mov       esi,[esp+14h]
  00422e58  33db                  xor       ebx,ebx
  00422e5a  57                    push      edi
  00422e5b  8b466c                mov       eax,[esi+6Ch]
  00422e5e  3bc3                  cmp       eax,ebx
  00422e60  7505                  jne       short 00422E67h
  00422e62  b858e55d00            mov       eax,5DE558h

LAB_00422e67:
  00422e67  8b406c                mov       eax,[eax+6Ch]
  00422e6a  3bc3                  cmp       eax,ebx
  00422e6c  89442418              mov       [esp+18h],eax
  00422e70  7508                  jne       short 00422E7Ah
  00422e72  c744241858e55d00      mov       dword [esp+18h],5DE558h

LAB_00422e7a:
  00422e7a  395e5c                cmp       [esi+5Ch],ebx
  00422e7d  0f8594010000          jne       near 00423017h
  00422e83  66395e10              cmp       [esi+10h],bx
  00422e87  755b                  jne       short 00422EE4h
  00422e89  66b81900              mov       ax,19h
  00422e8d  668b4e36              mov       cx,[esi+36h]
  00422e91  662b4612              sub       ax,[esi+12h]
  00422e95  83e1fe                and       ecx,0FFFFFFFEh
  00422e98  bd01000000            mov       ebp,1
  00422e9d  c6460407              mov       byte [esi+4],7
  00422ea1  c6460a02              mov       byte [esi+0Ah],2
  00422ea5  885e0b                mov       [esi+0Bh],bl
  00422ea8  8d0480                lea       eax,[eax+eax*4]
  00422eab  c64638fa              mov       byte [esi+38h],0FAh
  00422eaf  c64639fa              mov       byte [esi+39h],0FAh
  00422eb3  c6463afa              mov       byte [esi+3Ah],0FAh
  00422eb7  8d0480                lea       eax,[eax+eax*4]
  00422eba  66895e28              mov       [esi+28h],bx
  00422ebe  c1e004                shl       eax,4
  00422ec1  83c902                or        ecx,2
  00422ec4  6689462a              mov       [esi+2Ah],ax
  00422ec8  66895e2c              mov       [esi+2Ch],bx
  00422ecc  66896e10              mov       [esi+10h],bp
  00422ed0  66895e12              mov       [esi+12h],bx
  00422ed4  66895e14              mov       [esi+14h],bx
  00422ed8  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  00422ede  66894e36              mov       [esi+36h],cx
  00422ee2  eb05                  jmp       short 00422EE9h

LAB_00422ee4:
  00422ee4  bd01000000            mov       ebp,1

LAB_00422ee9:
  00422ee9  66396e10              cmp       [esi+10h],bp
  00422eed  0f8529010000          jne       near 0042301Ch
  00422ef3  6afb                  push      0FFFFFFFBh
  00422ef5  6af9                  push      0FFFFFFF9h
  00422ef7  6af9                  push      0FFFFFFF9h
  00422ef9  56                    push      esi
  00422efa  e8e11efeff            call      00404DE0h
  00422eff  668b4628              mov       ax,[esi+28h]
  00422f03  b930750000            mov       ecx,7530h
  00422f08  0fbfd0                movsx     edx,ax
  00422f0b  2bca                  sub       ecx,edx
  00422f0d  bf30750000            mov       edi,7530h
  00422f12  c1f903                sar       ecx,3
  00422f15  03c8                  add       ecx,eax
  00422f17  668b462c              mov       ax,[esi+2Ch]
  00422f1b  0fbfd0                movsx     edx,ax
  00422f1e  2bfa                  sub       edi,edx
  00422f20  83c410                add       esp,10h
  00422f23  c1ff03                sar       edi,3
  00422f26  03f8                  add       edi,eax
  00422f28  8a463a                mov       al,[esi+3Ah]
  00422f2b  3ac3                  cmp       al,bl
  00422f2d  66894e28              mov       [esi+28h],cx
  00422f31  66897e2c              mov       [esi+2Ch],di
  00422f35  7506                  jne       short 00422F3Dh
  00422f37  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00422f3d:
  00422f3d  66395e16              cmp       [esi+16h],bx
  00422f41  0f85d5000000          jne       near 0042301Ch
  00422f47  6681f97869            cmp       cx,6978h
  00422f4c  0f8dca000000          jge       near 0042301Ch
  00422f52  33ed                  xor       ebp,ebp
  00422f54  bfc0935e00            mov       edi,5E93C0h
  00422f59  c744241004000000      mov       dword [esp+10h],4

LAB_00422f61:
  00422f61  8b07                  mov       eax,[edi]
  00422f63  a900000080            test      eax,80000000h
  00422f68  0f8493000000          je        near 00423001h
  00422f6e  a900000050            test      eax,50000000h
  00422f73  0f8588000000          jne       near 00423001h
  00422f79  8b471c                mov       eax,[edi+1Ch]
  00422f7c  8b561c                mov       edx,[esi+1Ch]
  00422f7f  8b4f24                mov       ecx,[edi+24h]
  00422f82  2bc2                  sub       eax,edx
  00422f84  8b5624                mov       edx,[esi+24h]
  00422f87  c1f80c                sar       eax,0Ch
  00422f8a  2bca                  sub       ecx,edx
  00422f8c  8bd0                  mov       edx,eax
  00422f8e  c1f90c                sar       ecx,0Ch
  00422f91  0fafd0                imul      edx,eax
  00422f94  8bc1                  mov       eax,ecx
  00422f96  0fafc1                imul      eax,ecx
  00422f99  03d0                  add       edx,eax
  00422f9b  52                    push      edx
  00422f9c  e86f2a1200            call      00545A10h
  00422fa1  0fbf5628              movsx     edx,word [esi+28h]
  00422fa5  8bc8                  mov       ecx,eax
  00422fa7  b81f85eb51            mov       eax,51EB851Fh
  00422fac  f7ea                  imul      edx
  00422fae  c1fa04                sar       edx,4
  00422fb1  8bc2                  mov       eax,edx
  00422fb3  c1e81f                shr       eax,1Fh
  00422fb6  03d0                  add       edx,eax
  00422fb8  3bca                  cmp       ecx,edx
  00422fba  7d45                  jge       short 00423001h
  00422fbc  668b4614              mov       ax,[esi+14h]
  00422fc0  ba01000000            mov       edx,1
  00422fc5  8bcd                  mov       ecx,ebp
  00422fc7  d3e2                  shl       edx,cl
  00422fc9  0fbfc8                movsx     ecx,ax
  00422fcc  85d1                  test      ecx,edx
  00422fce  7531                  jne       short 00423001h
  00422fd0  ba01000000            mov       edx,1
  00422fd5  8bcd                  mov       ecx,ebp
  00422fd7  d3e2                  shl       edx,cl
  00422fd9  53                    push      ebx
  00422fda  57                    push      edi
  00422fdb  0bd0                  or        edx,eax
  00422fdd  8b4644                mov       eax,[esi+44h]
  00422fe0  3bc3                  cmp       eax,ebx
  00422fe2  66895614              mov       [esi+14h],dx
  00422fe6  750c                  jne       short 00422FF4h
  00422fe8  8b442420              mov       eax,[esp+20h]
  00422fec  50                    push      eax
  00422fed  e8ce010000            call      004231C0h
  00422ff2  eb0a                  jmp       short 00422FFEh

LAB_00422ff4:
  00422ff4  8b4c2420              mov       ecx,[esp+20h]
  00422ff8  51                    push      ecx
  00422ff9  e802020000            call      00423200h

LAB_00422ffe:
  00422ffe  83c40c                add       esp,0Ch

LAB_00423001:
  00423001  8b442410              mov       eax,[esp+10h]
  00423005  45                    inc       ebp
  00423006  81c79c000000          add       edi,9Ch
  0042300c  48                    dec       eax
  0042300d  89442410              mov       [esp+10h],eax
  00423011  0f854affffff          jne       near 00422F61h

LAB_00423017:
  00423017  bd01000000            mov       ebp,1

LAB_0042301c:
  0042301c  396e5c                cmp       [esi+5Ch],ebp
  0042301f  0f858c010000          jne       near 004231B1h
  00423025  66395e10              cmp       [esi+10h],bx
  00423029  0f859b000000          jne       near 004230CAh
  0042302f  c6460430              mov       byte [esi+4],30h
  00423033  c6460a02              mov       byte [esi+0Ah],2
  00423037  885e0b                mov       [esi+0Bh],bl
  0042303a  e801411200            call      00547140h
  0042303f  99                    cdq
  00423040  b9c8000000            mov       ecx,0C8h
  00423045  f7f9                  idiv      ecx
  00423047  03d1                  add       edx,ecx
  00423049  66895628              mov       [esi+28h],dx
  0042304d  e8ee401200            call      00547140h
  00423052  99                    cdq
  00423053  b9c8000000            mov       ecx,0C8h
  00423058  f7f9                  idiv      ecx
  0042305a  03d1                  add       edx,ecx
  0042305c  6689562a              mov       [esi+2Ah],dx
  00423060  e8db401200            call      00547140h
  00423065  99                    cdq
  00423066  b9c8000000            mov       ecx,0C8h
  0042306b  f7f9                  idiv      ecx
  0042306d  03d1                  add       edx,ecx
  0042306f  6689562c              mov       [esi+2Ch],dx
  00423073  e8c8401200            call      00547140h
  00423078  99                    cdq
  00423079  33c2                  xor       eax,edx
  0042307b  2bc2                  sub       eax,edx
  0042307d  25ff0f0000            and       eax,0FFFh
  00423082  33c2                  xor       eax,edx
  00423084  2bc2                  sub       eax,edx
  00423086  66894632              mov       [esi+32h],ax
  0042308a  e8b1401200            call      00547140h
  0042308f  99                    cdq
  00423090  b9f4010000            mov       ecx,1F4h
  00423095  885e38                mov       [esi+38h],bl
  00423098  f7f9                  idiv      ecx
  0042309a  885e39                mov       [esi+39h],bl
  0042309d  885e3a                mov       [esi+3Ah],bl
  004230a0  66896e10              mov       [esi+10h],bp
  004230a4  66895e12              mov       [esi+12h],bx
  004230a8  66895630              mov       [esi+30h],dx
  004230ac  e88f401200            call      00547140h
  004230b1  99                    cdq
  004230b2  b9c8000000            mov       ecx,0C8h
  004230b7  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  004230bd  f7f9                  idiv      ecx
  004230bf  8b4620                mov       eax,[esi+20h]
  004230c2  c1e20c                shl       edx,0Ch
  004230c5  03c2                  add       eax,edx
  004230c7  894620                mov       [esi+20h],eax

LAB_004230ca:
  004230ca  66396e10              cmp       [esi+10h],bp
  004230ce  b96affffff            mov       ecx,0FFFFFF6Ah
  004230d3  bf64000000            mov       edi,64h
  004230d8  7570                  jne       short 0042314Ah
  004230da  8a4638                mov       al,[esi+38h]
  004230dd  bafa000000            mov       edx,0FAh
  004230e2  8be8                  mov       ebp,eax
  004230e4  66014e32              add       [esi+32h],cx
  004230e8  81e5ff000000          and       ebp,0FFh
  004230ee  2bd5                  sub       edx,ebp
  004230f0  d1fa                  sar       edx,1
  004230f2  02d0                  add       dl,al
  004230f4  8a4639                mov       al,[esi+39h]
  004230f7  8be8                  mov       ebp,eax
  004230f9  885638                mov       [esi+38h],dl
  004230fc  81e5ff000000          and       ebp,0FFh
  00423102  bafa000000            mov       edx,0FAh
  00423107  2bd5                  sub       edx,ebp
  00423109  d1fa                  sar       edx,1
  0042310b  02d0                  add       dl,al
  0042310d  8a463a                mov       al,[esi+3Ah]
  00423110  8be8                  mov       ebp,eax
  00423112  885639                mov       [esi+39h],dl
  00423115  81e5ff000000          and       ebp,0FFh
  0042311b  bafa000000            mov       edx,0FAh
  00423120  2bd5                  sub       edx,ebp
  00423122  d1fa                  sar       edx,1
  00423124  02d0                  add       dl,al
  00423126  66ff4612              inc       word [esi+12h]
  0042312a  66837e1205            cmp       word [esi+12h],5
  0042312f  88563a                mov       [esi+3Ah],dl
  00423132  7e0a                  jle       short 0042313Eh
  00423134  66c746100200          mov       word [esi+10h],2
  0042313a  66895e12              mov       [esi+12h],bx

LAB_0042313e:
  0042313e  66017e28              add       [esi+28h],di
  00423142  66017e2a              add       [esi+2Ah],di
  00423146  66017e2c              add       [esi+2Ch],di

LAB_0042314a:
  0042314a  66837e1002            cmp       word [esi+10h],2
  0042314f  7560                  jne       short 004231B1h
  00423151  8a4638                mov       al,[esi+38h]
  00423154  66014e32              add       [esi+32h],cx
  00423158  8bd0                  mov       edx,eax
  0042315a  81e2ff000000          and       edx,0FFh
  00423160  f7da                  neg       edx
  00423162  c1fa03                sar       edx,3
  00423165  02d0                  add       dl,al
  00423167  8a4639                mov       al,[esi+39h]
  0042316a  885638                mov       [esi+38h],dl
  0042316d  8bd0                  mov       edx,eax
  0042316f  81e2ff000000          and       edx,0FFh
  00423175  f7da                  neg       edx
  00423177  c1fa03                sar       edx,3
  0042317a  02d0                  add       dl,al
  0042317c  8a463a                mov       al,[esi+3Ah]
  0042317f  885639                mov       [esi+39h],dl
  00423182  8bd0                  mov       edx,eax
  00423184  81e2ff000000          and       edx,0FFh
  0042318a  f7da                  neg       edx
  0042318c  c1fa03                sar       edx,3
  0042318f  02d0                  add       dl,al
  00423191  66ff4612              inc       word [esi+12h]
  00423195  66837e1214            cmp       word [esi+12h],14h
  0042319a  88563a                mov       [esi+3Ah],dl
  0042319d  7e06                  jle       short 004231A5h
  0042319f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004231a5:
  004231a5  66017e28              add       [esi+28h],di
  004231a9  66017e2a              add       [esi+2Ah],di
  004231ad  66017e2c              add       [esi+2Ch],di

LAB_004231b1:
  004231b1  5f                    pop       edi
  004231b2  5e                    pop       esi
  004231b3  5d                    pop       ebp
  004231b4  5b                    pop       ebx
  004231b5  59                    pop       ecx
  004231b6  c3                    ret

16 cible(s) de saut interne : 0x00422e67, 0x00422e7a, 0x00422ee4, 0x00422ee9, 0x00422f3d, 0x00422f61, 0x00422ff4, 0x00422ffe, 0x00423001, 0x00423017, 0x0042301c, 0x004230ca, 0x0042313e, 0x0042314a, 0x004231a5, 0x004231b1
