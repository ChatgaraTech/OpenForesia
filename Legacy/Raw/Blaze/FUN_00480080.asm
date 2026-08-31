C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480080  section .text  offset fichier 0x7f480  1026 octets

  00480080  83ec20                sub       esp,20h
  00480083  53                    push      ebx
  00480084  55                    push      ebp
  00480085  56                    push      esi
  00480086  8b742430              mov       esi,[esp+30h]
  0048008a  57                    push      edi
  0048008b  33ff                  xor       edi,edi
  0048008d  8b06                  mov       eax,[esi]
  0048008f  66c744242400fc        mov       word [esp+24h],0FC00h
  00480096  a900000002            test      eax,2000000h
  0048009b  66c7442426b0ff        mov       word [esp+26h],0FFB0h
  004800a2  66c74424283c00        mov       word [esp+28h],3Ch
  004800a9  66c744242a6400        mov       word [esp+2Ah],64h
  004800b0  66897c242c            mov       [esp+2Ch],di
  004800b5  66c744242ea000        mov       word [esp+2Eh],0A0h
  004800bc  742d                  je        short 004800EBh
  004800be  668b4628              mov       ax,[esi+28h]
  004800c2  668bc8                mov       cx,ax
  004800c5  66c1f902              sar       cx,2
  004800c9  2bc1                  sub       eax,ecx
  004800cb  663d0001              cmp       ax,100h
  004800cf  66894628              mov       [esi+28h],ax
  004800d3  6689462a              mov       [esi+2Ah],ax
  004800d7  6689462c              mov       [esi+2Ch],ax
  004800db  0f8d95030000          jge       near 00480476h
  004800e1  893e                  mov       [esi],edi
  004800e3  5f                    pop       edi
  004800e4  5e                    pop       esi
  004800e5  5d                    pop       ebp
  004800e6  5b                    pop       ebx
  004800e7  83c420                add       esp,20h
  004800ea  c3                    ret

LAB_004800eb:
  004800eb  8b4e6c                mov       ecx,[esi+6Ch]
  004800ee  8b11                  mov       edx,[ecx]
  004800f0  81e200000002          and       edx,2000000h
  004800f6  0bd0                  or        edx,eax
  004800f8  66397e10              cmp       [esi+10h],di
  004800fc  8916                  mov       [esi],edx
  004800fe  7520                  jne       short 00480120h
  00480100  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00480107  897e28                mov       [esi+28h],edi
  0048010a  66897e2c              mov       [esi+2Ch],di
  0048010e  897e50                mov       [esi+50h],edi
  00480111  897e54                mov       [esi+54h],edi
  00480114  897e58                mov       [esi+58h],edi
  00480117  66c7869a0000000010    mov       word [esi+9Ah],1000h

LAB_00480120:
  00480120  668b4628              mov       ax,[esi+28h]
  00480124  bb00180000            mov       ebx,1800h
  00480129  0fbfd0                movsx     edx,ax
  0048012c  2bda                  sub       ebx,edx
  0048012e  c1fb02                sar       ebx,2
  00480131  03c3                  add       eax,ebx
  00480133  663de017              cmp       ax,17E0h
  00480137  66894628              mov       [esi+28h],ax
  0048013b  6689462a              mov       [esi+2Ah],ax
  0048013f  6689462c              mov       [esi+2Ch],ax
  00480143  7e06                  jle       short 0048014Bh
  00480145  8b01                  mov       eax,[ecx]
  00480147  0c02                  or        al,2
  00480149  8901                  mov       [ecx],eax

LAB_0048014b:
  0048014b  8b5e78                mov       ebx,[esi+78h]
  0048014e  3bdf                  cmp       ebx,edi
  00480150  741d                  je        short 0048016Fh
  00480152  f70300000050          test      dword [ebx],50000000h
  00480158  7507                  jne       short 00480161h
  0048015a  66837e1028            cmp       word [esi+10h],28h
  0048015f  7c20                  jl        short 00480181h

LAB_00480161:
  00480161  897b74                mov       [ebx+74h],edi
  00480164  897e78                mov       [esi+78h],edi
  00480167  66897e10              mov       [esi+10h],di
  0048016b  33db                  xor       ebx,ebx
  0048016d  eb12                  jmp       short 00480181h

LAB_0048016f:
  0048016f  8b4674                mov       eax,[esi+74h]
  00480172  6a01                  push      1
  00480174  6aff                  push      0FFFFFFFFh
  00480176  50                    push      eax
  00480177  e8344dffff            call      00474EB0h
  0048017c  83c40c                add       esp,0Ch
  0048017f  8bd8                  mov       ebx,eax

LAB_00480181:
  00480181  668146128000          add       word [esi+12h],80h
  00480187  668b4612              mov       ax,[esi+12h]
  0048018b  3bdf                  cmp       ebx,edi
  0048018d  7434                  je        short 004801C3h
  0048018f  895e78                mov       [esi+78h],ebx
  00480192  8b7b1c                mov       edi,[ebx+1Ch]
  00480195  897374                mov       [ebx+74h],esi
  00480198  8b461c                mov       eax,[esi+1Ch]
  0048019b  2bf8                  sub       edi,eax
  0048019d  53                    push      ebx
  0048019e  c1ff0c                sar       edi,0Ch
  004801a1  e81a8a0b00            call      00538BC0h
  004801a6  8b4b20                mov       ecx,[ebx+20h]
  004801a9  8be8                  mov       ebp,eax
  004801ab  8b4620                mov       eax,[esi+20h]
  004801ae  83e5fe                and       ebp,0FFFFFFFEh
  004801b1  8b5b24                mov       ebx,[ebx+24h]
  004801b4  83c404                add       esp,4
  004801b7  c1e50b                shl       ebp,0Bh
  004801ba  03e9                  add       ebp,ecx
  004801bc  2be8                  sub       ebp,eax
  004801be  c1fd0c                sar       ebp,0Ch
  004801c1  eb57                  jmp       short 0048021Ah

LAB_004801c3:
  004801c3  8b6e74                mov       ebp,[esi+74h]
  004801c6  55                    push      ebp
  004801c7  896c2414              mov       [esp+14h],ebp
  004801cb  0fbfd8                movsx     ebx,ax
  004801ce  e8ed890b00            call      00538BC0h
  004801d3  83c404                add       esp,4
  004801d6  89442434              mov       [esp+34h],eax
  004801da  53                    push      ebx
  004801db  e83c580c00            call      00545A1Ch
  004801e0  8bf8                  mov       edi,eax
  004801e2  8b442434              mov       eax,[esp+34h]
  004801e6  0faff8                imul      edi,eax
  004801e9  8b4d1c                mov       ecx,[ebp+1Ch]
  004801ec  8b561c                mov       edx,[esi+1Ch]
  004801ef  8b6d20                mov       ebp,[ebp+20h]
  004801f2  03f9                  add       edi,ecx
  004801f4  c1e00c                shl       eax,0Ch
  004801f7  2bfa                  sub       edi,edx
  004801f9  8b5620                mov       edx,[esi+20h]
  004801fc  03e8                  add       ebp,eax
  004801fe  53                    push      ebx
  004801ff  2bea                  sub       ebp,edx
  00480201  c1ff0c                sar       edi,0Ch
  00480204  c1fd0c                sar       ebp,0Ch
  00480207  e80a580c00            call      00545A16h
  0048020c  8bd8                  mov       ebx,eax
  0048020e  8b4c2410              mov       ecx,[esp+10h]
  00480212  0faf5c2434            imul      ebx,[esp+34h]
  00480217  035924                add       ebx,[ecx+24h]

LAB_0048021a:
  0048021a  8b4e24                mov       ecx,[esi+24h]
  0048021d  8bd7                  mov       edx,edi
  0048021f  2bd9                  sub       ebx,ecx
  00480221  0fafd7                imul      edx,edi
  00480224  c1fb0c                sar       ebx,0Ch
  00480227  8bc3                  mov       eax,ebx
  00480229  0fafc3                imul      eax,ebx
  0048022c  03d0                  add       edx,eax
  0048022e  52                    push      edx
  0048022f  e8dc570c00            call      00545A10h
  00480234  53                    push      ebx
  00480235  57                    push      edi
  00480236  8944243c              mov       [esp+3Ch],eax
  0048023a  e8e3570c00            call      00545A22h
  0048023f  8b4c2434              mov       ecx,[esp+34h]
  00480243  8bd8                  mov       ebx,eax
  00480245  51                    push      ecx
  00480246  55                    push      ebp
  00480247  e8d6570c00            call      00545A22h
  0048024c  8b565c                mov       edx,[esi+5Ch]
  0048024f  8bf8                  mov       edi,eax
  00480251  52                    push      edx
  00480252  57                    push      edi
  00480253  8d442420              lea       eax,[esp+20h]
  00480257  53                    push      ebx
  00480258  50                    push      eax
  00480259  e872280b00            call      00532AD0h
  0048025e  8b4c2428              mov       ecx,[esp+28h]
  00480262  8b5650                mov       edx,[esi+50h]
  00480265  8b44242c              mov       eax,[esp+2Ch]
  00480269  03d1                  add       edx,ecx
  0048026b  8b4e54                mov       ecx,[esi+54h]
  0048026e  668b6e34              mov       bp,[esi+34h]
  00480272  03c8                  add       ecx,eax
  00480274  8b4658                mov       eax,[esi+58h]
  00480277  894e54                mov       [esi+54h],ecx
  0048027a  8b4c2430              mov       ecx,[esp+30h]
  0048027e  03c1                  add       eax,ecx
  00480280  668b4e32              mov       cx,[esi+32h]
  00480284  894658                mov       [esi+58h],eax
  00480287  8a4610                mov       al,[esi+10h]
  0048028a  83e007                and       eax,7
  0048028d  2bd9                  sub       ebx,ecx
  0048028f  c1e007                shl       eax,7
  00480292  2bf8                  sub       edi,eax
  00480294  81c300040000          add       ebx,400h
  0048029a  2bfd                  sub       edi,ebp
  0048029c  81e3ff0f0000          and       ebx,0FFFh
  004802a2  81c700020000          add       edi,200h
  004802a8  83c410                add       esp,10h
  004802ab  81e7ff0f0000          and       edi,0FFFh
  004802b1  81fb00080000          cmp       ebx,800h
  004802b7  895650                mov       [esi+50h],edx
  004802ba  897c2434              mov       [esp+34h],edi
  004802be  7d05                  jge       short 004802C5h
  004802c0  c1fb02                sar       ebx,2
  004802c3  eb18                  jmp       short 004802DDh

LAB_004802c5:
  004802c5  bf00100000            mov       edi,1000h
  004802ca  2bfb                  sub       edi,ebx
  004802cc  c1ff02                sar       edi,2
  004802cf  f7df                  neg       edi
  004802d1  897c2410              mov       [esp+10h],edi
  004802d5  8b7c2434              mov       edi,[esp+34h]
  004802d9  8b5c2410              mov       ebx,[esp+10h]

LAB_004802dd:
  004802dd  03cb                  add       ecx,ebx
  004802df  81ff00080000          cmp       edi,800h
  004802e5  66894e32              mov       [esi+32h],cx
  004802e9  7d07                  jge       short 004802F2h
  004802eb  c1ff02                sar       edi,2
  004802ee  8bcf                  mov       ecx,edi
  004802f0  eb0c                  jmp       short 004802FEh

LAB_004802f2:
  004802f2  b900100000            mov       ecx,1000h
  004802f7  2bcf                  sub       ecx,edi
  004802f9  c1f902                sar       ecx,2
  004802fc  f7d9                  neg       ecx

LAB_004802fe:
  004802fe  03e9                  add       ebp,ecx
  00480300  8bca                  mov       ecx,edx
  00480302  c1f805                sar       eax,5
  00480305  89465c                mov       [esi+5Ch],eax
  00480308  8bc2                  mov       eax,edx
  0048030a  c1f902                sar       ecx,2
  0048030d  2bc1                  sub       eax,ecx
  0048030f  8b4e54                mov       ecx,[esi+54h]
  00480312  8bd1                  mov       edx,ecx
  00480314  8d5e1c                lea       ebx,[esi+1Ch]
  00480317  c1fa02                sar       edx,2
  0048031a  2bca                  sub       ecx,edx
  0048031c  8b5658                mov       edx,[esi+58h]
  0048031f  8bfa                  mov       edi,edx
  00480321  894650                mov       [esi+50h],eax
  00480324  c1ff02                sar       edi,2
  00480327  2bd7                  sub       edx,edi
  00480329  8b3b                  mov       edi,[ebx]
  0048032b  03f8                  add       edi,eax
  0048032d  8b4620                mov       eax,[esi+20h]
  00480330  03c1                  add       eax,ecx
  00480332  66896e34              mov       [esi+34h],bp
  00480336  8b6e24                mov       ebp,[esi+24h]
  00480339  894620                mov       [esi+20h],eax
  0048033c  8d442424              lea       eax,[esp+24h]
  00480340  03ea                  add       ebp,edx
  00480342  50                    push      eax
  00480343  56                    push      esi
  00480344  894e54                mov       [esi+54h],ecx
  00480347  895658                mov       [esi+58h],edx
  0048034a  893b                  mov       [ebx],edi
  0048034c  896e24                mov       [esi+24h],ebp
  0048034f  e84c49ffff            call      00474CA0h
  00480354  8bf8                  mov       edi,eax
  00480356  83c408                add       esp,8
  00480359  85ff                  test      edi,edi
  0048035b  743f                  je        short 0048039Ch
  0048035d  8b4674                mov       eax,[esi+74h]
  00480360  33c9                  xor       ecx,ecx
  00480362  8a4e06                mov       cl,[esi+6]
  00480365  33d2                  xor       edx,edx
  00480367  8a5607                mov       dl,[esi+7]
  0048036a  6a00                  push      0
  0048036c  51                    push      ecx
  0048036d  52                    push      edx
  0048036e  57                    push      edi
  0048036f  50                    push      eax
  00480370  e83b9f0b00            call      0053A2B0h
  00480375  8b07                  mov       eax,[edi]
  00480377  83c414                add       esp,14h
  0048037a  0c40                  or        al,40h
  0048037c  66c787960000001000    mov       word [edi+96h],10h
  00480385  53                    push      ebx
  00480386  6800120000            push      1200h
  0048038b  684e0f0000            push      0F4Eh
  00480390  6a22                  push      22h
  00480392  8907                  mov       [edi],eax
  00480394  e887d70b00            call      0053DB20h
  00480399  83c410                add       esp,10h

LAB_0048039c:
  0048039c  f6461003              test      byte [esi+10h],3
  004803a0  0f85d0000000          jne       near 00480476h
  004803a6  0fbf7e32              movsx     edi,word [esi+32h]
  004803aa  8b4e28                mov       ecx,[esi+28h]
  004803ad  8b562c                mov       edx,[esi+2Ch]
  004803b0  8b4638                mov       eax,[esi+38h]
  004803b3  81c700040000          add       edi,400h
  004803b9  57                    push      edi
  004803ba  894c2414              mov       [esp+14h],ecx
  004803be  89542418              mov       [esp+18h],edx
  004803c2  89442438              mov       [esp+38h],eax
  004803c6  e851560c00            call      00545A1Ch
  004803cb  8d0440                lea       eax,[eax+eax*2]
  004803ce  8d2c80                lea       ebp,[eax+eax*4]
  004803d1  e86a6d0c00            call      00547140h
  004803d6  83e03f                and       eax,3Fh
  004803d9  83e820                sub       eax,20h
  004803dc  c1e00c                shl       eax,0Ch
  004803df  8d0ca8                lea       ecx,[eax+ebp*4]
  004803e2  8b03                  mov       eax,[ebx]
  004803e4  03c8                  add       ecx,eax
  004803e6  894c2418              mov       [esp+18h],ecx
  004803ea  e8516d0c00            call      00547140h
  004803ef  8b4e20                mov       ecx,[esi+20h]
  004803f2  83e03f                and       eax,3Fh
  004803f5  83e820                sub       eax,20h
  004803f8  57                    push      edi
  004803f9  c1e00c                shl       eax,0Ch
  004803fc  03c1                  add       eax,ecx
  004803fe  89442420              mov       [esp+20h],eax
  00480402  e80f560c00            call      00545A16h
  00480407  8d0440                lea       eax,[eax+eax*2]
  0048040a  8d3c80                lea       edi,[eax+eax*4]
  0048040d  e82e6d0c00            call      00547140h
  00480412  83e03f                and       eax,3Fh
  00480415  8b4e24                mov       ecx,[esi+24h]
  00480418  83e820                sub       eax,20h
  0048041b  6aff                  push      0FFFFFFFFh
  0048041d  c1e00c                shl       eax,0Ch
  00480420  c7462800180018        mov       dword [esi+28h],18001800h
  00480427  66c7462c1f00          mov       word [esi+2Ch],1Fh
  0048042d  8d14b8                lea       edx,[eax+edi*4]
  00480430  33c0                  xor       eax,eax
  00480432  8a460b                mov       al,[esi+0Bh]
  00480435  03d1                  add       edx,ecx
  00480437  50                    push      eax
  00480438  68bf000000            push      0BFh
  0048043d  8d4c2424              lea       ecx,[esp+24h]
  00480441  68b81b5500            push      551BB8h
  00480446  51                    push      ecx
  00480447  56                    push      esi
  00480448  89542438              mov       [esp+38h],edx
  0048044c  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00480453  66c7461a0100          mov       word [esi+1Ah],1
  00480459  e8e2a7ffff            call      0047AC40h
  0048045e  8b542428              mov       edx,[esp+28h]
  00480462  8b44242c              mov       eax,[esp+2Ch]
  00480466  8b4c244c              mov       ecx,[esp+4Ch]
  0048046a  83c418                add       esp,18h
  0048046d  895628                mov       [esi+28h],edx
  00480470  89462c                mov       [esi+2Ch],eax
  00480473  894e38                mov       [esi+38h],ecx

LAB_00480476:
  00480476  66ff4610              inc       word [esi+10h]
  0048047a  5f                    pop       edi
  0048047b  5e                    pop       esi
  0048047c  5d                    pop       ebp
  0048047d  5b                    pop       ebx
  0048047e  83c420                add       esp,20h
  00480481  c3                    ret

14 cible(s) de saut interne : 0x004800eb, 0x00480120, 0x0048014b, 0x00480161, 0x0048016f, 0x00480181, 0x004801c3, 0x0048021a, 0x004802c5, 0x004802dd, 0x004802f2, 0x004802fe, 0x0048039c, 0x00480476
