  00438380  53                    push      ebx
  00438381  56                    push      esi
  00438382  8b74240c              mov       esi,[esp+0Ch]
  00438386  33db                  xor       ebx,ebx
  00438388  57                    push      edi
  00438389  bf01000000            mov       edi,1
  0043838e  66395e10              cmp       [esi+10h],bx
  00438392  7569                  jne       short 004383FDh
  00438394  66897e10              mov       [esi+10h],di
  00438398  c7463c300f5500        mov       dword [esi+3Ch],550F30h
  0043839f  66895e28              mov       [esi+28h],bx
  004383a3  66895e2a              mov       [esi+2Ah],bx
  004383a7  c6460a09              mov       byte [esi+0Ah],9
  004383ab  885e0b                mov       [esi+0Bh],bl
  004383ae  e88ded1000            call      00547140h
  004383b3  99                    cdq
  004383b4  b9c8000000            mov       ecx,0C8h
  004383b9  66c746140a00          mov       word [esi+14h],0Ah
  004383bf  f7f9                  idiv      ecx
  004383c1  8b4620                mov       eax,[esi+20h]
  004383c4  8b4e24                mov       ecx,[esi+24h]
  004383c7  894654                mov       [esi+54h],eax
  004383ca  894e58                mov       [esi+58h],ecx
  004383cd  885e40                mov       [esi+40h],bl
  004383d0  81c2fa000000          add       edx,0FAh
  004383d6  66895612              mov       [esi+12h],dx
  004383da  8b561c                mov       edx,[esi+1Ch]
  004383dd  895650                mov       [esi+50h],edx
  004383e0  e85bed1000            call      00547140h
  004383e5  99                    cdq
  004383e6  33c2                  xor       eax,edx
  004383e8  66c7462ee2ff          mov       word [esi+2Eh],0FFE2h
  004383ee  2bc2                  sub       eax,edx
  004383f0  25ff0f0000            and       eax,0FFFh
  004383f5  33c2                  xor       eax,edx
  004383f7  2bc2                  sub       eax,edx
  004383f9  66894616              mov       [esi+16h],ax
  004383fd  66397e10              cmp       [esi+10h],di
  00438401  0f85e3000000          jne       near 004384EAh
  00438407  b9f4010000            mov       ecx,1F4h
  0043840c  ba00100000            mov       edx,1000h
  00438411  66014e28              add       [esi+28h],cx
  00438415  668b4628              mov       ax,[esi+28h]
  00438419  663bc2                cmp       ax,dx
  0043841c  7e04                  jle       short 00438422h
  0043841e  66895628              mov       [esi+28h],dx
  00438422  66014e2a              add       [esi+2Ah],cx
  00438426  6639562a              cmp       [esi+2Ah],dx
  0043842a  7e04                  jle       short 00438430h
  0043842c  6689562a              mov       [esi+2Ah],dx
  00438430  0fbf5616              movsx     edx,word [esi+16h]
  00438434  52                    push      edx
  00438435  e8e2d51000            call      00545A1Ch
  0043843a  0fbf4e12              movsx     ecx,word [esi+12h]
  0043843e  c1f902                sar       ecx,2
  00438441  0fafc1                imul      eax,ecx
  00438444  0fbf5632              movsx     edx,word [esi+32h]
  00438448  8b4e50                mov       ecx,[esi+50h]
  0043844b  52                    push      edx
  0043844c  03c1                  add       eax,ecx
  0043844e  89461c                mov       [esi+1Ch],eax
  00438451  e8c0d51000            call      00545A16h
  00438456  0fbf4e12              movsx     ecx,word [esi+12h]
  0043845a  0fafc1                imul      eax,ecx
  0043845d  0fbf5616              movsx     edx,word [esi+16h]
  00438461  8b4e54                mov       ecx,[esi+54h]
  00438464  668b7e12              mov       di,[esi+12h]
  00438468  03c1                  add       eax,ecx
  0043846a  52                    push      edx
  0043846b  894620                mov       [esi+20h],eax
  0043846e  e8a3d51000            call      00545A16h
  00438473  0fbfcf                movsx     ecx,di
  00438476  c1f902                sar       ecx,2
  00438479  0fafc1                imul      eax,ecx
  0043847c  034658                add       eax,[esi+58h]
  0043847f  894624                mov       [esi+24h],eax
  00438482  668b4614              mov       ax,[esi+14h]
  00438486  668bd0                mov       dx,ax
  00438489  66d1fa                sar       dx,1
  0043848c  2bfa                  sub       edi,edx
  0043848e  40                    inc       eax
  0043848f  66894614              mov       [esi+14h],ax
  00438493  668b4616              mov       ax,[esi+16h]
  00438497  66056400              add       ax,64h
  0043849b  66897e12              mov       [esi+12h],di
  0043849f  25ff0f0000            and       eax,0FFFh
  004384a4  66894616              mov       [esi+16h],ax
  004384a8  e893ec1000            call      00547140h
  004384ad  99                    cdq
  004384ae  b906000000            mov       ecx,6
  004384b3  f7f9                  idiv      ecx
  004384b5  8b4e1c                mov       ecx,[esi+1Ch]
  004384b8  83ea03                sub       edx,3
  004384bb  c1e20c                shl       edx,0Ch
  004384be  03ca                  add       ecx,edx
  004384c0  894e1c                mov       [esi+1Ch],ecx
  004384c3  e878ec1000            call      00547140h
  004384c8  99                    cdq
  004384c9  b906000000            mov       ecx,6
  004384ce  f7f9                  idiv      ecx
  004384d0  8b4e24                mov       ecx,[esi+24h]
  004384d3  83ea03                sub       edx,3
  004384d6  c1e20c                shl       edx,0Ch
  004384d9  03ca                  add       ecx,edx
  004384db  66395e12              cmp       [esi+12h],bx
  004384df  894e24                mov       [esi+24h],ecx
  004384e2  7d06                  jge       short 004384EAh
  004384e4  8126ffffff7f          and       dword [esi],7FFFFFFFh
  004384ea  5f                    pop       edi
  004384eb  5e                    pop       esi
  004384ec  5b                    pop       ebx
  004384ed  c3                    ret
