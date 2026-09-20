  0046ba50  83ec14                sub       esp,14h
  0046ba53  53                    push      ebx
  0046ba54  55                    push      ebp
  0046ba55  56                    push      esi
  0046ba56  8b742424              mov       esi,[esp+24h]
  0046ba5a  57                    push      edi
  0046ba5b  56                    push      esi
  0046ba5c  e85f55ffff            call      00460FC0h
  0046ba61  8b4660                mov       eax,[esi+60h]
  0046ba64  8b4e64                mov       ecx,[esi+64h]
  0046ba67  8b561c                mov       edx,[esi+1Ch]
  0046ba6a  8b6e7c                mov       ebp,[esi+7Ch]
  0046ba6d  668bbe90000000        mov       di,[esi+90h]
  0046ba74  8944242c              mov       [esp+2Ch],eax
  0046ba78  8b4620                mov       eax,[esi+20h]
  0046ba7b  894c2414              mov       [esp+14h],ecx
  0046ba7f  8b4e24                mov       ecx,[esi+24h]
  0046ba82  895644                mov       [esi+44h],edx
  0046ba85  8b9680000000          mov       edx,[esi+80h]
  0046ba8b  894648                mov       [esi+48h],eax
  0046ba8e  8b8684000000          mov       eax,[esi+84h]
  0046ba94  894e4c                mov       [esi+4Ch],ecx
  0046ba97  8b8e88000000          mov       ecx,[esi+88h]
  0046ba9d  89542418              mov       [esp+18h],edx
  0046baa1  8b968c000000          mov       edx,[esi+8Ch]
  0046baa7  33db                  xor       ebx,ebx
  0046baa9  83c404                add       esp,4
  0046baac  66395e10              cmp       [esi+10h],bx
  0046bab0  89442418              mov       [esp+18h],eax
  0046bab4  894c241c              mov       [esp+1Ch],ecx
  0046bab8  89542420              mov       [esp+20h],edx
  0046babc  0f8580000000          jne       near 0046BB42h
  0046bac2  66c746100100          mov       word [esi+10h],1
  0046bac8  e873b60d00            call      00547140h
  0046bacd  99                    cdq
  0046bace  33c2                  xor       eax,edx
  0046bad0  2bc2                  sub       eax,edx
  0046bad2  83e001                and       eax,1
  0046bad5  33c2                  xor       eax,edx
  0046bad7  2bc2                  sub       eax,edx
  0046bad9  8d5c00ff              lea       ebx,[eax+eax-1]
  0046badd  e85eb60d00            call      00547140h
  0046bae2  99                    cdq
  0046bae3  b90d000000            mov       ecx,0Dh
  0046bae8  f7f9                  idiv      ecx
  0046baea  83c205                add       edx,5
  0046baed  0fafda                imul      ebx,edx
  0046baf0  66895e12              mov       [esi+12h],bx
  0046baf4  e847b60d00            call      00547140h
  0046baf9  99                    cdq
  0046bafa  33c2                  xor       eax,edx
  0046bafc  2bc2                  sub       eax,edx
  0046bafe  83e001                and       eax,1
  0046bb01  33c2                  xor       eax,edx
  0046bb03  2bc2                  sub       eax,edx
  0046bb05  8d5c00ff              lea       ebx,[eax+eax-1]
  0046bb09  e832b60d00            call      00547140h
  0046bb0e  99                    cdq
  0046bb0f  b90d000000            mov       ecx,0Dh
  0046bb14  f7f9                  idiv      ecx
  0046bb16  83c205                add       edx,5
  0046bb19  0fafda                imul      ebx,edx
  0046bb1c  66895e14              mov       [esi+14h],bx
  0046bb20  e81bb60d00            call      00547140h
  0046bb25  99                    cdq
  0046bb26  33c2                  xor       eax,edx
  0046bb28  2bc2                  sub       eax,edx
  0046bb2a  83e001                and       eax,1
  0046bb2d  33c2                  xor       eax,edx
  0046bb2f  2bc2                  sub       eax,edx
  0046bb31  33db                  xor       ebx,ebx
  0046bb33  66899e98000000        mov       [esi+98h],bx
  0046bb3a  8d5400ff              lea       edx,[eax+eax-1]
  0046bb3e  66895616              mov       [esi+16h],dx
  0046bb42  66ff8698000000        inc       word [esi+98h]
  0046bb49  668b8698000000        mov       ax,[esi+98h]
  0046bb50  663d0a00              cmp       ax,0Ah
  0046bb54  752f                  jne       short 0046BB85h
  0046bb56  a1cc975e00            mov       eax,[5E97CCh]
  0046bb5b  53                    push      ebx
  0046bb5c  53                    push      ebx
  0046bb5d  6800100000            push      1000h
  0046bb62  8b88a8000000          mov       ecx,[eax+0A8h]
  0046bb68  6800010000            push      100h
  0046bb6d  8b11                  mov       edx,[ecx]
  0046bb6f  660fb64212            movzx     ax,byte [edx+12h]
  0046bb74  50                    push      eax
  0046bb75  56                    push      esi
  0046bb76  e805c5ffff            call      00468080h
  0046bb7b  83c418                add       esp,18h
  0046bb7e  66899e98000000        mov       [esi+98h],bx
  0046bb85  0fbf4612              movsx     eax,word [esi+12h]
  0046bb89  8b561c                mov       edx,[esi+1Ch]
  0046bb8c  8bc8                  mov       ecx,eax
  0046bb8e  c1e10c                shl       ecx,0Ch
  0046bb91  03d1                  add       edx,ecx
  0046bb93  0fbf4e14              movsx     ecx,word [esi+14h]
  0046bb97  89561c                mov       [esi+1Ch],edx
  0046bb9a  8bd1                  mov       edx,ecx
  0046bb9c  c1e10c                shl       ecx,0Ch
  0046bb9f  51                    push      ecx
  0046bba0  53                    push      ebx
  0046bba1  c1e00c                shl       eax,0Ch
  0046bba4  c1e20c                shl       edx,0Ch
  0046bba7  015624                add       [esi+24h],edx
  0046bbaa  50                    push      eax
  0046bbab  56                    push      esi
  0046bbac  e8cf89feff            call      00454580h
  0046bbb1  668b4616              mov       ax,[esi+16h]
  0046bbb5  83c410                add       esp,10h
  0046bbb8  66c1e007              shl       ax,7
  0046bbbc  66014632              add       [esi+32h],ax
  0046bbc0  b800100000            mov       eax,1000h
  0046bbc5  66394632              cmp       [esi+32h],ax
  0046bbc9  7e04                  jle       short 0046BBCFh
  0046bbcb  66895e32              mov       [esi+32h],bx
  0046bbcf  66395e32              cmp       [esi+32h],bx
  0046bbd3  7d04                  jge       short 0046BBD9h
  0046bbd5  66894632              mov       [esi+32h],ax
  0046bbd9  56                    push      esi
  0046bbda  e8617d0c00            call      00533940h
  0046bbdf  8b8690000000          mov       eax,[esi+90h]
  0046bbe5  83c404                add       esp,4
  0046bbe8  a980080000            test      eax,880h
  0046bbed  750c                  jne       short 0046BBFBh
  0046bbef  8b4e7c                mov       ecx,[esi+7Ch]
  0046bbf2  38590f                cmp       [ecx+0Fh],bl
  0046bbf5  0f84b4000000          je        near 0046BCAFh
  0046bbfb  8b542428              mov       edx,[esp+28h]
  0046bbff  8b442410              mov       eax,[esp+10h]
  0046bc03  8b4e44                mov       ecx,[esi+44h]
  0046bc06  895660                mov       [esi+60h],edx
  0046bc09  8b564c                mov       edx,[esi+4Ch]
  0046bc0c  894664                mov       [esi+64h],eax
  0046bc0f  8b442414              mov       eax,[esp+14h]
  0046bc13  894e1c                mov       [esi+1Ch],ecx
  0046bc16  8b4c2418              mov       ecx,[esp+18h]
  0046bc1a  895624                mov       [esi+24h],edx
  0046bc1d  8b54241c              mov       edx,[esp+1Ch]
  0046bc21  898680000000          mov       [esi+80h],eax
  0046bc27  8b442420              mov       eax,[esp+20h]
  0046bc2b  81e7ffff0000          and       edi,0FFFFh
  0046bc31  896e7c                mov       [esi+7Ch],ebp
  0046bc34  89be90000000          mov       [esi+90h],edi
  0046bc3a  898e84000000          mov       [esi+84h],ecx
  0046bc40  899688000000          mov       [esi+88h],edx
  0046bc46  89868c000000          mov       [esi+8Ch],eax
  0046bc4c  e8efb40d00            call      00547140h
  0046bc51  99                    cdq
  0046bc52  33c2                  xor       eax,edx
  0046bc54  2bc2                  sub       eax,edx
  0046bc56  83e001                and       eax,1
  0046bc59  33c2                  xor       eax,edx
  0046bc5b  2bc2                  sub       eax,edx
  0046bc5d  8d7c00ff              lea       edi,[eax+eax-1]
  0046bc61  e8dab40d00            call      00547140h
  0046bc66  99                    cdq
  0046bc67  b90d000000            mov       ecx,0Dh
  0046bc6c  f7f9                  idiv      ecx
  0046bc6e  83c205                add       edx,5
  0046bc71  0faffa                imul      edi,edx
  0046bc74  66897e12              mov       [esi+12h],di
  0046bc78  e8c3b40d00            call      00547140h
  0046bc7d  99                    cdq
  0046bc7e  33c2                  xor       eax,edx
  0046bc80  2bc2                  sub       eax,edx
  0046bc82  83e001                and       eax,1
  0046bc85  33c2                  xor       eax,edx
  0046bc87  2bc2                  sub       eax,edx
  0046bc89  8d7c00ff              lea       edi,[eax+eax-1]
  0046bc8d  e8aeb40d00            call      00547140h
  0046bc92  99                    cdq
  0046bc93  b90d000000            mov       ecx,0Dh
  0046bc98  f7f9                  idiv      ecx
  0046bc9a  83c205                add       edx,5
  0046bc9d  0faffa                imul      edi,edx
  0046bca0  668b5616              mov       dx,[esi+16h]
  0046bca4  66897e14              mov       [esi+14h],di
  0046bca8  66f7da                neg       dx
  0046bcab  66895616              mov       [esi+16h],dx
  0046bcaf  53                    push      ebx
  0046bcb0  53                    push      ebx
  0046bcb1  53                    push      ebx
  0046bcb2  6a0a                  push      0Ah
  0046bcb4  56                    push      esi
  0046bcb5  e8861dffff            call      0045DA40h
  0046bcba  83c414                add       esp,14h
  0046bcbd  5f                    pop       edi
  0046bcbe  5e                    pop       esi
  0046bcbf  5d                    pop       ebp
  0046bcc0  5b                    pop       ebx
  0046bcc1  83c414                add       esp,14h
  0046bcc4  c3                    ret
