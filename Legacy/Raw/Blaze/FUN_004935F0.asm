Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004935f0  section .text  offset fichier 0x929f0  1664 octets

  004935f0  83ec2c                sub       esp,2Ch
  004935f3  53                    push      ebx
  004935f4  55                    push      ebp
  004935f5  56                    push      esi
  004935f6  8b74243c              mov       esi,[esp+3Ch]
  004935fa  8d442424              lea       eax,[esp+24h]
  004935fe  57                    push      edi
  004935ff  8d4c242c              lea       ecx,[esp+2Ch]
  00493603  50                    push      eax
  00493604  8d542424              lea       edx,[esp+24h]
  00493608  51                    push      ecx
  00493609  33db                  xor       ebx,ebx
  0049360b  52                    push      edx
  0049360c  56                    push      esi
  0049360d  66895c2440            mov       [esp+40h],bx
  00493612  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  00493619  66c74424443c00        mov       word [esp+44h],3Ch
  00493620  66c74424469600        mov       word [esp+46h],96h
  00493627  66895c2448            mov       [esp+48h],bx
  0049362c  66c744244a7800        mov       word [esp+4Ah],78h
  00493633  e848530a00            call      00538980h
  00493638  83c410                add       esp,10h
  0049363b  8d442424              lea       eax,[esp+24h]
  0049363f  8d4c241c              lea       ecx,[esp+1Ch]
  00493643  8d542414              lea       edx,[esp+14h]
  00493647  50                    push      eax
  00493648  51                    push      ecx
  00493649  8d442418              lea       eax,[esp+18h]
  0049364d  52                    push      edx
  0049364e  8d4c244c              lea       ecx,[esp+4Ch]
  00493652  50                    push      eax
  00493653  51                    push      ecx
  00493654  56                    push      esi
  00493655  e8c6280a00            call      00535F20h
  0049365a  8b4c2458              mov       ecx,[esp+58h]
  0049365e  b840000000            mov       eax,40h
  00493663  2bc1                  sub       eax,ecx
  00493665  83c418                add       esp,18h
  00493668  89442418              mov       [esp+18h],eax
  0049366c  8b44241c              mov       eax,[esp+1Ch]
  00493670  3bc3                  cmp       eax,ebx
  00493672  0f8415030000          je        near 0049398Dh
  00493678  56                    push      esi
  00493679  e8227f0400            call      004DB5A0h
  0049367e  8b06                  mov       eax,[esi]
  00493680  83c404                add       esp,4
  00493683  a802                  test      al,2
  00493685  0f841f010000          je        near 004937AAh
  0049368b  f644242410            test      byte [esp+24h],10h
  00493690  740c                  je        short 0049369Eh
  00493692  24fd                  and       al,0FDh
  00493694  885e43                mov       [esi+43h],bl
  00493697  8906                  mov       [esi],eax
  00493699  e9ef020000            jmp       0049398Dh

LAB_0049369e:
  0049369e  8d542430              lea       edx,[esp+30h]
  004936a2  53                    push      ebx
  004936a3  8d461c                lea       eax,[esi+1Ch]
  004936a6  52                    push      edx
  004936a7  50                    push      eax
  004936a8  56                    push      esi
  004936a9  e882650800            call      00519C30h
  004936ae  8bf8                  mov       edi,eax
  004936b0  a1b8e05d00            mov       eax,[5DE0B8h]
  004936b5  83c410                add       esp,10h
  004936b8  3bc3                  cmp       eax,ebx
  004936ba  7432                  je        short 004936EEh
  004936bc  8b17                  mov       edx,[edi]
  004936be  57                    push      edi
  004936bf  83ca40                or        edx,40h
  004936c2  56                    push      esi
  004936c3  66c787960000001000    mov       word [edi+96h],10h
  004936cc  8917                  mov       [edi],edx
  004936ce  e8ad640a00            call      00539B80h
  004936d3  83c408                add       esp,8
  004936d6  83c71c                add       edi,1Ch
  004936d9  57                    push      edi
  004936da  6800180000            push      1800h
  004936df  6800030000            push      300h
  004936e4  6a03                  push      3
  004936e6  e835a40a00            call      0053DB20h
  004936eb  83c410                add       esp,10h

LAB_004936ee:
  004936ee  8b4c2440              mov       ecx,[esp+40h]
  004936f2  8b465c                mov       eax,[esi+5Ch]
  004936f5  81c180020000          add       ecx,280h
  004936fb  c1e107                shl       ecx,7
  004936fe  2bc8                  sub       ecx,eax
  00493700  c1f904                sar       ecx,4
  00493703  03c8                  add       ecx,eax
  00493705  668b4628              mov       ax,[esi+28h]
  00493709  894e5c                mov       [esi+5Ch],ecx
  0049370c  8a4e43                mov       cl,[esi+43h]
  0049370f  80f908                cmp       cl,8
  00493712  0fbfd0                movsx     edx,ax
  00493715  7235                  jb        short 0049374Ch
  00493717  bf00300000            mov       edi,3000h
  0049371c  2bfa                  sub       edi,edx
  0049371e  c1ff02                sar       edi,2
  00493721  03f8                  add       edi,eax
  00493723  668b462a              mov       ax,[esi+2Ah]
  00493727  0fbfd0                movsx     edx,ax
  0049372a  66897e28              mov       [esi+28h],di
  0049372e  bf00300000            mov       edi,3000h
  00493733  2bfa                  sub       edi,edx
  00493735  c1ff02                sar       edi,2
  00493738  03f8                  add       edi,eax
  0049373a  668b462c              mov       ax,[esi+2Ch]
  0049373e  66897e2a              mov       [esi+2Ah],di
  00493742  bf00300000            mov       edi,3000h
  00493747  0fbfd0                movsx     edx,ax
  0049374a  eb33                  jmp       short 0049377Fh

LAB_0049374c:
  0049374c  bf00100000            mov       edi,1000h
  00493751  2bfa                  sub       edi,edx
  00493753  c1ff02                sar       edi,2
  00493756  03f8                  add       edi,eax
  00493758  668b462a              mov       ax,[esi+2Ah]
  0049375c  0fbfd0                movsx     edx,ax
  0049375f  66897e28              mov       [esi+28h],di
  00493763  bf00100000            mov       edi,1000h
  00493768  2bfa                  sub       edi,edx
  0049376a  c1ff02                sar       edi,2
  0049376d  03f8                  add       edi,eax
  0049376f  668b462c              mov       ax,[esi+2Ch]
  00493773  66897e2a              mov       [esi+2Ah],di
  00493777  bf00100000            mov       edi,1000h
  0049377c  0fbfd0                movsx     edx,ax

LAB_0049377f:
  0049377f  2bfa                  sub       edi,edx
  00493781  c1ff02                sar       edi,2
  00493784  03f8                  add       edi,eax
  00493786  84c9                  test      cl,cl
  00493788  66897e2c              mov       [esi+2Ch],di
  0049378c  0f84fb010000          je        near 0049398Dh
  00493792  8ac1                  mov       al,cl
  00493794  fec8                  dec       al
  00493796  884643                mov       [esi+43h],al
  00493799  0f85ee010000          jne       near 0049398Dh
  0049379f  8b06                  mov       eax,[esi]
  004937a1  24fd                  and       al,0FDh
  004937a3  8906                  mov       [esi],eax
  004937a5  e9e3010000            jmp       0049398Dh

LAB_004937aa:
  004937aa  a810                  test      al,10h
  004937ac  0f85db010000          jne       near 0049398Dh
  004937b2  a904080000            test      eax,804h
  004937b7  0f85d0010000          jne       near 0049398Dh
  004937bd  33c0                  xor       eax,eax
  004937bf  8a4642                mov       al,[esi+42h]
  004937c2  83f803                cmp       eax,3
  004937c5  0f8776010000          ja        near 00493941h
  004937cb  ff2485543c4900        jmp       dword [eax*4+493C54h]
  004937d2  817c24102c010000      cmp       dword [esp+10h],12Ch
  004937da  7d09                  jge       short 004937E5h
  004937dc  c6464300              mov       byte [esi+43h],0
  004937e0  e95c010000            jmp       00493941h

LAB_004937e5:
  004937e5  8a4643                mov       al,[esi+43h]
  004937e8  84c0                  test      al,al
  004937ea  7463                  je        short 0049384Fh
  004937ec  fec8                  dec       al
  004937ee  884643                mov       [esi+43h],al
  004937f1  8b465c                mov       eax,[esi+5Ch]
  004937f4  8bc8                  mov       ecx,eax
  004937f6  c1f904                sar       ecx,4
  004937f9  2bc1                  sub       eax,ecx
  004937fb  89465c                mov       [esi+5Ch],eax
  004937fe  e93e010000            jmp       00493941h
  00493803  8b442440              mov       eax,[esp+40h]
  00493807  8b4c2414              mov       ecx,[esp+14h]
  0049380b  c1f802                sar       eax,2
  0049380e  83c038                add       eax,38h
  00493811  8d1408                lea       edx,[eax+ecx]
  00493814  8d3c00                lea       edi,[eax+eax]
  00493817  81e2ff0f0000          and       edx,0FFFh
  0049381d  3bd7                  cmp       edx,edi
  0049381f  7f1a                  jg        short 0049383Bh
  00493821  8b465c                mov       eax,[esi+5Ch]
  00493824  66014e32              add       [esi+32h],cx
  00493828  8bc8                  mov       ecx,eax
  0049382a  c6464300              mov       byte [esi+43h],0
  0049382e  c1f904                sar       ecx,4
  00493831  2bc1                  sub       eax,ecx
  00493833  89465c                mov       [esi+5Ch],eax
  00493836  e906010000            jmp       00493941h

LAB_0049383b:
  0049383b  81e1ff0f0000          and       ecx,0FFFh
  00493841  81f900080000          cmp       ecx,800h
  00493847  7c02                  jl        short 0049384Bh
  00493849  f7d8                  neg       eax

LAB_0049384b:
  0049384b  66014632              add       [esi+32h],ax

LAB_0049384f:
  0049384f  8b465c                mov       eax,[esi+5Ch]
  00493852  8bc8                  mov       ecx,eax
  00493854  c1f904                sar       ecx,4
  00493857  2bc1                  sub       eax,ecx
  00493859  89465c                mov       [esi+5Ch],eax
  0049385c  e9e0000000            jmp       00493941h
  00493861  817c24102c010000      cmp       dword [esp+10h],12Ch
  00493869  7d09                  jge       short 00493874h
  0049386b  c6464300              mov       byte [esi+43h],0
  0049386f  e9cd000000            jmp       00493941h

LAB_00493874:
  00493874  8a4643                mov       al,[esi+43h]
  00493877  84c0                  test      al,al
  00493879  7405                  je        short 00493880h
  0049387b  fec8                  dec       al
  0049387d  884643                mov       [esi+43h],al

LAB_00493880:
  00493880  8b542440              mov       edx,[esp+40h]
  00493884  8b4c2414              mov       ecx,[esp+14h]
  00493888  8bc2                  mov       eax,edx
  0049388a  c1f802                sar       eax,2
  0049388d  83c038                add       eax,38h
  00493890  8d3c08                lea       edi,[eax+ecx]
  00493893  8d1c00                lea       ebx,[eax+eax]
  00493896  81e7ff0f0000          and       edi,0FFFh
  0049389c  3bfb                  cmp       edi,ebx
  0049389e  7f06                  jg        short 004938A6h
  004938a0  66014e32              add       [esi+32h],cx
  004938a4  eb14                  jmp       short 004938BAh

LAB_004938a6:
  004938a6  81e1ff0f0000          and       ecx,0FFFh
  004938ac  81f900080000          cmp       ecx,800h
  004938b2  7c02                  jl        short 004938B6h
  004938b4  f7d8                  neg       eax

LAB_004938b6:
  004938b6  66014632              add       [esi+32h],ax

LAB_004938ba:
  004938ba  8b465c                mov       eax,[esi+5Ch]
  004938bd  81c2a0010000          add       edx,1A0h
  004938c3  c1e207                shl       edx,7
  004938c6  2bd0                  sub       edx,eax
  004938c8  c1fa04                sar       edx,4
  004938cb  03d0                  add       edx,eax
  004938cd  89565c                mov       [esi+5Ch],edx
  004938d0  eb6f                  jmp       short 00493941h
  004938d2  8a4643                mov       al,[esi+43h]
  004938d5  84c0                  test      al,al
  004938d7  7405                  je        short 004938DEh
  004938d9  fec8                  dec       al
  004938db  884643                mov       [esi+43h],al

LAB_004938de:
  004938de  8b4c2440              mov       ecx,[esp+40h]
  004938e2  8b465c                mov       eax,[esi+5Ch]
  004938e5  8d9140010000          lea       edx,[ecx+140h]
  004938eb  c1e207                shl       edx,7
  004938ee  2bd0                  sub       edx,eax
  004938f0  c1fa04                sar       edx,4
  004938f3  03d0                  add       edx,eax
  004938f5  668b4610              mov       ax,[esi+10h]
  004938f9  89565c                mov       [esi+5Ch],edx
  004938fc  668b5632              mov       dx,[esi+32h]
  00493900  2bc2                  sub       eax,edx
  00493902  25ff0f0000            and       eax,0FFFh
  00493907  8db800010000          lea       edi,[eax+100h]
  0049390d  81e7ff0f0000          and       edi,0FFFh
  00493913  81ff00020000          cmp       edi,200h
  00493919  7e26                  jle       short 00493941h
  0049391b  25ff0f0000            and       eax,0FFFh
  00493920  3d00080000            cmp       eax,800h
  00493925  7d0a                  jge       short 00493931h
  00493927  c1f902                sar       ecx,2
  0049392a  83c138                add       ecx,38h
  0049392d  8bc1                  mov       eax,ecx
  0049392f  eb0a                  jmp       short 0049393Bh

LAB_00493931:
  00493931  c1f902                sar       ecx,2
  00493934  b8c8ffffff            mov       eax,0FFFFFFC8h
  00493939  2bc1                  sub       eax,ecx

LAB_0049393b:
  0049393b  03d0                  add       edx,eax
  0049393d  66895632              mov       [esi+32h],dx

LAB_00493941:
  00493941  0fbf4614              movsx     eax,word [esi+14h]
  00493945  50                    push      eax
  00493946  e8d1200b00            call      00545A1Ch
  0049394b  668b4e28              mov       cx,[esi+28h]
  0049394f  c1f802                sar       eax,2
  00493952  0500100000            add       eax,1000h
  00493957  0fbfd1                movsx     edx,cx
  0049395a  8bf8                  mov       edi,eax
  0049395c  2bfa                  sub       edi,edx
  0049395e  c1ff02                sar       edi,2
  00493961  03f9                  add       edi,ecx
  00493963  668b4e2a              mov       cx,[esi+2Ah]
  00493967  0fbfd1                movsx     edx,cx
  0049396a  66897e28              mov       [esi+28h],di
  0049396e  8bf8                  mov       edi,eax
  00493970  2bfa                  sub       edi,edx
  00493972  c1ff02                sar       edi,2
  00493975  03f9                  add       edi,ecx
  00493977  668b4e2c              mov       cx,[esi+2Ch]
  0049397b  0fbfd1                movsx     edx,cx
  0049397e  2bc2                  sub       eax,edx
  00493980  66897e2a              mov       [esi+2Ah],di
  00493984  c1f802                sar       eax,2
  00493987  03c1                  add       eax,ecx
  00493989  6689462c              mov       [esi+2Ch],ax

LAB_0049398d:
  0049398d  8b44241c              mov       eax,[esp+1Ch]
  00493991  50                    push      eax
  00493992  56                    push      esi
  00493993  e8486f0000            call      0049A8E0h
  00493998  83c408                add       esp,8
  0049399b  56                    push      esi
  0049399c  e89fff0900            call      00533940h
  004939a1  8b466c                mov       eax,[esi+6Ch]
  004939a4  b301                  mov       bl,1
  004939a6  83c404                add       esp,4
  004939a9  8418                  test      [eax],bl
  004939ab  7506                  jne       short 004939B3h
  004939ad  8b4860                mov       ecx,[eax+60h]
  004939b0  894e48                mov       [esi+48h],ecx

LAB_004939b3:
  004939b3  e888370b00            call      00547140h
  004939b8  8b7e60                mov       edi,[esi+60h]
  004939bb  25ff000000            and       eax,0FFh
  004939c0  0500010000            add       eax,100h
  004939c5  66014614              add       [esi+14h],ax
  004939c9  8b4648                mov       eax,[esi+48h]
  004939cc  668b4e14              mov       cx,[esi+14h]
  004939d0  3bc7                  cmp       eax,edi
  004939d2  7c02                  jl        short 004939D6h
  004939d4  8bf8                  mov       edi,eax

LAB_004939d6:
  004939d6  f60602                test      byte [esi],2
  004939d9  7415                  je        short 004939F0h
  004939db  8b4620                mov       eax,[esi+20h]
  004939de  2bf8                  sub       edi,eax
  004939e0  81c700800200          add       edi,28000h
  004939e6  c1ff03                sar       edi,3
  004939e9  03f8                  add       edi,eax
  004939eb  897e20                mov       [esi+20h],edi
  004939ee  eb20                  jmp       short 00493A10h

LAB_004939f0:
  004939f0  8b6e20                mov       ebp,[esi+20h]
  004939f3  0fbfd1                movsx     edx,cx
  004939f6  52                    push      edx
  004939f7  e820200b00            call      00545A1Ch
  004939fc  0500320000            add       eax,3200h
  00493a01  c1e006                shl       eax,6
  00493a04  03c7                  add       eax,edi
  00493a06  2bc5                  sub       eax,ebp
  00493a08  c1f804                sar       eax,4
  00493a0b  03c5                  add       eax,ebp
  00493a0d  894620                mov       [esi+20h],eax

LAB_00493a10:
  00493a10  0fbf4614              movsx     eax,word [esi+14h]
  00493a14  50                    push      eax
  00493a15  e802200b00            call      00545A1Ch
  00493a1a  8bc8                  mov       ecx,eax
  00493a1c  d1f9                  sar       ecx,1
  00493a1e  03c8                  add       ecx,eax
  00493a20  c1f906                sar       ecx,6
  00493a23  81c180000000          add       ecx,80h
  00493a29  81e1ff000000          and       ecx,0FFh
  00493a2f  8bd1                  mov       edx,ecx
  00493a31  c1e208                shl       edx,8
  00493a34  0bd1                  or        edx,ecx
  00493a36  c1e208                shl       edx,8
  00493a39  0bd1                  or        edx,ecx
  00493a3b  66837e160a            cmp       word [esi+16h],0Ah
  00493a40  895638                mov       [esi+38h],edx
  00493a43  7c0d                  jl        short 00493A52h
  00493a45  8b442420              mov       eax,[esp+20h]
  00493a49  50                    push      eax
  00493a4a  56                    push      esi
  00493a4b  e820fd0400            call      004E3770h
  00493a50  eb1d                  jmp       short 00493A6Fh

LAB_00493a52:
  00493a52  8bbe90000000          mov       edi,[esi+90h]
  00493a58  8b4c2420              mov       ecx,[esp+20h]
  00493a5c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  00493a62  51                    push      ecx
  00493a63  56                    push      esi
  00493a64  89be90000000          mov       [esi+90h],edi
  00493a6a  e881e20000            call      004A1CF0h

LAB_00493a6f:
  00493a6f  8b442424              mov       eax,[esp+24h]
  00493a73  83c408                add       esp,8
  00493a76  85c0                  test      eax,eax
  00493a78  0f84ce010000          je        near 00493C4Ch
  00493a7e  f70616080000          test      dword [esi],816h
  00493a84  0f85c2010000          jne       near 00493C4Ch
  00493a8a  8a4643                mov       al,[esi+43h]
  00493a8d  84c0                  test      al,al
  00493a8f  0f85b7010000          jne       near 00493C4Ch
  00493a95  8b466c                mov       eax,[esi+6Ch]
  00493a98  8b561c                mov       edx,[esi+1Ch]
  00493a9b  8b781c                mov       edi,[eax+1Ch]
  00493a9e  8b6824                mov       ebp,[eax+24h]
  00493aa1  8b4624                mov       eax,[esi+24h]
  00493aa4  2bfa                  sub       edi,edx
  00493aa6  2be8                  sub       ebp,eax
  00493aa8  c1ff0c                sar       edi,0Ch
  00493aab  c1fd0c                sar       ebp,0Ch
  00493aae  8bd7                  mov       edx,edi
  00493ab0  8bc5                  mov       eax,ebp
  00493ab2  0fafd7                imul      edx,edi
  00493ab5  0fafc5                imul      eax,ebp
  00493ab8  03d0                  add       edx,eax
  00493aba  52                    push      edx
  00493abb  e8501f0b00            call      00545A10h
  00493ac0  55                    push      ebp
  00493ac1  57                    push      edi
  00493ac2  89442418              mov       [esp+18h],eax
  00493ac6  e8571f0b00            call      00545A22h
  00493acb  668b4e32              mov       cx,[esi+32h]
  00493acf  2bc1                  sub       eax,ecx
  00493ad1  25ff0f0000            and       eax,0FFFh
  00493ad6  89442414              mov       [esp+14h],eax
  00493ada  8b442410              mov       eax,[esp+10h]
  00493ade  3d2c010000            cmp       eax,12Ch
  00493ae3  0f8dbc000000          jge       near 00493BA5h
  00493ae9  e852360b00            call      00547140h
  00493aee  99                    cdq
  00493aef  b964000000            mov       ecx,64h
  00493af4  f7f9                  idiv      ecx
  00493af6  83fa3c                cmp       edx,3Ch
  00493af9  7d5c                  jge       short 00493B57h
  00493afb  8b542414              mov       edx,[esp+14h]
  00493aff  8b7c2418              mov       edi,[esp+18h]
  00493b03  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00493b0a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  00493b11  25ff0f0000            and       eax,0FFFh
  00493b16  3bc1                  cmp       eax,ecx
  00493b18  0f8ff8000000          jg        near 00493C16h
  00493b1e  e81d360b00            call      00547140h
  00493b23  99                    cdq
  00493b24  8d4c3f01              lea       ecx,[edi+edi+1]
  00493b28  f7f9                  idiv      ecx
  00493b2a  8b4c2410              mov       ecx,[esp+10h]
  00493b2e  b8fa000000            mov       eax,0FAh
  00493b33  2bc2                  sub       eax,edx
  00493b35  3bc8                  cmp       ecx,eax
  00493b37  0f8dd9000000          jge       near 00493C16h
  00493b3d  8b1e                  mov       ebx,[esi]
  00493b3f  8bcf                  mov       ecx,edi
  00493b41  c1f903                sar       ecx,3
  00493b44  83cb02                or        ebx,2
  00493b47  80c110                add       cl,10h
  00493b4a  891e                  mov       [esi],ebx
  00493b4c  884e43                mov       [esi+43h],cl
  00493b4f  5f                    pop       edi
  00493b50  5e                    pop       esi
  00493b51  5d                    pop       ebp
  00493b52  5b                    pop       ebx
  00493b53  83c42c                add       esp,2Ch
  00493b56  c3                    ret

LAB_00493b57:
  00493b57  c6464203              mov       byte [esi+42h],3
  00493b5b  e8e0350b00            call      00547140h
  00493b60  99                    cdq
  00493b61  b90a000000            mov       ecx,0Ah
  00493b66  f7f9                  idiv      ecx
  00493b68  8b442418              mov       eax,[esp+18h]
  00493b6c  c1f802                sar       eax,2
  00493b6f  02d0                  add       dl,al
  00493b71  02d1                  add       dl,cl
  00493b73  8b4c2440              mov       ecx,[esp+40h]
  00493b77  885643                mov       [esi+43h],dl
  00493b7a  8db980000000          lea       edi,[ecx+80h]
  00493b80  c1e704                shl       edi,4
  00493b83  e8b8350b00            call      00547140h
  00493b88  99                    cdq
  00493b89  f7ff                  idiv      edi
  00493b8b  8b4c2414              mov       ecx,[esp+14h]
  00493b8f  d1ff                  sar       edi,1
  00493b91  2bd7                  sub       edx,edi
  00493b93  03d1                  add       edx,ecx
  00493b95  66035632              add       dx,[esi+32h]
  00493b99  66895610              mov       [esi+10h],dx
  00493b9d  5f                    pop       edi
  00493b9e  5e                    pop       esi
  00493b9f  5d                    pop       ebp
  00493ba0  5b                    pop       ebx
  00493ba1  83c42c                add       esp,2Ch
  00493ba4  c3                    ret

LAB_00493ba5:
  00493ba5  3d58020000            cmp       eax,258h
  00493baa  7d2c                  jge       short 00493BD8h
  00493bac  e88f350b00            call      00547140h
  00493bb1  c6464202              mov       byte [esi+42h],2
  00493bb5  e886350b00            call      00547140h
  00493bba  99                    cdq
  00493bbb  b90a000000            mov       ecx,0Ah
  00493bc0  f7f9                  idiv      ecx
  00493bc2  8b442418              mov       eax,[esp+18h]
  00493bc6  c1f802                sar       eax,2
  00493bc9  02d0                  add       dl,al
  00493bcb  02d1                  add       dl,cl
  00493bcd  885643                mov       [esi+43h],dl
  00493bd0  5f                    pop       edi
  00493bd1  5e                    pop       esi
  00493bd2  5d                    pop       ebp
  00493bd3  5b                    pop       ebx
  00493bd4  83c42c                add       esp,2Ch
  00493bd7  c3                    ret

LAB_00493bd8:
  00493bd8  e863350b00            call      00547140h
  00493bdd  99                    cdq
  00493bde  b964000000            mov       ecx,64h
  00493be3  f7f9                  idiv      ecx
  00493be5  83fa1e                cmp       edx,1Eh
  00493be8  7d27                  jge       short 00493C11h
  00493bea  c6464200              mov       byte [esi+42h],0
  00493bee  e84d350b00            call      00547140h
  00493bf3  99                    cdq
  00493bf4  b90a000000            mov       ecx,0Ah
  00493bf9  f7f9                  idiv      ecx
  00493bfb  8b442418              mov       eax,[esp+18h]
  00493bff  c1f802                sar       eax,2
  00493c02  02d0                  add       dl,al
  00493c04  02d1                  add       dl,cl
  00493c06  885643                mov       [esi+43h],dl
  00493c09  5f                    pop       edi
  00493c0a  5e                    pop       esi
  00493c0b  5d                    pop       ebp
  00493c0c  5b                    pop       ebx
  00493c0d  83c42c                add       esp,2Ch
  00493c10  c3                    ret

LAB_00493c11:
  00493c11  83fa3c                cmp       edx,3Ch
  00493c14  7d0e                  jge       short 00493C24h

LAB_00493c16:
  00493c16  885e42                mov       [esi+42h],bl
  00493c19  885e43                mov       [esi+43h],bl
  00493c1c  5f                    pop       edi
  00493c1d  5e                    pop       esi
  00493c1e  5d                    pop       ebp
  00493c1f  5b                    pop       ebx
  00493c20  83c42c                add       esp,2Ch
  00493c23  c3                    ret

LAB_00493c24:
  00493c24  c6464203              mov       byte [esi+42h],3
  00493c28  e813350b00            call      00547140h
  00493c2d  99                    cdq
  00493c2e  b90a000000            mov       ecx,0Ah
  00493c33  f7f9                  idiv      ecx
  00493c35  8b442418              mov       eax,[esp+18h]
  00493c39  c1f802                sar       eax,2
  00493c3c  02d0                  add       dl,al
  00493c3e  02d1                  add       dl,cl
  00493c40  885643                mov       [esi+43h],dl
  00493c43  e8f8340b00            call      00547140h
  00493c48  66894610              mov       [esi+10h],ax

LAB_00493c4c:
  00493c4c  5f                    pop       edi
  00493c4d  5e                    pop       esi
  00493c4e  5d                    pop       ebp
  00493c4f  5b                    pop       ebx
  00493c50  83c42c                add       esp,2Ch
  00493c53  c3                    ret
  00493c54  d237                  sal       byte [edi],cl
  00493c56  49                    dec       ecx
  00493c57  0003                  add       [ebx],al
  00493c59  384900                cmp       [ecx],cl
  00493c5c  61                    popa
  00493c5d  384900                cmp       [ecx],cl
  00493c60  d238                  sar       byte [eax],cl
  00493c62  49                    dec       ecx
  00493c63  009090909090          add       [eax-6F6F6F70h],dl
  00493c69  90                    nop
  00493c6a  90                    nop
  00493c6b  90                    nop
  00493c6c  90                    nop
  00493c6d  90                    nop
  00493c6e  90                    nop
  00493c6f  90                    nop

32 cible(s) de saut interne : 0x0049369e, 0x004936ee, 0x0049374c, 0x0049377f, 0x004937aa, 0x004937e5, 0x0049383b, 0x0049384b, 0x0049384f, 0x00493874, 0x00493880, 0x004938a6, 0x004938b6, 0x004938ba, 0x004938de, 0x00493931, 0x0049393b, 0x00493941, 0x0049398d, 0x004939b3, 0x004939d6, 0x004939f0, 0x00493a10, 0x00493a52, 0x00493a6f, 0x00493b57, 0x00493ba5, 0x00493bd8, 0x00493c11, 0x00493c16, 0x00493c24, 0x00493c4c
