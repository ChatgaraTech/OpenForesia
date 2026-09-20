  0046ede0  83ec14                sub       esp,14h
  0046ede3  53                    push      ebx
  0046ede4  55                    push      ebp
  0046ede5  56                    push      esi
  0046ede6  8b742424              mov       esi,[esp+24h]
  0046edea  57                    push      edi
  0046edeb  668b4610              mov       ax,[esi+10h]
  0046edef  663d6300              cmp       ax,63h
  0046edf3  7520                  jne       short 0046EE15h
  0046edf5  c7461c00c0f9ff        mov       dword [esi+1Ch],0FFF9C000h
  0046edfc  c74620000001ff        mov       dword [esi+20h],0FF010000h
  0046ee03  c7462400f078ff        mov       dword [esi+24h],0FF78F000h
  0046ee0a  66c746120000          mov       word [esi+12h],0
  0046ee10  e905020000            jmp       0046F01Ah
  0046ee15  663d0200              cmp       ax,2
  0046ee19  751e                  jne       short 0046EE39h
  0046ee1b  0fbf4632              movsx     eax,word [esi+32h]
  0046ee1f  50                    push      eax
  0046ee20  e8f76b0d00            call      00545A1Ch
  0046ee25  8a4e12                mov       cl,[esi+12h]
  0046ee28  d3e0                  shl       eax,cl
  0046ee2a  b900c0f9ff            mov       ecx,0FFF9C000h
  0046ee2f  2bc8                  sub       ecx,eax
  0046ee31  8b4620                mov       eax,[esi+20h]
  0046ee34  03c1                  add       eax,ecx
  0046ee36  894620                mov       [esi+20h],eax
  0046ee39  33c0                  xor       eax,eax
  0046ee3b  66837e1001            cmp       word [esi+10h],1
  0046ee40  7513                  jne       short 0046EE55h
  0046ee42  66c746100200          mov       word [esi+10h],2
  0046ee48  66c746140500          mov       word [esi+14h],5
  0046ee4e  66894616              mov       [esi+16h],ax
  0046ee52  89465c                mov       [esi+5Ch],eax
  0046ee55  8bae80000000          mov       ebp,[esi+80h]
  0046ee5b  8b4664                mov       eax,[esi+64h]
  0046ee5e  896c2414              mov       [esp+14h],ebp
  0046ee62  8bae84000000          mov       ebp,[esi+84h]
  0046ee68  8b5660                mov       edx,[esi+60h]
  0046ee6b  896c2418              mov       [esp+18h],ebp
  0046ee6f  8bae88000000          mov       ebp,[esi+88h]
  0046ee75  8b4e24                mov       ecx,[esi+24h]
  0046ee78  896c241c              mov       [esp+1Ch],ebp
  0046ee7c  8bae8c000000          mov       ebp,[esi+8Ch]
  0046ee82  8b5e7c                mov       ebx,[esi+7Ch]
  0046ee85  668bbe90000000        mov       di,[esi+90h]
  0046ee8c  89442410              mov       [esp+10h],eax
  0046ee90  8b461c                mov       eax,[esi+1Ch]
  0046ee93  896c2420              mov       [esp+20h],ebp
  0046ee97  8be8                  mov       ebp,eax
  0046ee99  89542428              mov       [esp+28h],edx
  0046ee9d  8b5620                mov       edx,[esi+20h]
  0046eea0  81e500f0ffff          and       ebp,0FFFFF000h
  0046eea6  894644                mov       [esi+44h],eax
  0046eea9  81fd0070f9ff          cmp       ebp,0FFF97000h
  0046eeaf  895648                mov       [esi+48h],edx
  0046eeb2  894e4c                mov       [esi+4Ch],ecx
  0046eeb5  7d08                  jge       short 0046EEBFh
  0046eeb7  0500300000            add       eax,3000h
  0046eebc  89461c                mov       [esi+1Ch],eax
  0046eebf  8b461c                mov       eax,[esi+1Ch]
  0046eec2  8be8                  mov       ebp,eax
  0046eec4  81e500f0ffff          and       ebp,0FFFFF000h
  0046eeca  81fd0010faff          cmp       ebp,0FFFA1000h
  0046eed0  7e08                  jle       short 0046EEDAh
  0046eed2  0500d0ffff            add       eax,0FFFFD000h
  0046eed7  89461c                mov       [esi+1Ch],eax
  0046eeda  668b4614              mov       ax,[esi+14h]
  0046eede  0fbfe8                movsx     ebp,ax
  0046eee1  c1e50c                shl       ebp,0Ch
  0046eee4  03e9                  add       ebp,ecx
  0046eee6  66ff4616              inc       word [esi+16h]
  0046eeea  668b4e16              mov       cx,[esi+16h]
  0046eeee  896e24                mov       [esi+24h],ebp
  0046eef1  6683f903              cmp       cx,3
  0046eef5  bd00010000            mov       ebp,100h
  0046eefa  7e1f                  jle       short 0046EF1Bh
  0046eefc  85ae90000000          test      [esi+90h],ebp
  0046ef02  7417                  je        short 0046EF1Bh
  0046ef04  40                    inc       eax
  0046ef05  66c746160000          mov       word [esi+16h],0
  0046ef0b  663d2800              cmp       ax,28h
  0046ef0f  66894614              mov       [esi+14h],ax
  0046ef13  7e06                  jle       short 0046EF1Bh
  0046ef15  66c746142800          mov       word [esi+14h],28h
  0046ef1b  c1fa0c                sar       edx,0Ch
  0046ef1e  56                    push      esi
  0046ef1f  6689969a000000        mov       [esi+9Ah],dx
  0046ef26  e8154a0c00            call      00533940h
  0046ef2b  8b8690000000          mov       eax,[esi+90h]
  0046ef31  83c404                add       esp,4
  0046ef34  f6c488                test      ah,88h
  0046ef37  7461                  je        short 0046EF9Ah
  0046ef39  8b4c2428              mov       ecx,[esp+28h]
  0046ef3d  8b4644                mov       eax,[esi+44h]
  0046ef40  8b542410              mov       edx,[esp+10h]
  0046ef44  894e60                mov       [esi+60h],ecx
  0046ef47  8b4e4c                mov       ecx,[esi+4Ch]
  0046ef4a  89461c                mov       [esi+1Ch],eax
  0046ef4d  8b442418              mov       eax,[esp+18h]
  0046ef51  894e24                mov       [esi+24h],ecx
  0046ef54  8b4c241c              mov       ecx,[esp+1Ch]
  0046ef58  895664                mov       [esi+64h],edx
  0046ef5b  8b542414              mov       edx,[esp+14h]
  0046ef5f  898684000000          mov       [esi+84h],eax
  0046ef65  8b4650                mov       eax,[esi+50h]
  0046ef68  898e88000000          mov       [esi+88h],ecx
  0046ef6e  8b4e58                mov       ecx,[esi+58h]
  0046ef71  81e7ffff0000          and       edi,0FFFFh
  0046ef77  899680000000          mov       [esi+80h],edx
  0046ef7d  8b542420              mov       edx,[esp+20h]
  0046ef81  f7d8                  neg       eax
  0046ef83  f7d9                  neg       ecx
  0046ef85  895e7c                mov       [esi+7Ch],ebx
  0046ef88  89be90000000          mov       [esi+90h],edi
  0046ef8e  89968c000000          mov       [esi+8Ch],edx
  0046ef94  894650                mov       [esi+50h],eax
  0046ef97  894e58                mov       [esi+58h],ecx
  0046ef9a  85ae90000000          test      [esi+90h],ebp
  0046efa0  7511                  jne       short 0046EFB3h
  0046efa2  8b465c                mov       eax,[esi+5Ch]
  0046efa5  85c0                  test      eax,eax
  0046efa7  750a                  jne       short 0046EFB3h
  0046efa9  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0046efb0  89565c                mov       [esi+5Ch],edx
  0046efb3  8b06                  mov       eax,[esi]
  0046efb5  a900000020            test      eax,20000000h
  0046efba  7407                  je        short 0046EFC3h
  0046efbc  25ffffff7f            and       eax,7FFFFFFFh
  0046efc1  8906                  mov       [esi],eax
  0046efc3  8b4e60                mov       ecx,[esi+60h]
  0046efc6  8b4620                mov       eax,[esi+20h]
  0046efc9  3bc1                  cmp       eax,ecx
  0046efcb  7e03                  jle       short 0046EFD0h
  0046efcd  894e20                mov       [esi+20h],ecx
  0046efd0  8b465c                mov       eax,[esi+5Ch]
  0046efd3  33d2                  xor       edx,edx
  0046efd5  3bc2                  cmp       eax,edx
  0046efd7  7441                  je        short 0046F01Ah
  0046efd9  8bf8                  mov       edi,eax
  0046efdb  83c0f3                add       eax,0FFFFFFF3h
  0046efde  c1e70c                shl       edi,0Ch
  0046efe1  3df0d8ffff            cmp       eax,0FFFFD8F0h
  0046efe6  897e20                mov       [esi+20h],edi
  0046efe9  89465c                mov       [esi+5Ch],eax
  0046efec  7d07                  jge       short 0046EFF5h
  0046efee  c7465cf0d8ffff        mov       dword [esi+5Ch],0FFFFD8F0h
  0046eff5  394e20                cmp       [esi+20h],ecx
  0046eff8  7d03                  jge       short 0046EFFDh
  0046effa  894e20                mov       [esi+20h],ecx
  0046effd  66ff4616              inc       word [esi+16h]
  0046f001  66837e1601            cmp       word [esi+16h],1
  0046f006  7e12                  jle       short 0046F01Ah
  0046f008  66ff4e14              dec       word [esi+14h]
  0046f00c  66395614              cmp       [esi+14h],dx
  0046f010  66895616              mov       [esi+16h],dx
  0046f014  7d04                  jge       short 0046F01Ah
  0046f016  66895614              mov       [esi+14h],dx
  0046f01a  6683463244            add       word [esi+32h],44h
  0046f01f  668b4632              mov       ax,[esi+32h]
  0046f023  663d0010              cmp       ax,1000h
  0046f027  7c09                  jl        short 0046F032h
  0046f029  0500f0ffff            add       eax,0FFFFF000h
  0046f02e  66894632              mov       [esi+32h],ax
  0046f032  0fbf4632              movsx     eax,word [esi+32h]
  0046f036  50                    push      eax
  0046f037  e8e0690d00            call      00545A1Ch
  0046f03c  8a4e12                mov       cl,[esi+12h]
  0046f03f  d3e0                  shl       eax,cl
  0046f041  8b4e20                mov       ecx,[esi+20h]
  0046f044  0500400600            add       eax,64000h
  0046f049  03c8                  add       ecx,eax
  0046f04b  66837e1063            cmp       word [esi+10h],63h
  0046f050  894e20                mov       [esi+20h],ecx
  0046f053  7409                  je        short 0046F05Eh
  0046f055  56                    push      esi
  0046f056  e8755c0c00            call      00534CD0h
  0046f05b  83c404                add       esp,4
  0046f05e  5f                    pop       edi
  0046f05f  5e                    pop       esi
  0046f060  5d                    pop       ebp
  0046f061  5b                    pop       ebx
  0046f062  83c414                add       esp,14h
  0046f065  c3                    ret
