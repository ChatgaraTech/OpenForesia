Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045ecc0  section .text  offset fichier 0x5e0c0  1632 octets

  0045ecc0  53                    push      ebx
  0045ecc1  55                    push      ebp
  0045ecc2  56                    push      esi
  0045ecc3  8b742410              mov       esi,[esp+10h]
  0045ecc7  57                    push      edi
  0045ecc8  668b4e10              mov       cx,[esi+10h]
  0045eccc  6683f906              cmp       cx,6
  0045ecd0  744b                  je        short 0045ED1Dh
  0045ecd2  6683f907              cmp       cx,7
  0045ecd6  7445                  je        short 0045ED1Dh
  0045ecd8  668b4628              mov       ax,[esi+28h]
  0045ecdc  bf00100000            mov       edi,1000h
  0045ece1  0fbfd0                movsx     edx,ax
  0045ece4  2bfa                  sub       edi,edx
  0045ece6  c1ff03                sar       edi,3
  0045ece9  03f8                  add       edi,eax
  0045eceb  668b462a              mov       ax,[esi+2Ah]
  0045ecef  0fbfd0                movsx     edx,ax
  0045ecf2  66897e28              mov       [esi+28h],di
  0045ecf6  bf00100000            mov       edi,1000h
  0045ecfb  2bfa                  sub       edi,edx
  0045ecfd  c1ff03                sar       edi,3
  0045ed00  03f8                  add       edi,eax
  0045ed02  668b462c              mov       ax,[esi+2Ch]
  0045ed06  0fbfd0                movsx     edx,ax
  0045ed09  66897e2a              mov       [esi+2Ah],di
  0045ed0d  bf00100000            mov       edi,1000h
  0045ed12  2bfa                  sub       edi,edx
  0045ed14  c1ff03                sar       edi,3
  0045ed17  03f8                  add       edi,eax
  0045ed19  66897e2c              mov       [esi+2Ch],di

LAB_0045ed1d:
  0045ed1d  33ff                  xor       edi,edi
  0045ed1f  663bcf                cmp       cx,di
  0045ed22  0f85c7000000          jne       near 0045EDEFh
  0045ed28  8b5e1c                mov       ebx,[esi+1Ch]
  0045ed2b  8b6e20                mov       ebp,[esi+20h]
  0045ed2e  8b4624                mov       eax,[esi+24h]
  0045ed31  8bcb                  mov       ecx,ebx
  0045ed33  8bd5                  mov       edx,ebp
  0045ed35  89442414              mov       [esp+14h],eax
  0045ed39  c1f90c                sar       ecx,0Ch
  0045ed3c  c1fa0c                sar       edx,0Ch
  0045ed3f  c1f80c                sar       eax,0Ch
  0045ed42  6689be98000000        mov       [esi+98h],di
  0045ed49  66897e12              mov       [esi+12h],di
  0045ed4d  897e5c                mov       [esi+5Ch],edi
  0045ed50  894e44                mov       [esi+44h],ecx
  0045ed53  895648                mov       [esi+48h],edx
  0045ed56  89464c                mov       [esi+4Ch],eax

LAB_0045ed59:
  0045ed59  0fbf4e16              movsx     ecx,word [esi+16h]
  0045ed5d  8b465c                mov       eax,[esi+5Ch]
  0045ed60  3bc1                  cmp       eax,ecx
  0045ed62  7d0e                  jge       short 0045ED72h
  0045ed64  668b5612              mov       dx,[esi+12h]
  0045ed68  663b5614              cmp       dx,[esi+14h]
  0045ed6c  7d04                  jge       short 0045ED72h
  0045ed6e  40                    inc       eax
  0045ed6f  89465c                mov       [esi+5Ch],eax

LAB_0045ed72:
  0045ed72  0fbf4632              movsx     eax,word [esi+32h]
  0045ed76  50                    push      eax
  0045ed77  e8a06c0e00            call      00545A1Ch
  0045ed7c  0faf465c              imul      eax,[esi+5Ch]
  0045ed80  8b7e1c                mov       edi,[esi+1Ch]
  0045ed83  0fbf4e32              movsx     ecx,word [esi+32h]
  0045ed87  03f8                  add       edi,eax
  0045ed89  51                    push      ecx
  0045ed8a  897e1c                mov       [esi+1Ch],edi
  0045ed8d  8b7e5c                mov       edi,[esi+5Ch]
  0045ed90  e8816c0e00            call      00545A16h
  0045ed95  0fafc7                imul      eax,edi
  0045ed98  8b4e24                mov       ecx,[esi+24h]
  0045ed9b  668b565c              mov       dx,[esi+5Ch]
  0045ed9f  66015612              add       [esi+12h],dx
  0045eda3  03c8                  add       ecx,eax
  0045eda5  668b4612              mov       ax,[esi+12h]
  0045eda9  894e24                mov       [esi+24h],ecx
  0045edac  663b4614              cmp       ax,[esi+14h]
  0045edb0  7e11                  jle       short 0045EDC3h
  0045edb2  8d47ff                lea       eax,[edi-1]
  0045edb5  83f8ff                cmp       eax,0FFFFFFFFh
  0045edb8  89465c                mov       [esi+5Ch],eax
  0045edbb  7f06                  jg        short 0045EDC3h
  0045edbd  66c746100800          mov       word [esi+10h],8

LAB_0045edc3:
  0045edc3  66837e1000            cmp       word [esi+10h],0
  0045edc8  748f                  je        short 0045ED59h
  0045edca  8b461c                mov       eax,[esi+1Ch]
  0045edcd  8b4e24                mov       ecx,[esi+24h]
  0045edd0  8b542414              mov       edx,[esp+14h]
  0045edd4  895e1c                mov       [esi+1Ch],ebx
  0045edd7  c1f80c                sar       eax,0Ch
  0045edda  c1f90c                sar       ecx,0Ch
  0045eddd  894648                mov       [esi+48h],eax
  0045ede0  66898e9a000000        mov       [esi+9Ah],cx
  0045ede7  896e20                mov       [esi+20h],ebp
  0045edea  895624                mov       [esi+24h],edx
  0045eded  33ff                  xor       edi,edi

LAB_0045edef:
  0045edef  6800000040            push      40000000h
  0045edf4  6a10                  push      10h
  0045edf6  56                    push      esi
  0045edf7  e804620d00            call      00535000h
  0045edfc  8be8                  mov       ebp,eax
  0045edfe  83c40c                add       esp,0Ch
  0045ee01  896c2414              mov       [esp+14h],ebp
  0045ee05  e8667f0000            call      00466D70h
  0045ee0a  0fbfc0                movsx     eax,ax
  0045ee0d  3bc5                  cmp       eax,ebp
  0045ee0f  bb08000000            mov       ebx,8
  0045ee14  0f84b4000000          je        near 0045EECEh
  0045ee1a  66395e10              cmp       [esi+10h],bx
  0045ee1e  0f85aa000000          jne       near 0045EECEh
  0045ee24  a168e05d00            mov       eax,[5DE068h]
  0045ee29  8b7e44                mov       edi,[esi+44h]
  0045ee2c  8b0d70e05d00          mov       ecx,[5DE070h]
  0045ee32  8b6e4c                mov       ebp,[esi+4Ch]
  0045ee35  c1f80c                sar       eax,0Ch
  0045ee38  2bc7                  sub       eax,edi
  0045ee3a  c1f90c                sar       ecx,0Ch
  0045ee3d  8bd0                  mov       edx,eax
  0045ee3f  2bcd                  sub       ecx,ebp
  0045ee41  0fafd0                imul      edx,eax
  0045ee44  8bc1                  mov       eax,ecx
  0045ee46  0fafc1                imul      eax,ecx
  0045ee49  03d0                  add       edx,eax
  0045ee4b  52                    push      edx
  0045ee4c  e8bf6b0e00            call      00545A10h
  0045ee51  8b4e48                mov       ecx,[esi+48h]
  0045ee54  8bf8                  mov       edi,eax
  0045ee56  a168e05d00            mov       eax,[5DE068h]
  0045ee5b  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045ee62  c1f80c                sar       eax,0Ch
  0045ee65  2bc1                  sub       eax,ecx
  0045ee67  8b0d70e05d00          mov       ecx,[5DE070h]
  0045ee6d  c1f90c                sar       ecx,0Ch
  0045ee70  2bca                  sub       ecx,edx
  0045ee72  8bd0                  mov       edx,eax
  0045ee74  0fafd0                imul      edx,eax
  0045ee77  8bc1                  mov       eax,ecx
  0045ee79  0fafc1                imul      eax,ecx
  0045ee7c  03d0                  add       edx,eax
  0045ee7e  52                    push      edx
  0045ee7f  e88c6b0e00            call      00545A10h
  0045ee84  3bf8                  cmp       edi,eax
  0045ee86  7e2f                  jle       short 0045EEB7h
  0045ee88  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045ee8f  8b4e48                mov       ecx,[esi+48h]
  0045ee92  66c746100200          mov       word [esi+10h],2
  0045ee98  c1e10c                shl       ecx,0Ch
  0045ee9b  c1e20c                shl       edx,0Ch
  0045ee9e  33c0                  xor       eax,eax
  0045eea0  894e1c                mov       [esi+1Ch],ecx
  0045eea3  895624                mov       [esi+24h],edx
  0045eea6  66894628              mov       [esi+28h],ax
  0045eeaa  6689462a              mov       [esi+2Ah],ax
  0045eeae  6689462c              mov       [esi+2Ch],ax
  0045eeb2  5f                    pop       edi
  0045eeb3  5e                    pop       esi
  0045eeb4  5d                    pop       ebp
  0045eeb5  5b                    pop       ebx
  0045eeb6  c3                    ret

LAB_0045eeb7:
  0045eeb7  8b4644                mov       eax,[esi+44h]
  0045eeba  8b4e4c                mov       ecx,[esi+4Ch]
  0045eebd  c1e00c                shl       eax,0Ch
  0045eec0  c1e10c                shl       ecx,0Ch
  0045eec3  89461c                mov       [esi+1Ch],eax
  0045eec6  894e24                mov       [esi+24h],ecx
  0045eec9  5f                    pop       edi
  0045eeca  5e                    pop       esi
  0045eecb  5d                    pop       ebp
  0045eecc  5b                    pop       ebx
  0045eecd  c3                    ret

LAB_0045eece:
  0045eece  e89d7e0000            call      00466D70h
  0045eed3  0fbfd0                movsx     edx,ax
  0045eed6  3bd5                  cmp       edx,ebp
  0045eed8  0f84b3000000          je        near 0045EF91h
  0045eede  66837e1002            cmp       word [esi+10h],2
  0045eee3  0f85a8000000          jne       near 0045EF91h
  0045eee9  a168e05d00            mov       eax,[5DE068h]
  0045eeee  8b0d70e05d00          mov       ecx,[5DE070h]
  0045eef4  8b6e44                mov       ebp,[esi+44h]
  0045eef7  8b564c                mov       edx,[esi+4Ch]
  0045eefa  c1f80c                sar       eax,0Ch
  0045eefd  c1f90c                sar       ecx,0Ch
  0045ef00  2bc5                  sub       eax,ebp
  0045ef02  2bca                  sub       ecx,edx
  0045ef04  8bd0                  mov       edx,eax
  0045ef06  0fafd0                imul      edx,eax
  0045ef09  8bc1                  mov       eax,ecx
  0045ef0b  0fafc1                imul      eax,ecx
  0045ef0e  03d0                  add       edx,eax
  0045ef10  52                    push      edx
  0045ef11  e8fa6a0e00            call      00545A10h
  0045ef16  8b0d70e05d00          mov       ecx,[5DE070h]
  0045ef1c  8b6e48                mov       ebp,[esi+48h]
  0045ef1f  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045ef26  8bf8                  mov       edi,eax
  0045ef28  a168e05d00            mov       eax,[5DE068h]
  0045ef2d  c1f80c                sar       eax,0Ch
  0045ef30  c1f90c                sar       ecx,0Ch
  0045ef33  2bc5                  sub       eax,ebp
  0045ef35  2bca                  sub       ecx,edx
  0045ef37  8bd0                  mov       edx,eax
  0045ef39  0fafd0                imul      edx,eax
  0045ef3c  8bc1                  mov       eax,ecx
  0045ef3e  0fafc1                imul      eax,ecx
  0045ef41  03d0                  add       edx,eax
  0045ef43  52                    push      edx
  0045ef44  e8c76a0e00            call      00545A10h
  0045ef49  3bf8                  cmp       edi,eax
  0045ef4b  7d29                  jge       short 0045EF76h
  0045ef4d  8b4e44                mov       ecx,[esi+44h]
  0045ef50  8b564c                mov       edx,[esi+4Ch]
  0045ef53  c1e10c                shl       ecx,0Ch
  0045ef56  c1e20c                shl       edx,0Ch
  0045ef59  33c0                  xor       eax,eax
  0045ef5b  894e1c                mov       [esi+1Ch],ecx
  0045ef5e  895624                mov       [esi+24h],edx
  0045ef61  66895e10              mov       [esi+10h],bx
  0045ef65  66894628              mov       [esi+28h],ax
  0045ef69  6689462a              mov       [esi+2Ah],ax
  0045ef6d  6689462c              mov       [esi+2Ch],ax
  0045ef71  5f                    pop       edi
  0045ef72  5e                    pop       esi
  0045ef73  5d                    pop       ebp
  0045ef74  5b                    pop       ebx
  0045ef75  c3                    ret

LAB_0045ef76:
  0045ef76  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0045ef7d  8b4648                mov       eax,[esi+48h]
  0045ef80  c1e00c                shl       eax,0Ch
  0045ef83  c1e10c                shl       ecx,0Ch
  0045ef86  89461c                mov       [esi+1Ch],eax
  0045ef89  894e24                mov       [esi+24h],ecx
  0045ef8c  5f                    pop       edi
  0045ef8d  5e                    pop       esi
  0045ef8e  5d                    pop       ebp
  0045ef8f  5b                    pop       ebx
  0045ef90  c3                    ret

LAB_0045ef91:
  0045ef91  e8da7d0000            call      00466D70h
  0045ef96  0fbfd0                movsx     edx,ax
  0045ef99  3bd5                  cmp       edx,ebp
  0045ef9b  7540                  jne       short 0045EFDDh
  0045ef9d  66395e10              cmp       [esi+10h],bx
  0045efa1  753a                  jne       short 0045EFDDh
  0045efa3  bb01000000            mov       ebx,1
  0045efa8  897e5c                mov       [esi+5Ch],edi
  0045efab  66895e10              mov       [esi+10h],bx
  0045efaf  66897e12              mov       [esi+12h],di
  0045efb3  a1cc975e00            mov       eax,[5E97CCh]
  0045efb8  57                    push      edi
  0045efb9  57                    push      edi
  0045efba  6800100000            push      1000h
  0045efbf  8b88a8000000          mov       ecx,[eax+0A8h]
  0045efc5  6880010000            push      180h
  0045efca  8b11                  mov       edx,[ecx]
  0045efcc  660fb6422b            movzx     ax,byte [edx+2Bh]
  0045efd1  50                    push      eax
  0045efd2  56                    push      esi
  0045efd3  e8a8900000            call      00468080h
  0045efd8  83c418                add       esp,18h
  0045efdb  eb05                  jmp       short 0045EFE2h

LAB_0045efdd:
  0045efdd  bb01000000            mov       ebx,1

LAB_0045efe2:
  0045efe2  66395e10              cmp       [esi+10h],bx
  0045efe6  0f85d9000000          jne       near 0045F0C5h
  0045efec  0fbf4e16              movsx     ecx,word [esi+16h]
  0045eff0  8b465c                mov       eax,[esi+5Ch]
  0045eff3  3bc1                  cmp       eax,ecx
  0045eff5  7d0e                  jge       short 0045F005h
  0045eff7  668b5612              mov       dx,[esi+12h]
  0045effb  663b5614              cmp       dx,[esi+14h]
  0045efff  7d04                  jge       short 0045F005h
  0045f001  40                    inc       eax
  0045f002  89465c                mov       [esi+5Ch],eax

LAB_0045f005:
  0045f005  0fbf4632              movsx     eax,word [esi+32h]
  0045f009  50                    push      eax
  0045f00a  e80d6a0e00            call      00545A1Ch
  0045f00f  0faf465c              imul      eax,[esi+5Ch]
  0045f013  8b7e1c                mov       edi,[esi+1Ch]
  0045f016  0fbf4e32              movsx     ecx,word [esi+32h]
  0045f01a  03f8                  add       edi,eax
  0045f01c  51                    push      ecx
  0045f01d  897e1c                mov       [esi+1Ch],edi
  0045f020  8b7e5c                mov       edi,[esi+5Ch]
  0045f023  e8ee690e00            call      00545A16h
  0045f028  0fafc7                imul      eax,edi
  0045f02b  8b4e24                mov       ecx,[esi+24h]
  0045f02e  668b565c              mov       dx,[esi+5Ch]
  0045f032  66015612              add       [esi+12h],dx
  0045f036  03c8                  add       ecx,eax
  0045f038  668b4612              mov       ax,[esi+12h]
  0045f03c  894e24                mov       [esi+24h],ecx
  0045f03f  663b4614              cmp       ax,[esi+14h]
  0045f043  7e55                  jle       short 0045F09Ah
  0045f045  6683be9800000000      cmp       word [esi+98h],0
  0045f04d  7507                  jne       short 0045F056h
  0045f04f  66899e98000000        mov       [esi+98h],bx

LAB_0045f056:
  0045f056  8d47ff                lea       eax,[edi-1]
  0045f059  83f8ff                cmp       eax,0FFFFFFFFh
  0045f05c  89465c                mov       [esi+5Ch],eax
  0045f05f  7539                  jne       short 0045F09Ah
  0045f061  66c746100400          mov       word [esi+10h],4
  0045f067  66c786980000000000    mov       word [esi+98h],0
  0045f070  a1cc975e00            mov       eax,[5E97CCh]
  0045f075  6a00                  push      0
  0045f077  6a00                  push      0
  0045f079  6800100000            push      1000h
  0045f07e  8b88a8000000          mov       ecx,[eax+0A8h]
  0045f084  6800030000            push      300h
  0045f089  8b11                  mov       edx,[ecx]
  0045f08b  660fb6422c            movzx     ax,byte [edx+2Ch]
  0045f090  50                    push      eax
  0045f091  56                    push      esi
  0045f092  e8e98f0000            call      00468080h
  0045f097  83c418                add       esp,18h

LAB_0045f09a:
  0045f09a  6800000040            push      40000000h
  0045f09f  6a20                  push      20h
  0045f0a1  56                    push      esi
  0045f0a2  e8595f0d00            call      00535000h
  0045f0a7  83c40c                add       esp,0Ch
  0045f0aa  8bf8                  mov       edi,eax
  0045f0ac  e8bf7c0000            call      00466D70h
  0045f0b1  0fbfc8                movsx     ecx,ax
  0045f0b4  3bcf                  cmp       ecx,edi
  0045f0b6  740d                  je        short 0045F0C5h
  0045f0b8  66c746100600          mov       word [esi+10h],6
  0045f0be  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0045f0c5:
  0045f0c5  e8a67c0000            call      00466D70h
  0045f0ca  0fbfd0                movsx     edx,ax
  0045f0cd  3bd5                  cmp       edx,ebp
  0045f0cf  bf03000000            mov       edi,3
  0045f0d4  753e                  jne       short 0045F114h
  0045f0d6  66837e1002            cmp       word [esi+10h],2
  0045f0db  7537                  jne       short 0045F114h
  0045f0dd  33ed                  xor       ebp,ebp
  0045f0df  66897e10              mov       [esi+10h],di
  0045f0e3  896e5c                mov       [esi+5Ch],ebp
  0045f0e6  66896e12              mov       [esi+12h],bp
  0045f0ea  a1cc975e00            mov       eax,[5E97CCh]
  0045f0ef  55                    push      ebp
  0045f0f0  55                    push      ebp
  0045f0f1  6800100000            push      1000h
  0045f0f6  8b88a8000000          mov       ecx,[eax+0A8h]
  0045f0fc  6880010000            push      180h
  0045f101  8b11                  mov       edx,[ecx]
  0045f103  660fb6422b            movzx     ax,byte [edx+2Bh]
  0045f108  50                    push      eax
  0045f109  56                    push      esi
  0045f10a  e8718f0000            call      00468080h
  0045f10f  83c418                add       esp,18h
  0045f112  eb02                  jmp       short 0045F116h

LAB_0045f114:
  0045f114  33ed                  xor       ebp,ebp

LAB_0045f116:
  0045f116  66397e10              cmp       [esi+10h],di
  0045f11a  bb05000000            mov       ebx,5
  0045f11f  0f85d0000000          jne       near 0045F1F5h
  0045f125  0fbf4e16              movsx     ecx,word [esi+16h]
  0045f129  8b465c                mov       eax,[esi+5Ch]
  0045f12c  3bc1                  cmp       eax,ecx
  0045f12e  7d0e                  jge       short 0045F13Eh
  0045f130  668b5612              mov       dx,[esi+12h]
  0045f134  663b5614              cmp       dx,[esi+14h]
  0045f138  7d04                  jge       short 0045F13Eh
  0045f13a  40                    inc       eax
  0045f13b  89465c                mov       [esi+5Ch],eax

LAB_0045f13e:
  0045f13e  0fbf4632              movsx     eax,word [esi+32h]
  0045f142  50                    push      eax
  0045f143  e8d4680e00            call      00545A1Ch
  0045f148  0faf465c              imul      eax,[esi+5Ch]
  0045f14c  8b7e1c                mov       edi,[esi+1Ch]
  0045f14f  0fbf4e32              movsx     ecx,word [esi+32h]
  0045f153  2bf8                  sub       edi,eax
  0045f155  51                    push      ecx
  0045f156  897e1c                mov       [esi+1Ch],edi
  0045f159  8b7e5c                mov       edi,[esi+5Ch]
  0045f15c  e8b5680e00            call      00545A16h
  0045f161  0fafc7                imul      eax,edi
  0045f164  8b4e24                mov       ecx,[esi+24h]
  0045f167  668b565c              mov       dx,[esi+5Ch]
  0045f16b  66015612              add       [esi+12h],dx
  0045f16f  2bc8                  sub       ecx,eax
  0045f171  668b4612              mov       ax,[esi+12h]
  0045f175  894e24                mov       [esi+24h],ecx
  0045f178  663b4614              cmp       ax,[esi+14h]
  0045f17c  7e50                  jle       short 0045F1CEh
  0045f17e  6639ae98000000        cmp       [esi+98h],bp
  0045f185  7509                  jne       short 0045F190h
  0045f187  66c786980000000100    mov       word [esi+98h],1

LAB_0045f190:
  0045f190  8d47ff                lea       eax,[edi-1]
  0045f193  83f8ff                cmp       eax,0FFFFFFFFh
  0045f196  89465c                mov       [esi+5Ch],eax
  0045f199  7533                  jne       short 0045F1CEh
  0045f19b  66895e10              mov       [esi+10h],bx
  0045f19f  6689ae98000000        mov       [esi+98h],bp
  0045f1a6  a1cc975e00            mov       eax,[5E97CCh]
  0045f1ab  55                    push      ebp
  0045f1ac  55                    push      ebp
  0045f1ad  6800100000            push      1000h
  0045f1b2  8b88a8000000          mov       ecx,[eax+0A8h]
  0045f1b8  6800030000            push      300h
  0045f1bd  8b11                  mov       edx,[ecx]
  0045f1bf  660fb6422c            movzx     ax,byte [edx+2Ch]
  0045f1c4  50                    push      eax
  0045f1c5  56                    push      esi
  0045f1c6  e8b58e0000            call      00468080h
  0045f1cb  83c418                add       esp,18h

LAB_0045f1ce:
  0045f1ce  6800000040            push      40000000h
  0045f1d3  6a20                  push      20h
  0045f1d5  56                    push      esi
  0045f1d6  e8255e0d00            call      00535000h
  0045f1db  83c40c                add       esp,0Ch
  0045f1de  8bf8                  mov       edi,eax
  0045f1e0  e88b7b0000            call      00466D70h
  0045f1e5  0fbfc8                movsx     ecx,ax
  0045f1e8  3bcf                  cmp       ecx,edi
  0045f1ea  7409                  je        short 0045F1F5h
  0045f1ec  66c746100700          mov       word [esi+10h],7
  0045f1f2  896e5c                mov       [esi+5Ch],ebp

LAB_0045f1f5:
  0045f1f5  66837e1004            cmp       word [esi+10h],4
  0045f1fa  750c                  jne       short 0045F208h
  0045f1fc  396c2414              cmp       [esp+14h],ebp
  0045f200  7506                  jne       short 0045F208h
  0045f202  66c746100200          mov       word [esi+10h],2

LAB_0045f208:
  0045f208  66395e10              cmp       [esi+10h],bx
  0045f20c  750c                  jne       short 0045F21Ah
  0045f20e  396c2414              cmp       [esp+14h],ebp
  0045f212  7506                  jne       short 0045F21Ah
  0045f214  66c746100800          mov       word [esi+10h],8

LAB_0045f21a:
  0045f21a  66837e1006            cmp       word [esi+10h],6
  0045f21f  b938ffffff            mov       ecx,0FFFFFF38h
  0045f224  752c                  jne       short 0045F252h
  0045f226  396e5c                cmp       [esi+5Ch],ebp
  0045f229  7527                  jne       short 0045F252h
  0045f22b  66014e28              add       [esi+28h],cx
  0045f22f  668b4628              mov       ax,[esi+28h]
  0045f233  66014e2a              add       [esi+2Ah],cx
  0045f237  66014e2c              add       [esi+2Ch],cx
  0045f23b  663bc5                cmp       ax,bp
  0045f23e  7f12                  jg        short 0045F252h
  0045f240  66896e28              mov       [esi+28h],bp
  0045f244  66896e2a              mov       [esi+2Ah],bp
  0045f248  66896e2c              mov       [esi+2Ch],bp
  0045f24c  66c746100800          mov       word [esi+10h],8

LAB_0045f252:
  0045f252  66837e1007            cmp       word [esi+10h],7
  0045f257  752c                  jne       short 0045F285h
  0045f259  396e5c                cmp       [esi+5Ch],ebp
  0045f25c  7527                  jne       short 0045F285h
  0045f25e  66014e28              add       [esi+28h],cx
  0045f262  668b4628              mov       ax,[esi+28h]
  0045f266  66014e2a              add       [esi+2Ah],cx
  0045f26a  66014e2c              add       [esi+2Ch],cx
  0045f26e  663bc5                cmp       ax,bp
  0045f271  7f12                  jg        short 0045F285h
  0045f273  66896e28              mov       [esi+28h],bp
  0045f277  66896e2a              mov       [esi+2Ah],bp
  0045f27b  66896e2c              mov       [esi+2Ch],bp
  0045f27f  66c746100200          mov       word [esi+10h],2

LAB_0045f285:
  0045f285  5f                    pop       edi
  0045f286  5e                    pop       esi
  0045f287  5d                    pop       ebp
  0045f288  5b                    pop       ebx
  0045f289  c3                    ret
  0045f28a  90                    nop
  0045f28b  90                    nop
  0045f28c  90                    nop
  0045f28d  90                    nop
  0045f28e  90                    nop
  0045f28f  90                    nop
  0045f290  53                    push      ebx
  0045f291  56                    push      esi
  0045f292  8b74240c              mov       esi,[esp+0Ch]
  0045f296  56                    push      esi
  0045f297  e8241d0000            call      00460FC0h
  0045f29c  83c404                add       esp,4
  0045f29f  66837e1000            cmp       word [esi+10h],0
  0045f2a4  b801000000            mov       eax,1
  0045f2a9  750a                  jne       short 0045F2B5h
  0045f2ab  66894610              mov       [esi+10h],ax
  0045f2af  66c746120000          mov       word [esi+12h],0

LAB_0045f2b5:
  0045f2b5  66394610              cmp       [esi+10h],ax
  0045f2b9  7554                  jne       short 0045F30Fh
  0045f2bb  668146120001          add       word [esi+12h],100h
  0045f2c1  0fbf4612              movsx     eax,word [esi+12h]
  0045f2c5  50                    push      eax
  0045f2c6  e851670e00            call      00545A1Ch
  0045f2cb  8bd8                  mov       ebx,eax
  0045f2cd  c1fb08                sar       ebx,8
  0045f2d0  e86b7e0e00            call      00547140h
  0045f2d5  99                    cdq
  0045f2d6  33c2                  xor       eax,edx
  0045f2d8  2bc2                  sub       eax,edx
  0045f2da  83e00f                and       eax,0Fh
  0045f2dd  33c2                  xor       eax,edx
  0045f2df  2bc2                  sub       eax,edx
  0045f2e1  2ac3                  sub       al,bl
  0045f2e3  0478                  add       al,78h
  0045f2e5  8844240c              mov       [esp+0Ch],al
  0045f2e9  8b44240c              mov       eax,[esp+0Ch]
  0045f2ed  25ff000000            and       eax,0FFh
  0045f2f2  8bc8                  mov       ecx,eax
  0045f2f4  c1e108                shl       ecx,8
  0045f2f7  0bc8                  or        ecx,eax
  0045f2f9  c1e108                shl       ecx,8
  0045f2fc  0bc8                  or        ecx,eax
  0045f2fe  66817e120010          cmp       word [esi+12h],1000h
  0045f304  894e38                mov       [esi+38h],ecx
  0045f307  7506                  jne       short 0045F30Fh
  0045f309  66c746120000          mov       word [esi+12h],0

LAB_0045f30f:
  0045f30f  5e                    pop       esi
  0045f310  5b                    pop       ebx
  0045f311  c3                    ret
  0045f312  90                    nop
  0045f313  90                    nop
  0045f314  90                    nop
  0045f315  90                    nop
  0045f316  90                    nop
  0045f317  90                    nop
  0045f318  90                    nop
  0045f319  90                    nop
  0045f31a  90                    nop
  0045f31b  90                    nop
  0045f31c  90                    nop
  0045f31d  90                    nop
  0045f31e  90                    nop
  0045f31f  90                    nop

27 cible(s) de saut interne : 0x0045ed1d, 0x0045ed59, 0x0045ed72, 0x0045edc3, 0x0045edef, 0x0045eeb7, 0x0045eece, 0x0045ef76, 0x0045ef91, 0x0045efdd, 0x0045efe2, 0x0045f005, 0x0045f056, 0x0045f09a, 0x0045f0c5, 0x0045f114, 0x0045f116, 0x0045f13e, 0x0045f190, 0x0045f1ce, 0x0045f1f5, 0x0045f208, 0x0045f21a, 0x0045f252, 0x0045f285, 0x0045f2b5, 0x0045f30f
