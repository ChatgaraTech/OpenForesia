Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004879f0  section .text  offset fichier 0x86df0  1904 octets

  004879f0  83ec2c                sub       esp,2Ch
  004879f3  53                    push      ebx
  004879f4  55                    push      ebp
  004879f5  56                    push      esi
  004879f6  8b74243c              mov       esi,[esp+3Ch]
  004879fa  8d442424              lea       eax,[esp+24h]
  004879fe  57                    push      edi
  004879ff  8d4c242c              lea       ecx,[esp+2Ch]
  00487a03  50                    push      eax
  00487a04  8d542428              lea       edx,[esp+28h]
  00487a08  51                    push      ecx
  00487a09  33ed                  xor       ebp,ebp
  00487a0b  52                    push      edx
  00487a0c  56                    push      esi
  00487a0d  66896c2440            mov       [esp+40h],bp
  00487a12  66c74424421e00        mov       word [esp+42h],1Eh
  00487a19  66c74424443200        mov       word [esp+44h],32h
  00487a20  66c74424466400        mov       word [esp+46h],64h
  00487a27  66896c2448            mov       [esp+48h],bp
  00487a2c  66c744244a4600        mov       word [esp+4Ah],46h
  00487a33  e8480f0b00            call      00538980h
  00487a38  83c410                add       esp,10h
  00487a3b  8d442420              lea       eax,[esp+20h]
  00487a3f  8d4c241c              lea       ecx,[esp+1Ch]
  00487a43  8d542410              lea       edx,[esp+10h]
  00487a47  50                    push      eax
  00487a48  51                    push      ecx
  00487a49  8d442420              lea       eax,[esp+20h]
  00487a4d  52                    push      edx
  00487a4e  8d4c244c              lea       ecx,[esp+4Ch]
  00487a52  50                    push      eax
  00487a53  51                    push      ecx
  00487a54  56                    push      esi
  00487a55  e8c6e40a00            call      00535F20h
  00487a5a  8b4c2458              mov       ecx,[esp+58h]
  00487a5e  b840000000            mov       eax,40h
  00487a63  2bc1                  sub       eax,ecx
  00487a65  83c418                add       esp,18h
  00487a68  89442414              mov       [esp+14h],eax
  00487a6c  8b44241c              mov       eax,[esp+1Ch]
  00487a70  3bc5                  cmp       eax,ebp
  00487a72  bf01000000            mov       edi,1
  00487a77  0f844f030000          je        near 00487DCCh
  00487a7d  56                    push      esi
  00487a7e  e81d3b0500            call      004DB5A0h
  00487a83  8b06                  mov       eax,[esi]
  00487a85  83c404                add       esp,4
  00487a88  a802                  test      al,2
  00487a8a  0f84cf000000          je        near 00487B5Fh
  00487a90  8a4c2420              mov       cl,[esp+20h]
  00487a94  bb10000000            mov       ebx,10h
  00487a99  84cb                  test      bl,cl
  00487a9b  740d                  je        short 00487AAAh
  00487a9d  24fd                  and       al,0FDh
  00487a9f  c6464300              mov       byte [esi+43h],0
  00487aa3  8906                  mov       [esi],eax
  00487aa5  e922030000            jmp       00487DCCh

LAB_00487aaa:
  00487aaa  33c9                  xor       ecx,ecx
  00487aac  8a4e42                mov       cl,[esi+42h]
  00487aaf  2bcd                  sub       ecx,ebp
  00487ab1  7432                  je        short 00487AE5h
  00487ab3  49                    dec       ecx
  00487ab4  0f8512030000          jne       near 00487DCCh
  00487aba  66837e1a07            cmp       word [esi+1Ah],7
  00487abf  750e                  jne       short 00487ACFh
  00487ac1  6aff                  push      0FFFFFFFFh
  00487ac3  55                    push      ebp
  00487ac4  6a49                  push      49h
  00487ac6  56                    push      esi
  00487ac7  e8042fffff            call      0047A9D0h
  00487acc  83c410                add       esp,10h

LAB_00487acf:
  00487acf  8b06                  mov       eax,[esi]
  00487ad1  a900000002            test      eax,2000000h
  00487ad6  0f84f0020000          je        near 00487DCCh
  00487adc  24fd                  and       al,0FDh
  00487ade  8906                  mov       [esi],eax
  00487ae0  e9e7020000            jmp       00487DCCh

LAB_00487ae5:
  00487ae5  66837e1a09            cmp       word [esi+1Ah],9
  00487aea  7557                  jne       short 00487B43h
  00487aec  8d542430              lea       edx,[esp+30h]
  00487af0  55                    push      ebp
  00487af1  8d461c                lea       eax,[esi+1Ch]
  00487af4  52                    push      edx
  00487af5  50                    push      eax
  00487af6  56                    push      esi
  00487af7  e834210900            call      00519C30h
  00487afc  8bf8                  mov       edi,eax
  00487afe  a1b8e05d00            mov       eax,[5DE0B8h]
  00487b03  83c410                add       esp,10h
  00487b06  3bc5                  cmp       eax,ebp
  00487b08  742f                  je        short 00487B39h
  00487b0a  57                    push      edi
  00487b0b  56                    push      esi
  00487b0c  e86f200b00            call      00539B80h
  00487b11  8b07                  mov       eax,[edi]
  00487b13  83c408                add       esp,8
  00487b16  0c40                  or        al,40h
  00487b18  66899f96000000        mov       [edi+96h],bx
  00487b1f  8907                  mov       [edi],eax
  00487b21  83c71c                add       edi,1Ch
  00487b24  57                    push      edi
  00487b25  6800180000            push      1800h
  00487b2a  6800030000            push      300h
  00487b2f  6a03                  push      3
  00487b31  e8ea5f0b00            call      0053DB20h
  00487b36  83c410                add       esp,10h

LAB_00487b39:
  00487b39  8b06                  mov       eax,[esi]
  00487b3b  a801                  test      al,1
  00487b3d  740c                  je        short 00487B4Bh
  00487b3f  66ff4e1a              dec       word [esi+1Ah]

LAB_00487b43:
  00487b43  a801                  test      al,1
  00487b45  0f8581020000          jne       near 00487DCCh

LAB_00487b4b:
  00487b4b  a900000002            test      eax,2000000h
  00487b50  0f8473020000          je        near 00487DC9h
  00487b56  24fd                  and       al,0FDh
  00487b58  8906                  mov       [esi],eax
  00487b5a  e96a020000            jmp       00487DC9h

LAB_00487b5f:
  00487b5f  a810                  test      al,10h
  00487b61  0f8565020000          jne       near 00487DCCh
  00487b67  a904080000            test      eax,804h
  00487b6c  0f855a020000          jne       near 00487DCCh
  00487b72  a801                  test      al,1
  00487b74  7439                  je        short 00487BAFh
  00487b76  e8c5f50b00            call      00547140h
  00487b7b  99                    cdq
  00487b7c  b964000000            mov       ecx,64h
  00487b81  f7f9                  idiv      ecx
  00487b83  8b442440              mov       eax,[esp+40h]
  00487b87  c1f802                sar       eax,2
  00487b8a  3bd0                  cmp       edx,eax
  00487b8c  0f8d3a020000          jge       near 00487DCCh
  00487b92  8b0e                  mov       ecx,[esi]
  00487b94  c7461803000000        mov       dword [esi+18h],3
  00487b9b  81e1fffffffe          and       ecx,0FEFFFFFFh
  00487ba1  c6464200              mov       byte [esi+42h],0
  00487ba5  83c902                or        ecx,2
  00487ba8  890e                  mov       [esi],ecx
  00487baa  e91a020000            jmp       00487DC9h

LAB_00487baf:
  00487baf  33c9                  xor       ecx,ecx
  00487bb1  8a4e42                mov       cl,[esi+42h]
  00487bb4  83f904                cmp       ecx,4
  00487bb7  0f870f020000          ja        near 00487DCCh
  00487bbd  ff248d4c814800        jmp       dword [ecx*4+48814Ch]
  00487bc4  66396e18              cmp       [esi+18h],bp
  00487bc8  740a                  je        short 00487BD4h
  00487bca  0d00000001            or        eax,1000000h
  00487bcf  896e18                mov       [esi+18h],ebp
  00487bd2  8906                  mov       [esi],eax

LAB_00487bd4:
  00487bd4  817c24182c010000      cmp       dword [esp+18h],12Ch
  00487bdc  7d09                  jge       short 00487BE7h
  00487bde  c6464300              mov       byte [esi+43h],0
  00487be2  e9e5010000            jmp       00487DCCh

LAB_00487be7:
  00487be7  8a4643                mov       al,[esi+43h]
  00487bea  84c0                  test      al,al
  00487bec  0f84d7010000          je        near 00487DC9h
  00487bf2  fec8                  dec       al
  00487bf4  884643                mov       [esi+43h],al
  00487bf7  e9cd010000            jmp       00487DC9h
  00487bfc  66397e18              cmp       [esi+18h],di
  00487c00  740a                  je        short 00487C0Ch
  00487c02  0d00000001            or        eax,1000000h
  00487c07  897e18                mov       [esi+18h],edi
  00487c0a  8906                  mov       [esi],eax

LAB_00487c0c:
  00487c0c  8b542440              mov       edx,[esp+40h]
  00487c10  8b4c2410              mov       ecx,[esp+10h]
  00487c14  8d841280000000        lea       eax,[edx+edx+80h]
  00487c1b  8d1408                lea       edx,[eax+ecx]
  00487c1e  8d3c00                lea       edi,[eax+eax]
  00487c21  81e2ff0f0000          and       edx,0FFFh
  00487c27  3bd7                  cmp       edx,edi
  00487c29  0f8f20010000          jg        near 00487D4Fh
  00487c2f  66014e32              add       [esi+32h],cx
  00487c33  c6464300              mov       byte [esi+43h],0
  00487c37  e98d010000            jmp       00487DC9h
  00487c3c  66397e18              cmp       [esi+18h],di
  00487c40  740a                  je        short 00487C4Ch
  00487c42  0d00000001            or        eax,1000000h
  00487c47  897e18                mov       [esi+18h],edi
  00487c4a  8906                  mov       [esi],eax

LAB_00487c4c:
  00487c4c  0fbf4610              movsx     eax,word [esi+10h]
  00487c50  39442418              cmp       [esp+18h],eax
  00487c54  7d09                  jge       short 00487C5Fh
  00487c56  c6464300              mov       byte [esi+43h],0
  00487c5a  e96d010000            jmp       00487DCCh

LAB_00487c5f:
  00487c5f  8a4643                mov       al,[esi+43h]
  00487c62  84c0                  test      al,al
  00487c64  7405                  je        short 00487C6Bh
  00487c66  fec8                  dec       al
  00487c68  884643                mov       [esi+43h],al

LAB_00487c6b:
  00487c6b  8b542440              mov       edx,[esp+40h]
  00487c6f  8b4c2410              mov       ecx,[esp+10h]
  00487c73  8d841280000000        lea       eax,[edx+edx+80h]
  00487c7a  8d3c08                lea       edi,[eax+ecx]
  00487c7d  8d1c00                lea       ebx,[eax+eax]
  00487c80  81e7ff0f0000          and       edi,0FFFh
  00487c86  3bfb                  cmp       edi,ebx
  00487c88  7f15                  jg        short 00487C9Fh
  00487c8a  66014e32              add       [esi+32h],cx
  00487c8e  81c2c0000000          add       edx,0C0h
  00487c94  c1e207                shl       edx,7
  00487c97  89565c                mov       [esi+5Ch],edx
  00487c9a  e92d010000            jmp       00487DCCh

LAB_00487c9f:
  00487c9f  81e1ff0f0000          and       ecx,0FFFh
  00487ca5  81f900080000          cmp       ecx,800h
  00487cab  7c02                  jl        short 00487CAFh
  00487cad  f7d8                  neg       eax

LAB_00487caf:
  00487caf  66014632              add       [esi+32h],ax
  00487cb3  81c2c0000000          add       edx,0C0h
  00487cb9  c1e207                shl       edx,7
  00487cbc  89565c                mov       [esi+5Ch],edx
  00487cbf  e908010000            jmp       00487DCCh
  00487cc4  8a4e43                mov       cl,[esi+43h]
  00487cc7  84c9                  test      cl,cl
  00487cc9  7405                  je        short 00487CD0h
  00487ccb  fec9                  dec       cl
  00487ccd  884e43                mov       [esi+43h],cl

LAB_00487cd0:
  00487cd0  66397e18              cmp       [esi+18h],di
  00487cd4  740a                  je        short 00487CE0h
  00487cd6  0d00000001            or        eax,1000000h
  00487cdb  897e18                mov       [esi+18h],edi
  00487cde  8906                  mov       [esi],eax

LAB_00487ce0:
  00487ce0  8b4c2440              mov       ecx,[esp+40h]
  00487ce4  8b7c241c              mov       edi,[esp+1Ch]
  00487ce8  0fbf5610              movsx     edx,word [esi+10h]
  00487cec  8d8180000000          lea       eax,[ecx+80h]
  00487cf2  52                    push      edx
  00487cf3  c1e007                shl       eax,7
  00487cf6  0faff8                imul      edi,eax
  00487cf9  89465c                mov       [esi+5Ch],eax
  00487cfc  c1ff0c                sar       edi,0Ch
  00487cff  e818dd0b00            call      00545A1Ch
  00487d04  0fafc7                imul      eax,edi
  00487d07  8b5e1c                mov       ebx,[esi+1Ch]
  00487d0a  c1f80c                sar       eax,0Ch
  00487d0d  03d8                  add       ebx,eax
  00487d0f  0fbf4610              movsx     eax,word [esi+10h]
  00487d13  50                    push      eax
  00487d14  895e1c                mov       [esi+1Ch],ebx
  00487d17  e8fadc0b00            call      00545A16h
  00487d1c  0fafc7                imul      eax,edi
  00487d1f  8b5e24                mov       ebx,[esi+24h]
  00487d22  8b4c2440              mov       ecx,[esp+40h]
  00487d26  c1f80c                sar       eax,0Ch
  00487d29  03d8                  add       ebx,eax
  00487d2b  8d048d32000000        lea       eax,[ecx*4+32h]
  00487d32  8b4c2410              mov       ecx,[esp+10h]
  00487d36  895e24                mov       [esi+24h],ebx
  00487d39  8d3c00                lea       edi,[eax+eax]
  00487d3c  8d1408                lea       edx,[eax+ecx]
  00487d3f  81e2ff0f0000          and       edx,0FFFh
  00487d45  3bd7                  cmp       edx,edi
  00487d47  7f06                  jg        short 00487D4Fh
  00487d49  66014e32              add       [esi+32h],cx
  00487d4d  eb7a                  jmp       short 00487DC9h

LAB_00487d4f:
  00487d4f  81e1ff0f0000          and       ecx,0FFFh
  00487d55  81f900080000          cmp       ecx,800h
  00487d5b  7c02                  jl        short 00487D5Fh
  00487d5d  f7d8                  neg       eax

LAB_00487d5f:
  00487d5f  66014632              add       [esi+32h],ax
  00487d63  eb64                  jmp       short 00487DC9h
  00487d65  25fffffffe            and       eax,0FEFFFFFFh
  00487d6a  c7461804000000        mov       dword [esi+18h],4
  00487d71  0bc7                  or        eax,edi
  00487d73  8b7c241c              mov       edi,[esp+1Ch]
  00487d77  8906                  mov       [esi],eax
  00487d79  8b442440              mov       eax,[esp+40h]
  00487d7d  05c0000000            add       eax,0C0h
  00487d82  c1e007                shl       eax,7
  00487d85  0fbf4e10              movsx     ecx,word [esi+10h]
  00487d89  0faff8                imul      edi,eax
  00487d8c  51                    push      ecx
  00487d8d  89465c                mov       [esi+5Ch],eax
  00487d90  c1ff0c                sar       edi,0Ch
  00487d93  e884dc0b00            call      00545A1Ch
  00487d98  0fafc7                imul      eax,edi
  00487d9b  0fbf5610              movsx     edx,word [esi+10h]
  00487d9f  8b4e50                mov       ecx,[esi+50h]
  00487da2  52                    push      edx
  00487da3  c1f80c                sar       eax,0Ch
  00487da6  03c8                  add       ecx,eax
  00487da8  894e50                mov       [esi+50h],ecx
  00487dab  e866dc0b00            call      00545A16h
  00487db0  0fafc7                imul      eax,edi
  00487db3  8b4e58                mov       ecx,[esi+58h]
  00487db6  c1f80c                sar       eax,0Ch
  00487db9  03c8                  add       ecx,eax
  00487dbb  8b4654                mov       eax,[esi+54h]
  00487dbe  0500900100            add       eax,19000h
  00487dc3  894e58                mov       [esi+58h],ecx
  00487dc6  894654                mov       [esi+54h],eax

LAB_00487dc9:
  00487dc9  896e5c                mov       [esi+5Ch],ebp

LAB_00487dcc:
  00487dcc  8b44241c              mov       eax,[esp+1Ch]
  00487dd0  50                    push      eax
  00487dd1  56                    push      esi
  00487dd2  e889030000            call      00488160h
  00487dd7  83c408                add       esp,8
  00487dda  56                    push      esi
  00487ddb  e860bb0a00            call      00533940h
  00487de0  83c404                add       esp,4
  00487de3  56                    push      esi
  00487de4  e8077d0500            call      004DFAF0h
  00487de9  8b4c2428              mov       ecx,[esp+28h]
  00487ded  83c404                add       esp,4
  00487df0  51                    push      ecx
  00487df1  56                    push      esi
  00487df2  e879b90500            call      004E3770h
  00487df7  8b442424              mov       eax,[esp+24h]
  00487dfb  83c408                add       esp,8
  00487dfe  3bc5                  cmp       eax,ebp
  00487e00  0f843c030000          je        near 00488142h
  00487e06  f70617080000          test      dword [esi],817h
  00487e0c  0f8530030000          jne       near 00488142h
  00487e12  8a4643                mov       al,[esi+43h]
  00487e15  84c0                  test      al,al
  00487e17  0f8525030000          jne       near 00488142h
  00487e1d  8b466c                mov       eax,[esi+6Ch]
  00487e20  8b5e1c                mov       ebx,[esi+1Ch]
  00487e23  8b4e24                mov       ecx,[esi+24h]
  00487e26  8b781c                mov       edi,[eax+1Ch]
  00487e29  8b6824                mov       ebp,[eax+24h]
  00487e2c  2bfb                  sub       edi,ebx
  00487e2e  2be9                  sub       ebp,ecx
  00487e30  c1ff0c                sar       edi,0Ch
  00487e33  c1fd0c                sar       ebp,0Ch
  00487e36  8bd7                  mov       edx,edi
  00487e38  8bc5                  mov       eax,ebp
  00487e3a  0fafd7                imul      edx,edi
  00487e3d  0fafc5                imul      eax,ebp
  00487e40  03d0                  add       edx,eax
  00487e42  52                    push      edx
  00487e43  e8c8db0b00            call      00545A10h
  00487e48  55                    push      ebp
  00487e49  57                    push      edi
  00487e4a  89442420              mov       [esp+20h],eax
  00487e4e  e8cfdb0b00            call      00545A22h
  00487e53  668b4e32              mov       cx,[esi+32h]
  00487e57  2bc1                  sub       eax,ecx
  00487e59  25ff0f0000            and       eax,0FFFh
  00487e5e  89442410              mov       [esp+10h],eax
  00487e62  8b442418              mov       eax,[esp+18h]
  00487e66  3dfa000000            cmp       eax,0FAh
  00487e6b  0f8d26010000          jge       near 00487F97h
  00487e71  e8caf20b00            call      00547140h
  00487e76  99                    cdq
  00487e77  b964000000            mov       ecx,64h
  00487e7c  f7f9                  idiv      ecx
  00487e7e  83fa2d                cmp       edx,2Dh
  00487e81  7d65                  jge       short 00487EE8h
  00487e83  8b542410              mov       edx,[esp+10h]
  00487e87  8b7c2414              mov       edi,[esp+14h]
  00487e8b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00487e92  8d0cfd00020000        lea       ecx,[edi*8+200h]
  00487e99  25ff0f0000            and       eax,0FFFh
  00487e9e  3bc1                  cmp       eax,ecx
  00487ea0  0f8f38020000          jg        near 004880DEh
  00487ea6  e895f20b00            call      00547140h
  00487eab  99                    cdq
  00487eac  8d4c3f01              lea       ecx,[edi+edi+1]
  00487eb0  f7f9                  idiv      ecx
  00487eb2  8b442418              mov       eax,[esp+18h]
  00487eb6  83c264                add       edx,64h
  00487eb9  3bc2                  cmp       eax,edx
  00487ebb  0f8d1d020000          jge       near 004880DEh
  00487ec1  8b16                  mov       edx,[esi]
  00487ec3  c7461803000000        mov       dword [esi+18h],3
  00487eca  81e2fffffffe          and       edx,0FEFFFFFFh
  00487ed0  c6464200              mov       byte [esi+42h],0
  00487ed4  83ca02                or        edx,2
  00487ed7  c7465c00000000        mov       dword [esi+5Ch],0
  00487ede  8916                  mov       [esi],edx
  00487ee0  5f                    pop       edi
  00487ee1  5e                    pop       esi
  00487ee2  5d                    pop       ebp
  00487ee3  5b                    pop       ebx
  00487ee4  83c42c                add       esp,2Ch
  00487ee7  c3                    ret

LAB_00487ee8:
  00487ee8  83fa3c                cmp       edx,3Ch
  00487eeb  7d2d                  jge       short 00487F1Ah
  00487eed  c6464202              mov       byte [esi+42h],2
  00487ef1  e84af20b00            call      00547140h
  00487ef6  99                    cdq
  00487ef7  b90a000000            mov       ecx,0Ah
  00487efc  66c746106400          mov       word [esi+10h],64h
  00487f02  f7f9                  idiv      ecx
  00487f04  8b442414              mov       eax,[esp+14h]
  00487f08  d1f8                  sar       eax,1
  00487f0a  02d0                  add       dl,al
  00487f0c  80c214                add       dl,14h
  00487f0f  885643                mov       [esi+43h],dl
  00487f12  5f                    pop       edi
  00487f13  5e                    pop       esi
  00487f14  5d                    pop       ebp
  00487f15  5b                    pop       ebx
  00487f16  83c42c                add       esp,2Ch
  00487f19  c3                    ret

LAB_00487f1a:
  00487f1a  83fa4b                cmp       edx,4Bh
  00487f1d  7d24                  jge       short 00487F43h
  00487f1f  8b4c2440              mov       ecx,[esp+40h]
  00487f23  8b442418              mov       eax,[esp+18h]
  00487f27  83c164                add       ecx,64h
  00487f2a  3bc1                  cmp       eax,ecx
  00487f2c  7e15                  jle       short 00487F43h
  00487f2e  c6464204              mov       byte [esi+42h],4
  00487f32  e809f20b00            call      00547140h
  00487f37  66894610              mov       [esi+10h],ax
  00487f3b  5f                    pop       edi
  00487f3c  5e                    pop       esi
  00487f3d  5d                    pop       ebp
  00487f3e  5b                    pop       ebx
  00487f3f  83c42c                add       esp,2Ch
  00487f42  c3                    ret

LAB_00487f43:
  00487f43  c6464203              mov       byte [esi+42h],3
  00487f47  e8f4f10b00            call      00547140h
  00487f4c  99                    cdq
  00487f4d  b90a000000            mov       ecx,0Ah
  00487f52  f7f9                  idiv      ecx
  00487f54  8b442414              mov       eax,[esp+14h]
  00487f58  8b4c2440              mov       ecx,[esp+40h]
  00487f5c  d1f8                  sar       eax,1
  00487f5e  8db980000000          lea       edi,[ecx+80h]
  00487f64  c1e704                shl       edi,4
  00487f67  02d0                  add       dl,al
  00487f69  80c214                add       dl,14h
  00487f6c  885643                mov       [esi+43h],dl
  00487f6f  e8ccf10b00            call      00547140h
  00487f74  99                    cdq
  00487f75  f7ff                  idiv      edi
  00487f77  d1ff                  sar       edi,1
  00487f79  2bd7                  sub       edx,edi
  00487f7b  8b7c2410              mov       edi,[esp+10h]
  00487f7f  03d7                  add       edx,edi
  00487f81  66035632              add       dx,[esi+32h]
  00487f85  81c200080000          add       edx,800h
  00487f8b  66895610              mov       [esi+10h],dx
  00487f8f  5f                    pop       edi
  00487f90  5e                    pop       esi
  00487f91  5d                    pop       ebp
  00487f92  5b                    pop       ebx
  00487f93  83c42c                add       esp,2Ch
  00487f96  c3                    ret

LAB_00487f97:
  00487f97  3df4010000            cmp       eax,1F4h
  00487f9c  0f8dd1000000          jge       near 00488073h
  00487fa2  e899f10b00            call      00547140h
  00487fa7  99                    cdq
  00487fa8  b964000000            mov       ecx,64h
  00487fad  f7f9                  idiv      ecx
  00487faf  8b442440              mov       eax,[esp+40h]
  00487fb3  d1f8                  sar       eax,1
  00487fb5  83c008                add       eax,8
  00487fb8  3bd0                  cmp       edx,eax
  00487fba  7d2e                  jge       short 00487FEAh
  00487fbc  8b4c2410              mov       ecx,[esp+10h]
  00487fc0  81c100040000          add       ecx,400h
  00487fc6  81e1ff0f0000          and       ecx,0FFFh
  00487fcc  81f900080000          cmp       ecx,800h
  00487fd2  0f8d06010000          jge       near 004880DEh
  00487fd8  8b16                  mov       edx,[esi]
  00487fda  81e2fffffffe          and       edx,0FEFFFFFFh
  00487fe0  83ca02                or        edx,2
  00487fe3  8916                  mov       [esi],edx
  00487fe5  e9da000000            jmp       004880C4h

LAB_00487fea:
  00487fea  83fa46                cmp       edx,46h
  00487fed  7d4e                  jge       short 0048803Dh
  00487fef  c6464203              mov       byte [esi+42h],3
  00487ff3  e848f10b00            call      00547140h
  00487ff8  99                    cdq
  00487ff9  b90a000000            mov       ecx,0Ah
  00487ffe  f7f9                  idiv      ecx
  00488000  8b442414              mov       eax,[esp+14h]
  00488004  8b4c2440              mov       ecx,[esp+40h]
  00488008  d1f8                  sar       eax,1
  0048800a  8db980000000          lea       edi,[ecx+80h]
  00488010  c1e704                shl       edi,4
  00488013  02d0                  add       dl,al
  00488015  80c214                add       dl,14h
  00488018  885643                mov       [esi+43h],dl
  0048801b  e820f10b00            call      00547140h
  00488020  99                    cdq
  00488021  f7ff                  idiv      edi
  00488023  8b4c2410              mov       ecx,[esp+10h]
  00488027  d1ff                  sar       edi,1
  00488029  2bd7                  sub       edx,edi
  0048802b  03d1                  add       edx,ecx
  0048802d  66035632              add       dx,[esi+32h]
  00488031  66895610              mov       [esi+10h],dx
  00488035  5f                    pop       edi
  00488036  5e                    pop       esi
  00488037  5d                    pop       ebp
  00488038  5b                    pop       ebx
  00488039  83c42c                add       esp,2Ch
  0048803c  c3                    ret

LAB_0048803d:
  0048803d  83fa5a                cmp       edx,5Ah
  00488040  0f8dad000000          jge       near 004880F3h
  00488046  c6464202              mov       byte [esi+42h],2
  0048804a  e8f1f00b00            call      00547140h
  0048804f  99                    cdq
  00488050  b90a000000            mov       ecx,0Ah
  00488055  66c746106400          mov       word [esi+10h],64h
  0048805b  f7f9                  idiv      ecx
  0048805d  8b442414              mov       eax,[esp+14h]
  00488061  d1f8                  sar       eax,1
  00488063  02d0                  add       dl,al
  00488065  80c214                add       dl,14h
  00488068  885643                mov       [esi+43h],dl
  0048806b  5f                    pop       edi
  0048806c  5e                    pop       esi
  0048806d  5d                    pop       ebp
  0048806e  5b                    pop       ebx
  0048806f  83c42c                add       esp,2Ch
  00488072  c3                    ret

LAB_00488073:
  00488073  e8c8f00b00            call      00547140h
  00488078  99                    cdq
  00488079  b964000000            mov       ecx,64h
  0048807e  f7f9                  idiv      ecx
  00488080  8b442440              mov       eax,[esp+40h]
  00488084  d1f8                  sar       eax,1
  00488086  83c008                add       eax,8
  00488089  3bd0                  cmp       edx,eax
  0048808b  7d61                  jge       short 004880EEh
  0048808d  817c2418bc020000      cmp       dword [esp+18h],2BCh
  00488095  7d47                  jge       short 004880DEh
  00488097  8b4c2410              mov       ecx,[esp+10h]
  0048809b  8b442414              mov       eax,[esp+14h]
  0048809f  8d948100010000        lea       edx,[ecx+eax*4+100h]
  004880a6  8d04c500020000        lea       eax,[eax*8+200h]
  004880ad  81e2ff0f0000          and       edx,0FFFh
  004880b3  3bd0                  cmp       edx,eax
  004880b5  7f27                  jg        short 004880DEh
  004880b7  8b0e                  mov       ecx,[esi]
  004880b9  81e1fffffffe          and       ecx,0FEFFFFFFh
  004880bf  83c902                or        ecx,2
  004880c2  890e                  mov       [esi],ecx

LAB_004880c4:
  004880c4  c7461803000000        mov       dword [esi+18h],3
  004880cb  c6464201              mov       byte [esi+42h],1
  004880cf  c7465c00000000        mov       dword [esi+5Ch],0
  004880d6  5f                    pop       edi
  004880d7  5e                    pop       esi
  004880d8  5d                    pop       ebp
  004880d9  5b                    pop       ebx
  004880da  83c42c                add       esp,2Ch
  004880dd  c3                    ret

LAB_004880de:
  004880de  c6464201              mov       byte [esi+42h],1
  004880e2  c6464301              mov       byte [esi+43h],1
  004880e6  5f                    pop       edi
  004880e7  5e                    pop       esi
  004880e8  5d                    pop       ebp
  004880e9  5b                    pop       ebx
  004880ea  83c42c                add       esp,2Ch
  004880ed  c3                    ret

LAB_004880ee:
  004880ee  83fa3c                cmp       edx,3Ch
  004880f1  7d27                  jge       short 0048811Ah

LAB_004880f3:
  004880f3  c6464200              mov       byte [esi+42h],0
  004880f7  e844f00b00            call      00547140h
  004880fc  99                    cdq
  004880fd  b90a000000            mov       ecx,0Ah
  00488102  f7f9                  idiv      ecx
  00488104  8b442414              mov       eax,[esp+14h]
  00488108  d1f8                  sar       eax,1
  0048810a  02d0                  add       dl,al
  0048810c  80c214                add       dl,14h
  0048810f  885643                mov       [esi+43h],dl
  00488112  5f                    pop       edi
  00488113  5e                    pop       esi
  00488114  5d                    pop       ebp
  00488115  5b                    pop       ebx
  00488116  83c42c                add       esp,2Ch
  00488119  c3                    ret

LAB_0048811a:
  0048811a  c6464203              mov       byte [esi+42h],3
  0048811e  e81df00b00            call      00547140h
  00488123  99                    cdq
  00488124  b90a000000            mov       ecx,0Ah
  00488129  f7f9                  idiv      ecx
  0048812b  8b442414              mov       eax,[esp+14h]
  0048812f  d1f8                  sar       eax,1
  00488131  02d0                  add       dl,al
  00488133  80c214                add       dl,14h
  00488136  885643                mov       [esi+43h],dl
  00488139  e802f00b00            call      00547140h
  0048813e  66894610              mov       [esi+10h],ax

LAB_00488142:
  00488142  5f                    pop       edi
  00488143  5e                    pop       esi
  00488144  5d                    pop       ebp
  00488145  5b                    pop       ebx
  00488146  83c42c                add       esp,2Ch
  00488149  c3                    ret
  0048814a  8bff                  mov       edi,edi
  0048814c  c47b48                les       edi,[ebx+48h]
  0048814f  00fc                  add       ah,bh
  00488151  7b48                  jnp       short 0048819Bh
  00488153  003c7c                add       [esp+edi*2],bh
  00488156  48                    dec       eax
  00488157  00c4                  add       ah,al
  00488159  7c48                  jl        short 004881A3h
  0048815b  00657d                add       [ebp+7Dh],ah
  0048815e  48                    dec       eax
  0048815f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
