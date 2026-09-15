Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a8150  section .text  offset fichier 0xa7550  960 octets

  004a8150  83ec10                sub       esp,10h
  004a8153  b002                  mov       al,2
  004a8155  53                    push      ebx
  004a8156  88442406              mov       [esp+6],al
  004a815a  88442409              mov       [esp+9],al
  004a815e  8844240c              mov       [esp+0Ch],al
  004a8162  88442413              mov       [esp+13h],al
  004a8166  8b442418              mov       eax,[esp+18h]
  004a816a  55                    push      ebp
  004a816b  56                    push      esi
  004a816c  33db                  xor       ebx,ebx
  004a816e  57                    push      edi
  004a816f  8b786c                mov       edi,[eax+6Ch]
  004a8172  885c2410              mov       [esp+10h],bl
  004a8176  c644241101            mov       byte [esp+11h],1
  004a817b  c644241303            mov       byte [esp+13h],3
  004a8180  c644241401            mov       byte [esp+14h],1
  004a8185  c644241603            mov       byte [esp+16h],3
  004a818a  885c2417              mov       [esp+17h],bl
  004a818e  c644241903            mov       byte [esp+19h],3
  004a8193  885c241a              mov       [esp+1Ah],bl
  004a8197  c644241b01            mov       byte [esp+1Bh],1
  004a819c  c644241c03            mov       byte [esp+1Ch],3
  004a81a1  885c241d              mov       [esp+1Dh],bl
  004a81a5  c644241e01            mov       byte [esp+1Eh],1
  004a81aa  e891ef0900            call      00547140h
  004a81af  99                    cdq
  004a81b0  33c2                  xor       eax,edx
  004a81b2  2bc2                  sub       eax,edx
  004a81b4  660fb66f08            movzx     bp,byte [edi+8]
  004a81b9  83e003                and       eax,3
  004a81bc  33c2                  xor       eax,edx
  004a81be  8bf0                  mov       esi,eax
  004a81c0  2bf2                  sub       esi,edx
  004a81c2  e879ef0900            call      00547140h
  004a81c7  99                    cdq
  004a81c8  b90a000000            mov       ecx,0Ah
  004a81cd  f7f9                  idiv      ecx
  004a81cf  6683fa05              cmp       dx,5
  004a81d3  0f8c0b030000          jl        near 004A84E4h
  004a81d9  e862ef0900            call      00547140h
  004a81de  99                    cdq
  004a81df  b90a000000            mov       ecx,0Ah
  004a81e4  f7f9                  idiv      ecx
  004a81e6  6683fa05              cmp       dx,5
  004a81ea  7f05                  jg        short 004A81F1h
  004a81ec  83c8ff                or        eax,0FFFFFFFFh
  004a81ef  eb04                  jmp       short 004A81F5h

LAB_004a81f1:
  004a81f1  8b442428              mov       eax,[esp+28h]

LAB_004a81f5:
  004a81f5  0fbfc0                movsx     eax,ax
  004a81f8  40                    inc       eax
  004a81f9  83f808                cmp       eax,8
  004a81fc  0f87c9020000          ja        near 004A84CBh
  004a8202  ff2485ec844a00        jmp       dword [eax*4+4A84ECh]
  004a8209  33f6                  xor       esi,esi

LAB_004a820b:
  004a820b  e830ef0900            call      00547140h
  004a8210  99                    cdq
  004a8211  33c2                  xor       eax,edx
  004a8213  2bc2                  sub       eax,edx
  004a8215  83e003                and       eax,3
  004a8218  33c2                  xor       eax,edx
  004a821a  2bc2                  sub       eax,edx
  004a821c  8d1480                lea       edx,[eax+eax*4]
  004a821f  c1e203                shl       edx,3
  004a8222  2bd0                  sub       edx,eax
  004a8224  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  004a822b  f7c100000080          test      ecx,80000000h
  004a8231  740f                  je        short 004A8242h
  004a8233  f7c100000050          test      ecx,50000000h
  004a8239  7507                  jne       short 004A8242h
  004a823b  be01000000            mov       esi,1
  004a8240  8be8                  mov       ebp,eax

LAB_004a8242:
  004a8242  663bf3                cmp       si,bx
  004a8245  74c4                  je        short 004A820Bh
  004a8247  e97f020000            jmp       004A84CBh
  004a824c  8b3db4876000          mov       edi,[6087B4h]
  004a8252  33ed                  xor       ebp,ebp
  004a8254  0fbfc6                movsx     eax,si
  004a8257  c744242804000000      mov       dword [esp+28h],4
  004a825f  8d748410              lea       esi,[esp+eax*4+10h]

LAB_004a8263:
  004a8263  8a1e                  mov       bl,[esi]
  004a8265  0fbec3                movsx     eax,bl
  004a8268  8d0c80                lea       ecx,[eax+eax*4]
  004a826b  c1e103                shl       ecx,3
  004a826e  2bc8                  sub       ecx,eax
  004a8270  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a8277  f7c100000080          test      ecx,80000000h
  004a827d  742e                  je        short 004A82ADh
  004a827f  f7c100000050          test      ecx,50000000h
  004a8285  7526                  jne       short 004A82ADh
  004a8287  8d4878                lea       ecx,[eax+78h]
  004a828a  8b07                  mov       eax,[edi]
  004a828c  c1e10d                shl       ecx,0Dh
  004a828f  03c8                  add       ecx,eax
  004a8291  0fbf8148010000        movsx     eax,word [ecx+148h]
  004a8298  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004a829f  c1e00c                shl       eax,0Ch
  004a82a2  99                    cdq
  004a82a3  f7f9                  idiv      ecx
  004a82a5  85c0                  test      eax,eax
  004a82a7  7e04                  jle       short 004A82ADh
  004a82a9  660fbeeb              movsx     bp,bl

LAB_004a82ad:
  004a82ad  8b442428              mov       eax,[esp+28h]
  004a82b1  46                    inc       esi
  004a82b2  48                    dec       eax
  004a82b3  89442428              mov       [esp+28h],eax
  004a82b7  75aa                  jne       short 004A8263h
  004a82b9  e90d020000            jmp       004A84CBh
  004a82be  8b3db4876000          mov       edi,[6087B4h]
  004a82c4  33ed                  xor       ebp,ebp
  004a82c6  0fbfd6                movsx     edx,si
  004a82c9  c744242804000000      mov       dword [esp+28h],4
  004a82d1  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a82d5:
  004a82d5  8a1e                  mov       bl,[esi]
  004a82d7  0fbec3                movsx     eax,bl
  004a82da  8d0c80                lea       ecx,[eax+eax*4]
  004a82dd  c1e103                shl       ecx,3
  004a82e0  2bc8                  sub       ecx,eax
  004a82e2  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a82e9  f7c100000080          test      ecx,80000000h
  004a82ef  7431                  je        short 004A8322h
  004a82f1  f7c100000050          test      ecx,50000000h
  004a82f7  7529                  jne       short 004A8322h
  004a82f9  8d4878                lea       ecx,[eax+78h]
  004a82fc  8b07                  mov       eax,[edi]
  004a82fe  c1e10d                shl       ecx,0Dh
  004a8301  03c8                  add       ecx,eax
  004a8303  0fbf8148010000        movsx     eax,word [ecx+148h]
  004a830a  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004a8311  c1e00c                shl       eax,0Ch
  004a8314  99                    cdq
  004a8315  f7f9                  idiv      ecx
  004a8317  3d10270000            cmp       eax,2710h
  004a831c  7d04                  jge       short 004A8322h
  004a831e  660fbeeb              movsx     bp,bl

LAB_004a8322:
  004a8322  8b442428              mov       eax,[esp+28h]
  004a8326  46                    inc       esi
  004a8327  48                    dec       eax
  004a8328  89442428              mov       [esp+28h],eax
  004a832c  75a7                  jne       short 004A82D5h
  004a832e  e998010000            jmp       004A84CBh
  004a8333  8b3db4876000          mov       edi,[6087B4h]
  004a8339  bb04000000            mov       ebx,4
  004a833e  0fbfd6                movsx     edx,si
  004a8341  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a8345:
  004a8345  8a16                  mov       dl,[esi]
  004a8347  0fbec2                movsx     eax,dl
  004a834a  8d0c80                lea       ecx,[eax+eax*4]
  004a834d  c1e103                shl       ecx,3
  004a8350  2bc8                  sub       ecx,eax
  004a8352  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a8359  f7c100000080          test      ecx,80000000h
  004a835f  741b                  je        short 004A837Ch
  004a8361  f7c100000050          test      ecx,50000000h
  004a8367  7513                  jne       short 004A837Ch
  004a8369  8b0f                  mov       ecx,[edi]
  004a836b  c1e00d                shl       eax,0Dh
  004a836e  f6840850010f0007      test      byte [eax+ecx+0F0150h],7
  004a8376  7504                  jne       short 004A837Ch
  004a8378  660fbeea              movsx     bp,dl

LAB_004a837c:
  004a837c  46                    inc       esi
  004a837d  4b                    dec       ebx
  004a837e  75c5                  jne       short 004A8345h
  004a8380  e946010000            jmp       004A84CBh
  004a8385  8b3db4876000          mov       edi,[6087B4h]
  004a838b  bb04000000            mov       ebx,4
  004a8390  0fbfd6                movsx     edx,si
  004a8393  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a8397:
  004a8397  8a16                  mov       dl,[esi]
  004a8399  0fbec2                movsx     eax,dl
  004a839c  8d0c80                lea       ecx,[eax+eax*4]
  004a839f  c1e103                shl       ecx,3
  004a83a2  2bc8                  sub       ecx,eax
  004a83a4  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a83ab  f7c100000080          test      ecx,80000000h
  004a83b1  741e                  je        short 004A83D1h
  004a83b3  f7c100000050          test      ecx,50000000h
  004a83b9  7516                  jne       short 004A83D1h
  004a83bb  8b0f                  mov       ecx,[edi]
  004a83bd  c1e00d                shl       eax,0Dh
  004a83c0  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004a83c7  2407                  and       al,7
  004a83c9  3c01                  cmp       al,1
  004a83cb  7504                  jne       short 004A83D1h
  004a83cd  660fbeea              movsx     bp,dl

LAB_004a83d1:
  004a83d1  46                    inc       esi
  004a83d2  4b                    dec       ebx
  004a83d3  75c2                  jne       short 004A8397h
  004a83d5  e9f1000000            jmp       004A84CBh
  004a83da  8b3db4876000          mov       edi,[6087B4h]
  004a83e0  bb04000000            mov       ebx,4
  004a83e5  0fbfce                movsx     ecx,si
  004a83e8  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004a83ec:
  004a83ec  8a16                  mov       dl,[esi]
  004a83ee  0fbec2                movsx     eax,dl
  004a83f1  8d0c80                lea       ecx,[eax+eax*4]
  004a83f4  c1e103                shl       ecx,3
  004a83f7  2bc8                  sub       ecx,eax
  004a83f9  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a8400  f7c100000080          test      ecx,80000000h
  004a8406  741e                  je        short 004A8426h
  004a8408  f7c100000050          test      ecx,50000000h
  004a840e  7516                  jne       short 004A8426h
  004a8410  8b0f                  mov       ecx,[edi]
  004a8412  c1e00d                shl       eax,0Dh
  004a8415  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004a841c  2407                  and       al,7
  004a841e  3c03                  cmp       al,3
  004a8420  7504                  jne       short 004A8426h
  004a8422  660fbeea              movsx     bp,dl

LAB_004a8426:
  004a8426  46                    inc       esi
  004a8427  4b                    dec       ebx
  004a8428  75c2                  jne       short 004A83ECh
  004a842a  e99c000000            jmp       004A84CBh
  004a842f  8b3db4876000          mov       edi,[6087B4h]
  004a8435  bb04000000            mov       ebx,4
  004a843a  0fbfce                movsx     ecx,si
  004a843d  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004a8441:
  004a8441  8a16                  mov       dl,[esi]
  004a8443  0fbec2                movsx     eax,dl
  004a8446  8d0c80                lea       ecx,[eax+eax*4]
  004a8449  c1e103                shl       ecx,3
  004a844c  2bc8                  sub       ecx,eax
  004a844e  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a8455  f7c100000080          test      ecx,80000000h
  004a845b  741b                  je        short 004A8478h
  004a845d  f7c100000050          test      ecx,50000000h
  004a8463  7513                  jne       short 004A8478h
  004a8465  8b0f                  mov       ecx,[edi]
  004a8467  c1e00d                shl       eax,0Dh
  004a846a  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004a8472  7504                  jne       short 004A8478h
  004a8474  660fbeea              movsx     bp,dl

LAB_004a8478:
  004a8478  46                    inc       esi
  004a8479  4b                    dec       ebx
  004a847a  75c5                  jne       short 004A8441h
  004a847c  eb4d                  jmp       short 004A84CBh
  004a847e  8b3db4876000          mov       edi,[6087B4h]
  004a8484  bb04000000            mov       ebx,4
  004a8489  0fbfd6                movsx     edx,si
  004a848c  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a8490:
  004a8490  8a16                  mov       dl,[esi]

LAB_004a8492:
  004a8492  0fbec2                movsx     eax,dl
  004a8495  8d0c80                lea       ecx,[eax+eax*4]
  004a8498  c1e103                shl       ecx,3
  004a849b  2bc8                  sub       ecx,eax
  004a849d  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a84a4  f7c100000080          test      ecx,80000000h
  004a84aa  741b                  je        short 004A84C7h
  004a84ac  f7c100000050          test      ecx,50000000h
  004a84b2  7513                  jne       short 004A84C7h
  004a84b4  8b0f                  mov       ecx,[edi]
  004a84b6  c1e00d                shl       eax,0Dh
  004a84b9  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004a84c1  7404                  je        short 004A84C7h
  004a84c3  660fbeea              movsx     bp,dl

LAB_004a84c7:
  004a84c7  46                    inc       esi
  004a84c8  4b                    dec       ebx
  004a84c9  75c5                  jne       short 004A8490h

LAB_004a84cb:
  004a84cb  0fbfc5                movsx     eax,bp
  004a84ce  8b4c2424              mov       ecx,[esp+24h]
  004a84d2  8d1480                lea       edx,[eax+eax*4]
  004a84d5  c1e203                shl       edx,3
  004a84d8  2bd0                  sub       edx,eax
  004a84da  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004a84e1  89416c                mov       [ecx+6Ch],eax

LAB_004a84e4:
  004a84e4  5f                    pop       edi
  004a84e5  5e                    pop       esi
  004a84e6  5d                    pop       ebp
  004a84e7  5b                    pop       ebx
  004a84e8  83c410                add       esp,10h
  004a84eb  c3                    ret
  004a84ec  09824a00cb84          or        [edx-7B34FFB6h],eax
  004a84f2  4a                    dec       edx
  004a84f3  004c824a              add       [edx+eax*4+4Ah],cl
  004a84f7  00be824a0033          add       [esi+33004A82h],bh
  004a84fd  834a0085              or        dword [edx],0FFFFFF85h
  004a8501  834a00da              or        dword [edx],0FFFFFFDAh
  004a8505  834a002f              or        dword [edx],2Fh
  004a8509  844a00                test      [edx],cl
  004a850c  7e84                  jle       short 004A8492h
  004a850e  4a                    dec       edx
  004a850f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
