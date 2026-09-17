Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045c2e0  section .text  offset fichier 0x5b6e0  896 octets

  0045c2e0  83ec20                sub       esp,20h
  0045c2e3  53                    push      ebx
  0045c2e4  55                    push      ebp
  0045c2e5  56                    push      esi
  0045c2e6  b003                  mov       al,3
  0045c2e8  57                    push      edi
  0045c2e9  8b7c2434              mov       edi,[esp+34h]
  0045c2ed  83cbff                or        ebx,0FFFFFFFFh
  0045c2f0  c644242000            mov       byte [esp+20h],0
  0045c2f5  88442421              mov       [esp+21h],al
  0045c2f9  c644242200            mov       byte [esp+22h],0
  0045c2fe  c644242301            mov       byte [esp+23h],1
  0045c303  c644242400            mov       byte [esp+24h],0
  0045c308  c644242500            mov       byte [esp+25h],0
  0045c30d  c644242800            mov       byte [esp+28h],0
  0045c312  88442429              mov       [esp+29h],al
  0045c316  c644242a01            mov       byte [esp+2Ah],1
  0045c31b  c644242b01            mov       byte [esp+2Bh],1
  0045c320  c644242c00            mov       byte [esp+2Ch],0
  0045c325  c644242d00            mov       byte [esp+2Dh],0
  0045c32a  33ed                  xor       ebp,ebp
  0045c32c  bea0ca5e00            mov       esi,5ECAA0h
  0045c331  c744241820000000      mov       dword [esp+18h],20h

LAB_0045c339:
  0045c339  8b06                  mov       eax,[esi]
  0045c33b  a900000080            test      eax,80000000h
  0045c340  7465                  je        short 0045C3A7h
  0045c342  807e0c04              cmp       byte [esi+0Ch],4
  0045c346  755f                  jne       short 0045C3A7h
  0045c348  a900000040            test      eax,40000000h
  0045c34d  7558                  jne       short 0045C3A7h
  0045c34f  80cc40                or        ah,40h
  0045c352  8d4c2420              lea       ecx,[esp+20h]
  0045c356  8906                  mov       [esi],eax
  0045c358  8d442410              lea       eax,[esp+10h]
  0045c35c  50                    push      eax
  0045c35d  51                    push      ecx
  0045c35e  c644242d01            mov       byte [esp+2Dh],1
  0045c363  c644243501            mov       byte [esp+35h],1
  0045c368  e8b37c0b00            call      00514020h
  0045c36d  83c408                add       esp,8
  0045c370  8d542410              lea       edx,[esp+10h]
  0045c374  8d442428              lea       eax,[esp+28h]
  0045c378  52                    push      edx
  0045c379  50                    push      eax
  0045c37a  e8a17c0b00            call      00514020h
  0045c37f  b864000000            mov       eax,64h
  0045c384  83c408                add       esp,8
  0045c387  66c70560915e00fa00    mov       word [5E9160h],0FAh
  0045c390  66a362915e00          mov       [5E9162h],ax
  0045c396  66a364915e00          mov       [5E9164h],ax
  0045c39c  66c70566915e000200    mov       word [5E9166h],2
  0045c3a5  8bdd                  mov       ebx,ebp

LAB_0045c3a7:
  0045c3a7  8b06                  mov       eax,[esi]
  0045c3a9  a900000080            test      eax,80000000h
  0045c3ae  7468                  je        short 0045C418h
  0045c3b0  807e0c04              cmp       byte [esi+0Ch],4
  0045c3b4  7562                  jne       short 0045C418h
  0045c3b6  a900000040            test      eax,40000000h
  0045c3bb  745b                  je        short 0045C418h
  0045c3bd  8d4c2410              lea       ecx,[esp+10h]
  0045c3c1  8d542420              lea       edx,[esp+20h]
  0045c3c5  51                    push      ecx
  0045c3c6  52                    push      edx
  0045c3c7  c644242d00            mov       byte [esp+2Dh],0
  0045c3cc  c644243500            mov       byte [esp+35h],0
  0045c3d1  e84a7c0b00            call      00514020h
  0045c3d6  83c408                add       esp,8
  0045c3d9  8d442410              lea       eax,[esp+10h]
  0045c3dd  8d4c2428              lea       ecx,[esp+28h]
  0045c3e1  50                    push      eax
  0045c3e2  51                    push      ecx
  0045c3e3  e8387c0b00            call      00514020h
  0045c3e8  b878000000            mov       eax,78h
  0045c3ed  83c408                add       esp,8
  0045c3f0  66a360915e00          mov       [5E9160h],ax
  0045c3f6  66a362915e00          mov       [5E9162h],ax
  0045c3fc  66a364915e00          mov       [5E9164h],ax
  0045c402  66c70566915e000200    mov       word [5E9166h],2
  0045c40b  66837f1600            cmp       word [edi+16h],0
  0045c410  7506                  jne       short 0045C418h
  0045c412  66c747160100          mov       word [edi+16h],1

LAB_0045c418:
  0045c418  8b442418              mov       eax,[esp+18h]
  0045c41c  45                    inc       ebp
  0045c41d  81c69c000000          add       esi,9Ch
  0045c423  48                    dec       eax
  0045c424  89442418              mov       [esp+18h],eax
  0045c428  0f850bffffff          jne       near 0045C339h
  0045c42e  83fbff                cmp       ebx,0FFFFFFFFh
  0045c431  bea0ca5e00            mov       esi,5ECAA0h
  0045c436  7463                  je        short 0045C49Bh
  0045c438  bb20000000            mov       ebx,20h
  0045c43d  bd30750000            mov       ebp,7530h

LAB_0045c442:
  0045c442  8b06                  mov       eax,[esi]
  0045c444  a900000080            test      eax,80000000h
  0045c449  7445                  je        short 0045C490h
  0045c44b  807e0c04              cmp       byte [esi+0Ch],4
  0045c44f  743f                  je        short 0045C490h
  0045c451  a900000040            test      eax,40000000h
  0045c456  7538                  jne       short 0045C490h
  0045c458  8d542414              lea       edx,[esp+14h]
  0045c45c  8d44241c              lea       eax,[esp+1Ch]
  0045c460  52                    push      edx
  0045c461  8d4c2438              lea       ecx,[esp+38h]
  0045c465  50                    push      eax
  0045c466  51                    push      ecx
  0045c467  56                    push      esi
  0045c468  e813c50d00            call      00538980h
  0045c46d  8b542444              mov       edx,[esp+44h]
  0045c471  83c410                add       esp,10h
  0045c474  66896a32              mov       [edx+32h],bp
  0045c478  8b442434              mov       eax,[esp+34h]
  0045c47c  6689683a              mov       [eax+3Ah],bp
  0045c480  8a4e3b                mov       cl,[esi+3Bh]
  0045c483  80c902                or        cl,2
  0045c486  884e3b                mov       [esi+3Bh],cl
  0045c489  8b06                  mov       eax,[esi]
  0045c48b  80cc20                or        ah,20h
  0045c48e  8906                  mov       [esi],eax

LAB_0045c490:
  0045c490  81c69c000000          add       esi,9Ch
  0045c496  4b                    dec       ebx
  0045c497  75a9                  jne       short 0045C442h
  0045c499  eb6d                  jmp       short 0045C508h

LAB_0045c49b:
  0045c49b  bd20000000            mov       ebp,20h
  0045c4a0  b3fd                  mov       bl,0FDh

LAB_0045c4a2:
  0045c4a2  8b06                  mov       eax,[esi]
  0045c4a4  a900000080            test      eax,80000000h
  0045c4a9  7454                  je        short 0045C4FFh
  0045c4ab  807e0c04              cmp       byte [esi+0Ch],4
  0045c4af  744e                  je        short 0045C4FFh
  0045c4b1  a900000040            test      eax,40000000h
  0045c4b6  7547                  jne       short 0045C4FFh
  0045c4b8  8d4c2414              lea       ecx,[esp+14h]
  0045c4bc  8d54241c              lea       edx,[esp+1Ch]
  0045c4c0  51                    push      ecx
  0045c4c1  8d442438              lea       eax,[esp+38h]
  0045c4c5  52                    push      edx
  0045c4c6  50                    push      eax
  0045c4c7  56                    push      esi
  0045c4c8  e8b3c40d00            call      00538980h
  0045c4cd  8b4c2424              mov       ecx,[esp+24h]
  0045c4d1  8b442444              mov       eax,[esp+44h]
  0045c4d5  83c410                add       esp,10h
  0045c4d8  668b5132              mov       dx,[ecx+32h]
  0045c4dc  66895032              mov       [eax+32h],dx
  0045c4e0  8b4c2414              mov       ecx,[esp+14h]
  0045c4e4  8b442434              mov       eax,[esp+34h]
  0045c4e8  668b513a              mov       dx,[ecx+3Ah]
  0045c4ec  6689503a              mov       [eax+3Ah],dx
  0045c4f0  8a4e3b                mov       cl,[esi+3Bh]
  0045c4f3  22cb                  and       cl,bl
  0045c4f5  884e3b                mov       [esi+3Bh],cl
  0045c4f8  8b06                  mov       eax,[esi]
  0045c4fa  80e4df                and       ah,0DFh
  0045c4fd  8906                  mov       [esi],eax

LAB_0045c4ff:
  0045c4ff  81c69c000000          add       esi,9Ch
  0045c505  4d                    dec       ebp
  0045c506  759a                  jne       short 0045C4A2h

LAB_0045c508:
  0045c508  be01000000            mov       esi,1
  0045c50d  66397716              cmp       [edi+16h],si
  0045c511  757f                  jne       short 0045C592h
  0045c513  f6472801              test      byte [edi+28h],1
  0045c517  7532                  jne       short 0045C54Bh
  0045c519  8b0dcc975e00          mov       ecx,[5E97CCh]
  0045c51f  6a00                  push      0
  0045c521  6a00                  push      0
  0045c523  6800180000            push      1800h
  0045c528  8b91a8000000          mov       edx,[ecx+0A8h]
  0045c52e  6800040000            push      400h
  0045c533  8b02                  mov       eax,[edx]
  0045c535  660fb6482f            movzx     cx,byte [eax+2Fh]
  0045c53a  51                    push      ecx
  0045c53b  57                    push      edi
  0045c53c  e83fbb0000            call      00468080h
  0045c541  8b07                  mov       eax,[edi]
  0045c543  83c418                add       esp,18h
  0045c546  80cc40                or        ah,40h
  0045c549  eb05                  jmp       short 0045C550h

LAB_0045c54b:
  0045c54b  8b07                  mov       eax,[edi]
  0045c54d  80e4bf                and       ah,0BFh

LAB_0045c550:
  0045c550  6a00                  push      0
  0045c552  6a00                  push      0
  0045c554  6aff                  push      0FFFFFFFFh
  0045c556  6a00                  push      0
  0045c558  6a36                  push      36h
  0045c55a  57                    push      edi
  0045c55b  8907                  mov       [edi],eax
  0045c55d  e8aebb0000            call      00468110h
  0045c562  b937ffffff            mov       ecx,0FFFFFF37h
  0045c567  83c418                add       esp,18h
  0045c56a  66014f28              add       [edi+28h],cx
  0045c56e  668b4728              mov       ax,[edi+28h]
  0045c572  66014f2c              add       [edi+2Ch],cx
  0045c576  6685c0                test      ax,ax
  0045c579  7d17                  jge       short 0045C592h
  0045c57b  33c0                  xor       eax,eax
  0045c57d  66c747160200          mov       word [edi+16h],2
  0045c583  66894728              mov       [edi+28h],ax
  0045c587  6689472c              mov       [edi+2Ch],ax
  0045c58b  8b07                  mov       eax,[edi]
  0045c58d  80cc40                or        ah,40h
  0045c590  8907                  mov       [edi],eax

LAB_0045c592:
  0045c592  33c0                  xor       eax,eax
  0045c594  66394710              cmp       [edi+10h],ax
  0045c598  7510                  jne       short 0045C5AAh
  0045c59a  66897710              mov       [edi+10h],si
  0045c59e  66894712              mov       [edi+12h],ax
  0045c5a2  66894714              mov       [edi+14h],ax
  0045c5a6  66894716              mov       [edi+16h],ax

LAB_0045c5aa:
  0045c5aa  6683473244            add       word [edi+32h],44h
  0045c5af  668b4732              mov       ax,[edi+32h]
  0045c5b3  663d0010              cmp       ax,1000h
  0045c5b7  7e09                  jle       short 0045C5C2h
  0045c5b9  0500f0ffff            add       eax,0FFFFF000h
  0045c5be  66894732              mov       [edi+32h],ax

LAB_0045c5c2:
  0045c5c2  0fbf5732              movsx     edx,word [edi+32h]
  0045c5c6  52                    push      edx
  0045c5c7  e850940e00            call      00545A1Ch
  0045c5cc  8a4f12                mov       cl,[edi+12h]
  0045c5cf  d3e0                  shl       eax,cl
  0045c5d1  8b4f20                mov       ecx,[edi+20h]
  0045c5d4  03c8                  add       ecx,eax
  0045c5d6  66837f1400            cmp       word [edi+14h],0
  0045c5db  894f20                mov       [edi+20h],ecx
  0045c5de  7469                  je        short 0045C649h
  0045c5e0  6a00                  push      0
  0045c5e2  6a00                  push      0
  0045c5e4  6aff                  push      0FFFFFFFFh
  0045c5e6  6a00                  push      0
  0045c5e8  6a36                  push      36h
  0045c5ea  57                    push      edi
  0045c5eb  e820bb0000            call      00468110h
  0045c5f0  83c418                add       esp,18h
  0045c5f3  6a00                  push      0
  0045c5f5  6a00                  push      0
  0045c5f7  6aff                  push      0FFFFFFFFh
  0045c5f9  6a00                  push      0
  0045c5fb  6a36                  push      36h
  0045c5fd  57                    push      edi
  0045c5fe  e80dbb0000            call      00468110h
  0045c603  83c418                add       esp,18h
  0045c606  6a00                  push      0
  0045c608  6a00                  push      0
  0045c60a  6aff                  push      0FFFFFFFFh
  0045c60c  6a00                  push      0
  0045c60e  6a36                  push      36h
  0045c610  57                    push      edi
  0045c611  e8faba0000            call      00468110h
  0045c616  a1cc975e00            mov       eax,[5E97CCh]
  0045c61b  83c418                add       esp,18h
  0045c61e  8b88a8000000          mov       ecx,[eax+0A8h]
  0045c624  6a00                  push      0
  0045c626  6a00                  push      0
  0045c628  6800100000            push      1000h
  0045c62d  8b11                  mov       edx,[ecx]
  0045c62f  6800040000            push      400h
  0045c634  660fb6422a            movzx     ax,byte [edx+2Ah]
  0045c639  50                    push      eax
  0045c63a  57                    push      edi
  0045c63b  e840ba0000            call      00468080h
  0045c640  83c418                add       esp,18h
  0045c643  66c747140000          mov       word [edi+14h],0

LAB_0045c649:
  0045c649  5f                    pop       edi
  0045c64a  5e                    pop       esi
  0045c64b  5d                    pop       ebp
  0045c64c  5b                    pop       ebx
  0045c64d  83c420                add       esp,20h
  0045c650  c3                    ret
  0045c651  90                    nop
  0045c652  90                    nop
  0045c653  90                    nop
  0045c654  90                    nop
  0045c655  90                    nop
  0045c656  90                    nop
  0045c657  90                    nop
  0045c658  90                    nop
  0045c659  90                    nop
  0045c65a  90                    nop
  0045c65b  90                    nop
  0045c65c  90                    nop
  0045c65d  90                    nop
  0045c65e  90                    nop
  0045c65f  90                    nop

15 cible(s) de saut interne : 0x0045c339, 0x0045c3a7, 0x0045c418, 0x0045c442, 0x0045c490, 0x0045c49b, 0x0045c4a2, 0x0045c4ff, 0x0045c508, 0x0045c54b, 0x0045c550, 0x0045c592, 0x0045c5aa, 0x0045c5c2, 0x0045c649
