  00431d80  53                    push      ebx
  00431d81  56                    push      esi
  00431d82  8b74240c              mov       esi,[esp+0Ch]
  00431d86  bb01000000            mov       ebx,1
  00431d8b  66837e1000            cmp       word [esi+10h],0
  00431d90  0f85bd000000          jne       near 00431E53h
  00431d96  b82c010000            mov       eax,12Ch
  00431d9b  c6460400              mov       byte [esi+4],0
  00431d9f  66894628              mov       [esi+28h],ax
  00431da3  6689462c              mov       [esi+2Ch],ax
  00431da7  b096                  mov       al,96h
  00431da9  66c7462a0000          mov       word [esi+2Ah],0
  00431daf  c6460a03              mov       byte [esi+0Ah],3
  00431db3  885e0b                mov       [esi+0Bh],bl
  00431db6  884638                mov       [esi+38h],al
  00431db9  884639                mov       [esi+39h],al
  00431dbc  88463a                mov       [esi+3Ah],al
  00431dbf  66c746120000          mov       word [esi+12h],0
  00431dc5  e876531100            call      00547140h
  00431dca  99                    cdq
  00431dcb  b919000000            mov       ecx,19h
  00431dd0  f7f9                  idiv      ecx
  00431dd2  8b4620                mov       eax,[esi+20h]
  00431dd5  8b4e24                mov       ecx,[esi+24h]
  00431dd8  894654                mov       [esi+54h],eax
  00431ddb  894e58                mov       [esi+58h],ecx
  00431dde  83c228                add       edx,28h
  00431de1  89565c                mov       [esi+5Ch],edx
  00431de4  8b561c                mov       edx,[esi+1Ch]
  00431de7  895650                mov       [esi+50h],edx
  00431dea  e851531100            call      00547140h
  00431def  99                    cdq
  00431df0  b914000000            mov       ecx,14h
  00431df5  66c746340004          mov       word [esi+34h],400h
  00431dfb  f7f9                  idiv      ecx
  00431dfd  668b4632              mov       ax,[esi+32h]
  00431e01  83c21e                add       edx,1Eh
  00431e04  66895616              mov       [esi+16h],dx
  00431e08  0fbfd0                movsx     edx,ax
  00431e0b  81c200080000          add       edx,800h
  00431e11  05000c0000            add       eax,0C00h
  00431e16  895648                mov       [esi+48h],edx
  00431e19  66894632              mov       [esi+32h],ax
  00431e1d  e81e531100            call      00547140h
  00431e22  99                    cdq
  00431e23  b903000000            mov       ecx,3
  00431e28  66895e10              mov       [esi+10h],bx
  00431e2c  f7f9                  idiv      ecx
  00431e2e  b8feffffff            mov       eax,0FFFFFFFEh
  00431e33  2bc2                  sub       eax,edx
  00431e35  6689869a000000        mov       [esi+9Ah],ax
  00431e3c  e8ff521100            call      00547140h
  00431e41  99                    cdq
  00431e42  33c2                  xor       eax,edx
  00431e44  2bc2                  sub       eax,edx
  00431e46  25ff070000            and       eax,7FFh
  00431e4b  33c2                  xor       eax,edx
  00431e4d  2bc2                  sub       eax,edx
  00431e4f  66894614              mov       [esi+14h],ax
  00431e53  66395e10              cmp       [esi+10h],bx
  00431e57  0f85be000000          jne       near 00431F1Bh
  00431e5d  8b4e48                mov       ecx,[esi+48h]
  00431e60  51                    push      ecx
  00431e61  e8b63b1100            call      00545A1Ch
  00431e66  0fbf5612              movsx     edx,word [esi+12h]
  00431e6a  8bd8                  mov       ebx,eax
  00431e6c  0fbf4614              movsx     eax,word [esi+14h]
  00431e70  0fafda                imul      ebx,edx
  00431e73  50                    push      eax
  00431e74  e89d3b1100            call      00545A16h
  00431e79  8d0440                lea       eax,[eax+eax*2]
  00431e7c  8d0c80                lea       ecx,[eax+eax*4]
  00431e7f  8b4648                mov       eax,[esi+48h]
  00431e82  50                    push      eax
  00431e83  8d144b                lea       edx,[ebx+ecx*2]
  00431e86  8b5e50                mov       ebx,[esi+50h]
  00431e89  03d3                  add       edx,ebx
  00431e8b  89561c                mov       [esi+1Ch],edx
  00431e8e  e8833b1100            call      00545A16h
  00431e93  0fbf5614              movsx     edx,word [esi+14h]
  00431e97  0fbf4e12              movsx     ecx,word [esi+12h]
  00431e9b  8bd8                  mov       ebx,eax
  00431e9d  52                    push      edx
  00431e9e  0fafd9                imul      ebx,ecx
  00431ea1  e8703b1100            call      00545A16h
  00431ea6  0fbf5614              movsx     edx,word [esi+14h]
  00431eaa  8d0440                lea       eax,[eax+eax*2]
  00431ead  52                    push      edx
  00431eae  8d0480                lea       eax,[eax+eax*4]
  00431eb1  8d0c43                lea       ecx,[ebx+eax*2]
  00431eb4  8b4658                mov       eax,[esi+58h]
  00431eb7  03c8                  add       ecx,eax
  00431eb9  894e24                mov       [esi+24h],ecx
  00431ebc  e85b3b1100            call      00545A1Ch
  00431ec1  8b4e54                mov       ecx,[esi+54h]
  00431ec4  8d0440                lea       eax,[eax+eax*2]
  00431ec7  8d0480                lea       eax,[eax+eax*4]
  00431eca  8d1441                lea       edx,[ecx+eax*2]
  00431ecd  668b465c              mov       ax,[esi+5Ch]
  00431ed1  66014612              add       [esi+12h],ax
  00431ed5  8b465c                mov       eax,[esi+5Ch]
  00431ed8  83c002                add       eax,2
  00431edb  895620                mov       [esi+20h],edx
  00431ede  89465c                mov       [esi+5Ch],eax
  00431ee1  668b869a000000        mov       ax,[esi+9Ah]
  00431ee8  50                    push      eax
  00431ee9  50                    push      eax
  00431eea  50                    push      eax
  00431eeb  56                    push      esi
  00431eec  e8ef2efdff            call      00404DE0h
  00431ef1  6681462af401          add       word [esi+2Ah],1F4h
  00431ef7  668b462a              mov       ax,[esi+2Ah]
  00431efb  83c410                add       esp,10h
  00431efe  663d7017              cmp       ax,1770h
  00431f02  7e06                  jle       short 00431F0Ah
  00431f04  66c7462a7017          mov       word [esi+2Ah],1770h
  00431f0a  66ff4e16              dec       word [esi+16h]
  00431f0e  66837e1600            cmp       word [esi+16h],0
  00431f13  7d06                  jge       short 00431F1Bh
  00431f15  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00431f1b  5e                    pop       esi
  00431f1c  5b                    pop       ebx
  00431f1d  c3                    ret
