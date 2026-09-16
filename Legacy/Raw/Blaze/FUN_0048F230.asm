Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048f230  section .text  offset fichier 0x8e630  3968 octets

  0048f230  81ec90000000          sub       esp,90h
  0048f236  53                    push      ebx
  0048f237  55                    push      ebp
  0048f238  56                    push      esi
  0048f239  8bb424a0000000        mov       esi,[esp+0A0h]
  0048f240  b864000000            mov       eax,64h
  0048f245  33db                  xor       ebx,ebx
  0048f247  6689442436            mov       [esp+36h],ax
  0048f24c  668944243a            mov       [esp+3Ah],ax
  0048f251  6689442442            mov       [esp+42h],ax
  0048f256  8a4641                mov       al,[esi+41h]
  0048f259  b9c8000000            mov       ecx,0C8h
  0048f25e  57                    push      edi
  0048f25f  84c0                  test      al,al
  0048f261  66895c2434            mov       [esp+34h],bx
  0048f266  66c74424361e00        mov       word [esp+36h],1Eh
  0048f26d  66c74424383200        mov       word [esp+38h],32h
  0048f274  66895c243c            mov       [esp+3Ch],bx
  0048f279  66895c2440            mov       [esp+40h],bx
  0048f27e  66c74424429cff        mov       word [esp+42h],0FF9Ch
  0048f285  66894c2444            mov       [esp+44h],cx
  0048f28a  66895c2448            mov       [esp+48h],bx
  0048f28f  66894c244a            mov       [esp+4Ah],cx
  0048f294  7510                  jne       short 0048F2A6h
  0048f296  53                    push      ebx
  0048f297  6a03                  push      3
  0048f299  56                    push      esi
  0048f29a  c6464101              mov       byte [esi+41h],1
  0048f29e  e8cdceffff            call      0048C170h
  0048f2a3  83c40c                add       esp,0Ch

LAB_0048f2a6:
  0048f2a6  8b461c                mov       eax,[esi+1Ch]
  0048f2a9  8b4e20                mov       ecx,[esi+20h]
  0048f2ac  8b5624                mov       edx,[esi+24h]
  0048f2af  8d6e1c                lea       ebp,[esi+1Ch]
  0048f2b2  89442428              mov       [esp+28h],eax
  0048f2b6  8b4664                mov       eax,[esi+64h]
  0048f2b9  894c242c              mov       [esp+2Ch],ecx
  0048f2bd  8b4e60                mov       ecx,[esi+60h]
  0048f2c0  89542430              mov       [esp+30h],edx
  0048f2c4  668b5632              mov       dx,[esi+32h]
  0048f2c8  89442450              mov       [esp+50h],eax
  0048f2cc  8b467c                mov       eax,[esi+7Ch]
  0048f2cf  894c2454              mov       [esp+54h],ecx
  0048f2d3  668b8e90000000        mov       cx,[esi+90h]
  0048f2da  668994248e000000      mov       [esp+8Eh],dx
  0048f2e2  8b9680000000          mov       edx,[esi+80h]
  0048f2e8  89442464              mov       [esp+64h],eax
  0048f2ec  8b8684000000          mov       eax,[esi+84h]
  0048f2f2  66894c246c            mov       [esp+6Ch],cx
  0048f2f7  8b8e88000000          mov       ecx,[esi+88h]
  0048f2fd  8b7e6c                mov       edi,[esi+6Ch]
  0048f300  89542460              mov       [esp+60h],edx
  0048f304  8b968c000000          mov       edx,[esi+8Ch]
  0048f30a  8944244c              mov       [esp+4Ch],eax
  0048f30e  894c2458              mov       [esp+58h],ecx
  0048f312  8d442424              lea       eax,[esp+24h]
  0048f316  89542468              mov       [esp+68h],edx
  0048f31a  8d4c2470              lea       ecx,[esp+70h]
  0048f31e  50                    push      eax
  0048f31f  8d542478              lea       edx,[esp+78h]
  0048f323  51                    push      ecx
  0048f324  52                    push      edx
  0048f325  56                    push      esi
  0048f326  e855960a00            call      00538980h
  0048f32b  83c410                add       esp,10h
  0048f32e  8d44245c              lea       eax,[esp+5Ch]
  0048f332  8d4c2420              lea       ecx,[esp+20h]
  0048f336  8d542418              lea       edx,[esp+18h]
  0048f33a  50                    push      eax
  0048f33b  51                    push      ecx
  0048f33c  8d44241c              lea       eax,[esp+1Ch]
  0048f340  52                    push      edx
  0048f341  8d4c241c              lea       ecx,[esp+1Ch]
  0048f345  50                    push      eax
  0048f346  51                    push      ecx
  0048f347  56                    push      esi
  0048f348  e8d36b0a00            call      00535F20h
  0048f34d  8b4c2428              mov       ecx,[esp+28h]
  0048f351  8b542474              mov       edx,[esp+74h]
  0048f355  83c418                add       esp,18h
  0048f358  b840000000            mov       eax,40h
  0048f35d  2bc1                  sub       eax,ecx
  0048f35f  52                    push      edx
  0048f360  56                    push      esi
  0048f361  89442424              mov       [esp+24h],eax
  0048f365  e8766a0400            call      004D5DE0h
  0048f36a  8b4500                mov       eax,[ebp]
  0048f36d  8b571c                mov       edx,[edi+1Ch]
  0048f370  8b4e20                mov       ecx,[esi+20h]
  0048f373  2bc2                  sub       eax,edx
  0048f375  8b5720                mov       edx,[edi+20h]
  0048f378  83c408                add       esp,8
  0048f37b  2bca                  sub       ecx,edx
  0048f37d  8b5624                mov       edx,[esi+24h]
  0048f380  2b5724                sub       edx,[edi+24h]
  0048f383  c1f80c                sar       eax,0Ch
  0048f386  8bf8                  mov       edi,eax
  0048f388  c1f90c                sar       ecx,0Ch
  0048f38b  0faff8                imul      edi,eax
  0048f38e  8bc1                  mov       eax,ecx
  0048f390  c1fa0c                sar       edx,0Ch
  0048f393  0fafc1                imul      eax,ecx
  0048f396  8bca                  mov       ecx,edx
  0048f398  03f8                  add       edi,eax
  0048f39a  0fafca                imul      ecx,edx
  0048f39d  03f9                  add       edi,ecx
  0048f39f  57                    push      edi
  0048f3a0  e86b660b00            call      00545A10h
  0048f3a5  89442414              mov       [esp+14h],eax
  0048f3a9  8b442420              mov       eax,[esp+20h]
  0048f3ad  3bc3                  cmp       eax,ebx
  0048f3af  0f84cd070000          je        near 0048FB82h
  0048f3b5  8b3e                  mov       edi,[esi]
  0048f3b7  33d2                  xor       edx,edx
  0048f3b9  8a5604                mov       dl,[esi+4]
  0048f3bc  81e7fffffffd          and       edi,0FDFFFFFFh
  0048f3c2  0fbf4e18              movsx     ecx,word [esi+18h]
  0048f3c6  893e                  mov       [esi],edi
  0048f3c8  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0048f3cf  8bc7                  mov       eax,edi
  0048f3d1  8b0c8a                mov       ecx,[edx+ecx*4]
  0048f3d4  a900000001            test      eax,1000000h
  0048f3d9  741b                  je        short 0048F3F6h
  0048f3db  66ff461a              inc       word [esi+1Ah]
  0048f3df  668b461a              mov       ax,[esi+1Ah]
  0048f3e3  33d2                  xor       edx,edx
  0048f3e5  668b5116              mov       dx,[ecx+16h]
  0048f3e9  0fbfc0                movsx     eax,ax
  0048f3ec  3bc2                  cmp       eax,edx
  0048f3ee  7c27                  jl        short 0048F417h
  0048f3f0  66895e1a              mov       [esi+1Ah],bx
  0048f3f4  eb21                  jmp       short 0048F417h

LAB_0048f3f6:
  0048f3f6  66ff461a              inc       word [esi+1Ah]
  0048f3fa  668b561a              mov       dx,[esi+1Ah]
  0048f3fe  33ff                  xor       edi,edi
  0048f400  668b7916              mov       di,[ecx+16h]
  0048f404  0fbfca                movsx     ecx,dx
  0048f407  3bcf                  cmp       ecx,edi
  0048f409  7c0c                  jl        short 0048F417h
  0048f40b  4a                    dec       edx
  0048f40c  0d00000002            or        eax,2000000h
  0048f411  6689561a              mov       [esi+1Ah],dx
  0048f415  8906                  mov       [esi],eax

LAB_0048f417:
  0048f417  8a4642                mov       al,[esi+42h]
  0048f41a  8b0e                  mov       ecx,[esi]
  0048f41c  3c05                  cmp       al,5
  0048f41e  7505                  jne       short 0048F425h
  0048f420  83c910                or        ecx,10h
  0048f423  eb03                  jmp       short 0048F428h

LAB_0048f425:
  0048f425  83e1ef                and       ecx,0FFFFFFEFh

LAB_0048f428:
  0048f428  f6c102                test      cl,2
  0048f42b  890e                  mov       [esi],ecx
  0048f42d  0f841b020000          je        near 0048F64Eh
  0048f433  3c0c                  cmp       al,0Ch
  0048f435  bb08000000            mov       ebx,8
  0048f43a  7479                  je        short 0048F4B5h
  0048f43c  3c0d                  cmp       al,0Dh
  0048f43e  7475                  je        short 0048F4B5h
  0048f440  66395e1a              cmp       [esi+1Ah],bx
  0048f444  7554                  jne       short 0048F49Ah
  0048f446  8d542434              lea       edx,[esp+34h]
  0048f44a  6a00                  push      0
  0048f44c  52                    push      edx
  0048f44d  55                    push      ebp
  0048f44e  56                    push      esi
  0048f44f  e8dca70800            call      00519C30h
  0048f454  8bf8                  mov       edi,eax
  0048f456  a1b8e05d00            mov       eax,[5DE0B8h]
  0048f45b  83c410                add       esp,10h
  0048f45e  85c0                  test      eax,eax
  0048f460  7438                  je        short 0048F49Ah
  0048f462  66395e1a              cmp       [esi+1Ah],bx
  0048f466  7532                  jne       short 0048F49Ah
  0048f468  8b0f                  mov       ecx,[edi]
  0048f46a  57                    push      edi
  0048f46b  83c940                or        ecx,40h
  0048f46e  56                    push      esi
  0048f46f  66c787960000001000    mov       word [edi+96h],10h
  0048f478  890f                  mov       [edi],ecx
  0048f47a  e801a70a00            call      00539B80h
  0048f47f  83c408                add       esp,8
  0048f482  83c71c                add       edi,1Ch
  0048f485  57                    push      edi
  0048f486  6800180000            push      1800h
  0048f48b  6800030000            push      300h
  0048f490  6a03                  push      3
  0048f492  e889e60a00            call      0053DB20h
  0048f497  83c410                add       esp,10h

LAB_0048f49a:
  0048f49a  8b06                  mov       eax,[esi]
  0048f49c  a900000002            test      eax,2000000h
  0048f4a1  7412                  je        short 0048F4B5h
  0048f4a3  a801                  test      al,1
  0048f4a5  750a                  jne       short 0048F4B1h
  0048f4a7  24fd                  and       al,0FDh
  0048f4a9  c6464300              mov       byte [esi+43h],0
  0048f4ad  8906                  mov       [esi],eax
  0048f4af  eb04                  jmp       short 0048F4B5h

LAB_0048f4b1:
  0048f4b1  24fd                  and       al,0FDh
  0048f4b3  8906                  mov       [esi],eax

LAB_0048f4b5:
  0048f4b5  807e420c              cmp       byte [esi+42h],0Ch
  0048f4b9  0f85c5000000          jne       near 0048F584h
  0048f4bf  668b461a              mov       ax,[esi+1Ah]
  0048f4c3  663bc3                cmp       ax,bx
  0048f4c6  7507                  jne       short 0048F4CFh
  0048f4c8  66837e1803            cmp       word [esi+18h],3
  0048f4cd  7415                  je        short 0048F4E4h

LAB_0048f4cf:
  0048f4cf  663d0c00              cmp       ax,0Ch
  0048f4d3  0f85ab000000          jne       near 0048F584h
  0048f4d9  66837e1806            cmp       word [esi+18h],6
  0048f4de  0f85a0000000          jne       near 0048F584h

LAB_0048f4e4:
  0048f4e4  8d442434              lea       eax,[esp+34h]
  0048f4e8  6a00                  push      0
  0048f4ea  50                    push      eax
  0048f4eb  55                    push      ebp
  0048f4ec  56                    push      esi
  0048f4ed  e83ea70800            call      00519C30h
  0048f4f2  8bf8                  mov       edi,eax
  0048f4f4  a1b8e05d00            mov       eax,[5DE0B8h]
  0048f4f9  83c410                add       esp,10h
  0048f4fc  85c0                  test      eax,eax
  0048f4fe  0f8480000000          je        near 0048F584h
  0048f504  668b461a              mov       ax,[esi+1Ah]
  0048f508  663bc3                cmp       ax,bx
  0048f50b  7507                  jne       short 0048F514h
  0048f50d  66837e1803            cmp       word [esi+18h],3
  0048f512  740d                  je        short 0048F521h

LAB_0048f514:
  0048f514  663d0c00              cmp       ax,0Ch
  0048f518  756a                  jne       short 0048F584h
  0048f51a  66837e1806            cmp       word [esi+18h],6
  0048f51f  7563                  jne       short 0048F584h

LAB_0048f521:
  0048f521  66837e1201            cmp       word [esi+12h],1
  0048f526  750b                  jne       short 0048F533h
  0048f528  66c787960000000000    mov       word [edi+96h],0
  0048f531  eb0f                  jmp       short 0048F542h

LAB_0048f533:
  0048f533  8b07                  mov       eax,[edi]
  0048f535  66c787960000001000    mov       word [edi+96h],10h
  0048f53e  0c40                  or        al,40h
  0048f540  8907                  mov       [edi],eax

LAB_0048f542:
  0048f542  8b4c2424              mov       ecx,[esp+24h]
  0048f546  57                    push      edi
  0048f547  56                    push      esi
  0048f548  668b4130              mov       ax,[ecx+30h]
  0048f54c  668bd0                mov       dx,ax
  0048f54f  66c1fa02              sar       dx,2
  0048f553  0fbfd8                movsx     ebx,ax
  0048f556  2bc2                  sub       eax,edx
  0048f558  66894130              mov       [ecx+30h],ax
  0048f55c  e81fa60a00            call      00539B80h
  0048f561  8b44242c              mov       eax,[esp+2Ch]
  0048f565  83c408                add       esp,8
  0048f568  83c71c                add       edi,1Ch
  0048f56b  57                    push      edi
  0048f56c  6800180000            push      1800h
  0048f571  6800030000            push      300h
  0048f576  6a03                  push      3
  0048f578  66895830              mov       [eax+30h],bx
  0048f57c  e89fe50a00            call      0053DB20h
  0048f581  83c410                add       esp,10h

LAB_0048f584:
  0048f584  807e420d              cmp       byte [esi+42h],0Dh
  0048f588  0f85fe000000          jne       near 0048F68Ch
  0048f58e  33db                  xor       ebx,ebx
  0048f590  8d4c2440              lea       ecx,[esp+40h]
  0048f594  53                    push      ebx
  0048f595  51                    push      ecx
  0048f596  55                    push      ebp
  0048f597  56                    push      esi
  0048f598  e893a60800            call      00519C30h
  0048f59d  8bf8                  mov       edi,eax
  0048f59f  a1b8e05d00            mov       eax,[5DE0B8h]
  0048f5a4  83c410                add       esp,10h
  0048f5a7  3bc3                  cmp       eax,ebx
  0048f5a9  0f84e5000000          je        near 0048F694h
  0048f5af  8b1f                  mov       ebx,[edi]
  0048f5b1  66c787960000001000    mov       word [edi+96h],10h
  0048f5ba  83cb40                or        ebx,40h
  0048f5bd  57                    push      edi
  0048f5be  891f                  mov       [edi],ebx
  0048f5c0  8b4c2428              mov       ecx,[esp+28h]
  0048f5c4  56                    push      esi
  0048f5c5  668b4130              mov       ax,[ecx+30h]
  0048f5c9  0fbfd8                movsx     ebx,ax
  0048f5cc  66d1f8                sar       ax,1
  0048f5cf  66894130              mov       [ecx+30h],ax
  0048f5d3  e8a8a50a00            call      00539B80h
  0048f5d8  8b54242c              mov       edx,[esp+2Ch]
  0048f5dc  83c408                add       esp,8
  0048f5df  66895a30              mov       [edx+30h],bx
  0048f5e3  8d5f1c                lea       ebx,[edi+1Ch]
  0048f5e6  53                    push      ebx
  0048f5e7  6800180000            push      1800h
  0048f5ec  6800030000            push      300h
  0048f5f1  6a03                  push      3
  0048f5f3  e828e50a00            call      0053DB20h
  0048f5f8  8b07                  mov       eax,[edi]
  0048f5fa  83c410                add       esp,10h
  0048f5fd  a900000040            test      eax,40000000h
  0048f602  753c                  jne       short 0048F640h
  0048f604  6aff                  push      0FFFFFFFFh
  0048f606  6a00                  push      0
  0048f608  6a39                  push      39h
  0048f60a  56                    push      esi
  0048f60b  e8c0b3feff            call      0047A9D0h
  0048f610  8b17                  mov       edx,[edi]
  0048f612  8b0b                  mov       ecx,[ebx]
  0048f614  83ca01                or        edx,1
  0048f617  8b5f24                mov       ebx,[edi+24h]
  0048f61a  8917                  mov       [edi],edx
  0048f61c  8b4500                mov       eax,[ebp]
  0048f61f  2bc1                  sub       eax,ecx
  0048f621  83c410                add       esp,10h
  0048f624  c1f803                sar       eax,3
  0048f627  f7d8                  neg       eax
  0048f629  894750                mov       [edi+50h],eax
  0048f62c  8b4e24                mov       ecx,[esi+24h]
  0048f62f  2bcb                  sub       ecx,ebx
  0048f631  c7475400800100        mov       dword [edi+54h],18000h
  0048f638  c1f903                sar       ecx,3
  0048f63b  f7d9                  neg       ecx
  0048f63d  894f58                mov       [edi+58h],ecx

LAB_0048f640:
  0048f640  8b06                  mov       eax,[esi]
  0048f642  8b4c2410              mov       ecx,[esp+10h]
  0048f646  24fd                  and       al,0FDh
  0048f648  33db                  xor       ebx,ebx
  0048f64a  8906                  mov       [esi],eax
  0048f64c  eb4a                  jmp       short 0048F698h

LAB_0048f64e:
  0048f64e  f6c101                test      cl,1
  0048f651  7439                  je        short 0048F68Ch
  0048f653  66837e1a03            cmp       word [esi+1Ah],3
  0048f658  7d32                  jge       short 0048F68Ch
  0048f65a  e8e17a0b00            call      00547140h
  0048f65f  99                    cdq
  0048f660  b93c000000            mov       ecx,3Ch
  0048f665  f7f9                  idiv      ecx
  0048f667  8b4c2410              mov       ecx,[esp+10h]
  0048f66b  3bd1                  cmp       edx,ecx
  0048f66d  7d21                  jge       short 0048F690h
  0048f66f  8b16                  mov       edx,[esi]
  0048f671  66c746180300          mov       word [esi+18h],3
  0048f677  81e2fffffffe          and       edx,0FEFFFFFFh
  0048f67d  66c7461a0000          mov       word [esi+1Ah],0
  0048f683  83ca02                or        edx,2
  0048f686  33db                  xor       ebx,ebx
  0048f688  8916                  mov       [esi],edx
  0048f68a  eb0c                  jmp       short 0048F698h

LAB_0048f68c:
  0048f68c  8b4c2410              mov       ecx,[esp+10h]

LAB_0048f690:
  0048f690  33db                  xor       ebx,ebx
  0048f692  eb04                  jmp       short 0048F698h

LAB_0048f694:
  0048f694  8b4c2410              mov       ecx,[esp+10h]

LAB_0048f698:
  0048f698  33c0                  xor       eax,eax
  0048f69a  8a4642                mov       al,[esi+42h]
  0048f69d  83f80d                cmp       eax,0Dh
  0048f6a0  0f87dc040000          ja        near 0048FB82h
  0048f6a6  ff248574014900        jmp       dword [eax*4+490174h]
  0048f6ad  66395e12              cmp       [esi+12h],bx
  0048f6b1  751a                  jne       short 0048F6CDh
  0048f6b3  8b06                  mov       eax,[esi]
  0048f6b5  66895e18              mov       [esi+18h],bx
  0048f6b9  0d00000001            or        eax,1000000h
  0048f6be  66895e1a              mov       [esi+1Ah],bx
  0048f6c2  8906                  mov       [esi],eax
  0048f6c4  66c746120100          mov       word [esi+12h],1
  0048f6ca  895e5c                mov       [esi+5Ch],ebx

LAB_0048f6cd:
  0048f6cd  0fbf4610              movsx     eax,word [esi+10h]
  0048f6d1  39442414              cmp       [esp+14h],eax
  0048f6d5  7d09                  jge       short 0048F6E0h
  0048f6d7  c6464300              mov       byte [esi+43h],0
  0048f6db  e9a2040000            jmp       0048FB82h

LAB_0048f6e0:
  0048f6e0  8a4643                mov       al,[esi+43h]
  0048f6e3  84c0                  test      al,al
  0048f6e5  0f8494040000          je        near 0048FB7Fh
  0048f6eb  fec8                  dec       al
  0048f6ed  884643                mov       [esi+43h],al
  0048f6f0  e98a040000            jmp       0048FB7Fh
  0048f6f5  66395e12              cmp       [esi+12h],bx
  0048f6f9  751b                  jne       short 0048F716h
  0048f6fb  8b16                  mov       edx,[esi]
  0048f6fd  b801000000            mov       eax,1
  0048f702  81ca00000001          or        edx,1000000h
  0048f708  66894618              mov       [esi+18h],ax
  0048f70c  66895e1a              mov       [esi+1Ah],bx
  0048f710  8916                  mov       [esi],edx
  0048f712  66894612              mov       [esi+12h],ax

LAB_0048f716:
  0048f716  8b442418              mov       eax,[esp+18h]
  0048f71a  8d90c8000000          lea       edx,[eax+0C8h]
  0048f720  81e2ff0f0000          and       edx,0FFFh
  0048f726  81fa90010000          cmp       edx,190h
  0048f72c  7f09                  jg        short 0048F737h
  0048f72e  c6464300              mov       byte [esi+43h],0
  0048f732  e948040000            jmp       0048FB7Fh

LAB_0048f737:
  0048f737  25ff0f0000            and       eax,0FFFh
  0048f73c  3d00080000            cmp       eax,800h
  0048f741  7d0c                  jge       short 0048F74Fh
  0048f743  8d413c                lea       eax,[ecx+3Ch]
  0048f746  66014632              add       [esi+32h],ax
  0048f74a  e930040000            jmp       0048FB7Fh

LAB_0048f74f:
  0048f74f  b8c4ffffff            mov       eax,0FFFFFFC4h
  0048f754  2bc1                  sub       eax,ecx
  0048f756  66014632              add       [esi+32h],ax
  0048f75a  e920040000            jmp       0048FB7Fh
  0048f75f  66395e12              cmp       [esi+12h],bx
  0048f763  b801000000            mov       eax,1
  0048f768  7525                  jne       short 0048F78Fh
  0048f76a  8b16                  mov       edx,[esi]
  0048f76c  66894618              mov       [esi+18h],ax
  0048f770  81ca00000001          or        edx,1000000h
  0048f776  66895e1a              mov       [esi+1Ah],bx
  0048f77a  8916                  mov       [esi],edx
  0048f77c  8d14c9                lea       edx,[ecx+ecx*8]
  0048f77f  c1e204                shl       edx,4
  0048f782  81c200600000          add       edx,6000h
  0048f788  66894612              mov       [esi+12h],ax
  0048f78c  89565c                mov       [esi+5Ch],edx

LAB_0048f78f:
  0048f78f  66394612              cmp       [esi+12h],ax
  0048f793  0f85e9030000          jne       near 0048FB82h
  0048f799  0fbf4610              movsx     eax,word [esi+10h]
  0048f79d  39442414              cmp       [esp+14h],eax
  0048f7a1  7d09                  jge       short 0048F7ACh
  0048f7a3  c6464300              mov       byte [esi+43h],0
  0048f7a7  e9d3030000            jmp       0048FB7Fh

LAB_0048f7ac:
  0048f7ac  8a4643                mov       al,[esi+43h]
  0048f7af  84c0                  test      al,al
  0048f7b1  7405                  je        short 0048F7B8h
  0048f7b3  fec8                  dec       al
  0048f7b5  884643                mov       [esi+43h],al

LAB_0048f7b8:
  0048f7b8  8b442418              mov       eax,[esp+18h]
  0048f7bc  8d9000010000          lea       edx,[eax+100h]
  0048f7c2  81e2ff0f0000          and       edx,0FFFh
  0048f7c8  81fa00020000          cmp       edx,200h
  0048f7ce  0f8eae030000          jle       near 0048FB82h
  0048f7d4  25ff0f0000            and       eax,0FFFh
  0048f7d9  3d00080000            cmp       eax,800h
  0048f7de  7d0c                  jge       short 0048F7ECh
  0048f7e0  8d413c                lea       eax,[ecx+3Ch]
  0048f7e3  66014632              add       [esi+32h],ax
  0048f7e7  e996030000            jmp       0048FB82h

LAB_0048f7ec:
  0048f7ec  b8c4ffffff            mov       eax,0FFFFFFC4h
  0048f7f1  2bc1                  sub       eax,ecx
  0048f7f3  66014632              add       [esi+32h],ax
  0048f7f7  e986030000            jmp       0048FB82h
  0048f7fc  bb01000000            mov       ebx,1
  0048f801  66395e12              cmp       [esi+12h],bx
  0048f805  0f859b000000          jne       near 0048F8A6h
  0048f80b  8a4643                mov       al,[esi+43h]
  0048f80e  84c0                  test      al,al
  0048f810  7405                  je        short 0048F817h
  0048f812  fec8                  dec       al
  0048f814  884643                mov       [esi+43h],al

LAB_0048f817:
  0048f817  8b7c2420              mov       edi,[esp+20h]
  0048f81b  8d04c9                lea       eax,[ecx+ecx*8]
  0048f81e  c1e004                shl       eax,4
  0048f821  0500500000            add       eax,5000h
  0048f826  0fbf4e10              movsx     ecx,word [esi+10h]
  0048f82a  0faff8                imul      edi,eax
  0048f82d  51                    push      ecx
  0048f82e  89465c                mov       [esi+5Ch],eax
  0048f831  c1ff0c                sar       edi,0Ch
  0048f834  e8e3610b00            call      00545A1Ch
  0048f839  0fafc7                imul      eax,edi
  0048f83c  0fbf5610              movsx     edx,word [esi+10h]
  0048f840  8b4d00                mov       ecx,[ebp]
  0048f843  52                    push      edx
  0048f844  c1f80c                sar       eax,0Ch
  0048f847  03c8                  add       ecx,eax
  0048f849  894d00                mov       [ebp],ecx
  0048f84c  e8c5610b00            call      00545A16h
  0048f851  0fafc7                imul      eax,edi
  0048f854  8b6e24                mov       ebp,[esi+24h]
  0048f857  c7465c00000000        mov       dword [esi+5Ch],0
  0048f85e  c1f80c                sar       eax,0Ch
  0048f861  03e8                  add       ebp,eax
  0048f863  8b442418              mov       eax,[esp+18h]
  0048f867  896e24                mov       [esi+24h],ebp
  0048f86a  8d8800010000          lea       ecx,[eax+100h]
  0048f870  81e1ff0f0000          and       ecx,0FFFh
  0048f876  81f900020000          cmp       ecx,200h
  0048f87c  7e28                  jle       short 0048F8A6h
  0048f87e  25ff0f0000            and       eax,0FFFh
  0048f883  3d00080000            cmp       eax,800h
  0048f888  7d0a                  jge       short 0048F894h
  0048f88a  8b542410              mov       edx,[esp+10h]
  0048f88e  8d441232              lea       eax,[edx+edx+32h]
  0048f892  eb0e                  jmp       short 0048F8A2h

LAB_0048f894:
  0048f894  8b442410              mov       eax,[esp+10h]
  0048f898  8d0c00                lea       ecx,[eax+eax]
  0048f89b  b8ceffffff            mov       eax,0FFFFFFCEh
  0048f8a0  2bc1                  sub       eax,ecx

LAB_0048f8a2:
  0048f8a2  66014632              add       [esi+32h],ax

LAB_0048f8a6:
  0048f8a6  66837e1200            cmp       word [esi+12h],0
  0048f8ab  0f85d1020000          jne       near 0048FB82h
  0048f8b1  8b06                  mov       eax,[esi]
  0048f8b3  66895e18              mov       [esi+18h],bx
  0048f8b7  0d00000001            or        eax,1000000h
  0048f8bc  66c7461a0000          mov       word [esi+1Ah],0
  0048f8c2  8906                  mov       [esi],eax
  0048f8c4  66895e12              mov       [esi+12h],bx
  0048f8c8  e9b5020000            jmp       0048FB82h
  0048f8cd  66395e12              cmp       [esi+12h],bx
  0048f8d1  757b                  jne       short 0048F94Eh
  0048f8d3  8b06                  mov       eax,[esi]
  0048f8d5  a801                  test      al,1
  0048f8d7  7406                  je        short 0048F8DFh
  0048f8d9  c6464300              mov       byte [esi+43h],0
  0048f8dd  eb6f                  jmp       short 0048F94Eh

LAB_0048f8df:
  0048f8df  8b7c2420              mov       edi,[esp+20h]
  0048f8e3  25fffffffe            and       eax,0FEFFFFFFh
  0048f8e8  8906                  mov       [esi],eax
  0048f8ea  8d8100010000          lea       eax,[ecx+100h]
  0048f8f0  c1e006                shl       eax,6
  0048f8f3  0fbf5610              movsx     edx,word [esi+10h]
  0048f8f7  0faff8                imul      edi,eax
  0048f8fa  52                    push      edx
  0048f8fb  66c746180400          mov       word [esi+18h],4
  0048f901  66895e1a              mov       [esi+1Ah],bx
  0048f905  89465c                mov       [esi+5Ch],eax
  0048f908  c1ff0c                sar       edi,0Ch
  0048f90b  e80c610b00            call      00545A1Ch
  0048f910  0fafc7                imul      eax,edi
  0048f913  8b6e50                mov       ebp,[esi+50h]
  0048f916  c1f80c                sar       eax,0Ch
  0048f919  03e8                  add       ebp,eax
  0048f91b  0fbf4610              movsx     eax,word [esi+10h]
  0048f91f  50                    push      eax
  0048f920  896e50                mov       [esi+50h],ebp
  0048f923  e8ee600b00            call      00545A16h
  0048f928  0fafc7                imul      eax,edi
  0048f92b  8b5658                mov       edx,[esi+58h]
  0048f92e  8b4e54                mov       ecx,[esi+54h]
  0048f931  c1f80c                sar       eax,0Ch
  0048f934  03d0                  add       edx,eax
  0048f936  8b06                  mov       eax,[esi]
  0048f938  81c100e00100          add       ecx,1E000h
  0048f93e  0c01                  or        al,1
  0048f940  895658                mov       [esi+58h],edx
  0048f943  894e54                mov       [esi+54h],ecx
  0048f946  8906                  mov       [esi],eax
  0048f948  66c746120100          mov       word [esi+12h],1

LAB_0048f94e:
  0048f94e  f60601                test      byte [esi],1
  0048f951  0f852b020000          jne       near 0048FB82h
  0048f957  c6464300              mov       byte [esi+43h],0
  0048f95b  e922020000            jmp       0048FB82h
  0048f960  668b5612              mov       dx,[esi+12h]
  0048f964  bf01000000            mov       edi,1
  0048f969  663bd7                cmp       dx,di
  0048f96c  751c                  jne       short 0048F98Ah
  0048f96e  0fbf4610              movsx     eax,word [esi+10h]
  0048f972  39442414              cmp       [esp+14h],eax
  0048f976  7d06                  jge       short 0048F97Eh
  0048f978  c6464300              mov       byte [esi+43h],0
  0048f97c  eb0c                  jmp       short 0048F98Ah

LAB_0048f97e:
  0048f97e  8a4643                mov       al,[esi+43h]
  0048f981  84c0                  test      al,al
  0048f983  7405                  je        short 0048F98Ah
  0048f985  fec8                  dec       al
  0048f987  884643                mov       [esi+43h],al

LAB_0048f98a:
  0048f98a  663bd3                cmp       dx,bx
  0048f98d  0f85ef010000          jne       near 0048FB82h
  0048f993  66895e1a              mov       [esi+1Ah],bx
  0048f997  8b1e                  mov       ebx,[esi]
  0048f999  8d0cc9                lea       ecx,[ecx+ecx*8]
  0048f99c  81cb00000001          or        ebx,1000000h
  0048f9a2  c1e104                shl       ecx,4
  0048f9a5  81c100500000          add       ecx,5000h
  0048f9ab  66897e18              mov       [esi+18h],di
  0048f9af  891e                  mov       [esi],ebx
  0048f9b1  66897e12              mov       [esi+12h],di
  0048f9b5  894e5c                mov       [esi+5Ch],ecx
  0048f9b8  e9c5010000            jmp       0048FB82h
  0048f9bd  bf01000000            mov       edi,1
  0048f9c2  66397e12              cmp       [esi+12h],di
  0048f9c6  751f                  jne       short 0048F9E7h
  0048f9c8  66837e1a0a            cmp       word [esi+1Ah],0Ah
  0048f9cd  7518                  jne       short 0048F9E7h
  0048f9cf  66397e16              cmp       [esi+16h],di
  0048f9d3  7512                  jne       short 0048F9E7h
  0048f9d5  6aff                  push      0FFFFFFFFh
  0048f9d7  53                    push      ebx
  0048f9d8  6a36                  push      36h
  0048f9da  56                    push      esi
  0048f9db  e8f0affeff            call      0047A9D0h
  0048f9e0  8b4c2420              mov       ecx,[esp+20h]
  0048f9e4  83c410                add       esp,10h

LAB_0048f9e7:
  0048f9e7  8b06                  mov       eax,[esi]
  0048f9e9  a900000002            test      eax,2000000h
  0048f9ee  7404                  je        short 0048F9F4h
  0048f9f0  c6464300              mov       byte [esi+43h],0

LAB_0048f9f4:
  0048f9f4  66395e12              cmp       [esi+12h],bx
  0048f9f8  0f8584010000          jne       near 0048FB82h
  0048f9fe  25fffffffe            and       eax,0FEFFFFFFh
  0048fa03  83f91e                cmp       ecx,1Eh
  0048fa06  66897e12              mov       [esi+12h],di
  0048fa0a  66c746180600          mov       word [esi+18h],6
  0048fa10  66895e1a              mov       [esi+1Ah],bx
  0048fa14  8906                  mov       [esi],eax
  0048fa16  7d03                  jge       short 0048FA1Bh
  0048fa18  895e5c                mov       [esi+5Ch],ebx

LAB_0048fa1b:
  0048fa1b  66395e16              cmp       [esi+16h],bx
  0048fa1f  0f855d010000          jne       near 0048FB82h
  0048fa25  c6464300              mov       byte [esi+43h],0
  0048fa29  e954010000            jmp       0048FB82h
  0048fa2e  668b4e12              mov       cx,[esi+12h]
  0048fa32  6683f901              cmp       cx,1
  0048fa36  7511                  jne       short 0048FA49h
  0048fa38  8b06                  mov       eax,[esi]
  0048fa3a  a900000002            test      eax,2000000h
  0048fa3f  7408                  je        short 0048FA49h
  0048fa41  24fd                  and       al,0FDh
  0048fa43  c6464300              mov       byte [esi+43h],0
  0048fa47  8906                  mov       [esi],eax

LAB_0048fa49:
  0048fa49  663bcb                cmp       cx,bx
  0048fa4c  0f8530010000          jne       near 0048FB82h
  0048fa52  8b06                  mov       eax,[esi]
  0048fa54  66c746180300          mov       word [esi+18h],3
  0048fa5a  25fffffffc            and       eax,0FCFFFFFFh
  0048fa5f  66895e1a              mov       [esi+1Ah],bx
  0048fa63  8906                  mov       [esi],eax
  0048fa65  66c746120100          mov       word [esi+12h],1
  0048fa6b  e912010000            jmp       0048FB82h
  0048fa70  668b4e12              mov       cx,[esi+12h]
  0048fa74  6683f902              cmp       cx,2
  0048fa78  7511                  jne       short 0048FA8Bh
  0048fa7a  8b06                  mov       eax,[esi]
  0048fa7c  a900000002            test      eax,2000000h
  0048fa81  7408                  je        short 0048FA8Bh
  0048fa83  24fd                  and       al,0FDh
  0048fa85  c6464300              mov       byte [esi+43h],0
  0048fa89  8906                  mov       [esi],eax

LAB_0048fa8b:
  0048fa8b  6683f901              cmp       cx,1
  0048fa8f  7522                  jne       short 0048FAB3h
  0048fa91  8b06                  mov       eax,[esi]
  0048fa93  a900000002            test      eax,2000000h
  0048fa98  7419                  je        short 0048FAB3h
  0048fa9a  25fffffffe            and       eax,0FEFFFFFFh
  0048fa9f  66c746120200          mov       word [esi+12h],2
  0048faa5  8906                  mov       [esi],eax
  0048faa7  66c746180600          mov       word [esi+18h],6
  0048faad  66c7461a0700          mov       word [esi+1Ah],7

LAB_0048fab3:
  0048fab3  66395e12              cmp       [esi+12h],bx
  0048fab7  0f85c5000000          jne       near 0048FB82h
  0048fabd  8b16                  mov       edx,[esi]
  0048fabf  66c746120100          mov       word [esi+12h],1
  0048fac5  81e2fffffffe          and       edx,0FEFFFFFFh
  0048facb  66c746180300          mov       word [esi+18h],3
  0048fad1  83ca02                or        edx,2
  0048fad4  66895e1a              mov       [esi+1Ah],bx
  0048fad8  8916                  mov       [esi],edx
  0048fada  e9a0000000            jmp       0048FB7Fh
  0048fadf  668b5612              mov       dx,[esi+12h]
  0048fae3  6683fa02              cmp       dx,2
  0048fae7  751a                  jne       short 0048FB03h
  0048fae9  8b465c                mov       eax,[esi+5Ch]
  0048faec  8bf8                  mov       edi,eax
  0048faee  c1ff04                sar       edi,4
  0048faf1  2bc7                  sub       eax,edi
  0048faf3  89465c                mov       [esi+5Ch],eax
  0048faf6  8b06                  mov       eax,[esi]
  0048faf8  a900000002            test      eax,2000000h
  0048fafd  7404                  je        short 0048FB03h
  0048faff  c6464300              mov       byte [esi+43h],0

LAB_0048fb03:
  0048fb03  6683fa01              cmp       dx,1
  0048fb07  7557                  jne       short 0048FB60h
  0048fb09  8b442418              mov       eax,[esp+18h]
  0048fb0d  8d9000010000          lea       edx,[eax+100h]
  0048fb13  81e2ff0f0000          and       edx,0FFFh
  0048fb19  81fa00020000          cmp       edx,200h
  0048fb1f  7e20                  jle       short 0048FB41h
  0048fb21  25ff0f0000            and       eax,0FFFh
  0048fb26  3d00080000            cmp       eax,800h
  0048fb2b  7d06                  jge       short 0048FB33h
  0048fb2d  8d4c0932              lea       ecx,[ecx+ecx+32h]
  0048fb31  eb0a                  jmp       short 0048FB3Dh

LAB_0048fb33:
  0048fb33  8d0409                lea       eax,[ecx+ecx]
  0048fb36  b9ceffffff            mov       ecx,0FFFFFFCEh
  0048fb3b  2bc8                  sub       ecx,eax

LAB_0048fb3d:
  0048fb3d  66014e32              add       [esi+32h],cx

LAB_0048fb41:
  0048fb41  66837e1a0a            cmp       word [esi+1Ah],0Ah
  0048fb46  7c18                  jl        short 0048FB60h
  0048fb48  8b4e54                mov       ecx,[esi+54h]
  0048fb4b  8b06                  mov       eax,[esi]
  0048fb4d  81c100e00100          add       ecx,1E000h
  0048fb53  0c03                  or        al,3
  0048fb55  66c746120200          mov       word [esi+12h],2
  0048fb5b  894e54                mov       [esi+54h],ecx
  0048fb5e  8906                  mov       [esi],eax

LAB_0048fb60:
  0048fb60  66395e12              cmp       [esi+12h],bx
  0048fb64  751c                  jne       short 0048FB82h
  0048fb66  8b06                  mov       eax,[esi]
  0048fb68  66c746120100          mov       word [esi+12h],1
  0048fb6e  25fffffffe            and       eax,0FEFFFFFFh
  0048fb73  66c746180500          mov       word [esi+18h],5
  0048fb79  66895e1a              mov       [esi+1Ah],bx
  0048fb7d  8906                  mov       [esi],eax

LAB_0048fb7f:
  0048fb7f  895e5c                mov       [esi+5Ch],ebx

LAB_0048fb82:
  0048fb82  0fbf4e32              movsx     ecx,word [esi+32h]
  0048fb86  51                    push      ecx
  0048fb87  e8905e0b00            call      00545A1Ch
  0048fb8c  0fbf5632              movsx     edx,word [esi+32h]
  0048fb90  52                    push      edx
  0048fb91  8944247c              mov       [esp+7Ch],eax
  0048fb95  e87c5e0b00            call      00545A16h
  0048fb9a  8b5650                mov       edx,[esi+50h]
  0048fb9d  89842480000000        mov       [esp+80h],eax
  0048fba4  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  0048fbab  8bc2                  mov       eax,edx
  0048fbad  8bfa                  mov       edi,edx
  0048fbaf  c1f803                sar       eax,3
  0048fbb2  8b4c2420              mov       ecx,[esp+20h]
  0048fbb6  2bf8                  sub       edi,eax
  0048fbb8  c1fa08                sar       edx,8
  0048fbbb  0faf4e5c              imul      ecx,[esi+5Ch]
  0048fbbf  2bfa                  sub       edi,edx
  0048fbc1  8b5654                mov       edx,[esi+54h]
  0048fbc4  8bc5                  mov       eax,ebp
  0048fbc6  8bda                  mov       ebx,edx
  0048fbc8  c1f808                sar       eax,8
  0048fbcb  c1fb08                sar       ebx,8
  0048fbce  2bc3                  sub       eax,ebx
  0048fbd0  897e50                mov       [esi+50h],edi
  0048fbd3  2bc5                  sub       eax,ebp
  0048fbd5  56                    push      esi
  0048fbd6  03c2                  add       eax,edx
  0048fbd8  8b5658                mov       edx,[esi+58h]
  0048fbdb  8bda                  mov       ebx,edx
  0048fbdd  8bea                  mov       ebp,edx
  0048fbdf  c1fb08                sar       ebx,8
  0048fbe2  2beb                  sub       ebp,ebx
  0048fbe4  8b5e1c                mov       ebx,[esi+1Ch]
  0048fbe7  c1fa03                sar       edx,3
  0048fbea  c1f90c                sar       ecx,0Ch
  0048fbed  2bea                  sub       ebp,edx
  0048fbef  8bd1                  mov       edx,ecx
  0048fbf1  0faf54247c            imul      edx,[esp+7Ch]
  0048fbf6  0faf8c2484000000      imul      ecx,[esp+84h]
  0048fbfe  c1fa0c                sar       edx,0Ch
  0048fc01  c1f90c                sar       ecx,0Ch
  0048fc04  896e58                mov       [esi+58h],ebp
  0048fc07  03d7                  add       edx,edi
  0048fc09  8b7e20                mov       edi,[esi+20h]
  0048fc0c  03cd                  add       ecx,ebp
  0048fc0e  8b6e24                mov       ebp,[esi+24h]
  0048fc11  03da                  add       ebx,edx
  0048fc13  03f8                  add       edi,eax
  0048fc15  03e9                  add       ebp,ecx
  0048fc17  894654                mov       [esi+54h],eax
  0048fc1a  895e1c                mov       [esi+1Ch],ebx
  0048fc1d  897e20                mov       [esi+20h],edi
  0048fc20  896e24                mov       [esi+24h],ebp
  0048fc23  e8183d0a00            call      00533940h
  0048fc28  8b9690000000          mov       edx,[esi+90h]
  0048fc2e  8b4660                mov       eax,[esi+60h]
  0048fc31  8b4e20                mov       ecx,[esi+20h]
  0048fc34  83c404                add       esp,4
  0048fc37  80e6fc                and       dh,0FCh
  0048fc3a  3bc8                  cmp       ecx,eax
  0048fc3c  899690000000          mov       [esi+90h],edx
  0048fc42  7f2a                  jg        short 0048FC6Eh
  0048fc44  f6c680                test      dh,80h
  0048fc47  7525                  jne       short 0048FC6Eh
  0048fc49  8b0e                  mov       ecx,[esi]
  0048fc4b  894620                mov       [esi+20h],eax
  0048fc4e  f6c101                test      cl,1
  0048fc51  740f                  je        short 0048FC62h
  0048fc53  83e1fe                and       ecx,0FFFFFFFEh
  0048fc56  33db                  xor       ebx,ebx
  0048fc58  890e                  mov       [esi],ecx
  0048fc5a  895e54                mov       [esi+54h],ebx
  0048fc5d  e923010000            jmp       0048FD85h

LAB_0048fc62:
  0048fc62  83e1fe                and       ecx,0FFFFFFFEh
  0048fc65  33db                  xor       ebx,ebx
  0048fc67  890e                  mov       [esi],ecx
  0048fc69  895e54                mov       [esi+54h],ebx
  0048fc6c  eb02                  jmp       short 0048FC70h

LAB_0048fc6e:
  0048fc6e  33db                  xor       ebx,ebx

LAB_0048fc70:
  0048fc70  8b0e                  mov       ecx,[esi]
  0048fc72  f6c101                test      cl,1
  0048fc75  750a                  jne       short 0048FC81h
  0048fc77  394620                cmp       [esi+20h],eax
  0048fc7a  7e05                  jle       short 0048FC81h
  0048fc7c  83c901                or        ecx,1
  0048fc7f  890e                  mov       [esi],ecx

LAB_0048fc81:
  0048fc81  f7c282ad0020          test      edx,2000AD82h
  0048fc87  0f84f8000000          je        near 0048FD85h
  0048fc8d  f60601                test      byte [esi],1
  0048fc90  0f85c7000000          jne       near 0048FD5Dh
  0048fc96  807e4207              cmp       byte [esi+42h],7
  0048fc9a  7421                  je        short 0048FCBDh
  0048fc9c  c6464207              mov       byte [esi+42h],7
  0048fca0  e89b740b00            call      00547140h
  0048fca5  99                    cdq
  0048fca6  b928000000            mov       ecx,28h
  0048fcab  66895e12              mov       [esi+12h],bx
  0048fcaf  f7f9                  idiv      ecx
  0048fcb1  66c746105000          mov       word [esi+10h],50h
  0048fcb7  80c214                add       dl,14h
  0048fcba  885643                mov       [esi+43h],dl

LAB_0048fcbd:
  0048fcbd  8b16                  mov       edx,[esi]
  0048fcbf  8b44242c              mov       eax,[esp+2Ch]
  0048fcc3  8b4c2430              mov       ecx,[esp+30h]
  0048fcc7  83e2fd                and       edx,0FFFFFFFDh
  0048fcca  8916                  mov       [esi],edx
  0048fccc  8b542428              mov       edx,[esp+28h]
  0048fcd0  894620                mov       [esi+20h],eax
  0048fcd3  8b44246c              mov       eax,[esp+6Ch]
  0048fcd7  89561c                mov       [esi+1Ch],edx
  0048fcda  8b542464              mov       edx,[esp+64h]
  0048fcde  25ffff0000            and       eax,0FFFFh
  0048fce3  894e24                mov       [esi+24h],ecx
  0048fce6  8b4c244c              mov       ecx,[esp+4Ch]
  0048fcea  89567c                mov       [esi+7Ch],edx
  0048fced  8b542458              mov       edx,[esp+58h]
  0048fcf1  898690000000          mov       [esi+90h],eax
  0048fcf7  8b442460              mov       eax,[esp+60h]
  0048fcfb  898e84000000          mov       [esi+84h],ecx
  0048fd01  8b4c2450              mov       ecx,[esp+50h]
  0048fd05  898680000000          mov       [esi+80h],eax
  0048fd0b  8b442468              mov       eax,[esp+68h]
  0048fd0f  899688000000          mov       [esi+88h],edx
  0048fd15  8b542454              mov       edx,[esp+54h]
  0048fd19  6882ad0020            push      2000AD82h
  0048fd1e  56                    push      esi
  0048fd1f  89868c000000          mov       [esi+8Ch],eax
  0048fd25  894e64                mov       [esi+64h],ecx
  0048fd28  895660                mov       [esi+60h],edx
  0048fd2b  e830610400            call      004D5E60h
  0048fd30  83c408                add       esp,8
  0048fd33  663bc3                cmp       ax,bx
  0048fd36  7515                  jne       short 0048FD4Dh
  0048fd38  e803740b00            call      00547140h
  0048fd3d  99                    cdq
  0048fd3e  33c2                  xor       eax,edx
  0048fd40  2bc2                  sub       eax,edx
  0048fd42  83e001                and       eax,1
  0048fd45  33c2                  xor       eax,edx
  0048fd47  2bc2                  sub       eax,edx
  0048fd49  8d4400ff              lea       eax,[eax+eax-1]

LAB_0048fd4d:
  0048fd4d  8b4c2410              mov       ecx,[esp+10h]
  0048fd51  83c140                add       ecx,40h
  0048fd54  0fafc8                imul      ecx,eax
  0048fd57  66014e32              add       [esi+32h],cx
  0048fd5b  eb28                  jmp       short 0048FD85h

LAB_0048fd5d:
  0048fd5d  8b542428              mov       edx,[esp+28h]
  0048fd61  8b442430              mov       eax,[esp+30h]
  0048fd65  895e5c                mov       [esi+5Ch],ebx
  0048fd68  895e50                mov       [esi+50h],ebx
  0048fd6b  895e58                mov       [esi+58h],ebx
  0048fd6e  89561c                mov       [esi+1Ch],edx
  0048fd71  894624                mov       [esi+24h],eax
  0048fd74  885e42                mov       [esi+42h],bl
  0048fd77  c6464328              mov       byte [esi+43h],28h
  0048fd7b  66895e12              mov       [esi+12h],bx
  0048fd7f  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_0048fd85:
  0048fd85  6aff                  push      0FFFFFFFFh
  0048fd87  6aff                  push      0FFFFFFFFh
  0048fd89  6aff                  push      0FFFFFFFFh
  0048fd8b  56                    push      esi
  0048fd8c  e87f3a0300            call      004C3810h
  0048fd91  8b442430              mov       eax,[esp+30h]
  0048fd95  83c410                add       esp,10h
  0048fd98  3bc3                  cmp       eax,ebx
  0048fd9a  7517                  jne       short 0048FDB3h
  0048fd9c  668b8c248e000000      mov       cx,[esp+8Eh]
  0048fda4  66894e32              mov       [esi+32h],cx
  0048fda8  5f                    pop       edi
  0048fda9  5e                    pop       esi
  0048fdaa  5d                    pop       ebp
  0048fdab  5b                    pop       ebx
  0048fdac  81c490000000          add       esp,90h
  0048fdb2  c3                    ret

LAB_0048fdb3:
  0048fdb3  385e43                cmp       [esi+43h],bl
  0048fdb6  0f856a030000          jne       near 00490126h
  0048fdbc  8b466c                mov       eax,[esi+6Ch]
  0048fdbf  8b561c                mov       edx,[esi+1Ch]
  0048fdc2  8b781c                mov       edi,[eax+1Ch]
  0048fdc5  8b6824                mov       ebp,[eax+24h]
  0048fdc8  8b4624                mov       eax,[esi+24h]
  0048fdcb  2bfa                  sub       edi,edx
  0048fdcd  2be8                  sub       ebp,eax
  0048fdcf  c1ff0c                sar       edi,0Ch
  0048fdd2  c1fd0c                sar       ebp,0Ch
  0048fdd5  8bd7                  mov       edx,edi
  0048fdd7  8bc5                  mov       eax,ebp
  0048fdd9  0fafd7                imul      edx,edi
  0048fddc  0fafc5                imul      eax,ebp
  0048fddf  03d0                  add       edx,eax
  0048fde1  52                    push      edx
  0048fde2  e8295c0b00            call      00545A10h
  0048fde7  55                    push      ebp
  0048fde8  57                    push      edi
  0048fde9  8944241c              mov       [esp+1Ch],eax
  0048fded  e8305c0b00            call      00545A22h
  0048fdf2  668b4e32              mov       cx,[esi+32h]
  0048fdf6  66895e12              mov       [esi+12h],bx
  0048fdfa  2bc1                  sub       eax,ecx
  0048fdfc  25ff0f0000            and       eax,0FFFh
  0048fe01  89442418              mov       [esp+18h],eax
  0048fe05  8b442414              mov       eax,[esp+14h]
  0048fe09  3dfa000000            cmp       eax,0FAh
  0048fe0e  0f8dd9000000          jge       near 0048FEEDh
  0048fe14  e827730b00            call      00547140h
  0048fe19  99                    cdq
  0048fe1a  b964000000            mov       ecx,64h
  0048fe1f  f7f9                  idiv      ecx
  0048fe21  8bea                  mov       ebp,edx
  0048fe23  83fd32                cmp       ebp,32h
  0048fe26  7d77                  jge       short 0048FE9Fh
  0048fe28  8b542418              mov       edx,[esp+18h]
  0048fe2c  8b7c241c              mov       edi,[esp+1Ch]
  0048fe30  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0048fe37  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048fe3e  25ff0f0000            and       eax,0FFFh
  0048fe43  3bc1                  cmp       eax,ecx
  0048fe45  7f4e                  jg        short 0048FE95h
  0048fe47  e8f4720b00            call      00547140h
  0048fe4c  99                    cdq
  0048fe4d  8d4c3f01              lea       ecx,[edi+edi+1]
  0048fe51  f7f9                  idiv      ecx
  0048fe53  8b442414              mov       eax,[esp+14h]
  0048fe57  83c264                add       edx,64h
  0048fe5a  3bc2                  cmp       eax,edx
  0048fe5c  7d37                  jge       short 0048FE95h
  0048fe5e  83fd1e                cmp       ebp,1Eh
  0048fe61  7d24                  jge       short 0048FE87h
  0048fe63  8b16                  mov       edx,[esi]
  0048fe65  c646420b              mov       byte [esi+42h],0Bh
  0048fe69  81e2fffffffe          and       edx,0FEFFFFFFh
  0048fe6f  c6464301              mov       byte [esi+43h],1
  0048fe73  83ca02                or        edx,2
  0048fe76  895e5c                mov       [esi+5Ch],ebx
  0048fe79  8916                  mov       [esi],edx
  0048fe7b  66c746180300          mov       word [esi+18h],3
  0048fe81  66895e1a              mov       [esi+1Ah],bx
  0048fe85  eb6a                  jmp       short 0048FEF1h

LAB_0048fe87:
  0048fe87  c646420c              mov       byte [esi+42h],0Ch
  0048fe8b  c6464301              mov       byte [esi+43h],1
  0048fe8f  66895e12              mov       [esi+12h],bx
  0048fe93  eb5c                  jmp       short 0048FEF1h

LAB_0048fe95:
  0048fe95  c6464201              mov       byte [esi+42h],1
  0048fe99  c6464301              mov       byte [esi+43h],1
  0048fe9d  eb52                  jmp       short 0048FEF1h

LAB_0048fe9f:
  0048fe9f  83fd3c                cmp       ebp,3Ch
  0048fea2  7d29                  jge       short 0048FECDh
  0048fea4  c6464202              mov       byte [esi+42h],2
  0048fea8  e893720b00            call      00547140h
  0048fead  99                    cdq
  0048feae  b90a000000            mov       ecx,0Ah
  0048feb3  8b7c241c              mov       edi,[esp+1Ch]
  0048feb7  f7f9                  idiv      ecx
  0048feb9  8bc7                  mov       eax,edi
  0048febb  66c746105000          mov       word [esi+10h],50h
  0048fec1  d1f8                  sar       eax,1
  0048fec3  02d0                  add       dl,al
  0048fec5  80c214                add       dl,14h
  0048fec8  885643                mov       [esi+43h],dl
  0048fecb  eb24                  jmp       short 0048FEF1h

LAB_0048fecd:
  0048fecd  83fd5a                cmp       ebp,5Ah
  0048fed0  0f8d5b020000          jge       near 00490131h
  0048fed6  837c241464            cmp       dword [esp+14h],64h
  0048fedb  0f8d50020000          jge       near 00490131h
  0048fee1  c646420d              mov       byte [esi+42h],0Dh
  0048fee5  c6464301              mov       byte [esi+43h],1
  0048fee9  66895e12              mov       [esi+12h],bx

LAB_0048feed:
  0048feed  8b7c241c              mov       edi,[esp+1Ch]

LAB_0048fef1:
  0048fef1  8b442414              mov       eax,[esp+14h]
  0048fef5  3d58020000            cmp       eax,258h
  0048fefa  0f8dea000000          jge       near 0048FFEAh
  0048ff00  3dfa000000            cmp       eax,0FAh
  0048ff05  0f8cdf000000          jl        near 0048FFEAh
  0048ff0b  e830720b00            call      00547140h
  0048ff10  99                    cdq
  0048ff11  b964000000            mov       ecx,64h
  0048ff16  f7f9                  idiv      ecx
  0048ff18  83fa14                cmp       edx,14h
  0048ff1b  7d40                  jge       short 0048FF5Dh
  0048ff1d  8b542418              mov       edx,[esp+18h]
  0048ff21  81c200040000          add       edx,400h
  0048ff27  81e2ff0f0000          and       edx,0FFFh
  0048ff2d  81fa00080000          cmp       edx,800h
  0048ff33  7c0d                  jl        short 0048FF42h
  0048ff35  c6464201              mov       byte [esi+42h],1
  0048ff39  c6464301              mov       byte [esi+43h],1
  0048ff3d  e9a8000000            jmp       0048FFEAh

LAB_0048ff42:
  0048ff42  c6464207              mov       byte [esi+42h],7
  0048ff46  e8f5710b00            call      00547140h
  0048ff4b  99                    cdq
  0048ff4c  b914000000            mov       ecx,14h
  0048ff51  f7f9                  idiv      ecx
  0048ff53  02d1                  add       dl,cl
  0048ff55  885643                mov       [esi+43h],dl
  0048ff58  e989000000            jmp       0048FFE6h

LAB_0048ff5d:
  0048ff5d  83fa28                cmp       edx,28h
  0048ff60  7d25                  jge       short 0048FF87h
  0048ff62  c6464202              mov       byte [esi+42h],2
  0048ff66  e8d5710b00            call      00547140h
  0048ff6b  99                    cdq
  0048ff6c  b90a000000            mov       ecx,0Ah
  0048ff71  66c746105000          mov       word [esi+10h],50h
  0048ff77  f7f9                  idiv      ecx
  0048ff79  8bc7                  mov       eax,edi
  0048ff7b  d1f8                  sar       eax,1
  0048ff7d  02d0                  add       dl,al
  0048ff7f  80c214                add       dl,14h
  0048ff82  885643                mov       [esi+43h],dl
  0048ff85  eb63                  jmp       short 0048FFEAh

LAB_0048ff87:
  0048ff87  83fa3c                cmp       edx,3Ch
  0048ff8a  7d13                  jge       short 0048FF9Fh
  0048ff8c  c6464206              mov       byte [esi+42h],6
  0048ff90  c6464301              mov       byte [esi+43h],1
  0048ff94  e8a7710b00            call      00547140h
  0048ff99  66894610              mov       [esi+10h],ax
  0048ff9d  eb47                  jmp       short 0048FFE6h

LAB_0048ff9f:
  0048ff9f  83fa50                cmp       edx,50h
  0048ffa2  7d3a                  jge       short 0048FFDEh
  0048ffa4  c6464203              mov       byte [esi+42h],3
  0048ffa8  66895e12              mov       [esi+12h],bx
  0048ffac  e88f710b00            call      00547140h
  0048ffb1  99                    cdq
  0048ffb2  b90a000000            mov       ecx,0Ah
  0048ffb7  f7f9                  idiv      ecx
  0048ffb9  8bc7                  mov       eax,edi
  0048ffbb  d1f8                  sar       eax,1
  0048ffbd  02d0                  add       dl,al
  0048ffbf  80c214                add       dl,14h
  0048ffc2  885643                mov       [esi+43h],dl
  0048ffc5  e876710b00            call      00547140h
  0048ffca  99                    cdq
  0048ffcb  33c2                  xor       eax,edx
  0048ffcd  2bc2                  sub       eax,edx
  0048ffcf  25ff0f0000            and       eax,0FFFh
  0048ffd4  33c2                  xor       eax,edx
  0048ffd6  2bc2                  sub       eax,edx
  0048ffd8  66894610              mov       [esi+10h],ax
  0048ffdc  eb0c                  jmp       short 0048FFEAh

LAB_0048ffde:
  0048ffde  c646420a              mov       byte [esi+42h],0Ah
  0048ffe2  c6464301              mov       byte [esi+43h],1

LAB_0048ffe6:
  0048ffe6  66895e12              mov       [esi+12h],bx

LAB_0048ffea:
  0048ffea  817c241458020000      cmp       dword [esp+14h],258h
  0048fff2  0f8c2e010000          jl        near 00490126h
  0048fff8  e843710b00            call      00547140h
  0048fffd  bd64000000            mov       ebp,64h
  00490002  99                    cdq
  00490003  8bcd                  mov       ecx,ebp
  00490005  f7f9                  idiv      ecx
  00490007  83fa1e                cmp       edx,1Eh
  0049000a  7d45                  jge       short 00490051h
  0049000c  8b442410              mov       eax,[esp+10h]
  00490010  8d4c000a              lea       ecx,[eax+eax+0Ah]
  00490014  3bd1                  cmp       edx,ecx
  00490016  0f8ccd000000          jl        near 004900E9h
  0049001c  885e42                mov       [esi+42h],bl
  0049001f  e81c710b00            call      00547140h
  00490024  99                    cdq
  00490025  b90a000000            mov       ecx,0Ah
  0049002a  66895e12              mov       [esi+12h],bx
  0049002e  f7f9                  idiv      ecx
  00490030  8b4c2410              mov       ecx,[esp+10h]
  00490034  8bc7                  mov       eax,edi
  00490036  c1f802                sar       eax,2
  00490039  02d0                  add       dl,al
  0049003b  885643                mov       [esi+43h],dl
  0049003e  8d540950              lea       edx,[ecx+ecx+50h]
  00490042  66895610              mov       [esi+10h],dx
  00490046  5f                    pop       edi
  00490047  5e                    pop       esi
  00490048  5d                    pop       ebp
  00490049  5b                    pop       ebx
  0049004a  81c490000000          add       esp,90h
  00490050  c3                    ret

LAB_00490051:
  00490051  83fa28                cmp       edx,28h
  00490054  7d43                  jge       short 00490099h
  00490056  c6464203              mov       byte [esi+42h],3
  0049005a  66895e12              mov       [esi+12h],bx
  0049005e  e8dd700b00            call      00547140h
  00490063  99                    cdq
  00490064  b90a000000            mov       ecx,0Ah
  00490069  f7f9                  idiv      ecx
  0049006b  8bc7                  mov       eax,edi
  0049006d  d1f8                  sar       eax,1
  0049006f  02d0                  add       dl,al
  00490071  80c214                add       dl,14h
  00490074  885643                mov       [esi+43h],dl
  00490077  e8c4700b00            call      00547140h
  0049007c  99                    cdq
  0049007d  33c2                  xor       eax,edx
  0049007f  2bc2                  sub       eax,edx
  00490081  25ff0f0000            and       eax,0FFFh
  00490086  33c2                  xor       eax,edx
  00490088  2bc2                  sub       eax,edx
  0049008a  66894610              mov       [esi+10h],ax
  0049008e  5f                    pop       edi
  0049008f  5e                    pop       esi
  00490090  5d                    pop       ebp
  00490091  5b                    pop       ebx
  00490092  81c490000000          add       esp,90h
  00490098  c3                    ret

LAB_00490099:
  00490099  83fa3c                cmp       edx,3Ch
  0049009c  7d13                  jge       short 004900B1h
  0049009e  c6464201              mov       byte [esi+42h],1
  004900a2  c6464301              mov       byte [esi+43h],1
  004900a6  5f                    pop       edi
  004900a7  5e                    pop       esi
  004900a8  5d                    pop       ebp
  004900a9  5b                    pop       ebx
  004900aa  81c490000000          add       esp,90h
  004900b0  c3                    ret

LAB_004900b1:
  004900b1  83fa50                cmp       edx,50h
  004900b4  7d5f                  jge       short 00490115h
  004900b6  8b4c2418              mov       ecx,[esp+18h]
  004900ba  8d04fd00020000        lea       eax,[edi*8+200h]
  004900c1  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004900c8  81e2ff0f0000          and       edx,0FFFh
  004900ce  3bd0                  cmp       edx,eax
  004900d0  7f17                  jg        short 004900E9h
  004900d2  c646420a              mov       byte [esi+42h],0Ah
  004900d6  c6464301              mov       byte [esi+43h],1
  004900da  66895e12              mov       [esi+12h],bx
  004900de  5f                    pop       edi
  004900df  5e                    pop       esi
  004900e0  5d                    pop       ebp
  004900e1  5b                    pop       ebx
  004900e2  81c490000000          add       esp,90h
  004900e8  c3                    ret

LAB_004900e9:
  004900e9  c6464202              mov       byte [esi+42h],2
  004900ed  e84e700b00            call      00547140h
  004900f2  99                    cdq
  004900f3  b90a000000            mov       ecx,0Ah
  004900f8  66896e10              mov       [esi+10h],bp
  004900fc  f7f9                  idiv      ecx
  004900fe  8bc7                  mov       eax,edi
  00490100  d1f8                  sar       eax,1
  00490102  02d0                  add       dl,al
  00490104  80c214                add       dl,14h
  00490107  885643                mov       [esi+43h],dl
  0049010a  5f                    pop       edi
  0049010b  5e                    pop       esi
  0049010c  5d                    pop       ebp
  0049010d  5b                    pop       ebx
  0049010e  81c490000000          add       esp,90h
  00490114  c3                    ret

LAB_00490115:
  00490115  c6464206              mov       byte [esi+42h],6
  00490119  c6464301              mov       byte [esi+43h],1
  0049011d  e81e700b00            call      00547140h
  00490122  66894610              mov       [esi+10h],ax

LAB_00490126:
  00490126  5f                    pop       edi
  00490127  5e                    pop       esi
  00490128  5d                    pop       ebp
  00490129  5b                    pop       ebx
  0049012a  81c490000000          add       esp,90h
  00490130  c3                    ret

LAB_00490131:
  00490131  c6464203              mov       byte [esi+42h],3
  00490135  66895e12              mov       [esi+12h],bx
  00490139  e802700b00            call      00547140h
  0049013e  99                    cdq
  0049013f  b90a000000            mov       ecx,0Ah
  00490144  8b7c241c              mov       edi,[esp+1Ch]
  00490148  f7f9                  idiv      ecx
  0049014a  8bc7                  mov       eax,edi
  0049014c  d1f8                  sar       eax,1
  0049014e  02d0                  add       dl,al
  00490150  80c214                add       dl,14h
  00490153  885643                mov       [esi+43h],dl
  00490156  e8e56f0b00            call      00547140h
  0049015b  99                    cdq
  0049015c  33c2                  xor       eax,edx
  0049015e  2bc2                  sub       eax,edx
  00490160  25ff0f0000            and       eax,0FFFh
  00490165  33c2                  xor       eax,edx
  00490167  2bc2                  sub       eax,edx
  00490169  66894610              mov       [esi+10h],ax
  0049016d  e97ffdffff            jmp       0048FEF1h
  00490172  8bff                  mov       edi,edi
  00490174  ad                    lodsd
  00490175  f64800f5              test      byte [eax],0F5h
  00490179  f648005f              test      byte [eax],5Fh
  0049017d  f74800fcf74800        test      dword [eax],48F7FCh
  00490184  82fb48                cmp       bl,48h
  00490187  0025fa4800cd          add       [0CD0048FAh],ah
  0049018d  f8                    clc
  0049018e  48                    dec       eax
  0049018f  0060f9                add       [eax-7],ah
  00490192  48                    dec       eax
  00490193  0082fb480082          add       [edx-7DFFB705h],al
  00490199  fb                    sti
  0049019a  48                    dec       eax
  0049019b  00bdf948002e          add       [ebp+2E0048F9h],bh
  004901a1  fa                    cli
  004901a2  48                    dec       eax
  004901a3  0070fa                add       [eax-6],dh
  004901a6  48                    dec       eax
  004901a7  00df                  add       bh,bl
  004901a9  fa                    cli
  004901aa  48                    dec       eax
  004901ab  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
