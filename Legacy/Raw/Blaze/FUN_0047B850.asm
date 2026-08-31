
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b850  section .text  offset fichier 0x7ac50  700 octets

  0047b850  83ec0c                sub       esp,0Ch
  0047b853  53                    push      ebx
  0047b854  55                    push      ebp
  0047b855  56                    push      esi
  0047b856  8b74241c              mov       esi,[esp+1Ch]
  0047b85a  33c9                  xor       ecx,ecx
  0047b85c  b864000000            mov       eax,64h
  0047b861  66394e10              cmp       [esi+10h],cx
  0047b865  57                    push      edi
  0047b866  8b7e74                mov       edi,[esi+74h]
  0047b869  66894c2410            mov       [esp+10h],cx
  0047b86e  66c7442412ceff        mov       word [esp+12h],0FFCEh
  0047b875  66c74424143200        mov       word [esp+14h],32h
  0047b87c  6689442416            mov       [esp+16h],ax
  0047b881  66894c2418            mov       [esp+18h],cx
  0047b886  668944241a            mov       [esp+1Ah],ax
  0047b88b  0f859b000000          jne       near 0047B92Ch
  0047b891  8b16                  mov       edx,[esi]
  0047b893  80ca80                or        dl,80h
  0047b896  8916                  mov       [esi],edx
  0047b898  e8a3b80c00            call      00547140h
  0047b89d  8bd8                  mov       ebx,eax
  0047b89f  66895e32              mov       [esi+32h],bx
  0047b8a3  e898b80c00            call      00547140h
  0047b8a8  66894634              mov       [esi+34h],ax
  0047b8ac  8b4760                mov       eax,[edi+60h]
  0047b8af  40                    inc       eax
  0047b8b0  53                    push      ebx
  0047b8b1  894620                mov       [esi+20h],eax
  0047b8b4  8d6e1c                lea       ebp,[esi+1Ch]
  0047b8b7  e860a10c00            call      00545A1Ch
  0047b8bc  8b571c                mov       edx,[edi+1Ch]
  0047b8bf  8d0480                lea       eax,[eax+eax*4]
  0047b8c2  53                    push      ebx
  0047b8c3  8d0cc0                lea       ecx,[eax+eax*8]
  0047b8c6  8d044a                lea       eax,[edx+ecx*2]
  0047b8c9  894500                mov       [ebp],eax
  0047b8cc  e845a10c00            call      00545A16h
  0047b8d1  8b5724                mov       edx,[edi+24h]
  0047b8d4  8d0480                lea       eax,[eax+eax*4]
  0047b8d7  8d0cc0                lea       ecx,[eax+eax*8]
  0047b8da  8d044a                lea       eax,[edx+ecx*2]
  0047b8dd  894624                mov       [esi+24h],eax
  0047b8e0  e85bb80c00            call      00547140h
  0047b8e5  25ff010000            and       eax,1FFh
  0047b8ea  33d2                  xor       edx,edx
  0047b8ec  8a560b                mov       dl,[esi+0Bh]
  0047b8ef  0500060000            add       eax,600h
  0047b8f4  8bc8                  mov       ecx,eax
  0047b8f6  6aff                  push      0FFFFFFFFh
  0047b8f8  52                    push      edx
  0047b8f9  6a4b                  push      4Bh
  0047b8fb  c1e110                shl       ecx,10h
  0047b8fe  68981a5500            push      551A98h
  0047b903  0bc8                  or        ecx,eax
  0047b905  55                    push      ebp
  0047b906  56                    push      esi
  0047b907  894e28                mov       [esi+28h],ecx
  0047b90a  6689462c              mov       [esi+2Ch],ax
  0047b90e  c74638ffffff02        mov       dword [esi+38h],2FFFFFFh
  0047b915  c646041b              mov       byte [esi+4],1Bh
  0047b919  e822f3ffff            call      0047AC40h
  0047b91e  83c418                add       esp,18h
  0047b921  66c746120002          mov       word [esi+12h],200h
  0047b927  e9cb000000            jmp       0047B9F7h

LAB_0047b92c:
  0047b92c  668b4612              mov       ax,[esi+12h]
  0047b930  663d1000              cmp       ax,10h
  0047b934  0f8cb4000000          jl        near 0047B9EEh
  0047b93a  668bc8                mov       cx,ax
  0047b93d  66c1f903              sar       cx,3
  0047b941  2bc1                  sub       eax,ecx
  0047b943  66014632              add       [esi+32h],ax
  0047b947  66894612              mov       [esi+12h],ax
  0047b94b  0fbf5e32              movsx     ebx,word [esi+32h]
  0047b94f  53                    push      ebx
  0047b950  e8c7a00c00            call      00545A1Ch
  0047b955  8d0480                lea       eax,[eax+eax*4]
  0047b958  53                    push      ebx
  0047b959  8d14c0                lea       edx,[eax+eax*8]
  0047b95c  8b471c                mov       eax,[edi+1Ch]
  0047b95f  8d0c50                lea       ecx,[eax+edx*2]
  0047b962  894e1c                mov       [esi+1Ch],ecx
  0047b965  e8aca00c00            call      00545A16h
  0047b96a  8d0480                lea       eax,[eax+eax*4]
  0047b96d  8d14c0                lea       edx,[eax+eax*8]
  0047b970  8b4724                mov       eax,[edi+24h]
  0047b973  8d0c50                lea       ecx,[eax+edx*2]
  0047b976  894e24                mov       [esi+24h],ecx
  0047b979  8b4e20                mov       ecx,[esi+20h]
  0047b97c  81c100700000          add       ecx,7000h
  0047b982  66837e1210            cmp       word [esi+12h],10h
  0047b987  894e20                mov       [esi+20h],ecx
  0047b98a  7d6b                  jge       short 0047B9F7h
  0047b98c  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0047b995  e8a6b70c00            call      00547140h
  0047b99a  0fbf5732              movsx     edx,word [edi+32h]
  0047b99e  25ff010000            and       eax,1FFh
  0047b9a3  8dbc1000ffffff        lea       edi,[eax+edx-100h]
  0047b9aa  57                    push      edi
  0047b9ab  e86ca00c00            call      00545A1Ch
  0047b9b0  8bd8                  mov       ebx,eax
  0047b9b2  e889b70c00            call      00547140h
  0047b9b7  83e01f                and       eax,1Fh
  0047b9ba  57                    push      edi
  0047b9bb  83c032                add       eax,32h
  0047b9be  0fafd8                imul      ebx,eax
  0047b9c1  895e50                mov       [esi+50h],ebx
  0047b9c4  e84da00c00            call      00545A16h
  0047b9c9  8bf8                  mov       edi,eax
  0047b9cb  e870b70c00            call      00547140h
  0047b9d0  83e01f                and       eax,1Fh
  0047b9d3  83c032                add       eax,32h
  0047b9d6  0faff8                imul      edi,eax
  0047b9d9  8b466c                mov       eax,[esi+6Ch]
  0047b9dc  897e58                mov       [esi+58h],edi
  0047b9df  66ff4814              dec       word [eax+14h]
  0047b9e3  8b06                  mov       eax,[esi]
  0047b9e5  0d00000002            or        eax,2000000h
  0047b9ea  8906                  mov       [esi],eax
  0047b9ec  eb09                  jmp       short 0047B9F7h

LAB_0047b9ee:
  0047b9ee  56                    push      esi
  0047b9ef  e88c9e0b00            call      00535880h
  0047b9f4  83c404                add       esp,4

LAB_0047b9f7:
  0047b9f7  56                    push      esi
  0047b9f8  e8437f0b00            call      00533940h
  0047b9fd  8b8690000000          mov       eax,[esi+90h]
  0047ba03  83c404                add       esp,4
  0047ba06  f6c4a0                test      ah,0A0h
  0047ba09  756d                  jne       short 0047BA78h
  0047ba0b  a902080000            test      eax,802h
  0047ba10  750a                  jne       short 0047BA1Ch
  0047ba12  8b4620                mov       eax,[esi+20h]
  0047ba15  8b4e60                mov       ecx,[esi+60h]
  0047ba18  3bc1                  cmp       eax,ecx
  0047ba1a  7f5c                  jg        short 0047BA78h

LAB_0047ba1c:
  0047ba1c  8b4e60                mov       ecx,[esi+60h]
  0047ba1f  33d2                  xor       edx,edx
  0047ba21  8a560b                mov       dl,[esi+0Bh]
  0047ba24  6aff                  push      0FFFFFFFFh
  0047ba26  52                    push      edx
  0047ba27  8d7e1c                lea       edi,[esi+1Ch]
  0047ba2a  6a4b                  push      4Bh
  0047ba2c  68981a5500            push      551A98h
  0047ba31  57                    push      edi
  0047ba32  56                    push      esi
  0047ba33  894e20                mov       [esi+20h],ecx
  0047ba36  e805f2ffff            call      0047AC40h
  0047ba3b  8b06                  mov       eax,[esi]
  0047ba3d  83c418                add       esp,18h
  0047ba40  a900000002            test      eax,2000000h
  0047ba45  7507                  jne       short 0047BA4Eh
  0047ba47  8b466c                mov       eax,[esi+6Ch]
  0047ba4a  66ff4814              dec       word [eax+14h]

LAB_0047ba4e:
  0047ba4e  8b466c                mov       eax,[esi+6Ch]
  0047ba51  57                    push      edi
  0047ba52  6800120000            push      1200h
  0047ba57  68aa040000            push      4AAh
  0047ba5c  66ff4816              dec       word [eax+16h]
  0047ba60  6a04                  push      4
  0047ba62  c70600000000          mov       dword [esi],0
  0047ba68  e8b3200c00            call      0053DB20h
  0047ba6d  83c410                add       esp,10h
  0047ba70  5f                    pop       edi
  0047ba71  5e                    pop       esi
  0047ba72  5d                    pop       ebp
  0047ba73  5b                    pop       ebx
  0047ba74  83c40c                add       esp,0Ch
  0047ba77  c3                    ret

LAB_0047ba78:
  0047ba78  8d442410              lea       eax,[esp+10h]
  0047ba7c  50                    push      eax
  0047ba7d  56                    push      esi
  0047ba7e  e81d92ffff            call      00474CA0h
  0047ba83  8bf8                  mov       edi,eax
  0047ba85  83c408                add       esp,8
  0047ba88  85ff                  test      edi,edi
  0047ba8a  7450                  je        short 0047BADCh
  0047ba8c  8b4674                mov       eax,[esi+74h]
  0047ba8f  33c9                  xor       ecx,ecx
  0047ba91  8a4e06                mov       cl,[esi+6]
  0047ba94  33d2                  xor       edx,edx
  0047ba96  8a5607                mov       dl,[esi+7]
  0047ba99  6a00                  push      0
  0047ba9b  51                    push      ecx
  0047ba9c  52                    push      edx
  0047ba9d  57                    push      edi
  0047ba9e  50                    push      eax
  0047ba9f  e80ce80b00            call      0053A2B0h
  0047baa4  0fbf5e32              movsx     ebx,word [esi+32h]
  0047baa8  83c414                add       esp,14h
  0047baab  53                    push      ebx
  0047baac  e86b9f0c00            call      00545A1Ch
  0047bab1  8b4f50                mov       ecx,[edi+50h]
  0047bab4  53                    push      ebx
  0047bab5  c1e004                shl       eax,4
  0047bab8  03c8                  add       ecx,eax
  0047baba  894f50                mov       [edi+50h],ecx
  0047babd  e8549f0c00            call      00545A16h
  0047bac2  8b4f58                mov       ecx,[edi+58h]
  0047bac5  66c787960000001000    mov       word [edi+96h],10h
  0047bace  c1e004                shl       eax,4
  0047bad1  03c8                  add       ecx,eax
  0047bad3  8b07                  mov       eax,[edi]
  0047bad5  0c40                  or        al,40h
  0047bad7  894f58                mov       [edi+58h],ecx
  0047bada  8907                  mov       [edi],eax

LAB_0047badc:
  0047badc  8b06                  mov       eax,[esi]
  0047bade  66ff4610              inc       word [esi+10h]
  0047bae2  a900000060            test      eax,60000000h
  0047bae7  741b                  je        short 0047BB04h
  0047bae9  a900000002            test      eax,2000000h
  0047baee  7507                  jne       short 0047BAF7h
  0047baf0  8b466c                mov       eax,[esi+6Ch]
  0047baf3  66ff4814              dec       word [eax+14h]

LAB_0047baf7:
  0047baf7  8b466c                mov       eax,[esi+6Ch]
  0047bafa  66ff4816              dec       word [eax+16h]
  0047bafe  c70600000000          mov       dword [esi],0

LAB_0047bb04:
  0047bb04  5f                    pop       edi
  0047bb05  5e                    pop       esi
  0047bb06  5d                    pop       ebp
  0047bb07  5b                    pop       ebx
  0047bb08  83c40c                add       esp,0Ch
  0047bb0b  c3                    ret

9 cible(s) de saut interne : 0x0047b92c, 0x0047b9ee, 0x0047b9f7, 0x0047ba1c, 0x0047ba4e, 0x0047ba78, 0x0047badc, 0x0047baf7, 0x0047bb04
