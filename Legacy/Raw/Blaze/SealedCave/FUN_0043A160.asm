  0043a160  83ec10                sub       esp,10h
  0043a163  53                    push      ebx
  0043a164  55                    push      ebp
  0043a165  56                    push      esi
  0043a166  8b742420              mov       esi,[esp+20h]
  0043a16a  57                    push      edi
  0043a16b  bb01000000            mov       ebx,1
  0043a170  8b6e78                mov       ebp,[esi+78h]
  0043a173  66837e1000            cmp       word [esi+10h],0
  0043a178  896c2410              mov       [esp+10h],ebp
  0043a17c  bf05000000            mov       edi,5
  0043a181  7574                  jne       short 0043A1F7h
  0043a183  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043a18a  e8b1cf1000            call      00547140h
  0043a18f  99                    cdq
  0043a190  b903000000            mov       ecx,3
  0043a195  c6460a02              mov       byte [esi+0Ah],2
  0043a199  f7f9                  idiv      ecx
  0043a19b  fec2                  inc       dl
  0043a19d  6683be9a00000000      cmp       word [esi+9Ah],0
  0043a1a5  885640                mov       [esi+40h],dl
  0043a1a8  7512                  jne       short 0043A1BCh
  0043a1aa  b0b4                  mov       al,0B4h
  0043a1ac  c6460b00              mov       byte [esi+0Bh],0
  0043a1b0  884638                mov       [esi+38h],al
  0043a1b3  884639                mov       [esi+39h],al
  0043a1b6  c6463a82              mov       byte [esi+3Ah],82h
  0043a1ba  eb0e                  jmp       short 0043A1CAh
  0043a1bc  b0aa                  mov       al,0AAh
  0043a1be  885e0b                mov       [esi+0Bh],bl
  0043a1c1  884638                mov       [esi+38h],al
  0043a1c4  884639                mov       [esi+39h],al
  0043a1c7  88463a                mov       [esi+3Ah],al
  0043a1ca  8b561c                mov       edx,[esi+1Ch]
  0043a1cd  8b4e24                mov       ecx,[esi+24h]
  0043a1d0  b8dc050000            mov       eax,5DCh
  0043a1d5  895650                mov       [esi+50h],edx
  0043a1d8  66894628              mov       [esi+28h],ax
  0043a1dc  6689462a              mov       [esi+2Ah],ax
  0043a1e0  8b4620                mov       eax,[esi+20h]
  0043a1e3  894e58                mov       [esi+58h],ecx
  0043a1e6  894654                mov       [esi+54h],eax
  0043a1e9  66895e10              mov       [esi+10h],bx
  0043a1ed  66c746120000          mov       word [esi+12h],0
  0043a1f3  66897e14              mov       [esi+14h],di
  0043a1f7  66395e10              cmp       [esi+10h],bx
  0043a1fb  0f85e5010000          jne       near 0043A3E6h
  0043a201  0fbf5632              movsx     edx,word [esi+32h]
  0043a205  52                    push      edx
  0043a206  e811b81000            call      00545A1Ch
  0043a20b  0fbf4e12              movsx     ecx,word [esi+12h]
  0043a20f  0fafc1                imul      eax,ecx
  0043a212  0fbf5632              movsx     edx,word [esi+32h]
  0043a216  8b4e50                mov       ecx,[esi+50h]
  0043a219  52                    push      edx
  0043a21a  03c1                  add       eax,ecx
  0043a21c  89461c                mov       [esi+1Ch],eax
  0043a21f  e8f2b71000            call      00545A16h
  0043a224  0fbf4e12              movsx     ecx,word [esi+12h]
  0043a228  0fafc1                imul      eax,ecx
  0043a22b  8b4e58                mov       ecx,[esi+58h]
  0043a22e  6af9                  push      0FFFFFFF9h
  0043a230  03c1                  add       eax,ecx
  0043a232  6af9                  push      0FFFFFFF9h
  0043a234  894624                mov       [esi+24h],eax
  0043a237  8b4620                mov       eax,[esi+20h]
  0043a23a  0500a0ffff            add       eax,0FFFFA000h
  0043a23f  6af9                  push      0FFFFFFF9h
  0043a241  894620                mov       [esi+20h],eax
  0043a244  b8c8000000            mov       eax,0C8h
  0043a249  66014628              add       [esi+28h],ax
  0043a24d  6601462a              add       [esi+2Ah],ax
  0043a251  56                    push      esi
  0043a252  e889abfcff            call      00404DE0h
  0043a257  8a4638                mov       al,[esi+38h]
  0043a25a  83c410                add       esp,10h
  0043a25d  84c0                  test      al,al
  0043a25f  7506                  jne       short 0043A267h
  0043a261  66c746100200          mov       word [esi+10h],2
  0043a267  56                    push      esi
  0043a268  e8d3960f00            call      00533940h
  0043a26d  8b8690000000          mov       eax,[esi+90h]
  0043a273  83c404                add       esp,4
  0043a276  a902880000            test      eax,8802h
  0043a27b  7406                  je        short 0043A283h
  0043a27d  66c746100200          mov       word [esi+10h],2
  0043a283  8b5660                mov       edx,[esi+60h]
  0043a286  8b4620                mov       eax,[esi+20h]
  0043a289  3bd0                  cmp       edx,eax
  0043a28b  7c06                  jl        short 0043A293h
  0043a28d  66c746100200          mov       word [esi+10h],2
  0043a293  66ff4e14              dec       word [esi+14h]
  0043a297  668b4614              mov       ax,[esi+14h]
  0043a29b  6683461214            add       word [esi+12h],14h
  0043a2a0  6685c0                test      ax,ax
  0043a2a3  7f15                  jg        short 0043A2BAh
  0043a2a5  8a5640                mov       dl,[esi+40h]
  0043a2a8  66897e14              mov       [esi+14h],di
  0043a2ac  fec2                  inc       dl
  0043a2ae  8ac2                  mov       al,dl
  0043a2b0  885640                mov       [esi+40h],dl
  0043a2b3  3c04                  cmp       al,4
  0043a2b5  7503                  jne       short 0043A2BAh
  0043a2b7  885e40                mov       [esi+40h],bl
  0043a2ba  bfc0935e00            mov       edi,5E93C0h
  0043a2bf  c744241404000000      mov       dword [esp+14h],4
  0043a2c7  8b07                  mov       eax,[edi]
  0043a2c9  a900000080            test      eax,80000000h
  0043a2ce  0f84fd000000          je        near 0043A3D1h
  0043a2d4  a900000050            test      eax,50000000h
  0043a2d9  0f85f2000000          jne       near 0043A3D1h
  0043a2df  66837f1200            cmp       word [edi+12h],0
  0043a2e4  0f85e7000000          jne       near 0043A3D1h
  0043a2ea  8b4f1c                mov       ecx,[edi+1Ch]
  0043a2ed  8b461c                mov       eax,[esi+1Ch]
  0043a2f0  8b5620                mov       edx,[esi+20h]
  0043a2f3  8b5e24                mov       ebx,[esi+24h]
  0043a2f6  2bc8                  sub       ecx,eax
  0043a2f8  8b4720                mov       eax,[edi+20h]
  0043a2fb  2bc2                  sub       eax,edx
  0043a2fd  8b5724                mov       edx,[edi+24h]
  0043a300  c1f90c                sar       ecx,0Ch
  0043a303  0500c00300            add       eax,3C000h
  0043a308  2bd3                  sub       edx,ebx
  0043a30a  8bd9                  mov       ebx,ecx
  0043a30c  c1f80c                sar       eax,0Ch
  0043a30f  0fafd9                imul      ebx,ecx
  0043a312  8bc8                  mov       ecx,eax
  0043a314  c1fa0c                sar       edx,0Ch
  0043a317  0fafc8                imul      ecx,eax
  0043a31a  8bc2                  mov       eax,edx
  0043a31c  03d9                  add       ebx,ecx
  0043a31e  0fafc2                imul      eax,edx
  0043a321  03d8                  add       ebx,eax
  0043a323  53                    push      ebx
  0043a324  e8e7b61000            call      00545A10h
  0043a329  6683bf9600000000      cmp       word [edi+96h],0
  0043a331  0f859a000000          jne       near 0043A3D1h
  0043a337  83f850                cmp       eax,50h
  0043a33a  0f8d91000000          jge       near 0043A3D1h
  0043a340  8b4e24                mov       ecx,[esi+24h]
  0043a343  8b5620                mov       edx,[esi+20h]
  0043a346  8b461c                mov       eax,[esi+1Ch]
  0043a349  51                    push      ecx
  0043a34a  52                    push      edx
  0043a34b  50                    push      eax
  0043a34c  57                    push      edi
  0043a34d  e84ed2ffff            call      004375A0h
  0043a352  83c410                add       esp,10h
  0043a355  6685c0                test      ax,ax
  0043a358  7571                  jne       short 0043A3CBh
  0043a35a  6639869a000000        cmp       [esi+9Ah],ax
  0043a361  755a                  jne       short 0043A3BDh
  0043a363  8d4c2424              lea       ecx,[esp+24h]
  0043a367  8d542418              lea       edx,[esp+18h]
  0043a36b  51                    push      ecx
  0043a36c  8d442420              lea       eax,[esp+20h]
  0043a370  52                    push      edx
  0043a371  50                    push      eax
  0043a372  55                    push      ebp
  0043a373  e808e60f00            call      00538980h
  0043a378  8b442434              mov       eax,[esp+34h]
  0043a37c  83c410                add       esp,10h
  0043a37f  668b6830              mov       bp,[eax+30h]
  0043a383  57                    push      edi
  0043a384  668bcd                mov       cx,bp
  0043a387  8bd5                  mov       edx,ebp
  0043a389  66c1f902              sar       cx,2
  0043a38d  2bd1                  sub       edx,ecx
  0043a38f  66895030              mov       [eax+30h],dx
  0043a393  8b442414              mov       eax,[esp+14h]
  0043a397  50                    push      eax
  0043a398  e8e3f70f00            call      00539B80h
  0043a39d  8b4c242c              mov       ecx,[esp+2Ch]
  0043a3a1  83c408                add       esp,8
  0043a3a4  66896930              mov       [ecx+30h],bp
  0043a3a8  8b6c2410              mov       ebp,[esp+10h]
  0043a3ac  66c787960000001000    mov       word [edi+96h],10h
  0043a3b5  8b07                  mov       eax,[edi]
  0043a3b7  0c40                  or        al,40h
  0043a3b9  8907                  mov       [edi],eax
  0043a3bb  eb14                  jmp       short 0043A3D1h
  0043a3bd  6a00                  push      0
  0043a3bf  57                    push      edi
  0043a3c0  55                    push      ebp
  0043a3c1  e83a000000            call      0043A400h
  0043a3c6  83c40c                add       esp,0Ch
  0043a3c9  eb06                  jmp       short 0043A3D1h
  0043a3cb  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043a3d1  8b442414              mov       eax,[esp+14h]
  0043a3d5  81c79c000000          add       edi,9Ch
  0043a3db  48                    dec       eax
  0043a3dc  89442414              mov       [esp+14h],eax
  0043a3e0  0f85e1feffff          jne       near 0043A2C7h
  0043a3e6  66837e1002            cmp       word [esi+10h],2
  0043a3eb  7506                  jne       short 0043A3F3h
  0043a3ed  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043a3f3  5f                    pop       edi
  0043a3f4  5e                    pop       esi
  0043a3f5  5d                    pop       ebp
  0043a3f6  5b                    pop       ebx
  0043a3f7  83c410                add       esp,10h
  0043a3fa  c3                    ret
