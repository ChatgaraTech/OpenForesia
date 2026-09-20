  0043d190  53                    push      ebx
  0043d191  56                    push      esi
  0043d192  8b74240c              mov       esi,[esp+0Ch]
  0043d196  66837e1000            cmp       word [esi+10h],0
  0043d19b  0f8552010000          jne       near 0043D2F3h
  0043d1a1  66837e1400            cmp       word [esi+14h],0
  0043d1a6  66c746100100          mov       word [esi+10h],1
  0043d1ac  b360                  mov       bl,60h
  0043d1ae  0f8597000000          jne       near 0043D24Bh
  0043d1b4  c7463c78125500        mov       dword [esi+3Ch],551278h
  0043d1bb  c6464000              mov       byte [esi+40h],0
  0043d1bf  c6460a02              mov       byte [esi+0Ah],2
  0043d1c3  c6460b00              mov       byte [esi+0Bh],0
  0043d1c7  e8749f1000            call      00547140h
  0043d1cc  99                    cdq
  0043d1cd  33c2                  xor       eax,edx
  0043d1cf  2bc2                  sub       eax,edx
  0043d1d1  25ff010000            and       eax,1FFh
  0043d1d6  33c2                  xor       eax,edx
  0043d1d8  2bc2                  sub       eax,edx
  0043d1da  0500100000            add       eax,1000h
  0043d1df  66894628              mov       [esi+28h],ax
  0043d1e3  6689462a              mov       [esi+2Ah],ax
  0043d1e7  e8549f1000            call      00547140h
  0043d1ec  99                    cdq
  0043d1ed  33c2                  xor       eax,edx
  0043d1ef  2bc2                  sub       eax,edx
  0043d1f1  25ff0f0000            and       eax,0FFFh
  0043d1f6  33c2                  xor       eax,edx
  0043d1f8  2bc2                  sub       eax,edx
  0043d1fa  66894612              mov       [esi+12h],ax
  0043d1fe  e83d9f1000            call      00547140h
  0043d203  99                    cdq
  0043d204  b91e000000            mov       ecx,1Eh
  0043d209  885e38                mov       [esi+38h],bl
  0043d20c  f7f9                  idiv      ecx
  0043d20e  885e39                mov       [esi+39h],bl
  0043d211  885e3a                mov       [esi+3Ah],bl
  0043d214  03d1                  add       edx,ecx
  0043d216  66895614              mov       [esi+14h],dx
  0043d21a  e8219f1000            call      00547140h
  0043d21f  99                    cdq
  0043d220  b914000000            mov       ecx,14h
  0043d225  f7f9                  idiv      ecx
  0043d227  03d1                  add       edx,ecx
  0043d229  89565c                mov       [esi+5Ch],edx
  0043d22c  e80f9f1000            call      00547140h
  0043d231  99                    cdq
  0043d232  b914000000            mov       ecx,14h
  0043d237  f7f9                  idiv      ecx
  0043d239  8b4e20                mov       ecx,[esi+20h]
  0043d23c  b800c0feff            mov       eax,0FFFEC000h
  0043d241  c1e20c                shl       edx,0Ch
  0043d244  2bc2                  sub       eax,edx
  0043d246  03c8                  add       ecx,eax
  0043d248  894e20                mov       [esi+20h],ecx
  0043d24b  66837e1401            cmp       word [esi+14h],1
  0043d250  0f8585000000          jne       near 0043D2DBh
  0043d256  c7463c50125500        mov       dword [esi+3Ch],551250h
  0043d25d  c6464000              mov       byte [esi+40h],0
  0043d261  c6460a01              mov       byte [esi+0Ah],1
  0043d265  c6460b00              mov       byte [esi+0Bh],0
  0043d269  e8d29e1000            call      00547140h
  0043d26e  99                    cdq
  0043d26f  33c2                  xor       eax,edx
  0043d271  2bc2                  sub       eax,edx
  0043d273  25ff010000            and       eax,1FFh
  0043d278  33c2                  xor       eax,edx
  0043d27a  2bc2                  sub       eax,edx
  0043d27c  05000c0000            add       eax,0C00h
  0043d281  66894628              mov       [esi+28h],ax
  0043d285  6689462a              mov       [esi+2Ah],ax
  0043d289  e8b29e1000            call      00547140h
  0043d28e  99                    cdq
  0043d28f  33c2                  xor       eax,edx
  0043d291  2bc2                  sub       eax,edx
  0043d293  25ff0f0000            and       eax,0FFFh
  0043d298  33c2                  xor       eax,edx
  0043d29a  2bc2                  sub       eax,edx
  0043d29c  66894612              mov       [esi+12h],ax
  0043d2a0  e89b9e1000            call      00547140h
  0043d2a5  99                    cdq
  0043d2a6  b91e000000            mov       ecx,1Eh
  0043d2ab  885e38                mov       [esi+38h],bl
  0043d2ae  f7f9                  idiv      ecx
  0043d2b0  885e39                mov       [esi+39h],bl
  0043d2b3  885e3a                mov       [esi+3Ah],bl
  0043d2b6  c7465c0e000000        mov       dword [esi+5Ch],0Eh
  0043d2bd  03d1                  add       edx,ecx
  0043d2bf  66895614              mov       [esi+14h],dx
  0043d2c3  e8789e1000            call      00547140h
  0043d2c8  99                    cdq
  0043d2c9  b978000000            mov       ecx,78h
  0043d2ce  f7f9                  idiv      ecx
  0043d2d0  8b4620                mov       eax,[esi+20h]
  0043d2d3  c1e20c                shl       edx,0Ch
  0043d2d6  03c2                  add       eax,edx
  0043d2d8  894620                mov       [esi+20h],eax
  0043d2db  8b561c                mov       edx,[esi+1Ch]
  0043d2de  8b4620                mov       eax,[esi+20h]
  0043d2e1  8b4e24                mov       ecx,[esi+24h]
  0043d2e4  895650                mov       [esi+50h],edx
  0043d2e7  894654                mov       [esi+54h],eax
  0043d2ea  894e58                mov       [esi+58h],ecx
  0043d2ed  66c746160000          mov       word [esi+16h],0
  0043d2f3  66837e1001            cmp       word [esi+10h],1
  0043d2f8  0f859b000000          jne       near 0043D399h
  0043d2fe  6afd                  push      0FFFFFFFDh
  0043d300  6afd                  push      0FFFFFFFDh
  0043d302  6afd                  push      0FFFFFFFDh
  0043d304  56                    push      esi
  0043d305  e8d67afcff            call      00404DE0h
  0043d30a  6683461240            add       word [esi+12h],40h
  0043d30f  668b4612              mov       ax,[esi+12h]
  0043d313  83c410                add       esp,10h
  0043d316  663d0010              cmp       ax,1000h
  0043d31a  7c09                  jl        short 0043D325h
  0043d31c  0500f0ffff            add       eax,0FFFFF000h
  0043d321  66894612              mov       [esi+12h],ax
  0043d325  0fbf5612              movsx     edx,word [esi+12h]
  0043d329  52                    push      edx
  0043d32a  e8ed861000            call      00545A1Ch
  0043d32f  0fbf4e14              movsx     ecx,word [esi+14h]
  0043d333  0fafc1                imul      eax,ecx
  0043d336  0fbf5612              movsx     edx,word [esi+12h]
  0043d33a  8b4e50                mov       ecx,[esi+50h]
  0043d33d  52                    push      edx
  0043d33e  03c1                  add       eax,ecx
  0043d340  89461c                mov       [esi+1Ch],eax
  0043d343  e8ce861000            call      00545A16h
  0043d348  0fbf4e14              movsx     ecx,word [esi+14h]
  0043d34c  0fafc1                imul      eax,ecx
  0043d34f  8b5e58                mov       ebx,[esi+58h]
  0043d352  8b5620                mov       edx,[esi+20h]
  0043d355  03c3                  add       eax,ebx
  0043d357  81c200a00000          add       edx,0A000h
  0043d35d  66ff4616              inc       word [esi+16h]
  0043d361  66837e1602            cmp       word [esi+16h],2
  0043d366  894624                mov       [esi+24h],eax
  0043d369  895620                mov       [esi+20h],edx
  0043d36c  7518                  jne       short 0043D386h
  0043d36e  8a4e40                mov       cl,[esi+40h]
  0043d371  66c746160000          mov       word [esi+16h],0
  0043d377  fec1                  inc       cl
  0043d379  8ac1                  mov       al,cl
  0043d37b  884e40                mov       [esi+40h],cl
  0043d37e  3c07                  cmp       al,7
  0043d380  7504                  jne       short 0043D386h
  0043d382  c6464000              mov       byte [esi+40h],0
  0043d386  8b4e5c                mov       ecx,[esi+5Ch]
  0043d389  49                    dec       ecx
  0043d38a  8bc1                  mov       eax,ecx
  0043d38c  894e5c                mov       [esi+5Ch],ecx
  0043d38f  85c0                  test      eax,eax
  0043d391  7f06                  jg        short 0043D399h
  0043d393  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043d399  5e                    pop       esi
  0043d39a  5b                    pop       ebx
  0043d39b  c3                    ret
