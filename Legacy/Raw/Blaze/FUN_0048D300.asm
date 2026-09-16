Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048d300  section .text  offset fichier 0x8c700  2096 octets

  0048d300  83ec38                sub       esp,38h
  0048d303  53                    push      ebx
  0048d304  55                    push      ebp
  0048d305  56                    push      esi
  0048d306  8b742448              mov       esi,[esp+48h]
  0048d30a  8d44241c              lea       eax,[esp+1Ch]
  0048d30e  57                    push      edi
  0048d30f  8d4c2438              lea       ecx,[esp+38h]
  0048d313  50                    push      eax
  0048d314  8d542438              lea       edx,[esp+38h]
  0048d318  51                    push      ecx
  0048d319  33db                  xor       ebx,ebx
  0048d31b  52                    push      edx
  0048d31c  56                    push      esi
  0048d31d  66895c244c            mov       [esp+4Ch],bx
  0048d322  66c744244e3200        mov       word [esp+4Eh],32h
  0048d329  66c74424503c00        mov       word [esp+50h],3Ch
  0048d330  66c74424526400        mov       word [esp+52h],64h
  0048d337  66895c2454            mov       [esp+54h],bx
  0048d33c  66c74424565000        mov       word [esp+56h],50h
  0048d343  e838b60a00            call      00538980h
  0048d348  83c410                add       esp,10h
  0048d34b  8d442424              lea       eax,[esp+24h]
  0048d34f  8d4c241c              lea       ecx,[esp+1Ch]
  0048d353  8d542410              lea       edx,[esp+10h]
  0048d357  50                    push      eax
  0048d358  51                    push      ecx
  0048d359  8d442420              lea       eax,[esp+20h]
  0048d35d  52                    push      edx
  0048d35e  8d4c2458              lea       ecx,[esp+58h]
  0048d362  50                    push      eax
  0048d363  51                    push      ecx
  0048d364  56                    push      esi
  0048d365  e8b68b0a00            call      00535F20h
  0048d36a  8b4c2464              mov       ecx,[esp+64h]
  0048d36e  b840000000            mov       eax,40h
  0048d373  2bc1                  sub       eax,ecx
  0048d375  83c418                add       esp,18h
  0048d378  89442414              mov       [esp+14h],eax
  0048d37c  8b44241c              mov       eax,[esp+1Ch]
  0048d380  3bc3                  cmp       eax,ebx
  0048d382  bf01000000            mov       edi,1
  0048d387  0f849a040000          je        near 0048D827h
  0048d38d  56                    push      esi
  0048d38e  e80de20400            call      004DB5A0h
  0048d393  8b06                  mov       eax,[esi]
  0048d395  83c404                add       esp,4
  0048d398  a802                  test      al,2
  0048d39a  0f849d020000          je        near 0048D63Dh
  0048d3a0  8a4c2424              mov       cl,[esp+24h]
  0048d3a4  bb10000000            mov       ebx,10h
  0048d3a9  84cb                  test      bl,cl
  0048d3ab  740d                  je        short 0048D3BAh
  0048d3ad  24fd                  and       al,0FDh
  0048d3af  c6464300              mov       byte [esi+43h],0
  0048d3b3  8906                  mov       [esi],eax
  0048d3b5  e96d040000            jmp       0048D827h

LAB_0048d3ba:
  0048d3ba  33c9                  xor       ecx,ecx
  0048d3bc  8a4e42                mov       cl,[esi+42h]
  0048d3bf  83e900                sub       ecx,0
  0048d3c2  0f84f9010000          je        near 0048D5C1h
  0048d3c8  49                    dec       ecx
  0048d3c9  0f8558040000          jne       near 0048D827h
  0048d3cf  668b5610              mov       dx,[esi+10h]
  0048d3d3  0fbfca                movsx     ecx,dx
  0048d3d6  83e900                sub       ecx,0
  0048d3d9  0f84b2000000          je        near 0048D491h
  0048d3df  49                    dec       ecx
  0048d3e0  745e                  je        short 0048D440h
  0048d3e2  49                    dec       ecx
  0048d3e3  0f853e040000          jne       near 0048D827h
  0048d3e9  8b465c                mov       eax,[esi+5Ch]
  0048d3ec  8bc8                  mov       ecx,eax
  0048d3ee  c1f904                sar       ecx,4
  0048d3f1  2bc1                  sub       eax,ecx
  0048d3f3  66837e1a0e            cmp       word [esi+1Ah],0Eh
  0048d3f8  89465c                mov       [esi+5Ch],eax
  0048d3fb  7505                  jne       short 0048D402h
  0048d3fd  4a                    dec       edx
  0048d3fe  66895610              mov       [esi+10h],dx

LAB_0048d402:
  0048d402  8b44244c              mov       eax,[esp+4Ch]
  0048d406  8b4c2410              mov       ecx,[esp+10h]
  0048d40a  c1f802                sar       eax,2
  0048d40d  83c020                add       eax,20h
  0048d410  8d1408                lea       edx,[eax+ecx]
  0048d413  8d3c00                lea       edi,[eax+eax]
  0048d416  81e2ff0f0000          and       edx,0FFFh
  0048d41c  3bd7                  cmp       edx,edi
  0048d41e  7f09                  jg        short 0048D429h
  0048d420  66014e32              add       [esi+32h],cx
  0048d424  e9fe030000            jmp       0048D827h

LAB_0048d429:
  0048d429  81e1ff0f0000          and       ecx,0FFFh
  0048d42f  81f900080000          cmp       ecx,800h
  0048d435  0f8ce8030000          jl        near 0048D823h
  0048d43b  e9e1030000            jmp       0048D821h

LAB_0048d440:
  0048d440  8b44244c              mov       eax,[esp+4Ch]
  0048d444  0fbf4e32              movsx     ecx,word [esi+32h]
  0048d448  0580030000            add       eax,380h
  0048d44d  51                    push      ecx
  0048d44e  c1e007                shl       eax,7
  0048d451  89465c                mov       [esi+5Ch],eax
  0048d454  e8c3850b00            call      00545A1Ch
  0048d459  0faf465c              imul      eax,[esi+5Ch]
  0048d45d  0fbf5632              movsx     edx,word [esi+32h]
  0048d461  8b5e50                mov       ebx,[esi+50h]
  0048d464  52                    push      edx
  0048d465  c1f80c                sar       eax,0Ch
  0048d468  03d8                  add       ebx,eax
  0048d46a  895e50                mov       [esi+50h],ebx
  0048d46d  e8a4850b00            call      00545A16h
  0048d472  0faf465c              imul      eax,[esi+5Ch]
  0048d476  8b4e58                mov       ecx,[esi+58h]
  0048d479  c7465c00000000        mov       dword [esi+5Ch],0
  0048d480  c1f80c                sar       eax,0Ch
  0048d483  03c8                  add       ecx,eax
  0048d485  66ff4e10              dec       word [esi+10h]
  0048d489  894e58                mov       [esi+58h],ecx
  0048d48c  e996030000            jmp       0048D827h

LAB_0048d491:
  0048d491  a900000002            test      eax,2000000h
  0048d496  7412                  je        short 0048D4AAh
  0048d498  8a4e43                mov       cl,[esi+43h]
  0048d49b  84c9                  test      cl,cl
  0048d49d  740b                  je        short 0048D4AAh
  0048d49f  fec9                  dec       cl
  0048d4a1  884e43                mov       [esi+43h],cl
  0048d4a4  7504                  jne       short 0048D4AAh
  0048d4a6  24dd                  and       al,0DDh
  0048d4a8  8906                  mov       [esi],eax

LAB_0048d4aa:
  0048d4aa  f60620                test      byte [esi],20h
  0048d4ad  0f8574030000          jne       near 0048D827h
  0048d4b3  8d44243c              lea       eax,[esp+3Ch]
  0048d4b7  8d6e1c                lea       ebp,[esi+1Ch]
  0048d4ba  6a00                  push      0
  0048d4bc  50                    push      eax
  0048d4bd  55                    push      ebp
  0048d4be  56                    push      esi
  0048d4bf  e86cc70800            call      00519C30h
  0048d4c4  8bf8                  mov       edi,eax
  0048d4c6  83c410                add       esp,10h
  0048d4c9  85ff                  test      edi,edi
  0048d4cb  7406                  je        short 0048D4D3h
  0048d4cd  8b06                  mov       eax,[esi]
  0048d4cf  0c20                  or        al,20h
  0048d4d1  8906                  mov       [esi],eax

LAB_0048d4d3:
  0048d4d3  a1b8e05d00            mov       eax,[5DE0B8h]
  0048d4d8  85c0                  test      eax,eax
  0048d4da  0f8447030000          je        near 0048D827h
  0048d4e0  66899f96000000        mov       [edi+96h],bx
  0048d4e7  8b1f                  mov       ebx,[edi]
  0048d4e9  83cb40                or        ebx,40h
  0048d4ec  57                    push      edi
  0048d4ed  891f                  mov       [edi],ebx
  0048d4ef  8b4c2424              mov       ecx,[esp+24h]
  0048d4f3  56                    push      esi
  0048d4f4  668b4130              mov       ax,[ecx+30h]
  0048d4f8  0fbfd8                movsx     ebx,ax
  0048d4fb  66d1f8                sar       ax,1
  0048d4fe  66894130              mov       [ecx+30h],ax
  0048d502  e879c60a00            call      00539B80h
  0048d507  8b4c2428              mov       ecx,[esp+28h]
  0048d50b  83c408                add       esp,8
  0048d50e  66895930              mov       [ecx+30h],bx
  0048d512  8d5f1c                lea       ebx,[edi+1Ch]
  0048d515  53                    push      ebx
  0048d516  6800180000            push      1800h
  0048d51b  6800030000            push      300h
  0048d520  6a03                  push      3
  0048d522  e8f9050b00            call      0053DB20h
  0048d527  8b07                  mov       eax,[edi]
  0048d529  83c410                add       esp,10h
  0048d52c  a900000040            test      eax,40000000h
  0048d531  0f85f0020000          jne       near 0048D827h
  0048d537  0fbf5632              movsx     edx,word [esi+32h]
  0048d53b  52                    push      edx
  0048d53c  e8db840b00            call      00545A1Ch
  0048d541  8d0440                lea       eax,[eax+eax*2]
  0048d544  8d0480                lea       eax,[eax+eax*4]
  0048d547  d1e0                  shl       eax,1
  0048d549  894750                mov       [edi+50h],eax
  0048d54c  0fbf4e32              movsx     ecx,word [esi+32h]
  0048d550  51                    push      ecx
  0048d551  e8c0840b00            call      00545A16h
  0048d556  8d0440                lea       eax,[eax+eax*2]
  0048d559  8d1480                lea       edx,[eax+eax*4]
  0048d55c  8b07                  mov       eax,[edi]
  0048d55e  d1e2                  shl       edx,1
  0048d560  a801                  test      al,1
  0048d562  895758                mov       [edi+58h],edx
  0048d565  750b                  jne       short 0048D572h
  0048d567  0c01                  or        al,1
  0048d569  c7475400800100        mov       dword [edi+54h],18000h
  0048d570  8907                  mov       [edi],eax

LAB_0048d572:
  0048d572  8b4500                mov       eax,[ebp]
  0048d575  8b4e20                mov       ecx,[esi+20h]
  0048d578  8b5624                mov       edx,[esi+24h]
  0048d57b  89442428              mov       [esp+28h],eax
  0048d57f  8b03                  mov       eax,[ebx]
  0048d581  894c242c              mov       [esp+2Ch],ecx
  0048d585  894500                mov       [ebp],eax
  0048d588  8b4f20                mov       ecx,[edi+20h]
  0048d58b  6aff                  push      0FFFFFFFFh
  0048d58d  89542434              mov       [esp+34h],edx
  0048d591  894e20                mov       [esi+20h],ecx
  0048d594  8b5724                mov       edx,[edi+24h]
  0048d597  6a00                  push      0
  0048d599  6a39                  push      39h
  0048d59b  56                    push      esi
  0048d59c  895624                mov       [esi+24h],edx
  0048d59f  e82cd4feff            call      0047A9D0h
  0048d5a4  8b442438              mov       eax,[esp+38h]
  0048d5a8  8b4c243c              mov       ecx,[esp+3Ch]
  0048d5ac  8b542440              mov       edx,[esp+40h]
  0048d5b0  83c410                add       esp,10h
  0048d5b3  894500                mov       [ebp],eax
  0048d5b6  894e20                mov       [esi+20h],ecx
  0048d5b9  895624                mov       [esi+24h],edx
  0048d5bc  e966020000            jmp       0048D827h

LAB_0048d5c1:
  0048d5c1  668b461a              mov       ax,[esi+1Ah]
  0048d5c5  663d0c00              cmp       ax,0Ch
  0048d5c9  7c55                  jl        short 0048D620h
  0048d5cb  663d0f00              cmp       ax,0Fh
  0048d5cf  7f4f                  jg        short 0048D620h
  0048d5d1  8d44243c              lea       eax,[esp+3Ch]
  0048d5d5  6a00                  push      0
  0048d5d7  8d4e1c                lea       ecx,[esi+1Ch]
  0048d5da  50                    push      eax
  0048d5db  51                    push      ecx
  0048d5dc  56                    push      esi
  0048d5dd  e84ec60800            call      00519C30h
  0048d5e2  8bf8                  mov       edi,eax
  0048d5e4  a1b8e05d00            mov       eax,[5DE0B8h]
  0048d5e9  83c410                add       esp,10h
  0048d5ec  85c0                  test      eax,eax
  0048d5ee  7430                  je        short 0048D620h
  0048d5f0  8b17                  mov       edx,[edi]
  0048d5f2  57                    push      edi
  0048d5f3  83ca40                or        edx,40h
  0048d5f6  56                    push      esi
  0048d5f7  66899f96000000        mov       [edi+96h],bx
  0048d5fe  8917                  mov       [edi],edx
  0048d600  e87bc50a00            call      00539B80h
  0048d605  83c408                add       esp,8
  0048d608  83c71c                add       edi,1Ch
  0048d60b  57                    push      edi
  0048d60c  6800180000            push      1800h
  0048d611  6800030000            push      300h
  0048d616  6a03                  push      3
  0048d618  e803050b00            call      0053DB20h
  0048d61d  83c410                add       esp,10h

LAB_0048d620:
  0048d620  8b06                  mov       eax,[esi]
  0048d622  a900000002            test      eax,2000000h
  0048d627  0f84c8000000          je        near 0048D6F5h
  0048d62d  24fd                  and       al,0FDh
  0048d62f  c7465c00000000        mov       dword [esi+5Ch],0
  0048d636  8906                  mov       [esi],eax
  0048d638  e9ea010000            jmp       0048D827h

LAB_0048d63d:
  0048d63d  a811                  test      al,11h
  0048d63f  0f85e2010000          jne       near 0048D827h
  0048d645  a904080000            test      eax,804h
  0048d64a  0f85d7010000          jne       near 0048D827h
  0048d650  33c9                  xor       ecx,ecx
  0048d652  8a4e42                mov       cl,[esi+42h]
  0048d655  83f903                cmp       ecx,3
  0048d658  0f87c9010000          ja        near 0048D827h
  0048d65e  ff248d1cdb4800        jmp       dword [ecx*4+48DB1Ch]
  0048d665  33c9                  xor       ecx,ecx
  0048d667  66394e18              cmp       [esi+18h],cx
  0048d66b  740a                  je        short 0048D677h
  0048d66d  0d00000001            or        eax,1000000h
  0048d672  894e18                mov       [esi+18h],ecx
  0048d675  8906                  mov       [esi],eax

LAB_0048d677:
  0048d677  817c24182c010000      cmp       dword [esp+18h],12Ch
  0048d67f  7d09                  jge       short 0048D68Ah
  0048d681  c6464300              mov       byte [esi+43h],0
  0048d685  e99d010000            jmp       0048D827h

LAB_0048d68a:
  0048d68a  8a4643                mov       al,[esi+43h]
  0048d68d  84c0                  test      al,al
  0048d68f  7405                  je        short 0048D696h
  0048d691  fec8                  dec       al
  0048d693  884643                mov       [esi+43h],al

LAB_0048d696:
  0048d696  894e5c                mov       [esi+5Ch],ecx
  0048d699  e989010000            jmp       0048D827h
  0048d69e  66397e18              cmp       [esi+18h],di
  0048d6a2  740a                  je        short 0048D6AEh
  0048d6a4  0d00000001            or        eax,1000000h
  0048d6a9  897e18                mov       [esi+18h],edi
  0048d6ac  8906                  mov       [esi],eax

LAB_0048d6ae:
  0048d6ae  8b54244c              mov       edx,[esp+4Ch]
  0048d6b2  8b4c2410              mov       ecx,[esp+10h]
  0048d6b6  8d841280000000        lea       eax,[edx+edx+80h]
  0048d6bd  8d1408                lea       edx,[eax+ecx]
  0048d6c0  8d3c00                lea       edi,[eax+eax]
  0048d6c3  81e2ff0f0000          and       edx,0FFFh
  0048d6c9  3bd7                  cmp       edx,edi
  0048d6cb  7f14                  jg        short 0048D6E1h
  0048d6cd  66014e32              add       [esi+32h],cx
  0048d6d1  c6464300              mov       byte [esi+43h],0
  0048d6d5  c7465c00000000        mov       dword [esi+5Ch],0
  0048d6dc  e946010000            jmp       0048D827h

LAB_0048d6e1:
  0048d6e1  81e1ff0f0000          and       ecx,0FFFh
  0048d6e7  81f900080000          cmp       ecx,800h
  0048d6ed  7c02                  jl        short 0048D6F1h
  0048d6ef  f7d8                  neg       eax

LAB_0048d6f1:
  0048d6f1  66014632              add       [esi+32h],ax

LAB_0048d6f5:
  0048d6f5  c7465c00000000        mov       dword [esi+5Ch],0
  0048d6fc  e926010000            jmp       0048D827h
  0048d701  66397e18              cmp       [esi+18h],di
  0048d705  740a                  je        short 0048D711h
  0048d707  0d00000001            or        eax,1000000h
  0048d70c  897e18                mov       [esi+18h],edi
  0048d70f  8906                  mov       [esi],eax

LAB_0048d711:
  0048d711  0fbf4610              movsx     eax,word [esi+10h]
  0048d715  39442418              cmp       [esp+18h],eax
  0048d719  7d09                  jge       short 0048D724h
  0048d71b  c6464300              mov       byte [esi+43h],0
  0048d71f  e903010000            jmp       0048D827h

LAB_0048d724:
  0048d724  8a4643                mov       al,[esi+43h]
  0048d727  84c0                  test      al,al
  0048d729  7405                  je        short 0048D730h
  0048d72b  fec8                  dec       al
  0048d72d  884643                mov       [esi+43h],al

LAB_0048d730:
  0048d730  8b54244c              mov       edx,[esp+4Ch]
  0048d734  8b4c2410              mov       ecx,[esp+10h]
  0048d738  8d841280000000        lea       eax,[edx+edx+80h]
  0048d73f  8d3c08                lea       edi,[eax+ecx]
  0048d742  8d1c00                lea       ebx,[eax+eax]
  0048d745  81e7ff0f0000          and       edi,0FFFh
  0048d74b  3bfb                  cmp       edi,ebx
  0048d74d  7f15                  jg        short 0048D764h
  0048d74f  66014e32              add       [esi+32h],cx
  0048d753  81c2c0000000          add       edx,0C0h
  0048d759  c1e207                shl       edx,7
  0048d75c  89565c                mov       [esi+5Ch],edx
  0048d75f  e9c3000000            jmp       0048D827h

LAB_0048d764:
  0048d764  81e1ff0f0000          and       ecx,0FFFh
  0048d76a  81f900080000          cmp       ecx,800h
  0048d770  7c02                  jl        short 0048D774h
  0048d772  f7d8                  neg       eax

LAB_0048d774:
  0048d774  66014632              add       [esi+32h],ax
  0048d778  81c2c0000000          add       edx,0C0h
  0048d77e  c1e207                shl       edx,7
  0048d781  89565c                mov       [esi+5Ch],edx
  0048d784  e99e000000            jmp       0048D827h
  0048d789  8a4e43                mov       cl,[esi+43h]
  0048d78c  84c9                  test      cl,cl
  0048d78e  7405                  je        short 0048D795h
  0048d790  fec9                  dec       cl
  0048d792  884e43                mov       [esi+43h],cl

LAB_0048d795:
  0048d795  66397e18              cmp       [esi+18h],di
  0048d799  740a                  je        short 0048D7A5h
  0048d79b  0d00000001            or        eax,1000000h
  0048d7a0  897e18                mov       [esi+18h],edi
  0048d7a3  8906                  mov       [esi],eax

LAB_0048d7a5:
  0048d7a5  8b4c244c              mov       ecx,[esp+4Ch]
  0048d7a9  0fbf5610              movsx     edx,word [esi+10h]
  0048d7ad  81c180000000          add       ecx,80h
  0048d7b3  52                    push      edx
  0048d7b4  c1e107                shl       ecx,7
  0048d7b7  894e5c                mov       [esi+5Ch],ecx
  0048d7ba  e85d820b00            call      00545A1Ch
  0048d7bf  0faf465c              imul      eax,[esi+5Ch]
  0048d7c3  8b5e1c                mov       ebx,[esi+1Ch]
  0048d7c6  c1f80c                sar       eax,0Ch
  0048d7c9  03d8                  add       ebx,eax
  0048d7cb  0fbf4610              movsx     eax,word [esi+10h]
  0048d7cf  50                    push      eax
  0048d7d0  895e1c                mov       [esi+1Ch],ebx
  0048d7d3  e83e820b00            call      00545A16h
  0048d7d8  0faf465c              imul      eax,[esi+5Ch]
  0048d7dc  8b4e24                mov       ecx,[esi+24h]
  0048d7df  c7465c00000000        mov       dword [esi+5Ch],0
  0048d7e6  c1f80c                sar       eax,0Ch
  0048d7e9  03c8                  add       ecx,eax
  0048d7eb  894e24                mov       [esi+24h],ecx
  0048d7ee  8b4c244c              mov       ecx,[esp+4Ch]
  0048d7f2  8d048d32000000        lea       eax,[ecx*4+32h]
  0048d7f9  8b4c2410              mov       ecx,[esp+10h]
  0048d7fd  8d1408                lea       edx,[eax+ecx]
  0048d800  8d3c00                lea       edi,[eax+eax]
  0048d803  81e2ff0f0000          and       edx,0FFFh
  0048d809  3bd7                  cmp       edx,edi
  0048d80b  7f06                  jg        short 0048D813h
  0048d80d  66014e32              add       [esi+32h],cx
  0048d811  eb14                  jmp       short 0048D827h

LAB_0048d813:
  0048d813  81e1ff0f0000          and       ecx,0FFFh
  0048d819  81f900080000          cmp       ecx,800h
  0048d81f  7c02                  jl        short 0048D823h

LAB_0048d821:
  0048d821  f7d8                  neg       eax

LAB_0048d823:
  0048d823  66014632              add       [esi+32h],ax

LAB_0048d827:
  0048d827  8b44241c              mov       eax,[esp+1Ch]
  0048d82b  50                    push      eax
  0048d82c  56                    push      esi
  0048d82d  e82ea9ffff            call      00488160h
  0048d832  83c408                add       esp,8
  0048d835  56                    push      esi
  0048d836  e805610a00            call      00533940h
  0048d83b  83c404                add       esp,4
  0048d83e  56                    push      esi
  0048d83f  e8ac220500            call      004DFAF0h
  0048d844  8b4c2438              mov       ecx,[esp+38h]
  0048d848  83c404                add       esp,4
  0048d84b  51                    push      ecx
  0048d84c  56                    push      esi
  0048d84d  e81e5f0500            call      004E3770h
  0048d852  8b442424              mov       eax,[esp+24h]
  0048d856  83c408                add       esp,8
  0048d859  85c0                  test      eax,eax
  0048d85b  0f84b0020000          je        near 0048DB11h
  0048d861  f70617080000          test      dword [esi],817h
  0048d867  0f85a4020000          jne       near 0048DB11h
  0048d86d  8a4643                mov       al,[esi+43h]
  0048d870  84c0                  test      al,al
  0048d872  0f8599020000          jne       near 0048DB11h
  0048d878  8b466c                mov       eax,[esi+6Ch]
  0048d87b  8b5e1c                mov       ebx,[esi+1Ch]
  0048d87e  8b4e24                mov       ecx,[esi+24h]
  0048d881  8b781c                mov       edi,[eax+1Ch]
  0048d884  8b6824                mov       ebp,[eax+24h]
  0048d887  2bfb                  sub       edi,ebx
  0048d889  2be9                  sub       ebp,ecx
  0048d88b  c1ff0c                sar       edi,0Ch
  0048d88e  c1fd0c                sar       ebp,0Ch
  0048d891  8bd7                  mov       edx,edi
  0048d893  8bc5                  mov       eax,ebp
  0048d895  0fafd7                imul      edx,edi
  0048d898  0fafc5                imul      eax,ebp
  0048d89b  03d0                  add       edx,eax
  0048d89d  52                    push      edx
  0048d89e  e86d810b00            call      00545A10h
  0048d8a3  55                    push      ebp
  0048d8a4  57                    push      edi
  0048d8a5  89442420              mov       [esp+20h],eax
  0048d8a9  e874810b00            call      00545A22h
  0048d8ae  668b4e32              mov       cx,[esi+32h]
  0048d8b2  2bc1                  sub       eax,ecx
  0048d8b4  25ff0f0000            and       eax,0FFFh
  0048d8b9  89442410              mov       [esp+10h],eax
  0048d8bd  8b442418              mov       eax,[esp+18h]
  0048d8c1  3dfa000000            cmp       eax,0FAh
  0048d8c6  0f8dd4000000          jge       near 0048D9A0h
  0048d8cc  e86f980b00            call      00547140h
  0048d8d1  99                    cdq
  0048d8d2  b964000000            mov       ecx,64h
  0048d8d7  f7f9                  idiv      ecx
  0048d8d9  83fa1e                cmp       edx,1Eh
  0048d8dc  7d65                  jge       short 0048D943h
  0048d8de  8b542410              mov       edx,[esp+10h]
  0048d8e2  8b7c2414              mov       edi,[esp+14h]
  0048d8e6  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0048d8ed  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048d8f4  25ff0f0000            and       eax,0FFFh
  0048d8f9  3bc1                  cmp       eax,ecx
  0048d8fb  0f8fd8010000          jg        near 0048DAD9h
  0048d901  e83a980b00            call      00547140h
  0048d906  99                    cdq
  0048d907  8d4c3f01              lea       ecx,[edi+edi+1]
  0048d90b  f7f9                  idiv      ecx
  0048d90d  8b442418              mov       eax,[esp+18h]
  0048d911  83c264                add       edx,64h
  0048d914  3bc2                  cmp       eax,edx
  0048d916  0f8dbd010000          jge       near 0048DAD9h
  0048d91c  8b16                  mov       edx,[esi]
  0048d91e  c7461803000000        mov       dword [esi+18h],3
  0048d925  81e2fffffffe          and       edx,0FEFFFFFFh
  0048d92b  c6464200              mov       byte [esi+42h],0
  0048d92f  83ca02                or        edx,2
  0048d932  c7465c00000000        mov       dword [esi+5Ch],0
  0048d939  8916                  mov       [esi],edx
  0048d93b  5f                    pop       edi
  0048d93c  5e                    pop       esi
  0048d93d  5d                    pop       ebp
  0048d93e  5b                    pop       ebx
  0048d93f  83c438                add       esp,38h
  0048d942  c3                    ret

LAB_0048d943:
  0048d943  83fa3c                cmp       edx,3Ch
  0048d946  0f8c22010000          jl        near 0048DA6Eh
  0048d94c  c6464203              mov       byte [esi+42h],3
  0048d950  e8eb970b00            call      00547140h
  0048d955  99                    cdq
  0048d956  b90a000000            mov       ecx,0Ah
  0048d95b  f7f9                  idiv      ecx
  0048d95d  8b442414              mov       eax,[esp+14h]
  0048d961  8b4c244c              mov       ecx,[esp+4Ch]
  0048d965  d1f8                  sar       eax,1
  0048d967  8db980000000          lea       edi,[ecx+80h]
  0048d96d  c1e704                shl       edi,4
  0048d970  02d0                  add       dl,al
  0048d972  80c214                add       dl,14h
  0048d975  885643                mov       [esi+43h],dl
  0048d978  e8c3970b00            call      00547140h
  0048d97d  99                    cdq
  0048d97e  f7ff                  idiv      edi
  0048d980  d1ff                  sar       edi,1
  0048d982  2bd7                  sub       edx,edi
  0048d984  8b7c2410              mov       edi,[esp+10h]
  0048d988  03d7                  add       edx,edi
  0048d98a  66035632              add       dx,[esi+32h]
  0048d98e  81c200080000          add       edx,800h
  0048d994  66895610              mov       [esi+10h],dx
  0048d998  5f                    pop       edi
  0048d999  5e                    pop       esi
  0048d99a  5d                    pop       ebp
  0048d99b  5b                    pop       ebx
  0048d99c  83c438                add       esp,38h
  0048d99f  c3                    ret

LAB_0048d9a0:
  0048d9a0  3df4010000            cmp       eax,1F4h
  0048d9a5  0f8df0000000          jge       near 0048DA9Bh
  0048d9ab  e890970b00            call      00547140h
  0048d9b0  99                    cdq
  0048d9b1  b964000000            mov       ecx,64h
  0048d9b6  f7f9                  idiv      ecx
  0048d9b8  83fa2d                cmp       edx,2Dh
  0048d9bb  7d59                  jge       short 0048DA16h
  0048d9bd  8b542410              mov       edx,[esp+10h]
  0048d9c1  81c200040000          add       edx,400h
  0048d9c7  81e2ff0f0000          and       edx,0FFFh
  0048d9cd  81fa00080000          cmp       edx,800h
  0048d9d3  7c10                  jl        short 0048D9E5h
  0048d9d5  c6464201              mov       byte [esi+42h],1
  0048d9d9  c6464301              mov       byte [esi+43h],1
  0048d9dd  5f                    pop       edi
  0048d9de  5e                    pop       esi
  0048d9df  5d                    pop       ebp
  0048d9e0  5b                    pop       ebx
  0048d9e1  83c438                add       esp,38h
  0048d9e4  c3                    ret

LAB_0048d9e5:
  0048d9e5  8b06                  mov       eax,[esi]
  0048d9e7  8b4c2414              mov       ecx,[esp+14h]
  0048d9eb  25fffffffe            and       eax,0FEFFFFFFh
  0048d9f0  c7461804000000        mov       dword [esi+18h],4
  0048d9f7  c1f908                sar       ecx,8
  0048d9fa  0c02                  or        al,2
  0048d9fc  80c108                add       cl,8
  0048d9ff  8906                  mov       [esi],eax
  0048da01  c6464201              mov       byte [esi+42h],1
  0048da05  884e43                mov       [esi+43h],cl
  0048da08  66c746100200          mov       word [esi+10h],2
  0048da0e  5f                    pop       edi
  0048da0f  5e                    pop       esi
  0048da10  5d                    pop       ebp
  0048da11  5b                    pop       ebx
  0048da12  83c438                add       esp,38h
  0048da15  c3                    ret

LAB_0048da16:
  0048da16  83fa46                cmp       edx,46h
  0048da19  7d4e                  jge       short 0048DA69h
  0048da1b  c6464203              mov       byte [esi+42h],3
  0048da1f  e81c970b00            call      00547140h
  0048da24  99                    cdq
  0048da25  b90a000000            mov       ecx,0Ah
  0048da2a  f7f9                  idiv      ecx
  0048da2c  8b442414              mov       eax,[esp+14h]
  0048da30  8b4c244c              mov       ecx,[esp+4Ch]
  0048da34  d1f8                  sar       eax,1
  0048da36  8db980000000          lea       edi,[ecx+80h]
  0048da3c  c1e704                shl       edi,4
  0048da3f  02d0                  add       dl,al
  0048da41  80c214                add       dl,14h
  0048da44  885643                mov       [esi+43h],dl
  0048da47  e8f4960b00            call      00547140h
  0048da4c  99                    cdq
  0048da4d  f7ff                  idiv      edi
  0048da4f  8b4c2410              mov       ecx,[esp+10h]
  0048da53  d1ff                  sar       edi,1
  0048da55  2bd7                  sub       edx,edi
  0048da57  03d1                  add       edx,ecx
  0048da59  66035632              add       dx,[esi+32h]
  0048da5d  66895610              mov       [esi+10h],dx
  0048da61  5f                    pop       edi
  0048da62  5e                    pop       esi
  0048da63  5d                    pop       ebp
  0048da64  5b                    pop       ebx
  0048da65  83c438                add       esp,38h
  0048da68  c3                    ret

LAB_0048da69:
  0048da69  83fa5a                cmp       edx,5Ah
  0048da6c  7d3f                  jge       short 0048DAADh

LAB_0048da6e:
  0048da6e  c6464202              mov       byte [esi+42h],2
  0048da72  e8c9960b00            call      00547140h
  0048da77  99                    cdq
  0048da78  b90a000000            mov       ecx,0Ah
  0048da7d  66c746106400          mov       word [esi+10h],64h
  0048da83  f7f9                  idiv      ecx
  0048da85  8b442414              mov       eax,[esp+14h]
  0048da89  d1f8                  sar       eax,1
  0048da8b  02d0                  add       dl,al
  0048da8d  80c214                add       dl,14h
  0048da90  885643                mov       [esi+43h],dl
  0048da93  5f                    pop       edi
  0048da94  5e                    pop       esi
  0048da95  5d                    pop       ebp
  0048da96  5b                    pop       ebx
  0048da97  83c438                add       esp,38h
  0048da9a  c3                    ret

LAB_0048da9b:
  0048da9b  e8a0960b00            call      00547140h
  0048daa0  99                    cdq
  0048daa1  b964000000            mov       ecx,64h
  0048daa6  f7f9                  idiv      ecx
  0048daa8  83fa1e                cmp       edx,1Eh
  0048daab  7d27                  jge       short 0048DAD4h

LAB_0048daad:
  0048daad  c6464200              mov       byte [esi+42h],0
  0048dab1  e88a960b00            call      00547140h
  0048dab6  99                    cdq
  0048dab7  b90a000000            mov       ecx,0Ah
  0048dabc  f7f9                  idiv      ecx
  0048dabe  8b442414              mov       eax,[esp+14h]
  0048dac2  d1f8                  sar       eax,1
  0048dac4  02d0                  add       dl,al
  0048dac6  80c214                add       dl,14h
  0048dac9  885643                mov       [esi+43h],dl
  0048dacc  5f                    pop       edi
  0048dacd  5e                    pop       esi
  0048dace  5d                    pop       ebp
  0048dacf  5b                    pop       ebx
  0048dad0  83c438                add       esp,38h
  0048dad3  c3                    ret

LAB_0048dad4:
  0048dad4  83fa3c                cmp       edx,3Ch
  0048dad7  7d10                  jge       short 0048DAE9h

LAB_0048dad9:
  0048dad9  c6464201              mov       byte [esi+42h],1
  0048dadd  c6464301              mov       byte [esi+43h],1
  0048dae1  5f                    pop       edi
  0048dae2  5e                    pop       esi
  0048dae3  5d                    pop       ebp
  0048dae4  5b                    pop       ebx
  0048dae5  83c438                add       esp,38h
  0048dae8  c3                    ret

LAB_0048dae9:
  0048dae9  c6464203              mov       byte [esi+42h],3
  0048daed  e84e960b00            call      00547140h
  0048daf2  99                    cdq
  0048daf3  b90a000000            mov       ecx,0Ah
  0048daf8  f7f9                  idiv      ecx
  0048dafa  8b442414              mov       eax,[esp+14h]
  0048dafe  d1f8                  sar       eax,1
  0048db00  02d0                  add       dl,al
  0048db02  80c214                add       dl,14h
  0048db05  885643                mov       [esi+43h],dl
  0048db08  e833960b00            call      00547140h
  0048db0d  66894610              mov       [esi+10h],ax

LAB_0048db11:
  0048db11  5f                    pop       edi
  0048db12  5e                    pop       esi
  0048db13  5d                    pop       ebp
  0048db14  5b                    pop       ebx
  0048db15  83c438                add       esp,38h
  0048db18  c3                    ret
  0048db19  90                    nop
  0048db1a  8bff                  mov       edi,edi
  0048db1c  65d6                  salc
  0048db1e  48                    dec       eax
  0048db1f  009ed6480001          add       [esi+10048D6h],bl
  0048db25  d7                    xlatb
  0048db26  48                    dec       eax
  0048db27  0089d7480090          add       [ecx-6FFFB729h],cl
  0048db2d  90                    nop
  0048db2e  90                    nop
  0048db2f  90                    nop

41 cible(s) de saut interne : 0x0048d3ba, 0x0048d402, 0x0048d429, 0x0048d440, 0x0048d491, 0x0048d4aa, 0x0048d4d3, 0x0048d572, 0x0048d5c1, 0x0048d620, 0x0048d63d, 0x0048d677, 0x0048d68a, 0x0048d696, 0x0048d6ae, 0x0048d6e1, 0x0048d6f1, 0x0048d6f5, 0x0048d711, 0x0048d724, 0x0048d730, 0x0048d764, 0x0048d774, 0x0048d795, 0x0048d7a5, 0x0048d813, 0x0048d821, 0x0048d823, 0x0048d827, 0x0048d943, 0x0048d9a0, 0x0048d9e5, 0x0048da16, 0x0048da69, 0x0048da6e, 0x0048da9b, 0x0048daad, 0x0048dad4, 0x0048dad9, 0x0048dae9, 0x0048db11
