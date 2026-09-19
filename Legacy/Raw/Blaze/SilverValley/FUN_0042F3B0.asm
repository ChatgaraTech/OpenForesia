Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f3b0  section .text  offset fichier 0x2e7b0  624 octets

  0042f3b0  83ec0c                sub       esp,0Ch
  0042f3b3  53                    push      ebx
  0042f3b4  55                    push      ebp
  0042f3b5  56                    push      esi
  0042f3b6  8b74241c              mov       esi,[esp+1Ch]
  0042f3ba  57                    push      edi
  0042f3bb  8b6e74                mov       ebp,[esi+74h]
  0042f3be  85ed                  test      ebp,ebp
  0042f3c0  7505                  jne       short 0042F3C7h
  0042f3c2  bd58e55d00            mov       ebp,5DE558h

LAB_0042f3c7:
  0042f3c7  66837e1000            cmp       word [esi+10h],0
  0042f3cc  bb01000000            mov       ebx,1
  0042f3d1  0f859d000000          jne       near 0042F474h
  0042f3d7  c646042a              mov       byte [esi+4],2Ah
  0042f3db  885e0a                mov       [esi+0Ah],bl
  0042f3de  885e0b                mov       [esi+0Bh],bl
  0042f3e1  c64638be              mov       byte [esi+38h],0BEh
  0042f3e5  e8567d1100            call      00547140h
  0042f3ea  99                    cdq
  0042f3eb  b928000000            mov       ecx,28h
  0042f3f0  c6463abe              mov       byte [esi+3Ah],0BEh
  0042f3f4  f7f9                  idiv      ecx
  0042f3f6  66895e10              mov       [esi+10h],bx
  0042f3fa  80ea42                sub       dl,42h
  0042f3fd  885639                mov       [esi+39h],dl
  0042f400  e83b7d1100            call      00547140h
  0042f405  99                    cdq
  0042f406  b970170000            mov       ecx,1770h
  0042f40b  f7f9                  idiv      ecx
  0042f40d  81c2581b0000          add       edx,1B58h
  0042f413  66895628              mov       [esi+28h],dx
  0042f417  e8247d1100            call      00547140h
  0042f41c  99                    cdq
  0042f41d  b970170000            mov       ecx,1770h
  0042f422  f7f9                  idiv      ecx
  0042f424  81c2581b0000          add       edx,1B58h
  0042f42a  6689562a              mov       [esi+2Ah],dx
  0042f42e  e80d7d1100            call      00547140h
  0042f433  99                    cdq
  0042f434  b970170000            mov       ecx,1770h
  0042f439  f7f9                  idiv      ecx
  0042f43b  8b4620                mov       eax,[esi+20h]
  0042f43e  8b4e24                mov       ecx,[esi+24h]
  0042f441  894654                mov       [esi+54h],eax
  0042f444  894e58                mov       [esi+58h],ecx
  0042f447  81c2581b0000          add       edx,1B58h
  0042f44d  6689562c              mov       [esi+2Ch],dx
  0042f451  8b561c                mov       edx,[esi+1Ch]
  0042f454  895650                mov       [esi+50h],edx
  0042f457  e8e47c1100            call      00547140h
  0042f45c  99                    cdq
  0042f45d  b920030000            mov       ecx,320h
  0042f462  66c746121e00          mov       word [esi+12h],1Eh
  0042f468  f7f9                  idiv      ecx
  0042f46a  81ea90010000          sub       edx,190h
  0042f470  66895634              mov       [esi+34h],dx

LAB_0042f474:
  0042f474  66395e10              cmp       [esi+10h],bx
  0042f478  0f8592010000          jne       near 0042F610h
  0042f47e  8a4638                mov       al,[esi+38h]
  0042f481  8bd0                  mov       edx,eax
  0042f483  81e2ff000000          and       edx,0FFh
  0042f489  f7da                  neg       edx
  0042f48b  c1fa03                sar       edx,3
  0042f48e  02d0                  add       dl,al
  0042f490  8a4639                mov       al,[esi+39h]
  0042f493  8bc8                  mov       ecx,eax
  0042f495  885638                mov       [esi+38h],dl
  0042f498  81e1ff000000          and       ecx,0FFh
  0042f49e  f7d9                  neg       ecx
  0042f4a0  c1f903                sar       ecx,3
  0042f4a3  02c8                  add       cl,al
  0042f4a5  8a463a                mov       al,[esi+3Ah]
  0042f4a8  8bd0                  mov       edx,eax
  0042f4aa  884e39                mov       [esi+39h],cl
  0042f4ad  81e2ff000000          and       edx,0FFh
  0042f4b3  f7da                  neg       edx
  0042f4b5  c1fa03                sar       edx,3
  0042f4b8  02d0                  add       dl,al
  0042f4ba  0fbf4632              movsx     eax,word [esi+32h]
  0042f4be  50                    push      eax
  0042f4bf  88563a                mov       [esi+3Ah],dl
  0042f4c2  e855651100            call      00545A1Ch
  0042f4c7  0fbf5632              movsx     edx,word [esi+32h]
  0042f4cb  8b5e1c                mov       ebx,[esi+1Ch]
  0042f4ce  8d0c80                lea       ecx,[eax+eax*4]
  0042f4d1  c1e103                shl       ecx,3
  0042f4d4  03d9                  add       ebx,ecx
  0042f4d6  52                    push      edx
  0042f4d7  895e1c                mov       [esi+1Ch],ebx
  0042f4da  e837651100            call      00545A16h
  0042f4df  8b4e24                mov       ecx,[esi+24h]
  0042f4e2  8d0480                lea       eax,[eax+eax*4]
  0042f4e5  c1e003                shl       eax,3
  0042f4e8  03c8                  add       ecx,eax
  0042f4ea  8a4638                mov       al,[esi+38h]
  0042f4ed  3c14                  cmp       al,14h
  0042f4ef  894e24                mov       [esi+24h],ecx
  0042f4f2  0f8607010000          jbe       near 0042F5FFh
  0042f4f8  bfc0935e00            mov       edi,5E93C0h
  0042f4fd  c744241004000000      mov       dword [esp+10h],4

LAB_0042f505:
  0042f505  8b07                  mov       eax,[edi]
  0042f507  a900000080            test      eax,80000000h
  0042f50c  0f84d8000000          je        near 0042F5EAh
  0042f512  a900000050            test      eax,50000000h
  0042f517  0f85cd000000          jne       near 0042F5EAh
  0042f51d  66837f1200            cmp       word [edi+12h],0
  0042f522  0f85c2000000          jne       near 0042F5EAh
  0042f528  8b4f1c                mov       ecx,[edi+1Ch]
  0042f52b  8b461c                mov       eax,[esi+1Ch]
  0042f52e  8b5620                mov       edx,[esi+20h]
  0042f531  8b5e24                mov       ebx,[esi+24h]
  0042f534  2bc8                  sub       ecx,eax
  0042f536  8b4720                mov       eax,[edi+20h]
  0042f539  2bc2                  sub       eax,edx
  0042f53b  8b5724                mov       edx,[edi+24h]
  0042f53e  c1f90c                sar       ecx,0Ch
  0042f541  0500c00300            add       eax,3C000h
  0042f546  2bd3                  sub       edx,ebx
  0042f548  8bd9                  mov       ebx,ecx
  0042f54a  c1f80c                sar       eax,0Ch
  0042f54d  0fafd9                imul      ebx,ecx
  0042f550  8bc8                  mov       ecx,eax
  0042f552  c1fa0c                sar       edx,0Ch
  0042f555  0fafc8                imul      ecx,eax
  0042f558  8bc2                  mov       eax,edx
  0042f55a  03d9                  add       ebx,ecx
  0042f55c  0fafc2                imul      eax,edx
  0042f55f  03d8                  add       ebx,eax
  0042f561  53                    push      ebx
  0042f562  e8a9641100            call      00545A10h
  0042f567  6683bf9600000000      cmp       word [edi+96h],0
  0042f56f  7579                  jne       short 0042F5EAh
  0042f571  3dfa000000            cmp       eax,0FAh
  0042f576  7d72                  jge       short 0042F5EAh
  0042f578  8b4e24                mov       ecx,[esi+24h]
  0042f57b  8b5620                mov       edx,[esi+20h]
  0042f57e  8b461c                mov       eax,[esi+1Ch]
  0042f581  51                    push      ecx
  0042f582  52                    push      edx
  0042f583  50                    push      eax
  0042f584  57                    push      edi
  0042f585  e816800000            call      004375A0h
  0042f58a  83c410                add       esp,10h
  0042f58d  6685c0                test      ax,ax
  0042f590  7552                  jne       short 0042F5E4h
  0042f592  8d4c2420              lea       ecx,[esp+20h]
  0042f596  8d542414              lea       edx,[esp+14h]
  0042f59a  51                    push      ecx
  0042f59b  8d44241c              lea       eax,[esp+1Ch]
  0042f59f  52                    push      edx
  0042f5a0  50                    push      eax
  0042f5a1  55                    push      ebp
  0042f5a2  e8d9931000            call      00538980h
  0042f5a7  8b442430              mov       eax,[esp+30h]
  0042f5ab  83c410                add       esp,10h
  0042f5ae  668b5830              mov       bx,[eax+30h]
  0042f5b2  57                    push      edi
  0042f5b3  668bcb                mov       cx,bx
  0042f5b6  8bd3                  mov       edx,ebx
  0042f5b8  66c1f902              sar       cx,2
  0042f5bc  2bd1                  sub       edx,ecx
  0042f5be  55                    push      ebp
  0042f5bf  66895030              mov       [eax+30h],dx
  0042f5c3  e8b8a51000            call      00539B80h
  0042f5c8  8b442428              mov       eax,[esp+28h]
  0042f5cc  83c408                add       esp,8
  0042f5cf  66895830              mov       [eax+30h],bx
  0042f5d3  66c787960000001000    mov       word [edi+96h],10h
  0042f5dc  8b07                  mov       eax,[edi]
  0042f5de  0c40                  or        al,40h
  0042f5e0  8907                  mov       [edi],eax
  0042f5e2  eb06                  jmp       short 0042F5EAh

LAB_0042f5e4:
  0042f5e4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042f5ea:
  0042f5ea  8b442410              mov       eax,[esp+10h]
  0042f5ee  81c79c000000          add       edi,9Ch
  0042f5f4  48                    dec       eax
  0042f5f5  89442410              mov       [esp+10h],eax
  0042f5f9  0f8506ffffff          jne       near 0042F505h

LAB_0042f5ff:
  0042f5ff  66ff4e12              dec       word [esi+12h]
  0042f603  66837e1200            cmp       word [esi+12h],0
  0042f608  7d06                  jge       short 0042F610h
  0042f60a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042f610:
  0042f610  5f                    pop       edi
  0042f611  5e                    pop       esi
  0042f612  5d                    pop       ebp
  0042f613  5b                    pop       ebx
  0042f614  83c40c                add       esp,0Ch
  0042f617  c3                    ret
  0042f618  90                    nop
  0042f619  90                    nop
  0042f61a  90                    nop
  0042f61b  90                    nop
  0042f61c  90                    nop
  0042f61d  90                    nop
  0042f61e  90                    nop
  0042f61f  90                    nop

7 cible(s) de saut interne : 0x0042f3c7, 0x0042f474, 0x0042f505, 0x0042f5e4, 0x0042f5ea, 0x0042f5ff, 0x0042f610
