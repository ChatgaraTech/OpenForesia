  004370e0  53                    push      ebx
  004370e1  56                    push      esi
  004370e2  8b74240c              mov       esi,[esp+0Ch]
  004370e6  57                    push      edi
  004370e7  bb01000000            mov       ebx,1
  004370ec  66837e1000            cmp       word [esi+10h],0
  004370f1  750a                  jne       short 004370FDh
  004370f3  66895e10              mov       [esi+10h],bx
  004370f7  66c746160000          mov       word [esi+16h],0
  004370fd  8b466c                mov       eax,[esi+6Ch]
  00437100  8bf8                  mov       edi,eax
  00437102  85ff                  test      edi,edi
  00437104  7505                  jne       short 0043710Bh
  00437106  bf58e55d00            mov       edi,5DE558h
  0043710b  894678                mov       [esi+78h],eax
  0043710e  668b869a000000        mov       ax,[esi+9Ah]
  00437115  6685c0                test      ax,ax
  00437118  7520                  jne       short 0043713Ah
  0043711a  8b4f1c                mov       ecx,[edi+1Ch]
  0043711d  894e1c                mov       [esi+1Ch],ecx
  00437120  8b5720                mov       edx,[edi+20h]
  00437123  81c200600900          add       edx,96000h
  00437129  895620                mov       [esi+20h],edx
  0043712c  8b4f24                mov       ecx,[edi+24h]
  0043712f  894e24                mov       [esi+24h],ecx
  00437132  668b5732              mov       dx,[edi+32h]
  00437136  66895632              mov       [esi+32h],dx
  0043713a  663bc3                cmp       ax,bx
  0043713d  7520                  jne       short 0043715Fh
  0043713f  8b471c                mov       eax,[edi+1Ch]
  00437142  89461c                mov       [esi+1Ch],eax
  00437145  8b4f20                mov       ecx,[edi+20h]
  00437148  81c100400600          add       ecx,64000h
  0043714e  894e20                mov       [esi+20h],ecx
  00437151  8b5724                mov       edx,[edi+24h]
  00437154  895624                mov       [esi+24h],edx
  00437157  668b4732              mov       ax,[edi+32h]
  0043715b  66894632              mov       [esi+32h],ax
  0043715f  66837e1000            cmp       word [esi+10h],0
  00437164  7526                  jne       short 0043718Ch
  00437166  6a00                  push      0
  00437168  6a00                  push      0
  0043716a  6800100000            push      1000h
  0043716f  68fd060000            push      6FDh
  00437174  6a1f                  push      1Fh
  00437176  56                    push      esi
  00437177  66895e10              mov       [esi+10h],bx
  0043717b  e8000f0300            call      00468080h
  00437180  83c418                add       esp,18h
  00437183  66c786980000000000    mov       word [esi+98h],0
  0043718c  66399e98000000        cmp       [esi+98h],bx
  00437193  7526                  jne       short 004371BBh
  00437195  66399e9a000000        cmp       [esi+9Ah],bx
  0043719c  6a00                  push      0
  0043719e  6a00                  push      0
  004371a0  6800080000            push      800h
  004371a5  6800100000            push      1000h
  004371aa  7504                  jne       short 004371B0h
  004371ac  6a23                  push      23h
  004371ae  eb02                  jmp       short 004371B2h
  004371b0  6a20                  push      20h
  004371b2  56                    push      esi
  004371b3  e8c80e0300            call      00468080h
  004371b8  83c418                add       esp,18h
  004371bb  6683be9800000005      cmp       word [esi+98h],5
  004371c3  7509                  jne       short 004371CEh
  004371c5  66c786980000000000    mov       word [esi+98h],0
  004371ce  66ff8698000000        inc       word [esi+98h]
  004371d5  8a4743                mov       al,[edi+43h]
  004371d8  3ac3                  cmp       al,bl
  004371da  7506                  jne       short 004371E2h
  004371dc  8126ffffff7f          and       dword [esi],7FFFFFFFh
  004371e2  8b3f                  mov       edi,[edi]
  004371e4  f7c700000040          test      edi,40000000h
  004371ea  7508                  jne       short 004371F4h
  004371ec  f7c700000080          test      edi,80000000h
  004371f2  7506                  jne       short 004371FAh
  004371f4  8126ffffff7f          and       dword [esi],7FFFFFFFh
  004371fa  6683be9a00000000      cmp       word [esi+9Ah],0
  00437202  bf0060ffff            mov       edi,0FFFF6000h
  00437207  753c                  jne       short 00437245h
  00437209  0fbf4e32              movsx     ecx,word [esi+32h]
  0043720d  51                    push      ecx
  0043720e  e809e81000            call      00545A1Ch
  00437213  8d0480                lea       eax,[eax+eax*4]
  00437216  8d1480                lea       edx,[eax+eax*4]
  00437219  8b461c                mov       eax,[esi+1Ch]
  0043721c  d1e2                  shl       edx,1
  0043721e  03c2                  add       eax,edx
  00437220  89461c                mov       [esi+1Ch],eax
  00437223  0fbf4632              movsx     eax,word [esi+32h]
  00437227  50                    push      eax
  00437228  e8e9e71000            call      00545A16h
  0043722d  8b5624                mov       edx,[esi+24h]
  00437230  8d0480                lea       eax,[eax+eax*4]
  00437233  8d0c80                lea       ecx,[eax+eax*4]
  00437236  8b4620                mov       eax,[esi+20h]
  00437239  d1e1                  shl       ecx,1
  0043723b  03d1                  add       edx,ecx
  0043723d  03c7                  add       eax,edi
  0043723f  895624                mov       [esi+24h],edx
  00437242  894620                mov       [esi+20h],eax
  00437245  66399e9a000000        cmp       [esi+9Ah],bx
  0043724c  7544                  jne       short 00437292h
  0043724e  0fbf5632              movsx     edx,word [esi+32h]
  00437252  52                    push      edx
  00437253  e8c4e71000            call      00545A1Ch
  00437258  8d0c80                lea       ecx,[eax+eax*4]
  0043725b  8b5e1c                mov       ebx,[esi+1Ch]
  0043725e  8d0448                lea       eax,[eax+ecx*2]
  00437261  8d1480                lea       edx,[eax+eax*4]
  00437264  0fbf4632              movsx     eax,word [esi+32h]
  00437268  c1e202                shl       edx,2
  0043726b  03da                  add       ebx,edx
  0043726d  50                    push      eax
  0043726e  895e1c                mov       [esi+1Ch],ebx
  00437271  e8a0e71000            call      00545A16h
  00437276  8d0c80                lea       ecx,[eax+eax*4]
  00437279  8d0448                lea       eax,[eax+ecx*2]
  0043727c  8b4e24                mov       ecx,[esi+24h]
  0043727f  8d1480                lea       edx,[eax+eax*4]
  00437282  8b4620                mov       eax,[esi+20h]
  00437285  c1e202                shl       edx,2
  00437288  03ca                  add       ecx,edx
  0043728a  03c7                  add       eax,edi
  0043728c  894e24                mov       [esi+24h],ecx
  0043728f  894620                mov       [esi+20h],eax
  00437292  6aff                  push      0FFFFFFFFh
  00437294  6a00                  push      0
  00437296  68de000000            push      0DEh
  0043729b  8d461c                lea       eax,[esi+1Ch]
  0043729e  6a00                  push      0
  004372a0  50                    push      eax
  004372a1  56                    push      esi
  004372a2  e899390400            call      0047AC40h
  004372a7  83c418                add       esp,18h
  004372aa  5f                    pop       edi
  004372ab  5e                    pop       esi
  004372ac  5b                    pop       ebx
  004372ad  c3                    ret
