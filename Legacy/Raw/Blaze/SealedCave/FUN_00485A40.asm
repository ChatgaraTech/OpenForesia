  00485a40  83ec18                sub       esp,18h
  00485a43  53                    push      ebx
  00485a44  55                    push      ebp
  00485a45  56                    push      esi
  00485a46  8b742428              mov       esi,[esp+28h]
  00485a4a  57                    push      edi
  00485a4b  66c744241000fc        mov       word [esp+10h],0FC00h
  00485a52  66837e1004            cmp       word [esi+10h],4
  00485a57  66c7442412e2ff        mov       word [esp+12h],0FFE2h
  00485a5e  66c74424141e00        mov       word [esp+14h],1Eh
  00485a65  66c7442416c800        mov       word [esp+16h],0C8h
  00485a6c  66c74424180000        mov       word [esp+18h],0
  00485a73  66c744241a0401        mov       word [esp+1Ah],104h
  00485a7a  0f85c1000000          jne       near 00485B41h
  00485a80  668b4612              mov       ax,[esi+12h]
  00485a84  6685c0                test      ax,ax
  00485a87  0f84b4000000          je        near 00485B41h
  00485a8d  0fbf6e32              movsx     ebp,word [esi+32h]
  00485a91  8b4e20                mov       ecx,[esi+20h]
  00485a94  8b5624                mov       edx,[esi+24h]
  00485a97  48                    dec       eax
  00485a98  8dbd00fcffff          lea       edi,[ebp-400h]
  00485a9e  66894612              mov       [esi+12h],ax
  00485aa2  8b461c                mov       eax,[esi+1Ch]
  00485aa5  57                    push      edi
  00485aa6  89442420              mov       [esp+20h],eax
  00485aaa  894c2424              mov       [esp+24h],ecx
  00485aae  89542428              mov       [esp+28h],edx
  00485ab2  e865ff0b00            call      00545A1Ch
  00485ab7  8b4e1c                mov       ecx,[esi+1Ch]
  00485aba  8d0480                lea       eax,[eax+eax*4]
  00485abd  57                    push      edi
  00485abe  8d0480                lea       eax,[eax+eax*4]
  00485ac1  c1e003                shl       eax,3
  00485ac4  03c8                  add       ecx,eax
  00485ac6  894e1c                mov       [esi+1Ch],ecx
  00485ac9  e848ff0b00            call      00545A16h
  00485ace  8d0480                lea       eax,[eax+eax*4]
  00485ad1  bf02000000            mov       edi,2
  00485ad6  0fbf5e32              movsx     ebx,word [esi+32h]
  00485ada  8d0c80                lea       ecx,[eax+eax*4]
  00485add  8b4624                mov       eax,[esi+24h]
  00485ae0  c1e103                shl       ecx,3
  00485ae3  03c1                  add       eax,ecx
  00485ae5  894624                mov       [esi+24h],eax
  00485ae8  e853160c00            call      00547140h
  00485aed  25ff010000            and       eax,1FFh
  00485af2  83ff02                cmp       edi,2
  00485af5  7402                  je        short 00485AF9h
  00485af7  f7d8                  neg       eax
  00485af9  8d1403                lea       edx,[ebx+eax]
  00485afc  6a01                  push      1
  00485afe  66895632              mov       [esi+32h],dx
  00485b02  a1cc975e00            mov       eax,[5E97CCh]
  00485b07  6a01                  push      1
  00485b09  8b88d0000000          mov       ecx,[eax+0D0h]
  00485b0f  33c0                  xor       eax,eax
  00485b11  8b11                  mov       edx,[ecx]
  00485b13  8a4203                mov       al,[edx+3]
  00485b16  05c0000000            add       eax,0C0h
  00485b1b  50                    push      eax
  00485b1c  56                    push      esi
  00485b1d  e8be4fffff            call      0047AAE0h
  00485b22  83c410                add       esp,10h
  00485b25  4f                    dec       edi
  00485b26  75c0                  jne       short 00485AE8h
  00485b28  8b4c241c              mov       ecx,[esp+1Ch]
  00485b2c  8b542420              mov       edx,[esp+20h]
  00485b30  8b442424              mov       eax,[esp+24h]
  00485b34  894e1c                mov       [esi+1Ch],ecx
  00485b37  895620                mov       [esi+20h],edx
  00485b3a  894624                mov       [esi+24h],eax
  00485b3d  66896e32              mov       [esi+32h],bp
  00485b41  66837e1014            cmp       word [esi+10h],14h
  00485b46  8b4638                mov       eax,[esi+38h]
  00485b49  0f8d87000000          jge       near 00485BD6h
  00485b4f  3d80808000            cmp       eax,808080h
  00485b54  7314                  jae       short 00485B6Ah
  00485b56  0508080800            add       eax,80808h
  00485b5b  66ff4610              inc       word [esi+10h]
  00485b5f  894638                mov       [esi+38h],eax
  00485b62  5f                    pop       edi
  00485b63  5e                    pop       esi
  00485b64  5d                    pop       ebp
  00485b65  5b                    pop       ebx
  00485b66  83c418                add       esp,18h
  00485b69  c3                    ret
  00485b6a  668b462a              mov       ax,[esi+2Ah]
  00485b6e  ba00300000            mov       edx,3000h
  00485b73  0fbfc8                movsx     ecx,ax
  00485b76  2bd1                  sub       edx,ecx
  00485b78  c1fa02                sar       edx,2
  00485b7b  03d0                  add       edx,eax
  00485b7d  8d442410              lea       eax,[esp+10h]
  00485b81  50                    push      eax
  00485b82  56                    push      esi
  00485b83  6689562a              mov       [esi+2Ah],dx
  00485b87  e814f1feff            call      00474CA0h
  00485b8c  8bf8                  mov       edi,eax
  00485b8e  83c408                add       esp,8
  00485b91  85ff                  test      edi,edi
  00485b93  7453                  je        short 00485BE8h
  00485b95  8b4e74                mov       ecx,[esi+74h]
  00485b98  57                    push      edi
  00485b99  51                    push      ecx
  00485b9a  e8e13f0b00            call      00539B80h
  00485b9f  8b2f                  mov       ebp,[edi]
  00485ba1  83c408                add       esp,8
  00485ba4  8d561c                lea       edx,[esi+1Ch]
  00485ba7  83cd40                or        ebp,40h
  00485baa  52                    push      edx
  00485bab  6800180000            push      1800h
  00485bb0  6800040000            push      400h
  00485bb5  6a03                  push      3
  00485bb7  66c787960000000800    mov       word [edi+96h],8
  00485bc0  892f                  mov       [edi],ebp
  00485bc2  e8597f0b00            call      0053DB20h
  00485bc7  83c410                add       esp,10h
  00485bca  66ff4610              inc       word [esi+10h]
  00485bce  5f                    pop       edi
  00485bcf  5e                    pop       esi
  00485bd0  5d                    pop       ebp
  00485bd1  5b                    pop       ebx
  00485bd2  83c418                add       esp,18h
  00485bd5  c3                    ret
  00485bd6  85c0                  test      eax,eax
  00485bd8  740e                  je        short 00485BE8h
  00485bda  05f8f7f7ff            add       eax,0FFF7F7F8h
  00485bdf  85c0                  test      eax,eax
  00485be1  894638                mov       [esi+38h],eax
  00485be4  7502                  jne       short 00485BE8h
  00485be6  8906                  mov       [esi],eax
  00485be8  66ff4610              inc       word [esi+10h]
  00485bec  5f                    pop       edi
  00485bed  5e                    pop       esi
  00485bee  5d                    pop       ebp
  00485bef  5b                    pop       ebx
  00485bf0  83c418                add       esp,18h
  00485bf3  c3                    ret
