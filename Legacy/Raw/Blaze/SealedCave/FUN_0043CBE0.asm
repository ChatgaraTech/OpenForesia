  0043cbe0  53                    push      ebx
  0043cbe1  56                    push      esi
  0043cbe2  8b74240c              mov       esi,[esp+0Ch]
  0043cbe6  33db                  xor       ebx,ebx
  0043cbe8  66395e10              cmp       [esi+10h],bx
  0043cbec  0f85cc000000          jne       near 0043CCBEh
  0043cbf2  66c746100100          mov       word [esi+10h],1
  0043cbf8  c7463cb0125500        mov       dword [esi+3Ch],5512B0h
  0043cbff  885e40                mov       [esi+40h],bl
  0043cc02  c6460a02              mov       byte [esi+0Ah],2
  0043cc06  885e0b                mov       [esi+0Bh],bl
  0043cc09  e832a51000            call      00547140h
  0043cc0e  99                    cdq
  0043cc0f  33c2                  xor       eax,edx
  0043cc11  66895e14              mov       [esi+14h],bx
  0043cc15  2bc2                  sub       eax,edx
  0043cc17  25ff0f0000            and       eax,0FFFh
  0043cc1c  33c2                  xor       eax,edx
  0043cc1e  2bc2                  sub       eax,edx
  0043cc20  66894612              mov       [esi+12h],ax
  0043cc24  e817a51000            call      00547140h
  0043cc29  99                    cdq
  0043cc2a  33c2                  xor       eax,edx
  0043cc2c  8b4e24                mov       ecx,[esi+24h]
  0043cc2f  2bc2                  sub       eax,edx
  0043cc31  894e58                mov       [esi+58h],ecx
  0043cc34  25ff030000            and       eax,3FFh
  0043cc39  33c2                  xor       eax,edx
  0043cc3b  2bc2                  sub       eax,edx
  0043cc3d  0500080000            add       eax,800h
  0043cc42  66894628              mov       [esi+28h],ax
  0043cc46  6689462a              mov       [esi+2Ah],ax
  0043cc4a  8b461c                mov       eax,[esi+1Ch]
  0043cc4d  894650                mov       [esi+50h],eax
  0043cc50  e8eba41000            call      00547140h
  0043cc55  99                    cdq
  0043cc56  33c2                  xor       eax,edx
  0043cc58  66895e16              mov       [esi+16h],bx
  0043cc5c  2bc2                  sub       eax,edx
  0043cc5e  66899e9a000000        mov       [esi+9Ah],bx
  0043cc65  83e001                and       eax,1
  0043cc68  33c2                  xor       eax,edx
  0043cc6a  2bc2                  sub       eax,edx
  0043cc6c  83c002                add       eax,2
  0043cc6f  894654                mov       [esi+54h],eax
  0043cc72  e8c9a41000            call      00547140h
  0043cc77  99                    cdq
  0043cc78  b919000000            mov       ecx,19h
  0043cc7d  f7f9                  idiv      ecx
  0043cc7f  80c220                add       dl,20h
  0043cc82  885638                mov       [esi+38h],dl
  0043cc85  e8b6a41000            call      00547140h
  0043cc8a  99                    cdq
  0043cc8b  b919000000            mov       ecx,19h
  0043cc90  f7f9                  idiv      ecx
  0043cc92  80c220                add       dl,20h
  0043cc95  885639                mov       [esi+39h],dl
  0043cc98  e8a3a41000            call      00547140h
  0043cc9d  99                    cdq
  0043cc9e  b919000000            mov       ecx,19h
  0043cca3  f7f9                  idiv      ecx
  0043cca5  80c220                add       dl,20h
  0043cca8  88563a                mov       [esi+3Ah],dl
  0043ccab  e890a41000            call      00547140h
  0043ccb0  99                    cdq
  0043ccb1  b90f000000            mov       ecx,0Fh
  0043ccb6  f7f9                  idiv      ecx
  0043ccb8  83c228                add       edx,28h
  0043ccbb  89565c                mov       [esi+5Ch],edx
  0043ccbe  66837e1001            cmp       word [esi+10h],1
  0043ccc3  0f85a2000000          jne       near 0043CD6Bh
  0043ccc9  8b4654                mov       eax,[esi+54h]
  0043cccc  8b4e20                mov       ecx,[esi+20h]
  0043cccf  8bd0                  mov       edx,eax
  0043ccd1  c1e20c                shl       edx,0Ch
  0043ccd4  03ca                  add       ecx,edx
  0043ccd6  40                    inc       eax
  0043ccd7  894654                mov       [esi+54h],eax
  0043ccda  894e20                mov       [esi+20h],ecx
  0043ccdd  0fbf4612              movsx     eax,word [esi+12h]
  0043cce1  50                    push      eax
  0043cce2  e8358d1000            call      00545A1Ch
  0043cce7  0fbf4e14              movsx     ecx,word [esi+14h]
  0043cceb  0fafc1                imul      eax,ecx
  0043ccee  0fbf5612              movsx     edx,word [esi+12h]
  0043ccf2  8b4e50                mov       ecx,[esi+50h]
  0043ccf5  52                    push      edx
  0043ccf6  03c1                  add       eax,ecx
  0043ccf8  89461c                mov       [esi+1Ch],eax
  0043ccfb  e8168d1000            call      00545A16h
  0043cd00  0fbf4e14              movsx     ecx,word [esi+14h]
  0043cd04  0fafc1                imul      eax,ecx
  0043cd07  8b5658                mov       edx,[esi+58h]
  0043cd0a  6683461264            add       word [esi+12h],64h
  0043cd0f  03c2                  add       eax,edx
  0043cd11  66ff869a000000        inc       word [esi+9Ah]
  0043cd18  894624                mov       [esi+24h],eax
  0043cd1b  668b869a000000        mov       ax,[esi+9Ah]
  0043cd22  b9f4010000            mov       ecx,1F4h
  0043cd27  66014e28              add       [esi+28h],cx
  0043cd2b  66014e2a              add       [esi+2Ah],cx
  0043cd2f  663b4616              cmp       ax,[esi+16h]
  0043cd33  7c18                  jl        short 0043CD4Dh
  0043cd35  8a4e40                mov       cl,[esi+40h]
  0043cd38  66899e9a000000        mov       [esi+9Ah],bx
  0043cd3f  fec1                  inc       cl
  0043cd41  8ac1                  mov       al,cl
  0043cd43  884e40                mov       [esi+40h],cl
  0043cd46  3c07                  cmp       al,7
  0043cd48  7503                  jne       short 0043CD4Dh
  0043cd4a  885e40                mov       [esi+40h],bl
  0043cd4d  6afe                  push      0FFFFFFFEh
  0043cd4f  6afe                  push      0FFFFFFFEh
  0043cd51  6aff                  push      0FFFFFFFFh
  0043cd53  56                    push      esi
  0043cd54  e88780fcff            call      00404DE0h
  0043cd59  8b465c                mov       eax,[esi+5Ch]
  0043cd5c  83c410                add       esp,10h
  0043cd5f  48                    dec       eax
  0043cd60  89465c                mov       [esi+5Ch],eax
  0043cd63  7906                  jns       short 0043CD6Bh
  0043cd65  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043cd6b  5e                    pop       esi
  0043cd6c  5b                    pop       ebx
  0043cd6d  c3                    ret
