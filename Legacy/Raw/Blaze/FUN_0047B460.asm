
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b460  section .text  offset fichier 0x7a860  583 octets

  0047b460  83ec10                sub       esp,10h
  0047b463  53                    push      ebx
  0047b464  55                    push      ebp
  0047b465  56                    push      esi
  0047b466  8b742420              mov       esi,[esp+20h]
  0047b46a  33c0                  xor       eax,eax
  0047b46c  b964000000            mov       ecx,64h
  0047b471  66394610              cmp       [esi+10h],ax
  0047b475  57                    push      edi
  0047b476  6689442414            mov       [esp+14h],ax
  0047b47b  66c7442416f6ff        mov       word [esp+16h],0FFF6h
  0047b482  66c74424183c00        mov       word [esp+18h],3Ch
  0047b489  66894c241a            mov       [esp+1Ah],cx
  0047b48e  668944241c            mov       [esp+1Ch],ax
  0047b493  66894c241e            mov       [esp+1Eh],cx
  0047b498  7539                  jne       short 0047B4D3h
  0047b49a  668b4e36              mov       cx,[esi+36h]
  0047b49e  8b3e                  mov       edi,[esi]
  0047b4a0  8d561c                lea       edx,[esi+1Ch]
  0047b4a3  83e1fb                and       ecx,0FFFFFFFBh
  0047b4a6  52                    push      edx
  0047b4a7  6800120000            push      1200h
  0047b4ac  81cf80000000          or        edi,80h
  0047b4b2  83c902                or        ecx,2
  0047b4b5  68fd060000            push      6FDh
  0047b4ba  6a18                  push      18h
  0047b4bc  893e                  mov       [esi],edi
  0047b4be  66894e36              mov       [esi+36h],cx
  0047b4c2  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047b4c8  894654                mov       [esi+54h],eax
  0047b4cb  e850260c00            call      0053DB20h
  0047b4d0  83c410                add       esp,10h

LAB_0047b4d3:
  0047b4d3  0fbf5e32              movsx     ebx,word [esi+32h]
  0047b4d7  53                    push      ebx
  0047b4d8  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0047b4e1  8d7e1c                lea       edi,[esi+1Ch]
  0047b4e4  e833a50c00            call      00545A1Ch
  0047b4e9  8b0f                  mov       ecx,[edi]
  0047b4eb  8d0480                lea       eax,[eax+eax*4]
  0047b4ee  c1e002                shl       eax,2
  0047b4f1  03c8                  add       ecx,eax
  0047b4f3  53                    push      ebx
  0047b4f4  890f                  mov       [edi],ecx
  0047b4f6  e81ba50c00            call      00545A16h
  0047b4fb  8d0c80                lea       ecx,[eax+eax*4]
  0047b4fe  8b4624                mov       eax,[esi+24h]
  0047b501  c1e102                shl       ecx,2
  0047b504  03c1                  add       eax,ecx
  0047b506  8b6e20                mov       ebp,[esi+20h]
  0047b509  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0047b510  894624                mov       [esi+24h],eax
  0047b513  8b4654                mov       eax,[esi+54h]
  0047b516  8bd1                  mov       edx,ecx
  0047b518  8bd8                  mov       ebx,eax
  0047b51a  c1fa08                sar       edx,8
  0047b51d  c1fb08                sar       ebx,8
  0047b520  2bd3                  sub       edx,ebx
  0047b522  03e8                  add       ebp,eax
  0047b524  03d0                  add       edx,eax
  0047b526  33c0                  xor       eax,eax
  0047b528  8a460b                mov       al,[esi+0Bh]
  0047b52b  6aff                  push      0FFFFFFFFh
  0047b52d  50                    push      eax
  0047b52e  2bd1                  sub       edx,ecx
  0047b530  6a66                  push      66h
  0047b532  56                    push      esi
  0047b533  896e20                mov       [esi+20h],ebp
  0047b536  895654                mov       [esi+54h],edx
  0047b539  e8a2f5ffff            call      0047AAE0h
  0047b53e  8b4e54                mov       ecx,[esi+54h]
  0047b541  8b5658                mov       edx,[esi+58h]
  0047b544  8b6e50                mov       ebp,[esi+50h]
  0047b547  83c410                add       esp,10h
  0047b54a  894c2424              mov       [esp+24h],ecx
  0047b54e  89542410              mov       [esp+10h],edx
  0047b552  e8e9bb0c00            call      00547140h
  0047b557  0fbf4e32              movsx     ecx,word [esi+32h]
  0047b55b  25ff030000            and       eax,3FFh
  0047b560  8d9c0800060000        lea       ebx,[eax+ecx+600h]
  0047b567  53                    push      ebx
  0047b568  e8afa40c00            call      00545A1Ch
  0047b56d  8d1480                lea       edx,[eax+eax*4]
  0047b570  c1e202                shl       edx,2
  0047b573  895650                mov       [esi+50h],edx
  0047b576  e8c5bb0c00            call      00547140h
  0047b57b  83e00f                and       eax,0Fh
  0047b57e  53                    push      ebx
  0047b57f  83c014                add       eax,14h
  0047b582  c1e00c                shl       eax,0Ch
  0047b585  894654                mov       [esi+54h],eax
  0047b588  e889a40c00            call      00545A16h
  0047b58d  33c9                  xor       ecx,ecx
  0047b58f  6aff                  push      0FFFFFFFFh
  0047b591  8a4e0b                mov       cl,[esi+0Bh]
  0047b594  8d0480                lea       eax,[eax+eax*4]
  0047b597  51                    push      ecx
  0047b598  6a4a                  push      4Ah
  0047b59a  68b81b5500            push      551BB8h
  0047b59f  57                    push      edi
  0047b5a0  c1e002                shl       eax,2
  0047b5a3  56                    push      esi
  0047b5a4  894658                mov       [esi+58h],eax
  0047b5a7  e894f6ffff            call      0047AC40h
  0047b5ac  8b54243c              mov       edx,[esp+3Ch]
  0047b5b0  8b442428              mov       eax,[esp+28h]
  0047b5b4  8b7e20                mov       edi,[esi+20h]
  0047b5b7  83c418                add       esp,18h
  0047b5ba  896e50                mov       [esi+50h],ebp
  0047b5bd  895654                mov       [esi+54h],edx
  0047b5c0  56                    push      esi
  0047b5c1  894658                mov       [esi+58h],eax
  0047b5c4  e877830b00            call      00533940h
  0047b5c9  83c404                add       esp,4
  0047b5cc  8b4620                mov       eax,[esi+20h]
  0047b5cf  8b4e60                mov       ecx,[esi+60h]
  0047b5d2  3bc1                  cmp       eax,ecx
  0047b5d4  7c35                  jl        short 0047B60Bh
  0047b5d6  8b9690000000          mov       edx,[esi+90h]
  0047b5dc  8bda                  mov       ebx,edx
  0047b5de  81e300220000          and       ebx,2200h
  0047b5e4  81fb00200000          cmp       ebx,2000h
  0047b5ea  741f                  je        short 0047B60Bh
  0047b5ec  81c700a00000          add       edi,0A000h
  0047b5f2  3bf8                  cmp       edi,eax
  0047b5f4  7f0b                  jg        short 0047B601h
  0047b5f6  80ce08                or        dh,8
  0047b5f9  899690000000          mov       [esi+90h],edx
  0047b5ff  eb03                  jmp       short 0047B604h

LAB_0047b601:
  0047b601  894e20                mov       [esi+20h],ecx

LAB_0047b604:
  0047b604  c7465400000000        mov       dword [esi+54h],0

LAB_0047b60b:
  0047b60b  f70600000040          test      dword [esi],40000000h
  0047b611  0f8582000000          jne       near 0047B699h
  0047b617  f7869000000002080000  test      dword [esi+90h],802h
  0047b621  7576                  jne       short 0047B699h
  0047b623  8d4c2414              lea       ecx,[esp+14h]
  0047b627  51                    push      ecx
  0047b628  56                    push      esi
  0047b629  e87296ffff            call      00474CA0h
  0047b62e  8bf8                  mov       edi,eax
  0047b630  83c408                add       esp,8
  0047b633  85ff                  test      edi,edi
  0047b635  7450                  je        short 0047B687h
  0047b637  8b4e74                mov       ecx,[esi+74h]
  0047b63a  33d2                  xor       edx,edx
  0047b63c  8a5606                mov       dl,[esi+6]
  0047b63f  33c0                  xor       eax,eax
  0047b641  8a4607                mov       al,[esi+7]
  0047b644  6a00                  push      0
  0047b646  52                    push      edx
  0047b647  50                    push      eax
  0047b648  57                    push      edi
  0047b649  51                    push      ecx
  0047b64a  e861ec0b00            call      0053A2B0h
  0047b64f  0fbf5e32              movsx     ebx,word [esi+32h]
  0047b653  83c414                add       esp,14h
  0047b656  53                    push      ebx
  0047b657  e8c0a30c00            call      00545A1Ch
  0047b65c  8b4f50                mov       ecx,[edi+50h]
  0047b65f  53                    push      ebx
  0047b660  c1e004                shl       eax,4
  0047b663  03c8                  add       ecx,eax
  0047b665  894f50                mov       [edi+50h],ecx
  0047b668  e8a9a30c00            call      00545A16h
  0047b66d  8b4f58                mov       ecx,[edi+58h]
  0047b670  66c787960000000400    mov       word [edi+96h],4
  0047b679  c1e004                shl       eax,4
  0047b67c  03c8                  add       ecx,eax
  0047b67e  8b07                  mov       eax,[edi]
  0047b680  0c40                  or        al,40h
  0047b682  894f58                mov       [edi+58h],ecx
  0047b685  8907                  mov       [edi],eax

LAB_0047b687:
  0047b687  66ff4610              inc       word [esi+10h]
  0047b68b  668b4610              mov       ax,[esi+10h]
  0047b68f  66ff4612              inc       word [esi+12h]
  0047b693  663d1400              cmp       ax,14h
  0047b697  7c06                  jl        short 0047B69Fh

LAB_0047b699:
  0047b699  c70600000000          mov       dword [esi],0

LAB_0047b69f:
  0047b69f  5f                    pop       edi
  0047b6a0  5e                    pop       esi
  0047b6a1  5d                    pop       ebp
  0047b6a2  5b                    pop       ebx
  0047b6a3  83c410                add       esp,10h
  0047b6a6  c3                    ret

7 cible(s) de saut interne : 0x0047b4d3, 0x0047b601, 0x0047b604, 0x0047b60b, 0x0047b687, 0x0047b699, 0x0047b69f
