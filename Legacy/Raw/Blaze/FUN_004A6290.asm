Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a6290  section .text  offset fichier 0xa5690  1904 octets

  004a6290  83ec2c                sub       esp,2Ch
  004a6293  53                    push      ebx
  004a6294  55                    push      ebp
  004a6295  56                    push      esi
  004a6296  8b74243c              mov       esi,[esp+3Ch]
  004a629a  8d442424              lea       eax,[esp+24h]
  004a629e  57                    push      edi
  004a629f  8d4c242c              lea       ecx,[esp+2Ch]
  004a62a3  50                    push      eax
  004a62a4  8d542428              lea       edx,[esp+28h]
  004a62a8  51                    push      ecx
  004a62a9  33ed                  xor       ebp,ebp
  004a62ab  52                    push      edx
  004a62ac  56                    push      esi
  004a62ad  66896c2440            mov       [esp+40h],bp
  004a62b2  66c74424421e00        mov       word [esp+42h],1Eh
  004a62b9  66c74424443200        mov       word [esp+44h],32h
  004a62c0  66c74424466400        mov       word [esp+46h],64h
  004a62c7  66896c2448            mov       [esp+48h],bp
  004a62cc  66c744244a4600        mov       word [esp+4Ah],46h
  004a62d3  e8a8260900            call      00538980h
  004a62d8  83c410                add       esp,10h
  004a62db  8d442420              lea       eax,[esp+20h]
  004a62df  8d4c241c              lea       ecx,[esp+1Ch]
  004a62e3  8d542410              lea       edx,[esp+10h]
  004a62e7  50                    push      eax
  004a62e8  51                    push      ecx
  004a62e9  8d442420              lea       eax,[esp+20h]
  004a62ed  52                    push      edx
  004a62ee  8d4c244c              lea       ecx,[esp+4Ch]
  004a62f2  50                    push      eax
  004a62f3  51                    push      ecx
  004a62f4  56                    push      esi
  004a62f5  e826fc0800            call      00535F20h
  004a62fa  8b4c2458              mov       ecx,[esp+58h]
  004a62fe  b840000000            mov       eax,40h
  004a6303  2bc1                  sub       eax,ecx
  004a6305  83c418                add       esp,18h
  004a6308  89442414              mov       [esp+14h],eax
  004a630c  8b44241c              mov       eax,[esp+1Ch]
  004a6310  3bc5                  cmp       eax,ebp
  004a6312  bf01000000            mov       edi,1
  004a6317  0f844f030000          je        near 004A666Ch
  004a631d  56                    push      esi
  004a631e  e87d520300            call      004DB5A0h
  004a6323  8b06                  mov       eax,[esi]
  004a6325  83c404                add       esp,4
  004a6328  a802                  test      al,2
  004a632a  0f84cf000000          je        near 004A63FFh
  004a6330  8a4c2420              mov       cl,[esp+20h]
  004a6334  bb10000000            mov       ebx,10h
  004a6339  84cb                  test      bl,cl
  004a633b  740d                  je        short 004A634Ah
  004a633d  24fd                  and       al,0FDh
  004a633f  c6464300              mov       byte [esi+43h],0
  004a6343  8906                  mov       [esi],eax
  004a6345  e922030000            jmp       004A666Ch

LAB_004a634a:
  004a634a  33c9                  xor       ecx,ecx
  004a634c  8a4e42                mov       cl,[esi+42h]
  004a634f  2bcd                  sub       ecx,ebp
  004a6351  7432                  je        short 004A6385h
  004a6353  49                    dec       ecx
  004a6354  0f8512030000          jne       near 004A666Ch
  004a635a  66837e1a07            cmp       word [esi+1Ah],7
  004a635f  750e                  jne       short 004A636Fh
  004a6361  6aff                  push      0FFFFFFFFh
  004a6363  55                    push      ebp
  004a6364  6a49                  push      49h
  004a6366  56                    push      esi
  004a6367  e86446fdff            call      0047A9D0h
  004a636c  83c410                add       esp,10h

LAB_004a636f:
  004a636f  8b06                  mov       eax,[esi]
  004a6371  a900000002            test      eax,2000000h
  004a6376  0f84f0020000          je        near 004A666Ch
  004a637c  24fd                  and       al,0FDh
  004a637e  8906                  mov       [esi],eax
  004a6380  e9e7020000            jmp       004A666Ch

LAB_004a6385:
  004a6385  66837e1a09            cmp       word [esi+1Ah],9
  004a638a  7557                  jne       short 004A63E3h
  004a638c  8d542430              lea       edx,[esp+30h]
  004a6390  55                    push      ebp
  004a6391  8d461c                lea       eax,[esi+1Ch]
  004a6394  52                    push      edx
  004a6395  50                    push      eax
  004a6396  56                    push      esi
  004a6397  e894380700            call      00519C30h
  004a639c  8bf8                  mov       edi,eax
  004a639e  a1b8e05d00            mov       eax,[5DE0B8h]
  004a63a3  83c410                add       esp,10h
  004a63a6  3bc5                  cmp       eax,ebp
  004a63a8  742f                  je        short 004A63D9h
  004a63aa  57                    push      edi
  004a63ab  56                    push      esi
  004a63ac  e8cf370900            call      00539B80h
  004a63b1  8b07                  mov       eax,[edi]
  004a63b3  83c408                add       esp,8
  004a63b6  0c40                  or        al,40h
  004a63b8  66899f96000000        mov       [edi+96h],bx
  004a63bf  8907                  mov       [edi],eax
  004a63c1  83c71c                add       edi,1Ch
  004a63c4  57                    push      edi
  004a63c5  6800180000            push      1800h
  004a63ca  6800030000            push      300h
  004a63cf  6a03                  push      3
  004a63d1  e84a770900            call      0053DB20h
  004a63d6  83c410                add       esp,10h

LAB_004a63d9:
  004a63d9  8b06                  mov       eax,[esi]
  004a63db  a801                  test      al,1
  004a63dd  740c                  je        short 004A63EBh
  004a63df  66ff4e1a              dec       word [esi+1Ah]

LAB_004a63e3:
  004a63e3  a801                  test      al,1
  004a63e5  0f8581020000          jne       near 004A666Ch

LAB_004a63eb:
  004a63eb  a900000002            test      eax,2000000h
  004a63f0  0f8473020000          je        near 004A6669h
  004a63f6  24fd                  and       al,0FDh
  004a63f8  8906                  mov       [esi],eax
  004a63fa  e96a020000            jmp       004A6669h

LAB_004a63ff:
  004a63ff  a810                  test      al,10h
  004a6401  0f8565020000          jne       near 004A666Ch
  004a6407  a904080000            test      eax,804h
  004a640c  0f855a020000          jne       near 004A666Ch
  004a6412  a801                  test      al,1
  004a6414  7439                  je        short 004A644Fh
  004a6416  e8250d0a00            call      00547140h
  004a641b  99                    cdq
  004a641c  b964000000            mov       ecx,64h
  004a6421  f7f9                  idiv      ecx
  004a6423  8b442440              mov       eax,[esp+40h]
  004a6427  c1f802                sar       eax,2
  004a642a  3bd0                  cmp       edx,eax
  004a642c  0f8d3a020000          jge       near 004A666Ch
  004a6432  8b0e                  mov       ecx,[esi]
  004a6434  c7461803000000        mov       dword [esi+18h],3
  004a643b  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a6441  c6464200              mov       byte [esi+42h],0
  004a6445  83c902                or        ecx,2
  004a6448  890e                  mov       [esi],ecx
  004a644a  e91a020000            jmp       004A6669h

LAB_004a644f:
  004a644f  33c9                  xor       ecx,ecx
  004a6451  8a4e42                mov       cl,[esi+42h]
  004a6454  83f904                cmp       ecx,4
  004a6457  0f870f020000          ja        near 004A666Ch
  004a645d  ff248dec694a00        jmp       dword [ecx*4+4A69ECh]
  004a6464  66396e18              cmp       [esi+18h],bp
  004a6468  740a                  je        short 004A6474h
  004a646a  0d00000001            or        eax,1000000h
  004a646f  896e18                mov       [esi+18h],ebp
  004a6472  8906                  mov       [esi],eax

LAB_004a6474:
  004a6474  817c24182c010000      cmp       dword [esp+18h],12Ch
  004a647c  7d09                  jge       short 004A6487h
  004a647e  c6464300              mov       byte [esi+43h],0
  004a6482  e9e5010000            jmp       004A666Ch

LAB_004a6487:
  004a6487  8a4643                mov       al,[esi+43h]
  004a648a  84c0                  test      al,al
  004a648c  0f84d7010000          je        near 004A6669h
  004a6492  fec8                  dec       al
  004a6494  884643                mov       [esi+43h],al
  004a6497  e9cd010000            jmp       004A6669h
  004a649c  66397e18              cmp       [esi+18h],di
  004a64a0  740a                  je        short 004A64ACh
  004a64a2  0d00000001            or        eax,1000000h
  004a64a7  897e18                mov       [esi+18h],edi
  004a64aa  8906                  mov       [esi],eax

LAB_004a64ac:
  004a64ac  8b542440              mov       edx,[esp+40h]
  004a64b0  8b4c2410              mov       ecx,[esp+10h]
  004a64b4  8d841280000000        lea       eax,[edx+edx+80h]
  004a64bb  8d1408                lea       edx,[eax+ecx]
  004a64be  8d3c00                lea       edi,[eax+eax]
  004a64c1  81e2ff0f0000          and       edx,0FFFh
  004a64c7  3bd7                  cmp       edx,edi
  004a64c9  0f8f20010000          jg        near 004A65EFh
  004a64cf  66014e32              add       [esi+32h],cx
  004a64d3  c6464300              mov       byte [esi+43h],0
  004a64d7  e98d010000            jmp       004A6669h
  004a64dc  66397e18              cmp       [esi+18h],di
  004a64e0  740a                  je        short 004A64ECh
  004a64e2  0d00000001            or        eax,1000000h
  004a64e7  897e18                mov       [esi+18h],edi
  004a64ea  8906                  mov       [esi],eax

LAB_004a64ec:
  004a64ec  0fbf4610              movsx     eax,word [esi+10h]
  004a64f0  39442418              cmp       [esp+18h],eax
  004a64f4  7d09                  jge       short 004A64FFh
  004a64f6  c6464300              mov       byte [esi+43h],0
  004a64fa  e96d010000            jmp       004A666Ch

LAB_004a64ff:
  004a64ff  8a4643                mov       al,[esi+43h]
  004a6502  84c0                  test      al,al
  004a6504  7405                  je        short 004A650Bh
  004a6506  fec8                  dec       al
  004a6508  884643                mov       [esi+43h],al

LAB_004a650b:
  004a650b  8b542440              mov       edx,[esp+40h]
  004a650f  8b4c2410              mov       ecx,[esp+10h]
  004a6513  8d841280000000        lea       eax,[edx+edx+80h]
  004a651a  8d3c08                lea       edi,[eax+ecx]
  004a651d  8d1c00                lea       ebx,[eax+eax]
  004a6520  81e7ff0f0000          and       edi,0FFFh
  004a6526  3bfb                  cmp       edi,ebx
  004a6528  7f15                  jg        short 004A653Fh
  004a652a  66014e32              add       [esi+32h],cx
  004a652e  81c2c0000000          add       edx,0C0h
  004a6534  c1e207                shl       edx,7
  004a6537  89565c                mov       [esi+5Ch],edx
  004a653a  e92d010000            jmp       004A666Ch

LAB_004a653f:
  004a653f  81e1ff0f0000          and       ecx,0FFFh
  004a6545  81f900080000          cmp       ecx,800h
  004a654b  7c02                  jl        short 004A654Fh
  004a654d  f7d8                  neg       eax

LAB_004a654f:
  004a654f  66014632              add       [esi+32h],ax
  004a6553  81c2c0000000          add       edx,0C0h
  004a6559  c1e207                shl       edx,7
  004a655c  89565c                mov       [esi+5Ch],edx
  004a655f  e908010000            jmp       004A666Ch
  004a6564  8a4e43                mov       cl,[esi+43h]
  004a6567  84c9                  test      cl,cl
  004a6569  7405                  je        short 004A6570h
  004a656b  fec9                  dec       cl
  004a656d  884e43                mov       [esi+43h],cl

LAB_004a6570:
  004a6570  66397e18              cmp       [esi+18h],di
  004a6574  740a                  je        short 004A6580h
  004a6576  0d00000001            or        eax,1000000h
  004a657b  897e18                mov       [esi+18h],edi
  004a657e  8906                  mov       [esi],eax

LAB_004a6580:
  004a6580  8b4c2440              mov       ecx,[esp+40h]
  004a6584  8b7c241c              mov       edi,[esp+1Ch]
  004a6588  0fbf5610              movsx     edx,word [esi+10h]
  004a658c  8d8180000000          lea       eax,[ecx+80h]
  004a6592  52                    push      edx
  004a6593  c1e007                shl       eax,7
  004a6596  0faff8                imul      edi,eax
  004a6599  89465c                mov       [esi+5Ch],eax
  004a659c  c1ff0c                sar       edi,0Ch
  004a659f  e878f40900            call      00545A1Ch
  004a65a4  0fafc7                imul      eax,edi
  004a65a7  8b5e1c                mov       ebx,[esi+1Ch]
  004a65aa  c1f80c                sar       eax,0Ch
  004a65ad  03d8                  add       ebx,eax
  004a65af  0fbf4610              movsx     eax,word [esi+10h]
  004a65b3  50                    push      eax
  004a65b4  895e1c                mov       [esi+1Ch],ebx
  004a65b7  e85af40900            call      00545A16h
  004a65bc  0fafc7                imul      eax,edi
  004a65bf  8b5e24                mov       ebx,[esi+24h]
  004a65c2  8b4c2440              mov       ecx,[esp+40h]
  004a65c6  c1f80c                sar       eax,0Ch
  004a65c9  03d8                  add       ebx,eax
  004a65cb  8d048d32000000        lea       eax,[ecx*4+32h]
  004a65d2  8b4c2410              mov       ecx,[esp+10h]
  004a65d6  895e24                mov       [esi+24h],ebx
  004a65d9  8d3c00                lea       edi,[eax+eax]
  004a65dc  8d1408                lea       edx,[eax+ecx]
  004a65df  81e2ff0f0000          and       edx,0FFFh
  004a65e5  3bd7                  cmp       edx,edi
  004a65e7  7f06                  jg        short 004A65EFh
  004a65e9  66014e32              add       [esi+32h],cx
  004a65ed  eb7a                  jmp       short 004A6669h

LAB_004a65ef:
  004a65ef  81e1ff0f0000          and       ecx,0FFFh
  004a65f5  81f900080000          cmp       ecx,800h
  004a65fb  7c02                  jl        short 004A65FFh
  004a65fd  f7d8                  neg       eax

LAB_004a65ff:
  004a65ff  66014632              add       [esi+32h],ax
  004a6603  eb64                  jmp       short 004A6669h
  004a6605  25fffffffe            and       eax,0FEFFFFFFh
  004a660a  c7461804000000        mov       dword [esi+18h],4
  004a6611  0bc7                  or        eax,edi
  004a6613  8b7c241c              mov       edi,[esp+1Ch]
  004a6617  8906                  mov       [esi],eax
  004a6619  8b442440              mov       eax,[esp+40h]
  004a661d  05c0000000            add       eax,0C0h
  004a6622  c1e007                shl       eax,7
  004a6625  0fbf4e10              movsx     ecx,word [esi+10h]
  004a6629  0faff8                imul      edi,eax
  004a662c  51                    push      ecx
  004a662d  89465c                mov       [esi+5Ch],eax
  004a6630  c1ff0c                sar       edi,0Ch
  004a6633  e8e4f30900            call      00545A1Ch
  004a6638  0fafc7                imul      eax,edi
  004a663b  0fbf5610              movsx     edx,word [esi+10h]
  004a663f  8b4e50                mov       ecx,[esi+50h]
  004a6642  52                    push      edx
  004a6643  c1f80c                sar       eax,0Ch
  004a6646  03c8                  add       ecx,eax
  004a6648  894e50                mov       [esi+50h],ecx
  004a664b  e8c6f30900            call      00545A16h
  004a6650  0fafc7                imul      eax,edi
  004a6653  8b4e58                mov       ecx,[esi+58h]
  004a6656  c1f80c                sar       eax,0Ch
  004a6659  03c8                  add       ecx,eax
  004a665b  8b4654                mov       eax,[esi+54h]
  004a665e  0500900100            add       eax,19000h
  004a6663  894e58                mov       [esi+58h],ecx
  004a6666  894654                mov       [esi+54h],eax

LAB_004a6669:
  004a6669  896e5c                mov       [esi+5Ch],ebp

LAB_004a666c:
  004a666c  8b44241c              mov       eax,[esp+1Ch]
  004a6670  50                    push      eax
  004a6671  56                    push      esi
  004a6672  e8e91afeff            call      00488160h
  004a6677  83c408                add       esp,8
  004a667a  56                    push      esi
  004a667b  e8c0d20800            call      00533940h
  004a6680  83c404                add       esp,4
  004a6683  56                    push      esi
  004a6684  e867940300            call      004DFAF0h
  004a6689  8b4c2428              mov       ecx,[esp+28h]
  004a668d  83c404                add       esp,4
  004a6690  51                    push      ecx
  004a6691  56                    push      esi
  004a6692  e8d9d00300            call      004E3770h
  004a6697  8b442424              mov       eax,[esp+24h]
  004a669b  83c408                add       esp,8
  004a669e  3bc5                  cmp       eax,ebp
  004a66a0  0f843c030000          je        near 004A69E2h
  004a66a6  f70617080000          test      dword [esi],817h
  004a66ac  0f8530030000          jne       near 004A69E2h
  004a66b2  8a4643                mov       al,[esi+43h]
  004a66b5  84c0                  test      al,al
  004a66b7  0f8525030000          jne       near 004A69E2h
  004a66bd  8b466c                mov       eax,[esi+6Ch]
  004a66c0  8b5e1c                mov       ebx,[esi+1Ch]
  004a66c3  8b4e24                mov       ecx,[esi+24h]
  004a66c6  8b781c                mov       edi,[eax+1Ch]
  004a66c9  8b6824                mov       ebp,[eax+24h]
  004a66cc  2bfb                  sub       edi,ebx
  004a66ce  2be9                  sub       ebp,ecx
  004a66d0  c1ff0c                sar       edi,0Ch
  004a66d3  c1fd0c                sar       ebp,0Ch
  004a66d6  8bd7                  mov       edx,edi
  004a66d8  8bc5                  mov       eax,ebp
  004a66da  0fafd7                imul      edx,edi
  004a66dd  0fafc5                imul      eax,ebp
  004a66e0  03d0                  add       edx,eax
  004a66e2  52                    push      edx
  004a66e3  e828f30900            call      00545A10h
  004a66e8  55                    push      ebp
  004a66e9  57                    push      edi
  004a66ea  89442420              mov       [esp+20h],eax
  004a66ee  e82ff30900            call      00545A22h
  004a66f3  668b4e32              mov       cx,[esi+32h]
  004a66f7  2bc1                  sub       eax,ecx
  004a66f9  25ff0f0000            and       eax,0FFFh
  004a66fe  89442410              mov       [esp+10h],eax
  004a6702  8b442418              mov       eax,[esp+18h]
  004a6706  3dfa000000            cmp       eax,0FAh
  004a670b  0f8d26010000          jge       near 004A6837h
  004a6711  e82a0a0a00            call      00547140h
  004a6716  99                    cdq
  004a6717  b964000000            mov       ecx,64h
  004a671c  f7f9                  idiv      ecx
  004a671e  83fa2d                cmp       edx,2Dh
  004a6721  7d65                  jge       short 004A6788h
  004a6723  8b542410              mov       edx,[esp+10h]
  004a6727  8b7c2414              mov       edi,[esp+14h]
  004a672b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a6732  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a6739  25ff0f0000            and       eax,0FFFh
  004a673e  3bc1                  cmp       eax,ecx
  004a6740  0f8f38020000          jg        near 004A697Eh
  004a6746  e8f5090a00            call      00547140h
  004a674b  99                    cdq
  004a674c  8d4c3f01              lea       ecx,[edi+edi+1]
  004a6750  f7f9                  idiv      ecx
  004a6752  8b442418              mov       eax,[esp+18h]
  004a6756  83c264                add       edx,64h
  004a6759  3bc2                  cmp       eax,edx
  004a675b  0f8d1d020000          jge       near 004A697Eh
  004a6761  8b16                  mov       edx,[esi]
  004a6763  c7461803000000        mov       dword [esi+18h],3
  004a676a  81e2fffffffe          and       edx,0FEFFFFFFh
  004a6770  c6464200              mov       byte [esi+42h],0
  004a6774  83ca02                or        edx,2
  004a6777  c7465c00000000        mov       dword [esi+5Ch],0
  004a677e  8916                  mov       [esi],edx
  004a6780  5f                    pop       edi
  004a6781  5e                    pop       esi
  004a6782  5d                    pop       ebp
  004a6783  5b                    pop       ebx
  004a6784  83c42c                add       esp,2Ch
  004a6787  c3                    ret

LAB_004a6788:
  004a6788  83fa3c                cmp       edx,3Ch
  004a678b  7d2d                  jge       short 004A67BAh
  004a678d  c6464202              mov       byte [esi+42h],2
  004a6791  e8aa090a00            call      00547140h
  004a6796  99                    cdq
  004a6797  b90a000000            mov       ecx,0Ah
  004a679c  66c746106400          mov       word [esi+10h],64h
  004a67a2  f7f9                  idiv      ecx
  004a67a4  8b442414              mov       eax,[esp+14h]
  004a67a8  d1f8                  sar       eax,1
  004a67aa  02d0                  add       dl,al
  004a67ac  80c214                add       dl,14h
  004a67af  885643                mov       [esi+43h],dl
  004a67b2  5f                    pop       edi
  004a67b3  5e                    pop       esi
  004a67b4  5d                    pop       ebp
  004a67b5  5b                    pop       ebx
  004a67b6  83c42c                add       esp,2Ch
  004a67b9  c3                    ret

LAB_004a67ba:
  004a67ba  83fa4b                cmp       edx,4Bh
  004a67bd  7d24                  jge       short 004A67E3h
  004a67bf  8b4c2440              mov       ecx,[esp+40h]
  004a67c3  8b442418              mov       eax,[esp+18h]
  004a67c7  83c164                add       ecx,64h
  004a67ca  3bc1                  cmp       eax,ecx
  004a67cc  7e15                  jle       short 004A67E3h
  004a67ce  c6464204              mov       byte [esi+42h],4
  004a67d2  e869090a00            call      00547140h
  004a67d7  66894610              mov       [esi+10h],ax
  004a67db  5f                    pop       edi
  004a67dc  5e                    pop       esi
  004a67dd  5d                    pop       ebp
  004a67de  5b                    pop       ebx
  004a67df  83c42c                add       esp,2Ch
  004a67e2  c3                    ret

LAB_004a67e3:
  004a67e3  c6464203              mov       byte [esi+42h],3
  004a67e7  e854090a00            call      00547140h
  004a67ec  99                    cdq
  004a67ed  b90a000000            mov       ecx,0Ah
  004a67f2  f7f9                  idiv      ecx
  004a67f4  8b442414              mov       eax,[esp+14h]
  004a67f8  8b4c2440              mov       ecx,[esp+40h]
  004a67fc  d1f8                  sar       eax,1
  004a67fe  8db980000000          lea       edi,[ecx+80h]
  004a6804  c1e704                shl       edi,4
  004a6807  02d0                  add       dl,al
  004a6809  80c214                add       dl,14h
  004a680c  885643                mov       [esi+43h],dl
  004a680f  e82c090a00            call      00547140h
  004a6814  99                    cdq
  004a6815  f7ff                  idiv      edi
  004a6817  d1ff                  sar       edi,1
  004a6819  2bd7                  sub       edx,edi
  004a681b  8b7c2410              mov       edi,[esp+10h]
  004a681f  03d7                  add       edx,edi
  004a6821  66035632              add       dx,[esi+32h]
  004a6825  81c200080000          add       edx,800h
  004a682b  66895610              mov       [esi+10h],dx
  004a682f  5f                    pop       edi
  004a6830  5e                    pop       esi
  004a6831  5d                    pop       ebp
  004a6832  5b                    pop       ebx
  004a6833  83c42c                add       esp,2Ch
  004a6836  c3                    ret

LAB_004a6837:
  004a6837  3df4010000            cmp       eax,1F4h
  004a683c  0f8dd1000000          jge       near 004A6913h
  004a6842  e8f9080a00            call      00547140h
  004a6847  99                    cdq
  004a6848  b964000000            mov       ecx,64h
  004a684d  f7f9                  idiv      ecx
  004a684f  8b442440              mov       eax,[esp+40h]
  004a6853  d1f8                  sar       eax,1
  004a6855  83c008                add       eax,8
  004a6858  3bd0                  cmp       edx,eax
  004a685a  7d2e                  jge       short 004A688Ah
  004a685c  8b4c2410              mov       ecx,[esp+10h]
  004a6860  81c100040000          add       ecx,400h
  004a6866  81e1ff0f0000          and       ecx,0FFFh
  004a686c  81f900080000          cmp       ecx,800h
  004a6872  0f8d06010000          jge       near 004A697Eh
  004a6878  8b16                  mov       edx,[esi]
  004a687a  81e2fffffffe          and       edx,0FEFFFFFFh
  004a6880  83ca02                or        edx,2
  004a6883  8916                  mov       [esi],edx
  004a6885  e9da000000            jmp       004A6964h

LAB_004a688a:
  004a688a  83fa46                cmp       edx,46h
  004a688d  7d4e                  jge       short 004A68DDh
  004a688f  c6464203              mov       byte [esi+42h],3
  004a6893  e8a8080a00            call      00547140h
  004a6898  99                    cdq
  004a6899  b90a000000            mov       ecx,0Ah
  004a689e  f7f9                  idiv      ecx
  004a68a0  8b442414              mov       eax,[esp+14h]
  004a68a4  8b4c2440              mov       ecx,[esp+40h]
  004a68a8  d1f8                  sar       eax,1
  004a68aa  8db980000000          lea       edi,[ecx+80h]
  004a68b0  c1e704                shl       edi,4
  004a68b3  02d0                  add       dl,al
  004a68b5  80c214                add       dl,14h
  004a68b8  885643                mov       [esi+43h],dl
  004a68bb  e880080a00            call      00547140h
  004a68c0  99                    cdq
  004a68c1  f7ff                  idiv      edi
  004a68c3  8b4c2410              mov       ecx,[esp+10h]
  004a68c7  d1ff                  sar       edi,1
  004a68c9  2bd7                  sub       edx,edi
  004a68cb  03d1                  add       edx,ecx
  004a68cd  66035632              add       dx,[esi+32h]
  004a68d1  66895610              mov       [esi+10h],dx
  004a68d5  5f                    pop       edi
  004a68d6  5e                    pop       esi
  004a68d7  5d                    pop       ebp
  004a68d8  5b                    pop       ebx
  004a68d9  83c42c                add       esp,2Ch
  004a68dc  c3                    ret

LAB_004a68dd:
  004a68dd  83fa5a                cmp       edx,5Ah
  004a68e0  0f8dad000000          jge       near 004A6993h
  004a68e6  c6464202              mov       byte [esi+42h],2
  004a68ea  e851080a00            call      00547140h
  004a68ef  99                    cdq
  004a68f0  b90a000000            mov       ecx,0Ah
  004a68f5  66c746106400          mov       word [esi+10h],64h
  004a68fb  f7f9                  idiv      ecx
  004a68fd  8b442414              mov       eax,[esp+14h]
  004a6901  d1f8                  sar       eax,1
  004a6903  02d0                  add       dl,al
  004a6905  80c214                add       dl,14h
  004a6908  885643                mov       [esi+43h],dl
  004a690b  5f                    pop       edi
  004a690c  5e                    pop       esi
  004a690d  5d                    pop       ebp
  004a690e  5b                    pop       ebx
  004a690f  83c42c                add       esp,2Ch
  004a6912  c3                    ret

LAB_004a6913:
  004a6913  e828080a00            call      00547140h
  004a6918  99                    cdq
  004a6919  b964000000            mov       ecx,64h
  004a691e  f7f9                  idiv      ecx
  004a6920  8b442440              mov       eax,[esp+40h]
  004a6924  d1f8                  sar       eax,1
  004a6926  83c008                add       eax,8
  004a6929  3bd0                  cmp       edx,eax
  004a692b  7d61                  jge       short 004A698Eh
  004a692d  817c2418bc020000      cmp       dword [esp+18h],2BCh
  004a6935  7d47                  jge       short 004A697Eh
  004a6937  8b4c2410              mov       ecx,[esp+10h]
  004a693b  8b442414              mov       eax,[esp+14h]
  004a693f  8d948100010000        lea       edx,[ecx+eax*4+100h]
  004a6946  8d04c500020000        lea       eax,[eax*8+200h]
  004a694d  81e2ff0f0000          and       edx,0FFFh
  004a6953  3bd0                  cmp       edx,eax
  004a6955  7f27                  jg        short 004A697Eh
  004a6957  8b0e                  mov       ecx,[esi]
  004a6959  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a695f  83c902                or        ecx,2
  004a6962  890e                  mov       [esi],ecx

LAB_004a6964:
  004a6964  c7461803000000        mov       dword [esi+18h],3
  004a696b  c6464201              mov       byte [esi+42h],1
  004a696f  c7465c00000000        mov       dword [esi+5Ch],0
  004a6976  5f                    pop       edi
  004a6977  5e                    pop       esi
  004a6978  5d                    pop       ebp
  004a6979  5b                    pop       ebx
  004a697a  83c42c                add       esp,2Ch
  004a697d  c3                    ret

LAB_004a697e:
  004a697e  c6464201              mov       byte [esi+42h],1
  004a6982  c6464301              mov       byte [esi+43h],1
  004a6986  5f                    pop       edi
  004a6987  5e                    pop       esi
  004a6988  5d                    pop       ebp
  004a6989  5b                    pop       ebx
  004a698a  83c42c                add       esp,2Ch
  004a698d  c3                    ret

LAB_004a698e:
  004a698e  83fa3c                cmp       edx,3Ch
  004a6991  7d27                  jge       short 004A69BAh

LAB_004a6993:
  004a6993  c6464200              mov       byte [esi+42h],0
  004a6997  e8a4070a00            call      00547140h
  004a699c  99                    cdq
  004a699d  b90a000000            mov       ecx,0Ah
  004a69a2  f7f9                  idiv      ecx
  004a69a4  8b442414              mov       eax,[esp+14h]
  004a69a8  d1f8                  sar       eax,1
  004a69aa  02d0                  add       dl,al
  004a69ac  80c214                add       dl,14h
  004a69af  885643                mov       [esi+43h],dl
  004a69b2  5f                    pop       edi
  004a69b3  5e                    pop       esi
  004a69b4  5d                    pop       ebp
  004a69b5  5b                    pop       ebx
  004a69b6  83c42c                add       esp,2Ch
  004a69b9  c3                    ret

LAB_004a69ba:
  004a69ba  c6464203              mov       byte [esi+42h],3
  004a69be  e87d070a00            call      00547140h
  004a69c3  99                    cdq
  004a69c4  b90a000000            mov       ecx,0Ah
  004a69c9  f7f9                  idiv      ecx
  004a69cb  8b442414              mov       eax,[esp+14h]
  004a69cf  d1f8                  sar       eax,1
  004a69d1  02d0                  add       dl,al
  004a69d3  80c214                add       dl,14h
  004a69d6  885643                mov       [esi+43h],dl
  004a69d9  e862070a00            call      00547140h
  004a69de  66894610              mov       [esi+10h],ax

LAB_004a69e2:
  004a69e2  5f                    pop       edi
  004a69e3  5e                    pop       esi
  004a69e4  5d                    pop       ebp
  004a69e5  5b                    pop       ebx
  004a69e6  83c42c                add       esp,2Ch
  004a69e9  c3                    ret
  004a69ea  8bff                  mov       edi,edi
  004a69ec  64644a                dec       edx
  004a69ef  009c644a00dc64        add       [esp+64DC004Ah],bl
  004a69f6  4a                    dec       edx
  004a69f7  0064654a              add       [ebp+4Ah],ah
  004a69fb  0005664a00            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
