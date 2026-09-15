Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d1f70  section .text  offset fichier 0xd1370  4176 octets

  004d1f70  81ec94000000          sub       esp,94h
  004d1f76  53                    push      ebx
  004d1f77  55                    push      ebp
  004d1f78  56                    push      esi
  004d1f79  8bb424a4000000        mov       esi,[esp+0A4h]
  004d1f80  b864000000            mov       eax,64h
  004d1f85  33ed                  xor       ebp,ebp
  004d1f87  6689442436            mov       [esp+36h],ax
  004d1f8c  668944243a            mov       [esp+3Ah],ax
  004d1f91  6689442442            mov       [esp+42h],ax
  004d1f96  6689442446            mov       [esp+46h],ax
  004d1f9b  8a4641                mov       al,[esi+41h]
  004d1f9e  57                    push      edi
  004d1f9f  84c0                  test      al,al
  004d1fa1  66896c2434            mov       [esp+34h],bp
  004d1fa6  66c74424361e00        mov       word [esp+36h],1Eh
  004d1fad  66c74424383200        mov       word [esp+38h],32h
  004d1fb4  66896c243c            mov       [esp+3Ch],bp
  004d1fb9  66896c2440            mov       [esp+40h],bp
  004d1fbe  66896c2442            mov       [esp+42h],bp
  004d1fc3  66c74424445000        mov       word [esp+44h],50h
  004d1fca  66896c2448            mov       [esp+48h],bp
  004d1fcf  7510                  jne       short 004D1FE1h
  004d1fd1  55                    push      ebp
  004d1fd2  6a03                  push      3
  004d1fd4  56                    push      esi
  004d1fd5  c6464101              mov       byte [esi+41h],1
  004d1fd9  e802d0ffff            call      004CEFE0h
  004d1fde  83c40c                add       esp,0Ch

LAB_004d1fe1:
  004d1fe1  8b461c                mov       eax,[esi+1Ch]
  004d1fe4  8b4e20                mov       ecx,[esi+20h]
  004d1fe7  8b5624                mov       edx,[esi+24h]
  004d1fea  8d5e1c                lea       ebx,[esi+1Ch]
  004d1fed  89442428              mov       [esp+28h],eax
  004d1ff1  8b4664                mov       eax,[esi+64h]
  004d1ff4  894c242c              mov       [esp+2Ch],ecx
  004d1ff8  8b4e60                mov       ecx,[esi+60h]
  004d1ffb  89542430              mov       [esp+30h],edx
  004d1fff  668b5632              mov       dx,[esi+32h]
  004d2003  8944246c              mov       [esp+6Ch],eax
  004d2007  8b467c                mov       eax,[esi+7Ch]
  004d200a  894c2454              mov       [esp+54h],ecx
  004d200e  668b8e90000000        mov       cx,[esi+90h]
  004d2015  6689942492000000      mov       [esp+92h],dx
  004d201d  8b9680000000          mov       edx,[esi+80h]
  004d2023  8b7e6c                mov       edi,[esi+6Ch]
  004d2026  89442468              mov       [esp+68h],eax
  004d202a  8b8684000000          mov       eax,[esi+84h]
  004d2030  66894c2470            mov       [esp+70h],cx
  004d2035  8b8e88000000          mov       ecx,[esi+88h]
  004d203b  89542464              mov       [esp+64h],edx
  004d203f  8b968c000000          mov       edx,[esi+8Ch]
  004d2045  8944244c              mov       [esp+4Ch],eax
  004d2049  894c2460              mov       [esp+60h],ecx
  004d204d  8d442424              lea       eax,[esp+24h]
  004d2051  8954245c              mov       [esp+5Ch],edx
  004d2055  8d4c2474              lea       ecx,[esp+74h]
  004d2059  50                    push      eax
  004d205a  8d54247c              lea       edx,[esp+7Ch]
  004d205e  51                    push      ecx
  004d205f  52                    push      edx
  004d2060  56                    push      esi
  004d2061  897c2460              mov       [esp+60h],edi
  004d2065  e816690600            call      00538980h
  004d206a  83c410                add       esp,10h
  004d206d  8d442458              lea       eax,[esp+58h]
  004d2071  8d4c2420              lea       ecx,[esp+20h]
  004d2075  8d54241c              lea       edx,[esp+1Ch]
  004d2079  50                    push      eax
  004d207a  51                    push      ecx
  004d207b  8d44241c              lea       eax,[esp+1Ch]
  004d207f  52                    push      edx
  004d2080  8d4c241c              lea       ecx,[esp+1Ch]
  004d2084  50                    push      eax
  004d2085  51                    push      ecx
  004d2086  56                    push      esi
  004d2087  e8943e0600            call      00535F20h
  004d208c  8b4c2428              mov       ecx,[esp+28h]
  004d2090  8b542470              mov       edx,[esp+70h]
  004d2094  83c418                add       esp,18h
  004d2097  b840000000            mov       eax,40h
  004d209c  2bc1                  sub       eax,ecx
  004d209e  52                    push      edx
  004d209f  56                    push      esi
  004d20a0  89442420              mov       [esp+20h],eax
  004d20a4  e8373d0000            call      004D5DE0h
  004d20a9  8b03                  mov       eax,[ebx]
  004d20ab  8b571c                mov       edx,[edi+1Ch]
  004d20ae  8b4e20                mov       ecx,[esi+20h]
  004d20b1  2bc2                  sub       eax,edx
  004d20b3  8b5720                mov       edx,[edi+20h]
  004d20b6  83c408                add       esp,8
  004d20b9  2bca                  sub       ecx,edx
  004d20bb  8b5624                mov       edx,[esi+24h]
  004d20be  2b5724                sub       edx,[edi+24h]
  004d20c1  c1f80c                sar       eax,0Ch
  004d20c4  8bf8                  mov       edi,eax
  004d20c6  c1f90c                sar       ecx,0Ch
  004d20c9  0faff8                imul      edi,eax
  004d20cc  8bc1                  mov       eax,ecx
  004d20ce  c1fa0c                sar       edx,0Ch
  004d20d1  0fafc1                imul      eax,ecx
  004d20d4  8bca                  mov       ecx,edx
  004d20d6  03f8                  add       edi,eax
  004d20d8  0fafca                imul      ecx,edx
  004d20db  03f9                  add       edi,ecx
  004d20dd  57                    push      edi
  004d20de  e82d390700            call      00545A10h
  004d20e3  89442414              mov       [esp+14h],eax
  004d20e7  8b442420              mov       eax,[esp+20h]
  004d20eb  3bc5                  cmp       eax,ebp
  004d20ed  0f8449080000          je        near 004D293Ch
  004d20f3  8b3e                  mov       edi,[esi]
  004d20f5  33d2                  xor       edx,edx
  004d20f7  8a5604                mov       dl,[esi+4]
  004d20fa  81e7fffffffd          and       edi,0FDFFFFFFh
  004d2100  0fbf4e18              movsx     ecx,word [esi+18h]
  004d2104  893e                  mov       [esi],edi
  004d2106  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004d210d  8bc7                  mov       eax,edi
  004d210f  8b0c8a                mov       ecx,[edx+ecx*4]
  004d2112  a900000001            test      eax,1000000h
  004d2117  741b                  je        short 004D2134h
  004d2119  66ff461a              inc       word [esi+1Ah]
  004d211d  668b461a              mov       ax,[esi+1Ah]
  004d2121  33d2                  xor       edx,edx
  004d2123  668b5116              mov       dx,[ecx+16h]
  004d2127  0fbfc0                movsx     eax,ax
  004d212a  3bc2                  cmp       eax,edx
  004d212c  7c27                  jl        short 004D2155h
  004d212e  66896e1a              mov       [esi+1Ah],bp
  004d2132  eb21                  jmp       short 004D2155h

LAB_004d2134:
  004d2134  66ff461a              inc       word [esi+1Ah]
  004d2138  668b561a              mov       dx,[esi+1Ah]
  004d213c  33ff                  xor       edi,edi
  004d213e  668b7916              mov       di,[ecx+16h]
  004d2142  0fbfca                movsx     ecx,dx
  004d2145  3bcf                  cmp       ecx,edi
  004d2147  7c0c                  jl        short 004D2155h
  004d2149  4a                    dec       edx
  004d214a  0d00000002            or        eax,2000000h
  004d214f  6689561a              mov       [esi+1Ah],dx
  004d2153  8906                  mov       [esi],eax

LAB_004d2155:
  004d2155  8a4642                mov       al,[esi+42h]
  004d2158  8b0e                  mov       ecx,[esi]
  004d215a  3c05                  cmp       al,5
  004d215c  bd10000000            mov       ebp,10h
  004d2161  7504                  jne       short 004D2167h
  004d2163  0bcd                  or        ecx,ebp
  004d2165  eb03                  jmp       short 004D216Ah

LAB_004d2167:
  004d2167  83e1ef                and       ecx,0FFFFFFEFh

LAB_004d216a:
  004d216a  f6c102                test      cl,2
  004d216d  890e                  mov       [esi],ecx
  004d216f  0f841f020000          je        near 004D2394h
  004d2175  3c0c                  cmp       al,0Ch
  004d2177  7479                  je        short 004D21F2h
  004d2179  3c0d                  cmp       al,0Dh
  004d217b  7475                  je        short 004D21F2h
  004d217d  66837e1a08            cmp       word [esi+1Ah],8
  004d2182  7553                  jne       short 004D21D7h
  004d2184  8d542434              lea       edx,[esp+34h]
  004d2188  6a00                  push      0
  004d218a  52                    push      edx
  004d218b  53                    push      ebx
  004d218c  56                    push      esi
  004d218d  e89e7a0400            call      00519C30h
  004d2192  8bf8                  mov       edi,eax
  004d2194  a1b8e05d00            mov       eax,[5DE0B8h]
  004d2199  83c410                add       esp,10h
  004d219c  85c0                  test      eax,eax
  004d219e  7437                  je        short 004D21D7h
  004d21a0  66837e1a08            cmp       word [esi+1Ah],8
  004d21a5  7530                  jne       short 004D21D7h
  004d21a7  8b0f                  mov       ecx,[edi]
  004d21a9  57                    push      edi
  004d21aa  83c940                or        ecx,40h
  004d21ad  56                    push      esi
  004d21ae  6689af96000000        mov       [edi+96h],bp
  004d21b5  890f                  mov       [edi],ecx
  004d21b7  e8c4790600            call      00539B80h
  004d21bc  83c408                add       esp,8
  004d21bf  83c71c                add       edi,1Ch
  004d21c2  57                    push      edi
  004d21c3  6800180000            push      1800h
  004d21c8  6800030000            push      300h
  004d21cd  6a03                  push      3
  004d21cf  e84cb90600            call      0053DB20h
  004d21d4  83c410                add       esp,10h

LAB_004d21d7:
  004d21d7  8b06                  mov       eax,[esi]
  004d21d9  a900000002            test      eax,2000000h
  004d21de  7412                  je        short 004D21F2h
  004d21e0  a801                  test      al,1
  004d21e2  750a                  jne       short 004D21EEh
  004d21e4  24fd                  and       al,0FDh
  004d21e6  c6464300              mov       byte [esi+43h],0
  004d21ea  8906                  mov       [esi],eax
  004d21ec  eb04                  jmp       short 004D21F2h

LAB_004d21ee:
  004d21ee  24fd                  and       al,0FDh
  004d21f0  8906                  mov       [esi],eax

LAB_004d21f2:
  004d21f2  807e420c              cmp       byte [esi+42h],0Ch
  004d21f6  0f85c1000000          jne       near 004D22BDh
  004d21fc  668b461a              mov       ax,[esi+1Ah]
  004d2200  663d0800              cmp       ax,8
  004d2204  7507                  jne       short 004D220Dh
  004d2206  66837e1803            cmp       word [esi+18h],3
  004d220b  7415                  je        short 004D2222h

LAB_004d220d:
  004d220d  663d0a00              cmp       ax,0Ah
  004d2211  0f85a6000000          jne       near 004D22BDh
  004d2217  66837e1805            cmp       word [esi+18h],5
  004d221c  0f859b000000          jne       near 004D22BDh

LAB_004d2222:
  004d2222  8d442434              lea       eax,[esp+34h]
  004d2226  6a00                  push      0
  004d2228  50                    push      eax
  004d2229  53                    push      ebx
  004d222a  56                    push      esi
  004d222b  e8007a0400            call      00519C30h
  004d2230  8bf8                  mov       edi,eax
  004d2232  a1b8e05d00            mov       eax,[5DE0B8h]
  004d2237  83c410                add       esp,10h
  004d223a  85c0                  test      eax,eax
  004d223c  747f                  je        short 004D22BDh
  004d223e  668b461a              mov       ax,[esi+1Ah]
  004d2242  663d0800              cmp       ax,8
  004d2246  7507                  jne       short 004D224Fh
  004d2248  66837e1803            cmp       word [esi+18h],3
  004d224d  740d                  je        short 004D225Ch

LAB_004d224f:
  004d224f  663d0a00              cmp       ax,0Ah
  004d2253  7568                  jne       short 004D22BDh
  004d2255  66837e1805            cmp       word [esi+18h],5
  004d225a  7561                  jne       short 004D22BDh

LAB_004d225c:
  004d225c  66837e1201            cmp       word [esi+12h],1
  004d2261  750b                  jne       short 004D226Eh
  004d2263  66c787960000000000    mov       word [edi+96h],0
  004d226c  eb0d                  jmp       short 004D227Bh

LAB_004d226e:
  004d226e  8b07                  mov       eax,[edi]
  004d2270  6689af96000000        mov       [edi+96h],bp
  004d2277  0c40                  or        al,40h
  004d2279  8907                  mov       [edi],eax

LAB_004d227b:
  004d227b  8b4c2424              mov       ecx,[esp+24h]
  004d227f  57                    push      edi
  004d2280  56                    push      esi
  004d2281  668b4130              mov       ax,[ecx+30h]
  004d2285  668bd0                mov       dx,ax
  004d2288  66c1fa02              sar       dx,2
  004d228c  0fbfe8                movsx     ebp,ax
  004d228f  2bc2                  sub       eax,edx
  004d2291  66894130              mov       [ecx+30h],ax
  004d2295  e8e6780600            call      00539B80h
  004d229a  8b44242c              mov       eax,[esp+2Ch]
  004d229e  83c408                add       esp,8
  004d22a1  83c71c                add       edi,1Ch
  004d22a4  57                    push      edi
  004d22a5  6800180000            push      1800h
  004d22aa  6800030000            push      300h
  004d22af  6a03                  push      3
  004d22b1  66896830              mov       [eax+30h],bp
  004d22b5  e866b80600            call      0053DB20h
  004d22ba  83c410                add       esp,10h

LAB_004d22bd:
  004d22bd  807e420d              cmp       byte [esi+42h],0Dh
  004d22c1  0f850b010000          jne       near 004D23D2h
  004d22c7  33ed                  xor       ebp,ebp
  004d22c9  8d4c2440              lea       ecx,[esp+40h]
  004d22cd  55                    push      ebp
  004d22ce  51                    push      ecx
  004d22cf  53                    push      ebx
  004d22d0  56                    push      esi
  004d22d1  e85a790400            call      00519C30h
  004d22d6  8bf8                  mov       edi,eax
  004d22d8  a1b8e05d00            mov       eax,[5DE0B8h]
  004d22dd  83c410                add       esp,10h
  004d22e0  3bc5                  cmp       eax,ebp
  004d22e2  0f84f2000000          je        near 004D23DAh
  004d22e8  8b2f                  mov       ebp,[edi]
  004d22ea  66c787960000001000    mov       word [edi+96h],10h
  004d22f3  83cd40                or        ebp,40h
  004d22f6  57                    push      edi
  004d22f7  892f                  mov       [edi],ebp
  004d22f9  8b4c2428              mov       ecx,[esp+28h]
  004d22fd  56                    push      esi
  004d22fe  668b4130              mov       ax,[ecx+30h]
  004d2302  0fbfe8                movsx     ebp,ax
  004d2305  66d1f8                sar       ax,1
  004d2308  66894130              mov       [ecx+30h],ax
  004d230c  e86f780600            call      00539B80h
  004d2311  8b54242c              mov       edx,[esp+2Ch]
  004d2315  83c408                add       esp,8
  004d2318  8d471c                lea       eax,[edi+1Ch]
  004d231b  50                    push      eax
  004d231c  6800180000            push      1800h
  004d2321  6800030000            push      300h
  004d2326  6a03                  push      3
  004d2328  66896a30              mov       [edx+30h],bp
  004d232c  e8efb70600            call      0053DB20h
  004d2331  8b07                  mov       eax,[edi]
  004d2333  83c410                add       esp,10h
  004d2336  a900000040            test      eax,40000000h
  004d233b  7549                  jne       short 004D2386h
  004d233d  6aff                  push      0FFFFFFFFh
  004d233f  6a00                  push      0
  004d2341  6a39                  push      39h
  004d2343  56                    push      esi
  004d2344  e88786faff            call      0047A9D0h
  004d2349  8b2f                  mov       ebp,[edi]
  004d234b  83c410                add       esp,10h
  004d234e  83cd01                or        ebp,1
  004d2351  892f                  mov       [edi],ebp
  004d2353  0fbf4e32              movsx     ecx,word [esi+32h]
  004d2357  51                    push      ecx
  004d2358  e8bf360700            call      00545A1Ch
  004d235d  8b565c                mov       edx,[esi+5Ch]
  004d2360  c1fa0d                sar       edx,0Dh
  004d2363  0fafc2                imul      eax,edx
  004d2366  894750                mov       [edi+50h],eax
  004d2369  0fbf4632              movsx     eax,word [esi+32h]
  004d236d  50                    push      eax
  004d236e  e8a3360700            call      00545A16h
  004d2373  8b4e5c                mov       ecx,[esi+5Ch]
  004d2376  c7475400800100        mov       dword [edi+54h],18000h
  004d237d  c1f90d                sar       ecx,0Dh
  004d2380  0fafc1                imul      eax,ecx
  004d2383  894758                mov       [edi+58h],eax

LAB_004d2386:
  004d2386  8b06                  mov       eax,[esi]
  004d2388  24fd                  and       al,0FDh
  004d238a  33ed                  xor       ebp,ebp
  004d238c  8906                  mov       [esi],eax
  004d238e  8b442410              mov       eax,[esp+10h]
  004d2392  eb4a                  jmp       short 004D23DEh

LAB_004d2394:
  004d2394  f6c101                test      cl,1
  004d2397  7439                  je        short 004D23D2h
  004d2399  66837e1a03            cmp       word [esi+1Ah],3
  004d239e  7d32                  jge       short 004D23D2h
  004d23a0  e89b4d0700            call      00547140h
  004d23a5  99                    cdq
  004d23a6  b93c000000            mov       ecx,3Ch
  004d23ab  f7f9                  idiv      ecx
  004d23ad  8b442410              mov       eax,[esp+10h]
  004d23b1  3bd0                  cmp       edx,eax
  004d23b3  7d21                  jge       short 004D23D6h
  004d23b5  8b16                  mov       edx,[esi]
  004d23b7  66c746180300          mov       word [esi+18h],3
  004d23bd  81e2fffffffe          and       edx,0FEFFFFFFh
  004d23c3  66c7461a0000          mov       word [esi+1Ah],0
  004d23c9  83ca02                or        edx,2
  004d23cc  33ed                  xor       ebp,ebp
  004d23ce  8916                  mov       [esi],edx
  004d23d0  eb0c                  jmp       short 004D23DEh

LAB_004d23d2:
  004d23d2  8b442410              mov       eax,[esp+10h]

LAB_004d23d6:
  004d23d6  33ed                  xor       ebp,ebp
  004d23d8  eb04                  jmp       short 004D23DEh

LAB_004d23da:
  004d23da  8b442410              mov       eax,[esp+10h]

LAB_004d23de:
  004d23de  33c9                  xor       ecx,ecx
  004d23e0  8a4e42                mov       cl,[esi+42h]
  004d23e3  83f90d                cmp       ecx,0Dh
  004d23e6  0f8750050000          ja        near 004D293Ch
  004d23ec  ff248d802f4d00        jmp       dword [ecx*4+4D2F80h]
  004d23f3  66396e12              cmp       [esi+12h],bp
  004d23f7  751a                  jne       short 004D2413h
  004d23f9  8b06                  mov       eax,[esi]
  004d23fb  66896e18              mov       [esi+18h],bp
  004d23ff  0d00000001            or        eax,1000000h
  004d2404  66896e1a              mov       [esi+1Ah],bp
  004d2408  8906                  mov       [esi],eax
  004d240a  66c746120100          mov       word [esi+12h],1
  004d2410  896e5c                mov       [esi+5Ch],ebp

LAB_004d2413:
  004d2413  0fbf4610              movsx     eax,word [esi+10h]
  004d2417  39442414              cmp       [esp+14h],eax
  004d241b  7d09                  jge       short 004D2426h
  004d241d  c6464300              mov       byte [esi+43h],0
  004d2421  e916050000            jmp       004D293Ch

LAB_004d2426:
  004d2426  8a4643                mov       al,[esi+43h]
  004d2429  84c0                  test      al,al
  004d242b  0f8408050000          je        near 004D2939h
  004d2431  fec8                  dec       al
  004d2433  884643                mov       [esi+43h],al
  004d2436  e9fe040000            jmp       004D2939h
  004d243b  66396e12              cmp       [esi+12h],bp
  004d243f  751b                  jne       short 004D245Ch
  004d2441  8b16                  mov       edx,[esi]
  004d2443  b901000000            mov       ecx,1
  004d2448  81ca00000001          or        edx,1000000h
  004d244e  66894e18              mov       [esi+18h],cx
  004d2452  66896e1a              mov       [esi+1Ah],bp
  004d2456  8916                  mov       [esi],edx
  004d2458  66894e12              mov       [esi+12h],cx

LAB_004d245c:
  004d245c  8b4c241c              mov       ecx,[esp+1Ch]
  004d2460  8d91c8000000          lea       edx,[ecx+0C8h]
  004d2466  81e2ff0f0000          and       edx,0FFFh
  004d246c  81fa90010000          cmp       edx,190h
  004d2472  7f09                  jg        short 004D247Dh
  004d2474  c6464300              mov       byte [esi+43h],0
  004d2478  e9bc040000            jmp       004D2939h

LAB_004d247d:
  004d247d  81e1ff0f0000          and       ecx,0FFFh
  004d2483  81f900080000          cmp       ecx,800h
  004d2489  7d0f                  jge       short 004D249Ah
  004d248b  8d8880000000          lea       ecx,[eax+80h]
  004d2491  66014e32              add       [esi+32h],cx
  004d2495  e99f040000            jmp       004D2939h

LAB_004d249a:
  004d249a  b980ffffff            mov       ecx,0FFFFFF80h
  004d249f  2bc8                  sub       ecx,eax
  004d24a1  66014e32              add       [esi+32h],cx
  004d24a5  e98f040000            jmp       004D2939h
  004d24aa  66396e12              cmp       [esi+12h],bp
  004d24ae  b901000000            mov       ecx,1
  004d24b3  7525                  jne       short 004D24DAh
  004d24b5  8b16                  mov       edx,[esi]
  004d24b7  66894e18              mov       [esi+18h],cx
  004d24bb  81ca00000001          or        edx,1000000h
  004d24c1  66896e1a              mov       [esi+1Ah],bp
  004d24c5  8916                  mov       [esi],edx
  004d24c7  8d14c0                lea       edx,[eax+eax*8]
  004d24ca  c1e204                shl       edx,4
  004d24cd  81c200500000          add       edx,5000h
  004d24d3  66894e12              mov       [esi+12h],cx
  004d24d7  89565c                mov       [esi+5Ch],edx

LAB_004d24da:
  004d24da  66394e12              cmp       [esi+12h],cx
  004d24de  0f8558040000          jne       near 004D293Ch
  004d24e4  0fbf4e10              movsx     ecx,word [esi+10h]
  004d24e8  394c2414              cmp       [esp+14h],ecx
  004d24ec  7d09                  jge       short 004D24F7h
  004d24ee  c6464300              mov       byte [esi+43h],0
  004d24f2  e942040000            jmp       004D2939h

LAB_004d24f7:
  004d24f7  8a4e43                mov       cl,[esi+43h]
  004d24fa  84c9                  test      cl,cl
  004d24fc  7405                  je        short 004D2503h
  004d24fe  fec9                  dec       cl
  004d2500  884e43                mov       [esi+43h],cl

LAB_004d2503:
  004d2503  8b4c241c              mov       ecx,[esp+1Ch]
  004d2507  8d9100010000          lea       edx,[ecx+100h]
  004d250d  81e2ff0f0000          and       edx,0FFFh
  004d2513  81fa00020000          cmp       edx,200h
  004d2519  0f8e1d040000          jle       near 004D293Ch
  004d251f  81e1ff0f0000          and       ecx,0FFFh
  004d2525  81f900080000          cmp       ecx,800h
  004d252b  7d10                  jge       short 004D253Dh
  004d252d  8d840080000000        lea       eax,[eax+eax+80h]
  004d2534  66014632              add       [esi+32h],ax
  004d2538  e9ff030000            jmp       004D293Ch

LAB_004d253d:
  004d253d  8d0c00                lea       ecx,[eax+eax]
  004d2540  b880ffffff            mov       eax,0FFFFFF80h
  004d2545  2bc1                  sub       eax,ecx
  004d2547  66014632              add       [esi+32h],ax
  004d254b  e9ec030000            jmp       004D293Ch
  004d2550  bd01000000            mov       ebp,1
  004d2555  66396e12              cmp       [esi+12h],bp
  004d2559  0f859a000000          jne       near 004D25F9h
  004d255f  8a4e43                mov       cl,[esi+43h]
  004d2562  84c9                  test      cl,cl
  004d2564  7405                  je        short 004D256Bh
  004d2566  fec9                  dec       cl
  004d2568  884e43                mov       [esi+43h],cl

LAB_004d256b:
  004d256b  8b7c2420              mov       edi,[esp+20h]
  004d256f  8d14c0                lea       edx,[eax+eax*8]
  004d2572  c1e204                shl       edx,4
  004d2575  8d8200500000          lea       eax,[edx+5000h]
  004d257b  0faff8                imul      edi,eax
  004d257e  89465c                mov       [esi+5Ch],eax
  004d2581  0fbf4610              movsx     eax,word [esi+10h]
  004d2585  50                    push      eax
  004d2586  c1ff0c                sar       edi,0Ch
  004d2589  e88e340700            call      00545A1Ch
  004d258e  0fafc7                imul      eax,edi
  004d2591  8b0b                  mov       ecx,[ebx]
  004d2593  c1f80c                sar       eax,0Ch
  004d2596  03c8                  add       ecx,eax
  004d2598  890b                  mov       [ebx],ecx
  004d259a  0fbf4e10              movsx     ecx,word [esi+10h]
  004d259e  51                    push      ecx
  004d259f  e872340700            call      00545A16h
  004d25a4  0fafc7                imul      eax,edi
  004d25a7  8b5e24                mov       ebx,[esi+24h]
  004d25aa  c7465c00000000        mov       dword [esi+5Ch],0
  004d25b1  c1f80c                sar       eax,0Ch
  004d25b4  03d8                  add       ebx,eax
  004d25b6  8b44241c              mov       eax,[esp+1Ch]
  004d25ba  895e24                mov       [esi+24h],ebx
  004d25bd  8d9000010000          lea       edx,[eax+100h]
  004d25c3  81e2ff0f0000          and       edx,0FFFh
  004d25c9  81fa00020000          cmp       edx,200h
  004d25cf  7e28                  jle       short 004D25F9h
  004d25d1  25ff0f0000            and       eax,0FFFh
  004d25d6  3d00080000            cmp       eax,800h
  004d25db  7d0a                  jge       short 004D25E7h
  004d25dd  8b442410              mov       eax,[esp+10h]
  004d25e1  8d440032              lea       eax,[eax+eax+32h]
  004d25e5  eb0e                  jmp       short 004D25F5h

LAB_004d25e7:
  004d25e7  8b4c2410              mov       ecx,[esp+10h]
  004d25eb  b8ceffffff            mov       eax,0FFFFFFCEh
  004d25f0  8d1409                lea       edx,[ecx+ecx]
  004d25f3  2bc2                  sub       eax,edx

LAB_004d25f5:
  004d25f5  66014632              add       [esi+32h],ax

LAB_004d25f9:
  004d25f9  66837e1200            cmp       word [esi+12h],0
  004d25fe  0f8538030000          jne       near 004D293Ch
  004d2604  8b06                  mov       eax,[esi]
  004d2606  66896e18              mov       [esi+18h],bp
  004d260a  0d00000001            or        eax,1000000h
  004d260f  66c7461a0000          mov       word [esi+1Ah],0
  004d2615  8906                  mov       [esi],eax
  004d2617  66896e12              mov       [esi+12h],bp
  004d261b  e91c030000            jmp       004D293Ch
  004d2620  668b4e12              mov       cx,[esi+12h]
  004d2624  6683f901              cmp       cx,1
  004d2628  7514                  jne       short 004D263Eh
  004d262a  8a4643                mov       al,[esi+43h]
  004d262d  84c0                  test      al,al
  004d262f  740d                  je        short 004D263Eh
  004d2631  f70600000002          test      dword [esi],2000000h
  004d2637  7405                  je        short 004D263Eh
  004d2639  fec8                  dec       al
  004d263b  884643                mov       [esi+43h],al

LAB_004d263e:
  004d263e  663bcd                cmp       cx,bp
  004d2641  0f85f5020000          jne       near 004D293Ch
  004d2647  8b06                  mov       eax,[esi]
  004d2649  66c746120100          mov       word [esi+12h],1
  004d264f  25fffffffe            and       eax,0FEFFFFFFh
  004d2654  66c746180600          mov       word [esi+18h],6
  004d265a  66896e1a              mov       [esi+1Ah],bp
  004d265e  896e5c                mov       [esi+5Ch],ebp
  004d2661  8906                  mov       [esi],eax
  004d2663  e9d4020000            jmp       004D293Ch
  004d2668  66396e12              cmp       [esi+12h],bp
  004d266c  757d                  jne       short 004D26EBh
  004d266e  8b0e                  mov       ecx,[esi]
  004d2670  f6c101                test      cl,1
  004d2673  7406                  je        short 004D267Bh
  004d2675  c6464300              mov       byte [esi+43h],0
  004d2679  eb70                  jmp       short 004D26EBh

LAB_004d267b:
  004d267b  8b7c2420              mov       edi,[esp+20h]
  004d267f  0500010000            add       eax,100h
  004d2684  c1e006                shl       eax,6
  004d2687  0faff8                imul      edi,eax
  004d268a  89465c                mov       [esi+5Ch],eax
  004d268d  81e1fffffffe          and       ecx,0FEFFFFFFh
  004d2693  0fbf4610              movsx     eax,word [esi+10h]
  004d2697  bb01000000            mov       ebx,1
  004d269c  50                    push      eax
  004d269d  890e                  mov       [esi],ecx
  004d269f  66895e18              mov       [esi+18h],bx
  004d26a3  66896e1a              mov       [esi+1Ah],bp
  004d26a7  c1ff0c                sar       edi,0Ch
  004d26aa  e86d330700            call      00545A1Ch
  004d26af  0fafc7                imul      eax,edi
  004d26b2  8b4e50                mov       ecx,[esi+50h]
  004d26b5  c1f80c                sar       eax,0Ch
  004d26b8  03c8                  add       ecx,eax
  004d26ba  894e50                mov       [esi+50h],ecx
  004d26bd  0fbf4e10              movsx     ecx,word [esi+10h]
  004d26c1  51                    push      ecx
  004d26c2  e84f330700            call      00545A16h
  004d26c7  0fafc7                imul      eax,edi
  004d26ca  8b5658                mov       edx,[esi+58h]
  004d26cd  8b4e54                mov       ecx,[esi+54h]
  004d26d0  c1f80c                sar       eax,0Ch
  004d26d3  03d0                  add       edx,eax
  004d26d5  8b06                  mov       eax,[esi]
  004d26d7  81c100e00100          add       ecx,1E000h
  004d26dd  0bc3                  or        eax,ebx
  004d26df  895658                mov       [esi+58h],edx
  004d26e2  894e54                mov       [esi+54h],ecx
  004d26e5  8906                  mov       [esi],eax
  004d26e7  66895e12              mov       [esi+12h],bx

LAB_004d26eb:
  004d26eb  f60601                test      byte [esi],1
  004d26ee  0f8548020000          jne       near 004D293Ch
  004d26f4  c6464300              mov       byte [esi+43h],0
  004d26f8  e93f020000            jmp       004D293Ch
  004d26fd  668b5612              mov       dx,[esi+12h]
  004d2701  bf01000000            mov       edi,1
  004d2706  663bd7                cmp       dx,di
  004d2709  751c                  jne       short 004D2727h
  004d270b  0fbf4e10              movsx     ecx,word [esi+10h]
  004d270f  394c2414              cmp       [esp+14h],ecx
  004d2713  7d06                  jge       short 004D271Bh
  004d2715  c6464300              mov       byte [esi+43h],0
  004d2719  eb0c                  jmp       short 004D2727h

LAB_004d271b:
  004d271b  8a4e43                mov       cl,[esi+43h]
  004d271e  84c9                  test      cl,cl
  004d2720  7405                  je        short 004D2727h
  004d2722  fec9                  dec       cl
  004d2724  884e43                mov       [esi+43h],cl

LAB_004d2727:
  004d2727  663bd5                cmp       dx,bp
  004d272a  0f850c020000          jne       near 004D293Ch
  004d2730  8b16                  mov       edx,[esi]
  004d2732  66897e18              mov       [esi+18h],di
  004d2736  81ca00000001          or        edx,1000000h
  004d273c  66896e1a              mov       [esi+1Ah],bp
  004d2740  8916                  mov       [esi],edx
  004d2742  8d14c0                lea       edx,[eax+eax*8]
  004d2745  c1e204                shl       edx,4
  004d2748  81c200500000          add       edx,5000h
  004d274e  66897e12              mov       [esi+12h],di
  004d2752  89565c                mov       [esi+5Ch],edx
  004d2755  e9e2010000            jmp       004D293Ch
  004d275a  bf01000000            mov       edi,1
  004d275f  66397e12              cmp       [esi+12h],di
  004d2763  7538                  jne       short 004D279Dh
  004d2765  66837e1a08            cmp       word [esi+1Ah],8
  004d276a  7531                  jne       short 004D279Dh
  004d276c  66837e1602            cmp       word [esi+16h],2
  004d2771  7512                  jne       short 004D2785h
  004d2773  6aff                  push      0FFFFFFFFh
  004d2775  55                    push      ebp
  004d2776  6a36                  push      36h
  004d2778  56                    push      esi
  004d2779  e85282faff            call      0047A9D0h
  004d277e  8b442420              mov       eax,[esp+20h]
  004d2782  83c410                add       esp,10h

LAB_004d2785:
  004d2785  66397e16              cmp       [esi+16h],di
  004d2789  7512                  jne       short 004D279Dh
  004d278b  6aff                  push      0FFFFFFFFh
  004d278d  55                    push      ebp
  004d278e  6a4b                  push      4Bh
  004d2790  56                    push      esi
  004d2791  e83a82faff            call      0047A9D0h
  004d2796  8b442420              mov       eax,[esp+20h]
  004d279a  83c410                add       esp,10h

LAB_004d279d:
  004d279d  8b0e                  mov       ecx,[esi]
  004d279f  f7c100000002          test      ecx,2000000h
  004d27a5  7404                  je        short 004D27ABh
  004d27a7  c6464300              mov       byte [esi+43h],0

LAB_004d27ab:
  004d27ab  66396e12              cmp       [esi+12h],bp
  004d27af  0f8587010000          jne       near 004D293Ch
  004d27b5  81e1fffffffe          and       ecx,0FEFFFFFFh
  004d27bb  83f81e                cmp       eax,1Eh
  004d27be  66897e12              mov       [esi+12h],di
  004d27c2  66c746180500          mov       word [esi+18h],5
  004d27c8  66896e1a              mov       [esi+1Ah],bp
  004d27cc  890e                  mov       [esi],ecx
  004d27ce  7d03                  jge       short 004D27D3h
  004d27d0  896e5c                mov       [esi+5Ch],ebp

LAB_004d27d3:
  004d27d3  66396e16              cmp       [esi+16h],bp
  004d27d7  0f855f010000          jne       near 004D293Ch
  004d27dd  c6464300              mov       byte [esi+43h],0
  004d27e1  e956010000            jmp       004D293Ch
  004d27e6  668b4e12              mov       cx,[esi+12h]
  004d27ea  6683f901              cmp       cx,1
  004d27ee  7511                  jne       short 004D2801h
  004d27f0  8b06                  mov       eax,[esi]
  004d27f2  a900000002            test      eax,2000000h
  004d27f7  7408                  je        short 004D2801h
  004d27f9  24fd                  and       al,0FDh
  004d27fb  c6464200              mov       byte [esi+42h],0
  004d27ff  8906                  mov       [esi],eax

LAB_004d2801:
  004d2801  663bcd                cmp       cx,bp
  004d2804  0f8532010000          jne       near 004D293Ch
  004d280a  8b06                  mov       eax,[esi]
  004d280c  66c746120100          mov       word [esi+12h],1
  004d2812  25fffffffe            and       eax,0FEFFFFFFh
  004d2817  8906                  mov       [esi],eax
  004d2819  e91e010000            jmp       004D293Ch
  004d281e  668b4e12              mov       cx,[esi+12h]
  004d2822  6683f902              cmp       cx,2
  004d2826  7511                  jne       short 004D2839h
  004d2828  8b06                  mov       eax,[esi]
  004d282a  a900000002            test      eax,2000000h
  004d282f  7408                  je        short 004D2839h
  004d2831  24fd                  and       al,0FDh
  004d2833  c6464300              mov       byte [esi+43h],0
  004d2837  8906                  mov       [esi],eax

LAB_004d2839:
  004d2839  6683f901              cmp       cx,1
  004d283d  7522                  jne       short 004D2861h
  004d283f  8b06                  mov       eax,[esi]
  004d2841  a900000002            test      eax,2000000h
  004d2846  7419                  je        short 004D2861h
  004d2848  25fffffffe            and       eax,0FEFFFFFFh
  004d284d  66c746120200          mov       word [esi+12h],2
  004d2853  8906                  mov       [esi],eax
  004d2855  66c746180500          mov       word [esi+18h],5
  004d285b  66c7461a0700          mov       word [esi+1Ah],7

LAB_004d2861:
  004d2861  66396e12              cmp       [esi+12h],bp
  004d2865  0f85d1000000          jne       near 004D293Ch
  004d286b  8b06                  mov       eax,[esi]
  004d286d  66c746120100          mov       word [esi+12h],1
  004d2873  25fffffffe            and       eax,0FEFFFFFFh
  004d2878  66c746180300          mov       word [esi+18h],3
  004d287e  0c02                  or        al,2
  004d2880  66896e1a              mov       [esi+1Ah],bp
  004d2884  8906                  mov       [esi],eax
  004d2886  e9ae000000            jmp       004D2939h
  004d288b  668b5612              mov       dx,[esi+12h]
  004d288f  6683fa02              cmp       dx,2
  004d2893  7523                  jne       short 004D28B8h
  004d2895  8b4e5c                mov       ecx,[esi+5Ch]
  004d2898  8bf9                  mov       edi,ecx
  004d289a  c1ff04                sar       edi,4
  004d289d  2bcf                  sub       ecx,edi
  004d289f  81f900a00000          cmp       ecx,0A000h
  004d28a5  894e5c                mov       [esi+5Ch],ecx
  004d28a8  7d0e                  jge       short 004D28B8h
  004d28aa  8b0e                  mov       ecx,[esi]
  004d28ac  c6464300              mov       byte [esi+43h],0
  004d28b0  83e1fd                and       ecx,0FFFFFFFDh
  004d28b3  896e5c                mov       [esi+5Ch],ebp
  004d28b6  890e                  mov       [esi],ecx

LAB_004d28b8:
  004d28b8  6683fa01              cmp       dx,1
  004d28bc  bf04000000            mov       edi,4
  004d28c1  7559                  jne       short 004D291Ch
  004d28c3  8b4c241c              mov       ecx,[esp+1Ch]
  004d28c7  8d9100010000          lea       edx,[ecx+100h]
  004d28cd  81e2ff0f0000          and       edx,0FFFh
  004d28d3  81fa00020000          cmp       edx,200h
  004d28d9  7e22                  jle       short 004D28FDh
  004d28db  81e1ff0f0000          and       ecx,0FFFh
  004d28e1  81f900080000          cmp       ecx,800h
  004d28e7  7d06                  jge       short 004D28EFh
  004d28e9  8d440032              lea       eax,[eax+eax+32h]
  004d28ed  eb0a                  jmp       short 004D28F9h

LAB_004d28ef:
  004d28ef  8d0c00                lea       ecx,[eax+eax]
  004d28f2  b8ceffffff            mov       eax,0FFFFFFCEh
  004d28f7  2bc1                  sub       eax,ecx

LAB_004d28f9:
  004d28f9  66014632              add       [esi+32h],ax

LAB_004d28fd:
  004d28fd  66397e1a              cmp       [esi+1Ah],di
  004d2901  7c19                  jl        short 004D291Ch
  004d2903  0fbf5616              movsx     edx,word [esi+16h]
  004d2907  8b06                  mov       eax,[esi]
  004d2909  83c20f                add       edx,0Fh
  004d290c  c1e20d                shl       edx,0Dh
  004d290f  0c02                  or        al,2
  004d2911  66c746120200          mov       word [esi+12h],2
  004d2917  89565c                mov       [esi+5Ch],edx
  004d291a  8906                  mov       [esi],eax

LAB_004d291c:
  004d291c  66396e12              cmp       [esi+12h],bp
  004d2920  751a                  jne       short 004D293Ch
  004d2922  8b06                  mov       eax,[esi]
  004d2924  66c746120100          mov       word [esi+12h],1
  004d292a  25fffffffe            and       eax,0FEFFFFFFh
  004d292f  66897e18              mov       [esi+18h],di
  004d2933  66896e1a              mov       [esi+1Ah],bp
  004d2937  8906                  mov       [esi],eax

LAB_004d2939:
  004d2939  896e5c                mov       [esi+5Ch],ebp

LAB_004d293c:
  004d293c  0fbf4632              movsx     eax,word [esi+32h]
  004d2940  50                    push      eax
  004d2941  e8d6300700            call      00545A1Ch
  004d2946  0fbf4e32              movsx     ecx,word [esi+32h]
  004d294a  51                    push      ecx
  004d294b  89842480000000        mov       [esp+80h],eax
  004d2952  e8bf300700            call      00545A16h
  004d2957  8b4e50                mov       ecx,[esi+50h]
  004d295a  89842484000000        mov       [esp+84h],eax
  004d2961  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004d2968  8bc1                  mov       eax,ecx
  004d296a  8bd1                  mov       edx,ecx
  004d296c  c1f808                sar       eax,8
  004d296f  8b5c2420              mov       ebx,[esp+20h]
  004d2973  2bd0                  sub       edx,eax
  004d2975  c1f903                sar       ecx,3
  004d2978  0faf5e5c              imul      ebx,[esi+5Ch]
  004d297c  2bd1                  sub       edx,ecx
  004d297e  8b4e54                mov       ecx,[esi+54h]
  004d2981  8bc1                  mov       eax,ecx
  004d2983  8bef                  mov       ebp,edi
  004d2985  c1f808                sar       eax,8
  004d2988  c1fd08                sar       ebp,8
  004d298b  2be8                  sub       ebp,eax
  004d298d  895650                mov       [esi+50h],edx
  004d2990  2bef                  sub       ebp,edi
  004d2992  56                    push      esi
  004d2993  03e9                  add       ebp,ecx
  004d2995  8b4e58                mov       ecx,[esi+58h]
  004d2998  8bc1                  mov       eax,ecx
  004d299a  8bf9                  mov       edi,ecx
  004d299c  c1f803                sar       eax,3
  004d299f  2bf8                  sub       edi,eax
  004d29a1  8b4620                mov       eax,[esi+20h]
  004d29a4  c1f908                sar       ecx,8
  004d29a7  c1fb0c                sar       ebx,0Ch
  004d29aa  2bf9                  sub       edi,ecx
  004d29ac  8bcb                  mov       ecx,ebx
  004d29ae  0faf8c2480000000      imul      ecx,[esp+80h]
  004d29b6  c1f90c                sar       ecx,0Ch
  004d29b9  03ca                  add       ecx,edx
  004d29bb  8b561c                mov       edx,[esi+1Ch]
  004d29be  03d1                  add       edx,ecx
  004d29c0  03c5                  add       eax,ebp
  004d29c2  89561c                mov       [esi+1Ch],edx
  004d29c5  8bd3                  mov       edx,ebx
  004d29c7  0faf942488000000      imul      edx,[esp+88h]
  004d29cf  c1fa0c                sar       edx,0Ch
  004d29d2  894620                mov       [esi+20h],eax
  004d29d5  8b4624                mov       eax,[esi+24h]
  004d29d8  03d7                  add       edx,edi
  004d29da  896e54                mov       [esi+54h],ebp
  004d29dd  03c2                  add       eax,edx
  004d29df  897e58                mov       [esi+58h],edi
  004d29e2  894624                mov       [esi+24h],eax
  004d29e5  e8560f0600            call      00533940h
  004d29ea  8b9690000000          mov       edx,[esi+90h]
  004d29f0  8b4660                mov       eax,[esi+60h]
  004d29f3  8b4e20                mov       ecx,[esi+20h]
  004d29f6  83c404                add       esp,4
  004d29f9  80e6fc                and       dh,0FCh
  004d29fc  3bc8                  cmp       ecx,eax
  004d29fe  899690000000          mov       [esi+90h],edx
  004d2a04  7f2a                  jg        short 004D2A30h
  004d2a06  f6c680                test      dh,80h
  004d2a09  7525                  jne       short 004D2A30h
  004d2a0b  8b0e                  mov       ecx,[esi]
  004d2a0d  894620                mov       [esi+20h],eax
  004d2a10  f6c101                test      cl,1
  004d2a13  740f                  je        short 004D2A24h
  004d2a15  83e1fe                and       ecx,0FFFFFFFEh
  004d2a18  33ff                  xor       edi,edi
  004d2a1a  890e                  mov       [esi],ecx
  004d2a1c  897e54                mov       [esi+54h],edi
  004d2a1f  e924010000            jmp       004D2B48h

LAB_004d2a24:
  004d2a24  83e1fe                and       ecx,0FFFFFFFEh
  004d2a27  33ff                  xor       edi,edi
  004d2a29  890e                  mov       [esi],ecx
  004d2a2b  897e54                mov       [esi+54h],edi
  004d2a2e  eb02                  jmp       short 004D2A32h

LAB_004d2a30:
  004d2a30  33ff                  xor       edi,edi

LAB_004d2a32:
  004d2a32  8b0e                  mov       ecx,[esi]
  004d2a34  f6c101                test      cl,1
  004d2a37  750a                  jne       short 004D2A43h
  004d2a39  394620                cmp       [esi+20h],eax
  004d2a3c  7e05                  jle       short 004D2A43h
  004d2a3e  83c901                or        ecx,1
  004d2a41  890e                  mov       [esi],ecx

LAB_004d2a43:
  004d2a43  f7c282ad0020          test      edx,2000AD82h
  004d2a49  0f84f9000000          je        near 004D2B48h
  004d2a4f  f60601                test      byte [esi],1
  004d2a52  0f85c7000000          jne       near 004D2B1Fh
  004d2a58  807e4207              cmp       byte [esi+42h],7
  004d2a5c  7421                  je        short 004D2A7Fh
  004d2a5e  c6464207              mov       byte [esi+42h],7
  004d2a62  e8d9460700            call      00547140h
  004d2a67  99                    cdq
  004d2a68  b928000000            mov       ecx,28h
  004d2a6d  66897e12              mov       [esi+12h],di
  004d2a71  f7f9                  idiv      ecx
  004d2a73  66c746105000          mov       word [esi+10h],50h
  004d2a79  80c214                add       dl,14h
  004d2a7c  885643                mov       [esi+43h],dl

LAB_004d2a7f:
  004d2a7f  8b16                  mov       edx,[esi]
  004d2a81  8b44242c              mov       eax,[esp+2Ch]
  004d2a85  8b4c2430              mov       ecx,[esp+30h]
  004d2a89  83e2fd                and       edx,0FFFFFFFDh
  004d2a8c  8916                  mov       [esi],edx
  004d2a8e  8b542428              mov       edx,[esp+28h]
  004d2a92  894620                mov       [esi+20h],eax
  004d2a95  8b442470              mov       eax,[esp+70h]
  004d2a99  89561c                mov       [esi+1Ch],edx
  004d2a9c  8b542468              mov       edx,[esp+68h]
  004d2aa0  25ffff0000            and       eax,0FFFFh
  004d2aa5  894e24                mov       [esi+24h],ecx
  004d2aa8  8b4c244c              mov       ecx,[esp+4Ch]
  004d2aac  89567c                mov       [esi+7Ch],edx
  004d2aaf  8b542460              mov       edx,[esp+60h]
  004d2ab3  898690000000          mov       [esi+90h],eax
  004d2ab9  8b442464              mov       eax,[esp+64h]
  004d2abd  898e84000000          mov       [esi+84h],ecx
  004d2ac3  8b4c246c              mov       ecx,[esp+6Ch]
  004d2ac7  898680000000          mov       [esi+80h],eax
  004d2acd  8b44245c              mov       eax,[esp+5Ch]
  004d2ad1  899688000000          mov       [esi+88h],edx
  004d2ad7  8b542454              mov       edx,[esp+54h]
  004d2adb  6882ad0020            push      2000AD82h
  004d2ae0  56                    push      esi
  004d2ae1  89868c000000          mov       [esi+8Ch],eax
  004d2ae7  894e64                mov       [esi+64h],ecx
  004d2aea  895660                mov       [esi+60h],edx
  004d2aed  e86e330000            call      004D5E60h
  004d2af2  83c408                add       esp,8
  004d2af5  663bc7                cmp       ax,di
  004d2af8  7515                  jne       short 004D2B0Fh
  004d2afa  e841460700            call      00547140h
  004d2aff  99                    cdq
  004d2b00  33c2                  xor       eax,edx
  004d2b02  2bc2                  sub       eax,edx
  004d2b04  83e001                and       eax,1
  004d2b07  33c2                  xor       eax,edx
  004d2b09  2bc2                  sub       eax,edx
  004d2b0b  8d4400ff              lea       eax,[eax+eax-1]

LAB_004d2b0f:
  004d2b0f  8b4c2410              mov       ecx,[esp+10h]
  004d2b13  83c140                add       ecx,40h
  004d2b16  0fafc8                imul      ecx,eax
  004d2b19  66014e32              add       [esi+32h],cx
  004d2b1d  eb29                  jmp       short 004D2B48h

LAB_004d2b1f:
  004d2b1f  8b542428              mov       edx,[esp+28h]
  004d2b23  8b442430              mov       eax,[esp+30h]
  004d2b27  897e5c                mov       [esi+5Ch],edi
  004d2b2a  897e50                mov       [esi+50h],edi
  004d2b2d  897e58                mov       [esi+58h],edi
  004d2b30  89561c                mov       [esi+1Ch],edx
  004d2b33  894624                mov       [esi+24h],eax
  004d2b36  c6464200              mov       byte [esi+42h],0
  004d2b3a  c6464328              mov       byte [esi+43h],28h
  004d2b3e  66897e12              mov       [esi+12h],di
  004d2b42  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004d2b48:
  004d2b48  6aff                  push      0FFFFFFFFh
  004d2b4a  6aff                  push      0FFFFFFFFh
  004d2b4c  6aff                  push      0FFFFFFFFh
  004d2b4e  56                    push      esi
  004d2b4f  e8bc0cffff            call      004C3810h
  004d2b54  8b442430              mov       eax,[esp+30h]
  004d2b58  83c410                add       esp,10h
  004d2b5b  3bc7                  cmp       eax,edi
  004d2b5d  7517                  jne       short 004D2B76h
  004d2b5f  668b8c2492000000      mov       cx,[esp+92h]
  004d2b67  66894e32              mov       [esi+32h],cx
  004d2b6b  5f                    pop       edi
  004d2b6c  5e                    pop       esi
  004d2b6d  5d                    pop       ebp
  004d2b6e  5b                    pop       ebx
  004d2b6f  81c494000000          add       esp,94h
  004d2b75  c3                    ret

LAB_004d2b76:
  004d2b76  8a4643                mov       al,[esi+43h]
  004d2b79  84c0                  test      al,al
  004d2b7b  0f857a030000          jne       near 004D2EFBh
  004d2b81  8b466c                mov       eax,[esi+6Ch]
  004d2b84  8b561c                mov       edx,[esi+1Ch]
  004d2b87  8b781c                mov       edi,[eax+1Ch]
  004d2b8a  8b6824                mov       ebp,[eax+24h]
  004d2b8d  8b4624                mov       eax,[esi+24h]
  004d2b90  2bfa                  sub       edi,edx
  004d2b92  2be8                  sub       ebp,eax
  004d2b94  c1ff0c                sar       edi,0Ch
  004d2b97  c1fd0c                sar       ebp,0Ch
  004d2b9a  8bd7                  mov       edx,edi
  004d2b9c  8bc5                  mov       eax,ebp
  004d2b9e  0fafd7                imul      edx,edi
  004d2ba1  0fafc5                imul      eax,ebp
  004d2ba4  03d0                  add       edx,eax
  004d2ba6  52                    push      edx
  004d2ba7  e8642e0700            call      00545A10h
  004d2bac  55                    push      ebp
  004d2bad  57                    push      edi
  004d2bae  8944241c              mov       [esp+1Ch],eax
  004d2bb2  e86b2e0700            call      00545A22h
  004d2bb7  668b4e32              mov       cx,[esi+32h]
  004d2bbb  66c746120000          mov       word [esi+12h],0
  004d2bc1  2bc1                  sub       eax,ecx
  004d2bc3  25ff0f0000            and       eax,0FFFh
  004d2bc8  8944241c              mov       [esp+1Ch],eax
  004d2bcc  8b442414              mov       eax,[esp+14h]
  004d2bd0  3dfa000000            cmp       eax,0FAh
  004d2bd5  0f8dee000000          jge       near 004D2CC9h
  004d2bdb  e860450700            call      00547140h
  004d2be0  99                    cdq
  004d2be1  b964000000            mov       ecx,64h
  004d2be6  f7f9                  idiv      ecx
  004d2be8  8bda                  mov       ebx,edx
  004d2bea  83fb32                cmp       ebx,32h
  004d2bed  7d7d                  jge       short 004D2C6Ch
  004d2bef  8b7c2418              mov       edi,[esp+18h]
  004d2bf3  8b54241c              mov       edx,[esp+1Ch]
  004d2bf7  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004d2bfe  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004d2c05  25ff0f0000            and       eax,0FFFh
  004d2c0a  3bc1                  cmp       eax,ecx
  004d2c0c  7f52                  jg        short 004D2C60h
  004d2c0e  e82d450700            call      00547140h
  004d2c13  99                    cdq
  004d2c14  8d4c3f01              lea       ecx,[edi+edi+1]
  004d2c18  f7f9                  idiv      ecx
  004d2c1a  8b442414              mov       eax,[esp+14h]
  004d2c1e  83c264                add       edx,64h
  004d2c21  3bc2                  cmp       eax,edx
  004d2c23  7d3b                  jge       short 004D2C60h
  004d2c25  83fb1e                cmp       ebx,1Eh
  004d2c28  7d26                  jge       short 004D2C50h
  004d2c2a  8b16                  mov       edx,[esi]
  004d2c2c  c646420b              mov       byte [esi+42h],0Bh
  004d2c30  81e2fffffffe          and       edx,0FEFFFFFFh
  004d2c36  c6464301              mov       byte [esi+43h],1
  004d2c3a  83ca02                or        edx,2
  004d2c3d  33ed                  xor       ebp,ebp
  004d2c3f  8916                  mov       [esi],edx
  004d2c41  896e5c                mov       [esi+5Ch],ebp
  004d2c44  66c746180300          mov       word [esi+18h],3
  004d2c4a  66896e1a              mov       [esi+1Ah],bp
  004d2c4e  eb7f                  jmp       short 004D2CCFh

LAB_004d2c50:
  004d2c50  33ed                  xor       ebp,ebp
  004d2c52  c646420c              mov       byte [esi+42h],0Ch
  004d2c56  c6464301              mov       byte [esi+43h],1
  004d2c5a  66896e12              mov       [esi+12h],bp
  004d2c5e  eb6f                  jmp       short 004D2CCFh

LAB_004d2c60:
  004d2c60  c6464201              mov       byte [esi+42h],1
  004d2c64  c6464301              mov       byte [esi+43h],1
  004d2c68  33ed                  xor       ebp,ebp
  004d2c6a  eb63                  jmp       short 004D2CCFh

LAB_004d2c6c:
  004d2c6c  83fb3c                cmp       ebx,3Ch
  004d2c6f  7c2d                  jl        short 004D2C9Eh
  004d2c71  83fb46                cmp       ebx,46h
  004d2c74  7d28                  jge       short 004D2C9Eh
  004d2c76  8b4c2410              mov       ecx,[esp+10h]
  004d2c7a  8b442414              mov       eax,[esp+14h]
  004d2c7e  83c164                add       ecx,64h
  004d2c81  3bc1                  cmp       eax,ecx
  004d2c83  7e19                  jle       short 004D2C9Eh
  004d2c85  c6464206              mov       byte [esi+42h],6
  004d2c89  c6464301              mov       byte [esi+43h],1
  004d2c8d  e8ae440700            call      00547140h
  004d2c92  33ed                  xor       ebp,ebp
  004d2c94  66894610              mov       [esi+10h],ax
  004d2c98  66896e12              mov       [esi+12h],bp
  004d2c9c  eb2d                  jmp       short 004D2CCBh

LAB_004d2c9e:
  004d2c9e  c6464202              mov       byte [esi+42h],2
  004d2ca2  e899440700            call      00547140h
  004d2ca7  99                    cdq
  004d2ca8  b90a000000            mov       ecx,0Ah
  004d2cad  8b7c2418              mov       edi,[esp+18h]
  004d2cb1  f7f9                  idiv      ecx
  004d2cb3  8bc7                  mov       eax,edi
  004d2cb5  66c746105000          mov       word [esi+10h],50h
  004d2cbb  d1f8                  sar       eax,1
  004d2cbd  02d0                  add       dl,al
  004d2cbf  80c214                add       dl,14h
  004d2cc2  33ed                  xor       ebp,ebp
  004d2cc4  885643                mov       [esi+43h],dl
  004d2cc7  eb06                  jmp       short 004D2CCFh

LAB_004d2cc9:
  004d2cc9  33ed                  xor       ebp,ebp

LAB_004d2ccb:
  004d2ccb  8b7c2418              mov       edi,[esp+18h]

LAB_004d2ccf:
  004d2ccf  8b442414              mov       eax,[esp+14h]
  004d2cd3  3d58020000            cmp       eax,258h
  004d2cd8  0f8dda000000          jge       near 004D2DB8h
  004d2cde  3dfa000000            cmp       eax,0FAh
  004d2ce3  0f8ccf000000          jl        near 004D2DB8h
  004d2ce9  e852440700            call      00547140h
  004d2cee  99                    cdq
  004d2cef  b964000000            mov       ecx,64h
  004d2cf4  f7f9                  idiv      ecx
  004d2cf6  8bda                  mov       ebx,edx
  004d2cf8  83fb14                cmp       ebx,14h
  004d2cfb  7d2e                  jge       short 004D2D2Bh
  004d2cfd  8b54241c              mov       edx,[esp+1Ch]
  004d2d01  81c200040000          add       edx,400h
  004d2d07  81e2ff0f0000          and       edx,0FFFh
  004d2d0d  81fa00080000          cmp       edx,800h
  004d2d13  7c0d                  jl        short 004D2D22h
  004d2d15  c6464201              mov       byte [esi+42h],1
  004d2d19  c6464301              mov       byte [esi+43h],1
  004d2d1d  e996000000            jmp       004D2DB8h

LAB_004d2d22:
  004d2d22  c646420d              mov       byte [esi+42h],0Dh
  004d2d26  e985000000            jmp       004D2DB0h

LAB_004d2d2b:
  004d2d2b  83fb28                cmp       ebx,28h
  004d2d2e  7d25                  jge       short 004D2D55h
  004d2d30  c6464202              mov       byte [esi+42h],2
  004d2d34  e807440700            call      00547140h
  004d2d39  99                    cdq
  004d2d3a  b90a000000            mov       ecx,0Ah
  004d2d3f  66c746105000          mov       word [esi+10h],50h
  004d2d45  f7f9                  idiv      ecx
  004d2d47  8bc7                  mov       eax,edi
  004d2d49  d1f8                  sar       eax,1
  004d2d4b  02d0                  add       dl,al
  004d2d4d  80c214                add       dl,14h
  004d2d50  885643                mov       [esi+43h],dl
  004d2d53  eb63                  jmp       short 004D2DB8h

LAB_004d2d55:
  004d2d55  83fb3c                cmp       ebx,3Ch
  004d2d58  7d13                  jge       short 004D2D6Dh
  004d2d5a  c6464206              mov       byte [esi+42h],6
  004d2d5e  c6464301              mov       byte [esi+43h],1
  004d2d62  e8d9430700            call      00547140h
  004d2d67  66894610              mov       [esi+10h],ax
  004d2d6b  eb47                  jmp       short 004D2DB4h

LAB_004d2d6d:
  004d2d6d  83fb50                cmp       ebx,50h
  004d2d70  7d3a                  jge       short 004D2DACh
  004d2d72  c6464203              mov       byte [esi+42h],3
  004d2d76  66896e12              mov       [esi+12h],bp
  004d2d7a  e8c1430700            call      00547140h
  004d2d7f  99                    cdq
  004d2d80  b90a000000            mov       ecx,0Ah
  004d2d85  f7f9                  idiv      ecx
  004d2d87  8bc7                  mov       eax,edi
  004d2d89  d1f8                  sar       eax,1
  004d2d8b  02d0                  add       dl,al
  004d2d8d  80c214                add       dl,14h
  004d2d90  885643                mov       [esi+43h],dl
  004d2d93  e8a8430700            call      00547140h
  004d2d98  99                    cdq
  004d2d99  33c2                  xor       eax,edx
  004d2d9b  2bc2                  sub       eax,edx
  004d2d9d  25ff0f0000            and       eax,0FFFh
  004d2da2  33c2                  xor       eax,edx
  004d2da4  2bc2                  sub       eax,edx
  004d2da6  66894610              mov       [esi+10h],ax
  004d2daa  eb0c                  jmp       short 004D2DB8h

LAB_004d2dac:
  004d2dac  c646420a              mov       byte [esi+42h],0Ah

LAB_004d2db0:
  004d2db0  c6464301              mov       byte [esi+43h],1

LAB_004d2db4:
  004d2db4  66896e12              mov       [esi+12h],bp

LAB_004d2db8:
  004d2db8  817c241458020000      cmp       dword [esp+14h],258h
  004d2dc0  0f8c35010000          jl        near 004D2EFBh
  004d2dc6  e875430700            call      00547140h
  004d2dcb  bd64000000            mov       ebp,64h
  004d2dd0  99                    cdq
  004d2dd1  8bcd                  mov       ecx,ebp
  004d2dd3  f7f9                  idiv      ecx
  004d2dd5  8bda                  mov       ebx,edx
  004d2dd7  83fb1e                cmp       ebx,1Eh
  004d2dda  7d68                  jge       short 004D2E44h
  004d2ddc  8b542410              mov       edx,[esp+10h]
  004d2de0  8d44120a              lea       eax,[edx+edx+0Ah]
  004d2de4  3bd8                  cmp       ebx,eax
  004d2de6  7d28                  jge       short 004D2E10h
  004d2de8  c6464202              mov       byte [esi+42h],2
  004d2dec  e84f430700            call      00547140h
  004d2df1  99                    cdq
  004d2df2  b90a000000            mov       ecx,0Ah
  004d2df7  66896e10              mov       [esi+10h],bp
  004d2dfb  f7f9                  idiv      ecx
  004d2dfd  8b442418              mov       eax,[esp+18h]
  004d2e01  d1f8                  sar       eax,1
  004d2e03  02d0                  add       dl,al
  004d2e05  80c214                add       dl,14h
  004d2e08  885643                mov       [esi+43h],dl
  004d2e0b  e9eb000000            jmp       004D2EFBh

LAB_004d2e10:
  004d2e10  c6464200              mov       byte [esi+42h],0
  004d2e14  e827430700            call      00547140h
  004d2e19  99                    cdq
  004d2e1a  b90a000000            mov       ecx,0Ah
  004d2e1f  66c746120000          mov       word [esi+12h],0
  004d2e25  f7f9                  idiv      ecx
  004d2e27  8b442418              mov       eax,[esp+18h]
  004d2e2b  8b4c2410              mov       ecx,[esp+10h]
  004d2e2f  c1f802                sar       eax,2
  004d2e32  02d0                  add       dl,al
  004d2e34  885643                mov       [esi+43h],dl
  004d2e37  8d540950              lea       edx,[ecx+ecx+50h]
  004d2e3b  66895610              mov       [esi+10h],dx
  004d2e3f  e9b7000000            jmp       004D2EFBh

LAB_004d2e44:
  004d2e44  83fb28                cmp       ebx,28h
  004d2e47  7d3a                  jge       short 004D2E83h
  004d2e49  c6464203              mov       byte [esi+42h],3
  004d2e4d  66c746120000          mov       word [esi+12h],0
  004d2e53  e8e8420700            call      00547140h
  004d2e58  99                    cdq
  004d2e59  b90a000000            mov       ecx,0Ah
  004d2e5e  f7f9                  idiv      ecx
  004d2e60  8b442418              mov       eax,[esp+18h]
  004d2e64  d1f8                  sar       eax,1
  004d2e66  02d0                  add       dl,al
  004d2e68  80c214                add       dl,14h
  004d2e6b  885643                mov       [esi+43h],dl
  004d2e6e  e8cd420700            call      00547140h
  004d2e73  99                    cdq
  004d2e74  33c2                  xor       eax,edx
  004d2e76  2bc2                  sub       eax,edx
  004d2e78  25ff0f0000            and       eax,0FFFh
  004d2e7d  33c2                  xor       eax,edx
  004d2e7f  2bc2                  sub       eax,edx
  004d2e81  eb74                  jmp       short 004D2EF7h

LAB_004d2e83:
  004d2e83  83fb3c                cmp       ebx,3Ch
  004d2e86  7d0a                  jge       short 004D2E92h
  004d2e88  c6464201              mov       byte [esi+42h],1
  004d2e8c  c6464301              mov       byte [esi+43h],1
  004d2e90  eb69                  jmp       short 004D2EFBh

LAB_004d2e92:
  004d2e92  83fb50                cmp       ebx,50h
  004d2e95  7d53                  jge       short 004D2EEAh
  004d2e97  8b7c2418              mov       edi,[esp+18h]
  004d2e9b  8b4c241c              mov       ecx,[esp+1Ch]
  004d2e9f  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004d2ea6  8d04fd00020000        lea       eax,[edi*8+200h]
  004d2ead  81e2ff0f0000          and       edx,0FFFh
  004d2eb3  3bd0                  cmp       edx,eax
  004d2eb5  7f10                  jg        short 004D2EC7h
  004d2eb7  c646420a              mov       byte [esi+42h],0Ah
  004d2ebb  c6464301              mov       byte [esi+43h],1
  004d2ebf  66c746120000          mov       word [esi+12h],0
  004d2ec5  eb34                  jmp       short 004D2EFBh

LAB_004d2ec7:
  004d2ec7  c6464202              mov       byte [esi+42h],2
  004d2ecb  e870420700            call      00547140h
  004d2ed0  99                    cdq
  004d2ed1  b90a000000            mov       ecx,0Ah
  004d2ed6  66896e10              mov       [esi+10h],bp
  004d2eda  f7f9                  idiv      ecx
  004d2edc  8bc7                  mov       eax,edi
  004d2ede  d1f8                  sar       eax,1
  004d2ee0  02d0                  add       dl,al
  004d2ee2  80c214                add       dl,14h
  004d2ee5  885643                mov       [esi+43h],dl
  004d2ee8  eb11                  jmp       short 004D2EFBh

LAB_004d2eea:
  004d2eea  c6464206              mov       byte [esi+42h],6
  004d2eee  c6464301              mov       byte [esi+43h],1
  004d2ef2  e849420700            call      00547140h

LAB_004d2ef7:
  004d2ef7  66894610              mov       [esi+10h],ax

LAB_004d2efb:
  004d2efb  8b442418              mov       eax,[esp+18h]
  004d2eff  8b4c241c              mov       ecx,[esp+1Ch]
  004d2f03  8d948100020000        lea       edx,[ecx+eax*4+200h]
  004d2f0a  8d0cc500040000        lea       ecx,[eax*8+400h]
  004d2f11  81e2ff0f0000          and       edx,0FFFh
  004d2f17  3bd1                  cmp       edx,ecx
  004d2f19  7f5a                  jg        short 004D2F75h
  004d2f1b  8b542450              mov       edx,[esp+50h]
  004d2f1f  f60202                test      byte [edx],2
  004d2f22  7451                  je        short 004D2F75h
  004d2f24  3bd8                  cmp       ebx,eax
  004d2f26  7d4d                  jge       short 004D2F75h
  004d2f28  f60601                test      byte [esi],1
  004d2f2b  7548                  jne       short 004D2F75h
  004d2f2d  807e4203              cmp       byte [esi+42h],3
  004d2f31  7742                  ja        short 004D2F75h
  004d2f33  817c2414c8000000      cmp       dword [esp+14h],0C8h
  004d2f3b  7d38                  jge       short 004D2F75h
  004d2f3d  c6464205              mov       byte [esi+42h],5
  004d2f41  e8fa410700            call      00547140h
  004d2f46  99                    cdq
  004d2f47  b905000000            mov       ecx,5
  004d2f4c  66c746180600          mov       word [esi+18h],6
  004d2f52  f7f9                  idiv      ecx
  004d2f54  8b06                  mov       eax,[esi]
  004d2f56  66c7461a0700          mov       word [esi+1Ah],7
  004d2f5c  c7465c00000000        mov       dword [esi+5Ch],0
  004d2f63  66c746120100          mov       word [esi+12h],1
  004d2f69  02d1                  add       dl,cl
  004d2f6b  25fffffffe            and       eax,0FEFFFFFFh
  004d2f70  885643                mov       [esi+43h],dl
  004d2f73  8906                  mov       [esi],eax

LAB_004d2f75:
  004d2f75  5f                    pop       edi
  004d2f76  5e                    pop       esi
  004d2f77  5d                    pop       ebp
  004d2f78  5b                    pop       ebx
  004d2f79  81c494000000          add       esp,94h
  004d2f7f  c3                    ret
  004d2f80  f3234d00              and       ecx,[ebp]
  004d2f84  3b244d00aa244d        cmp       esp,[ecx*2+4D24AA00h]
  004d2f8b  005025                add       [eax+25h],dl
  004d2f8e  4d                    dec       ebp
  004d2f8f  003c29                add       [ecx+ebp],bh
  004d2f92  4d                    dec       ebp
  004d2f93  0020                  add       [eax],ah
  004d2f95  264d                  dec       ebp
  004d2f97  006826                add       [eax+26h],ch
  004d2f9a  4d                    dec       ebp
  004d2f9b  00fd                  add       ch,bh
  004d2f9d  264d                  dec       ebp
  004d2f9f  003c29                add       [ecx+ebp],bh
  004d2fa2  4d                    dec       ebp
  004d2fa3  003c29                add       [ecx+ebp],bh
  004d2fa6  4d                    dec       ebp
  004d2fa7  005a27                add       [edx+27h],bl
  004d2faa  4d                    dec       ebp
  004d2fab  00e6                  add       dh,ah
  004d2fad  27                    daa
  004d2fae  4d                    dec       ebp
  004d2faf  001e                  add       [esi],bl
  004d2fb1  284d00                sub       [ebp],cl
  004d2fb4  8b28                  mov       ebp,[eax]
  004d2fb6  4d                    dec       ebp
  004d2fb7  009090909090          add       [eax-6F6F6F70h],dl
  004d2fbd  90                    nop
  004d2fbe  90                    nop
  004d2fbf  90                    nop

86 cible(s) de saut interne : 0x004d1fe1, 0x004d2134, 0x004d2155, 0x004d2167, 0x004d216a, 0x004d21d7, 0x004d21ee, 0x004d21f2, 0x004d220d, 0x004d2222, 0x004d224f, 0x004d225c, 0x004d226e, 0x004d227b, 0x004d22bd, 0x004d2386, 0x004d2394, 0x004d23d2, 0x004d23d6, 0x004d23da, 0x004d23de, 0x004d2413, 0x004d2426, 0x004d245c, 0x004d247d, 0x004d249a, 0x004d24da, 0x004d24f7, 0x004d2503, 0x004d253d, 0x004d256b, 0x004d25e7, 0x004d25f5, 0x004d25f9, 0x004d263e, 0x004d267b, 0x004d26eb, 0x004d271b, 0x004d2727, 0x004d2785, 0x004d279d, 0x004d27ab, 0x004d27d3, 0x004d2801, 0x004d2839, 0x004d2861, 0x004d28b8, 0x004d28ef, 0x004d28f9, 0x004d28fd, 0x004d291c, 0x004d2939, 0x004d293c, 0x004d2a24, 0x004d2a30, 0x004d2a32, 0x004d2a43, 0x004d2a7f, 0x004d2b0f, 0x004d2b1f, 0x004d2b48, 0x004d2b76, 0x004d2c50, 0x004d2c60, 0x004d2c6c, 0x004d2c9e, 0x004d2cc9, 0x004d2ccb, 0x004d2ccf, 0x004d2d22, 0x004d2d2b, 0x004d2d55, 0x004d2d6d, 0x004d2dac, 0x004d2db0, 0x004d2db4, 0x004d2db8, 0x004d2e10, 0x004d2e44, 0x004d2e83, 0x004d2e92, 0x004d2ec7, 0x004d2eea, 0x004d2ef7, 0x004d2efb, 0x004d2f75
