C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a4270  section .text  offset fichier 0xa3670  2141 octets

  004a4270  83ec2c                sub       esp,2Ch
  004a4273  53                    push      ebx
  004a4274  55                    push      ebp
  004a4275  56                    push      esi
  004a4276  8b74243c              mov       esi,[esp+3Ch]
  004a427a  8d44241c              lea       eax,[esp+1Ch]
  004a427e  57                    push      edi
  004a427f  8d4c2438              lea       ecx,[esp+38h]
  004a4283  50                    push      eax
  004a4284  8d542438              lea       edx,[esp+38h]
  004a4288  51                    push      ecx
  004a4289  52                    push      edx
  004a428a  56                    push      esi
  004a428b  e8f0460900            call      00538980h
  004a4290  83c410                add       esp,10h
  004a4293  8d442424              lea       eax,[esp+24h]
  004a4297  8d4c241c              lea       ecx,[esp+1Ch]
  004a429b  8d542440              lea       edx,[esp+40h]
  004a429f  50                    push      eax
  004a42a0  51                    push      ecx
  004a42a1  8d44241c              lea       eax,[esp+1Ch]
  004a42a5  52                    push      edx
  004a42a6  8d4c241c              lea       ecx,[esp+1Ch]
  004a42aa  50                    push      eax
  004a42ab  51                    push      ecx
  004a42ac  56                    push      esi
  004a42ad  e86e1c0900            call      00535F20h
  004a42b2  8b4c2428              mov       ecx,[esp+28h]
  004a42b6  8b442434              mov       eax,[esp+34h]
  004a42ba  bb40000000            mov       ebx,40h
  004a42bf  83c418                add       esp,18h
  004a42c2  2bd9                  sub       ebx,ecx
  004a42c4  bf01000000            mov       edi,1
  004a42c9  85c0                  test      eax,eax
  004a42cb  895c2418              mov       [esp+18h],ebx
  004a42cf  0f8403020000          je        near 004A44D8h
  004a42d5  56                    push      esi
  004a42d6  e8c5720300            call      004DB5A0h
  004a42db  8b06                  mov       eax,[esi]
  004a42dd  83c404                add       esp,4
  004a42e0  a802                  test      al,2
  004a42e2  0f84f3050000          je        near 004A48DBh
  004a42e8  f644242410            test      byte [esp+24h],10h
  004a42ed  740d                  je        short 004A42FCh
  004a42ef  24fd                  and       al,0FDh
  004a42f1  c6464300              mov       byte [esi+43h],0
  004a42f5  8906                  mov       [esi],eax
  004a42f7  e9dc010000            jmp       004A44D8h

LAB_004a42fc:
  004a42fc  33c9                  xor       ecx,ecx
  004a42fe  8a4e42                mov       cl,[esi+42h]
  004a4301  83e900                sub       ecx,0
  004a4304  0f84c1040000          je        near 004A47CBh
  004a430a  49                    dec       ecx
  004a430b  0f85c7010000          jne       near 004A44D8h
  004a4311  668b5610              mov       dx,[esi+10h]
  004a4315  0fbfca                movsx     ecx,dx
  004a4318  83e900                sub       ecx,0
  004a431b  0f8490000000          je        near 004A43B1h
  004a4321  49                    dec       ecx
  004a4322  742f                  je        short 004A4353h
  004a4324  49                    dec       ecx
  004a4325  0f85ad010000          jne       near 004A44D8h
  004a432b  8b465c                mov       eax,[esi+5Ch]
  004a432e  8bc8                  mov       ecx,eax
  004a4330  c1f904                sar       ecx,4
  004a4333  2bc1                  sub       eax,ecx
  004a4335  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004a433a  89465c                mov       [esi+5Ch],eax
  004a433d  7505                  jne       short 004A4344h
  004a433f  4a                    dec       edx
  004a4340  66895610              mov       [esi+10h],dx

LAB_004a4344:
  004a4344  8b442410              mov       eax,[esp+10h]
  004a4348  c1f802                sar       eax,2
  004a434b  83c020                add       eax,20h
  004a434e  e944070000            jmp       004A4A97h

LAB_004a4353:
  004a4353  8b442410              mov       eax,[esp+10h]
  004a4357  0fbf4e32              movsx     ecx,word [esi+32h]
  004a435b  0540040000            add       eax,440h
  004a4360  51                    push      ecx
  004a4361  c1e007                shl       eax,7
  004a4364  89465c                mov       [esi+5Ch],eax
  004a4367  e8b0160a00            call      00545A1Ch
  004a436c  0faf465c              imul      eax,[esi+5Ch]
  004a4370  0fbf5632              movsx     edx,word [esi+32h]
  004a4374  8b4e50                mov       ecx,[esi+50h]
  004a4377  52                    push      edx
  004a4378  c1f80c                sar       eax,0Ch
  004a437b  03c8                  add       ecx,eax
  004a437d  894e50                mov       [esi+50h],ecx
  004a4380  e891160a00            call      00545A16h
  004a4385  0faf465c              imul      eax,[esi+5Ch]
  004a4389  8b5658                mov       edx,[esi+58h]
  004a438c  8b0e                  mov       ecx,[esi]
  004a438e  c1f80c                sar       eax,0Ch
  004a4391  03d0                  add       edx,eax
  004a4393  0bcf                  or        ecx,edi
  004a4395  66ff4e10              dec       word [esi+10h]
  004a4399  895658                mov       [esi+58h],edx
  004a439c  c7465400400200        mov       dword [esi+54h],24000h
  004a43a3  890e                  mov       [esi],ecx
  004a43a5  c7465c00000000        mov       dword [esi+5Ch],0
  004a43ac  e927010000            jmp       004A44D8h

LAB_004a43b1:
  004a43b1  a900000002            test      eax,2000000h
  004a43b6  7408                  je        short 004A43C0h
  004a43b8  a801                  test      al,1
  004a43ba  7504                  jne       short 004A43C0h
  004a43bc  24dd                  and       al,0DDh
  004a43be  8906                  mov       [esi],eax

LAB_004a43c0:
  004a43c0  f60620                test      byte [esi],20h
  004a43c3  0f850f010000          jne       near 004A44D8h
  004a43c9  8d6e1c                lea       ebp,[esi+1Ch]
  004a43cc  6a00                  push      0
  004a43ce  68f4295500            push      5529F4h
  004a43d3  55                    push      ebp
  004a43d4  56                    push      esi
  004a43d5  e856580700            call      00519C30h
  004a43da  8bf8                  mov       edi,eax
  004a43dc  83c410                add       esp,10h
  004a43df  85ff                  test      edi,edi
  004a43e1  7406                  je        short 004A43E9h
  004a43e3  8b06                  mov       eax,[esi]
  004a43e5  0c20                  or        al,20h
  004a43e7  8906                  mov       [esi],eax

LAB_004a43e9:
  004a43e9  a1b8e05d00            mov       eax,[5DE0B8h]
  004a43ee  85c0                  test      eax,eax
  004a43f0  0f84e2000000          je        near 004A44D8h
  004a43f6  8b1f                  mov       ebx,[edi]
  004a43f8  66c787960000001000    mov       word [edi+96h],10h
  004a4401  83cb40                or        ebx,40h
  004a4404  57                    push      edi
  004a4405  891f                  mov       [edi],ebx
  004a4407  8b4c2424              mov       ecx,[esp+24h]
  004a440b  56                    push      esi
  004a440c  668b4130              mov       ax,[ecx+30h]
  004a4410  0fbfd8                movsx     ebx,ax
  004a4413  66d1f8                sar       ax,1
  004a4416  66894130              mov       [ecx+30h],ax
  004a441a  e861570900            call      00539B80h
  004a441f  8b442428              mov       eax,[esp+28h]
  004a4423  83c408                add       esp,8
  004a4426  66895830              mov       [eax+30h],bx
  004a442a  8d5f1c                lea       ebx,[edi+1Ch]
  004a442d  53                    push      ebx
  004a442e  6800180000            push      1800h
  004a4433  6800030000            push      300h
  004a4438  6a03                  push      3
  004a443a  e8e1960900            call      0053DB20h
  004a443f  8b07                  mov       eax,[edi]
  004a4441  83c410                add       esp,10h
  004a4444  a900000040            test      eax,40000000h
  004a4449  0f8585000000          jne       near 004A44D4h
  004a444f  0fbf4e32              movsx     ecx,word [esi+32h]
  004a4453  51                    push      ecx
  004a4454  e8c3150a00            call      00545A1Ch
  004a4459  8d0440                lea       eax,[eax+eax*2]
  004a445c  8d1480                lea       edx,[eax+eax*4]
  004a445f  d1e2                  shl       edx,1
  004a4461  895750                mov       [edi+50h],edx
  004a4464  0fbf4632              movsx     eax,word [esi+32h]
  004a4468  50                    push      eax
  004a4469  e8a8150a00            call      00545A16h
  004a446e  8d0440                lea       eax,[eax+eax*2]
  004a4471  8d0c80                lea       ecx,[eax+eax*4]
  004a4474  8b07                  mov       eax,[edi]
  004a4476  d1e1                  shl       ecx,1
  004a4478  a801                  test      al,1
  004a447a  894f58                mov       [edi+58h],ecx
  004a447d  750b                  jne       short 004A448Ah
  004a447f  0c01                  or        al,1
  004a4481  c7475400800100        mov       dword [edi+54h],18000h
  004a4488  8907                  mov       [edi],eax

LAB_004a448a:
  004a448a  8b5500                mov       edx,[ebp]
  004a448d  8b4620                mov       eax,[esi+20h]
  004a4490  8b4e24                mov       ecx,[esi+24h]
  004a4493  89542428              mov       [esp+28h],edx
  004a4497  8b13                  mov       edx,[ebx]
  004a4499  8944242c              mov       [esp+2Ch],eax
  004a449d  895500                mov       [ebp],edx
  004a44a0  8b4720                mov       eax,[edi+20h]
  004a44a3  6aff                  push      0FFFFFFFFh
  004a44a5  894c2434              mov       [esp+34h],ecx
  004a44a9  894620                mov       [esi+20h],eax
  004a44ac  8b4f24                mov       ecx,[edi+24h]
  004a44af  6a00                  push      0
  004a44b1  6a39                  push      39h
  004a44b3  56                    push      esi
  004a44b4  894e24                mov       [esi+24h],ecx
  004a44b7  e81465fdff            call      0047A9D0h
  004a44bc  8b542438              mov       edx,[esp+38h]
  004a44c0  8b44243c              mov       eax,[esp+3Ch]
  004a44c4  8b4c2440              mov       ecx,[esp+40h]
  004a44c8  83c410                add       esp,10h
  004a44cb  895500                mov       [ebp],edx
  004a44ce  894620                mov       [esi+20h],eax
  004a44d1  894e24                mov       [esi+24h],ecx

LAB_004a44d4:
  004a44d4  8b5c2418              mov       ebx,[esp+18h]

LAB_004a44d8:
  004a44d8  8b44241c              mov       eax,[esp+1Ch]
  004a44dc  66c7869a0000000060    mov       word [esi+9Ah],6000h
  004a44e5  50                    push      eax
  004a44e6  56                    push      esi
  004a44e7  e8743cfeff            call      00488160h
  004a44ec  83c408                add       esp,8
  004a44ef  56                    push      esi
  004a44f0  e84bf40800            call      00533940h
  004a44f5  83c404                add       esp,4
  004a44f8  56                    push      esi
  004a44f9  e8f2b50300            call      004DFAF0h
  004a44fe  8b4c2438              mov       ecx,[esp+38h]
  004a4502  83c404                add       esp,4
  004a4505  51                    push      ecx
  004a4506  56                    push      esi
  004a4507  e864f20300            call      004E3770h
  004a450c  8b442424              mov       eax,[esp+24h]
  004a4510  83c408                add       esp,8
  004a4513  85c0                  test      eax,eax
  004a4515  0f84a8020000          je        near 004A47C3h
  004a451b  f70617080000          test      dword [esi],817h
  004a4521  0f859c020000          jne       near 004A47C3h
  004a4527  8a4643                mov       al,[esi+43h]
  004a452a  84c0                  test      al,al
  004a452c  0f8591020000          jne       near 004A47C3h
  004a4532  8b466c                mov       eax,[esi+6Ch]
  004a4535  8b561c                mov       edx,[esi+1Ch]
  004a4538  8b781c                mov       edi,[eax+1Ch]
  004a453b  8b6824                mov       ebp,[eax+24h]
  004a453e  8b4624                mov       eax,[esi+24h]
  004a4541  2bfa                  sub       edi,edx
  004a4543  2be8                  sub       ebp,eax
  004a4545  c1ff0c                sar       edi,0Ch
  004a4548  c1fd0c                sar       ebp,0Ch
  004a454b  8bd7                  mov       edx,edi
  004a454d  8bc5                  mov       eax,ebp
  004a454f  0fafd7                imul      edx,edi
  004a4552  0fafc5                imul      eax,ebp
  004a4555  03d0                  add       edx,eax
  004a4557  52                    push      edx
  004a4558  e8b3140a00            call      00545A10h
  004a455d  55                    push      ebp
  004a455e  57                    push      edi
  004a455f  8944241c              mov       [esp+1Ch],eax
  004a4563  e8ba140a00            call      00545A22h
  004a4568  668b4e32              mov       cx,[esi+32h]
  004a456c  2bc1                  sub       eax,ecx
  004a456e  25ff0f0000            and       eax,0FFFh
  004a4573  89442440              mov       [esp+40h],eax
  004a4577  8b442414              mov       eax,[esp+14h]
  004a457b  3dfa000000            cmp       eax,0FAh
  004a4580  0f8dfe000000          jge       near 004A4684h
  004a4586  e8b52b0a00            call      00547140h
  004a458b  99                    cdq
  004a458c  b964000000            mov       ecx,64h
  004a4591  f7f9                  idiv      ecx
  004a4593  83fa32                cmp       edx,32h
  004a4596  7d61                  jge       short 004A45F9h
  004a4598  8b542440              mov       edx,[esp+40h]
  004a459c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004a45a3  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004a45aa  25ff0f0000            and       eax,0FFFh
  004a45af  3bc1                  cmp       eax,ecx
  004a45b1  0f8f02010000          jg        near 004A46B9h
  004a45b7  e8842b0a00            call      00547140h
  004a45bc  99                    cdq
  004a45bd  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004a45c1  f7f9                  idiv      ecx
  004a45c3  8b442414              mov       eax,[esp+14h]
  004a45c7  83c264                add       edx,64h
  004a45ca  3bc2                  cmp       eax,edx
  004a45cc  0f8de7000000          jge       near 004A46B9h
  004a45d2  8b16                  mov       edx,[esi]
  004a45d4  c7461803000000        mov       dword [esi+18h],3
  004a45db  81e2fffffffe          and       edx,0FEFFFFFFh
  004a45e1  c6464200              mov       byte [esi+42h],0
  004a45e5  83ca02                or        edx,2
  004a45e8  c7465c00000000        mov       dword [esi+5Ch],0
  004a45ef  8916                  mov       [esi],edx
  004a45f1  5f                    pop       edi
  004a45f2  5e                    pop       esi
  004a45f3  5d                    pop       ebp
  004a45f4  5b                    pop       ebx
  004a45f5  83c42c                add       esp,2Ch
  004a45f8  c3                    ret

LAB_004a45f9:
  004a45f9  83fa4b                cmp       edx,4Bh
  004a45fc  0f8c3f010000          jl        near 004A4741h
  004a4602  83fa5a                cmp       edx,5Ah
  004a4605  7d2d                  jge       short 004A4634h
  004a4607  8b542440              mov       edx,[esp+40h]
  004a460b  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004a4612  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004a4619  25ff0f0000            and       eax,0FFFh
  004a461e  3bc1                  cmp       eax,ecx
  004a4620  7f12                  jg        short 004A4634h
  004a4622  8b16                  mov       edx,[esi]
  004a4624  81e2fffffffe          and       edx,0FEFFFFFFh
  004a462a  83ca02                or        edx,2
  004a462d  8916                  mov       [esi],edx
  004a462f  e9a0000000            jmp       004A46D4h

LAB_004a4634:
  004a4634  c6464203              mov       byte [esi+42h],3
  004a4638  e8032b0a00            call      00547140h
  004a463d  99                    cdq
  004a463e  b90a000000            mov       ecx,0Ah
  004a4643  f7f9                  idiv      ecx
  004a4645  d1fb                  sar       ebx,1
  004a4647  02d3                  add       dl,bl
  004a4649  80c214                add       dl,14h
  004a464c  885643                mov       [esi+43h],dl
  004a464f  8b542410              mov       edx,[esp+10h]
  004a4653  8dba80000000          lea       edi,[edx+80h]
  004a4659  c1e704                shl       edi,4
  004a465c  e8df2a0a00            call      00547140h
  004a4661  99                    cdq
  004a4662  f7ff                  idiv      edi
  004a4664  d1ff                  sar       edi,1
  004a4666  2bd7                  sub       edx,edi
  004a4668  8b7c2440              mov       edi,[esp+40h]
  004a466c  03d7                  add       edx,edi
  004a466e  66035632              add       dx,[esi+32h]
  004a4672  81c200080000          add       edx,800h
  004a4678  66895610              mov       [esi+10h],dx
  004a467c  5f                    pop       edi
  004a467d  5e                    pop       esi
  004a467e  5d                    pop       ebp
  004a467f  5b                    pop       ebx
  004a4680  83c42c                add       esp,2Ch
  004a4683  c3                    ret

LAB_004a4684:
  004a4684  3df4010000            cmp       eax,1F4h
  004a4689  0f8ddb000000          jge       near 004A476Ah
  004a468f  e8ac2a0a00            call      00547140h
  004a4694  99                    cdq
  004a4695  b964000000            mov       ecx,64h
  004a469a  f7f9                  idiv      ecx
  004a469c  83fa2d                cmp       edx,2Dh
  004a469f  7d4c                  jge       short 004A46EDh
  004a46a1  8b542440              mov       edx,[esp+40h]
  004a46a5  81c200040000          add       edx,400h
  004a46ab  81e2ff0f0000          and       edx,0FFFh
  004a46b1  81fa00080000          cmp       edx,800h
  004a46b7  7c10                  jl        short 004A46C9h

LAB_004a46b9:
  004a46b9  c6464201              mov       byte [esi+42h],1
  004a46bd  c6464301              mov       byte [esi+43h],1
  004a46c1  5f                    pop       edi
  004a46c2  5e                    pop       esi
  004a46c3  5d                    pop       ebp
  004a46c4  5b                    pop       ebx
  004a46c5  83c42c                add       esp,2Ch
  004a46c8  c3                    ret

LAB_004a46c9:
  004a46c9  8b06                  mov       eax,[esi]
  004a46cb  25fffffffe            and       eax,0FEFFFFFFh
  004a46d0  0c02                  or        al,2
  004a46d2  8906                  mov       [esi],eax

LAB_004a46d4:
  004a46d4  c7461804000000        mov       dword [esi+18h],4
  004a46db  c6464201              mov       byte [esi+42h],1
  004a46df  66c746100200          mov       word [esi+10h],2
  004a46e5  5f                    pop       edi
  004a46e6  5e                    pop       esi
  004a46e7  5d                    pop       ebp
  004a46e8  5b                    pop       ebx
  004a46e9  83c42c                add       esp,2Ch
  004a46ec  c3                    ret

LAB_004a46ed:
  004a46ed  83fa46                cmp       edx,46h
  004a46f0  7d4a                  jge       short 004A473Ch
  004a46f2  c6464203              mov       byte [esi+42h],3
  004a46f6  e8452a0a00            call      00547140h
  004a46fb  99                    cdq
  004a46fc  b90a000000            mov       ecx,0Ah
  004a4701  f7f9                  idiv      ecx
  004a4703  d1fb                  sar       ebx,1
  004a4705  02d3                  add       dl,bl
  004a4707  80c214                add       dl,14h
  004a470a  885643                mov       [esi+43h],dl
  004a470d  8b542410              mov       edx,[esp+10h]
  004a4711  8dba80000000          lea       edi,[edx+80h]
  004a4717  c1e704                shl       edi,4
  004a471a  e8212a0a00            call      00547140h
  004a471f  99                    cdq
  004a4720  f7ff                  idiv      edi
  004a4722  8b4c2440              mov       ecx,[esp+40h]
  004a4726  d1ff                  sar       edi,1
  004a4728  2bd7                  sub       edx,edi
  004a472a  03d1                  add       edx,ecx
  004a472c  66035632              add       dx,[esi+32h]
  004a4730  66895610              mov       [esi+10h],dx
  004a4734  5f                    pop       edi
  004a4735  5e                    pop       esi
  004a4736  5d                    pop       ebp
  004a4737  5b                    pop       ebx
  004a4738  83c42c                add       esp,2Ch
  004a473b  c3                    ret

LAB_004a473c:
  004a473c  83fa5a                cmp       edx,5Ah
  004a473f  7d3b                  jge       short 004A477Ch

LAB_004a4741:
  004a4741  c6464202              mov       byte [esi+42h],2
  004a4745  e8f6290a00            call      00547140h
  004a474a  99                    cdq
  004a474b  b90a000000            mov       ecx,0Ah
  004a4750  66c746106400          mov       word [esi+10h],64h
  004a4756  f7f9                  idiv      ecx
  004a4758  d1fb                  sar       ebx,1
  004a475a  02d3                  add       dl,bl
  004a475c  80c214                add       dl,14h
  004a475f  885643                mov       [esi+43h],dl
  004a4762  5f                    pop       edi
  004a4763  5e                    pop       esi
  004a4764  5d                    pop       ebp
  004a4765  5b                    pop       ebx
  004a4766  83c42c                add       esp,2Ch
  004a4769  c3                    ret

LAB_004a476a:
  004a476a  e8d1290a00            call      00547140h
  004a476f  99                    cdq
  004a4770  b964000000            mov       ecx,64h
  004a4775  f7f9                  idiv      ecx
  004a4777  83fa32                cmp       edx,32h
  004a477a  7d23                  jge       short 004A479Fh

LAB_004a477c:
  004a477c  c6464200              mov       byte [esi+42h],0
  004a4780  e8bb290a00            call      00547140h
  004a4785  99                    cdq
  004a4786  b90a000000            mov       ecx,0Ah
  004a478b  f7f9                  idiv      ecx
  004a478d  d1fb                  sar       ebx,1
  004a478f  02d3                  add       dl,bl
  004a4791  80c214                add       dl,14h
  004a4794  885643                mov       [esi+43h],dl
  004a4797  5f                    pop       edi
  004a4798  5e                    pop       esi
  004a4799  5d                    pop       ebp
  004a479a  5b                    pop       ebx
  004a479b  83c42c                add       esp,2Ch
  004a479e  c3                    ret

LAB_004a479f:
  004a479f  c6464203              mov       byte [esi+42h],3
  004a47a3  e898290a00            call      00547140h
  004a47a8  99                    cdq
  004a47a9  b90a000000            mov       ecx,0Ah
  004a47ae  f7f9                  idiv      ecx
  004a47b0  d1fb                  sar       ebx,1
  004a47b2  02d3                  add       dl,bl
  004a47b4  80c214                add       dl,14h
  004a47b7  885643                mov       [esi+43h],dl
  004a47ba  e881290a00            call      00547140h
  004a47bf  66894610              mov       [esi+10h],ax

LAB_004a47c3:
  004a47c3  5f                    pop       edi
  004a47c4  5e                    pop       esi
  004a47c5  5d                    pop       ebp
  004a47c6  5b                    pop       ebx
  004a47c7  83c42c                add       esp,2Ch
  004a47ca  c3                    ret

LAB_004a47cb:
  004a47cb  668b561a              mov       dx,[esi+1Ah]
  004a47cf  8b7c2410              mov       edi,[esp+10h]
  004a47d3  6683fa09              cmp       dx,9
  004a47d7  7d39                  jge       short 004A4812h
  004a47d9  8b4c2440              mov       ecx,[esp+40h]
  004a47dd  8bc7                  mov       eax,edi
  004a47df  d1f8                  sar       eax,1
  004a47e1  83c020                add       eax,20h
  004a47e4  8d1c08                lea       ebx,[eax+ecx]
  004a47e7  8d2c00                lea       ebp,[eax+eax]
  004a47ea  81e3ff0f0000          and       ebx,0FFFh
  004a47f0  3bdd                  cmp       ebx,ebp
  004a47f2  7f06                  jg        short 004A47FAh
  004a47f4  66014e32              add       [esi+32h],cx
  004a47f8  eb14                  jmp       short 004A480Eh

LAB_004a47fa:
  004a47fa  81e1ff0f0000          and       ecx,0FFFh
  004a4800  81f900080000          cmp       ecx,800h
  004a4806  7c02                  jl        short 004A480Ah
  004a4808  f7d8                  neg       eax

LAB_004a480a:
  004a480a  66014632              add       [esi+32h],ax

LAB_004a480e:
  004a480e  8b5c2418              mov       ebx,[esp+18h]

LAB_004a4812:
  004a4812  6683fa09              cmp       dx,9
  004a4816  7545                  jne       short 004A485Dh
  004a4818  0fbf5632              movsx     edx,word [esi+32h]
  004a481c  81c700030000          add       edi,300h
  004a4822  52                    push      edx
  004a4823  c1e707                shl       edi,7
  004a4826  897e5c                mov       [esi+5Ch],edi
  004a4829  e8ee110a00            call      00545A1Ch
  004a482e  0faf465c              imul      eax,[esi+5Ch]
  004a4832  8b6e50                mov       ebp,[esi+50h]
  004a4835  c1f80c                sar       eax,0Ch
  004a4838  03e8                  add       ebp,eax
  004a483a  0fbf4632              movsx     eax,word [esi+32h]
  004a483e  50                    push      eax
  004a483f  896e50                mov       [esi+50h],ebp
  004a4842  e8cf110a00            call      00545A16h
  004a4847  0faf465c              imul      eax,[esi+5Ch]
  004a484b  8b4e58                mov       ecx,[esi+58h]
  004a484e  c7465c00000000        mov       dword [esi+5Ch],0
  004a4855  c1f80c                sar       eax,0Ch
  004a4858  03c8                  add       ecx,eax
  004a485a  894e58                mov       [esi+58h],ecx

LAB_004a485d:
  004a485d  668b461a              mov       ax,[esi+1Ah]
  004a4861  663d0a00              cmp       ax,0Ah
  004a4865  7c57                  jl        short 004A48BEh
  004a4867  663d0d00              cmp       ax,0Dh
  004a486b  7f51                  jg        short 004A48BEh
  004a486d  6a00                  push      0
  004a486f  8d4e1c                lea       ecx,[esi+1Ch]
  004a4872  68e8295500            push      5529E8h
  004a4877  51                    push      ecx
  004a4878  56                    push      esi
  004a4879  e8b2530700            call      00519C30h
  004a487e  8bf8                  mov       edi,eax
  004a4880  a1b8e05d00            mov       eax,[5DE0B8h]
  004a4885  83c410                add       esp,10h
  004a4888  85c0                  test      eax,eax
  004a488a  7432                  je        short 004A48BEh
  004a488c  8b2f                  mov       ebp,[edi]
  004a488e  57                    push      edi
  004a488f  83cd40                or        ebp,40h
  004a4892  56                    push      esi
  004a4893  66c787960000001000    mov       word [edi+96h],10h
  004a489c  892f                  mov       [edi],ebp
  004a489e  e8dd520900            call      00539B80h
  004a48a3  83c408                add       esp,8
  004a48a6  83c71c                add       edi,1Ch
  004a48a9  57                    push      edi
  004a48aa  6800180000            push      1800h
  004a48af  6800030000            push      300h
  004a48b4  6a03                  push      3
  004a48b6  e865920900            call      0053DB20h
  004a48bb  83c410                add       esp,10h

LAB_004a48be:
  004a48be  8b06                  mov       eax,[esi]
  004a48c0  a900000002            test      eax,2000000h
  004a48c5  0f84c8000000          je        near 004A4993h
  004a48cb  24fd                  and       al,0FDh
  004a48cd  c7465c00000000        mov       dword [esi+5Ch],0
  004a48d4  8906                  mov       [esi],eax
  004a48d6  e9fdfbffff            jmp       004A44D8h

LAB_004a48db:
  004a48db  a811                  test      al,11h
  004a48dd  0f85f5fbffff          jne       near 004A44D8h
  004a48e3  a904080000            test      eax,804h
  004a48e8  0f85eafbffff          jne       near 004A44D8h
  004a48ee  33c9                  xor       ecx,ecx
  004a48f0  8a4e42                mov       cl,[esi+42h]
  004a48f3  83f903                cmp       ecx,3
  004a48f6  0f87dcfbffff          ja        near 004A44D8h
  004a48fc  ff248dd04a4a00        jmp       dword [ecx*4+4A4AD0h]
  004a4903  33c9                  xor       ecx,ecx
  004a4905  66394e18              cmp       [esi+18h],cx
  004a4909  740a                  je        short 004A4915h
  004a490b  0d00000001            or        eax,1000000h
  004a4910  894e18                mov       [esi+18h],ecx
  004a4913  8906                  mov       [esi],eax

LAB_004a4915:
  004a4915  817c24142c010000      cmp       dword [esp+14h],12Ch
  004a491d  7d09                  jge       short 004A4928h
  004a491f  c6464300              mov       byte [esi+43h],0
  004a4923  e9b0fbffff            jmp       004A44D8h

LAB_004a4928:
  004a4928  8a4643                mov       al,[esi+43h]
  004a492b  84c0                  test      al,al
  004a492d  7405                  je        short 004A4934h
  004a492f  fec8                  dec       al
  004a4931  884643                mov       [esi+43h],al

LAB_004a4934:
  004a4934  894e5c                mov       [esi+5Ch],ecx
  004a4937  e99cfbffff            jmp       004A44D8h
  004a493c  66397e18              cmp       [esi+18h],di
  004a4940  740a                  je        short 004A494Ch
  004a4942  0d00000001            or        eax,1000000h
  004a4947  897e18                mov       [esi+18h],edi
  004a494a  8906                  mov       [esi],eax

LAB_004a494c:
  004a494c  8b542410              mov       edx,[esp+10h]
  004a4950  8b4c2440              mov       ecx,[esp+40h]
  004a4954  8d841280000000        lea       eax,[edx+edx+80h]
  004a495b  8d1408                lea       edx,[eax+ecx]
  004a495e  8d3c00                lea       edi,[eax+eax]
  004a4961  81e2ff0f0000          and       edx,0FFFh
  004a4967  3bd7                  cmp       edx,edi
  004a4969  7f14                  jg        short 004A497Fh
  004a496b  66014e32              add       [esi+32h],cx
  004a496f  c6464300              mov       byte [esi+43h],0
  004a4973  c7465c00000000        mov       dword [esi+5Ch],0
  004a497a  e959fbffff            jmp       004A44D8h

LAB_004a497f:
  004a497f  81e1ff0f0000          and       ecx,0FFFh
  004a4985  81f900080000          cmp       ecx,800h
  004a498b  7c02                  jl        short 004A498Fh
  004a498d  f7d8                  neg       eax

LAB_004a498f:
  004a498f  66014632              add       [esi+32h],ax

LAB_004a4993:
  004a4993  c7465c00000000        mov       dword [esi+5Ch],0
  004a499a  e939fbffff            jmp       004A44D8h
  004a499f  66397e18              cmp       [esi+18h],di
  004a49a3  740a                  je        short 004A49AFh
  004a49a5  0d00000001            or        eax,1000000h
  004a49aa  897e18                mov       [esi+18h],edi
  004a49ad  8906                  mov       [esi],eax

LAB_004a49af:
  004a49af  0fbf4610              movsx     eax,word [esi+10h]
  004a49b3  39442414              cmp       [esp+14h],eax
  004a49b7  7d09                  jge       short 004A49C2h
  004a49b9  c6464300              mov       byte [esi+43h],0
  004a49bd  e916fbffff            jmp       004A44D8h

LAB_004a49c2:
  004a49c2  8a4643                mov       al,[esi+43h]
  004a49c5  84c0                  test      al,al
  004a49c7  7405                  je        short 004A49CEh
  004a49c9  fec8                  dec       al
  004a49cb  884643                mov       [esi+43h],al

LAB_004a49ce:
  004a49ce  8b542410              mov       edx,[esp+10h]
  004a49d2  8b4c2440              mov       ecx,[esp+40h]
  004a49d6  8d841280000000        lea       eax,[edx+edx+80h]
  004a49dd  8d3c08                lea       edi,[eax+ecx]
  004a49e0  8d2c00                lea       ebp,[eax+eax]
  004a49e3  81e7ff0f0000          and       edi,0FFFh
  004a49e9  3bfd                  cmp       edi,ebp
  004a49eb  7f15                  jg        short 004A4A02h
  004a49ed  66014e32              add       [esi+32h],cx
  004a49f1  81c2c0000000          add       edx,0C0h
  004a49f7  c1e207                shl       edx,7
  004a49fa  89565c                mov       [esi+5Ch],edx
  004a49fd  e9d6faffff            jmp       004A44D8h

LAB_004a4a02:
  004a4a02  81e1ff0f0000          and       ecx,0FFFh
  004a4a08  81f900080000          cmp       ecx,800h
  004a4a0e  7c02                  jl        short 004A4A12h
  004a4a10  f7d8                  neg       eax

LAB_004a4a12:
  004a4a12  66014632              add       [esi+32h],ax
  004a4a16  81c2c0000000          add       edx,0C0h
  004a4a1c  c1e207                shl       edx,7
  004a4a1f  89565c                mov       [esi+5Ch],edx
  004a4a22  e9b1faffff            jmp       004A44D8h
  004a4a27  8a4e43                mov       cl,[esi+43h]
  004a4a2a  84c9                  test      cl,cl
  004a4a2c  7405                  je        short 004A4A33h
  004a4a2e  fec9                  dec       cl
  004a4a30  884e43                mov       [esi+43h],cl

LAB_004a4a33:
  004a4a33  66397e18              cmp       [esi+18h],di
  004a4a37  740a                  je        short 004A4A43h
  004a4a39  0d00000001            or        eax,1000000h
  004a4a3e  897e18                mov       [esi+18h],edi
  004a4a41  8906                  mov       [esi],eax

LAB_004a4a43:
  004a4a43  8b4c2410              mov       ecx,[esp+10h]
  004a4a47  0fbf5610              movsx     edx,word [esi+10h]
  004a4a4b  81c180000000          add       ecx,80h
  004a4a51  52                    push      edx
  004a4a52  c1e107                shl       ecx,7
  004a4a55  894e5c                mov       [esi+5Ch],ecx
  004a4a58  e8bf0f0a00            call      00545A1Ch
  004a4a5d  0faf465c              imul      eax,[esi+5Ch]
  004a4a61  8b7e1c                mov       edi,[esi+1Ch]
  004a4a64  c1f80c                sar       eax,0Ch
  004a4a67  03f8                  add       edi,eax
  004a4a69  0fbf4610              movsx     eax,word [esi+10h]
  004a4a6d  50                    push      eax
  004a4a6e  897e1c                mov       [esi+1Ch],edi
  004a4a71  e8a00f0a00            call      00545A16h
  004a4a76  0faf465c              imul      eax,[esi+5Ch]
  004a4a7a  8b6e24                mov       ebp,[esi+24h]
  004a4a7d  8b4c2410              mov       ecx,[esp+10h]
  004a4a81  c1f80c                sar       eax,0Ch
  004a4a84  03e8                  add       ebp,eax
  004a4a86  c7465c00000000        mov       dword [esi+5Ch],0
  004a4a8d  896e24                mov       [esi+24h],ebp
  004a4a90  8d048d32000000        lea       eax,[ecx*4+32h]

LAB_004a4a97:
  004a4a97  8b4c2440              mov       ecx,[esp+40h]
  004a4a9b  8d3c00                lea       edi,[eax+eax]
  004a4a9e  8d1408                lea       edx,[eax+ecx]
  004a4aa1  81e2ff0f0000          and       edx,0FFFh
  004a4aa7  3bd7                  cmp       edx,edi
  004a4aa9  7f09                  jg        short 004A4AB4h
  004a4aab  66014e32              add       [esi+32h],cx
  004a4aaf  e924faffff            jmp       004A44D8h

LAB_004a4ab4:
  004a4ab4  81e1ff0f0000          and       ecx,0FFFh
  004a4aba  81f900080000          cmp       ecx,800h
  004a4ac0  7c02                  jl        short 004A4AC4h
  004a4ac2  f7d8                  neg       eax

LAB_004a4ac4:
  004a4ac4  66014632              add       [esi+32h],ax
  004a4ac8  e90bfaffff            jmp       004A44D8h

47 cible(s) de saut interne : 0x004a42fc, 0x004a4344, 0x004a4353, 0x004a43b1, 0x004a43c0, 0x004a43e9, 0x004a448a, 0x004a44d4, 0x004a44d8, 0x004a45f9, 0x004a4634, 0x004a4684, 0x004a46b9, 0x004a46c9, 0x004a46d4, 0x004a46ed, 0x004a473c, 0x004a4741, 0x004a476a, 0x004a477c, 0x004a479f, 0x004a47c3, 0x004a47cb, 0x004a47fa, 0x004a480a, 0x004a480e, 0x004a4812, 0x004a485d, 0x004a48be, 0x004a48db, 0x004a4915, 0x004a4928, 0x004a4934, 0x004a494c, 0x004a497f, 0x004a498f, 0x004a4993, 0x004a49af, 0x004a49c2, 0x004a49ce, 0x004a4a02, 0x004a4a12, 0x004a4a33, 0x004a4a43, 0x004a4a97, 0x004a4ab4, 0x004a4ac4
