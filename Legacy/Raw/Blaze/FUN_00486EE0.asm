C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00486ee0  section .text  offset fichier 0x862e0  1000 octets

  00486ee0  83ec0c                sub       esp,0Ch
  00486ee3  53                    push      ebx
  00486ee4  55                    push      ebp
  00486ee5  56                    push      esi
  00486ee6  8b74241c              mov       esi,[esp+1Ch]
  00486eea  57                    push      edi
  00486eeb  8b06                  mov       eax,[esi]
  00486eed  f6c440                test      ah,40h
  00486ef0  0f8553030000          jne       near 00487249h
  00486ef6  a900000008            test      eax,8000000h
  00486efb  7509                  jne       short 00486F06h
  00486efd  56                    push      esi
  00486efe  e89d460500            call      004DB5A0h
  00486f03  83c404                add       esp,4

LAB_00486f06:
  00486f06  33c0                  xor       eax,eax
  00486f08  8a4609                mov       al,[esi+9]
  00486f0b  89442418              mov       [esp+18h],eax
  00486f0f  a120905e00            mov       eax,[5E9020h]
  00486f14  85c0                  test      eax,eax
  00486f16  0f8463010000          je        near 0048707Fh
  00486f1c  bbe0935e00            mov       ebx,5E93E0h
  00486f21  89442420              mov       [esp+20h],eax

LAB_00486f25:
  00486f25  8b43e0                mov       eax,[ebx-20h]
  00486f28  a900000080            test      eax,80000000h
  00486f2d  0f8424010000          je        near 00487057h
  00486f33  a907000150            test      eax,50010007h
  00486f38  0f8519010000          jne       near 00487057h
  00486f3e  66837bf200            cmp       word [ebx-0Eh],0
  00486f43  0f850e010000          jne       near 00487057h
  00486f49  8b7e1c                mov       edi,[esi+1Ch]
  00486f4c  8b6bfc                mov       ebp,[ebx-4]
  00486f4f  8b5304                mov       edx,[ebx+4]
  00486f52  8b4620                mov       eax,[esi+20h]
  00486f55  8b0b                  mov       ecx,[ebx]
  00486f57  2bfd                  sub       edi,ebp
  00486f59  8b6e24                mov       ebp,[esi+24h]
  00486f5c  2bc1                  sub       eax,ecx
  00486f5e  2bea                  sub       ebp,edx
  00486f60  c1ff0c                sar       edi,0Ch
  00486f63  c1fd0c                sar       ebp,0Ch
  00486f66  8bcf                  mov       ecx,edi
  00486f68  8bd5                  mov       edx,ebp
  00486f6a  0fafcf                imul      ecx,edi
  00486f6d  0fafd5                imul      edx,ebp
  00486f70  03ca                  add       ecx,edx
  00486f72  c1f80c                sar       eax,0Ch
  00486f75  51                    push      ecx
  00486f76  89442418              mov       [esp+18h],eax
  00486f7a  e891ea0b00            call      00545A10h
  00486f7f  55                    push      ebp
  00486f80  57                    push      edi
  00486f81  89442418              mov       [esp+18h],eax
  00486f85  e898ea0b00            call      00545A22h
  00486f8a  668b4b12              mov       cx,[ebx+12h]
  00486f8e  2bc1                  sub       eax,ecx
  00486f90  8b4c2410              mov       ecx,[esp+10h]
  00486f94  25ff0f0000            and       eax,0FFFh
  00486f99  83f93c                cmp       ecx,3Ch
  00486f9c  0f8ca0000000          jl        near 00487042h
  00486fa2  83f978                cmp       ecx,78h
  00486fa5  0f8f97000000          jg        near 00487042h
  00486fab  8b542414              mov       edx,[esp+14h]
  00486faf  83c264                add       edx,64h
  00486fb2  81fac8000000          cmp       edx,0C8h
  00486fb8  0f8784000000          ja        near 00487042h
  00486fbe  0500020000            add       eax,200h
  00486fc3  25ff0f0000            and       eax,0FFFh
  00486fc8  3d00040000            cmp       eax,400h
  00486fcd  7f73                  jg        short 00487042h
  00486fcf  8b6b04                mov       ebp,[ebx+4]
  00486fd2  8b4624                mov       eax,[esi+24h]
  00486fd5  8b7bfc                mov       edi,[ebx-4]
  00486fd8  8b561c                mov       edx,[esi+1Ch]
  00486fdb  2be8                  sub       ebp,eax
  00486fdd  2bfa                  sub       edi,edx
  00486fdf  c1fd0c                sar       ebp,0Ch
  00486fe2  c1ff0c                sar       edi,0Ch
  00486fe5  55                    push      ebp
  00486fe6  57                    push      edi
  00486fe7  e836ea0b00            call      00545A22h
  00486fec  668b4e32              mov       cx,[esi+32h]
  00486ff0  2bc1                  sub       eax,ecx
  00486ff2  8a4e43                mov       cl,[esi+43h]
  00486ff5  25ff0f0000            and       eax,0FFFh
  00486ffa  84c9                  test      cl,cl
  00486ffc  89442414              mov       [esp+14h],eax
  00487000  740f                  je        short 00487011h
  00487002  55                    push      ebp
  00487003  57                    push      edi
  00487004  e819ea0b00            call      00545A22h
  00487009  66894632              mov       [esi+32h],ax
  0048700d  8b442414              mov       eax,[esp+14h]

LAB_00487011:
  00487011  0500020000            add       eax,200h
  00487016  25ff0f0000            and       eax,0FFFh
  0048701b  3d00040000            cmp       eax,400h
  00487020  7e07                  jle       short 00487029h
  00487022  8a4643                mov       al,[esi+43h]
  00487025  84c0                  test      al,al
  00487027  7441                  je        short 0048706Ah

LAB_00487029:
  00487029  8a4609                mov       al,[esi+9]
  0048702c  33c9                  xor       ecx,ecx
  0048702e  8a4be8                mov       cl,[ebx-18h]
  00487031  b201                  mov       dl,1
  00487033  d2e2                  shl       dl,cl
  00487035  0ac2                  or        al,dl
  00487037  884609                mov       [esi+9],al
  0048703a  8d43e0                lea       eax,[ebx-20h]
  0048703d  89466c                mov       [esi+6Ch],eax
  00487040  eb28                  jmp       short 0048706Ah

LAB_00487042:
  00487042  33c9                  xor       ecx,ecx
  00487044  8a4609                mov       al,[esi+9]
  00487047  8a4be8                mov       cl,[ebx-18h]
  0048704a  b201                  mov       dl,1
  0048704c  d2e2                  shl       dl,cl
  0048704e  f6d2                  not       dl
  00487050  22c2                  and       al,dl
  00487052  884609                mov       [esi+9],al
  00487055  eb13                  jmp       short 0048706Ah

LAB_00487057:
  00487057  33c9                  xor       ecx,ecx
  00487059  b001                  mov       al,1
  0048705b  8a4be8                mov       cl,[ebx-18h]
  0048705e  d2e0                  shl       al,cl
  00487060  8a4e09                mov       cl,[esi+9]
  00487063  f6d0                  not       al
  00487065  22c8                  and       cl,al
  00487067  884e09                mov       [esi+9],cl

LAB_0048706a:
  0048706a  8b442420              mov       eax,[esp+20h]
  0048706e  81c39c000000          add       ebx,9Ch
  00487074  48                    dec       eax
  00487075  89442420              mov       [esp+20h],eax
  00487079  0f85a6feffff          jne       near 00486F25h

LAB_0048707f:
  0048707f  8b442418              mov       eax,[esp+18h]
  00487083  33db                  xor       ebx,ebx
  00487085  3bc3                  cmp       eax,ebx
  00487087  751e                  jne       short 004870A7h
  00487089  8a4609                mov       al,[esi+9]
  0048708c  84c0                  test      al,al
  0048708e  7417                  je        short 004870A7h
  00487090  0fbf4e16              movsx     ecx,word [esi+16h]
  00487094  8b566c                mov       edx,[esi+6Ch]
  00487097  a1cc975e00            mov       eax,[5E97CCh]
  0048709c  51                    push      ecx
  0048709d  52                    push      edx
  0048709e  50                    push      eax
  0048709f  e82ccd0800            call      00513DD0h
  004870a4  83c40c                add       esp,0Ch

LAB_004870a7:
  004870a7  66395e10              cmp       [esi+10h],bx
  004870ab  7514                  jne       short 004870C1h
  004870ad  66395e12              cmp       [esi+12h],bx
  004870b1  750e                  jne       short 004870C1h
  004870b3  66395e14              cmp       [esi+14h],bx
  004870b7  7408                  je        short 004870C1h
  004870b9  66895e14              mov       [esi+14h],bx
  004870bd  c6464301              mov       byte [esi+43h],1

LAB_004870c1:
  004870c1  8a4643                mov       al,[esi+43h]
  004870c4  84c0                  test      al,al
  004870c6  7418                  je        short 004870E0h
  004870c8  66837e1801            cmp       word [esi+18h],1
  004870cd  7e1a                  jle       short 004870E9h
  004870cf  895e18                mov       [esi+18h],ebx
  004870d2  e869000c00            call      00547140h
  004870d7  2403                  and       al,3
  004870d9  0403                  add       al,3
  004870db  884640                mov       [esi+40h],al
  004870de  eb09                  jmp       short 004870E9h

LAB_004870e0:
  004870e0  66395e18              cmp       [esi+18h],bx
  004870e4  7403                  je        short 004870E9h
  004870e6  895e18                mov       [esi+18h],ebx

LAB_004870e9:
  004870e9  8a4643                mov       al,[esi+43h]
  004870ec  84c0                  test      al,al
  004870ee  0f8455010000          je        near 00487249h
  004870f4  0fbf4618              movsx     eax,word [esi+18h]
  004870f8  2bc3                  sub       eax,ebx
  004870fa  742b                  je        short 00487127h
  004870fc  48                    dec       eax
  004870fd  7531                  jne       short 00487130h
  004870ff  0fbf7e32              movsx     edi,word [esi+32h]
  00487103  57                    push      edi
  00487104  e813e90b00            call      00545A1Ch
  00487109  8b6e1c                mov       ebp,[esi+1Ch]
  0048710c  8d0c80                lea       ecx,[eax+eax*4]
  0048710f  03e9                  add       ebp,ecx
  00487111  57                    push      edi
  00487112  896e1c                mov       [esi+1Ch],ebp
  00487115  e8fce80b00            call      00545A16h
  0048711a  8b4e24                mov       ecx,[esi+24h]
  0048711d  8d1480                lea       edx,[eax+eax*4]
  00487120  03ca                  add       ecx,edx
  00487122  894e24                mov       [esi+24h],ecx
  00487125  eb06                  jmp       short 0048712Dh

LAB_00487127:
  00487127  66395e1a              cmp       [esi+1Ah],bx
  0048712b  7503                  jne       short 00487130h

LAB_0048712d:
  0048712d  fe4e40                dec       byte [esi+40h]

LAB_00487130:
  00487130  56                    push      esi
  00487131  e80ac80a00            call      00533940h
  00487136  8b4664                mov       eax,[esi+64h]
  00487139  83c404                add       esp,4
  0048713c  894620                mov       [esi+20h],eax
  0048713f  8b8690000000          mov       eax,[esi+90h]
  00487145  f6c408                test      ah,8
  00487148  7404                  je        short 0048714Eh
  0048714a  c6464000              mov       byte [esi+40h],0

LAB_0048714e:
  0048714e  8a4640                mov       al,[esi+40h]
  00487151  84c0                  test      al,al
  00487153  7408                  je        short 0048715Dh
  00487155  3cff                  cmp       al,0FFh
  00487157  0f85ec000000          jne       near 00487249h

LAB_0048715d:
  0048715d  6683761801            xor       word [esi+18h],1
  00487162  668b4618              mov       ax,[esi+18h]
  00487166  663bc3                cmp       ax,bx
  00487169  66895e1a              mov       [esi+1Ah],bx
  0048716d  0f84ca000000          je        near 0048723Dh
  00487173  6a27                  push      27h
  00487175  68409a5e00            push      5E9A40h
  0048717a  56                    push      esi
  0048717b  e860e10a00            call      005352E0h
  00487180  83c40c                add       esp,0Ch
  00487183  e8b8ff0b00            call      00547140h
  00487188  66a3729a5e00          mov       [5E9A72h],ax
  0048718e  e8adff0b00            call      00547140h
  00487193  0fbf0d729a5e00        movsx     ecx,word [5E9A72h]
  0048719a  241f                  and       al,1Fh
  0048719c  51                    push      ecx
  0048719d  0428                  add       al,28h
  0048719f  a2809a5e00            mov       [5E9A80h],al
  004871a4  e873e80b00            call      00545A1Ch
  004871a9  8b15809a5e00          mov       edx,[5E9A80h]
  004871af  81e2ff000000          and       edx,0FFh
  004871b5  0fafc2                imul      eax,edx
  004871b8  8b155c9a5e00          mov       edx,[5E9A5Ch]
  004871be  8bc8                  mov       ecx,eax
  004871c0  03ca                  add       ecx,edx
  004871c2  8d1481                lea       edx,[ecx+eax*4]
  004871c5  0fbf05729a5e00        movsx     eax,word [5E9A72h]
  004871cc  50                    push      eax
  004871cd  89155c9a5e00          mov       [5E9A5Ch],edx
  004871d3  e83ee80b00            call      00545A16h
  004871d8  8b0d809a5e00          mov       ecx,[5E9A80h]
  004871de  8b3d649a5e00          mov       edi,[5E9A64h]
  004871e4  81e1ff000000          and       ecx,0FFh
  004871ea  68409a5e00            push      5E9A40h
  004871ef  0fafc1                imul      eax,ecx
  004871f2  8bd0                  mov       edx,eax
  004871f4  03d7                  add       edx,edi
  004871f6  8d0482                lea       eax,[edx+eax*4]
  004871f9  a3649a5e00            mov       [5E9A64h],eax
  004871fe  e83dc70a00            call      00533940h
  00487203  a1d09a5e00            mov       eax,[5E9AD0h]
  00487208  83c404                add       esp,4
  0048720b  a982080000            test      eax,882h
  00487210  740f                  je        short 00487221h
  00487212  895e18                mov       [esi+18h],ebx
  00487215  c6464001              mov       byte [esi+40h],1
  00487219  5f                    pop       edi
  0048721a  5e                    pop       esi
  0048721b  5d                    pop       ebp
  0048721c  5b                    pop       ebx
  0048721d  83c40c                add       esp,0Ch
  00487220  c3                    ret

LAB_00487221:
  00487221  668b0d729a5e00        mov       cx,[5E9A72h]
  00487228  66894e32              mov       [esi+32h],cx
  0048722c  8a15809a5e00          mov       dl,[5E9A80h]
  00487232  885640                mov       [esi+40h],dl
  00487235  5f                    pop       edi
  00487236  5e                    pop       esi
  00487237  5d                    pop       ebp
  00487238  5b                    pop       ebx
  00487239  83c40c                add       esp,0Ch
  0048723c  c3                    ret

LAB_0048723d:
  0048723d  e8fefe0b00            call      00547140h
  00487242  2403                  and       al,3
  00487244  0403                  add       al,3
  00487246  884640                mov       [esi+40h],al

LAB_00487249:
  00487249  5f                    pop       edi
  0048724a  5e                    pop       esi
  0048724b  5d                    pop       ebp
  0048724c  5b                    pop       ebx
  0048724d  83c40c                add       esp,0Ch
  00487250  c3                    ret
  00487251  90                    nop
  00487252  90                    nop
  00487253  90                    nop
  00487254  90                    nop
  00487255  90                    nop
  00487256  90                    nop
  00487257  90                    nop
  00487258  90                    nop
  00487259  90                    nop
  0048725a  90                    nop
  0048725b  90                    nop
  0048725c  90                    nop
  0048725d  90                    nop
  0048725e  90                    nop
  0048725f  90                    nop
  00487260  83ec2c                sub       esp,2Ch
  00487263  53                    push      ebx
  00487264  55                    push      ebp
  00487265  56                    push      esi
  00487266  8b74243c              mov       esi,[esp+3Ch]
  0048726a  8d442424              lea       eax,[esp+24h]
  0048726e  57                    push      edi
  0048726f  8d4c242c              lea       ecx,[esp+2Ch]
  00487273  50                    push      eax
  00487274  8d542428              lea       edx,[esp+28h]
  00487278  51                    push      ecx
  00487279  33ed                  xor       ebp,ebp
  0048727b  52                    push      edx
  0048727c  56                    push      esi
  0048727d  66896c2440            mov       [esp+40h],bp
  00487282  66896c2442            mov       [esp+42h],bp
  00487287  66c74424443200        mov       word [esp+44h],32h
  0048728e  66c74424466400        mov       word [esp+46h],64h
  00487295  66896c2448            mov       [esp+48h],bp
  0048729a  66c744244a5a00        mov       word [esp+4Ah],5Ah
  004872a1  e8da160b00            call      00538980h
  004872a6  83c410                add       esp,10h
  004872a9  8d442420              lea       eax,[esp+20h]
  004872ad  8d4c241c              lea       ecx,[esp+1Ch]
  004872b1  8d542440              lea       edx,[esp+40h]
  004872b5  50                    push      eax
  004872b6  51                    push      ecx
  004872b7  8d442420              lea       eax,[esp+20h]
  004872bb  52                    push      edx
  004872bc  8d4c2420              lea       ecx,[esp+20h]
  004872c0  50                    push      eax
  004872c1  51                    push      ecx
  004872c2  56                    push      esi
  004872c3  e858ec0a00            call      00535F20h

20 cible(s) de saut interne : 0x00486f06, 0x00486f25, 0x00487011, 0x00487029, 0x00487042, 0x00487057, 0x0048706a, 0x0048707f, 0x004870a7, 0x004870c1, 0x004870e0, 0x004870e9, 0x00487127, 0x0048712d, 0x00487130, 0x0048714e, 0x0048715d, 0x00487221, 0x0048723d, 0x00487249
