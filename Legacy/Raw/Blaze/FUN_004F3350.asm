Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f3350  section .text  offset fichier 0xf2750  3412 octets

  004f3350  83ec7c                sub       esp,7Ch
  004f3353  53                    push      ebx
  004f3354  55                    push      ebp
  004f3355  56                    push      esi
  004f3356  8bb4248c000000        mov       esi,[esp+8Ch]
  004f335d  b864000000            mov       eax,64h
  004f3362  33db                  xor       ebx,ebx
  004f3364  668944245a            mov       [esp+5Ah],ax
  004f3369  668944245e            mov       [esp+5Eh],ax
  004f336e  8a4641                mov       al,[esi+41h]
  004f3371  57                    push      edi
  004f3372  3ac3                  cmp       al,bl
  004f3374  66895c2458            mov       [esp+58h],bx
  004f3379  66c744245a1e00        mov       word [esp+5Ah],1Eh
  004f3380  66c744245c3200        mov       word [esp+5Ch],32h
  004f3387  66895c2460            mov       [esp+60h],bx
  004f338c  7510                  jne       short 004F339Eh
  004f338e  53                    push      ebx
  004f338f  6a02                  push      2
  004f3391  56                    push      esi
  004f3392  c6464101              mov       byte [esi+41h],1
  004f3396  e8c5c4ffff            call      004EF860h
  004f339b  83c40c                add       esp,0Ch

LAB_004f339e:
  004f339e  8b461c                mov       eax,[esi+1Ch]
  004f33a1  8b4e20                mov       ecx,[esi+20h]
  004f33a4  8b5624                mov       edx,[esi+24h]
  004f33a7  8d6e1c                lea       ebp,[esi+1Ch]
  004f33aa  8944244c              mov       [esp+4Ch],eax
  004f33ae  668b4632              mov       ax,[esi+32h]
  004f33b2  894c2450              mov       [esp+50h],ecx
  004f33b6  8b4e7c                mov       ecx,[esi+7Ch]
  004f33b9  89542454              mov       [esp+54h],edx
  004f33bd  668b9690000000        mov       dx,[esi+90h]
  004f33c4  668944247a            mov       [esp+7Ah],ax
  004f33c9  8b8680000000          mov       eax,[esi+80h]
  004f33cf  894c2428              mov       [esp+28h],ecx
  004f33d3  8b8e84000000          mov       ecx,[esi+84h]
  004f33d9  668954242c            mov       [esp+2Ch],dx
  004f33de  8b9688000000          mov       edx,[esi+88h]
  004f33e4  8b7e6c                mov       edi,[esi+6Ch]
  004f33e7  89442430              mov       [esp+30h],eax
  004f33eb  8b868c000000          mov       eax,[esi+8Ch]
  004f33f1  894c2434              mov       [esp+34h],ecx
  004f33f5  89542438              mov       [esp+38h],edx
  004f33f9  8d4c2440              lea       ecx,[esp+40h]
  004f33fd  8944243c              mov       [esp+3Ch],eax
  004f3401  8d542444              lea       edx,[esp+44h]
  004f3405  51                    push      ecx
  004f3406  8d44244c              lea       eax,[esp+4Ch]
  004f340a  52                    push      edx
  004f340b  50                    push      eax
  004f340c  56                    push      esi
  004f340d  e86e550400            call      00538980h
  004f3412  83c410                add       esp,10h
  004f3415  8d4c2424              lea       ecx,[esp+24h]
  004f3419  8d542420              lea       edx,[esp+20h]
  004f341d  8d442418              lea       eax,[esp+18h]
  004f3421  51                    push      ecx
  004f3422  52                    push      edx
  004f3423  8d4c241c              lea       ecx,[esp+1Ch]
  004f3427  50                    push      eax
  004f3428  8d54241c              lea       edx,[esp+1Ch]
  004f342c  51                    push      ecx
  004f342d  52                    push      edx
  004f342e  56                    push      esi
  004f342f  e8ec2a0400            call      00535F20h
  004f3434  8b4c2428              mov       ecx,[esp+28h]
  004f3438  b840000000            mov       eax,40h
  004f343d  2bc1                  sub       eax,ecx
  004f343f  83c418                add       esp,18h
  004f3442  8944241c              mov       [esp+1Ch],eax
  004f3446  8b442424              mov       eax,[esp+24h]
  004f344a  50                    push      eax
  004f344b  56                    push      esi
  004f344c  e88f29feff            call      004D5DE0h
  004f3451  8b4500                mov       eax,[ebp]
  004f3454  8b571c                mov       edx,[edi+1Ch]
  004f3457  8b4e20                mov       ecx,[esi+20h]
  004f345a  2bc2                  sub       eax,edx
  004f345c  8b5720                mov       edx,[edi+20h]
  004f345f  83c408                add       esp,8
  004f3462  2bca                  sub       ecx,edx
  004f3464  8b5624                mov       edx,[esi+24h]
  004f3467  2b5724                sub       edx,[edi+24h]
  004f346a  c1f80c                sar       eax,0Ch
  004f346d  8bf8                  mov       edi,eax
  004f346f  c1f90c                sar       ecx,0Ch
  004f3472  0faff8                imul      edi,eax
  004f3475  8bc1                  mov       eax,ecx
  004f3477  c1fa0c                sar       edx,0Ch
  004f347a  0fafc1                imul      eax,ecx
  004f347d  8bca                  mov       ecx,edx
  004f347f  03f8                  add       edi,eax
  004f3481  0fafca                imul      ecx,edx
  004f3484  03f9                  add       edi,ecx
  004f3486  57                    push      edi
  004f3487  e884250500            call      00545A10h
  004f348c  8b4c2420              mov       ecx,[esp+20h]
  004f3490  89442414              mov       [esp+14h],eax
  004f3494  3bcb                  cmp       ecx,ebx
  004f3496  0f8435060000          je        near 004F3AD1h
  004f349c  8b16                  mov       edx,[esi]
  004f349e  33c0                  xor       eax,eax
  004f34a0  8a4604                mov       al,[esi+4]
  004f34a3  81e2fffffffd          and       edx,0FDFFFFFFh
  004f34a9  0fbf7e18              movsx     edi,word [esi+18h]
  004f34ad  8916                  mov       [esi],edx
  004f34af  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004f34b6  f7c200000001          test      edx,1000000h
  004f34bc  8b3cb8                mov       edi,[eax+edi*4]
  004f34bf  741b                  je        short 004F34DCh
  004f34c1  66ff461a              inc       word [esi+1Ah]
  004f34c5  668b461a              mov       ax,[esi+1Ah]
  004f34c9  33d2                  xor       edx,edx
  004f34cb  668b5716              mov       dx,[edi+16h]
  004f34cf  0fbfc0                movsx     eax,ax
  004f34d2  3bc2                  cmp       eax,edx
  004f34d4  7c2a                  jl        short 004F3500h
  004f34d6  66895e1a              mov       [esi+1Ah],bx
  004f34da  eb24                  jmp       short 004F3500h

LAB_004f34dc:
  004f34dc  66ff461a              inc       word [esi+1Ah]
  004f34e0  668b461a              mov       ax,[esi+1Ah]
  004f34e4  33db                  xor       ebx,ebx
  004f34e6  668b5f16              mov       bx,[edi+16h]
  004f34ea  0fbff8                movsx     edi,ax
  004f34ed  3bfb                  cmp       edi,ebx
  004f34ef  7c0d                  jl        short 004F34FEh
  004f34f1  48                    dec       eax
  004f34f2  81ca00000002          or        edx,2000000h
  004f34f8  6689461a              mov       [esi+1Ah],ax
  004f34fc  8916                  mov       [esi],edx

LAB_004f34fe:
  004f34fe  33db                  xor       ebx,ebx

LAB_004f3500:
  004f3500  8a4642                mov       al,[esi+42h]
  004f3503  3c05                  cmp       al,5
  004f3505  8b06                  mov       eax,[esi]
  004f3507  7504                  jne       short 004F350Dh
  004f3509  0c10                  or        al,10h
  004f350b  eb02                  jmp       short 004F350Fh

LAB_004f350d:
  004f350d  24ef                  and       al,0EFh

LAB_004f350f:
  004f350f  8906                  mov       [esi],eax
  004f3511  8a06                  mov       al,[esi]
  004f3513  a802                  test      al,2
  004f3515  7451                  je        short 004F3568h
  004f3517  8d4c2458              lea       ecx,[esp+58h]
  004f351b  53                    push      ebx
  004f351c  51                    push      ecx
  004f351d  55                    push      ebp
  004f351e  56                    push      esi
  004f351f  e80c670200            call      00519C30h
  004f3524  8bf8                  mov       edi,eax
  004f3526  a1b8e05d00            mov       eax,[5DE0B8h]
  004f352b  83c410                add       esp,10h
  004f352e  3bc3                  cmp       eax,ebx
  004f3530  7432                  je        short 004F3564h
  004f3532  8b0f                  mov       ecx,[edi]
  004f3534  57                    push      edi
  004f3535  83c940                or        ecx,40h
  004f3538  56                    push      esi
  004f3539  66c787960000001000    mov       word [edi+96h],10h
  004f3542  890f                  mov       [edi],ecx
  004f3544  e837660400            call      00539B80h
  004f3549  83c408                add       esp,8
  004f354c  83c71c                add       edi,1Ch
  004f354f  57                    push      edi
  004f3550  6800180000            push      1800h
  004f3555  6800030000            push      300h
  004f355a  6a03                  push      3
  004f355c  e8bfa50400            call      0053DB20h
  004f3561  83c410                add       esp,10h

LAB_004f3564:
  004f3564  8b4c2420              mov       ecx,[esp+20h]

LAB_004f3568:
  004f3568  8b7c2414              mov       edi,[esp+14h]
  004f356c  33c0                  xor       eax,eax
  004f356e  8a4642                mov       al,[esi+42h]
  004f3571  83f80c                cmp       eax,0Ch
  004f3574  0f8757050000          ja        near 004F3AD1h
  004f357a  ff248564404f00        jmp       dword [eax*4+4F4064h]
  004f3581  66395e12              cmp       [esi+12h],bx
  004f3585  751a                  jne       short 004F35A1h
  004f3587  8b06                  mov       eax,[esi]
  004f3589  66895e18              mov       [esi+18h],bx
  004f358d  0d00000001            or        eax,1000000h
  004f3592  66895e1a              mov       [esi+1Ah],bx
  004f3596  8906                  mov       [esi],eax
  004f3598  66c746120100          mov       word [esi+12h],1
  004f359e  895e5c                mov       [esi+5Ch],ebx

LAB_004f35a1:
  004f35a1  0fbf5610              movsx     edx,word [esi+10h]
  004f35a5  3bfa                  cmp       edi,edx
  004f35a7  7d08                  jge       short 004F35B1h
  004f35a9  885e43                mov       [esi+43h],bl
  004f35ac  e920050000            jmp       004F3AD1h

LAB_004f35b1:
  004f35b1  8a4643                mov       al,[esi+43h]
  004f35b4  3ac3                  cmp       al,bl
  004f35b6  0f84cc000000          je        near 004F3688h
  004f35bc  fec8                  dec       al
  004f35be  895e5c                mov       [esi+5Ch],ebx
  004f35c1  884643                mov       [esi+43h],al
  004f35c4  e908050000            jmp       004F3AD1h
  004f35c9  66395e12              cmp       [esi+12h],bx
  004f35cd  7522                  jne       short 004F35F1h
  004f35cf  8b0e                  mov       ecx,[esi]
  004f35d1  b801000000            mov       eax,1
  004f35d6  81c900000001          or        ecx,1000000h
  004f35dc  66894618              mov       [esi+18h],ax
  004f35e0  66895e1a              mov       [esi+1Ah],bx
  004f35e4  890e                  mov       [esi],ecx
  004f35e6  66894612              mov       [esi+12h],ax
  004f35ea  c7465c00300000        mov       dword [esi+5Ch],3000h

LAB_004f35f1:
  004f35f1  8b442418              mov       eax,[esp+18h]
  004f35f5  8d88c8000000          lea       ecx,[eax+0C8h]
  004f35fb  81e1ff0f0000          and       ecx,0FFFh
  004f3601  81f990010000          cmp       ecx,190h
  004f3607  7f08                  jg        short 004F3611h
  004f3609  885e43                mov       [esi+43h],bl
  004f360c  e9c0040000            jmp       004F3AD1h

LAB_004f3611:
  004f3611  25ff0f0000            and       eax,0FFFh
  004f3616  3d00080000            cmp       eax,800h
  004f361b  7d10                  jge       short 004F362Dh
  004f361d  8b542410              mov       edx,[esp+10h]
  004f3621  8d4232                lea       eax,[edx+32h]
  004f3624  66014632              add       [esi+32h],ax
  004f3628  e9a4040000            jmp       004F3AD1h

LAB_004f362d:
  004f362d  8b4c2410              mov       ecx,[esp+10h]
  004f3631  b8ceffffff            mov       eax,0FFFFFFCEh
  004f3636  2bc1                  sub       eax,ecx
  004f3638  66014632              add       [esi+32h],ax
  004f363c  e990040000            jmp       004F3AD1h
  004f3641  66395e12              cmp       [esi+12h],bx
  004f3645  8b4c2410              mov       ecx,[esp+10h]
  004f3649  7527                  jne       short 004F3672h
  004f364b  8b06                  mov       eax,[esi]
  004f364d  66c746180300          mov       word [esi+18h],3
  004f3653  0d00000001            or        eax,1000000h
  004f3658  66895e1a              mov       [esi+1Ah],bx
  004f365c  8906                  mov       [esi],eax
  004f365e  8d04c9                lea       eax,[ecx+ecx*8]
  004f3661  c1e004                shl       eax,4
  004f3664  0500c00000            add       eax,0C000h
  004f3669  66c746120100          mov       word [esi+12h],1
  004f366f  89465c                mov       [esi+5Ch],eax

LAB_004f3672:
  004f3672  66837e1201            cmp       word [esi+12h],1
  004f3677  0f8554040000          jne       near 004F3AD1h
  004f367d  0fbf5610              movsx     edx,word [esi+10h]
  004f3681  3bfa                  cmp       edi,edx
  004f3683  7d0b                  jge       short 004F3690h
  004f3685  885e43                mov       [esi+43h],bl

LAB_004f3688:
  004f3688  895e5c                mov       [esi+5Ch],ebx
  004f368b  e941040000            jmp       004F3AD1h

LAB_004f3690:
  004f3690  8a4643                mov       al,[esi+43h]
  004f3693  3ac3                  cmp       al,bl
  004f3695  7405                  je        short 004F369Ch
  004f3697  fec8                  dec       al
  004f3699  884643                mov       [esi+43h],al

LAB_004f369c:
  004f369c  8b442418              mov       eax,[esp+18h]
  004f36a0  8d9000010000          lea       edx,[eax+100h]
  004f36a6  81e2ff0f0000          and       edx,0FFFh
  004f36ac  81fa00020000          cmp       edx,200h
  004f36b2  0f8e19040000          jle       near 004F3AD1h
  004f36b8  25ff0f0000            and       eax,0FFFh
  004f36bd  3d00080000            cmp       eax,800h
  004f36c2  7d0d                  jge       short 004F36D1h
  004f36c4  8d440932              lea       eax,[ecx+ecx+32h]
  004f36c8  66014632              add       [esi+32h],ax
  004f36cc  e900040000            jmp       004F3AD1h

LAB_004f36d1:
  004f36d1  03c9                  add       ecx,ecx
  004f36d3  b8ceffffff            mov       eax,0FFFFFFCEh
  004f36d8  2bc1                  sub       eax,ecx
  004f36da  66014632              add       [esi+32h],ax
  004f36de  e9ee030000            jmp       004F3AD1h
  004f36e3  66837e1201            cmp       word [esi+12h],1
  004f36e8  0f859a000000          jne       near 004F3788h
  004f36ee  8a4643                mov       al,[esi+43h]
  004f36f1  3ac3                  cmp       al,bl
  004f36f3  7405                  je        short 004F36FAh
  004f36f5  fec8                  dec       al
  004f36f7  884643                mov       [esi+43h],al

LAB_004f36fa:
  004f36fa  8b442410              mov       eax,[esp+10h]
  004f36fe  8d14c0                lea       edx,[eax+eax*8]
  004f3701  c1e204                shl       edx,4
  004f3704  8d8200600000          lea       eax,[edx+6000h]
  004f370a  0fafc8                imul      ecx,eax
  004f370d  89465c                mov       [esi+5Ch],eax
  004f3710  0fbf4610              movsx     eax,word [esi+10h]
  004f3714  c1f90c                sar       ecx,0Ch
  004f3717  50                    push      eax
  004f3718  8bf9                  mov       edi,ecx
  004f371a  e8fd220500            call      00545A1Ch
  004f371f  0fafc7                imul      eax,edi
  004f3722  8b4d00                mov       ecx,[ebp]
  004f3725  c1f80c                sar       eax,0Ch
  004f3728  03c8                  add       ecx,eax
  004f372a  894d00                mov       [ebp],ecx
  004f372d  0fbf4e10              movsx     ecx,word [esi+10h]
  004f3731  51                    push      ecx
  004f3732  e8df220500            call      00545A16h
  004f3737  0fafc7                imul      eax,edi
  004f373a  8b6e24                mov       ebp,[esi+24h]
  004f373d  895e5c                mov       [esi+5Ch],ebx
  004f3740  c1f80c                sar       eax,0Ch
  004f3743  03e8                  add       ebp,eax
  004f3745  8b442418              mov       eax,[esp+18h]
  004f3749  896e24                mov       [esi+24h],ebp
  004f374c  8d9000010000          lea       edx,[eax+100h]
  004f3752  81e2ff0f0000          and       edx,0FFFh
  004f3758  81fa00020000          cmp       edx,200h
  004f375e  7e28                  jle       short 004F3788h
  004f3760  25ff0f0000            and       eax,0FFFh
  004f3765  3d00080000            cmp       eax,800h
  004f376a  7d0a                  jge       short 004F3776h
  004f376c  8b442410              mov       eax,[esp+10h]
  004f3770  8d440032              lea       eax,[eax+eax+32h]
  004f3774  eb0e                  jmp       short 004F3784h

LAB_004f3776:
  004f3776  8b4c2410              mov       ecx,[esp+10h]
  004f377a  b8ceffffff            mov       eax,0FFFFFFCEh
  004f377f  8d1409                lea       edx,[ecx+ecx]
  004f3782  2bc2                  sub       eax,edx

LAB_004f3784:
  004f3784  66014632              add       [esi+32h],ax

LAB_004f3788:
  004f3788  66395e12              cmp       [esi+12h],bx
  004f378c  0f853f030000          jne       near 004F3AD1h
  004f3792  8b0e                  mov       ecx,[esi]
  004f3794  b801000000            mov       eax,1
  004f3799  81c900000001          or        ecx,1000000h
  004f379f  66894618              mov       [esi+18h],ax
  004f37a3  66895e1a              mov       [esi+1Ah],bx
  004f37a7  890e                  mov       [esi],ecx
  004f37a9  66894612              mov       [esi+12h],ax
  004f37ad  e91f030000            jmp       004F3AD1h
  004f37b2  66395e12              cmp       [esi+12h],bx
  004f37b6  7520                  jne       short 004F37D8h
  004f37b8  8b06                  mov       eax,[esi]
  004f37ba  a801                  test      al,1
  004f37bc  7405                  je        short 004F37C3h
  004f37be  885e43                mov       [esi+43h],bl
  004f37c1  eb15                  jmp       short 004F37D8h

LAB_004f37c3:
  004f37c3  25fffffffe            and       eax,0FEFFFFFFh
  004f37c8  66895e18              mov       [esi+18h],bx
  004f37cc  8906                  mov       [esi],eax
  004f37ce  66895e1a              mov       [esi+1Ah],bx
  004f37d2  66c746120100          mov       word [esi+12h],1

LAB_004f37d8:
  004f37d8  66837e1201            cmp       word [esi+12h],1
  004f37dd  7579                  jne       short 004F3858h
  004f37df  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004f37e4  7c72                  jl        short 004F3858h
  004f37e6  8b442410              mov       eax,[esp+10h]
  004f37ea  8b3e                  mov       edi,[esi]
  004f37ec  8b5654                mov       edx,[esi+54h]
  004f37ef  81e7fffffffe          and       edi,0FEFFFFFFh
  004f37f5  8d04c500400100        lea       eax,[eax*8+14000h]
  004f37fc  893e                  mov       [esi],edi
  004f37fe  0fafc8                imul      ecx,eax
  004f3801  c1f90c                sar       ecx,0Ch
  004f3804  8bf9                  mov       edi,ecx
  004f3806  81c200400100          add       edx,14000h
  004f380c  0fbf4e10              movsx     ecx,word [esi+10h]
  004f3810  51                    push      ecx
  004f3811  66c746180300          mov       word [esi+18h],3
  004f3817  66895e1a              mov       [esi+1Ah],bx
  004f381b  66c746120200          mov       word [esi+12h],2
  004f3821  895654                mov       [esi+54h],edx
  004f3824  89465c                mov       [esi+5Ch],eax
  004f3827  e8f0210500            call      00545A1Ch
  004f382c  0fafc7                imul      eax,edi
  004f382f  0fbf5610              movsx     edx,word [esi+10h]
  004f3833  8b4e50                mov       ecx,[esi+50h]
  004f3836  52                    push      edx
  004f3837  c1f80c                sar       eax,0Ch
  004f383a  03c8                  add       ecx,eax
  004f383c  894e50                mov       [esi+50h],ecx
  004f383f  e8d2210500            call      00545A16h
  004f3844  0fafc7                imul      eax,edi
  004f3847  8b4e58                mov       ecx,[esi+58h]
  004f384a  c1f80c                sar       eax,0Ch
  004f384d  03c8                  add       ecx,eax
  004f384f  8b06                  mov       eax,[esi]
  004f3851  0c03                  or        al,3
  004f3853  894e58                mov       [esi+58h],ecx
  004f3856  8906                  mov       [esi],eax

LAB_004f3858:
  004f3858  66837e1202            cmp       word [esi+12h],2
  004f385d  0f856e020000          jne       near 004F3AD1h
  004f3863  8b06                  mov       eax,[esi]
  004f3865  a801                  test      al,1
  004f3867  0f8564020000          jne       near 004F3AD1h
  004f386d  24fd                  and       al,0FDh
  004f386f  885e43                mov       [esi+43h],bl
  004f3872  8906                  mov       [esi],eax
  004f3874  e958020000            jmp       004F3AD1h
  004f3879  668b4e12              mov       cx,[esi+12h]
  004f387d  ba01000000            mov       edx,1
  004f3882  663bca                cmp       cx,dx
  004f3885  7519                  jne       short 004F38A0h
  004f3887  0fbf4610              movsx     eax,word [esi+10h]
  004f388b  3bf8                  cmp       edi,eax
  004f388d  7d05                  jge       short 004F3894h
  004f388f  885e43                mov       [esi+43h],bl
  004f3892  eb0c                  jmp       short 004F38A0h

LAB_004f3894:
  004f3894  8a4643                mov       al,[esi+43h]
  004f3897  3ac3                  cmp       al,bl
  004f3899  7405                  je        short 004F38A0h
  004f389b  fec8                  dec       al
  004f389d  884643                mov       [esi+43h],al

LAB_004f38a0:
  004f38a0  663bcb                cmp       cx,bx
  004f38a3  0f8528020000          jne       near 004F3AD1h
  004f38a9  8b442410              mov       eax,[esp+10h]
  004f38ad  8b3e                  mov       edi,[esi]
  004f38af  81cf00000001          or        edi,1000000h
  004f38b5  66895618              mov       [esi+18h],dx
  004f38b9  8d0cc0                lea       ecx,[eax+eax*8]
  004f38bc  66895e1a              mov       [esi+1Ah],bx
  004f38c0  c1e104                shl       ecx,4
  004f38c3  81c100700000          add       ecx,7000h
  004f38c9  893e                  mov       [esi],edi
  004f38cb  66895612              mov       [esi+12h],dx
  004f38cf  894e5c                mov       [esi+5Ch],ecx
  004f38d2  e9fa010000            jmp       004F3AD1h
  004f38d7  66395e12              cmp       [esi+12h],bx
  004f38db  8b542410              mov       edx,[esp+10h]
  004f38df  bf00e00100            mov       edi,1E000h
  004f38e4  753b                  jne       short 004F3921h
  004f38e6  8b2e                  mov       ebp,[esi]
  004f38e8  66c746120100          mov       word [esi+12h],1
  004f38ee  81e5fffffffe          and       ebp,0FEFFFFFFh
  004f38f4  83fa32                cmp       edx,32h
  004f38f7  66895e18              mov       [esi+18h],bx
  004f38fb  66895e1a              mov       [esi+1Ah],bx
  004f38ff  892e                  mov       [esi],ebp
  004f3901  8bc5                  mov       eax,ebp
  004f3903  7e1c                  jle       short 004F3921h
  004f3905  25fffffffd            and       eax,0FDFFFFFFh
  004f390a  66c746180500          mov       word [esi+18h],5
  004f3910  0c02                  or        al,2
  004f3912  66895e1a              mov       [esi+1Ah],bx
  004f3916  66c746120200          mov       word [esi+12h],2
  004f391c  897e5c                mov       [esi+5Ch],edi
  004f391f  8906                  mov       [esi],eax

LAB_004f3921:
  004f3921  66837e1201            cmp       word [esi+12h],1
  004f3926  8b4c2418              mov       ecx,[esp+18h]
  004f392a  7559                  jne       short 004F3985h
  004f392c  8d8100010000          lea       eax,[ecx+100h]
  004f3932  25ff0f0000            and       eax,0FFFh
  004f3937  3d00020000            cmp       eax,200h
  004f393c  7e22                  jle       short 004F3960h
  004f393e  8bc1                  mov       eax,ecx
  004f3940  25ff0f0000            and       eax,0FFFh
  004f3945  3d00080000            cmp       eax,800h
  004f394a  7d06                  jge       short 004F3952h
  004f394c  8d441232              lea       eax,[edx+edx+32h]
  004f3950  eb0a                  jmp       short 004F395Ch

LAB_004f3952:
  004f3952  8d2c12                lea       ebp,[edx+edx]
  004f3955  b8ceffffff            mov       eax,0FFFFFFCEh
  004f395a  2bc5                  sub       eax,ebp

LAB_004f395c:
  004f395c  66014632              add       [esi+32h],ax

LAB_004f3960:
  004f3960  66837e1a06            cmp       word [esi+1Ah],6
  004f3965  7e1e                  jle       short 004F3985h
  004f3967  8b06                  mov       eax,[esi]
  004f3969  66c746180300          mov       word [esi+18h],3
  004f396f  25fffffffd            and       eax,0FDFFFFFFh
  004f3974  66895e1a              mov       [esi+1Ah],bx
  004f3978  0c02                  or        al,2
  004f397a  66c746120200          mov       word [esi+12h],2
  004f3980  897e5c                mov       [esi+5Ch],edi
  004f3983  8906                  mov       [esi],eax

LAB_004f3985:
  004f3985  66837e1202            cmp       word [esi+12h],2
  004f398a  0f8541010000          jne       near 004F3AD1h
  004f3990  8d8100010000          lea       eax,[ecx+100h]
  004f3996  25ff0f0000            and       eax,0FFFh
  004f399b  3d00020000            cmp       eax,200h
  004f39a0  7e22                  jle       short 004F39C4h
  004f39a2  81e1ff0f0000          and       ecx,0FFFh
  004f39a8  81f900080000          cmp       ecx,800h
  004f39ae  7d06                  jge       short 004F39B6h
  004f39b0  8d441232              lea       eax,[edx+edx+32h]
  004f39b4  eb0a                  jmp       short 004F39C0h

LAB_004f39b6:
  004f39b6  8d0c12                lea       ecx,[edx+edx]
  004f39b9  b8ceffffff            mov       eax,0FFFFFFCEh
  004f39be  2bc1                  sub       eax,ecx

LAB_004f39c0:
  004f39c0  66014632              add       [esi+32h],ax

LAB_004f39c4:
  004f39c4  8b06                  mov       eax,[esi]
  004f39c6  a900000002            test      eax,2000000h
  004f39cb  0f8400010000          je        near 004F3AD1h
  004f39d1  24fd                  and       al,0FDh
  004f39d3  885e43                mov       [esi+43h],bl
  004f39d6  8906                  mov       [esi],eax
  004f39d8  e9f4000000            jmp       004F3AD1h
  004f39dd  66395e12              cmp       [esi+12h],bx
  004f39e1  751c                  jne       short 004F39FFh
  004f39e3  8b06                  mov       eax,[esi]
  004f39e5  66c746180400          mov       word [esi+18h],4
  004f39eb  25fffffffc            and       eax,0FCFFFFFFh
  004f39f0  66895e1a              mov       [esi+1Ah],bx
  004f39f4  8906                  mov       [esi],eax
  004f39f6  66c746120100          mov       word [esi+12h],1
  004f39fc  895e5c                mov       [esi+5Ch],ebx

LAB_004f39ff:
  004f39ff  66837e1201            cmp       word [esi+12h],1
  004f3a04  8b542418              mov       edx,[esp+18h]
  004f3a08  757a                  jne       short 004F3A84h
  004f3a0a  8d8200010000          lea       eax,[edx+100h]
  004f3a10  25ff0f0000            and       eax,0FFFh
  004f3a15  3d00020000            cmp       eax,200h
  004f3a1a  8b442410              mov       eax,[esp+10h]
  004f3a1e  7e24                  jle       short 004F3A44h
  004f3a20  8bca                  mov       ecx,edx
  004f3a22  81e1ff0f0000          and       ecx,0FFFh
  004f3a28  81f900080000          cmp       ecx,800h
  004f3a2e  7d06                  jge       short 004F3A36h
  004f3a30  8d4c0032              lea       ecx,[eax+eax+32h]
  004f3a34  eb0a                  jmp       short 004F3A40h

LAB_004f3a36:
  004f3a36  8d2c00                lea       ebp,[eax+eax]
  004f3a39  b9ceffffff            mov       ecx,0FFFFFFCEh
  004f3a3e  2bcd                  sub       ecx,ebp

LAB_004f3a40:
  004f3a40  66014e32              add       [esi+32h],cx

LAB_004f3a44:
  004f3a44  f70600000002          test      dword [esi],2000000h
  004f3a4a  743c                  je        short 004F3A88h
  004f3a4c  81ffc4090000          cmp       edi,9C4h
  004f3a52  66c746120200          mov       word [esi+12h],2
  004f3a58  7d2e                  jge       short 004F3A88h
  004f3a5a  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004f3a61  6aff                  push      0FFFFFFFFh
  004f3a63  53                    push      ebx
  004f3a64  68dc000000            push      0DCh
  004f3a69  56                    push      esi
  004f3a6a  66899e9a000000        mov       [esi+9Ah],bx
  004f3a71  e85a6ff8ff            call      0047A9D0h
  004f3a76  8b542428              mov       edx,[esp+28h]
  004f3a7a  83c410                add       esp,10h
  004f3a7d  6689be9a000000        mov       [esi+9Ah],di

LAB_004f3a84:
  004f3a84  8b442410              mov       eax,[esp+10h]

LAB_004f3a88:
  004f3a88  66837e1202            cmp       word [esi+12h],2
  004f3a8d  7542                  jne       short 004F3AD1h
  004f3a8f  8d8a00010000          lea       ecx,[edx+100h]
  004f3a95  81e1ff0f0000          and       ecx,0FFFh
  004f3a9b  81f900020000          cmp       ecx,200h
  004f3aa1  7e22                  jle       short 004F3AC5h
  004f3aa3  81e2ff0f0000          and       edx,0FFFh
  004f3aa9  81fa00080000          cmp       edx,800h
  004f3aaf  7d06                  jge       short 004F3AB7h
  004f3ab1  8d440032              lea       eax,[eax+eax+32h]
  004f3ab5  eb0a                  jmp       short 004F3AC1h

LAB_004f3ab7:
  004f3ab7  8d1400                lea       edx,[eax+eax]
  004f3aba  b8ceffffff            mov       eax,0FFFFFFCEh
  004f3abf  2bc2                  sub       eax,edx

LAB_004f3ac1:
  004f3ac1  66014632              add       [esi+32h],ax

LAB_004f3ac5:
  004f3ac5  8a4643                mov       al,[esi+43h]
  004f3ac8  3ac3                  cmp       al,bl
  004f3aca  7405                  je        short 004F3AD1h
  004f3acc  fec8                  dec       al
  004f3ace  884643                mov       [esi+43h],al

LAB_004f3ad1:
  004f3ad1  0fbf4632              movsx     eax,word [esi+32h]
  004f3ad5  50                    push      eax
  004f3ad6  e8411f0500            call      00545A1Ch
  004f3adb  0fbf4e32              movsx     ecx,word [esi+32h]
  004f3adf  51                    push      ecx
  004f3ae0  89442468              mov       [esp+68h],eax
  004f3ae4  e82d1f0500            call      00545A16h
  004f3ae9  8b5650                mov       edx,[esi+50h]
  004f3aec  8b4c2420              mov       ecx,[esp+20h]
  004f3af0  8944246c              mov       [esp+6Ch],eax
  004f3af4  8b5e54                mov       ebx,[esi+54h]
  004f3af7  0faf4e5c              imul      ecx,[esi+5Ch]
  004f3afb  8bc2                  mov       eax,edx
  004f3afd  8bfa                  mov       edi,edx
  004f3aff  c1f803                sar       eax,3
  004f3b02  2bf8                  sub       edi,eax
  004f3b04  8beb                  mov       ebp,ebx
  004f3b06  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004f3b0d  c1fa08                sar       edx,8
  004f3b10  2bfa                  sub       edi,edx
  004f3b12  8bd0                  mov       edx,eax
  004f3b14  c1fa08                sar       edx,8
  004f3b17  c1fd08                sar       ebp,8
  004f3b1a  2bd5                  sub       edx,ebp
  004f3b1c  8b6e58                mov       ebp,[esi+58h]
  004f3b1f  03d3                  add       edx,ebx
  004f3b21  8bdd                  mov       ebx,ebp
  004f3b23  2bd0                  sub       edx,eax
  004f3b25  8bc5                  mov       eax,ebp
  004f3b27  c1fb08                sar       ebx,8
  004f3b2a  c1f90c                sar       ecx,0Ch
  004f3b2d  2bc3                  sub       eax,ebx
  004f3b2f  8bd9                  mov       ebx,ecx
  004f3b31  0faf5c2464            imul      ebx,[esp+64h]
  004f3b36  0faf4c246c            imul      ecx,[esp+6Ch]
  004f3b3b  c1fd03                sar       ebp,3
  004f3b3e  c1fb0c                sar       ebx,0Ch
  004f3b41  2bc5                  sub       eax,ebp
  004f3b43  8b6e1c                mov       ebp,[esi+1Ch]
  004f3b46  03df                  add       ebx,edi
  004f3b48  56                    push      esi
  004f3b49  03eb                  add       ebp,ebx
  004f3b4b  8b5e20                mov       ebx,[esi+20h]
  004f3b4e  c1f90c                sar       ecx,0Ch
  004f3b51  896e1c                mov       [esi+1Ch],ebp
  004f3b54  8b6e24                mov       ebp,[esi+24h]
  004f3b57  03c8                  add       ecx,eax
  004f3b59  03da                  add       ebx,edx
  004f3b5b  03e9                  add       ebp,ecx
  004f3b5d  897e50                mov       [esi+50h],edi
  004f3b60  895654                mov       [esi+54h],edx
  004f3b63  894658                mov       [esi+58h],eax
  004f3b66  895e20                mov       [esi+20h],ebx
  004f3b69  896e24                mov       [esi+24h],ebp
  004f3b6c  e8cffd0300            call      00533940h
  004f3b71  8b9690000000          mov       edx,[esi+90h]
  004f3b77  8b4660                mov       eax,[esi+60h]
  004f3b7a  8b4e20                mov       ecx,[esi+20h]
  004f3b7d  83c404                add       esp,4
  004f3b80  80e6fc                and       dh,0FCh
  004f3b83  3bc8                  cmp       ecx,eax
  004f3b85  899690000000          mov       [esi+90h],edx
  004f3b8b  7f2a                  jg        short 004F3BB7h
  004f3b8d  f6c680                test      dh,80h
  004f3b90  7525                  jne       short 004F3BB7h
  004f3b92  8b0e                  mov       ecx,[esi]
  004f3b94  894620                mov       [esi+20h],eax
  004f3b97  f6c101                test      cl,1
  004f3b9a  740f                  je        short 004F3BABh
  004f3b9c  83e1fe                and       ecx,0FFFFFFFEh
  004f3b9f  33db                  xor       ebx,ebx
  004f3ba1  890e                  mov       [esi],ecx
  004f3ba3  895e54                mov       [esi+54h],ebx
  004f3ba6  e91a010000            jmp       004F3CC5h

LAB_004f3bab:
  004f3bab  83e1fe                and       ecx,0FFFFFFFEh
  004f3bae  33db                  xor       ebx,ebx
  004f3bb0  890e                  mov       [esi],ecx
  004f3bb2  895e54                mov       [esi+54h],ebx
  004f3bb5  eb02                  jmp       short 004F3BB9h

LAB_004f3bb7:
  004f3bb7  33db                  xor       ebx,ebx

LAB_004f3bb9:
  004f3bb9  8b0e                  mov       ecx,[esi]
  004f3bbb  f6c101                test      cl,1
  004f3bbe  750a                  jne       short 004F3BCAh
  004f3bc0  394620                cmp       [esi+20h],eax
  004f3bc3  7e05                  jle       short 004F3BCAh
  004f3bc5  83c901                or        ecx,1
  004f3bc8  890e                  mov       [esi],ecx

LAB_004f3bca:
  004f3bca  f7c282ad0020          test      edx,2000AD82h
  004f3bd0  0f84ef000000          je        near 004F3CC5h
  004f3bd6  8b06                  mov       eax,[esi]
  004f3bd8  a801                  test      al,1
  004f3bda  0f85b9000000          jne       near 004F3C99h
  004f3be0  c6464207              mov       byte [esi+42h],7
  004f3be4  e857350500            call      00547140h
  004f3be9  99                    cdq
  004f3bea  b928000000            mov       ecx,28h
  004f3bef  8b3e                  mov       edi,[esi]
  004f3bf1  f7f9                  idiv      ecx
  004f3bf3  83e7fd                and       edi,0FFFFFFFDh
  004f3bf6  8b4c2454              mov       ecx,[esp+54h]
  004f3bfa  8bc7                  mov       eax,edi
  004f3bfc  893e                  mov       [esi],edi
  004f3bfe  24fd                  and       al,0FDh
  004f3c00  894e24                mov       [esi+24h],ecx
  004f3c03  8906                  mov       [esi],eax
  004f3c05  8b442450              mov       eax,[esp+50h]
  004f3c09  894620                mov       [esi+20h],eax
  004f3c0c  8b44242c              mov       eax,[esp+2Ch]
  004f3c10  8b4c2434              mov       ecx,[esp+34h]
  004f3c14  25ffff0000            and       eax,0FFFFh
  004f3c19  898690000000          mov       [esi+90h],eax
  004f3c1f  8b442430              mov       eax,[esp+30h]
  004f3c23  898680000000          mov       [esi+80h],eax
  004f3c29  8b44243c              mov       eax,[esp+3Ch]
  004f3c2d  6882ad0020            push      2000AD82h
  004f3c32  56                    push      esi
  004f3c33  66895e12              mov       [esi+12h],bx
  004f3c37  66c746105000          mov       word [esi+10h],50h
  004f3c3d  898e84000000          mov       [esi+84h],ecx
  004f3c43  89868c000000          mov       [esi+8Ch],eax
  004f3c49  80c214                add       dl,14h
  004f3c4c  885643                mov       [esi+43h],dl
  004f3c4f  8b542454              mov       edx,[esp+54h]
  004f3c53  89561c                mov       [esi+1Ch],edx
  004f3c56  8b542430              mov       edx,[esp+30h]
  004f3c5a  89567c                mov       [esi+7Ch],edx
  004f3c5d  8b542440              mov       edx,[esp+40h]
  004f3c61  899688000000          mov       [esi+88h],edx
  004f3c67  e8f421feff            call      004D5E60h
  004f3c6c  83c408                add       esp,8
  004f3c6f  663bc3                cmp       ax,bx
  004f3c72  7515                  jne       short 004F3C89h
  004f3c74  e8c7340500            call      00547140h
  004f3c79  99                    cdq
  004f3c7a  33c2                  xor       eax,edx
  004f3c7c  2bc2                  sub       eax,edx
  004f3c7e  83e001                and       eax,1
  004f3c81  33c2                  xor       eax,edx
  004f3c83  2bc2                  sub       eax,edx
  004f3c85  8d4400ff              lea       eax,[eax+eax-1]

LAB_004f3c89:
  004f3c89  8b4c2410              mov       ecx,[esp+10h]
  004f3c8d  83c140                add       ecx,40h
  004f3c90  0fafc8                imul      ecx,eax
  004f3c93  66014e32              add       [esi+32h],cx
  004f3c97  eb2c                  jmp       short 004F3CC5h

LAB_004f3c99:
  004f3c99  8b54244c              mov       edx,[esp+4Ch]
  004f3c9d  24fd                  and       al,0FDh
  004f3c9f  8906                  mov       [esi],eax
  004f3ca1  8b442454              mov       eax,[esp+54h]
  004f3ca5  895e5c                mov       [esi+5Ch],ebx
  004f3ca8  895e50                mov       [esi+50h],ebx
  004f3cab  895e58                mov       [esi+58h],ebx
  004f3cae  89561c                mov       [esi+1Ch],edx
  004f3cb1  894624                mov       [esi+24h],eax
  004f3cb4  885e42                mov       [esi+42h],bl
  004f3cb7  c6464328              mov       byte [esi+43h],28h
  004f3cbb  66895e12              mov       [esi+12h],bx
  004f3cbf  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004f3cc5:
  004f3cc5  6aff                  push      0FFFFFFFFh
  004f3cc7  6aff                  push      0FFFFFFFFh
  004f3cc9  6aff                  push      0FFFFFFFFh
  004f3ccb  56                    push      esi
  004f3ccc  e83ffbfcff            call      004C3810h
  004f3cd1  8b442430              mov       eax,[esp+30h]
  004f3cd5  83c410                add       esp,10h
  004f3cd8  3bc3                  cmp       eax,ebx
  004f3cda  7523                  jne       short 004F3CFFh
  004f3cdc  8a4642                mov       al,[esi+42h]
  004f3cdf  668b4c247a            mov       cx,[esp+7Ah]
  004f3ce4  3c0c                  cmp       al,0Ch
  004f3ce6  66894e32              mov       [esi+32h],cx
  004f3cea  0f856a030000          jne       near 004F405Ah
  004f3cf0  c6464301              mov       byte [esi+43h],1
  004f3cf4  885e42                mov       [esi+42h],bl
  004f3cf7  5f                    pop       edi
  004f3cf8  5e                    pop       esi
  004f3cf9  5d                    pop       ebp
  004f3cfa  5b                    pop       ebx
  004f3cfb  83c47c                add       esp,7Ch
  004f3cfe  c3                    ret

LAB_004f3cff:
  004f3cff  385e43                cmp       [esi+43h],bl
  004f3d02  0f8552030000          jne       near 004F405Ah
  004f3d08  8b466c                mov       eax,[esi+6Ch]
  004f3d0b  8b561c                mov       edx,[esi+1Ch]
  004f3d0e  8b781c                mov       edi,[eax+1Ch]
  004f3d11  8b6824                mov       ebp,[eax+24h]
  004f3d14  8b4624                mov       eax,[esi+24h]
  004f3d17  2bfa                  sub       edi,edx
  004f3d19  2be8                  sub       ebp,eax
  004f3d1b  c1ff0c                sar       edi,0Ch
  004f3d1e  c1fd0c                sar       ebp,0Ch
  004f3d21  8bd7                  mov       edx,edi
  004f3d23  8bc5                  mov       eax,ebp
  004f3d25  0fafd7                imul      edx,edi
  004f3d28  0fafc5                imul      eax,ebp
  004f3d2b  03d0                  add       edx,eax
  004f3d2d  52                    push      edx
  004f3d2e  e8dd1c0500            call      00545A10h
  004f3d33  55                    push      ebp
  004f3d34  57                    push      edi
  004f3d35  8944241c              mov       [esp+1Ch],eax
  004f3d39  e8e41c0500            call      00545A22h
  004f3d3e  668b4e32              mov       cx,[esi+32h]
  004f3d42  66895e12              mov       [esi+12h],bx
  004f3d46  2bc1                  sub       eax,ecx
  004f3d48  25ff0f0000            and       eax,0FFFh
  004f3d4d  89442418              mov       [esp+18h],eax
  004f3d51  8b442414              mov       eax,[esp+14h]
  004f3d55  3de6000000            cmp       eax,0E6h
  004f3d5a  0f8d1a010000          jge       near 004F3E7Ah
  004f3d60  e8db330500            call      00547140h
  004f3d65  99                    cdq
  004f3d66  b964000000            mov       ecx,64h
  004f3d6b  f7f9                  idiv      ecx
  004f3d6d  8bea                  mov       ebp,edx
  004f3d6f  83fd32                cmp       ebp,32h
  004f3d72  0f8d8b000000          jge       near 004F3E03h
  004f3d78  8b542418              mov       edx,[esp+18h]
  004f3d7c  8b7c241c              mov       edi,[esp+1Ch]
  004f3d80  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004f3d87  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004f3d8e  25ff0f0000            and       eax,0FFFh
  004f3d93  3bc1                  cmp       eax,ecx
  004f3d95  7f62                  jg        short 004F3DF9h
  004f3d97  e8a4330500            call      00547140h
  004f3d9c  99                    cdq
  004f3d9d  8d4c3f01              lea       ecx,[edi+edi+1]
  004f3da1  f7f9                  idiv      ecx
  004f3da3  8b442414              mov       eax,[esp+14h]
  004f3da7  83c264                add       edx,64h
  004f3daa  3bc2                  cmp       eax,edx
  004f3dac  7d4b                  jge       short 004F3DF9h
  004f3dae  83fd1e                cmp       ebp,1Eh
  004f3db1  7d20                  jge       short 004F3DD3h
  004f3db3  8b06                  mov       eax,[esi]
  004f3db5  c646420b              mov       byte [esi+42h],0Bh
  004f3db9  25fffffffe            and       eax,0FEFFFFFFh
  004f3dbe  c6464301              mov       byte [esi+43h],1
  004f3dc2  8906                  mov       [esi],eax
  004f3dc4  895e5c                mov       [esi+5Ch],ebx
  004f3dc7  66c746180300          mov       word [esi+18h],3
  004f3dcd  66895e1a              mov       [esi+1Ah],bx
  004f3dd1  eb6c                  jmp       short 004F3E3Fh

LAB_004f3dd3:
  004f3dd3  885e42                mov       [esi+42h],bl
  004f3dd6  e865330500            call      00547140h
  004f3ddb  99                    cdq
  004f3ddc  b90a000000            mov       ecx,0Ah
  004f3de1  66c746105000          mov       word [esi+10h],50h
  004f3de7  f7f9                  idiv      ecx
  004f3de9  8bc7                  mov       eax,edi
  004f3deb  66895e12              mov       [esi+12h],bx
  004f3def  c1f802                sar       eax,2
  004f3df2  02d0                  add       dl,al
  004f3df4  885643                mov       [esi+43h],dl
  004f3df7  eb46                  jmp       short 004F3E3Fh

LAB_004f3df9:
  004f3df9  c6464201              mov       byte [esi+42h],1
  004f3dfd  c6464301              mov       byte [esi+43h],1
  004f3e01  eb3c                  jmp       short 004F3E3Fh

LAB_004f3e03:
  004f3e03  83fd3c                cmp       ebp,3Ch
  004f3e06  7d0a                  jge       short 004F3E12h
  004f3e08  c6464201              mov       byte [esi+42h],1
  004f3e0c  c6464301              mov       byte [esi+43h],1
  004f3e10  eb29                  jmp       short 004F3E3Bh

LAB_004f3e12:
  004f3e12  83fd46                cmp       ebp,46h
  004f3e15  7d24                  jge       short 004F3E3Bh
  004f3e17  8b4c2410              mov       ecx,[esp+10h]
  004f3e1b  8b442414              mov       eax,[esp+14h]
  004f3e1f  83c164                add       ecx,64h
  004f3e22  3bc1                  cmp       eax,ecx
  004f3e24  7e15                  jle       short 004F3E3Bh
  004f3e26  c6464206              mov       byte [esi+42h],6
  004f3e2a  c6464301              mov       byte [esi+43h],1
  004f3e2e  e80d330500            call      00547140h
  004f3e33  66894610              mov       [esi+10h],ax
  004f3e37  66895e12              mov       [esi+12h],bx

LAB_004f3e3b:
  004f3e3b  8b7c241c              mov       edi,[esp+1Ch]

LAB_004f3e3f:
  004f3e3f  837c241464            cmp       dword [esp+14h],64h
  004f3e44  7d38                  jge       short 004F3E7Eh
  004f3e46  c6464203              mov       byte [esi+42h],3
  004f3e4a  66895e12              mov       [esi+12h],bx
  004f3e4e  e8ed320500            call      00547140h
  004f3e53  99                    cdq
  004f3e54  b905000000            mov       ecx,5
  004f3e59  f7f9                  idiv      ecx
  004f3e5b  80c20a                add       dl,0Ah
  004f3e5e  885643                mov       [esi+43h],dl
  004f3e61  e8da320500            call      00547140h
  004f3e66  99                    cdq
  004f3e67  33c2                  xor       eax,edx
  004f3e69  2bc2                  sub       eax,edx
  004f3e6b  25ff0f0000            and       eax,0FFFh
  004f3e70  33c2                  xor       eax,edx
  004f3e72  2bc2                  sub       eax,edx
  004f3e74  66894610              mov       [esi+10h],ax
  004f3e78  eb04                  jmp       short 004F3E7Eh

LAB_004f3e7a:
  004f3e7a  8b7c241c              mov       edi,[esp+1Ch]

LAB_004f3e7e:
  004f3e7e  8b442414              mov       eax,[esp+14h]
  004f3e82  3d58020000            cmp       eax,258h
  004f3e87  0f8dd0000000          jge       near 004F3F5Dh
  004f3e8d  3de6000000            cmp       eax,0E6h
  004f3e92  0f8cc5000000          jl        near 004F3F5Dh
  004f3e98  e8a3320500            call      00547140h
  004f3e9d  99                    cdq
  004f3e9e  b964000000            mov       ecx,64h
  004f3ea3  f7f9                  idiv      ecx
  004f3ea5  83fa14                cmp       edx,14h
  004f3ea8  7d23                  jge       short 004F3ECDh
  004f3eaa  c646420c              mov       byte [esi+42h],0Ch
  004f3eae  e88d320500            call      00547140h
  004f3eb3  99                    cdq
  004f3eb4  b91e000000            mov       ecx,1Eh
  004f3eb9  f7f9                  idiv      ecx
  004f3ebb  8b442410              mov       eax,[esp+10h]
  004f3ebf  d1f8                  sar       eax,1
  004f3ec1  02d0                  add       dl,al
  004f3ec3  02d1                  add       dl,cl
  004f3ec5  885643                mov       [esi+43h],dl
  004f3ec8  e990000000            jmp       004F3F5Dh

LAB_004f3ecd:
  004f3ecd  83fa28                cmp       edx,28h
  004f3ed0  7d1f                  jge       short 004F3EF1h
  004f3ed2  c6464202              mov       byte [esi+42h],2
  004f3ed6  e865320500            call      00547140h
  004f3edb  99                    cdq
  004f3edc  b90a000000            mov       ecx,0Ah
  004f3ee1  f7f9                  idiv      ecx
  004f3ee3  8bc7                  mov       eax,edi
  004f3ee5  d1f8                  sar       eax,1
  004f3ee7  02d0                  add       dl,al
  004f3ee9  80c214                add       dl,14h
  004f3eec  885643                mov       [esi+43h],dl
  004f3eef  eb66                  jmp       short 004F3F57h

LAB_004f3ef1:
  004f3ef1  83fa3c                cmp       edx,3Ch
  004f3ef4  7d17                  jge       short 004F3F0Dh
  004f3ef6  c6464206              mov       byte [esi+42h],6
  004f3efa  c6464301              mov       byte [esi+43h],1
  004f3efe  e83d320500            call      00547140h
  004f3f03  66894610              mov       [esi+10h],ax
  004f3f07  66895e12              mov       [esi+12h],bx
  004f3f0b  eb50                  jmp       short 004F3F5Dh

LAB_004f3f0d:
  004f3f0d  83fa50                cmp       edx,50h
  004f3f10  7d3a                  jge       short 004F3F4Ch
  004f3f12  c6464203              mov       byte [esi+42h],3
  004f3f16  66895e12              mov       [esi+12h],bx
  004f3f1a  e821320500            call      00547140h
  004f3f1f  99                    cdq
  004f3f20  b90a000000            mov       ecx,0Ah
  004f3f25  f7f9                  idiv      ecx
  004f3f27  8bc7                  mov       eax,edi
  004f3f29  d1f8                  sar       eax,1
  004f3f2b  02d0                  add       dl,al
  004f3f2d  80c214                add       dl,14h
  004f3f30  885643                mov       [esi+43h],dl
  004f3f33  e808320500            call      00547140h
  004f3f38  99                    cdq
  004f3f39  33c2                  xor       eax,edx
  004f3f3b  2bc2                  sub       eax,edx
  004f3f3d  25ff0f0000            and       eax,0FFFh
  004f3f42  33c2                  xor       eax,edx
  004f3f44  2bc2                  sub       eax,edx
  004f3f46  66894610              mov       [esi+10h],ax
  004f3f4a  eb11                  jmp       short 004F3F5Dh

LAB_004f3f4c:
  004f3f4c  885e42                mov       [esi+42h],bl
  004f3f4f  c6464301              mov       byte [esi+43h],1
  004f3f53  66895e12              mov       [esi+12h],bx

LAB_004f3f57:
  004f3f57  66c746105000          mov       word [esi+10h],50h

LAB_004f3f5d:
  004f3f5d  817c241458020000      cmp       dword [esp+14h],258h
  004f3f65  0f8cef000000          jl        near 004F405Ah
  004f3f6b  e8d0310500            call      00547140h
  004f3f70  bd64000000            mov       ebp,64h
  004f3f75  99                    cdq
  004f3f76  8bcd                  mov       ecx,ebp
  004f3f78  f7f9                  idiv      ecx
  004f3f7a  83fa1e                cmp       edx,1Eh
  004f3f7d  7d42                  jge       short 004F3FC1h
  004f3f7f  8b442410              mov       eax,[esp+10h]
  004f3f83  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004f3f87  3bd1                  cmp       edx,ecx
  004f3f89  0f8c91000000          jl        near 004F4020h
  004f3f8f  885e42                mov       [esi+42h],bl
  004f3f92  e8a9310500            call      00547140h
  004f3f97  99                    cdq
  004f3f98  b90a000000            mov       ecx,0Ah
  004f3f9d  66895e12              mov       [esi+12h],bx
  004f3fa1  f7f9                  idiv      ecx
  004f3fa3  8b4c2410              mov       ecx,[esp+10h]
  004f3fa7  8bc7                  mov       eax,edi
  004f3fa9  c1f802                sar       eax,2
  004f3fac  02d0                  add       dl,al
  004f3fae  885643                mov       [esi+43h],dl
  004f3fb1  8d540950              lea       edx,[ecx+ecx+50h]
  004f3fb5  66895610              mov       [esi+10h],dx
  004f3fb9  5f                    pop       edi
  004f3fba  5e                    pop       esi
  004f3fbb  5d                    pop       ebp
  004f3fbc  5b                    pop       ebx
  004f3fbd  83c47c                add       esp,7Ch
  004f3fc0  c3                    ret

LAB_004f3fc1:
  004f3fc1  83fa28                cmp       edx,28h
  004f3fc4  7d40                  jge       short 004F4006h
  004f3fc6  c6464203              mov       byte [esi+42h],3
  004f3fca  66895e12              mov       [esi+12h],bx
  004f3fce  e86d310500            call      00547140h
  004f3fd3  99                    cdq
  004f3fd4  b90a000000            mov       ecx,0Ah
  004f3fd9  f7f9                  idiv      ecx
  004f3fdb  8bc7                  mov       eax,edi
  004f3fdd  d1f8                  sar       eax,1
  004f3fdf  02d0                  add       dl,al
  004f3fe1  80c214                add       dl,14h
  004f3fe4  885643                mov       [esi+43h],dl
  004f3fe7  e854310500            call      00547140h
  004f3fec  99                    cdq
  004f3fed  33c2                  xor       eax,edx
  004f3fef  2bc2                  sub       eax,edx
  004f3ff1  25ff0f0000            and       eax,0FFFh
  004f3ff6  33c2                  xor       eax,edx
  004f3ff8  2bc2                  sub       eax,edx
  004f3ffa  66894610              mov       [esi+10h],ax
  004f3ffe  5f                    pop       edi
  004f3fff  5e                    pop       esi
  004f4000  5d                    pop       ebp
  004f4001  5b                    pop       ebx
  004f4002  83c47c                add       esp,7Ch
  004f4005  c3                    ret

LAB_004f4006:
  004f4006  83fa3c                cmp       edx,3Ch
  004f4009  7d10                  jge       short 004F401Bh
  004f400b  c6464201              mov       byte [esi+42h],1
  004f400f  c6464301              mov       byte [esi+43h],1
  004f4013  5f                    pop       edi
  004f4014  5e                    pop       esi
  004f4015  5d                    pop       ebp
  004f4016  5b                    pop       ebx
  004f4017  83c47c                add       esp,7Ch
  004f401a  c3                    ret

LAB_004f401b:
  004f401b  83fa50                cmp       edx,50h
  004f401e  7d29                  jge       short 004F4049h

LAB_004f4020:
  004f4020  c6464202              mov       byte [esi+42h],2
  004f4024  e817310500            call      00547140h
  004f4029  99                    cdq
  004f402a  b90a000000            mov       ecx,0Ah
  004f402f  66896e10              mov       [esi+10h],bp
  004f4033  f7f9                  idiv      ecx
  004f4035  8bc7                  mov       eax,edi
  004f4037  d1f8                  sar       eax,1
  004f4039  02d0                  add       dl,al
  004f403b  80c214                add       dl,14h
  004f403e  885643                mov       [esi+43h],dl
  004f4041  5f                    pop       edi
  004f4042  5e                    pop       esi
  004f4043  5d                    pop       ebp
  004f4044  5b                    pop       ebx
  004f4045  83c47c                add       esp,7Ch
  004f4048  c3                    ret

LAB_004f4049:
  004f4049  c6464206              mov       byte [esi+42h],6
  004f404d  c6464301              mov       byte [esi+43h],1
  004f4051  e8ea300500            call      00547140h
  004f4056  66894610              mov       [esi+10h],ax

LAB_004f405a:
  004f405a  5f                    pop       edi
  004f405b  5e                    pop       esi
  004f405c  5d                    pop       ebp
  004f405d  5b                    pop       ebx
  004f405e  83c47c                add       esp,7Ch
  004f4061  c3                    ret
  004f4062  8bff                  mov       edi,edi
  004f4064  81354f00c9354f004136  xor       dword [35C9004Fh],3641004Fh
  004f406e  4f                    dec       edi
  004f406f  00e3                  add       bl,ah
  004f4071  364f                  dec       edi
  004f4073  00d1                  add       cl,dl
  004f4075  3a4f00                cmp       cl,[edi]
  004f4078  d13a                  sar       dword [edx],1
  004f407a  4f                    dec       edi
  004f407b  00b2374f0079          add       [edx+79004F37h],dh
  004f4081  384f00                cmp       [edi],cl
  004f4084  d13a                  sar       dword [edx],1
  004f4086  4f                    dec       edi
  004f4087  00d1                  add       cl,dl
  004f4089  3a4f00                cmp       cl,[edi]
  004f408c  d13a                  sar       dword [edx],1
  004f408e  4f                    dec       edi
  004f408f  00d7                  add       bh,dl
  004f4091  384f00                cmp       [edi],cl
  004f4094  dd39                  fnstsw    [ecx]
  004f4096  4f                    dec       edi
  004f4097  009090909090          add       [eax-6F6F6F70h],dl
  004f409d  90                    nop
  004f409e  90                    nop
  004f409f  90                    nop
  004f40a0  81ec8400              (bad)
  (octet invalide - la plage demandee deborde probablement du code)
