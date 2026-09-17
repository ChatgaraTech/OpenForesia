Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004921a0  section .text  offset fichier 0x915a0  3232 octets

  004921a0  81ec88000000          sub       esp,88h
  004921a6  53                    push      ebx
  004921a7  55                    push      ebp
  004921a8  56                    push      esi
  004921a9  8bb42498000000        mov       esi,[esp+98h]
  004921b0  b864000000            mov       eax,64h
  004921b5  33ed                  xor       ebp,ebp
  004921b7  6689442432            mov       [esp+32h],ax
  004921bc  6689442436            mov       [esp+36h],ax
  004921c1  8a4641                mov       al,[esi+41h]
  004921c4  57                    push      edi
  004921c5  84c0                  test      al,al
  004921c7  66896c2430            mov       [esp+30h],bp
  004921cc  66c74424321e00        mov       word [esp+32h],1Eh
  004921d3  66c74424343200        mov       word [esp+34h],32h
  004921da  66896c2438            mov       [esp+38h],bp
  004921df  750f                  jne       short 004921F0h
  004921e1  55                    push      ebp
  004921e2  55                    push      ebp
  004921e3  56                    push      esi
  004921e4  c6464101              mov       byte [esi+41h],1
  004921e8  e8a3f0ffff            call      00491290h
  004921ed  83c40c                add       esp,0Ch

LAB_004921f0:
  004921f0  8b461c                mov       eax,[esi+1Ch]
  004921f3  8b4e20                mov       ecx,[esi+20h]
  004921f6  8b5624                mov       edx,[esi+24h]
  004921f9  8d5e1c                lea       ebx,[esi+1Ch]
  004921fc  89442424              mov       [esp+24h],eax
  00492200  8b4664                mov       eax,[esi+64h]
  00492203  894c2428              mov       [esp+28h],ecx
  00492207  8b4e60                mov       ecx,[esi+60h]
  0049220a  8954242c              mov       [esp+2Ch],edx
  0049220e  668b5632              mov       dx,[esi+32h]
  00492212  89442448              mov       [esp+48h],eax
  00492216  8b467c                mov       eax,[esi+7Ch]
  00492219  894c2440              mov       [esp+40h],ecx
  0049221d  668b8e90000000        mov       cx,[esi+90h]
  00492224  6689942486000000      mov       [esp+86h],dx
  0049222c  8b9680000000          mov       edx,[esi+80h]
  00492232  8944244c              mov       [esp+4Ch],eax
  00492236  8b8684000000          mov       eax,[esi+84h]
  0049223c  66894c2444            mov       [esp+44h],cx
  00492241  8b8e88000000          mov       ecx,[esi+88h]
  00492247  8b7e6c                mov       edi,[esi+6Ch]
  0049224a  89542450              mov       [esp+50h],edx
  0049224e  8b968c000000          mov       edx,[esi+8Ch]
  00492254  8944243c              mov       [esp+3Ch],eax
  00492258  894c245c              mov       [esp+5Ch],ecx
  0049225c  8d442464              lea       eax,[esp+64h]
  00492260  89542454              mov       [esp+54h],edx
  00492264  8d4c246c              lea       ecx,[esp+6Ch]
  00492268  50                    push      eax
  00492269  8d54246c              lea       edx,[esp+6Ch]
  0049226d  51                    push      ecx
  0049226e  52                    push      edx
  0049226f  56                    push      esi
  00492270  e80b670a00            call      00538980h
  00492275  83c410                add       esp,10h
  00492278  8d442458              lea       eax,[esp+58h]
  0049227c  8d4c2418              lea       ecx,[esp+18h]
  00492280  8d54241c              lea       edx,[esp+1Ch]
  00492284  50                    push      eax
  00492285  51                    push      ecx
  00492286  8d44241c              lea       eax,[esp+1Ch]
  0049228a  52                    push      edx
  0049228b  8d4c241c              lea       ecx,[esp+1Ch]
  0049228f  50                    push      eax
  00492290  51                    push      ecx
  00492291  56                    push      esi
  00492292  e8893c0a00            call      00535F20h
  00492297  8b4c2428              mov       ecx,[esp+28h]
  0049229b  8b542470              mov       edx,[esp+70h]
  0049229f  83c418                add       esp,18h
  004922a2  b840000000            mov       eax,40h
  004922a7  2bc1                  sub       eax,ecx
  004922a9  52                    push      edx
  004922aa  56                    push      esi
  004922ab  89442428              mov       [esp+28h],eax
  004922af  e82c3b0400            call      004D5DE0h
  004922b4  8b03                  mov       eax,[ebx]
  004922b6  8b571c                mov       edx,[edi+1Ch]
  004922b9  8b4e20                mov       ecx,[esi+20h]
  004922bc  2bc2                  sub       eax,edx
  004922be  8b5720                mov       edx,[edi+20h]
  004922c1  83c408                add       esp,8
  004922c4  2bca                  sub       ecx,edx
  004922c6  8b5624                mov       edx,[esi+24h]
  004922c9  2b5724                sub       edx,[edi+24h]
  004922cc  c1f80c                sar       eax,0Ch
  004922cf  8bf8                  mov       edi,eax
  004922d1  c1f90c                sar       ecx,0Ch
  004922d4  0faff8                imul      edi,eax
  004922d7  8bc1                  mov       eax,ecx
  004922d9  c1fa0c                sar       edx,0Ch
  004922dc  0fafc1                imul      eax,ecx
  004922df  8bca                  mov       ecx,edx
  004922e1  03f8                  add       edi,eax
  004922e3  0fafca                imul      ecx,edx
  004922e6  03f9                  add       edi,ecx
  004922e8  57                    push      edi
  004922e9  e822370b00            call      00545A10h
  004922ee  89442414              mov       [esp+14h],eax
  004922f2  8b442418              mov       eax,[esp+18h]
  004922f6  3bc5                  cmp       eax,ebp
  004922f8  0f84f3040000          je        near 004927F1h
  004922fe  8b3e                  mov       edi,[esi]
  00492300  33d2                  xor       edx,edx
  00492302  8a5604                mov       dl,[esi+4]
  00492305  81e7fffffffd          and       edi,0FDFFFFFFh
  0049230b  0fbf4e18              movsx     ecx,word [esi+18h]
  0049230f  893e                  mov       [esi],edi
  00492311  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00492318  f7c700000001          test      edi,1000000h
  0049231e  8b0c8a                mov       ecx,[edx+ecx*4]
  00492321  741d                  je        short 00492340h
  00492323  66ff461a              inc       word [esi+1Ah]
  00492327  668b561a              mov       dx,[esi+1Ah]
  0049232b  33ff                  xor       edi,edi
  0049232d  668b7916              mov       di,[ecx+16h]
  00492331  0fbfca                movsx     ecx,dx
  00492334  3bcf                  cmp       ecx,edi
  00492336  7c2a                  jl        short 00492362h
  00492338  66c7461a0000          mov       word [esi+1Ah],0
  0049233e  eb22                  jmp       short 00492362h

LAB_00492340:
  00492340  66ff461a              inc       word [esi+1Ah]
  00492344  668b561a              mov       dx,[esi+1Ah]
  00492348  33ed                  xor       ebp,ebp
  0049234a  668b6916              mov       bp,[ecx+16h]
  0049234e  0fbfca                movsx     ecx,dx
  00492351  3bcd                  cmp       ecx,ebp
  00492353  7c0d                  jl        short 00492362h
  00492355  4a                    dec       edx
  00492356  81cf00000002          or        edi,2000000h
  0049235c  6689561a              mov       [esi+1Ah],dx
  00492360  893e                  mov       [esi],edi

LAB_00492362:
  00492362  f60602                test      byte [esi],2
  00492365  0f84f7000000          je        near 00492462h
  0049236b  8a4e42                mov       cl,[esi+42h]
  0049236e  bd10000000            mov       ebp,10h
  00492373  80f90b                cmp       cl,0Bh
  00492376  755e                  jne       short 004923D6h
  00492378  66837e1a0b            cmp       word [esi+1Ah],0Bh
  0049237d  7557                  jne       short 004923D6h
  0049237f  8d542430              lea       edx,[esp+30h]
  00492383  6a00                  push      0
  00492385  52                    push      edx
  00492386  53                    push      ebx
  00492387  56                    push      esi
  00492388  e8a3780800            call      00519C30h
  0049238d  8bf8                  mov       edi,eax
  0049238f  a1b8e05d00            mov       eax,[5DE0B8h]
  00492394  83c410                add       esp,10h
  00492397  85c0                  test      eax,eax
  00492399  7437                  je        short 004923D2h
  0049239b  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004923a0  7530                  jne       short 004923D2h
  004923a2  8b0f                  mov       ecx,[edi]
  004923a4  57                    push      edi
  004923a5  83c940                or        ecx,40h
  004923a8  56                    push      esi
  004923a9  6689af96000000        mov       [edi+96h],bp
  004923b0  890f                  mov       [edi],ecx
  004923b2  e8c9770a00            call      00539B80h
  004923b7  83c408                add       esp,8
  004923ba  83c71c                add       edi,1Ch
  004923bd  57                    push      edi
  004923be  6800180000            push      1800h
  004923c3  6800030000            push      300h
  004923c8  6a03                  push      3
  004923ca  e851b70a00            call      0053DB20h
  004923cf  83c410                add       esp,10h

LAB_004923d2:
  004923d2  8b442418              mov       eax,[esp+18h]

LAB_004923d6:
  004923d6  807e420c              cmp       byte [esi+42h],0Ch
  004923da  0f8582000000          jne       near 00492462h
  004923e0  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004923e5  757b                  jne       short 00492462h
  004923e7  8d442430              lea       eax,[esp+30h]
  004923eb  6a00                  push      0
  004923ed  50                    push      eax
  004923ee  53                    push      ebx
  004923ef  56                    push      esi
  004923f0  e83b780800            call      00519C30h
  004923f5  8bf8                  mov       edi,eax
  004923f7  a1b8e05d00            mov       eax,[5DE0B8h]
  004923fc  83c410                add       esp,10h
  004923ff  85c0                  test      eax,eax
  00492401  745b                  je        short 0049245Eh
  00492403  66837e1a0a            cmp       word [esi+1Ah],0Ah
  00492408  7554                  jne       short 0049245Eh
  0049240a  8b07                  mov       eax,[edi]
  0049240c  57                    push      edi
  0049240d  0c40                  or        al,40h
  0049240f  56                    push      esi
  00492410  6689af96000000        mov       [edi+96h],bp
  00492417  8907                  mov       [edi],eax
  00492419  e862770a00            call      00539B80h
  0049241e  83c408                add       esp,8
  00492421  e81a4d0b00            call      00547140h
  00492426  99                    cdq
  00492427  b903000000            mov       ecx,3
  0049242c  6a00                  push      0
  0049242e  f7f9                  idiv      ecx
  00492430  57                    push      edi
  00492431  56                    push      esi
  00492432  83fa01                cmp       edx,1
  00492435  7507                  jne       short 0049243Eh
  00492437  e8c4090000            call      00492E00h
  0049243c  eb05                  jmp       short 00492443h

LAB_0049243e:
  0049243e  e81dfdffff            call      00492160h

LAB_00492443:
  00492443  83c40c                add       esp,0Ch
  00492446  83c71c                add       edi,1Ch
  00492449  57                    push      edi
  0049244a  6800180000            push      1800h
  0049244f  6800020000            push      200h
  00492454  6a03                  push      3
  00492456  e8c5b60a00            call      0053DB20h
  0049245b  83c410                add       esp,10h

LAB_0049245e:
  0049245e  8b442418              mov       eax,[esp+18h]

LAB_00492462:
  00492462  33c9                  xor       ecx,ecx
  00492464  8a4e42                mov       cl,[esi+42h]
  00492467  83f90c                cmp       ecx,0Ch
  0049246a  0f8781030000          ja        near 004927F1h
  00492470  ff248dc02d4900        jmp       dword [ecx*4+492DC0h]
  00492477  33c9                  xor       ecx,ecx
  00492479  66394e12              cmp       [esi+12h],cx
  0049247d  751a                  jne       short 00492499h
  0049247f  8b06                  mov       eax,[esi]
  00492481  66894e18              mov       [esi+18h],cx
  00492485  0d00000001            or        eax,1000000h
  0049248a  66894e1a              mov       [esi+1Ah],cx
  0049248e  8906                  mov       [esi],eax
  00492490  66c746120100          mov       word [esi+12h],1
  00492496  894e5c                mov       [esi+5Ch],ecx

LAB_00492499:
  00492499  0fbf5610              movsx     edx,word [esi+10h]
  0049249d  39542414              cmp       [esp+14h],edx
  004924a1  0f8c46030000          jl        near 004927EDh
  004924a7  8a4643                mov       al,[esi+43h]
  004924aa  84c0                  test      al,al
  004924ac  0f84fe020000          je        near 004927B0h
  004924b2  fec8                  dec       al
  004924b4  894e5c                mov       [esi+5Ch],ecx
  004924b7  884643                mov       [esi+43h],al
  004924ba  e932030000            jmp       004927F1h
  004924bf  33c9                  xor       ecx,ecx
  004924c1  66394e12              cmp       [esi+12h],cx
  004924c5  751b                  jne       short 004924E2h
  004924c7  8b16                  mov       edx,[esi]
  004924c9  b801000000            mov       eax,1
  004924ce  81ca00000001          or        edx,1000000h
  004924d4  66894618              mov       [esi+18h],ax
  004924d8  66894e1a              mov       [esi+1Ah],cx
  004924dc  8916                  mov       [esi],edx
  004924de  66894612              mov       [esi+12h],ax

LAB_004924e2:
  004924e2  8b44241c              mov       eax,[esp+1Ch]
  004924e6  8d90c8000000          lea       edx,[eax+0C8h]
  004924ec  81e2ff0f0000          and       edx,0FFFh
  004924f2  81fa90010000          cmp       edx,190h
  004924f8  7f0c                  jg        short 00492506h
  004924fa  c6464300              mov       byte [esi+43h],0
  004924fe  894e5c                mov       [esi+5Ch],ecx
  00492501  e9eb020000            jmp       004927F1h

LAB_00492506:
  00492506  25ff0f0000            and       eax,0FFFh
  0049250b  3d00080000            cmp       eax,800h
  00492510  7d13                  jge       short 00492525h
  00492512  8b442410              mov       eax,[esp+10h]
  00492516  894e5c                mov       [esi+5Ch],ecx
  00492519  83c03c                add       eax,3Ch
  0049251c  66014632              add       [esi+32h],ax
  00492520  e9cc020000            jmp       004927F1h

LAB_00492525:
  00492525  8b542410              mov       edx,[esp+10h]
  00492529  b8c4ffffff            mov       eax,0FFFFFFC4h
  0049252e  2bc2                  sub       eax,edx
  00492530  894e5c                mov       [esi+5Ch],ecx
  00492533  66014632              add       [esi+32h],ax
  00492537  e9b5020000            jmp       004927F1h
  0049253c  8b4c2410              mov       ecx,[esp+10h]
  00492540  33d2                  xor       edx,edx
  00492542  66395612              cmp       [esi+12h],dx
  00492546  b801000000            mov       eax,1
  0049254b  7525                  jne       short 00492572h
  0049254d  8b3e                  mov       edi,[esi]
  0049254f  66894618              mov       [esi+18h],ax
  00492553  81cf00000001          or        edi,1000000h
  00492559  6689561a              mov       [esi+1Ah],dx
  0049255d  893e                  mov       [esi],edi
  0049255f  8d3cc9                lea       edi,[ecx+ecx*8]
  00492562  c1e704                shl       edi,4
  00492565  81c700700000          add       edi,7000h
  0049256b  66894612              mov       [esi+12h],ax
  0049256f  897e5c                mov       [esi+5Ch],edi

LAB_00492572:
  00492572  66394612              cmp       [esi+12h],ax
  00492576  0f8575020000          jne       near 004927F1h
  0049257c  0fbf4610              movsx     eax,word [esi+10h]
  00492580  39442414              cmp       [esp+14h],eax
  00492584  7d0c                  jge       short 00492592h
  00492586  c6464300              mov       byte [esi+43h],0
  0049258a  89565c                mov       [esi+5Ch],edx
  0049258d  e95f020000            jmp       004927F1h

LAB_00492592:
  00492592  8a4643                mov       al,[esi+43h]
  00492595  84c0                  test      al,al
  00492597  7405                  je        short 0049259Eh
  00492599  fec8                  dec       al
  0049259b  884643                mov       [esi+43h],al

LAB_0049259e:
  0049259e  8b44241c              mov       eax,[esp+1Ch]
  004925a2  8d9000010000          lea       edx,[eax+100h]
  004925a8  81e2ff0f0000          and       edx,0FFFh
  004925ae  81fa00020000          cmp       edx,200h
  004925b4  0f8e37020000          jle       near 004927F1h
  004925ba  25ff0f0000            and       eax,0FFFh
  004925bf  3d00080000            cmp       eax,800h
  004925c4  7d0d                  jge       short 004925D3h
  004925c6  8d440928              lea       eax,[ecx+ecx+28h]
  004925ca  66014632              add       [esi+32h],ax
  004925ce  e91e020000            jmp       004927F1h

LAB_004925d3:
  004925d3  03c9                  add       ecx,ecx
  004925d5  b8d8ffffff            mov       eax,0FFFFFFD8h
  004925da  2bc1                  sub       eax,ecx
  004925dc  66014632              add       [esi+32h],ax
  004925e0  e90c020000            jmp       004927F1h
  004925e5  66837e1201            cmp       word [esi+12h],1
  004925ea  0f85a3000000          jne       near 00492693h
  004925f0  8a4e43                mov       cl,[esi+43h]
  004925f3  84c9                  test      cl,cl
  004925f5  7405                  je        short 004925FCh
  004925f7  fec9                  dec       cl
  004925f9  884e43                mov       [esi+43h],cl

LAB_004925fc:
  004925fc  8b4c2410              mov       ecx,[esp+10h]
  00492600  8d14c9                lea       edx,[ecx+ecx*8]
  00492603  c1e204                shl       edx,4
  00492606  8d8a00500000          lea       ecx,[edx+5000h]
  0049260c  0fafc1                imul      eax,ecx
  0049260f  c1f80c                sar       eax,0Ch
  00492612  8bf8                  mov       edi,eax
  00492614  894e5c                mov       [esi+5Ch],ecx
  00492617  0fbf4610              movsx     eax,word [esi+10h]
  0049261b  50                    push      eax
  0049261c  e8fb330b00            call      00545A1Ch
  00492621  0fafc7                imul      eax,edi
  00492624  0fbf4e10              movsx     ecx,word [esi+10h]
  00492628  8b2b                  mov       ebp,[ebx]
  0049262a  51                    push      ecx
  0049262b  c1f80c                sar       eax,0Ch
  0049262e  03e8                  add       ebp,eax
  00492630  892b                  mov       [ebx],ebp
  00492632  e8df330b00            call      00545A16h
  00492637  0fafc7                imul      eax,edi
  0049263a  8b6e24                mov       ebp,[esi+24h]
  0049263d  c7465c00000000        mov       dword [esi+5Ch],0
  00492644  c1f80c                sar       eax,0Ch
  00492647  03e8                  add       ebp,eax
  00492649  8b44241c              mov       eax,[esp+1Ch]
  0049264d  896e24                mov       [esi+24h],ebp
  00492650  8d9000010000          lea       edx,[eax+100h]
  00492656  81e2ff0f0000          and       edx,0FFFh
  0049265c  81fa00020000          cmp       edx,200h
  00492662  7e2f                  jle       short 00492693h
  00492664  25ff0f0000            and       eax,0FFFh
  00492669  3d00080000            cmp       eax,800h
  0049266e  7d0d                  jge       short 0049267Dh
  00492670  8b442410              mov       eax,[esp+10h]
  00492674  8d04c528000000        lea       eax,[eax*8+28h]
  0049267b  eb12                  jmp       short 0049268Fh

LAB_0049267d:
  0049267d  8b4c2410              mov       ecx,[esp+10h]
  00492681  b8d8ffffff            mov       eax,0FFFFFFD8h
  00492686  8d14cd00000000        lea       edx,[ecx*8]
  0049268d  2bc2                  sub       eax,edx

LAB_0049268f:
  0049268f  66014632              add       [esi+32h],ax

LAB_00492693:
  00492693  66837e1200            cmp       word [esi+12h],0
  00492698  0f8553010000          jne       near 004927F1h
  0049269e  8b0e                  mov       ecx,[esi]
  004926a0  b801000000            mov       eax,1
  004926a5  81c900000001          or        ecx,1000000h
  004926ab  66894618              mov       [esi+18h],ax
  004926af  66c7461a0000          mov       word [esi+1Ah],0
  004926b5  890e                  mov       [esi],ecx
  004926b7  66894612              mov       [esi+12h],ax
  004926bb  e931010000            jmp       004927F1h
  004926c0  66837e1200            cmp       word [esi+12h],0
  004926c5  0f8582000000          jne       near 0049274Dh
  004926cb  8b0e                  mov       ecx,[esi]
  004926cd  f6c101                test      cl,1
  004926d0  7406                  je        short 004926D8h
  004926d2  c6464300              mov       byte [esi+43h],0
  004926d6  eb75                  jmp       short 0049274Dh

LAB_004926d8:
  004926d8  81e1fffffffe          and       ecx,0FEFFFFFFh
  004926de  bd01000000            mov       ebp,1
  004926e3  890e                  mov       [esi],ecx
  004926e5  8b4c2410              mov       ecx,[esp+10h]
  004926e9  81c100010000          add       ecx,100h
  004926ef  66896e18              mov       [esi+18h],bp
  004926f3  c1e106                shl       ecx,6
  004926f6  0fafc1                imul      eax,ecx
  004926f9  0fbf5610              movsx     edx,word [esi+10h]
  004926fd  c1f80c                sar       eax,0Ch
  00492700  52                    push      edx
  00492701  66c7461a0000          mov       word [esi+1Ah],0
  00492707  894e5c                mov       [esi+5Ch],ecx
  0049270a  8bf8                  mov       edi,eax
  0049270c  e80b330b00            call      00545A1Ch
  00492711  0fafc7                imul      eax,edi
  00492714  8b5650                mov       edx,[esi+50h]
  00492717  c1f80c                sar       eax,0Ch
  0049271a  03d0                  add       edx,eax
  0049271c  0fbf4610              movsx     eax,word [esi+10h]
  00492720  50                    push      eax
  00492721  895650                mov       [esi+50h],edx
  00492724  e8ed320b00            call      00545A16h
  00492729  0fafc7                imul      eax,edi
  0049272c  8b5658                mov       edx,[esi+58h]
  0049272f  8b4e54                mov       ecx,[esi+54h]
  00492732  c1f80c                sar       eax,0Ch
  00492735  03d0                  add       edx,eax
  00492737  8b06                  mov       eax,[esi]
  00492739  81c100e00100          add       ecx,1E000h
  0049273f  0bc5                  or        eax,ebp
  00492741  895658                mov       [esi+58h],edx
  00492744  894e54                mov       [esi+54h],ecx
  00492747  8906                  mov       [esi],eax
  00492749  66896e12              mov       [esi+12h],bp

LAB_0049274d:
  0049274d  f60601                test      byte [esi],1
  00492750  0f859b000000          jne       near 004927F1h
  00492756  e992000000            jmp       004927EDh
  0049275b  668b4e12              mov       cx,[esi+12h]
  0049275f  ba01000000            mov       edx,1
  00492764  663bca                cmp       cx,dx
  00492767  751c                  jne       short 00492785h
  00492769  0fbf4610              movsx     eax,word [esi+10h]
  0049276d  39442414              cmp       [esp+14h],eax
  00492771  7d06                  jge       short 00492779h
  00492773  c6464300              mov       byte [esi+43h],0
  00492777  eb0c                  jmp       short 00492785h

LAB_00492779:
  00492779  8a4643                mov       al,[esi+43h]
  0049277c  84c0                  test      al,al
  0049277e  7405                  je        short 00492785h
  00492780  fec8                  dec       al
  00492782  884643                mov       [esi+43h],al

LAB_00492785:
  00492785  6685c9                test      cx,cx
  00492788  7567                  jne       short 004927F1h
  0049278a  8b442410              mov       eax,[esp+10h]
  0049278e  8b3e                  mov       edi,[esi]
  00492790  66894e1a              mov       [esi+1Ah],cx
  00492794  81cf00000001          or        edi,1000000h
  0049279a  8d0cc0                lea       ecx,[eax+eax*8]
  0049279d  66895618              mov       [esi+18h],dx
  004927a1  c1e104                shl       ecx,4
  004927a4  893e                  mov       [esi],edi
  004927a6  66895612              mov       [esi+12h],dx
  004927aa  81c100700000          add       ecx,7000h

LAB_004927b0:
  004927b0  894e5c                mov       [esi+5Ch],ecx
  004927b3  eb3c                  jmp       short 004927F1h
  004927b5  66837e1200            cmp       word [esi+12h],0
  004927ba  7535                  jne       short 004927F1h
  004927bc  8b06                  mov       eax,[esi]
  004927be  c7465c00000000        mov       dword [esi+5Ch],0
  004927c5  a900000002            test      eax,2000000h
  004927ca  7425                  je        short 004927F1h
  004927cc  24fd                  and       al,0FDh
  004927ce  c6464300              mov       byte [esi+43h],0
  004927d2  8906                  mov       [esi],eax
  004927d4  eb1b                  jmp       short 004927F1h
  004927d6  66837e1200            cmp       word [esi+12h],0
  004927db  7514                  jne       short 004927F1h
  004927dd  8b06                  mov       eax,[esi]
  004927df  c7465c00000000        mov       dword [esi+5Ch],0
  004927e6  a900000002            test      eax,2000000h
  004927eb  7404                  je        short 004927F1h

LAB_004927ed:
  004927ed  c6464300              mov       byte [esi+43h],0

LAB_004927f1:
  004927f1  0fbf5632              movsx     edx,word [esi+32h]
  004927f5  52                    push      edx
  004927f6  e821320b00            call      00545A1Ch
  004927fb  89442470              mov       [esp+70h],eax
  004927ff  0fbf4632              movsx     eax,word [esi+32h]
  00492803  50                    push      eax
  00492804  e80d320b00            call      00545A16h
  00492809  8b5650                mov       edx,[esi+50h]
  0049280c  89442478              mov       [esp+78h],eax
  00492810  8bc2                  mov       eax,edx
  00492812  8bfa                  mov       edi,edx
  00492814  c1f808                sar       eax,8
  00492817  2bf8                  sub       edi,eax
  00492819  8b4c2418              mov       ecx,[esp+18h]
  0049281d  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00492824  0faf4e5c              imul      ecx,[esi+5Ch]
  00492828  c1fa03                sar       edx,3
  0049282b  2bfa                  sub       edi,edx
  0049282d  8b5654                mov       edx,[esi+54h]
  00492830  8be8                  mov       ebp,eax
  00492832  897e50                mov       [esi+50h],edi
  00492835  c1fd08                sar       ebp,8
  00492838  c1fa08                sar       edx,8
  0049283b  2bea                  sub       ebp,edx
  0049283d  8b5658                mov       edx,[esi+58h]
  00492840  2be8                  sub       ebp,eax
  00492842  8b4654                mov       eax,[esi+54h]
  00492845  03c5                  add       eax,ebp
  00492847  8bea                  mov       ebp,edx
  00492849  89442460              mov       [esp+60h],eax
  0049284d  894654                mov       [esi+54h],eax
  00492850  8bc2                  mov       eax,edx
  00492852  56                    push      esi
  00492853  c1f803                sar       eax,3
  00492856  2be8                  sub       ebp,eax
  00492858  8b442464              mov       eax,[esp+64h]
  0049285c  c1fa08                sar       edx,8
  0049285f  c1f90c                sar       ecx,0Ch
  00492862  2bea                  sub       ebp,edx
  00492864  8bd1                  mov       edx,ecx
  00492866  0faf542474            imul      edx,[esp+74h]
  0049286b  0faf4c247c            imul      ecx,[esp+7Ch]
  00492870  c1fa0c                sar       edx,0Ch
  00492873  03d7                  add       edx,edi
  00492875  8b3b                  mov       edi,[ebx]
  00492877  03fa                  add       edi,edx
  00492879  8b5620                mov       edx,[esi+20h]
  0049287c  c1f90c                sar       ecx,0Ch
  0049287f  03d0                  add       edx,eax
  00492881  8b4624                mov       eax,[esi+24h]
  00492884  03cd                  add       ecx,ebp
  00492886  896e58                mov       [esi+58h],ebp
  00492889  03c1                  add       eax,ecx
  0049288b  893b                  mov       [ebx],edi
  0049288d  895620                mov       [esi+20h],edx
  00492890  894624                mov       [esi+24h],eax
  00492893  e8a8100a00            call      00533940h
  00492898  8b9690000000          mov       edx,[esi+90h]
  0049289e  8b4660                mov       eax,[esi+60h]
  004928a1  8b4e20                mov       ecx,[esi+20h]
  004928a4  83c404                add       esp,4
  004928a7  80e6fe                and       dh,0FEh
  004928aa  3bc8                  cmp       ecx,eax
  004928ac  899690000000          mov       [esi+90h],edx
  004928b2  7f2a                  jg        short 004928DEh
  004928b4  f6c680                test      dh,80h
  004928b7  7525                  jne       short 004928DEh
  004928b9  8b0e                  mov       ecx,[esi]
  004928bb  894620                mov       [esi+20h],eax
  004928be  f6c101                test      cl,1
  004928c1  740f                  je        short 004928D2h
  004928c3  83e1fe                and       ecx,0FFFFFFFEh
  004928c6  33ff                  xor       edi,edi
  004928c8  890e                  mov       [esi],ecx
  004928ca  897e54                mov       [esi+54h],edi
  004928cd  e924010000            jmp       004929F6h

LAB_004928d2:
  004928d2  83e1fe                and       ecx,0FFFFFFFEh
  004928d5  33ff                  xor       edi,edi
  004928d7  890e                  mov       [esi],ecx
  004928d9  897e54                mov       [esi+54h],edi
  004928dc  eb02                  jmp       short 004928E0h

LAB_004928de:
  004928de  33ff                  xor       edi,edi

LAB_004928e0:
  004928e0  8b0e                  mov       ecx,[esi]
  004928e2  f6c101                test      cl,1
  004928e5  750a                  jne       short 004928F1h
  004928e7  394620                cmp       [esi+20h],eax
  004928ea  7e05                  jle       short 004928F1h
  004928ec  83c901                or        ecx,1
  004928ef  890e                  mov       [esi],ecx

LAB_004928f1:
  004928f1  f7c282ad0020          test      edx,2000AD82h
  004928f7  0f84f9000000          je        near 004929F6h
  004928fd  f60601                test      byte [esi],1
  00492900  0f85c8000000          jne       near 004929CEh
  00492906  8a4e42                mov       cl,[esi+42h]
  00492909  b007                  mov       al,7
  0049290b  3ac8                  cmp       cl,al
  0049290d  7420                  je        short 0049292Fh
  0049290f  884642                mov       [esi+42h],al
  00492912  e829480b00            call      00547140h
  00492917  99                    cdq
  00492918  b928000000            mov       ecx,28h
  0049291d  66897e12              mov       [esi+12h],di
  00492921  f7f9                  idiv      ecx
  00492923  66c746105000          mov       word [esi+10h],50h
  00492929  80c214                add       dl,14h
  0049292c  885643                mov       [esi+43h],dl

LAB_0049292f:
  0049292f  8b16                  mov       edx,[esi]
  00492931  8b442428              mov       eax,[esp+28h]
  00492935  8b4c242c              mov       ecx,[esp+2Ch]
  00492939  83e2fd                and       edx,0FFFFFFFDh
  0049293c  8916                  mov       [esi],edx
  0049293e  8b542424              mov       edx,[esp+24h]
  00492942  894620                mov       [esi+20h],eax
  00492945  8b442444              mov       eax,[esp+44h]
  00492949  8913                  mov       [ebx],edx
  0049294b  8b54244c              mov       edx,[esp+4Ch]
  0049294f  25ffff0000            and       eax,0FFFFh
  00492954  894e24                mov       [esi+24h],ecx
  00492957  8b4c243c              mov       ecx,[esp+3Ch]
  0049295b  89567c                mov       [esi+7Ch],edx
  0049295e  8b54245c              mov       edx,[esp+5Ch]
  00492962  898690000000          mov       [esi+90h],eax
  00492968  8b442450              mov       eax,[esp+50h]
  0049296c  898e84000000          mov       [esi+84h],ecx
  00492972  8b4c2448              mov       ecx,[esp+48h]
  00492976  898680000000          mov       [esi+80h],eax
  0049297c  8b442454              mov       eax,[esp+54h]
  00492980  899688000000          mov       [esi+88h],edx
  00492986  8b542440              mov       edx,[esp+40h]
  0049298a  6882ad0020            push      2000AD82h
  0049298f  56                    push      esi
  00492990  89868c000000          mov       [esi+8Ch],eax
  00492996  894e64                mov       [esi+64h],ecx
  00492999  895660                mov       [esi+60h],edx
  0049299c  e8bf340400            call      004D5E60h
  004929a1  83c408                add       esp,8
  004929a4  663bc7                cmp       ax,di
  004929a7  7515                  jne       short 004929BEh
  004929a9  e892470b00            call      00547140h
  004929ae  99                    cdq
  004929af  33c2                  xor       eax,edx
  004929b1  2bc2                  sub       eax,edx
  004929b3  83e001                and       eax,1
  004929b6  33c2                  xor       eax,edx
  004929b8  2bc2                  sub       eax,edx
  004929ba  8d4400ff              lea       eax,[eax+eax-1]

LAB_004929be:
  004929be  8b4c2410              mov       ecx,[esp+10h]
  004929c2  83c140                add       ecx,40h
  004929c5  0fafc8                imul      ecx,eax
  004929c8  66014e32              add       [esi+32h],cx
  004929cc  eb28                  jmp       short 004929F6h

LAB_004929ce:
  004929ce  8b542424              mov       edx,[esp+24h]
  004929d2  8b44242c              mov       eax,[esp+2Ch]
  004929d6  897e5c                mov       [esi+5Ch],edi
  004929d9  897e50                mov       [esi+50h],edi
  004929dc  897e58                mov       [esi+58h],edi
  004929df  8913                  mov       [ebx],edx
  004929e1  894624                mov       [esi+24h],eax
  004929e4  c6464200              mov       byte [esi+42h],0
  004929e8  c6464328              mov       byte [esi+43h],28h
  004929ec  66897e12              mov       [esi+12h],di
  004929f0  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004929f6:
  004929f6  6aff                  push      0FFFFFFFFh
  004929f8  6aff                  push      0FFFFFFFFh
  004929fa  6aff                  push      0FFFFFFFFh
  004929fc  56                    push      esi
  004929fd  e80e0e0300            call      004C3810h
  00492a02  8b442428              mov       eax,[esp+28h]
  00492a06  83c410                add       esp,10h
  00492a09  3bc7                  cmp       eax,edi
  00492a0b  7517                  jne       short 00492A24h
  00492a0d  668b8c2486000000      mov       cx,[esp+86h]
  00492a15  66894e32              mov       [esi+32h],cx
  00492a19  5f                    pop       edi
  00492a1a  5e                    pop       esi
  00492a1b  5d                    pop       ebp
  00492a1c  5b                    pop       ebx
  00492a1d  81c488000000          add       esp,88h
  00492a23  c3                    ret

LAB_00492a24:
  00492a24  8a4643                mov       al,[esi+43h]
  00492a27  84c0                  test      al,al
  00492a29  0f8584030000          jne       near 00492DB3h
  00492a2f  8b466c                mov       eax,[esi+6Ch]
  00492a32  8b2b                  mov       ebp,[ebx]
  00492a34  8b4e24                mov       ecx,[esi+24h]
  00492a37  8b781c                mov       edi,[eax+1Ch]
  00492a3a  2bfd                  sub       edi,ebp
  00492a3c  8b6824                mov       ebp,[eax+24h]
  00492a3f  2be9                  sub       ebp,ecx
  00492a41  c1ff0c                sar       edi,0Ch
  00492a44  c1fd0c                sar       ebp,0Ch
  00492a47  8bd7                  mov       edx,edi
  00492a49  8bc5                  mov       eax,ebp
  00492a4b  0fafd7                imul      edx,edi
  00492a4e  0fafc5                imul      eax,ebp
  00492a51  03d0                  add       edx,eax
  00492a53  52                    push      edx
  00492a54  e8b72f0b00            call      00545A10h
  00492a59  55                    push      ebp
  00492a5a  57                    push      edi
  00492a5b  8944241c              mov       [esp+1Ch],eax
  00492a5f  e8be2f0b00            call      00545A22h
  00492a64  668b4e32              mov       cx,[esi+32h]
  00492a68  66c746120000          mov       word [esi+12h],0
  00492a6e  2bc1                  sub       eax,ecx
  00492a70  b306                  mov       bl,6
  00492a72  25ff0f0000            and       eax,0FFFh
  00492a77  8944241c              mov       [esp+1Ch],eax
  00492a7b  8b442414              mov       eax,[esp+14h]
  00492a7f  3dfa000000            cmp       eax,0FAh
  00492a84  0f8d54010000          jge       near 00492BDEh
  00492a8a  e8b1460b00            call      00547140h
  00492a8f  99                    cdq
  00492a90  b964000000            mov       ecx,64h
  00492a95  f7f9                  idiv      ecx
  00492a97  8bea                  mov       ebp,edx
  00492a99  83fd28                cmp       ebp,28h
  00492a9c  0f8da0000000          jge       near 00492B42h
  00492aa2  8b54241c              mov       edx,[esp+1Ch]
  00492aa6  8b7c2420              mov       edi,[esp+20h]
  00492aaa  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00492ab1  8d0cfd00020000        lea       ecx,[edi*8+200h]
  00492ab8  25ff0f0000            and       eax,0FFFh
  00492abd  3bc1                  cmp       eax,ecx
  00492abf  7f72                  jg        short 00492B33h
  00492ac1  e87a460b00            call      00547140h
  00492ac6  99                    cdq
  00492ac7  8d4c3f01              lea       ecx,[edi+edi+1]
  00492acb  f7f9                  idiv      ecx
  00492acd  8b442414              mov       eax,[esp+14h]
  00492ad1  83c264                add       edx,64h
  00492ad4  3bc2                  cmp       eax,edx
  00492ad6  7d5b                  jge       short 00492B33h
  00492ad8  8bd7                  mov       edx,edi
  00492ada  d1fa                  sar       edx,1
  00492adc  83c20a                add       edx,0Ah
  00492adf  3bea                  cmp       ebp,edx
  00492ae1  7d27                  jge       short 00492B0Ah
  00492ae3  8b06                  mov       eax,[esi]
  00492ae5  c646420b              mov       byte [esi+42h],0Bh
  00492ae9  25fffffffe            and       eax,0FEFFFFFFh
  00492aee  c6464301              mov       byte [esi+43h],1
  00492af2  0c02                  or        al,2
  00492af4  33ed                  xor       ebp,ebp
  00492af6  8906                  mov       [esi],eax
  00492af8  896e5c                mov       [esi+5Ch],ebp
  00492afb  66c746180300          mov       word [esi+18h],3
  00492b01  66896e1a              mov       [esi+1Ah],bp
  00492b05  e9da000000            jmp       00492BE4h

LAB_00492b0a:
  00492b0a  8b0e                  mov       ecx,[esi]
  00492b0c  c646420c              mov       byte [esi+42h],0Ch
  00492b10  81e1fffffffe          and       ecx,0FEFFFFFFh
  00492b16  c6464301              mov       byte [esi+43h],1
  00492b1a  83c902                or        ecx,2
  00492b1d  33ed                  xor       ebp,ebp
  00492b1f  890e                  mov       [esi],ecx
  00492b21  896e5c                mov       [esi+5Ch],ebp
  00492b24  66c746180400          mov       word [esi+18h],4
  00492b2a  66896e1a              mov       [esi+1Ah],bp
  00492b2e  e9b1000000            jmp       00492BE4h

LAB_00492b33:
  00492b33  c6464201              mov       byte [esi+42h],1
  00492b37  c6464301              mov       byte [esi+43h],1
  00492b3b  33ed                  xor       ebp,ebp
  00492b3d  e9a2000000            jmp       00492BE4h

LAB_00492b42:
  00492b42  83fd3c                cmp       ebp,3Ch
  00492b45  7d40                  jge       short 00492B87h
  00492b47  33ed                  xor       ebp,ebp
  00492b49  c6464203              mov       byte [esi+42h],3
  00492b4d  66896e12              mov       [esi+12h],bp
  00492b51  e8ea450b00            call      00547140h
  00492b56  99                    cdq
  00492b57  b90a000000            mov       ecx,0Ah
  00492b5c  8b7c2420              mov       edi,[esp+20h]
  00492b60  f7f9                  idiv      ecx
  00492b62  8bc7                  mov       eax,edi
  00492b64  d1f8                  sar       eax,1
  00492b66  02d0                  add       dl,al
  00492b68  80c214                add       dl,14h
  00492b6b  885643                mov       [esi+43h],dl
  00492b6e  e8cd450b00            call      00547140h
  00492b73  99                    cdq
  00492b74  33c2                  xor       eax,edx
  00492b76  2bc2                  sub       eax,edx
  00492b78  25ff0f0000            and       eax,0FFFh
  00492b7d  33c2                  xor       eax,edx
  00492b7f  2bc2                  sub       eax,edx
  00492b81  66894610              mov       [esi+10h],ax
  00492b85  eb5d                  jmp       short 00492BE4h

LAB_00492b87:
  00492b87  83fd46                cmp       ebp,46h
  00492b8a  7d27                  jge       short 00492BB3h
  00492b8c  8b4c2410              mov       ecx,[esp+10h]
  00492b90  8b442414              mov       eax,[esp+14h]
  00492b94  83c164                add       ecx,64h
  00492b97  3bc1                  cmp       eax,ecx
  00492b99  7e18                  jle       short 00492BB3h
  00492b9b  885e42                mov       [esi+42h],bl
  00492b9e  c6464301              mov       byte [esi+43h],1
  00492ba2  e899450b00            call      00547140h
  00492ba7  33ed                  xor       ebp,ebp
  00492ba9  66894610              mov       [esi+10h],ax
  00492bad  66896e12              mov       [esi+12h],bp
  00492bb1  eb2d                  jmp       short 00492BE0h

LAB_00492bb3:
  00492bb3  c6464202              mov       byte [esi+42h],2
  00492bb7  e884450b00            call      00547140h
  00492bbc  99                    cdq
  00492bbd  b90a000000            mov       ecx,0Ah
  00492bc2  8b7c2420              mov       edi,[esp+20h]
  00492bc6  f7f9                  idiv      ecx
  00492bc8  8bc7                  mov       eax,edi
  00492bca  66c746105000          mov       word [esi+10h],50h
  00492bd0  d1f8                  sar       eax,1
  00492bd2  02d0                  add       dl,al
  00492bd4  80c214                add       dl,14h
  00492bd7  33ed                  xor       ebp,ebp
  00492bd9  885643                mov       [esi+43h],dl
  00492bdc  eb06                  jmp       short 00492BE4h

LAB_00492bde:
  00492bde  33ed                  xor       ebp,ebp

LAB_00492be0:
  00492be0  8b7c2420              mov       edi,[esp+20h]

LAB_00492be4:
  00492be4  8b442414              mov       eax,[esp+14h]
  00492be8  3d58020000            cmp       eax,258h
  00492bed  0f8dc0000000          jge       near 00492CB3h
  00492bf3  3dfa000000            cmp       eax,0FAh
  00492bf8  0f8cb5000000          jl        near 00492CB3h
  00492bfe  e83d450b00            call      00547140h
  00492c03  99                    cdq
  00492c04  b964000000            mov       ecx,64h
  00492c09  f7f9                  idiv      ecx
  00492c0b  83fa14                cmp       edx,14h
  00492c0e  7d26                  jge       short 00492C36h
  00492c10  8b54241c              mov       edx,[esp+1Ch]
  00492c14  81c200040000          add       edx,400h
  00492c1a  81e2ff0f0000          and       edx,0FFFh
  00492c20  81fa00080000          cmp       edx,800h
  00492c26  0f8c87000000          jl        near 00492CB3h
  00492c2c  c6464201              mov       byte [esi+42h],1
  00492c30  c6464301              mov       byte [esi+43h],1
  00492c34  eb7d                  jmp       short 00492CB3h

LAB_00492c36:
  00492c36  83fa32                cmp       edx,32h
  00492c39  7d25                  jge       short 00492C60h
  00492c3b  c6464202              mov       byte [esi+42h],2
  00492c3f  e8fc440b00            call      00547140h
  00492c44  99                    cdq
  00492c45  b90a000000            mov       ecx,0Ah
  00492c4a  66c746105000          mov       word [esi+10h],50h
  00492c50  f7f9                  idiv      ecx
  00492c52  8bc7                  mov       eax,edi
  00492c54  d1f8                  sar       eax,1
  00492c56  02d0                  add       dl,al
  00492c58  80c214                add       dl,14h
  00492c5b  885643                mov       [esi+43h],dl
  00492c5e  eb53                  jmp       short 00492CB3h

LAB_00492c60:
  00492c60  83fa46                cmp       edx,46h
  00492c63  7d16                  jge       short 00492C7Bh
  00492c65  885e42                mov       [esi+42h],bl
  00492c68  c6464301              mov       byte [esi+43h],1
  00492c6c  e8cf440b00            call      00547140h
  00492c71  66894610              mov       [esi+10h],ax
  00492c75  66896e12              mov       [esi+12h],bp
  00492c79  eb38                  jmp       short 00492CB3h

LAB_00492c7b:
  00492c7b  c6464203              mov       byte [esi+42h],3
  00492c7f  66896e12              mov       [esi+12h],bp
  00492c83  e8b8440b00            call      00547140h
  00492c88  99                    cdq
  00492c89  b90a000000            mov       ecx,0Ah
  00492c8e  f7f9                  idiv      ecx
  00492c90  8bc7                  mov       eax,edi
  00492c92  d1f8                  sar       eax,1
  00492c94  02d0                  add       dl,al
  00492c96  80c214                add       dl,14h
  00492c99  885643                mov       [esi+43h],dl
  00492c9c  e89f440b00            call      00547140h
  00492ca1  99                    cdq
  00492ca2  33c2                  xor       eax,edx
  00492ca4  2bc2                  sub       eax,edx
  00492ca6  25ff0f0000            and       eax,0FFFh
  00492cab  33c2                  xor       eax,edx
  00492cad  2bc2                  sub       eax,edx
  00492caf  66894610              mov       [esi+10h],ax

LAB_00492cb3:
  00492cb3  817c241458020000      cmp       dword [esp+14h],258h
  00492cbb  0f8cf2000000          jl        near 00492DB3h
  00492cc1  e87a440b00            call      00547140h
  00492cc6  99                    cdq
  00492cc7  b964000000            mov       ecx,64h
  00492ccc  f7f9                  idiv      ecx
  00492cce  83fa1e                cmp       edx,1Eh
  00492cd1  7d70                  jge       short 00492D43h
  00492cd3  8b442410              mov       eax,[esp+10h]
  00492cd7  8d4c000a              lea       ecx,[eax+eax+0Ah]
  00492cdb  3bd1                  cmp       edx,ecx
  00492cdd  7d2e                  jge       short 00492D0Dh
  00492cdf  c6464202              mov       byte [esi+42h],2
  00492ce3  e858440b00            call      00547140h
  00492ce8  99                    cdq
  00492ce9  b90a000000            mov       ecx,0Ah
  00492cee  66c746106400          mov       word [esi+10h],64h
  00492cf4  f7f9                  idiv      ecx
  00492cf6  8bc7                  mov       eax,edi
  00492cf8  d1f8                  sar       eax,1
  00492cfa  02d0                  add       dl,al
  00492cfc  80c214                add       dl,14h
  00492cff  885643                mov       [esi+43h],dl
  00492d02  5f                    pop       edi
  00492d03  5e                    pop       esi
  00492d04  5d                    pop       ebp
  00492d05  5b                    pop       ebx
  00492d06  81c488000000          add       esp,88h
  00492d0c  c3                    ret

LAB_00492d0d:
  00492d0d  c6464200              mov       byte [esi+42h],0
  00492d11  e82a440b00            call      00547140h
  00492d16  99                    cdq
  00492d17  b90a000000            mov       ecx,0Ah
  00492d1c  66896e12              mov       [esi+12h],bp
  00492d20  f7f9                  idiv      ecx
  00492d22  8b4c2410              mov       ecx,[esp+10h]
  00492d26  8bc7                  mov       eax,edi
  00492d28  c1f802                sar       eax,2
  00492d2b  02d0                  add       dl,al
  00492d2d  885643                mov       [esi+43h],dl
  00492d30  8d540950              lea       edx,[ecx+ecx+50h]
  00492d34  66895610              mov       [esi+10h],dx
  00492d38  5f                    pop       edi
  00492d39  5e                    pop       esi
  00492d3a  5d                    pop       ebp
  00492d3b  5b                    pop       ebx
  00492d3c  81c488000000          add       esp,88h
  00492d42  c3                    ret

LAB_00492d43:
  00492d43  83fa28                cmp       edx,28h
  00492d46  7d43                  jge       short 00492D8Bh
  00492d48  c6464203              mov       byte [esi+42h],3
  00492d4c  66896e12              mov       [esi+12h],bp
  00492d50  e8eb430b00            call      00547140h
  00492d55  99                    cdq
  00492d56  b90a000000            mov       ecx,0Ah
  00492d5b  f7f9                  idiv      ecx
  00492d5d  8bc7                  mov       eax,edi
  00492d5f  d1f8                  sar       eax,1
  00492d61  02d0                  add       dl,al
  00492d63  80c214                add       dl,14h
  00492d66  885643                mov       [esi+43h],dl
  00492d69  e8d2430b00            call      00547140h
  00492d6e  99                    cdq
  00492d6f  33c2                  xor       eax,edx
  00492d71  2bc2                  sub       eax,edx
  00492d73  25ff0f0000            and       eax,0FFFh
  00492d78  33c2                  xor       eax,edx
  00492d7a  2bc2                  sub       eax,edx
  00492d7c  66894610              mov       [esi+10h],ax
  00492d80  5f                    pop       edi
  00492d81  5e                    pop       esi
  00492d82  5d                    pop       ebp
  00492d83  5b                    pop       ebx
  00492d84  81c488000000          add       esp,88h
  00492d8a  c3                    ret

LAB_00492d8b:
  00492d8b  83fa3c                cmp       edx,3Ch
  00492d8e  7d13                  jge       short 00492DA3h
  00492d90  c6464201              mov       byte [esi+42h],1
  00492d94  c6464301              mov       byte [esi+43h],1
  00492d98  5f                    pop       edi
  00492d99  5e                    pop       esi
  00492d9a  5d                    pop       ebp
  00492d9b  5b                    pop       ebx
  00492d9c  81c488000000          add       esp,88h
  00492da2  c3                    ret

LAB_00492da3:
  00492da3  885e42                mov       [esi+42h],bl
  00492da6  c6464301              mov       byte [esi+43h],1
  00492daa  e891430b00            call      00547140h
  00492daf  66894610              mov       [esi+10h],ax

LAB_00492db3:
  00492db3  5f                    pop       edi
  00492db4  5e                    pop       esi
  00492db5  5d                    pop       ebp
  00492db6  5b                    pop       ebx
  00492db7  81c488000000          add       esp,88h
  00492dbd  c3                    ret
  00492dbe  8bff                  mov       edi,edi
  00492dc0  7724                  ja        short 00492DE6h
  00492dc2  49                    dec       ecx
  00492dc3  00bf2449003c          add       [edi+3C004924h],bh
  00492dc9  254900e525            and       eax,25E50049h
  00492dce  49                    dec       ecx
  00492dcf  00f1                  add       cl,dh
  00492dd1  27                    daa
  00492dd2  49                    dec       ecx
  00492dd3  00f1                  add       cl,dh
  00492dd5  27                    daa
  00492dd6  49                    dec       ecx
  00492dd7  00c0                  add       al,al
  00492dd9  2649                  dec       ecx
  00492ddb  005b27                add       [ebx+27h],bl
  00492dde  49                    dec       ecx
  00492ddf  00f1                  add       cl,dh
  00492de1  27                    daa
  00492de2  49                    dec       ecx
  00492de3  00f1                  add       cl,dh
  00492de5  27                    daa

LAB_00492de6:
  00492de6  49                    dec       ecx
  00492de7  00f1                  add       cl,dh
  00492de9  27                    daa
  00492dea  49                    dec       ecx
  00492deb  00b5274900d6          add       [ebp-29FFB6D9h],dh
  00492df1  27                    daa
  00492df2  49                    dec       ecx
  00492df3  009090909090          add       [eax-6F6F6F70h],dl
  00492df9  90                    nop
  00492dfa  90                    nop
  00492dfb  90                    nop
  00492dfc  90                    nop
  00492dfd  90                    nop
  00492dfe  90                    nop
  00492dff  90                    nop
  00492e00  56                    push      esi
  00492e01  8b74240c              mov       esi,[esp+0Ch]
  00492e05  57                    push      edi
  00492e06  8b7c240c              mov       edi,[esp+0Ch]
  00492e0a  56                    push      esi
  00492e0b  57                    push      edi
  00492e0c  e81ffe0000            call      004A2C30h
  00492e11  83c408                add       esp,8
  00492e14  6685c0                test      ax,ax
  00492e17  7515                  jne       short 00492E2Eh
  00492e19  8b442414              mov       eax,[esp+14h]
  00492e1d  50                    push      eax
  00492e1e  56                    push      esi
  00492e1f  57                    push      edi
  00492e20  e89b850a00            call      0053B3C0h
  00492e25  83c40c                add       esp,0Ch
  00492e28  6633c0                xor       ax,ax
  00492e2b  5f                    pop       edi
  00492e2c  5e                    pop       esi
  00492e2d  c3                    ret

LAB_00492e2e:
  00492e2e  5f                    pop       edi
  00492e2f  66b80100              mov       ax,1
  00492e33  5e                    pop       esi
  00492e34  c3                    ret
  00492e35  90                    nop
  00492e36  90                    nop
  00492e37  90                    nop
  00492e38  90                    nop
  00492e39  90                    nop
  00492e3a  90                    nop
  00492e3b  90                    nop
  00492e3c  90                    nop
  00492e3d  90                    nop
  00492e3e  90                    nop
  00492e3f  90                    nop

56 cible(s) de saut interne : 0x004921f0, 0x00492340, 0x00492362, 0x004923d2, 0x004923d6, 0x0049243e, 0x00492443, 0x0049245e, 0x00492462, 0x00492499, 0x004924e2, 0x00492506, 0x00492525, 0x00492572, 0x00492592, 0x0049259e, 0x004925d3, 0x004925fc, 0x0049267d, 0x0049268f, 0x00492693, 0x004926d8, 0x0049274d, 0x00492779, 0x00492785, 0x004927b0, 0x004927ed, 0x004927f1, 0x004928d2, 0x004928de, 0x004928e0, 0x004928f1, 0x0049292f, 0x004929be, 0x004929ce, 0x004929f6, 0x00492a24, 0x00492b0a, 0x00492b33, 0x00492b42, 0x00492b87, 0x00492bb3, 0x00492bde, 0x00492be0, 0x00492be4, 0x00492c36, 0x00492c60, 0x00492c7b, 0x00492cb3, 0x00492d0d, 0x00492d43, 0x00492d8b, 0x00492da3, 0x00492db3, 0x00492de6, 0x00492e2e
