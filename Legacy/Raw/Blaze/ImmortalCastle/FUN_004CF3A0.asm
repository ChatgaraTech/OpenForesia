Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004cf3a0  section .text  offset fichier 0xce7a0  3616 octets

  004cf3a0  81ec84000000          sub       esp,84h
  004cf3a6  53                    push      ebx
  004cf3a7  55                    push      ebp
  004cf3a8  56                    push      esi
  004cf3a9  8bb42494000000        mov       esi,[esp+94h]
  004cf3b0  b864000000            mov       eax,64h
  004cf3b5  33db                  xor       ebx,ebx
  004cf3b7  6689442432            mov       [esp+32h],ax
  004cf3bc  6689442436            mov       [esp+36h],ax
  004cf3c1  8a4641                mov       al,[esi+41h]
  004cf3c4  57                    push      edi
  004cf3c5  3ac3                  cmp       al,bl
  004cf3c7  66895c2430            mov       [esp+30h],bx
  004cf3cc  66c74424321e00        mov       word [esp+32h],1Eh
  004cf3d3  66c74424343200        mov       word [esp+34h],32h
  004cf3da  66895c2438            mov       [esp+38h],bx
  004cf3df  7510                  jne       short 004CF3F1h
  004cf3e1  53                    push      ebx
  004cf3e2  6a01                  push      1
  004cf3e4  56                    push      esi
  004cf3e5  c6464101              mov       byte [esi+41h],1
  004cf3e9  e8f2fbffff            call      004CEFE0h
  004cf3ee  83c40c                add       esp,0Ch

LAB_004cf3f1:
  004cf3f1  8b461c                mov       eax,[esi+1Ch]
  004cf3f4  8b4e20                mov       ecx,[esi+20h]
  004cf3f7  8b5624                mov       edx,[esi+24h]
  004cf3fa  8d6e1c                lea       ebp,[esi+1Ch]
  004cf3fd  89442424              mov       [esp+24h],eax
  004cf401  668b4632              mov       ax,[esi+32h]
  004cf405  894c2428              mov       [esp+28h],ecx
  004cf409  8b4e64                mov       ecx,[esi+64h]
  004cf40c  8954242c              mov       [esp+2Ch],edx
  004cf410  8b5660                mov       edx,[esi+60h]
  004cf413  6689842482000000      mov       [esp+82h],ax
  004cf41b  8b467c                mov       eax,[esi+7Ch]
  004cf41e  894c2444              mov       [esp+44h],ecx
  004cf422  668b8e90000000        mov       cx,[esi+90h]
  004cf429  89542440              mov       [esp+40h],edx
  004cf42d  8b9680000000          mov       edx,[esi+80h]
  004cf433  8944245c              mov       [esp+5Ch],eax
  004cf437  8b8684000000          mov       eax,[esi+84h]
  004cf43d  66894c2448            mov       [esp+48h],cx
  004cf442  8b8e88000000          mov       ecx,[esi+88h]
  004cf448  8b7e6c                mov       edi,[esi+6Ch]
  004cf44b  8954244c              mov       [esp+4Ch],edx
  004cf44f  8b968c000000          mov       edx,[esi+8Ch]
  004cf455  8944243c              mov       [esp+3Ch],eax
  004cf459  894c2458              mov       [esp+58h],ecx
  004cf45d  8d442460              lea       eax,[esp+60h]
  004cf461  89542450              mov       [esp+50h],edx
  004cf465  8d4c2468              lea       ecx,[esp+68h]
  004cf469  50                    push      eax
  004cf46a  8d542468              lea       edx,[esp+68h]
  004cf46e  51                    push      ecx
  004cf46f  52                    push      edx
  004cf470  56                    push      esi
  004cf471  e80a950600            call      00538980h
  004cf476  83c410                add       esp,10h
  004cf479  8d442454              lea       eax,[esp+54h]
  004cf47d  8d4c241c              lea       ecx,[esp+1Ch]
  004cf481  8d542418              lea       edx,[esp+18h]
  004cf485  50                    push      eax
  004cf486  51                    push      ecx
  004cf487  8d44241c              lea       eax,[esp+1Ch]
  004cf48b  52                    push      edx
  004cf48c  8d4c241c              lea       ecx,[esp+1Ch]
  004cf490  50                    push      eax
  004cf491  51                    push      ecx
  004cf492  56                    push      esi
  004cf493  e8886a0600            call      00535F20h
  004cf498  8b4c2428              mov       ecx,[esp+28h]
  004cf49c  8b54246c              mov       edx,[esp+6Ch]
  004cf4a0  83c418                add       esp,18h
  004cf4a3  b840000000            mov       eax,40h
  004cf4a8  2bc1                  sub       eax,ecx
  004cf4aa  52                    push      edx
  004cf4ab  56                    push      esi
  004cf4ac  89442428              mov       [esp+28h],eax
  004cf4b0  e82b690000            call      004D5DE0h
  004cf4b5  8b4500                mov       eax,[ebp]
  004cf4b8  8b571c                mov       edx,[edi+1Ch]
  004cf4bb  8b4e20                mov       ecx,[esi+20h]
  004cf4be  2bc2                  sub       eax,edx
  004cf4c0  8b5720                mov       edx,[edi+20h]
  004cf4c3  83c408                add       esp,8
  004cf4c6  2bca                  sub       ecx,edx
  004cf4c8  8b5624                mov       edx,[esi+24h]
  004cf4cb  2b5724                sub       edx,[edi+24h]
  004cf4ce  c1f80c                sar       eax,0Ch
  004cf4d1  8bf8                  mov       edi,eax
  004cf4d3  c1f90c                sar       ecx,0Ch
  004cf4d6  0faff8                imul      edi,eax
  004cf4d9  8bc1                  mov       eax,ecx
  004cf4db  c1fa0c                sar       edx,0Ch
  004cf4de  0fafc1                imul      eax,ecx
  004cf4e1  8bca                  mov       ecx,edx
  004cf4e3  03f8                  add       edi,eax
  004cf4e5  0fafca                imul      ecx,edx
  004cf4e8  03f9                  add       edi,ecx
  004cf4ea  57                    push      edi
  004cf4eb  e820650700            call      00545A10h
  004cf4f0  8b4c241c              mov       ecx,[esp+1Ch]
  004cf4f4  89442414              mov       [esp+14h],eax
  004cf4f8  3bcb                  cmp       ecx,ebx
  004cf4fa  0f84ce060000          je        near 004CFBCEh
  004cf500  8b16                  mov       edx,[esi]
  004cf502  33c0                  xor       eax,eax
  004cf504  8a4604                mov       al,[esi+4]
  004cf507  81e2fffffffd          and       edx,0FDFFFFFFh
  004cf50d  0fbf7e18              movsx     edi,word [esi+18h]
  004cf511  8916                  mov       [esi],edx
  004cf513  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004cf51a  f7c200000001          test      edx,1000000h
  004cf520  8b3cb8                mov       edi,[eax+edi*4]
  004cf523  741b                  je        short 004CF540h
  004cf525  66ff461a              inc       word [esi+1Ah]
  004cf529  668b461a              mov       ax,[esi+1Ah]
  004cf52d  33d2                  xor       edx,edx
  004cf52f  668b5716              mov       dx,[edi+16h]
  004cf533  0fbfc0                movsx     eax,ax
  004cf536  3bc2                  cmp       eax,edx
  004cf538  7c2a                  jl        short 004CF564h
  004cf53a  66895e1a              mov       [esi+1Ah],bx
  004cf53e  eb24                  jmp       short 004CF564h

LAB_004cf540:
  004cf540  66ff461a              inc       word [esi+1Ah]
  004cf544  668b461a              mov       ax,[esi+1Ah]
  004cf548  33db                  xor       ebx,ebx
  004cf54a  668b5f16              mov       bx,[edi+16h]
  004cf54e  0fbff8                movsx     edi,ax
  004cf551  3bfb                  cmp       edi,ebx
  004cf553  7c0d                  jl        short 004CF562h
  004cf555  48                    dec       eax
  004cf556  81ca00000002          or        edx,2000000h
  004cf55c  6689461a              mov       [esi+1Ah],ax
  004cf560  8916                  mov       [esi],edx

LAB_004cf562:
  004cf562  33db                  xor       ebx,ebx

LAB_004cf564:
  004cf564  8a4642                mov       al,[esi+42h]
  004cf567  3c05                  cmp       al,5
  004cf569  8b06                  mov       eax,[esi]
  004cf56b  7504                  jne       short 004CF571h
  004cf56d  0c10                  or        al,10h
  004cf56f  eb02                  jmp       short 004CF573h

LAB_004cf571:
  004cf571  24ef                  and       al,0EFh

LAB_004cf573:
  004cf573  8906                  mov       [esi],eax
  004cf575  8a06                  mov       al,[esi]
  004cf577  a802                  test      al,2
  004cf579  747d                  je        short 004CF5F8h
  004cf57b  66837e1a09            cmp       word [esi+1Ah],9
  004cf580  7558                  jne       short 004CF5DAh
  004cf582  8d4c2430              lea       ecx,[esp+30h]
  004cf586  53                    push      ebx
  004cf587  51                    push      ecx
  004cf588  55                    push      ebp
  004cf589  56                    push      esi
  004cf58a  e8a1a60400            call      00519C30h
  004cf58f  8bf8                  mov       edi,eax
  004cf591  a1b8e05d00            mov       eax,[5DE0B8h]
  004cf596  83c410                add       esp,10h
  004cf599  3bc3                  cmp       eax,ebx
  004cf59b  7439                  je        short 004CF5D6h
  004cf59d  66837e1a09            cmp       word [esi+1Ah],9
  004cf5a2  7532                  jne       short 004CF5D6h
  004cf5a4  8b0f                  mov       ecx,[edi]
  004cf5a6  57                    push      edi
  004cf5a7  83c940                or        ecx,40h
  004cf5aa  56                    push      esi
  004cf5ab  66c787960000001000    mov       word [edi+96h],10h
  004cf5b4  890f                  mov       [edi],ecx
  004cf5b6  e8c5a50600            call      00539B80h
  004cf5bb  83c408                add       esp,8
  004cf5be  83c71c                add       edi,1Ch
  004cf5c1  57                    push      edi
  004cf5c2  6800180000            push      1800h
  004cf5c7  6800030000            push      300h
  004cf5cc  6a03                  push      3
  004cf5ce  e84de50600            call      0053DB20h
  004cf5d3  83c410                add       esp,10h

LAB_004cf5d6:
  004cf5d6  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004cf5da:
  004cf5da  8b16                  mov       edx,[esi]
  004cf5dc  f7c200000002          test      edx,2000000h
  004cf5e2  7414                  je        short 004CF5F8h
  004cf5e4  f6c201                test      dl,1
  004cf5e7  750a                  jne       short 004CF5F3h
  004cf5e9  83e2fd                and       edx,0FFFFFFFDh
  004cf5ec  885e43                mov       [esi+43h],bl
  004cf5ef  8916                  mov       [esi],edx
  004cf5f1  eb05                  jmp       short 004CF5F8h

LAB_004cf5f3:
  004cf5f3  83e2fd                and       edx,0FFFFFFFDh
  004cf5f6  8916                  mov       [esi],edx

LAB_004cf5f8:
  004cf5f8  33c0                  xor       eax,eax
  004cf5fa  8a4642                mov       al,[esi+42h]
  004cf5fd  83f80c                cmp       eax,0Ch
  004cf600  0f87c8050000          ja        near 004CFBCEh
  004cf606  ff248580014d00        jmp       dword [eax*4+4D0180h]
  004cf60d  66395e12              cmp       [esi+12h],bx
  004cf611  751a                  jne       short 004CF62Dh
  004cf613  8b06                  mov       eax,[esi]
  004cf615  66895e18              mov       [esi+18h],bx
  004cf619  0d00000001            or        eax,1000000h
  004cf61e  66895e1a              mov       [esi+1Ah],bx
  004cf622  8906                  mov       [esi],eax
  004cf624  66c746120100          mov       word [esi+12h],1
  004cf62a  895e5c                mov       [esi+5Ch],ebx

LAB_004cf62d:
  004cf62d  0fbf5610              movsx     edx,word [esi+10h]
  004cf631  39542414              cmp       [esp+14h],edx
  004cf635  0f8c90050000          jl        near 004CFBCBh
  004cf63b  8a4643                mov       al,[esi+43h]
  004cf63e  3ac3                  cmp       al,bl
  004cf640  0f84ce000000          je        near 004CF714h
  004cf646  fec8                  dec       al
  004cf648  895e5c                mov       [esi+5Ch],ebx
  004cf64b  884643                mov       [esi+43h],al
  004cf64e  e97b050000            jmp       004CFBCEh
  004cf653  66395e12              cmp       [esi+12h],bx
  004cf657  751b                  jne       short 004CF674h
  004cf659  8b0e                  mov       ecx,[esi]
  004cf65b  b801000000            mov       eax,1
  004cf660  81c900000001          or        ecx,1000000h
  004cf666  66894618              mov       [esi+18h],ax
  004cf66a  66895e1a              mov       [esi+1Ah],bx
  004cf66e  890e                  mov       [esi],ecx
  004cf670  66894612              mov       [esi+12h],ax

LAB_004cf674:
  004cf674  8b442418              mov       eax,[esp+18h]
  004cf678  8d8880000000          lea       ecx,[eax+80h]
  004cf67e  81e1ff0f0000          and       ecx,0FFFh
  004cf684  81f990010000          cmp       ecx,190h
  004cf68a  0f8e81000000          jle       near 004CF711h
  004cf690  25ff0f0000            and       eax,0FFFh
  004cf695  3d00080000            cmp       eax,800h
  004cf69a  7d16                  jge       short 004CF6B2h
  004cf69c  8b542410              mov       edx,[esp+10h]
  004cf6a0  895e5c                mov       [esi+5Ch],ebx
  004cf6a3  8d8280000000          lea       eax,[edx+80h]
  004cf6a9  66014632              add       [esi+32h],ax
  004cf6ad  e91c050000            jmp       004CFBCEh

LAB_004cf6b2:
  004cf6b2  8b4c2410              mov       ecx,[esp+10h]
  004cf6b6  b880ffffff            mov       eax,0FFFFFF80h
  004cf6bb  2bc1                  sub       eax,ecx
  004cf6bd  895e5c                mov       [esi+5Ch],ebx
  004cf6c0  66014632              add       [esi+32h],ax
  004cf6c4  e905050000            jmp       004CFBCEh
  004cf6c9  66395e12              cmp       [esi+12h],bx
  004cf6cd  8b4c2410              mov       ecx,[esp+10h]
  004cf6d1  b801000000            mov       eax,1
  004cf6d6  7525                  jne       short 004CF6FDh
  004cf6d8  8b16                  mov       edx,[esi]
  004cf6da  66894618              mov       [esi+18h],ax
  004cf6de  81ca00000001          or        edx,1000000h
  004cf6e4  66895e1a              mov       [esi+1Ah],bx
  004cf6e8  8916                  mov       [esi],edx
  004cf6ea  8d14c9                lea       edx,[ecx+ecx*8]
  004cf6ed  c1e204                shl       edx,4
  004cf6f0  81c200500000          add       edx,5000h
  004cf6f6  66894612              mov       [esi+12h],ax
  004cf6fa  89565c                mov       [esi+5Ch],edx

LAB_004cf6fd:
  004cf6fd  66394612              cmp       [esi+12h],ax
  004cf701  0f85c7040000          jne       near 004CFBCEh
  004cf707  0fbf4610              movsx     eax,word [esi+10h]
  004cf70b  39442414              cmp       [esp+14h],eax
  004cf70f  7d0b                  jge       short 004CF71Ch

LAB_004cf711:
  004cf711  885e43                mov       [esi+43h],bl

LAB_004cf714:
  004cf714  895e5c                mov       [esi+5Ch],ebx
  004cf717  e9b2040000            jmp       004CFBCEh

LAB_004cf71c:
  004cf71c  8a4643                mov       al,[esi+43h]
  004cf71f  3ac3                  cmp       al,bl
  004cf721  7405                  je        short 004CF728h
  004cf723  fec8                  dec       al
  004cf725  884643                mov       [esi+43h],al

LAB_004cf728:
  004cf728  8b442418              mov       eax,[esp+18h]
  004cf72c  8d9000010000          lea       edx,[eax+100h]
  004cf732  81e2ff0f0000          and       edx,0FFFh
  004cf738  81fa00020000          cmp       edx,200h
  004cf73e  0f8e8a040000          jle       near 004CFBCEh
  004cf744  25ff0f0000            and       eax,0FFFh
  004cf749  3d00080000            cmp       eax,800h
  004cf74e  7d0f                  jge       short 004CF75Fh
  004cf750  8d8180000000          lea       eax,[ecx+80h]
  004cf756  66014632              add       [esi+32h],ax
  004cf75a  e96f040000            jmp       004CFBCEh

LAB_004cf75f:
  004cf75f  b880ffffff            mov       eax,0FFFFFF80h
  004cf764  2bc1                  sub       eax,ecx
  004cf766  66014632              add       [esi+32h],ax
  004cf76a  e95f040000            jmp       004CFBCEh
  004cf76f  66837e1201            cmp       word [esi+12h],1
  004cf774  0f8599000000          jne       near 004CF813h
  004cf77a  8a4643                mov       al,[esi+43h]
  004cf77d  3ac3                  cmp       al,bl
  004cf77f  7405                  je        short 004CF786h
  004cf781  fec8                  dec       al
  004cf783  884643                mov       [esi+43h],al

LAB_004cf786:
  004cf786  8b442410              mov       eax,[esp+10h]
  004cf78a  8d04c0                lea       eax,[eax+eax*8]
  004cf78d  c1e004                shl       eax,4
  004cf790  0500500000            add       eax,5000h
  004cf795  0fafc8                imul      ecx,eax
  004cf798  c1f90c                sar       ecx,0Ch
  004cf79b  8bf9                  mov       edi,ecx
  004cf79d  89465c                mov       [esi+5Ch],eax
  004cf7a0  0fbf4e10              movsx     ecx,word [esi+10h]
  004cf7a4  51                    push      ecx
  004cf7a5  e872620700            call      00545A1Ch
  004cf7aa  0fafc7                imul      eax,edi
  004cf7ad  0fbf5610              movsx     edx,word [esi+10h]
  004cf7b1  8b4d00                mov       ecx,[ebp]
  004cf7b4  52                    push      edx
  004cf7b5  c1f80c                sar       eax,0Ch
  004cf7b8  03c8                  add       ecx,eax
  004cf7ba  894d00                mov       [ebp],ecx
  004cf7bd  e854620700            call      00545A16h
  004cf7c2  0fafc7                imul      eax,edi
  004cf7c5  8b6e24                mov       ebp,[esi+24h]
  004cf7c8  895e5c                mov       [esi+5Ch],ebx
  004cf7cb  c1f80c                sar       eax,0Ch
  004cf7ce  03e8                  add       ebp,eax
  004cf7d0  8b442418              mov       eax,[esp+18h]
  004cf7d4  896e24                mov       [esi+24h],ebp
  004cf7d7  8d8800010000          lea       ecx,[eax+100h]
  004cf7dd  81e1ff0f0000          and       ecx,0FFFh
  004cf7e3  81f900020000          cmp       ecx,200h
  004cf7e9  7e28                  jle       short 004CF813h
  004cf7eb  25ff0f0000            and       eax,0FFFh
  004cf7f0  3d00080000            cmp       eax,800h
  004cf7f5  7d0a                  jge       short 004CF801h
  004cf7f7  8b542410              mov       edx,[esp+10h]
  004cf7fb  8d441232              lea       eax,[edx+edx+32h]
  004cf7ff  eb0e                  jmp       short 004CF80Fh

LAB_004cf801:
  004cf801  8b442410              mov       eax,[esp+10h]
  004cf805  8d0c00                lea       ecx,[eax+eax]
  004cf808  b8ceffffff            mov       eax,0FFFFFFCEh
  004cf80d  2bc1                  sub       eax,ecx

LAB_004cf80f:
  004cf80f  66014632              add       [esi+32h],ax

LAB_004cf813:
  004cf813  66395e12              cmp       [esi+12h],bx
  004cf817  0f85b1030000          jne       near 004CFBCEh
  004cf81d  8b0e                  mov       ecx,[esi]
  004cf81f  b801000000            mov       eax,1
  004cf824  81c900000001          or        ecx,1000000h
  004cf82a  66894618              mov       [esi+18h],ax
  004cf82e  66895e1a              mov       [esi+1Ah],bx
  004cf832  890e                  mov       [esi],ecx
  004cf834  66894612              mov       [esi+12h],ax
  004cf838  e991030000            jmp       004CFBCEh
  004cf83d  66395e12              cmp       [esi+12h],bx
  004cf841  757d                  jne       short 004CF8C0h
  004cf843  8b06                  mov       eax,[esi]
  004cf845  a801                  test      al,1
  004cf847  7405                  je        short 004CF84Eh
  004cf849  885e43                mov       [esi+43h],bl
  004cf84c  eb72                  jmp       short 004CF8C0h

LAB_004cf84e:
  004cf84e  8b542410              mov       edx,[esp+10h]
  004cf852  25fffffffe            and       eax,0FEFFFFFFh
  004cf857  8906                  mov       [esi],eax
  004cf859  bd01000000            mov       ebp,1
  004cf85e  8d8200010000          lea       eax,[edx+100h]
  004cf864  66896e18              mov       [esi+18h],bp
  004cf868  c1e006                shl       eax,6
  004cf86b  0fafc8                imul      ecx,eax
  004cf86e  89465c                mov       [esi+5Ch],eax
  004cf871  66895e1a              mov       [esi+1Ah],bx
  004cf875  0fbf4610              movsx     eax,word [esi+10h]
  004cf879  c1f90c                sar       ecx,0Ch
  004cf87c  50                    push      eax
  004cf87d  8bf9                  mov       edi,ecx
  004cf87f  e898610700            call      00545A1Ch
  004cf884  0fafc7                imul      eax,edi
  004cf887  8b4e50                mov       ecx,[esi+50h]
  004cf88a  c1f80c                sar       eax,0Ch
  004cf88d  03c8                  add       ecx,eax
  004cf88f  894e50                mov       [esi+50h],ecx
  004cf892  0fbf4e10              movsx     ecx,word [esi+10h]
  004cf896  51                    push      ecx
  004cf897  e87a610700            call      00545A16h
  004cf89c  0fafc7                imul      eax,edi
  004cf89f  8b5658                mov       edx,[esi+58h]
  004cf8a2  8b4e54                mov       ecx,[esi+54h]
  004cf8a5  c1f80c                sar       eax,0Ch
  004cf8a8  03d0                  add       edx,eax
  004cf8aa  8b06                  mov       eax,[esi]
  004cf8ac  81c100e00100          add       ecx,1E000h
  004cf8b2  0bc5                  or        eax,ebp
  004cf8b4  895658                mov       [esi+58h],edx
  004cf8b7  894e54                mov       [esi+54h],ecx
  004cf8ba  8906                  mov       [esi],eax
  004cf8bc  66896e12              mov       [esi+12h],bp

LAB_004cf8c0:
  004cf8c0  f60601                test      byte [esi],1
  004cf8c3  0f8505030000          jne       near 004CFBCEh
  004cf8c9  e9fd020000            jmp       004CFBCBh
  004cf8ce  668b4e12              mov       cx,[esi+12h]
  004cf8d2  ba01000000            mov       edx,1
  004cf8d7  663bca                cmp       cx,dx
  004cf8da  751b                  jne       short 004CF8F7h
  004cf8dc  0fbf4610              movsx     eax,word [esi+10h]
  004cf8e0  39442414              cmp       [esp+14h],eax
  004cf8e4  7d05                  jge       short 004CF8EBh
  004cf8e6  885e43                mov       [esi+43h],bl
  004cf8e9  eb0c                  jmp       short 004CF8F7h

LAB_004cf8eb:
  004cf8eb  8a4643                mov       al,[esi+43h]
  004cf8ee  3ac3                  cmp       al,bl
  004cf8f0  7405                  je        short 004CF8F7h
  004cf8f2  fec8                  dec       al
  004cf8f4  884643                mov       [esi+43h],al

LAB_004cf8f7:
  004cf8f7  663bcb                cmp       cx,bx
  004cf8fa  0f85ce020000          jne       near 004CFBCEh
  004cf900  8b442410              mov       eax,[esp+10h]
  004cf904  8b3e                  mov       edi,[esi]
  004cf906  81cf00000001          or        edi,1000000h
  004cf90c  66895618              mov       [esi+18h],dx
  004cf910  8d0cc0                lea       ecx,[eax+eax*8]
  004cf913  66895e1a              mov       [esi+1Ah],bx
  004cf917  c1e104                shl       ecx,4
  004cf91a  81c100500000          add       ecx,5000h
  004cf920  893e                  mov       [esi],edi
  004cf922  66895612              mov       [esi+12h],dx
  004cf926  894e5c                mov       [esi+5Ch],ecx
  004cf929  e9a0020000            jmp       004CFBCEh
  004cf92e  66395e12              cmp       [esi+12h],bx
  004cf932  0f85dc000000          jne       near 004CFA14h
  004cf938  33ff                  xor       edi,edi
  004cf93a  b800f45f00            mov       eax,5FF400h

LAB_004cf93f:
  004cf93f  39706c                cmp       [eax+6Ch],esi
  004cf942  750c                  jne       short 004CF950h
  004cf944  f70000000080          test      dword [eax],80000000h
  004cf94a  0f857b020000          jne       near 004CFBCBh

LAB_004cf950:
  004cf950  059c000000            add       eax,9Ch
  004cf955  47                    inc       edi
  004cf956  3d80076000            cmp       eax,600780h
  004cf95b  7ce2                  jl        short 004CF93Fh
  004cf95d  8b06                  mov       eax,[esi]
  004cf95f  a904080000            test      eax,804h
  004cf964  740f                  je        short 004CF975h
  004cf966  25fbf7ffff            and       eax,0FFFFF7FBh
  004cf96b  885e43                mov       [esi+43h],bl
  004cf96e  8906                  mov       [esi],eax
  004cf970  e959020000            jmp       004CFBCEh

LAB_004cf975:
  004cf975  6aff                  push      0FFFFFFFFh
  004cf977  53                    push      ebx
  004cf978  68bd000000            push      0BDh
  004cf97d  56                    push      esi
  004cf97e  e85db1faff            call      0047AAE0h
  004cf983  83c410                add       esp,10h
  004cf986  66395e16              cmp       [esi+16h],bx
  004cf98a  bd01000000            mov       ebp,1
  004cf98f  66896e12              mov       [esi+12h],bp
  004cf993  751b                  jne       short 004CF9B0h
  004cf995  e8a6770700            call      00547140h
  004cf99a  99                    cdq
  004cf99b  33c2                  xor       eax,edx
  004cf99d  2bc2                  sub       eax,edx
  004cf99f  83e003                and       eax,3
  004cf9a2  33c2                  xor       eax,edx
  004cf9a4  2bc2                  sub       eax,edx
  004cf9a6  33ff                  xor       edi,edi
  004cf9a8  668b3c45102c5500      mov       di,[eax*2+552C10h]

LAB_004cf9b0:
  004cf9b0  66396e16              cmp       [esi+16h],bp
  004cf9b4  7517                  jne       short 004CF9CDh
  004cf9b6  e885770700            call      00547140h
  004cf9bb  99                    cdq
  004cf9bc  b905000000            mov       ecx,5
  004cf9c1  f7f9                  idiv      ecx
  004cf9c3  33ff                  xor       edi,edi
  004cf9c5  668b3c55182c5500      mov       di,[edx*2+552C18h]

LAB_004cf9cd:
  004cf9cd  66897e06              mov       [esi+6],di
  004cf9d1  a1c4975e00            mov       eax,[5E97C4h]
  004cf9d6  8bd7                  mov       edx,edi
  004cf9d8  81e7ff000000          and       edi,0FFh
  004cf9de  8b889c000000          mov       ecx,[eax+9Ch]
  004cf9e4  c1fa08                sar       edx,8
  004cf9e7  81e2ff000000          and       edx,0FFh
  004cf9ed  8d047f                lea       eax,[edi+edi*2]
  004cf9f0  c1e004                shl       eax,4
  004cf9f3  8b0c91                mov       ecx,[ecx+edx*4]
  004cf9f6  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004cf9fc  8b06                  mov       eax,[esi]
  004cf9fe  66895610              mov       [esi+10h],dx
  004cfa02  0d04080001            or        eax,1000804h
  004cfa07  895e5c                mov       [esi+5Ch],ebx
  004cfa0a  66895e18              mov       [esi+18h],bx
  004cfa0e  66895e1a              mov       [esi+1Ah],bx
  004cfa12  8906                  mov       [esi],eax

LAB_004cfa14:
  004cfa14  b801000000            mov       eax,1
  004cfa19  66394612              cmp       [esi+12h],ax
  004cfa1d  7523                  jne       short 004CFA42h
  004cfa1f  66ff4e10              dec       word [esi+10h]
  004cfa23  66394610              cmp       [esi+10h],ax
  004cfa27  7d19                  jge       short 004CFA42h
  004cfa29  8b06                  mov       eax,[esi]
  004cfa2b  66c746120200          mov       word [esi+12h],2
  004cfa31  25fbfffffe            and       eax,0FEFFFFFBh
  004cfa36  66c746180300          mov       word [esi+18h],3
  004cfa3c  66895e1a              mov       [esi+1Ah],bx
  004cfa40  8906                  mov       [esi],eax

LAB_004cfa42:
  004cfa42  66837e1202            cmp       word [esi+12h],2
  004cfa47  7532                  jne       short 004CFA7Bh
  004cfa49  66837e1a09            cmp       word [esi+1Ah],9
  004cfa4e  752b                  jne       short 004CFA7Bh
  004cfa50  33c0                  xor       eax,eax
  004cfa52  6aff                  push      0FFFFFFFFh
  004cfa54  668b4606              mov       ax,[esi+6]
  004cfa58  6aff                  push      0FFFFFFFFh
  004cfa5a  8bc8                  mov       ecx,eax
  004cfa5c  c1f808                sar       eax,8
  004cfa5f  81e1ff000000          and       ecx,0FFh
  004cfa65  25ff000000            and       eax,0FFh
  004cfa6a  51                    push      ecx
  004cfa6b  50                    push      eax
  004cfa6c  56                    push      esi
  004cfa6d  e80eaf0600            call      0053A980h
  004cfa72  83c414                add       esp,14h
  004cfa75  66c746120300          mov       word [esi+12h],3

LAB_004cfa7b:
  004cfa7b  66837e1203            cmp       word [esi+12h],3
  004cfa80  0f8548010000          jne       near 004CFBCEh
  004cfa86  8b06                  mov       eax,[esi]
  004cfa88  a900000002            test      eax,2000000h
  004cfa8d  0f843b010000          je        near 004CFBCEh
  004cfa93  80e4f7                and       ah,0F7h
  004cfa96  8906                  mov       [esi],eax
  004cfa98  e92e010000            jmp       004CFBCBh
  004cfa9d  668b4e12              mov       cx,[esi+12h]
  004cfaa1  6683f901              cmp       cx,1
  004cfaa5  7510                  jne       short 004CFAB7h
  004cfaa7  8b06                  mov       eax,[esi]
  004cfaa9  a900000002            test      eax,2000000h
  004cfaae  7407                  je        short 004CFAB7h
  004cfab0  24fd                  and       al,0FDh
  004cfab2  885e43                mov       [esi+43h],bl
  004cfab5  8906                  mov       [esi],eax

LAB_004cfab7:
  004cfab7  663bcb                cmp       cx,bx
  004cfaba  0f850e010000          jne       near 004CFBCEh
  004cfac0  8b06                  mov       eax,[esi]
  004cfac2  66c746120100          mov       word [esi+12h],1
  004cfac8  25fffffffe            and       eax,0FEFFFFFFh
  004cfacd  8906                  mov       [esi],eax
  004cfacf  e9fa000000            jmp       004CFBCEh
  004cfad4  66395e12              cmp       [esi+12h],bx
  004cfad8  7568                  jne       short 004CFB42h
  004cfada  b900f45f00            mov       ecx,5FF400h

LAB_004cfadf:
  004cfadf  39716c                cmp       [ecx+6Ch],esi
  004cfae2  750c                  jne       short 004CFAF0h
  004cfae4  f70100000080          test      dword [ecx],80000000h
  004cfaea  0f85db000000          jne       near 004CFBCBh

LAB_004cfaf0:
  004cfaf0  81c19c000000          add       ecx,9Ch
  004cfaf6  81f980076000          cmp       ecx,600780h
  004cfafc  7ce1                  jl        short 004CFADFh
  004cfafe  33d2                  xor       edx,edx
  004cfb00  b9e2ca5e00            mov       ecx,5ECAE2h

LAB_004cfb05:
  004cfb05  80390c                cmp       byte [ecx],0Ch
  004cfb08  750d                  jne       short 004CFB17h
  004cfb0a  33c0                  xor       eax,eax
  004cfb0c  8a4608                mov       al,[esi+8]
  004cfb0f  3bd0                  cmp       edx,eax
  004cfb11  0f85b4000000          jne       near 004CFBCBh

LAB_004cfb17:
  004cfb17  81c19c000000          add       ecx,9Ch
  004cfb1d  42                    inc       edx
  004cfb1e  81f962de5e00          cmp       ecx,5EDE62h
  004cfb24  7cdf                  jl        short 004CFB05h
  004cfb26  8b06                  mov       eax,[esi]
  004cfb28  66c746180300          mov       word [esi+18h],3
  004cfb2e  25fffffffc            and       eax,0FCFFFFFFh
  004cfb33  66895e1a              mov       [esi+1Ah],bx
  004cfb37  66c746120100          mov       word [esi+12h],1
  004cfb3d  895e5c                mov       [esi+5Ch],ebx
  004cfb40  8906                  mov       [esi],eax

LAB_004cfb42:
  004cfb42  66837e1201            cmp       word [esi+12h],1
  004cfb47  0f8581000000          jne       near 004CFBCEh
  004cfb4d  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004cfb55  7f74                  jg        short 004CFBCBh
  004cfb57  66837e1a09            cmp       word [esi+1Ah],9
  004cfb5c  7565                  jne       short 004CFBC3h
  004cfb5e  66395e16              cmp       [esi+16h],bx
  004cfb62  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004cfb69  751a                  jne       short 004CFB85h
  004cfb6b  e8d0750700            call      00547140h
  004cfb70  99                    cdq
  004cfb71  33c2                  xor       eax,edx
  004cfb73  2bc2                  sub       eax,edx
  004cfb75  83e001                and       eax,1
  004cfb78  33c2                  xor       eax,edx
  004cfb7a  2bc2                  sub       eax,edx
  004cfb7c  d1e0                  shl       eax,1
  004cfb7e  6689869a000000        mov       [esi+9Ah],ax

LAB_004cfb85:
  004cfb85  66837e1601            cmp       word [esi+16h],1
  004cfb8a  7519                  jne       short 004CFBA5h
  004cfb8c  e8af750700            call      00547140h
  004cfb91  99                    cdq
  004cfb92  33c2                  xor       eax,edx
  004cfb94  2bc2                  sub       eax,edx
  004cfb96  83e001                and       eax,1
  004cfb99  33c2                  xor       eax,edx
  004cfb9b  2bc2                  sub       eax,edx
  004cfb9d  40                    inc       eax
  004cfb9e  6689869a000000        mov       [esi+9Ah],ax

LAB_004cfba5:
  004cfba5  8b4e6c                mov       ecx,[esi+6Ch]
  004cfba8  6aff                  push      0FFFFFFFFh
  004cfbaa  53                    push      ebx
  004cfbab  68df000000            push      0DFh
  004cfbb0  56                    push      esi
  004cfbb1  894e78                mov       [esi+78h],ecx
  004cfbb4  e817aefaff            call      0047A9D0h
  004cfbb9  83c410                add       esp,10h
  004cfbbc  6689be9a000000        mov       [esi+9Ah],di

LAB_004cfbc3:
  004cfbc3  f70600000002          test      dword [esi],2000000h
  004cfbc9  7403                  je        short 004CFBCEh

LAB_004cfbcb:
  004cfbcb  885e43                mov       [esi+43h],bl

LAB_004cfbce:
  004cfbce  0fbf5632              movsx     edx,word [esi+32h]
  004cfbd2  52                    push      edx
  004cfbd3  e8445e0700            call      00545A1Ch
  004cfbd8  8944246c              mov       [esp+6Ch],eax
  004cfbdc  0fbf4632              movsx     eax,word [esi+32h]
  004cfbe0  50                    push      eax
  004cfbe1  e8305e0700            call      00545A16h
  004cfbe6  8b5650                mov       edx,[esi+50h]
  004cfbe9  89442474              mov       [esp+74h],eax
  004cfbed  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004cfbf4  8bc2                  mov       eax,edx
  004cfbf6  8bfa                  mov       edi,edx
  004cfbf8  c1f808                sar       eax,8
  004cfbfb  8b4c241c              mov       ecx,[esp+1Ch]
  004cfbff  2bf8                  sub       edi,eax
  004cfc01  c1fa03                sar       edx,3
  004cfc04  0faf4e5c              imul      ecx,[esi+5Ch]
  004cfc08  2bfa                  sub       edi,edx
  004cfc0a  8b5654                mov       edx,[esi+54h]
  004cfc0d  8bc5                  mov       eax,ebp
  004cfc0f  8bda                  mov       ebx,edx
  004cfc11  c1f808                sar       eax,8
  004cfc14  c1fb08                sar       ebx,8
  004cfc17  2bc3                  sub       eax,ebx
  004cfc19  897e50                mov       [esi+50h],edi
  004cfc1c  2bc5                  sub       eax,ebp
  004cfc1e  56                    push      esi
  004cfc1f  03c2                  add       eax,edx
  004cfc21  8b5658                mov       edx,[esi+58h]
  004cfc24  8bda                  mov       ebx,edx
  004cfc26  8bea                  mov       ebp,edx
  004cfc28  c1fb03                sar       ebx,3
  004cfc2b  2beb                  sub       ebp,ebx
  004cfc2d  8b5e1c                mov       ebx,[esi+1Ch]
  004cfc30  c1fa08                sar       edx,8
  004cfc33  c1f90c                sar       ecx,0Ch
  004cfc36  2bea                  sub       ebp,edx
  004cfc38  8bd1                  mov       edx,ecx
  004cfc3a  0faf542470            imul      edx,[esp+70h]
  004cfc3f  0faf4c2478            imul      ecx,[esp+78h]
  004cfc44  c1fa0c                sar       edx,0Ch
  004cfc47  c1f90c                sar       ecx,0Ch
  004cfc4a  896e58                mov       [esi+58h],ebp
  004cfc4d  03d7                  add       edx,edi
  004cfc4f  8b7e20                mov       edi,[esi+20h]
  004cfc52  03cd                  add       ecx,ebp
  004cfc54  8b6e24                mov       ebp,[esi+24h]
  004cfc57  03da                  add       ebx,edx
  004cfc59  03f8                  add       edi,eax
  004cfc5b  03e9                  add       ebp,ecx
  004cfc5d  894654                mov       [esi+54h],eax
  004cfc60  895e1c                mov       [esi+1Ch],ebx
  004cfc63  897e20                mov       [esi+20h],edi
  004cfc66  896e24                mov       [esi+24h],ebp
  004cfc69  e8d23c0600            call      00533940h
  004cfc6e  8b9690000000          mov       edx,[esi+90h]
  004cfc74  8b4660                mov       eax,[esi+60h]
  004cfc77  8b4e20                mov       ecx,[esi+20h]
  004cfc7a  83c404                add       esp,4
  004cfc7d  80e6fc                and       dh,0FCh
  004cfc80  3bc8                  cmp       ecx,eax
  004cfc82  899690000000          mov       [esi+90h],edx
  004cfc88  7f2a                  jg        short 004CFCB4h
  004cfc8a  f6c680                test      dh,80h
  004cfc8d  7525                  jne       short 004CFCB4h
  004cfc8f  8b0e                  mov       ecx,[esi]
  004cfc91  894620                mov       [esi+20h],eax
  004cfc94  f6c101                test      cl,1
  004cfc97  740f                  je        short 004CFCA8h
  004cfc99  83e1fe                and       ecx,0FFFFFFFEh
  004cfc9c  33db                  xor       ebx,ebx
  004cfc9e  890e                  mov       [esi],ecx
  004cfca0  895e54                mov       [esi+54h],ebx
  004cfca3  e925010000            jmp       004CFDCDh

LAB_004cfca8:
  004cfca8  83e1fe                and       ecx,0FFFFFFFEh
  004cfcab  33db                  xor       ebx,ebx
  004cfcad  890e                  mov       [esi],ecx
  004cfcaf  895e54                mov       [esi+54h],ebx
  004cfcb2  eb02                  jmp       short 004CFCB6h

LAB_004cfcb4:
  004cfcb4  33db                  xor       ebx,ebx

LAB_004cfcb6:
  004cfcb6  8b0e                  mov       ecx,[esi]
  004cfcb8  f6c101                test      cl,1
  004cfcbb  750a                  jne       short 004CFCC7h
  004cfcbd  394620                cmp       [esi+20h],eax
  004cfcc0  7e05                  jle       short 004CFCC7h
  004cfcc2  83c901                or        ecx,1
  004cfcc5  890e                  mov       [esi],ecx

LAB_004cfcc7:
  004cfcc7  f7c282ad0020          test      edx,2000AD82h
  004cfccd  0f84fa000000          je        near 004CFDCDh
  004cfcd3  f60601                test      byte [esi],1
  004cfcd6  0f85c9000000          jne       near 004CFDA5h
  004cfcdc  8a4e42                mov       cl,[esi+42h]
  004cfcdf  b007                  mov       al,7
  004cfce1  3ac8                  cmp       cl,al
  004cfce3  7420                  je        short 004CFD05h
  004cfce5  884642                mov       [esi+42h],al
  004cfce8  e853740700            call      00547140h
  004cfced  99                    cdq
  004cfcee  b928000000            mov       ecx,28h
  004cfcf3  66895e12              mov       [esi+12h],bx
  004cfcf7  f7f9                  idiv      ecx
  004cfcf9  66c746105000          mov       word [esi+10h],50h
  004cfcff  80c214                add       dl,14h
  004cfd02  885643                mov       [esi+43h],dl

LAB_004cfd05:
  004cfd05  8b16                  mov       edx,[esi]
  004cfd07  8b442428              mov       eax,[esp+28h]
  004cfd0b  8b4c242c              mov       ecx,[esp+2Ch]
  004cfd0f  83e2fd                and       edx,0FFFFFFFDh
  004cfd12  8916                  mov       [esi],edx
  004cfd14  8b542424              mov       edx,[esp+24h]
  004cfd18  894620                mov       [esi+20h],eax
  004cfd1b  8b442448              mov       eax,[esp+48h]
  004cfd1f  89561c                mov       [esi+1Ch],edx
  004cfd22  8b54245c              mov       edx,[esp+5Ch]
  004cfd26  25ffff0000            and       eax,0FFFFh
  004cfd2b  894e24                mov       [esi+24h],ecx
  004cfd2e  8b4c243c              mov       ecx,[esp+3Ch]
  004cfd32  89567c                mov       [esi+7Ch],edx
  004cfd35  8b542458              mov       edx,[esp+58h]
  004cfd39  898690000000          mov       [esi+90h],eax
  004cfd3f  8b44244c              mov       eax,[esp+4Ch]
  004cfd43  898e84000000          mov       [esi+84h],ecx
  004cfd49  8b4c2444              mov       ecx,[esp+44h]
  004cfd4d  898680000000          mov       [esi+80h],eax
  004cfd53  8b442450              mov       eax,[esp+50h]
  004cfd57  899688000000          mov       [esi+88h],edx
  004cfd5d  8b542440              mov       edx,[esp+40h]
  004cfd61  6882ad0020            push      2000AD82h
  004cfd66  56                    push      esi
  004cfd67  89868c000000          mov       [esi+8Ch],eax
  004cfd6d  894e64                mov       [esi+64h],ecx
  004cfd70  895660                mov       [esi+60h],edx
  004cfd73  e8e8600000            call      004D5E60h
  004cfd78  83c408                add       esp,8
  004cfd7b  663bc3                cmp       ax,bx
  004cfd7e  7515                  jne       short 004CFD95h
  004cfd80  e8bb730700            call      00547140h
  004cfd85  99                    cdq
  004cfd86  33c2                  xor       eax,edx
  004cfd88  2bc2                  sub       eax,edx
  004cfd8a  83e001                and       eax,1
  004cfd8d  33c2                  xor       eax,edx
  004cfd8f  2bc2                  sub       eax,edx
  004cfd91  8d4400ff              lea       eax,[eax+eax-1]

LAB_004cfd95:
  004cfd95  8b4c2410              mov       ecx,[esp+10h]
  004cfd99  83c140                add       ecx,40h
  004cfd9c  0fafc8                imul      ecx,eax
  004cfd9f  66014e32              add       [esi+32h],cx
  004cfda3  eb28                  jmp       short 004CFDCDh

LAB_004cfda5:
  004cfda5  8b542424              mov       edx,[esp+24h]
  004cfda9  8b44242c              mov       eax,[esp+2Ch]
  004cfdad  895e5c                mov       [esi+5Ch],ebx
  004cfdb0  895e50                mov       [esi+50h],ebx
  004cfdb3  895e58                mov       [esi+58h],ebx
  004cfdb6  89561c                mov       [esi+1Ch],edx
  004cfdb9  894624                mov       [esi+24h],eax
  004cfdbc  885e42                mov       [esi+42h],bl
  004cfdbf  c6464328              mov       byte [esi+43h],28h
  004cfdc3  66895e12              mov       [esi+12h],bx
  004cfdc7  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004cfdcd:
  004cfdcd  6aff                  push      0FFFFFFFFh
  004cfdcf  6aff                  push      0FFFFFFFFh
  004cfdd1  6aff                  push      0FFFFFFFFh
  004cfdd3  56                    push      esi
  004cfdd4  e8373affff            call      004C3810h
  004cfdd9  8b44242c              mov       eax,[esp+2Ch]
  004cfddd  83c410                add       esp,10h
  004cfde0  3bc3                  cmp       eax,ebx
  004cfde2  7517                  jne       short 004CFDFBh
  004cfde4  668b8c2482000000      mov       cx,[esp+82h]
  004cfdec  66894e32              mov       [esi+32h],cx
  004cfdf0  5f                    pop       edi
  004cfdf1  5e                    pop       esi
  004cfdf2  5d                    pop       ebp
  004cfdf3  5b                    pop       ebx
  004cfdf4  81c484000000          add       esp,84h
  004cfdfa  c3                    ret

LAB_004cfdfb:
  004cfdfb  385e43                cmp       [esi+43h],bl
  004cfdfe  0f852d030000          jne       near 004D0131h
  004cfe04  8b466c                mov       eax,[esi+6Ch]
  004cfe07  8b561c                mov       edx,[esi+1Ch]
  004cfe0a  8b781c                mov       edi,[eax+1Ch]
  004cfe0d  8b6824                mov       ebp,[eax+24h]
  004cfe10  8b4624                mov       eax,[esi+24h]
  004cfe13  2bfa                  sub       edi,edx
  004cfe15  2be8                  sub       ebp,eax
  004cfe17  c1ff0c                sar       edi,0Ch
  004cfe1a  c1fd0c                sar       ebp,0Ch
  004cfe1d  8bd7                  mov       edx,edi
  004cfe1f  8bc5                  mov       eax,ebp
  004cfe21  0fafd7                imul      edx,edi
  004cfe24  0fafc5                imul      eax,ebp
  004cfe27  03d0                  add       edx,eax
  004cfe29  52                    push      edx
  004cfe2a  e8e15b0700            call      00545A10h
  004cfe2f  55                    push      ebp
  004cfe30  57                    push      edi
  004cfe31  8944241c              mov       [esp+1Ch],eax
  004cfe35  e8e85b0700            call      00545A22h
  004cfe3a  668b4e32              mov       cx,[esi+32h]
  004cfe3e  66895e12              mov       [esi+12h],bx
  004cfe42  2bc1                  sub       eax,ecx
  004cfe44  25ff0f0000            and       eax,0FFFh
  004cfe49  89442418              mov       [esp+18h],eax
  004cfe4d  8b442414              mov       eax,[esp+14h]
  004cfe51  3dfa000000            cmp       eax,0FAh
  004cfe56  0f8dd6000000          jge       near 004CFF32h
  004cfe5c  e8df720700            call      00547140h
  004cfe61  99                    cdq
  004cfe62  b964000000            mov       ecx,64h
  004cfe67  f7f9                  idiv      ecx
  004cfe69  8bea                  mov       ebp,edx
  004cfe6b  83fd32                cmp       ebp,32h
  004cfe6e  0f8d8d000000          jge       near 004CFF01h
  004cfe74  8b542418              mov       edx,[esp+18h]
  004cfe78  8b7c2420              mov       edi,[esp+20h]
  004cfe7c  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004cfe83  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004cfe8a  25ff0f0000            and       eax,0FFFh
  004cfe8f  3bc1                  cmp       eax,ecx
  004cfe91  7f64                  jg        short 004CFEF7h
  004cfe93  e8a8720700            call      00547140h
  004cfe98  99                    cdq
  004cfe99  8d4c3f01              lea       ecx,[edi+edi+1]
  004cfe9d  f7f9                  idiv      ecx
  004cfe9f  8b442414              mov       eax,[esp+14h]
  004cfea3  83c264                add       edx,64h
  004cfea6  3bc2                  cmp       eax,edx
  004cfea8  7d4d                  jge       short 004CFEF7h
  004cfeaa  83fd1e                cmp       ebp,1Eh
  004cfead  7d24                  jge       short 004CFED3h
  004cfeaf  8b16                  mov       edx,[esi]
  004cfeb1  c646420b              mov       byte [esi+42h],0Bh
  004cfeb5  81e2fffffffe          and       edx,0FEFFFFFFh
  004cfebb  c6464301              mov       byte [esi+43h],1
  004cfebf  83ca02                or        edx,2
  004cfec2  895e5c                mov       [esi+5Ch],ebx
  004cfec5  8916                  mov       [esi],edx
  004cfec7  66c746180300          mov       word [esi+18h],3
  004cfecd  66895e1a              mov       [esi+1Ah],bx
  004cfed1  eb63                  jmp       short 004CFF36h

LAB_004cfed3:
  004cfed3  885e42                mov       [esi+42h],bl
  004cfed6  e865720700            call      00547140h
  004cfedb  99                    cdq
  004cfedc  b928000000            mov       ecx,28h
  004cfee1  66c746105000          mov       word [esi+10h],50h
  004cfee7  f7f9                  idiv      ecx
  004cfee9  8bc7                  mov       eax,edi
  004cfeeb  d1f8                  sar       eax,1
  004cfeed  02d0                  add       dl,al
  004cfeef  80c214                add       dl,14h
  004cfef2  885643                mov       [esi+43h],dl
  004cfef5  eb3f                  jmp       short 004CFF36h

LAB_004cfef7:
  004cfef7  c6464201              mov       byte [esi+42h],1
  004cfefb  c6464301              mov       byte [esi+43h],1
  004cfeff  eb35                  jmp       short 004CFF36h

LAB_004cff01:
  004cff01  83fd3c                cmp       ebp,3Ch
  004cff04  0f8c32020000          jl        near 004D013Ch
  004cff0a  83fd46                cmp       ebp,46h
  004cff0d  0f8d29020000          jge       near 004D013Ch
  004cff13  8b4c2410              mov       ecx,[esp+10h]
  004cff17  8b442414              mov       eax,[esp+14h]
  004cff1b  83c164                add       ecx,64h
  004cff1e  3bc1                  cmp       eax,ecx
  004cff20  0f8e16020000          jle       near 004D013Ch
  004cff26  c646420c              mov       byte [esi+42h],0Ch
  004cff2a  c6464301              mov       byte [esi+43h],1
  004cff2e  66895e12              mov       [esi+12h],bx

LAB_004cff32:
  004cff32  8b7c2420              mov       edi,[esp+20h]

LAB_004cff36:
  004cff36  8b442414              mov       eax,[esp+14h]
  004cff3a  3d58020000            cmp       eax,258h
  004cff3f  0f8df3000000          jge       near 004D0038h
  004cff45  3dfa000000            cmp       eax,0FAh
  004cff4a  0f8ce8000000          jl        near 004D0038h
  004cff50  e8eb710700            call      00547140h
  004cff55  99                    cdq
  004cff56  b964000000            mov       ecx,64h
  004cff5b  f7f9                  idiv      ecx
  004cff5d  83fa0f                cmp       edx,0Fh
  004cff60  7d36                  jge       short 004CFF98h
  004cff62  8b542418              mov       edx,[esp+18h]
  004cff66  81c200040000          add       edx,400h
  004cff6c  81e2ff0f0000          and       edx,0FFFh
  004cff72  81fa00080000          cmp       edx,800h
  004cff78  7c0d                  jl        short 004CFF87h
  004cff7a  c6464201              mov       byte [esi+42h],1
  004cff7e  c6464301              mov       byte [esi+43h],1
  004cff82  e9b1000000            jmp       004D0038h

LAB_004cff87:
  004cff87  c646420a              mov       byte [esi+42h],0Ah
  004cff8b  c6464301              mov       byte [esi+43h],1
  004cff8f  66895e12              mov       [esi+12h],bx
  004cff93  e9a0000000            jmp       004D0038h

LAB_004cff98:
  004cff98  83fa28                cmp       edx,28h
  004cff9b  7d25                  jge       short 004CFFC2h
  004cff9d  c6464202              mov       byte [esi+42h],2
  004cffa1  e89a710700            call      00547140h
  004cffa6  99                    cdq
  004cffa7  b90a000000            mov       ecx,0Ah
  004cffac  66c746105000          mov       word [esi+10h],50h
  004cffb2  f7f9                  idiv      ecx
  004cffb4  8bc7                  mov       eax,edi
  004cffb6  d1f8                  sar       eax,1
  004cffb8  02d0                  add       dl,al
  004cffba  80c214                add       dl,14h
  004cffbd  885643                mov       [esi+43h],dl
  004cffc0  eb76                  jmp       short 004D0038h

LAB_004cffc2:
  004cffc2  83fa46                cmp       edx,46h
  004cffc5  7d17                  jge       short 004CFFDEh
  004cffc7  c6464206              mov       byte [esi+42h],6
  004cffcb  c6464301              mov       byte [esi+43h],1
  004cffcf  e86c710700            call      00547140h
  004cffd4  66894610              mov       [esi+10h],ax
  004cffd8  66895e12              mov       [esi+12h],bx
  004cffdc  eb5a                  jmp       short 004D0038h

LAB_004cffde:
  004cffde  b964000000            mov       ecx,64h
  004cffe3  2bca                  sub       ecx,edx
  004cffe5  8b542410              mov       edx,[esp+10h]
  004cffe9  d1fa                  sar       edx,1
  004cffeb  83c20a                add       edx,0Ah
  004cffee  3bca                  cmp       ecx,edx
  004cfff0  7d0e                  jge       short 004D0000h
  004cfff2  c646420c              mov       byte [esi+42h],0Ch
  004cfff6  c6464301              mov       byte [esi+43h],1
  004cfffa  66895e12              mov       [esi+12h],bx
  004cfffe  eb38                  jmp       short 004D0038h

LAB_004d0000:
  004d0000  c6464203              mov       byte [esi+42h],3
  004d0004  66895e12              mov       [esi+12h],bx
  004d0008  e833710700            call      00547140h
  004d000d  99                    cdq
  004d000e  b90a000000            mov       ecx,0Ah
  004d0013  f7f9                  idiv      ecx
  004d0015  8bc7                  mov       eax,edi
  004d0017  d1f8                  sar       eax,1
  004d0019  02d0                  add       dl,al
  004d001b  80c214                add       dl,14h
  004d001e  885643                mov       [esi+43h],dl
  004d0021  e81a710700            call      00547140h
  004d0026  99                    cdq
  004d0027  33c2                  xor       eax,edx
  004d0029  2bc2                  sub       eax,edx
  004d002b  25ff0f0000            and       eax,0FFFh
  004d0030  33c2                  xor       eax,edx
  004d0032  2bc2                  sub       eax,edx
  004d0034  66894610              mov       [esi+10h],ax

LAB_004d0038:
  004d0038  817c241458020000      cmp       dword [esp+14h],258h
  004d0040  0f8ceb000000          jl        near 004D0131h
  004d0046  e8f5700700            call      00547140h
  004d004b  99                    cdq
  004d004c  b964000000            mov       ecx,64h
  004d0051  f7f9                  idiv      ecx
  004d0053  83fa1e                cmp       edx,1Eh
  004d0056  7d35                  jge       short 004D008Dh
  004d0058  885e42                mov       [esi+42h],bl
  004d005b  e8e0700700            call      00547140h
  004d0060  99                    cdq
  004d0061  b90a000000            mov       ecx,0Ah
  004d0066  66895e12              mov       [esi+12h],bx
  004d006a  f7f9                  idiv      ecx
  004d006c  8b4c2410              mov       ecx,[esp+10h]
  004d0070  8bc7                  mov       eax,edi
  004d0072  c1f802                sar       eax,2
  004d0075  02d0                  add       dl,al
  004d0077  885643                mov       [esi+43h],dl
  004d007a  8d540950              lea       edx,[ecx+ecx+50h]
  004d007e  66895610              mov       [esi+10h],dx
  004d0082  5f                    pop       edi
  004d0083  5e                    pop       esi
  004d0084  5d                    pop       ebp
  004d0085  5b                    pop       ebx
  004d0086  81c484000000          add       esp,84h
  004d008c  c3                    ret

LAB_004d008d:
  004d008d  83fa28                cmp       edx,28h
  004d0090  7d43                  jge       short 004D00D5h
  004d0092  c6464203              mov       byte [esi+42h],3
  004d0096  66895e12              mov       [esi+12h],bx
  004d009a  e8a1700700            call      00547140h
  004d009f  99                    cdq
  004d00a0  b90a000000            mov       ecx,0Ah
  004d00a5  f7f9                  idiv      ecx
  004d00a7  8bc7                  mov       eax,edi
  004d00a9  d1f8                  sar       eax,1
  004d00ab  02d0                  add       dl,al
  004d00ad  80c214                add       dl,14h
  004d00b0  885643                mov       [esi+43h],dl
  004d00b3  e888700700            call      00547140h
  004d00b8  99                    cdq
  004d00b9  33c2                  xor       eax,edx
  004d00bb  2bc2                  sub       eax,edx
  004d00bd  25ff0f0000            and       eax,0FFFh
  004d00c2  33c2                  xor       eax,edx
  004d00c4  2bc2                  sub       eax,edx
  004d00c6  66894610              mov       [esi+10h],ax
  004d00ca  5f                    pop       edi
  004d00cb  5e                    pop       esi
  004d00cc  5d                    pop       ebp
  004d00cd  5b                    pop       ebx
  004d00ce  81c484000000          add       esp,84h
  004d00d4  c3                    ret

LAB_004d00d5:
  004d00d5  83fa3c                cmp       edx,3Ch
  004d00d8  7d13                  jge       short 004D00EDh
  004d00da  c6464201              mov       byte [esi+42h],1
  004d00de  c6464301              mov       byte [esi+43h],1
  004d00e2  5f                    pop       edi
  004d00e3  5e                    pop       esi
  004d00e4  5d                    pop       ebp
  004d00e5  5b                    pop       ebx
  004d00e6  81c484000000          add       esp,84h
  004d00ec  c3                    ret

LAB_004d00ed:
  004d00ed  83fa50                cmp       edx,50h
  004d00f0  7d2e                  jge       short 004D0120h
  004d00f2  c6464202              mov       byte [esi+42h],2
  004d00f6  e845700700            call      00547140h
  004d00fb  99                    cdq
  004d00fc  b90a000000            mov       ecx,0Ah
  004d0101  66c746106400          mov       word [esi+10h],64h
  004d0107  f7f9                  idiv      ecx
  004d0109  8bc7                  mov       eax,edi
  004d010b  d1f8                  sar       eax,1
  004d010d  02d0                  add       dl,al
  004d010f  80c214                add       dl,14h
  004d0112  885643                mov       [esi+43h],dl
  004d0115  5f                    pop       edi
  004d0116  5e                    pop       esi
  004d0117  5d                    pop       ebp
  004d0118  5b                    pop       ebx
  004d0119  81c484000000          add       esp,84h
  004d011f  c3                    ret

LAB_004d0120:
  004d0120  c6464206              mov       byte [esi+42h],6
  004d0124  c6464301              mov       byte [esi+43h],1
  004d0128  e813700700            call      00547140h
  004d012d  66894610              mov       [esi+10h],ax

LAB_004d0131:
  004d0131  5f                    pop       edi
  004d0132  5e                    pop       esi
  004d0133  5d                    pop       ebp
  004d0134  5b                    pop       ebx
  004d0135  81c484000000          add       esp,84h
  004d013b  c3                    ret

LAB_004d013c:
  004d013c  c6464203              mov       byte [esi+42h],3
  004d0140  66895e12              mov       [esi+12h],bx
  004d0144  e8f76f0700            call      00547140h
  004d0149  99                    cdq
  004d014a  b90a000000            mov       ecx,0Ah
  004d014f  8b7c2420              mov       edi,[esp+20h]
  004d0153  f7f9                  idiv      ecx
  004d0155  8bc7                  mov       eax,edi
  004d0157  d1f8                  sar       eax,1
  004d0159  02d0                  add       dl,al
  004d015b  80c214                add       dl,14h
  004d015e  885643                mov       [esi+43h],dl
  004d0161  e8da6f0700            call      00547140h
  004d0166  99                    cdq
  004d0167  33c2                  xor       eax,edx
  004d0169  2bc2                  sub       eax,edx
  004d016b  25ff0f0000            and       eax,0FFFh
  004d0170  33c2                  xor       eax,edx
  004d0172  2bc2                  sub       eax,edx
  004d0174  66894610              mov       [esi+10h],ax
  004d0178  e9b9fdffff            jmp       004CFF36h
  004d017d  90                    nop
  004d017e  8bff                  mov       edi,edi
  004d0180  0df64c0053            or        eax,53004CF6h
  004d0185  f64c00c9f6            test      byte [eax+eax-37h],0F6h
  004d018a  4c                    dec       esp
  004d018b  006ff7                add       [edi-9],ch
  004d018e  4c                    dec       esp
  004d018f  00ce                  add       dh,cl
  004d0191  fb                    sti
  004d0192  4c                    dec       esp
  004d0193  00ce                  add       dh,cl
  004d0195  fb                    sti
  004d0196  4c                    dec       esp
  004d0197  003df84c00ce          add       [0CE004CF8h],bh
  004d019d  f8                    clc
  004d019e  4c                    dec       esp
  004d019f  00ce                  add       dh,cl
  004d01a1  fb                    sti
  004d01a2  4c                    dec       esp
  004d01a3  00ce                  add       dh,cl
  004d01a5  fb                    sti
  004d01a6  4c                    dec       esp
  004d01a7  002e                  add       [esi],ch
  004d01a9  f9                    stc
  004d01aa  4c                    dec       esp
  004d01ab  009dfa4c00d4          add       [ebp-2BFFB306h],bl
  004d01b1  fa                    cli
  004d01b2  4c                    dec       esp
  004d01b3  009090909090          add       [eax-6F6F6F70h],dl
  004d01b9  90                    nop
  004d01ba  90                    nop
  004d01bb  90                    nop
  004d01bc  90                    nop
  004d01bd  90                    nop
  004d01be  90                    nop
  004d01bf  90                    nop

72 cible(s) de saut interne : 0x004cf3f1, 0x004cf540, 0x004cf562, 0x004cf564, 0x004cf571, 0x004cf573, 0x004cf5d6, 0x004cf5da, 0x004cf5f3, 0x004cf5f8, 0x004cf62d, 0x004cf674, 0x004cf6b2, 0x004cf6fd, 0x004cf711, 0x004cf714, 0x004cf71c, 0x004cf728, 0x004cf75f, 0x004cf786, 0x004cf801, 0x004cf80f, 0x004cf813, 0x004cf84e, 0x004cf8c0, 0x004cf8eb, 0x004cf8f7, 0x004cf93f, 0x004cf950, 0x004cf975, 0x004cf9b0, 0x004cf9cd, 0x004cfa14, 0x004cfa42, 0x004cfa7b, 0x004cfab7, 0x004cfadf, 0x004cfaf0, 0x004cfb05, 0x004cfb17, 0x004cfb42, 0x004cfb85, 0x004cfba5, 0x004cfbc3, 0x004cfbcb, 0x004cfbce, 0x004cfca8, 0x004cfcb4, 0x004cfcb6, 0x004cfcc7, 0x004cfd05, 0x004cfd95, 0x004cfda5, 0x004cfdcd, 0x004cfdfb, 0x004cfed3, 0x004cfef7, 0x004cff01, 0x004cff32, 0x004cff36, 0x004cff87, 0x004cff98, 0x004cffc2, 0x004cffde, 0x004d0000, 0x004d0038, 0x004d008d, 0x004d00d5, 0x004d00ed, 0x004d0120, 0x004d0131, 0x004d013c
