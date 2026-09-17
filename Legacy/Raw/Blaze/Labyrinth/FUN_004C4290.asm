Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c4290  section .text  offset fichier 0xc3690  3328 octets

  004c4290  81ec84000000          sub       esp,84h
  004c4296  53                    push      ebx
  004c4297  55                    push      ebp
  004c4298  56                    push      esi
  004c4299  8bb42494000000        mov       esi,[esp+94h]
  004c42a0  b864000000            mov       eax,64h
  004c42a5  33db                  xor       ebx,ebx
  004c42a7  6689442432            mov       [esp+32h],ax
  004c42ac  6689442436            mov       [esp+36h],ax
  004c42b1  8a4641                mov       al,[esi+41h]
  004c42b4  57                    push      edi
  004c42b5  3ac3                  cmp       al,bl
  004c42b7  66895c2430            mov       [esp+30h],bx
  004c42bc  66c74424321e00        mov       word [esp+32h],1Eh
  004c42c3  66c74424343200        mov       word [esp+34h],32h
  004c42ca  66895c2438            mov       [esp+38h],bx
  004c42cf  7510                  jne       short 004C42E1h
  004c42d1  53                    push      ebx
  004c42d2  6a03                  push      3
  004c42d4  56                    push      esi
  004c42d5  c6464101              mov       byte [esi+41h],1
  004c42d9  e872f1ffff            call      004C3450h
  004c42de  83c40c                add       esp,0Ch

LAB_004c42e1:
  004c42e1  8b461c                mov       eax,[esi+1Ch]
  004c42e4  8b4e20                mov       ecx,[esi+20h]
  004c42e7  8b5624                mov       edx,[esi+24h]
  004c42ea  8d6e1c                lea       ebp,[esi+1Ch]
  004c42ed  89442424              mov       [esp+24h],eax
  004c42f1  8b4664                mov       eax,[esi+64h]
  004c42f4  894c2428              mov       [esp+28h],ecx
  004c42f8  8b4e60                mov       ecx,[esi+60h]
  004c42fb  8954242c              mov       [esp+2Ch],edx
  004c42ff  668b5632              mov       dx,[esi+32h]
  004c4303  89442440              mov       [esp+40h],eax
  004c4307  8b467c                mov       eax,[esi+7Ch]
  004c430a  894c2448              mov       [esp+48h],ecx
  004c430e  668b8e90000000        mov       cx,[esi+90h]
  004c4315  6689942482000000      mov       [esp+82h],dx
  004c431d  8b9680000000          mov       edx,[esi+80h]
  004c4323  89442454              mov       [esp+54h],eax
  004c4327  8b8684000000          mov       eax,[esi+84h]
  004c432d  66894c2458            mov       [esp+58h],cx
  004c4332  8b8e88000000          mov       ecx,[esi+88h]
  004c4338  8b7e6c                mov       edi,[esi+6Ch]
  004c433b  89542444              mov       [esp+44h],edx
  004c433f  8b968c000000          mov       edx,[esi+8Ch]
  004c4345  89442450              mov       [esp+50h],eax
  004c4349  894c243c              mov       [esp+3Ch],ecx
  004c434d  8d44245c              lea       eax,[esp+5Ch]
  004c4351  8954244c              mov       [esp+4Ch],edx
  004c4355  8d4c2460              lea       ecx,[esp+60h]
  004c4359  50                    push      eax
  004c435a  8d542468              lea       edx,[esp+68h]
  004c435e  51                    push      ecx
  004c435f  52                    push      edx
  004c4360  56                    push      esi
  004c4361  e81a460700            call      00538980h
  004c4366  83c410                add       esp,10h
  004c4369  8d442468              lea       eax,[esp+68h]
  004c436d  8d4c241c              lea       ecx,[esp+1Ch]
  004c4371  8d542418              lea       edx,[esp+18h]
  004c4375  50                    push      eax
  004c4376  51                    push      ecx
  004c4377  8d44241c              lea       eax,[esp+1Ch]
  004c437b  52                    push      edx
  004c437c  8d4c241c              lea       ecx,[esp+1Ch]
  004c4380  50                    push      eax
  004c4381  51                    push      ecx
  004c4382  56                    push      esi
  004c4383  e8981b0700            call      00535F20h
  004c4388  8b4c2428              mov       ecx,[esp+28h]
  004c438c  8b571c                mov       edx,[edi+1Ch]
  004c438f  b840000000            mov       eax,40h
  004c4394  83c418                add       esp,18h
  004c4397  2bc1                  sub       eax,ecx
  004c4399  8b4e20                mov       ecx,[esi+20h]
  004c439c  89442420              mov       [esp+20h],eax
  004c43a0  8b4500                mov       eax,[ebp]
  004c43a3  2bc2                  sub       eax,edx
  004c43a5  8b5720                mov       edx,[edi+20h]
  004c43a8  2bca                  sub       ecx,edx
  004c43aa  8b5624                mov       edx,[esi+24h]
  004c43ad  2b5724                sub       edx,[edi+24h]
  004c43b0  c1f80c                sar       eax,0Ch
  004c43b3  8bf8                  mov       edi,eax
  004c43b5  c1f90c                sar       ecx,0Ch
  004c43b8  0faff8                imul      edi,eax
  004c43bb  8bc1                  mov       eax,ecx
  004c43bd  c1fa0c                sar       edx,0Ch
  004c43c0  0fafc1                imul      eax,ecx
  004c43c3  8bca                  mov       ecx,edx
  004c43c5  03f8                  add       edi,eax
  004c43c7  0fafca                imul      ecx,edx
  004c43ca  03f9                  add       edi,ecx
  004c43cc  57                    push      edi
  004c43cd  e83e160800            call      00545A10h
  004c43d2  89442414              mov       [esp+14h],eax
  004c43d6  8b44241c              mov       eax,[esp+1Ch]
  004c43da  3bc3                  cmp       eax,ebx
  004c43dc  0f8460050000          je        near 004C4942h
  004c43e2  8b0e                  mov       ecx,[esi]
  004c43e4  33d2                  xor       edx,edx
  004c43e6  8a5604                mov       dl,[esi+4]
  004c43e9  81e1fffffffd          and       ecx,0FDFFFFFFh
  004c43ef  0fbf7e18              movsx     edi,word [esi+18h]
  004c43f3  890e                  mov       [esi],ecx
  004c43f5  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c43fc  f7c100000001          test      ecx,1000000h
  004c4402  8b14ba                mov       edx,[edx+edi*4]
  004c4405  741b                  je        short 004C4422h
  004c4407  66ff461a              inc       word [esi+1Ah]
  004c440b  668b4e1a              mov       cx,[esi+1Ah]
  004c440f  33ff                  xor       edi,edi
  004c4411  668b7a16              mov       di,[edx+16h]
  004c4415  0fbfc9                movsx     ecx,cx
  004c4418  3bcf                  cmp       ecx,edi
  004c441a  7c2a                  jl        short 004C4446h
  004c441c  66895e1a              mov       [esi+1Ah],bx
  004c4420  eb24                  jmp       short 004C4446h

LAB_004c4422:
  004c4422  66ff461a              inc       word [esi+1Ah]
  004c4426  668b7e1a              mov       di,[esi+1Ah]
  004c442a  33db                  xor       ebx,ebx
  004c442c  668b5a16              mov       bx,[edx+16h]
  004c4430  0fbfd7                movsx     edx,di
  004c4433  3bd3                  cmp       edx,ebx
  004c4435  7c0d                  jl        short 004C4444h
  004c4437  4f                    dec       edi
  004c4438  81c900000002          or        ecx,2000000h
  004c443e  66897e1a              mov       [esi+1Ah],di
  004c4442  890e                  mov       [esi],ecx

LAB_004c4444:
  004c4444  33db                  xor       ebx,ebx

LAB_004c4446:
  004c4446  8b0e                  mov       ecx,[esi]
  004c4448  f6c102                test      cl,2
  004c444b  0f84bc0a0000          je        near 004C4F0Dh
  004c4451  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004c4456  7558                  jne       short 004C44B0h
  004c4458  8d442430              lea       eax,[esp+30h]
  004c445c  53                    push      ebx
  004c445d  50                    push      eax
  004c445e  55                    push      ebp
  004c445f  56                    push      esi
  004c4460  e8cb570500            call      00519C30h
  004c4465  8bf8                  mov       edi,eax
  004c4467  a1b8e05d00            mov       eax,[5DE0B8h]
  004c446c  83c410                add       esp,10h
  004c446f  3bc3                  cmp       eax,ebx
  004c4471  7439                  je        short 004C44ACh
  004c4473  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004c4478  7532                  jne       short 004C44ACh
  004c447a  8b0f                  mov       ecx,[edi]
  004c447c  57                    push      edi
  004c447d  83c940                or        ecx,40h
  004c4480  56                    push      esi
  004c4481  66c787960000001000    mov       word [edi+96h],10h
  004c448a  890f                  mov       [edi],ecx
  004c448c  e8ef560700            call      00539B80h
  004c4491  83c408                add       esp,8
  004c4494  83c71c                add       edi,1Ch
  004c4497  57                    push      edi
  004c4498  6800180000            push      1800h
  004c449d  6800030000            push      300h
  004c44a2  6a03                  push      3
  004c44a4  e877960700            call      0053DB20h
  004c44a9  83c410                add       esp,10h

LAB_004c44ac:
  004c44ac  8b44241c              mov       eax,[esp+1Ch]

LAB_004c44b0:
  004c44b0  8b0e                  mov       ecx,[esi]
  004c44b2  f7c100000002          test      ecx,2000000h
  004c44b8  7411                  je        short 004C44CBh
  004c44ba  f6c101                test      cl,1
  004c44bd  0f85400a0000          jne       near 004C4F03h
  004c44c3  83e1fd                and       ecx,0FFFFFFFDh
  004c44c6  885e43                mov       [esi+43h],bl
  004c44c9  890e                  mov       [esi],ecx

LAB_004c44cb:
  004c44cb  8b4c2410              mov       ecx,[esp+10h]

LAB_004c44cf:
  004c44cf  33d2                  xor       edx,edx
  004c44d1  8a5642                mov       dl,[esi+42h]
  004c44d4  83fa0b                cmp       edx,0Bh
  004c44d7  0f8765040000          ja        near 004C4942h
  004c44dd  ff2495604f4c00        jmp       dword [edx*4+4C4F60h]
  004c44e4  66395e12              cmp       [esi+12h],bx
  004c44e8  751a                  jne       short 004C4504h
  004c44ea  8b06                  mov       eax,[esi]
  004c44ec  66895e18              mov       [esi+18h],bx
  004c44f0  0d00000001            or        eax,1000000h
  004c44f5  66895e1a              mov       [esi+1Ah],bx
  004c44f9  8906                  mov       [esi],eax
  004c44fb  66c746120100          mov       word [esi+12h],1
  004c4501  895e5c                mov       [esi+5Ch],ebx

LAB_004c4504:
  004c4504  0fbf4610              movsx     eax,word [esi+10h]
  004c4508  39442414              cmp       [esp+14h],eax
  004c450c  7d08                  jge       short 004C4516h
  004c450e  885e43                mov       [esi+43h],bl
  004c4511  e92c040000            jmp       004C4942h

LAB_004c4516:
  004c4516  8a4643                mov       al,[esi+43h]
  004c4519  3ac3                  cmp       al,bl
  004c451b  0f84ee030000          je        near 004C490Fh
  004c4521  fec8                  dec       al
  004c4523  895e5c                mov       [esi+5Ch],ebx
  004c4526  884643                mov       [esi+43h],al
  004c4529  e914040000            jmp       004C4942h
  004c452e  66395e12              cmp       [esi+12h],bx
  004c4532  751b                  jne       short 004C454Fh
  004c4534  8b16                  mov       edx,[esi]
  004c4536  b801000000            mov       eax,1
  004c453b  81ca00000001          or        edx,1000000h
  004c4541  66894618              mov       [esi+18h],ax
  004c4545  66895e1a              mov       [esi+1Ah],bx
  004c4549  8916                  mov       [esi],edx
  004c454b  66894612              mov       [esi+12h],ax

LAB_004c454f:
  004c454f  8b442418              mov       eax,[esp+18h]
  004c4553  8d90c8000000          lea       edx,[eax+0C8h]
  004c4559  81e2ff0f0000          and       edx,0FFFh
  004c455f  81fa90010000          cmp       edx,190h
  004c4565  7f0b                  jg        short 004C4572h
  004c4567  885e43                mov       [esi+43h],bl
  004c456a  895e5c                mov       [esi+5Ch],ebx
  004c456d  e9d0030000            jmp       004C4942h

LAB_004c4572:
  004c4572  25ff0f0000            and       eax,0FFFh
  004c4577  3d00080000            cmp       eax,800h
  004c457c  7d12                  jge       short 004C4590h
  004c457e  8d8180000000          lea       eax,[ecx+80h]
  004c4584  895e5c                mov       [esi+5Ch],ebx
  004c4587  66014632              add       [esi+32h],ax
  004c458b  e9b2030000            jmp       004C4942h

LAB_004c4590:
  004c4590  b880ffffff            mov       eax,0FFFFFF80h
  004c4595  895e5c                mov       [esi+5Ch],ebx
  004c4598  2bc1                  sub       eax,ecx
  004c459a  66014632              add       [esi+32h],ax
  004c459e  e99f030000            jmp       004C4942h
  004c45a3  66395e12              cmp       [esi+12h],bx
  004c45a7  b801000000            mov       eax,1
  004c45ac  7525                  jne       short 004C45D3h
  004c45ae  8b16                  mov       edx,[esi]
  004c45b0  66894618              mov       [esi+18h],ax
  004c45b4  81ca00000001          or        edx,1000000h
  004c45ba  66895e1a              mov       [esi+1Ah],bx
  004c45be  8916                  mov       [esi],edx
  004c45c0  8d14c9                lea       edx,[ecx+ecx*8]
  004c45c3  c1e204                shl       edx,4
  004c45c6  81c200500000          add       edx,5000h
  004c45cc  66894612              mov       [esi+12h],ax
  004c45d0  89565c                mov       [esi+5Ch],edx

LAB_004c45d3:
  004c45d3  66394612              cmp       [esi+12h],ax
  004c45d7  0f8565030000          jne       near 004C4942h
  004c45dd  0fbf4610              movsx     eax,word [esi+10h]
  004c45e1  39442414              cmp       [esp+14h],eax
  004c45e5  7d0b                  jge       short 004C45F2h
  004c45e7  885e43                mov       [esi+43h],bl
  004c45ea  895e5c                mov       [esi+5Ch],ebx
  004c45ed  e950030000            jmp       004C4942h

LAB_004c45f2:
  004c45f2  8a4643                mov       al,[esi+43h]
  004c45f5  3ac3                  cmp       al,bl
  004c45f7  7405                  je        short 004C45FEh
  004c45f9  fec8                  dec       al
  004c45fb  884643                mov       [esi+43h],al

LAB_004c45fe:
  004c45fe  8b442418              mov       eax,[esp+18h]
  004c4602  8d9000010000          lea       edx,[eax+100h]
  004c4608  81e2ff0f0000          and       edx,0FFFh
  004c460e  81fa00020000          cmp       edx,200h
  004c4614  0f8e28030000          jle       near 004C4942h
  004c461a  25ff0f0000            and       eax,0FFFh
  004c461f  3d00080000            cmp       eax,800h
  004c4624  7d10                  jge       short 004C4636h
  004c4626  8d840980000000        lea       eax,[ecx+ecx+80h]
  004c462d  66014632              add       [esi+32h],ax
  004c4631  e90c030000            jmp       004C4942h

LAB_004c4636:
  004c4636  03c9                  add       ecx,ecx
  004c4638  b880ffffff            mov       eax,0FFFFFF80h
  004c463d  2bc1                  sub       eax,ecx
  004c463f  66014632              add       [esi+32h],ax
  004c4643  e9fa020000            jmp       004C4942h
  004c4648  66837e1201            cmp       word [esi+12h],1
  004c464d  0f8596000000          jne       near 004C46E9h
  004c4653  8a5643                mov       dl,[esi+43h]
  004c4656  3ad3                  cmp       dl,bl
  004c4658  7405                  je        short 004C465Fh
  004c465a  feca                  dec       dl
  004c465c  885643                mov       [esi+43h],dl

LAB_004c465f:
  004c465f  8d14c9                lea       edx,[ecx+ecx*8]
  004c4662  c1e204                shl       edx,4
  004c4665  8d8a00500000          lea       ecx,[edx+5000h]
  004c466b  0fafc1                imul      eax,ecx
  004c466e  c1f80c                sar       eax,0Ch
  004c4671  8bf8                  mov       edi,eax
  004c4673  894e5c                mov       [esi+5Ch],ecx
  004c4676  0fbf4610              movsx     eax,word [esi+10h]
  004c467a  50                    push      eax
  004c467b  e89c130800            call      00545A1Ch
  004c4680  0fafc7                imul      eax,edi
  004c4683  8b4d00                mov       ecx,[ebp]
  004c4686  c1f80c                sar       eax,0Ch
  004c4689  03c8                  add       ecx,eax
  004c468b  894d00                mov       [ebp],ecx
  004c468e  0fbf4e10              movsx     ecx,word [esi+10h]
  004c4692  51                    push      ecx
  004c4693  e87e130800            call      00545A16h
  004c4698  0fafc7                imul      eax,edi
  004c469b  8b6e24                mov       ebp,[esi+24h]
  004c469e  895e5c                mov       [esi+5Ch],ebx
  004c46a1  c1f80c                sar       eax,0Ch
  004c46a4  03e8                  add       ebp,eax
  004c46a6  8b442418              mov       eax,[esp+18h]
  004c46aa  896e24                mov       [esi+24h],ebp
  004c46ad  8d9000010000          lea       edx,[eax+100h]
  004c46b3  81e2ff0f0000          and       edx,0FFFh
  004c46b9  81fa00020000          cmp       edx,200h
  004c46bf  7e28                  jle       short 004C46E9h
  004c46c1  25ff0f0000            and       eax,0FFFh
  004c46c6  3d00080000            cmp       eax,800h
  004c46cb  7d0a                  jge       short 004C46D7h
  004c46cd  8b442410              mov       eax,[esp+10h]
  004c46d1  8d440032              lea       eax,[eax+eax+32h]
  004c46d5  eb0e                  jmp       short 004C46E5h

LAB_004c46d7:
  004c46d7  8b4c2410              mov       ecx,[esp+10h]
  004c46db  b8ceffffff            mov       eax,0FFFFFFCEh
  004c46e0  8d1409                lea       edx,[ecx+ecx]
  004c46e3  2bc2                  sub       eax,edx

LAB_004c46e5:
  004c46e5  66014632              add       [esi+32h],ax

LAB_004c46e9:
  004c46e9  66395e12              cmp       [esi+12h],bx
  004c46ed  0f854f020000          jne       near 004C4942h
  004c46f3  8b0e                  mov       ecx,[esi]
  004c46f5  b801000000            mov       eax,1
  004c46fa  81c900000001          or        ecx,1000000h
  004c4700  66894618              mov       [esi+18h],ax
  004c4704  66895e1a              mov       [esi+1Ah],bx
  004c4708  890e                  mov       [esi],ecx
  004c470a  66894612              mov       [esi+12h],ax
  004c470e  e92f020000            jmp       004C4942h
  004c4713  66395e12              cmp       [esi+12h],bx
  004c4717  757b                  jne       short 004C4794h
  004c4719  8b16                  mov       edx,[esi]
  004c471b  f6c201                test      dl,1
  004c471e  7405                  je        short 004C4725h
  004c4720  885e43                mov       [esi+43h],bl
  004c4723  eb6f                  jmp       short 004C4794h

LAB_004c4725:
  004c4725  81c100010000          add       ecx,100h
  004c472b  81e2fffffffe          and       edx,0FEFFFFFFh
  004c4731  c1e106                shl       ecx,6
  004c4734  0fafc1                imul      eax,ecx
  004c4737  c1f80c                sar       eax,0Ch
  004c473a  8bf8                  mov       edi,eax
  004c473c  bd01000000            mov       ebp,1
  004c4741  0fbf4610              movsx     eax,word [esi+10h]
  004c4745  50                    push      eax
  004c4746  8916                  mov       [esi],edx
  004c4748  66896e18              mov       [esi+18h],bp
  004c474c  66895e1a              mov       [esi+1Ah],bx
  004c4750  894e5c                mov       [esi+5Ch],ecx
  004c4753  e8c4120800            call      00545A1Ch
  004c4758  0fafc7                imul      eax,edi
  004c475b  8b4e50                mov       ecx,[esi+50h]
  004c475e  c1f80c                sar       eax,0Ch
  004c4761  03c8                  add       ecx,eax
  004c4763  894e50                mov       [esi+50h],ecx
  004c4766  0fbf4e10              movsx     ecx,word [esi+10h]
  004c476a  51                    push      ecx
  004c476b  e8a6120800            call      00545A16h
  004c4770  0fafc7                imul      eax,edi
  004c4773  8b5658                mov       edx,[esi+58h]
  004c4776  8b4e54                mov       ecx,[esi+54h]
  004c4779  c1f80c                sar       eax,0Ch
  004c477c  03d0                  add       edx,eax
  004c477e  8b06                  mov       eax,[esi]
  004c4780  81c100e00100          add       ecx,1E000h
  004c4786  0bc5                  or        eax,ebp
  004c4788  895658                mov       [esi+58h],edx
  004c478b  894e54                mov       [esi+54h],ecx
  004c478e  8906                  mov       [esi],eax
  004c4790  66896e12              mov       [esi+12h],bp

LAB_004c4794:
  004c4794  f60601                test      byte [esi],1
  004c4797  0f85a5010000          jne       near 004C4942h
  004c479d  885e43                mov       [esi+43h],bl
  004c47a0  e99d010000            jmp       004C4942h
  004c47a5  668b5612              mov       dx,[esi+12h]
  004c47a9  bf01000000            mov       edi,1
  004c47ae  663bd7                cmp       dx,di
  004c47b1  751b                  jne       short 004C47CEh
  004c47b3  0fbf4610              movsx     eax,word [esi+10h]
  004c47b7  39442414              cmp       [esp+14h],eax
  004c47bb  7d05                  jge       short 004C47C2h
  004c47bd  885e43                mov       [esi+43h],bl
  004c47c0  eb0c                  jmp       short 004C47CEh

LAB_004c47c2:
  004c47c2  8a4643                mov       al,[esi+43h]
  004c47c5  3ac3                  cmp       al,bl
  004c47c7  7405                  je        short 004C47CEh
  004c47c9  fec8                  dec       al
  004c47cb  884643                mov       [esi+43h],al

LAB_004c47ce:
  004c47ce  663bd3                cmp       dx,bx
  004c47d1  0f856b010000          jne       near 004C4942h
  004c47d7  66895e1a              mov       [esi+1Ah],bx
  004c47db  8b1e                  mov       ebx,[esi]
  004c47dd  d1f9                  sar       ecx,1
  004c47df  81cb00000001          or        ebx,1000000h
  004c47e5  81c100400000          add       ecx,4000h
  004c47eb  66897e18              mov       [esi+18h],di
  004c47ef  891e                  mov       [esi],ebx
  004c47f1  66897e12              mov       [esi+12h],di
  004c47f5  894e5c                mov       [esi+5Ch],ecx
  004c47f8  e945010000            jmp       004C4942h
  004c47fd  668b4e12              mov       cx,[esi+12h]
  004c4801  bf01000000            mov       edi,1
  004c4806  663bcf                cmp       cx,di
  004c4809  7539                  jne       short 004C4844h
  004c480b  8b465c                mov       eax,[esi+5Ch]
  004c480e  66c746180200          mov       word [esi+18h],2
  004c4814  8bd0                  mov       edx,eax
  004c4816  66897e1a              mov       [esi+1Ah],di
  004c481a  c1fa03                sar       edx,3
  004c481d  2bc2                  sub       eax,edx
  004c481f  3d00500000            cmp       eax,5000h
  004c4824  89465c                mov       [esi+5Ch],eax
  004c4827  7d0c                  jge       short 004C4835h
  004c4829  885e43                mov       [esi+43h],bl
  004c482c  895e5c                mov       [esi+5Ch],ebx
  004c482f  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c4835:
  004c4835  8b465c                mov       eax,[esi+5Ch]
  004c4838  c1f805                sar       eax,5
  004c483b  0500100000            add       eax,1000h
  004c4840  6689462c              mov       [esi+2Ch],ax

LAB_004c4844:
  004c4844  663bcb                cmp       cx,bx
  004c4847  0f85f5000000          jne       near 004C4942h
  004c484d  8b16                  mov       edx,[esi]
  004c484f  66897e18              mov       [esi+18h],di
  004c4853  81e2fffffffe          and       edx,0FEFFFFFFh
  004c4859  66895e1a              mov       [esi+1Ah],bx
  004c485d  8916                  mov       [esi],edx
  004c485f  e8dc280800            call      00547140h
  004c4864  99                    cdq
  004c4865  b932000000            mov       ecx,32h
  004c486a  66897e12              mov       [esi+12h],di
  004c486e  f7f9                  idiv      ecx
  004c4870  8b442410              mov       eax,[esp+10h]
  004c4874  24fc                  and       al,0FCh
  004c4876  8d0c90                lea       ecx,[eax+edx*4]
  004c4879  c1e10a                shl       ecx,0Ah
  004c487c  894e5c                mov       [esi+5Ch],ecx
  004c487f  e9be000000            jmp       004C4942h
  004c4884  0fbf4610              movsx     eax,word [esi+10h]
  004c4888  668b4e32              mov       cx,[esi+32h]
  004c488c  8bf8                  mov       edi,eax
  004c488e  0fbfd1                movsx     edx,cx
  004c4891  2bfa                  sub       edi,edx
  004c4893  8bd0                  mov       edx,eax
  004c4895  c1ff03                sar       edi,3
  004c4898  03cf                  add       ecx,edi
  004c489a  66895e18              mov       [esi+18h],bx
  004c489e  66894e32              mov       [esi+32h],cx
  004c48a2  0fbfc9                movsx     ecx,cx
  004c48a5  2bd1                  sub       edx,ecx
  004c48a7  7804                  js        short 004C48ADh
  004c48a9  8bca                  mov       ecx,edx
  004c48ab  eb02                  jmp       short 004C48AFh

LAB_004c48ad:
  004c48ad  2bc8                  sub       ecx,eax

LAB_004c48af:
  004c48af  83f932                cmp       ecx,32h
  004c48b2  7d5b                  jge       short 004C490Fh
  004c48b4  885e43                mov       [esi+43h],bl
  004c48b7  895e5c                mov       [esi+5Ch],ebx
  004c48ba  e983000000            jmp       004C4942h
  004c48bf  bf01000000            mov       edi,1
  004c48c4  66397e12              cmp       [esi+12h],di
  004c48c8  7519                  jne       short 004C48E3h
  004c48ca  66837e1a11            cmp       word [esi+1Ah],11h
  004c48cf  7512                  jne       short 004C48E3h
  004c48d1  6aff                  push      0FFFFFFFFh
  004c48d3  53                    push      ebx
  004c48d4  6a36                  push      36h
  004c48d6  56                    push      esi
  004c48d7  e8f460fbff            call      0047A9D0h
  004c48dc  8b4c2420              mov       ecx,[esp+20h]
  004c48e0  83c410                add       esp,10h

LAB_004c48e3:
  004c48e3  8b06                  mov       eax,[esi]
  004c48e5  a900000002            test      eax,2000000h
  004c48ea  7403                  je        short 004C48EFh
  004c48ec  885e43                mov       [esi+43h],bl

LAB_004c48ef:
  004c48ef  66395e12              cmp       [esi+12h],bx
  004c48f3  754d                  jne       short 004C4942h
  004c48f5  25fffffffe            and       eax,0FEFFFFFFh
  004c48fa  83f91e                cmp       ecx,1Eh
  004c48fd  66897e12              mov       [esi+12h],di
  004c4901  66c746180400          mov       word [esi+18h],4
  004c4907  66895e1a              mov       [esi+1Ah],bx
  004c490b  8906                  mov       [esi],eax
  004c490d  7d33                  jge       short 004C4942h

LAB_004c490f:
  004c490f  895e5c                mov       [esi+5Ch],ebx
  004c4912  eb2e                  jmp       short 004C4942h
  004c4914  668b4e12              mov       cx,[esi+12h]
  004c4918  6683f901              cmp       cx,1
  004c491c  7510                  jne       short 004C492Eh
  004c491e  8b06                  mov       eax,[esi]
  004c4920  a900000002            test      eax,2000000h
  004c4925  7407                  je        short 004C492Eh
  004c4927  24fd                  and       al,0FDh
  004c4929  885e43                mov       [esi+43h],bl
  004c492c  8906                  mov       [esi],eax

LAB_004c492e:
  004c492e  663bcb                cmp       cx,bx
  004c4931  750f                  jne       short 004C4942h
  004c4933  8b06                  mov       eax,[esi]
  004c4935  66c746120100          mov       word [esi+12h],1
  004c493b  25fffffffe            and       eax,0FEFFFFFFh
  004c4940  8906                  mov       [esi],eax

LAB_004c4942:
  004c4942  0fbf4632              movsx     eax,word [esi+32h]
  004c4946  50                    push      eax
  004c4947  e8d0100800            call      00545A1Ch
  004c494c  0fbf4e32              movsx     ecx,word [esi+32h]
  004c4950  51                    push      ecx
  004c4951  89442470              mov       [esp+70h],eax
  004c4955  e8bc100800            call      00545A16h
  004c495a  8b5650                mov       edx,[esi+50h]
  004c495d  8b4c241c              mov       ecx,[esp+1Ch]
  004c4961  89442474              mov       [esp+74h],eax
  004c4965  8b5e54                mov       ebx,[esi+54h]
  004c4968  0faf4e5c              imul      ecx,[esi+5Ch]
  004c496c  8bc2                  mov       eax,edx
  004c496e  8bfa                  mov       edi,edx
  004c4970  c1f803                sar       eax,3
  004c4973  2bf8                  sub       edi,eax
  004c4975  8beb                  mov       ebp,ebx
  004c4977  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004c497e  c1fa08                sar       edx,8
  004c4981  2bfa                  sub       edi,edx
  004c4983  8bd0                  mov       edx,eax
  004c4985  c1fd08                sar       ebp,8
  004c4988  c1fa08                sar       edx,8
  004c498b  2bd5                  sub       edx,ebp
  004c498d  8b6e58                mov       ebp,[esi+58h]
  004c4990  03d3                  add       edx,ebx
  004c4992  8bdd                  mov       ebx,ebp
  004c4994  2bd0                  sub       edx,eax
  004c4996  8bc5                  mov       eax,ebp
  004c4998  c1fb08                sar       ebx,8
  004c499b  c1f90c                sar       ecx,0Ch
  004c499e  2bc3                  sub       eax,ebx
  004c49a0  8bd9                  mov       ebx,ecx
  004c49a2  0faf5c246c            imul      ebx,[esp+6Ch]
  004c49a7  0faf4c2474            imul      ecx,[esp+74h]
  004c49ac  c1fd03                sar       ebp,3
  004c49af  c1fb0c                sar       ebx,0Ch
  004c49b2  2bc5                  sub       eax,ebp
  004c49b4  8b6e1c                mov       ebp,[esi+1Ch]
  004c49b7  03df                  add       ebx,edi
  004c49b9  56                    push      esi
  004c49ba  03eb                  add       ebp,ebx
  004c49bc  8b5e20                mov       ebx,[esi+20h]
  004c49bf  c1f90c                sar       ecx,0Ch
  004c49c2  896e1c                mov       [esi+1Ch],ebp
  004c49c5  8b6e24                mov       ebp,[esi+24h]
  004c49c8  03c8                  add       ecx,eax
  004c49ca  03da                  add       ebx,edx
  004c49cc  03e9                  add       ebp,ecx
  004c49ce  897e50                mov       [esi+50h],edi
  004c49d1  895654                mov       [esi+54h],edx
  004c49d4  894658                mov       [esi+58h],eax
  004c49d7  895e20                mov       [esi+20h],ebx
  004c49da  896e24                mov       [esi+24h],ebp
  004c49dd  e85eef0600            call      00533940h
  004c49e2  8b9690000000          mov       edx,[esi+90h]
  004c49e8  8b4660                mov       eax,[esi+60h]
  004c49eb  8b4e20                mov       ecx,[esi+20h]
  004c49ee  83c404                add       esp,4
  004c49f1  80e6fc                and       dh,0FCh
  004c49f4  3bc8                  cmp       ecx,eax
  004c49f6  899690000000          mov       [esi+90h],edx
  004c49fc  7f2a                  jg        short 004C4A28h
  004c49fe  f6c680                test      dh,80h
  004c4a01  7525                  jne       short 004C4A28h
  004c4a03  8b0e                  mov       ecx,[esi]
  004c4a05  894620                mov       [esi+20h],eax
  004c4a08  f6c101                test      cl,1
  004c4a0b  740f                  je        short 004C4A1Ch
  004c4a0d  83e1fe                and       ecx,0FFFFFFFEh
  004c4a10  33db                  xor       ebx,ebx
  004c4a12  890e                  mov       [esi],ecx
  004c4a14  895e54                mov       [esi+54h],ebx
  004c4a17  e935010000            jmp       004C4B51h

LAB_004c4a1c:
  004c4a1c  83e1fe                and       ecx,0FFFFFFFEh
  004c4a1f  33db                  xor       ebx,ebx
  004c4a21  890e                  mov       [esi],ecx
  004c4a23  895e54                mov       [esi+54h],ebx
  004c4a26  eb02                  jmp       short 004C4A2Ah

LAB_004c4a28:
  004c4a28  33db                  xor       ebx,ebx

LAB_004c4a2a:
  004c4a2a  8b0e                  mov       ecx,[esi]
  004c4a2c  f6c101                test      cl,1
  004c4a2f  750a                  jne       short 004C4A3Bh
  004c4a31  394620                cmp       [esi+20h],eax
  004c4a34  7e05                  jle       short 004C4A3Bh
  004c4a36  83c901                or        ecx,1
  004c4a39  890e                  mov       [esi],ecx

LAB_004c4a3b:
  004c4a3b  f7c282ad0020          test      edx,2000AD82h
  004c4a41  0f840a010000          je        near 004C4B51h
  004c4a47  f60601                test      byte [esi],1
  004c4a4a  0f85d9000000          jne       near 004C4B29h
  004c4a50  8a4e42                mov       cl,[esi+42h]
  004c4a53  b007                  mov       al,7
  004c4a55  3ac8                  cmp       cl,al
  004c4a57  7420                  je        short 004C4A79h
  004c4a59  884642                mov       [esi+42h],al
  004c4a5c  e8df260800            call      00547140h
  004c4a61  99                    cdq
  004c4a62  b928000000            mov       ecx,28h
  004c4a67  66895e12              mov       [esi+12h],bx
  004c4a6b  f7f9                  idiv      ecx
  004c4a6d  66c746105000          mov       word [esi+10h],50h
  004c4a73  80c214                add       dl,14h
  004c4a76  885643                mov       [esi+43h],dl

LAB_004c4a79:
  004c4a79  8b16                  mov       edx,[esi]
  004c4a7b  8b442428              mov       eax,[esp+28h]
  004c4a7f  8b4c242c              mov       ecx,[esp+2Ch]
  004c4a83  83e2fd                and       edx,0FFFFFFFDh
  004c4a86  8916                  mov       [esi],edx
  004c4a88  8b542424              mov       edx,[esp+24h]
  004c4a8c  894620                mov       [esi+20h],eax
  004c4a8f  8b442458              mov       eax,[esp+58h]
  004c4a93  89561c                mov       [esi+1Ch],edx
  004c4a96  8b542454              mov       edx,[esp+54h]
  004c4a9a  25ffff0000            and       eax,0FFFFh
  004c4a9f  894e24                mov       [esi+24h],ecx
  004c4aa2  8b4c2450              mov       ecx,[esp+50h]
  004c4aa6  89567c                mov       [esi+7Ch],edx
  004c4aa9  8b54243c              mov       edx,[esp+3Ch]
  004c4aad  898690000000          mov       [esi+90h],eax
  004c4ab3  8b442444              mov       eax,[esp+44h]
  004c4ab7  898e84000000          mov       [esi+84h],ecx
  004c4abd  8b4c2440              mov       ecx,[esp+40h]
  004c4ac1  898680000000          mov       [esi+80h],eax
  004c4ac7  8b44244c              mov       eax,[esp+4Ch]
  004c4acb  899688000000          mov       [esi+88h],edx
  004c4ad1  8b542448              mov       edx,[esp+48h]
  004c4ad5  6882ad0020            push      2000AD82h
  004c4ada  56                    push      esi
  004c4adb  89868c000000          mov       [esi+8Ch],eax
  004c4ae1  894e64                mov       [esi+64h],ecx
  004c4ae4  895660                mov       [esi+60h],edx
  004c4ae7  e874130100            call      004D5E60h
  004c4aec  83c408                add       esp,8
  004c4aef  663bc3                cmp       ax,bx
  004c4af2  7525                  jne       short 004C4B19h
  004c4af4  e847260800            call      00547140h
  004c4af9  99                    cdq
  004c4afa  33c2                  xor       eax,edx
  004c4afc  8b4c2410              mov       ecx,[esp+10h]
  004c4b00  2bc2                  sub       eax,edx
  004c4b02  83c140                add       ecx,40h
  004c4b05  83e001                and       eax,1
  004c4b08  33c2                  xor       eax,edx
  004c4b0a  2bc2                  sub       eax,edx
  004c4b0c  8d4400ff              lea       eax,[eax+eax-1]
  004c4b10  0fafc8                imul      ecx,eax
  004c4b13  66014e32              add       [esi+32h],cx
  004c4b17  eb38                  jmp       short 004C4B51h

LAB_004c4b19:
  004c4b19  8b542410              mov       edx,[esp+10h]
  004c4b1d  83c240                add       edx,40h
  004c4b20  0fafd0                imul      edx,eax
  004c4b23  66015632              add       [esi+32h],dx
  004c4b27  eb28                  jmp       short 004C4B51h

LAB_004c4b29:
  004c4b29  8b442424              mov       eax,[esp+24h]
  004c4b2d  8b4c242c              mov       ecx,[esp+2Ch]
  004c4b31  895e5c                mov       [esi+5Ch],ebx
  004c4b34  895e50                mov       [esi+50h],ebx
  004c4b37  895e58                mov       [esi+58h],ebx
  004c4b3a  89461c                mov       [esi+1Ch],eax
  004c4b3d  894e24                mov       [esi+24h],ecx
  004c4b40  885e42                mov       [esi+42h],bl
  004c4b43  c6464328              mov       byte [esi+43h],28h
  004c4b47  66895e12              mov       [esi+12h],bx
  004c4b4b  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c4b51:
  004c4b51  6aff                  push      0FFFFFFFFh
  004c4b53  6aff                  push      0FFFFFFFFh
  004c4b55  6aff                  push      0FFFFFFFFh
  004c4b57  56                    push      esi
  004c4b58  e8b3ecffff            call      004C3810h
  004c4b5d  8b44242c              mov       eax,[esp+2Ch]
  004c4b61  83c410                add       esp,10h
  004c4b64  3bc3                  cmp       eax,ebx
  004c4b66  7517                  jne       short 004C4B7Fh
  004c4b68  668b942482000000      mov       dx,[esp+82h]
  004c4b70  66895632              mov       [esi+32h],dx
  004c4b74  5f                    pop       edi
  004c4b75  5e                    pop       esi
  004c4b76  5d                    pop       ebp
  004c4b77  5b                    pop       ebx
  004c4b78  81c484000000          add       esp,84h
  004c4b7e  c3                    ret

LAB_004c4b7f:
  004c4b7f  385e43                cmp       [esi+43h],bl
  004c4b82  0f8570030000          jne       near 004C4EF8h
  004c4b88  8b466c                mov       eax,[esi+6Ch]
  004c4b8b  8b6e1c                mov       ebp,[esi+1Ch]
  004c4b8e  8b4e24                mov       ecx,[esi+24h]
  004c4b91  8b781c                mov       edi,[eax+1Ch]
  004c4b94  2bfd                  sub       edi,ebp
  004c4b96  8b6824                mov       ebp,[eax+24h]
  004c4b99  2be9                  sub       ebp,ecx
  004c4b9b  c1ff0c                sar       edi,0Ch
  004c4b9e  c1fd0c                sar       ebp,0Ch
  004c4ba1  8bc7                  mov       eax,edi
  004c4ba3  8bcd                  mov       ecx,ebp
  004c4ba5  0fafc7                imul      eax,edi
  004c4ba8  0fafcd                imul      ecx,ebp
  004c4bab  03c1                  add       eax,ecx
  004c4bad  50                    push      eax
  004c4bae  e85d0e0800            call      00545A10h
  004c4bb3  55                    push      ebp
  004c4bb4  57                    push      edi
  004c4bb5  8944241c              mov       [esp+1Ch],eax
  004c4bb9  e8640e0800            call      00545A22h
  004c4bbe  668b5632              mov       dx,[esi+32h]
  004c4bc2  66895e12              mov       [esi+12h],bx
  004c4bc6  2bc2                  sub       eax,edx
  004c4bc8  25ff0f0000            and       eax,0FFFh
  004c4bcd  89442418              mov       [esp+18h],eax
  004c4bd1  8b442414              mov       eax,[esp+14h]
  004c4bd5  3dfa000000            cmp       eax,0FAh
  004c4bda  0f8dcf000000          jge       near 004C4CAFh
  004c4be0  e85b250800            call      00547140h
  004c4be5  99                    cdq
  004c4be6  b964000000            mov       ecx,64h
  004c4beb  f7f9                  idiv      ecx
  004c4bed  83fa32                cmp       edx,32h
  004c4bf0  7d64                  jge       short 004C4C56h
  004c4bf2  8b542418              mov       edx,[esp+18h]
  004c4bf6  8b7c2420              mov       edi,[esp+20h]
  004c4bfa  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004c4c01  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004c4c08  25ff0f0000            and       eax,0FFFh
  004c4c0d  3bc1                  cmp       eax,ecx
  004c4c0f  7f3b                  jg        short 004C4C4Ch
  004c4c11  e82a250800            call      00547140h
  004c4c16  99                    cdq
  004c4c17  8d4c3f01              lea       ecx,[edi+edi+1]
  004c4c1b  f7f9                  idiv      ecx
  004c4c1d  8b442414              mov       eax,[esp+14h]
  004c4c21  83c264                add       edx,64h
  004c4c24  3bc2                  cmp       eax,edx
  004c4c26  7d24                  jge       short 004C4C4Ch
  004c4c28  8b16                  mov       edx,[esi]
  004c4c2a  c646420b              mov       byte [esi+42h],0Bh
  004c4c2e  81e2fffffffe          and       edx,0FEFFFFFFh
  004c4c34  c6464301              mov       byte [esi+43h],1
  004c4c38  83ca02                or        edx,2
  004c4c3b  895e5c                mov       [esi+5Ch],ebx
  004c4c3e  8916                  mov       [esi],edx
  004c4c40  66c746180300          mov       word [esi+18h],3
  004c4c46  66895e1a              mov       [esi+1Ah],bx
  004c4c4a  eb67                  jmp       short 004C4CB3h

LAB_004c4c4c:
  004c4c4c  c6464201              mov       byte [esi+42h],1
  004c4c50  c6464301              mov       byte [esi+43h],1
  004c4c54  eb5d                  jmp       short 004C4CB3h

LAB_004c4c56:
  004c4c56  83fa3c                cmp       edx,3Ch
  004c4c59  7c2b                  jl        short 004C4C86h
  004c4c5b  83fa46                cmp       edx,46h
  004c4c5e  7d26                  jge       short 004C4C86h
  004c4c60  8b4c2410              mov       ecx,[esp+10h]
  004c4c64  8b442414              mov       eax,[esp+14h]
  004c4c68  83c164                add       ecx,64h
  004c4c6b  3bc1                  cmp       eax,ecx
  004c4c6d  7e17                  jle       short 004C4C86h
  004c4c6f  c6464206              mov       byte [esi+42h],6
  004c4c73  c6464301              mov       byte [esi+43h],1
  004c4c77  e8c4240800            call      00547140h
  004c4c7c  66894610              mov       [esi+10h],ax
  004c4c80  66895e12              mov       [esi+12h],bx
  004c4c84  eb29                  jmp       short 004C4CAFh

LAB_004c4c86:
  004c4c86  c6464202              mov       byte [esi+42h],2
  004c4c8a  e8b1240800            call      00547140h
  004c4c8f  99                    cdq
  004c4c90  b90a000000            mov       ecx,0Ah
  004c4c95  8b7c2420              mov       edi,[esp+20h]
  004c4c99  f7f9                  idiv      ecx
  004c4c9b  8bc7                  mov       eax,edi
  004c4c9d  66c746105000          mov       word [esi+10h],50h
  004c4ca3  d1f8                  sar       eax,1
  004c4ca5  02d0                  add       dl,al
  004c4ca7  80c214                add       dl,14h
  004c4caa  885643                mov       [esi+43h],dl
  004c4cad  eb04                  jmp       short 004C4CB3h

LAB_004c4caf:
  004c4caf  8b7c2420              mov       edi,[esp+20h]

LAB_004c4cb3:
  004c4cb3  8b442414              mov       eax,[esp+14h]
  004c4cb7  3d58020000            cmp       eax,258h
  004c4cbc  0f8dfa000000          jge       near 004C4DBCh
  004c4cc2  3dfa000000            cmp       eax,0FAh
  004c4cc7  0f8cef000000          jl        near 004C4DBCh
  004c4ccd  e86e240800            call      00547140h
  004c4cd2  99                    cdq
  004c4cd3  b964000000            mov       ecx,64h
  004c4cd8  f7f9                  idiv      ecx
  004c4cda  83fa14                cmp       edx,14h
  004c4cdd  7d50                  jge       short 004C4D2Fh
  004c4cdf  8b542418              mov       edx,[esp+18h]
  004c4ce3  81c200040000          add       edx,400h
  004c4ce9  81e2ff0f0000          and       edx,0FFFh
  004c4cef  81fa00080000          cmp       edx,800h
  004c4cf5  7c0d                  jl        short 004C4D04h
  004c4cf7  c6464201              mov       byte [esi+42h],1
  004c4cfb  c6464301              mov       byte [esi+43h],1
  004c4cff  e9b8000000            jmp       004C4DBCh

LAB_004c4d04:
  004c4d04  885e42                mov       [esi+42h],bl
  004c4d07  e834240800            call      00547140h
  004c4d0c  99                    cdq
  004c4d0d  b90a000000            mov       ecx,0Ah
  004c4d12  f7f9                  idiv      ecx
  004c4d14  8b4c2410              mov       ecx,[esp+10h]
  004c4d18  8bc7                  mov       eax,edi
  004c4d1a  c1f802                sar       eax,2
  004c4d1d  02d0                  add       dl,al
  004c4d1f  885643                mov       [esi+43h],dl
  004c4d22  8d540950              lea       edx,[ecx+ecx+50h]
  004c4d26  66895610              mov       [esi+10h],dx
  004c4d2a  e989000000            jmp       004C4DB8h

LAB_004c4d2f:
  004c4d2f  83fa28                cmp       edx,28h
  004c4d32  7d25                  jge       short 004C4D59h
  004c4d34  c6464202              mov       byte [esi+42h],2
  004c4d38  e803240800            call      00547140h
  004c4d3d  99                    cdq
  004c4d3e  b90a000000            mov       ecx,0Ah
  004c4d43  66c746105000          mov       word [esi+10h],50h
  004c4d49  f7f9                  idiv      ecx
  004c4d4b  8bc7                  mov       eax,edi
  004c4d4d  d1f8                  sar       eax,1
  004c4d4f  02d0                  add       dl,al
  004c4d51  80c214                add       dl,14h
  004c4d54  885643                mov       [esi+43h],dl
  004c4d57  eb63                  jmp       short 004C4DBCh

LAB_004c4d59:
  004c4d59  83fa3c                cmp       edx,3Ch
  004c4d5c  7d13                  jge       short 004C4D71h
  004c4d5e  c6464206              mov       byte [esi+42h],6
  004c4d62  c6464301              mov       byte [esi+43h],1
  004c4d66  e8d5230800            call      00547140h
  004c4d6b  66894610              mov       [esi+10h],ax
  004c4d6f  eb47                  jmp       short 004C4DB8h

LAB_004c4d71:
  004c4d71  83fa50                cmp       edx,50h
  004c4d74  7d3a                  jge       short 004C4DB0h
  004c4d76  c6464203              mov       byte [esi+42h],3
  004c4d7a  66895e12              mov       [esi+12h],bx
  004c4d7e  e8bd230800            call      00547140h
  004c4d83  99                    cdq
  004c4d84  b90a000000            mov       ecx,0Ah
  004c4d89  f7f9                  idiv      ecx
  004c4d8b  8bc7                  mov       eax,edi
  004c4d8d  d1f8                  sar       eax,1
  004c4d8f  02d0                  add       dl,al
  004c4d91  80c214                add       dl,14h
  004c4d94  885643                mov       [esi+43h],dl
  004c4d97  e8a4230800            call      00547140h
  004c4d9c  99                    cdq
  004c4d9d  33c2                  xor       eax,edx
  004c4d9f  2bc2                  sub       eax,edx
  004c4da1  25ff0f0000            and       eax,0FFFh
  004c4da6  33c2                  xor       eax,edx
  004c4da8  2bc2                  sub       eax,edx
  004c4daa  66894610              mov       [esi+10h],ax
  004c4dae  eb0c                  jmp       short 004C4DBCh

LAB_004c4db0:
  004c4db0  c646420a              mov       byte [esi+42h],0Ah
  004c4db4  c6464301              mov       byte [esi+43h],1

LAB_004c4db8:
  004c4db8  66895e12              mov       [esi+12h],bx

LAB_004c4dbc:
  004c4dbc  817c241458020000      cmp       dword [esp+14h],258h
  004c4dc4  0f8c2e010000          jl        near 004C4EF8h
  004c4dca  e871230800            call      00547140h
  004c4dcf  bd64000000            mov       ebp,64h
  004c4dd4  99                    cdq
  004c4dd5  8bcd                  mov       ecx,ebp
  004c4dd7  f7f9                  idiv      ecx
  004c4dd9  83fa1e                cmp       edx,1Eh
  004c4ddc  7d45                  jge       short 004C4E23h
  004c4dde  8b442410              mov       eax,[esp+10h]
  004c4de2  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004c4de6  3bd1                  cmp       edx,ecx
  004c4de8  0f8ccd000000          jl        near 004C4EBBh
  004c4dee  885e42                mov       [esi+42h],bl
  004c4df1  e84a230800            call      00547140h
  004c4df6  99                    cdq
  004c4df7  b90a000000            mov       ecx,0Ah
  004c4dfc  66895e12              mov       [esi+12h],bx
  004c4e00  f7f9                  idiv      ecx
  004c4e02  8b4c2410              mov       ecx,[esp+10h]
  004c4e06  8bc7                  mov       eax,edi
  004c4e08  c1f802                sar       eax,2
  004c4e0b  02d0                  add       dl,al
  004c4e0d  885643                mov       [esi+43h],dl
  004c4e10  8d540950              lea       edx,[ecx+ecx+50h]
  004c4e14  66895610              mov       [esi+10h],dx
  004c4e18  5f                    pop       edi
  004c4e19  5e                    pop       esi
  004c4e1a  5d                    pop       ebp
  004c4e1b  5b                    pop       ebx
  004c4e1c  81c484000000          add       esp,84h
  004c4e22  c3                    ret

LAB_004c4e23:
  004c4e23  83fa28                cmp       edx,28h
  004c4e26  7d43                  jge       short 004C4E6Bh
  004c4e28  c6464203              mov       byte [esi+42h],3
  004c4e2c  66895e12              mov       [esi+12h],bx
  004c4e30  e80b230800            call      00547140h
  004c4e35  99                    cdq
  004c4e36  b90a000000            mov       ecx,0Ah
  004c4e3b  f7f9                  idiv      ecx
  004c4e3d  8bc7                  mov       eax,edi
  004c4e3f  d1f8                  sar       eax,1
  004c4e41  02d0                  add       dl,al
  004c4e43  80c214                add       dl,14h
  004c4e46  885643                mov       [esi+43h],dl
  004c4e49  e8f2220800            call      00547140h
  004c4e4e  99                    cdq
  004c4e4f  33c2                  xor       eax,edx
  004c4e51  2bc2                  sub       eax,edx
  004c4e53  25ff0f0000            and       eax,0FFFh
  004c4e58  33c2                  xor       eax,edx
  004c4e5a  2bc2                  sub       eax,edx
  004c4e5c  66894610              mov       [esi+10h],ax
  004c4e60  5f                    pop       edi
  004c4e61  5e                    pop       esi
  004c4e62  5d                    pop       ebp
  004c4e63  5b                    pop       ebx
  004c4e64  81c484000000          add       esp,84h
  004c4e6a  c3                    ret

LAB_004c4e6b:
  004c4e6b  83fa3c                cmp       edx,3Ch
  004c4e6e  7d13                  jge       short 004C4E83h
  004c4e70  c6464201              mov       byte [esi+42h],1
  004c4e74  c6464301              mov       byte [esi+43h],1
  004c4e78  5f                    pop       edi
  004c4e79  5e                    pop       esi
  004c4e7a  5d                    pop       ebp
  004c4e7b  5b                    pop       ebx
  004c4e7c  81c484000000          add       esp,84h
  004c4e82  c3                    ret

LAB_004c4e83:
  004c4e83  83fa50                cmp       edx,50h
  004c4e86  7d5f                  jge       short 004C4EE7h
  004c4e88  8b4c2418              mov       ecx,[esp+18h]
  004c4e8c  8d04fd00020000        lea       eax,[edi*8+200h]
  004c4e93  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004c4e9a  81e2ff0f0000          and       edx,0FFFh
  004c4ea0  3bd0                  cmp       edx,eax
  004c4ea2  7f17                  jg        short 004C4EBBh
  004c4ea4  c646420a              mov       byte [esi+42h],0Ah
  004c4ea8  c6464301              mov       byte [esi+43h],1
  004c4eac  66895e12              mov       [esi+12h],bx
  004c4eb0  5f                    pop       edi
  004c4eb1  5e                    pop       esi
  004c4eb2  5d                    pop       ebp
  004c4eb3  5b                    pop       ebx
  004c4eb4  81c484000000          add       esp,84h
  004c4eba  c3                    ret

LAB_004c4ebb:
  004c4ebb  c6464202              mov       byte [esi+42h],2
  004c4ebf  e87c220800            call      00547140h
  004c4ec4  99                    cdq
  004c4ec5  b90a000000            mov       ecx,0Ah
  004c4eca  66896e10              mov       [esi+10h],bp
  004c4ece  f7f9                  idiv      ecx
  004c4ed0  8bc7                  mov       eax,edi
  004c4ed2  d1f8                  sar       eax,1
  004c4ed4  02d0                  add       dl,al
  004c4ed6  80c214                add       dl,14h
  004c4ed9  885643                mov       [esi+43h],dl
  004c4edc  5f                    pop       edi
  004c4edd  5e                    pop       esi
  004c4ede  5d                    pop       ebp
  004c4edf  5b                    pop       ebx
  004c4ee0  81c484000000          add       esp,84h
  004c4ee6  c3                    ret

LAB_004c4ee7:
  004c4ee7  c6464206              mov       byte [esi+42h],6
  004c4eeb  c6464301              mov       byte [esi+43h],1
  004c4eef  e84c220800            call      00547140h
  004c4ef4  66894610              mov       [esi+10h],ax

LAB_004c4ef8:
  004c4ef8  5f                    pop       edi
  004c4ef9  5e                    pop       esi
  004c4efa  5d                    pop       ebp
  004c4efb  5b                    pop       ebx
  004c4efc  81c484000000          add       esp,84h
  004c4f02  c3                    ret

LAB_004c4f03:
  004c4f03  83e1fd                and       ecx,0FFFFFFFDh
  004c4f06  890e                  mov       [esi],ecx
  004c4f08  e9bef5ffff            jmp       004C44CBh

LAB_004c4f0d:
  004c4f0d  f6c101                test      cl,1
  004c4f10  0f84b5f5ffff          je        near 004C44CBh
  004c4f16  66837e1a03            cmp       word [esi+1Ah],3
  004c4f1b  0f8daaf5ffff          jge       near 004C44CBh
  004c4f21  e81a220800            call      00547140h
  004c4f26  99                    cdq
  004c4f27  b928000000            mov       ecx,28h
  004c4f2c  f7f9                  idiv      ecx
  004c4f2e  8b4c2410              mov       ecx,[esp+10h]
  004c4f32  3bd1                  cmp       edx,ecx
  004c4f34  7d20                  jge       short 004C4F56h
  004c4f36  8b16                  mov       edx,[esi]
  004c4f38  8b44241c              mov       eax,[esp+1Ch]
  004c4f3c  81e2fffffffe          and       edx,0FEFFFFFFh
  004c4f42  66c746180300          mov       word [esi+18h],3
  004c4f48  83ca02                or        edx,2
  004c4f4b  66895e1a              mov       [esi+1Ah],bx
  004c4f4f  8916                  mov       [esi],edx
  004c4f51  e979f5ffff            jmp       004C44CFh

LAB_004c4f56:
  004c4f56  8b44241c              mov       eax,[esp+1Ch]
  004c4f5a  e970f5ffff            jmp       004C44CFh
  004c4f5f  90                    nop
  004c4f60  e444                  in        al,44h
  004c4f62  4c                    dec       esp
  004c4f63  002e                  add       [esi],ch
  004c4f65  45                    inc       ebp
  004c4f66  4c                    dec       esp
  004c4f67  00a3454c0048          add       [ebx+48004C45h],ah
  004c4f6d  46                    inc       esi
  004c4f6e  4c                    dec       esp
  004c4f6f  004249                add       [edx+49h],al
  004c4f72  4c                    dec       esp
  004c4f73  004249                add       [edx+49h],al
  004c4f76  4c                    dec       esp
  004c4f77  0013                  add       [ebx],dl
  004c4f79  47                    inc       edi
  004c4f7a  4c                    dec       esp
  004c4f7b  00a5474c00fd          add       [ebp-2FFB3B9h],ah
  004c4f81  47                    inc       edi
  004c4f82  4c                    dec       esp
  004c4f83  0084484c00bf48        add       [eax+ecx*2+48BF004Ch],al
  004c4f8a  4c                    dec       esp
  004c4f8b  001449                add       [ecx+ecx*2],dl
  004c4f8e  4c                    dec       esp
  004c4f8f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
