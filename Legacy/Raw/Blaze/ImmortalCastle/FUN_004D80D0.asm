C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d80d0  section .text  offset fichier 0xd74d0  3612 octets

  004d80d0  81ec80000000          sub       esp,80h
  004d80d6  53                    push      ebx
  004d80d7  55                    push      ebp
  004d80d8  56                    push      esi
  004d80d9  8bb42490000000        mov       esi,[esp+90h]
  004d80e0  b864000000            mov       eax,64h
  004d80e5  33db                  xor       ebx,ebx
  004d80e7  6689442452            mov       [esp+52h],ax
  004d80ec  6689442456            mov       [esp+56h],ax
  004d80f1  8a4641                mov       al,[esi+41h]
  004d80f4  57                    push      edi
  004d80f5  84c0                  test      al,al
  004d80f7  66895c2450            mov       [esp+50h],bx
  004d80fc  66c74424521e00        mov       word [esp+52h],1Eh
  004d8103  66c74424543200        mov       word [esp+54h],32h
  004d810a  66895c2458            mov       [esp+58h],bx
  004d810f  7510                  jne       short 004D8121h
  004d8111  53                    push      ebx
  004d8112  6a03                  push      3
  004d8114  56                    push      esi
  004d8115  c6464101              mov       byte [esi+41h],1
  004d8119  e802d9ffff            call      004D5A20h
  004d811e  83c40c                add       esp,0Ch

LAB_004d8121:
  004d8121  8b461c                mov       eax,[esi+1Ch]
  004d8124  8b4e20                mov       ecx,[esi+20h]
  004d8127  8b5624                mov       edx,[esi+24h]
  004d812a  8d6e1c                lea       ebp,[esi+1Ch]
  004d812d  8944245c              mov       [esp+5Ch],eax
  004d8131  8b4664                mov       eax,[esi+64h]
  004d8134  894c2460              mov       [esp+60h],ecx
  004d8138  8b4e60                mov       ecx,[esi+60h]
  004d813b  89542464              mov       [esp+64h],edx
  004d813f  668b5632              mov       dx,[esi+32h]
  004d8143  89442440              mov       [esp+40h],eax
  004d8147  8b467c                mov       eax,[esi+7Ch]
  004d814a  894c2444              mov       [esp+44h],ecx
  004d814e  668b8e90000000        mov       cx,[esi+90h]
  004d8155  668954247e            mov       [esp+7Eh],dx
  004d815a  8b9680000000          mov       edx,[esi+80h]
  004d8160  89442428              mov       [esp+28h],eax
  004d8164  8b8684000000          mov       eax,[esi+84h]
  004d816a  66894c242c            mov       [esp+2Ch],cx
  004d816f  8b8e88000000          mov       ecx,[esi+88h]
  004d8175  8b7e6c                mov       edi,[esi+6Ch]
  004d8178  89542430              mov       [esp+30h],edx
  004d817c  8b968c000000          mov       edx,[esi+8Ch]
  004d8182  89442434              mov       [esp+34h],eax
  004d8186  894c2438              mov       [esp+38h],ecx
  004d818a  8d442420              lea       eax,[esp+20h]
  004d818e  8954243c              mov       [esp+3Ch],edx
  004d8192  8d4c2448              lea       ecx,[esp+48h]
  004d8196  50                    push      eax
  004d8197  8d542450              lea       edx,[esp+50h]
  004d819b  51                    push      ecx
  004d819c  52                    push      edx
  004d819d  56                    push      esi
  004d819e  e8dd070600            call      00538980h
  004d81a3  83c410                add       esp,10h
  004d81a6  8d442424              lea       eax,[esp+24h]
  004d81aa  8d4c2418              lea       ecx,[esp+18h]
  004d81ae  8d54241c              lea       edx,[esp+1Ch]
  004d81b2  50                    push      eax
  004d81b3  51                    push      ecx
  004d81b4  8d44241c              lea       eax,[esp+1Ch]
  004d81b8  52                    push      edx
  004d81b9  8d4c241c              lea       ecx,[esp+1Ch]
  004d81bd  50                    push      eax
  004d81be  51                    push      ecx
  004d81bf  56                    push      esi
  004d81c0  e85bdd0500            call      00535F20h
  004d81c5  8b54243c              mov       edx,[esp+3Ch]
  004d81c9  83c418                add       esp,18h
  004d81cc  c744241040000000      mov       dword [esp+10h],40h
  004d81d4  52                    push      edx
  004d81d5  56                    push      esi
  004d81d6  e805dcffff            call      004D5DE0h
  004d81db  8b4500                mov       eax,[ebp]
  004d81de  8b571c                mov       edx,[edi+1Ch]
  004d81e1  8b4e20                mov       ecx,[esi+20h]
  004d81e4  2bc2                  sub       eax,edx
  004d81e6  8b5720                mov       edx,[edi+20h]
  004d81e9  83c408                add       esp,8
  004d81ec  2bca                  sub       ecx,edx
  004d81ee  8b5624                mov       edx,[esi+24h]
  004d81f1  2b5724                sub       edx,[edi+24h]
  004d81f4  c1f80c                sar       eax,0Ch
  004d81f7  8bf8                  mov       edi,eax
  004d81f9  c1f90c                sar       ecx,0Ch
  004d81fc  0faff8                imul      edi,eax
  004d81ff  8bc1                  mov       eax,ecx
  004d8201  c1fa0c                sar       edx,0Ch
  004d8204  0fafc1                imul      eax,ecx
  004d8207  8bca                  mov       ecx,edx
  004d8209  03f8                  add       edi,eax
  004d820b  0fafca                imul      ecx,edx
  004d820e  03f9                  add       edi,ecx
  004d8210  57                    push      edi
  004d8211  e8fad70600            call      00545A10h
  004d8216  89442414              mov       [esp+14h],eax
  004d821a  8b442418              mov       eax,[esp+18h]
  004d821e  3bc3                  cmp       eax,ebx
  004d8220  0f844c070000          je        near 004D8972h
  004d8226  8b1e                  mov       ebx,[esi]
  004d8228  33d2                  xor       edx,edx
  004d822a  8a5604                mov       dl,[esi+4]
  004d822d  81e3fffffffd          and       ebx,0FDFFFFFFh
  004d8233  0fbf7e18              movsx     edi,word [esi+18h]
  004d8237  891e                  mov       [esi],ebx
  004d8239  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004d8240  8bcb                  mov       ecx,ebx
  004d8242  8b14ba                mov       edx,[edx+edi*4]
  004d8245  f7c100000001          test      ecx,1000000h
  004d824b  741d                  je        short 004D826Ah
  004d824d  66ff461a              inc       word [esi+1Ah]
  004d8251  668b4e1a              mov       cx,[esi+1Ah]
  004d8255  33ff                  xor       edi,edi
  004d8257  668b7a16              mov       di,[edx+16h]
  004d825b  0fbfc9                movsx     ecx,cx
  004d825e  3bcf                  cmp       ecx,edi
  004d8260  7c2a                  jl        short 004D828Ch
  004d8262  66c7461a0000          mov       word [esi+1Ah],0
  004d8268  eb22                  jmp       short 004D828Ch

LAB_004d826a:
  004d826a  66ff461a              inc       word [esi+1Ah]
  004d826e  668b7e1a              mov       di,[esi+1Ah]
  004d8272  33db                  xor       ebx,ebx
  004d8274  668b5a16              mov       bx,[edx+16h]
  004d8278  0fbfd7                movsx     edx,di
  004d827b  3bd3                  cmp       edx,ebx
  004d827d  7c0d                  jl        short 004D828Ch
  004d827f  4f                    dec       edi
  004d8280  81c900000002          or        ecx,2000000h
  004d8286  66897e1a              mov       [esi+1Ah],di
  004d828a  890e                  mov       [esi],ecx

LAB_004d828c:
  004d828c  f60602                test      byte [esi],2
  004d828f  0f84f6010000          je        near 004D848Bh
  004d8295  8a4e42                mov       cl,[esi+42h]
  004d8298  80f90c                cmp       cl,0Ch
  004d829b  0f848b000000          je        near 004D832Ch
  004d82a1  80f90d                cmp       cl,0Dh
  004d82a4  0f8482000000          je        near 004D832Ch
  004d82aa  bb0b000000            mov       ebx,0Bh
  004d82af  66395e1a              cmp       [esi+1Ah],bx
  004d82b3  7558                  jne       short 004D830Dh
  004d82b5  8d442450              lea       eax,[esp+50h]
  004d82b9  6a00                  push      0
  004d82bb  50                    push      eax
  004d82bc  55                    push      ebp
  004d82bd  56                    push      esi
  004d82be  e86d190400            call      00519C30h
  004d82c3  8bf8                  mov       edi,eax
  004d82c5  a1b8e05d00            mov       eax,[5DE0B8h]
  004d82ca  83c410                add       esp,10h
  004d82cd  85c0                  test      eax,eax
  004d82cf  7438                  je        short 004D8309h
  004d82d1  66395e1a              cmp       [esi+1Ah],bx
  004d82d5  7532                  jne       short 004D8309h
  004d82d7  8b1f                  mov       ebx,[edi]
  004d82d9  57                    push      edi
  004d82da  83cb40                or        ebx,40h
  004d82dd  56                    push      esi
  004d82de  66c787960000001000    mov       word [edi+96h],10h
  004d82e7  891f                  mov       [edi],ebx
  004d82e9  e892180600            call      00539B80h
  004d82ee  83c408                add       esp,8
  004d82f1  83c71c                add       edi,1Ch
  004d82f4  57                    push      edi
  004d82f5  6800180000            push      1800h
  004d82fa  6800030000            push      300h
  004d82ff  6a03                  push      3
  004d8301  e81a580600            call      0053DB20h
  004d8306  83c410                add       esp,10h

LAB_004d8309:
  004d8309  8b442418              mov       eax,[esp+18h]

LAB_004d830d:
  004d830d  8b0e                  mov       ecx,[esi]
  004d830f  f7c100000002          test      ecx,2000000h
  004d8315  7415                  je        short 004D832Ch
  004d8317  f6c101                test      cl,1
  004d831a  750b                  jne       short 004D8327h
  004d831c  83e1fd                and       ecx,0FFFFFFFDh
  004d831f  c6464300              mov       byte [esi+43h],0
  004d8323  890e                  mov       [esi],ecx
  004d8325  eb05                  jmp       short 004D832Ch

LAB_004d8327:
  004d8327  83e1fd                and       ecx,0FFFFFFFDh
  004d832a  890e                  mov       [esi],ecx

LAB_004d832c:
  004d832c  807e420c              cmp       byte [esi+42h],0Ch
  004d8330  0f85c5000000          jne       near 004D83FBh
  004d8336  668b4e18              mov       cx,[esi+18h]
  004d833a  bb12000000            mov       ebx,12h
  004d833f  6683f903              cmp       cx,3
  004d8343  7507                  jne       short 004D834Ch
  004d8345  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004d834a  7414                  je        short 004D8360h

LAB_004d834c:
  004d834c  6683f905              cmp       cx,5
  004d8350  0f85a5000000          jne       near 004D83FBh
  004d8356  66395e1a              cmp       [esi+1Ah],bx
  004d835a  0f859b000000          jne       near 004D83FBh

LAB_004d8360:
  004d8360  8d4c2450              lea       ecx,[esp+50h]
  004d8364  6a00                  push      0
  004d8366  51                    push      ecx
  004d8367  55                    push      ebp
  004d8368  56                    push      esi
  004d8369  e8c2180400            call      00519C30h
  004d836e  8bf8                  mov       edi,eax
  004d8370  a1b8e05d00            mov       eax,[5DE0B8h]
  004d8375  83c410                add       esp,10h
  004d8378  85c0                  test      eax,eax
  004d837a  747b                  je        short 004D83F7h
  004d837c  668b4618              mov       ax,[esi+18h]
  004d8380  663d0300              cmp       ax,3
  004d8384  7507                  jne       short 004D838Dh
  004d8386  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004d838b  740c                  je        short 004D8399h

LAB_004d838d:
  004d838d  663d0500              cmp       ax,5
  004d8391  7564                  jne       short 004D83F7h
  004d8393  66395e1a              cmp       [esi+1Ah],bx
  004d8397  755e                  jne       short 004D83F7h

LAB_004d8399:
  004d8399  663d0300              cmp       ax,3
  004d839d  750b                  jne       short 004D83AAh
  004d839f  66c787960000000200    mov       word [edi+96h],2
  004d83a8  eb09                  jmp       short 004D83B3h

LAB_004d83aa:
  004d83aa  66c787960000001000    mov       word [edi+96h],10h

LAB_004d83b3:
  004d83b3  8b1f                  mov       ebx,[edi]
  004d83b5  57                    push      edi
  004d83b6  83cb40                or        ebx,40h
  004d83b9  56                    push      esi
  004d83ba  891f                  mov       [edi],ebx
  004d83bc  8b4c2428              mov       ecx,[esp+28h]
  004d83c0  668b4130              mov       ax,[ecx+30h]
  004d83c4  0fbfd8                movsx     ebx,ax
  004d83c7  66c1f802              sar       ax,2
  004d83cb  66894130              mov       [ecx+30h],ax
  004d83cf  e8ac170600            call      00539B80h
  004d83d4  8b542428              mov       edx,[esp+28h]
  004d83d8  83c408                add       esp,8
  004d83db  83c71c                add       edi,1Ch
  004d83de  57                    push      edi
  004d83df  6800180000            push      1800h
  004d83e4  6800030000            push      300h
  004d83e9  6a03                  push      3
  004d83eb  66895a30              mov       [edx+30h],bx
  004d83ef  e82c570600            call      0053DB20h
  004d83f4  83c410                add       esp,10h

LAB_004d83f7:
  004d83f7  8b442418              mov       eax,[esp+18h]

LAB_004d83fb:
  004d83fb  807e420d              cmp       byte [esi+42h],0Dh
  004d83ff  0f8586000000          jne       near 004D848Bh
  004d8405  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004d840a  7c7f                  jl        short 004D848Bh
  004d840c  8d442450              lea       eax,[esp+50h]
  004d8410  6a00                  push      0
  004d8412  50                    push      eax
  004d8413  55                    push      ebp
  004d8414  56                    push      esi
  004d8415  e816180400            call      00519C30h
  004d841a  8bf8                  mov       edi,eax
  004d841c  a1b8e05d00            mov       eax,[5DE0B8h]
  004d8421  83c410                add       esp,10h
  004d8424  85c0                  test      eax,eax
  004d8426  745f                  je        short 004D8487h
  004d8428  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004d842d  7558                  jne       short 004D8487h
  004d842f  8b1f                  mov       ebx,[edi]
  004d8431  66c787960000001000    mov       word [edi+96h],10h
  004d843a  83cb40                or        ebx,40h
  004d843d  57                    push      edi
  004d843e  891f                  mov       [edi],ebx
  004d8440  8b4c2424              mov       ecx,[esp+24h]
  004d8444  56                    push      esi
  004d8445  668b4130              mov       ax,[ecx+30h]
  004d8449  0fbfd8                movsx     ebx,ax
  004d844c  66d1f8                sar       ax,1
  004d844f  66894130              mov       [ecx+30h],ax
  004d8453  e828170600            call      00539B80h
  004d8458  8b4c2428              mov       ecx,[esp+28h]
  004d845c  83c408                add       esp,8
  004d845f  6a00                  push      0
  004d8461  57                    push      edi
  004d8462  56                    push      esi
  004d8463  66895930              mov       [ecx+30h],bx
  004d8467  e8c40a0000            call      004D8F30h
  004d846c  83c40c                add       esp,0Ch
  004d846f  83c71c                add       edi,1Ch
  004d8472  57                    push      edi
  004d8473  6800180000            push      1800h
  004d8478  6800030000            push      300h
  004d847d  6a03                  push      3
  004d847f  e89c560600            call      0053DB20h
  004d8484  83c410                add       esp,10h

LAB_004d8487:
  004d8487  8b442418              mov       eax,[esp+18h]

LAB_004d848b:
  004d848b  33c9                  xor       ecx,ecx
  004d848d  8a4e42                mov       cl,[esi+42h]
  004d8490  83f90e                cmp       ecx,0Eh
  004d8493  0f87d9040000          ja        near 004D8972h
  004d8499  ff248dec8e4d00        jmp       dword [ecx*4+4D8EECh]
  004d84a0  33c9                  xor       ecx,ecx
  004d84a2  66394e12              cmp       [esi+12h],cx
  004d84a6  751a                  jne       short 004D84C2h
  004d84a8  8b06                  mov       eax,[esi]
  004d84aa  66894e18              mov       [esi+18h],cx
  004d84ae  0d00000001            or        eax,1000000h
  004d84b3  66894e1a              mov       [esi+1Ah],cx
  004d84b7  8906                  mov       [esi],eax
  004d84b9  66c746120100          mov       word [esi+12h],1
  004d84bf  894e5c                mov       [esi+5Ch],ecx

LAB_004d84c2:
  004d84c2  0fbf5610              movsx     edx,word [esi+10h]
  004d84c6  39542414              cmp       [esp+14h],edx
  004d84ca  7d09                  jge       short 004D84D5h
  004d84cc  c6464300              mov       byte [esi+43h],0
  004d84d0  e99d040000            jmp       004D8972h

LAB_004d84d5:
  004d84d5  8a4643                mov       al,[esi+43h]
  004d84d8  84c0                  test      al,al
  004d84da  0f84f6030000          je        near 004D88D6h
  004d84e0  fec8                  dec       al
  004d84e2  894e5c                mov       [esi+5Ch],ecx
  004d84e5  884643                mov       [esi+43h],al
  004d84e8  e985040000            jmp       004D8972h
  004d84ed  33c9                  xor       ecx,ecx
  004d84ef  66394e12              cmp       [esi+12h],cx
  004d84f3  751b                  jne       short 004D8510h
  004d84f5  8b16                  mov       edx,[esi]
  004d84f7  b801000000            mov       eax,1
  004d84fc  81ca00000001          or        edx,1000000h
  004d8502  66894618              mov       [esi+18h],ax
  004d8506  66894e1a              mov       [esi+1Ah],cx
  004d850a  8916                  mov       [esi],edx
  004d850c  66894612              mov       [esi+12h],ax

LAB_004d8510:
  004d8510  8b44241c              mov       eax,[esp+1Ch]
  004d8514  8d90c8000000          lea       edx,[eax+0C8h]
  004d851a  81e2ff0f0000          and       edx,0FFFh
  004d8520  81fa90010000          cmp       edx,190h
  004d8526  7f0c                  jg        short 004D8534h
  004d8528  c6464300              mov       byte [esi+43h],0
  004d852c  894e5c                mov       [esi+5Ch],ecx
  004d852f  e93e040000            jmp       004D8972h

LAB_004d8534:
  004d8534  25ff0f0000            and       eax,0FFFh
  004d8539  3d00080000            cmp       eax,800h
  004d853e  7d15                  jge       short 004D8555h
  004d8540  8b442410              mov       eax,[esp+10h]
  004d8544  894e5c                mov       [esi+5Ch],ecx
  004d8547  0580000000            add       eax,80h
  004d854c  66014632              add       [esi+32h],ax
  004d8550  e91d040000            jmp       004D8972h

LAB_004d8555:
  004d8555  8b542410              mov       edx,[esp+10h]
  004d8559  b880ffffff            mov       eax,0FFFFFF80h
  004d855e  2bc2                  sub       eax,edx
  004d8560  894e5c                mov       [esi+5Ch],ecx
  004d8563  66014632              add       [esi+32h],ax
  004d8567  e906040000            jmp       004D8972h
  004d856c  8b4c2410              mov       ecx,[esp+10h]
  004d8570  33d2                  xor       edx,edx
  004d8572  66395612              cmp       [esi+12h],dx
  004d8576  b801000000            mov       eax,1
  004d857b  7525                  jne       short 004D85A2h
  004d857d  8b3e                  mov       edi,[esi]
  004d857f  66894618              mov       [esi+18h],ax
  004d8583  81cf00000001          or        edi,1000000h
  004d8589  6689561a              mov       [esi+1Ah],dx
  004d858d  893e                  mov       [esi],edi
  004d858f  8d3cc9                lea       edi,[ecx+ecx*8]
  004d8592  c1e704                shl       edi,4
  004d8595  81c700e00000          add       edi,0E000h
  004d859b  66894612              mov       [esi+12h],ax
  004d859f  897e5c                mov       [esi+5Ch],edi

LAB_004d85a2:
  004d85a2  66394612              cmp       [esi+12h],ax
  004d85a6  0f85c6030000          jne       near 004D8972h
  004d85ac  0fbf4610              movsx     eax,word [esi+10h]
  004d85b0  39442414              cmp       [esp+14h],eax
  004d85b4  7d0c                  jge       short 004D85C2h
  004d85b6  c6464300              mov       byte [esi+43h],0
  004d85ba  89565c                mov       [esi+5Ch],edx
  004d85bd  e9b0030000            jmp       004D8972h

LAB_004d85c2:
  004d85c2  8a4643                mov       al,[esi+43h]
  004d85c5  84c0                  test      al,al
  004d85c7  7405                  je        short 004D85CEh
  004d85c9  fec8                  dec       al
  004d85cb  884643                mov       [esi+43h],al

LAB_004d85ce:
  004d85ce  8b44241c              mov       eax,[esp+1Ch]
  004d85d2  8d9000010000          lea       edx,[eax+100h]
  004d85d8  81e2ff0f0000          and       edx,0FFFh
  004d85de  81fa00020000          cmp       edx,200h
  004d85e4  0f8e88030000          jle       near 004D8972h
  004d85ea  25ff0f0000            and       eax,0FFFh
  004d85ef  3d00080000            cmp       eax,800h
  004d85f4  7d10                  jge       short 004D8606h
  004d85f6  8d840980000000        lea       eax,[ecx+ecx+80h]
  004d85fd  66014632              add       [esi+32h],ax
  004d8601  e96c030000            jmp       004D8972h

LAB_004d8606:
  004d8606  03c9                  add       ecx,ecx
  004d8608  b880ffffff            mov       eax,0FFFFFF80h
  004d860d  2bc1                  sub       eax,ecx
  004d860f  66014632              add       [esi+32h],ax
  004d8613  e95a030000            jmp       004D8972h
  004d8618  bb01000000            mov       ebx,1
  004d861d  66395e12              cmp       [esi+12h],bx
  004d8621  0f859a000000          jne       near 004D86C1h
  004d8627  8a4e43                mov       cl,[esi+43h]
  004d862a  84c9                  test      cl,cl
  004d862c  7405                  je        short 004D8633h
  004d862e  fec9                  dec       cl
  004d8630  884e43                mov       [esi+43h],cl

LAB_004d8633:
  004d8633  8b4c2410              mov       ecx,[esp+10h]
  004d8637  8d14c9                lea       edx,[ecx+ecx*8]
  004d863a  c1e204                shl       edx,4
  004d863d  8d8a00e00000          lea       ecx,[edx+0E000h]
  004d8643  0fafc1                imul      eax,ecx
  004d8646  c1f80c                sar       eax,0Ch
  004d8649  8bf8                  mov       edi,eax
  004d864b  894e5c                mov       [esi+5Ch],ecx
  004d864e  0fbf4610              movsx     eax,word [esi+10h]
  004d8652  50                    push      eax
  004d8653  e8c4d30600            call      00545A1Ch
  004d8658  0fafc7                imul      eax,edi
  004d865b  8b4d00                mov       ecx,[ebp]
  004d865e  c1f80c                sar       eax,0Ch
  004d8661  03c8                  add       ecx,eax
  004d8663  894d00                mov       [ebp],ecx
  004d8666  0fbf4e10              movsx     ecx,word [esi+10h]
  004d866a  51                    push      ecx
  004d866b  e8a6d30600            call      00545A16h
  004d8670  0fafc7                imul      eax,edi
  004d8673  8b6e24                mov       ebp,[esi+24h]
  004d8676  c7465c00000000        mov       dword [esi+5Ch],0
  004d867d  c1f80c                sar       eax,0Ch
  004d8680  03e8                  add       ebp,eax
  004d8682  8b44241c              mov       eax,[esp+1Ch]
  004d8686  896e24                mov       [esi+24h],ebp
  004d8689  8d9000010000          lea       edx,[eax+100h]
  004d868f  81e2ff0f0000          and       edx,0FFFh
  004d8695  81fa00020000          cmp       edx,200h
  004d869b  7e24                  jle       short 004D86C1h
  004d869d  25ff0f0000            and       eax,0FFFh
  004d86a2  3d00080000            cmp       eax,800h
  004d86a7  7d09                  jge       short 004D86B2h
  004d86a9  8b442410              mov       eax,[esp+10h]
  004d86ad  83c032                add       eax,32h
  004d86b0  eb0b                  jmp       short 004D86BDh

LAB_004d86b2:
  004d86b2  8b4c2410              mov       ecx,[esp+10h]
  004d86b6  b8ceffffff            mov       eax,0FFFFFFCEh
  004d86bb  2bc1                  sub       eax,ecx

LAB_004d86bd:
  004d86bd  66014632              add       [esi+32h],ax

LAB_004d86c1:
  004d86c1  66837e1200            cmp       word [esi+12h],0
  004d86c6  0f85a6020000          jne       near 004D8972h
  004d86cc  8b06                  mov       eax,[esi]
  004d86ce  66895e18              mov       [esi+18h],bx
  004d86d2  0d00000001            or        eax,1000000h
  004d86d7  66c7461a0000          mov       word [esi+1Ah],0
  004d86dd  8906                  mov       [esi],eax
  004d86df  66895e12              mov       [esi+12h],bx
  004d86e3  e98a020000            jmp       004D8972h
  004d86e8  66837e1200            cmp       word [esi+12h],0
  004d86ed  0f8581000000          jne       near 004D8774h
  004d86f3  8b0e                  mov       ecx,[esi]
  004d86f5  f6c101                test      cl,1
  004d86f8  7406                  je        short 004D8700h
  004d86fa  c6464300              mov       byte [esi+43h],0
  004d86fe  eb74                  jmp       short 004D8774h

LAB_004d8700:
  004d8700  81e1fffffffe          and       ecx,0FEFFFFFFh
  004d8706  66c746180400          mov       word [esi+18h],4
  004d870c  890e                  mov       [esi],ecx
  004d870e  8b4c2410              mov       ecx,[esp+10h]
  004d8712  81c100040000          add       ecx,400h
  004d8718  66c7461a0000          mov       word [esi+1Ah],0
  004d871e  c1e106                shl       ecx,6
  004d8721  0fafc1                imul      eax,ecx
  004d8724  0fbf5610              movsx     edx,word [esi+10h]
  004d8728  c1f80c                sar       eax,0Ch
  004d872b  52                    push      edx
  004d872c  894e5c                mov       [esi+5Ch],ecx
  004d872f  8bf8                  mov       edi,eax
  004d8731  e8e6d20600            call      00545A1Ch
  004d8736  0fafc7                imul      eax,edi
  004d8739  8b6e50                mov       ebp,[esi+50h]
  004d873c  c1f80c                sar       eax,0Ch
  004d873f  03e8                  add       ebp,eax
  004d8741  0fbf4610              movsx     eax,word [esi+10h]
  004d8745  50                    push      eax
  004d8746  896e50                mov       [esi+50h],ebp
  004d8749  e8c8d20600            call      00545A16h
  004d874e  0fafc7                imul      eax,edi
  004d8751  8b5658                mov       edx,[esi+58h]
  004d8754  8b4e54                mov       ecx,[esi+54h]
  004d8757  c1f80c                sar       eax,0Ch
  004d875a  03d0                  add       edx,eax
  004d875c  8b06                  mov       eax,[esi]
  004d875e  81c100e00100          add       ecx,1E000h
  004d8764  0c01                  or        al,1
  004d8766  895658                mov       [esi+58h],edx
  004d8769  894e54                mov       [esi+54h],ecx
  004d876c  8906                  mov       [esi],eax
  004d876e  66c746120100          mov       word [esi+12h],1

LAB_004d8774:
  004d8774  f60601                test      byte [esi],1
  004d8777  0f85f5010000          jne       near 004D8972h
  004d877d  c6464300              mov       byte [esi+43h],0
  004d8781  e9ec010000            jmp       004D8972h
  004d8786  668b4e12              mov       cx,[esi+12h]
  004d878a  ba01000000            mov       edx,1
  004d878f  663bca                cmp       cx,dx
  004d8792  751c                  jne       short 004D87B0h
  004d8794  0fbf4610              movsx     eax,word [esi+10h]
  004d8798  39442414              cmp       [esp+14h],eax
  004d879c  7d06                  jge       short 004D87A4h
  004d879e  c6464300              mov       byte [esi+43h],0
  004d87a2  eb0c                  jmp       short 004D87B0h

LAB_004d87a4:
  004d87a4  8a4643                mov       al,[esi+43h]
  004d87a7  84c0                  test      al,al
  004d87a9  7405                  je        short 004D87B0h
  004d87ab  fec8                  dec       al
  004d87ad  884643                mov       [esi+43h],al

LAB_004d87b0:
  004d87b0  6685c9                test      cx,cx
  004d87b3  0f85b9010000          jne       near 004D8972h
  004d87b9  8b442410              mov       eax,[esp+10h]
  004d87bd  8b3e                  mov       edi,[esi]
  004d87bf  66894e1a              mov       [esi+1Ah],cx
  004d87c3  81cf00000001          or        edi,1000000h
  004d87c9  8d0cc0                lea       ecx,[eax+eax*8]
  004d87cc  66895618              mov       [esi+18h],dx
  004d87d0  c1e104                shl       ecx,4
  004d87d3  81c100c00000          add       ecx,0C000h
  004d87d9  893e                  mov       [esi],edi
  004d87db  66895612              mov       [esi+12h],dx
  004d87df  894e5c                mov       [esi+5Ch],ecx
  004d87e2  e98b010000            jmp       004D8972h
  004d87e7  668b4e12              mov       cx,[esi+12h]
  004d87eb  6683f901              cmp       cx,1
  004d87ef  7511                  jne       short 004D8802h
  004d87f1  8b06                  mov       eax,[esi]
  004d87f3  a900000002            test      eax,2000000h
  004d87f8  7408                  je        short 004D8802h
  004d87fa  24fd                  and       al,0FDh
  004d87fc  c6464200              mov       byte [esi+42h],0
  004d8800  8906                  mov       [esi],eax

LAB_004d8802:
  004d8802  6685c9                test      cx,cx
  004d8805  0f8567010000          jne       near 004D8972h
  004d880b  8b06                  mov       eax,[esi]
  004d880d  66c746120100          mov       word [esi+12h],1
  004d8813  25fffffffe            and       eax,0FEFFFFFFh
  004d8818  8906                  mov       [esi],eax
  004d881a  e953010000            jmp       004D8972h
  004d881f  817c241496000000      cmp       dword [esp+14h],96h
  004d8827  7e04                  jle       short 004D882Dh
  004d8829  c6464300              mov       byte [esi+43h],0

LAB_004d882d:
  004d882d  668b4612              mov       ax,[esi+12h]
  004d8831  663d0200              cmp       ax,2
  004d8835  750c                  jne       short 004D8843h
  004d8837  f70600000002          test      dword [esi],2000000h
  004d883d  7404                  je        short 004D8843h
  004d883f  c6464300              mov       byte [esi+43h],0

LAB_004d8843:
  004d8843  663d0100              cmp       ax,1
  004d8847  7522                  jne       short 004D886Bh
  004d8849  8b06                  mov       eax,[esi]
  004d884b  a900000002            test      eax,2000000h
  004d8850  7419                  je        short 004D886Bh
  004d8852  25fffffffe            and       eax,0FEFFFFFFh
  004d8857  66c746120200          mov       word [esi+12h],2
  004d885d  8906                  mov       [esi],eax
  004d885f  66c746180500          mov       word [esi+18h],5
  004d8865  66c7461a0b00          mov       word [esi+1Ah],0Bh

LAB_004d886b:
  004d886b  33c0                  xor       eax,eax
  004d886d  66394612              cmp       [esi+12h],ax
  004d8871  0f85fb000000          jne       near 004D8972h
  004d8877  8b16                  mov       edx,[esi]
  004d8879  66c746120100          mov       word [esi+12h],1
  004d887f  81e2fffffffe          and       edx,0FEFFFFFFh
  004d8885  66c746180300          mov       word [esi+18h],3
  004d888b  83ca02                or        edx,2
  004d888e  6689461a              mov       [esi+1Ah],ax
  004d8892  8916                  mov       [esi],edx
  004d8894  89465c                mov       [esi+5Ch],eax
  004d8897  e9d6000000            jmp       004D8972h
  004d889c  668b4612              mov       ax,[esi+12h]
  004d88a0  663d0100              cmp       ax,1
  004d88a4  750c                  jne       short 004D88B2h
  004d88a6  f70600000002          test      dword [esi],2000000h
  004d88ac  7404                  je        short 004D88B2h
  004d88ae  c6464300              mov       byte [esi+43h],0

LAB_004d88b2:
  004d88b2  33c9                  xor       ecx,ecx
  004d88b4  663bc1                cmp       ax,cx
  004d88b7  0f85b5000000          jne       near 004D8972h
  004d88bd  8b06                  mov       eax,[esi]
  004d88bf  66c746120100          mov       word [esi+12h],1
  004d88c5  25fffffffe            and       eax,0FEFFFFFFh
  004d88ca  66c746180700          mov       word [esi+18h],7
  004d88d0  66894e1a              mov       [esi+1Ah],cx
  004d88d4  8906                  mov       [esi],eax

LAB_004d88d6:
  004d88d6  894e5c                mov       [esi+5Ch],ecx
  004d88d9  e994000000            jmp       004D8972h
  004d88de  668b4612              mov       ax,[esi+12h]
  004d88e2  663d0200              cmp       ax,2
  004d88e6  750f                  jne       short 004D88F7h
  004d88e8  8a4e43                mov       cl,[esi+43h]
  004d88eb  33ff                  xor       edi,edi
  004d88ed  fec9                  dec       cl
  004d88ef  897e5c                mov       [esi+5Ch],edi
  004d88f2  884e43                mov       [esi+43h],cl
  004d88f5  eb02                  jmp       short 004D88F9h

LAB_004d88f7:
  004d88f7  33ff                  xor       edi,edi

LAB_004d88f9:
  004d88f9  663d0100              cmp       ax,1
  004d88fd  7551                  jne       short 004D8950h
  004d88ff  8b06                  mov       eax,[esi]
  004d8901  897e5c                mov       [esi+5Ch],edi
  004d8904  a900000002            test      eax,2000000h
  004d8909  7445                  je        short 004D8950h
  004d890b  a1cc975e00            mov       eax,[5E97CCh]
  004d8910  6800200000            push      2000h
  004d8915  6800200000            push      2000h
  004d891a  6800050000            push      500h
  004d891f  8b88a8000000          mov       ecx,[eax+0A8h]
  004d8925  33c0                  xor       eax,eax
  004d8927  8b11                  mov       edx,[ecx]
  004d8929  8a422c                mov       al,[edx+2Ch]
  004d892c  50                    push      eax
  004d892d  e86e450600            call      0053CEA0h
  004d8932  83c410                add       esp,10h
  004d8935  6aff                  push      0FFFFFFFFh
  004d8937  57                    push      edi
  004d8938  68de000000            push      0DEh
  004d893d  56                    push      esi
  004d893e  e88d20faff            call      0047A9D0h
  004d8943  83c410                add       esp,10h
  004d8946  66c746120200          mov       word [esi+12h],2
  004d894c  c6464314              mov       byte [esi+43h],14h

LAB_004d8950:
  004d8950  66397e12              cmp       [esi+12h],di
  004d8954  751c                  jne       short 004D8972h
  004d8956  8b06                  mov       eax,[esi]
  004d8958  66c746120100          mov       word [esi+12h],1
  004d895e  25fffffffe            and       eax,0FEFFFFFFh
  004d8963  66c746180600          mov       word [esi+18h],6
  004d8969  66897e1a              mov       [esi+1Ah],di
  004d896d  8906                  mov       [esi],eax
  004d896f  897e5c                mov       [esi+5Ch],edi

LAB_004d8972:
  004d8972  0fbf4e32              movsx     ecx,word [esi+32h]
  004d8976  51                    push      ecx
  004d8977  e8a0d00600            call      00545A1Ch
  004d897c  0fbf5632              movsx     edx,word [esi+32h]
  004d8980  52                    push      edx
  004d8981  8944246c              mov       [esp+6Ch],eax
  004d8985  e88cd00600            call      00545A16h
  004d898a  8b5650                mov       edx,[esi+50h]
  004d898d  89442470              mov       [esp+70h],eax
  004d8991  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004d8998  8bc2                  mov       eax,edx
  004d899a  8bfa                  mov       edi,edx
  004d899c  c1f803                sar       eax,3
  004d899f  8b4c2418              mov       ecx,[esp+18h]
  004d89a3  2bf8                  sub       edi,eax
  004d89a5  c1fa08                sar       edx,8
  004d89a8  0faf4e5c              imul      ecx,[esi+5Ch]
  004d89ac  2bfa                  sub       edi,edx
  004d89ae  8b5654                mov       edx,[esi+54h]
  004d89b1  8bc5                  mov       eax,ebp
  004d89b3  8bda                  mov       ebx,edx
  004d89b5  c1f808                sar       eax,8
  004d89b8  c1fb08                sar       ebx,8
  004d89bb  2bc3                  sub       eax,ebx
  004d89bd  897e50                mov       [esi+50h],edi
  004d89c0  2bc5                  sub       eax,ebp
  004d89c2  56                    push      esi
  004d89c3  03c2                  add       eax,edx
  004d89c5  8b5658                mov       edx,[esi+58h]
  004d89c8  8bda                  mov       ebx,edx
  004d89ca  8bea                  mov       ebp,edx
  004d89cc  c1fb08                sar       ebx,8
  004d89cf  2beb                  sub       ebp,ebx
  004d89d1  8b5e1c                mov       ebx,[esi+1Ch]
  004d89d4  c1fa03                sar       edx,3
  004d89d7  c1f90c                sar       ecx,0Ch
  004d89da  2bea                  sub       ebp,edx
  004d89dc  8bd1                  mov       edx,ecx
  004d89de  0faf54246c            imul      edx,[esp+6Ch]
  004d89e3  0faf4c2474            imul      ecx,[esp+74h]
  004d89e8  c1fa0c                sar       edx,0Ch
  004d89eb  c1f90c                sar       ecx,0Ch
  004d89ee  896e58                mov       [esi+58h],ebp
  004d89f1  03d7                  add       edx,edi
  004d89f3  8b7e20                mov       edi,[esi+20h]
  004d89f6  03cd                  add       ecx,ebp
  004d89f8  8b6e24                mov       ebp,[esi+24h]
  004d89fb  03da                  add       ebx,edx
  004d89fd  03f8                  add       edi,eax
  004d89ff  03e9                  add       ebp,ecx
  004d8a01  894654                mov       [esi+54h],eax
  004d8a04  895e1c                mov       [esi+1Ch],ebx
  004d8a07  897e20                mov       [esi+20h],edi
  004d8a0a  896e24                mov       [esi+24h],ebp
  004d8a0d  e82eaf0500            call      00533940h
  004d8a12  8b9690000000          mov       edx,[esi+90h]
  004d8a18  8b4660                mov       eax,[esi+60h]
  004d8a1b  8b4e20                mov       ecx,[esi+20h]
  004d8a1e  83c404                add       esp,4
  004d8a21  80e6fc                and       dh,0FCh
  004d8a24  3bc8                  cmp       ecx,eax
  004d8a26  899690000000          mov       [esi+90h],edx
  004d8a2c  7f2a                  jg        short 004D8A58h
  004d8a2e  f6c680                test      dh,80h
  004d8a31  7525                  jne       short 004D8A58h
  004d8a33  8b0e                  mov       ecx,[esi]
  004d8a35  894620                mov       [esi+20h],eax
  004d8a38  f6c101                test      cl,1
  004d8a3b  740f                  je        short 004D8A4Ch
  004d8a3d  83e1fe                and       ecx,0FFFFFFFEh
  004d8a40  33db                  xor       ebx,ebx
  004d8a42  890e                  mov       [esi],ecx
  004d8a44  895e54                mov       [esi+54h],ebx
  004d8a47  e925010000            jmp       004D8B71h

LAB_004d8a4c:
  004d8a4c  83e1fe                and       ecx,0FFFFFFFEh
  004d8a4f  33db                  xor       ebx,ebx
  004d8a51  890e                  mov       [esi],ecx
  004d8a53  895e54                mov       [esi+54h],ebx
  004d8a56  eb02                  jmp       short 004D8A5Ah

LAB_004d8a58:
  004d8a58  33db                  xor       ebx,ebx

LAB_004d8a5a:
  004d8a5a  8b0e                  mov       ecx,[esi]
  004d8a5c  f6c101                test      cl,1
  004d8a5f  750a                  jne       short 004D8A6Bh
  004d8a61  394620                cmp       [esi+20h],eax
  004d8a64  7e05                  jle       short 004D8A6Bh
  004d8a66  83c901                or        ecx,1
  004d8a69  890e                  mov       [esi],ecx

LAB_004d8a6b:
  004d8a6b  f7c282ad0020          test      edx,2000AD82h
  004d8a71  0f84fa000000          je        near 004D8B71h
  004d8a77  f60601                test      byte [esi],1
  004d8a7a  0f85c9000000          jne       near 004D8B49h
  004d8a80  8a4e42                mov       cl,[esi+42h]
  004d8a83  b007                  mov       al,7
  004d8a85  3ac8                  cmp       cl,al
  004d8a87  7420                  je        short 004D8AA9h
  004d8a89  884642                mov       [esi+42h],al
  004d8a8c  e8afe60600            call      00547140h
  004d8a91  99                    cdq
  004d8a92  b928000000            mov       ecx,28h
  004d8a97  66895e12              mov       [esi+12h],bx
  004d8a9b  f7f9                  idiv      ecx
  004d8a9d  66c746105000          mov       word [esi+10h],50h
  004d8aa3  80c214                add       dl,14h
  004d8aa6  885643                mov       [esi+43h],dl

LAB_004d8aa9:
  004d8aa9  8b16                  mov       edx,[esi]
  004d8aab  8b442460              mov       eax,[esp+60h]
  004d8aaf  8b4c2464              mov       ecx,[esp+64h]
  004d8ab3  83e2fd                and       edx,0FFFFFFFDh
  004d8ab6  8916                  mov       [esi],edx
  004d8ab8  8b54245c              mov       edx,[esp+5Ch]
  004d8abc  894620                mov       [esi+20h],eax
  004d8abf  8b44242c              mov       eax,[esp+2Ch]
  004d8ac3  89561c                mov       [esi+1Ch],edx
  004d8ac6  8b542428              mov       edx,[esp+28h]
  004d8aca  25ffff0000            and       eax,0FFFFh
  004d8acf  894e24                mov       [esi+24h],ecx
  004d8ad2  8b4c2434              mov       ecx,[esp+34h]
  004d8ad6  89567c                mov       [esi+7Ch],edx
  004d8ad9  8b542438              mov       edx,[esp+38h]
  004d8add  898690000000          mov       [esi+90h],eax
  004d8ae3  8b442430              mov       eax,[esp+30h]
  004d8ae7  898e84000000          mov       [esi+84h],ecx
  004d8aed  8b4c2440              mov       ecx,[esp+40h]
  004d8af1  898680000000          mov       [esi+80h],eax
  004d8af7  8b44243c              mov       eax,[esp+3Ch]
  004d8afb  899688000000          mov       [esi+88h],edx
  004d8b01  8b542444              mov       edx,[esp+44h]
  004d8b05  6882ad0020            push      2000AD82h
  004d8b0a  56                    push      esi
  004d8b0b  89868c000000          mov       [esi+8Ch],eax
  004d8b11  894e64                mov       [esi+64h],ecx
  004d8b14  895660                mov       [esi+60h],edx
  004d8b17  e844d3ffff            call      004D5E60h
  004d8b1c  83c408                add       esp,8
  004d8b1f  663bc3                cmp       ax,bx
  004d8b22  7515                  jne       short 004D8B39h
  004d8b24  e817e60600            call      00547140h
  004d8b29  99                    cdq
  004d8b2a  33c2                  xor       eax,edx
  004d8b2c  2bc2                  sub       eax,edx
  004d8b2e  83e001                and       eax,1
  004d8b31  33c2                  xor       eax,edx
  004d8b33  2bc2                  sub       eax,edx
  004d8b35  8d4400ff              lea       eax,[eax+eax-1]

LAB_004d8b39:
  004d8b39  8b4c2410              mov       ecx,[esp+10h]
  004d8b3d  83c140                add       ecx,40h
  004d8b40  0fafc8                imul      ecx,eax
  004d8b43  66014e32              add       [esi+32h],cx
  004d8b47  eb28                  jmp       short 004D8B71h

LAB_004d8b49:
  004d8b49  8b54245c              mov       edx,[esp+5Ch]
  004d8b4d  8b442464              mov       eax,[esp+64h]
  004d8b51  895e5c                mov       [esi+5Ch],ebx
  004d8b54  895e50                mov       [esi+50h],ebx
  004d8b57  895e58                mov       [esi+58h],ebx
  004d8b5a  89561c                mov       [esi+1Ch],edx
  004d8b5d  894624                mov       [esi+24h],eax
  004d8b60  885e42                mov       [esi+42h],bl
  004d8b63  c6464328              mov       byte [esi+43h],28h
  004d8b67  66895e12              mov       [esi+12h],bx
  004d8b6b  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004d8b71:
  004d8b71  6aff                  push      0FFFFFFFFh
  004d8b73  6aff                  push      0FFFFFFFFh
  004d8b75  6aff                  push      0FFFFFFFFh
  004d8b77  56                    push      esi
  004d8b78  e893acfeff            call      004C3810h
  004d8b7d  8b442428              mov       eax,[esp+28h]
  004d8b81  83c410                add       esp,10h
  004d8b84  3bc3                  cmp       eax,ebx
  004d8b86  7514                  jne       short 004D8B9Ch
  004d8b88  668b4c247e            mov       cx,[esp+7Eh]
  004d8b8d  66894e32              mov       [esi+32h],cx
  004d8b91  5f                    pop       edi
  004d8b92  5e                    pop       esi
  004d8b93  5d                    pop       ebp
  004d8b94  5b                    pop       ebx
  004d8b95  81c480000000          add       esp,80h
  004d8b9b  c3                    ret

LAB_004d8b9c:
  004d8b9c  385e43                cmp       [esi+43h],bl
  004d8b9f  0f853c030000          jne       near 004D8EE1h
  004d8ba5  8b466c                mov       eax,[esi+6Ch]
  004d8ba8  8b561c                mov       edx,[esi+1Ch]
  004d8bab  8b781c                mov       edi,[eax+1Ch]
  004d8bae  8b6824                mov       ebp,[eax+24h]
  004d8bb1  8b4624                mov       eax,[esi+24h]
  004d8bb4  2bfa                  sub       edi,edx
  004d8bb6  2be8                  sub       ebp,eax
  004d8bb8  c1ff0c                sar       edi,0Ch
  004d8bbb  c1fd0c                sar       ebp,0Ch
  004d8bbe  8bd7                  mov       edx,edi
  004d8bc0  8bc5                  mov       eax,ebp
  004d8bc2  0fafd7                imul      edx,edi
  004d8bc5  0fafc5                imul      eax,ebp
  004d8bc8  03d0                  add       edx,eax
  004d8bca  52                    push      edx
  004d8bcb  e840ce0600            call      00545A10h
  004d8bd0  55                    push      ebp
  004d8bd1  57                    push      edi
  004d8bd2  8944241c              mov       [esp+1Ch],eax
  004d8bd6  e847ce0600            call      00545A22h
  004d8bdb  668b4e32              mov       cx,[esi+32h]
  004d8bdf  bd64000000            mov       ebp,64h
  004d8be4  2bc1                  sub       eax,ecx
  004d8be6  66895e12              mov       [esi+12h],bx
  004d8bea  25ff0f0000            and       eax,0FFFh
  004d8bef  8944241c              mov       [esp+1Ch],eax
  004d8bf3  8b442414              mov       eax,[esp+14h]
  004d8bf7  3bc5                  cmp       eax,ebp
  004d8bf9  0f8dd3000000          jge       near 004D8CD2h
  004d8bff  e83ce50600            call      00547140h
  004d8c04  99                    cdq
  004d8c05  8bcd                  mov       ecx,ebp
  004d8c07  f7f9                  idiv      ecx
  004d8c09  8bfa                  mov       edi,edx
  004d8c0b  83ff4b                cmp       edi,4Bh
  004d8c0e  7d75                  jge       short 004D8C85h
  004d8c10  8b54241c              mov       edx,[esp+1Ch]
  004d8c14  81c200010000          add       edx,100h
  004d8c1a  81e2ff0f0000          and       edx,0FFFh
  004d8c20  81fa00020000          cmp       edx,200h
  004d8c26  7f53                  jg        short 004D8C7Bh
  004d8c28  e813e50600            call      00547140h
  004d8c2d  396c2414              cmp       [esp+14h],ebp
  004d8c31  7d48                  jge       short 004D8C7Bh
  004d8c33  83ff1e                cmp       edi,1Eh
  004d8c36  7d22                  jge       short 004D8C5Ah
  004d8c38  8b06                  mov       eax,[esi]
  004d8c3a  c646420b              mov       byte [esi+42h],0Bh
  004d8c3e  25fffffffe            and       eax,0FEFFFFFFh
  004d8c43  c6464301              mov       byte [esi+43h],1
  004d8c47  0c02                  or        al,2
  004d8c49  895e5c                mov       [esi+5Ch],ebx
  004d8c4c  8906                  mov       [esi],eax
  004d8c4e  66c746180300          mov       word [esi+18h],3
  004d8c54  66895e1a              mov       [esi+1Ah],bx
  004d8c58  eb78                  jmp       short 004D8CD2h

LAB_004d8c5a:
  004d8c5a  83ff3c                cmp       edi,3Ch
  004d8c5d  7d0e                  jge       short 004D8C6Dh
  004d8c5f  c646420c              mov       byte [esi+42h],0Ch
  004d8c63  c6464301              mov       byte [esi+43h],1
  004d8c67  66895e12              mov       [esi+12h],bx
  004d8c6b  eb65                  jmp       short 004D8CD2h

LAB_004d8c6d:
  004d8c6d  c646420d              mov       byte [esi+42h],0Dh
  004d8c71  c6464301              mov       byte [esi+43h],1
  004d8c75  66895e12              mov       [esi+12h],bx
  004d8c79  eb57                  jmp       short 004D8CD2h

LAB_004d8c7b:
  004d8c7b  c6464201              mov       byte [esi+42h],1
  004d8c7f  c6464301              mov       byte [esi+43h],1
  004d8c83  eb4d                  jmp       short 004D8CD2h

LAB_004d8c85:
  004d8c85  83ff55                cmp       edi,55h
  004d8c88  7c2b                  jl        short 004D8CB5h
  004d8c8a  83ff46                cmp       edi,46h
  004d8c8d  7d26                  jge       short 004D8CB5h
  004d8c8f  8b542410              mov       edx,[esp+10h]
  004d8c93  8b442414              mov       eax,[esp+14h]
  004d8c97  83c264                add       edx,64h
  004d8c9a  3bc2                  cmp       eax,edx
  004d8c9c  7e17                  jle       short 004D8CB5h
  004d8c9e  c6464206              mov       byte [esi+42h],6
  004d8ca2  c6464301              mov       byte [esi+43h],1
  004d8ca6  e895e40600            call      00547140h
  004d8cab  66894610              mov       [esi+10h],ax
  004d8caf  66895e12              mov       [esi+12h],bx
  004d8cb3  eb1d                  jmp       short 004D8CD2h

LAB_004d8cb5:
  004d8cb5  c6464202              mov       byte [esi+42h],2
  004d8cb9  e882e40600            call      00547140h
  004d8cbe  99                    cdq
  004d8cbf  b90a000000            mov       ecx,0Ah
  004d8cc4  66c746105000          mov       word [esi+10h],50h
  004d8cca  f7f9                  idiv      ecx
  004d8ccc  80c214                add       dl,14h
  004d8ccf  885643                mov       [esi+43h],dl

LAB_004d8cd2:
  004d8cd2  8b442414              mov       eax,[esp+14h]
  004d8cd6  3d58020000            cmp       eax,258h
  004d8cdb  0f8dab000000          jge       near 004D8D8Ch
  004d8ce1  3bc5                  cmp       eax,ebp
  004d8ce3  0f8ca3000000          jl        near 004D8D8Ch
  004d8ce9  e852e40600            call      00547140h
  004d8cee  99                    cdq
  004d8cef  8bcd                  mov       ecx,ebp
  004d8cf1  f7f9                  idiv      ecx
  004d8cf3  83fa14                cmp       edx,14h
  004d8cf6  7d22                  jge       short 004D8D1Ah
  004d8cf8  8b54241c              mov       edx,[esp+1Ch]
  004d8cfc  81c200040000          add       edx,400h
  004d8d02  81e2ff0f0000          and       edx,0FFFh
  004d8d08  81fa00080000          cmp       edx,800h
  004d8d0e  7c0f                  jl        short 004D8D1Fh
  004d8d10  c6464201              mov       byte [esi+42h],1
  004d8d14  c6464301              mov       byte [esi+43h],1
  004d8d18  eb72                  jmp       short 004D8D8Ch

LAB_004d8d1a:
  004d8d1a  83fa28                cmp       edx,28h
  004d8d1d  7d1f                  jge       short 004D8D3Eh

LAB_004d8d1f:
  004d8d1f  c6464202              mov       byte [esi+42h],2
  004d8d23  e818e40600            call      00547140h
  004d8d28  99                    cdq
  004d8d29  b90a000000            mov       ecx,0Ah
  004d8d2e  66c746105000          mov       word [esi+10h],50h
  004d8d34  f7f9                  idiv      ecx
  004d8d36  80c214                add       dl,14h
  004d8d39  885643                mov       [esi+43h],dl
  004d8d3c  eb4e                  jmp       short 004D8D8Ch

LAB_004d8d3e:
  004d8d3e  83fa3c                cmp       edx,3Ch
  004d8d41  7d17                  jge       short 004D8D5Ah
  004d8d43  c6464206              mov       byte [esi+42h],6
  004d8d47  c6464301              mov       byte [esi+43h],1
  004d8d4b  e8f0e30600            call      00547140h
  004d8d50  66894610              mov       [esi+10h],ax
  004d8d54  66895e12              mov       [esi+12h],bx
  004d8d58  eb32                  jmp       short 004D8D8Ch

LAB_004d8d5a:
  004d8d5a  c6464203              mov       byte [esi+42h],3
  004d8d5e  66895e12              mov       [esi+12h],bx
  004d8d62  e8d9e30600            call      00547140h
  004d8d67  99                    cdq
  004d8d68  b90a000000            mov       ecx,0Ah
  004d8d6d  f7f9                  idiv      ecx
  004d8d6f  80c214                add       dl,14h
  004d8d72  885643                mov       [esi+43h],dl
  004d8d75  e8c6e30600            call      00547140h
  004d8d7a  99                    cdq
  004d8d7b  33c2                  xor       eax,edx
  004d8d7d  2bc2                  sub       eax,edx
  004d8d7f  25ff0f0000            and       eax,0FFFh
  004d8d84  33c2                  xor       eax,edx
  004d8d86  2bc2                  sub       eax,edx
  004d8d88  66894610              mov       [esi+10h],ax

LAB_004d8d8c:
  004d8d8c  817c241458020000      cmp       dword [esp+14h],258h
  004d8d94  0f8c47010000          jl        near 004D8EE1h
  004d8d9a  e8a1e30600            call      00547140h
  004d8d9f  99                    cdq
  004d8da0  8bcd                  mov       ecx,ebp
  004d8da2  f7f9                  idiv      ecx
  004d8da4  83fa1e                cmp       edx,1Eh
  004d8da7  7d60                  jge       short 004D8E09h
  004d8da9  8b442410              mov       eax,[esp+10h]
  004d8dad  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004d8db1  3bd1                  cmp       edx,ecx
  004d8db3  7d26                  jge       short 004D8DDBh
  004d8db5  c6464202              mov       byte [esi+42h],2
  004d8db9  e882e30600            call      00547140h
  004d8dbe  99                    cdq
  004d8dbf  b90a000000            mov       ecx,0Ah
  004d8dc4  66896e10              mov       [esi+10h],bp
  004d8dc8  f7f9                  idiv      ecx
  004d8dca  80c214                add       dl,14h
  004d8dcd  885643                mov       [esi+43h],dl
  004d8dd0  5f                    pop       edi
  004d8dd1  5e                    pop       esi
  004d8dd2  5d                    pop       ebp
  004d8dd3  5b                    pop       ebx
  004d8dd4  81c480000000          add       esp,80h
  004d8dda  c3                    ret

LAB_004d8ddb:
  004d8ddb  885e42                mov       [esi+42h],bl
  004d8dde  e85de30600            call      00547140h
  004d8de3  99                    cdq
  004d8de4  b90a000000            mov       ecx,0Ah
  004d8de9  66895e12              mov       [esi+12h],bx
  004d8ded  f7f9                  idiv      ecx
  004d8def  885643                mov       [esi+43h],dl
  004d8df2  8b542410              mov       edx,[esp+10h]
  004d8df6  8d441250              lea       eax,[edx+edx+50h]
  004d8dfa  66894610              mov       [esi+10h],ax
  004d8dfe  5f                    pop       edi
  004d8dff  5e                    pop       esi
  004d8e00  5d                    pop       ebp
  004d8e01  5b                    pop       ebx
  004d8e02  81c480000000          add       esp,80h
  004d8e08  c3                    ret

LAB_004d8e09:
  004d8e09  83fa28                cmp       edx,28h
  004d8e0c  7d3d                  jge       short 004D8E4Bh
  004d8e0e  c6464203              mov       byte [esi+42h],3
  004d8e12  66895e12              mov       [esi+12h],bx
  004d8e16  e825e30600            call      00547140h
  004d8e1b  99                    cdq
  004d8e1c  b90a000000            mov       ecx,0Ah
  004d8e21  f7f9                  idiv      ecx
  004d8e23  80c214                add       dl,14h
  004d8e26  885643                mov       [esi+43h],dl
  004d8e29  e812e30600            call      00547140h
  004d8e2e  99                    cdq
  004d8e2f  33c2                  xor       eax,edx
  004d8e31  2bc2                  sub       eax,edx
  004d8e33  25ff0f0000            and       eax,0FFFh
  004d8e38  33c2                  xor       eax,edx
  004d8e3a  2bc2                  sub       eax,edx
  004d8e3c  66894610              mov       [esi+10h],ax
  004d8e40  5f                    pop       edi
  004d8e41  5e                    pop       esi
  004d8e42  5d                    pop       ebp
  004d8e43  5b                    pop       ebx
  004d8e44  81c480000000          add       esp,80h
  004d8e4a  c3                    ret

LAB_004d8e4b:
  004d8e4b  83fa3c                cmp       edx,3Ch
  004d8e4e  7d26                  jge       short 004D8E76h
  004d8e50  c6464202              mov       byte [esi+42h],2
  004d8e54  e8e7e20600            call      00547140h
  004d8e59  99                    cdq
  004d8e5a  b90a000000            mov       ecx,0Ah
  004d8e5f  66896e10              mov       [esi+10h],bp
  004d8e63  f7f9                  idiv      ecx
  004d8e65  80c214                add       dl,14h
  004d8e68  885643                mov       [esi+43h],dl
  004d8e6b  5f                    pop       edi
  004d8e6c  5e                    pop       esi
  004d8e6d  5d                    pop       ebp
  004d8e6e  5b                    pop       ebx
  004d8e6f  81c480000000          add       esp,80h
  004d8e75  c3                    ret

LAB_004d8e76:
  004d8e76  83fa41                cmp       edx,41h
  004d8e79  7d55                  jge       short 004D8ED0h
  004d8e7b  8b54241c              mov       edx,[esp+1Ch]
  004d8e7f  81c200010000          add       edx,100h
  004d8e85  81e2ff0f0000          and       edx,0FFFh
  004d8e8b  81fa00020000          cmp       edx,200h
  004d8e91  7f17                  jg        short 004D8EAAh
  004d8e93  c646420e              mov       byte [esi+42h],0Eh
  004d8e97  c6464301              mov       byte [esi+43h],1
  004d8e9b  66895e12              mov       [esi+12h],bx
  004d8e9f  5f                    pop       edi
  004d8ea0  5e                    pop       esi
  004d8ea1  5d                    pop       ebp
  004d8ea2  5b                    pop       ebx
  004d8ea3  81c480000000          add       esp,80h
  004d8ea9  c3                    ret

LAB_004d8eaa:
  004d8eaa  c6464202              mov       byte [esi+42h],2
  004d8eae  e88de20600            call      00547140h
  004d8eb3  99                    cdq
  004d8eb4  b90a000000            mov       ecx,0Ah
  004d8eb9  66896e10              mov       [esi+10h],bp
  004d8ebd  f7f9                  idiv      ecx
  004d8ebf  80c214                add       dl,14h
  004d8ec2  885643                mov       [esi+43h],dl
  004d8ec5  5f                    pop       edi
  004d8ec6  5e                    pop       esi
  004d8ec7  5d                    pop       ebp
  004d8ec8  5b                    pop       ebx
  004d8ec9  81c480000000          add       esp,80h
  004d8ecf  c3                    ret

LAB_004d8ed0:
  004d8ed0  c6464206              mov       byte [esi+42h],6
  004d8ed4  c6464301              mov       byte [esi+43h],1
  004d8ed8  e863e20600            call      00547140h
  004d8edd  66894610              mov       [esi+10h],ax

LAB_004d8ee1:
  004d8ee1  5f                    pop       edi
  004d8ee2  5e                    pop       esi
  004d8ee3  5d                    pop       ebp
  004d8ee4  5b                    pop       ebx
  004d8ee5  81c480000000          add       esp,80h
  004d8eeb  c3                    ret

71 cible(s) de saut interne : 0x004d8121, 0x004d826a, 0x004d828c, 0x004d8309, 0x004d830d, 0x004d8327, 0x004d832c, 0x004d834c, 0x004d8360, 0x004d838d, 0x004d8399, 0x004d83aa, 0x004d83b3, 0x004d83f7, 0x004d83fb, 0x004d8487, 0x004d848b, 0x004d84c2, 0x004d84d5, 0x004d8510, 0x004d8534, 0x004d8555, 0x004d85a2, 0x004d85c2, 0x004d85ce, 0x004d8606, 0x004d8633, 0x004d86b2, 0x004d86bd, 0x004d86c1, 0x004d8700, 0x004d8774, 0x004d87a4, 0x004d87b0, 0x004d8802, 0x004d882d, 0x004d8843, 0x004d886b, 0x004d88b2, 0x004d88d6, 0x004d88f7, 0x004d88f9, 0x004d8950, 0x004d8972, 0x004d8a4c, 0x004d8a58, 0x004d8a5a, 0x004d8a6b, 0x004d8aa9, 0x004d8b39, 0x004d8b49, 0x004d8b71, 0x004d8b9c, 0x004d8c5a, 0x004d8c6d, 0x004d8c7b, 0x004d8c85, 0x004d8cb5, 0x004d8cd2, 0x004d8d1a, 0x004d8d1f, 0x004d8d3e, 0x004d8d5a, 0x004d8d8c, 0x004d8ddb, 0x004d8e09, 0x004d8e4b, 0x004d8e76, 0x004d8eaa, 0x004d8ed0, 0x004d8ee1
