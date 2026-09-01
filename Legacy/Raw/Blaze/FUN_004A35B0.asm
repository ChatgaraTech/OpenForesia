C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a35b0  section .text  offset fichier 0xa29b0  1636 octets

  004a35b0  83ec2c                sub       esp,2Ch
  004a35b3  53                    push      ebx
  004a35b4  55                    push      ebp
  004a35b5  56                    push      esi
  004a35b6  8b74243c              mov       esi,[esp+3Ch]
  004a35ba  8d442424              lea       eax,[esp+24h]
  004a35be  57                    push      edi
  004a35bf  8d4c242c              lea       ecx,[esp+2Ch]
  004a35c3  50                    push      eax
  004a35c4  8d542424              lea       edx,[esp+24h]
  004a35c8  51                    push      ecx
  004a35c9  33db                  xor       ebx,ebx
  004a35cb  52                    push      edx
  004a35cc  56                    push      esi
  004a35cd  66895c2440            mov       [esp+40h],bx
  004a35d2  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  004a35d9  66c74424443c00        mov       word [esp+44h],3Ch
  004a35e0  66c74424469600        mov       word [esp+46h],96h
  004a35e7  66895c2448            mov       [esp+48h],bx
  004a35ec  66c744244a7800        mov       word [esp+4Ah],78h
  004a35f3  e888530900            call      00538980h
  004a35f8  83c410                add       esp,10h
  004a35fb  8d442424              lea       eax,[esp+24h]
  004a35ff  8d4c241c              lea       ecx,[esp+1Ch]
  004a3603  8d542414              lea       edx,[esp+14h]
  004a3607  50                    push      eax
  004a3608  51                    push      ecx
  004a3609  8d442418              lea       eax,[esp+18h]
  004a360d  52                    push      edx
  004a360e  8d4c244c              lea       ecx,[esp+4Ch]
  004a3612  50                    push      eax
  004a3613  51                    push      ecx
  004a3614  56                    push      esi
  004a3615  e806290900            call      00535F20h
  004a361a  8b4c2458              mov       ecx,[esp+58h]
  004a361e  b840000000            mov       eax,40h
  004a3623  2bc1                  sub       eax,ecx
  004a3625  83c418                add       esp,18h
  004a3628  89442418              mov       [esp+18h],eax
  004a362c  8b44241c              mov       eax,[esp+1Ch]
  004a3630  3bc3                  cmp       eax,ebx
  004a3632  0f8415030000          je        near 004A394Dh
  004a3638  56                    push      esi
  004a3639  e8627f0300            call      004DB5A0h
  004a363e  8b06                  mov       eax,[esi]
  004a3640  83c404                add       esp,4
  004a3643  a802                  test      al,2
  004a3645  0f841f010000          je        near 004A376Ah
  004a364b  f644242410            test      byte [esp+24h],10h
  004a3650  740c                  je        short 004A365Eh
  004a3652  24fd                  and       al,0FDh
  004a3654  885e43                mov       [esi+43h],bl
  004a3657  8906                  mov       [esi],eax
  004a3659  e9ef020000            jmp       004A394Dh

LAB_004a365e:
  004a365e  8d542430              lea       edx,[esp+30h]
  004a3662  53                    push      ebx
  004a3663  8d461c                lea       eax,[esi+1Ch]
  004a3666  52                    push      edx
  004a3667  50                    push      eax
  004a3668  56                    push      esi
  004a3669  e8c2650700            call      00519C30h
  004a366e  8bf8                  mov       edi,eax
  004a3670  a1b8e05d00            mov       eax,[5DE0B8h]
  004a3675  83c410                add       esp,10h
  004a3678  3bc3                  cmp       eax,ebx
  004a367a  7432                  je        short 004A36AEh
  004a367c  8b17                  mov       edx,[edi]
  004a367e  57                    push      edi
  004a367f  83ca40                or        edx,40h
  004a3682  56                    push      esi
  004a3683  66c787960000001000    mov       word [edi+96h],10h
  004a368c  8917                  mov       [edi],edx
  004a368e  e8ed640900            call      00539B80h
  004a3693  83c408                add       esp,8
  004a3696  83c71c                add       edi,1Ch
  004a3699  57                    push      edi
  004a369a  6800180000            push      1800h
  004a369f  6800030000            push      300h
  004a36a4  6a03                  push      3
  004a36a6  e875a40900            call      0053DB20h
  004a36ab  83c410                add       esp,10h

LAB_004a36ae:
  004a36ae  8b4c2440              mov       ecx,[esp+40h]
  004a36b2  8b465c                mov       eax,[esi+5Ch]
  004a36b5  81c180020000          add       ecx,280h
  004a36bb  c1e107                shl       ecx,7
  004a36be  2bc8                  sub       ecx,eax
  004a36c0  c1f904                sar       ecx,4
  004a36c3  03c8                  add       ecx,eax
  004a36c5  668b4628              mov       ax,[esi+28h]
  004a36c9  894e5c                mov       [esi+5Ch],ecx
  004a36cc  8a4e43                mov       cl,[esi+43h]
  004a36cf  80f908                cmp       cl,8
  004a36d2  0fbfd0                movsx     edx,ax
  004a36d5  7235                  jb        short 004A370Ch
  004a36d7  bf00300000            mov       edi,3000h
  004a36dc  2bfa                  sub       edi,edx
  004a36de  c1ff02                sar       edi,2
  004a36e1  03f8                  add       edi,eax
  004a36e3  668b462a              mov       ax,[esi+2Ah]
  004a36e7  0fbfd0                movsx     edx,ax
  004a36ea  66897e28              mov       [esi+28h],di
  004a36ee  bf00300000            mov       edi,3000h
  004a36f3  2bfa                  sub       edi,edx
  004a36f5  c1ff02                sar       edi,2
  004a36f8  03f8                  add       edi,eax
  004a36fa  668b462c              mov       ax,[esi+2Ch]
  004a36fe  66897e2a              mov       [esi+2Ah],di
  004a3702  bf00300000            mov       edi,3000h
  004a3707  0fbfd0                movsx     edx,ax
  004a370a  eb33                  jmp       short 004A373Fh

LAB_004a370c:
  004a370c  bf00100000            mov       edi,1000h
  004a3711  2bfa                  sub       edi,edx
  004a3713  c1ff02                sar       edi,2
  004a3716  03f8                  add       edi,eax
  004a3718  668b462a              mov       ax,[esi+2Ah]
  004a371c  0fbfd0                movsx     edx,ax
  004a371f  66897e28              mov       [esi+28h],di
  004a3723  bf00100000            mov       edi,1000h
  004a3728  2bfa                  sub       edi,edx
  004a372a  c1ff02                sar       edi,2
  004a372d  03f8                  add       edi,eax
  004a372f  668b462c              mov       ax,[esi+2Ch]
  004a3733  66897e2a              mov       [esi+2Ah],di
  004a3737  bf00100000            mov       edi,1000h
  004a373c  0fbfd0                movsx     edx,ax

LAB_004a373f:
  004a373f  2bfa                  sub       edi,edx
  004a3741  c1ff02                sar       edi,2
  004a3744  03f8                  add       edi,eax
  004a3746  84c9                  test      cl,cl
  004a3748  66897e2c              mov       [esi+2Ch],di
  004a374c  0f84fb010000          je        near 004A394Dh
  004a3752  8ac1                  mov       al,cl
  004a3754  fec8                  dec       al
  004a3756  884643                mov       [esi+43h],al
  004a3759  0f85ee010000          jne       near 004A394Dh
  004a375f  8b06                  mov       eax,[esi]
  004a3761  24fd                  and       al,0FDh
  004a3763  8906                  mov       [esi],eax
  004a3765  e9e3010000            jmp       004A394Dh

LAB_004a376a:
  004a376a  a810                  test      al,10h
  004a376c  0f85db010000          jne       near 004A394Dh
  004a3772  a904080000            test      eax,804h
  004a3777  0f85d0010000          jne       near 004A394Dh
  004a377d  33c0                  xor       eax,eax
  004a377f  8a4642                mov       al,[esi+42h]
  004a3782  83f803                cmp       eax,3
  004a3785  0f8776010000          ja        near 004A3901h
  004a378b  ff2485143c4a00        jmp       dword [eax*4+4A3C14h]
  004a3792  817c24102c010000      cmp       dword [esp+10h],12Ch
  004a379a  7d09                  jge       short 004A37A5h
  004a379c  c6464300              mov       byte [esi+43h],0
  004a37a0  e95c010000            jmp       004A3901h

LAB_004a37a5:
  004a37a5  8a4643                mov       al,[esi+43h]
  004a37a8  84c0                  test      al,al
  004a37aa  7463                  je        short 004A380Fh
  004a37ac  fec8                  dec       al
  004a37ae  884643                mov       [esi+43h],al
  004a37b1  8b465c                mov       eax,[esi+5Ch]
  004a37b4  8bc8                  mov       ecx,eax
  004a37b6  c1f904                sar       ecx,4
  004a37b9  2bc1                  sub       eax,ecx
  004a37bb  89465c                mov       [esi+5Ch],eax
  004a37be  e93e010000            jmp       004A3901h
  004a37c3  8b442440              mov       eax,[esp+40h]
  004a37c7  8b4c2414              mov       ecx,[esp+14h]
  004a37cb  c1f802                sar       eax,2
  004a37ce  83c038                add       eax,38h
  004a37d1  8d1408                lea       edx,[eax+ecx]
  004a37d4  8d3c00                lea       edi,[eax+eax]
  004a37d7  81e2ff0f0000          and       edx,0FFFh
  004a37dd  3bd7                  cmp       edx,edi
  004a37df  7f1a                  jg        short 004A37FBh
  004a37e1  8b465c                mov       eax,[esi+5Ch]
  004a37e4  66014e32              add       [esi+32h],cx
  004a37e8  8bc8                  mov       ecx,eax
  004a37ea  c6464300              mov       byte [esi+43h],0
  004a37ee  c1f904                sar       ecx,4
  004a37f1  2bc1                  sub       eax,ecx
  004a37f3  89465c                mov       [esi+5Ch],eax
  004a37f6  e906010000            jmp       004A3901h

LAB_004a37fb:
  004a37fb  81e1ff0f0000          and       ecx,0FFFh
  004a3801  81f900080000          cmp       ecx,800h
  004a3807  7c02                  jl        short 004A380Bh
  004a3809  f7d8                  neg       eax

LAB_004a380b:
  004a380b  66014632              add       [esi+32h],ax

LAB_004a380f:
  004a380f  8b465c                mov       eax,[esi+5Ch]
  004a3812  8bc8                  mov       ecx,eax
  004a3814  c1f904                sar       ecx,4
  004a3817  2bc1                  sub       eax,ecx
  004a3819  89465c                mov       [esi+5Ch],eax
  004a381c  e9e0000000            jmp       004A3901h
  004a3821  817c24102c010000      cmp       dword [esp+10h],12Ch
  004a3829  7d09                  jge       short 004A3834h
  004a382b  c6464300              mov       byte [esi+43h],0
  004a382f  e9cd000000            jmp       004A3901h

LAB_004a3834:
  004a3834  8a4643                mov       al,[esi+43h]
  004a3837  84c0                  test      al,al
  004a3839  7405                  je        short 004A3840h
  004a383b  fec8                  dec       al
  004a383d  884643                mov       [esi+43h],al

LAB_004a3840:
  004a3840  8b542440              mov       edx,[esp+40h]
  004a3844  8b4c2414              mov       ecx,[esp+14h]
  004a3848  8bc2                  mov       eax,edx
  004a384a  c1f802                sar       eax,2
  004a384d  83c038                add       eax,38h
  004a3850  8d3c08                lea       edi,[eax+ecx]
  004a3853  8d1c00                lea       ebx,[eax+eax]
  004a3856  81e7ff0f0000          and       edi,0FFFh
  004a385c  3bfb                  cmp       edi,ebx
  004a385e  7f06                  jg        short 004A3866h
  004a3860  66014e32              add       [esi+32h],cx
  004a3864  eb14                  jmp       short 004A387Ah

LAB_004a3866:
  004a3866  81e1ff0f0000          and       ecx,0FFFh
  004a386c  81f900080000          cmp       ecx,800h
  004a3872  7c02                  jl        short 004A3876h
  004a3874  f7d8                  neg       eax

LAB_004a3876:
  004a3876  66014632              add       [esi+32h],ax

LAB_004a387a:
  004a387a  8b465c                mov       eax,[esi+5Ch]
  004a387d  81c2a0010000          add       edx,1A0h
  004a3883  c1e207                shl       edx,7
  004a3886  2bd0                  sub       edx,eax
  004a3888  c1fa04                sar       edx,4
  004a388b  03d0                  add       edx,eax
  004a388d  89565c                mov       [esi+5Ch],edx
  004a3890  eb6f                  jmp       short 004A3901h
  004a3892  8a4643                mov       al,[esi+43h]
  004a3895  84c0                  test      al,al
  004a3897  7405                  je        short 004A389Eh
  004a3899  fec8                  dec       al
  004a389b  884643                mov       [esi+43h],al

LAB_004a389e:
  004a389e  8b4c2440              mov       ecx,[esp+40h]
  004a38a2  8b465c                mov       eax,[esi+5Ch]
  004a38a5  8d9140010000          lea       edx,[ecx+140h]
  004a38ab  c1e207                shl       edx,7
  004a38ae  2bd0                  sub       edx,eax
  004a38b0  c1fa04                sar       edx,4
  004a38b3  03d0                  add       edx,eax
  004a38b5  668b4610              mov       ax,[esi+10h]
  004a38b9  89565c                mov       [esi+5Ch],edx
  004a38bc  668b5632              mov       dx,[esi+32h]
  004a38c0  2bc2                  sub       eax,edx
  004a38c2  25ff0f0000            and       eax,0FFFh
  004a38c7  8db800010000          lea       edi,[eax+100h]
  004a38cd  81e7ff0f0000          and       edi,0FFFh
  004a38d3  81ff00020000          cmp       edi,200h
  004a38d9  7e26                  jle       short 004A3901h
  004a38db  25ff0f0000            and       eax,0FFFh
  004a38e0  3d00080000            cmp       eax,800h
  004a38e5  7d0a                  jge       short 004A38F1h
  004a38e7  c1f902                sar       ecx,2
  004a38ea  83c138                add       ecx,38h
  004a38ed  8bc1                  mov       eax,ecx
  004a38ef  eb0a                  jmp       short 004A38FBh

LAB_004a38f1:
  004a38f1  c1f902                sar       ecx,2
  004a38f4  b8c8ffffff            mov       eax,0FFFFFFC8h
  004a38f9  2bc1                  sub       eax,ecx

LAB_004a38fb:
  004a38fb  03d0                  add       edx,eax
  004a38fd  66895632              mov       [esi+32h],dx

LAB_004a3901:
  004a3901  0fbf4614              movsx     eax,word [esi+14h]
  004a3905  50                    push      eax
  004a3906  e811210a00            call      00545A1Ch
  004a390b  668b4e28              mov       cx,[esi+28h]
  004a390f  c1f802                sar       eax,2
  004a3912  0500100000            add       eax,1000h
  004a3917  0fbfd1                movsx     edx,cx
  004a391a  8bf8                  mov       edi,eax
  004a391c  2bfa                  sub       edi,edx
  004a391e  c1ff02                sar       edi,2
  004a3921  03f9                  add       edi,ecx
  004a3923  668b4e2a              mov       cx,[esi+2Ah]
  004a3927  0fbfd1                movsx     edx,cx
  004a392a  66897e28              mov       [esi+28h],di
  004a392e  8bf8                  mov       edi,eax
  004a3930  2bfa                  sub       edi,edx
  004a3932  c1ff02                sar       edi,2
  004a3935  03f9                  add       edi,ecx
  004a3937  668b4e2c              mov       cx,[esi+2Ch]
  004a393b  0fbfd1                movsx     edx,cx
  004a393e  2bc2                  sub       eax,edx
  004a3940  66897e2a              mov       [esi+2Ah],di
  004a3944  c1f802                sar       eax,2
  004a3947  03c1                  add       eax,ecx
  004a3949  6689462c              mov       [esi+2Ch],ax

LAB_004a394d:
  004a394d  8b44241c              mov       eax,[esp+1Ch]
  004a3951  50                    push      eax
  004a3952  56                    push      esi
  004a3953  e8886fffff            call      0049A8E0h
  004a3958  83c408                add       esp,8
  004a395b  56                    push      esi
  004a395c  e8dfff0800            call      00533940h
  004a3961  8b466c                mov       eax,[esi+6Ch]
  004a3964  b301                  mov       bl,1
  004a3966  83c404                add       esp,4
  004a3969  8418                  test      [eax],bl
  004a396b  7506                  jne       short 004A3973h
  004a396d  8b4860                mov       ecx,[eax+60h]
  004a3970  894e48                mov       [esi+48h],ecx

LAB_004a3973:
  004a3973  e8c8370a00            call      00547140h
  004a3978  8b7e60                mov       edi,[esi+60h]
  004a397b  25ff000000            and       eax,0FFh
  004a3980  0500010000            add       eax,100h
  004a3985  66014614              add       [esi+14h],ax
  004a3989  8b4648                mov       eax,[esi+48h]
  004a398c  668b4e14              mov       cx,[esi+14h]
  004a3990  3bc7                  cmp       eax,edi
  004a3992  7c02                  jl        short 004A3996h
  004a3994  8bf8                  mov       edi,eax

LAB_004a3996:
  004a3996  f60602                test      byte [esi],2
  004a3999  7415                  je        short 004A39B0h
  004a399b  8b4620                mov       eax,[esi+20h]
  004a399e  2bf8                  sub       edi,eax
  004a39a0  81c700800200          add       edi,28000h
  004a39a6  c1ff03                sar       edi,3
  004a39a9  03f8                  add       edi,eax
  004a39ab  897e20                mov       [esi+20h],edi
  004a39ae  eb20                  jmp       short 004A39D0h

LAB_004a39b0:
  004a39b0  8b6e20                mov       ebp,[esi+20h]
  004a39b3  0fbfd1                movsx     edx,cx
  004a39b6  52                    push      edx
  004a39b7  e860200a00            call      00545A1Ch
  004a39bc  0500320000            add       eax,3200h
  004a39c1  c1e006                shl       eax,6
  004a39c4  03c7                  add       eax,edi
  004a39c6  2bc5                  sub       eax,ebp
  004a39c8  c1f804                sar       eax,4
  004a39cb  03c5                  add       eax,ebp
  004a39cd  894620                mov       [esi+20h],eax

LAB_004a39d0:
  004a39d0  0fbf4614              movsx     eax,word [esi+14h]
  004a39d4  50                    push      eax
  004a39d5  e842200a00            call      00545A1Ch
  004a39da  8bc8                  mov       ecx,eax
  004a39dc  d1f9                  sar       ecx,1
  004a39de  03c8                  add       ecx,eax
  004a39e0  c1f906                sar       ecx,6
  004a39e3  81c180000000          add       ecx,80h
  004a39e9  81e1ff000000          and       ecx,0FFh
  004a39ef  8bd1                  mov       edx,ecx
  004a39f1  c1e208                shl       edx,8
  004a39f4  0bd1                  or        edx,ecx
  004a39f6  c1e208                shl       edx,8
  004a39f9  0bd1                  or        edx,ecx
  004a39fb  66837e160a            cmp       word [esi+16h],0Ah
  004a3a00  895638                mov       [esi+38h],edx
  004a3a03  7c0d                  jl        short 004A3A12h
  004a3a05  8b442420              mov       eax,[esp+20h]
  004a3a09  50                    push      eax
  004a3a0a  56                    push      esi
  004a3a0b  e860fd0300            call      004E3770h
  004a3a10  eb1d                  jmp       short 004A3A2Fh

LAB_004a3a12:
  004a3a12  8bbe90000000          mov       edi,[esi+90h]
  004a3a18  8b4c2420              mov       ecx,[esp+20h]
  004a3a1c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004a3a22  51                    push      ecx
  004a3a23  56                    push      esi
  004a3a24  89be90000000          mov       [esi+90h],edi
  004a3a2a  e8c1e2ffff            call      004A1CF0h

LAB_004a3a2f:
  004a3a2f  8b442424              mov       eax,[esp+24h]
  004a3a33  83c408                add       esp,8
  004a3a36  85c0                  test      eax,eax
  004a3a38  0f84ce010000          je        near 004A3C0Ch
  004a3a3e  f70616080000          test      dword [esi],816h
  004a3a44  0f85c2010000          jne       near 004A3C0Ch
  004a3a4a  8a4643                mov       al,[esi+43h]
  004a3a4d  84c0                  test      al,al
  004a3a4f  0f85b7010000          jne       near 004A3C0Ch
  004a3a55  8b466c                mov       eax,[esi+6Ch]
  004a3a58  8b561c                mov       edx,[esi+1Ch]
  004a3a5b  8b781c                mov       edi,[eax+1Ch]
  004a3a5e  8b6824                mov       ebp,[eax+24h]
  004a3a61  8b4624                mov       eax,[esi+24h]
  004a3a64  2bfa                  sub       edi,edx
  004a3a66  2be8                  sub       ebp,eax
  004a3a68  c1ff0c                sar       edi,0Ch
  004a3a6b  c1fd0c                sar       ebp,0Ch
  004a3a6e  8bd7                  mov       edx,edi
  004a3a70  8bc5                  mov       eax,ebp
  004a3a72  0fafd7                imul      edx,edi
  004a3a75  0fafc5                imul      eax,ebp
  004a3a78  03d0                  add       edx,eax
  004a3a7a  52                    push      edx
  004a3a7b  e8901f0a00            call      00545A10h
  004a3a80  55                    push      ebp
  004a3a81  57                    push      edi
  004a3a82  89442418              mov       [esp+18h],eax
  004a3a86  e8971f0a00            call      00545A22h
  004a3a8b  668b4e32              mov       cx,[esi+32h]
  004a3a8f  2bc1                  sub       eax,ecx
  004a3a91  25ff0f0000            and       eax,0FFFh
  004a3a96  89442414              mov       [esp+14h],eax
  004a3a9a  8b442410              mov       eax,[esp+10h]
  004a3a9e  3d2c010000            cmp       eax,12Ch
  004a3aa3  0f8dbc000000          jge       near 004A3B65h
  004a3aa9  e892360a00            call      00547140h
  004a3aae  99                    cdq
  004a3aaf  b964000000            mov       ecx,64h
  004a3ab4  f7f9                  idiv      ecx
  004a3ab6  83fa3c                cmp       edx,3Ch
  004a3ab9  7d5c                  jge       short 004A3B17h
  004a3abb  8b542414              mov       edx,[esp+14h]
  004a3abf  8b7c2418              mov       edi,[esp+18h]
  004a3ac3  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a3aca  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a3ad1  25ff0f0000            and       eax,0FFFh
  004a3ad6  3bc1                  cmp       eax,ecx
  004a3ad8  0f8ff8000000          jg        near 004A3BD6h
  004a3ade  e85d360a00            call      00547140h
  004a3ae3  99                    cdq
  004a3ae4  8d4c3f01              lea       ecx,[edi+edi+1]
  004a3ae8  f7f9                  idiv      ecx
  004a3aea  8b4c2410              mov       ecx,[esp+10h]
  004a3aee  b8fa000000            mov       eax,0FAh
  004a3af3  2bc2                  sub       eax,edx
  004a3af5  3bc8                  cmp       ecx,eax
  004a3af7  0f8dd9000000          jge       near 004A3BD6h
  004a3afd  8b1e                  mov       ebx,[esi]
  004a3aff  8bcf                  mov       ecx,edi
  004a3b01  c1f903                sar       ecx,3
  004a3b04  83cb02                or        ebx,2
  004a3b07  80c110                add       cl,10h
  004a3b0a  891e                  mov       [esi],ebx
  004a3b0c  884e43                mov       [esi+43h],cl
  004a3b0f  5f                    pop       edi
  004a3b10  5e                    pop       esi
  004a3b11  5d                    pop       ebp
  004a3b12  5b                    pop       ebx
  004a3b13  83c42c                add       esp,2Ch
  004a3b16  c3                    ret

LAB_004a3b17:
  004a3b17  c6464203              mov       byte [esi+42h],3
  004a3b1b  e820360a00            call      00547140h
  004a3b20  99                    cdq
  004a3b21  b90a000000            mov       ecx,0Ah
  004a3b26  f7f9                  idiv      ecx
  004a3b28  8b442418              mov       eax,[esp+18h]
  004a3b2c  c1f802                sar       eax,2
  004a3b2f  02d0                  add       dl,al
  004a3b31  02d1                  add       dl,cl
  004a3b33  8b4c2440              mov       ecx,[esp+40h]
  004a3b37  885643                mov       [esi+43h],dl
  004a3b3a  8db980000000          lea       edi,[ecx+80h]
  004a3b40  c1e704                shl       edi,4
  004a3b43  e8f8350a00            call      00547140h
  004a3b48  99                    cdq
  004a3b49  f7ff                  idiv      edi
  004a3b4b  8b4c2414              mov       ecx,[esp+14h]
  004a3b4f  d1ff                  sar       edi,1
  004a3b51  2bd7                  sub       edx,edi
  004a3b53  03d1                  add       edx,ecx
  004a3b55  66035632              add       dx,[esi+32h]
  004a3b59  66895610              mov       [esi+10h],dx
  004a3b5d  5f                    pop       edi
  004a3b5e  5e                    pop       esi
  004a3b5f  5d                    pop       ebp
  004a3b60  5b                    pop       ebx
  004a3b61  83c42c                add       esp,2Ch
  004a3b64  c3                    ret

LAB_004a3b65:
  004a3b65  3d58020000            cmp       eax,258h
  004a3b6a  7d2c                  jge       short 004A3B98h
  004a3b6c  e8cf350a00            call      00547140h
  004a3b71  c6464202              mov       byte [esi+42h],2
  004a3b75  e8c6350a00            call      00547140h
  004a3b7a  99                    cdq
  004a3b7b  b90a000000            mov       ecx,0Ah
  004a3b80  f7f9                  idiv      ecx
  004a3b82  8b442418              mov       eax,[esp+18h]
  004a3b86  c1f802                sar       eax,2
  004a3b89  02d0                  add       dl,al
  004a3b8b  02d1                  add       dl,cl
  004a3b8d  885643                mov       [esi+43h],dl
  004a3b90  5f                    pop       edi
  004a3b91  5e                    pop       esi
  004a3b92  5d                    pop       ebp
  004a3b93  5b                    pop       ebx
  004a3b94  83c42c                add       esp,2Ch
  004a3b97  c3                    ret

LAB_004a3b98:
  004a3b98  e8a3350a00            call      00547140h
  004a3b9d  99                    cdq
  004a3b9e  b964000000            mov       ecx,64h
  004a3ba3  f7f9                  idiv      ecx
  004a3ba5  83fa1e                cmp       edx,1Eh
  004a3ba8  7d27                  jge       short 004A3BD1h
  004a3baa  c6464200              mov       byte [esi+42h],0
  004a3bae  e88d350a00            call      00547140h
  004a3bb3  99                    cdq
  004a3bb4  b90a000000            mov       ecx,0Ah
  004a3bb9  f7f9                  idiv      ecx
  004a3bbb  8b442418              mov       eax,[esp+18h]
  004a3bbf  c1f802                sar       eax,2
  004a3bc2  02d0                  add       dl,al
  004a3bc4  02d1                  add       dl,cl
  004a3bc6  885643                mov       [esi+43h],dl
  004a3bc9  5f                    pop       edi
  004a3bca  5e                    pop       esi
  004a3bcb  5d                    pop       ebp
  004a3bcc  5b                    pop       ebx
  004a3bcd  83c42c                add       esp,2Ch
  004a3bd0  c3                    ret

LAB_004a3bd1:
  004a3bd1  83fa3c                cmp       edx,3Ch
  004a3bd4  7d0e                  jge       short 004A3BE4h

LAB_004a3bd6:
  004a3bd6  885e42                mov       [esi+42h],bl
  004a3bd9  885e43                mov       [esi+43h],bl
  004a3bdc  5f                    pop       edi
  004a3bdd  5e                    pop       esi
  004a3bde  5d                    pop       ebp
  004a3bdf  5b                    pop       ebx
  004a3be0  83c42c                add       esp,2Ch
  004a3be3  c3                    ret

LAB_004a3be4:
  004a3be4  c6464203              mov       byte [esi+42h],3
  004a3be8  e853350a00            call      00547140h
  004a3bed  99                    cdq
  004a3bee  b90a000000            mov       ecx,0Ah
  004a3bf3  f7f9                  idiv      ecx
  004a3bf5  8b442418              mov       eax,[esp+18h]
  004a3bf9  c1f802                sar       eax,2
  004a3bfc  02d0                  add       dl,al
  004a3bfe  02d1                  add       dl,cl
  004a3c00  885643                mov       [esi+43h],dl
  004a3c03  e838350a00            call      00547140h
  004a3c08  66894610              mov       [esi+10h],ax

LAB_004a3c0c:
  004a3c0c  5f                    pop       edi
  004a3c0d  5e                    pop       esi
  004a3c0e  5d                    pop       ebp
  004a3c0f  5b                    pop       ebx
  004a3c10  83c42c                add       esp,2Ch
  004a3c13  c3                    ret

32 cible(s) de saut interne : 0x004a365e, 0x004a36ae, 0x004a370c, 0x004a373f, 0x004a376a, 0x004a37a5, 0x004a37fb, 0x004a380b, 0x004a380f, 0x004a3834, 0x004a3840, 0x004a3866, 0x004a3876, 0x004a387a, 0x004a389e, 0x004a38f1, 0x004a38fb, 0x004a3901, 0x004a394d, 0x004a3973, 0x004a3996, 0x004a39b0, 0x004a39d0, 0x004a3a12, 0x004a3a2f, 0x004a3b17, 0x004a3b65, 0x004a3b98, 0x004a3bd1, 0x004a3bd6, 0x004a3be4, 0x004a3c0c
