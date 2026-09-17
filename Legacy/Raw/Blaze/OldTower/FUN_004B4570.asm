Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b4570  section .text  offset fichier 0xb3970  2112 octets

  004b4570  83ec1c                sub       esp,1Ch
  004b4573  8d442410              lea       eax,[esp+10h]
  004b4577  8d4c2414              lea       ecx,[esp+14h]
  004b457b  8d54240c              lea       edx,[esp+0Ch]
  004b457f  53                    push      ebx
  004b4580  55                    push      ebp
  004b4581  56                    push      esi
  004b4582  8b74242c              mov       esi,[esp+2Ch]
  004b4586  57                    push      edi
  004b4587  50                    push      eax
  004b4588  51                    push      ecx
  004b4589  52                    push      edx
  004b458a  56                    push      esi
  004b458b  e8f0430800            call      00538980h
  004b4590  83c410                add       esp,10h
  004b4593  8d442418              lea       eax,[esp+18h]
  004b4597  8d4c2414              lea       ecx,[esp+14h]
  004b459b  8d542430              lea       edx,[esp+30h]
  004b459f  50                    push      eax
  004b45a0  51                    push      ecx
  004b45a1  8d442418              lea       eax,[esp+18h]
  004b45a5  52                    push      edx
  004b45a6  8d4c2434              lea       ecx,[esp+34h]
  004b45aa  50                    push      eax
  004b45ab  51                    push      ecx
  004b45ac  56                    push      esi
  004b45ad  e86e190800            call      00535F20h
  004b45b2  8b44242c              mov       eax,[esp+2Ch]
  004b45b6  33ed                  xor       ebp,ebp
  004b45b8  83c418                add       esp,18h
  004b45bb  3bc5                  cmp       eax,ebp
  004b45bd  bf01000000            mov       edi,1
  004b45c2  0f847c050000          je        near 004B4B44h
  004b45c8  56                    push      esi
  004b45c9  e8d26f0200            call      004DB5A0h
  004b45ce  8b06                  mov       eax,[esi]
  004b45d0  83c404                add       esp,4
  004b45d3  a802                  test      al,2
  004b45d5  0f8474020000          je        near 004B484Fh
  004b45db  8a4c2418              mov       cl,[esp+18h]
  004b45df  bb10000000            mov       ebx,10h
  004b45e4  84cb                  test      bl,cl
  004b45e6  740d                  je        short 004B45F5h
  004b45e8  24fd                  and       al,0FDh
  004b45ea  c6464300              mov       byte [esi+43h],0
  004b45ee  8906                  mov       [esi],eax
  004b45f0  e94f050000            jmp       004B4B44h

LAB_004b45f5:
  004b45f5  33c9                  xor       ecx,ecx
  004b45f7  8a4e42                mov       cl,[esi+42h]
  004b45fa  2bcd                  sub       ecx,ebp
  004b45fc  0f8457010000          je        near 004B4759h
  004b4602  49                    dec       ecx
  004b4603  0f8450010000          je        near 004B4759h
  004b4609  49                    dec       ecx
  004b460a  0f8534050000          jne       near 004B4B44h
  004b4610  668b5610              mov       dx,[esi+10h]
  004b4614  0fbfca                movsx     ecx,dx
  004b4617  2bcd                  sub       ecx,ebp
  004b4619  0f84ac000000          je        near 004B46CBh
  004b461f  49                    dec       ecx
  004b4620  7457                  je        short 004B4679h
  004b4622  49                    dec       ecx
  004b4623  0f851b050000          jne       near 004B4B44h
  004b4629  8b465c                mov       eax,[esi+5Ch]
  004b462c  8bc8                  mov       ecx,eax
  004b462e  c1f902                sar       ecx,2
  004b4631  2bc1                  sub       eax,ecx
  004b4633  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004b4638  89465c                mov       [esi+5Ch],eax
  004b463b  7505                  jne       short 004B4642h
  004b463d  4a                    dec       edx
  004b463e  66895610              mov       [esi+10h],dx

LAB_004b4642:
  004b4642  8b442430              mov       eax,[esp+30h]
  004b4646  8d5040                lea       edx,[eax+40h]
  004b4649  81e2ff0f0000          and       edx,0FFFh
  004b464f  81fa80000000          cmp       edx,80h
  004b4655  0f8ee5040000          jle       near 004B4B40h
  004b465b  25ff0f0000            and       eax,0FFFh
  004b4660  33c9                  xor       ecx,ecx
  004b4662  3d00080000            cmp       eax,800h
  004b4667  0f9dc1                setge     cl
  004b466a  49                    dec       ecx
  004b466b  81e180000000          and       ecx,80h
  004b4671  83c1c0                add       ecx,0FFFFFFC0h
  004b4674  e9c5040000            jmp       004B4B3Eh

LAB_004b4679:
  004b4679  0fbf5632              movsx     edx,word [esi+32h]
  004b467d  52                    push      edx
  004b467e  c7465c00000300        mov       dword [esi+5Ch],30000h
  004b4685  e892130900            call      00545A1Ch
  004b468a  0faf465c              imul      eax,[esi+5Ch]
  004b468e  8b5650                mov       edx,[esi+50h]
  004b4691  c1f80c                sar       eax,0Ch
  004b4694  03d0                  add       edx,eax
  004b4696  0fbf4632              movsx     eax,word [esi+32h]
  004b469a  50                    push      eax
  004b469b  895650                mov       [esi+50h],edx
  004b469e  e873130900            call      00545A16h
  004b46a3  0faf465c              imul      eax,[esi+5Ch]
  004b46a7  8b5658                mov       edx,[esi+58h]
  004b46aa  8b0e                  mov       ecx,[esi]
  004b46ac  c1f80c                sar       eax,0Ch
  004b46af  03d0                  add       edx,eax
  004b46b1  0bcf                  or        ecx,edi
  004b46b3  66ff4e10              dec       word [esi+10h]
  004b46b7  895658                mov       [esi+58h],edx
  004b46ba  c7465400800100        mov       dword [esi+54h],18000h
  004b46c1  890e                  mov       [esi],ecx
  004b46c3  896e5c                mov       [esi+5Ch],ebp
  004b46c6  e979040000            jmp       004B4B44h

LAB_004b46cb:
  004b46cb  a801                  test      al,1
  004b46cd  7476                  je        short 004B4745h
  004b46cf  396e54                cmp       [esi+54h],ebp
  004b46d2  7c0f                  jl        short 004B46E3h
  004b46d4  b80e000000            mov       eax,0Eh
  004b46d9  6639461a              cmp       [esi+1Ah],ax
  004b46dd  7e04                  jle       short 004B46E3h
  004b46df  6689461a              mov       [esi+1Ah],ax

LAB_004b46e3:
  004b46e3  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004b46e8  0f8c56040000          jl        near 004B4B44h
  004b46ee  55                    push      ebp
  004b46ef  8d4e1c                lea       ecx,[esi+1Ch]
  004b46f2  68982a5500            push      552A98h
  004b46f7  51                    push      ecx
  004b46f8  56                    push      esi
  004b46f9  e832550600            call      00519C30h
  004b46fe  8bf8                  mov       edi,eax
  004b4700  a1b8e05d00            mov       eax,[5DE0B8h]
  004b4705  83c410                add       esp,10h
  004b4708  3bc5                  cmp       eax,ebp
  004b470a  0f8434040000          je        near 004B4B44h
  004b4710  66899f96000000        mov       [edi+96h],bx
  004b4717  8b1f                  mov       ebx,[edi]
  004b4719  83cb40                or        ebx,40h
  004b471c  57                    push      edi
  004b471d  56                    push      esi
  004b471e  891f                  mov       [edi],ebx
  004b4720  e85b540800            call      00539B80h
  004b4725  83c408                add       esp,8
  004b4728  83c71c                add       edi,1Ch
  004b472b  57                    push      edi
  004b472c  6800180000            push      1800h
  004b4731  6800030000            push      300h
  004b4736  6a03                  push      3
  004b4738  e8e3930800            call      0053DB20h
  004b473d  83c410                add       esp,10h
  004b4740  e9ff030000            jmp       004B4B44h

LAB_004b4745:
  004b4745  a900000002            test      eax,2000000h
  004b474a  0f84f4030000          je        near 004B4B44h
  004b4750  24fd                  and       al,0FDh
  004b4752  8906                  mov       [esi],eax
  004b4754  e9eb030000            jmp       004B4B44h

LAB_004b4759:
  004b4759  668b4e1a              mov       cx,[esi+1Ah]
  004b475d  6683f90a              cmp       cx,0Ah
  004b4761  7f3c                  jg        short 004B479Fh
  004b4763  8b442430              mov       eax,[esp+30h]
  004b4767  8d5040                lea       edx,[eax+40h]
  004b476a  81e2ff0f0000          and       edx,0FFFh
  004b4770  81fa80000000          cmp       edx,80h
  004b4776  7f06                  jg        short 004B477Eh
  004b4778  66014632              add       [esi+32h],ax
  004b477c  eb21                  jmp       short 004B479Fh

LAB_004b477e:
  004b477e  25ff0f0000            and       eax,0FFFh
  004b4783  33d2                  xor       edx,edx
  004b4785  3d00080000            cmp       eax,800h
  004b478a  0f9dc2                setge     dl
  004b478d  4a                    dec       edx
  004b478e  81e280000000          and       edx,80h
  004b4794  83c2c0                add       edx,0FFFFFFC0h
  004b4797  8bc2                  mov       eax,edx
  004b4799  66014632              add       [esi+32h],ax
  004b479d  33ed                  xor       ebp,ebp

LAB_004b479f:
  004b479f  6683f90b              cmp       cx,0Bh
  004b47a3  753c                  jne       short 004B47E1h
  004b47a5  0fbf4632              movsx     eax,word [esi+32h]
  004b47a9  50                    push      eax
  004b47aa  c7465c00c00100        mov       dword [esi+5Ch],1C000h
  004b47b1  e866120900            call      00545A1Ch
  004b47b6  0faf465c              imul      eax,[esi+5Ch]
  004b47ba  8b4e50                mov       ecx,[esi+50h]
  004b47bd  c1f80c                sar       eax,0Ch
  004b47c0  03c8                  add       ecx,eax
  004b47c2  894e50                mov       [esi+50h],ecx
  004b47c5  0fbf4e32              movsx     ecx,word [esi+32h]
  004b47c9  51                    push      ecx
  004b47ca  e847120900            call      00545A16h
  004b47cf  0faf465c              imul      eax,[esi+5Ch]
  004b47d3  8b4e58                mov       ecx,[esi+58h]
  004b47d6  896e5c                mov       [esi+5Ch],ebp
  004b47d9  c1f80c                sar       eax,0Ch
  004b47dc  03c8                  add       ecx,eax
  004b47de  894e58                mov       [esi+58h],ecx

LAB_004b47e1:
  004b47e1  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004b47e6  7c4e                  jl        short 004B4836h
  004b47e8  55                    push      ebp
  004b47e9  8d561c                lea       edx,[esi+1Ch]
  004b47ec  68982a5500            push      552A98h
  004b47f1  52                    push      edx
  004b47f2  56                    push      esi
  004b47f3  e838540600            call      00519C30h
  004b47f8  8bf8                  mov       edi,eax
  004b47fa  a1b8e05d00            mov       eax,[5DE0B8h]
  004b47ff  83c410                add       esp,10h
  004b4802  3bc5                  cmp       eax,ebp
  004b4804  7430                  je        short 004B4836h
  004b4806  66899f96000000        mov       [edi+96h],bx
  004b480d  8b1f                  mov       ebx,[edi]
  004b480f  83cb40                or        ebx,40h
  004b4812  57                    push      edi
  004b4813  56                    push      esi
  004b4814  891f                  mov       [edi],ebx
  004b4816  e865530800            call      00539B80h
  004b481b  83c408                add       esp,8
  004b481e  83c71c                add       edi,1Ch
  004b4821  57                    push      edi
  004b4822  6800180000            push      1800h
  004b4827  6800030000            push      300h
  004b482c  6a03                  push      3
  004b482e  e8ed920800            call      0053DB20h
  004b4833  83c410                add       esp,10h

LAB_004b4836:
  004b4836  8b06                  mov       eax,[esi]
  004b4838  a900000002            test      eax,2000000h
  004b483d  0f84f8010000          je        near 004B4A3Bh
  004b4843  24fd                  and       al,0FDh
  004b4845  896e5c                mov       [esi+5Ch],ebp
  004b4848  8906                  mov       [esi],eax
  004b484a  e9f5020000            jmp       004B4B44h

LAB_004b484f:
  004b484f  a810                  test      al,10h
  004b4851  0f85ed020000          jne       near 004B4B44h
  004b4857  a904080000            test      eax,804h
  004b485c  0f842b010000          je        near 004B498Dh
  004b4862  f7442418e8030040      test      dword [esp+18h],400003E8h
  004b486a  740b                  je        short 004B4877h
  004b486c  25fbf7ffff            and       eax,0FFFFF7FBh
  004b4871  c6464300              mov       byte [esi+43h],0
  004b4875  8906                  mov       [esi],eax

LAB_004b4877:
  004b4877  668b4610              mov       ax,[esi+10h]
  004b487b  0fbfc8                movsx     ecx,ax
  004b487e  83f903                cmp       ecx,3
  004b4881  0f87bd020000          ja        near 004B4B44h
  004b4887  ff248d884d4b00        jmp       dword [ecx*4+4B4D88h]
  004b488e  8b06                  mov       eax,[esi]
  004b4890  a900000002            test      eax,2000000h
  004b4895  0f84a9020000          je        near 004B4B44h
  004b489b  80e4f7                and       ah,0F7h
  004b489e  8906                  mov       [esi],eax
  004b48a0  e99f020000            jmp       004B4B44h
  004b48a5  b806000000            mov       eax,6
  004b48aa  66394618              cmp       [esi+18h],ax
  004b48ae  7411                  je        short 004B48C1h
  004b48b0  894618                mov       [esi+18h],eax
  004b48b3  8b06                  mov       eax,[esi]
  004b48b5  25fbfffffe            and       eax,0FEFFFFFBh
  004b48ba  8906                  mov       [esi],eax
  004b48bc  e983020000            jmp       004B4B44h

LAB_004b48c1:
  004b48c1  66837e1a13            cmp       word [esi+1Ah],13h
  004b48c6  0f8578020000          jne       near 004B4B44h
  004b48cc  33c0                  xor       eax,eax
  004b48ce  6aff                  push      0FFFFFFFFh
  004b48d0  668b4606              mov       ax,[esi+6]
  004b48d4  6aff                  push      0FFFFFFFFh
  004b48d6  8bc8                  mov       ecx,eax
  004b48d8  c1f808                sar       eax,8
  004b48db  81e1ff000000          and       ecx,0FFh
  004b48e1  25ff000000            and       eax,0FFh
  004b48e6  51                    push      ecx
  004b48e7  50                    push      eax
  004b48e8  56                    push      esi
  004b48e9  e892600800            call      0053A980h
  004b48ee  83c414                add       esp,14h
  004b48f1  66ff4e10              dec       word [esi+10h]
  004b48f5  e94a020000            jmp       004B4B44h
  004b48fa  8a4e43                mov       cl,[esi+43h]
  004b48fd  fec9                  dec       cl
  004b48ff  884e43                mov       [esi+43h],cl
  004b4902  7505                  jne       short 004B4909h
  004b4904  48                    dec       eax
  004b4905  66894610              mov       [esi+10h],ax

LAB_004b4909:
  004b4909  8b442430              mov       eax,[esp+30h]
  004b490d  8d9080000000          lea       edx,[eax+80h]
  004b4913  81e2ff0f0000          and       edx,0FFFh
  004b4919  81fa00010000          cmp       edx,100h
  004b491f  0f8e1b020000          jle       near 004B4B40h
  004b4925  e9fb010000            jmp       004B4B25h
  004b492a  55                    push      ebp
  004b492b  55                    push      ebp
  004b492c  68bd000000            push      0BDh
  004b4931  56                    push      esi
  004b4932  e8a961fcff            call      0047AAE0h
  004b4937  83c410                add       esp,10h
  004b493a  e801280900            call      00547140h
  004b493f  99                    cdq
  004b4940  b906000000            mov       ecx,6
  004b4945  f7f9                  idiv      ecx
  004b4947  33c0                  xor       eax,eax
  004b4949  668b0455b02a5500      mov       ax,[edx*2+552AB0h]
  004b4951  66894606              mov       [esi+6],ax
  004b4955  8b0dc4975e00          mov       ecx,[5E97C4h]
  004b495b  8bd0                  mov       edx,eax
  004b495d  25ff000000            and       eax,0FFh
  004b4962  8b899c000000          mov       ecx,[ecx+9Ch]
  004b4968  c1fa08                sar       edx,8
  004b496b  81e2ff000000          and       edx,0FFh
  004b4971  8d0440                lea       eax,[eax+eax*2]
  004b4974  c1e004                shl       eax,4
  004b4977  8b0c91                mov       ecx,[ecx+edx*4]
  004b497a  8a540113              mov       dl,[ecx+eax+13h]
  004b497e  66ff4e10              dec       word [esi+10h]
  004b4982  885643                mov       [esi+43h],dl
  004b4985  896e5c                mov       [esi+5Ch],ebp
  004b4988  e9b7010000            jmp       004B4B44h

LAB_004b498d:
  004b498d  a801                  test      al,1
  004b498f  0f85af010000          jne       near 004B4B44h
  004b4995  33c9                  xor       ecx,ecx
  004b4997  8a4e42                mov       cl,[esi+42h]
  004b499a  83f903                cmp       ecx,3
  004b499d  0f87a1010000          ja        near 004B4B44h
  004b49a3  ff248d984d4b00        jmp       dword [ecx*4+4B4D98h]
  004b49aa  66396e18              cmp       [esi+18h],bp
  004b49ae  740a                  je        short 004B49BAh
  004b49b0  0d00000001            or        eax,1000000h
  004b49b5  896e18                mov       [esi+18h],ebp
  004b49b8  8906                  mov       [esi],eax

LAB_004b49ba:
  004b49ba  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b49c2  7d09                  jge       short 004B49CDh
  004b49c4  c6464300              mov       byte [esi+43h],0
  004b49c8  e977010000            jmp       004B4B44h

LAB_004b49cd:
  004b49cd  8a4643                mov       al,[esi+43h]
  004b49d0  84c0                  test      al,al
  004b49d2  7467                  je        short 004B4A3Bh
  004b49d4  fec8                  dec       al
  004b49d6  896e5c                mov       [esi+5Ch],ebp
  004b49d9  884643                mov       [esi+43h],al
  004b49dc  e963010000            jmp       004B4B44h
  004b49e1  66397e18              cmp       [esi+18h],di
  004b49e5  740a                  je        short 004B49F1h
  004b49e7  0d00000001            or        eax,1000000h
  004b49ec  897e18                mov       [esi+18h],edi
  004b49ef  8906                  mov       [esi],eax

LAB_004b49f1:
  004b49f1  8b442430              mov       eax,[esp+30h]
  004b49f5  8d8800010000          lea       ecx,[eax+100h]
  004b49fb  81e1ff0f0000          and       ecx,0FFFh
  004b4a01  81f900020000          cmp       ecx,200h
  004b4a07  7f10                  jg        short 004B4A19h
  004b4a09  66014632              add       [esi+32h],ax
  004b4a0d  c6464300              mov       byte [esi+43h],0
  004b4a11  896e5c                mov       [esi+5Ch],ebp
  004b4a14  e92b010000            jmp       004B4B44h

LAB_004b4a19:
  004b4a19  25ff0f0000            and       eax,0FFFh
  004b4a1e  33d2                  xor       edx,edx
  004b4a20  3d00080000            cmp       eax,800h
  004b4a25  0f9dc2                setge     dl
  004b4a28  4a                    dec       edx
  004b4a29  81e200020000          and       edx,200h
  004b4a2f  81c200ffffff          add       edx,0FFFFFF00h
  004b4a35  8bc2                  mov       eax,edx
  004b4a37  66014632              add       [esi+32h],ax

LAB_004b4a3b:
  004b4a3b  896e5c                mov       [esi+5Ch],ebp
  004b4a3e  e901010000            jmp       004B4B44h
  004b4a43  66397e18              cmp       [esi+18h],di
  004b4a47  740a                  je        short 004B4A53h
  004b4a49  0d00000001            or        eax,1000000h
  004b4a4e  897e18                mov       [esi+18h],edi
  004b4a51  8906                  mov       [esi],eax

LAB_004b4a53:
  004b4a53  0fbf4610              movsx     eax,word [esi+10h]
  004b4a57  39442410              cmp       [esp+10h],eax
  004b4a5b  7d09                  jge       short 004B4A66h
  004b4a5d  c6464300              mov       byte [esi+43h],0
  004b4a61  e9de000000            jmp       004B4B44h

LAB_004b4a66:
  004b4a66  8a4643                mov       al,[esi+43h]
  004b4a69  84c0                  test      al,al
  004b4a6b  7405                  je        short 004B4A72h
  004b4a6d  fec8                  dec       al
  004b4a6f  884643                mov       [esi+43h],al

LAB_004b4a72:
  004b4a72  8b442430              mov       eax,[esp+30h]
  004b4a76  8d8880000000          lea       ecx,[eax+80h]
  004b4a7c  81e1ff0f0000          and       ecx,0FFFh
  004b4a82  81f900010000          cmp       ecx,100h
  004b4a88  7e1b                  jle       short 004B4AA5h
  004b4a8a  25ff0f0000            and       eax,0FFFh
  004b4a8f  33d2                  xor       edx,edx
  004b4a91  3d00080000            cmp       eax,800h
  004b4a96  0f9dc2                setge     dl
  004b4a99  4a                    dec       edx
  004b4a9a  81e200010000          and       edx,100h
  004b4aa0  83c280                add       edx,0FFFFFF80h
  004b4aa3  8bc2                  mov       eax,edx

LAB_004b4aa5:
  004b4aa5  66014632              add       [esi+32h],ax
  004b4aa9  c7465c00400100        mov       dword [esi+5Ch],14000h
  004b4ab0  e98f000000            jmp       004B4B44h
  004b4ab5  8a4e43                mov       cl,[esi+43h]
  004b4ab8  84c9                  test      cl,cl
  004b4aba  7405                  je        short 004B4AC1h
  004b4abc  fec9                  dec       cl
  004b4abe  884e43                mov       [esi+43h],cl

LAB_004b4ac1:
  004b4ac1  66397e18              cmp       [esi+18h],di
  004b4ac5  740a                  je        short 004B4AD1h
  004b4ac7  0d00000001            or        eax,1000000h
  004b4acc  897e18                mov       [esi+18h],edi
  004b4acf  8906                  mov       [esi],eax

LAB_004b4ad1:
  004b4ad1  0fbf4610              movsx     eax,word [esi+10h]
  004b4ad5  50                    push      eax
  004b4ad6  c7465c00000100        mov       dword [esi+5Ch],10000h
  004b4add  e83a0f0900            call      00545A1Ch
  004b4ae2  0faf465c              imul      eax,[esi+5Ch]
  004b4ae6  0fbf4e10              movsx     ecx,word [esi+10h]
  004b4aea  8b7e1c                mov       edi,[esi+1Ch]
  004b4aed  51                    push      ecx
  004b4aee  c1f80c                sar       eax,0Ch
  004b4af1  03f8                  add       edi,eax
  004b4af3  897e1c                mov       [esi+1Ch],edi
  004b4af6  e81b0f0900            call      00545A16h
  004b4afb  0faf465c              imul      eax,[esi+5Ch]
  004b4aff  8b5e24                mov       ebx,[esi+24h]
  004b4b02  896e5c                mov       [esi+5Ch],ebp
  004b4b05  c1f80c                sar       eax,0Ch
  004b4b08  03d8                  add       ebx,eax
  004b4b0a  8b442430              mov       eax,[esp+30h]
  004b4b0e  895e24                mov       [esi+24h],ebx
  004b4b11  8d9080000000          lea       edx,[eax+80h]
  004b4b17  81e2ff0f0000          and       edx,0FFFh
  004b4b1d  81fa00010000          cmp       edx,100h
  004b4b23  7e1b                  jle       short 004B4B40h

LAB_004b4b25:
  004b4b25  25ff0f0000            and       eax,0FFFh
  004b4b2a  33c9                  xor       ecx,ecx
  004b4b2c  3d00080000            cmp       eax,800h
  004b4b31  0f9dc1                setge     cl
  004b4b34  49                    dec       ecx
  004b4b35  81e100010000          and       ecx,100h
  004b4b3b  83c180                add       ecx,0FFFFFF80h

LAB_004b4b3e:
  004b4b3e  8bc1                  mov       eax,ecx

LAB_004b4b40:
  004b4b40  66014632              add       [esi+32h],ax

LAB_004b4b44:
  004b4b44  8b542414              mov       edx,[esp+14h]
  004b4b48  66c7869a0000000060    mov       word [esi+9Ah],6000h
  004b4b51  52                    push      edx
  004b4b52  56                    push      esi
  004b4b53  e80836fdff            call      00488160h
  004b4b58  83c408                add       esp,8
  004b4b5b  56                    push      esi
  004b4b5c  e8dfed0700            call      00533940h
  004b4b61  83c404                add       esp,4
  004b4b64  56                    push      esi
  004b4b65  e886af0200            call      004DFAF0h
  004b4b6a  8b442420              mov       eax,[esp+20h]
  004b4b6e  83c404                add       esp,4
  004b4b71  50                    push      eax
  004b4b72  56                    push      esi
  004b4b73  e8f8eb0200            call      004E3770h
  004b4b78  8b44241c              mov       eax,[esp+1Ch]
  004b4b7c  83c408                add       esp,8
  004b4b7f  3bc5                  cmp       eax,ebp
  004b4b81  0f84f7010000          je        near 004B4D7Eh
  004b4b87  f70617080000          test      dword [esi],817h
  004b4b8d  0f85eb010000          jne       near 004B4D7Eh
  004b4b93  8a4643                mov       al,[esi+43h]
  004b4b96  84c0                  test      al,al
  004b4b98  0f85e0010000          jne       near 004B4D7Eh
  004b4b9e  8b466c                mov       eax,[esi+6Ch]
  004b4ba1  8b5e1c                mov       ebx,[esi+1Ch]
  004b4ba4  8b4e24                mov       ecx,[esi+24h]
  004b4ba7  8b781c                mov       edi,[eax+1Ch]
  004b4baa  8b6824                mov       ebp,[eax+24h]
  004b4bad  2bfb                  sub       edi,ebx
  004b4baf  2be9                  sub       ebp,ecx
  004b4bb1  c1ff0c                sar       edi,0Ch
  004b4bb4  c1fd0c                sar       ebp,0Ch
  004b4bb7  8bcf                  mov       ecx,edi
  004b4bb9  8bd5                  mov       edx,ebp
  004b4bbb  0fafcf                imul      ecx,edi
  004b4bbe  0fafd5                imul      edx,ebp
  004b4bc1  03ca                  add       ecx,edx
  004b4bc3  51                    push      ecx
  004b4bc4  e8470e0900            call      00545A10h
  004b4bc9  55                    push      ebp
  004b4bca  57                    push      edi
  004b4bcb  89442418              mov       [esp+18h],eax
  004b4bcf  e84e0e0900            call      00545A22h
  004b4bd4  668b4e32              mov       cx,[esi+32h]
  004b4bd8  2bc1                  sub       eax,ecx
  004b4bda  25ff0f0000            and       eax,0FFFh
  004b4bdf  89442430              mov       [esp+30h],eax
  004b4be3  e858250900            call      00547140h
  004b4be8  99                    cdq
  004b4be9  b964000000            mov       ecx,64h
  004b4bee  f7f9                  idiv      ecx
  004b4bf0  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b4bf8  7d73                  jge       short 004B4C6Dh
  004b4bfa  83fa32                cmp       edx,32h
  004b4bfd  7d40                  jge       short 004B4C3Fh
  004b4bff  8b542430              mov       edx,[esp+30h]
  004b4c03  81c200010000          add       edx,100h
  004b4c09  81e2ff0f0000          and       edx,0FFFh
  004b4c0f  81fa00020000          cmp       edx,200h
  004b4c15  0f8fce000000          jg        near 004B4CE9h
  004b4c1b  8b06                  mov       eax,[esi]
  004b4c1d  c7461803000000        mov       dword [esi+18h],3
  004b4c24  25fffffffe            and       eax,0FEFFFFFFh
  004b4c29  c6464200              mov       byte [esi+42h],0
  004b4c2d  0c02                  or        al,2
  004b4c2f  66c746100200          mov       word [esi+10h],2
  004b4c35  8906                  mov       [esi],eax
  004b4c37  5f                    pop       edi
  004b4c38  5e                    pop       esi
  004b4c39  5d                    pop       ebp
  004b4c3a  5b                    pop       ebx
  004b4c3b  83c41c                add       esp,1Ch
  004b4c3e  c3                    ret

LAB_004b4c3f:
  004b4c3f  83fa4b                cmp       edx,4Bh
  004b4c42  0f8de0000000          jge       near 004B4D28h
  004b4c48  c6464202              mov       byte [esi+42h],2
  004b4c4c  e8ef240900            call      00547140h
  004b4c51  99                    cdq
  004b4c52  b90a000000            mov       ecx,0Ah
  004b4c57  66c74610c800          mov       word [esi+10h],0C8h
  004b4c5d  f7f9                  idiv      ecx
  004b4c5f  80c214                add       dl,14h
  004b4c62  885643                mov       [esi+43h],dl
  004b4c65  5f                    pop       edi
  004b4c66  5e                    pop       esi
  004b4c67  5d                    pop       ebp
  004b4c68  5b                    pop       ebx
  004b4c69  83c41c                add       esp,1Ch
  004b4c6c  c3                    ret

LAB_004b4c6d:
  004b4c6d  83fa1e                cmp       edx,1Eh
  004b4c70  7d3c                  jge       short 004B4CAEh
  004b4c72  8b542430              mov       edx,[esp+30h]
  004b4c76  81c200020000          add       edx,200h
  004b4c7c  81e2ff0f0000          and       edx,0FFFh
  004b4c82  81fa00040000          cmp       edx,400h
  004b4c88  7d5f                  jge       short 004B4CE9h
  004b4c8a  8b06                  mov       eax,[esi]
  004b4c8c  c7461808000000        mov       dword [esi+18h],8
  004b4c93  25fffffffe            and       eax,0FEFFFFFFh
  004b4c98  c6464202              mov       byte [esi+42h],2
  004b4c9c  0c02                  or        al,2
  004b4c9e  66c746100200          mov       word [esi+10h],2
  004b4ca4  8906                  mov       [esi],eax
  004b4ca6  5f                    pop       edi
  004b4ca7  5e                    pop       esi
  004b4ca8  5d                    pop       ebp
  004b4ca9  5b                    pop       ebx
  004b4caa  83c41c                add       esp,1Ch
  004b4cad  c3                    ret

LAB_004b4cae:
  004b4cae  83fa3c                cmp       edx,3Ch
  004b4cb1  7d46                  jge       short 004B4CF9h
  004b4cb3  8b4c2430              mov       ecx,[esp+30h]
  004b4cb7  81c100020000          add       ecx,200h
  004b4cbd  81e1ff0f0000          and       ecx,0FFFh
  004b4cc3  81f900040000          cmp       ecx,400h
  004b4cc9  7d1e                  jge       short 004B4CE9h
  004b4ccb  8b06                  mov       eax,[esi]
  004b4ccd  c7461805000000        mov       dword [esi+18h],5
  004b4cd4  0d04080001            or        eax,1000804h
  004b4cd9  66c746100300          mov       word [esi+10h],3
  004b4cdf  8906                  mov       [esi],eax
  004b4ce1  5f                    pop       edi
  004b4ce2  5e                    pop       esi
  004b4ce3  5d                    pop       ebp
  004b4ce4  5b                    pop       ebx
  004b4ce5  83c41c                add       esp,1Ch
  004b4ce8  c3                    ret

LAB_004b4ce9:
  004b4ce9  c6464201              mov       byte [esi+42h],1
  004b4ced  c6464301              mov       byte [esi+43h],1
  004b4cf1  5f                    pop       edi
  004b4cf2  5e                    pop       esi
  004b4cf3  5d                    pop       ebp
  004b4cf4  5b                    pop       ebx
  004b4cf5  83c41c                add       esp,1Ch
  004b4cf8  c3                    ret

LAB_004b4cf9:
  004b4cf9  83fa50                cmp       edx,50h
  004b4cfc  7d25                  jge       short 004B4D23h
  004b4cfe  c6464202              mov       byte [esi+42h],2
  004b4d02  e839240900            call      00547140h
  004b4d07  99                    cdq
  004b4d08  b90a000000            mov       ecx,0Ah
  004b4d0d  66c74610c800          mov       word [esi+10h],0C8h
  004b4d13  f7f9                  idiv      ecx
  004b4d15  80c214                add       dl,14h
  004b4d18  885643                mov       [esi+43h],dl
  004b4d1b  5f                    pop       edi
  004b4d1c  5e                    pop       esi
  004b4d1d  5d                    pop       ebp
  004b4d1e  5b                    pop       ebx
  004b4d1f  83c41c                add       esp,1Ch
  004b4d22  c3                    ret

LAB_004b4d23:
  004b4d23  83fa5a                cmp       edx,5Ah
  004b4d26  7d3f                  jge       short 004B4D67h

LAB_004b4d28:
  004b4d28  c6464203              mov       byte [esi+42h],3
  004b4d2c  e80f240900            call      00547140h
  004b4d31  99                    cdq
  004b4d32  b90a000000            mov       ecx,0Ah
  004b4d37  f7f9                  idiv      ecx
  004b4d39  02d1                  add       dl,cl
  004b4d3b  885643                mov       [esi+43h],dl
  004b4d3e  e8fd230900            call      00547140h
  004b4d43  99                    cdq
  004b4d44  b9000c0000            mov       ecx,0C00h
  004b4d49  8b7c2430              mov       edi,[esp+30h]
  004b4d4d  f7f9                  idiv      ecx
  004b4d4f  03d7                  add       edx,edi
  004b4d51  66035632              add       dx,[esi+32h]
  004b4d55  81c200020000          add       edx,200h
  004b4d5b  66895610              mov       [esi+10h],dx
  004b4d5f  5f                    pop       edi
  004b4d60  5e                    pop       esi
  004b4d61  5d                    pop       ebp
  004b4d62  5b                    pop       ebx
  004b4d63  83c41c                add       esp,1Ch
  004b4d66  c3                    ret

LAB_004b4d67:
  004b4d67  c6464200              mov       byte [esi+42h],0
  004b4d6b  e8d0230900            call      00547140h
  004b4d70  99                    cdq
  004b4d71  b90a000000            mov       ecx,0Ah
  004b4d76  f7f9                  idiv      ecx
  004b4d78  80c214                add       dl,14h
  004b4d7b  885643                mov       [esi+43h],dl

LAB_004b4d7e:
  004b4d7e  5f                    pop       edi
  004b4d7f  5e                    pop       esi
  004b4d80  5d                    pop       ebp
  004b4d81  5b                    pop       ebx
  004b4d82  83c41c                add       esp,1Ch
  004b4d85  c3                    ret
  004b4d86  8bff                  mov       edi,edi
  004b4d88  8e484b                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
