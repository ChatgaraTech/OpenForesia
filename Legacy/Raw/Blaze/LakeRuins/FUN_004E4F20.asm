Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e4f20  section .text  offset fichier 0xe4320  1952 octets

  004e4f20  83ec28                sub       esp,28h
  004e4f23  8d44240c              lea       eax,[esp+0Ch]
  004e4f27  8d4c2424              lea       ecx,[esp+24h]
  004e4f2b  8d542420              lea       edx,[esp+20h]
  004e4f2f  53                    push      ebx
  004e4f30  55                    push      ebp
  004e4f31  56                    push      esi
  004e4f32  8b742438              mov       esi,[esp+38h]
  004e4f36  57                    push      edi
  004e4f37  50                    push      eax
  004e4f38  51                    push      ecx
  004e4f39  52                    push      edx
  004e4f3a  56                    push      esi
  004e4f3b  e8403a0500            call      00538980h
  004e4f40  83c410                add       esp,10h
  004e4f43  8d442424              lea       eax,[esp+24h]
  004e4f47  8d4c2414              lea       ecx,[esp+14h]
  004e4f4b  8d54243c              lea       edx,[esp+3Ch]
  004e4f4f  50                    push      eax
  004e4f50  51                    push      ecx
  004e4f51  8d442420              lea       eax,[esp+20h]
  004e4f55  52                    push      edx
  004e4f56  8d4c241c              lea       ecx,[esp+1Ch]
  004e4f5a  50                    push      eax
  004e4f5b  51                    push      ecx
  004e4f5c  56                    push      esi
  004e4f5d  e8be0f0500            call      00535F20h
  004e4f62  8b44242c              mov       eax,[esp+2Ch]
  004e4f66  33db                  xor       ebx,ebx
  004e4f68  83c418                add       esp,18h
  004e4f6b  3bc3                  cmp       eax,ebx
  004e4f6d  0f84ca050000          je        near 004E553Dh
  004e4f73  56                    push      esi
  004e4f74  e82766ffff            call      004DB5A0h
  004e4f79  8b06                  mov       eax,[esi]
  004e4f7b  83c404                add       esp,4
  004e4f7e  a802                  test      al,2
  004e4f80  0f848d020000          je        near 004E5213h
  004e4f86  f644242410            test      byte [esp+24h],10h
  004e4f8b  740c                  je        short 004E4F99h
  004e4f8d  24fd                  and       al,0FDh
  004e4f8f  885e43                mov       [esi+43h],bl
  004e4f92  8906                  mov       [esi],eax
  004e4f94  e9a4050000            jmp       004E553Dh

LAB_004e4f99:
  004e4f99  8a4642                mov       al,[esi+42h]
  004e4f9c  84c0                  test      al,al
  004e4f9e  0f8599050000          jne       near 004E553Dh
  004e4fa4  0fbf4610              movsx     eax,word [esi+10h]
  004e4fa8  2bc3                  sub       eax,ebx
  004e4faa  0f8491000000          je        near 004E5041h
  004e4fb0  48                    dec       eax
  004e4fb1  0f8586050000          jne       near 004E553Dh
  004e4fb7  66837e1a09            cmp       word [esi+1Ah],9
  004e4fbc  895e5c                mov       [esi+5Ch],ebx
  004e4fbf  7544                  jne       short 004E5005h
  004e4fc1  0fbf5632              movsx     edx,word [esi+32h]
  004e4fc5  52                    push      edx
  004e4fc6  c7465c00800200        mov       dword [esi+5Ch],28000h
  004e4fcd  e84a0a0600            call      00545A1Ch
  004e4fd2  0faf465c              imul      eax,[esi+5Ch]
  004e4fd6  8b6e50                mov       ebp,[esi+50h]
  004e4fd9  c1f80c                sar       eax,0Ch
  004e4fdc  03e8                  add       ebp,eax
  004e4fde  0fbf4632              movsx     eax,word [esi+32h]
  004e4fe2  50                    push      eax
  004e4fe3  896e50                mov       [esi+50h],ebp
  004e4fe6  e82b0a0600            call      00545A16h
  004e4feb  0faf465c              imul      eax,[esi+5Ch]
  004e4fef  8b4e58                mov       ecx,[esi+58h]
  004e4ff2  c7465c00500000        mov       dword [esi+5Ch],5000h
  004e4ff9  c1f80c                sar       eax,0Ch
  004e4ffc  03c8                  add       ecx,eax
  004e4ffe  66ff4e10              dec       word [esi+10h]
  004e5002  894e58                mov       [esi+58h],ecx

LAB_004e5005:
  004e5005  8b44243c              mov       eax,[esp+3Ch]
  004e5009  8d8880000000          lea       ecx,[eax+80h]
  004e500f  81e1ff0f0000          and       ecx,0FFFh
  004e5015  81f900010000          cmp       ecx,100h
  004e501b  0f8e18050000          jle       near 004E5539h
  004e5021  25ff0f0000            and       eax,0FFFh
  004e5026  33d2                  xor       edx,edx
  004e5028  3d00080000            cmp       eax,800h
  004e502d  0f9dc2                setge     dl
  004e5030  4a                    dec       edx
  004e5031  81e200010000          and       edx,100h
  004e5037  83c280                add       edx,0FFFFFF80h
  004e503a  8bc2                  mov       eax,edx
  004e503c  e9f8040000            jmp       004E5539h

LAB_004e5041:
  004e5041  8b4650                mov       eax,[esi+50h]
  004e5044  8b4e58                mov       ecx,[esi+58h]
  004e5047  c1f80c                sar       eax,0Ch
  004e504a  8bd0                  mov       edx,eax
  004e504c  c1f90c                sar       ecx,0Ch
  004e504f  0fafd0                imul      edx,eax
  004e5052  8bc1                  mov       eax,ecx
  004e5054  0fafc1                imul      eax,ecx
  004e5057  03d0                  add       edx,eax
  004e5059  52                    push      edx
  004e505a  e8b1090600            call      00545A10h
  004e505f  8b0e                  mov       ecx,[esi]
  004e5061  f7c100000002          test      ecx,2000000h
  004e5067  740a                  je        short 004E5073h
  004e5069  83f805                cmp       eax,5
  004e506c  7f05                  jg        short 004E5073h
  004e506e  83e1dd                and       ecx,0FFFFFFDDh
  004e5071  890e                  mov       [esi],ecx

LAB_004e5073:
  004e5073  83f803                cmp       eax,3
  004e5076  0f8cc1040000          jl        near 004E553Dh
  004e507c  f60620                test      byte [esi],20h
  004e507f  0f85b8040000          jne       near 004E553Dh
  004e5085  8b44243c              mov       eax,[esp+3Ch]
  004e5089  8d4828                lea       ecx,[eax+28h]
  004e508c  81e1ff0f0000          and       ecx,0FFFh
  004e5092  83f950                cmp       ecx,50h
  004e5095  7e18                  jle       short 004E50AFh
  004e5097  25ff0f0000            and       eax,0FFFh
  004e509c  33d2                  xor       edx,edx
  004e509e  3d00080000            cmp       eax,800h
  004e50a3  0f9dc2                setge     dl
  004e50a6  4a                    dec       edx
  004e50a7  83e250                and       edx,50h
  004e50aa  83c2d8                add       edx,0FFFFFFD8h
  004e50ad  8bc2                  mov       eax,edx

LAB_004e50af:
  004e50af  66014632              add       [esi+32h],ax
  004e50b3  8d6e1c                lea       ebp,[esi+1Ch]
  004e50b6  53                    push      ebx
  004e50b7  68282e5500            push      552E28h
  004e50bc  55                    push      ebp
  004e50bd  56                    push      esi
  004e50be  e86d4b0300            call      00519C30h
  004e50c3  8bf8                  mov       edi,eax
  004e50c5  83c410                add       esp,10h
  004e50c8  3bfb                  cmp       edi,ebx
  004e50ca  7406                  je        short 004E50D2h
  004e50cc  8b06                  mov       eax,[esi]
  004e50ce  0c20                  or        al,20h
  004e50d0  8906                  mov       [esi],eax

LAB_004e50d2:
  004e50d2  391db8e05d00          cmp       [5DE0B8h],ebx
  004e50d8  0f845f040000          je        near 004E553Dh
  004e50de  8b1f                  mov       ebx,[edi]
  004e50e0  66c787960000000800    mov       word [edi+96h],8
  004e50e9  83cb40                or        ebx,40h
  004e50ec  891f                  mov       [edi],ebx
  004e50ee  8d5f1c                lea       ebx,[edi+1Ch]
  004e50f1  53                    push      ebx
  004e50f2  6800180000            push      1800h
  004e50f7  6800030000            push      300h
  004e50fc  6a03                  push      3
  004e50fe  895c2430              mov       [esp+30h],ebx
  004e5102  e8198a0500            call      0053DB20h
  004e5107  83c410                add       esp,10h
  004e510a  e831200600            call      00547140h
  004e510f  99                    cdq
  004e5110  b964000000            mov       ecx,64h
  004e5115  f7f9                  idiv      ecx
  004e5117  83fa1e                cmp       edx,1Eh
  004e511a  0f8dcd000000          jge       near 004E51EDh
  004e5120  8b4c241c              mov       ecx,[esp+1Ch]
  004e5124  57                    push      edi
  004e5125  56                    push      esi
  004e5126  668b4130              mov       ax,[ecx+30h]
  004e512a  0fbfd0                movsx     edx,ax
  004e512d  66d1f8                sar       ax,1
  004e5130  89542430              mov       [esp+30h],edx
  004e5134  66894130              mov       [ecx+30h],ax
  004e5138  e8434a0500            call      00539B80h
  004e513d  8b442424              mov       eax,[esp+24h]
  004e5141  668b4c2430            mov       cx,[esp+30h]
  004e5146  83c408                add       esp,8
  004e5149  66894830              mov       [eax+30h],cx
  004e514d  8b07                  mov       eax,[edi]
  004e514f  a900000040            test      eax,40000000h
  004e5154  0f859d000000          jne       near 004E51F7h
  004e515a  0fbf5632              movsx     edx,word [esi+32h]
  004e515e  8b44243c              mov       eax,[esp+3Ch]
  004e5162  03c2                  add       eax,edx
  004e5164  50                    push      eax
  004e5165  e8b2080600            call      00545A1Ch
  004e516a  8d0440                lea       eax,[eax+eax*2]
  004e516d  8d0c80                lea       ecx,[eax+eax*4]
  004e5170  d1e1                  shl       ecx,1
  004e5172  894f50                mov       [edi+50h],ecx
  004e5175  8b44243c              mov       eax,[esp+3Ch]
  004e5179  0fbf5632              movsx     edx,word [esi+32h]
  004e517d  03c2                  add       eax,edx
  004e517f  50                    push      eax
  004e5180  e891080600            call      00545A16h
  004e5185  8d0440                lea       eax,[eax+eax*2]
  004e5188  8d0c80                lea       ecx,[eax+eax*4]
  004e518b  8b07                  mov       eax,[edi]
  004e518d  d1e1                  shl       ecx,1
  004e518f  a801                  test      al,1
  004e5191  894f58                mov       [edi+58h],ecx
  004e5194  750b                  jne       short 004E51A1h
  004e5196  0c01                  or        al,1
  004e5198  c7475400800100        mov       dword [edi+54h],18000h
  004e519f  8907                  mov       [edi],eax

LAB_004e51a1:
  004e51a1  8b5620                mov       edx,[esi+20h]
  004e51a4  8b4c2420              mov       ecx,[esp+20h]
  004e51a8  8b4624                mov       eax,[esi+24h]
  004e51ab  8b5d00                mov       ebx,[ebp]
  004e51ae  89542428              mov       [esp+28h],edx
  004e51b2  8b11                  mov       edx,[ecx]
  004e51b4  8944242c              mov       [esp+2Ch],eax
  004e51b8  895500                mov       [ebp],edx
  004e51bb  8b4720                mov       eax,[edi+20h]
  004e51be  6aff                  push      0FFFFFFFFh
  004e51c0  894620                mov       [esi+20h],eax
  004e51c3  8b4f24                mov       ecx,[edi+24h]
  004e51c6  6a00                  push      0
  004e51c8  6a39                  push      39h
  004e51ca  56                    push      esi
  004e51cb  894e24                mov       [esi+24h],ecx
  004e51ce  e8fd57f9ff            call      0047A9D0h
  004e51d3  8b542438              mov       edx,[esp+38h]
  004e51d7  8b44243c              mov       eax,[esp+3Ch]
  004e51db  895d00                mov       [ebp],ebx
  004e51de  8b5c2430              mov       ebx,[esp+30h]
  004e51e2  83c410                add       esp,10h
  004e51e5  895620                mov       [esi+20h],edx
  004e51e8  894624                mov       [esi+24h],eax
  004e51eb  eb0a                  jmp       short 004E51F7h

LAB_004e51ed:
  004e51ed  57                    push      edi
  004e51ee  56                    push      esi
  004e51ef  e88c490500            call      00539B80h
  004e51f4  83c408                add       esp,8

LAB_004e51f7:
  004e51f7  53                    push      ebx
  004e51f8  6800180000            push      1800h
  004e51fd  6800030000            push      300h
  004e5202  6a03                  push      3
  004e5204  e817890500            call      0053DB20h
  004e5209  83c410                add       esp,10h
  004e520c  33db                  xor       ebx,ebx
  004e520e  e92a030000            jmp       004E553Dh

LAB_004e5213:
  004e5213  a810                  test      al,10h
  004e5215  0f8522030000          jne       near 004E553Dh
  004e521b  a904080000            test      eax,804h
  004e5220  0f843c010000          je        near 004E5362h
  004e5226  f7442424e8030040      test      dword [esp+24h],400003E8h
  004e522e  740b                  je        short 004E523Bh
  004e5230  25fbf7ffff            and       eax,0FFFFF7FBh
  004e5235  c6464300              mov       byte [esi+43h],0
  004e5239  8906                  mov       [esi],eax

LAB_004e523b:
  004e523b  668b4610              mov       ax,[esi+10h]
  004e523f  0fbfc8                movsx     ecx,ax
  004e5242  83f903                cmp       ecx,3
  004e5245  0f87f2020000          ja        near 004E553Dh
  004e524b  ff248d94564e00        jmp       dword [ecx*4+4E5694h]
  004e5252  8b06                  mov       eax,[esi]
  004e5254  a900000002            test      eax,2000000h
  004e5259  0f84de020000          je        near 004E553Dh
  004e525f  80e4f7                and       ah,0F7h
  004e5262  8906                  mov       [esi],eax
  004e5264  e9d4020000            jmp       004E553Dh
  004e5269  66395e18              cmp       [esi+18h],bx
  004e526d  7411                  je        short 004E5280h
  004e526f  8b06                  mov       eax,[esi]
  004e5271  895e18                mov       [esi+18h],ebx
  004e5274  25fbfffffe            and       eax,0FEFFFFFBh
  004e5279  8906                  mov       [esi],eax
  004e527b  e9bd020000            jmp       004E553Dh

LAB_004e5280:
  004e5280  66837e1a10            cmp       word [esi+1Ah],10h
  004e5285  0f85b2020000          jne       near 004E553Dh
  004e528b  33c0                  xor       eax,eax
  004e528d  6aff                  push      0FFFFFFFFh
  004e528f  668b4606              mov       ax,[esi+6]
  004e5293  6aff                  push      0FFFFFFFFh
  004e5295  8bc8                  mov       ecx,eax
  004e5297  c1f808                sar       eax,8
  004e529a  81e1ff000000          and       ecx,0FFh
  004e52a0  25ff000000            and       eax,0FFh
  004e52a5  51                    push      ecx
  004e52a6  50                    push      eax
  004e52a7  56                    push      esi
  004e52a8  e8d3560500            call      0053A980h
  004e52ad  83c414                add       esp,14h
  004e52b0  66ff4e10              dec       word [esi+10h]
  004e52b4  e984020000            jmp       004E553Dh
  004e52b9  8a4e43                mov       cl,[esi+43h]
  004e52bc  fec9                  dec       cl
  004e52be  884e43                mov       [esi+43h],cl
  004e52c1  7505                  jne       short 004E52C8h
  004e52c3  48                    dec       eax
  004e52c4  66894610              mov       [esi+10h],ax

LAB_004e52c8:
  004e52c8  8b542410              mov       edx,[esp+10h]
  004e52cc  8b4c243c              mov       ecx,[esp+3Ch]
  004e52d0  8d049532000000        lea       eax,[edx*4+32h]
  004e52d7  8d1408                lea       edx,[eax+ecx]
  004e52da  8d3c00                lea       edi,[eax+eax]
  004e52dd  81e2ff0f0000          and       edx,0FFFh
  004e52e3  3bd7                  cmp       edx,edi
  004e52e5  7f09                  jg        short 004E52F0h
  004e52e7  66014e32              add       [esi+32h],cx
  004e52eb  e94d020000            jmp       004E553Dh

LAB_004e52f0:
  004e52f0  81e1ff0f0000          and       ecx,0FFFh
  004e52f6  81f900080000          cmp       ecx,800h
  004e52fc  0f8c37020000          jl        near 004E5539h
  004e5302  e930020000            jmp       004E5537h
  004e5307  53                    push      ebx
  004e5308  53                    push      ebx
  004e5309  68bd000000            push      0BDh
  004e530e  56                    push      esi
  004e530f  e8cc57f9ff            call      0047AAE0h
  004e5314  83c410                add       esp,10h
  004e5317  e8241e0600            call      00547140h
  004e531c  a1402e5500            mov       eax,[552E40h]
  004e5321  25ffff0000            and       eax,0FFFFh
  004e5326  66894606              mov       [esi+6],ax
  004e532a  8b15c4975e00          mov       edx,[5E97C4h]
  004e5330  8bc8                  mov       ecx,eax
  004e5332  25ff000000            and       eax,0FFh
  004e5337  8b929c000000          mov       edx,[edx+9Ch]
  004e533d  c1f908                sar       ecx,8
  004e5340  81e1ff000000          and       ecx,0FFh
  004e5346  8d0440                lea       eax,[eax+eax*2]
  004e5349  c1e004                shl       eax,4
  004e534c  8b0c8a                mov       ecx,[edx+ecx*4]
  004e534f  8a540113              mov       dl,[ecx+eax+13h]
  004e5353  66ff4e10              dec       word [esi+10h]
  004e5357  885643                mov       [esi+43h],dl
  004e535a  895e5c                mov       [esi+5Ch],ebx
  004e535d  e9db010000            jmp       004E553Dh

LAB_004e5362:
  004e5362  a801                  test      al,1
  004e5364  0f85d3010000          jne       near 004E553Dh
  004e536a  33c9                  xor       ecx,ecx
  004e536c  8a4e42                mov       cl,[esi+42h]
  004e536f  83f903                cmp       ecx,3
  004e5372  0f87c5010000          ja        near 004E553Dh
  004e5378  ff248da4564e00        jmp       dword [ecx*4+4E56A4h]
  004e537f  66395e18              cmp       [esi+18h],bx
  004e5383  740a                  je        short 004E538Fh
  004e5385  0d00000001            or        eax,1000000h
  004e538a  895e18                mov       [esi+18h],ebx
  004e538d  8906                  mov       [esi],eax

LAB_004e538f:
  004e538f  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e5397  7d09                  jge       short 004E53A2h
  004e5399  c6464300              mov       byte [esi+43h],0
  004e539d  e99b010000            jmp       004E553Dh

LAB_004e53a2:
  004e53a2  8a4643                mov       al,[esi+43h]
  004e53a5  84c0                  test      al,al
  004e53a7  7474                  je        short 004E541Dh
  004e53a9  fec8                  dec       al
  004e53ab  884643                mov       [esi+43h],al
  004e53ae  8b465c                mov       eax,[esi+5Ch]
  004e53b1  8bc8                  mov       ecx,eax
  004e53b3  c1f904                sar       ecx,4
  004e53b6  2bc1                  sub       eax,ecx
  004e53b8  89465c                mov       [esi+5Ch],eax
  004e53bb  e97d010000            jmp       004E553Dh
  004e53c0  66395e18              cmp       [esi+18h],bx
  004e53c4  740a                  je        short 004E53D0h
  004e53c6  0d00000001            or        eax,1000000h
  004e53cb  895e18                mov       [esi+18h],ebx
  004e53ce  8906                  mov       [esi],eax

LAB_004e53d0:
  004e53d0  8b542410              mov       edx,[esp+10h]
  004e53d4  8b4c243c              mov       ecx,[esp+3Ch]
  004e53d8  8d841280000000        lea       eax,[edx+edx+80h]
  004e53df  8d1408                lea       edx,[eax+ecx]
  004e53e2  8d3c00                lea       edi,[eax+eax]
  004e53e5  81e2ff0f0000          and       edx,0FFFh
  004e53eb  3bd7                  cmp       edx,edi
  004e53ed  7f1a                  jg        short 004E5409h
  004e53ef  8b465c                mov       eax,[esi+5Ch]
  004e53f2  66014e32              add       [esi+32h],cx
  004e53f6  8bc8                  mov       ecx,eax
  004e53f8  c6464300              mov       byte [esi+43h],0
  004e53fc  c1f904                sar       ecx,4
  004e53ff  2bc1                  sub       eax,ecx
  004e5401  89465c                mov       [esi+5Ch],eax
  004e5404  e934010000            jmp       004E553Dh

LAB_004e5409:
  004e5409  81e1ff0f0000          and       ecx,0FFFh
  004e540f  81f900080000          cmp       ecx,800h
  004e5415  7c02                  jl        short 004E5419h
  004e5417  f7d8                  neg       eax

LAB_004e5419:
  004e5419  66014632              add       [esi+32h],ax

LAB_004e541d:
  004e541d  8b465c                mov       eax,[esi+5Ch]
  004e5420  8bc8                  mov       ecx,eax
  004e5422  c1f904                sar       ecx,4
  004e5425  2bc1                  sub       eax,ecx
  004e5427  89465c                mov       [esi+5Ch],eax
  004e542a  e90e010000            jmp       004E553Dh
  004e542f  66395e18              cmp       [esi+18h],bx
  004e5433  740a                  je        short 004E543Fh
  004e5435  0d00000001            or        eax,1000000h
  004e543a  895e18                mov       [esi+18h],ebx
  004e543d  8906                  mov       [esi],eax

LAB_004e543f:
  004e543f  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e5447  7d09                  jge       short 004E5452h
  004e5449  c6464300              mov       byte [esi+43h],0
  004e544d  e9eb000000            jmp       004E553Dh

LAB_004e5452:
  004e5452  8a4643                mov       al,[esi+43h]
  004e5455  84c0                  test      al,al
  004e5457  7405                  je        short 004E545Eh
  004e5459  fec8                  dec       al
  004e545b  884643                mov       [esi+43h],al

LAB_004e545e:
  004e545e  8b442410              mov       eax,[esp+10h]
  004e5462  8b4c243c              mov       ecx,[esp+3Ch]
  004e5466  c1f802                sar       eax,2
  004e5469  83c038                add       eax,38h
  004e546c  8d1408                lea       edx,[eax+ecx]
  004e546f  8d3c00                lea       edi,[eax+eax]
  004e5472  81e2ff0f0000          and       edx,0FFFh
  004e5478  3bd7                  cmp       edx,edi
  004e547a  7f06                  jg        short 004E5482h
  004e547c  66014e32              add       [esi+32h],cx
  004e5480  eb14                  jmp       short 004E5496h

LAB_004e5482:
  004e5482  81e1ff0f0000          and       ecx,0FFFh
  004e5488  81f900080000          cmp       ecx,800h
  004e548e  7c02                  jl        short 004E5492h
  004e5490  f7d8                  neg       eax

LAB_004e5492:
  004e5492  66014632              add       [esi+32h],ax

LAB_004e5496:
  004e5496  8b465c                mov       eax,[esi+5Ch]
  004e5499  b900000100            mov       ecx,10000h
  004e549e  2bc8                  sub       ecx,eax
  004e54a0  c1f904                sar       ecx,4
  004e54a3  03c8                  add       ecx,eax
  004e54a5  894e5c                mov       [esi+5Ch],ecx
  004e54a8  e990000000            jmp       004E553Dh
  004e54ad  66395e18              cmp       [esi+18h],bx
  004e54b1  740a                  je        short 004E54BDh
  004e54b3  0d00000001            or        eax,1000000h
  004e54b8  895e18                mov       [esi+18h],ebx
  004e54bb  8906                  mov       [esi],eax

LAB_004e54bd:
  004e54bd  8a4643                mov       al,[esi+43h]
  004e54c0  84c0                  test      al,al
  004e54c2  7405                  je        short 004E54C9h
  004e54c4  fec8                  dec       al
  004e54c6  884643                mov       [esi+43h],al

LAB_004e54c9:
  004e54c9  0fbf5610              movsx     edx,word [esi+10h]
  004e54cd  52                    push      edx
  004e54ce  c7465c00300000        mov       dword [esi+5Ch],3000h
  004e54d5  e842050600            call      00545A1Ch
  004e54da  0faf465c              imul      eax,[esi+5Ch]
  004e54de  8b4e50                mov       ecx,[esi+50h]
  004e54e1  c1f80c                sar       eax,0Ch
  004e54e4  03c8                  add       ecx,eax
  004e54e6  0fbf4610              movsx     eax,word [esi+10h]
  004e54ea  50                    push      eax
  004e54eb  894e50                mov       [esi+50h],ecx
  004e54ee  e823050600            call      00545A16h
  004e54f3  0faf465c              imul      eax,[esi+5Ch]
  004e54f7  8b5658                mov       edx,[esi+58h]
  004e54fa  8b4c243c              mov       ecx,[esp+3Ch]
  004e54fe  c1f80c                sar       eax,0Ch
  004e5501  03d0                  add       edx,eax
  004e5503  8b442410              mov       eax,[esp+10h]
  004e5507  c1f802                sar       eax,2
  004e550a  83c040                add       eax,40h
  004e550d  895658                mov       [esi+58h],edx
  004e5510  895e5c                mov       [esi+5Ch],ebx
  004e5513  8d1408                lea       edx,[eax+ecx]
  004e5516  8d3c00                lea       edi,[eax+eax]
  004e5519  81e2ff0f0000          and       edx,0FFFh
  004e551f  3bd7                  cmp       edx,edi
  004e5521  7f06                  jg        short 004E5529h
  004e5523  66014e32              add       [esi+32h],cx
  004e5527  eb14                  jmp       short 004E553Dh

LAB_004e5529:
  004e5529  81e1ff0f0000          and       ecx,0FFFh
  004e552f  81f900080000          cmp       ecx,800h
  004e5535  7c02                  jl        short 004E5539h

LAB_004e5537:
  004e5537  f7d8                  neg       eax

LAB_004e5539:
  004e5539  66014632              add       [esi+32h],ax

LAB_004e553d:
  004e553d  8b442414              mov       eax,[esp+14h]
  004e5541  50                    push      eax
  004e5542  56                    push      esi
  004e5543  e89853fbff            call      0049A8E0h
  004e5548  83c408                add       esp,8
  004e554b  c746200000e7ff        mov       dword [esi+20h],0FFE70000h
  004e5552  56                    push      esi
  004e5553  e8e8e30400            call      00533940h
  004e5558  8b8e90000000          mov       ecx,[esi+90h]
  004e555e  8b542434              mov       edx,[esp+34h]
  004e5562  83c404                add       esp,4
  004e5565  81f100200000          xor       ecx,2000h
  004e556b  81e1ffffffdf          and       ecx,0DFFFFFFFh
  004e5571  52                    push      edx
  004e5572  56                    push      esi
  004e5573  898e90000000          mov       [esi+90h],ecx
  004e5579  e872c7fbff            call      004A1CF0h
  004e557e  8b44241c              mov       eax,[esp+1Ch]
  004e5582  83c408                add       esp,8
  004e5585  3bc3                  cmp       eax,ebx
  004e5587  0f84fc000000          je        near 004E5689h
  004e558d  f70616080000          test      dword [esi],816h
  004e5593  0f85f0000000          jne       near 004E5689h
  004e5599  8a4643                mov       al,[esi+43h]
  004e559c  84c0                  test      al,al
  004e559e  0f85e5000000          jne       near 004E5689h
  004e55a4  8b466c                mov       eax,[esi+6Ch]
  004e55a7  8b561c                mov       edx,[esi+1Ch]
  004e55aa  8b781c                mov       edi,[eax+1Ch]
  004e55ad  8b6824                mov       ebp,[eax+24h]
  004e55b0  8b4624                mov       eax,[esi+24h]
  004e55b3  2bfa                  sub       edi,edx
  004e55b5  2be8                  sub       ebp,eax
  004e55b7  c1ff0c                sar       edi,0Ch
  004e55ba  c1fd0c                sar       ebp,0Ch
  004e55bd  8bc7                  mov       eax,edi
  004e55bf  8bcd                  mov       ecx,ebp
  004e55c1  0fafc7                imul      eax,edi
  004e55c4  0fafcd                imul      ecx,ebp
  004e55c7  03c1                  add       eax,ecx
  004e55c9  50                    push      eax
  004e55ca  e841040600            call      00545A10h
  004e55cf  55                    push      ebp
  004e55d0  57                    push      edi
  004e55d1  89442420              mov       [esp+20h],eax
  004e55d5  e848040600            call      00545A22h
  004e55da  668b5632              mov       dx,[esi+32h]
  004e55de  2bc2                  sub       eax,edx
  004e55e0  25ff0f0000            and       eax,0FFFh
  004e55e5  8944243c              mov       [esp+3Ch],eax
  004e55e9  e8521b0600            call      00547140h
  004e55ee  99                    cdq
  004e55ef  b964000000            mov       ecx,64h
  004e55f4  f7f9                  idiv      ecx
  004e55f6  83fa14                cmp       edx,14h
  004e55f9  7d4c                  jge       short 004E5647h
  004e55fb  8b54243c              mov       edx,[esp+3Ch]
  004e55ff  81c200010000          add       edx,100h
  004e5605  81e2ff0f0000          and       edx,0FFFh
  004e560b  81fa00020000          cmp       edx,200h
  004e5611  7f24                  jg        short 004E5637h
  004e5613  8b06                  mov       eax,[esi]
  004e5615  c7461802000000        mov       dword [esi+18h],2
  004e561c  25fffffffe            and       eax,0FEFFFFFFh
  004e5621  c6464200              mov       byte [esi+42h],0
  004e5625  0c02                  or        al,2
  004e5627  66c746100100          mov       word [esi+10h],1
  004e562d  8906                  mov       [esi],eax
  004e562f  5f                    pop       edi
  004e5630  5e                    pop       esi
  004e5631  5d                    pop       ebp
  004e5632  5b                    pop       ebx
  004e5633  83c428                add       esp,28h
  004e5636  c3                    ret

LAB_004e5637:
  004e5637  c6464201              mov       byte [esi+42h],1
  004e563b  c6464301              mov       byte [esi+43h],1
  004e563f  5f                    pop       edi
  004e5640  5e                    pop       esi
  004e5641  5d                    pop       ebp
  004e5642  5b                    pop       ebx
  004e5643  83c428                add       esp,28h
  004e5646  c3                    ret

LAB_004e5647:
  004e5647  83fa5a                cmp       edx,5Ah
  004e564a  7d27                  jge       short 004E5673h
  004e564c  c6464203              mov       byte [esi+42h],3
  004e5650  e8eb1a0600            call      00547140h
  004e5655  99                    cdq
  004e5656  b90a000000            mov       ecx,0Ah
  004e565b  f7f9                  idiv      ecx
  004e565d  02d1                  add       dl,cl
  004e565f  885643                mov       [esi+43h],dl
  004e5662  e8d91a0600            call      00547140h
  004e5667  66894610              mov       [esi+10h],ax
  004e566b  5f                    pop       edi
  004e566c  5e                    pop       esi
  004e566d  5d                    pop       ebp
  004e566e  5b                    pop       ebx
  004e566f  83c428                add       esp,28h
  004e5672  c3                    ret

LAB_004e5673:
  004e5673  8b06                  mov       eax,[esi]
  004e5675  c7461801000000        mov       dword [esi+18h],1
  004e567c  0d04080001            or        eax,1000804h
  004e5681  66c746100300          mov       word [esi+10h],3
  004e5687  8906                  mov       [esi],eax

LAB_004e5689:
  004e5689  5f                    pop       edi
  004e568a  5e                    pop       esi
  004e568b  5d                    pop       ebp
  004e568c  5b                    pop       ebx
  004e568d  83c428                add       esp,28h
  004e5690  c3                    ret
  004e5691  90                    nop
  004e5692  8bff                  mov       edi,edi
  004e5694  52                    push      edx
  004e5695  52                    push      edx
  004e5696  4e                    dec       esi
  004e5697  006952                add       [ecx+52h],ch
  004e569a  4e                    dec       esi
  004e569b  00b9524e0007          add       [ecx+7004E52h],bh
  004e56a1  53                    push      ebx
  004e56a2  4e                    dec       esi
  004e56a3  007f53                add       [edi+53h],bh
  004e56a6  4e                    dec       esi
  004e56a7  00c0                  add       al,al
  004e56a9  53                    push      ebx
  004e56aa  4e                    dec       esi
  004e56ab  002f                  add       [edi],ch
  004e56ad  54                    push      esp
  004e56ae  4e                    dec       esi
  004e56af  00ad544e0090          add       [ebp-6FFFB1ACh],ch
  004e56b5  90                    nop
  004e56b6  90                    nop
  004e56b7  90                    nop
  004e56b8  90                    nop
  004e56b9  90                    nop
  004e56ba  90                    nop
  004e56bb  90                    nop
  004e56bc  90                    nop
  004e56bd  90                    nop
  004e56be  90                    nop
  004e56bf  90                    nop

37 cible(s) de saut interne : 0x004e4f99, 0x004e5005, 0x004e5041, 0x004e5073, 0x004e50af, 0x004e50d2, 0x004e51a1, 0x004e51ed, 0x004e51f7, 0x004e5213, 0x004e523b, 0x004e5280, 0x004e52c8, 0x004e52f0, 0x004e5362, 0x004e538f, 0x004e53a2, 0x004e53d0, 0x004e5409, 0x004e5419, 0x004e541d, 0x004e543f, 0x004e5452, 0x004e545e, 0x004e5482, 0x004e5492, 0x004e5496, 0x004e54bd, 0x004e54c9, 0x004e5529, 0x004e5537, 0x004e5539, 0x004e553d, 0x004e5637, 0x004e5647, 0x004e5673, 0x004e5689
