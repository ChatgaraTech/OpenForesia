  00436070  51                    push      ecx
  00436071  53                    push      ebx
  00436072  55                    push      ebp
  00436073  56                    push      esi
  00436074  8b742414              mov       esi,[esp+14h]
  00436078  33db                  xor       ebx,ebx
  0043607a  57                    push      edi
  0043607b  8b466c                mov       eax,[esi+6Ch]
  0043607e  3bc3                  cmp       eax,ebx
  00436080  7505                  jne       short 00436087h
  00436082  b858e55d00            mov       eax,5DE558h
  00436087  8b686c                mov       ebp,[eax+6Ch]
  0043608a  3beb                  cmp       ebp,ebx
  0043608c  7505                  jne       short 00436093h
  0043608e  bd58e55d00            mov       ebp,5DE558h
  00436093  395e5c                cmp       [esi+5Ch],ebx
  00436096  0f85fc010000          jne       near 00436298h
  0043609c  66395e10              cmp       [esi+10h],bx
  004360a0  754f                  jne       short 004360F1h
  004360a2  66b81900              mov       ax,19h
  004360a6  668b4e36              mov       cx,[esi+36h]
  004360aa  662b4612              sub       ax,[esi+12h]
  004360ae  83e1fe                and       ecx,0FFFFFFFEh
  004360b1  c6460407              mov       byte [esi+4],7
  004360b5  c64638fa              mov       byte [esi+38h],0FAh
  004360b9  c64639fa              mov       byte [esi+39h],0FAh
  004360bd  c6463afa              mov       byte [esi+3Ah],0FAh
  004360c1  8d0480                lea       eax,[eax+eax*4]
  004360c4  66895e28              mov       [esi+28h],bx
  004360c8  66895e2c              mov       [esi+2Ch],bx
  004360cc  66c746100100          mov       word [esi+10h],1
  004360d2  8d0480                lea       eax,[eax+eax*4]
  004360d5  66895e12              mov       [esi+12h],bx
  004360d9  c1e004                shl       eax,4
  004360dc  83c902                or        ecx,2
  004360df  6689462a              mov       [esi+2Ah],ax
  004360e3  66895e14              mov       [esi+14h],bx
  004360e7  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  004360ed  66894e36              mov       [esi+36h],cx
  004360f1  66837e1001            cmp       word [esi+10h],1
  004360f6  0f859c010000          jne       near 00436298h
  004360fc  6afb                  push      0FFFFFFFBh
  004360fe  6af9                  push      0FFFFFFF9h
  00436100  6af9                  push      0FFFFFFF9h
  00436102  56                    push      esi
  00436103  e8d8ecfcff            call      00404DE0h
  00436108  668b4628              mov       ax,[esi+28h]
  0043610c  8b4e48                mov       ecx,[esi+48h]
  0043610f  0fbfd0                movsx     edx,ax
  00436112  8bf9                  mov       edi,ecx
  00436114  8bd9                  mov       ebx,ecx
  00436116  2bfa                  sub       edi,edx
  00436118  83c410                add       esp,10h
  0043611b  c1ff03                sar       edi,3
  0043611e  8d1407                lea       edx,[edi+eax]
  00436121  668b462c              mov       ax,[esi+2Ch]
  00436125  0fbff8                movsx     edi,ax
  00436128  2bdf                  sub       ebx,edi
  0043612a  66895628              mov       [esi+28h],dx
  0043612e  c1fb03                sar       ebx,3
  00436131  03d8                  add       ebx,eax
  00436133  8a463a                mov       al,[esi+3Ah]
  00436136  66895e2c              mov       [esi+2Ch],bx
  0043613a  33db                  xor       ebx,ebx
  0043613c  3ac3                  cmp       al,bl
  0043613e  7506                  jne       short 00436146h
  00436140  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00436146  66395e16              cmp       [esi+16h],bx
  0043614a  0f8548010000          jne       near 00436298h
  00436150  0fbfc2                movsx     eax,dx
  00436153  81c148f4ffff          add       ecx,0FFFFF448h
  00436159  3bc1                  cmp       eax,ecx
  0043615b  0f8d37010000          jge       near 00436298h
  00436161  895c2418              mov       [esp+18h],ebx
  00436165  bfc0935e00            mov       edi,5E93C0h
  0043616a  c744241004000000      mov       dword [esp+10h],4
  00436172  8b07                  mov       eax,[edi]
  00436174  a900000080            test      eax,80000000h
  00436179  0f84fb000000          je        near 0043627Ah
  0043617f  a900000050            test      eax,50000000h
  00436184  0f85f0000000          jne       near 0043627Ah
  0043618a  8b471c                mov       eax,[edi+1Ch]
  0043618d  8b561c                mov       edx,[esi+1Ch]
  00436190  8b4f24                mov       ecx,[edi+24h]
  00436193  2bc2                  sub       eax,edx
  00436195  8b5624                mov       edx,[esi+24h]
  00436198  c1f80c                sar       eax,0Ch
  0043619b  2bca                  sub       ecx,edx
  0043619d  8bd0                  mov       edx,eax
  0043619f  c1f90c                sar       ecx,0Ch
  004361a2  0fafd0                imul      edx,eax
  004361a5  8bc1                  mov       eax,ecx
  004361a7  0fafc1                imul      eax,ecx
  004361aa  03d0                  add       edx,eax
  004361ac  52                    push      edx
  004361ad  e85ef81000            call      00545A10h
  004361b2  0fbf5628              movsx     edx,word [esi+28h]
  004361b6  8bc8                  mov       ecx,eax
  004361b8  b81f85eb51            mov       eax,51EB851Fh
  004361bd  f7ea                  imul      edx
  004361bf  c1fa04                sar       edx,4
  004361c2  8bc2                  mov       eax,edx
  004361c4  c1e81f                shr       eax,1Fh
  004361c7  03d0                  add       edx,eax
  004361c9  3bca                  cmp       ecx,edx
  004361cb  0f8da9000000          jge       near 0043627Ah
  004361d1  8b4c2418              mov       ecx,[esp+18h]
  004361d5  668b4614              mov       ax,[esi+14h]
  004361d9  ba01000000            mov       edx,1
  004361de  d3e2                  shl       edx,cl
  004361e0  0fbfc8                movsx     ecx,ax
  004361e3  85d1                  test      ecx,edx
  004361e5  0f858f000000          jne       near 0043627Ah
  004361eb  8b4c2418              mov       ecx,[esp+18h]
  004361ef  ba01000000            mov       edx,1
  004361f4  d3e2                  shl       edx,cl
  004361f6  0bd0                  or        edx,eax
  004361f8  8b4644                mov       eax,[esi+44h]
  004361fb  83f80b                cmp       eax,0Bh
  004361fe  66895614              mov       [esi+14h],dx
  00436202  7776                  ja        short 0043627Ah
  00436204  ff248534644300        jmp       dword [eax*4+436434h]
  0043620b  53                    push      ebx
  0043620c  57                    push      edi
  0043620d  55                    push      ebp
  0043620e  e8fd030000            call      00436610h
  00436213  eb62                  jmp       short 00436277h
  00436215  53                    push      ebx
  00436216  57                    push      edi
  00436217  55                    push      ebp
  00436218  e833040000            call      00436650h
  0043621d  eb58                  jmp       short 00436277h
  0043621f  53                    push      ebx
  00436220  57                    push      edi
  00436221  55                    push      ebp
  00436222  e829050000            call      00436750h
  00436227  eb4e                  jmp       short 00436277h
  00436229  53                    push      ebx
  0043622a  57                    push      edi
  0043622b  55                    push      ebp
  0043622c  e83f020000            call      00436470h
  00436231  eb44                  jmp       short 00436277h
  00436233  53                    push      ebx
  00436234  57                    push      edi
  00436235  55                    push      ebp
  00436236  e8d5020000            call      00436510h
  0043623b  eb3a                  jmp       short 00436277h
  0043623d  53                    push      ebx
  0043623e  57                    push      edi
  0043623f  55                    push      ebp
  00436240  e84b030000            call      00436590h
  00436245  eb30                  jmp       short 00436277h
  00436247  53                    push      ebx
  00436248  57                    push      edi
  00436249  55                    push      ebp
  0043624a  e801030000            call      00436550h
  0043624f  eb26                  jmp       short 00436277h
  00436251  53                    push      ebx
  00436252  57                    push      edi
  00436253  55                    push      ebp
  00436254  e877030000            call      004365D0h
  00436259  eb1c                  jmp       short 00436277h
  0043625b  53                    push      ebx
  0043625c  57                    push      edi
  0043625d  55                    push      ebp
  0043625e  e82d040000            call      00436690h
  00436263  eb12                  jmp       short 00436277h
  00436265  53                    push      ebx
  00436266  57                    push      edi
  00436267  55                    push      ebp
  00436268  e863040000            call      004366D0h
  0043626d  eb08                  jmp       short 00436277h
  0043626f  53                    push      ebx
  00436270  57                    push      edi
  00436271  55                    push      ebp
  00436272  e899040000            call      00436710h
  00436277  83c40c                add       esp,0Ch
  0043627a  8b542418              mov       edx,[esp+18h]
  0043627e  8b442410              mov       eax,[esp+10h]
  00436282  42                    inc       edx
  00436283  81c79c000000          add       edi,9Ch
  00436289  48                    dec       eax
  0043628a  89542418              mov       [esp+18h],edx
  0043628e  89442410              mov       [esp+10h],eax
  00436292  0f85dafeffff          jne       near 00436172h
  00436298  8b465c                mov       eax,[esi+5Ch]
  0043629b  bf01000000            mov       edi,1
  004362a0  3bc7                  cmp       eax,edi
  004362a2  0f8586010000          jne       near 0043642Eh
  004362a8  66395e10              cmp       [esi+10h],bx
  004362ac  0f8594000000          jne       near 00436346h
  004362b2  c6460430              mov       byte [esi+4],30h
  004362b6  e8850e1100            call      00547140h
  004362bb  99                    cdq
  004362bc  b9c8000000            mov       ecx,0C8h
  004362c1  f7f9                  idiv      ecx
  004362c3  03d1                  add       edx,ecx
  004362c5  66895628              mov       [esi+28h],dx
  004362c9  e8720e1100            call      00547140h
  004362ce  99                    cdq
  004362cf  b9c8000000            mov       ecx,0C8h
  004362d4  f7f9                  idiv      ecx
  004362d6  03d1                  add       edx,ecx
  004362d8  6689562a              mov       [esi+2Ah],dx
  004362dc  e85f0e1100            call      00547140h
  004362e1  99                    cdq
  004362e2  b9c8000000            mov       ecx,0C8h
  004362e7  f7f9                  idiv      ecx
  004362e9  03d1                  add       edx,ecx
  004362eb  6689562c              mov       [esi+2Ch],dx
  004362ef  e84c0e1100            call      00547140h
  004362f4  99                    cdq
  004362f5  33c2                  xor       eax,edx
  004362f7  2bc2                  sub       eax,edx
  004362f9  25ff0f0000            and       eax,0FFFh
  004362fe  33c2                  xor       eax,edx
  00436300  2bc2                  sub       eax,edx
  00436302  66894632              mov       [esi+32h],ax
  00436306  e8350e1100            call      00547140h
  0043630b  99                    cdq
  0043630c  b9f4010000            mov       ecx,1F4h
  00436311  885e38                mov       [esi+38h],bl
  00436314  f7f9                  idiv      ecx
  00436316  885e39                mov       [esi+39h],bl
  00436319  885e3a                mov       [esi+3Ah],bl
  0043631c  66897e10              mov       [esi+10h],di
  00436320  66895e12              mov       [esi+12h],bx
  00436324  66895630              mov       [esi+30h],dx
  00436328  e8130e1100            call      00547140h
  0043632d  99                    cdq
  0043632e  b9c8000000            mov       ecx,0C8h
  00436333  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  00436339  f7f9                  idiv      ecx
  0043633b  8b4620                mov       eax,[esi+20h]
  0043633e  c1e20c                shl       edx,0Ch
  00436341  03c2                  add       eax,edx
  00436343  894620                mov       [esi+20h],eax
  00436346  66837e1001            cmp       word [esi+10h],1
  0043634b  b96affffff            mov       ecx,0FFFFFF6Ah
  00436350  bf64000000            mov       edi,64h
  00436355  7570                  jne       short 004363C7h
  00436357  8a4638                mov       al,[esi+38h]
  0043635a  bafa000000            mov       edx,0FAh
  0043635f  8be8                  mov       ebp,eax
  00436361  66014e32              add       [esi+32h],cx
  00436365  81e5ff000000          and       ebp,0FFh
  0043636b  2bd5                  sub       edx,ebp
  0043636d  d1fa                  sar       edx,1
  0043636f  02d0                  add       dl,al
  00436371  8a4639                mov       al,[esi+39h]
  00436374  8be8                  mov       ebp,eax
  00436376  885638                mov       [esi+38h],dl
  00436379  81e5ff000000          and       ebp,0FFh
  0043637f  bafa000000            mov       edx,0FAh
  00436384  2bd5                  sub       edx,ebp
  00436386  d1fa                  sar       edx,1
  00436388  02d0                  add       dl,al
  0043638a  8a463a                mov       al,[esi+3Ah]
  0043638d  8be8                  mov       ebp,eax
  0043638f  885639                mov       [esi+39h],dl
  00436392  81e5ff000000          and       ebp,0FFh
  00436398  bafa000000            mov       edx,0FAh
  0043639d  2bd5                  sub       edx,ebp
  0043639f  d1fa                  sar       edx,1
  004363a1  02d0                  add       dl,al
  004363a3  66ff4612              inc       word [esi+12h]
  004363a7  66837e1205            cmp       word [esi+12h],5
  004363ac  88563a                mov       [esi+3Ah],dl
  004363af  7e0a                  jle       short 004363BBh
  004363b1  66c746100200          mov       word [esi+10h],2
  004363b7  66895e12              mov       [esi+12h],bx
  004363bb  66017e28              add       [esi+28h],di
  004363bf  66017e2a              add       [esi+2Ah],di
  004363c3  66017e2c              add       [esi+2Ch],di
  004363c7  66837e1002            cmp       word [esi+10h],2
  004363cc  7560                  jne       short 0043642Eh
  004363ce  8a4638                mov       al,[esi+38h]
  004363d1  66014e32              add       [esi+32h],cx
  004363d5  8bd0                  mov       edx,eax
  004363d7  81e2ff000000          and       edx,0FFh
  004363dd  f7da                  neg       edx
  004363df  c1fa03                sar       edx,3
  004363e2  02d0                  add       dl,al
  004363e4  8a4639                mov       al,[esi+39h]
  004363e7  885638                mov       [esi+38h],dl
  004363ea  8bd0                  mov       edx,eax
  004363ec  81e2ff000000          and       edx,0FFh
  004363f2  f7da                  neg       edx
  004363f4  c1fa03                sar       edx,3
  004363f7  02d0                  add       dl,al
  004363f9  8a463a                mov       al,[esi+3Ah]
  004363fc  885639                mov       [esi+39h],dl
  004363ff  8bd0                  mov       edx,eax
  00436401  81e2ff000000          and       edx,0FFh
  00436407  f7da                  neg       edx
  00436409  c1fa03                sar       edx,3
  0043640c  02d0                  add       dl,al
  0043640e  66ff4612              inc       word [esi+12h]
  00436412  66837e1214            cmp       word [esi+12h],14h
  00436417  88563a                mov       [esi+3Ah],dl
  0043641a  7e06                  jle       short 00436422h
  0043641c  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00436422  66017e28              add       [esi+28h],di
  00436426  66017e2a              add       [esi+2Ah],di
  0043642a  66017e2c              add       [esi+2Ch],di
  0043642e  5f                    pop       edi
  0043642f  5e                    pop       esi
  00436430  5d                    pop       ebp
  00436431  5b                    pop       ebx
  00436432  59                    pop       ecx
  00436433  c3                    ret
