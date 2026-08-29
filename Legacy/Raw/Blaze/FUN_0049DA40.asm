C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049da40  section .text  offset fichier 0x9ce40  1872 octets

  0049da40  83ec2c                sub       esp,2Ch
  0049da43  53                    push      ebx
  0049da44  55                    push      ebp
  0049da45  56                    push      esi
  0049da46  8b74243c              mov       esi,[esp+3Ch]
  0049da4a  8d442424              lea       eax,[esp+24h]
  0049da4e  57                    push      edi
  0049da4f  8d4c242c              lea       ecx,[esp+2Ch]
  0049da53  50                    push      eax
  0049da54  8d542428              lea       edx,[esp+28h]
  0049da58  51                    push      ecx
  0049da59  33ed                  xor       ebp,ebp
  0049da5b  52                    push      edx
  0049da5c  56                    push      esi
  0049da5d  66896c2440            mov       [esp+40h],bp
  0049da62  66c74424421e00        mov       word [esp+42h],1Eh
  0049da69  66c74424443200        mov       word [esp+44h],32h
  0049da70  66c74424466400        mov       word [esp+46h],64h
  0049da77  66896c2448            mov       [esp+48h],bp
  0049da7c  66c744244a4600        mov       word [esp+4Ah],46h
  0049da83  e8f8ae0900            call      00538980h
  0049da88  83c410                add       esp,10h
  0049da8b  8d442420              lea       eax,[esp+20h]
  0049da8f  8d4c241c              lea       ecx,[esp+1Ch]
  0049da93  8d542410              lea       edx,[esp+10h]
  0049da97  50                    push      eax
  0049da98  51                    push      ecx
  0049da99  8d442420              lea       eax,[esp+20h]
  0049da9d  52                    push      edx
  0049da9e  8d4c244c              lea       ecx,[esp+4Ch]
  0049daa2  50                    push      eax
  0049daa3  51                    push      ecx
  0049daa4  56                    push      esi
  0049daa5  e876840900            call      00535F20h
  0049daaa  8b4c2458              mov       ecx,[esp+58h]
  0049daae  b840000000            mov       eax,40h
  0049dab3  2bc1                  sub       eax,ecx
  0049dab5  83c418                add       esp,18h
  0049dab8  89442414              mov       [esp+14h],eax
  0049dabc  8b44241c              mov       eax,[esp+1Ch]
  0049dac0  3bc5                  cmp       eax,ebp
  0049dac2  bf01000000            mov       edi,1
  0049dac7  0f844f030000          je        near 0049DE1Ch
  0049dacd  56                    push      esi
  0049dace  e8cdda0300            call      004DB5A0h
  0049dad3  8b06                  mov       eax,[esi]
  0049dad5  83c404                add       esp,4
  0049dad8  a802                  test      al,2
  0049dada  0f84cf000000          je        near 0049DBAFh
  0049dae0  8a4c2420              mov       cl,[esp+20h]
  0049dae4  bb10000000            mov       ebx,10h
  0049dae9  84cb                  test      bl,cl
  0049daeb  740d                  je        short 0049DAFAh
  0049daed  24fd                  and       al,0FDh
  0049daef  c6464300              mov       byte [esi+43h],0
  0049daf3  8906                  mov       [esi],eax
  0049daf5  e922030000            jmp       0049DE1Ch

LAB_0049dafa:
  0049dafa  33c9                  xor       ecx,ecx
  0049dafc  8a4e42                mov       cl,[esi+42h]
  0049daff  2bcd                  sub       ecx,ebp
  0049db01  7432                  je        short 0049DB35h
  0049db03  49                    dec       ecx
  0049db04  0f8512030000          jne       near 0049DE1Ch
  0049db0a  66837e1a07            cmp       word [esi+1Ah],7
  0049db0f  750e                  jne       short 0049DB1Fh
  0049db11  6aff                  push      0FFFFFFFFh
  0049db13  55                    push      ebp
  0049db14  6a49                  push      49h
  0049db16  56                    push      esi
  0049db17  e8b4cefdff            call      0047A9D0h
  0049db1c  83c410                add       esp,10h

LAB_0049db1f:
  0049db1f  8b06                  mov       eax,[esi]
  0049db21  a900000002            test      eax,2000000h
  0049db26  0f84f0020000          je        near 0049DE1Ch
  0049db2c  24fd                  and       al,0FDh
  0049db2e  8906                  mov       [esi],eax
  0049db30  e9e7020000            jmp       0049DE1Ch

LAB_0049db35:
  0049db35  66837e1a09            cmp       word [esi+1Ah],9
  0049db3a  7557                  jne       short 0049DB93h
  0049db3c  8d542430              lea       edx,[esp+30h]
  0049db40  55                    push      ebp
  0049db41  8d461c                lea       eax,[esi+1Ch]
  0049db44  52                    push      edx
  0049db45  50                    push      eax
  0049db46  56                    push      esi
  0049db47  e8e4c00700            call      00519C30h
  0049db4c  8bf8                  mov       edi,eax
  0049db4e  a1b8e05d00            mov       eax,[5DE0B8h]
  0049db53  83c410                add       esp,10h
  0049db56  3bc5                  cmp       eax,ebp
  0049db58  742f                  je        short 0049DB89h
  0049db5a  57                    push      edi
  0049db5b  56                    push      esi
  0049db5c  e81fc00900            call      00539B80h
  0049db61  8b07                  mov       eax,[edi]
  0049db63  83c408                add       esp,8
  0049db66  0c40                  or        al,40h
  0049db68  66899f96000000        mov       [edi+96h],bx
  0049db6f  8907                  mov       [edi],eax
  0049db71  83c71c                add       edi,1Ch
  0049db74  57                    push      edi
  0049db75  6800180000            push      1800h
  0049db7a  6800030000            push      300h
  0049db7f  6a03                  push      3
  0049db81  e89aff0900            call      0053DB20h
  0049db86  83c410                add       esp,10h

LAB_0049db89:
  0049db89  8b06                  mov       eax,[esi]
  0049db8b  a801                  test      al,1
  0049db8d  740c                  je        short 0049DB9Bh
  0049db8f  66ff4e1a              dec       word [esi+1Ah]

LAB_0049db93:
  0049db93  a801                  test      al,1
  0049db95  0f8581020000          jne       near 0049DE1Ch

LAB_0049db9b:
  0049db9b  a900000002            test      eax,2000000h
  0049dba0  0f8473020000          je        near 0049DE19h
  0049dba6  24fd                  and       al,0FDh
  0049dba8  8906                  mov       [esi],eax
  0049dbaa  e96a020000            jmp       0049DE19h

LAB_0049dbaf:
  0049dbaf  a810                  test      al,10h
  0049dbb1  0f8565020000          jne       near 0049DE1Ch
  0049dbb7  a904080000            test      eax,804h
  0049dbbc  0f855a020000          jne       near 0049DE1Ch
  0049dbc2  a801                  test      al,1
  0049dbc4  7439                  je        short 0049DBFFh
  0049dbc6  e875950a00            call      00547140h
  0049dbcb  99                    cdq
  0049dbcc  b964000000            mov       ecx,64h
  0049dbd1  f7f9                  idiv      ecx
  0049dbd3  8b442440              mov       eax,[esp+40h]
  0049dbd7  c1f802                sar       eax,2
  0049dbda  3bd0                  cmp       edx,eax
  0049dbdc  0f8d3a020000          jge       near 0049DE1Ch
  0049dbe2  8b0e                  mov       ecx,[esi]
  0049dbe4  c7461803000000        mov       dword [esi+18h],3
  0049dbeb  81e1fffffffe          and       ecx,0FEFFFFFFh
  0049dbf1  c6464200              mov       byte [esi+42h],0
  0049dbf5  83c902                or        ecx,2
  0049dbf8  890e                  mov       [esi],ecx
  0049dbfa  e91a020000            jmp       0049DE19h

LAB_0049dbff:
  0049dbff  33c9                  xor       ecx,ecx
  0049dc01  8a4e42                mov       cl,[esi+42h]
  0049dc04  83f904                cmp       ecx,4
  0049dc07  0f870f020000          ja        near 0049DE1Ch
  0049dc0d  ff248d74e14900        jmp       dword [ecx*4+49E174h]
  0049dc14  66396e18              cmp       [esi+18h],bp
  0049dc18  740a                  je        short 0049DC24h
  0049dc1a  0d00000001            or        eax,1000000h
  0049dc1f  896e18                mov       [esi+18h],ebp
  0049dc22  8906                  mov       [esi],eax

LAB_0049dc24:
  0049dc24  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049dc2c  7d09                  jge       short 0049DC37h
  0049dc2e  c6464300              mov       byte [esi+43h],0
  0049dc32  e9e5010000            jmp       0049DE1Ch

LAB_0049dc37:
  0049dc37  8a4643                mov       al,[esi+43h]
  0049dc3a  84c0                  test      al,al
  0049dc3c  0f84d7010000          je        near 0049DE19h
  0049dc42  fec8                  dec       al
  0049dc44  884643                mov       [esi+43h],al
  0049dc47  e9cd010000            jmp       0049DE19h
  0049dc4c  66397e18              cmp       [esi+18h],di
  0049dc50  740a                  je        short 0049DC5Ch
  0049dc52  0d00000001            or        eax,1000000h
  0049dc57  897e18                mov       [esi+18h],edi
  0049dc5a  8906                  mov       [esi],eax

LAB_0049dc5c:
  0049dc5c  8b542440              mov       edx,[esp+40h]
  0049dc60  8b4c2410              mov       ecx,[esp+10h]
  0049dc64  8d841280000000        lea       eax,[edx+edx+80h]
  0049dc6b  8d1408                lea       edx,[eax+ecx]
  0049dc6e  8d3c00                lea       edi,[eax+eax]
  0049dc71  81e2ff0f0000          and       edx,0FFFh
  0049dc77  3bd7                  cmp       edx,edi
  0049dc79  0f8f20010000          jg        near 0049DD9Fh
  0049dc7f  66014e32              add       [esi+32h],cx
  0049dc83  c6464300              mov       byte [esi+43h],0
  0049dc87  e98d010000            jmp       0049DE19h
  0049dc8c  66397e18              cmp       [esi+18h],di
  0049dc90  740a                  je        short 0049DC9Ch
  0049dc92  0d00000001            or        eax,1000000h
  0049dc97  897e18                mov       [esi+18h],edi
  0049dc9a  8906                  mov       [esi],eax

LAB_0049dc9c:
  0049dc9c  0fbf4610              movsx     eax,word [esi+10h]
  0049dca0  39442418              cmp       [esp+18h],eax
  0049dca4  7d09                  jge       short 0049DCAFh
  0049dca6  c6464300              mov       byte [esi+43h],0
  0049dcaa  e96d010000            jmp       0049DE1Ch

LAB_0049dcaf:
  0049dcaf  8a4643                mov       al,[esi+43h]
  0049dcb2  84c0                  test      al,al
  0049dcb4  7405                  je        short 0049DCBBh
  0049dcb6  fec8                  dec       al
  0049dcb8  884643                mov       [esi+43h],al

LAB_0049dcbb:
  0049dcbb  8b542440              mov       edx,[esp+40h]
  0049dcbf  8b4c2410              mov       ecx,[esp+10h]
  0049dcc3  8d841280000000        lea       eax,[edx+edx+80h]
  0049dcca  8d3c08                lea       edi,[eax+ecx]
  0049dccd  8d1c00                lea       ebx,[eax+eax]
  0049dcd0  81e7ff0f0000          and       edi,0FFFh
  0049dcd6  3bfb                  cmp       edi,ebx
  0049dcd8  7f15                  jg        short 0049DCEFh
  0049dcda  66014e32              add       [esi+32h],cx
  0049dcde  81c2c0000000          add       edx,0C0h
  0049dce4  c1e207                shl       edx,7
  0049dce7  89565c                mov       [esi+5Ch],edx
  0049dcea  e92d010000            jmp       0049DE1Ch

LAB_0049dcef:
  0049dcef  81e1ff0f0000          and       ecx,0FFFh
  0049dcf5  81f900080000          cmp       ecx,800h
  0049dcfb  7c02                  jl        short 0049DCFFh
  0049dcfd  f7d8                  neg       eax

LAB_0049dcff:
  0049dcff  66014632              add       [esi+32h],ax
  0049dd03  81c2c0000000          add       edx,0C0h
  0049dd09  c1e207                shl       edx,7
  0049dd0c  89565c                mov       [esi+5Ch],edx
  0049dd0f  e908010000            jmp       0049DE1Ch
  0049dd14  8a4e43                mov       cl,[esi+43h]
  0049dd17  84c9                  test      cl,cl
  0049dd19  7405                  je        short 0049DD20h
  0049dd1b  fec9                  dec       cl
  0049dd1d  884e43                mov       [esi+43h],cl

LAB_0049dd20:
  0049dd20  66397e18              cmp       [esi+18h],di
  0049dd24  740a                  je        short 0049DD30h
  0049dd26  0d00000001            or        eax,1000000h
  0049dd2b  897e18                mov       [esi+18h],edi
  0049dd2e  8906                  mov       [esi],eax

LAB_0049dd30:
  0049dd30  8b4c2440              mov       ecx,[esp+40h]
  0049dd34  8b7c241c              mov       edi,[esp+1Ch]
  0049dd38  0fbf5610              movsx     edx,word [esi+10h]
  0049dd3c  8d8180000000          lea       eax,[ecx+80h]
  0049dd42  52                    push      edx
  0049dd43  c1e007                shl       eax,7
  0049dd46  0faff8                imul      edi,eax
  0049dd49  89465c                mov       [esi+5Ch],eax
  0049dd4c  c1ff0c                sar       edi,0Ch
  0049dd4f  e8c87c0a00            call      00545A1Ch
  0049dd54  0fafc7                imul      eax,edi
  0049dd57  8b5e1c                mov       ebx,[esi+1Ch]
  0049dd5a  c1f80c                sar       eax,0Ch
  0049dd5d  03d8                  add       ebx,eax
  0049dd5f  0fbf4610              movsx     eax,word [esi+10h]
  0049dd63  50                    push      eax
  0049dd64  895e1c                mov       [esi+1Ch],ebx
  0049dd67  e8aa7c0a00            call      00545A16h
  0049dd6c  0fafc7                imul      eax,edi
  0049dd6f  8b5e24                mov       ebx,[esi+24h]
  0049dd72  8b4c2440              mov       ecx,[esp+40h]
  0049dd76  c1f80c                sar       eax,0Ch
  0049dd79  03d8                  add       ebx,eax
  0049dd7b  8d048d32000000        lea       eax,[ecx*4+32h]
  0049dd82  8b4c2410              mov       ecx,[esp+10h]
  0049dd86  895e24                mov       [esi+24h],ebx
  0049dd89  8d3c00                lea       edi,[eax+eax]
  0049dd8c  8d1408                lea       edx,[eax+ecx]
  0049dd8f  81e2ff0f0000          and       edx,0FFFh
  0049dd95  3bd7                  cmp       edx,edi
  0049dd97  7f06                  jg        short 0049DD9Fh
  0049dd99  66014e32              add       [esi+32h],cx
  0049dd9d  eb7a                  jmp       short 0049DE19h

LAB_0049dd9f:
  0049dd9f  81e1ff0f0000          and       ecx,0FFFh
  0049dda5  81f900080000          cmp       ecx,800h
  0049ddab  7c02                  jl        short 0049DDAFh
  0049ddad  f7d8                  neg       eax

LAB_0049ddaf:
  0049ddaf  66014632              add       [esi+32h],ax
  0049ddb3  eb64                  jmp       short 0049DE19h
  0049ddb5  25fffffffe            and       eax,0FEFFFFFFh
  0049ddba  c7461804000000        mov       dword [esi+18h],4
  0049ddc1  0bc7                  or        eax,edi
  0049ddc3  8b7c241c              mov       edi,[esp+1Ch]
  0049ddc7  8906                  mov       [esi],eax
  0049ddc9  8b442440              mov       eax,[esp+40h]
  0049ddcd  05c0000000            add       eax,0C0h
  0049ddd2  c1e007                shl       eax,7
  0049ddd5  0fbf4e10              movsx     ecx,word [esi+10h]
  0049ddd9  0faff8                imul      edi,eax
  0049dddc  51                    push      ecx
  0049dddd  89465c                mov       [esi+5Ch],eax
  0049dde0  c1ff0c                sar       edi,0Ch
  0049dde3  e8347c0a00            call      00545A1Ch
  0049dde8  0fafc7                imul      eax,edi
  0049ddeb  0fbf5610              movsx     edx,word [esi+10h]
  0049ddef  8b4e50                mov       ecx,[esi+50h]
  0049ddf2  52                    push      edx
  0049ddf3  c1f80c                sar       eax,0Ch
  0049ddf6  03c8                  add       ecx,eax
  0049ddf8  894e50                mov       [esi+50h],ecx
  0049ddfb  e8167c0a00            call      00545A16h
  0049de00  0fafc7                imul      eax,edi
  0049de03  8b4e58                mov       ecx,[esi+58h]
  0049de06  c1f80c                sar       eax,0Ch
  0049de09  03c8                  add       ecx,eax
  0049de0b  8b4654                mov       eax,[esi+54h]
  0049de0e  0500900100            add       eax,19000h
  0049de13  894e58                mov       [esi+58h],ecx
  0049de16  894654                mov       [esi+54h],eax

LAB_0049de19:
  0049de19  896e5c                mov       [esi+5Ch],ebp

LAB_0049de1c:
  0049de1c  8b44241c              mov       eax,[esp+1Ch]
  0049de20  50                    push      eax
  0049de21  56                    push      esi
  0049de22  e839a3feff            call      00488160h
  0049de27  83c408                add       esp,8
  0049de2a  56                    push      esi
  0049de2b  e8105b0900            call      00533940h
  0049de30  83c404                add       esp,4
  0049de33  56                    push      esi
  0049de34  e8b71c0400            call      004DFAF0h
  0049de39  8b4c2428              mov       ecx,[esp+28h]
  0049de3d  83c404                add       esp,4
  0049de40  51                    push      ecx
  0049de41  56                    push      esi
  0049de42  e829590400            call      004E3770h
  0049de47  8b442424              mov       eax,[esp+24h]
  0049de4b  83c408                add       esp,8
  0049de4e  3bc5                  cmp       eax,ebp
  0049de50  0f8413030000          je        near 0049E169h
  0049de56  f70617080000          test      dword [esi],817h
  0049de5c  0f8507030000          jne       near 0049E169h
  0049de62  8a4643                mov       al,[esi+43h]
  0049de65  84c0                  test      al,al
  0049de67  0f85fc020000          jne       near 0049E169h
  0049de6d  8b466c                mov       eax,[esi+6Ch]
  0049de70  8b5e1c                mov       ebx,[esi+1Ch]
  0049de73  8b4e24                mov       ecx,[esi+24h]
  0049de76  8b781c                mov       edi,[eax+1Ch]
  0049de79  8b6824                mov       ebp,[eax+24h]
  0049de7c  2bfb                  sub       edi,ebx
  0049de7e  2be9                  sub       ebp,ecx
  0049de80  c1ff0c                sar       edi,0Ch
  0049de83  c1fd0c                sar       ebp,0Ch
  0049de86  8bd7                  mov       edx,edi
  0049de88  8bc5                  mov       eax,ebp
  0049de8a  0fafd7                imul      edx,edi
  0049de8d  0fafc5                imul      eax,ebp
  0049de90  03d0                  add       edx,eax
  0049de92  52                    push      edx
  0049de93  e8787b0a00            call      00545A10h
  0049de98  55                    push      ebp
  0049de99  57                    push      edi
  0049de9a  89442420              mov       [esp+20h],eax
  0049de9e  e87f7b0a00            call      00545A22h
  0049dea3  668b4e32              mov       cx,[esi+32h]
  0049dea7  2bc1                  sub       eax,ecx
  0049dea9  25ff0f0000            and       eax,0FFFh
  0049deae  89442410              mov       [esp+10h],eax
  0049deb2  8b442418              mov       eax,[esp+18h]
  0049deb6  3dfa000000            cmp       eax,0FAh
  0049debb  0f8dfd000000          jge       near 0049DFBEh
  0049dec1  e87a920a00            call      00547140h
  0049dec6  99                    cdq
  0049dec7  b964000000            mov       ecx,64h
  0049decc  f7f9                  idiv      ecx
  0049dece  83fa2d                cmp       edx,2Dh
  0049ded1  7d65                  jge       short 0049DF38h
  0049ded3  8b542410              mov       edx,[esp+10h]
  0049ded7  8b7c2414              mov       edi,[esp+14h]
  0049dedb  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049dee2  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049dee9  25ff0f0000            and       eax,0FFFh
  0049deee  3bc1                  cmp       eax,ecx
  0049def0  0f8f0f020000          jg        near 0049E105h
  0049def6  e845920a00            call      00547140h
  0049defb  99                    cdq
  0049defc  8d4c3f01              lea       ecx,[edi+edi+1]
  0049df00  f7f9                  idiv      ecx
  0049df02  8b442418              mov       eax,[esp+18h]
  0049df06  83c264                add       edx,64h
  0049df09  3bc2                  cmp       eax,edx
  0049df0b  0f8df4010000          jge       near 0049E105h
  0049df11  8b16                  mov       edx,[esi]
  0049df13  c7461803000000        mov       dword [esi+18h],3
  0049df1a  81e2fffffffe          and       edx,0FEFFFFFFh
  0049df20  c6464200              mov       byte [esi+42h],0
  0049df24  83ca02                or        edx,2
  0049df27  c7465c00000000        mov       dword [esi+5Ch],0
  0049df2e  8916                  mov       [esi],edx
  0049df30  5f                    pop       edi
  0049df31  5e                    pop       esi
  0049df32  5d                    pop       ebp
  0049df33  5b                    pop       ebx
  0049df34  83c42c                add       esp,2Ch
  0049df37  c3                    ret

LAB_0049df38:
  0049df38  83fa3c                cmp       edx,3Ch
  0049df3b  0f8c2c010000          jl        near 0049E06Dh
  0049df41  83fa4b                cmp       edx,4Bh
  0049df44  7d24                  jge       short 0049DF6Ah
  0049df46  8b4c2440              mov       ecx,[esp+40h]
  0049df4a  8b442418              mov       eax,[esp+18h]
  0049df4e  83c164                add       ecx,64h
  0049df51  3bc1                  cmp       eax,ecx
  0049df53  7e15                  jle       short 0049DF6Ah
  0049df55  c6464204              mov       byte [esi+42h],4
  0049df59  e8e2910a00            call      00547140h
  0049df5e  66894610              mov       [esi+10h],ax
  0049df62  5f                    pop       edi
  0049df63  5e                    pop       esi
  0049df64  5d                    pop       ebp
  0049df65  5b                    pop       ebx
  0049df66  83c42c                add       esp,2Ch
  0049df69  c3                    ret

LAB_0049df6a:
  0049df6a  c6464203              mov       byte [esi+42h],3
  0049df6e  e8cd910a00            call      00547140h
  0049df73  99                    cdq
  0049df74  b90a000000            mov       ecx,0Ah
  0049df79  f7f9                  idiv      ecx
  0049df7b  8b442414              mov       eax,[esp+14h]
  0049df7f  8b4c2440              mov       ecx,[esp+40h]
  0049df83  d1f8                  sar       eax,1
  0049df85  8db980000000          lea       edi,[ecx+80h]
  0049df8b  c1e704                shl       edi,4
  0049df8e  02d0                  add       dl,al
  0049df90  80c214                add       dl,14h
  0049df93  885643                mov       [esi+43h],dl
  0049df96  e8a5910a00            call      00547140h
  0049df9b  99                    cdq
  0049df9c  f7ff                  idiv      edi
  0049df9e  d1ff                  sar       edi,1
  0049dfa0  2bd7                  sub       edx,edi
  0049dfa2  8b7c2410              mov       edi,[esp+10h]
  0049dfa6  03d7                  add       edx,edi
  0049dfa8  66035632              add       dx,[esi+32h]
  0049dfac  81c200080000          add       edx,800h
  0049dfb2  66895610              mov       [esi+10h],dx
  0049dfb6  5f                    pop       edi
  0049dfb7  5e                    pop       esi
  0049dfb8  5d                    pop       ebp
  0049dfb9  5b                    pop       ebx
  0049dfba  83c42c                add       esp,2Ch
  0049dfbd  c3                    ret

LAB_0049dfbe:
  0049dfbe  3df4010000            cmp       eax,1F4h
  0049dfc3  0f8dd1000000          jge       near 0049E09Ah
  0049dfc9  e872910a00            call      00547140h
  0049dfce  99                    cdq
  0049dfcf  b964000000            mov       ecx,64h
  0049dfd4  f7f9                  idiv      ecx
  0049dfd6  8b442440              mov       eax,[esp+40h]
  0049dfda  d1f8                  sar       eax,1
  0049dfdc  83c008                add       eax,8
  0049dfdf  3bd0                  cmp       edx,eax
  0049dfe1  7d2e                  jge       short 0049E011h
  0049dfe3  8b4c2410              mov       ecx,[esp+10h]
  0049dfe7  81c100040000          add       ecx,400h
  0049dfed  81e1ff0f0000          and       ecx,0FFFh
  0049dff3  81f900080000          cmp       ecx,800h
  0049dff9  0f8d06010000          jge       near 0049E105h
  0049dfff  8b16                  mov       edx,[esi]
  0049e001  81e2fffffffe          and       edx,0FEFFFFFFh
  0049e007  83ca02                or        edx,2
  0049e00a  8916                  mov       [esi],edx
  0049e00c  e9da000000            jmp       0049E0EBh

LAB_0049e011:
  0049e011  83fa46                cmp       edx,46h
  0049e014  7d4e                  jge       short 0049E064h
  0049e016  c6464203              mov       byte [esi+42h],3
  0049e01a  e821910a00            call      00547140h
  0049e01f  99                    cdq
  0049e020  b90a000000            mov       ecx,0Ah
  0049e025  f7f9                  idiv      ecx
  0049e027  8b442414              mov       eax,[esp+14h]
  0049e02b  8b4c2440              mov       ecx,[esp+40h]
  0049e02f  d1f8                  sar       eax,1
  0049e031  8db980000000          lea       edi,[ecx+80h]
  0049e037  c1e704                shl       edi,4
  0049e03a  02d0                  add       dl,al
  0049e03c  80c214                add       dl,14h
  0049e03f  885643                mov       [esi+43h],dl
  0049e042  e8f9900a00            call      00547140h
  0049e047  99                    cdq
  0049e048  f7ff                  idiv      edi
  0049e04a  8b4c2410              mov       ecx,[esp+10h]
  0049e04e  d1ff                  sar       edi,1
  0049e050  2bd7                  sub       edx,edi
  0049e052  03d1                  add       edx,ecx
  0049e054  66035632              add       dx,[esi+32h]
  0049e058  66895610              mov       [esi+10h],dx
  0049e05c  5f                    pop       edi
  0049e05d  5e                    pop       esi
  0049e05e  5d                    pop       ebp
  0049e05f  5b                    pop       ebx
  0049e060  83c42c                add       esp,2Ch
  0049e063  c3                    ret

LAB_0049e064:
  0049e064  83fa5a                cmp       edx,5Ah
  0049e067  0f8dad000000          jge       near 0049E11Ah

LAB_0049e06d:
  0049e06d  c6464202              mov       byte [esi+42h],2
  0049e071  e8ca900a00            call      00547140h
  0049e076  99                    cdq
  0049e077  b90a000000            mov       ecx,0Ah
  0049e07c  66c746106400          mov       word [esi+10h],64h
  0049e082  f7f9                  idiv      ecx
  0049e084  8b442414              mov       eax,[esp+14h]
  0049e088  d1f8                  sar       eax,1
  0049e08a  02d0                  add       dl,al
  0049e08c  80c214                add       dl,14h
  0049e08f  885643                mov       [esi+43h],dl
  0049e092  5f                    pop       edi
  0049e093  5e                    pop       esi
  0049e094  5d                    pop       ebp
  0049e095  5b                    pop       ebx
  0049e096  83c42c                add       esp,2Ch
  0049e099  c3                    ret

LAB_0049e09a:
  0049e09a  e8a1900a00            call      00547140h
  0049e09f  99                    cdq
  0049e0a0  b964000000            mov       ecx,64h
  0049e0a5  f7f9                  idiv      ecx
  0049e0a7  8b442440              mov       eax,[esp+40h]
  0049e0ab  d1f8                  sar       eax,1
  0049e0ad  83c008                add       eax,8
  0049e0b0  3bd0                  cmp       edx,eax
  0049e0b2  7d61                  jge       short 0049E115h
  0049e0b4  817c2418bc020000      cmp       dword [esp+18h],2BCh
  0049e0bc  7d47                  jge       short 0049E105h
  0049e0be  8b4c2410              mov       ecx,[esp+10h]
  0049e0c2  8b442414              mov       eax,[esp+14h]
  0049e0c6  8d948100010000        lea       edx,[ecx+eax*4+100h]
  0049e0cd  8d04c500020000        lea       eax,[eax*8+200h]
  0049e0d4  81e2ff0f0000          and       edx,0FFFh
  0049e0da  3bd0                  cmp       edx,eax
  0049e0dc  7f27                  jg        short 0049E105h
  0049e0de  8b0e                  mov       ecx,[esi]
  0049e0e0  81e1fffffffe          and       ecx,0FEFFFFFFh
  0049e0e6  83c902                or        ecx,2
  0049e0e9  890e                  mov       [esi],ecx

LAB_0049e0eb:
  0049e0eb  c7461803000000        mov       dword [esi+18h],3
  0049e0f2  c6464201              mov       byte [esi+42h],1
  0049e0f6  c7465c00000000        mov       dword [esi+5Ch],0
  0049e0fd  5f                    pop       edi
  0049e0fe  5e                    pop       esi
  0049e0ff  5d                    pop       ebp
  0049e100  5b                    pop       ebx
  0049e101  83c42c                add       esp,2Ch
  0049e104  c3                    ret

LAB_0049e105:
  0049e105  c6464201              mov       byte [esi+42h],1
  0049e109  c6464301              mov       byte [esi+43h],1
  0049e10d  5f                    pop       edi
  0049e10e  5e                    pop       esi
  0049e10f  5d                    pop       ebp
  0049e110  5b                    pop       ebx
  0049e111  83c42c                add       esp,2Ch
  0049e114  c3                    ret

LAB_0049e115:
  0049e115  83fa3c                cmp       edx,3Ch
  0049e118  7d27                  jge       short 0049E141h

LAB_0049e11a:
  0049e11a  c6464200              mov       byte [esi+42h],0
  0049e11e  e81d900a00            call      00547140h
  0049e123  99                    cdq
  0049e124  b90a000000            mov       ecx,0Ah
  0049e129  f7f9                  idiv      ecx
  0049e12b  8b442414              mov       eax,[esp+14h]
  0049e12f  d1f8                  sar       eax,1
  0049e131  02d0                  add       dl,al
  0049e133  80c214                add       dl,14h
  0049e136  885643                mov       [esi+43h],dl
  0049e139  5f                    pop       edi
  0049e13a  5e                    pop       esi
  0049e13b  5d                    pop       ebp
  0049e13c  5b                    pop       ebx
  0049e13d  83c42c                add       esp,2Ch
  0049e140  c3                    ret

LAB_0049e141:
  0049e141  c6464203              mov       byte [esi+42h],3
  0049e145  e8f68f0a00            call      00547140h
  0049e14a  99                    cdq
  0049e14b  b90a000000            mov       ecx,0Ah
  0049e150  f7f9                  idiv      ecx
  0049e152  8b442414              mov       eax,[esp+14h]
  0049e156  d1f8                  sar       eax,1
  0049e158  02d0                  add       dl,al
  0049e15a  80c214                add       dl,14h
  0049e15d  885643                mov       [esi+43h],dl
  0049e160  e8db8f0a00            call      00547140h
  0049e165  66894610              mov       [esi+10h],ax

LAB_0049e169:
  0049e169  5f                    pop       edi
  0049e16a  5e                    pop       esi
  0049e16b  5d                    pop       ebp
  0049e16c  5b                    pop       ebx
  0049e16d  83c42c                add       esp,2Ch
  0049e170  c3                    ret
  0049e171  90                    nop
  0049e172  8bff                  mov       edi,edi
  0049e174  14dc                  adc       al,0DCh
  0049e176  49                    dec       ecx
  0049e177  004cdc49              add       [esp+ebx*8+49h],cl
  0049e17b  008cdc490014dd        add       [esp+ebx*8-22EBFFB7h],cl
  0049e182  49                    dec       ecx
  0049e183  00b5dd490090          add       [ebp-6FFFB623h],dh
  0049e189  90                    nop
  0049e18a  90                    nop
  0049e18b  90                    nop
  0049e18c  90                    nop
  0049e18d  90                    nop
  0049e18e  90                    nop
  0049e18f  90                    nop

35 cible(s) de saut interne : 0x0049dafa, 0x0049db1f, 0x0049db35, 0x0049db89, 0x0049db93, 0x0049db9b, 0x0049dbaf, 0x0049dbff, 0x0049dc24, 0x0049dc37, 0x0049dc5c, 0x0049dc9c, 0x0049dcaf, 0x0049dcbb, 0x0049dcef, 0x0049dcff, 0x0049dd20, 0x0049dd30, 0x0049dd9f, 0x0049ddaf, 0x0049de19, 0x0049de1c, 0x0049df38, 0x0049df6a, 0x0049dfbe, 0x0049e011, 0x0049e064, 0x0049e06d, 0x0049e09a, 0x0049e0eb, 0x0049e105, 0x0049e115, 0x0049e11a, 0x0049e141, 0x0049e169
