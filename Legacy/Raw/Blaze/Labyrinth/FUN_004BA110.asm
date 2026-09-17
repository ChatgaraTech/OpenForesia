Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004ba110  section .text  offset fichier 0xb9510  2512 octets

  004ba110  83ec54                sub       esp,54h
  004ba113  53                    push      ebx
  004ba114  55                    push      ebp
  004ba115  56                    push      esi
  004ba116  8b742464              mov       esi,[esp+64h]
  004ba11a  57                    push      edi
  004ba11b  33ff                  xor       edi,edi
  004ba11d  8a4641                mov       al,[esi+41h]
  004ba120  66897c2430            mov       [esp+30h],di
  004ba125  84c0                  test      al,al
  004ba127  66c74424321e00        mov       word [esp+32h],1Eh
  004ba12e  66c74424343200        mov       word [esp+34h],32h
  004ba135  66c74424366400        mov       word [esp+36h],64h
  004ba13c  66897c2438            mov       [esp+38h],di
  004ba141  66c744243a5000        mov       word [esp+3Ah],50h
  004ba148  750f                  jne       short 004BA159h
  004ba14a  57                    push      edi
  004ba14b  57                    push      edi
  004ba14c  56                    push      esi
  004ba14d  c6464101              mov       byte [esi+41h],1
  004ba151  e8caf0ffff            call      004B9220h
  004ba156  83c40c                add       esp,0Ch

LAB_004ba159:
  004ba159  8d442424              lea       eax,[esp+24h]
  004ba15d  8d4c2428              lea       ecx,[esp+28h]
  004ba161  50                    push      eax
  004ba162  8d542430              lea       edx,[esp+30h]
  004ba166  51                    push      ecx
  004ba167  52                    push      edx
  004ba168  56                    push      esi
  004ba169  e812e80700            call      00538980h
  004ba16e  83c410                add       esp,10h
  004ba171  8d442420              lea       eax,[esp+20h]
  004ba175  8d4c2418              lea       ecx,[esp+18h]
  004ba179  8d542410              lea       edx,[esp+10h]
  004ba17d  50                    push      eax
  004ba17e  51                    push      ecx
  004ba17f  8d442424              lea       eax,[esp+24h]
  004ba183  52                    push      edx
  004ba184  8d4c2474              lea       ecx,[esp+74h]
  004ba188  50                    push      eax
  004ba189  51                    push      ecx
  004ba18a  56                    push      esi
  004ba18b  e890bd0700            call      00535F20h
  004ba190  8bac2480000000        mov       ebp,[esp+80h]
  004ba197  8b542438              mov       edx,[esp+38h]
  004ba19b  83c418                add       esp,18h
  004ba19e  b840000000            mov       eax,40h
  004ba1a3  2bc5                  sub       eax,ebp
  004ba1a5  52                    push      edx
  004ba1a6  56                    push      esi
  004ba1a7  8944241c              mov       [esp+1Ch],eax
  004ba1ab  e830bc0100            call      004D5DE0h
  004ba1b0  8b4c2420              mov       ecx,[esp+20h]
  004ba1b4  668b4632              mov       ax,[esi+32h]
  004ba1b8  83c408                add       esp,8
  004ba1bb  3bcf                  cmp       ecx,edi
  004ba1bd  6689442452            mov       [esp+52h],ax
  004ba1c2  bb07000000            mov       ebx,7
  004ba1c7  0f84b9040000          je        near 004BA686h
  004ba1cd  8b2e                  mov       ebp,[esi]
  004ba1cf  33d2                  xor       edx,edx
  004ba1d1  8a5604                mov       dl,[esi+4]
  004ba1d4  81e5fffffffd          and       ebp,0FDFFFFFFh
  004ba1da  0fbf7e18              movsx     edi,word [esi+18h]
  004ba1de  892e                  mov       [esi],ebp
  004ba1e0  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004ba1e7  8bc5                  mov       eax,ebp
  004ba1e9  8b14ba                mov       edx,[edx+edi*4]
  004ba1ec  a900000001            test      eax,1000000h
  004ba1f1  741d                  je        short 004BA210h
  004ba1f3  66ff461a              inc       word [esi+1Ah]
  004ba1f7  668b461a              mov       ax,[esi+1Ah]
  004ba1fb  33ff                  xor       edi,edi
  004ba1fd  668b7a16              mov       di,[edx+16h]
  004ba201  0fbfc0                movsx     eax,ax
  004ba204  3bc7                  cmp       eax,edi
  004ba206  7c29                  jl        short 004BA231h
  004ba208  66c7461a0000          mov       word [esi+1Ah],0
  004ba20e  eb21                  jmp       short 004BA231h

LAB_004ba210:
  004ba210  66ff461a              inc       word [esi+1Ah]
  004ba214  668b7e1a              mov       di,[esi+1Ah]
  004ba218  33ed                  xor       ebp,ebp
  004ba21a  668b6a16              mov       bp,[edx+16h]
  004ba21e  0fbfd7                movsx     edx,di
  004ba221  3bd5                  cmp       edx,ebp
  004ba223  7c0c                  jl        short 004BA231h
  004ba225  4f                    dec       edi
  004ba226  0d00000002            or        eax,2000000h
  004ba22b  66897e1a              mov       [esi+1Ah],di
  004ba22f  8906                  mov       [esi],eax

LAB_004ba231:
  004ba231  f60602                test      byte [esi],2
  004ba234  0f84b1000000          je        near 004BA2EBh
  004ba23a  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004ba23f  7409                  je        short 004BA24Ah
  004ba241  385e42                cmp       [esi+42h],bl
  004ba244  0f858b000000          jne       near 004BA2D5h

LAB_004ba24a:
  004ba24a  8d442430              lea       eax,[esp+30h]
  004ba24e  6a00                  push      0
  004ba250  8d4e1c                lea       ecx,[esi+1Ch]
  004ba253  50                    push      eax
  004ba254  51                    push      ecx
  004ba255  56                    push      esi
  004ba256  e8d5f90500            call      00519C30h
  004ba25b  8bf8                  mov       edi,eax
  004ba25d  a1b8e05d00            mov       eax,[5DE0B8h]
  004ba262  83c410                add       esp,10h
  004ba265  85c0                  test      eax,eax
  004ba267  7468                  je        short 004BA2D1h
  004ba269  8b0f                  mov       ecx,[edi]
  004ba26b  57                    push      edi
  004ba26c  83c940                or        ecx,40h
  004ba26f  56                    push      esi
  004ba270  66c787960000001000    mov       word [edi+96h],10h
  004ba279  890f                  mov       [edi],ecx
  004ba27b  e800f90700            call      00539B80h
  004ba280  83c408                add       esp,8
  004ba283  8d571c                lea       edx,[edi+1Ch]
  004ba286  52                    push      edx
  004ba287  6800180000            push      1800h
  004ba28c  6800030000            push      300h
  004ba291  6a03                  push      3
  004ba293  e888380800            call      0053DB20h
  004ba298  83c410                add       esp,10h
  004ba29b  e8a0ce0800            call      00547140h
  004ba2a0  99                    cdq
  004ba2a1  b964000000            mov       ecx,64h
  004ba2a6  f7f9                  idiv      ecx
  004ba2a8  8bea                  mov       ebp,edx
  004ba2aa  83fd0a                cmp       ebp,0Ah
  004ba2ad  7f11                  jg        short 004BA2C0h
  004ba2af  6a00                  push      0
  004ba2b1  57                    push      edi
  004ba2b2  56                    push      esi
  004ba2b3  e868080000            call      004BAB20h
  004ba2b8  83c40c                add       esp,0Ch
  004ba2bb  83fd0a                cmp       ebp,0Ah
  004ba2be  7e11                  jle       short 004BA2D1h

LAB_004ba2c0:
  004ba2c0  83fd14                cmp       ebp,14h
  004ba2c3  7f0c                  jg        short 004BA2D1h
  004ba2c5  6a00                  push      0
  004ba2c7  57                    push      edi
  004ba2c8  56                    push      esi
  004ba2c9  e812080000            call      004BAAE0h
  004ba2ce  83c40c                add       esp,0Ch

LAB_004ba2d1:
  004ba2d1  8b4c2418              mov       ecx,[esp+18h]

LAB_004ba2d5:
  004ba2d5  8b06                  mov       eax,[esi]
  004ba2d7  a900000002            test      eax,2000000h
  004ba2dc  740d                  je        short 004BA2EBh
  004ba2de  385e42                cmp       [esi+42h],bl
  004ba2e1  7408                  je        short 004BA2EBh
  004ba2e3  24fd                  and       al,0FDh
  004ba2e5  c6464300              mov       byte [esi+43h],0
  004ba2e9  8906                  mov       [esi],eax

LAB_004ba2eb:
  004ba2eb  8b06                  mov       eax,[esi]
  004ba2ed  a802                  test      al,2
  004ba2ef  7409                  je        short 004BA2FAh
  004ba2f1  385e42                cmp       [esi+42h],bl
  004ba2f4  0f858c030000          jne       near 004BA686h

LAB_004ba2fa:
  004ba2fa  8bf8                  mov       edi,eax
  004ba2fc  83e701                and       edi,1
  004ba2ff  7409                  je        short 004BA30Ah
  004ba301  385e42                cmp       [esi+42h],bl
  004ba304  0f857c030000          jne       near 004BA686h

LAB_004ba30a:
  004ba30a  33d2                  xor       edx,edx
  004ba30c  8a5642                mov       dl,[esi+42h]
  004ba30f  3bd3                  cmp       edx,ebx
  004ba311  0f876f030000          ja        near 004BA686h
  004ba317  ff2495b8aa4b00        jmp       dword [edx*4+4BAAB8h]
  004ba31e  817c241c2c010000      cmp       dword [esp+1Ch],12Ch
  004ba326  0f8c56030000          jl        near 004BA682h
  004ba32c  8a4643                mov       al,[esi+43h]
  004ba32f  84c0                  test      al,al
  004ba331  0f8481000000          je        near 004BA3B8h
  004ba337  fec8                  dec       al
  004ba339  c7465c00000000        mov       dword [esi+5Ch],0
  004ba340  884643                mov       [esi+43h],al
  004ba343  e93e030000            jmp       004BA686h
  004ba348  8b4c2410              mov       ecx,[esp+10h]
  004ba34c  8d91c8000000          lea       edx,[ecx+0C8h]
  004ba352  81e2ff0f0000          and       edx,0FFFh
  004ba358  81fa90010000          cmp       edx,190h
  004ba35e  7e54                  jle       short 004BA3B4h
  004ba360  81e1ff0f0000          and       ecx,0FFFh
  004ba366  81f900080000          cmp       ecx,800h
  004ba36c  7d0c                  jge       short 004BA37Ah
  004ba36e  8b4c2468              mov       ecx,[esp+68h]
  004ba372  81c180000000          add       ecx,80h
  004ba378  eb0b                  jmp       short 004BA385h

LAB_004ba37a:
  004ba37a  8b542468              mov       edx,[esp+68h]
  004ba37e  b980ffffff            mov       ecx,0FFFFFF80h
  004ba383  2bca                  sub       ecx,edx

LAB_004ba385:
  004ba385  66014e32              add       [esi+32h],cx
  004ba389  b901000000            mov       ecx,1
  004ba38e  66394e18              cmp       [esi+18h],cx
  004ba392  7424                  je        short 004BA3B8h
  004ba394  0d00000001            or        eax,1000000h
  004ba399  894e18                mov       [esi+18h],ecx
  004ba39c  8906                  mov       [esi],eax
  004ba39e  c7465c00000000        mov       dword [esi+5Ch],0
  004ba3a5  e9dc020000            jmp       004BA686h
  004ba3aa  0fbf5610              movsx     edx,word [esi+10h]
  004ba3ae  3954241c              cmp       [esp+1Ch],edx
  004ba3b2  7d10                  jge       short 004BA3C4h

LAB_004ba3b4:
  004ba3b4  c6464300              mov       byte [esi+43h],0

LAB_004ba3b8:
  004ba3b8  c7465c00000000        mov       dword [esi+5Ch],0
  004ba3bf  e9c2020000            jmp       004BA686h

LAB_004ba3c4:
  004ba3c4  8a4e43                mov       cl,[esi+43h]
  004ba3c7  84c9                  test      cl,cl
  004ba3c9  7405                  je        short 004BA3D0h
  004ba3cb  fec9                  dec       cl
  004ba3cd  884e43                mov       [esi+43h],cl

LAB_004ba3d0:
  004ba3d0  b901000000            mov       ecx,1
  004ba3d5  66394e18              cmp       [esi+18h],cx
  004ba3d9  740a                  je        short 004BA3E5h
  004ba3db  0d00000001            or        eax,1000000h
  004ba3e0  894e18                mov       [esi+18h],ecx
  004ba3e3  8906                  mov       [esi],eax

LAB_004ba3e5:
  004ba3e5  8b442410              mov       eax,[esp+10h]
  004ba3e9  8d8800010000          lea       ecx,[eax+100h]
  004ba3ef  81e1ff0f0000          and       ecx,0FFFh
  004ba3f5  81f900020000          cmp       ecx,200h
  004ba3fb  8b4c2468              mov       ecx,[esp+68h]
  004ba3ff  7e1f                  jle       short 004BA420h
  004ba401  25ff0f0000            and       eax,0FFFh
  004ba406  3d00080000            cmp       eax,800h
  004ba40b  7d08                  jge       short 004BA415h
  004ba40d  8d8180000000          lea       eax,[ecx+80h]
  004ba413  eb07                  jmp       short 004BA41Ch

LAB_004ba415:
  004ba415  b880ffffff            mov       eax,0FFFFFF80h
  004ba41a  2bc1                  sub       eax,ecx

LAB_004ba41c:
  004ba41c  66014632              add       [esi+32h],ax

LAB_004ba420:
  004ba420  8d14c9                lea       edx,[ecx+ecx*8]
  004ba423  c1e204                shl       edx,4
  004ba426  81c200a00000          add       edx,0A000h
  004ba42c  89565c                mov       [esi+5Ch],edx
  004ba42f  e952020000            jmp       004BA686h
  004ba434  8a5643                mov       dl,[esi+43h]
  004ba437  84d2                  test      dl,dl
  004ba439  7405                  je        short 004BA440h
  004ba43b  feca                  dec       dl
  004ba43d  885643                mov       [esi+43h],dl

LAB_004ba440:
  004ba440  ba01000000            mov       edx,1
  004ba445  66395618              cmp       [esi+18h],dx
  004ba449  740a                  je        short 004BA455h
  004ba44b  0d00000001            or        eax,1000000h
  004ba450  895618                mov       [esi+18h],edx
  004ba453  8906                  mov       [esi],eax

LAB_004ba455:
  004ba455  8b442468              mov       eax,[esp+68h]
  004ba459  83c070                add       eax,70h
  004ba45c  c1e008                shl       eax,8
  004ba45f  0fafc8                imul      ecx,eax
  004ba462  c1f90c                sar       ecx,0Ch
  004ba465  8bf9                  mov       edi,ecx
  004ba467  89465c                mov       [esi+5Ch],eax
  004ba46a  0fbf4e10              movsx     ecx,word [esi+10h]
  004ba46e  51                    push      ecx
  004ba46f  e8a8b50800            call      00545A1Ch
  004ba474  0fafc7                imul      eax,edi
  004ba477  0fbf5610              movsx     edx,word [esi+10h]
  004ba47b  8b4e1c                mov       ecx,[esi+1Ch]
  004ba47e  52                    push      edx
  004ba47f  c1f80c                sar       eax,0Ch
  004ba482  03c8                  add       ecx,eax
  004ba484  894e1c                mov       [esi+1Ch],ecx
  004ba487  e88ab50800            call      00545A16h
  004ba48c  0fafc7                imul      eax,edi
  004ba48f  8b5e24                mov       ebx,[esi+24h]
  004ba492  c7465c00000000        mov       dword [esi+5Ch],0
  004ba499  c1f80c                sar       eax,0Ch
  004ba49c  03d8                  add       ebx,eax
  004ba49e  8b442410              mov       eax,[esp+10h]
  004ba4a2  895e24                mov       [esi+24h],ebx
  004ba4a5  8d8800010000          lea       ecx,[eax+100h]
  004ba4ab  81e1ff0f0000          and       ecx,0FFFh
  004ba4b1  81f900020000          cmp       ecx,200h
  004ba4b7  0f8ec9010000          jle       near 004BA686h
  004ba4bd  25ff0f0000            and       eax,0FFFh
  004ba4c2  3d00080000            cmp       eax,800h
  004ba4c7  7d14                  jge       short 004BA4DDh
  004ba4c9  8b542468              mov       edx,[esp+68h]
  004ba4cd  8d04d532000000        lea       eax,[edx*8+32h]
  004ba4d4  66014632              add       [esi+32h],ax
  004ba4d8  e9a9010000            jmp       004BA686h

LAB_004ba4dd:
  004ba4dd  8b442468              mov       eax,[esp+68h]
  004ba4e1  8d0cc500000000        lea       ecx,[eax*8]
  004ba4e8  b8ceffffff            mov       eax,0FFFFFFCEh
  004ba4ed  2bc1                  sub       eax,ecx
  004ba4ef  66014632              add       [esi+32h],ax
  004ba4f3  e98e010000            jmp       004BA686h
  004ba4f8  85ff                  test      edi,edi
  004ba4fa  0f8586010000          jne       near 004BA686h
  004ba500  8b542468              mov       edx,[esp+68h]
  004ba504  0d00000001            or        eax,1000000h
  004ba509  8906                  mov       [esi],eax
  004ba50b  c7461801000000        mov       dword [esi+18h],1
  004ba512  8d82c0000000          lea       eax,[edx+0C0h]
  004ba518  c1e008                shl       eax,8
  004ba51b  0fafc8                imul      ecx,eax
  004ba51e  89465c                mov       [esi+5Ch],eax
  004ba521  0fbf4610              movsx     eax,word [esi+10h]
  004ba525  c1f90c                sar       ecx,0Ch
  004ba528  50                    push      eax
  004ba529  8bf9                  mov       edi,ecx
  004ba52b  e8ecb40800            call      00545A1Ch
  004ba530  0fafc7                imul      eax,edi
  004ba533  0fbf4e10              movsx     ecx,word [esi+10h]
  004ba537  8b6e50                mov       ebp,[esi+50h]
  004ba53a  51                    push      ecx
  004ba53b  c1f80c                sar       eax,0Ch
  004ba53e  03e8                  add       ebp,eax
  004ba540  896e50                mov       [esi+50h],ebp
  004ba543  e8ceb40800            call      00545A16h
  004ba548  0fafc7                imul      eax,edi
  004ba54b  8b5658                mov       edx,[esi+58h]
  004ba54e  8b4e54                mov       ecx,[esi+54h]
  004ba551  c1f80c                sar       eax,0Ch
  004ba554  03d0                  add       edx,eax
  004ba556  8b06                  mov       eax,[esi]
  004ba558  81c100e00100          add       ecx,1E000h
  004ba55e  0c01                  or        al,1
  004ba560  895658                mov       [esi+58h],edx
  004ba563  894e54                mov       [esi+54h],ecx
  004ba566  8906                  mov       [esi],eax
  004ba568  e919010000            jmp       004BA686h
  004ba56d  668b4e12              mov       cx,[esi+12h]
  004ba571  6685c9                test      cx,cx
  004ba574  7507                  jne       short 004BA57Dh
  004ba576  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004ba57d:
  004ba57d  6683f90a              cmp       cx,0Ah
  004ba581  bf03000000            mov       edi,3
  004ba586  0f8da4000000          jge       near 004BA630h
  004ba58c  66837e1a09            cmp       word [esi+1Ah],9
  004ba591  66897e18              mov       [esi+18h],di
  004ba595  7c06                  jl        short 004BA59Dh
  004ba597  66c7461a0800          mov       word [esi+1Ah],8

LAB_004ba59d:
  004ba59d  41                    inc       ecx
  004ba59e  663bcb                cmp       cx,bx
  004ba5a1  66894e12              mov       [esi+12h],cx
  004ba5a5  7504                  jne       short 004BA5ABh
  004ba5a7  0c02                  or        al,2
  004ba5a9  8906                  mov       [esi],eax

LAB_004ba5ab:
  004ba5ab  6683f90a              cmp       cx,0Ah
  004ba5af  7543                  jne       short 004BA5F4h
  004ba5b1  8b542468              mov       edx,[esp+68h]
  004ba5b5  8b4e54                mov       ecx,[esi+54h]
  004ba5b8  81c100400100          add       ecx,14000h
  004ba5be  66c746126400          mov       word [esi+12h],64h
  004ba5c4  8d8200010000          lea       eax,[edx+100h]
  004ba5ca  894e54                mov       [esi+54h],ecx
  004ba5cd  8d4e1c                lea       ecx,[esi+1Ch]
  004ba5d0  8d0480                lea       eax,[eax+eax*4]
  004ba5d3  51                    push      ecx
  004ba5d4  c1e008                shl       eax,8
  004ba5d7  89465c                mov       [esi+5Ch],eax
  004ba5da  8b06                  mov       eax,[esi]
  004ba5dc  6800120000            push      1200h
  004ba5e1  0bc7                  or        eax,edi
  004ba5e3  6800060000            push      600h
  004ba5e8  6a24                  push      24h
  004ba5ea  8906                  mov       [esi],eax
  004ba5ec  e82f350800            call      0053DB20h
  004ba5f1  83c410                add       esp,10h

LAB_004ba5f4:
  004ba5f4  8b442410              mov       eax,[esp+10h]
  004ba5f8  8d9000010000          lea       edx,[eax+100h]
  004ba5fe  81e2ff0f0000          and       edx,0FFFh
  004ba604  81fa00020000          cmp       edx,200h
  004ba60a  7e24                  jle       short 004BA630h
  004ba60c  25ff0f0000            and       eax,0FFFh
  004ba611  3d00080000            cmp       eax,800h
  004ba616  7d09                  jge       short 004BA621h
  004ba618  8b442468              mov       eax,[esp+68h]
  004ba61c  83c032                add       eax,32h
  004ba61f  eb0b                  jmp       short 004BA62Ch

LAB_004ba621:
  004ba621  8b4c2468              mov       ecx,[esp+68h]
  004ba625  b8ceffffff            mov       eax,0FFFFFFCEh
  004ba62a  2bc1                  sub       eax,ecx

LAB_004ba62c:
  004ba62c  66014632              add       [esi+32h],ax

LAB_004ba630:
  004ba630  66837e1264            cmp       word [esi+12h],64h
  004ba635  b965000000            mov       ecx,65h
  004ba63a  752e                  jne       short 004BA66Ah
  004ba63c  8b465c                mov       eax,[esi+5Ch]
  004ba63f  66897e18              mov       [esi+18h],di
  004ba643  8bd0                  mov       edx,eax
  004ba645  c1fa02                sar       edx,2
  004ba648  2bc2                  sub       eax,edx
  004ba64a  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004ba64f  89465c                mov       [esi+5Ch],eax
  004ba652  7c06                  jl        short 004BA65Ah
  004ba654  66c7461a0d00          mov       word [esi+1Ah],0Dh

LAB_004ba65a:
  004ba65a  2500f0ffff            and       eax,0FFFFF000h
  004ba65f  3d00a00000            cmp       eax,0A000h
  004ba664  7d04                  jge       short 004BA66Ah
  004ba666  66894e12              mov       [esi+12h],cx

LAB_004ba66a:
  004ba66a  66394e12              cmp       [esi+12h],cx
  004ba66e  7516                  jne       short 004BA686h
  004ba670  8b0e                  mov       ecx,[esi]
  004ba672  66897e18              mov       [esi+18h],di
  004ba676  83e1fd                and       ecx,0FFFFFFFDh
  004ba679  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004ba67e  890e                  mov       [esi],ecx
  004ba680  7504                  jne       short 004BA686h

LAB_004ba682:
  004ba682  c6464300              mov       byte [esi+43h],0

LAB_004ba686:
  004ba686  0fbf4632              movsx     eax,word [esi+32h]
  004ba68a  50                    push      eax
  004ba68b  e88cb30800            call      00545A1Ch
  004ba690  0fbf4e32              movsx     ecx,word [esi+32h]
  004ba694  51                    push      ecx
  004ba695  89442440              mov       [esp+40h],eax
  004ba699  e878b30800            call      00545A16h
  004ba69e  8b5650                mov       edx,[esi+50h]
  004ba6a1  89442444              mov       [esp+44h],eax
  004ba6a5  8bc2                  mov       eax,edx
  004ba6a7  8b6e54                mov       ebp,[esi+54h]
  004ba6aa  c1f808                sar       eax,8
  004ba6ad  8bfa                  mov       edi,edx
  004ba6af  8b4c2418              mov       ecx,[esp+18h]
  004ba6b3  2bf8                  sub       edi,eax
  004ba6b5  0faf4e5c              imul      ecx,[esi+5Ch]
  004ba6b9  c1fa03                sar       edx,3
  004ba6bc  2bfa                  sub       edi,edx
  004ba6be  8bdd                  mov       ebx,ebp
  004ba6c0  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004ba6c7  8bc2                  mov       eax,edx
  004ba6c9  897e50                mov       [esi+50h],edi
  004ba6cc  c1f808                sar       eax,8
  004ba6cf  c1fb08                sar       ebx,8
  004ba6d2  2bc3                  sub       eax,ebx
  004ba6d4  56                    push      esi
  004ba6d5  2bc2                  sub       eax,edx
  004ba6d7  8b5658                mov       edx,[esi+58h]
  004ba6da  8bda                  mov       ebx,edx
  004ba6dc  03c5                  add       eax,ebp
  004ba6de  c1fb03                sar       ebx,3
  004ba6e1  8bea                  mov       ebp,edx
  004ba6e3  894654                mov       [esi+54h],eax
  004ba6e6  2beb                  sub       ebp,ebx
  004ba6e8  8b5e20                mov       ebx,[esi+20h]
  004ba6eb  c1fa08                sar       edx,8
  004ba6ee  c1f90c                sar       ecx,0Ch
  004ba6f1  2bea                  sub       ebp,edx
  004ba6f3  8bd1                  mov       edx,ecx
  004ba6f5  0faf542440            imul      edx,[esp+40h]
  004ba6fa  0faf4c2448            imul      ecx,[esp+48h]
  004ba6ff  c1fa0c                sar       edx,0Ch
  004ba702  c1f90c                sar       ecx,0Ch
  004ba705  03d7                  add       edx,edi
  004ba707  8b7e1c                mov       edi,[esi+1Ch]
  004ba70a  03d8                  add       ebx,eax
  004ba70c  8b4624                mov       eax,[esi+24h]
  004ba70f  03cd                  add       ecx,ebp
  004ba711  03fa                  add       edi,edx
  004ba713  03c1                  add       eax,ecx
  004ba715  896e58                mov       [esi+58h],ebp
  004ba718  897e1c                mov       [esi+1Ch],edi
  004ba71b  895e20                mov       [esi+20h],ebx
  004ba71e  894624                mov       [esi+24h],eax
  004ba721  e81a920700            call      00533940h
  004ba726  8b06                  mov       eax,[esi]
  004ba728  8bbe90000000          mov       edi,[esi+90h]
  004ba72e  8bc8                  mov       ecx,eax
  004ba730  83c404                add       esp,4
  004ba733  81e7fffcffff          and       edi,0FFFFFCFFh
  004ba739  83e101                and       ecx,1
  004ba73c  89be90000000          mov       [esi+90h],edi
  004ba742  740a                  je        short 004BA74Eh
  004ba744  8b5660                mov       edx,[esi+60h]
  004ba747  8b5e20                mov       ebx,[esi+20h]
  004ba74a  3bda                  cmp       ebx,edx
  004ba74c  7e18                  jle       short 004BA766h

LAB_004ba74e:
  004ba74e  33db                  xor       ebx,ebx
  004ba750  3bcb                  cmp       ecx,ebx
  004ba752  7536                  jne       short 004BA78Ah
  004ba754  8b6e20                mov       ebp,[esi+20h]
  004ba757  8b5660                mov       edx,[esi+60h]
  004ba75a  81ed00c00000          sub       ebp,0C000h
  004ba760  3bea                  cmp       ebp,edx
  004ba762  7f26                  jg        short 004BA78Ah
  004ba764  eb02                  jmp       short 004BA768h

LAB_004ba766:
  004ba766  33db                  xor       ebx,ebx

LAB_004ba768:
  004ba768  f7c700a00000          test      edi,0A000h
  004ba76e  751a                  jne       short 004BA78Ah
  004ba770  a802                  test      al,2
  004ba772  895620                mov       [esi+20h],edx
  004ba775  750c                  jne       short 004BA783h
  004ba777  3bcb                  cmp       ecx,ebx
  004ba779  7408                  je        short 004BA783h
  004ba77b  385e43                cmp       [esi+43h],bl
  004ba77e  7403                  je        short 004BA783h
  004ba780  885e43                mov       [esi+43h],bl

LAB_004ba783:
  004ba783  24fe                  and       al,0FEh
  004ba785  895e54                mov       [esi+54h],ebx
  004ba788  8906                  mov       [esi],eax

LAB_004ba78a:
  004ba78a  f7c782ad0020          test      edi,2000AD82h
  004ba790  7410                  je        short 004BA7A2h
  004ba792  f60601                test      byte [esi],1
  004ba795  7508                  jne       short 004BA79Fh
  004ba797  c6464201              mov       byte [esi+42h],1
  004ba79b  c6464301              mov       byte [esi+43h],1

LAB_004ba79f:
  004ba79f  895e5c                mov       [esi+5Ch],ebx

LAB_004ba7a2:
  004ba7a2  6aff                  push      0FFFFFFFFh
  004ba7a4  6aff                  push      0FFFFFFFFh
  004ba7a6  6aff                  push      0FFFFFFFFh
  004ba7a8  56                    push      esi
  004ba7a9  e862900000            call      004C3810h
  004ba7ae  8b442428              mov       eax,[esp+28h]
  004ba7b2  83c410                add       esp,10h
  004ba7b5  3bc3                  cmp       eax,ebx
  004ba7b7  7511                  jne       short 004BA7CAh
  004ba7b9  668b442452            mov       ax,[esp+52h]
  004ba7be  66894632              mov       [esi+32h],ax
  004ba7c2  5f                    pop       edi
  004ba7c3  5e                    pop       esi
  004ba7c4  5d                    pop       ebp
  004ba7c5  5b                    pop       ebx
  004ba7c6  83c454                add       esp,54h
  004ba7c9  c3                    ret

LAB_004ba7ca:
  004ba7ca  385e43                cmp       [esi+43h],bl
  004ba7cd  0f85dc020000          jne       near 004BAAAFh
  004ba7d3  8b466c                mov       eax,[esi+6Ch]
  004ba7d6  8b4e1c                mov       ecx,[esi+1Ch]
  004ba7d9  8b5624                mov       edx,[esi+24h]
  004ba7dc  8b781c                mov       edi,[eax+1Ch]
  004ba7df  8b6824                mov       ebp,[eax+24h]
  004ba7e2  2bf9                  sub       edi,ecx
  004ba7e4  2bea                  sub       ebp,edx
  004ba7e6  c1ff0c                sar       edi,0Ch
  004ba7e9  c1fd0c                sar       ebp,0Ch
  004ba7ec  8bcf                  mov       ecx,edi
  004ba7ee  8bd5                  mov       edx,ebp
  004ba7f0  0fafcf                imul      ecx,edi
  004ba7f3  0fafd5                imul      edx,ebp
  004ba7f6  03ca                  add       ecx,edx
  004ba7f8  51                    push      ecx
  004ba7f9  e812b20800            call      00545A10h
  004ba7fe  55                    push      ebp
  004ba7ff  57                    push      edi
  004ba800  89442424              mov       [esp+24h],eax
  004ba804  e819b20800            call      00545A22h
  004ba809  668b4e32              mov       cx,[esi+32h]
  004ba80d  2bc1                  sub       eax,ecx
  004ba80f  25ff0f0000            and       eax,0FFFh
  004ba814  89442410              mov       [esp+10h],eax
  004ba818  8b44241c              mov       eax,[esp+1Ch]
  004ba81c  3dfa000000            cmp       eax,0FAh
  004ba821  0f8d19010000          jge       near 004BA940h
  004ba827  e814c90800            call      00547140h
  004ba82c  99                    cdq
  004ba82d  b964000000            mov       ecx,64h
  004ba832  f7f9                  idiv      ecx
  004ba834  83fa32                cmp       edx,32h
  004ba837  0f8d9d000000          jge       near 004BA8DAh
  004ba83d  8b7c2414              mov       edi,[esp+14h]
  004ba841  8b542410              mov       edx,[esp+10h]
  004ba845  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ba84c  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ba853  25ff0f0000            and       eax,0FFFh
  004ba858  3bc1                  cmp       eax,ecx
  004ba85a  7f3a                  jg        short 004BA896h
  004ba85c  e8dfc80800            call      00547140h
  004ba861  99                    cdq
  004ba862  8d4c3f01              lea       ecx,[edi+edi+1]
  004ba866  f7f9                  idiv      ecx
  004ba868  8b44241c              mov       eax,[esp+1Ch]
  004ba86c  83c264                add       edx,64h
  004ba86f  3bc2                  cmp       eax,edx
  004ba871  7d23                  jge       short 004BA896h
  004ba873  8b16                  mov       edx,[esi]
  004ba875  c7461803000000        mov       dword [esi+18h],3
  004ba87c  81e2fffffffe          and       edx,0FEFFFFFFh
  004ba882  c6464301              mov       byte [esi+43h],1
  004ba886  83ca02                or        edx,2
  004ba889  895e5c                mov       [esi+5Ch],ebx
  004ba88c  8916                  mov       [esi],edx
  004ba88e  5f                    pop       edi
  004ba88f  5e                    pop       esi
  004ba890  5d                    pop       ebp
  004ba891  5b                    pop       ebx
  004ba892  83c454                add       esp,54h
  004ba895  c3                    ret

LAB_004ba896:
  004ba896  8b442410              mov       eax,[esp+10h]
  004ba89a  8d14fd00040000        lea       edx,[edi*8+400h]
  004ba8a1  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  004ba8a8  81e1ff0f0000          and       ecx,0FFFh
  004ba8ae  3bca                  cmp       ecx,edx
  004ba8b0  0f8fa3010000          jg        near 004BAA59h
  004ba8b6  8b06                  mov       eax,[esi]
  004ba8b8  c6464207              mov       byte [esi+42h],7
  004ba8bc  25fffffffe            and       eax,0FEFFFFFFh
  004ba8c1  c6464301              mov       byte [esi+43h],1
  004ba8c5  8906                  mov       [esi],eax
  004ba8c7  c7461803000000        mov       dword [esi+18h],3
  004ba8ce  66895e12              mov       [esi+12h],bx
  004ba8d2  5f                    pop       edi
  004ba8d3  5e                    pop       esi
  004ba8d4  5d                    pop       ebp
  004ba8d5  5b                    pop       ebx
  004ba8d6  83c454                add       esp,54h
  004ba8d9  c3                    ret

LAB_004ba8da:
  004ba8da  83fa46                cmp       edx,46h
  004ba8dd  0f8c02010000          jl        near 004BA9E5h
  004ba8e3  83fa50                cmp       edx,50h
  004ba8e6  0f8db2010000          jge       near 004BAA9Eh
  004ba8ec  c6464203              mov       byte [esi+42h],3
  004ba8f0  e84bc80800            call      00547140h
  004ba8f5  99                    cdq
  004ba8f6  b90a000000            mov       ecx,0Ah
  004ba8fb  f7f9                  idiv      ecx
  004ba8fd  8b442414              mov       eax,[esp+14h]
  004ba901  8b4c2468              mov       ecx,[esp+68h]
  004ba905  d1f8                  sar       eax,1
  004ba907  8db980000000          lea       edi,[ecx+80h]
  004ba90d  c1e704                shl       edi,4
  004ba910  02d0                  add       dl,al
  004ba912  80c214                add       dl,14h
  004ba915  885643                mov       [esi+43h],dl
  004ba918  e823c80800            call      00547140h
  004ba91d  99                    cdq
  004ba91e  f7ff                  idiv      edi
  004ba920  d1ff                  sar       edi,1
  004ba922  2bd7                  sub       edx,edi
  004ba924  8b7c2410              mov       edi,[esp+10h]
  004ba928  03d7                  add       edx,edi
  004ba92a  66035632              add       dx,[esi+32h]
  004ba92e  81c200080000          add       edx,800h
  004ba934  66895610              mov       [esi+10h],dx
  004ba938  5f                    pop       edi
  004ba939  5e                    pop       esi
  004ba93a  5d                    pop       ebp
  004ba93b  5b                    pop       ebx
  004ba93c  83c454                add       esp,54h
  004ba93f  c3                    ret

LAB_004ba940:
  004ba940  3d58020000            cmp       eax,258h
  004ba945  0f8dc7000000          jge       near 004BAA12h
  004ba94b  e8f0c70800            call      00547140h
  004ba950  99                    cdq
  004ba951  b964000000            mov       ecx,64h
  004ba956  f7f9                  idiv      ecx
  004ba958  83fa1e                cmp       edx,1Eh
  004ba95b  7d2c                  jge       short 004BA989h
  004ba95d  8b542410              mov       edx,[esp+10h]
  004ba961  81c200040000          add       edx,400h
  004ba967  81e2ff0f0000          and       edx,0FFFh
  004ba96d  81fa00080000          cmp       edx,800h
  004ba973  0f8cab000000          jl        near 004BAA24h
  004ba979  c6464201              mov       byte [esi+42h],1
  004ba97d  c6464301              mov       byte [esi+43h],1
  004ba981  5f                    pop       edi
  004ba982  5e                    pop       esi
  004ba983  5d                    pop       ebp
  004ba984  5b                    pop       ebx
  004ba985  83c454                add       esp,54h
  004ba988  c3                    ret

LAB_004ba989:
  004ba989  83fa32                cmp       edx,32h
  004ba98c  7d4e                  jge       short 004BA9DCh
  004ba98e  c6464203              mov       byte [esi+42h],3
  004ba992  e8a9c70800            call      00547140h
  004ba997  99                    cdq
  004ba998  b90a000000            mov       ecx,0Ah
  004ba99d  f7f9                  idiv      ecx
  004ba99f  8b442414              mov       eax,[esp+14h]
  004ba9a3  8b4c2468              mov       ecx,[esp+68h]
  004ba9a7  d1f8                  sar       eax,1
  004ba9a9  8db980000000          lea       edi,[ecx+80h]
  004ba9af  c1e704                shl       edi,4
  004ba9b2  02d0                  add       dl,al
  004ba9b4  80c214                add       dl,14h
  004ba9b7  885643                mov       [esi+43h],dl
  004ba9ba  e881c70800            call      00547140h
  004ba9bf  99                    cdq
  004ba9c0  f7ff                  idiv      edi
  004ba9c2  8b4c2410              mov       ecx,[esp+10h]
  004ba9c6  d1ff                  sar       edi,1
  004ba9c8  2bd7                  sub       edx,edi
  004ba9ca  03d1                  add       edx,ecx
  004ba9cc  66035632              add       dx,[esi+32h]
  004ba9d0  66895610              mov       [esi+10h],dx
  004ba9d4  5f                    pop       edi
  004ba9d5  5e                    pop       esi
  004ba9d6  5d                    pop       ebp
  004ba9d7  5b                    pop       ebx
  004ba9d8  83c454                add       esp,54h
  004ba9db  c3                    ret

LAB_004ba9dc:
  004ba9dc  83fa5a                cmp       edx,5Ah
  004ba9df  0f8db9000000          jge       near 004BAA9Eh

LAB_004ba9e5:
  004ba9e5  c6464202              mov       byte [esi+42h],2
  004ba9e9  e852c70800            call      00547140h
  004ba9ee  99                    cdq
  004ba9ef  b90a000000            mov       ecx,0Ah
  004ba9f4  66c746106400          mov       word [esi+10h],64h
  004ba9fa  f7f9                  idiv      ecx
  004ba9fc  8b442414              mov       eax,[esp+14h]
  004baa00  d1f8                  sar       eax,1
  004baa02  02d0                  add       dl,al
  004baa04  80c214                add       dl,14h
  004baa07  885643                mov       [esi+43h],dl
  004baa0a  5f                    pop       edi
  004baa0b  5e                    pop       esi
  004baa0c  5d                    pop       ebp
  004baa0d  5b                    pop       ebx
  004baa0e  83c454                add       esp,54h
  004baa11  c3                    ret

LAB_004baa12:
  004baa12  e829c70800            call      00547140h
  004baa17  99                    cdq
  004baa18  b964000000            mov       ecx,64h
  004baa1d  f7f9                  idiv      ecx
  004baa1f  83fa0a                cmp       edx,0Ah
  004baa22  7d30                  jge       short 004BAA54h

LAB_004baa24:
  004baa24  885e42                mov       [esi+42h],bl
  004baa27  e814c70800            call      00547140h
  004baa2c  99                    cdq
  004baa2d  b90a000000            mov       ecx,0Ah
  004baa32  895e18                mov       [esi+18h],ebx
  004baa35  f7f9                  idiv      ecx
  004baa37  8b442414              mov       eax,[esp+14h]
  004baa3b  c1f802                sar       eax,2
  004baa3e  02d0                  add       dl,al
  004baa40  8b06                  mov       eax,[esi]
  004baa42  0d00000001            or        eax,1000000h
  004baa47  885643                mov       [esi+43h],dl
  004baa4a  8906                  mov       [esi],eax
  004baa4c  5f                    pop       edi
  004baa4d  5e                    pop       esi
  004baa4e  5d                    pop       ebp
  004baa4f  5b                    pop       ebx
  004baa50  83c454                add       esp,54h
  004baa53  c3                    ret

LAB_004baa54:
  004baa54  83fa32                cmp       edx,32h
  004baa57  7d10                  jge       short 004BAA69h

LAB_004baa59:
  004baa59  c6464201              mov       byte [esi+42h],1
  004baa5d  c6464301              mov       byte [esi+43h],1
  004baa61  5f                    pop       edi
  004baa62  5e                    pop       esi
  004baa63  5d                    pop       ebp
  004baa64  5b                    pop       ebx
  004baa65  83c454                add       esp,54h
  004baa68  c3                    ret

LAB_004baa69:
  004baa69  83fa5a                cmp       edx,5Ah
  004baa6c  7d30                  jge       short 004BAA9Eh
  004baa6e  c6464203              mov       byte [esi+42h],3
  004baa72  e8c9c60800            call      00547140h
  004baa77  99                    cdq
  004baa78  b90a000000            mov       ecx,0Ah
  004baa7d  f7f9                  idiv      ecx
  004baa7f  8b442414              mov       eax,[esp+14h]
  004baa83  d1f8                  sar       eax,1
  004baa85  02d0                  add       dl,al
  004baa87  80c214                add       dl,14h
  004baa8a  885643                mov       [esi+43h],dl
  004baa8d  e8aec60800            call      00547140h
  004baa92  66894610              mov       [esi+10h],ax
  004baa96  5f                    pop       edi
  004baa97  5e                    pop       esi
  004baa98  5d                    pop       ebp
  004baa99  5b                    pop       ebx
  004baa9a  83c454                add       esp,54h
  004baa9d  c3                    ret

LAB_004baa9e:
  004baa9e  c6464206              mov       byte [esi+42h],6
  004baaa2  c6464301              mov       byte [esi+43h],1
  004baaa6  e895c60800            call      00547140h
  004baaab  66894610              mov       [esi+10h],ax

LAB_004baaaf:
  004baaaf  5f                    pop       edi
  004baab0  5e                    pop       esi
  004baab1  5d                    pop       ebp
  004baab2  5b                    pop       ebx
  004baab3  83c454                add       esp,54h
  004baab6  c3                    ret
  004baab7  90                    nop
  004baab8  1e                    push      ds
  004baab9  a34b0048a3            mov       [0A348004Bh],eax
  004baabe  4b                    dec       ebx
  004baabf  00aaa34b0034          add       [edx+34004BA3h],ch
  004baac5  a4                    movsb
  004baac6  4b                    dec       ebx
  004baac7  0086a64b0086          add       [esi-79FFB45Ah],al
  004baacd  a6                    cmpsb
  004baace  4b                    dec       ebx
  004baacf  00f8                  add       al,bh
  004baad1  a4                    movsb
  004baad2  4b                    dec       ebx
  004baad3  006da5                add       [ebp-5Bh],ch
  004baad6  4b                    dec       ebx
  004baad7  009090909090          add       [eax-6F6F6F70h],dl
  004baadd  90                    nop
  004baade  90                    nop
  004baadf  90                    nop

55 cible(s) de saut interne : 0x004ba159, 0x004ba210, 0x004ba231, 0x004ba24a, 0x004ba2c0, 0x004ba2d1, 0x004ba2d5, 0x004ba2eb, 0x004ba2fa, 0x004ba30a, 0x004ba37a, 0x004ba385, 0x004ba3b4, 0x004ba3b8, 0x004ba3c4, 0x004ba3d0, 0x004ba3e5, 0x004ba415, 0x004ba41c, 0x004ba420, 0x004ba440, 0x004ba455, 0x004ba4dd, 0x004ba57d, 0x004ba59d, 0x004ba5ab, 0x004ba5f4, 0x004ba621, 0x004ba62c, 0x004ba630, 0x004ba65a, 0x004ba66a, 0x004ba682, 0x004ba686, 0x004ba74e, 0x004ba766, 0x004ba768, 0x004ba783, 0x004ba78a, 0x004ba79f, 0x004ba7a2, 0x004ba7ca, 0x004ba896, 0x004ba8da, 0x004ba940, 0x004ba989, 0x004ba9dc, 0x004ba9e5, 0x004baa12, 0x004baa24, 0x004baa54, 0x004baa59, 0x004baa69, 0x004baa9e, 0x004baaaf
