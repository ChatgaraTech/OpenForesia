Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e20f0  section .text  offset fichier 0xe14f0  3184 octets

  004e20f0  81ec88000000          sub       esp,88h
  004e20f6  53                    push      ebx
  004e20f7  55                    push      ebp
  004e20f8  56                    push      esi
  004e20f9  8bb42498000000        mov       esi,[esp+98h]
  004e2100  b896000000            mov       eax,96h
  004e2105  33db                  xor       ebx,ebx
  004e2107  668944242a            mov       [esp+2Ah],ax
  004e210c  668944242e            mov       [esp+2Eh],ax
  004e2111  8a4641                mov       al,[esi+41h]
  004e2114  57                    push      edi
  004e2115  84c0                  test      al,al
  004e2117  66895c2428            mov       [esp+28h],bx
  004e211c  66c744242a5000        mov       word [esp+2Ah],50h
  004e2123  66c744242c6400        mov       word [esp+2Ch],64h
  004e212a  66895c2430            mov       [esp+30h],bx
  004e212f  7516                  jne       short 004E2147h
  004e2131  53                    push      ebx
  004e2132  6a03                  push      3
  004e2134  56                    push      esi
  004e2135  c6464101              mov       byte [esi+41h],1
  004e2139  e812e6ffff            call      004E0750h
  004e213e  8b4620                mov       eax,[esi+20h]
  004e2141  83c40c                add       esp,0Ch
  004e2144  894648                mov       [esi+48h],eax

LAB_004e2147:
  004e2147  8b16                  mov       edx,[esi]
  004e2149  8b4e1c                mov       ecx,[esi+1Ch]
  004e214c  8b4664                mov       eax,[esi+64h]
  004e214f  8d6e1c                lea       ebp,[esi+1Ch]
  004e2152  83e2fe                and       edx,0FFFFFFFEh
  004e2155  894c2458              mov       [esp+58h],ecx
  004e2159  8b4e60                mov       ecx,[esi+60h]
  004e215c  8916                  mov       [esi],edx
  004e215e  8b5624                mov       edx,[esi+24h]
  004e2161  89442440              mov       [esp+40h],eax
  004e2165  8b467c                mov       eax,[esi+7Ch]
  004e2168  89542460              mov       [esp+60h],edx
  004e216c  668b5632              mov       dx,[esi+32h]
  004e2170  894c2438              mov       [esp+38h],ecx
  004e2174  668b8e90000000        mov       cx,[esi+90h]
  004e217b  6689942486000000      mov       [esp+86h],dx
  004e2183  8b9680000000          mov       edx,[esi+80h]
  004e2189  8b7e6c                mov       edi,[esi+6Ch]
  004e218c  89442448              mov       [esp+48h],eax
  004e2190  8b8684000000          mov       eax,[esi+84h]
  004e2196  66894c243c            mov       [esp+3Ch],cx
  004e219b  8b8e88000000          mov       ecx,[esi+88h]
  004e21a1  8954244c              mov       [esp+4Ch],edx
  004e21a5  8b968c000000          mov       edx,[esi+8Ch]
  004e21ab  89442434              mov       [esp+34h],eax
  004e21af  894c2444              mov       [esp+44h],ecx
  004e21b3  8d442424              lea       eax,[esp+24h]
  004e21b7  89542450              mov       [esp+50h],edx
  004e21bb  8d4c2464              lea       ecx,[esp+64h]
  004e21bf  50                    push      eax
  004e21c0  8d542470              lea       edx,[esp+70h]
  004e21c4  51                    push      ecx
  004e21c5  52                    push      edx
  004e21c6  56                    push      esi
  004e21c7  897c2430              mov       [esp+30h],edi
  004e21cb  e8b0670500            call      00538980h
  004e21d0  83c410                add       esp,10h
  004e21d3  8d442454              lea       eax,[esp+54h]
  004e21d7  8d4c241c              lea       ecx,[esp+1Ch]
  004e21db  8d542418              lea       edx,[esp+18h]
  004e21df  50                    push      eax
  004e21e0  51                    push      ecx
  004e21e1  8d44241c              lea       eax,[esp+1Ch]
  004e21e5  52                    push      edx
  004e21e6  8d4c241c              lea       ecx,[esp+1Ch]
  004e21ea  50                    push      eax
  004e21eb  51                    push      ecx
  004e21ec  56                    push      esi
  004e21ed  e82e3d0500            call      00535F20h
  004e21f2  8b54246c              mov       edx,[esp+6Ch]
  004e21f6  83c418                add       esp,18h
  004e21f9  c744241040000000      mov       dword [esp+10h],40h
  004e2201  52                    push      edx
  004e2202  56                    push      esi
  004e2203  e8d83bffff            call      004D5DE0h
  004e2208  8b4500                mov       eax,[ebp]
  004e220b  8b571c                mov       edx,[edi+1Ch]
  004e220e  8b4e24                mov       ecx,[esi+24h]
  004e2211  2bc2                  sub       eax,edx
  004e2213  8b5724                mov       edx,[edi+24h]
  004e2216  83c408                add       esp,8
  004e2219  c1f80c                sar       eax,0Ch
  004e221c  2bca                  sub       ecx,edx
  004e221e  8bd0                  mov       edx,eax
  004e2220  c1f90c                sar       ecx,0Ch
  004e2223  0fafd0                imul      edx,eax
  004e2226  8bc1                  mov       eax,ecx
  004e2228  0fafc1                imul      eax,ecx
  004e222b  03d0                  add       edx,eax
  004e222d  52                    push      edx
  004e222e  e8dd370600            call      00545A10h
  004e2233  8b4c241c              mov       ecx,[esp+1Ch]
  004e2237  8bf8                  mov       edi,eax
  004e2239  3bcb                  cmp       ecx,ebx
  004e223b  897c2414              mov       [esp+14h],edi
  004e223f  0f8431070000          je        near 004E2976h
  004e2245  8b06                  mov       eax,[esi]
  004e2247  0fbf5e18              movsx     ebx,word [esi+18h]
  004e224b  25fffffffd            and       eax,0FDFFFFFFh
  004e2250  8906                  mov       [esi],eax
  004e2252  8bd0                  mov       edx,eax
  004e2254  33c0                  xor       eax,eax
  004e2256  8a4604                mov       al,[esi+4]
  004e2259  f7c200000001          test      edx,1000000h
  004e225f  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004e2266  8b0498                mov       eax,[eax+ebx*4]
  004e2269  741d                  je        short 004E2288h
  004e226b  66ff461a              inc       word [esi+1Ah]
  004e226f  668b561a              mov       dx,[esi+1Ah]
  004e2273  33db                  xor       ebx,ebx
  004e2275  668b5816              mov       bx,[eax+16h]
  004e2279  0fbfd2                movsx     edx,dx
  004e227c  3bd3                  cmp       edx,ebx
  004e227e  7c2b                  jl        short 004E22ABh
  004e2280  33db                  xor       ebx,ebx
  004e2282  66895e1a              mov       [esi+1Ah],bx
  004e2286  eb25                  jmp       short 004E22ADh

LAB_004e2288:
  004e2288  66ff461a              inc       word [esi+1Ah]
  004e228c  33db                  xor       ebx,ebx
  004e228e  668b5816              mov       bx,[eax+16h]
  004e2292  0fbf461a              movsx     eax,word [esi+1Ah]
  004e2296  3bc3                  cmp       eax,ebx
  004e2298  7c11                  jl        short 004E22ABh
  004e229a  668b461a              mov       ax,[esi+1Ah]
  004e229e  48                    dec       eax
  004e229f  81ca00000002          or        edx,2000000h
  004e22a5  6689461a              mov       [esi+1Ah],ax
  004e22a9  8916                  mov       [esi],edx

LAB_004e22ab:
  004e22ab  33db                  xor       ebx,ebx

LAB_004e22ad:
  004e22ad  f60602                test      byte [esi],2
  004e22b0  0f84ed000000          je        near 004E23A3h
  004e22b6  8a4642                mov       al,[esi+42h]
  004e22b9  3c0b                  cmp       al,0Bh
  004e22bb  7508                  jne       short 004E22C5h
  004e22bd  668b561a              mov       dx,[esi+1Ah]
  004e22c1  89542468              mov       [esp+68h],edx

LAB_004e22c5:
  004e22c5  3c0c                  cmp       al,0Ch
  004e22c7  0f85d6000000          jne       near 004E23A3h
  004e22cd  8d442428              lea       eax,[esp+28h]
  004e22d1  53                    push      ebx
  004e22d2  50                    push      eax
  004e22d3  55                    push      ebp
  004e22d4  56                    push      esi
  004e22d5  e856790300            call      00519C30h
  004e22da  8bf8                  mov       edi,eax
  004e22dc  a1b8e05d00            mov       eax,[5DE0B8h]
  004e22e1  83c410                add       esp,10h
  004e22e4  3bc3                  cmp       eax,ebx
  004e22e6  0f84af000000          je        near 004E239Bh
  004e22ec  8b1f                  mov       ebx,[edi]
  004e22ee  66c787960000001e00    mov       word [edi+96h],1Eh
  004e22f7  83cb40                or        ebx,40h
  004e22fa  57                    push      edi
  004e22fb  891f                  mov       [edi],ebx
  004e22fd  8b4c2428              mov       ecx,[esp+28h]
  004e2301  56                    push      esi
  004e2302  668b4130              mov       ax,[ecx+30h]
  004e2306  0fbfd8                movsx     ebx,ax
  004e2309  66d1f8                sar       ax,1
  004e230c  66894130              mov       [ecx+30h],ax
  004e2310  e86b780500            call      00539B80h
  004e2315  8b4c242c              mov       ecx,[esp+2Ch]
  004e2319  83c408                add       esp,8
  004e231c  8d571c                lea       edx,[edi+1Ch]
  004e231f  52                    push      edx
  004e2320  6800180000            push      1800h
  004e2325  6800030000            push      300h
  004e232a  6a03                  push      3
  004e232c  66895930              mov       [ecx+30h],bx
  004e2330  e8ebb70500            call      0053DB20h
  004e2335  8b07                  mov       eax,[edi]
  004e2337  83c410                add       esp,10h
  004e233a  a900000040            test      eax,40000000h
  004e233f  7548                  jne       short 004E2389h
  004e2341  6aff                  push      0FFFFFFFFh
  004e2343  6a00                  push      0
  004e2345  6a39                  push      39h
  004e2347  56                    push      esi
  004e2348  e88386f9ff            call      0047A9D0h
  004e234d  8b07                  mov       eax,[edi]
  004e234f  83c410                add       esp,10h
  004e2352  0c01                  or        al,1
  004e2354  8907                  mov       [edi],eax
  004e2356  0fbf4632              movsx     eax,word [esi+32h]
  004e235a  50                    push      eax
  004e235b  e8bc360600            call      00545A1Ch
  004e2360  8b4e5c                mov       ecx,[esi+5Ch]
  004e2363  c1f90b                sar       ecx,0Bh
  004e2366  0fafc1                imul      eax,ecx
  004e2369  894750                mov       [edi+50h],eax
  004e236c  0fbf5632              movsx     edx,word [esi+32h]
  004e2370  52                    push      edx
  004e2371  e8a0360600            call      00545A16h
  004e2376  8b4e5c                mov       ecx,[esi+5Ch]
  004e2379  c7475400800100        mov       dword [edi+54h],18000h
  004e2380  c1f90b                sar       ecx,0Bh
  004e2383  0fafc1                imul      eax,ecx
  004e2386  894758                mov       [edi+58h],eax

LAB_004e2389:
  004e2389  8b06                  mov       eax,[esi]
  004e238b  8b4c241c              mov       ecx,[esp+1Ch]
  004e238f  8b7c2414              mov       edi,[esp+14h]
  004e2393  24fd                  and       al,0FDh
  004e2395  8906                  mov       [esi],eax
  004e2397  33db                  xor       ebx,ebx
  004e2399  eb08                  jmp       short 004E23A3h

LAB_004e239b:
  004e239b  8b4c241c              mov       ecx,[esp+1Ch]
  004e239f  8b7c2414              mov       edi,[esp+14h]

LAB_004e23a3:
  004e23a3  33c0                  xor       eax,eax
  004e23a5  ba01000000            mov       edx,1
  004e23aa  8a4642                mov       al,[esi+42h]
  004e23ad  83f80d                cmp       eax,0Dh
  004e23b0  0f8747050000          ja        near 004E28FDh
  004e23b6  ff2485242d4e00        jmp       dword [eax*4+4E2D24h]
  004e23bd  66395e12              cmp       [esi+12h],bx
  004e23c1  751a                  jne       short 004E23DDh
  004e23c3  8b06                  mov       eax,[esi]
  004e23c5  66c746180500          mov       word [esi+18h],5
  004e23cb  0d00000001            or        eax,1000000h
  004e23d0  66895e1a              mov       [esi+1Ah],bx
  004e23d4  8906                  mov       [esi],eax
  004e23d6  66895612              mov       [esi+12h],dx
  004e23da  895e5c                mov       [esi+5Ch],ebx

LAB_004e23dd:
  004e23dd  0fbf5610              movsx     edx,word [esi+10h]
  004e23e1  3bfa                  cmp       edi,edx
  004e23e3  7d09                  jge       short 004E23EEh
  004e23e5  c6464300              mov       byte [esi+43h],0
  004e23e9  e90f050000            jmp       004E28FDh

LAB_004e23ee:
  004e23ee  8a4643                mov       al,[esi+43h]
  004e23f1  84c0                  test      al,al
  004e23f3  0f8401050000          je        near 004E28FAh
  004e23f9  fec8                  dec       al
  004e23fb  884643                mov       [esi+43h],al
  004e23fe  e9f7040000            jmp       004E28FAh
  004e2403  66395e12              cmp       [esi+12h],bx
  004e2407  751e                  jne       short 004E2427h
  004e2409  8b06                  mov       eax,[esi]
  004e240b  66c746180500          mov       word [esi+18h],5
  004e2411  0d00000001            or        eax,1000000h
  004e2416  66895e1a              mov       [esi+1Ah],bx
  004e241a  8906                  mov       [esi],eax
  004e241c  66895612              mov       [esi+12h],dx
  004e2420  c7465c00700000        mov       dword [esi+5Ch],7000h

LAB_004e2427:
  004e2427  8b442418              mov       eax,[esp+18h]
  004e242b  8d88c8000000          lea       ecx,[eax+0C8h]
  004e2431  81e1ff0f0000          and       ecx,0FFFh
  004e2437  81f990010000          cmp       ecx,190h
  004e243d  7f09                  jg        short 004E2448h
  004e243f  c6464300              mov       byte [esi+43h],0
  004e2443  e9b5040000            jmp       004E28FDh

LAB_004e2448:
  004e2448  25ff0f0000            and       eax,0FFFh
  004e244d  3d00080000            cmp       eax,800h
  004e2452  7d13                  jge       short 004E2467h
  004e2454  8b542410              mov       edx,[esp+10h]
  004e2458  8d8280000000          lea       eax,[edx+80h]
  004e245e  66014632              add       [esi+32h],ax
  004e2462  e996040000            jmp       004E28FDh

LAB_004e2467:
  004e2467  8b4c2410              mov       ecx,[esp+10h]
  004e246b  b880ffffff            mov       eax,0FFFFFF80h
  004e2470  2bc1                  sub       eax,ecx
  004e2472  66014632              add       [esi+32h],ax
  004e2476  e982040000            jmp       004E28FDh
  004e247b  66395e12              cmp       [esi+12h],bx
  004e247f  8b4c2410              mov       ecx,[esp+10h]
  004e2483  7525                  jne       short 004E24AAh
  004e2485  8b06                  mov       eax,[esi]
  004e2487  66c746180600          mov       word [esi+18h],6
  004e248d  0d00000001            or        eax,1000000h
  004e2492  66895e1a              mov       [esi+1Ah],bx
  004e2496  8906                  mov       [esi],eax
  004e2498  8d04c9                lea       eax,[ecx+ecx*8]
  004e249b  c1e004                shl       eax,4
  004e249e  0500a00000            add       eax,0A000h
  004e24a3  66895612              mov       [esi+12h],dx
  004e24a7  89465c                mov       [esi+5Ch],eax

LAB_004e24aa:
  004e24aa  66395612              cmp       [esi+12h],dx
  004e24ae  0f8549040000          jne       near 004E28FDh
  004e24b4  0fbf5610              movsx     edx,word [esi+10h]
  004e24b8  3bfa                  cmp       edi,edx
  004e24ba  7d09                  jge       short 004E24C5h
  004e24bc  c6464300              mov       byte [esi+43h],0
  004e24c0  e935040000            jmp       004E28FAh

LAB_004e24c5:
  004e24c5  8a4643                mov       al,[esi+43h]
  004e24c8  84c0                  test      al,al
  004e24ca  7405                  je        short 004E24D1h
  004e24cc  fec8                  dec       al
  004e24ce  884643                mov       [esi+43h],al

LAB_004e24d1:
  004e24d1  8b442418              mov       eax,[esp+18h]
  004e24d5  8d9000010000          lea       edx,[eax+100h]
  004e24db  81e2ff0f0000          and       edx,0FFFh
  004e24e1  81fa00020000          cmp       edx,200h
  004e24e7  0f8e10040000          jle       near 004E28FDh
  004e24ed  25ff0f0000            and       eax,0FFFh
  004e24f2  3d00080000            cmp       eax,800h
  004e24f7  7d0d                  jge       short 004E2506h
  004e24f9  8d440928              lea       eax,[ecx+ecx+28h]
  004e24fd  66014632              add       [esi+32h],ax
  004e2501  e9f7030000            jmp       004E28FDh

LAB_004e2506:
  004e2506  03c9                  add       ecx,ecx
  004e2508  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e250d  2bc1                  sub       eax,ecx
  004e250f  66014632              add       [esi+32h],ax
  004e2513  e9e5030000            jmp       004E28FDh
  004e2518  66395612              cmp       [esi+12h],dx
  004e251c  0f859b000000          jne       near 004E25BDh
  004e2522  8a4643                mov       al,[esi+43h]
  004e2525  84c0                  test      al,al
  004e2527  7405                  je        short 004E252Eh
  004e2529  fec8                  dec       al
  004e252b  884643                mov       [esi+43h],al

LAB_004e252e:
  004e252e  8b442410              mov       eax,[esp+10h]
  004e2532  8d14c0                lea       edx,[eax+eax*8]
  004e2535  c1e204                shl       edx,4
  004e2538  8d8200500000          lea       eax,[edx+5000h]
  004e253e  0fafc8                imul      ecx,eax
  004e2541  89465c                mov       [esi+5Ch],eax
  004e2544  0fbf4610              movsx     eax,word [esi+10h]
  004e2548  c1f90c                sar       ecx,0Ch
  004e254b  50                    push      eax
  004e254c  8bf9                  mov       edi,ecx
  004e254e  e8c9340600            call      00545A1Ch
  004e2553  0fafc7                imul      eax,edi
  004e2556  8b4d00                mov       ecx,[ebp]
  004e2559  c1f80c                sar       eax,0Ch
  004e255c  03c8                  add       ecx,eax
  004e255e  894d00                mov       [ebp],ecx
  004e2561  0fbf4e10              movsx     ecx,word [esi+10h]
  004e2565  51                    push      ecx
  004e2566  e8ab340600            call      00545A16h
  004e256b  0fafc7                imul      eax,edi
  004e256e  8b6e24                mov       ebp,[esi+24h]
  004e2571  895e5c                mov       [esi+5Ch],ebx
  004e2574  c1f80c                sar       eax,0Ch
  004e2577  03e8                  add       ebp,eax
  004e2579  8b442418              mov       eax,[esp+18h]
  004e257d  896e24                mov       [esi+24h],ebp
  004e2580  8d9000010000          lea       edx,[eax+100h]
  004e2586  81e2ff0f0000          and       edx,0FFFh
  004e258c  81fa00020000          cmp       edx,200h
  004e2592  7e24                  jle       short 004E25B8h
  004e2594  25ff0f0000            and       eax,0FFFh
  004e2599  3d00080000            cmp       eax,800h
  004e259e  7d09                  jge       short 004E25A9h
  004e25a0  8b442410              mov       eax,[esp+10h]
  004e25a4  83c032                add       eax,32h
  004e25a7  eb0b                  jmp       short 004E25B4h

LAB_004e25a9:
  004e25a9  8b4c2410              mov       ecx,[esp+10h]
  004e25ad  b8ceffffff            mov       eax,0FFFFFFCEh
  004e25b2  2bc1                  sub       eax,ecx

LAB_004e25b4:
  004e25b4  66014632              add       [esi+32h],ax

LAB_004e25b8:
  004e25b8  ba01000000            mov       edx,1

LAB_004e25bd:
  004e25bd  66395e12              cmp       [esi+12h],bx
  004e25c1  0f8536030000          jne       near 004E28FDh
  004e25c7  8b06                  mov       eax,[esi]
  004e25c9  66c746180600          mov       word [esi+18h],6
  004e25cf  0d00000001            or        eax,1000000h
  004e25d4  66895e1a              mov       [esi+1Ah],bx
  004e25d8  8906                  mov       [esi],eax
  004e25da  66895612              mov       [esi+12h],dx
  004e25de  e91a030000            jmp       004E28FDh
  004e25e3  668b4e12              mov       cx,[esi+12h]
  004e25e7  663bca                cmp       cx,dx
  004e25ea  751a                  jne       short 004E2606h
  004e25ec  0fbf4610              movsx     eax,word [esi+10h]
  004e25f0  3bf8                  cmp       edi,eax
  004e25f2  7d06                  jge       short 004E25FAh
  004e25f4  c6464300              mov       byte [esi+43h],0
  004e25f8  eb0c                  jmp       short 004E2606h

LAB_004e25fa:
  004e25fa  8a4643                mov       al,[esi+43h]
  004e25fd  84c0                  test      al,al
  004e25ff  7405                  je        short 004E2606h
  004e2601  fec8                  dec       al
  004e2603  884643                mov       [esi+43h],al

LAB_004e2606:
  004e2606  663bcb                cmp       cx,bx
  004e2609  0f85ee020000          jne       near 004E28FDh
  004e260f  8b442410              mov       eax,[esp+10h]
  004e2613  8b3e                  mov       edi,[esi]
  004e2615  81cf00000001          or        edi,1000000h
  004e261b  66c746180600          mov       word [esi+18h],6
  004e2621  8d0cc0                lea       ecx,[eax+eax*8]
  004e2624  66895e1a              mov       [esi+1Ah],bx
  004e2628  c1e104                shl       ecx,4
  004e262b  81c100600000          add       ecx,6000h
  004e2631  893e                  mov       [esi],edi
  004e2633  66895612              mov       [esi+12h],dx
  004e2637  894e5c                mov       [esi+5Ch],ecx
  004e263a  e9be020000            jmp       004E28FDh
  004e263f  66395e12              cmp       [esi+12h],bx
  004e2643  754a                  jne       short 004E268Fh
  004e2645  668b7e32              mov       di,[esi+32h]
  004e2649  e8f24a0600            call      00547140h
  004e264e  99                    cdq
  004e264f  33c2                  xor       eax,edx
  004e2651  66c746120100          mov       word [esi+12h],1
  004e2657  2bc2                  sub       eax,edx
  004e2659  66c746180600          mov       word [esi+18h],6
  004e265f  25ff070000            and       eax,7FFh
  004e2664  66895e1a              mov       [esi+1Ah],bx
  004e2668  33c2                  xor       eax,edx
  004e266a  2bc2                  sub       eax,edx
  004e266c  8d943800fcffff        lea       edx,[eax+edi-400h]
  004e2673  8b06                  mov       eax,[esi]
  004e2675  81e7ff0f0000          and       edi,0FFFh
  004e267b  0d00000001            or        eax,1000000h
  004e2680  66895610              mov       [esi+10h],dx
  004e2684  66897e32              mov       [esi+32h],di
  004e2688  8906                  mov       [esi],eax
  004e268a  ba01000000            mov       edx,1

LAB_004e268f:
  004e268f  66395612              cmp       [esi+12h],dx
  004e2693  0f8564020000          jne       near 004E28FDh
  004e2699  0fbf4610              movsx     eax,word [esi+10h]
  004e269d  668b4e32              mov       cx,[esi+32h]
  004e26a1  8bf8                  mov       edi,eax
  004e26a3  0fbfd1                movsx     edx,cx
  004e26a6  2bfa                  sub       edi,edx
  004e26a8  c1ff03                sar       edi,3
  004e26ab  03cf                  add       ecx,edi
  004e26ad  66894e32              mov       [esi+32h],cx
  004e26b1  0fbfc9                movsx     ecx,cx
  004e26b4  2bc1                  sub       eax,ecx
  004e26b6  83f814                cmp       eax,14h
  004e26b9  0f8d3e020000          jge       near 004E28FDh
  004e26bf  e87c4a0600            call      00547140h
  004e26c4  99                    cdq
  004e26c5  b914000000            mov       ecx,14h
  004e26ca  c6464207              mov       byte [esi+42h],7
  004e26ce  f7f9                  idiv      ecx
  004e26d0  80c21e                add       dl,1Eh
  004e26d3  885643                mov       [esi+43h],dl
  004e26d6  e922020000            jmp       004E28FDh
  004e26db  66395e12              cmp       [esi+12h],bx
  004e26df  7531                  jne       short 004E2712h
  004e26e1  8b0e                  mov       ecx,[esi]
  004e26e3  66c746180300          mov       word [esi+18h],3
  004e26e9  81c900000001          or        ecx,1000000h
  004e26ef  66895e1a              mov       [esi+1Ah],bx
  004e26f3  890e                  mov       [esi],ecx
  004e26f5  66c746120100          mov       word [esi+12h],1
  004e26fb  e8404a0600            call      00547140h
  004e2700  99                    cdq
  004e2701  b90a000000            mov       ecx,0Ah
  004e2706  f7f9                  idiv      ecx
  004e2708  02d1                  add       dl,cl
  004e270a  885643                mov       [esi+43h],dl
  004e270d  ba01000000            mov       edx,1

LAB_004e2712:
  004e2712  66395612              cmp       [esi+12h],dx
  004e2716  0f85e1010000          jne       near 004E28FDh
  004e271c  8b542420              mov       edx,[esp+20h]
  004e2720  8b4620                mov       eax,[esi+20h]
  004e2723  8b4a20                mov       ecx,[edx+20h]
  004e2726  2bc8                  sub       ecx,eax
  004e2728  81c100400100          add       ecx,14000h
  004e272e  c1f902                sar       ecx,2
  004e2731  03c8                  add       ecx,eax
  004e2733  8a4643                mov       al,[esi+43h]
  004e2736  fec8                  dec       al
  004e2738  894e20                mov       [esi+20h],ecx
  004e273b  884643                mov       [esi+43h],al
  004e273e  e9ba010000            jmp       004E28FDh
  004e2743  66395612              cmp       [esi+12h],dx
  004e2747  8b7c2410              mov       edi,[esp+10h]
  004e274b  8b4c2418              mov       ecx,[esp+18h]
  004e274f  7574                  jne       short 004E27C5h
  004e2751  8b6e20                mov       ebp,[esi+20h]
  004e2754  8b5660                mov       edx,[esi+60h]
  004e2757  81c500600000          add       ebp,6000h
  004e275d  8bc5                  mov       eax,ebp
  004e275f  896e20                mov       [esi+20h],ebp
  004e2762  2bc2                  sub       eax,edx
  004e2764  c1f80c                sar       eax,0Ch
  004e2767  3db4000000            cmp       eax,0B4h
  004e276c  7e21                  jle       short 004E278Fh
  004e276e  8b16                  mov       edx,[esi]
  004e2770  66c746120200          mov       word [esi+12h],2
  004e2776  81e2fffffffe          and       edx,0FEFFFFFFh
  004e277c  66895e10              mov       [esi+10h],bx
  004e2780  83ca02                or        edx,2
  004e2783  66c746180600          mov       word [esi+18h],6
  004e2789  66895e1a              mov       [esi+1Ah],bx
  004e278d  8916                  mov       [esi],edx

LAB_004e278f:
  004e278f  8d8100010000          lea       eax,[ecx+100h]
  004e2795  25ff0f0000            and       eax,0FFFh
  004e279a  3d00020000            cmp       eax,200h
  004e279f  7e24                  jle       short 004E27C5h
  004e27a1  8bd1                  mov       edx,ecx
  004e27a3  81e2ff0f0000          and       edx,0FFFh
  004e27a9  81fa00080000          cmp       edx,800h
  004e27af  7d06                  jge       short 004E27B7h
  004e27b1  8d443f28              lea       eax,[edi+edi+28h]
  004e27b5  eb0a                  jmp       short 004E27C1h

LAB_004e27b7:
  004e27b7  8d143f                lea       edx,[edi+edi]
  004e27ba  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e27bf  2bc2                  sub       eax,edx

LAB_004e27c1:
  004e27c1  66014632              add       [esi+32h],ax

LAB_004e27c5:
  004e27c5  66837e1202            cmp       word [esi+12h],2
  004e27ca  757e                  jne       short 004E284Ah
  004e27cc  b80d000000            mov       eax,0Dh
  004e27d1  6639461a              cmp       [esi+1Ah],ax
  004e27d5  7e04                  jle       short 004E27DBh
  004e27d7  6689461a              mov       [esi+1Ah],ax

LAB_004e27db:
  004e27db  8b542420              mov       edx,[esp+20h]
  004e27df  8b4620                mov       eax,[esi+20h]
  004e27e2  c7465c00900100        mov       dword [esi+5Ch],19000h
  004e27e9  8b5220                mov       edx,[edx+20h]
  004e27ec  2bd0                  sub       edx,eax
  004e27ee  81c200400100          add       edx,14000h
  004e27f4  c1fa02                sar       edx,2
  004e27f7  03d0                  add       edx,eax
  004e27f9  66ff4610              inc       word [esi+10h]
  004e27fd  8d8100010000          lea       eax,[ecx+100h]
  004e2803  895620                mov       [esi+20h],edx
  004e2806  668b5610              mov       dx,[esi+10h]
  004e280a  25ff0f0000            and       eax,0FFFh
  004e280f  3d00020000            cmp       eax,200h
  004e2814  7e22                  jle       short 004E2838h
  004e2816  81e1ff0f0000          and       ecx,0FFFh
  004e281c  81f900080000          cmp       ecx,800h
  004e2822  7d06                  jge       short 004E282Ah
  004e2824  8d443f28              lea       eax,[edi+edi+28h]
  004e2828  eb0a                  jmp       short 004E2834h

LAB_004e282a:
  004e282a  8d0c3f                lea       ecx,[edi+edi]
  004e282d  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e2832  2bc1                  sub       eax,ecx

LAB_004e2834:
  004e2834  66014632              add       [esi+32h],ax

LAB_004e2838:
  004e2838  6683fa14              cmp       dx,14h
  004e283c  7c0c                  jl        short 004E284Ah
  004e283e  8b06                  mov       eax,[esi]
  004e2840  66c746120300          mov       word [esi+12h],3
  004e2846  24fd                  and       al,0FDh
  004e2848  8906                  mov       [esi],eax

LAB_004e284a:
  004e284a  668b4e12              mov       cx,[esi+12h]
  004e284e  6683f903              cmp       cx,3
  004e2852  751d                  jne       short 004E2871h
  004e2854  8b465c                mov       eax,[esi+5Ch]
  004e2857  8bd0                  mov       edx,eax
  004e2859  c1fa03                sar       edx,3
  004e285c  2bc2                  sub       eax,edx
  004e285e  89465c                mov       [esi+5Ch],eax
  004e2861  2500f0ffff            and       eax,0FFFFF000h
  004e2866  3d00f00000            cmp       eax,0F000h
  004e286b  7d04                  jge       short 004E2871h
  004e286d  c6464300              mov       byte [esi+43h],0

LAB_004e2871:
  004e2871  663bcb                cmp       cx,bx
  004e2874  0f8583000000          jne       near 004E28FDh
  004e287a  8b06                  mov       eax,[esi]
  004e287c  66c746120100          mov       word [esi+12h],1
  004e2882  24fd                  and       al,0FDh
  004e2884  66c746180500          mov       word [esi+18h],5
  004e288a  0d00000001            or        eax,1000000h
  004e288f  66895e1a              mov       [esi+1Ah],bx
  004e2893  895e5c                mov       [esi+5Ch],ebx
  004e2896  8906                  mov       [esi],eax
  004e2898  eb63                  jmp       short 004E28FDh
  004e289a  66395612              cmp       [esi+12h],dx
  004e289e  753d                  jne       short 004E28DDh
  004e28a0  66837e1a14            cmp       word [esi+1Ah],14h
  004e28a5  7516                  jne       short 004E28BDh
  004e28a7  6aff                  push      0FFFFFFFFh
  004e28a9  53                    push      ebx
  004e28aa  68c4000000            push      0C4h
  004e28af  56                    push      esi
  004e28b0  e81b81f9ff            call      0047A9D0h
  004e28b5  83c410                add       esp,10h
  004e28b8  ba01000000            mov       edx,1

LAB_004e28bd:
  004e28bd  f70600000002          test      dword [esi],2000000h
  004e28c3  7404                  je        short 004E28C9h
  004e28c5  c6464300              mov       byte [esi+43h],0

LAB_004e28c9:
  004e28c9  8b4c2420              mov       ecx,[esp+20h]
  004e28cd  8b4620                mov       eax,[esi+20h]
  004e28d0  8b4920                mov       ecx,[ecx+20h]
  004e28d3  2bc8                  sub       ecx,eax
  004e28d5  c1f902                sar       ecx,2
  004e28d8  03c8                  add       ecx,eax
  004e28da  894e20                mov       [esi+20h],ecx

LAB_004e28dd:
  004e28dd  66395e12              cmp       [esi+12h],bx
  004e28e1  751a                  jne       short 004E28FDh
  004e28e3  8b06                  mov       eax,[esi]
  004e28e5  66895612              mov       [esi+12h],dx
  004e28e9  25fffffffe            and       eax,0FEFFFFFFh
  004e28ee  66c746180800          mov       word [esi+18h],8
  004e28f4  66895e1a              mov       [esi+1Ah],bx
  004e28f8  8906                  mov       [esi],eax

LAB_004e28fa:
  004e28fa  895e5c                mov       [esi+5Ch],ebx

LAB_004e28fd:
  004e28fd  8a4642                mov       al,[esi+42h]
  004e2900  3c0c                  cmp       al,0Ch
  004e2902  745b                  je        short 004E295Fh
  004e2904  3c0b                  cmp       al,0Bh
  004e2906  7457                  je        short 004E295Fh
  004e2908  3c0d                  cmp       al,0Dh
  004e290a  7453                  je        short 004E295Fh
  004e290c  e82f480600            call      00547140h
  004e2911  99                    cdq
  004e2912  b964000000            mov       ecx,64h
  004e2917  f7f9                  idiv      ecx
  004e2919  81c2c8000000          add       edx,0C8h
  004e291f  66015614              add       [esi+14h],dx
  004e2923  668b4614              mov       ax,[esi+14h]
  004e2927  663d0010              cmp       ax,1000h
  004e292b  7c09                  jl        short 004E2936h
  004e292d  0500f0ffff            add       eax,0FFFFF000h
  004e2932  66894614              mov       [esi+14h],ax

LAB_004e2936:
  004e2936  0fbf5614              movsx     edx,word [esi+14h]
  004e293a  8b7e20                mov       edi,[esi+20h]
  004e293d  52                    push      edx
  004e293e  e8d9300600            call      00545A1Ch
  004e2943  8b4e48                mov       ecx,[esi+48h]
  004e2946  8d0440                lea       eax,[eax+eax*2]
  004e2949  8d0480                lea       eax,[eax+eax*4]
  004e294c  8d1481                lea       edx,[ecx+eax*4]
  004e294f  2bd7                  sub       edx,edi
  004e2951  81c200c00800          add       edx,8C000h
  004e2957  c1fa03                sar       edx,3
  004e295a  03d7                  add       edx,edi
  004e295c  895620                mov       [esi+20h],edx

LAB_004e295f:
  004e295f  8b4c2420              mov       ecx,[esp+20h]
  004e2963  8b4164                mov       eax,[ecx+64h]
  004e2966  8b4920                mov       ecx,[ecx+20h]
  004e2969  33c8                  xor       ecx,eax
  004e296b  f7c100f0ffff          test      ecx,0FFFFF000h
  004e2971  7503                  jne       short 004E2976h
  004e2973  894648                mov       [esi+48h],eax

LAB_004e2976:
  004e2976  0fbf5632              movsx     edx,word [esi+32h]
  004e297a  52                    push      edx
  004e297b  e89c300600            call      00545A1Ch
  004e2980  89442470              mov       [esp+70h],eax
  004e2984  0fbf4632              movsx     eax,word [esi+32h]
  004e2988  50                    push      eax
  004e2989  e888300600            call      00545A16h
  004e298e  8b5650                mov       edx,[esi+50h]
  004e2991  89442478              mov       [esp+78h],eax
  004e2995  8bc2                  mov       eax,edx
  004e2997  8b4c241c              mov       ecx,[esp+1Ch]
  004e299b  c1f803                sar       eax,3
  004e299e  0faf4e5c              imul      ecx,[esi+5Ch]
  004e29a2  8bfa                  mov       edi,edx
  004e29a4  56                    push      esi
  004e29a5  2bf8                  sub       edi,eax
  004e29a7  c1fa08                sar       edx,8
  004e29aa  2bfa                  sub       edi,edx
  004e29ac  8b5654                mov       edx,[esi+54h]
  004e29af  8bc2                  mov       eax,edx
  004e29b1  8bea                  mov       ebp,edx
  004e29b3  c1f803                sar       eax,3
  004e29b6  2be8                  sub       ebp,eax
  004e29b8  897e50                mov       [esi+50h],edi
  004e29bb  c1fa08                sar       edx,8
  004e29be  2bea                  sub       ebp,edx
  004e29c0  8b5658                mov       edx,[esi+58h]
  004e29c3  8bda                  mov       ebx,edx
  004e29c5  8bc2                  mov       eax,edx
  004e29c7  c1fb08                sar       ebx,8
  004e29ca  2bc3                  sub       eax,ebx
  004e29cc  8b5e20                mov       ebx,[esi+20h]
  004e29cf  c1fa03                sar       edx,3
  004e29d2  c1f90c                sar       ecx,0Ch
  004e29d5  2bc2                  sub       eax,edx
  004e29d7  8bd1                  mov       edx,ecx
  004e29d9  0faf542474            imul      edx,[esp+74h]
  004e29de  0faf4c247c            imul      ecx,[esp+7Ch]
  004e29e3  c1fa0c                sar       edx,0Ch
  004e29e6  c1f90c                sar       ecx,0Ch
  004e29e9  894658                mov       [esi+58h],eax
  004e29ec  03d7                  add       edx,edi
  004e29ee  8b7e1c                mov       edi,[esi+1Ch]
  004e29f1  03c8                  add       ecx,eax
  004e29f3  8b4624                mov       eax,[esi+24h]
  004e29f6  03fa                  add       edi,edx
  004e29f8  03dd                  add       ebx,ebp
  004e29fa  03c1                  add       eax,ecx
  004e29fc  896e54                mov       [esi+54h],ebp
  004e29ff  897e1c                mov       [esi+1Ch],edi
  004e2a02  895e20                mov       [esi+20h],ebx
  004e2a05  894624                mov       [esi+24h],eax
  004e2a08  e8330f0500            call      00533940h
  004e2a0d  8bbe90000000          mov       edi,[esi+90h]
  004e2a13  8b4660                mov       eax,[esi+60h]
  004e2a16  8b5620                mov       edx,[esi+20h]
  004e2a19  83c404                add       esp,4
  004e2a1c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004e2a22  3bd0                  cmp       edx,eax
  004e2a24  89be90000000          mov       [esi+90h],edi
  004e2a2a  8bcf                  mov       ecx,edi
  004e2a2c  7f15                  jg        short 004E2A43h
  004e2a2e  f6c580                test      ch,80h
  004e2a31  7510                  jne       short 004E2A43h
  004e2a33  894620                mov       [esi+20h],eax
  004e2a36  8b06                  mov       eax,[esi]
  004e2a38  24fe                  and       al,0FEh
  004e2a3a  33db                  xor       ebx,ebx
  004e2a3c  8906                  mov       [esi],eax
  004e2a3e  895e54                mov       [esi+54h],ebx
  004e2a41  eb02                  jmp       short 004E2A45h

LAB_004e2a43:
  004e2a43  33db                  xor       ebx,ebx

LAB_004e2a45:
  004e2a45  f7c182880000          test      ecx,8882h
  004e2a4b  0f84c0000000          je        near 004E2B11h
  004e2a51  8a4e42                mov       cl,[esi+42h]
  004e2a54  b007                  mov       al,7
  004e2a56  3ac8                  cmp       cl,al
  004e2a58  7420                  je        short 004E2A7Ah
  004e2a5a  884642                mov       [esi+42h],al
  004e2a5d  e8de460600            call      00547140h
  004e2a62  99                    cdq
  004e2a63  b928000000            mov       ecx,28h
  004e2a68  66895e12              mov       [esi+12h],bx
  004e2a6c  f7f9                  idiv      ecx
  004e2a6e  66c746105000          mov       word [esi+10h],50h
  004e2a74  80c214                add       dl,14h
  004e2a77  885643                mov       [esi+43h],dl

LAB_004e2a7a:
  004e2a7a  8b16                  mov       edx,[esi]
  004e2a7c  8b442460              mov       eax,[esp+60h]
  004e2a80  8b4c2448              mov       ecx,[esp+48h]
  004e2a84  83e2fd                and       edx,0FFFFFFFDh
  004e2a87  894624                mov       [esi+24h],eax
  004e2a8a  8b44243c              mov       eax,[esp+3Ch]
  004e2a8e  8916                  mov       [esi],edx
  004e2a90  8b542458              mov       edx,[esp+58h]
  004e2a94  25ffff0000            and       eax,0FFFFh
  004e2a99  89561c                mov       [esi+1Ch],edx
  004e2a9c  8b54244c              mov       edx,[esp+4Ch]
  004e2aa0  894e7c                mov       [esi+7Ch],ecx
  004e2aa3  8b4c2444              mov       ecx,[esp+44h]
  004e2aa7  898690000000          mov       [esi+90h],eax
  004e2aad  8b442434              mov       eax,[esp+34h]
  004e2ab1  899680000000          mov       [esi+80h],edx
  004e2ab7  8b542450              mov       edx,[esp+50h]
  004e2abb  898684000000          mov       [esi+84h],eax
  004e2ac1  8b442440              mov       eax,[esp+40h]
  004e2ac5  898e88000000          mov       [esi+88h],ecx
  004e2acb  8b4c2438              mov       ecx,[esp+38h]
  004e2acf  6882880000            push      8882h
  004e2ad4  56                    push      esi
  004e2ad5  89968c000000          mov       [esi+8Ch],edx
  004e2adb  894664                mov       [esi+64h],eax
  004e2ade  894e60                mov       [esi+60h],ecx
  004e2ae1  e87a33ffff            call      004D5E60h
  004e2ae6  83c408                add       esp,8
  004e2ae9  663bc3                cmp       ax,bx
  004e2aec  7515                  jne       short 004E2B03h
  004e2aee  e84d460600            call      00547140h
  004e2af3  99                    cdq
  004e2af4  33c2                  xor       eax,edx
  004e2af6  2bc2                  sub       eax,edx
  004e2af8  83e001                and       eax,1
  004e2afb  33c2                  xor       eax,edx
  004e2afd  2bc2                  sub       eax,edx
  004e2aff  8d4400ff              lea       eax,[eax+eax-1]

LAB_004e2b03:
  004e2b03  8b542410              mov       edx,[esp+10h]
  004e2b07  83c240                add       edx,40h
  004e2b0a  0fafd0                imul      edx,eax
  004e2b0d  66015632              add       [esi+32h],dx

LAB_004e2b11:
  004e2b11  6aff                  push      0FFFFFFFFh
  004e2b13  6aff                  push      0FFFFFFFFh
  004e2b15  6aff                  push      0FFFFFFFFh
  004e2b17  56                    push      esi
  004e2b18  e8f30cfeff            call      004C3810h
  004e2b1d  8b44242c              mov       eax,[esp+2Ch]
  004e2b21  83c410                add       esp,10h
  004e2b24  3bc3                  cmp       eax,ebx
  004e2b26  7517                  jne       short 004E2B3Fh
  004e2b28  668b842486000000      mov       ax,[esp+86h]
  004e2b30  66894632              mov       [esi+32h],ax
  004e2b34  5f                    pop       edi
  004e2b35  5e                    pop       esi
  004e2b36  5d                    pop       ebp
  004e2b37  5b                    pop       ebx
  004e2b38  81c488000000          add       esp,88h
  004e2b3e  c3                    ret

LAB_004e2b3f:
  004e2b3f  385e43                cmp       [esi+43h],bl
  004e2b42  0f85d0010000          jne       near 004E2D18h
  004e2b48  8b466c                mov       eax,[esi+6Ch]
  004e2b4b  8b4e1c                mov       ecx,[esi+1Ch]
  004e2b4e  8b5624                mov       edx,[esi+24h]
  004e2b51  8b781c                mov       edi,[eax+1Ch]
  004e2b54  8b6824                mov       ebp,[eax+24h]
  004e2b57  2bf9                  sub       edi,ecx
  004e2b59  2bea                  sub       ebp,edx
  004e2b5b  c1ff0c                sar       edi,0Ch
  004e2b5e  c1fd0c                sar       ebp,0Ch
  004e2b61  8bcf                  mov       ecx,edi
  004e2b63  8bd5                  mov       edx,ebp
  004e2b65  0fafcf                imul      ecx,edi
  004e2b68  0fafd5                imul      edx,ebp
  004e2b6b  03ca                  add       ecx,edx
  004e2b6d  51                    push      ecx
  004e2b6e  e89d2e0600            call      00545A10h
  004e2b73  55                    push      ebp
  004e2b74  57                    push      edi
  004e2b75  8944241c              mov       [esp+1Ch],eax
  004e2b79  e8a42e0600            call      00545A22h
  004e2b7e  668b4e32              mov       cx,[esi+32h]
  004e2b82  66895e12              mov       [esi+12h],bx
  004e2b86  2bc1                  sub       eax,ecx
  004e2b88  25ff0f0000            and       eax,0FFFh
  004e2b8d  89442418              mov       [esp+18h],eax
  004e2b91  8b442414              mov       eax,[esp+14h]
  004e2b95  3d96000000            cmp       eax,96h
  004e2b9a  0f8db5000000          jge       near 004E2C55h
  004e2ba0  e89b450600            call      00547140h
  004e2ba5  99                    cdq
  004e2ba6  b964000000            mov       ecx,64h
  004e2bab  f7f9                  idiv      ecx
  004e2bad  8bfa                  mov       edi,edx
  004e2baf  83ff32                cmp       edi,32h
  004e2bb2  7d60                  jge       short 004E2C14h
  004e2bb4  8b542418              mov       edx,[esp+18h]
  004e2bb8  81c200010000          add       edx,100h
  004e2bbe  81e2ff0f0000          and       edx,0FFFh
  004e2bc4  81fa00020000          cmp       edx,200h
  004e2bca  7f4d                  jg        short 004E2C19h
  004e2bcc  e86f450600            call      00547140h
  004e2bd1  837c241464            cmp       dword [esp+14h],64h
  004e2bd6  7d41                  jge       short 004E2C19h
  004e2bd8  83ff1e                cmp       edi,1Eh
  004e2bdb  7d22                  jge       short 004E2BFFh
  004e2bdd  8b06                  mov       eax,[esi]
  004e2bdf  c646420b              mov       byte [esi+42h],0Bh
  004e2be3  25fffffffe            and       eax,0FEFFFFFFh
  004e2be8  c6464301              mov       byte [esi+43h],1
  004e2bec  0c02                  or        al,2
  004e2bee  895e5c                mov       [esi+5Ch],ebx
  004e2bf1  8906                  mov       [esi],eax
  004e2bf3  66c746180300          mov       word [esi+18h],3
  004e2bf9  66895e1a              mov       [esi+1Ah],bx
  004e2bfd  eb56                  jmp       short 004E2C55h

LAB_004e2bff:
  004e2bff  885e42                mov       [esi+42h],bl
  004e2c02  e839450600            call      00547140h
  004e2c07  99                    cdq
  004e2c08  b928000000            mov       ecx,28h
  004e2c0d  f7f9                  idiv      ecx
  004e2c0f  885643                mov       [esi+43h],dl
  004e2c12  eb41                  jmp       short 004E2C55h

LAB_004e2c14:
  004e2c14  83ff46                cmp       edi,46h
  004e2c17  7d34                  jge       short 004E2C4Dh

LAB_004e2c19:
  004e2c19  c6464203              mov       byte [esi+42h],3
  004e2c1d  66895e12              mov       [esi+12h],bx
  004e2c21  e81a450600            call      00547140h
  004e2c26  99                    cdq
  004e2c27  b90a000000            mov       ecx,0Ah
  004e2c2c  f7f9                  idiv      ecx
  004e2c2e  80c214                add       dl,14h
  004e2c31  885643                mov       [esi+43h],dl
  004e2c34  e807450600            call      00547140h
  004e2c39  99                    cdq
  004e2c3a  33c2                  xor       eax,edx
  004e2c3c  2bc2                  sub       eax,edx
  004e2c3e  25ff0f0000            and       eax,0FFFh
  004e2c43  33c2                  xor       eax,edx
  004e2c45  2bc2                  sub       eax,edx
  004e2c47  66894610              mov       [esi+10h],ax
  004e2c4b  eb08                  jmp       short 004E2C55h

LAB_004e2c4d:
  004e2c4d  c646420c              mov       byte [esi+42h],0Ch
  004e2c51  c6464301              mov       byte [esi+43h],1

LAB_004e2c55:
  004e2c55  8b442414              mov       eax,[esp+14h]
  004e2c59  3d20030000            cmp       eax,320h
  004e2c5e  0f8d8d000000          jge       near 004E2CF1h
  004e2c64  3d96000000            cmp       eax,96h
  004e2c69  0f8c82000000          jl        near 004E2CF1h
  004e2c6f  e8cc440600            call      00547140h
  004e2c74  99                    cdq
  004e2c75  b964000000            mov       ecx,64h
  004e2c7a  f7f9                  idiv      ecx
  004e2c7c  83fa14                cmp       edx,14h
  004e2c7f  7d0b                  jge       short 004E2C8Ch
  004e2c81  83fa0a                cmp       edx,0Ah
  004e2c84  7c63                  jl        short 004E2CE9h
  004e2c86  c646420c              mov       byte [esi+42h],0Ch
  004e2c8a  eb61                  jmp       short 004E2CEDh

LAB_004e2c8c:
  004e2c8c  83fa3c                cmp       edx,3Ch
  004e2c8f  7d1f                  jge       short 004E2CB0h
  004e2c91  c6464202              mov       byte [esi+42h],2
  004e2c95  e8a6440600            call      00547140h
  004e2c9a  99                    cdq
  004e2c9b  b90a000000            mov       ecx,0Ah
  004e2ca0  66c746105000          mov       word [esi+10h],50h
  004e2ca6  f7f9                  idiv      ecx
  004e2ca8  80c214                add       dl,14h
  004e2cab  885643                mov       [esi+43h],dl
  004e2cae  eb41                  jmp       short 004E2CF1h

LAB_004e2cb0:
  004e2cb0  83fa50                cmp       edx,50h
  004e2cb3  7d34                  jge       short 004E2CE9h
  004e2cb5  c6464203              mov       byte [esi+42h],3
  004e2cb9  66895e12              mov       [esi+12h],bx
  004e2cbd  e87e440600            call      00547140h
  004e2cc2  99                    cdq
  004e2cc3  b90a000000            mov       ecx,0Ah
  004e2cc8  f7f9                  idiv      ecx
  004e2cca  80c214                add       dl,14h
  004e2ccd  885643                mov       [esi+43h],dl
  004e2cd0  e86b440600            call      00547140h
  004e2cd5  99                    cdq
  004e2cd6  33c2                  xor       eax,edx
  004e2cd8  2bc2                  sub       eax,edx
  004e2cda  25ff0f0000            and       eax,0FFFh
  004e2cdf  33c2                  xor       eax,edx
  004e2ce1  2bc2                  sub       eax,edx
  004e2ce3  66894610              mov       [esi+10h],ax
  004e2ce7  eb08                  jmp       short 004E2CF1h

LAB_004e2ce9:
  004e2ce9  c646420d              mov       byte [esi+42h],0Dh

LAB_004e2ced:
  004e2ced  c6464301              mov       byte [esi+43h],1

LAB_004e2cf1:
  004e2cf1  817c241420030000      cmp       dword [esp+14h],320h
  004e2cf9  7c1d                  jl        short 004E2D18h
  004e2cfb  c6464202              mov       byte [esi+42h],2
  004e2cff  e83c440600            call      00547140h
  004e2d04  99                    cdq
  004e2d05  b90a000000            mov       ecx,0Ah
  004e2d0a  66c746106400          mov       word [esi+10h],64h
  004e2d10  f7f9                  idiv      ecx
  004e2d12  80c214                add       dl,14h
  004e2d15  885643                mov       [esi+43h],dl

LAB_004e2d18:
  004e2d18  5f                    pop       edi
  004e2d19  5e                    pop       esi
  004e2d1a  5d                    pop       ebp
  004e2d1b  5b                    pop       ebx
  004e2d1c  81c488000000          add       esp,88h
  004e2d22  c3                    ret
  004e2d23  90                    nop
  004e2d24  bd234e0003            mov       ebp,3004E23h
  004e2d29  244e                  and       al,4Eh
  004e2d2b  007b24                add       [ebx+24h],bh
  004e2d2e  4e                    dec       esi
  004e2d2f  0018                  add       [eax],bl
  004e2d31  254e00fd28            and       eax,28FD004Eh
  004e2d36  4e                    dec       esi
  004e2d37  00fd                  add       ch,bh
  004e2d39  284e00                sub       [esi],cl
  004e2d3c  fd                    std
  004e2d3d  284e00                sub       [esi],cl
  004e2d40  e325                  jecxz     004E2D67h
  004e2d42  4e                    dec       esi
  004e2d43  00fd                  add       ch,bh
  004e2d45  284e00                sub       [esi],cl
  004e2d48  3f                    aas
  004e2d49  264e                  dec       esi
  004e2d4b  00fd                  add       ch,bh
  004e2d4d  284e00                sub       [esi],cl
  004e2d50  db26                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
