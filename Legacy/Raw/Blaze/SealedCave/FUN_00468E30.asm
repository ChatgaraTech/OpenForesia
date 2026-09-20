  00468e30  83ec14                sub       esp,14h
  00468e33  53                    push      ebx
  00468e34  55                    push      ebp
  00468e35  56                    push      esi
  00468e36  8b742424              mov       esi,[esp+24h]
  00468e3a  57                    push      edi
  00468e3b  8b4660                mov       eax,[esi+60h]
  00468e3e  8b4e64                mov       ecx,[esi+64h]
  00468e41  8b561c                mov       edx,[esi+1Ch]
  00468e44  66837e1000            cmp       word [esi+10h],0
  00468e49  8b6e7c                mov       ebp,[esi+7Ch]
  00468e4c  668bbe90000000        mov       di,[esi+90h]
  00468e53  89442428              mov       [esp+28h],eax
  00468e57  8b4620                mov       eax,[esi+20h]
  00468e5a  894c2410              mov       [esp+10h],ecx
  00468e5e  8b4e24                mov       ecx,[esi+24h]
  00468e61  895644                mov       [esi+44h],edx
  00468e64  8b9680000000          mov       edx,[esi+80h]
  00468e6a  894648                mov       [esi+48h],eax
  00468e6d  8b8684000000          mov       eax,[esi+84h]
  00468e73  894e4c                mov       [esi+4Ch],ecx
  00468e76  8b8e88000000          mov       ecx,[esi+88h]
  00468e7c  89542414              mov       [esp+14h],edx
  00468e80  8b968c000000          mov       edx,[esi+8Ch]
  00468e86  89442418              mov       [esp+18h],eax
  00468e8a  894c241c              mov       [esp+1Ch],ecx
  00468e8e  89542420              mov       [esp+20h],edx
  00468e92  755a                  jne       short 00468EEEh
  00468e94  66c746100100          mov       word [esi+10h],1
  00468e9a  e8a1e20d00            call      00547140h
  00468e9f  99                    cdq
  00468ea0  33c2                  xor       eax,edx
  00468ea2  2bc2                  sub       eax,edx
  00468ea4  83e001                and       eax,1
  00468ea7  33c2                  xor       eax,edx
  00468ea9  2bc2                  sub       eax,edx
  00468eab  8d5c00ff              lea       ebx,[eax+eax-1]
  00468eaf  e88ce20d00            call      00547140h
  00468eb4  99                    cdq
  00468eb5  b90a000000            mov       ecx,0Ah
  00468eba  f7f9                  idiv      ecx
  00468ebc  03d1                  add       edx,ecx
  00468ebe  0fafda                imul      ebx,edx
  00468ec1  66895e14              mov       [esi+14h],bx
  00468ec5  e876e20d00            call      00547140h
  00468eca  99                    cdq
  00468ecb  33c2                  xor       eax,edx
  00468ecd  66c786980000000000    mov       word [esi+98h],0
  00468ed6  2bc2                  sub       eax,edx
  00468ed8  83e001                and       eax,1
  00468edb  33c2                  xor       eax,edx
  00468edd  2bc2                  sub       eax,edx
  00468edf  8d5400ff              lea       edx,[eax+eax-1]
  00468ee3  8b06                  mov       eax,[esi]
  00468ee5  80cc20                or        ah,20h
  00468ee8  66895616              mov       [esi+16h],dx
  00468eec  8906                  mov       [esi],eax
  00468eee  66ff8698000000        inc       word [esi+98h]
  00468ef5  668b8698000000        mov       ax,[esi+98h]
  00468efc  663d0900              cmp       ax,9
  00468f00  7533                  jne       short 00468F35h
  00468f02  66c786980000000000    mov       word [esi+98h],0
  00468f0b  a1cc975e00            mov       eax,[5E97CCh]
  00468f10  6a00                  push      0
  00468f12  6a00                  push      0
  00468f14  8b88a8000000          mov       ecx,[eax+0A8h]
  00468f1a  6800080000            push      800h
  00468f1f  6800040000            push      400h
  00468f24  8b11                  mov       edx,[ecx]
  00468f26  660fb6422d            movzx     ax,byte [edx+2Dh]
  00468f2b  50                    push      eax
  00468f2c  56                    push      esi
  00468f2d  e84ef1ffff            call      00468080h
  00468f32  83c418                add       esp,18h
  00468f35  668b5616              mov       dx,[esi+16h]
  00468f39  8b4624                mov       eax,[esi+24h]
  00468f3c  0fbf4e14              movsx     ecx,word [esi+14h]
  00468f40  6669d2f000            imul      dx,0F0h
  00468f45  66015632              add       [esi+32h],dx
  00468f49  c1e10c                shl       ecx,0Ch
  00468f4c  03c1                  add       eax,ecx
  00468f4e  894624                mov       [esi+24h],eax
  00468f51  668b4632              mov       ax,[esi+32h]
  00468f55  663d0010              cmp       ax,1000h
  00468f59  7e06                  jle       short 00468F61h
  00468f5b  66c746320000          mov       word [esi+32h],0
  00468f61  66837e3200            cmp       word [esi+32h],0
  00468f66  7d06                  jge       short 00468F6Eh
  00468f68  66c746320010          mov       word [esi+32h],1000h
  00468f6e  56                    push      esi
  00468f6f  e8cca90c00            call      00533940h
  00468f74  8b8690000000          mov       eax,[esi+90h]
  00468f7a  83c404                add       esp,4
  00468f7d  a980080000            test      eax,880h
  00468f82  750e                  jne       short 00468F92h
  00468f84  8b467c                mov       eax,[esi+7Ch]
  00468f87  8a480f                mov       cl,[eax+0Fh]
  00468f8a  84c9                  test      cl,cl
  00468f8c  0f84a2000000          je        near 00469034h
  00468f92  8b4644                mov       eax,[esi+44h]
  00468f95  8b4c2428              mov       ecx,[esp+28h]
  00468f99  8b542410              mov       edx,[esp+10h]
  00468f9d  89461c                mov       [esi+1Ch],eax
  00468fa0  8b442418              mov       eax,[esp+18h]
  00468fa4  894e60                mov       [esi+60h],ecx
  00468fa7  8b4e4c                mov       ecx,[esi+4Ch]
  00468faa  895664                mov       [esi+64h],edx
  00468fad  8b542414              mov       edx,[esp+14h]
  00468fb1  898684000000          mov       [esi+84h],eax
  00468fb7  668b4614              mov       ax,[esi+14h]
  00468fbb  894e24                mov       [esi+24h],ecx
  00468fbe  8b4c241c              mov       ecx,[esp+1Ch]
  00468fc2  899680000000          mov       [esi+80h],edx
  00468fc8  8b542420              mov       edx,[esp+20h]
  00468fcc  81e7ffff0000          and       edi,0FFFFh
  00468fd2  66f7d8                neg       ax
  00468fd5  896e7c                mov       [esi+7Ch],ebp
  00468fd8  89be90000000          mov       [esi+90h],edi
  00468fde  898e88000000          mov       [esi+88h],ecx
  00468fe4  89968c000000          mov       [esi+8Ch],edx
  00468fea  66894614              mov       [esi+14h],ax
  00468fee  8b0dcc975e00          mov       ecx,[5E97CCh]
  00468ff4  6a00                  push      0
  00468ff6  6a00                  push      0
  00468ff8  8b91a8000000          mov       edx,[ecx+0A8h]
  00468ffe  6800100000            push      1000h
  00469003  6800040000            push      400h
  00469008  8b02                  mov       eax,[edx]
  0046900a  660fb64830            movzx     cx,byte [eax+30h]
  0046900f  51                    push      ecx
  00469010  56                    push      esi
  00469011  e86af0ffff            call      00468080h
  00469016  83c418                add       esp,18h
  00469019  bf08000000            mov       edi,8
  0046901e  6a00                  push      0
  00469020  6a00                  push      0
  00469022  6aff                  push      0FFFFFFFFh
  00469024  6a00                  push      0
  00469026  6a34                  push      34h
  00469028  56                    push      esi
  00469029  e8e2f0ffff            call      00468110h
  0046902e  83c418                add       esp,18h
  00469031  4f                    dec       edi
  00469032  75ea                  jne       short 0046901Eh
  00469034  e807e10d00            call      00547140h
  00469039  99                    cdq
  0046903a  b907000000            mov       ecx,7
  0046903f  f7f9                  idiv      ecx
  00469041  6683fa01              cmp       dx,1
  00469045  7513                  jne       short 0046905Ah
  00469047  6a00                  push      0
  00469049  6a00                  push      0
  0046904b  6aff                  push      0FFFFFFFFh
  0046904d  6a00                  push      0
  0046904f  6a35                  push      35h
  00469051  56                    push      esi
  00469052  e8b9f0ffff            call      00468110h
  00469057  83c418                add       esp,18h
  0046905a  c6460900              mov       byte [esi+9],0
  0046905e  33ed                  xor       ebp,ebp
  00469060  bfc0935e00            mov       edi,5E93C0h
  00469065  bb04000000            mov       ebx,4
  0046906a  8b07                  mov       eax,[edi]
  0046906c  a900000080            test      eax,80000000h
  00469071  745d                  je        short 004690D0h
  00469073  a900000050            test      eax,50000000h
  00469078  7556                  jne       short 004690D0h
  0046907a  8b471c                mov       eax,[edi+1Ch]
  0046907d  8b561c                mov       edx,[esi+1Ch]
  00469080  8b4f24                mov       ecx,[edi+24h]
  00469083  c1f80c                sar       eax,0Ch
  00469086  c1fa0c                sar       edx,0Ch
  00469089  2bc2                  sub       eax,edx
  0046908b  8b5624                mov       edx,[esi+24h]
  0046908e  c1f90c                sar       ecx,0Ch
  00469091  c1fa0c                sar       edx,0Ch
  00469094  2bca                  sub       ecx,edx
  00469096  8bd0                  mov       edx,eax
  00469098  0fafd0                imul      edx,eax
  0046909b  8bc1                  mov       eax,ecx
  0046909d  0fafc1                imul      eax,ecx
  004690a0  03d0                  add       edx,eax
  004690a2  52                    push      edx
  004690a3  e868c90d00            call      00545A10h
  004690a8  8b4f20                mov       ecx,[edi+20h]
  004690ab  8b5620                mov       edx,[esi+20h]
  004690ae  c1f90c                sar       ecx,0Ch
  004690b1  c1fa0c                sar       edx,0Ch
  004690b4  2bca                  sub       ecx,edx
  004690b6  83c146                add       ecx,46h
  004690b9  7902                  jns       short 004690BDh
  004690bb  f7d9                  neg       ecx
  004690bd  83f83c                cmp       eax,3Ch
  004690c0  7d0e                  jge       short 004690D0h
  004690c2  83f950                cmp       ecx,50h
  004690c5  7d09                  jge       short 004690D0h
  004690c7  b001                  mov       al,1
  004690c9  8bcd                  mov       ecx,ebp
  004690cb  d2e0                  shl       al,cl
  004690cd  084609                or        [esi+9],al
  004690d0  45                    inc       ebp
  004690d1  81c79c000000          add       edi,9Ch
  004690d7  4b                    dec       ebx
  004690d8  7590                  jne       short 0046906Ah
  004690da  6a00                  push      0
  004690dc  6a00                  push      0
  004690de  6a00                  push      0
  004690e0  6a0a                  push      0Ah
  004690e2  56                    push      esi
  004690e3  e85849ffff            call      0045DA40h
  004690e8  83c414                add       esp,14h
  004690eb  5f                    pop       edi
  004690ec  5e                    pop       esi
  004690ed  5d                    pop       ebp
  004690ee  5b                    pop       ebx
  004690ef  83c414                add       esp,14h
  004690f2  c3                    ret
