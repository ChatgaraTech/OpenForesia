  0046e410  83ec08                sub       esp,8
  0046e413  56                    push      esi
  0046e414  8b742410              mov       esi,[esp+10h]
  0046e418  668b4610              mov       ax,[esi+10h]
  0046e41c  663d0200              cmp       ax,2
  0046e420  750b                  jne       short 0046E42Dh
  0046e422  66c746300000          mov       word [esi+30h],0
  0046e428  5e                    pop       esi
  0046e429  83c408                add       esp,8
  0046e42c  c3                    ret
  0046e42d  33c9                  xor       ecx,ecx
  0046e42f  57                    push      edi
  0046e430  55                    push      ebp
  0046e431  663bc1                cmp       ax,cx
  0046e434  53                    push      ebx
  0046e435  7533                  jne       short 0046E46Ah
  0046e437  0fbf4616              movsx     eax,word [esi+16h]
  0046e43b  89465c                mov       [esi+5Ch],eax
  0046e43e  668b4630              mov       ax,[esi+30h]
  0046e442  663bc1                cmp       ax,cx
  0046e445  66c746100100          mov       word [esi+10h],1
  0046e44b  66898e9a000000        mov       [esi+9Ah],cx
  0046e452  740d                  je        short 0046E461h
  0046e454  66898698000000        mov       [esi+98h],ax
  0046e45b  66894e30              mov       [esi+30h],cx
  0046e45f  eb09                  jmp       short 0046E46Ah
  0046e461  66c786980000002c01    mov       word [esi+98h],12Ch
  0046e46a  8b4658                mov       eax,[esi+58h]
  0046e46d  8b4e50                mov       ecx,[esi+50h]
  0046e470  89442414              mov       [esp+14h],eax
  0046e474  894c241c              mov       [esp+1Ch],ecx
  0046e478  0fbf4614              movsx     eax,word [esi+14h]
  0046e47c  8b5654                mov       edx,[esi+54h]
  0046e47f  8b6e20                mov       ebp,[esi+20h]
  0046e482  8d0c80                lea       ecx,[eax+eax*4]
  0046e485  89542410              mov       [esp+10h],edx
  0046e489  c1e103                shl       ecx,3
  0046e48c  2bc8                  sub       ecx,eax
  0046e48e  8bfd                  mov       edi,ebp
  0046e490  c1ff0c                sar       edi,0Ch
  0046e493  8b548e1c              mov       edx,[esi+ecx*4+1Ch]
  0046e497  8d048e                lea       eax,[esi+ecx*4]
  0046e49a  895650                mov       [esi+50h],edx
  0046e49d  8b561c                mov       edx,[esi+1Ch]
  0046e4a0  8b4820                mov       ecx,[eax+20h]
  0046e4a3  894e54                mov       [esi+54h],ecx
  0046e4a6  8b4e50                mov       ecx,[esi+50h]
  0046e4a9  8b4024                mov       eax,[eax+24h]
  0046e4ac  c1fa0c                sar       edx,0Ch
  0046e4af  c1f90c                sar       ecx,0Ch
  0046e4b2  2bca                  sub       ecx,edx
  0046e4b4  8b5654                mov       edx,[esi+54h]
  0046e4b7  c1fa0c                sar       edx,0Ch
  0046e4ba  2bd7                  sub       edx,edi
  0046e4bc  8b7e24                mov       edi,[esi+24h]
  0046e4bf  894658                mov       [esi+58h],eax
  0046e4c2  c1ff0c                sar       edi,0Ch
  0046e4c5  c1f80c                sar       eax,0Ch
  0046e4c8  2bc7                  sub       eax,edi
  0046e4ca  8bf9                  mov       edi,ecx
  0046e4cc  0faff9                imul      edi,ecx
  0046e4cf  8bca                  mov       ecx,edx
  0046e4d1  0fafca                imul      ecx,edx
  0046e4d4  8bd0                  mov       edx,eax
  0046e4d6  03f9                  add       edi,ecx
  0046e4d8  0fafd0                imul      edx,eax
  0046e4db  03fa                  add       edi,edx
  0046e4dd  57                    push      edi
  0046e4de  e82d750d00            call      00545A10h
  0046e4e3  8b4e5c                mov       ecx,[esi+5Ch]
  0046e4e6  8bf8                  mov       edi,eax
  0046e4e8  8bc1                  mov       eax,ecx
  0046e4ea  99                    cdq
  0046e4eb  83e207                and       edx,7
  0046e4ee  03c2                  add       eax,edx
  0046e4f0  8bd8                  mov       ebx,eax
  0046e4f2  0fbf8698000000        movsx     eax,word [esi+98h]
  0046e4f9  c1fb03                sar       ebx,3
  0046e4fc  3bf8                  cmp       edi,eax
  0046e4fe  0f8dc4000000          jge       near 0046E5C8h
  0046e504  81462000200800        add       dword [esi+20h],82000h
  0046e50b  0fbf4614              movsx     eax,word [esi+14h]
  0046e50f  8d0c80                lea       ecx,[eax+eax*4]
  0046e512  c1e103                shl       ecx,3
  0046e515  2bc8                  sub       ecx,eax
  0046e517  817c8e2000802500      cmp       dword [esi+ecx*4+20h],258000h
  0046e51f  7f09                  jg        short 0046E52Ah
  0046e521  803d2ce55d0007        cmp       byte [5DE52Ch],7
  0046e528  7431                  je        short 0046E55Bh
  0046e52a  33ff                  xor       edi,edi
  0046e52c  47                    inc       edi
  0046e52d  66897e12              mov       [esi+12h],di
  0046e531  e80a8c0d00            call      00547140h
  0046e536  99                    cdq
  0046e537  b903000000            mov       ecx,3
  0046e53c  6a00                  push      0
  0046e53e  f7f9                  idiv      ecx
  0046e540  6a00                  push      0
  0046e542  6aff                  push      0FFFFFFFFh
  0046e544  6a10                  push      10h
  0046e546  6a30                  push      30h
  0046e548  56                    push      esi
  0046e549  66895616              mov       [esi+16h],dx
  0046e54d  e8be9bffff            call      00468110h
  0046e552  83c418                add       esp,18h
  0046e555  6683ff05              cmp       di,5
  0046e559  7ed1                  jle       short 0046E52Ch
  0046e55b  8b4e5c                mov       ecx,[esi+5Ch]
  0046e55e  85c9                  test      ecx,ecx
  0046e560  7420                  je        short 0046E582h
  0046e562  0fbfc3                movsx     eax,bx
  0046e565  8d14c500000000        lea       edx,[eax*8]
  0046e56c  2bca                  sub       ecx,edx
  0046e56e  b201                  mov       dl,1
  0046e570  d2e2                  shl       dl,cl
  0046e572  8a88e0965e00          mov       cl,[eax+5E96E0h]
  0046e578  f6d2                  not       dl
  0046e57a  22ca                  and       cl,dl
  0046e57c  8888e0965e00          mov       [eax+5E96E0h],cl
  0046e582  66ff869a000000        inc       word [esi+9Ah]
  0046e589  6683be9a0000000d      cmp       word [esi+9Ah],0Dh
  0046e591  754f                  jne       short 0046E5E2h
  0046e593  66c7869a0000000000    mov       word [esi+9Ah],0
  0046e59c  a1cc975e00            mov       eax,[5E97CCh]
  0046e5a1  6a00                  push      0
  0046e5a3  6a00                  push      0
  0046e5a5  8b88a8000000          mov       ecx,[eax+0A8h]
  0046e5ab  6800100000            push      1000h
  0046e5b0  6800020000            push      200h
  0046e5b5  8b11                  mov       edx,[ecx]
  0046e5b7  660fb64237            movzx     ax,byte [edx+37h]
  0046e5bc  50                    push      eax
  0046e5bd  56                    push      esi
  0046e5be  e8bd9affff            call      00468080h
  0046e5c3  83c418                add       esp,18h
  0046e5c6  eb1a                  jmp       short 0046E5E2h
  0046e5c8  85c9                  test      ecx,ecx
  0046e5ca  7416                  je        short 0046E5E2h
  0046e5cc  0fbfc3                movsx     eax,bx
  0046e5cf  8d14c500000000        lea       edx,[eax*8]
  0046e5d6  2bca                  sub       ecx,edx
  0046e5d8  b201                  mov       dl,1
  0046e5da  d2e2                  shl       dl,cl
  0046e5dc  0890e0965e00          or        [eax+5E96E0h],dl
  0046e5e2  8b44241c              mov       eax,[esp+1Ch]
  0046e5e6  8b4c2410              mov       ecx,[esp+10h]
  0046e5ea  8b542414              mov       edx,[esp+14h]
  0046e5ee  896e20                mov       [esi+20h],ebp
  0046e5f1  5b                    pop       ebx
  0046e5f2  5d                    pop       ebp
  0046e5f3  894650                mov       [esi+50h],eax
  0046e5f6  894e54                mov       [esi+54h],ecx
  0046e5f9  895658                mov       [esi+58h],edx
  0046e5fc  5f                    pop       edi
  0046e5fd  5e                    pop       esi
  0046e5fe  83c408                add       esp,8
  0046e601  c3                    ret
