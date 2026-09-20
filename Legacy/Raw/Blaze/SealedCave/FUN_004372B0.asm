  004372b0  83ec14                sub       esp,14h
  004372b3  53                    push      ebx
  004372b4  55                    push      ebp
  004372b5  56                    push      esi
  004372b6  8b742424              mov       esi,[esp+24h]
  004372ba  33db                  xor       ebx,ebx
  004372bc  57                    push      edi
  004372bd  8b466c                mov       eax,[esi+6Ch]
  004372c0  b958e55d00            mov       ecx,5DE558h
  004372c5  3bc3                  cmp       eax,ebx
  004372c7  89442410              mov       [esp+10h],eax
  004372cb  7504                  jne       short 004372D1h
  004372cd  894c2410              mov       [esp+10h],ecx
  004372d1  8b4678                mov       eax,[esi+78h]
  004372d4  3bc3                  cmp       eax,ebx
  004372d6  89442428              mov       [esp+28h],eax
  004372da  7504                  jne       short 004372E0h
  004372dc  894c2428              mov       [esp+28h],ecx
  004372e0  66395e10              cmp       [esi+10h],bx
  004372e4  bf05000000            mov       edi,5
  004372e9  7574                  jne       short 0043735Fh
  004372eb  c7463c680f5500        mov       dword [esi+3Ch],550F68h
  004372f2  e849fe1000            call      00547140h
  004372f7  99                    cdq
  004372f8  b903000000            mov       ecx,3
  004372fd  c6460a02              mov       byte [esi+0Ah],2
  00437301  f7f9                  idiv      ecx
  00437303  fec2                  inc       dl
  00437305  66399e9a000000        cmp       [esi+9Ah],bx
  0043730c  885640                mov       [esi+40h],dl
  0043730f  7512                  jne       short 00437323h
  00437311  b0b4                  mov       al,0B4h
  00437313  c6460b00              mov       byte [esi+0Bh],0
  00437317  884638                mov       [esi+38h],al
  0043731a  884639                mov       [esi+39h],al
  0043731d  c6463a82              mov       byte [esi+3Ah],82h
  00437321  eb0f                  jmp       short 00437332h
  00437323  b0aa                  mov       al,0AAh
  00437325  c6460b01              mov       byte [esi+0Bh],1
  00437329  884638                mov       [esi+38h],al
  0043732c  884639                mov       [esi+39h],al
  0043732f  88463a                mov       [esi+3Ah],al
  00437332  8b561c                mov       edx,[esi+1Ch]
  00437335  8b4e24                mov       ecx,[esi+24h]
  00437338  b8dc050000            mov       eax,5DCh
  0043733d  895650                mov       [esi+50h],edx
  00437340  66894628              mov       [esi+28h],ax
  00437344  6689462a              mov       [esi+2Ah],ax
  00437348  8b4620                mov       eax,[esi+20h]
  0043734b  894e58                mov       [esi+58h],ecx
  0043734e  894654                mov       [esi+54h],eax
  00437351  66c746100100          mov       word [esi+10h],1
  00437357  66895e12              mov       [esi+12h],bx
  0043735b  66897e14              mov       [esi+14h],di
  0043735f  66837e1001            cmp       word [esi+10h],1
  00437364  0f851a020000          jne       near 00437584h
  0043736a  0fbf5632              movsx     edx,word [esi+32h]
  0043736e  52                    push      edx
  0043736f  e8a8e61000            call      00545A1Ch
  00437374  0fbf4e12              movsx     ecx,word [esi+12h]
  00437378  0fafc1                imul      eax,ecx
  0043737b  0fbf5632              movsx     edx,word [esi+32h]
  0043737f  8b4e50                mov       ecx,[esi+50h]
  00437382  52                    push      edx
  00437383  03c1                  add       eax,ecx
  00437385  89461c                mov       [esi+1Ch],eax
  00437388  e889e61000            call      00545A16h
  0043738d  0fbf4e12              movsx     ecx,word [esi+12h]
  00437391  0fafc1                imul      eax,ecx
  00437394  8b4e58                mov       ecx,[esi+58h]
  00437397  6af9                  push      0FFFFFFF9h
  00437399  03c1                  add       eax,ecx
  0043739b  6af9                  push      0FFFFFFF9h
  0043739d  894624                mov       [esi+24h],eax
  004373a0  8b4620                mov       eax,[esi+20h]
  004373a3  0500a0ffff            add       eax,0FFFFA000h
  004373a8  6af9                  push      0FFFFFFF9h
  004373aa  894620                mov       [esi+20h],eax
  004373ad  b8c8000000            mov       eax,0C8h
  004373b2  66014628              add       [esi+28h],ax
  004373b6  6601462a              add       [esi+2Ah],ax
  004373ba  56                    push      esi
  004373bb  e820dafcff            call      00404DE0h
  004373c0  8a4638                mov       al,[esi+38h]
  004373c3  83c410                add       esp,10h
  004373c6  84c0                  test      al,al
  004373c8  7506                  jne       short 004373D0h
  004373ca  66c746100200          mov       word [esi+10h],2
  004373d0  56                    push      esi
  004373d1  e86ac50f00            call      00533940h
  004373d6  8b8690000000          mov       eax,[esi+90h]
  004373dc  83c404                add       esp,4
  004373df  a902880000            test      eax,8802h
  004373e4  7406                  je        short 004373ECh
  004373e6  66c746100200          mov       word [esi+10h],2
  004373ec  8b5660                mov       edx,[esi+60h]
  004373ef  8b4620                mov       eax,[esi+20h]
  004373f2  3bd0                  cmp       edx,eax
  004373f4  7c06                  jl        short 004373FCh
  004373f6  66c746100200          mov       word [esi+10h],2
  004373fc  66ff4e14              dec       word [esi+14h]
  00437400  668b4614              mov       ax,[esi+14h]
  00437404  6683461214            add       word [esi+12h],14h
  00437409  663bc3                cmp       ax,bx
  0043740c  7f16                  jg        short 00437424h
  0043740e  8a5640                mov       dl,[esi+40h]
  00437411  66897e14              mov       [esi+14h],di
  00437415  fec2                  inc       dl
  00437417  8ac2                  mov       al,dl
  00437419  885640                mov       [esi+40h],dl
  0043741c  3c04                  cmp       al,4
  0043741e  7504                  jne       short 00437424h
  00437420  c6464001              mov       byte [esi+40h],1
  00437424  bfc0935e00            mov       edi,5E93C0h
  00437429  c744241804000000      mov       dword [esp+18h],4
  00437431  8b07                  mov       eax,[edi]
  00437433  a900000080            test      eax,80000000h
  00437438  0f8430010000          je        near 0043756Eh
  0043743e  a900000050            test      eax,50000000h
  00437443  0f8525010000          jne       near 0043756Eh
  00437449  66837f1200            cmp       word [edi+12h],0
  0043744e  0f851a010000          jne       near 0043756Eh
  00437454  8b4f1c                mov       ecx,[edi+1Ch]
  00437457  8b461c                mov       eax,[esi+1Ch]
  0043745a  8b5620                mov       edx,[esi+20h]
  0043745d  8b6e24                mov       ebp,[esi+24h]
  00437460  2bc8                  sub       ecx,eax
  00437462  8b4720                mov       eax,[edi+20h]
  00437465  2bc2                  sub       eax,edx
  00437467  8b5724                mov       edx,[edi+24h]
  0043746a  c1f90c                sar       ecx,0Ch
  0043746d  0500c00300            add       eax,3C000h
  00437472  2bd5                  sub       edx,ebp
  00437474  8be9                  mov       ebp,ecx
  00437476  c1f80c                sar       eax,0Ch
  00437479  0fafe9                imul      ebp,ecx
  0043747c  8bc8                  mov       ecx,eax
  0043747e  c1fa0c                sar       edx,0Ch
  00437481  0fafc8                imul      ecx,eax
  00437484  8bc2                  mov       eax,edx
  00437486  03e9                  add       ebp,ecx
  00437488  0fafc2                imul      eax,edx
  0043748b  03e8                  add       ebp,eax
  0043748d  55                    push      ebp
  0043748e  e87de51000            call      00545A10h
  00437493  6683bf9600000000      cmp       word [edi+96h],0
  0043749b  0f85cd000000          jne       near 0043756Eh
  004374a1  83f850                cmp       eax,50h
  004374a4  0f8dc4000000          jge       near 0043756Eh
  004374aa  8b4e24                mov       ecx,[esi+24h]
  004374ad  8b5620                mov       edx,[esi+20h]
  004374b0  8b461c                mov       eax,[esi+1Ch]
  004374b3  51                    push      ecx
  004374b4  52                    push      edx
  004374b5  50                    push      eax
  004374b6  57                    push      edi
  004374b7  e8e4000000            call      004375A0h
  004374bc  83c410                add       esp,10h
  004374bf  6685c0                test      ax,ax
  004374c2  0f85a0000000          jne       near 00437568h
  004374c8  6639869a000000        cmp       [esi+9Ah],ax
  004374cf  755a                  jne       short 0043752Bh
  004374d1  8d4c2414              lea       ecx,[esp+14h]
  004374d5  8d54241c              lea       edx,[esp+1Ch]
  004374d9  51                    push      ecx
  004374da  8b4c242c              mov       ecx,[esp+2Ch]
  004374de  8d442424              lea       eax,[esp+24h]
  004374e2  52                    push      edx
  004374e3  50                    push      eax
  004374e4  51                    push      ecx
  004374e5  e896141000            call      00538980h
  004374ea  8b442424              mov       eax,[esp+24h]
  004374ee  83c410                add       esp,10h
  004374f1  668b6830              mov       bp,[eax+30h]
  004374f5  57                    push      edi
  004374f6  668bd5                mov       dx,bp
  004374f9  8bcd                  mov       ecx,ebp
  004374fb  66c1fa02              sar       dx,2
  004374ff  2bca                  sub       ecx,edx
  00437501  8b54242c              mov       edx,[esp+2Ch]
  00437505  52                    push      edx
  00437506  66894830              mov       [eax+30h],cx
  0043750a  e871261000            call      00539B80h
  0043750f  8b44241c              mov       eax,[esp+1Ch]
  00437513  83c408                add       esp,8
  00437516  66896830              mov       [eax+30h],bp
  0043751a  66c787960000001000    mov       word [edi+96h],10h
  00437523  8b07                  mov       eax,[edi]
  00437525  0c40                  or        al,40h
  00437527  8907                  mov       [edi],eax
  00437529  eb43                  jmp       short 0043756Eh
  0043752b  8b4c2410              mov       ecx,[esp+10h]
  0043752f  ba01000000            mov       edx,1
  00437534  668b4116              mov       ax,[ecx+16h]
  00437538  8bcb                  mov       ecx,ebx
  0043753a  d3e2                  shl       edx,cl
  0043753c  0fbfc8                movsx     ecx,ax
  0043753f  85d1                  test      ecx,edx
  00437541  752b                  jne       short 0043756Eh
  00437543  ba01000000            mov       edx,1
  00437548  8bcb                  mov       ecx,ebx
  0043754a  d3e2                  shl       edx,cl
  0043754c  8b4c2428              mov       ecx,[esp+28h]
  00437550  6a00                  push      0
  00437552  57                    push      edi
  00437553  51                    push      ecx
  00437554  0bd0                  or        edx,eax
  00437556  8b44241c              mov       eax,[esp+1Ch]
  0043755a  66895016              mov       [eax+16h],dx
  0043755e  e86df0ffff            call      004365D0h
  00437563  83c40c                add       esp,0Ch
  00437566  eb06                  jmp       short 0043756Eh
  00437568  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043756e  8b442418              mov       eax,[esp+18h]
  00437572  43                    inc       ebx
  00437573  81c79c000000          add       edi,9Ch
  00437579  48                    dec       eax
  0043757a  89442418              mov       [esp+18h],eax
  0043757e  0f85adfeffff          jne       near 00437431h
  00437584  66837e1002            cmp       word [esi+10h],2
  00437589  7506                  jne       short 00437591h
  0043758b  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00437591  5f                    pop       edi
  00437592  5e                    pop       esi
  00437593  5d                    pop       ebp
  00437594  5b                    pop       ebx
  00437595  83c414                add       esp,14h
  00437598  c3                    ret
