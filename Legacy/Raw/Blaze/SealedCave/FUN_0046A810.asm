  0046a810  83ec08                sub       esp,8
  0046a813  56                    push      esi
  0046a814  8b742410              mov       esi,[esp+10h]
  0046a818  668b4610              mov       ax,[esi+10h]
  0046a81c  663d0200              cmp       ax,2
  0046a820  750b                  jne       short 0046A82Dh
  0046a822  66c746300000          mov       word [esi+30h],0
  0046a828  5e                    pop       esi
  0046a829  83c408                add       esp,8
  0046a82c  c3                    ret
  0046a82d  33c9                  xor       ecx,ecx
  0046a82f  57                    push      edi
  0046a830  55                    push      ebp
  0046a831  663bc1                cmp       ax,cx
  0046a834  53                    push      ebx
  0046a835  7533                  jne       short 0046A86Ah
  0046a837  0fbf4616              movsx     eax,word [esi+16h]
  0046a83b  89465c                mov       [esi+5Ch],eax
  0046a83e  668b4630              mov       ax,[esi+30h]
  0046a842  663bc1                cmp       ax,cx
  0046a845  66c746100100          mov       word [esi+10h],1
  0046a84b  66898e9a000000        mov       [esi+9Ah],cx
  0046a852  740d                  je        short 0046A861h
  0046a854  66898698000000        mov       [esi+98h],ax
  0046a85b  66894e30              mov       [esi+30h],cx
  0046a85f  eb09                  jmp       short 0046A86Ah
  0046a861  66c786980000002c01    mov       word [esi+98h],12Ch
  0046a86a  8b4658                mov       eax,[esi+58h]
  0046a86d  8b4e50                mov       ecx,[esi+50h]
  0046a870  89442414              mov       [esp+14h],eax
  0046a874  894c241c              mov       [esp+1Ch],ecx
  0046a878  0fbf4614              movsx     eax,word [esi+14h]
  0046a87c  8b5654                mov       edx,[esi+54h]
  0046a87f  8b6e20                mov       ebp,[esi+20h]
  0046a882  8d0c80                lea       ecx,[eax+eax*4]
  0046a885  89542410              mov       [esp+10h],edx
  0046a889  c1e103                shl       ecx,3
  0046a88c  2bc8                  sub       ecx,eax
  0046a88e  8bfd                  mov       edi,ebp
  0046a890  c1ff0c                sar       edi,0Ch
  0046a893  8b548e1c              mov       edx,[esi+ecx*4+1Ch]
  0046a897  8d048e                lea       eax,[esi+ecx*4]
  0046a89a  895650                mov       [esi+50h],edx
  0046a89d  8b561c                mov       edx,[esi+1Ch]
  0046a8a0  8b4820                mov       ecx,[eax+20h]
  0046a8a3  894e54                mov       [esi+54h],ecx
  0046a8a6  8b4e50                mov       ecx,[esi+50h]
  0046a8a9  8b4024                mov       eax,[eax+24h]
  0046a8ac  c1fa0c                sar       edx,0Ch
  0046a8af  c1f90c                sar       ecx,0Ch
  0046a8b2  2bca                  sub       ecx,edx
  0046a8b4  8b5654                mov       edx,[esi+54h]
  0046a8b7  c1fa0c                sar       edx,0Ch
  0046a8ba  2bd7                  sub       edx,edi
  0046a8bc  8b7e24                mov       edi,[esi+24h]
  0046a8bf  894658                mov       [esi+58h],eax
  0046a8c2  c1ff0c                sar       edi,0Ch
  0046a8c5  c1f80c                sar       eax,0Ch
  0046a8c8  2bc7                  sub       eax,edi
  0046a8ca  8bf9                  mov       edi,ecx
  0046a8cc  0faff9                imul      edi,ecx
  0046a8cf  8bca                  mov       ecx,edx
  0046a8d1  0fafca                imul      ecx,edx
  0046a8d4  8bd0                  mov       edx,eax
  0046a8d6  03f9                  add       edi,ecx
  0046a8d8  0fafd0                imul      edx,eax
  0046a8db  03fa                  add       edi,edx
  0046a8dd  57                    push      edi
  0046a8de  e82db10d00            call      00545A10h
  0046a8e3  8b4e5c                mov       ecx,[esi+5Ch]
  0046a8e6  8bf8                  mov       edi,eax
  0046a8e8  8bc1                  mov       eax,ecx
  0046a8ea  99                    cdq
  0046a8eb  83e207                and       edx,7
  0046a8ee  03c2                  add       eax,edx
  0046a8f0  8bd8                  mov       ebx,eax
  0046a8f2  0fbf8698000000        movsx     eax,word [esi+98h]
  0046a8f9  c1fb03                sar       ebx,3
  0046a8fc  3bf8                  cmp       edi,eax
  0046a8fe  0f8d9e000000          jge       near 0046A9A2h
  0046a904  33ff                  xor       edi,edi
  0046a906  47                    inc       edi
  0046a907  66897e12              mov       [esi+12h],di
  0046a90b  e830c80d00            call      00547140h
  0046a910  99                    cdq
  0046a911  b903000000            mov       ecx,3
  0046a916  6a00                  push      0
  0046a918  f7f9                  idiv      ecx
  0046a91a  6a00                  push      0
  0046a91c  6aff                  push      0FFFFFFFFh
  0046a91e  6a10                  push      10h
  0046a920  6a30                  push      30h
  0046a922  56                    push      esi
  0046a923  66895616              mov       [esi+16h],dx
  0046a927  e8e4d7ffff            call      00468110h
  0046a92c  83c418                add       esp,18h
  0046a92f  6683ff05              cmp       di,5
  0046a933  7ed1                  jle       short 0046A906h
  0046a935  8b4e5c                mov       ecx,[esi+5Ch]
  0046a938  85c9                  test      ecx,ecx
  0046a93a  7420                  je        short 0046A95Ch
  0046a93c  0fbfc3                movsx     eax,bx
  0046a93f  8d14c500000000        lea       edx,[eax*8]
  0046a946  2bca                  sub       ecx,edx
  0046a948  b201                  mov       dl,1
  0046a94a  d2e2                  shl       dl,cl
  0046a94c  8a88e0965e00          mov       cl,[eax+5E96E0h]
  0046a952  f6d2                  not       dl
  0046a954  22ca                  and       cl,dl
  0046a956  8888e0965e00          mov       [eax+5E96E0h],cl
  0046a95c  66ff869a000000        inc       word [esi+9Ah]
  0046a963  6683be9a0000000d      cmp       word [esi+9Ah],0Dh
  0046a96b  754f                  jne       short 0046A9BCh
  0046a96d  66c7869a0000000000    mov       word [esi+9Ah],0
  0046a976  a1cc975e00            mov       eax,[5E97CCh]
  0046a97b  6a00                  push      0
  0046a97d  6a00                  push      0
  0046a97f  8b88a8000000          mov       ecx,[eax+0A8h]
  0046a985  6800100000            push      1000h
  0046a98a  6800020000            push      200h
  0046a98f  8b11                  mov       edx,[ecx]
  0046a991  660fb64237            movzx     ax,byte [edx+37h]
  0046a996  50                    push      eax
  0046a997  56                    push      esi
  0046a998  e8e3d6ffff            call      00468080h
  0046a99d  83c418                add       esp,18h
  0046a9a0  eb1a                  jmp       short 0046A9BCh
  0046a9a2  85c9                  test      ecx,ecx
  0046a9a4  7416                  je        short 0046A9BCh
  0046a9a6  0fbfc3                movsx     eax,bx
  0046a9a9  8d14c500000000        lea       edx,[eax*8]
  0046a9b0  2bca                  sub       ecx,edx
  0046a9b2  b201                  mov       dl,1
  0046a9b4  d2e2                  shl       dl,cl
  0046a9b6  0890e0965e00          or        [eax+5E96E0h],dl
  0046a9bc  8b44241c              mov       eax,[esp+1Ch]
  0046a9c0  8b4c2410              mov       ecx,[esp+10h]
  0046a9c4  8b542414              mov       edx,[esp+14h]
  0046a9c8  896e20                mov       [esi+20h],ebp
  0046a9cb  5b                    pop       ebx
  0046a9cc  5d                    pop       ebp
  0046a9cd  894650                mov       [esi+50h],eax
  0046a9d0  894e54                mov       [esi+54h],ecx
  0046a9d3  895658                mov       [esi+58h],edx
  0046a9d6  5f                    pop       edi
  0046a9d7  5e                    pop       esi
  0046a9d8  83c408                add       esp,8
  0046a9db  c3                    ret
