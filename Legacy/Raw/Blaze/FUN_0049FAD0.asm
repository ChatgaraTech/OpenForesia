C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049fad0  section .text  offset fichier 0x9eed0  2437 octets

  0049fad0  83ec2c                sub       esp,2Ch
  0049fad3  53                    push      ebx
  0049fad4  55                    push      ebp
  0049fad5  56                    push      esi
  0049fad6  8b74243c              mov       esi,[esp+3Ch]
  0049fada  8d442418              lea       eax,[esp+18h]
  0049fade  57                    push      edi
  0049fadf  8d4c2438              lea       ecx,[esp+38h]
  0049fae3  50                    push      eax
  0049fae4  8d542438              lea       edx,[esp+38h]
  0049fae8  51                    push      ecx
  0049fae9  52                    push      edx
  0049faea  56                    push      esi
  0049faeb  e8908e0900            call      00538980h
  0049faf0  83c410                add       esp,10h
  0049faf3  8d442424              lea       eax,[esp+24h]
  0049faf7  8d4c2418              lea       ecx,[esp+18h]
  0049fafb  8d542410              lea       edx,[esp+10h]
  0049faff  50                    push      eax
  0049fb00  51                    push      ecx
  0049fb01  8d44241c              lea       eax,[esp+1Ch]
  0049fb05  52                    push      edx
  0049fb06  8d4c244c              lea       ecx,[esp+4Ch]
  0049fb0a  50                    push      eax
  0049fb0b  51                    push      ecx
  0049fb0c  56                    push      esi
  0049fb0d  e80e640900            call      00535F20h
  0049fb12  8b4c2458              mov       ecx,[esp+58h]
  0049fb16  8b442430              mov       eax,[esp+30h]
  0049fb1a  bb40000000            mov       ebx,40h
  0049fb1f  33ed                  xor       ebp,ebp
  0049fb21  83c418                add       esp,18h
  0049fb24  2bd9                  sub       ebx,ecx
  0049fb26  3bc5                  cmp       eax,ebp
  0049fb28  895c2420              mov       [esp+20h],ebx
  0049fb2c  bf01000000            mov       edi,1
  0049fb31  0f8417060000          je        near 004A014Eh
  0049fb37  56                    push      esi
  0049fb38  e863ba0300            call      004DB5A0h
  0049fb3d  8b06                  mov       eax,[esi]
  0049fb3f  83c404                add       esp,4
  0049fb42  a802                  test      al,2
  0049fb44  0f846d020000          je        near 0049FDB7h
  0049fb4a  f644242410            test      byte [esp+24h],10h
  0049fb4f  740d                  je        short 0049FB5Eh
  0049fb51  24fd                  and       al,0FDh
  0049fb53  c6464300              mov       byte [esi+43h],0
  0049fb57  8906                  mov       [esi],eax
  0049fb59  e9f0050000            jmp       004A014Eh

LAB_0049fb5e:
  0049fb5e  33c9                  xor       ecx,ecx
  0049fb60  8a4e42                mov       cl,[esi+42h]
  0049fb63  2bcd                  sub       ecx,ebp
  0049fb65  0f84c8010000          je        near 0049FD33h
  0049fb6b  49                    dec       ecx
  0049fb6c  0f85dc050000          jne       near 004A014Eh
  0049fb72  8b4650                mov       eax,[esi+50h]
  0049fb75  8b4e58                mov       ecx,[esi+58h]
  0049fb78  c1f80c                sar       eax,0Ch
  0049fb7b  8bd0                  mov       edx,eax
  0049fb7d  c1f90c                sar       ecx,0Ch
  0049fb80  0fafd0                imul      edx,eax
  0049fb83  8bc1                  mov       eax,ecx
  0049fb85  0fafc1                imul      eax,ecx
  0049fb88  03d0                  add       edx,eax
  0049fb8a  52                    push      edx
  0049fb8b  e8805e0a00            call      00545A10h
  0049fb90  8b0e                  mov       ecx,[esi]
  0049fb92  f7c100000002          test      ecx,2000000h
  0049fb98  7416                  je        short 0049FBB0h
  0049fb9a  8b542440              mov       edx,[esp+40h]
  0049fb9e  c1fa03                sar       edx,3
  0049fba1  42                    inc       edx
  0049fba2  3bc2                  cmp       eax,edx
  0049fba4  7f0a                  jg        short 0049FBB0h
  0049fba6  83e1dd                and       ecx,0FFFFFFDDh
  0049fba9  890e                  mov       [esi],ecx
  0049fbab  e99e050000            jmp       004A014Eh

LAB_0049fbb0:
  0049fbb0  66837e1a11            cmp       word [esi+1Ah],11h
  0049fbb5  0f8c21010000          jl        near 0049FCDCh
  0049fbbb  83f806                cmp       eax,6
  0049fbbe  0f8c18010000          jl        near 0049FCDCh
  0049fbc4  f6c120                test      cl,20h
  0049fbc7  0f850f010000          jne       near 0049FCDCh
  0049fbcd  8d6e1c                lea       ebp,[esi+1Ch]
  0049fbd0  6a00                  push      0
  0049fbd2  689c295500            push      55299Ch
  0049fbd7  55                    push      ebp
  0049fbd8  56                    push      esi
  0049fbd9  e852a00700            call      00519C30h
  0049fbde  8bf8                  mov       edi,eax
  0049fbe0  83c410                add       esp,10h
  0049fbe3  85ff                  test      edi,edi
  0049fbe5  7406                  je        short 0049FBEDh
  0049fbe7  8b06                  mov       eax,[esi]
  0049fbe9  0c20                  or        al,20h
  0049fbeb  8906                  mov       [esi],eax

LAB_0049fbed:
  0049fbed  a1b8e05d00            mov       eax,[5DE0B8h]
  0049fbf2  85c0                  test      eax,eax
  0049fbf4  0f84e2000000          je        near 0049FCDCh
  0049fbfa  8b1f                  mov       ebx,[edi]
  0049fbfc  66c787960000001000    mov       word [edi+96h],10h
  0049fc05  83cb40                or        ebx,40h
  0049fc08  57                    push      edi
  0049fc09  891f                  mov       [edi],ebx
  0049fc0b  8b4c2420              mov       ecx,[esp+20h]
  0049fc0f  56                    push      esi
  0049fc10  668b4130              mov       ax,[ecx+30h]
  0049fc14  0fbfd8                movsx     ebx,ax
  0049fc17  66d1f8                sar       ax,1
  0049fc1a  66894130              mov       [ecx+30h],ax
  0049fc1e  e85d9f0900            call      00539B80h
  0049fc23  8b442424              mov       eax,[esp+24h]
  0049fc27  83c408                add       esp,8
  0049fc2a  66895830              mov       [eax+30h],bx
  0049fc2e  8d5f1c                lea       ebx,[edi+1Ch]
  0049fc31  53                    push      ebx
  0049fc32  6800180000            push      1800h
  0049fc37  6800030000            push      300h
  0049fc3c  6a03                  push      3
  0049fc3e  e8ddde0900            call      0053DB20h
  0049fc43  8b07                  mov       eax,[edi]
  0049fc45  83c410                add       esp,10h
  0049fc48  a900000040            test      eax,40000000h
  0049fc4d  0f8585000000          jne       near 0049FCD8h
  0049fc53  0fbf4e32              movsx     ecx,word [esi+32h]
  0049fc57  51                    push      ecx
  0049fc58  e8bf5d0a00            call      00545A1Ch
  0049fc5d  8d0440                lea       eax,[eax+eax*2]
  0049fc60  8d1480                lea       edx,[eax+eax*4]
  0049fc63  d1e2                  shl       edx,1
  0049fc65  895750                mov       [edi+50h],edx
  0049fc68  0fbf4632              movsx     eax,word [esi+32h]
  0049fc6c  50                    push      eax
  0049fc6d  e8a45d0a00            call      00545A16h
  0049fc72  8d0440                lea       eax,[eax+eax*2]
  0049fc75  8d0c80                lea       ecx,[eax+eax*4]
  0049fc78  8b07                  mov       eax,[edi]
  0049fc7a  d1e1                  shl       ecx,1
  0049fc7c  a801                  test      al,1
  0049fc7e  894f58                mov       [edi+58h],ecx
  0049fc81  750b                  jne       short 0049FC8Eh
  0049fc83  0c01                  or        al,1
  0049fc85  c7475400800100        mov       dword [edi+54h],18000h
  0049fc8c  8907                  mov       [edi],eax

LAB_0049fc8e:
  0049fc8e  8b5500                mov       edx,[ebp]
  0049fc91  8b4620                mov       eax,[esi+20h]
  0049fc94  8b4e24                mov       ecx,[esi+24h]
  0049fc97  89542428              mov       [esp+28h],edx
  0049fc9b  8b13                  mov       edx,[ebx]
  0049fc9d  8944242c              mov       [esp+2Ch],eax
  0049fca1  895500                mov       [ebp],edx
  0049fca4  8b4720                mov       eax,[edi+20h]
  0049fca7  6aff                  push      0FFFFFFFFh
  0049fca9  894c2434              mov       [esp+34h],ecx
  0049fcad  894620                mov       [esi+20h],eax
  0049fcb0  8b4f24                mov       ecx,[edi+24h]
  0049fcb3  6a00                  push      0
  0049fcb5  6a39                  push      39h
  0049fcb7  56                    push      esi
  0049fcb8  894e24                mov       [esi+24h],ecx
  0049fcbb  e810adfdff            call      0047A9D0h
  0049fcc0  8b542438              mov       edx,[esp+38h]
  0049fcc4  8b44243c              mov       eax,[esp+3Ch]
  0049fcc8  8b4c2440              mov       ecx,[esp+40h]
  0049fccc  83c410                add       esp,10h
  0049fccf  895500                mov       [ebp],edx
  0049fcd2  894620                mov       [esi+20h],eax
  0049fcd5  894e24                mov       [esi+24h],ecx

LAB_0049fcd8:
  0049fcd8  8b5c2420              mov       ebx,[esp+20h]

LAB_0049fcdc:
  0049fcdc  66837e1a10            cmp       word [esi+1Ah],10h
  0049fce1  7542                  jne       short 0049FD25h
  0049fce3  8b542440              mov       edx,[esp+40h]
  0049fce7  0fbf4632              movsx     eax,word [esi+32h]
  0049fceb  81c260030000          add       edx,360h
  0049fcf1  50                    push      eax
  0049fcf2  c1e207                shl       edx,7
  0049fcf5  89565c                mov       [esi+5Ch],edx
  0049fcf8  e81f5d0a00            call      00545A1Ch
  0049fcfd  0faf465c              imul      eax,[esi+5Ch]
  0049fd01  0fbf4e32              movsx     ecx,word [esi+32h]
  0049fd05  8b6e50                mov       ebp,[esi+50h]
  0049fd08  51                    push      ecx
  0049fd09  c1f80c                sar       eax,0Ch
  0049fd0c  03e8                  add       ebp,eax
  0049fd0e  896e50                mov       [esi+50h],ebp
  0049fd11  e8005d0a00            call      00545A16h
  0049fd16  0faf465c              imul      eax,[esi+5Ch]
  0049fd1a  8b4e58                mov       ecx,[esi+58h]
  0049fd1d  c1f80c                sar       eax,0Ch
  0049fd20  03c8                  add       ecx,eax
  0049fd22  894e58                mov       [esi+58h],ecx

LAB_0049fd25:
  0049fd25  c7465c00000000        mov       dword [esi+5Ch],0
  0049fd2c  33ed                  xor       ebp,ebp
  0049fd2e  e91b040000            jmp       004A014Eh

LAB_0049fd33:
  0049fd33  66837e1a0d            cmp       word [esi+1Ah],0Dh
  0049fd38  7c5a                  jl        short 0049FD94h
  0049fd3a  55                    push      ebp
  0049fd3b  8d561c                lea       edx,[esi+1Ch]
  0049fd3e  6890295500            push      552990h
  0049fd43  52                    push      edx
  0049fd44  56                    push      esi
  0049fd45  e8e69e0700            call      00519C30h
  0049fd4a  8bf8                  mov       edi,eax
  0049fd4c  a1b8e05d00            mov       eax,[5DE0B8h]
  0049fd51  83c410                add       esp,10h
  0049fd54  3bc5                  cmp       eax,ebp
  0049fd56  7432                  je        short 0049FD8Ah
  0049fd58  8b0f                  mov       ecx,[edi]
  0049fd5a  57                    push      edi
  0049fd5b  83c940                or        ecx,40h
  0049fd5e  56                    push      esi
  0049fd5f  66c787960000001000    mov       word [edi+96h],10h
  0049fd68  890f                  mov       [edi],ecx
  0049fd6a  e8119e0900            call      00539B80h
  0049fd6f  83c408                add       esp,8
  0049fd72  83c71c                add       edi,1Ch
  0049fd75  57                    push      edi
  0049fd76  6800180000            push      1800h
  0049fd7b  6800030000            push      300h
  0049fd80  6a03                  push      3
  0049fd82  e899dd0900            call      0053DB20h
  0049fd87  83c410                add       esp,10h

LAB_0049fd8a:
  0049fd8a  8b06                  mov       eax,[esi]
  0049fd8c  a801                  test      al,1
  0049fd8e  740c                  je        short 0049FD9Ch
  0049fd90  66ff4e1a              dec       word [esi+1Ah]

LAB_0049fd94:
  0049fd94  a801                  test      al,1
  0049fd96  0f85b2030000          jne       near 004A014Eh

LAB_0049fd9c:
  0049fd9c  a900000002            test      eax,2000000h
  0049fda1  0f8413020000          je        near 0049FFBAh
  0049fda7  24fd                  and       al,0FDh
  0049fda9  c6464300              mov       byte [esi+43h],0
  0049fdad  8906                  mov       [esi],eax
  0049fdaf  896e5c                mov       [esi+5Ch],ebp
  0049fdb2  e997030000            jmp       004A014Eh

LAB_0049fdb7:
  0049fdb7  a810                  test      al,10h
  0049fdb9  0f858f030000          jne       near 004A014Eh
  0049fdbf  a904080000            test      eax,804h
  0049fdc4  0f8449010000          je        near 0049FF13h
  0049fdca  f7442424e8030040      test      dword [esp+24h],400003E8h
  0049fdd2  740b                  je        short 0049FDDFh
  0049fdd4  25fbf7ffff            and       eax,0FFFFF7FBh
  0049fdd9  c6464300              mov       byte [esi+43h],0
  0049fddd  8906                  mov       [esi],eax

LAB_0049fddf:
  0049fddf  668b4610              mov       ax,[esi+10h]
  0049fde3  0fbfc8                movsx     ecx,ax
  0049fde6  83f903                cmp       ecx,3
  0049fde9  0f875f030000          ja        near 004A014Eh
  0049fdef  ff248d58044a00        jmp       dword [ecx*4+4A0458h]
  0049fdf6  8b06                  mov       eax,[esi]
  0049fdf8  a900000002            test      eax,2000000h
  0049fdfd  0f844b030000          je        near 004A014Eh
  0049fe03  80e4f7                and       ah,0F7h
  0049fe06  8906                  mov       [esi],eax
  0049fe08  e941030000            jmp       004A014Eh
  0049fe0d  b806000000            mov       eax,6
  0049fe12  66394618              cmp       [esi+18h],ax
  0049fe16  7411                  je        short 0049FE29h
  0049fe18  894618                mov       [esi+18h],eax
  0049fe1b  8b06                  mov       eax,[esi]
  0049fe1d  25fbfffffe            and       eax,0FEFFFFFBh
  0049fe22  8906                  mov       [esi],eax
  0049fe24  e925030000            jmp       004A014Eh

LAB_0049fe29:
  0049fe29  66837e1a11            cmp       word [esi+1Ah],11h
  0049fe2e  0f851a030000          jne       near 004A014Eh
  0049fe34  33c0                  xor       eax,eax
  0049fe36  6aff                  push      0FFFFFFFFh
  0049fe38  668b4606              mov       ax,[esi+6]
  0049fe3c  6aff                  push      0FFFFFFFFh
  0049fe3e  8bc8                  mov       ecx,eax
  0049fe40  c1f808                sar       eax,8
  0049fe43  81e1ff000000          and       ecx,0FFh
  0049fe49  25ff000000            and       eax,0FFh
  0049fe4e  51                    push      ecx
  0049fe4f  50                    push      eax
  0049fe50  56                    push      esi
  0049fe51  e82aab0900            call      0053A980h
  0049fe56  83c414                add       esp,14h
  0049fe59  66ff4e10              dec       word [esi+10h]
  0049fe5d  e9ec020000            jmp       004A014Eh
  0049fe62  8a4e43                mov       cl,[esi+43h]
  0049fe65  fec9                  dec       cl
  0049fe67  884e43                mov       [esi+43h],cl
  0049fe6a  7505                  jne       short 0049FE71h
  0049fe6c  48                    dec       eax
  0049fe6d  66894610              mov       [esi+10h],ax

LAB_0049fe71:
  0049fe71  8b542440              mov       edx,[esp+40h]
  0049fe75  8b4c2410              mov       ecx,[esp+10h]
  0049fe79  8d049532000000        lea       eax,[edx*4+32h]
  0049fe80  8d1408                lea       edx,[eax+ecx]
  0049fe83  8d3c00                lea       edi,[eax+eax]
  0049fe86  81e2ff0f0000          and       edx,0FFFh
  0049fe8c  3bd7                  cmp       edx,edi
  0049fe8e  7f09                  jg        short 0049FE99h
  0049fe90  66014e32              add       [esi+32h],cx
  0049fe94  e9b5020000            jmp       004A014Eh

LAB_0049fe99:
  0049fe99  81e1ff0f0000          and       ecx,0FFFh
  0049fe9f  81f900080000          cmp       ecx,800h
  0049fea5  0f8c9f020000          jl        near 004A014Ah
  0049feab  e998020000            jmp       004A0148h
  0049feb0  55                    push      ebp
  0049feb1  55                    push      ebp
  0049feb2  68bd000000            push      0BDh
  0049feb7  56                    push      esi
  0049feb8  e823acfdff            call      0047AAE0h
  0049febd  83c410                add       esp,10h
  0049fec0  e87b720a00            call      00547140h
  0049fec5  99                    cdq
  0049fec6  b903000000            mov       ecx,3
  0049fecb  f7f9                  idiv      ecx
  0049fecd  33c0                  xor       eax,eax
  0049fecf  668b0455a8295500      mov       ax,[edx*2+5529A8h]
  0049fed7  66894606              mov       [esi+6],ax
  0049fedb  8b0dc4975e00          mov       ecx,[5E97C4h]
  0049fee1  8bd0                  mov       edx,eax
  0049fee3  25ff000000            and       eax,0FFh
  0049fee8  8b899c000000          mov       ecx,[ecx+9Ch]
  0049feee  c1fa08                sar       edx,8
  0049fef1  81e2ff000000          and       edx,0FFh
  0049fef7  8d0440                lea       eax,[eax+eax*2]
  0049fefa  c1e004                shl       eax,4
  0049fefd  8b0c91                mov       ecx,[ecx+edx*4]
  0049ff00  8a540113              mov       dl,[ecx+eax+13h]
  0049ff04  66ff4e10              dec       word [esi+10h]
  0049ff08  885643                mov       [esi+43h],dl
  0049ff0b  896e5c                mov       [esi+5Ch],ebp
  0049ff0e  e93b020000            jmp       004A014Eh

LAB_0049ff13:
  0049ff13  a801                  test      al,1
  0049ff15  0f8533020000          jne       near 004A014Eh
  0049ff1b  33c9                  xor       ecx,ecx
  0049ff1d  8a4e42                mov       cl,[esi+42h]
  0049ff20  83f903                cmp       ecx,3
  0049ff23  0f8725020000          ja        near 004A014Eh
  0049ff29  ff248d68044a00        jmp       dword [ecx*4+4A0468h]
  0049ff30  66396e18              cmp       [esi+18h],bp
  0049ff34  740a                  je        short 0049FF40h
  0049ff36  0d00000001            or        eax,1000000h
  0049ff3b  896e18                mov       [esi+18h],ebp
  0049ff3e  8906                  mov       [esi],eax

LAB_0049ff40:
  0049ff40  817c24142c010000      cmp       dword [esp+14h],12Ch
  0049ff48  7d09                  jge       short 0049FF53h
  0049ff4a  c6464300              mov       byte [esi+43h],0
  0049ff4e  e9fb010000            jmp       004A014Eh

LAB_0049ff53:
  0049ff53  8a4643                mov       al,[esi+43h]
  0049ff56  84c0                  test      al,al
  0049ff58  7460                  je        short 0049FFBAh
  0049ff5a  fec8                  dec       al
  0049ff5c  896e5c                mov       [esi+5Ch],ebp
  0049ff5f  884643                mov       [esi+43h],al
  0049ff62  e9e7010000            jmp       004A014Eh
  0049ff67  66397e18              cmp       [esi+18h],di
  0049ff6b  740a                  je        short 0049FF77h
  0049ff6d  0d00000001            or        eax,1000000h
  0049ff72  897e18                mov       [esi+18h],edi
  0049ff75  8906                  mov       [esi],eax

LAB_0049ff77:
  0049ff77  8b442440              mov       eax,[esp+40h]
  0049ff7b  8b4c2410              mov       ecx,[esp+10h]
  0049ff7f  8d840080000000        lea       eax,[eax+eax+80h]
  0049ff86  8d1408                lea       edx,[eax+ecx]
  0049ff89  8d3c00                lea       edi,[eax+eax]
  0049ff8c  81e2ff0f0000          and       edx,0FFFh
  0049ff92  3bd7                  cmp       edx,edi
  0049ff94  7f10                  jg        short 0049FFA6h
  0049ff96  66014e32              add       [esi+32h],cx
  0049ff9a  c6464300              mov       byte [esi+43h],0
  0049ff9e  896e5c                mov       [esi+5Ch],ebp
  0049ffa1  e9a8010000            jmp       004A014Eh

LAB_0049ffa6:
  0049ffa6  81e1ff0f0000          and       ecx,0FFFh
  0049ffac  81f900080000          cmp       ecx,800h
  0049ffb2  7c02                  jl        short 0049FFB6h
  0049ffb4  f7d8                  neg       eax

LAB_0049ffb6:
  0049ffb6  66014632              add       [esi+32h],ax

LAB_0049ffba:
  0049ffba  896e5c                mov       [esi+5Ch],ebp
  0049ffbd  e98c010000            jmp       004A014Eh
  0049ffc2  66397e18              cmp       [esi+18h],di
  0049ffc6  740a                  je        short 0049FFD2h
  0049ffc8  0d00000001            or        eax,1000000h
  0049ffcd  897e18                mov       [esi+18h],edi
  0049ffd0  8906                  mov       [esi],eax

LAB_0049ffd2:
  0049ffd2  0fbf4610              movsx     eax,word [esi+10h]
  0049ffd6  39442414              cmp       [esp+14h],eax
  0049ffda  7d09                  jge       short 0049FFE5h
  0049ffdc  c6464300              mov       byte [esi+43h],0
  0049ffe0  e969010000            jmp       004A014Eh

LAB_0049ffe5:
  0049ffe5  8a4643                mov       al,[esi+43h]
  0049ffe8  84c0                  test      al,al
  0049ffea  7405                  je        short 0049FFF1h
  0049ffec  fec8                  dec       al
  0049ffee  884643                mov       [esi+43h],al

LAB_0049fff1:
  0049fff1  8b542440              mov       edx,[esp+40h]
  0049fff5  8b4c2410              mov       ecx,[esp+10h]
  0049fff9  8d841280000000        lea       eax,[edx+edx+80h]
  004a0000  8d3c08                lea       edi,[eax+ecx]
  004a0003  8d2c00                lea       ebp,[eax+eax]
  004a0006  81e7ff0f0000          and       edi,0FFFh
  004a000c  3bfd                  cmp       edi,ebp
  004a000e  7f17                  jg        short 004A0027h
  004a0010  66014e32              add       [esi+32h],cx
  004a0014  81c2c0000000          add       edx,0C0h
  004a001a  c1e207                shl       edx,7
  004a001d  89565c                mov       [esi+5Ch],edx
  004a0020  33ed                  xor       ebp,ebp
  004a0022  e927010000            jmp       004A014Eh

LAB_004a0027:
  004a0027  81e1ff0f0000          and       ecx,0FFFh
  004a002d  81f900080000          cmp       ecx,800h
  004a0033  7c02                  jl        short 004A0037h
  004a0035  f7d8                  neg       eax

LAB_004a0037:
  004a0037  66014632              add       [esi+32h],ax
  004a003b  81c2c0000000          add       edx,0C0h
  004a0041  c1e207                shl       edx,7
  004a0044  89565c                mov       [esi+5Ch],edx
  004a0047  33ed                  xor       ebp,ebp
  004a0049  e900010000            jmp       004A014Eh
  004a004e  8a4e43                mov       cl,[esi+43h]
  004a0051  84c9                  test      cl,cl
  004a0053  7405                  je        short 004A005Ah
  004a0055  fec9                  dec       cl
  004a0057  884e43                mov       [esi+43h],cl

LAB_004a005a:
  004a005a  66397e18              cmp       [esi+18h],di
  004a005e  740a                  je        short 004A006Ah
  004a0060  0d00000001            or        eax,1000000h
  004a0065  897e18                mov       [esi+18h],edi
  004a0068  8906                  mov       [esi],eax

LAB_004a006a:
  004a006a  e8d1700a00            call      00547140h
  004a006f  99                    cdq
  004a0070  b964000000            mov       ecx,64h
  004a0075  f7f9                  idiv      ecx
  004a0077  8b442440              mov       eax,[esp+40h]
  004a007b  8bc8                  mov       ecx,eax
  004a007d  c1f903                sar       ecx,3
  004a0080  3bd1                  cmp       edx,ecx
  004a0082  7d51                  jge       short 004A00D5h
  004a0084  0fbf5610              movsx     edx,word [esi+10h]
  004a0088  0500010000            add       eax,100h
  004a008d  52                    push      edx
  004a008e  c1e007                shl       eax,7
  004a0091  89465c                mov       [esi+5Ch],eax
  004a0094  e883590a00            call      00545A1Ch
  004a0099  0faf465c              imul      eax,[esi+5Ch]
  004a009d  8b4e50                mov       ecx,[esi+50h]
  004a00a0  c1f80c                sar       eax,0Ch
  004a00a3  03c8                  add       ecx,eax
  004a00a5  0fbf4610              movsx     eax,word [esi+10h]
  004a00a9  50                    push      eax
  004a00aa  894e50                mov       [esi+50h],ecx
  004a00ad  e864590a00            call      00545A16h
  004a00b2  0faf465c              imul      eax,[esi+5Ch]
  004a00b6  8b5658                mov       edx,[esi+58h]
  004a00b9  8b4e54                mov       ecx,[esi+54h]
  004a00bc  c1f80c                sar       eax,0Ch
  004a00bf  03d0                  add       edx,eax
  004a00c1  8b06                  mov       eax,[esi]
  004a00c3  81c100000100          add       ecx,10000h
  004a00c9  0bc7                  or        eax,edi
  004a00cb  895658                mov       [esi+58h],edx
  004a00ce  894e54                mov       [esi+54h],ecx
  004a00d1  8906                  mov       [esi],eax
  004a00d3  eb79                  jmp       short 004A014Eh

LAB_004a00d5:
  004a00d5  0fbf4e10              movsx     ecx,word [esi+10h]
  004a00d9  0580000000            add       eax,80h
  004a00de  51                    push      ecx
  004a00df  c1e007                shl       eax,7
  004a00e2  89465c                mov       [esi+5Ch],eax
  004a00e5  e832590a00            call      00545A1Ch
  004a00ea  0faf465c              imul      eax,[esi+5Ch]
  004a00ee  0fbf5610              movsx     edx,word [esi+10h]
  004a00f2  8b4e1c                mov       ecx,[esi+1Ch]
  004a00f5  52                    push      edx
  004a00f6  c1f80c                sar       eax,0Ch
  004a00f9  03c8                  add       ecx,eax
  004a00fb  894e1c                mov       [esi+1Ch],ecx
  004a00fe  e813590a00            call      00545A16h
  004a0103  0faf465c              imul      eax,[esi+5Ch]
  004a0107  8b4e24                mov       ecx,[esi+24h]
  004a010a  896e5c                mov       [esi+5Ch],ebp
  004a010d  c1f80c                sar       eax,0Ch
  004a0110  03c8                  add       ecx,eax
  004a0112  8b442440              mov       eax,[esp+40h]
  004a0116  894e24                mov       [esi+24h],ecx
  004a0119  8b4c2410              mov       ecx,[esp+10h]
  004a011d  8d048532000000        lea       eax,[eax*4+32h]
  004a0124  8d1408                lea       edx,[eax+ecx]
  004a0127  8d3c00                lea       edi,[eax+eax]
  004a012a  81e2ff0f0000          and       edx,0FFFh
  004a0130  3bd7                  cmp       edx,edi
  004a0132  7f06                  jg        short 004A013Ah
  004a0134  66014e32              add       [esi+32h],cx
  004a0138  eb14                  jmp       short 004A014Eh

LAB_004a013a:
  004a013a  81e1ff0f0000          and       ecx,0FFFh
  004a0140  81f900080000          cmp       ecx,800h
  004a0146  7c02                  jl        short 004A014Ah

LAB_004a0148:
  004a0148  f7d8                  neg       eax

LAB_004a014a:
  004a014a  66014632              add       [esi+32h],ax

LAB_004a014e:
  004a014e  8b442418              mov       eax,[esp+18h]
  004a0152  50                    push      eax
  004a0153  56                    push      esi
  004a0154  e80780feff            call      00488160h
  004a0159  83c408                add       esp,8
  004a015c  56                    push      esi
  004a015d  e8de370900            call      00533940h
  004a0162  83c404                add       esp,4
  004a0165  56                    push      esi
  004a0166  e885f90300            call      004DFAF0h
  004a016b  8b4c2438              mov       ecx,[esp+38h]
  004a016f  83c404                add       esp,4
  004a0172  51                    push      ecx
  004a0173  56                    push      esi
  004a0174  e8f7350400            call      004E3770h
  004a0179  8b442420              mov       eax,[esp+20h]
  004a017d  83c408                add       esp,8
  004a0180  3bc5                  cmp       eax,ebp
  004a0182  0f84c5020000          je        near 004A044Dh
  004a0188  f70617080000          test      dword [esi],817h
  004a018e  0f85b9020000          jne       near 004A044Dh
  004a0194  8a4643                mov       al,[esi+43h]
  004a0197  84c0                  test      al,al
  004a0199  0f85ae020000          jne       near 004A044Dh
  004a019f  8b466c                mov       eax,[esi+6Ch]
  004a01a2  8b561c                mov       edx,[esi+1Ch]
  004a01a5  8b781c                mov       edi,[eax+1Ch]
  004a01a8  8b6824                mov       ebp,[eax+24h]
  004a01ab  8b4624                mov       eax,[esi+24h]
  004a01ae  2bfa                  sub       edi,edx
  004a01b0  2be8                  sub       ebp,eax
  004a01b2  c1ff0c                sar       edi,0Ch
  004a01b5  c1fd0c                sar       ebp,0Ch
  004a01b8  8bd7                  mov       edx,edi
  004a01ba  8bc5                  mov       eax,ebp
  004a01bc  0fafd7                imul      edx,edi
  004a01bf  0fafc5                imul      eax,ebp
  004a01c2  03d0                  add       edx,eax
  004a01c4  52                    push      edx
  004a01c5  e846580a00            call      00545A10h
  004a01ca  55                    push      ebp
  004a01cb  57                    push      edi
  004a01cc  8944241c              mov       [esp+1Ch],eax
  004a01d0  e84d580a00            call      00545A22h
  004a01d5  668b4e32              mov       cx,[esi+32h]
  004a01d9  2bc1                  sub       eax,ecx
  004a01db  25ff0f0000            and       eax,0FFFh
  004a01e0  89442410              mov       [esp+10h],eax
  004a01e4  8b442414              mov       eax,[esp+14h]
  004a01e8  3dfa000000            cmp       eax,0FAh
  004a01ed  0f8dcc000000          jge       near 004A02BFh
  004a01f3  e8486f0a00            call      00547140h
  004a01f8  99                    cdq
  004a01f9  b964000000            mov       ecx,64h
  004a01fe  f7f9                  idiv      ecx
  004a0200  83fa1e                cmp       edx,1Eh
  004a0203  7d61                  jge       short 004A0266h
  004a0205  8b542410              mov       edx,[esp+10h]
  004a0209  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004a0210  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004a0217  25ff0f0000            and       eax,0FFFh
  004a021c  3bc1                  cmp       eax,ecx
  004a021e  0f8fcd010000          jg        near 004A03F1h
  004a0224  e8176f0a00            call      00547140h
  004a0229  99                    cdq
  004a022a  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004a022e  f7f9                  idiv      ecx
  004a0230  8b442414              mov       eax,[esp+14h]
  004a0234  83c264                add       edx,64h
  004a0237  3bc2                  cmp       eax,edx
  004a0239  0f8db2010000          jge       near 004A03F1h
  004a023f  8b16                  mov       edx,[esi]
  004a0241  c7461803000000        mov       dword [esi+18h],3
  004a0248  81e2fffffffe          and       edx,0FEFFFFFFh
  004a024e  c6464200              mov       byte [esi+42h],0
  004a0252  83ca02                or        edx,2
  004a0255  c7465c00000000        mov       dword [esi+5Ch],0
  004a025c  8916                  mov       [esi],edx
  004a025e  5f                    pop       edi
  004a025f  5e                    pop       esi
  004a0260  5d                    pop       ebp
  004a0261  5b                    pop       ebx
  004a0262  83c42c                add       esp,2Ch
  004a0265  c3                    ret

LAB_004a0266:
  004a0266  83fa3c                cmp       edx,3Ch
  004a0269  0f8c0e010000          jl        near 004A037Dh
  004a026f  c6464203              mov       byte [esi+42h],3
  004a0273  e8c86e0a00            call      00547140h
  004a0278  99                    cdq
  004a0279  b90a000000            mov       ecx,0Ah
  004a027e  f7f9                  idiv      ecx
  004a0280  d1fb                  sar       ebx,1
  004a0282  02d3                  add       dl,bl
  004a0284  80c214                add       dl,14h
  004a0287  885643                mov       [esi+43h],dl
  004a028a  8b542440              mov       edx,[esp+40h]
  004a028e  8dba80000000          lea       edi,[edx+80h]
  004a0294  c1e704                shl       edi,4
  004a0297  e8a46e0a00            call      00547140h
  004a029c  99                    cdq
  004a029d  f7ff                  idiv      edi
  004a029f  d1ff                  sar       edi,1
  004a02a1  2bd7                  sub       edx,edi
  004a02a3  8b7c2410              mov       edi,[esp+10h]
  004a02a7  03d7                  add       edx,edi
  004a02a9  66035632              add       dx,[esi+32h]
  004a02ad  81c200080000          add       edx,800h
  004a02b3  66895610              mov       [esi+10h],dx
  004a02b7  5f                    pop       edi
  004a02b8  5e                    pop       esi
  004a02b9  5d                    pop       ebp
  004a02ba  5b                    pop       ebx
  004a02bb  83c42c                add       esp,2Ch
  004a02be  c3                    ret

LAB_004a02bf:
  004a02bf  3df4010000            cmp       eax,1F4h
  004a02c4  0f8ddc000000          jge       near 004A03A6h
  004a02ca  e8716e0a00            call      00547140h
  004a02cf  99                    cdq
  004a02d0  b964000000            mov       ecx,64h
  004a02d5  f7f9                  idiv      ecx
  004a02d7  83fa2d                cmp       edx,2Dh
  004a02da  7d49                  jge       short 004A0325h
  004a02dc  8b442410              mov       eax,[esp+10h]
  004a02e0  8d8c9800010000        lea       ecx,[eax+ebx*4+100h]
  004a02e7  8d04dd00020000        lea       eax,[ebx*8+200h]
  004a02ee  81e1ff0f0000          and       ecx,0FFFh
  004a02f4  3bc8                  cmp       ecx,eax
  004a02f6  0f8ff5000000          jg        near 004A03F1h
  004a02fc  83fa1e                cmp       edx,1Eh
  004a02ff  0f8cce000000          jl        near 004A03D3h
  004a0305  8b0e                  mov       ecx,[esi]
  004a0307  c7461804000000        mov       dword [esi+18h],4
  004a030e  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a0314  c6464201              mov       byte [esi+42h],1
  004a0318  83c902                or        ecx,2
  004a031b  890e                  mov       [esi],ecx
  004a031d  5f                    pop       edi
  004a031e  5e                    pop       esi
  004a031f  5d                    pop       ebp
  004a0320  5b                    pop       ebx
  004a0321  83c42c                add       esp,2Ch
  004a0324  c3                    ret

LAB_004a0325:
  004a0325  83fa46                cmp       edx,46h
  004a0328  7d4a                  jge       short 004A0374h
  004a032a  c6464203              mov       byte [esi+42h],3
  004a032e  e80d6e0a00            call      00547140h
  004a0333  99                    cdq
  004a0334  b90a000000            mov       ecx,0Ah
  004a0339  f7f9                  idiv      ecx
  004a033b  d1fb                  sar       ebx,1
  004a033d  02d3                  add       dl,bl
  004a033f  80c214                add       dl,14h
  004a0342  885643                mov       [esi+43h],dl
  004a0345  8b542440              mov       edx,[esp+40h]
  004a0349  8dba80000000          lea       edi,[edx+80h]
  004a034f  c1e704                shl       edi,4
  004a0352  e8e96d0a00            call      00547140h
  004a0357  99                    cdq
  004a0358  f7ff                  idiv      edi
  004a035a  8b4c2410              mov       ecx,[esp+10h]
  004a035e  d1ff                  sar       edi,1
  004a0360  2bd7                  sub       edx,edi
  004a0362  03d1                  add       edx,ecx
  004a0364  66035632              add       dx,[esi+32h]
  004a0368  66895610              mov       [esi+10h],dx
  004a036c  5f                    pop       edi
  004a036d  5e                    pop       esi
  004a036e  5d                    pop       ebp
  004a036f  5b                    pop       ebx
  004a0370  83c42c                add       esp,2Ch
  004a0373  c3                    ret

LAB_004a0374:
  004a0374  83fa5a                cmp       edx,5Ah
  004a0377  0f8d89000000          jge       near 004A0406h

LAB_004a037d:
  004a037d  c6464202              mov       byte [esi+42h],2
  004a0381  e8ba6d0a00            call      00547140h
  004a0386  99                    cdq
  004a0387  b90a000000            mov       ecx,0Ah
  004a038c  66c746106400          mov       word [esi+10h],64h
  004a0392  f7f9                  idiv      ecx
  004a0394  d1fb                  sar       ebx,1
  004a0396  02d3                  add       dl,bl
  004a0398  80c214                add       dl,14h
  004a039b  885643                mov       [esi+43h],dl
  004a039e  5f                    pop       edi
  004a039f  5e                    pop       esi
  004a03a0  5d                    pop       ebp
  004a03a1  5b                    pop       ebx
  004a03a2  83c42c                add       esp,2Ch
  004a03a5  c3                    ret

LAB_004a03a6:
  004a03a6  e8956d0a00            call      00547140h
  004a03ab  99                    cdq
  004a03ac  b964000000            mov       ecx,64h
  004a03b1  f7f9                  idiv      ecx
  004a03b3  83fa1e                cmp       edx,1Eh
  004a03b6  7d49                  jge       short 004A0401h
  004a03b8  8b542410              mov       edx,[esp+10h]
  004a03bc  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004a03c3  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004a03ca  25ff0f0000            and       eax,0FFFh
  004a03cf  3bc1                  cmp       eax,ecx
  004a03d1  7f1e                  jg        short 004A03F1h

LAB_004a03d3:
  004a03d3  8b06                  mov       eax,[esi]
  004a03d5  c7461805000000        mov       dword [esi+18h],5
  004a03dc  0d04080001            or        eax,1000804h
  004a03e1  66c746100300          mov       word [esi+10h],3
  004a03e7  8906                  mov       [esi],eax
  004a03e9  5f                    pop       edi
  004a03ea  5e                    pop       esi
  004a03eb  5d                    pop       ebp
  004a03ec  5b                    pop       ebx
  004a03ed  83c42c                add       esp,2Ch
  004a03f0  c3                    ret

LAB_004a03f1:
  004a03f1  c6464201              mov       byte [esi+42h],1
  004a03f5  c6464301              mov       byte [esi+43h],1
  004a03f9  5f                    pop       edi
  004a03fa  5e                    pop       esi
  004a03fb  5d                    pop       ebp
  004a03fc  5b                    pop       ebx
  004a03fd  83c42c                add       esp,2Ch
  004a0400  c3                    ret

LAB_004a0401:
  004a0401  83fa3c                cmp       edx,3Ch
  004a0404  7d23                  jge       short 004A0429h

LAB_004a0406:
  004a0406  c6464200              mov       byte [esi+42h],0
  004a040a  e8316d0a00            call      00547140h
  004a040f  99                    cdq
  004a0410  b90a000000            mov       ecx,0Ah
  004a0415  f7f9                  idiv      ecx
  004a0417  d1fb                  sar       ebx,1
  004a0419  02d3                  add       dl,bl
  004a041b  80c214                add       dl,14h
  004a041e  885643                mov       [esi+43h],dl
  004a0421  5f                    pop       edi
  004a0422  5e                    pop       esi
  004a0423  5d                    pop       ebp
  004a0424  5b                    pop       ebx
  004a0425  83c42c                add       esp,2Ch
  004a0428  c3                    ret

LAB_004a0429:
  004a0429  c6464203              mov       byte [esi+42h],3
  004a042d  e80e6d0a00            call      00547140h
  004a0432  99                    cdq
  004a0433  b90a000000            mov       ecx,0Ah
  004a0438  f7f9                  idiv      ecx
  004a043a  d1fb                  sar       ebx,1
  004a043c  02d3                  add       dl,bl
  004a043e  80c214                add       dl,14h
  004a0441  885643                mov       [esi+43h],dl
  004a0444  e8f76c0a00            call      00547140h
  004a0449  66894610              mov       [esi+10h],ax

LAB_004a044d:
  004a044d  5f                    pop       edi
  004a044e  5e                    pop       esi
  004a044f  5d                    pop       ebp
  004a0450  5b                    pop       ebx
  004a0451  83c42c                add       esp,2Ch
  004a0454  c3                    ret

47 cible(s) de saut interne : 0x0049fb5e, 0x0049fbb0, 0x0049fbed, 0x0049fc8e, 0x0049fcd8, 0x0049fcdc, 0x0049fd25, 0x0049fd33, 0x0049fd8a, 0x0049fd94, 0x0049fd9c, 0x0049fdb7, 0x0049fddf, 0x0049fe29, 0x0049fe71, 0x0049fe99, 0x0049ff13, 0x0049ff40, 0x0049ff53, 0x0049ff77, 0x0049ffa6, 0x0049ffb6, 0x0049ffba, 0x0049ffd2, 0x0049ffe5, 0x0049fff1, 0x004a0027, 0x004a0037, 0x004a005a, 0x004a006a, 0x004a00d5, 0x004a013a, 0x004a0148, 0x004a014a, 0x004a014e, 0x004a0266, 0x004a02bf, 0x004a0325, 0x004a0374, 0x004a037d, 0x004a03a6, 0x004a03d3, 0x004a03f1, 0x004a0401, 0x004a0406, 0x004a0429, 0x004a044d
