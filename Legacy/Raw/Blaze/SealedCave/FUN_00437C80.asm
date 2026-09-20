  00437c80  53                    push      ebx
  00437c81  56                    push      esi
  00437c82  8b74240c              mov       esi,[esp+0Ch]
  00437c86  33db                  xor       ebx,ebx
  00437c88  57                    push      edi
  00437c89  bf01000000            mov       edi,1
  00437c8e  66395e10              cmp       [esi+10h],bx
  00437c92  0f85a3000000          jne       near 00437D3Bh
  00437c98  66897e10              mov       [esi+10h],di
  00437c9c  a02ce55d00            mov       al,[5DE52Ch]
  00437ca1  3c10                  cmp       al,10h
  00437ca3  7506                  jne       short 00437CABh
  00437ca5  c6460a09              mov       byte [esi+0Ah],9
  00437ca9  eb04                  jmp       short 00437CAFh
  00437cab  c6460a06              mov       byte [esi+0Ah],6
  00437caf  803d2ce55d0004        cmp       byte [5DE52Ch],4
  00437cb6  7504                  jne       short 00437CBCh
  00437cb8  c6460a02              mov       byte [esi+0Ah],2
  00437cbc  b800200000            mov       eax,2000h
  00437cc1  885e0b                mov       [esi+0Bh],bl
  00437cc4  c7463c680f5500        mov       dword [esi+3Ch],550F68h
  00437ccb  885e40                mov       [esi+40h],bl
  00437cce  66894628              mov       [esi+28h],ax
  00437cd2  6689462a              mov       [esi+2Ah],ax
  00437cd6  e865f41000            call      00547140h
  00437cdb  99                    cdq
  00437cdc  33c2                  xor       eax,edx
  00437cde  2bc2                  sub       eax,edx
  00437ce0  25ff0f0000            and       eax,0FFFh
  00437ce5  33c2                  xor       eax,edx
  00437ce7  2bc2                  sub       eax,edx
  00437ce9  66894612              mov       [esi+12h],ax
  00437ced  e84ef41000            call      00547140h
  00437cf2  99                    cdq
  00437cf3  b90a000000            mov       ecx,0Ah
  00437cf8  f7f9                  idiv      ecx
  00437cfa  03d1                  add       edx,ecx
  00437cfc  66895614              mov       [esi+14h],dx
  00437d00  e83bf41000            call      00547140h
  00437d05  99                    cdq
  00437d06  b905000000            mov       ecx,5
  00437d0b  895e5c                mov       [esi+5Ch],ebx
  00437d0e  f7f9                  idiv      ecx
  00437d10  8b4620                mov       eax,[esi+20h]
  00437d13  8b4e24                mov       ecx,[esi+24h]
  00437d16  894654                mov       [esi+54h],eax
  00437d19  b080                  mov       al,80h
  00437d1b  894e58                mov       [esi+58h],ecx
  00437d1e  66899e9a000000        mov       [esi+9Ah],bx
  00437d25  884638                mov       [esi+38h],al
  00437d28  884639                mov       [esi+39h],al
  00437d2b  88463a                mov       [esi+3Ah],al
  00437d2e  83c203                add       edx,3
  00437d31  66895616              mov       [esi+16h],dx
  00437d35  8b561c                mov       edx,[esi+1Ch]
  00437d38  895650                mov       [esi+50h],edx
  00437d3b  66397e10              cmp       [esi+10h],di
  00437d3f  0f8581000000          jne       near 00437DC6h
  00437d45  0fbf5612              movsx     edx,word [esi+12h]
  00437d49  52                    push      edx
  00437d4a  e8cddc1000            call      00545A1Ch
  00437d4f  0faf465c              imul      eax,[esi+5Ch]
  00437d53  8b7e50                mov       edi,[esi+50h]
  00437d56  03c7                  add       eax,edi
  00437d58  8b7e5c                mov       edi,[esi+5Ch]
  00437d5b  89461c                mov       [esi+1Ch],eax
  00437d5e  0fbf4612              movsx     eax,word [esi+12h]
  00437d62  50                    push      eax
  00437d63  e8aedc1000            call      00545A16h
  00437d68  0fafc7                imul      eax,edi
  00437d6b  8b4e58                mov       ecx,[esi+58h]
  00437d6e  8b5620                mov       edx,[esi+20h]
  00437d71  03c1                  add       eax,ecx
  00437d73  6afb                  push      0FFFFFFFBh
  00437d75  0fbf4e16              movsx     ecx,word [esi+16h]
  00437d79  c1e10c                shl       ecx,0Ch
  00437d7c  03d1                  add       edx,ecx
  00437d7e  6afb                  push      0FFFFFFFBh
  00437d80  895620                mov       [esi+20h],edx
  00437d83  6afb                  push      0FFFFFFFBh
  00437d85  0fbf5614              movsx     edx,word [esi+14h]
  00437d89  03d7                  add       edx,edi
  00437d8b  66ff869a000000        inc       word [esi+9Ah]
  00437d92  56                    push      esi
  00437d93  894624                mov       [esi+24h],eax
  00437d96  89565c                mov       [esi+5Ch],edx
  00437d99  e842d0fcff            call      00404DE0h
  00437d9e  83c410                add       esp,10h
  00437da1  6683be9a00000003      cmp       word [esi+9Ah],3
  00437da9  7e1b                  jle       short 00437DC6h
  00437dab  8a4e40                mov       cl,[esi+40h]
  00437dae  66899e9a000000        mov       [esi+9Ah],bx
  00437db5  fec1                  inc       cl
  00437db7  8ac1                  mov       al,cl
  00437db9  884e40                mov       [esi+40h],cl
  00437dbc  3c04                  cmp       al,4
  00437dbe  7506                  jne       short 00437DC6h
  00437dc0  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00437dc6  5f                    pop       edi
  00437dc7  5e                    pop       esi
  00437dc8  5b                    pop       ebx
  00437dc9  c3                    ret
