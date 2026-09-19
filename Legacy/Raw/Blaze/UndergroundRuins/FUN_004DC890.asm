Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004dc890  section .text  offset fichier 0xdbc90  1664 octets

  004dc890  83ec2c                sub       esp,2Ch
  004dc893  53                    push      ebx
  004dc894  55                    push      ebp
  004dc895  56                    push      esi
  004dc896  8b74243c              mov       esi,[esp+3Ch]
  004dc89a  8d442424              lea       eax,[esp+24h]
  004dc89e  57                    push      edi
  004dc89f  8d4c242c              lea       ecx,[esp+2Ch]
  004dc8a3  50                    push      eax
  004dc8a4  8d542424              lea       edx,[esp+24h]
  004dc8a8  51                    push      ecx
  004dc8a9  33db                  xor       ebx,ebx
  004dc8ab  52                    push      edx
  004dc8ac  56                    push      esi
  004dc8ad  66895c2440            mov       [esp+40h],bx
  004dc8b2  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  004dc8b9  66c74424443c00        mov       word [esp+44h],3Ch
  004dc8c0  66c74424469600        mov       word [esp+46h],96h
  004dc8c7  66895c2448            mov       [esp+48h],bx
  004dc8cc  66c744244a7800        mov       word [esp+4Ah],78h
  004dc8d3  e8a8c00500            call      00538980h
  004dc8d8  83c410                add       esp,10h
  004dc8db  8d442424              lea       eax,[esp+24h]
  004dc8df  8d4c241c              lea       ecx,[esp+1Ch]
  004dc8e3  8d542414              lea       edx,[esp+14h]
  004dc8e7  50                    push      eax
  004dc8e8  51                    push      ecx
  004dc8e9  8d442418              lea       eax,[esp+18h]
  004dc8ed  52                    push      edx
  004dc8ee  8d4c244c              lea       ecx,[esp+4Ch]
  004dc8f2  50                    push      eax
  004dc8f3  51                    push      ecx
  004dc8f4  56                    push      esi
  004dc8f5  e826960500            call      00535F20h
  004dc8fa  8b4c2458              mov       ecx,[esp+58h]
  004dc8fe  b840000000            mov       eax,40h
  004dc903  2bc1                  sub       eax,ecx
  004dc905  83c418                add       esp,18h
  004dc908  89442418              mov       [esp+18h],eax
  004dc90c  8b44241c              mov       eax,[esp+1Ch]
  004dc910  3bc3                  cmp       eax,ebx
  004dc912  0f8415030000          je        near 004DCC2Dh
  004dc918  56                    push      esi
  004dc919  e882ecffff            call      004DB5A0h
  004dc91e  8b06                  mov       eax,[esi]
  004dc920  83c404                add       esp,4
  004dc923  a802                  test      al,2
  004dc925  0f841f010000          je        near 004DCA4Ah
  004dc92b  f644242410            test      byte [esp+24h],10h
  004dc930  740c                  je        short 004DC93Eh
  004dc932  24fd                  and       al,0FDh
  004dc934  885e43                mov       [esi+43h],bl
  004dc937  8906                  mov       [esi],eax
  004dc939  e9ef020000            jmp       004DCC2Dh

LAB_004dc93e:
  004dc93e  8d542430              lea       edx,[esp+30h]
  004dc942  53                    push      ebx
  004dc943  8d461c                lea       eax,[esi+1Ch]
  004dc946  52                    push      edx
  004dc947  50                    push      eax
  004dc948  56                    push      esi
  004dc949  e8e2d20300            call      00519C30h
  004dc94e  8bf8                  mov       edi,eax
  004dc950  a1b8e05d00            mov       eax,[5DE0B8h]
  004dc955  83c410                add       esp,10h
  004dc958  3bc3                  cmp       eax,ebx
  004dc95a  7432                  je        short 004DC98Eh
  004dc95c  8b17                  mov       edx,[edi]
  004dc95e  57                    push      edi
  004dc95f  83ca40                or        edx,40h
  004dc962  56                    push      esi
  004dc963  66c787960000001000    mov       word [edi+96h],10h
  004dc96c  8917                  mov       [edi],edx
  004dc96e  e80dd20500            call      00539B80h
  004dc973  83c408                add       esp,8
  004dc976  83c71c                add       edi,1Ch
  004dc979  57                    push      edi
  004dc97a  6800180000            push      1800h
  004dc97f  6800030000            push      300h
  004dc984  6a03                  push      3
  004dc986  e895110600            call      0053DB20h
  004dc98b  83c410                add       esp,10h

LAB_004dc98e:
  004dc98e  8b4c2440              mov       ecx,[esp+40h]
  004dc992  8b465c                mov       eax,[esi+5Ch]
  004dc995  81c180020000          add       ecx,280h
  004dc99b  c1e107                shl       ecx,7
  004dc99e  2bc8                  sub       ecx,eax
  004dc9a0  c1f904                sar       ecx,4
  004dc9a3  03c8                  add       ecx,eax
  004dc9a5  668b4628              mov       ax,[esi+28h]
  004dc9a9  894e5c                mov       [esi+5Ch],ecx
  004dc9ac  8a4e43                mov       cl,[esi+43h]
  004dc9af  80f908                cmp       cl,8
  004dc9b2  0fbfd0                movsx     edx,ax
  004dc9b5  7235                  jb        short 004DC9ECh
  004dc9b7  bf00300000            mov       edi,3000h
  004dc9bc  2bfa                  sub       edi,edx
  004dc9be  c1ff02                sar       edi,2
  004dc9c1  03f8                  add       edi,eax
  004dc9c3  668b462a              mov       ax,[esi+2Ah]
  004dc9c7  0fbfd0                movsx     edx,ax
  004dc9ca  66897e28              mov       [esi+28h],di
  004dc9ce  bf00300000            mov       edi,3000h
  004dc9d3  2bfa                  sub       edi,edx
  004dc9d5  c1ff02                sar       edi,2
  004dc9d8  03f8                  add       edi,eax
  004dc9da  668b462c              mov       ax,[esi+2Ch]
  004dc9de  66897e2a              mov       [esi+2Ah],di
  004dc9e2  bf00300000            mov       edi,3000h
  004dc9e7  0fbfd0                movsx     edx,ax
  004dc9ea  eb33                  jmp       short 004DCA1Fh

LAB_004dc9ec:
  004dc9ec  bf00100000            mov       edi,1000h
  004dc9f1  2bfa                  sub       edi,edx
  004dc9f3  c1ff02                sar       edi,2
  004dc9f6  03f8                  add       edi,eax
  004dc9f8  668b462a              mov       ax,[esi+2Ah]
  004dc9fc  0fbfd0                movsx     edx,ax
  004dc9ff  66897e28              mov       [esi+28h],di
  004dca03  bf00100000            mov       edi,1000h
  004dca08  2bfa                  sub       edi,edx
  004dca0a  c1ff02                sar       edi,2
  004dca0d  03f8                  add       edi,eax
  004dca0f  668b462c              mov       ax,[esi+2Ch]
  004dca13  66897e2a              mov       [esi+2Ah],di
  004dca17  bf00100000            mov       edi,1000h
  004dca1c  0fbfd0                movsx     edx,ax

LAB_004dca1f:
  004dca1f  2bfa                  sub       edi,edx
  004dca21  c1ff02                sar       edi,2
  004dca24  03f8                  add       edi,eax
  004dca26  84c9                  test      cl,cl
  004dca28  66897e2c              mov       [esi+2Ch],di
  004dca2c  0f84fb010000          je        near 004DCC2Dh
  004dca32  8ac1                  mov       al,cl
  004dca34  fec8                  dec       al
  004dca36  884643                mov       [esi+43h],al
  004dca39  0f85ee010000          jne       near 004DCC2Dh
  004dca3f  8b06                  mov       eax,[esi]
  004dca41  24fd                  and       al,0FDh
  004dca43  8906                  mov       [esi],eax
  004dca45  e9e3010000            jmp       004DCC2Dh

LAB_004dca4a:
  004dca4a  a810                  test      al,10h
  004dca4c  0f85db010000          jne       near 004DCC2Dh
  004dca52  a904080000            test      eax,804h
  004dca57  0f85d0010000          jne       near 004DCC2Dh
  004dca5d  33c0                  xor       eax,eax
  004dca5f  8a4642                mov       al,[esi+42h]
  004dca62  83f803                cmp       eax,3
  004dca65  0f8776010000          ja        near 004DCBE1h
  004dca6b  ff2485f4ce4d00        jmp       dword [eax*4+4DCEF4h]
  004dca72  817c24102c010000      cmp       dword [esp+10h],12Ch
  004dca7a  7d09                  jge       short 004DCA85h
  004dca7c  c6464300              mov       byte [esi+43h],0
  004dca80  e95c010000            jmp       004DCBE1h

LAB_004dca85:
  004dca85  8a4643                mov       al,[esi+43h]
  004dca88  84c0                  test      al,al
  004dca8a  7463                  je        short 004DCAEFh
  004dca8c  fec8                  dec       al
  004dca8e  884643                mov       [esi+43h],al
  004dca91  8b465c                mov       eax,[esi+5Ch]
  004dca94  8bc8                  mov       ecx,eax
  004dca96  c1f904                sar       ecx,4
  004dca99  2bc1                  sub       eax,ecx
  004dca9b  89465c                mov       [esi+5Ch],eax
  004dca9e  e93e010000            jmp       004DCBE1h
  004dcaa3  8b442440              mov       eax,[esp+40h]
  004dcaa7  8b4c2414              mov       ecx,[esp+14h]
  004dcaab  c1f802                sar       eax,2
  004dcaae  83c038                add       eax,38h
  004dcab1  8d1408                lea       edx,[eax+ecx]
  004dcab4  8d3c00                lea       edi,[eax+eax]
  004dcab7  81e2ff0f0000          and       edx,0FFFh
  004dcabd  3bd7                  cmp       edx,edi
  004dcabf  7f1a                  jg        short 004DCADBh
  004dcac1  8b465c                mov       eax,[esi+5Ch]
  004dcac4  66014e32              add       [esi+32h],cx
  004dcac8  8bc8                  mov       ecx,eax
  004dcaca  c6464300              mov       byte [esi+43h],0
  004dcace  c1f904                sar       ecx,4
  004dcad1  2bc1                  sub       eax,ecx
  004dcad3  89465c                mov       [esi+5Ch],eax
  004dcad6  e906010000            jmp       004DCBE1h

LAB_004dcadb:
  004dcadb  81e1ff0f0000          and       ecx,0FFFh
  004dcae1  81f900080000          cmp       ecx,800h
  004dcae7  7c02                  jl        short 004DCAEBh
  004dcae9  f7d8                  neg       eax

LAB_004dcaeb:
  004dcaeb  66014632              add       [esi+32h],ax

LAB_004dcaef:
  004dcaef  8b465c                mov       eax,[esi+5Ch]
  004dcaf2  8bc8                  mov       ecx,eax
  004dcaf4  c1f904                sar       ecx,4
  004dcaf7  2bc1                  sub       eax,ecx
  004dcaf9  89465c                mov       [esi+5Ch],eax
  004dcafc  e9e0000000            jmp       004DCBE1h
  004dcb01  817c24102c010000      cmp       dword [esp+10h],12Ch
  004dcb09  7d09                  jge       short 004DCB14h
  004dcb0b  c6464300              mov       byte [esi+43h],0
  004dcb0f  e9cd000000            jmp       004DCBE1h

LAB_004dcb14:
  004dcb14  8a4643                mov       al,[esi+43h]
  004dcb17  84c0                  test      al,al
  004dcb19  7405                  je        short 004DCB20h
  004dcb1b  fec8                  dec       al
  004dcb1d  884643                mov       [esi+43h],al

LAB_004dcb20:
  004dcb20  8b542440              mov       edx,[esp+40h]
  004dcb24  8b4c2414              mov       ecx,[esp+14h]
  004dcb28  8bc2                  mov       eax,edx
  004dcb2a  c1f802                sar       eax,2
  004dcb2d  83c038                add       eax,38h
  004dcb30  8d3c08                lea       edi,[eax+ecx]
  004dcb33  8d1c00                lea       ebx,[eax+eax]
  004dcb36  81e7ff0f0000          and       edi,0FFFh
  004dcb3c  3bfb                  cmp       edi,ebx
  004dcb3e  7f06                  jg        short 004DCB46h
  004dcb40  66014e32              add       [esi+32h],cx
  004dcb44  eb14                  jmp       short 004DCB5Ah

LAB_004dcb46:
  004dcb46  81e1ff0f0000          and       ecx,0FFFh
  004dcb4c  81f900080000          cmp       ecx,800h
  004dcb52  7c02                  jl        short 004DCB56h
  004dcb54  f7d8                  neg       eax

LAB_004dcb56:
  004dcb56  66014632              add       [esi+32h],ax

LAB_004dcb5a:
  004dcb5a  8b465c                mov       eax,[esi+5Ch]
  004dcb5d  81c2a0010000          add       edx,1A0h
  004dcb63  c1e207                shl       edx,7
  004dcb66  2bd0                  sub       edx,eax
  004dcb68  c1fa04                sar       edx,4
  004dcb6b  03d0                  add       edx,eax
  004dcb6d  89565c                mov       [esi+5Ch],edx
  004dcb70  eb6f                  jmp       short 004DCBE1h
  004dcb72  8a4643                mov       al,[esi+43h]
  004dcb75  84c0                  test      al,al
  004dcb77  7405                  je        short 004DCB7Eh
  004dcb79  fec8                  dec       al
  004dcb7b  884643                mov       [esi+43h],al

LAB_004dcb7e:
  004dcb7e  8b4c2440              mov       ecx,[esp+40h]
  004dcb82  8b465c                mov       eax,[esi+5Ch]
  004dcb85  8d9140010000          lea       edx,[ecx+140h]
  004dcb8b  c1e207                shl       edx,7
  004dcb8e  2bd0                  sub       edx,eax
  004dcb90  c1fa04                sar       edx,4
  004dcb93  03d0                  add       edx,eax
  004dcb95  668b4610              mov       ax,[esi+10h]
  004dcb99  89565c                mov       [esi+5Ch],edx
  004dcb9c  668b5632              mov       dx,[esi+32h]
  004dcba0  2bc2                  sub       eax,edx
  004dcba2  25ff0f0000            and       eax,0FFFh
  004dcba7  8db800010000          lea       edi,[eax+100h]
  004dcbad  81e7ff0f0000          and       edi,0FFFh
  004dcbb3  81ff00020000          cmp       edi,200h
  004dcbb9  7e26                  jle       short 004DCBE1h
  004dcbbb  25ff0f0000            and       eax,0FFFh
  004dcbc0  3d00080000            cmp       eax,800h
  004dcbc5  7d0a                  jge       short 004DCBD1h
  004dcbc7  c1f902                sar       ecx,2
  004dcbca  83c138                add       ecx,38h
  004dcbcd  8bc1                  mov       eax,ecx
  004dcbcf  eb0a                  jmp       short 004DCBDBh

LAB_004dcbd1:
  004dcbd1  c1f902                sar       ecx,2
  004dcbd4  b8c8ffffff            mov       eax,0FFFFFFC8h
  004dcbd9  2bc1                  sub       eax,ecx

LAB_004dcbdb:
  004dcbdb  03d0                  add       edx,eax
  004dcbdd  66895632              mov       [esi+32h],dx

LAB_004dcbe1:
  004dcbe1  0fbf4614              movsx     eax,word [esi+14h]
  004dcbe5  50                    push      eax
  004dcbe6  e8318e0600            call      00545A1Ch
  004dcbeb  668b4e28              mov       cx,[esi+28h]
  004dcbef  c1f802                sar       eax,2
  004dcbf2  0500100000            add       eax,1000h
  004dcbf7  0fbfd1                movsx     edx,cx
  004dcbfa  8bf8                  mov       edi,eax
  004dcbfc  2bfa                  sub       edi,edx
  004dcbfe  c1ff02                sar       edi,2
  004dcc01  03f9                  add       edi,ecx
  004dcc03  668b4e2a              mov       cx,[esi+2Ah]
  004dcc07  0fbfd1                movsx     edx,cx
  004dcc0a  66897e28              mov       [esi+28h],di
  004dcc0e  8bf8                  mov       edi,eax
  004dcc10  2bfa                  sub       edi,edx
  004dcc12  c1ff02                sar       edi,2
  004dcc15  03f9                  add       edi,ecx
  004dcc17  668b4e2c              mov       cx,[esi+2Ch]
  004dcc1b  0fbfd1                movsx     edx,cx
  004dcc1e  2bc2                  sub       eax,edx
  004dcc20  66897e2a              mov       [esi+2Ah],di
  004dcc24  c1f802                sar       eax,2
  004dcc27  03c1                  add       eax,ecx
  004dcc29  6689462c              mov       [esi+2Ch],ax

LAB_004dcc2d:
  004dcc2d  8b44241c              mov       eax,[esp+1Ch]
  004dcc31  50                    push      eax
  004dcc32  56                    push      esi
  004dcc33  e8a8dcfbff            call      0049A8E0h
  004dcc38  83c408                add       esp,8
  004dcc3b  56                    push      esi
  004dcc3c  e8ff6c0500            call      00533940h
  004dcc41  8b466c                mov       eax,[esi+6Ch]
  004dcc44  b301                  mov       bl,1
  004dcc46  83c404                add       esp,4
  004dcc49  8418                  test      [eax],bl
  004dcc4b  7506                  jne       short 004DCC53h
  004dcc4d  8b4860                mov       ecx,[eax+60h]
  004dcc50  894e48                mov       [esi+48h],ecx

LAB_004dcc53:
  004dcc53  e8e8a40600            call      00547140h
  004dcc58  8b7e60                mov       edi,[esi+60h]
  004dcc5b  25ff000000            and       eax,0FFh
  004dcc60  0500010000            add       eax,100h
  004dcc65  66014614              add       [esi+14h],ax
  004dcc69  8b4648                mov       eax,[esi+48h]
  004dcc6c  668b4e14              mov       cx,[esi+14h]
  004dcc70  3bc7                  cmp       eax,edi
  004dcc72  7c02                  jl        short 004DCC76h
  004dcc74  8bf8                  mov       edi,eax

LAB_004dcc76:
  004dcc76  f60602                test      byte [esi],2
  004dcc79  7415                  je        short 004DCC90h
  004dcc7b  8b4620                mov       eax,[esi+20h]
  004dcc7e  2bf8                  sub       edi,eax
  004dcc80  81c700800200          add       edi,28000h
  004dcc86  c1ff03                sar       edi,3
  004dcc89  03f8                  add       edi,eax
  004dcc8b  897e20                mov       [esi+20h],edi
  004dcc8e  eb20                  jmp       short 004DCCB0h

LAB_004dcc90:
  004dcc90  8b6e20                mov       ebp,[esi+20h]
  004dcc93  0fbfd1                movsx     edx,cx
  004dcc96  52                    push      edx
  004dcc97  e8808d0600            call      00545A1Ch
  004dcc9c  0500320000            add       eax,3200h
  004dcca1  c1e006                shl       eax,6
  004dcca4  03c7                  add       eax,edi
  004dcca6  2bc5                  sub       eax,ebp
  004dcca8  c1f804                sar       eax,4
  004dccab  03c5                  add       eax,ebp
  004dccad  894620                mov       [esi+20h],eax

LAB_004dccb0:
  004dccb0  0fbf4614              movsx     eax,word [esi+14h]
  004dccb4  50                    push      eax
  004dccb5  e8628d0600            call      00545A1Ch
  004dccba  8bc8                  mov       ecx,eax
  004dccbc  d1f9                  sar       ecx,1
  004dccbe  03c8                  add       ecx,eax
  004dccc0  c1f906                sar       ecx,6
  004dccc3  81c180000000          add       ecx,80h
  004dccc9  81e1ff000000          and       ecx,0FFh
  004dcccf  8bd1                  mov       edx,ecx
  004dccd1  c1e208                shl       edx,8
  004dccd4  0bd1                  or        edx,ecx
  004dccd6  c1e208                shl       edx,8
  004dccd9  0bd1                  or        edx,ecx
  004dccdb  66837e160a            cmp       word [esi+16h],0Ah
  004dcce0  895638                mov       [esi+38h],edx
  004dcce3  7c0d                  jl        short 004DCCF2h
  004dcce5  8b442420              mov       eax,[esp+20h]
  004dcce9  50                    push      eax
  004dccea  56                    push      esi
  004dcceb  e8806a0000            call      004E3770h
  004dccf0  eb1d                  jmp       short 004DCD0Fh

LAB_004dccf2:
  004dccf2  8bbe90000000          mov       edi,[esi+90h]
  004dccf8  8b4c2420              mov       ecx,[esp+20h]
  004dccfc  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004dcd02  51                    push      ecx
  004dcd03  56                    push      esi
  004dcd04  89be90000000          mov       [esi+90h],edi
  004dcd0a  e8e14ffcff            call      004A1CF0h

LAB_004dcd0f:
  004dcd0f  8b442424              mov       eax,[esp+24h]
  004dcd13  83c408                add       esp,8
  004dcd16  85c0                  test      eax,eax
  004dcd18  0f84ce010000          je        near 004DCEECh
  004dcd1e  f70616080000          test      dword [esi],816h
  004dcd24  0f85c2010000          jne       near 004DCEECh
  004dcd2a  8a4643                mov       al,[esi+43h]
  004dcd2d  84c0                  test      al,al
  004dcd2f  0f85b7010000          jne       near 004DCEECh
  004dcd35  8b466c                mov       eax,[esi+6Ch]
  004dcd38  8b561c                mov       edx,[esi+1Ch]
  004dcd3b  8b781c                mov       edi,[eax+1Ch]
  004dcd3e  8b6824                mov       ebp,[eax+24h]
  004dcd41  8b4624                mov       eax,[esi+24h]
  004dcd44  2bfa                  sub       edi,edx
  004dcd46  2be8                  sub       ebp,eax
  004dcd48  c1ff0c                sar       edi,0Ch
  004dcd4b  c1fd0c                sar       ebp,0Ch
  004dcd4e  8bd7                  mov       edx,edi
  004dcd50  8bc5                  mov       eax,ebp
  004dcd52  0fafd7                imul      edx,edi
  004dcd55  0fafc5                imul      eax,ebp
  004dcd58  03d0                  add       edx,eax
  004dcd5a  52                    push      edx
  004dcd5b  e8b08c0600            call      00545A10h
  004dcd60  55                    push      ebp
  004dcd61  57                    push      edi
  004dcd62  89442418              mov       [esp+18h],eax
  004dcd66  e8b78c0600            call      00545A22h
  004dcd6b  668b4e32              mov       cx,[esi+32h]
  004dcd6f  2bc1                  sub       eax,ecx
  004dcd71  25ff0f0000            and       eax,0FFFh
  004dcd76  89442414              mov       [esp+14h],eax
  004dcd7a  8b442410              mov       eax,[esp+10h]
  004dcd7e  3d2c010000            cmp       eax,12Ch
  004dcd83  0f8dbc000000          jge       near 004DCE45h
  004dcd89  e8b2a30600            call      00547140h
  004dcd8e  99                    cdq
  004dcd8f  b964000000            mov       ecx,64h
  004dcd94  f7f9                  idiv      ecx
  004dcd96  83fa3c                cmp       edx,3Ch
  004dcd99  7d5c                  jge       short 004DCDF7h
  004dcd9b  8b542414              mov       edx,[esp+14h]
  004dcd9f  8b7c2418              mov       edi,[esp+18h]
  004dcda3  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004dcdaa  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004dcdb1  25ff0f0000            and       eax,0FFFh
  004dcdb6  3bc1                  cmp       eax,ecx
  004dcdb8  0f8ff8000000          jg        near 004DCEB6h
  004dcdbe  e87da30600            call      00547140h
  004dcdc3  99                    cdq
  004dcdc4  8d4c3f01              lea       ecx,[edi+edi+1]
  004dcdc8  f7f9                  idiv      ecx
  004dcdca  8b4c2410              mov       ecx,[esp+10h]
  004dcdce  b8fa000000            mov       eax,0FAh
  004dcdd3  2bc2                  sub       eax,edx
  004dcdd5  3bc8                  cmp       ecx,eax
  004dcdd7  0f8dd9000000          jge       near 004DCEB6h
  004dcddd  8b1e                  mov       ebx,[esi]
  004dcddf  8bcf                  mov       ecx,edi
  004dcde1  c1f903                sar       ecx,3
  004dcde4  83cb02                or        ebx,2
  004dcde7  80c110                add       cl,10h
  004dcdea  891e                  mov       [esi],ebx
  004dcdec  884e43                mov       [esi+43h],cl
  004dcdef  5f                    pop       edi
  004dcdf0  5e                    pop       esi
  004dcdf1  5d                    pop       ebp
  004dcdf2  5b                    pop       ebx
  004dcdf3  83c42c                add       esp,2Ch
  004dcdf6  c3                    ret

LAB_004dcdf7:
  004dcdf7  c6464203              mov       byte [esi+42h],3
  004dcdfb  e840a30600            call      00547140h
  004dce00  99                    cdq
  004dce01  b90a000000            mov       ecx,0Ah
  004dce06  f7f9                  idiv      ecx
  004dce08  8b442418              mov       eax,[esp+18h]
  004dce0c  c1f802                sar       eax,2
  004dce0f  02d0                  add       dl,al
  004dce11  02d1                  add       dl,cl
  004dce13  8b4c2440              mov       ecx,[esp+40h]
  004dce17  885643                mov       [esi+43h],dl
  004dce1a  8db980000000          lea       edi,[ecx+80h]
  004dce20  c1e704                shl       edi,4
  004dce23  e818a30600            call      00547140h
  004dce28  99                    cdq
  004dce29  f7ff                  idiv      edi
  004dce2b  8b4c2414              mov       ecx,[esp+14h]
  004dce2f  d1ff                  sar       edi,1
  004dce31  2bd7                  sub       edx,edi
  004dce33  03d1                  add       edx,ecx
  004dce35  66035632              add       dx,[esi+32h]
  004dce39  66895610              mov       [esi+10h],dx
  004dce3d  5f                    pop       edi
  004dce3e  5e                    pop       esi
  004dce3f  5d                    pop       ebp
  004dce40  5b                    pop       ebx
  004dce41  83c42c                add       esp,2Ch
  004dce44  c3                    ret

LAB_004dce45:
  004dce45  3d58020000            cmp       eax,258h
  004dce4a  7d2c                  jge       short 004DCE78h
  004dce4c  e8efa20600            call      00547140h
  004dce51  c6464202              mov       byte [esi+42h],2
  004dce55  e8e6a20600            call      00547140h
  004dce5a  99                    cdq
  004dce5b  b90a000000            mov       ecx,0Ah
  004dce60  f7f9                  idiv      ecx
  004dce62  8b442418              mov       eax,[esp+18h]
  004dce66  c1f802                sar       eax,2
  004dce69  02d0                  add       dl,al
  004dce6b  02d1                  add       dl,cl
  004dce6d  885643                mov       [esi+43h],dl
  004dce70  5f                    pop       edi
  004dce71  5e                    pop       esi
  004dce72  5d                    pop       ebp
  004dce73  5b                    pop       ebx
  004dce74  83c42c                add       esp,2Ch
  004dce77  c3                    ret

LAB_004dce78:
  004dce78  e8c3a20600            call      00547140h
  004dce7d  99                    cdq
  004dce7e  b964000000            mov       ecx,64h
  004dce83  f7f9                  idiv      ecx
  004dce85  83fa1e                cmp       edx,1Eh
  004dce88  7d27                  jge       short 004DCEB1h
  004dce8a  c6464200              mov       byte [esi+42h],0
  004dce8e  e8ada20600            call      00547140h
  004dce93  99                    cdq
  004dce94  b90a000000            mov       ecx,0Ah
  004dce99  f7f9                  idiv      ecx
  004dce9b  8b442418              mov       eax,[esp+18h]
  004dce9f  c1f802                sar       eax,2
  004dcea2  02d0                  add       dl,al
  004dcea4  02d1                  add       dl,cl
  004dcea6  885643                mov       [esi+43h],dl
  004dcea9  5f                    pop       edi
  004dceaa  5e                    pop       esi
  004dceab  5d                    pop       ebp
  004dceac  5b                    pop       ebx
  004dcead  83c42c                add       esp,2Ch
  004dceb0  c3                    ret

LAB_004dceb1:
  004dceb1  83fa3c                cmp       edx,3Ch
  004dceb4  7d0e                  jge       short 004DCEC4h

LAB_004dceb6:
  004dceb6  885e42                mov       [esi+42h],bl
  004dceb9  885e43                mov       [esi+43h],bl
  004dcebc  5f                    pop       edi
  004dcebd  5e                    pop       esi
  004dcebe  5d                    pop       ebp
  004dcebf  5b                    pop       ebx

LAB_004dcec0:
  004dcec0  83c42c                add       esp,2Ch
  004dcec3  c3                    ret

LAB_004dcec4:
  004dcec4  c6464203              mov       byte [esi+42h],3
  004dcec8  e873a20600            call      00547140h
  004dcecd  99                    cdq
  004dcece  b90a000000            mov       ecx,0Ah
  004dced3  f7f9                  idiv      ecx
  004dced5  8b442418              mov       eax,[esp+18h]
  004dced9  c1f802                sar       eax,2
  004dcedc  02d0                  add       dl,al
  004dcede  02d1                  add       dl,cl
  004dcee0  885643                mov       [esi+43h],dl
  004dcee3  e858a20600            call      00547140h
  004dcee8  66894610              mov       [esi+10h],ax

LAB_004dceec:
  004dceec  5f                    pop       edi
  004dceed  5e                    pop       esi
  004dceee  5d                    pop       ebp
  004dceef  5b                    pop       ebx
  004dcef0  83c42c                add       esp,2Ch
  004dcef3  c3                    ret
  004dcef4  72ca                  jb        short 004DCEC0h
  004dcef6  4d                    dec       ebp
  004dcef7  00a3ca4d0001          add       [ebx+1004DCAh],ah
  004dcefd  cb                    retf
  004dcefe  4d                    dec       ebp
  004dceff  0072cb                add       [edx-35h],dh
  004dcf02  4d                    dec       ebp
  004dcf03  009090909090          add       [eax-6F6F6F70h],dl
  004dcf09  90                    nop
  004dcf0a  90                    nop
  004dcf0b  90                    nop
  004dcf0c  90                    nop
  004dcf0d  90                    nop
  004dcf0e  90                    nop
  004dcf0f  90                    nop

33 cible(s) de saut interne : 0x004dc93e, 0x004dc98e, 0x004dc9ec, 0x004dca1f, 0x004dca4a, 0x004dca85, 0x004dcadb, 0x004dcaeb, 0x004dcaef, 0x004dcb14, 0x004dcb20, 0x004dcb46, 0x004dcb56, 0x004dcb5a, 0x004dcb7e, 0x004dcbd1, 0x004dcbdb, 0x004dcbe1, 0x004dcc2d, 0x004dcc53, 0x004dcc76, 0x004dcc90, 0x004dccb0, 0x004dccf2, 0x004dcd0f, 0x004dcdf7, 0x004dce45, 0x004dce78, 0x004dceb1, 0x004dceb6, 0x004dcec0, 0x004dcec4, 0x004dceec
