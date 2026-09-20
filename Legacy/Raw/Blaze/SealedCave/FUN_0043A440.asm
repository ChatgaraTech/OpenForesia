  0043a440  53                    push      ebx
  0043a441  56                    push      esi
  0043a442  8b74240c              mov       esi,[esp+0Ch]
  0043a446  33db                  xor       ebx,ebx
  0043a448  57                    push      edi
  0043a449  bf01000000            mov       edi,1
  0043a44e  66395e10              cmp       [esi+10h],bx
  0043a452  0f85d0000000          jne       near 0043A528h
  0043a458  66897e10              mov       [esi+10h],di
  0043a45c  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043a463  885e40                mov       [esi+40h],bl
  0043a466  c6460a02              mov       byte [esi+0Ah],2
  0043a46a  885e0b                mov       [esi+0Bh],bl
  0043a46d  e8cecc1000            call      00547140h
  0043a472  99                    cdq
  0043a473  33c2                  xor       eax,edx
  0043a475  66899e98000000        mov       [esi+98h],bx
  0043a47c  2bc2                  sub       eax,edx
  0043a47e  25ff0f0000            and       eax,0FFFh
  0043a483  33c2                  xor       eax,edx
  0043a485  2bc2                  sub       eax,edx
  0043a487  66894612              mov       [esi+12h],ax
  0043a48b  e8b0cc1000            call      00547140h
  0043a490  99                    cdq
  0043a491  33c2                  xor       eax,edx
  0043a493  8b4e24                mov       ecx,[esi+24h]
  0043a496  2bc2                  sub       eax,edx
  0043a498  894e58                mov       [esi+58h],ecx
  0043a49b  25ff030000            and       eax,3FFh
  0043a4a0  33c2                  xor       eax,edx
  0043a4a2  2bc2                  sub       eax,edx
  0043a4a4  05000c0000            add       eax,0C00h
  0043a4a9  66894628              mov       [esi+28h],ax
  0043a4ad  6689462a              mov       [esi+2Ah],ax
  0043a4b1  8b461c                mov       eax,[esi+1Ch]
  0043a4b4  894650                mov       [esi+50h],eax
  0043a4b7  e884cc1000            call      00547140h
  0043a4bc  99                    cdq
  0043a4bd  33c2                  xor       eax,edx
  0043a4bf  66895e16              mov       [esi+16h],bx
  0043a4c3  2bc2                  sub       eax,edx
  0043a4c5  66899e9a000000        mov       [esi+9Ah],bx
  0043a4cc  23c7                  and       eax,edi
  0043a4ce  33c2                  xor       eax,edx
  0043a4d0  2bc2                  sub       eax,edx
  0043a4d2  40                    inc       eax
  0043a4d3  894654                mov       [esi+54h],eax
  0043a4d6  e865cc1000            call      00547140h
  0043a4db  99                    cdq
  0043a4dc  b919000000            mov       ecx,19h
  0043a4e1  f7f9                  idiv      ecx
  0043a4e3  80c240                add       dl,40h
  0043a4e6  885638                mov       [esi+38h],dl
  0043a4e9  e852cc1000            call      00547140h
  0043a4ee  99                    cdq
  0043a4ef  b919000000            mov       ecx,19h
  0043a4f4  f7f9                  idiv      ecx
  0043a4f6  80c220                add       dl,20h
  0043a4f9  885639                mov       [esi+39h],dl
  0043a4fc  e83fcc1000            call      00547140h
  0043a501  99                    cdq
  0043a502  b919000000            mov       ecx,19h
  0043a507  f7f9                  idiv      ecx
  0043a509  80c220                add       dl,20h
  0043a50c  88563a                mov       [esi+3Ah],dl
  0043a50f  e82ccc1000            call      00547140h
  0043a514  99                    cdq
  0043a515  b90f000000            mov       ecx,0Fh
  0043a51a  66c746146400          mov       word [esi+14h],64h
  0043a520  f7f9                  idiv      ecx
  0043a522  83c219                add       edx,19h
  0043a525  89565c                mov       [esi+5Ch],edx
  0043a528  66397e10              cmp       [esi+10h],di
  0043a52c  0f85c7000000          jne       near 0043A5F9h
  0043a532  8b4e54                mov       ecx,[esi+54h]
  0043a535  8b7e20                mov       edi,[esi+20h]
  0043a538  8bd1                  mov       edx,ecx
  0043a53a  b889888888            mov       eax,88888889h
  0043a53f  c1e20a                shl       edx,0Ah
  0043a542  03fa                  add       edi,edx
  0043a544  897e20                mov       [esi+20h],edi
  0043a547  0fbf7e14              movsx     edi,word [esi+14h]
  0043a54b  f7ef                  imul      edi
  0043a54d  03d7                  add       edx,edi
  0043a54f  c1fa06                sar       edx,6
  0043a552  8bc2                  mov       eax,edx
  0043a554  c1e81f                shr       eax,1Fh
  0043a557  03d0                  add       edx,eax
  0043a559  8d4c0a01              lea       ecx,[edx+ecx+1]
  0043a55d  0fbf5612              movsx     edx,word [esi+12h]
  0043a561  52                    push      edx
  0043a562  894e54                mov       [esi+54h],ecx
  0043a565  e8b2b41000            call      00545A1Ch
  0043a56a  0fbf8e98000000        movsx     ecx,word [esi+98h]
  0043a571  0fafc1                imul      eax,ecx
  0043a574  0fbf5612              movsx     edx,word [esi+12h]
  0043a578  8b4e50                mov       ecx,[esi+50h]
  0043a57b  52                    push      edx
  0043a57c  03c1                  add       eax,ecx
  0043a57e  89461c                mov       [esi+1Ch],eax
  0043a581  e890b41000            call      00545A16h
  0043a586  0fbf8e98000000        movsx     ecx,word [esi+98h]
  0043a58d  0fafc1                imul      eax,ecx
  0043a590  8b5658                mov       edx,[esi+58h]
  0043a593  6683461264            add       word [esi+12h],64h
  0043a598  03c2                  add       eax,edx
  0043a59a  66ff869a000000        inc       word [esi+9Ah]
  0043a5a1  668b8e9a000000        mov       cx,[esi+9Ah]
  0043a5a8  894624                mov       [esi+24h],eax
  0043a5ab  668b4614              mov       ax,[esi+14h]
  0043a5af  8d5050                lea       edx,[eax+50h]
  0043a5b2  83c050                add       eax,50h
  0043a5b5  66015628              add       [esi+28h],dx
  0043a5b9  6601462a              add       [esi+2Ah],ax
  0043a5bd  663b4e16              cmp       cx,[esi+16h]
  0043a5c1  7c18                  jl        short 0043A5DBh
  0043a5c3  8a4e40                mov       cl,[esi+40h]
  0043a5c6  66899e9a000000        mov       [esi+9Ah],bx
  0043a5cd  fec1                  inc       cl
  0043a5cf  8ac1                  mov       al,cl
  0043a5d1  884e40                mov       [esi+40h],cl
  0043a5d4  3c04                  cmp       al,4
  0043a5d6  7503                  jne       short 0043A5DBh
  0043a5d8  885e40                mov       [esi+40h],bl
  0043a5db  6afe                  push      0FFFFFFFEh
  0043a5dd  6aff                  push      0FFFFFFFFh
  0043a5df  6afe                  push      0FFFFFFFEh
  0043a5e1  56                    push      esi
  0043a5e2  e8f9a7fcff            call      00404DE0h
  0043a5e7  8b465c                mov       eax,[esi+5Ch]
  0043a5ea  83c410                add       esp,10h
  0043a5ed  48                    dec       eax
  0043a5ee  89465c                mov       [esi+5Ch],eax
  0043a5f1  7906                  jns       short 0043A5F9h
  0043a5f3  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043a5f9  5f                    pop       edi
  0043a5fa  5e                    pop       esi
  0043a5fb  5b                    pop       ebx
  0043a5fc  c3                    ret
