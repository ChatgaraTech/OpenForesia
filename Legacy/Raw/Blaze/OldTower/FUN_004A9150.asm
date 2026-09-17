Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a9150  section .text  offset fichier 0xa8550  3872 octets

  004a9150  81ec94000000          sub       esp,94h
  004a9156  53                    push      ebx
  004a9157  55                    push      ebp
  004a9158  56                    push      esi
  004a9159  8bb424a4000000        mov       esi,[esp+0A4h]
  004a9160  b864000000            mov       eax,64h
  004a9165  33db                  xor       ebx,ebx
  004a9167  668944243e            mov       [esp+3Eh],ax
  004a916c  6689442442            mov       [esp+42h],ax
  004a9171  6689442432            mov       [esp+32h],ax
  004a9176  6689442436            mov       [esp+36h],ax
  004a917b  8a4641                mov       al,[esi+41h]
  004a917e  57                    push      edi
  004a917f  84c0                  test      al,al
  004a9181  66895c243c            mov       [esp+3Ch],bx
  004a9186  66c744243e1e00        mov       word [esp+3Eh],1Eh
  004a918d  66c74424403200        mov       word [esp+40h],32h
  004a9194  66895c2444            mov       [esp+44h],bx
  004a9199  66895c2430            mov       [esp+30h],bx
  004a919e  66895c2432            mov       [esp+32h],bx
  004a91a3  66c74424345000        mov       word [esp+34h],50h
  004a91aa  66895c2438            mov       [esp+38h],bx
  004a91af  750f                  jne       short 004A91C0h
  004a91b1  53                    push      ebx
  004a91b2  53                    push      ebx
  004a91b3  56                    push      esi
  004a91b4  c6464101              mov       byte [esi+41h],1
  004a91b8  e893efffff            call      004A8150h
  004a91bd  83c40c                add       esp,0Ch

LAB_004a91c0:
  004a91c0  668b4e16              mov       cx,[esi+16h]
  004a91c4  8b7e6c                mov       edi,[esi+6Ch]
  004a91c7  6683f902              cmp       cx,2
  004a91cb  897c2420              mov       [esp+20h],edi
  004a91cf  7509                  jne       short 004A91DAh
  004a91d1  bfe0a55f00            mov       edi,5FA5E0h
  004a91d6  897c2420              mov       [esp+20h],edi

LAB_004a91da:
  004a91da  8b2e                  mov       ebp,[esi]
  004a91dc  83e5fe                and       ebp,0FFFFFFFEh
  004a91df  6683f902              cmp       cx,2
  004a91e3  892e                  mov       [esi],ebp
  004a91e5  8bc5                  mov       eax,ebp
  004a91e7  7505                  jne       short 004A91EEh
  004a91e9  80cc10                or        ah,10h
  004a91ec  8906                  mov       [esi],eax

LAB_004a91ee:
  004a91ee  8b461c                mov       eax,[esi+1Ch]
  004a91f1  8b4e24                mov       ecx,[esi+24h]
  004a91f4  8b5664                mov       edx,[esi+64h]
  004a91f7  8d6e1c                lea       ebp,[esi+1Ch]
  004a91fa  8944246c              mov       [esp+6Ch],eax
  004a91fe  8b4660                mov       eax,[esi+60h]
  004a9201  894c2474              mov       [esp+74h],ecx
  004a9205  668b4e32              mov       cx,[esi+32h]
  004a9209  89542464              mov       [esp+64h],edx
  004a920d  8b567c                mov       edx,[esi+7Ch]
  004a9210  8944244c              mov       [esp+4Ch],eax
  004a9214  668b8690000000        mov       ax,[esi+90h]
  004a921b  66898c2492000000      mov       [esp+92h],cx
  004a9223  8b8e80000000          mov       ecx,[esi+80h]
  004a9229  89542468              mov       [esp+68h],edx
  004a922d  8b9684000000          mov       edx,[esi+84h]
  004a9233  6689442448            mov       [esp+48h],ax
  004a9238  8b8688000000          mov       eax,[esi+88h]
  004a923e  894c2450              mov       [esp+50h],ecx
  004a9242  8b8e8c000000          mov       ecx,[esi+8Ch]
  004a9248  89542454              mov       [esp+54h],edx
  004a924c  89442460              mov       [esp+60h],eax
  004a9250  8d54242c              lea       edx,[esp+2Ch]
  004a9254  894c2458              mov       [esp+58h],ecx
  004a9258  8d442478              lea       eax,[esp+78h]
  004a925c  52                    push      edx
  004a925d  8d4c2460              lea       ecx,[esp+60h]
  004a9261  50                    push      eax
  004a9262  51                    push      ecx
  004a9263  56                    push      esi
  004a9264  e817f70800            call      00538980h
  004a9269  83c410                add       esp,10h
  004a926c  66837e1602            cmp       word [esi+16h],2
  004a9271  743a                  je        short 004A92ADh
  004a9273  8d542428              lea       edx,[esp+28h]
  004a9277  8d442414              lea       eax,[esp+14h]
  004a927b  52                    push      edx
  004a927c  8d4c241c              lea       ecx,[esp+1Ch]
  004a9280  50                    push      eax
  004a9281  8d542424              lea       edx,[esp+24h]
  004a9285  51                    push      ecx
  004a9286  8d44241c              lea       eax,[esp+1Ch]
  004a928a  52                    push      edx
  004a928b  50                    push      eax
  004a928c  56                    push      esi
  004a928d  e88ecc0800            call      00535F20h
  004a9292  8b4c2428              mov       ecx,[esp+28h]
  004a9296  b840000000            mov       eax,40h
  004a929b  83c418                add       esp,18h
  004a929e  2bc1                  sub       eax,ecx
  004a92a0  89442424              mov       [esp+24h],eax
  004a92a4  8b442428              mov       eax,[esp+28h]
  004a92a8  e99e000000            jmp       004A934Bh

LAB_004a92ad:
  004a92ad  33c0                  xor       eax,eax
  004a92af  8b1504a65f00          mov       edx,[5FA604h]
  004a92b5  8a4608                mov       al,[esi+8]
  004a92b8  895c2410              mov       [esp+10h],ebx
  004a92bc  c744242440000000      mov       dword [esp+24h],40h
  004a92c4  8d04c0                lea       eax,[eax+eax*8]
  004a92c7  c1e005                shl       eax,5
  004a92ca  0560a55e00            add       eax,5EA560h
  004a92cf  8944245c              mov       [esp+5Ch],eax
  004a92d3  8b4840                mov       ecx,[eax+40h]
  004a92d6  8b4624                mov       eax,[esi+24h]
  004a92d9  894c2428              mov       [esp+28h],ecx
  004a92dd  8b4d00                mov       ecx,[ebp]
  004a92e0  2bd0                  sub       edx,eax
  004a92e2  a1fca55f00            mov       eax,[5FA5FCh]
  004a92e7  2bc1                  sub       eax,ecx
  004a92e9  c1fa0c                sar       edx,0Ch
  004a92ec  c1f80c                sar       eax,0Ch
  004a92ef  52                    push      edx
  004a92f0  50                    push      eax
  004a92f1  e82cc70900            call      00545A22h
  004a92f6  668b4e32              mov       cx,[esi+32h]
  004a92fa  2bc1                  sub       eax,ecx
  004a92fc  25ff0f0000            and       eax,0FFFh
  004a9301  89442418              mov       [esp+18h],eax
  004a9305  8b442428              mov       eax,[esp+28h]
  004a9309  a808                  test      al,8
  004a930b  7412                  je        short 004A931Fh
  004a930d  e82ede0900            call      00547140h
  004a9312  25ff0f0000            and       eax,0FFFh
  004a9317  89442418              mov       [esp+18h],eax
  004a931b  8b442428              mov       eax,[esp+28h]

LAB_004a931f:
  004a931f  f6c480                test      ah,80h
  004a9322  c744241400100000      mov       dword [esp+14h],1000h
  004a932a  7408                  je        short 004A9334h
  004a932c  c7442414cc0c0000      mov       dword [esp+14h],0CCCh

LAB_004a9334:
  004a9334  a804                  test      al,4
  004a9336  7408                  je        short 004A9340h
  004a9338  c744241400080000      mov       dword [esp+14h],800h

LAB_004a9340:
  004a9340  a9c0010000            test      eax,1C0h
  004a9345  7404                  je        short 004A934Bh
  004a9347  895c2414              mov       [esp+14h],ebx

LAB_004a934b:
  004a934b  50                    push      eax
  004a934c  56                    push      esi
  004a934d  e88eca0200            call      004D5DE0h
  004a9352  8b4500                mov       eax,[ebp]
  004a9355  8b571c                mov       edx,[edi+1Ch]
  004a9358  8b4e24                mov       ecx,[esi+24h]
  004a935b  2bc2                  sub       eax,edx
  004a935d  8b5724                mov       edx,[edi+24h]
  004a9360  83c408                add       esp,8
  004a9363  c1f80c                sar       eax,0Ch
  004a9366  2bca                  sub       ecx,edx
  004a9368  8bd0                  mov       edx,eax
  004a936a  c1f90c                sar       ecx,0Ch
  004a936d  0fafd0                imul      edx,eax
  004a9370  8bc1                  mov       eax,ecx
  004a9372  0fafc1                imul      eax,ecx
  004a9375  03d0                  add       edx,eax
  004a9377  52                    push      edx
  004a9378  e893c60900            call      00545A10h
  004a937d  8b7c2414              mov       edi,[esp+14h]
  004a9381  8944241c              mov       [esp+1Ch],eax
  004a9385  3bfb                  cmp       edi,ebx
  004a9387  0f84d6070000          je        near 004A9B63h
  004a938d  8b1e                  mov       ebx,[esi]
  004a938f  33c9                  xor       ecx,ecx
  004a9391  8a4e04                mov       cl,[esi+4]
  004a9394  81e3fffffffd          and       ebx,0FDFFFFFFh
  004a939a  0fbf5618              movsx     edx,word [esi+18h]
  004a939e  891e                  mov       [esi],ebx
  004a93a0  8b0c8d40965e00        mov       ecx,[ecx*4+5E9640h]
  004a93a7  8bc3                  mov       eax,ebx
  004a93a9  8b0c91                mov       ecx,[ecx+edx*4]
  004a93ac  a900000001            test      eax,1000000h
  004a93b1  741d                  je        short 004A93D0h
  004a93b3  66ff461a              inc       word [esi+1Ah]
  004a93b7  668b461a              mov       ax,[esi+1Ah]
  004a93bb  33d2                  xor       edx,edx
  004a93bd  668b5116              mov       dx,[ecx+16h]
  004a93c1  0fbfc0                movsx     eax,ax
  004a93c4  3bc2                  cmp       eax,edx
  004a93c6  7c29                  jl        short 004A93F1h
  004a93c8  66c7461a0000          mov       word [esi+1Ah],0
  004a93ce  eb21                  jmp       short 004A93F1h

LAB_004a93d0:
  004a93d0  66ff461a              inc       word [esi+1Ah]
  004a93d4  668b561a              mov       dx,[esi+1Ah]
  004a93d8  33db                  xor       ebx,ebx
  004a93da  668b5916              mov       bx,[ecx+16h]
  004a93de  0fbfca                movsx     ecx,dx
  004a93e1  3bcb                  cmp       ecx,ebx
  004a93e3  7c0c                  jl        short 004A93F1h
  004a93e5  4a                    dec       edx
  004a93e6  0d00000002            or        eax,2000000h
  004a93eb  6689561a              mov       [esi+1Ah],dx
  004a93ef  8906                  mov       [esi],eax

LAB_004a93f1:
  004a93f1  8a4642                mov       al,[esi+42h]
  004a93f4  bb10000000            mov       ebx,10h
  004a93f9  3c05                  cmp       al,5
  004a93fb  8b06                  mov       eax,[esi]
  004a93fd  7504                  jne       short 004A9403h
  004a93ff  0bc3                  or        eax,ebx
  004a9401  eb02                  jmp       short 004A9405h

LAB_004a9403:
  004a9403  24ef                  and       al,0EFh

LAB_004a9405:
  004a9405  8906                  mov       [esi],eax
  004a9407  8a06                  mov       al,[esi]
  004a9409  a802                  test      al,2
  004a940b  0f8461010000          je        near 004A9572h
  004a9411  66837e1601            cmp       word [esi+16h],1
  004a9416  746b                  je        short 004A9483h
  004a9418  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004a941d  0f8c38010000          jl        near 004A955Bh
  004a9423  8d54243c              lea       edx,[esp+3Ch]
  004a9427  6a00                  push      0
  004a9429  52                    push      edx
  004a942a  55                    push      ebp
  004a942b  56                    push      esi
  004a942c  e8ff070700            call      00519C30h
  004a9431  8bf8                  mov       edi,eax
  004a9433  a1b8e05d00            mov       eax,[5DE0B8h]
  004a9438  83c410                add       esp,10h
  004a943b  85c0                  test      eax,eax
  004a943d  0f8414010000          je        near 004A9557h
  004a9443  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004a9448  0f8509010000          jne       near 004A9557h
  004a944e  66899f96000000        mov       [edi+96h],bx
  004a9455  8b1f                  mov       ebx,[edi]
  004a9457  83cb40                or        ebx,40h
  004a945a  57                    push      edi
  004a945b  56                    push      esi
  004a945c  891f                  mov       [edi],ebx
  004a945e  e81d070900            call      00539B80h
  004a9463  83c408                add       esp,8
  004a9466  83c71c                add       edi,1Ch
  004a9469  57                    push      edi
  004a946a  6800180000            push      1800h
  004a946f  6800030000            push      300h
  004a9474  6a03                  push      3
  004a9476  e8a5460900            call      0053DB20h
  004a947b  83c410                add       esp,10h
  004a947e  e9d4000000            jmp       004A9557h

LAB_004a9483:
  004a9483  8d442430              lea       eax,[esp+30h]
  004a9487  6a00                  push      0
  004a9489  50                    push      eax
  004a948a  55                    push      ebp
  004a948b  56                    push      esi
  004a948c  e89f070700            call      00519C30h
  004a9491  8bf8                  mov       edi,eax
  004a9493  a1b8e05d00            mov       eax,[5DE0B8h]
  004a9498  83c410                add       esp,10h
  004a949b  85c0                  test      eax,eax
  004a949d  0f84b4000000          je        near 004A9557h
  004a94a3  66899f96000000        mov       [edi+96h],bx
  004a94aa  8b1f                  mov       ebx,[edi]
  004a94ac  83cb40                or        ebx,40h
  004a94af  57                    push      edi
  004a94b0  891f                  mov       [edi],ebx
  004a94b2  8b4c2430              mov       ecx,[esp+30h]
  004a94b6  56                    push      esi
  004a94b7  668b4130              mov       ax,[ecx+30h]
  004a94bb  0fbfd8                movsx     ebx,ax
  004a94be  66d1f8                sar       ax,1
  004a94c1  66894130              mov       [ecx+30h],ax
  004a94c5  e8b6060900            call      00539B80h
  004a94ca  8b4c2434              mov       ecx,[esp+34h]
  004a94ce  83c408                add       esp,8
  004a94d1  8d571c                lea       edx,[edi+1Ch]
  004a94d4  52                    push      edx
  004a94d5  6800180000            push      1800h
  004a94da  6800030000            push      300h
  004a94df  6a03                  push      3
  004a94e1  66895930              mov       [ecx+30h],bx
  004a94e5  e836460900            call      0053DB20h
  004a94ea  8b07                  mov       eax,[edi]
  004a94ec  83c410                add       esp,10h
  004a94ef  a900000040            test      eax,40000000h
  004a94f4  7561                  jne       short 004A9557h
  004a94f6  6aff                  push      0FFFFFFFFh
  004a94f8  6a00                  push      0
  004a94fa  6a39                  push      39h
  004a94fc  56                    push      esi
  004a94fd  e8ce14fdff            call      0047A9D0h
  004a9502  8b17                  mov       edx,[edi]
  004a9504  83c410                add       esp,10h
  004a9507  83ca01                or        edx,1
  004a950a  8917                  mov       [edi],edx
  004a950c  0fbf4632              movsx     eax,word [esi+32h]
  004a9510  50                    push      eax
  004a9511  e806c50900            call      00545A1Ch
  004a9516  8bd8                  mov       ebx,eax
  004a9518  e823dc0900            call      00547140h
  004a951d  99                    cdq
  004a951e  b90a000000            mov       ecx,0Ah
  004a9523  f7f9                  idiv      ecx
  004a9525  83c219                add       edx,19h
  004a9528  0fafda                imul      ebx,edx
  004a952b  895f50                mov       [edi+50h],ebx
  004a952e  0fbf5632              movsx     edx,word [esi+32h]
  004a9532  52                    push      edx
  004a9533  e8dec40900            call      00545A16h
  004a9538  8bd8                  mov       ebx,eax
  004a953a  e801dc0900            call      00547140h
  004a953f  99                    cdq
  004a9540  b90a000000            mov       ecx,0Ah
  004a9545  c7475400800100        mov       dword [edi+54h],18000h
  004a954c  f7f9                  idiv      ecx
  004a954e  83c219                add       edx,19h
  004a9551  0fafda                imul      ebx,edx
  004a9554  895f58                mov       [edi+58h],ebx

LAB_004a9557:
  004a9557  8b7c2414              mov       edi,[esp+14h]

LAB_004a955b:
  004a955b  8b06                  mov       eax,[esi]
  004a955d  a900000002            test      eax,2000000h
  004a9562  740e                  je        short 004A9572h
  004a9564  807e420a              cmp       byte [esi+42h],0Ah
  004a9568  7408                  je        short 004A9572h
  004a956a  24fd                  and       al,0FDh
  004a956c  c6464300              mov       byte [esi+43h],0
  004a9570  8906                  mov       [esi],eax

LAB_004a9572:
  004a9572  33c0                  xor       eax,eax
  004a9574  8a4642                mov       al,[esi+42h]
  004a9577  83f80e                cmp       eax,0Eh
  004a957a  0f87f1040000          ja        near 004A9A71h
  004a9580  33d2                  xor       edx,edx
  004a9582  8a9054a04a00          mov       dl,[eax+4AA054h]
  004a9588  ff249530a04a00        jmp       dword [edx*4+4AA030h]
  004a958f  33c9                  xor       ecx,ecx
  004a9591  66394e12              cmp       [esi+12h],cx
  004a9595  751a                  jne       short 004A95B1h
  004a9597  8b06                  mov       eax,[esi]
  004a9599  66894e18              mov       [esi+18h],cx
  004a959d  0d00000001            or        eax,1000000h
  004a95a2  66894e1a              mov       [esi+1Ah],cx
  004a95a6  8906                  mov       [esi],eax
  004a95a8  66c746120100          mov       word [esi+12h],1
  004a95ae  894e5c                mov       [esi+5Ch],ecx

LAB_004a95b1:
  004a95b1  0fbf4610              movsx     eax,word [esi+10h]
  004a95b5  3944241c              cmp       [esp+1Ch],eax
  004a95b9  7d09                  jge       short 004A95C4h
  004a95bb  c6464300              mov       byte [esi+43h],0
  004a95bf  e9ad040000            jmp       004A9A71h

LAB_004a95c4:
  004a95c4  8a4643                mov       al,[esi+43h]
  004a95c7  84c0                  test      al,al
  004a95c9  0f8465020000          je        near 004A9834h
  004a95cf  fec8                  dec       al
  004a95d1  894e5c                mov       [esi+5Ch],ecx
  004a95d4  884643                mov       [esi+43h],al
  004a95d7  e995040000            jmp       004A9A71h
  004a95dc  33c9                  xor       ecx,ecx
  004a95de  66394e12              cmp       [esi+12h],cx
  004a95e2  7517                  jne       short 004A95FBh
  004a95e4  8b06                  mov       eax,[esi]
  004a95e6  66894e18              mov       [esi+18h],cx
  004a95ea  0d00000001            or        eax,1000000h
  004a95ef  66894e1a              mov       [esi+1Ah],cx
  004a95f3  8906                  mov       [esi],eax
  004a95f5  66c746120100          mov       word [esi+12h],1

LAB_004a95fb:
  004a95fb  8b442418              mov       eax,[esp+18h]
  004a95ff  8d90c8000000          lea       edx,[eax+0C8h]
  004a9605  81e2ff0f0000          and       edx,0FFFh
  004a960b  81fa90010000          cmp       edx,190h
  004a9611  7f0c                  jg        short 004A961Fh
  004a9613  c6464300              mov       byte [esi+43h],0
  004a9617  894e5c                mov       [esi+5Ch],ecx
  004a961a  e952040000            jmp       004A9A71h

LAB_004a961f:
  004a961f  25ff0f0000            and       eax,0FFFh
  004a9624  3d00080000            cmp       eax,800h
  004a9629  7d15                  jge       short 004A9640h
  004a962b  8b442410              mov       eax,[esp+10h]
  004a962f  894e5c                mov       [esi+5Ch],ecx
  004a9632  0580000000            add       eax,80h
  004a9637  66014632              add       [esi+32h],ax
  004a963b  e931040000            jmp       004A9A71h

LAB_004a9640:
  004a9640  8b542410              mov       edx,[esp+10h]
  004a9644  b880ffffff            mov       eax,0FFFFFF80h
  004a9649  2bc2                  sub       eax,edx
  004a964b  894e5c                mov       [esi+5Ch],ecx
  004a964e  66014632              add       [esi+32h],ax
  004a9652  e91a040000            jmp       004A9A71h
  004a9657  8b4c2410              mov       ecx,[esp+10h]
  004a965b  33d2                  xor       edx,edx
  004a965d  66395612              cmp       [esi+12h],dx
  004a9661  b801000000            mov       eax,1
  004a9666  7525                  jne       short 004A968Dh
  004a9668  8b3e                  mov       edi,[esi]
  004a966a  66894618              mov       [esi+18h],ax
  004a966e  81cf00000001          or        edi,1000000h
  004a9674  6689561a              mov       [esi+1Ah],dx
  004a9678  893e                  mov       [esi],edi
  004a967a  8d3cc9                lea       edi,[ecx+ecx*8]
  004a967d  c1e704                shl       edi,4
  004a9680  81c700700000          add       edi,7000h
  004a9686  66894612              mov       [esi+12h],ax
  004a968a  897e5c                mov       [esi+5Ch],edi

LAB_004a968d:
  004a968d  66394612              cmp       [esi+12h],ax
  004a9691  0f85da030000          jne       near 004A9A71h
  004a9697  0fbf4610              movsx     eax,word [esi+10h]
  004a969b  3944241c              cmp       [esp+1Ch],eax
  004a969f  7d0c                  jge       short 004A96ADh
  004a96a1  c6464300              mov       byte [esi+43h],0
  004a96a5  89565c                mov       [esi+5Ch],edx
  004a96a8  e9c4030000            jmp       004A9A71h

LAB_004a96ad:
  004a96ad  8a4643                mov       al,[esi+43h]
  004a96b0  84c0                  test      al,al
  004a96b2  7405                  je        short 004A96B9h
  004a96b4  fec8                  dec       al
  004a96b6  884643                mov       [esi+43h],al

LAB_004a96b9:
  004a96b9  8b442418              mov       eax,[esp+18h]
  004a96bd  8d9000010000          lea       edx,[eax+100h]
  004a96c3  81e2ff0f0000          and       edx,0FFFh
  004a96c9  81fa00020000          cmp       edx,200h
  004a96cf  0f8e9c030000          jle       near 004A9A71h
  004a96d5  25ff0f0000            and       eax,0FFFh
  004a96da  3d00080000            cmp       eax,800h
  004a96df  7d10                  jge       short 004A96F1h
  004a96e1  8d840980000000        lea       eax,[ecx+ecx+80h]
  004a96e8  66014632              add       [esi+32h],ax
  004a96ec  e980030000            jmp       004A9A71h

LAB_004a96f1:
  004a96f1  03c9                  add       ecx,ecx
  004a96f3  b880ffffff            mov       eax,0FFFFFF80h
  004a96f8  2bc1                  sub       eax,ecx
  004a96fa  66014632              add       [esi+32h],ax
  004a96fe  e96e030000            jmp       004A9A71h
  004a9703  bb01000000            mov       ebx,1
  004a9708  66395e12              cmp       [esi+12h],bx
  004a970c  0f85a3000000          jne       near 004A97B5h
  004a9712  8a4643                mov       al,[esi+43h]
  004a9715  84c0                  test      al,al
  004a9717  7405                  je        short 004A971Eh
  004a9719  fec8                  dec       al
  004a971b  884643                mov       [esi+43h],al

LAB_004a971e:
  004a971e  8b442410              mov       eax,[esp+10h]
  004a9722  8d14c0                lea       edx,[eax+eax*8]
  004a9725  c1e204                shl       edx,4
  004a9728  8d8200500000          lea       eax,[edx+5000h]
  004a972e  0faff8                imul      edi,eax
  004a9731  89465c                mov       [esi+5Ch],eax
  004a9734  0fbf4610              movsx     eax,word [esi+10h]
  004a9738  50                    push      eax
  004a9739  c1ff0c                sar       edi,0Ch
  004a973c  e8dbc20900            call      00545A1Ch
  004a9741  0fafc7                imul      eax,edi
  004a9744  8b4d00                mov       ecx,[ebp]
  004a9747  c1f80c                sar       eax,0Ch
  004a974a  03c8                  add       ecx,eax
  004a974c  894d00                mov       [ebp],ecx
  004a974f  0fbf4e10              movsx     ecx,word [esi+10h]
  004a9753  51                    push      ecx
  004a9754  e8bdc20900            call      00545A16h
  004a9759  0fafc7                imul      eax,edi
  004a975c  8b4e24                mov       ecx,[esi+24h]
  004a975f  c1f80c                sar       eax,0Ch
  004a9762  03c8                  add       ecx,eax
  004a9764  8b442418              mov       eax,[esp+18h]
  004a9768  894e24                mov       [esi+24h],ecx
  004a976b  33c9                  xor       ecx,ecx
  004a976d  8d9000010000          lea       edx,[eax+100h]
  004a9773  894e5c                mov       [esi+5Ch],ecx
  004a9776  81e2ff0f0000          and       edx,0FFFh
  004a977c  81fa00020000          cmp       edx,200h
  004a9782  7e33                  jle       short 004A97B7h
  004a9784  25ff0f0000            and       eax,0FFFh
  004a9789  3d00080000            cmp       eax,800h
  004a978e  7d11                  jge       short 004A97A1h
  004a9790  8b442410              mov       eax,[esp+10h]
  004a9794  8d04c532000000        lea       eax,[eax*8+32h]
  004a979b  66014632              add       [esi+32h],ax
  004a979f  eb16                  jmp       short 004A97B7h

LAB_004a97a1:
  004a97a1  8b542410              mov       edx,[esp+10h]
  004a97a5  b8ceffffff            mov       eax,0FFFFFFCEh
  004a97aa  c1e203                shl       edx,3
  004a97ad  2bc2                  sub       eax,edx
  004a97af  66014632              add       [esi+32h],ax
  004a97b3  eb02                  jmp       short 004A97B7h

LAB_004a97b5:
  004a97b5  33c9                  xor       ecx,ecx

LAB_004a97b7:
  004a97b7  66394e12              cmp       [esi+12h],cx
  004a97bb  0f85b0020000          jne       near 004A9A71h
  004a97c1  8b06                  mov       eax,[esi]
  004a97c3  66894e18              mov       [esi+18h],cx
  004a97c7  0d00000001            or        eax,1000000h
  004a97cc  66894e1a              mov       [esi+1Ah],cx
  004a97d0  8906                  mov       [esi],eax
  004a97d2  66895e12              mov       [esi+12h],bx
  004a97d6  e996020000            jmp       004A9A71h
  004a97db  668b4e12              mov       cx,[esi+12h]
  004a97df  ba01000000            mov       edx,1
  004a97e4  663bca                cmp       cx,dx
  004a97e7  751c                  jne       short 004A9805h
  004a97e9  0fbf4610              movsx     eax,word [esi+10h]
  004a97ed  3944241c              cmp       [esp+1Ch],eax
  004a97f1  7d06                  jge       short 004A97F9h
  004a97f3  c6464300              mov       byte [esi+43h],0
  004a97f7  eb0c                  jmp       short 004A9805h

LAB_004a97f9:
  004a97f9  8a4643                mov       al,[esi+43h]
  004a97fc  84c0                  test      al,al
  004a97fe  7405                  je        short 004A9805h
  004a9800  fec8                  dec       al
  004a9802  884643                mov       [esi+43h],al

LAB_004a9805:
  004a9805  6685c9                test      cx,cx
  004a9808  0f8563020000          jne       near 004A9A71h
  004a980e  8b442410              mov       eax,[esp+10h]
  004a9812  8b3e                  mov       edi,[esi]
  004a9814  66894e1a              mov       [esi+1Ah],cx
  004a9818  81cf00000001          or        edi,1000000h
  004a981e  8d0cc0                lea       ecx,[eax+eax*8]
  004a9821  66895618              mov       [esi+18h],dx
  004a9825  c1e104                shl       ecx,4
  004a9828  893e                  mov       [esi],edi
  004a982a  66895612              mov       [esi+12h],dx
  004a982e  81c100500000          add       ecx,5000h

LAB_004a9834:
  004a9834  894e5c                mov       [esi+5Ch],ecx
  004a9837  e935020000            jmp       004A9A71h
  004a983c  66837e1201            cmp       word [esi+12h],1
  004a9841  8b7c2410              mov       edi,[esp+10h]
  004a9845  8b4c2418              mov       ecx,[esp+18h]
  004a9849  0f8586000000          jne       near 004A98D5h
  004a984f  8b4620                mov       eax,[esi+20h]
  004a9852  8b6e60                mov       ebp,[esi+60h]
  004a9855  0500a00000            add       eax,0A000h
  004a985a  894620                mov       [esi+20h],eax
  004a985d  2bc5                  sub       eax,ebp
  004a985f  c1f80c                sar       eax,0Ch
  004a9862  3dc8000000            cmp       eax,0C8h
  004a9867  7e2e                  jle       short 004A9897h
  004a9869  8b16                  mov       edx,[esi]
  004a986b  33db                  xor       ebx,ebx
  004a986d  81e2fffffffe          and       edx,0FEFFFFFFh
  004a9873  66c746120200          mov       word [esi+12h],2
  004a9879  83ca02                or        edx,2
  004a987c  66895e14              mov       [esi+14h],bx
  004a9880  66c746180300          mov       word [esi+18h],3
  004a9886  66c7461a0800          mov       word [esi+1Ah],8
  004a988c  8916                  mov       [esi],edx
  004a988e  c7465c00f00000        mov       dword [esi+5Ch],0F000h
  004a9895  eb02                  jmp       short 004A9899h

LAB_004a9897:
  004a9897  33db                  xor       ebx,ebx

LAB_004a9899:
  004a9899  8d8100010000          lea       eax,[ecx+100h]
  004a989f  25ff0f0000            and       eax,0FFFh
  004a98a4  3d00020000            cmp       eax,200h
  004a98a9  7e2c                  jle       short 004A98D7h
  004a98ab  8bd1                  mov       edx,ecx
  004a98ad  81e2ff0f0000          and       edx,0FFFh
  004a98b3  81fa00080000          cmp       edx,800h
  004a98b9  7d0a                  jge       short 004A98C5h
  004a98bb  8d443f40              lea       eax,[edi+edi+40h]
  004a98bf  66014632              add       [esi+32h],ax
  004a98c3  eb12                  jmp       short 004A98D7h

LAB_004a98c5:
  004a98c5  8d143f                lea       edx,[edi+edi]
  004a98c8  b8c0ffffff            mov       eax,0FFFFFFC0h
  004a98cd  2bc2                  sub       eax,edx
  004a98cf  66014632              add       [esi+32h],ax
  004a98d3  eb02                  jmp       short 004A98D7h

LAB_004a98d5:
  004a98d5  33db                  xor       ebx,ebx

LAB_004a98d7:
  004a98d7  668b5612              mov       dx,[esi+12h]
  004a98db  6683fa02              cmp       dx,2
  004a98df  0f8589000000          jne       near 004A996Eh
  004a98e5  8b6c2420              mov       ebp,[esp+20h]
  004a98e9  66395616              cmp       [esi+16h],dx
  004a98ed  8b4620                mov       eax,[esi+20h]
  004a98f0  8b6d20                mov       ebp,[ebp+20h]
  004a98f3  740a                  je        short 004A98FFh
  004a98f5  2be8                  sub       ebp,eax
  004a98f7  81c500800200          add       ebp,28000h
  004a98fd  eb08                  jmp       short 004A9907h

LAB_004a98ff:
  004a98ff  2be8                  sub       ebp,eax
  004a9901  81ed00000a00          sub       ebp,0A0000h

LAB_004a9907:
  004a9907  d1fd                  sar       ebp,1
  004a9909  03e8                  add       ebp,eax
  004a990b  66ff4614              inc       word [esi+14h]
  004a990f  8d8100010000          lea       eax,[ecx+100h]
  004a9915  896e20                mov       [esi+20h],ebp
  004a9918  25ff0f0000            and       eax,0FFFh
  004a991d  3d00020000            cmp       eax,200h
  004a9922  7e22                  jle       short 004A9946h
  004a9924  81e1ff0f0000          and       ecx,0FFFh
  004a992a  81f900080000          cmp       ecx,800h
  004a9930  7d06                  jge       short 004A9938h
  004a9932  8d443f40              lea       eax,[edi+edi+40h]
  004a9936  eb0a                  jmp       short 004A9942h

LAB_004a9938:
  004a9938  8d0c3f                lea       ecx,[edi+edi]
  004a993b  b8c0ffffff            mov       eax,0FFFFFFC0h
  004a9940  2bc1                  sub       eax,ecx

LAB_004a9942:
  004a9942  66014632              add       [esi+32h],ax

LAB_004a9946:
  004a9946  8b465c                mov       eax,[esi+5Ch]
  004a9949  8bc8                  mov       ecx,eax
  004a994b  81e100f0ffff          and       ecx,0FFFFF000h
  004a9951  81f900a00000          cmp       ecx,0A000h
  004a9957  7d0b                  jge       short 004A9964h
  004a9959  8b0e                  mov       ecx,[esi]
  004a995b  c6464300              mov       byte [esi+43h],0
  004a995f  83e1fd                and       ecx,0FFFFFFFDh
  004a9962  890e                  mov       [esi],ecx

LAB_004a9964:
  004a9964  8bc8                  mov       ecx,eax
  004a9966  c1f905                sar       ecx,5
  004a9969  2bc1                  sub       eax,ecx
  004a996b  89465c                mov       [esi+5Ch],eax

LAB_004a996e:
  004a996e  663bd3                cmp       dx,bx
  004a9971  0f85fa000000          jne       near 004A9A71h
  004a9977  8b06                  mov       eax,[esi]
  004a9979  66c746120100          mov       word [esi+12h],1
  004a997f  0d00000001            or        eax,1000000h
  004a9984  66895e18              mov       [esi+18h],bx
  004a9988  66895e1a              mov       [esi+1Ah],bx
  004a998c  895e5c                mov       [esi+5Ch],ebx
  004a998f  8906                  mov       [esi],eax
  004a9991  e9db000000            jmp       004A9A71h
  004a9996  8b542420              mov       edx,[esp+20h]
  004a999a  8b4620                mov       eax,[esi+20h]
  004a999d  8b4a20                mov       ecx,[edx+20h]
  004a99a0  2bc8                  sub       ecx,eax
  004a99a2  81c100800200          add       ecx,28000h
  004a99a8  c1f902                sar       ecx,2
  004a99ab  03c8                  add       ecx,eax
  004a99ad  894e20                mov       [esi+20h],ecx
  004a99b0  668b4e12              mov       cx,[esi+12h]
  004a99b4  6683f901              cmp       cx,1
  004a99b8  7511                  jne       short 004A99CBh
  004a99ba  8b06                  mov       eax,[esi]
  004a99bc  a900000002            test      eax,2000000h
  004a99c1  7408                  je        short 004A99CBh
  004a99c3  24fd                  and       al,0FDh
  004a99c5  c6464300              mov       byte [esi+43h],0
  004a99c9  8906                  mov       [esi],eax

LAB_004a99cb:
  004a99cb  6685c9                test      cx,cx
  004a99ce  0f859d000000          jne       near 004A9A71h
  004a99d4  8b06                  mov       eax,[esi]
  004a99d6  66c746120100          mov       word [esi+12h],1
  004a99dc  25fffffffe            and       eax,0FEFFFFFFh
  004a99e1  8906                  mov       [esi],eax
  004a99e3  e989000000            jmp       004A9A71h
  004a99e8  bb01000000            mov       ebx,1
  004a99ed  66395e12              cmp       [esi+12h],bx
  004a99f1  755d                  jne       short 004A9A50h
  004a99f3  8a4643                mov       al,[esi+43h]
  004a99f6  84c0                  test      al,al
  004a99f8  7405                  je        short 004A99FFh
  004a99fa  fec8                  dec       al
  004a99fc  884643                mov       [esi+43h],al

LAB_004a99ff:
  004a99ff  0fbf5632              movsx     edx,word [esi+32h]
  004a9a03  81c200080000          add       edx,800h
  004a9a09  52                    push      edx
  004a9a0a  e80dc00900            call      00545A1Ch
  004a9a0f  8bf8                  mov       edi,eax
  004a9a11  0fbf4632              movsx     eax,word [esi+32h]
  004a9a15  0500080000            add       eax,800h
  004a9a1a  50                    push      eax
  004a9a1b  e8f6bf0900            call      00545A16h
  004a9a20  8b4c2410              mov       ecx,[esp+10h]
  004a9a24  83c160                add       ecx,60h
  004a9a27  0faf4c2414            imul      ecx,[esp+14h]
  004a9a2c  c1e108                shl       ecx,8
  004a9a2f  c1f90c                sar       ecx,0Ch
  004a9a32  8bd1                  mov       edx,ecx
  004a9a34  0fafc8                imul      ecx,eax
  004a9a37  0fafd7                imul      edx,edi
  004a9a3a  8b7d00                mov       edi,[ebp]
  004a9a3d  8b4624                mov       eax,[esi+24h]
  004a9a40  c1fa0c                sar       edx,0Ch
  004a9a43  c1f90c                sar       ecx,0Ch
  004a9a46  03fa                  add       edi,edx
  004a9a48  03c1                  add       eax,ecx
  004a9a4a  897d00                mov       [ebp],edi
  004a9a4d  894624                mov       [esi+24h],eax

LAB_004a9a50:
  004a9a50  33c0                  xor       eax,eax
  004a9a52  66394612              cmp       [esi+12h],ax
  004a9a56  7519                  jne       short 004A9A71h
  004a9a58  8b0e                  mov       ecx,[esi]
  004a9a5a  66894618              mov       [esi+18h],ax
  004a9a5e  81c900000001          or        ecx,1000000h
  004a9a64  6689461a              mov       [esi+1Ah],ax
  004a9a68  890e                  mov       [esi],ecx
  004a9a6a  66895e12              mov       [esi+12h],bx
  004a9a6e  89465c                mov       [esi+5Ch],eax

LAB_004a9a71:
  004a9a71  66837e1602            cmp       word [esi+16h],2
  004a9a76  8a4642                mov       al,[esi+42h]
  004a9a79  0f8491000000          je        near 004A9B10h
  004a9a7f  3c0a                  cmp       al,0Ah
  004a9a81  7509                  jne       short 004A9A8Ch
  004a9a83  f60602                test      byte [esi],2
  004a9a86  0f84d7000000          je        near 004A9B63h

LAB_004a9a8c:
  004a9a8c  e8afd60900            call      00547140h
  004a9a91  99                    cdq
  004a9a92  b932000000            mov       ecx,32h
  004a9a97  f7f9                  idiv      ecx
  004a9a99  83c264                add       edx,64h
  004a9a9c  66015614              add       [esi+14h],dx
  004a9aa0  668b4614              mov       ax,[esi+14h]
  004a9aa4  663d0010              cmp       ax,1000h
  004a9aa8  7c09                  jl        short 004A9AB3h
  004a9aaa  0500f0ffff            add       eax,0FFFFF000h
  004a9aaf  66894614              mov       [esi+14h],ax

LAB_004a9ab3:
  004a9ab3  8b5c2420              mov       ebx,[esp+20h]
  004a9ab7  8b4660                mov       eax,[esi+60h]
  004a9aba  8b7e20                mov       edi,[esi+20h]
  004a9abd  8b5360                mov       edx,[ebx+60h]
  004a9ac0  3bd0                  cmp       edx,eax
  004a9ac2  7e26                  jle       short 004A9AEAh
  004a9ac4  0fbf4614              movsx     eax,word [esi+14h]
  004a9ac8  50                    push      eax
  004a9ac9  e84ebf0900            call      00545A1Ch
  004a9ace  8b6b60                mov       ebp,[ebx+60h]
  004a9ad1  0500340000            add       eax,3400h
  004a9ad6  8d0c80                lea       ecx,[eax+eax*4]
  004a9ad9  c1e103                shl       ecx,3
  004a9adc  2bcf                  sub       ecx,edi
  004a9ade  03cd                  add       ecx,ebp
  004a9ae0  c1f903                sar       ecx,3
  004a9ae3  03cf                  add       ecx,edi
  004a9ae5  894e20                mov       [esi+20h],ecx
  004a9ae8  eb79                  jmp       short 004A9B63h

LAB_004a9aea:
  004a9aea  0fbf5614              movsx     edx,word [esi+14h]
  004a9aee  52                    push      edx
  004a9aef  e828bf0900            call      00545A1Ch
  004a9af4  8b5e60                mov       ebx,[esi+60h]
  004a9af7  0500340000            add       eax,3400h
  004a9afc  8d0480                lea       eax,[eax+eax*4]
  004a9aff  c1e003                shl       eax,3
  004a9b02  2bc7                  sub       eax,edi
  004a9b04  03c3                  add       eax,ebx
  004a9b06  c1f803                sar       eax,3
  004a9b09  03c7                  add       eax,edi
  004a9b0b  894620                mov       [esi+20h],eax
  004a9b0e  eb53                  jmp       short 004A9B63h

LAB_004a9b10:
  004a9b10  3c0a                  cmp       al,0Ah
  004a9b12  7505                  jne       short 004A9B19h
  004a9b14  f60602                test      byte [esi],2
  004a9b17  744a                  je        short 004A9B63h

LAB_004a9b19:
  004a9b19  e822d60900            call      00547140h
  004a9b1e  99                    cdq
  004a9b1f  b932000000            mov       ecx,32h
  004a9b24  f7f9                  idiv      ecx
  004a9b26  83c264                add       edx,64h
  004a9b29  66015614              add       [esi+14h],dx
  004a9b2d  668b4614              mov       ax,[esi+14h]
  004a9b31  663d0010              cmp       ax,1000h
  004a9b35  7c09                  jl        short 004A9B40h
  004a9b37  0500f0ffff            add       eax,0FFFFF000h
  004a9b3c  66894614              mov       [esi+14h],ax

LAB_004a9b40:
  004a9b40  0fbf5614              movsx     edx,word [esi+14h]
  004a9b44  8b7e20                mov       edi,[esi+20h]
  004a9b47  52                    push      edx
  004a9b48  e8cfbe0900            call      00545A1Ch
  004a9b4d  8b0d00a65f00          mov       ecx,[5FA600h]
  004a9b53  8d0480                lea       eax,[eax+eax*4]
  004a9b56  8d14c1                lea       edx,[ecx+eax*8]
  004a9b59  2bd7                  sub       edx,edi
  004a9b5b  c1fa03                sar       edx,3
  004a9b5e  03d7                  add       edx,edi
  004a9b60  895620                mov       [esi+20h],edx

LAB_004a9b63:
  004a9b63  0fbf4632              movsx     eax,word [esi+32h]
  004a9b67  50                    push      eax
  004a9b68  e8afbe0900            call      00545A1Ch
  004a9b6d  0fbf4e32              movsx     ecx,word [esi+32h]
  004a9b71  51                    push      ecx
  004a9b72  89842480000000        mov       [esp+80h],eax
  004a9b79  e898be0900            call      00545A16h
  004a9b7e  8b5650                mov       edx,[esi+50h]
  004a9b81  89842484000000        mov       [esp+84h],eax
  004a9b88  8bc2                  mov       eax,edx
  004a9b8a  8b4c2414              mov       ecx,[esp+14h]
  004a9b8e  c1f803                sar       eax,3
  004a9b91  0faf4e5c              imul      ecx,[esi+5Ch]
  004a9b95  8bfa                  mov       edi,edx
  004a9b97  56                    push      esi
  004a9b98  2bf8                  sub       edi,eax
  004a9b9a  c1fa08                sar       edx,8
  004a9b9d  2bfa                  sub       edi,edx
  004a9b9f  8b5654                mov       edx,[esi+54h]
  004a9ba2  8bc2                  mov       eax,edx
  004a9ba4  8bea                  mov       ebp,edx
  004a9ba6  c1f803                sar       eax,3
  004a9ba9  2be8                  sub       ebp,eax
  004a9bab  897e50                mov       [esi+50h],edi
  004a9bae  c1fa08                sar       edx,8
  004a9bb1  2bea                  sub       ebp,edx
  004a9bb3  8b5658                mov       edx,[esi+58h]
  004a9bb6  8bda                  mov       ebx,edx
  004a9bb8  8bc2                  mov       eax,edx
  004a9bba  c1fb08                sar       ebx,8
  004a9bbd  2bc3                  sub       eax,ebx
  004a9bbf  8b5e20                mov       ebx,[esi+20h]
  004a9bc2  c1fa03                sar       edx,3
  004a9bc5  c1f90c                sar       ecx,0Ch
  004a9bc8  2bc2                  sub       eax,edx
  004a9bca  8bd1                  mov       edx,ecx
  004a9bcc  0faf942480000000      imul      edx,[esp+80h]
  004a9bd4  0faf8c2488000000      imul      ecx,[esp+88h]
  004a9bdc  c1fa0c                sar       edx,0Ch
  004a9bdf  c1f90c                sar       ecx,0Ch
  004a9be2  896e54                mov       [esi+54h],ebp
  004a9be5  03d7                  add       edx,edi
  004a9be7  8b7e1c                mov       edi,[esi+1Ch]
  004a9bea  03dd                  add       ebx,ebp
  004a9bec  8b6e24                mov       ebp,[esi+24h]
  004a9bef  03c8                  add       ecx,eax
  004a9bf1  03fa                  add       edi,edx
  004a9bf3  03e9                  add       ebp,ecx
  004a9bf5  894658                mov       [esi+58h],eax
  004a9bf8  897e1c                mov       [esi+1Ch],edi
  004a9bfb  895e20                mov       [esi+20h],ebx
  004a9bfe  896e24                mov       [esi+24h],ebp
  004a9c01  e83a9d0800            call      00533940h
  004a9c06  8b9690000000          mov       edx,[esi+90h]
  004a9c0c  83c404                add       esp,4
  004a9c0f  80e6f8                and       dh,0F8h
  004a9c12  66837e1602            cmp       word [esi+16h],2
  004a9c17  899690000000          mov       [esi+90h],edx
  004a9c1d  8bc2                  mov       eax,edx
  004a9c1f  bf82a80020            mov       edi,2000A882h
  004a9c24  7510                  jne       short 004A9C36h
  004a9c26  25ffcfffdf            and       eax,0DFFFCFFFh
  004a9c2b  bf82880000            mov       edi,8882h
  004a9c30  898690000000          mov       [esi+90h],eax

LAB_004a9c36:
  004a9c36  8b4660                mov       eax,[esi+60h]
  004a9c39  8b4e20                mov       ecx,[esi+20h]
  004a9c3c  3bc8                  cmp       ecx,eax
  004a9c3e  7f1b                  jg        short 004A9C5Bh
  004a9c40  8b8e90000000          mov       ecx,[esi+90h]
  004a9c46  f6c580                test      ch,80h
  004a9c49  7510                  jne       short 004A9C5Bh
  004a9c4b  894620                mov       [esi+20h],eax
  004a9c4e  8b06                  mov       eax,[esi]
  004a9c50  24fe                  and       al,0FEh
  004a9c52  33db                  xor       ebx,ebx
  004a9c54  8906                  mov       [esi],eax
  004a9c56  895e54                mov       [esi+54h],ebx
  004a9c59  eb02                  jmp       short 004A9C5Dh

LAB_004a9c5b:
  004a9c5b  33db                  xor       ebx,ebx

LAB_004a9c5d:
  004a9c5d  85be90000000          test      [esi+90h],edi
  004a9c63  0f84bc000000          je        near 004A9D25h
  004a9c69  8a4e42                mov       cl,[esi+42h]
  004a9c6c  b007                  mov       al,7
  004a9c6e  3ac8                  cmp       cl,al
  004a9c70  7420                  je        short 004A9C92h
  004a9c72  884642                mov       [esi+42h],al
  004a9c75  e8c6d40900            call      00547140h
  004a9c7a  99                    cdq
  004a9c7b  b928000000            mov       ecx,28h
  004a9c80  66895e12              mov       [esi+12h],bx
  004a9c84  f7f9                  idiv      ecx
  004a9c86  66c746105000          mov       word [esi+10h],50h
  004a9c8c  80c214                add       dl,14h
  004a9c8f  885643                mov       [esi+43h],dl

LAB_004a9c92:
  004a9c92  8b16                  mov       edx,[esi]
  004a9c94  8b442474              mov       eax,[esp+74h]
  004a9c98  8b4c2468              mov       ecx,[esp+68h]
  004a9c9c  83e2fd                and       edx,0FFFFFFFDh
  004a9c9f  894624                mov       [esi+24h],eax
  004a9ca2  8b442448              mov       eax,[esp+48h]
  004a9ca6  8916                  mov       [esi],edx
  004a9ca8  8b54246c              mov       edx,[esp+6Ch]
  004a9cac  25ffff0000            and       eax,0FFFFh
  004a9cb1  89561c                mov       [esi+1Ch],edx
  004a9cb4  8b542450              mov       edx,[esp+50h]
  004a9cb8  894e7c                mov       [esi+7Ch],ecx
  004a9cbb  8b4c2460              mov       ecx,[esp+60h]
  004a9cbf  898690000000          mov       [esi+90h],eax
  004a9cc5  8b442454              mov       eax,[esp+54h]
  004a9cc9  899680000000          mov       [esi+80h],edx
  004a9ccf  8b542458              mov       edx,[esp+58h]
  004a9cd3  898684000000          mov       [esi+84h],eax
  004a9cd9  8b442464              mov       eax,[esp+64h]
  004a9cdd  898e88000000          mov       [esi+88h],ecx
  004a9ce3  8b4c244c              mov       ecx,[esp+4Ch]
  004a9ce7  57                    push      edi
  004a9ce8  56                    push      esi
  004a9ce9  89968c000000          mov       [esi+8Ch],edx
  004a9cef  894664                mov       [esi+64h],eax
  004a9cf2  894e60                mov       [esi+60h],ecx
  004a9cf5  e866c10200            call      004D5E60h
  004a9cfa  83c408                add       esp,8
  004a9cfd  663bc3                cmp       ax,bx
  004a9d00  7515                  jne       short 004A9D17h
  004a9d02  e839d40900            call      00547140h
  004a9d07  99                    cdq
  004a9d08  33c2                  xor       eax,edx
  004a9d0a  2bc2                  sub       eax,edx
  004a9d0c  83e001                and       eax,1
  004a9d0f  33c2                  xor       eax,edx
  004a9d11  2bc2                  sub       eax,edx
  004a9d13  8d4400ff              lea       eax,[eax+eax-1]

LAB_004a9d17:
  004a9d17  8b542410              mov       edx,[esp+10h]
  004a9d1b  83c240                add       edx,40h
  004a9d1e  0fafd0                imul      edx,eax
  004a9d21  66015632              add       [esi+32h],dx

LAB_004a9d25:
  004a9d25  6aff                  push      0FFFFFFFFh
  004a9d27  6aff                  push      0FFFFFFFFh
  004a9d29  6aff                  push      0FFFFFFFFh
  004a9d2b  56                    push      esi
  004a9d2c  e8df9a0100            call      004C3810h
  004a9d31  8b442424              mov       eax,[esp+24h]
  004a9d35  83c410                add       esp,10h
  004a9d38  3bc3                  cmp       eax,ebx
  004a9d3a  7517                  jne       short 004A9D53h
  004a9d3c  668b842492000000      mov       ax,[esp+92h]
  004a9d44  66894632              mov       [esi+32h],ax
  004a9d48  5f                    pop       edi
  004a9d49  5e                    pop       esi
  004a9d4a  5d                    pop       ebp
  004a9d4b  5b                    pop       ebx
  004a9d4c  81c494000000          add       esp,94h
  004a9d52  c3                    ret

LAB_004a9d53:
  004a9d53  385e43                cmp       [esi+43h],bl
  004a9d56  0f859a020000          jne       near 004A9FF6h
  004a9d5c  8b466c                mov       eax,[esi+6Ch]
  004a9d5f  8b4e1c                mov       ecx,[esi+1Ch]
  004a9d62  8b5624                mov       edx,[esi+24h]
  004a9d65  8b781c                mov       edi,[eax+1Ch]
  004a9d68  8b6824                mov       ebp,[eax+24h]
  004a9d6b  2bf9                  sub       edi,ecx
  004a9d6d  2bea                  sub       ebp,edx
  004a9d6f  c1ff0c                sar       edi,0Ch
  004a9d72  c1fd0c                sar       ebp,0Ch
  004a9d75  8bcf                  mov       ecx,edi
  004a9d77  8bd5                  mov       edx,ebp
  004a9d79  0fafcf                imul      ecx,edi
  004a9d7c  0fafd5                imul      edx,ebp
  004a9d7f  03ca                  add       ecx,edx
  004a9d81  51                    push      ecx
  004a9d82  e889bc0900            call      00545A10h
  004a9d87  55                    push      ebp
  004a9d88  57                    push      edi
  004a9d89  89442424              mov       [esp+24h],eax
  004a9d8d  e890bc0900            call      00545A22h
  004a9d92  668b4e32              mov       cx,[esi+32h]
  004a9d96  66895e12              mov       [esi+12h],bx
  004a9d9a  2bc1                  sub       eax,ecx
  004a9d9c  25ff0f0000            and       eax,0FFFh
  004a9da1  89442418              mov       [esp+18h],eax
  004a9da5  8b44241c              mov       eax,[esp+1Ch]
  004a9da9  3dfa000000            cmp       eax,0FAh
  004a9dae  0f8db7000000          jge       near 004A9E6Bh
  004a9db4  e887d30900            call      00547140h
  004a9db9  99                    cdq
  004a9dba  b964000000            mov       ecx,64h
  004a9dbf  f7f9                  idiv      ecx
  004a9dc1  8bea                  mov       ebp,edx
  004a9dc3  83fd32                cmp       ebp,32h
  004a9dc6  0f8d82000000          jge       near 004A9E4Eh
  004a9dcc  8b542418              mov       edx,[esp+18h]
  004a9dd0  8b7c2424              mov       edi,[esp+24h]
  004a9dd4  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a9ddb  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a9de2  25ff0f0000            and       eax,0FFFh
  004a9de7  3bc1                  cmp       eax,ecx
  004a9de9  7f59                  jg        short 004A9E44h
  004a9deb  e850d30900            call      00547140h
  004a9df0  99                    cdq
  004a9df1  8d4c3f01              lea       ecx,[edi+edi+1]
  004a9df5  f7f9                  idiv      ecx
  004a9df7  8b44241c              mov       eax,[esp+1Ch]
  004a9dfb  83c264                add       edx,64h
  004a9dfe  3bc2                  cmp       eax,edx
  004a9e00  7d42                  jge       short 004A9E44h
  004a9e02  83fd1e                cmp       ebp,1Eh
  004a9e05  7d24                  jge       short 004A9E2Bh
  004a9e07  8b16                  mov       edx,[esi]
  004a9e09  c646420b              mov       byte [esi+42h],0Bh
  004a9e0d  81e2fffffffe          and       edx,0FEFFFFFFh
  004a9e13  c6464301              mov       byte [esi+43h],1
  004a9e17  83ca02                or        edx,2
  004a9e1a  895e5c                mov       [esi+5Ch],ebx
  004a9e1d  8916                  mov       [esi],edx
  004a9e1f  66c746180300          mov       word [esi+18h],3
  004a9e25  66895e1a              mov       [esi+1Ah],bx
  004a9e29  eb44                  jmp       short 004A9E6Fh

LAB_004a9e2b:
  004a9e2b  c646420e              mov       byte [esi+42h],0Eh
  004a9e2f  e80cd30900            call      00547140h
  004a9e34  99                    cdq
  004a9e35  b928000000            mov       ecx,28h
  004a9e3a  f7f9                  idiv      ecx
  004a9e3c  80c21e                add       dl,1Eh
  004a9e3f  885643                mov       [esi+43h],dl
  004a9e42  eb2b                  jmp       short 004A9E6Fh

LAB_004a9e44:
  004a9e44  c6464201              mov       byte [esi+42h],1
  004a9e48  c6464301              mov       byte [esi+43h],1
  004a9e4c  eb21                  jmp       short 004A9E6Fh

LAB_004a9e4e:
  004a9e4e  83fd4b                cmp       ebp,4Bh
  004a9e51  0f8daa010000          jge       near 004AA001h
  004a9e57  c646420e              mov       byte [esi+42h],0Eh
  004a9e5b  e8e0d20900            call      00547140h
  004a9e60  99                    cdq
  004a9e61  b928000000            mov       ecx,28h
  004a9e66  f7f9                  idiv      ecx
  004a9e68  885643                mov       [esi+43h],dl

LAB_004a9e6b:
  004a9e6b  8b7c2424              mov       edi,[esp+24h]

LAB_004a9e6f:
  004a9e6f  8b44241c              mov       eax,[esp+1Ch]
  004a9e73  3d58020000            cmp       eax,258h
  004a9e78  0f8d94000000          jge       near 004A9F12h
  004a9e7e  3dfa000000            cmp       eax,0FAh
  004a9e83  0f8c89000000          jl        near 004A9F12h
  004a9e89  e8b2d20900            call      00547140h
  004a9e8e  99                    cdq
  004a9e8f  b964000000            mov       ecx,64h
  004a9e94  f7f9                  idiv      ecx
  004a9e96  83fa14                cmp       edx,14h
  004a9e99  7d22                  jge       short 004A9EBDh
  004a9e9b  8b542418              mov       edx,[esp+18h]
  004a9e9f  81c200040000          add       edx,400h
  004a9ea5  81e2ff0f0000          and       edx,0FFFh
  004a9eab  81fa00080000          cmp       edx,800h
  004a9eb1  7c53                  jl        short 004A9F06h
  004a9eb3  c6464201              mov       byte [esi+42h],1
  004a9eb7  c6464301              mov       byte [esi+43h],1
  004a9ebb  eb55                  jmp       short 004A9F12h

LAB_004a9ebd:
  004a9ebd  83fa28                cmp       edx,28h
  004a9ec0  7c0a                  jl        short 004A9ECCh
  004a9ec2  83fa3c                cmp       edx,3Ch
  004a9ec5  7c3f                  jl        short 004A9F06h
  004a9ec7  83fa50                cmp       edx,50h
  004a9eca  7d3a                  jge       short 004A9F06h

LAB_004a9ecc:
  004a9ecc  c6464203              mov       byte [esi+42h],3
  004a9ed0  66895e12              mov       [esi+12h],bx
  004a9ed4  e867d20900            call      00547140h
  004a9ed9  99                    cdq
  004a9eda  b90a000000            mov       ecx,0Ah
  004a9edf  f7f9                  idiv      ecx
  004a9ee1  8bc7                  mov       eax,edi
  004a9ee3  d1f8                  sar       eax,1
  004a9ee5  02d0                  add       dl,al
  004a9ee7  80c214                add       dl,14h
  004a9eea  885643                mov       [esi+43h],dl
  004a9eed  e84ed20900            call      00547140h
  004a9ef2  99                    cdq
  004a9ef3  33c2                  xor       eax,edx
  004a9ef5  2bc2                  sub       eax,edx
  004a9ef7  25ff0f0000            and       eax,0FFFh
  004a9efc  33c2                  xor       eax,edx
  004a9efe  2bc2                  sub       eax,edx
  004a9f00  66894610              mov       [esi+10h],ax
  004a9f04  eb0c                  jmp       short 004A9F12h

LAB_004a9f06:
  004a9f06  c646420a              mov       byte [esi+42h],0Ah
  004a9f0a  c6464301              mov       byte [esi+43h],1
  004a9f0e  66895e12              mov       [esi+12h],bx

LAB_004a9f12:
  004a9f12  817c241c58020000      cmp       dword [esp+1Ch],258h
  004a9f1a  0f8cd6000000          jl        near 004A9FF6h
  004a9f20  e81bd20900            call      00547140h
  004a9f25  99                    cdq
  004a9f26  b964000000            mov       ecx,64h
  004a9f2b  f7f9                  idiv      ecx
  004a9f2d  83fa32                cmp       edx,32h
  004a9f30  7d6f                  jge       short 004A9FA1h
  004a9f32  8b442410              mov       eax,[esp+10h]
  004a9f36  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004a9f3a  3bd1                  cmp       edx,ecx
  004a9f3c  7d2e                  jge       short 004A9F6Ch
  004a9f3e  c6464202              mov       byte [esi+42h],2
  004a9f42  e8f9d10900            call      00547140h
  004a9f47  99                    cdq
  004a9f48  b90a000000            mov       ecx,0Ah
  004a9f4d  66c746106400          mov       word [esi+10h],64h
  004a9f53  f7f9                  idiv      ecx
  004a9f55  8bc7                  mov       eax,edi
  004a9f57  d1f8                  sar       eax,1
  004a9f59  02d0                  add       dl,al
  004a9f5b  80c214                add       dl,14h
  004a9f5e  885643                mov       [esi+43h],dl
  004a9f61  5f                    pop       edi
  004a9f62  5e                    pop       esi
  004a9f63  5d                    pop       ebp
  004a9f64  5b                    pop       ebx
  004a9f65  81c494000000          add       esp,94h
  004a9f6b  c3                    ret

LAB_004a9f6c:
  004a9f6c  885e42                mov       [esi+42h],bl
  004a9f6f  e8ccd10900            call      00547140h
  004a9f74  99                    cdq
  004a9f75  b90a000000            mov       ecx,0Ah
  004a9f7a  66895e12              mov       [esi+12h],bx
  004a9f7e  f7f9                  idiv      ecx
  004a9f80  8b4c2410              mov       ecx,[esp+10h]
  004a9f84  8bc7                  mov       eax,edi
  004a9f86  c1f802                sar       eax,2
  004a9f89  02d0                  add       dl,al
  004a9f8b  885643                mov       [esi+43h],dl
  004a9f8e  8d540950              lea       edx,[ecx+ecx+50h]
  004a9f92  66895610              mov       [esi+10h],dx
  004a9f96  5f                    pop       edi
  004a9f97  5e                    pop       esi
  004a9f98  5d                    pop       ebp
  004a9f99  5b                    pop       ebx
  004a9f9a  81c494000000          add       esp,94h
  004a9fa0  c3                    ret

LAB_004a9fa1:
  004a9fa1  83fa4b                cmp       edx,4Bh
  004a9fa4  7d43                  jge       short 004A9FE9h
  004a9fa6  c6464203              mov       byte [esi+42h],3
  004a9faa  66895e12              mov       [esi+12h],bx
  004a9fae  e88dd10900            call      00547140h
  004a9fb3  99                    cdq
  004a9fb4  b90a000000            mov       ecx,0Ah
  004a9fb9  f7f9                  idiv      ecx
  004a9fbb  8bc7                  mov       eax,edi
  004a9fbd  d1f8                  sar       eax,1
  004a9fbf  02d0                  add       dl,al
  004a9fc1  80c214                add       dl,14h
  004a9fc4  885643                mov       [esi+43h],dl
  004a9fc7  e874d10900            call      00547140h
  004a9fcc  99                    cdq
  004a9fcd  33c2                  xor       eax,edx
  004a9fcf  2bc2                  sub       eax,edx
  004a9fd1  25ff0f0000            and       eax,0FFFh
  004a9fd6  33c2                  xor       eax,edx
  004a9fd8  2bc2                  sub       eax,edx
  004a9fda  66894610              mov       [esi+10h],ax
  004a9fde  5f                    pop       edi
  004a9fdf  5e                    pop       esi
  004a9fe0  5d                    pop       ebp
  004a9fe1  5b                    pop       ebx
  004a9fe2  81c494000000          add       esp,94h
  004a9fe8  c3                    ret

LAB_004a9fe9:
  004a9fe9  83fa3c                cmp       edx,3Ch
  004a9fec  7d08                  jge       short 004A9FF6h
  004a9fee  c6464201              mov       byte [esi+42h],1
  004a9ff2  c6464301              mov       byte [esi+43h],1

LAB_004a9ff6:
  004a9ff6  5f                    pop       edi
  004a9ff7  5e                    pop       esi
  004a9ff8  5d                    pop       ebp
  004a9ff9  5b                    pop       ebx
  004a9ffa  81c494000000          add       esp,94h
  004aa000  c3                    ret

LAB_004aa001:
  004aa001  c6464202              mov       byte [esi+42h],2
  004aa005  e836d10900            call      00547140h
  004aa00a  99                    cdq
  004aa00b  b90a000000            mov       ecx,0Ah
  004aa010  8b7c2424              mov       edi,[esp+24h]
  004aa014  f7f9                  idiv      ecx
  004aa016  8bc7                  mov       eax,edi
  004aa018  66c746105000          mov       word [esi+10h],50h
  004aa01e  d1f8                  sar       eax,1
  004aa020  02d0                  add       dl,al
  004aa022  80c228                add       dl,28h
  004aa025  885643                mov       [esi+43h],dl
  004aa028  e942feffff            jmp       004A9E6Fh
  004aa02d  90                    nop
  004aa02e  8bff                  mov       edi,edi
  004aa030  8f954a00              (bad)
  (octet invalide - la plage demandee deborde probablement du code)
