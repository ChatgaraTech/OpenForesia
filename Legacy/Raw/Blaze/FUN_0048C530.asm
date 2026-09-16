Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048c530  section .text  offset fichier 0x8b930  1664 octets

  0048c530  83ec2c                sub       esp,2Ch
  0048c533  53                    push      ebx
  0048c534  55                    push      ebp
  0048c535  56                    push      esi
  0048c536  8b74243c              mov       esi,[esp+3Ch]
  0048c53a  8d442424              lea       eax,[esp+24h]
  0048c53e  57                    push      edi
  0048c53f  8d4c242c              lea       ecx,[esp+2Ch]
  0048c543  50                    push      eax
  0048c544  8d542424              lea       edx,[esp+24h]
  0048c548  51                    push      ecx
  0048c549  33db                  xor       ebx,ebx
  0048c54b  52                    push      edx
  0048c54c  56                    push      esi
  0048c54d  66895c2440            mov       [esp+40h],bx
  0048c552  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  0048c559  66c74424443c00        mov       word [esp+44h],3Ch
  0048c560  66c74424469600        mov       word [esp+46h],96h
  0048c567  66895c2448            mov       [esp+48h],bx
  0048c56c  66c744244a7800        mov       word [esp+4Ah],78h
  0048c573  e808c40a00            call      00538980h
  0048c578  83c410                add       esp,10h
  0048c57b  8d442424              lea       eax,[esp+24h]
  0048c57f  8d4c241c              lea       ecx,[esp+1Ch]
  0048c583  8d542414              lea       edx,[esp+14h]
  0048c587  50                    push      eax
  0048c588  51                    push      ecx
  0048c589  8d442418              lea       eax,[esp+18h]
  0048c58d  52                    push      edx
  0048c58e  8d4c244c              lea       ecx,[esp+4Ch]
  0048c592  50                    push      eax
  0048c593  51                    push      ecx
  0048c594  56                    push      esi
  0048c595  e886990a00            call      00535F20h
  0048c59a  8b4c2458              mov       ecx,[esp+58h]
  0048c59e  b840000000            mov       eax,40h
  0048c5a3  2bc1                  sub       eax,ecx
  0048c5a5  83c418                add       esp,18h
  0048c5a8  89442418              mov       [esp+18h],eax
  0048c5ac  8b44241c              mov       eax,[esp+1Ch]
  0048c5b0  3bc3                  cmp       eax,ebx
  0048c5b2  0f8415030000          je        near 0048C8CDh
  0048c5b8  56                    push      esi
  0048c5b9  e8e2ef0400            call      004DB5A0h
  0048c5be  8b06                  mov       eax,[esi]
  0048c5c0  83c404                add       esp,4
  0048c5c3  a802                  test      al,2
  0048c5c5  0f841f010000          je        near 0048C6EAh
  0048c5cb  f644242410            test      byte [esp+24h],10h
  0048c5d0  740c                  je        short 0048C5DEh
  0048c5d2  24fd                  and       al,0FDh
  0048c5d4  885e43                mov       [esi+43h],bl
  0048c5d7  8906                  mov       [esi],eax
  0048c5d9  e9ef020000            jmp       0048C8CDh

LAB_0048c5de:
  0048c5de  8d542430              lea       edx,[esp+30h]
  0048c5e2  53                    push      ebx
  0048c5e3  8d461c                lea       eax,[esi+1Ch]
  0048c5e6  52                    push      edx
  0048c5e7  50                    push      eax
  0048c5e8  56                    push      esi
  0048c5e9  e842d60800            call      00519C30h
  0048c5ee  8bf8                  mov       edi,eax
  0048c5f0  a1b8e05d00            mov       eax,[5DE0B8h]
  0048c5f5  83c410                add       esp,10h
  0048c5f8  3bc3                  cmp       eax,ebx
  0048c5fa  7432                  je        short 0048C62Eh
  0048c5fc  8b17                  mov       edx,[edi]
  0048c5fe  57                    push      edi
  0048c5ff  83ca40                or        edx,40h
  0048c602  56                    push      esi
  0048c603  66c787960000001000    mov       word [edi+96h],10h
  0048c60c  8917                  mov       [edi],edx
  0048c60e  e86dd50a00            call      00539B80h
  0048c613  83c408                add       esp,8
  0048c616  83c71c                add       edi,1Ch
  0048c619  57                    push      edi
  0048c61a  6800180000            push      1800h
  0048c61f  6800030000            push      300h
  0048c624  6a03                  push      3
  0048c626  e8f5140b00            call      0053DB20h
  0048c62b  83c410                add       esp,10h

LAB_0048c62e:
  0048c62e  8b4c2440              mov       ecx,[esp+40h]
  0048c632  8b465c                mov       eax,[esi+5Ch]
  0048c635  81c180020000          add       ecx,280h
  0048c63b  c1e107                shl       ecx,7
  0048c63e  2bc8                  sub       ecx,eax
  0048c640  c1f904                sar       ecx,4
  0048c643  03c8                  add       ecx,eax
  0048c645  668b4628              mov       ax,[esi+28h]
  0048c649  894e5c                mov       [esi+5Ch],ecx
  0048c64c  8a4e43                mov       cl,[esi+43h]
  0048c64f  80f908                cmp       cl,8
  0048c652  0fbfd0                movsx     edx,ax
  0048c655  7235                  jb        short 0048C68Ch
  0048c657  bf00300000            mov       edi,3000h
  0048c65c  2bfa                  sub       edi,edx
  0048c65e  c1ff02                sar       edi,2
  0048c661  03f8                  add       edi,eax
  0048c663  668b462a              mov       ax,[esi+2Ah]
  0048c667  0fbfd0                movsx     edx,ax
  0048c66a  66897e28              mov       [esi+28h],di
  0048c66e  bf00300000            mov       edi,3000h
  0048c673  2bfa                  sub       edi,edx
  0048c675  c1ff02                sar       edi,2
  0048c678  03f8                  add       edi,eax
  0048c67a  668b462c              mov       ax,[esi+2Ch]
  0048c67e  66897e2a              mov       [esi+2Ah],di
  0048c682  bf00300000            mov       edi,3000h
  0048c687  0fbfd0                movsx     edx,ax
  0048c68a  eb33                  jmp       short 0048C6BFh

LAB_0048c68c:
  0048c68c  bf00100000            mov       edi,1000h
  0048c691  2bfa                  sub       edi,edx
  0048c693  c1ff02                sar       edi,2
  0048c696  03f8                  add       edi,eax
  0048c698  668b462a              mov       ax,[esi+2Ah]
  0048c69c  0fbfd0                movsx     edx,ax
  0048c69f  66897e28              mov       [esi+28h],di
  0048c6a3  bf00100000            mov       edi,1000h
  0048c6a8  2bfa                  sub       edi,edx
  0048c6aa  c1ff02                sar       edi,2
  0048c6ad  03f8                  add       edi,eax
  0048c6af  668b462c              mov       ax,[esi+2Ch]
  0048c6b3  66897e2a              mov       [esi+2Ah],di
  0048c6b7  bf00100000            mov       edi,1000h
  0048c6bc  0fbfd0                movsx     edx,ax

LAB_0048c6bf:
  0048c6bf  2bfa                  sub       edi,edx
  0048c6c1  c1ff02                sar       edi,2
  0048c6c4  03f8                  add       edi,eax
  0048c6c6  84c9                  test      cl,cl
  0048c6c8  66897e2c              mov       [esi+2Ch],di
  0048c6cc  0f84fb010000          je        near 0048C8CDh
  0048c6d2  8ac1                  mov       al,cl
  0048c6d4  fec8                  dec       al
  0048c6d6  884643                mov       [esi+43h],al
  0048c6d9  0f85ee010000          jne       near 0048C8CDh
  0048c6df  8b06                  mov       eax,[esi]
  0048c6e1  24fd                  and       al,0FDh
  0048c6e3  8906                  mov       [esi],eax
  0048c6e5  e9e3010000            jmp       0048C8CDh

LAB_0048c6ea:
  0048c6ea  a810                  test      al,10h
  0048c6ec  0f85db010000          jne       near 0048C8CDh
  0048c6f2  a904080000            test      eax,804h
  0048c6f7  0f85d0010000          jne       near 0048C8CDh
  0048c6fd  33c0                  xor       eax,eax
  0048c6ff  8a4642                mov       al,[esi+42h]
  0048c702  83f803                cmp       eax,3
  0048c705  0f8776010000          ja        near 0048C881h
  0048c70b  ff248594cb4800        jmp       dword [eax*4+48CB94h]
  0048c712  817c24102c010000      cmp       dword [esp+10h],12Ch
  0048c71a  7d09                  jge       short 0048C725h
  0048c71c  c6464300              mov       byte [esi+43h],0
  0048c720  e95c010000            jmp       0048C881h

LAB_0048c725:
  0048c725  8a4643                mov       al,[esi+43h]
  0048c728  84c0                  test      al,al
  0048c72a  7463                  je        short 0048C78Fh
  0048c72c  fec8                  dec       al
  0048c72e  884643                mov       [esi+43h],al
  0048c731  8b465c                mov       eax,[esi+5Ch]
  0048c734  8bc8                  mov       ecx,eax
  0048c736  c1f904                sar       ecx,4
  0048c739  2bc1                  sub       eax,ecx
  0048c73b  89465c                mov       [esi+5Ch],eax
  0048c73e  e93e010000            jmp       0048C881h
  0048c743  8b442440              mov       eax,[esp+40h]
  0048c747  8b4c2414              mov       ecx,[esp+14h]
  0048c74b  c1f802                sar       eax,2
  0048c74e  83c038                add       eax,38h
  0048c751  8d1408                lea       edx,[eax+ecx]
  0048c754  8d3c00                lea       edi,[eax+eax]
  0048c757  81e2ff0f0000          and       edx,0FFFh
  0048c75d  3bd7                  cmp       edx,edi
  0048c75f  7f1a                  jg        short 0048C77Bh
  0048c761  8b465c                mov       eax,[esi+5Ch]
  0048c764  66014e32              add       [esi+32h],cx
  0048c768  8bc8                  mov       ecx,eax
  0048c76a  c6464300              mov       byte [esi+43h],0
  0048c76e  c1f904                sar       ecx,4
  0048c771  2bc1                  sub       eax,ecx
  0048c773  89465c                mov       [esi+5Ch],eax
  0048c776  e906010000            jmp       0048C881h

LAB_0048c77b:
  0048c77b  81e1ff0f0000          and       ecx,0FFFh
  0048c781  81f900080000          cmp       ecx,800h
  0048c787  7c02                  jl        short 0048C78Bh
  0048c789  f7d8                  neg       eax

LAB_0048c78b:
  0048c78b  66014632              add       [esi+32h],ax

LAB_0048c78f:
  0048c78f  8b465c                mov       eax,[esi+5Ch]
  0048c792  8bc8                  mov       ecx,eax
  0048c794  c1f904                sar       ecx,4
  0048c797  2bc1                  sub       eax,ecx
  0048c799  89465c                mov       [esi+5Ch],eax
  0048c79c  e9e0000000            jmp       0048C881h
  0048c7a1  817c24102c010000      cmp       dword [esp+10h],12Ch
  0048c7a9  7d09                  jge       short 0048C7B4h
  0048c7ab  c6464300              mov       byte [esi+43h],0
  0048c7af  e9cd000000            jmp       0048C881h

LAB_0048c7b4:
  0048c7b4  8a4643                mov       al,[esi+43h]
  0048c7b7  84c0                  test      al,al
  0048c7b9  7405                  je        short 0048C7C0h
  0048c7bb  fec8                  dec       al
  0048c7bd  884643                mov       [esi+43h],al

LAB_0048c7c0:
  0048c7c0  8b542440              mov       edx,[esp+40h]
  0048c7c4  8b4c2414              mov       ecx,[esp+14h]
  0048c7c8  8bc2                  mov       eax,edx
  0048c7ca  c1f802                sar       eax,2
  0048c7cd  83c038                add       eax,38h
  0048c7d0  8d3c08                lea       edi,[eax+ecx]
  0048c7d3  8d1c00                lea       ebx,[eax+eax]
  0048c7d6  81e7ff0f0000          and       edi,0FFFh
  0048c7dc  3bfb                  cmp       edi,ebx
  0048c7de  7f06                  jg        short 0048C7E6h
  0048c7e0  66014e32              add       [esi+32h],cx
  0048c7e4  eb14                  jmp       short 0048C7FAh

LAB_0048c7e6:
  0048c7e6  81e1ff0f0000          and       ecx,0FFFh
  0048c7ec  81f900080000          cmp       ecx,800h
  0048c7f2  7c02                  jl        short 0048C7F6h
  0048c7f4  f7d8                  neg       eax

LAB_0048c7f6:
  0048c7f6  66014632              add       [esi+32h],ax

LAB_0048c7fa:
  0048c7fa  8b465c                mov       eax,[esi+5Ch]
  0048c7fd  81c2a0010000          add       edx,1A0h
  0048c803  c1e207                shl       edx,7
  0048c806  2bd0                  sub       edx,eax
  0048c808  c1fa04                sar       edx,4
  0048c80b  03d0                  add       edx,eax
  0048c80d  89565c                mov       [esi+5Ch],edx
  0048c810  eb6f                  jmp       short 0048C881h
  0048c812  8a4643                mov       al,[esi+43h]
  0048c815  84c0                  test      al,al
  0048c817  7405                  je        short 0048C81Eh
  0048c819  fec8                  dec       al
  0048c81b  884643                mov       [esi+43h],al

LAB_0048c81e:
  0048c81e  8b4c2440              mov       ecx,[esp+40h]
  0048c822  8b465c                mov       eax,[esi+5Ch]
  0048c825  8d9140010000          lea       edx,[ecx+140h]
  0048c82b  c1e207                shl       edx,7
  0048c82e  2bd0                  sub       edx,eax
  0048c830  c1fa04                sar       edx,4
  0048c833  03d0                  add       edx,eax
  0048c835  668b4610              mov       ax,[esi+10h]
  0048c839  89565c                mov       [esi+5Ch],edx
  0048c83c  668b5632              mov       dx,[esi+32h]
  0048c840  2bc2                  sub       eax,edx
  0048c842  25ff0f0000            and       eax,0FFFh
  0048c847  8db800010000          lea       edi,[eax+100h]
  0048c84d  81e7ff0f0000          and       edi,0FFFh
  0048c853  81ff00020000          cmp       edi,200h
  0048c859  7e26                  jle       short 0048C881h
  0048c85b  25ff0f0000            and       eax,0FFFh
  0048c860  3d00080000            cmp       eax,800h
  0048c865  7d0a                  jge       short 0048C871h
  0048c867  c1f902                sar       ecx,2
  0048c86a  83c138                add       ecx,38h
  0048c86d  8bc1                  mov       eax,ecx
  0048c86f  eb0a                  jmp       short 0048C87Bh

LAB_0048c871:
  0048c871  c1f902                sar       ecx,2
  0048c874  b8c8ffffff            mov       eax,0FFFFFFC8h
  0048c879  2bc1                  sub       eax,ecx

LAB_0048c87b:
  0048c87b  03d0                  add       edx,eax
  0048c87d  66895632              mov       [esi+32h],dx

LAB_0048c881:
  0048c881  0fbf4614              movsx     eax,word [esi+14h]
  0048c885  50                    push      eax
  0048c886  e891910b00            call      00545A1Ch
  0048c88b  668b4e28              mov       cx,[esi+28h]
  0048c88f  c1f802                sar       eax,2
  0048c892  0500100000            add       eax,1000h
  0048c897  0fbfd1                movsx     edx,cx
  0048c89a  8bf8                  mov       edi,eax
  0048c89c  2bfa                  sub       edi,edx
  0048c89e  c1ff02                sar       edi,2
  0048c8a1  03f9                  add       edi,ecx
  0048c8a3  668b4e2a              mov       cx,[esi+2Ah]
  0048c8a7  0fbfd1                movsx     edx,cx
  0048c8aa  66897e28              mov       [esi+28h],di
  0048c8ae  8bf8                  mov       edi,eax
  0048c8b0  2bfa                  sub       edi,edx
  0048c8b2  c1ff02                sar       edi,2
  0048c8b5  03f9                  add       edi,ecx
  0048c8b7  668b4e2c              mov       cx,[esi+2Ch]
  0048c8bb  0fbfd1                movsx     edx,cx
  0048c8be  2bc2                  sub       eax,edx
  0048c8c0  66897e2a              mov       [esi+2Ah],di
  0048c8c4  c1f802                sar       eax,2
  0048c8c7  03c1                  add       eax,ecx
  0048c8c9  6689462c              mov       [esi+2Ch],ax

LAB_0048c8cd:
  0048c8cd  8b44241c              mov       eax,[esp+1Ch]
  0048c8d1  50                    push      eax
  0048c8d2  56                    push      esi
  0048c8d3  e808e00000            call      0049A8E0h
  0048c8d8  83c408                add       esp,8
  0048c8db  56                    push      esi
  0048c8dc  e85f700a00            call      00533940h
  0048c8e1  8b466c                mov       eax,[esi+6Ch]
  0048c8e4  b301                  mov       bl,1
  0048c8e6  83c404                add       esp,4
  0048c8e9  8418                  test      [eax],bl
  0048c8eb  7506                  jne       short 0048C8F3h
  0048c8ed  8b4860                mov       ecx,[eax+60h]
  0048c8f0  894e48                mov       [esi+48h],ecx

LAB_0048c8f3:
  0048c8f3  e848a80b00            call      00547140h
  0048c8f8  8b7e60                mov       edi,[esi+60h]
  0048c8fb  25ff000000            and       eax,0FFh
  0048c900  0500010000            add       eax,100h
  0048c905  66014614              add       [esi+14h],ax
  0048c909  8b4648                mov       eax,[esi+48h]
  0048c90c  668b4e14              mov       cx,[esi+14h]
  0048c910  3bc7                  cmp       eax,edi
  0048c912  7c02                  jl        short 0048C916h
  0048c914  8bf8                  mov       edi,eax

LAB_0048c916:
  0048c916  f60602                test      byte [esi],2
  0048c919  7415                  je        short 0048C930h
  0048c91b  8b4620                mov       eax,[esi+20h]
  0048c91e  2bf8                  sub       edi,eax
  0048c920  81c700800200          add       edi,28000h
  0048c926  c1ff03                sar       edi,3
  0048c929  03f8                  add       edi,eax
  0048c92b  897e20                mov       [esi+20h],edi
  0048c92e  eb20                  jmp       short 0048C950h

LAB_0048c930:
  0048c930  8b6e20                mov       ebp,[esi+20h]
  0048c933  0fbfd1                movsx     edx,cx
  0048c936  52                    push      edx
  0048c937  e8e0900b00            call      00545A1Ch
  0048c93c  0500320000            add       eax,3200h
  0048c941  c1e006                shl       eax,6
  0048c944  03c7                  add       eax,edi
  0048c946  2bc5                  sub       eax,ebp
  0048c948  c1f804                sar       eax,4
  0048c94b  03c5                  add       eax,ebp
  0048c94d  894620                mov       [esi+20h],eax

LAB_0048c950:
  0048c950  0fbf4614              movsx     eax,word [esi+14h]
  0048c954  50                    push      eax
  0048c955  e8c2900b00            call      00545A1Ch
  0048c95a  8bc8                  mov       ecx,eax
  0048c95c  d1f9                  sar       ecx,1
  0048c95e  03c8                  add       ecx,eax
  0048c960  c1f906                sar       ecx,6
  0048c963  81c180000000          add       ecx,80h
  0048c969  81e1ff000000          and       ecx,0FFh
  0048c96f  8bd1                  mov       edx,ecx
  0048c971  c1e208                shl       edx,8
  0048c974  0bd1                  or        edx,ecx
  0048c976  c1e208                shl       edx,8
  0048c979  0bd1                  or        edx,ecx
  0048c97b  66837e160a            cmp       word [esi+16h],0Ah
  0048c980  895638                mov       [esi+38h],edx
  0048c983  7c0d                  jl        short 0048C992h
  0048c985  8b442420              mov       eax,[esp+20h]
  0048c989  50                    push      eax
  0048c98a  56                    push      esi
  0048c98b  e8e06d0500            call      004E3770h
  0048c990  eb1d                  jmp       short 0048C9AFh

LAB_0048c992:
  0048c992  8bbe90000000          mov       edi,[esi+90h]
  0048c998  8b4c2420              mov       ecx,[esp+20h]
  0048c99c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  0048c9a2  51                    push      ecx
  0048c9a3  56                    push      esi
  0048c9a4  89be90000000          mov       [esi+90h],edi
  0048c9aa  e841530100            call      004A1CF0h

LAB_0048c9af:
  0048c9af  8b442424              mov       eax,[esp+24h]
  0048c9b3  83c408                add       esp,8
  0048c9b6  85c0                  test      eax,eax
  0048c9b8  0f84ce010000          je        near 0048CB8Ch
  0048c9be  f70616080000          test      dword [esi],816h
  0048c9c4  0f85c2010000          jne       near 0048CB8Ch
  0048c9ca  8a4643                mov       al,[esi+43h]
  0048c9cd  84c0                  test      al,al
  0048c9cf  0f85b7010000          jne       near 0048CB8Ch
  0048c9d5  8b466c                mov       eax,[esi+6Ch]
  0048c9d8  8b561c                mov       edx,[esi+1Ch]
  0048c9db  8b781c                mov       edi,[eax+1Ch]
  0048c9de  8b6824                mov       ebp,[eax+24h]
  0048c9e1  8b4624                mov       eax,[esi+24h]
  0048c9e4  2bfa                  sub       edi,edx
  0048c9e6  2be8                  sub       ebp,eax
  0048c9e8  c1ff0c                sar       edi,0Ch
  0048c9eb  c1fd0c                sar       ebp,0Ch
  0048c9ee  8bd7                  mov       edx,edi
  0048c9f0  8bc5                  mov       eax,ebp
  0048c9f2  0fafd7                imul      edx,edi
  0048c9f5  0fafc5                imul      eax,ebp
  0048c9f8  03d0                  add       edx,eax
  0048c9fa  52                    push      edx
  0048c9fb  e810900b00            call      00545A10h
  0048ca00  55                    push      ebp
  0048ca01  57                    push      edi
  0048ca02  89442418              mov       [esp+18h],eax
  0048ca06  e817900b00            call      00545A22h
  0048ca0b  668b4e32              mov       cx,[esi+32h]
  0048ca0f  2bc1                  sub       eax,ecx
  0048ca11  25ff0f0000            and       eax,0FFFh
  0048ca16  89442414              mov       [esp+14h],eax
  0048ca1a  8b442410              mov       eax,[esp+10h]
  0048ca1e  3d2c010000            cmp       eax,12Ch
  0048ca23  0f8dbc000000          jge       near 0048CAE5h
  0048ca29  e812a70b00            call      00547140h
  0048ca2e  99                    cdq
  0048ca2f  b964000000            mov       ecx,64h
  0048ca34  f7f9                  idiv      ecx
  0048ca36  83fa3c                cmp       edx,3Ch
  0048ca39  7d5c                  jge       short 0048CA97h
  0048ca3b  8b542414              mov       edx,[esp+14h]
  0048ca3f  8b7c2418              mov       edi,[esp+18h]
  0048ca43  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0048ca4a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048ca51  25ff0f0000            and       eax,0FFFh
  0048ca56  3bc1                  cmp       eax,ecx
  0048ca58  0f8ff8000000          jg        near 0048CB56h
  0048ca5e  e8dda60b00            call      00547140h
  0048ca63  99                    cdq
  0048ca64  8d4c3f01              lea       ecx,[edi+edi+1]
  0048ca68  f7f9                  idiv      ecx
  0048ca6a  8b4c2410              mov       ecx,[esp+10h]
  0048ca6e  b8fa000000            mov       eax,0FAh
  0048ca73  2bc2                  sub       eax,edx
  0048ca75  3bc8                  cmp       ecx,eax
  0048ca77  0f8dd9000000          jge       near 0048CB56h
  0048ca7d  8b1e                  mov       ebx,[esi]
  0048ca7f  8bcf                  mov       ecx,edi
  0048ca81  c1f903                sar       ecx,3
  0048ca84  83cb02                or        ebx,2
  0048ca87  80c110                add       cl,10h
  0048ca8a  891e                  mov       [esi],ebx
  0048ca8c  884e43                mov       [esi+43h],cl
  0048ca8f  5f                    pop       edi
  0048ca90  5e                    pop       esi
  0048ca91  5d                    pop       ebp
  0048ca92  5b                    pop       ebx
  0048ca93  83c42c                add       esp,2Ch
  0048ca96  c3                    ret

LAB_0048ca97:
  0048ca97  c6464203              mov       byte [esi+42h],3
  0048ca9b  e8a0a60b00            call      00547140h
  0048caa0  99                    cdq
  0048caa1  b90a000000            mov       ecx,0Ah
  0048caa6  f7f9                  idiv      ecx
  0048caa8  8b442418              mov       eax,[esp+18h]
  0048caac  c1f802                sar       eax,2
  0048caaf  02d0                  add       dl,al
  0048cab1  02d1                  add       dl,cl
  0048cab3  8b4c2440              mov       ecx,[esp+40h]
  0048cab7  885643                mov       [esi+43h],dl
  0048caba  8db980000000          lea       edi,[ecx+80h]
  0048cac0  c1e704                shl       edi,4
  0048cac3  e878a60b00            call      00547140h
  0048cac8  99                    cdq
  0048cac9  f7ff                  idiv      edi
  0048cacb  8b4c2414              mov       ecx,[esp+14h]
  0048cacf  d1ff                  sar       edi,1
  0048cad1  2bd7                  sub       edx,edi
  0048cad3  03d1                  add       edx,ecx
  0048cad5  66035632              add       dx,[esi+32h]
  0048cad9  66895610              mov       [esi+10h],dx
  0048cadd  5f                    pop       edi
  0048cade  5e                    pop       esi
  0048cadf  5d                    pop       ebp
  0048cae0  5b                    pop       ebx
  0048cae1  83c42c                add       esp,2Ch
  0048cae4  c3                    ret

LAB_0048cae5:
  0048cae5  3d58020000            cmp       eax,258h
  0048caea  7d2c                  jge       short 0048CB18h
  0048caec  e84fa60b00            call      00547140h
  0048caf1  c6464202              mov       byte [esi+42h],2
  0048caf5  e846a60b00            call      00547140h
  0048cafa  99                    cdq
  0048cafb  b90a000000            mov       ecx,0Ah
  0048cb00  f7f9                  idiv      ecx
  0048cb02  8b442418              mov       eax,[esp+18h]
  0048cb06  c1f802                sar       eax,2
  0048cb09  02d0                  add       dl,al
  0048cb0b  02d1                  add       dl,cl
  0048cb0d  885643                mov       [esi+43h],dl
  0048cb10  5f                    pop       edi
  0048cb11  5e                    pop       esi
  0048cb12  5d                    pop       ebp
  0048cb13  5b                    pop       ebx
  0048cb14  83c42c                add       esp,2Ch
  0048cb17  c3                    ret

LAB_0048cb18:
  0048cb18  e823a60b00            call      00547140h
  0048cb1d  99                    cdq
  0048cb1e  b964000000            mov       ecx,64h
  0048cb23  f7f9                  idiv      ecx
  0048cb25  83fa1e                cmp       edx,1Eh
  0048cb28  7d27                  jge       short 0048CB51h
  0048cb2a  c6464200              mov       byte [esi+42h],0
  0048cb2e  e80da60b00            call      00547140h
  0048cb33  99                    cdq
  0048cb34  b90a000000            mov       ecx,0Ah
  0048cb39  f7f9                  idiv      ecx
  0048cb3b  8b442418              mov       eax,[esp+18h]
  0048cb3f  c1f802                sar       eax,2
  0048cb42  02d0                  add       dl,al
  0048cb44  02d1                  add       dl,cl
  0048cb46  885643                mov       [esi+43h],dl
  0048cb49  5f                    pop       edi
  0048cb4a  5e                    pop       esi
  0048cb4b  5d                    pop       ebp
  0048cb4c  5b                    pop       ebx
  0048cb4d  83c42c                add       esp,2Ch
  0048cb50  c3                    ret

LAB_0048cb51:
  0048cb51  83fa3c                cmp       edx,3Ch
  0048cb54  7d0e                  jge       short 0048CB64h

LAB_0048cb56:
  0048cb56  885e42                mov       [esi+42h],bl
  0048cb59  885e43                mov       [esi+43h],bl
  0048cb5c  5f                    pop       edi
  0048cb5d  5e                    pop       esi
  0048cb5e  5d                    pop       ebp
  0048cb5f  5b                    pop       ebx
  0048cb60  83c42c                add       esp,2Ch
  0048cb63  c3                    ret

LAB_0048cb64:
  0048cb64  c6464203              mov       byte [esi+42h],3
  0048cb68  e8d3a50b00            call      00547140h
  0048cb6d  99                    cdq
  0048cb6e  b90a000000            mov       ecx,0Ah
  0048cb73  f7f9                  idiv      ecx
  0048cb75  8b442418              mov       eax,[esp+18h]
  0048cb79  c1f802                sar       eax,2
  0048cb7c  02d0                  add       dl,al
  0048cb7e  02d1                  add       dl,cl
  0048cb80  885643                mov       [esi+43h],dl
  0048cb83  e8b8a50b00            call      00547140h
  0048cb88  66894610              mov       [esi+10h],ax

LAB_0048cb8c:
  0048cb8c  5f                    pop       edi
  0048cb8d  5e                    pop       esi
  0048cb8e  5d                    pop       ebp
  0048cb8f  5b                    pop       ebx
  0048cb90  83c42c                add       esp,2Ch
  0048cb93  c3                    ret
  0048cb94  12c7                  adc       al,bh
  0048cb96  48                    dec       eax
  0048cb97  0043c7                add       [ebx-39h],al
  0048cb9a  48                    dec       eax
  0048cb9b  00a1c7480012          add       [ecx+120048C7h],ah
  0048cba1  c8480090              enter     48h,90h
  0048cba5  90                    nop
  0048cba6  90                    nop
  0048cba7  90                    nop
  0048cba8  90                    nop
  0048cba9  90                    nop
  0048cbaa  90                    nop
  0048cbab  90                    nop
  0048cbac  90                    nop
  0048cbad  90                    nop
  0048cbae  90                    nop
  0048cbaf  90                    nop

32 cible(s) de saut interne : 0x0048c5de, 0x0048c62e, 0x0048c68c, 0x0048c6bf, 0x0048c6ea, 0x0048c725, 0x0048c77b, 0x0048c78b, 0x0048c78f, 0x0048c7b4, 0x0048c7c0, 0x0048c7e6, 0x0048c7f6, 0x0048c7fa, 0x0048c81e, 0x0048c871, 0x0048c87b, 0x0048c881, 0x0048c8cd, 0x0048c8f3, 0x0048c916, 0x0048c930, 0x0048c950, 0x0048c992, 0x0048c9af, 0x0048ca97, 0x0048cae5, 0x0048cb18, 0x0048cb51, 0x0048cb56, 0x0048cb64, 0x0048cb8c
