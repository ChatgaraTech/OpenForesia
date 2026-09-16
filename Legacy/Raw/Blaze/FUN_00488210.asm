Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00488210  section .text  offset fichier 0x87610  1936 octets

  00488210  83ec2c                sub       esp,2Ch
  00488213  53                    push      ebx
  00488214  55                    push      ebp
  00488215  56                    push      esi
  00488216  8b74243c              mov       esi,[esp+3Ch]
  0048821a  8d442424              lea       eax,[esp+24h]
  0048821e  57                    push      edi
  0048821f  8d4c242c              lea       ecx,[esp+2Ch]
  00488223  50                    push      eax
  00488224  8d542428              lea       edx,[esp+28h]
  00488228  51                    push      ecx
  00488229  33ed                  xor       ebp,ebp
  0048822b  52                    push      edx
  0048822c  56                    push      esi
  0048822d  66896c2440            mov       [esp+40h],bp
  00488232  66896c2442            mov       [esp+42h],bp
  00488237  66c74424443200        mov       word [esp+44h],32h
  0048823e  66c74424466400        mov       word [esp+46h],64h
  00488245  66896c2448            mov       [esp+48h],bp
  0048824a  66c744244a5a00        mov       word [esp+4Ah],5Ah
  00488251  e82a070b00            call      00538980h
  00488256  83c410                add       esp,10h
  00488259  8d442420              lea       eax,[esp+20h]
  0048825d  8d4c241c              lea       ecx,[esp+1Ch]
  00488261  8d542440              lea       edx,[esp+40h]
  00488265  50                    push      eax
  00488266  51                    push      ecx
  00488267  8d442420              lea       eax,[esp+20h]
  0048826b  52                    push      edx
  0048826c  8d4c2420              lea       ecx,[esp+20h]
  00488270  50                    push      eax
  00488271  51                    push      ecx
  00488272  56                    push      esi
  00488273  e8a8dc0a00            call      00535F20h
  00488278  8b4c242c              mov       ecx,[esp+2Ch]
  0048827c  b840000000            mov       eax,40h
  00488281  2bc1                  sub       eax,ecx
  00488283  83c418                add       esp,18h
  00488286  89442410              mov       [esp+10h],eax
  0048828a  8b44241c              mov       eax,[esp+1Ch]
  0048828e  3bc5                  cmp       eax,ebp
  00488290  bf01000000            mov       edi,1
  00488295  0f84e5030000          je        near 00488680h
  0048829b  56                    push      esi
  0048829c  e8ff320500            call      004DB5A0h
  004882a1  8b06                  mov       eax,[esi]
  004882a3  83c404                add       esp,4
  004882a6  a802                  test      al,2
  004882a8  0f849c000000          je        near 0048834Ah
  004882ae  8a4c2420              mov       cl,[esp+20h]
  004882b2  bb10000000            mov       ebx,10h
  004882b7  84cb                  test      bl,cl
  004882b9  740d                  je        short 004882C8h
  004882bb  24fd                  and       al,0FDh
  004882bd  c6464300              mov       byte [esi+43h],0
  004882c1  8906                  mov       [esi],eax
  004882c3  e9b8030000            jmp       00488680h

LAB_004882c8:
  004882c8  66837e1a08            cmp       word [esi+1Ah],8
  004882cd  7c58                  jl        short 00488327h
  004882cf  8d542430              lea       edx,[esp+30h]
  004882d3  55                    push      ebp
  004882d4  8d461c                lea       eax,[esi+1Ch]
  004882d7  52                    push      edx
  004882d8  50                    push      eax
  004882d9  56                    push      esi
  004882da  e851190900            call      00519C30h
  004882df  8bf8                  mov       edi,eax
  004882e1  a1b8e05d00            mov       eax,[5DE0B8h]
  004882e6  83c410                add       esp,10h
  004882e9  3bc5                  cmp       eax,ebp
  004882eb  7430                  je        short 0048831Dh
  004882ed  66899f96000000        mov       [edi+96h],bx
  004882f4  8b1f                  mov       ebx,[edi]
  004882f6  83cb40                or        ebx,40h
  004882f9  57                    push      edi
  004882fa  56                    push      esi
  004882fb  891f                  mov       [edi],ebx
  004882fd  e87e180b00            call      00539B80h
  00488302  83c408                add       esp,8
  00488305  83c71c                add       edi,1Ch
  00488308  57                    push      edi
  00488309  6800180000            push      1800h
  0048830e  6800030000            push      300h
  00488313  6a03                  push      3
  00488315  e806580b00            call      0053DB20h
  0048831a  83c410                add       esp,10h

LAB_0048831d:
  0048831d  8b06                  mov       eax,[esi]
  0048831f  a801                  test      al,1
  00488321  740c                  je        short 0048832Fh
  00488323  66ff4e1a              dec       word [esi+1Ah]

LAB_00488327:
  00488327  a801                  test      al,1
  00488329  0f8551030000          jne       near 00488680h

LAB_0048832f:
  0048832f  a900000002            test      eax,2000000h
  00488334  0f841c020000          je        near 00488556h
  0048833a  24fd                  and       al,0FDh
  0048833c  c6464300              mov       byte [esi+43h],0
  00488340  8906                  mov       [esi],eax
  00488342  896e5c                mov       [esi+5Ch],ebp
  00488345  e936030000            jmp       00488680h

LAB_0048834a:
  0048834a  a810                  test      al,10h
  0048834c  0f852e030000          jne       near 00488680h
  00488352  a904080000            test      eax,804h
  00488357  0f8452010000          je        near 004884AFh
  0048835d  f7442420e8030040      test      dword [esp+20h],400003E8h
  00488365  740b                  je        short 00488372h
  00488367  25fbf7ffff            and       eax,0FFFFF7FBh
  0048836c  c6464300              mov       byte [esi+43h],0
  00488370  8906                  mov       [esi],eax

LAB_00488372:
  00488372  668b4610              mov       ax,[esi+10h]
  00488376  0fbfc8                movsx     ecx,ax
  00488379  83f903                cmp       ecx,3
  0048837c  0f87fe020000          ja        near 00488680h
  00488382  ff248d80894800        jmp       dword [ecx*4+488980h]
  00488389  8b06                  mov       eax,[esi]
  0048838b  a900000002            test      eax,2000000h
  00488390  0f84ea020000          je        near 00488680h
  00488396  80e4f7                and       ah,0F7h
  00488399  8906                  mov       [esi],eax
  0048839b  e9e0020000            jmp       00488680h
  004883a0  b805000000            mov       eax,5
  004883a5  66394618              cmp       [esi+18h],ax
  004883a9  7411                  je        short 004883BCh
  004883ab  894618                mov       [esi+18h],eax
  004883ae  8b06                  mov       eax,[esi]
  004883b0  25fbfffffe            and       eax,0FEFFFFFBh
  004883b5  8906                  mov       [esi],eax
  004883b7  e9c4020000            jmp       00488680h

LAB_004883bc:
  004883bc  66837e1a07            cmp       word [esi+1Ah],7
  004883c1  0f85b9020000          jne       near 00488680h
  004883c7  33c0                  xor       eax,eax
  004883c9  6aff                  push      0FFFFFFFFh
  004883cb  668b4606              mov       ax,[esi+6]
  004883cf  6aff                  push      0FFFFFFFFh
  004883d1  8bc8                  mov       ecx,eax
  004883d3  c1f808                sar       eax,8
  004883d6  81e1ff000000          and       ecx,0FFh
  004883dc  25ff000000            and       eax,0FFh
  004883e1  51                    push      ecx
  004883e2  50                    push      eax
  004883e3  56                    push      esi
  004883e4  e897250b00            call      0053A980h
  004883e9  83c414                add       esp,14h
  004883ec  66ff4e10              dec       word [esi+10h]
  004883f0  e98b020000            jmp       00488680h
  004883f5  8a4e43                mov       cl,[esi+43h]
  004883f8  fec9                  dec       cl
  004883fa  884e43                mov       [esi+43h],cl
  004883fd  7505                  jne       short 00488404h
  004883ff  48                    dec       eax
  00488400  66894610              mov       [esi+10h],ax

LAB_00488404:
  00488404  8b542414              mov       edx,[esp+14h]
  00488408  8b4c2440              mov       ecx,[esp+40h]
  0048840c  8d049532000000        lea       eax,[edx*4+32h]
  00488413  8d1408                lea       edx,[eax+ecx]
  00488416  8d3c00                lea       edi,[eax+eax]
  00488419  81e2ff0f0000          and       edx,0FFFh
  0048841f  3bd7                  cmp       edx,edi
  00488421  7f09                  jg        short 0048842Ch
  00488423  66014e32              add       [esi+32h],cx
  00488427  e954020000            jmp       00488680h

LAB_0048842c:
  0048842c  81e1ff0f0000          and       ecx,0FFFh
  00488432  81f900080000          cmp       ecx,800h
  00488438  0f8c3e020000          jl        near 0048867Ch
  0048843e  e937020000            jmp       0048867Ah
  00488443  55                    push      ebp
  00488444  55                    push      ebp
  00488445  68bd000000            push      0BDh
  0048844a  56                    push      esi
  0048844b  e89026ffff            call      0047AAE0h
  00488450  83c410                add       esp,10h
  00488453  e8e8ec0b00            call      00547140h
  00488458  99                    cdq
  00488459  b903000000            mov       ecx,3
  0048845e  f7f9                  idiv      ecx
  00488460  0fbf4e14              movsx     ecx,word [esi+14h]
  00488464  33c0                  xor       eax,eax
  00488466  8d144a                lea       edx,[edx+ecx*2]
  00488469  03ca                  add       ecx,edx
  0048846b  668b044d80285500      mov       ax,[ecx*2+552880h]
  00488473  66894606              mov       [esi+6],ax
  00488477  8b15c4975e00          mov       edx,[5E97C4h]
  0048847d  8bc8                  mov       ecx,eax
  0048847f  25ff000000            and       eax,0FFh
  00488484  8b929c000000          mov       edx,[edx+9Ch]
  0048848a  c1f908                sar       ecx,8
  0048848d  81e1ff000000          and       ecx,0FFh
  00488493  8d0440                lea       eax,[eax+eax*2]
  00488496  c1e004                shl       eax,4
  00488499  8b0c8a                mov       ecx,[edx+ecx*4]
  0048849c  8a540113              mov       dl,[ecx+eax+13h]
  004884a0  66ff4e10              dec       word [esi+10h]
  004884a4  885643                mov       [esi+43h],dl
  004884a7  896e5c                mov       [esi+5Ch],ebp
  004884aa  e9d1010000            jmp       00488680h

LAB_004884af:
  004884af  a801                  test      al,1
  004884b1  0f85c9010000          jne       near 00488680h
  004884b7  33c9                  xor       ecx,ecx
  004884b9  8a4e42                mov       cl,[esi+42h]
  004884bc  83f903                cmp       ecx,3
  004884bf  0f87bb010000          ja        near 00488680h
  004884c5  ff248d90894800        jmp       dword [ecx*4+488990h]
  004884cc  66396e18              cmp       [esi+18h],bp
  004884d0  740a                  je        short 004884DCh
  004884d2  0d00000001            or        eax,1000000h
  004884d7  896e18                mov       [esi+18h],ebp
  004884da  8906                  mov       [esi],eax

LAB_004884dc:
  004884dc  817c24182c010000      cmp       dword [esp+18h],12Ch
  004884e4  7d09                  jge       short 004884EFh
  004884e6  c6464300              mov       byte [esi+43h],0
  004884ea  e991010000            jmp       00488680h

LAB_004884ef:
  004884ef  8a4643                mov       al,[esi+43h]
  004884f2  84c0                  test      al,al
  004884f4  7460                  je        short 00488556h
  004884f6  fec8                  dec       al
  004884f8  896e5c                mov       [esi+5Ch],ebp
  004884fb  884643                mov       [esi+43h],al
  004884fe  e97d010000            jmp       00488680h
  00488503  66397e18              cmp       [esi+18h],di
  00488507  740a                  je        short 00488513h
  00488509  0d00000001            or        eax,1000000h
  0048850e  897e18                mov       [esi+18h],edi
  00488511  8906                  mov       [esi],eax

LAB_00488513:
  00488513  8b442414              mov       eax,[esp+14h]
  00488517  8b4c2440              mov       ecx,[esp+40h]
  0048851b  8d840080000000        lea       eax,[eax+eax+80h]
  00488522  8d1408                lea       edx,[eax+ecx]
  00488525  8d3c00                lea       edi,[eax+eax]
  00488528  81e2ff0f0000          and       edx,0FFFh
  0048852e  3bd7                  cmp       edx,edi
  00488530  7f10                  jg        short 00488542h
  00488532  66014e32              add       [esi+32h],cx
  00488536  c6464300              mov       byte [esi+43h],0
  0048853a  896e5c                mov       [esi+5Ch],ebp
  0048853d  e93e010000            jmp       00488680h

LAB_00488542:
  00488542  81e1ff0f0000          and       ecx,0FFFh
  00488548  81f900080000          cmp       ecx,800h
  0048854e  7c02                  jl        short 00488552h
  00488550  f7d8                  neg       eax

LAB_00488552:
  00488552  66014632              add       [esi+32h],ax

LAB_00488556:
  00488556  896e5c                mov       [esi+5Ch],ebp
  00488559  e922010000            jmp       00488680h
  0048855e  66397e18              cmp       [esi+18h],di
  00488562  740a                  je        short 0048856Eh
  00488564  0d00000001            or        eax,1000000h
  00488569  897e18                mov       [esi+18h],edi
  0048856c  8906                  mov       [esi],eax

LAB_0048856e:
  0048856e  0fbf4610              movsx     eax,word [esi+10h]
  00488572  39442418              cmp       [esp+18h],eax
  00488576  7d09                  jge       short 00488581h
  00488578  c6464300              mov       byte [esi+43h],0
  0048857c  e9ff000000            jmp       00488680h

LAB_00488581:
  00488581  8a4643                mov       al,[esi+43h]
  00488584  84c0                  test      al,al
  00488586  7405                  je        short 0048858Dh
  00488588  fec8                  dec       al
  0048858a  884643                mov       [esi+43h],al

LAB_0048858d:
  0048858d  8b542414              mov       edx,[esp+14h]
  00488591  8b4c2440              mov       ecx,[esp+40h]
  00488595  8d841280000000        lea       eax,[edx+edx+80h]
  0048859c  8d3c08                lea       edi,[eax+ecx]
  0048859f  8d1c00                lea       ebx,[eax+eax]
  004885a2  81e7ff0f0000          and       edi,0FFFh
  004885a8  3bfb                  cmp       edi,ebx
  004885aa  7f15                  jg        short 004885C1h
  004885ac  66014e32              add       [esi+32h],cx
  004885b0  81c2c0000000          add       edx,0C0h
  004885b6  c1e207                shl       edx,7
  004885b9  89565c                mov       [esi+5Ch],edx
  004885bc  e9bf000000            jmp       00488680h

LAB_004885c1:
  004885c1  81e1ff0f0000          and       ecx,0FFFh
  004885c7  81f900080000          cmp       ecx,800h
  004885cd  7c02                  jl        short 004885D1h
  004885cf  f7d8                  neg       eax

LAB_004885d1:
  004885d1  66014632              add       [esi+32h],ax
  004885d5  81c2c0000000          add       edx,0C0h
  004885db  c1e207                shl       edx,7
  004885de  89565c                mov       [esi+5Ch],edx
  004885e1  e99a000000            jmp       00488680h
  004885e6  8a4e43                mov       cl,[esi+43h]
  004885e9  84c9                  test      cl,cl
  004885eb  7405                  je        short 004885F2h
  004885ed  fec9                  dec       cl
  004885ef  884e43                mov       [esi+43h],cl

LAB_004885f2:
  004885f2  66397e18              cmp       [esi+18h],di
  004885f6  740a                  je        short 00488602h
  004885f8  0d00000001            or        eax,1000000h
  004885fd  897e18                mov       [esi+18h],edi
  00488600  8906                  mov       [esi],eax

LAB_00488602:
  00488602  8b4c2414              mov       ecx,[esp+14h]
  00488606  0fbf5610              movsx     edx,word [esi+10h]
  0048860a  81c180000000          add       ecx,80h
  00488610  52                    push      edx
  00488611  c1e107                shl       ecx,7
  00488614  894e5c                mov       [esi+5Ch],ecx
  00488617  e800d40b00            call      00545A1Ch
  0048861c  0faf465c              imul      eax,[esi+5Ch]
  00488620  8b7e1c                mov       edi,[esi+1Ch]
  00488623  c1f80c                sar       eax,0Ch
  00488626  03f8                  add       edi,eax
  00488628  0fbf4610              movsx     eax,word [esi+10h]
  0048862c  50                    push      eax
  0048862d  897e1c                mov       [esi+1Ch],edi
  00488630  e8e1d30b00            call      00545A16h
  00488635  0faf465c              imul      eax,[esi+5Ch]
  00488639  8b5e24                mov       ebx,[esi+24h]
  0048863c  8b4c2414              mov       ecx,[esp+14h]
  00488640  c1f80c                sar       eax,0Ch
  00488643  03d8                  add       ebx,eax
  00488645  8d048d32000000        lea       eax,[ecx*4+32h]
  0048864c  8b4c2440              mov       ecx,[esp+40h]
  00488650  895e24                mov       [esi+24h],ebx
  00488653  8d3c00                lea       edi,[eax+eax]
  00488656  896e5c                mov       [esi+5Ch],ebp
  00488659  8d1408                lea       edx,[eax+ecx]
  0048865c  81e2ff0f0000          and       edx,0FFFh
  00488662  3bd7                  cmp       edx,edi
  00488664  7f06                  jg        short 0048866Ch
  00488666  66014e32              add       [esi+32h],cx
  0048866a  eb14                  jmp       short 00488680h

LAB_0048866c:
  0048866c  81e1ff0f0000          and       ecx,0FFFh
  00488672  81f900080000          cmp       ecx,800h
  00488678  7c02                  jl        short 0048867Ch

LAB_0048867a:
  0048867a  f7d8                  neg       eax

LAB_0048867c:
  0048867c  66014632              add       [esi+32h],ax

LAB_00488680:
  00488680  8b44241c              mov       eax,[esp+1Ch]
  00488684  50                    push      eax
  00488685  56                    push      esi
  00488686  e8d5faffff            call      00488160h
  0048868b  83c408                add       esp,8
  0048868e  56                    push      esi
  0048868f  e8acb20a00            call      00533940h
  00488694  83c404                add       esp,4
  00488697  56                    push      esi
  00488698  e853740500            call      004DFAF0h
  0048869d  8b4c2428              mov       ecx,[esp+28h]
  004886a1  83c404                add       esp,4
  004886a4  51                    push      ecx
  004886a5  56                    push      esi
  004886a6  e8c5b00500            call      004E3770h
  004886ab  8b442424              mov       eax,[esp+24h]
  004886af  83c408                add       esp,8
  004886b2  3bc5                  cmp       eax,ebp
  004886b4  0f84be020000          je        near 00488978h
  004886ba  f70617080000          test      dword [esi],817h
  004886c0  0f85b2020000          jne       near 00488978h
  004886c6  8a4643                mov       al,[esi+43h]
  004886c9  84c0                  test      al,al
  004886cb  0f85a7020000          jne       near 00488978h
  004886d1  8b466c                mov       eax,[esi+6Ch]
  004886d4  8b5e1c                mov       ebx,[esi+1Ch]
  004886d7  8b4e24                mov       ecx,[esi+24h]
  004886da  8b781c                mov       edi,[eax+1Ch]
  004886dd  8b6824                mov       ebp,[eax+24h]
  004886e0  2bfb                  sub       edi,ebx
  004886e2  2be9                  sub       ebp,ecx
  004886e4  c1ff0c                sar       edi,0Ch
  004886e7  c1fd0c                sar       ebp,0Ch
  004886ea  8bd7                  mov       edx,edi
  004886ec  8bc5                  mov       eax,ebp
  004886ee  0fafd7                imul      edx,edi
  004886f1  0fafc5                imul      eax,ebp
  004886f4  03d0                  add       edx,eax
  004886f6  52                    push      edx
  004886f7  e814d30b00            call      00545A10h
  004886fc  55                    push      ebp
  004886fd  57                    push      edi
  004886fe  89442420              mov       [esp+20h],eax
  00488702  e81bd30b00            call      00545A22h
  00488707  668b4e32              mov       cx,[esi+32h]
  0048870b  2bc1                  sub       eax,ecx
  0048870d  25ff0f0000            and       eax,0FFFh
  00488712  89442440              mov       [esp+40h],eax
  00488716  8b442418              mov       eax,[esp+18h]
  0048871a  3dfa000000            cmp       eax,0FAh
  0048871f  0f8dd0000000          jge       near 004887F5h
  00488725  e816ea0b00            call      00547140h
  0048872a  99                    cdq
  0048872b  b964000000            mov       ecx,64h
  00488730  f7f9                  idiv      ecx
  00488732  83fa1e                cmp       edx,1Eh
  00488735  7d61                  jge       short 00488798h
  00488737  8b542440              mov       edx,[esp+40h]
  0048873b  8b7c2410              mov       edi,[esp+10h]
  0048873f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00488746  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048874d  25ff0f0000            and       eax,0FFFh
  00488752  3bc1                  cmp       eax,ecx
  00488754  0f8fba010000          jg        near 00488914h
  0048875a  e8e1e90b00            call      00547140h
  0048875f  99                    cdq
  00488760  8d4c3f01              lea       ecx,[edi+edi+1]
  00488764  f7f9                  idiv      ecx
  00488766  8b442418              mov       eax,[esp+18h]
  0048876a  83c250                add       edx,50h
  0048876d  3bc2                  cmp       eax,edx
  0048876f  0f8d9f010000          jge       near 00488914h
  00488775  8b16                  mov       edx,[esi]
  00488777  c7461803000000        mov       dword [esi+18h],3
  0048877e  81e2fffffffe          and       edx,0FEFFFFFFh
  00488784  c7465c00000000        mov       dword [esi+5Ch],0
  0048878b  83ca02                or        edx,2
  0048878e  8916                  mov       [esi],edx
  00488790  5f                    pop       edi
  00488791  5e                    pop       esi
  00488792  5d                    pop       ebp
  00488793  5b                    pop       ebx
  00488794  83c42c                add       esp,2Ch
  00488797  c3                    ret

LAB_00488798:
  00488798  83fa3c                cmp       edx,3Ch
  0048879b  0f8cf6000000          jl        near 00488897h
  004887a1  c6464203              mov       byte [esi+42h],3
  004887a5  e896e90b00            call      00547140h
  004887aa  99                    cdq
  004887ab  b90a000000            mov       ecx,0Ah
  004887b0  f7f9                  idiv      ecx
  004887b2  8b442410              mov       eax,[esp+10h]
  004887b6  8b4c2414              mov       ecx,[esp+14h]
  004887ba  d1f8                  sar       eax,1
  004887bc  8db980000000          lea       edi,[ecx+80h]
  004887c2  c1e704                shl       edi,4
  004887c5  02d0                  add       dl,al
  004887c7  80c214                add       dl,14h
  004887ca  885643                mov       [esi+43h],dl
  004887cd  e86ee90b00            call      00547140h
  004887d2  99                    cdq
  004887d3  f7ff                  idiv      edi
  004887d5  d1ff                  sar       edi,1
  004887d7  2bd7                  sub       edx,edi
  004887d9  8b7c2440              mov       edi,[esp+40h]
  004887dd  03d7                  add       edx,edi
  004887df  66035632              add       dx,[esi+32h]
  004887e3  81c200080000          add       edx,800h
  004887e9  66895610              mov       [esi+10h],dx
  004887ed  5f                    pop       edi
  004887ee  5e                    pop       esi
  004887ef  5d                    pop       ebp
  004887f0  5b                    pop       ebx
  004887f1  83c42c                add       esp,2Ch
  004887f4  c3                    ret

LAB_004887f5:
  004887f5  3df4010000            cmp       eax,1F4h
  004887fa  0f8dc4000000          jge       near 004888C4h
  00488800  e83be90b00            call      00547140h
  00488805  99                    cdq
  00488806  b964000000            mov       ecx,64h
  0048880b  f7f9                  idiv      ecx
  0048880d  83fa2d                cmp       edx,2Dh
  00488810  7d29                  jge       short 0048883Bh
  00488812  8b542440              mov       edx,[esp+40h]
  00488816  8b442410              mov       eax,[esp+10h]
  0048881a  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  00488821  8d14c500020000        lea       edx,[eax*8+200h]
  00488828  81e1ff0f0000          and       ecx,0FFFh
  0048882e  3bca                  cmp       ecx,edx
  00488830  0f8fde000000          jg        near 00488914h
  00488836  e9bb000000            jmp       004888F6h

LAB_0048883b:
  0048883b  83fa46                cmp       edx,46h
  0048883e  7d4e                  jge       short 0048888Eh
  00488840  c6464203              mov       byte [esi+42h],3
  00488844  e8f7e80b00            call      00547140h
  00488849  99                    cdq
  0048884a  b90a000000            mov       ecx,0Ah
  0048884f  f7f9                  idiv      ecx
  00488851  8b442410              mov       eax,[esp+10h]
  00488855  8b4c2414              mov       ecx,[esp+14h]
  00488859  d1f8                  sar       eax,1
  0048885b  8db980000000          lea       edi,[ecx+80h]
  00488861  c1e704                shl       edi,4
  00488864  02d0                  add       dl,al
  00488866  80c214                add       dl,14h
  00488869  885643                mov       [esi+43h],dl
  0048886c  e8cfe80b00            call      00547140h
  00488871  99                    cdq
  00488872  f7ff                  idiv      edi
  00488874  8b4c2440              mov       ecx,[esp+40h]
  00488878  d1ff                  sar       edi,1
  0048887a  2bd7                  sub       edx,edi
  0048887c  03d1                  add       edx,ecx
  0048887e  66035632              add       dx,[esi+32h]
  00488882  66895610              mov       [esi+10h],dx
  00488886  5f                    pop       edi
  00488887  5e                    pop       esi
  00488888  5d                    pop       ebp
  00488889  5b                    pop       ebx
  0048888a  83c42c                add       esp,2Ch
  0048888d  c3                    ret

LAB_0048888e:
  0048888e  83fa5a                cmp       edx,5Ah
  00488891  0f8d92000000          jge       near 00488929h

LAB_00488897:
  00488897  c6464202              mov       byte [esi+42h],2
  0048889b  e8a0e80b00            call      00547140h
  004888a0  99                    cdq
  004888a1  b90a000000            mov       ecx,0Ah
  004888a6  66c746106400          mov       word [esi+10h],64h
  004888ac  f7f9                  idiv      ecx
  004888ae  8b442410              mov       eax,[esp+10h]
  004888b2  d1f8                  sar       eax,1
  004888b4  02d0                  add       dl,al
  004888b6  80c214                add       dl,14h
  004888b9  885643                mov       [esi+43h],dl
  004888bc  5f                    pop       edi
  004888bd  5e                    pop       esi
  004888be  5d                    pop       ebp
  004888bf  5b                    pop       ebx
  004888c0  83c42c                add       esp,2Ch
  004888c3  c3                    ret

LAB_004888c4:
  004888c4  e877e80b00            call      00547140h
  004888c9  99                    cdq
  004888ca  b964000000            mov       ecx,64h
  004888cf  f7f9                  idiv      ecx
  004888d1  83fa1e                cmp       edx,1Eh
  004888d4  7d4e                  jge       short 00488924h
  004888d6  8b542440              mov       edx,[esp+40h]
  004888da  8b442410              mov       eax,[esp+10h]
  004888de  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  004888e5  8d14c500020000        lea       edx,[eax*8+200h]
  004888ec  81e1ff0f0000          and       ecx,0FFFh
  004888f2  3bca                  cmp       ecx,edx
  004888f4  7f1e                  jg        short 00488914h

LAB_004888f6:
  004888f6  8b06                  mov       eax,[esi]
  004888f8  c7461804000000        mov       dword [esi+18h],4
  004888ff  0d04080001            or        eax,1000804h
  00488904  66c746100300          mov       word [esi+10h],3
  0048890a  8906                  mov       [esi],eax
  0048890c  5f                    pop       edi
  0048890d  5e                    pop       esi
  0048890e  5d                    pop       ebp
  0048890f  5b                    pop       ebx
  00488910  83c42c                add       esp,2Ch
  00488913  c3                    ret

LAB_00488914:
  00488914  c6464201              mov       byte [esi+42h],1
  00488918  c6464301              mov       byte [esi+43h],1
  0048891c  5f                    pop       edi
  0048891d  5e                    pop       esi
  0048891e  5d                    pop       ebp
  0048891f  5b                    pop       ebx
  00488920  83c42c                add       esp,2Ch
  00488923  c3                    ret

LAB_00488924:
  00488924  83fa3c                cmp       edx,3Ch
  00488927  7d27                  jge       short 00488950h

LAB_00488929:
  00488929  c6464200              mov       byte [esi+42h],0
  0048892d  e80ee80b00            call      00547140h
  00488932  99                    cdq
  00488933  b90a000000            mov       ecx,0Ah
  00488938  f7f9                  idiv      ecx
  0048893a  8b442410              mov       eax,[esp+10h]
  0048893e  d1f8                  sar       eax,1
  00488940  02d0                  add       dl,al
  00488942  80c214                add       dl,14h
  00488945  885643                mov       [esi+43h],dl
  00488948  5f                    pop       edi
  00488949  5e                    pop       esi
  0048894a  5d                    pop       ebp
  0048894b  5b                    pop       ebx
  0048894c  83c42c                add       esp,2Ch
  0048894f  c3                    ret

LAB_00488950:
  00488950  c6464203              mov       byte [esi+42h],3
  00488954  e8e7e70b00            call      00547140h
  00488959  99                    cdq
  0048895a  b90a000000            mov       ecx,0Ah
  0048895f  f7f9                  idiv      ecx
  00488961  8b442410              mov       eax,[esp+10h]
  00488965  d1f8                  sar       eax,1
  00488967  02d0                  add       dl,al
  00488969  80c214                add       dl,14h
  0048896c  885643                mov       [esi+43h],dl
  0048896f  e8cce70b00            call      00547140h
  00488974  66894610              mov       [esi+10h],ax

LAB_00488978:
  00488978  5f                    pop       edi
  00488979  5e                    pop       esi
  0048897a  5d                    pop       ebp
  0048897b  5b                    pop       ebx
  0048897c  83c42c                add       esp,2Ch
  0048897f  c3                    ret
  00488980  89834800a083          mov       [ebx-7C5FFFB8h],eax
  00488986  48                    dec       eax
  00488987  00f5                  add       ch,dh
  00488989  83480043              or        dword [eax],43h
  0048898d  844800                test      [eax],cl
  00488990  cc                    int3
  00488991  844800                test      [eax],cl
  00488994  038548005e85          add       eax,[ebp-7AA1FFB8h]
  0048899a  48                    dec       eax
  0048899b  00e6                  add       dh,ah
  0048899d  854800                test      [eax],ecx

39 cible(s) de saut interne : 0x004882c8, 0x0048831d, 0x00488327, 0x0048832f, 0x0048834a, 0x00488372, 0x004883bc, 0x00488404, 0x0048842c, 0x004884af, 0x004884dc, 0x004884ef, 0x00488513, 0x00488542, 0x00488552, 0x00488556, 0x0048856e, 0x00488581, 0x0048858d, 0x004885c1, 0x004885d1, 0x004885f2, 0x00488602, 0x0048866c, 0x0048867a, 0x0048867c, 0x00488680, 0x00488798, 0x004887f5, 0x0048883b, 0x0048888e, 0x00488897, 0x004888c4, 0x004888f6, 0x00488914, 0x00488924, 0x00488929, 0x00488950, 0x00488978
