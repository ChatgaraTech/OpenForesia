  00439fa0  53                    push      ebx
  00439fa1  56                    push      esi
  00439fa2  8b74240c              mov       esi,[esp+0Ch]
  00439fa6  57                    push      edi
  00439fa7  8b466c                mov       eax,[esi+6Ch]
  00439faa  8bf8                  mov       edi,eax
  00439fac  85ff                  test      edi,edi
  00439fae  7505                  jne       short 00439FB5h
  00439fb0  bf58e55d00            mov       edi,5DE558h
  00439fb5  894678                mov       [esi+78h],eax
  00439fb8  668b869a000000        mov       ax,[esi+9Ah]
  00439fbf  6685c0                test      ax,ax
  00439fc2  7520                  jne       short 00439FE4h
  00439fc4  8b4f1c                mov       ecx,[edi+1Ch]
  00439fc7  894e1c                mov       [esi+1Ch],ecx
  00439fca  8b5720                mov       edx,[edi+20h]
  00439fcd  81c200600900          add       edx,96000h
  00439fd3  895620                mov       [esi+20h],edx
  00439fd6  8b4f24                mov       ecx,[edi+24h]
  00439fd9  894e24                mov       [esi+24h],ecx
  00439fdc  668b5732              mov       dx,[edi+32h]
  00439fe0  66895632              mov       [esi+32h],dx
  00439fe4  bb01000000            mov       ebx,1
  00439fe9  663bc3                cmp       ax,bx
  00439fec  7520                  jne       short 0043A00Eh
  00439fee  8b471c                mov       eax,[edi+1Ch]
  00439ff1  89461c                mov       [esi+1Ch],eax
  00439ff4  8b4f20                mov       ecx,[edi+20h]
  00439ff7  81c100400600          add       ecx,64000h
  00439ffd  894e20                mov       [esi+20h],ecx
  0043a000  8b5724                mov       edx,[edi+24h]
  0043a003  895624                mov       [esi+24h],edx
  0043a006  668b4732              mov       ax,[edi+32h]
  0043a00a  66894632              mov       [esi+32h],ax
  0043a00e  66837e1000            cmp       word [esi+10h],0
  0043a013  7526                  jne       short 0043A03Bh
  0043a015  6a00                  push      0
  0043a017  6a00                  push      0
  0043a019  6800100000            push      1000h
  0043a01e  68fd060000            push      6FDh
  0043a023  6a1f                  push      1Fh
  0043a025  56                    push      esi
  0043a026  66895e10              mov       [esi+10h],bx
  0043a02a  e851e00200            call      00468080h
  0043a02f  83c418                add       esp,18h
  0043a032  66c786980000000000    mov       word [esi+98h],0
  0043a03b  66399e98000000        cmp       [esi+98h],bx
  0043a042  7526                  jne       short 0043A06Ah
  0043a044  66399e9a000000        cmp       [esi+9Ah],bx
  0043a04b  6a00                  push      0
  0043a04d  6a00                  push      0
  0043a04f  6800080000            push      800h
  0043a054  6800100000            push      1000h
  0043a059  7504                  jne       short 0043A05Fh
  0043a05b  6a23                  push      23h
  0043a05d  eb02                  jmp       short 0043A061h
  0043a05f  6a20                  push      20h
  0043a061  56                    push      esi
  0043a062  e819e00200            call      00468080h
  0043a067  83c418                add       esp,18h
  0043a06a  6683be9800000005      cmp       word [esi+98h],5
  0043a072  7509                  jne       short 0043A07Dh
  0043a074  66c786980000000000    mov       word [esi+98h],0
  0043a07d  66ff8698000000        inc       word [esi+98h]
  0043a084  8a4743                mov       al,[edi+43h]
  0043a087  3ac3                  cmp       al,bl
  0043a089  7506                  jne       short 0043A091h
  0043a08b  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043a091  8b3f                  mov       edi,[edi]
  0043a093  f7c700000040          test      edi,40000000h
  0043a099  7508                  jne       short 0043A0A3h
  0043a09b  f7c700000080          test      edi,80000000h
  0043a0a1  7506                  jne       short 0043A0A9h
  0043a0a3  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043a0a9  6683be9a00000000      cmp       word [esi+9Ah],0
  0043a0b1  bf0060ffff            mov       edi,0FFFF6000h
  0043a0b6  753c                  jne       short 0043A0F4h
  0043a0b8  0fbf4e32              movsx     ecx,word [esi+32h]
  0043a0bc  51                    push      ecx
  0043a0bd  e85ab91000            call      00545A1Ch
  0043a0c2  8d0480                lea       eax,[eax+eax*4]
  0043a0c5  8d1480                lea       edx,[eax+eax*4]
  0043a0c8  8b461c                mov       eax,[esi+1Ch]
  0043a0cb  d1e2                  shl       edx,1
  0043a0cd  03c2                  add       eax,edx
  0043a0cf  89461c                mov       [esi+1Ch],eax
  0043a0d2  0fbf4632              movsx     eax,word [esi+32h]
  0043a0d6  50                    push      eax
  0043a0d7  e83ab91000            call      00545A16h
  0043a0dc  8b5624                mov       edx,[esi+24h]
  0043a0df  8d0480                lea       eax,[eax+eax*4]
  0043a0e2  8d0c80                lea       ecx,[eax+eax*4]
  0043a0e5  8b4620                mov       eax,[esi+20h]
  0043a0e8  d1e1                  shl       ecx,1
  0043a0ea  03d1                  add       edx,ecx
  0043a0ec  03c7                  add       eax,edi
  0043a0ee  895624                mov       [esi+24h],edx
  0043a0f1  894620                mov       [esi+20h],eax
  0043a0f4  66399e9a000000        cmp       [esi+9Ah],bx
  0043a0fb  7544                  jne       short 0043A141h
  0043a0fd  0fbf5632              movsx     edx,word [esi+32h]
  0043a101  52                    push      edx
  0043a102  e815b91000            call      00545A1Ch
  0043a107  8d0c80                lea       ecx,[eax+eax*4]
  0043a10a  8b5e1c                mov       ebx,[esi+1Ch]
  0043a10d  8d0448                lea       eax,[eax+ecx*2]
  0043a110  8d1480                lea       edx,[eax+eax*4]
  0043a113  0fbf4632              movsx     eax,word [esi+32h]
  0043a117  c1e202                shl       edx,2
  0043a11a  03da                  add       ebx,edx
  0043a11c  50                    push      eax
  0043a11d  895e1c                mov       [esi+1Ch],ebx
  0043a120  e8f1b81000            call      00545A16h
  0043a125  8d0c80                lea       ecx,[eax+eax*4]
  0043a128  8d0448                lea       eax,[eax+ecx*2]
  0043a12b  8b4e24                mov       ecx,[esi+24h]
  0043a12e  8d1480                lea       edx,[eax+eax*4]
  0043a131  8b4620                mov       eax,[esi+20h]
  0043a134  c1e202                shl       edx,2
  0043a137  03ca                  add       ecx,edx
  0043a139  03c7                  add       eax,edi
  0043a13b  894e24                mov       [esi+24h],ecx
  0043a13e  894620                mov       [esi+20h],eax
  0043a141  6aff                  push      0FFFFFFFFh
  0043a143  6a00                  push      0
  0043a145  68de000000            push      0DEh
  0043a14a  8d461c                lea       eax,[esi+1Ch]
  0043a14d  6a00                  push      0
  0043a14f  50                    push      eax
  0043a150  56                    push      esi
  0043a151  e8ea0a0400            call      0047AC40h
  0043a156  83c418                add       esp,18h
  0043a159  5f                    pop       edi
  0043a15a  5e                    pop       esi
  0043a15b  5b                    pop       ebx
  0043a15c  c3                    ret
