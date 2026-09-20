  0046a9e0  83ec08                sub       esp,8
  0046a9e3  56                    push      esi
  0046a9e4  8b742410              mov       esi,[esp+10h]
  0046a9e8  668b4610              mov       ax,[esi+10h]
  0046a9ec  663d0200              cmp       ax,2
  0046a9f0  750b                  jne       short 0046A9FDh
  0046a9f2  66c746300000          mov       word [esi+30h],0
  0046a9f8  5e                    pop       esi
  0046a9f9  83c408                add       esp,8
  0046a9fc  c3                    ret
  0046a9fd  33c9                  xor       ecx,ecx
  0046a9ff  57                    push      edi
  0046aa00  55                    push      ebp
  0046aa01  663bc1                cmp       ax,cx
  0046aa04  53                    push      ebx
  0046aa05  7533                  jne       short 0046AA3Ah
  0046aa07  0fbf4616              movsx     eax,word [esi+16h]
  0046aa0b  89465c                mov       [esi+5Ch],eax
  0046aa0e  668b4630              mov       ax,[esi+30h]
  0046aa12  663bc1                cmp       ax,cx
  0046aa15  66c746100100          mov       word [esi+10h],1
  0046aa1b  66898e9a000000        mov       [esi+9Ah],cx
  0046aa22  740d                  je        short 0046AA31h
  0046aa24  66898698000000        mov       [esi+98h],ax
  0046aa2b  66894e30              mov       [esi+30h],cx
  0046aa2f  eb09                  jmp       short 0046AA3Ah
  0046aa31  66c786980000002c01    mov       word [esi+98h],12Ch
  0046aa3a  8b4658                mov       eax,[esi+58h]
  0046aa3d  8b4e50                mov       ecx,[esi+50h]
  0046aa40  89442414              mov       [esp+14h],eax
  0046aa44  894c241c              mov       [esp+1Ch],ecx
  0046aa48  0fbf4614              movsx     eax,word [esi+14h]
  0046aa4c  8b5654                mov       edx,[esi+54h]
  0046aa4f  8b6e20                mov       ebp,[esi+20h]
  0046aa52  8d0c80                lea       ecx,[eax+eax*4]
  0046aa55  89542410              mov       [esp+10h],edx
  0046aa59  c1e103                shl       ecx,3
  0046aa5c  2bc8                  sub       ecx,eax
  0046aa5e  8bfd                  mov       edi,ebp
  0046aa60  c1ff0c                sar       edi,0Ch
  0046aa63  8b548e1c              mov       edx,[esi+ecx*4+1Ch]
  0046aa67  8d048e                lea       eax,[esi+ecx*4]
  0046aa6a  895650                mov       [esi+50h],edx
  0046aa6d  8b561c                mov       edx,[esi+1Ch]
  0046aa70  8b4820                mov       ecx,[eax+20h]
  0046aa73  894e54                mov       [esi+54h],ecx
  0046aa76  8b4e50                mov       ecx,[esi+50h]
  0046aa79  8b4024                mov       eax,[eax+24h]
  0046aa7c  c1fa0c                sar       edx,0Ch
  0046aa7f  c1f90c                sar       ecx,0Ch
  0046aa82  2bca                  sub       ecx,edx
  0046aa84  8b5654                mov       edx,[esi+54h]
  0046aa87  c1fa0c                sar       edx,0Ch
  0046aa8a  2bd7                  sub       edx,edi
  0046aa8c  8b7e24                mov       edi,[esi+24h]
  0046aa8f  894658                mov       [esi+58h],eax
  0046aa92  c1ff0c                sar       edi,0Ch
  0046aa95  c1f80c                sar       eax,0Ch
  0046aa98  2bc7                  sub       eax,edi
  0046aa9a  8bf9                  mov       edi,ecx
  0046aa9c  0faff9                imul      edi,ecx
  0046aa9f  8bca                  mov       ecx,edx
  0046aaa1  0fafca                imul      ecx,edx
  0046aaa4  8bd0                  mov       edx,eax
  0046aaa6  03f9                  add       edi,ecx
  0046aaa8  0fafd0                imul      edx,eax
  0046aaab  03fa                  add       edi,edx
  0046aaad  57                    push      edi
  0046aaae  e85daf0d00            call      00545A10h
  0046aab3  8b4e5c                mov       ecx,[esi+5Ch]
  0046aab6  8bf8                  mov       edi,eax
  0046aab8  8bc1                  mov       eax,ecx
  0046aaba  99                    cdq
  0046aabb  83e207                and       edx,7
  0046aabe  03c2                  add       eax,edx
  0046aac0  8bd8                  mov       ebx,eax
  0046aac2  0fbf8698000000        movsx     eax,word [esi+98h]
  0046aac9  c1fb03                sar       ebx,3
  0046aacc  3bf8                  cmp       edi,eax
  0046aace  0f8dad000000          jge       near 0046AB81h
  0046aad4  8b4620                mov       eax,[esi+20h]
  0046aad7  0500200800            add       eax,82000h
  0046aadc  894620                mov       [esi+20h],eax
  0046aadf  33c0                  xor       eax,eax
  0046aae1  8d7801                lea       edi,[eax+1]
  0046aae4  66897e12              mov       [esi+12h],di
  0046aae8  e853c60d00            call      00547140h
  0046aaed  99                    cdq
  0046aaee  b903000000            mov       ecx,3
  0046aaf3  6a00                  push      0
  0046aaf5  f7f9                  idiv      ecx
  0046aaf7  6a00                  push      0
  0046aaf9  6aff                  push      0FFFFFFFFh
  0046aafb  6a10                  push      10h
  0046aafd  6a30                  push      30h
  0046aaff  56                    push      esi
  0046ab00  66895616              mov       [esi+16h],dx
  0046ab04  e807d6ffff            call      00468110h
  0046ab09  8bc7                  mov       eax,edi
  0046ab0b  83c418                add       esp,18h
  0046ab0e  663d0500              cmp       ax,5
  0046ab12  7ecd                  jle       short 0046AAE1h
  0046ab14  8b4e5c                mov       ecx,[esi+5Ch]
  0046ab17  85c9                  test      ecx,ecx
  0046ab19  7420                  je        short 0046AB3Bh
  0046ab1b  0fbfc3                movsx     eax,bx
  0046ab1e  8d14c500000000        lea       edx,[eax*8]
  0046ab25  2bca                  sub       ecx,edx
  0046ab27  b201                  mov       dl,1
  0046ab29  d2e2                  shl       dl,cl
  0046ab2b  8a88e0965e00          mov       cl,[eax+5E96E0h]
  0046ab31  f6d2                  not       dl
  0046ab33  22ca                  and       cl,dl
  0046ab35  8888e0965e00          mov       [eax+5E96E0h],cl
  0046ab3b  66ff869a000000        inc       word [esi+9Ah]
  0046ab42  6683be9a0000000d      cmp       word [esi+9Ah],0Dh
  0046ab4a  754f                  jne       short 0046AB9Bh
  0046ab4c  66c7869a0000000000    mov       word [esi+9Ah],0
  0046ab55  a1cc975e00            mov       eax,[5E97CCh]
  0046ab5a  6a00                  push      0
  0046ab5c  6a00                  push      0
  0046ab5e  8b88a8000000          mov       ecx,[eax+0A8h]
  0046ab64  6800100000            push      1000h
  0046ab69  6800020000            push      200h
  0046ab6e  8b11                  mov       edx,[ecx]
  0046ab70  660fb64237            movzx     ax,byte [edx+37h]
  0046ab75  50                    push      eax
  0046ab76  56                    push      esi
  0046ab77  e804d5ffff            call      00468080h
  0046ab7c  83c418                add       esp,18h
  0046ab7f  eb1a                  jmp       short 0046AB9Bh
  0046ab81  85c9                  test      ecx,ecx
  0046ab83  7416                  je        short 0046AB9Bh
  0046ab85  0fbfc3                movsx     eax,bx
  0046ab88  8d14c500000000        lea       edx,[eax*8]
  0046ab8f  2bca                  sub       ecx,edx
  0046ab91  b201                  mov       dl,1
  0046ab93  d2e2                  shl       dl,cl
  0046ab95  0890e0965e00          or        [eax+5E96E0h],dl
  0046ab9b  8b44241c              mov       eax,[esp+1Ch]
  0046ab9f  8b4c2410              mov       ecx,[esp+10h]
  0046aba3  8b542414              mov       edx,[esp+14h]
  0046aba7  896e20                mov       [esi+20h],ebp
  0046abaa  5b                    pop       ebx
  0046abab  5d                    pop       ebp
  0046abac  894650                mov       [esi+50h],eax
  0046abaf  894e54                mov       [esi+54h],ecx
  0046abb2  895658                mov       [esi+58h],edx
  0046abb5  5f                    pop       edi
  0046abb6  5e                    pop       esi
  0046abb7  83c408                add       esp,8
  0046abba  c3                    ret
