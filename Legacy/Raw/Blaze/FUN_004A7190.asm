Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a7190  section .text  offset fichier 0xa6590  4032 octets

  004a7190  81ec88000000          sub       esp,88h
  004a7196  53                    push      ebx
  004a7197  55                    push      ebp
  004a7198  56                    push      esi
  004a7199  8bb42498000000        mov       esi,[esp+98h]
  004a71a0  33ed                  xor       ebp,ebp
  004a71a2  57                    push      edi
  004a71a3  8a4641                mov       al,[esi+41h]
  004a71a6  66896c242c            mov       [esp+2Ch],bp
  004a71ab  84c0                  test      al,al
  004a71ad  66c744242e1400        mov       word [esp+2Eh],14h
  004a71b4  66c74424303200        mov       word [esp+30h],32h
  004a71bb  66c74424326400        mov       word [esp+32h],64h
  004a71c2  66896c2434            mov       [esp+34h],bp
  004a71c7  66c74424365000        mov       word [esp+36h],50h
  004a71ce  7533                  jne       short 004A7203h
  004a71d0  55                    push      ebp
  004a71d1  6a03                  push      3
  004a71d3  56                    push      esi
  004a71d4  c6464101              mov       byte [esi+41h],1
  004a71d8  e8730f0000            call      004A8150h
  004a71dd  8b4620                mov       eax,[esi+20h]
  004a71e0  83c40c                add       esp,0Ch
  004a71e3  894648                mov       [esi+48h],eax
  004a71e6  668b4616              mov       ax,[esi+16h]
  004a71ea  663d0a00              cmp       ax,0Ah
  004a71ee  7c0d                  jl        short 004A71FDh
  004a71f0  83c0f6                add       eax,0FFFFFFF6h
  004a71f3  66894616              mov       [esi+16h],ax
  004a71f7  8b06                  mov       eax,[esi]
  004a71f9  24df                  and       al,0DFh
  004a71fb  eb04                  jmp       short 004A7201h

LAB_004a71fd:
  004a71fd  8b06                  mov       eax,[esi]
  004a71ff  0c20                  or        al,20h

LAB_004a7201:
  004a7201  8906                  mov       [esi],eax

LAB_004a7203:
  004a7203  8b16                  mov       edx,[esi]
  004a7205  8b4e1c                mov       ecx,[esi+1Ch]
  004a7208  8b4664                mov       eax,[esi+64h]
  004a720b  8d7e1c                lea       edi,[esi+1Ch]
  004a720e  83e2fe                and       edx,0FFFFFFFEh
  004a7211  894c245c              mov       [esp+5Ch],ecx
  004a7215  8b4e60                mov       ecx,[esi+60h]
  004a7218  8916                  mov       [esi],edx
  004a721a  8b5624                mov       edx,[esi+24h]
  004a721d  8944243c              mov       [esp+3Ch],eax
  004a7221  8b467c                mov       eax,[esi+7Ch]
  004a7224  89542464              mov       [esp+64h],edx
  004a7228  668b5632              mov       dx,[esi+32h]
  004a722c  894c2440              mov       [esp+40h],ecx
  004a7230  668b8e90000000        mov       cx,[esi+90h]
  004a7237  6689942486000000      mov       [esp+86h],dx
  004a723f  8b9680000000          mov       edx,[esi+80h]
  004a7245  8b5e6c                mov       ebx,[esi+6Ch]
  004a7248  89442458              mov       [esp+58h],eax
  004a724c  8b8684000000          mov       eax,[esi+84h]
  004a7252  66894c2448            mov       [esp+48h],cx
  004a7257  8b8e88000000          mov       ecx,[esi+88h]
  004a725d  89542438              mov       [esp+38h],edx
  004a7261  8b968c000000          mov       edx,[esi+8Ch]
  004a7267  8944244c              mov       [esp+4Ch],eax
  004a726b  894c2454              mov       [esp+54h],ecx
  004a726f  8d442428              lea       eax,[esp+28h]
  004a7273  89542450              mov       [esp+50h],edx
  004a7277  8d4c2468              lea       ecx,[esp+68h]
  004a727b  50                    push      eax
  004a727c  8d542470              lea       edx,[esp+70h]
  004a7280  51                    push      ecx
  004a7281  52                    push      edx
  004a7282  56                    push      esi
  004a7283  895c2428              mov       [esp+28h],ebx
  004a7287  e8f4160900            call      00538980h
  004a728c  83c410                add       esp,10h
  004a728f  8d442444              lea       eax,[esp+44h]
  004a7293  8d4c2424              lea       ecx,[esp+24h]
  004a7297  8d54241c              lea       edx,[esp+1Ch]
  004a729b  50                    push      eax
  004a729c  51                    push      ecx
  004a729d  8d44241c              lea       eax,[esp+1Ch]
  004a72a1  52                    push      edx
  004a72a2  8d4c241c              lea       ecx,[esp+1Ch]
  004a72a6  50                    push      eax
  004a72a7  51                    push      ecx
  004a72a8  56                    push      esi
  004a72a9  e872ec0800            call      00535F20h
  004a72ae  8b4c2428              mov       ecx,[esp+28h]
  004a72b2  8b54245c              mov       edx,[esp+5Ch]
  004a72b6  83c418                add       esp,18h
  004a72b9  b840000000            mov       eax,40h
  004a72be  2bc1                  sub       eax,ecx
  004a72c0  52                    push      edx
  004a72c1  56                    push      esi
  004a72c2  89442428              mov       [esp+28h],eax
  004a72c6  e815eb0200            call      004D5DE0h
  004a72cb  8b07                  mov       eax,[edi]
  004a72cd  8b531c                mov       edx,[ebx+1Ch]
  004a72d0  8b4e24                mov       ecx,[esi+24h]
  004a72d3  2bc2                  sub       eax,edx
  004a72d5  8b5324                mov       edx,[ebx+24h]
  004a72d8  83c408                add       esp,8
  004a72db  c1f80c                sar       eax,0Ch
  004a72de  2bca                  sub       ecx,edx
  004a72e0  8bd0                  mov       edx,eax
  004a72e2  c1f90c                sar       ecx,0Ch
  004a72e5  0fafd0                imul      edx,eax
  004a72e8  8bc1                  mov       eax,ecx
  004a72ea  0fafc1                imul      eax,ecx
  004a72ed  03d0                  add       edx,eax
  004a72ef  52                    push      edx
  004a72f0  e81be70900            call      00545A10h
  004a72f5  89442414              mov       [esp+14h],eax
  004a72f9  8b442424              mov       eax,[esp+24h]
  004a72fd  3bc5                  cmp       eax,ebp
  004a72ff  0f84aa080000          je        near 004A7BAFh
  004a7305  8b1e                  mov       ebx,[esi]
  004a7307  33c9                  xor       ecx,ecx
  004a7309  8a4e04                mov       cl,[esi+4]
  004a730c  81e3fffffffd          and       ebx,0FDFFFFFFh
  004a7312  0fbf5618              movsx     edx,word [esi+18h]
  004a7316  891e                  mov       [esi],ebx
  004a7318  8b0c8d40965e00        mov       ecx,[ecx*4+5E9640h]
  004a731f  8bc3                  mov       eax,ebx
  004a7321  8b0c91                mov       ecx,[ecx+edx*4]
  004a7324  a900000001            test      eax,1000000h
  004a7329  741b                  je        short 004A7346h
  004a732b  66ff461a              inc       word [esi+1Ah]
  004a732f  668b461a              mov       ax,[esi+1Ah]
  004a7333  33d2                  xor       edx,edx
  004a7335  668b5116              mov       dx,[ecx+16h]
  004a7339  0fbfc0                movsx     eax,ax
  004a733c  3bc2                  cmp       eax,edx
  004a733e  7c27                  jl        short 004A7367h
  004a7340  66896e1a              mov       [esi+1Ah],bp
  004a7344  eb21                  jmp       short 004A7367h

LAB_004a7346:
  004a7346  66ff461a              inc       word [esi+1Ah]
  004a734a  668b561a              mov       dx,[esi+1Ah]
  004a734e  33db                  xor       ebx,ebx
  004a7350  668b5916              mov       bx,[ecx+16h]
  004a7354  0fbfca                movsx     ecx,dx
  004a7357  3bcb                  cmp       ecx,ebx
  004a7359  7c0c                  jl        short 004A7367h
  004a735b  4a                    dec       edx
  004a735c  0d00000002            or        eax,2000000h
  004a7361  6689561a              mov       [esi+1Ah],dx
  004a7365  8906                  mov       [esi],eax

LAB_004a7367:
  004a7367  f60602                test      byte [esi],2
  004a736a  0f8401010000          je        near 004A7471h
  004a7370  807e420b              cmp       byte [esi+42h],0Bh
  004a7374  7479                  je        short 004A73EFh
  004a7376  bb11000000            mov       ebx,11h
  004a737b  66395e1a              cmp       [esi+1Ah],bx
  004a737f  7555                  jne       short 004A73D6h
  004a7381  8d54242c              lea       edx,[esp+2Ch]
  004a7385  6a00                  push      0
  004a7387  52                    push      edx
  004a7388  57                    push      edi
  004a7389  56                    push      esi
  004a738a  e8a1280700            call      00519C30h
  004a738f  8be8                  mov       ebp,eax
  004a7391  a1b8e05d00            mov       eax,[5DE0B8h]
  004a7396  83c410                add       esp,10h
  004a7399  85c0                  test      eax,eax
  004a739b  7439                  je        short 004A73D6h
  004a739d  66395e1a              cmp       [esi+1Ah],bx
  004a73a1  7533                  jne       short 004A73D6h
  004a73a3  8b4500                mov       eax,[ebp]
  004a73a6  55                    push      ebp
  004a73a7  0c40                  or        al,40h
  004a73a9  56                    push      esi
  004a73aa  66c785960000000400    mov       word [ebp+96h],4
  004a73b3  894500                mov       [ebp],eax
  004a73b6  e8c5270900            call      00539B80h
  004a73bb  83c408                add       esp,8
  004a73be  83c51c                add       ebp,1Ch
  004a73c1  55                    push      ebp
  004a73c2  6800180000            push      1800h
  004a73c7  6800030000            push      300h
  004a73cc  6a03                  push      3
  004a73ce  e84d670900            call      0053DB20h
  004a73d3  83c410                add       esp,10h

LAB_004a73d6:
  004a73d6  8b06                  mov       eax,[esi]
  004a73d8  a900000002            test      eax,2000000h
  004a73dd  0f848e000000          je        near 004A7471h
  004a73e3  807e420c              cmp       byte [esi+42h],0Ch
  004a73e7  0f8484000000          je        near 004A7471h
  004a73ed  eb7a                  jmp       short 004A7469h

LAB_004a73ef:
  004a73ef  8d44242c              lea       eax,[esp+2Ch]
  004a73f3  6a00                  push      0
  004a73f5  50                    push      eax
  004a73f6  57                    push      edi
  004a73f7  56                    push      esi
  004a73f8  e833280700            call      00519C30h
  004a73fd  8be8                  mov       ebp,eax
  004a73ff  a1b8e05d00            mov       eax,[5DE0B8h]
  004a7404  83c410                add       esp,10h
  004a7407  85c0                  test      eax,eax
  004a7409  744f                  je        short 004A745Ah
  004a740b  8b5d00                mov       ebx,[ebp]
  004a740e  66c785960000000800    mov       word [ebp+96h],8
  004a7417  83cb40                or        ebx,40h
  004a741a  55                    push      ebp
  004a741b  895d00                mov       [ebp],ebx
  004a741e  8b4c242c              mov       ecx,[esp+2Ch]
  004a7422  56                    push      esi
  004a7423  668b4130              mov       ax,[ecx+30h]
  004a7427  0fbfd8                movsx     ebx,ax
  004a742a  66c1f803              sar       ax,3
  004a742e  66894130              mov       [ecx+30h],ax
  004a7432  e849270900            call      00539B80h
  004a7437  8b4c2430              mov       ecx,[esp+30h]
  004a743b  83c408                add       esp,8
  004a743e  83c51c                add       ebp,1Ch
  004a7441  55                    push      ebp
  004a7442  6800180000            push      1800h
  004a7447  6800030000            push      300h
  004a744c  6a03                  push      3
  004a744e  66895930              mov       [ecx+30h],bx
  004a7452  e8c9660900            call      0053DB20h
  004a7457  83c410                add       esp,10h

LAB_004a745a:
  004a745a  8b06                  mov       eax,[esi]
  004a745c  a900000002            test      eax,2000000h
  004a7461  740e                  je        short 004A7471h
  004a7463  807e420c              cmp       byte [esi+42h],0Ch
  004a7467  7408                  je        short 004A7471h

LAB_004a7469:
  004a7469  24fd                  and       al,0FDh
  004a746b  c6464300              mov       byte [esi+43h],0
  004a746f  8906                  mov       [esi],eax

LAB_004a7471:
  004a7471  33c0                  xor       eax,eax
  004a7473  8a4642                mov       al,[esi+42h]
  004a7476  83f80c                cmp       eax,0Ch
  004a7479  0f8771060000          ja        near 004A7AF0h
  004a747f  ff24851c814a00        jmp       dword [eax*4+4A811Ch]
  004a7486  33c9                  xor       ecx,ecx
  004a7488  66394e12              cmp       [esi+12h],cx
  004a748c  751a                  jne       short 004A74A8h
  004a748e  8b06                  mov       eax,[esi]
  004a7490  66894e18              mov       [esi+18h],cx
  004a7494  0d00000001            or        eax,1000000h
  004a7499  66894e1a              mov       [esi+1Ah],cx
  004a749d  8906                  mov       [esi],eax
  004a749f  66c746120100          mov       word [esi+12h],1
  004a74a5  894e5c                mov       [esi+5Ch],ecx

LAB_004a74a8:
  004a74a8  0fbf5610              movsx     edx,word [esi+10h]
  004a74ac  39542414              cmp       [esp+14h],edx
  004a74b0  7d09                  jge       short 004A74BBh
  004a74b2  c6464300              mov       byte [esi+43h],0
  004a74b6  e935060000            jmp       004A7AF0h

LAB_004a74bb:
  004a74bb  8a4643                mov       al,[esi+43h]
  004a74be  84c0                  test      al,al
  004a74c0  0f8465020000          je        near 004A772Bh
  004a74c6  fec8                  dec       al
  004a74c8  894e5c                mov       [esi+5Ch],ecx
  004a74cb  884643                mov       [esi+43h],al
  004a74ce  e91d060000            jmp       004A7AF0h
  004a74d3  33c9                  xor       ecx,ecx
  004a74d5  66394e12              cmp       [esi+12h],cx
  004a74d9  7517                  jne       short 004A74F2h
  004a74db  8b06                  mov       eax,[esi]
  004a74dd  66894e18              mov       [esi+18h],cx
  004a74e1  0d00000001            or        eax,1000000h
  004a74e6  66894e1a              mov       [esi+1Ah],cx
  004a74ea  8906                  mov       [esi],eax
  004a74ec  66c746120100          mov       word [esi+12h],1

LAB_004a74f2:
  004a74f2  8b44241c              mov       eax,[esp+1Ch]
  004a74f6  8d90c8000000          lea       edx,[eax+0C8h]
  004a74fc  81e2ff0f0000          and       edx,0FFFh
  004a7502  81fa90010000          cmp       edx,190h
  004a7508  7f0c                  jg        short 004A7516h
  004a750a  c6464300              mov       byte [esi+43h],0
  004a750e  894e5c                mov       [esi+5Ch],ecx
  004a7511  e9da050000            jmp       004A7AF0h

LAB_004a7516:
  004a7516  25ff0f0000            and       eax,0FFFh
  004a751b  3d00080000            cmp       eax,800h
  004a7520  7d15                  jge       short 004A7537h
  004a7522  8b442410              mov       eax,[esp+10h]
  004a7526  894e5c                mov       [esi+5Ch],ecx
  004a7529  0580000000            add       eax,80h
  004a752e  66014632              add       [esi+32h],ax
  004a7532  e9b9050000            jmp       004A7AF0h

LAB_004a7537:
  004a7537  8b542410              mov       edx,[esp+10h]
  004a753b  b880ffffff            mov       eax,0FFFFFF80h
  004a7540  2bc2                  sub       eax,edx
  004a7542  894e5c                mov       [esi+5Ch],ecx
  004a7545  66014632              add       [esi+32h],ax
  004a7549  e9a2050000            jmp       004A7AF0h
  004a754e  8b4c2410              mov       ecx,[esp+10h]
  004a7552  33c0                  xor       eax,eax
  004a7554  66394612              cmp       [esi+12h],ax
  004a7558  7527                  jne       short 004A7581h
  004a755a  8b2e                  mov       ebp,[esi]
  004a755c  8d14c9                lea       edx,[ecx+ecx*8]
  004a755f  c1e204                shl       edx,4
  004a7562  81cd00000001          or        ebp,1000000h
  004a7568  81c200500000          add       edx,5000h
  004a756e  66894618              mov       [esi+18h],ax
  004a7572  6689461a              mov       [esi+1Ah],ax
  004a7576  892e                  mov       [esi],ebp
  004a7578  66c746120100          mov       word [esi+12h],1
  004a757e  89565c                mov       [esi+5Ch],edx

LAB_004a7581:
  004a7581  66837e1201            cmp       word [esi+12h],1
  004a7586  0f8564050000          jne       near 004A7AF0h
  004a758c  0fbf5610              movsx     edx,word [esi+10h]
  004a7590  39542414              cmp       [esp+14h],edx
  004a7594  7d0c                  jge       short 004A75A2h
  004a7596  c6464300              mov       byte [esi+43h],0
  004a759a  89465c                mov       [esi+5Ch],eax
  004a759d  e94e050000            jmp       004A7AF0h

LAB_004a75a2:
  004a75a2  8a4643                mov       al,[esi+43h]
  004a75a5  84c0                  test      al,al
  004a75a7  7405                  je        short 004A75AEh
  004a75a9  fec8                  dec       al
  004a75ab  884643                mov       [esi+43h],al

LAB_004a75ae:
  004a75ae  8b44241c              mov       eax,[esp+1Ch]
  004a75b2  8d9000010000          lea       edx,[eax+100h]
  004a75b8  81e2ff0f0000          and       edx,0FFFh
  004a75be  81fa00020000          cmp       edx,200h
  004a75c4  0f8e26050000          jle       near 004A7AF0h
  004a75ca  25ff0f0000            and       eax,0FFFh
  004a75cf  3d00080000            cmp       eax,800h
  004a75d4  7d10                  jge       short 004A75E6h
  004a75d6  8d840980000000        lea       eax,[ecx+ecx+80h]
  004a75dd  66014632              add       [esi+32h],ax
  004a75e1  e90a050000            jmp       004A7AF0h

LAB_004a75e6:
  004a75e6  03c9                  add       ecx,ecx
  004a75e8  b880ffffff            mov       eax,0FFFFFF80h
  004a75ed  2bc1                  sub       eax,ecx
  004a75ef  66014632              add       [esi+32h],ax
  004a75f3  e9f8040000            jmp       004A7AF0h
  004a75f8  bb01000000            mov       ebx,1
  004a75fd  66395e12              cmp       [esi+12h],bx
  004a7601  0f85a5000000          jne       near 004A76ACh
  004a7607  8a4643                mov       al,[esi+43h]
  004a760a  84c0                  test      al,al
  004a760c  7405                  je        short 004A7613h
  004a760e  fec8                  dec       al
  004a7610  884643                mov       [esi+43h],al

LAB_004a7613:
  004a7613  8b442410              mov       eax,[esp+10h]
  004a7617  8b6c2424              mov       ebp,[esp+24h]
  004a761b  8d14c0                lea       edx,[eax+eax*8]
  004a761e  c1e204                shl       edx,4
  004a7621  8d8200500000          lea       eax,[edx+5000h]
  004a7627  0fafe8                imul      ebp,eax
  004a762a  89465c                mov       [esi+5Ch],eax
  004a762d  0fbf4610              movsx     eax,word [esi+10h]
  004a7631  50                    push      eax
  004a7632  c1fd0c                sar       ebp,0Ch
  004a7635  e8e2e30900            call      00545A1Ch
  004a763a  0fafc5                imul      eax,ebp
  004a763d  8b0f                  mov       ecx,[edi]
  004a763f  c1f80c                sar       eax,0Ch
  004a7642  03c8                  add       ecx,eax
  004a7644  890f                  mov       [edi],ecx
  004a7646  0fbf4e10              movsx     ecx,word [esi+10h]
  004a764a  51                    push      ecx
  004a764b  e8c6e30900            call      00545A16h
  004a7650  0fafc5                imul      eax,ebp
  004a7653  8b4e24                mov       ecx,[esi+24h]
  004a7656  c1f80c                sar       eax,0Ch
  004a7659  03c8                  add       ecx,eax
  004a765b  8b44241c              mov       eax,[esp+1Ch]
  004a765f  894e24                mov       [esi+24h],ecx
  004a7662  33c9                  xor       ecx,ecx
  004a7664  8d9000010000          lea       edx,[eax+100h]
  004a766a  894e5c                mov       [esi+5Ch],ecx
  004a766d  81e2ff0f0000          and       edx,0FFFh
  004a7673  81fa00020000          cmp       edx,200h
  004a7679  7e33                  jle       short 004A76AEh
  004a767b  25ff0f0000            and       eax,0FFFh
  004a7680  3d00080000            cmp       eax,800h
  004a7685  7d11                  jge       short 004A7698h
  004a7687  8b442410              mov       eax,[esp+10h]
  004a768b  8d04c532000000        lea       eax,[eax*8+32h]
  004a7692  66014632              add       [esi+32h],ax
  004a7696  eb16                  jmp       short 004A76AEh

LAB_004a7698:
  004a7698  8b542410              mov       edx,[esp+10h]
  004a769c  b8ceffffff            mov       eax,0FFFFFFCEh
  004a76a1  c1e203                shl       edx,3
  004a76a4  2bc2                  sub       eax,edx
  004a76a6  66014632              add       [esi+32h],ax
  004a76aa  eb02                  jmp       short 004A76AEh

LAB_004a76ac:
  004a76ac  33c9                  xor       ecx,ecx

LAB_004a76ae:
  004a76ae  66394e12              cmp       [esi+12h],cx
  004a76b2  0f8538040000          jne       near 004A7AF0h
  004a76b8  8b06                  mov       eax,[esi]
  004a76ba  66894e18              mov       [esi+18h],cx
  004a76be  0d00000001            or        eax,1000000h
  004a76c3  66894e1a              mov       [esi+1Ah],cx
  004a76c7  8906                  mov       [esi],eax
  004a76c9  66895e12              mov       [esi+12h],bx
  004a76cd  e91e040000            jmp       004A7AF0h
  004a76d2  668b4e12              mov       cx,[esi+12h]
  004a76d6  6683f901              cmp       cx,1
  004a76da  751c                  jne       short 004A76F8h
  004a76dc  0fbf4610              movsx     eax,word [esi+10h]
  004a76e0  39442414              cmp       [esp+14h],eax
  004a76e4  7d06                  jge       short 004A76ECh
  004a76e6  c6464300              mov       byte [esi+43h],0
  004a76ea  eb0c                  jmp       short 004A76F8h

LAB_004a76ec:
  004a76ec  8a4643                mov       al,[esi+43h]
  004a76ef  84c0                  test      al,al
  004a76f1  7405                  je        short 004A76F8h
  004a76f3  fec8                  dec       al
  004a76f5  884643                mov       [esi+43h],al

LAB_004a76f8:
  004a76f8  33c0                  xor       eax,eax
  004a76fa  663bc8                cmp       cx,ax
  004a76fd  0f85ed030000          jne       near 004A7AF0h
  004a7703  8b3e                  mov       edi,[esi]
  004a7705  66894618              mov       [esi+18h],ax
  004a7709  6689461a              mov       [esi+1Ah],ax
  004a770d  8b442410              mov       eax,[esp+10h]
  004a7711  81cf00000001          or        edi,1000000h
  004a7717  66c746120100          mov       word [esi+12h],1
  004a771d  8d0cc0                lea       ecx,[eax+eax*8]
  004a7720  893e                  mov       [esi],edi
  004a7722  c1e104                shl       ecx,4
  004a7725  81c100500000          add       ecx,5000h

LAB_004a772b:
  004a772b  894e5c                mov       [esi+5Ch],ecx
  004a772e  e9bd030000            jmp       004A7AF0h
  004a7733  33ed                  xor       ebp,ebp
  004a7735  66396e12              cmp       [esi+12h],bp
  004a7739  0f85f9000000          jne       near 004A7838h
  004a773f  33ff                  xor       edi,edi
  004a7741  b800f45f00            mov       eax,5FF400h

LAB_004a7746:
  004a7746  39706c                cmp       [eax+6Ch],esi
  004a7749  750c                  jne       short 004A7757h
  004a774b  f70000000080          test      dword [eax],80000000h
  004a7751  0f85bc090000          jne       near 004A8113h

LAB_004a7757:
  004a7757  059c000000            add       eax,9Ch
  004a775c  47                    inc       edi
  004a775d  3d80076000            cmp       eax,600780h
  004a7762  7ce2                  jl        short 004A7746h
  004a7764  8b06                  mov       eax,[esi]
  004a7766  a904080000            test      eax,804h
  004a776b  740e                  je        short 004A777Bh
  004a776d  c6464300              mov       byte [esi+43h],0
  004a7771  25fbf7ffff            and       eax,0FFFFF7FBh
  004a7776  e973030000            jmp       004A7AEEh

LAB_004a777b:
  004a777b  8b4c2418              mov       ecx,[esp+18h]
  004a777f  8b5620                mov       edx,[esi+20h]
  004a7782  81c200400b00          add       edx,0B4000h
  004a7788  3b5120                cmp       edx,[ecx+20h]
  004a778b  7d13                  jge       short 004A77A0h
  004a778d  25f9f7ffff            and       eax,0FFFFF7F9h
  004a7792  c6464300              mov       byte [esi+43h],0
  004a7796  8906                  mov       [esi],eax
  004a7798  896e5c                mov       [esi+5Ch],ebp
  004a779b  e950030000            jmp       004A7AF0h

LAB_004a77a0:
  004a77a0  6aff                  push      0FFFFFFFFh
  004a77a2  55                    push      ebp
  004a77a3  68bd000000            push      0BDh
  004a77a8  56                    push      esi
  004a77a9  e83233fdff            call      0047AAE0h
  004a77ae  83c410                add       esp,10h
  004a77b1  66396e16              cmp       [esi+16h],bp
  004a77b5  bb01000000            mov       ebx,1
  004a77ba  66895e12              mov       [esi+12h],bx
  004a77be  751b                  jne       short 004A77DBh
  004a77c0  e87bf90900            call      00547140h
  004a77c5  99                    cdq
  004a77c6  33c2                  xor       eax,edx
  004a77c8  2bc2                  sub       eax,edx
  004a77ca  83e003                and       eax,3
  004a77cd  33c2                  xor       eax,edx
  004a77cf  2bc2                  sub       eax,edx
  004a77d1  33ff                  xor       edi,edi
  004a77d3  668b3c45502a5500      mov       di,[eax*2+552A50h]

LAB_004a77db:
  004a77db  66395e16              cmp       [esi+16h],bx
  004a77df  750c                  jne       short 004A77EDh
  004a77e1  8b3d582a5500          mov       edi,[552A58h]
  004a77e7  81e7ffff0000          and       edi,0FFFFh

LAB_004a77ed:
  004a77ed  66897e06              mov       [esi+6],di
  004a77f1  a1c4975e00            mov       eax,[5E97C4h]
  004a77f6  8bd7                  mov       edx,edi
  004a77f8  81e7ff000000          and       edi,0FFh
  004a77fe  8b889c000000          mov       ecx,[eax+9Ch]
  004a7804  c1fa08                sar       edx,8
  004a7807  81e2ff000000          and       edx,0FFh
  004a780d  8d047f                lea       eax,[edi+edi*2]
  004a7810  c1e004                shl       eax,4
  004a7813  8b0c91                mov       ecx,[ecx+edx*4]
  004a7816  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004a781c  8b06                  mov       eax,[esi]
  004a781e  66895610              mov       [esi+10h],dx
  004a7822  0d04080001            or        eax,1000804h
  004a7827  896e5c                mov       [esi+5Ch],ebp
  004a782a  66c746180500          mov       word [esi+18h],5
  004a7830  66896e1a              mov       [esi+1Ah],bp
  004a7834  8906                  mov       [esi],eax
  004a7836  eb05                  jmp       short 004A783Dh

LAB_004a7838:
  004a7838  bb01000000            mov       ebx,1

LAB_004a783d:
  004a783d  66395e12              cmp       [esi+12h],bx
  004a7841  753d                  jne       short 004A7880h
  004a7843  8b4c2418              mov       ecx,[esp+18h]
  004a7847  8b4620                mov       eax,[esi+20h]
  004a784a  8b5120                mov       edx,[ecx+20h]
  004a784d  2bd0                  sub       edx,eax
  004a784f  81c200400100          add       edx,14000h
  004a7855  c1fa03                sar       edx,3
  004a7858  03d0                  add       edx,eax
  004a785a  66ff4e10              dec       word [esi+10h]
  004a785e  66395e10              cmp       [esi+10h],bx
  004a7862  895620                mov       [esi+20h],edx
  004a7865  7d19                  jge       short 004A7880h
  004a7867  8b06                  mov       eax,[esi]
  004a7869  66c746120200          mov       word [esi+12h],2
  004a786f  25fbfffffe            and       eax,0FEFFFFFBh
  004a7874  66c746180600          mov       word [esi+18h],6
  004a787a  66896e1a              mov       [esi+1Ah],bp
  004a787e  8906                  mov       [esi],eax

LAB_004a7880:
  004a7880  66837e1202            cmp       word [esi+12h],2
  004a7885  754c                  jne       short 004A78D3h
  004a7887  8b4c2418              mov       ecx,[esp+18h]
  004a788b  8b4620                mov       eax,[esi+20h]
  004a788e  8b5120                mov       edx,[ecx+20h]
  004a7891  2bd0                  sub       edx,eax
  004a7893  81c200400100          add       edx,14000h
  004a7899  c1fa03                sar       edx,3
  004a789c  03d0                  add       edx,eax
  004a789e  66837e1a03            cmp       word [esi+1Ah],3
  004a78a3  895620                mov       [esi+20h],edx
  004a78a6  752b                  jne       short 004A78D3h
  004a78a8  33c0                  xor       eax,eax
  004a78aa  6aff                  push      0FFFFFFFFh
  004a78ac  668b4606              mov       ax,[esi+6]
  004a78b0  6aff                  push      0FFFFFFFFh
  004a78b2  8bc8                  mov       ecx,eax
  004a78b4  c1f808                sar       eax,8
  004a78b7  81e1ff000000          and       ecx,0FFh
  004a78bd  25ff000000            and       eax,0FFh
  004a78c2  51                    push      ecx
  004a78c3  50                    push      eax
  004a78c4  56                    push      esi
  004a78c5  e8b6300900            call      0053A980h
  004a78ca  83c414                add       esp,14h
  004a78cd  66c746120300          mov       word [esi+12h],3

LAB_004a78d3:
  004a78d3  66837e1203            cmp       word [esi+12h],3
  004a78d8  0f8512020000          jne       near 004A7AF0h
  004a78de  8b542418              mov       edx,[esp+18h]
  004a78e2  8b4620                mov       eax,[esi+20h]
  004a78e5  8b4a20                mov       ecx,[edx+20h]
  004a78e8  2bc8                  sub       ecx,eax
  004a78ea  81c100400100          add       ecx,14000h
  004a78f0  c1f903                sar       ecx,3
  004a78f3  03c8                  add       ecx,eax
  004a78f5  8b06                  mov       eax,[esi]
  004a78f7  a900000002            test      eax,2000000h
  004a78fc  894e20                mov       [esi+20h],ecx
  004a78ff  0f84eb010000          je        near 004A7AF0h
  004a7905  c6464300              mov       byte [esi+43h],0
  004a7909  80e4f7                and       ah,0F7h
  004a790c  e9dd010000            jmp       004A7AEEh
  004a7911  8b542418              mov       edx,[esp+18h]
  004a7915  8b4e20                mov       ecx,[esi+20h]
  004a7918  8b4220                mov       eax,[edx+20h]
  004a791b  8d9100400b00          lea       edx,[ecx+0B4000h]
  004a7921  3bd0                  cmp       edx,eax
  004a7923  7d16                  jge       short 004A793Bh
  004a7925  8b06                  mov       eax,[esi]
  004a7927  c6464300              mov       byte [esi+43h],0
  004a792b  24fd                  and       al,0FDh
  004a792d  c7465c00000000        mov       dword [esi+5Ch],0
  004a7934  8906                  mov       [esi],eax
  004a7936  e9b5010000            jmp       004A7AF0h

LAB_004a793b:
  004a793b  2bc1                  sub       eax,ecx
  004a793d  0500800200            add       eax,28000h
  004a7942  c1f802                sar       eax,2
  004a7945  03c1                  add       eax,ecx
  004a7947  668b4e12              mov       cx,[esi+12h]
  004a794b  6683f901              cmp       cx,1
  004a794f  894620                mov       [esi+20h],eax
  004a7952  7511                  jne       short 004A7965h
  004a7954  8b06                  mov       eax,[esi]
  004a7956  a900000002            test      eax,2000000h
  004a795b  7408                  je        short 004A7965h
  004a795d  24fd                  and       al,0FDh
  004a795f  c6464300              mov       byte [esi+43h],0
  004a7963  8906                  mov       [esi],eax

LAB_004a7965:
  004a7965  6685c9                test      cx,cx
  004a7968  0f8582010000          jne       near 004A7AF0h
  004a796e  8b06                  mov       eax,[esi]
  004a7970  66c746120100          mov       word [esi+12h],1
  004a7976  25fffffffe            and       eax,0FEFFFFFFh
  004a797b  e96e010000            jmp       004A7AEEh
  004a7980  66837e1201            cmp       word [esi+12h],1
  004a7985  8b4c241c              mov       ecx,[esp+1Ch]
  004a7989  8b7c2410              mov       edi,[esp+10h]
  004a798d  0f859a000000          jne       near 004A7A2Dh
  004a7993  8b6e20                mov       ebp,[esi+20h]
  004a7996  8b5e60                mov       ebx,[esi+60h]
  004a7999  81c500300000          add       ebp,3000h
  004a799f  8bd5                  mov       edx,ebp
  004a79a1  896e20                mov       [esi+20h],ebp
  004a79a4  8bc2                  mov       eax,edx
  004a79a6  2bc3                  sub       eax,ebx
  004a79a8  c1f80c                sar       eax,0Ch
  004a79ab  3dc8000000            cmp       eax,0C8h
  004a79b0  7e21                  jle       short 004A79D3h
  004a79b2  8b06                  mov       eax,[esi]
  004a79b4  33db                  xor       ebx,ebx
  004a79b6  0d02000001            or        eax,1000002h
  004a79bb  66c746120200          mov       word [esi+12h],2
  004a79c1  66895e10              mov       [esi+10h],bx
  004a79c5  66c746180300          mov       word [esi+18h],3
  004a79cb  66895e1a              mov       [esi+1Ah],bx
  004a79cf  8906                  mov       [esi],eax
  004a79d1  eb02                  jmp       short 004A79D5h

LAB_004a79d3:
  004a79d3  33db                  xor       ebx,ebx

LAB_004a79d5:
  004a79d5  8d8100010000          lea       eax,[ecx+100h]
  004a79db  25ff0f0000            and       eax,0FFFh
  004a79e0  3d00020000            cmp       eax,200h
  004a79e5  7e25                  jle       short 004A7A0Ch
  004a79e7  8bc1                  mov       eax,ecx
  004a79e9  25ff0f0000            and       eax,0FFFh
  004a79ee  3d00080000            cmp       eax,800h
  004a79f3  7d09                  jge       short 004A79FEh
  004a79f5  8d843f80000000        lea       eax,[edi+edi+80h]
  004a79fc  eb0a                  jmp       short 004A7A08h

LAB_004a79fe:
  004a79fe  8d2c3f                lea       ebp,[edi+edi]
  004a7a01  b880ffffff            mov       eax,0FFFFFF80h
  004a7a06  2bc5                  sub       eax,ebp

LAB_004a7a08:
  004a7a08  66014632              add       [esi+32h],ax

LAB_004a7a0c:
  004a7a0c  8b442418              mov       eax,[esp+18h]
  004a7a10  81c200801100          add       edx,118000h
  004a7a16  3b5020                cmp       edx,[eax+20h]
  004a7a19  7d14                  jge       short 004A7A2Fh
  004a7a1b  8b06                  mov       eax,[esi]
  004a7a1d  c6464300              mov       byte [esi+43h],0
  004a7a21  24fd                  and       al,0FDh
  004a7a23  895e5c                mov       [esi+5Ch],ebx
  004a7a26  8906                  mov       [esi],eax
  004a7a28  e9c3000000            jmp       004A7AF0h

LAB_004a7a2d:
  004a7a2d  33db                  xor       ebx,ebx

LAB_004a7a2f:
  004a7a2f  66837e1202            cmp       word [esi+12h],2
  004a7a34  7572                  jne       short 004A7AA8h
  004a7a36  8b542418              mov       edx,[esp+18h]
  004a7a3a  8b4620                mov       eax,[esi+20h]
  004a7a3d  c7465c00e00100        mov       dword [esi+5Ch],1E000h
  004a7a44  8b5220                mov       edx,[edx+20h]
  004a7a47  2bd0                  sub       edx,eax
  004a7a49  81c200800200          add       edx,28000h
  004a7a4f  c1fa02                sar       edx,2
  004a7a52  03d0                  add       edx,eax
  004a7a54  66ff4610              inc       word [esi+10h]
  004a7a58  8d8100010000          lea       eax,[ecx+100h]
  004a7a5e  895620                mov       [esi+20h],edx
  004a7a61  668b5610              mov       dx,[esi+10h]
  004a7a65  25ff0f0000            and       eax,0FFFh
  004a7a6a  3d00020000            cmp       eax,200h
  004a7a6f  7e25                  jle       short 004A7A96h
  004a7a71  81e1ff0f0000          and       ecx,0FFFh
  004a7a77  81f900080000          cmp       ecx,800h
  004a7a7d  7d09                  jge       short 004A7A88h
  004a7a7f  8d843f80000000        lea       eax,[edi+edi+80h]
  004a7a86  eb0a                  jmp       short 004A7A92h

LAB_004a7a88:
  004a7a88  8d0c3f                lea       ecx,[edi+edi]
  004a7a8b  b880ffffff            mov       eax,0FFFFFF80h
  004a7a90  2bc1                  sub       eax,ecx

LAB_004a7a92:
  004a7a92  66014632              add       [esi+32h],ax

LAB_004a7a96:
  004a7a96  6683fa21              cmp       dx,21h
  004a7a9a  7c0c                  jl        short 004A7AA8h
  004a7a9c  8b06                  mov       eax,[esi]
  004a7a9e  66c746120300          mov       word [esi+12h],3
  004a7aa4  24fd                  and       al,0FDh
  004a7aa6  8906                  mov       [esi],eax

LAB_004a7aa8:
  004a7aa8  668b4e12              mov       cx,[esi+12h]
  004a7aac  6683f903              cmp       cx,3
  004a7ab0  751d                  jne       short 004A7ACFh
  004a7ab2  8b465c                mov       eax,[esi+5Ch]
  004a7ab5  8bd0                  mov       edx,eax
  004a7ab7  c1fa03                sar       edx,3
  004a7aba  2bc2                  sub       eax,edx
  004a7abc  89465c                mov       [esi+5Ch],eax
  004a7abf  2500f0ffff            and       eax,0FFFFF000h
  004a7ac4  3d00a00000            cmp       eax,0A000h
  004a7ac9  7d04                  jge       short 004A7ACFh
  004a7acb  c6464300              mov       byte [esi+43h],0

LAB_004a7acf:
  004a7acf  663bcb                cmp       cx,bx
  004a7ad2  751c                  jne       short 004A7AF0h
  004a7ad4  8b06                  mov       eax,[esi]
  004a7ad6  66c746120100          mov       word [esi+12h],1
  004a7adc  66c746180400          mov       word [esi+18h],4
  004a7ae2  66895e1a              mov       [esi+1Ah],bx
  004a7ae6  895e5c                mov       [esi+5Ch],ebx
  004a7ae9  0d00000001            or        eax,1000000h

LAB_004a7aee:
  004a7aee  8906                  mov       [esi],eax

LAB_004a7af0:
  004a7af0  8b06                  mov       eax,[esi]
  004a7af2  8a4e42                mov       cl,[esi+42h]
  004a7af5  a820                  test      al,20h
  004a7af7  7467                  je        short 004A7B60h
  004a7af9  80f90c                cmp       cl,0Ch
  004a7afc  7504                  jne       short 004A7B02h
  004a7afe  a802                  test      al,2
  004a7b00  7445                  je        short 004A7B47h

LAB_004a7b02:
  004a7b02  e839f60900            call      00547140h
  004a7b07  99                    cdq
  004a7b08  b932000000            mov       ecx,32h
  004a7b0d  8b7e20                mov       edi,[esi+20h]
  004a7b10  f7f9                  idiv      ecx
  004a7b12  66035614              add       dx,[esi+14h]
  004a7b16  83c264                add       edx,64h
  004a7b19  81e2ff0f0000          and       edx,0FFFh
  004a7b1f  66895614              mov       [esi+14h],dx
  004a7b23  0fbfd2                movsx     edx,dx
  004a7b26  52                    push      edx
  004a7b27  e8f0de0900            call      00545A1Ch
  004a7b2c  8b4e48                mov       ecx,[esi+48h]
  004a7b2f  0500300000            add       eax,3000h
  004a7b34  8d0440                lea       eax,[eax+eax*2]
  004a7b37  8d0480                lea       eax,[eax+eax*4]
  004a7b3a  8d1481                lea       edx,[ecx+eax*4]
  004a7b3d  2bd7                  sub       edx,edi
  004a7b3f  c1fa03                sar       edx,3
  004a7b42  03d7                  add       edx,edi
  004a7b44  895620                mov       [esi+20h],edx

LAB_004a7b47:
  004a7b47  8b4c2418              mov       ecx,[esp+18h]
  004a7b4b  8b4164                mov       eax,[ecx+64h]
  004a7b4e  8b4920                mov       ecx,[ecx+20h]
  004a7b51  33c8                  xor       ecx,eax
  004a7b53  f7c100f0ffff          test      ecx,0FFFFF000h
  004a7b59  7554                  jne       short 004A7BAFh
  004a7b5b  894648                mov       [esi+48h],eax
  004a7b5e  eb4f                  jmp       short 004A7BAFh

LAB_004a7b60:
  004a7b60  80f90c                cmp       cl,0Ch
  004a7b63  7504                  jne       short 004A7B69h
  004a7b65  a802                  test      al,2
  004a7b67  7446                  je        short 004A7BAFh

LAB_004a7b69:
  004a7b69  e8d2f50900            call      00547140h
  004a7b6e  99                    cdq
  004a7b6f  b932000000            mov       ecx,32h
  004a7b74  8b7e20                mov       edi,[esi+20h]
  004a7b77  f7f9                  idiv      ecx
  004a7b79  66035614              add       dx,[esi+14h]
  004a7b7d  83c264                add       edx,64h
  004a7b80  81e2ff0f0000          and       edx,0FFFh
  004a7b86  66895614              mov       [esi+14h],dx
  004a7b8a  0fbfd2                movsx     edx,dx
  004a7b8d  52                    push      edx
  004a7b8e  e889de0900            call      00545A1Ch
  004a7b93  8b4e60                mov       ecx,[esi+60h]
  004a7b96  8d0440                lea       eax,[eax+eax*2]
  004a7b99  8d0480                lea       eax,[eax+eax*4]
  004a7b9c  8d1481                lea       edx,[ecx+eax*4]
  004a7b9f  2bd7                  sub       edx,edi
  004a7ba1  81c200c00800          add       edx,8C000h
  004a7ba7  c1fa03                sar       edx,3
  004a7baa  03d7                  add       edx,edi
  004a7bac  895620                mov       [esi+20h],edx

LAB_004a7baf:
  004a7baf  0fbf4632              movsx     eax,word [esi+32h]
  004a7bb3  50                    push      eax
  004a7bb4  e863de0900            call      00545A1Ch
  004a7bb9  0fbf4e32              movsx     ecx,word [esi+32h]
  004a7bbd  51                    push      ecx
  004a7bbe  89442474              mov       [esp+74h],eax
  004a7bc2  e84fde0900            call      00545A16h
  004a7bc7  8b5650                mov       edx,[esi+50h]
  004a7bca  89442478              mov       [esp+78h],eax
  004a7bce  8bc2                  mov       eax,edx
  004a7bd0  8b4c2424              mov       ecx,[esp+24h]
  004a7bd4  c1f808                sar       eax,8
  004a7bd7  0faf4e5c              imul      ecx,[esi+5Ch]
  004a7bdb  8bfa                  mov       edi,edx
  004a7bdd  56                    push      esi
  004a7bde  2bf8                  sub       edi,eax
  004a7be0  c1fa03                sar       edx,3
  004a7be3  2bfa                  sub       edi,edx
  004a7be5  8b5654                mov       edx,[esi+54h]
  004a7be8  8bc2                  mov       eax,edx
  004a7bea  8bea                  mov       ebp,edx
  004a7bec  c1f808                sar       eax,8
  004a7bef  2be8                  sub       ebp,eax
  004a7bf1  897e50                mov       [esi+50h],edi
  004a7bf4  c1fa03                sar       edx,3
  004a7bf7  2bea                  sub       ebp,edx
  004a7bf9  8b5658                mov       edx,[esi+58h]
  004a7bfc  8bda                  mov       ebx,edx
  004a7bfe  8bc2                  mov       eax,edx
  004a7c00  c1fb03                sar       ebx,3
  004a7c03  2bc3                  sub       eax,ebx
  004a7c05  8b5e20                mov       ebx,[esi+20h]
  004a7c08  c1fa08                sar       edx,8
  004a7c0b  c1f90c                sar       ecx,0Ch
  004a7c0e  2bc2                  sub       eax,edx
  004a7c10  8bd1                  mov       edx,ecx
  004a7c12  0faf542474            imul      edx,[esp+74h]
  004a7c17  0faf4c247c            imul      ecx,[esp+7Ch]
  004a7c1c  c1fa0c                sar       edx,0Ch
  004a7c1f  c1f90c                sar       ecx,0Ch
  004a7c22  894658                mov       [esi+58h],eax
  004a7c25  03d7                  add       edx,edi
  004a7c27  8b7e1c                mov       edi,[esi+1Ch]
  004a7c2a  03c8                  add       ecx,eax
  004a7c2c  8b4624                mov       eax,[esi+24h]
  004a7c2f  03fa                  add       edi,edx
  004a7c31  03dd                  add       ebx,ebp
  004a7c33  03c1                  add       eax,ecx
  004a7c35  896e54                mov       [esi+54h],ebp
  004a7c38  897e1c                mov       [esi+1Ch],edi
  004a7c3b  895e20                mov       [esi+20h],ebx
  004a7c3e  894624                mov       [esi+24h],eax
  004a7c41  e8fabc0800            call      00533940h
  004a7c46  8bbe90000000          mov       edi,[esi+90h]
  004a7c4c  8b06                  mov       eax,[esi]
  004a7c4e  83c404                add       esp,4
  004a7c51  81e7fff8ffff          and       edi,0FFFFF8FFh
  004a7c57  a820                  test      al,20h
  004a7c59  89be90000000          mov       [esi+90h],edi
  004a7c5f  8bcf                  mov       ecx,edi
  004a7c61  740c                  je        short 004A7C6Fh
  004a7c63  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  004a7c69  898e90000000          mov       [esi+90h],ecx

LAB_004a7c6f:
  004a7c6f  8b4e60                mov       ecx,[esi+60h]
  004a7c72  8b5620                mov       edx,[esi+20h]
  004a7c75  3bd1                  cmp       edx,ecx
  004a7c77  7f19                  jg        short 004A7C92h
  004a7c79  8b9690000000          mov       edx,[esi+90h]
  004a7c7f  f6c680                test      dh,80h
  004a7c82  750e                  jne       short 004A7C92h
  004a7c84  24fe                  and       al,0FEh
  004a7c86  33db                  xor       ebx,ebx
  004a7c88  894e20                mov       [esi+20h],ecx
  004a7c8b  8906                  mov       [esi],eax
  004a7c8d  895e54                mov       [esi+54h],ebx
  004a7c90  eb02                  jmp       short 004A7C94h

LAB_004a7c92:
  004a7c92  33db                  xor       ebx,ebx

LAB_004a7c94:
  004a7c94  f7869000000082a80020  test      dword [esi+90h],2000A882h
  004a7c9e  0f84c0000000          je        near 004A7D64h
  004a7ca4  8a4e42                mov       cl,[esi+42h]
  004a7ca7  b007                  mov       al,7
  004a7ca9  3ac8                  cmp       cl,al
  004a7cab  7420                  je        short 004A7CCDh
  004a7cad  884642                mov       [esi+42h],al
  004a7cb0  e88bf40900            call      00547140h
  004a7cb5  99                    cdq
  004a7cb6  b928000000            mov       ecx,28h
  004a7cbb  66895e12              mov       [esi+12h],bx
  004a7cbf  f7f9                  idiv      ecx
  004a7cc1  66c746105000          mov       word [esi+10h],50h
  004a7cc7  80c214                add       dl,14h
  004a7cca  885643                mov       [esi+43h],dl

LAB_004a7ccd:
  004a7ccd  8b16                  mov       edx,[esi]
  004a7ccf  8b442464              mov       eax,[esp+64h]
  004a7cd3  8b4c2458              mov       ecx,[esp+58h]
  004a7cd7  83e2fd                and       edx,0FFFFFFFDh
  004a7cda  894624                mov       [esi+24h],eax
  004a7cdd  8b442448              mov       eax,[esp+48h]
  004a7ce1  8916                  mov       [esi],edx
  004a7ce3  8b54245c              mov       edx,[esp+5Ch]
  004a7ce7  25ffff0000            and       eax,0FFFFh
  004a7cec  89561c                mov       [esi+1Ch],edx
  004a7cef  8b542438              mov       edx,[esp+38h]
  004a7cf3  894e7c                mov       [esi+7Ch],ecx
  004a7cf6  8b4c2454              mov       ecx,[esp+54h]
  004a7cfa  898690000000          mov       [esi+90h],eax
  004a7d00  8b44244c              mov       eax,[esp+4Ch]
  004a7d04  899680000000          mov       [esi+80h],edx
  004a7d0a  8b542450              mov       edx,[esp+50h]
  004a7d0e  898684000000          mov       [esi+84h],eax
  004a7d14  8b44243c              mov       eax,[esp+3Ch]
  004a7d18  898e88000000          mov       [esi+88h],ecx
  004a7d1e  8b4c2440              mov       ecx,[esp+40h]
  004a7d22  6882a80020            push      2000A882h
  004a7d27  56                    push      esi
  004a7d28  89968c000000          mov       [esi+8Ch],edx
  004a7d2e  894664                mov       [esi+64h],eax
  004a7d31  894e60                mov       [esi+60h],ecx
  004a7d34  e827e10200            call      004D5E60h
  004a7d39  83c408                add       esp,8
  004a7d3c  663bc3                cmp       ax,bx
  004a7d3f  7515                  jne       short 004A7D56h
  004a7d41  e8faf30900            call      00547140h
  004a7d46  99                    cdq
  004a7d47  33c2                  xor       eax,edx
  004a7d49  2bc2                  sub       eax,edx
  004a7d4b  83e001                and       eax,1
  004a7d4e  33c2                  xor       eax,edx
  004a7d50  2bc2                  sub       eax,edx
  004a7d52  8d4400ff              lea       eax,[eax+eax-1]

LAB_004a7d56:
  004a7d56  8b542410              mov       edx,[esp+10h]
  004a7d5a  83c240                add       edx,40h
  004a7d5d  0fafd0                imul      edx,eax
  004a7d60  66015632              add       [esi+32h],dx

LAB_004a7d64:
  004a7d64  6aff                  push      0FFFFFFFFh
  004a7d66  6aff                  push      0FFFFFFFFh
  004a7d68  6aff                  push      0FFFFFFFFh
  004a7d6a  56                    push      esi
  004a7d6b  e8a0ba0100            call      004C3810h
  004a7d70  8b442434              mov       eax,[esp+34h]
  004a7d74  83c410                add       esp,10h
  004a7d77  3bc3                  cmp       eax,ebx
  004a7d79  7517                  jne       short 004A7D92h
  004a7d7b  668b842486000000      mov       ax,[esp+86h]
  004a7d83  66894632              mov       [esi+32h],ax
  004a7d87  5f                    pop       edi
  004a7d88  5e                    pop       esi
  004a7d89  5d                    pop       ebp
  004a7d8a  5b                    pop       ebx
  004a7d8b  81c488000000          add       esp,88h
  004a7d91  c3                    ret

LAB_004a7d92:
  004a7d92  385e43                cmp       [esi+43h],bl
  004a7d95  0f856d030000          jne       near 004A8108h
  004a7d9b  8b466c                mov       eax,[esi+6Ch]
  004a7d9e  8b4e1c                mov       ecx,[esi+1Ch]
  004a7da1  8b5624                mov       edx,[esi+24h]
  004a7da4  8b781c                mov       edi,[eax+1Ch]
  004a7da7  8b6824                mov       ebp,[eax+24h]
  004a7daa  2bf9                  sub       edi,ecx
  004a7dac  2bea                  sub       ebp,edx
  004a7dae  c1ff0c                sar       edi,0Ch
  004a7db1  c1fd0c                sar       ebp,0Ch
  004a7db4  8bcf                  mov       ecx,edi
  004a7db6  8bd5                  mov       edx,ebp
  004a7db8  0fafcf                imul      ecx,edi
  004a7dbb  0fafd5                imul      edx,ebp
  004a7dbe  03ca                  add       ecx,edx
  004a7dc0  51                    push      ecx
  004a7dc1  e84adc0900            call      00545A10h
  004a7dc6  55                    push      ebp
  004a7dc7  57                    push      edi
  004a7dc8  8944241c              mov       [esp+1Ch],eax
  004a7dcc  e851dc0900            call      00545A22h
  004a7dd1  668b4e32              mov       cx,[esi+32h]
  004a7dd5  66895e12              mov       [esi+12h],bx
  004a7dd9  2bc1                  sub       eax,ecx
  004a7ddb  25ff0f0000            and       eax,0FFFh
  004a7de0  8944241c              mov       [esp+1Ch],eax
  004a7de4  8b442414              mov       eax,[esp+14h]
  004a7de8  3d96000000            cmp       eax,96h
  004a7ded  0f8d27010000          jge       near 004A7F1Ah
  004a7df3  e848f30900            call      00547140h
  004a7df8  99                    cdq
  004a7df9  b964000000            mov       ecx,64h
  004a7dfe  f7f9                  idiv      ecx
  004a7e00  8bea                  mov       ebp,edx
  004a7e02  83fd32                cmp       ebp,32h
  004a7e05  0f8d87000000          jge       near 004A7E92h
  004a7e0b  8b54241c              mov       edx,[esp+1Ch]
  004a7e0f  8b7c2420              mov       edi,[esp+20h]
  004a7e13  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a7e1a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a7e21  25ff0f0000            and       eax,0FFFh
  004a7e26  3bc1                  cmp       eax,ecx
  004a7e28  7f5b                  jg        short 004A7E85h
  004a7e2a  e811f30900            call      00547140h
  004a7e2f  99                    cdq
  004a7e30  8d4c3f01              lea       ecx,[edi+edi+1]
  004a7e34  f7f9                  idiv      ecx
  004a7e36  8b442414              mov       eax,[esp+14h]
  004a7e3a  83c264                add       edx,64h
  004a7e3d  3bc2                  cmp       eax,edx
  004a7e3f  7d44                  jge       short 004A7E85h
  004a7e41  83fd1e                cmp       ebp,1Eh
  004a7e44  7d27                  jge       short 004A7E6Dh
  004a7e46  8b16                  mov       edx,[esi]
  004a7e48  c646420b              mov       byte [esi+42h],0Bh
  004a7e4c  81e2fffffffe          and       edx,0FEFFFFFFh
  004a7e52  c6464301              mov       byte [esi+43h],1
  004a7e56  83ca02                or        edx,2
  004a7e59  895e5c                mov       [esi+5Ch],ebx
  004a7e5c  8916                  mov       [esi],edx
  004a7e5e  66c746180300          mov       word [esi+18h],3
  004a7e64  66895e1a              mov       [esi+1Ah],bx
  004a7e68  e9b1000000            jmp       004A7F1Eh

LAB_004a7e6d:
  004a7e6d  885e42                mov       [esi+42h],bl
  004a7e70  e8cbf20900            call      00547140h
  004a7e75  99                    cdq
  004a7e76  b928000000            mov       ecx,28h
  004a7e7b  f7f9                  idiv      ecx
  004a7e7d  885643                mov       [esi+43h],dl
  004a7e80  e999000000            jmp       004A7F1Eh

LAB_004a7e85:
  004a7e85  c6464201              mov       byte [esi+42h],1
  004a7e89  c6464301              mov       byte [esi+43h],1
  004a7e8d  e98c000000            jmp       004A7F1Eh

LAB_004a7e92:
  004a7e92  83fd46                cmp       ebp,46h
  004a7e95  7d3e                  jge       short 004A7ED5h
  004a7e97  c6464203              mov       byte [esi+42h],3
  004a7e9b  66895e12              mov       [esi+12h],bx
  004a7e9f  e89cf20900            call      00547140h
  004a7ea4  99                    cdq
  004a7ea5  b90a000000            mov       ecx,0Ah
  004a7eaa  8b7c2420              mov       edi,[esp+20h]
  004a7eae  f7f9                  idiv      ecx
  004a7eb0  8bc7                  mov       eax,edi
  004a7eb2  d1f8                  sar       eax,1
  004a7eb4  02d0                  add       dl,al
  004a7eb6  80c214                add       dl,14h
  004a7eb9  885643                mov       [esi+43h],dl
  004a7ebc  e87ff20900            call      00547140h
  004a7ec1  99                    cdq
  004a7ec2  33c2                  xor       eax,edx
  004a7ec4  2bc2                  sub       eax,edx
  004a7ec6  25ff0f0000            and       eax,0FFFh
  004a7ecb  33c2                  xor       eax,edx
  004a7ecd  2bc2                  sub       eax,edx
  004a7ecf  66894610              mov       [esi+10h],ax
  004a7ed3  eb49                  jmp       short 004A7F1Eh

LAB_004a7ed5:
  004a7ed5  8b542410              mov       edx,[esp+10h]
  004a7ed9  b964000000            mov       ecx,64h
  004a7ede  d1fa                  sar       edx,1
  004a7ee0  2bcd                  sub       ecx,ebp
  004a7ee2  83c20a                add       edx,0Ah
  004a7ee5  3bca                  cmp       ecx,edx
  004a7ee7  7d1e                  jge       short 004A7F07h
  004a7ee9  66395e16              cmp       [esi+16h],bx
  004a7eed  750e                  jne       short 004A7EFDh
  004a7eef  c646420a              mov       byte [esi+42h],0Ah
  004a7ef3  c6464301              mov       byte [esi+43h],1
  004a7ef7  66895e12              mov       [esi+12h],bx
  004a7efb  eb1d                  jmp       short 004A7F1Ah

LAB_004a7efd:
  004a7efd  c646420c              mov       byte [esi+42h],0Ch
  004a7f01  c6464301              mov       byte [esi+43h],1
  004a7f05  eb13                  jmp       short 004A7F1Ah

LAB_004a7f07:
  004a7f07  885e42                mov       [esi+42h],bl
  004a7f0a  e831f20900            call      00547140h
  004a7f0f  99                    cdq
  004a7f10  b928000000            mov       ecx,28h
  004a7f15  f7f9                  idiv      ecx
  004a7f17  885643                mov       [esi+43h],dl

LAB_004a7f1a:
  004a7f1a  8b7c2420              mov       edi,[esp+20h]

LAB_004a7f1e:
  004a7f1e  8b442414              mov       eax,[esp+14h]
  004a7f22  3d58020000            cmp       eax,258h
  004a7f27  0f8df7000000          jge       near 004A8024h
  004a7f2d  3d96000000            cmp       eax,96h
  004a7f32  0f8cec000000          jl        near 004A8024h
  004a7f38  e803f20900            call      00547140h
  004a7f3d  99                    cdq
  004a7f3e  b964000000            mov       ecx,64h
  004a7f43  f7f9                  idiv      ecx
  004a7f45  83fa14                cmp       edx,14h
  004a7f48  7d29                  jge       short 004A7F73h
  004a7f4a  8b54241c              mov       edx,[esp+1Ch]
  004a7f4e  81c200040000          add       edx,400h
  004a7f54  81e2ff0f0000          and       edx,0FFFh
  004a7f5a  81fa00080000          cmp       edx,800h
  004a7f60  0f8c93000000          jl        near 004A7FF9h
  004a7f66  c6464201              mov       byte [esi+42h],1
  004a7f6a  c6464301              mov       byte [esi+43h],1
  004a7f6e  e9b1000000            jmp       004A8024h

LAB_004a7f73:
  004a7f73  83fa28                cmp       edx,28h
  004a7f76  7d28                  jge       short 004A7FA0h
  004a7f78  c6464202              mov       byte [esi+42h],2
  004a7f7c  e8bff10900            call      00547140h
  004a7f81  99                    cdq
  004a7f82  b90a000000            mov       ecx,0Ah
  004a7f87  66c746105000          mov       word [esi+10h],50h
  004a7f8d  f7f9                  idiv      ecx
  004a7f8f  8bc7                  mov       eax,edi
  004a7f91  d1f8                  sar       eax,1
  004a7f93  02d0                  add       dl,al
  004a7f95  80c214                add       dl,14h
  004a7f98  885643                mov       [esi+43h],dl
  004a7f9b  e984000000            jmp       004A8024h

LAB_004a7fa0:
  004a7fa0  83fa50                cmp       edx,50h
  004a7fa3  7d3a                  jge       short 004A7FDFh
  004a7fa5  c6464203              mov       byte [esi+42h],3
  004a7fa9  66895e12              mov       [esi+12h],bx
  004a7fad  e88ef10900            call      00547140h
  004a7fb2  99                    cdq
  004a7fb3  b90a000000            mov       ecx,0Ah
  004a7fb8  f7f9                  idiv      ecx
  004a7fba  8bc7                  mov       eax,edi
  004a7fbc  d1f8                  sar       eax,1
  004a7fbe  02d0                  add       dl,al
  004a7fc0  80c214                add       dl,14h
  004a7fc3  885643                mov       [esi+43h],dl
  004a7fc6  e875f10900            call      00547140h
  004a7fcb  99                    cdq
  004a7fcc  33c2                  xor       eax,edx
  004a7fce  2bc2                  sub       eax,edx
  004a7fd0  25ff0f0000            and       eax,0FFFh
  004a7fd5  33c2                  xor       eax,edx
  004a7fd7  2bc2                  sub       eax,edx
  004a7fd9  66894610              mov       [esi+10h],ax
  004a7fdd  eb45                  jmp       short 004A8024h

LAB_004a7fdf:
  004a7fdf  b964000000            mov       ecx,64h
  004a7fe4  2bca                  sub       ecx,edx
  004a7fe6  8b542410              mov       edx,[esp+10h]
  004a7fea  d1fa                  sar       edx,1
  004a7fec  83c20a                add       edx,0Ah
  004a7fef  3bca                  cmp       ecx,edx
  004a7ff1  7d1e                  jge       short 004A8011h
  004a7ff3  66395e16              cmp       [esi+16h],bx
  004a7ff7  750e                  jne       short 004A8007h

LAB_004a7ff9:
  004a7ff9  c646420a              mov       byte [esi+42h],0Ah
  004a7ffd  c6464301              mov       byte [esi+43h],1
  004a8001  66895e12              mov       [esi+12h],bx
  004a8005  eb1d                  jmp       short 004A8024h

LAB_004a8007:
  004a8007  c646420c              mov       byte [esi+42h],0Ch
  004a800b  c6464301              mov       byte [esi+43h],1
  004a800f  eb13                  jmp       short 004A8024h

LAB_004a8011:
  004a8011  885e42                mov       [esi+42h],bl
  004a8014  e827f10900            call      00547140h
  004a8019  99                    cdq
  004a801a  b928000000            mov       ecx,28h
  004a801f  f7f9                  idiv      ecx
  004a8021  885643                mov       [esi+43h],dl

LAB_004a8024:
  004a8024  817c241458020000      cmp       dword [esp+14h],258h
  004a802c  0f8cd6000000          jl        near 004A8108h
  004a8032  e809f10900            call      00547140h
  004a8037  99                    cdq
  004a8038  b964000000            mov       ecx,64h
  004a803d  f7f9                  idiv      ecx
  004a803f  83fa32                cmp       edx,32h
  004a8042  7d6f                  jge       short 004A80B3h
  004a8044  8b442410              mov       eax,[esp+10h]
  004a8048  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004a804c  3bd1                  cmp       edx,ecx
  004a804e  7d2e                  jge       short 004A807Eh
  004a8050  c6464202              mov       byte [esi+42h],2
  004a8054  e8e7f00900            call      00547140h
  004a8059  99                    cdq
  004a805a  b90a000000            mov       ecx,0Ah
  004a805f  66c746106400          mov       word [esi+10h],64h
  004a8065  f7f9                  idiv      ecx
  004a8067  8bc7                  mov       eax,edi
  004a8069  d1f8                  sar       eax,1
  004a806b  02d0                  add       dl,al
  004a806d  80c214                add       dl,14h
  004a8070  885643                mov       [esi+43h],dl
  004a8073  5f                    pop       edi
  004a8074  5e                    pop       esi
  004a8075  5d                    pop       ebp
  004a8076  5b                    pop       ebx
  004a8077  81c488000000          add       esp,88h
  004a807d  c3                    ret

LAB_004a807e:
  004a807e  885e42                mov       [esi+42h],bl
  004a8081  e8baf00900            call      00547140h
  004a8086  99                    cdq
  004a8087  b90a000000            mov       ecx,0Ah
  004a808c  66895e12              mov       [esi+12h],bx
  004a8090  f7f9                  idiv      ecx
  004a8092  8b4c2410              mov       ecx,[esp+10h]
  004a8096  8bc7                  mov       eax,edi
  004a8098  c1f802                sar       eax,2
  004a809b  02d0                  add       dl,al
  004a809d  885643                mov       [esi+43h],dl
  004a80a0  8d540950              lea       edx,[ecx+ecx+50h]
  004a80a4  66895610              mov       [esi+10h],dx
  004a80a8  5f                    pop       edi
  004a80a9  5e                    pop       esi
  004a80aa  5d                    pop       ebp
  004a80ab  5b                    pop       ebx
  004a80ac  81c488000000          add       esp,88h
  004a80b2  c3                    ret

LAB_004a80b3:
  004a80b3  83fa4b                cmp       edx,4Bh
  004a80b6  7d43                  jge       short 004A80FBh
  004a80b8  c6464203              mov       byte [esi+42h],3
  004a80bc  66895e12              mov       [esi+12h],bx
  004a80c0  e87bf00900            call      00547140h
  004a80c5  99                    cdq
  004a80c6  b90a000000            mov       ecx,0Ah
  004a80cb  f7f9                  idiv      ecx
  004a80cd  8bc7                  mov       eax,edi
  004a80cf  d1f8                  sar       eax,1
  004a80d1  02d0                  add       dl,al
  004a80d3  80c214                add       dl,14h
  004a80d6  885643                mov       [esi+43h],dl
  004a80d9  e862f00900            call      00547140h
  004a80de  99                    cdq
  004a80df  33c2                  xor       eax,edx
  004a80e1  2bc2                  sub       eax,edx
  004a80e3  25ff0f0000            and       eax,0FFFh
  004a80e8  33c2                  xor       eax,edx
  004a80ea  2bc2                  sub       eax,edx
  004a80ec  66894610              mov       [esi+10h],ax
  004a80f0  5f                    pop       edi
  004a80f1  5e                    pop       esi
  004a80f2  5d                    pop       ebp
  004a80f3  5b                    pop       ebx
  004a80f4  81c488000000          add       esp,88h
  004a80fa  c3                    ret

LAB_004a80fb:
  004a80fb  83fa3c                cmp       edx,3Ch
  004a80fe  7d08                  jge       short 004A8108h
  004a8100  c6464201              mov       byte [esi+42h],1
  004a8104  c6464301              mov       byte [esi+43h],1

LAB_004a8108:
  004a8108  5f                    pop       edi
  004a8109  5e                    pop       esi
  004a810a  5d                    pop       ebp
  004a810b  5b                    pop       ebx
  004a810c  81c488000000          add       esp,88h
  004a8112  c3                    ret

LAB_004a8113:
  004a8113  c6464300              mov       byte [esi+43h],0
  004a8117  e9d4f9ffff            jmp       004A7AF0h
  004a811c  86744a00              xchg      dh,[edx+ecx*2]
  004a8120  d3744a00              sal       dword [edx+ecx*2],cl
  004a8124  4e                    dec       esi
  004a8125  754a                  jne       short 004A8171h
  004a8127  00f8                  add       al,bh
  004a8129  754a                  jne       short 004A8175h
  004a812b  00f0                  add       al,dh
  004a812d  7a4a                  jp        short 004A8179h
  004a812f  00f0                  add       al,dh
  004a8131  7a4a                  jp        short 004A817Dh
  004a8133  00f0                  add       al,dh
  004a8135  7a4a                  jp        short 004A8181h
  004a8137  00d2                  add       dl,dl
  004a8139  764a                  jbe       short 004A8185h
  004a813b  00f0                  add       al,dh
  004a813d  7a4a                  jp        short 004A8189h
  004a813f  00f0                  add       al,dh
  004a8141  7a4a                  jp        short 004A818Dh
  004a8143  0033                  add       [ebx],dh
  004a8145  774a                  ja        short 004A8191h
  004a8147  0011                  add       [ecx],dl
  004a8149  794a                  jns       short 004A8195h
  004a814b  0080794a00            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
