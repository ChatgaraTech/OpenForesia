Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040cea0  section .text  offset fichier 0xc2a0  972 octets

  0040cea0  83ec08                sub       esp,8
  0040cea3  53                    push      ebx
  0040cea4  55                    push      ebp
  0040cea5  56                    push      esi
  0040cea6  8b742418              mov       esi,[esp+18h]
  0040ceaa  33db                  xor       ebx,ebx
  0040ceac  57                    push      edi
  0040cead  8b466c                mov       eax,[esi+6Ch]
  0040ceb0  3bc3                  cmp       eax,ebx
  0040ceb2  89442414              mov       [esp+14h],eax
  0040ceb6  7405                  je        short 0040CEBDh
  0040ceb8  8b686c                mov       ebp,[eax+6Ch]
  0040cebb  eb04                  jmp       short 0040CEC1h

LAB_0040cebd:
  0040cebd  8b6c241c              mov       ebp,[esp+1Ch]

LAB_0040cec1:
  0040cec1  395e5c                cmp       [esi+5Ch],ebx
  0040cec4  0f8504020000          jne       near 0040D0CEh
  0040ceca  66395e10              cmp       [esi+10h],bx
  0040cece  754f                  jne       short 0040CF1Fh
  0040ced0  66b81900              mov       ax,19h
  0040ced4  668b4e36              mov       cx,[esi+36h]
  0040ced8  662b4612              sub       ax,[esi+12h]
  0040cedc  83e1fe                and       ecx,0FFFFFFFEh
  0040cedf  c6460407              mov       byte [esi+4],7
  0040cee3  c64638fa              mov       byte [esi+38h],0FAh
  0040cee7  c64639fa              mov       byte [esi+39h],0FAh
  0040ceeb  c6463afa              mov       byte [esi+3Ah],0FAh
  0040ceef  8d0480                lea       eax,[eax+eax*4]
  0040cef2  66895e28              mov       [esi+28h],bx
  0040cef6  66895e2c              mov       [esi+2Ch],bx
  0040cefa  66c746100100          mov       word [esi+10h],1
  0040cf00  8d0480                lea       eax,[eax+eax*4]
  0040cf03  66895e12              mov       [esi+12h],bx
  0040cf07  c1e004                shl       eax,4
  0040cf0a  83c902                or        ecx,2
  0040cf0d  6689462a              mov       [esi+2Ah],ax
  0040cf11  66895e14              mov       [esi+14h],bx
  0040cf15  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0040cf1b  66894e36              mov       [esi+36h],cx

LAB_0040cf1f:
  0040cf1f  66837e1001            cmp       word [esi+10h],1
  0040cf24  0f85a4010000          jne       near 0040D0CEh
  0040cf2a  6afb                  push      0FFFFFFFBh
  0040cf2c  6af9                  push      0FFFFFFF9h
  0040cf2e  6af9                  push      0FFFFFFF9h
  0040cf30  56                    push      esi
  0040cf31  e8aa7effff            call      00404DE0h
  0040cf36  668b4628              mov       ax,[esi+28h]
  0040cf3a  8b4e48                mov       ecx,[esi+48h]
  0040cf3d  0fbfd0                movsx     edx,ax
  0040cf40  8bf9                  mov       edi,ecx
  0040cf42  8bd9                  mov       ebx,ecx
  0040cf44  2bfa                  sub       edi,edx
  0040cf46  83c410                add       esp,10h
  0040cf49  c1ff03                sar       edi,3
  0040cf4c  8d1407                lea       edx,[edi+eax]
  0040cf4f  668b462c              mov       ax,[esi+2Ch]
  0040cf53  0fbff8                movsx     edi,ax
  0040cf56  2bdf                  sub       ebx,edi
  0040cf58  66895628              mov       [esi+28h],dx
  0040cf5c  c1fb03                sar       ebx,3
  0040cf5f  03d8                  add       ebx,eax
  0040cf61  8a463a                mov       al,[esi+3Ah]
  0040cf64  66895e2c              mov       [esi+2Ch],bx
  0040cf68  33db                  xor       ebx,ebx
  0040cf6a  3ac3                  cmp       al,bl
  0040cf6c  7506                  jne       short 0040CF74h
  0040cf6e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040cf74:
  0040cf74  66395e16              cmp       [esi+16h],bx
  0040cf78  0f8550010000          jne       near 0040D0CEh
  0040cf7e  0fbfc2                movsx     eax,dx
  0040cf81  81c148f4ffff          add       ecx,0FFFFF448h
  0040cf87  3bc1                  cmp       eax,ecx
  0040cf89  0f8d3f010000          jge       near 0040D0CEh
  0040cf8f  895c241c              mov       [esp+1Ch],ebx
  0040cf93  bfc0935e00            mov       edi,5E93C0h
  0040cf98  c744241004000000      mov       dword [esp+10h],4

LAB_0040cfa0:
  0040cfa0  8b07                  mov       eax,[edi]
  0040cfa2  a900000080            test      eax,80000000h
  0040cfa7  0f8403010000          je        near 0040D0B0h
  0040cfad  a900000050            test      eax,50000000h
  0040cfb2  0f85f8000000          jne       near 0040D0B0h
  0040cfb8  8b471c                mov       eax,[edi+1Ch]
  0040cfbb  8b561c                mov       edx,[esi+1Ch]
  0040cfbe  8b4f24                mov       ecx,[edi+24h]
  0040cfc1  2bc2                  sub       eax,edx
  0040cfc3  8b5624                mov       edx,[esi+24h]
  0040cfc6  c1f80c                sar       eax,0Ch
  0040cfc9  2bca                  sub       ecx,edx
  0040cfcb  8bd0                  mov       edx,eax
  0040cfcd  c1f90c                sar       ecx,0Ch
  0040cfd0  0fafd0                imul      edx,eax
  0040cfd3  8bc1                  mov       eax,ecx
  0040cfd5  0fafc1                imul      eax,ecx
  0040cfd8  03d0                  add       edx,eax
  0040cfda  52                    push      edx
  0040cfdb  e8308a1300            call      00545A10h
  0040cfe0  0fbf5628              movsx     edx,word [esi+28h]
  0040cfe4  8bc8                  mov       ecx,eax
  0040cfe6  b81f85eb51            mov       eax,51EB851Fh
  0040cfeb  f7ea                  imul      edx
  0040cfed  c1fa04                sar       edx,4
  0040cff0  8bc2                  mov       eax,edx
  0040cff2  c1e81f                shr       eax,1Fh
  0040cff5  03d0                  add       edx,eax
  0040cff7  3bca                  cmp       ecx,edx
  0040cff9  0f8db1000000          jge       near 0040D0B0h
  0040cfff  8b4c241c              mov       ecx,[esp+1Ch]
  0040d003  668b4614              mov       ax,[esi+14h]
  0040d007  ba01000000            mov       edx,1
  0040d00c  d3e2                  shl       edx,cl
  0040d00e  0fbfc8                movsx     ecx,ax
  0040d011  85d1                  test      ecx,edx
  0040d013  0f8597000000          jne       near 0040D0B0h
  0040d019  8b4c241c              mov       ecx,[esp+1Ch]
  0040d01d  ba01000000            mov       edx,1
  0040d022  d3e2                  shl       edx,cl
  0040d024  0bd0                  or        edx,eax
  0040d026  8b442414              mov       eax,[esp+14h]
  0040d02a  3bc3                  cmp       eax,ebx
  0040d02c  66895614              mov       [esi+14h],dx
  0040d030  747e                  je        short 0040D0B0h
  0040d032  8b4644                mov       eax,[esi+44h]
  0040d035  83f80b                cmp       eax,0Bh
  0040d038  7776                  ja        short 0040D0B0h
  0040d03a  ff24856cd24000        jmp       dword [eax*4+40D26Ch]
  0040d041  53                    push      ebx
  0040d042  57                    push      edi
  0040d043  55                    push      ebp
  0040d044  e897030000            call      0040D3E0h
  0040d049  eb62                  jmp       short 0040D0ADh
  0040d04b  53                    push      ebx
  0040d04c  57                    push      edi
  0040d04d  55                    push      ebp
  0040d04e  e8cd030000            call      0040D420h
  0040d053  eb58                  jmp       short 0040D0ADh
  0040d055  53                    push      ebx
  0040d056  57                    push      edi
  0040d057  55                    push      ebp
  0040d058  e8c3040000            call      0040D520h
  0040d05d  eb4e                  jmp       short 0040D0ADh
  0040d05f  53                    push      ebx
  0040d060  57                    push      edi
  0040d061  55                    push      ebp
  0040d062  e839020000            call      0040D2A0h
  0040d067  eb44                  jmp       short 0040D0ADh
  0040d069  53                    push      ebx
  0040d06a  57                    push      edi
  0040d06b  55                    push      ebp
  0040d06c  e86f020000            call      0040D2E0h
  0040d071  eb3a                  jmp       short 0040D0ADh
  0040d073  53                    push      ebx
  0040d074  57                    push      edi
  0040d075  55                    push      ebp
  0040d076  e8e5020000            call      0040D360h
  0040d07b  eb30                  jmp       short 0040D0ADh
  0040d07d  53                    push      ebx
  0040d07e  57                    push      edi
  0040d07f  55                    push      ebp
  0040d080  e89b020000            call      0040D320h
  0040d085  eb26                  jmp       short 0040D0ADh
  0040d087  53                    push      ebx
  0040d088  57                    push      edi
  0040d089  55                    push      ebp
  0040d08a  e811030000            call      0040D3A0h
  0040d08f  eb1c                  jmp       short 0040D0ADh
  0040d091  53                    push      ebx
  0040d092  57                    push      edi
  0040d093  55                    push      ebp
  0040d094  e8c7030000            call      0040D460h
  0040d099  eb12                  jmp       short 0040D0ADh
  0040d09b  53                    push      ebx
  0040d09c  57                    push      edi
  0040d09d  55                    push      ebp
  0040d09e  e8fd030000            call      0040D4A0h
  0040d0a3  eb08                  jmp       short 0040D0ADh
  0040d0a5  53                    push      ebx
  0040d0a6  57                    push      edi
  0040d0a7  55                    push      ebp
  0040d0a8  e833040000            call      0040D4E0h

LAB_0040d0ad:
  0040d0ad  83c40c                add       esp,0Ch

LAB_0040d0b0:
  0040d0b0  8b54241c              mov       edx,[esp+1Ch]
  0040d0b4  8b442410              mov       eax,[esp+10h]
  0040d0b8  42                    inc       edx
  0040d0b9  81c79c000000          add       edi,9Ch
  0040d0bf  48                    dec       eax
  0040d0c0  8954241c              mov       [esp+1Ch],edx
  0040d0c4  89442410              mov       [esp+10h],eax
  0040d0c8  0f85d2feffff          jne       near 0040CFA0h

LAB_0040d0ce:
  0040d0ce  8b465c                mov       eax,[esi+5Ch]
  0040d0d1  bf01000000            mov       edi,1
  0040d0d6  3bc7                  cmp       eax,edi
  0040d0d8  0f8586010000          jne       near 0040D264h
  0040d0de  66395e10              cmp       [esi+10h],bx
  0040d0e2  0f8594000000          jne       near 0040D17Ch
  0040d0e8  c6460430              mov       byte [esi+4],30h
  0040d0ec  e84fa01300            call      00547140h
  0040d0f1  99                    cdq
  0040d0f2  b9c8000000            mov       ecx,0C8h
  0040d0f7  f7f9                  idiv      ecx
  0040d0f9  03d1                  add       edx,ecx
  0040d0fb  66895628              mov       [esi+28h],dx
  0040d0ff  e83ca01300            call      00547140h
  0040d104  99                    cdq
  0040d105  b9c8000000            mov       ecx,0C8h
  0040d10a  f7f9                  idiv      ecx
  0040d10c  03d1                  add       edx,ecx
  0040d10e  6689562a              mov       [esi+2Ah],dx
  0040d112  e829a01300            call      00547140h
  0040d117  99                    cdq
  0040d118  b9c8000000            mov       ecx,0C8h
  0040d11d  f7f9                  idiv      ecx
  0040d11f  03d1                  add       edx,ecx
  0040d121  6689562c              mov       [esi+2Ch],dx
  0040d125  e816a01300            call      00547140h
  0040d12a  99                    cdq
  0040d12b  33c2                  xor       eax,edx
  0040d12d  2bc2                  sub       eax,edx
  0040d12f  25ff0f0000            and       eax,0FFFh
  0040d134  33c2                  xor       eax,edx
  0040d136  2bc2                  sub       eax,edx
  0040d138  66894632              mov       [esi+32h],ax
  0040d13c  e8ff9f1300            call      00547140h
  0040d141  99                    cdq
  0040d142  b9f4010000            mov       ecx,1F4h
  0040d147  885e38                mov       [esi+38h],bl
  0040d14a  f7f9                  idiv      ecx
  0040d14c  885e39                mov       [esi+39h],bl
  0040d14f  885e3a                mov       [esi+3Ah],bl
  0040d152  66897e10              mov       [esi+10h],di
  0040d156  66895e12              mov       [esi+12h],bx
  0040d15a  66895630              mov       [esi+30h],dx
  0040d15e  e8dd9f1300            call      00547140h
  0040d163  99                    cdq
  0040d164  b9c8000000            mov       ecx,0C8h
  0040d169  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0040d16f  f7f9                  idiv      ecx
  0040d171  8b4620                mov       eax,[esi+20h]
  0040d174  c1e20c                shl       edx,0Ch
  0040d177  03c2                  add       eax,edx
  0040d179  894620                mov       [esi+20h],eax

LAB_0040d17c:
  0040d17c  66837e1001            cmp       word [esi+10h],1
  0040d181  b96affffff            mov       ecx,0FFFFFF6Ah
  0040d186  bf64000000            mov       edi,64h
  0040d18b  7570                  jne       short 0040D1FDh
  0040d18d  8a4638                mov       al,[esi+38h]
  0040d190  bafa000000            mov       edx,0FAh
  0040d195  8be8                  mov       ebp,eax
  0040d197  66014e32              add       [esi+32h],cx
  0040d19b  81e5ff000000          and       ebp,0FFh
  0040d1a1  2bd5                  sub       edx,ebp
  0040d1a3  d1fa                  sar       edx,1
  0040d1a5  02d0                  add       dl,al
  0040d1a7  8a4639                mov       al,[esi+39h]
  0040d1aa  8be8                  mov       ebp,eax
  0040d1ac  885638                mov       [esi+38h],dl
  0040d1af  81e5ff000000          and       ebp,0FFh
  0040d1b5  bafa000000            mov       edx,0FAh
  0040d1ba  2bd5                  sub       edx,ebp
  0040d1bc  d1fa                  sar       edx,1
  0040d1be  02d0                  add       dl,al
  0040d1c0  8a463a                mov       al,[esi+3Ah]
  0040d1c3  8be8                  mov       ebp,eax
  0040d1c5  885639                mov       [esi+39h],dl
  0040d1c8  81e5ff000000          and       ebp,0FFh
  0040d1ce  bafa000000            mov       edx,0FAh
  0040d1d3  2bd5                  sub       edx,ebp
  0040d1d5  d1fa                  sar       edx,1
  0040d1d7  02d0                  add       dl,al
  0040d1d9  66ff4612              inc       word [esi+12h]
  0040d1dd  66837e1205            cmp       word [esi+12h],5
  0040d1e2  88563a                mov       [esi+3Ah],dl
  0040d1e5  7e0a                  jle       short 0040D1F1h
  0040d1e7  66c746100200          mov       word [esi+10h],2
  0040d1ed  66895e12              mov       [esi+12h],bx

LAB_0040d1f1:
  0040d1f1  66017e28              add       [esi+28h],di
  0040d1f5  66017e2a              add       [esi+2Ah],di
  0040d1f9  66017e2c              add       [esi+2Ch],di

LAB_0040d1fd:
  0040d1fd  66837e1002            cmp       word [esi+10h],2
  0040d202  7560                  jne       short 0040D264h
  0040d204  8a4638                mov       al,[esi+38h]
  0040d207  66014e32              add       [esi+32h],cx
  0040d20b  8bd0                  mov       edx,eax
  0040d20d  81e2ff000000          and       edx,0FFh
  0040d213  f7da                  neg       edx
  0040d215  c1fa03                sar       edx,3
  0040d218  02d0                  add       dl,al
  0040d21a  8a4639                mov       al,[esi+39h]
  0040d21d  885638                mov       [esi+38h],dl
  0040d220  8bd0                  mov       edx,eax
  0040d222  81e2ff000000          and       edx,0FFh
  0040d228  f7da                  neg       edx
  0040d22a  c1fa03                sar       edx,3
  0040d22d  02d0                  add       dl,al
  0040d22f  8a463a                mov       al,[esi+3Ah]
  0040d232  885639                mov       [esi+39h],dl
  0040d235  8bd0                  mov       edx,eax
  0040d237  81e2ff000000          and       edx,0FFh
  0040d23d  f7da                  neg       edx
  0040d23f  c1fa03                sar       edx,3
  0040d242  02d0                  add       dl,al
  0040d244  66ff4612              inc       word [esi+12h]
  0040d248  66837e1214            cmp       word [esi+12h],14h
  0040d24d  88563a                mov       [esi+3Ah],dl
  0040d250  7e06                  jle       short 0040D258h
  0040d252  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040d258:
  0040d258  66017e28              add       [esi+28h],di
  0040d25c  66017e2a              add       [esi+2Ah],di
  0040d260  66017e2c              add       [esi+2Ch],di

LAB_0040d264:
  0040d264  5f                    pop       edi
  0040d265  5e                    pop       esi
  0040d266  5d                    pop       ebp
  0040d267  5b                    pop       ebx
  0040d268  83c408                add       esp,8
  0040d26b  c3                    ret

13 cible(s) de saut interne : 0x0040cebd, 0x0040cec1, 0x0040cf1f, 0x0040cf74, 0x0040cfa0, 0x0040d0ad, 0x0040d0b0, 0x0040d0ce, 0x0040d17c, 0x0040d1f1, 0x0040d1fd, 0x0040d258, 0x0040d264
