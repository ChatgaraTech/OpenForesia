  0046ad70  83ec14                sub       esp,14h
  0046ad73  53                    push      ebx
  0046ad74  55                    push      ebp
  0046ad75  56                    push      esi
  0046ad76  8b742424              mov       esi,[esp+24h]
  0046ad7a  57                    push      edi
  0046ad7b  668b4e10              mov       cx,[esi+10h]
  0046ad7f  6683f906              cmp       cx,6
  0046ad83  744b                  je        short 0046ADD0h
  0046ad85  6683f907              cmp       cx,7
  0046ad89  7445                  je        short 0046ADD0h
  0046ad8b  668b4628              mov       ax,[esi+28h]
  0046ad8f  bf00100000            mov       edi,1000h
  0046ad94  0fbfd0                movsx     edx,ax
  0046ad97  2bfa                  sub       edi,edx
  0046ad99  c1ff03                sar       edi,3
  0046ad9c  03f8                  add       edi,eax
  0046ad9e  668b462a              mov       ax,[esi+2Ah]
  0046ada2  0fbfd0                movsx     edx,ax
  0046ada5  66897e28              mov       [esi+28h],di
  0046ada9  bf00100000            mov       edi,1000h
  0046adae  2bfa                  sub       edi,edx
  0046adb0  c1ff03                sar       edi,3
  0046adb3  03f8                  add       edi,eax
  0046adb5  668b462c              mov       ax,[esi+2Ch]
  0046adb9  0fbfd0                movsx     edx,ax
  0046adbc  66897e2a              mov       [esi+2Ah],di
  0046adc0  bf00100000            mov       edi,1000h
  0046adc5  2bfa                  sub       edi,edx
  0046adc7  c1ff03                sar       edi,3
  0046adca  03f8                  add       edi,eax
  0046adcc  66897e2c              mov       [esi+2Ch],di
  0046add0  33c0                  xor       eax,eax
  0046add2  663bc8                cmp       cx,ax
  0046add5  0f8519010000          jne       near 0046AEF4h
  0046addb  668b4e30              mov       cx,[esi+30h]
  0046addf  8b5e1c                mov       ebx,[esi+1Ch]
  0046ade2  8b6e20                mov       ebp,[esi+20h]
  0046ade5  66894612              mov       [esi+12h],ax
  0046ade9  89465c                mov       [esi+5Ch],eax
  0046adec  66898e9a000000        mov       [esi+9Ah],cx
  0046adf3  66894630              mov       [esi+30h],ax
  0046adf7  8b4624                mov       eax,[esi+24h]
  0046adfa  8bd3                  mov       edx,ebx
  0046adfc  8bcd                  mov       ecx,ebp
  0046adfe  89442428              mov       [esp+28h],eax
  0046ae02  81e200f0ffff          and       edx,0FFFFF000h
  0046ae08  81e100f0ffff          and       ecx,0FFFFF000h
  0046ae0e  2500f0ffff            and       eax,0FFFFF000h
  0046ae13  c1e204                shl       edx,4
  0046ae16  c1e104                shl       ecx,4
  0046ae19  c1e004                shl       eax,4
  0046ae1c  895644                mov       [esi+44h],edx
  0046ae1f  894e48                mov       [esi+48h],ecx
  0046ae22  89464c                mov       [esi+4Ch],eax
  0046ae25  0fbf5616              movsx     edx,word [esi+16h]
  0046ae29  8b465c                mov       eax,[esi+5Ch]
  0046ae2c  3bc2                  cmp       eax,edx
  0046ae2e  7d0e                  jge       short 0046AE3Eh
  0046ae30  668b4e12              mov       cx,[esi+12h]
  0046ae34  663b4e14              cmp       cx,[esi+14h]
  0046ae38  7d04                  jge       short 0046AE3Eh
  0046ae3a  40                    inc       eax
  0046ae3b  89465c                mov       [esi+5Ch],eax
  0046ae3e  0fbf5632              movsx     edx,word [esi+32h]
  0046ae42  52                    push      edx
  0046ae43  e8d4ab0d00            call      00545A1Ch
  0046ae48  0faf465c              imul      eax,[esi+5Ch]
  0046ae4c  8b7e1c                mov       edi,[esi+1Ch]
  0046ae4f  03f8                  add       edi,eax
  0046ae51  0fbf4632              movsx     eax,word [esi+32h]
  0046ae55  897e1c                mov       [esi+1Ch],edi
  0046ae58  8b7e5c                mov       edi,[esi+5Ch]
  0046ae5b  50                    push      eax
  0046ae5c  e8b5ab0d00            call      00545A16h
  0046ae61  0fafc7                imul      eax,edi
  0046ae64  8b4e24                mov       ecx,[esi+24h]
  0046ae67  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0046ae6e  03c8                  add       ecx,eax
  0046ae70  894e24                mov       [esi+24h],ecx
  0046ae73  668b4e5c              mov       cx,[esi+5Ch]
  0046ae77  66014e12              add       [esi+12h],cx
  0046ae7b  8b4e20                mov       ecx,[esi+20h]
  0046ae7e  668b4612              mov       ax,[esi+12h]
  0046ae82  c1e20c                shl       edx,0Ch
  0046ae85  03ca                  add       ecx,edx
  0046ae87  663b4614              cmp       ax,[esi+14h]
  0046ae8b  894e20                mov       [esi+20h],ecx
  0046ae8e  7e11                  jle       short 0046AEA1h
  0046ae90  8d47ff                lea       eax,[edi-1]
  0046ae93  83f8ff                cmp       eax,0FFFFFFFFh
  0046ae96  89465c                mov       [esi+5Ch],eax
  0046ae99  7f06                  jg        short 0046AEA1h
  0046ae9b  66c746100800          mov       word [esi+10h],8
  0046aea1  66837e1000            cmp       word [esi+10h],0
  0046aea6  0f8479ffffff          je        near 0046AE25h
  0046aeac  8b461c                mov       eax,[esi+1Ch]
  0046aeaf  8b4e44                mov       ecx,[esi+44h]
  0046aeb2  c1f80c                sar       eax,0Ch
  0046aeb5  25ffff0000            and       eax,0FFFFh
  0046aeba  8b5624                mov       edx,[esi+24h]
  0046aebd  0bc8                  or        ecx,eax
  0046aebf  8b4648                mov       eax,[esi+48h]
  0046aec2  894e44                mov       [esi+44h],ecx
  0046aec5  8b4e20                mov       ecx,[esi+20h]
  0046aec8  c1f90c                sar       ecx,0Ch
  0046aecb  81e1ffff0000          and       ecx,0FFFFh
  0046aed1  895e1c                mov       [esi+1Ch],ebx
  0046aed4  0bc1                  or        eax,ecx
  0046aed6  896e20                mov       [esi+20h],ebp
  0046aed9  c1fa0c                sar       edx,0Ch
  0046aedc  894648                mov       [esi+48h],eax
  0046aedf  8b464c                mov       eax,[esi+4Ch]
  0046aee2  81e2ffff0000          and       edx,0FFFFh
  0046aee8  0bc2                  or        eax,edx
  0046aeea  89464c                mov       [esi+4Ch],eax
  0046aeed  8b442428              mov       eax,[esp+28h]
  0046aef1  894624                mov       [esi+24h],eax
  0046aef4  8b4648                mov       eax,[esi+48h]
  0046aef7  8b7e44                mov       edi,[esi+44h]
  0046aefa  8b5e4c                mov       ebx,[esi+4Ch]
  0046aefd  668b6e44              mov       bp,[esi+44h]
  0046af01  668b4e48              mov       cx,[esi+48h]
  0046af05  668b564c              mov       dx,[esi+4Ch]
  0046af09  6800000040            push      40000000h
  0046af0e  6a10                  push      10h
  0046af10  c1f810                sar       eax,10h
  0046af13  56                    push      esi
  0046af14  89442420              mov       [esp+20h],eax
  0046af18  c1ff10                sar       edi,10h
  0046af1b  c1fb10                sar       ebx,10h
  0046af1e  896c2428              mov       [esp+28h],ebp
  0046af22  66894c2424            mov       [esp+24h],cx
  0046af27  668954241c            mov       [esp+1Ch],dx
  0046af2c  e8cfa00c00            call      00535000h
  0046af31  83c40c                add       esp,0Ch
  0046af34  89442428              mov       [esp+28h],eax
  0046af38  e833beffff            call      00466D70h
  0046af3d  8b4c2428              mov       ecx,[esp+28h]
  0046af41  0fbfc0                movsx     eax,ax
  0046af44  3bc1                  cmp       eax,ecx
  0046af46  0f84cd000000          je        near 0046B019h
  0046af4c  66837e1008            cmp       word [esi+10h],8
  0046af51  0f85c2000000          jne       near 0046B019h
  0046af57  a168e05d00            mov       eax,[5DE068h]
  0046af5c  8b0d70e05d00          mov       ecx,[5DE070h]
  0046af62  0fbfef                movsx     ebp,di
  0046af65  0fbfd3                movsx     edx,bx
  0046af68  c1f80c                sar       eax,0Ch
  0046af6b  c1f90c                sar       ecx,0Ch
  0046af6e  2bc5                  sub       eax,ebp
  0046af70  89542420              mov       [esp+20h],edx
  0046af74  2bca                  sub       ecx,edx
  0046af76  8bd0                  mov       edx,eax
  0046af78  0fafd0                imul      edx,eax
  0046af7b  8bc1                  mov       eax,ecx
  0046af7d  0fafc1                imul      eax,ecx
  0046af80  03d0                  add       edx,eax
  0046af82  52                    push      edx
  0046af83  e888aa0d00            call      00545A10h
  0046af88  0fbf7c241c            movsx     edi,word [esp+1Ch]
  0046af8d  0fbf5c2410            movsx     ebx,word [esp+10h]
  0046af92  8b0d70e05d00          mov       ecx,[5DE070h]
  0046af98  89442428              mov       [esp+28h],eax
  0046af9c  a168e05d00            mov       eax,[5DE068h]
  0046afa1  c1f80c                sar       eax,0Ch
  0046afa4  2bc7                  sub       eax,edi
  0046afa6  c1f90c                sar       ecx,0Ch
  0046afa9  8bd0                  mov       edx,eax
  0046afab  2bcb                  sub       ecx,ebx
  0046afad  0fafd0                imul      edx,eax
  0046afb0  8bc1                  mov       eax,ecx
  0046afb2  0fafc1                imul      eax,ecx
  0046afb5  03d0                  add       edx,eax
  0046afb7  52                    push      edx
  0046afb8  e853aa0d00            call      00545A10h
  0046afbd  39442428              cmp       [esp+28h],eax
  0046afc1  7e33                  jle       short 0046AFF6h
  0046afc3  0fbf4c2418            movsx     ecx,word [esp+18h]
  0046afc8  c1e70c                shl       edi,0Ch
  0046afcb  c1e10c                shl       ecx,0Ch
  0046afce  c1e30c                shl       ebx,0Ch
  0046afd1  33c0                  xor       eax,eax
  0046afd3  897e1c                mov       [esi+1Ch],edi
  0046afd6  894e20                mov       [esi+20h],ecx
  0046afd9  895e24                mov       [esi+24h],ebx
  0046afdc  66c746100200          mov       word [esi+10h],2
  0046afe2  66894628              mov       [esi+28h],ax
  0046afe6  6689462a              mov       [esi+2Ah],ax
  0046afea  6689462c              mov       [esi+2Ch],ax
  0046afee  5f                    pop       edi
  0046afef  5e                    pop       esi
  0046aff0  5d                    pop       ebp
  0046aff1  5b                    pop       ebx
  0046aff2  83c414                add       esp,14h
  0046aff5  c3                    ret
  0046aff6  0fbf542414            movsx     edx,word [esp+14h]
  0046affb  8b442420              mov       eax,[esp+20h]
  0046afff  c1e50c                shl       ebp,0Ch
  0046b002  c1e20c                shl       edx,0Ch
  0046b005  c1e00c                shl       eax,0Ch
  0046b008  896e1c                mov       [esi+1Ch],ebp
  0046b00b  895620                mov       [esi+20h],edx
  0046b00e  894624                mov       [esi+24h],eax
  0046b011  5f                    pop       edi
  0046b012  5e                    pop       esi
  0046b013  5d                    pop       ebp
  0046b014  5b                    pop       ebx
  0046b015  83c414                add       esp,14h
  0046b018  c3                    ret
  0046b019  e852bdffff            call      00466D70h
  0046b01e  8b4c2428              mov       ecx,[esp+28h]
  0046b022  0fbfc0                movsx     eax,ax
  0046b025  3bc1                  cmp       eax,ecx
  0046b027  0f84cb000000          je        near 0046B0F8h
  0046b02d  66837e1002            cmp       word [esi+10h],2
  0046b032  0f85c0000000          jne       near 0046B0F8h
  0046b038  a168e05d00            mov       eax,[5DE068h]
  0046b03d  8b0d70e05d00          mov       ecx,[5DE070h]
  0046b043  0fbfff                movsx     edi,di
  0046b046  c1f80c                sar       eax,0Ch
  0046b049  0fbfdb                movsx     ebx,bx
  0046b04c  2bc7                  sub       eax,edi
  0046b04e  c1f90c                sar       ecx,0Ch
  0046b051  8bd0                  mov       edx,eax
  0046b053  2bcb                  sub       ecx,ebx
  0046b055  0fafd0                imul      edx,eax
  0046b058  8bc1                  mov       eax,ecx
  0046b05a  0fafc1                imul      eax,ecx
  0046b05d  03d0                  add       edx,eax
  0046b05f  52                    push      edx
  0046b060  e8aba90d00            call      00545A10h
  0046b065  0fbf542410            movsx     edx,word [esp+10h]
  0046b06a  8b0d70e05d00          mov       ecx,[5DE070h]
  0046b070  89442428              mov       [esp+28h],eax
  0046b074  a168e05d00            mov       eax,[5DE068h]
  0046b079  89542420              mov       [esp+20h],edx
  0046b07d  0fbfed                movsx     ebp,bp
  0046b080  c1f80c                sar       eax,0Ch
  0046b083  c1f90c                sar       ecx,0Ch
  0046b086  2bc5                  sub       eax,ebp
  0046b088  2bca                  sub       ecx,edx
  0046b08a  8bd0                  mov       edx,eax
  0046b08c  0fafd0                imul      edx,eax
  0046b08f  8bc1                  mov       eax,ecx
  0046b091  0fafc1                imul      eax,ecx
  0046b094  03d0                  add       edx,eax
  0046b096  52                    push      edx
  0046b097  e874a90d00            call      00545A10h
  0046b09c  39442428              cmp       [esp+28h],eax
  0046b0a0  7d33                  jge       short 0046B0D5h
  0046b0a2  0fbf4c2414            movsx     ecx,word [esp+14h]
  0046b0a7  c1e70c                shl       edi,0Ch
  0046b0aa  c1e10c                shl       ecx,0Ch
  0046b0ad  c1e30c                shl       ebx,0Ch
  0046b0b0  33c0                  xor       eax,eax
  0046b0b2  897e1c                mov       [esi+1Ch],edi
  0046b0b5  894e20                mov       [esi+20h],ecx
  0046b0b8  895e24                mov       [esi+24h],ebx
  0046b0bb  66c746100800          mov       word [esi+10h],8
  0046b0c1  66894628              mov       [esi+28h],ax
  0046b0c5  6689462a              mov       [esi+2Ah],ax
  0046b0c9  6689462c              mov       [esi+2Ch],ax
  0046b0cd  5f                    pop       edi
  0046b0ce  5e                    pop       esi
  0046b0cf  5d                    pop       ebp
  0046b0d0  5b                    pop       ebx
  0046b0d1  83c414                add       esp,14h
  0046b0d4  c3                    ret
  0046b0d5  0fbf542418            movsx     edx,word [esp+18h]
  0046b0da  8b442420              mov       eax,[esp+20h]
  0046b0de  c1e50c                shl       ebp,0Ch
  0046b0e1  c1e20c                shl       edx,0Ch
  0046b0e4  c1e00c                shl       eax,0Ch
  0046b0e7  896e1c                mov       [esi+1Ch],ebp
  0046b0ea  895620                mov       [esi+20h],edx
  0046b0ed  894624                mov       [esi+24h],eax
  0046b0f0  5f                    pop       edi
  0046b0f1  5e                    pop       esi
  0046b0f2  5d                    pop       ebp
  0046b0f3  5b                    pop       ebx
  0046b0f4  83c414                add       esp,14h
  0046b0f7  c3                    ret
  0046b0f8  e873bcffff            call      00466D70h
  0046b0fd  8b5c2428              mov       ebx,[esp+28h]
  0046b101  0fbfc8                movsx     ecx,ax
  0046b104  3bcb                  cmp       ecx,ebx
  0046b106  b801000000            mov       eax,1
  0046b10b  7516                  jne       short 0046B123h
  0046b10d  66837e1008            cmp       word [esi+10h],8
  0046b112  750f                  jne       short 0046B123h
  0046b114  33ed                  xor       ebp,ebp
  0046b116  66894610              mov       [esi+10h],ax
  0046b11a  896e5c                mov       [esi+5Ch],ebp
  0046b11d  66896e12              mov       [esi+12h],bp
  0046b121  eb02                  jmp       short 0046B125h
  0046b123  33ed                  xor       ebp,ebp
  0046b125  66394610              cmp       [esi+10h],ax
  0046b129  0f85a3000000          jne       near 0046B1D2h
  0046b12f  0fbf5616              movsx     edx,word [esi+16h]
  0046b133  8b465c                mov       eax,[esi+5Ch]
  0046b136  3bc2                  cmp       eax,edx
  0046b138  7d0e                  jge       short 0046B148h
  0046b13a  668b4e12              mov       cx,[esi+12h]
  0046b13e  663b4e14              cmp       cx,[esi+14h]
  0046b142  7d04                  jge       short 0046B148h
  0046b144  40                    inc       eax
  0046b145  89465c                mov       [esi+5Ch],eax
  0046b148  0fbf5632              movsx     edx,word [esi+32h]
  0046b14c  52                    push      edx
  0046b14d  e8caa80d00            call      00545A1Ch
  0046b152  0faf465c              imul      eax,[esi+5Ch]
  0046b156  8b7e1c                mov       edi,[esi+1Ch]
  0046b159  03f8                  add       edi,eax
  0046b15b  0fbf4632              movsx     eax,word [esi+32h]
  0046b15f  897e1c                mov       [esi+1Ch],edi
  0046b162  8b7e5c                mov       edi,[esi+5Ch]
  0046b165  50                    push      eax
  0046b166  e8aba80d00            call      00545A16h
  0046b16b  0fafc7                imul      eax,edi
  0046b16e  8b4e24                mov       ecx,[esi+24h]
  0046b171  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0046b178  03c8                  add       ecx,eax
  0046b17a  894e24                mov       [esi+24h],ecx
  0046b17d  668b4e5c              mov       cx,[esi+5Ch]
  0046b181  66014e12              add       [esi+12h],cx
  0046b185  8b4e20                mov       ecx,[esi+20h]
  0046b188  668b4612              mov       ax,[esi+12h]
  0046b18c  c1e20c                shl       edx,0Ch
  0046b18f  03ca                  add       ecx,edx
  0046b191  663b4614              cmp       ax,[esi+14h]
  0046b195  894e20                mov       [esi+20h],ecx
  0046b198  7e11                  jle       short 0046B1ABh
  0046b19a  8d47ff                lea       eax,[edi-1]
  0046b19d  83f8ff                cmp       eax,0FFFFFFFFh
  0046b1a0  89465c                mov       [esi+5Ch],eax
  0046b1a3  7506                  jne       short 0046B1ABh
  0046b1a5  66c746100400          mov       word [esi+10h],4
  0046b1ab  6800000040            push      40000000h
  0046b1b0  6a20                  push      20h
  0046b1b2  56                    push      esi
  0046b1b3  e8489e0c00            call      00535000h
  0046b1b8  83c40c                add       esp,0Ch
  0046b1bb  8bf8                  mov       edi,eax
  0046b1bd  e8aebbffff            call      00466D70h
  0046b1c2  0fbfc0                movsx     eax,ax
  0046b1c5  3bc7                  cmp       eax,edi
  0046b1c7  7409                  je        short 0046B1D2h
  0046b1c9  66c746100600          mov       word [esi+10h],6
  0046b1cf  896e5c                mov       [esi+5Ch],ebp
  0046b1d2  e899bbffff            call      00466D70h
  0046b1d7  0fbfc8                movsx     ecx,ax
  0046b1da  3bcb                  cmp       ecx,ebx
  0046b1dc  b803000000            mov       eax,3
  0046b1e1  7512                  jne       short 0046B1F5h
  0046b1e3  66837e1002            cmp       word [esi+10h],2
  0046b1e8  750b                  jne       short 0046B1F5h
  0046b1ea  66894610              mov       [esi+10h],ax
  0046b1ee  896e5c                mov       [esi+5Ch],ebp
  0046b1f1  66896e12              mov       [esi+12h],bp
  0046b1f5  66394610              cmp       [esi+10h],ax
  0046b1f9  bb05000000            mov       ebx,5
  0046b1fe  0f85a1000000          jne       near 0046B2A5h
  0046b204  0fbf5616              movsx     edx,word [esi+16h]
  0046b208  8b465c                mov       eax,[esi+5Ch]
  0046b20b  3bc2                  cmp       eax,edx
  0046b20d  7d0e                  jge       short 0046B21Dh
  0046b20f  668b4e12              mov       cx,[esi+12h]
  0046b213  663b4e14              cmp       cx,[esi+14h]
  0046b217  7d04                  jge       short 0046B21Dh
  0046b219  40                    inc       eax
  0046b21a  89465c                mov       [esi+5Ch],eax
  0046b21d  0fbf5632              movsx     edx,word [esi+32h]
  0046b221  52                    push      edx
  0046b222  e8f5a70d00            call      00545A1Ch
  0046b227  0faf465c              imul      eax,[esi+5Ch]
  0046b22b  8b7e1c                mov       edi,[esi+1Ch]
  0046b22e  2bf8                  sub       edi,eax
  0046b230  0fbf4632              movsx     eax,word [esi+32h]
  0046b234  897e1c                mov       [esi+1Ch],edi
  0046b237  8b7e5c                mov       edi,[esi+5Ch]
  0046b23a  50                    push      eax
  0046b23b  e8d6a70d00            call      00545A16h
  0046b240  0fafc7                imul      eax,edi
  0046b243  8b4e24                mov       ecx,[esi+24h]
  0046b246  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0046b24d  2bc8                  sub       ecx,eax
  0046b24f  894e24                mov       [esi+24h],ecx
  0046b252  668b4e5c              mov       cx,[esi+5Ch]
  0046b256  66014e12              add       [esi+12h],cx
  0046b25a  8b4e20                mov       ecx,[esi+20h]
  0046b25d  668b4612              mov       ax,[esi+12h]
  0046b261  c1e20c                shl       edx,0Ch
  0046b264  2bca                  sub       ecx,edx
  0046b266  663b4614              cmp       ax,[esi+14h]
  0046b26a  894e20                mov       [esi+20h],ecx
  0046b26d  7e0f                  jle       short 0046B27Eh
  0046b26f  8d47ff                lea       eax,[edi-1]
  0046b272  83f8ff                cmp       eax,0FFFFFFFFh
  0046b275  89465c                mov       [esi+5Ch],eax
  0046b278  7504                  jne       short 0046B27Eh
  0046b27a  66895e10              mov       [esi+10h],bx
  0046b27e  6800000040            push      40000000h
  0046b283  6a20                  push      20h
  0046b285  56                    push      esi
  0046b286  e8759d0c00            call      00535000h
  0046b28b  83c40c                add       esp,0Ch
  0046b28e  8bf8                  mov       edi,eax
  0046b290  e8dbbaffff            call      00466D70h
  0046b295  0fbfc0                movsx     eax,ax
  0046b298  3bc7                  cmp       eax,edi
  0046b29a  7409                  je        short 0046B2A5h
  0046b29c  66c746100700          mov       word [esi+10h],7
  0046b2a2  896e5c                mov       [esi+5Ch],ebp
  0046b2a5  66837e1004            cmp       word [esi+10h],4
  0046b2aa  750c                  jne       short 0046B2B8h
  0046b2ac  396c2428              cmp       [esp+28h],ebp
  0046b2b0  7506                  jne       short 0046B2B8h
  0046b2b2  66c746100200          mov       word [esi+10h],2
  0046b2b8  66395e10              cmp       [esi+10h],bx
  0046b2bc  750c                  jne       short 0046B2CAh
  0046b2be  396c2428              cmp       [esp+28h],ebp
  0046b2c2  7506                  jne       short 0046B2CAh
  0046b2c4  66c746100800          mov       word [esi+10h],8
  0046b2ca  66837e1006            cmp       word [esi+10h],6
  0046b2cf  b938ffffff            mov       ecx,0FFFFFF38h
  0046b2d4  752c                  jne       short 0046B302h
  0046b2d6  396e5c                cmp       [esi+5Ch],ebp
  0046b2d9  7527                  jne       short 0046B302h
  0046b2db  66014e28              add       [esi+28h],cx
  0046b2df  668b4628              mov       ax,[esi+28h]
  0046b2e3  66014e2a              add       [esi+2Ah],cx
  0046b2e7  66014e2c              add       [esi+2Ch],cx
  0046b2eb  663bc5                cmp       ax,bp
  0046b2ee  7f12                  jg        short 0046B302h
  0046b2f0  66896e28              mov       [esi+28h],bp
  0046b2f4  66896e2a              mov       [esi+2Ah],bp
  0046b2f8  66896e2c              mov       [esi+2Ch],bp
  0046b2fc  66c746100800          mov       word [esi+10h],8
  0046b302  66837e1007            cmp       word [esi+10h],7
  0046b307  752c                  jne       short 0046B335h
  0046b309  396e5c                cmp       [esi+5Ch],ebp
  0046b30c  7527                  jne       short 0046B335h
  0046b30e  66014e28              add       [esi+28h],cx
  0046b312  668b4628              mov       ax,[esi+28h]
  0046b316  66014e2a              add       [esi+2Ah],cx
  0046b31a  66014e2c              add       [esi+2Ch],cx
  0046b31e  663bc5                cmp       ax,bp
  0046b321  7f12                  jg        short 0046B335h
  0046b323  66896e28              mov       [esi+28h],bp
  0046b327  66896e2a              mov       [esi+2Ah],bp
  0046b32b  66896e2c              mov       [esi+2Ch],bp
  0046b32f  66c746100200          mov       word [esi+10h],2
  0046b335  5f                    pop       edi
  0046b336  5e                    pop       esi
  0046b337  5d                    pop       ebp
  0046b338  5b                    pop       ebx
  0046b339  83c414                add       esp,14h
  0046b33c  c3                    ret
