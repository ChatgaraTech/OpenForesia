  00486b10  83ec18                sub       esp,18h
  00486b13  53                    push      ebx
  00486b14  55                    push      ebp
  00486b15  56                    push      esi
  00486b16  8b742428              mov       esi,[esp+28h]
  00486b1a  57                    push      edi
  00486b1b  33ff                  xor       edi,edi
  00486b1d  8b16                  mov       edx,[esi]
  00486b1f  8b466c                mov       eax,[esi+6Ch]
  00486b22  bb00010000            mov       ebx,100h
  00486b27  b900020000            mov       ecx,200h
  00486b2c  f7c200000002          test      edx,2000000h
  00486b32  66897c2410            mov       [esp+10h],di
  00486b37  66c744241200ff        mov       word [esp+12h],0FF00h
  00486b3e  66895c2414            mov       [esp+14h],bx
  00486b43  66c74424169600        mov       word [esp+16h],96h
  00486b4a  66897c2418            mov       [esp+18h],di
  00486b4f  66894c241a            mov       [esp+1Ah],cx
  00486b54  0f847b010000          je        near 00486CD5h
  00486b5a  668b4610              mov       ax,[esi+10h]
  00486b5e  663d1100              cmp       ax,11h
  00486b62  7d3c                  jge       short 00486BA0h
  00486b64  40                    inc       eax
  00486b65  66c746160100          mov       word [esi+16h],1
  00486b6b  66894610              mov       [esi+10h],ax
  00486b6f  a1cc975e00            mov       eax,[5E97CCh]
  00486b74  6a01                  push      1
  00486b76  6a01                  push      1
  00486b78  8b88d0000000          mov       ecx,[eax+0D0h]
  00486b7e  33c0                  xor       eax,eax
  00486b80  8b11                  mov       edx,[ecx]
  00486b82  8a4212                mov       al,[edx+12h]
  00486b85  05c0000000            add       eax,0C0h
  00486b8a  50                    push      eax
  00486b8b  56                    push      esi
  00486b8c  e84f3fffff            call      0047AAE0h
  00486b91  83c410                add       esp,10h
  00486b94  66015e32              add       [esi+32h],bx
  00486b98  5f                    pop       edi
  00486b99  5e                    pop       esi
  00486b9a  5d                    pop       ebp
  00486b9b  5b                    pop       ebx
  00486b9c  83c418                add       esp,18h
  00486b9f  c3                    ret
  00486ba0  7511                  jne       short 00486BB3h
  00486ba2  8d4e1c                lea       ecx,[esi+1Ch]
  00486ba5  51                    push      ecx
  00486ba6  6820285500            push      552820h
  00486bab  e830700b00            call      0053DBE0h
  00486bb0  83c408                add       esp,8
  00486bb3  66ff4610              inc       word [esi+10h]
  00486bb7  66c7461a0700          mov       word [esi+1Ah],7
  00486bbd  0fbf6e32              movsx     ebp,word [esi+32h]
  00486bc1  c7463860606001        mov       dword [esi+38h],1606060h
  00486bc8  66c746120300          mov       word [esi+12h],3
  00486bce  c7462800200020        mov       dword [esi+28h],20002000h
  00486bd5  66c746140040          mov       word [esi+14h],4000h
  00486bdb  66c746160200          mov       word [esi+16h],2
  00486be1  c744242c04000000      mov       dword [esp+2Ch],4
  00486be9  e852050c00            call      00547140h
  00486bee  99                    cdq
  00486bef  b990010000            mov       ecx,190h
  00486bf4  f7f9                  idiv      ecx
  00486bf6  8bfa                  mov       edi,edx
  00486bf8  83c732                add       edi,32h
  00486bfb  e840050c00            call      00547140h
  00486c00  25ff000000            and       eax,0FFh
  00486c05  2d80000000            sub       eax,80h
  00486c0a  8d1c28                lea       ebx,[eax+ebp]
  00486c0d  53                    push      ebx
  00486c0e  e809ee0b00            call      00545A1Ch
  00486c13  0fafc7                imul      eax,edi
  00486c16  8b4e1c                mov       ecx,[esi+1Ch]
  00486c19  8b5620                mov       edx,[esi+20h]
  00486c1c  03c1                  add       eax,ecx
  00486c1e  53                    push      ebx
  00486c1f  89442420              mov       [esp+20h],eax
  00486c23  89542424              mov       [esp+24h],edx
  00486c27  e8eaed0b00            call      00545A16h
  00486c2c  0fafc7                imul      eax,edi
  00486c2f  8b7e24                mov       edi,[esi+24h]
  00486c32  53                    push      ebx
  00486c33  03c7                  add       eax,edi
  00486c35  89442428              mov       [esp+28h],eax
  00486c39  e8deed0b00            call      00545A1Ch
  00486c3e  c1e006                shl       eax,6
  00486c41  53                    push      ebx
  00486c42  894650                mov       [esi+50h],eax
  00486c45  c7465400000400        mov       dword [esi+54h],40000h
  00486c4c  e8c5ed0b00            call      00545A16h
  00486c51  c1e006                shl       eax,6
  00486c54  6a01                  push      1
  00486c56  894658                mov       [esi+58h],eax
  00486c59  6a01                  push      1
  00486c5b  68be000000            push      0BEh
  00486c60  8d442428              lea       eax,[esp+28h]
  00486c64  68081a5500            push      551A08h
  00486c69  50                    push      eax
  00486c6a  56                    push      esi
  00486c6b  e8d03fffff            call      0047AC40h
  00486c70  8b442444              mov       eax,[esp+44h]
  00486c74  83c418                add       esp,18h
  00486c77  48                    dec       eax
  00486c78  8944242c              mov       [esp+2Ch],eax
  00486c7c  0f8567ffffff          jne       near 00486BE9h
  00486c82  8d4c2410              lea       ecx,[esp+10h]
  00486c86  81c500010000          add       ebp,100h
  00486c8c  51                    push      ecx
  00486c8d  56                    push      esi
  00486c8e  66896e32              mov       [esi+32h],bp
  00486c92  e809e0feff            call      00474CA0h
  00486c97  8bf8                  mov       edi,eax
  00486c99  83c408                add       esp,8
  00486c9c  85ff                  test      edi,edi
  00486c9e  741c                  je        short 00486CBCh
  00486ca0  8b5674                mov       edx,[esi+74h]
  00486ca3  57                    push      edi
  00486ca4  52                    push      edx
  00486ca5  e8d62e0b00            call      00539B80h
  00486caa  8b07                  mov       eax,[edi]
  00486cac  83c408                add       esp,8
  00486caf  0c40                  or        al,40h
  00486cb1  66c787960000000800    mov       word [edi+96h],8
  00486cba  8907                  mov       [edi],eax
  00486cbc  66837e1021            cmp       word [esi+10h],21h
  00486cc1  0f8c91000000          jl        near 00486D58h
  00486cc7  c70600000000          mov       dword [esi],0
  00486ccd  5f                    pop       edi
  00486cce  5e                    pop       esi
  00486ccf  5d                    pop       ebp
  00486cd0  5b                    pop       ebx
  00486cd1  83c418                add       esp,18h
  00486cd4  c3                    ret
  00486cd5  66397e10              cmp       [esi+10h],di
  00486cd9  757d                  jne       short 00486D58h
  00486cdb  66c746100100          mov       word [esi+10h],1
  00486ce1  894674                mov       [esi+74h],eax
  00486ce4  897670                mov       [esi+70h],esi
  00486ce7  66c746360400          mov       word [esi+36h],4
  00486ced  c646043b              mov       byte [esi+4],3Bh
  00486cf1  897e28                mov       [esi+28h],edi
  00486cf4  66894e2c              mov       [esi+2Ch],cx
  00486cf8  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00486cff  897e44                mov       [esi+44h],edi
  00486d02  897e48                mov       [esi+48h],edi
  00486d05  897e4c                mov       [esi+4Ch],edi
  00486d08  66897e16              mov       [esi+16h],di
  00486d0c  a1cc975e00            mov       eax,[5E97CCh]
  00486d11  6a01                  push      1
  00486d13  6a01                  push      1
  00486d15  8b88d0000000          mov       ecx,[eax+0D0h]
  00486d1b  33c0                  xor       eax,eax
  00486d1d  8b11                  mov       edx,[ecx]
  00486d1f  8a4212                mov       al,[edx+12h]
  00486d22  05c0000000            add       eax,0C0h
  00486d27  50                    push      eax
  00486d28  56                    push      esi
  00486d29  e8b23dffff            call      0047AAE0h
  00486d2e  668146328000          add       word [esi+32h],80h
  00486d34  83c410                add       esp,10h
  00486d37  897e70                mov       [esi+70h],edi
  00486d3a  c7462800180010        mov       dword [esi+28h],10001800h
  00486d41  66c7462cf811          mov       word [esi+2Ch],11F8h
  00486d47  83c61c                add       esi,1Ch
  00486d4a  56                    push      esi
  00486d4b  6800285500            push      552800h
  00486d50  e88b6e0b00            call      0053DBE0h
  00486d55  83c408                add       esp,8
  00486d58  5f                    pop       edi
  00486d59  5e                    pop       esi
  00486d5a  5d                    pop       ebp
  00486d5b  5b                    pop       ebx
  00486d5c  83c418                add       esp,18h
  00486d5f  c3                    ret
