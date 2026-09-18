C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d7200  section .text  offset fichier 0xd6600  3717 octets

  004d7200  81ec8c000000          sub       esp,8Ch
  004d7206  53                    push      ebx
  004d7207  55                    push      ebp
  004d7208  56                    push      esi
  004d7209  8bb4249c000000        mov       esi,[esp+9Ch]
  004d7210  b864000000            mov       eax,64h
  004d7215  33db                  xor       ebx,ebx
  004d7217  6689442432            mov       [esp+32h],ax
  004d721c  6689442436            mov       [esp+36h],ax
  004d7221  668944243e            mov       [esp+3Eh],ax
  004d7226  6689442442            mov       [esp+42h],ax
  004d722b  8a4641                mov       al,[esi+41h]
  004d722e  57                    push      edi
  004d722f  84c0                  test      al,al
  004d7231  66895c2430            mov       [esp+30h],bx
  004d7236  66c74424321e00        mov       word [esp+32h],1Eh
  004d723d  66c74424343200        mov       word [esp+34h],32h
  004d7244  66895c2438            mov       [esp+38h],bx
  004d7249  66895c243c            mov       [esp+3Ch],bx
  004d724e  66895c243e            mov       [esp+3Eh],bx
  004d7253  66c74424405000        mov       word [esp+40h],50h
  004d725a  66895c2444            mov       [esp+44h],bx
  004d725f  7510                  jne       short 004D7271h
  004d7261  53                    push      ebx
  004d7262  6a03                  push      3
  004d7264  56                    push      esi
  004d7265  c6464101              mov       byte [esi+41h],1
  004d7269  e8b2e7ffff            call      004D5A20h
  004d726e  83c40c                add       esp,0Ch

LAB_004d7271:
  004d7271  8b461c                mov       eax,[esi+1Ch]
  004d7274  8b4e20                mov       ecx,[esi+20h]
  004d7277  8b5624                mov       edx,[esi+24h]
  004d727a  8d6e1c                lea       ebp,[esi+1Ch]
  004d727d  89442424              mov       [esp+24h],eax
  004d7281  8b4664                mov       eax,[esi+64h]
  004d7284  894c2428              mov       [esp+28h],ecx
  004d7288  8b4e60                mov       ecx,[esi+60h]
  004d728b  8954242c              mov       [esp+2Ch],edx
  004d728f  668b5632              mov       dx,[esi+32h]
  004d7293  8944244c              mov       [esp+4Ch],eax
  004d7297  8b467c                mov       eax,[esi+7Ch]
  004d729a  894c2450              mov       [esp+50h],ecx
  004d729e  668b8e90000000        mov       cx,[esi+90h]
  004d72a5  668994248a000000      mov       [esp+8Ah],dx
  004d72ad  8b9680000000          mov       edx,[esi+80h]
  004d72b3  89442460              mov       [esp+60h],eax
  004d72b7  8b8684000000          mov       eax,[esi+84h]
  004d72bd  66894c2468            mov       [esp+68h],cx
  004d72c2  8b8e88000000          mov       ecx,[esi+88h]
  004d72c8  8b7e6c                mov       edi,[esi+6Ch]
  004d72cb  8954245c              mov       [esp+5Ch],edx
  004d72cf  8b968c000000          mov       edx,[esi+8Ch]
  004d72d5  89442448              mov       [esp+48h],eax
  004d72d9  894c2454              mov       [esp+54h],ecx
  004d72dd  8d442420              lea       eax,[esp+20h]
  004d72e1  89542464              mov       [esp+64h],edx
  004d72e5  8d4c246c              lea       ecx,[esp+6Ch]
  004d72e9  50                    push      eax
  004d72ea  8d542474              lea       edx,[esp+74h]
  004d72ee  51                    push      ecx
  004d72ef  52                    push      edx
  004d72f0  56                    push      esi
  004d72f1  e88a160600            call      00538980h
  004d72f6  83c410                add       esp,10h
  004d72f9  8d442458              lea       eax,[esp+58h]
  004d72fd  8d4c241c              lea       ecx,[esp+1Ch]
  004d7301  8d542418              lea       edx,[esp+18h]
  004d7305  50                    push      eax
  004d7306  51                    push      ecx
  004d7307  8d44241c              lea       eax,[esp+1Ch]
  004d730b  52                    push      edx
  004d730c  8d4c241c              lea       ecx,[esp+1Ch]
  004d7310  50                    push      eax
  004d7311  51                    push      ecx
  004d7312  56                    push      esi
  004d7313  e808ec0500            call      00535F20h
  004d7318  8b542470              mov       edx,[esp+70h]
  004d731c  83c418                add       esp,18h
  004d731f  c744241040000000      mov       dword [esp+10h],40h
  004d7327  52                    push      edx
  004d7328  56                    push      esi
  004d7329  e8b2eaffff            call      004D5DE0h
  004d732e  8b4500                mov       eax,[ebp]
  004d7331  8b571c                mov       edx,[edi+1Ch]
  004d7334  8b4e20                mov       ecx,[esi+20h]
  004d7337  2bc2                  sub       eax,edx
  004d7339  8b5720                mov       edx,[edi+20h]
  004d733c  83c408                add       esp,8
  004d733f  2bca                  sub       ecx,edx
  004d7341  8b5624                mov       edx,[esi+24h]
  004d7344  2b5724                sub       edx,[edi+24h]
  004d7347  c1f80c                sar       eax,0Ch
  004d734a  8bf8                  mov       edi,eax
  004d734c  c1f90c                sar       ecx,0Ch
  004d734f  0faff8                imul      edi,eax
  004d7352  8bc1                  mov       eax,ecx
  004d7354  c1fa0c                sar       edx,0Ch
  004d7357  0fafc1                imul      eax,ecx
  004d735a  8bca                  mov       ecx,edx
  004d735c  03f8                  add       edi,eax
  004d735e  0fafca                imul      ecx,edx
  004d7361  03f9                  add       edi,ecx
  004d7363  57                    push      edi
  004d7364  e8a7e60600            call      00545A10h
  004d7369  89442414              mov       [esp+14h],eax
  004d736d  8b44241c              mov       eax,[esp+1Ch]
  004d7371  3bc3                  cmp       eax,ebx
  004d7373  0f8489070000          je        near 004D7B02h
  004d7379  8b0e                  mov       ecx,[esi]
  004d737b  33d2                  xor       edx,edx
  004d737d  8a5604                mov       dl,[esi+4]
  004d7380  81e1fffffffd          and       ecx,0FDFFFFFFh
  004d7386  0fbf7e18              movsx     edi,word [esi+18h]
  004d738a  890e                  mov       [esi],ecx
  004d738c  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004d7393  f7c100000001          test      ecx,1000000h
  004d7399  8b14ba                mov       edx,[edx+edi*4]
  004d739c  741b                  je        short 004D73B9h
  004d739e  66ff461a              inc       word [esi+1Ah]
  004d73a2  668b4e1a              mov       cx,[esi+1Ah]
  004d73a6  33ff                  xor       edi,edi
  004d73a8  668b7a16              mov       di,[edx+16h]
  004d73ac  0fbfc9                movsx     ecx,cx
  004d73af  3bcf                  cmp       ecx,edi
  004d73b1  7c2a                  jl        short 004D73DDh
  004d73b3  66895e1a              mov       [esi+1Ah],bx
  004d73b7  eb24                  jmp       short 004D73DDh

LAB_004d73b9:
  004d73b9  66ff461a              inc       word [esi+1Ah]
  004d73bd  668b7e1a              mov       di,[esi+1Ah]
  004d73c1  33db                  xor       ebx,ebx
  004d73c3  668b5a16              mov       bx,[edx+16h]
  004d73c7  0fbfd7                movsx     edx,di
  004d73ca  3bd3                  cmp       edx,ebx
  004d73cc  7c0d                  jl        short 004D73DBh
  004d73ce  4f                    dec       edi
  004d73cf  81c900000002          or        ecx,2000000h
  004d73d5  66897e1a              mov       [esi+1Ah],di
  004d73d9  890e                  mov       [esi],ecx

LAB_004d73db:
  004d73db  33db                  xor       ebx,ebx

LAB_004d73dd:
  004d73dd  f60602                test      byte [esi],2
  004d73e0  0f84ef010000          je        near 004D75D5h
  004d73e6  8a4e42                mov       cl,[esi+42h]
  004d73e9  80f90c                cmp       cl,0Ch
  004d73ec  0f8483000000          je        near 004D7475h
  004d73f2  80f90d                cmp       cl,0Dh
  004d73f5  747e                  je        short 004D7475h
  004d73f7  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004d73fc  7558                  jne       short 004D7456h
  004d73fe  8d442430              lea       eax,[esp+30h]
  004d7402  53                    push      ebx
  004d7403  50                    push      eax
  004d7404  55                    push      ebp
  004d7405  56                    push      esi
  004d7406  e825280400            call      00519C30h
  004d740b  8bf8                  mov       edi,eax
  004d740d  a1b8e05d00            mov       eax,[5DE0B8h]
  004d7412  83c410                add       esp,10h
  004d7415  3bc3                  cmp       eax,ebx
  004d7417  7439                  je        short 004D7452h
  004d7419  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004d741e  7532                  jne       short 004D7452h
  004d7420  8b0f                  mov       ecx,[edi]
  004d7422  57                    push      edi
  004d7423  83c940                or        ecx,40h
  004d7426  56                    push      esi
  004d7427  66c787960000001000    mov       word [edi+96h],10h
  004d7430  890f                  mov       [edi],ecx
  004d7432  e849270600            call      00539B80h
  004d7437  83c408                add       esp,8
  004d743a  83c71c                add       edi,1Ch
  004d743d  57                    push      edi
  004d743e  6800180000            push      1800h
  004d7443  6800030000            push      300h
  004d7448  6a03                  push      3
  004d744a  e8d1660600            call      0053DB20h
  004d744f  83c410                add       esp,10h

LAB_004d7452:
  004d7452  8b44241c              mov       eax,[esp+1Ch]

LAB_004d7456:
  004d7456  8b0e                  mov       ecx,[esi]
  004d7458  f7c100000002          test      ecx,2000000h
  004d745e  7415                  je        short 004D7475h
  004d7460  f6c101                test      cl,1
  004d7463  750b                  jne       short 004D7470h
  004d7465  83e1fd                and       ecx,0FFFFFFFDh
  004d7468  c6464300              mov       byte [esi+43h],0
  004d746c  890e                  mov       [esi],ecx
  004d746e  eb05                  jmp       short 004D7475h

LAB_004d7470:
  004d7470  83e1fd                and       ecx,0FFFFFFFDh
  004d7473  890e                  mov       [esi],ecx

LAB_004d7475:
  004d7475  807e420c              cmp       byte [esi+42h],0Ch
  004d7479  0f8582000000          jne       near 004D7501h
  004d747f  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004d7484  7c7b                  jl        short 004D7501h
  004d7486  8d4c2430              lea       ecx,[esp+30h]
  004d748a  53                    push      ebx
  004d748b  51                    push      ecx
  004d748c  55                    push      ebp
  004d748d  56                    push      esi
  004d748e  e89d270400            call      00519C30h
  004d7493  8bf8                  mov       edi,eax
  004d7495  a1b8e05d00            mov       eax,[5DE0B8h]
  004d749a  83c410                add       esp,10h
  004d749d  3bc3                  cmp       eax,ebx
  004d749f  745c                  je        short 004D74FDh
  004d74a1  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004d74a6  7555                  jne       short 004D74FDh
  004d74a8  8b1f                  mov       ebx,[edi]
  004d74aa  66c787960000000600    mov       word [edi+96h],6
  004d74b3  83cb40                or        ebx,40h
  004d74b6  57                    push      edi
  004d74b7  891f                  mov       [edi],ebx
  004d74b9  8b4c2424              mov       ecx,[esp+24h]
  004d74bd  56                    push      esi
  004d74be  668b4130              mov       ax,[ecx+30h]
  004d74c2  0fbfd8                movsx     ebx,ax
  004d74c5  66c1f802              sar       ax,2
  004d74c9  66894130              mov       [ecx+30h],ax
  004d74cd  e8ae260600            call      00539B80h
  004d74d2  8b542428              mov       edx,[esp+28h]
  004d74d6  83c408                add       esp,8
  004d74d9  83c71c                add       edi,1Ch
  004d74dc  57                    push      edi
  004d74dd  6800180000            push      1800h
  004d74e2  6800030000            push      300h
  004d74e7  6a03                  push      3
  004d74e9  66895a30              mov       [edx+30h],bx
  004d74ed  e82e660600            call      0053DB20h
  004d74f2  8b44242c              mov       eax,[esp+2Ch]
  004d74f6  83c410                add       esp,10h
  004d74f9  33db                  xor       ebx,ebx
  004d74fb  eb04                  jmp       short 004D7501h

LAB_004d74fd:
  004d74fd  8b44241c              mov       eax,[esp+1Ch]

LAB_004d7501:
  004d7501  807e420d              cmp       byte [esi+42h],0Dh
  004d7505  0f85ca000000          jne       near 004D75D5h
  004d750b  8d44243c              lea       eax,[esp+3Ch]
  004d750f  53                    push      ebx
  004d7510  50                    push      eax
  004d7511  55                    push      ebp
  004d7512  56                    push      esi
  004d7513  e818270400            call      00519C30h
  004d7518  8bf8                  mov       edi,eax
  004d751a  a1b8e05d00            mov       eax,[5DE0B8h]
  004d751f  83c410                add       esp,10h
  004d7522  3bc3                  cmp       eax,ebx
  004d7524  0f84a7000000          je        near 004D75D1h
  004d752a  8b1f                  mov       ebx,[edi]
  004d752c  66c787960000001000    mov       word [edi+96h],10h
  004d7535  83cb40                or        ebx,40h
  004d7538  57                    push      edi
  004d7539  891f                  mov       [edi],ebx
  004d753b  8b4c2424              mov       ecx,[esp+24h]
  004d753f  56                    push      esi
  004d7540  668b4130              mov       ax,[ecx+30h]
  004d7544  0fbfd8                movsx     ebx,ax
  004d7547  66d1f8                sar       ax,1
  004d754a  66894130              mov       [ecx+30h],ax
  004d754e  e82d260600            call      00539B80h
  004d7553  8b4c2428              mov       ecx,[esp+28h]
  004d7557  83c408                add       esp,8
  004d755a  8d571c                lea       edx,[edi+1Ch]
  004d755d  52                    push      edx
  004d755e  6800180000            push      1800h
  004d7563  6800030000            push      300h
  004d7568  6a03                  push      3
  004d756a  66895930              mov       [ecx+30h],bx
  004d756e  e8ad650600            call      0053DB20h
  004d7573  8b07                  mov       eax,[edi]
  004d7575  83c410                add       esp,10h
  004d7578  a900000040            test      eax,40000000h
  004d757d  7550                  jne       short 004D75CFh
  004d757f  6aff                  push      0FFFFFFFFh
  004d7581  6a00                  push      0
  004d7583  6a39                  push      39h
  004d7585  56                    push      esi
  004d7586  e84534faff            call      0047A9D0h
  004d758b  8b07                  mov       eax,[edi]
  004d758d  83c410                add       esp,10h
  004d7590  0c01                  or        al,1
  004d7592  8907                  mov       [edi],eax
  004d7594  0fbf4632              movsx     eax,word [esi+32h]
  004d7598  50                    push      eax
  004d7599  e87ee40600            call      00545A1Ch
  004d759e  8b4e5c                mov       ecx,[esi+5Ch]
  004d75a1  c1f90b                sar       ecx,0Bh
  004d75a4  0fafc1                imul      eax,ecx
  004d75a7  894750                mov       [edi+50h],eax
  004d75aa  0fbf5632              movsx     edx,word [esi+32h]
  004d75ae  52                    push      edx
  004d75af  e862e40600            call      00545A16h
  004d75b4  8b4e5c                mov       ecx,[esi+5Ch]
  004d75b7  c7475400800100        mov       dword [edi+54h],18000h
  004d75be  c1f90b                sar       ecx,0Bh
  004d75c1  0fafc1                imul      eax,ecx
  004d75c4  894758                mov       [edi+58h],eax
  004d75c7  8b44241c              mov       eax,[esp+1Ch]
  004d75cb  33db                  xor       ebx,ebx
  004d75cd  eb06                  jmp       short 004D75D5h

LAB_004d75cf:
  004d75cf  33db                  xor       ebx,ebx

LAB_004d75d1:
  004d75d1  8b44241c              mov       eax,[esp+1Ch]

LAB_004d75d5:
  004d75d5  33c9                  xor       ecx,ecx
  004d75d7  8a4e42                mov       cl,[esi+42h]
  004d75da  83f90e                cmp       ecx,0Eh
  004d75dd  0f871f050000          ja        near 004D7B02h
  004d75e3  ff248d88804d00        jmp       dword [ecx*4+4D8088h]
  004d75ea  66395e12              cmp       [esi+12h],bx
  004d75ee  751a                  jne       short 004D760Ah
  004d75f0  8b06                  mov       eax,[esi]
  004d75f2  66895e18              mov       [esi+18h],bx
  004d75f6  0d00000001            or        eax,1000000h
  004d75fb  66895e1a              mov       [esi+1Ah],bx
  004d75ff  8906                  mov       [esi],eax
  004d7601  66c746120100          mov       word [esi+12h],1
  004d7607  895e5c                mov       [esi+5Ch],ebx

LAB_004d760a:
  004d760a  0fbf5610              movsx     edx,word [esi+10h]
  004d760e  39542414              cmp       [esp+14h],edx
  004d7612  7d09                  jge       short 004D761Dh
  004d7614  c6464300              mov       byte [esi+43h],0
  004d7618  e9e5040000            jmp       004D7B02h

LAB_004d761d:
  004d761d  8a4643                mov       al,[esi+43h]
  004d7620  84c0                  test      al,al
  004d7622  0f84d7040000          je        near 004D7AFFh
  004d7628  fec8                  dec       al
  004d762a  884643                mov       [esi+43h],al
  004d762d  e9cd040000            jmp       004D7AFFh
  004d7632  66395e12              cmp       [esi+12h],bx
  004d7636  751b                  jne       short 004D7653h
  004d7638  8b0e                  mov       ecx,[esi]
  004d763a  b801000000            mov       eax,1
  004d763f  81c900000001          or        ecx,1000000h
  004d7645  66894618              mov       [esi+18h],ax
  004d7649  66895e1a              mov       [esi+1Ah],bx
  004d764d  890e                  mov       [esi],ecx
  004d764f  66894612              mov       [esi+12h],ax

LAB_004d7653:
  004d7653  8b442418              mov       eax,[esp+18h]
  004d7657  8d88c8000000          lea       ecx,[eax+0C8h]
  004d765d  81e1ff0f0000          and       ecx,0FFFh
  004d7663  81f990010000          cmp       ecx,190h
  004d7669  7f09                  jg        short 004D7674h
  004d766b  c6464300              mov       byte [esi+43h],0
  004d766f  e98b040000            jmp       004D7AFFh

LAB_004d7674:
  004d7674  25ff0f0000            and       eax,0FFFh
  004d7679  3d00080000            cmp       eax,800h
  004d767e  7d13                  jge       short 004D7693h
  004d7680  8b542410              mov       edx,[esp+10h]
  004d7684  8d8280000000          lea       eax,[edx+80h]
  004d768a  66014632              add       [esi+32h],ax
  004d768e  e96c040000            jmp       004D7AFFh

LAB_004d7693:
  004d7693  8b4c2410              mov       ecx,[esp+10h]
  004d7697  b880ffffff            mov       eax,0FFFFFF80h
  004d769c  2bc1                  sub       eax,ecx
  004d769e  66014632              add       [esi+32h],ax
  004d76a2  e958040000            jmp       004D7AFFh
  004d76a7  66395e12              cmp       [esi+12h],bx
  004d76ab  8b4c2410              mov       ecx,[esp+10h]
  004d76af  b801000000            mov       eax,1
  004d76b4  7525                  jne       short 004D76DBh
  004d76b6  8b16                  mov       edx,[esi]
  004d76b8  66894618              mov       [esi+18h],ax
  004d76bc  81ca00000001          or        edx,1000000h
  004d76c2  66895e1a              mov       [esi+1Ah],bx
  004d76c6  8916                  mov       [esi],edx
  004d76c8  8d14c9                lea       edx,[ecx+ecx*8]
  004d76cb  c1e204                shl       edx,4
  004d76ce  81c200a00000          add       edx,0A000h
  004d76d4  66894612              mov       [esi+12h],ax
  004d76d8  89565c                mov       [esi+5Ch],edx

LAB_004d76db:
  004d76db  66394612              cmp       [esi+12h],ax
  004d76df  0f851d040000          jne       near 004D7B02h
  004d76e5  0fbf4610              movsx     eax,word [esi+10h]
  004d76e9  39442414              cmp       [esp+14h],eax
  004d76ed  7d09                  jge       short 004D76F8h
  004d76ef  c6464300              mov       byte [esi+43h],0
  004d76f3  e907040000            jmp       004D7AFFh

LAB_004d76f8:
  004d76f8  8a4643                mov       al,[esi+43h]
  004d76fb  84c0                  test      al,al
  004d76fd  7405                  je        short 004D7704h
  004d76ff  fec8                  dec       al
  004d7701  884643                mov       [esi+43h],al

LAB_004d7704:
  004d7704  8b442418              mov       eax,[esp+18h]
  004d7708  8d9000010000          lea       edx,[eax+100h]
  004d770e  81e2ff0f0000          and       edx,0FFFh
  004d7714  81fa00020000          cmp       edx,200h
  004d771a  0f8ee2030000          jle       near 004D7B02h
  004d7720  25ff0f0000            and       eax,0FFFh
  004d7725  3d00080000            cmp       eax,800h
  004d772a  7d10                  jge       short 004D773Ch
  004d772c  8d840980000000        lea       eax,[ecx+ecx+80h]
  004d7733  66014632              add       [esi+32h],ax
  004d7737  e9c6030000            jmp       004D7B02h

LAB_004d773c:
  004d773c  03c9                  add       ecx,ecx
  004d773e  b880ffffff            mov       eax,0FFFFFF80h
  004d7743  2bc1                  sub       eax,ecx
  004d7745  66014632              add       [esi+32h],ax
  004d7749  e9b4030000            jmp       004D7B02h
  004d774e  66837e1201            cmp       word [esi+12h],1
  004d7753  0f8596000000          jne       near 004D77EFh
  004d7759  8a4e43                mov       cl,[esi+43h]
  004d775c  84c9                  test      cl,cl
  004d775e  7405                  je        short 004D7765h
  004d7760  fec9                  dec       cl
  004d7762  884e43                mov       [esi+43h],cl

LAB_004d7765:
  004d7765  8b4c2410              mov       ecx,[esp+10h]
  004d7769  8d14c9                lea       edx,[ecx+ecx*8]
  004d776c  c1e204                shl       edx,4
  004d776f  8d8a00700000          lea       ecx,[edx+7000h]
  004d7775  0fafc1                imul      eax,ecx
  004d7778  c1f80c                sar       eax,0Ch
  004d777b  8bf8                  mov       edi,eax
  004d777d  894e5c                mov       [esi+5Ch],ecx
  004d7780  0fbf4610              movsx     eax,word [esi+10h]
  004d7784  50                    push      eax
  004d7785  e892e20600            call      00545A1Ch
  004d778a  0fafc7                imul      eax,edi
  004d778d  8b4d00                mov       ecx,[ebp]
  004d7790  c1f80c                sar       eax,0Ch
  004d7793  03c8                  add       ecx,eax
  004d7795  894d00                mov       [ebp],ecx
  004d7798  0fbf4e10              movsx     ecx,word [esi+10h]
  004d779c  51                    push      ecx
  004d779d  e874e20600            call      00545A16h
  004d77a2  0fafc7                imul      eax,edi
  004d77a5  8b6e24                mov       ebp,[esi+24h]
  004d77a8  895e5c                mov       [esi+5Ch],ebx
  004d77ab  c1f80c                sar       eax,0Ch
  004d77ae  03e8                  add       ebp,eax
  004d77b0  8b442418              mov       eax,[esp+18h]
  004d77b4  896e24                mov       [esi+24h],ebp
  004d77b7  8d9000010000          lea       edx,[eax+100h]
  004d77bd  81e2ff0f0000          and       edx,0FFFh
  004d77c3  81fa00020000          cmp       edx,200h
  004d77c9  7e24                  jle       short 004D77EFh
  004d77cb  25ff0f0000            and       eax,0FFFh
  004d77d0  3d00080000            cmp       eax,800h
  004d77d5  7d09                  jge       short 004D77E0h
  004d77d7  8b442410              mov       eax,[esp+10h]
  004d77db  83c032                add       eax,32h
  004d77de  eb0b                  jmp       short 004D77EBh

LAB_004d77e0:
  004d77e0  8b4c2410              mov       ecx,[esp+10h]
  004d77e4  b8ceffffff            mov       eax,0FFFFFFCEh
  004d77e9  2bc1                  sub       eax,ecx

LAB_004d77eb:
  004d77eb  66014632              add       [esi+32h],ax

LAB_004d77ef:
  004d77ef  66395e12              cmp       [esi+12h],bx
  004d77f3  0f8509030000          jne       near 004D7B02h
  004d77f9  8b0e                  mov       ecx,[esi]
  004d77fb  b801000000            mov       eax,1
  004d7800  81c900000001          or        ecx,1000000h
  004d7806  66894618              mov       [esi+18h],ax
  004d780a  66895e1a              mov       [esi+1Ah],bx
  004d780e  890e                  mov       [esi],ecx
  004d7810  66894612              mov       [esi+12h],ax
  004d7814  e9e9020000            jmp       004D7B02h
  004d7819  66395e12              cmp       [esi+12h],bx
  004d781d  757f                  jne       short 004D789Eh
  004d781f  8b0e                  mov       ecx,[esi]
  004d7821  f6c101                test      cl,1
  004d7824  7406                  je        short 004D782Ch
  004d7826  c6464300              mov       byte [esi+43h],0
  004d782a  eb72                  jmp       short 004D789Eh

LAB_004d782c:
  004d782c  81e1fffffffe          and       ecx,0FEFFFFFFh
  004d7832  66c746180400          mov       word [esi+18h],4
  004d7838  890e                  mov       [esi],ecx
  004d783a  8b4c2410              mov       ecx,[esp+10h]
  004d783e  81c180020000          add       ecx,280h
  004d7844  66895e1a              mov       [esi+1Ah],bx
  004d7848  c1e106                shl       ecx,6
  004d784b  0fafc1                imul      eax,ecx
  004d784e  0fbf5610              movsx     edx,word [esi+10h]
  004d7852  c1f80c                sar       eax,0Ch
  004d7855  52                    push      edx
  004d7856  894e5c                mov       [esi+5Ch],ecx
  004d7859  8bf8                  mov       edi,eax
  004d785b  e8bce10600            call      00545A1Ch
  004d7860  0fafc7                imul      eax,edi
  004d7863  8b6e50                mov       ebp,[esi+50h]
  004d7866  c1f80c                sar       eax,0Ch
  004d7869  03e8                  add       ebp,eax
  004d786b  0fbf4610              movsx     eax,word [esi+10h]
  004d786f  50                    push      eax
  004d7870  896e50                mov       [esi+50h],ebp
  004d7873  e89ee10600            call      00545A16h
  004d7878  0fafc7                imul      eax,edi
  004d787b  8b5658                mov       edx,[esi+58h]
  004d787e  8b4e54                mov       ecx,[esi+54h]
  004d7881  c1f80c                sar       eax,0Ch
  004d7884  03d0                  add       edx,eax
  004d7886  8b06                  mov       eax,[esi]
  004d7888  81c100e00100          add       ecx,1E000h
  004d788e  0c01                  or        al,1
  004d7890  895658                mov       [esi+58h],edx
  004d7893  894e54                mov       [esi+54h],ecx
  004d7896  8906                  mov       [esi],eax
  004d7898  66c746120100          mov       word [esi+12h],1

LAB_004d789e:
  004d789e  f60601                test      byte [esi],1
  004d78a1  0f855b020000          jne       near 004D7B02h
  004d78a7  c6464300              mov       byte [esi+43h],0
  004d78ab  e952020000            jmp       004D7B02h
  004d78b0  668b4e12              mov       cx,[esi+12h]
  004d78b4  ba01000000            mov       edx,1
  004d78b9  663bca                cmp       cx,dx
  004d78bc  751c                  jne       short 004D78DAh
  004d78be  0fbf4610              movsx     eax,word [esi+10h]
  004d78c2  39442414              cmp       [esp+14h],eax
  004d78c6  7d06                  jge       short 004D78CEh
  004d78c8  c6464300              mov       byte [esi+43h],0
  004d78cc  eb0c                  jmp       short 004D78DAh

LAB_004d78ce:
  004d78ce  8a4643                mov       al,[esi+43h]
  004d78d1  84c0                  test      al,al
  004d78d3  7405                  je        short 004D78DAh
  004d78d5  fec8                  dec       al
  004d78d7  884643                mov       [esi+43h],al

LAB_004d78da:
  004d78da  663bcb                cmp       cx,bx
  004d78dd  0f851f020000          jne       near 004D7B02h
  004d78e3  8b442410              mov       eax,[esp+10h]
  004d78e7  8b3e                  mov       edi,[esi]
  004d78e9  81cf00000001          or        edi,1000000h
  004d78ef  66895618              mov       [esi+18h],dx
  004d78f3  8d0cc0                lea       ecx,[eax+eax*8]
  004d78f6  66895e1a              mov       [esi+1Ah],bx
  004d78fa  c1e104                shl       ecx,4
  004d78fd  81c100500000          add       ecx,5000h
  004d7903  893e                  mov       [esi],edi
  004d7905  66895612              mov       [esi+12h],dx
  004d7909  894e5c                mov       [esi+5Ch],ecx
  004d790c  e9f1010000            jmp       004D7B02h
  004d7911  668b4e12              mov       cx,[esi+12h]
  004d7915  6683f901              cmp       cx,1
  004d7919  7511                  jne       short 004D792Ch
  004d791b  8b06                  mov       eax,[esi]
  004d791d  a900000002            test      eax,2000000h
  004d7922  7408                  je        short 004D792Ch
  004d7924  24fd                  and       al,0FDh
  004d7926  c6464200              mov       byte [esi+42h],0
  004d792a  8906                  mov       [esi],eax

LAB_004d792c:
  004d792c  663bcb                cmp       cx,bx
  004d792f  0f85cd010000          jne       near 004D7B02h
  004d7935  8b06                  mov       eax,[esi]
  004d7937  66c746120100          mov       word [esi+12h],1
  004d793d  25fffffffe            and       eax,0FEFFFFFFh
  004d7942  8906                  mov       [esi],eax
  004d7944  e9b9010000            jmp       004D7B02h
  004d7949  668b4612              mov       ax,[esi+12h]
  004d794d  663d0100              cmp       ax,1
  004d7951  751a                  jne       short 004D796Dh
  004d7953  f70600000002          test      dword [esi],2000000h
  004d7959  7404                  je        short 004D795Fh
  004d795b  c6464300              mov       byte [esi+43h],0

LAB_004d795f:
  004d795f  817c241496000000      cmp       dword [esp+14h],96h
  004d7967  7e04                  jle       short 004D796Dh
  004d7969  c6464300              mov       byte [esi+43h],0

LAB_004d796d:
  004d796d  663bc3                cmp       ax,bx
  004d7970  0f858c010000          jne       near 004D7B02h
  004d7976  8b16                  mov       edx,[esi]
  004d7978  66c746120100          mov       word [esi+12h],1
  004d797e  81e2fffffffe          and       edx,0FEFFFFFFh
  004d7984  66c746180300          mov       word [esi+18h],3
  004d798a  83ca02                or        edx,2
  004d798d  66895e1a              mov       [esi+1Ah],bx
  004d7991  8916                  mov       [esi],edx
  004d7993  e967010000            jmp       004D7AFFh
  004d7998  668b4e12              mov       cx,[esi+12h]
  004d799c  6683f902              cmp       cx,2
  004d79a0  7509                  jne       short 004D79ABh
  004d79a2  f60601                test      byte [esi],1
  004d79a5  7504                  jne       short 004D79ABh
  004d79a7  c6464300              mov       byte [esi+43h],0

LAB_004d79ab:
  004d79ab  6683f901              cmp       cx,1
  004d79af  0f858d000000          jne       near 004D7A42h
  004d79b5  8b4c2418              mov       ecx,[esp+18h]
  004d79b9  81e1ff0f0000          and       ecx,0FFFh
  004d79bf  81f900080000          cmp       ecx,800h
  004d79c5  7d09                  jge       short 004D79D0h
  004d79c7  8b4c2410              mov       ecx,[esp+10h]
  004d79cb  8d5132                lea       edx,[ecx+32h]
  004d79ce  eb0b                  jmp       short 004D79DBh

LAB_004d79d0:
  004d79d0  8b4c2410              mov       ecx,[esp+10h]
  004d79d4  baceffffff            mov       edx,0FFFFFFCEh
  004d79d9  2bd1                  sub       edx,ecx

LAB_004d79db:
  004d79db  66015632              add       [esi+32h],dx
  004d79df  66837e1a06            cmp       word [esi+1Ah],6
  004d79e4  7c5c                  jl        short 004D7A42h
  004d79e6  81c1c0030000          add       ecx,3C0h
  004d79ec  c1e106                shl       ecx,6
  004d79ef  0fafc1                imul      eax,ecx
  004d79f2  0fbf5610              movsx     edx,word [esi+10h]
  004d79f6  c1f80c                sar       eax,0Ch
  004d79f9  52                    push      edx
  004d79fa  894e5c                mov       [esi+5Ch],ecx
  004d79fd  8bf8                  mov       edi,eax
  004d79ff  e818e00600            call      00545A1Ch
  004d7a04  0fafc7                imul      eax,edi
  004d7a07  8b4e50                mov       ecx,[esi+50h]
  004d7a0a  c1f80c                sar       eax,0Ch
  004d7a0d  03c8                  add       ecx,eax
  004d7a0f  0fbf4610              movsx     eax,word [esi+10h]
  004d7a13  50                    push      eax
  004d7a14  894e50                mov       [esi+50h],ecx
  004d7a17  e8fadf0600            call      00545A16h
  004d7a1c  0fafc7                imul      eax,edi
  004d7a1f  8b5658                mov       edx,[esi+58h]
  004d7a22  8b4e54                mov       ecx,[esi+54h]
  004d7a25  c1f80c                sar       eax,0Ch
  004d7a28  03d0                  add       edx,eax
  004d7a2a  8b06                  mov       eax,[esi]
  004d7a2c  81c100e00100          add       ecx,1E000h
  004d7a32  0c03                  or        al,3
  004d7a34  895658                mov       [esi+58h],edx
  004d7a37  894e54                mov       [esi+54h],ecx
  004d7a3a  8906                  mov       [esi],eax
  004d7a3c  66c746120200          mov       word [esi+12h],2

LAB_004d7a42:
  004d7a42  66395e12              cmp       [esi+12h],bx
  004d7a46  0f85b6000000          jne       near 004D7B02h
  004d7a4c  8b06                  mov       eax,[esi]
  004d7a4e  668b4c2418            mov       cx,[esp+18h]
  004d7a53  25fffffffe            and       eax,0FEFFFFFFh
  004d7a58  66c746120100          mov       word [esi+12h],1
  004d7a5e  66894e10              mov       [esi+10h],cx
  004d7a62  66c746180500          mov       word [esi+18h],5
  004d7a68  66895e1a              mov       [esi+1Ah],bx
  004d7a6c  8906                  mov       [esi],eax
  004d7a6e  e98f000000            jmp       004D7B02h
  004d7a73  668b4612              mov       ax,[esi+12h]
  004d7a77  663d0200              cmp       ax,2
  004d7a7b  750b                  jne       short 004D7A88h
  004d7a7d  8a4e43                mov       cl,[esi+43h]
  004d7a80  895e5c                mov       [esi+5Ch],ebx
  004d7a83  fec9                  dec       cl
  004d7a85  884e43                mov       [esi+43h],cl

LAB_004d7a88:
  004d7a88  663d0100              cmp       ax,1
  004d7a8c  7552                  jne       short 004D7AE0h
  004d7a8e  8b06                  mov       eax,[esi]
  004d7a90  895e5c                mov       [esi+5Ch],ebx
  004d7a93  a900000002            test      eax,2000000h
  004d7a98  7446                  je        short 004D7AE0h
  004d7a9a  8b15cc975e00          mov       edx,[5E97CCh]
  004d7aa0  6800200000            push      2000h
  004d7aa5  6800200000            push      2000h
  004d7aaa  6800040000            push      400h
  004d7aaf  8b82a8000000          mov       eax,[edx+0A8h]
  004d7ab5  33d2                  xor       edx,edx
  004d7ab7  8b08                  mov       ecx,[eax]
  004d7ab9  8a512c                mov       dl,[ecx+2Ch]
  004d7abc  52                    push      edx
  004d7abd  e8de530600            call      0053CEA0h
  004d7ac2  83c410                add       esp,10h
  004d7ac5  6aff                  push      0FFFFFFFFh
  004d7ac7  53                    push      ebx
  004d7ac8  68de000000            push      0DEh
  004d7acd  56                    push      esi
  004d7ace  e8fd2efaff            call      0047A9D0h
  004d7ad3  83c410                add       esp,10h
  004d7ad6  66c746120200          mov       word [esi+12h],2
  004d7adc  c6464314              mov       byte [esi+43h],14h

LAB_004d7ae0:
  004d7ae0  66395e12              cmp       [esi+12h],bx
  004d7ae4  751c                  jne       short 004D7B02h
  004d7ae6  8b06                  mov       eax,[esi]
  004d7ae8  66c746120100          mov       word [esi+12h],1
  004d7aee  25fffffffe            and       eax,0FEFFFFFFh
  004d7af3  66c746180600          mov       word [esi+18h],6
  004d7af9  66895e1a              mov       [esi+1Ah],bx
  004d7afd  8906                  mov       [esi],eax

LAB_004d7aff:
  004d7aff  895e5c                mov       [esi+5Ch],ebx

LAB_004d7b02:
  004d7b02  0fbf4632              movsx     eax,word [esi+32h]
  004d7b06  50                    push      eax
  004d7b07  e810df0600            call      00545A1Ch
  004d7b0c  0fbf4e32              movsx     ecx,word [esi+32h]
  004d7b10  51                    push      ecx
  004d7b11  89442478              mov       [esp+78h],eax
  004d7b15  e8fcde0600            call      00545A16h
  004d7b1a  8b5650                mov       edx,[esi+50h]
  004d7b1d  8944247c              mov       [esp+7Ch],eax
  004d7b21  8bc2                  mov       eax,edx
  004d7b23  8b6e54                mov       ebp,[esi+54h]
  004d7b26  c1f808                sar       eax,8
  004d7b29  8bfa                  mov       edi,edx
  004d7b2b  8b4c241c              mov       ecx,[esp+1Ch]
  004d7b2f  2bf8                  sub       edi,eax
  004d7b31  0faf4e5c              imul      ecx,[esi+5Ch]
  004d7b35  c1fa03                sar       edx,3
  004d7b38  2bfa                  sub       edi,edx
  004d7b3a  8bdd                  mov       ebx,ebp
  004d7b3c  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004d7b43  8bc2                  mov       eax,edx
  004d7b45  897e50                mov       [esi+50h],edi
  004d7b48  c1f808                sar       eax,8
  004d7b4b  c1fb08                sar       ebx,8
  004d7b4e  2bc3                  sub       eax,ebx
  004d7b50  56                    push      esi
  004d7b51  2bc2                  sub       eax,edx
  004d7b53  8b5658                mov       edx,[esi+58h]
  004d7b56  8bda                  mov       ebx,edx
  004d7b58  03c5                  add       eax,ebp
  004d7b5a  c1fb03                sar       ebx,3
  004d7b5d  8bea                  mov       ebp,edx
  004d7b5f  894654                mov       [esi+54h],eax
  004d7b62  2beb                  sub       ebp,ebx
  004d7b64  8b5e1c                mov       ebx,[esi+1Ch]
  004d7b67  c1fa08                sar       edx,8
  004d7b6a  c1f90c                sar       ecx,0Ch
  004d7b6d  2bea                  sub       ebp,edx
  004d7b6f  8bd1                  mov       edx,ecx
  004d7b71  0faf542478            imul      edx,[esp+78h]
  004d7b76  0faf8c2480000000      imul      ecx,[esp+80h]
  004d7b7e  c1fa0c                sar       edx,0Ch
  004d7b81  c1f90c                sar       ecx,0Ch
  004d7b84  896e58                mov       [esi+58h],ebp
  004d7b87  03d7                  add       edx,edi
  004d7b89  8b7e20                mov       edi,[esi+20h]
  004d7b8c  03cd                  add       ecx,ebp
  004d7b8e  8b6e24                mov       ebp,[esi+24h]
  004d7b91  03da                  add       ebx,edx
  004d7b93  03f8                  add       edi,eax
  004d7b95  03e9                  add       ebp,ecx
  004d7b97  895e1c                mov       [esi+1Ch],ebx
  004d7b9a  897e20                mov       [esi+20h],edi
  004d7b9d  896e24                mov       [esi+24h],ebp
  004d7ba0  e89bbd0500            call      00533940h
  004d7ba5  8b9690000000          mov       edx,[esi+90h]
  004d7bab  8b4660                mov       eax,[esi+60h]
  004d7bae  8b4e20                mov       ecx,[esi+20h]
  004d7bb1  83c404                add       esp,4
  004d7bb4  80e6fc                and       dh,0FCh
  004d7bb7  3bc8                  cmp       ecx,eax
  004d7bb9  899690000000          mov       [esi+90h],edx
  004d7bbf  7f2a                  jg        short 004D7BEBh
  004d7bc1  f6c680                test      dh,80h
  004d7bc4  7525                  jne       short 004D7BEBh
  004d7bc6  8b0e                  mov       ecx,[esi]
  004d7bc8  894620                mov       [esi+20h],eax
  004d7bcb  f6c101                test      cl,1
  004d7bce  740f                  je        short 004D7BDFh
  004d7bd0  83e1fe                and       ecx,0FFFFFFFEh
  004d7bd3  33db                  xor       ebx,ebx
  004d7bd5  890e                  mov       [esi],ecx
  004d7bd7  895e54                mov       [esi+54h],ebx
  004d7bda  e923010000            jmp       004D7D02h

LAB_004d7bdf:
  004d7bdf  83e1fe                and       ecx,0FFFFFFFEh
  004d7be2  33db                  xor       ebx,ebx
  004d7be4  890e                  mov       [esi],ecx
  004d7be6  895e54                mov       [esi+54h],ebx
  004d7be9  eb02                  jmp       short 004D7BEDh

LAB_004d7beb:
  004d7beb  33db                  xor       ebx,ebx

LAB_004d7bed:
  004d7bed  8b0e                  mov       ecx,[esi]
  004d7bef  f6c101                test      cl,1
  004d7bf2  750a                  jne       short 004D7BFEh
  004d7bf4  394620                cmp       [esi+20h],eax
  004d7bf7  7e05                  jle       short 004D7BFEh
  004d7bf9  83c901                or        ecx,1
  004d7bfc  890e                  mov       [esi],ecx

LAB_004d7bfe:
  004d7bfe  f7c282ad0020          test      edx,2000AD82h
  004d7c04  0f84f8000000          je        near 004D7D02h
  004d7c0a  f60601                test      byte [esi],1
  004d7c0d  0f85c7000000          jne       near 004D7CDAh
  004d7c13  807e4207              cmp       byte [esi+42h],7
  004d7c17  7421                  je        short 004D7C3Ah
  004d7c19  c6464207              mov       byte [esi+42h],7
  004d7c1d  e81ef50600            call      00547140h
  004d7c22  99                    cdq
  004d7c23  b928000000            mov       ecx,28h
  004d7c28  66895e12              mov       [esi+12h],bx
  004d7c2c  f7f9                  idiv      ecx
  004d7c2e  66c746105000          mov       word [esi+10h],50h
  004d7c34  80c214                add       dl,14h
  004d7c37  885643                mov       [esi+43h],dl

LAB_004d7c3a:
  004d7c3a  8b16                  mov       edx,[esi]
  004d7c3c  8b442428              mov       eax,[esp+28h]
  004d7c40  8b4c242c              mov       ecx,[esp+2Ch]
  004d7c44  83e2fd                and       edx,0FFFFFFFDh
  004d7c47  8916                  mov       [esi],edx
  004d7c49  8b542424              mov       edx,[esp+24h]
  004d7c4d  894620                mov       [esi+20h],eax
  004d7c50  8b442468              mov       eax,[esp+68h]
  004d7c54  89561c                mov       [esi+1Ch],edx
  004d7c57  8b542460              mov       edx,[esp+60h]
  004d7c5b  25ffff0000            and       eax,0FFFFh
  004d7c60  894e24                mov       [esi+24h],ecx
  004d7c63  8b4c2448              mov       ecx,[esp+48h]
  004d7c67  89567c                mov       [esi+7Ch],edx
  004d7c6a  8b542454              mov       edx,[esp+54h]
  004d7c6e  898690000000          mov       [esi+90h],eax
  004d7c74  8b44245c              mov       eax,[esp+5Ch]
  004d7c78  898e84000000          mov       [esi+84h],ecx
  004d7c7e  8b4c244c              mov       ecx,[esp+4Ch]
  004d7c82  898680000000          mov       [esi+80h],eax
  004d7c88  8b442464              mov       eax,[esp+64h]
  004d7c8c  899688000000          mov       [esi+88h],edx
  004d7c92  8b542450              mov       edx,[esp+50h]
  004d7c96  6882ad0020            push      2000AD82h
  004d7c9b  56                    push      esi
  004d7c9c  89868c000000          mov       [esi+8Ch],eax
  004d7ca2  894e64                mov       [esi+64h],ecx
  004d7ca5  895660                mov       [esi+60h],edx
  004d7ca8  e8b3e1ffff            call      004D5E60h
  004d7cad  83c408                add       esp,8
  004d7cb0  663bc3                cmp       ax,bx
  004d7cb3  7515                  jne       short 004D7CCAh
  004d7cb5  e886f40600            call      00547140h
  004d7cba  99                    cdq
  004d7cbb  33c2                  xor       eax,edx
  004d7cbd  2bc2                  sub       eax,edx
  004d7cbf  83e001                and       eax,1
  004d7cc2  33c2                  xor       eax,edx
  004d7cc4  2bc2                  sub       eax,edx
  004d7cc6  8d4400ff              lea       eax,[eax+eax-1]

LAB_004d7cca:
  004d7cca  8b4c2410              mov       ecx,[esp+10h]
  004d7cce  83c140                add       ecx,40h
  004d7cd1  0fafc8                imul      ecx,eax
  004d7cd4  66014e32              add       [esi+32h],cx
  004d7cd8  eb28                  jmp       short 004D7D02h

LAB_004d7cda:
  004d7cda  8b542424              mov       edx,[esp+24h]
  004d7cde  8b44242c              mov       eax,[esp+2Ch]
  004d7ce2  895e5c                mov       [esi+5Ch],ebx
  004d7ce5  895e50                mov       [esi+50h],ebx
  004d7ce8  895e58                mov       [esi+58h],ebx
  004d7ceb  89561c                mov       [esi+1Ch],edx
  004d7cee  894624                mov       [esi+24h],eax
  004d7cf1  885e42                mov       [esi+42h],bl
  004d7cf4  c6464328              mov       byte [esi+43h],28h
  004d7cf8  66895e12              mov       [esi+12h],bx
  004d7cfc  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004d7d02:
  004d7d02  6aff                  push      0FFFFFFFFh
  004d7d04  6aff                  push      0FFFFFFFFh
  004d7d06  6aff                  push      0FFFFFFFFh
  004d7d08  56                    push      esi
  004d7d09  e802bbfeff            call      004C3810h
  004d7d0e  8b44242c              mov       eax,[esp+2Ch]
  004d7d12  83c410                add       esp,10h
  004d7d15  3bc3                  cmp       eax,ebx
  004d7d17  7517                  jne       short 004D7D30h
  004d7d19  668b8c248a000000      mov       cx,[esp+8Ah]
  004d7d21  66894e32              mov       [esi+32h],cx
  004d7d25  5f                    pop       edi
  004d7d26  5e                    pop       esi
  004d7d27  5d                    pop       ebp
  004d7d28  5b                    pop       ebx
  004d7d29  81c48c000000          add       esp,8Ch
  004d7d2f  c3                    ret

LAB_004d7d30:
  004d7d30  385e43                cmp       [esi+43h],bl
  004d7d33  0f8541030000          jne       near 004D807Ah
  004d7d39  8b466c                mov       eax,[esi+6Ch]
  004d7d3c  8b561c                mov       edx,[esi+1Ch]
  004d7d3f  8b781c                mov       edi,[eax+1Ch]
  004d7d42  8b6824                mov       ebp,[eax+24h]
  004d7d45  8b4624                mov       eax,[esi+24h]
  004d7d48  2bfa                  sub       edi,edx
  004d7d4a  2be8                  sub       ebp,eax
  004d7d4c  c1ff0c                sar       edi,0Ch
  004d7d4f  c1fd0c                sar       ebp,0Ch
  004d7d52  8bd7                  mov       edx,edi
  004d7d54  8bc5                  mov       eax,ebp
  004d7d56  0fafd7                imul      edx,edi
  004d7d59  0fafc5                imul      eax,ebp
  004d7d5c  03d0                  add       edx,eax
  004d7d5e  52                    push      edx
  004d7d5f  e8acdc0600            call      00545A10h
  004d7d64  55                    push      ebp
  004d7d65  57                    push      edi
  004d7d66  8944241c              mov       [esp+1Ch],eax
  004d7d6a  e8b3dc0600            call      00545A22h
  004d7d6f  668b4e32              mov       cx,[esi+32h]
  004d7d73  bd64000000            mov       ebp,64h
  004d7d78  2bc1                  sub       eax,ecx
  004d7d7a  66895e12              mov       [esi+12h],bx
  004d7d7e  25ff0f0000            and       eax,0FFFh
  004d7d83  89442418              mov       [esp+18h],eax
  004d7d87  8b442414              mov       eax,[esp+14h]
  004d7d8b  3bc5                  cmp       eax,ebp
  004d7d8d  0f8dc0000000          jge       near 004D7E53h
  004d7d93  e8a8f30600            call      00547140h
  004d7d98  99                    cdq
  004d7d99  8bcd                  mov       ecx,ebp
  004d7d9b  f7f9                  idiv      ecx
  004d7d9d  8bfa                  mov       edi,edx
  004d7d9f  83ff32                cmp       edi,32h
  004d7da2  7d62                  jge       short 004D7E06h
  004d7da4  8b542418              mov       edx,[esp+18h]
  004d7da8  81c200010000          add       edx,100h
  004d7dae  81e2ff0f0000          and       edx,0FFFh
  004d7db4  81fa00020000          cmp       edx,200h
  004d7dba  7f40                  jg        short 004D7DFCh
  004d7dbc  e87ff30600            call      00547140h
  004d7dc1  396c2414              cmp       [esp+14h],ebp
  004d7dc5  7d35                  jge       short 004D7DFCh
  004d7dc7  83ff1e                cmp       edi,1Eh
  004d7dca  7d22                  jge       short 004D7DEEh
  004d7dcc  8b06                  mov       eax,[esi]
  004d7dce  c646420b              mov       byte [esi+42h],0Bh
  004d7dd2  25fffffffe            and       eax,0FEFFFFFFh
  004d7dd7  c6464301              mov       byte [esi+43h],1
  004d7ddb  0c02                  or        al,2
  004d7ddd  895e5c                mov       [esi+5Ch],ebx
  004d7de0  8906                  mov       [esi],eax
  004d7de2  66c746180700          mov       word [esi+18h],7
  004d7de8  66895e1a              mov       [esi+1Ah],bx
  004d7dec  eb65                  jmp       short 004D7E53h

LAB_004d7dee:
  004d7dee  c646420c              mov       byte [esi+42h],0Ch
  004d7df2  c6464301              mov       byte [esi+43h],1
  004d7df6  66895e12              mov       [esi+12h],bx
  004d7dfa  eb57                  jmp       short 004D7E53h

LAB_004d7dfc:
  004d7dfc  c6464201              mov       byte [esi+42h],1
  004d7e00  c6464301              mov       byte [esi+43h],1
  004d7e04  eb4d                  jmp       short 004D7E53h

LAB_004d7e06:
  004d7e06  83ff3c                cmp       edi,3Ch
  004d7e09  7c2b                  jl        short 004D7E36h
  004d7e0b  83ff46                cmp       edi,46h
  004d7e0e  7d26                  jge       short 004D7E36h
  004d7e10  8b542410              mov       edx,[esp+10h]
  004d7e14  8b442414              mov       eax,[esp+14h]
  004d7e18  83c264                add       edx,64h
  004d7e1b  3bc2                  cmp       eax,edx
  004d7e1d  7e17                  jle       short 004D7E36h
  004d7e1f  c6464206              mov       byte [esi+42h],6
  004d7e23  c6464301              mov       byte [esi+43h],1
  004d7e27  e814f30600            call      00547140h
  004d7e2c  66894610              mov       [esi+10h],ax
  004d7e30  66895e12              mov       [esi+12h],bx
  004d7e34  eb1d                  jmp       short 004D7E53h

LAB_004d7e36:
  004d7e36  c6464202              mov       byte [esi+42h],2
  004d7e3a  e801f30600            call      00547140h
  004d7e3f  99                    cdq
  004d7e40  b90a000000            mov       ecx,0Ah
  004d7e45  66c746105000          mov       word [esi+10h],50h
  004d7e4b  f7f9                  idiv      ecx
  004d7e4d  80c214                add       dl,14h
  004d7e50  885643                mov       [esi+43h],dl

LAB_004d7e53:
  004d7e53  8b442414              mov       eax,[esp+14h]
  004d7e57  3d58020000            cmp       eax,258h
  004d7e5c  0f8dc3000000          jge       near 004D7F25h
  004d7e62  3bc5                  cmp       eax,ebp
  004d7e64  0f8cbb000000          jl        near 004D7F25h
  004d7e6a  e8d1f20600            call      00547140h
  004d7e6f  99                    cdq
  004d7e70  8bcd                  mov       ecx,ebp
  004d7e72  f7f9                  idiv      ecx
  004d7e74  83fa14                cmp       edx,14h
  004d7e77  7d2b                  jge       short 004D7EA4h
  004d7e79  8b542418              mov       edx,[esp+18h]
  004d7e7d  81c200040000          add       edx,400h
  004d7e83  81e2ff0f0000          and       edx,0FFFh
  004d7e89  81fa00080000          cmp       edx,800h
  004d7e8f  7c0d                  jl        short 004D7E9Eh
  004d7e91  c6464201              mov       byte [esi+42h],1
  004d7e95  c6464301              mov       byte [esi+43h],1
  004d7e99  e987000000            jmp       004D7F25h

LAB_004d7e9e:
  004d7e9e  c646420d              mov       byte [esi+42h],0Dh
  004d7ea2  eb79                  jmp       short 004D7F1Dh

LAB_004d7ea4:
  004d7ea4  83fa28                cmp       edx,28h
  004d7ea7  7d1f                  jge       short 004D7EC8h
  004d7ea9  c6464202              mov       byte [esi+42h],2
  004d7ead  e88ef20600            call      00547140h
  004d7eb2  99                    cdq
  004d7eb3  b90a000000            mov       ecx,0Ah
  004d7eb8  66c746105000          mov       word [esi+10h],50h
  004d7ebe  f7f9                  idiv      ecx
  004d7ec0  80c214                add       dl,14h
  004d7ec3  885643                mov       [esi+43h],dl
  004d7ec6  eb5d                  jmp       short 004D7F25h

LAB_004d7ec8:
  004d7ec8  83fa3c                cmp       edx,3Ch
  004d7ecb  7d13                  jge       short 004D7EE0h
  004d7ecd  c6464206              mov       byte [esi+42h],6
  004d7ed1  c6464301              mov       byte [esi+43h],1
  004d7ed5  e866f20600            call      00547140h
  004d7eda  66894610              mov       [esi+10h],ax
  004d7ede  eb41                  jmp       short 004D7F21h

LAB_004d7ee0:
  004d7ee0  83fa55                cmp       edx,55h
  004d7ee3  7d34                  jge       short 004D7F19h
  004d7ee5  c6464203              mov       byte [esi+42h],3
  004d7ee9  66895e12              mov       [esi+12h],bx
  004d7eed  e84ef20600            call      00547140h
  004d7ef2  99                    cdq
  004d7ef3  b90a000000            mov       ecx,0Ah
  004d7ef8  f7f9                  idiv      ecx
  004d7efa  80c214                add       dl,14h
  004d7efd  885643                mov       [esi+43h],dl
  004d7f00  e83bf20600            call      00547140h
  004d7f05  99                    cdq
  004d7f06  33c2                  xor       eax,edx
  004d7f08  2bc2                  sub       eax,edx
  004d7f0a  25ff0f0000            and       eax,0FFFh
  004d7f0f  33c2                  xor       eax,edx
  004d7f11  2bc2                  sub       eax,edx
  004d7f13  66894610              mov       [esi+10h],ax
  004d7f17  eb0c                  jmp       short 004D7F25h

LAB_004d7f19:
  004d7f19  c646420e              mov       byte [esi+42h],0Eh

LAB_004d7f1d:
  004d7f1d  c6464301              mov       byte [esi+43h],1

LAB_004d7f21:
  004d7f21  66895e12              mov       [esi+12h],bx

LAB_004d7f25:
  004d7f25  817c241458020000      cmp       dword [esp+14h],258h
  004d7f2d  0f8c47010000          jl        near 004D807Ah
  004d7f33  e808f20600            call      00547140h
  004d7f38  99                    cdq
  004d7f39  8bcd                  mov       ecx,ebp
  004d7f3b  f7f9                  idiv      ecx
  004d7f3d  83fa1e                cmp       edx,1Eh
  004d7f40  7d60                  jge       short 004D7FA2h
  004d7f42  8b442410              mov       eax,[esp+10h]
  004d7f46  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004d7f4a  3bd1                  cmp       edx,ecx
  004d7f4c  7d26                  jge       short 004D7F74h
  004d7f4e  c6464202              mov       byte [esi+42h],2
  004d7f52  e8e9f10600            call      00547140h
  004d7f57  99                    cdq
  004d7f58  b90a000000            mov       ecx,0Ah
  004d7f5d  66896e10              mov       [esi+10h],bp
  004d7f61  f7f9                  idiv      ecx
  004d7f63  80c214                add       dl,14h
  004d7f66  885643                mov       [esi+43h],dl
  004d7f69  5f                    pop       edi
  004d7f6a  5e                    pop       esi
  004d7f6b  5d                    pop       ebp
  004d7f6c  5b                    pop       ebx
  004d7f6d  81c48c000000          add       esp,8Ch
  004d7f73  c3                    ret

LAB_004d7f74:
  004d7f74  885e42                mov       [esi+42h],bl
  004d7f77  e8c4f10600            call      00547140h
  004d7f7c  99                    cdq
  004d7f7d  b90a000000            mov       ecx,0Ah
  004d7f82  66895e12              mov       [esi+12h],bx
  004d7f86  f7f9                  idiv      ecx
  004d7f88  885643                mov       [esi+43h],dl
  004d7f8b  8b542410              mov       edx,[esp+10h]
  004d7f8f  8d441250              lea       eax,[edx+edx+50h]
  004d7f93  66894610              mov       [esi+10h],ax
  004d7f97  5f                    pop       edi
  004d7f98  5e                    pop       esi
  004d7f99  5d                    pop       ebp
  004d7f9a  5b                    pop       ebx
  004d7f9b  81c48c000000          add       esp,8Ch
  004d7fa1  c3                    ret

LAB_004d7fa2:
  004d7fa2  83fa28                cmp       edx,28h
  004d7fa5  7d3d                  jge       short 004D7FE4h
  004d7fa7  c6464203              mov       byte [esi+42h],3
  004d7fab  66895e12              mov       [esi+12h],bx
  004d7faf  e88cf10600            call      00547140h
  004d7fb4  99                    cdq
  004d7fb5  b90a000000            mov       ecx,0Ah
  004d7fba  f7f9                  idiv      ecx
  004d7fbc  80c214                add       dl,14h
  004d7fbf  885643                mov       [esi+43h],dl
  004d7fc2  e879f10600            call      00547140h
  004d7fc7  99                    cdq
  004d7fc8  33c2                  xor       eax,edx
  004d7fca  2bc2                  sub       eax,edx
  004d7fcc  25ff0f0000            and       eax,0FFFh
  004d7fd1  33c2                  xor       eax,edx
  004d7fd3  2bc2                  sub       eax,edx
  004d7fd5  66894610              mov       [esi+10h],ax
  004d7fd9  5f                    pop       edi
  004d7fda  5e                    pop       esi
  004d7fdb  5d                    pop       ebp
  004d7fdc  5b                    pop       ebx
  004d7fdd  81c48c000000          add       esp,8Ch
  004d7fe3  c3                    ret

LAB_004d7fe4:
  004d7fe4  83fa3c                cmp       edx,3Ch
  004d7fe7  7d26                  jge       short 004D800Fh
  004d7fe9  c6464202              mov       byte [esi+42h],2
  004d7fed  e84ef10600            call      00547140h
  004d7ff2  99                    cdq
  004d7ff3  b90a000000            mov       ecx,0Ah
  004d7ff8  66896e10              mov       [esi+10h],bp
  004d7ffc  f7f9                  idiv      ecx
  004d7ffe  80c214                add       dl,14h
  004d8001  885643                mov       [esi+43h],dl
  004d8004  5f                    pop       edi
  004d8005  5e                    pop       esi
  004d8006  5d                    pop       ebp
  004d8007  5b                    pop       ebx
  004d8008  81c48c000000          add       esp,8Ch
  004d800e  c3                    ret

LAB_004d800f:
  004d800f  83fa46                cmp       edx,46h
  004d8012  7d55                  jge       short 004D8069h
  004d8014  8b542418              mov       edx,[esp+18h]
  004d8018  81c200010000          add       edx,100h
  004d801e  81e2ff0f0000          and       edx,0FFFh
  004d8024  81fa00020000          cmp       edx,200h
  004d802a  7f17                  jg        short 004D8043h
  004d802c  c646420e              mov       byte [esi+42h],0Eh
  004d8030  c6464301              mov       byte [esi+43h],1
  004d8034  66895e12              mov       [esi+12h],bx
  004d8038  5f                    pop       edi
  004d8039  5e                    pop       esi
  004d803a  5d                    pop       ebp
  004d803b  5b                    pop       ebx
  004d803c  81c48c000000          add       esp,8Ch
  004d8042  c3                    ret

LAB_004d8043:
  004d8043  c6464202              mov       byte [esi+42h],2
  004d8047  e8f4f00600            call      00547140h
  004d804c  99                    cdq
  004d804d  b90a000000            mov       ecx,0Ah
  004d8052  66896e10              mov       [esi+10h],bp
  004d8056  f7f9                  idiv      ecx
  004d8058  80c214                add       dl,14h
  004d805b  885643                mov       [esi+43h],dl
  004d805e  5f                    pop       edi
  004d805f  5e                    pop       esi
  004d8060  5d                    pop       ebp
  004d8061  5b                    pop       ebx
  004d8062  81c48c000000          add       esp,8Ch
  004d8068  c3                    ret

LAB_004d8069:
  004d8069  c6464206              mov       byte [esi+42h],6
  004d806d  c6464301              mov       byte [esi+43h],1
  004d8071  e8caf00600            call      00547140h
  004d8076  66894610              mov       [esi+10h],ax

LAB_004d807a:
  004d807a  5f                    pop       edi
  004d807b  5e                    pop       esi
  004d807c  5d                    pop       ebp
  004d807d  5b                    pop       ebx
  004d807e  81c48c000000          add       esp,8Ch
  004d8084  c3                    ret

70 cible(s) de saut interne : 0x004d7271, 0x004d73b9, 0x004d73db, 0x004d73dd, 0x004d7452, 0x004d7456, 0x004d7470, 0x004d7475, 0x004d74fd, 0x004d7501, 0x004d75cf, 0x004d75d1, 0x004d75d5, 0x004d760a, 0x004d761d, 0x004d7653, 0x004d7674, 0x004d7693, 0x004d76db, 0x004d76f8, 0x004d7704, 0x004d773c, 0x004d7765, 0x004d77e0, 0x004d77eb, 0x004d77ef, 0x004d782c, 0x004d789e, 0x004d78ce, 0x004d78da, 0x004d792c, 0x004d795f, 0x004d796d, 0x004d79ab, 0x004d79d0, 0x004d79db, 0x004d7a42, 0x004d7a88, 0x004d7ae0, 0x004d7aff, 0x004d7b02, 0x004d7bdf, 0x004d7beb, 0x004d7bed, 0x004d7bfe, 0x004d7c3a, 0x004d7cca, 0x004d7cda, 0x004d7d02, 0x004d7d30, 0x004d7dee, 0x004d7dfc, 0x004d7e06, 0x004d7e36, 0x004d7e53, 0x004d7e9e, 0x004d7ea4, 0x004d7ec8, 0x004d7ee0, 0x004d7f19, 0x004d7f1d, 0x004d7f21, 0x004d7f25, 0x004d7f74, 0x004d7fa2, 0x004d7fe4, 0x004d800f, 0x004d8043, 0x004d8069, 0x004d807a
