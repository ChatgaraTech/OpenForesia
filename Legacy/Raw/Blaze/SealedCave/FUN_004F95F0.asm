Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f95f0  section .text  offset fichier 0xf89f0  1664 octets

  004f95f0  83ec2c                sub       esp,2Ch
  004f95f3  53                    push      ebx
  004f95f4  55                    push      ebp
  004f95f5  56                    push      esi
  004f95f6  8b74243c              mov       esi,[esp+3Ch]
  004f95fa  8d442424              lea       eax,[esp+24h]
  004f95fe  57                    push      edi
  004f95ff  8d4c242c              lea       ecx,[esp+2Ch]
  004f9603  50                    push      eax
  004f9604  8d542424              lea       edx,[esp+24h]
  004f9608  51                    push      ecx
  004f9609  33db                  xor       ebx,ebx
  004f960b  52                    push      edx
  004f960c  56                    push      esi
  004f960d  66895c2440            mov       [esp+40h],bx
  004f9612  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  004f9619  66c74424443c00        mov       word [esp+44h],3Ch
  004f9620  66c74424469600        mov       word [esp+46h],96h
  004f9627  66895c2448            mov       [esp+48h],bx
  004f962c  66c744244a7800        mov       word [esp+4Ah],78h
  004f9633  e848f30300            call      00538980h
  004f9638  83c410                add       esp,10h
  004f963b  8d442424              lea       eax,[esp+24h]
  004f963f  8d4c241c              lea       ecx,[esp+1Ch]
  004f9643  8d542414              lea       edx,[esp+14h]
  004f9647  50                    push      eax
  004f9648  51                    push      ecx
  004f9649  8d442418              lea       eax,[esp+18h]
  004f964d  52                    push      edx
  004f964e  8d4c244c              lea       ecx,[esp+4Ch]
  004f9652  50                    push      eax
  004f9653  51                    push      ecx
  004f9654  56                    push      esi
  004f9655  e8c6c80300            call      00535F20h
  004f965a  8b4c2458              mov       ecx,[esp+58h]
  004f965e  b840000000            mov       eax,40h
  004f9663  2bc1                  sub       eax,ecx
  004f9665  83c418                add       esp,18h
  004f9668  89442418              mov       [esp+18h],eax
  004f966c  8b44241c              mov       eax,[esp+1Ch]
  004f9670  3bc3                  cmp       eax,ebx
  004f9672  0f8415030000          je        near 004F998Dh
  004f9678  56                    push      esi
  004f9679  e8221ffeff            call      004DB5A0h
  004f967e  8b06                  mov       eax,[esi]
  004f9680  83c404                add       esp,4
  004f9683  a802                  test      al,2
  004f9685  0f841f010000          je        near 004F97AAh
  004f968b  f644242410            test      byte [esp+24h],10h
  004f9690  740c                  je        short 004F969Eh
  004f9692  24fd                  and       al,0FDh
  004f9694  885e43                mov       [esi+43h],bl
  004f9697  8906                  mov       [esi],eax
  004f9699  e9ef020000            jmp       004F998Dh

LAB_004f969e:
  004f969e  8d542430              lea       edx,[esp+30h]
  004f96a2  53                    push      ebx
  004f96a3  8d461c                lea       eax,[esi+1Ch]
  004f96a6  52                    push      edx
  004f96a7  50                    push      eax
  004f96a8  56                    push      esi
  004f96a9  e882050200            call      00519C30h
  004f96ae  8bf8                  mov       edi,eax
  004f96b0  a1b8e05d00            mov       eax,[5DE0B8h]
  004f96b5  83c410                add       esp,10h
  004f96b8  3bc3                  cmp       eax,ebx
  004f96ba  7432                  je        short 004F96EEh
  004f96bc  8b17                  mov       edx,[edi]
  004f96be  57                    push      edi
  004f96bf  83ca40                or        edx,40h
  004f96c2  56                    push      esi
  004f96c3  66c787960000001000    mov       word [edi+96h],10h
  004f96cc  8917                  mov       [edi],edx
  004f96ce  e8ad040400            call      00539B80h
  004f96d3  83c408                add       esp,8
  004f96d6  83c71c                add       edi,1Ch
  004f96d9  57                    push      edi
  004f96da  6800180000            push      1800h
  004f96df  6800030000            push      300h
  004f96e4  6a03                  push      3
  004f96e6  e835440400            call      0053DB20h
  004f96eb  83c410                add       esp,10h

LAB_004f96ee:
  004f96ee  8b4c2440              mov       ecx,[esp+40h]
  004f96f2  8b465c                mov       eax,[esi+5Ch]
  004f96f5  81c180020000          add       ecx,280h
  004f96fb  c1e107                shl       ecx,7
  004f96fe  2bc8                  sub       ecx,eax
  004f9700  c1f904                sar       ecx,4
  004f9703  03c8                  add       ecx,eax
  004f9705  668b4628              mov       ax,[esi+28h]
  004f9709  894e5c                mov       [esi+5Ch],ecx
  004f970c  8a4e43                mov       cl,[esi+43h]
  004f970f  80f908                cmp       cl,8
  004f9712  0fbfd0                movsx     edx,ax
  004f9715  7235                  jb        short 004F974Ch
  004f9717  bf00300000            mov       edi,3000h
  004f971c  2bfa                  sub       edi,edx
  004f971e  c1ff02                sar       edi,2
  004f9721  03f8                  add       edi,eax
  004f9723  668b462a              mov       ax,[esi+2Ah]
  004f9727  0fbfd0                movsx     edx,ax
  004f972a  66897e28              mov       [esi+28h],di
  004f972e  bf00300000            mov       edi,3000h
  004f9733  2bfa                  sub       edi,edx
  004f9735  c1ff02                sar       edi,2
  004f9738  03f8                  add       edi,eax
  004f973a  668b462c              mov       ax,[esi+2Ch]
  004f973e  66897e2a              mov       [esi+2Ah],di
  004f9742  bf00300000            mov       edi,3000h
  004f9747  0fbfd0                movsx     edx,ax
  004f974a  eb33                  jmp       short 004F977Fh

LAB_004f974c:
  004f974c  bf00100000            mov       edi,1000h
  004f9751  2bfa                  sub       edi,edx
  004f9753  c1ff02                sar       edi,2
  004f9756  03f8                  add       edi,eax
  004f9758  668b462a              mov       ax,[esi+2Ah]
  004f975c  0fbfd0                movsx     edx,ax
  004f975f  66897e28              mov       [esi+28h],di
  004f9763  bf00100000            mov       edi,1000h
  004f9768  2bfa                  sub       edi,edx
  004f976a  c1ff02                sar       edi,2
  004f976d  03f8                  add       edi,eax
  004f976f  668b462c              mov       ax,[esi+2Ch]
  004f9773  66897e2a              mov       [esi+2Ah],di
  004f9777  bf00100000            mov       edi,1000h
  004f977c  0fbfd0                movsx     edx,ax

LAB_004f977f:
  004f977f  2bfa                  sub       edi,edx
  004f9781  c1ff02                sar       edi,2
  004f9784  03f8                  add       edi,eax
  004f9786  84c9                  test      cl,cl
  004f9788  66897e2c              mov       [esi+2Ch],di
  004f978c  0f84fb010000          je        near 004F998Dh
  004f9792  8ac1                  mov       al,cl
  004f9794  fec8                  dec       al
  004f9796  884643                mov       [esi+43h],al
  004f9799  0f85ee010000          jne       near 004F998Dh
  004f979f  8b06                  mov       eax,[esi]
  004f97a1  24fd                  and       al,0FDh
  004f97a3  8906                  mov       [esi],eax
  004f97a5  e9e3010000            jmp       004F998Dh

LAB_004f97aa:
  004f97aa  a810                  test      al,10h
  004f97ac  0f85db010000          jne       near 004F998Dh
  004f97b2  a904080000            test      eax,804h
  004f97b7  0f85d0010000          jne       near 004F998Dh
  004f97bd  33c0                  xor       eax,eax
  004f97bf  8a4642                mov       al,[esi+42h]
  004f97c2  83f803                cmp       eax,3
  004f97c5  0f8776010000          ja        near 004F9941h
  004f97cb  ff2485549c4f00        jmp       dword [eax*4+4F9C54h]
  004f97d2  817c24102c010000      cmp       dword [esp+10h],12Ch
  004f97da  7d09                  jge       short 004F97E5h
  004f97dc  c6464300              mov       byte [esi+43h],0
  004f97e0  e95c010000            jmp       004F9941h

LAB_004f97e5:
  004f97e5  8a4643                mov       al,[esi+43h]
  004f97e8  84c0                  test      al,al
  004f97ea  7463                  je        short 004F984Fh
  004f97ec  fec8                  dec       al
  004f97ee  884643                mov       [esi+43h],al
  004f97f1  8b465c                mov       eax,[esi+5Ch]
  004f97f4  8bc8                  mov       ecx,eax
  004f97f6  c1f904                sar       ecx,4
  004f97f9  2bc1                  sub       eax,ecx
  004f97fb  89465c                mov       [esi+5Ch],eax
  004f97fe  e93e010000            jmp       004F9941h
  004f9803  8b442440              mov       eax,[esp+40h]
  004f9807  8b4c2414              mov       ecx,[esp+14h]
  004f980b  c1f802                sar       eax,2
  004f980e  83c038                add       eax,38h
  004f9811  8d1408                lea       edx,[eax+ecx]
  004f9814  8d3c00                lea       edi,[eax+eax]
  004f9817  81e2ff0f0000          and       edx,0FFFh
  004f981d  3bd7                  cmp       edx,edi
  004f981f  7f1a                  jg        short 004F983Bh
  004f9821  8b465c                mov       eax,[esi+5Ch]
  004f9824  66014e32              add       [esi+32h],cx
  004f9828  8bc8                  mov       ecx,eax
  004f982a  c6464300              mov       byte [esi+43h],0
  004f982e  c1f904                sar       ecx,4
  004f9831  2bc1                  sub       eax,ecx
  004f9833  89465c                mov       [esi+5Ch],eax
  004f9836  e906010000            jmp       004F9941h

LAB_004f983b:
  004f983b  81e1ff0f0000          and       ecx,0FFFh
  004f9841  81f900080000          cmp       ecx,800h
  004f9847  7c02                  jl        short 004F984Bh
  004f9849  f7d8                  neg       eax

LAB_004f984b:
  004f984b  66014632              add       [esi+32h],ax

LAB_004f984f:
  004f984f  8b465c                mov       eax,[esi+5Ch]
  004f9852  8bc8                  mov       ecx,eax
  004f9854  c1f904                sar       ecx,4
  004f9857  2bc1                  sub       eax,ecx
  004f9859  89465c                mov       [esi+5Ch],eax
  004f985c  e9e0000000            jmp       004F9941h
  004f9861  817c24102c010000      cmp       dword [esp+10h],12Ch
  004f9869  7d09                  jge       short 004F9874h
  004f986b  c6464300              mov       byte [esi+43h],0
  004f986f  e9cd000000            jmp       004F9941h

LAB_004f9874:
  004f9874  8a4643                mov       al,[esi+43h]
  004f9877  84c0                  test      al,al
  004f9879  7405                  je        short 004F9880h
  004f987b  fec8                  dec       al
  004f987d  884643                mov       [esi+43h],al

LAB_004f9880:
  004f9880  8b542440              mov       edx,[esp+40h]
  004f9884  8b4c2414              mov       ecx,[esp+14h]
  004f9888  8bc2                  mov       eax,edx
  004f988a  c1f802                sar       eax,2
  004f988d  83c038                add       eax,38h
  004f9890  8d3c08                lea       edi,[eax+ecx]
  004f9893  8d1c00                lea       ebx,[eax+eax]
  004f9896  81e7ff0f0000          and       edi,0FFFh
  004f989c  3bfb                  cmp       edi,ebx
  004f989e  7f06                  jg        short 004F98A6h
  004f98a0  66014e32              add       [esi+32h],cx
  004f98a4  eb14                  jmp       short 004F98BAh

LAB_004f98a6:
  004f98a6  81e1ff0f0000          and       ecx,0FFFh
  004f98ac  81f900080000          cmp       ecx,800h
  004f98b2  7c02                  jl        short 004F98B6h
  004f98b4  f7d8                  neg       eax

LAB_004f98b6:
  004f98b6  66014632              add       [esi+32h],ax

LAB_004f98ba:
  004f98ba  8b465c                mov       eax,[esi+5Ch]
  004f98bd  81c2a0010000          add       edx,1A0h
  004f98c3  c1e207                shl       edx,7
  004f98c6  2bd0                  sub       edx,eax
  004f98c8  c1fa04                sar       edx,4
  004f98cb  03d0                  add       edx,eax
  004f98cd  89565c                mov       [esi+5Ch],edx
  004f98d0  eb6f                  jmp       short 004F9941h
  004f98d2  8a4643                mov       al,[esi+43h]
  004f98d5  84c0                  test      al,al
  004f98d7  7405                  je        short 004F98DEh
  004f98d9  fec8                  dec       al
  004f98db  884643                mov       [esi+43h],al

LAB_004f98de:
  004f98de  8b4c2440              mov       ecx,[esp+40h]
  004f98e2  8b465c                mov       eax,[esi+5Ch]
  004f98e5  8d9140010000          lea       edx,[ecx+140h]
  004f98eb  c1e207                shl       edx,7
  004f98ee  2bd0                  sub       edx,eax
  004f98f0  c1fa04                sar       edx,4
  004f98f3  03d0                  add       edx,eax
  004f98f5  668b4610              mov       ax,[esi+10h]
  004f98f9  89565c                mov       [esi+5Ch],edx
  004f98fc  668b5632              mov       dx,[esi+32h]
  004f9900  2bc2                  sub       eax,edx
  004f9902  25ff0f0000            and       eax,0FFFh
  004f9907  8db800010000          lea       edi,[eax+100h]
  004f990d  81e7ff0f0000          and       edi,0FFFh
  004f9913  81ff00020000          cmp       edi,200h
  004f9919  7e26                  jle       short 004F9941h
  004f991b  25ff0f0000            and       eax,0FFFh
  004f9920  3d00080000            cmp       eax,800h
  004f9925  7d0a                  jge       short 004F9931h
  004f9927  c1f902                sar       ecx,2
  004f992a  83c138                add       ecx,38h
  004f992d  8bc1                  mov       eax,ecx
  004f992f  eb0a                  jmp       short 004F993Bh

LAB_004f9931:
  004f9931  c1f902                sar       ecx,2
  004f9934  b8c8ffffff            mov       eax,0FFFFFFC8h
  004f9939  2bc1                  sub       eax,ecx

LAB_004f993b:
  004f993b  03d0                  add       edx,eax
  004f993d  66895632              mov       [esi+32h],dx

LAB_004f9941:
  004f9941  0fbf4614              movsx     eax,word [esi+14h]
  004f9945  50                    push      eax
  004f9946  e8d1c00400            call      00545A1Ch
  004f994b  668b4e28              mov       cx,[esi+28h]
  004f994f  c1f802                sar       eax,2
  004f9952  0500100000            add       eax,1000h
  004f9957  0fbfd1                movsx     edx,cx
  004f995a  8bf8                  mov       edi,eax
  004f995c  2bfa                  sub       edi,edx
  004f995e  c1ff02                sar       edi,2
  004f9961  03f9                  add       edi,ecx
  004f9963  668b4e2a              mov       cx,[esi+2Ah]
  004f9967  0fbfd1                movsx     edx,cx
  004f996a  66897e28              mov       [esi+28h],di
  004f996e  8bf8                  mov       edi,eax
  004f9970  2bfa                  sub       edi,edx
  004f9972  c1ff02                sar       edi,2
  004f9975  03f9                  add       edi,ecx
  004f9977  668b4e2c              mov       cx,[esi+2Ch]
  004f997b  0fbfd1                movsx     edx,cx
  004f997e  2bc2                  sub       eax,edx
  004f9980  66897e2a              mov       [esi+2Ah],di
  004f9984  c1f802                sar       eax,2
  004f9987  03c1                  add       eax,ecx
  004f9989  6689462c              mov       [esi+2Ch],ax

LAB_004f998d:
  004f998d  8b44241c              mov       eax,[esp+1Ch]
  004f9991  50                    push      eax
  004f9992  56                    push      esi
  004f9993  e8480ffaff            call      0049A8E0h
  004f9998  83c408                add       esp,8
  004f999b  56                    push      esi
  004f999c  e89f9f0300            call      00533940h
  004f99a1  8b466c                mov       eax,[esi+6Ch]
  004f99a4  b301                  mov       bl,1
  004f99a6  83c404                add       esp,4
  004f99a9  8418                  test      [eax],bl
  004f99ab  7506                  jne       short 004F99B3h
  004f99ad  8b4860                mov       ecx,[eax+60h]
  004f99b0  894e48                mov       [esi+48h],ecx

LAB_004f99b3:
  004f99b3  e888d70400            call      00547140h
  004f99b8  8b7e60                mov       edi,[esi+60h]
  004f99bb  25ff000000            and       eax,0FFh
  004f99c0  0500010000            add       eax,100h
  004f99c5  66014614              add       [esi+14h],ax
  004f99c9  8b4648                mov       eax,[esi+48h]
  004f99cc  668b4e14              mov       cx,[esi+14h]
  004f99d0  3bc7                  cmp       eax,edi
  004f99d2  7c02                  jl        short 004F99D6h
  004f99d4  8bf8                  mov       edi,eax

LAB_004f99d6:
  004f99d6  f60602                test      byte [esi],2
  004f99d9  7415                  je        short 004F99F0h
  004f99db  8b4620                mov       eax,[esi+20h]
  004f99de  2bf8                  sub       edi,eax
  004f99e0  81c700800200          add       edi,28000h
  004f99e6  c1ff03                sar       edi,3
  004f99e9  03f8                  add       edi,eax
  004f99eb  897e20                mov       [esi+20h],edi
  004f99ee  eb20                  jmp       short 004F9A10h

LAB_004f99f0:
  004f99f0  8b6e20                mov       ebp,[esi+20h]
  004f99f3  0fbfd1                movsx     edx,cx
  004f99f6  52                    push      edx
  004f99f7  e820c00400            call      00545A1Ch
  004f99fc  0500320000            add       eax,3200h
  004f9a01  c1e006                shl       eax,6
  004f9a04  03c7                  add       eax,edi
  004f9a06  2bc5                  sub       eax,ebp
  004f9a08  c1f804                sar       eax,4
  004f9a0b  03c5                  add       eax,ebp
  004f9a0d  894620                mov       [esi+20h],eax

LAB_004f9a10:
  004f9a10  0fbf4614              movsx     eax,word [esi+14h]
  004f9a14  50                    push      eax
  004f9a15  e802c00400            call      00545A1Ch
  004f9a1a  8bc8                  mov       ecx,eax
  004f9a1c  d1f9                  sar       ecx,1
  004f9a1e  03c8                  add       ecx,eax
  004f9a20  c1f906                sar       ecx,6
  004f9a23  81c180000000          add       ecx,80h
  004f9a29  81e1ff000000          and       ecx,0FFh
  004f9a2f  8bd1                  mov       edx,ecx
  004f9a31  c1e208                shl       edx,8
  004f9a34  0bd1                  or        edx,ecx
  004f9a36  c1e208                shl       edx,8
  004f9a39  0bd1                  or        edx,ecx
  004f9a3b  66837e160a            cmp       word [esi+16h],0Ah
  004f9a40  895638                mov       [esi+38h],edx
  004f9a43  7c0d                  jl        short 004F9A52h
  004f9a45  8b442420              mov       eax,[esp+20h]
  004f9a49  50                    push      eax
  004f9a4a  56                    push      esi
  004f9a4b  e8209dfeff            call      004E3770h
  004f9a50  eb1d                  jmp       short 004F9A6Fh

LAB_004f9a52:
  004f9a52  8bbe90000000          mov       edi,[esi+90h]
  004f9a58  8b4c2420              mov       ecx,[esp+20h]
  004f9a5c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004f9a62  51                    push      ecx
  004f9a63  56                    push      esi
  004f9a64  89be90000000          mov       [esi+90h],edi
  004f9a6a  e88182faff            call      004A1CF0h

LAB_004f9a6f:
  004f9a6f  8b442424              mov       eax,[esp+24h]
  004f9a73  83c408                add       esp,8
  004f9a76  85c0                  test      eax,eax
  004f9a78  0f84ce010000          je        near 004F9C4Ch
  004f9a7e  f70616080000          test      dword [esi],816h
  004f9a84  0f85c2010000          jne       near 004F9C4Ch
  004f9a8a  8a4643                mov       al,[esi+43h]
  004f9a8d  84c0                  test      al,al
  004f9a8f  0f85b7010000          jne       near 004F9C4Ch
  004f9a95  8b466c                mov       eax,[esi+6Ch]
  004f9a98  8b561c                mov       edx,[esi+1Ch]
  004f9a9b  8b781c                mov       edi,[eax+1Ch]
  004f9a9e  8b6824                mov       ebp,[eax+24h]
  004f9aa1  8b4624                mov       eax,[esi+24h]
  004f9aa4  2bfa                  sub       edi,edx
  004f9aa6  2be8                  sub       ebp,eax
  004f9aa8  c1ff0c                sar       edi,0Ch
  004f9aab  c1fd0c                sar       ebp,0Ch
  004f9aae  8bd7                  mov       edx,edi
  004f9ab0  8bc5                  mov       eax,ebp
  004f9ab2  0fafd7                imul      edx,edi
  004f9ab5  0fafc5                imul      eax,ebp
  004f9ab8  03d0                  add       edx,eax
  004f9aba  52                    push      edx
  004f9abb  e850bf0400            call      00545A10h
  004f9ac0  55                    push      ebp
  004f9ac1  57                    push      edi
  004f9ac2  89442418              mov       [esp+18h],eax
  004f9ac6  e857bf0400            call      00545A22h
  004f9acb  668b4e32              mov       cx,[esi+32h]
  004f9acf  2bc1                  sub       eax,ecx
  004f9ad1  25ff0f0000            and       eax,0FFFh
  004f9ad6  89442414              mov       [esp+14h],eax
  004f9ada  8b442410              mov       eax,[esp+10h]
  004f9ade  3d2c010000            cmp       eax,12Ch
  004f9ae3  0f8dbc000000          jge       near 004F9BA5h
  004f9ae9  e852d60400            call      00547140h
  004f9aee  99                    cdq
  004f9aef  b964000000            mov       ecx,64h
  004f9af4  f7f9                  idiv      ecx
  004f9af6  83fa3c                cmp       edx,3Ch
  004f9af9  7d5c                  jge       short 004F9B57h
  004f9afb  8b542414              mov       edx,[esp+14h]
  004f9aff  8b7c2418              mov       edi,[esp+18h]
  004f9b03  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004f9b0a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004f9b11  25ff0f0000            and       eax,0FFFh
  004f9b16  3bc1                  cmp       eax,ecx
  004f9b18  0f8ff8000000          jg        near 004F9C16h
  004f9b1e  e81dd60400            call      00547140h
  004f9b23  99                    cdq
  004f9b24  8d4c3f01              lea       ecx,[edi+edi+1]
  004f9b28  f7f9                  idiv      ecx
  004f9b2a  8b4c2410              mov       ecx,[esp+10h]
  004f9b2e  b8fa000000            mov       eax,0FAh
  004f9b33  2bc2                  sub       eax,edx
  004f9b35  3bc8                  cmp       ecx,eax
  004f9b37  0f8dd9000000          jge       near 004F9C16h
  004f9b3d  8b1e                  mov       ebx,[esi]
  004f9b3f  8bcf                  mov       ecx,edi
  004f9b41  c1f903                sar       ecx,3
  004f9b44  83cb02                or        ebx,2
  004f9b47  80c110                add       cl,10h
  004f9b4a  891e                  mov       [esi],ebx
  004f9b4c  884e43                mov       [esi+43h],cl
  004f9b4f  5f                    pop       edi
  004f9b50  5e                    pop       esi
  004f9b51  5d                    pop       ebp
  004f9b52  5b                    pop       ebx
  004f9b53  83c42c                add       esp,2Ch
  004f9b56  c3                    ret

LAB_004f9b57:
  004f9b57  c6464203              mov       byte [esi+42h],3
  004f9b5b  e8e0d50400            call      00547140h
  004f9b60  99                    cdq
  004f9b61  b90a000000            mov       ecx,0Ah
  004f9b66  f7f9                  idiv      ecx
  004f9b68  8b442418              mov       eax,[esp+18h]
  004f9b6c  c1f802                sar       eax,2
  004f9b6f  02d0                  add       dl,al
  004f9b71  02d1                  add       dl,cl
  004f9b73  8b4c2440              mov       ecx,[esp+40h]
  004f9b77  885643                mov       [esi+43h],dl
  004f9b7a  8db980000000          lea       edi,[ecx+80h]
  004f9b80  c1e704                shl       edi,4
  004f9b83  e8b8d50400            call      00547140h
  004f9b88  99                    cdq
  004f9b89  f7ff                  idiv      edi
  004f9b8b  8b4c2414              mov       ecx,[esp+14h]
  004f9b8f  d1ff                  sar       edi,1
  004f9b91  2bd7                  sub       edx,edi
  004f9b93  03d1                  add       edx,ecx
  004f9b95  66035632              add       dx,[esi+32h]
  004f9b99  66895610              mov       [esi+10h],dx
  004f9b9d  5f                    pop       edi
  004f9b9e  5e                    pop       esi
  004f9b9f  5d                    pop       ebp
  004f9ba0  5b                    pop       ebx
  004f9ba1  83c42c                add       esp,2Ch
  004f9ba4  c3                    ret

LAB_004f9ba5:
  004f9ba5  3d58020000            cmp       eax,258h
  004f9baa  7d2c                  jge       short 004F9BD8h
  004f9bac  e88fd50400            call      00547140h
  004f9bb1  c6464202              mov       byte [esi+42h],2
  004f9bb5  e886d50400            call      00547140h
  004f9bba  99                    cdq
  004f9bbb  b90a000000            mov       ecx,0Ah
  004f9bc0  f7f9                  idiv      ecx
  004f9bc2  8b442418              mov       eax,[esp+18h]
  004f9bc6  c1f802                sar       eax,2
  004f9bc9  02d0                  add       dl,al
  004f9bcb  02d1                  add       dl,cl
  004f9bcd  885643                mov       [esi+43h],dl
  004f9bd0  5f                    pop       edi
  004f9bd1  5e                    pop       esi
  004f9bd2  5d                    pop       ebp
  004f9bd3  5b                    pop       ebx
  004f9bd4  83c42c                add       esp,2Ch
  004f9bd7  c3                    ret

LAB_004f9bd8:
  004f9bd8  e863d50400            call      00547140h
  004f9bdd  99                    cdq
  004f9bde  b964000000            mov       ecx,64h
  004f9be3  f7f9                  idiv      ecx
  004f9be5  83fa1e                cmp       edx,1Eh
  004f9be8  7d27                  jge       short 004F9C11h
  004f9bea  c6464200              mov       byte [esi+42h],0
  004f9bee  e84dd50400            call      00547140h
  004f9bf3  99                    cdq
  004f9bf4  b90a000000            mov       ecx,0Ah
  004f9bf9  f7f9                  idiv      ecx
  004f9bfb  8b442418              mov       eax,[esp+18h]
  004f9bff  c1f802                sar       eax,2
  004f9c02  02d0                  add       dl,al
  004f9c04  02d1                  add       dl,cl
  004f9c06  885643                mov       [esi+43h],dl
  004f9c09  5f                    pop       edi
  004f9c0a  5e                    pop       esi
  004f9c0b  5d                    pop       ebp
  004f9c0c  5b                    pop       ebx
  004f9c0d  83c42c                add       esp,2Ch
  004f9c10  c3                    ret

LAB_004f9c11:
  004f9c11  83fa3c                cmp       edx,3Ch
  004f9c14  7d0e                  jge       short 004F9C24h

LAB_004f9c16:
  004f9c16  885e42                mov       [esi+42h],bl
  004f9c19  885e43                mov       [esi+43h],bl
  004f9c1c  5f                    pop       edi
  004f9c1d  5e                    pop       esi
  004f9c1e  5d                    pop       ebp
  004f9c1f  5b                    pop       ebx
  004f9c20  83c42c                add       esp,2Ch
  004f9c23  c3                    ret

LAB_004f9c24:
  004f9c24  c6464203              mov       byte [esi+42h],3
  004f9c28  e813d50400            call      00547140h
  004f9c2d  99                    cdq
  004f9c2e  b90a000000            mov       ecx,0Ah
  004f9c33  f7f9                  idiv      ecx
  004f9c35  8b442418              mov       eax,[esp+18h]
  004f9c39  c1f802                sar       eax,2
  004f9c3c  02d0                  add       dl,al
  004f9c3e  02d1                  add       dl,cl
  004f9c40  885643                mov       [esi+43h],dl
  004f9c43  e8f8d40400            call      00547140h
  004f9c48  66894610              mov       [esi+10h],ax

LAB_004f9c4c:
  004f9c4c  5f                    pop       edi
  004f9c4d  5e                    pop       esi
  004f9c4e  5d                    pop       ebp
  004f9c4f  5b                    pop       ebx
  004f9c50  83c42c                add       esp,2Ch
  004f9c53  c3                    ret
  004f9c54  d2974f000398          rcl       byte [edi-67FCFFB1h],cl
  004f9c5a  4f                    dec       edi
  004f9c5b  006198                add       [ecx-68h],ah
  004f9c5e  4f                    dec       edi
  004f9c5f  00d2                  add       dl,dl
  004f9c61  98                    cwde
  004f9c62  4f                    dec       edi
  004f9c63  009090909090          add       [eax-6F6F6F70h],dl
  004f9c69  90                    nop
  004f9c6a  90                    nop
  004f9c6b  90                    nop
  004f9c6c  90                    nop
  004f9c6d  90                    nop
  004f9c6e  90                    nop
  004f9c6f  90                    nop

32 cible(s) de saut interne : 0x004f969e, 0x004f96ee, 0x004f974c, 0x004f977f, 0x004f97aa, 0x004f97e5, 0x004f983b, 0x004f984b, 0x004f984f, 0x004f9874, 0x004f9880, 0x004f98a6, 0x004f98b6, 0x004f98ba, 0x004f98de, 0x004f9931, 0x004f993b, 0x004f9941, 0x004f998d, 0x004f99b3, 0x004f99d6, 0x004f99f0, 0x004f9a10, 0x004f9a52, 0x004f9a6f, 0x004f9b57, 0x004f9ba5, 0x004f9bd8, 0x004f9c11, 0x004f9c16, 0x004f9c24, 0x004f9c4c
