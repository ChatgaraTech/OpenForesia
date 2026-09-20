C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469100  section .text  offset fichier 0x68500  896 octets

  00469100  83ec14                sub       esp,14h
  00469103  53                    push      ebx
  00469104  55                    push      ebp
  00469105  56                    push      esi
  00469106  8b742424              mov       esi,[esp+24h]
  0046910a  57                    push      edi
  0046910b  56                    push      esi
  0046910c  e8af7effff            call      00460FC0h
  00469111  8b4660                mov       eax,[esi+60h]
  00469114  8b4e64                mov       ecx,[esi+64h]
  00469117  8b561c                mov       edx,[esi+1Ch]
  0046911a  8b6e7c                mov       ebp,[esi+7Ch]
  0046911d  668bbe90000000        mov       di,[esi+90h]
  00469124  8944242c              mov       [esp+2Ch],eax
  00469128  8b4620                mov       eax,[esi+20h]
  0046912b  894c2414              mov       [esp+14h],ecx
  0046912f  8b4e24                mov       ecx,[esi+24h]
  00469132  895644                mov       [esi+44h],edx
  00469135  8b9680000000          mov       edx,[esi+80h]
  0046913b  894648                mov       [esi+48h],eax
  0046913e  8b8684000000          mov       eax,[esi+84h]
  00469144  894e4c                mov       [esi+4Ch],ecx
  00469147  8b8e88000000          mov       ecx,[esi+88h]
  0046914d  89542418              mov       [esp+18h],edx
  00469151  8b968c000000          mov       edx,[esi+8Ch]
  00469157  33db                  xor       ebx,ebx
  00469159  83c404                add       esp,4
  0046915c  66395e10              cmp       [esi+10h],bx
  00469160  89442418              mov       [esp+18h],eax
  00469164  894c241c              mov       [esp+1Ch],ecx
  00469168  89542420              mov       [esp+20h],edx
  0046916c  0f8594000000          jne       near 00469206h
  00469172  0fbf4614              movsx     eax,word [esi+14h]
  00469176  66c746100100          mov       word [esi+10h],1
  0046917c  89465c                mov       [esi+5Ch],eax
  0046917f  e8bcdf0d00            call      00547140h
  00469184  99                    cdq
  00469185  b917000000            mov       ecx,17h
  0046918a  f7f9                  idiv      ecx
  0046918c  8bda                  mov       ebx,edx
  0046918e  66035e5c              add       bx,[esi+5Ch]
  00469192  e8a9df0d00            call      00547140h
  00469197  99                    cdq
  00469198  33c2                  xor       eax,edx
  0046919a  2bc2                  sub       eax,edx
  0046919c  83e001                and       eax,1
  0046919f  33c2                  xor       eax,edx
  004691a1  2bc2                  sub       eax,edx
  004691a3  8d5400ff              lea       edx,[eax+eax-1]
  004691a7  0fafda                imul      ebx,edx
  004691aa  66895e12              mov       [esi+12h],bx
  004691ae  e88ddf0d00            call      00547140h
  004691b3  99                    cdq
  004691b4  b917000000            mov       ecx,17h
  004691b9  f7f9                  idiv      ecx
  004691bb  8bda                  mov       ebx,edx
  004691bd  66035e5c              add       bx,[esi+5Ch]
  004691c1  e87adf0d00            call      00547140h
  004691c6  99                    cdq
  004691c7  33c2                  xor       eax,edx
  004691c9  2bc2                  sub       eax,edx
  004691cb  83e001                and       eax,1
  004691ce  33c2                  xor       eax,edx
  004691d0  2bc2                  sub       eax,edx
  004691d2  8d5400ff              lea       edx,[eax+eax-1]
  004691d6  0fafda                imul      ebx,edx
  004691d9  66895e14              mov       [esi+14h],bx
  004691dd  e85edf0d00            call      00547140h
  004691e2  99                    cdq
  004691e3  33c2                  xor       eax,edx
  004691e5  33db                  xor       ebx,ebx
  004691e7  2bc2                  sub       eax,edx
  004691e9  66899e98000000        mov       [esi+98h],bx
  004691f0  83e001                and       eax,1
  004691f3  33c2                  xor       eax,edx
  004691f5  2bc2                  sub       eax,edx
  004691f7  8d4400ff              lea       eax,[eax+eax-1]
  004691fb  66894616              mov       [esi+16h],ax
  004691ff  8b06                  mov       eax,[esi]
  00469201  80cc20                or        ah,20h
  00469204  8906                  mov       [esi],eax

LAB_00469206:
  00469206  66ff8698000000        inc       word [esi+98h]
  0046920d  668b8698000000        mov       ax,[esi+98h]
  00469214  663d0900              cmp       ax,9
  00469218  7530                  jne       short 0046924Ah
  0046921a  66899e98000000        mov       [esi+98h],bx
  00469221  8b0dcc975e00          mov       ecx,[5E97CCh]
  00469227  53                    push      ebx
  00469228  53                    push      ebx
  00469229  8b91a8000000          mov       edx,[ecx+0A8h]
  0046922f  6800080000            push      800h
  00469234  6800040000            push      400h
  00469239  8b02                  mov       eax,[edx]
  0046923b  660fb6482d            movzx     cx,byte [eax+2Dh]
  00469240  51                    push      ecx
  00469241  56                    push      esi
  00469242  e839eeffff            call      00468080h
  00469247  83c418                add       esp,18h

LAB_0046924a:
  0046924a  0fbf4612              movsx     eax,word [esi+12h]
  0046924e  8b4e1c                mov       ecx,[esi+1Ch]
  00469251  8bd0                  mov       edx,eax
  00469253  c1e20c                shl       edx,0Ch
  00469256  03ca                  add       ecx,edx
  00469258  894e1c                mov       [esi+1Ch],ecx
  0046925b  0fbf4e14              movsx     ecx,word [esi+14h]
  0046925f  8bd1                  mov       edx,ecx
  00469261  c1e10c                shl       ecx,0Ch
  00469264  51                    push      ecx
  00469265  53                    push      ebx
  00469266  c1e00c                shl       eax,0Ch
  00469269  c1e20c                shl       edx,0Ch
  0046926c  015624                add       [esi+24h],edx
  0046926f  50                    push      eax
  00469270  56                    push      esi
  00469271  e80ab3feff            call      00454580h
  00469276  668b4616              mov       ax,[esi+16h]
  0046927a  83c410                add       esp,10h
  0046927d  6669c0f000            imul      ax,0F0h
  00469282  66014632              add       [esi+32h],ax
  00469286  668b4632              mov       ax,[esi+32h]
  0046928a  663d0010              cmp       ax,1000h
  0046928e  7e04                  jle       short 00469294h
  00469290  66895e32              mov       [esi+32h],bx

LAB_00469294:
  00469294  66395e32              cmp       [esi+32h],bx
  00469298  7d06                  jge       short 004692A0h
  0046929a  66c746320010          mov       word [esi+32h],1000h

LAB_004692a0:
  004692a0  56                    push      esi
  004692a1  e89aa60c00            call      00533940h
  004692a6  8b8690000000          mov       eax,[esi+90h]
  004692ac  83c404                add       esp,4
  004692af  a980080000            test      eax,880h
  004692b4  750c                  jne       short 004692C2h
  004692b6  8b4e7c                mov       ecx,[esi+7Ch]
  004692b9  38590f                cmp       [ecx+0Fh],bl
  004692bc  0f84fb000000          je        near 004693BDh

LAB_004692c2:
  004692c2  8b542428              mov       edx,[esp+28h]
  004692c6  8b442410              mov       eax,[esp+10h]
  004692ca  8b4e44                mov       ecx,[esi+44h]
  004692cd  895660                mov       [esi+60h],edx
  004692d0  8b564c                mov       edx,[esi+4Ch]
  004692d3  894664                mov       [esi+64h],eax
  004692d6  8b442414              mov       eax,[esp+14h]
  004692da  894e1c                mov       [esi+1Ch],ecx
  004692dd  8b4c2418              mov       ecx,[esp+18h]
  004692e1  895624                mov       [esi+24h],edx
  004692e4  8b54241c              mov       edx,[esp+1Ch]
  004692e8  898680000000          mov       [esi+80h],eax
  004692ee  8b442420              mov       eax,[esp+20h]
  004692f2  81e7ffff0000          and       edi,0FFFFh
  004692f8  896e7c                mov       [esi+7Ch],ebp
  004692fb  89be90000000          mov       [esi+90h],edi
  00469301  898e84000000          mov       [esi+84h],ecx
  00469307  899688000000          mov       [esi+88h],edx
  0046930d  89868c000000          mov       [esi+8Ch],eax
  00469313  e828de0d00            call      00547140h
  00469318  99                    cdq
  00469319  b917000000            mov       ecx,17h
  0046931e  f7f9                  idiv      ecx
  00469320  8bfa                  mov       edi,edx
  00469322  66037e5c              add       di,[esi+5Ch]
  00469326  e815de0d00            call      00547140h
  0046932b  99                    cdq
  0046932c  33c2                  xor       eax,edx
  0046932e  2bc2                  sub       eax,edx
  00469330  83e001                and       eax,1
  00469333  33c2                  xor       eax,edx
  00469335  2bc2                  sub       eax,edx
  00469337  8d5400ff              lea       edx,[eax+eax-1]
  0046933b  0faffa                imul      edi,edx
  0046933e  66897e12              mov       [esi+12h],di
  00469342  e8f9dd0d00            call      00547140h
  00469347  99                    cdq
  00469348  b917000000            mov       ecx,17h
  0046934d  f7f9                  idiv      ecx
  0046934f  8bfa                  mov       edi,edx
  00469351  66037e5c              add       di,[esi+5Ch]
  00469355  e8e6dd0d00            call      00547140h
  0046935a  99                    cdq
  0046935b  33c2                  xor       eax,edx
  0046935d  53                    push      ebx
  0046935e  2bc2                  sub       eax,edx
  00469360  53                    push      ebx
  00469361  83e001                and       eax,1
  00469364  6800100000            push      1000h
  00469369  33c2                  xor       eax,edx
  0046936b  6800040000            push      400h
  00469370  2bc2                  sub       eax,edx
  00469372  8d5400ff              lea       edx,[eax+eax-1]
  00469376  668b4616              mov       ax,[esi+16h]
  0046937a  0faffa                imul      edi,edx
  0046937d  66f7d8                neg       ax
  00469380  66897e14              mov       [esi+14h],di
  00469384  66894616              mov       [esi+16h],ax
  00469388  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046938e  8b91a8000000          mov       edx,[ecx+0A8h]
  00469394  8b02                  mov       eax,[edx]
  00469396  660fb64830            movzx     cx,byte [eax+30h]
  0046939b  51                    push      ecx
  0046939c  56                    push      esi
  0046939d  e8deecffff            call      00468080h
  004693a2  83c418                add       esp,18h
  004693a5  bf07000000            mov       edi,7

LAB_004693aa:
  004693aa  53                    push      ebx
  004693ab  53                    push      ebx
  004693ac  6aff                  push      0FFFFFFFFh
  004693ae  53                    push      ebx
  004693af  6a34                  push      34h
  004693b1  56                    push      esi
  004693b2  e859edffff            call      00468110h
  004693b7  83c418                add       esp,18h
  004693ba  4f                    dec       edi
  004693bb  75ed                  jne       short 004693AAh

LAB_004693bd:
  004693bd  e87edd0d00            call      00547140h
  004693c2  99                    cdq
  004693c3  b907000000            mov       ecx,7
  004693c8  f7f9                  idiv      ecx
  004693ca  83fa01                cmp       edx,1
  004693cd  7510                  jne       short 004693DFh
  004693cf  53                    push      ebx
  004693d0  53                    push      ebx
  004693d1  6aff                  push      0FFFFFFFFh
  004693d3  53                    push      ebx
  004693d4  6a35                  push      35h
  004693d6  56                    push      esi
  004693d7  e834edffff            call      00468110h
  004693dc  83c418                add       esp,18h

LAB_004693df:
  004693df  885e09                mov       [esi+9],bl
  004693e2  33ed                  xor       ebp,ebp
  004693e4  bfc0935e00            mov       edi,5E93C0h

LAB_004693e9:
  004693e9  8b07                  mov       eax,[edi]
  004693eb  a900000080            test      eax,80000000h
  004693f0  745d                  je        short 0046944Fh
  004693f2  a900000050            test      eax,50000000h
  004693f7  7556                  jne       short 0046944Fh
  004693f9  8b471c                mov       eax,[edi+1Ch]
  004693fc  8b561c                mov       edx,[esi+1Ch]
  004693ff  8b4f24                mov       ecx,[edi+24h]
  00469402  c1f80c                sar       eax,0Ch
  00469405  c1fa0c                sar       edx,0Ch
  00469408  2bc2                  sub       eax,edx
  0046940a  8b5624                mov       edx,[esi+24h]
  0046940d  c1f90c                sar       ecx,0Ch
  00469410  c1fa0c                sar       edx,0Ch
  00469413  2bca                  sub       ecx,edx
  00469415  8bd0                  mov       edx,eax
  00469417  0fafd0                imul      edx,eax
  0046941a  8bc1                  mov       eax,ecx
  0046941c  0fafc1                imul      eax,ecx
  0046941f  03d0                  add       edx,eax
  00469421  52                    push      edx
  00469422  e8e9c50d00            call      00545A10h
  00469427  8b4f20                mov       ecx,[edi+20h]
  0046942a  8b5620                mov       edx,[esi+20h]
  0046942d  c1f90c                sar       ecx,0Ch
  00469430  c1fa0c                sar       edx,0Ch
  00469433  2bca                  sub       ecx,edx
  00469435  83c146                add       ecx,46h
  00469438  7902                  jns       short 0046943Ch
  0046943a  f7d9                  neg       ecx

LAB_0046943c:
  0046943c  83f83c                cmp       eax,3Ch
  0046943f  7d0e                  jge       short 0046944Fh
  00469441  83f950                cmp       ecx,50h
  00469444  7d09                  jge       short 0046944Fh
  00469446  b001                  mov       al,1
  00469448  8bcd                  mov       ecx,ebp
  0046944a  d2e0                  shl       al,cl
  0046944c  084609                or        [esi+9],al

LAB_0046944f:
  0046944f  81c79c000000          add       edi,9Ch
  00469455  45                    inc       ebp
  00469456  81ff94955e00          cmp       edi,5E9594h
  0046945c  7e8b                  jle       short 004693E9h
  0046945e  53                    push      ebx
  0046945f  53                    push      ebx
  00469460  53                    push      ebx
  00469461  6a0a                  push      0Ah
  00469463  56                    push      esi
  00469464  e8d745ffff            call      0045DA40h
  00469469  83c414                add       esp,14h
  0046946c  5f                    pop       edi
  0046946d  5e                    pop       esi
  0046946e  5d                    pop       ebp
  0046946f  5b                    pop       ebx
  00469470  83c414                add       esp,14h
  00469473  c3                    ret
  00469474  90                    nop
  00469475  90                    nop
  00469476  90                    nop
  00469477  90                    nop
  00469478  90                    nop
  00469479  90                    nop
  0046947a  90                    nop
  0046947b  90                    nop
  0046947c  90                    nop
  0046947d  90                    nop
  0046947e  90                    nop
  0046947f  90                    nop

11 cible(s) de saut interne : 0x00469206, 0x0046924a, 0x00469294, 0x004692a0, 0x004692c2, 0x004693aa, 0x004693bd, 0x004693df, 0x004693e9, 0x0046943c, 0x0046944f
