Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048b530  section .text  offset fichier 0x8a930  3136 octets

  0048b530  83ec60                sub       esp,60h
  0048b533  53                    push      ebx
  0048b534  55                    push      ebp
  0048b535  56                    push      esi
  0048b536  8b742470              mov       esi,[esp+70h]
  0048b53a  33db                  xor       ebx,ebx
  0048b53c  57                    push      edi
  0048b53d  8a4641                mov       al,[esi+41h]
  0048b540  66895c2430            mov       [esp+30h],bx
  0048b545  84c0                  test      al,al
  0048b547  66c7442432ceff        mov       word [esp+32h],0FFCEh
  0048b54e  66c74424343200        mov       word [esp+34h],32h
  0048b555  66c74424366400        mov       word [esp+36h],64h
  0048b55c  66895c2438            mov       [esp+38h],bx
  0048b561  66c744243a9600        mov       word [esp+3Ah],96h
  0048b568  7510                  jne       short 0048B57Ah
  0048b56a  53                    push      ebx
  0048b56b  6a03                  push      3
  0048b56d  56                    push      esi
  0048b56e  c6464101              mov       byte [esi+41h],1
  0048b572  e8f90b0000            call      0048C170h
  0048b577  83c40c                add       esp,0Ch

LAB_0048b57a:
  0048b57a  8b16                  mov       edx,[esi]
  0048b57c  8b461c                mov       eax,[esi+1Ch]
  0048b57f  8b4e24                mov       ecx,[esi+24h]
  0048b582  8d6e1c                lea       ebp,[esi+1Ch]
  0048b585  83ca02                or        edx,2
  0048b588  8b7e6c                mov       edi,[esi+6Ch]
  0048b58b  8916                  mov       [esi],edx
  0048b58d  668b5632              mov       dx,[esi+32h]
  0048b591  8944243c              mov       [esp+3Ch],eax
  0048b595  894c2444              mov       [esp+44h],ecx
  0048b599  8d442420              lea       eax,[esp+20h]
  0048b59d  668954245e            mov       [esp+5Eh],dx
  0048b5a2  8d4c2424              lea       ecx,[esp+24h]
  0048b5a6  50                    push      eax
  0048b5a7  8d54242c              lea       edx,[esp+2Ch]
  0048b5ab  51                    push      ecx
  0048b5ac  52                    push      edx
  0048b5ad  56                    push      esi
  0048b5ae  e8cdd30a00            call      00538980h
  0048b5b3  83c410                add       esp,10h
  0048b5b6  8d44242c              lea       eax,[esp+2Ch]
  0048b5ba  8d4c2414              lea       ecx,[esp+14h]
  0048b5be  8d54241c              lea       edx,[esp+1Ch]
  0048b5c2  50                    push      eax
  0048b5c3  51                    push      ecx
  0048b5c4  8d442418              lea       eax,[esp+18h]
  0048b5c8  52                    push      edx
  0048b5c9  8d8c2480000000        lea       ecx,[esp+80h]
  0048b5d0  50                    push      eax
  0048b5d1  51                    push      ecx
  0048b5d2  56                    push      esi
  0048b5d3  e848a90a00            call      00535F20h
  0048b5d8  8b94248c000000        mov       edx,[esp+8Ch]
  0048b5df  b840000000            mov       eax,40h
  0048b5e4  2bc2                  sub       eax,edx
  0048b5e6  83c418                add       esp,18h
  0048b5e9  89442418              mov       [esp+18h],eax
  0048b5ed  8b442414              mov       eax,[esp+14h]
  0048b5f1  3bc3                  cmp       eax,ebx
  0048b5f3  0f8474060000          je        near 0048BC6Dh
  0048b5f9  8b0e                  mov       ecx,[esi]
  0048b5fb  33d2                  xor       edx,edx
  0048b5fd  8a5604                mov       dl,[esi+4]
  0048b600  81e1fffffffd          and       ecx,0FDFFFFFFh
  0048b606  0fbf5e18              movsx     ebx,word [esi+18h]
  0048b60a  890e                  mov       [esi],ecx
  0048b60c  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0048b613  f7c100000001          test      ecx,1000000h
  0048b619  8b149a                mov       edx,[edx+ebx*4]
  0048b61c  741d                  je        short 0048B63Bh
  0048b61e  66ff461a              inc       word [esi+1Ah]
  0048b622  668b4e1a              mov       cx,[esi+1Ah]
  0048b626  33db                  xor       ebx,ebx
  0048b628  668b5a16              mov       bx,[edx+16h]
  0048b62c  0fbfc9                movsx     ecx,cx
  0048b62f  3bcb                  cmp       ecx,ebx
  0048b631  7c2b                  jl        short 0048B65Eh
  0048b633  66c7461a0000          mov       word [esi+1Ah],0
  0048b639  eb23                  jmp       short 0048B65Eh

LAB_0048b63b:
  0048b63b  66ff461a              inc       word [esi+1Ah]
  0048b63f  33db                  xor       ebx,ebx
  0048b641  668b5a16              mov       bx,[edx+16h]
  0048b645  0fbf561a              movsx     edx,word [esi+1Ah]
  0048b649  3bd3                  cmp       edx,ebx
  0048b64b  7c11                  jl        short 0048B65Eh
  0048b64d  668b561a              mov       dx,[esi+1Ah]
  0048b651  4a                    dec       edx
  0048b652  81c900000002          or        ecx,2000000h
  0048b658  6689561a              mov       [esi+1Ah],dx
  0048b65c  890e                  mov       [esi],ecx

LAB_0048b65e:
  0048b65e  f60602                test      byte [esi],2
  0048b661  747d                  je        short 0048B6E0h
  0048b663  8d442430              lea       eax,[esp+30h]
  0048b667  6a00                  push      0
  0048b669  50                    push      eax
  0048b66a  55                    push      ebp
  0048b66b  56                    push      esi
  0048b66c  e8bfe50800            call      00519C30h
  0048b671  8bd8                  mov       ebx,eax
  0048b673  a1b8e05d00            mov       eax,[5DE0B8h]
  0048b678  83c410                add       esp,10h
  0048b67b  85c0                  test      eax,eax
  0048b67d  745d                  je        short 0048B6DCh
  0048b67f  8b0b                  mov       ecx,[ebx]
  0048b681  53                    push      ebx
  0048b682  83c940                or        ecx,40h
  0048b685  56                    push      esi
  0048b686  66c783960000001000    mov       word [ebx+96h],10h
  0048b68f  890b                  mov       [ebx],ecx
  0048b691  e8eae40a00            call      00539B80h
  0048b696  8a4642                mov       al,[esi+42h]
  0048b699  83c408                add       esp,8
  0048b69c  3c07                  cmp       al,7
  0048b69e  7412                  je        short 0048B6B2h
  0048b6a0  83c31c                add       ebx,1Ch
  0048b6a3  53                    push      ebx
  0048b6a4  6800180000            push      1800h
  0048b6a9  6800030000            push      300h
  0048b6ae  6a03                  push      3
  0048b6b0  eb22                  jmp       short 0048B6D4h

LAB_0048b6b2:
  0048b6b2  8b0dcc975e00          mov       ecx,[5E97CCh]
  0048b6b8  83c31c                add       ebx,1Ch
  0048b6bb  53                    push      ebx
  0048b6bc  6800260000            push      2600h
  0048b6c1  8b91a8000000          mov       edx,[ecx+0A8h]
  0048b6c7  33c9                  xor       ecx,ecx
  0048b6c9  6800020000            push      200h
  0048b6ce  8b02                  mov       eax,[edx]
  0048b6d0  8a483e                mov       cl,[eax+3Eh]
  0048b6d3  51                    push      ecx

LAB_0048b6d4:
  0048b6d4  e847240b00            call      0053DB20h
  0048b6d9  83c410                add       esp,10h

LAB_0048b6dc:
  0048b6dc  8b442414              mov       eax,[esp+14h]

LAB_0048b6e0:
  0048b6e0  33c9                  xor       ecx,ecx
  0048b6e2  8a4e42                mov       cl,[esi+42h]
  0048b6e5  83f909                cmp       ecx,9
  0048b6e8  0f877f050000          ja        near 0048BC6Dh
  0048b6ee  ff248d40c14800        jmp       dword [ecx*4+48C140h]
  0048b6f5  33c9                  xor       ecx,ecx
  0048b6f7  66394e12              cmp       [esi+12h],cx
  0048b6fb  751a                  jne       short 0048B717h
  0048b6fd  8b06                  mov       eax,[esi]
  0048b6ff  66894e18              mov       [esi+18h],cx
  0048b703  0d00000001            or        eax,1000000h
  0048b708  66894e1a              mov       [esi+1Ah],cx
  0048b70c  8906                  mov       [esi],eax
  0048b70e  66c746120100          mov       word [esi+12h],1
  0048b714  894e5c                mov       [esi+5Ch],ecx

LAB_0048b717:
  0048b717  0fbf5610              movsx     edx,word [esi+10h]
  0048b71b  39542410              cmp       [esp+10h],edx
  0048b71f  7d09                  jge       short 0048B72Ah
  0048b721  c6464300              mov       byte [esi+43h],0
  0048b725  e943050000            jmp       0048BC6Dh

LAB_0048b72a:
  0048b72a  8a4643                mov       al,[esi+43h]
  0048b72d  84c0                  test      al,al
  0048b72f  0f8484000000          je        near 0048B7B9h
  0048b735  fec8                  dec       al
  0048b737  894e5c                mov       [esi+5Ch],ecx
  0048b73a  884643                mov       [esi+43h],al
  0048b73d  e92b050000            jmp       0048BC6Dh
  0048b742  33c9                  xor       ecx,ecx
  0048b744  66394e12              cmp       [esi+12h],cx
  0048b748  751b                  jne       short 0048B765h
  0048b74a  8b16                  mov       edx,[esi]
  0048b74c  b801000000            mov       eax,1
  0048b751  81ca00000001          or        edx,1000000h
  0048b757  66894618              mov       [esi+18h],ax
  0048b75b  66894e1a              mov       [esi+1Ah],cx
  0048b75f  8916                  mov       [esi],edx
  0048b761  66894612              mov       [esi+12h],ax

LAB_0048b765:
  0048b765  8b44241c              mov       eax,[esp+1Ch]
  0048b769  8d90c8000000          lea       edx,[eax+0C8h]
  0048b76f  81e2ff0f0000          and       edx,0FFFh
  0048b775  81fa90010000          cmp       edx,190h
  0048b77b  7f0c                  jg        short 0048B789h
  0048b77d  c6464300              mov       byte [esi+43h],0
  0048b781  894e5c                mov       [esi+5Ch],ecx
  0048b784  e9e4040000            jmp       0048BC6Dh

LAB_0048b789:
  0048b789  25ff0f0000            and       eax,0FFFh
  0048b78e  3d00080000            cmp       eax,800h
  0048b793  7d15                  jge       short 0048B7AAh
  0048b795  8b442474              mov       eax,[esp+74h]
  0048b799  894e5c                mov       [esi+5Ch],ecx
  0048b79c  0580000000            add       eax,80h
  0048b7a1  66014632              add       [esi+32h],ax
  0048b7a5  e9c3040000            jmp       0048BC6Dh

LAB_0048b7aa:
  0048b7aa  8b542474              mov       edx,[esp+74h]
  0048b7ae  b880ffffff            mov       eax,0FFFFFF80h
  0048b7b3  2bc2                  sub       eax,edx
  0048b7b5  66014632              add       [esi+32h],ax

LAB_0048b7b9:
  0048b7b9  894e5c                mov       [esi+5Ch],ecx
  0048b7bc  e9ac040000            jmp       0048BC6Dh
  0048b7c1  668b4e12              mov       cx,[esi+12h]
  0048b7c5  ba01000000            mov       edx,1
  0048b7ca  663bca                cmp       cx,dx
  0048b7cd  755c                  jne       short 0048B82Bh
  0048b7cf  8b465c                mov       eax,[esi+5Ch]
  0048b7d2  8b5c2410              mov       ebx,[esp+10h]
  0048b7d6  8bf8                  mov       edi,eax
  0048b7d8  66c746180200          mov       word [esi+18h],2
  0048b7de  c1ff04                sar       edi,4
  0048b7e1  2bc7                  sub       eax,edi
  0048b7e3  6689561a              mov       [esi+1Ah],dx
  0048b7e7  0fbf7e10              movsx     edi,word [esi+10h]
  0048b7eb  3bdf                  cmp       ebx,edi
  0048b7ed  89465c                mov       [esi+5Ch],eax
  0048b7f0  7d06                  jge       short 0048B7F8h
  0048b7f2  c6464300              mov       byte [esi+43h],0
  0048b7f6  eb24                  jmp       short 0048B81Ch

LAB_0048b7f8:
  0048b7f8  2500f0ffff            and       eax,0FFFFF000h
  0048b7fd  3d00600000            cmp       eax,6000h
  0048b802  7d18                  jge       short 0048B81Ch
  0048b804  b800100000            mov       eax,1000h
  0048b809  c6464300              mov       byte [esi+43h],0
  0048b80d  6689462c              mov       [esi+2Ch],ax
  0048b811  6689462a              mov       [esi+2Ah],ax
  0048b815  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0048b81c:
  0048b81c  8b465c                mov       eax,[esi+5Ch]
  0048b81f  c1f805                sar       eax,5
  0048b822  0500100000            add       eax,1000h
  0048b827  6689462c              mov       [esi+2Ch],ax

LAB_0048b82b:
  0048b82b  6685c9                test      cx,cx
  0048b82e  0f8539040000          jne       near 0048BC6Dh
  0048b834  8b0e                  mov       ecx,[esi]
  0048b836  66c746180200          mov       word [esi+18h],2
  0048b83c  81c900000001          or        ecx,1000000h
  0048b842  6689561a              mov       [esi+1Ah],dx
  0048b846  890e                  mov       [esi],ecx
  0048b848  66895612              mov       [esi+12h],dx
  0048b84c  e8efb80b00            call      00547140h
  0048b851  99                    cdq
  0048b852  b905000000            mov       ecx,5
  0048b857  f7f9                  idiv      ecx
  0048b859  8b442474              mov       eax,[esp+74h]
  0048b85d  c1f803                sar       eax,3
  0048b860  24fc                  and       al,0FCh
  0048b862  83c203                add       edx,3
  0048b865  c1e20e                shl       edx,0Eh
  0048b868  03d0                  add       edx,eax
  0048b86a  89565c                mov       [esi+5Ch],edx
  0048b86d  e9fb030000            jmp       0048BC6Dh
  0048b872  bb01000000            mov       ebx,1
  0048b877  66395e12              cmp       [esi+12h],bx
  0048b87b  0f858f000000          jne       near 0048B910h
  0048b881  8a4e43                mov       cl,[esi+43h]
  0048b884  66c746180200          mov       word [esi+18h],2
  0048b88a  84c9                  test      cl,cl
  0048b88c  66895e1a              mov       [esi+1Ah],bx
  0048b890  7405                  je        short 0048B897h
  0048b892  fec9                  dec       cl
  0048b894  884e43                mov       [esi+43h],cl

LAB_0048b897:
  0048b897  668b4e10              mov       cx,[esi+10h]
  0048b89b  0fbf7e14              movsx     edi,word [esi+14h]
  0048b89f  66894e32              mov       [esi+32h],cx
  0048b8a3  0fbfc9                movsx     ecx,cx
  0048b8a6  c1ff04                sar       edi,4
  0048b8a9  51                    push      ecx
  0048b8aa  0faff8                imul      edi,eax
  0048b8ad  e86aa10b00            call      00545A1Ch
  0048b8b2  0fafc7                imul      eax,edi
  0048b8b5  0fbf5610              movsx     edx,word [esi+10h]
  0048b8b9  8b4d00                mov       ecx,[ebp]
  0048b8bc  52                    push      edx
  0048b8bd  c1f80c                sar       eax,0Ch
  0048b8c0  03c8                  add       ecx,eax
  0048b8c2  894d00                mov       [ebp],ecx
  0048b8c5  e84ca10b00            call      00545A16h
  0048b8ca  0fafc7                imul      eax,edi
  0048b8cd  8b6e24                mov       ebp,[esi+24h]
  0048b8d0  c7465c00000000        mov       dword [esi+5Ch],0
  0048b8d7  c1f80c                sar       eax,0Ch
  0048b8da  03e8                  add       ebp,eax
  0048b8dc  668b4614              mov       ax,[esi+14h]
  0048b8e0  668bc8                mov       cx,ax
  0048b8e3  896e24                mov       [esi+24h],ebp
  0048b8e6  66c1f903              sar       cx,3
  0048b8ea  2bc1                  sub       eax,ecx
  0048b8ec  663d0500              cmp       ax,5
  0048b8f0  66894614              mov       [esi+14h],ax
  0048b8f4  7d11                  jge       short 0048B907h
  0048b8f6  b900100000            mov       ecx,1000h
  0048b8fb  c6464300              mov       byte [esi+43h],0
  0048b8ff  66894e2c              mov       [esi+2Ch],cx
  0048b903  66894e2a              mov       [esi+2Ah],cx

LAB_0048b907:
  0048b907  0500100000            add       eax,1000h
  0048b90c  6689462c              mov       [esi+2Ch],ax

LAB_0048b910:
  0048b910  66837e1200            cmp       word [esi+12h],0
  0048b915  0f8552030000          jne       near 0048BC6Dh
  0048b91b  8b0e                  mov       ecx,[esi]
  0048b91d  66c746180200          mov       word [esi+18h],2
  0048b923  81c900000001          or        ecx,1000000h
  0048b929  66895e1a              mov       [esi+1Ah],bx
  0048b92d  890e                  mov       [esi],ecx
  0048b92f  66895e12              mov       [esi+12h],bx
  0048b933  e808b80b00            call      00547140h
  0048b938  99                    cdq
  0048b939  b914000000            mov       ecx,14h
  0048b93e  f7f9                  idiv      ecx
  0048b940  8b442474              mov       eax,[esp+74h]
  0048b944  8d4c020a              lea       ecx,[edx+eax+0Ah]
  0048b948  c1e104                shl       ecx,4
  0048b94b  66894e14              mov       [esi+14h],cx
  0048b94f  e919030000            jmp       0048BC6Dh
  0048b954  66837e1200            cmp       word [esi+12h],0
  0048b959  0f8581000000          jne       near 0048B9E0h
  0048b95f  8b0e                  mov       ecx,[esi]
  0048b961  f6c101                test      cl,1
  0048b964  7406                  je        short 0048B96Ch
  0048b966  c6464300              mov       byte [esi+43h],0
  0048b96a  eb74                  jmp       short 0048B9E0h

LAB_0048b96c:
  0048b96c  8b542474              mov       edx,[esp+74h]
  0048b970  81e1fffffffe          and       ecx,0FEFFFFFFh
  0048b976  890e                  mov       [esi],ecx
  0048b978  66c746180200          mov       word [esi+18h],2
  0048b97e  8d8a00010000          lea       ecx,[edx+100h]
  0048b984  66c7461a0000          mov       word [esi+1Ah],0
  0048b98a  c1e106                shl       ecx,6
  0048b98d  0fafc1                imul      eax,ecx
  0048b990  c1f80c                sar       eax,0Ch
  0048b993  8bf8                  mov       edi,eax
  0048b995  894e5c                mov       [esi+5Ch],ecx
  0048b998  0fbf4610              movsx     eax,word [esi+10h]
  0048b99c  50                    push      eax
  0048b99d  e87aa00b00            call      00545A1Ch
  0048b9a2  0fafc7                imul      eax,edi
  0048b9a5  0fbf4e10              movsx     ecx,word [esi+10h]
  0048b9a9  8b6e50                mov       ebp,[esi+50h]
  0048b9ac  51                    push      ecx
  0048b9ad  c1f80c                sar       eax,0Ch
  0048b9b0  03e8                  add       ebp,eax
  0048b9b2  896e50                mov       [esi+50h],ebp
  0048b9b5  e85ca00b00            call      00545A16h
  0048b9ba  0fafc7                imul      eax,edi
  0048b9bd  8b5658                mov       edx,[esi+58h]
  0048b9c0  8b4e54                mov       ecx,[esi+54h]
  0048b9c3  c1f80c                sar       eax,0Ch
  0048b9c6  03d0                  add       edx,eax
  0048b9c8  8b06                  mov       eax,[esi]
  0048b9ca  81c100e00100          add       ecx,1E000h
  0048b9d0  0c01                  or        al,1
  0048b9d2  895658                mov       [esi+58h],edx
  0048b9d5  894e54                mov       [esi+54h],ecx
  0048b9d8  8906                  mov       [esi],eax
  0048b9da  66c746120100          mov       word [esi+12h],1

LAB_0048b9e0:
  0048b9e0  f60601                test      byte [esi],1
  0048b9e3  0f8584020000          jne       near 0048BC6Dh
  0048b9e9  c6464300              mov       byte [esi+43h],0
  0048b9ed  e97b020000            jmp       0048BC6Dh
  0048b9f2  33db                  xor       ebx,ebx
  0048b9f4  66395e12              cmp       [esi+12h],bx
  0048b9f8  7530                  jne       short 0048BA2Ah
  0048b9fa  8b06                  mov       eax,[esi]
  0048b9fc  a801                  test      al,1
  0048b9fe  7405                  je        short 0048BA05h
  0048ba00  885e43                mov       [esi+43h],bl
  0048ba03  eb25                  jmp       short 0048BA2Ah

LAB_0048ba05:
  0048ba05  8b4e54                mov       ecx,[esi+54h]
  0048ba08  25fffffffe            and       eax,0FEFFFFFFh
  0048ba0d  81c100800200          add       ecx,28000h
  0048ba13  0c01                  or        al,1
  0048ba15  66c746180200          mov       word [esi+18h],2
  0048ba1b  66895e1a              mov       [esi+1Ah],bx
  0048ba1f  894e54                mov       [esi+54h],ecx
  0048ba22  8906                  mov       [esi],eax
  0048ba24  66c746120100          mov       word [esi+12h],1

LAB_0048ba2a:
  0048ba2a  66837e1201            cmp       word [esi+12h],1
  0048ba2f  7579                  jne       short 0048BAAAh
  0048ba31  8b4d00                mov       ecx,[ebp]
  0048ba34  66c746180200          mov       word [esi+18h],2
  0048ba3a  66895e1a              mov       [esi+1Ah],bx
  0048ba3e  8b571c                mov       edx,[edi+1Ch]
  0048ba41  2bd1                  sub       edx,ecx
  0048ba43  c1fa04                sar       edx,4
  0048ba46  895650                mov       [esi+50h],edx
  0048ba49  8b5624                mov       edx,[esi+24h]
  0048ba4c  8b4724                mov       eax,[edi+24h]
  0048ba4f  2bc2                  sub       eax,edx
  0048ba51  c1f804                sar       eax,4
  0048ba54  894658                mov       [esi+58h],eax
  0048ba57  8a06                  mov       al,[esi]
  0048ba59  a801                  test      al,1
  0048ba5b  753d                  jne       short 0048BA9Ah
  0048ba5d  8b471c                mov       eax,[edi+1Ch]
  0048ba60  2bc1                  sub       eax,ecx
  0048ba62  8b4f24                mov       ecx,[edi+24h]
  0048ba65  c1f80c                sar       eax,0Ch
  0048ba68  2bca                  sub       ecx,edx
  0048ba6a  8bd0                  mov       edx,eax
  0048ba6c  c1f90c                sar       ecx,0Ch
  0048ba6f  0fafd0                imul      edx,eax
  0048ba72  8bc1                  mov       eax,ecx
  0048ba74  0fafc1                imul      eax,ecx
  0048ba77  03d0                  add       edx,eax
  0048ba79  52                    push      edx
  0048ba7a  e8919f0b00            call      00545A10h
  0048ba7f  83f864                cmp       eax,64h
  0048ba82  89442410              mov       [esp+10h],eax
  0048ba86  7e06                  jle       short 0048BA8Eh
  0048ba88  c6464300              mov       byte [esi+43h],0
  0048ba8c  eb0c                  jmp       short 0048BA9Ah

LAB_0048ba8e:
  0048ba8e  66c746120200          mov       word [esi+12h],2
  0048ba94  66c746146400          mov       word [esi+14h],64h

LAB_0048ba9a:
  0048ba9a  f7869000000082ad0020  test      dword [esi+90h],2000AD82h
  0048baa4  7404                  je        short 0048BAAAh
  0048baa6  c6464300              mov       byte [esi+43h],0

LAB_0048baaa:
  0048baaa  668b4612              mov       ax,[esi+12h]
  0048baae  663d0200              cmp       ax,2
  0048bab2  0f8cb5010000          jl        near 0048BC6Dh
  0048bab8  8b4f1c                mov       ecx,[edi+1Ch]
  0048babb  8b5500                mov       edx,[ebp]
  0048babe  2bca                  sub       ecx,edx
  0048bac0  d1f9                  sar       ecx,1
  0048bac2  894e50                mov       [esi+50h],ecx
  0048bac5  8b5724                mov       edx,[edi+24h]
  0048bac8  8b4e24                mov       ecx,[esi+24h]
  0048bacb  0fbfc0                movsx     eax,ax
  0048bace  2bd1                  sub       edx,ecx
  0048bad0  50                    push      eax
  0048bad1  d1fa                  sar       edx,1
  0048bad3  895658                mov       [esi+58h],edx
  0048bad6  e8419f0b00            call      00545A1Ch
  0048badb  0fbf4e12              movsx     ecx,word [esi+12h]
  0048badf  0500200000            add       eax,2000h
  0048bae4  51                    push      ecx
  0048bae5  6689462a              mov       [esi+2Ah],ax
  0048bae9  e8289f0b00            call      00545A16h
  0048baee  0fbf5612              movsx     edx,word [esi+12h]
  0048baf2  c1f802                sar       eax,2
  0048baf5  0500100000            add       eax,1000h
  0048bafa  52                    push      edx
  0048bafb  66894628              mov       [esi+28h],ax
  0048baff  e8129f0b00            call      00545A16h
  0048bb04  66814612c800          add       word [esi+12h],0C8h
  0048bb0a  895e5c                mov       [esi+5Ch],ebx
  0048bb0d  c1f802                sar       eax,2
  0048bb10  0500100000            add       eax,1000h
  0048bb15  6689462c              mov       [esi+2Ch],ax
  0048bb19  668b4612              mov       ax,[esi+12h]
  0048bb1d  663d0010              cmp       ax,1000h
  0048bb21  7c09                  jl        short 0048BB2Ch
  0048bb23  0500f0ffff            add       eax,0FFFFF000h
  0048bb28  66894612              mov       [esi+12h],ax

LAB_0048bb2c:
  0048bb2c  668b4612              mov       ax,[esi+12h]
  0048bb30  663d0200              cmp       ax,2
  0048bb34  7f07                  jg        short 0048BB3Dh
  0048bb36  83c002                add       eax,2
  0048bb39  66894612              mov       [esi+12h],ax

LAB_0048bb3d:
  0048bb3d  33c0                  xor       eax,eax
  0048bb3f  8a4708                mov       al,[edi+8]
  0048bb42  8d0cc0                lea       ecx,[eax+eax*8]
  0048bb45  8d1448                lea       edx,[eax+ecx*2]
  0048bb48  33c0                  xor       eax,eax
  0048bb4a  8a049588915e00        mov       al,[edx*4+5E9188h]
  0048bb51  c1e004                shl       eax,4
  0048bb54  f680224a6000f0        test      byte [eax+604A22h],0F0h
  0048bb5b  7411                  je        short 0048BB6Eh
  0048bb5d  66834614fd            add       word [esi+14h],0FFFDh
  0048bb62  66395e14              cmp       [esi+14h],bx
  0048bb66  7d06                  jge       short 0048BB6Eh
  0048bb68  66c746140100          mov       word [esi+14h],1

LAB_0048bb6e:
  0048bb6e  f70700000040          test      dword [edi],40000000h
  0048bb74  7406                  je        short 0048BB7Ch
  0048bb76  66c746140100          mov       word [esi+14h],1

LAB_0048bb7c:
  0048bb7c  66ff4e14              dec       word [esi+14h]
  0048bb80  66395e14              cmp       [esi+14h],bx
  0048bb84  0f85e3000000          jne       near 0048BC6Dh
  0048bb8a  c6464300              mov       byte [esi+43h],0
  0048bb8e  66c7462a0010          mov       word [esi+2Ah],1000h
  0048bb94  e9d4000000            jmp       0048BC6Dh
  0048bb99  668b4e12              mov       cx,[esi+12h]
  0048bb9d  bf01000000            mov       edi,1
  0048bba2  663bcf                cmp       cx,di
  0048bba5  7545                  jne       short 0048BBECh
  0048bba7  8b465c                mov       eax,[esi+5Ch]
  0048bbaa  66c746180200          mov       word [esi+18h],2
  0048bbb0  8bd0                  mov       edx,eax
  0048bbb2  66897e1a              mov       [esi+1Ah],di
  0048bbb6  c1fa03                sar       edx,3
  0048bbb9  2bc2                  sub       eax,edx
  0048bbbb  3d00500000            cmp       eax,5000h
  0048bbc0  89465c                mov       [esi+5Ch],eax
  0048bbc3  7d18                  jge       short 0048BBDDh
  0048bbc5  b800100000            mov       eax,1000h
  0048bbca  c6464300              mov       byte [esi+43h],0
  0048bbce  c7465c00000000        mov       dword [esi+5Ch],0
  0048bbd5  6689462c              mov       [esi+2Ch],ax
  0048bbd9  6689462a              mov       [esi+2Ah],ax

LAB_0048bbdd:
  0048bbdd  8b465c                mov       eax,[esi+5Ch]
  0048bbe0  c1f805                sar       eax,5
  0048bbe3  0500100000            add       eax,1000h
  0048bbe8  6689462c              mov       [esi+2Ch],ax

LAB_0048bbec:
  0048bbec  6685c9                test      cx,cx
  0048bbef  757c                  jne       short 0048BC6Dh
  0048bbf1  66c746180200          mov       word [esi+18h],2
  0048bbf7  66897e1a              mov       [esi+1Ah],di
  0048bbfb  e840b50b00            call      00547140h
  0048bc00  99                    cdq
  0048bc01  b932000000            mov       ecx,32h
  0048bc06  66897e12              mov       [esi+12h],di
  0048bc0a  f7f9                  idiv      ecx
  0048bc0c  8b442474              mov       eax,[esp+74h]
  0048bc10  24fc                  and       al,0FCh
  0048bc12  8d0c90                lea       ecx,[eax+edx*4]
  0048bc15  c1e10a                shl       ecx,0Ah
  0048bc18  894e5c                mov       [esi+5Ch],ecx
  0048bc1b  eb50                  jmp       short 0048BC6Dh
  0048bc1d  668b4e32              mov       cx,[esi+32h]
  0048bc21  33ff                  xor       edi,edi
  0048bc23  0fbf4610              movsx     eax,word [esi+10h]
  0048bc27  0fbfd1                movsx     edx,cx
  0048bc2a  8bd8                  mov       ebx,eax
  0048bc2c  66897e18              mov       [esi+18h],di
  0048bc30  2bda                  sub       ebx,edx
  0048bc32  8bd0                  mov       edx,eax
  0048bc34  c1fb03                sar       ebx,3
  0048bc37  03cb                  add       ecx,ebx
  0048bc39  66894e32              mov       [esi+32h],cx
  0048bc3d  0fbfc9                movsx     ecx,cx
  0048bc40  2bd1                  sub       edx,ecx
  0048bc42  7804                  js        short 0048BC48h
  0048bc44  8bca                  mov       ecx,edx
  0048bc46  eb02                  jmp       short 0048BC4Ah

LAB_0048bc48:
  0048bc48  2bc8                  sub       ecx,eax

LAB_0048bc4a:
  0048bc4a  83f932                cmp       ecx,32h
  0048bc4d  7d04                  jge       short 0048BC53h
  0048bc4f  c6464300              mov       byte [esi+43h],0

LAB_0048bc53:
  0048bc53  668b462c              mov       ax,[esi+2Ch]
  0048bc57  ba00100000            mov       edx,1000h
  0048bc5c  0fbfc8                movsx     ecx,ax
  0048bc5f  2bd1                  sub       edx,ecx
  0048bc61  897e5c                mov       [esi+5Ch],edi
  0048bc64  c1fa02                sar       edx,2
  0048bc67  03d0                  add       edx,eax
  0048bc69  6689562c              mov       [esi+2Ch],dx

LAB_0048bc6d:
  0048bc6d  0fbf4632              movsx     eax,word [esi+32h]
  0048bc71  50                    push      eax
  0048bc72  e8a59d0b00            call      00545A1Ch
  0048bc77  0fbf4e32              movsx     ecx,word [esi+32h]
  0048bc7b  51                    push      ecx
  0048bc7c  8944244c              mov       [esp+4Ch],eax
  0048bc80  e8919d0b00            call      00545A16h
  0048bc85  8b5650                mov       edx,[esi+50h]
  0048bc88  89442450              mov       [esp+50h],eax
  0048bc8c  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  0048bc93  8bc2                  mov       eax,edx
  0048bc95  8bfa                  mov       edi,edx
  0048bc97  c1f803                sar       eax,3
  0048bc9a  8b4c2414              mov       ecx,[esp+14h]
  0048bc9e  2bf8                  sub       edi,eax
  0048bca0  c1fa08                sar       edx,8
  0048bca3  0faf4e5c              imul      ecx,[esi+5Ch]
  0048bca7  2bfa                  sub       edi,edx
  0048bca9  8b5654                mov       edx,[esi+54h]
  0048bcac  8bc5                  mov       eax,ebp
  0048bcae  8bda                  mov       ebx,edx
  0048bcb0  c1f808                sar       eax,8
  0048bcb3  c1fb08                sar       ebx,8
  0048bcb6  2bc3                  sub       eax,ebx
  0048bcb8  897e50                mov       [esi+50h],edi
  0048bcbb  2bc5                  sub       eax,ebp
  0048bcbd  56                    push      esi
  0048bcbe  03c2                  add       eax,edx
  0048bcc0  8b5658                mov       edx,[esi+58h]
  0048bcc3  8bda                  mov       ebx,edx
  0048bcc5  8bea                  mov       ebp,edx
  0048bcc7  c1fb08                sar       ebx,8
  0048bcca  2beb                  sub       ebp,ebx
  0048bccc  8b5e20                mov       ebx,[esi+20h]
  0048bccf  c1fa03                sar       edx,3
  0048bcd2  c1f90c                sar       ecx,0Ch
  0048bcd5  2bea                  sub       ebp,edx
  0048bcd7  8bd1                  mov       edx,ecx
  0048bcd9  0faf54244c            imul      edx,[esp+4Ch]
  0048bcde  0faf4c2454            imul      ecx,[esp+54h]
  0048bce3  c1fa0c                sar       edx,0Ch
  0048bce6  c1f90c                sar       ecx,0Ch
  0048bce9  894654                mov       [esi+54h],eax
  0048bcec  03d7                  add       edx,edi
  0048bcee  8b7e1c                mov       edi,[esi+1Ch]
  0048bcf1  03d8                  add       ebx,eax
  0048bcf3  8b4624                mov       eax,[esi+24h]
  0048bcf6  03cd                  add       ecx,ebp
  0048bcf8  03fa                  add       edi,edx
  0048bcfa  03c1                  add       eax,ecx
  0048bcfc  896e58                mov       [esi+58h],ebp
  0048bcff  897e1c                mov       [esi+1Ch],edi
  0048bd02  895e20                mov       [esi+20h],ebx
  0048bd05  894624                mov       [esi+24h],eax
  0048bd08  e8337c0a00            call      00533940h
  0048bd0d  8bbe90000000          mov       edi,[esi+90h]
  0048bd13  8b467c                mov       eax,[esi+7Ch]
  0048bd16  81e7fffcffff          and       edi,0FFFFFCFFh
  0048bd1c  33d2                  xor       edx,edx
  0048bd1e  89be90000000          mov       [esi+90h],edi
  0048bd24  8a500f                mov       dl,[eax+0Fh]
  0048bd27  8bc2                  mov       eax,edx
  0048bd29  33db                  xor       ebx,ebx
  0048bd2b  83c404                add       esp,4
  0048bd2e  3bc3                  cmp       eax,ebx
  0048bd30  8bcf                  mov       ecx,edi
  0048bd32  7416                  je        short 0048BD4Ah
  0048bd34  33d2                  xor       edx,edx
  0048bd36  48                    dec       eax
  0048bd37  8a1538e05d00          mov       dl,[5DE038h]
  0048bd3d  3bd0                  cmp       edx,eax
  0048bd3f  7409                  je        short 0048BD4Ah
  0048bd41  80cd08                or        ch,8
  0048bd44  898e90000000          mov       [esi+90h],ecx

LAB_0048bd4a:
  0048bd4a  8b06                  mov       eax,[esi]
  0048bd4c  8bd0                  mov       edx,eax
  0048bd4e  83e201                and       edx,1
  0048bd51  740a                  je        short 0048BD5Dh
  0048bd53  8b4e60                mov       ecx,[esi+60h]
  0048bd56  8b7e20                mov       edi,[esi+20h]
  0048bd59  3bf9                  cmp       edi,ecx
  0048bd5b  7e1c                  jle       short 0048BD79h

LAB_0048bd5d:
  0048bd5d  3bd3                  cmp       edx,ebx
  0048bd5f  0f853a030000          jne       near 0048C09Fh
  0048bd65  8b7e20                mov       edi,[esi+20h]
  0048bd68  8b4e60                mov       ecx,[esi+60h]
  0048bd6b  81ef00c00000          sub       edi,0C000h
  0048bd71  3bf9                  cmp       edi,ecx
  0048bd73  0f8f14030000          jg        near 0048C08Dh

LAB_0048bd79:
  0048bd79  f7869000000000a00000  test      dword [esi+90h],0A000h
  0048bd83  0f8504030000          jne       near 0048C08Dh
  0048bd89  24fe                  and       al,0FEh
  0048bd8b  894e20                mov       [esi+20h],ecx
  0048bd8e  8906                  mov       [esi],eax
  0048bd90  895e54                mov       [esi+54h],ebx

LAB_0048bd93:
  0048bd93  b800100000            mov       eax,1000h

LAB_0048bd98:
  0048bd98  66399e96000000        cmp       [esi+96h],bx
  0048bd9f  740c                  je        short 0048BDADh
  0048bda1  66894628              mov       [esi+28h],ax
  0048bda5  6689462a              mov       [esi+2Ah],ax
  0048bda9  6689462c              mov       [esi+2Ch],ax

LAB_0048bdad:
  0048bdad  6aff                  push      0FFFFFFFFh
  0048bdaf  6aff                  push      0FFFFFFFFh
  0048bdb1  6aff                  push      0FFFFFFFFh
  0048bdb3  56                    push      esi
  0048bdb4  e8577a0300            call      004C3810h
  0048bdb9  8b442424              mov       eax,[esp+24h]
  0048bdbd  83c410                add       esp,10h
  0048bdc0  3bc3                  cmp       eax,ebx
  0048bdc2  7511                  jne       short 0048BDD5h
  0048bdc4  668b54245e            mov       dx,[esp+5Eh]
  0048bdc9  66895632              mov       [esi+32h],dx
  0048bdcd  5f                    pop       edi
  0048bdce  5e                    pop       esi
  0048bdcf  5d                    pop       ebp
  0048bdd0  5b                    pop       ebx
  0048bdd1  83c460                add       esp,60h
  0048bdd4  c3                    ret

LAB_0048bdd5:
  0048bdd5  385e43                cmp       [esi+43h],bl
  0048bdd8  0f85a7020000          jne       near 0048C085h
  0048bdde  8b466c                mov       eax,[esi+6Ch]
  0048bde1  8b6e1c                mov       ebp,[esi+1Ch]
  0048bde4  8b4e24                mov       ecx,[esi+24h]
  0048bde7  8b781c                mov       edi,[eax+1Ch]
  0048bdea  2bfd                  sub       edi,ebp
  0048bdec  8b6824                mov       ebp,[eax+24h]
  0048bdef  2be9                  sub       ebp,ecx
  0048bdf1  c1ff0c                sar       edi,0Ch
  0048bdf4  c1fd0c                sar       ebp,0Ch
  0048bdf7  8bc7                  mov       eax,edi
  0048bdf9  8bcd                  mov       ecx,ebp
  0048bdfb  0fafc7                imul      eax,edi
  0048bdfe  0fafcd                imul      ecx,ebp
  0048be01  03c1                  add       eax,ecx
  0048be03  50                    push      eax
  0048be04  e8079c0b00            call      00545A10h
  0048be09  55                    push      ebp
  0048be0a  57                    push      edi
  0048be0b  89442418              mov       [esp+18h],eax
  0048be0f  e80e9c0b00            call      00545A22h
  0048be14  668b5632              mov       dx,[esi+32h]
  0048be18  66895e12              mov       [esi+12h],bx
  0048be1c  2bc2                  sub       eax,edx
  0048be1e  bd50000000            mov       ebp,50h
  0048be23  25ff0f0000            and       eax,0FFFh
  0048be28  8944241c              mov       [esp+1Ch],eax
  0048be2c  8b442410              mov       eax,[esp+10h]
  0048be30  3dfa000000            cmp       eax,0FAh
  0048be35  0f8dab000000          jge       near 0048BEE6h
  0048be3b  e800b30b00            call      00547140h
  0048be40  99                    cdq
  0048be41  b964000000            mov       ecx,64h
  0048be46  f7f9                  idiv      ecx
  0048be48  83fa1e                cmp       edx,1Eh
  0048be4b  7d27                  jge       short 0048BE74h
  0048be4d  c6464202              mov       byte [esi+42h],2
  0048be51  e8eab20b00            call      00547140h
  0048be56  99                    cdq
  0048be57  b90a000000            mov       ecx,0Ah
  0048be5c  8b7c2418              mov       edi,[esp+18h]
  0048be60  f7f9                  idiv      ecx
  0048be62  8bc7                  mov       eax,edi
  0048be64  66896e10              mov       [esi+10h],bp
  0048be68  d1f8                  sar       eax,1
  0048be6a  02d0                  add       dl,al
  0048be6c  80c214                add       dl,14h
  0048be6f  885643                mov       [esi+43h],dl
  0048be72  eb76                  jmp       short 0048BEEAh

LAB_0048be74:
  0048be74  83fa32                cmp       edx,32h
  0048be77  7d23                  jge       short 0048BE9Ch
  0048be79  8b7c2418              mov       edi,[esp+18h]
  0048be7d  8b542474              mov       edx,[esp+74h]
  0048be81  8bcf                  mov       ecx,edi
  0048be83  885e42                mov       [esi+42h],bl
  0048be86  d1f9                  sar       ecx,1
  0048be88  80c10a                add       cl,0Ah
  0048be8b  8d441250              lea       eax,[edx+edx+50h]
  0048be8f  884e43                mov       [esi+43h],cl
  0048be92  66894610              mov       [esi+10h],ax
  0048be96  66895e12              mov       [esi+12h],bx
  0048be9a  eb4e                  jmp       short 0048BEEAh

LAB_0048be9c:
  0048be9c  83fa5a                cmp       edx,5Ah
  0048be9f  7d26                  jge       short 0048BEC7h
  0048bea1  8b4c2474              mov       ecx,[esp+74h]
  0048bea5  8b442410              mov       eax,[esp+10h]
  0048bea9  83c164                add       ecx,64h
  0048beac  3bc1                  cmp       eax,ecx
  0048beae  7e17                  jle       short 0048BEC7h
  0048beb0  c6464207              mov       byte [esi+42h],7
  0048beb4  c6464301              mov       byte [esi+43h],1
  0048beb8  e883b20b00            call      00547140h
  0048bebd  66894610              mov       [esi+10h],ax
  0048bec1  66895e12              mov       [esi+12h],bx
  0048bec5  eb1f                  jmp       short 0048BEE6h

LAB_0048bec7:
  0048bec7  c6464209              mov       byte [esi+42h],9
  0048becb  e870b20b00            call      00547140h
  0048bed0  99                    cdq
  0048bed1  33c2                  xor       eax,edx
  0048bed3  c6464301              mov       byte [esi+43h],1
  0048bed7  2bc2                  sub       eax,edx
  0048bed9  25ff0f0000            and       eax,0FFFh
  0048bede  33c2                  xor       eax,edx
  0048bee0  2bc2                  sub       eax,edx
  0048bee2  66894610              mov       [esi+10h],ax

LAB_0048bee6:
  0048bee6  8b7c2418              mov       edi,[esp+18h]

LAB_0048beea:
  0048beea  8b442410              mov       eax,[esp+10h]
  0048beee  3d58020000            cmp       eax,258h
  0048bef3  0f8dc8000000          jge       near 0048BFC1h
  0048bef9  3dfa000000            cmp       eax,0FAh
  0048befe  0f8cbd000000          jl        near 0048BFC1h
  0048bf04  e837b20b00            call      00547140h
  0048bf09  99                    cdq
  0048bf0a  b964000000            mov       ecx,64h
  0048bf0f  f7f9                  idiv      ecx
  0048bf11  83fa14                cmp       edx,14h
  0048bf14  7d25                  jge       short 0048BF3Bh
  0048bf16  8b54241c              mov       edx,[esp+1Ch]
  0048bf1a  81c200040000          add       edx,400h
  0048bf20  81e2ff0f0000          and       edx,0FFFh
  0048bf26  81fa00080000          cmp       edx,800h
  0048bf2c  7c69                  jl        short 0048BF97h
  0048bf2e  c6464201              mov       byte [esi+42h],1
  0048bf32  c6464301              mov       byte [esi+43h],1
  0048bf36  e986000000            jmp       0048BFC1h

LAB_0048bf3b:
  0048bf3b  83fa28                cmp       edx,28h
  0048bf3e  7d23                  jge       short 0048BF63h
  0048bf40  c6464202              mov       byte [esi+42h],2
  0048bf44  e8f7b10b00            call      00547140h
  0048bf49  99                    cdq
  0048bf4a  b90a000000            mov       ecx,0Ah
  0048bf4f  66896e10              mov       [esi+10h],bp
  0048bf53  f7f9                  idiv      ecx
  0048bf55  8bc7                  mov       eax,edi
  0048bf57  d1f8                  sar       eax,1
  0048bf59  02d0                  add       dl,al
  0048bf5b  80c214                add       dl,14h
  0048bf5e  885643                mov       [esi+43h],dl
  0048bf61  eb5e                  jmp       short 0048BFC1h

LAB_0048bf63:
  0048bf63  83fa3c                cmp       edx,3Ch
  0048bf66  7d21                  jge       short 0048BF89h
  0048bf68  c6464209              mov       byte [esi+42h],9
  0048bf6c  e8cfb10b00            call      00547140h
  0048bf71  99                    cdq
  0048bf72  33c2                  xor       eax,edx
  0048bf74  c6464301              mov       byte [esi+43h],1
  0048bf78  2bc2                  sub       eax,edx
  0048bf7a  25ff0f0000            and       eax,0FFFh
  0048bf7f  33c2                  xor       eax,edx
  0048bf81  2bc2                  sub       eax,edx
  0048bf83  66894610              mov       [esi+10h],ax
  0048bf87  eb38                  jmp       short 0048BFC1h

LAB_0048bf89:
  0048bf89  3bd5                  cmp       edx,ebp
  0048bf8b  7d0a                  jge       short 0048BF97h
  0048bf8d  c6464208              mov       byte [esi+42h],8
  0048bf91  c6464301              mov       byte [esi+43h],1
  0048bf95  eb2a                  jmp       short 0048BFC1h

LAB_0048bf97:
  0048bf97  885e42                mov       [esi+42h],bl
  0048bf9a  e8a1b10b00            call      00547140h
  0048bf9f  99                    cdq
  0048bfa0  b90a000000            mov       ecx,0Ah
  0048bfa5  66895e12              mov       [esi+12h],bx
  0048bfa9  f7f9                  idiv      ecx
  0048bfab  8b4c2474              mov       ecx,[esp+74h]
  0048bfaf  8bc7                  mov       eax,edi
  0048bfb1  c1f802                sar       eax,2
  0048bfb4  02d0                  add       dl,al
  0048bfb6  885643                mov       [esi+43h],dl
  0048bfb9  8d540950              lea       edx,[ecx+ecx+50h]
  0048bfbd  66895610              mov       [esi+10h],dx

LAB_0048bfc1:
  0048bfc1  817c241058020000      cmp       dword [esp+10h],258h
  0048bfc9  0f8cb6000000          jl        near 0048C085h
  0048bfcf  e86cb10b00            call      00547140h
  0048bfd4  99                    cdq
  0048bfd5  b964000000            mov       ecx,64h
  0048bfda  f7f9                  idiv      ecx
  0048bfdc  83fa32                cmp       edx,32h
  0048bfdf  7d69                  jge       short 0048C04Ah
  0048bfe1  8b442474              mov       eax,[esp+74h]
  0048bfe5  8d4c000a              lea       ecx,[eax+eax+0Ah]
  0048bfe9  3bd1                  cmp       edx,ecx
  0048bfeb  7d2b                  jge       short 0048C018h
  0048bfed  c6464202              mov       byte [esi+42h],2
  0048bff1  e84ab10b00            call      00547140h
  0048bff6  99                    cdq
  0048bff7  b90a000000            mov       ecx,0Ah
  0048bffc  66c746106400          mov       word [esi+10h],64h
  0048c002  f7f9                  idiv      ecx
  0048c004  8bc7                  mov       eax,edi
  0048c006  d1f8                  sar       eax,1
  0048c008  02d0                  add       dl,al
  0048c00a  80c214                add       dl,14h
  0048c00d  885643                mov       [esi+43h],dl
  0048c010  5f                    pop       edi
  0048c011  5e                    pop       esi
  0048c012  5d                    pop       ebp
  0048c013  5b                    pop       ebx
  0048c014  83c460                add       esp,60h
  0048c017  c3                    ret

LAB_0048c018:
  0048c018  885e42                mov       [esi+42h],bl
  0048c01b  e820b10b00            call      00547140h
  0048c020  99                    cdq
  0048c021  b90a000000            mov       ecx,0Ah
  0048c026  66895e12              mov       [esi+12h],bx
  0048c02a  f7f9                  idiv      ecx
  0048c02c  8b4c2474              mov       ecx,[esp+74h]
  0048c030  8bc7                  mov       eax,edi
  0048c032  c1f802                sar       eax,2
  0048c035  02d0                  add       dl,al
  0048c037  885643                mov       [esi+43h],dl
  0048c03a  8d540950              lea       edx,[ecx+ecx+50h]
  0048c03e  66895610              mov       [esi+10h],dx
  0048c042  5f                    pop       edi
  0048c043  5e                    pop       esi
  0048c044  5d                    pop       ebp
  0048c045  5b                    pop       ebx
  0048c046  83c460                add       esp,60h
  0048c049  c3                    ret

LAB_0048c04a:
  0048c04a  83fa46                cmp       edx,46h
  0048c04d  7d10                  jge       short 0048C05Fh
  0048c04f  c6464208              mov       byte [esi+42h],8
  0048c053  c6464301              mov       byte [esi+43h],1
  0048c057  5f                    pop       edi
  0048c058  5e                    pop       esi
  0048c059  5d                    pop       ebp
  0048c05a  5b                    pop       ebx
  0048c05b  83c460                add       esp,60h
  0048c05e  c3                    ret

LAB_0048c05f:
  0048c05f  83fa5a                cmp       edx,5Ah
  0048c062  7d10                  jge       short 0048C074h
  0048c064  c6464201              mov       byte [esi+42h],1
  0048c068  c6464301              mov       byte [esi+43h],1
  0048c06c  5f                    pop       edi
  0048c06d  5e                    pop       esi
  0048c06e  5d                    pop       ebp
  0048c06f  5b                    pop       ebx
  0048c070  83c460                add       esp,60h
  0048c073  c3                    ret

LAB_0048c074:
  0048c074  c6464206              mov       byte [esi+42h],6
  0048c078  c6464301              mov       byte [esi+43h],1
  0048c07c  e8bfb00b00            call      00547140h
  0048c081  66894610              mov       [esi+10h],ax

LAB_0048c085:
  0048c085  5f                    pop       edi
  0048c086  5e                    pop       esi
  0048c087  5d                    pop       ebp
  0048c088  5b                    pop       ebx
  0048c089  83c460                add       esp,60h
  0048c08c  c3                    ret

LAB_0048c08d:
  0048c08d  3bd3                  cmp       edx,ebx
  0048c08f  750e                  jne       short 0048C09Fh
  0048c091  8b4e20                mov       ecx,[esi+20h]
  0048c094  8b5660                mov       edx,[esi+60h]
  0048c097  3bca                  cmp       ecx,edx
  0048c099  7e04                  jle       short 0048C09Fh
  0048c09b  0c01                  or        al,1
  0048c09d  8906                  mov       [esi],eax

LAB_0048c09f:
  0048c09f  f7869000000082ad0020  test      dword [esi+90h],2000AD82h
  0048c0a9  0f84e4fcffff          je        near 0048BD93h
  0048c0af  f60601                test      byte [esi],1
  0048c0b2  7552                  jne       short 0048C106h
  0048c0b4  668b7e32              mov       di,[esi+32h]
  0048c0b8  c6464209              mov       byte [esi+42h],9
  0048c0bc  c6464301              mov       byte [esi+43h],1
  0048c0c0  e87bb00b00            call      00547140h
  0048c0c5  99                    cdq
  0048c0c6  33c2                  xor       eax,edx
  0048c0c8  2bc2                  sub       eax,edx
  0048c0ca  25ff030000            and       eax,3FFh
  0048c0cf  33c2                  xor       eax,edx
  0048c0d1  2bc2                  sub       eax,edx
  0048c0d3  8d943800040000        lea       edx,[eax+edi+400h]
  0048c0da  b800100000            mov       eax,1000h
  0048c0df  663bf8                cmp       di,ax
  0048c0e2  66895610              mov       [esi+10h],dx
  0048c0e6  7c0a                  jl        short 0048C0F2h
  0048c0e8  81c700f0ffff          add       edi,0FFFFF000h
  0048c0ee  66897e32              mov       [esi+32h],di

LAB_0048c0f2:
  0048c0f2  66895e12              mov       [esi+12h],bx
  0048c0f6  6689462c              mov       [esi+2Ch],ax
  0048c0fa  6689462a              mov       [esi+2Ah],ax
  0048c0fe  895e5c                mov       [esi+5Ch],ebx
  0048c101  e992fcffff            jmp       0048BD98h

LAB_0048c106:
  0048c106  8b44243c              mov       eax,[esp+3Ch]
  0048c10a  8b4c2444              mov       ecx,[esp+44h]
  0048c10e  89461c                mov       [esi+1Ch],eax
  0048c111  b800100000            mov       eax,1000h
  0048c116  895e5c                mov       [esi+5Ch],ebx
  0048c119  895e50                mov       [esi+50h],ebx
  0048c11c  895e58                mov       [esi+58h],ebx
  0048c11f  894e24                mov       [esi+24h],ecx
  0048c122  885e42                mov       [esi+42h],bl
  0048c125  c6464328              mov       byte [esi+43h],28h
  0048c129  66895e12              mov       [esi+12h],bx
  0048c12d  6689462c              mov       [esi+2Ch],ax
  0048c131  6689462a              mov       [esi+2Ah],ax
  0048c135  895e5c                mov       [esi+5Ch],ebx
  0048c138  e95bfcffff            jmp       0048BD98h
  0048c13d  90                    nop
  0048c13e  8bff                  mov       edi,edi
  0048c140  f5                    cmc
  0048c141  b648                  mov       dh,48h
  0048c143  0042b7                add       [edx-49h],al
  0048c146  48                    dec       eax
  0048c147  00c1                  add       cl,al
  0048c149  b748                  mov       bh,48h
  0048c14b  0072b8                add       [edx-48h],dh
  0048c14e  48                    dec       eax
  0048c14f  006dbc                add       [ebp-44h],ch
  0048c152  48                    dec       eax
  0048c153  006dbc                add       [ebp-44h],ch
  0048c156  48                    dec       eax
  0048c157  0054b948              add       [ecx+edi*4+48h],dl
  0048c15b  00f2                  add       dl,dh
  0048c15d  b9480099bb            mov       ecx,0BB990048h
  0048c162  48                    dec       eax
  0048c163  001dbc480090          add       [900048BCh],bl
  0048c169  90                    nop
  0048c16a  90                    nop
  0048c16b  90                    nop
  0048c16c  90                    nop
  0048c16d  90                    nop
  0048c16e  90                    nop
  0048c16f  90                    nop

62 cible(s) de saut interne : 0x0048b57a, 0x0048b63b, 0x0048b65e, 0x0048b6b2, 0x0048b6d4, 0x0048b6dc, 0x0048b6e0, 0x0048b717, 0x0048b72a, 0x0048b765, 0x0048b789, 0x0048b7aa, 0x0048b7b9, 0x0048b7f8, 0x0048b81c, 0x0048b82b, 0x0048b897, 0x0048b907, 0x0048b910, 0x0048b96c, 0x0048b9e0, 0x0048ba05, 0x0048ba2a, 0x0048ba8e, 0x0048ba9a, 0x0048baaa, 0x0048bb2c, 0x0048bb3d, 0x0048bb6e, 0x0048bb7c, 0x0048bbdd, 0x0048bbec, 0x0048bc48, 0x0048bc4a, 0x0048bc53, 0x0048bc6d, 0x0048bd4a, 0x0048bd5d, 0x0048bd79, 0x0048bd93, 0x0048bd98, 0x0048bdad, 0x0048bdd5, 0x0048be74, 0x0048be9c, 0x0048bec7, 0x0048bee6, 0x0048beea, 0x0048bf3b, 0x0048bf63, 0x0048bf89, 0x0048bf97, 0x0048bfc1, 0x0048c018, 0x0048c04a, 0x0048c05f, 0x0048c074, 0x0048c085, 0x0048c08d, 0x0048c09f, 0x0048c0f2, 0x0048c106
