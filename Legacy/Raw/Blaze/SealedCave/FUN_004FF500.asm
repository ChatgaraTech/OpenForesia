Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004ff500  section .text  offset fichier 0xfe900  1872 octets

  004ff500  83ec2c                sub       esp,2Ch
  004ff503  53                    push      ebx
  004ff504  55                    push      ebp
  004ff505  56                    push      esi
  004ff506  8b74243c              mov       esi,[esp+3Ch]
  004ff50a  8d442424              lea       eax,[esp+24h]
  004ff50e  57                    push      edi
  004ff50f  8d4c242c              lea       ecx,[esp+2Ch]
  004ff513  50                    push      eax
  004ff514  8d542428              lea       edx,[esp+28h]
  004ff518  51                    push      ecx
  004ff519  33ed                  xor       ebp,ebp
  004ff51b  52                    push      edx
  004ff51c  56                    push      esi
  004ff51d  66896c2440            mov       [esp+40h],bp
  004ff522  66c74424421e00        mov       word [esp+42h],1Eh
  004ff529  66c74424443200        mov       word [esp+44h],32h
  004ff530  66c74424466400        mov       word [esp+46h],64h
  004ff537  66896c2448            mov       [esp+48h],bp
  004ff53c  66c744244a4600        mov       word [esp+4Ah],46h
  004ff543  e838940300            call      00538980h
  004ff548  83c410                add       esp,10h
  004ff54b  8d442420              lea       eax,[esp+20h]
  004ff54f  8d4c241c              lea       ecx,[esp+1Ch]
  004ff553  8d542410              lea       edx,[esp+10h]
  004ff557  50                    push      eax
  004ff558  51                    push      ecx
  004ff559  8d442420              lea       eax,[esp+20h]
  004ff55d  52                    push      edx
  004ff55e  8d4c244c              lea       ecx,[esp+4Ch]
  004ff562  50                    push      eax
  004ff563  51                    push      ecx
  004ff564  56                    push      esi
  004ff565  e8b6690300            call      00535F20h
  004ff56a  8b4c2458              mov       ecx,[esp+58h]
  004ff56e  b840000000            mov       eax,40h
  004ff573  2bc1                  sub       eax,ecx
  004ff575  83c418                add       esp,18h
  004ff578  89442414              mov       [esp+14h],eax
  004ff57c  8b44241c              mov       eax,[esp+1Ch]
  004ff580  3bc5                  cmp       eax,ebp
  004ff582  bf01000000            mov       edi,1
  004ff587  0f844f030000          je        near 004FF8DCh
  004ff58d  56                    push      esi
  004ff58e  e80dc0fdff            call      004DB5A0h
  004ff593  8b06                  mov       eax,[esi]
  004ff595  83c404                add       esp,4
  004ff598  a802                  test      al,2
  004ff59a  0f84cf000000          je        near 004FF66Fh
  004ff5a0  8a4c2420              mov       cl,[esp+20h]
  004ff5a4  bb10000000            mov       ebx,10h
  004ff5a9  84cb                  test      bl,cl
  004ff5ab  740d                  je        short 004FF5BAh
  004ff5ad  24fd                  and       al,0FDh
  004ff5af  c6464300              mov       byte [esi+43h],0
  004ff5b3  8906                  mov       [esi],eax
  004ff5b5  e922030000            jmp       004FF8DCh

LAB_004ff5ba:
  004ff5ba  33c9                  xor       ecx,ecx
  004ff5bc  8a4e42                mov       cl,[esi+42h]
  004ff5bf  2bcd                  sub       ecx,ebp
  004ff5c1  7432                  je        short 004FF5F5h
  004ff5c3  49                    dec       ecx
  004ff5c4  0f8512030000          jne       near 004FF8DCh
  004ff5ca  66837e1a07            cmp       word [esi+1Ah],7
  004ff5cf  750e                  jne       short 004FF5DFh
  004ff5d1  6aff                  push      0FFFFFFFFh
  004ff5d3  55                    push      ebp
  004ff5d4  6a49                  push      49h
  004ff5d6  56                    push      esi
  004ff5d7  e8f4b3f7ff            call      0047A9D0h
  004ff5dc  83c410                add       esp,10h

LAB_004ff5df:
  004ff5df  8b06                  mov       eax,[esi]
  004ff5e1  a900000002            test      eax,2000000h
  004ff5e6  0f84f0020000          je        near 004FF8DCh
  004ff5ec  24fd                  and       al,0FDh
  004ff5ee  8906                  mov       [esi],eax
  004ff5f0  e9e7020000            jmp       004FF8DCh

LAB_004ff5f5:
  004ff5f5  66837e1a09            cmp       word [esi+1Ah],9
  004ff5fa  7557                  jne       short 004FF653h
  004ff5fc  8d542430              lea       edx,[esp+30h]
  004ff600  55                    push      ebp
  004ff601  8d461c                lea       eax,[esi+1Ch]
  004ff604  52                    push      edx
  004ff605  50                    push      eax
  004ff606  56                    push      esi
  004ff607  e824a60100            call      00519C30h
  004ff60c  8bf8                  mov       edi,eax
  004ff60e  a1b8e05d00            mov       eax,[5DE0B8h]
  004ff613  83c410                add       esp,10h
  004ff616  3bc5                  cmp       eax,ebp
  004ff618  742f                  je        short 004FF649h
  004ff61a  57                    push      edi
  004ff61b  56                    push      esi
  004ff61c  e85fa50300            call      00539B80h
  004ff621  8b07                  mov       eax,[edi]
  004ff623  83c408                add       esp,8
  004ff626  0c40                  or        al,40h
  004ff628  66899f96000000        mov       [edi+96h],bx
  004ff62f  8907                  mov       [edi],eax
  004ff631  83c71c                add       edi,1Ch
  004ff634  57                    push      edi
  004ff635  6800180000            push      1800h
  004ff63a  6800030000            push      300h
  004ff63f  6a03                  push      3
  004ff641  e8dae40300            call      0053DB20h
  004ff646  83c410                add       esp,10h

LAB_004ff649:
  004ff649  8b06                  mov       eax,[esi]
  004ff64b  a801                  test      al,1
  004ff64d  740c                  je        short 004FF65Bh
  004ff64f  66ff4e1a              dec       word [esi+1Ah]

LAB_004ff653:
  004ff653  a801                  test      al,1
  004ff655  0f8581020000          jne       near 004FF8DCh

LAB_004ff65b:
  004ff65b  a900000002            test      eax,2000000h
  004ff660  0f8473020000          je        near 004FF8D9h
  004ff666  24fd                  and       al,0FDh
  004ff668  8906                  mov       [esi],eax
  004ff66a  e96a020000            jmp       004FF8D9h

LAB_004ff66f:
  004ff66f  a810                  test      al,10h
  004ff671  0f8565020000          jne       near 004FF8DCh
  004ff677  a904080000            test      eax,804h
  004ff67c  0f855a020000          jne       near 004FF8DCh
  004ff682  a801                  test      al,1
  004ff684  7439                  je        short 004FF6BFh
  004ff686  e8b57a0400            call      00547140h
  004ff68b  99                    cdq
  004ff68c  b964000000            mov       ecx,64h
  004ff691  f7f9                  idiv      ecx
  004ff693  8b442440              mov       eax,[esp+40h]
  004ff697  c1f802                sar       eax,2
  004ff69a  3bd0                  cmp       edx,eax
  004ff69c  0f8d3a020000          jge       near 004FF8DCh
  004ff6a2  8b0e                  mov       ecx,[esi]
  004ff6a4  c7461803000000        mov       dword [esi+18h],3
  004ff6ab  81e1fffffffe          and       ecx,0FEFFFFFFh
  004ff6b1  c6464200              mov       byte [esi+42h],0
  004ff6b5  83c902                or        ecx,2
  004ff6b8  890e                  mov       [esi],ecx
  004ff6ba  e91a020000            jmp       004FF8D9h

LAB_004ff6bf:
  004ff6bf  33c9                  xor       ecx,ecx
  004ff6c1  8a4e42                mov       cl,[esi+42h]
  004ff6c4  83f904                cmp       ecx,4
  004ff6c7  0f870f020000          ja        near 004FF8DCh
  004ff6cd  ff248d34fc4f00        jmp       dword [ecx*4+4FFC34h]
  004ff6d4  66396e18              cmp       [esi+18h],bp
  004ff6d8  740a                  je        short 004FF6E4h
  004ff6da  0d00000001            or        eax,1000000h
  004ff6df  896e18                mov       [esi+18h],ebp
  004ff6e2  8906                  mov       [esi],eax

LAB_004ff6e4:
  004ff6e4  817c24182c010000      cmp       dword [esp+18h],12Ch
  004ff6ec  7d09                  jge       short 004FF6F7h
  004ff6ee  c6464300              mov       byte [esi+43h],0
  004ff6f2  e9e5010000            jmp       004FF8DCh

LAB_004ff6f7:
  004ff6f7  8a4643                mov       al,[esi+43h]
  004ff6fa  84c0                  test      al,al
  004ff6fc  0f84d7010000          je        near 004FF8D9h
  004ff702  fec8                  dec       al
  004ff704  884643                mov       [esi+43h],al
  004ff707  e9cd010000            jmp       004FF8D9h
  004ff70c  66397e18              cmp       [esi+18h],di
  004ff710  740a                  je        short 004FF71Ch
  004ff712  0d00000001            or        eax,1000000h
  004ff717  897e18                mov       [esi+18h],edi
  004ff71a  8906                  mov       [esi],eax

LAB_004ff71c:
  004ff71c  8b542440              mov       edx,[esp+40h]
  004ff720  8b4c2410              mov       ecx,[esp+10h]
  004ff724  8d841280000000        lea       eax,[edx+edx+80h]
  004ff72b  8d1408                lea       edx,[eax+ecx]
  004ff72e  8d3c00                lea       edi,[eax+eax]
  004ff731  81e2ff0f0000          and       edx,0FFFh
  004ff737  3bd7                  cmp       edx,edi
  004ff739  0f8f20010000          jg        near 004FF85Fh
  004ff73f  66014e32              add       [esi+32h],cx
  004ff743  c6464300              mov       byte [esi+43h],0
  004ff747  e98d010000            jmp       004FF8D9h
  004ff74c  66397e18              cmp       [esi+18h],di
  004ff750  740a                  je        short 004FF75Ch
  004ff752  0d00000001            or        eax,1000000h
  004ff757  897e18                mov       [esi+18h],edi
  004ff75a  8906                  mov       [esi],eax

LAB_004ff75c:
  004ff75c  0fbf4610              movsx     eax,word [esi+10h]
  004ff760  39442418              cmp       [esp+18h],eax
  004ff764  7d09                  jge       short 004FF76Fh
  004ff766  c6464300              mov       byte [esi+43h],0
  004ff76a  e96d010000            jmp       004FF8DCh

LAB_004ff76f:
  004ff76f  8a4643                mov       al,[esi+43h]
  004ff772  84c0                  test      al,al
  004ff774  7405                  je        short 004FF77Bh
  004ff776  fec8                  dec       al
  004ff778  884643                mov       [esi+43h],al

LAB_004ff77b:
  004ff77b  8b542440              mov       edx,[esp+40h]
  004ff77f  8b4c2410              mov       ecx,[esp+10h]
  004ff783  8d841280000000        lea       eax,[edx+edx+80h]
  004ff78a  8d3c08                lea       edi,[eax+ecx]
  004ff78d  8d1c00                lea       ebx,[eax+eax]
  004ff790  81e7ff0f0000          and       edi,0FFFh
  004ff796  3bfb                  cmp       edi,ebx
  004ff798  7f15                  jg        short 004FF7AFh
  004ff79a  66014e32              add       [esi+32h],cx
  004ff79e  81c2c0000000          add       edx,0C0h
  004ff7a4  c1e207                shl       edx,7
  004ff7a7  89565c                mov       [esi+5Ch],edx
  004ff7aa  e92d010000            jmp       004FF8DCh

LAB_004ff7af:
  004ff7af  81e1ff0f0000          and       ecx,0FFFh
  004ff7b5  81f900080000          cmp       ecx,800h
  004ff7bb  7c02                  jl        short 004FF7BFh
  004ff7bd  f7d8                  neg       eax

LAB_004ff7bf:
  004ff7bf  66014632              add       [esi+32h],ax
  004ff7c3  81c2c0000000          add       edx,0C0h
  004ff7c9  c1e207                shl       edx,7
  004ff7cc  89565c                mov       [esi+5Ch],edx
  004ff7cf  e908010000            jmp       004FF8DCh
  004ff7d4  8a4e43                mov       cl,[esi+43h]
  004ff7d7  84c9                  test      cl,cl
  004ff7d9  7405                  je        short 004FF7E0h
  004ff7db  fec9                  dec       cl
  004ff7dd  884e43                mov       [esi+43h],cl

LAB_004ff7e0:
  004ff7e0  66397e18              cmp       [esi+18h],di
  004ff7e4  740a                  je        short 004FF7F0h
  004ff7e6  0d00000001            or        eax,1000000h
  004ff7eb  897e18                mov       [esi+18h],edi
  004ff7ee  8906                  mov       [esi],eax

LAB_004ff7f0:
  004ff7f0  8b4c2440              mov       ecx,[esp+40h]
  004ff7f4  8b7c241c              mov       edi,[esp+1Ch]
  004ff7f8  0fbf5610              movsx     edx,word [esi+10h]
  004ff7fc  8d8180000000          lea       eax,[ecx+80h]
  004ff802  52                    push      edx
  004ff803  c1e007                shl       eax,7
  004ff806  0faff8                imul      edi,eax
  004ff809  89465c                mov       [esi+5Ch],eax
  004ff80c  c1ff0c                sar       edi,0Ch
  004ff80f  e808620400            call      00545A1Ch
  004ff814  0fafc7                imul      eax,edi
  004ff817  8b5e1c                mov       ebx,[esi+1Ch]
  004ff81a  c1f80c                sar       eax,0Ch
  004ff81d  03d8                  add       ebx,eax
  004ff81f  0fbf4610              movsx     eax,word [esi+10h]
  004ff823  50                    push      eax
  004ff824  895e1c                mov       [esi+1Ch],ebx
  004ff827  e8ea610400            call      00545A16h
  004ff82c  0fafc7                imul      eax,edi
  004ff82f  8b5e24                mov       ebx,[esi+24h]
  004ff832  8b4c2440              mov       ecx,[esp+40h]
  004ff836  c1f80c                sar       eax,0Ch
  004ff839  03d8                  add       ebx,eax
  004ff83b  8d048d32000000        lea       eax,[ecx*4+32h]
  004ff842  8b4c2410              mov       ecx,[esp+10h]
  004ff846  895e24                mov       [esi+24h],ebx
  004ff849  8d3c00                lea       edi,[eax+eax]
  004ff84c  8d1408                lea       edx,[eax+ecx]
  004ff84f  81e2ff0f0000          and       edx,0FFFh
  004ff855  3bd7                  cmp       edx,edi
  004ff857  7f06                  jg        short 004FF85Fh
  004ff859  66014e32              add       [esi+32h],cx
  004ff85d  eb7a                  jmp       short 004FF8D9h

LAB_004ff85f:
  004ff85f  81e1ff0f0000          and       ecx,0FFFh
  004ff865  81f900080000          cmp       ecx,800h
  004ff86b  7c02                  jl        short 004FF86Fh
  004ff86d  f7d8                  neg       eax

LAB_004ff86f:
  004ff86f  66014632              add       [esi+32h],ax
  004ff873  eb64                  jmp       short 004FF8D9h
  004ff875  25fffffffe            and       eax,0FEFFFFFFh
  004ff87a  c7461804000000        mov       dword [esi+18h],4
  004ff881  0bc7                  or        eax,edi
  004ff883  8b7c241c              mov       edi,[esp+1Ch]
  004ff887  8906                  mov       [esi],eax
  004ff889  8b442440              mov       eax,[esp+40h]
  004ff88d  05c0000000            add       eax,0C0h
  004ff892  c1e007                shl       eax,7
  004ff895  0fbf4e10              movsx     ecx,word [esi+10h]
  004ff899  0faff8                imul      edi,eax
  004ff89c  51                    push      ecx
  004ff89d  89465c                mov       [esi+5Ch],eax
  004ff8a0  c1ff0c                sar       edi,0Ch
  004ff8a3  e874610400            call      00545A1Ch
  004ff8a8  0fafc7                imul      eax,edi
  004ff8ab  0fbf5610              movsx     edx,word [esi+10h]
  004ff8af  8b4e50                mov       ecx,[esi+50h]
  004ff8b2  52                    push      edx
  004ff8b3  c1f80c                sar       eax,0Ch
  004ff8b6  03c8                  add       ecx,eax
  004ff8b8  894e50                mov       [esi+50h],ecx
  004ff8bb  e856610400            call      00545A16h
  004ff8c0  0fafc7                imul      eax,edi
  004ff8c3  8b4e58                mov       ecx,[esi+58h]
  004ff8c6  c1f80c                sar       eax,0Ch
  004ff8c9  03c8                  add       ecx,eax
  004ff8cb  8b4654                mov       eax,[esi+54h]
  004ff8ce  0500900100            add       eax,19000h
  004ff8d3  894e58                mov       [esi+58h],ecx
  004ff8d6  894654                mov       [esi+54h],eax

LAB_004ff8d9:
  004ff8d9  896e5c                mov       [esi+5Ch],ebp

LAB_004ff8dc:
  004ff8dc  8b44241c              mov       eax,[esp+1Ch]
  004ff8e0  50                    push      eax
  004ff8e1  56                    push      esi
  004ff8e2  e87988f8ff            call      00488160h
  004ff8e7  83c408                add       esp,8
  004ff8ea  56                    push      esi
  004ff8eb  e850400300            call      00533940h
  004ff8f0  83c404                add       esp,4
  004ff8f3  56                    push      esi
  004ff8f4  e8f701feff            call      004DFAF0h
  004ff8f9  8b4c2428              mov       ecx,[esp+28h]
  004ff8fd  83c404                add       esp,4
  004ff900  51                    push      ecx
  004ff901  56                    push      esi
  004ff902  e8693efeff            call      004E3770h
  004ff907  8b442424              mov       eax,[esp+24h]
  004ff90b  83c408                add       esp,8
  004ff90e  3bc5                  cmp       eax,ebp
  004ff910  0f8413030000          je        near 004FFC29h
  004ff916  f70617080000          test      dword [esi],817h
  004ff91c  0f8507030000          jne       near 004FFC29h
  004ff922  8a4643                mov       al,[esi+43h]
  004ff925  84c0                  test      al,al
  004ff927  0f85fc020000          jne       near 004FFC29h
  004ff92d  8b466c                mov       eax,[esi+6Ch]
  004ff930  8b5e1c                mov       ebx,[esi+1Ch]
  004ff933  8b4e24                mov       ecx,[esi+24h]
  004ff936  8b781c                mov       edi,[eax+1Ch]
  004ff939  8b6824                mov       ebp,[eax+24h]
  004ff93c  2bfb                  sub       edi,ebx
  004ff93e  2be9                  sub       ebp,ecx
  004ff940  c1ff0c                sar       edi,0Ch
  004ff943  c1fd0c                sar       ebp,0Ch
  004ff946  8bd7                  mov       edx,edi
  004ff948  8bc5                  mov       eax,ebp
  004ff94a  0fafd7                imul      edx,edi
  004ff94d  0fafc5                imul      eax,ebp
  004ff950  03d0                  add       edx,eax
  004ff952  52                    push      edx
  004ff953  e8b8600400            call      00545A10h
  004ff958  55                    push      ebp
  004ff959  57                    push      edi
  004ff95a  89442420              mov       [esp+20h],eax
  004ff95e  e8bf600400            call      00545A22h
  004ff963  668b4e32              mov       cx,[esi+32h]
  004ff967  2bc1                  sub       eax,ecx
  004ff969  25ff0f0000            and       eax,0FFFh
  004ff96e  89442410              mov       [esp+10h],eax
  004ff972  8b442418              mov       eax,[esp+18h]
  004ff976  3dfa000000            cmp       eax,0FAh
  004ff97b  0f8dfd000000          jge       near 004FFA7Eh
  004ff981  e8ba770400            call      00547140h
  004ff986  99                    cdq
  004ff987  b964000000            mov       ecx,64h
  004ff98c  f7f9                  idiv      ecx
  004ff98e  83fa2d                cmp       edx,2Dh
  004ff991  7d65                  jge       short 004FF9F8h
  004ff993  8b542410              mov       edx,[esp+10h]
  004ff997  8b7c2414              mov       edi,[esp+14h]
  004ff99b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ff9a2  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ff9a9  25ff0f0000            and       eax,0FFFh
  004ff9ae  3bc1                  cmp       eax,ecx
  004ff9b0  0f8f0f020000          jg        near 004FFBC5h
  004ff9b6  e885770400            call      00547140h
  004ff9bb  99                    cdq
  004ff9bc  8d4c3f01              lea       ecx,[edi+edi+1]
  004ff9c0  f7f9                  idiv      ecx
  004ff9c2  8b442418              mov       eax,[esp+18h]
  004ff9c6  83c264                add       edx,64h
  004ff9c9  3bc2                  cmp       eax,edx
  004ff9cb  0f8df4010000          jge       near 004FFBC5h
  004ff9d1  8b16                  mov       edx,[esi]
  004ff9d3  c7461803000000        mov       dword [esi+18h],3
  004ff9da  81e2fffffffe          and       edx,0FEFFFFFFh
  004ff9e0  c6464200              mov       byte [esi+42h],0
  004ff9e4  83ca02                or        edx,2
  004ff9e7  c7465c00000000        mov       dword [esi+5Ch],0
  004ff9ee  8916                  mov       [esi],edx
  004ff9f0  5f                    pop       edi
  004ff9f1  5e                    pop       esi
  004ff9f2  5d                    pop       ebp
  004ff9f3  5b                    pop       ebx
  004ff9f4  83c42c                add       esp,2Ch
  004ff9f7  c3                    ret

LAB_004ff9f8:
  004ff9f8  83fa3c                cmp       edx,3Ch
  004ff9fb  0f8c2c010000          jl        near 004FFB2Dh
  004ffa01  83fa4b                cmp       edx,4Bh
  004ffa04  7d24                  jge       short 004FFA2Ah
  004ffa06  8b4c2440              mov       ecx,[esp+40h]
  004ffa0a  8b442418              mov       eax,[esp+18h]
  004ffa0e  83c164                add       ecx,64h
  004ffa11  3bc1                  cmp       eax,ecx
  004ffa13  7e15                  jle       short 004FFA2Ah
  004ffa15  c6464204              mov       byte [esi+42h],4
  004ffa19  e822770400            call      00547140h
  004ffa1e  66894610              mov       [esi+10h],ax
  004ffa22  5f                    pop       edi
  004ffa23  5e                    pop       esi
  004ffa24  5d                    pop       ebp
  004ffa25  5b                    pop       ebx
  004ffa26  83c42c                add       esp,2Ch
  004ffa29  c3                    ret

LAB_004ffa2a:
  004ffa2a  c6464203              mov       byte [esi+42h],3
  004ffa2e  e80d770400            call      00547140h
  004ffa33  99                    cdq
  004ffa34  b90a000000            mov       ecx,0Ah
  004ffa39  f7f9                  idiv      ecx
  004ffa3b  8b442414              mov       eax,[esp+14h]
  004ffa3f  8b4c2440              mov       ecx,[esp+40h]
  004ffa43  d1f8                  sar       eax,1
  004ffa45  8db980000000          lea       edi,[ecx+80h]
  004ffa4b  c1e704                shl       edi,4
  004ffa4e  02d0                  add       dl,al
  004ffa50  80c214                add       dl,14h
  004ffa53  885643                mov       [esi+43h],dl
  004ffa56  e8e5760400            call      00547140h
  004ffa5b  99                    cdq
  004ffa5c  f7ff                  idiv      edi
  004ffa5e  d1ff                  sar       edi,1
  004ffa60  2bd7                  sub       edx,edi
  004ffa62  8b7c2410              mov       edi,[esp+10h]
  004ffa66  03d7                  add       edx,edi
  004ffa68  66035632              add       dx,[esi+32h]
  004ffa6c  81c200080000          add       edx,800h
  004ffa72  66895610              mov       [esi+10h],dx
  004ffa76  5f                    pop       edi
  004ffa77  5e                    pop       esi
  004ffa78  5d                    pop       ebp
  004ffa79  5b                    pop       ebx
  004ffa7a  83c42c                add       esp,2Ch
  004ffa7d  c3                    ret

LAB_004ffa7e:
  004ffa7e  3df4010000            cmp       eax,1F4h
  004ffa83  0f8dd1000000          jge       near 004FFB5Ah
  004ffa89  e8b2760400            call      00547140h
  004ffa8e  99                    cdq
  004ffa8f  b964000000            mov       ecx,64h
  004ffa94  f7f9                  idiv      ecx
  004ffa96  8b442440              mov       eax,[esp+40h]
  004ffa9a  d1f8                  sar       eax,1
  004ffa9c  83c008                add       eax,8
  004ffa9f  3bd0                  cmp       edx,eax
  004ffaa1  7d2e                  jge       short 004FFAD1h
  004ffaa3  8b4c2410              mov       ecx,[esp+10h]
  004ffaa7  81c100040000          add       ecx,400h
  004ffaad  81e1ff0f0000          and       ecx,0FFFh
  004ffab3  81f900080000          cmp       ecx,800h
  004ffab9  0f8d06010000          jge       near 004FFBC5h
  004ffabf  8b16                  mov       edx,[esi]
  004ffac1  81e2fffffffe          and       edx,0FEFFFFFFh
  004ffac7  83ca02                or        edx,2
  004ffaca  8916                  mov       [esi],edx
  004ffacc  e9da000000            jmp       004FFBABh

LAB_004ffad1:
  004ffad1  83fa46                cmp       edx,46h
  004ffad4  7d4e                  jge       short 004FFB24h
  004ffad6  c6464203              mov       byte [esi+42h],3
  004ffada  e861760400            call      00547140h
  004ffadf  99                    cdq
  004ffae0  b90a000000            mov       ecx,0Ah
  004ffae5  f7f9                  idiv      ecx
  004ffae7  8b442414              mov       eax,[esp+14h]
  004ffaeb  8b4c2440              mov       ecx,[esp+40h]
  004ffaef  d1f8                  sar       eax,1
  004ffaf1  8db980000000          lea       edi,[ecx+80h]
  004ffaf7  c1e704                shl       edi,4
  004ffafa  02d0                  add       dl,al
  004ffafc  80c214                add       dl,14h
  004ffaff  885643                mov       [esi+43h],dl
  004ffb02  e839760400            call      00547140h
  004ffb07  99                    cdq
  004ffb08  f7ff                  idiv      edi
  004ffb0a  8b4c2410              mov       ecx,[esp+10h]
  004ffb0e  d1ff                  sar       edi,1
  004ffb10  2bd7                  sub       edx,edi
  004ffb12  03d1                  add       edx,ecx
  004ffb14  66035632              add       dx,[esi+32h]
  004ffb18  66895610              mov       [esi+10h],dx
  004ffb1c  5f                    pop       edi
  004ffb1d  5e                    pop       esi
  004ffb1e  5d                    pop       ebp
  004ffb1f  5b                    pop       ebx
  004ffb20  83c42c                add       esp,2Ch
  004ffb23  c3                    ret

LAB_004ffb24:
  004ffb24  83fa5a                cmp       edx,5Ah
  004ffb27  0f8dad000000          jge       near 004FFBDAh

LAB_004ffb2d:
  004ffb2d  c6464202              mov       byte [esi+42h],2
  004ffb31  e80a760400            call      00547140h
  004ffb36  99                    cdq
  004ffb37  b90a000000            mov       ecx,0Ah
  004ffb3c  66c746106400          mov       word [esi+10h],64h
  004ffb42  f7f9                  idiv      ecx
  004ffb44  8b442414              mov       eax,[esp+14h]
  004ffb48  d1f8                  sar       eax,1
  004ffb4a  02d0                  add       dl,al
  004ffb4c  80c214                add       dl,14h
  004ffb4f  885643                mov       [esi+43h],dl
  004ffb52  5f                    pop       edi
  004ffb53  5e                    pop       esi
  004ffb54  5d                    pop       ebp
  004ffb55  5b                    pop       ebx
  004ffb56  83c42c                add       esp,2Ch
  004ffb59  c3                    ret

LAB_004ffb5a:
  004ffb5a  e8e1750400            call      00547140h
  004ffb5f  99                    cdq
  004ffb60  b964000000            mov       ecx,64h
  004ffb65  f7f9                  idiv      ecx
  004ffb67  8b442440              mov       eax,[esp+40h]
  004ffb6b  d1f8                  sar       eax,1
  004ffb6d  83c008                add       eax,8
  004ffb70  3bd0                  cmp       edx,eax
  004ffb72  7d61                  jge       short 004FFBD5h
  004ffb74  817c2418bc020000      cmp       dword [esp+18h],2BCh
  004ffb7c  7d47                  jge       short 004FFBC5h
  004ffb7e  8b4c2410              mov       ecx,[esp+10h]
  004ffb82  8b442414              mov       eax,[esp+14h]
  004ffb86  8d948100010000        lea       edx,[ecx+eax*4+100h]
  004ffb8d  8d04c500020000        lea       eax,[eax*8+200h]
  004ffb94  81e2ff0f0000          and       edx,0FFFh
  004ffb9a  3bd0                  cmp       edx,eax
  004ffb9c  7f27                  jg        short 004FFBC5h
  004ffb9e  8b0e                  mov       ecx,[esi]
  004ffba0  81e1fffffffe          and       ecx,0FEFFFFFFh
  004ffba6  83c902                or        ecx,2
  004ffba9  890e                  mov       [esi],ecx

LAB_004ffbab:
  004ffbab  c7461803000000        mov       dword [esi+18h],3
  004ffbb2  c6464201              mov       byte [esi+42h],1
  004ffbb6  c7465c00000000        mov       dword [esi+5Ch],0
  004ffbbd  5f                    pop       edi
  004ffbbe  5e                    pop       esi
  004ffbbf  5d                    pop       ebp
  004ffbc0  5b                    pop       ebx
  004ffbc1  83c42c                add       esp,2Ch
  004ffbc4  c3                    ret

LAB_004ffbc5:
  004ffbc5  c6464201              mov       byte [esi+42h],1
  004ffbc9  c6464301              mov       byte [esi+43h],1
  004ffbcd  5f                    pop       edi
  004ffbce  5e                    pop       esi
  004ffbcf  5d                    pop       ebp
  004ffbd0  5b                    pop       ebx
  004ffbd1  83c42c                add       esp,2Ch
  004ffbd4  c3                    ret

LAB_004ffbd5:
  004ffbd5  83fa3c                cmp       edx,3Ch
  004ffbd8  7d27                  jge       short 004FFC01h

LAB_004ffbda:
  004ffbda  c6464200              mov       byte [esi+42h],0
  004ffbde  e85d750400            call      00547140h
  004ffbe3  99                    cdq
  004ffbe4  b90a000000            mov       ecx,0Ah
  004ffbe9  f7f9                  idiv      ecx
  004ffbeb  8b442414              mov       eax,[esp+14h]
  004ffbef  d1f8                  sar       eax,1
  004ffbf1  02d0                  add       dl,al
  004ffbf3  80c214                add       dl,14h
  004ffbf6  885643                mov       [esi+43h],dl
  004ffbf9  5f                    pop       edi
  004ffbfa  5e                    pop       esi
  004ffbfb  5d                    pop       ebp
  004ffbfc  5b                    pop       ebx
  004ffbfd  83c42c                add       esp,2Ch
  004ffc00  c3                    ret

LAB_004ffc01:
  004ffc01  c6464203              mov       byte [esi+42h],3
  004ffc05  e836750400            call      00547140h
  004ffc0a  99                    cdq
  004ffc0b  b90a000000            mov       ecx,0Ah
  004ffc10  f7f9                  idiv      ecx
  004ffc12  8b442414              mov       eax,[esp+14h]
  004ffc16  d1f8                  sar       eax,1
  004ffc18  02d0                  add       dl,al
  004ffc1a  80c214                add       dl,14h
  004ffc1d  885643                mov       [esi+43h],dl
  004ffc20  e81b750400            call      00547140h
  004ffc25  66894610              mov       [esi+10h],ax

LAB_004ffc29:
  004ffc29  5f                    pop       edi
  004ffc2a  5e                    pop       esi
  004ffc2b  5d                    pop       ebp
  004ffc2c  5b                    pop       ebx
  004ffc2d  83c42c                add       esp,2Ch
  004ffc30  c3                    ret
  004ffc31  90                    nop
  004ffc32  8bff                  mov       edi,edi
  004ffc34  d4f6                  aam       0F6h
  004ffc36  4f                    dec       edi
  004ffc37  000cf7                add       [edi+esi*8],cl
  004ffc3a  4f                    dec       edi
  004ffc3b  004cf74f              add       [edi+esi*8+4Fh],cl
  004ffc3f  00d4                  add       ah,dl
  004ffc41  f74f0075f84f00        test      dword [edi],4FF875h
  004ffc48  90                    nop
  004ffc49  90                    nop
  004ffc4a  90                    nop
  004ffc4b  90                    nop
  004ffc4c  90                    nop
  004ffc4d  90                    nop
  004ffc4e  90                    nop
  004ffc4f  90                    nop

35 cible(s) de saut interne : 0x004ff5ba, 0x004ff5df, 0x004ff5f5, 0x004ff649, 0x004ff653, 0x004ff65b, 0x004ff66f, 0x004ff6bf, 0x004ff6e4, 0x004ff6f7, 0x004ff71c, 0x004ff75c, 0x004ff76f, 0x004ff77b, 0x004ff7af, 0x004ff7bf, 0x004ff7e0, 0x004ff7f0, 0x004ff85f, 0x004ff86f, 0x004ff8d9, 0x004ff8dc, 0x004ff9f8, 0x004ffa2a, 0x004ffa7e, 0x004ffad1, 0x004ffb24, 0x004ffb2d, 0x004ffb5a, 0x004ffbab, 0x004ffbc5, 0x004ffbd5, 0x004ffbda, 0x004ffc01, 0x004ffc29
