Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00487260  section .text  offset fichier 0x86660  1904 octets

  00487260  83ec2c                sub       esp,2Ch
  00487263  53                    push      ebx
  00487264  55                    push      ebp
  00487265  56                    push      esi
  00487266  8b74243c              mov       esi,[esp+3Ch]
  0048726a  8d442424              lea       eax,[esp+24h]
  0048726e  57                    push      edi
  0048726f  8d4c242c              lea       ecx,[esp+2Ch]
  00487273  50                    push      eax
  00487274  8d542428              lea       edx,[esp+28h]
  00487278  51                    push      ecx
  00487279  33ed                  xor       ebp,ebp
  0048727b  52                    push      edx
  0048727c  56                    push      esi
  0048727d  66896c2440            mov       [esp+40h],bp
  00487282  66896c2442            mov       [esp+42h],bp
  00487287  66c74424443200        mov       word [esp+44h],32h
  0048728e  66c74424466400        mov       word [esp+46h],64h
  00487295  66896c2448            mov       [esp+48h],bp
  0048729a  66c744244a5a00        mov       word [esp+4Ah],5Ah
  004872a1  e8da160b00            call      00538980h
  004872a6  83c410                add       esp,10h
  004872a9  8d442420              lea       eax,[esp+20h]
  004872ad  8d4c241c              lea       ecx,[esp+1Ch]
  004872b1  8d542440              lea       edx,[esp+40h]
  004872b5  50                    push      eax
  004872b6  51                    push      ecx
  004872b7  8d442420              lea       eax,[esp+20h]
  004872bb  52                    push      edx
  004872bc  8d4c2420              lea       ecx,[esp+20h]
  004872c0  50                    push      eax
  004872c1  51                    push      ecx
  004872c2  56                    push      esi
  004872c3  e858ec0a00            call      00535F20h
  004872c8  8b4c242c              mov       ecx,[esp+2Ch]
  004872cc  b840000000            mov       eax,40h
  004872d1  2bc1                  sub       eax,ecx
  004872d3  83c418                add       esp,18h
  004872d6  89442410              mov       [esp+10h],eax
  004872da  8b44241c              mov       eax,[esp+1Ch]
  004872de  3bc5                  cmp       eax,ebp
  004872e0  bf01000000            mov       edi,1
  004872e5  0f84e5030000          je        near 004876D0h
  004872eb  56                    push      esi
  004872ec  e8af420500            call      004DB5A0h
  004872f1  8b06                  mov       eax,[esi]
  004872f3  83c404                add       esp,4
  004872f6  a802                  test      al,2
  004872f8  0f849c000000          je        near 0048739Ah
  004872fe  8a4c2420              mov       cl,[esp+20h]
  00487302  bb10000000            mov       ebx,10h
  00487307  84cb                  test      bl,cl
  00487309  740d                  je        short 00487318h
  0048730b  24fd                  and       al,0FDh
  0048730d  c6464300              mov       byte [esi+43h],0
  00487311  8906                  mov       [esi],eax
  00487313  e9b8030000            jmp       004876D0h

LAB_00487318:
  00487318  66837e1a08            cmp       word [esi+1Ah],8
  0048731d  7c58                  jl        short 00487377h
  0048731f  8d542430              lea       edx,[esp+30h]
  00487323  55                    push      ebp
  00487324  8d461c                lea       eax,[esi+1Ch]
  00487327  52                    push      edx
  00487328  50                    push      eax
  00487329  56                    push      esi
  0048732a  e801290900            call      00519C30h
  0048732f  8bf8                  mov       edi,eax
  00487331  a1b8e05d00            mov       eax,[5DE0B8h]
  00487336  83c410                add       esp,10h
  00487339  3bc5                  cmp       eax,ebp
  0048733b  7430                  je        short 0048736Dh
  0048733d  66899f96000000        mov       [edi+96h],bx
  00487344  8b1f                  mov       ebx,[edi]
  00487346  83cb40                or        ebx,40h
  00487349  57                    push      edi
  0048734a  56                    push      esi
  0048734b  891f                  mov       [edi],ebx
  0048734d  e82e280b00            call      00539B80h
  00487352  83c408                add       esp,8
  00487355  83c71c                add       edi,1Ch
  00487358  57                    push      edi
  00487359  6800180000            push      1800h
  0048735e  6800030000            push      300h
  00487363  6a03                  push      3
  00487365  e8b6670b00            call      0053DB20h
  0048736a  83c410                add       esp,10h

LAB_0048736d:
  0048736d  8b06                  mov       eax,[esi]
  0048736f  a801                  test      al,1
  00487371  740c                  je        short 0048737Fh
  00487373  66ff4e1a              dec       word [esi+1Ah]

LAB_00487377:
  00487377  a801                  test      al,1
  00487379  0f8551030000          jne       near 004876D0h

LAB_0048737f:
  0048737f  a900000002            test      eax,2000000h
  00487384  0f841c020000          je        near 004875A6h
  0048738a  24fd                  and       al,0FDh
  0048738c  c6464300              mov       byte [esi+43h],0
  00487390  8906                  mov       [esi],eax
  00487392  896e5c                mov       [esi+5Ch],ebp
  00487395  e936030000            jmp       004876D0h

LAB_0048739a:
  0048739a  a810                  test      al,10h
  0048739c  0f852e030000          jne       near 004876D0h
  004873a2  a904080000            test      eax,804h
  004873a7  0f8452010000          je        near 004874FFh
  004873ad  f7442420e8030040      test      dword [esp+20h],400003E8h
  004873b5  740b                  je        short 004873C2h
  004873b7  25fbf7ffff            and       eax,0FFFFF7FBh
  004873bc  c6464300              mov       byte [esi+43h],0
  004873c0  8906                  mov       [esi],eax

LAB_004873c2:
  004873c2  668b4610              mov       ax,[esi+10h]
  004873c6  0fbfc8                movsx     ecx,ax
  004873c9  83f903                cmp       ecx,3
  004873cc  0f87fe020000          ja        near 004876D0h
  004873d2  ff248dd0794800        jmp       dword [ecx*4+4879D0h]
  004873d9  8b06                  mov       eax,[esi]
  004873db  a900000002            test      eax,2000000h
  004873e0  0f84ea020000          je        near 004876D0h
  004873e6  80e4f7                and       ah,0F7h
  004873e9  8906                  mov       [esi],eax
  004873eb  e9e0020000            jmp       004876D0h
  004873f0  b805000000            mov       eax,5
  004873f5  66394618              cmp       [esi+18h],ax
  004873f9  7411                  je        short 0048740Ch
  004873fb  894618                mov       [esi+18h],eax
  004873fe  8b06                  mov       eax,[esi]
  00487400  25fbfffffe            and       eax,0FEFFFFFBh
  00487405  8906                  mov       [esi],eax
  00487407  e9c4020000            jmp       004876D0h

LAB_0048740c:
  0048740c  66837e1a07            cmp       word [esi+1Ah],7
  00487411  0f85b9020000          jne       near 004876D0h
  00487417  33c0                  xor       eax,eax
  00487419  6aff                  push      0FFFFFFFFh
  0048741b  668b4606              mov       ax,[esi+6]
  0048741f  6aff                  push      0FFFFFFFFh
  00487421  8bc8                  mov       ecx,eax
  00487423  c1f808                sar       eax,8
  00487426  81e1ff000000          and       ecx,0FFh
  0048742c  25ff000000            and       eax,0FFh
  00487431  51                    push      ecx
  00487432  50                    push      eax
  00487433  56                    push      esi
  00487434  e847350b00            call      0053A980h
  00487439  83c414                add       esp,14h
  0048743c  66ff4e10              dec       word [esi+10h]
  00487440  e98b020000            jmp       004876D0h
  00487445  8a4e43                mov       cl,[esi+43h]
  00487448  fec9                  dec       cl
  0048744a  884e43                mov       [esi+43h],cl
  0048744d  7505                  jne       short 00487454h
  0048744f  48                    dec       eax
  00487450  66894610              mov       [esi+10h],ax

LAB_00487454:
  00487454  8b542414              mov       edx,[esp+14h]
  00487458  8b4c2440              mov       ecx,[esp+40h]
  0048745c  8d049532000000        lea       eax,[edx*4+32h]
  00487463  8d1408                lea       edx,[eax+ecx]
  00487466  8d3c00                lea       edi,[eax+eax]
  00487469  81e2ff0f0000          and       edx,0FFFh
  0048746f  3bd7                  cmp       edx,edi
  00487471  7f09                  jg        short 0048747Ch
  00487473  66014e32              add       [esi+32h],cx
  00487477  e954020000            jmp       004876D0h

LAB_0048747c:
  0048747c  81e1ff0f0000          and       ecx,0FFFh
  00487482  81f900080000          cmp       ecx,800h
  00487488  0f8c3e020000          jl        near 004876CCh
  0048748e  e937020000            jmp       004876CAh
  00487493  55                    push      ebp
  00487494  55                    push      ebp
  00487495  68bd000000            push      0BDh
  0048749a  56                    push      esi
  0048749b  e84036ffff            call      0047AAE0h
  004874a0  83c410                add       esp,10h
  004874a3  e898fc0b00            call      00547140h
  004874a8  99                    cdq
  004874a9  b903000000            mov       ecx,3
  004874ae  f7f9                  idiv      ecx
  004874b0  0fbf4e14              movsx     ecx,word [esi+14h]
  004874b4  33c0                  xor       eax,eax
  004874b6  8d144a                lea       edx,[edx+ecx*2]
  004874b9  03ca                  add       ecx,edx
  004874bb  668b044d68285500      mov       ax,[ecx*2+552868h]
  004874c3  66894606              mov       [esi+6],ax
  004874c7  8b15c4975e00          mov       edx,[5E97C4h]
  004874cd  8bc8                  mov       ecx,eax
  004874cf  25ff000000            and       eax,0FFh
  004874d4  8b929c000000          mov       edx,[edx+9Ch]
  004874da  c1f908                sar       ecx,8
  004874dd  81e1ff000000          and       ecx,0FFh
  004874e3  8d0440                lea       eax,[eax+eax*2]
  004874e6  c1e004                shl       eax,4
  004874e9  8b0c8a                mov       ecx,[edx+ecx*4]
  004874ec  8a540113              mov       dl,[ecx+eax+13h]
  004874f0  66ff4e10              dec       word [esi+10h]
  004874f4  885643                mov       [esi+43h],dl
  004874f7  896e5c                mov       [esi+5Ch],ebp
  004874fa  e9d1010000            jmp       004876D0h

LAB_004874ff:
  004874ff  a801                  test      al,1
  00487501  0f85c9010000          jne       near 004876D0h
  00487507  33c9                  xor       ecx,ecx
  00487509  8a4e42                mov       cl,[esi+42h]
  0048750c  83f903                cmp       ecx,3
  0048750f  0f87bb010000          ja        near 004876D0h
  00487515  ff248de0794800        jmp       dword [ecx*4+4879E0h]
  0048751c  66396e18              cmp       [esi+18h],bp
  00487520  740a                  je        short 0048752Ch
  00487522  0d00000001            or        eax,1000000h
  00487527  896e18                mov       [esi+18h],ebp
  0048752a  8906                  mov       [esi],eax

LAB_0048752c:
  0048752c  817c24182c010000      cmp       dword [esp+18h],12Ch
  00487534  7d09                  jge       short 0048753Fh
  00487536  c6464300              mov       byte [esi+43h],0
  0048753a  e991010000            jmp       004876D0h

LAB_0048753f:
  0048753f  8a4643                mov       al,[esi+43h]
  00487542  84c0                  test      al,al
  00487544  7460                  je        short 004875A6h
  00487546  fec8                  dec       al
  00487548  896e5c                mov       [esi+5Ch],ebp
  0048754b  884643                mov       [esi+43h],al
  0048754e  e97d010000            jmp       004876D0h
  00487553  66397e18              cmp       [esi+18h],di
  00487557  740a                  je        short 00487563h
  00487559  0d00000001            or        eax,1000000h
  0048755e  897e18                mov       [esi+18h],edi
  00487561  8906                  mov       [esi],eax

LAB_00487563:
  00487563  8b442414              mov       eax,[esp+14h]
  00487567  8b4c2440              mov       ecx,[esp+40h]
  0048756b  8d840080000000        lea       eax,[eax+eax+80h]
  00487572  8d1408                lea       edx,[eax+ecx]
  00487575  8d3c00                lea       edi,[eax+eax]
  00487578  81e2ff0f0000          and       edx,0FFFh
  0048757e  3bd7                  cmp       edx,edi
  00487580  7f10                  jg        short 00487592h
  00487582  66014e32              add       [esi+32h],cx
  00487586  c6464300              mov       byte [esi+43h],0
  0048758a  896e5c                mov       [esi+5Ch],ebp
  0048758d  e93e010000            jmp       004876D0h

LAB_00487592:
  00487592  81e1ff0f0000          and       ecx,0FFFh
  00487598  81f900080000          cmp       ecx,800h
  0048759e  7c02                  jl        short 004875A2h
  004875a0  f7d8                  neg       eax

LAB_004875a2:
  004875a2  66014632              add       [esi+32h],ax

LAB_004875a6:
  004875a6  896e5c                mov       [esi+5Ch],ebp
  004875a9  e922010000            jmp       004876D0h
  004875ae  66397e18              cmp       [esi+18h],di
  004875b2  740a                  je        short 004875BEh
  004875b4  0d00000001            or        eax,1000000h
  004875b9  897e18                mov       [esi+18h],edi
  004875bc  8906                  mov       [esi],eax

LAB_004875be:
  004875be  0fbf4610              movsx     eax,word [esi+10h]
  004875c2  39442418              cmp       [esp+18h],eax
  004875c6  7d09                  jge       short 004875D1h
  004875c8  c6464300              mov       byte [esi+43h],0
  004875cc  e9ff000000            jmp       004876D0h

LAB_004875d1:
  004875d1  8a4643                mov       al,[esi+43h]
  004875d4  84c0                  test      al,al
  004875d6  7405                  je        short 004875DDh
  004875d8  fec8                  dec       al
  004875da  884643                mov       [esi+43h],al

LAB_004875dd:
  004875dd  8b542414              mov       edx,[esp+14h]
  004875e1  8b4c2440              mov       ecx,[esp+40h]
  004875e5  8d841280000000        lea       eax,[edx+edx+80h]
  004875ec  8d3c08                lea       edi,[eax+ecx]
  004875ef  8d1c00                lea       ebx,[eax+eax]
  004875f2  81e7ff0f0000          and       edi,0FFFh
  004875f8  3bfb                  cmp       edi,ebx
  004875fa  7f15                  jg        short 00487611h
  004875fc  66014e32              add       [esi+32h],cx
  00487600  81c2c0000000          add       edx,0C0h
  00487606  c1e207                shl       edx,7
  00487609  89565c                mov       [esi+5Ch],edx
  0048760c  e9bf000000            jmp       004876D0h

LAB_00487611:
  00487611  81e1ff0f0000          and       ecx,0FFFh
  00487617  81f900080000          cmp       ecx,800h
  0048761d  7c02                  jl        short 00487621h
  0048761f  f7d8                  neg       eax

LAB_00487621:
  00487621  66014632              add       [esi+32h],ax
  00487625  81c2c0000000          add       edx,0C0h
  0048762b  c1e207                shl       edx,7
  0048762e  89565c                mov       [esi+5Ch],edx
  00487631  e99a000000            jmp       004876D0h
  00487636  8a4e43                mov       cl,[esi+43h]
  00487639  84c9                  test      cl,cl
  0048763b  7405                  je        short 00487642h
  0048763d  fec9                  dec       cl
  0048763f  884e43                mov       [esi+43h],cl

LAB_00487642:
  00487642  66397e18              cmp       [esi+18h],di
  00487646  740a                  je        short 00487652h
  00487648  0d00000001            or        eax,1000000h
  0048764d  897e18                mov       [esi+18h],edi
  00487650  8906                  mov       [esi],eax

LAB_00487652:
  00487652  8b4c2414              mov       ecx,[esp+14h]
  00487656  0fbf5610              movsx     edx,word [esi+10h]
  0048765a  81c180000000          add       ecx,80h
  00487660  52                    push      edx
  00487661  c1e107                shl       ecx,7
  00487664  894e5c                mov       [esi+5Ch],ecx
  00487667  e8b0e30b00            call      00545A1Ch
  0048766c  0faf465c              imul      eax,[esi+5Ch]
  00487670  8b7e1c                mov       edi,[esi+1Ch]
  00487673  c1f80c                sar       eax,0Ch
  00487676  03f8                  add       edi,eax
  00487678  0fbf4610              movsx     eax,word [esi+10h]
  0048767c  50                    push      eax
  0048767d  897e1c                mov       [esi+1Ch],edi
  00487680  e891e30b00            call      00545A16h
  00487685  0faf465c              imul      eax,[esi+5Ch]
  00487689  8b5e24                mov       ebx,[esi+24h]
  0048768c  8b4c2414              mov       ecx,[esp+14h]
  00487690  c1f80c                sar       eax,0Ch
  00487693  03d8                  add       ebx,eax
  00487695  8d048d32000000        lea       eax,[ecx*4+32h]
  0048769c  8b4c2440              mov       ecx,[esp+40h]
  004876a0  895e24                mov       [esi+24h],ebx
  004876a3  8d3c00                lea       edi,[eax+eax]
  004876a6  896e5c                mov       [esi+5Ch],ebp
  004876a9  8d1408                lea       edx,[eax+ecx]
  004876ac  81e2ff0f0000          and       edx,0FFFh
  004876b2  3bd7                  cmp       edx,edi
  004876b4  7f06                  jg        short 004876BCh
  004876b6  66014e32              add       [esi+32h],cx
  004876ba  eb14                  jmp       short 004876D0h

LAB_004876bc:
  004876bc  81e1ff0f0000          and       ecx,0FFFh
  004876c2  81f900080000          cmp       ecx,800h
  004876c8  7c02                  jl        short 004876CCh

LAB_004876ca:
  004876ca  f7d8                  neg       eax

LAB_004876cc:
  004876cc  66014632              add       [esi+32h],ax

LAB_004876d0:
  004876d0  8b44241c              mov       eax,[esp+1Ch]
  004876d4  50                    push      eax
  004876d5  56                    push      esi
  004876d6  e8850a0000            call      00488160h
  004876db  83c408                add       esp,8
  004876de  56                    push      esi
  004876df  e85cc20a00            call      00533940h
  004876e4  83c404                add       esp,4
  004876e7  56                    push      esi
  004876e8  e803840500            call      004DFAF0h
  004876ed  8b4c2428              mov       ecx,[esp+28h]
  004876f1  83c404                add       esp,4
  004876f4  51                    push      ecx
  004876f5  56                    push      esi
  004876f6  e875c00500            call      004E3770h
  004876fb  8b442424              mov       eax,[esp+24h]
  004876ff  83c408                add       esp,8
  00487702  3bc5                  cmp       eax,ebp
  00487704  0f84be020000          je        near 004879C8h
  0048770a  f70617080000          test      dword [esi],817h
  00487710  0f85b2020000          jne       near 004879C8h
  00487716  8a4643                mov       al,[esi+43h]
  00487719  84c0                  test      al,al
  0048771b  0f85a7020000          jne       near 004879C8h
  00487721  8b466c                mov       eax,[esi+6Ch]
  00487724  8b5e1c                mov       ebx,[esi+1Ch]
  00487727  8b4e24                mov       ecx,[esi+24h]
  0048772a  8b781c                mov       edi,[eax+1Ch]
  0048772d  8b6824                mov       ebp,[eax+24h]
  00487730  2bfb                  sub       edi,ebx
  00487732  2be9                  sub       ebp,ecx
  00487734  c1ff0c                sar       edi,0Ch
  00487737  c1fd0c                sar       ebp,0Ch
  0048773a  8bd7                  mov       edx,edi
  0048773c  8bc5                  mov       eax,ebp
  0048773e  0fafd7                imul      edx,edi
  00487741  0fafc5                imul      eax,ebp
  00487744  03d0                  add       edx,eax
  00487746  52                    push      edx
  00487747  e8c4e20b00            call      00545A10h
  0048774c  55                    push      ebp
  0048774d  57                    push      edi
  0048774e  89442420              mov       [esp+20h],eax
  00487752  e8cbe20b00            call      00545A22h
  00487757  668b4e32              mov       cx,[esi+32h]
  0048775b  2bc1                  sub       eax,ecx
  0048775d  25ff0f0000            and       eax,0FFFh
  00487762  89442440              mov       [esp+40h],eax
  00487766  8b442418              mov       eax,[esp+18h]
  0048776a  3dfa000000            cmp       eax,0FAh
  0048776f  0f8dd0000000          jge       near 00487845h
  00487775  e8c6f90b00            call      00547140h
  0048777a  99                    cdq
  0048777b  b964000000            mov       ecx,64h
  00487780  f7f9                  idiv      ecx
  00487782  83fa1e                cmp       edx,1Eh
  00487785  7d61                  jge       short 004877E8h
  00487787  8b542440              mov       edx,[esp+40h]
  0048778b  8b7c2410              mov       edi,[esp+10h]
  0048778f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00487796  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048779d  25ff0f0000            and       eax,0FFFh
  004877a2  3bc1                  cmp       eax,ecx
  004877a4  0f8fba010000          jg        near 00487964h
  004877aa  e891f90b00            call      00547140h
  004877af  99                    cdq
  004877b0  8d4c3f01              lea       ecx,[edi+edi+1]
  004877b4  f7f9                  idiv      ecx
  004877b6  8b442418              mov       eax,[esp+18h]
  004877ba  83c250                add       edx,50h
  004877bd  3bc2                  cmp       eax,edx
  004877bf  0f8d9f010000          jge       near 00487964h
  004877c5  8b16                  mov       edx,[esi]
  004877c7  c7461803000000        mov       dword [esi+18h],3
  004877ce  81e2fffffffe          and       edx,0FEFFFFFFh
  004877d4  c7465c00000000        mov       dword [esi+5Ch],0
  004877db  83ca02                or        edx,2
  004877de  8916                  mov       [esi],edx
  004877e0  5f                    pop       edi
  004877e1  5e                    pop       esi
  004877e2  5d                    pop       ebp
  004877e3  5b                    pop       ebx
  004877e4  83c42c                add       esp,2Ch
  004877e7  c3                    ret

LAB_004877e8:
  004877e8  83fa3c                cmp       edx,3Ch
  004877eb  0f8cf6000000          jl        near 004878E7h
  004877f1  c6464203              mov       byte [esi+42h],3
  004877f5  e846f90b00            call      00547140h
  004877fa  99                    cdq
  004877fb  b90a000000            mov       ecx,0Ah
  00487800  f7f9                  idiv      ecx
  00487802  8b442410              mov       eax,[esp+10h]
  00487806  8b4c2414              mov       ecx,[esp+14h]
  0048780a  d1f8                  sar       eax,1
  0048780c  8db980000000          lea       edi,[ecx+80h]
  00487812  c1e704                shl       edi,4
  00487815  02d0                  add       dl,al
  00487817  80c214                add       dl,14h
  0048781a  885643                mov       [esi+43h],dl
  0048781d  e81ef90b00            call      00547140h
  00487822  99                    cdq
  00487823  f7ff                  idiv      edi
  00487825  d1ff                  sar       edi,1
  00487827  2bd7                  sub       edx,edi
  00487829  8b7c2440              mov       edi,[esp+40h]
  0048782d  03d7                  add       edx,edi
  0048782f  66035632              add       dx,[esi+32h]
  00487833  81c200080000          add       edx,800h
  00487839  66895610              mov       [esi+10h],dx
  0048783d  5f                    pop       edi
  0048783e  5e                    pop       esi
  0048783f  5d                    pop       ebp
  00487840  5b                    pop       ebx
  00487841  83c42c                add       esp,2Ch
  00487844  c3                    ret

LAB_00487845:
  00487845  3df4010000            cmp       eax,1F4h
  0048784a  0f8dc4000000          jge       near 00487914h
  00487850  e8ebf80b00            call      00547140h
  00487855  99                    cdq
  00487856  b964000000            mov       ecx,64h
  0048785b  f7f9                  idiv      ecx
  0048785d  83fa2d                cmp       edx,2Dh
  00487860  7d29                  jge       short 0048788Bh
  00487862  8b542440              mov       edx,[esp+40h]
  00487866  8b442410              mov       eax,[esp+10h]
  0048786a  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  00487871  8d14c500020000        lea       edx,[eax*8+200h]
  00487878  81e1ff0f0000          and       ecx,0FFFh
  0048787e  3bca                  cmp       ecx,edx
  00487880  0f8fde000000          jg        near 00487964h
  00487886  e9bb000000            jmp       00487946h

LAB_0048788b:
  0048788b  83fa46                cmp       edx,46h
  0048788e  7d4e                  jge       short 004878DEh
  00487890  c6464203              mov       byte [esi+42h],3
  00487894  e8a7f80b00            call      00547140h
  00487899  99                    cdq
  0048789a  b90a000000            mov       ecx,0Ah
  0048789f  f7f9                  idiv      ecx
  004878a1  8b442410              mov       eax,[esp+10h]
  004878a5  8b4c2414              mov       ecx,[esp+14h]
  004878a9  d1f8                  sar       eax,1
  004878ab  8db980000000          lea       edi,[ecx+80h]
  004878b1  c1e704                shl       edi,4
  004878b4  02d0                  add       dl,al
  004878b6  80c214                add       dl,14h
  004878b9  885643                mov       [esi+43h],dl
  004878bc  e87ff80b00            call      00547140h
  004878c1  99                    cdq
  004878c2  f7ff                  idiv      edi
  004878c4  8b4c2440              mov       ecx,[esp+40h]
  004878c8  d1ff                  sar       edi,1
  004878ca  2bd7                  sub       edx,edi
  004878cc  03d1                  add       edx,ecx
  004878ce  66035632              add       dx,[esi+32h]
  004878d2  66895610              mov       [esi+10h],dx
  004878d6  5f                    pop       edi
  004878d7  5e                    pop       esi
  004878d8  5d                    pop       ebp
  004878d9  5b                    pop       ebx
  004878da  83c42c                add       esp,2Ch
  004878dd  c3                    ret

LAB_004878de:
  004878de  83fa5a                cmp       edx,5Ah
  004878e1  0f8d92000000          jge       near 00487979h

LAB_004878e7:
  004878e7  c6464202              mov       byte [esi+42h],2
  004878eb  e850f80b00            call      00547140h
  004878f0  99                    cdq
  004878f1  b90a000000            mov       ecx,0Ah
  004878f6  66c746106400          mov       word [esi+10h],64h
  004878fc  f7f9                  idiv      ecx
  004878fe  8b442410              mov       eax,[esp+10h]
  00487902  d1f8                  sar       eax,1
  00487904  02d0                  add       dl,al
  00487906  80c214                add       dl,14h
  00487909  885643                mov       [esi+43h],dl
  0048790c  5f                    pop       edi
  0048790d  5e                    pop       esi
  0048790e  5d                    pop       ebp
  0048790f  5b                    pop       ebx
  00487910  83c42c                add       esp,2Ch
  00487913  c3                    ret

LAB_00487914:
  00487914  e827f80b00            call      00547140h
  00487919  99                    cdq
  0048791a  b964000000            mov       ecx,64h
  0048791f  f7f9                  idiv      ecx
  00487921  83fa1e                cmp       edx,1Eh
  00487924  7d4e                  jge       short 00487974h
  00487926  8b542440              mov       edx,[esp+40h]
  0048792a  8b442410              mov       eax,[esp+10h]
  0048792e  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  00487935  8d14c500020000        lea       edx,[eax*8+200h]
  0048793c  81e1ff0f0000          and       ecx,0FFFh
  00487942  3bca                  cmp       ecx,edx
  00487944  7f1e                  jg        short 00487964h

LAB_00487946:
  00487946  8b06                  mov       eax,[esi]
  00487948  c7461804000000        mov       dword [esi+18h],4
  0048794f  0d04080001            or        eax,1000804h
  00487954  66c746100300          mov       word [esi+10h],3
  0048795a  8906                  mov       [esi],eax
  0048795c  5f                    pop       edi
  0048795d  5e                    pop       esi
  0048795e  5d                    pop       ebp
  0048795f  5b                    pop       ebx
  00487960  83c42c                add       esp,2Ch
  00487963  c3                    ret

LAB_00487964:
  00487964  c6464201              mov       byte [esi+42h],1
  00487968  c6464301              mov       byte [esi+43h],1
  0048796c  5f                    pop       edi
  0048796d  5e                    pop       esi
  0048796e  5d                    pop       ebp
  0048796f  5b                    pop       ebx
  00487970  83c42c                add       esp,2Ch
  00487973  c3                    ret

LAB_00487974:
  00487974  83fa3c                cmp       edx,3Ch
  00487977  7d27                  jge       short 004879A0h

LAB_00487979:
  00487979  c6464200              mov       byte [esi+42h],0
  0048797d  e8bef70b00            call      00547140h
  00487982  99                    cdq
  00487983  b90a000000            mov       ecx,0Ah
  00487988  f7f9                  idiv      ecx
  0048798a  8b442410              mov       eax,[esp+10h]
  0048798e  d1f8                  sar       eax,1
  00487990  02d0                  add       dl,al
  00487992  80c214                add       dl,14h
  00487995  885643                mov       [esi+43h],dl
  00487998  5f                    pop       edi
  00487999  5e                    pop       esi
  0048799a  5d                    pop       ebp
  0048799b  5b                    pop       ebx
  0048799c  83c42c                add       esp,2Ch
  0048799f  c3                    ret

LAB_004879a0:
  004879a0  c6464203              mov       byte [esi+42h],3
  004879a4  e897f70b00            call      00547140h
  004879a9  99                    cdq
  004879aa  b90a000000            mov       ecx,0Ah
  004879af  f7f9                  idiv      ecx
  004879b1  8b442410              mov       eax,[esp+10h]
  004879b5  d1f8                  sar       eax,1
  004879b7  02d0                  add       dl,al
  004879b9  80c214                add       dl,14h
  004879bc  885643                mov       [esi+43h],dl
  004879bf  e87cf70b00            call      00547140h
  004879c4  66894610              mov       [esi+10h],ax

LAB_004879c8:
  004879c8  5f                    pop       edi
  004879c9  5e                    pop       esi
  004879ca  5d                    pop       ebp
  004879cb  5b                    pop       ebx
  004879cc  83c42c                add       esp,2Ch
  004879cf  c3                    ret

39 cible(s) de saut interne : 0x00487318, 0x0048736d, 0x00487377, 0x0048737f, 0x0048739a, 0x004873c2, 0x0048740c, 0x00487454, 0x0048747c, 0x004874ff, 0x0048752c, 0x0048753f, 0x00487563, 0x00487592, 0x004875a2, 0x004875a6, 0x004875be, 0x004875d1, 0x004875dd, 0x00487611, 0x00487621, 0x00487642, 0x00487652, 0x004876bc, 0x004876ca, 0x004876cc, 0x004876d0, 0x004877e8, 0x00487845, 0x0048788b, 0x004878de, 0x004878e7, 0x00487914, 0x00487946, 0x00487964, 0x00487974, 0x00487979, 0x004879a0, 0x004879c8
