Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004ed250  section .text  offset fichier 0xec650  2320 octets

  004ed250  83ec2c                sub       esp,2Ch
  004ed253  53                    push      ebx
  004ed254  55                    push      ebp
  004ed255  56                    push      esi
  004ed256  8b74243c              mov       esi,[esp+3Ch]
  004ed25a  8d44241c              lea       eax,[esp+1Ch]
  004ed25e  57                    push      edi
  004ed25f  8d4c2438              lea       ecx,[esp+38h]
  004ed263  50                    push      eax
  004ed264  8d542428              lea       edx,[esp+28h]
  004ed268  51                    push      ecx
  004ed269  52                    push      edx
  004ed26a  56                    push      esi
  004ed26b  e810b70400            call      00538980h
  004ed270  83c410                add       esp,10h
  004ed273  8d442428              lea       eax,[esp+28h]
  004ed277  8d4c241c              lea       ecx,[esp+1Ch]
  004ed27b  8d542410              lea       edx,[esp+10h]
  004ed27f  50                    push      eax
  004ed280  51                    push      ecx
  004ed281  8d442420              lea       eax,[esp+20h]
  004ed285  52                    push      edx
  004ed286  8d4c244c              lea       ecx,[esp+4Ch]
  004ed28a  50                    push      eax
  004ed28b  51                    push      ecx
  004ed28c  56                    push      esi
  004ed28d  e88e8c0400            call      00535F20h
  004ed292  8b4c2458              mov       ecx,[esp+58h]
  004ed296  b840000000            mov       eax,40h
  004ed29b  2bc1                  sub       eax,ecx
  004ed29d  83c418                add       esp,18h
  004ed2a0  89442414              mov       [esp+14h],eax
  004ed2a4  8b44241c              mov       eax,[esp+1Ch]
  004ed2a8  85c0                  test      eax,eax
  004ed2aa  bb03000000            mov       ebx,3
  004ed2af  bd10000000            mov       ebp,10h
  004ed2b4  0f845e050000          je        near 004ED818h
  004ed2ba  56                    push      esi
  004ed2bb  e8e0e2feff            call      004DB5A0h
  004ed2c0  8b06                  mov       eax,[esi]
  004ed2c2  83c404                add       esp,4
  004ed2c5  a802                  test      al,2
  004ed2c7  0f844a030000          je        near 004ED617h
  004ed2cd  f644242810            test      byte [esp+28h],10h
  004ed2d2  740d                  je        short 004ED2E1h
  004ed2d4  24fd                  and       al,0FDh
  004ed2d6  c6464300              mov       byte [esi+43h],0
  004ed2da  8906                  mov       [esi],eax
  004ed2dc  e937050000            jmp       004ED818h

LAB_004ed2e1:
  004ed2e1  33c9                  xor       ecx,ecx
  004ed2e3  8a4e42                mov       cl,[esi+42h]
  004ed2e6  83e900                sub       ecx,0
  004ed2e9  0f8458020000          je        near 004ED547h
  004ed2ef  49                    dec       ecx
  004ed2f0  0f8522050000          jne       near 004ED818h
  004ed2f6  0fbf4610              movsx     eax,word [esi+10h]
  004ed2fa  83e800                sub       eax,0
  004ed2fd  0f84a6000000          je        near 004ED3A9h
  004ed303  48                    dec       eax
  004ed304  0f850e050000          jne       near 004ED818h
  004ed30a  66396e1a              cmp       [esi+1Ah],bp
  004ed30e  c7465c00000000        mov       dword [esi+5Ch],0
  004ed315  7555                  jne       short 004ED36Ch
  004ed317  8b542440              mov       edx,[esp+40h]
  004ed31b  0fbf4632              movsx     eax,word [esi+32h]
  004ed31f  81c200050000          add       edx,500h
  004ed325  50                    push      eax
  004ed326  c1e207                shl       edx,7
  004ed329  89565c                mov       [esi+5Ch],edx
  004ed32c  e8eb860500            call      00545A1Ch
  004ed331  0faf465c              imul      eax,[esi+5Ch]
  004ed335  8b4e50                mov       ecx,[esi+50h]
  004ed338  c1f80c                sar       eax,0Ch
  004ed33b  03c8                  add       ecx,eax
  004ed33d  894e50                mov       [esi+50h],ecx
  004ed340  0fbf4e32              movsx     ecx,word [esi+32h]
  004ed344  51                    push      ecx
  004ed345  e8cc860500            call      00545A16h
  004ed34a  0faf465c              imul      eax,[esi+5Ch]
  004ed34e  8b5e58                mov       ebx,[esi+58h]
  004ed351  c1f80c                sar       eax,0Ch
  004ed354  03d8                  add       ebx,eax
  004ed356  8b442440              mov       eax,[esp+40h]
  004ed35a  895e58                mov       [esi+58h],ebx
  004ed35d  8d5060                lea       edx,[eax+60h]
  004ed360  c1e207                shl       edx,7
  004ed363  66ff4e10              dec       word [esi+10h]
  004ed367  89565c                mov       [esi+5Ch],edx
  004ed36a  eb04                  jmp       short 004ED370h

LAB_004ed36c:
  004ed36c  8b442440              mov       eax,[esp+40h]

LAB_004ed370:
  004ed370  8b4c2410              mov       ecx,[esp+10h]
  004ed374  d1f8                  sar       eax,1
  004ed376  83c040                add       eax,40h
  004ed379  8d1408                lea       edx,[eax+ecx]
  004ed37c  8d3c00                lea       edi,[eax+eax]
  004ed37f  81e2ff0f0000          and       edx,0FFFh
  004ed385  3bd7                  cmp       edx,edi
  004ed387  7f09                  jg        short 004ED392h
  004ed389  66014e32              add       [esi+32h],cx
  004ed38d  e986040000            jmp       004ED818h

LAB_004ed392:
  004ed392  81e1ff0f0000          and       ecx,0FFFh
  004ed398  81f900080000          cmp       ecx,800h
  004ed39e  0f8c70040000          jl        near 004ED814h
  004ed3a4  e969040000            jmp       004ED812h

LAB_004ed3a9:
  004ed3a9  8b4650                mov       eax,[esi+50h]
  004ed3ac  8b4e58                mov       ecx,[esi+58h]
  004ed3af  c1f80c                sar       eax,0Ch
  004ed3b2  8bd0                  mov       edx,eax
  004ed3b4  c1f90c                sar       ecx,0Ch
  004ed3b7  0fafd0                imul      edx,eax
  004ed3ba  8bc1                  mov       eax,ecx
  004ed3bc  0fafc1                imul      eax,ecx
  004ed3bf  03d0                  add       edx,eax
  004ed3c1  52                    push      edx
  004ed3c2  e849860500            call      00545A10h
  004ed3c7  8b16                  mov       edx,[esi]
  004ed3c9  8b4c2440              mov       ecx,[esp+40h]
  004ed3cd  f7c200000002          test      edx,2000000h
  004ed3d3  740f                  je        short 004ED3E4h
  004ed3d5  8bf9                  mov       edi,ecx
  004ed3d7  c1ff03                sar       edi,3
  004ed3da  47                    inc       edi
  004ed3db  3bc7                  cmp       eax,edi
  004ed3dd  7f05                  jg        short 004ED3E4h
  004ed3df  83e2dd                and       edx,0FFFFFFDDh
  004ed3e2  8916                  mov       [esi],edx

LAB_004ed3e4:
  004ed3e4  3bc3                  cmp       eax,ebx
  004ed3e6  0f8c2c040000          jl        near 004ED818h
  004ed3ec  f60620                test      byte [esi],20h
  004ed3ef  0f8523040000          jne       near 004ED818h
  004ed3f5  c1f903                sar       ecx,3
  004ed3f8  83c120                add       ecx,20h
  004ed3fb  8bc1                  mov       eax,ecx
  004ed3fd  8b4c2410              mov       ecx,[esp+10h]
  004ed401  8d1408                lea       edx,[eax+ecx]
  004ed404  8d3c00                lea       edi,[eax+eax]
  004ed407  81e2ff0f0000          and       edx,0FFFh
  004ed40d  3bd7                  cmp       edx,edi
  004ed40f  7f06                  jg        short 004ED417h
  004ed411  66014e32              add       [esi+32h],cx
  004ed415  eb14                  jmp       short 004ED42Bh

LAB_004ed417:
  004ed417  81e1ff0f0000          and       ecx,0FFFh
  004ed41d  81f900080000          cmp       ecx,800h
  004ed423  7c02                  jl        short 004ED427h
  004ed425  f7d8                  neg       eax

LAB_004ed427:
  004ed427  66014632              add       [esi+32h],ax

LAB_004ed42b:
  004ed42b  8d6e1c                lea       ebp,[esi+1Ch]
  004ed42e  6a00                  push      0
  004ed430  68ec2e5500            push      552EECh
  004ed435  55                    push      ebp
  004ed436  56                    push      esi
  004ed437  e8f4c70200            call      00519C30h
  004ed43c  8bf8                  mov       edi,eax
  004ed43e  83c410                add       esp,10h
  004ed441  85ff                  test      edi,edi
  004ed443  7406                  je        short 004ED44Bh
  004ed445  8b06                  mov       eax,[esi]
  004ed447  0c20                  or        al,20h
  004ed449  8906                  mov       [esi],eax

LAB_004ed44b:
  004ed44b  a1b8e05d00            mov       eax,[5DE0B8h]
  004ed450  85c0                  test      eax,eax
  004ed452  0f84c0030000          je        near 004ED818h
  004ed458  8b1f                  mov       ebx,[edi]
  004ed45a  66c787960000001000    mov       word [edi+96h],10h
  004ed463  83cb40                or        ebx,40h
  004ed466  57                    push      edi
  004ed467  891f                  mov       [edi],ebx
  004ed469  8b4c2424              mov       ecx,[esp+24h]
  004ed46d  56                    push      esi
  004ed46e  668b4130              mov       ax,[ecx+30h]
  004ed472  0fbfd8                movsx     ebx,ax
  004ed475  66d1f8                sar       ax,1
  004ed478  66894130              mov       [ecx+30h],ax
  004ed47c  e8ffc60400            call      00539B80h
  004ed481  8b442428              mov       eax,[esp+28h]
  004ed485  83c408                add       esp,8
  004ed488  66895830              mov       [eax+30h],bx
  004ed48c  8d5f1c                lea       ebx,[edi+1Ch]
  004ed48f  53                    push      ebx
  004ed490  6800180000            push      1800h
  004ed495  6800030000            push      300h
  004ed49a  6a03                  push      3
  004ed49c  e87f060500            call      0053DB20h
  004ed4a1  8b07                  mov       eax,[edi]
  004ed4a3  83c410                add       esp,10h
  004ed4a6  a900000040            test      eax,40000000h
  004ed4ab  0f8567030000          jne       near 004ED818h
  004ed4b1  0fbf4e32              movsx     ecx,word [esi+32h]
  004ed4b5  8b542410              mov       edx,[esp+10h]
  004ed4b9  03d1                  add       edx,ecx
  004ed4bb  52                    push      edx
  004ed4bc  e85b850500            call      00545A1Ch
  004ed4c1  8d0440                lea       eax,[eax+eax*2]
  004ed4c4  8d0480                lea       eax,[eax+eax*4]
  004ed4c7  d1e0                  shl       eax,1
  004ed4c9  894750                mov       [edi+50h],eax
  004ed4cc  8b542410              mov       edx,[esp+10h]
  004ed4d0  0fbf4e32              movsx     ecx,word [esi+32h]
  004ed4d4  03d1                  add       edx,ecx
  004ed4d6  52                    push      edx
  004ed4d7  e83a850500            call      00545A16h
  004ed4dc  8d0440                lea       eax,[eax+eax*2]
  004ed4df  8d0480                lea       eax,[eax+eax*4]
  004ed4e2  d1e0                  shl       eax,1
  004ed4e4  894758                mov       [edi+58h],eax
  004ed4e7  8b07                  mov       eax,[edi]
  004ed4e9  a801                  test      al,1
  004ed4eb  750b                  jne       short 004ED4F8h
  004ed4ed  0c01                  or        al,1
  004ed4ef  c7475400800100        mov       dword [edi+54h],18000h
  004ed4f6  8907                  mov       [edi],eax

LAB_004ed4f8:
  004ed4f8  8b4d00                mov       ecx,[ebp]
  004ed4fb  8b5620                mov       edx,[esi+20h]
  004ed4fe  8b4624                mov       eax,[esi+24h]
  004ed501  894c242c              mov       [esp+2Ch],ecx
  004ed505  8b0b                  mov       ecx,[ebx]
  004ed507  89542430              mov       [esp+30h],edx
  004ed50b  894d00                mov       [ebp],ecx
  004ed50e  8b5720                mov       edx,[edi+20h]
  004ed511  6aff                  push      0FFFFFFFFh
  004ed513  89442438              mov       [esp+38h],eax
  004ed517  895620                mov       [esi+20h],edx
  004ed51a  8b4724                mov       eax,[edi+24h]
  004ed51d  6a00                  push      0
  004ed51f  6a39                  push      39h
  004ed521  56                    push      esi
  004ed522  894624                mov       [esi+24h],eax
  004ed525  e8a6d4f8ff            call      0047A9D0h
  004ed52a  8b4c243c              mov       ecx,[esp+3Ch]
  004ed52e  8b542440              mov       edx,[esp+40h]
  004ed532  8b442444              mov       eax,[esp+44h]
  004ed536  83c410                add       esp,10h
  004ed539  894d00                mov       [ebp],ecx
  004ed53c  895620                mov       [esi+20h],edx
  004ed53f  894624                mov       [esi+24h],eax
  004ed542  e9d1020000            jmp       004ED818h

LAB_004ed547:
  004ed547  66395e18              cmp       [esi+18h],bx
  004ed54b  740a                  je        short 004ED557h
  004ed54d  25fffffffe            and       eax,0FEFFFFFFh
  004ed552  895e18                mov       [esi+18h],ebx
  004ed555  8906                  mov       [esi],eax

LAB_004ed557:
  004ed557  8b442440              mov       eax,[esp+40h]
  004ed55b  8b4c2410              mov       ecx,[esp+10h]
  004ed55f  d1f8                  sar       eax,1
  004ed561  83c040                add       eax,40h
  004ed564  8d1408                lea       edx,[eax+ecx]
  004ed567  8d3c00                lea       edi,[eax+eax]
  004ed56a  81e2ff0f0000          and       edx,0FFFh
  004ed570  3bd7                  cmp       edx,edi
  004ed572  7f06                  jg        short 004ED57Ah
  004ed574  66014e32              add       [esi+32h],cx
  004ed578  eb14                  jmp       short 004ED58Eh

LAB_004ed57a:
  004ed57a  81e1ff0f0000          and       ecx,0FFFh
  004ed580  81f900080000          cmp       ecx,800h
  004ed586  7c02                  jl        short 004ED58Ah
  004ed588  f7d8                  neg       eax

LAB_004ed58a:
  004ed58a  66014632              add       [esi+32h],ax

LAB_004ed58e:
  004ed58e  6a00                  push      0
  004ed590  8d461c                lea       eax,[esi+1Ch]
  004ed593  68e02e5500            push      552EE0h
  004ed598  50                    push      eax
  004ed599  56                    push      esi
  004ed59a  e891c60200            call      00519C30h
  004ed59f  8bf8                  mov       edi,eax
  004ed5a1  a1b8e05d00            mov       eax,[5DE0B8h]
  004ed5a6  83c410                add       esp,10h
  004ed5a9  85c0                  test      eax,eax
  004ed5ab  742f                  je        short 004ED5DCh
  004ed5ad  6689af96000000        mov       [edi+96h],bp
  004ed5b4  8b2f                  mov       ebp,[edi]
  004ed5b6  83cd40                or        ebp,40h
  004ed5b9  57                    push      edi
  004ed5ba  56                    push      esi
  004ed5bb  892f                  mov       [edi],ebp
  004ed5bd  e8bec50400            call      00539B80h
  004ed5c2  83c408                add       esp,8
  004ed5c5  83c71c                add       edi,1Ch
  004ed5c8  57                    push      edi
  004ed5c9  6800180000            push      1800h
  004ed5ce  6800030000            push      300h
  004ed5d3  53                    push      ebx
  004ed5d4  e847050500            call      0053DB20h
  004ed5d9  83c410                add       esp,10h

LAB_004ed5dc:
  004ed5dc  8b4c2440              mov       ecx,[esp+40h]
  004ed5e0  8b465c                mov       eax,[esi+5Ch]
  004ed5e3  81c180020000          add       ecx,280h
  004ed5e9  c1e107                shl       ecx,7
  004ed5ec  2bc8                  sub       ecx,eax
  004ed5ee  c1f904                sar       ecx,4
  004ed5f1  03c8                  add       ecx,eax
  004ed5f3  8a4643                mov       al,[esi+43h]
  004ed5f6  84c0                  test      al,al
  004ed5f8  894e5c                mov       [esi+5Ch],ecx
  004ed5fb  0f8417020000          je        near 004ED818h
  004ed601  fec8                  dec       al
  004ed603  884643                mov       [esi+43h],al
  004ed606  0f850c020000          jne       near 004ED818h
  004ed60c  8b06                  mov       eax,[esi]
  004ed60e  24fd                  and       al,0FDh
  004ed610  8906                  mov       [esi],eax
  004ed612  e901020000            jmp       004ED818h

LAB_004ed617:
  004ed617  a810                  test      al,10h
  004ed619  0f85f9010000          jne       near 004ED818h
  004ed61f  a904080000            test      eax,804h
  004ed624  0f85ee010000          jne       near 004ED818h
  004ed62a  33c9                  xor       ecx,ecx
  004ed62c  8a4e42                mov       cl,[esi+42h]
  004ed62f  3bcb                  cmp       ecx,ebx
  004ed631  0f87e1010000          ja        near 004ED818h
  004ed637  ff248d44db4e00        jmp       dword [ecx*4+4EDB44h]
  004ed63e  66837e1800            cmp       word [esi+18h],0
  004ed643  740e                  je        short 004ED653h
  004ed645  0d00000001            or        eax,1000000h
  004ed64a  c7461800000000        mov       dword [esi+18h],0
  004ed651  8906                  mov       [esi],eax

LAB_004ed653:
  004ed653  817c24182c010000      cmp       dword [esp+18h],12Ch
  004ed65b  7d09                  jge       short 004ED666h
  004ed65d  c6464300              mov       byte [esi+43h],0
  004ed661  e9b2010000            jmp       004ED818h

LAB_004ed666:
  004ed666  8a4643                mov       al,[esi+43h]
  004ed669  84c0                  test      al,al
  004ed66b  7405                  je        short 004ED672h
  004ed66d  fec8                  dec       al
  004ed66f  884643                mov       [esi+43h],al

LAB_004ed672:
  004ed672  8b465c                mov       eax,[esi+5Ch]
  004ed675  8bd0                  mov       edx,eax
  004ed677  c1fa04                sar       edx,4
  004ed67a  2bc2                  sub       eax,edx
  004ed67c  89465c                mov       [esi+5Ch],eax
  004ed67f  e994010000            jmp       004ED818h
  004ed684  66837e1800            cmp       word [esi+18h],0
  004ed689  740e                  je        short 004ED699h
  004ed68b  0d00000001            or        eax,1000000h
  004ed690  c7461800000000        mov       dword [esi+18h],0
  004ed697  8906                  mov       [esi],eax

LAB_004ed699:
  004ed699  8b442440              mov       eax,[esp+40h]
  004ed69d  8b4c2410              mov       ecx,[esp+10h]
  004ed6a1  c1f802                sar       eax,2
  004ed6a4  83c038                add       eax,38h
  004ed6a7  8d1408                lea       edx,[eax+ecx]
  004ed6aa  8d3c00                lea       edi,[eax+eax]
  004ed6ad  81e2ff0f0000          and       edx,0FFFh
  004ed6b3  3bd7                  cmp       edx,edi
  004ed6b5  7f1a                  jg        short 004ED6D1h
  004ed6b7  8b465c                mov       eax,[esi+5Ch]
  004ed6ba  66014e32              add       [esi+32h],cx
  004ed6be  8bc8                  mov       ecx,eax
  004ed6c0  c6464300              mov       byte [esi+43h],0
  004ed6c4  c1f904                sar       ecx,4
  004ed6c7  2bc1                  sub       eax,ecx
  004ed6c9  89465c                mov       [esi+5Ch],eax
  004ed6cc  e947010000            jmp       004ED818h

LAB_004ed6d1:
  004ed6d1  81e1ff0f0000          and       ecx,0FFFh
  004ed6d7  81f900080000          cmp       ecx,800h
  004ed6dd  7c02                  jl        short 004ED6E1h
  004ed6df  f7d8                  neg       eax

LAB_004ed6e1:
  004ed6e1  66014632              add       [esi+32h],ax
  004ed6e5  8b465c                mov       eax,[esi+5Ch]
  004ed6e8  8bc8                  mov       ecx,eax
  004ed6ea  c1f904                sar       ecx,4
  004ed6ed  2bc1                  sub       eax,ecx
  004ed6ef  89465c                mov       [esi+5Ch],eax
  004ed6f2  e921010000            jmp       004ED818h
  004ed6f7  b901000000            mov       ecx,1
  004ed6fc  66394e18              cmp       [esi+18h],cx
  004ed700  740a                  je        short 004ED70Ch
  004ed702  0d00000001            or        eax,1000000h
  004ed707  894e18                mov       [esi+18h],ecx
  004ed70a  8906                  mov       [esi],eax

LAB_004ed70c:
  004ed70c  817c24182c010000      cmp       dword [esp+18h],12Ch
  004ed714  7d09                  jge       short 004ED71Fh
  004ed716  c6464300              mov       byte [esi+43h],0
  004ed71a  e9f9000000            jmp       004ED818h

LAB_004ed71f:
  004ed71f  8a4643                mov       al,[esi+43h]
  004ed722  84c0                  test      al,al
  004ed724  7405                  je        short 004ED72Bh
  004ed726  fec8                  dec       al
  004ed728  884643                mov       [esi+43h],al

LAB_004ed72b:
  004ed72b  8b542440              mov       edx,[esp+40h]
  004ed72f  8b4c2410              mov       ecx,[esp+10h]
  004ed733  8bc2                  mov       eax,edx
  004ed735  c1f802                sar       eax,2
  004ed738  83c038                add       eax,38h
  004ed73b  8d3c08                lea       edi,[eax+ecx]
  004ed73e  8d1c00                lea       ebx,[eax+eax]
  004ed741  81e7ff0f0000          and       edi,0FFFh
  004ed747  3bfb                  cmp       edi,ebx
  004ed749  7f06                  jg        short 004ED751h
  004ed74b  66014e32              add       [esi+32h],cx
  004ed74f  eb14                  jmp       short 004ED765h

LAB_004ed751:
  004ed751  81e1ff0f0000          and       ecx,0FFFh
  004ed757  81f900080000          cmp       ecx,800h
  004ed75d  7c02                  jl        short 004ED761h
  004ed75f  f7d8                  neg       eax

LAB_004ed761:
  004ed761  66014632              add       [esi+32h],ax

LAB_004ed765:
  004ed765  8b465c                mov       eax,[esi+5Ch]
  004ed768  81c2a0010000          add       edx,1A0h
  004ed76e  c1e207                shl       edx,7
  004ed771  2bd0                  sub       edx,eax
  004ed773  c1fa04                sar       edx,4
  004ed776  03d0                  add       edx,eax
  004ed778  89565c                mov       [esi+5Ch],edx
  004ed77b  e998000000            jmp       004ED818h
  004ed780  33ff                  xor       edi,edi
  004ed782  66397e18              cmp       [esi+18h],di
  004ed786  740a                  je        short 004ED792h
  004ed788  0d00000001            or        eax,1000000h
  004ed78d  897e18                mov       [esi+18h],edi
  004ed790  8906                  mov       [esi],eax

LAB_004ed792:
  004ed792  8a4643                mov       al,[esi+43h]
  004ed795  84c0                  test      al,al
  004ed797  7405                  je        short 004ED79Eh
  004ed799  fec8                  dec       al
  004ed79b  884643                mov       [esi+43h],al

LAB_004ed79e:
  004ed79e  8b542440              mov       edx,[esp+40h]
  004ed7a2  0fbf4610              movsx     eax,word [esi+10h]
  004ed7a6  83c240                add       edx,40h
  004ed7a9  50                    push      eax
  004ed7aa  c1e207                shl       edx,7
  004ed7ad  89565c                mov       [esi+5Ch],edx
  004ed7b0  e867820500            call      00545A1Ch
  004ed7b5  0faf465c              imul      eax,[esi+5Ch]
  004ed7b9  8b4e50                mov       ecx,[esi+50h]
  004ed7bc  c1f80c                sar       eax,0Ch
  004ed7bf  03c8                  add       ecx,eax
  004ed7c1  894e50                mov       [esi+50h],ecx
  004ed7c4  0fbf4e10              movsx     ecx,word [esi+10h]
  004ed7c8  51                    push      ecx
  004ed7c9  e848820500            call      00545A16h
  004ed7ce  0faf465c              imul      eax,[esi+5Ch]
  004ed7d2  8b5e58                mov       ebx,[esi+58h]
  004ed7d5  8b4c2410              mov       ecx,[esp+10h]
  004ed7d9  c1f80c                sar       eax,0Ch
  004ed7dc  03d8                  add       ebx,eax
  004ed7de  8b442440              mov       eax,[esp+40h]
  004ed7e2  c1f802                sar       eax,2
  004ed7e5  83c040                add       eax,40h
  004ed7e8  897e5c                mov       [esi+5Ch],edi
  004ed7eb  895e58                mov       [esi+58h],ebx
  004ed7ee  8d1408                lea       edx,[eax+ecx]
  004ed7f1  8d3c00                lea       edi,[eax+eax]
  004ed7f4  81e2ff0f0000          and       edx,0FFFh
  004ed7fa  3bd7                  cmp       edx,edi
  004ed7fc  7f06                  jg        short 004ED804h
  004ed7fe  66014e32              add       [esi+32h],cx
  004ed802  eb14                  jmp       short 004ED818h

LAB_004ed804:
  004ed804  81e1ff0f0000          and       ecx,0FFFh
  004ed80a  81f900080000          cmp       ecx,800h
  004ed810  7c02                  jl        short 004ED814h

LAB_004ed812:
  004ed812  f7d8                  neg       eax

LAB_004ed814:
  004ed814  66014632              add       [esi+32h],ax

LAB_004ed818:
  004ed818  8b44241c              mov       eax,[esp+1Ch]
  004ed81c  50                    push      eax
  004ed81d  56                    push      esi
  004ed81e  e8bdd0faff            call      0049A8E0h
  004ed823  83c408                add       esp,8
  004ed826  56                    push      esi
  004ed827  e814610400            call      00533940h
  004ed82c  8b466c                mov       eax,[esi+6Ch]
  004ed82f  83c404                add       esp,4
  004ed832  f60001                test      byte [eax],1
  004ed835  7506                  jne       short 004ED83Dh
  004ed837  8b4860                mov       ecx,[eax+60h]
  004ed83a  894e48                mov       [esi+48h],ecx

LAB_004ed83d:
  004ed83d  e8fe980500            call      00547140h
  004ed842  8b7e60                mov       edi,[esi+60h]
  004ed845  25ff000000            and       eax,0FFh
  004ed84a  0500010000            add       eax,100h
  004ed84f  66014614              add       [esi+14h],ax
  004ed853  8b4648                mov       eax,[esi+48h]
  004ed856  668b4e14              mov       cx,[esi+14h]
  004ed85a  3bc7                  cmp       eax,edi
  004ed85c  7c02                  jl        short 004ED860h
  004ed85e  8bf8                  mov       edi,eax

LAB_004ed860:
  004ed860  f60602                test      byte [esi],2
  004ed863  7415                  je        short 004ED87Ah
  004ed865  8b4620                mov       eax,[esi+20h]
  004ed868  2bf8                  sub       edi,eax
  004ed86a  81c700800200          add       edi,28000h
  004ed870  c1ff03                sar       edi,3
  004ed873  03f8                  add       edi,eax
  004ed875  897e20                mov       [esi+20h],edi
  004ed878  eb20                  jmp       short 004ED89Ah

LAB_004ed87a:
  004ed87a  8b6e20                mov       ebp,[esi+20h]
  004ed87d  0fbfd1                movsx     edx,cx
  004ed880  52                    push      edx
  004ed881  e896810500            call      00545A1Ch
  004ed886  0500320000            add       eax,3200h
  004ed88b  c1e006                shl       eax,6
  004ed88e  03c7                  add       eax,edi
  004ed890  2bc5                  sub       eax,ebp
  004ed892  c1f804                sar       eax,4
  004ed895  03c5                  add       eax,ebp
  004ed897  894620                mov       [esi+20h],eax

LAB_004ed89a:
  004ed89a  66837e160a            cmp       word [esi+16h],0Ah
  004ed89f  7c0d                  jl        short 004ED8AEh
  004ed8a1  8b442424              mov       eax,[esp+24h]
  004ed8a5  50                    push      eax
  004ed8a6  56                    push      esi
  004ed8a7  e8c45effff            call      004E3770h
  004ed8ac  eb1d                  jmp       short 004ED8CBh

LAB_004ed8ae:
  004ed8ae  8bbe90000000          mov       edi,[esi+90h]
  004ed8b4  8b4c2424              mov       ecx,[esp+24h]
  004ed8b8  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004ed8be  51                    push      ecx
  004ed8bf  56                    push      esi
  004ed8c0  89be90000000          mov       [esi+90h],edi
  004ed8c6  e82544fbff            call      004A1CF0h

LAB_004ed8cb:
  004ed8cb  8b442424              mov       eax,[esp+24h]
  004ed8cf  83c408                add       esp,8
  004ed8d2  85c0                  test      eax,eax
  004ed8d4  0f8461020000          je        near 004EDB3Bh
  004ed8da  f70616080000          test      dword [esi],816h
  004ed8e0  0f8555020000          jne       near 004EDB3Bh
  004ed8e6  8a4643                mov       al,[esi+43h]
  004ed8e9  84c0                  test      al,al
  004ed8eb  0f854a020000          jne       near 004EDB3Bh
  004ed8f1  8b466c                mov       eax,[esi+6Ch]
  004ed8f4  8b5e1c                mov       ebx,[esi+1Ch]
  004ed8f7  8b4e24                mov       ecx,[esi+24h]
  004ed8fa  8b781c                mov       edi,[eax+1Ch]
  004ed8fd  8b6824                mov       ebp,[eax+24h]
  004ed900  2bfb                  sub       edi,ebx
  004ed902  2be9                  sub       ebp,ecx
  004ed904  c1ff0c                sar       edi,0Ch
  004ed907  c1fd0c                sar       ebp,0Ch
  004ed90a  8bd7                  mov       edx,edi
  004ed90c  8bc5                  mov       eax,ebp
  004ed90e  0fafd7                imul      edx,edi
  004ed911  0fafc5                imul      eax,ebp
  004ed914  03d0                  add       edx,eax
  004ed916  52                    push      edx
  004ed917  e8f4800500            call      00545A10h
  004ed91c  55                    push      ebp
  004ed91d  57                    push      edi
  004ed91e  89442420              mov       [esp+20h],eax
  004ed922  e8fb800500            call      00545A22h
  004ed927  668b4e32              mov       cx,[esi+32h]
  004ed92b  2bc1                  sub       eax,ecx
  004ed92d  25ff0f0000            and       eax,0FFFh
  004ed932  89442410              mov       [esp+10h],eax
  004ed936  8b442418              mov       eax,[esp+18h]
  004ed93a  3dfa000000            cmp       eax,0FAh
  004ed93f  0f8da3000000          jge       near 004ED9E8h
  004ed945  e8f6970500            call      00547140h
  004ed94a  99                    cdq
  004ed94b  b964000000            mov       ecx,64h
  004ed950  f7f9                  idiv      ecx
  004ed952  83fa3c                cmp       edx,3Ch
  004ed955  7d60                  jge       short 004ED9B7h
  004ed957  8b542410              mov       edx,[esp+10h]
  004ed95b  8b7c2414              mov       edi,[esp+14h]
  004ed95f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ed966  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ed96d  25ff0f0000            and       eax,0FFFh
  004ed972  3bc1                  cmp       eax,ecx
  004ed974  0f8f89010000          jg        near 004EDB03h
  004ed97a  e8c1970500            call      00547140h
  004ed97f  99                    cdq
  004ed980  8d4c3f01              lea       ecx,[edi+edi+1]
  004ed984  f7f9                  idiv      ecx
  004ed986  8b4c2418              mov       ecx,[esp+18h]
  004ed98a  b82c010000            mov       eax,12Ch
  004ed98f  2bc2                  sub       eax,edx
  004ed991  3bc8                  cmp       ecx,eax
  004ed993  0f8d6a010000          jge       near 004EDB03h
  004ed999  8b1e                  mov       ebx,[esi]
  004ed99b  8bcf                  mov       ecx,edi
  004ed99d  c1f903                sar       ecx,3
  004ed9a0  83cb02                or        ebx,2
  004ed9a3  80c110                add       cl,10h
  004ed9a6  891e                  mov       [esi],ebx
  004ed9a8  c6464200              mov       byte [esi+42h],0
  004ed9ac  884e43                mov       [esi+43h],cl
  004ed9af  5f                    pop       edi
  004ed9b0  5e                    pop       esi
  004ed9b1  5d                    pop       ebp
  004ed9b2  5b                    pop       ebx
  004ed9b3  83c42c                add       esp,2Ch
  004ed9b6  c3                    ret

LAB_004ed9b7:
  004ed9b7  c6464203              mov       byte [esi+42h],3
  004ed9bb  e880970500            call      00547140h
  004ed9c0  99                    cdq
  004ed9c1  b90a000000            mov       ecx,0Ah
  004ed9c6  f7f9                  idiv      ecx
  004ed9c8  8b442414              mov       eax,[esp+14h]
  004ed9cc  c1f802                sar       eax,2
  004ed9cf  02d0                  add       dl,al
  004ed9d1  02d1                  add       dl,cl
  004ed9d3  8b4c2440              mov       ecx,[esp+40h]
  004ed9d7  885643                mov       [esi+43h],dl
  004ed9da  8db980000000          lea       edi,[ecx+80h]
  004ed9e0  c1e704                shl       edi,4
  004ed9e3  e98b000000            jmp       004EDA73h

LAB_004ed9e8:
  004ed9e8  3df4010000            cmp       eax,1F4h
  004ed9ed  0f8dd2000000          jge       near 004EDAC5h
  004ed9f3  e848970500            call      00547140h
  004ed9f8  99                    cdq
  004ed9f9  b964000000            mov       ecx,64h
  004ed9fe  f7f9                  idiv      ecx
  004eda00  83fa3c                cmp       edx,3Ch
  004eda03  0f8d8c000000          jge       near 004EDA95h
  004eda09  8b542410              mov       edx,[esp+10h]
  004eda0d  8b7c2414              mov       edi,[esp+14h]
  004eda11  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004eda18  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004eda1f  25ff0f0000            and       eax,0FFFh
  004eda24  3bc1                  cmp       eax,ecx
  004eda26  7f26                  jg        short 004EDA4Eh
  004eda28  8b16                  mov       edx,[esi]
  004eda2a  c7461804000000        mov       dword [esi+18h],4
  004eda31  81e2fffffffe          and       edx,0FEFFFFFFh
  004eda37  c6464201              mov       byte [esi+42h],1
  004eda3b  83ca02                or        edx,2
  004eda3e  66c746100100          mov       word [esi+10h],1
  004eda44  8916                  mov       [esi],edx
  004eda46  5f                    pop       edi
  004eda47  5e                    pop       esi
  004eda48  5d                    pop       ebp
  004eda49  5b                    pop       ebx
  004eda4a  83c42c                add       esp,2Ch
  004eda4d  c3                    ret

LAB_004eda4e:
  004eda4e  c6464203              mov       byte [esi+42h],3
  004eda52  e8e9960500            call      00547140h
  004eda57  99                    cdq
  004eda58  b90a000000            mov       ecx,0Ah
  004eda5d  f7f9                  idiv      ecx
  004eda5f  8bc7                  mov       eax,edi
  004eda61  8b7c2440              mov       edi,[esp+40h]
  004eda65  c1f802                sar       eax,2
  004eda68  c1e704                shl       edi,4
  004eda6b  02d0                  add       dl,al
  004eda6d  02d1                  add       dl,cl
  004eda6f  47                    inc       edi
  004eda70  885643                mov       [esi+43h],dl

LAB_004eda73:
  004eda73  e8c8960500            call      00547140h
  004eda78  99                    cdq
  004eda79  f7ff                  idiv      edi
  004eda7b  8b4c2410              mov       ecx,[esp+10h]
  004eda7f  d1ff                  sar       edi,1
  004eda81  2bd7                  sub       edx,edi
  004eda83  03d1                  add       edx,ecx
  004eda85  66035632              add       dx,[esi+32h]
  004eda89  66895610              mov       [esi+10h],dx
  004eda8d  5f                    pop       edi
  004eda8e  5e                    pop       esi
  004eda8f  5d                    pop       ebp
  004eda90  5b                    pop       ebx
  004eda91  83c42c                add       esp,2Ch
  004eda94  c3                    ret

LAB_004eda95:
  004eda95  83fa50                cmp       edx,50h
  004eda98  0f8d9d000000          jge       near 004EDB3Bh
  004eda9e  c6464202              mov       byte [esi+42h],2
  004edaa2  e899960500            call      00547140h
  004edaa7  99                    cdq
  004edaa8  b90a000000            mov       ecx,0Ah
  004edaad  f7f9                  idiv      ecx
  004edaaf  8b442414              mov       eax,[esp+14h]
  004edab3  c1f802                sar       eax,2
  004edab6  02d0                  add       dl,al
  004edab8  02d1                  add       dl,cl
  004edaba  885643                mov       [esi+43h],dl
  004edabd  5f                    pop       edi
  004edabe  5e                    pop       esi
  004edabf  5d                    pop       ebp
  004edac0  5b                    pop       ebx
  004edac1  83c42c                add       esp,2Ch
  004edac4  c3                    ret

LAB_004edac5:
  004edac5  e876960500            call      00547140h
  004edaca  99                    cdq
  004edacb  b964000000            mov       ecx,64h
  004edad0  f7f9                  idiv      ecx
  004edad2  83fa1e                cmp       edx,1Eh
  004edad5  7d27                  jge       short 004EDAFEh
  004edad7  c6464200              mov       byte [esi+42h],0
  004edadb  e860960500            call      00547140h
  004edae0  99                    cdq
  004edae1  b90a000000            mov       ecx,0Ah
  004edae6  f7f9                  idiv      ecx
  004edae8  8b442414              mov       eax,[esp+14h]
  004edaec  c1f802                sar       eax,2
  004edaef  02d0                  add       dl,al
  004edaf1  02d1                  add       dl,cl
  004edaf3  885643                mov       [esi+43h],dl
  004edaf6  5f                    pop       edi
  004edaf7  5e                    pop       esi
  004edaf8  5d                    pop       ebp
  004edaf9  5b                    pop       ebx
  004edafa  83c42c                add       esp,2Ch
  004edafd  c3                    ret

LAB_004edafe:
  004edafe  83fa3c                cmp       edx,3Ch
  004edb01  7d10                  jge       short 004EDB13h

LAB_004edb03:
  004edb03  c6464201              mov       byte [esi+42h],1
  004edb07  c6464301              mov       byte [esi+43h],1
  004edb0b  5f                    pop       edi
  004edb0c  5e                    pop       esi
  004edb0d  5d                    pop       ebp
  004edb0e  5b                    pop       ebx
  004edb0f  83c42c                add       esp,2Ch
  004edb12  c3                    ret

LAB_004edb13:
  004edb13  c6464203              mov       byte [esi+42h],3
  004edb17  e824960500            call      00547140h
  004edb1c  99                    cdq
  004edb1d  b90a000000            mov       ecx,0Ah
  004edb22  f7f9                  idiv      ecx
  004edb24  8b442414              mov       eax,[esp+14h]
  004edb28  c1f802                sar       eax,2
  004edb2b  02d0                  add       dl,al
  004edb2d  02d1                  add       dl,cl
  004edb2f  885643                mov       [esi+43h],dl
  004edb32  e809960500            call      00547140h
  004edb37  66894610              mov       [esi+10h],ax

LAB_004edb3b:
  004edb3b  5f                    pop       edi
  004edb3c  5e                    pop       esi
  004edb3d  5d                    pop       ebp
  004edb3e  5b                    pop       ebx
  004edb3f  83c42c                add       esp,2Ch
  004edb42  c3                    ret
  004edb43  90                    nop
  004edb44  3ed6                  salc
  004edb46  4e                    dec       esi
  004edb47  0084d64e00f7d6        add       [esi+edx*8-2908FFB2h],al
  004edb4e  4e                    dec       esi
  004edb4f  0080d74e0090          add       [eax-6FFFB129h],al
  004edb55  90                    nop
  004edb56  90                    nop
  004edb57  90                    nop
  004edb58  90                    nop
  004edb59  90                    nop
  004edb5a  90                    nop
  004edb5b  90                    nop
  004edb5c  90                    nop
  004edb5d  90                    nop
  004edb5e  90                    nop
  004edb5f  90                    nop

52 cible(s) de saut interne : 0x004ed2e1, 0x004ed36c, 0x004ed370, 0x004ed392, 0x004ed3a9, 0x004ed3e4, 0x004ed417, 0x004ed427, 0x004ed42b, 0x004ed44b, 0x004ed4f8, 0x004ed547, 0x004ed557, 0x004ed57a, 0x004ed58a, 0x004ed58e, 0x004ed5dc, 0x004ed617, 0x004ed653, 0x004ed666, 0x004ed672, 0x004ed699, 0x004ed6d1, 0x004ed6e1, 0x004ed70c, 0x004ed71f, 0x004ed72b, 0x004ed751, 0x004ed761, 0x004ed765, 0x004ed792, 0x004ed79e, 0x004ed804, 0x004ed812, 0x004ed814, 0x004ed818, 0x004ed83d, 0x004ed860, 0x004ed87a, 0x004ed89a, 0x004ed8ae, 0x004ed8cb, 0x004ed9b7, 0x004ed9e8, 0x004eda4e, 0x004eda73, 0x004eda95, 0x004edac5, 0x004edafe, 0x004edb03, 0x004edb13, 0x004edb3b
