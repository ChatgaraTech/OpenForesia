  0043a8e0  53                    push      ebx
  0043a8e1  56                    push      esi
  0043a8e2  8b74240c              mov       esi,[esp+0Ch]
  0043a8e6  33db                  xor       ebx,ebx
  0043a8e8  66395e10              cmp       [esi+10h],bx
  0043a8ec  0f85ef000000          jne       near 0043A9E1h
  0043a8f2  66c746100100          mov       word [esi+10h],1
  0043a8f8  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043a8ff  885e40                mov       [esi+40h],bl
  0043a902  c6460a02              mov       byte [esi+0Ah],2
  0043a906  885e0b                mov       [esi+0Bh],bl
  0043a909  e832c81000            call      00547140h
  0043a90e  99                    cdq
  0043a90f  33c2                  xor       eax,edx
  0043a911  66895e14              mov       [esi+14h],bx
  0043a915  2bc2                  sub       eax,edx
  0043a917  25ff0f0000            and       eax,0FFFh
  0043a91c  33c2                  xor       eax,edx
  0043a91e  2bc2                  sub       eax,edx
  0043a920  66894612              mov       [esi+12h],ax
  0043a924  e817c81000            call      00547140h
  0043a929  99                    cdq
  0043a92a  33c2                  xor       eax,edx
  0043a92c  8b4e24                mov       ecx,[esi+24h]
  0043a92f  2bc2                  sub       eax,edx
  0043a931  894e58                mov       [esi+58h],ecx
  0043a934  25ff030000            and       eax,3FFh
  0043a939  33c2                  xor       eax,edx
  0043a93b  2bc2                  sub       eax,edx
  0043a93d  0500080000            add       eax,800h
  0043a942  66894628              mov       [esi+28h],ax
  0043a946  6689462a              mov       [esi+2Ah],ax
  0043a94a  8b461c                mov       eax,[esi+1Ch]
  0043a94d  894650                mov       [esi+50h],eax
  0043a950  e8ebc71000            call      00547140h
  0043a955  99                    cdq
  0043a956  33c2                  xor       eax,edx
  0043a958  66895e16              mov       [esi+16h],bx
  0043a95c  2bc2                  sub       eax,edx
  0043a95e  66899e9a000000        mov       [esi+9Ah],bx
  0043a965  83e001                and       eax,1
  0043a968  33c2                  xor       eax,edx
  0043a96a  2bc2                  sub       eax,edx
  0043a96c  83c002                add       eax,2
  0043a96f  894654                mov       [esi+54h],eax
  0043a972  e8c9c71000            call      00547140h
  0043a977  99                    cdq
  0043a978  b919000000            mov       ecx,19h
  0043a97d  f7f9                  idiv      ecx
  0043a97f  80c25a                add       dl,5Ah
  0043a982  885638                mov       [esi+38h],dl
  0043a985  e8b6c71000            call      00547140h
  0043a98a  99                    cdq
  0043a98b  b919000000            mov       ecx,19h
  0043a990  f7f9                  idiv      ecx
  0043a992  80c25a                add       dl,5Ah
  0043a995  885639                mov       [esi+39h],dl
  0043a998  e8a3c71000            call      00547140h
  0043a99d  99                    cdq
  0043a99e  b919000000            mov       ecx,19h
  0043a9a3  f7f9                  idiv      ecx
  0043a9a5  80c25a                add       dl,5Ah
  0043a9a8  88563a                mov       [esi+3Ah],dl
  0043a9ab  e890c71000            call      00547140h
  0043a9b0  99                    cdq
  0043a9b1  b90f000000            mov       ecx,0Fh
  0043a9b6  53                    push      ebx
  0043a9b7  f7f9                  idiv      ecx
  0043a9b9  8b4e20                mov       ecx,[esi+20h]
  0043a9bc  53                    push      ebx
  0043a9bd  6800200000            push      2000h
  0043a9c2  6800010000            push      100h
  0043a9c7  81c100400600          add       ecx,64000h
  0043a9cd  6a1e                  push      1Eh
  0043a9cf  56                    push      esi
  0043a9d0  894e20                mov       [esi+20h],ecx
  0043a9d3  83c228                add       edx,28h
  0043a9d6  89565c                mov       [esi+5Ch],edx
  0043a9d9  e8a2d60200            call      00468080h
  0043a9de  83c418                add       esp,18h
  0043a9e1  66837e1001            cmp       word [esi+10h],1
  0043a9e6  0f859c000000          jne       near 0043AA88h
  0043a9ec  8b5654                mov       edx,[esi+54h]
  0043a9ef  8b4620                mov       eax,[esi+20h]
  0043a9f2  c1e20c                shl       edx,0Ch
  0043a9f5  03c2                  add       eax,edx
  0043a9f7  894620                mov       [esi+20h],eax
  0043a9fa  0fbf4612              movsx     eax,word [esi+12h]
  0043a9fe  50                    push      eax
  0043a9ff  e818b01000            call      00545A1Ch
  0043aa04  0fbf4e14              movsx     ecx,word [esi+14h]
  0043aa08  0fafc1                imul      eax,ecx
  0043aa0b  0fbf5612              movsx     edx,word [esi+12h]
  0043aa0f  8b4e50                mov       ecx,[esi+50h]
  0043aa12  52                    push      edx
  0043aa13  03c1                  add       eax,ecx
  0043aa15  89461c                mov       [esi+1Ch],eax
  0043aa18  e8f9af1000            call      00545A16h
  0043aa1d  0fbf4e14              movsx     ecx,word [esi+14h]
  0043aa21  0fafc1                imul      eax,ecx
  0043aa24  8b5658                mov       edx,[esi+58h]
  0043aa27  6683461264            add       word [esi+12h],64h
  0043aa2c  03c2                  add       eax,edx
  0043aa2e  66ff869a000000        inc       word [esi+9Ah]
  0043aa35  894624                mov       [esi+24h],eax
  0043aa38  668b869a000000        mov       ax,[esi+9Ah]
  0043aa3f  b9f4010000            mov       ecx,1F4h
  0043aa44  66014e28              add       [esi+28h],cx
  0043aa48  66014e2a              add       [esi+2Ah],cx
  0043aa4c  663b4616              cmp       ax,[esi+16h]
  0043aa50  7c18                  jl        short 0043AA6Ah
  0043aa52  8a4e40                mov       cl,[esi+40h]
  0043aa55  66899e9a000000        mov       [esi+9Ah],bx
  0043aa5c  fec1                  inc       cl
  0043aa5e  8ac1                  mov       al,cl
  0043aa60  884e40                mov       [esi+40h],cl
  0043aa63  3c04                  cmp       al,4
  0043aa65  7503                  jne       short 0043AA6Ah
  0043aa67  885e40                mov       [esi+40h],bl
  0043aa6a  6afe                  push      0FFFFFFFEh
  0043aa6c  6afe                  push      0FFFFFFFEh
  0043aa6e  6aff                  push      0FFFFFFFFh
  0043aa70  56                    push      esi
  0043aa71  e86aa3fcff            call      00404DE0h
  0043aa76  8b465c                mov       eax,[esi+5Ch]
  0043aa79  83c410                add       esp,10h
  0043aa7c  48                    dec       eax
  0043aa7d  89465c                mov       [esi+5Ch],eax
  0043aa80  7906                  jns       short 0043AA88h
  0043aa82  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043aa88  5e                    pop       esi
  0043aa89  5b                    pop       ebx
  0043aa8a  c3                    ret
