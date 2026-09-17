Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c4f90  section .text  offset fichier 0xc4390  2720 octets

  004c4f90  83ec28                sub       esp,28h
  004c4f93  8d442408              lea       eax,[esp+8]
  004c4f97  8d4c2420              lea       ecx,[esp+20h]
  004c4f9b  8d54241c              lea       edx,[esp+1Ch]
  004c4f9f  53                    push      ebx
  004c4fa0  55                    push      ebp
  004c4fa1  56                    push      esi
  004c4fa2  8b742438              mov       esi,[esp+38h]
  004c4fa6  57                    push      edi
  004c4fa7  50                    push      eax
  004c4fa8  51                    push      ecx
  004c4fa9  52                    push      edx
  004c4faa  56                    push      esi
  004c4fab  e8d0390700            call      00538980h
  004c4fb0  83c410                add       esp,10h
  004c4fb3  8d44241c              lea       eax,[esp+1Ch]
  004c4fb7  8d4c2414              lea       ecx,[esp+14h]
  004c4fbb  8d54243c              lea       edx,[esp+3Ch]
  004c4fbf  50                    push      eax
  004c4fc0  51                    push      ecx
  004c4fc1  8d442418              lea       eax,[esp+18h]
  004c4fc5  52                    push      edx
  004c4fc6  8d4c2440              lea       ecx,[esp+40h]
  004c4fca  50                    push      eax
  004c4fcb  51                    push      ecx
  004c4fcc  56                    push      esi
  004c4fcd  e84e0f0700            call      00535F20h
  004c4fd2  8b44242c              mov       eax,[esp+2Ch]
  004c4fd6  83c418                add       esp,18h
  004c4fd9  85c0                  test      eax,eax
  004c4fdb  0f8400060000          je        near 004C55E1h
  004c4fe1  56                    push      esi
  004c4fe2  e8b9650100            call      004DB5A0h
  004c4fe7  8b06                  mov       eax,[esi]
  004c4fe9  83c404                add       esp,4
  004c4fec  a802                  test      al,2
  004c4fee  0f847a050000          je        near 004C556Eh
  004c4ff4  8a4c241c              mov       cl,[esp+1Ch]
  004c4ff8  bb10000000            mov       ebx,10h
  004c4ffd  84cb                  test      bl,cl
  004c4fff  740d                  je        short 004C500Eh
  004c5001  24fd                  and       al,0FDh
  004c5003  c6464300              mov       byte [esi+43h],0
  004c5007  8906                  mov       [esi],eax
  004c5009  e9d3050000            jmp       004C55E1h

LAB_004c500e:
  004c500e  33c9                  xor       ecx,ecx
  004c5010  8a4e42                mov       cl,[esi+42h]
  004c5013  83f903                cmp       ecx,3
  004c5016  0f87c5050000          ja        near 004C55E1h
  004c501c  ff248d0c5a4c00        jmp       dword [ecx*4+4C5A0Ch]
  004c5023  668b4e1a              mov       cx,[esi+1Ah]
  004c5027  6683f906              cmp       cx,6
  004c502b  7f34                  jg        short 004C5061h
  004c502d  8b44243c              mov       eax,[esp+3Ch]
  004c5031  8d5040                lea       edx,[eax+40h]
  004c5034  81e2ff0f0000          and       edx,0FFFh
  004c503a  81fa80000000          cmp       edx,80h
  004c5040  7e1b                  jle       short 004C505Dh
  004c5042  25ff0f0000            and       eax,0FFFh
  004c5047  33d2                  xor       edx,edx
  004c5049  3d00080000            cmp       eax,800h
  004c504e  0f9dc2                setge     dl
  004c5051  4a                    dec       edx
  004c5052  81e280000000          and       edx,80h
  004c5058  83c2c0                add       edx,0FFFFFFC0h
  004c505b  8bc2                  mov       eax,edx

LAB_004c505d:
  004c505d  66014632              add       [esi+32h],ax

LAB_004c5061:
  004c5061  6683f90c              cmp       cx,0Ch
  004c5065  7558                  jne       short 004C50BFh
  004c5067  0fbf4632              movsx     eax,word [esi+32h]
  004c506b  50                    push      eax
  004c506c  c7465c00800100        mov       dword [esi+5Ch],18000h
  004c5073  e8a4090800            call      00545A1Ch
  004c5078  0faf465c              imul      eax,[esi+5Ch]
  004c507c  0fbf4e32              movsx     ecx,word [esi+32h]
  004c5080  8b6e50                mov       ebp,[esi+50h]
  004c5083  51                    push      ecx
  004c5084  c1f80c                sar       eax,0Ch
  004c5087  03e8                  add       ebp,eax
  004c5089  896e50                mov       [esi+50h],ebp
  004c508c  e885090800            call      00545A16h
  004c5091  0faf465c              imul      eax,[esi+5Ch]
  004c5095  8b4e58                mov       ecx,[esi+58h]
  004c5098  8d561c                lea       edx,[esi+1Ch]
  004c509b  c1f80c                sar       eax,0Ch
  004c509e  52                    push      edx
  004c509f  6800180000            push      1800h
  004c50a4  03c8                  add       ecx,eax
  004c50a6  6802030000            push      302h
  004c50ab  6a24                  push      24h
  004c50ad  894e58                mov       [esi+58h],ecx
  004c50b0  c7465c00000000        mov       dword [esi+5Ch],0
  004c50b7  e8648a0700            call      0053DB20h
  004c50bc  83c410                add       esp,10h

LAB_004c50bf:
  004c50bf  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004c50c4  7c71                  jl        short 004C5137h
  004c50c6  6a00                  push      0
  004c50c8  8d461c                lea       eax,[esi+1Ch]
  004c50cb  68882b5500            push      552B88h
  004c50d0  50                    push      eax
  004c50d1  56                    push      esi
  004c50d2  e8594b0500            call      00519C30h
  004c50d7  8bf8                  mov       edi,eax
  004c50d9  a1b8e05d00            mov       eax,[5DE0B8h]
  004c50de  83c410                add       esp,10h
  004c50e1  85c0                  test      eax,eax
  004c50e3  7452                  je        short 004C5137h
  004c50e5  8b2f                  mov       ebp,[edi]
  004c50e7  57                    push      edi
  004c50e8  83cd40                or        ebp,40h
  004c50eb  56                    push      esi
  004c50ec  66899f96000000        mov       [edi+96h],bx
  004c50f3  892f                  mov       [edi],ebp
  004c50f5  e8864a0700            call      00539B80h
  004c50fa  83c408                add       esp,8
  004c50fd  e83e200800            call      00547140h
  004c5102  99                    cdq
  004c5103  b964000000            mov       ecx,64h
  004c5108  f7f9                  idiv      ecx
  004c510a  83fa1e                cmp       edx,1Eh
  004c510d  7d10                  jge       short 004C511Fh
  004c510f  6a00                  push      0
  004c5111  57                    push      edi
  004c5112  56                    push      esi
  004c5113  c6460707              mov       byte [esi+7],7
  004c5117  e804730700            call      0053C420h
  004c511c  83c40c                add       esp,0Ch

LAB_004c511f:
  004c511f  83c71c                add       edi,1Ch
  004c5122  57                    push      edi
  004c5123  6800180000            push      1800h
  004c5128  6800030000            push      300h
  004c512d  6a03                  push      3
  004c512f  e8ec890700            call      0053DB20h
  004c5134  83c410                add       esp,10h

LAB_004c5137:
  004c5137  f70600000002          test      dword [esi],2000000h
  004c513d  0f847c010000          je        near 004C52BFh
  004c5143  e8f81f0800            call      00547140h
  004c5148  a801                  test      al,1
  004c514a  7412                  je        short 004C515Eh
  004c514c  8b06                  mov       eax,[esi]
  004c514e  c7465c00000000        mov       dword [esi+5Ch],0
  004c5155  24fd                  and       al,0FDh
  004c5157  8906                  mov       [esi],eax
  004c5159  e983040000            jmp       004C55E1h

LAB_004c515e:
  004c515e  c7461806000000        mov       dword [esi+18h],6
  004c5165  c6464201              mov       byte [esi+42h],1
  004c5169  c7465c00000000        mov       dword [esi+5Ch],0
  004c5170  e96c040000            jmp       004C55E1h
  004c5175  668b4e1a              mov       cx,[esi+1Ah]
  004c5179  6683f906              cmp       cx,6
  004c517d  7f34                  jg        short 004C51B3h
  004c517f  8b44243c              mov       eax,[esp+3Ch]
  004c5183  8d5040                lea       edx,[eax+40h]
  004c5186  81e2ff0f0000          and       edx,0FFFh
  004c518c  81fa80000000          cmp       edx,80h
  004c5192  7e1b                  jle       short 004C51AFh
  004c5194  25ff0f0000            and       eax,0FFFh
  004c5199  33d2                  xor       edx,edx
  004c519b  3d00080000            cmp       eax,800h
  004c51a0  0f9dc2                setge     dl
  004c51a3  4a                    dec       edx
  004c51a4  81e280000000          and       edx,80h
  004c51aa  83c2c0                add       edx,0FFFFFFC0h
  004c51ad  8bc2                  mov       eax,edx

LAB_004c51af:
  004c51af  66014632              add       [esi+32h],ax

LAB_004c51b3:
  004c51b3  6683f90c              cmp       cx,0Ch
  004c51b7  757c                  jne       short 004C5235h
  004c51b9  0fbf4632              movsx     eax,word [esi+32h]
  004c51bd  50                    push      eax
  004c51be  c7465c00800100        mov       dword [esi+5Ch],18000h
  004c51c5  e852080800            call      00545A1Ch
  004c51ca  0faf465c              imul      eax,[esi+5Ch]
  004c51ce  0fbf4e32              movsx     ecx,word [esi+32h]
  004c51d2  8b6e50                mov       ebp,[esi+50h]
  004c51d5  51                    push      ecx
  004c51d6  c1f80c                sar       eax,0Ch
  004c51d9  03e8                  add       ebp,eax
  004c51db  896e50                mov       [esi+50h],ebp
  004c51de  e833080800            call      00545A16h
  004c51e3  0faf465c              imul      eax,[esi+5Ch]
  004c51e7  8b4e58                mov       ecx,[esi+58h]
  004c51ea  c7465c00000000        mov       dword [esi+5Ch],0
  004c51f1  c1f80c                sar       eax,0Ch
  004c51f4  03c8                  add       ecx,eax
  004c51f6  894e58                mov       [esi+58h],ecx
  004c51f9  e8421f0800            call      00547140h
  004c51fe  99                    cdq
  004c51ff  b964000000            mov       ecx,64h
  004c5204  f7f9                  idiv      ecx
  004c5206  83fa1e                cmp       edx,1Eh
  004c5209  7d12                  jge       short 004C521Dh
  004c520b  0fbf5614              movsx     edx,word [esi+14h]
  004c520f  6aff                  push      0FFFFFFFFh
  004c5211  6aff                  push      0FFFFFFFFh
  004c5213  52                    push      edx
  004c5214  56                    push      esi
  004c5215  e8b657fbff            call      0047A9D0h
  004c521a  83c410                add       esp,10h

LAB_004c521d:
  004c521d  8d461c                lea       eax,[esi+1Ch]
  004c5220  50                    push      eax
  004c5221  6800180000            push      1800h
  004c5226  6802030000            push      302h
  004c522b  6a24                  push      24h
  004c522d  e8ee880700            call      0053DB20h
  004c5232  83c410                add       esp,10h

LAB_004c5235:
  004c5235  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004c523a  7c4f                  jl        short 004C528Bh
  004c523c  6a00                  push      0
  004c523e  8d4e1c                lea       ecx,[esi+1Ch]
  004c5241  68942b5500            push      552B94h
  004c5246  51                    push      ecx
  004c5247  56                    push      esi
  004c5248  e8e3490500            call      00519C30h
  004c524d  8bf8                  mov       edi,eax
  004c524f  a1b8e05d00            mov       eax,[5DE0B8h]
  004c5254  83c410                add       esp,10h
  004c5257  85c0                  test      eax,eax
  004c5259  7430                  je        short 004C528Bh
  004c525b  8b17                  mov       edx,[edi]
  004c525d  57                    push      edi
  004c525e  83ca40                or        edx,40h
  004c5261  56                    push      esi
  004c5262  66899f96000000        mov       [edi+96h],bx
  004c5269  8917                  mov       [edi],edx
  004c526b  e810490700            call      00539B80h
  004c5270  83c408                add       esp,8
  004c5273  83c71c                add       edi,1Ch
  004c5276  57                    push      edi
  004c5277  6800180000            push      1800h
  004c527c  6800030000            push      300h
  004c5281  6a03                  push      3
  004c5283  e898880700            call      0053DB20h
  004c5288  83c410                add       esp,10h

LAB_004c528b:
  004c528b  f70600000002          test      dword [esi],2000000h
  004c5291  742c                  je        short 004C52BFh
  004c5293  e8a81e0800            call      00547140h
  004c5298  a801                  test      al,1
  004c529a  7412                  je        short 004C52AEh
  004c529c  8b06                  mov       eax,[esi]
  004c529e  c7465c00000000        mov       dword [esi+5Ch],0
  004c52a5  24fd                  and       al,0FDh
  004c52a7  8906                  mov       [esi],eax
  004c52a9  e933030000            jmp       004C55E1h

LAB_004c52ae:
  004c52ae  c7461807000000        mov       dword [esi+18h],7
  004c52b5  c6464202              mov       byte [esi+42h],2
  004c52b9  66c746100200          mov       word [esi+10h],2

LAB_004c52bf:
  004c52bf  c7465c00000000        mov       dword [esi+5Ch],0
  004c52c6  e916030000            jmp       004C55E1h
  004c52cb  668b5610              mov       dx,[esi+10h]
  004c52cf  0fbfca                movsx     ecx,dx
  004c52d2  83e900                sub       ecx,0
  004c52d5  0f84cb000000          je        near 004C53A6h
  004c52db  49                    dec       ecx
  004c52dc  7462                  je        short 004C5340h
  004c52de  49                    dec       ecx
  004c52df  0f85fc020000          jne       near 004C55E1h
  004c52e5  8b465c                mov       eax,[esi+5Ch]
  004c52e8  8bc8                  mov       ecx,eax
  004c52ea  c1f902                sar       ecx,2
  004c52ed  2bc1                  sub       eax,ecx
  004c52ef  66837e1a09            cmp       word [esi+1Ah],9
  004c52f4  89465c                mov       [esi+5Ch],eax
  004c52f7  7505                  jne       short 004C52FEh
  004c52f9  4a                    dec       edx
  004c52fa  66895610              mov       [esi+10h],dx

LAB_004c52fe:
  004c52fe  8b44243c              mov       eax,[esp+3Ch]
  004c5302  8d5040                lea       edx,[eax+40h]
  004c5305  81e2ff0f0000          and       edx,0FFFh
  004c530b  81fa80000000          cmp       edx,80h
  004c5311  7f09                  jg        short 004C531Ch
  004c5313  66014632              add       [esi+32h],ax
  004c5317  e9c5020000            jmp       004C55E1h

LAB_004c531c:
  004c531c  25ff0f0000            and       eax,0FFFh
  004c5321  33c9                  xor       ecx,ecx
  004c5323  3d00080000            cmp       eax,800h
  004c5328  0f9dc1                setge     cl
  004c532b  49                    dec       ecx
  004c532c  81e180000000          and       ecx,80h
  004c5332  83c1c0                add       ecx,0FFFFFFC0h
  004c5335  8bc1                  mov       eax,ecx
  004c5337  66014632              add       [esi+32h],ax
  004c533b  e9a1020000            jmp       004C55E1h

LAB_004c5340:
  004c5340  0fbf5632              movsx     edx,word [esi+32h]
  004c5344  52                    push      edx
  004c5345  c7465c00e00100        mov       dword [esi+5Ch],1E000h
  004c534c  e8cb060800            call      00545A1Ch
  004c5351  0faf465c              imul      eax,[esi+5Ch]
  004c5355  8b5e50                mov       ebx,[esi+50h]
  004c5358  c1f80c                sar       eax,0Ch
  004c535b  03d8                  add       ebx,eax
  004c535d  0fbf4632              movsx     eax,word [esi+32h]
  004c5361  50                    push      eax
  004c5362  895e50                mov       [esi+50h],ebx
  004c5365  e8ac060800            call      00545A16h
  004c536a  0faf465c              imul      eax,[esi+5Ch]
  004c536e  8b4e58                mov       ecx,[esi+58h]
  004c5371  c7465400200300        mov       dword [esi+54h],32000h
  004c5378  c1f80c                sar       eax,0Ch
  004c537b  03c8                  add       ecx,eax
  004c537d  8b06                  mov       eax,[esi]
  004c537f  894e58                mov       [esi+58h],ecx
  004c5382  8d4e1c                lea       ecx,[esi+1Ch]
  004c5385  51                    push      ecx
  004c5386  0c01                  or        al,1
  004c5388  66ff4e10              dec       word [esi+10h]
  004c538c  6800180000            push      1800h
  004c5391  68fa0d0000            push      0DFAh
  004c5396  8906                  mov       [esi],eax
  004c5398  c7465c00000000        mov       dword [esi+5Ch],0
  004c539f  6a2e                  push      2Eh
  004c53a1  e933020000            jmp       004C55D9h

LAB_004c53a6:
  004c53a6  a900000002            test      eax,2000000h
  004c53ab  7408                  je        short 004C53B5h
  004c53ad  a801                  test      al,1
  004c53af  7504                  jne       short 004C53B5h
  004c53b1  24dd                  and       al,0DDh
  004c53b3  8906                  mov       [esi],eax

LAB_004c53b5:
  004c53b5  f60620                test      byte [esi],20h
  004c53b8  0f8523020000          jne       near 004C55E1h
  004c53be  8b4654                mov       eax,[esi+54h]
  004c53c1  85c0                  test      eax,eax
  004c53c3  0f8e18020000          jle       near 004C55E1h
  004c53c9  8d6e1c                lea       ebp,[esi+1Ch]
  004c53cc  6a00                  push      0
  004c53ce  68a02b5500            push      552BA0h
  004c53d3  55                    push      ebp
  004c53d4  56                    push      esi
  004c53d5  e856480500            call      00519C30h
  004c53da  8bf8                  mov       edi,eax
  004c53dc  83c410                add       esp,10h
  004c53df  85ff                  test      edi,edi
  004c53e1  7406                  je        short 004C53E9h
  004c53e3  8b06                  mov       eax,[esi]
  004c53e5  0c20                  or        al,20h
  004c53e7  8906                  mov       [esi],eax

LAB_004c53e9:
  004c53e9  a1b8e05d00            mov       eax,[5DE0B8h]
  004c53ee  85c0                  test      eax,eax
  004c53f0  0f84eb010000          je        near 004C55E1h
  004c53f6  66899f96000000        mov       [edi+96h],bx
  004c53fd  8b1f                  mov       ebx,[edi]
  004c53ff  83cb40                or        ebx,40h
  004c5402  57                    push      edi
  004c5403  891f                  mov       [edi],ebx
  004c5405  8b4c241c              mov       ecx,[esp+1Ch]
  004c5409  56                    push      esi
  004c540a  668b4130              mov       ax,[ecx+30h]
  004c540e  0fbfd8                movsx     ebx,ax
  004c5411  66d1f8                sar       ax,1
  004c5414  66894130              mov       [ecx+30h],ax
  004c5418  e863470700            call      00539B80h
  004c541d  8b542420              mov       edx,[esp+20h]
  004c5421  83c408                add       esp,8
  004c5424  66895a30              mov       [edx+30h],bx
  004c5428  8d5f1c                lea       ebx,[edi+1Ch]
  004c542b  53                    push      ebx
  004c542c  6800180000            push      1800h
  004c5431  6800030000            push      300h
  004c5436  6a03                  push      3
  004c5438  e8e3860700            call      0053DB20h
  004c543d  8b07                  mov       eax,[edi]
  004c543f  83c410                add       esp,10h
  004c5442  a900000040            test      eax,40000000h
  004c5447  0f8594010000          jne       near 004C55E1h
  004c544d  0fbf4632              movsx     eax,word [esi+32h]
  004c5451  50                    push      eax
  004c5452  e8c5050800            call      00545A1Ch
  004c5457  8d0440                lea       eax,[eax+eax*2]
  004c545a  8d0c80                lea       ecx,[eax+eax*4]
  004c545d  d1e1                  shl       ecx,1
  004c545f  894f50                mov       [edi+50h],ecx
  004c5462  0fbf5632              movsx     edx,word [esi+32h]
  004c5466  52                    push      edx
  004c5467  e8aa050800            call      00545A16h
  004c546c  8d0440                lea       eax,[eax+eax*2]
  004c546f  8d0480                lea       eax,[eax+eax*4]
  004c5472  d1e0                  shl       eax,1
  004c5474  894758                mov       [edi+58h],eax
  004c5477  8b07                  mov       eax,[edi]
  004c5479  a801                  test      al,1
  004c547b  750b                  jne       short 004C5488h
  004c547d  0c01                  or        al,1
  004c547f  c7475400800100        mov       dword [edi+54h],18000h
  004c5486  8907                  mov       [edi],eax

LAB_004c5488:
  004c5488  8b4d00                mov       ecx,[ebp]
  004c548b  8b5620                mov       edx,[esi+20h]
  004c548e  8b4624                mov       eax,[esi+24h]
  004c5491  894c2420              mov       [esp+20h],ecx
  004c5495  8b0b                  mov       ecx,[ebx]
  004c5497  89542424              mov       [esp+24h],edx
  004c549b  894d00                mov       [ebp],ecx
  004c549e  8b5720                mov       edx,[edi+20h]
  004c54a1  6aff                  push      0FFFFFFFFh
  004c54a3  8944242c              mov       [esp+2Ch],eax
  004c54a7  895620                mov       [esi+20h],edx
  004c54aa  8b4724                mov       eax,[edi+24h]
  004c54ad  6a00                  push      0
  004c54af  6a39                  push      39h
  004c54b1  56                    push      esi
  004c54b2  894624                mov       [esi+24h],eax
  004c54b5  e81655fbff            call      0047A9D0h
  004c54ba  8b4c2430              mov       ecx,[esp+30h]
  004c54be  8b542434              mov       edx,[esp+34h]
  004c54c2  8b442438              mov       eax,[esp+38h]
  004c54c6  83c410                add       esp,10h
  004c54c9  894d00                mov       [ebp],ecx
  004c54cc  895620                mov       [esi+20h],edx
  004c54cf  894624                mov       [esi+24h],eax
  004c54d2  e90a010000            jmp       004C55E1h
  004c54d7  0fbf4e10              movsx     ecx,word [esi+10h]
  004c54db  33db                  xor       ebx,ebx
  004c54dd  2bcb                  sub       ecx,ebx
  004c54df  747a                  je        short 004C555Bh
  004c54e1  49                    dec       ecx
  004c54e2  0f8581000000          jne       near 004C5569h
  004c54e8  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004c54ed  7518                  jne       short 004C5507h
  004c54ef  8d4e1c                lea       ecx,[esi+1Ch]
  004c54f2  51                    push      ecx
  004c54f3  6800180000            push      1800h
  004c54f8  682a030000            push      32Ah
  004c54fd  6a15                  push      15h
  004c54ff  e81c860700            call      0053DB20h
  004c5504  83c410                add       esp,10h

LAB_004c5507:
  004c5507  f70600000002          test      dword [esi],2000000h
  004c550d  745a                  je        short 004C5569h
  004c550f  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004c5516  66899e9a000000        mov       [esi+9Ah],bx
  004c551d  8b15cc975e00          mov       edx,[5E97CCh]
  004c5523  6aff                  push      0FFFFFFFFh
  004c5525  6aff                  push      0FFFFFFFFh
  004c5527  8b82cc000000          mov       eax,[edx+0CCh]
  004c552d  33d2                  xor       edx,edx
  004c552f  8b08                  mov       ecx,[eax]
  004c5531  8a5109                mov       dl,[ecx+9]
  004c5534  81c2c0000000          add       edx,0C0h
  004c553a  52                    push      edx
  004c553b  56                    push      esi
  004c553c  e88f54fbff            call      0047A9D0h
  004c5541  83c410                add       esp,10h
  004c5544  66ff4e10              dec       word [esi+10h]
  004c5548  6689be9a000000        mov       [esi+9Ah],di
  004c554f  c646430a              mov       byte [esi+43h],0Ah
  004c5553  895e5c                mov       [esi+5Ch],ebx
  004c5556  e986000000            jmp       004C55E1h

LAB_004c555b:
  004c555b  8a4e43                mov       cl,[esi+43h]
  004c555e  fec9                  dec       cl
  004c5560  884e43                mov       [esi+43h],cl
  004c5563  7504                  jne       short 004C5569h
  004c5565  24fd                  and       al,0FDh
  004c5567  8906                  mov       [esi],eax

LAB_004c5569:
  004c5569  895e5c                mov       [esi+5Ch],ebx
  004c556c  eb73                  jmp       short 004C55E1h

LAB_004c556e:
  004c556e  a811                  test      al,11h
  004c5570  756f                  jne       short 004C55E1h
  004c5572  a904080000            test      eax,804h
  004c5577  7568                  jne       short 004C55E1h
  004c5579  33c9                  xor       ecx,ecx
  004c557b  8a4e42                mov       cl,[esi+42h]
  004c557e  83f903                cmp       ecx,3
  004c5581  772b                  ja        short 004C55AEh
  004c5583  ff248d1c5a4c00        jmp       dword [ecx*4+4C5A1Ch]
  004c558a  33c9                  xor       ecx,ecx
  004c558c  66394e18              cmp       [esi+18h],cx
  004c5590  740a                  je        short 004C559Ch
  004c5592  0d00000001            or        eax,1000000h
  004c5597  894e18                mov       [esi+18h],ecx
  004c559a  8906                  mov       [esi],eax

LAB_004c559c:
  004c559c  817c24102c010000      cmp       dword [esp+10h],12Ch
  004c55a4  0f8dc4020000          jge       near 004C586Eh
  004c55aa  c6464300              mov       byte [esi+43h],0

LAB_004c55ae:
  004c55ae  bf01000000            mov       edi,1

LAB_004c55b3:
  004c55b3  66397e18              cmp       [esi+18h],di
  004c55b7  7528                  jne       short 004C55E1h
  004c55b9  668b461a              mov       ax,[esi+1Ah]
  004c55bd  663d0400              cmp       ax,4
  004c55c1  7406                  je        short 004C55C9h
  004c55c3  663d0900              cmp       ax,9
  004c55c7  7518                  jne       short 004C55E1h

LAB_004c55c9:
  004c55c9  8d561c                lea       edx,[esi+1Ch]
  004c55cc  52                    push      edx
  004c55cd  6800180000            push      1800h
  004c55d2  6800040000            push      400h
  004c55d7  6a47                  push      47h

LAB_004c55d9:
  004c55d9  e842850700            call      0053DB20h
  004c55de  83c410                add       esp,10h

LAB_004c55e1:
  004c55e1  8b442414              mov       eax,[esp+14h]
  004c55e5  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  004c55ee  50                    push      eax
  004c55ef  56                    push      esi
  004c55f0  e86b2bfcff            call      00488160h
  004c55f5  83c408                add       esp,8
  004c55f8  56                    push      esi
  004c55f9  e842e30600            call      00533940h
  004c55fe  83c404                add       esp,4
  004c5601  56                    push      esi
  004c5602  e8e9a40100            call      004DFAF0h
  004c5607  8b4c2430              mov       ecx,[esp+30h]
  004c560b  83c404                add       esp,4
  004c560e  51                    push      ecx
  004c560f  56                    push      esi
  004c5610  e85be10100            call      004E3770h
  004c5615  8b44241c              mov       eax,[esp+1Ch]
  004c5619  83c408                add       esp,8
  004c561c  85c0                  test      eax,eax
  004c561e  0f8442020000          je        near 004C5866h
  004c5624  f70617080000          test      dword [esi],817h
  004c562a  0f8536020000          jne       near 004C5866h
  004c5630  8a4643                mov       al,[esi+43h]
  004c5633  84c0                  test      al,al
  004c5635  0f852b020000          jne       near 004C5866h
  004c563b  8b466c                mov       eax,[esi+6Ch]
  004c563e  8b5e1c                mov       ebx,[esi+1Ch]
  004c5641  8b4e24                mov       ecx,[esi+24h]
  004c5644  8b781c                mov       edi,[eax+1Ch]
  004c5647  8b6824                mov       ebp,[eax+24h]
  004c564a  2bfb                  sub       edi,ebx
  004c564c  2be9                  sub       ebp,ecx
  004c564e  c1ff0c                sar       edi,0Ch
  004c5651  c1fd0c                sar       ebp,0Ch
  004c5654  8bd7                  mov       edx,edi
  004c5656  8bc5                  mov       eax,ebp
  004c5658  0fafd7                imul      edx,edi
  004c565b  0fafc5                imul      eax,ebp
  004c565e  03d0                  add       edx,eax
  004c5660  52                    push      edx
  004c5661  e8aa030800            call      00545A10h
  004c5666  55                    push      ebp
  004c5667  57                    push      edi
  004c5668  89442418              mov       [esp+18h],eax
  004c566c  e8b1030800            call      00545A22h
  004c5671  668b4e32              mov       cx,[esi+32h]
  004c5675  2bc1                  sub       eax,ecx
  004c5677  25ff0f0000            and       eax,0FFFh
  004c567c  8944243c              mov       [esp+3Ch],eax
  004c5680  e8bb1a0800            call      00547140h
  004c5685  bf64000000            mov       edi,64h
  004c568a  99                    cdq
  004c568b  8bcf                  mov       ecx,edi
  004c568d  f7f9                  idiv      ecx
  004c568f  817c24102c010000      cmp       dword [esp+10h],12Ch
  004c5697  0f8d1a010000          jge       near 004C57B7h
  004c569d  83fa32                cmp       edx,32h
  004c56a0  7d4e                  jge       short 004C56F0h
  004c56a2  8b44243c              mov       eax,[esp+3Ch]
  004c56a6  0500010000            add       eax,100h
  004c56ab  25ff0f0000            and       eax,0FFFh
  004c56b0  3d00020000            cmp       eax,200h
  004c56b5  7f29                  jg        short 004C56E0h
  004c56b7  f6c201                test      dl,1
  004c56ba  0f8497000000          je        near 004C5757h
  004c56c0  8b0e                  mov       ecx,[esi]
  004c56c2  c7461803000000        mov       dword [esi+18h],3
  004c56c9  81e1fffffffe          and       ecx,0FEFFFFFFh
  004c56cf  c6464200              mov       byte [esi+42h],0
  004c56d3  83c902                or        ecx,2
  004c56d6  890e                  mov       [esi],ecx
  004c56d8  5f                    pop       edi
  004c56d9  5e                    pop       esi
  004c56da  5d                    pop       ebp
  004c56db  5b                    pop       ebx
  004c56dc  83c428                add       esp,28h
  004c56df  c3                    ret

LAB_004c56e0:
  004c56e0  c6464201              mov       byte [esi+42h],1
  004c56e4  c6464301              mov       byte [esi+43h],1
  004c56e8  5f                    pop       edi
  004c56e9  5e                    pop       esi
  004c56ea  5d                    pop       ebp
  004c56eb  5b                    pop       ebx
  004c56ec  83c428                add       esp,28h
  004c56ef  c3                    ret

LAB_004c56f0:
  004c56f0  83fa4b                cmp       edx,4Bh
  004c56f3  7d23                  jge       short 004C5718h
  004c56f5  c6464202              mov       byte [esi+42h],2
  004c56f9  e8421a0800            call      00547140h
  004c56fe  99                    cdq
  004c56ff  b90a000000            mov       ecx,0Ah
  004c5704  66897e10              mov       [esi+10h],di
  004c5708  f7f9                  idiv      ecx
  004c570a  80c214                add       dl,14h
  004c570d  885643                mov       [esi+43h],dl
  004c5710  5f                    pop       edi
  004c5711  5e                    pop       esi
  004c5712  5d                    pop       ebp
  004c5713  5b                    pop       ebx
  004c5714  83c428                add       esp,28h
  004c5717  c3                    ret

LAB_004c5718:
  004c5718  83fa5a                cmp       edx,5Ah
  004c571b  7d5a                  jge       short 004C5777h
  004c571d  8b44243c              mov       eax,[esp+3Ch]
  004c5721  0500010000            add       eax,100h
  004c5726  25ff0f0000            and       eax,0FFFh
  004c572b  3d00020000            cmp       eax,200h
  004c5730  7f45                  jg        short 004C5777h
  004c5732  f6c201                test      dl,1
  004c5735  7420                  je        short 004C5757h
  004c5737  8b0e                  mov       ecx,[esi]
  004c5739  c7461803000000        mov       dword [esi+18h],3
  004c5740  81e1fffffffe          and       ecx,0FEFFFFFFh
  004c5746  c6464200              mov       byte [esi+42h],0
  004c574a  83c902                or        ecx,2
  004c574d  890e                  mov       [esi],ecx
  004c574f  5f                    pop       edi
  004c5750  5e                    pop       esi
  004c5751  5d                    pop       ebp
  004c5752  5b                    pop       ebx
  004c5753  83c428                add       esp,28h
  004c5756  c3                    ret

LAB_004c5757:
  004c5757  8b16                  mov       edx,[esi]
  004c5759  c7461806000000        mov       dword [esi+18h],6
  004c5760  81e2fffffffe          and       edx,0FEFFFFFFh
  004c5766  c6464201              mov       byte [esi+42h],1
  004c576a  83ca02                or        edx,2
  004c576d  8916                  mov       [esi],edx
  004c576f  5f                    pop       edi
  004c5770  5e                    pop       esi
  004c5771  5d                    pop       ebp
  004c5772  5b                    pop       ebx
  004c5773  83c428                add       esp,28h
  004c5776  c3                    ret

LAB_004c5777:
  004c5777  c6464203              mov       byte [esi+42h],3
  004c577b  e8c0190800            call      00547140h
  004c5780  99                    cdq
  004c5781  b90a000000            mov       ecx,0Ah
  004c5786  f7f9                  idiv      ecx
  004c5788  80c214                add       dl,14h
  004c578b  885643                mov       [esi+43h],dl
  004c578e  e8ad190800            call      00547140h
  004c5793  99                    cdq
  004c5794  b9000c0000            mov       ecx,0C00h
  004c5799  8b7c243c              mov       edi,[esp+3Ch]
  004c579d  f7f9                  idiv      ecx
  004c579f  03d7                  add       edx,edi
  004c57a1  66035632              add       dx,[esi+32h]
  004c57a5  81c200020000          add       edx,200h
  004c57ab  66895610              mov       [esi+10h],dx
  004c57af  5f                    pop       edi
  004c57b0  5e                    pop       esi
  004c57b1  5d                    pop       ebp
  004c57b2  5b                    pop       ebx
  004c57b3  83c428                add       esp,28h
  004c57b6  c3                    ret

LAB_004c57b7:
  004c57b7  83fa14                cmp       edx,14h
  004c57ba  7d26                  jge       short 004C57E2h
  004c57bc  8b16                  mov       edx,[esi]
  004c57be  c7461805000000        mov       dword [esi+18h],5
  004c57c5  81e2fffffffe          and       edx,0FEFFFFFFh
  004c57cb  c6464203              mov       byte [esi+42h],3
  004c57cf  83ca02                or        edx,2
  004c57d2  66c746100100          mov       word [esi+10h],1
  004c57d8  8916                  mov       [esi],edx
  004c57da  5f                    pop       edi
  004c57db  5e                    pop       esi
  004c57dc  5d                    pop       ebp
  004c57dd  5b                    pop       ebx
  004c57de  83c428                add       esp,28h
  004c57e1  c3                    ret

LAB_004c57e2:
  004c57e2  83fa28                cmp       edx,28h
  004c57e5  7d40                  jge       short 004C5827h
  004c57e7  c6464203              mov       byte [esi+42h],3
  004c57eb  e850190800            call      00547140h
  004c57f0  99                    cdq
  004c57f1  b90a000000            mov       ecx,0Ah
  004c57f6  f7f9                  idiv      ecx
  004c57f8  80c214                add       dl,14h
  004c57fb  885643                mov       [esi+43h],dl
  004c57fe  e83d190800            call      00547140h
  004c5803  99                    cdq
  004c5804  b9000c0000            mov       ecx,0C00h
  004c5809  8b7c243c              mov       edi,[esp+3Ch]
  004c580d  f7f9                  idiv      ecx
  004c580f  03d7                  add       edx,edi
  004c5811  66035632              add       dx,[esi+32h]
  004c5815  81ea00060000          sub       edx,600h
  004c581b  66895610              mov       [esi+10h],dx
  004c581f  5f                    pop       edi
  004c5820  5e                    pop       esi
  004c5821  5d                    pop       ebp
  004c5822  5b                    pop       ebx
  004c5823  83c428                add       esp,28h
  004c5826  c3                    ret

LAB_004c5827:
  004c5827  83fa5a                cmp       edx,5Ah
  004c582a  7d23                  jge       short 004C584Fh
  004c582c  c6464202              mov       byte [esi+42h],2
  004c5830  e80b190800            call      00547140h
  004c5835  99                    cdq
  004c5836  b90a000000            mov       ecx,0Ah
  004c583b  66897e10              mov       [esi+10h],di
  004c583f  f7f9                  idiv      ecx
  004c5841  80c214                add       dl,14h
  004c5844  885643                mov       [esi+43h],dl
  004c5847  5f                    pop       edi
  004c5848  5e                    pop       esi
  004c5849  5d                    pop       ebp
  004c584a  5b                    pop       ebx
  004c584b  83c428                add       esp,28h
  004c584e  c3                    ret

LAB_004c584f:
  004c584f  c6464200              mov       byte [esi+42h],0
  004c5853  e8e8180800            call      00547140h
  004c5858  99                    cdq
  004c5859  b90a000000            mov       ecx,0Ah
  004c585e  f7f9                  idiv      ecx
  004c5860  80c214                add       dl,14h
  004c5863  885643                mov       [esi+43h],dl

LAB_004c5866:
  004c5866  5f                    pop       edi
  004c5867  5e                    pop       esi
  004c5868  5d                    pop       ebp
  004c5869  5b                    pop       ebx
  004c586a  83c428                add       esp,28h
  004c586d  c3                    ret

LAB_004c586e:
  004c586e  8a4643                mov       al,[esi+43h]
  004c5871  84c0                  test      al,al
  004c5873  7405                  je        short 004C587Ah
  004c5875  fec8                  dec       al
  004c5877  884643                mov       [esi+43h],al

LAB_004c587a:
  004c587a  894e5c                mov       [esi+5Ch],ecx
  004c587d  e92cfdffff            jmp       004C55AEh
  004c5882  bf01000000            mov       edi,1
  004c5887  66397e18              cmp       [esi+18h],di
  004c588b  740a                  je        short 004C5897h
  004c588d  0d00000001            or        eax,1000000h
  004c5892  897e18                mov       [esi+18h],edi
  004c5895  8906                  mov       [esi],eax

LAB_004c5897:
  004c5897  8b44243c              mov       eax,[esp+3Ch]
  004c589b  8d8880000000          lea       ecx,[eax+80h]
  004c58a1  81e1ff0f0000          and       ecx,0FFFh
  004c58a7  81f900010000          cmp       ecx,100h
  004c58ad  7f14                  jg        short 004C58C3h
  004c58af  66014632              add       [esi+32h],ax
  004c58b3  c6464300              mov       byte [esi+43h],0
  004c58b7  c7465c00000000        mov       dword [esi+5Ch],0
  004c58be  e9f0fcffff            jmp       004C55B3h

LAB_004c58c3:
  004c58c3  25ff0f0000            and       eax,0FFFh
  004c58c8  33d2                  xor       edx,edx
  004c58ca  3d00080000            cmp       eax,800h
  004c58cf  c7465c00000000        mov       dword [esi+5Ch],0
  004c58d6  0f9dc2                setge     dl
  004c58d9  4a                    dec       edx
  004c58da  81e200010000          and       edx,100h
  004c58e0  83c280                add       edx,0FFFFFF80h
  004c58e3  8bc2                  mov       eax,edx
  004c58e5  66014632              add       [esi+32h],ax
  004c58e9  e9c5fcffff            jmp       004C55B3h
  004c58ee  bf01000000            mov       edi,1
  004c58f3  66397e18              cmp       [esi+18h],di
  004c58f7  740a                  je        short 004C5903h
  004c58f9  0d00000001            or        eax,1000000h
  004c58fe  897e18                mov       [esi+18h],edi
  004c5901  8906                  mov       [esi],eax

LAB_004c5903:
  004c5903  0fbf4610              movsx     eax,word [esi+10h]
  004c5907  39442410              cmp       [esp+10h],eax
  004c590b  7d09                  jge       short 004C5916h
  004c590d  c6464300              mov       byte [esi+43h],0
  004c5911  e99dfcffff            jmp       004C55B3h

LAB_004c5916:
  004c5916  8a4643                mov       al,[esi+43h]
  004c5919  84c0                  test      al,al
  004c591b  7405                  je        short 004C5922h
  004c591d  fec8                  dec       al
  004c591f  884643                mov       [esi+43h],al

LAB_004c5922:
  004c5922  8b44243c              mov       eax,[esp+3Ch]
  004c5926  8d8880000000          lea       ecx,[eax+80h]
  004c592c  81e1ff0f0000          and       ecx,0FFFh
  004c5932  81f900010000          cmp       ecx,100h
  004c5938  7e1b                  jle       short 004C5955h
  004c593a  25ff0f0000            and       eax,0FFFh
  004c593f  33d2                  xor       edx,edx
  004c5941  3d00080000            cmp       eax,800h
  004c5946  0f9dc2                setge     dl
  004c5949  4a                    dec       edx
  004c594a  81e200010000          and       edx,100h
  004c5950  83c280                add       edx,0FFFFFF80h
  004c5953  8bc2                  mov       eax,edx

LAB_004c5955:
  004c5955  66014632              add       [esi+32h],ax
  004c5959  c7465c00200100        mov       dword [esi+5Ch],12000h
  004c5960  e94efcffff            jmp       004C55B3h
  004c5965  8a4e43                mov       cl,[esi+43h]
  004c5968  84c9                  test      cl,cl
  004c596a  7405                  je        short 004C5971h
  004c596c  fec9                  dec       cl
  004c596e  884e43                mov       [esi+43h],cl

LAB_004c5971:
  004c5971  bf01000000            mov       edi,1
  004c5976  66397e18              cmp       [esi+18h],di
  004c597a  740a                  je        short 004C5986h
  004c597c  0d00000001            or        eax,1000000h
  004c5981  897e18                mov       [esi+18h],edi
  004c5984  8906                  mov       [esi],eax

LAB_004c5986:
  004c5986  0fbf4610              movsx     eax,word [esi+10h]
  004c598a  50                    push      eax
  004c598b  c7465c00e00000        mov       dword [esi+5Ch],0E000h
  004c5992  e885000800            call      00545A1Ch
  004c5997  0faf465c              imul      eax,[esi+5Ch]
  004c599b  0fbf4e10              movsx     ecx,word [esi+10h]
  004c599f  8b5e1c                mov       ebx,[esi+1Ch]
  004c59a2  51                    push      ecx
  004c59a3  c1f80c                sar       eax,0Ch
  004c59a6  03d8                  add       ebx,eax
  004c59a8  895e1c                mov       [esi+1Ch],ebx
  004c59ab  e866000800            call      00545A16h
  004c59b0  0faf465c              imul      eax,[esi+5Ch]
  004c59b4  8b6e24                mov       ebp,[esi+24h]
  004c59b7  c7465c00000000        mov       dword [esi+5Ch],0
  004c59be  c1f80c                sar       eax,0Ch
  004c59c1  03e8                  add       ebp,eax
  004c59c3  8b44243c              mov       eax,[esp+3Ch]
  004c59c7  896e24                mov       [esi+24h],ebp
  004c59ca  8d9080000000          lea       edx,[eax+80h]
  004c59d0  81e2ff0f0000          and       edx,0FFFh
  004c59d6  81fa00010000          cmp       edx,100h
  004c59dc  7f09                  jg        short 004C59E7h
  004c59de  66014632              add       [esi+32h],ax
  004c59e2  e9ccfbffff            jmp       004C55B3h

LAB_004c59e7:
  004c59e7  25ff0f0000            and       eax,0FFFh
  004c59ec  33c9                  xor       ecx,ecx
  004c59ee  3d00080000            cmp       eax,800h
  004c59f3  0f9dc1                setge     cl
  004c59f6  49                    dec       ecx
  004c59f7  81e100010000          and       ecx,100h
  004c59fd  83c180                add       ecx,0FFFFFF80h
  004c5a00  8bc1                  mov       eax,ecx
  004c5a02  66014632              add       [esi+32h],ax
  004c5a06  e9a8fbffff            jmp       004C55B3h
  004c5a0b  90                    nop
  004c5a0c  23504c                and       edx,[eax+4Ch]
  004c5a0f  007551                add       [ebp+51h],dh
  004c5a12  4c                    dec       esp
  004c5a13  00cb                  add       bl,cl
  004c5a15  52                    push      edx
  004c5a16  4c                    dec       esp
  004c5a17  00d7                  add       bh,dl
  004c5a19  54                    push      esp
  004c5a1a  4c                    dec       esp
  004c5a1b  008a554c0082          add       [edx-7DFFB3ABh],cl
  004c5a21  58                    pop       eax
  004c5a22  4c                    dec       esp
  004c5a23  00ee                  add       dh,ch
  004c5a25  58                    pop       eax
  004c5a26  4c                    dec       esp
  004c5a27  006559                add       [ebp+59h],ah
  004c5a2a  4c                    dec       esp
  004c5a2b  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
