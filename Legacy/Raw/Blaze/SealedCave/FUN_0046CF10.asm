  0046cf10  55                    push      ebp
  0046cf11  56                    push      esi
  0046cf12  8b74240c              mov       esi,[esp+0Ch]
  0046cf16  57                    push      edi
  0046cf17  6800000040            push      40000000h
  0046cf1c  6a10                  push      10h
  0046cf1e  56                    push      esi
  0046cf1f  e8dc800c00            call      00535000h
  0046cf24  83c40c                add       esp,0Ch
  0046cf27  8bf8                  mov       edi,eax
  0046cf29  6800000040            push      40000000h
  0046cf2e  6a20                  push      20h
  0046cf30  56                    push      esi
  0046cf31  e8ca800c00            call      00535000h
  0046cf36  b801000000            mov       eax,1
  0046cf3b  83c40c                add       esp,0Ch
  0046cf3e  33ed                  xor       ebp,ebp
  0046cf40  3bf8                  cmp       edi,eax
  0046cf42  7c11                  jl        short 0046CF55h
  0046cf44  66396e10              cmp       [esi+10h],bp
  0046cf48  750b                  jne       short 0046CF55h
  0046cf4a  66894610              mov       [esi+10h],ax
  0046cf4e  66896e12              mov       [esi+12h],bp
  0046cf52  896e5c                mov       [esi+5Ch],ebp
  0046cf55  3bfd                  cmp       edi,ebp
  0046cf57  7514                  jne       short 0046CF6Dh
  0046cf59  66837e1002            cmp       word [esi+10h],2
  0046cf5e  750d                  jne       short 0046CF6Dh
  0046cf60  66c746100300          mov       word [esi+10h],3
  0046cf66  66896e12              mov       [esi+12h],bp
  0046cf6a  896e5c                mov       [esi+5Ch],ebp
  0046cf6d  66394610              cmp       [esi+10h],ax
  0046cf71  7579                  jne       short 0046CFECh
  0046cf73  668b4e12              mov       cx,[esi+12h]
  0046cf77  8b7e20                mov       edi,[esi+20h]
  0046cf7a  0fbfc1                movsx     eax,cx
  0046cf7d  8bd0                  mov       edx,eax
  0046cf7f  c1e20c                shl       edx,0Ch
  0046cf82  03fa                  add       edi,edx
  0046cf84  8b565c                mov       edx,[esi+5Ch]
  0046cf87  03d0                  add       edx,eax
  0046cf89  897e20                mov       [esi+20h],edi
  0046cf8c  89565c                mov       [esi+5Ch],edx
  0046cf8f  8bc2                  mov       eax,edx
  0046cf91  0fbf5614              movsx     edx,word [esi+14h]
  0046cf95  3bc2                  cmp       eax,edx
  0046cf97  7e12                  jle       short 0046CFABh
  0046cf99  8d41ff                lea       eax,[ecx-1]
  0046cf9c  663bc5                cmp       ax,bp
  0046cf9f  66894612              mov       [esi+12h],ax
  0046cfa3  7d06                  jge       short 0046CFABh
  0046cfa5  66c746100200          mov       word [esi+10h],2
  0046cfab  668b4612              mov       ax,[esi+12h]
  0046cfaf  663d1400              cmp       ax,14h
  0046cfb3  7d0a                  jge       short 0046CFBFh
  0046cfb5  39565c                cmp       [esi+5Ch],edx
  0046cfb8  7d05                  jge       short 0046CFBFh
  0046cfba  40                    inc       eax
  0046cfbb  66894612              mov       [esi+12h],ax
  0046cfbf  0fbf4632              movsx     eax,word [esi+32h]
  0046cfc3  50                    push      eax
  0046cfc4  e8538a0d00            call      00545A1Ch
  0046cfc9  0fbf4e16              movsx     ecx,word [esi+16h]
  0046cfcd  0fafc1                imul      eax,ecx
  0046cfd0  0fbf5632              movsx     edx,word [esi+32h]
  0046cfd4  8b4e1c                mov       ecx,[esi+1Ch]
  0046cfd7  52                    push      edx
  0046cfd8  03c8                  add       ecx,eax
  0046cfda  894e1c                mov       [esi+1Ch],ecx
  0046cfdd  e8348a0d00            call      00545A16h
  0046cfe2  0fbf4e16              movsx     ecx,word [esi+16h]
  0046cfe6  0fafc1                imul      eax,ecx
  0046cfe9  014624                add       [esi+24h],eax
  0046cfec  66837e1003            cmp       word [esi+10h],3
  0046cff1  7577                  jne       short 0046D06Ah
  0046cff3  668b4e12              mov       cx,[esi+12h]
  0046cff7  8b7e20                mov       edi,[esi+20h]
  0046cffa  0fbfc1                movsx     eax,cx
  0046cffd  8bd0                  mov       edx,eax
  0046cfff  c1e20c                shl       edx,0Ch
  0046d002  2bfa                  sub       edi,edx
  0046d004  8b565c                mov       edx,[esi+5Ch]
  0046d007  03d0                  add       edx,eax
  0046d009  897e20                mov       [esi+20h],edi
  0046d00c  89565c                mov       [esi+5Ch],edx
  0046d00f  8bc2                  mov       eax,edx
  0046d011  0fbf5614              movsx     edx,word [esi+14h]
  0046d015  3bc2                  cmp       eax,edx
  0046d017  7e10                  jle       short 0046D029h
  0046d019  8d41ff                lea       eax,[ecx-1]
  0046d01c  663bc5                cmp       ax,bp
  0046d01f  66894612              mov       [esi+12h],ax
  0046d023  7d04                  jge       short 0046D029h
  0046d025  66896e10              mov       [esi+10h],bp
  0046d029  668b4612              mov       ax,[esi+12h]
  0046d02d  663d1400              cmp       ax,14h
  0046d031  7d0a                  jge       short 0046D03Dh
  0046d033  39565c                cmp       [esi+5Ch],edx
  0046d036  7d05                  jge       short 0046D03Dh
  0046d038  40                    inc       eax
  0046d039  66894612              mov       [esi+12h],ax
  0046d03d  0fbf4632              movsx     eax,word [esi+32h]
  0046d041  50                    push      eax
  0046d042  e8d5890d00            call      00545A1Ch
  0046d047  0fbf4e16              movsx     ecx,word [esi+16h]
  0046d04b  0fafc1                imul      eax,ecx
  0046d04e  0fbf5632              movsx     edx,word [esi+32h]
  0046d052  8b6e1c                mov       ebp,[esi+1Ch]
  0046d055  52                    push      edx
  0046d056  2be8                  sub       ebp,eax
  0046d058  896e1c                mov       [esi+1Ch],ebp
  0046d05b  e8b6890d00            call      00545A16h
  0046d060  0fbf4e16              movsx     ecx,word [esi+16h]
  0046d064  0fafc1                imul      eax,ecx
  0046d067  294624                sub       [esi+24h],eax
  0046d06a  5f                    pop       edi
  0046d06b  5e                    pop       esi
  0046d06c  5d                    pop       ebp
  0046d06d  c3                    ret
