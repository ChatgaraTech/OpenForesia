; 0x0049e190 — la troisieme intelligence d'entite de scene (572 instructions, absente de Blaze.c).
; Deux tables de sauts : 0x0049e900 (etat +0x10) et 0x0049e910 (posture +0x42), bornees a 3.
; Table des gestes de sort : 0x00552978, trois mots par espece indexes par +0x14.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049e190  section .text  offset fichier 0x9d590  1900 octets

  0049e190  83ec2c                sub       esp,2Ch
  0049e193  53                    push      ebx
  0049e194  55                    push      ebp
  0049e195  56                    push      esi
  0049e196  8b74243c              mov       esi,[esp+3Ch]
  0049e19a  8d442424              lea       eax,[esp+24h]
  0049e19e  57                    push      edi
  0049e19f  8d4c242c              lea       ecx,[esp+2Ch]
  0049e1a3  50                    push      eax
  0049e1a4  8d542428              lea       edx,[esp+28h]
  0049e1a8  51                    push      ecx
  0049e1a9  33ed                  xor       ebp,ebp
  0049e1ab  52                    push      edx
  0049e1ac  56                    push      esi
  0049e1ad  66896c2440            mov       [esp+40h],bp
  0049e1b2  66896c2442            mov       [esp+42h],bp
  0049e1b7  66c74424443200        mov       word [esp+44h],32h
  0049e1be  66c74424466400        mov       word [esp+46h],64h
  0049e1c5  66896c2448            mov       [esp+48h],bp
  0049e1ca  66c744244a5a00        mov       word [esp+4Ah],5Ah
  0049e1d1  e8aaa70900            call      00538980h
  0049e1d6  83c410                add       esp,10h
  0049e1d9  8d442420              lea       eax,[esp+20h]
  0049e1dd  8d4c241c              lea       ecx,[esp+1Ch]
  0049e1e1  8d542440              lea       edx,[esp+40h]
  0049e1e5  50                    push      eax
  0049e1e6  51                    push      ecx
  0049e1e7  8d442420              lea       eax,[esp+20h]
  0049e1eb  52                    push      edx
  0049e1ec  8d4c2420              lea       ecx,[esp+20h]
  0049e1f0  50                    push      eax
  0049e1f1  51                    push      ecx
  0049e1f2  56                    push      esi
  0049e1f3  e8287d0900            call      00535F20h
  0049e1f8  8b4c242c              mov       ecx,[esp+2Ch]
  0049e1fc  b840000000            mov       eax,40h
  0049e201  2bc1                  sub       eax,ecx
  0049e203  83c418                add       esp,18h
  0049e206  89442410              mov       [esp+10h],eax
  0049e20a  8b44241c              mov       eax,[esp+1Ch]
  0049e20e  3bc5                  cmp       eax,ebp
  0049e210  bf01000000            mov       edi,1
  0049e215  0f84e5030000          je        near 0049E600h
  0049e21b  56                    push      esi
  0049e21c  e87fd30300            call      004DB5A0h
  0049e221  8b06                  mov       eax,[esi]
  0049e223  83c404                add       esp,4
  0049e226  a802                  test      al,2
  0049e228  0f849c000000          je        near 0049E2CAh
  0049e22e  8a4c2420              mov       cl,[esp+20h]
  0049e232  bb10000000            mov       ebx,10h
  0049e237  84cb                  test      bl,cl
  0049e239  740d                  je        short 0049E248h
  0049e23b  24fd                  and       al,0FDh
  0049e23d  c6464300              mov       byte [esi+43h],0
  0049e241  8906                  mov       [esi],eax
  0049e243  e9b8030000            jmp       0049E600h

LAB_0049e248:
  0049e248  66837e1a08            cmp       word [esi+1Ah],8
  0049e24d  7c58                  jl        short 0049E2A7h
  0049e24f  8d542430              lea       edx,[esp+30h]
  0049e253  55                    push      ebp
  0049e254  8d461c                lea       eax,[esi+1Ch]
  0049e257  52                    push      edx
  0049e258  50                    push      eax
  0049e259  56                    push      esi
  0049e25a  e8d1b90700            call      00519C30h
  0049e25f  8bf8                  mov       edi,eax
  0049e261  a1b8e05d00            mov       eax,[5DE0B8h]
  0049e266  83c410                add       esp,10h
  0049e269  3bc5                  cmp       eax,ebp
  0049e26b  7430                  je        short 0049E29Dh
  0049e26d  66899f96000000        mov       [edi+96h],bx
  0049e274  8b1f                  mov       ebx,[edi]
  0049e276  83cb40                or        ebx,40h
  0049e279  57                    push      edi
  0049e27a  56                    push      esi
  0049e27b  891f                  mov       [edi],ebx
  0049e27d  e8feb80900            call      00539B80h
  0049e282  83c408                add       esp,8
  0049e285  83c71c                add       edi,1Ch
  0049e288  57                    push      edi
  0049e289  6800180000            push      1800h
  0049e28e  6800030000            push      300h
  0049e293  6a03                  push      3
  0049e295  e886f80900            call      0053DB20h
  0049e29a  83c410                add       esp,10h

LAB_0049e29d:
  0049e29d  8b06                  mov       eax,[esi]
  0049e29f  a801                  test      al,1
  0049e2a1  740c                  je        short 0049E2AFh
  0049e2a3  66ff4e1a              dec       word [esi+1Ah]

LAB_0049e2a7:
  0049e2a7  a801                  test      al,1
  0049e2a9  0f8551030000          jne       near 0049E600h

LAB_0049e2af:
  0049e2af  a900000002            test      eax,2000000h
  0049e2b4  0f841c020000          je        near 0049E4D6h
  0049e2ba  24fd                  and       al,0FDh
  0049e2bc  c6464300              mov       byte [esi+43h],0
  0049e2c0  8906                  mov       [esi],eax
  0049e2c2  896e5c                mov       [esi+5Ch],ebp
  0049e2c5  e936030000            jmp       0049E600h

LAB_0049e2ca:
  0049e2ca  a810                  test      al,10h
  0049e2cc  0f852e030000          jne       near 0049E600h
  0049e2d2  a904080000            test      eax,804h
  0049e2d7  0f8452010000          je        near 0049E42Fh
  0049e2dd  f7442420e8030040      test      dword [esp+20h],400003E8h
  0049e2e5  740b                  je        short 0049E2F2h
  0049e2e7  25fbf7ffff            and       eax,0FFFFF7FBh
  0049e2ec  c6464300              mov       byte [esi+43h],0
  0049e2f0  8906                  mov       [esi],eax

LAB_0049e2f2:
  0049e2f2  668b4610              mov       ax,[esi+10h]
  0049e2f6  0fbfc8                movsx     ecx,ax
  0049e2f9  83f903                cmp       ecx,3
  0049e2fc  0f87fe020000          ja        near 0049E600h
  0049e302  ff248d00e94900        jmp       dword [ecx*4+49E900h]
  0049e309  8b06                  mov       eax,[esi]
  0049e30b  a900000002            test      eax,2000000h
  0049e310  0f84ea020000          je        near 0049E600h
  0049e316  80e4f7                and       ah,0F7h
  0049e319  8906                  mov       [esi],eax
  0049e31b  e9e0020000            jmp       0049E600h
  0049e320  b805000000            mov       eax,5
  0049e325  66394618              cmp       [esi+18h],ax
  0049e329  7411                  je        short 0049E33Ch
  0049e32b  894618                mov       [esi+18h],eax
  0049e32e  8b06                  mov       eax,[esi]
  0049e330  25fbfffffe            and       eax,0FEFFFFFBh
  0049e335  8906                  mov       [esi],eax
  0049e337  e9c4020000            jmp       0049E600h

LAB_0049e33c:
  0049e33c  66837e1a07            cmp       word [esi+1Ah],7
  0049e341  0f85b9020000          jne       near 0049E600h
  0049e347  33c0                  xor       eax,eax
  0049e349  6aff                  push      0FFFFFFFFh
  0049e34b  668b4606              mov       ax,[esi+6]
  0049e34f  6aff                  push      0FFFFFFFFh
  0049e351  8bc8                  mov       ecx,eax
  0049e353  c1f808                sar       eax,8
  0049e356  81e1ff000000          and       ecx,0FFh
  0049e35c  25ff000000            and       eax,0FFh
  0049e361  51                    push      ecx
  0049e362  50                    push      eax
  0049e363  56                    push      esi
  0049e364  e817c60900            call      0053A980h
  0049e369  83c414                add       esp,14h
  0049e36c  66ff4e10              dec       word [esi+10h]
  0049e370  e98b020000            jmp       0049E600h
  0049e375  8a4e43                mov       cl,[esi+43h]
  0049e378  fec9                  dec       cl
  0049e37a  884e43                mov       [esi+43h],cl
  0049e37d  7505                  jne       short 0049E384h
  0049e37f  48                    dec       eax
  0049e380  66894610              mov       [esi+10h],ax

LAB_0049e384:
  0049e384  8b542414              mov       edx,[esp+14h]
  0049e388  8b4c2440              mov       ecx,[esp+40h]
  0049e38c  8d049532000000        lea       eax,[edx*4+32h]
  0049e393  8d1408                lea       edx,[eax+ecx]
  0049e396  8d3c00                lea       edi,[eax+eax]
  0049e399  81e2ff0f0000          and       edx,0FFFh
  0049e39f  3bd7                  cmp       edx,edi
  0049e3a1  7f09                  jg        short 0049E3ACh
  0049e3a3  66014e32              add       [esi+32h],cx
  0049e3a7  e954020000            jmp       0049E600h

LAB_0049e3ac:
  0049e3ac  81e1ff0f0000          and       ecx,0FFFh
  0049e3b2  81f900080000          cmp       ecx,800h
  0049e3b8  0f8c3e020000          jl        near 0049E5FCh
  0049e3be  e937020000            jmp       0049E5FAh
  0049e3c3  55                    push      ebp
  0049e3c4  55                    push      ebp
  0049e3c5  68bd000000            push      0BDh
  0049e3ca  56                    push      esi
  0049e3cb  e810c7fdff            call      0047AAE0h
  0049e3d0  83c410                add       esp,10h
  0049e3d3  e8688d0a00            call      00547140h
  0049e3d8  99                    cdq
  0049e3d9  b903000000            mov       ecx,3
  0049e3de  f7f9                  idiv      ecx
  0049e3e0  0fbf4e14              movsx     ecx,word [esi+14h]
  0049e3e4  33c0                  xor       eax,eax
  0049e3e6  8d144a                lea       edx,[edx+ecx*2]
  0049e3e9  03ca                  add       ecx,edx
  0049e3eb  668b044d78295500      mov       ax,[ecx*2+552978h]
  0049e3f3  66894606              mov       [esi+6],ax
  0049e3f7  8b15c4975e00          mov       edx,[5E97C4h]
  0049e3fd  8bc8                  mov       ecx,eax
  0049e3ff  25ff000000            and       eax,0FFh
  0049e404  8b929c000000          mov       edx,[edx+9Ch]
  0049e40a  c1f908                sar       ecx,8
  0049e40d  81e1ff000000          and       ecx,0FFh
  0049e413  8d0440                lea       eax,[eax+eax*2]
  0049e416  c1e004                shl       eax,4
  0049e419  8b0c8a                mov       ecx,[edx+ecx*4]
  0049e41c  8a540113              mov       dl,[ecx+eax+13h]
  0049e420  66ff4e10              dec       word [esi+10h]
  0049e424  885643                mov       [esi+43h],dl
  0049e427  896e5c                mov       [esi+5Ch],ebp
  0049e42a  e9d1010000            jmp       0049E600h

LAB_0049e42f:
  0049e42f  a801                  test      al,1
  0049e431  0f85c9010000          jne       near 0049E600h
  0049e437  33c9                  xor       ecx,ecx
  0049e439  8a4e42                mov       cl,[esi+42h]
  0049e43c  83f903                cmp       ecx,3
  0049e43f  0f87bb010000          ja        near 0049E600h
  0049e445  ff248d10e94900        jmp       dword [ecx*4+49E910h]
  0049e44c  66396e18              cmp       [esi+18h],bp
  0049e450  740a                  je        short 0049E45Ch
  0049e452  0d00000001            or        eax,1000000h
  0049e457  896e18                mov       [esi+18h],ebp
  0049e45a  8906                  mov       [esi],eax

LAB_0049e45c:
  0049e45c  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049e464  7d09                  jge       short 0049E46Fh
  0049e466  c6464300              mov       byte [esi+43h],0
  0049e46a  e991010000            jmp       0049E600h

LAB_0049e46f:
  0049e46f  8a4643                mov       al,[esi+43h]
  0049e472  84c0                  test      al,al
  0049e474  7460                  je        short 0049E4D6h
  0049e476  fec8                  dec       al
  0049e478  896e5c                mov       [esi+5Ch],ebp
  0049e47b  884643                mov       [esi+43h],al
  0049e47e  e97d010000            jmp       0049E600h
  0049e483  66397e18              cmp       [esi+18h],di
  0049e487  740a                  je        short 0049E493h
  0049e489  0d00000001            or        eax,1000000h
  0049e48e  897e18                mov       [esi+18h],edi
  0049e491  8906                  mov       [esi],eax

LAB_0049e493:
  0049e493  8b442414              mov       eax,[esp+14h]
  0049e497  8b4c2440              mov       ecx,[esp+40h]
  0049e49b  8d840080000000        lea       eax,[eax+eax+80h]
  0049e4a2  8d1408                lea       edx,[eax+ecx]
  0049e4a5  8d3c00                lea       edi,[eax+eax]
  0049e4a8  81e2ff0f0000          and       edx,0FFFh
  0049e4ae  3bd7                  cmp       edx,edi
  0049e4b0  7f10                  jg        short 0049E4C2h
  0049e4b2  66014e32              add       [esi+32h],cx
  0049e4b6  c6464300              mov       byte [esi+43h],0
  0049e4ba  896e5c                mov       [esi+5Ch],ebp
  0049e4bd  e93e010000            jmp       0049E600h

LAB_0049e4c2:
  0049e4c2  81e1ff0f0000          and       ecx,0FFFh
  0049e4c8  81f900080000          cmp       ecx,800h
  0049e4ce  7c02                  jl        short 0049E4D2h
  0049e4d0  f7d8                  neg       eax

LAB_0049e4d2:
  0049e4d2  66014632              add       [esi+32h],ax

LAB_0049e4d6:
  0049e4d6  896e5c                mov       [esi+5Ch],ebp
  0049e4d9  e922010000            jmp       0049E600h
  0049e4de  66397e18              cmp       [esi+18h],di
  0049e4e2  740a                  je        short 0049E4EEh
  0049e4e4  0d00000001            or        eax,1000000h
  0049e4e9  897e18                mov       [esi+18h],edi
  0049e4ec  8906                  mov       [esi],eax

LAB_0049e4ee:
  0049e4ee  0fbf4610              movsx     eax,word [esi+10h]
  0049e4f2  39442418              cmp       [esp+18h],eax
  0049e4f6  7d09                  jge       short 0049E501h
  0049e4f8  c6464300              mov       byte [esi+43h],0
  0049e4fc  e9ff000000            jmp       0049E600h

LAB_0049e501:
  0049e501  8a4643                mov       al,[esi+43h]
  0049e504  84c0                  test      al,al
  0049e506  7405                  je        short 0049E50Dh
  0049e508  fec8                  dec       al
  0049e50a  884643                mov       [esi+43h],al

LAB_0049e50d:
  0049e50d  8b542414              mov       edx,[esp+14h]
  0049e511  8b4c2440              mov       ecx,[esp+40h]
  0049e515  8d841280000000        lea       eax,[edx+edx+80h]
  0049e51c  8d3c08                lea       edi,[eax+ecx]
  0049e51f  8d1c00                lea       ebx,[eax+eax]
  0049e522  81e7ff0f0000          and       edi,0FFFh
  0049e528  3bfb                  cmp       edi,ebx
  0049e52a  7f15                  jg        short 0049E541h
  0049e52c  66014e32              add       [esi+32h],cx
  0049e530  81c2c0000000          add       edx,0C0h
  0049e536  c1e207                shl       edx,7
  0049e539  89565c                mov       [esi+5Ch],edx
  0049e53c  e9bf000000            jmp       0049E600h

LAB_0049e541:
  0049e541  81e1ff0f0000          and       ecx,0FFFh
  0049e547  81f900080000          cmp       ecx,800h
  0049e54d  7c02                  jl        short 0049E551h
  0049e54f  f7d8                  neg       eax

LAB_0049e551:
  0049e551  66014632              add       [esi+32h],ax
  0049e555  81c2c0000000          add       edx,0C0h
  0049e55b  c1e207                shl       edx,7
  0049e55e  89565c                mov       [esi+5Ch],edx
  0049e561  e99a000000            jmp       0049E600h
  0049e566  8a4e43                mov       cl,[esi+43h]
  0049e569  84c9                  test      cl,cl
  0049e56b  7405                  je        short 0049E572h
  0049e56d  fec9                  dec       cl
  0049e56f  884e43                mov       [esi+43h],cl

LAB_0049e572:
  0049e572  66397e18              cmp       [esi+18h],di
  0049e576  740a                  je        short 0049E582h
  0049e578  0d00000001            or        eax,1000000h
  0049e57d  897e18                mov       [esi+18h],edi
  0049e580  8906                  mov       [esi],eax

LAB_0049e582:
  0049e582  8b4c2414              mov       ecx,[esp+14h]
  0049e586  0fbf5610              movsx     edx,word [esi+10h]
  0049e58a  81c180000000          add       ecx,80h
  0049e590  52                    push      edx
  0049e591  c1e107                shl       ecx,7
  0049e594  894e5c                mov       [esi+5Ch],ecx
  0049e597  e880740a00            call      00545A1Ch
  0049e59c  0faf465c              imul      eax,[esi+5Ch]
  0049e5a0  8b7e1c                mov       edi,[esi+1Ch]
  0049e5a3  c1f80c                sar       eax,0Ch
  0049e5a6  03f8                  add       edi,eax
  0049e5a8  0fbf4610              movsx     eax,word [esi+10h]
  0049e5ac  50                    push      eax
  0049e5ad  897e1c                mov       [esi+1Ch],edi
  0049e5b0  e861740a00            call      00545A16h
  0049e5b5  0faf465c              imul      eax,[esi+5Ch]
  0049e5b9  8b5e24                mov       ebx,[esi+24h]
  0049e5bc  8b4c2414              mov       ecx,[esp+14h]
  0049e5c0  c1f80c                sar       eax,0Ch
  0049e5c3  03d8                  add       ebx,eax
  0049e5c5  8d048d32000000        lea       eax,[ecx*4+32h]
  0049e5cc  8b4c2440              mov       ecx,[esp+40h]
  0049e5d0  895e24                mov       [esi+24h],ebx
  0049e5d3  8d3c00                lea       edi,[eax+eax]
  0049e5d6  896e5c                mov       [esi+5Ch],ebp
  0049e5d9  8d1408                lea       edx,[eax+ecx]
  0049e5dc  81e2ff0f0000          and       edx,0FFFh
  0049e5e2  3bd7                  cmp       edx,edi
  0049e5e4  7f06                  jg        short 0049E5ECh
  0049e5e6  66014e32              add       [esi+32h],cx
  0049e5ea  eb14                  jmp       short 0049E600h

LAB_0049e5ec:
  0049e5ec  81e1ff0f0000          and       ecx,0FFFh
  0049e5f2  81f900080000          cmp       ecx,800h
  0049e5f8  7c02                  jl        short 0049E5FCh

LAB_0049e5fa:
  0049e5fa  f7d8                  neg       eax

LAB_0049e5fc:
  0049e5fc  66014632              add       [esi+32h],ax

LAB_0049e600:
  0049e600  8b44241c              mov       eax,[esp+1Ch]
  0049e604  50                    push      eax
  0049e605  56                    push      esi
  0049e606  e8559bfeff            call      00488160h
  0049e60b  83c408                add       esp,8
  0049e60e  56                    push      esi
  0049e60f  e82c530900            call      00533940h
  0049e614  83c404                add       esp,4
  0049e617  56                    push      esi
  0049e618  e8d3140400            call      004DFAF0h
  0049e61d  8b4c2428              mov       ecx,[esp+28h]
  0049e621  83c404                add       esp,4
  0049e624  51                    push      ecx
  0049e625  56                    push      esi
  0049e626  e845510400            call      004E3770h
  0049e62b  8b442424              mov       eax,[esp+24h]
  0049e62f  83c408                add       esp,8
  0049e632  3bc5                  cmp       eax,ebp
  0049e634  0f84be020000          je        near 0049E8F8h
  0049e63a  f70617080000          test      dword [esi],817h
  0049e640  0f85b2020000          jne       near 0049E8F8h
  0049e646  8a4643                mov       al,[esi+43h]
  0049e649  84c0                  test      al,al
  0049e64b  0f85a7020000          jne       near 0049E8F8h
  0049e651  8b466c                mov       eax,[esi+6Ch]
  0049e654  8b5e1c                mov       ebx,[esi+1Ch]
  0049e657  8b4e24                mov       ecx,[esi+24h]
  0049e65a  8b781c                mov       edi,[eax+1Ch]
  0049e65d  8b6824                mov       ebp,[eax+24h]
  0049e660  2bfb                  sub       edi,ebx
  0049e662  2be9                  sub       ebp,ecx
  0049e664  c1ff0c                sar       edi,0Ch
  0049e667  c1fd0c                sar       ebp,0Ch
  0049e66a  8bd7                  mov       edx,edi
  0049e66c  8bc5                  mov       eax,ebp
  0049e66e  0fafd7                imul      edx,edi
  0049e671  0fafc5                imul      eax,ebp
  0049e674  03d0                  add       edx,eax
  0049e676  52                    push      edx
  0049e677  e894730a00            call      00545A10h
  0049e67c  55                    push      ebp
  0049e67d  57                    push      edi
  0049e67e  89442420              mov       [esp+20h],eax
  0049e682  e89b730a00            call      00545A22h
  0049e687  668b4e32              mov       cx,[esi+32h]
  0049e68b  2bc1                  sub       eax,ecx
  0049e68d  25ff0f0000            and       eax,0FFFh
  0049e692  89442440              mov       [esp+40h],eax
  0049e696  8b442418              mov       eax,[esp+18h]
  0049e69a  3dfa000000            cmp       eax,0FAh
  0049e69f  0f8dd0000000          jge       near 0049E775h
  0049e6a5  e8968a0a00            call      00547140h
  0049e6aa  99                    cdq
  0049e6ab  b964000000            mov       ecx,64h
  0049e6b0  f7f9                  idiv      ecx
  0049e6b2  83fa1e                cmp       edx,1Eh
  0049e6b5  7d61                  jge       short 0049E718h
  0049e6b7  8b542440              mov       edx,[esp+40h]
  0049e6bb  8b7c2410              mov       edi,[esp+10h]
  0049e6bf  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049e6c6  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049e6cd  25ff0f0000            and       eax,0FFFh
  0049e6d2  3bc1                  cmp       eax,ecx
  0049e6d4  0f8fba010000          jg        near 0049E894h
  0049e6da  e8618a0a00            call      00547140h
  0049e6df  99                    cdq
  0049e6e0  8d4c3f01              lea       ecx,[edi+edi+1]
  0049e6e4  f7f9                  idiv      ecx
  0049e6e6  8b442418              mov       eax,[esp+18h]
  0049e6ea  83c250                add       edx,50h
  0049e6ed  3bc2                  cmp       eax,edx
  0049e6ef  0f8d9f010000          jge       near 0049E894h
  0049e6f5  8b16                  mov       edx,[esi]
  0049e6f7  c7461803000000        mov       dword [esi+18h],3
  0049e6fe  81e2fffffffe          and       edx,0FEFFFFFFh
  0049e704  c7465c00000000        mov       dword [esi+5Ch],0
  0049e70b  83ca02                or        edx,2
  0049e70e  8916                  mov       [esi],edx
  0049e710  5f                    pop       edi
  0049e711  5e                    pop       esi
  0049e712  5d                    pop       ebp
  0049e713  5b                    pop       ebx
  0049e714  83c42c                add       esp,2Ch
  0049e717  c3                    ret

LAB_0049e718:
  0049e718  83fa3c                cmp       edx,3Ch
  0049e71b  0f8cf6000000          jl        near 0049E817h
  0049e721  c6464203              mov       byte [esi+42h],3
  0049e725  e8168a0a00            call      00547140h
  0049e72a  99                    cdq
  0049e72b  b90a000000            mov       ecx,0Ah
  0049e730  f7f9                  idiv      ecx
  0049e732  8b442410              mov       eax,[esp+10h]
  0049e736  8b4c2414              mov       ecx,[esp+14h]
  0049e73a  d1f8                  sar       eax,1
  0049e73c  8db980000000          lea       edi,[ecx+80h]
  0049e742  c1e704                shl       edi,4
  0049e745  02d0                  add       dl,al
  0049e747  80c214                add       dl,14h
  0049e74a  885643                mov       [esi+43h],dl
  0049e74d  e8ee890a00            call      00547140h
  0049e752  99                    cdq
  0049e753  f7ff                  idiv      edi
  0049e755  d1ff                  sar       edi,1
  0049e757  2bd7                  sub       edx,edi
  0049e759  8b7c2440              mov       edi,[esp+40h]
  0049e75d  03d7                  add       edx,edi
  0049e75f  66035632              add       dx,[esi+32h]
  0049e763  81c200080000          add       edx,800h
  0049e769  66895610              mov       [esi+10h],dx
  0049e76d  5f                    pop       edi
  0049e76e  5e                    pop       esi
  0049e76f  5d                    pop       ebp
  0049e770  5b                    pop       ebx
  0049e771  83c42c                add       esp,2Ch
  0049e774  c3                    ret

LAB_0049e775:
  0049e775  3df4010000            cmp       eax,1F4h
  0049e77a  0f8dc4000000          jge       near 0049E844h
  0049e780  e8bb890a00            call      00547140h
  0049e785  99                    cdq
  0049e786  b964000000            mov       ecx,64h
  0049e78b  f7f9                  idiv      ecx
  0049e78d  83fa2d                cmp       edx,2Dh
  0049e790  7d29                  jge       short 0049E7BBh
  0049e792  8b542440              mov       edx,[esp+40h]
  0049e796  8b442410              mov       eax,[esp+10h]
  0049e79a  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  0049e7a1  8d14c500020000        lea       edx,[eax*8+200h]
  0049e7a8  81e1ff0f0000          and       ecx,0FFFh
  0049e7ae  3bca                  cmp       ecx,edx
  0049e7b0  0f8fde000000          jg        near 0049E894h
  0049e7b6  e9bb000000            jmp       0049E876h

LAB_0049e7bb:
  0049e7bb  83fa46                cmp       edx,46h
  0049e7be  7d4e                  jge       short 0049E80Eh
  0049e7c0  c6464203              mov       byte [esi+42h],3
  0049e7c4  e877890a00            call      00547140h
  0049e7c9  99                    cdq
  0049e7ca  b90a000000            mov       ecx,0Ah
  0049e7cf  f7f9                  idiv      ecx
  0049e7d1  8b442410              mov       eax,[esp+10h]
  0049e7d5  8b4c2414              mov       ecx,[esp+14h]
  0049e7d9  d1f8                  sar       eax,1
  0049e7db  8db980000000          lea       edi,[ecx+80h]
  0049e7e1  c1e704                shl       edi,4
  0049e7e4  02d0                  add       dl,al
  0049e7e6  80c214                add       dl,14h
  0049e7e9  885643                mov       [esi+43h],dl
  0049e7ec  e84f890a00            call      00547140h
  0049e7f1  99                    cdq
  0049e7f2  f7ff                  idiv      edi
  0049e7f4  8b4c2440              mov       ecx,[esp+40h]
  0049e7f8  d1ff                  sar       edi,1
  0049e7fa  2bd7                  sub       edx,edi
  0049e7fc  03d1                  add       edx,ecx
  0049e7fe  66035632              add       dx,[esi+32h]
  0049e802  66895610              mov       [esi+10h],dx
  0049e806  5f                    pop       edi
  0049e807  5e                    pop       esi
  0049e808  5d                    pop       ebp
  0049e809  5b                    pop       ebx
  0049e80a  83c42c                add       esp,2Ch
  0049e80d  c3                    ret

LAB_0049e80e:
  0049e80e  83fa5a                cmp       edx,5Ah
  0049e811  0f8d92000000          jge       near 0049E8A9h

LAB_0049e817:
  0049e817  c6464202              mov       byte [esi+42h],2
  0049e81b  e820890a00            call      00547140h
  0049e820  99                    cdq
  0049e821  b90a000000            mov       ecx,0Ah
  0049e826  66c746106400          mov       word [esi+10h],64h
  0049e82c  f7f9                  idiv      ecx
  0049e82e  8b442410              mov       eax,[esp+10h]
  0049e832  d1f8                  sar       eax,1
  0049e834  02d0                  add       dl,al
  0049e836  80c214                add       dl,14h
  0049e839  885643                mov       [esi+43h],dl
  0049e83c  5f                    pop       edi
  0049e83d  5e                    pop       esi
  0049e83e  5d                    pop       ebp
  0049e83f  5b                    pop       ebx
  0049e840  83c42c                add       esp,2Ch
  0049e843  c3                    ret

LAB_0049e844:
  0049e844  e8f7880a00            call      00547140h
  0049e849  99                    cdq
  0049e84a  b964000000            mov       ecx,64h
  0049e84f  f7f9                  idiv      ecx
  0049e851  83fa1e                cmp       edx,1Eh
  0049e854  7d4e                  jge       short 0049E8A4h
  0049e856  8b542440              mov       edx,[esp+40h]
  0049e85a  8b442410              mov       eax,[esp+10h]
  0049e85e  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  0049e865  8d14c500020000        lea       edx,[eax*8+200h]
  0049e86c  81e1ff0f0000          and       ecx,0FFFh
  0049e872  3bca                  cmp       ecx,edx
  0049e874  7f1e                  jg        short 0049E894h

LAB_0049e876:
  0049e876  8b06                  mov       eax,[esi]
  0049e878  c7461804000000        mov       dword [esi+18h],4
  0049e87f  0d04080001            or        eax,1000804h
  0049e884  66c746100300          mov       word [esi+10h],3
  0049e88a  8906                  mov       [esi],eax
  0049e88c  5f                    pop       edi
  0049e88d  5e                    pop       esi
  0049e88e  5d                    pop       ebp
  0049e88f  5b                    pop       ebx
  0049e890  83c42c                add       esp,2Ch
  0049e893  c3                    ret

LAB_0049e894:
  0049e894  c6464201              mov       byte [esi+42h],1
  0049e898  c6464301              mov       byte [esi+43h],1
  0049e89c  5f                    pop       edi
  0049e89d  5e                    pop       esi
  0049e89e  5d                    pop       ebp
  0049e89f  5b                    pop       ebx
  0049e8a0  83c42c                add       esp,2Ch
  0049e8a3  c3                    ret

LAB_0049e8a4:
  0049e8a4  83fa3c                cmp       edx,3Ch
  0049e8a7  7d27                  jge       short 0049E8D0h

LAB_0049e8a9:
  0049e8a9  c6464200              mov       byte [esi+42h],0
  0049e8ad  e88e880a00            call      00547140h
  0049e8b2  99                    cdq
  0049e8b3  b90a000000            mov       ecx,0Ah
  0049e8b8  f7f9                  idiv      ecx
  0049e8ba  8b442410              mov       eax,[esp+10h]
  0049e8be  d1f8                  sar       eax,1
  0049e8c0  02d0                  add       dl,al
  0049e8c2  80c214                add       dl,14h
  0049e8c5  885643                mov       [esi+43h],dl
  0049e8c8  5f                    pop       edi
  0049e8c9  5e                    pop       esi
  0049e8ca  5d                    pop       ebp
  0049e8cb  5b                    pop       ebx
  0049e8cc  83c42c                add       esp,2Ch
  0049e8cf  c3                    ret

LAB_0049e8d0:
  0049e8d0  c6464203              mov       byte [esi+42h],3
  0049e8d4  e867880a00            call      00547140h
  0049e8d9  99                    cdq
  0049e8da  b90a000000            mov       ecx,0Ah
  0049e8df  f7f9                  idiv      ecx
  0049e8e1  8b442410              mov       eax,[esp+10h]
  0049e8e5  d1f8                  sar       eax,1
  0049e8e7  02d0                  add       dl,al
  0049e8e9  80c214                add       dl,14h
  0049e8ec  885643                mov       [esi+43h],dl
  0049e8ef  e84c880a00            call      00547140h
  0049e8f4  66894610              mov       [esi+10h],ax

LAB_0049e8f8:
  0049e8f8  5f                    pop       edi
  0049e8f9  5e                    pop       esi
  0049e8fa  5d                    pop       ebp
  0049e8fb  5b                    pop       ebx

39 cible(s) de saut interne : 0x0049e248, 0x0049e29d, 0x0049e2a7, 0x0049e2af, 0x0049e2ca, 0x0049e2f2, 0x0049e33c, 0x0049e384, 0x0049e3ac, 0x0049e42f, 0x0049e45c, 0x0049e46f, 0x0049e493, 0x0049e4c2, 0x0049e4d2, 0x0049e4d6, 0x0049e4ee, 0x0049e501, 0x0049e50d, 0x0049e541, 0x0049e551, 0x0049e572, 0x0049e582, 0x0049e5ec, 0x0049e5fa, 0x0049e5fc, 0x0049e600, 0x0049e718, 0x0049e775, 0x0049e7bb, 0x0049e80e, 0x0049e817, 0x0049e844, 0x0049e876, 0x0049e894, 0x0049e8a4, 0x0049e8a9, 0x0049e8d0, 0x0049e8f8
