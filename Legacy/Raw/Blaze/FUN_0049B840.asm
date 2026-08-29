C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049b840  section .text  offset fichier 0x9ac40  2160 octets

  0049b840  83ec2c                sub       esp,2Ch
  0049b843  53                    push      ebx
  0049b844  55                    push      ebp
  0049b845  56                    push      esi
  0049b846  8b74243c              mov       esi,[esp+3Ch]
  0049b84a  8d44241c              lea       eax,[esp+1Ch]
  0049b84e  57                    push      edi
  0049b84f  8d4c2438              lea       ecx,[esp+38h]
  0049b853  50                    push      eax
  0049b854  8d542438              lea       edx,[esp+38h]
  0049b858  51                    push      ecx
  0049b859  52                    push      edx
  0049b85a  56                    push      esi
  0049b85b  e820d10900            call      00538980h
  0049b860  83c410                add       esp,10h
  0049b863  8d442424              lea       eax,[esp+24h]
  0049b867  8d4c241c              lea       ecx,[esp+1Ch]
  0049b86b  8d542440              lea       edx,[esp+40h]
  0049b86f  50                    push      eax
  0049b870  51                    push      ecx
  0049b871  8d44241c              lea       eax,[esp+1Ch]
  0049b875  52                    push      edx
  0049b876  8d4c241c              lea       ecx,[esp+1Ch]
  0049b87a  50                    push      eax
  0049b87b  51                    push      ecx
  0049b87c  56                    push      esi
  0049b87d  e89ea60900            call      00535F20h
  0049b882  8b4c2428              mov       ecx,[esp+28h]
  0049b886  8b442434              mov       eax,[esp+34h]
  0049b88a  bb40000000            mov       ebx,40h
  0049b88f  83c418                add       esp,18h
  0049b892  2bd9                  sub       ebx,ecx
  0049b894  bf01000000            mov       edi,1
  0049b899  85c0                  test      eax,eax
  0049b89b  895c2418              mov       [esp+18h],ebx
  0049b89f  0f8403020000          je        near 0049BAA8h
  0049b8a5  56                    push      esi
  0049b8a6  e8f5fc0300            call      004DB5A0h
  0049b8ab  8b06                  mov       eax,[esi]
  0049b8ad  83c404                add       esp,4
  0049b8b0  a802                  test      al,2
  0049b8b2  0f84f3050000          je        near 0049BEABh
  0049b8b8  f644242410            test      byte [esp+24h],10h
  0049b8bd  740d                  je        short 0049B8CCh
  0049b8bf  24fd                  and       al,0FDh
  0049b8c1  c6464300              mov       byte [esi+43h],0
  0049b8c5  8906                  mov       [esi],eax
  0049b8c7  e9dc010000            jmp       0049BAA8h

LAB_0049b8cc:
  0049b8cc  33c9                  xor       ecx,ecx
  0049b8ce  8a4e42                mov       cl,[esi+42h]
  0049b8d1  83e900                sub       ecx,0
  0049b8d4  0f84c1040000          je        near 0049BD9Bh
  0049b8da  49                    dec       ecx
  0049b8db  0f85c7010000          jne       near 0049BAA8h
  0049b8e1  668b5610              mov       dx,[esi+10h]
  0049b8e5  0fbfca                movsx     ecx,dx
  0049b8e8  83e900                sub       ecx,0
  0049b8eb  0f8490000000          je        near 0049B981h
  0049b8f1  49                    dec       ecx
  0049b8f2  742f                  je        short 0049B923h
  0049b8f4  49                    dec       ecx
  0049b8f5  0f85ad010000          jne       near 0049BAA8h
  0049b8fb  8b465c                mov       eax,[esi+5Ch]
  0049b8fe  8bc8                  mov       ecx,eax
  0049b900  c1f904                sar       ecx,4
  0049b903  2bc1                  sub       eax,ecx
  0049b905  66837e1a0e            cmp       word [esi+1Ah],0Eh
  0049b90a  89465c                mov       [esi+5Ch],eax
  0049b90d  7505                  jne       short 0049B914h
  0049b90f  4a                    dec       edx
  0049b910  66895610              mov       [esi+10h],dx

LAB_0049b914:
  0049b914  8b442410              mov       eax,[esp+10h]
  0049b918  c1f802                sar       eax,2
  0049b91b  83c020                add       eax,20h
  0049b91e  e944070000            jmp       0049C067h

LAB_0049b923:
  0049b923  8b442410              mov       eax,[esp+10h]
  0049b927  0fbf4e32              movsx     ecx,word [esi+32h]
  0049b92b  0540040000            add       eax,440h
  0049b930  51                    push      ecx
  0049b931  c1e007                shl       eax,7
  0049b934  89465c                mov       [esi+5Ch],eax
  0049b937  e8e0a00a00            call      00545A1Ch
  0049b93c  0faf465c              imul      eax,[esi+5Ch]
  0049b940  0fbf5632              movsx     edx,word [esi+32h]
  0049b944  8b4e50                mov       ecx,[esi+50h]
  0049b947  52                    push      edx
  0049b948  c1f80c                sar       eax,0Ch
  0049b94b  03c8                  add       ecx,eax
  0049b94d  894e50                mov       [esi+50h],ecx
  0049b950  e8c1a00a00            call      00545A16h
  0049b955  0faf465c              imul      eax,[esi+5Ch]
  0049b959  8b5658                mov       edx,[esi+58h]
  0049b95c  8b0e                  mov       ecx,[esi]
  0049b95e  c1f80c                sar       eax,0Ch
  0049b961  03d0                  add       edx,eax
  0049b963  0bcf                  or        ecx,edi
  0049b965  66ff4e10              dec       word [esi+10h]
  0049b969  895658                mov       [esi+58h],edx
  0049b96c  c7465400400200        mov       dword [esi+54h],24000h
  0049b973  890e                  mov       [esi],ecx
  0049b975  c7465c00000000        mov       dword [esi+5Ch],0
  0049b97c  e927010000            jmp       0049BAA8h

LAB_0049b981:
  0049b981  a900000002            test      eax,2000000h
  0049b986  7408                  je        short 0049B990h
  0049b988  a801                  test      al,1
  0049b98a  7504                  jne       short 0049B990h
  0049b98c  24dd                  and       al,0DDh
  0049b98e  8906                  mov       [esi],eax

LAB_0049b990:
  0049b990  f60620                test      byte [esi],20h
  0049b993  0f850f010000          jne       near 0049BAA8h
  0049b999  8d6e1c                lea       ebp,[esi+1Ch]
  0049b99c  6a00                  push      0
  0049b99e  686c295500            push      55296Ch
  0049b9a3  55                    push      ebp
  0049b9a4  56                    push      esi
  0049b9a5  e886e20700            call      00519C30h
  0049b9aa  8bf8                  mov       edi,eax
  0049b9ac  83c410                add       esp,10h
  0049b9af  85ff                  test      edi,edi
  0049b9b1  7406                  je        short 0049B9B9h
  0049b9b3  8b06                  mov       eax,[esi]
  0049b9b5  0c20                  or        al,20h
  0049b9b7  8906                  mov       [esi],eax

LAB_0049b9b9:
  0049b9b9  a1b8e05d00            mov       eax,[5DE0B8h]
  0049b9be  85c0                  test      eax,eax
  0049b9c0  0f84e2000000          je        near 0049BAA8h
  0049b9c6  8b1f                  mov       ebx,[edi]
  0049b9c8  66c787960000001000    mov       word [edi+96h],10h
  0049b9d1  83cb40                or        ebx,40h
  0049b9d4  57                    push      edi
  0049b9d5  891f                  mov       [edi],ebx
  0049b9d7  8b4c2424              mov       ecx,[esp+24h]
  0049b9db  56                    push      esi
  0049b9dc  668b4130              mov       ax,[ecx+30h]
  0049b9e0  0fbfd8                movsx     ebx,ax
  0049b9e3  66d1f8                sar       ax,1
  0049b9e6  66894130              mov       [ecx+30h],ax
  0049b9ea  e891e10900            call      00539B80h
  0049b9ef  8b442428              mov       eax,[esp+28h]
  0049b9f3  83c408                add       esp,8
  0049b9f6  66895830              mov       [eax+30h],bx
  0049b9fa  8d5f1c                lea       ebx,[edi+1Ch]
  0049b9fd  53                    push      ebx
  0049b9fe  6800180000            push      1800h
  0049ba03  6800030000            push      300h
  0049ba08  6a03                  push      3
  0049ba0a  e811210a00            call      0053DB20h
  0049ba0f  8b07                  mov       eax,[edi]
  0049ba11  83c410                add       esp,10h
  0049ba14  a900000040            test      eax,40000000h
  0049ba19  0f8585000000          jne       near 0049BAA4h
  0049ba1f  0fbf4e32              movsx     ecx,word [esi+32h]
  0049ba23  51                    push      ecx
  0049ba24  e8f39f0a00            call      00545A1Ch
  0049ba29  8d0440                lea       eax,[eax+eax*2]
  0049ba2c  8d1480                lea       edx,[eax+eax*4]
  0049ba2f  d1e2                  shl       edx,1
  0049ba31  895750                mov       [edi+50h],edx
  0049ba34  0fbf4632              movsx     eax,word [esi+32h]
  0049ba38  50                    push      eax
  0049ba39  e8d89f0a00            call      00545A16h
  0049ba3e  8d0440                lea       eax,[eax+eax*2]
  0049ba41  8d0c80                lea       ecx,[eax+eax*4]
  0049ba44  8b07                  mov       eax,[edi]
  0049ba46  d1e1                  shl       ecx,1
  0049ba48  a801                  test      al,1
  0049ba4a  894f58                mov       [edi+58h],ecx
  0049ba4d  750b                  jne       short 0049BA5Ah
  0049ba4f  0c01                  or        al,1
  0049ba51  c7475400800100        mov       dword [edi+54h],18000h
  0049ba58  8907                  mov       [edi],eax

LAB_0049ba5a:
  0049ba5a  8b5500                mov       edx,[ebp]
  0049ba5d  8b4620                mov       eax,[esi+20h]
  0049ba60  8b4e24                mov       ecx,[esi+24h]
  0049ba63  89542428              mov       [esp+28h],edx
  0049ba67  8b13                  mov       edx,[ebx]
  0049ba69  8944242c              mov       [esp+2Ch],eax
  0049ba6d  895500                mov       [ebp],edx
  0049ba70  8b4720                mov       eax,[edi+20h]
  0049ba73  6aff                  push      0FFFFFFFFh
  0049ba75  894c2434              mov       [esp+34h],ecx
  0049ba79  894620                mov       [esi+20h],eax
  0049ba7c  8b4f24                mov       ecx,[edi+24h]
  0049ba7f  6a00                  push      0
  0049ba81  6a39                  push      39h
  0049ba83  56                    push      esi
  0049ba84  894e24                mov       [esi+24h],ecx
  0049ba87  e844effdff            call      0047A9D0h
  0049ba8c  8b542438              mov       edx,[esp+38h]
  0049ba90  8b44243c              mov       eax,[esp+3Ch]
  0049ba94  8b4c2440              mov       ecx,[esp+40h]
  0049ba98  83c410                add       esp,10h
  0049ba9b  895500                mov       [ebp],edx
  0049ba9e  894620                mov       [esi+20h],eax
  0049baa1  894e24                mov       [esi+24h],ecx

LAB_0049baa4:
  0049baa4  8b5c2418              mov       ebx,[esp+18h]

LAB_0049baa8:
  0049baa8  8b44241c              mov       eax,[esp+1Ch]
  0049baac  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0049bab5  50                    push      eax
  0049bab6  56                    push      esi
  0049bab7  e8a4c6feff            call      00488160h
  0049babc  83c408                add       esp,8
  0049babf  56                    push      esi
  0049bac0  e87b7e0900            call      00533940h
  0049bac5  83c404                add       esp,4
  0049bac8  56                    push      esi
  0049bac9  e822400400            call      004DFAF0h
  0049bace  8b4c2438              mov       ecx,[esp+38h]
  0049bad2  83c404                add       esp,4
  0049bad5  51                    push      ecx
  0049bad6  56                    push      esi
  0049bad7  e8947c0400            call      004E3770h
  0049badc  8b442424              mov       eax,[esp+24h]
  0049bae0  83c408                add       esp,8
  0049bae3  85c0                  test      eax,eax
  0049bae5  0f84a8020000          je        near 0049BD93h
  0049baeb  f70617080000          test      dword [esi],817h
  0049baf1  0f859c020000          jne       near 0049BD93h
  0049baf7  8a4643                mov       al,[esi+43h]
  0049bafa  84c0                  test      al,al
  0049bafc  0f8591020000          jne       near 0049BD93h
  0049bb02  8b466c                mov       eax,[esi+6Ch]
  0049bb05  8b561c                mov       edx,[esi+1Ch]
  0049bb08  8b781c                mov       edi,[eax+1Ch]
  0049bb0b  8b6824                mov       ebp,[eax+24h]
  0049bb0e  8b4624                mov       eax,[esi+24h]
  0049bb11  2bfa                  sub       edi,edx
  0049bb13  2be8                  sub       ebp,eax
  0049bb15  c1ff0c                sar       edi,0Ch
  0049bb18  c1fd0c                sar       ebp,0Ch
  0049bb1b  8bd7                  mov       edx,edi
  0049bb1d  8bc5                  mov       eax,ebp
  0049bb1f  0fafd7                imul      edx,edi
  0049bb22  0fafc5                imul      eax,ebp
  0049bb25  03d0                  add       edx,eax
  0049bb27  52                    push      edx
  0049bb28  e8e39e0a00            call      00545A10h
  0049bb2d  55                    push      ebp
  0049bb2e  57                    push      edi
  0049bb2f  8944241c              mov       [esp+1Ch],eax
  0049bb33  e8ea9e0a00            call      00545A22h
  0049bb38  668b4e32              mov       cx,[esi+32h]
  0049bb3c  2bc1                  sub       eax,ecx
  0049bb3e  25ff0f0000            and       eax,0FFFh
  0049bb43  89442440              mov       [esp+40h],eax
  0049bb47  8b442414              mov       eax,[esp+14h]
  0049bb4b  3dfa000000            cmp       eax,0FAh
  0049bb50  0f8dfe000000          jge       near 0049BC54h
  0049bb56  e8e5b50a00            call      00547140h
  0049bb5b  99                    cdq
  0049bb5c  b964000000            mov       ecx,64h
  0049bb61  f7f9                  idiv      ecx
  0049bb63  83fa32                cmp       edx,32h
  0049bb66  7d61                  jge       short 0049BBC9h
  0049bb68  8b542440              mov       edx,[esp+40h]
  0049bb6c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0049bb73  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0049bb7a  25ff0f0000            and       eax,0FFFh
  0049bb7f  3bc1                  cmp       eax,ecx
  0049bb81  0f8f02010000          jg        near 0049BC89h
  0049bb87  e8b4b50a00            call      00547140h
  0049bb8c  99                    cdq
  0049bb8d  8d4c1b01              lea       ecx,[ebx+ebx+1]
  0049bb91  f7f9                  idiv      ecx
  0049bb93  8b442414              mov       eax,[esp+14h]
  0049bb97  83c264                add       edx,64h
  0049bb9a  3bc2                  cmp       eax,edx
  0049bb9c  0f8de7000000          jge       near 0049BC89h
  0049bba2  8b16                  mov       edx,[esi]
  0049bba4  c7461803000000        mov       dword [esi+18h],3
  0049bbab  81e2fffffffe          and       edx,0FEFFFFFFh
  0049bbb1  c6464200              mov       byte [esi+42h],0
  0049bbb5  83ca02                or        edx,2
  0049bbb8  c7465c00000000        mov       dword [esi+5Ch],0
  0049bbbf  8916                  mov       [esi],edx
  0049bbc1  5f                    pop       edi
  0049bbc2  5e                    pop       esi
  0049bbc3  5d                    pop       ebp
  0049bbc4  5b                    pop       ebx
  0049bbc5  83c42c                add       esp,2Ch
  0049bbc8  c3                    ret

LAB_0049bbc9:
  0049bbc9  83fa4b                cmp       edx,4Bh
  0049bbcc  0f8c3f010000          jl        near 0049BD11h
  0049bbd2  83fa5a                cmp       edx,5Ah
  0049bbd5  7d2d                  jge       short 0049BC04h
  0049bbd7  8b542440              mov       edx,[esp+40h]
  0049bbdb  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0049bbe2  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0049bbe9  25ff0f0000            and       eax,0FFFh
  0049bbee  3bc1                  cmp       eax,ecx
  0049bbf0  7f12                  jg        short 0049BC04h
  0049bbf2  8b16                  mov       edx,[esi]
  0049bbf4  81e2fffffffe          and       edx,0FEFFFFFFh
  0049bbfa  83ca02                or        edx,2
  0049bbfd  8916                  mov       [esi],edx
  0049bbff  e9a0000000            jmp       0049BCA4h

LAB_0049bc04:
  0049bc04  c6464203              mov       byte [esi+42h],3
  0049bc08  e833b50a00            call      00547140h
  0049bc0d  99                    cdq
  0049bc0e  b90a000000            mov       ecx,0Ah
  0049bc13  f7f9                  idiv      ecx
  0049bc15  d1fb                  sar       ebx,1
  0049bc17  02d3                  add       dl,bl
  0049bc19  80c214                add       dl,14h
  0049bc1c  885643                mov       [esi+43h],dl
  0049bc1f  8b542410              mov       edx,[esp+10h]
  0049bc23  8dba80000000          lea       edi,[edx+80h]
  0049bc29  c1e704                shl       edi,4
  0049bc2c  e80fb50a00            call      00547140h
  0049bc31  99                    cdq
  0049bc32  f7ff                  idiv      edi
  0049bc34  d1ff                  sar       edi,1
  0049bc36  2bd7                  sub       edx,edi
  0049bc38  8b7c2440              mov       edi,[esp+40h]
  0049bc3c  03d7                  add       edx,edi
  0049bc3e  66035632              add       dx,[esi+32h]
  0049bc42  81c200080000          add       edx,800h
  0049bc48  66895610              mov       [esi+10h],dx
  0049bc4c  5f                    pop       edi
  0049bc4d  5e                    pop       esi
  0049bc4e  5d                    pop       ebp
  0049bc4f  5b                    pop       ebx
  0049bc50  83c42c                add       esp,2Ch
  0049bc53  c3                    ret

LAB_0049bc54:
  0049bc54  3df4010000            cmp       eax,1F4h
  0049bc59  0f8ddb000000          jge       near 0049BD3Ah
  0049bc5f  e8dcb40a00            call      00547140h
  0049bc64  99                    cdq
  0049bc65  b964000000            mov       ecx,64h
  0049bc6a  f7f9                  idiv      ecx
  0049bc6c  83fa2d                cmp       edx,2Dh
  0049bc6f  7d4c                  jge       short 0049BCBDh
  0049bc71  8b542440              mov       edx,[esp+40h]
  0049bc75  81c200040000          add       edx,400h
  0049bc7b  81e2ff0f0000          and       edx,0FFFh
  0049bc81  81fa00080000          cmp       edx,800h
  0049bc87  7c10                  jl        short 0049BC99h

LAB_0049bc89:
  0049bc89  c6464201              mov       byte [esi+42h],1
  0049bc8d  c6464301              mov       byte [esi+43h],1
  0049bc91  5f                    pop       edi
  0049bc92  5e                    pop       esi
  0049bc93  5d                    pop       ebp
  0049bc94  5b                    pop       ebx
  0049bc95  83c42c                add       esp,2Ch
  0049bc98  c3                    ret

LAB_0049bc99:
  0049bc99  8b06                  mov       eax,[esi]
  0049bc9b  25fffffffe            and       eax,0FEFFFFFFh
  0049bca0  0c02                  or        al,2
  0049bca2  8906                  mov       [esi],eax

LAB_0049bca4:
  0049bca4  c7461804000000        mov       dword [esi+18h],4
  0049bcab  c6464201              mov       byte [esi+42h],1
  0049bcaf  66c746100200          mov       word [esi+10h],2
  0049bcb5  5f                    pop       edi
  0049bcb6  5e                    pop       esi
  0049bcb7  5d                    pop       ebp
  0049bcb8  5b                    pop       ebx
  0049bcb9  83c42c                add       esp,2Ch
  0049bcbc  c3                    ret

LAB_0049bcbd:
  0049bcbd  83fa46                cmp       edx,46h
  0049bcc0  7d4a                  jge       short 0049BD0Ch
  0049bcc2  c6464203              mov       byte [esi+42h],3
  0049bcc6  e875b40a00            call      00547140h
  0049bccb  99                    cdq
  0049bccc  b90a000000            mov       ecx,0Ah
  0049bcd1  f7f9                  idiv      ecx
  0049bcd3  d1fb                  sar       ebx,1
  0049bcd5  02d3                  add       dl,bl
  0049bcd7  80c214                add       dl,14h
  0049bcda  885643                mov       [esi+43h],dl
  0049bcdd  8b542410              mov       edx,[esp+10h]
  0049bce1  8dba80000000          lea       edi,[edx+80h]
  0049bce7  c1e704                shl       edi,4
  0049bcea  e851b40a00            call      00547140h
  0049bcef  99                    cdq
  0049bcf0  f7ff                  idiv      edi
  0049bcf2  8b4c2440              mov       ecx,[esp+40h]
  0049bcf6  d1ff                  sar       edi,1
  0049bcf8  2bd7                  sub       edx,edi
  0049bcfa  03d1                  add       edx,ecx
  0049bcfc  66035632              add       dx,[esi+32h]
  0049bd00  66895610              mov       [esi+10h],dx
  0049bd04  5f                    pop       edi
  0049bd05  5e                    pop       esi
  0049bd06  5d                    pop       ebp
  0049bd07  5b                    pop       ebx
  0049bd08  83c42c                add       esp,2Ch
  0049bd0b  c3                    ret

LAB_0049bd0c:
  0049bd0c  83fa5a                cmp       edx,5Ah
  0049bd0f  7d3b                  jge       short 0049BD4Ch

LAB_0049bd11:
  0049bd11  c6464202              mov       byte [esi+42h],2
  0049bd15  e826b40a00            call      00547140h
  0049bd1a  99                    cdq
  0049bd1b  b90a000000            mov       ecx,0Ah
  0049bd20  66c746106400          mov       word [esi+10h],64h
  0049bd26  f7f9                  idiv      ecx
  0049bd28  d1fb                  sar       ebx,1
  0049bd2a  02d3                  add       dl,bl
  0049bd2c  80c214                add       dl,14h
  0049bd2f  885643                mov       [esi+43h],dl
  0049bd32  5f                    pop       edi
  0049bd33  5e                    pop       esi
  0049bd34  5d                    pop       ebp
  0049bd35  5b                    pop       ebx
  0049bd36  83c42c                add       esp,2Ch
  0049bd39  c3                    ret

LAB_0049bd3a:
  0049bd3a  e801b40a00            call      00547140h
  0049bd3f  99                    cdq
  0049bd40  b964000000            mov       ecx,64h
  0049bd45  f7f9                  idiv      ecx
  0049bd47  83fa32                cmp       edx,32h
  0049bd4a  7d23                  jge       short 0049BD6Fh

LAB_0049bd4c:
  0049bd4c  c6464200              mov       byte [esi+42h],0
  0049bd50  e8ebb30a00            call      00547140h
  0049bd55  99                    cdq
  0049bd56  b90a000000            mov       ecx,0Ah
  0049bd5b  f7f9                  idiv      ecx
  0049bd5d  d1fb                  sar       ebx,1
  0049bd5f  02d3                  add       dl,bl
  0049bd61  80c214                add       dl,14h
  0049bd64  885643                mov       [esi+43h],dl
  0049bd67  5f                    pop       edi
  0049bd68  5e                    pop       esi
  0049bd69  5d                    pop       ebp
  0049bd6a  5b                    pop       ebx
  0049bd6b  83c42c                add       esp,2Ch
  0049bd6e  c3                    ret

LAB_0049bd6f:
  0049bd6f  c6464203              mov       byte [esi+42h],3
  0049bd73  e8c8b30a00            call      00547140h
  0049bd78  99                    cdq
  0049bd79  b90a000000            mov       ecx,0Ah
  0049bd7e  f7f9                  idiv      ecx
  0049bd80  d1fb                  sar       ebx,1
  0049bd82  02d3                  add       dl,bl
  0049bd84  80c214                add       dl,14h
  0049bd87  885643                mov       [esi+43h],dl
  0049bd8a  e8b1b30a00            call      00547140h
  0049bd8f  66894610              mov       [esi+10h],ax

LAB_0049bd93:
  0049bd93  5f                    pop       edi
  0049bd94  5e                    pop       esi
  0049bd95  5d                    pop       ebp
  0049bd96  5b                    pop       ebx
  0049bd97  83c42c                add       esp,2Ch
  0049bd9a  c3                    ret

LAB_0049bd9b:
  0049bd9b  668b561a              mov       dx,[esi+1Ah]
  0049bd9f  8b7c2410              mov       edi,[esp+10h]
  0049bda3  6683fa09              cmp       dx,9
  0049bda7  7d39                  jge       short 0049BDE2h
  0049bda9  8b4c2440              mov       ecx,[esp+40h]
  0049bdad  8bc7                  mov       eax,edi
  0049bdaf  d1f8                  sar       eax,1
  0049bdb1  83c020                add       eax,20h
  0049bdb4  8d1c08                lea       ebx,[eax+ecx]
  0049bdb7  8d2c00                lea       ebp,[eax+eax]
  0049bdba  81e3ff0f0000          and       ebx,0FFFh
  0049bdc0  3bdd                  cmp       ebx,ebp
  0049bdc2  7f06                  jg        short 0049BDCAh
  0049bdc4  66014e32              add       [esi+32h],cx
  0049bdc8  eb14                  jmp       short 0049BDDEh

LAB_0049bdca:
  0049bdca  81e1ff0f0000          and       ecx,0FFFh
  0049bdd0  81f900080000          cmp       ecx,800h
  0049bdd6  7c02                  jl        short 0049BDDAh
  0049bdd8  f7d8                  neg       eax

LAB_0049bdda:
  0049bdda  66014632              add       [esi+32h],ax

LAB_0049bdde:
  0049bdde  8b5c2418              mov       ebx,[esp+18h]

LAB_0049bde2:
  0049bde2  6683fa09              cmp       dx,9
  0049bde6  7545                  jne       short 0049BE2Dh
  0049bde8  0fbf5632              movsx     edx,word [esi+32h]
  0049bdec  81c700030000          add       edi,300h
  0049bdf2  52                    push      edx
  0049bdf3  c1e707                shl       edi,7
  0049bdf6  897e5c                mov       [esi+5Ch],edi
  0049bdf9  e81e9c0a00            call      00545A1Ch
  0049bdfe  0faf465c              imul      eax,[esi+5Ch]
  0049be02  8b6e50                mov       ebp,[esi+50h]
  0049be05  c1f80c                sar       eax,0Ch
  0049be08  03e8                  add       ebp,eax
  0049be0a  0fbf4632              movsx     eax,word [esi+32h]
  0049be0e  50                    push      eax
  0049be0f  896e50                mov       [esi+50h],ebp
  0049be12  e8ff9b0a00            call      00545A16h
  0049be17  0faf465c              imul      eax,[esi+5Ch]
  0049be1b  8b4e58                mov       ecx,[esi+58h]
  0049be1e  c7465c00000000        mov       dword [esi+5Ch],0
  0049be25  c1f80c                sar       eax,0Ch
  0049be28  03c8                  add       ecx,eax
  0049be2a  894e58                mov       [esi+58h],ecx

LAB_0049be2d:
  0049be2d  668b461a              mov       ax,[esi+1Ah]
  0049be31  663d0a00              cmp       ax,0Ah
  0049be35  7c57                  jl        short 0049BE8Eh
  0049be37  663d0d00              cmp       ax,0Dh
  0049be3b  7f51                  jg        short 0049BE8Eh
  0049be3d  6a00                  push      0
  0049be3f  8d4e1c                lea       ecx,[esi+1Ch]
  0049be42  6860295500            push      552960h
  0049be47  51                    push      ecx
  0049be48  56                    push      esi
  0049be49  e8e2dd0700            call      00519C30h
  0049be4e  8bf8                  mov       edi,eax
  0049be50  a1b8e05d00            mov       eax,[5DE0B8h]
  0049be55  83c410                add       esp,10h
  0049be58  85c0                  test      eax,eax
  0049be5a  7432                  je        short 0049BE8Eh
  0049be5c  8b2f                  mov       ebp,[edi]
  0049be5e  57                    push      edi
  0049be5f  83cd40                or        ebp,40h
  0049be62  56                    push      esi
  0049be63  66c787960000001000    mov       word [edi+96h],10h
  0049be6c  892f                  mov       [edi],ebp
  0049be6e  e80ddd0900            call      00539B80h
  0049be73  83c408                add       esp,8
  0049be76  83c71c                add       edi,1Ch
  0049be79  57                    push      edi
  0049be7a  6800180000            push      1800h
  0049be7f  6800030000            push      300h
  0049be84  6a03                  push      3
  0049be86  e8951c0a00            call      0053DB20h
  0049be8b  83c410                add       esp,10h

LAB_0049be8e:
  0049be8e  8b06                  mov       eax,[esi]
  0049be90  a900000002            test      eax,2000000h
  0049be95  0f84c8000000          je        near 0049BF63h
  0049be9b  24fd                  and       al,0FDh
  0049be9d  c7465c00000000        mov       dword [esi+5Ch],0
  0049bea4  8906                  mov       [esi],eax
  0049bea6  e9fdfbffff            jmp       0049BAA8h

LAB_0049beab:
  0049beab  a811                  test      al,11h
  0049bead  0f85f5fbffff          jne       near 0049BAA8h
  0049beb3  a904080000            test      eax,804h
  0049beb8  0f85eafbffff          jne       near 0049BAA8h
  0049bebe  33c9                  xor       ecx,ecx
  0049bec0  8a4e42                mov       cl,[esi+42h]
  0049bec3  83f903                cmp       ecx,3
  0049bec6  0f87dcfbffff          ja        near 0049BAA8h
  0049becc  ff248da0c04900        jmp       dword [ecx*4+49C0A0h]
  0049bed3  33c9                  xor       ecx,ecx
  0049bed5  66394e18              cmp       [esi+18h],cx
  0049bed9  740a                  je        short 0049BEE5h
  0049bedb  0d00000001            or        eax,1000000h
  0049bee0  894e18                mov       [esi+18h],ecx
  0049bee3  8906                  mov       [esi],eax

LAB_0049bee5:
  0049bee5  817c24142c010000      cmp       dword [esp+14h],12Ch
  0049beed  7d09                  jge       short 0049BEF8h
  0049beef  c6464300              mov       byte [esi+43h],0
  0049bef3  e9b0fbffff            jmp       0049BAA8h

LAB_0049bef8:
  0049bef8  8a4643                mov       al,[esi+43h]
  0049befb  84c0                  test      al,al
  0049befd  7405                  je        short 0049BF04h
  0049beff  fec8                  dec       al
  0049bf01  884643                mov       [esi+43h],al

LAB_0049bf04:
  0049bf04  894e5c                mov       [esi+5Ch],ecx
  0049bf07  e99cfbffff            jmp       0049BAA8h
  0049bf0c  66397e18              cmp       [esi+18h],di
  0049bf10  740a                  je        short 0049BF1Ch
  0049bf12  0d00000001            or        eax,1000000h
  0049bf17  897e18                mov       [esi+18h],edi
  0049bf1a  8906                  mov       [esi],eax

LAB_0049bf1c:
  0049bf1c  8b542410              mov       edx,[esp+10h]
  0049bf20  8b4c2440              mov       ecx,[esp+40h]
  0049bf24  8d841280000000        lea       eax,[edx+edx+80h]
  0049bf2b  8d1408                lea       edx,[eax+ecx]
  0049bf2e  8d3c00                lea       edi,[eax+eax]
  0049bf31  81e2ff0f0000          and       edx,0FFFh
  0049bf37  3bd7                  cmp       edx,edi
  0049bf39  7f14                  jg        short 0049BF4Fh
  0049bf3b  66014e32              add       [esi+32h],cx
  0049bf3f  c6464300              mov       byte [esi+43h],0
  0049bf43  c7465c00000000        mov       dword [esi+5Ch],0
  0049bf4a  e959fbffff            jmp       0049BAA8h

LAB_0049bf4f:
  0049bf4f  81e1ff0f0000          and       ecx,0FFFh
  0049bf55  81f900080000          cmp       ecx,800h
  0049bf5b  7c02                  jl        short 0049BF5Fh
  0049bf5d  f7d8                  neg       eax

LAB_0049bf5f:
  0049bf5f  66014632              add       [esi+32h],ax

LAB_0049bf63:
  0049bf63  c7465c00000000        mov       dword [esi+5Ch],0
  0049bf6a  e939fbffff            jmp       0049BAA8h
  0049bf6f  66397e18              cmp       [esi+18h],di
  0049bf73  740a                  je        short 0049BF7Fh
  0049bf75  0d00000001            or        eax,1000000h
  0049bf7a  897e18                mov       [esi+18h],edi
  0049bf7d  8906                  mov       [esi],eax

LAB_0049bf7f:
  0049bf7f  0fbf4610              movsx     eax,word [esi+10h]
  0049bf83  39442414              cmp       [esp+14h],eax
  0049bf87  7d09                  jge       short 0049BF92h
  0049bf89  c6464300              mov       byte [esi+43h],0
  0049bf8d  e916fbffff            jmp       0049BAA8h

LAB_0049bf92:
  0049bf92  8a4643                mov       al,[esi+43h]
  0049bf95  84c0                  test      al,al
  0049bf97  7405                  je        short 0049BF9Eh
  0049bf99  fec8                  dec       al
  0049bf9b  884643                mov       [esi+43h],al

LAB_0049bf9e:
  0049bf9e  8b542410              mov       edx,[esp+10h]
  0049bfa2  8b4c2440              mov       ecx,[esp+40h]
  0049bfa6  8d841280000000        lea       eax,[edx+edx+80h]
  0049bfad  8d3c08                lea       edi,[eax+ecx]
  0049bfb0  8d2c00                lea       ebp,[eax+eax]
  0049bfb3  81e7ff0f0000          and       edi,0FFFh
  0049bfb9  3bfd                  cmp       edi,ebp
  0049bfbb  7f15                  jg        short 0049BFD2h
  0049bfbd  66014e32              add       [esi+32h],cx
  0049bfc1  81c2c0000000          add       edx,0C0h
  0049bfc7  c1e207                shl       edx,7
  0049bfca  89565c                mov       [esi+5Ch],edx
  0049bfcd  e9d6faffff            jmp       0049BAA8h

LAB_0049bfd2:
  0049bfd2  81e1ff0f0000          and       ecx,0FFFh
  0049bfd8  81f900080000          cmp       ecx,800h
  0049bfde  7c02                  jl        short 0049BFE2h
  0049bfe0  f7d8                  neg       eax

LAB_0049bfe2:
  0049bfe2  66014632              add       [esi+32h],ax
  0049bfe6  81c2c0000000          add       edx,0C0h
  0049bfec  c1e207                shl       edx,7
  0049bfef  89565c                mov       [esi+5Ch],edx
  0049bff2  e9b1faffff            jmp       0049BAA8h
  0049bff7  8a4e43                mov       cl,[esi+43h]
  0049bffa  84c9                  test      cl,cl
  0049bffc  7405                  je        short 0049C003h
  0049bffe  fec9                  dec       cl
  0049c000  884e43                mov       [esi+43h],cl

LAB_0049c003:
  0049c003  66397e18              cmp       [esi+18h],di
  0049c007  740a                  je        short 0049C013h
  0049c009  0d00000001            or        eax,1000000h
  0049c00e  897e18                mov       [esi+18h],edi
  0049c011  8906                  mov       [esi],eax

LAB_0049c013:
  0049c013  8b4c2410              mov       ecx,[esp+10h]
  0049c017  0fbf5610              movsx     edx,word [esi+10h]
  0049c01b  81c180000000          add       ecx,80h
  0049c021  52                    push      edx
  0049c022  c1e107                shl       ecx,7
  0049c025  894e5c                mov       [esi+5Ch],ecx
  0049c028  e8ef990a00            call      00545A1Ch
  0049c02d  0faf465c              imul      eax,[esi+5Ch]
  0049c031  8b7e1c                mov       edi,[esi+1Ch]
  0049c034  c1f80c                sar       eax,0Ch
  0049c037  03f8                  add       edi,eax
  0049c039  0fbf4610              movsx     eax,word [esi+10h]
  0049c03d  50                    push      eax
  0049c03e  897e1c                mov       [esi+1Ch],edi
  0049c041  e8d0990a00            call      00545A16h
  0049c046  0faf465c              imul      eax,[esi+5Ch]
  0049c04a  8b6e24                mov       ebp,[esi+24h]
  0049c04d  8b4c2410              mov       ecx,[esp+10h]
  0049c051  c1f80c                sar       eax,0Ch
  0049c054  03e8                  add       ebp,eax
  0049c056  c7465c00000000        mov       dword [esi+5Ch],0
  0049c05d  896e24                mov       [esi+24h],ebp
  0049c060  8d048d32000000        lea       eax,[ecx*4+32h]

LAB_0049c067:
  0049c067  8b4c2440              mov       ecx,[esp+40h]
  0049c06b  8d3c00                lea       edi,[eax+eax]
  0049c06e  8d1408                lea       edx,[eax+ecx]
  0049c071  81e2ff0f0000          and       edx,0FFFh
  0049c077  3bd7                  cmp       edx,edi
  0049c079  7f09                  jg        short 0049C084h
  0049c07b  66014e32              add       [esi+32h],cx
  0049c07f  e924faffff            jmp       0049BAA8h

LAB_0049c084:
  0049c084  81e1ff0f0000          and       ecx,0FFFh
  0049c08a  81f900080000          cmp       ecx,800h
  0049c090  7c02                  jl        short 0049C094h
  0049c092  f7d8                  neg       eax

LAB_0049c094:
  0049c094  66014632              add       [esi+32h],ax
  0049c098  e90bfaffff            jmp       0049BAA8h
  0049c09d  90                    nop
  0049c09e  8bff                  mov       edi,edi
  0049c0a0  d3be49000cbf          sar       dword [esi-40F3FFB7h],cl
  0049c0a6  49                    dec       ecx
  0049c0a7  006fbf                add       [edi-41h],ch
  0049c0aa  49                    dec       ecx
  0049c0ab  00f7                  add       bh,dh
  0049c0ad  bf4900                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
