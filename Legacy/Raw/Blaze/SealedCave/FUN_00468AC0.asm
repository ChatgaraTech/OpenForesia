  00468ac0  83ec18                sub       esp,18h
  00468ac3  53                    push      ebx
  00468ac4  55                    push      ebp
  00468ac5  56                    push      esi
  00468ac6  8b742428              mov       esi,[esp+28h]
  00468aca  b800100000            mov       eax,1000h
  00468acf  57                    push      edi
  00468ad0  668b4e16              mov       cx,[esi+16h]
  00468ad4  2bc1                  sub       eax,ecx
  00468ad6  894c2414              mov       [esp+14h],ecx
  00468ada  89442418              mov       [esp+18h],eax
  00468ade  33c0                  xor       eax,eax
  00468ae0  66394610              cmp       [esi+10h],ax
  00468ae4  7538                  jne       short 00468B1Eh
  00468ae6  668b4e32              mov       cx,[esi+32h]
  00468aea  66c746100100          mov       word [esi+10h],1
  00468af0  0fbf5614              movsx     edx,word [esi+14h]
  00468af4  66894e12              mov       [esi+12h],cx
  00468af8  895644                mov       [esi+44h],edx
  00468afb  0fbf4e30              movsx     ecx,word [esi+30h]
  00468aff  894e48                mov       [esi+48h],ecx
  00468b02  66894630              mov       [esi+30h],ax
  00468b06  89464c                mov       [esi+4Ch],eax
  00468b09  c7465cffffffff        mov       dword [esi+5Ch],0FFFFFFFFh
  00468b10  6689869a000000        mov       [esi+9Ah],ax
  00468b17  66898698000000        mov       [esi+98h],ax
  00468b1e  8b4e5c                mov       ecx,[esi+5Ch]
  00468b21  83f9ff                cmp       ecx,0FFFFFFFFh
  00468b24  7507                  jne       short 00468B2Dh
  00468b26  66898698000000        mov       [esi+98h],ax
  00468b2d  668b4610              mov       ax,[esi+10h]
  00468b31  663d0100              cmp       ax,1
  00468b35  0f8530010000          jne       near 00468C6Bh
  00468b3b  8d542420              lea       edx,[esp+20h]
  00468b3f  c7465cffffffff        mov       dword [esi+5Ch],0FFFFFFFFh
  00468b46  8954242c              mov       [esp+2Ch],edx
  00468b4a  bdc0935e00            mov       ebp,5E93C0h
  00468b4f  c744241004000000      mov       dword [esp+10h],4
  00468b57  8b44242c              mov       eax,[esp+2Ch]
  00468b5b  66c7000f27            mov       word [eax],270Fh
  00468b60  8b4500                mov       eax,[ebp]
  00468b63  a900000080            test      eax,80000000h
  00468b68  7476                  je        short 00468BE0h
  00468b6a  a900000050            test      eax,50000000h
  00468b6f  756f                  jne       short 00468BE0h
  00468b71  8b7d1c                mov       edi,[ebp+1Ch]
  00468b74  8b461c                mov       eax,[esi+1Ch]
  00468b77  8b5d24                mov       ebx,[ebp+24h]
  00468b7a  8b5624                mov       edx,[esi+24h]
  00468b7d  2bf8                  sub       edi,eax
  00468b7f  2bda                  sub       ebx,edx
  00468b81  c1ff0c                sar       edi,0Ch
  00468b84  c1fb0c                sar       ebx,0Ch
  00468b87  8bcf                  mov       ecx,edi
  00468b89  8bd3                  mov       edx,ebx
  00468b8b  0fafcf                imul      ecx,edi
  00468b8e  0fafd3                imul      edx,ebx
  00468b91  03ca                  add       ecx,edx
  00468b93  51                    push      ecx
  00468b94  e877ce0d00            call      00545A10h
  00468b99  53                    push      ebx
  00468b9a  57                    push      edi
  00468b9b  89442424              mov       [esp+24h],eax
  00468b9f  e87ece0d00            call      00545A22h
  00468ba4  0fbf4e12              movsx     ecx,word [esi+12h]
  00468ba8  ba00080000            mov       edx,800h
  00468bad  2bd1                  sub       edx,ecx
  00468baf  8b4c241c              mov       ecx,[esp+1Ch]
  00468bb3  03c2                  add       eax,edx
  00468bb5  8b5644                mov       edx,[esi+44h]
  00468bb8  3bca                  cmp       ecx,edx
  00468bba  7d24                  jge       short 00468BE0h
  00468bbc  0fbf542414            movsx     edx,word [esp+14h]
  00468bc1  3bc2                  cmp       eax,edx
  00468bc3  7d04                  jge       short 00468BC9h
  00468bc5  85c0                  test      eax,eax
  00468bc7  7f10                  jg        short 00468BD9h
  00468bc9  3d00100000            cmp       eax,1000h
  00468bce  7d10                  jge       short 00468BE0h
  00468bd0  0fbf542418            movsx     edx,word [esp+18h]
  00468bd5  3bc2                  cmp       eax,edx
  00468bd7  7e07                  jle       short 00468BE0h
  00468bd9  8b44242c              mov       eax,[esp+2Ch]
  00468bdd  668908                mov       [eax],cx
  00468be0  8b4c242c              mov       ecx,[esp+2Ch]
  00468be4  8b442410              mov       eax,[esp+10h]
  00468be8  81c59c000000          add       ebp,9Ch
  00468bee  83c102                add       ecx,2
  00468bf1  48                    dec       eax
  00468bf2  894c242c              mov       [esp+2Ch],ecx
  00468bf6  89442410              mov       [esp+10h],eax
  00468bfa  0f8557ffffff          jne       near 00468B57h
  00468c00  ba0f270000            mov       edx,270Fh
  00468c05  8d4c2420              lea       ecx,[esp+20h]
  00468c09  bf04000000            mov       edi,4
  00468c0e  668b01                mov       ax,[ecx]
  00468c11  663bc2                cmp       ax,dx
  00468c14  7d02                  jge       short 00468C18h
  00468c16  8bd0                  mov       edx,eax
  00468c18  83c102                add       ecx,2
  00468c1b  4f                    dec       edi
  00468c1c  75f0                  jne       short 00468C0Eh
  00468c1e  6681fa0f27            cmp       dx,270Fh
  00468c23  751e                  jne       short 00468C43h
  00468c25  668b4632              mov       ax,[esi+32h]
  00468c29  0fbf4e12              movsx     ecx,word [esi+12h]
  00468c2d  0fbfd0                movsx     edx,ax
  00468c30  2bca                  sub       ecx,edx
  00468c32  c1f903                sar       ecx,3
  00468c35  03c8                  add       ecx,eax
  00468c37  66894e32              mov       [esi+32h],cx
  00468c3b  5f                    pop       edi
  00468c3c  5e                    pop       esi
  00468c3d  5d                    pop       ebp
  00468c3e  5b                    pop       ebx
  00468c3f  83c418                add       esp,18h
  00468c42  c3                    ret
  00468c43  33c0                  xor       eax,eax
  00468c45  8d4c2420              lea       ecx,[esp+20h]
  00468c49  bf04000000            mov       edi,4
  00468c4e  663911                cmp       [ecx],dx
  00468c51  7503                  jne       short 00468C56h
  00468c53  89465c                mov       [esi+5Ch],eax
  00468c56  40                    inc       eax
  00468c57  83c102                add       ecx,2
  00468c5a  4f                    dec       edi
  00468c5b  75f1                  jne       short 00468C4Eh
  00468c5d  66c746100200          mov       word [esi+10h],2
  00468c63  5f                    pop       edi
  00468c64  5e                    pop       esi
  00468c65  5d                    pop       ebp
  00468c66  5b                    pop       ebx
  00468c67  83c418                add       esp,18h
  00468c6a  c3                    ret
  00468c6b  663d0200              cmp       ax,2
  00468c6f  0f85ee000000          jne       near 00468D63h
  00468c75  8d0489                lea       eax,[ecx+ecx*4]
  00468c78  8b5624                mov       edx,[esi+24h]
  00468c7b  c1e003                shl       eax,3
  00468c7e  8b6e1c                mov       ebp,[esi+1Ch]
  00468c81  2bc1                  sub       eax,ecx
  00468c83  c1e002                shl       eax,2
  00468c86  8b98e4935e00          mov       ebx,[eax+5E93E4h]
  00468c8c  8bb8dc935e00          mov       edi,[eax+5E93DCh]
  00468c92  2bda                  sub       ebx,edx
  00468c94  2bfd                  sub       edi,ebp
  00468c96  c1fb0c                sar       ebx,0Ch
  00468c99  c1ff0c                sar       edi,0Ch
  00468c9c  53                    push      ebx
  00468c9d  57                    push      edi
  00468c9e  e87fcd0d00            call      00545A22h
  00468ca3  0fbf4e32              movsx     ecx,word [esi+32h]
  00468ca7  2bc1                  sub       eax,ecx
  00468ca9  8b4e48                mov       ecx,[esi+48h]
  00468cac  25ff0f0000            and       eax,0FFFh
  00468cb1  2d00080000            sub       eax,800h
  00468cb6  d3f8                  sar       eax,cl
  00468cb8  66014632              add       [esi+32h],ax
  00468cbc  668b5632              mov       dx,[esi+32h]
  00468cc0  668b464c              mov       ax,[esi+4Ch]
  00468cc4  662bc2                sub       ax,dx
  00468cc7  6685c0                test      ax,ax
  00468cca  0fbfc0                movsx     eax,ax
  00468ccd  7d02                  jge       short 00468CD1h
  00468ccf  f7d8                  neg       eax
  00468cd1  83f805                cmp       eax,5
  00468cd4  7d1a                  jge       short 00468CF0h
  00468cd6  6683be9a00000000      cmp       word [esi+9Ah],0
  00468cde  7523                  jne       short 00468D03h
  00468ce0  f6c5ff                test      ch,0FFh
  00468ce3  750b                  jne       short 00468CF0h
  00468ce5  66c7869a0000001e00    mov       word [esi+9Ah],1Eh
  00468cee  eb13                  jmp       short 00468D03h
  00468cf0  6683be9a00000000      cmp       word [esi+9Ah],0
  00468cf8  7509                  jne       short 00468D03h
  00468cfa  66c786980000000000    mov       word [esi+98h],0
  00468d03  53                    push      ebx
  00468d04  57                    push      edi
  00468d05  e818cd0d00            call      00545A22h
  00468d0a  0fbf5612              movsx     edx,word [esi+12h]
  00468d0e  8be8                  mov       ebp,eax
  00468d10  b800080000            mov       eax,800h
  00468d15  2bc2                  sub       eax,edx
  00468d17  8bcf                  mov       ecx,edi
  00468d19  8bd3                  mov       edx,ebx
  00468d1b  0fafcf                imul      ecx,edi
  00468d1e  0fafd3                imul      edx,ebx
  00468d21  03ca                  add       ecx,edx
  00468d23  03e8                  add       ebp,eax
  00468d25  51                    push      ecx
  00468d26  e8e5cc0d00            call      00545A10h
  00468d2b  3b4644                cmp       eax,[esi+44h]
  00468d2e  7f1d                  jg        short 00468D4Dh
  00468d30  0fbf442414            movsx     eax,word [esp+14h]
  00468d35  052c010000            add       eax,12Ch
  00468d3a  3be8                  cmp       ebp,eax
  00468d3c  7e1e                  jle       short 00468D5Ch
  00468d3e  0fbf4c2418            movsx     ecx,word [esp+18h]
  00468d43  81e92c010000          sub       ecx,12Ch
  00468d49  3be9                  cmp       ebp,ecx
  00468d4b  7d0f                  jge       short 00468D5Ch
  00468d4d  66c746100100          mov       word [esi+10h],1
  00468d53  66c7869a0000000000    mov       word [esi+9Ah],0
  00468d5c  0fbf5632              movsx     edx,word [esi+32h]
  00468d60  89564c                mov       [esi+4Ch],edx
  00468d63  668b869a000000        mov       ax,[esi+9Ah]
  00468d6a  6685c0                test      ax,ax
  00468d6d  0f8eaf000000          jle       near 00468E22h
  00468d73  8b4e5c                mov       ecx,[esi+5Ch]
  00468d76  48                    dec       eax
  00468d77  6689869a000000        mov       [esi+9Ah],ax
  00468d7e  8d0489                lea       eax,[ecx+ecx*4]
  00468d81  c1e003                shl       eax,3
  00468d84  2bc1                  sub       eax,ecx
  00468d86  c1e002                shl       eax,2
  00468d89  8b88dc935e00          mov       ecx,[eax+5E93DCh]
  00468d8f  894e50                mov       [esi+50h],ecx
  00468d92  8b4e20                mov       ecx,[esi+20h]
  00468d95  8b90e0935e00          mov       edx,[eax+5E93E0h]
  00468d9b  c1f90c                sar       ecx,0Ch
  00468d9e  895654                mov       [esi+54h],edx
  00468da1  8b80e4935e00          mov       eax,[eax+5E93E4h]
  00468da7  81c1b4000000          add       ecx,0B4h
  00468dad  894658                mov       [esi+58h],eax
  00468db0  c1fa0c                sar       edx,0Ch
  00468db3  3bca                  cmp       ecx,edx
  00468db5  7e52                  jle       short 00468E09h
  00468db7  6a00                  push      0
  00468db9  6a00                  push      0
  00468dbb  6aff                  push      0FFFFFFFFh
  00468dbd  6a00                  push      0
  00468dbf  6a31                  push      31h
  00468dc1  56                    push      esi
  00468dc2  e849f3ffff            call      00468110h
  00468dc7  83c418                add       esp,18h
  00468dca  6683be9800000000      cmp       word [esi+98h],0
  00468dd2  753e                  jne       short 00468E12h
  00468dd4  a1cc975e00            mov       eax,[5E97CCh]
  00468dd9  6a00                  push      0
  00468ddb  6a00                  push      0
  00468ddd  6800100000            push      1000h
  00468de2  8b88a8000000          mov       ecx,[eax+0A8h]
  00468de8  6800080000            push      800h
  00468ded  8b11                  mov       edx,[ecx]
  00468def  660fb64228            movzx     ax,byte [edx+28h]
  00468df4  50                    push      eax
  00468df5  56                    push      esi
  00468df6  e885f2ffff            call      00468080h
  00468dfb  83c418                add       esp,18h
  00468dfe  66c786980000000100    mov       word [esi+98h],1
  00468e07  eb09                  jmp       short 00468E12h
  00468e09  66c7869a0000000000    mov       word [esi+9Ah],0
  00468e12  6683be9a00000000      cmp       word [esi+9Ah],0
  00468e1a  7506                  jne       short 00468E22h
  00468e1c  66c746100100          mov       word [esi+10h],1
  00468e22  5f                    pop       edi
  00468e23  5e                    pop       esi
  00468e24  5d                    pop       ebp
  00468e25  5b                    pop       ebx
  00468e26  83c418                add       esp,18h
  00468e29  c3                    ret
