C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049a360  section .text  offset fichier 0x99760  1568 octets

  0049a360  83ec28                sub       esp,28h
  0049a363  53                    push      ebx
  0049a364  55                    push      ebp
  0049a365  56                    push      esi
  0049a366  8b742438              mov       esi,[esp+38h]
  0049a36a  b850000000            mov       eax,50h
  0049a36f  57                    push      edi
  0049a370  6689442430            mov       [esp+30h],ax
  0049a375  6689442436            mov       [esp+36h],ax
  0049a37a  8b06                  mov       eax,[esi]
  0049a37c  bf01000000            mov       edi,1
  0049a381  0bc7                  or        eax,edi
  0049a383  8d4c2428              lea       ecx,[esp+28h]
  0049a387  8906                  mov       [esi],eax
  0049a389  8d442424              lea       eax,[esp+24h]
  0049a38d  50                    push      eax
  0049a38e  8d542424              lea       edx,[esp+24h]
  0049a392  51                    push      ecx
  0049a393  33ed                  xor       ebp,ebp
  0049a395  52                    push      edx
  0049a396  56                    push      esi
  0049a397  66896c243c            mov       [esp+3Ch],bp
  0049a39c  66c744243e1e00        mov       word [esp+3Eh],1Eh
  0049a3a3  66c74424426400        mov       word [esp+42h],64h
  0049a3aa  66c744244400ff        mov       word [esp+44h],0FF00h
  0049a3b1  e8cae50900            call      00538980h
  0049a3b6  83c410                add       esp,10h
  0049a3b9  8d44241c              lea       eax,[esp+1Ch]
  0049a3bd  8d4c2418              lea       ecx,[esp+18h]
  0049a3c1  8d542414              lea       edx,[esp+14h]
  0049a3c5  50                    push      eax
  0049a3c6  51                    push      ecx
  0049a3c7  8d442418              lea       eax,[esp+18h]
  0049a3cb  52                    push      edx
  0049a3cc  8d4c2448              lea       ecx,[esp+48h]
  0049a3d0  50                    push      eax
  0049a3d1  51                    push      ecx
  0049a3d2  56                    push      esi
  0049a3d3  e848bb0900            call      00535F20h
  0049a3d8  8b4c2454              mov       ecx,[esp+54h]
  0049a3dc  8b442430              mov       eax,[esp+30h]
  0049a3e0  bb40000000            mov       ebx,40h
  0049a3e5  83c418                add       esp,18h
  0049a3e8  2bd9                  sub       ebx,ecx
  0049a3ea  3bc5                  cmp       eax,ebp
  0049a3ec  0f8489020000          je        near 0049A67Bh
  0049a3f2  56                    push      esi
  0049a3f3  e8a8110400            call      004DB5A0h
  0049a3f8  8b06                  mov       eax,[esi]
  0049a3fa  83c404                add       esp,4
  0049a3fd  a802                  test      al,2
  0049a3ff  0f849f000000          je        near 0049A4A4h
  0049a405  f644241c10            test      byte [esp+1Ch],10h
  0049a40a  740d                  je        short 0049A419h
  0049a40c  24fd                  and       al,0FDh
  0049a40e  c6464300              mov       byte [esi+43h],0
  0049a412  8906                  mov       [esi],eax
  0049a414  e962020000            jmp       0049A67Bh

LAB_0049a419:
  0049a419  8d54242c              lea       edx,[esp+2Ch]
  0049a41d  55                    push      ebp
  0049a41e  8d461c                lea       eax,[esi+1Ch]
  0049a421  52                    push      edx
  0049a422  50                    push      eax
  0049a423  56                    push      esi
  0049a424  e807f80700            call      00519C30h
  0049a429  8bf8                  mov       edi,eax
  0049a42b  a1b8e05d00            mov       eax,[5DE0B8h]
  0049a430  83c410                add       esp,10h
  0049a433  3bc5                  cmp       eax,ebp
  0049a435  7432                  je        short 0049A469h
  0049a437  8b2f                  mov       ebp,[edi]
  0049a439  57                    push      edi
  0049a43a  83cd40                or        ebp,40h
  0049a43d  56                    push      esi
  0049a43e  66c787960000001000    mov       word [edi+96h],10h
  0049a447  892f                  mov       [edi],ebp
  0049a449  e832f70900            call      00539B80h
  0049a44e  83c408                add       esp,8
  0049a451  83c71c                add       edi,1Ch
  0049a454  57                    push      edi
  0049a455  6800180000            push      1800h
  0049a45a  6800030000            push      300h
  0049a45f  6a03                  push      3
  0049a461  e8ba360a00            call      0053DB20h
  0049a466  83c410                add       esp,10h

LAB_0049a469:
  0049a469  8b4c243c              mov       ecx,[esp+3Ch]
  0049a46d  8b465c                mov       eax,[esi+5Ch]
  0049a470  81c180020000          add       ecx,280h
  0049a476  c1e107                shl       ecx,7
  0049a479  2bc8                  sub       ecx,eax
  0049a47b  c1f904                sar       ecx,4
  0049a47e  03c8                  add       ecx,eax
  0049a480  8a4643                mov       al,[esi+43h]
  0049a483  84c0                  test      al,al
  0049a485  894e5c                mov       [esi+5Ch],ecx
  0049a488  0f84ed010000          je        near 0049A67Bh
  0049a48e  fec8                  dec       al
  0049a490  884643                mov       [esi+43h],al
  0049a493  0f85e2010000          jne       near 0049A67Bh
  0049a499  8b06                  mov       eax,[esi]
  0049a49b  24fd                  and       al,0FDh
  0049a49d  8906                  mov       [esi],eax
  0049a49f  e9d7010000            jmp       0049A67Bh

LAB_0049a4a4:
  0049a4a4  a810                  test      al,10h
  0049a4a6  0f85cf010000          jne       near 0049A67Bh
  0049a4ac  a904080000            test      eax,804h
  0049a4b1  0f85c4010000          jne       near 0049A67Bh
  0049a4b7  33c9                  xor       ecx,ecx
  0049a4b9  8a4e42                mov       cl,[esi+42h]
  0049a4bc  83f903                cmp       ecx,3
  0049a4bf  0f87b6010000          ja        near 0049A67Bh
  0049a4c5  ff248dd0a84900        jmp       dword [ecx*4+49A8D0h]
  0049a4cc  66396e18              cmp       [esi+18h],bp
  0049a4d0  740a                  je        short 0049A4DCh
  0049a4d2  0d00000001            or        eax,1000000h
  0049a4d7  896e18                mov       [esi+18h],ebp
  0049a4da  8906                  mov       [esi],eax

LAB_0049a4dc:
  0049a4dc  817c24102c010000      cmp       dword [esp+10h],12Ch
  0049a4e4  7d09                  jge       short 0049A4EFh
  0049a4e6  c6464300              mov       byte [esi+43h],0
  0049a4ea  e98c010000            jmp       0049A67Bh

LAB_0049a4ef:
  0049a4ef  8a4643                mov       al,[esi+43h]
  0049a4f2  84c0                  test      al,al
  0049a4f4  7405                  je        short 0049A4FBh
  0049a4f6  fec8                  dec       al
  0049a4f8  884643                mov       [esi+43h],al

LAB_0049a4fb:
  0049a4fb  8b465c                mov       eax,[esi+5Ch]
  0049a4fe  8bd0                  mov       edx,eax
  0049a500  c1fa04                sar       edx,4
  0049a503  2bc2                  sub       eax,edx
  0049a505  89465c                mov       [esi+5Ch],eax
  0049a508  e96e010000            jmp       0049A67Bh
  0049a50d  66397e18              cmp       [esi+18h],di
  0049a511  740a                  je        short 0049A51Dh
  0049a513  0d00000001            or        eax,1000000h
  0049a518  897e18                mov       [esi+18h],edi
  0049a51b  8906                  mov       [esi],eax

LAB_0049a51d:
  0049a51d  8b44243c              mov       eax,[esp+3Ch]
  0049a521  8b4c2414              mov       ecx,[esp+14h]
  0049a525  c1f802                sar       eax,2
  0049a528  83c038                add       eax,38h
  0049a52b  8d1408                lea       edx,[eax+ecx]
  0049a52e  8d3c00                lea       edi,[eax+eax]
  0049a531  81e2ff0f0000          and       edx,0FFFh
  0049a537  3bd7                  cmp       edx,edi
  0049a539  7f1a                  jg        short 0049A555h
  0049a53b  8b465c                mov       eax,[esi+5Ch]
  0049a53e  66014e32              add       [esi+32h],cx
  0049a542  8bc8                  mov       ecx,eax
  0049a544  c6464300              mov       byte [esi+43h],0
  0049a548  c1f904                sar       ecx,4
  0049a54b  2bc1                  sub       eax,ecx
  0049a54d  89465c                mov       [esi+5Ch],eax
  0049a550  e926010000            jmp       0049A67Bh

LAB_0049a555:
  0049a555  81e1ff0f0000          and       ecx,0FFFh
  0049a55b  81f900080000          cmp       ecx,800h
  0049a561  7c02                  jl        short 0049A565h
  0049a563  f7d8                  neg       eax

LAB_0049a565:
  0049a565  66014632              add       [esi+32h],ax
  0049a569  8b465c                mov       eax,[esi+5Ch]
  0049a56c  8bc8                  mov       ecx,eax
  0049a56e  c1f904                sar       ecx,4
  0049a571  2bc1                  sub       eax,ecx
  0049a573  89465c                mov       [esi+5Ch],eax
  0049a576  e900010000            jmp       0049A67Bh
  0049a57b  66397e18              cmp       [esi+18h],di
  0049a57f  740a                  je        short 0049A58Bh
  0049a581  0d00000001            or        eax,1000000h
  0049a586  897e18                mov       [esi+18h],edi
  0049a589  8906                  mov       [esi],eax

LAB_0049a58b:
  0049a58b  817c24102c010000      cmp       dword [esp+10h],12Ch
  0049a593  7d09                  jge       short 0049A59Eh
  0049a595  c6464300              mov       byte [esi+43h],0
  0049a599  e9dd000000            jmp       0049A67Bh

LAB_0049a59e:
  0049a59e  8a4643                mov       al,[esi+43h]
  0049a5a1  84c0                  test      al,al
  0049a5a3  7405                  je        short 0049A5AAh
  0049a5a5  fec8                  dec       al
  0049a5a7  884643                mov       [esi+43h],al

LAB_0049a5aa:
  0049a5aa  8b54243c              mov       edx,[esp+3Ch]
  0049a5ae  8b4c2414              mov       ecx,[esp+14h]
  0049a5b2  8bc2                  mov       eax,edx
  0049a5b4  c1f802                sar       eax,2
  0049a5b7  83c038                add       eax,38h
  0049a5ba  8d3c08                lea       edi,[eax+ecx]
  0049a5bd  8d2c00                lea       ebp,[eax+eax]
  0049a5c0  81e7ff0f0000          and       edi,0FFFh
  0049a5c6  3bfd                  cmp       edi,ebp
  0049a5c8  7f06                  jg        short 0049A5D0h
  0049a5ca  66014e32              add       [esi+32h],cx
  0049a5ce  eb14                  jmp       short 0049A5E4h

LAB_0049a5d0:
  0049a5d0  81e1ff0f0000          and       ecx,0FFFh
  0049a5d6  81f900080000          cmp       ecx,800h
  0049a5dc  7c02                  jl        short 0049A5E0h
  0049a5de  f7d8                  neg       eax

LAB_0049a5e0:
  0049a5e0  66014632              add       [esi+32h],ax

LAB_0049a5e4:
  0049a5e4  8b465c                mov       eax,[esi+5Ch]
  0049a5e7  81c2a0010000          add       edx,1A0h
  0049a5ed  c1e207                shl       edx,7
  0049a5f0  2bd0                  sub       edx,eax
  0049a5f2  c1fa04                sar       edx,4
  0049a5f5  03d0                  add       edx,eax
  0049a5f7  89565c                mov       [esi+5Ch],edx
  0049a5fa  eb7f                  jmp       short 0049A67Bh
  0049a5fc  8a4e43                mov       cl,[esi+43h]
  0049a5ff  84c9                  test      cl,cl
  0049a601  7405                  je        short 0049A608h
  0049a603  fec9                  dec       cl
  0049a605  884e43                mov       [esi+43h],cl

LAB_0049a608:
  0049a608  66397e18              cmp       [esi+18h],di
  0049a60c  740a                  je        short 0049A618h
  0049a60e  0d00000001            or        eax,1000000h
  0049a613  897e18                mov       [esi+18h],edi
  0049a616  8906                  mov       [esi],eax

LAB_0049a618:
  0049a618  8b4c243c              mov       ecx,[esp+3Ch]
  0049a61c  8b465c                mov       eax,[esi+5Ch]
  0049a61f  8d9140010000          lea       edx,[ecx+140h]
  0049a625  c1e207                shl       edx,7
  0049a628  2bd0                  sub       edx,eax
  0049a62a  c1fa04                sar       edx,4
  0049a62d  03d0                  add       edx,eax
  0049a62f  668b4612              mov       ax,[esi+12h]
  0049a633  89565c                mov       [esi+5Ch],edx
  0049a636  668b5632              mov       dx,[esi+32h]
  0049a63a  2bc2                  sub       eax,edx
  0049a63c  25ff0f0000            and       eax,0FFFh
  0049a641  8db800010000          lea       edi,[eax+100h]
  0049a647  81e7ff0f0000          and       edi,0FFFh
  0049a64d  81ff00020000          cmp       edi,200h
  0049a653  7e26                  jle       short 0049A67Bh
  0049a655  25ff0f0000            and       eax,0FFFh
  0049a65a  3d00080000            cmp       eax,800h
  0049a65f  7d0a                  jge       short 0049A66Bh
  0049a661  c1f902                sar       ecx,2
  0049a664  83c138                add       ecx,38h
  0049a667  8bc1                  mov       eax,ecx
  0049a669  eb0a                  jmp       short 0049A675h

LAB_0049a66b:
  0049a66b  c1f902                sar       ecx,2
  0049a66e  b8c8ffffff            mov       eax,0FFFFFFC8h
  0049a673  2bc1                  sub       eax,ecx

LAB_0049a675:
  0049a675  03d0                  add       edx,eax
  0049a677  66895632              mov       [esi+32h],dx

LAB_0049a67b:
  0049a67b  8b442418              mov       eax,[esp+18h]
  0049a67f  50                    push      eax
  0049a680  56                    push      esi
  0049a681  e85a020000            call      0049A8E0h
  0049a686  83c408                add       esp,8
  0049a689  56                    push      esi
  0049a68a  e8b1920900            call      00533940h
  0049a68f  8b9690000000          mov       edx,[esi+90h]
  0049a695  8b466c                mov       eax,[esi+6Ch]
  0049a698  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  0049a69e  83c404                add       esp,4
  0049a6a1  899690000000          mov       [esi+90h],edx
  0049a6a7  8a08                  mov       cl,[eax]
  0049a6a9  f6c101                test      cl,1
  0049a6ac  7506                  jne       short 0049A6B4h
  0049a6ae  8b4860                mov       ecx,[eax+60h]
  0049a6b1  894e48                mov       [esi+48h],ecx

LAB_0049a6b4:
  0049a6b4  e887ca0a00            call      00547140h
  0049a6b9  8b7e60                mov       edi,[esi+60h]
  0049a6bc  25ff000000            and       eax,0FFh
  0049a6c1  0500010000            add       eax,100h
  0049a6c6  66014614              add       [esi+14h],ax
  0049a6ca  8b4648                mov       eax,[esi+48h]
  0049a6cd  668b4e14              mov       cx,[esi+14h]
  0049a6d1  3bc7                  cmp       eax,edi
  0049a6d3  7c02                  jl        short 0049A6D7h
  0049a6d5  8bf8                  mov       edi,eax

LAB_0049a6d7:
  0049a6d7  f60602                test      byte [esi],2
  0049a6da  7415                  je        short 0049A6F1h
  0049a6dc  8b4620                mov       eax,[esi+20h]
  0049a6df  2bf8                  sub       edi,eax
  0049a6e1  81c700800200          add       edi,28000h
  0049a6e7  c1ff03                sar       edi,3
  0049a6ea  03f8                  add       edi,eax
  0049a6ec  897e20                mov       [esi+20h],edi
  0049a6ef  eb20                  jmp       short 0049A711h

LAB_0049a6f1:
  0049a6f1  8b6e20                mov       ebp,[esi+20h]
  0049a6f4  0fbfd1                movsx     edx,cx
  0049a6f7  52                    push      edx
  0049a6f8  e81fb30a00            call      00545A1Ch
  0049a6fd  0500320000            add       eax,3200h
  0049a702  c1e006                shl       eax,6
  0049a705  03c7                  add       eax,edi
  0049a707  2bc5                  sub       eax,ebp
  0049a709  c1f804                sar       eax,4
  0049a70c  03c5                  add       eax,ebp
  0049a70e  894620                mov       [esi+20h],eax

LAB_0049a711:
  0049a711  8b442420              mov       eax,[esp+20h]
  0049a715  50                    push      eax
  0049a716  56                    push      esi
  0049a717  e8d4750000            call      004A1CF0h
  0049a71c  8b442420              mov       eax,[esp+20h]
  0049a720  83c408                add       esp,8
  0049a723  85c0                  test      eax,eax
  0049a725  0f849d010000          je        near 0049A8C8h
  0049a72b  f70616080000          test      dword [esi],816h
  0049a731  0f8591010000          jne       near 0049A8C8h
  0049a737  8a4643                mov       al,[esi+43h]
  0049a73a  84c0                  test      al,al
  0049a73c  0f8586010000          jne       near 0049A8C8h
  0049a742  8b466c                mov       eax,[esi+6Ch]
  0049a745  8b4e1c                mov       ecx,[esi+1Ch]
  0049a748  8b5624                mov       edx,[esi+24h]
  0049a74b  8b781c                mov       edi,[eax+1Ch]
  0049a74e  8b6824                mov       ebp,[eax+24h]
  0049a751  2bf9                  sub       edi,ecx
  0049a753  2bea                  sub       ebp,edx
  0049a755  c1ff0c                sar       edi,0Ch
  0049a758  c1fd0c                sar       ebp,0Ch
  0049a75b  8bcf                  mov       ecx,edi
  0049a75d  8bd5                  mov       edx,ebp
  0049a75f  0fafcf                imul      ecx,edi
  0049a762  0fafd5                imul      edx,ebp
  0049a765  03ca                  add       ecx,edx
  0049a767  51                    push      ecx
  0049a768  e8a3b20a00            call      00545A10h
  0049a76d  55                    push      ebp
  0049a76e  57                    push      edi
  0049a76f  89442418              mov       [esp+18h],eax
  0049a773  e8aab20a00            call      00545A22h
  0049a778  668b4e32              mov       cx,[esi+32h]
  0049a77c  2bc1                  sub       eax,ecx
  0049a77e  25ff0f0000            and       eax,0FFFh
  0049a783  89442414              mov       [esp+14h],eax
  0049a787  8b442410              mov       eax,[esp+10h]
  0049a78b  3d2c010000            cmp       eax,12Ch
  0049a790  0f8db2000000          jge       near 0049A848h
  0049a796  e8a5c90a00            call      00547140h
  0049a79b  99                    cdq
  0049a79c  b964000000            mov       ecx,64h
  0049a7a1  f7f9                  idiv      ecx
  0049a7a3  83fa3c                cmp       edx,3Ch
  0049a7a6  7d56                  jge       short 0049A7FEh
  0049a7a8  8b542414              mov       edx,[esp+14h]
  0049a7ac  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0049a7b3  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0049a7ba  25ff0f0000            and       eax,0FFFh
  0049a7bf  3bc1                  cmp       eax,ecx
  0049a7c1  0f8fcd000000          jg        near 0049A894h
  0049a7c7  e874c90a00            call      00547140h
  0049a7cc  99                    cdq
  0049a7cd  8d4c1b01              lea       ecx,[ebx+ebx+1]
  0049a7d1  f7f9                  idiv      ecx
  0049a7d3  8b4c2410              mov       ecx,[esp+10h]
  0049a7d7  b8fa000000            mov       eax,0FAh
  0049a7dc  2bc2                  sub       eax,edx
  0049a7de  3bc8                  cmp       ecx,eax
  0049a7e0  0f8dae000000          jge       near 0049A894h
  0049a7e6  8b16                  mov       edx,[esi]
  0049a7e8  c1fb03                sar       ebx,3
  0049a7eb  83ca02                or        edx,2
  0049a7ee  80c310                add       bl,10h
  0049a7f1  8916                  mov       [esi],edx
  0049a7f3  885e43                mov       [esi+43h],bl
  0049a7f6  5f                    pop       edi
  0049a7f7  5e                    pop       esi
  0049a7f8  5d                    pop       ebp
  0049a7f9  5b                    pop       ebx
  0049a7fa  83c428                add       esp,28h
  0049a7fd  c3                    ret

LAB_0049a7fe:
  0049a7fe  c6464203              mov       byte [esi+42h],3
  0049a802  e839c90a00            call      00547140h
  0049a807  99                    cdq
  0049a808  b90a000000            mov       ecx,0Ah
  0049a80d  f7f9                  idiv      ecx
  0049a80f  c1fb02                sar       ebx,2
  0049a812  02d3                  add       dl,bl
  0049a814  02d1                  add       dl,cl
  0049a816  885643                mov       [esi+43h],dl
  0049a819  8b54243c              mov       edx,[esp+3Ch]
  0049a81d  8dba80000000          lea       edi,[edx+80h]
  0049a823  c1e704                shl       edi,4
  0049a826  e815c90a00            call      00547140h
  0049a82b  99                    cdq
  0049a82c  f7ff                  idiv      edi
  0049a82e  8b4c2414              mov       ecx,[esp+14h]
  0049a832  d1ff                  sar       edi,1
  0049a834  2bd7                  sub       edx,edi
  0049a836  03d1                  add       edx,ecx
  0049a838  66035632              add       dx,[esi+32h]
  0049a83c  66895612              mov       [esi+12h],dx
  0049a840  5f                    pop       edi
  0049a841  5e                    pop       esi
  0049a842  5d                    pop       ebp
  0049a843  5b                    pop       ebx
  0049a844  83c428                add       esp,28h
  0049a847  c3                    ret

LAB_0049a848:
  0049a848  3d58020000            cmp       eax,258h
  0049a84d  7d0b                  jge       short 0049A85Ah
  0049a84f  e8ecc80a00            call      00547140h
  0049a854  c6464202              mov       byte [esi+42h],2
  0049a858  eb16                  jmp       short 0049A870h

LAB_0049a85a:
  0049a85a  e8e1c80a00            call      00547140h
  0049a85f  99                    cdq
  0049a860  b964000000            mov       ecx,64h
  0049a865  f7f9                  idiv      ecx
  0049a867  83fa1e                cmp       edx,1Eh
  0049a86a  7d23                  jge       short 0049A88Fh
  0049a86c  c6464200              mov       byte [esi+42h],0

LAB_0049a870:
  0049a870  e8cbc80a00            call      00547140h
  0049a875  99                    cdq
  0049a876  b90a000000            mov       ecx,0Ah
  0049a87b  f7f9                  idiv      ecx
  0049a87d  c1fb02                sar       ebx,2
  0049a880  02d3                  add       dl,bl
  0049a882  02d1                  add       dl,cl
  0049a884  885643                mov       [esi+43h],dl
  0049a887  5f                    pop       edi
  0049a888  5e                    pop       esi
  0049a889  5d                    pop       ebp
  0049a88a  5b                    pop       ebx
  0049a88b  83c428                add       esp,28h
  0049a88e  c3                    ret

LAB_0049a88f:
  0049a88f  83fa3c                cmp       edx,3Ch
  0049a892  7d10                  jge       short 0049A8A4h

LAB_0049a894:
  0049a894  c6464201              mov       byte [esi+42h],1
  0049a898  c6464301              mov       byte [esi+43h],1
  0049a89c  5f                    pop       edi
  0049a89d  5e                    pop       esi
  0049a89e  5d                    pop       ebp
  0049a89f  5b                    pop       ebx
  0049a8a0  83c428                add       esp,28h
  0049a8a3  c3                    ret

LAB_0049a8a4:
  0049a8a4  c6464203              mov       byte [esi+42h],3
  0049a8a8  e893c80a00            call      00547140h
  0049a8ad  99                    cdq
  0049a8ae  b90a000000            mov       ecx,0Ah
  0049a8b3  f7f9                  idiv      ecx
  0049a8b5  c1fb02                sar       ebx,2
  0049a8b8  02d3                  add       dl,bl
  0049a8ba  02d1                  add       dl,cl
  0049a8bc  885643                mov       [esi+43h],dl
  0049a8bf  e87cc80a00            call      00547140h
  0049a8c4  66894612              mov       [esi+12h],ax

LAB_0049a8c8:
  0049a8c8  5f                    pop       edi
  0049a8c9  5e                    pop       esi
  0049a8ca  5d                    pop       ebp
  0049a8cb  5b                    pop       ebx
  0049a8cc  83c428                add       esp,28h
  0049a8cf  c3                    ret
  0049a8d0  cc                    int3
  0049a8d1  a4                    movsb
  0049a8d2  49                    dec       ecx
  0049a8d3  000da549007b          add       [7B0049A5h],cl
  0049a8d9  a5                    movsd
  0049a8da  49                    dec       ecx
  0049a8db  00fc                  add       ah,bh
  0049a8dd  a5                    movsd
  0049a8de  49                    dec       ecx
  0049a8df  0083ec285355          add       [ebx+555328ECh],al
  0049a8e5  56                    push      esi
  0049a8e6  8b742438              mov       esi,[esp+38h]
  0049a8ea  57                    push      edi
  0049a8eb  0fbf7e32              movsx     edi,word [esi+32h]
  0049a8ef  57                    push      edi
  0049a8f0  e827b10a00            call      00545A1Ch
  0049a8f5  57                    push      edi
  0049a8f6  89442414              mov       [esp+14h],eax
  0049a8fa  e817b10a00            call      00545A16h
  0049a8ff  8b5650                mov       edx,[esi+50h]
  0049a902  89442418              mov       [esp+18h],eax
  0049a906  8bc2                  mov       eax,edx
  0049a908  8b4e5c                mov       ecx,[esi+5Ch]
  0049a90b  c1f803                sar       eax,3
  0049a90e  0faf4c2440            imul      ecx,[esp+40h]
  0049a913  8bfa                  mov       edi,edx
  0049a915  2bf8                  sub       edi,eax
  0049a917  c1fa08                sar       edx,8
  0049a91a  2bfa                  sub       edi,edx
  0049a91c  8b5654                mov       edx,[esi+54h]
  0049a91f  8bc2                  mov       eax,edx
  0049a921  8bda                  mov       ebx,edx
  0049a923  c1f808                sar       eax,8
  0049a926  2bd8                  sub       ebx,eax
  0049a928  897e50                mov       [esi+50h],edi
  0049a92b  c1fa03                sar       edx,3
  0049a92e  2bda                  sub       ebx,edx
  0049a930  8b5658                mov       edx,[esi+58h]
  0049a933  8bea                  mov       ebp,edx
  0049a935  8bc2                  mov       eax,edx
  0049a937  c1fd03                sar       ebp,3
  0049a93a  2bc5                  sub       eax,ebp
  0049a93c  8b6e20                mov       ebp,[esi+20h]
  0049a93f  c1fa08                sar       edx,8
  0049a942  c1f90c                sar       ecx,0Ch
  0049a945  2bc2                  sub       eax,edx
  0049a947  8bd1                  mov       edx,ecx
  0049a949  0faf542410            imul      edx,[esp+10h]
  0049a94e  0faf4c2418            imul      ecx,[esp+18h]
  0049a953  c1fa0c                sar       edx,0Ch
  0049a956  c1f90c                sar       ecx,0Ch
  0049a959  03d7                  add       edx,edi
  0049a95b  8b7e1c                mov       edi,[esi+1Ch]
  0049a95e  894658                mov       [esi+58h],eax
  0049a961  03c8                  add       ecx,eax
  0049a963  8b4624                mov       eax,[esi+24h]
  0049a966  03fa                  add       edi,edx
  0049a968  03eb                  add       ebp,ebx
  0049a96a  03c1                  add       eax,ecx
  0049a96c  897e1c                mov       [esi+1Ch],edi
  0049a96f  895e54                mov       [esi+54h],ebx
  0049a972  896e20                mov       [esi+20h],ebp
  0049a975  894624                mov       [esi+24h],eax
  0049a978  5f                    pop       edi
  0049a979  5e                    pop       esi
  0049a97a  5d                    pop       ebp
  0049a97b  5b                    pop       ebx
  0049a97c  83c428                add       esp,28h
  0049a97f  c3                    ret

32 cible(s) de saut interne : 0x0049a419, 0x0049a469, 0x0049a4a4, 0x0049a4dc, 0x0049a4ef, 0x0049a4fb, 0x0049a51d, 0x0049a555, 0x0049a565, 0x0049a58b, 0x0049a59e, 0x0049a5aa, 0x0049a5d0, 0x0049a5e0, 0x0049a5e4, 0x0049a608, 0x0049a618, 0x0049a66b, 0x0049a675, 0x0049a67b, 0x0049a6b4, 0x0049a6d7, 0x0049a6f1, 0x0049a711, 0x0049a7fe, 0x0049a848, 0x0049a85a, 0x0049a870, 0x0049a88f, 0x0049a894, 0x0049a8a4, 0x0049a8c8
