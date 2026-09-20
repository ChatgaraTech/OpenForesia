C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469f60  section .text  offset fichier 0x69360  1168 octets

  00469f60  8b0d28185700          mov       ecx,[571828h]
  00469f66  83ec1c                sub       esp,1Ch
  00469f69  b800040000            mov       eax,400h
  00469f6e  53                    push      ebx
  00469f6f  55                    push      ebp
  00469f70  56                    push      esi
  00469f71  8b74242c              mov       esi,[esp+2Ch]
  00469f75  85c8                  test      eax,ecx
  00469f77  57                    push      edi
  00469f78  7507                  jne       short 00469F81h
  00469f7a  8b0e                  mov       ecx,[esi]
  00469f7c  80cd20                or        ch,20h
  00469f7f  890e                  mov       [esi],ecx

LAB_00469f81:
  00469f81  850528185700          test      [571828h],eax
  00469f87  7407                  je        short 00469F90h
  00469f89  8b06                  mov       eax,[esi]
  00469f8b  80e4df                and       ah,0DFh
  00469f8e  8906                  mov       [esi],eax

LAB_00469f90:
  00469f90  a128185700            mov       eax,[571828h]
  00469f95  f6c408                test      ah,8
  00469f98  750f                  jne       short 00469FA9h
  00469f9a  8b06                  mov       eax,[esi]
  00469f9c  80cc40                or        ah,40h
  00469f9f  8906                  mov       [esi],eax
  00469fa1  5f                    pop       edi
  00469fa2  5e                    pop       esi
  00469fa3  5d                    pop       ebp
  00469fa4  5b                    pop       ebx
  00469fa5  83c41c                add       esp,1Ch
  00469fa8  c3                    ret

LAB_00469fa9:
  00469fa9  8b2e                  mov       ebp,[esi]
  00469fab  b83c000000            mov       eax,3Ch
  00469fb0  81e5ff9fffff          and       ebp,0FFFF9FFFh
  00469fb6  66ff8698000000        inc       word [esi+98h]
  00469fbd  668b8e98000000        mov       cx,[esi+98h]
  00469fc4  892e                  mov       [esi],ebp
  00469fc6  2b05d0175700          sub       eax,[5717D0h]
  00469fcc  83f803                cmp       eax,3
  00469fcf  7d05                  jge       short 00469FD6h
  00469fd1  b803000000            mov       eax,3

LAB_00469fd6:
  00469fd6  0fbfc9                movsx     ecx,cx
  00469fd9  3bc8                  cmp       ecx,eax
  00469fdb  7e4d                  jle       short 0046A02Ah
  00469fdd  a128185700            mov       eax,[571828h]
  00469fe2  f6c402                test      ah,2
  00469fe5  7543                  jne       short 0046A02Ah
  00469fe7  33ff                  xor       edi,edi
  00469fe9  57                    push      edi
  00469fea  57                    push      edi
  00469feb  6800120000            push      1200h
  00469ff0  6689be98000000        mov       [esi+98h],di
  00469ff7  e844d10d00            call      00547140h
  00469ffc  99                    cdq
  00469ffd  b964000000            mov       ecx,64h
  0046a002  f7f9                  idiv      ecx
  0046a004  81c2ce020000          add       edx,2CEh
  0046a00a  52                    push      edx
  0046a00b  8b15cc975e00          mov       edx,[5E97CCh]
  0046a011  8b82a8000000          mov       eax,[edx+0A8h]
  0046a017  8b08                  mov       ecx,[eax]
  0046a019  660fb65114            movzx     dx,byte [ecx+14h]
  0046a01e  52                    push      edx
  0046a01f  56                    push      esi
  0046a020  e85be0ffff            call      00468080h
  0046a025  83c418                add       esp,18h
  0046a028  eb02                  jmp       short 0046A02Ch

LAB_0046a02a:
  0046a02a  33ff                  xor       edi,edi

LAB_0046a02c:
  0046a02c  83c8ff                or        eax,0FFFFFFFFh
  0046a02f  66397e10              cmp       [esi+10h],di
  0046a033  751c                  jne       short 0046A051h
  0046a035  66894612              mov       [esi+12h],ax
  0046a039  c705d017570003000000  mov       dword [5717D0h],3
  0046a043  66897e16              mov       [esi+16h],di
  0046a047  c705ec17570001000000  mov       dword [5717ECh],1

LAB_0046a051:
  0046a051  8b4e7c                mov       ecx,[esi+7Ch]
  0046a054  8b5660                mov       edx,[esi+60h]
  0046a057  894c2410              mov       [esp+10h],ecx
  0046a05b  8b4e64                mov       ecx,[esi+64h]
  0046a05e  89542414              mov       [esp+14h],edx
  0046a062  8b561c                mov       edx,[esi+1Ch]
  0046a065  66ff4610              inc       word [esi+10h]
  0046a069  894c2418              mov       [esp+18h],ecx
  0046a06d  8b4e20                mov       ecx,[esi+20h]
  0046a070  895644                mov       [esi+44h],edx
  0046a073  8b5624                mov       edx,[esi+24h]
  0046a076  66817e103075          cmp       word [esi+10h],7530h
  0046a07c  668bae90000000        mov       bp,[esi+90h]
  0046a083  894e48                mov       [esi+48h],ecx
  0046a086  8b8e80000000          mov       ecx,[esi+80h]
  0046a08c  89564c                mov       [esi+4Ch],edx
  0046a08f  8b9684000000          mov       edx,[esi+84h]
  0046a095  894c241c              mov       [esp+1Ch],ecx
  0046a099  8b8e88000000          mov       ecx,[esi+88h]
  0046a09f  89542420              mov       [esp+20h],edx
  0046a0a3  8b968c000000          mov       edx,[esi+8Ch]
  0046a0a9  894c2424              mov       [esp+24h],ecx
  0046a0ad  89542428              mov       [esp+28h],edx
  0046a0b1  7c06                  jl        short 0046A0B9h
  0046a0b3  66c74610204e          mov       word [esi+10h],4E20h

LAB_0046a0b9:
  0046a0b9  66394612              cmp       [esi+12h],ax
  0046a0bd  756a                  jne       short 0046A129h
  0046a0bf  6800000040            push      40000000h
  0046a0c4  6a10                  push      10h
  0046a0c6  56                    push      esi
  0046a0c7  e834af0c00            call      00535000h
  0046a0cc  83c40c                add       esp,0Ch
  0046a0cf  8bd8                  mov       ebx,eax
  0046a0d1  e89accffff            call      00466D70h
  0046a0d6  0fbfc0                movsx     eax,ax
  0046a0d9  3bc3                  cmp       eax,ebx
  0046a0db  754c                  jne       short 0046A129h
  0046a0dd  668b4e12              mov       cx,[esi+12h]
  0046a0e1  66c746100100          mov       word [esi+10h],1
  0046a0e7  66f7d9                neg       cx
  0046a0ea  66894e12              mov       [esi+12h],cx
  0046a0ee  c705d017570005000000  mov       dword [5717D0h],5
  0046a0f8  893d24185700          mov       [571824h],edi
  0046a0fe  66897e16              mov       [esi+16h],di
  0046a102  893de0175700          mov       [5717E0h],edi
  0046a108  66897e14              mov       [esi+14h],di
  0046a10c  8b15cc975e00          mov       edx,[5E97CCh]
  0046a112  57                    push      edi
  0046a113  57                    push      edi
  0046a114  52                    push      edx
  0046a115  893d1c185700          mov       [57181Ch],edi
  0046a11b  893d20185700          mov       [571820h],edi
  0046a121  e8aa9c0a00            call      00513DD0h
  0046a126  83c40c                add       esp,0Ch

LAB_0046a129:
  0046a129  bb01000000            mov       ebx,1
  0046a12e  66395e12              cmp       [esi+12h],bx
  0046a132  0f85ad020000          jne       near 0046A3E5h
  0046a138  a1d0175700            mov       eax,[5717D0h]
  0046a13d  8b0d24185700          mov       ecx,[571824h]
  0046a143  57                    push      edi
  0046a144  03c8                  add       ecx,eax
  0046a146  57                    push      edi
  0046a147  56                    push      esi
  0046a148  890d24185700          mov       [571824h],ecx
  0046a14e  e89d020000            call      0046A3F0h
  0046a153  83c40c                add       esp,0Ch
  0046a156  57                    push      edi
  0046a157  57                    push      edi
  0046a158  56                    push      esi
  0046a159  e842040000            call      0046A5A0h
  0046a15e  83c40c                add       esp,0Ch
  0046a161  66395e16              cmp       [esi+16h],bx
  0046a165  7530                  jne       short 0046A197h
  0046a167  0fbf4e10              movsx     ecx,word [esi+10h]
  0046a16b  8bc1                  mov       eax,ecx
  0046a16d  99                    cdq
  0046a16e  83e203                and       edx,3
  0046a171  03c2                  add       eax,edx
  0046a173  c1f802                sar       eax,2
  0046a176  c1e002                shl       eax,2
  0046a179  3bc8                  cmp       ecx,eax
  0046a17b  751a                  jne       short 0046A197h
  0046a17d  a1d0175700            mov       eax,[5717D0h]
  0046a182  48                    dec       eax
  0046a183  83f805                cmp       eax,5
  0046a186  a3d0175700            mov       [5717D0h],eax
  0046a18b  7f0a                  jg        short 0046A197h
  0046a18d  c705d017570005000000  mov       dword [5717D0h],5

LAB_0046a197:
  0046a197  0fbf4e10              movsx     ecx,word [esi+10h]
  0046a19b  b856555555            mov       eax,55555556h
  0046a1a0  f7e9                  imul      ecx
  0046a1a2  8bc2                  mov       eax,edx
  0046a1a4  c1e81f                shr       eax,1Fh
  0046a1a7  03d0                  add       edx,eax
  0046a1a9  8d1452                lea       edx,[edx+edx*2]
  0046a1ac  3bca                  cmp       ecx,edx
  0046a1ae  751a                  jne       short 0046A1CAh
  0046a1b0  a1d0175700            mov       eax,[5717D0h]
  0046a1b5  40                    inc       eax
  0046a1b6  83f828                cmp       eax,28h
  0046a1b9  a3d0175700            mov       [5717D0h],eax
  0046a1be  7c0a                  jl        short 0046A1CAh
  0046a1c0  c705d017570028000000  mov       dword [5717D0h],28h

LAB_0046a1ca:
  0046a1ca  0fbf4632              movsx     eax,word [esi+32h]
  0046a1ce  50                    push      eax
  0046a1cf  e848b80d00            call      00545A1Ch
  0046a1d4  0fbf4e32              movsx     ecx,word [esi+32h]
  0046a1d8  8bf8                  mov       edi,eax
  0046a1da  51                    push      ecx
  0046a1db  0faf3dd0175700        imul      edi,[5717D0h]
  0046a1e2  e82fb80d00            call      00545A16h
  0046a1e7  0faf05d0175700        imul      eax,[5717D0h]
  0046a1ee  8b5624                mov       edx,[esi+24h]
  0046a1f1  8b4e1c                mov       ecx,[esi+1Ch]
  0046a1f4  03d7                  add       edx,edi
  0046a1f6  8b7e20                mov       edi,[esi+20h]
  0046a1f9  895624                mov       [esi+24h],edx
  0046a1fc  8b567c                mov       edx,[esi+7Ch]
  0046a1ff  2bc8                  sub       ecx,eax
  0046a201  33c0                  xor       eax,eax
  0046a203  894e1c                mov       [esi+1Ch],ecx
  0046a206  8a420e                mov       al,[edx+0Eh]
  0046a209  8d9700800c00          lea       edx,[edi+0C8000h]
  0046a20f  56                    push      esi
  0046a210  668b8c8680000000      mov       cx,[esi+eax*4+80h]
  0046a218  895620                mov       [esi+20h],edx
  0046a21b  66894c2434            mov       [esp+34h],cx
  0046a220  e81b970c00            call      00533940h
  0046a225  8b4660                mov       eax,[esi+60h]
  0046a228  83c404                add       esp,4
  0046a22b  894620                mov       [esi+20h],eax
  0046a22e  a1d0175700            mov       eax,[5717D0h]
  0046a233  a9fcffffff            test      eax,0FFFFFFFCh
  0046a238  7405                  je        short 0046A23Fh
  0046a23a  c1f802                sar       eax,2
  0046a23d  8bd8                  mov       ebx,eax

LAB_0046a23f:
  0046a23f  6a00                  push      0
  0046a241  6a00                  push      0
  0046a243  68e7030000            push      3E7h
  0046a248  e8f3ce0d00            call      00547140h
  0046a24d  99                    cdq
  0046a24e  f7fb                  idiv      ebx
  0046a250  52                    push      edx
  0046a251  68e7030000            push      3E7h
  0046a256  56                    push      esi
  0046a257  e8041cfbff            call      0041BE60h
  0046a25c  83c418                add       esp,18h
  0046a25f  e8dcce0d00            call      00547140h
  0046a264  99                    cdq
  0046a265  b91e000000            mov       ecx,1Eh
  0046a26a  f7f9                  idiv      ecx
  0046a26c  83fa01                cmp       edx,1
  0046a26f  751c                  jne       short 0046A28Dh
  0046a271  e8cace0d00            call      00547140h
  0046a276  99                    cdq
  0046a277  33c2                  xor       eax,edx
  0046a279  2bc2                  sub       eax,edx
  0046a27b  83e003                and       eax,3
  0046a27e  33c2                  xor       eax,edx
  0046a280  2bc2                  sub       eax,edx
  0046a282  40                    inc       eax
  0046a283  50                    push      eax
  0046a284  56                    push      esi
  0046a285  e8a6030000            call      0046A630h
  0046a28a  83c408                add       esp,8

LAB_0046a28d:
  0046a28d  c1ff0c                sar       edi,0Ch
  0046a290  8bdf                  mov       ebx,edi
  0046a292  8b7e20                mov       edi,[esi+20h]
  0046a295  c1ff0c                sar       edi,0Ch
  0046a298  3bdf                  cmp       ebx,edi
  0046a29a  7d12                  jge       short 0046A2AEh
  0046a29c  6800010000            push      100h
  0046a2a1  6a01                  push      1
  0046a2a3  56                    push      esi
  0046a2a4  e8f7020000            call      0046A5A0h
  0046a2a9  83c40c                add       esp,0Ch
  0046a2ac  3bdf                  cmp       ebx,edi

LAB_0046a2ae:
  0046a2ae  7e22                  jle       short 0046A2D2h
  0046a2b0  668b4616              mov       ax,[esi+16h]
  0046a2b4  663d0400              cmp       ax,4
  0046a2b8  7416                  je        short 0046A2D0h
  0046a2ba  663d0200              cmp       ax,2
  0046a2be  7410                  je        short 0046A2D0h
  0046a2c0  6800ffffff            push      0FFFFFF00h
  0046a2c5  6a01                  push      1
  0046a2c7  56                    push      esi
  0046a2c8  e8d3020000            call      0046A5A0h
  0046a2cd  83c40c                add       esp,0Ch

LAB_0046a2d0:
  0046a2d0  3bdf                  cmp       ebx,edi

LAB_0046a2d2:
  0046a2d2  750d                  jne       short 0046A2E1h
  0046a2d4  6a00                  push      0
  0046a2d6  6a01                  push      1
  0046a2d8  56                    push      esi
  0046a2d9  e8c2020000            call      0046A5A0h
  0046a2de  83c40c                add       esp,0Ch

LAB_0046a2e1:
  0046a2e1  8b567c                mov       edx,[esi+7Ch]
  0046a2e4  668b4c2430            mov       cx,[esp+30h]
  0046a2e9  33c0                  xor       eax,eax
  0046a2eb  8a420e                mov       al,[edx+0Eh]
  0046a2ee  66398c8680000000      cmp       [esi+eax*4+80h],cx
  0046a2f6  7424                  je        short 0046A31Ch
  0046a2f8  66ff4616              inc       word [esi+16h]
  0046a2fc  668b4616              mov       ax,[esi+16h]
  0046a300  663d0400              cmp       ax,4
  0046a304  7406                  je        short 0046A30Ch
  0046a306  663d0200              cmp       ax,2
  0046a30a  7510                  jne       short 0046A31Ch

LAB_0046a30c:
  0046a30c  6800feffff            push      0FFFFFE00h
  0046a311  6a01                  push      1
  0046a313  56                    push      esi
  0046a314  e887020000            call      0046A5A0h
  0046a319  83c40c                add       esp,0Ch

LAB_0046a31c:
  0046a31c  8b8690000000          mov       eax,[esi+90h]
  0046a322  f6c408                test      ah,8
  0046a325  7455                  je        short 0046A37Ch
  0046a327  8b542410              mov       edx,[esp+10h]
  0046a32b  8b442414              mov       eax,[esp+14h]
  0046a32f  8b4c2418              mov       ecx,[esp+18h]
  0046a333  89567c                mov       [esi+7Ch],edx
  0046a336  8b5644                mov       edx,[esi+44h]
  0046a339  894660                mov       [esi+60h],eax
  0046a33c  8b464c                mov       eax,[esi+4Ch]
  0046a33f  894e64                mov       [esi+64h],ecx
  0046a342  8b4c241c              mov       ecx,[esp+1Ch]
  0046a346  89561c                mov       [esi+1Ch],edx
  0046a349  8b542420              mov       edx,[esp+20h]
  0046a34d  894624                mov       [esi+24h],eax
  0046a350  8b442424              mov       eax,[esp+24h]
  0046a354  898e80000000          mov       [esi+80h],ecx
  0046a35a  8b4c2428              mov       ecx,[esp+28h]
  0046a35e  81e5ffff0000          and       ebp,0FFFFh
  0046a364  89ae90000000          mov       [esi+90h],ebp
  0046a36a  899684000000          mov       [esi+84h],edx
  0046a370  898688000000          mov       [esi+88h],eax
  0046a376  898e8c000000          mov       [esi+8Ch],ecx

LAB_0046a37c:
  0046a37c  813d2418570004100000  cmp       dword [571824h],1004h
  0046a386  725d                  jb        short 0046A3E5h
  0046a388  66c746120200          mov       word [esi+12h],2
  0046a38e  8b15cc975e00          mov       edx,[5E97CCh]
  0046a394  6a01                  push      1
  0046a396  68c0935e00            push      5E93C0h
  0046a39b  52                    push      edx
  0046a39c  e82f9a0a00            call      00513DD0h
  0046a3a1  a1cc975e00            mov       eax,[5E97CCh]
  0046a3a6  83c40c                add       esp,0Ch
  0046a3a9  6a01                  push      1
  0046a3ab  685c945e00            push      5E945Ch
  0046a3b0  50                    push      eax
  0046a3b1  e81a9a0a00            call      00513DD0h
  0046a3b6  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046a3bc  83c40c                add       esp,0Ch
  0046a3bf  6a01                  push      1
  0046a3c1  68f8945e00            push      5E94F8h
  0046a3c6  51                    push      ecx
  0046a3c7  e8049a0a00            call      00513DD0h
  0046a3cc  8b15cc975e00          mov       edx,[5E97CCh]
  0046a3d2  83c40c                add       esp,0Ch
  0046a3d5  6a01                  push      1
  0046a3d7  6894955e00            push      5E9594h
  0046a3dc  52                    push      edx
  0046a3dd  e8ee990a00            call      00513DD0h
  0046a3e2  83c40c                add       esp,0Ch

LAB_0046a3e5:
  0046a3e5  5f                    pop       edi
  0046a3e6  5e                    pop       esi
  0046a3e7  5d                    pop       ebp
  0046a3e8  5b                    pop       ebx
  0046a3e9  83c41c                add       esp,1Ch
  0046a3ec  c3                    ret
  0046a3ed  90                    nop
  0046a3ee  90                    nop
  0046a3ef  90                    nop

21 cible(s) de saut interne : 0x00469f81, 0x00469f90, 0x00469fa9, 0x00469fd6, 0x0046a02a, 0x0046a02c, 0x0046a051, 0x0046a0b9, 0x0046a129, 0x0046a197, 0x0046a1ca, 0x0046a23f, 0x0046a28d, 0x0046a2ae, 0x0046a2d0, 0x0046a2d2, 0x0046a2e1, 0x0046a30c, 0x0046a31c, 0x0046a37c, 0x0046a3e5
